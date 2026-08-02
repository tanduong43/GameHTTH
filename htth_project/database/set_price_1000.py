import os
import re

def main():
    base_dir = r"d:\project\GameHTTH\htth_project"
    sql_path = os.path.join(base_dir, "database", "new_fashions.sql")
    
    with open(sql_path, "r", encoding="utf-8") as f:
        content = f.read()
        
    # Replace the price ID (which is the last element in the tuple) to 1000
    # For example: ... ,1) -> ... ,1000)
    
    def replacer(match):
        return match.group(0)[:-2] + "1000)"
        
    # The regex matches the end of the tuple before the closing parenthesis.
    # E.g. '\[\[.*?\]\]',([-\d]+)\)
    pattern = re.compile(r"('\[\[.*?\]\]',)([-\d]+)\)")
    
    new_content = pattern.sub(r"\g<1>1000)", content)
    
    with open(sql_path, "w", encoding="utf-8") as f:
        f.write(new_content)
        
    print("Updated all prices to 1000.")

if __name__ == "__main__":
    main()
