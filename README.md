# SKCOM — 群益 CapitalAPI 的 AI 可讀規格庫

**讓任何人都能用 AI 開發群益證券/期貨 CapitalAPI（策略王 COM 元件）**。把 21 份官方 docx 手冊與官方範例碼，轉化為 AI 可直接消化的結構化規格與知識圖譜節點——這是一把公開的鏟子，給所有想挖礦的人。

## 📚 API 規格庫（核心交付）— [`api_spec/`](api_spec/)

- **[modules/](api_spec/modules/)** — 6 個 COM lib 完整規格（382 節）：SKCenterLib（登入）、SKOrderLib（下單全家族，211 節）、SKQuoteLib（國內報價）、SKOSQuoteLib（海期）、SKOOQuoteLib（海選）、SKReplyLib（回報）；每方法含簽名/參數表/回傳/版本陷阱/範例碼 path:line
- **[workflows.md](api_spec/workflows.md) + [flows/](api_spec/flows/)** — 四條端到端開發流程（登入/期選下單/報價訂閱/回報解析），含 Mermaid 時序圖與最小可運作 C# 骨架
- **[error_codes.md](api_spec/error_codes.md)** — 226 筆代碼對照（9 分類）
- **[_raw/](api_spec/_raw/)** — 21 份官方手冊全文（`tools/extract_docx.py` 抽取，可重跑）
- 品質：每模組經「生成 agent + 獨立對抗驗證 agent」雙重處理；用法見 [api_spec/README.md](api_spec/README.md)

> 版本 2.13.57 ｜ 非官方文件，以群益官方為準 ｜ 下單涉真實金流，先用模擬環境

## 🚀 給 AI 用的兩種方式

### 方式一：直接餵檔案（零安裝）
把 `api_spec/README.md` 或任一 `modules/*.md`、`flows/*.md` 貼給任何 LLM（ChatGPT/Claude/本地模型皆可）當 context，直接問怎麼呼叫某個函式、怎麼寫下單流程。

### 方式二：查詢預建知識圖譜（免重新索引）
本 repo 內建 [codebase-memory-mcp](https://github.com/DeusData/codebase-memory-mcp) 的預建圖譜快照 `.codebase-memory/graph.db.zst`——clone 下來後裝好該 MCP server、指到本資料夾，即可直接用 `search_graph` / `trace_path` / `get_code_snippet` 查詢，**不必等它重新掃描整個專案**。圖譜同時收錄「規格文件節點」與「官方範例程式碼節點」，一次查詢就能拿到函式簽名＋規格說明＋真實呼叫範例。

```bash
# 安裝 codebase-memory-mcp 後
codebase-memory-mcp cli index_repository '{"repo_path":"/path/to/SKCOM"}'
# 有 graph.db.zst 存在時會優先從快照還原，而非整個重新解析
```

## 授權

- 本專案原創內容（`api_spec/` 規格文字、`tools/`）：[MIT](LICENSE)
- `Source_code/` 與 `api_spec/_raw/`：群益期貨官方 CapitalAPI 範例與文件，著作權歸群益期貨所有，收錄僅供技術參考，散布前請自行確認符合官方授權條款

## 內容統計
- 6 模組規格 / 382 節 / 5,562 行
- 4 條端到端開發流程（含 Mermaid 時序圖）
- 226 筆錯誤碼對照
- 知識圖譜 11,700+ 節點、21,000+ 邊

## 如何貢獻

歡迎規格勘誤、補充與工具改善——詳見 [CONTRIBUTING.md](CONTRIBUTING.md)，涵蓋 Fork/PR 流程、規格勘誤如何附出處、如何重跑 `tools/extract_docx.py`、如何重建知識圖譜快照，以及 commit 訊息慣例。參與前請先閱讀 [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md)；安全或敏感問題的私下回報管道見 [SECURITY.md](SECURITY.md)。

## 安全與防濫用

- **Branch protection**：`main` 分支要求 PR 審查與 CI 通過後才能合併，不接受直接 push
- **PR 審查**：所有變更皆由 [CODEOWNERS](.github/CODEOWNERS) 指定的維護者審查後合併；外部 fork 送出的 PR 一樣會自動觸發 CI
- **CI 最小權限**：`.github/workflows/ci.yml` 只宣告 `contents: read`、使用 `pull_request`（而非有安全疑慮的 `pull_request_target`）、第三方 action 一律釘選完整 commit SHA，且**全程不需要任何 repo secret**——外部貢獻者的 PR 無法藉由 CI 觸碰到任何機密資訊
- 詳細安全回報方式見 [SECURITY.md](SECURITY.md)
