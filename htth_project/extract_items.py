import re

sql_file = r"d:\project\GameHTTH\htth_project\htth_truongbk.sql"
out_file = r"C:\Users\admin\.gemini\antigravity\brain\3370c109-6b53-4243-bd71-071a9e4de540\item_list.md"

with open(sql_file, 'r', encoding='utf-8') as f:
    lines = f.readlines()

output = []
output.append("# 🎒 Danh Sách ID Vật Phẩm Game Hải Tặc Tý Hon")
output.append("> Bạn có thể dùng ID này để Nhập vào ô `ID Item` khi Tạo Giftcode hoặc lệnh `admin item`.\n")

output.append("## 📦 Vật phẩm tiêu dùng / Nguyên liệu (item4)")
output.append("| ID | Tên Vật Phẩm |")
output.append("|---|---|")
for line in lines:
    if line.startswith("INSERT INTO `item4` VALUES"):
        match = re.search(r"VALUES \((\d+),\s*'([^']+)'", line)
        if match:
            output.append(f"| **{match.group(1)}** | {match.group(2)} |")

output.append("\n## ⚔️ Trang Bị / Vũ Khí (item3)")
output.append("| ID | Tên Trang Bị |")
output.append("|---|---|")
for line in lines:
    if line.startswith("INSERT INTO `item3` VALUES"):
        match = re.search(r"VALUES \((\d+),\s*'([^']+)'", line)
        if match:
            output.append(f"| **{match.group(1)}** | {match.group(2)} |")

with open(out_file, 'w', encoding='utf-8') as f:
    f.write('\n'.join(output))

print("Done")
