import re

with open(r"d:\project\GameHTTH\temp_db.sql", "r", encoding="utf-16", errors="ignore") as f:
    old_db = f.read()

with open(r"d:\project\GameHTTH\htth_project\database\db.sql", "r", encoding="utf-8", errors="ignore") as f:
    new_db = f.read()

def get_columns(schema):
    matches = re.finditer(r'CREATE TABLE (\w+) \((.*?)\) ENGINE=', schema, re.DOTALL)
    tables = {}
    for match in matches:
        table_name = match.group(1)
        columns = [c.strip() for c in match.group(2).split('\n') if c.strip().startswith('')]
        tables[table_name] = set([c.split('')[1] for c in columns if '' in c])
    return tables

old_tables = get_columns(old_db)
new_tables = get_columns(new_db)

for table, old_cols in old_tables.items():
    if table in new_tables:
        new_cols = set(new_tables[table])
        missing_in_new = old_cols - new_cols
        if missing_in_new:
            print(f"Table {table} is missing columns from CodeTemp: {missing_in_new}")
