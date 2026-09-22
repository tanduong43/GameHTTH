import os
import sys
import struct
import io
import numpy as np
from PIL import Image

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRIPT_DIR = r'D:\project\GameHTTH\htth_project'
BASE_DIR = os.path.join(SCRIPT_DIR, 'data', 'template', 'skill')
SRC_IMG = r'C:\Users\admin\.gemini\antigravity-ide\brain\a785e093-427f-476f-9051-2e79532b30ab\.user_uploaded\media_1790065917499.png'
SCRATCH = r'C:\Users\admin\.gemini\antigravity-ide\brain\a785e093-427f-476f-9051-2e79532b30ab\scratch'

SCALE = 0.72

def round4(val):
    return int(round(val / 4.0) * 4)

regions = [
    ('P0_Tornado',      12, 192,  48, 239),
    ('P1_FistEmerge',    0, 202, 295, 496),
    ('P2_FistExtend',    6, 190, 545, 736),
    ('P3_FistThrust',    8, 204, 816, 992),
    ('P4_FistRings',   205, 395,  30, 280),
    ('P5_BlastMid',    190, 395, 343, 607),
    ('P6_GroundCracks',239, 384, 665, 991),
    ('P7_BlastBig1',   400, 558,  16, 249),
    ('P8_BlastBig2',   400, 560, 251, 440),
    ('P9_BlastBig3',   401, 560, 440, 630),
    ('P10_RingFade',   432, 560, 672, 830),
    ('P11_DustFade',   464, 558, 846, 1024),
]

def extract_and_assemble():
    im = Image.open(SRC_IMG).convert('RGBA')
    arr = np.array(im)
    r, g, b = arr[:, :, 0].astype(float), arr[:, :, 1].astype(float), arr[:, :, 2].astype(float)
    dev = np.maximum.reduce([np.abs(r - g), np.abs(g - b), np.abs(r - b)])
    brightness = (r + g + b) / 3.0
    is_bg = (brightness >= 48) & (brightness <= 62) & (dev < 8)
    arr[is_bg, 3] = 0
    arr[arr[:, :, 3] < 15] = 0

    sprites = []
    for name, y0, y1, x0, x1 in regions:
        sub = arr[y0:y1, x0:x1]
        ys, xs = np.where(sub[:, :, 3] > 0)
        cropped = sub[ys.min():ys.max()+1, xs.min():xs.max()+1]
        s_im = Image.fromarray(cropped)
        nw = round4(s_im.width * SCALE)
        nh = round4(s_im.height * SCALE)
        sprites.append(s_im.resize((nw, nh), Image.Resampling.LANCZOS))

    row1 = sprites[:4]
    row2 = sprites[4:7]
    row3 = sprites[7:]

    r1_h = round4(max(s.height for s in row1))
    r2_h = round4(max(s.height for s in row2))
    r3_h = round4(max(s.height for s in row3))

    sheet_w = round4(max(sum(s.width for s in row1), sum(s.width for s in row2), sum(s.width for s in row3)))
    sheet_h = round4(r1_h + r2_h + r3_h)

    sheet_4x = Image.new('RGBA', (sheet_w, sheet_h), (0, 0, 0, 0))
    small_images = []

    def pack_row(row, y_offset):
        x = 0
        for s in row:
            sheet_4x.paste(s, (x, y_offset), s)
            small_images.append([len(small_images), x // 4, y_offset // 4, s.width // 4, s.height // 4])
            x += s.width

    pack_row(row1, 0)
    pack_row(row2, r1_h)
    pack_row(row3, r1_h + r2_h)

    arr_s = np.array(sheet_4x)
    arr_s[arr_s[:, :, 3] < 15] = 0
    return Image.fromarray(arr_s), sprites, small_images

frames = [
    # Frame 0: Tornado cloud
    [(-17, -80, 0, 0, 1)],
    # Frame 1: Fist emerge
    [(-18, -80, 1, 0, 1)],
    # Frame 2: Fist extend
    [(-17, -75, 2, 0, 1)],
    # Frame 3: Plunging high
    [(-16, -70, 3, 0, 1)],
    # Frame 4: Plunging mid
    [(-16, -52, 3, 0, 1)],
    # Frame 5: Plunging touch ground!
    [(-16, -35, 3, 0, 1)],
    # Frame 6: Ground cracks + Fist with golden shockwave rings
    [(-29, -20, 6, 0, 1), (-22, -34, 4, 0, 1)],
    # Frame 7: Eruption pillar
    [(-24, -37, 5, 0, 1)],
    # Frame 8: Mega 3-spike golden blast
    [(-17, -29, 8, 0, 1)],
    # Frame 9: Blast dispersing
    [(-17, -29, 9, 0, 1)],
    # Frame 10: Ring residue
    [(-14, -23, 10, 0, 1)],
    # Frame 11: Dust fading
    [(-17, -17, 11, 0, 1)],
]

sequence = (
    [0, 0, 0, 0] +        # Mây tụ 160ms
    [1, 1, 1] +           # Hé lộ 120ms
    [2, 2, 2] +           # Vươn xuống 120ms
    [3, 3] +              # Lao cao 80ms
    [4, 4] +              # Lao nhanh 80ms
    [5, 5] +              # Chạm đất 80ms
    [6, 6, 6, 6] +        # Nện đất địa chấn + vòng vàng 160ms
    [7, 7, 7, 7] +        # Nổ cột vàng 160ms
    [8, 8, 8, 8, 8, 8] +  # Nổ cực đại 3 cột 240ms
    [9, 9, 9, 9] +        # Nổ tản 160ms
    [10, 10, 10, 10] +    # Vòng lửa tan dần 160ms
    [11, 11, 11, 11]      # Bụi tan 160ms
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

    # Generate animated GIF preview
    cx, cy = 200, 320
    def make_canvas(parts):
        canv = Image.new('RGBA', (400, 360), (28, 30, 36, 255))
        for dx, dy, idSmall, flip, onTop in parts:
            sp = sprites[idSmall]
            canv.paste(sp, (cx + dx * 4, cy + dy * 4), sp)
        return canv

    gif_frames = [make_canvas(frames[s]) for s in sequence]
    os.makedirs(SCRATCH, exist_ok=True)
    gif_path = os.path.join(SCRATCH, 'nika919_straight_punch_preview.gif')
    gif_frames[0].save(gif_path, save_all=True, append_images=gif_frames[1:], duration=40, loop=0)
    print(f"Saved {gif_path}")

    # Zooms
    w4, h4 = sheet_4x.size
    w1, h1 = w4 // 4, h4 // 4
    zooms = {
        'x4': (sheet_4x, 48),
        'x3': (sheet_4x.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 96),
        'x2': (sheet_4x.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 128),
        'x1': (sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 256),
        'x0': (sheet_4x.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR), 256),
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

        img_path = os.path.join(img_dir, '919.png')
        buf = io.BytesIO()
        im_q.save(buf, format='PNG', compress_level=9, optimize=True)
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

    print("\n=== SUCCESS! Effect 919 with new image deployed! ===")

if __name__ == '__main__':
    generate()
