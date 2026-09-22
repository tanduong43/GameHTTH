"""
Fix Effect 920 (Buff Nika Awakening):
- Bug 1: Vong tron duoi chan khong xoay - fix bang cach giam spike xuong 8, rotation ro rang hon
- Bug 2: Tia haki xuat hien roi mat - fix bang continuous flow, khong dung modular particle
"""
import os
import sys
import struct
import math
import numpy as np
from PIL import Image, ImageFilter

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

SCRATCH = 'C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch'
HAKI_SRC = f'{SCRATCH}/test_extract_clean.png'

# ==============================================================================
# BUILD DATA EFFECT
# ==============================================================================
def build_data_effect(small_images, frames, sequence, frame_char=None, index_splash=None):
    out = bytearray()
    out.append(len(small_images))
    for s in small_images:
        out.append(s[0]); out.append(s[1]); out.append(s[2])
        out.append(s[3]); out.append(s[4])
    out.extend(struct.pack('>h', len(frames)))
    for f in frames:
        out.append(len(f))
        for p in f:
            out.extend(struct.pack('>h', p[0]))
            out.extend(struct.pack('>h', p[1]))
            out.append(p[2])
            out.append(p[3] if len(p) > 3 else 0)
            out.append(p[4] if len(p) > 4 else 0)
    out.append(len(sequence))
    for s in sequence:
        out.extend(struct.pack('>h', s))
    out.append(0)
    if frame_char is None:
        frame_char = [[0], [0], [0]]
    for fc in frame_char:
        out.append(len(fc))
        for b in fc:
            out.append(b)
    if index_splash is None:
        index_splash = [0, 0, 0]
    out.extend(bytes(index_splash))
    return bytes(out)

def save_multizoom_effect(eff_id, img_x4, data_bytes):
    script_dir = os.path.dirname(os.path.abspath(__file__))
    base_dir = os.path.normpath(os.path.join(script_dir, 'data', 'template', 'skill'))
    w4, h4 = img_x4.size
    w1, h1 = w4 // 4, h4 // 4
    zooms = {
        'x4': img_x4,
        'x3': img_x4.resize((w1 * 3, h1 * 3), Image.Resampling.LANCZOS),
        'x2': img_x4.resize((w1 * 2, h1 * 2), Image.Resampling.LANCZOS),
        'x1': img_x4.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR),
        'x0': img_x4.resize((w1 * 1, h1 * 1), Image.Resampling.BILINEAR),
    }
    sz = 0
    for z, im in zooms.items():
        dir_img = os.path.normpath(os.path.join(base_dir, z, 'img'))
        dir_data = os.path.normpath(os.path.join(base_dir, z, 'data'))
        os.makedirs(dir_img, exist_ok=True)
        os.makedirs(dir_data, exist_ok=True)
        img_path = os.path.normpath(os.path.join(dir_img, f'{eff_id}.png'))
        if im.mode == 'RGBA':
            arr_im = np.array(im)
            arr_im[arr_im[:, :, 3] < 15] = 0
            im_clean = Image.fromarray(arr_im)
            im_save = im_clean.quantize(colors=100, method=Image.Quantize.FASTOCTREE)
        else:
            im_save = im
        im_save.save(img_path, format='PNG', optimize=True)
        sz = os.path.getsize(img_path)
        data_path = os.path.normpath(os.path.join(dir_data, f'{eff_id}'))
        with open(data_path, 'wb') as f_:
            f_.write(data_bytes)
        print(f'  [{z}] {sz} bytes')
    print(f'-> OK Effect {eff_id} x0..x4 (max {sz} bytes)')

# ==============================================================================
# DRAW ROTATING RING - FIX: 8 spikes de rotation ro rang
# ==============================================================================
def draw_ring_frame(H, W, alpha_rot, cx_ground=480.0, cy_ground=800.0, aspect=0.245):
    grid_y, grid_x = np.indices((H, W), dtype=float)
    X_g = grid_x - cx_ground
    Z_g = (grid_y - cy_ground) / aspect
    R_g = np.sqrt(X_g**2 + Z_g**2)
    Theta_g = np.arctan2(Z_g, X_g)
    Theta_rot = Theta_g - alpha_rot

    ring_layer = np.zeros((H, W, 4), dtype=float)

    # === FIX: Dung 8 spikes thay vi 24 de rotation ro rang hon ===
    num_spikes = 8
    spike_wave = np.maximum(0.0, np.cos(num_spikes * Theta_rot)) ** 2.5
    spike_r_max = 405.0 + 80.0 * spike_wave

    is_spike = (R_g >= 380) & (R_g <= spike_r_max)
    st = ((R_g - 380) / np.maximum(1.0, spike_r_max - 380))[is_spike]
    sw = spike_wave[is_spike]
    ring_layer[is_spike, 0] = 255
    ring_layer[is_spike, 1] = np.clip(25 + 200 * st * sw, 0, 255)
    ring_layer[is_spike, 2] = np.clip(40 + 200 * st * sw, 0, 255)
    ring_layer[is_spike, 3] = np.clip(255 * (1.0 - st**1.5), 0, 255)

    # Outer red border
    is_outer_red = (R_g >= 398) & (R_g <= 412)
    ring_layer[is_outer_red] = [240, 10, 8, 255]

    # Outer yellow ring
    is_outer_yel = (R_g >= 342) & (R_g < 398)
    ring_layer[is_outer_yel] = [255, 245, 45, 255]

    # Middle red divider
    is_mid_red = (R_g >= 330) & (R_g < 342)
    ring_layer[is_mid_red] = [240, 10, 8, 255]

    # === FIX: Middle golden ring - dung 8 ray segments de rotation ro rang ===
    is_mid_gold = (R_g >= 250) & (R_g < 330)
    notch = np.cos(num_spikes * Theta_rot)   # aligned with spikes
    r_g = np.where(notch[is_mid_gold] > 0.2, 255, 248)
    g_g = np.where(notch[is_mid_gold] > 0.2, 215, 175)
    b_g = np.where(notch[is_mid_gold] > 0.2, 55, 15)
    ring_layer[is_mid_gold, 0] = r_g
    ring_layer[is_mid_gold, 1] = g_g
    ring_layer[is_mid_gold, 2] = b_g
    ring_layer[is_mid_gold, 3] = 255

    # Inner red divider
    is_in_red = (R_g >= 238) & (R_g < 250)
    ring_layer[is_in_red] = [240, 10, 8, 255]

    # Inner yellow ring
    is_in_yel = (R_g >= 160) & (R_g < 238)
    ring_layer[is_in_yel] = [255, 235, 65, 255]

    # === FIX: 8 rotating teeth (khop voi spikes) ===
    num_teeth = 8
    teeth_wave = np.maximum(0.0, np.cos(num_teeth * Theta_rot + math.pi/8)) ** 2.0
    teeth_r_max = 160.0 + 50.0 * teeth_wave
    is_teeth = (R_g >= 145) & (R_g <= teeth_r_max)
    ring_layer[is_teeth] = [255, 255, 220, 255]

    # Core warm glow
    is_core = (R_g < 145)
    core_t = R_g[is_core] / 145.0
    ring_layer[is_core, 0] = 255
    ring_layer[is_core, 1] = np.clip(155 + 60 * core_t, 0, 255)
    ring_layer[is_core, 2] = np.clip(30 + 20 * core_t, 0, 255)
    ring_layer[is_core, 3] = 140 + 80 * core_t

    # Hide above ground
    ring_layer[grid_y < 690, 3] = 0

    return ring_layer


# ==============================================================================
# DRAW HAKI CONTINUOUS FLOW - FIX: khong dung modular particle de tranh blink
# ==============================================================================
def boost_haki_colors(arr):
    """
    Boost haki colors: tang red/dark contrast, giam grey
    Input: (H, W, 4) float array
    """
    r, g, b, a = arr[:,:,0], arr[:,:,1], arr[:,:,2], arr[:,:,3]
    
    # Detect black core (dark pixels with alpha)
    darkness = 255.0 - np.maximum(np.maximum(r, g), b)
    is_black = (darkness > 150) & (a > 50)
    
    # Detect red aura
    is_red = (r > g + 30) & (r > b + 20) & (a > 30)
    
    # Detect white/pink spark
    is_white = (r > 200) & (g > 180) & (a > 50)
    
    out = arr.copy()
    
    # Boost black core: make pure black
    out[is_black, 0] = 0
    out[is_black, 1] = 0
    out[is_black, 2] = 0
    out[is_black, 3] = np.clip(arr[is_black, 3] * 1.4, 0, 255)
    
    # Boost red aura: saturate red, suppress green/blue
    out[is_red, 0] = np.clip(arr[is_red, 0] * 1.3, 0, 255)
    out[is_red, 1] = np.clip(arr[is_red, 1] * 0.4, 0, 255)
    out[is_red, 2] = np.clip(arr[is_red, 2] * 0.4, 0, 255)
    out[is_red, 3] = np.clip(arr[is_red, 3] * 1.2, 0, 255)
    
    # Suppress grey (near neutral colors that are not black)
    is_grey = (~is_black) & (~is_red) & (~is_white)
    grey_val = (r + g + b) / 3.0
    is_neutral_grey = is_grey & (np.abs(r - grey_val) < 25) & (np.abs(g - grey_val) < 25)
    # Reduce alpha of neutral grey
    out[is_neutral_grey, 3] = np.clip(arr[is_neutral_grey, 3] * 0.3, 0, 255)
    
    return out


def draw_haki_frame(haki_src, H, W, frame_idx, num_frames=6,
                    cx_ground=480.0, cy_ground=800.0):
    """
    FIX: 3 lop scroll lien tuc, khong blink.
    Moi lop duoc color-boosted truoc khi composite.
    """
    grid_y, grid_x = np.indices((H, W), dtype=float)
    phase = frame_idx / float(num_frames)

    # Haki source: chi tren ground
    haki_src_f = haki_src.astype(float)
    # Boost colors truoc
    haki_src_f = boost_haki_colors(haki_src_f)
    
    haki_above = haki_src_f.copy()
    dist_to_ground = cy_ground - grid_y
    fade = np.clip((dist_to_ground - 10.0) / 80.0, 0.0, 1.0)
    haki_above[:, :, 3] *= fade
    haki_above[grid_y > cy_ground, 3] = 0

    haki_out = np.zeros((H, W, 4), dtype=float)

    for layer in range(3):
        layer_phase = (phase + layer / 3.0) % 1.0
        off_y = -layer_phase * 240.0

        src_y = grid_y - off_y
        src_x = grid_x + 8.0 * np.sin(2 * math.pi * (grid_y / 300.0 - phase)) * np.clip(1 - grid_y / cy_ground, 0, 1)

        x_i0 = np.clip(np.floor(src_x).astype(int), 0, W - 2)
        x_i1 = x_i0 + 1
        y_i0 = np.clip(np.floor(src_y).astype(int), 0, H - 2)
        y_i1 = y_i0 + 1

        wx = np.clip((src_x - x_i0)[:, :, None], 0, 1)
        wy = np.clip((src_y - y_i0)[:, :, None], 0, 1)

        sampled = (
            haki_above[y_i0, x_i0] * (1 - wx) * (1 - wy) +
            haki_above[y_i0, x_i1] * wx * (1 - wy) +
            haki_above[y_i1, x_i0] * (1 - wx) * wy +
            haki_above[y_i1, x_i1] * wx * wy
        )

        brightness = 1.0 - layer * 0.15
        sampled[:, :, 3] *= brightness

        a_s = sampled[:, :, 3:] / 255.0
        a_o = haki_out[:, :, 3:] / 255.0
        a_out = a_s + a_o * (1 - a_s)
        rgb_out = np.where(a_out > 0,
            (sampled[:, :, :3] * a_s + haki_out[:, :, :3] * a_o * (1 - a_s)) / np.maximum(a_out, 1e-6),
            0)
        haki_out[:, :, :3] = rgb_out * 255
        haki_out[:, :, 3] = np.clip(a_out[:, :, 0] * 255, 0, 255)

    haki_out = np.clip(haki_out, 0, 255)
    return haki_out


# ==============================================================================
# MAIN: Generate 6-frame Effect 920 with rotating ring + continuous haki
# ==============================================================================
def generate_effect_920():
    print("Loading Haki source...")
    im_src = Image.open(HAKI_SRC)
    src_full = np.array(im_src).astype(np.uint8)

    crop_w, crop_h = 960, 960
    x0, y0 = 32, 45
    haki_src = src_full[y0:y0+crop_h, x0:x0+crop_w]  # (960, 960, 4)
    H, W = haki_src.shape[:2]

    cx_ground = 480.0
    cy_ground = 800.0
    aspect = 0.245

    num_frames = 6
    frames_240 = []
    grid_y_full, grid_x_full = np.indices((H, W), dtype=float)

    print(f"Generating {num_frames} frames (ring rotation + continuous haki)...")
    for fi in range(num_frames):
        # === 1. Ring layer - xoay 60 do moi frame ===
        # FIX: 8 spikes -> 360/8 = 45 do/spike -> 60 do rotation = 1.33 spike width => ROI RANG
        alpha_rot = fi * (2.0 * math.pi / num_frames)
        ring_layer = draw_ring_frame(H, W, alpha_rot, cx_ground, cy_ground, aspect)

        # === 2. Haki layer - continuous flow, no blink ===
        haki_layer = draw_haki_frame(haki_src, H, W, fi, num_frames, cx_ground, cy_ground)

        # === 3. Composite: ring + haki ===
        a_haki = haki_layer[:, :, 3:] / 255.0
        a_ring = ring_layer[:, :, 3:] / 255.0
        out_rgb = ring_layer[:, :, :3] * a_ring + haki_layer[:, :, :3] * a_haki * (1.0 - a_ring)
        out_a = np.clip((a_ring + a_haki * (1.0 - a_ring)) * 255.0, 0, 255)
        out_frame = np.concatenate([out_rgb, out_a], axis=2)

        img_960 = Image.fromarray(np.clip(out_frame, 0, 255).astype(np.uint8))
        img_240 = img_960.resize((240, 240), Image.Resampling.LANCZOS)
        frames_240.append(img_240)
        print(f"  Frame {fi} done")

    # Save GIF preview
    preview_path = 'C:/Users/admin/.gemini/antigravity-ide/brain/992601b6-fcd2-41b8-819d-db234853d047/scratch/eff920_preview.gif'
    os.makedirs(os.path.dirname(preview_path), exist_ok=True)
    frames_240[0].save(preview_path, save_all=True, append_images=frames_240[1:], duration=100, loop=0)
    print(f"Saved preview GIF: {preview_path}")

    # === 4. Assemble 3x2 sprite sheet (720x480 at 4x) ===
    sheet_x4 = Image.new('RGBA', (720, 480), (0, 0, 0, 0))
    for i, f_img in enumerate(frames_240):
        col = i % 3
        row = i // 3
        sheet_x4.paste(f_img, (col * 240, row * 240))

    # SmallImage: each cell at 1x = 60x60
    # Layout: 3 cols x 2 rows
    small_images = [
        [0,   0, 0, 60, 60],
        [1,  60, 0, 60, 60],
        [2, 120, 0, 60, 60],
        [3,   0, 60, 60, 60],
        [4,  60, 60, 60, 60],
        [5, 120, 60, 60, 60],
    ]

    # dx, dy: center of ring is at 120px from left, 200px from top at 4x
    # => 1x: 30px from left, 50px from top => dx=-30, dy=-50
    dx = -30
    dy = -50

    frame_list = [
        [(dx, dy, 0, 0, 0)],
        [(dx, dy, 1, 0, 0)],
        [(dx, dy, 2, 0, 0)],
        [(dx, dy, 3, 0, 0)],
        [(dx, dy, 4, 0, 0)],
        [(dx, dy, 5, 0, 0)],
    ]

    # Sequence: 12 steps, moi frame duoc hien thi 2 lan -> 200ms/frame rotation
    # FIX: thay doi sequence de ring xoay smoother (moi frame 1 lan thay vi 2 lan)
    # voi 6 frames x 1 hinh = 100ms/frame -> rotation tron 1 vong mat 600ms (OK!)
    sequence = [0, 1, 2, 3, 4, 5]

    data_bytes = build_data_effect(small_images, frame_list, sequence)
    print(f"Data size: {len(data_bytes)} bytes")

    save_multizoom_effect(920, sheet_x4, data_bytes)
    print("Done! Effect 920 updated.")

if __name__ == '__main__':
    generate_effect_920()
