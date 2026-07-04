
namespace WindowsFormsApp1
{
    partial class OOQuoteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonSKOOQuoteLib_EnterMonitorLONG = new System.Windows.Forms.Button();
            this.buttonSKOOQuoteLib_RequestProducts = new System.Windows.Forms.Button();
            this.richTextBoxMethodMessage = new System.Windows.Forms.RichTextBox();
            this.searchText = new System.Windows.Forms.TextBox();
            this.buttonSearchKeyWord = new System.Windows.Forms.Button();
            this.richTextBoxOnProducts = new System.Windows.Forms.RichTextBox();
            this.panelOOQuoteForm = new System.Windows.Forms.Panel();
            this.tabControlQuote = new System.Windows.Forms.TabControl();
            this.tabPageOverseaOptionList = new System.Windows.Forms.TabPage();
            this.tabPageSKOSQuoteLib_GetStockByNoLONG = new System.Windows.Forms.TabPage();
            this.buttonSKOOQuoteLib_GetStockByNoLONG = new System.Windows.Forms.Button();
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG = new System.Windows.Forms.DataGridView();
            this.textBoxSKOOQuoteLib_GetStockByNoLONG = new System.Windows.Forms.TextBox();
            this.labelSKOOQuoteLib_GetStockByNoLONG = new System.Windows.Forms.Label();
            this.tabPageRequest = new System.Windows.Forms.TabPage();
            this.textBoxpsPageNo2 = new System.Windows.Forms.TextBox();
            this.labelpsPageNo2 = new System.Windows.Forms.Label();
            this.buttonSKOOQuoteLib_RequestStocks = new System.Windows.Forms.Button();
            this.textBoxStockNos = new System.Windows.Forms.TextBox();
            this.labelRequestStocks = new System.Windows.Forms.Label();
            this.dataGridViewStocks = new System.Windows.Forms.DataGridView();
            this.tabPageTicks = new System.Windows.Forms.TabPage();
            this.buttonSKOOQuoteLib_RequestMarketDepth = new System.Windows.Forms.Button();
            this.dataGridViewOnNotifyBest10NineDigitLONG = new System.Windows.Forms.DataGridView();
            this.dataGridViewOnNotifyTicksLONG = new System.Windows.Forms.DataGridView();
            this.labelPage = new System.Windows.Forms.Label();
            this.textBoxpsPageNo = new System.Windows.Forms.TextBox();
            this.textBoxTicks = new System.Windows.Forms.TextBox();
            this.labelTicks = new System.Windows.Forms.Label();
            this.buttonSKOOQuoteLib_RequestTicks = new System.Windows.Forms.Button();
            this.dataGridViewTicks = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSKOOQuoteLib_LeaveMonitor = new System.Windows.Forms.Button();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.labelUserID = new System.Windows.Forms.Label();
            this.buttonSKOOQuoteLib_IsConnected = new System.Windows.Forms.Button();
            this.panelOOQuoteForm.SuspendLayout();
            this.tabControlQuote.SuspendLayout();
            this.tabPageOverseaOptionList.SuspendLayout();
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSKOOQuoteLib_GetStockByNoLONG)).BeginInit();
            this.tabPageRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStocks)).BeginInit();
            this.tabPageTicks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnNotifyBest10NineDigitLONG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnNotifyTicksLONG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicks)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSKOOQuoteLib_EnterMonitorLONG
            // 
            this.buttonSKOOQuoteLib_EnterMonitorLONG.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_EnterMonitorLONG.Location = new System.Drawing.Point(3, 79);
            this.buttonSKOOQuoteLib_EnterMonitorLONG.Name = "buttonSKOOQuoteLib_EnterMonitorLONG";
            this.buttonSKOOQuoteLib_EnterMonitorLONG.Size = new System.Drawing.Size(187, 30);
            this.buttonSKOOQuoteLib_EnterMonitorLONG.TabIndex = 17;
            this.buttonSKOOQuoteLib_EnterMonitorLONG.Text = "連線報價主機";
            this.buttonSKOOQuoteLib_EnterMonitorLONG.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_EnterMonitorLONG.Click += new System.EventHandler(this.buttonSKOOQuoteLib_EnterMonitorLONG_Click);
            // 
            // buttonSKOOQuoteLib_RequestProducts
            // 
            this.buttonSKOOQuoteLib_RequestProducts.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_RequestProducts.Location = new System.Drawing.Point(6, 6);
            this.buttonSKOOQuoteLib_RequestProducts.Name = "buttonSKOOQuoteLib_RequestProducts";
            this.buttonSKOOQuoteLib_RequestProducts.Size = new System.Drawing.Size(168, 34);
            this.buttonSKOOQuoteLib_RequestProducts.TabIndex = 22;
            this.buttonSKOOQuoteLib_RequestProducts.Text = "取得海選商品檔";
            this.buttonSKOOQuoteLib_RequestProducts.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_RequestProducts.Click += new System.EventHandler(this.buttonSKOOQuoteLib_RequestProducts_Click);
            // 
            // richTextBoxMethodMessage
            // 
            this.richTextBoxMethodMessage.Location = new System.Drawing.Point(2, 3);
            this.richTextBoxMethodMessage.Name = "richTextBoxMethodMessage";
            this.richTextBoxMethodMessage.ReadOnly = true;
            this.richTextBoxMethodMessage.Size = new System.Drawing.Size(327, 52);
            this.richTextBoxMethodMessage.TabIndex = 24;
            this.richTextBoxMethodMessage.Text = "";
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(787, 32);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(100, 22);
            this.searchText.TabIndex = 26;
            // 
            // buttonSearchKeyWord
            // 
            this.buttonSearchKeyWord.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSearchKeyWord.Location = new System.Drawing.Point(664, 22);
            this.buttonSearchKeyWord.Name = "buttonSearchKeyWord";
            this.buttonSearchKeyWord.Size = new System.Drawing.Size(117, 34);
            this.buttonSearchKeyWord.TabIndex = 27;
            this.buttonSearchKeyWord.Text = "搜尋";
            this.buttonSearchKeyWord.UseVisualStyleBackColor = true;
            this.buttonSearchKeyWord.Click += new System.EventHandler(this.buttonSearchKeyWord_Click);
            // 
            // richTextBoxOnProducts
            // 
            this.richTextBoxOnProducts.Location = new System.Drawing.Point(6, 51);
            this.richTextBoxOnProducts.Name = "richTextBoxOnProducts";
            this.richTextBoxOnProducts.ReadOnly = true;
            this.richTextBoxOnProducts.Size = new System.Drawing.Size(497, 167);
            this.richTextBoxOnProducts.TabIndex = 28;
            this.richTextBoxOnProducts.Text = "";
            // 
            // panelOOQuoteForm
            // 
            this.panelOOQuoteForm.AutoScroll = true;
            this.panelOOQuoteForm.Controls.Add(this.tabControlQuote);
            this.panelOOQuoteForm.Controls.Add(this.panel1);
            this.panelOOQuoteForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOOQuoteForm.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.panelOOQuoteForm.Location = new System.Drawing.Point(0, 0);
            this.panelOOQuoteForm.Name = "panelOOQuoteForm";
            this.panelOOQuoteForm.Size = new System.Drawing.Size(1367, 262);
            this.panelOOQuoteForm.TabIndex = 29;
            // 
            // tabControlQuote
            // 
            this.tabControlQuote.Controls.Add(this.tabPageOverseaOptionList);
            this.tabControlQuote.Controls.Add(this.tabPageSKOSQuoteLib_GetStockByNoLONG);
            this.tabControlQuote.Controls.Add(this.tabPageRequest);
            this.tabControlQuote.Controls.Add(this.tabPageTicks);
            this.tabControlQuote.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlQuote.Location = new System.Drawing.Point(343, 3);
            this.tabControlQuote.Name = "tabControlQuote";
            this.tabControlQuote.SelectedIndex = 0;
            this.tabControlQuote.Size = new System.Drawing.Size(1021, 257);
            this.tabControlQuote.TabIndex = 77;
            // 
            // tabPageOverseaOptionList
            // 
            this.tabPageOverseaOptionList.AutoScroll = true;
            this.tabPageOverseaOptionList.Controls.Add(this.richTextBoxOnProducts);
            this.tabPageOverseaOptionList.Controls.Add(this.buttonSKOOQuoteLib_RequestProducts);
            this.tabPageOverseaOptionList.Controls.Add(this.searchText);
            this.tabPageOverseaOptionList.Controls.Add(this.buttonSearchKeyWord);
            this.tabPageOverseaOptionList.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageOverseaOptionList.Location = new System.Drawing.Point(4, 33);
            this.tabPageOverseaOptionList.Name = "tabPageOverseaOptionList";
            this.tabPageOverseaOptionList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverseaOptionList.Size = new System.Drawing.Size(1013, 220);
            this.tabPageOverseaOptionList.TabIndex = 3;
            this.tabPageOverseaOptionList.Text = "商品清單";
            this.tabPageOverseaOptionList.UseVisualStyleBackColor = true;
            // 
            // tabPageSKOSQuoteLib_GetStockByNoLONG
            // 
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.AutoScroll = true;
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Controls.Add(this.buttonSKOOQuoteLib_GetStockByNoLONG);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Controls.Add(this.dataGridViewSKOOQuoteLib_GetStockByNoLONG);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Controls.Add(this.textBoxSKOOQuoteLib_GetStockByNoLONG);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Controls.Add(this.labelSKOOQuoteLib_GetStockByNoLONG);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Location = new System.Drawing.Point(4, 33);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Name = "tabPageSKOSQuoteLib_GetStockByNoLONG";
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Size = new System.Drawing.Size(1013, 220);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.TabIndex = 6;
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.Text = "海選資訊";
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.UseVisualStyleBackColor = true;
            // 
            // buttonSKOOQuoteLib_GetStockByNoLONG
            // 
            this.buttonSKOOQuoteLib_GetStockByNoLONG.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_GetStockByNoLONG.Location = new System.Drawing.Point(300, 10);
            this.buttonSKOOQuoteLib_GetStockByNoLONG.Name = "buttonSKOOQuoteLib_GetStockByNoLONG";
            this.buttonSKOOQuoteLib_GetStockByNoLONG.Size = new System.Drawing.Size(105, 30);
            this.buttonSKOOQuoteLib_GetStockByNoLONG.TabIndex = 79;
            this.buttonSKOOQuoteLib_GetStockByNoLONG.Text = "個選資訊";
            this.buttonSKOOQuoteLib_GetStockByNoLONG.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_GetStockByNoLONG.Click += new System.EventHandler(this.buttonSKOOQuoteLib_GetStockByNoLONG_Click);
            // 
            // dataGridViewSKOOQuoteLib_GetStockByNoLONG
            // 
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.AllowUserToAddRows = false;
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.AllowUserToDeleteRows = false;
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.AllowUserToOrderColumns = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.Location = new System.Drawing.Point(12, 44);
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.Name = "dataGridViewSKOOQuoteLib_GetStockByNoLONG";
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.RowTemplate.Height = 24;
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.Size = new System.Drawing.Size(525, 171);
            this.dataGridViewSKOOQuoteLib_GetStockByNoLONG.TabIndex = 140;
            // 
            // textBoxSKOOQuoteLib_GetStockByNoLONG
            // 
            this.textBoxSKOOQuoteLib_GetStockByNoLONG.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSKOOQuoteLib_GetStockByNoLONG.Location = new System.Drawing.Point(96, 10);
            this.textBoxSKOOQuoteLib_GetStockByNoLONG.Name = "textBoxSKOOQuoteLib_GetStockByNoLONG";
            this.textBoxSKOOQuoteLib_GetStockByNoLONG.Size = new System.Drawing.Size(198, 29);
            this.textBoxSKOOQuoteLib_GetStockByNoLONG.TabIndex = 139;
            this.textBoxSKOOQuoteLib_GetStockByNoLONG.Text = "CBOT,C00435Q4";
            // 
            // labelSKOOQuoteLib_GetStockByNoLONG
            // 
            this.labelSKOOQuoteLib_GetStockByNoLONG.AutoSize = true;
            this.labelSKOOQuoteLib_GetStockByNoLONG.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelSKOOQuoteLib_GetStockByNoLONG.Location = new System.Drawing.Point(8, 14);
            this.labelSKOOQuoteLib_GetStockByNoLONG.Name = "labelSKOOQuoteLib_GetStockByNoLONG";
            this.labelSKOOQuoteLib_GetStockByNoLONG.Size = new System.Drawing.Size(91, 24);
            this.labelSKOOQuoteLib_GetStockByNoLONG.TabIndex = 138;
            this.labelSKOOQuoteLib_GetStockByNoLONG.Text = "商品代碼:";
            // 
            // tabPageRequest
            // 
            this.tabPageRequest.AutoScroll = true;
            this.tabPageRequest.Controls.Add(this.textBoxpsPageNo2);
            this.tabPageRequest.Controls.Add(this.labelpsPageNo2);
            this.tabPageRequest.Controls.Add(this.buttonSKOOQuoteLib_RequestStocks);
            this.tabPageRequest.Controls.Add(this.textBoxStockNos);
            this.tabPageRequest.Controls.Add(this.labelRequestStocks);
            this.tabPageRequest.Controls.Add(this.dataGridViewStocks);
            this.tabPageRequest.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageRequest.Location = new System.Drawing.Point(4, 33);
            this.tabPageRequest.Name = "tabPageRequest";
            this.tabPageRequest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRequest.Size = new System.Drawing.Size(1013, 220);
            this.tabPageRequest.TabIndex = 5;
            this.tabPageRequest.Text = "即時報價";
            this.tabPageRequest.UseVisualStyleBackColor = true;
            // 
            // textBoxpsPageNo2
            // 
            this.textBoxpsPageNo2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxpsPageNo2.Location = new System.Drawing.Point(65, 9);
            this.textBoxpsPageNo2.Name = "textBoxpsPageNo2";
            this.textBoxpsPageNo2.Size = new System.Drawing.Size(26, 29);
            this.textBoxpsPageNo2.TabIndex = 43;
            this.textBoxpsPageNo2.Text = "1";
            // 
            // labelpsPageNo2
            // 
            this.labelpsPageNo2.AutoSize = true;
            this.labelpsPageNo2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelpsPageNo2.Location = new System.Drawing.Point(3, 12);
            this.labelpsPageNo2.Name = "labelpsPageNo2";
            this.labelpsPageNo2.Size = new System.Drawing.Size(56, 22);
            this.labelpsPageNo2.TabIndex = 42;
            this.labelpsPageNo2.Text = "Page:";
            // 
            // buttonSKOOQuoteLib_RequestStocks
            // 
            this.buttonSKOOQuoteLib_RequestStocks.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_RequestStocks.Location = new System.Drawing.Point(645, 11);
            this.buttonSKOOQuoteLib_RequestStocks.Name = "buttonSKOOQuoteLib_RequestStocks";
            this.buttonSKOOQuoteLib_RequestStocks.Size = new System.Drawing.Size(66, 30);
            this.buttonSKOOQuoteLib_RequestStocks.TabIndex = 33;
            this.buttonSKOOQuoteLib_RequestStocks.Text = "訂閱";
            this.buttonSKOOQuoteLib_RequestStocks.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_RequestStocks.Click += new System.EventHandler(this.buttonSKOOQuoteLib_RequestStocks_Click);
            // 
            // textBoxStockNos
            // 
            this.textBoxStockNos.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStockNos.Location = new System.Drawing.Point(400, 11);
            this.textBoxStockNos.Name = "textBoxStockNos";
            this.textBoxStockNos.Size = new System.Drawing.Size(239, 29);
            this.textBoxStockNos.TabIndex = 32;
            this.textBoxStockNos.Text = "CBOT,C00435Q4#NYM,NG00750Q4";
            // 
            // labelRequestStocks
            // 
            this.labelRequestStocks.AutoSize = true;
            this.labelRequestStocks.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelRequestStocks.Location = new System.Drawing.Point(92, 14);
            this.labelRequestStocks.Name = "labelRequestStocks";
            this.labelRequestStocks.Size = new System.Drawing.Size(313, 24);
            this.labelRequestStocks.TabIndex = 31;
            this.labelRequestStocks.Text = "請輸入商品代號(每檔以\",\"做區隔)：";
            // 
            // dataGridViewStocks
            // 
            this.dataGridViewStocks.AllowUserToAddRows = false;
            this.dataGridViewStocks.AllowUserToDeleteRows = false;
            this.dataGridViewStocks.AllowUserToOrderColumns = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewStocks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStocks.Location = new System.Drawing.Point(3, 43);
            this.dataGridViewStocks.Name = "dataGridViewStocks";
            this.dataGridViewStocks.RowTemplate.Height = 24;
            this.dataGridViewStocks.Size = new System.Drawing.Size(708, 172);
            this.dataGridViewStocks.TabIndex = 34;
            // 
            // tabPageTicks
            // 
            this.tabPageTicks.AutoScroll = true;
            this.tabPageTicks.Controls.Add(this.buttonSKOOQuoteLib_RequestMarketDepth);
            this.tabPageTicks.Controls.Add(this.dataGridViewOnNotifyBest10NineDigitLONG);
            this.tabPageTicks.Controls.Add(this.dataGridViewOnNotifyTicksLONG);
            this.tabPageTicks.Controls.Add(this.labelPage);
            this.tabPageTicks.Controls.Add(this.textBoxpsPageNo);
            this.tabPageTicks.Controls.Add(this.textBoxTicks);
            this.tabPageTicks.Controls.Add(this.labelTicks);
            this.tabPageTicks.Controls.Add(this.buttonSKOOQuoteLib_RequestTicks);
            this.tabPageTicks.Controls.Add(this.dataGridViewTicks);
            this.tabPageTicks.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageTicks.Location = new System.Drawing.Point(4, 33);
            this.tabPageTicks.Name = "tabPageTicks";
            this.tabPageTicks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTicks.Size = new System.Drawing.Size(1013, 220);
            this.tabPageTicks.TabIndex = 4;
            this.tabPageTicks.Text = "十檔&五檔&成交明細";
            this.tabPageTicks.UseVisualStyleBackColor = true;
            // 
            // buttonSKOOQuoteLib_RequestMarketDepth
            // 
            this.buttonSKOOQuoteLib_RequestMarketDepth.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_RequestMarketDepth.Location = new System.Drawing.Point(332, 48);
            this.buttonSKOOQuoteLib_RequestMarketDepth.Name = "buttonSKOOQuoteLib_RequestMarketDepth";
            this.buttonSKOOQuoteLib_RequestMarketDepth.Size = new System.Drawing.Size(185, 49);
            this.buttonSKOOQuoteLib_RequestMarketDepth.TabIndex = 123;
            this.buttonSKOOQuoteLib_RequestMarketDepth.Text = "訂閱(不含成交明細)";
            this.buttonSKOOQuoteLib_RequestMarketDepth.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_RequestMarketDepth.Click += new System.EventHandler(this.buttonSKOOQuoteLib_RequestMarketDepth_Click);
            // 
            // dataGridViewOnNotifyBest10NineDigitLONG
            // 
            this.dataGridViewOnNotifyBest10NineDigitLONG.AllowUserToAddRows = false;
            this.dataGridViewOnNotifyBest10NineDigitLONG.AllowUserToDeleteRows = false;
            this.dataGridViewOnNotifyBest10NineDigitLONG.AllowUserToOrderColumns = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOnNotifyBest10NineDigitLONG.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewOnNotifyBest10NineDigitLONG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOnNotifyBest10NineDigitLONG.Location = new System.Drawing.Point(523, 6);
            this.dataGridViewOnNotifyBest10NineDigitLONG.Name = "dataGridViewOnNotifyBest10NineDigitLONG";
            this.dataGridViewOnNotifyBest10NineDigitLONG.RowTemplate.Height = 24;
            this.dataGridViewOnNotifyBest10NineDigitLONG.Size = new System.Drawing.Size(487, 209);
            this.dataGridViewOnNotifyBest10NineDigitLONG.TabIndex = 122;
            // 
            // dataGridViewOnNotifyTicksLONG
            // 
            this.dataGridViewOnNotifyTicksLONG.AllowUserToAddRows = false;
            this.dataGridViewOnNotifyTicksLONG.AllowUserToDeleteRows = false;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOnNotifyTicksLONG.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewOnNotifyTicksLONG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOnNotifyTicksLONG.Location = new System.Drawing.Point(10, 60);
            this.dataGridViewOnNotifyTicksLONG.Name = "dataGridViewOnNotifyTicksLONG";
            this.dataGridViewOnNotifyTicksLONG.RowTemplate.Height = 24;
            this.dataGridViewOnNotifyTicksLONG.Size = new System.Drawing.Size(316, 66);
            this.dataGridViewOnNotifyTicksLONG.TabIndex = 121;
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPage.Location = new System.Drawing.Point(6, 17);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(56, 22);
            this.labelPage.TabIndex = 41;
            this.labelPage.Text = "Page:";
            // 
            // textBoxpsPageNo
            // 
            this.textBoxpsPageNo.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxpsPageNo.Location = new System.Drawing.Point(74, 13);
            this.textBoxpsPageNo.Name = "textBoxpsPageNo";
            this.textBoxpsPageNo.Size = new System.Drawing.Size(26, 29);
            this.textBoxpsPageNo.TabIndex = 40;
            this.textBoxpsPageNo.Text = "1";
            // 
            // textBoxTicks
            // 
            this.textBoxTicks.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTicks.Location = new System.Drawing.Point(304, 13);
            this.textBoxTicks.Name = "textBoxTicks";
            this.textBoxTicks.Size = new System.Drawing.Size(141, 29);
            this.textBoxTicks.TabIndex = 36;
            this.textBoxTicks.Text = "CBOT,C00435Q4";
            // 
            // labelTicks
            // 
            this.labelTicks.AutoSize = true;
            this.labelTicks.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelTicks.Location = new System.Drawing.Point(106, 17);
            this.labelTicks.Name = "labelTicks";
            this.labelTicks.Size = new System.Drawing.Size(192, 24);
            this.labelTicks.TabIndex = 35;
            this.labelTicks.Text = "輸入商品代碼(僅1檔):";
            // 
            // buttonSKOOQuoteLib_RequestTicks
            // 
            this.buttonSKOOQuoteLib_RequestTicks.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_RequestTicks.Location = new System.Drawing.Point(451, 11);
            this.buttonSKOOQuoteLib_RequestTicks.Name = "buttonSKOOQuoteLib_RequestTicks";
            this.buttonSKOOQuoteLib_RequestTicks.Size = new System.Drawing.Size(66, 30);
            this.buttonSKOOQuoteLib_RequestTicks.TabIndex = 37;
            this.buttonSKOOQuoteLib_RequestTicks.Text = "訂閱";
            this.buttonSKOOQuoteLib_RequestTicks.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_RequestTicks.Click += new System.EventHandler(this.buttonSKOOQuoteLib_RequestTicks_Click);
            // 
            // dataGridViewTicks
            // 
            this.dataGridViewTicks.AllowUserToAddRows = false;
            this.dataGridViewTicks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTicks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTicks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTicks.Location = new System.Drawing.Point(7, 132);
            this.dataGridViewTicks.Name = "dataGridViewTicks";
            this.dataGridViewTicks.RowTemplate.Height = 24;
            this.dataGridViewTicks.Size = new System.Drawing.Size(510, 83);
            this.dataGridViewTicks.TabIndex = 38;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonSKOOQuoteLib_LeaveMonitor);
            this.panel1.Controls.Add(this.richTextBoxMessage);
            this.panel1.Controls.Add(this.buttonSKOOQuoteLib_EnterMonitorLONG);
            this.panel1.Controls.Add(this.labelUserID);
            this.panel1.Controls.Add(this.buttonSKOOQuoteLib_IsConnected);
            this.panel1.Controls.Add(this.richTextBoxMethodMessage);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 253);
            this.panel1.TabIndex = 76;
            // 
            // buttonSKOOQuoteLib_LeaveMonitor
            // 
            this.buttonSKOOQuoteLib_LeaveMonitor.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_LeaveMonitor.Location = new System.Drawing.Point(3, 115);
            this.buttonSKOOQuoteLib_LeaveMonitor.Name = "buttonSKOOQuoteLib_LeaveMonitor";
            this.buttonSKOOQuoteLib_LeaveMonitor.Size = new System.Drawing.Size(187, 30);
            this.buttonSKOOQuoteLib_LeaveMonitor.TabIndex = 55;
            this.buttonSKOOQuoteLib_LeaveMonitor.Text = "斷線報價主機";
            this.buttonSKOOQuoteLib_LeaveMonitor.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_LeaveMonitor.Click += new System.EventHandler(this.buttonSKOOQuoteLib_LeaveMonitor_Click);
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Location = new System.Drawing.Point(2, 187);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(327, 52);
            this.richTextBoxMessage.TabIndex = 54;
            this.richTextBoxMessage.Text = "";
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserID.Location = new System.Drawing.Point(3, 54);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(105, 22);
            this.labelUserID.TabIndex = 53;
            this.labelUserID.Text = "UserIDxxxx";
            // 
            // buttonSKOOQuoteLib_IsConnected
            // 
            this.buttonSKOOQuoteLib_IsConnected.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonSKOOQuoteLib_IsConnected.Location = new System.Drawing.Point(3, 151);
            this.buttonSKOOQuoteLib_IsConnected.Name = "buttonSKOOQuoteLib_IsConnected";
            this.buttonSKOOQuoteLib_IsConnected.Size = new System.Drawing.Size(187, 30);
            this.buttonSKOOQuoteLib_IsConnected.TabIndex = 47;
            this.buttonSKOOQuoteLib_IsConnected.Text = "檢查連線狀態";
            this.buttonSKOOQuoteLib_IsConnected.UseVisualStyleBackColor = true;
            this.buttonSKOOQuoteLib_IsConnected.Click += new System.EventHandler(this.buttonSKOOQuoteLib_IsConnected_Click);
            // 
            // OOQuoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 262);
            this.Controls.Add(this.panelOOQuoteForm);
            this.Name = "OOQuoteForm";
            this.Text = "OOQuoteForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OOQuoteForm_FormClosing);
            this.Load += new System.EventHandler(this.OOQuoteForm_Load);
            this.panelOOQuoteForm.ResumeLayout(false);
            this.tabControlQuote.ResumeLayout(false);
            this.tabPageOverseaOptionList.ResumeLayout(false);
            this.tabPageOverseaOptionList.PerformLayout();
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.ResumeLayout(false);
            this.tabPageSKOSQuoteLib_GetStockByNoLONG.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSKOOQuoteLib_GetStockByNoLONG)).EndInit();
            this.tabPageRequest.ResumeLayout(false);
            this.tabPageRequest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStocks)).EndInit();
            this.tabPageTicks.ResumeLayout(false);
            this.tabPageTicks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnNotifyBest10NineDigitLONG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOnNotifyTicksLONG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicks)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSKOOQuoteLib_EnterMonitorLONG;
        private System.Windows.Forms.Button buttonSKOOQuoteLib_RequestProducts;
        private System.Windows.Forms.RichTextBox richTextBoxMethodMessage;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.Button buttonSearchKeyWord;
        private System.Windows.Forms.RichTextBox richTextBoxOnProducts;
        private System.Windows.Forms.Panel panelOOQuoteForm;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Button buttonSKOOQuoteLib_LeaveMonitor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSKOOQuoteLib_IsConnected;
        private System.Windows.Forms.TabControl tabControlQuote;
        private System.Windows.Forms.TabPage tabPageOverseaOptionList;
        private System.Windows.Forms.TabPage tabPageSKOSQuoteLib_GetStockByNoLONG;
        private System.Windows.Forms.Button buttonSKOOQuoteLib_GetStockByNoLONG;
        private System.Windows.Forms.DataGridView dataGridViewSKOOQuoteLib_GetStockByNoLONG;
        private System.Windows.Forms.TextBox textBoxSKOOQuoteLib_GetStockByNoLONG;
        private System.Windows.Forms.Label labelSKOOQuoteLib_GetStockByNoLONG;
        private System.Windows.Forms.TabPage tabPageRequest;
        private System.Windows.Forms.TextBox textBoxpsPageNo2;
        private System.Windows.Forms.Label labelpsPageNo2;
        private System.Windows.Forms.Button buttonSKOOQuoteLib_RequestStocks;
        private System.Windows.Forms.TextBox textBoxStockNos;
        private System.Windows.Forms.Label labelRequestStocks;
        private System.Windows.Forms.DataGridView dataGridViewStocks;
        private System.Windows.Forms.TabPage tabPageTicks;
        private System.Windows.Forms.Button buttonSKOOQuoteLib_RequestMarketDepth;
        private System.Windows.Forms.DataGridView dataGridViewOnNotifyBest10NineDigitLONG;
        private System.Windows.Forms.DataGridView dataGridViewOnNotifyTicksLONG;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.TextBox textBoxpsPageNo;
        private System.Windows.Forms.TextBox textBoxTicks;
        private System.Windows.Forms.Label labelTicks;
        private System.Windows.Forms.Button buttonSKOOQuoteLib_RequestTicks;
        private System.Windows.Forms.DataGridView dataGridViewTicks;
    }
}