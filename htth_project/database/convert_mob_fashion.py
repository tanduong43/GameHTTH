import re
import json
import random

sql_path = "database.sql"
out_path = "new_fashions.sql"

def main():
    try:
        with open(sql_path, "r", encoding="utf-8") as f:
            sql_text = f.read()
    except FileNotFoundError:
        print(f"File {sql_path} not found.")
        return

    # Parse itemoption
    # Example: (1,'Tăng tấn công',0,1)
    option_pattern = re.compile(r"\((\d+),'([^']*)',\d+,(\d+)\)")
    options_dict = {}
    
    # We find the itemoption insert
    opt_start = sql_text.find("INSERT INTO `itemoption` VALUES")
    if opt_start != -1:
        opt_end = sql_text.find(";", opt_start)
        opt_chunk = sql_text[opt_start:opt_end]
        for match in option_pattern.finditer(opt_chunk):
            opt_id = int(match.group(1))
            opt_name = match.group(2)
            opt_percent = int(match.group(3))
            
            # Filter valid options
            # Avoid some weird options like 23 (+ HP/Thức ăn), 28, 29, 30, etc.
            if opt_percent == 1 and opt_id not in [23, 24, 28, 29, 30, 32, 33, 34, 35]:
                options_dict[opt_id] = opt_name

    pattern = re.compile(r"\((\d+),'([^']*)',\d+,\d+,\d+,\d+,1,\d+,'(\[1,[^']+\])','[^']*',\d+\)")
    
    matches = pattern.findall(sql_text)
    
    start_id = 200
    fashion_inserts = [
        "DELETE FROM `fashiontemplate` WHERE `id` >= 200;\n",
        "INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES "
    ]
    values_list = []
    
    opt_keys = list(options_dict.keys())
    
    for match in matches:
        mob_id = match[0]
        mob_name = match[1].replace("'", "''") # escape quotes
        idicon_str = match[2]
        
        try:
            idicon = json.loads(idicon_str)
            if len(idicon) == 4 and idicon[0] == 1:
                head = idicon[1]
                hair = idicon[2]
                wearing = idicon[3]
                
                if len(wearing) == 6:
                    mwear = wearing + [head, hair]
                    icon = 15
                    
                    # Random options
                    chosen_opts = random.sample(opt_keys, 2)
                    op_list = []
                    info_lines = [f"Thời trang {mob_name}"]
                    
                    for opt_id in chosen_opts:
                        # 5% to 10% -> 50 to 100
                        val_pct = random.randint(5, 10)
                        val_points = val_pct * 10
                        op_list.append([opt_id, val_points])
                        info_lines.append(f"+{val_pct}% {options_dict[opt_id]}")
                    
                    info_lines.append("Hạn sử dụng vĩnh viễn")
                    info = "\\n".join(info_lines)
                    
                    op_str = json.dumps(op_list, separators=(',', ':'))
                    price = 1
                    
                    val = f"({start_id},{icon},'{mob_name}','{info}','{op_str}','{op_str}',{price})"
                    # Wait, in fashiontemplate, the 6th field is op.
                    # val format: (id, icon, name, info, mwear, op, price)
                    val = f"({start_id},{icon},'{mob_name}','{info}','{json.dumps(mwear, separators=(',', ':'))}','{op_str}',{price})"
                    values_list.append(val)
                    start_id += 1
        except Exception as e:
            print(f"Error parsing mob {mob_name}: {e}")

    if values_list:
        fashion_inserts.append(",\n".join(values_list) + ";\n")
        with open(out_path, "w", encoding="utf-8") as f:
            f.write("".join(fashion_inserts))
        print(f"Successfully generated {len(values_list)} fashion items in {out_path}.")
    else:
        print("No human mobs found.")

if __name__ == "__main__":
    main()
