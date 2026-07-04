#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace CppCLITester {

    /// <summary>
    /// SKOSQuote 的摘要
    /// </summary>
    public ref class SKOSQuote : public System::Windows::Forms::UserControl
    {


#pragma region 變數

    public:
        delegate void GetMessageHandler(System::String^ strType, int nCode, System::String^ strMessage);
        event GetMessageHandler^ GetMessage;

        delegate void WriteMessageHandler(System::String^ strMessage);
        event WriteMessageHandler^ WriteMessage;

        void get_SKOSQuoteObj(SKCOMLib::SKOSQuoteLibClass^ value)
        {
            m_pSKOSQuote = value;
        }

    private:
        SKCOMLib::SKOSQuoteLibClass^ m_pSKOSQuote;
        bool m_bfirst = true;
        DataTable^ m_dtForeigns;
        DataTable^ m_dtBest5Ask;
        DataTable^ m_dtBest5Bid;
        DataTable^ m_dtBest10Ask;
   
           DataTable^ m_dtBest10Bid;
#pragma endregion

#pragma region methods
           //methods
           Void btnInitialize_Click(System::Object^ sender, System::EventArgs^ e);
           Void button1_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnDisconnect_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnIsConnected_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnQueryStocks_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnOverseaProducts_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnOverseaProducts2_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnTicks_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnLiveTick_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnMarketDepth_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnGetTick_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnGetBest5_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnKLine_Click(System::Object^ sender, System::EventArgs^ e);
           Void btnKLineByDate_Click(System::Object^ sender, System::EventArgs^ e);

           //event
           Void OnConnect(int nKind, int nCode);
           Void OnOverSeaProducts(System::String^ strValue);
           Void OnOverSeaProductsDetail(System::String^ strValue);
           //Void OnNotifyTicks(short sStockidx, int nPtr, int nDate, int nTime, int nClose, int nQty);
           Void OnQuoteUpdate(int nStockidx);
           Void OnUpDateDataQuote(SKCOMLib::SKFOREIGN_9LONG pForeign);
           //Void OnNotifyHistoryTicks(short sStockidx, int nPtr, int nDate, int nTime, int nClose, int nQty);
           Void OnNotifyBest5LONG(int nStockidx, Int64 nBestBid1, int nBestBidQty1, Int64 nBestBid2, int nBestBidQty2, Int64 nBestBid3, int nBestBidQty3, Int64 nBestBid4, int nBestBidQty4, Int64 nBestBid5, int nBestBidQty5,
               Int64 nBestAsk1, int nBestAskQty1, Int64 nBestAsk2, int nBestAskQty2, Int64 nBestAsk3, int nBestAskQty3, Int64 nBestAsk4, int nBestAskQty4, Int64 nBestAsk5, int nBestAskQty5);
           Void OnNotifyBest10LONG(int nStockIdx, Int64 nBestBid1, int nBestBidQty1, Int64 nBestBid2, int nBestBidQty2, Int64 nBestBid3, int nBestBidQty3,
               Int64 nBestBid4, int nBestBidQty4, Int64 nBestBid5, int nBestBidQty5, Int64 nBestBid6, int nBestBidQty6, Int64 nBestBid7, int nBestBidQty7,
               Int64 nBestBid8, int nBestBidQty8, Int64 nBestBid9, int nBestBidQty9, Int64 nBestBid10, int nBestBidQty10, Int64 nBestAsk1, int nBestAskQty1,
               Int64 nBestAsk2, int nBestAskQty2, Int64 nBestAsk3, int nBestAskQty3, Int64 nBestAsk4, int nBestAskQty4, Int64 nBestAsk5, int nBestAskQty5,
               Int64 nBestAsk6, int nBestAskQty6, Int64 nBestAsk7, int nBestAskQty7, Int64 nBestAsk8, int nBestAskQty8, Int64 nBestAsk9, int nBestAskQty9,
               Int64 nBestAsk10, int nBestAskQty10);
           Void OnNotifyTicksNineLONG(int nStockidx, int nPtr, int nDate, int nTime, Int64 nClose, int nQty);
           Void OnNotifyHistoryTicksNineLONG(int nStockidx, int nPtr, int nDate, int nTime, Int64 nClose, int nQty);
           Void OnKLineData(System::String^ strStockNo, System::String^ strData);

           //custom
           DataTable^ CreateStocksDataTable();
           DataTable^ CreateBest5AskTable();
           
#pragma endregion
  
    public:
        SKOSQuote(void)
        {
            m_dtForeigns = CreateStocksDataTable();
            m_dtBest5Ask = CreateBest5AskTable();
            m_dtBest5Bid = CreateBest5AskTable();
            m_dtBest10Ask = CreateBest5AskTable();
            m_dtBest10Bid = CreateBest5AskTable();

            InitializeComponent();
            //
            //TODO:  在此加入建構函式程式碼
            //
        }


    protected:
        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        ~SKOSQuote()
        {
            if (components)
            {
                delete components;
            }
        }
    private: System::Windows::Forms::Button^ btnIsConnected;
    protected:
    private: System::Windows::Forms::Button^ btnInitialize;
    private: System::Windows::Forms::Button^ btnDisconnect;
    private: System::Windows::Forms::Button^ button1;
    private: System::Windows::Forms::GroupBox^ groupBox1;
    private: System::Windows::Forms::Label^ lblSignal;

    private: System::Windows::Forms::Label^ ConnectedLabel;
    private: System::Windows::Forms::TabControl^ tabControl1;
    private: System::Windows::Forms::TabPage^ tabPage1;
    private: System::Windows::Forms::TextBox^ txtPageNo;
    private: System::Windows::Forms::Label^ label6;
    private: System::Windows::Forms::Label^ lblPage;
    private: System::Windows::Forms::DataGridView^ gridStocks;
    private: System::Windows::Forms::Button^ btnQueryStocks;
    private: System::Windows::Forms::Label^ label2;
    private: System::Windows::Forms::TextBox^ txtStocks;
    private: System::Windows::Forms::TabPage^ tabPage2;
    private: System::Windows::Forms::Button^ btnMarketDepth;
    private: System::Windows::Forms::Label^ label7;
    private: System::Windows::Forms::TextBox^ txtOSTickPage;
    private: System::Windows::Forms::Button^ btnLiveTick;
    private: System::Windows::Forms::Label^ label5;
    private: System::Windows::Forms::Label^ label3;
    private: System::Windows::Forms::DataGridView^ gridBest10Bid;
    private: System::Windows::Forms::DataGridView^ gridBest10Ask;
    private: System::Windows::Forms::GroupBox^ groupBox5;
    private: System::Windows::Forms::Label^ lblBest5Ask;
    private: System::Windows::Forms::Label^ lblBest5Bid;
    private: System::Windows::Forms::Button^ btnGetBest5;
    private: System::Windows::Forms::TextBox^ txtBestStockidx;
    private: System::Windows::Forms::Label^ label4;
    private: System::Windows::Forms::GroupBox^ groupBox4;
    private: System::Windows::Forms::Label^ lblGetTick;
    private: System::Windows::Forms::Button^ btnGetTick;
    private: System::Windows::Forms::Label^ label8;
    private: System::Windows::Forms::TextBox^ txtTickPtr;
    private: System::Windows::Forms::TextBox^ txtTickStockidx;
    private: System::Windows::Forms::DataGridView^ gridBest5Bid;
    private: System::Windows::Forms::DataGridView^ gridBest5Ask;
    private: System::Windows::Forms::ListBox^ listTicks;
    private: System::Windows::Forms::Button^ btnTicks;
    private: System::Windows::Forms::TextBox^ txtTick;
    private: System::Windows::Forms::TabPage^ tabPage3;
    private: System::Windows::Forms::Button^ btnOverseaProducts2;
    private: System::Windows::Forms::ListBox^ listOverseaProducts;
    private: System::Windows::Forms::Button^ btnOverseaProducts;
    private: System::Windows::Forms::TabPage^ tabPage4;
    private: System::Windows::Forms::Button^ btnKLineByDate;
    private: System::Windows::Forms::TextBox^ txtEndDate;
    private: System::Windows::Forms::TextBox^ txtStartDate;
    private: System::Windows::Forms::Label^ label9;
    private: System::Windows::Forms::Label^ label10;
    private: System::Windows::Forms::ComboBox^ boxKLineType;
    private: System::Windows::Forms::ListBox^ listKLine;
    private: System::Windows::Forms::Button^ btnKLine;
    private: System::Windows::Forms::TextBox^ txtKLine;
    private: System::Windows::Forms::TextBox^ txtMinuteNumber;
    private: System::Windows::Forms::Label^ label1;
    private:
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        void InitializeComponent(void)
        {
            System::Windows::Forms::DataGridViewCellStyle^ dataGridViewCellStyle5 = (gcnew System::Windows::Forms::DataGridViewCellStyle());
            System::Windows::Forms::DataGridViewCellStyle^ dataGridViewCellStyle6 = (gcnew System::Windows::Forms::DataGridViewCellStyle());
            this->btnIsConnected = (gcnew System::Windows::Forms::Button());
            this->btnInitialize = (gcnew System::Windows::Forms::Button());
            this->btnDisconnect = (gcnew System::Windows::Forms::Button());
            this->button1 = (gcnew System::Windows::Forms::Button());
            this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
            this->lblSignal = (gcnew System::Windows::Forms::Label());
            this->ConnectedLabel = (gcnew System::Windows::Forms::Label());
            this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
            this->tabPage1 = (gcnew System::Windows::Forms::TabPage());
            this->txtPageNo = (gcnew System::Windows::Forms::TextBox());
            this->label6 = (gcnew System::Windows::Forms::Label());
            this->lblPage = (gcnew System::Windows::Forms::Label());
            this->gridStocks = (gcnew System::Windows::Forms::DataGridView());
            this->btnQueryStocks = (gcnew System::Windows::Forms::Button());
            this->label2 = (gcnew System::Windows::Forms::Label());
            this->txtStocks = (gcnew System::Windows::Forms::TextBox());
            this->tabPage2 = (gcnew System::Windows::Forms::TabPage());
            this->btnMarketDepth = (gcnew System::Windows::Forms::Button());
            this->label7 = (gcnew System::Windows::Forms::Label());
            this->txtOSTickPage = (gcnew System::Windows::Forms::TextBox());
            this->btnLiveTick = (gcnew System::Windows::Forms::Button());
            this->label5 = (gcnew System::Windows::Forms::Label());
            this->label3 = (gcnew System::Windows::Forms::Label());
            this->gridBest10Bid = (gcnew System::Windows::Forms::DataGridView());
            this->gridBest10Ask = (gcnew System::Windows::Forms::DataGridView());
            this->groupBox5 = (gcnew System::Windows::Forms::GroupBox());
            this->lblBest5Ask = (gcnew System::Windows::Forms::Label());
            this->lblBest5Bid = (gcnew System::Windows::Forms::Label());
            this->btnGetBest5 = (gcnew System::Windows::Forms::Button());
            this->txtBestStockidx = (gcnew System::Windows::Forms::TextBox());
            this->label4 = (gcnew System::Windows::Forms::Label());
            this->groupBox4 = (gcnew System::Windows::Forms::GroupBox());
            this->lblGetTick = (gcnew System::Windows::Forms::Label());
            this->btnGetTick = (gcnew System::Windows::Forms::Button());
            this->label8 = (gcnew System::Windows::Forms::Label());
            this->txtTickPtr = (gcnew System::Windows::Forms::TextBox());
            this->txtTickStockidx = (gcnew System::Windows::Forms::TextBox());
            this->gridBest5Bid = (gcnew System::Windows::Forms::DataGridView());
            this->gridBest5Ask = (gcnew System::Windows::Forms::DataGridView());
            this->listTicks = (gcnew System::Windows::Forms::ListBox());
            this->btnTicks = (gcnew System::Windows::Forms::Button());
            this->txtTick = (gcnew System::Windows::Forms::TextBox());
            this->tabPage3 = (gcnew System::Windows::Forms::TabPage());
            this->btnOverseaProducts2 = (gcnew System::Windows::Forms::Button());
            this->listOverseaProducts = (gcnew System::Windows::Forms::ListBox());
            this->btnOverseaProducts = (gcnew System::Windows::Forms::Button());
            this->tabPage4 = (gcnew System::Windows::Forms::TabPage());
            this->btnKLineByDate = (gcnew System::Windows::Forms::Button());
            this->txtEndDate = (gcnew System::Windows::Forms::TextBox());
            this->txtStartDate = (gcnew System::Windows::Forms::TextBox());
            this->label9 = (gcnew System::Windows::Forms::Label());
            this->label10 = (gcnew System::Windows::Forms::Label());
            this->boxKLineType = (gcnew System::Windows::Forms::ComboBox());
            this->listKLine = (gcnew System::Windows::Forms::ListBox());
            this->btnKLine = (gcnew System::Windows::Forms::Button());
            this->txtKLine = (gcnew System::Windows::Forms::TextBox());
            this->txtMinuteNumber = (gcnew System::Windows::Forms::TextBox());
            this->label1 = (gcnew System::Windows::Forms::Label());
            this->groupBox1->SuspendLayout();
            this->tabControl1->SuspendLayout();
            this->tabPage1->SuspendLayout();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridStocks))->BeginInit();
            this->tabPage2->SuspendLayout();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest10Bid))->BeginInit();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest10Ask))->BeginInit();
            this->groupBox5->SuspendLayout();
            this->groupBox4->SuspendLayout();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest5Bid))->BeginInit();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest5Ask))->BeginInit();
            this->tabPage3->SuspendLayout();
            this->tabPage4->SuspendLayout();
            this->SuspendLayout();
            // 
            // btnIsConnected
            // 
            this->btnIsConnected->Location = System::Drawing::Point(494, 44);
            this->btnIsConnected->Name = L"btnIsConnected";
            this->btnIsConnected->Size = System::Drawing::Size(82, 32);
            this->btnIsConnected->TabIndex = 47;
            this->btnIsConnected->Text = L"IsConnected";
            this->btnIsConnected->UseVisualStyleBackColor = true;
            this->btnIsConnected->Click += gcnew System::EventHandler(this, &SKOSQuote::btnIsConnected_Click);
            // 
            // btnInitialize
            // 
            this->btnInitialize->Location = System::Drawing::Point(25, 21);
            this->btnInitialize->Name = L"btnInitialize";
            this->btnInitialize->Size = System::Drawing::Size(156, 66);
            this->btnInitialize->TabIndex = 46;
            this->btnInitialize->Text = L"Initialize";
            this->btnInitialize->UseVisualStyleBackColor = true;
            this->btnInitialize->Click += gcnew System::EventHandler(this, &SKOSQuote::btnInitialize_Click);
            // 
            // btnDisconnect
            // 
            this->btnDisconnect->Location = System::Drawing::Point(221, 59);
            this->btnDisconnect->Name = L"btnDisconnect";
            this->btnDisconnect->Size = System::Drawing::Size(70, 32);
            this->btnDisconnect->TabIndex = 45;
            this->btnDisconnect->Text = L"Disconnect";
            this->btnDisconnect->UseVisualStyleBackColor = true;
            this->btnDisconnect->Click += gcnew System::EventHandler(this, &SKOSQuote::btnDisconnect_Click);
            // 
            // button1
            // 
            this->button1->Location = System::Drawing::Point(221, 21);
            this->button1->Name = L"button1";
            this->button1->Size = System::Drawing::Size(70, 32);
            this->button1->TabIndex = 44;
            this->button1->Text = L"Connect";
            this->button1->UseVisualStyleBackColor = true;
            this->button1->Click += gcnew System::EventHandler(this, &SKOSQuote::button1_Click);
            // 
            // groupBox1
            // 
            this->groupBox1->Controls->Add(this->lblSignal);
            this->groupBox1->Location = System::Drawing::Point(323, 21);
            this->groupBox1->Name = L"groupBox1";
            this->groupBox1->Size = System::Drawing::Size(84, 70);
            this->groupBox1->TabIndex = 48;
            this->groupBox1->TabStop = false;
            this->groupBox1->Text = L"Server 0";
            // 
            // lblSignal
            // 
            this->lblSignal->AutoSize = true;
            this->lblSignal->Font = (gcnew System::Drawing::Font(L"新細明體", 16, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
                static_cast<System::Byte>(136)));
            this->lblSignal->ForeColor = System::Drawing::Color::Red;
            this->lblSignal->Location = System::Drawing::Point(25, 26);
            this->lblSignal->Name = L"lblSignal";
            this->lblSignal->Size = System::Drawing::Size(32, 22);
            this->lblSignal->TabIndex = 0;
            this->lblSignal->Text = L"●";
            // 
            // ConnectedLabel
            // 
            this->ConnectedLabel->AutoSize = true;
            this->ConnectedLabel->Location = System::Drawing::Point(606, 54);
            this->ConnectedLabel->Name = L"ConnectedLabel";
            this->ConnectedLabel->Size = System::Drawing::Size(11, 12);
            this->ConnectedLabel->TabIndex = 49;
            this->ConnectedLabel->Text = L"0";
            // 
            // tabControl1
            // 
            this->tabControl1->Controls->Add(this->tabPage1);
            this->tabControl1->Controls->Add(this->tabPage2);
            this->tabControl1->Controls->Add(this->tabPage3);
            this->tabControl1->Controls->Add(this->tabPage4);
            this->tabControl1->Location = System::Drawing::Point(25, 119);
            this->tabControl1->Name = L"tabControl1";
            this->tabControl1->SelectedIndex = 0;
            this->tabControl1->Size = System::Drawing::Size(906, 594);
            this->tabControl1->TabIndex = 50;
            // 
            // tabPage1
            // 
            this->tabPage1->Controls->Add(this->txtPageNo);
            this->tabPage1->Controls->Add(this->label6);
            this->tabPage1->Controls->Add(this->lblPage);
            this->tabPage1->Controls->Add(this->gridStocks);
            this->tabPage1->Controls->Add(this->btnQueryStocks);
            this->tabPage1->Controls->Add(this->label2);
            this->tabPage1->Controls->Add(this->txtStocks);
            this->tabPage1->Location = System::Drawing::Point(4, 22);
            this->tabPage1->Name = L"tabPage1";
            this->tabPage1->Padding = System::Windows::Forms::Padding(3);
            this->tabPage1->Size = System::Drawing::Size(898, 568);
            this->tabPage1->TabIndex = 0;
            this->tabPage1->Text = L"Quote";
            this->tabPage1->UseVisualStyleBackColor = true;
            // 
            // txtPageNo
            // 
            this->txtPageNo->Location = System::Drawing::Point(61, 23);
            this->txtPageNo->Name = L"txtPageNo";
            this->txtPageNo->Size = System::Drawing::Size(46, 22);
            this->txtPageNo->TabIndex = 15;
            this->txtPageNo->Text = L"-1";
            // 
            // label6
            // 
            this->label6->AutoSize = true;
            this->label6->Location = System::Drawing::Point(14, 26);
            this->label6->Name = L"label6";
            this->label6->Size = System::Drawing::Size(41, 12);
            this->label6->TabIndex = 14;
            this->label6->Text = L"PageNo";
            // 
            // lblPage
            // 
            this->lblPage->AutoSize = true;
            this->lblPage->Location = System::Drawing::Point(111, 8);
            this->lblPage->Name = L"lblPage";
            this->lblPage->Size = System::Drawing::Size(39, 12);
            this->lblPage->TabIndex = 13;
            this->lblPage->Text = L"lblPage";
            // 
            // gridStocks
            // 
            this->gridStocks->AllowUserToAddRows = false;
            this->gridStocks->AllowUserToDeleteRows = false;
            dataGridViewCellStyle5->Alignment = System::Windows::Forms::DataGridViewContentAlignment::MiddleCenter;
            dataGridViewCellStyle5->BackColor = System::Drawing::SystemColors::AppWorkspace;
            dataGridViewCellStyle5->Font = (gcnew System::Drawing::Font(L"Verdana", 11, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
                static_cast<System::Byte>(136)));
            dataGridViewCellStyle5->ForeColor = System::Drawing::SystemColors::WindowText;
            dataGridViewCellStyle5->SelectionBackColor = System::Drawing::SystemColors::Highlight;
            dataGridViewCellStyle5->SelectionForeColor = System::Drawing::SystemColors::HighlightText;
            dataGridViewCellStyle5->WrapMode = System::Windows::Forms::DataGridViewTriState::True;
            this->gridStocks->ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this->gridStocks->Location = System::Drawing::Point(6, 48);
            this->gridStocks->Name = L"gridStocks";
            this->gridStocks->ReadOnly = true;
            this->gridStocks->RowHeadersVisible = false;
            dataGridViewCellStyle6->Font = (gcnew System::Drawing::Font(L"新細明體", 11.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
                static_cast<System::Byte>(136)));
            this->gridStocks->RowsDefaultCellStyle = dataGridViewCellStyle6;
            this->gridStocks->RowTemplate->Height = 24;
            this->gridStocks->Size = System::Drawing::Size(886, 514);
            this->gridStocks->TabIndex = 12;
            // 
            // btnQueryStocks
            // 
            this->btnQueryStocks->Location = System::Drawing::Point(400, 6);
            this->btnQueryStocks->Name = L"btnQueryStocks";
            this->btnQueryStocks->Size = System::Drawing::Size(133, 23);
            this->btnQueryStocks->TabIndex = 11;
            this->btnQueryStocks->Text = L"查詢";
            this->btnQueryStocks->UseVisualStyleBackColor = true;
            this->btnQueryStocks->Click += gcnew System::EventHandler(this, &SKOSQuote::btnQueryStocks_Click);
            // 
            // label2
            // 
            this->label2->AutoSize = true;
            this->label2->Location = System::Drawing::Point(376, 33);
            this->label2->Name = L"label2";
            this->label2->Size = System::Drawing::Size(289, 12);
            this->label2->TabIndex = 10;
            this->label2->Text = L"格式 [ 交易代碼,商品報價代碼 ] ( 多筆以 井號{#}區隔 )";
            // 
            // txtStocks
            // 
            this->txtStocks->Location = System::Drawing::Point(113, 23);
            this->txtStocks->Name = L"txtStocks";
            this->txtStocks->Size = System::Drawing::Size(243, 22);
            this->txtStocks->TabIndex = 9;
            this->txtStocks->Text = L"NYM,NG2006#NYM,NG2007#NYM,NG2008#NYM,NG2009#NYM,NG2010";
            // 
            // tabPage2
            // 
            this->tabPage2->Controls->Add(this->btnMarketDepth);
            this->tabPage2->Controls->Add(this->label7);
            this->tabPage2->Controls->Add(this->txtOSTickPage);
            this->tabPage2->Controls->Add(this->btnLiveTick);
            this->tabPage2->Controls->Add(this->label5);
            this->tabPage2->Controls->Add(this->label3);
            this->tabPage2->Controls->Add(this->gridBest10Bid);
            this->tabPage2->Controls->Add(this->gridBest10Ask);
            this->tabPage2->Controls->Add(this->groupBox5);
            this->tabPage2->Controls->Add(this->groupBox4);
            this->tabPage2->Controls->Add(this->gridBest5Bid);
            this->tabPage2->Controls->Add(this->gridBest5Ask);
            this->tabPage2->Controls->Add(this->listTicks);
            this->tabPage2->Controls->Add(this->btnTicks);
            this->tabPage2->Controls->Add(this->txtTick);
            this->tabPage2->Location = System::Drawing::Point(4, 22);
            this->tabPage2->Name = L"tabPage2";
            this->tabPage2->Padding = System::Windows::Forms::Padding(3);
            this->tabPage2->Size = System::Drawing::Size(898, 568);
            this->tabPage2->TabIndex = 1;
            this->tabPage2->Text = L"Ticks & Best5";
            this->tabPage2->UseVisualStyleBackColor = true;
            // 
            // btnMarketDepth
            // 
            this->btnMarketDepth->Location = System::Drawing::Point(420, 3);
            this->btnMarketDepth->Name = L"btnMarketDepth";
            this->btnMarketDepth->Size = System::Drawing::Size(118, 29);
            this->btnMarketDepth->TabIndex = 22;
            this->btnMarketDepth->Text = L"Request MarketDepth";
            this->btnMarketDepth->UseVisualStyleBackColor = true;
            this->btnMarketDepth->Click += gcnew System::EventHandler(this, &SKOSQuote::btnMarketDepth_Click);
            // 
            // label7
            // 
            this->label7->AutoSize = true;
            this->label7->Location = System::Drawing::Point(8, 14);
            this->label7->Name = L"label7";
            this->label7->Size = System::Drawing::Size(27, 12);
            this->label7->TabIndex = 21;
            this->label7->Text = L"Page";
            // 
            // txtOSTickPage
            // 
            this->txtOSTickPage->Location = System::Drawing::Point(41, 11);
            this->txtOSTickPage->Name = L"txtOSTickPage";
            this->txtOSTickPage->Size = System::Drawing::Size(49, 22);
            this->txtOSTickPage->TabIndex = 20;
            this->txtOSTickPage->Text = L"-1";
            // 
            // btnLiveTick
            // 
            this->btnLiveTick->Location = System::Drawing::Point(315, 3);
            this->btnLiveTick->Name = L"btnLiveTick";
            this->btnLiveTick->Size = System::Drawing::Size(99, 29);
            this->btnLiveTick->TabIndex = 2;
            this->btnLiveTick->Text = L"Request LiveTick";
            this->btnLiveTick->UseVisualStyleBackColor = true;
            this->btnLiveTick->Click += gcnew System::EventHandler(this, &SKOSQuote::btnLiveTick_Click);
            // 
            // label5
            // 
            this->label5->AutoSize = true;
            this->label5->Location = System::Drawing::Point(6, 234);
            this->label5->Name = L"label5";
            this->label5->Size = System::Drawing::Size(29, 12);
            this->label5->TabIndex = 17;
            this->label5->Text = L"五檔";
            // 
            // label3
            // 
            this->label3->AutoSize = true;
            this->label3->Location = System::Drawing::Point(331, 34);
            this->label3->Name = L"label3";
            this->label3->Size = System::Drawing::Size(29, 12);
            this->label3->TabIndex = 16;
            this->label3->Text = L"十檔";
            // 
            // gridBest10Bid
            // 
            this->gridBest10Bid->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
            this->gridBest10Bid->Location = System::Drawing::Point(470, 49);
            this->gridBest10Bid->MultiSelect = false;
            this->gridBest10Bid->Name = L"gridBest10Bid";
            this->gridBest10Bid->ReadOnly = true;
            this->gridBest10Bid->RowHeadersVisible = false;
            this->gridBest10Bid->RowTemplate->Height = 24;
            this->gridBest10Bid->ScrollBars = System::Windows::Forms::ScrollBars::None;
            this->gridBest10Bid->Size = System::Drawing::Size(131, 292);
            this->gridBest10Bid->TabIndex = 15;
            // 
            // gridBest10Ask
            // 
            this->gridBest10Ask->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
            this->gridBest10Ask->Location = System::Drawing::Point(333, 49);
            this->gridBest10Ask->MultiSelect = false;
            this->gridBest10Ask->Name = L"gridBest10Ask";
            this->gridBest10Ask->ReadOnly = true;
            this->gridBest10Ask->RowHeadersVisible = false;
            this->gridBest10Ask->RowTemplate->Height = 24;
            this->gridBest10Ask->ScrollBars = System::Windows::Forms::ScrollBars::None;
            this->gridBest10Ask->Size = System::Drawing::Size(131, 292);
            this->gridBest10Ask->TabIndex = 14;
            // 
            // groupBox5
            // 
            this->groupBox5->Controls->Add(this->lblBest5Ask);
            this->groupBox5->Controls->Add(this->lblBest5Bid);
            this->groupBox5->Controls->Add(this->btnGetBest5);
            this->groupBox5->Controls->Add(this->txtBestStockidx);
            this->groupBox5->Controls->Add(this->label4);
            this->groupBox5->Location = System::Drawing::Point(611, 143);
            this->groupBox5->Name = L"groupBox5";
            this->groupBox5->Size = System::Drawing::Size(238, 119);
            this->groupBox5->TabIndex = 13;
            this->groupBox5->TabStop = false;
            this->groupBox5->Text = L"GetBest5";
            // 
            // lblBest5Ask
            // 
            this->lblBest5Ask->AutoSize = true;
            this->lblBest5Ask->Location = System::Drawing::Point(15, 91);
            this->lblBest5Ask->Name = L"lblBest5Ask";
            this->lblBest5Ask->Size = System::Drawing::Size(33, 12);
            this->lblBest5Ask->TabIndex = 7;
            this->lblBest5Ask->Text = L"label5";
            // 
            // lblBest5Bid
            // 
            this->lblBest5Bid->AutoSize = true;
            this->lblBest5Bid->Location = System::Drawing::Point(15, 66);
            this->lblBest5Bid->Name = L"lblBest5Bid";
            this->lblBest5Bid->Size = System::Drawing::Size(33, 12);
            this->lblBest5Bid->TabIndex = 6;
            this->lblBest5Bid->Text = L"label5";
            // 
            // btnGetBest5
            // 
            this->btnGetBest5->Location = System::Drawing::Point(155, 17);
            this->btnGetBest5->Name = L"btnGetBest5";
            this->btnGetBest5->Size = System::Drawing::Size(75, 23);
            this->btnGetBest5->TabIndex = 5;
            this->btnGetBest5->Text = L"GetBest5";
            this->btnGetBest5->UseVisualStyleBackColor = true;
            this->btnGetBest5->Click += gcnew System::EventHandler(this, &SKOSQuote::btnGetBest5_Click);
            // 
            // txtBestStockidx
            // 
            this->txtBestStockidx->Location = System::Drawing::Point(67, 18);
            this->txtBestStockidx->Name = L"txtBestStockidx";
            this->txtBestStockidx->Size = System::Drawing::Size(67, 22);
            this->txtBestStockidx->TabIndex = 4;
            // 
            // label4
            // 
            this->label4->AutoSize = true;
            this->label4->Location = System::Drawing::Point(15, 28);
            this->label4->Name = L"label4";
            this->label4->Size = System::Drawing::Size(46, 12);
            this->label4->TabIndex = 3;
            this->label4->Text = L"Stockidx";
            // 
            // groupBox4
            // 
            this->groupBox4->Controls->Add(this->lblGetTick);
            this->groupBox4->Controls->Add(this->btnGetTick);
            this->groupBox4->Controls->Add(this->label8);
            this->groupBox4->Controls->Add(this->txtTickPtr);
            this->groupBox4->Controls->Add(this->txtTickStockidx);
            this->groupBox4->Location = System::Drawing::Point(611, 49);
            this->groupBox4->Name = L"groupBox4";
            this->groupBox4->Size = System::Drawing::Size(236, 88);
            this->groupBox4->TabIndex = 12;
            this->groupBox4->TabStop = false;
            this->groupBox4->Text = L"GetTick";
            // 
            // lblGetTick
            // 
            this->lblGetTick->AutoSize = true;
            this->lblGetTick->Location = System::Drawing::Point(94, 55);
            this->lblGetTick->Name = L"lblGetTick";
            this->lblGetTick->Size = System::Drawing::Size(33, 12);
            this->lblGetTick->TabIndex = 4;
            this->lblGetTick->Text = L"label4";
            // 
            // btnGetTick
            // 
            this->btnGetTick->Location = System::Drawing::Point(6, 50);
            this->btnGetTick->Name = L"btnGetTick";
            this->btnGetTick->Size = System::Drawing::Size(75, 23);
            this->btnGetTick->TabIndex = 4;
            this->btnGetTick->Text = L"GetTick";
            this->btnGetTick->UseVisualStyleBackColor = true;
            this->btnGetTick->Click += gcnew System::EventHandler(this, &SKOSQuote::btnGetTick_Click);
            // 
            // label8
            // 
            this->label8->AutoSize = true;
            this->label8->Location = System::Drawing::Point(6, 24);
            this->label8->Name = L"label8";
            this->label8->Size = System::Drawing::Size(68, 12);
            this->label8->TabIndex = 2;
            this->label8->Text = L"Stockidx/nPtr";
            // 
            // txtTickPtr
            // 
            this->txtTickPtr->Location = System::Drawing::Point(162, 14);
            this->txtTickPtr->Name = L"txtTickPtr";
            this->txtTickPtr->Size = System::Drawing::Size(68, 22);
            this->txtTickPtr->TabIndex = 1;
            // 
            // txtTickStockidx
            // 
            this->txtTickStockidx->Location = System::Drawing::Point(79, 14);
            this->txtTickStockidx->Name = L"txtTickStockidx";
            this->txtTickStockidx->Size = System::Drawing::Size(67, 22);
            this->txtTickStockidx->TabIndex = 0;
            // 
            // gridBest5Bid
            // 
            this->gridBest5Bid->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
            this->gridBest5Bid->Location = System::Drawing::Point(196, 234);
            this->gridBest5Bid->MultiSelect = false;
            this->gridBest5Bid->Name = L"gridBest5Bid";
            this->gridBest5Bid->ReadOnly = true;
            this->gridBest5Bid->RowHeadersVisible = false;
            this->gridBest5Bid->RowTemplate->Height = 24;
            this->gridBest5Bid->ScrollBars = System::Windows::Forms::ScrollBars::None;
            this->gridBest5Bid->Size = System::Drawing::Size(131, 172);
            this->gridBest5Bid->TabIndex = 11;
            // 
            // gridBest5Ask
            // 
            this->gridBest5Ask->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
            this->gridBest5Ask->Location = System::Drawing::Point(59, 234);
            this->gridBest5Ask->MultiSelect = false;
            this->gridBest5Ask->Name = L"gridBest5Ask";
            this->gridBest5Ask->ReadOnly = true;
            this->gridBest5Ask->RowHeadersVisible = false;
            this->gridBest5Ask->RowTemplate->Height = 24;
            this->gridBest5Ask->ScrollBars = System::Windows::Forms::ScrollBars::None;
            this->gridBest5Ask->Size = System::Drawing::Size(131, 172);
            this->gridBest5Ask->TabIndex = 10;
            // 
            // listTicks
            // 
            this->listTicks->Font = (gcnew System::Drawing::Font(L"新細明體", 13, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
                static_cast<System::Byte>(136)));
            this->listTicks->FormattingEnabled = true;
            this->listTicks->HorizontalScrollbar = true;
            this->listTicks->ItemHeight = 17;
            this->listTicks->Location = System::Drawing::Point(8, 49);
            this->listTicks->Name = L"listTicks";
            this->listTicks->Size = System::Drawing::Size(319, 174);
            this->listTicks->TabIndex = 3;
            // 
            // btnTicks
            // 
            this->btnTicks->Location = System::Drawing::Point(215, 3);
            this->btnTicks->Name = L"btnTicks";
            this->btnTicks->Size = System::Drawing::Size(94, 29);
            this->btnTicks->TabIndex = 1;
            this->btnTicks->Text = L"Request Tick";
            this->btnTicks->UseVisualStyleBackColor = true;
            this->btnTicks->Click += gcnew System::EventHandler(this, &SKOSQuote::btnTicks_Click);
            // 
            // txtTick
            // 
            this->txtTick->Location = System::Drawing::Point(96, 11);
            this->txtTick->Name = L"txtTick";
            this->txtTick->Size = System::Drawing::Size(113, 22);
            this->txtTick->TabIndex = 0;
            this->txtTick->Text = L"CBOT,FF2007";
            // 
            // tabPage3
            // 
            this->tabPage3->Controls->Add(this->btnOverseaProducts2);
            this->tabPage3->Controls->Add(this->listOverseaProducts);
            this->tabPage3->Controls->Add(this->btnOverseaProducts);
            this->tabPage3->Location = System::Drawing::Point(4, 22);
            this->tabPage3->Name = L"tabPage3";
            this->tabPage3->Padding = System::Windows::Forms::Padding(3);
            this->tabPage3->Size = System::Drawing::Size(898, 568);
            this->tabPage3->TabIndex = 2;
            this->tabPage3->Text = L"Products";
            this->tabPage3->UseVisualStyleBackColor = true;
            // 
            // btnOverseaProducts2
            // 
            this->btnOverseaProducts2->Location = System::Drawing::Point(218, 21);
            this->btnOverseaProducts2->Name = L"btnOverseaProducts2";
            this->btnOverseaProducts2->Size = System::Drawing::Size(223, 43);
            this->btnOverseaProducts2->TabIndex = 4;
            this->btnOverseaProducts2->Text = L"GetOverseaProductsDetail";
            this->btnOverseaProducts2->UseVisualStyleBackColor = true;
            this->btnOverseaProducts2->Click += gcnew System::EventHandler(this, &SKOSQuote::btnOverseaProducts2_Click);
            // 
            // listOverseaProducts
            // 
            this->listOverseaProducts->Font = (gcnew System::Drawing::Font(L"新細明體", 13, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
                static_cast<System::Byte>(136)));
            this->listOverseaProducts->FormattingEnabled = true;
            this->listOverseaProducts->HorizontalScrollbar = true;
            this->listOverseaProducts->ItemHeight = 17;
            this->listOverseaProducts->Location = System::Drawing::Point(63, 70);
            this->listOverseaProducts->Name = L"listOverseaProducts";
            this->listOverseaProducts->Size = System::Drawing::Size(717, 480);
            this->listOverseaProducts->TabIndex = 3;
            // 
            // btnOverseaProducts
            // 
            this->btnOverseaProducts->Location = System::Drawing::Point(63, 21);
            this->btnOverseaProducts->Name = L"btnOverseaProducts";
            this->btnOverseaProducts->Size = System::Drawing::Size(117, 43);
            this->btnOverseaProducts->TabIndex = 2;
            this->btnOverseaProducts->Text = L"GetOverseaProducts";
            this->btnOverseaProducts->UseVisualStyleBackColor = true;
            this->btnOverseaProducts->Click += gcnew System::EventHandler(this, &SKOSQuote::btnOverseaProducts_Click);
            // 
            // tabPage4
            // 
            this->tabPage4->Controls->Add(this->txtMinuteNumber);
            this->tabPage4->Controls->Add(this->label1);
            this->tabPage4->Controls->Add(this->btnKLineByDate);
            this->tabPage4->Controls->Add(this->txtEndDate);
            this->tabPage4->Controls->Add(this->txtStartDate);
            this->tabPage4->Controls->Add(this->label9);
            this->tabPage4->Controls->Add(this->label10);
            this->tabPage4->Controls->Add(this->boxKLineType);
            this->tabPage4->Controls->Add(this->listKLine);
            this->tabPage4->Controls->Add(this->btnKLine);
            this->tabPage4->Controls->Add(this->txtKLine);
            this->tabPage4->Location = System::Drawing::Point(4, 22);
            this->tabPage4->Name = L"tabPage4";
            this->tabPage4->Padding = System::Windows::Forms::Padding(3);
            this->tabPage4->Size = System::Drawing::Size(898, 568);
            this->tabPage4->TabIndex = 3;
            this->tabPage4->Text = L"KLine Data";
            this->tabPage4->UseVisualStyleBackColor = true;
            // 
            // btnKLineByDate
            // 
            this->btnKLineByDate->Location = System::Drawing::Point(646, 46);
            this->btnKLineByDate->Name = L"btnKLineByDate";
            this->btnKLineByDate->Size = System::Drawing::Size(163, 40);
            this->btnKLineByDate->TabIndex = 20;
            this->btnKLineByDate->Text = L"RequestKLineByDate";
            this->btnKLineByDate->UseVisualStyleBackColor = true;
            this->btnKLineByDate->Click += gcnew System::EventHandler(this, &SKOSQuote::btnKLineByDate_Click);
            // 
            // txtEndDate
            // 
            this->txtEndDate->Location = System::Drawing::Point(529, 43);
            this->txtEndDate->Name = L"txtEndDate";
            this->txtEndDate->Size = System::Drawing::Size(100, 22);
            this->txtEndDate->TabIndex = 19;
            this->txtEndDate->Text = L"20200901";
            // 
            // txtStartDate
            // 
            this->txtStartDate->Location = System::Drawing::Point(529, 15);
            this->txtStartDate->Name = L"txtStartDate";
            this->txtStartDate->Size = System::Drawing::Size(100, 22);
            this->txtStartDate->TabIndex = 18;
            this->txtStartDate->Text = L"20200801";
            // 
            // label9
            // 
            this->label9->AutoSize = true;
            this->label9->Location = System::Drawing::Point(476, 46);
            this->label9->Name = L"label9";
            this->label9->Size = System::Drawing::Size(45, 12);
            this->label9->TabIndex = 16;
            this->label9->Text = L"EndDate";
            // 
            // label10
            // 
            this->label10->AutoSize = true;
            this->label10->Location = System::Drawing::Point(476, 18);
            this->label10->Name = L"label10";
            this->label10->Size = System::Drawing::Size(47, 12);
            this->label10->TabIndex = 15;
            this->label10->Text = L"StartDate";
            // 
            // boxKLineType
            // 
            this->boxKLineType->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
            this->boxKLineType->FormattingEnabled = true;
            this->boxKLineType->Items->AddRange(gcnew cli::array< System::Object^  >(4) { L"分鐘線", L"日線", L"週線", L"月線" });
            this->boxKLineType->Location = System::Drawing::Point(156, 26);
            this->boxKLineType->Name = L"boxKLineType";
            this->boxKLineType->Size = System::Drawing::Size(121, 20);
            this->boxKLineType->TabIndex = 14;
            // 
            // listKLine
            // 
            this->listKLine->Font = (gcnew System::Drawing::Font(L"新細明體", 11, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
                static_cast<System::Byte>(136)));
            this->listKLine->FormattingEnabled = true;
            this->listKLine->ItemHeight = 15;
            this->listKLine->Location = System::Drawing::Point(41, 130);
            this->listKLine->Name = L"listKLine";
            this->listKLine->Size = System::Drawing::Size(768, 424);
            this->listKLine->TabIndex = 13;
            // 
            // btnKLine
            // 
            this->btnKLine->Location = System::Drawing::Point(292, 15);
            this->btnKLine->Name = L"btnKLine";
            this->btnKLine->Size = System::Drawing::Size(171, 40);
            this->btnKLine->TabIndex = 12;
            this->btnKLine->Text = L"RequestLKine";
            this->btnKLine->UseVisualStyleBackColor = true;
            this->btnKLine->Click += gcnew System::EventHandler(this, &SKOSQuote::btnKLine_Click);
            // 
            // txtKLine
            // 
            this->txtKLine->Location = System::Drawing::Point(41, 26);
            this->txtKLine->Name = L"txtKLine";
            this->txtKLine->Size = System::Drawing::Size(100, 22);
            this->txtKLine->TabIndex = 11;
            this->txtKLine->Text = L"CME,ES1912";
            // 
            // txtMinuteNumber
            // 
            this->txtMinuteNumber->Location = System::Drawing::Point(529, 80);
            this->txtMinuteNumber->Name = L"txtMinuteNumber";
            this->txtMinuteNumber->Size = System::Drawing::Size(100, 22);
            this->txtMinuteNumber->TabIndex = 25;
            this->txtMinuteNumber->Text = L"1";
            // 
            // label1
            // 
            this->label1->AutoSize = true;
            this->label1->Location = System::Drawing::Point(447, 78);
            this->label1->Name = L"label1";
            this->label1->Size = System::Drawing::Size(76, 24);
            this->label1->TabIndex = 24;
            this->label1->Text = L"MinuteNumber\r\n(指定幾分K)";
            // 
            // SKOSQuote
            // 
            this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
            this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
            this->Controls->Add(this->tabControl1);
            this->Controls->Add(this->ConnectedLabel);
            this->Controls->Add(this->groupBox1);
            this->Controls->Add(this->btnIsConnected);
            this->Controls->Add(this->btnInitialize);
            this->Controls->Add(this->btnDisconnect);
            this->Controls->Add(this->button1);
            this->Name = L"SKOSQuote";
            this->Size = System::Drawing::Size(953, 828);
            this->groupBox1->ResumeLayout(false);
            this->groupBox1->PerformLayout();
            this->tabControl1->ResumeLayout(false);
            this->tabPage1->ResumeLayout(false);
            this->tabPage1->PerformLayout();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridStocks))->EndInit();
            this->tabPage2->ResumeLayout(false);
            this->tabPage2->PerformLayout();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest10Bid))->EndInit();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest10Ask))->EndInit();
            this->groupBox5->ResumeLayout(false);
            this->groupBox5->PerformLayout();
            this->groupBox4->ResumeLayout(false);
            this->groupBox4->PerformLayout();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest5Bid))->EndInit();
            (cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->gridBest5Ask))->EndInit();
            this->tabPage3->ResumeLayout(false);
            this->tabPage4->ResumeLayout(false);
            this->tabPage4->PerformLayout();
            this->ResumeLayout(false);
            this->PerformLayout();

        }
#pragma endregion


    };
}
