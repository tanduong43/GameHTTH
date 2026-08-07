import re

with open(r"d:\project\GameHTTH\htth_project\src\main\java\client\Player.java", "r", encoding="utf-8") as f:
    content = f.read()

# 1. Update query in flush()
query_match = re.search(r'(String query = "UPDATE players SET.*?WHERE id = "\s*\+\s*p\.id\s*\+\s*";")', content, re.DOTALL)
if query_match:
    old_query = query_match.group(1)
    new_query = old_query.replace('WHERE id = "', ', codetemp_data = ? WHERE id = "')
    content = content.replace(old_query, new_query)

# 2. Update ps.set calls in flush()
ps_match = re.search(r'(ps\.setInt\(32, p\.num_phao_hoa\);)', content)
if ps_match:
    old_ps = ps_match.group(1)
    new_ps = old_ps + """
            // Save codetemp_data
            org.json.simple.JSONObject ctData = new org.json.simple.JSONObject();
            
            org.json.simple.JSONArray mCheck = new org.json.simple.JSONArray();
            for(byte b : p.MissionCheck) mCheck.add(b);
            ctData.put("MissionCheck", mCheck);
            
            ctData.put("num1", p.num1);
            ctData.put("num2", p.num2);
            ctData.put("num3", p.num3);
            ctData.put("num4", p.num4);
            ctData.put("num5", p.num5);
            ctData.put("tieu_ruby", p.tieu_ruby);
            
            org.json.simple.JSONArray tCheck = new org.json.simple.JSONArray();
            for(byte b : p.tichTieuCheck) tCheck.add(b);
            ctData.put("tichTieuCheck", tCheck);
            
            org.json.simple.JSONArray trCheck = new org.json.simple.JSONArray();
            for(byte b : p.tichTieuRubyCheck) trCheck.add(b);
            ctData.put("tichTieuRubyCheck", trCheck);
            
            ctData.put("danhhieu", p.danhhieu);
            ctData.put("id_danh_hieu_su_dung", p.id_danh_hieu_su_dung);
            
            org.json.simple.JSONArray dh_sohuu = new org.json.simple.JSONArray();
            for(int idh : p.id_danh_hieu_da_so_huu) dh_sohuu.add(idh);
            ctData.put("id_danh_hieu_da_so_huu", dh_sohuu);
            
            ps.setNString(33, ctData.toJSONString());
"""
    content = content.replace(old_ps, new_ps)

# 3. Update setup() to load codetemp_data
setup_match = re.search(r'(this\.num_phao_hoa = rs\.getInt\("num_phao_hoa"\);)', content)
if setup_match:
    old_setup = setup_match.group(1)
    new_setup = old_setup + """
            // Load codetemp_data
            String ctStr = rs.getString("codetemp_data");
            if (ctStr != null && !ctStr.isEmpty()) {
                org.json.simple.JSONObject ctData = (org.json.simple.JSONObject) org.json.simple.JSONValue.parse(ctStr);
                if (ctData != null) {
                    if (ctData.containsKey("MissionCheck")) {
                        org.json.simple.JSONArray mCheck = (org.json.simple.JSONArray) ctData.get("MissionCheck");
                        for(int i=0; i<mCheck.size() && i<this.MissionCheck.length; i++) this.MissionCheck[i] = Byte.parseByte(mCheck.get(i).toString());
                    }
                    if (ctData.containsKey("num1")) this.num1 = Integer.parseInt(ctData.get("num1").toString());
                    if (ctData.containsKey("num2")) this.num2 = Integer.parseInt(ctData.get("num2").toString());
                    if (ctData.containsKey("num3")) this.num3 = Integer.parseInt(ctData.get("num3").toString());
                    if (ctData.containsKey("num4")) this.num4 = Integer.parseInt(ctData.get("num4").toString());
                    if (ctData.containsKey("num5")) this.num5 = Integer.parseInt(ctData.get("num5").toString());
                    if (ctData.containsKey("tieu_ruby")) this.tieu_ruby = Integer.parseInt(ctData.get("tieu_ruby").toString());
                    
                    if (ctData.containsKey("tichTieuCheck")) {
                        org.json.simple.JSONArray tCheck = (org.json.simple.JSONArray) ctData.get("tichTieuCheck");
                        for(int i=0; i<tCheck.size() && i<this.tichTieuCheck.length; i++) this.tichTieuCheck[i] = Byte.parseByte(tCheck.get(i).toString());
                    }
                    if (ctData.containsKey("tichTieuRubyCheck")) {
                        org.json.simple.JSONArray trCheck = (org.json.simple.JSONArray) ctData.get("tichTieuRubyCheck");
                        for(int i=0; i<trCheck.size() && i<this.tichTieuRubyCheck.length; i++) this.tichTieuRubyCheck[i] = Byte.parseByte(trCheck.get(i).toString());
                    }
                    if (ctData.containsKey("danhhieu")) this.danhhieu = Byte.parseByte(ctData.get("danhhieu").toString());
                    if (ctData.containsKey("id_danh_hieu_su_dung")) this.id_danh_hieu_su_dung = Integer.parseInt(ctData.get("id_danh_hieu_su_dung").toString());
                    if (ctData.containsKey("id_danh_hieu_da_so_huu")) {
                        org.json.simple.JSONArray dh_sohuu = (org.json.simple.JSONArray) ctData.get("id_danh_hieu_da_so_huu");
                        for(int i=0; i<dh_sohuu.size(); i++) this.id_danh_hieu_da_so_huu.add(Integer.parseInt(dh_sohuu.get(i).toString()));
                    }
                }
            }
"""
    content = content.replace(old_setup, new_setup)

with open(r"d:\project\GameHTTH\htth_project\src\main\java\client\Player.java", "w", encoding="utf-8") as f:
    f.write(content)
print("Updated Player.java")
