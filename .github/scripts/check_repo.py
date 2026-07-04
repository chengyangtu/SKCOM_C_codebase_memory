#!/usr/bin/env python3
"""CI 用的規格庫完整性檢查。純標準庫實作，CI 不需安裝任何第三方套件。

用法：python3 .github/scripts/check_repo.py

檢查項目：
1. Markdown 基本檢查——每個 .md 檔至少有一個標題（# 開頭的行），且檔內站內相對連結的
   目標檔案／目錄必須存在（外部連結、錨點、mailto 不檢查）
2. api_spec/modules/ 至少包含 6 個預期的 lib 規格檔，且都非空
3. .codebase-memory/graph.db.zst 知識圖譜快照存在且非空
"""
import re
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent.parent

EXPECTED_MODULES = {
    "SKCenterLib.md",
    "SKOrderLib.md",
    "SKQuoteLib.md",
    "SKOSQuoteLib.md",
    "SKOOQuoteLib.md",
    "SKReplyLib.md",
}

# _raw/ 是官方文件逐字抽取、Source_code/ 不含 .md，兩者皆略過連結檢查（僅防禦性保留）。
SKIP_LINK_CHECK_DIRS = {"_raw", "Source_code"}

HEADING_RE = re.compile(r"(?m)^#{1,6}\s+\S")
LINK_RE = re.compile(r"\[[^\]]+\]\(([^)]+)\)")

errors = []


def check_link(md_file, target):
    if target.startswith(("http://", "https://", "mailto:", "#")):
        return  # 外部連結／純錨點不檢查存在性
    path_part = target.split("#", 1)[0]
    if not path_part:
        return
    resolved = (md_file.parent / path_part).resolve()
    if not resolved.exists():
        errors.append(f"[連結] {md_file.relative_to(ROOT)} 連結目標不存在：{target}")


def check_markdown():
    for md in sorted(ROOT.rglob("*.md")):
        if not md.is_file() or ".git" in md.parts:
            continue
        text = md.read_text(encoding="utf-8", errors="replace")
        if not HEADING_RE.search(text):
            errors.append(f"[標題] {md.relative_to(ROOT)} 找不到任何 Markdown 標題（# 開頭）")

        if set(md.parts) & SKIP_LINK_CHECK_DIRS:
            continue
        for target in LINK_RE.findall(text):
            check_link(md, target)


def check_modules():
    mod_dir = ROOT / "api_spec" / "modules"
    if not mod_dir.is_dir():
        errors.append(f"[模組] 找不到目錄 {mod_dir.relative_to(ROOT)}")
        return
    found = {f.name for f in mod_dir.glob("*.md")}
    missing = EXPECTED_MODULES - found
    if missing:
        errors.append(f"[模組] api_spec/modules/ 缺少預期檔案：{sorted(missing)}")
    for name in sorted(EXPECTED_MODULES & found):
        f = mod_dir / name
        if f.stat().st_size == 0:
            errors.append(f"[模組] {f.relative_to(ROOT)} 是空檔案")


def check_graph_snapshot():
    snapshot = ROOT / ".codebase-memory" / "graph.db.zst"
    if not snapshot.is_file():
        errors.append(f"[圖譜] 找不到 {snapshot.relative_to(ROOT)}")
    elif snapshot.stat().st_size == 0:
        errors.append(f"[圖譜] {snapshot.relative_to(ROOT)} 是空檔案")


def main():
    check_markdown()
    check_modules()
    check_graph_snapshot()
    if errors:
        print(f"發現 {len(errors)} 個問題：\n")
        for e in errors:
            print(f"  - {e}")
        sys.exit(1)
    print("規格庫完整性檢查通過。")


if __name__ == "__main__":
    main()
