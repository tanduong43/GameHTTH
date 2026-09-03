#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Script tạo hình ảnh Sprite sheet và nhị phân TeaMobi Effect Data cho 3 chiêu thức
Trái Nikyu Nikyu no Mi (Kuma) và bộ Icon kỹ năng.
- Effect 910: Áp Lực Pháo (Pad Ho)
- Effect 911: Đại Hùng Chưởng (Ursus Shock)
- Effect 912: Đệm Thịt Hộ Thể (Nikyu Defense & Repel Pain)
- Icon ID 4416: Trái Nikyu Nikyu (Item 1015)
- Icon ID 4417: Skill 1 - Áp Lực Pháo
- Icon ID 4418: Skill 2 - Đại Hùng Chưởng
- Icon ID 4419: Skill 3 - Đệm Thịt Hộ Thể
"""

import os
import sys
import struct
import math
from PIL import Image, ImageDraw

sys.stdout.reconfigure(encoding='utf-8')

def draw_paw_pad(draw, cx, cy, scale=1.0, color_main=(255, 105, 180, 240), 
                 color_highlight=(255, 255, 255, 255), color_outline=(255, 255, 255, 200)):
    main_rx = int(12 * scale)
    main_ry = int(10 * scale)
    draw.ellipse([cx - main_rx, cy - main_ry + int(2*scale), 
                  cx + main_rx, cy + main_ry + int(2*scale)], 
                 fill=color_main, outline=color_outline, width=max(1, int(scale)))
    
    hl_rx = max(1, int(main_rx * 0.45))
    hl_ry = max(1, int(main_ry * 0.35))
    draw.ellipse([cx - hl_rx, cy - hl_ry - int(1*scale), 
                  cx + hl_rx, cy + hl_ry - int(1*scale)], 
                 fill=color_highlight)

    toes = [
        (-8 * scale, -10 * scale, 3.5 * scale, 3.5 * scale),
        (-3 * scale, -13 * scale, 4.0 * scale, 4.0 * scale),
        ( 3 * scale, -13 * scale, 4.0 * scale, 4.0 * scale),
        ( 8 * scale, -10 * scale, 3.5 * scale, 3.5 * scale)
    ]
    for tx, ty, trx, try_ in toes:
        draw.ellipse([cx + tx - trx, cy + ty - try_, 
                      cx + tx + trx, cy + ty + try_], 
                     fill=color_main, outline=color_outline, width=max(1, int(scale * 0.8)))
        draw.ellipse([cx + tx - trx*0.35, cy + ty - try_*0.35 - int(scale*0.5), 
                      cx + tx + trx*0.35, cy + ty + try_*0.35 - int(scale*0.5)], 
                     fill=color_highlight)

def draw_shockwave_ring(draw, cx, cy, rx, ry, color=(224, 247, 250, 200), width=2):
    draw.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], outline=color, width=width)

def draw_sparkle(draw, cx, cy, r=6, color=(255, 255, 255, 240)):
    draw.line([(cx - r, cy), (cx + r, cy)], fill=color, width=2)
    draw.line([(cx, cy - r), (cx, cy + r)], fill=color, width=2)
    r2 = int(r * 0.6)
    draw.line([(cx - r2, cy - r2), (cx + r2, cy + r2)], fill=color, width=1)
    draw.line([(cx - r2, cy + r2), (cx + r2, cy - r2)], fill=color, width=1)

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
    base_dir = 'htth_project/data/template/skill'
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
        dir_img = os.path.join(base_dir, z, 'img')
        dir_data = os.path.join(base_dir, z, 'data')
        os.makedirs(dir_img, exist_ok=True)
        os.makedirs(dir_data, exist_ok=True)
        
        # Optimize PNG to keep size very small and clean
        img_path = os.path.join(dir_img, f'{eff_id}.png')
        im_q = im.quantize(colors=128, method=Image.Quantize.FASTOCTREE)
        im_q.save(img_path, format='PNG', optimize=True)
        
        with open(os.path.join(dir_data, f'{eff_id}'), 'wb') as f:
            f.write(data_bytes)
    print(f"-> Đã tạo thành công Effect ID {eff_id} cho toàn bộ các zoom x0..x4 (tối ưu dung lượng)")

# ==============================================================================
# 1. TẠO EFFECT 910: PAD HO (ÁP LỰC PHÁO - CHƯỞNG TỪ NHỎ ĐẾN KHỔNG LỒ)
# ==============================================================================
def draw_horizontal_paw(draw, cx, cy, rx, ry, scale=4.0, has_speed_tail=True):
    if has_speed_tail:
        tail_len = int(rx * 1.4)
        for dy_off, a in [(-int(ry*0.4), 80), (0, 180), (int(ry*0.4), 80)]:
            draw.line([(cx - rx, cy + dy_off), (cx - rx - tail_len, cy + dy_off)],
                      fill=(255, 235, 180, a), width=max(1, int(1.5*scale)))
        draw.arc([cx - rx - int(10*scale), cy - ry - int(4*scale),
                  cx - rx + int(6*scale), cy + ry + int(4*scale)],
                 start=90, end=270, fill=(255, 255, 255, 180), width=max(1, int(1.5*scale)))

    for r_add, a in [(int(6*scale), 50), (int(3*scale), 120)]:
        draw.ellipse([cx - rx - r_add, cy - ry - r_add,
                      cx + rx + r_add, cy + ry + r_add],
                     fill=(255, 80, 160, a), outline=(255, 240, 220, a+50), width=max(1, int(scale)))

    draw.ellipse([cx - rx, cy - ry, cx + rx, cy + ry],
                 fill=(40, 30, 45, 240), outline=(255, 255, 255, 255), width=max(1, int(1.2*scale)))

    prx = int(rx * 0.72)
    pry = int(ry * 0.68)
    draw.ellipse([cx - prx, cy - pry, cx + prx, cy + pry],
                 fill=(255, 105, 180, 255), outline=(255, 250, 250, 255), width=max(1, int(scale)))
    draw.ellipse([cx - int(prx*0.45), cy - int(pry*0.4), cx + int(prx*0.45), cy + int(pry*0.25)],
                 fill=(255, 200, 225, 255))
    draw.ellipse([cx - int(prx*0.2), cy - int(pry*0.3), cx + int(prx*0.2), cy],
                 fill=(255, 255, 255, 255))

    toes = [
        (0.72, -0.65, 0.24),
        (0.95, -0.22, 0.27),
        (0.95,  0.22, 0.27),
        (0.72,  0.65, 0.24)
    ]
    for bx, by, br in toes:
        tx = cx + bx * rx
        ty = cy + by * ry
        tr = br * rx
        draw.ellipse([tx - tr, ty - tr, tx + tr, ty + tr],
                     fill=(255, 105, 180, 255), outline=(255, 255, 255, 250), width=max(1, int(0.8*scale)))
        draw.ellipse([tx - tr*0.4, ty - tr*0.4, tx + tr*0.4, ty + tr*0.4],
                     fill=(255, 255, 255, 255))

def create_effect_910_pad_ho():
    scale = 4
    w_sheet = 240 * scale
    h_sheet = 120 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))
    draw = ImageDraw.Draw(im)

    # Sprite 0: Mini Pad (w=22, h=20) -> x=0, y=0
    draw_horizontal_paw(draw, 11*scale, 10*scale, rx=7*scale, ry=6*scale, scale=scale, has_speed_tail=False)

    # Sprite 1: Medium Pad (w=34, h=30) -> x=24, y=0
    draw_horizontal_paw(draw, (24+17)*scale, 15*scale, rx=11*scale, ry=9*scale, scale=scale, has_speed_tail=True)

    # Sprite 2: Large Pad (w=48, h=42) -> x=60, y=0
    draw_horizontal_paw(draw, (60+24)*scale, 21*scale, rx=16*scale, ry=13*scale, scale=scale, has_speed_tail=True)

    # Sprite 3: Giant Pad Cannon (w=68, h=56) -> x=110, y=0
    draw_horizontal_paw(draw, (110+34)*scale, 28*scale, rx=23*scale, ry=18*scale, scale=scale, has_speed_tail=True)

    # Sprite 6: Dust & Sparks (w=38, h=34) -> x=180, y=0
    cx6, cy6 = (180 + 19)*scale, 17*scale
    for dx_c, dy_c, rc, col in [
        (-8, 2, 9, (180, 170, 170, 180)),
        (4, -4, 8, (210, 200, 200, 200)),
        (0, 4, 7, (235, 230, 230, 220)),
        (-4, -2, 6, (250, 245, 245, 240))
    ]:
        draw.ellipse([(cx6 + dx_c*scale - rc*scale), (cy6 + dy_c*scale - rc*scale),
                      (cx6 + dx_c*scale + rc*scale), (cy6 + dy_c*scale + rc*scale)],
                     fill=col)

    # Row 2 (y=60)
    # Sprite 4: Impact Blast Flash (w=64, h=50) -> x=0, y=60
    cx4, cy4 = 32*scale, (60 + 25)*scale
    star_rays = [
        (0, -22), (6, -9), (26, -5), (9, 4), (20, 20), (3, 10), (-7, 22), (-9, 7), (-26, 9), (-11, -4), (-20, -18), (-5, -9)
    ]
    pts = [(cx4 + x*scale, cy4 + y*scale) for x, y in star_rays]
    draw.polygon(pts, fill=(255, 220, 80, 240), outline=(255, 255, 255, 255))
    draw.ellipse([cx4 - 12*scale, cy4 - 9*scale, cx4 + 12*scale, cy4 + 9*scale], fill=(255, 255, 255, 255))
    for ang in range(0, 360, 45):
        rad = math.radians(ang)
        x2 = cx4 + math.cos(rad) * 28 * scale
        y2 = cy4 + math.sin(rad) * 20 * scale
        draw.line([(cx4, cy4), (x2, y2)], fill=(255, 255, 255, 240), width=int(2*scale))

    # Sprite 5: Expanding Blast Shockwave (w=84, h=50) -> x=66, y=60
    cx5, cy5 = (66 + 42)*scale, (60 + 25)*scale
    for rx, ry, col, w in [(40*scale, 23*scale, (255, 200, 220, 130), 2),
                           (32*scale, 18*scale, (255, 235, 160, 180), 2),
                           (22*scale, 12*scale, (255, 255, 255, 255), 3)]:
        draw.ellipse([cx5 - rx, cy5 - ry, cx5 + rx, cy5 + ry], outline=col, width=int(w*scale/2))

    small_imgs = [
        (0,   0,  0, 22, 20), # Sprite 0: Mini Pad
        (1,  24,  0, 34, 30), # Sprite 1: Medium Pad
        (2,  60,  0, 48, 42), # Sprite 2: Large Pad
        (3, 110,  0, 68, 56), # Sprite 3: Giant Pad Cannon
        (4,   0, 60, 64, 50), # Sprite 4: Impact Blast Flash
        (5,  66, 60, 84, 50), # Sprite 5: Expanding Blast Shockwave
        (6, 180,  0, 38, 34), # Sprite 6: Dust & Sparks
    ]

    # HOẠT ẢNH: ĐẠN CHƯỞNG TỪ NHỎ XÍU TỤ KHÍ -> NỞ TO DẦN -> CỰC ĐẠI KHỔNG LỒ -> NỔ TUNG
    frames = [
        # Frame 0: Tụ đạn nhỏ ngay trước ngực/tay nhân vật (Sprite 0: w=22, h=20)
        [(-11, -25, 0, 0, 1)],
        # Frame 1: Bắt đầu phóng ra, nở vừa (Sprite 1: w=34, h=30)
        [(10, -27, 1, 0, 1)],
        # Frame 2: Bay nhanh, nở lớn với sóng áp lực (Sprite 2: w=48, h=42)
        [(35, -30, 2, 0, 1)],
        # Frame 3: Phóng cực đại khổng lồ càn quét mục tiêu (Sprite 3: w=68, h=56)
        [(65, -34, 3, 0, 1)],
        # Frame 4: Đâm trúng mục tiêu - NỔ TUNG VA CHẠM! (Sprite 4 + 5)
        [
            (55, -32, 5, 0, 1), # Sóng nổ
            (70, -32, 4, 0, 1), # Chớp nổ
        ],
        # Frame 5: Sóng nổ bung tỏa cực đại và khói bụi
        [
            (55, -32, 5, 0, 1), # Sóng nổ cực đại
            (68, -28, 6, 0, 1), # Khói bụi & tia sáng
        ],
        # Frame 6: Khói bụi tàn dư mờ dần
        [(72, -28, 6, 0, 1)],
        # Frame 7: Biến mất hoàn toàn
        []
    ]

    # SEQUENCE: KÉO DÀI, CHẬM RÃI, NHÌN RÕ MỌI GIAI ĐOẠN (27 ticks ~ 0.9 giây)
    seq = [
        0, 0, 0, 0,    # Tụ đạn nhỏ xíu (4 ticks)
        1, 1, 1,       # Phóng ra và nở vừa (3 ticks)
        2, 2, 2,       # Nở lớn với sóng khí nén (3 ticks)
        3, 3, 3, 3,    # NỞ CỰC ĐẠI KHỔNG LỒ lao vào mục tiêu (4 ticks)
        4, 4, 4, 4,    # NỔ TUNG va chạm (4 ticks)
        5, 5, 5, 5,    # Sóng nổ bung tỏa (4 ticks)
        6, 6, 6, 6,    # Khói bụi tan dần (4 ticks)
        7              # Biến mất (1 tick)
    ]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(910, im, data_bytes)

# ==============================================================================
# 2. TẠO EFFECT 911: URSUS SHOCK (ĐẠI HÙNG CHƯỞNG - 2 ĐỢT RƠI, ĐỢT 2 CÓ 3 TAY GẤU)
# ==============================================================================
def create_effect_911_ursus_shock():
    scale = 4
    w_sheet = 250 * scale
    h_sheet = 150 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))
    draw = ImageDraw.Draw(im)

    def draw_artistic_kuma_paw(draw, cx, cy, rx, ry, angle_deg=0, scale=4.0):
        rad = math.radians(angle_deg)
        trail_len = 20 * scale
        for off_r, a in [(-0.4, 90), (0, 200), (0.4, 90)]:
            x1 = cx + off_r * rx * math.cos(rad)
            y1 = cy - off_r * rx * math.sin(rad) - ry * math.cos(rad)
            x2 = x1 - math.sin(rad) * trail_len
            y2 = y1 - math.cos(rad) * trail_len
            draw.line([(x1, y1), (x2, y2)], fill=(255, 245, 210, a), width=int(2*scale))
        
        arm_w = int(rx * 1.3)
        arm_h = int(22 * scale)
        p1 = (cx - arm_w//2 * math.cos(rad), cy - arm_w//2 * math.sin(rad) - ry)
        p2 = (cx + arm_w//2 * math.cos(rad), cy + arm_w//2 * math.sin(rad) - ry)
        p3 = (p2[0] - math.sin(rad)*arm_h, p2[1] - math.cos(rad)*arm_h)
        p4 = (p1[0] - math.sin(rad)*arm_h, p1[1] - math.cos(rad)*arm_h)
        draw.polygon([p1, p2, p3, p4], fill=(30, 25, 35, 250), outline=(180, 175, 195, 220))

        for r_add, a in [(int(8*scale), 50), (int(4*scale), 110)]:
            draw.ellipse([cx - rx - r_add, cy - ry - r_add,
                          cx + rx + r_add, cy + ry + r_add],
                         fill=(255, 80, 160, a), outline=(255, 230, 200, a+50), width=max(1, int(scale)))

        draw.ellipse([cx - rx, cy - ry, cx + rx, cy + ry],
                     fill=(45, 38, 50, 255), outline=(255, 255, 255, 255), width=int(2*scale))

        prx = int(rx * 0.72)
        pry = int(ry * 0.68)
        draw.ellipse([cx - prx, cy - pry + int(scale), cx + prx, cy + pry + int(scale)],
                     fill=(255, 105, 180, 255), outline=(255, 245, 250, 255), width=max(1, int(1.2*scale)))
        draw.ellipse([cx - int(prx*0.5), cy - int(pry*0.4), cx + int(prx*0.5), cy + int(pry*0.25)],
                     fill=(255, 190, 220, 255))
        draw.ellipse([cx - int(prx*0.25), cy - int(pry*0.35), cx + int(prx*0.25), cy],
                     fill=(255, 255, 255, 255))

        toes = [
            (-0.72, 0.72, 0.25, -20),
            (-0.25, 0.95, 0.28,  -5),
            ( 0.25, 0.95, 0.28,   5),
            ( 0.72, 0.72, 0.25,  20)
        ]
        for bx, by, br, ang in toes:
            tx = cx + bx * rx * math.cos(rad) - by * ry * math.sin(rad)
            ty = cy + bx * rx * math.sin(rad) + by * ry * math.cos(rad)
            tr = br * rx
            claw_len = int(6 * scale)
            rad_c = math.radians(ang + angle_deg)
            claw_tip = (tx + math.sin(rad_c)*claw_len, ty + math.cos(rad_c)*claw_len + tr)
            claw_poly = [
                (tx - tr*0.7, ty + tr*0.4),
                claw_tip,
                (tx + tr*0.7, ty + tr*0.4)
            ]
            draw.polygon(claw_poly, fill=(255, 255, 255, 255), outline=(220, 200, 180, 255))
            draw.ellipse([tx - tr, ty - tr, tx + tr, ty + tr],
                         fill=(255, 105, 180, 255), outline=(255, 255, 255, 250), width=max(1, int(scale)))
            draw.ellipse([tx - tr*0.4, ty - tr*0.4, tx + tr*0.4, ty + tr*0.4],
                         fill=(255, 255, 255, 255))

    # --- ROW 1 (y = 0) ---
    # Sprite 0: Đại bàn tay giữa (w=54, h=64) -> x=0, y=0
    draw_artistic_kuma_paw(draw, 27*scale, 38*scale, rx=17*scale, ry=13*scale, angle_deg=0, scale=scale)

    # Sprite 1: Bàn tay nghiêng trái (w=44, h=60) -> x=56, y=0
    draw_artistic_kuma_paw(draw, (56+22)*scale, 36*scale, rx=13*scale, ry=11*scale, angle_deg=-15, scale=scale)

    # Sprite 2: Bàn tay nghiêng phải (w=44, h=60) -> x=102, y=0
    draw_artistic_kuma_paw(draw, (102+22)*scale, 36*scale, rx=13*scale, ry=11*scale, angle_deg=15, scale=scale)

    # Sprite 3: Bàn tay dậm chạm đất (w=56, h=36) -> x=148, y=0
    cx3, cy3 = (148 + 28)*scale, 18*scale
    draw.ellipse([cx3 - 25*scale, cy3 - 13*scale, cx3 + 25*scale, cy3 + 13*scale],
                 fill=(50, 40, 55, 255), outline=(255, 255, 255, 255), width=int(2*scale))
    draw.ellipse([cx3 - 18*scale, cy3 - 9*scale, cx3 + 18*scale, cy3 + 9*scale],
                 fill=(255, 105, 180, 255), outline=(255, 240, 245, 255), width=int(1.5*scale))
    draw.ellipse([cx3 - 9*scale, cy3 - 5*scale, cx3 + 9*scale, cy3 + 5*scale],
                 fill=(255, 255, 255, 255))
    for tx, ty in [(-18, 6), (-7, 10), (7, 10), (18, 6)]:
        draw.ellipse([cx3 + tx*scale - 4*scale, cy3 + ty*scale - 4*scale,
                      cx3 + tx*scale + 4*scale, cy3 + ty*scale + 4*scale],
                     fill=(255, 105, 180, 255), outline=(255, 255, 255, 255))

    # Sprite 10: Vòng nén dự báo tiếp đất (w=36, h=16) -> x=206, y=0
    cx10, cy10 = (206 + 18)*scale, 8*scale
    draw.ellipse([cx10 - 16*scale, cy10 - 6*scale, cx10 + 16*scale, cy10 + 6*scale],
                 outline=(255, 220, 120, 200), width=int(1.5*scale))
    draw.ellipse([cx10 - 10*scale, cy10 - 4*scale, cx10 + 10*scale, cy10 + 4*scale],
                 outline=(255, 105, 180, 160), width=max(1, int(scale)))

    # --- ROW 2 (y = 66) ---
    # Sprite 4: Chớp sáng nổ va chạm (w=64, h=44) -> x=0, y=66
    cx4, cy4 = 32*scale, (66 + 22)*scale
    star_rays = [
        (0, -20), (5, -8), (24, -4), (8, 3), (18, 18), (2, 9), (-6, 20), (-8, 6), (-24, 8), (-10, -3), (-18, -16), (-4, -8)
    ]
    pts = [(cx4 + x*scale, cy4 + y*scale) for x, y in star_rays]
    draw.polygon(pts, fill=(255, 220, 80, 240), outline=(255, 255, 255, 255))
    draw.ellipse([cx4 - 10*scale, cy4 - 7*scale, cx4 + 10*scale, cy4 + 7*scale], fill=(255, 255, 255, 255))
    for ang in [25, 70, 115, 160, 205, 250, 295, 340]:
        rad = math.radians(ang)
        x1 = cx4 + math.cos(rad) * 12 * scale
        y1 = cy4 + math.sin(rad) * 8 * scale
        x2 = cx4 + math.cos(rad) * 26 * scale
        y2 = cy4 + math.sin(rad) * 18 * scale
        draw.line([(x1, y1), (x2, y2)], fill=(255, 255, 255, 255), width=int(2*scale))

    # Sprite 5: Đại Sóng Xung Kích Ursus Shock (w=90, h=38) -> x=66, y=66
    cx5, cy5 = (66 + 45)*scale, (66 + 19)*scale
    for rx, ry, col, w in [(43*scale, 17*scale, (255, 200, 220, 120), 2),
                           (35*scale, 13*scale, (255, 235, 150, 180), 2),
                           (25*scale, 9*scale, (255, 255, 255, 255), 3)]:
        draw.ellipse([cx5 - rx, cy5 - ry, cx5 + rx, cy5 + ry], outline=col, width=int(w*scale/2))

    # Sprite 6: Dấu ấn 3 bàn chân gấu in sâu xuống đất đá nứt toác (w=76, h=30) -> x=158, y=66
    cx6, cy6 = (158 + 38)*scale, (66 + 15)*scale
    draw.ellipse([cx6 - 36*scale, cy6 - 13*scale, cx6 + 36*scale, cy6 + 13*scale],
                 fill=(45, 25, 20, 220), outline=(130, 80, 50, 255), width=int(2*scale))
    cracks = [
        [(-35, 0), (-42, -5), (-48, -4)],
        [(35, 0), (42, 6), (48, 7)],
        [(-24, 11), (-30, 16), (-36, 19)],
        [(24, 11), (28, 17), (35, 19)],
        [(0, 12), (2, 18), (0, 24)]
    ]
    for cr in cracks:
        pts_c = [(cx6 + x*scale, cy6 + y*scale) for x, y in cr]
        draw.line(pts_c, fill=(255, 140, 100, 220), width=int(1.5*scale))
    for pox, poy, pr in [(0, 0, 8), (-20, 1, 6), (20, 1, 6)]:
        pcx = cx6 + pox * scale
        pcy = cy6 + poy * scale
        draw.ellipse([pcx - pr*scale, pcy - int(pr*0.6*scale), pcx + pr*scale, pcy + int(pr*0.6*scale)],
                     fill=(255, 105, 180, 240), outline=(255, 255, 255, 255), width=max(1, int(scale)))

    # --- ROW 3 (y = 112) ---
    # Sprite 7: Khói Bụi Cuộn Trào Bên Trái (w=38, h=32) -> x=0, y=112
    cx7, cy7 = 19*scale, (112 + 16)*scale
    for dx_c, dy_c, rc, col in [
        (-8, 2, 9, (180, 170, 170, 180)),
        (4, -4, 8, (210, 200, 200, 200)),
        (0, 4, 7, (235, 230, 230, 220)),
        (-4, -2, 6, (250, 245, 245, 240))
    ]:
        draw.ellipse([(cx7 + dx_c*scale - rc*scale), (cy7 + dy_c*scale - rc*scale),
                      (cx7 + dx_c*scale + rc*scale), (cy7 + dy_c*scale + rc*scale)],
                     fill=col)

    # Sprite 8: Khói Bụi Cuộn Trào Bên Phải (w=38, h=32) -> x=40, y=112
    cx8, cy8 = (40 + 19)*scale, (112 + 16)*scale
    for dx_c, dy_c, rc, col in [
        (8, 2, 9, (180, 170, 170, 180)),
        (-4, -4, 8, (210, 200, 200, 200)),
        (0, 4, 7, (235, 230, 230, 220)),
        (4, -2, 6, (250, 245, 245, 240))
    ]:
        draw.ellipse([(cx8 + dx_c*scale - rc*scale), (cy8 + dy_c*scale - rc*scale),
                      (cx8 + dx_c*scale + rc*scale), (cy8 + dy_c*scale + rc*scale)],
                     fill=col)

    # Sprite 9: Mảnh Đá Văng & Tia Sét (w=36, h=34) -> x=80, y=112
    cx9, cy9 = (80 + 18)*scale, (112 + 17)*scale
    rocks = [
        [(-10, -8), (-6, -14), (-2, -10), (-7, -6)],
        [(6, -10), (12, -15), (14, -8), (9, -5)],
        [(-12, 6), (-8, 2), (-4, 8), (-9, 11)],
        [(8, 4), (13, 2), (16, 7), (10, 10)]
    ]
    for rk in rocks:
        pts_rk = [(cx9 + x*scale, cy9 + y*scale) for x, y in rk]
        draw.polygon(pts_rk, fill=(70, 50, 45, 240), outline=(200, 180, 160, 240))
    for r in [12*scale, 6*scale]:
        draw.line([(cx9 - r, cy9), (cx9 + r, cy9)], fill=(255, 220, 80, 240), width=int(2*scale))
        draw.line([(cx9, cy9 - r), (cx9, cy9 + r)], fill=(255, 220, 80, 240), width=int(2*scale))

    small_imgs = [
        (0,   0,   0, 54, 64), # Sprite 0: Đại bàn tay giữa (w=54, h=64)
        (1,  56,   0, 44, 60), # Sprite 1: Bàn tay nghiêng trái (w=44, h=60)
        (2, 102,   0, 44, 60), # Sprite 2: Bàn tay nghiêng phải (w=44, h=60)
        (3, 148,   0, 56, 36), # Sprite 3: Bàn tay dậm chạm đất (w=56, h=36)
        (4,   0,  66, 64, 44), # Sprite 4: Chớp sáng nổ va chạm (w=64, h=44)
        (5,  66,  66, 90, 38), # Sprite 5: Đại Sóng Xung Kích Ursus Shock (w=90, h=38)
        (6, 158,  66, 76, 30), # Sprite 6: Dấu ấn 3 bàn chân in đất (w=76, h=30)
        (7,   0, 112, 38, 32), # Sprite 7: Khói Bụi Trái (w=38, h=32)
        (8,  40, 112, 38, 32), # Sprite 8: Khói Bụi Phải (w=38, h=32)
        (9,  80, 112, 36, 34), # Sprite 9: Mảnh Đá & Tia Sét (w=36, h=34)
        (10, 206,  0, 36, 16), # Sprite 10: Vòng nén dự báo đất (w=36, h=16)
    ]

    # HOẠT ẢNH: 2 ĐỢT RƠI (ĐỢT 1: 2 TAY DẬM MỞ MÀN -> ĐỢT 2: 3 TAY CÙNG 1 LÚC DẬM SẬP ĐẤT!)
    frames = [
        # --- ĐỢT 1: 2 BÀN TAY GẤU RƠI MỞ MÀN ---
        # Frame 0: 2 Bàn tay xuất hiện trên đỉnh trời (dy = -160)
        [
            (-46, -10, 10, 0, 0), # Vòng dự báo trái
            ( 10, -10, 10, 0, 0), # Vòng dự báo phải
            (-44, -160, 1, 0, 1), # Bàn tay gấu trái
            ( 10, -160, 2, 0, 1), # Bàn tay gấu phải
        ],
        # Frame 1: 2 Bàn tay lao nhanh xuống lưng chừng (dy = -85)
        [
            (-46, -10, 10, 0, 0), # Vòng dự báo
            ( 10, -10, 10, 0, 0),
            (-44, -85, 1, 0, 1),
            ( 10, -85, 2, 0, 1),
        ],
        # Frame 2: DẬM ĐẤT ĐỢT 1 - FIRST IMPACT!
        [
            (-48, -16, 3, 0, 1), # Bàn tay dậm trái
            (  8, -16, 3, 0, 1), # Bàn tay dậm phải
            (-52, -22, 4, 0, 1), # Chớp nổ trái
            (  6, -22, 4, 0, 1), # Chớp nổ phải
            (-18, -25, 9, 0, 1), # Mảnh đá văng đợt 1
        ],

        # --- ĐỢT 2: 3 BÀN TAY GẤU KHỔNG LỒ CÙNG 1 LÚC GIÁNG LÂM TỐI THƯỢNG ---
        # Frame 3: 3 BÀN TAY XUẤT HIỆN TRÊN ĐỈNH BẦU TRỜI!
        [
            (-45, -12, 5, 0, 0),  # Vòng năng lượng nén cực đại dưới đất (onTop=0)
            (-27, -175, 0, 0, 1), # Bàn tay giữa (CỰC ĐẠI)
            (-65, -155, 1, 0, 1), # Bàn tay trái
            ( 22, -155, 2, 0, 1), # Bàn tay phải
        ],
        # Frame 4: 3 Bàn tay đồng loạt lao xé gió xuống lưng chừng
        [
            (-45, -12, 5, 0, 0),
            (-27, -110, 0, 0, 1), # Bàn tay giữa
            (-65,  -95, 1, 0, 1), # Bàn tay trái
            ( 22,  -95, 2, 0, 1), # Bàn tay phải
            (-18,  -25, 9, 0, 1), # Tia sét chấn động
        ],
        # Frame 5: Gia tốc cực hạn - 3 Bàn tay áp sát mặt đất!
        [
            (-27, -40, 0, 0, 1), # Bàn tay giữa sát đất
            (-65, -30, 1, 0, 1), # Bàn tay trái sát đất
            ( 22, -30, 2, 0, 1), # Bàn tay phải sát đất
            (-18, -25, 9, 0, 1), # Tia sét
        ],
        # Frame 6: 3 TAY GẤU ĐỒNG LOẠT DẬM SẬP MẶT ĐẤT - CỰC ĐẠI IMPACT!
        [
            (-28, -16, 3, 0, 1), # Dậm đất giữa
            (-62, -16, 3, 0, 1), # Dậm đất trái
            ( 18, -16, 3, 0, 1), # Dậm đất phải
            (-32, -22, 4, 0, 1), # Cực đại chớp nổ va chạm
            (-18, -28, 9, 0, 1), # Mảnh đá văng tứ tung
        ],
        # Frame 7: ĐẠI SÓNG XUNG KÍCH URSUS SHOCK & DẤU ẤN 3 BÀN CHÂN IN ĐẤT
        [
            (-38, -12, 6, 0, 0), # Dấu ấn 3 bàn chân gấu in lún nứt đất (onTop=0)
            (-45, -18, 5, 0, 1), # Đại sóng xung kích khổng lồ (onTop=1)
            (-52, -20, 7, 0, 0), # Khói bụi cuộn tung trái
            ( 14, -20, 8, 0, 0), # Khói bụi cuộn tung phải
            (-18, -35, 9, 0, 1), # Mảnh đá văng lên cao
        ],
        # Frame 8: SÓNG CHẤN QUÉT TOÀN BỘ CHIẾN TRƯỜNG & KHÓI BỤI BỐC CAO
        [
            (-38, -12, 6, 0, 0), # Dấu ấn 3 bàn chân gấu phát sáng dưới đất
            (-45, -18, 5, 0, 1), # Đại sóng xung kích
            (-56, -26, 7, 0, 0), # Khói bụi bốc lên cao trái
            ( 18, -26, 8, 0, 0), # Khói bụi bốc lên cao phải
        ],
        # Frame 9: Khói bụi cuộn cao và tàn dư chấn động mờ dần
        [
            (-38, -12, 6, 0, 0), # Dấu ấn 3 bàn chân mờ dần
            (-50, -24, 7, 0, 0), # Khói bụi mờ dần trái
            ( 12, -24, 8, 0, 0), # Khói bụi mờ dần phải
        ],
        # Frame 10: Biến mất hoàn toàn
        []
    ]

    # SEQUENCE: KÉO DÀI, CHẬM RÃI, MÃN NHÃN 2 ĐỢT RƠI (38 ticks ~ 1.3 giây)
    seq = [
        # Đợt 1: 2 tay rơi dậm mở màn (9 ticks)
        0, 0, 0,
        1, 1, 1,
        2, 2, 2,
        # Đợt 2: 3 TAY GẤU CÙNG 1 LÚC RƠI DẬM MẶT ĐẤT (29 ticks)
        3, 3, 3, 3, 3,   # 3 Tay xuất hiện trên đỉnh trời (5 ticks - nhìn rõ mồn một 3 tay gấu!)
        4, 4, 4,         # 3 Tay lao xuống lưng chừng (3 ticks)
        5, 5, 5,         # 3 Tay áp sát đất (3 ticks)
        6, 6, 6, 6,      # 3 TAY CÙNG DẬM SẬP ĐẤT (4 ticks)
        7, 7, 7, 7, 7,   # Đại sóng xung kích & dấu ấn 3 bàn chân in đất (5 ticks)
        8, 8, 8, 8,      # Sóng quét cực đại & khói bụi (4 ticks)
        9, 9, 9, 9,      # Khói bụi bốc cao tan dần (4 ticks)
        10               # Biến mất hoàn toàn (1 tick)
    ]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(911, im, data_bytes)

# ==============================================================================
# 3. TẠO EFFECT 912: NIKYU DEFENSE & REPEL PAIN (ĐỆM THỊT HỘ THỂ)
# ==============================================================================
def create_effect_912_nikyu_defense():
    scale = 4
    w_sheet = 240 * scale
    h_sheet = 140 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))
    draw = ImageDraw.Draw(im)

    # --- Sprite 0: Khiên đệm thịt cơ bản (nhịp 0) ---
    s0_cx = 28 * scale
    s0_cy = 30 * scale
    for r, alpha in [(27*scale, 40), (24*scale, 70), (20*scale, 100)]:
        draw.ellipse([s0_cx - r, s0_cy - r, s0_cx + r, s0_cy + r], 
                     fill=(255, 120, 180, alpha), outline=(255, 255, 255, alpha + 60), width=2*scale)
    draw_paw_pad(draw, s0_cx, s0_cy, scale=1.0*scale, 
                 color_main=(255, 140, 190, 220), 
                 color_highlight=(255, 255, 255, 255), 
                 color_outline=(255, 255, 255, 240))
    draw_sparkle(draw, s0_cx - 16*scale, s0_cy - 16*scale, r=5*scale, color=(255, 255, 255, 240))
    draw_sparkle(draw, s0_cx + 16*scale, s0_cy + 16*scale, r=5*scale, color=(255, 255, 255, 240))

    # --- Sprite 1: Khiên đệm thịt nở nhẹ & sóng phản lực (nhịp 1) ---
    s1_x0 = 60 * scale
    s1_cx = s1_x0 + 28 * scale
    s1_cy = 30 * scale
    for r, alpha in [(28*scale, 50), (25*scale, 90), (21*scale, 130)]:
        draw.ellipse([s1_cx - r, s1_cy - r, s1_cx + r, s1_cy + r], 
                     fill=(255, 130, 190, alpha), outline=(255, 230, 150, alpha + 70), width=2*scale)
    draw.ellipse([s1_cx - 26*scale, s1_cy - 26*scale, s1_cx + 26*scale, s1_cy + 26*scale], 
                 outline=(255, 215, 0, 220), width=2*scale)
    draw_paw_pad(draw, s1_cx, s1_cy, scale=1.1*scale, 
                 color_main=(255, 255, 255, 240), 
                 color_highlight=(255, 215, 0, 255), 
                 color_outline=(255, 105, 180, 240))
    draw_sparkle(draw, s1_cx + 17*scale, s1_cy - 17*scale, r=6*scale, color=(255, 255, 255, 250))
    draw_sparkle(draw, s1_cx - 17*scale, s1_cy + 17*scale, r=6*scale, color=(255, 255, 255, 250))

    # --- Sprite 2: Khiên đệm thịt cực đại năng lượng (nhịp 2) ---
    s2_x0 = 120 * scale
    s2_cx = s2_x0 + 28 * scale
    s2_cy = 30 * scale
    for r, alpha in [(27*scale, 60), (24*scale, 110), (19*scale, 150)]:
        draw.ellipse([s2_cx - r, s2_cy - r, s2_cx + r, s2_cy + r], 
                     fill=(255, 105, 180, alpha), outline=(255, 255, 255, 240), width=2*scale)
    draw.ellipse([s2_cx - 27*scale, s2_cy - 27*scale, s2_cx + 27*scale, s2_cy + 27*scale], 
                 outline=(255, 255, 255, 250), width=3*scale)
    draw_paw_pad(draw, s2_cx, s2_cy, scale=1.15*scale, 
                 color_main=(255, 182, 193, 240), 
                 color_highlight=(255, 255, 255, 255), 
                 color_outline=(255, 255, 255, 255))
    draw_sparkle(draw, s2_cx, s2_cy - 22*scale, r=6*scale, color=(255, 255, 255, 255))
    draw_sparkle(draw, s2_cx - 20*scale, s2_cy, r=6*scale, color=(255, 255, 255, 255))
    draw_sparkle(draw, s2_cx + 20*scale, s2_cy, r=6*scale, color=(255, 255, 255, 255))

    # --- Sprite 3: Khiên đệm thịt êm dịu thu hồi (nhịp 3) ---
    s3_x0 = 180 * scale
    s3_cx = s3_x0 + 28 * scale
    s3_cy = 30 * scale
    for r, alpha in [(26*scale, 40), (22*scale, 80), (18*scale, 120)]:
        draw.ellipse([s3_cx - r, s3_cy - r, s3_cx + r, s3_cy + r], 
                     fill=(255, 140, 190, alpha), outline=(255, 255, 255, alpha + 50), width=2*scale)
    draw_paw_pad(draw, s3_cx, s3_cy, scale=0.95*scale, 
                 color_main=(255, 130, 180, 230), 
                 color_highlight=(255, 255, 255, 255), 
                 color_outline=(255, 255, 255, 220))
    draw_sparkle(draw, s3_cx + 14*scale, s3_cy - 14*scale, r=5*scale, color=(255, 255, 255, 240))
    draw_sparkle(draw, s3_cx - 14*scale, s3_cy - 14*scale, r=5*scale, color=(255, 255, 255, 240))

    # --- Sprite 4: Vòng chân trạng thái 1 ---
    s4_y0 = 65 * scale
    s4_cx = 22 * scale
    s4_cy = s4_y0 + 7 * scale
    for rx, ry, a in [(21*scale, 6*scale, 90), (17*scale, 5*scale, 160), (13*scale, 4*scale, 220)]:
        draw.ellipse([s4_cx - rx, s4_cy - ry, s4_cx + rx, s4_cy + ry], 
                     outline=(255, 120, 180, a), width=max(1, int(1.5*scale)))
    draw_paw_pad(draw, s4_cx, s4_cy, scale=0.45*scale, 
                 color_main=(255, 105, 180, 200), 
                 color_highlight=(255, 255, 255, 255), 
                 color_outline=(255, 215, 0, 220))

    # --- Sprite 5: Vòng chân trạng thái 2 (sáng hơn / viền vàng) ---
    s5_x0 = 50 * scale
    s5_y0 = 65 * scale
    s5_cx = s5_x0 + 22 * scale
    s5_cy = s5_y0 + 7 * scale
    for rx, ry, a in [(21*scale, 6*scale, 120), (17*scale, 5*scale, 190), (13*scale, 4*scale, 250)]:
        draw.ellipse([s5_cx - rx, s5_cy - ry, s5_cx + rx, s5_cy + ry], 
                     outline=(255, 215, 0, a), width=max(1, int(1.5*scale)))
    draw_paw_pad(draw, s5_cx, s5_cy, scale=0.5*scale, 
                 color_main=(255, 255, 255, 220), 
                 color_highlight=(255, 215, 0, 255), 
                 color_outline=(255, 105, 180, 240))

    small_imgs = [
        (0,   0,  0, 56, 60), # Sprite 0: Khiên thân nhịp 0
        (1,  60,  0, 56, 60), # Sprite 1: Khiên thân nhịp 1
        (2, 120,  0, 56, 60), # Sprite 2: Khiên thân nhịp 2
        (3, 180,  0, 56, 60), # Sprite 3: Khiên thân nhịp 3
        (4,   0, 65, 44, 14), # Sprite 4: Vòng chân nhịp 0
        (5,  50, 65, 44, 14), # Sprite 5: Vòng chân nhịp 1
    ]

    # CỐ ĐỊNH TỌA ĐỘ 100%, KHÔNG NHẢY LÊN XUỐNG:
    # Vòng chân: dx=-22, dy=-7 (sát mặt đất chân)
    # Khiên thân: dx=-28, dy=-57 (trung tâm ngực, bao trọn thân)
    frames = [
        [
            (-22, -7,  4, 0, 0),
            (-28, -57, 0, 0, 1),
        ],
        [
            (-22, -7,  5, 0, 0),
            (-28, -57, 1, 0, 1),
        ],
        [
            (-22, -7,  4, 0, 0),
            (-28, -57, 2, 0, 1),
        ],
        [
            (-22, -7,  5, 0, 0),
            (-28, -57, 3, 0, 1),
        ]
    ]

    seq = [0, 0, 1, 1, 2, 2, 3, 3, 2, 2, 1, 1]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(912, im, data_bytes)

# ==============================================================================
# 4. TẠO ICONS CHO TRÁI ÁC QUỶ & 3 SKILL (ID 4416..4419)
# ==============================================================================
def create_all_icons():
    base_icon_dir = 'htth_project/data/icon'
    
    # Xóa các file icon cũ 28342..28345 nếu còn tồn tại
    for old_id in [28342, 28343, 28344, 28345]:
        for z in ['x0', 'x1', 'x2', 'x3', 'x4']:
            old_p = os.path.join(base_icon_dir, z, f'{old_id}.png')
            if os.path.exists(old_p):
                os.remove(old_p)
    
    # Icon 1: Trái Nikyu Nikyu (Item ID 1015, Icon ID 4416)
    im_fruit = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d_f = ImageDraw.Draw(im_fruit)
    d_f.ellipse([14, 20, 82, 86], fill=(255, 112, 162), outline=(194, 24, 91), width=3)
    d_f.arc([36, 6, 60, 26], start=180, end=360, fill=(76, 175, 80), width=4)
    d_f.line([(48, 16), (48, 24)], fill=(56, 142, 60), width=4)
    draw_paw_pad(d_f, 48, 56, scale=1.4, 
                 color_main=(255, 240, 245), 
                 color_highlight=(255, 255, 255), 
                 color_outline=(233, 30, 99))
    d_f.arc([22, 32, 38, 48], start=45, end=270, fill=(233, 30, 99), width=2)
    d_f.arc([58, 32, 74, 48], start=270, end=135, fill=(233, 30, 99), width=2)
    
    # Icon 2: Skill 1 - Áp Lực Pháo (Pad Ho) - Icon ID 4417
    im_sk1 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d1 = ImageDraw.Draw(im_sk1)
    d1.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(33, 33, 33, 240), outline=(255, 64, 129), width=3)
    for dx_t, a in [(-16, 80), (-8, 150), (0, 220)]:
        d1.ellipse([24+dx_t, 24, 72+dx_t, 72], outline=(224, 247, 250, a), width=2)
    draw_paw_pad(d1, 48, 48, scale=1.5, 
                 color_main=(255, 64, 129), 
                 color_highlight=(255, 255, 255), 
                 color_outline=(255, 255, 255))
    draw_sparkle(d1, 72, 48, r=10, color=(255, 255, 255))

    # Icon 3: Skill 2 - Đại Hùng Chưởng (Ursus Shock) - Icon ID 4418
    im_sk2 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d2 = ImageDraw.Draw(im_sk2)
    d2.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(20, 0, 30, 240), outline=(255, 215, 0), width=3)
    for r, col in [(38, (255, 64, 129, 120)), (28, (255, 215, 0, 180)), (18, (255, 255, 255, 230))]:
        d2.ellipse([48-r, 48-r, 48+r, 48+r], outline=col, width=3)
    draw_paw_pad(d2, 48, 48, scale=1.3, 
                 color_main=(255, 255, 255), 
                 color_highlight=(255, 215, 0), 
                 color_outline=(255, 20, 147))
    for ang in range(0, 360, 45):
        rad = math.radians(ang)
        d2.line([(48, 48), (48 + math.cos(rad)*38, 48 + math.sin(rad)*38)], fill=(255, 255, 255, 200), width=2)

    # Icon 4: Skill 3 - Đệm Thịt Hộ Thể / Đẩy Đau Đớn - Icon ID 4419
    im_sk3 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d3 = ImageDraw.Draw(im_sk3)
    d3.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(10, 25, 40, 240), outline=(76, 175, 80), width=3)
    d3.ellipse([12, 12, 84, 84], outline=(255, 215, 0, 200), width=3)
    draw_paw_pad(d3, 40, 52, scale=1.2, 
                 color_main=(255, 105, 180), 
                 color_highlight=(255, 255, 255), 
                 color_outline=(255, 255, 255))
    d3.ellipse([54, 14, 84, 44], fill=(255, 23, 68, 220), outline=(255, 255, 255, 255), width=2)
    draw_paw_pad(d3, 69, 29, scale=0.45, color_main=(255, 255, 255))
    draw_sparkle(d3, 24, 24, r=8, color=(255, 215, 0))

    # Icon 5: Skill 4 - Phản Chấn Đệm Thịt (Nội tại) - Icon ID 4420
    im_sk4 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d4 = ImageDraw.Draw(im_sk4)
    d4.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(18, 12, 38, 245), outline=(0, 229, 255), width=3)
    for r, col, w in [(40, (0, 229, 255, 100), 2), (34, (255, 215, 0, 160), 2), (28, (255, 255, 255, 220), 2)]:
        d4.ellipse([48 - r, 48 - r, 48 + r, 48 + r], outline=col, width=w)
    for dx, dy in [(-26, -26), (26, -26), (-26, 26), (26, 26)]:
        d4.line([(48 + int(dx*0.6), 48 + int(dy*0.6)), (48 + dx, 48 + dy)], fill=(255, 215, 0, 230), width=2)
    draw_paw_pad(d4, 48, 48, scale=1.35, color_main=(255, 245, 250), color_highlight=(255, 255, 255), color_outline=(255, 105, 180))
    draw_sparkle(d4, 22, 22, r=7, color=(0, 229, 255))
    draw_sparkle(d4, 74, 22, r=6, color=(255, 215, 0))
    draw_sparkle(d4, 74, 74, r=7, color=(0, 229, 255))
    draw_sparkle(d4, 22, 74, r=6, color=(255, 215, 0))

    icons_to_save = {
        2190: im_fruit,
        4417: im_sk1,
        4418: im_sk2,
        4419: im_sk3,
        4420: im_sk4
    }
    
    for icon_id, im_x4 in icons_to_save.items():
        w4, h4 = im_x4.size
        w1, h1 = w4 // 4, h4 // 4
        zooms = {
            'x4': im_x4,
            'x3': im_x4.resize((w1 * 3, h1 * 3), Image.Resampling.BILINEAR),
            'x2': im_x4.resize((w1 * 2, h1 * 2), Image.Resampling.BILINEAR),
            'x1': im_x4.resize((w1 * 1, h1 * 1), Image.Resampling.NEAREST),
            'x0': im_x4.resize((w1 * 1, h1 * 1), Image.Resampling.NEAREST)
        }
        for z, im_z in zooms.items():
            dir_icon = os.path.join(base_icon_dir, z)
            os.makedirs(dir_icon, exist_ok=True)
            im_z.save(os.path.join(dir_icon, f'{icon_id}.png'), format='PNG', optimize=True)
        print(f"-> Đã tạo thành công Icon ID {icon_id} tại data/icon/ cho toàn bộ x0..x4")

if __name__ == '__main__':
    print("=== ĐANG TẠO SPRITE SHEETS & DATA EFFECT CHO TRÁI NIKYU NIKYU NO MI (ID 910..912) ===")
    create_effect_910_pad_ho()
    create_effect_911_ursus_shock()
    create_effect_912_nikyu_defense()
    print("=== ĐANG TẠO ICONS TRÁI ÁC QUỶ & KỸ NĂNG VỊ TRÍ MỚI (ID 4416..4419) ===")
    create_all_icons()
    print("=== HOÀN TẤT TOÀN BỘ TÀI NGUYÊN HÌNH ẢNH & HIỆU ỨNG! ===")
