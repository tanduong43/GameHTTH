"""
Effect 920 (Buff Nika Awakening) – Dual-layer: ring behind + bolts in front
Features:
  1. RING layer (onTop=0): drawn BEHIND player – rotating ground ring with wide red spikes
  2. BOLT layer (onTop=1): drawn IN FRONT of player – Haki lightning bolts on both sides
  3. 8+2 segmented Haki bolt branches (220x228px at 4x), taller bolts
  4. Alternated across 6 frames (2-4 bolts per frame)
  5. Sprite sheet: 720x1120 at 4x = 180x280 at 1x (both < 255 byte limit)
  6. 2 parts per frame: ring(onTop=0) + bolts(onTop=1)
"""
import math, os, sys, struct, io
import numpy as np
from PIL import Image
from scipy import ndimage

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
BASE_DIR = os.path.normpath(os.path.join(SCRIPT_DIR, 'data', 'template', 'skill'))
SCRATCH = r'C:\Users\admin\.gemini\antigravity-ide\brain\c86bc279-4d82-458a-911a-7fcf750991f2\scratch'

# ============================================================
# 1. LOAD & SEGMENT HAKI BOLTS
# ============================================================
print("Loading clean source for Haki bolts...")
src_path = os.path.join(SCRATCH, 'test_extract_clean.png')
im_clean = Image.open(src_path)
src_full = np.array(im_clean)

# Haki crop: Y=45..690, X=32..972 -> 645 x 940
haki_raw = src_full[45:690, 32:972].copy()
HH, HW = haki_raw.shape[:2]
mask = haki_raw[:, :, 3] > 35

# Segment 8 branches using connected components above Y=500
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

# Boost contrast: deep black core
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

# Pre-render 10 bolt layers (8 original + 2 mirrored) at 220 wide x 228 tall (TALLER!)
target_w, target_h = 220, 228
bolt_layers = []
for i in range(len(bolt_masks)):
    b_img = np.zeros_like(haki_boosted, dtype=np.uint8)
    b_img[bolt_masks[i]] = haki_boosted[bolt_masks[i]].astype(np.uint8)
    im_scaled = Image.fromarray(b_img).resize((target_w, target_h), Image.Resampling.BILINEAR)
    bolt_layers.append(np.array(im_scaled).astype(float))

# Bolt 8: Left Giant (mirror of Bolt 6 = Right Giant)
b_rg = np.zeros_like(haki_boosted, dtype=np.uint8)
b_rg[bolt_masks[6]] = haki_boosted[bolt_masks[6]].astype(np.uint8)
bolt_layers.append(np.array(Image.fromarray(np.fliplr(b_rg)).resize((target_w, target_h), Image.Resampling.BILINEAR)).astype(float))

# Bolt 9: Right Main (mirror of Bolt 1 = Left Main)
b_lm = np.zeros_like(haki_boosted, dtype=np.uint8)
b_lm[bolt_masks[1]] = haki_boosted[bolt_masks[1]].astype(np.uint8)
bolt_layers.append(np.array(Image.fromarray(np.fliplr(b_lm)).resize((target_w, target_h), Image.Resampling.BILINEAR)).astype(float))

print(f"Pre-rendered {len(bolt_layers)} taller bolt layers (height {target_h}px)!")

# ============================================================
# 2. FRAME SCHEDULE (Same as v12: 2-4 bolts per frame)
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
# 3. DRAW ROTATING GROUND RING (Wide radiating red spikes)
# ============================================================
def draw_ground_ring(H, W, alpha_rot, cx=120.0, cy=236.0, aspect=0.245):
    gy, gx = np.indices((H, W), dtype=float)
    X = gx - cx
    Z = (gy - cy) / aspect
    R = np.sqrt(X**2 + Z**2)
    T = np.arctan2(Z, X)
    T_rot = T - alpha_rot

    ring = np.zeros((H, W, 4), dtype=float)
    num_spikes = 8

    # ── ULTRA-LONG thin red rays: 24 rays, reach 70px beyond outer rim ──
    num_ultra = 24
    ultra_w = np.maximum(0.0, np.cos(num_ultra * T_rot))**5.0
    r_ultra_max = 80.0 + 70.0 * ultra_w
    m_ultra = (R >= 80.0) & (R <= r_ultra_max)
    st_u = ((R - 80.0) / np.maximum(1.0, r_ultra_max - 80.0))[m_ultra]
    ring[m_ultra, 0] = 255
    ring[m_ultra, 1] = np.clip(10 + 60 * st_u, 0, 255)
    ring[m_ultra, 2] = np.clip(8 + 40 * st_u, 0, 255)
    ring[m_ultra, 3] = np.clip(200 * (1.0 - st_u**1.2), 0, 255)

    # ── 8 Medium thick red rays: reach 52px beyond outer rim ──
    num_med = 8
    med_w = np.maximum(0.0, np.cos(num_med * T_rot + math.pi / 24))**2.0
    r_med_max = 80.0 + 52.0 * med_w
    m_med_ray = (R >= 80.0) & (R <= r_med_max)
    st_m = ((R - 80.0) / np.maximum(1.0, r_med_max - 80.0))[m_med_ray]
    sw_m = med_w[m_med_ray]
    ring[m_med_ray, 0] = 255
    ring[m_med_ray, 1] = np.clip(15 + 100 * st_m * sw_m, 0, 255)
    ring[m_med_ray, 2] = np.clip(12 + 80 * st_m * sw_m, 0, 255)
    ring[m_med_ray, 3] = np.clip(240 * (1.0 - st_m**1.3), 0, 255)

    # ── Original 8 Primary thick Lightning Spikes (compact, bright) ──
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

    # Center core glow (soft transparent so player body is clear)
    m_core = (R < 28)
    core_t = R[m_core] / 28.0
    ring[m_core, 0] = 255
    ring[m_core, 1] = np.clip(160 + 60 * core_t, 0, 255)
    ring[m_core, 2] = np.clip(30 + 30 * core_t, 0, 255)
    ring[m_core, 3] = np.clip(50 + 90 * core_t, 0, 255)

    return ring

# ============================================================
# 4. RENDER 6 FRAMES – SEPARATED INTO RING & BOLT LAYERS
# ============================================================
# Frame size: 240x280 at 4x = 60x70 at 1x
# Ring layer: full 240x280 (ring at cy=236)
# Bolt layer: 240x280 (bolts from y=8 to y=236, bottom transparent)
print("Rendering 6 ring frames + 6 bolt frames (dual-layer)...")
ring_frames = []   # drawn BEHIND player (onTop=0)
bolt_frames = []   # drawn IN FRONT of player (onTop=1)

for fi in range(6):
    rot = fi * (2.0 * math.pi / 6.0)

    # ── Ring-only frame ──
    ring_arr = draw_ground_ring(280, 240, rot, cx=120.0, cy=236.0)
    ring_img = Image.fromarray(np.clip(ring_arr, 0, 255).astype(np.uint8))
    ring_frames.append(ring_img)

    # ── Bolt-only frame ──
    active_b = frame_schedules[fi]
    haki_comb = np.zeros((228, 220, 4), dtype=float)
    for b_idx in active_b:
        b_arr = bolt_layers[b_idx]
        ab = b_arr[:, :, 3:] / 255.0
        ac = haki_comb[:, :, 3:] / 255.0
        an = ab + ac * (1.0 - ab)
        rgb_n = np.where(an > 0,
            (b_arr[:, :, :3] * ab + haki_comb[:, :, :3] * ac * (1.0 - ab)) / np.maximum(an, 1e-6),
            0.0)
        haki_comb[:, :, :3] = rgb_n
        haki_comb[:, :, 3:] = an * 255.0

    bolt_canvas = np.zeros((280, 240, 4), dtype=np.uint8)
    bolt_canvas[8:236, 10:230] = np.clip(haki_comb, 0, 255).astype(np.uint8)
    bolt_frames.append(Image.fromarray(bolt_canvas))

    print(f"  Frame {fi}: {len(active_b)} bolts")

# Save animated GIF preview (composite ring+bolt for preview only)
preview_frames = []
for fi in range(6):
    r_arr = np.array(ring_frames[fi]).astype(float)
    b_arr = np.array(bolt_frames[fi]).astype(float)
    a_r = r_arr[:, :, 3:] / 255.0
    a_b = b_arr[:, :, 3:] / 255.0
    a_out = a_r + a_b * (1.0 - a_r)
    rgb_out = np.where(a_out > 0,
        (r_arr[:, :, :3] * a_r + b_arr[:, :, :3] * a_b * (1.0 - a_r)) / np.maximum(a_out, 1e-6),
        0.0)
    comp = np.zeros((280, 240, 4), dtype=np.uint8)
    comp[:, :, :3] = np.clip(rgb_out, 0, 255).astype(np.uint8)
    comp[:, :, 3] = np.clip(a_out[:, :, 0] * 255, 0, 255).astype(np.uint8)
    preview_frames.append(Image.fromarray(comp))

gif_path = os.path.join(SCRATCH, 'eff920_v12_tall_preview.gif')
preview_frames[0].save(gif_path, save_all=True, append_images=preview_frames[1:], duration=110, loop=0)
gif_proj = os.path.join(SCRIPT_DIR, 'eff920_v12_preview.gif')
preview_frames[0].save(gif_proj, save_all=True, append_images=preview_frames[1:], duration=110, loop=0)
print(f"Saved preview GIFs to {gif_path} and {gif_proj}")

# ============================================================
# 5. ASSEMBLE SPRITE SHEET (720x1120 at 4x = 180x280 at 1x)
# ============================================================
# y=   0..279: Ring frames 0,1,2  (each 240x280)
# y= 280..559: Ring frames 3,4,5
# y= 560..839: Bolt frames 0,1,2
# y= 840..1119: Bolt frames 3,4,5
sheet = Image.new('RGBA', (720, 1120), (0, 0, 0, 0))
for i, fr in enumerate(ring_frames):
    sheet.paste(fr, ((i % 3) * 240, (i // 3) * 280))
for i, fr in enumerate(bolt_frames):
    sheet.paste(fr, ((i % 3) * 240, 560 + (i // 3) * 280))

# ============================================================
# 6. BUILD BINARY DATA (2 parts per frame)
# ============================================================
def build_data():
    out = bytearray()
    # 12 SmallImages at 1x (sheet=180x280 at 1x, each cell=60x70)
    small_images = [
        # Ring frames (behind player) – row y=0 and y=70
        [0,   0,   0, 60, 70],  # ring 0
        [1,  60,   0, 60, 70],  # ring 1
        [2, 120,   0, 60, 70],  # ring 2
        [3,   0,  70, 60, 70],  # ring 3
        [4,  60,  70, 60, 70],  # ring 4
        [5, 120,  70, 60, 70],  # ring 5
        # Bolt frames (in front of player) – row y=140 and y=210
        [6,   0, 140, 60, 70],  # bolt 0
        [7,  60, 140, 60, 70],  # bolt 1
        [8, 120, 140, 60, 70],  # bolt 2
        [9,   0, 210, 60, 70],  # bolt 3
        [10,  60, 210, 60, 70], # bolt 4
        [11, 120, 210, 60, 70], # bolt 5
    ]
    out.append(len(small_images))
    for s in small_images:
        for v in s:
            out.append(v)

    # 6 frames, 3 parts each
    # dy=-59 places y=59 of the 70px-tall image at character feet (236/4=59)
    out.extend(struct.pack('>h', 6))
    for i in range(6):
        out.append(3)  # 3 parts per frame
        # Part 0: Ring – BEHIND player (ground ring with red spikes)
        out.extend(struct.pack('>h', -30))  # dx
        out.extend(struct.pack('>h', -59))  # dy
        out.append(i)     # idSmallImg = ring frame i
        out.append(0)     # flip
        out.append(0)     # onTop = 0 (BEHIND player)
        # Part 1: Bolts BEHIND player (back half visible through character)
        out.extend(struct.pack('>h', -30))  # dx
        out.extend(struct.pack('>h', -59))  # dy
        out.append(i + 6) # idSmallImg = bolt frame i
        out.append(0)     # flip
        out.append(0)     # onTop = 0 (BEHIND player → bolts wrap around back)
        # Part 2: Bolts IN FRONT of player (front half on top of character)
        out.extend(struct.pack('>h', -30))  # dx
        out.extend(struct.pack('>h', -59))  # dy
        out.append(i + 6) # idSmallImg = bolt frame i (same image!)
        out.append(0)     # flip
        out.append(1)     # onTop = 1 (IN FRONT of player → bolts wrap around front)

    # Sequence: loop frames 0→5
    seq = [0, 1, 2, 3, 4, 5]
    out.append(len(seq))
    for s in seq:
        out.extend(struct.pack('>h', s))

    # Trailing data
    out.append(0)  # typeupdate
    for _ in range(3):
        out.append(1)
        out.append(0)
    out.extend(bytes([0, 0, 0]))
    return bytes(out)

data_bytes = build_data()
print(f"Data binary size: {len(data_bytes)} bytes")

# ============================================================
# 7. SAVE TO ALL ZOOM LEVELS (x0..x4)
# ============================================================
def safe_write(path, data):
    import time
    for i in range(10):
        try:
            with open(path, 'wb') as f:
                f.write(data)
            return
        except OSError:
            time.sleep(0.3)
    with open(path, 'wb') as f:
        f.write(data)

def save_all_zooms():
    w4, h4 = sheet.size  # 720, 1120
    w1, h1 = w4 // 4, h4 // 4  # 180, 280

    zooms = {
        'x4': (sheet, 80),
        'x3': (sheet.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 80),
        'x2': (sheet.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 80),
        'x1': (sheet.resize((w1, h1), Image.Resampling.BILINEAR), 80),
        'x0': (sheet.resize((w1, h1), Image.Resampling.BILINEAR), 80),
    }

    for z, (im_z, n_col) in zooms.items():
        img_dir = os.path.join(BASE_DIR, z, 'img')
        dat_dir = os.path.join(BASE_DIR, z, 'data')
        os.makedirs(img_dir, exist_ok=True)
        os.makedirs(dat_dir, exist_ok=True)

        arr_z = np.array(im_z)
        arr_z[arr_z[:, :, 3] < 15] = 0
        im_clean_z = Image.fromarray(arr_z)
        im_q = im_clean_z.quantize(colors=n_col, method=Image.Quantize.FASTOCTREE)

        img_path = os.path.join(img_dir, '920.png')
        buf = io.BytesIO()
        im_q.save(buf, format='PNG', optimize=True)
        safe_write(img_path, buf.getvalue())
        img_sz = os.path.getsize(img_path)

        dat_path = os.path.join(dat_dir, '920')
        safe_write(dat_path, data_bytes)

        total_sz = img_sz + len(data_bytes)
        status = "✓" if total_sz < 60000 else "✗ OVER LIMIT!"
        print(f"  [{z}] 920.png = {img_sz} bytes, total = {total_sz} bytes {status}")

    print("\n=== SUCCESS! Effect 920 (ring behind + bolts in front) deployed to x0..x4! ===")

if __name__ == '__main__':
    save_all_zooms()
