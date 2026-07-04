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
    public partial class SKOOQuote : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        private bool m_bfirst = true;
        private int m_nCode;
        private short m_nQuoteServer;

        SKCOMLib.SKOOQuoteLib m_SKOOQuoteLib = null;
        public SKOOQuoteLib SKOOQuoteLib
        {
            get { return m_SKOOQuoteLib; }
            set { m_SKOOQuoteLib = value; }
        }

        private DataTable m_dtBest5Ask;
        private DataTable m_dtBest5Bid;
        private DataTable m_dtBest10Ask;
        private DataTable m_dtBest10Bid;
        private DataTable m_dtForeigns;

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public SKOOQuote()
        {
            InitializeComponent();

            m_nQuoteServer = 0;
        }

        private void SKOOQuote_Load(object sender, EventArgs e)
        {
            m_dtForeigns = CreateStocksDataTable();
            m_dtBest5Ask = CreateBest5AskTable();
            m_dtBest5Bid = CreateBest5AskTable();
            m_dtBest10Ask = CreateBest5AskTable();
            m_dtBest10Bid = CreateBest5AskTable();

            SetDoubleBuffered(gridStocks);
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
                m_SKOOQuoteLib.OnConnect += new _ISKOOQuoteLibEvents_OnConnectEventHandler(m_SKOOQuoteLib_OnConnect);
                m_SKOOQuoteLib.OnProducts += new _ISKOOQuoteLibEvents_OnProductsEventHandler(m_SKOOQuoteLib_OnProducts);
                m_SKOOQuoteLib.OnNotifyQuoteLONG += new _ISKOOQuoteLibEvents_OnNotifyQuoteLONGEventHandler(m_SKOOQuoteLib_OnNotifyQuoteLONG);
                m_SKOOQuoteLib.OnNotifyTicksLONG += new _ISKOOQuoteLibEvents_OnNotifyTicksLONGEventHandler(m_SKOOQuoteLib_OnNotifyTicksLONG);
                m_SKOOQuoteLib.OnNotifyHistoryTicksLONG += new _ISKOOQuoteLibEvents_OnNotifyHistoryTicksLONGEventHandler(m_SKOOQuoteLib_OnNotifyHistoryTicksLONG);
                m_SKOOQuoteLib.OnNotifyBest5LONG += new _ISKOOQuoteLibEvents_OnNotifyBest5LONGEventHandler(m_SKOOQuoteLib_OnNotifyBest5LONG);
                m_SKOOQuoteLib.OnNotifyBest10LONG += new _ISKOOQuoteLibEvents_OnNotifyBest10LONGEventHandler(m_SKOOQuoteLib_OnNotifyBest10LONG);
                
                //m_SKOOQuoteLib.OnNotifyQuote += new _ISKOOQuoteLibEvents_OnNotifyQuoteEventHandler(m_SKOOQuoteLib_OnNotifyQuote);
                //m_SKOOQuoteLib.OnNotifyTicks += new _ISKOOQuoteLibEvents_OnNotifyTicksEventHandler(m_SKOOQuoteLib_OnNotifyTicks);
                //m_SKOOQuoteLib.OnNotifyHistoryTicks += new _ISKOOQuoteLibEvents_OnNotifyHistoryTicksEventHandler(m_SKOOQuoteLib_OnNotifyHistoryTicks);
                //m_SKOOQuoteLib.OnNotifyBest5 += new _ISKOOQuoteLibEvents_OnNotifyBest5EventHandler(m_SKOOQuoteLib_OnNotifyBest5);
                //m_SKOOQuoteLib.OnNotifyBest10 += new _ISKOOQuoteLibEvents_OnNotifyBest10EventHandler(m_SKOOQuoteLib_OnNotifyBest10);
                
                m_bfirst = false;
            }

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_EnterMonitorLONG();

            SendReturnMessage("SKOOQuoteLib_EnterMonitorLONG");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_LeaveMonitor();

            SendReturnMessage("SKOOQuoteLib_LeaveMonitor");
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            listProducts.Items.Clear();
            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_RequestProducts();

            SendReturnMessage("SKOOQuoteLib_RequestProducts");
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
            gridStocks.Columns["m_caStockName"].Width = 170;

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

            short sPageNo ;

            if (short.TryParse(txtOOQuotePage.Text.ToString(), out sPageNo) == false)
                sPageNo = -1;

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_RequestStocks(ref sPageNo, txtStocks.Text.Trim());

            txtOOQuotePage.Text = sPageNo.ToString();

            SendReturnMessage("SKOOQuoteLib_RequestStocks");
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

            short sPageNo;

            if (short.TryParse(txtOOTickPage.Text.ToString(), out sPageNo) == false)
                sPageNo = -1;

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_RequestTicks(ref sPageNo, txtTick.Text.ToString().Trim());

            txtOOTickPage.Text = sPageNo.ToString();

            SendReturnMessage("SKOOQuoteLib_RequestTiks");
        }

        private void btnGetTick_Click(object sender, EventArgs e)
        {
            int nStockidx;
            int nPtr;

            if (int.TryParse(txtTickStockidx.Text, out nStockidx) == false)
                return;

            if (int.TryParse(txtTickPtr.Text, out nPtr) == false)
                return;

            SKFOREIGNTICK skTick = new SKFOREIGNTICK();

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_GetTickLONG(nStockidx, nPtr, ref skTick);

            SendReturnMessage("SKOOQuoteLib_GetTickLONG");

            if (m_nCode == 0)
            {
                lblGetTick.Text = skTick.nDate.ToString() + " " + skTick.nTime.ToString() + "/" + skTick.nClose.ToString() + "/" + skTick.nQty.ToString();
            }
        }

        private void btnGetBest5_Click(object sender, EventArgs e)
        {
            int nStockidx;

            if (int.TryParse(txtBestStockidx.Text, out nStockidx) == false)
                return;

            SKBEST5 skBest5 = new SKBEST5();

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_GetBest5LONG(nStockidx, ref skBest5);

            SendReturnMessage("SKOOQuoteLib_GetBest5LONG");

            if (m_nCode == 0)
            {
                lblBest5Bid.Text = skBest5.nBid1.ToString() + "/" + skBest5.nBidQty1.ToString() + " " + skBest5.nBid2.ToString() + "/" + skBest5.nBidQty2.ToString() + " ...";

                lblBest5Ask.Text = skBest5.nAsk1.ToString() + "/" + skBest5.nAskQty1.ToString() + " " + skBest5.nAsk2.ToString() + "/" + skBest5.nAskQty2.ToString() + " ...";
            }
        }

        private void btnIsConnected_Click(object sender, EventArgs e)
        {
            int nConnected = m_SKOOQuoteLib.SKOOQuoteLib_IsConnected();

            if (nConnected == 0)
            {
                ConnectedLabel.Text = "False";
                ConnectedLabel.BackColor = Color.Red;
            }
            else if (nConnected == 1)
            {
                ConnectedLabel.Text = "True";
                ConnectedLabel.BackColor = Color.Green;
            }
            SendReturnMessage("SKOOQuoteLib_IsConnected");
        }

        private void btnLiveTick_Click(object sender, EventArgs e)
        {
            listTicks.Items.Clear();

            short sPageNo;

            if (short.TryParse(txtOOTickPage.Text.ToString(), out sPageNo) == false)
                sPageNo = -1;

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_RequestLiveTick(ref sPageNo, txtTick.Text.ToString().Trim());

            txtOOTickPage.Text = sPageNo.ToString();

            SendReturnMessage("SKOOQuoteLib_RequestLiveTick");
        }

        #endregion

        #region COM Event
        //----------------------------------------------------------------------
        // COM Event
        //----------------------------------------------------------------------
        void m_SKOOQuoteLib_OnConnect(int nCode, int nSocketCode)
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

        void m_SKOOQuoteLib_OnProducts(string bstrValue)
        {
            listProducts.Items.Add("[OnProducts]" + bstrValue);
            label8.Text = (Convert.ToInt32(label8.Text) + 1).ToString();
        }

        void m_SKOOQuoteLib_OnNotifyQuoteLONG(int nIndex)
        {
            SKFOREIGNLONG pForeignLONG = new SKFOREIGNLONG();

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_GetStockByIndexLONG(nIndex, ref pForeignLONG);

            SKFOREIGNLONG pForeignLONG_2 = new SKFOREIGNLONG();
            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_GetStockByNoLONG(pForeignLONG.bstrExchangeNo + "," + pForeignLONG.bstrStockNo, ref pForeignLONG_2);

            OnUpDateDataQuote(pForeignLONG);      
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

                                strCellValue = nValue1.ToString() + "'" + (dValue2 * nDenominator).ToString("#0.##");
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


        void m_SKOOQuoteLib_OnNotifyTicksLONG(int nStockIdx, int nPtr, int nDate, int nTime, int nClose, int nQty)
        {
            string strData = "[OnNotifyTicksLONG]" + nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            listTicks.Items.Add(strData);

            listTicks.SelectedIndex = listTicks.Items.Count - 1;
        }

        void m_SKOOQuoteLib_OnNotifyHistoryTicksLONG(int nStockIdx, int nPtr, int nDate, int nTime, int nClose, int nQty)
        {
            string strData = "[OnNotifyHistoryTickssLONG]" + nStockIdx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

            listTicks.Items.Add(strData);

            listTicks.SelectedIndex = listTicks.Items.Count - 1;
        }

        void m_SKOOQuoteLib_OnNotifyBest5LONG(int nStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5)
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

        void m_SKOOQuoteLib_OnNotifyBest10LONG(int nStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3,
                int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nBestBid6, int nBestBidQty6, int nBestBid7, int nBestBidQty7,
                int nBestBid8, int nBestBidQty8, int nBestBid9, int nBestBidQty9, int nBestBid10, int nBestBidQty10, int nBestAsk1, int nBestAskQty1,
                int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5,
                int nBestAsk6, int nBestAskQty6, int nBestAsk7, int nBestAskQty7, int nBestAsk8, int nBestAskQty8, int nBestAsk9, int nBestAskQty9,
                int nBestAsk10, int nBestAskQty10)
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
        


        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------

        void SendReturnMessage(string strMessage)
        {
            if (GetMessage != null)
            {
                GetMessage("OOQuote", m_nCode, strMessage);
            }
        }

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

        void OnUpDateDataQuote(SKFOREIGNLONG pForeignLONG)
        {
            int nStockIdx = pForeignLONG.nStockIdx;

            DataRow drFind = m_dtForeigns.Rows.Find(nStockIdx);
            if (drFind == null)
            {
                DataRow myDataRow = m_dtForeigns.NewRow();

                myDataRow["m_nStockidx"] = pForeignLONG.nStockIdx;
                myDataRow["m_sDecimal"] = pForeignLONG.sDecimal;
                myDataRow["m_nDenominator"] = pForeignLONG.nDenominator;
                myDataRow["m_cMarketNo"] = pForeignLONG.bstrMarketNo.Trim();
                myDataRow["m_caExchangeNo"] = pForeignLONG.bstrExchangeNo.Trim();
                myDataRow["m_caExchangeName"] = pForeignLONG.bstrExchangeName.Trim();
                myDataRow["m_caStockNo"] = pForeignLONG.bstrStockNo.Trim();
                myDataRow["m_caStockName"] = pForeignLONG.bstrStockName.Trim() + " " + pForeignLONG.nStrikePrice.ToString() + " " + pForeignLONG.bstrCallPut;

                myDataRow["m_nOpen"] = pForeignLONG.nOpen / (Math.Pow(10, pForeignLONG.sDecimal));
                myDataRow["m_nHigh"] = pForeignLONG.nHigh / (Math.Pow(10, pForeignLONG.sDecimal));
                myDataRow["m_nLow"] = pForeignLONG.nLow / (Math.Pow(10, pForeignLONG.sDecimal));
                myDataRow["m_nClose"] = pForeignLONG.nClose / (Math.Pow(10, pForeignLONG.sDecimal));
                myDataRow["m_dSettlePrice"] = pForeignLONG.nSettlePrice / (Math.Pow(10, pForeignLONG.sDecimal));

                myDataRow["m_nTickQty"] = pForeignLONG.nTickQty;
                myDataRow["m_nRef"] = pForeignLONG.nRef / (Math.Pow(10, pForeignLONG.sDecimal));
                myDataRow["m_nBid"] = pForeignLONG.nBid / (Math.Pow(10, pForeignLONG.sDecimal));
                myDataRow["m_nBc"] = pForeignLONG.nBc;
                myDataRow["m_nAsk"] = pForeignLONG.nAsk;
                myDataRow["m_nAc"] = pForeignLONG.nAc / (Math.Pow(10, pForeignLONG.sDecimal));
                myDataRow["m_nTQty"] = pForeignLONG.nTQty;

                m_dtForeigns.Rows.Add(myDataRow);
            }
            else
            {
                drFind["m_nStockidx"] = pForeignLONG.nStockIdx;
                drFind["m_sDecimal"] = pForeignLONG.sDecimal;
                drFind["m_nDenominator"] = pForeignLONG.nDenominator;
                drFind["m_cMarketNo"] = pForeignLONG.bstrMarketNo.Trim();
                drFind["m_caExchangeNo"] = pForeignLONG.bstrExchangeNo.Trim();
                drFind["m_caExchangeName"] = pForeignLONG.bstrExchangeName.Trim();
                drFind["m_caStockNo"] = pForeignLONG.bstrStockNo.Trim();
                drFind["m_caStockName"] = pForeignLONG.bstrStockName.Trim() + " " + pForeignLONG.nStrikePrice.ToString() + " " + pForeignLONG.bstrCallPut;

                drFind["m_nOpen"] = pForeignLONG.nOpen / (Math.Pow(10, pForeignLONG.sDecimal));
                drFind["m_nHigh"] = pForeignLONG.nHigh / (Math.Pow(10, pForeignLONG.sDecimal));
                drFind["m_nLow"] = pForeignLONG.nLow / (Math.Pow(10, pForeignLONG.sDecimal));
                drFind["m_nClose"] = pForeignLONG.nClose / (Math.Pow(10, pForeignLONG.sDecimal));
                drFind["m_dSettlePrice"] = pForeignLONG.nSettlePrice / (Math.Pow(10, pForeignLONG.sDecimal));

                drFind["m_nTickQty"] = pForeignLONG.nTickQty;
                drFind["m_nRef"] = pForeignLONG.nRef / (Math.Pow(10, pForeignLONG.sDecimal));
                drFind["m_nBid"] = pForeignLONG.nBid / (Math.Pow(10, pForeignLONG.sDecimal));
                drFind["m_nBc"] = pForeignLONG.nBc;
                drFind["m_nAsk"] = pForeignLONG.nAsk / (Math.Pow(10, pForeignLONG.sDecimal));
                drFind["m_nAc"] = pForeignLONG.nAc;
                drFind["m_nTQty"] = pForeignLONG.nTQty;
            }
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

        private DataTable CreateBesT10AskTable()
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

            if (short.TryParse(txtOOTickPage.Text.ToString(), out nPage) == false)
                nPage = -1;

            m_nCode = m_SKOOQuoteLib.SKOOQuoteLib_RequestMarketDepth(ref nPage, txtTick.Text.Trim());

            txtOOTickPage.Text = nPage.ToString();

            SendReturnMessage("SKOOQuoteLib_RequestMarketDepth");
        }
    }
}
