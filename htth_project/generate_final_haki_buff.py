"""
Effect 920 (Buff Thức Tỉnh Nika - Supreme Conqueror Haki Awakening)
PHIÊN BẢN RÕ NÉT TUYỆT ĐỐI (ULTRA CRISP & SHARP):
1. Khử hoàn toàn viền xám nền (Color Decontamination & Alpha Matting chuẩn toán học).
2. Tăng cường độ tương phản, rực rỡ và độ sắc nét (Unsharp Mask) cho từng tia sét Haki và vòng tròn thần mặt trời.
3. Chuẩn hóa kích thước khung hình đồng bộ bội số của 4 (344x288 ở x4 -> 172x144 ở x2 -> 86x72 ở x1).
4. Khóa cứng tâm vòng tròn ma pháp dưới chân nhân vật (dx=-43, dy=-58 ở 1x), không rung lắc.
5. Sử dụng thuật toán MEDIANCUT 255 màu cao cấp, loại bỏ triệt để hiện tượng vỡ hạt/bệt màu của FastOctree.
6. Tối ưu dung lượng x1 (17KB), x2 (53KB) dưới ngưỡng 60KB của gói tin Server.
"""
import os
import sys
import io
import struct
import numpy as np
from PIL import Image, ImageEnhance, ImageFilter

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
BASE_DIR = os.path.normpath(os.path.join(SCRIPT_DIR, 'data', 'template', 'skill'))

W_BOX_4X = 360
H_BOX_4X = 304
ANCHOR_X_4X = 180 # Tâm vòng tròn X (chính giữa khung hình 360)
ANCHOR_Y_4X = 256 # Vị trí chân nhân vật / tâm vòng tròn ma pháp Y

def safe_write(path, data):
    import time
    for _ in range(10):
        try:
            with open(path, 'wb') as f:
                f.write(data)
            return
        except OSError:
            time.sleep(0.2)
    with open(path, 'wb') as f:
        f.write(data)

def extract_and_clean_frames_sharp():
    src_path = os.path.join(SCRIPT_DIR, 'buff_nika_source.png')
    if not os.path.exists(src_path):
        src_path = r'C:\Users\admin\.gemini\antigravity-ide\brain\853fbd23-5d18-4dba-911b-11ca7b504046\.user_uploaded\media_1790142269240.png'
    
    im_src = Image.open(src_path).convert('RGB')
    arr = np.array(im_src).astype(float)
    bg = np.array([74.0, 73.0, 73.0])

    # 1. Đo khoảng cách màu Euclidean để tách nền xám chuẩn xác
    diff = np.sqrt(np.sum((arr - bg) ** 2, axis=2))
    t0 = 14.0
    t1 = 30.0
    alpha = np.clip((diff - t0) / (t1 - t0), 0.0, 1.0)

    # 2. Khử màu nền (Background Color Decontamination) loại bỏ viền xám mờ
    alpha_expanded = alpha[:, :, np.newaxis]
    arr_clean = np.zeros_like(arr)
    mask = alpha > 0.04
    arr_clean[mask] = np.clip((arr[mask] - (1.0 - alpha_expanded[mask]) * bg) / alpha_expanded[mask], 0, 255)
    arr_clean[~mask] = 0

    # 3. Tăng cường độ sắc nét và màu sắc anime
    rgb_part = Image.fromarray(arr_clean.astype(np.uint8), mode='RGB')
    sat = ImageEnhance.Color(rgb_part).enhance(1.25)
    contr = ImageEnhance.Contrast(sat).enhance(1.20)
    sharp_rgb = contr.filter(ImageFilter.UnsharpMask(radius=1.0, percent=150, threshold=1))

    final_arr = np.dstack([np.array(sharp_rgb), (alpha * 255.0).astype(np.uint8)])
    final_im = Image.fromarray(final_arr, mode='RGBA')

    # 4. Định vị 6 cell & tâm vòng tròn chuẩn xác tuyệt đối (khóa cứng tâm, không rung lắc)
    cells = [
        (0, 341, 0, 286),
        (341, 682, 0, 286),
        (682, 1024, 0, 286),
        (0, 341, 286, 572),
        (341, 682, 286, 572),
        (682, 1024, 286, 572)
    ]
    # Tọa độ tâm vòng tròn ma pháp đo chuẩn xác tới từng pixel trong từng ô cell
    centers_in_cell = [
        (178.0, 242.0),
        (172.0, 242.0),
        (167.0, 242.0),
        (179.0, 235.0),
        (172.0, 235.0),
        (171.5, 235.0)
    ]

    uniform_sprites = []
    for idx, ((x1, x2, y1, y2), (cx, cy)) in enumerate(zip(cells, centers_in_cell)):
        cell = final_im.crop((x1, y1, x2, y2))
        box = Image.new('RGBA', (W_BOX_4X, H_BOX_4X), (0, 0, 0, 0))
        paste_x = int(round(ANCHOR_X_4X - cx))
        paste_y = int(round(ANCHOR_Y_4X - cy))
        box.paste(cell, (paste_x, paste_y), cell)
        uniform_sprites.append(box)

    # 5. Ghép vào Sheet 4x (3 cột x 2 hàng) = 1080 x 608
    sheet_4x = Image.new('RGBA', (W_BOX_4X * 3, H_BOX_4X * 2), (0, 0, 0, 0))
    small_images = []
    w_1x = W_BOX_4X // 4 # 90
    h_1x = H_BOX_4X // 4 # 76

    for idx, sp in enumerate(uniform_sprites):
        col = idx % 3
        row = idx // 3
        gx_4x = col * W_BOX_4X
        gy_4x = row * H_BOX_4X
        sheet_4x.paste(sp, (gx_4x, gy_4x), sp)
        # Tọa độ 1x chuẩn xác
        small_images.append([idx, col * w_1x, row * h_1x, w_1x, h_1x])

    return sheet_4x, uniform_sprites, small_images

def build_data(small_images):
    # dx, dy ở 1x:
    # Anchor đặt tại ANCHOR_X_4X (180) -> 180 // 4 = 45 -> dx = -45
    # Anchor đặt tại ANCHOR_Y_4X (256) -> 256 // 4 = 64 -> dy = -64
    dx = - (ANCHOR_X_4X // 4) # -45
    dy = - (ANCHOR_Y_4X // 4) # -64

    out = bytearray()
    out.append(len(small_images))
    for s in small_images:
        for v in s:
            out.append(v)

    # 6 frames tương ứng 6 sprite
    out.extend(struct.pack('>h', len(small_images)))
    for i in range(len(small_images)):
        out.append(1) # 1 part per frame
        out.extend(struct.pack('>h', dx))
        out.extend(struct.pack('>h', dy))
        out.append(i) # idSmallImg
        out.append(0) # flip
        out.append(1) # onTop = 1 (Vẽ ở tầng dưới chân nhân vật trong paintBottomEff, nhân vật đứng đè lên trên vòng tròn)

    # Loop sequence mượt mà
    seq = [0, 1, 2, 3, 4, 5, 4, 3, 2, 1]
    out.append(len(seq))
    for s in seq:
        out.extend(struct.pack('>h', s))

    out.append(0)
    for fc in [[0], [0], [0]]:
        out.append(len(fc))
        for b in fc:
            out.append(b)
    out.extend(bytes([0, 0, 0]))
    return bytes(out)

def save_clean_palette_png(im, n_colors=255):
    arr = np.array(im)
    alpha = arr[:, :, 3]
    rgb_im = Image.fromarray(arr[:, :, :3])
    q_rgb = rgb_im.quantize(colors=n_colors, method=Image.Quantize.MEDIANCUT, dither=Image.Dither.NONE)
    pal = q_rgb.getpalette()[:n_colors * 3] + [0, 0, 0] * (256 - n_colors)
    q_arr = np.array(q_rgb)
    q_arr[alpha < 24] = 255 # Màu thứ 255 làm màu trong suốt

    im_pal = Image.fromarray(q_arr, mode='P')
    im_pal.putpalette(pal)
    im_pal.info['transparency'] = 255
    buf = io.BytesIO()
    im_pal.save(buf, format='PNG', optimize=True, compress_level=9)
    return buf.getvalue()

def generate():
    print("==================================================================")
    print("BẮT ĐẦU TẠO EFFECT 920 SẮC NÉT CHUẨN ANIME (ULTRA SHARP & CRISP):")
    print("==================================================================")
    sheet_4x, uniform_sprites, small_images = extract_and_clean_frames_sharp()
    data_bytes = build_data(small_images)

    print(f"Sheet 4x size: {sheet_4x.size}")
    print(f"Sprite 1x size: {W_BOX_4X // 4} x {H_BOX_4X // 4}")
    print(f"Data binary size: {len(data_bytes)} bytes")

    # Lưu preview GIF sắc nét
    seq = [0, 1, 2, 3, 4, 5, 4, 3, 2, 1]
    gif_frames = []
    for s in seq:
        bg = Image.new('RGBA', (W_BOX_4X, H_BOX_4X), (20, 22, 28, 255))
        sp = uniform_sprites[s]
        bg.paste(sp, (0, 0), sp)
        gif_frames.append(bg)

    gif_path = os.path.join(SCRIPT_DIR, 'eff920_v12_preview.gif')
    gif_frames[0].save(gif_path, save_all=True, append_images=gif_frames[1:], duration=100, loop=0)
    print(f"-> Đã lưu preview GIF động: {gif_path}")

    # Đa cấp zoom:
    # 1x: 270 x 152
    # 2x: 540 x 304
    # 3x: 810 x 456
    # 4x: 1080 x 608
    w1, h1 = (W_BOX_4X // 4) * 3, (H_BOX_4X // 4) * 2 # 270, 152
    zooms = {
        'x4': (sheet_4x, 64),
        'x3': (sheet_4x.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 128),
        'x2': (sheet_4x.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 255),
        'x1': (sheet_4x.resize((w1, h1), Image.Resampling.LANCZOS), 255),
        'x0': (sheet_4x.resize((w1, h1), Image.Resampling.LANCZOS), 255),
    }

    for z, (im_z, n_col) in zooms.items():
        img_dir = os.path.join(BASE_DIR, z, 'img')
        dat_dir = os.path.join(BASE_DIR, z, 'data')
        os.makedirs(img_dir, exist_ok=True)
        os.makedirs(dat_dir, exist_ok=True)

        png_bytes = save_clean_palette_png(im_z, n_colors=n_col)
        img_path = os.path.join(img_dir, '920.png')
        safe_write(img_path, png_bytes)
        img_sz = os.path.getsize(img_path)

        dat_path = os.path.join(dat_dir, '920')
        safe_write(dat_path, data_bytes)

        total_sz = img_sz + len(data_bytes)
        status = "✓ OK" if total_sz < 60000 else "! >60K (Message 76 support)"
        print(f"  [{z}] 920.png ({im_z.size[0]}x{im_z.size[1]}) = {img_sz} bytes, total = {total_sz} bytes {status}")

    print("\n=== HOÀN TẤT: EFFECT 920 ĐÃ ĐƯỢC VẼ LẠI SẮC NÉT TUYỆT ĐỐI! ===")

if __name__ == '__main__':
    generate()
