#include "SKOSQuote.h"

Void CppCLITester::SKOSQuote::OnUpDateDataQuote(SKCOMLib::SKFOREIGN_9LONG pForeign)
{
    int nStockIdx = pForeign.nStockIdx;

    DataRow^ drFind = m_dtForeigns->Rows->Find(nStockIdx);
    if (drFind == nullptr)
    {
        DataRow^ myDataRow = m_dtForeigns->NewRow();

        myDataRow["m_nStockidx"] = pForeign.nStockIdx;
        myDataRow["m_sDecimal"] = pForeign.sDecimal;
        myDataRow["m_nDenominator"] = pForeign.nDenominator;
        myDataRow["m_cMarketNo"] = pForeign.bstrMarketNo->Trim();
        myDataRow["m_caExchangeNo"] = pForeign.bstrExchangeNo->Trim();
        myDataRow["m_caExchangeName"] = pForeign.bstrExchangeName->Trim();
        myDataRow["m_caStockNo"] = pForeign.bstrStockNo->Trim();
        myDataRow["m_caStockName"] = pForeign.bstrStockName->Trim();

        myDataRow["m_nOpen"] = pForeign.nOpen / (Math::Pow(10, pForeign.sDecimal));
        myDataRow["m_nHigh"] = pForeign.nHigh / (Math::Pow(10, pForeign.sDecimal));
        myDataRow["m_nLow"] = pForeign.nLow / (Math::Pow(10, pForeign.sDecimal));
        myDataRow["m_nClose"] = pForeign.nClose / (Math::Pow(10, pForeign.sDecimal));
        myDataRow["m_dSettlePrice"] = pForeign.nSettlePrice / (Math::Pow(10, pForeign.sDecimal));

        myDataRow["m_nTickQty"] = pForeign.nTickQty;
        myDataRow["m_nRef"] = pForeign.nRef / (Math::Pow(10, pForeign.sDecimal));
        myDataRow["m_nBid"] = pForeign.nBid / (Math::Pow(10, pForeign.sDecimal));
        myDataRow["m_nBc"] = pForeign.nBc;
        myDataRow["m_nAsk"] = pForeign.nAsk;
        myDataRow["m_nAc"] = pForeign.nAc / (Math::Pow(10, pForeign.sDecimal));
        myDataRow["m_nTQty"] = pForeign.nTQty;

        m_dtForeigns->Rows->Add(myDataRow);
    }
    else
    {
        drFind["m_nStockidx"] = pForeign.nStockIdx;
        drFind["m_sDecimal"] = pForeign.sDecimal;
        drFind["m_nDenominator"] = pForeign.nDenominator;
        drFind["m_cMarketNo"] = pForeign.bstrMarketNo->Trim();
        drFind["m_caExchangeNo"] = pForeign.bstrExchangeNo->Trim();
        drFind["m_caExchangeName"] = pForeign.bstrExchangeName->Trim();
        drFind["m_caStockNo"] = pForeign.bstrStockNo->Trim();
        drFind["m_caStockName"] = pForeign.bstrStockName->Trim();

        drFind["m_nOpen"] = pForeign.nOpen / (Math::Pow(10, pForeign.sDecimal));
        drFind["m_nHigh"] = pForeign.nHigh / (Math::Pow(10, pForeign.sDecimal));
        drFind["m_nLow"] = pForeign.nLow / (Math::Pow(10, pForeign.sDecimal));
        drFind["m_nClose"] = pForeign.nClose / (Math::Pow(10, pForeign.sDecimal));
        drFind["m_dSettlePrice"] = pForeign.nSettlePrice / (Math::Pow(10, pForeign.sDecimal));

        drFind["m_nTickQty"] = pForeign.nTickQty;
        drFind["m_nRef"] = pForeign.nRef / (Math::Pow(10, pForeign.sDecimal));
        drFind["m_nBid"] = pForeign.nBid / (Math::Pow(10, pForeign.sDecimal));
        drFind["m_nBc"] = pForeign.nBc;
        drFind["m_nAsk"] = pForeign.nAsk / (Math::Pow(10, pForeign.sDecimal));
        drFind["m_nAc"] = pForeign.nAc;
        drFind["m_nTQty"] = pForeign.nTQty;
    }
}

//Void CppCLITester::SKOSQuote::OnNotifyHistoryTicks(short sStockidx, int nPtr, int nDate, int nTime, int nClose, int nQty)
//{
//    System::String^ strData = "[OnNotifyHistoryTicks]" + sStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
//
//    listTicks->Items->Add(strData);
//
//    listTicks->SelectedIndex = listTicks->Items->Count - 1;
//}

Void CppCLITester::SKOSQuote::OnNotifyBest5LONG(int nStockidx, Int64 nBestBid1, int nBestBidQty1, Int64 nBestBid2, int nBestBidQty2, Int64 nBestBid3, int nBestBidQty3, Int64 nBestBid4, int nBestBidQty4, Int64 nBestBid5, int nBestBidQty5, Int64 nBestAsk1, int nBestAskQty1, Int64 nBestAsk2, int nBestAskQty2, Int64 nBestAsk3, int nBestAskQty3, Int64 nBestAsk4, int nBestAskQty4, Int64 nBestAsk5, int nBestAskQty5)
{
    if (m_dtBest5Ask->Rows->Count == 0 && m_dtBest5Bid->Rows->Count == 0)
    {
        DataRow^ myDataRow;

        myDataRow = m_dtBest5Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty1;
        myDataRow["m_nAsk"] = nBestAsk1;
        m_dtBest5Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty2;
        myDataRow["m_nAsk"] = nBestAsk2;
        m_dtBest5Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty3;
        myDataRow["m_nAsk"] = nBestAsk3;
        m_dtBest5Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty4;
        myDataRow["m_nAsk"] = nBestAsk4;
        m_dtBest5Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty5;
        myDataRow["m_nAsk"] = nBestAsk5;
        m_dtBest5Ask->Rows->Add(myDataRow);



        myDataRow = m_dtBest5Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty1;
        myDataRow["m_nAsk"] = nBestBid1;
        m_dtBest5Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty2;
        myDataRow["m_nAsk"] = nBestBid2;
        m_dtBest5Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty3;
        myDataRow["m_nAsk"] = nBestBid3;
        m_dtBest5Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty4;
        myDataRow["m_nAsk"] = nBestBid4;
        m_dtBest5Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest5Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty5;
        myDataRow["m_nAsk"] = nBestBid5;
        m_dtBest5Bid->Rows->Add(myDataRow);


    }
   else
    {
        gridBest5Ask->Rows[0]->Cells["m_nAskQty"]->Value = nBestAskQty1;
        gridBest5Ask->Rows[0]->Cells["m_nAsk"]->Value = nBestAsk1;

        gridBest5Ask->Rows[1]->Cells["m_nAskQty"]->Value = nBestAskQty2;
        gridBest5Ask->Rows[1]->Cells["m_nAsk"]->Value = nBestAsk2;

        gridBest5Ask->Rows[2]->Cells["m_nAskQty"]->Value = nBestAskQty3;
        gridBest5Ask->Rows[2]->Cells["m_nAsk"]->Value = nBestAsk3;

        gridBest5Ask->Rows[3]->Cells["m_nAskQty"]->Value = nBestAskQty4;
        gridBest5Ask->Rows[3]->Cells["m_nAsk"]->Value = nBestAsk4;

        gridBest5Ask->Rows[4]->Cells["m_nAskQty"]->Value = nBestAskQty5;
        gridBest5Ask->Rows[4]->Cells["m_nAsk"]->Value = nBestAsk5;


        gridBest5Bid->Rows[0]->Cells["m_nAskQty"]->Value = nBestBidQty1;
        gridBest5Bid->Rows[0]->Cells["m_nAsk"]->Value = nBestBid1;

        gridBest5Bid->Rows[1]->Cells["m_nAskQty"]->Value = nBestBidQty2;
        gridBest5Bid->Rows[1]->Cells["m_nAsk"]->Value = nBestBid2;

        gridBest5Bid->Rows[2]->Cells["m_nAskQty"]->Value = nBestBidQty3;
        gridBest5Bid->Rows[2]->Cells["m_nAsk"]->Value = nBestBid3;

        gridBest5Bid->Rows[3]->Cells["m_nAskQty"]->Value = nBestBidQty4;
        gridBest5Bid->Rows[3]->Cells["m_nAsk"]->Value = nBestBid4;

        gridBest5Bid->Rows[4]->Cells["m_nAskQty"]->Value = nBestBidQty5;
        gridBest5Bid->Rows[4]->Cells["m_nAsk"]->Value = nBestBid5;
    }
}

Void CppCLITester::SKOSQuote::OnNotifyBest10LONG(int nStockIdx, Int64 nBestBid1, int nBestBidQty1, Int64 nBestBid2, int nBestBidQty2, Int64 nBestBid3, int nBestBidQty3, Int64 nBestBid4, int nBestBidQty4, Int64 nBestBid5, int nBestBidQty5, Int64 nBestBid6, int nBestBidQty6, Int64 nBestBid7, int nBestBidQty7, Int64 nBestBid8, int nBestBidQty8, Int64 nBestBid9, int nBestBidQty9, Int64 nBestBid10, int nBestBidQty10, Int64 nBestAsk1, int nBestAskQty1, Int64 nBestAsk2, int nBestAskQty2, Int64 nBestAsk3, int nBestAskQty3, Int64 nBestAsk4, int nBestAskQty4, Int64 nBestAsk5, int nBestAskQty5, Int64 nBestAsk6, int nBestAskQty6, Int64 nBestAsk7, int nBestAskQty7, Int64 nBestAsk8, int nBestAskQty8, Int64 nBestAsk9, int nBestAskQty9, Int64 nBestAsk10, int nBestAskQty10)
{
    if (m_dtBest10Ask->Rows->Count == 0 && m_dtBest10Bid->Rows->Count == 0)
    {
        DataRow^ myDataRow;

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty1;
        myDataRow["m_nAsk"] = nBestAsk1;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty2;
        myDataRow["m_nAsk"] = nBestAsk2;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty3;
        myDataRow["m_nAsk"] = nBestAsk3;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty4;
        myDataRow["m_nAsk"] = nBestAsk4;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty5;
        myDataRow["m_nAsk"] = nBestAsk5;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty6;
        myDataRow["m_nAsk"] = nBestAsk6;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty7;
        myDataRow["m_nAsk"] = nBestAsk7;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty8;
        myDataRow["m_nAsk"] = nBestAsk8;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty9;
        myDataRow["m_nAsk"] = nBestAsk9;
        m_dtBest10Ask->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Ask->NewRow();
        myDataRow["m_nAskQty"] = nBestAskQty10;
        myDataRow["m_nAsk"] = nBestAsk10;
        m_dtBest10Ask->Rows->Add(myDataRow);



        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty1;
        myDataRow["m_nAsk"] = nBestBid1;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty2;
        myDataRow["m_nAsk"] = nBestBid2;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty3;
        myDataRow["m_nAsk"] = nBestBid3;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty4;
        myDataRow["m_nAsk"] = nBestBid4;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty5;
        myDataRow["m_nAsk"] = nBestBid5;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty6;
        myDataRow["m_nAsk"] = nBestBid6;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty7;
        myDataRow["m_nAsk"] = nBestBid7;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty8;
        myDataRow["m_nAsk"] = nBestBid8;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty9;
        myDataRow["m_nAsk"] = nBestBid9;
        m_dtBest10Bid->Rows->Add(myDataRow);

        myDataRow = m_dtBest10Bid->NewRow();
        myDataRow["m_nAskQty"] = nBestBidQty10;
        myDataRow["m_nAsk"] = nBestBid10;
        m_dtBest10Bid->Rows->Add(myDataRow);

    }
    else
    {
        gridBest10Ask->Rows[0]->Cells["m_nAskQty"]->Value = nBestAskQty1;
        gridBest10Ask->Rows[0]->Cells["m_nAsk"]->Value = nBestAsk1;

        gridBest10Ask->Rows[1]->Cells["m_nAskQty"]->Value = nBestAskQty2;
        gridBest10Ask->Rows[1]->Cells["m_nAsk"]->Value = nBestAsk2;

        gridBest10Ask->Rows[2]->Cells["m_nAskQty"]->Value = nBestAskQty3;
        gridBest10Ask->Rows[2]->Cells["m_nAsk"]->Value = nBestAsk3;

        gridBest10Ask->Rows[3]->Cells["m_nAskQty"]->Value = nBestAskQty4;
        gridBest10Ask->Rows[3]->Cells["m_nAsk"]->Value = nBestAsk4;

        gridBest10Ask->Rows[4]->Cells["m_nAskQty"]->Value = nBestAskQty5;
        gridBest10Ask->Rows[4]->Cells["m_nAsk"]->Value = nBestAsk5;

        gridBest10Ask->Rows[5]->Cells["m_nAskQty"]->Value = nBestAskQty6;
        gridBest10Ask->Rows[5]->Cells["m_nAsk"]->Value = nBestAsk6;

        gridBest10Ask->Rows[6]->Cells["m_nAskQty"]->Value = nBestAskQty7;
        gridBest10Ask->Rows[6]->Cells["m_nAsk"]->Value = nBestAsk7;

        gridBest10Ask->Rows[7]->Cells["m_nAskQty"]->Value = nBestAskQty8;
        gridBest10Ask->Rows[7]->Cells["m_nAsk"]->Value = nBestAsk8;

        gridBest10Ask->Rows[8]->Cells["m_nAskQty"]->Value = nBestAskQty9;
        gridBest10Ask->Rows[8]->Cells["m_nAsk"]->Value = nBestAsk9;

        gridBest10Ask->Rows[9]->Cells["m_nAskQty"]->Value = nBestAskQty10;
        gridBest10Ask->Rows[9]->Cells["m_nAsk"]->Value = nBestAsk10;


        gridBest10Bid->Rows[0]->Cells["m_nAskQty"]->Value = nBestBidQty1;
        gridBest10Bid->Rows[0]->Cells["m_nAsk"]->Value = nBestBid1;

        gridBest10Bid->Rows[1]->Cells["m_nAskQty"]->Value = nBestBidQty2;
        gridBest10Bid->Rows[1]->Cells["m_nAsk"]->Value = nBestBid2;

        gridBest10Bid->Rows[2]->Cells["m_nAskQty"]->Value = nBestBidQty3;
        gridBest10Bid->Rows[2]->Cells["m_nAsk"]->Value = nBestBid3;

        gridBest10Bid->Rows[3]->Cells["m_nAskQty"]->Value = nBestBidQty4;
        gridBest10Bid->Rows[3]->Cells["m_nAsk"]->Value = nBestBid4;

        gridBest10Bid->Rows[4]->Cells["m_nAskQty"]->Value = nBestBidQty5;
        gridBest10Bid->Rows[4]->Cells["m_nAsk"]->Value = nBestBid5;

        gridBest10Bid->Rows[5]->Cells["m_nAskQty"]->Value = nBestBidQty6;
        gridBest10Bid->Rows[5]->Cells["m_nAsk"]->Value = nBestBid6;

        gridBest10Bid->Rows[6]->Cells["m_nAskQty"]->Value = nBestBidQty7;
        gridBest10Bid->Rows[6]->Cells["m_nAsk"]->Value = nBestBid7;

        gridBest10Bid->Rows[7]->Cells["m_nAskQty"]->Value = nBestBidQty8;
        gridBest10Bid->Rows[7]->Cells["m_nAsk"]->Value = nBestBid8;

        gridBest10Bid->Rows[8]->Cells["m_nAskQty"]->Value = nBestBidQty9;
        gridBest10Bid->Rows[8]->Cells["m_nAsk"]->Value = nBestBid9;

        gridBest10Bid->Rows[9]->Cells["m_nAskQty"]->Value = nBestBidQty10;
        gridBest10Bid->Rows[9]->Cells["m_nAsk"]->Value = nBestBid10;
    }
}

Void CppCLITester::SKOSQuote::OnNotifyTicksNineLONG(int nStockidx, int nPtr, int nDate, int nTime, Int64 nClose, int nQty)
{
    System::String^ strData = "[OnNotifyTicksNineLONG]" + nStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

    listTicks->Items->Add(strData);

    listTicks->SelectedIndex = listTicks->Items->Count - 1;
}

Void CppCLITester::SKOSQuote::OnNotifyHistoryTicksNineLONG(int nStockidx, int nPtr, int nDate, int nTime, Int64 nClose, int nQty)
{
    System::String^ strData = "[OnNotifyHistoryTicksNineLONG]" + nStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();

    listTicks->Items->Add(strData);

    listTicks->SelectedIndex = listTicks->Items->Count - 1;
}

Void CppCLITester::SKOSQuote::OnKLineData(System::String^ strStockNo, System::String^ strData)
{
    listKLine->Items->Add("[OnKLineData]" + strStockNo + ":" + strData);
}

Void CppCLITester::SKOSQuote::OnConnect(int nKind, int nCode)
{
    if (nKind == 3001 && nCode == 0)
    {
        lblSignal->ForeColor = Color::Green;
    }
    else
    {
        lblSignal->ForeColor = Color::Red;
    }
}

Void CppCLITester::SKOSQuote::OnOverSeaProducts(System::String^ strValue)
{
    listOverseaProducts->Items->Add("[OnOverSeaProducts]" + strValue);
}

Void CppCLITester::SKOSQuote::OnOverSeaProductsDetail(System::String^ strValue)
{
    listOverseaProducts->Items->Add("[OnOverSeaProductsDetail]" + strValue);
}

//Void CppCLITester::SKOSQuote::OnNotifyTicks(short sStockidx, int nPtr, int nDate, int nTime, int nClose, int nQty)
//{
//    System::String^ strData = "[OnNotifyTicks]" + sStockidx.ToString() + "," + nPtr.ToString() + "," + nDate.ToString() + " " + nTime.ToString() + "," + nClose.ToString() + "," + nQty.ToString();
//
//    listTicks->Items->Add(strData);
//
//    listTicks->SelectedIndex = listTicks->Items->Count - 1;
//}

Void CppCLITester::SKOSQuote::OnQuoteUpdate(int nStockidx)
{
    SKCOMLib::SKFOREIGN_9LONG pForeign;

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_GetStockByIndexNineDigitLONG(nStockidx, pForeign);

    OnUpDateDataQuote(pForeign);
}

Void CppCLITester::SKOSQuote::btnInitialize_Click(System::Object^ sender, System::EventArgs^ e)
{
    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_Initialize();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_Initialize");
}

Void CppCLITester::SKOSQuote::button1_Click(System::Object^ sender, System::EventArgs^ e)
{
    if (m_bfirst == true)
    {
        // 硈絬
        m_pSKOSQuote->OnConnect += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnConnectEventHandler(this, &CppCLITester::SKOSQuote::OnConnect);

        // Quote
        m_pSKOSQuote->OnNotifyQuoteLONG += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnNotifyQuoteLONGEventHandler(this, &CppCLITester::SKOSQuote::OnQuoteUpdate);

        //Ticks
        //m_pSKOSQuote->OnNotifyTicks += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnNotifyTicksEventHandler(this, &CppCLITester::SKOSQuote::OnNotifyTicks);
        //m_pSKOSQuote->OnNotifyHistoryTicks += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnNotifyHistoryTicksEventHandler(this, &CppCLITester::SKOSQuote::OnNotifyHistoryTicks);

        m_pSKOSQuote->OnNotifyBest5NineDigitLONG += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnNotifyBest5NineDigitLONGEventHandler(this, &CppCLITester::SKOSQuote::OnNotifyBest5LONG);
        m_pSKOSQuote->OnNotifyBest10NineDigitLONG += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnNotifyBest10NineDigitLONGEventHandler(this, &CppCLITester::SKOSQuote::OnNotifyBest10LONG);

        m_pSKOSQuote->OnNotifyTicksNineDigitLONG += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnNotifyTicksNineDigitLONGEventHandler(this, &CppCLITester::SKOSQuote::OnNotifyTicksNineLONG);
        m_pSKOSQuote->OnNotifyHistoryTicksNineDigitLONG += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnNotifyHistoryTicksNineDigitLONGEventHandler(this, &CppCLITester::SKOSQuote::OnNotifyHistoryTicksNineLONG);

        // products
        m_pSKOSQuote->OnOverseaProducts += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnOverseaProductsEventHandler(this, &CppCLITester::SKOSQuote::OnOverSeaProducts);
        m_pSKOSQuote->OnOverseaProductsDetail += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnOverseaProductsDetailEventHandler(this, &CppCLITester::SKOSQuote::OnOverSeaProductsDetail);

        // KLine
        m_pSKOSQuote->OnKLineData += gcnew SKCOMLib::_ISKOSQuoteLibEvents_OnKLineDataEventHandler(this, &CppCLITester::SKOSQuote::OnKLineData);

        m_bfirst = false;
    }
    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_EnterMonitorLONG();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_EnterMonitorLONG");

    if (m_nCode == 0)
    {
        lblSignal->ForeColor = Color::Yellow;
    }
}

Void CppCLITester::SKOSQuote::btnDisconnect_Click(System::Object^ sender, System::EventArgs^ e)
{
    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_LeaveMonitor();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_LeaveMonitor");
}

Void CppCLITester::SKOSQuote::btnIsConnected_Click(System::Object^ sender, System::EventArgs^ e)
{
    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_IsConnected();

    if (m_nCode == 0)
    {
        ConnectedLabel->Text = "False";
        ConnectedLabel->BackColor = Color::Red;
    }
    else if (m_nCode == 1)
    {
        ConnectedLabel->Text = "True";
        ConnectedLabel->BackColor = Color::Green;
    }
    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_IsConnected");
}

Void CppCLITester::SKOSQuote::btnQueryStocks_Click(System::Object^ sender, System::EventArgs^ e)
{
    m_dtForeigns->Clear();
    gridStocks->ClearSelection();

    gridStocks->DataSource = m_dtForeigns;

    gridStocks->Columns["m_nStockidx"]->Visible = false;
    gridStocks->Columns["m_sDecimal"]->Visible = false;
    gridStocks->Columns["m_nDenominator"]->Visible = false;
    gridStocks->Columns["m_cMarketNo"]->Visible = false;
    gridStocks->Columns["m_caExchangeNo"]->HeaderText = "ユ┮絏";
    gridStocks->Columns["m_caExchangeName"]->HeaderText = "ユ┮嘿";
    gridStocks->Columns["m_caStockNo"]->HeaderText = "坝珇絏";
    gridStocks->Columns["m_caStockName"]->HeaderText = "坝珇嘿";

    gridStocks->Columns["m_nOpen"]->HeaderText = "秨絃基";
    gridStocks->Columns["m_nHigh"]->HeaderText = "程蔼基";
    gridStocks->Columns["m_nLow"]->HeaderText = "程基";
    gridStocks->Columns["m_nClose"]->HeaderText = "Θユ基";
    gridStocks->Columns["m_dSettlePrice"]->HeaderText = "挡衡基";
    gridStocks->Columns["m_nTickQty"]->HeaderText = "虫秖";
    gridStocks->Columns["m_nRef"]->HeaderText = "琎Μ基";

    gridStocks->Columns["m_nBid"]->HeaderText = "禦基";
    gridStocks->Columns["m_nBc"]->HeaderText = "禦秖";
    gridStocks->Columns["m_nAsk"]->HeaderText = "芥基";
    gridStocks->Columns["m_nAc"]->HeaderText = "芥秖";
    gridStocks->Columns["m_nTQty"]->HeaderText = "Θユ秖";

    short sPageNo = Convert::ToInt16(txtPageNo->Text);
    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestStocks(sPageNo, txtStocks->Text->Trim());

    txtPageNo->Text = sPageNo.ToString();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestStocks");
}

Void CppCLITester::SKOSQuote::btnOverseaProducts2_Click(System::Object^ sender, System::EventArgs^ e)
{
    listOverseaProducts->Items->Clear();

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_GetOverseaProductDetail(1);
    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_GetOverseaProductsDetail");
}

Void CppCLITester::SKOSQuote::btnTicks_Click(System::Object^ sender, System::EventArgs^ e)
{
    listTicks->Items->Clear();
    m_dtBest5Ask->Clear();
    m_dtBest5Bid->Clear();
    m_dtBest10Ask->Clear();
    m_dtBest10Bid->Clear();

    gridBest5Bid->DataSource = m_dtBest5Bid;
    gridBest5Ask->DataSource = m_dtBest5Ask;

    gridBest5Ask->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest5Ask->Columns["m_nAskQty"]->Width = 60;
    gridBest5Ask->Columns["m_nAsk"]->HeaderText = "芥基";
    gridBest5Ask->Columns["m_nAsk"]->Width = 60;

    gridBest5Bid->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest5Bid->Columns["m_nAskQty"]->Width = 60;
    gridBest5Bid->Columns["m_nAsk"]->HeaderText = "禦基";
    gridBest5Bid->Columns["m_nAsk"]->Width = 60;

    gridBest10Bid->DataSource = m_dtBest10Bid;
    gridBest10Ask->DataSource = m_dtBest10Ask;

    gridBest10Ask->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest10Ask->Columns["m_nAskQty"]->Width = 60;
    gridBest10Ask->Columns["m_nAsk"]->HeaderText = "芥基";
    gridBest10Ask->Columns["m_nAsk"]->Width = 60;

    gridBest10Bid->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest10Bid->Columns["m_nAskQty"]->Width = 60;
    gridBest10Bid->Columns["m_nAsk"]->HeaderText = "禦基";
    gridBest10Bid->Columns["m_nAsk"]->Width = 60;

    short nPage = 0;

    if (short::TryParse(txtOSTickPage->Text->ToString(), nPage) == false)
        nPage = -1;

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestTicks(nPage, txtTick->Text->Trim());

    txtOSTickPage->Text = nPage.ToString();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestTicks");
}

Void CppCLITester::SKOSQuote::btnLiveTick_Click(System::Object^ sender, System::EventArgs^ e)
{
    listTicks->Items->Clear();

    short nPage = 0;

    if (short::TryParse(txtOSTickPage->Text->ToString(), nPage) == false)
        nPage = -1;

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestLiveTick(nPage, txtTick->Text->Trim());

    txtOSTickPage->Text = nPage.ToString();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestLiveTick");
}

Void CppCLITester::SKOSQuote::btnMarketDepth_Click(System::Object^ sender, System::EventArgs^ e)
{
    m_dtBest5Ask->Clear();
    m_dtBest5Bid->Clear();
    m_dtBest10Ask->Clear();
    m_dtBest10Bid->Clear();

    gridBest5Bid->DataSource = m_dtBest5Bid;
    gridBest5Ask->DataSource = m_dtBest5Ask;

    gridBest5Ask->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest5Ask->Columns["m_nAskQty"]->Width = 60;
    gridBest5Ask->Columns["m_nAsk"]->HeaderText = "芥基";
    gridBest5Ask->Columns["m_nAsk"]->Width = 60;

    gridBest5Bid->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest5Bid->Columns["m_nAskQty"]->Width = 60;
    gridBest5Bid->Columns["m_nAsk"]->HeaderText = "禦基";
    gridBest5Bid->Columns["m_nAsk"]->Width = 60;

    gridBest10Bid->DataSource = m_dtBest10Bid;
    gridBest10Ask->DataSource = m_dtBest10Ask;

    gridBest10Ask->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest10Ask->Columns["m_nAskQty"]->Width = 60;
    gridBest10Ask->Columns["m_nAsk"]->HeaderText = "芥基";
    gridBest10Ask->Columns["m_nAsk"]->Width = 60;

    gridBest10Bid->Columns["m_nAskQty"]->HeaderText = "眎计";
    gridBest10Bid->Columns["m_nAskQty"]->Width = 60;
    gridBest10Bid->Columns["m_nAsk"]->HeaderText = "禦基";
    gridBest10Bid->Columns["m_nAsk"]->Width = 60;

    short nPage = 0;

    if (short::TryParse(txtOSTickPage->Text->ToString(), nPage) == false)
        nPage = -1;

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestMarketDepth(nPage, txtTick->Text->Trim());

    txtOSTickPage->Text = nPage.ToString();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestMarketDepth");
}

Void CppCLITester::SKOSQuote::btnGetTick_Click(System::Object^ sender, System::EventArgs^ e)
{
    int nStockidx;
    int nPtr;

    if (int::TryParse(txtTickStockidx->Text, nStockidx) == false)
        return;

    if (int::TryParse(txtTickPtr->Text, nPtr) == false)
        return;

    SKCOMLib::SKFOREIGNTICK_9 skTick;

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_GetTickNineDigitLONG(nStockidx, nPtr, skTick);

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_GetTickNineDigitLONG");

    if (m_nCode == 0)
    {
        lblGetTick->Text = skTick.nTime.ToString() + "/" + skTick.nClose.ToString() + "/" + skTick.nQty.ToString();
    }
}

Void CppCLITester::SKOSQuote::btnGetBest5_Click(System::Object^ sender, System::EventArgs^ e)
{
    int nStockidx;

    if (int::TryParse(txtBestStockidx->Text, nStockidx) == false)
        return;

    SKCOMLib::SKBEST5_9 skBest5;

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_GetBest5NineDigitLONG(nStockidx, skBest5);

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_GetBest5NineDigitLONG");

    if (m_nCode == 0)
    {
        lblBest5Bid->Text = skBest5.nBid1.ToString() + "/" + skBest5.nBidQty1.ToString() + " " + skBest5.nBid2.ToString() + "/" + skBest5.nBidQty2.ToString() + " ...";

        lblBest5Ask->Text = skBest5.nAsk1.ToString() + "/" + skBest5.nAskQty1.ToString() + " " + skBest5.nAsk2.ToString() + "/" + skBest5.nAskQty2.ToString() + " ...";
    }
}

Void CppCLITester::SKOSQuote::btnKLine_Click(System::Object^ sender, System::EventArgs^ e)
{
    listKLine->Items->Clear();

    System::String^ strStock = "";
    short nType = 0;

    strStock = txtKLine->Text->Trim();
    nType = short::Parse(boxKLineType->SelectedIndex.ToString());

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestKLine(strStock, nType);

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestKLine");
}

Void CppCLITester::SKOSQuote::btnKLineByDate_Click(System::Object^ sender, System::EventArgs^ e)
{
    listKLine->Items->Clear();

    System::String^ strStock = "";
    short nType = 0;
    System::String^ strStartDate = "";
    System::String^ strEndDate = "";

    strStock = txtKLine->Text->Trim();
    strStartDate = txtStartDate->Text->Trim();
    strEndDate = txtEndDate->Text->Trim();
    nType = short::Parse(boxKLineType->SelectedIndex.ToString());

    short sMinuteNumber;

    if (short::TryParse(txtMinuteNumber->Text, sMinuteNumber) == false)
    {
        sMinuteNumber = 0;
    }


    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestKLineByDate(strStock, nType, strStartDate, strEndDate, sMinuteNumber);
    //int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestKLineByDate(strStock, nType, strStartDate, strEndDate);

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestKLineByDate");
}

Void CppCLITester::SKOSQuote::btnOverseaProducts_Click(System::Object^ sender, System::EventArgs^ e)
{
    listOverseaProducts->Items->Clear();

    int m_nCode = m_pSKOSQuote->SKOSQuoteLib_RequestOverseaProducts();

    GetMessage("OSQuote", m_nCode, "SKOSQuoteLib_RequestOverseaProducts");
}

DataTable^ CppCLITester::SKOSQuote::CreateStocksDataTable()
{
    DataTable^ myDataTable = gcnew DataTable();

    DataColumn^ myDataColumn;

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nStockidx";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int16");
    myDataColumn->ColumnName = "m_sDecimal";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nDenominator";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_cMarketNo";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_caExchangeNo";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_caExchangeName";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_caStockNo";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.String");
    myDataColumn->ColumnName = "m_caStockName";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nOpen";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nHigh";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nLow";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nClose";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_dSettlePrice";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nTickQty";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nRef";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nBid";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nBc";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nAsk";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nAc";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int64");
    myDataColumn->ColumnName = "m_nTQty";
    myDataTable->Columns->Add(myDataColumn);

    // 砞﹚ DataTable PrimaryKey
    array<System::Data::DataColumn^>^ colarray = gcnew array<System::Data::DataColumn^>(1);
    colarray[0] = myDataTable->Columns["m_nStockidx"];
    myDataTable->PrimaryKey = colarray;

    return myDataTable;
}

DataTable^ CppCLITester::SKOSQuote::CreateBest5AskTable()
{
    DataTable^ myDataTable = gcnew DataTable();

    DataColumn^ myDataColumn;

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Int32");
    myDataColumn->ColumnName = "m_nAskQty";
    myDataTable->Columns->Add(myDataColumn);

    myDataColumn = gcnew DataColumn();
    myDataColumn->DataType = Type::GetType("System.Double");
    myDataColumn->ColumnName = "m_nAsk";
    myDataTable->Columns->Add(myDataColumn);

    return myDataTable;
}
