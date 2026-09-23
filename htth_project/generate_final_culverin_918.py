"""
Effect 918: CAO SU XÀ QUYỀN (SNAKEMAN CULVERIN GATLING - 3 GIÂY ĐẤM LIÊN THANH & HỐ NỔ ĐẤT LƯU LẠI ẨN DẦN)
Tạo animation từ 2 bộ ảnh:
- culverin_918_source.png (nắm đấm Haki xà quyền, vệt lửa vàng, chấn động, sét Haki Bá Vương)
- crater_ground_source.png (vết nứt miệng hố đất đá nhỏ & lớn)

Đặc điểm kỹ thuật theo yêu cầu:
1. ĐÃ XÓA TOÀN BỘ ĐÁ VỤN quanh nắm đấm lao xuống (dive): Chỉ giữ lại nắm đấm Haki đen bóng và vệt lửa/tốc độ vàng sắc nét, không còn đá vụn bay lơ lửng trên không.
2. Đấm liên phanh rơi liên tục trong 3 GIÂY từ góc trên bên trái (gần HP/MP) dội xối xả xuống quái.
3. Hiệu ứng nổ đất (crater_sm) được lưu lại trên mặt đất suốt 3 giây đấm liên thanh, tích lũy vết nứt nổ đất dưới chân quái.
4. Sau khi cú đấm kết thúc bùng nổ đại địa chấn (crater_large, shockwave, debris, sét Haki), các vết nứt hố đất mới bắt đầu ẩn dần đi.
5. Nén x2 (360x408) 120 màu đạt ~51KB (tổng packet 57KB < 60KB chuẩn).
"""
import os
import sys
import io
import struct
import shutil
import scipy.ndimage as ndi
import numpy as np
from PIL import Image, ImageDraw, ImageEnhance, ImageFilter

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
BASE_DIR = os.path.normpath(os.path.join(SCRIPT_DIR, 'data', 'template', 'skill'))

def round4(x):
    return int(np.ceil(x / 4.0) * 4)

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

def apply_red_fist_border(pad_img, fist_mask):
    arr = np.array(pad_img)
    d_out = ndi.binary_dilation(fist_mask, iterations=3)
    d_mid = ndi.binary_dilation(fist_mask, iterations=2)
    d_in = ndi.binary_dilation(fist_mask, iterations=1)

    b_out = d_out & ~d_mid
    b_mid = d_mid & ~d_in
    b_in = d_in & ~fist_mask

    eroded = ndi.binary_erosion(fist_mask, iterations=1)
    fist_rim = fist_mask & ~eroded

    res = np.copy(arr)
    res[b_out] = [210, 20, 30, 220]
    res[b_mid] = [255, 35, 45, 255]
    res[b_in] = [255, 20, 30, 255]

    res[fist_rim, 0] = np.clip(res[fist_rim, 0].astype(int) + 140, 0, 255)
    res[fist_rim, 1] = np.clip(res[fist_rim, 1].astype(int) // 3, 0, 255)
    res[fist_rim, 2] = np.clip(res[fist_rim, 2].astype(int) // 3, 0, 255)

    return Image.fromarray(res, mode='RGBA')

def create_thin_smoke(w=240, h=96, fade=False):
    canvas = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    alpha_mult = 0.55 if fade else 1.0
    blur_rad = 3.5 if fade else 2.0

    def create_ribbon(x0, y0, length, base_slope, wave_amp, wave_freq, phase, width_profile, alpha_profile):
        layer = Image.new('RGBA', (w, h), (0, 0, 0, 0))
        draw = ImageDraw.Draw(layer)
        xs = np.linspace(x0, x0 - length, 80)
        ys = y0 - (x0 - xs) * base_slope + wave_amp * np.sin(wave_freq * (x0 - xs) + phase)

        for i in range(len(xs) - 1):
            progress = i / (len(xs) - 1)
            width = int(np.interp(progress, [0, 0.3, 0.7, 1.0], width_profile))
            if fade:
                width = int(width * 1.3)
            alpha = int(np.interp(progress, [0, 0.2, 0.7, 1.0], alpha_profile) * alpha_mult)
            if width > 0 and alpha > 0:
                draw.line([(xs[i], ys[i]), (xs[i+1], ys[i+1])], fill=(245, 248, 255, alpha), width=width)
                draw.ellipse((xs[i] - width//2, ys[i] - width//2, xs[i] + width//2, ys[i] + width//2), fill=(245, 248, 255, alpha))
        return layer

    # 3 dải khói mỏng uốn lượn bay bay theo sau
    r1 = create_ribbon(220, 65, 200, 0.28, 6.0, 0.04, 0.0, [8, 14, 10, 2], [160, 140, 70, 0])
    r1 = r1.filter(ImageFilter.GaussianBlur(radius=blur_rad))
    canvas = Image.alpha_composite(canvas, r1)

    r2 = create_ribbon(195, 48, 160, 0.26, 5.0, 0.05, 1.5, [5, 10, 7, 1], [140, 110, 50, 0])
    r2 = r2.filter(ImageFilter.GaussianBlur(radius=blur_rad))
    canvas = Image.alpha_composite(canvas, r2)

    r3 = create_ribbon(210, 78, 170, 0.30, 7.0, 0.045, 3.0, [6, 12, 8, 2], [150, 120, 60, 0])
    r3 = r3.filter(ImageFilter.GaussianBlur(radius=blur_rad))
    canvas = Image.alpha_composite(canvas, r3)

    if not fade:
        c1 = create_ribbon(215, 65, 140, 0.28, 6.0, 0.04, 0.0, [2, 4, 3, 1], [220, 180, 80, 0])
        c1 = c1.filter(ImageFilter.GaussianBlur(radius=1.0))
        canvas = Image.alpha_composite(canvas, c1)

    pad_smk = Image.new('RGBA', (360, 96), (0, 0, 0, 0))
    pad_smk.paste(canvas, (0, 0))
    return pad_smk

def extract_and_prepare_all_sprites():
    src_punch = os.path.join(SCRIPT_DIR, 'culverin_918_source.png')
    src_crater = os.path.join(SCRIPT_DIR, 'crater_ground_source.png')

    im_punch = Image.open(src_punch).convert('RGB')
    arr_p = np.array(im_punch).astype(float)
    bg = np.array([68.4, 68.1, 68.1])

    diff = np.sqrt(np.sum((arr_p - bg) ** 2, axis=2))
    t0 = 12.0
    t1 = 26.0
    alpha = np.clip((diff - t0) / (t1 - t0), 0.0, 1.0)

    alpha_expanded = alpha[:, :, np.newaxis]
    arr_clean = np.zeros_like(arr_p)
    mask = alpha > 0.04
    arr_clean[mask] = np.clip((arr_p[mask] - (1.0 - alpha_expanded[mask]) * bg) / alpha_expanded[mask], 0, 255)
    arr_clean[~mask] = 0

    rgb_p = Image.fromarray(arr_clean.astype(np.uint8), mode='RGB')
    sat = ImageEnhance.Color(rgb_p).enhance(1.25)
    contr = ImageEnhance.Contrast(sat).enhance(1.20)
    sharp_rgb = contr.filter(ImageFilter.UnsharpMask(radius=1.0, percent=150, threshold=1))

    punch_clean = Image.fromarray(np.dstack([np.array(sharp_rgb), (alpha * 255.0).astype(np.uint8)]), mode='RGBA')

    pads = {}

    # 1. RING (360 x 288) - GIỮ TRỌN VẸN NÉT NHỌN ĐUÔI TAY VÀ VỆT VÀNG TỐC ĐỘ (THEO YÊU CẦU)
    sub_ring = punch_clean.crop((250, 90, 680, 400))
    arr_ring = np.array(sub_ring)
    alpha_r = arr_ring[:, :, 3] > 15
    lab_r, num_r = ndi.label(alpha_r)
    ring_lbl = lab_r[190, 200]
    clean_ring_mask = (lab_r == ring_lbl)
    for i in range(1, num_r + 1):
        cmask = lab_r == i
        ys, xs = np.where(cmask)
        if xs.min() < 51 and ys.min() > 100:
            continue
        if ys.max() < 25:
            continue
        if xs.min() > 390 and np.sum(cmask) < 50:
            continue
        clean_ring_mask |= cmask

    arr_ring_clean = np.zeros_like(arr_ring)
    arr_ring_clean[clean_ring_mask] = arr_ring[clean_ring_mask]
    arr_ring_clean[:, :51] = 0
    arr_ring_clean[280:, :60] = 0

    lab_rc, num_rc = ndi.label(arr_ring_clean[:, :, 3] > 15)
    for i in range(1, num_rc + 1):
        cmask = lab_rc == i
        if np.sum(cmask) < 5:
            arr_ring_clean[cmask] = 0

    im_ring_clean = Image.fromarray(arr_ring_clean, mode='RGBA')
    ys_r, xs_r = np.where(arr_ring_clean[:, :, 3] > 15)
    cr_ring = im_ring_clean.crop((xs_r.min(), ys_r.min(), xs_r.max() + 1, ys_r.max() + 1))
    pad_ring = Image.new('RGBA', (360, 288), (0, 0, 0, 0))
    pad_ring.paste(cr_ring, (7, 4))
    
    # Viền đỏ cho cánh tay đấm trong ring
    arr_rg = np.array(pad_ring)
    ys_rg, xs_rg = np.indices(arr_rg.shape[:2])
    fist_mask_rg = (arr_rg[:, :, 3] > 50) & (arr_rg[:, :, 0] < 65) & (arr_rg[:, :, 1] < 50) & (arr_rg[:, :, 2] < 50) & (xs_rg > 40) & (xs_rg < 260) & (ys_rg > 30) & (ys_rg < 220)
    pads['ring'] = apply_red_fist_border(pad_ring, fist_mask_rg)

    # 2. DEBRIS (360 x 224)
    sub_deb = punch_clean.crop((670, 160, 1024, 400))
    arr_deb = np.array(sub_deb)
    alpha_d = arr_deb[:, :, 3] > 15
    lab_d, num_d = ndi.label(alpha_d)
    clean_deb_mask = np.zeros_like(alpha_d)
    for i in range(1, num_d + 1):
        cmask = lab_d == i
        ys, xs = np.where(cmask)
        if xs.max() < 25:
            continue
        clean_deb_mask |= cmask
    arr_deb_clean = np.zeros_like(arr_deb)
    arr_deb_clean[clean_deb_mask] = arr_deb[clean_deb_mask]
    im_deb_clean = Image.fromarray(arr_deb_clean, mode='RGBA')
    ys_d, xs_d = np.where(arr_deb_clean[:, :, 3] > 15)
    cr_deb = im_deb_clean.crop((xs_d.min(), ys_d.min(), xs_d.max() + 1, ys_d.max() + 1))
    pad_deb = Image.new('RGBA', (360, 224), (0, 0, 0, 0))
    pad_deb.paste(cr_deb, ((356 - cr_deb.width) // 2, (224 - cr_deb.height) // 2))

    # Viền đỏ cho cánh tay đấm trong debris
    arr_db = np.array(pad_deb)
    ys_db, xs_db = np.indices(arr_db.shape[:2])
    fist_mask_db = (arr_db[:, :, 3] > 50) & (arr_db[:, :, 0] < 65) & (arr_db[:, :, 1] < 50) & (arr_db[:, :, 2] < 50) & (xs_db > 40) & (xs_db < 260) & (ys_db > 30) & (ys_db < 220)
    pads['debris'] = apply_red_fist_border(pad_deb, fist_mask_db)

    # 3. DIVE (320 x 112, pad 360 x 112) - TAY BAY LAO XUỐNG CÙNG KHÓI BAY THEO SAU VÀ VIỀN ĐỎ
    center_sub = punch_clean.crop((300, 0, 630, 130))
    arr_c = np.array(center_sub)
    lab_c, num_c = ndi.label(arr_c[:, :, 3] > 15)
    main_c = (lab_c == lab_c[60, 250])
    for i in range(1, num_c + 1):
        cmask = lab_c == i
        ys, xs = np.where(cmask)
        if ys.min() < 120 and xs.min() > 10 and xs.max() < 315:
            main_c |= cmask
    arr_c_clean = np.zeros_like(arr_c)
    arr_c_clean[main_c] = arr_c[main_c]
    ys_c, xs_c = np.where(arr_c_clean[:, :, 3] > 15)
    cr_c = Image.fromarray(arr_c_clean).crop((xs_c.min(), ys_c.min(), xs_c.max() + 1, ys_c.max() + 1))

    rot_14 = cr_c.rotate(-14.0, resample=Image.Resampling.BICUBIC, expand=True)
    a_rot = np.array(rot_14)
    lab_r, num_r = ndi.label(a_rot[:, :, 3] > 15)
    out_rot = np.zeros_like(a_rot)
    out_rot[lab_r == 1] = a_rot[lab_r == 1]
    ys_r, xs_r = np.where(out_rot[:, :, 3] > 15)
    cr_dive = Image.fromarray(out_rot).crop((xs_r.min(), ys_r.min(), xs_r.max() + 1, ys_r.max() + 1))

    pad_dive = Image.new('RGBA', (360, 112), (0, 0, 0, 0))
    pad_dive.paste(cr_dive, (0, 1))

    # Áp dụng viền đỏ cho cú đấm
    arr_dv = np.array(pad_dive)
    a_dv = arr_dv[:, :, 3]
    r_dv, g_dv, b_dv = arr_dv[:, :, 0], arr_dv[:, :, 1], arr_dv[:, :, 2]
    fist_mask_dv = (a_dv > 40) & ~((r_dv > 170) & (g_dv > 130) & (b_dv < 150))
    pads['dive'] = apply_red_fist_border(pad_dive, fist_mask_dv)

    # 4. HAKI (336 x 200, pad 360 x 200) - TIA SÉT ĐỎ HAKI BÁ VƯƠNG
    sub_haki = punch_clean.crop((0, 350, 380, 572))
    arr_haki = np.array(sub_haki)
    alpha_h = arr_haki[:, :, 3] > 15
    lab_h, num_h = ndi.label(alpha_h)
    clean_haki_mask = np.zeros_like(alpha_h)
    for i in range(1, num_h + 1):
        cmask = lab_h == i
        ys, xs = np.where(cmask)
        if xs.min() <= 2 and np.sum(cmask) < 20:
            continue
        clean_haki_mask |= cmask
    arr_haki_clean = np.zeros_like(arr_haki)
    arr_haki_clean[clean_haki_mask] = arr_haki[clean_haki_mask]
    im_haki_clean = Image.fromarray(arr_haki_clean, mode='RGBA')
    ys_h, xs_h = np.where(arr_haki_clean[:, :, 3] > 15)
    cr_haki = im_haki_clean.crop((xs_h.min(), ys_h.min(), xs_h.max() + 1, ys_h.max() + 1))
    pad_haki = Image.new('RGBA', (360, 200), (0, 0, 0, 0))
    pad_haki.paste(cr_haki, ((336 - cr_haki.width) // 2, (200 - cr_haki.height) // 2))
    pads['haki'] = pad_haki

    # 5. SLAM (360 x 228) - NẮM ĐẤM BỔ NHÀO CÙNG KHÓI THEO SAU VÀ VIỀN ĐỎ
    sub_slam = punch_clean.crop((0, 70, 340, 340))
    arr_slam = np.array(sub_slam)
    alpha_s = arr_slam[:, :, 3] > 15
    lab_s, num_s = ndi.label(alpha_s)
    fist_label_s = lab_s[260 - 70, 200]
    clean_slam_mask = (lab_s == fist_label_s)
    for i in range(1, num_s + 1):
        cmask = lab_s == i
        ys, xs = np.where(cmask)
        if ys.max() < 35:
            continue
        if ys.min() > 270 or (ys.min() > 190 and xs.max() < 100 and np.sum(cmask) < 200):
            continue
        if xs.max() > 315:
            continue
        clean_slam_mask |= cmask

    arr_slam_clean = np.zeros_like(arr_slam)
    arr_slam_clean[clean_slam_mask] = arr_slam[clean_slam_mask]
    arr_slam_clean[200:, :50] = 0

    lab_sc, num_sc = ndi.label(arr_slam_clean[:, :, 3] > 15)
    for i in range(1, num_sc + 1):
        cmask = lab_sc == i
        if np.sum(cmask) < 5:
            arr_slam_clean[cmask] = 0

    im_slam_clean = Image.fromarray(arr_slam_clean, mode='RGBA')
    ys_s, xs_s = np.where(arr_slam_clean[:, :, 3] > 15)
    cr_slam = im_slam_clean.crop((xs_s.min(), ys_s.min(), xs_s.max() + 1, ys_s.max() + 1))
    pad_slam = Image.new('RGBA', (360, 228), (0, 0, 0, 0))
    pad_slam.paste(cr_slam, (0, 1))

    # Áp dụng viền đỏ cho cú đấm slam
    arr_sl = np.array(pad_slam)
    a_sl = arr_sl[:, :, 3]
    r_sl, g_sl, b_sl = arr_sl[:, :, 0], arr_sl[:, :, 1], arr_sl[:, :, 2]
    black_core_sl = (a_sl > 50) & (r_sl < 70) & (g_sl < 55) & (b_sl < 55)
    lab_sl, num_sl = ndi.label(black_core_sl)
    main_lbl_sl = lab_sl[160, 160]
    fist_mask_sl = ndi.binary_dilation((lab_sl == main_lbl_sl), iterations=4) & (a_sl > 30) & ~((r_sl > 170) & (g_sl > 130) & (b_sl < 150)) & ~((r_sl > 140) & (g_sl > 80) & (b_sl < 50) & (np.arange(228)[:, None] > 185))
    pads['slam'] = apply_red_fist_border(pad_slam, fist_mask_sl)

    # 6. ROCKS (212 x 144, pad 360 x 144) - MẢNH ĐÁ VỠ BAY TUNG TÓE
    sub_rocks = punch_clean.crop((680, 380, 1024, 572))
    arr_rocks = np.array(sub_rocks)
    alpha_rk = arr_rocks[:, :, 3] > 15
    ys_rk, xs_rk = np.where(alpha_rk)
    cr_rocks = sub_rocks.crop((xs_rk.min(), ys_rk.min(), xs_rk.max() + 1, ys_rk.max() + 1))
    pad_rocks = Image.new('RGBA', (360, 144), (0, 0, 0, 0))
    pad_rocks.paste(cr_rocks, ((212 - cr_rocks.width) // 2, (144 - cr_rocks.height) // 2))
    pads['rocks'] = pad_rocks

    # 7. TÁCH RIÊNG HIỆU ỨNG KHÓI MỎNG BAY BAY THEO (KHÔNG GÁN VÀO CÚ ĐẤM)
    pads['thin_smoke'] = create_thin_smoke(w=240, h=96, fade=False)
    pads['thin_smoke_fade'] = create_thin_smoke(w=240, h=96, fade=True)

    # ĐẢM BẢO MỌI PART CÓ KHOẢNG ĐỆM AN TOÀN (SAFE MARGIN) ĐỂ KHÔNG BAO GIỜ BỊ DÍNH ẢNH CỦA NHAU
    def apply_safe_margin(img, pad_w, pad_h, margin_y=8, margin_x=4):
        arr = np.array(img)
        alpha = arr[:, :, 3] > 10
        if not np.any(alpha):
            return Image.new('RGBA', (pad_w, pad_h), (0, 0, 0, 0))
        ys, xs = np.where(alpha)
        crop = img.crop((xs.min(), ys.min(), xs.max() + 1, ys.max() + 1))
        avail_w = pad_w - margin_x * 2
        avail_h = pad_h - margin_y * 2
        scale = min(avail_w / crop.width, avail_h / crop.height, 1.0)
        new_w = int(crop.width * scale)
        new_h = int(crop.height * scale)
        crop_scaled = crop.resize((new_w, new_h), Image.Resampling.LANCZOS)
        pad = Image.new('RGBA', (pad_w, pad_h), (0, 0, 0, 0))
        px = (pad_w - new_w) // 2
        py = (pad_h - new_h) // 2
        pad.paste(crop_scaled, (px, py))
        return pad

    pads['ring'] = apply_safe_margin(pads['ring'], 360, 288, margin_y=8, margin_x=4)
    pads['debris'] = apply_safe_margin(pads['debris'], 360, 224, margin_y=8, margin_x=4)
    pads['dive'] = apply_safe_margin(pads['dive'], 360, 112, margin_y=10, margin_x=4)
    pads['thin_smoke'] = apply_safe_margin(pads['thin_smoke'], 360, 96, margin_y=6, margin_x=4)

    pads['haki'] = apply_safe_margin(pads['haki'], 360, 200, margin_y=8, margin_x=4)
    pads['slam'] = apply_safe_margin(pads['slam'], 360, 228, margin_y=8, margin_x=4)
    pads['rocks'] = apply_safe_margin(pads['rocks'], 360, 144, margin_y=8, margin_x=4)
    pads['thin_smoke_fade'] = apply_safe_margin(pads['thin_smoke_fade'], 360, 96, margin_y=6, margin_x=4)

    # Kích thước từng part
    for k, v in pads.items():
        print(f"Part [{k}]: 4x={v.size}, 1x=({v.width//4},{v.height//4})")

    # Sheet 4x (720 x 720) - 1x là (180 x 180)
    # Col 1: ring (y=0, h=288), debris (y=288, h=224), dive (y=512, h=112), thin_smoke (y=624, h=96) -> total h=720
    # Col 2: haki (y=0, h=200), slam (y=200, h=228), rocks (y=428, h=144), thin_smoke_fade (y=624, h=96) -> total h=720
    sheet_4x = Image.new('RGBA', (720, 720), (0, 0, 0, 0))

    sheet_4x.paste(pads['ring'], (0, 0))
    sheet_4x.paste(pads['debris'], (0, 288))
    sheet_4x.paste(pads['dive'], (0, 512))
    sheet_4x.paste(pads['thin_smoke'], (0, 624))

    sheet_4x.paste(pads['haki'], (360, 0))
    sheet_4x.paste(pads['slam'], (360, 200))
    sheet_4x.paste(pads['rocks'], (360, 428))
    sheet_4x.paste(pads['thin_smoke_fade'], (360, 624))

    small_images = [
        [0, 0, 0, 90, 72],       # 0: ring (sóng xung kích + đuôi nhọn trọn vẹn)
        [1, 0, 72, 89, 56],      # 1: debris (nổ đất đá - SẠCH SẼ HOÀN TOÀN, KHÔNG DÍNH DIVE HAY RING)
        [2, 0, 128, 80, 28],     # 2: dive (nắm đấm lao xuống - SẠCH SẼ, viền đỏ, KHÔNG gán khói)
        [3, 90, 0, 84, 50],      # 3: haki (sét đỏ Haki Bá Vương)
        [4, 90, 50, 78, 57],     # 4: slam (nắm đấm cắm xuống - SẠCH SẼ, viền đỏ, KHÔNG gán khói)
        [5, 90, 107, 53, 36],    # 5: rocks (mảnh đất đá bắn tung tóe)
        [6, 0, 156, 60, 24],     # 6: thin_smoke (dải khói mỏng bay lượn theo sau tách riêng)
        [7, 90, 156, 60, 24],    # 7: thin_smoke_fade (khói mỏng tan dần theo sau)
    ]

    return sheet_4x, pads, small_images

def build_data_effect(small_images):
    crater_slots = [
        (-24, 0), (20, 1), (-14, -2), (15, 2),
        (-20, 1), (8, -2), (22, -1), (-8, 2)
    ]

    TOTAL_FRAMES = 105
    frames = []

    for t in range(TOTAL_FRAMES):
        part_list = []

        # 1. onTop = 1: Đấm liên phanh rơi liên tục trong 3 GIÂY (32 cú đấm, từ t=0 đến t=58)
        # Nắm đấm dive sạch bóng đá vụn, knuckle đặt đúng vị trí (67, 26)
        for i in range(32):
            t_start = int(i * 1.8)
            t_rel = t - t_start
            ox, oy = crater_slots[i % 8]
            if t_rel == 0:
                # Giai đoạn 0: Góc trên bên trái gần HP/MP
                part_list.append((ox - 179, oy - 158, 2, 0, 1))
            elif t_rel == 1:
                # Giai đoạn 1: Giữa trời
                part_list.append((ox - 129, oy - 98, 2, 0, 1))
                # Khói mỏng bay lượn theo sau cú đấm (tách riêng, đặt ở vị trí cú đấm vừa bay qua)
                part_list.append((ox - 180, oy - 156, 6, 0, 1))
            elif t_rel == 2:
                # Giai đoạn 2: Sát đầu quái
                part_list.append((ox - 84, oy - 38, 2, 0, 1))
                part_list.append((ox - 130, oy - 96, 6, 0, 1))
                part_list.append((ox - 180, oy - 156, 7, 0, 1)) # khói cũ tan dần
            elif t_rel in [3, 4]:
                # Giai đoạn 3: Nện sập xuống quái & đất (chậm lại 2 frames tạo lực đấm rõ rệt)
                part_list.append((ox - 41, oy - 54, 4, 0, 1)) # slam
                part_list.append((ox - 20, oy - 38, 5, 0, 1)) # rocks nổ bốc lên
                if t_rel == 3:
                    part_list.append((ox - 85, oy - 36, 6, 0, 1))
                    part_list.append((ox - 130, oy - 96, 7, 0, 1))
                elif t_rel == 4:
                    part_list.append((ox - 85, oy - 36, 7, 0, 1))
            elif t_rel in [5, 6]:
                part_list.append((ox - 15, oy - 50, 5, 0, 1)) # rocks tung lên cao

        # 2. Cú đấm thứ 33 (Final Climax Smash): Chạm đất vỡ đất ra, chậm lại và lưu suốt 2 giây
        t_start_final = 58
        t_rel_f = t - t_start_final
        if t_rel_f == 0:
            part_list.append((-187, -166, 2, 0, 1)) # dive từ trời cao
        elif t_rel_f == 1:
            part_list.append((-129, -98, 2, 0, 1)) # dive lao xuống
            part_list.append((-188, -164, 6, 0, 1)) # khói mỏng bay theo sau
        elif t_rel_f == 2:
            part_list.append((-84, -38, 2, 0, 1))   # dive chạm mặt đất
            part_list.append((-130, -96, 6, 0, 1))  # khói mới bay theo sau
            part_list.append((-188, -164, 7, 0, 1)) # khói trước tan dần
        elif t_rel_f in [3, 4, 5]:
            # 'KIỂU CHẬM CHỖ ĐÓ LẠI': Hitstop / Dừng hình 3 frames tạo độ trễ cực nặng khi nắm đấm cắm sâu vào đất
            part_list.append((-41, -54, 4, 0, 1))   # slam (nắm đấm cắm chặt mặt đất)
            part_list.append((-20, -38, 5, 0, 1))   # rocks
            if t_rel_f == 3:
                part_list.append((-85, -36, 6, 0, 1))
                part_list.append((-130, -96, 7, 0, 1))
            elif t_rel_f == 4:
                part_list.append((-85, -36, 7, 0, 1))
        elif t_rel_f >= 6 and t <= 104:
            # 'KHI CHẠM ĐẤT VỠ ĐẤT RA 2 GIÂY SAU MỚI BIẾN MẤT' (t=64 đến t=104 là đúng 40 frames = 2.0s)
            # 1. Đất đá vỡ toác khói bụi cuồn cuộn (debris) LƯU LẠI SUỐT 2 GIÂY
            part_list.append((-44, -55, 1, 0, 1)) # debris (vỡ đất ra)

            # Sóng xung kích hoàng kim bùng nổ cực đại
            if 6 <= t_rel_f <= 11:
                part_list.append((-45, -72, 0, 0, 1)) # ring

            # Mảnh đất đá văng tung tóe nhiều hướng
            if 6 <= t_rel_f <= 14:
                shrap_dx = -26 + (t_rel_f - 6) * 5
                shrap_dy = -35 - (t_rel_f - 6) * 6
                part_list.append((shrap_dx, shrap_dy, 5, 0, 1)) # rocks

            # Sét đỏ Haki Bá Vương phụt lên thành 3 đợt chớp giật dữ dội trong suốt 2 giây
            if (7 <= t_rel_f <= 14) or (20 <= t_rel_f <= 26) or (32 <= t_rel_f <= 38):
                part_list.append((-42, -50, 3, 0, 1)) # haki

        if not part_list:
            part_list.append((0, 0, 0, 0, 0))
        frames.append(part_list)

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

    sequence = list(range(len(frames)))
    out.append(len(sequence))
    for s in sequence:
        out.extend(struct.pack('>h', s))

    out.append(0)
    for _ in range(3):
        out.append(1)
        out.append(0)
    out.extend(bytes([0, 0, 0]))
    return bytes(out)

def save_clean_palette_png(im, n_colors=120):
    arr = np.array(im)
    alpha = arr[:, :, 3]
    rgb_im = Image.fromarray(arr[:, :, :3])
    q_rgb = rgb_im.quantize(colors=n_colors, method=Image.Quantize.MEDIANCUT, dither=Image.Dither.NONE)
    pal = q_rgb.getpalette()[:n_colors * 3] + [0, 0, 0] * (256 - n_colors)
    q_arr = np.array(q_rgb)
    q_arr[alpha < 20] = 255

    im_pal = Image.fromarray(q_arr, mode='P')
    im_pal.putpalette(pal)
    im_pal.info['transparency'] = 255
    buf = io.BytesIO()
    im_pal.save(buf, format='PNG', optimize=True, compress_level=9)
    return buf.getvalue()

def render_preview_gif(pads):
    sprites_1x = {
        0: pads['ring'].crop((0, 0, 360, 288)).resize((90, 72), Image.Resampling.LANCZOS),
        1: pads['debris'].crop((0, 0, 356, 224)).resize((89, 56), Image.Resampling.LANCZOS),
        2: pads['dive'].crop((0, 0, 320, 112)).resize((80, 28), Image.Resampling.LANCZOS),
        3: pads['haki'].crop((0, 0, 336, 200)).resize((84, 50), Image.Resampling.LANCZOS),
        4: pads['slam'].crop((0, 0, 312, 228)).resize((78, 57), Image.Resampling.LANCZOS),
        5: pads['rocks'].crop((0, 0, 212, 144)).resize((53, 36), Image.Resampling.LANCZOS),
        6: pads['thin_smoke'].crop((0, 0, 240, 96)).resize((60, 24), Image.Resampling.LANCZOS),
        7: pads['thin_smoke_fade'].crop((0, 0, 240, 96)).resize((60, 24), Image.Resampling.LANCZOS),
    }

    sprites_flip_1x = {
        k: v.transpose(Image.Transpose.FLIP_LEFT_RIGHT) for k, v in sprites_1x.items()
    }

    crater_slots = [
        (-24, 0), (20, 1), (-14, -2), (15, 2),
        (-20, 1), (8, -2), (22, -1), (-8, 2)
    ]

    TOTAL_FRAMES = 105
    gif_frames = []

    W_VIEW = 460
    H_VIEW = 280

    # Chạy 2 chu kỳ tuần tự:
    # Chu kỳ 1 (t in 0..104): Nhân vật nhìn sang PHẢI -> Đấm 1 hướng từ trên trái xuống quái bên phải
    # Chu kỳ 2 (t in 0..104): Nhân vật nhìn sang TRÁI -> Đấm 1 hướng từ trên phải xuống quái bên trái
    for cycle in [0, 1]:
        is_facing_left = (cycle == 1)
        for t in range(TOTAL_FRAMES):
            frame_im = Image.new('RGBA', (W_VIEW, H_VIEW), (24, 28, 36, 255))
            draw = ImageDraw.Draw(frame_im)

            # Mặt đất
            draw.line([(0, 220), (W_VIEW, 220)], fill=(60, 65, 75), width=2)

            if not is_facing_left:
                # --- CHU KỲ 1: PLAYER NHÌN PHẢI -> QUÁI BÊN PHẢI ---
                draw.rectangle([(15, 8), (445, 28)], fill=(35, 45, 65), outline=(70, 130, 220))
                draw.text((25, 12), "NHÂN VẬT NHÌN PHẢI -> ĐẤM 1 HƯỚNG VỀ BÊN PHẢI", fill=(100, 200, 255))

                px, py = 80, 220
                tx, ty = 300, 220

                # Player nhìn phải ->
                draw.ellipse([(px - 12, py - 42), (px + 12, py - 18)], fill=(240, 240, 240), outline=(0, 0, 0))
                draw.line([(px, py - 18), (px - 6, py)], fill=(50, 50, 150), width=3)
                draw.line([(px, py - 18), (px + 8, py)], fill=(50, 50, 150), width=3)
                draw.polygon([(px + 8, py - 32), (px + 18, py - 30), (px + 8, py - 28)], fill=(255, 200, 50))
                draw.text((px - 18, py - 55), "PLAYER ->", fill=(120, 220, 120))

                # Quái
                draw.ellipse([(tx - 14, ty - 45), (tx + 14, ty - 15)], fill=(180, 60, 60), outline=(0, 0, 0))
                draw.line([(tx, ty - 15), (tx - 8, ty)], fill=(80, 30, 30), width=4)
                draw.line([(tx, ty - 15), (tx + 8, ty)], fill=(80, 30, 30), width=4)
                draw.text((tx - 16, ty - 58), "QUÁI", fill=(255, 100, 100))

                def draw_part_single(sid, dx, dy):
                    frame_im.alpha_composite(sprites_1x[sid], (tx + dx, ty + dy))
            else:
                # --- CHU KỲ 2: PLAYER NHÌN TRÁI -> QUÁI BÊN TRÁI ---
                draw.rectangle([(15, 8), (445, 28)], fill=(45, 35, 45), outline=(220, 100, 70))
                draw.text((25, 12), "NHÂN VẬT NHÌN TRÁI -> ĐẤM 1 HƯỚNG VỀ BÊN TRÁI", fill=(255, 170, 100))

                px, py = 380, 220
                tx, ty = 160, 220

                # Player nhìn trái <-
                draw.ellipse([(px - 12, py - 42), (px + 12, py - 18)], fill=(240, 240, 240), outline=(0, 0, 0))
                draw.line([(px, py - 18), (px - 8, py)], fill=(50, 50, 150), width=3)
                draw.line([(px, py - 18), (px + 6, py)], fill=(50, 50, 150), width=3)
                draw.polygon([(px - 8, py - 32), (px - 18, py - 30), (px - 8, py - 28)], fill=(255, 200, 50))
                draw.text((px - 22, py - 55), "<- PLAYER", fill=(120, 220, 120))

                # Quái
                draw.ellipse([(tx - 14, ty - 45), (tx + 14, ty - 15)], fill=(180, 60, 60), outline=(0, 0, 0))
                draw.line([(tx, ty - 15), (tx - 8, ty)], fill=(80, 30, 30), width=4)
                draw.line([(tx, ty - 15), (tx + 8, ty)], fill=(80, 30, 30), width=4)
                draw.text((tx - 16, ty - 58), "QUÁI", fill=(255, 100, 100))

                def draw_part_single(sid, dx, dy):
                    w, h = sprites_1x[sid].size
                    frame_im.alpha_composite(sprites_flip_1x[sid], (tx - dx - w, ty + dy))

            # 32 cú đấm liên thanh (3 giây đầu)
            for i in range(32):
                t_start = int(i * 1.8)
                t_rel = t - t_start
                ox, oy = crater_slots[i % 8]
                if t_rel == 0:
                    draw_part_single(2, ox - 179, oy - 158)
                elif t_rel == 1:
                    draw_part_single(2, ox - 129, oy - 98)
                    draw_part_single(6, ox - 180, oy - 156)
                elif t_rel == 2:
                    draw_part_single(2, ox - 84, oy - 38)
                    draw_part_single(6, ox - 130, oy - 96)
                    draw_part_single(7, ox - 180, oy - 156)
                elif t_rel in [3, 4]:
                    draw_part_single(4, ox - 41, oy - 54)
                    draw_part_single(5, ox - 20, oy - 38)
                    if t_rel == 3:
                        draw_part_single(6, ox - 85, oy - 36)
                        draw_part_single(7, ox - 130, oy - 96)
                    elif t_rel == 4:
                        draw_part_single(7, ox - 85, oy - 36)
                elif t_rel in [5, 6]:
                    draw_part_single(5, ox - 15, oy - 50)

            # Cú đấm thứ 33 (Final Climax Smash)
            t_start_final = 58
            t_rel_f = t - t_start_final
            if t_rel_f == 0:
                draw_part_single(2, -187, -166)
            elif t_rel_f == 1:
                draw_part_single(2, -129, -98)
                draw_part_single(6, -188, -164)
            elif t_rel_f == 2:
                draw_part_single(2, -84, -38)
                draw_part_single(6, -130, -96)
                draw_part_single(7, -188, -164)
            elif t_rel_f in [3, 4, 5]:
                draw_part_single(4, -41, -54)
                draw_part_single(5, -20, -38)
                if t_rel_f == 3:
                    draw_part_single(6, -85, -36)
                    draw_part_single(7, -130, -96)
                elif t_rel_f == 4:
                    draw_part_single(7, -85, -36)
            elif t_rel_f >= 6 and t <= 104:
                draw_part_single(1, -44, -55)
                if 6 <= t_rel_f <= 11:
                    draw_part_single(0, -45, -72)
                if 6 <= t_rel_f <= 14:
                    shrap_dx = -26 + (t_rel_f - 6) * 5
                    shrap_dy = -35 - (t_rel_f - 6) * 6
                    draw_part_single(5, shrap_dx, shrap_dy)
                if (7 <= t_rel_f <= 14) or (20 <= t_rel_f <= 26) or (32 <= t_rel_f <= 38):
                    draw_part_single(3, -42, -50)

            gif_frames.append(frame_im.convert('RGB'))

    out_gif = os.path.join(SCRIPT_DIR, 'eff918_preview.gif')
    gif_frames[0].save(out_gif, save_all=True, append_images=gif_frames[1:], duration=50, loop=0)
    print(f"-> Đã tạo GIF preview tại: {out_gif}")

    artifact_gif = r"C:\Users\admin\.gemini\antigravity-ide\brain\cd398ae1-c6a5-49b9-a7d4-2103ee77e6a0\eff918_preview.gif"
    try:
        shutil.copy(out_gif, artifact_gif)
        print(f"-> Đã copy GIF sang artifact: {artifact_gif}")
    except Exception as e:
        print(f"-> Không copy được artifact GIF: {e}")

def generate():
    print("==================================================================")
    print("BẮT ĐẦU TẠO EFFECT 918 (ĐÃ BỎ HOÀN TOÀN HIỆU ỨNG LÚN ĐẤT):")
    print("==================================================================")
    sheet_4x, pads, small_images = extract_and_prepare_all_sprites()
    data_bytes = build_data_effect(small_images)

    print(f"Sheet 4x size: {sheet_4x.size}")
    print(f"Data binary size: {len(data_bytes)} bytes")

    render_preview_gif(pads)

    w1, h1 = 180, 180
    zooms = {
        'x4': (sheet_4x, 64),
        'x3': (sheet_4x.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS), 100),
        'x2': (sheet_4x.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS), 120),
        'x1': (sheet_4x.resize((w1, h1), Image.Resampling.LANCZOS), 255),
        'x0': (sheet_4x.resize((w1, h1), Image.Resampling.LANCZOS), 255),
    }

    for z, (im_z, n_col) in zooms.items():
        img_dir = os.path.join(BASE_DIR, z, 'img')
        dat_dir = os.path.join(BASE_DIR, z, 'data')
        os.makedirs(img_dir, exist_ok=True)
        os.makedirs(dat_dir, exist_ok=True)

        png_bytes = save_clean_palette_png(im_z, n_colors=n_col)
        img_path = os.path.join(img_dir, '918.png')
        safe_write(img_path, png_bytes)
        img_sz = os.path.getsize(img_path)

        dat_path = os.path.join(dat_dir, '918')
        safe_write(dat_path, data_bytes)

        total_sz = img_sz + len(data_bytes)
        status = "✓ OK (<60K)" if total_sz < 60000 else "! >60K (Packet 76 support)"
        print(f"  [{z}] 918.png ({im_z.size[0]}x{im_z.size[1]}) = {img_sz} bytes, total = {total_sz} bytes {status}")

    print("\n=== HOÀN TẤT: EFFECT 918 ĐÃ XÓA ĐÁ VỤN QUANH NẮM ĐẤM LAO XUỐNG! ===")

if __name__ == '__main__':
    generate()
