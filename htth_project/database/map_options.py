import json

s = open(r'd:\project\GameHTTH\htth_project\database\new_fashions.sql', encoding='utf-8', errors='ignore').read()
lines = s.split('\n')
mapping = {}
for line in lines:
    if 'INSERT' in line or '(' not in line:
        continue
    try:
        # (id, icon, name, info, mwear, op, price)
        # We can split by "," but inside strings it's hard.
        # Just find the info string and op string.
        start_info = line.find("','") + 3
        start_info = line.find("','", start_info) + 3
        end_info = line.find("','", start_info)
        info = line[start_info:end_info]
        
        op_start = line.rfind(",'") - 1
        while line[op_start] != "'":
            op_start -= 1
        op_end = line.rfind("',")
        op_str = line[op_start+1:op_end]
        
        ops = json.loads(op_str)
        info_lines = info.split('\\n')
        
        for i in range(len(ops)):
            op_id = ops[i][0]
            # info_lines[0] is title, info_lines[1] is first op, info_lines[2] is second op
            op_text = info_lines[i+1].split('% ')[1] if '% ' in info_lines[i+1] else info_lines[i+1]
            mapping[op_id] = op_text
    except Exception as e:
        pass

with open("mapping.txt", "w", encoding="utf-8") as f:
    for k in sorted(mapping.keys()):
        f.write(f'{k}: {mapping[k]}\n')
