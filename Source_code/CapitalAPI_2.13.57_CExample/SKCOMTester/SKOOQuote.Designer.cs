namespace SKCOMTester
{
    partial class SKOOQuote
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSignal = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOOQuotePage = new System.Windows.Forms.TextBox();
            this.gridStocks = new System.Windows.Forms.DataGridView();
            this.btnQueryStocks = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStocks = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnMarketDepth = new System.Windows.Forms.Button();
            this.btnLiveTick = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gridBest10Bid = new System.Windows.Forms.DataGridView();
            this.gridBest10Ask = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblGetTick = new System.Windows.Forms.Label();
            this.btnGetTick = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTickPtr = new System.Windows.Forms.TextBox();
            this.txtTickStockidx = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblBest5Ask = new System.Windows.Forms.Label();
            this.lblBest5Bid = new System.Windows.Forms.Label();
            this.btnGetBest5 = new System.Windows.Forms.Button();
            this.txtBestStockidx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOOTickPage = new System.Windows.Forms.TextBox();
            this.gridBest5Bid = new System.Windows.Forms.DataGridView();
            this.gridBest5Ask = new System.Windows.Forms.DataGridView();
            this.listTicks = new System.Windows.Forms.ListBox();
            this.btnTicks = new System.Windows.Forms.Button();
            this.txtTick = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.listProducts = new System.Windows.Forms.ListBox();
            this.btnProducts = new System.Windows.Forms.Button();
            this.ConnectedLabel = new System.Windows.Forms.Label();
            this.btnIsConnected = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStocks)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest10Bid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest10Ask)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest5Bid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest5Ask)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSignal);
            this.groupBox1.Location = new System.Drawing.Point(540, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(65, 54);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // lblSignal
            // 
            this.lblSignal.AutoSize = true;
            this.lblSignal.Font = new System.Drawing.Font("新細明體", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignal.ForeColor = System.Drawing.Color.Red;
            this.lblSignal.Location = new System.Drawing.Point(15, 21);
            this.lblSignal.Name = "lblSignal";
            this.lblSignal.Size = new System.Drawing.Size(32, 22);
            this.lblSignal.TabIndex = 0;
            this.lblSignal.Text = "●";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(291, 24);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(70, 32);
            this.btnDisconnect.TabIndex = 37;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 32);
            this.button1.TabIndex = 36;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(30, 72);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(862, 455);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtOOQuotePage);
            this.tabPage1.Controls.Add(this.gridStocks);
            this.tabPage1.Controls.Add(this.btnQueryStocks);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtStocks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(854, 429);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "報價";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "Page";
            // 
            // txtOOQuotePage
            // 
            this.txtOOQuotePage.Location = new System.Drawing.Point(40, 29);
            this.txtOOQuotePage.Name = "txtOOQuotePage";
            this.txtOOQuotePage.Size = new System.Drawing.Size(46, 22);
            this.txtOOQuotePage.TabIndex = 17;
            this.txtOOQuotePage.Text = "-1";
            // 
            // gridStocks
            // 
            this.gridStocks.AllowUserToAddRows = false;
            this.gridStocks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridStocks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridStocks.Location = new System.Drawing.Point(6, 57);
            this.gridStocks.Name = "gridStocks";
            this.gridStocks.ReadOnly = true;
            this.gridStocks.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gridStocks.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridStocks.RowTemplate.Height = 24;
            this.gridStocks.Size = new System.Drawing.Size(841, 214);
            this.gridStocks.TabIndex = 16;
            this.gridStocks.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridStocks_CellPainting);
            // 
            // btnQueryStocks
            // 
            this.btnQueryStocks.Location = new System.Drawing.Point(545, 16);
            this.btnQueryStocks.Name = "btnQueryStocks";
            this.btnQueryStocks.Size = new System.Drawing.Size(133, 23);
            this.btnQueryStocks.TabIndex = 15;
            this.btnQueryStocks.Text = "查詢";
            this.btnQueryStocks.UseVisualStyleBackColor = true;
            this.btnQueryStocks.Click += new System.EventHandler(this.btnQueryStocks_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(543, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(289, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "格式 [ 交易代碼,商品報價代碼 ] ( 多筆以 井號{#}區隔 )";
            // 
            // txtStocks
            // 
            this.txtStocks.Location = new System.Drawing.Point(92, 29);
            this.txtStocks.Name = "txtStocks";
            this.txtStocks.Size = new System.Drawing.Size(445, 22);
            this.txtStocks.TabIndex = 13;
            this.txtStocks.Text = "CBOT,US00930I3";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnMarketDepth);
            this.tabPage2.Controls.Add(this.btnLiveTick);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.gridBest10Bid);
            this.tabPage2.Controls.Add(this.gridBest10Ask);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtOOTickPage);
            this.tabPage2.Controls.Add(this.gridBest5Bid);
            this.tabPage2.Controls.Add(this.gridBest5Ask);
            this.tabPage2.Controls.Add(this.listTicks);
            this.tabPage2.Controls.Add(this.btnTicks);
            this.tabPage2.Controls.Add(this.txtTick);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(854, 429);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ticks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnMarketDepth
            // 
            this.btnMarketDepth.Location = new System.Drawing.Point(441, 15);
            this.btnMarketDepth.Name = "btnMarketDepth";
            this.btnMarketDepth.Size = new System.Drawing.Size(93, 33);
            this.btnMarketDepth.TabIndex = 28;
            this.btnMarketDepth.Text = "Request MarketDepth";
            this.btnMarketDepth.UseVisualStyleBackColor = true;
            this.btnMarketDepth.Click += new System.EventHandler(this.btnMarketDepth_Click);
            // 
            // btnLiveTick
            // 
            this.btnLiveTick.Location = new System.Drawing.Point(333, 15);
            this.btnLiveTick.Name = "btnLiveTick";
            this.btnLiveTick.Size = new System.Drawing.Size(102, 33);
            this.btnLiveTick.TabIndex = 4;
            this.btnLiveTick.Text = "Request LiveTick";
            this.btnLiveTick.UseVisualStyleBackColor = true;
            this.btnLiveTick.Click += new System.EventHandler(this.btnLiveTick_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(333, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "十檔";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "五檔";
            // 
            // gridBest10Bid
            // 
            this.gridBest10Bid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBest10Bid.Location = new System.Drawing.Point(468, 63);
            this.gridBest10Bid.MultiSelect = false;
            this.gridBest10Bid.Name = "gridBest10Bid";
            this.gridBest10Bid.ReadOnly = true;
            this.gridBest10Bid.RowHeadersVisible = false;
            this.gridBest10Bid.RowTemplate.Height = 24;
            this.gridBest10Bid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridBest10Bid.Size = new System.Drawing.Size(131, 300);
            this.gridBest10Bid.TabIndex = 23;
            // 
            // gridBest10Ask
            // 
            this.gridBest10Ask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBest10Ask.Location = new System.Drawing.Point(331, 63);
            this.gridBest10Ask.MultiSelect = false;
            this.gridBest10Ask.Name = "gridBest10Ask";
            this.gridBest10Ask.ReadOnly = true;
            this.gridBest10Ask.RowHeadersVisible = false;
            this.gridBest10Ask.RowTemplate.Height = 24;
            this.gridBest10Ask.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.gridBest10Ask.Size = new System.Drawing.Size(131, 300);
            this.gridBest10Ask.TabIndex = 22;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblGetTick);
            this.groupBox4.Controls.Add(this.btnGetTick);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtTickPtr);
            this.groupBox4.Controls.Add(this.txtTickStockidx);
            this.groupBox4.Location = new System.Drawing.Point(605, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(236, 115);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "GetTick";
            // 
            // lblGetTick
            // 
            this.lblGetTick.AutoSize = true;
            this.lblGetTick.Location = new System.Drawing.Point(15, 89);
            this.lblGetTick.Name = "lblGetTick";
            this.lblGetTick.Size = new System.Drawing.Size(33, 12);
            this.lblGetTick.TabIndex = 4;
            this.lblGetTick.Text = "label4";
            // 
            // btnGetTick
            // 
            this.btnGetTick.Location = new System.Drawing.Point(6, 50);
            this.btnGetTick.Name = "btnGetTick";
            this.btnGetTick.Size = new System.Drawing.Size(75, 23);
            this.btnGetTick.TabIndex = 3;
            this.btnGetTick.Text = "GetTick";
            this.btnGetTick.UseVisualStyleBackColor = true;
            this.btnGetTick.Click += new System.EventHandler(this.btnGetTick_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Stockidx/nPtr";
            // 
            // txtTickPtr
            // 
            this.txtTickPtr.Location = new System.Drawing.Point(162, 14);
            this.txtTickPtr.Name = "txtTickPtr";
            this.txtTickPtr.Size = new System.Drawing.Size(68, 22);
            this.txtTickPtr.TabIndex = 1;
            // 
            // txtTickStockidx
            // 
            this.txtTickStockidx.Location = new System.Drawing.Point(79, 14);
            this.txtTickStockidx.Name = "txtTickStockidx";
            this.txtTickStockidx.Size = new System.Drawing.Size(67, 22);
            this.txtTickStockidx.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblBest5Ask);
            this.groupBox5.Controls.Add(this.lblBest5Bid);
            this.groupBox5.Controls.Add(this.btnGetBest5);
            this.groupBox5.Controls.Add(this.txtBestStockidx);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(605, 187);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(238, 119);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "GetBest5";
            // 
            // lblBest5Ask
            // 
            this.lblBest5Ask.AutoSize = true;
            this.lblBest5Ask.Location = new System.Drawing.Point(15, 91);
            this.lblBest5Ask.Name = "lblBest5Ask";
            this.lblBest5Ask.Size = new System.Drawing.Size(33, 12);
            this.lblBest5Ask.TabIndex = 7;
            this.lblBest5Ask.Text = "label5";
            // 
            // lblBest5Bid
            // 
            this.lblBest5Bid.AutoSize = true;
            this.lblBest5Bid.Location = new System.Drawing.Point(15, 66);
            this.lblBest5Bid.Name = "lblBest5Bid";
            this.lblBest5Bid.Size = new System.Drawing.Size(33, 12);
            this.lblBest5Bid.TabIndex = 6;
            this.lblBest5Bid.Text = "label5";
            // 
            // btnGetBest5
            // 
            this.btnGetBest5.Location = new System.Drawing.Point(155, 17);
            this.btnGetBest5.Name = "btnGetBest5";
            this.btnGetBest5.Size = new System.Drawing.Size(75, 23);
            this.btnGetBest5.TabIndex = 5;
            this.btnGetBest5.Text = "GetBest5";
            this.btnGetBest5.UseVisualStyleBackColor = true;
            this.btnGetBest5.Click += new System.EventHandler(this.btnGetBest5_Click);
            // 
            // txtBestStockidx
            // 
            this.txtBestStockidx.Location = new System.Drawing.Point(67, 18);
            this.txtBestStockidx.Name = "txtBestStockidx";
            this.txtBestStockidx.Size = new System.Drawing.Size(67, 22);
            this.txtBestStockidx.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Stockidx";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "Page";
            // 
            // txtOOTickPage
            // 
            this.txtOOTickPage.Location = new System.Drawing.Point(39, 22);
            this.txtOOTickPage.Name = "txtOOTickPage";
            this.txtOOTickPage.Size = new System.Drawing.Size(49, 22);
            this.txtOOTickPage.TabIndex = 15;
            this.txtOOTickPage.Text = "-1";
            // 
            // gridBest5Bid
            // 
            this.gridBest5Bid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBest5Bid.Location = new System.Drawing.Point(178, 242);
            this.gridBest5Bid.MultiSelect = false;
            this.gridBest5Bid.Name = "gridBest5Bid";
            this.gridBest5Bid.ReadOnly = true;
            this.gridBest5Bid.RowHeadersVisible = false;
            this.gridBest5Bid.RowTemplate.Height = 24;
            this.gridBest5Bid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridBest5Bid.Size = new System.Drawing.Size(131, 172);
            this.gridBest5Bid.TabIndex = 14;
            // 
            // gridBest5Ask
            // 
            this.gridBest5Ask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBest5Ask.Location = new System.Drawing.Point(41, 242);
            this.gridBest5Ask.MultiSelect = false;
            this.gridBest5Ask.Name = "gridBest5Ask";
            this.gridBest5Ask.ReadOnly = true;
            this.gridBest5Ask.RowHeadersVisible = false;
            this.gridBest5Ask.RowTemplate.Height = 24;
            this.gridBest5Ask.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridBest5Ask.Size = new System.Drawing.Size(131, 172);
            this.gridBest5Ask.TabIndex = 13;
            // 
            // listTicks
            // 
            this.listTicks.Font = new System.Drawing.Font("新細明體", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listTicks.FormattingEnabled = true;
            this.listTicks.HorizontalScrollbar = true;
            this.listTicks.ItemHeight = 17;
            this.listTicks.Location = new System.Drawing.Point(6, 63);
            this.listTicks.Name = "listTicks";
            this.listTicks.Size = new System.Drawing.Size(319, 174);
            this.listTicks.TabIndex = 12;
            // 
            // btnTicks
            // 
            this.btnTicks.Location = new System.Drawing.Point(233, 15);
            this.btnTicks.Name = "btnTicks";
            this.btnTicks.Size = new System.Drawing.Size(94, 33);
            this.btnTicks.TabIndex = 3;
            this.btnTicks.Text = "Request Tick";
            this.btnTicks.UseVisualStyleBackColor = true;
            this.btnTicks.Click += new System.EventHandler(this.btnTicks_Click);
            // 
            // txtTick
            // 
            this.txtTick.Location = new System.Drawing.Point(114, 22);
            this.txtTick.Name = "txtTick";
            this.txtTick.Size = new System.Drawing.Size(113, 22);
            this.txtTick.TabIndex = 2;
            this.txtTick.Text = "NYM,GC01910I3";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.listProducts);
            this.tabPage3.Controls.Add(this.btnProducts);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(854, 429);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "商品";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(523, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "0";
            // 
            // listProducts
            // 
            this.listProducts.Font = new System.Drawing.Font("新細明體", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listProducts.FormattingEnabled = true;
            this.listProducts.ItemHeight = 17;
            this.listProducts.Location = new System.Drawing.Point(23, 88);
            this.listProducts.Name = "listProducts";
            this.listProducts.Size = new System.Drawing.Size(717, 174);
            this.listProducts.TabIndex = 5;
            // 
            // btnProducts
            // 
            this.btnProducts.Location = new System.Drawing.Point(252, 24);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(202, 43);
            this.btnProducts.TabIndex = 4;
            this.btnProducts.Text = "GetOverseaProducts";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // ConnectedLabel
            // 
            this.ConnectedLabel.AutoSize = true;
            this.ConnectedLabel.Location = new System.Drawing.Point(755, 44);
            this.ConnectedLabel.Name = "ConnectedLabel";
            this.ConnectedLabel.Size = new System.Drawing.Size(11, 12);
            this.ConnectedLabel.TabIndex = 45;
            this.ConnectedLabel.Text = "0";
            // 
            // btnIsConnected
            // 
            this.btnIsConnected.Location = new System.Drawing.Point(653, 33);
            this.btnIsConnected.Name = "btnIsConnected";
            this.btnIsConnected.Size = new System.Drawing.Size(82, 32);
            this.btnIsConnected.TabIndex = 44;
            this.btnIsConnected.Text = "IsConnected";
            this.btnIsConnected.UseVisualStyleBackColor = true;
            this.btnIsConnected.Click += new System.EventHandler(this.btnIsConnected_Click);
            // 
            // SKOOQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ConnectedLabel);
            this.Controls.Add(this.btnIsConnected);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.button1);
            this.Name = "SKOOQuote";
            this.Size = new System.Drawing.Size(925, 539);
            this.Load += new System.EventHandler(this.SKOOQuote_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStocks)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest10Bid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest10Ask)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest5Bid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBest5Ask)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSignal;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listProducts;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.DataGridView gridStocks;
        private System.Windows.Forms.Button btnQueryStocks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStocks;
        private System.Windows.Forms.TextBox txtOOQuotePage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTicks;
        private System.Windows.Forms.TextBox txtTick;
        private System.Windows.Forms.DataGridView gridBest5Bid;
        private System.Windows.Forms.DataGridView gridBest5Ask;
        private System.Windows.Forms.ListBox listTicks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOOTickPage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblGetTick;
        private System.Windows.Forms.Button btnGetTick;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTickPtr;
        private System.Windows.Forms.TextBox txtTickStockidx;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblBest5Ask;
        private System.Windows.Forms.Label lblBest5Bid;
        private System.Windows.Forms.Button btnGetBest5;
        private System.Windows.Forms.TextBox txtBestStockidx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView gridBest10Bid;
        private System.Windows.Forms.DataGridView gridBest10Ask;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ConnectedLabel;
        private System.Windows.Forms.Button btnIsConnected;
        private System.Windows.Forms.Button btnLiveTick;
        private System.Windows.Forms.Button btnMarketDepth;
        private System.Windows.Forms.Label label8;
    }
}
