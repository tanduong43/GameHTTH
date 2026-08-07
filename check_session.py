import re
with open(r"d:\project\GameHTTH\htth_project\src\main\java\io\Session.java", "r", encoding="utf-8") as f:
    content = f.read()

for i, line in enumerate(content.split('\n')):
    if 'tongnap' in line:
        print(f"Line {i+1}: {line.strip()}")
