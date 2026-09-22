import numpy as np
from PIL import Image
import math, struct, os

SCRATCH_PREV = 'C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch'
SCRATCH_OUT = 'C:/Users/admin/.gemini/antigravity-ide/brain/992601b6-fcd2-41b8-819d-db234853d047/scratch'

im_ring = Image.open(f'{SCRATCH_PREV}/ring_only.png')
im_clean = Image.open(f'{SCRATCH_PREV}/test_extract_clean.png')
ring_src = np.array(im_ring).astype(np.float64)
clean_src = np.array(im_clean).astype(np.float64)

RH, RW = ring_src.shape[:2]   # 280, 940
ring_cx, ring_cy, aspect = 472, 125, 0.238

# HAKI: crop full haki region (Y=45..742, X=32..972)
x0_h, y0_h = 32, 45
crop_h = 742 - 45  # 697
haki_arr = clean_src[y0_h:y0_h+crop_h, x0_h:x0_h+940].copy().astype(np.float64)
HH, HW = haki_arr.shape[:2]
print(f'Haki crop: {HW}x{HH}')

# Alpha boost: haki pixels tend to be sparse, multiply alpha x2
haki_arr[:,:,3] = np.clip(haki_arr[:,:,3] * 2.0, 0, 255)

# Canvas: 750x940
CANVAS_H, CANVAS_W = 750, 940
RING_CX_C, RING_CY_C = 470, 695

num_frames = 6
frames_240 = []

for fi in range(num_frames):
    alpha_rot = fi * (2.0 * math.pi / num_frames)
    canvas = np.zeros((CANVAS_H, CANVAS_W, 4), dtype=np.float64)

    # === ROTATING RING ===
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

    # === SCROLL HAKI (15px/frame) ===
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

    img = Image.fromarray(np.clip(canvas, 0, 255).astype(np.uint8))
    img_240 = img.resize((240, 240), Image.Resampling.LANCZOS)
    frames_240.append(img_240)
    img_240.save(os.path.join(SCRATCH_OUT, f'eff920_v8_f{fi}.png'))
    print(f'Frame {fi} done')

# === BUILD SPRITE SHEET + DATA ===
sheet = Image.new('RGBA', (720, 480), (0, 0, 0, 0))
for i, fr in enumerate(frames_240):
    sheet.paste(fr, ((i % 3) * 240, (i // 3) * 240))

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
    [0,  0,  0, 60, 60],
    [1, 60,  0, 60, 60],
    [2, 120, 0, 60, 60],
    [3,  0, 60, 60, 60],
    [4, 60, 60, 60, 60],
    [5, 120,60, 60, 60],
]
frame_list = [[(-30, -50, i, 0, 0)] for i in range(6)]
seq = [0, 1, 2, 3, 4, 5]
data_bytes = build_data(small_images, frame_list, seq)
print(f'Data size: {len(data_bytes)} bytes')

base_dir = 'D:/project/GameHTTH/htth_project/data/template/skill'
w1, h1 = 180, 120
zooms = {
    'x4': sheet,
    'x3': sheet.resize((w1*3, h1*3), Image.Resampling.LANCZOS),
    'x2': sheet.resize((w1*2, h1*2), Image.Resampling.LANCZOS),
    'x1': sheet.resize((w1, h1),     Image.Resampling.BILINEAR),
    'x0': sheet.resize((w1, h1),     Image.Resampling.BILINEAR),
}
for z, im_z in zooms.items():
    img_dir = os.path.join(base_dir, z, 'img')
    dat_dir = os.path.join(base_dir, z, 'data')
    os.makedirs(img_dir, exist_ok=True)
    os.makedirs(dat_dir, exist_ok=True)
    arr_z = np.array(im_z)
    arr_z[arr_z[:,:,3] < 15] = 0
    im_q = Image.fromarray(arr_z).quantize(colors=120, method=Image.Quantize.FASTOCTREE)
    img_path = os.path.join(img_dir, '920.png')
    im_q.save(img_path, optimize=True)
    dat_path = os.path.join(dat_dir, '920')
    with open(dat_path, 'wb') as f:
        f.write(data_bytes)
    sz = os.path.getsize(img_path)
    print(f'  [{z}] {sz} bytes -> {img_path}')

print('DONE - Effect 920 v8 saved to game!')
