import sys
sys.stdout.reconfigure(encoding='utf-8')

s = open(r'd:\project\GameHTTH\htth_project\database\database.sql', encoding='utf-8').read()
opts = [x for x in s.split('\n') if 'INSERT INTO' in x and 'option' in x.lower()]

with open("options_list.txt", "w", encoding="utf-8") as f:
    for op in opts[:100]:
        f.write(op + "\n")
