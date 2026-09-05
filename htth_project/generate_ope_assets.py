#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Script tạo hình ảnh Sprite sheet và nhị phân TeaMobi Effect Data cho 3 chiêu thức
Trái Ope Ope no Mi (Trafalgar D. Water Law) và bộ Icon kỹ năng.
- Effect 914: Room - Trảm Không Gian (Spatial Slash)
- Effect 915: Dao Phóng Xạ Gamma (Gamma Knife)
- Effect 916: Curtain - Khiên Phẫu Thuật & Tái Tạo Sinh Mệnh (Curtain & Scan)
- Icon ID 2191: Trái Ope Ope (Item 1016)
- Icon ID 4421: Skill 1 - Room Trảm Không Gian
- Icon ID 4422: Skill 2 - Dao Phóng Xạ Gamma
- Icon ID 4423: Skill 3 - Curtain Khiên Phẫu Thuật
- Icon ID 4424: Skill 4 - Bác Sĩ Tử Thần (Nội Tại)
"""

import os
import sys
import struct
import math
from PIL import Image, ImageDraw

sys.stdout.reconfigure(encoding='utf-8')

def draw_room_grid_circle(draw, cx, cy, rx, ry, color=(0, 229, 255, 200), width=2):
    """Vẽ vòng tròn Room neon lam ngọc kèm đường lưới không gian"""
    draw.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], outline=color, width=width)
    # Đường vĩ tuyến ngang
    draw.arc([cx - rx, cy - int(ry*0.5), cx + rx, cy + int(ry*0.5)], 
             start=0, end=360, fill=(color[0], color[1], color[2], max(40, color[3]-60)), width=max(1, width-1))
    # Đường kinh tuyến dọc
    draw.arc([cx - int(rx*0.5), cy - ry, cx + int(rx*0.5), cy + ry], 
             start=0, end=360, fill=(color[0], color[1], color[2], max(40, color[3]-60)), width=max(1, width-1))

def draw_sparkle(draw, cx, cy, r=6, color=(255, 255, 255, 240)):
    draw.line([(cx - r, cy), (cx + r, cy)], fill=color, width=2)
    draw.line([(cx, cy - r), (cx, cy + r)], fill=color, width=2)
    r2 = int(r * 0.6)
    draw.line([(cx - r2, cy - r2), (cx + r2, cy + r2)], fill=color, width=1)
    draw.line([(cx - r2, cy + r2), (cx + r2, cy - r2)], fill=color, width=1)

def draw_slash_blade(draw, x1, y1, x2, y2, color_core=(255, 255, 255, 255), color_glow=(0, 229, 255, 180), width=4):
    """Vẽ đường chém kiếm khí không gian phát sáng"""
    draw.line([(x1, y1), (x2, y2)], fill=color_glow, width=width + 3)
    draw.line([(x1, y1), (x2, y2)], fill=color_core, width=max(1, width))

def draw_lightning(draw, pts, color_glow=(0, 255, 200, 180), color_core=(255, 255, 255, 255), width=3):
    """Vẽ tia sét điện trường Gamma"""
    for i in range(len(pts) - 1):
        draw.line([pts[i], pts[i+1]], fill=color_glow, width=width + 2)
        draw.line([pts[i], pts[i+1]], fill=color_core, width=max(1, width))

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
        im_q = im.quantize(colors=128, method=Image.Quantize.FASTOCTREE)
        im_q.save(img_path, format='PNG', optimize=True)
        
        data_path = os.path.normpath(os.path.join(dir_data, f'{eff_id}'))
        with open(data_path, 'wb') as f:
            f.write(data_bytes)
    print(f"-> Đã tạo thành công Effect ID {eff_id} cho toàn bộ các zoom x0..x4 (tối ưu dung lượng)")

# ==============================================================================
# 1. TẠO EFFECT 914: ROOM - TRẢM KHÔNG GIAN (SPATIAL SLASH)
# ==============================================================================
def create_effect_914_room_slash():
    scale = 4
    w_sheet = 240 * scale
    h_sheet = 130 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))
    draw = ImageDraw.Draw(im)

    # Row 1 (y = 0)
    # Sprite 0: Mini Room Ball (Tụ cầu Room trong lòng bàn tay) (w=24, h=24) -> x=0, y=0
    cx0, cy0 = 12*scale, 12*scale
    for r, a in [(10*scale, 80), (7*scale, 160), (4*scale, 240)]:
        draw.ellipse([cx0 - r, cy0 - r, cx0 + r, cy0 + r], fill=(0, 229, 255, a))
    draw.ellipse([cx0 - 3*scale, cy0 - 3*scale, cx0 + 3*scale, cy0 + 3*scale], fill=(255, 255, 255, 255))
    draw_sparkle(draw, cx0, cy0, r=6*scale, color=(255, 255, 255, 240))

    # Sprite 1: Expanding Room Sphere (Vòm Room mở rộng) (w=50, h=46) -> x=30, y=0
    cx1, cy1 = (30 + 25)*scale, 23*scale
    for r, a in [(21*scale, 50), (17*scale, 100)]:
        draw.ellipse([cx1 - r, cy1 - r, cx1 + r, cy1 + r], fill=(0, 229, 255, a), outline=(128, 255, 255, a+80), width=int(1.5*scale))
    draw_room_grid_circle(draw, cx1, cy1, 19*scale, 19*scale, color=(0, 255, 255, 220), width=int(1.5*scale))
    draw.ellipse([cx1 - 5*scale, cy1 - 5*scale, cx1 + 5*scale, cy1 + 5*scale], fill=(255, 255, 255, 240))

    # Sprite 2: Giant Room Domain (Vùng Room Cực Đại) (w=74, h=60) -> x=85, y=0
    cx2, cy2 = (85 + 37)*scale, 30*scale
    for r, a in [(33*scale, 35), (28*scale, 70)]:
        draw.ellipse([cx2 - r, cy2 - r, cx2 + r, cy2 + r], fill=(0, 200, 255, a), outline=(0, 255, 255, 200), width=int(2*scale))
    draw_room_grid_circle(draw, cx2, cy2, 31*scale, 31*scale, color=(128, 255, 255, 230), width=int(2*scale))
    for ang in [30, 75, 120, 165, 210, 255, 300, 345]:
        rad = math.radians(ang)
        draw_sparkle(draw, cx2 + int(math.cos(rad)*27*scale), cy2 + int(math.sin(rad)*27*scale), r=4*scale, color=(255, 255, 255, 230))

    # Sprite 3: Spatial Slash Cross (Vết chém phân tách không gian chéo 1) (w=60, h=56) -> x=165, y=0
    cx3, cy3 = (165 + 30)*scale, 28*scale
    draw_slash_blade(draw, cx3 - 26*scale, cy3 - 22*scale, cx3 + 26*scale, cy3 + 22*scale, color_core=(255, 255, 255, 255), color_glow=(0, 229, 255, 220), width=int(3*scale))
    draw_slash_blade(draw, cx3 - 22*scale, cy3 + 20*scale, cx3 + 22*scale, cy3 - 20*scale, color_core=(255, 255, 255, 255), color_glow=(0, 255, 200, 200), width=int(2*scale))
    draw_sparkle(draw, cx3, cy3, r=8*scale, color=(255, 255, 255, 255))

    # Row 2 (y = 65)
    # Sprite 4: Massive Spatial Rift (Vết nứt không gian khổng lồ phân tách vật thể) (w=76, h=54) -> x=0, y=65
    cx4, cy4 = 38*scale, (65 + 27)*scale
    draw_slash_blade(draw, cx4 - 34*scale, cy4, cx4 + 34*scale, cy4, color_core=(255, 255, 255, 255), color_glow=(0, 229, 255, 240), width=int(4*scale))
    draw_slash_blade(draw, cx4 - 28*scale, cy4 - 18*scale, cx4 + 28*scale, cy4 + 18*scale, color_core=(255, 255, 255, 255), color_glow=(128, 255, 255, 200), width=int(3*scale))
    for dx_c, dy_c in [(-20, -10), (18, 12), (-14, 14), (22, -8)]:
        draw.rectangle([(cx4 + dx_c*scale - 4*scale), (cy4 + dy_c*scale - 4*scale),
                        (cx4 + dx_c*scale + 4*scale), (cy4 + dy_c*scale + 4*scale)], 
                       fill=(0, 229, 255, 180), outline=(255, 255, 255, 240), width=1)

    # Sprite 5: Spatial Implosion Shockwave (Sóng nổ phân rã không gian) (w=80, h=56) -> x=80, y=65
    cx5, cy5 = (80 + 40)*scale, (65 + 28)*scale
    for rx, ry, col, w in [(36*scale, 25*scale, (0, 229, 255, 120), 2),
                           (28*scale, 19*scale, (128, 255, 255, 180), 2),
                           (18*scale, 12*scale, (255, 255, 255, 240), 3)]:
        draw.ellipse([cx5 - rx, cy5 - ry, cx5 + rx, cy5 + ry], outline=col, width=int(w*scale/2))
    for ang in range(0, 360, 40):
        rad = math.radians(ang)
        x2 = cx5 + math.cos(rad) * 32 * scale
        y2 = cy5 + math.sin(rad) * 22 * scale
        draw.line([(cx5, cy5), (x2, y2)], fill=(255, 255, 255, 200), width=int(1.5*scale))

    # Sprite 6: Spatial Dissolve Sparks (Bụi năng lượng không gian tan biến) (w=40, h=40) -> x=165, y=65
    cx6, cy6 = (165 + 20)*scale, (65 + 20)*scale
    for dx_s, dy_s, r_s in [(-12, -10, 4), (10, -12, 5), (-8, 10, 4), (12, 8, 5), (0, 0, 6)]:
        draw_sparkle(draw, cx6 + dx_s*scale, cy6 + dy_s*scale, r=r_s*scale, color=(0, 255, 255, 230))

    small_imgs = [
        (0,   0,  0, 24, 24), # Sprite 0: Mini Room Ball
        (1,  30,  0, 50, 46), # Sprite 1: Expanding Room Sphere
        (2,  85,  0, 74, 60), # Sprite 2: Giant Room Domain
        (3, 165,  0, 60, 56), # Sprite 3: Spatial Slash Cross
        (4,   0, 65, 76, 54), # Sprite 4: Massive Spatial Rift
        (5,  80, 65, 80, 56), # Sprite 5: Spatial Implosion Shockwave
        (6, 165, 65, 40, 40), # Sprite 6: Spatial Dissolve Sparks
    ]

    # Hoạt ảnh thi triển Room Trảm Không Gian:
    # 1. Tụ Room trong tay -> Bung tỏa Room bao trùm chiến trường -> Tung đường chém xé toạc không gian -> Nổ phân tách
    frames = [
        # Frame 0: Tụ hạt Room nhỏ trước ngực
        [(-12, -26, 0, 0, 1)],
        # Frame 1: Room bung nở vừa
        [(-25, -35, 1, 0, 1)],
        # Frame 2: Room khổng lồ bao trùm không gian
        [(-37, -45, 2, 0, 1)],
        # Frame 3: Giữ Room khổng lồ + Vết chém chéo xé rách mục tiêu
        [
            (-37, -45, 2, 0, 1),
            ( 15, -35, 3, 0, 1),
        ],
        # Frame 4: Vết chém cực đại Massive Rift + Sóng nổ không gian
        [
            ( 10, -35, 4, 0, 1),
            ( 12, -36, 5, 0, 1),
        ],
        # Frame 5: Sóng nổ lan tỏa + Bụi năng lượng không gian
        [
            ( 12, -36, 5, 0, 1),
            ( 25, -30, 6, 0, 1),
        ],
        # Frame 6: Tan biến nhẹ
        [( 25, -30, 6, 0, 1)],
    ]

    seq = [0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(914, im, data_bytes)

# ==============================================================================
# 2. TẠO EFFECT 915: GAMMA KNIFE (DAO PHÓNG XẠ GAMMA)
# ==============================================================================
def create_effect_915_gamma_knife():
    scale = 4
    w_sheet = 240 * scale
    h_sheet = 130 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))
    draw = ImageDraw.Draw(im)

    # Row 1 (y = 0)
    # Sprite 0: Gamma Energy Dagger Mini (Dao năng lượng Gamma nhỏ tụ trên tay) (w=30, h=24) -> x=0, y=0
    cx0, cy0 = 15*scale, 12*scale
    # Lưỡi dao Plasma
    draw.polygon([(cx0 - 12*scale, cy0), (cx0 + 12*scale, cy0 - 4*scale), (cx0 + 4*scale, cy0 + 6*scale)], 
                 fill=(0, 255, 170, 220), outline=(255, 255, 255, 255))
    pts_l0 = [(cx0 - 10*scale, cy0), (cx0 - 2*scale, cy0 - 6*scale), (cx0 + 6*scale, cy0 + 4*scale), (cx0 + 12*scale, cy0 - 4*scale)]
    draw_lightning(draw, pts_l0, color_glow=(0, 229, 255, 200), color_core=(255, 255, 255, 255), width=int(1.5*scale))

    # Sprite 1: Gamma Plasma Blade Medium (Lưỡi dao năng lượng phóng lớn kèm sấm sét) (w=48, h=34) -> x=35, y=0
    cx1, cy1 = (35 + 24)*scale, 17*scale
    draw.polygon([(cx1 - 20*scale, cy1), (cx1 + 22*scale, cy1 - 7*scale), (cx1 + 6*scale, cy1 + 10*scale)], 
                 fill=(0, 255, 200, 240), outline=(255, 255, 255, 255))
    pts_l1 = [(cx1 - 18*scale, cy1), (cx1 - 6*scale, cy1 - 10*scale), (cx1 + 8*scale, cy1 + 8*scale), (cx1 + 22*scale, cy1 - 7*scale)]
    draw_lightning(draw, pts_l1, color_glow=(0, 255, 128, 220), color_core=(255, 255, 255, 255), width=int(2*scale))
    draw_sparkle(draw, cx1 + 22*scale, cy1 - 7*scale, r=7*scale, color=(255, 255, 255, 255))

    # Sprite 2: Giant Gamma Thrust Blade (Mũi dao Gamma cực đại đâm xuyên) (w=68, h=44) -> x=90, y=0
    cx2, cy2 = (90 + 34)*scale, 22*scale
    draw.polygon([(cx2 - 30*scale, cy2), (cx2 + 32*scale, cy2 - 10*scale), (cx2 + 10*scale, cy2 + 14*scale)], 
                 fill=(0, 255, 220, 250), outline=(255, 255, 255, 255))
    pts_l2 = [(cx2 - 28*scale, cy2), (cx2 - 10*scale, cy2 - 14*scale), (cx2 + 12*scale, cy2 + 12*scale), (cx2 + 32*scale, cy2 - 10*scale)]
    draw_lightning(draw, pts_l2, color_glow=(0, 229, 255, 240), color_core=(255, 255, 255, 255), width=int(3*scale))
    draw_sparkle(draw, cx2 + 32*scale, cy2 - 10*scale, r=9*scale, color=(255, 255, 255, 255))

    # Sprite 3: Internal Organ Gamma Lightning Burst (Điện trường Gamma giật xé nội tạng) (w=56, h=52) -> x=165, y=0
    cx3, cy3 = (165 + 28)*scale, 26*scale
    for ang in [0, 60, 120, 180, 240, 300]:
        rad = math.radians(ang)
        p1 = (cx3, cy3)
        p2 = (cx3 + int(math.cos(rad + 0.3)*14*scale), cy3 + int(math.sin(rad + 0.3)*14*scale))
        p3 = (cx3 + int(math.cos(rad)*24*scale), cy3 + int(math.sin(rad)*24*scale))
        draw_lightning(draw, [p1, p2, p3], color_glow=(0, 255, 180, 220), color_core=(255, 255, 255, 255), width=int(2*scale))
    draw.ellipse([cx3 - 8*scale, cy3 - 8*scale, cx3 + 8*scale, cy3 + 8*scale], fill=(255, 255, 255, 255), outline=(0, 255, 200, 240), width=int(1.5*scale))

    # Row 2 (y = 65)
    # Sprite 4: Gamma Cataclysmic Explosion (Vụ nổ xung kích Gamma cực đại) (w=78, h=56) -> x=0, y=65
    cx4, cy4 = 39*scale, (65 + 28)*scale
    star_pts = []
    for i in range(12):
        r_c = 34*scale if (i % 2 == 0) else 14*scale
        ang_c = i * 30
        rad_c = math.radians(ang_c)
        star_pts.append((cx4 + math.cos(rad_c)*r_c, cy4 + math.sin(rad_c)*r_c))
    draw.polygon(star_pts, fill=(0, 255, 200, 230), outline=(255, 255, 255, 255))
    draw.ellipse([cx4 - 14*scale, cy4 - 14*scale, cx4 + 14*scale, cy4 + 14*scale], fill=(255, 255, 255, 255))
    for ang in range(15, 360, 45):
        rad = math.radians(ang)
        x2 = cx4 + math.cos(rad) * 36 * scale
        y2 = cy4 + math.sin(rad) * 26 * scale
        draw.line([(cx4, cy4), (x2, y2)], fill=(255, 255, 255, 220), width=int(2*scale))

    # Sprite 5: Expanding Gamma Plasma Ring (Vòng sóng xung kích Plasma lan tỏa) (w=82, h=54) -> x=82, y=65
    cx5, cy5 = (82 + 41)*scale, (65 + 27)*scale
    for rx, ry, col, w in [(38*scale, 23*scale, (0, 255, 180, 130), 2),
                           (30*scale, 17*scale, (0, 229, 255, 180), 2),
                           (18*scale, 10*scale, (255, 255, 255, 240), 3)]:
        draw.ellipse([cx5 - rx, cy5 - ry, cx5 + rx, cy5 + ry], outline=col, width=int(w*scale/2))

    # Sprite 6: Dissolving Gamma Sparks (Hạt điện tích Gamma tan biến) (w=40, h=40) -> x=170, y=65
    cx6, cy6 = (170 + 20)*scale, (65 + 20)*scale
    for dx_g, dy_g, r_g in [(-10, -8, 5), (12, -10, 4), (-6, 12, 5), (10, 8, 4), (0, 0, 7)]:
        draw_sparkle(draw, cx6 + dx_g*scale, cy6 + dy_g*scale, r=r_g*scale, color=(0, 255, 200, 240))

    small_imgs = [
        (0,   0,  0, 30, 24), # Sprite 0: Mini Gamma Dagger
        (1,  35,  0, 48, 34), # Sprite 1: Medium Gamma Blade
        (2,  90,  0, 68, 44), # Sprite 2: Giant Gamma Thrust Blade
        (3, 165,  0, 56, 52), # Sprite 3: Internal Lightning Burst
        (4,   0, 65, 78, 56), # Sprite 4: Cataclysmic Explosion
        (5,  82, 65, 82, 54), # Sprite 5: Plasma Ring
        (6, 170, 65, 40, 40), # Sprite 6: Dissolving Sparks
    ]

    # Hoạt ảnh Gamma Knife:
    # Tụ dao Gamma -> Phóng lưỡi dao đâm xuyên mục tiêu -> Xung điện phá hủy nội tạng -> Nổ tung Plasma cực đại
    frames = [
        # Frame 0: Tụ dao Gamma trước ngực
        [(-15, -24, 0, 0, 1)],
        # Frame 1: Phóng dao vừa về phía trước
        [( 10, -26, 1, 0, 1)],
        # Frame 2: Lưỡi dao Gamma cực đại đâm thẳng mục tiêu
        [( 35, -28, 2, 0, 1)],
        # Frame 3: Đâm trúng! Điện trường Gamma giật xé nội tạng bên trong
        [
            ( 45, -28, 3, 0, 1),
            ( 40, -28, 5, 0, 1),
        ],
        # Frame 4: NỔ TUNG XUNG KÍCH GAMMA NỘI TẠNG CỰC ĐẠI
        [
            ( 45, -28, 4, 0, 1),
            ( 40, -28, 5, 0, 1),
        ],
        # Frame 5: Sóng nổ bung tỏa + Hạt điện tích tan biến
        [
            ( 40, -28, 5, 0, 1),
            ( 50, -25, 6, 0, 1),
        ],
        # Frame 6: Tan biến
        [( 50, -25, 6, 0, 1)],
    ]

    seq = [0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(915, im, data_bytes)

# ==============================================================================
# 3. TẠO EFFECT 916: ĐẠI TRẬN PHÁP KHỔNG LỒ DƯỚI CHÂN & QUẢ CẦU ROOM UY LỰC
# ==============================================================================
def draw_giant_ope_underfoot(angle_deg, w_frame=352, h_frame=176):
    """
    Vẽ đại trận pháp không gian Ope Ope khổng lồ dưới chân nhân vật:
    - Kích thước 1x: 88x44 (x4: 352x176) - Uy lực bao trùm mặt đất
    - Vòng hào quang đa tầng + Cổ tự định vị phẫu thuật Room
    - 4 Đại Lưỡi Kiếm Ma Thuật Xoáy Lốc (Spatial Scythes) phát sáng cực đại
    - Trái Tim Ope Thần Thánh & Trận Đồ Tử Thần ở trung tâm
    - Tia sét điện trường và các mảnh lập phương không gian phân tách
    """
    im = Image.new('RGBA', (w_frame, h_frame), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w_frame // 2, h_frame // 2
    rx_out, ry_out = 166, 80
    
    # 1. Đĩa năng lượng phát quang trên mặt đất (Ground Energy Discs)
    d.ellipse([cx - rx_out - 4, cy - ry_out - 4, cx + rx_out + 4, cy + ry_out + 4], fill=(0, 200, 255, 30))
    d.ellipse([cx - int(rx_out*0.88), cy - int(ry_out*0.88), cx + int(rx_out*0.88), cy + int(ry_out*0.88)], fill=(0, 240, 255, 45))
    d.ellipse([cx - int(rx_out*0.55), cy - int(ry_out*0.55), cx + int(rx_out*0.55), cy + int(ry_out*0.55)], fill=(255, 40, 110, 35))
    
    # 2. Vòng hào quang Room đa tầng phát sáng rực rỡ
    # Vòng ngoài cùng
    d.ellipse([cx - rx_out, cy - ry_out, cx + rx_out, cy + ry_out], outline=(0, 229, 255, 160), width=4)
    d.ellipse([cx - rx_out + 6, cy - ry_out + 3, cx + rx_out - 6, cy + ry_out - 3], outline=(0, 255, 255, 240), width=3)
    # Vòng ma thuật hồng ngọc / vàng kim
    d.ellipse([cx - int(rx_out*0.82), cy - int(ry_out*0.82), cx + int(rx_out*0.82), cy + int(ry_out*0.82)], outline=(255, 64, 129, 200), width=3)
    # Vòng nội vi ngọc bích
    d.ellipse([cx - int(rx_out*0.62), cy - int(ry_out*0.62), cx + int(rx_out*0.62), cy + int(ry_out*0.62)], outline=(0, 255, 200, 180), width=2)
    
    # 3. 36 Vạch cổ tự định vị không gian Room quanh viền
    for ang in range(0, 360, 10):
        rad = math.radians(ang + angle_deg * 0.4)
        is_major = (ang % 30 == 0)
        len_notch = 10 if is_major else 6
        p_in = (cx + math.cos(rad) * (rx_out - len_notch), cy + math.sin(rad) * (ry_out - int(len_notch * (ry_out / rx_out))))
        p_out = (cx + math.cos(rad) * rx_out, cy + math.sin(rad) * ry_out)
        col_notch = (0, 255, 255, 240) if is_major else (128, 255, 255, 170)
        d.line([p_in, p_out], fill=col_notch, width=3 if is_major else 2)
    
    # 4. 8 Biểu tượng Thập Tự / Kim Cương Tử Thần xoay trên viền ngoài
    for i in range(8):
        ang_star = math.radians(i * 45 + angle_deg * 0.6)
        star_x = cx + math.cos(ang_star) * (rx_out - 4)
        star_y = cy + math.sin(ang_star) * (ry_out - 2)
        # Vẽ kim cương phát sáng
        d.polygon([(star_x, star_y - 6), (star_x + 5, star_y), (star_x, star_y + 6), (star_x - 5, star_y)], 
                  fill=(255, 255, 255, 255), outline=(255, 215, 0, 255))
        d.ellipse([star_x - 7, star_y - 7, star_x + 7, star_y + 7], outline=(0, 255, 255, 190), width=1)
    
    # 5. 4 ĐẠI LƯỠI KIẾM MA THUẬT XOÁY LỐC KHÔNG GIAN (4 GIANT SPATIAL SCYTHES)
    rad_0 = math.radians(angle_deg)
    for i in range(4):
        arm_angle = rad_0 + i * (math.pi / 2)
        pts = []
        for step in range(28):
            t = step / 27.0
            r_cur = 20 + t * (rx_out - 24)
            ang_cur = arm_angle + t * 1.65
            px = cx + math.cos(ang_cur) * r_cur
            py = cy + math.sin(ang_cur) * (r_cur * (ry_out / rx_out))
            pts.append((px, py))
        
        # Vẽ các lớp hào quang kiếm khí xoáy cực đại
        for p in range(len(pts)-1):
            t_arm = p / len(pts)
            a = int(100 + 155 * t_arm)
            w_line = max(2, int(11 * (1.15 - t_arm * 0.65)))
            
            # Lớp 1: Hào quang lam ngọc rộng
            d.line([pts[p], pts[p+1]], fill=(0, 229, 255, a), width=w_line + 6)
            # Lớp 2: Lớp plasma ngọc bích
            d.line([pts[p], pts[p+1]], fill=(0, 255, 210, a), width=w_line + 3)
            # Lớp 3: Lưỡi cắt hồng ngọc rực rỡ
            d.line([pts[p], pts[p+1]], fill=(255, 40, 120, a), width=w_line)
            # Lớp 4: Lõi kiếm khí trắng tinh khiết phát sáng
            d.line([pts[p], pts[p+1]], fill=(255, 255, 255, a), width=max(1, w_line - 3))
            
            # Đỉnh mũi kiếm bùng nổ ngôi sao năng lượng cực lớn
            if p == len(pts)-2:
                tip_x, tip_y = pts[p+1]
                draw_sparkle(d, int(tip_x), int(tip_y), r=12, color=(255, 255, 255, 255))
                d.ellipse([tip_x - 9, tip_y - 9, tip_x + 9, tip_y + 9], outline=(0, 255, 255, 230), width=2)
                d.ellipse([tip_x - 14, tip_y - 14, tip_x + 14, tip_y + 14], outline=(255, 64, 129, 160), width=2)
        
        # Nhánh tia sét không gian phóng ra từ thân kiếm
        mid_pt = pts[16]
        rad_branch = arm_angle + 1.2
        b_x2 = mid_pt[0] + math.cos(rad_branch + 0.5) * 22
        b_y2 = mid_pt[1] + math.sin(rad_branch + 0.5) * 11
        d.line([mid_pt, (b_x2, b_y2)], fill=(255, 255, 255, 220), width=2)
        d.line([mid_pt, (b_x2, b_y2)], fill=(0, 255, 255, 180), width=4)
    
    # 6. Các khối lập phương không gian Room bay bổng quanh trận đồ
    for cube_i in range(8):
        cube_rad = math.radians(cube_i * 45 - angle_deg * 0.8)
        cube_r = 75 + (cube_i % 3) * 25
        cb_x = cx + math.cos(cube_rad) * cube_r
        cb_y = cy + math.sin(cube_rad) * (cube_r * (ry_out / rx_out))
        d.rectangle([cb_x - 3, cb_y - 3, cb_x + 3, cb_y + 3], fill=(0, 229, 255, 200), outline=(255, 255, 255, 255), width=1)
    
    # 7. Thập Tự Kiếm Kikoku phát sáng 4 hướng từ tâm
    cross_len = 54
    for cross_ang in [0, 90, 180, 270]:
        c_rad = math.radians(cross_ang + angle_deg * 0.5)
        cp_x = cx + math.cos(c_rad) * cross_len
        cp_y = cy + math.sin(c_rad) * (cross_len * (ry_out / rx_out))
        d.line([(cx, cy), (cp_x, cp_y)], fill=(0, 255, 255, 180), width=5)
        d.line([(cx, cy), (cp_x, cp_y)], fill=(255, 255, 255, 240), width=2)
        draw_sparkle(d, int(cp_x), int(cp_y), r=6, color=(255, 215, 0, 255))
    
    # 8. TRÁI TIM OPE OPE THẦN THÁNH PHÁT SÁNG RỰC RỠ Ở TRUNG TÂM
    hrx, hry = 34, 22
    # 2 Thùy đỉnh trái tim
    d.ellipse([cx - hrx, cy - hry - 6, cx, cy + 6], fill=(229, 57, 53, 245), outline=(255, 215, 0, 255), width=3)
    d.ellipse([cx, cy - hry - 6, cx + hrx, cy + 6], fill=(229, 57, 53, 245), outline=(255, 215, 0, 255), width=3)
    # Tam giác đáy trái tim
    d.polygon([(cx - hrx + 2, cy - 4), (cx + hrx - 2, cy - 4), (cx, cy + hry + 8)], fill=(229, 57, 53, 245))
    d.line([(cx - hrx + 2, cy - 4), (cx, cy + hry + 8)], fill=(255, 215, 0, 255), width=3)
    d.line([(cx + hrx - 2, cy - 4), (cx, cy + hry + 8)], fill=(255, 215, 0, 255), width=3)
    # Hoa văn xoắn ốc Ope Ope trắng tinh khiết & vàng kim
    d.arc([cx - int(hrx*0.75), cy - int(hry*0.75), cx + int(hrx*0.75), cy + int(hry*0.75)], start=45, end=270, fill=(255, 255, 255, 250), width=3)
    d.arc([cx - int(hrx*0.45), cy - int(hry*0.45), cx + int(hrx*0.45), cy + int(hry*0.45)], start=225, end=90, fill=(255, 215, 0, 250), width=2)
    # Lõi năng lượng phát sáng rực rỡ tại tâm trái tim
    draw_sparkle(d, cx, cy, r=10, color=(255, 255, 255, 255))
    d.ellipse([cx - 6, cy - 6, cx + 6, cy + 6], fill=(255, 255, 255, 255))
    return im

def draw_room_sphere(phase=0, w_box=288, h_box=288):
    """
    Vẽ quả cầu Room không gian bảo hộ bao quanh thân nhân vật (x4: 288x288, 1x: 72x72)
    """
    im = Image.new('RGBA', (w_box, h_box), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w_box // 2, h_box // 2
    r = 136 if phase == 1 else (133 if phase == 0 else 138)
    
    # 1. Hào quang nội vi quả cầu
    d.ellipse([cx - r, cy - r, cx + r, cy + r], fill=(0, 229, 255, 25))
    d.ellipse([cx - r, cy - r, cx + r, cy + r], outline=(0, 255, 255, 240), width=6)
    d.ellipse([cx - r - 4, cy - r - 4, cx + r + 4, cy + r + 4], outline=(0, 200, 255, 120), width=3)
    
    # 2. Lưới không gian vĩ tuyến & kinh tuyến 3D
    d.arc([cx - r, cy - int(r*0.55), cx + r, cy + int(r*0.55)], start=0, end=360, fill=(128, 255, 255, 140), width=3)
    d.arc([cx - r, cy - int(r*0.85), cx + r, cy + int(r*0.85)], start=0, end=360, fill=(0, 229, 255, 90), width=2)
    d.arc([cx - int(r*0.55), cy - r, cx + int(r*0.55), cy + r], start=0, end=360, fill=(128, 255, 255, 140), width=3)
    
    # 3. Các hạt ánh sao không gian xoay quanh quả cầu Room
    offset_ang = phase * 20
    for ang in [15, 60, 105, 150, 195, 240, 285, 330]:
        rad = math.radians(ang + offset_ang)
        sx = cx + int(math.cos(rad) * (r - 2))
        sy = cy + int(math.sin(rad) * (r - 2))
        d.ellipse([sx-4, sy-4, sx+4, sy+4], fill=(255, 255, 255, 255))
        draw_sparkle(d, sx, sy, r=5, color=(0, 255, 255, 240))
    return im

def create_effect_916_curtain_shield():
    angles = [0, 60, 120, 180, 240, 300]
    underfoot_imgs = [draw_giant_ope_underfoot(a) for a in angles]
    room_spheres = [draw_room_sphere(p) for p in [0, 1, 2]]

    w_sheet = 1292
    h_sheet = 580
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    # Bố trí 6 sprite trận pháp chân khổng lồ (352x176 tại x=0..708, y=0..536)
    # Hàng 1 (y=0): Frame 0 tại (0, 0), Frame 1 tại (356, 0)
    im.paste(underfoot_imgs[0], (0, 0), underfoot_imgs[0])
    im.paste(underfoot_imgs[1], (356, 0), underfoot_imgs[1])
    # Hàng 2 (y=180): Frame 2 tại (0, 180), Frame 3 tại (356, 180)
    im.paste(underfoot_imgs[2], (0, 180), underfoot_imgs[2])
    im.paste(underfoot_imgs[3], (356, 180), underfoot_imgs[3])
    # Hàng 3 (y=360): Frame 4 tại (0, 360), Frame 5 tại (356, 360)
    im.paste(underfoot_imgs[4], (0, 360), underfoot_imgs[4])
    im.paste(underfoot_imgs[5], (356, 360), underfoot_imgs[5])

    # Bố trí 3 sprite Room Sphere bao quanh thân (288x288 tại x=712..1292)
    im.paste(room_spheres[0], (712, 0), room_spheres[0])
    im.paste(room_spheres[1], (712, 292), room_spheres[1])
    im.paste(room_spheres[2], (1004, 0), room_spheres[2])

    # Tọa độ 1x (chia 4 từ x4):
    small_imgs = [
        (0,   0,  0, 88, 44), # Sprite 0: Trận pháp chân góc 0 độ
        (1,  89,  0, 88, 44), # Sprite 1: Trận pháp chân góc 60 độ
        (2,   0, 45, 88, 44), # Sprite 2: Trận pháp chân góc 120 độ
        (3,  89, 45, 88, 44), # Sprite 3: Trận pháp chân góc 180 độ
        (4,   0, 90, 88, 44), # Sprite 4: Trận pháp chân góc 240 độ
        (5,  89, 90, 88, 44), # Sprite 5: Trận pháp chân góc 300 độ
        (6, 178,  0, 72, 72), # Sprite 6: Quả cầu Room Sphere pha 0
        (7, 178, 73, 72, 72), # Sprite 7: Quả cầu Room Sphere pha 1
        (8, 251,  0, 72, 72), # Sprite 8: Quả cầu Room Sphere pha 2
    ]

    # CỐ ĐỊNH TỌA ĐỘ UY LỰC CỰC ĐẠI:
    # 1. Đại trận pháp chân khổng lồ (88x44): dx=-44, dy=-22 (layer 0: vẽ trải dài dưới chân nhân vật)
    # 2. Quả cầu Room bảo hộ (72x72): dx=-36, dy=-64 (layer 1: bao trọn toàn thân nhân vật)
    frames = [
        [
            (-44, -22, 0, 0, 0),
            (-36, -64, 6, 0, 1),
        ],
        [
            (-44, -22, 1, 0, 0),
            (-36, -64, 7, 0, 1),
        ],
        [
            (-44, -22, 2, 0, 0),
            (-36, -64, 8, 0, 1),
        ],
        [
            (-44, -22, 3, 0, 0),
            (-36, -64, 6, 0, 1),
        ],
        [
            (-44, -22, 4, 0, 0),
            (-36, -64, 7, 0, 1),
        ],
        [
            (-44, -22, 5, 0, 0),
            (-36, -64, 8, 0, 1),
        ]
    ]

    # Chuỗi chuyển động xoay tròn liên tục 360 độ siêu mượt mà
    seq = [0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(916, im, data_bytes)

# ==============================================================================
# 4. TẠO ICONS CHO TRÁI OPE OPE & 4 SKILL (ID 2191, 4421..4424)
# ==============================================================================
def create_all_ope_icons():
    script_dir = os.path.dirname(os.path.abspath(__file__))
    base_icon_dir = os.path.join(script_dir, 'data', 'icon')
    
    # --------------------------------------------------------------------------
    # Icon 1: Trái Ope Ope no Mi (Item ID 1016, Icon File ID 2191)
    # Hình trái tim màu đỏ thắm với hoa văn xoắn ốc Ope và cuống lá xanh lục
    # --------------------------------------------------------------------------
    im_fruit = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    df = ImageDraw.Draw(im_fruit)
    # Cuống trái
    df.arc([36, 6, 60, 26], start=180, end=360, fill=(76, 175, 80), width=4)
    df.line([(48, 16), (48, 26)], fill=(56, 142, 60), width=4)
    
    # Trái tim Ope Ope
    # Vẽ 2 vòng tròn đỉnh trái tim + tam giác đáy
    df.ellipse([14, 22, 54, 62], fill=(229, 57, 53), outline=(183, 28, 28), width=2)
    df.ellipse([42, 22, 82, 62], fill=(229, 57, 53), outline=(183, 28, 28), width=2)
    df.polygon([(16, 44), (80, 44), (48, 88)], fill=(229, 57, 53))
    df.line([(16, 44), (48, 88)], fill=(183, 28, 28), width=3)
    df.line([(80, 44), (48, 88)], fill=(183, 28, 28), width=3)
    
    # Hoa văn xoắn ốc Ope Ope vàng kim & trắng
    df.arc([24, 30, 46, 52], start=45, end=270, fill=(255, 215, 0), width=3)
    df.arc([50, 30, 72, 52], start=270, end=135, fill=(255, 215, 0), width=3)
    df.arc([36, 48, 60, 72], start=0, end=220, fill=(255, 255, 255), width=2)
    draw_sparkle(df, 30, 34, r=6, color=(255, 255, 255, 255))

    # --------------------------------------------------------------------------
    # Icon 2: Skill 1 - Room Trảm Không Gian (Icon File ID 4421)
    # --------------------------------------------------------------------------
    im_sk1 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d1 = ImageDraw.Draw(im_sk1)
    d1.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(10, 25, 45, 245), outline=(0, 229, 255), width=3)
    # Vòng tròn Room phát sáng
    d1.ellipse([14, 14, 82, 82], outline=(0, 229, 255, 220), width=3)
    d1.arc([14, 31, 82, 65], start=0, end=360, fill=(0, 255, 200, 180), width=2)
    # 2 đường chém kiếm khí xé không gian
    d1.line([(18, 78), (78, 18)], fill=(255, 255, 255), width=4)
    d1.line([(18, 78), (78, 18)], fill=(0, 229, 255, 180), width=7)
    d1.line([(22, 22), (74, 74)], fill=(128, 255, 255), width=3)
    draw_sparkle(d1, 48, 48, r=10, color=(255, 255, 255))
    draw_sparkle(d1, 74, 22, r=6, color=(0, 229, 255))

    # --------------------------------------------------------------------------
    # Icon 3: Skill 2 - Dao Phóng Xạ Gamma (Icon File ID 4422)
    # --------------------------------------------------------------------------
    im_sk2 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d2 = ImageDraw.Draw(im_sk2)
    d2.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(12, 35, 28, 245), outline=(0, 255, 170), width=3)
    # Lưỡi dao Gamma Plasma
    d2.polygon([(20, 76), (76, 20), (52, 20), (20, 52)], fill=(0, 255, 200), outline=(255, 255, 255), width=2)
    # Tia sét Gamma
    pts_l = [(18, 80), (36, 56), (42, 62), (62, 38), (78, 18)]
    for i in range(len(pts_l) - 1):
        d2.line([pts_l[i], pts_l[i+1]], fill=(0, 255, 255), width=4)
        d2.line([pts_l[i], pts_l[i+1]], fill=(255, 255, 255), width=2)
    draw_sparkle(d2, 76, 20, r=11, color=(255, 255, 255))
    draw_sparkle(d2, 28, 68, r=6, color=(0, 255, 170))

    # --------------------------------------------------------------------------
    # Icon 4: Skill 3 - Trận Pháp Trái Tim Ope (Icon File ID 4423)
    # --------------------------------------------------------------------------
    im_sk3 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d3 = ImageDraw.Draw(im_sk3)
    d3.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(18, 12, 35, 245), outline=(0, 229, 255), width=3)
    
    # Vòng trận pháp xoáy Ope
    d3.ellipse([12, 12, 84, 84], outline=(0, 229, 255, 220), width=2)
    d3.ellipse([20, 20, 76, 76], outline=(255, 64, 129, 180), width=2)
    for ang_i in [0, 90, 180, 270]:
        rad_i = math.radians(ang_i)
        d3.arc([48 - 28, 48 - 28, 48 + 28, 48 + 28], start=ang_i, end=ang_i+70, fill=(0, 255, 220, 230), width=3)
        
    # Trái Tim Ope ở trung tâm
    d3.ellipse([32, 34, 48, 50], fill=(229, 57, 53, 240), outline=(255, 215, 0, 240), width=1)
    d3.ellipse([48, 34, 64, 50], fill=(229, 57, 53, 240), outline=(255, 215, 0, 240), width=1)
    d3.polygon([(33, 44), (63, 44), (48, 64)], fill=(229, 57, 53, 240))
    d3.line([(33, 44), (48, 64)], fill=(255, 215, 0, 240), width=1)
    d3.line([(63, 44), (48, 64)], fill=(255, 215, 0, 240), width=1)
    d3.arc([40, 40, 56, 56], start=45, end=270, fill=(255, 255, 255, 240), width=1)
    
    draw_sparkle(d3, 20, 20, r=7, color=(0, 229, 255))
    draw_sparkle(d3, 76, 20, r=6, color=(255, 215, 0))
    draw_sparkle(d3, 76, 76, r=7, color=(0, 229, 255))
    draw_sparkle(d3, 20, 76, r=6, color=(255, 215, 0))

    # --------------------------------------------------------------------------
    # Icon 5: Skill 4 - Bác Sĩ Tử Thần (Icon File ID 4424)
    # Mũ đốm đặc trưng của Law + kiếm Nodachi Kikoku chéo
    # --------------------------------------------------------------------------
    im_sk4 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d4 = ImageDraw.Draw(im_sk4)
    d4.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(20, 20, 30, 245), outline=(255, 215, 0), width=3)
    # Mũ phớt đốm của Law
    d4.ellipse([22, 30, 74, 70], fill=(245, 245, 245), outline=(60, 60, 60), width=2)
    d4.ellipse([14, 56, 82, 74], fill=(230, 230, 230), outline=(60, 60, 60), width=2)
    # Đốm đen
    for spot_x, spot_y, spot_r in [(34, 42, 5), (58, 40, 6), (46, 52, 5), (32, 60, 4), (62, 58, 5)]:
        d4.ellipse([spot_x - spot_r, spot_y - spot_r, spot_x + spot_r, spot_y + spot_r], fill=(35, 35, 35))
    # Kiếm Nodachi Kikoku chữ thập vàng
    d4.line([(18, 78), (78, 18)], fill=(255, 215, 0), width=3)
    draw_sparkle(d4, 78, 18, r=9, color=(255, 255, 255))
    draw_sparkle(d4, 20, 20, r=6, color=(255, 215, 0))

    icons_to_save = {
        2191: im_fruit,
        4421: im_sk1,
        4422: im_sk2,
        4423: im_sk3,
        4424: im_sk4
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
    print("=== ĐANG TẠO SPRITE SHEETS & DATA EFFECT CHO TRÁI OPE OPE NO MI (ID 914..916) ===")
    create_effect_914_room_slash()
    create_effect_915_gamma_knife()
    create_effect_916_curtain_shield()
    print("=== ĐANG TẠO ICONS TRÁI ÁC QUỶ & KỸ NĂNG OPE OPE (ID 2191, 4421..4424) ===")
    create_all_ope_icons()
    print("=== HOÀN TẤT TOÀN BỘ TÀI NGUYÊN HÌNH ẢNH & HIỆU ỨNG TRÁI OPE OPE! ===")
