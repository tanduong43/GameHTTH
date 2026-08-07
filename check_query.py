with open(r"d:\project\GameHTTH\temp_player.java", "r", encoding="utf-16") as f:
    content = f.read()

lines = content.split('\n')
for i, line in enumerate(lines):
    if 'tieu_ruby' in line:
        print(f"Line {i+1}: {line.strip()}")
