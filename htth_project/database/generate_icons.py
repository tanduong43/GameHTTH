import os
import re
from PIL import Image

def shift_hue(image, amount):
    if image.mode != 'RGBA':
        image = image.convert('RGBA')
    
    # Split into channels
    r, g, b, a = image.split()
    
    # We can use HSV conversion, or simply a quick PIL hack
    # PIL doesn't have a direct hue shift for RGBA that preserves alpha easily without numpy
    # Let's do it per pixel or convert to HSV
    hsv_image = image.convert('HSV')
    h, s, v = hsv_image.split()
    
    # Shift hue
    # amount is 0-255
    h = h.point(lambda p: (p + amount) % 256)
    
    # Recombine
    hsv_shifted = Image.merge('HSV', (h, s, v))
    rgb_shifted = hsv_shifted.convert('RGB')
    
    # Re-apply alpha
    r, g, b = rgb_shifted.split()
    final_image = Image.merge('RGBA', (r, g, b, a))
    return final_image

def main():
    base_dir = r"d:\project\GameHTTH\htth_project"
    sql_path = os.path.join(base_dir, "database", "new_fashions.sql")
    
    with open(sql_path, "r", encoding="utf-8") as f:
        content = f.read()
        
    ids = []
    
    def replacer(match):
        item_id = int(match.group(1))
        # Start new IDs from 30000
        new_icon_id = 30000 + (item_id - 200)
        ids.append((item_id, new_icon_id))
        return f"({item_id},{new_icon_id},"
        
    new_content = re.sub(r"\((\d+),15,", replacer, content)
    
    with open(sql_path, "w", encoding="utf-8") as f:
        f.write(new_content)
        
    print(f"Updated SQL. Found {len(ids)} items.")
    
    icon_dirs = ["x1", "x2", "x3", "x4"]
    
    for x_dir in icon_dirs:
        dir_path = os.path.join(base_dir, "data", "icon", x_dir)
        source_icon = os.path.join(dir_path, "15.png")
        
        if not os.path.exists(source_icon):
            print(f"Source icon not found: {source_icon}")
            continue
            
        print(f"Processing directory: {x_dir}")
        img = Image.open(source_icon)
        
        for item_id, new_icon_id in ids:
            # Generate a pseudo-random hue shift amount based on item_id
            # 137 is a prime that distributes well across 256
            hue_shift = (item_id * 137) % 256
            
            new_img = shift_hue(img, hue_shift)
            
            out_path = os.path.join(dir_path, f"{new_icon_id}.png")
            new_img.save(out_path)
            
    print("Done generating icons!")

if __name__ == "__main__":
    main()
