import os
import re

additions = {
    "client\\Player.java": "\n    public byte[] MissionCheck = new byte[10];\n    public int num1;\n    public int num2;\n    public int num3;\n    public int num4;\n    public int num5;\n    public int tieu_ruby;\n    public byte[] tichTieuRubyCheck = new byte[10];\n    public byte[] tichTieuCheck = new byte[10];\n    public byte danhhieu;\n    public int id_danh_hieu_su_dung;\n    public java.util.List<Integer> id_danh_hieu_da_so_huu = new java.util.ArrayList<>();\n",
    "io\\Session.java": "\n    public int tongnap;\n",
    "map\\Map.java": "\n    public template.VuonCam vuonCam;\n"
}

codeTemp = r"d:\project\GameHTTH\CodeTemp"
src = r"d:\project\GameHTTH\htth_project\src\main\java"

for rel_path, code in additions.items():
    f1 = os.path.join(src, rel_path)
    with open(f1, 'r', encoding='utf-8', errors='ignore') as f:
        content = f.read()
    
    last_brace_idx = content.rfind('}')
    if last_brace_idx != -1:
        new_content = content[:last_brace_idx] + "\n\n" + code + "\n" + content[last_brace_idx:]
        with open(f1, 'w', encoding='utf-8') as f:
            f.write(new_content)
        print(f"Injected fields into {rel_path}")

# Add check_id_danhhieu implementation
methods_for_player = """
    public boolean check_id_danhhieu(int ids) {
        for (int idd : id_danh_hieu_da_so_huu) {
            if (idd == ids) {
                return true;
            }
        }
        return false;
    }
"""
f_player = os.path.join(src, "client\\Player.java")
with open(f_player, 'r', encoding='utf-8') as f:
    content = f.read()
last_brace = content.rfind('}')
new_content = content[:last_brace] + methods_for_player + content[last_brace:]
with open(f_player, 'w', encoding='utf-8') as f:
    f.write(new_content)
print("Injected check_id_danhhieu into Player.java")
