import os
import sys
import struct
import io
import numpy as np
from PIL import Image, ImageDraw
import scipy.ndimage as ndi

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRIPT_DIR = r'D:\project\GameHTTH\htth_project'
BASE_DIR = os.path.join(SCRIPT_DIR, 'data', 'template', 'skill')
SRC_IMG = r'C:\Users\admin\.gemini\antigravity-ide\brain\a785e093-427f-476f-9051-2e79532b30ab\.user_uploaded\media_1790065917499.png'
ICON_24413 = os.path.join(SCRIPT_DIR, 'data', 'icon', 'x4', '24413.png')
ICON_24312 = os.path.join(SCRIPT_DIR, 'data', 'icon', 'x4', '24312.png')
SCRATCH = r'C:\Users\admin\.gemini\antigravity-ide\brain\962f8d45-51ec-4554-8c92-12988262654e\scratch'

def round4(val):
    return int(round(val / 4.0) * 4)

def recolor_to_black_red(im_in):
    arr = np.array(im_in, dtype=float)
    a = arr[:, :, 3]
    m = a > 10
    
    r, g, b = arr[:, :, 0], arr[:, :, 1], arr[:, :, 2]
    lum = 0.299 * r + 0.587 * g + 0.114 * b
    
    new_r = np.zeros_like(r)
    new_g = np.zeros_like(g)
    new_b = np.zeros_like(b)
    
    # 1. Jet Black Haki fist body (lum < 55)
    m_dark = m & (lum < 55)
    new_r[m_dark] = np.clip(lum[m_dark] * 0.40, 0, 25)
    new_g[m_dark] = np.clip(lum[m_dark] * 0.05, 0, 8)
    new_b[m_dark] = np.clip(lum[m_dark] * 0.05, 0, 8)
    
    # 2. Red edge/contour around fist (55 <= lum < 95)
    m_edge = m & (lum >= 55) & (lum < 95)
    t = (lum[m_edge] - 55.0) / 40.0
    new_r[m_edge] = np.clip(60.0 + t * 135.0, 0, 210)
    new_g[m_edge] = np.clip(t * 8.0, 0, 15)
    new_b[m_edge] = np.clip(t * 5.0, 0, 10)
    
    # 3. Vivid Crimson Red aura / lightning / shockwave rings (lum >= 95)
    m_red = m & (lum >= 95)
    t2 = (lum[m_red] - 95.0) / 160.0
    new_r[m_red] = np.clip(210.0 + t2 * 45.0, 0, 255)
    new_g[m_red] = np.clip(t2 * 25.0, 0, 40)
    new_b[m_red] = np.clip(t2 * 8.0, 0, 15)
    
    # 4. White-hot / yellow-hot electric core for extreme highlights (lum > 220)
    m_hi = m & (lum > 220)
    t3 = (lum[m_hi] - 220.0) / 35.0
    new_r[m_hi] = 255
    new_g[m_hi] = np.clip(35.0 + t3 * 180.0, 0, 220)
    new_b[m_hi] = np.clip(t3 * 80.0, 0, 100)
    
    res = np.stack([new_r, new_g, new_b, a], axis=-1).astype(np.uint8)
    return Image.fromarray(res)

def recolor_p3_fist_thrust(im_in):
    """Black Haki fist with vivid crimson red border and crackling red Haki lightning arcs"""
    arr = np.array(im_in)
    h, w = arr.shape[:2]
    r, g, b, a = arr[:, :, 0].astype(float), arr[:, :, 1].astype(float), arr[:, :, 2].astype(float), arr[:, :, 3]
    m = a > 15
    lum = 0.299 * r + 0.587 * g + 0.114 * b

    in_box = (np.arange(w) >= 42)[None, :] & (np.arange(w) <= 126)[None, :] & (np.arange(h) >= 35)[:, None] & (np.arange(h) <= 186)[:, None]
    fist_dark = m & in_box & (r < 55) & (g < 45) & (b < 45)
    lab, num = ndi.label(fist_dark)
    main_lbl = lab[110, 85]
    fist_body = (lab == main_lbl)

    fist_rim = m & in_box & (r > 50) & (g < 40) & (b < 40)
    lab_rim, _ = ndi.label(fist_rim | fist_body)
    fist_full = (lab_rim == lab_rim[110, 85])

    out = np.zeros_like(arr)

    # 1. Golden Energy Spikes
    is_spike = m & ~fist_full
    t_sp = np.clip((lum[is_spike] - 70) / 185.0, 0, 1)
    out[is_spike, 0] = np.clip(235 + t_sp * 20, 0, 255)
    out[is_spike, 1] = np.clip(185 + t_sp * 70, 0, 255)
    out[is_spike, 2] = np.clip(35 + t_sp * 140, 0, 180)
    out[is_spike, 3] = a[is_spike]

    # 2. Fist Body: Jet Black
    out[fist_body, 0] = np.clip(lum[fist_body] * 0.35, 0, 22)
    out[fist_body, 1] = np.clip(lum[fist_body] * 0.05, 0, 8)
    out[fist_body, 2] = np.clip(lum[fist_body] * 0.05, 0, 8)
    out[fist_body, 3] = 255

    # 3. Vivid Crimson Red Border for Fist
    d_out = ndi.binary_dilation(fist_full, iterations=3) & m & ~fist_body
    d_mid = ndi.binary_dilation(fist_full, iterations=2) & m & ~fist_body
    d_in = ndi.binary_dilation(fist_full, iterations=1) & m & ~fist_body

    out[d_out] = [200, 20, 30, 240]
    out[d_mid] = [245, 30, 40, 255]
    out[d_in] = [255, 30, 35, 255]

    inner_rim = fist_body & ~ndi.binary_erosion(fist_body, iterations=1)
    out[inner_rim, 0] = np.clip(out[inner_rim, 0].astype(int) + 160, 0, 255)
    out[inner_rim, 1] = np.clip(out[inner_rim, 1].astype(int) // 4, 0, 255)
    out[inner_rim, 2] = np.clip(out[inner_rim, 2].astype(int) // 4, 0, 255)

    im_out = Image.fromarray(out)
    draw = ImageDraw.Draw(im_out)

    # Red Haki Lightning Arcs crackling around P3
    haki_arcs = [
        [(88, 38), (95, 30), (88, 22), (100, 14), (96, 6)],
        [(50, 58), (42, 50), (46, 40), (34, 32), (38, 22)],
        [(52, 95), (42, 88), (46, 78), (32, 70), (28, 58)],
        [(114, 105), (124, 96), (118, 84), (132, 76), (126, 64)],
        [(66, 120), (74, 128), (68, 138), (82, 144)],
        [(76, 172), (82, 184), (94, 178), (100, 188)],
        [(60, 168), (52, 178), (62, 186)],
        # Right jagged bolt
        [(156, 5), (150, 20), (168, 40), (138, 48), (162, 58), (134, 66), (152, 78), (130, 85), (146, 92)]
    ]
    for arc in haki_arcs:
        for i in range(len(arc) - 1):
            draw.line([arc[i], arc[i+1]], fill=(255, 25, 35, 255), width=3)
        for i in range(len(arc) - 1):
            draw.line([arc[i], arc[i+1]], fill=(255, 230, 240, 255), width=1)

    return im_out

def recolor_p4_fist_rings(im_in):
    """Black Haki fist with red border, red Haki ground crackles & red Haki lightning arcs through golden rings"""
    arr = np.array(im_in)
    h, w = arr.shape[:2]
    r, g, b, a = arr[:, :, 0].astype(float), arr[:, :, 1].astype(float), arr[:, :, 2].astype(float), arr[:, :, 3]
    m = a > 15
    lum = 0.299 * r + 0.587 * g + 0.114 * b

    in_box = (np.arange(w) >= 90)[None, :] & (np.arange(w) <= 160)[None, :] & (np.arange(h) <= 126)[:, None]
    fist_dark = m & in_box & (r < 55) & (g < 45) & (b < 45)
    lab, num = ndi.label(fist_dark)
    main_lbl = lab[50, 125]
    fist_body = (lab == main_lbl)

    fist_rim = m & in_box & (r > 50) & (g < 40) & (b < 40)
    lab_rim, _ = ndi.label(fist_rim | fist_body)
    fist_full = (lab_rim == lab_rim[50, 125])

    out = np.zeros_like(arr)

    # 1. Shockwave rings / blast: keep golden yellow
    is_ring = m & ~fist_full & (r > 80) & (g > 55)
    t_r = np.clip((lum[is_ring] - 60) / 195.0, 0, 1)
    out[is_ring, 0] = np.clip(235 + t_r * 20, 0, 255)
    out[is_ring, 1] = np.clip(185 + t_r * 70, 0, 255)
    out[is_ring, 2] = np.clip(35 + t_r * 140, 0, 180)
    out[is_ring, 3] = a[is_ring]

    # 2. Ground crackles beneath ring: Red Haki!
    is_crackle = m & ~fist_full & (r > 60) & (g < 50) & (b < 50)
    out[is_crackle, 0] = 255
    out[is_crackle, 1] = np.clip(lum[is_crackle] * 0.4, 0, 40)
    out[is_crackle, 2] = np.clip(lum[is_crackle] * 0.2, 0, 30)
    out[is_crackle, 3] = a[is_crackle]

    # 3. Fist Body: Jet Black
    out[fist_body, 0] = np.clip(lum[fist_body] * 0.35, 0, 22)
    out[fist_body, 1] = np.clip(lum[fist_body] * 0.05, 0, 8)
    out[fist_body, 2] = np.clip(lum[fist_body] * 0.05, 0, 8)
    out[fist_body, 3] = 255

    # 4. Vivid Crimson Red Border for Fist
    d_out = ndi.binary_dilation(fist_full, iterations=3) & m & ~fist_body
    d_mid = ndi.binary_dilation(fist_full, iterations=2) & m & ~fist_body
    d_in = ndi.binary_dilation(fist_full, iterations=1) & m & ~fist_body

    out[d_out] = [200, 20, 30, 240]
    out[d_mid] = [245, 30, 40, 255]
    out[d_in] = [255, 30, 35, 255]

    inner_rim = fist_body & ~ndi.binary_erosion(fist_body, iterations=1)
    out[inner_rim, 0] = np.clip(out[inner_rim, 0].astype(int) + 160, 0, 255)
    out[inner_rim, 1] = np.clip(out[inner_rim, 1].astype(int) // 4, 0, 255)
    out[inner_rim, 2] = np.clip(out[inner_rim, 2].astype(int) // 4, 0, 255)

    im_out = Image.fromarray(out)
    draw = ImageDraw.Draw(im_out)

    # Red Haki Lightning Arcs around P4 punch!
    haki_arcs = [
        [(102, 50), (94, 60), (98, 72), (86, 84), (90, 96), (80, 108)],
        [(148, 55), (156, 68), (150, 80), (162, 92), (156, 104), (168, 114)],
        [(115, 112), (110, 122), (118, 130), (128, 134)],
        [(135, 112), (140, 122), (132, 130), (124, 134)],
        [(118, 15), (124, 25), (118, 35), (126, 42)],
    ]
    for arc in haki_arcs:
        for i in range(len(arc) - 1):
            draw.line([arc[i], arc[i+1]], fill=(255, 25, 35, 255), width=3)
        for i in range(len(arc) - 1):
            draw.line([arc[i], arc[i+1]], fill=(255, 230, 240, 255), width=1)

    return im_out

def recolor_blast_natural(im_in):
    """P5_BlastMid: earth=brown natural, central spike + ring = yellow-white fire"""
    arr = np.array(im_in, dtype=float)
    a = arr[:, :, 3]
    m = a > 10
    r_o, g_o, b_o = arr[:, :, 0], arr[:, :, 1], arr[:, :, 2]
    lum = 0.299 * r_o + 0.587 * g_o + 0.114 * b_o
    
    # Detect earth/brown pixels: warm tone, R dominant, G middle, B low
    is_brown = m & (r_o > g_o * 1.05) & (g_o > b_o * 1.2) & (r_o > 60) & (b_o < 120) & (lum < 185)
    
    new_r = r_o.copy()
    new_g = g_o.copy()
    new_b = b_o.copy()
    
    # Earth/brown pixels: keep natural color (nau dat tu nhien)
    # (these stay as-is from source)
    
    # Non-brown energy: golden-yellow fire/spike/ring (cực kỳ rực rỡ)
    is_energy = m & ~is_brown & (lum >= 40)
    t = np.clip((lum[is_energy] - 40) / 215.0, 0, 1)
    new_r[is_energy] = np.clip(230 + t * 25, 0, 255)
    new_g[is_energy] = np.clip(160 + t * 95, 0, 255)
    new_b[is_energy] = np.clip(t * 60, 0, 80)
    
    # Very dark non-brown shadow
    is_dark = m & ~is_brown & (lum < 50)
    new_r[is_dark] = np.clip(lum[is_dark] * 0.4, 0, 25)
    new_g[is_dark] = np.clip(lum[is_dark] * 0.3, 0, 20)
    new_b[is_dark] = np.clip(lum[is_dark] * 0.1, 0, 8)
    
    res = np.stack([new_r, new_g, new_b, a], axis=-1).astype(np.uint8)
    return Image.fromarray(res)

def extract_and_assemble():
    sprites = []
    sprites_info = []

    # 1. 24413 (3 frames before cast - charging vortex)
    im24413 = Image.open(ICON_24413).convert('RGBA')
    arr24413 = np.array(im24413)
    c_slices = [
        ('C0_Charge1', arr24413[0:210, :], 0.85),
        ('C1_Charge2', arr24413[210:435, :], 0.85),
        ('C2_Charge3', arr24413[435:657, :], 0.85),
    ]
    for name, sub, sc in c_slices:
        ys, xs = np.where(sub[:, :, 3] > 10)
        cropped = sub[ys.min():ys.max()+1, xs.min():xs.max()+1]
        s_im = Image.fromarray(cropped)
        nw = round4(s_im.width * sc)
        nh = round4(s_im.height * sc)
        s_res = s_im.resize((nw, nh), Image.Resampling.LANCZOS)
        sprites.append(s_res)
        sprites_info.append((name, (nw, nh)))

    # 2. Fist descent (recolored to black fist with red outline/aura)
    im_punch = Image.open(SRC_IMG).convert('RGBA')
    arr_punch = np.array(im_punch)
    r, g, b = arr_punch[:, :, 0].astype(float), arr_punch[:, :, 1].astype(float), arr_punch[:, :, 2].astype(float)
    dev = np.maximum.reduce([np.abs(r - g), np.abs(g - b), np.abs(r - b)])
    brightness = (r + g + b) / 3.0
    is_bg = (brightness >= 48) & (brightness <= 62) & (dev < 8)
    arr_punch[is_bg, 3] = 0
    arr_punch[arr_punch[:, :, 3] < 15] = 0

    p_slices = [
        ('P1_FistEmerge',    0, 202, 295, 496, 1.05),
        ('P2_FistExtend',    6, 190, 545, 736, 1.05),
        ('P3_FistThrust',    8, 204, 816, 992, 1.10),
        ('P4_FistRings',   205, 395,  30, 280, 1.00),
        ('P5_BlastMid',    155, 395, 343, 607, 0.95),
    ]
    for name, y0, y1, x0, x1, sc in p_slices:
        sub = arr_punch[y0:y1, x0:x1].copy()
        if name == 'P1_FistEmerge':
            sub[175:, 165:] = 0
        elif name == 'P5_BlastMid':
            sub[0:40, 0:90] = 0
        ys, xs = np.where(sub[:, :, 3] > 0)
        cropped = sub[ys.min():ys.max()+1, xs.min():xs.max()+1]
        s_im = Image.fromarray(cropped)
        # Use different recolor per sprite type
        if name == 'P3_FistThrust':
            s_im_rec = recolor_p3_fist_thrust(s_im)
        elif name == 'P4_FistRings':
            s_im_rec = recolor_p4_fist_rings(s_im)
        elif name == 'P5_BlastMid':
            s_im_rec = recolor_blast_natural(s_im)
        else:
            s_im_rec = recolor_to_black_red(s_im)
        nw = round4(s_im_rec.width * sc)
        nh = round4(s_im_rec.height * sc)
        s_res = s_im_rec.resize((nw, nh), Image.Resampling.LANCZOS)
        sprites.append(s_res)
        sprites_info.append((name, (nw, nh)))

    # 3. 24312 (2 frames after impact - ground shattering burst - MÀU NÂU ĐẤT TỰ NHIÊN)
    im24312 = Image.open(ICON_24312).convert('RGBA')
    arr24312 = np.array(im24312)
    e_slices = [
        ('E0_EarthBurst1', arr24312[0:300, :], 0.85),
        ('E1_EarthBurst2', arr24312[300:616, :], 0.85),
    ]
    for name, sub, sc in e_slices:
        ys, xs = np.where(sub[:, :, 3] > 10)
        cropped = sub[ys.min():ys.max()+1, xs.min():xs.max()+1]
        s_im = Image.fromarray(cropped)
        s_im_rec = s_im  # Giữ nguyên màu nâu đất tự nhiên như ảnh gốc
        nw = round4(s_im_rec.width * sc)
        nh = round4(s_im_rec.height * sc)
        s_res = s_im_rec.resize((nw, nh), Image.Resampling.LANCZOS)
        sprites.append(s_res)
        sprites_info.append((name, (nw, nh)))

    # 4. Dust fade
    sub = arr_punch[464:558, 846:1024]
    ys, xs = np.where(sub[:, :, 3] > 0)
    cropped = sub[ys.min():ys.max()+1, xs.min():xs.max()+1]
    s_im = Image.fromarray(cropped)
    s_im_rec = s_im
    nw = round4(s_im_rec.width * 0.70)
    nh = round4(s_im_rec.height * 0.70)
    s_res = s_im_rec.resize((nw, nh), Image.Resampling.LANCZOS)
    sprites.append(s_res)
    sprites_info.append(('P11_DustFade', (nw, nh)))

    # Skyline packing algorithm
    max_w, max_h = 1020, 1020
    rects = [(w, h, name, idx) for idx, (name, (w, h)) in enumerate(sprites_info)]
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

    arr_s = np.array(sheet_4x)
    arr_s[arr_s[:, :, 3] < 15] = 0
    return Image.fromarray(arr_s), sprites, small_images

frames = [
    # 0..2: 24413 (Trước khi xuất chiêu - Vòng xoáy năng lượng sấm sét tụ khí)
    [(-22, -30, 0, 0, 1)],
    [(-29, -36, 1, 0, 1)],
    [(-28, -38, 2, 0, 1)],
    # 3..7: Cú đấm khổng lồ đỏ tím lao từ trên trời xuống
    [(-26, -105, 3, 0, 1)],
    [(-25, -95, 4, 0, 1)],
    [(-24, -80, 5, 0, 1)],
    [(-24, -55, 5, 0, 1)],
    [(-24, -35, 5, 0, 1)],
    # 8..9: Nện đất địa chấn & cột năng lượng đỏ
    [(-31, -38, 6, 0, 1)],
    [(-31, -47, 7, 0, 1)],
    # 10..11: 24312 (Khi kết thúc chiêu đấm xuống đất - Vỡ toác đất đá địa chấn cực đại)
    [(-35, -40, 8, 0, 1)],
    [(-51, -52, 9, 0, 1)],
    # 12: Bụi tản biến
    [(-15, -16, 10, 0, 1)],
]

sequence = (
    [0, 0, 0] +           # 24413: Tụ khí cuồng phong 120ms
    [1, 1, 1] +           # 24413: Vòng xoáy bùng nổ 120ms
    [2, 2, 2] +           # 24413: Sấm sét mây cuộn 120ms
    [3, 3, 3] +           # Hé lộ nắm đấm khổng lồ từ mây 120ms
    [4, 4, 4] +           # Nắm đấm vươn xuống 120ms
    [5, 5] +              # Lao cao 80ms
    [6, 6] +              # Lao cực nhanh 80ms
    [7, 7, 7] +           # Chạm đất 120ms
    [8] * 8 +             # Nện đất địa chấn & vòng năng lượng vàng nổ tung: 8 ticks (~250ms)
    [9] * 18 +            # CỘT VÀNG NỔ ĐẤT BÙNG LÊN RỰC RỠ: 18 ticks (~550ms) - Nổ chậm lại rõ rệt, mãn nhãn!
    [10] * 8 +            # 24312: Vỡ toác đất đá bùng nổ: 8 ticks (~250ms)
    [11] * 24 +           # 24312: Vết nứt vỡ toác đất giữ lại trên mặt đất: 24 ticks (~750ms)
    [12] * 8              # Khói bụi tản biến dần: 8 ticks (~250ms)
)

def build_data(small_images):
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

def generate():
    sheet_4x, sprites, small_images = extract_and_assemble()
    data_bytes = build_data(small_images)
    print(f"Data binary size: {len(data_bytes)} bytes")

    # Multi-zoom levels
    w4, h4 = sheet_4x.size
    w1, h1 = w4 // 4, h4 // 4
    zooms = {
        'x4': sheet_4x,
        'x3': sheet_4x.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS),
        'x2': sheet_4x.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS),
        'x1': sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.LANCZOS),
        'x0': sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.LANCZOS),
    }

    for z, im_z in zooms.items():
        img_dir = os.path.join(BASE_DIR, z, 'img')
        dat_dir = os.path.join(BASE_DIR, z, 'data')
        os.makedirs(img_dir, exist_ok=True)
        os.makedirs(dat_dir, exist_ok=True)

        arr_z = np.array(im_z)
        alpha = arr_z[:, :, 3]
        rgb_im = Image.fromarray(arr_z[:, :, :3])
        q_rgb = rgb_im.quantize(colors=255, method=Image.Quantize.MEDIANCUT, dither=Image.Dither.NONE)
        pal = q_rgb.getpalette()[:255 * 3] + [0, 0, 0]
        q_arr = np.array(q_rgb)
        q_arr[alpha < 16] = 255

        im_pal = Image.fromarray(q_arr, mode='P')
        im_pal.putpalette(pal)
        im_pal.info['transparency'] = 255

        img_path = os.path.join(img_dir, '919.png')
        buf = io.BytesIO()
        im_pal.save(buf, format='PNG', compress_level=9, optimize=True)
        tmp_img = img_path + '.tmp'
        with open(tmp_img, 'wb') as f:
            f.write(buf.getvalue())
        os.replace(tmp_img, img_path)
        img_sz = os.path.getsize(img_path)

        dat_path = os.path.join(dat_dir, '919')
        tmp_dat = dat_path + '.tmp'
        with open(tmp_dat, 'wb') as f:
            f.write(data_bytes)
        os.replace(tmp_dat, dat_path)

        total_sz = img_sz + len(data_bytes)
        print(f"  [{z}] 919.png = {img_sz} bytes, total packet body = {total_sz} bytes (max 60000)")

    print("\n=== SUCCESS! Effect 919 with 24413 pre-cast + giant red-purple punch + 24312 earth burst deployed! ===")

if __name__ == '__main__':
    generate()
