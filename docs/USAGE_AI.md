# 使用 AI 開發群益 API 應用

本指南說明如何利用 AI 輔助工具（Claude Code、Codex 等）快速理解與開發群益 CapitalAPI 應用。

## 方式一：零安裝 — 直接貼到 AI

無需任何安裝，將規格文件直接貼給任何 LLM。

1. **開啟 API 主文檔**  
   從本專案複製：
   - `api_spec/README.md` — API 總覽與核心流程

2. **複製所需模組說明**  
   根據開發需求，複製相關 Markdown：
   - `api_spec/modules/` — 各功能模組規格（如 futures_order、account 等）
   - `api_spec/flows/` — 完整業務流程說明（如「下單流程」、「帳務查詢」等）

3. **貼入 AI Prompt**  
   將文檔內容貼到 Claude Code、Gemini Code 或其他支援代碼生成的 LLM，要求生成相應的實作程式碼。

**優點**：簡單快速，無依賴安裝。  
**缺點**：無法跨文件聯繫（如「這個函式呼叫了哪個官方範例」），需手動查找參考代碼。

---

## 方式二：預建圖譜 — 結構化查詢與跨節點追蹤

安裝 codebase-memory-mcp 後，本專案的結構化知識圖會自動啟用，支援快速的符號追蹤與多維度查詢。

### 安裝步驟

1. **複製本專案**
   ```bash
   git clone https://github.com/your-org/SKCOM_C_codebase_memory.git
   cd SKCOM_C_codebase_memory
   ```

2. **安裝 codebase-memory-mcp**  
   按照 [codebase-memory-mcp 官方指南](https://github.com/DeusData/codebase-memory-mcp) 進行安裝。

3. **索引本倉庫（或還原預建快照）**  
   執行以下命令：
   ```bash
   codebase-memory-mcp cli index_repository '{"repo_path":"<your-clone-path>"}'
   ```
   
   - 若本目錄已包含 `.codebase-memory/graph.db.zst` 快照，系統會直接還原而非全量重掃（秒級完成）。
   - 若無快照，索引器會掃描 `api_spec/`、`Source_code/` 等目錄，建立完整的知識圖。

### 在 AI 工具中使用圖譜

在支援 MCP 的 AI 工具（Claude Code、Codex、Gemini CLI 等）中，使用以下查詢：

- **`search_graph`** — 按名稱或關鍵字搜尋規格節點與程式碼節點  
- **`trace_path`** — 追蹤調用關係（如「哪些官方範例呼叫了 SendFutureOrder」）  
- **`get_code_snippet`** — 取得程式碼片段的完整上下文

### 實例：查詢下單函式

**查詢**：「請查詢 SendFutureOrder 的規格與官方實現」

**圖譜回應**：
- **規格節點**：`api_spec/modules/futures_order.md` → 下單參數、回傳值、錯誤碼
- **C# 範例**：`Source_code/CapitalAPI_Csharp/OrderExample.cs:45-78` → 官方 C# 實現
- **C++ 範例**：`Source_code/CapitalAPI_Cpp/OrderExample.cpp:120-150` → 官方 C++ 實現
- **相關調用鏈**：其他函式呼叫 SendFutureOrder 的位置

**優點**：跨文件追蹤、快速定位、自動關聯規格+範例。  
**缺點**：需要 MCP 支援的 AI 工具，首次索引有初始成本。

---

## 快速選擇表

| 需求 | 推薦方式 | 備註 |
|------|--------|------|
| 快速原型、單一任務 | 方式一（零安裝） | 貼規格文檔，5 分鐘上手 |
| 完整的 API 應用開發 | 方式二（預建圖譜） | 需 10 分鐘設置，後續開發效率翻倍 |
| 追蹤官方範例程式碼實現 | 方式二（預建圖譜） | 必要，無法用方式一達成 |
| 跨模組流程設計 | 方式二（預建圖譜） | 圖譜自動關聯相關流程 |

---

## 進階：自訂與擴展

### 更新圖譜

若本倉庫更新了規格或範例：
```bash
codebase-memory-mcp cli index_repository '{"repo_path":"<your-clone-path>","force":true}'
```

### 檢查圖譜狀態

```bash
codebase-memory-mcp cli index_status '{"repo_path":"<your-clone-path>"}'
```

### 圖形化查詢（Web UI）

若安裝了圖形化界面（如 codebase-memory 配套的 Web UI），訪問 `http://localhost:9749` 可視化圖譜。

---

## 支援與問題

- 規格或範例疑問 → 查閱 `api_spec/` 或 `Source_code/` 中的文檔與範例
- 圖譜索引問題 → 參考 [codebase-memory-mcp 文檔](https://github.com/DeusData/codebase-memory-mcp)
- 倉庫問題 → 提交 Issue 或聯絡專案維護者
