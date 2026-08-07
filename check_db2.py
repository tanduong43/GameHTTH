import re

with open(r"d:\project\GameHTTH\htth_project\database\db.sql", "r", encoding="utf-8", errors="ignore") as f:
    content = f.read()

match = re.search(r'CREATE TABLE players \((.*?)\) ENGINE=', content, re.DOTALL)
if match:
    print(match.group(1))
