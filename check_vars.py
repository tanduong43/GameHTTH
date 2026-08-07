import re
with open(r"d:\project\GameHTTH\htth_project\src\main\java\client\Player.java", "r", encoding="utf-8") as f:
    content = f.read()

lines = content.split('\n')
for i, line in enumerate(lines):
    if 'mission' in line.lower() or 'num' in line.lower():
        if 'public' in line or 'private' in line:
            print(f"Line {i+1}: {line.strip()}")
