import sys

def main():
    file_path = r"d:\project\GameHTTH\item_list.md"
    with open(file_path, 'r', encoding='utf-8') as f:
        lines = f.readlines()
    
    out_lines = []
    current_type = None
    
    for line in lines:
        if line.startswith("## 📦 Vật phẩm tiêu dùng / Nguyên liệu (item4)"):
            current_type = "4"
            out_lines.append(line)
        elif line.startswith("## ⚔️ Trang Bị / Vũ Khí (item3)"):
            current_type = "3"
            out_lines.append(line)
        elif line.startswith("| ID | Tên Vật Phẩm |"):
            out_lines.append("| Loại | ID | Tên Vật Phẩm |\n")
        elif line.startswith("| ID | Tên Trang Bị |"):
            out_lines.append("| Loại | ID | Tên Trang Bị |\n")
        elif line.startswith("|---|---|"):
            out_lines.append("|---|---|---|\n")
        elif line.startswith("| **") and current_type is not None:
            # Table row like: | **0** | beri |
            parts = line.split("|")
            if len(parts) >= 4:
                # new line
                new_line = f"| {current_type} |" + "|".join(parts[1:])
                out_lines.append(new_line)
            else:
                out_lines.append(line)
        else:
            out_lines.append(line)
            
    with open(file_path, 'w', encoding='utf-8') as f:
        f.writelines(out_lines)
    print("Done")

if __name__ == "__main__":
    main()
