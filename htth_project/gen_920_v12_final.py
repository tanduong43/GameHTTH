"""
Effect 920 (Buff Nika Awakening) - Combined v14
Features:
  1. Original Anime Haki lightning bolts + Rotating 8-spike ground ring
  2. PLUS omnidirectional radiating Haki shockwaves & energy arcs expanding outward 360 degrees
"""
import math, os, sys, struct, io
import numpy as np
from PIL import Image, ImageDraw
from scipy import ndimage

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRATCH_PREV = 'C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch'
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
BASE_DIR = os.path.normpath(os.path.join(SCRIPT_DIR, 'data', 'template', 'skill'))
SCRATCH = r'C:\Users\admin\.gemini\antigravity-ide\brain\962f8d45-51ec-4554-8c92-12988262654e\scratch'

# ============================================================
# 1. LOAD & SEGMENT HAKI BOLTS
# ============================================================
print("Loading source...")
im_clean = Image.open(f'{SCRATCH_PREV}/test_extract_clean.png')
src_full = np.array(im_clean)

# Haki crop: Y=45..690, X=32..972 -> 645 x 940
haki_raw = src_full[45:690, 32:972].copy()
HH, HW = haki_raw.shape[:2]
mask = haki_raw[:, :, 3] > 35

# Segment 8 branches using connected components above Y=500
print("Segmenting haki bolts...")
above = mask.copy()
above[500:, :] = False
labeled, num_features = ndimage.label(above)
sizes = ndimage.sum(above, labeled, range(1, num_features + 1))

seed_ids = [idx + 1 for idx, s in enumerate(sizes) if s > 800]
seed_map = np.zeros_like(labeled)
for new_id, sid in enumerate(seed_ids, 1):
    seed_map[labeled == sid] = new_id

# Propagate labels to full mask via nearest-seed distance
dist, indices = ndimage.distance_transform_edt(seed_map == 0, return_indices=True)
full_labeled = seed_map[tuple(indices)]
full_labeled[~mask] = 0

# Sort 8 bolts left-to-right by X center of mass
coms = []
for i in range(1, len(seed_ids) + 1):
    m = (full_labeled == i)
    y_com, x_com = ndimage.center_of_mass(m)
    coms.append((i, x_com, y_com))
coms.sort(key=lambda x: x[1])
sorted_orig_ids = [c[0] for c in coms]
bolt_masks = [(full_labeled == orig_id) for orig_id in sorted_orig_ids]
print(f"  Found {len(bolt_masks)} bolt branches")

# Boost contrast: pure black cores
haki_boosted = haki_raw.copy().astype(float)
is_core_black = ((haki_boosted[:, :, 3] > 60) &
                 (haki_boosted[:, :, 0] < 55) &
                 (haki_boosted[:, :, 1] < 55) &
                 (haki_boosted[:, :, 2] < 55))
haki_boosted[is_core_black, :3] = 0.0

# Fade near bottom (ground ring zone)
gy_h = np.indices((HH, HW))[0]
base_fade = np.clip((640 - gy_h) / 40.0, 0.0, 1.0)
haki_boosted[:, :, 3] *= base_fade

# Pre-render 10 bolt layers (8 original + 2 mirrored) at 220x156
bolt_layers = []
for i in range(len(bolt_masks)):
    b_img = np.zeros_like(haki_boosted, dtype=np.uint8)
    b_img[bolt_masks[i]] = haki_boosted[bolt_masks[i]].astype(np.uint8)
    im_scaled = Image.fromarray(b_img).resize((220, 156), Image.Resampling.BILINEAR)
    bolt_layers.append(np.array(im_scaled).astype(float))

# Bolt 8: Left Giant (mirror of Bolt 6 = Right Giant)
b_rg = np.zeros_like(haki_boosted, dtype=np.uint8)
b_rg[bolt_masks[6]] = haki_boosted[bolt_masks[6]].astype(np.uint8)
im_lg = Image.fromarray(np.fliplr(b_rg)).resize((220, 156), Image.Resampling.BILINEAR)
bolt_layers.append(np.array(im_lg).astype(float))

# Bolt 9: Right Main (mirror of Bolt 1 = Left Main)
b_lm = np.zeros_like(haki_boosted, dtype=np.uint8)
b_lm[bolt_masks[1]] = haki_boosted[bolt_masks[1]].astype(np.uint8)
im_rm = Image.fromarray(np.fliplr(b_lm)).resize((220, 156), Image.Resampling.BILINEAR)
bolt_layers.append(np.array(im_rm).astype(float))

print(f"  Pre-rendered {len(bolt_layers)} bolt layers")

# ============================================================
# 2. FRAME SCHEDULE
# ============================================================
frame_schedules = [
    [1, 6],          # Frame 0: Left Main + Right Giant
    [8, 4, 7],       # Frame 1: Left Giant(mirror) + Center-Low + Far Right
    [3, 6],          # Frame 2: Left-High + Right Giant
    [0, 2, 5, 9],    # Frame 3: Far Left + Mid-Left + Center-High + Right Main(mirror)
    [8, 7],          # Frame 4: Left Giant(mirror) + Far Right
    [1, 4, 6],       # Frame 5: Left Main + Center-Low + Right Giant
]

# ============================================================
# 3. DRAW ROTATING GROUND RING
# ============================================================
def draw_ground_ring(H, W, alpha_rot, cx=120.0, cy=198.0, aspect=0.245):
    gy, gx = np.indices((H, W), dtype=float)
    X = gx - cx
    Z = (gy - cy) / aspect
    R = np.sqrt(X**2 + Z**2)
    T = np.arctan2(Z, X)
    T_rot = T - alpha_rot

    ring = np.zeros((H, W, 4), dtype=float)
    num_spikes = 8

    # Outer 8 Primary Lightning Spikes
    spike_w = np.maximum(0.0, np.cos(num_spikes * T_rot))**2.8
    r_spike_max = 78.0 + 26.0 * spike_w
    m_spike = (R >= 78.0) & (R <= r_spike_max)
    st = ((R - 78.0) / np.maximum(1.0, r_spike_max - 78.0))[m_spike]
    sw = spike_w[m_spike]
    ring[m_spike, 0] = 255
    ring[m_spike, 1] = np.clip(18 + 225 * st * sw, 0, 255)
    ring[m_spike, 2] = np.clip(28 + 225 * st * sw, 0, 255)
    ring[m_spike, 3] = np.clip(255 * (1.0 - st**1.5), 0, 255)

    # 8 Secondary Spikes (staggered)
    spike_w2 = np.maximum(0.0, np.cos(num_spikes * T_rot + math.pi / num_spikes))**3.5
    r_spike2_max = 78.0 + 13.0 * spike_w2
    m_spike2 = (R >= 78.0) & (R <= r_spike2_max) & (~m_spike)
    st2 = ((R - 78.0) / np.maximum(1.0, r_spike2_max - 78.0))[m_spike2]
    ring[m_spike2, 0] = 240
    ring[m_spike2, 1] = 20
    ring[m_spike2, 2] = 25
    ring[m_spike2, 3] = np.clip(220 * (1.0 - st2**1.2), 0, 255)

    # Outer red rim
    ring[(R >= 77) & (R <= 80)] = [230, 15, 15, 255]
    # Outer yellow band
    ring[(R >= 66) & (R < 77)] = [255, 235, 30, 255]
    # Mid red rim
    ring[(R >= 63) & (R < 66)] = [220, 15, 15, 255]

    # Mid golden ring with 8 rotating flame notches
    m_mid = (R >= 48) & (R < 63)
    flame = np.cos(num_spikes * T_rot)
    ring[m_mid, 0] = np.where(flame[m_mid] > 0.12, 255, 245)
    ring[m_mid, 1] = np.where(flame[m_mid] > 0.12, 215, 160)
    ring[m_mid, 2] = np.where(flame[m_mid] > 0.12, 45, 12)
    ring[m_mid, 3] = 255

    # Inner red rim
    ring[(R >= 45) & (R < 48)] = [220, 15, 15, 255]
    # Inner yellow band
    ring[(R >= 30) & (R < 45)] = [255, 230, 50, 255]

    # Inner 8 rotating teeth
    teeth_w = np.maximum(0.0, np.cos(num_spikes * T_rot + math.pi / 8))**2.0
    r_teeth_max = 30.0 + 9.0 * teeth_w
    m_teeth = (R >= 27) & (R <= r_teeth_max)
    ring[m_teeth] = [255, 255, 220, 255]

    # Center core glow
    m_core = (R < 28)
    core_t = R[m_core] / 28.0
    ring[m_core, 0] = 255
    ring[m_core, 1] = np.clip(160 + 60 * core_t, 0, 255)
    ring[m_core, 2] = np.clip(30 + 30 * core_t, 0, 255)
    ring[m_core, 3] = np.clip(50 + 90 * core_t, 0, 255)

    return ring

# ============================================================
# 4. DRAW RADIATING HAKI WAVES AROUND CHARACTER
# ============================================================
def draw_radiating_haki_waves(H, W, fi, num_frames=6, cx=120, cy=140):
    im_wave = Image.new('RGBA', (W, H), (0, 0, 0, 0))
    d = ImageDraw.Draw(im_wave)
    t = fi / float(num_frames)

    # Concentric expanding Haki shockwave rings
    for r_i in range(2):
        rt = (t + r_i / 2.0) % 1.0
        rad_x = 24 + rt * 80
        rad_y = 16 + rt * 56
        alpha = int(220 * math.sin(rt * math.pi))
        
        d.ellipse([cx - rad_x - 3, cy - rad_y - 2, cx + rad_x + 3, cy + rad_y + 2],
                  outline=(255, int(20 + 40 * (1 - rt)), int(80 + 120 * rt), int(alpha * 0.4)), width=3)
        d.ellipse([cx - rad_x, cy - rad_y, cx + rad_x, cy + rad_y],
                  outline=(255, int(100 * (1 - rt)), int(220 * rt + 30), alpha), width=2)
        if alpha > 70:
            d.arc([cx - rad_x, cy - rad_y, cx + rad_x, cy + rad_y],
                  start=20, end=160, fill=(255, 240, 255, int(alpha * 0.8)), width=2)

    # Outward branching radial Haki lightning tendrils
    num_tendrils = 6
    for k in range(num_tendrils):
        base_ang = (k / float(num_tendrils)) * 2 * math.pi + t * 0.8
        t_len = 35 + 25 * math.sin(k * 2.1 + t * 2 * math.pi)
        pts = [(cx + math.cos(base_ang) * 15, cy + math.sin(base_ang) * 12)]
        for s in range(1, 4):
            st = s / 3.0
            jag = 8 * math.sin(s * 3.1 + k * 2.3 + fi)
            ang = base_ang + jag * 0.04
            dist = 15 + st * (t_len - 15)
            nx = cx + math.cos(ang) * dist + jag * math.sin(base_ang)
            ny = cy + math.sin(ang) * dist * 0.75 - jag * math.cos(base_ang)
            pts.append((nx, ny))
            
        for i in range(len(pts) - 1):
            p1, p2 = pts[i], pts[i+1]
            w = max(1, int(3 * (1.0 - i / float(len(pts)))))
            d.line([p1, p2], fill=(255, 20, 80, 200), width=w + 3)
            d.line([p1, p2], fill=(15, 2, 20, 255), width=w + 1)
            d.line([p1, p2], fill=(255, 220, 250, 230), width=max(1, w - 1))
            
    # Bursting spark particles flying outward
    for p_i in range(8):
        p_ang = (p_i / 8.0) * 2 * math.pi + p_i * 1.4
        p_dist = 18 + (t * 60 + p_i * 8) % 65
        px = cx + math.cos(p_ang) * p_dist
        py = cy + math.sin(p_ang) * p_dist * 0.75
        pr = max(1, int(2.5 * (1.0 - p_dist / 85.0)))
        d.ellipse([px - pr, py - pr, px + pr, py + pr],
                  fill=(255, 160, 230, int(230 * (1.0 - p_dist / 85.0))))

    return np.array(im_wave).astype(float)

# ============================================================
# 5. RENDER & COMPOSITE 6 FRAMES
# ============================================================
print("Rendering 6 frames with added radiating Haki waves...")
frames_240 = []
for fi in range(6):
    rot = fi * (2.0 * math.pi / 6.0)
    ring_layer = draw_ground_ring(240, 240, rot)
    wave_layer = draw_radiating_haki_waves(240, 240, fi)

    # Composite active bolts
    active_b = frame_schedules[fi]
    haki_combined = np.zeros((156, 220, 4), dtype=float)
    for b_idx in active_b:
        b_arr = bolt_layers[b_idx]
        ab = b_arr[:, :, 3:] / 255.0
        ac = haki_combined[:, :, 3:] / 255.0
        an = ab + ac * (1.0 - ab)
        rgb_n = np.where(an > 0,
            (b_arr[:, :, :3] * ab + haki_combined[:, :, :3] * ac * (1.0 - ab)) / np.maximum(an, 1e-6),
            0.0)
        haki_combined[:, :, :3] = rgb_n
        haki_combined[:, :, 3:] = an * 255.0

    # Place haki & radiating waves on canvas
    canvas = ring_layer.copy()
    
    # Blend wave_layer over ring
    aw = wave_layer[:, :, 3:] / 255.0
    ar = canvas[:, :, 3:] / 255.0
    a_rw = aw + ar * (1.0 - aw)
    rgb_rw = np.where(a_rw > 0,
        (wave_layer[:, :, :3] * aw + canvas[:, :, :3] * ar * (1.0 - aw)) / np.maximum(a_rw, 1e-6),
        0.0)
    canvas[:, :, :3] = rgb_rw
    canvas[:, :, 3:] = a_rw * 255.0

    # Place main source Haki bolts
    haki_canvas = np.zeros((240, 240, 4), dtype=float)
    haki_canvas[44:200, 10:230] = haki_combined

    # Composite Haki over Ring + Waves
    ah = haki_canvas[:, :, 3:] / 255.0
    ac = canvas[:, :, 3:] / 255.0
    a_out = ah + ac * (1.0 - ah)
    rgb_out = np.where(a_out > 0,
        (haki_canvas[:, :, :3] * ah + canvas[:, :, :3] * ac * (1.0 - ah)) / np.maximum(a_out, 1e-6),
        0.0)
    frame_final = np.zeros((240, 240, 4), dtype=np.uint8)
    frame_final[:, :, :3] = np.clip(rgb_out, 0, 255).astype(np.uint8)
    frame_final[:, :, 3] = np.clip(a_out[:, :, 0] * 255, 0, 255).astype(np.uint8)

    frames_240.append(Image.fromarray(frame_final))
    print(f"  Frame {fi}: {len(active_b)} bolts + radiating Haki waves")

# ============================================================
# 6. BUILD SPRITE SHEET (3x2, 720x480)
# ============================================================
print("Building sprite sheet...")
sheet = Image.new('RGBA', (720, 480), (0, 0, 0, 0))
for i, fr in enumerate(frames_240):
    sheet.paste(fr, ((i % 3) * 240, (i // 3) * 240))

# ============================================================
# 7. BUILD BINARY DATA
# ============================================================
def build_data():
    out = bytearray()
    small_images = [
        [0,   0, 0, 60, 60],
        [1,  60, 0, 60, 60],
        [2, 120, 0, 60, 60],
        [3,   0, 60, 60, 60],
        [4,  60, 60, 60, 60],
        [5, 120, 60, 60, 60],
    ]
    out.append(len(small_images))
    for s in small_images:
        for v in s:
            out.append(v)

    out.extend(struct.pack('>h', 6))
    for i in range(6):
        out.append(1)
        out.extend(struct.pack('>h', -30))
        out.extend(struct.pack('>h', -50))
        out.append(i)
        out.append(0)
        out.append(1)

    seq = [0, 1, 2, 3, 4, 5]
    out.append(len(seq))
    for s in seq:
        out.extend(struct.pack('>h', s))

    out.append(0)
    for _ in range(3):
        out.append(1)
        out.append(0)
    out.extend(bytes([0, 0, 0]))
    return bytes(out)

# ============================================================
# 8. DEPLOY ALL ZOOMS
# ============================================================
def generate():
    data_bytes = build_data()
    print(f"Data binary size: {len(data_bytes)} bytes")

    os.makedirs(SCRATCH, exist_ok=True)
    gif_path = os.path.join(SCRATCH, 'nika920_combined_radiating_haki.gif')
    frames_240[0].save(gif_path, save_all=True, append_images=frames_240[1:], duration=60, loop=0)
    print(f"Saved preview GIF: {gif_path}")

    w4, h4 = sheet.size
    w1, h1 = w4 // 4, h4 // 4
    zooms = {
        'x4': (sheet, 100),
        'x3': (sheet.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 100),
        'x2': (sheet.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 100),
        'x1': (sheet.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 128),
        'x0': (sheet.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 128),
    }

    for z, (im_z, n_col) in zooms.items():
        img_dir = os.path.join(BASE_DIR, z, 'img')
        dat_dir = os.path.join(BASE_DIR, z, 'data')
        os.makedirs(img_dir, exist_ok=True)
        os.makedirs(dat_dir, exist_ok=True)

        arr_z = np.array(im_z)
        arr_z[arr_z[:, :, 3] < 15] = [0, 0, 0, 0]
        im_clean_z = Image.fromarray(arr_z)
        im_q = im_clean_z.quantize(colors=n_col, method=Image.Quantize.FASTOCTREE)

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

    print("\n=== SUCCESS! Effect 920 (Original Bolts + Ground Ring + Radiating Haki Waves) Deployed! ===")

if __name__ == '__main__':
    generate()
