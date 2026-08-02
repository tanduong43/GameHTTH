import os
import re

def main():
    base_dir = r"d:\project\GameHTTH\htth_project"
    sql_path = os.path.join(base_dir, "database", "new_fashions.sql")
    
    with open(sql_path, "r", encoding="utf-8") as f:
        content = f.read()
        
    def repl(match):
        old_icon = int(match.group(1))
        # 30000 -> 28500, etc.
        new_icon = 28500 + (old_icon - 30000)
        return f"{new_icon}"

    # The SQL format is (id, icon, ...)
    # Let's just do a regex replace for the second value in the tuple.
    new_content = re.sub(r"(?<=\(\d{3},)(30\d{3})(?=,')", repl, content)
    
    with open(sql_path, "w", encoding="utf-8") as f:
        f.write(new_content)
        
    print("Replaced 30xxx with 285xx in new_fashions.sql")

if __name__ == "__main__":
    main()
