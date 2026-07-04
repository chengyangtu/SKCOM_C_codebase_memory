using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKCOMLib;

namespace SKCOMTester
{
    public partial class SKOSQuote : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        private bool m_bfirst = true;
        private int m_nCode;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        SKCOMLib.SKOSQuoteLib m_SKOSQuoteLib = null;
        public SKOSQuoteLib SKOSQuoteLib
        {
            get { return m_SKOSQuoteLib; }
            set { m_SKOSQuoteLib = value; }
        }

        public string m_strLoginID = "";
        public string LoginID
        {
            get { return m_strLoginID; }
            set
            {
                m_strLoginID = value;
            }
        }

        private DataTable m_dtBest5Ask;
        private DataTable m_dtBest5Bid;
        private DataTable m_dtBest10Ask;
        private DataTable m_dtBest10Bid;
        private DataTable m_dtForeigns;

        short m_nQuoteServedr = 0;

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public SKOSQuote()
        {
            InitializeComponent();
        }

        private void SKOSQuote_Load(object sender, EventArgs e)
        {
            m_dtForeigns = CreateStocksDataTable();
            m_dtBest5Ask = CreateBest5AskTable();
            m_dtBest5Bid = CreateBest5AskTable();
            m_dtBest10Ask = CreateBest5AskTable();
            m_dtBest10Bid = CreateBest5AskTable();

            SetDoubleBuffered(gridStocks);
        }

        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------

        void SendReturnMessage(string strType, int nCode, string strMessage)
        {
            if (GetMessage != null)
            {
                GetMessage(strType, nCode, strMessage);
            }
        }
        #endregion

        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_bfirst == true)
            {
                m_SKOSQuoteLib.OnConnect += new _ISKOSQuoteLibEvents_OnConnectEventHandler(this.OnConnect);
                m_SKOSQuoteLib.OnOverseaProducts += new _ISKOSQuoteLibEvents_OnOverseaProductsEventHandler(this.OnOverSeaProducts);
                m_SKOSQuoteLib.OnKLineData += new _ISKOSQuoteLibEvents_OnKLineDataEventHandler(this.OnKLineData);
                m_SKOSQuoteLib.OnNotifyServerTime += new _ISKOSQuoteLibEvents_OnNotifyServerTimeEventHandler(this.OnServerTime);
                m_SKOSQuoteLib.OnOverseaProductsDetail += new _ISKOSQuoteLibEvents_OnOverseaProductsDetailEventHandler(this.OnOverSeaProductsDetail);
                
                m_SKOSQuoteLib.OnNotifyQuoteLONG += new _ISKOSQuoteLibEvents_OnNotifyQuoteLONGEventHandler(this.OnQuoteUpdate);
                m_SKOSQuoteLib.OnNotifyTicksNineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyTicksNineDigitLONGEventHandler(this.OnNotifyTicksNineLONG);
                m_SKOSQuoteLib.OnNotifyHistoryTicksNineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyHistoryTicksNineDigitLONGEventHandler(this.OnNotifyHistoryTicksNineLONG);
                m_SKOSQuoteLib.OnNotifyBest5NineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyBest5NineDigitLONGEventHandler(this.OnNotifyBest5);
                m_SKOSQuoteLib.OnNotifyBest10NineDigitLONG += new _ISKOSQuoteLibEvents_OnNotifyBest10NineDigitLONGEventHandler(this.OnNotifyBest10);
                
                //m_SKOSQuoteLib.OnNotifyQuote += new _ISKOSQuoteLibEvents_OnNotifyQuoteEventHandler(this.OnQuoteUpdate);
                //m_SKOSQuoteLib.OnNotifyBest10NineDigit += new _ISKOSQuoteLibEvents_OnNotifyBest10NineDigitEventHandler(this.OnNotifyBest10);
                //m_SKOSQuoteLib.OnNotifyTicks += new _ISKOSQuoteLibEvents_OnNotifyTicksEventHandler(this.OnNotifyTicks);
                //m_SKOSQuoteLib.OnNotifyHistoryTicks += new _ISKOSQuoteLibEvents_OnNotifyHistoryTicksEventHandler(this.OnNotifyHistoryTicks);
                //m_SKOSQuoteLib.OnNotifyTicksNineDigit += new _ISKOSQuoteLibEvents_OnNotifyTicksNineDigitEventHandler(this.OnNotifyTicksNine);
                //m_SKOSQuoteLib.OnNotifyHistoryTicksNineDigit += new _ISKOSQuoteLibEvents_OnNotifyHistoryTicksNineDigitEventHandler(this.OnNotifyHistoryTicksNine);
                //m_SKOSQuoteLib.OnNotifyBest5NineDigit += new _ISKOSQuoteLibEvents_OnNotifyBest5NineDigitEventHandler(this.OnNotifyBest5);

                m_bfirst = false;
            }
            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_EnterMonitorLONG();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_EnterMonitorLONG");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_LeaveMonitor();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_LeaveMonitor");
        }

        private void btnTicks_Click(object sender, EventArgs e)
        {
            listTicks.Items.Clear();
            m_dtBest5Ask.Clear();
            m_dtBest5Bid.Clear();
            m_dtBest10Ask.Clear();
            m_dtBest10Bid.Clear();

            gridBest5Bid.DataSource = m_dtBest5Bid;
            gridBest5Ask.DataSource = m_dtBest5Ask;

            gridBest5Ask.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest5Ask.Columns["m_nAskQty"].Width = 60;
            gridBest5Ask.Columns["m_nAsk"].HeaderText = "賣價";
            gridBest5Ask.Columns["m_nAsk"].Width = 60;

            gridBest5Bid.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest5Bid.Columns["m_nAskQty"].Width = 60;
            gridBest5Bid.Columns["m_nAsk"].HeaderText = "買價";
            gridBest5Bid.Columns["m_nAsk"].Width = 60;

            gridBest10Bid.DataSource = m_dtBest10Bid;
            gridBest10Ask.DataSource = m_dtBest10Ask;

            gridBest10Ask.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest10Ask.Columns["m_nAskQty"].Width = 60;
            gridBest10Ask.Columns["m_nAsk"].HeaderText = "賣價";
            gridBest10Ask.Columns["m_nAsk"].Width = 60;

            gridBest10Bid.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest10Bid.Columns["m_nAskQty"].Width = 60;
            gridBest10Bid.Columns["m_nAsk"].HeaderText = "買價";
            gridBest10Bid.Columns["m_nAsk"].Width = 60;

            short nPage = 0;

            if (short.TryParse(txtOSTickPage.Text.ToString(), out nPage) == false)
                nPage = -1;

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestTicks(ref nPage, txtTick.Text.Trim());

            txtOSTickPage.Text = nPage.ToString();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestTicks");
        }

        private void btnOverseaProducts_Click(object sender, EventArgs e)
        {
            listOverseaProducts.Items.Clear();

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestOverseaProducts();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestOverseaProducts");
        }

        private void btnKLine_Click(object sender, EventArgs e)
        {

            listKLine.Items.Clear();

            string strStock = "";
            short nType = 0;

            strStock = txtKLine.Text.Trim();
            nType = short.Parse(boxKLineType.SelectedIndex.ToString());

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestKLine(strStock, nType);

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestKLine");
        }

        private void btnKLineByDate_Click(object sender, EventArgs e)
        {
            listKLine.Items.Clear();

            short nType = short.Parse(boxKLineType.SelectedIndex.ToString());
            string strStock = txtKLine.Text.Trim();
            string strStartDate = txtStartDate.Text.Trim();
            string strEndDate = txtEndDate.Text.Trim();
            short sMinuteNumber;

            if (short.TryParse(txtMinuteNumber.Text, out sMinuteNumber) == false)
            {
                sMinuteNumber = 0;
            }

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestKLineByDate(strStock, nType, strStartDate, strEndDate, sMinuteNumber);
            
            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestKLineByDate");
        }

        private void btnServerTime_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestServerTime();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestServerTime");
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if (m_nQuoteServedr == 0)
            {
                groupBox1.Text = "Server 1";
                m_nQuoteServedr = 1;
            }
            else
            {
                groupBox1.Text = "Server 0";
                m_nQuoteServedr = 0;
            }

            m_SKOSQuoteLib.SKOSQuoteLib_SetOSQuoteServer(m_nQuoteServedr);
        }

        private void btnQueryStocks_Click(object sender, EventArgs e)
        {

            m_dtForeigns.Clear();
            gridStocks.ClearSelection();

            gridStocks.DataSource = m_dtForeigns;

            gridStocks.Columns["m_nStockidx"].Visible = false;
            gridStocks.Columns["m_sDecimal"].Visible = false;
            gridStocks.Columns["m_nDenominator"].Visible = false;
            gridStocks.Columns["m_cMarketNo"].Visible = false;
            gridStocks.Columns["m_caExchangeNo"].HeaderText = "交易所代碼";
            gridStocks.Columns["m_caExchangeName"].HeaderText = "交易所名稱";
            gridStocks.Columns["m_caStockNo"].HeaderText = "商品代碼";
            gridStocks.Columns["m_caStockName"].HeaderText = "商品名稱";

            gridStocks.Columns["m_nOpen"].HeaderText = "開盤價";
            gridStocks.Columns["m_nHigh"].HeaderText = "最高價";
            gridStocks.Columns["m_nLow"].HeaderText = "最低價";
            gridStocks.Columns["m_nClose"].HeaderText = "成交價";
            gridStocks.Columns["m_dSettlePrice"].HeaderText = "結算價";
            gridStocks.Columns["m_nTickQty"].HeaderText = "單量";
            gridStocks.Columns["m_nRef"].HeaderText = "昨收價";

            gridStocks.Columns["m_nBid"].HeaderText = "買價";
            gridStocks.Columns["m_nBc"].HeaderText = "買量";
            gridStocks.Columns["m_nAsk"].HeaderText = "賣價";
            gridStocks.Columns["m_nAc"].HeaderText = "賣量";
            gridStocks.Columns["m_nTQty"].HeaderText = "成交量";

            short sPageNo = Convert.ToInt16(txtPageNo.Text);
            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestStocks(ref sPageNo, txtStocks.Text.Trim());

            txtPageNo.Text = sPageNo.ToString();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestStocks");
        }

        private void gridStocks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);

                if (e.Value != null)
                {
                    string strHeaderText = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText.ToString();

                    int nDenominator = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells["m_nDenominator"].Value.ToString());

                    if (strHeaderText == "名稱")
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.SkyBlue, e.CellBounds.X, e.CellBounds.Y);
                    }
                    else if (strHeaderText == "買價" || strHeaderText == "賣價" || strHeaderText == "成交價" || strHeaderText == "開盤價" || strHeaderText == "最高價" || strHeaderText == "最低價" || strHeaderText == "昨收價")
                    {
                        double dPrc = double.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells["m_nRef"].Value.ToString());

                        double dValue = double.Parse(e.Value.ToString());

                        string strCellValue = "";


                        if (nDenominator > 1)
                        {
                            string strValue = e.Value.ToString();

                            if (strValue.IndexOf('.') > -1)
                            {
                                int nValue1 = int.Parse(strValue.Substring(0, strValue.IndexOf('.')));

                                double dValue2 = double.Parse(strValue.Substring(strValue.IndexOf('.'), (strValue.Length - strValue.IndexOf('.'))));

                                if (Convert.ToDouble(strValue) >= 0 || Convert.ToDouble(strValue) <= -1)
                                    strCellValue = nValue1.ToString() + "'" + (dValue2 * nDenominator).ToString("#0.####");
                                else
                                    strCellValue = "-" + nValue1.ToString() + "'" + (dValue2 * nDenominator).ToString("#0.####");
                            }
                            else
                            {
                                strCellValue = strValue;
                            }
                        }
                        else
                        {
                            strCellValue = e.Value.ToString();
                        }

                        if (dValue > dPrc)
                        {
                            e.Graphics.DrawString(strCellValue, e.CellStyle.Font, Brushes.Red, e.CellBounds.X, e.CellBounds.Y);

                        }
                        else if (dValue < dPrc)
                        {
                            e.Graphics.DrawString(strCellValue, e.CellStyle.Font, Brushes.Lime, e.CellBounds.X, e.CellBounds.Y);
                        }
                        else
                        {
                            e.Graphics.DrawString(strCellValue, e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                        }
                    }
                    else if (strHeaderText == "單量" || strHeaderText == "成交量")
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Yellow, e.CellBounds.X, e.CellBounds.Y);
                    }
                    else
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X, e.CellBounds.Y);
                    }
                }
                e.Handled = true;
            }
        }

        private void btnGetTick_Click(object sender, EventArgs e)
        {
            int nStockidx;
            int nPtr;

            if (int.TryParse(txtTickStockidx.Text, out nStockidx) == false)
                return;

            if (int.TryParse(txtTickPtr.Text, out nPtr) == false)
                return;

            SKFOREIGNTICK_9 skTick = new SKFOREIGNTICK_9();

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_GetTickNineDigitLONG(nStockidx, nPtr, ref skTick);

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_GetTickNineDigitLONG");

            if (m_nCode == 0)
            {
                lblGetTick.Text = skTick.nDate.ToString()  + "/" + skTick.nTime.ToString() + "/" + skTick.nClose.ToString() + "/" + skTick.nQty.ToString();
            }
        }
        private void btnGetBest5_Click(object sender, EventArgs e)
        {
            int nStockidx;

            if (int.TryParse(txtBestStockidx.Text, out nStockidx) == false)
                return;

            SKBEST5_9 skBest5 = new SKBEST5_9();

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_GetBest5NineDigitLONG(nStockidx, ref skBest5);

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_GetBest5");

            if (m_nCode == 0)
            {
                lblBest5Bid.Text = skBest5.nBid1.ToString() + "/" + skBest5.nBidQty1.ToString() + " " + skBest5.nBid2.ToString() + "/" + skBest5.nBidQty2.ToString() + " ...";

                lblBest5Ask.Text = skBest5.nAsk1.ToString() + "/" + skBest5.nAskQty1.ToString() + " " + skBest5.nAsk2.ToString() + "/" + skBest5.nAskQty2.ToString() + " ...";
            }
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_Initialize();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_Initialize");
        }

        private void btnOverseaProducts2_Click(object sender, EventArgs e)
        {
            listOverseaProducts.Items.Clear();

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_GetOverseaProductDetail(1);
            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_GetOverseaProductsDetail");
        }

        private void btnIsConnected_Click(object sender, EventArgs e)
        {
            int nConnected = m_SKOSQuoteLib.SKOSQuoteLib_IsConnected();

            if (nConnected == 0)
            {
                ConnectedLabel.Text = "False_" + nConnected.ToString();
                ConnectedLabel.BackColor = Color.Red;
            }
            else if (nConnected == 1)
            {
                ConnectedLabel.Text = "True_" + nConnected.ToString();
                ConnectedLabel.BackColor = Color.Green;
            }
            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_IsConnected");
        }

        private void btnLiveTick_Click(object sender, EventArgs e)
        {
            listTicks.Items.Clear();

            short nPage = 0;

            if (short.TryParse(txtOSTickPage.Text.ToString(), out nPage) == false)
                nPage = -1;

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestLiveTick(ref nPage, txtTick.Text.Trim());

            txtOSTickPage.Text = nPage.ToString();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestLiveTick");
        }

        #endregion

        #region COM Event
        //----------------------------------------------------------------------
        // COM Event
        //----------------------------------------------------------------------
        void OnConnect(int nCode, int nSocketCode)
        {
            if (nCode == 3001 && nSocketCode == 0)
            {
                lblSignal.ForeColor = Color.Green;
            }
            else
            {
                lblSignal.ForeColor = Color.Red;
            }
        }

        void OnNotifyTicks(short sStockidx, int nPtr, int nDate, int nTime, int nClose, int nQty)
        {
            string strData = "[OnNotifyTicks]" + sStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            listTicks.Items.Add(strData);

            listTicks.SelectedIndex = listTicks.Items.Count - 1;
        }

        void OnNotifyHistoryTicks(short sStockidx, int nPtr, int nDate, int nTime, int nClose, int nQty)
        {
            string strData = "[OnNotifyHistoryTicks]" + sStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            listTicks.Items.Add(strData);

            listTicks.SelectedIndex = listTicks.Items.Count - 1;
        }

        void OnNotifyTicksNineLONG(int nStockidx, int nPtr, int nDate, int nTime, Int64 nClose, int nQty)
        {
            string strData = "[OnNotifyTicksNineLONG]" + nStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            listTicks.Items.Add(strData);

            listTicks.SelectedIndex = listTicks.Items.Count - 1;
        }

        void OnNotifyHistoryTicksNineLONG(int nStockidx, int nPtr, int nDate, int nTime, Int64 nClose, int nQty)
        {
            string strData = "[OnNotifyHistoryTicksNineLONG]" + nStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            listTicks.Items.Add(strData);

            listTicks.SelectedIndex = listTicks.Items.Count - 1;
        }

        void OnNotifyBest5(int nStockidx, Int64 nBestBid1, int nBestBidQty1, Int64 nBestBid2, int nBestBidQty2, Int64 nBestBid3, int nBestBidQty3, Int64 nBestBid4, int nBestBidQty4, Int64 nBestBid5, int nBestBidQty5,
                                Int64 nBestAsk1, int nBestAskQty1, Int64 nBestAsk2, int nBestAskQty2, Int64 nBestAsk3, int nBestAskQty3, Int64 nBestAsk4, int nBestAskQty4, Int64 nBestAsk5, int nBestAskQty5)
        {
            if (m_dtBest5Ask.Rows.Count == 0 && m_dtBest5Bid.Rows.Count == 0)
            {
                DataRow myDataRow;

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty1;
                myDataRow["m_nAsk"] = nBestAsk1;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty2;
                myDataRow["m_nAsk"] = nBestAsk2;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty3;
                myDataRow["m_nAsk"] = nBestAsk3;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty4;
                myDataRow["m_nAsk"] = nBestAsk4;
                m_dtBest5Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty5;
                myDataRow["m_nAsk"] = nBestAsk5;
                m_dtBest5Ask.Rows.Add(myDataRow);



                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty1;
                myDataRow["m_nAsk"] = nBestBid1;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty2;
                myDataRow["m_nAsk"] = nBestBid2;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty3;
                myDataRow["m_nAsk"] = nBestBid3;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty4;
                myDataRow["m_nAsk"] = nBestBid4;
                m_dtBest5Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest5Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty5;
                myDataRow["m_nAsk"] = nBestBid5;
                m_dtBest5Bid.Rows.Add(myDataRow);

            }
            else
            {
                m_dtBest5Ask.Rows[0]["m_nAskQty"] = nBestAskQty1;
                m_dtBest5Ask.Rows[0]["m_nAsk"] = nBestAsk1;

                m_dtBest5Ask.Rows[1]["m_nAskQty"] = nBestAskQty2;
                m_dtBest5Ask.Rows[1]["m_nAsk"] = nBestAsk2;

                m_dtBest5Ask.Rows[2]["m_nAskQty"] = nBestAskQty3;
                m_dtBest5Ask.Rows[2]["m_nAsk"] = nBestAsk3;

                m_dtBest5Ask.Rows[3]["m_nAskQty"] = nBestAskQty4;
                m_dtBest5Ask.Rows[3]["m_nAsk"] = nBestAsk4;

                m_dtBest5Ask.Rows[4]["m_nAskQty"] = nBestAskQty5;
                m_dtBest5Ask.Rows[4]["m_nAsk"] = nBestAsk5;


                m_dtBest5Bid.Rows[0]["m_nAskQty"] = nBestBidQty1;
                m_dtBest5Bid.Rows[0]["m_nAsk"] = nBestBid1;

                m_dtBest5Bid.Rows[1]["m_nAskQty"] = nBestBidQty2;
                m_dtBest5Bid.Rows[1]["m_nAsk"] = nBestBid2;

                m_dtBest5Bid.Rows[2]["m_nAskQty"] = nBestBidQty3;
                m_dtBest5Bid.Rows[2]["m_nAsk"] = nBestBid3;

                m_dtBest5Bid.Rows[3]["m_nAskQty"] = nBestBidQty4;
                m_dtBest5Bid.Rows[3]["m_nAsk"] = nBestBid4;

                m_dtBest5Bid.Rows[4]["m_nAskQty"] = nBestBidQty5;
                m_dtBest5Bid.Rows[4]["m_nAsk"] = nBestBid5;
            }
        }

        void OnNotifyBest10(int nStockIdx, Int64 nBestBid1, int nBestBidQty1, Int64 nBestBid2, int nBestBidQty2, Int64 nBestBid3, int nBestBidQty3,
                Int64 nBestBid4, int nBestBidQty4, Int64 nBestBid5, int nBestBidQty5, Int64 nBestBid6, int nBestBidQty6, Int64 nBestBid7, int nBestBidQty7,
                Int64 nBestBid8, int nBestBidQty8, Int64 nBestBid9, int nBestBidQty9, Int64 nBestBid10, int nBestBidQty10, Int64 nBestAsk1, int nBestAskQty1,
                Int64 nBestAsk2, int nBestAskQty2, Int64 nBestAsk3, int nBestAskQty3, Int64 nBestAsk4, int nBestAskQty4, Int64 nBestAsk5, int nBestAskQty5,
                Int64 nBestAsk6, int nBestAskQty6, Int64 nBestAsk7, int nBestAskQty7, Int64 nBestAsk8, int nBestAskQty8, Int64 nBestAsk9, int nBestAskQty9,
                Int64 nBestAsk10, int nBestAskQty10)
        {
            if (m_dtBest10Ask.Rows.Count == 0 && m_dtBest10Bid.Rows.Count == 0)
            {
                DataRow myDataRow;

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty1;
                myDataRow["m_nAsk"] = nBestAsk1;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty2;
                myDataRow["m_nAsk"] = nBestAsk2;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty3;
                myDataRow["m_nAsk"] = nBestAsk3;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty4;
                myDataRow["m_nAsk"] = nBestAsk4;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty5;
                myDataRow["m_nAsk"] = nBestAsk5;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty6;
                myDataRow["m_nAsk"] = nBestAsk6;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty7;
                myDataRow["m_nAsk"] = nBestAsk7;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty8;
                myDataRow["m_nAsk"] = nBestAsk8;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty9;
                myDataRow["m_nAsk"] = nBestAsk9;
                m_dtBest10Ask.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Ask.NewRow();
                myDataRow["m_nAskQty"] = nBestAskQty10;
                myDataRow["m_nAsk"] = nBestAsk10;
                m_dtBest10Ask.Rows.Add(myDataRow);



                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty1;
                myDataRow["m_nAsk"] = nBestBid1;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty2;
                myDataRow["m_nAsk"] = nBestBid2;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty3;
                myDataRow["m_nAsk"] = nBestBid3;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty4;
                myDataRow["m_nAsk"] = nBestBid4;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty5;
                myDataRow["m_nAsk"] = nBestBid5;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty6;
                myDataRow["m_nAsk"] = nBestBid6;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty7;
                myDataRow["m_nAsk"] = nBestBid7;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty8;
                myDataRow["m_nAsk"] = nBestBid8;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty9;
                myDataRow["m_nAsk"] = nBestBid9;
                m_dtBest10Bid.Rows.Add(myDataRow);

                myDataRow = m_dtBest10Bid.NewRow();
                myDataRow["m_nAskQty"] = nBestBidQty10;
                myDataRow["m_nAsk"] = nBestBid10;
                m_dtBest10Bid.Rows.Add(myDataRow);

            }
            else
            {
                m_dtBest10Ask.Rows[0]["m_nAskQty"] = nBestAskQty1;
                m_dtBest10Ask.Rows[0]["m_nAsk"] = nBestAsk1;

                m_dtBest10Ask.Rows[1]["m_nAskQty"] = nBestAskQty2;
                m_dtBest10Ask.Rows[1]["m_nAsk"] = nBestAsk2;

                m_dtBest10Ask.Rows[2]["m_nAskQty"] = nBestAskQty3;
                m_dtBest10Ask.Rows[2]["m_nAsk"] = nBestAsk3;

                m_dtBest10Ask.Rows[3]["m_nAskQty"] = nBestAskQty4;
                m_dtBest10Ask.Rows[3]["m_nAsk"] = nBestAsk4;

                m_dtBest10Ask.Rows[4]["m_nAskQty"] = nBestAskQty5;
                m_dtBest10Ask.Rows[4]["m_nAsk"] = nBestAsk5;

                m_dtBest10Ask.Rows[5]["m_nAskQty"] = nBestAskQty6;
                m_dtBest10Ask.Rows[5]["m_nAsk"] = nBestAsk6;

                m_dtBest10Ask.Rows[6]["m_nAskQty"] = nBestAskQty7;
                m_dtBest10Ask.Rows[6]["m_nAsk"] = nBestAsk7;

                m_dtBest10Ask.Rows[7]["m_nAskQty"] = nBestAskQty8;
                m_dtBest10Ask.Rows[7]["m_nAsk"] = nBestAsk8;

                m_dtBest10Ask.Rows[8]["m_nAskQty"] = nBestAskQty9;
                m_dtBest10Ask.Rows[8]["m_nAsk"] = nBestAsk9;

                m_dtBest10Ask.Rows[9]["m_nAskQty"] = nBestAskQty10;
                m_dtBest10Ask.Rows[9]["m_nAsk"] = nBestAsk10;


                m_dtBest10Bid.Rows[0]["m_nAskQty"] = nBestBidQty1;
                m_dtBest10Bid.Rows[0]["m_nAsk"] = nBestBid1;

                m_dtBest10Bid.Rows[1]["m_nAskQty"] = nBestBidQty2;
                m_dtBest10Bid.Rows[1]["m_nAsk"] = nBestBid2;

                m_dtBest10Bid.Rows[2]["m_nAskQty"] = nBestBidQty3;
                m_dtBest10Bid.Rows[2]["m_nAsk"] = nBestBid3;

                m_dtBest10Bid.Rows[3]["m_nAskQty"] = nBestBidQty4;
                m_dtBest10Bid.Rows[3]["m_nAsk"] = nBestBid4;

                m_dtBest10Bid.Rows[4]["m_nAskQty"] = nBestBidQty5;
                m_dtBest10Bid.Rows[4]["m_nAsk"] = nBestBid5;

                m_dtBest10Bid.Rows[5]["m_nAskQty"] = nBestBidQty6;
                m_dtBest10Bid.Rows[5]["m_nAsk"] = nBestBid6;

                m_dtBest10Bid.Rows[6]["m_nAskQty"] = nBestBidQty7;
                m_dtBest10Bid.Rows[6]["m_nAsk"] = nBestBid7;

                m_dtBest10Bid.Rows[7]["m_nAskQty"] = nBestBidQty8;
                m_dtBest10Bid.Rows[7]["m_nAsk"] = nBestBid8;

                m_dtBest10Bid.Rows[8]["m_nAskQty"] = nBestBidQty9;
                m_dtBest10Bid.Rows[8]["m_nAsk"] = nBestBid9;

                m_dtBest10Bid.Rows[9]["m_nAskQty"] = nBestBidQty10;
                m_dtBest10Bid.Rows[9]["m_nAsk"] = nBestBid10;
            }
        }

        void OnOverSeaProducts(string strValue)
        {
            listOverseaProducts.Items.Add("[OnOverSeaProducts]" + strValue);
        }

        void OnOverSeaProductsDetail(string strValue)
        {
            listOverseaProducts.Items.Add("[OnOverSeaProductsDetail]" + strValue);
        }

        void OnKLineData(string strStockNo, string strData)
        {
            listKLine.Items.Add("[OnKLineData]" + strStockNo + ":" + strData);
        }

        void OnServerTime(short sHour, short sMinute, short sSecond)
        {
            lblServerTime.Text = sHour.ToString("00") + "：" + sMinute.ToString("00") + "：" + sSecond.ToString("00");
        }

        void OnQuoteUpdate(int nStockidx)
        {
            SKFOREIGN_9LONG pForeignLONG = new SKFOREIGN_9LONG();
            try
            {
                  m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_GetStockByIndexNineDigitLONG(nStockidx, ref pForeignLONG);
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
            OnUpDateDataQuote(pForeignLONG);           
        }

        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;

            System.Reflection.PropertyInfo aProp =
                        typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        private DataTable CreateBest5AskTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAskQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }

        DataTable CreateStocksDataTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nStockidx";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sDecimal";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nDenominator";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_cMarketNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caExchangeNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caExchangeName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOpen";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nHigh";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nLow";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_dSettlePrice";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nRef";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int64");
            myDataColumn.ColumnName = "m_nTQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["m_nStockidx"] };

            return myDataTable;
        }

        void OnUpDateDataQuote(SKFOREIGN_9LONG pForeign_9LONG)
        {
            int nStockIdx = pForeign_9LONG.nStockIdx;

            DataRow drFind = m_dtForeigns.Rows.Find(nStockIdx);
            if (drFind == null)
            {
                DataRow myDataRow = m_dtForeigns.NewRow();

                myDataRow["m_nStockidx"] = pForeign_9LONG.nStockIdx;
                myDataRow["m_sDecimal"] = pForeign_9LONG.sDecimal;
                myDataRow["m_nDenominator"] = pForeign_9LONG.nDenominator;
                myDataRow["m_cMarketNo"] = pForeign_9LONG.bstrMarketNo.Trim();
                myDataRow["m_caExchangeNo"] = pForeign_9LONG.bstrExchangeNo.Trim();
                myDataRow["m_caExchangeName"] = pForeign_9LONG.bstrExchangeName.Trim();
                myDataRow["m_caStockNo"] = pForeign_9LONG.bstrStockNo.Trim();
                myDataRow["m_caStockName"] = pForeign_9LONG.bstrStockName.Trim();

                myDataRow["m_nOpen"] = pForeign_9LONG.nOpen / (Math.Pow(10, pForeign_9LONG.sDecimal));
                myDataRow["m_nHigh"] = pForeign_9LONG.nHigh / (Math.Pow(10, pForeign_9LONG.sDecimal));
                myDataRow["m_nLow"] = pForeign_9LONG.nLow / (Math.Pow(10, pForeign_9LONG.sDecimal));
                myDataRow["m_nClose"] = pForeign_9LONG.nClose / (Math.Pow(10, pForeign_9LONG.sDecimal));
                myDataRow["m_dSettlePrice"] = pForeign_9LONG.nSettlePrice / (Math.Pow(10, pForeign_9LONG.sDecimal));

                myDataRow["m_nTickQty"] = pForeign_9LONG.nTickQty;
                myDataRow["m_nRef"] = pForeign_9LONG.nRef / (Math.Pow(10, pForeign_9LONG.sDecimal));
                myDataRow["m_nBid"] = pForeign_9LONG.nBid / (Math.Pow(10, pForeign_9LONG.sDecimal));
                myDataRow["m_nBc"] = pForeign_9LONG.nBc;
                myDataRow["m_nAsk"] = pForeign_9LONG.nAsk;
                myDataRow["m_nAc"] = pForeign_9LONG.nAc / (Math.Pow(10, pForeign_9LONG.sDecimal));
                myDataRow["m_nTQty"] = pForeign_9LONG.nTQty;

                m_dtForeigns.Rows.Add(myDataRow);
            }
            else
            {
                drFind["m_nStockidx"] = pForeign_9LONG.nStockIdx;
                drFind["m_sDecimal"] = pForeign_9LONG.sDecimal;
                drFind["m_nDenominator"] = pForeign_9LONG.nDenominator;
                drFind["m_cMarketNo"] = pForeign_9LONG.bstrMarketNo.Trim();
                drFind["m_caExchangeNo"] = pForeign_9LONG.bstrExchangeNo.Trim();
                drFind["m_caExchangeName"] = pForeign_9LONG.bstrExchangeName.Trim();
                drFind["m_caStockNo"] = pForeign_9LONG.bstrStockNo.Trim();
                drFind["m_caStockName"] = pForeign_9LONG.bstrStockName.Trim();

                drFind["m_nOpen"] = pForeign_9LONG.nOpen / (Math.Pow(10, pForeign_9LONG.sDecimal));
                drFind["m_nHigh"] = pForeign_9LONG.nHigh / (Math.Pow(10, pForeign_9LONG.sDecimal));
                drFind["m_nLow"] = pForeign_9LONG.nLow / (Math.Pow(10, pForeign_9LONG.sDecimal));
                drFind["m_nClose"] = pForeign_9LONG.nClose / (Math.Pow(10, pForeign_9LONG.sDecimal));
                drFind["m_dSettlePrice"] = pForeign_9LONG.nSettlePrice / (Math.Pow(10, pForeign_9LONG.sDecimal));

                drFind["m_nTickQty"] = pForeign_9LONG.nTickQty;
                drFind["m_nRef"] = pForeign_9LONG.nRef / (Math.Pow(10, pForeign_9LONG.sDecimal));
                drFind["m_nBid"] = pForeign_9LONG.nBid / (Math.Pow(10, pForeign_9LONG.sDecimal));
                drFind["m_nBc"] = pForeign_9LONG.nBc;
                drFind["m_nAsk"] = pForeign_9LONG.nAsk / (Math.Pow(10, pForeign_9LONG.sDecimal));
                drFind["m_nAc"] = pForeign_9LONG.nAc;
                drFind["m_nTQty"] = pForeign_9LONG.nTQty;
            }
        }
        #endregion

        private void btnMarketDepth_Click(object sender, EventArgs e)
        {
            m_dtBest5Ask.Clear();
            m_dtBest5Bid.Clear();
            m_dtBest10Ask.Clear();
            m_dtBest10Bid.Clear();

            gridBest5Bid.DataSource = m_dtBest5Bid;
            gridBest5Ask.DataSource = m_dtBest5Ask;

            gridBest5Ask.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest5Ask.Columns["m_nAskQty"].Width = 60;
            gridBest5Ask.Columns["m_nAsk"].HeaderText = "賣價";
            gridBest5Ask.Columns["m_nAsk"].Width = 60;

            gridBest5Bid.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest5Bid.Columns["m_nAskQty"].Width = 60;
            gridBest5Bid.Columns["m_nAsk"].HeaderText = "買價";
            gridBest5Bid.Columns["m_nAsk"].Width = 60;

            gridBest10Bid.DataSource = m_dtBest10Bid;
            gridBest10Ask.DataSource = m_dtBest10Ask;

            gridBest10Ask.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest10Ask.Columns["m_nAskQty"].Width = 60;
            gridBest10Ask.Columns["m_nAsk"].HeaderText = "賣價";
            gridBest10Ask.Columns["m_nAsk"].Width = 60;

            gridBest10Bid.Columns["m_nAskQty"].HeaderText = "張數";
            gridBest10Bid.Columns["m_nAskQty"].Width = 60;
            gridBest10Bid.Columns["m_nAsk"].HeaderText = "買價";
            gridBest10Bid.Columns["m_nAsk"].Width = 60;

            short nPage = 0;

            if (short.TryParse(txtOSTickPage.Text.ToString(), out nPage) == false)
                nPage = -1;

            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_RequestMarketDepth(ref nPage, txtTick.Text.Trim());

            txtOSTickPage.Text = nPage.ToString();

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestMarketDepth");
        }

        private void btn_GetQuoteStatus_Click(object sender, EventArgs e)
        {
            bool bIsFull = false;
            int nCount = 0;
            m_nCode = m_SKOSQuoteLib.SKOSQuoteLib_GetQuoteStatus(ref nCount,ref  bIsFull);

            Status_Lbl.Text = nCount.ToString();
            if (bIsFull == true)
                Status_Lbl.BackColor = Color.Red;
            else
                Status_Lbl.BackColor = Color.Green;

            SendReturnMessage("OSQuote", m_nCode, "SKOSQuoteLib_GetQuoteStatus");
        }
    }
}
