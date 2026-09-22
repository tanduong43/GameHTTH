"""
Effect 920 (Buff Nika Awakening) - FINAL v12
Fixes:
  1. Vong tron xoay ro rang: 8-spike ring, 60deg/frame rotation
  2. Tia haki khong blink: 8 bolt branches segmented from source,
     alternated across 6 frames (2-4 bolts per frame, different each frame)
"""
import math, os, sys, struct
import numpy as np
from PIL import Image
from scipy import ndimage

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRATCH_PREV = 'C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch'
SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
BASE_DIR = os.path.normpath(os.path.join(SCRIPT_DIR, 'data', 'template', 'skill'))

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
# Each frame shows 2-4 bolts, different each frame, never same set twice
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
# 4. RENDER 6 FRAMES
# ============================================================
print("Rendering 6 frames...")
frames_240 = []
for fi in range(6):
    rot = fi * (2.0 * math.pi / 6.0)
    ring_layer = draw_ground_ring(240, 240, rot)

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

    # Place haki on canvas
    canvas = ring_layer.copy()
    haki_canvas = np.zeros((240, 240, 4), dtype=float)
    haki_canvas[44:200, 10:230] = haki_combined

    # Composite: Haki over Ring
    a_h = haki_canvas[:, :, 3:] / 255.0
    a_r = canvas[:, :, 3:] / 255.0
    a_out = a_h + a_r * (1.0 - a_h)
    rgb_out = np.where(a_out > 0,
        (haki_canvas[:, :, :3] * a_h + canvas[:, :, :3] * a_r * (1.0 - a_h)) / np.maximum(a_out, 1e-6),
        0.0)
    frame_final = np.zeros((240, 240, 4), dtype=np.uint8)
    frame_final[:, :, :3] = np.clip(rgb_out, 0, 255).astype(np.uint8)
    frame_final[:, :, 3] = np.clip(a_out[:, :, 0] * 255, 0, 255).astype(np.uint8)

    frames_240.append(Image.fromarray(frame_final))
    print(f"  Frame {fi}: {len(active_b)} bolts")

# ============================================================
# 5. BUILD SPRITE SHEET (3x2, 720x480)
# ============================================================
print("Building sprite sheet...")
sheet = Image.new('RGBA', (720, 480), (0, 0, 0, 0))
for i, fr in enumerate(frames_240):
    sheet.paste(fr, ((i % 3) * 240, (i // 3) * 240))

# ============================================================
# 6. BUILD BINARY DATA
# ============================================================
def build_data():
    out = bytearray()
    # SmallImages: 6 cells at 1x = 60x60
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

    # Frames: 6 frames, 1 part each
    # dx=-30, dy=-50 centers effect at player feet
    out.extend(struct.pack('>h', 6))
    for i in range(6):
        out.append(1)  # 1 part per frame
        out.extend(struct.pack('>h', -30))  # dx
        out.extend(struct.pack('>h', -50))  # dy
        out.append(i)   # idSmallImg
        out.append(0)   # flip
        out.append(1)   # onTop = 1 (render on top of player)

    # Sequence: 6 steps
    seq = [0, 1, 2, 3, 4, 5]
    out.append(len(seq))
    for s in seq:
        out.extend(struct.pack('>h', s))

    # Trailing data: typeupdate byte + 3 frameChar arrays + 3 indexSplash bytes
    out.append(0)  # typeupdate
    for _ in range(3):
        out.append(1)  # frameChar length
        out.append(0)  # frameChar value
    out.extend(bytes([0, 0, 0]))  # indexSplash
    return bytes(out)

data_bytes = build_data()
print(f"  Data size: {len(data_bytes)} bytes")

# ============================================================
# 7. SAVE TO ALL ZOOM LEVELS
# ============================================================
print("Saving to game data...")
w4, h4 = sheet.size  # 720, 480
w1, h1 = w4 // 4, h4 // 4  # 180, 120

zooms = {
    'x4': sheet,
    'x3': sheet.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS),
    'x2': sheet.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS),
    'x1': sheet.resize((w1, h1), Image.Resampling.BILINEAR),
    'x0': sheet.resize((w1, h1), Image.Resampling.BILINEAR),
}

for z, im_z in zooms.items():
    img_dir = os.path.join(BASE_DIR, z, 'img')
    dat_dir = os.path.join(BASE_DIR, z, 'data')
    os.makedirs(img_dir, exist_ok=True)
    os.makedirs(dat_dir, exist_ok=True)

    # Clean near-transparent pixels, quantize to reduce size
    arr_z = np.array(im_z)
    arr_z[arr_z[:, :, 3] < 15] = 0
    im_clean = Image.fromarray(arr_z)
    im_q = im_clean.quantize(colors=100, method=Image.Quantize.FASTOCTREE)

    img_path = os.path.join(img_dir, '920.png')
    im_q.save(img_path, optimize=True)
    sz = os.path.getsize(img_path)

    dat_path = os.path.join(dat_dir, '920')
    with open(dat_path, 'wb') as f:
        f.write(data_bytes)

    print(f"  [{z}] img={sz} bytes, data={len(data_bytes)} bytes")

print("\n=== DONE! Effect 920 v12 saved to game! ===")
print("Ring: 8 spikes rotating 60deg/frame")
print("Haki: 8+2 bolt branches alternating across 6 frames")
print("onTop=0: renders below player character")
