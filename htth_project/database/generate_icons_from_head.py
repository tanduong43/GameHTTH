import os
import re
import json
import io
from PIL import Image

def get_head_png(part_msg_path):
    if not os.path.exists(part_msg_path):
        return None
    with open(part_msg_path, "rb") as f:
        data = f.read()
        
    idx = data.find(b'\x89PNG\r\n\x1a\n')
    if idx == -1:
        return None
        
    png_data = data[idx:]
    try:
        img = Image.open(io.BytesIO(png_data))
        return img
    except Exception:
        return None

def main():
    base_dir = r"d:\project\GameHTTH\htth_project"
    sql_path = os.path.join(base_dir, "database", "new_fashions.sql")
    
    with open(sql_path, "r", encoding="utf-8") as f:
        content = f.read()
        
    # We parse the VALUES lines
    # (id, icon, name, info, mwear, op, price)
    # e.g. (200,28500,'Hải tặc','...', '[63,11,-1,9,-1,10,8,-1]', '...', 1)
    
    pattern = re.compile(r"\((\d+),(\d+),'[^']*','[^']*','(\[[^\]]+\])'")
    
    items = []
    for match in pattern.finditer(content):
        item_id = int(match.group(1))
        icon_id = int(match.group(2))
        mwear_str = match.group(3)
        try:
            mwear = json.loads(mwear_str)
            head_id = mwear[6]
            items.append((item_id, icon_id, head_id))
        except Exception:
            pass

    print(f"Found {len(items)} items in SQL.")
    
    icon_dirs = ["x1", "x2", "x3", "x4"]
    
    for x_dir in icon_dirs:
        out_dir = os.path.join(base_dir, "data", "icon", x_dir)
        part_dir = os.path.join(base_dir, "data", "datafromsver", x_dir)
        
        if not os.path.exists(out_dir):
            os.makedirs(out_dir)
            
        print(f"Processing directory: {x_dir}")
        missing = 0
        
        for item_id, icon_id, head_id in items:
            part_msg_path = os.path.join(part_dir, f"{head_id}_msg_-39")
            
            img = get_head_png(part_msg_path)
            if img:
                # Crop the first frame if it's a spritesheet
                w, h = img.size
                # Assuming the first frame is a square of w x w
                if h > w:
                    img = img.crop((0, 0, w, w))
                
                out_path = os.path.join(out_dir, f"{icon_id}.png")
                
                # To be absolutely safe for J2ME clients:
                # Convert RGBA to standard P if there's transparency,
                # though RGB/RGBA usually works if no weird chunks are added.
                # Just save it as normal PNG without optimize
                img.save(out_path, "PNG", optimize=False)
            else:
                missing += 1
                
        print(f"Finished {x_dir}. Missing head parts: {missing}")

if __name__ == "__main__":
    main()
