with open(r"d:\project\GameHTTH\htth_project\database\db.sql", "r", encoding="utf-8") as f:
    content = f.read()

if "codetemp_data" not in content:
    content = content.replace("
um_phao_hoa int NOT NULL DEFAULT '0',", "
um_phao_hoa int NOT NULL DEFAULT '0',\n  codetemp_data text,")
    with open(r"d:\project\GameHTTH\htth_project\database\db.sql", "w", encoding="utf-8") as f:
        f.write(content)
    print("Added codetemp_data to db.sql")
else:
    print("codetemp_data already exists")
