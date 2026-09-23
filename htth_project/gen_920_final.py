"""
Effect 920 (Buff Thức Tỉnh Nika - Vibrant & Clean Alpha)
Features:
  1. Giữ 100% đồ họa gốc: Vòng xoay ma trận ma thuật 8 gai dưới chân + Cột Haki Bá Vương cuồn cuộn.
  2. Khử sạch toàn bộ vệt trắng background / bleed, phục hồi màu đỏ tím đậm (crimson/magenta) và lõi đen (pitch-black).
  3. Vẽ THÊM các luồng Haki Bá Vương uốn lượn tỏa rộng ra 2 bên và xung quanh 360 độ kèm sóng xung kích mặt đất.
  4. Đóng gói chuẩn Indexed PNG với bảng màu Median Cut và tRNS sạch tuyệt đối, dung lượng ~50KB (< 60KB).
"""
import numpy as np
from PIL import Image, ImageDraw
import math, os, struct

SCRATCH_PREV = 'C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch'
SCRATCH_OUT = 'C:/Users/admin/.gemini/antigravity-ide/brain/962f8d45-51ec-4554-8c92-12988262654e/scratch'

im_ring = Image.open(f'{SCRATCH_PREV}/ring_only.png')
im_clean = Image.open(f'{SCRATCH_PREV}/test_extract_clean.png')

# Clean white background contamination on Haki
arr_c = np.array(im_clean).astype(np.float64)
r, g, b, a = arr_c[:,:,0], arr_c[:,:,1], arr_c[:,:,2], arr_c[:,:,3]
excess_white = np.minimum(g, b)
new_r = np.clip(r + excess_white * 0.2, 0, 255)
new_g = np.clip(g - excess_white * 0.75, 0, 255)
new_b = np.clip(b - excess_white * 0.35, 0, 255)
clean_src = np.dstack([new_r, new_g, new_b, a])
is_black = (r < 60) & (g < 60) & (b < 60) & (a > 30)
clean_src[is_black, :3] = 0

# Clean ring white bleed
ring_src = np.array(im_ring).astype(np.float64)
r_r, r_g, r_b = ring_src[:,:,0], ring_src[:,:,1], ring_src[:,:,2]
r_excess = np.minimum(r_g, r_b)
ring_src[:,:,1] = np.clip(r_g - r_excess * 0.6, 0, 255)
ring_src[:,:,2] = np.clip(r_b - r_excess * 0.8, 0, 255)

RH, RW = ring_src.shape[:2]
ring_cx, ring_cy, aspect = 472, 125, 0.238

x0_h, y0_h = 32, 45
crop_h = 742 - 45
haki_arr = clean_src[y0_h:y0_h+crop_h, x0_h:x0_h+940].copy()
HH, HW = haki_arr.shape[:2]

CANVAS_H, CANVAS_W = 750, 940
RING_CX_C, RING_CY_C = 470, 695

num_frames = 6
frames_240 = []

tendrils = [
    (-160, 300, 0.45, 22, 0.0),
    (-135, 360, -0.35, 26, 0.2),
    (-105, 380, 0.4, 28, 0.4),
    (-75,  370, -0.4, 28, 0.6),
    (-45,  340, 0.35, 26, 0.8),
    (-20,  290, -0.45, 22, 0.1),
    (-175, 250, -0.3, 18, 0.5),
    (-5,   250, 0.3, 18, 0.7),
]

CX, CY = 470, 530

for fi in range(num_frames):
    alpha_rot = fi * (2.0 * math.pi / num_frames)
    canvas = np.zeros((CANVAS_H, CANVAS_W, 4), dtype=np.float64)

    # 1. ROTATING RING
    gy_c, gx_c = np.indices((CANVAS_H, CANVAS_W), dtype=np.float64)
    dx = gx_c - RING_CX_C
    dy = (gy_c - RING_CY_C) / aspect
    R = np.sqrt(dx**2 + dy**2)
    T = np.arctan2(dy, dx) - alpha_rot
    sx = R * np.cos(T) + ring_cx
    sy = R * np.sin(T) * aspect + ring_cy

    x0 = np.clip(np.floor(sx).astype(np.int32), 0, RW-2)
    x1 = x0 + 1
    y0 = np.clip(np.floor(sy).astype(np.int32), 0, RH-2)
    y1 = y0 + 1
    wx = np.clip((sx - x0)[:,:,None], 0.0, 1.0)
    wy = np.clip((sy - y0)[:,:,None], 0.0, 1.0)
    ring_rot = (ring_src[y0,x0]*(1-wx)*(1-wy) + ring_src[y0,x1]*wx*(1-wy) +
                ring_src[y1,x0]*(1-wx)*wy + ring_src[y1,x1]*wx*wy)
    ar = ring_rot[:,:,3:] / 255.0
    canvas[:,:,:3] = ring_rot[:,:,:3] * ar
    canvas[:,:,3:] = ar * 255

    # 2. SCROLL HAKI
    shift = fi * 15
    h_rolled = np.roll(haki_arr, -shift, axis=0)
    if shift > 0:
        h_rolled[HH-shift:, :, 3] = 0

    place_h = min(HH, CANVAS_H)
    ah = h_rolled[:place_h, :HW, 3:] / 255.0
    ao = canvas[:place_h, :HW, 3:] / 255.0
    an = ah + ao * (1 - ah)
    rgb = np.where(an > 0,
        (h_rolled[:place_h,:HW,:3]*ah + canvas[:place_h,:HW,:3]*ao*(1-ah)) / np.maximum(an, 1e-6),
        0)
    canvas[:place_h, :HW, :3] = rgb * 255
    canvas[:place_h, :HW, 3:] = np.clip(an * 255, 0, 255)

    base_img = Image.fromarray(np.clip(canvas, 0, 255).astype(np.uint8))
    
    # 3. EXTRA RADIATING HAKI STREAMS
    overlay = Image.new('RGBA', (CANVAS_W, CANVAS_H), (0, 0, 0, 0))
    draw = ImageDraw.Draw(overlay)
    t = fi / num_frames
    
    for sw_i in range(3):
        p = (t + sw_i / 3.0) % 1.0
        rad = 60 + p * 340
        al_sw = int(220 * (1.0 - p))
        if al_sw > 0:
            rx, ry = rad, rad * 0.32
            cy_sw = RING_CY_C - 20
            bbox = [CX - rx, cy_sw - ry, CX + rx, cy_sw + ry]
            draw.ellipse(bbox, outline=(230, 20, 70, al_sw), width=5)
            draw.ellipse([bbox[0]-2, bbox[1]-1, bbox[2]+2, bbox[3]+1], outline=(170, 0, 220, int(al_sw * 0.7)), width=3)
    
    for s_idx, (ang_deg, blen, curv, max_w, ph) in enumerate(tendrils):
        p_time = (t + ph) % 1.0
        surge = math.sin(p_time * math.pi)
        if surge <= 0.05: continue
        
        cur_len = blen * (0.65 + 0.45 * surge)
        cur_ang = math.radians(ang_deg + 8 * math.sin(p_time * 2 * math.pi))
        num_pts = 16
        pts = []
        for i_pt in range(num_pts):
            u = i_pt / (num_pts - 1)
            dist = u * cur_len
            side = curv * math.sin(u * math.pi) * 75.0
            zigzag = math.sin(u * 12.0 + fi * 2.5 + s_idx * 2.0) * (8.0 * (1.0 - u * 0.4))
            px = CX + dist * math.cos(cur_ang) - (side + zigzag) * math.sin(cur_ang)
            py = CY + dist * math.sin(cur_ang) + (side + zigzag) * math.cos(cur_ang)
            pts.append((px, py))
            
        for p_i in range(len(pts)-1):
            u = p_i / len(pts)
            w = max(2, int(max_w * (1.0 - u * 0.65) * surge))
            al = int(255 * surge * (1.0 - u * 0.2))
            draw.line([pts[p_i], pts[p_i+1]], fill=(180, 0, 230, int(al * 0.8)), width=w + 6)
            draw.line([pts[p_i], pts[p_i+1]], fill=(255, 20, 80, al), width=w + 2)
            draw.line([pts[p_i], pts[p_i+1]], fill=(10, 0, 15, int(al * 0.95)), width=max(1, w - 2))

    comp = Image.alpha_composite(base_img, overlay)
    img_240 = comp.resize((240, 240), Image.Resampling.LANCZOS)
    frames_240.append(img_240)

sheet = Image.new('RGBA', (720, 480), (0, 0, 0, 0))
for i, fr in enumerate(frames_240):
    sheet.paste(fr, ((i % 3) * 240, (i // 3) * 240))

sheet_4x = sheet.resize((600, 400), Image.Resampling.LANCZOS)

def build_data(small_imgs, frames, seq):
    out = bytearray()
    out.append(len(small_imgs))
    for s in small_imgs:
        for v in s:
            out.append(v)
    out.extend(struct.pack('>h', len(frames)))
    for f in frames:
        out.append(len(f))
        for p in f:
            out.extend(struct.pack('>h', p[0]))
            out.extend(struct.pack('>h', p[1]))
            out.append(p[2])
            out.append(0)
            out.append(0)
    out.append(len(seq))
    for s in seq:
        out.extend(struct.pack('>h', s))
    out.append(0)
    for _ in range(3):
        out.append(1)
        out.append(0)
    out.extend(bytes([0, 0, 0]))
    return bytes(out)

small_images = [
    [0,   0,   0, 50, 50],
    [1,  50,   0, 50, 50],
    [2, 100,   0, 50, 50],
    [3,   0,  50, 50, 50],
    [4,  50,  50, 50, 50],
    [5, 100,  50, 50, 50],
]
frame_list = [[(-25, -45, i, 0, 0)] for i in range(6)]
seq = [0, 1, 2, 3, 4, 5]
data_bytes = build_data(small_images, frame_list, seq)

base_dir = r'D:\project\GameHTTH\htth_project\data\template\skill'
w1, h1 = 150, 100
zooms = {
    'x4': (sheet_4x, 64),
    'x3': (sheet_4x.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 128),
    'x2': (sheet_4x.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 128),
    'x1': (sheet_4x.resize((w1, h1), Image.Resampling.BILINEAR), 128),
    'x0': (sheet_4x.resize((w1, h1), Image.Resampling.BILINEAR), 128),
}

def save_clean_indexed_png(im_rgba, out_path, n_colors=64):
    arr = np.array(im_rgba)
    alpha = arr[:, :, 3]
    is_trans = alpha < 16
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

for z, (im_z, ncol) in zooms.items():
    img_dir = os.path.normpath(os.path.join(base_dir, z, 'img'))
    dat_dir = os.path.normpath(os.path.join(base_dir, z, 'data'))
    os.makedirs(img_dir, exist_ok=True)
    os.makedirs(dat_dir, exist_ok=True)
    
    img_path = os.path.join(img_dir, '920.png')
    save_clean_indexed_png(im_z, img_path, n_colors=ncol)
    
    dat_path = os.path.join(dat_dir, '920')
    with open(dat_path, 'wb') as f:
        f.write(data_bytes)
        
    sz = os.path.getsize(img_path)
    print(f'[{z}] 920.png size: {sz} bytes (data: {len(data_bytes)}) -> total: {sz + len(data_bytes)}')

print('SUCCESS! Deployed vibrant 920 effect!')
