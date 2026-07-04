using System;
using SKCOMLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class OSQuoteForm : Form
    {
        // 關閉標誌
        bool isClosing = false;
        string m_UserID;
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
        SKOSQuoteLib m_pSKOSQuote = new SKOSQuoteLib(); // 海期報價物件
        public OSQuoteForm(string UserID)
        {
            InitializeComponent();
            m_UserID = UserID;

            //dataGridView
            {
                //dataGridViewTicks
                {
                    dataGridViewTicks.Columns.Add("Column1", "買量");
                    dataGridViewTicks.Columns.Add("Column2", "買價");
                    dataGridViewTicks.Columns.Add("Column3", "賣價");
                    dataGridViewTicks.Columns.Add("Column4", "賣量");

                    for (int i = 0; i < 5; i++)
                        dataGridViewTicks.Rows.Add();
                }

                // dataGridViewOnNotifyBest10NineDigitLONG
                {
                    dataGridViewOnNotifyBest10NineDigitLONG.Columns.Add("Column1", "買量");
                    dataGridViewOnNotifyBest10NineDigitLONG.Columns.Add("Column2", "買價");
                    dataGridViewOnNotifyBest10NineDigitLONG.Columns.Add("Column3", "賣價");
                    dataGridViewOnNotifyBest10NineDigitLONG.Columns.Add("Column4", "賣量");

                    for (int i = 0; i < 10; i++)
                        dataGridViewOnNotifyBest10NineDigitLONG.Rows.Add();
                }

                // dataGridViewOnNotifyTicksLONG
                {
                    dataGridViewOnNotifyTicksLONG.Columns.Add("Column1", "交易日期");
                    dataGridViewOnNotifyTicksLONG.Columns.Add("Column2", "時：分：秒");
                    dataGridViewOnNotifyTicksLONG.Columns.Add("Column3", "成交價");
                    dataGridViewOnNotifyTicksLONG.Columns.Add("Column4", "成交量");

                    for (int i = 0; i < 1; i++)
                        dataGridViewOnNotifyTicksLONG.Rows.Add();
                }
                //dataGridViewKLine
                {
                    dataGridViewKLine.Columns.Add("Column1", "日期(時間)");
                    dataGridViewKLine.Columns.Add("Column2", "開盤價");
                    dataGridViewKLine.Columns.Add("Column3", "最高價");
                    dataGridViewKLine.Columns.Add("Column4", "最低價");
                    dataGridViewKLine.Columns.Add("Column5", "收盤價");
                    dataGridViewKLine.Columns.Add("Column6", "成交量(張)/成交金額");
                }
                // SKFOREIGNLONG初始化
                {
                    // dataGridViewStocks
                    {
                        dataGridViewStocks.Columns.Add("Column1", "商品代號");
                        dataGridViewStocks.Columns.Add("Column2", "商品名稱");
                        dataGridViewStocks.Columns.Add("Column3", "報價小數位數");
                        dataGridViewStocks.Columns.Add("Column4", "分母");
                        dataGridViewStocks.Columns.Add("Column5", "市場代碼");
                        dataGridViewStocks.Columns.Add("Column6", "交易所代號");
                        dataGridViewStocks.Columns.Add("Column7", "交易所名稱");
                        dataGridViewStocks.Columns.Add("Column8", "CallPut");
                        dataGridViewStocks.Columns.Add("Column9", "開盤價");
                        dataGridViewStocks.Columns.Add("Column10", "最高");

                        dataGridViewStocks.Columns.Add("Column11", "最低");
                        dataGridViewStocks.Columns.Add("Column12", "成交價");
                        dataGridViewStocks.Columns.Add("Column13", "結算價");
                        dataGridViewStocks.Columns.Add("Column14", "單量");
                        dataGridViewStocks.Columns.Add("Column15", "昨收(參考價)");
                        dataGridViewStocks.Columns.Add("Column16", "買價");
                        dataGridViewStocks.Columns.Add("Column17", "買量");
                        dataGridViewStocks.Columns.Add("Column18", "賣價");
                        dataGridViewStocks.Columns.Add("Column19", "賣量");
                        dataGridViewStocks.Columns.Add("Column20", "交易日");
                    }
                    // dataGridViewSKOSQuoteLib_GetStockByNoLONG
                    {
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column1", "商品代號");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column2", "商品名稱");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column3", "報價小數位數");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column4", "分母");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column5", "市場代碼");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column6", "交易所代號");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column7", "交易所名稱");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column8", "CallPut");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column9", "開盤價");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column10", "最高");

                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column11", "最低");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column12", "成交價");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column13", "結算價");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column14", "單量");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column15", "昨收(參考價)");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column16", "買價");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column17", "買量");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column18", "賣價");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column19", "賣量");
                        dataGridViewSKOSQuoteLib_GetStockByNoLONG.Columns.Add("Column20", "交易日");
                    }
                }
            }
            //comboBox
            {
                //comboBoxsKLineType
                {
                    comboBoxsKLineType.Items.Add("分線");
                    comboBoxsKLineType.Items.Add("日線");
                    comboBoxsKLineType.Items.Add("週線");
                    comboBoxsKLineType.Items.Add("月線");
                }
            }
        }
        private void OSQuoteForm_Load(object sender, EventArgs e)
        {
            labelUserID.Text = m_UserID;
            
            //事件最佳五檔(海期報價)
            m_pSKOSQuote.OnNotifyBest5NineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyBest5NineDigitLONGEventHandler(OnNotifyBest5NineDigitLONG);
            void OnNotifyBest5NineDigitLONG(int nStockidx, long nBestBid1, int nBestBidQty1, long nBestBid2, int nBestBidQty2, long nBestBid3, int nBestBidQty3, long nBestBid4, int nBestBidQty4, long nBestBid5, int nBestBidQty5, long nBestAsk1, int nBestAskQty1, long nBestAsk2, int nBestAskQty2, long nBestAsk3, int nBestAskQty3, long nBestAsk4, int nBestAskQty4, long nBestAsk5, int nBestAskQty5)
            {
                if (isClosing != true)
                {
                    SKBEST5_9 pSKBest5 = new SKBEST5_9();
                    int nCode = m_pSKOSQuote.SKOSQuoteLib_GetBest5NineDigitLONG(nStockidx, ref pSKBest5);

                    // 取得回傳訊息
                    string msg = "【SKOSQuoteLib_GetBest5NineDigitLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    if (dataGridViewTicks.Rows.Count > 0)
                    {
                        DataGridViewRow Row1 = dataGridViewTicks.Rows[0];
                        Row1.Cells[0].Value = pSKBest5.nBidQty1; //買量
                        Row1.Cells[1].Value = pSKBest5.nBid1 / 100.0; //買價
                        Row1.Cells[2].Value = pSKBest5.nAsk1 / 100.0; //賣價
                        Row1.Cells[3].Value = pSKBest5.nAskQty1; //賣量

                        DataGridViewRow Row2 = dataGridViewTicks.Rows[1];
                        Row2.Cells[0].Value = pSKBest5.nBidQty2; //買量
                        Row2.Cells[1].Value = pSKBest5.nBid2 / 100.0; //買價
                        Row2.Cells[2].Value = pSKBest5.nAsk2 / 100.0; //賣價
                        Row2.Cells[3].Value = pSKBest5.nAskQty2; //賣量

                        DataGridViewRow Row3 = dataGridViewTicks.Rows[2];
                        Row3.Cells[0].Value = pSKBest5.nBidQty3; //買量
                        Row3.Cells[1].Value = pSKBest5.nBid3 / 100.0; //買價
                        Row3.Cells[2].Value = pSKBest5.nAsk3 / 100.0; //賣價
                        Row3.Cells[3].Value = pSKBest5.nAskQty3; //賣量

                        DataGridViewRow Row4 = dataGridViewTicks.Rows[3];
                        Row4.Cells[0].Value = pSKBest5.nBidQty4; //買量
                        Row4.Cells[1].Value = pSKBest5.nBid4 / 100.0; //買價
                        Row4.Cells[2].Value = pSKBest5.nAsk4 / 100.0; //賣價
                        Row4.Cells[3].Value = pSKBest5.nAskQty4; //賣量

                        DataGridViewRow Row5 = dataGridViewTicks.Rows[4];
                        Row5.Cells[0].Value = pSKBest5.nBidQty5; //買量
                        Row5.Cells[1].Value = pSKBest5.nBid5 / 100.0; //買價
                        Row5.Cells[2].Value = pSKBest5.nAsk5 / 100.0; //賣價
                        Row5.Cells[3].Value = pSKBest5.nAskQty5; //賣量
                    }
                }
            }

            //事件最佳十檔(海期報價)
            m_pSKOSQuote.OnNotifyBest10NineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyBest10NineDigitLONGEventHandler(OnNotifyBest10NineDigitLONG);
            void OnNotifyBest10NineDigitLONG(int nStockIdx, long nBestBid1, int nBestBidQty1, long nBestBid2, int nBestBidQty2, long nBestBid3, int nBestBidQty3, long nBestBid4, int nBestBidQty4, long nBestBid5, int nBestBidQty5, long nBestBid6, int nBestBidQty6, long nBestBid7, int nBestBidQty7, long nBestBid8, int nBestBidQty8, long nBestBid9, int nBestBidQty9, long nBestBid10, int nBestBidQty10, long nBestAsk1, int nBestAskQty1, long nBestAsk2, int nBestAskQty2, long nBestAsk3, int nBestAskQty3, long nBestAsk4, int nBestAskQty4, long nBestAsk5, int nBestAskQty5, long nBestAsk6, int nBestAskQty6, long nBestAsk7, int nBestAskQty7, long nBestAsk8, int nBestAskQty8, long nBestAsk9, int nBestAskQty9, long nBestAsk10, int nBestAskQty10)
            {
                if (isClosing != true)
                {
                    if (dataGridViewOnNotifyBest10NineDigitLONG.Rows.Count > 0)
                    {
                        DataGridViewRow Row1 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[0];
                        Row1.Cells[0].Value = nBestBidQty1; //買量
                        Row1.Cells[1].Value = nBestBid1 / 100.0; //買價
                        Row1.Cells[2].Value = nBestAsk1 / 100.0; //賣價
                        Row1.Cells[3].Value = nBestAskQty1; //賣量

                        DataGridViewRow Row2 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[1];
                        Row2.Cells[0].Value = nBestBidQty2; //買量
                        Row2.Cells[1].Value = nBestBid2 / 100.0; //買價
                        Row2.Cells[2].Value = nBestAsk2 / 100.0; //賣價
                        Row2.Cells[3].Value = nBestAskQty2; //賣量

                        DataGridViewRow Row3 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[2];
                        Row3.Cells[0].Value = nBestBidQty3; //買量
                        Row3.Cells[1].Value = nBestBid3 / 100.0; //買價
                        Row3.Cells[2].Value = nBestAsk3 / 100.0; //賣價
                        Row3.Cells[3].Value = nBestAskQty3; //賣量

                        DataGridViewRow Row4 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[3];
                        Row4.Cells[0].Value = nBestBidQty4; //買量
                        Row4.Cells[1].Value = nBestBid4 / 100.0; //買價
                        Row4.Cells[2].Value = nBestAsk4 / 100.0; //賣價
                        Row4.Cells[3].Value = nBestAskQty4; //賣量

                        DataGridViewRow Row5 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[4];
                        Row5.Cells[0].Value = nBestBidQty5; //買量
                        Row5.Cells[1].Value = nBestBid5 / 100.0; //買價
                        Row5.Cells[2].Value = nBestAsk5 / 100.0; //賣價
                        Row5.Cells[3].Value = nBestAskQty5; //賣量

                        DataGridViewRow Row6 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[5];
                        Row6.Cells[0].Value = nBestBidQty6; //買量
                        Row6.Cells[1].Value = nBestBid6 / 100.0; //買價
                        Row6.Cells[2].Value = nBestAsk6 / 100.0; //賣價
                        Row6.Cells[3].Value = nBestAskQty6; //賣量

                        DataGridViewRow Row7 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[6];
                        Row7.Cells[0].Value = nBestBidQty7; //買量
                        Row7.Cells[1].Value = nBestBid7 / 100.0; //買價
                        Row7.Cells[2].Value = nBestAsk7 / 100.0; //賣價
                        Row7.Cells[3].Value = nBestAskQty7; //賣量

                        DataGridViewRow Row8 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[7];
                        Row8.Cells[0].Value = nBestBidQty8; //買量
                        Row8.Cells[1].Value = nBestBid8 / 100.0; //買價
                        Row8.Cells[2].Value = nBestAsk8 / 100.0; //賣價
                        Row8.Cells[3].Value = nBestAskQty8; //賣量

                        DataGridViewRow Row9 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[8];
                        Row9.Cells[0].Value = nBestBidQty9; //買量
                        Row9.Cells[1].Value = nBestBid9 / 100.0; //買價
                        Row9.Cells[2].Value = nBestAsk9 / 100.0; //賣價
                        Row9.Cells[3].Value = nBestAskQty9; //賣量

                        DataGridViewRow Row10 = dataGridViewOnNotifyBest10NineDigitLONG.Rows[9];
                        Row10.Cells[0].Value = nBestBidQty10; //買量
                        Row10.Cells[1].Value = nBestBid10 / 100.0; //買價
                        Row10.Cells[2].Value = nBestAsk10 / 100.0; //賣價
                        Row10.Cells[3].Value = nBestAskQty10; //賣量
                    }
                }
            }

            //(LONG index)當有索取的個股成交明細有所異動，即透過向此註冊事件回傳所異動的個股成交明細
            m_pSKOSQuote.OnNotifyTicksNineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyTicksNineDigitLONGEventHandler(OnNotifyTicksNineDigitLONG);
            void OnNotifyTicksNineDigitLONG(int nIndex, int nPtr, int nDate, int nTime, long nClose, int nQty)
            {
                if (isClosing != true)
                {
                    SKFOREIGNTICK_9 pSKTick = new SKFOREIGNTICK_9();
                    int nCode = m_pSKOSQuote.SKOSQuoteLib_GetTickNineDigitLONG(nIndex, nPtr, ref pSKTick);

                    // 取得回傳訊息
                    string msg = "【SKOSQuoteLib_GetTickNineDigitLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    if (dataGridViewOnNotifyTicksLONG.Rows.Count > 0)
                    {
                        DataGridViewRow Row1 = dataGridViewOnNotifyTicksLONG.Rows[0];
                        Row1.Cells[0].Value = pSKTick.nDate; // 交易日期。(YYYYMMDD)
                        Row1.Cells[1].Value = pSKTick.nTime; // 時間1。(時：分：秒)
                        Row1.Cells[2].Value = pSKTick.nClose / 100.0; // 成交價
                        Row1.Cells[3].Value = pSKTick.nQty; // 成交量
                    }
                }
            }
            //(LONG index)當首次索取個別海期商品成交明細，此事件會回補當天Tick 【沒用到喔】
            //m_pSKOSQuote.OnNotifyHistoryTicksNineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyHistoryTicksNineDigitLONGEventHandler(OnNotifyHistoryTicksNineDigitLONG);
            void OnNotifyHistoryTicksNineDigitLONG(int nIndex, int nPtr, int nDate, int nTime, long nClose, int nQty)
            {
                if (isClosing != true)
                {
                    if (dataGridViewOnNotifyTicksLONG.Rows.Count > 0)
                    {
                        DataGridViewRow Row1 = dataGridViewOnNotifyTicksLONG.Rows[0];
                        Row1.Cells[0].Value = nDate; // 交易日期。(YYYYMMDD)
                        Row1.Cells[1].Value = nTime; // 時間1。(時：分：秒)
                        Row1.Cells[5].Value = nClose / 100.0; // 成交價
                        Row1.Cells[6].Value = nQty; // 成交量
                    }
                }
            }
            //歴史K線資料
            m_pSKOSQuote.OnKLineData += new _ISKOSQuoteLibEvents_OnKLineDataEventHandler(OnKLineData);
            void OnKLineData(string bstrStockNo, string bstrData)
            {
                // [日期(時間)], [開盤價],[最高價],[最低價],[收盤價],[ 成交量(張) 或 成交金額 ]
                string[] values = new string[6];
                values = bstrData.Split(',');
                dataGridViewKLine.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5]);
            }
            //事件報價通知
            m_pSKOSQuote.OnNotifyQuoteLONG += new _ISKOSQuoteLibEvents_OnNotifyQuoteLONGEventHandler(OnNotifyQuoteLONG);
            void OnNotifyQuoteLONG(int nIndex)
            {
                if (isClosing != true)
                {
                    bool stockFound = false;

                    // (LONG index)根據系統所編的索引代碼，取回海期報價的相關資訊

                    SKFOREIGNLONG pSKStock = new SKFOREIGNLONG();
                    int nCode = m_pSKOSQuote.SKOSQuoteLib_GetStockByIndexLONG(nIndex, ref pSKStock);

                    // (LONG index)(CME九位擴充)根據系統所編的索引代碼，取回海期報價的相關資訊。
                    {
                        // SKFOREIGN_9LONG pSKStock = new SKFOREIGN_9LONG();
                        // int nCode = m_pSKOSQuote.SKOSQuoteLib_GetStockByIndexNineDigitLONG(nIndex, ref pSKStock);
                    }

                    // 取得回傳訊息
                    string msg = "【SKQuoteLib_GetStockByIndexLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMethodMessage.AppendText(msg + "\n");

                    foreach (DataGridViewRow row in dataGridViewStocks.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == pSKStock.bstrStockNo)
                        {
                            row.Cells[2].Value = pSKStock.sDecimal;
                            row.Cells[3].Value = pSKStock.nDenominator;
                            row.Cells[4].Value = pSKStock.bstrMarketNo;
                            row.Cells[5].Value = pSKStock.bstrExchangeNo;
                            row.Cells[6].Value = pSKStock.bstrExchangeName;
                            row.Cells[7].Value = pSKStock.bstrCallPut;
                            row.Cells[8].Value = pSKStock.nOpen;
                            row.Cells[9].Value = pSKStock.nHigh;
                            row.Cells[10].Value = pSKStock.nLow;
                            row.Cells[11].Value = pSKStock.nClose;
                            row.Cells[12].Value = pSKStock.nSettlePrice;
                            row.Cells[13].Value = pSKStock.nTickQty;
                            row.Cells[14].Value = pSKStock.nRef;
                            row.Cells[15].Value = pSKStock.nBid;
                            row.Cells[16].Value = pSKStock.nBc;
                            row.Cells[17].Value = pSKStock.nAsk;
                            row.Cells[18].Value = pSKStock.nAc;
                            row.Cells[19].Value = pSKStock.nTradingDay;
                            stockFound = true;
                            break;
                        }
                    }
                    if (stockFound == false)
                    {
                        dataGridViewStocks.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.sDecimal, pSKStock.nDenominator, pSKStock.bstrMarketNo, pSKStock.bstrExchangeNo, pSKStock.bstrExchangeName, pSKStock.bstrCallPut, pSKStock.nOpen, pSKStock.nHigh, pSKStock.nLow, pSKStock.nClose, pSKStock.nSettlePrice, pSKStock.nTickQty, pSKStock.nRef, pSKStock.nBid, pSKStock.nBc, pSKStock.nAsk, pSKStock.nAc, pSKStock.nTradingDay);
                    }
                }
            }
            //接收連線狀態
            m_pSKOSQuote.OnConnect += new _ISKOSQuoteLibEvents_OnConnectEventHandler(OnConnect);
            void OnConnect(int nCode, int nSocketCode)
            {
                if (isClosing != true)
                {
                    // 取得回傳訊息
                    string msg = "【OnConnect】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                    richTextBoxMessage.AppendText(msg + "\n");

                    if (nSocketCode != 0)
                    {
                        // 取得回傳訊息
                        msg = "【OnConnect例外事件】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nSocketCode);
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            //海期報價商品清單資訊
            m_pSKOSQuote.OnOverseaProducts += new _ISKOSQuoteLibEvents_OnOverseaProductsEventHandler(OnOverseaProducts);
            void OnOverseaProducts(string bstrValue)
            {
                //取得回傳訊息
               string msg = bstrValue;
                richTextBoxOnOverseaProducts.AppendText(msg + "\n");
            }

            //海期報價商品清單資訊(含下單代碼)
            m_pSKOSQuote.OnOverseaProductsDetail += new _ISKOSQuoteLibEvents_OnOverseaProductsDetailEventHandler(OnOverseaProductsDetail);
            void OnOverseaProductsDetail(string bstrValue)
            {
                //取得回傳訊息
               string msg = bstrValue;
                richTextBoxOnOverseaProductsDetail.AppendText(msg + "\n");
            }
        }

        private void buttonSKOSQuoteLib_EnterMonitorLONG_Click(object sender, EventArgs e)
        {
            // 與報價伺服器連線
            int nCode = m_pSKOSQuote.SKOSQuoteLib_EnterMonitorLONG();
            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_EnterMonitorLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_RequestOverseaProducts_Click(object sender, EventArgs e)
        {
            // 取得海外商品檔
            int nCode = m_pSKOSQuote.SKOSQuoteLib_RequestOverseaProducts();
            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_RequestOverseaProducts】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_GetOverseaProductDetail_Click(object sender, EventArgs e)
        {
            short sType = 1;
            // 取得海期商品檔(含下單代碼)
            int nCode = m_pSKOSQuote.SKOSQuoteLib_GetOverseaProductDetail(sType);
            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_GetOverseaProductDetail】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_LeaveMonitor_Click(object sender, EventArgs e)
        {
            // 中斷報價伺服器連線
            int nCode = m_pSKOSQuote.SKOSQuoteLib_LeaveMonitor();
            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_LeaveMonitor】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        short sServer = 0; // 0：預設  1：備援
        private void buttonSKOSQuoteLib_SetOSQuoteServer_Click(object sender, EventArgs e)
        {
            if (sServer == 0) sServer = 1;
            else sServer = 0;
            // 切換海期、海選報價資訊源
            int nCode = m_pSKOSQuote.SKOSQuoteLib_SetOSQuoteServer(sServer);
            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_SetOSQuoteServer】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
            // 切換至...
            if (sServer == 0) msg = "預設";
            else msg = "備援";
            msg = "切換海期、海選報價資訊源至:" + msg;
            richTextBoxMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_Initialize_Click(object sender, EventArgs e)
        {
            // 重新初始海期物件
            int nCode = m_pSKOSQuote.SKOSQuoteLib_Initialize();
            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_Initialize】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_IsConnected_Click(object sender, EventArgs e)
        {
            // 檢查目前連線狀態
            int nCode = m_pSKOSQuote.SKOSQuoteLib_IsConnected();
            // 取得回傳訊息
            string msg;
            if (nCode == 1) msg = "連線中";
            else msg = "失敗";          
            richTextBoxMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_GetQuoteStatus_Click(object sender, EventArgs e)
        {
            int pnConnectionCount = 0; // 當pbIsOutLimit連線數超過限制為true,連線數為目前限制最大可使用連線數；當pbIsOutLimit連線數超過限制為false, 連線數為先前已使用連線數(不含當次新連線)

            bool pbIsOutLimit = false; // 報價連線數是超過限制，當pbIsOutLimit 帶入false，函式庫會回傳是否超過連線數布林值，並回傳給呼叫端
            // 查詢報價連線狀態(是否超過報價連線限制,連線數資訊)
            int nCode = m_pSKOSQuote.SKOSQuoteLib_GetQuoteStatus(ref pnConnectionCount, ref pbIsOutLimit);
            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_GetQuoteStatus】" + "連線數:" + pnConnectionCount + "超過限制:" + pbIsOutLimit;
            richTextBoxMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_RequestTicks_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                // 訂閱與要求傳送成交明細以及五檔
                short psPageNo = short.Parse(textBoxpsPageNo.Text);
                int nCode = m_pSKOSQuote.SKOSQuoteLib_RequestTicks(psPageNo, textBoxTicks.Text);

                // 取得回傳訊息
                string msg = "【SKOSQuoteLib_RequestTicks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }

        private void buttonSearchKeyword_Click(object sender, EventArgs e)
        {
            // 選擇整個文本範圍
            richTextBoxOnOverseaProductsDetail.SelectAll();
            // 還原文字的背景色（取消高亮顯示）
            richTextBoxOnOverseaProductsDetail.SelectionBackColor = richTextBoxOnOverseaProductsDetail.BackColor;
            // 清除選擇
            richTextBoxOnOverseaProductsDetail.DeselectAll();
            int index = 0;
            while (index < richTextBoxOnOverseaProductsDetail.Text.Length)
            {
                // 在文字中查找搜尋文字的位置
                index = richTextBoxOnOverseaProductsDetail.Find(TextBoxSearchKeyword.Text, index, richTextBoxOnOverseaProductsDetail.TextLength, RichTextBoxFinds.None);

                if (index != -1)
                {
                    // 如果找到，選中找到的文字
                    richTextBoxOnOverseaProductsDetail.Select(index, TextBoxSearchKeyword.Text.Length);

                    // 高亮顯示找到的文字
                    richTextBoxOnOverseaProductsDetail.SelectionBackColor = Color.Yellow;

                    // 移動到下一個位置繼續搜尋
                    index += TextBoxSearchKeyword.Text.Length;
                }
                else
                {
                    break;
                }
            }
        }

        private void comboBoxsKLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxsKLineType.SelectedItem != null)
            {
                buttonSKOSQuoteLib_RequestKLineByDate.Enabled = true;
                textBoxsMinuteNumber.Enabled = true;
            }
        }

        private void buttonSKOSQuoteLib_RequestKLineByDate_Click(object sender, EventArgs e)
        {
            dataGridViewKLine.Rows.Clear();

            short sKLineType = 0;
            string bstrStartDate = textBoxbstrStartDate.Text;
            string bstrEndDate = textBoxbstrEndDate.Text;
            short sMinuteNumber = short.Parse(textBoxsMinuteNumber.Text);

            string selectValue = comboBoxsKLineType.SelectedItem.ToString();
            if (selectValue == "分線") sKLineType = 0;
            else if (selectValue == "日線") sKLineType = 1;
            else if (selectValue == "週線") sKLineType = 2;
            else if (selectValue == "月線") sKLineType = 3;

            // 取得K線資料，可指定日期區間，分K時可指定幾分K

            int nCode = m_pSKOSQuote.SKOSQuoteLib_RequestKLineByDate(textBoxbstrStockNo.Text, sKLineType, bstrStartDate, bstrEndDate, sMinuteNumber);

            // 取得K線資料，不可指定日期區間
            {
                //int nCode = m_pSKOSQuote.SKOSQuoteLib_RequestKLine(textBoxbstrStockNo.Text, sKLineType);
            }

            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_RequestKLineByDate】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }

        private void buttonSKOSQuoteLib_RequestStocks_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo2.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                dataGridViewStocks.Rows.Clear();

                short psPageNo = short.Parse(textBoxpsPageNo2.Text);
                // 訂閱指定商品即時報價，要求伺服器針對 bstrStockNos 內的商品代號做報價通知動作。報價更新由OnNotifyQuote事件取得更通知。
                int nCode = m_pSKOSQuote.SKOSQuoteLib_RequestStocks(psPageNo, textBoxStockNos.Text);

                // 取得回傳訊息
                string msg = "【SKOSQuoteLib_RequestStocks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }

        private void buttonSKOSQuoteLib_GetStockByNoNineDigitLONG_Click(object sender, EventArgs e)
        {
            dataGridViewSKOSQuoteLib_GetStockByNoLONG.Rows.Clear();

            SKFOREIGN_9LONG pSKStock = new SKFOREIGN_9LONG();
            string bstrStockNo = textBoxSKOSQuoteLib_GetStockByNoLONG.Text;
            // (LONG index)(CME九位小數擴充)根據商品代號，取回海期報價的相關資訊。
            int nCode = m_pSKOSQuote.SKOSQuoteLib_GetStockByNoNineDigitLONG(bstrStockNo, ref pSKStock);

            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_GetStockByNoNineDigitLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            // SKFOREIGNLONG
            bool found = false; // 找到就不新增資料
            string[] values = bstrStockNo.Split(','); // 取出商品代號
            foreach (DataGridViewRow row in dataGridViewSKOSQuoteLib_GetStockByNoLONG.Rows)
            {
                if (row.IsNewRow)
                    continue;
                if (row.Cells[0].Value != null)
                {
                    if (values[1] == row.Cells[0].Value.ToString())// 搜尋過的商品代號
                    {
                        found = true;
                        // Update existing row with new stock data
                        row.Cells[0].Value = pSKStock.bstrStockNo;
                        row.Cells[1].Value = pSKStock.bstrStockName;
                        row.Cells[2].Value = pSKStock.sDecimal;
                        row.Cells[3].Value = pSKStock.nDenominator;
                        row.Cells[4].Value = pSKStock.bstrMarketNo;
                        row.Cells[5].Value = pSKStock.bstrExchangeNo;
                        row.Cells[6].Value = pSKStock.bstrExchangeName;
                        row.Cells[7].Value = pSKStock.bstrCallPut;
                        row.Cells[8].Value = pSKStock.nOpen;
                        row.Cells[9].Value = pSKStock.nHigh;
                        row.Cells[10].Value = pSKStock.nLow;
                        row.Cells[11].Value = pSKStock.nClose;
                        row.Cells[12].Value = pSKStock.nSettlePrice;
                        row.Cells[13].Value = pSKStock.nTickQty;
                        row.Cells[14].Value = pSKStock.nRef;
                        row.Cells[15].Value = pSKStock.nBid;
                        row.Cells[16].Value = pSKStock.nBc;
                        row.Cells[17].Value = pSKStock.nAsk;
                        row.Cells[18].Value = pSKStock.nAc;
                        row.Cells[19].Value = pSKStock.nTradingDay;
                        break;
                    }
                }
            }
            if (found == false)
                dataGridViewSKOSQuoteLib_GetStockByNoLONG.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.sDecimal, pSKStock.nDenominator, pSKStock.bstrMarketNo, pSKStock.bstrExchangeNo, pSKStock.bstrExchangeName, pSKStock.bstrCallPut, pSKStock.nOpen, pSKStock.nHigh, pSKStock.nLow, pSKStock.nClose, pSKStock.nSettlePrice, pSKStock.nTickQty, pSKStock.nRef, pSKStock.nBid, pSKStock.nBc, pSKStock.nAsk, pSKStock.nAc, pSKStock.nTradingDay);
        }

        private void buttonSKOSQuoteLib_GetStockByNoLONG_Click(object sender, EventArgs e)
        {
            dataGridViewSKOSQuoteLib_GetStockByNoLONG.Rows.Clear();

            SKFOREIGNLONG pSKStock = new SKFOREIGNLONG();
            string bstrStockNo = textBoxSKOSQuoteLib_GetStockByNoLONG.Text;
            // (LONG index)根據商品代號，取回海期報價的相關資訊。
            int nCode = m_pSKOSQuote.SKOSQuoteLib_GetStockByNoLONG(bstrStockNo, ref pSKStock);

            // 取得回傳訊息
            string msg = "【SKOSQuoteLib_GetStockByNoLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            // SKFOREIGNLONG
            bool found = false; // 找到就不新增資料
            string[] values = bstrStockNo.Split(','); //  取出商品代號
            foreach (DataGridViewRow row in dataGridViewSKOSQuoteLib_GetStockByNoLONG.Rows)
            {
                if (row.IsNewRow)
                    continue;
                if (row.Cells[0].Value != null)
                {
                    if (values[1] == row.Cells[0].Value.ToString())// 搜尋過的商品代號
                    {
                        found = true;
                        // Update existing row with new stock data
                        row.Cells[0].Value = pSKStock.bstrStockNo;
                        row.Cells[1].Value = pSKStock.bstrStockName;
                        row.Cells[2].Value = pSKStock.sDecimal;
                        row.Cells[3].Value = pSKStock.nDenominator;
                        row.Cells[4].Value = pSKStock.bstrMarketNo;
                        row.Cells[5].Value = pSKStock.bstrExchangeNo;
                        row.Cells[6].Value = pSKStock.bstrExchangeName;
                        row.Cells[7].Value = pSKStock.bstrCallPut;
                        row.Cells[8].Value = pSKStock.nOpen;
                        row.Cells[9].Value = pSKStock.nHigh;
                        row.Cells[10].Value = pSKStock.nLow;
                        row.Cells[11].Value = pSKStock.nClose;
                        row.Cells[12].Value = pSKStock.nSettlePrice;
                        row.Cells[13].Value = pSKStock.nTickQty;
                        row.Cells[14].Value = pSKStock.nRef;
                        row.Cells[15].Value = pSKStock.nBid;
                        row.Cells[16].Value = pSKStock.nBc;
                        row.Cells[17].Value = pSKStock.nAsk;
                        row.Cells[18].Value = pSKStock.nAc;
                        row.Cells[19].Value = pSKStock.nTradingDay;
                        break;
                    }
                }
            }
            if (found == false)
                dataGridViewSKOSQuoteLib_GetStockByNoLONG.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.sDecimal, pSKStock.nDenominator, pSKStock.bstrMarketNo, pSKStock.bstrExchangeNo, pSKStock.bstrExchangeName, pSKStock.bstrCallPut, pSKStock.nOpen, pSKStock.nHigh, pSKStock.nLow, pSKStock.nClose, pSKStock.nSettlePrice, pSKStock.nTickQty, pSKStock.nRef, pSKStock.nBid, pSKStock.nBc, pSKStock.nAsk, pSKStock.nAc, pSKStock.nTradingDay);
        }
        private void buttonSKOSQuoteLib_RequestMarketDepth_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                // 訂閱與要求傳送最佳十檔(僅包含最佳十、五檔；不包含訂閱歷史與即時成交明細)
                short psPageNo = short.Parse(textBoxpsPageNo.Text);
                int nCode = m_pSKOSQuote.SKOSQuoteLib_RequestMarketDepth(psPageNo, textBoxTicks.Text);

                // 取得回傳訊息
                string msg = "【SKOSQuoteLib_RequestMarketDepth】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }

        private void OSQuoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            // 中斷報價伺服器連線
            m_pSKOSQuote.SKOSQuoteLib_LeaveMonitor();
        }
    }
}
