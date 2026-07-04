# SKCOM — 群益 CapitalAPI 的 AI 可讀規格庫

群益期貨 CapitalAPI（策略王 COM 元件，v2.13.57）規格庫，供 AI 輔助開發使用。

- 規格庫入口：`api_spec/README.md`；原文：`api_spec/_raw/`（`tools/extract_docx.py` 可重抽）
- API 查詢優先序：codebase-memory 圖譜（`search_graph`，規格 Section＋官方範例 Method 雙軌可查）→ `api_spec/modules/*.md` → `api_spec/flows/*.md` → `api_spec/_raw/`
- 下單相關操作涉及真實金流，任何生成的程式碼先在模擬環境測試
- 本庫非官方文件，內容以群益期貨官方公告為準；已知未盡完善之處記錄於各 `modules/*.md` 的「陷阱與注意」節
