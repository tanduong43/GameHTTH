"""
Effect 920 (Buff Thức Tỉnh Nika - Supreme Conqueror Haki Awakening)
Tạo animation từ bộ sprite Haki đỏ đen mới do người dùng cung cấp.
Bao gồm 7 frame:
  0..3: Tụ khí cuồn cuộn & chớp sét từ mặt đất
  4..6: Cột Haki xoắn ốc khổng lồ & bùng nổ Haki Bá Vương tỏa tia sét 360 độ
"""
import os
import sys
import struct
import numpy as np
from PIL import Image

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRIPT_DIR = r'D:\project\GameHTTH\htth_project'
BASE_DIR = os.path.join(SCRIPT_DIR, 'data', 'template', 'skill')
SRC_IMG = r'C:\Users\admin\.gemini\antigravity-ide\brain\962f8d45-51ec-4554-8c92-12988262654e\.user_uploaded\media_1790102874489.png'
SCRATCH = r'C:\Users\admin\.gemini\antigravity-ide\brain\962f8d45-51ec-4554-8c92-12988262654e\scratch'

def round4(val):
    return int(round(val / 4.0) * 4)

def extract_and_pack():
    im = Image.open(SRC_IMG).convert('RGBA')
    arr = np.array(im).astype(float)
    
    r, g, b = arr[:, :, 0], arr[:, :, 1], arr[:, :, 2]
    dev = np.maximum.reduce([np.abs(r - g), np.abs(g - b), np.abs(r - b)])
    brightness = (r + g + b) / 3.0

    # Background removal
    is_bg = (brightness >= 45) & (brightness <= 62) & (dev <= 6)
    arr_clean = arr.copy()
    arr_clean[is_bg, 3] = 0

    near_bg = (brightness >= 40) & (brightness <= 68) & (dev <= 10) & (~is_bg)
    arr_clean[near_bg, 3] = np.clip((dev[near_bg] / 8.0) * 255.0, 0, 255)
    
    clean_im = Image.fromarray(np.clip(arr_clean, 0, 255).astype(np.uint8))
    arr_c = np.array(clean_im)
    
    crops = [
        ('f0_small',   0, 215,  30, 250),
        ('f1_flame1',  0, 215, 270, 490),
        ('f2_burst1',  0, 215, 520, 740),
        ('f3_flame2',  0, 215, 760, 980),
        ('f4_spiral1', 200, 570,  20, 320),
        ('f5_surge',   200, 570, 330, 640),
        ('f6_supreme', 200, 570, 640, 1010),
    ]
    
    scale = 0.60
    sprites = []
    sprites_info = []
    
    for name, y0, y1, x0, x1 in crops:
        sub = arr_c[y0:y1, x0:x1]
        ys, xs = np.where(sub[:, :, 3] > 15)
        cr = sub[ys.min():ys.max()+1, xs.min():xs.max()+1]
        im_cr = Image.fromarray(cr)
        
        nw = round4(im_cr.width * scale)
        nh = round4(im_cr.height * scale)
        s_res = im_cr.resize((nw, nh), Image.Resampling.LANCZOS)
        sprites.append(s_res)
        sprites_info.append((name, nw, nh))
    
    # Pack tightly
    w6, h6 = sprites[6].size
    w4, h4 = sprites[4].size
    w5, h5 = sprites[5].size
    
    sw = round4(max(w6 + w4, w5 + sprites[3].size[0] + sprites[2].size[0] + sprites[1].size[0]) + 16)
    sh = round4(max(h6, h4) + max(h5, sprites[3].size[1] + sprites[0].size[1]) + 16)
    
    sheet_4x = Image.new('RGBA', (sw, sh), (0, 0, 0, 0))
    
    # Place positions
    # 6: (0, 0)
    # 4: (w6 + 4, 0)
    sheet_4x.paste(sprites[6], (0, 0), sprites[6])
    sheet_4x.paste(sprites[4], (w6 + 4, 0), sprites[4])
    
    y1 = max(h6, h4) + 4
    sheet_4x.paste(sprites[5], (0, y1), sprites[5])
    
    x_cur = w5 + 4
    sheet_4x.paste(sprites[3], (x_cur, y1), sprites[3])
    sheet_4x.paste(sprites[0], (x_cur, y1 + sprites[3].size[1] + 4), sprites[0])
    
    x_cur2 = x_cur + max(sprites[3].size[0], sprites[0].size[0]) + 4
    sheet_4x.paste(sprites[2], (x_cur2, y1), sprites[2])
    sheet_4x.paste(sprites[1], (x_cur2, y1 + sprites[2].size[1] + 4), sprites[1])
    
    positions = [
        (x_cur, y1 + sprites[3].size[1] + 4, sprites[0].size[0], sprites[0].size[1]), # 0
        (x_cur2, y1 + sprites[2].size[1] + 4, sprites[1].size[0], sprites[1].size[1]), # 1
        (x_cur2, y1, sprites[2].size[0], sprites[2].size[1]),                         # 2
        (x_cur, y1, sprites[3].size[0], sprites[3].size[1]),                          # 3
        (w6 + 4, 0, sprites[4].size[0], sprites[4].size[1]),                          # 4
        (0, y1, sprites[5].size[0], sprites[5].size[1]),                              # 5
        (0, 0, sprites[6].size[0], sprites[6].size[1]),                               # 6
    ]
    
    small_images = []
    for idx, (px, py, pw, ph) in enumerate(positions):
        small_images.append([idx, px // 4, py // 4, pw // 4, ph // 4])
        
    return sheet_4x, sprites, small_images

def build_data(small_images, sprites):
    # Frames: anchor each sprite at bottom center of the ground ring
    frames = []
    for i, sp in enumerate(sprites):
        w, h = sp.size
        # In 1x coordinates:
        dx = - (w // 2) // 4
        dy = - (h - 12) // 4
        frames.append([(dx, dy, i, 0, 1)]) # onTop = 1
        
    # Sequence: dynamic breathing Supreme Haki buff loop
    sequence = (
        [0, 0] +               # 0: Initial ground ripple surge
        [1, 1] +               # 1: Rising flame
        [2, 2] +               # 2: Lightning crackles
        [3, 3] +               # 3: Blazing flame
        [4, 4, 4] +            # 4: Spiral Haki pillar
        [5, 5, 5] +            # 5: Surging colossal pillar
        [6, 6, 6, 6] +         # 6: Supreme Haki explosion radiating outward 360°!
        [5, 5, 5] +            # 5: Surging pillar
        [4, 4, 4] +            # 4: Spiral pillar
        [6, 6, 6, 6] +         # 6: Second burst
        [5, 5, 5] +            # 5: Return to surge
        [3, 3]                 # 3: Loop transition
    )
    
    out = bytearray()
    out.append(len(small_images))
    for s in small_images:
        for v in s:
            out.append(v)
            
    out.extend(struct.pack('>h', len(frames)))
    for f in frames:
        out.append(len(f))
        for dx, dy, idSmall, flip, onTop in f:
            out.extend(struct.pack('>h', dx))
            out.extend(struct.pack('>h', dy))
            out.append(idSmall)
            out.append(flip)
            out.append(onTop)
            
    out.append(len(sequence))
    for s in sequence:
        out.extend(struct.pack('>h', s))
        
    out.append(0)
    for _ in range(3):
        out.append(1)
        out.append(0)
    out.extend(bytes([0, 0, 0]))
    return bytes(out)

def save_clean_indexed_png(im_rgba, out_path, n_colors=48):
    arr = np.array(im_rgba)
    alpha = arr[:, :, 3]
    is_trans = alpha < 15
    arr[is_trans] = [0, 0, 0, 0]
    
    rgb = Image.fromarray(arr[:, :, :3])
    rgb_q = rgb.quantize(colors=n_colors-1, method=Image.Quantize.MEDIANCUT, dither=Image.Dither.NONE)
    pal = rgb_q.getpalette()
    pal_data = pal[:(n_colors-1)*3] + [0, 0, 0] * (256 - (n_colors-1))
    
    q_arr = np.array(rgb_q)
    q_arr[is_trans] = 255
    
    out_im = Image.fromarray(q_arr, mode='P')
    out_im.putpalette(pal_data)
    out_im.info['transparency'] = 255
    
    out_path_norm = os.path.normpath(out_path)
    out_im.save(out_path_norm, optimize=True, compress_level=9)

def generate():
    sheet_4x, sprites, small_images = extract_and_pack()
    data_bytes = build_data(small_images, sprites)
    print(f"Data binary size: {len(data_bytes)} bytes")
    
    w4, h4 = sheet_4x.size
    w1, h1 = w4 // 4, h4 // 4
    
    zooms = {
        'x4': (sheet_4x, 48),
        'x3': (sheet_4x.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 64),
        'x2': (sheet_4x.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 96),
        'x1': (sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 128),
        'x0': (sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 128),
    }
    
    for z, (im_z, ncol) in zooms.items():
        img_dir = os.path.normpath(os.path.join(BASE_DIR, z, 'img'))
        dat_dir = os.path.normpath(os.path.join(BASE_DIR, z, 'data'))
        os.makedirs(img_dir, exist_ok=True)
        os.makedirs(dat_dir, exist_ok=True)
        
        img_path = os.path.join(img_dir, '920.png')
        save_clean_indexed_png(im_z, img_path, n_colors=ncol)
        
        dat_path = os.path.join(dat_dir, '920')
        with open(dat_path, 'wb') as f:
            f.write(data_bytes)
            
        img_sz = os.path.getsize(img_path)
        total_sz = img_sz + len(data_bytes)
        print(f"  [{z}] 920.png = {img_sz} bytes, total packet body = {total_sz} bytes (max 60000)")
        
    print("\n=== SUCCESS! Effect 920 deployed with new Supreme Conqueror Haki image! ===")

if __name__ == '__main__':
    generate()
