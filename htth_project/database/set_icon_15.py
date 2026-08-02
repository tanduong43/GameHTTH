import os
import re

def main():
    base_dir = r"d:\project\GameHTTH\htth_project"
    sql_path = os.path.join(base_dir, "database", "new_fashions.sql")
    
    with open(sql_path, "r", encoding="utf-8") as f:
        content = f.read()
        
    # Replace the icon ID (which is the second element in the tuple) to 15
    # For example: (200,28500,'Hải tặc' -> (200,15,'Hải tặc'
    pattern = re.compile(r"\((\d+),\d+,'")
    
    new_content = pattern.sub(r"(\1,15,'", content)
    
    with open(sql_path, "w", encoding="utf-8") as f:
        f.write(new_content)
        
    print("Updated all icon IDs to 15.")

if __name__ == "__main__":
    main()
