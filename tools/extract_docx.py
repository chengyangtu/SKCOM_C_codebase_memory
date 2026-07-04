#!/usr/bin/env python3
"""群益 CapitalAPI docx 手冊 → Markdown 純文字抽取器。

用法：python3 tools/extract_docx.py
輸入：Source_code/**/*.docx
輸出：api_spec/_raw/<檔名>.md（段落 + Markdown 表格，供 AI/人閱讀與後續規格結構化）
"""
import re
import zipfile
from pathlib import Path
from xml.etree import ElementTree as ET

W = "{http://schemas.openxmlformats.org/wordprocessingml/2006/main}"
MC = "{http://schemas.openxmlformats.org/markup-compatibility/2006}"

ROOT = Path(__file__).resolve().parent.parent
SRC = ROOT / "Source_code"
OUT = ROOT / "api_spec" / "_raw"


def strip_fallbacks(el):
    """移除 mc:Fallback，避免 AlternateContent（如文字方塊）內容重複抽出。"""
    for parent in el.iter():
        for child in list(parent):
            if child.tag == f"{MC}Fallback":
                parent.remove(child)


def para_text(p):
    return "".join(t.text or "" for t in p.iter(f"{W}t"))


def para_style(p):
    ppr = p.find(f"{W}pPr")
    if ppr is not None:
        st = ppr.find(f"{W}pStyle")
        if st is not None:
            return st.get(f"{W}val", "")
    return ""


def heading_prefix(style):
    m = re.match(r"(?:Heading|標題)\s*([1-6])", style or "")
    return "#" * (int(m.group(1)) + 1) + " " if m else ""


def cell_text(tc):
    parts = [para_text(p).strip() for p in tc.findall(f"{W}p")]
    return " / ".join(x for x in parts if x).replace("|", "\\|")


def table_md(tbl):
    lines = []
    rows = tbl.findall(f"{W}tr")
    for i, tr in enumerate(rows):
        cells = [cell_text(tc) for tc in tr.findall(f"{W}tc")]
        lines.append("| " + " | ".join(cells) + " |")
        if i == 0:
            lines.append("|" + "---|" * len(cells))
    return lines


def extract(docx_path):
    z = zipfile.ZipFile(docx_path)
    root = ET.fromstring(z.read("word/document.xml"))
    strip_fallbacks(root)
    body = root.find(f"{W}body")
    out = []
    for child in body:
        tag = child.tag
        if tag == f"{W}p":
            text = para_text(child).strip()
            if text:
                out.append(heading_prefix(para_style(child)) + text)
        elif tag == f"{W}tbl":
            out.extend(table_md(child))
            out.append("")
    return "\n".join(out) + "\n"


def main():
    OUT.mkdir(parents=True, exist_ok=True)
    for docx in sorted(SRC.rglob("*.docx")):
        if "~$" in docx.name:  # Word 暫存檔
            continue
        md = OUT / (docx.stem + ".md")
        content = f"# {docx.stem}\n\n> 來源：{docx.relative_to(ROOT)}\n\n" + extract(docx)
        md.write_text(content, encoding="utf-8")
        print(f"{len(content.splitlines()):6d} 行  {md.name}")


if __name__ == "__main__":
    main()
