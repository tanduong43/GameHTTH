import re
import json
def main():
    content = open('htth_truongbk.sql', encoding='utf-8', errors='ignore').read()
    inserts = re.findall(r'INSERT INTO `maps` VALUES \((.*?)\);', content)
    print("Found", len(inserts), "inserts")
    for ins in inserts:
        # Simple parser for sql values row:
        # fields are separated by commas, but strings can contain commas
        # Let's parse with a simple regex for integers, floats, NULL, and single quoted strings
        fields = []
        pattern = re.compile(r"'(.*?)'(?=\s*,|\s*\)$)|NULL|(\d+)")
        # Actually let's just parse the NPCs JSON using regex directly from the insert string
        # NPCs field starts with '[[ or '[
        m = re.search(r"'({\s*\"|\[\s*\[.*?\]\s*\])'", ins)
        if not m:
            m = re.search(r"'(\[.*?\])'", ins)
        if m:
            npcs_str = m.group(1)
            # Find the map ID which is the first number in ins
            map_id = ins.split(',')[0].strip()
            # Find map name which is the second field
            map_name = ins.split(',')[1].strip("' ")
            
            # Try to locate the JSON arrays inside the single quotes
            # Let's find all single quoted strings in ins
            all_str = re.findall(r"'(.*?)'", ins)
            for s in all_str:
                if s.startswith('[[') and s.endswith(']]'):
                    try:
                        # Clean escaped quotes
                        clean_s = s.replace('\\"', '"').replace("\\'", "'")
                        npcs_json = json.loads(clean_s)
                        for npc in npcs_json:
                            if isinstance(npc, list) and len(npc) > 2:
                                npc_id = npc[0]
                                npc_name = npc[2]
                                if isinstance(npc_id, int) and npc_id < 0:
                                    print(f"Map {map_id} ({map_name}): NPC {npc_id} = {npc_name}")
                    except Exception as e:
                        # print("Err", e, s[:100])
                        pass
if __name__ == '__main__':
    main()