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
    public partial class OOQuoteForm : Form
    {
        // 關閉標誌
        bool isClosing = false;
        string m_UserID;
        // 宣告物件
        SKCenterLib m_pSKCenter = new SKCenterLib(); // 登入&環境設定物件
        SKOOQuoteLib m_pSKOOQuote = new SKOOQuoteLib(); // 海選報價物件
        public OOQuoteForm(string UserID)
        {
            // Init
            {
                InitializeComponent();
                m_UserID = UserID;
                labelUserID.Text = UserID;
                // dataGridView
                {
                    // dataGridViewSKOOQuoteLib_GetStockByNoLONG
                    {
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column1", "商品代號");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column2", "商品名稱");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column3", "報價小數位數");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column4", "分母");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column5", "市場代碼");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column6", "交易所代號");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column7", "交易所名稱");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column8", "CallPut");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column9", "開盤價");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column10", "最高");

                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column11", "最低");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column12", "成交價");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column13", "結算價");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column14", "單量");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column15", "昨收(參考價)");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column16", "買價");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column17", "買量");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column18", "賣價");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column19", "賣量");
                        dataGridViewSKOOQuoteLib_GetStockByNoLONG.Columns.Add("Column20", "交易日");
                    }
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
                    // OnNotifyTicksLONG
                    {
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column1", "交易日期");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column2", "時：分：秒");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column3", "成交價");
                        dataGridViewOnNotifyTicksLONG.Columns.Add("Column4", "成交量");

                        for (int i = 0; i < 1; i++)
                            dataGridViewOnNotifyTicksLONG.Rows.Add();
                    }
                    // Ticks
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
                }
            }
        }
        private void OOQuoteForm_Load(object sender, EventArgs e)
        {
            //接收連線狀態
            m_pSKOOQuote.OnConnect += new _ISKOOQuoteLibEvents_OnConnectEventHandler(OnConnect);
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
                        msg = "【OnConnect】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nSocketCode);
                        richTextBoxMessage.AppendText(msg + "\n");
                    }
                }
            }
            //海期報價商品清單資訊
            m_pSKOOQuote.OnProducts += new _ISKOOQuoteLibEvents_OnProductsEventHandler(OnProducts);
            void OnProducts(string bstrValue)
            {
                if (isClosing != true)
                {
                    //取得回傳訊息
                    string msg = bstrValue;
                    richTextBoxOnProducts.AppendText(msg + "\n");
                }
            }
            //(LONG index)當有索取的海選商品報價異動時，將透過此事件通知應用程式處理
            m_pSKOOQuote.OnNotifyQuoteLONG += new _ISKOOQuoteLibEvents_OnNotifyQuoteLONGEventHandler(OnNotifyQuoteLONG);
            void OnNotifyQuoteLONG(int nIndex)
            {
                if (isClosing != true)
                {
                    bool stockFound = false; // dataGridView找不到，就新增一筆
                    SKFOREIGNLONG pSKStock = new SKFOREIGNLONG();
                    // (LONG index)根據系統所編的索引代碼，取回海選報價的相關資訊
                    int nCode = m_pSKOOQuote.SKOOQuoteLib_GetStockByIndexLONG(nIndex, ref pSKStock);
                    // 取得回傳訊息
                    string msg = "【SKOOQuoteLib_GetStockByIndexLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
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
            //(LONG index)當有索取的個股成交明細有所異動，即透過向此註冊事件回傳所異動的個股成交明細
            m_pSKOOQuote.OnNotifyTicksLONG += new _ISKOOQuoteLibEvents_OnNotifyTicksLONGEventHandler(OnNotifyTicksLONG);
            void OnNotifyTicksLONG(int nIndex, int nPtr, int nDate, int nTime, int nClose, int nQty)
            {
                if (isClosing != true)
                {
                    SKFOREIGNTICK pSKTick = new SKFOREIGNTICK();
                    int nCode = m_pSKOOQuote.SKOOQuoteLib_GetTickLONG(nIndex, nPtr, ref pSKTick);

                    // 取得回傳訊息
                    string msg = "【SKOOQuoteLib_GetTickLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
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
            //(LONG index)當首次索取個別海選商品成交明細，此事件會回補當天Tick 【沒用到喔】
            // m_pSKOOQuote.OnNotifyHistoryTicksLONG += new _ISKOOQuoteLibEvents_OnNotifyHistoryTicksLONGEventHandler(OnNotifyHistoryTicksLONG);
            void OnNotifyHistoryTicksLONG(int nIndex, int nPtr, int nDate, int nTime, int nClose, int nQty)
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
            //事件最佳五檔(海選報價)
            m_pSKOOQuote.OnNotifyBest5LONG += new _ISKOOQuoteLibEvents_OnNotifyBest5LONGEventHandler(OnNotifyBest5LONG);
            void OnNotifyBest5LONG(int nStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5)
            {
                if (isClosing != true)
                {
                    SKBEST5 pSKBest5 = new SKBEST5();
                    int nCode = m_pSKOOQuote.SKOOQuoteLib_GetBest5LONG(nStockIdx, ref pSKBest5);

                    // 取得回傳訊息
                    string msg = "【SKOOQuoteLib_GetBest5LONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
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
            //事件最佳十檔(海選報價)
            m_pSKOOQuote.OnNotifyBest10LONG += new _ISKOOQuoteLibEvents_OnNotifyBest10LONGEventHandler(OnNotifyBest10LONG);
            void OnNotifyBest10LONG(int nStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nBestBid6, int nBestBidQty6, int nBestBid7, int nBestBidQty7, int nBestBid8, int nBestBidQty8, int nBestBid9, int nBestBidQty9, int nBestBid10, int nBestBidQty10, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nBestAsk6, int nBestAskQty6, int nBestAsk7, int nBestAskQty7, int nBestAsk8, int nBestAskQty8, int nBestAsk9, int nBestAskQty9, int nBestAsk10, int nBestAskQty10)
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
        }
        private void OOQuoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            // 中斷報價伺服器連線
            m_pSKOOQuote.SKOOQuoteLib_LeaveMonitor();
        }
        private void buttonSearchKeyWord_Click(object sender, EventArgs e)
        {
            // 選擇整個文本範圍
            richTextBoxOnProducts.SelectAll();

            // 還原文字的背景色（取消高亮顯示）
            richTextBoxOnProducts.SelectionBackColor = richTextBoxOnProducts.BackColor;

            // 清除選擇
            richTextBoxOnProducts.DeselectAll();

            int index = 0;

            while (index < richTextBoxOnProducts.Text.Length)
            {
                // 在文字中查找搜尋文字的位置
                index = richTextBoxOnProducts.Find(searchText.Text, index, richTextBoxOnProducts.TextLength, RichTextBoxFinds.None);

                if (index != -1)
                {
                    // 如果找到，選中找到的文字
                    richTextBoxOnProducts.Select(index, searchText.Text.Length);

                    // 高亮顯示找到的文字
                    richTextBoxOnProducts.SelectionBackColor = Color.Yellow;

                    // 移動到下一個位置繼續搜尋
                    index += searchText.Text.Length;
                }
                else
                {
                    break;
                }
            }
        }
        private void buttonSKOOQuoteLib_EnterMonitorLONG_Click(object sender, EventArgs e)
        {
            // 與報價伺服器連線
            int nCode = m_pSKOOQuote.SKOOQuoteLib_EnterMonitorLONG();
            // 取得回傳訊息
            string msg = "【SKOOQuoteLib_EnterMonitorLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKOOQuoteLib_LeaveMonitor_Click(object sender, EventArgs e)
        {
            // 中斷報價伺服器連線
            int nCode = m_pSKOOQuote.SKOOQuoteLib_LeaveMonitor();
            // 取得回傳訊息
            string msg = "【SKOOQuoteLib_LeaveMonitor】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");
        }
        private void buttonSKOOQuoteLib_IsConnected_Click(object sender, EventArgs e)
        {
            // 檢查目前連線狀態
            int nCode = m_pSKOOQuote.SKOOQuoteLib_IsConnected();
            // 取得回傳訊息
            string msg = "";
            if (nCode == 1) msg = "連線中";
            else msg = "失敗";
            msg = "【SKOOQuoteLib_IsConnected】" + msg;
            richTextBoxMessage.AppendText(msg + "\n");
        }
        private void buttonSKOOQuoteLib_RequestProducts_Click(object sender, EventArgs e)
        {
            richTextBoxOnProducts.Clear();
            // 取得海外商品檔
            int nCode = m_pSKOOQuote.SKOOQuoteLib_RequestProducts();
            // 取得回傳訊息
            string msg = "【SKOOQuoteLib_RequestProducts】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");            
        }
        private void buttonSKOOQuoteLib_GetStockByNoLONG_Click(object sender, EventArgs e)
        {
            SKFOREIGNLONG pSKStock = new SKFOREIGNLONG();
            string bstrStockNo = textBoxSKOOQuoteLib_GetStockByNoLONG.Text;
            // (LONG index)根據商品代號，取回海選報價的相關資訊。
            int nCode = m_pSKOOQuote.SKOOQuoteLib_GetStockByNoLONG(bstrStockNo, ref pSKStock);

            // 取得回傳訊息
            string msg = "【SKOOQuoteLib_GetStockByNoLONG】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
            richTextBoxMethodMessage.AppendText(msg + "\n");

            // SKFOREIGNLONG
            bool found = false; // 找到就不新增資料
            string[] values = bstrStockNo.Split(','); //  取出商品代號
            foreach (DataGridViewRow row in dataGridViewSKOOQuoteLib_GetStockByNoLONG.Rows)
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
                dataGridViewSKOOQuoteLib_GetStockByNoLONG.Rows.Add(pSKStock.bstrStockNo, pSKStock.bstrStockName, pSKStock.sDecimal, pSKStock.nDenominator, pSKStock.bstrMarketNo, pSKStock.bstrExchangeNo, pSKStock.bstrExchangeName, pSKStock.bstrCallPut, pSKStock.nOpen, pSKStock.nHigh, pSKStock.nLow, pSKStock.nClose, pSKStock.nSettlePrice, pSKStock.nTickQty, pSKStock.nRef, pSKStock.nBid, pSKStock.nBc, pSKStock.nAsk, pSKStock.nAc, pSKStock.nTradingDay);
        }
        private void buttonSKOOQuoteLib_RequestStocks_Click(object sender, EventArgs e)
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
                int nCode = m_pSKOOQuote.SKOOQuoteLib_RequestStocks(psPageNo, textBoxStockNos.Text);

                // 取得回傳訊息
                string msg = "【SKOOQuoteLib_RequestStocks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKOOQuoteLib_RequestTicks_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                // 訂閱與要求傳送成交明細以及五檔
                short psPageNo = short.Parse(textBoxpsPageNo.Text);
                int nCode = m_pSKOOQuote.SKOOQuoteLib_RequestTicks(psPageNo, textBoxTicks.Text);

                // 訂閱與要求傳送即時成交明細(本功能不會訂閱最佳五檔與最佳十檔、亦不包含歷史Ticks)
                {
                    //int nCode = m_pSKOOQuote.SKOOQuoteLib_RequestLiveTick(psPageNo, textBoxTicks.Text);
                }
                // 取得回傳訊息
                string msg = "【SKOOQuoteLib_RequestTicks】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
        private void buttonSKOOQuoteLib_RequestMarketDepth_Click(object sender, EventArgs e)
        {
            if (textBoxpsPageNo.Text == "") // 防呆機制，要填寫完整資訊!
            {
                MessageBox.Show("請填寫完整資訊！", "", MessageBoxButtons.OK);
            }
            else
            {
                // 訂閱與要求傳送最佳十檔(僅包含最佳十、五檔；不包含訂閱歷史與即時成交明細)
                short psPageNo = short.Parse(textBoxpsPageNo.Text);
                int nCode = m_pSKOOQuote.SKOOQuoteLib_RequestMarketDepth(psPageNo, textBoxTicks.Text);

                // 取得回傳訊息
                string msg = "【SKOOQuoteLib_RequestMarketDepth】" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode);
                richTextBoxMethodMessage.AppendText(msg + "\n");
            }
        }
    }
}
