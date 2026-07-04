# SKQuoteLib — 國內報價元件（證券／期貨／選擇權即時行情、Ticks 五檔、K 線、技術分析；含期貨新制商品報價與 DLL 元件載體）

> 來源：`api_spec/_raw/策略王COM元件使用說明_V2.13.57.md`（4-4 節）、`api_spec/_raw/13.國內報價.md`、`api_spec/_raw/策略王COM元件使用說明_期貨新制商品報價元件.md`、`api_spec/_raw/C_Sharp策略王DLL元件使用說明.md`；範例碼：`Source_code/CapitalAPI_2.13.57_CExample/`。
> 本元件有兩種載體：**COM 元件**（`Interop.SKCOMLib`，物件 `SKQuoteLib`，本檔主體）與 **DLL 載體**（`SKDLLCSharp.dll`，靜態類 `SK`，方法同名但簽名不同，見「DLL 載體專屬方法」節）。

## 總覽：功能分區表

| 分區 | 函式 / 事件 | 用途一句話 |
|---|---|---|
| 連線 | SKQuoteLib_EnterMonitorLONG | 與報價伺服器建立連線（含盤中零股） |
| 連線 | SKQuoteLib_LeaveMonitor | 中斷所有 Solace 伺服器連線 |
| 連線 | SKQuoteLib_IsConnected | 查目前報價連線狀態（0 斷線/1 連線/2 下載中） |
| 連線 | SKQuoteLib_GetQuoteStatus | 查連線數與是否超過連線限制（僅限連線後） |
| 連線 | SKQuoteLib_RequestServerTime | 要求主機回傳目前時間（兼作 keep-alive） |
| 連線 | OnConnection（事件） | 回傳連線/斷線/商品檔下載完成狀態 |
| 連線 | OnNotifyServerTime（事件） | 回傳主機時間 |
| 商品清單 | SKQuoteLib_RequestStockList | 依市場別取得商品基本資料清單 |
| 商品清單 | OnNotifyStockList（事件） | 回傳指定市場各類股商品清單 |
| 商品清單 | OnNotifyCommodityListWithTypeNo（事件） | 回傳含類別代碼/名稱之商品清單 |
| 個股資訊 | SKQuoteLib_GetStockByNoLONG | 依商品代號取回商品物件（不適用盤中零股） |
| 個股資訊 | SKQuoteLib_GetStockByMarketAndNo | 依市場別＋商品代號取回商品物件（適用盤中零股） |
| 個股資訊 | SKQuoteLib_GetStockByIndexLONG | 依市場別＋LONG index 取回商品物件 |
| 即時報價 | SKQuoteLib_RequestStocks | 訂閱即時報價（100 檔，不支援盤中零股） |
| 即時報價 | SKQuoteLib_RequestStocksWithMarketNo | 指定市場別訂閱即時報價（盤中零股/客製化期選） |
| 即時報價 | SKQuoteLib_CancelRequestStocks | 取消 RequestStocks 訂閱 |
| 即時報價 | SKQuoteLib_GetMarketPriceTS | 取得證券逐筆「市價」欄位特殊值 |
| 即時報價 | OnNotifyQuoteLONG（事件） | 報價異動通知（sMarketNo＋nIndex） |
| 即時報價 | OnNotifyOddLotSpreadDeal（事件） | 整零價差即時行情 |
| 五檔&成交明細 | SKQuoteLib_RequestTicks | 訂閱成交明細＋五檔（含回補，不支援盤中零股） |
| 五檔&成交明細 | SKQuoteLib_RequestTicksWithMarketNo | 指定市場別訂閱成交明細＋五檔（盤中零股） |
| 五檔&成交明細 | SKQuoteLib_RequestLiveTick | 只訂閱即時成交明細（不含五檔、不回補） |
| 五檔&成交明細 | SKQuoteLib_CancelRequestTicks | 取消 RequestTicks 訂閱 |
| 五檔&成交明細 | SKQuoteLib_GetTickLONG | 取得指定第幾筆成交明細（SKTICK） |
| 五檔&成交明細 | SKQuoteLib_GetBest5LONG | 取得最佳五檔（SKBEST5） |
| 五檔&成交明細 | OnNotifyTicksLONG（事件） | 即時 Tick 通知 |
| 五檔&成交明細 | OnNotifyHistoryTicksLONG（事件） | 當天 Tick 回補通知 |
| 五檔&成交明細 | OnNotifyBest5LONG（事件） | 最佳五檔異動通知 |
| 五檔&成交明細 | OnNotifyBest5Emerging（事件） | 興櫃五檔通知（單位為股） |
| K 線 | SKQuoteLib_RequestKLine | 歷史 K 線查詢（僅 AM 盤輸出） |
| K 線 | SKQuoteLib_RequestKLineAM | 歷史 K 線查詢，可選 AM 盤/全盤 |
| K 線 | SKQuoteLib_RequestKLineAMByDate | 歷史 K 線查詢，可指定日期區間與幾分 K |
| K 線 | OnNotifyKLineData（事件） | 回傳 K 線（技術分析）資料 |
| K 線 | OnKLineComplete（事件） | K 線回補完成通知（"##"） |
| 技術分析 | SKQuoteLib_RequestMACD | 訂閱 MACD（僅證券市場） |
| 技術分析 | SKQuoteLib_GetMACDLONG | 取得 MACD 數值（SKMACD） |
| 技術分析 | SKQuoteLib_RequestBoolTunel | 訂閱布林通道（僅證券市場） |
| 技術分析 | SKQuoteLib_GetBoolTunelLONG | 取得布林通道數值（SKBoolTunel） |
| 技術分析 | OnNotifyMACDLONG（事件） | 回傳 MACD |
| 技術分析 | OnNotifyBoolTunelLONG（事件） | 回傳布林通道 |
| 大盤資訊 | SKQuoteLib_GetMarketBuySellUpDown | 要求上市/上櫃大盤成交、買賣、漲跌家數 |
| 大盤資訊 | OnNotifyMarketTot（事件） | 大盤成交張筆 |
| 大盤資訊 | OnNotifyMarketBuySell（事件） | 大盤買賣張筆數 |
| 大盤資訊 | OnNotifyMarketHighLow（事件） | 大盤漲跌家數 |
| 大盤資訊 | OnNotifyMarketHighLowNoWarrant（事件） | 大盤漲跌家數（含/不含權證） |
| 期選交易資訊 | SKQuoteLib_RequestFutureTradeInfo | 訂閱期貨商品委託/成交統計 |
| 期選交易資訊 | SKQuoteLib_GetStrikePrices | 取得選擇權商品（履約價）資訊 |
| 期選交易資訊 | OnNotifyFutureTradeInfoLONG（事件） | 回傳期貨商品交易資訊 |
| 期選交易資訊 | OnNotifyStrikePrices（事件） | 回傳選擇權履約價清單 |
| 選擇權風險參數 | SKQuoteLib_Delta / SKQuoteLib_Gamma / SKQuoteLib_Theta / SKQuoteLib_Vega / SKQuoteLib_Rho | Black-Scholes 風險參數計算（純本地計算） |
| DLL 載體專屬 | SKQuoteLib_GetStockByStockNo | （DLL）依市場別＋代號直接回傳 SKSTOCKLONG2 物件 |
| DLL 載體專屬 | SKQuoteLib_RequestStocksOddLot | （DLL）訂閱盤中零股報價 |
| DLL 載體專屬 | SKQuoteLib_RequestTicksOddLot | （DLL）訂閱盤中零股五檔＋Ticks |
| 舊版（已移除） | SKQuoteLib_EnterMonitor / SKQuoteLib_GetStockByIndex / SKQuoteLib_GetStockByNo / SKQuoteLib_GetTick / SKQuoteLib_GetBest5 / SKQuoteLib_GetMACD / SKQuoteLib_GetBoolTunel | SHORT index 舊版，V2.13.46 起移除，改用 LONG 版 |
| 舊版事件（不再觸發） | OnNotifyQuote / OnNotifyTicks / OnNotifyHistoryTicks / OnNotifyBest5 / OnNotifyMACD / OnNotifyBoolTunel / OnNotifyFutureTradeInfo | 使用 EnterMonitorLONG 後不會被觸發，改接 LONG 版事件 |

## 初始化與事件註冊

COM 載體實際寫法（抄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:1-21,251-252,264,309`）：

```csharp
using SKCOMLib;   // 引用 Interop.SKCOMLib.dll

// 宣告物件
SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
SKQuoteLib  m_pSKQuote  = new SKQuoteLib();  // 國內報價物件

// 事件掛載（掛載後才呼叫 SKQuoteLib_EnterMonitorLONG）
m_pSKQuote.OnConnection      += new _ISKQuoteLibEvents_OnConnectionEventHandler(OnConnection);
m_pSKQuote.OnNotifyBest5LONG += new _ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(OnNotifyBest5LONG);
m_pSKQuote.OnNotifyTicksLONG += new _ISKQuoteLibEvents_OnNotifyTicksLONGEventHandler(OnNotifyTicksLONG);

void OnConnection(int nKind, int nCode)
{
    string msg = "【OnConnection】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nKind);
    richTextBoxMessage.AppendText(msg + "\n");
}
```

完整事件掛載清單（抄自 `Source_code/CapitalAPI_2.13.57_CExample/SKCOMTester/SKQuote.cs:124-143`；handler 命名慣例 `m_SKQuoteLib_On...`，對應右側事件）：

```csharp
m_SKQuoteLib.OnConnection                    += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib_OnConnection);
m_SKQuoteLib.OnNotifyQuoteLONG               += new _ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(m_SKQuoteLib_OnNotifyQuote);
m_SKQuoteLib.OnNotifyHistoryTicksLONG        += new _ISKQuoteLibEvents_OnNotifyHistoryTicksLONGEventHandler(m_SKQuoteLib_OnNotifyHistoryTicks);
m_SKQuoteLib.OnNotifyTicksLONG               += new _ISKQuoteLibEvents_OnNotifyTicksLONGEventHandler(m_SKQuoteLib_OnNotifyTicks);
m_SKQuoteLib.OnNotifyBest5LONG               += new _ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(m_SKQuoteLib_OnNotifyBest5);
m_SKQuoteLib.OnNotifyKLineData               += new _ISKQuoteLibEvents_OnNotifyKLineDataEventHandler(m_SKQuoteLib_OnNotifyKLineData);
m_SKQuoteLib.OnNotifyServerTime              += new _ISKQuoteLibEvents_OnNotifyServerTimeEventHandler(m_SKQuoteLib_OnNotifyServerTime);
m_SKQuoteLib.OnNotifyMarketTot               += new _ISKQuoteLibEvents_OnNotifyMarketTotEventHandler(m_SKQuoteLib_OnNotifyMarketTot);
m_SKQuoteLib.OnNotifyMarketBuySell           += new _ISKQuoteLibEvents_OnNotifyMarketBuySellEventHandler(m_SKQuoteLib_OnNotifyMarketBuySell);
//m_SKQuoteLib.OnNotifyMarketHighLow         += new _ISKQuoteLibEvents_OnNotifyMarketHighLowEventHandler(m_SKQuoteLib_OnNotifyMarketHighLow);
m_SKQuoteLib.OnNotifyMACDLONG                += new _ISKQuoteLibEvents_OnNotifyMACDLONGEventHandler(m_SKQuoteLib_OnNotifyMACD);
m_SKQuoteLib.OnNotifyBoolTunelLONG           += new _ISKQuoteLibEvents_OnNotifyBoolTunelLONGEventHandler(m_SKQuoteLib_OnNotifyBoolTunel);
m_SKQuoteLib.OnNotifyFutureTradeInfoLONG     += new _ISKQuoteLibEvents_OnNotifyFutureTradeInfoLONGEventHandler(m_SKQuoteLib_OnNotifyFutureTradeInfo);
m_SKQuoteLib.OnNotifyStrikePrices            += new _ISKQuoteLibEvents_OnNotifyStrikePricesEventHandler(m_SKQuoteLib_OnNotifyStrikePrices);
//m_SKQuoteLib.OnNotifyStockList             += new _ISKQuoteLibEvents_OnNotifyStockListEventHandler(m_SKQuoteLib_OnNotifyStockList);
m_SKQuoteLib.OnNotifyMarketHighLowNoWarrant  += new _ISKQuoteLibEvents_OnNotifyMarketHighLowNoWarrantEventHandler(m_SKQuoteLib_OnNotifyMarketHighLowNoWarrant);
m_SKQuoteLib.OnNotifyCommodityListWithTypeNo += new _ISKQuoteLibEvents_OnNotifyCommodityListWithTypeNoEventHandler(m_SKQuoteLib_OnNotifyCommodityListWithTypeNo);
m_SKQuoteLib.OnNotifyOddLotSpreadDeal        += new _ISKQuoteLibEvents_OnNotifyOddLotSpreadDealEventHandler(m_SKQuoteLib_OnNotifyOddLotSpreadDeal);

// 連線
m_nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitorLONG();
```

DLL 載體寫法（抄自 `api_spec/_raw/C_Sharp策略王DLL元件使用說明.md`；VS 引用 `SKDLLCSharp.dll`，`using SKDLLCSharp;`，全部透過靜態類 `SK.`）：

```csharp
using SKDLLCSharp;

var result = SK.Login(strLoginID, strPassword, nFlag);        // 先登入
SK.OnConnection += (loginID, code) => { /* 國內行情 loginID == "SKQuote" */ };
int rc = SK.ManageServerConnection("", 0, 1);                 // nTargetType=1 國內行情連線
SK.LoadCommodity(11);                                         // 下載商品檔（11=全市場）
SK.OnNotifyQuoteLONG += (nMarketNo, strStockNo) =>
{
    SK.SKSTOCKLONG2 pSKStockLONG = SK.SKQuoteLib_GetStockByStockNo(nMarketNo, strStockNo);
    if (pSKStockLONG.nCode == 0) OnUpDateDataRow(pSKStockLONG);
};
```

DLL 載體不使用 `SKQuoteLib_EnterMonitorLONG`／`SKQuoteLib_LeaveMonitor`，改由 `ManageServerConnection` + `LoadCommodity` 管理連線與商品檔。

## 方法（COM 元件，V2.13.57 現行，共 34 個）

### SKQuoteLib_EnterMonitorLONG
- 用途：(LONG index) 與報價伺服器建立連線（含盤中零股市場商品）。
- 簽名：`int SKQuoteLib_EnterMonitorLONG();`
- 參數：無。
- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：連線狀態由 OnConnection 回傳；需先簽署證券或期貨 API 下單聲明書。與舊版 SKQuoteLib_EnterMonitor 僅能擇一使用；使用本函式後，系統只會回傳 LONG index 類事件（OnNotifyQuoteLONG 等），舊版事件（OnNotifyQuote/OnNotifyHistoryTicks/OnNotifyTicks/OnNotifyBest5/OnNotifyBoolTunel/OnNotifyMACD/OnNotifyFutureTradeInfo）不會被觸發。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:689`、`SKCOMTester/SKQuote.cs:147`

### SKQuoteLib_LeaveMonitor
- 用途：中斷所有 Solace 伺服器連線。
- 簽名：`int SKQuoteLib_LeaveMonitor();`
- 參數：無。
- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：會中斷所有 Solace 連線，包含報價與回報，但不中斷模擬平台回報連線與公告。斷線後重新連線即還原設定；限制：單一 SKQuoteLib 物件僅一個訂閱 Tick 報價（10 檔）及一個 RequestStocks 即時報價（100 檔）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:697`、`SKCOMTester/SKQuote.cs:154`

### SKQuoteLib_RequestStocks
- 用途：訂閱指定商品即時報價（不支援盤中零股）。
- 簽名：`int SKQuoteLib_RequestStocks(short psPageNo, string bstrStockNos);`（IDL：`Long SKQuoteLib_RequestStocks([in,out] SHORT* psPageNo, [in] BSTR bstrStockNos);`，V1 範例 interop 以 `ref short` 傳遞）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（in,out） | 請固定帶 1（V2.13.54 起；一般用戶 PageNo 上限為 1） |
| bstrStockNos | BSTR | 欲訂閱商品代號，多筆以「,」分隔，最多 100 檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：須先 SKQuoteLib_EnterMonitorLONG，等 OnConnection 收到 SK_SUBJECT_CONNECTION_STOCKS_READY 後方可訂閱。一個 SKQuoteLib 物件僅可在 RequestStocks 與 RequestStocksWithMarketNo 中擇一使用。超過 100 檔僅取 100 檔處理不回錯；代號不存在直接略過不回錯。期選 T+1 盤：代號加 AM 可取純 AM 盤行情（如 TX00AM），但 AM 代號不可下單。對應事件 OnNotifyQuoteLONG。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:748`、`SKCOMTester/SKQuote.cs:269`、`SKDLLTester/SKDLLTester/Form1.cs:3322`（DLL 版）

### SKQuoteLib_RequestTicks
- 用途：訂閱成交明細以及五檔，含當天 Tick 回補（不支援盤中零股）。
- 簽名：`int SKQuoteLib_RequestTicks(short psPageNo, string bstrStockNo);`（IDL：`Long SKQuoteLib_RequestTicks([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（in,out） | 請從 0 開始 |
| bstrStockNo | BSTR | 商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：須先 EnterMonitorLONG 並等 SK_SUBJECT_CONNECTION_STOCKS_READY。即時 Tick→OnNotifyTicksLONG；Tick 回補→OnNotifyHistoryTicksLONG；五檔→OnNotifyBest5LONG。期選 AM 盤同 RequestStocks。盤中零股請改用 SKQuoteLib_RequestTicksWithMarketNo。V2.13.57 修正可無限訂閱問題（訂閱數限制 10 檔）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:765`、`SKCOMTester/SKQuote.cs:182`

### SKQuoteLib_RequestKLine
- 用途：（僅歷史資料）取得單一商品技術分析（K 線）；僅輸出 AM 盤。
- 簽名：`int SKQuoteLib_RequestKLine(string bstrStockNo, short sKLineType, string sOutType);`（IDL：`Long SKQuoteLib_RequestKLine([in] BSTR bstrStockNo, [in] SHORT sKLineType, [in] BSTR sOutType);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 商品代號，例如 6005 |
| sKLineType | SHORT | 0=1分鐘線、4=完整日線、5=週線、6=月線 |
| sOutType | BSTR | 0=舊版輸出格式、1=新版輸出格式 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：不支援盤中零股。證券以整股計算（不含鉅額交易、盤中零股、盤後零股）。未開立證券帳戶無法取得證券商品/指數 K 線；未開立期貨帳戶無法取得期選 K 線。solace 僅提供 1 分鐘 K（5/30 分自行組合），不提供 288 日 K。若需全盤請改用 SKQuoteLib_RequestKLineAM。對應事件 OnNotifyKLineData、OnKLineComplete。
- 範例：`SKCOMTester/SKQuote.cs:336`

### SKQuoteLib_RequestServerTime
- 用途：要求報價主機傳送目前時間；建議固定每 15 秒呼叫作 keep-alive，避免收盤後連線被防火牆切斷。
- 簽名：`int SKQuoteLib_RequestServerTime();`
- 參數：無。
- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：solace 固定每 5 秒自動更新時間。對應事件 OnNotifyServerTime。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:1056`、`SKCOMTester/SKQuote.cs:285`

### SKQuoteLib_GetMarketBuySellUpDown
- 用途：要求傳送上市與上櫃大盤資訊（成交數、買賣數、漲跌家數）。
- 簽名：`int SKQuoteLib_GetMarketBuySellUpDown();`
- 參數：無。
- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：不支援盤中零股。對應事件：OnNotifyMarketTot（張筆）、OnNotifyMarketBuySell（買賣）、OnNotifyMarketHighLow（漲跌家數）、OnNotifyMarketHighLowNoWarrant（含/不含權證家數）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:1064`、`SKCOMTester/SKQuote.cs:1306`

### SKQuoteLib_RequestMACD
- 用途：訂閱商品技術指標 MACD（平滑異同平均線）。
- 簽名：`int SKQuoteLib_RequestMACD(short psPageNo, string bstrStockNo);`（IDL：`Long SKQuoteLib_RequestMACD([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（in,out） | Page 編號；帶入 50 會取消該檔 MACD 訂閱 |
| bstrStockNo | BSTR | 商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：目前只提供證券市場，不含盤中零股。對應事件 OnNotifyMACDLONG。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:1076`、`SKCOMTester/SKQuote.cs:1318`（取消：`SKQuote.cs:1330`，Page=50）

### SKQuoteLib_RequestBoolTunel
- 用途：訂閱商品布林通道（Bollinger Band）。
- 簽名：`int SKQuoteLib_RequestBoolTunel(short psPageNo, string bstrStockNo);`（IDL：`Long SKQuoteLib_RequestBoolTunel([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（in,out） | Page 編號；帶入 50 會取消該檔 BoolTunel 訂閱 |
| bstrStockNo | BSTR | 商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：目前只提供證券市場，不含盤中零股。對應事件 OnNotifyBoolTunelLONG。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:1089`、`SKCOMTester/SKQuote.cs:1312`（取消：`SKQuote.cs:1324`，Page=50）

### SKQuoteLib_RequestFutureTradeInfo
- 用途：註冊接收期貨商品的交易資訊（總委託/成交買賣筆數口數）。
- 簽名：`int SKQuoteLib_RequestFutureTradeInfo(short psPageNo, string bstrStockNo);`（IDL：`Long SKQuoteLib_RequestFutureTradeInfo([in] SHORT* psPageNo, [in] BSTR bstrStockNo);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT* | Page 編號（範例中帶 50 用於取消訂閱） |
| bstrStockNo | BSTR | 商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。未簽署期貨 API 下單同意書無法查詢（錯誤代碼 3031）。
- 備註：對應事件 OnNotifyFutureTradeInfoLONG。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:979`、`SKCOMTester/SKQuote.cs:1344`（取消：`SKQuote.cs:1351`，Page=50）

### SKQuoteLib_Delta
- 用途：輸入買賣權別、指數、履約價、無風險利率、剩餘天數、sigma，計算選擇權 Delta 值（本地計算，不需連線）。
- 簽名：`int SKQuoteLib_Delta(short nCallPut, double S, double K, double R, double T, double sigma, out double dDelta);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| nCallPut | SHORT | 商品買賣權別（Call: 0, Put: 1） |
| S | DOUBLE | 指數 |
| K | DOUBLE | StrikePrice（履約價） |
| R | DOUBLE | RisklessRate（無風險利率） |
| T | DOUBLE | RemainMaturity（剩餘天數） |
| sigma | DOUBLE | 波動率 sigma |
| dDelta | DOUBLE*（out） | 計算結果 Delta 值 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:804`

### SKQuoteLib_Gamma
- 用途：輸入指數、履約價、無風險利率、剩餘天數、sigma，計算選擇權 Gamma 值。
- 簽名：`int SKQuoteLib_Gamma(double S, double K, double R, double T, double sigma, out double dGamma);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| S | DOUBLE | 指數 |
| K | DOUBLE | StrikePrice（履約價） |
| R | DOUBLE | RisklessRate（無風險利率） |
| T | DOUBLE | RemainMaturity（剩餘天數） |
| sigma | DOUBLE | 波動率 sigma |
| dGamma | DOUBLE*（out） | 計算結果 Gamma 值 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：Gamma/Vega 不分買賣權別（無 nCallPut 參數）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:782`

### SKQuoteLib_Theta
- 用途：輸入買賣權別、台指期成交價、履約價、無風險利率、剩餘天數、波動率，計算選擇權 Theta 值。
- 簽名：`int SKQuoteLib_Theta(short nCallPut, double S, double K, double R, double T, double v, out double dTheta);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| nCallPut | SHORT | 商品買賣權別（Call: 0, Put: 1） |
| S | DOUBLE | 台指期成交價 |
| K | DOUBLE | StrikePrice（履約價） |
| R | DOUBLE | RisklessRate（無風險利率） |
| T | DOUBLE | RemainMaturity（剩餘天數） |
| v | DOUBLE | 波動率 = sigma |
| dTheta | DOUBLE*（out） | 計算結果 Theta 值 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:812`

### SKQuoteLib_Vega
- 用途：輸入台指期成交價、履約價、無風險利率、剩餘天數、波動率，計算選擇權 Vega 值。
- 簽名：`int SKQuoteLib_Vega(double S, double K, double R, double T, double sigma, out double dVega);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| S | DOUBLE | 台指期成交價 |
| K | DOUBLE | StrikePrice（履約價） |
| R | DOUBLE | RisklessRate（無風險利率） |
| T | DOUBLE | RemainMaturity（剩餘天數） |
| sigma | DOUBLE | 波動率 = sigma |
| dVega | DOUBLE*（out） | 計算結果 Vega 值 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:790`

### SKQuoteLib_Rho
- 用途：輸入買賣權別、台指期成交價、履約價、無風險利率、剩餘天數、波動率，計算選擇權 Rho 值。
- 簽名：`int SKQuoteLib_Rho(short nCallPut, double S, double K, double R, double T, double v, out double dRho);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| nCallPut | SHORT | 商品買賣權別（Call: 0, Put: 1） |
| S | DOUBLE | 台指期成交價（文件參數表寫「指數」） |
| K | DOUBLE | StrikePrice（履約價） |
| R | DOUBLE | RisklessRate（無風險利率） |
| T | DOUBLE | RemainMaturity（剩餘天數） |
| v | DOUBLE | 波動率 = sigma |
| dRho | DOUBLE*（out） | 計算結果 Rho 值 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:820`

### SKQuoteLib_GetStrikePrices
- 用途：取得選擇權交易商品（履約價）資訊。
- 簽名：`int SKQuoteLib_GetStrikePrices();`
- 參數：無。
- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。未簽署期貨 API 下單同意書無法查詢（錯誤代碼 3031）。
- 備註：資料由 OnNotifyStrikePrices 事件回傳，結束以「##」開頭一筆表示查詢完畢。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:989`、`SKCOMTester/SKQuote.cs:1366`

### SKQuoteLib_RequestKLineAM
- 用途：（僅歷史資料）取得單一商品 K 線，可選 AM 盤或全盤。
- 簽名：`int SKQuoteLib_RequestKLineAM(string bstrStockNo, short sKLineType, string sOutType, short sTradeSession);`（IDL：`Long SKQuoteLib_RequestKLineAM([in] BSTR bstrStockNo, [in] SHORT sKLineType, [in] BSTR sOutType, [in] SHORT sTradeSession);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 商品代號，例如 6005 |
| sKLineType | SHORT | 0=1分鐘線、4=完整日線、5=週線、6=月線 |
| sOutType | BSTR | 0=舊版輸出格式、1=新版輸出格式 |
| sTradeSession | SHORT | 僅國內期權有效：0=全盤、1=AM 盤 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：全盤例：2023/9/5 全盤 K 含 2023/9/4 17:25～2023/9/5 05:00 及 2023/9/5 8:45～16:15；AM 盤=當日 T 盤。僅 1 分鐘 K，不提供 288 日 K。對應事件 OnNotifyKLineData。
- 範例：`SKCOMTester/SKQuote.cs:1395`

### SKQuoteLib_RequestStockList
- 用途：依市場別編號取得國內各市場商品基本資料清單。
- 簽名：`int SKQuoteLib_RequestStockList(short sMarketNo);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代碼：上市 0、上櫃 1、期貨 2、選擇權 3、興櫃 4、盤中零股-上市 5、盤中零股-上櫃 6、客製化期貨 9、客製化選擇權 10 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。未開戶或未簽同意書查對應市場回錯誤代碼 3031。
- 備註：須先 EnterMonitorLONG 並等 SK_SUBJECT_CONNECTION_STOCKS_READY。對應事件 OnNotifyStockList、OnNotifyCommodityListWithTypeNo。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:877`、`SKCOMTester/SKQuote.cs:1445`

### SKQuoteLib_RequestLiveTick
- 用途：訂閱並要求傳送「僅即時」成交明細（不含五檔、不回補歷史 Ticks）。
- 簽名：`int SKQuoteLib_RequestLiveTick(short psPageNo, string bstrStockNo);`（IDL：`Long SKQuoteLib_RequestLiveTick([in,out] SHORT* psPageNo, [in] BSTR bstrStockNo);`，V1 範例以 `ref short` 傳遞）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（in,out） | 請從 0 開始 |
| bstrStockNo | BSTR | 商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：與 SKQuoteLib_RequestTicks 擇一使用；因不回補歷史成交明細，SKQuoteLib_GetTickLONG() 僅能查閱訂閱後的即時成交明細。即時 Tick 由 OnNotifyTicksLONG 通知。V2.13.57 修正訂閱數問題。
- 範例：`SKCOMTester/SKQuote.cs:1457`

### SKQuoteLib_IsConnected
- 用途：檢查目前報價連線狀態。
- 簽名：`int SKQuoteLib_IsConnected();`
- 參數：無。
- 回傳：0=斷線、1=連線中、2=下載中（商品檔下載中）；其餘為錯誤代碼（見 ../error_codes.md）。
- 備註：請同時接收 OnConnection 事件。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:706`、`SKCOMTester/SKQuote.cs:1480`

### SKQuoteLib_GetMarketPriceTS
- 用途：取得證券市場逐筆交易價格欄位為「市價」時的特殊值。
- 簽名：`int SKQuoteLib_GetMarketPriceTS();`
- 參數：無。
- 回傳：市價特殊值 `-2147483647`（int.MinValue+1）。
- 備註：證券即時報價中買價、賣價等於此值即代表「市價」。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:383`（SKQuote.cs:476 的呼叫已被註解，改用常數 kMarketPrice）

### SKQuoteLib_CancelRequestStocks
- 用途：取消 SKQuoteLib_RequestStocks 的報價訂閱並停止更新商品報價。
- 簽名：`int SKQuoteLib_CancelRequestStocks(string bstrStockNos);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNos | BSTR | 欲解除訂閱的商品代號，多筆以「,」分隔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：不支援盤中零股。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:917`、`SKCOMTester/SKQuote.cs:1507`、`SKDLLTester/SKDLLTester/Form1.cs:3330`（DLL 版）

### SKQuoteLib_CancelRequestTicks
- 用途：取消 RequestTicks 的成交明細及五檔訂閱。
- 簽名：`int SKQuoteLib_CancelRequestTicks(string bstrStockNo);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 欲解除訂閱的商品代號，一次僅能解訂閱一檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：不支援盤中零股。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:886`、`SKCOMTester/SKQuote.cs:278`、`SKDLLTester/SKDLLTester/Form1.cs:3359`（DLL 版）

### SKQuoteLib_GetQuoteStatus
- 用途：查詢報價連線狀態（連線數、是否超過報價連線限制）。
- 簽名：`int SKQuoteLib_GetQuoteStatus(ref int pnConnectionCount, ref bool pbIsOutLimit);`（IDL：`Long SKQuoteLib_GetQuoteStatus([in,out] LONG* pnConnectionCount, [in,out] VARIANT_BOOL* pbIsOutLimit);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| pnConnectionCount | LONG*（in,out） | 連線數：超限時=最大可使用連線數；未超限時=先前已使用連線數（不含當次新連線） |
| pbIsOutLimit | VARIANT_BOOL*（in,out） | 是否超過報價連線限制（帶入 false，函式庫回填） |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：僅限當次報價連線成功後查詢。例：最大連線數 2 且目前超限 → 回傳 2, True。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:732`、`SKCOMTester/SKQuote.cs:1516`

### SKQuoteLib_RequestKLineAMByDate
- 用途：（僅歷史資料）取得單一商品 K 線，可選 AM 盤/全盤、指定日期區間，分 K 可指定幾分 K。
- 簽名：`int SKQuoteLib_RequestKLineAMByDate(string bstrStockNo, short sKLineType, short sOutType, short sTradeSession, string bstrStartDate, string bstrEndDate, short sMinuteNumber);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 商品代號，例如 6005 |
| sKLineType | SHORT | 0=分線、4=日線、5=週線、6=月線 |
| sOutType | SHORT | 0=舊版輸出格式、1=新版輸出格式 |
| sTradeSession | SHORT | 僅國內期權有效：0=全盤、1=AM 盤 |
| bstrStartDate | BSTR | 起始日期 YYYYMMDD（ex: 20201001） |
| bstrEndDate | BSTR | 結束日期 YYYYMMDD（ex: 20201010） |
| sMinuteNumber | SHORT | 指定幾分 K（1=1分K、3=3分K）；僅 sKLineType=0 時有意義 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：不支援盤中零股；證券以整股計算。注意本函式 sOutType 為 SHORT（RequestKLine/RequestKLineAM 的 sOutType 在文件中為 BSTR）。對應事件 OnNotifyKLineData。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:854`、`SKCOMTester/SKQuote.cs:1429`

### SKQuoteLib_GetStockByIndexLONG
- 用途：(LONG index) 依市場別編號與系統索引代碼取回商品報價與基本資訊；須先訂閱即時報價方可取得報價，未訂閱僅可取得基本資料。
- 簽名：`int SKQuoteLib_GetStockByIndexLONG(short sMarketNo, int nIndex, ref SKSTOCKLONG pSKStock);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代碼 |
| nIndex | LONG | 系統所編的索引代碼 |
| pSKStock | SKSTOCKLONG*（in,out） | SKCOM 元件的 SKSTOCKLONG 物件 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：市場編號型態與 SKSTOCK 物件內 bstrMarketNo（BSTR）不同。須等 SK_SUBJECT_CONNECTION_STOCKS_READY；須以 SKQuoteLib_EnterMonitorLONG 登入。未訂閱即時報價（例如只訂 RequestTicks）時僅能取得非即時欄位（商品名稱、昨收價）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:365`、`SKCOMTester/SKQuote.cs:460`

### SKQuoteLib_GetStockByNoLONG
- 用途：(LONG index) 依商品代號取回商品報價相關資訊。
- 簽名：`int SKQuoteLib_GetStockByNoLONG(string bstrStockNo, ref SKSTOCKLONG pSKStock);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 商品代號，例如 6005 |
| pSKStock | SKSTOCKLONG*（in,out） | SKCOM 元件的 SKSTOCKLONG 物件 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：僅適用現股，不適用盤中零股-上市/上櫃（零股改用 GetStockByMarketAndNo）。須等 SK_SUBJECT_CONNECTION_STOCKS_READY、須以 EnterMonitorLONG 登入。未訂閱即時報價時僅回基本資料。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:1001`、`SKCOMTester/SKQuote.cs:257`

### SKQuoteLib_GetTickLONG
- 用途：(LONG index) 取得指定第幾筆成交明細（需先以 RequestTicks 訂閱）。
- 簽名：`int SKQuoteLib_GetTickLONG(short sMarketNo, int nIndex, int nPtr, ref SKTICK pSKTick);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代碼 |
| nIndex | LONG | 系統所編的索引代碼 |
| nPtr | LONG | 第幾筆成交明細（由 0 開始） |
| pSKTick | SKTICK*（in,out） | SKCOM 元件的 SKTICK 物件 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：避免在 SKQuoteLib_OnNotifyHistoryTicksLONG、SKQuoteLib_OnNotifyTicksLONG 通知事件裡呼叫 SKQuoteLib_GetTickLONG()。須以 EnterMonitorLONG 登入。搭配 RequestLiveTick 時僅能查閱訂閱後的即時明細。
- 範例：`SKCOMTester/SKQuote.cs:1289`

### SKQuoteLib_GetBest5LONG
- 用途：(LONG index) 取得最佳五檔價格資訊（需先以 RequestTicks 訂閱）。
- 簽名：`int SKQuoteLib_GetBest5LONG(short sMarketNo, int nIndex, ref SKBEST5 pSKBest5);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代碼 |
| nIndex | LONG | 系統所編的索引代碼 |
| pSKBest5 | SKBEST5*（in,out） | SKCOM 元件的 SKBEST5 物件（docx 參數表誤植為 pSKStock） |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：避免在 SKQuoteLib_OnNotifyBest5LONG 通知事件裡呼叫 SKQuoteLib_GetBest5LONG()。須以 EnterMonitorLONG 登入。
- 範例：`SKCOMTester/SKQuote.cs:303`

### SKQuoteLib_GetMACDLONG
- 用途：(LONG index) 取得商品 MACD 技術指標資訊。
- 簽名：`int SKQuoteLib_GetMACDLONG(short sMarketNo, int nIndex, ref SKMACD pSKMACD);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代碼 |
| nIndex | LONG | 系統所編的索引代碼 |
| pSKMACD | SKMACD*（in,out） | SKCOM 元件的 SKMACD 物件（docx 參數表誤植為 pSKStock） |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：目前只提供證券市場。須以 EnterMonitorLONG 登入。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:532`

### SKQuoteLib_GetBoolTunelLONG
- 用途：(LONG index) 取得商品布林通道資訊。
- 簽名：`int SKQuoteLib_GetBoolTunelLONG(short sMarketNo, int nIndex, ref SKBoolTunel pSKBoolTunel);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代碼 |
| nIndex | LONG | 系統所編的索引代碼 |
| pSKBoolTunel | SKBoolTunel*（in,out） | SKCOM 元件的 SKBoolTunel 物件 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：目前只提供證券市場。須以 EnterMonitorLONG 登入。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:553`

### SKQuoteLib_RequestStocksWithMarketNo
- 用途：訂閱指定市場別及指定商品即時報價（盤中零股、客製化期選專用入口）。
- 簽名：`int SKQuoteLib_RequestStocksWithMarketNo(short psPageNo, short sMarketNo, string bstrStockNos);`（IDL：`Long SKQuoteLib_RequestStocksWithMarketNo([in,out] SHORT* psPageNo, [in] SHORT sMarketNo, [in] BSTR bstrStockNos);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（in,out） | 請固定帶 1 |
| sMarketNo | SHORT | 盤中零股-上市 5、盤中零股-上櫃 6、客製化期貨 9、客製化選擇權 10 |
| bstrStockNos | BSTR | 欲訂閱商品代號，多筆以「,」分隔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：適用盤中零股。須先 EnterMonitorLONG 並等 SK_SUBJECT_CONNECTION_STOCKS_READY。與 RequestStocks 擇一使用（單一 SKQuoteLib 物件僅一個即時報價訂閱，100 檔）。對應事件 OnNotifyQuoteLONG、OnNotifyOddLotSpreadDeal（13.國內報價.md:348）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:962`、`SKCOMTester/SKQuote.cs:1617`

### SKQuoteLib_GetStockByMarketAndNo
- 用途：(LONG index) 依市場別編號與商品代號取回商品報價資訊（適用盤中零股）。
- 簽名：`int SKQuoteLib_GetStockByMarketAndNo(short sMarketNo, string bstrStockNo, ref SKSTOCKLONG pSKStock);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別編號 |
| bstrStockNo | BSTR | 商品代號，例如 6005 |
| pSKStock | SKSTOCKLONG*（in,out） | SKCOM 元件的 SKSTOCKLONG 物件 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：適用盤中零股。須等 SK_SUBJECT_CONNECTION_STOCKS_READY。未以 RequestStocksWithMarketNo 訂閱即時報價時僅回基本資料。
- 範例：`SKCOMTester/SKQuote.cs:1538,1607`

### SKQuoteLib_RequestTicksWithMarketNo
- 用途：指定市場別訂閱成交明細以及五檔（盤中零股、客製化期選）。
- 簽名：`int SKQuoteLib_RequestTicksWithMarketNo(short psPageNo, short sMarketNo, string bstrStockNo);`（IDL：`Long SKQuoteLib_RequestTicksWithMarketNo([in,out] SHORT* psPageNo, [in] SHORT sMarketNo, [in] BSTR bstrStockNo);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| psPageNo | SHORT*（in,out） | 請從 0 開始 |
| sMarketNo | SHORT | 盤中零股-上市 5、盤中零股-上櫃 6、客製化期貨 9、客製化選擇權 10 |
| bstrStockNo | BSTR | 商品代號，一個 Page 僅能索取一檔 |

- 回傳：LONG；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：適用盤中零股。須先 EnterMonitorLONG 並等 SK_SUBJECT_CONNECTION_STOCKS_READY。即時 Tick→OnNotifyTicksLONG；回補→OnNotifyHistoryTicksLONG；五檔→OnNotifyBest5LONG。與 RequestTicks 不建議在同一 SKQuoteLib 物件同時使用。V2.13.57 修正可無限訂閱問題。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:908`、`SKCOMTester/SKQuote.cs:194`

## 方法（DLL 載體 SKDLLCSharp 專屬，共 3 個）

DLL 載體（`SKDLLCSharp.dll`，靜態類 `SK`）與 COM 同名方法簽名不同：`SKQuoteLib_RequestStocks(string strStockNos)`（無 PageNo）、`SKQuoteLib_RequestTicks(int nItemNo, string strStockNo)`、`SKQuoteLib_CancelRequestStocks(string strStockNos)`、`SKQuoteLib_CancelRequestTicks(string strStockNo)`。以下三個方法僅存在於 DLL 載體：

### SKQuoteLib_GetStockByStockNo
- 用途：（DLL）依市場別與商品代號直接「回傳」商品報價物件（取代 COM 的 GetStockByMarketAndNo 之 out 參數寫法）。
- 簽名：`SKSTOCKLONG2 SKQuoteLib_GetStockByStockNo(int nMarketNo, string strStockNo);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| nMarketNo | int | 市場別編號 |
| strStockNo | string | 商品代號 |

- 回傳：`SKSTOCKLONG2` 結構（含 `nCode` 欄位：0 表成功，其餘為錯誤代碼，見 ../error_codes.md）。
- 備註：適用盤中零股。須等 SK_SUBJECT_CONNECTION_STOCKS_READY。未訂閱即時報價時僅回基本資料。常於 OnNotifyQuoteLONG handler 內呼叫。
- 範例：`SKDLLTester/SKDLLTester/Form1.cs:837`

### SKQuoteLib_RequestStocksOddLot
- 用途：（DLL）訂閱盤中零股報價。
- 簽名：`int SKQuoteLib_RequestStocksOddLot(string strStockNos);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| strStockNos | string | 欲訂閱商品代號，多筆以「,」分隔 |

- 回傳：int；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：適用盤中零股。相關事件 OnNotifyQuoteLONG、OnNotifyOddLotSpreadDeal（文件註：整零價差「尚未更新上來」）。與 SKQuoteLib_RequestStocks 擇一使用（同 100 檔限制）。
- 範例：`SKDLLTester/SKDLLTester/Form1.cs:3271`

### SKQuoteLib_RequestTicksOddLot
- 用途：（DLL）訂閱盤中零股五檔＋Ticks。
- 簽名：`int SKQuoteLib_RequestTicksOddLot(int nItemNo, string strStockNo);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| nItemNo | int | Page/項目編號 |
| strStockNo | string | 商品代號，一個項目僅能索取一檔 |

- 回傳：int；0 成功，非 0 失敗（見 ../error_codes.md）。
- 備註：適用盤中零股。即時 Tick→OnNotifyTicksLONG；回補→OnNotifyHistoryTicksLONG；五檔→OnNotifyBest5LONG。與 SKQuoteLib_RequestTicks 擇一訂閱。
- 範例：`SKDLLTester/SKDLLTester/Form1.cs:3407`

## 方法（舊版 SHORT index，V2.13.46 起已移除，共 7 個）

V2.13.31 起因單一市場商品總數可能超過 SHORT 上限（32767），新增 LONG index 系列；V2.13.46（含）以上版本不再提供下列舊版函式，僅為對照保留（新舊對照見主說明「3-4 行情功能修改說明」）：

### SKQuoteLib_EnterMonitor
- 用途：舊版報價連線。已移除，改用 SKQuoteLib_EnterMonitorLONG。簽名待確認（推測 `int SKQuoteLib_EnterMonitor();`）。與 EnterMonitorLONG 僅能擇一使用。
- 範例：僅見於註解程式碼 `SKCOMTester/SKQuote.cs:148,1468`（皆被註解）。

### SKQuoteLib_GetStockByIndex
- 用途：舊版依市場別＋SHORT index 取商品物件（SKSTOCK）。已移除，改用 SKQuoteLib_GetStockByIndexLONG。簽名待確認。範例未見。

### SKQuoteLib_GetStockByNo
- 用途：舊版依商品代號取商品物件（SKSTOCK）。已移除，改用 SKQuoteLib_GetStockByNoLONG。簽名待確認。範例未見。

### SKQuoteLib_GetTick
- 用途：舊版取得成交明細。已移除，改用 SKQuoteLib_GetTickLONG。簽名待確認。範例未見。

### SKQuoteLib_GetBest5
- 用途：舊版取得最佳五檔。已移除，改用 SKQuoteLib_GetBest5LONG。簽名待確認。範例未見。

### SKQuoteLib_GetMACD
- 用途：舊版取得 MACD。已移除，改用 SKQuoteLib_GetMACDLONG。簽名待確認。範例未見。

### SKQuoteLib_GetBoolTunel
- 用途：舊版取得布林通道。已移除，改用 SKQuoteLib_GetBoolTunelLONG。簽名待確認。範例未見。

## 事件（共 20 個）

COM 事件介面為 `_ISKQuoteLibEvents`，掛載寫法 `m_pSKQuote.<事件> += new _ISKQuoteLibEvents_<事件>EventHandler(handler);`。文件行文中偶以 `SKQuoteLib_OnNotifyKLineData`、`SKQuoteLib_OnNotifyTicksLONG` 等「lib 前綴」稱呼同一事件。

### OnConnection
- 用途：接收連線狀態（連線/斷線/商品檔下載完成）。
- 簽名：`void OnConnection(int nKind, int nCode);`（IDL：`void OnConnection([in] LONG nKind, [in] LONG nCode);`）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| nKind | LONG | SK_SUBJECT_CONNECTION_CONNECTED（3001）連線；SK_SUBJECT_CONNECTION_DISCONNECT（3002）斷線；SK_SUBJECT_CONNECTION_STOCKS_READY（3003）商品基本資料下載完成；其他見代碼定義表 |
| nCode | LONG | 0 表正確，非 0 為例外事件 |

- 備註：避免在此事件內直接進行 EnterMonitor、LeaveMonitor、RequestStocks、RequestTicks 等連線/斷線/訂閱動作——全市場商品未下載完成前無法訂閱。DLL 載體宣告為 `event Action<string, int> OnConnection;`（第一參數國內行情固定 "SKQuote"）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:251-260`、`SKCOMTester/SKQuote.cs:124`

### OnNotifyKLineData
- 用途：回傳技術分析（K 線）資料，一筆一事件。
- 簽名：`void OnNotifyKLineData(string bstrStockNo, string bstrData);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 商品代號 |
| bstrData | BSTR | K 線資料字串，逗號分隔，共四種格式：(1) 舊版 1 分線 `月/日/年, 時:分, 開, 高, 低, 收, 量`；(2) 舊版日/週/月線 `月/日/年, 開, 高, 低, 收, 量`；(3) 新版 1 分線 `年/月/日, 時:分, 開, 高, 低, 收, 量`；(4) 新版日/週/月線 `年/月/日, 開, 高, 低, 收, 量` |

- 備註：舊版輸出價格未做小數處理（36.50 傳回 3650；期匯率商品 TypeNo=209 有四位小數，如 6.8712 傳回 68712）；新版輸出已做小數處理並由 Solace K 線主機提供。證券日 K 成交量為整股交易，不含鉅額交易、盤中零股、盤後零股。文件版本歷程中亦稱 SKQuoteLib_OnNotifyKLineData。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:564`、`SKCOMTester/SKQuote.cs:129`

### OnNotifyServerTime
- 用途：回傳查詢主機時間的結果。
- 簽名：`void OnNotifyServerTime(short sHour, short sMinute, short sSecond, int nTotal);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sHour | SHORT | 時 |
| sMinute | SHORT | 分 |
| sSecond | SHORT | 秒 |
| nTotal | LONG | 總秒數 |

- 備註：由 SKQuoteLib_RequestServerTime 觸發。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:214`、`SKCOMTester/SKQuote.cs:130`

### OnNotifyMarketTot
- 用途：回傳大盤成交張筆資料（由 GetMarketBuySellUpDown 觸發）。
- 簽名：`void OnNotifyMarketTot(short sMarketNo, short sPtr, int nTime, int nTotv, int nTots, int nTotc);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號（0x00 上市、0x01 上櫃） |
| sPtr | SHORT | 目前第幾筆資料 |
| nTime | LONG | 大盤成交時間（92000 = 09:20:00） |
| nTotv | LONG | 大盤成交值（億）；88542 = 885.42 億（除以 100） |
| nTots | LONG | 大盤成交張數 |
| nTotc | LONG | 大盤成交筆數 |

- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:432`、`SKCOMTester/SKQuote.cs:131`

### OnNotifyMarketBuySell
- 用途：回傳大盤成交買賣張筆數資料（由 GetMarketBuySellUpDown 觸發）。
- 簽名：`void OnNotifyMarketBuySell(short sMarketNo, short sPtr, int nTime, int nBc, int nSc, int nBs, int nSs);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號（0x00 上市、0x01 上櫃） |
| sPtr | SHORT | 目前第幾筆資料 |
| nTime | LONG | 大盤成交時間 |
| nBc | LONG | 大盤成交買進筆數 |
| nSc | LONG | 大盤成交賣出筆數 |
| nBs | LONG | 大盤成交買進張數 |
| nSs | LONG | 大盤成交賣出張數 |

- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:458`、`SKCOMTester/SKQuote.cs:132`

### OnNotifyMarketHighLow
- 用途：回傳大盤成交上漲下跌家數資料（由 GetMarketBuySellUpDown 觸發）。
- 簽名：`void OnNotifyMarketHighLow(short sMarketNo, short sPtr, int nTime, short sUp, short sDown, short sHigh, short sLow, short sNoChange);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號（0x00 上市、0x01 上櫃） |
| sPtr | SHORT | 目前第幾筆資料 |
| nTime | LONG | 大盤成交時間 |
| sUp | SHORT | 上漲家數 |
| sDown | SHORT | 下跌家數 |
| sHigh | SHORT | 漲停家數 |
| sLow | SHORT | 跌停家數 |
| sNoChange | SHORT | 平盤家數 |

- 備註：範例碼掛載處已註解，實務多改接 OnNotifyMarketHighLowNoWarrant（多了不含權證家數）。
- 範例：`SKCOMTester/SKQuote.cs:133`（掛載已註解）、handler 實作 `SKCOMTester/SKQuote.cs:749`

### OnNotifyStrikePrices
- 用途：回傳選擇權（履約價）商品資訊，由 GetStrikePrices 觸發。
- 簽名：`void OnNotifyStrikePrices(string bstrOptionData);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrOptionData | BSTR | 以「,」分隔：[選擇權商品代碼],[選擇權中文名],[Call商品買賣權代碼],[Put商品買賣權代碼],[履約價],[年+月],[最後交易日]。ex: TXO,台選,TXO11000H8,TXO11000T8,1100000,201808,20180815 |

- 備註：資料回傳完畢會收到一筆以「##」開頭的內容表示查詢結束。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:615`、`SKCOMTester/SKQuote.cs:137`

### OnNotifyStockList
- 用途：回傳指定國內市場各類股商品清單，由 RequestStockList 觸發。
- 簽名：`void OnNotifyStockList(short sMarketNo, string bstrStockData);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號 |
| bstrStockData | BSTR | 商品資料；「,」分隔欄位：[商品代碼],[商品名稱],[最後交易日],[交易所商品代碼],[跳動點],[幣別]；「;」分隔下一筆，各類股結尾以換行分隔 |

- 備註：回傳完畢以「##」開頭一筆表示查詢結束。跳動點格式：`10| 0.01 |/ 50| 0.05 |` 表示 0~10 跳 0.01、10~50 跳 0.05（「|」隔值、「/」隔區段）。V2.13.52 新增回傳跳動點、幣別欄位。
- 範例：`SKCOMTester/SKQuote.cs:138`（掛載已註解，V2 範例改接 OnNotifyCommodityListWithTypeNo）

### OnNotifyMarketHighLowNoWarrant
- 用途：回傳大盤漲跌家數（同時含『含權證』與『不含權證』），由 GetMarketBuySellUpDown 觸發。
- 簽名：`void OnNotifyMarketHighLowNoWarrant(short sMarketNo, int nPtr, int nTime, int nUp, int nDown, int nHigh, int nLow, int nNoChange, int nUpNoW, int nDownNoW, int nHighNoW, int nLowNoW, int nNoChangeNoW);`（IDL 末參數名 nUnChange/nUnChangeNoW）
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號（0x00 上市、0x01 上櫃） |
| nPtr | LONG | 目前第幾筆資料 |
| nTime | LONG | 大盤成交時間 |
| nUp / nDown / nHigh / nLow / nNoChange | LONG | 上漲/下跌/漲停/跌停/平盤家數 |
| nUpNoW / nDownNoW / nHighNoW / nLowNoW / nNoChangeNoW | LONG | 同上，但不含權證 |

- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:484`、`SKCOMTester/SKQuote.cs:140`

### OnNotifyCommodityListWithTypeNo
- 用途：回傳指定國內市場—含類別代碼及類別中文名稱之商品清單，由 RequestStockList 觸發。
- 簽名：`void OnNotifyCommodityListWithTypeNo(short sMarketNo, string bstrCommodityData);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號 |
| bstrCommodityData | BSTR | 開頭 `%類別代碼%類別中文名稱%`（ex: %1%水泥%），後接商品資料，欄位同 OnNotifyStockList |

- 備註：回傳完畢以「##」開頭一筆表示查詢結束。V2.13.52 新增跳動點、幣別欄位。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:576`、`SKCOMTester/SKQuote.cs:142`

### OnNotifyQuoteLONG
- 用途：(LONG index) 訂閱之個股報價異動通知。
- 簽名：`void OnNotifyQuoteLONG(short sMarketNo, int nIndex);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 報價異動商品的市場別 |
| nIndex | LONG | 系統所編的索引代碼 |

- 備註：以此兩參數呼叫 SKQuoteLib_GetStockByIndexLONG 取出報價內容。須以 EnterMonitorLONG 登入才會觸發。依市場可能需搭配 MarketNo。DLL 載體宣告為 `event Action<int, string> OnNotifyQuoteLONG;`（參數為 nMarketNo, strStockNo，改配 SKQuoteLib_GetStockByStockNo）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:358`、`SKCOMTester/SKQuote.cs:125`

### OnNotifyHistoryTicksLONG
- 用途：(LONG index) 首次索取個股成交明細時回補當天 Tick。
- 簽名：`void OnNotifyHistoryTicksLONG(short sMarketNo, int nIndex, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別 |
| nIndex | LONG | 系統索引代碼 |
| nPtr | LONG | 資料位址（Key） |
| nDate | LONG | 交易日期 YYYYMMDD |
| nTimehms | LONG | 時間1（時:分:秒） |
| nTimemillismicros | LONG | 時間2（毫秒微秒；目前 solace 僅證券商品提供） |
| nBid | LONG | 買價 |
| nAsk | LONG | 賣價 |
| nClose | LONG | 成交價 |
| nQty | LONG | 成交量 |
| nSimulate | LONG | 0:一般揭示 1:試算揭示 |

- 備註：未做揭示處理，需自行判斷一般/試算揭示。T 盤切 T+1 盤不保留前一盤資料，需自行清除。避免在本事件內呼叫 SKQuoteLib_GetTickLONG()、SKQuoteLib_GetStockByIndexLONG（僅可取基本資料）。盤中零股會有試撮（試算揭示）資料。須以 EnterMonitorLONG 登入才會觸發。
- 範例：`SKCOMTester/SKQuote.cs:126`（V2 範例 QuoteForm.cs:330 掛載已註解）

### OnNotifyTicksLONG
- 用途：(LONG index) 訂閱之個股成交明細異動即時通知。
- 簽名：`void OnNotifyTicksLONG(short sMarketNo, int nIndex, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate);`
- 參數：同 OnNotifyHistoryTicksLONG（見上表）。
- 備註：同 OnNotifyHistoryTicksLONG 的揭示/T+1/避免重入注意事項。RequestTicks、RequestTicksWithMarketNo、RequestLiveTick 皆由本事件通知即時 Tick。DLL 載體宣告 `event Action<int, string, int, int, int, int, int, int, int, int, int> OnNotifyTicksLONG;`（第二參數為 strStockNo）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:309-319`、`SKCOMTester/SKQuote.cs:127`

### OnNotifyBest5LONG
- 用途：(LONG index) 訂閱之個股五檔價格異動通知。
- 簽名：`void OnNotifyBest5LONG(short sMarketNo, int nStockidx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場代碼 |
| nStockidx | LONG | 系統索引代碼 |
| nBestBid1..5 / nBestBidQty1..5 | LONG | 第一～五檔買價/買量 |
| nExtendBid / nExtendBidQty | LONG | 衍生一檔買價/買量 |
| nBestAsk1..5 / nBestAskQty1..5 | LONG | 第一～五檔賣價/賣量 |
| nExtendAsk / nExtendAskQty | LONG | 衍生一檔賣價/賣量 |
| nSimulate | LONG | 0:一般揭示 1:試算揭示 |

- 備註：由 RequestTicks 系列觸發。未做揭示處理，需自行判斷試算揭示；盤中零股有試撮資料。須以 EnterMonitorLONG 登入才會觸發。價格通常需除以 100.0（見範例）。DLL 載體宣告 `event Action<int, string, int[], int[], int[], int[], int, int, int, int, int> OnNotifyBest5LONG;`（五檔以陣列傳遞）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:264-303`、`SKCOMTester/SKQuote.cs:128`

### OnNotifyBoolTunelLONG
- 用途：(LONG index) 回傳布林通道技術分析（日線—完整）。
- 簽名：`void OnNotifyBoolTunelLONG(short sMarketNo, int nStockidx, string bstrAVG, string bstrUBT, string bstrLBT);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號 |
| nStockidx | LONG | 系統索引代碼 |
| bstrAVG | BSTR | 均線 |
| bstrUBT | BSTR | 通道上端 |
| bstrLBT | BSTR | 通道下端 |

- 備註：目前只提供證券市場（不含盤中零股）。須以 EnterMonitorLONG 登入才會觸發。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:543`、`SKCOMTester/SKQuote.cs:135`

### OnNotifyMACDLONG
- 用途：(LONG index) 回傳證券市場 MACD 數值（日線—完整）。
- 簽名：`void OnNotifyMACDLONG(short sMarketNo, int nStockidx, string bstrMACD, string bstrDIF, string bstrOSC);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號 |
| nStockidx | LONG | 系統索引代碼 |
| bstrMACD | BSTR | MACD 平滑異同平均線 |
| bstrDIF | BSTR | DIF |
| bstrOSC | BSTR | OSC = DIF - MACD |

- 備註：目前只提供證券市場（不含盤中零股）。須以 EnterMonitorLONG 登入才會觸發。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:522`、`SKCOMTester/SKQuote.cs:134`

### OnNotifyFutureTradeInfoLONG
- 用途：(LONG index) 回傳期貨商品交易資訊（委託/成交統計），由 RequestFutureTradeInfo 觸發。
- 簽名：`void OnNotifyFutureTradeInfoLONG(string bstrStockNo, short sMarketNo, int nStockidx, int nBuyTotalCount, int nSellTotalCount, int nBuyTotalQty, int nSellTotalQty, int nBuyDealTotalCount, int nSellDealTotalCount);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrStockNo | BSTR | 商品代號 |
| sMarketNo | SHORT | 市場別代號 |
| nStockidx | LONG | 系統索引代碼（可搭配 GetStockByIndexLONG 取商品基本資訊） |
| nBuyTotalCount | LONG | 總委託買進筆數 |
| nSellTotalCount | LONG | 總委託賣出筆數 |
| nBuyTotalQty | LONG | 總委託買進口數 |
| nSellTotalQty | LONG | 總委託賣出口數 |
| nBuyDealTotalCount | LONG | 總成交買進筆數 |
| nSellDealTotalCount | LONG | 總成交賣出筆數 |

- 備註：須以 EnterMonitorLONG 登入才會觸發。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:598`、`SKCOMTester/SKQuote.cs:136`

### OnNotifyOddLotSpreadDeal
- 用途：回傳證券市場整零價差即時行情。
- 簽名：`void OnNotifyOddLotSpreadDeal(short sMarketNo, string bstrStockNo, int nDealPrice, int nDigit);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| sMarketNo | SHORT | 市場別代號 |
| bstrStockNo | BSTR | 商品代碼 |
| nDealPrice | LONG | 整零成交價差（負數含負號） |
| nDigit | LONG | 小數位數 |

- 備註：目前只提供證券市場；實際價差需以小數位數自行換算。公式：整零價差＝整股成交價－零股成交價；整股當日無成交以昨收價計；零股當日無成交則不計算。V2.13.46 修正宣告（移除 `[in] CHAR cSignDeal`）。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:223`、`SKCOMTester/SKQuote.cs:143`

### OnNotifyBest5Emerging
- 用途：(LONG index) 興櫃商品五檔異動通知；五檔單位為「股」。
- 簽名：`void OnNotifyBest5Emerging(short sMarketNo, int nStockidx, int nBestBid1, int nBestBidQty1, ..., int nExtendAsk, int nExtendAskQty, int nSimulate);`（參數結構與 OnNotifyBest5LONG 完全相同，共 27 個參數）
- 參數：同 OnNotifyBest5LONG（見該節參數表）。
- 備註：由 RequestTicks 系列觸發。未做揭示處理需自行判斷；須以 EnterMonitorLONG 登入才會觸發。
- 範例：範例未見（範例碼未掛載此事件）。

### OnKLineComplete
- 用途：歷史 KLine 收完通知；收到此事件表示 K 線回補完成。
- 簽名：`void OnKLineComplete(string bstrEndString);`
- 參數：

| 參數 | 型別 | 說明 |
|---|---|---|
| bstrEndString | BSTR | 結尾字串「##」 |

- 備註：資料全部回傳完畢後回傳一筆以「##」開頭的內容表示查詢結束。
- 範例：`SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:350-353`

### 舊版事件（使用 EnterMonitorLONG 後不再觸發）

OnNotifyQuote、OnNotifyHistoryTicks、OnNotifyTicks、OnNotifyBest5、OnNotifyBoolTunel、OnNotifyMACD、OnNotifyFutureTradeInfo — SHORT index 舊版事件，V2.13.46 起隨舊版函式移除；一律改接對應的 LONG 版事件。範例碼中 `m_SKQuoteLib_OnNotifyQuote`、`m_SKQuoteLib_OnNotifyBest5`、`m_SKQuoteLib_OnConnection` 等只是 handler 方法命名慣例，實際掛載的都是 LONG 版事件（見「初始化與事件註冊」）。

## 僅見於範例碼

以下名稱僅出現在範例碼的訊息字串（`SendReturnMessage(..., "名稱")`）或 UI 控件命名中，**不是**實際存在的 API 方法；列出對照避免誤判：

| 範例碼中的名稱 | 實際對應 | 出處 |
|---|---|---|
| SKQuoteLib_MarketTrading | 無此 API；按鈕訊息字串，該按鈕實際未呼叫任何方法（推測簽名：不存在） | `SKCOMTester/SKQuote.cs:1270` |
| SKQuoteLib_GetOneHistoryTick | 實際呼叫 `SKQuoteLib_GetTickLONG(sMarket, nStockidx, nPtr, ref skTick)` 的訊息標籤 | `SKCOMTester/SKQuote.cs:1300`（呼叫在 :1289） |
| SKQuoteLib_RequestMarketBuySellUpDown | 實際呼叫 `SKQuoteLib_GetMarketBuySellUpDown()` 的訊息標籤 | `SKCOMTester/SKQuote.cs:1307`（呼叫在 :1306） |
| SKQuoteLib_CancelRequstBoolTunel | 實際呼叫 `SKQuoteLib_RequestBoolTunel(50, ...)`（Page=50 即取消）；注意拼字 Requst | `SKCOMTester/SKQuote.cs:1325`（呼叫在 :1324） |
| SKQuoteLib_CancelRequstMACD | 實際呼叫 `SKQuoteLib_RequestMACD(50, ...)`（Page=50 即取消）；注意拼字 Requst | `SKCOMTester/SKQuote.cs:1331`（呼叫在 :1330） |
| SKQuoteLib_CancelRequestFutureTradeInfo | 實際呼叫 `SKQuoteLib_RequestFutureTradeInfo(50, ...)`（Page=50 即取消） | `SKCOMTester/SKQuote.cs:1353`（呼叫在 :1351） |
| SKQuoteLib_GetStrikePrice | 實際呼叫 `SKQuoteLib_GetStrikePrices()` 的訊息標籤（少一個 s） | `SKCOMTester/SKQuote.cs:1368`（呼叫在 :1366） |
| SKQuoteLib_RequestTicksWithMarketNos | UI 控件名 `checkBoxSKQuoteLib_RequestTicksWithMarketNos`（「盤中零股」勾選框），非方法 | `SKCOMTesterV2/WindowsFormsApp1/Quote/QuoteForm.cs:631` |

由此可推得未見於文件的重要用法：**MACD／布林通道／期貨交易資訊的「取消訂閱」沒有獨立 API，一律以原訂閱函式帶 Page=50 取消**。

## 名稱勘誤與抽取雜訊（文件端）

原始 docx→Markdown 抽取產生的黏字與官方文件本身的拼字錯誤，全部對照如下（皆非獨立 API）：

| 文件中出現的名稱 | 實際對應 | 出處 |
|---|---|---|
| SKQuoteLib_ReqeustStocks | SKQuoteLib_RequestStocks（官方文件拼字錯誤） | 主說明:2580、13.國內報價.md:334 |
| SKQuoteLib_ReqeustTicks | SKQuoteLib_RequestTicks（官方文件拼字錯誤） | 13.國內報價.md:276、341（備註欄） |
| SKQuoteLib_RequestStocksByMarketNo | SKQuoteLib_RequestStocksWithMarketNo（功能列表誤植） | 主說明:2286 |
| SKQuoteLib_GetStockByMarketAndNoSKQuoteLib | 表格黏字：SKQuoteLib_GetStockByMarketAndNo＋SKQuoteLib_GetStockByNoLONG 兩欄相連 | 主說明:177、2.導覽.md:119 |
| SKQuoteLib_RequestFutureTradeInfopsPageNo / SKQuoteLib_RequestFutureTradeInfobstrStockNo | 範例 UI 控件名 textBoxSKQuoteLib_RequestFutureTradeInfopsPageNo / ...bstrStockNo | 13.國內報價.md:734 |
| SKQuoteLib_RequestMACDpsPageNo | 範例 UI 控件名 textBoxSKQuoteLib_RequestMACDpsPageNo | 13.國內報價.md:517 |
| SKQuoteLib_RequestTicksWithMarketNosMarketNo | 範例 UI 控件名 comboBoxSKQuoteLib_RequestTicksWithMarketNosMarketNo | 13.國內報價.md:460 |

## 陷阱與注意

1. **連線順序是硬約束**：`SKQuoteLib_EnterMonitorLONG()` → 等 `OnConnection` 收到 `SK_SUBJECT_CONNECTION_STOCKS_READY`（3003）→ 才能 RequestStocks / RequestTicks / RequestStockList 等。在 OnConnection 事件內直接做連線/斷線/訂閱動作會失敗（商品檔未下載完成）。V2.13.46 曾修正收不到 3003 事件的問題。
2. **LONG index 世代**：V2.13.31 起商品總數可能超過 SHORT 上限（32767），V2.13.46 起舊版（非 LONG）函式與事件全部移除；EnterMonitor 與 EnterMonitorLONG 擇一，用 LONG 登入後只會觸發 LONG 版事件。
3. **訂閱數限制**：單一 SKQuoteLib 物件僅一個 RequestStocks 類即時報價訂閱（100 檔，RequestStocks 與 RequestStocksWithMarketNo 擇一）及 Tick 訂閱 10 檔（RequestTicks 與 RequestTicksWithMarketNo 不建議同物件混用）；重新連線即還原限制與設定。psPageNo：RequestStocks 固定帶 1（一般用戶上限 1），RequestTicks 從 0 開始。超過 100 檔僅取前 100 檔、代號不存在直接略過——**都不回傳錯誤**。
4. **雙物件跑整股＋零股**：官方建議建立兩個 SKQuoteLib 物件：物件1 跑 RequestStocks/RequestTicks（不含盤中零股），物件2 跑 RequestStocksWithMarketNo/RequestTicksWithMarketNo（盤中零股 5/6、客製化期選 9/10）。
5. **取消訂閱的兩套機制**：報價/Ticks 有專用 Cancel 函式；MACD、布林通道、期貨交易資訊則以原訂閱函式帶 **Page=50** 取消（文件備註，範例碼證實）。
6. **價格小數**：SKSTOCKLONG 內價格欄位為整數，一般除以 100.0 顯示；舊版 K 線輸出未做小數處理（期匯率商品 TypeNo=209 為四位小數）；新版（sOutType=1）已處理。證券市價買賣價判斷用 `SKQuoteLib_GetMarketPriceTS()` 回傳的特殊值 -2147483647。
7. **試算揭示**：Ticks/五檔事件的 nSimulate=1 為試撮（試算揭示）資料，API 不做揭示處理，開發者需自行過濾；盤中零股盤中也會進入試撮。
8. **事件重入禁忌**：勿在 OnNotifyTicksLONG / OnNotifyHistoryTicksLONG 內呼叫 GetTickLONG，勿在 OnNotifyBest5LONG 內呼叫 GetBest5LONG（僅可取得基本資料或造成問題）。
9. **T+1 盤**：期選 T 盤切 T+1 盤不保留前一盤資料，需自行清除；商品代號加 AM（如 TX00AM）可取純 AM 盤行情，但 AM 代號不能下單。
10. **帳戶與同意書**：未開立/未簽署證券 API 同意書無法取得上市/上櫃/興櫃/盤中零股資料，未簽期貨同意書無法取得期選資料與 RequestFutureTradeInfo、GetStrikePrices（錯誤代碼 3031）。
11. **keep-alive**：收盤後無資料流可能被防火牆斷線，官方建議每 15 秒呼叫 SKQuoteLib_RequestServerTime。
12. **LeaveMonitor 影響範圍**：會中斷所有 Solace 連線（報價＋回報），但不中斷模擬平台回報與公告；只想斷單一連線用 SKReplyLib 對應功能。
13. **技術分析限制**：MACD/布林通道僅證券市場；K 線 solace 僅 1 分鐘 K（5/30 分自行組）、無 288 日 K；盤中零股不提供歷史 K 線、大盤查詢、MACD、布林通道。
14. **GetQuoteStatus 僅限連線成功後使用**；回傳語意依 pbIsOutLimit 而不同（超限時為最大可用連線數，未超限時為先前已用連線數）。
15. **DLL 載體差異**：SKDLLCSharp 用 `SK.` 靜態類，連線改用 ManageServerConnection＋LoadCommodity；事件用 `event Action<...>`（OnNotifyQuoteLONG 直接給 strStockNo、Best5 用陣列）；零股訂閱為獨立方法 RequestStocksOddLot / RequestTicksOddLot；商品清單查詢 RequestStockList 直接回傳 StockListParser 物件而非事件。
16. **版本備註**：V2.13.57 修正 RequestLiveTick 訂閱數、RequestTicks/RequestTicksWithMarketNo 可無限訂閱問題；V2.13.52 起 OnNotifyStockList / OnNotifyCommodityListWithTypeNo 回傳內容新增跳動點、幣別。
