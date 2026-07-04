# 貢獻指南

感謝你願意協助改善這份群益 CapitalAPI AI 可讀規格庫。這個專案的價值完全來自準確性——規格哪裡錯了、漏了、跟官方文件或範例碼對不上，都歡迎回報或直接送 PR 修正。

參與前請先閱讀 [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md)。

## 貢獻流程（Fork → PR）

1. Fork 本專案到你自己的帳號
2. 從 `main` 切一個新分支，例如 `git checkout -b fix/skorderlib-return-code`
3. 修改後在本機驗證（見下方「重抽官方 docx」「重建知識圖譜」「本機驗證」）
4. Commit，訊息請遵循下方「Commit 訊息慣例」
5. Push 到你的 fork，對本專案的 `main` 開 PR
6. 依 [PR 模板](.github/PULL_REQUEST_TEMPLATE.md) 填寫檢查清單，等待 CI 通過與維護者審查

## 規格勘誤怎麼提

規格庫裡的每一句話都應該能追溯回官方原文或官方範例碼。發現規格內容與官方文件、或與 `Source_code/` 範例碼行為不一致時：

- **小修正**（錯字、格式、單一函式描述有誤）：直接開 PR，說明欄附上**出處**——對應的 `api_spec/_raw/<檔名>.md` 行號，或 `Source_code/.../*.cs:行號`
- **不確定怎麼改、或想先討論**：開 Issue，使用 [規格勘誤模板](.github/ISSUE_TEMPLATE/spec_correction.md)，同樣要附出處
- 修改 `api_spec/modules/*.md`、`api_spec/flows/*.md` 時，若牽涉「陷阱與注意」節，請以**新增**取代既有條目為主，除非能確認該陷阱已不存在

判斷優先序：**範例碼實際行為 > 官方文件原文 > 規格庫既有整理**。規格庫是對前兩者的摘要與重組，三者衝突時以前兩者為準。

## 重抽官方 docx（`tools/extract_docx.py`）

`api_spec/_raw/` 底下的檔案是用腳本從 `Source_code/**/*.docx` 逐字抽取出的段落與表格，正常情況下不需要手改。若官方更新了 docx 手冊、或抽取腳本本身需要修正重跑：

```bash
python3 tools/extract_docx.py
```

腳本會掃描 `Source_code/` 下所有 `.docx`，於 `api_spec/_raw/<檔名>.md` 覆寫輸出。重跑後請用 `git diff` 確認差異符合預期，再一併提交腳本改動（如有）與重新產生的 `_raw/*.md`。

**請勿**手動編輯 `_raw/*.md` 的抽取結果本身——那是官方文件的忠實轉錄；如果抽取有誤，要改的是 `tools/extract_docx.py` 這支腳本。

## 重建知識圖譜

`.codebase-memory/graph.db.zst` 是 [codebase-memory-mcp](https://github.com/DeusData/codebase-memory-mcp) 的預建索引快照，讓 clone 下來的人不必重新掃描整個專案即可查詢。**任何會改變 `api_spec/` 或 `Source_code/` 內容的 PR，都需要重建這個快照並一併 commit**，否則圖譜會跟規格文字脫節。

```bash
# 安裝 codebase-memory-mcp 後，在本專案根目錄執行（repo_path 換成你本機的 clone 路徑）
codebase-memory-mcp cli index_repository '{"repo_path":"/path/to/your/clone/SKCOM_C_codebase_memory","persistence":true}'
```

- `persistence:true` 會把索引結果壓縮輸出到 `.codebase-memory/graph.db.zst`，並更新同目錄下的 `artifact.json`（節點數／邊數統計）
- 執行前後用 `git status` 確認 `graph.db.zst` 有變動（檔案大小或內容雜湊改變）再一併加入 commit
- CI 只檢查快照檔**存在且非空**，不會重新驗證圖譜內容是否與最新規格同步，所以這一步需要貢獻者自律執行；審查者也會留意「改了 api_spec/ 卻沒動 graph.db.zst」的 PR

## 本機驗證（送出 PR 前）

CI 會跑的檢查可以先在本機執行一次，省去一次來回等待：

```bash
python3 .github/scripts/check_repo.py
```

這支腳本會做 Markdown 基本檢查（標題、站內相對連結是否存在）、確認六個模組規格檔存在且非空、確認知識圖譜快照存在。

## Commit 訊息慣例

```
<type>: <description>
```

常用 `type`：`feat`、`fix`、`docs`、`refactor`、`test`、`chore`、`ci`。規格類修正大多用 `docs` 或 `fix`，例如：

```
fix: SKOrderLib 修正 SendFutureOrderCLR 回傳碼說明錯誤
docs: 補充 SKQuoteLib psPageNo 陷阱說明
```

## PR 需通過 CI

PR 需通過 `.github/workflows/ci.yml` 的自動檢查才能合併。CI 只做唯讀的內容驗證，**不需要任何 repo secret**，因此對外部貢獻者的 PR 一樣會自動執行、無需維護者手動核准工作流程。

## 授權注意事項

- `api_spec/`（`_raw/` 除外）與 `tools/` 是本專案原創內容，MIT 授權，歡迎修改
- `Source_code/` 與 `api_spec/_raw/` 是群益期貨官方著作，收錄僅供技術參考——請勿在 PR 中提交任何其他來源、未經授權確認的第三方著作內容
- 詳見 [NOTICE.md](NOTICE.md) 與 [LICENSE](LICENSE)
