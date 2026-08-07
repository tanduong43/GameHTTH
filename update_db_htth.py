with open(r'd:\project\GameHTTH\htth_project\database\db_htth.sql', 'r', encoding='utf-8') as f:
    content = f.read()

target = '
um_phao_hoa int NOT NULL DEFAULT \'0\','
replacement = '
um_phao_hoa int NOT NULL DEFAULT \'0\',\n  codetemp_data text,'

if 'codetemp_data' not in content:
    content = content.replace(target, replacement)
    with open(r'd:\project\GameHTTH\htth_project\database\db_htth.sql', 'w', encoding='utf-8') as f:
        f.write(content)
    print('Successfully added codetemp_data to db_htth.sql')
else:
    print('codetemp_data already present in db_htth.sql')
