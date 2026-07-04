using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using SKCOMLib;
using System.Collections.Generic;

namespace CapitalQuoteService
{
    /// <summary>
    /// 群益報價中介服務
    /// 功能：處理群益 API 登入、報價訂閱，並透過 JSON 檔案與 CTA 通訊
    /// 注意：GC (黃金期貨) 等海外期貨使用 SKOSQuoteLib
    /// </summary>
    class Program
    {
        // ════════════════════════════════════════════════════════════════
        // 設定
        // ════════════════════════════════════════════════════════════════
        
        // 通訊檔案目錄
        static string DataPath;
        
        // 群益 API 物件
        static SKCenterLib m_center;
        static SKReplyLib m_reply;
        static SKOSQuoteLib m_osQuote;  // 海期報價
        static SKOrderLib m_order;      // 下單元件
        
        // 狀態
        static bool isLoggedIn = false;
        static bool isQuoteConnected = false;
        static bool isOrderInitialized = false;
        static string lastError = null;
        static Dictionary<string, string> stockExchangeMap = new Dictionary<string, string>(); // 商品 -> 交易所
        
        // 報價資料
        static string quoteSymbol = "";
        static double quoteBid = 0;
        static double quoteAsk = 0;
        static double quoteLast = 0;
        static int quoteVolume = 0;
        
        static string targetSymbol = "MGC2602"; // 預設商品 (微型黃金)
        static short currentPageNo = 0; // 當前訂閱頁面 (0-9 循環使用)
        
        // 日誌檔案
        static string LogFilePath;
        static DateTime lastQuoteUpdate = DateTime.MinValue;
        
        // Tick Size 對照表 (GC/MGC=0.1, 1OZ=0.25)
        static Dictionary<string, double> tickSizeMap = new Dictionary<string, double>()
        {
            {"GC", 0.10},
            {"MGC", 0.10},
            {"1OZ", 0.25}
        };
        
        /// <summary>
        /// 根據商品代碼取得 Tick Size
        /// </summary>
        static double GetTickSize(string symbol)
        {
            foreach (var key in tickSizeMap.Keys)
            {
                if (symbol.StartsWith(key)) return tickSizeMap[key];
            }
            return 0.01; // 預設
        }
        
        /// <summary>
        /// 將價格捨入至正確的 Tick Size
        /// </summary>
        static double RoundToTickSize(double price, string symbol)
        {
            double tickSize = GetTickSize(symbol);
            return Math.Round(price / tickSize) * tickSize;
        }
        
        // ════════════════════════════════════════════════════════════════
        // 主程式
        // ════════════════════════════════════════════════════════════════
        
        static void Main(string[] args)
        {
            // 設定資料路徑
            DataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "CapitalQuote"
            );
            
            // 建立資料目錄
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            
            // 設定日誌檔案
            LogFilePath = Path.Combine(DataPath, "log.txt");
            
            // 全域例外處理
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
            }
            catch { }
            
            Console.Title = "群益報價中介服務 v1.1 (海期版)";
            
            WriteLog("========== 程式啟動 ==========");
            
            try
            {
                RunMain(args);
            }
            catch (Exception ex)
            {
                WriteLog("主程式錯誤: " + ex.ToString());
                Console.WriteLine();
                Console.WriteLine("錯誤: " + ex.Message);
                Console.WriteLine("詳細日誌: " + LogFilePath);
                Console.WriteLine();
                Console.WriteLine("按任意鍵結束...");
                Console.ReadKey();
            }
        }
        
        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            string msg = ex != null ? ex.ToString() : "Unknown error";
            WriteLog("CRASH: " + msg);
            Console.WriteLine("程式發生錯誤，詳見: " + LogFilePath);
            Console.WriteLine("按任意鍵結束...");
            Console.ReadKey();
        }
        
        static void WriteLog(string message)
        {
            try
            {
                string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message + "\r\n";
                File.AppendAllText(LogFilePath, log);
            }
            catch { }
        }
        
        static void RunMain(string[] args)
        {
            PrintHeader();
            
            // 解析命令列參數
            string account = "";
            string password = "";
            
            if (args.Length >= 2)
            {
                account = args[0];
                password = args[1];
                if (args.Length >= 3)
                {
                    targetSymbol = args[2];
                }
            }
            else
            {
                Console.Write("請輸入帳號 (身份證字號): ");
                string input = Console.ReadLine();
                account = input != null ? input.Trim().ToUpper() : "";
                
                Console.Write("請輸入密碼: ");
                password = ReadPassword();
                
                Console.Write("請輸入期貨商品代碼 (預設 " + targetSymbol + "): ");
                string symbolInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(symbolInput))
                {
                    targetSymbol = symbolInput.Trim().ToUpper();
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("目標商品: " + targetSymbol + " (海外期貨)");
            Console.WriteLine();
            
            // 初始化 API
            if (!InitializeAPI())
            {
                Console.WriteLine("API 初始化失敗");
                UpdateStatus("error", "API 初始化失敗");
                WaitForExit();
                return;
            }
            
            // 登入
            if (!Login(account, password))
            {
                Console.WriteLine("登入失敗");
                UpdateStatus("error", lastError != null ? lastError : "登入失敗");
                WaitForExit();
                return;
            }
            
            // 訂閱報價
            if (!SubscribeQuote())
            {
                Console.WriteLine("報價訂閱失敗");
                UpdateStatus("error", "報價訂閱失敗");
                WaitForExit();
                return;
            }
            
            // 初始化下單功能 (必須在登入後)
            if (!InitializeOrder(account, password))
            {
                 Console.WriteLine("下單功能初始化失敗 (憑證可能未安裝)");
                 // 不強制退出，允許僅使用報價功能
            }
            
            Console.WriteLine();
            Console.WriteLine("════════════════════════════════════════════");
            Console.WriteLine("服務已啟動！按 Q 結束程式");
            Console.WriteLine("════════════════════════════════════════════");
            Console.WriteLine();
            
            // 訂閱檔案路徑
            // 訂閱檔案路徑
            string subscribePath = Path.Combine(DataPath, "subscribe.json");
            string orderPath = Path.Combine(DataPath, "order.json");
            string lastSubscribedSymbol = targetSymbol;
            DateTime lastSubscribeCheck = DateTime.MinValue;
            DateTime lastOrderCheck = DateTime.MinValue;
            
            // 主迴圈
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Q)
                    {
                        break;
                    }
                }
                
                // 每 2 秒檢查一次 subscribe.json
                if ((DateTime.Now - lastSubscribeCheck).TotalSeconds >= 2)
                {
                    lastSubscribeCheck = DateTime.Now;
                    CheckAndSwitchSubscription(subscribePath, ref lastSubscribedSymbol);
                }
                
                // 每 0.5 秒檢查一次 order.json
                if ((DateTime.Now - lastOrderCheck).TotalSeconds >= 0.5)
                {
                    lastOrderCheck = DateTime.Now;
                    if (isOrderInitialized) 
                    {
                        CheckAndSendOrder(orderPath, account);
                    }
                }
                
                // 更新狀態檔案
                UpdateStatus("running", null);
                
                // 定期更新 quote.json (Heartbeat) 確保檔案存在
                if ((DateTime.Now - lastQuoteUpdate).TotalSeconds > 5)
                {
                    WriteQuoteFile();
                }
                
                Thread.Sleep(100);
            }
            
            // 清理
            Cleanup();
            Console.WriteLine("服務已停止");
        }
        
        /// <summary>
        /// 檢查 subscribe.json 並在商品改變時切換訂閱
        /// </summary>
        static void CheckAndSwitchSubscription(string subscribePath, ref string lastSubscribedSymbol)
        {
            try
            {
                if (!File.Exists(subscribePath)) return;
                
                string json = File.ReadAllText(subscribePath);
                
                // 解析 symbol
                int symStart = json.IndexOf("\"symbol\"");
                if (symStart == -1) return;
                
                int colonIdx = json.IndexOf(":", symStart);
                if (colonIdx == -1) return;
                
                int quoteStart = json.IndexOf("\"", colonIdx);
                if (quoteStart == -1) return;
                
                int quoteEnd = json.IndexOf("\"", quoteStart + 1);
                if (quoteEnd == -1) return;
                
                string newSymbol = json.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
                
                if (string.IsNullOrEmpty(newSymbol) || newSymbol == lastSubscribedSymbol) return;
                
                // 商品改變，重新訂閱
                Console.WriteLine();
                Console.WriteLine("════════════════════════════════════════════");
                Console.WriteLine("偵測到商品切換: " + lastSubscribedSymbol + " -> " + newSymbol);
                WriteLog("商品切換: " + lastSubscribedSymbol + " -> " + newSymbol);
                
                targetSymbol = newSymbol;
                lastSubscribedSymbol = newSymbol;
                
                // 重新訂閱
                SwitchSubscription(newSymbol);
                
                Console.WriteLine("════════════════════════════════════════════");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                WriteLog("CheckAndSwitchSubscription 錯誤: " + ex.Message);
            }
        }
        
        /// <summary>
        /// 切換訂閱到新商品
        /// </summary>
        static void SwitchSubscription(string newSymbol)
        {
            try
            {
                // 建構訂閱字串
                string subscribeSymbol = newSymbol;
                
                // 如果是純代碼，嘗試從 Map 找交易所
                if (!newSymbol.Contains(","))
                {
                    if (stockExchangeMap.ContainsKey(newSymbol))
                    {
                        string exchange = stockExchangeMap[newSymbol];
                        subscribeSymbol = exchange + "," + newSymbol;
                    }
                    else if (newSymbol.StartsWith("GC") || newSymbol.StartsWith("MGC") || newSymbol.StartsWith("1OZ"))
                    {
                        // 黃金類商品預設 NYM
                        subscribeSymbol = "NYM," + newSymbol;
                    }
                }
                
                // 重置報價連線 (Leave -> Enter) 以清除所有舊訂閱
                Console.WriteLine("   重置報價連線...");
                WriteLog("重置報價連線 (LeaveMonitor)...");
                m_osQuote.SKOSQuoteLib_LeaveMonitor();
                isQuoteConnected = false;
                Thread.Sleep(500);
                
                WriteLog("重新連線 (EnterMonitorLONG)...");
                int result = m_osQuote.SKOSQuoteLib_EnterMonitorLONG();
                if (result != 0)
                {
                     WriteLog("重新連線失敗: " + result);
                     Console.WriteLine("   重新連線失敗 Error: " + result);
                     return;
                }
                
                // 等待連線
                int wait = 0;
                while(!isQuoteConnected && wait < 50)
                {
                    Thread.Sleep(100);
                    wait++;
                }
                
                if (isQuoteConnected)
                {
                    Console.WriteLine("   重新連線成功");
                }
                else
                {
                    Console.WriteLine("   重新連線逾時，嘗試繼續訂閱...");
                }

                Console.WriteLine("   訂閱: " + subscribeSymbol);
                WriteLog("切換訂閱: " + subscribeSymbol);
                
                // 始終使用 Page 0，因為我們已經重置了連線
                short pageNo = 0; 
                
                result = m_osQuote.SKOSQuoteLib_RequestStocks(pageNo, subscribeSymbol);
                
                if (result == 0)
                {
                    Console.WriteLine("   訂閱成功！");
                    WriteLog("訂閱成功: " + subscribeSymbol);
                    targetSymbol = subscribeSymbol;
                    // 重置 PageNo 變數 (雖已不再使用，但保持一致)
                    currentPageNo = 0;
                }
                else
                {
                    string msg = m_center.SKCenterLib_GetReturnCodeMessage(result);
                    Console.WriteLine("   訂閱失敗: " + msg);
                    WriteLog("訂閱失敗: " + msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("   訂閱錯誤: " + ex.Message);
                WriteLog("SwitchSubscription 錯誤: " + ex.ToString());
            }
        }
        
        // ════════════════════════════════════════════════════════════════
        // API 初始化
        // ════════════════════════════════════════════════════════════════
        
        static bool InitializeAPI()
        {
            try
            {
                Console.WriteLine("初始化群益 API (海期版)...");
                WriteLog("開始初始化 API");
                
                WriteLog("建立 SKCenterLib...");
                m_center = new SKCenterLib();
                WriteLog("SKCenterLib 建立成功");
                
                WriteLog("建立 SKReplyLib...");
                m_reply = new SKReplyLib();
                WriteLog("SKReplyLib 建立成功");
                
                WriteLog("建立 SKOSQuoteLib (海期報價)...");
                m_osQuote = new SKOSQuoteLib();
                WriteLog("SKOSQuoteLib 建立成功");
                
                // 註冊公告回應事件 (關鍵！解決 2017 錯誤)
                WriteLog("註冊 OnReplyMessage 事件...");
                m_reply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(OnReplyMessage);
                WriteLog("OnReplyMessage 事件已註冊");
                
                // 註冊海期報價事件
                WriteLog("註冊 OnConnect 事件...");
                m_osQuote.OnConnect += new _ISKOSQuoteLibEvents_OnConnectEventHandler(OnOSQuoteConnect);
                WriteLog("OnConnect 事件已註冊");
                
                WriteLog("註冊 OnNotifyQuoteLONG 事件...");
                m_osQuote.OnNotifyQuoteLONG += new _ISKOSQuoteLibEvents_OnNotifyQuoteLONGEventHandler(OnOSNotifyQuote);
                WriteLog("OnNotifyQuoteLONG 事件已註冊");
                
                // 註冊商品清單事件
                WriteLog("註冊 OnOverseaProducts 事件...");
                m_osQuote.OnOverseaProducts += new _ISKOSQuoteLibEvents_OnOverseaProductsEventHandler(OnOverseaProducts);
                WriteLog("OnOverseaProducts 事件已註冊");
                
                // 註冊商品清單事件 (Detail)
                WriteLog("註冊 OnOverseaProductsDetail 事件...");
                m_osQuote.OnOverseaProductsDetail += new _ISKOSQuoteLibEvents_OnOverseaProductsDetailEventHandler(OnOverseaProductsDetail);
                WriteLog("OnOverseaProductsDetail 事件已註冊");
                
                WriteLog("建立 SKOrderLib...");
                m_order = new SKOrderLib();
                WriteLog("SKOrderLib 建立成功");
                
                // 註冊帳號回應 (必要的)
                m_order.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(OnAccount);

                // [NEW] 註冊非同步委託回報 (包含錯誤訊息)
                m_order.OnAsyncOrder += new _ISKOrderLibEvents_OnAsyncOrderEventHandler(OnAsyncOrder);
                WriteLog("OnAsyncOrder 事件已註冊");
                
                Console.WriteLine("API 初始化成功 (海期版)");
                WriteLog("API 初始化完成");
                return true;
            }
            catch (Exception ex)
            {
                WriteLog("API 初始化錯誤: " + ex.ToString());
                Console.WriteLine("API 初始化錯誤: " + ex.Message);
                lastError = ex.Message;
                return false;
            }
        }
        
        

        
        

        
        // ════════════════════════════════════════════════════════════════
        // 登入
        // ════════════════════════════════════════════════════════════════
        
        static bool Login(string account, string password)
        {
            try
            {
                Console.WriteLine("正在登入... 帳號: " + account);
                WriteLog("開始登入: " + account);
                
                int result = m_center.SKCenterLib_Login(account, password);
                
                Console.WriteLine("   登入結果: " + result);
                WriteLog("登入結果: " + result);
                
                if (result == 0)
                {
                    Console.WriteLine("登入成功！");
                    WriteLog("登入成功");
                    isLoggedIn = true;
                    return true;
                }
                else if (result >= 600 && result <= 699)
                {
                    // 部分成功（憑證警告等）
                    string msg = m_center.SKCenterLib_GetReturnCodeMessage(result);
                    Console.WriteLine("登入警告: " + msg);
                    WriteLog("登入警告: " + msg);
                    Console.WriteLine("   繼續使用報價功能...");
                    isLoggedIn = true;
                    return true;
                }
                else
                {
                    string msg = m_center.SKCenterLib_GetReturnCodeMessage(result);
                    Console.WriteLine("登入失敗: " + msg);
                    WriteLog("登入失敗: " + msg);
                    lastError = msg;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("登入錯誤: " + ex.Message);
                WriteLog("登入錯誤: " + ex.ToString());
                lastError = ex.Message;
                return false;
            }
        }
        
        // ════════════════════════════════════════════════════════════════
        // 報價訂閱 (海外期貨)
        // ════════════════════════════════════════════════════════════════
        
        static bool SubscribeQuote()
        {
            try
            {
                Console.WriteLine("訂閱海期報價: " + targetSymbol);
                WriteLog("訂閱海期報價: " + targetSymbol);
                
                // 初始化海期報價 (可能是必要步驟)
                int result = m_osQuote.SKOSQuoteLib_Initialize();
                Console.WriteLine("   Initialize: " + result);
                WriteLog("Initialize: " + result);
                
                // 進入監控模式 (海期)
                result = m_osQuote.SKOSQuoteLib_EnterMonitorLONG();
                Console.WriteLine("   EnterMonitorLONG: " + result);
                WriteLog("EnterMonitorLONG: " + result);
                
                if (result != 0)
                {
                    string msg = m_center.SKCenterLib_GetReturnCodeMessage(result);
                    Console.WriteLine("   錯誤: " + msg);
                    WriteLog("EnterMonitor 錯誤: " + msg);
                    return false;
                }
                
                // 等待連線完成
                Console.WriteLine("   等待海期連線...");
                WriteLog("等待海期連線...");
                int waitCount = 0;
                while (!isQuoteConnected && waitCount < 50) // 最多等待 5 秒
                {
                    Thread.Sleep(100);
                    waitCount++;
                }
                
                if (!isQuoteConnected)
                {
                    Console.WriteLine("   海期連線逾時，嘗試繼續...");
                    WriteLog("海期連線逾時，嘗試繼續...");
                }
                else
                {
                    Console.WriteLine("   海期已連線");
                    WriteLog("海期已連線");
                }
                
                // (已移除) 請求海期商品清單 - 優化啟動速度，避免佔用額度
                // productsReceivedCount = 0;
                // Console.WriteLine("   請求海期商品清單...");
                // WriteLog("請求海期商品清單...");
                // result = m_osQuote.SKOSQuoteLib_RequestOverseaProducts();
                // Console.WriteLine("   RequestOverseaProducts: " + result);
                
                // 建構正確的訂閱字串 (交易所,商品代碼)
                string subscribeSymbol = targetSymbol;
                
                // 如果是純代碼 (例如 GC2602)，嘗試從 Map 找交易所
                if (!targetSymbol.Contains(","))
                {
                    if (stockExchangeMap.ContainsKey(targetSymbol))
                    {
                        string exchange = stockExchangeMap[targetSymbol];
                        subscribeSymbol = exchange + "," + targetSymbol;
                        Console.WriteLine("   自動偵測交易所: " + exchange + " -> " + subscribeSymbol);
                        WriteLog("自動偵測交易所: " + subscribeSymbol);
                    }
                    else
                    {
                        // 找不到對應，可能是使用者已經給了完整格式或者還在載入中
                        // 嘗試預設加上 NYM (如果看起來像黃金)
                        if (targetSymbol.StartsWith("GC") || targetSymbol.StartsWith("CL") || targetSymbol.StartsWith("ES") || targetSymbol.StartsWith("NQ"))
                        {
                             // 這裡其實有點冒險，但作為 fallback
                             // 這裡不做預設，讓下面的失敗後再人工處理
                             Console.WriteLine("   警告: 找不到 " + targetSymbol + " 的交易所資訊，嘗試直接訂閱");
                        }
                    }
                }
                
                Console.WriteLine("   最終訂閱字串: " + subscribeSymbol);
                WriteLog("最終訂閱字串: " + subscribeSymbol);
                
                // 執行訂閱
                try
                {
                    short pageNo = 0;
                    // 使用新 DLL 的 non-ref 方法
                    result = m_osQuote.SKOSQuoteLib_RequestStocks(pageNo, subscribeSymbol);
                    Console.WriteLine("   RequestStocks: " + result);
                    WriteLog("RequestStocks: " + result);
                    
                    if (result == 0)
                    {
                        Console.WriteLine("   >>> 訂閱請求成功！等待報價...");
                        targetSymbol = subscribeSymbol; // 更新為實際訂閱的格式
                        return true;
                    }
                    else
                    {
                        string msg = m_center.SKCenterLib_GetReturnCodeMessage(result);
                        Console.WriteLine("   訂閱失敗: " + msg);
                        
                        // Fallback: 如果 3023 且沒有逗號，強制加上 NYM 試試看 (針對黃金)
                        if (result == 3023 && !subscribeSymbol.Contains(","))
                        {
                            Console.WriteLine("   嘗試強制加上 NYM 前綴...");
                            subscribeSymbol = "NYM," + targetSymbol;
                             result = m_osQuote.SKOSQuoteLib_RequestStocks(pageNo, subscribeSymbol);
                             Console.WriteLine("   RequestStocks (Retry with NYM): " + result);
                             if (result == 0)
                             {
                                 Console.WriteLine("   >>> 重試成功！");
                                 targetSymbol = subscribeSymbol;
                                 return true;
                             }
                        }
                        
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("   訂閱發生例外: " + ex.Message);
                    WriteLog("訂閱例外: " + ex.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("報價訂閱流程錯誤: " + ex.Message);
                WriteLog("報價訂閱流程錯誤: " + ex.ToString());
                lastError = ex.Message;
                return false;
            }
        }
        
        // ════════════════════════════════════════════════════════════════
        // 下單功能 integration
        // ════════════════════════════════════════════════════════════════
        
        static bool InitializeOrder(string account, string password)
        {
            try
            {
                Console.WriteLine("初始化下單功能...");
                WriteLog("初始化下單功能...");

                // 新增: 必須先初始化下單元件
                int result = m_order.SKOrderLib_Initialize();
                Console.WriteLine("   元件初始化 (Initialize): " + result);
                WriteLog("元件初始化: " + result);
                
                // 1. 嘗試讀取已安裝的 CA 憑證
                result = m_order.ReadCertByID(account);
                Console.WriteLine("   讀取已安裝憑證 (ReadCertByID): " + result);
                WriteLog("讀取已安裝憑證: " + result);
                
                // 2. 如果安裝的憑證失敗 (任何錯誤)，嘗試讀取 PFX 檔案
                // 原本只針對 1001 (未安裝)，現在放寬條件
                if (result != 0)
                {
                     Console.WriteLine("   => 憑證未讀取成功，嘗試搜尋 PFX 檔案...");
                     
                     // 定義搜尋路徑
                     string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                     List<string> searchPaths = new List<string>
                     {
                         DataPath, // Documents\CapitalQuote
                         Path.Combine(docPath, "EVOCODE VIBE"), // 專案路徑
                         AppDomain.CurrentDomain.BaseDirectory // 執行檔目錄
                     };
                     
                     string foundPfxPath = null;
                     
                     foreach (string path in searchPaths)
                     {
                         if (Directory.Exists(path))
                         {
                             Console.WriteLine($"      搜尋: {path}");
                             string[] pfxFiles = Directory.GetFiles(path, "*.pfx");
                             if (pfxFiles.Length > 0)
                             {
                                 foundPfxPath = pfxFiles[0]; // 取第一個
                                 break;
                             }
                         }
                     }

                     if (!string.IsNullOrEmpty(foundPfxPath))
                     {
                         Console.WriteLine("   發現 PFX: " + foundPfxPath);
                         WriteLog("發現 PFX: " + foundPfxPath);
                         try 
                         {
                             // 使用 dynamic 強制呼叫 ReadCertByPfxFilePath
                             result = ((dynamic)m_order).ReadCertByPfxFilePath(foundPfxPath, password);
                             Console.WriteLine("   讀取 PFX (ReadCertByPfxFilePath): " + result);
                             WriteLog("讀取 PFX 檔案結果: " + result);
                         }
                         catch (Exception ex)
                         {
                             Console.WriteLine("   不支援直接讀取 PFX (可能 API 版本過舊): " + ex.Message);
                             WriteLog("不支援直接讀取 PFX: " + ex.Message);
                         }
                     }
                     else
                     {
                         Console.WriteLine("   未找到任何 PFX 檔案");
                         WriteLog("未找到任何 PFX 檔案");
                     }
                }
                
                if (result == 0)
                {
                    Console.WriteLine("   憑證讀取成功！已啟用下單功能");
                    isOrderInitialized = true;
                    
                    // 獲取海期帳號 (必要)
                    m_order.GetUserAccount();
                    return true;
                }
                else
                {
                    string msg = m_center.SKCenterLib_GetReturnCodeMessage(result);
                    Console.WriteLine("   憑證讀取失敗: " + msg);
                    Console.WriteLine("   (若無憑證，將無法執行下單指令)");
                    WriteLog("憑證失敗: " + msg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteLog("InitializeOrder 錯誤: " + ex.ToString());
                return false;
            }
        }
        
        // 簡易 JSON 解析 (字串處理)
        static string GetJsonValue(string json, string key)
        {
            try {
                int start = json.IndexOf("\"" + key + "\"");
                if (start == -1) return null;
                int colon = json.IndexOf(":", start);
                if (colon == -1) return null;
                
                // 判斷是字串還是數字
                int quoteStart = json.IndexOf("\"", colon);
                if (quoteStart != -1 && quoteStart < json.IndexOf(",", colon) && quoteStart < json.IndexOf("}", colon))
                {
                    // 字串
                    int quoteEnd = json.IndexOf("\"", quoteStart + 1);
                    if (quoteEnd == -1) return null;
                    return json.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
                }
                else
                {
                    // 數字或布林
                    int comma = json.IndexOf(",", colon);
                    int brace = json.IndexOf("}", colon);
                    int end = (comma == -1) ? brace : (brace == -1 ? comma : Math.Min(comma, brace));
                    if (end == -1) return null;
                    return json.Substring(colon + 1, end - colon - 1).Trim();
                }
            } catch { return null; }
        }
        
        static DateTime lastProcessedOrderTime = DateTime.MinValue;
        static void CheckAndSendOrder(string orderPath, string account)
        {
            try
            {
                if (!File.Exists(orderPath)) return;
                
                string json = File.ReadAllText(orderPath);
                
                // Find the LAST order in the file (since we append)
                int lastTsIdx = json.LastIndexOf("\"timestamp\"");
                if (lastTsIdx == -1) 
                {
                    try { File.Delete(orderPath); } catch {}
                    return;
                }
                
                // Find the start of the object containing this timestamp
                int objectStart = json.LastIndexOf("{", lastTsIdx);
                if (objectStart == -1) 
                {
                    try { File.Delete(orderPath); } catch {}
                    return;
                }
                
                string lastOrderJson = json.Substring(objectStart);
                
                // Parse Timestamp
                string timeStr = GetJsonValue(lastOrderJson, "timestamp");
                DateTime orderTime;
                if (!DateTime.TryParse(timeStr, out orderTime)) 
                {
                    try { File.Delete(orderPath); } catch {}
                    return;
                }
                
                // Check if too old (> 10 seconds)
                if ((DateTime.Now - orderTime).TotalSeconds > 10)
                {
                    try { File.Delete(orderPath); } catch {}
                    return;
                }
                
                if (orderTime <= lastProcessedOrderTime) 
                {
                    try { File.Delete(orderPath); } catch {}
                    return;
                }

                lastProcessedOrderTime = orderTime;
                
                Console.WriteLine();
                Console.WriteLine("收到新訂單請求！");
                WriteLog("收到訂單: " + lastOrderJson);
                
                string action = GetJsonValue(lastOrderJson, "action");
                string symbol = GetJsonValue(lastOrderJson, "symbol");
                string qtyStr = GetJsonValue(lastOrderJson, "quantity");
                string priceType = GetJsonValue(lastOrderJson, "priceType");
                string priceStr = GetJsonValue(lastOrderJson, "price");
                // [NEW] 讀取觸發價
                string triggerStr = GetJsonValue(lastOrderJson, "triggerPrice");
                string simStr = GetJsonValue(lastOrderJson, "simulation");
                string condition = GetJsonValue(lastOrderJson, "condition");
                
                bool isSimulation = true;
                if (!string.IsNullOrEmpty(simStr))
                {
                    bool.TryParse(simStr, out isSimulation);
                }
                
                int qty = 1;
                int.TryParse(qtyStr, out qty);
                
                double price = 0;
                double.TryParse(priceStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out price);

                double triggerPrice = 0;
                if (!string.IsNullOrEmpty(triggerStr))
                {
                    double.TryParse(triggerStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out triggerPrice);
                }
                        
                // Safety check
                if (qty > 1) 
                {
                    Console.WriteLine("   [安全攔截] 口數超過限制 (Max=1)");
                    WriteLog("安全攔截: Qty > 1");
                    try { File.Delete(orderPath); } catch {}
                    return;
                }
                
                if (string.IsNullOrEmpty(validAccount))
                {
                     m_order.GetUserAccount();
                     Thread.Sleep(500);
                     if (string.IsNullOrEmpty(validAccount))
                     {
                         Console.WriteLine("   [錯誤] 無效帳號，無法下單");
                         try { File.Delete(orderPath); } catch {}
                         return;
                     }
                }

                // Execute order
                // [MODIFY] 傳入 triggerPrice
                SendOverseaFutureOrder(account, validAccount, action, symbol, qty, priceType, price, triggerPrice, condition, isSimulation);
                
                // DELETE file after processing
                try { File.Delete(orderPath); } catch {}
            }
            catch (Exception ex)
            {
                WriteLog("CheckOrder 錯誤: " + ex.Message);
            }
        }

        static string validAccount = ""; 

        static void OnAccount(string sLoginID, string sAccountData)
        {
             WriteLog("收到帳號: " + sAccountData);
             validAccount = sAccountData; 
        }

        // [MODIFY] 新增 triggerPrice 參數
        static void SendOverseaFutureOrder(string userID, string fullAccount, string action, string symbolRaw, int qty, string priceType, double limitPrice, double triggerPrice, string condition, bool isSimulation)
        {
            try
            {
                string condDisplay = string.IsNullOrEmpty(condition) ? "ROD" : condition;
                
                if (isSimulation)
                {
                    string priceInfo = priceType == "LIMIT" ? $" @{limitPrice:F2}" : (priceType == "STOP" ? $" STP@{triggerPrice:F2}" : "");
                    Console.WriteLine($"   [模擬模式] {action} {symbolRaw} {qty}口 {priceInfo} ({condDisplay})");
                    WriteLog($"[模擬執行] 下單成功 (未送出) Condition={condDisplay}");
                    return;
                }

                // 解析商品 (e.g., "GC2604" -> "GC", "2604")
                string stockNo = "";
                string yearMonth = "";
                
                for(int i=0; i<symbolRaw.Length; i++) {
                    if (char.IsDigit(symbolRaw[i])) {
                        stockNo = symbolRaw.Substring(0, i);
                        yearMonth = symbolRaw.Substring(i);
                        break;
                    }
                }
                
                // 修正年份格式: API 需要 YYYYMM (e.g. 202602)
                if (yearMonth.Length == 4)
                {
                    yearMonth = "20" + yearMonth;
                }
                
                // 解析交易所
                string exchange = "NYM"; // 預設
                if (stockExchangeMap.ContainsKey(stockNo)) {
                    exchange = stockExchangeMap[stockNo];
                } else {
                     // 簡單判斷
                     if (stockNo == "MGC" || stockNo == "GC" || stockNo == "1OZ") exchange = "NYM";
                     if (stockNo == "NQ" || stockNo == "MNQ") exchange = "CME";
                }
                
                // 準備下單結構
                SKCOMLib.OVERSEAFUTUREORDER order = new SKCOMLib.OVERSEAFUTUREORDER();
                
                order.bstrFullAccount = fullAccount;
                order.bstrExchangeNo = exchange;
                order.bstrStockNo = stockNo;
                order.bstrYearMonth = yearMonth;
                
                order.sBuySell = (short)(action == "BUY" ? 0 : 1); 
                order.sNewClose = 0; // 新倉
                order.sDayTrade = 0; // 非當沖
                
                // 設定委託條件 (根據 API 範例: 0=ROD, 1=FOK, 2=IOC)
                order.sTradeType = 0; // ROD
                if (condition == "FOK") order.sTradeType = 1;      
                else if (condition == "IOC") order.sTradeType = 2; 
                
                order.nQty = qty;
                
                // [MODIFY] 價格與類型邏輯重構
                // sSpecialTradeType: 0:LMT限價單 1:MKT市價單 2:STL停損限價 3.STP停損市價
                
                // 預設值 (重置)
                order.bstrOrder = "0";
                order.bstrOrderNumerator = "0";
                order.bstrTrigger = "0"; 
                order.bstrTriggerNumerator = "0";

                if (priceType == "MARKET")
                {
                    order.sSpecialTradeType = 1; // MKT
                    // MKT 通常不需要設定價格，但部分交易所可能需要 "0" 或市場價
                    order.bstrOrder = "0";
                }
                else if (priceType == "STOP") // 停損市價 (Stop Market)
                {
                    order.sSpecialTradeType = 3; // STP
                    double roundedTrigger = RoundToTickSize(triggerPrice, stockNo);
                    order.bstrTrigger = roundedTrigger.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                    order.bstrOrder = "0"; // 市價
                }
                else if (priceType == "STOP_LIMIT") // 停損限價 (Stop Limit)
                {
                    order.sSpecialTradeType = 2; // STL
                    double roundedTrigger = RoundToTickSize(triggerPrice, stockNo);
                    order.bstrTrigger = roundedTrigger.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                    
                    double roundedOrder = RoundToTickSize(limitPrice, stockNo);
                    order.bstrOrder = roundedOrder.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                }
                else // LIMIT (預設)
                {
                    order.sSpecialTradeType = 0; // LMT
                    double roundedPrice = RoundToTickSize(limitPrice, stockNo);
                    order.bstrOrder = roundedPrice.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                }
                
                string priceDisplay = priceType;
                if (priceType == "LIMIT") priceDisplay = $"@{order.bstrOrder}";
                else if (priceType == "STOP") priceDisplay = $"STP@{order.bstrTrigger}";
                else if (priceType == "STOP_LIMIT") priceDisplay = $"STL {order.bstrTrigger} -> {order.bstrOrder}";

                Console.WriteLine($"   [執行實單] {action} {exchange},{stockNo}{yearMonth} {qty}口 ({priceDisplay}) condition={condDisplay}");
                WriteLog($"執行下單: {userID}, {fullAccount}, {exchange}, {stockNo}{yearMonth}, {action}, Qty={qty}, Type={priceType}, Price={order.bstrOrder}, Trig={order.bstrTrigger}, Cond={condDisplay}");
                
                string msg = "";
                // [NOTE] 使用 false (同步) 或 true (非同步)?
                // 為了避免卡住 UI/Loop，且我們已經註冊了 OnAsyncOrder，建議改用 true (非同步)
                // 但目前的程式架構是 Loop 檢測，同步比較方便確認「發送成功」，非同步則要看 callback
                // 先維持 false (同步發送，等待 API return code)，但 API 內部網路傳輸仍是將委託送出
                // 如果 API 文件建議 heavy load 下用 async，未來可改 true
                int res = m_order.SendOverseaFutureOrder(userID, false, ref order, out msg);
                
                if (res == 0)
                {
                    Console.WriteLine("   => 下單指令發送成功");
                    WriteLog("下單API回傳成功");
                }
                else
                {
                    string err = m_center.SKCenterLib_GetReturnCodeMessage(res);
                    Console.WriteLine("   => 下單失敗: " + res + " " + err);
                    WriteLog("下單失敗: " + res + " " + err);
                }
            }
            catch (Exception ex)
            {
                WriteLog("下單執行錯誤: " + ex.Message);
            }
        }
        
        static void OnReplyMessage(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            Console.WriteLine("公告: " + bstrMessage);
            WriteLog("公告: " + bstrMessage);
            nConfirmCode = -1; // 確認公告
        }
        
        static void OnOSQuoteConnect(int nCode, int nSocketCode)
        {
            WriteLog("OnOSQuoteConnect: nCode=" + nCode + ", nSocketCode=" + nSocketCode);
            if (nCode == 3001 && nSocketCode == 0)
            {
                Console.WriteLine("海期報價連線成功");
                WriteLog("海期報價連線成功");
                isQuoteConnected = true;
            }
            else
            {
                Console.WriteLine("海期報價連線狀態: " + nCode);
                WriteLog("海期報價連線狀態: " + nCode);
            }
        }

        static void OnAsyncOrder(int nThreadID, int nCode, string bstrMessage)
        {
            // 非同步回報 (例如: 委託被拒絕、委託成功送出但尚未成交)
            // 格式: TID:{ThreadID} Code:{nCode} Msg:{Message}
            string logMsg = $"OnAsyncOrder: TID={nThreadID}, Code={nCode}, Msg={bstrMessage}";
            Console.WriteLine("   [Async回報] " + m_center.SKCenterLib_GetReturnCodeMessage(nCode) + " " + bstrMessage);
            WriteLog(logMsg);
        }
        
        static void OnOSNotifyQuote(int nStockIdx)
        {
            try
            {
                WriteLog("OnOSNotifyQuote: nStockIdx=" + nStockIdx);
                
                SKFOREIGNLONG stock = new SKFOREIGNLONG();
                int result = m_osQuote.SKOSQuoteLib_GetStockByIndexLONG(nStockIdx, ref stock);
                
                WriteLog("GetStockByIndexLONG result: " + result);
                
                if (result == 0)
                {
                    // 海期報價欄位 (捨入至正確 Tick Size)
                    double divisor = Math.Pow(10, stock.sDecimal);
                    
                    quoteSymbol = stock.bstrStockNo;
                    double rawBid = stock.nBid / divisor;
                    double rawAsk = stock.nAsk / divisor;
                    double rawLast = stock.nClose / divisor;
                    
                    // 捨入至正確 Tick Size
                    quoteBid = RoundToTickSize(rawBid, quoteSymbol);
                    quoteAsk = RoundToTickSize(rawAsk, quoteSymbol);
                    quoteLast = RoundToTickSize(rawLast, quoteSymbol);
                    quoteVolume = stock.nTQty;
                    
                    // 寫入檔案
                    WriteQuoteFile();
                    
                    // 顯示報價
                    Console.Write("\r" + quoteSymbol + ": Bid=" + quoteBid.ToString("F2") + 
                                  " Ask=" + quoteAsk.ToString("F2") + 
                                  " Last=" + quoteLast.ToString("F2") + "   ");
                }
            }
            catch (Exception ex)
            {
                WriteLog("報價處理錯誤: " + ex.ToString());
                Console.WriteLine("報價處理錯誤: " + ex.Message);
            }
        }
        
        // 接收到的商品數量
        static int productsReceivedCount = 0;
        static string lastReceivedProduct = "";
        
        static void OnOverseaProducts(string bstrValue)
        {
            productsReceivedCount++;
            lastReceivedProduct = bstrValue;
            
            // 解析 CSV: 交易所,交易所名稱,商品代碼,...
            // 例如: NYM,紐約商業交易所,GC2602,...
            try 
            {
                string[] parts = bstrValue.Split(',');
                if (parts.Length >= 3)
                {
                    string exchange = parts[0].Trim();
                    string symbol = parts[2].Trim();
                    
                    if (!stockExchangeMap.ContainsKey(symbol))
                    {
                        stockExchangeMap[symbol] = exchange;
                    }
                }
            }
            catch {}

            // 只記錄包含目標商品的資訊，避免太多輸出
            if (bstrValue.Contains("GC") || productsReceivedCount <= 3)
            {
                // Console.WriteLine("   商品: " + bstrValue);
                // WriteLog("OnOverseaProducts: " + bstrValue);
            }
            
            // 每 1000 個商品報告一次
            if (productsReceivedCount % 1000 == 0)
            {
                Console.WriteLine("   ... 已收到 " + productsReceivedCount + " 個商品");
            }
        }

        static void OnOverseaProductsDetail(string bstrValue)
        {
            // Detail 格式通常也包含交易所資訊
            OnOverseaProducts(bstrValue);
        }
        
        // ════════════════════════════════════════════════════════════════
        // 檔案輸出 (手動生成 JSON)
        // ════════════════════════════════════════════════════════════════
        
        static void WriteQuoteFile()
        {
            try
            {
                string json = "{\n" +
                    "  \"symbol\": \"" + quoteSymbol + "\",\n" +
                    "  \"bid\": " + quoteBid.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) + ",\n" +
                    "  \"ask\": " + quoteAsk.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) + ",\n" +
                    "  \"last\": " + quoteLast.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) + ",\n" +
                    "  \"volume\": " + quoteVolume + ",\n" +
                    "  \"timestamp\": \"" + DateTime.Now.ToString("o") + "\",\n" +
                    "  \"status\": \"connected\"\n" +
                    "}";
                
                string filePath = Path.Combine(DataPath, "quote.json");
                File.WriteAllText(filePath, json);
                
                // Update timestamp to prevent main loop from writing too frequently
                lastQuoteUpdate = DateTime.Now;
            }
            catch { }
        }
        
        static void UpdateStatus(string status, string error)
        {
            try
            {
                string errorStr = error != null ? "\"" + error + "\"" : "null";
                
                string json = "{\n" +
                    "  \"service\": \"" + status + "\",\n" +
                    "  \"loginStatus\": \"" + (isLoggedIn ? "connected" : "disconnected") + "\",\n" +
                    "  \"quoteStatus\": \"" + (isQuoteConnected ? "subscribed" : "pending") + "\",\n" +
                    "  \"lastUpdate\": \"" + DateTime.Now.ToString("o") + "\",\n" +
                    "  \"error\": " + errorStr + "\n" +
                    "}";
                
                string filePath = Path.Combine(DataPath, "status.json");
                File.WriteAllText(filePath, json);
            }
            catch { }
        }
        
        // ════════════════════════════════════════════════════════════════
        // 輔助方法
        // ════════════════════════════════════════════════════════════════
        
        static void PrintHeader()
        {
            Console.WriteLine("════════════════════════════════════════════");
            Console.WriteLine("  群益報價中介服務 v1.1 (海期版)");
            Console.WriteLine("  Capital Futures Quote Service");
            Console.WriteLine("  支援: GC, CL, SI, NQ, ES 等海外期貨");
            Console.WriteLine("════════════════════════════════════════════");
            Console.WriteLine();
        }
        
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
        
        static void WaitForExit()
        {
            Console.WriteLine();
            Console.WriteLine("按任意鍵結束...");
            Console.ReadKey();
        }
        
        static void Cleanup()
        {
            try
            {
                if (m_osQuote != null)
                {
                    m_osQuote.SKOSQuoteLib_LeaveMonitor();
                }
                UpdateStatus("stopped", null);
            }
            catch { }
        }
    }
}
