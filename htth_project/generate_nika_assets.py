import os
import sys
import struct
import math
import random
import numpy as np
from PIL import Image, ImageDraw, ImageFilter

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

# ==============================================================================
# DRAWING UTILITIES FOR HIGH-FIDELITY ANIME ART
# ==============================================================================

def draw_sparkle(d, cx, cy, r=10, color=(255, 255, 255, 255)):
    d.line([(cx - r, cy), (cx + r, cy)], fill=color, width=3)
    d.line([(cx, cy - r), (cx, cy + r)], fill=color, width=3)
    rs = max(2, int(r * 0.65))
    d.line([(cx - rs, cy - rs), (cx + rs, cy + rs)], fill=color, width=2)
    d.line([(cx - rs, cy + rs), (cx + rs, cy - rs)], fill=color, width=2)
    d.ellipse([cx - 2, cy - 2, cx + 2, cy + 2], fill=(255, 255, 255, 255))

def draw_tapered_haki_lightning(d, pts, glow_col=(255, 20, 60), core_col=(255, 255, 255), base_w=5):
    n = len(pts)
    if n < 2:
        return
    # Pass 1: Dark outer Haki shadow / void border
    for i in range(n - 1):
        t = i / float(n - 1)
        w_cur = max(1, int(round(base_w * (1.0 - t * 0.35))))
        p1, p2 = pts[i], pts[i+1]
        d.line([p1, p2], fill=(10, 0, 8, 255), width=w_cur + 5)
        r1 = (w_cur + 5) // 2
        d.ellipse([p1[0]-r1, p1[1]-r1, p1[0]+r1, p1[1]+r1], fill=(10, 0, 8, 255))
        d.ellipse([p2[0]-r1, p2[1]-r1, p2[0]+r1, p2[1]+r1], fill=(10, 0, 8, 255))

    # Pass 2: Glowing crimson Haki aura
    glow_rgba = (glow_col[0], glow_col[1], glow_col[2], 255)
    for i in range(n - 1):
        t = i / float(n - 1)
        w_cur = max(1, int(round(base_w * (1.0 - t * 0.35))))
        p1, p2 = pts[i], pts[i+1]
        d.line([p1, p2], fill=glow_rgba, width=w_cur + 2)
        r2 = (w_cur + 2) // 2
        d.ellipse([p1[0]-r2, p1[1]-r2, p1[0]+r2, p1[1]+r2], fill=glow_rgba)
        d.ellipse([p2[0]-r2, p2[1]-r2, p2[0]+r2, p2[1]+r2], fill=glow_rgba)

    # Pass 3: Blinding hot white core
    core_rgba = (core_col[0], core_col[1], core_col[2], 255)
    for i in range(n - 1):
        t = i / float(n - 1)
        w_cur = max(1, int(round(base_w * (1.0 - t * 0.35))))
        p1, p2 = pts[i], pts[i+1]
        w_core = max(1, w_cur - 1)
        d.line([p1, p2], fill=core_rgba, width=w_core)
        r3 = w_core // 2
        d.ellipse([p1[0]-r3, p1[1]-r3, p1[0]+r3, p1[1]+r3], fill=core_rgba)
        d.ellipse([p2[0]-r3, p2[1]-r3, p2[0]+r3, p2[1]+r3], fill=core_rgba)

def draw_cloud_cluster(d, cx, cy, scale=1.0):
    # Cluster of fluffy anime Hagoromo steam clouds
    base_circles = [
        (-22 * scale, 0, 16 * scale),
        (0, -6 * scale, 20 * scale),
        (22 * scale, 0, 16 * scale),
        (-12 * scale, 10 * scale, 15 * scale),
        (12 * scale, 10 * scale, 15 * scale)
    ]
    # Outline
    for ox, oy, r in base_circles:
        d.ellipse([cx+ox-r-2, cy+oy-r-2, cx+ox+r+2, cy+oy+r+2], fill=(180, 205, 235, 255))
    # Body
    for ox, oy, r in base_circles:
        d.ellipse([cx+ox-r, cy+oy-r, cx+ox+r, cy+oy+r], fill=(245, 250, 255, 255))
    # Highlights
    for ox, oy, r in base_circles:
        hr = int(r * 0.55)
        d.ellipse([cx+ox-hr, cy+oy-hr-2, cx+ox+hr, cy+oy], fill=(255, 255, 255, 255))

# ==============================================================================
# BINARY EFFECT BUILDER
# ==============================================================================
def build_data_effect(small_images, frames, sequence, frame_char=None, index_splash=None):
    out = bytearray()
    out.append(len(small_images))
    for s in small_images:
        out.append(s[0])
        out.append(s[1])
        out.append(s[2])
        out.append(s[3])
        out.append(s[4])
    
    out.extend(struct.pack('>h', len(frames)))
    for f in frames:
        out.append(len(f))
        for p in f:
            out.extend(struct.pack('>h', p[0]))
            out.extend(struct.pack('>h', p[1]))
            out.append(p[2])
            out.append(p[3] if len(p) > 3 else 0)
            out.append(p[4] if len(p) > 4 else 0)
    
    out.append(len(sequence))
    for s in sequence:
        out.extend(struct.pack('>h', s))
    
    out.append(0)
    if frame_char is None:
        frame_char = [[0], [0], [0]]
    for fc in frame_char:
        out.append(len(fc))
        for b in fc:
            out.append(b)
    
    if index_splash is None:
        index_splash = [0, 0, 0]
    out.extend(bytes(index_splash))
    return bytes(out)

def save_multizoom_effect(eff_id, img_x4, data_bytes):
    script_dir = os.path.dirname(os.path.abspath(__file__))
    base_dir = os.path.normpath(os.path.join(script_dir, 'data', 'template', 'skill'))
    w4, h4 = img_x4.size
    w1, h1 = w4 // 4, h4 // 4
    
    zooms = {
        'x4': img_x4,
        'x3': img_x4.resize((w1 * 3, h1 * 3), Image.Resampling.BILINEAR),
        'x2': img_x4.resize((w1 * 2, h1 * 2), Image.Resampling.BILINEAR),
        'x1': img_x4.resize((w1 * 1, h1 * 1), Image.Resampling.NEAREST),
        'x0': img_x4.resize((w1 * 1, h1 * 1), Image.Resampling.NEAREST)
    }
    
    for z, im in zooms.items():
        dir_img = os.path.normpath(os.path.join(base_dir, z, 'img'))
        dir_data = os.path.normpath(os.path.join(base_dir, z, 'data'))
        os.makedirs(dir_img, exist_ok=True)
        os.makedirs(dir_data, exist_ok=True)
        
        img_path = os.path.normpath(os.path.join(dir_img, f'{eff_id}.png'))
        im.save(img_path, format='PNG', optimize=True)
        sz = os.path.getsize(img_path)
        
        data_path = os.path.normpath(os.path.join(dir_data, f'{eff_id}'))
        with open(data_path, 'wb') as f:
            f.write(data_bytes)
    print(f"-> [OK] Effect ID {eff_id} đã lưu thành công x0..x4 (File size x4: {sz} bytes, sắc nét tuyệt đối)")

# ==============================================================================
# 1. EFFECT 918: CAO SU XÀ QUYỀN (SNAKEMAN HYDRA / CULVERIN BARRAGE) - HÌNH 3
# ==============================================================================
def draw_detailed_culverin_arm(w=200, h=200, seed=1):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    rng = random.Random(seed)
    
    # Path of the rubber arm
    p0 = (16, h - 25)
    p1 = (55 + rng.randint(-10, 10), 38 + rng.randint(-8, 12))
    p2 = (118 + rng.randint(-10, 10), h - 45 + rng.randint(-6, 8))
    p3 = (156 + rng.randint(-8, 8), 58 + rng.randint(-8, 8))
    target = (w - 28, h // 2 + rng.randint(-18, 18))
    pts = [p0, p1, p2, p3, target]
    
    # Layer 1: Fiery Crimson/Orange Haki speed aura trail
    for i in range(len(pts) - 1):
        a, b = pts[i], pts[i+1]
        d.line([a, b], fill=(255, 30, 10, 160), width=20)
        d.line([a, b], fill=(255, 120, 0, 200), width=14)

    # Layer 2: Black-Purple Armament Rubber Body with crisp dark outline
    for i in range(len(pts) - 1):
        a, b = pts[i], pts[i+1]
        d.line([a, b], fill=(6, 1, 10, 255), width=10)
        d.line([a, b], fill=(28, 8, 36, 255), width=7)
        d.line([a, b], fill=(200, 20, 60, 255), width=3) # Crimson rim light

    # Layer 3: Metallic Specular Sheen (glossy rubber look)
    for i in range(len(pts) - 1):
        a, b = pts[i], pts[i+1]
        d.line([a, b], fill=(255, 230, 240, 220), width=1)

    # Layer 4: Gear 4/5 Steam Cloud Puffs at every elbow/turn
    for pt in pts[1:-1]:
        draw_cloud_cluster(d, pt[0], pt[1], scale=0.6)

    # Layer 5: Giant Haki Fist at tip
    fx, fy = target
    fist_r = 22
    # Outer fiery burst aura
    d.ellipse([fx-fist_r-8, fy-fist_r-8, fx+fist_r+8, fy+fist_r+8], fill=(255, 50, 10, 180))
    d.ellipse([fx-fist_r-4, fy-fist_r-4, fx+fist_r+4, fy+fist_r+4], fill=(255, 170, 30, 230))
    # Obsidian Fist body
    d.ellipse([fx-fist_r, fy-fist_r, fx+fist_r, fy+fist_r], fill=(10, 2, 14, 255), outline=(255, 50, 80, 255), width=3)
    # Knuckles details (4 knuckles)
    for dx_k in [-10, -3, 4, 11]:
        d.ellipse([fx+dx_k-3, fy-fist_r+5, fx+dx_k+3, fy-fist_r+12], fill=(45, 12, 55, 255), outline=(255, 110, 150, 255), width=2)
    # Hot specular glare
    d.ellipse([fx-fist_r//2, fy-fist_r//2, fx, fy], fill=(255, 255, 255, 240))
    draw_sparkle(d, fx, fy, r=16, color=(255, 255, 240, 255))
    
    # Haki lightning arcing from fist
    pts_haki = [(fx, fy), (fx - 18, fy - 22), (fx - 36, fy - 12), (fx - 50, fy - 25)]
    draw_tapered_haki_lightning(d, pts_haki, base_w=4)
    return im

def draw_detailed_culverin_impact(w=200, h=200, scale=1.0):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    r_max = int(64 * scale)
    
    # Radiating fire spikes
    num_spikes = 18
    for i in range(num_spikes):
        ang = (i / float(num_spikes)) * math.pi * 2
        rad_in = r_max * 0.35
        rad_out = r_max * (0.95 + (i % 2) * 0.3)
        p1 = (cx + math.cos(ang) * rad_in, cy + math.sin(ang) * rad_in)
        p2 = (cx + math.cos(ang) * rad_out, cy + math.sin(ang) * rad_out)
        d.line([p1, p2], fill=(255, 40, 10, 255), width=5)
        d.line([p1, p2], fill=(255, 210, 60, 255), width=2)
    
    # Shockwave circles
    d.ellipse([cx-r_max*0.8, cy-r_max*0.8, cx+r_max*0.8, cy+r_max*0.8], fill=(255, 50, 10, 180))
    d.ellipse([cx-r_max*0.55, cy-r_max*0.55, cx+r_max*0.55, cy+r_max*0.55], fill=(255, 170, 20, 230))
    d.ellipse([cx-r_max*0.35, cy-r_max*0.35, cx+r_max*0.35, cy+r_max*0.35], fill=(255, 255, 230, 255))
    draw_sparkle(d, cx, cy, r=int(30 * scale), color=(255, 255, 255, 255))
    
    # 4 Conqueror's lightning bolts bursting outward
    for ang_deg in [25, 115, 205, 295]:
        rad = math.radians(ang_deg)
        x1 = cx + math.cos(rad) * 16
        y1 = cy + math.sin(rad) * 16
        x2 = cx + math.cos(rad) * (r_max * 0.7) + random.randint(-8, 8)
        y2 = cy + math.sin(rad) * (r_max * 0.7) + random.randint(-8, 8)
        x3 = cx + math.cos(rad) * (r_max * 1.25)
        y3 = cy + math.sin(rad) * (r_max * 1.25)
        draw_tapered_haki_lightning(d, [(x1, y1), (x2, y2), (x3, y3)], base_w=4)
    return im

def create_effect_918():
    w_box, h_box = 200, 200
    f0 = draw_detailed_culverin_arm(w_box, h_box, seed=15)
    f1 = draw_detailed_culverin_arm(w_box, h_box, seed=35).transpose(Image.Transpose.FLIP_LEFT_RIGHT)
    f2 = draw_detailed_culverin_arm(w_box, h_box, seed=55)
    f3 = draw_detailed_culverin_arm(w_box, h_box, seed=75).transpose(Image.Transpose.FLIP_TOP_BOTTOM)
    imp1 = draw_detailed_culverin_impact(w_box, h_box, scale=0.85)
    imp2 = draw_detailed_culverin_impact(w_box, h_box, scale=1.15)
    
    frames_img = [f0, f1, f2, f3, imp1, imp2]
    # 3 cols x 2 rows = 600 x 400 (1x: 150 x 100 <= 255)
    cols = 3
    rows = 2
    im = Image.new('RGBA', (w_box * cols, h_box * rows), (0, 0, 0, 0))
    small_imgs = []
    for idx, f_im in enumerate(frames_img):
        col = idx % cols
        row = idx // cols
        gx = col * w_box
        gy = row * h_box
        im.paste(f_im, (gx, gy))
        small_imgs.append([idx, gx // 4, gy // 4, w_box // 4, h_box // 4])
    
    dx = -25
    dy = -25
    frame_list = [
        [(dx, dy, 0, 0, 0)],
        [(dx, dy, 1, 0, 0)],
        [(dx, dy, 2, 0, 0), (dx, dy, 4, 0, 0)],
        [(dx, dy, 3, 0, 0), (dx, dy, 5, 0, 0)],
        [(dx, dy, 1, 0, 0), (dx, dy, 4, 0, 0)],
        [(dx, dy, 5, 0, 0)]
    ]
    seq = [0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5]
    data_bytes = build_data_effect(small_imgs, frame_list, seq)
    save_multizoom_effect(918, im, data_bytes)

# ==============================================================================
# 2. EFFECT 919: THẦN NIKA CỰ QUYỀN (BAJRANG GUN & CỘT SÁNG NỔ ĐẤT) - HÌNH 1 & 2
# ==============================================================================
def draw_detailed_bajrang_fist(w=216, h=264):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    
    # 1. Storm Clouds at top wrist (deep purple and white)
    for ox, oy, sc in [(-40, 20, 0.9), (0, 14, 1.2), (40, 20, 0.9), (-20, 38, 0.8), (20, 38, 0.8)]:
        draw_cloud_cluster(d, cx + ox, oy, scale=sc)
    
    # 2. Arm descending from sky with flame edge
    d.polygon([(cx - 36, 25), (cx + 36, 25), (cx + 46, 110), (cx - 46, 110)], fill=(255, 30, 60, 255))
    d.polygon([(cx - 30, 25), (cx + 30, 25), (cx + 40, 110), (cx - 40, 110)], fill=(12, 2, 16, 255))
    d.line([(cx - 28, 30), (cx - 36, 105)], fill=(255, 210, 230, 220), width=3)

    # 3. Massive Fist Body (Crisp, High Contrast, Sculpted Muscles)
    fist_box = [cx - 68, 80, cx + 68, 240]
    # Fiery Haki Rim
    d.ellipse([fist_box[0]-8, fist_box[1]-8, fist_box[2]+8, fist_box[3]+8], fill=(255, 40, 70, 220))
    d.ellipse([fist_box[0]-4, fist_box[1]-4, fist_box[2]+4, fist_box[3]+4], fill=(255, 160, 20, 255))
    # Deep Obsidian Core
    d.ellipse(fist_box, fill=(8, 2, 12, 255), outline=(255, 60, 90, 255), width=3)
    
    # 4 Knuckles (Thịt đốt ngón tay cuồn cuộn gân thép)
    knuckles = [
        (cx - 42, 150, 16, 30),
        (cx - 14, 168, 17, 34),
        (cx + 14, 168, 17, 34),
        (cx + 42, 150, 16, 30)
    ]
    for kx, ky, kw, kh in knuckles:
        d.ellipse([kx-kw-3, ky-kh-3, kx+kw+3, ky+kh+3], fill=(255, 30, 60, 255))
        d.ellipse([kx-kw, ky-kh, kx+kw, ky+kh], fill=(22, 5, 28, 255), outline=(255, 80, 120, 255), width=2)
        # Specular ridge on knuckle
        d.arc([kx-kw+4, ky-kh+4, kx+kw-4, ky+kh-4], start=180, end=340, fill=(255, 240, 250, 255), width=3)
    
    # Fist bottom / palm crease
    d.ellipse([cx - 52, 185, cx + 52, 232], fill=(14, 3, 18, 255), outline=(255, 60, 100, 255), width=2)
    d.arc([cx - 38, 192, cx + 38, 225], start=190, end=350, fill=(255, 245, 255, 255), width=3)
    
    # White Hagoromo Steam Swirls wrapping around the giant fist
    for ox, r in [(-48, 18), (-24, 24), (14, 26), (42, 20)]:
        draw_cloud_cluster(d, cx + ox, 235, scale=0.7)

    # Black-and-Crimson Conqueror's Haki Lightning Bolts crackling from fist
    draw_tapered_haki_lightning(d, [(cx - 60, 90), (cx - 82, 130), (cx - 56, 175), (cx - 86, 220)], base_w=5)
    draw_tapered_haki_lightning(d, [(cx + 60, 100), (cx + 84, 140), (cx + 58, 185), (cx + 88, 225)], base_w=5)
    draw_tapered_haki_lightning(d, [(cx - 20, 110), (cx - 40, 150), (cx - 15, 195)], base_w=4)
    draw_tapered_haki_lightning(d, [(cx + 20, 115), (cx + 42, 155), (cx + 18, 200)], base_w=4)
    draw_sparkle(d, cx - 42, 150, r=14)
    draw_sparkle(d, cx + 14, 168, r=16)
    return im

def draw_detailed_divine_pillar(w=216, h=264, phase=0):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    ground_y = h - 38
    
    # 1. Vertical Divine Golden Pillar
    pw = 42 + phase * 12
    # Outer crimson fire aura
    d.polygon([(cx - pw - 8, ground_y), (cx + pw + 8, ground_y), (cx + (pw+8)//2, 4), (cx - (pw+8)//2, 4)], fill=(255, 40, 10, 170))
    # Golden radiant pillar
    d.polygon([(cx - pw, ground_y), (cx + pw, ground_y), (cx + pw//2, 4), (cx - pw//2, 4)], fill=(255, 215, 25, 240))
    # White holy core
    d.polygon([(cx - pw//2, ground_y), (cx + pw//2, ground_y), (cx + pw//4, 4), (cx - pw//4, 4)], fill=(255, 255, 250, 255))
    
    # 2. Ground Shockwave Ellipses
    rx = 80 + phase * 24
    ry = 24 + phase * 6
    # Outer shockwave rim
    d.ellipse([cx - rx - 6, ground_y - ry - 3, cx + rx + 6, ground_y + ry + 3], fill=(255, 35, 10, 190))
    # Golden ring
    d.ellipse([cx - rx, ground_y - ry, cx + rx, ground_y + ry], fill=(255, 185, 20, 230))
    # Inner bright crater
    d.ellipse([cx - rx * 0.7, ground_y - ry * 0.7, cx + rx * 0.7, ground_y + ry * 0.7], fill=(255, 245, 170, 255))
    d.ellipse([cx - rx * 0.4, ground_y - ry * 0.4, cx + rx * 0.4, ground_y + ry * 0.4], fill=(255, 255, 255, 255))
    
    # 3. Radial Impact Spikes
    num_spikes = 16
    for i in range(num_spikes):
        ang = (i / float(num_spikes)) * math.pi * 2
        sx1 = cx + math.cos(ang) * (rx * 0.6)
        sy1 = ground_y + math.sin(ang) * (ry * 0.6)
        sx2 = cx + math.cos(ang) * (rx * 1.25)
        sy2 = ground_y + math.sin(ang) * (ry * 1.25) - (10 + (i % 3) * 8)
        d.polygon([(sx1-4, sy1), (sx1+4, sy1), (sx2, sy2)], fill=(255, 45, 10, 240))
        d.line([(sx1, sy1), (sx2, sy2)], fill=(255, 245, 190, 255), width=2)
    
    # 4. Dense Conqueror's Haki Lightning branching from impact
    pts_left = [(cx - 18, ground_y - 14), (cx - 50, ground_y - 52), (cx - 82, ground_y - 105), (cx - 102, ground_y - 155)]
    pts_right = [(cx + 18, ground_y - 14), (cx + 52, ground_y - 56), (cx + 84, ground_y - 102), (cx + 104, ground_y - 152)]
    draw_tapered_haki_lightning(d, pts_left, base_w=5)
    draw_tapered_haki_lightning(d, pts_right, base_w=5)
    
    # Ground cracks
    d.line([(cx - 10, ground_y), (cx - 45, ground_y + 12), (cx - 75, ground_y + 8)], fill=(12, 2, 16, 255), width=3)
    d.line([(cx + 10, ground_y), (cx + 48, ground_y + 10), (cx + 78, ground_y + 5)], fill=(12, 2, 16, 255), width=3)

    # Impact Flash Sparkles
    draw_sparkle(d, cx, ground_y - 28, r=32, color=(255, 255, 255, 255))
    draw_sparkle(d, cx - 40, ground_y - 16, r=18, color=(255, 240, 180, 255))
    draw_sparkle(d, cx + 40, ground_y - 16, r=18, color=(255, 240, 180, 255))
    return im

def create_effect_919():
    import generate_final_nika_919
    generate_final_nika_919.generate()

# ==============================================================================
# 3. EFFECT 920: THỨC TỈNH NIKA (SUN GOD AWAKENING & UY ÁP BÁ VƯƠNG) - HÌNH 4
# ==============================================================================
def draw_detailed_nika_awakening(w=200, h=200, phase=0):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    cy = h - 45
    
    # 1. Concentric Rubbery Ground Warps
    num_rings = 4
    for i in range(num_rings, 0, -1):
        rx = 32 + i * 18 + phase * 6
        ry = 12 + i * 5 + phase * 2
        # Outline
        d.ellipse([cx - rx - 3, cy - ry - 2, cx + rx + 3, cy + ry + 2], fill=(255, 50, 10, 170))
        # Golden rubber wave
        col_wave = (255, 215, 35, 230) if i % 2 == 0 else (255, 245, 170, 240)
        d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], fill=col_wave)
        # Specular crest on wave
        d.arc([cx - rx + 4, cy - ry + 2, cx + rx - 4, cy + ry - 2], start=190, end=350, fill=(255, 255, 255, 255), width=2)
    
    # Core glowing pool at feet
    d.ellipse([cx - 28, cy - 10, cx + 28, cy + 10], fill=(255, 255, 245, 255))
    
    # 2. Radial Solar Rays
    ray_len = 55 + phase * 10
    num_rays = 14
    rot_offset = phase * (math.pi / 7.0)
    for i in range(num_rays):
        ang = (i / float(num_rays)) * math.pi * 2 + rot_offset
        p1 = (cx + math.cos(ang) * 18, cy + math.sin(ang) * 8)
        p2 = (cx + math.cos(ang) * ray_len, cy + math.sin(ang) * (ray_len * 0.45) - 18)
        d.line([p1, p2], fill=(255, 170, 10, 200), width=4)
        d.line([p1, p2], fill=(255, 250, 210, 255), width=2)
        
    # 3. Dense Branching Conqueror's Haki Lightning
    haki_angles = [20, 65, 110, 155, 200, 245, 290, 335]
    for ang_deg in haki_angles:
        rad = math.radians(ang_deg + (phase * 15))
        r1 = 22
        r2 = 48 + (ang_deg % 20)
        r3 = 76 + (ang_deg % 15)
        x1 = cx + math.cos(rad) * r1
        y1 = cy + math.sin(rad) * (r1 * 0.4)
        x2 = cx + math.cos(rad) * r2 + random.randint(-8, 8)
        y2 = cy + math.sin(rad) * (r2 * 0.4) - 12 + random.randint(-8, 8)
        x3 = cx + math.cos(rad) * r3 + random.randint(-10, 10)
        y3 = cy + math.sin(rad) * (r3 * 0.4) - 24 + random.randint(-10, 10)
        draw_tapered_haki_lightning(d, [(x1, y1), (x2, y2), (x3, y3)], base_w=4)
        
    # 4. Swirling Hagoromo White Steam Clouds encircling the body
    cloud_positions = [
        (cx - 52, cy - 28, 0.7),
        (cx - 30, cy - 54, 0.8),
        (cx + 30, cy - 54, 0.8),
        (cx + 52, cy - 28, 0.7),
        (cx, cy - 64, 0.9)
    ]
    for ox, oy, sc in cloud_positions:
        draw_cloud_cluster(d, ox, oy, scale=sc)
        
    # Brilliant Sparkles
    draw_sparkle(d, cx, cy - 18, r=22, color=(255, 255, 255, 255))
    draw_sparkle(d, cx - 38, cy - 38, r=14, color=(255, 245, 190, 255))
    draw_sparkle(d, cx + 38, cy - 38, r=14, color=(255, 245, 190, 255))
    return im

def create_effect_920():
    from generate_final_haki_buff import generate_exact_haki_buff
    generate_exact_haki_buff()

# ==============================================================================
# MAIN EXECUTION
# ==============================================================================
if __name__ == '__main__':
    print("==================================================================")
    print("BẮT ĐẦU TẠO ASSET EFFECT NIKA SẮC NÉT CHUẨN ANIME 100%:")
    print("==================================================================")
    create_effect_918()
    create_effect_919()
    create_effect_920()
    print("==================================================================")
    print("HOÀN THÀNH TẠO ASSETS HIỆU ỨNG THẦN NIKA SẮC NÉT!")
    print("==================================================================")
