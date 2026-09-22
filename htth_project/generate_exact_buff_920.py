import os
import sys
import struct
import math
import random
import numpy as np
from PIL import Image, ImageDraw

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

# ==============================================================================
# PIXEL-PERFECT DRAWING FUNCTIONS FOR NIKA AWAKENING BUFF (MATCHING USER SCREENSHOT)
# ==============================================================================

def draw_star_8point(d, cx, cy, r_main=15, r_diag=10, pulse=0):
    rm = int(r_main + pulse)
    rd = int(r_diag + pulse * 0.7)
    
    # 1. Golden soft aura
    d.ellipse([cx - rm - 3, cy - rm - 3, cx + rm + 3, cy + rm + 3], fill=(255, 215, 0, 80))
    
    # 2. Main Rays (Vertical & Horizontal) - Tapered diamond polygons
    col_gold_edge = (240, 185, 0, 255)
    col_gold = (255, 225, 30, 255)
    col_white = (255, 255, 255, 255)
    
    # Horizontal ray
    d.polygon([(cx - rm, cy), (cx, cy - 3), (cx + rm, cy), (cx, cy + 3)], fill=col_gold)
    d.line([(cx - rm, cy), (cx + rm, cy)], fill=col_white, width=1)
    
    # Vertical ray
    d.polygon([(cx, cy - rm), (cx - 3, cy), (cx, cy + rm), (cx + 3, cy)], fill=col_gold)
    d.line([(cx, cy - rm), (cx, cy + rm)], fill=col_white, width=1)
    
    # 4 Diagonal rays (45 deg)
    d.polygon([(cx - rd, cy - rd), (cx + 1, cy - 2), (cx + rd, cy + rd), (cx - 1, cy + 2)], fill=col_gold)
    d.polygon([(cx - rd, cy + rd), (cx - 2, cy - 1), (cx + rd, cy - rd), (cx + 2, cy + 1)], fill=col_gold)
    d.line([(cx - rd, cy - rd), (cx + rd, cy + rd)], fill=col_white, width=1)
    d.line([(cx - rd, cy + rd), (cx + rd, cy - rd)], fill=col_white, width=1)
    
    # Center diamond / bright core
    d.polygon([(cx - 4, cy), (cx, cy - 4), (cx + 4, cy), (cx, cy + 4)], fill=col_white)

def draw_cloud_cluster_bank(d, cx, cy_apex=26):
    """
    Vẽ vòm mây trắng Thần Nika chuẩn từng cụm mây tròn viền xanh như screenshot:
    - 2 tầng mây xếp lớp (tầng trên nhỏ, tầng dưới lớn hơn)
    - Viền xanh dương pixel đặc trưng (#4678b4)
    - Các chấm đổ bóng mềm bên trong (#d8e7f8)
    """
    col_border = (70, 120, 180, 255) # #4678b4
    col_fill = (255, 255, 255, 255)
    col_dot = (216, 232, 248, 255)   # #d8e8f8
    
    # Tọa độ các khối mây (cx, cy, radius)
    cloud_defs = [
        # Đỉnh vòm (Top Center Apex)
        (cx, cy_apex + 4, 28),
        (cx - 22, cy_apex + 12, 24),
        (cx + 22, cy_apex + 12, 24),
        (cx - 44, cy_apex + 22, 23),
        (cx + 44, cy_apex + 22, 23),
        # Lưng chừng vòm (Mid Arch)
        (cx - 66, cy_apex + 38, 22),
        (cx + 66, cy_apex + 38, 22),
        (cx - 86, cy_apex + 58, 21),
        (cx + 86, cy_apex + 58, 21),
        # Chân vòm mây 2 bên (Lower ends)
        (cx - 102, cy_apex + 82, 19),
        (cx + 102, cy_apex + 82, 19),
        (cx - 114, cy_apex + 106, 17),
        (cx + 114, cy_apex + 106, 17),
        # Lớp phụ tạo độ dày và bồng bềnh
        (cx - 14, cy_apex - 2, 20),
        (cx + 14, cy_apex - 2, 20),
        (cx, cy_apex - 6, 18),
        (cx - 36, cy_apex + 6, 18),
        (cx + 36, cy_apex + 6, 18),
        (cx - 72, cy_apex + 26, 16),
        (cx + 72, cy_apex + 26, 16),
    ]
    
    # 1. Vẽ viền xanh ngoài cùng trước
    for x, y, r in cloud_defs:
        d.ellipse([x - r - 2, y - r - 2, x + r + 2, y + r + 2], fill=col_border)
        
    # 2. Vẽ thân mây trắng đặc
    for x, y, r in cloud_defs:
        d.ellipse([x - r, y - r, x + r, y + r], fill=col_fill)
        
    # 3. Vẽ các chấm hoa văn bóng mây tròn mềm bên trong
    for x, y, r in cloud_defs:
        dr = max(2, int(r * 0.22))
        d.ellipse([x - int(r*0.35) - dr, y + int(r*0.15) - dr, x - int(r*0.35) + dr, y + int(r*0.15) + dr], fill=col_dot)
        d.ellipse([x + int(r*0.25) - dr, y + int(r*0.25) - dr, x + int(r*0.25) + dr, y + int(r*0.25) + dr], fill=col_dot)
        d.ellipse([x - dr, y + int(r*0.42) - dr, x + dr, y + int(r*0.42) + dr], fill=col_dot)

def draw_dark_haki_tongue(d, cx, cy_top=52):
    """
    Vẽ ngọn lửa Haki đen viền đỏ bốc lên ở vòm trong dưới mây (chuẩn ảnh mẫu)
    """
    tongue_pts = [
        (cx - 40, cy_top + 45),
        (cx - 28, cy_top + 30),
        (cx - 16, cy_top + 18),
        (cx, cy_top), # Đỉnh nhọn chọc lên mây
        (cx + 16, cy_top + 18),
        (cx + 28, cy_top + 30),
        (cx + 40, cy_top + 45),
    ]
    # Crimson aura outline
    for i in range(len(tongue_pts) - 1):
        d.line([tongue_pts[i], tongue_pts[i+1]], fill=(255, 0, 36, 255), width=10)
    # Solid black core
    for i in range(len(tongue_pts) - 1):
        d.line([tongue_pts[i], tongue_pts[i+1]], fill=(0, 0, 0, 255), width=5)

def draw_conqueror_bolt(d, pts, base_w=7):
    """
    Tia sét Haki Bá Vương đen viền đỏ hung dữ (chuẩn ảnh mẫu):
    - Lớp ngoài: Hào quang đỏ rực rỡ (#ff0024)
    - Lớp trong: Lõi đen đặc hoàn toàn (#000000)
    """
    # Pass 1: Crimson aura
    for i in range(len(pts) - 1):
        p1, p2 = pts[i], pts[i+1]
        d.line([p1, p2], fill=(255, 0, 36, 255), width=base_w + 6)
        r = (base_w + 6) // 2
        d.ellipse([p1[0]-r, p1[1]-r, p1[0]+r, p1[1]+r], fill=(255, 0, 36, 255))
        d.ellipse([p2[0]-r, p2[1]-r, p2[0]+r, p2[1]+r], fill=(255, 0, 36, 255))
    # Pass 2: Solid black core
    for i in range(len(pts) - 1):
        p1, p2 = pts[i], pts[i+1]
        d.line([p1, p2], fill=(0, 0, 0, 255), width=base_w)
        r = base_w // 2
        d.ellipse([p1[0]-r, p1[1]-r, p1[0]+r, p1[1]+r], fill=(0, 0, 0, 255))
        d.ellipse([p2[0]-r, p2[1]-r, p2[0]+r, p2[1]+r], fill=(0, 0, 0, 255))

def draw_pink_white_spark(d, pts, base_w=2):
    """
    Tia sét trắng viền hồng rực bao quanh người nhân vật
    """
    for i in range(len(pts) - 1):
        d.line([pts[i], pts[i+1]], fill=(255, 20, 100, 230), width=base_w + 3)
    for i in range(len(pts) - 1):
        d.line([pts[i], pts[i+1]], fill=(255, 255, 255, 255), width=base_w)

def draw_nika_buff_frame_exact(w=256, h=256, phase=0):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2 # 128
    cy_ground = h - 54 # 202 (Mặt đất phẳng ngay dưới chân nhân vật)

    # ==========================================================================
    # 1. GROUND SHOCKWAVE RINGS (VÀNH ĐẤT NỔ THẦN NIKA - CHUẨN SCREENSHOT)
    # ==========================================================================
    # 1.1 Outermost radiating jagged red spikes (Gai đỏ sắc nhọn phóng ra mặt đất)
    num_spikes = 32
    rx_sp = 120 + (phase % 2) * 2
    ry_sp = 34 + (phase % 2) * 1
    for i in range(num_spikes):
        ang = (i / float(num_spikes)) * math.pi * 2
        # Base on yellow ring edge
        bx = cx + math.cos(ang) * (rx_sp * 0.78)
        by = cy_ground + math.sin(ang) * (ry_sp * 0.78)
        # Tip of spike
        sp_len = 1.15 + (0.18 if (i % 2 == 0) else -0.04)
        tx = cx + math.cos(ang) * (rx_sp * sp_len)
        ty = cy_ground + math.sin(ang) * (ry_sp * sp_len)
        # Draw spike triangle
        dx_perp = -math.sin(ang) * 3.5
        dy_perp = math.cos(ang) * 3.5
        d.polygon([(bx + dx_perp, by + dy_perp), (bx - dx_perp, by - dy_perp), (tx, ty)], fill=(255, 0, 36, 255))
        d.line([(bx, by), (tx, ty)], fill=(255, 140, 180, 255), width=1)

    # 1.2 Ring 1: Bold Yellow Band with Red border
    rx1, ry1 = 112, 32
    d.ellipse([cx - rx1 - 2, cy_ground - ry1 - 1, cx + rx1 + 2, cy_ground + ry1 + 1], fill=(230, 0, 30, 255))
    d.ellipse([cx - rx1, cy_ground - ry1, cx + rx1, cy_ground + ry1], fill=(255, 204, 0, 255))

    # 1.3 Ring 2: Light Butter/Cream Yellow Band with Red border
    rx2, ry2 = 94, 25
    d.ellipse([cx - rx2 - 2, cy_ground - ry2 - 1, cx + rx2 + 2, cy_ground + ry2 + 1], fill=(230, 0, 30, 255))
    d.ellipse([cx - rx2, cy_ground - ry2, cx + rx2, cy_ground + ry2], fill=(255, 246, 165, 255))

    # 1.4 Ring 3: Intense Fiery Red-Orange Band
    rx3, ry3 = 74, 18
    d.ellipse([cx - rx3 - 1, cy_ground - ry3 - 1, cx + rx3 + 1, cy_ground + ry3 + 1], fill=(210, 0, 25, 255))
    d.ellipse([cx - rx3, cy_ground - ry3, cx + rx3, cy_ground + ry3], fill=(255, 45, 10, 255))

    # 1.5 Ring 4: Deep Maroon Inner Basin with White Shockwave teeth
    rx4, ry4 = 54, 13
    d.ellipse([cx - rx4, cy_ground - ry4, cx + rx4, cy_ground + ry4], fill=(130, 0, 40, 255))
    
    # Yellow tick marks around inner basin rim (như trong ảnh mẫu)
    for i in range(24):
        ang = (i / 24.0) * math.pi * 2
        px1 = cx + math.cos(ang) * (rx4 * 0.85)
        py1 = cy_ground + math.sin(ang) * (ry4 * 0.85)
        px2 = cx + math.cos(ang) * (rx4 * 1.05)
        py2 = cy_ground + math.sin(ang) * (ry4 * 1.05)
        d.line([(px1, py1), (px2, py2)], fill=(255, 220, 30, 255), width=2)
        
    # White-hot radial teeth/spikes inside center
    for i in range(16):
        ang = (i / 16.0) * math.pi * 2
        px1 = cx + math.cos(ang) * (rx4 * 0.35)
        py1 = cy_ground + math.sin(ang) * (ry4 * 0.35)
        px2 = cx + math.cos(ang) * (rx4 * 0.95)
        py2 = cy_ground + math.sin(ang) * (ry4 * 0.95) - 3
        d.line([(px1, py1), (px2, py2)], fill=(255, 255, 255, 255), width=2)
    # White-hot center flash
    d.ellipse([cx - 16, cy_ground - 4, cx + 16, cy_ground + 4], fill=(255, 255, 255, 255))

    # ==========================================================================
    # 2. TWO PILLARS OF BLACK-RED CONQUEROR'S HAKI LIGHTNING (CHÂN ĐẾ ĐẾN VÒM MÂY)
    # ==========================================================================
    shift = (phase % 2) * 3
    # Left jagged bolt
    pts_left = [
        (cx - 82, cy_ground - 4),
        (cx - 68 + shift, cy_ground - 30),
        (cx - 86 - shift, cy_ground - 58),
        (cx - 64, cy_ground - 86),
        (cx - 82 + shift, cy_ground - 114),
        (cx - 60, cy_ground - 138),
        (cx - 80, cy_ground - 156),
        (cx - 96, cy_ground - 170)
    ]
    draw_conqueror_bolt(d, pts_left, base_w=7)

    # Right jagged bolt
    pts_right = [
        (cx + 82, cy_ground - 4),
        (cx + 68 - shift, cy_ground - 30),
        (cx + 86 + shift, cy_ground - 58),
        (cx + 64, cy_ground - 86),
        (cx + 82 - shift, cy_ground - 114),
        (cx + 60, cy_ground - 138),
        (cx + 80, cy_ground - 156),
        (cx + 96, cy_ground - 170)
    ]
    draw_conqueror_bolt(d, pts_right, base_w=7)

    # Branching sub-bolts
    branch_l1 = [(cx - 86, cy_ground - 58), (cx - 104, cy_ground - 74), (cx - 112, cy_ground - 96)]
    draw_conqueror_bolt(d, branch_l1, base_w=4)
    branch_r1 = [(cx + 86, cy_ground - 58), (cx + 104, cy_ground - 74), (cx + 112, cy_ground - 96)]
    draw_conqueror_bolt(d, branch_r1, base_w=4)

    # ==========================================================================
    # 3. WHITE-PINK LIGHTNING FORKS CRACKLING AROUND BODY
    # ==========================================================================
    sparks = [
        # Left side rising sparks
        [(cx - 55, cy_ground - 12), (cx - 40, cy_ground - 42), (cx - 52, cy_ground - 72), (cx - 32, cy_ground - 100)],
        # Right side rising sparks
        [(cx + 55, cy_ground - 12), (cx + 40, cy_ground - 42), (cx + 52, cy_ground - 72), (cx + 32, cy_ground - 100)],
        # Close waist sparks
        [(cx - 34, cy_ground - 16), (cx - 22, cy_ground - 48), (cx - 32, cy_ground - 76)],
        [(cx + 34, cy_ground - 16), (cx + 22, cy_ground - 48), (cx + 32, cy_ground - 76)],
        # Inner leg sparks
        [(cx - 18, cy_ground - 8), (cx - 10, cy_ground - 30)],
        [(cx + 18, cy_ground - 8), (cx + 10, cy_ground - 30)]
    ]
    for sp in sparks:
        draw_pink_white_spark(d, sp, base_w=2)

    # ==========================================================================
    # 4. UNDER-CLOUD DARK HAKI TONGUE (NGỌN LỬA HAKI ĐEN DƯỚI MÂY)
    # ==========================================================================
    draw_dark_haki_tongue(d, cx=cx, cy_top=48)

    # ==========================================================================
    # 5. OVERHEAD CLOUD ARCH WITH 5 GOLDEN STARS (VÒM MÂY VÀ 5 NGÔI SAO VÀNG)
    # ==========================================================================
    draw_cloud_cluster_bank(d, cx=cx, cy_apex=24)

    # 5 Golden Stars exactly at the positions in the screenshot
    star_positions = [
        (cx - 78, 100),
        (cx - 42, 54),
        (cx, 36),
        (cx + 42, 54),
        (cx + 78, 100)
    ]
    for idx, (sx, sy) in enumerate(star_positions):
        pulse = 2 if (idx % 2 == phase % 2) else 0
        draw_star_8point(d, sx, sy, r_main=14, r_diag=9, pulse=pulse)

    return im

def build_data_effect(small_images, frames, sequence, frame_char=None, index_splash=None):
    out = bytearray()
    out.append(len(small_images))
    for s in small_images:
        out.append(s[0])
        out.append(s[1])
        out.append(s[2])
        out.append(s[3])
        out.append(s[4])
    
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
        'x3': img_x4.resize((w1 * 3, h1 * 3), Image.Resampling.BILINEAR),
        'x2': img_x4.resize((w1 * 2, h1 * 2), Image.Resampling.BILINEAR),
        'x1': img_x4.resize((w1 * 1, h1 * 1), Image.Resampling.NEAREST),
        'x0': img_x4.resize((w1 * 1, h1 * 1), Image.Resampling.NEAREST)
    }
    
    for z, im in zooms.items():
        dir_img = os.path.normpath(os.path.join(base_dir, z, 'img'))
        dir_data = os.path.normpath(os.path.join(base_dir, z, 'data'))
        os.makedirs(dir_img, exist_ok=True)
        os.makedirs(dir_data, exist_ok=True)
        
        img_path = os.path.normpath(os.path.join(dir_img, f'{eff_id}.png'))
        im.save(img_path, format='PNG', optimize=True)
        sz = os.path.getsize(img_path)
        
        data_path = os.path.normpath(os.path.join(dir_data, f'{eff_id}'))
        with open(data_path, 'wb') as f:
            f.write(data_bytes)
    print(f"-> [OK] Effect ID {eff_id} đã lưu thành công x0..x4 (File size x4: {sz} bytes, sắc nét tuyệt đối)")

def create_effect_920_exact_as_image():
    w_box = 256
    h_box = 256
    f0 = draw_nika_buff_frame_exact(w_box, h_box, phase=0)
    f1 = draw_nika_buff_frame_exact(w_box, h_box, phase=1)
    f2 = draw_nika_buff_frame_exact(w_box, h_box, phase=2)
    f3 = draw_nika_buff_frame_exact(w_box, h_box, phase=3)
    
    frames_img = [f0, f1, f2, f3]
    # 2 cols x 2 rows = 512 x 512 (1x: 128 x 128 <= 255)
    cols = 2
    rows = 2
    im = Image.new('RGBA', (w_box * cols, h_box * rows), (0, 0, 0, 0))
    small_imgs = []
    for idx, f_im in enumerate(frames_img):
        col = idx % cols
        row = idx // cols
        gx = col * w_box
        gy = row * h_box
        im.paste(f_im, (gx, gy))
        small_imgs.append([idx, gx // 4, gy // 4, w_box // 4, h_box // 4])
    
    # dx = -128 // 4 = -32
    # dy = -202 // 4 = -50 (chân nhân vật đứng chính xác tại tâm vành đất)
    dx = -32
    dy = -50
    
    frame_list = [
        [(dx, dy, 0, 0, 0)],
        [(dx, dy, 1, 0, 0)],
        [(dx, dy, 2, 0, 0)],
        [(dx, dy, 3, 0, 0)]
    ]
    seq = [0, 0, 1, 1, 2, 2, 3, 3]
    data_bytes = build_data_effect(small_imgs, frame_list, seq)
    save_multizoom_effect(920, im, data_bytes)

if __name__ == '__main__':
    print("Vẽ lại hiệu ứng Effect 920 (Buff Nika) chính xác 100% như hình mẫu...")
    create_effect_920_exact_as_image()
