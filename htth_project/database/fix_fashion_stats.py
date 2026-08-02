import os
import re
import random

def main():
    base_dir = r"d:\project\GameHTTH\htth_project"
    sql_path = os.path.join(base_dir, "database", "new_fashions.sql")
    
    with open(sql_path, "r", encoding="utf-8") as f:
        content = f.read()
        
    OPTIONS = [
        (10, "Chí mạng"),
        (14, "Phản đòn"),
        (13, "Xuyên giáp"),
        (17, "Tăng HP"),
        (1, "Tăng tấn công"),
        (25, "Tốc độ hồi chiêu"),
        (12, "Né tránh"),
        (56, "Máu cuối")
    ]
    
    # We want to replace the `info` and `op` columns in the SQL.
    # The pattern matches: (id, icon, 'name', 'info', 'mwear', 'op', price)
    # The info string ends with 'Hạn sử dụng vĩnh viễn'
    
    def replacer(match):
        item_id = match.group(1)
        icon_id = match.group(2)
        name = match.group(3)
        mwear = match.group(4)
        price = match.group(5)
        
        # Pick 2 random options
        chosen_ops = random.sample(OPTIONS, 2)
        
        op1_id, op1_name = chosen_ops[0]
        op2_id, op2_name = chosen_ops[1]
        
        # Random values 5% to 10%
        val1 = random.randint(5, 10)
        val2 = random.randint(5, 10)
        
        # Info string
        new_info = f"Thời trang {name}\\n+{val1}% {op1_name}\\n+{val2}% {op2_name}\\nHạn sử dụng vĩnh viễn"
        
        # Op string
        new_op = f"[[{op1_id},{val1*10}],[{op2_id},{val2*10}]]"
        
        # Reconstruct the line
        return f"({item_id},{icon_id},'{name}','{new_info}','{mwear}','{new_op}',{price})"
        
    # The regex needs to carefully capture the fields
    # \( (\d+),(\d+),'([^']*)','[^']*','(\[[^\]]*\])','\[\[.*?\]\]',([-\d]+) \)
    pattern = re.compile(r"\((\d+),(\d+),'([^']*)','[^']*','(\[[^\]]*\])','\[\[.*?\]\]',([-\d]+)\)")
    
    new_content = pattern.sub(replacer, content)
    
    with open(sql_path, "w", encoding="utf-8") as f:
        f.write(new_content)
        
    print("Fixed stats for all fashions.")

if __name__ == "__main__":
    main()
