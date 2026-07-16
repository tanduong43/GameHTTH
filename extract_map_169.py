import io

input_file = r'd:\project\GameHTTH\htth_project\database\htth_truongbk.sql'
output_file = r'd:\project\GameHTTH\new_map_169_modified.sql'

with io.open(input_file, 'r', encoding='utf-8') as f:
    for line in f:
        if line.startswith("INSERT INTO `maps` VALUES (169,"):
            # Replace the ID and name placeholders
            line = line.replace("VALUES (169, 'Khu vực 3',", "VALUES (999, 'Tên Map Mới',")
            
            # Replace the specific vgos array with '[]'
            line = line.replace("'[[167,36,12,348,168],[172,1452,168,84,12],[171,132,168,1548,12]]'", "'[]'")
            
            with io.open(output_file, 'w', encoding='utf-8') as out_f:
                out_f.write('-- Lưu ý: Hãy thay đổi số 999 thành ID map mới và Tên Map Mới thành tên bạn muốn\n')
                out_f.write(line)
            print(f'Successfully wrote the modified query to {output_file}')
            break
