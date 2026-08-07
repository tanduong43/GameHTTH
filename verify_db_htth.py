import os
import re

db_path = r"d:\project\GameHTTH\htth_project\database\db_htth.sql"
src_path = r"d:\project\GameHTTH\htth_project\src\main\java"

if not os.path.exists(db_path):
    print("db_htth.sql NOT FOUND!")
    exit()

with open(db_path, "r", encoding="utf-8", errors="ignore") as f:
    db_content = f.read()

# Extract table structures
table_schemas = {}
table_matches = re.finditer(r'CREATE TABLE ?(\w+)?\s*\((.*?)\)\s*ENGINE=', db_content, re.DOTALL | re.IGNORECASE)
for match in table_matches:
    tname = match.group(1).lower()
    cols_text = match.group(2)
    cols = set()
    for line in cols_text.split('\n'):
        line = line.strip()
        if line.startswith(''):
            cname = line.split('')[1].lower()
            cols.add(cname)
    table_schemas[tname] = cols

print(f"Parsed {len(table_schemas)} tables from db_htth.sql:")
for t in sorted(table_schemas.keys()):
    print(f" - {t} ({len(table_schemas[t])} columns)")

# Check specific crucial columns for players
if "players" in table_schemas:
    p_cols = table_schemas["players"]
    check_cols = ["codetemp_data", "id", "level", "exp", "site", "tichtieu_ruby", "danhhieu", "hangdong_stage"]
    print("\nChecking 'players' table key columns:")
    for c in check_cols:
        status = "FOUND" if c in p_cols else "MISSING"
        print(f" - {c}: {status}")

# Search for SELECT/UPDATE/INSERT in src to check table and column references
missing_report = []
for root, _, files in os.walk(src_path):
    for file in files:
        if file.endswith('.java'):
            filepath = os.path.join(root, file)
            with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
                code = f.read()
            
            # Match SELECT FROM table
            selects = re.finditer(r'FROM ?(\w+)?', code, re.IGNORECASE)
            for s in selects:
                tname = s.group(1).lower()
                if tname not in ['where', 'select', 'set', 'join', 'left', 'right', 'inner'] and tname not in table_schemas:
                    missing_report.append(f"[{file}] Table '{tname}' referenced in SQL query not found in db_htth.sql")

if missing_report:
    print("\nPotential issues found:")
    for rep in set(missing_report[:20]):
        print(" ", rep)
else:
    print("\nAll SQL table references in src code exist in db_htth.sql!")
