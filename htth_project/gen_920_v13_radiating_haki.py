import os
import sys
import struct
import io
import math
import numpy as np
from PIL import Image, ImageDraw

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRIPT_DIR = r'D:\project\GameHTTH\htth_project'
BASE_DIR = os.path.join(SCRIPT_DIR, 'data', 'template', 'skill')
SCRATCH = r'C:\Users\admin\.gemini\antigravity-ide\brain\962f8d45-51ec-4554-8c92-12988262654e\scratch'

W_BOX, H_BOX = 200, 200
cx, cy = 100, 110 # Character center

def round4(val):
    return int(round(val / 4.0) * 4)

def draw_dual_layer_frame(phase, num_phases=6):
    im_back = Image.new('RGBA', (W_BOX, H_BOX), (0, 0, 0, 0))
    d_back = ImageDraw.Draw(im_back)
    
    im_front = Image.new('RGBA', (W_BOX, H_BOX), (0, 0, 0, 0))
    d_front = ImageDraw.Draw(im_front)
    
    t = phase / float(num_phases)
    
    # --- A. EXPANDING 360-DEGREE CONCENTRIC HAKI SHOCKWAVES (Behind) ---
    for ring_i in range(2):
        r_t = (t + ring_i / 2.0) % 1.0
        rad = 18 + r_t * 70
        alpha = int(240 * math.sin(r_t * math.pi))
        
        # Crimson-Purple / Magenta Haki Gradient
        d_back.ellipse([cx - rad - 3, cy - rad * 0.75 - 2, cx + rad + 3, cy + rad * 0.75 + 2],
                       outline=(255, int(20 + 40 * (1 - r_t)), int(90 + 120 * r_t), int(alpha * 0.4)), width=3)
        d_back.ellipse([cx - rad, cy - rad * 0.75, cx + rad, cy + rad * 0.75],
                       outline=(255, int(80 * (1 - r_t)), int(200 * r_t + 40), alpha), width=2)
        if alpha > 70:
            d_back.arc([cx - rad, cy - rad * 0.75, cx + rad, cy + rad * 0.75],
                       start=20, end=160, fill=(255, 230, 255, int(alpha * 0.8)), width=2)

    # --- B. OMNIDIRECTIONAL BRANCHING HAKI LIGHTNING (Both Layers) ---
    num_bolts = 8
    for b in range(num_bolts):
        base_angle = (b / float(num_bolts)) * 2 * math.pi + t * 0.5
        b_len = 45 + 35 * math.sin(b * 1.9 + t * 2 * math.pi)
        
        pts = [(cx + math.cos(base_angle) * 12, cy + math.sin(base_angle) * 10)]
        segments = 4
        for s in range(1, segments + 1):
            st = s / float(segments)
            jag = 10 * math.sin(s * 2.5 + b * 2.7 + phase)
            ang = base_angle + (jag * 0.04)
            dist = 12 + st * (b_len - 12)
            nx = cx + math.cos(ang) * dist + jag * math.sin(base_angle)
            ny = cy + math.sin(ang) * dist * 0.8 - jag * math.cos(base_angle)
            pts.append((nx, ny))
            
        is_front = (math.sin(base_angle) > 0.1) or (b % 2 == 1)
        d = d_front if is_front else d_back
        
        for i in range(len(pts) - 1):
            p1, p2 = pts[i], pts[i+1]
            w = max(1, int(4 * (1.0 - (i / float(len(pts))))))
            d.line([p1, p2], fill=(255, 30, 110, 220), width=w + 3)
            d.line([p1, p2], fill=(15, 2, 22, 255), width=w + 1)
            d.line([p1, p2], fill=(255, 230, 255, 240), width=max(1, w - 1))
            
        tip_x, tip_y = pts[-1]
        tip_r = 3 + 2 * math.sin(b + t * math.pi * 2)
        d.ellipse([tip_x - tip_r, tip_y - tip_r, tip_x + tip_r, tip_y + tip_r],
                  fill=(255, 240, 255, 255))

    # --- C. HAGOROMO SUN GOD CLOUDS (Back & Front 3D Ribbon) ---
    for c_i in range(6):
        c_ang = (c_i / 6.0) * 2 * math.pi + t * 2 * math.pi
        c_rad_x = 36 + 6 * math.sin(c_i * 2 + t * 2 * math.pi)
        c_rad_y = 24 + 4 * math.cos(c_i * 2 + t * 2 * math.pi)
        c_x = cx + math.cos(c_ang) * c_rad_x
        c_y = cy + math.sin(c_ang) * c_rad_y - 10
        
        is_front_cloud = math.sin(c_ang) > 0
        d_c = d_front if is_front_cloud else d_back
        
        cr = 9 + 2 * math.sin(c_ang + t)
        d_c.ellipse([c_x - cr - 2, c_y - cr - 2, c_x + cr + 2, c_y + cr + 2],
                    fill=(255, 60, 140, 180))
        d_c.ellipse([c_x - cr, c_y - cr, c_x + cr, c_y + cr],
                    fill=(255, 255, 255, 245))
        d_c.ellipse([c_x - cr * 0.5, c_y - cr * 0.5, c_x + cr * 0.5, c_y + cr * 0.5],
                    fill=(255, 240, 255, 255))

    # --- D. BURSTING SPARK PARTICLES (Both Layers) ---
    for p_i in range(10):
        p_ang = (p_i / 10.0) * 2 * math.pi + p_i * 1.5
        p_dist = 16 + (t * 60 + p_i * 6) % 65
        p_x = cx + math.cos(p_ang) * p_dist
        p_y = cy + math.sin(p_ang) * p_dist * 0.8
        p_r = max(1, int(2.5 * (1.0 - p_dist / 85.0)))
        
        d_p = d_front if math.sin(p_ang) > 0 else d_back
        d_p.ellipse([p_x - p_r, p_y - p_r, p_x + p_r, p_y + p_r],
                    fill=(255, 170, 240, int(255 * (1.0 - p_dist / 85.0))))
                    
    return im_back, im_front

def generate():
    num_frames = 6
    sprites = []
    sprites_info = []

    for f in range(num_frames):
        b, fr = draw_dual_layer_frame(f, num_frames)
        arr_b = np.array(b)
        ys, xs = np.where(arr_b[:, :, 3] > 10)
        cropped_b = arr_b[ys.min():ys.max()+1, xs.min():xs.max()+1]
        sb = Image.fromarray(cropped_b)
        wb = round4(sb.width)
        hb = round4(sb.height)
        sb_res = sb.resize((wb, hb), Image.Resampling.LANCZOS)
        sprites.append(sb_res)
        sprites_info.append((f'B_{f}', (wb, hb), (xs.min(), ys.min())))

        arr_fr = np.array(fr)
        ys, xs = np.where(arr_fr[:, :, 3] > 10)
        cropped_fr = arr_fr[ys.min():ys.max()+1, xs.min():xs.max()+1]
        sfr = Image.fromarray(cropped_fr)
        wfr = round4(sfr.width)
        hfr = round4(sfr.height)
        sfr_res = sfr.resize((wfr, hfr), Image.Resampling.LANCZOS)
        sprites.append(sfr_res)
        sprites_info.append((f'F_{f}', (wfr, hfr), (xs.min(), ys.min())))

    # Skyline packing
    max_w, max_h = 1020, 1020
    rects = [(w, h, name, idx) for idx, (name, (w, h), (ox, oy)) in enumerate(sprites_info)]
    rects.sort(key=lambda r: (r[1], r[0]), reverse=True)

    skyline = [(0, 0, max_w)]
    positions = {}

    for rw, rh, name, idx in rects:
        best_i = -1
        best_y = 1e9
        best_x = 0
        for i in range(len(skyline)):
            sx = skyline[i][0]
            if sx + rw > max_w:
                continue
            cur_w = 0
            max_h_here = 0
            for j in range(i, len(skyline)):
                max_h_here = max(max_h_here, skyline[j][1])
                cur_w += skyline[j][2]
                if cur_w >= rw:
                    break
            if cur_w >= rw and max_h_here + rh <= max_h:
                if max_h_here < best_y:
                    best_y = max_h_here
                    best_x = sx
                    best_i = i
        
        positions[idx] = (best_x, best_y, rw, rh, name)
        new_node = (best_x, best_y + rh, rw)
        new_skyline = []
        for s in skyline:
            sx, sy, sw = s
            if sx + sw <= best_x or sx >= best_x + rw:
                new_skyline.append(s)
            else:
                if sx < best_x:
                    new_skyline.append((sx, sy, best_x - sx))
                if sx + sw > best_x + rw:
                    new_skyline.append((best_x + rw, sy, (sx + sw) - (best_x + rw)))
        new_skyline.append(new_node)
        new_skyline.sort(key=lambda s: s[0])
        merged = []
        for s in new_skyline:
            if merged and merged[-1][1] == s[1] and merged[-1][0] + merged[-1][2] == s[0]:
                merged[-1] = (merged[-1][0], merged[-1][1], merged[-1][2] + s[2])
            else:
                merged.append(s)
        skyline = merged

    tw = round4(max(p[0] + p[2] for p in positions.values()))
    th = round4(max(p[1] + p[3] for p in positions.values()))

    sheet_4x = Image.new('RGBA', (tw, th), (0, 0, 0, 0))
    small_images = []
    for idx in range(len(sprites)):
        x, y, w, h, name = positions[idx]
        sp = sprites[idx]
        sheet_4x.paste(sp, (x, y), sp)
        small_images.append([idx, x // 4, y // 4, w // 4, h // 4])

    frames = []
    for f in range(num_frames):
        id_back = f * 2
        id_front = f * 2 + 1
        
        name_b, (wb, hb), (ox_b, oy_b) = sprites_info[id_back]
        name_fr, (wfr, hfr), (ox_fr, oy_fr) = sprites_info[id_front]
        
        dx_b = (ox_b - cx) // 4
        dy_b = (oy_b - cy - 20) // 4
        
        dx_fr = (ox_fr - cx) // 4
        dy_fr = (oy_fr - cy - 20) // 4
        
        parts = [
            (dx_b, dy_b, id_back, 0, 0),
            (dx_fr, dy_fr, id_front, 0, 1),
        ]
        frames.append(parts)

    sequence = [0, 1, 2, 3, 4, 5]

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
    data_bytes = bytes(out)

    # Save animated GIF preview
    os.makedirs(SCRATCH, exist_ok=True)
    def make_canvas(parts):
        canv = Image.new('RGBA', (300, 300), (28, 30, 36, 255))
        # Draw mock player stick figure in between onTop=0 and onTop=1
        d_c = ImageDraw.Draw(canv)
        # Background layer
        for dx, dy, idSmall, flip, onTop in parts:
            if onTop == 0:
                sp = sprites[idSmall]
                canv.paste(sp, (150 + dx * 4, 150 + dy * 4), sp)
        # Character
        d_c.ellipse([140, 115, 160, 135], fill=(240, 200, 170, 255)) # Head
        d_c.rectangle([142, 135, 158, 175], fill=(220, 40, 40, 255)) # Body
        # Foreground layer
        for dx, dy, idSmall, flip, onTop in parts:
            if onTop == 1:
                sp = sprites[idSmall]
                canv.paste(sp, (150 + dx * 4, 150 + dy * 4), sp)
        return canv

    gif_frames = [make_canvas(frames[s]) for s in sequence]
    gif_path = os.path.join(SCRATCH, 'nika920_radiating_haki_preview.gif')
    gif_frames[0].save(gif_path, save_all=True, append_images=gif_frames[1:], duration=60, loop=0)
    print(f"Saved preview GIF: {gif_path}")

    # Deploy multi-zoom levels
    w4, h4 = sheet_4x.size
    w1, h1 = w4 // 4, h4 // 4
    zooms = {
        'x4': (sheet_4x, 48),
        'x3': (sheet_4x.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 64),
        'x2': (sheet_4x.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 96),
        'x1': (sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 128),
        'x0': (sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 128),
    }

    for z, (im_z, n_col) in zooms.items():
        img_dir = os.path.join(BASE_DIR, z, 'img')
        dat_dir = os.path.join(BASE_DIR, z, 'data')
        os.makedirs(img_dir, exist_ok=True)
        os.makedirs(dat_dir, exist_ok=True)

        arr_z = np.array(im_z)
        arr_z[arr_z[:, :, 3] < 15] = [0, 0, 0, 0]
        im_clean = Image.fromarray(arr_z)
        im_q = im_clean.quantize(colors=n_col, method=Image.Quantize.FASTOCTREE)

        img_path = os.path.join(img_dir, '920.png')
        buf = io.BytesIO()
        im_q.save(buf, format='PNG', compress_level=9, optimize=True)
        tmp_img = img_path + '.tmp'
        with open(tmp_img, 'wb') as f:
            f.write(buf.getvalue())
        os.replace(tmp_img, img_path)
        img_sz = os.path.getsize(img_path)

        dat_path = os.path.join(dat_dir, '920')
        tmp_dat = dat_path + '.tmp'
        with open(tmp_dat, 'wb') as f:
            f.write(data_bytes)
        os.replace(tmp_dat, dat_path)

        total_sz = img_sz + len(data_bytes)
        print(f"  [{z}] 920.png = {img_sz} bytes, total packet body = {total_sz} bytes (max 60000)")

    print("\n=== SUCCESS! Effect 920 with 360-degree radiating Haki aura deployed! ===")

if __name__ == '__main__':
    generate()
