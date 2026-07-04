# 開發流程總覽

四條端到端流程，AI 照著即可寫出可運作的群益 CapitalAPI 程式。建議閱讀順序：A → D → B/C（登入與回報是一切前提）。

| 流程 | 檔案 | 涵蓋 | 難度 |
|---|---|---|---|
| A. 初始化與登入 | [flows/A-login.md](flows/A-login.md) | COM 環境 → SKReplyLib 事件註冊（**登入前必做**）→ SKCenterLib_Login → ConnectByID → 兩階段連線確認 | ★ |
| B. 國內期選下單 | [flows/B-future-order.md](flows/B-future-order.md) | 下單準備 → FUTUREORDER 建構 → SendFutureOrder(CLR) 同步/非同步 → 回報比對 → 刪改單 | ★★★ |
| C. 報價訂閱三型 | [flows/C-quotes.md](flows/C-quotes.md) | 國內 SKQuoteLib／海期 SKOSQuoteLib／海選 SKOOQuoteLib：EnterMonitor → 連線事件 → 訂閱 → 接收 | ★★ |
| D. 回報處理 | [flows/D-reply.md](flows/D-reply.md) | SKReplyLib 事件全覽 → OnNewData 49 欄解析 → 委託狀態機 → 斷線重連 | ★★ |

每條流程均含：前置條件（引用 [modules/](modules/) 規格節名）、步驟總表、最小可運作 C# 骨架（標註官方範例來源 path:line）、Mermaid 時序圖、常見錯誤（代碼指向 [error_codes.md](error_codes.md)）。
