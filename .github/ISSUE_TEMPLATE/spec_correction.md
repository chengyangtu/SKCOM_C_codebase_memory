---
name: 規格勘誤 / Spec Correction
about: 回報 api_spec/ 內容錯誤、遺漏，或與官方文件／範例碼不一致之處
title: "[規格勘誤] "
labels: spec-correction
---

詳細流程說明見 [CONTRIBUTING.md 的「規格勘誤怎麼提」](../../CONTRIBUTING.md#規格勘誤怎麼提)。

## 函式 / 章節名稱

（例如：`SKOrderLib_SendFutureOrderCLR`，或章節「SKQuoteLib 陷阱與注意 第 3 點」）

## 出處

- 官方原文：`api_spec/_raw/<檔名>.md` 第 ___ 行（或官方 docx 手冊名稱＋頁碼）
- 範例碼（如適用）：`Source_code/.../*.cs:行號`
- 目前規格檔位置：`api_spec/modules/<檔名>.md` 或 `api_spec/flows/<檔名>.md` 第 ___ 行

## 目前規格寫法（有問題的內容）

## 正確內容（建議修改為）

## 判斷依據

（為什麼認為目前寫法有誤——例如與官方原文矛盾、與範例碼實際行為不符、實測結果等）

---

- [ ] 我願意直接送 PR 修正（而非只回報）
- [ ] 此問題屬於已知陷阱的更新／修正，對應 `modules/*.md` 已有「陷阱與注意」條目
