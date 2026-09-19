import os
import sys
import struct
import math
import random
import numpy as np
from PIL import Image, ImageDraw, ImageFilter

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

def draw_sparkle(draw, cx, cy, r=6, color=(255, 255, 255, 240)):
    draw.line([(cx - r, cy), (cx + r, cy)], fill=color, width=2)
    draw.line([(cx, cy - r), (cx, cy + r)], fill=color, width=2)
    r_small = max(2, int(r * 0.6))
    draw.line([(cx - r_small, cy - r_small), (cx + r_small, cy + r_small)], fill=color, width=1)
    draw.line([(cx - r_small, cy + r_small), (cx + r_small, cy - r_small)], fill=color, width=1)

def draw_lightning(draw, pts, color_glow=(0, 255, 200, 180), color_core=(255, 255, 255, 255), width=3):
    for i in range(len(pts) - 1):
        draw.line([pts[i], pts[i+1]], fill=color_glow, width=width + 4)
        draw.line([pts[i], pts[i+1]], fill=color_core, width=max(1, width))

def draw_tapered_haki_lightning(draw, pts, glow_col=(255, 20, 60), core_col=(255, 255, 255), base_w=3):
    n = len(pts)
    if n < 2:
        return
    # Pass 1: Dark outer Haki shadow
    for i in range(n - 1):
        t = i / float(n - 1)
        w_cur = max(1, int(round(base_w * (1.0 - t * 0.45))))
        p1, p2 = pts[i], pts[i+1]
        draw.line([p1, p2], fill=(12, 0, 8, 230), width=w_cur + 4)
        r1 = (w_cur + 4) // 2
        draw.ellipse([p1[0]-r1, p1[1]-r1, p1[0]+r1, p1[1]+r1], fill=(12, 0, 8, 230))
        draw.ellipse([p2[0]-r1, p2[1]-r1, p2[0]+r1, p2[1]+r1], fill=(12, 0, 8, 230))

    # Pass 2: Glowing crimson Haki aura
    glow_rgba = (glow_col[0], glow_col[1], glow_col[2], 255)
    for i in range(n - 1):
        t = i / float(n - 1)
        w_cur = max(1, int(round(base_w * (1.0 - t * 0.45))))
        p1, p2 = pts[i], pts[i+1]
        draw.line([p1, p2], fill=glow_rgba, width=w_cur + 2)
        r2 = (w_cur + 2) // 2
        draw.ellipse([p1[0]-r2, p1[1]-r2, p1[0]+r2, p1[1]+r2], fill=glow_rgba)
        draw.ellipse([p2[0]-r2, p2[1]-r2, p2[0]+r2, p2[1]+r2], fill=glow_rgba)

    # Pass 3: Blinding hot white core
    core_rgba = (core_col[0], core_col[1], core_col[2], 255)
    for i in range(n - 1):
        t = i / float(n - 1)
        w_cur = max(1, int(round(base_w * (1.0 - t * 0.45))))
        p1, p2 = pts[i], pts[i+1]
        w_core = max(1, w_cur - 1)
        draw.line([p1, p2], fill=core_rgba, width=w_core)
        r3 = w_core // 2
        draw.ellipse([p1[0]-r3, p1[1]-r3, p1[0]+r3, p1[1]+r3], fill=core_rgba)
        draw.ellipse([p2[0]-r3, p2[1]-r3, p2[0]+r3, p2[1]+r3], fill=core_rgba)

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

def save_multizoom_effect(eff_id, img_x4, data_bytes, max_colors=128):
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
        c = max_colors
        import io
        buf = io.BytesIO()
        im_q = im.quantize(colors=c, method=Image.Quantize.FASTOCTREE)
        try:
            im_q.save(buf, format='PNG')
            with open(img_path, 'wb') as f:
                f.write(buf.getvalue())
        except Exception as e:
            print(f"Lỗi khi save {z}/{eff_id}: {e}")
            im.save(img_path, format='PNG')
        
        data_path = os.path.normpath(os.path.join(dir_data, f'{eff_id}'))
        with open(data_path, 'wb') as f:
            f.write(data_bytes)
    print(f"-> Đã tạo thành công Effect ID {eff_id} cho toàn bộ các zoom x0..x4 (tối ưu dung lượng)")

# ==============================================================================
# 1. TẠO EFFECT 914: ROOM - TRẢM KHÔNG GIAN (SPATIAL SLASH - SWORD RAIN)
# ==============================================================================
def draw_anime_hemisphere_room_dome(w=992, h=608):
    """
    Vẽ VÒM NỬA HÌNH CẦU ROOM KHỔNG LỒ CHUẨN ANIME ONE PIECE (PHIÊN BẢN TO HƠN):
    - Kích thước vút cao: 4x: 992x608 (1x: 248x152), chiều cao vòm 130px
    - Màu sắc xanh cyan ngọc lam trong suốt (Sky Blue / Cyan Glass Glow) chuẩn ảnh Room mẫu
    - Lòng vòm trong suốt dịu nhẹ, không che khuất nhân vật/quái
    - Vành viền vòm cầu phát sáng neon cyan mềm mại
    - Vệt bóng kính phản chiếu (Glass Sheen Highlight) cong mềm và đốm sáng phản quang chuẩn phối cảnh
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2 # 496
    cy_base = h - 48 # 560
    rx = 460
    h_dome = cy_base - 40 # 520
    ry = 44

    # 1. Màng năng lượng Room dạng Gradient bán elip trong suốt
    y_coords, x_coords = np.ogrid[:h, :w]
    dx = (x_coords - cx) / float(rx)
    dy = np.where(y_coords <= cy_base, (cy_base - y_coords) / float(h_dome), 0.0)
    dist = np.sqrt(dx**2 + dy**2)
    in_dome = (dist <= 1.0) & (y_coords <= cy_base)

    arr = np.zeros((h, w, 4), dtype=np.uint8)
    alpha_body = np.clip(40.0 + 75.0 * (dist ** 2.2), 0, 115)

    r_body = 172.0 - 50.0 * dist
    g_body = 240.0 - 25.0 * dist
    b_body = 253.0 - 10.0 * dist

    arr[in_dome, 0] = np.clip(r_body[in_dome], 0, 255)
    arr[in_dome, 1] = np.clip(g_body[in_dome], 0, 255)
    arr[in_dome, 2] = np.clip(b_body[in_dome], 0, 255)
    arr[in_dome, 3] = np.clip(alpha_body[in_dome], 0, 255)

    im_body = Image.fromarray(arr, mode='RGBA')
    im.alpha_composite(im_body)

    # 2. Vành vòm cầu phát quang đa tầng mềm mại
    for dr, a, width in [(14, 35, 16), (10, 65, 12), (5, 110, 7)]:
        pts = []
        for deg in range(180, 361, 2):
            rad = math.radians(deg)
            px = cx + int(math.cos(rad) * (rx + dr * 0.5))
            py = cy_base + int(math.sin(rad) * (h_dome + dr * 0.5))
            pts.append((px, py))
        d.line(pts, fill=(70, 195, 245, a), width=width)

    # Vành chính của Room (Lõi sáng trắng-cyan với viền xanh thiên thanh)
    pts_main = []
    for deg in range(180, 361, 1):
        rad = math.radians(deg)
        px = cx + int(math.cos(rad) * rx)
        py = cy_base + int(math.sin(rad) * h_dome)
        pts_main.append((px, py))

    d.line(pts_main, fill=(50, 180, 240, 240), width=10)
    d.line(pts_main, fill=(130, 230, 255, 255), width=6)
    d.line(pts_main, fill=(230, 252, 255, 255), width=3)

    # 3. Vết bóng kính phản chiếu (Glass Sheen Highlight)
    sheen_pts = []
    for deg in range(200, 260, 2):
        rad = math.radians(deg)
        px = cx + int(math.cos(rad) * (rx * 0.94))
        py = cy_base + int(math.sin(rad) * (h_dome * 0.95))
        sheen_pts.append((px, py))
    d.line(sheen_pts, fill=(245, 255, 255, 190), width=9)
    d.line(sheen_pts, fill=(255, 255, 255, 255), width=4)

    # Đốm sáng phản quang nhỏ (Specular Dot)
    dot_rad = math.radians(205)
    dot_x = cx + int(math.cos(dot_rad) * (rx * 0.88))
    dot_y = cy_base + int(math.sin(dot_rad) * (h_dome * 0.89))
    d.ellipse([dot_x - 8, dot_y - 12, dot_x + 8, dot_y + 12], fill=(245, 255, 255, 210))

    # 4. Vành tiếp đất dạng elip phát sáng mềm
    d.ellipse([cx - rx - 5, cy_base - ry - 5, cx + rx + 5, cy_base + ry + 5], fill=(130, 220, 255, 30))
    d.ellipse([cx - rx, cy_base - ry, cx + rx, cy_base + ry], outline=(60, 190, 240, 210), width=6)
    d.ellipse([cx - rx + 2, cy_base - ry + 2, cx + rx - 2, cy_base + ry - 2], outline=(220, 250, 255, 240), width=3)

    return im

def draw_circling_smoke_puff(w=128, h=72, variant=0):
    """
    Vẽ cụm mây khói cuộn xoay tròn bồng bềnh quanh viền chân Room (Phiên bản to hơn):
    - Phong cách Anime bồng bềnh nhiều tầng khối (3D puffy lobes)
    - Viền bóng đổ xanh lam ngọc dịu nhẹ (Soft cyan shadow)
    - Thân mây trắng ngà viền mềm sắc nét
    - Đỉnh mây phát sáng trắng chói lọi (Crest highlights)
    - Luồng gió lốc Room phát quang ôm quanh
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    sx, sy = w / 96.0, h / 56.0
    
    base_lobes = [
        (-22,  3, 16, 12),
        ( -8, -5, 22, 16),
        ( 10, -3, 20, 15),
        ( 24,  4, 15, 11),
        (  0,  6, 24, 11),
    ] if variant == 0 else [
        (-26,  3, 17, 12),
        (-10, -6, 25, 17),
        ( 12, -4, 23, 16),
        ( 28,  3, 18, 12),
        (  2,  6, 27, 12),
    ]
    lobes = [(int(dx*sx), int(dy*sy), int(rx*sx), int(ry*sy)) for dx, dy, rx, ry in base_lobes]

    # 1. Lớp bóng mây đáy
    for dx, dy, rx, ry in lobes:
        d.ellipse([cx + dx - rx - 1, cy + dy - ry + 3, cx + dx + rx + 1, cy + dy + ry + 5], fill=(130, 200, 235, 120))
    # 2. Lớp thân mây
    for dx, dy, rx, ry in lobes:
        d.ellipse([cx + dx - rx, cy + dy - ry, cx + dx + rx, cy + dy + ry], fill=(220, 245, 255, 210), outline=(140, 215, 245, 160), width=1)
    # 3. Lớp đỉnh mây sáng chói
    for dx, dy, rx, ry in lobes:
        hl_rx = max(4, int(rx * 0.65))
        hl_ry = max(3, int(ry * 0.55))
        d.ellipse([cx + dx - hl_rx + 1, cy + dy - hl_ry - 3, cx + dx + hl_rx - 1, cy + dy + hl_ry - 3], fill=(255, 255, 255, 245))

    # 4. Vệt gió cuốn năng lượng Room phát quang
    wind_y = cy + int(8 * sy)
    arc_rx = int((40 if variant == 0 else 48) * sx)
    d.arc([cx - arc_rx, wind_y - 8, cx + arc_rx, wind_y + 8], start=160, end=350, fill=(255, 255, 255, 220), width=2)
    d.arc([cx - int(arc_rx*0.75), wind_y - 4, cx + int(arc_rx*0.75), wind_y + 4], start=175, end=335, fill=(80, 220, 255, 180), width=2)
    return im

def draw_straight_kikoku_sword(w=88, h=224, is_trail=False, is_giant=False):
    """
    Vẽ kiếm Kikoku rơi THẲNG ĐỨNG UY LỰC - BỌC HAKI ĐEN & HIỆU ỨNG TIA SÉT:
    - Thân kiếm Hắc Kiếm bọc Haki Vũ Trang (Busoshoku Koka) đen tuyền ánh than cực ngầu
    - Sống kiếm ánh kim loại tím bạc sắc lẹm với vệt phản chiếu lấp lánh
    - Lưỡi cắt sắc như dao cạo
    - Vành Tsuba chữ thập bọc lông tơ trắng đặc trưng của Law với khuyên kẹp vàng
    - Chuôi kiếm bọc vải tím đậm quấn chéo hoa văn chữ thập trắng, chuôi nắp vàng
    - Hào quang Haki đen - đỏ thẫm ôm sát lưỡi kiếm
    - Hiệu ứng tia sét Haki (Conqueror/Armament Lightning) đỏ rực rỡ bọc lõi trắng chói lọi,
      ngoằn ngoèo xé toạc không gian quanh thân kiếm và bắn toé tia sét
    - Sóng xung kích siêu thanh (sonic shockwave) và vệt xé gió Haki phía sau khi rơi
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    s = 1.0 if not is_giant else 1.95

    y_pommel = 18 if not is_giant else 20
    y_tsuba  = 68 if not is_giant else 95
    y_tip    = h - 10 if not is_giant else h - 20
    blade_w  = 16 if not is_giant else 32
    hw = blade_w // 2

    # 1. VỆT RƠI XÉ GIÓ / SÓNG XUNG KÍCH HAKI PHÍA TRÊN (KHI RƠI)
    if is_trail:
        trail_w_top = 32 if not is_giant else 64
        trail_w_bot = hw + 8 if not is_giant else hw + 14
        y_trail_end = y_tsuba + 4

        # Vệt luồng khí Haki đen - đỏ cuộn xoáy phía sau kiếm khi rơi
        d.polygon([(cx - trail_w_top, 0), (cx + trail_w_top, 0), 
                   (cx + trail_w_bot, y_trail_end), (cx - trail_w_bot, y_trail_end)], 
                  fill=(15, 2, 10, 160))
        d.polygon([(cx - trail_w_top + 8, 0), (cx + trail_w_top - 8, 0), 
                   (cx + hw + 2, y_trail_end), (cx - hw - 2, y_trail_end)], 
                  fill=(180, 15, 45, 120))
        d.line([(cx, 0), (cx, y_tsuba)], fill=(255, 60, 90, 220), width=4 if not is_giant else 9)
        d.line([(cx, 0), (cx, y_tsuba)], fill=(255, 255, 255, 240), width=2 if not is_giant else 4)

        # Sóng sonic chevron Haki
        for y_c in [int(y_tsuba * 0.25), int(y_tsuba * 0.55), int(y_tsuba * 0.85)]:
            sw = int((y_tsuba - y_c) * 0.35) + 6
            d.line([(cx - sw - 8, y_c - 6), (cx, y_c + 6), (cx + sw + 8, y_c - 6)], 
                   fill=(255, 20, 60, 190), width=2 if not is_giant else 4)
            d.line([(cx - sw - 4, y_c - 6), (cx, y_c + 6), (cx + sw + 4, y_c - 6)], 
                   fill=(255, 255, 255, 230), width=1 if not is_giant else 2)

    # 2. HÀO QUANG HAKI ĐEN - ĐỎ THẪM ÔM SÁT LƯỠI KIẾM
    d.polygon([
        (cx - hw - 4, y_tsuba), (cx + hw + 4, y_tsuba),
        (cx + hw + 4, y_tip - 20), (cx, y_tip + 4),
        (cx - hw - 4, y_tip - 20)
    ], fill=(160, 10, 40, 200))
    d.polygon([
        (cx - hw - 2, y_tsuba), (cx + hw + 2, y_tsuba),
        (cx + hw + 2, y_tip - 18), (cx, y_tip + 2),
        (cx - hw - 2, y_tip - 18)
    ], fill=(220, 20, 60, 220))

    # 3. THÂN KIẾM HẮC KIẾM TUYỀN (BUSOSHOKU KOKA - JET BLACK KATANA)
    # Bevel bên trái: Đen kịt obsidian (mặt tối)
    d.polygon([
        (cx - hw, y_tsuba), (cx, y_tsuba),
        (cx, y_tip - 16), (cx - hw, y_tip - 22)
    ], fill=(8, 6, 12, 255))
    # Bevel bên phải: Đen thép nòng súng (mặt sáng phản chiếu)
    d.polygon([
        (cx, y_tsuba), (cx + hw, y_tsuba),
        (cx + hw, y_tip - 22), (cx, y_tip - 16)
    ], fill=(24, 20, 30, 255))

    # Mũi nhọn Katana (Kissaki)
    d.polygon([
        (cx - hw, y_tip - 22), (cx, y_tip - 16),
        (cx, y_tip), (cx - 2, y_tip)
    ], fill=(12, 9, 15, 255))
    d.polygon([
        (cx, y_tsuba), (cx, y_tip),
        (cx + hw, y_tip - 22)
    ], fill=(20, 16, 26, 255))

    # Sống kiếm giữa (Shinogi): Ánh bạc tím kim loại cực kỳ sắc bén
    d.line([(cx, y_tsuba), (cx, y_tip - 4)], fill=(160, 150, 180, 240), width=1 if not is_giant else 2)
    # Vệt lóa sáng phản quang trên sống kiếm
    step_g = 35 if not is_giant else 50
    for yg in range(y_tsuba + 25, y_tip - 25, step_g):
        d.line([(cx, yg), (cx, yg + 10)], fill=(255, 255, 255, 255), width=1 if not is_giant else 2)

    # Lưỡi cắt mép trái và phải (sắc như dao cạo)
    d.line([(cx - hw, y_tsuba), (cx - hw, y_tip - 22)], fill=(70, 60, 85, 230), width=1)
    d.line([(cx + hw, y_tsuba), (cx + hw, y_tip - 22)], fill=(225, 220, 245, 250), width=1 if not is_giant else 2)
    d.line([(cx + hw, y_tip - 22), (cx, y_tip)], fill=(255, 255, 255, 255), width=2 if not is_giant else 3)
    d.line([(cx - hw, y_tip - 22), (cx, y_tip)], fill=(130, 120, 150, 240), width=1)

    # 4. VÀNH TSUBA KIKOKU (chữ thập lông tơ trắng)
    tsuba_w = 34 if not is_giant else 64
    tsuba_h = 15 if not is_giant else 24
    d.ellipse([cx - tsuba_w//2 - 2, y_tsuba - tsuba_h//2 - 1, cx + tsuba_w//2 + 2, y_tsuba + tsuba_h//2 + 1], fill=(215, 220, 225, 255))
    d.ellipse([cx - tsuba_w//2, y_tsuba - tsuba_h//2, cx + tsuba_w//2, y_tsuba + tsuba_h//2], fill=(255, 255, 255, 255), outline=(140, 150, 160, 255), width=2)
    d.ellipse([cx - 8, y_tsuba - 5, cx + 8, y_tsuba + 5], fill=(255, 215, 0, 255), outline=(190, 150, 0, 255))

    # 5. CHUÔI KIẾM KIKOKU (TSUKA)
    hilt_w = 9 if not is_giant else 15
    d.line([(cx, y_pommel), (cx, y_tsuba)], fill=(30, 10, 40, 255), width=hilt_w)
    cross_step = 8 if not is_giant else 13
    for y_c in range(y_pommel + 5, y_tsuba - 5, cross_step):
        d.line([(cx - 5, y_c - 2), (cx + 5, y_c + 2)], fill=(255, 255, 255, 230), width=1 if not is_giant else 2)
        d.line([(cx - 5, y_c + 2), (cx + 5, y_c - 2)], fill=(255, 255, 255, 230), width=1 if not is_giant else 2)
    d.ellipse([cx - 7, y_pommel - 6, cx + 7, y_pommel + 6], fill=(255, 215, 0, 255), outline=(190, 150, 0, 255))

    # 6. TIA SÉT HAKI NGUYÊN BẢN (ORGANIC ANIME LIGHTNING BOLTS)
    # Bolt A: Tia sét trên bên trái, chạy từ cổ kiếm và phóng nhánh ra không trung
    bolt_A = [
        (cx - int(2*s), y_tsuba + int(6*s)),
        (cx - int(12*s), y_tsuba + int(20*s)),
        (cx - int(24*s), y_tsuba + int(32*s)),
        (cx - int(16*s), y_tsuba + int(48*s)),
        (cx - int(4*s), y_tsuba + int(64*s)),
        (cx + int(8*s), y_tsuba + int(84*s))
    ]
    draw_tapered_haki_lightning(d, bolt_A, base_w=3 if not is_giant else 5)

    # Branch A1 phóng ra ngoài bên trái
    branch_A1 = [
        (cx - int(24*s), y_tsuba + int(32*s)),
        (cx - int(38*s), y_tsuba + int(24*s)),
        (cx - int(48*s), y_tsuba + int(34*s))
    ]
    draw_tapered_haki_lightning(d, branch_A1, base_w=2 if not is_giant else 3)

    # Bolt B: Tia sét giữa bên phải, xé toạc không gian và đâm vào thân kiếm
    bolt_B = [
        (cx + int(4*s), y_tsuba + int(68*s)),
        (cx + int(20*s), y_tsuba + int(80*s)),
        (cx + int(36*s), y_tsuba + int(72*s)),
        (cx + int(44*s), y_tsuba + int(90*s)),
        (cx + int(26*s), y_tsuba + int(106*s)),
        (cx + int(10*s), y_tsuba + int(120*s)),
        (cx - int(5*s), y_tsuba + int(138*s))
    ]
    draw_tapered_haki_lightning(d, bolt_B, base_w=3 if not is_giant else 5)

    # Branch B1 phóng ra ngoài bên phải
    branch_B1 = [
        (cx + int(36*s), y_tsuba + int(72*s)),
        (cx + int(50*s), y_tsuba + int(60*s))
    ]
    draw_tapered_haki_lightning(d, branch_B1, base_w=2 if not is_giant else 3)

    # Bolt C: Tia sét phần dưới, phóng thẳng xuống mũi kiếm
    bolt_C = [
        (cx - int(8*s), y_tsuba + int(135*s)),
        (cx - int(22*s), y_tsuba + int(152*s)),
        (cx - int(34*s), y_tsuba + int(144*s)),
        (cx - int(20*s), y_tsuba + int(168*s)),
        (cx + int(4*s), y_tsuba + int(184*s)),
        (cx - int(6*s), y_tip - int(6*s)),
        (cx, y_tip + int(8*s))
    ]
    draw_tapered_haki_lightning(d, bolt_C, base_w=3 if not is_giant else 5)

    # Branch C1 phóng ra ngoài bên trái ở đoạn dưới
    branch_C1 = [
        (cx - int(22*s), y_tsuba + int(152*s)),
        (cx - int(42*s), y_tsuba + int(162*s)),
        (cx - int(50*s), y_tsuba + int(154*s))
    ]
    draw_tapered_haki_lightning(d, branch_C1, base_w=2 if not is_giant else 3)

    # Branch C2: Nhánh sét đánh ngang từ mũi kiếm
    branch_C2 = [
        (cx - int(4*s), y_tip - int(4*s)),
        (cx + int(16*s), y_tip - int(12*s)),
        (cx + int(24*s), y_tip - int(6*s))
    ]
    draw_tapered_haki_lightning(d, branch_C2, base_w=2 if not is_giant else 2)

    # Tia sét giật ngược lên phía trên vệt rơi (nếu có trail)
    if is_trail:
        bolt_trail = [
            (cx - int(4*s), y_tsuba - int(6*s)),
            (cx + int(14*s), y_tsuba - int(30*s)),
            (cx - int(18*s), y_tsuba - int(56*s)),
            (cx + int(12*s), 0)
        ]
        draw_tapered_haki_lightning(d, bolt_trail, base_w=3 if not is_giant else 5)

    if is_giant:
        # Đại Thần Kiếm: Thêm tia sét khổng lồ giật ngang
        bolt_G = [
            (cx + int(26*s), y_tsuba + int(106*s)),
            (cx + int(52*s), y_tsuba + int(124*s)),
            (cx + int(66*s), y_tsuba + int(145*s)),
            (cx + int(56*s), y_tsuba + int(170*s))
        ]
        draw_tapered_haki_lightning(d, bolt_G, base_w=4)

    # Bùng nổ tia sét tại mũi kiếm (Tip discharge)
    r_tip = 12 if not is_giant else 24
    for r_s, c in [(r_tip + 6, (255, 20, 60, 200)), (r_tip, (255, 255, 255, 255))]:
        d.line([(cx - r_s, y_tip), (cx + r_s, y_tip)], fill=c, width=2 if not is_giant else 4)
        d.line([(cx, y_tip - r_s), (cx, y_tip + r_s)], fill=c, width=2 if not is_giant else 4)
    d.ellipse([cx - 4, y_tip - 4, cx + 4, y_tip + 4], fill=(255, 255, 255, 255))

    # Đốm sao sáng tại các đầu mút tia sét
    spark_pts = [
        (cx - int(48*s), y_tsuba + int(34*s)),
        (cx + int(50*s), y_tsuba + int(60*s)),
        (cx - int(50*s), y_tsuba + int(154*s)),
        (cx + int(30*s), y_tip - int(8*s))
    ]
    for px, py in spark_pts:
        draw_sparkle(d, px, py, r=int(5*s), color=(255, 255, 255, 240))
        draw_sparkle(d, px, py, r=int(9*s), color=(255, 30, 80, 220))

    return im

def draw_ground_crater_and_debris(w=208, h=104, phase=0):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 160.0
    rx = int((54 if phase == 0 else 66) * s)
    ry = int((16 if phase == 0 else 20) * s)

    d.ellipse([cx - rx - 5, cy - ry - 5, cx + rx + 5, cy + ry + 5], fill=(16, 8, 4, 240))
    d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], fill=(8, 4, 2, 255))
    d.arc([cx - rx, cy - ry, cx + rx, cy + ry], start=0, end=180, fill=(80, 50, 30, 255), width=int(4*s))

    cracks = [
        [(-rx, 0), (-rx - int(16*s), -int(4*s)), (-rx - int(28*s), int(2*s))],
        [(rx, 0), (rx + int(16*s), -int(2*s)), (rx + int(28*s), int(4*s))],
        [(-int(rx*0.5), ry), (-int(rx*0.7), ry + int(8*s))],
        [(int(rx*0.5), ry), (int(rx*0.7), ry + int(8*s))],
    ]
    for pts in cracks:
        for i in range(len(pts)-1):
            p1 = (cx + pts[i][0], cy + pts[i][1])
            p2 = (cx + pts[i+1][0], cy + pts[i+1][1])
            d.line([p1, p2], fill=(0, 255, 240, 220), width=int(3*s))
            d.line([p1, p2], fill=(255, 255, 255, 240), width=1)

    debris_left = [
        [(cx - rx - int(10*s), cy - int(16*s)), int(8*s), (120, 80, 50)],
        [(cx - rx + int(4*s), cy - int(24*s)), int(10*s), (140, 95, 60)],
        [(cx - int(rx*0.5), cy - int(28*s)), int(7*s), (100, 65, 45)],
    ] if phase==0 else [
        [(cx - rx - int(18*s), cy - int(22*s)), int(11*s), (130, 85, 60)],
        [(cx - rx - int(4*s), cy - int(36*s)), int(13*s), (150, 105, 70)],
        [(cx - int(rx*0.6), cy - int(42*s)), int(9*s), (110, 75, 50)],
    ]
    debris_right = [
        [(cx + rx + int(8*s), cy - int(14*s)), int(8*s), (125, 85, 60)],
        [(cx + rx - int(6*s), cy - int(24*s)), int(11*s), (145, 100, 70)],
        [(cx + int(rx*0.5), cy - int(26*s)), int(7*s), (105, 70, 50)],
    ] if phase==0 else [
        [(cx + rx + int(16*s), cy - int(24*s)), int(12*s), (135, 90, 65)],
        [(cx + rx + int(2*s), cy - int(38*s)), int(14*s), (155, 110, 75)],
        [(cx + int(rx*0.6), cy - int(44*s)), int(9*s), (115, 80, 55)],
    ]

    for (bx, by), sz, col in debris_left + debris_right:
        d.polygon([(bx - sz, by), (bx - sz//2, by - sz), (bx + sz//2, by - sz//2), (bx + sz, by + sz//3), (bx, by + sz)], fill=col, outline=(40, 25, 18, 255))
        d.line([(bx - sz//2, by - sz), (bx + sz//2, by - sz//2)], fill=(210, 180, 150, 255), width=2)
    
    draw_sparkle(d, cx, cy, r=int(10*s), color=(0, 255, 255, 230))
    return im

def draw_sword_stuck_in_crater(w=120, h=184, phase=0):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    s = w / 96.0
    y_ground = int(110 * s)
    y_pommel = int(18 * s)
    y_tsuba  = int(62 * s)

    # 1. Hố đất đá bị nứt toác do kiếm Haki cắm ngập
    rx_crater = int(42 * s)
    ry_crater = int(15 * s)
    d.ellipse([cx - rx_crater - 2, y_ground - ry_crater - 2, cx + rx_crater + 2, y_ground + ry_crater + 2], fill=(16, 6, 8, 245))
    d.ellipse([cx - int(30*s), y_ground - int(9*s), cx + int(30*s), y_ground + int(11*s)], fill=(6, 2, 4, 255))
    d.arc([cx - rx_crater, y_ground - ry_crater, cx + rx_crater, y_ground + ry_crater], start=0, end=180, fill=(80, 40, 30, 255), width=int(4*s))

    # 2. Vết nứt đất rực sáng năng lượng sét Haki
    cracks = [
        (-36, 4, -18, 2), (-18, 2, 0, 0), (0, 0, 20, 3), (20, 3, 38, 6), (0, 0, 8, 14), (0, 0, -10, 12)
    ]
    for dx1, dy1, dx2, dy2 in cracks:
        p1 = (cx + int(dx1*s), y_ground + int(dy1*s))
        p2 = (cx + int(dx2*s), y_ground + int(dy2*s))
        c_glow = (255, 20, 60, 240) if phase in [2, 3] else (255, 30, 70, 200)
        d.line([p1, p2], fill=c_glow, width=int(3*s))
        d.line([p1, p2], fill=(255, 255, 255, 240), width=1)

    # Đất đá vụn văng quanh chân kiếm
    d.polygon([(cx - int(14*s), y_ground + int(4*s)), (cx - int(8*s), y_ground - int(10*s)), 
               (cx + int(8*s), y_ground - int(8*s)), (cx + int(14*s), y_ground + int(4*s))], 
              fill=(100, 60, 40), outline=(40, 20, 15))

    # 3. LƯỠI KIẾM HẮC KIẾM CẮM XUỐNG ĐẤT
    blade_w = int(12 * s)
    hw = blade_w // 2

    # Hào quang Haki đỏ ôm thân kiếm (dao động nhẹ theo phase tạo độ thở uy lực)
    pulse_w = 1 if phase in [1, 3] else (2 if phase == 2 else 0)
    d.polygon([
        (cx - hw - 2 - pulse_w, y_tsuba), (cx + hw + 2 + pulse_w, y_tsuba),
        (cx + hw + 2 + pulse_w, y_ground), (cx - hw - 2 - pulse_w, y_ground)
    ], fill=(180, 10, 40, 190))
    d.polygon([
        (cx - hw - 1 - pulse_w, y_tsuba), (cx + hw + 1 + pulse_w, y_tsuba),
        (cx + hw + 1 + pulse_w, y_ground), (cx - hw - 1 - pulse_w, y_ground)
    ], fill=(240, 25, 65, 220))

    # Thân kiếm Haki đen tuyền
    d.polygon([(cx - hw, y_tsuba), (cx, y_tsuba), (cx, y_ground), (cx - hw, y_ground)], fill=(8, 6, 12, 255))
    d.polygon([(cx, y_tsuba), (cx + hw, y_tsuba), (cx + hw, y_ground), (cx, y_ground)], fill=(24, 20, 30, 255))

    # Sống kiếm giữa phản quang
    d.line([(cx, y_tsuba), (cx, y_ground)], fill=(160, 150, 180, 240), width=1)
    d.line([(cx, y_tsuba + 10), (cx, y_tsuba + 25)], fill=(255, 255, 255, 255), width=1)

    # Mép lưỡi cắt sắc bén
    d.line([(cx - hw, y_tsuba), (cx - hw, y_ground)], fill=(70, 60, 85, 230), width=1)
    d.line([(cx + hw, y_tsuba), (cx + hw, y_ground)], fill=(225, 220, 245, 250), width=1)

    # 4. Vành Tsuba lông tơ trắng
    tsuba_w = int(28 * s)
    tsuba_h = int(13 * s)
    d.ellipse([cx - tsuba_w//2 - 2, y_tsuba - tsuba_h//2 - 1, cx + tsuba_w//2 + 2, y_tsuba + tsuba_h//2 + 1], fill=(215, 220, 225, 255))
    d.ellipse([cx - tsuba_w//2, y_tsuba - tsuba_h//2, cx + tsuba_w//2, y_tsuba + tsuba_h//2], fill=(255, 255, 255, 255), outline=(140, 150, 160, 255), width=2)
    d.ellipse([cx - 5, y_tsuba - 3, cx + 5, y_tsuba + 3], fill=(255, 215, 0, 255), outline=(190, 150, 0, 255))

    # 5. Chuôi kiếm Tsuka
    hilt_w = int(8 * s)
    d.line([(cx, y_pommel), (cx, y_tsuba)], fill=(30, 10, 40, 255), width=hilt_w)
    cross_step = int(7 * s)
    for y_c in range(y_pommel + 4, y_tsuba - 4, cross_step):
        d.line([(cx - 3, y_c - 2), (cx + 3, y_c + 2)], fill=(255, 255, 255, 230), width=1)
        d.line([(cx - 3, y_c + 2), (cx + 3, y_c - 2)], fill=(255, 255, 255, 230), width=1)
    d.ellipse([cx - 5, y_pommel - 5, cx + 5, y_pommel + 5], fill=(255, 215, 0, 255), outline=(190, 150, 0, 255))

    # 6. TIA SÉT HAKI DYNAMIC (4 PHASES CHUYỂN ĐỘNG LIÊN HOÀN)
    if phase == 0:
        # Phase 0: Sét Haki cuộn xoáy cánh trái, phóng thẳng xuống đất
        b1 = [
            (cx + int(2*s), y_tsuba + int(2*s)),
            (cx - int(10*s), y_tsuba + int(14*s)),
            (cx - int(22*s), y_tsuba + int(26*s)),
            (cx - int(12*s), y_tsuba + int(38*s)),
            (cx - int(4*s), y_tsuba + int(50*s)),
            (cx - int(16*s), y_ground + int(6*s))
        ]
        draw_tapered_haki_lightning(d, b1, base_w=3)
        br1 = [
            (cx - int(22*s), y_tsuba + int(26*s)),
            (cx - int(34*s), y_tsuba + int(22*s)),
            (cx - int(42*s), y_tsuba + int(30*s))
        ]
        draw_tapered_haki_lightning(d, br1, base_w=2)
        draw_sparkle(d, cx - int(42*s), y_tsuba + int(30*s), r=int(6*s), color=(255, 255, 255))
        draw_sparkle(d, cx - int(16*s), y_ground + int(6*s), r=int(7*s), color=(255, 40, 80))

    elif phase == 1:
        # Phase 1: Sét Haki giật sang cánh phải, ngoằn ngoèo xé toạc thân kiếm
        b2 = [
            (cx - int(4*s), y_tsuba + int(6*s)),
            (cx + int(12*s), y_tsuba + int(18*s)),
            (cx + int(26*s), y_tsuba + int(14*s)),
            (cx + int(16*s), y_tsuba + int(32*s)),
            (cx + int(28*s), y_tsuba + int(46*s)),
            (cx + int(14*s), y_ground - int(2*s)),
            (cx + int(26*s), y_ground + int(8*s))
        ]
        draw_tapered_haki_lightning(d, b2, base_w=3)
        br2 = [
            (cx + int(12*s), y_tsuba + int(18*s)),
            (cx + int(30*s), y_tsuba + int(6*s))
        ]
        draw_tapered_haki_lightning(d, br2, base_w=2)
        draw_sparkle(d, cx + int(30*s), y_tsuba + int(6*s), r=int(6*s), color=(255, 255, 255))
        draw_sparkle(d, cx + int(26*s), y_ground + int(8*s), r=int(8*s), color=(255, 255, 255))

    elif phase == 2:
        # Phase 2: BỘC PHÁ CẢ 2 BÊN - SẤM SÉT BÁ VƯƠNG BÙNG NỔ DỮ DỘI
        b_left = [
            (cx - int(2*s), y_tsuba + int(4*s)),
            (cx - int(16*s), y_tsuba + int(20*s)),
            (cx - int(28*s), y_tsuba + int(34*s)),
            (cx - int(14*s), y_ground + int(4*s))
        ]
        draw_tapered_haki_lightning(d, b_left, base_w=3)
        b_right = [
            (cx + int(3*s), y_tsuba + int(8*s)),
            (cx + int(18*s), y_tsuba + int(24*s)),
            (cx + int(32*s), y_tsuba + int(38*s)),
            (cx + int(20*s), y_ground + int(8*s))
        ]
        draw_tapered_haki_lightning(d, b_right, base_w=3)
        b_mid = [
            (cx - int(16*s), y_tsuba + int(20*s)),
            (cx, y_tsuba + int(26*s)),
            (cx + int(18*s), y_tsuba + int(24*s))
        ]
        draw_tapered_haki_lightning(d, b_mid, base_w=2)
        draw_sparkle(d, cx - int(28*s), y_tsuba + int(34*s), r=int(7*s), color=(255, 255, 255))
        draw_sparkle(d, cx + int(32*s), y_tsuba + int(38*s), r=int(7*s), color=(255, 30, 80))
        draw_sparkle(d, cx, y_tsuba + int(26*s), r=int(9*s), color=(255, 255, 255))

    elif phase == 3:
        # Phase 3: Sét Haki cuộn xoắn từ chuôi xuống tận mũi kiếm + tia hồ quang vòng
        b3 = [
            (cx + int(5*s), y_tsuba - int(2*s)),
            (cx - int(8*s), y_tsuba + int(10*s)),
            (cx + int(16*s), y_tsuba + int(24*s)),
            (cx - int(18*s), y_tsuba + int(36*s)),
            (cx + int(8*s), y_ground + int(2*s)),
            (cx - int(4*s), y_ground + int(10*s))
        ]
        draw_tapered_haki_lightning(d, b3, base_w=3)
        br3 = [
            (cx - int(18*s), y_tsuba + int(36*s)),
            (cx - int(32*s), y_tsuba + int(44*s)),
            (cx - int(40*s), y_tsuba + int(38*s))
        ]
        draw_tapered_haki_lightning(d, br3, base_w=2)
        draw_sparkle(d, cx - int(40*s), y_tsuba + int(38*s), r=int(6*s), color=(255, 255, 255))
        draw_sparkle(d, cx + int(5*s), y_tsuba - int(2*s), r=int(7*s), color=(255, 40, 80))
        draw_sparkle(d, cx - int(4*s), y_ground + int(10*s), r=int(8*s), color=(255, 255, 255))

    return im

def draw_massive_climax_crater(w=336, h=136):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 256.0
    rx, ry = int(98 * s), int(28 * s)

    d.ellipse([cx - rx - 7, cy - ry - 5, cx + rx + 7, cy + ry + 7], fill=(16, 8, 4, 240))
    d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], fill=(8, 3, 1, 255))
    d.ellipse([cx - int(rx*0.65), cy - int(ry*0.65), cx + int(rx*0.65), cy + int(ry*0.65)], fill=(0, 0, 0, 255))
    d.arc([cx - rx, cy - ry, cx + rx, cy + ry], start=0, end=180, fill=(100, 65, 45, 255), width=int(6*s))

    for pts in [
        [(-rx, 0), (-rx - int(24*s), -int(10*s)), (-rx - int(44*s), -int(4*s))],
        [(rx, 0), (rx + int(24*s), -int(8*s)), (rx + int(44*s), int(4*s))],
        [(-int(rx*0.6), ry), (-int(rx*0.8), ry + int(14*s)), (-int(rx*1.0), ry + int(20*s))],
        [(int(rx*0.6), ry), (int(rx*0.8), ry + int(14*s)), (int(rx*1.0), ry + int(20*s))],
    ]:
        for i in range(len(pts)-1):
            p1 = (cx + pts[i][0], cy + pts[i][1])
            p2 = (cx + pts[i+1][0], cy + pts[i+1][1])
            d.line([p1, p2], fill=(0, 255, 240, 230), width=int(4*s))
            d.line([p1, p2], fill=(255, 255, 255, 255), width=2)

    rocks = [
        [(cx - int(85*s), cy - int(35*s)), int(14*s), (130, 85, 55)],
        [(cx - int(45*s), cy - int(50*s)), int(18*s), (150, 100, 65)],
        [(cx + int(45*s), cy - int(52*s)), int(18*s), (150, 100, 65)],
        [(cx + int(85*s), cy - int(36*s)), int(15*s), (130, 85, 55)],
        [(cx - int(110*s), cy - int(20*s)), int(12*s), (110, 75, 50)],
        [(cx + int(110*s), cy - int(22*s)), int(12*s), (110, 75, 50)],
    ]
    for (bx, by), sz, col in rocks:
        d.polygon([(bx - sz, by), (bx - sz//2, by - sz), (bx + sz//2, by - sz//2), (bx + sz, by + sz//3), (bx, by + sz)], fill=col, outline=(30, 18, 12, 255))
        d.line([(bx - sz//2, by - sz), (bx + sz//2, by - sz//2)], fill=(220, 190, 160, 255), width=int(3*s))

    draw_sparkle(d, cx, cy, r=int(18*s), color=(255, 255, 255, 255))
    return im

def draw_ground_shockwave(w=216, h=88):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 160.0
    rx, ry = int(74 * s), int(26 * s)
    d.ellipse([cx - rx - 5, cy - ry - 5, cx + rx + 5, cy + ry + 5], fill=(0, 200, 255, 30))
    d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], outline=(0, 229, 255, 180), width=int(4*s))
    d.ellipse([cx - int(rx*0.75), cy - int(ry*0.75), cx + int(rx*0.75), cy + int(ry*0.75)], outline=(0, 255, 240, 220), width=int(3*s))
    d.ellipse([cx - int(rx*0.45), cy - int(ry*0.45), cx + int(rx*0.45), cy + int(ry*0.45)], outline=(255, 255, 255, 255), width=2)
    return im

def draw_slash_burst(w=216, h=192):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 160.0
    
    # 1. Các vệt chém nứt rách không gian mang năng lượng Room & Haki
    d.line([(cx - int(56*s), cy - int(40*s)), (cx + int(56*s), cy + int(40*s))], fill=(0, 229, 255, 180), width=int(10*s))
    d.line([(cx - int(56*s), cy - int(40*s)), (cx + int(56*s), cy + int(40*s))], fill=(255, 255, 255, 255), width=int(4*s))
    d.line([(cx - int(50*s), cy + int(42*s)), (cx + int(50*s), cy - int(42*s))], fill=(255, 20, 60, 180), width=int(8*s))
    d.line([(cx - int(50*s), cy + int(42*s)), (cx + int(50*s), cy - int(42*s))], fill=(255, 255, 255, 255), width=3)
    
    # 2. Tia sét Haki nổ bùng xé toạc vết chém
    bolt_s1 = [
        (cx - int(46*s), cy - int(32*s)),
        (cx - int(20*s), cy - int(10*s)),
        (cx, cy),
        (cx + int(24*s), cy + int(14*s)),
        (cx + int(50*s), cy + int(36*s))
    ]
    draw_tapered_haki_lightning(d, bolt_s1, base_w=3)
    
    bolt_s2 = [
        (cx - int(38*s), cy + int(30*s)),
        (cx - int(14*s), cy + int(8*s)),
        (cx, cy),
        (cx + int(18*s), cy - int(16*s)),
        (cx + int(42*s), cy - int(34*s))
    ]
    draw_tapered_haki_lightning(d, bolt_s2, base_w=3)

    draw_sparkle(d, cx, cy, r=int(24*s), color=(255, 255, 255, 255))
    r_center = int(12 * s)
    d.ellipse([cx - r_center, cy - r_center, cx + r_center, cy + r_center], fill=(255, 255, 255, 240))
    for dx, dy in [(-30, -20), (32, 18), (-22, 28), (28, -25)]:
        draw_sparkle(d, cx + int(dx*s), cy + int(dy*s), r=int(9*s), color=(255, 40, 90, 230))
    return im

def draw_dissolve_sparks(w=176, h=176):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 144.0
    sparks = [
        (0, 0, int(16*s), (255, 255, 255, 255)),
        (int(-35*s), int(-25*s), int(10*s), (0, 240, 255, 240)),
        (int(38*s), int(-20*s), int(12*s), (128, 255, 255, 240)),
        (int(-25*s), int(28*s), int(11*s), (0, 229, 255, 220)),
        (int(32*s), int(26*s), int(9*s), (0, 255, 210, 220)),
        (int(-12*s), int(-40*s), int(8*s), (255, 255, 255, 200)),
        (int(16*s), int(40*s), int(8*s), (0, 240, 255, 200))
    ]
    for sx, sy, sr, col in sparks:
        draw_sparkle(d, cx + sx, cy + sy, r=sr, color=col)
    return im

def create_effect_914_room_slash():
    scale = 4
    w_sheet = 248 * scale
    h_sheet = 290 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    im_room             = draw_anime_hemisphere_room_dome(248*scale, 152*scale)
    im_giant_sword      = draw_straight_kikoku_sword(46*scale, 100*scale, is_trail=True, is_giant=True)
    im_sword_crater_p0  = draw_sword_stuck_in_crater(30*scale, 46*scale, phase=0)
    im_sword_crater_p1  = draw_sword_stuck_in_crater(30*scale, 46*scale, phase=1)
    im_sword_crater_p2  = draw_sword_stuck_in_crater(30*scale, 46*scale, phase=2)
    im_sword_crater_p3  = draw_sword_stuck_in_crater(30*scale, 46*scale, phase=3)
    im_sword_trail      = draw_straight_kikoku_sword(22*scale, 56*scale, is_trail=True)

    im_crater1          = draw_ground_crater_and_debris(52*scale, 24*scale, phase=0)
    im_crater2          = draw_ground_crater_and_debris(58*scale, 28*scale, phase=1)
    im_shockwave        = draw_ground_shockwave(54*scale, 22*scale)

    im_massive_crater   = draw_massive_climax_crater(84*scale, 34*scale)
    im_slash_burst      = draw_slash_burst(54*scale, 48*scale)
    im_dissolve         = draw_dissolve_sparks(44*scale, 44*scale)

    im_smoke_puff1      = draw_circling_smoke_puff(32*scale, 18*scale, variant=0)
    im_smoke_puff2      = draw_circling_smoke_puff(34*scale, 20*scale, variant=1)

    # Bố trí Sprite vào Sheet
    im.paste(im_room,             (0*scale,   0*scale),   im_room)
    im.paste(im_giant_sword,      (0*scale,   154*scale), im_giant_sword)
    im.paste(im_sword_crater_p1,  (48*scale,  154*scale), im_sword_crater_p1) # 1: 48, 154, 30, 46
    im.paste(im_sword_trail,      (78*scale,  154*scale), im_sword_trail)     # 2: 78, 154, 22, 56
    im.paste(im_sword_crater_p0,  (100*scale, 154*scale), im_sword_crater_p0) # 5: 100, 154, 30, 46
    im.paste(im_massive_crater,   (130*scale, 154*scale), im_massive_crater)  # 8: 130, 154, 84, 34
    im.paste(im_smoke_puff1,      (214*scale, 154*scale), im_smoke_puff1)     # 11: 214, 154, 32, 18
    im.paste(im_smoke_puff2,      (214*scale, 174*scale), im_smoke_puff2)     # 12: 214, 174, 34, 20
    im.paste(im_slash_burst,      (130*scale, 190*scale), im_slash_burst)     # 9: 130, 190, 54, 48
    im.paste(im_dissolve,         (186*scale, 196*scale), im_dissolve)        # 10: 186, 196, 44, 44
    im.paste(im_crater2,          (48*scale,  212*scale), im_crater2)         # 4: 48, 212, 58, 28
    im.paste(im_shockwave,        (48*scale,  242*scale), im_shockwave)       # 6: 48, 242, 54, 22
    im.paste(im_crater1,          (104*scale, 242*scale), im_crater1)         # 3: 104, 242, 52, 24
    im.paste(im_sword_crater_p2,  (160*scale, 242*scale), im_sword_crater_p2) # 13: 160, 242, 30, 46
    im.paste(im_sword_crater_p3,  (192*scale, 242*scale), im_sword_crater_p3) # 14: 192, 242, 30, 46

    small_imgs = [
        (0,    0,   0, 248, 152), # Sprite 0: Vòm Nửa Hình Cầu Room
        (1,   48, 154,  30,  46), # Sprite 1: Kiếm cắm đất Phase 1 (Sét Haki giật sang phải)
        (2,   78, 154,  22,  56), # Sprite 2: Kiếm Kikoku rơi thẳng xé gió
        (3,  104, 242,  52,  24), # Sprite 3: Hố lún đất + đá văng pha 1
        (4,   48, 212,  58,  28), # Sprite 4: Hố lún đất + đá văng bùng nổ pha 2
        (5,  100, 154,  30,  46), # Sprite 5: Kiếm cắm đất Phase 0 (Sét Haki cuộn cánh trái)
        (6,   48, 242,  54,  22), # Sprite 6: Sóng chấn động lún đất
        (7,    0, 154,  46, 100), # Sprite 7: Đại Thần Kiếm khổng lồ uy lực rơi thẳng
        (8,  130, 154,  84,  34), # Sprite 8: Đại hố lún cực đại
        (9,  130, 190,  54,  48), # Sprite 9: Vết chém nứt rách không gian
        (10, 186, 196,  44,  44), # Sprite 10: Tinh thể tan biến
        (11, 214, 154,  32,  18), # Sprite 11: Khói bụi xoáy tròn chân đế 1
        (12, 214, 174,  34,  20), # Sprite 12: Khói bụi xoáy tròn chân đế 2
        (13, 160, 242,  30,  46), # Sprite 13: Kiếm cắm đất Phase 2 (Song tia sét Bá Vương bộc phá)
        (14, 192, 242,  30,  46), # Sprite 14: Kiếm cắm đất Phase 3 (Sét Haki xoắn ốc + hồ quang)
    ]

    sword_coords = [
        (-31, -35),
        (-75, -35),
        ( 29, -35),
        (-103, -35),
        ( 57, -35),
        (-43, -35),
        (  5, -35),
        (-63, -35)
    ]
    phases = [5, 1, 13, 14]

    def make_swords(count, step):
        parts = []
        for i in range(count):
            sx, sy = sword_coords[i]
            p_idx = (i + step) % 4
            sp_id = phases[p_idx]
            fl = 1 if (i + step) % 2 == 1 else 0
            parts.append((sx, sy, sp_id, fl, 1))
        return parts

    raw_frames = [
        # Frame 0: Vòm Room mở ra (0.0s)
        [(-124, -129, 0, 0, 0)],
        
        # Frame 1: Vòm Room + Đợt 1: Kiếm 1 & 2 lao nhanh từ trên trời (0.08s)
        [
            (-124, -129, 0, 0, 0),
            ( -27, -100, 2, 0, 1),
            ( -71, -100, 2, 0, 1),
        ],
        
        # Frame 2: Kiếm 1 & 2 CẮM ĐẤT! Nổ hố lún + Đợt 2: Kiếm 3 & 4 lao nhanh (0.16s)
        [(-124, -129, 0, 0, 0)] + make_swords(2, 0) + [
            ( -42,  -12, 3, 0, 1),
            ( -43,  -11, 6, 0, 1),
            ( -86,  -12, 3, 0, 1),
            (  33, -100, 2, 0, 1),
            ( -99, -100, 2, 0, 1),
        ],
        
        # Frame 3: Kiếm 3 & 4 CẮM ĐẤT! (Sét Haki di chuyển động liên hoàn) + Đợt 3 lao nhanh (0.28s)
        [(-124, -129, 0, 0, 0)] + make_swords(4, 1) + [
            (  15,  -14, 4, 0, 1),
            (-117,  -14, 4, 0, 1),
            (  61, -100, 2, 0, 1),
            ( -39, -100, 2, 0, 1),
        ],
        
        # Frame 4: Kiếm 5 & 6 CẮM ĐẤT! (Sét Haki tiếp tục di chuyển nhảy pha) + Đợt 4 lao nhanh (0.40s)
        [(-124, -129, 0, 0, 0)] + make_swords(6, 2) + [
            (  46,  -12, 3, 0, 1),
            ( -54,  -12, 3, 0, 1),
            (   9, -100, 2, 0, 1),
            ( -59, -100, 2, 0, 1),
        ],
        
        # Frame 5a: Kiếm 7 & 8 CẮM ĐẤT! Cả 8 kiếm cắm hoàn tất, Haki sét rực sáng pha 3 (0.52s)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 3) + [
            (  -9,  -14, 4, 0, 1),
            ( -77,  -14, 4, 0, 1),
        ],

        # Frame 5b: Cả 8 kiếm Haki sét giật nhảy pha 4 (nhấp nháy sống động)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 4) + [
            (  -9,  -14, 4, 0, 1),
            ( -77,  -14, 4, 0, 1),
        ],
        
        # Frame 6a: Chấn động cực đại! 8 cây kiếm Haki giật nổ pha 5 (0.64s)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 5) + [
            ( -42,  -14, 4, 0, 1),
            (  15,  -14, 4, 0, 1),
            ( -43,  -11, 6, 0, 1),
        ],

        # Frame 6b: 8 cây kiếm Haki giật nổ pha 6
        [(-124, -129, 0, 0, 0)] + make_swords(8, 6) + [
            ( -42,  -14, 4, 0, 1),
            (  15,  -14, 4, 0, 1),
            ( -43,  -11, 6, 0, 1),
        ],
        
        # Frame 7a: ĐẠI THẦN KIẾM xuất hiện! 8 kiếm Haki giật pha 7 (0.76s)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 7) + [
            ( -33, -150, 7, 0, 1),
        ],

        # Frame 7b: Đại Thần Kiếm tích tụ Haki giật sang phải! 8 kiếm Haki giật pha 8
        [(-124, -129, 0, 0, 0)] + make_swords(8, 8) + [
            ( -33, -150, 7, 1, 1),
        ],
        
        # Frame 8a: Đại Thần Kiếm lao nhanh xé gió! 8 kiếm Haki giật pha 9 (0.88s)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 9) + [
            ( -33, -110, 7, 0, 1),
        ],

        # Frame 8b: Đại Thần Kiếm lao nhanh xé gió! 8 kiếm Haki giật pha 10
        [(-124, -129, 0, 0, 0)] + make_swords(8, 10) + [
            ( -33, -110, 7, 1, 1),
        ],
        
        # Frame 9: Đại Thần Kiếm sát mặt đất + Chấn động! 8 kiếm Haki giật pha 11 (1.00s)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 11) + [
            ( -33,  -85, 7, 0, 1),
            ( -43,  -11, 6, 0, 1),
        ],
        
        # Frame 10a: ĐẠI THẦN KIẾM CẮM ĐẤT! Đại hố lún nứt toác! 8 kiếm Haki giật pha 12 (1.12s)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 12) + [
            ( -52,  -17, 8, 0, 1),
            ( -33,  -75, 7, 0, 1),
            ( -37,  -40, 9, 0, 1),
        ],

        # Frame 10b: Đại Thần Kiếm cắm đất chấn động giật sét! 8 kiếm Haki giật pha 13
        [(-124, -129, 0, 0, 0)] + make_swords(8, 13) + [
            ( -52,  -17, 8, 0, 1),
            ( -33,  -75, 7, 1, 1),
            ( -37,  -40, 9, 0, 1),
        ],
        
        # Frame 11a: Toàn bộ Room nổ bùng chói lọi! 8 kiếm Haki giật pha 14 (1.24s)
        [(-124, -129, 0, 0, 0)] + make_swords(8, 14) + [
            ( -52,  -17, 8, 0, 1),
            ( -33,  -75, 7, 0, 1),
            ( -37,  -40, 9, 0, 1),
            ( -43,  -11, 6, 0, 1),
        ],

        # Frame 11b: Toàn bộ Room nổ bùng chấn động! 8 kiếm Haki giật pha 15
        [(-124, -129, 0, 0, 0)] + make_swords(8, 15) + [
            ( -52,  -17, 8, 0, 1),
            ( -33,  -75, 7, 1, 1),
            ( -37,  -40, 9, 0, 1),
            ( -43,  -11, 6, 0, 1),
        ],
        
        # Frame 12: TOÀN BỘ CÁC CÂY KIẾM BIẾN MẤT! Không gian vỡ vụn (1.36s)
        [
            (-124, -129, 0, 0, 0),
            ( -37,  -40, 9, 0, 1),
            ( -52,  -22, 10, 0, 1),
            (   0,  -22, 10, 0, 1),
            (  36,  -22, 10, 0, 1),
        ],
        
        # Frame 13: Vòm Room vỡ vụn + Tinh thể không gian lấp lánh (1.48s)
        [
            ( -42,  -24, 10, 0, 1),
            (  -6,  -28, 10, 0, 1),
            (  30,  -24, 10, 0, 1),
        ],
        
        # Frame 14: Tinh thể bay tỏa nhẹ (1.60s)
        [
            ( -32,  -30, 10, 0, 1),
            (  24,  -32, 10, 0, 1),
        ],
        
        # Frame 15: Tia sáng cuối cùng tan biến (1.72s - 1.80s)
        [
            (  -4,  -34, 10, 0, 1),
        ],
    ]

    # Cung elip đáy Room (rx=115, ry=11)
    rx_e, ry_e = 115.0, 11.0
    num_samples = 1000
    angles = [2 * math.pi * i / num_samples for i in range(num_samples + 1)]
    arc_lens = [0.0]
    total_len = 0.0
    for i in range(num_samples):
        t1, t2 = angles[i], angles[i+1]
        x1, y1 = rx_e * math.cos(t1), ry_e * math.sin(t1)
        x2, y2 = rx_e * math.cos(t2), ry_e * math.sin(t2)
        total_len += math.hypot(x2 - x1, y2 - y1)
        arc_lens.append(total_len)

    def get_ellipse_point(target_s):
        s = target_s % total_len
        for idx in range(len(arc_lens) - 1):
            if arc_lens[idx] <= s <= arc_lens[idx+1]:
                frac = (s - arc_lens[idx]) / (arc_lens[idx+1] - arc_lens[idx] + 1e-9)
                t = angles[idx] + frac * (angles[idx+1] - angles[idx])
                return rx_e * math.cos(t), ry_e * math.sin(t), math.sin(t)
        return rx_e, 0.0, 0.0

    N_puffs = 22
    frames = []
    for f_idx, rf in enumerate(raw_frames):
        f_list = list(rf)
        if f_idx <= 17:
            offset_s = (f_idx * (total_len / 18.0)) % total_len
            for k in range(N_puffs):
                target_s = (k / N_puffs) * total_len + offset_s
                ex, ey, sin_t = get_ellipse_point(target_s)
                sp_id = 11 if k % 2 == 0 else 12
                px = int(round(ex)) - (16 if k % 2 == 0 else 17)
                py = int(round(11 + ey)) - (9 if k % 2 == 0 else 10)
                layer = 0 if sin_t < 0 else 1
                f_list.append((px, py, sp_id, 0, layer))
        frames.append(f_list)

    seq = (
        [0] * 3 +       # 3 ticks (0.12s)
        [1] * 3 +       # 3 ticks (0.12s)
        [2] * 5 +       # 5 ticks (0.20s)
        [3] * 5 +       # 5 ticks (0.20s)
        [4] * 5 +       # 5 ticks (0.20s)
        [5, 6, 5, 6, 5] +   # 5 ticks: 8 kiếm Haki sét di chuyển nhảy pha liên hoàn!
        [7, 8, 7, 8, 7, 8] + # 6 ticks: Dư chấn rung chuyển, Haki sét crackling liên tục!
        [9, 10, 9, 10, 9, 10] + # 6 ticks: Đại thần kiếm xuất hiện tích tụ Haki chớp giật!
        [11, 12, 11, 12] +      # 4 ticks: Đại thần kiếm lao nhanh xé gió
        [13] * 4 +              # 4 ticks: Đại thần kiếm sát đất
        [14, 15, 14, 15, 14, 15, 14, 15] + # 8 ticks: Đại thần kiếm cắm đất, Haki nổ bùng chấn động!
        [16, 17, 16, 17, 16, 17, 16, 17] + # 8 ticks: Toàn bộ Room nổ bùng, sấm sét Bá Vương cuộn xoáy!
        [18] * 5 +      # 5 ticks: Kiếm tan vỡ
        [19] * 4 +      # 4 ticks: Vòm tan biến
        [20] * 3 +      # 3 ticks: Tinh thể tỏa
        [21] * 1        # 1 tick: Hết
    )

    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(914, im, data_bytes)

# ==============================================================================
# 2. TẠO EFFECT 915: VÒNG XOÁY DAO PHÓNG XẠ GAMMA (GAMMA KNIFE OBSIDIAN VORTEX)
# Chuẩn 100% hình ảnh mẫu người dùng + TIA SÉT TỪ TRỜI GIÁNG XUỐNG + CHỚP TOÀN MÀN HÌNH:
# - Vành đai 10 khối đao Hắc Diện Thạch đa giác 3D góc cạnh sắc bén
# - Vòng sấm sét Cyan / Neon Electric Blue cuộn xoáy tròn bao quanh
# - Lõi cực quang và 32 chùm tia sáng nan hoa (Sunburst Beams) xuyên tâm
# - Cột sét siêu thực từ trên trời đánh thẳng xuống tâm vòng xoáy
# - Chớp sáng phủ kín toàn màn hình (Full Screen Flash) khi bộc phá nổ cực hạn
# ==============================================================================

def draw_gamma_vortex_base_and_rays(w=520, h=416):
    """
    Sprite 0: Vòng xoáy Năng Lượng & Chùm Tia Sáng Nan Hoa (130x104 ở 1x -> 520x416 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    cx, cy = w // 2, h // 2
    rx, ry = int(w * 0.45), int(h * 0.42)
    
    # 1. Đĩa chấn động bụi cát & hào quang nền
    d = ImageDraw.Draw(im)
    for dr in range(16, 0, -3):
        alpha = int(35 * (1.0 - dr / 16.0))
        d.ellipse([cx - rx - dr, cy - ry - dr, cx + rx + dr, cy + ry + dr], outline=(170, 145, 105, alpha), width=3)
    d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], outline=(0, 220, 255, 120), width=4)
    
    # 2. Hào quang xanh dịu
    glow_im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d_g = ImageDraw.Draw(glow_im)
    for r_f in np.linspace(1.0, 0.15, 10):
        cur_rx = int(rx * r_f)
        cur_ry = int(ry * r_f)
        alpha = int(40 * (1.1 - r_f))
        d_g.ellipse([cx - cur_rx, cy - cur_ry, cx + cur_rx, cy + cur_ry], fill=(0, 175, 255, alpha))
    glow_im = glow_im.filter(ImageFilter.GaussianBlur(12))
    im.alpha_composite(glow_im)
    
    # 3. 32 Chùm tia sáng nan hoa
    d = ImageDraw.Draw(im)
    num_rays = 32
    np.random.seed(77)
    for i in range(num_rays):
        angle = 2 * math.pi * i / num_rays + np.random.uniform(-0.03, 0.03)
        cos_a = math.cos(angle)
        sin_a = math.sin(angle)
        ray_len = np.random.uniform(0.70, 0.98)
        ex = cx + rx * cos_a * ray_len
        ey = cy + ry * sin_a * ray_len
        w_ray = np.random.uniform(3.0, 7.5)
        perp_x = -sin_a * w_ray
        perp_y = cos_a * w_ray * 0.85
        poly = [
            (cx - perp_x * 0.25, cy - perp_y * 0.25),
            (cx + perp_x * 0.25, cy + perp_y * 0.25),
            (ex + perp_x * 0.05, ey + perp_y * 0.05),
            (ex, ey),
            (ex - perp_x * 0.05, ey - perp_y * 0.05)
        ]
        alpha = int(np.random.uniform(140, 230))
        d.polygon(poly, fill=(0, 235, 255, alpha))
        if w_ray > 4.5:
            poly_core = [
                (cx, cy),
                (cx + perp_x * 0.12, cy + perp_y * 0.12),
                (ex * 0.75 + cx * 0.25, ey * 0.75 + cy * 0.25),
                (cx - perp_x * 0.12, cy - perp_y * 0.12)
            ]
            d.polygon(poly_core, fill=(255, 255, 255, int(alpha * 0.9)))

    # 4. Lõi cực quang chói lòa
    core_layer = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d_c = ImageDraw.Draw(core_layer)
    for r_c, a_c in [(55, 80), (38, 140), (22, 220)]:
        d_c.ellipse([cx - r_c, cy - int(r_c * 0.85), cx + r_c, cy + int(r_c * 0.85)], fill=(0, 230, 255, a_c))
    core_layer = core_layer.filter(ImageFilter.GaussianBlur(6))
    im.alpha_composite(core_layer)
    
    d.ellipse([cx - 18, cy - 14, cx + 18, cy + 14], fill=(235, 250, 255, 250))
    d.ellipse([cx - 10, cy - 8, cx + 10, cy + 8], fill=(255, 255, 255, 255))
    draw_sparkle(d, cx, cy, r=22, color=(255, 255, 255, 255))
    return im

def draw_gamma_lightning_ring(w=520, h=416):
    """
    Sprite 1: Vành Đai Sét Hồ Quang Cuộn Tròn (130x104 ở 1x -> 520x416 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    cx, cy = w // 2, h // 2
    rx, ry = int(w * 0.44), int(h * 0.42)
    np.random.seed(101)
    glow_im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d_g = ImageDraw.Draw(glow_im)
    d = ImageDraw.Draw(im)
    
    for ring_i in range(3):
        offset = (ring_i - 1) * 8
        cur_rx = rx + offset
        cur_ry = ry + offset * 0.92
        num_pts = 42
        pts = []
        for k in range(num_pts + 1):
            theta = 2 * math.pi * k / num_pts
            jx = np.random.uniform(-5, 5)
            jy = np.random.uniform(-5, 5)
            px = cx + (cur_rx + jx) * math.cos(theta)
            py = cy + (cur_ry + jy) * math.sin(theta)
            pts.append((px, py))
        for k in range(len(pts) - 1):
            d_g.line([pts[k], pts[k+1]], fill=(0, 210, 255, 180), width=12)
            d.line([pts[k], pts[k+1]], fill=(0, 245, 255, 230), width=5)
            d.line([pts[k], pts[k+1]], fill=(255, 255, 255, 255), width=2)
            
    glow_im = glow_im.filter(ImageFilter.GaussianBlur(8))
    im = Image.alpha_composite(glow_im, im)
    d = ImageDraw.Draw(im)
    
    for k in range(12):
        theta = 2 * math.pi * k / 12 + np.random.uniform(-0.1, 0.1)
        sx = cx + rx * math.cos(theta)
        sy = cy + ry * math.sin(theta)
        d_mult = -1.0 if (k % 2 == 0) else 0.7
        l_sp = np.random.uniform(28, 55)
        mx = sx + math.cos(theta) * l_sp * 0.5 * d_mult + np.random.uniform(-10, 10)
        my = sy + math.sin(theta) * l_sp * 0.5 * d_mult + np.random.uniform(-10, 10)
        ex = sx + math.cos(theta) * l_sp * d_mult + np.random.uniform(-12, 12)
        ey = sy + math.sin(theta) * l_sp * d_mult + np.random.uniform(-12, 12)
        d.line([(sx, sy), (mx, my), (ex, ey)], fill=(0, 240, 255, 220), width=3)
        d.line([(sx, sy), (mx, my), (ex, ey)], fill=(255, 255, 255, 255), width=1)
        draw_sparkle(d, int(ex), int(ey), r=5, color=(255, 255, 255, 255))
    return im

def draw_gamma_sky_lightning(w=176, h=560, seed_val=404):
    """
    Sprite 2: Tia Sét Từ Trời Giáng Xuống Liền Mạch (44x140 ở 1x -> 176x560 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    cx = w // 2
    np.random.seed(seed_val)
    nodes = [
        (cx + np.random.uniform(-8, 8), 0),
        (cx + np.random.uniform(-28, -10), int(h * 0.20)),
        (cx + np.random.uniform(10, 30), int(h * 0.40)),
        (cx + np.random.uniform(-30, -12), int(h * 0.60)),
        (cx + np.random.uniform(12, 28), int(h * 0.80)),
        (cx + np.random.uniform(-10, 8), int(h * 0.95)),
        (cx, h)
    ]
    full_path = [nodes[0]]
    for i in range(len(nodes) - 1):
        p1, p2 = nodes[i], nodes[i+1]
        mid1 = ((p1[0] + p2[0])/2.0 + np.random.uniform(-12, 12), (p1[1] + p2[1])/2.0)
        sub1 = ((p1[0] + mid1[0])/2.0 + np.random.uniform(-8, 8), (p1[1] + mid1[1])/2.0)
        sub2 = ((mid1[0] + p2[0])/2.0 + np.random.uniform(-8, 8), (mid1[1] + p2[1])/2.0)
        full_path.extend([sub1, mid1, sub2, p2])
        
    glow_im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d_g = ImageDraw.Draw(glow_im)
    for i in range(len(full_path) - 1):
        d_g.line([full_path[i], full_path[i+1]], fill=(0, 200, 255, 200), width=18)
    glow_im = glow_im.filter(ImageFilter.GaussianBlur(8))
    im.alpha_composite(glow_im)
    
    d = ImageDraw.Draw(im)
    for i in range(len(full_path) - 1):
        d.line([full_path[i], full_path[i+1]], fill=(0, 235, 255, 240), width=8)
        d.line([full_path[i], full_path[i+1]], fill=(255, 255, 255, 255), width=3)
        
    for branch_i in range(3):
        idx_b = int(len(full_path) * (0.25 + branch_i * 0.28))
        bp_start = full_path[idx_b]
        b_dir = -1.0 if (branch_i % 2 == 0) else 1.0
        bp_mid = (bp_start[0] + b_dir * np.random.uniform(25, 45), bp_start[1] + np.random.uniform(20, 45))
        bp_end = (bp_mid[0] + b_dir * np.random.uniform(20, 35), bp_mid[1] + np.random.uniform(20, 40))
        d.line([bp_start, bp_mid, bp_end], fill=(0, 230, 255, 220), width=4)
        d.line([bp_start, bp_mid, bp_end], fill=(255, 255, 255, 255), width=2)
        draw_sparkle(d, int(bp_end[0]), int(bp_end[1]), r=6, color=(255, 255, 255, 255))
        
    return im

def draw_gamma_center_mega_flash(w=296, h=272):
    """
    Sprite 3: Chớp Sáng Cực Đại Bùng Nổ Ngay Tại Trung Tâm Chiêu (74x68 ở 1x -> 296x272 ở 4x)
    Vầng hào quang cyan bloom tỏa rộng, các tia chớp nan hoa xé toạc không gian từ tâm và lõi trắng chói lòa
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    cx, cy = w // 2, h // 2
    
    # 1. Vầng hào quang cyan bloom nở rộng
    glow = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d_g = ImageDraw.Draw(glow)
    for r_f in np.linspace(1.0, 0.2, 10):
        rx = int((w * 0.46) * r_f)
        ry = int((h * 0.44) * r_f)
        alpha = int(120 * (1.1 - r_f * 0.6))
        d_g.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], fill=(0, 220, 255, alpha))
    glow = glow.filter(ImageFilter.GaussianBlur(14))
    im.alpha_composite(glow)
    
    d = ImageDraw.Draw(im)
    
    # 2. Các tia chớp nan hoa xé toạc không gian từ tâm (Flash Spikes)
    for ang_deg in [0, 45, 90, 135, 180, 225, 270, 315]:
        ang = math.radians(ang_deg)
        cos_a, sin_a = math.cos(ang), math.sin(ang)
        r_spike = (w * 0.46) if (ang_deg % 90 == 0) else (w * 0.38)
        ex = cx + cos_a * r_spike
        ey = cy + sin_a * r_spike * 0.85
        w_b = 6.0 if (ang_deg % 90 == 0) else 4.0
        px = -sin_a * w_b
        py = cos_a * w_b
        poly = [(cx - px, cy - py), (cx + px, cy + py), (ex, ey)]
        d.polygon(poly, fill=(0, 240, 255, 230))
        d.polygon([(cx - px*0.4, cy - py*0.4), (cx + px*0.4, cy + py*0.4), (ex*0.8 + cx*0.2, ey*0.8 + cy*0.2)], fill=(255, 255, 255, 255))
        
    for i in range(16):
        ang = 2 * math.pi * i / 16.0 + 0.1
        ex = cx + math.cos(ang) * (w * 0.32)
        ey = cy + math.sin(ang) * (h * 0.28)
        d.line([(cx, cy), (ex, ey)], fill=(160, 245, 255, 220), width=2)
        draw_sparkle(d, int(ex), int(ey), r=5, color=(255, 255, 255, 255))
        
    # 3. Lõi trắng chói lòa tại trung tâm (Pure White Starburst)
    core = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d_c = ImageDraw.Draw(core)
    for r_c, a_c in [(65, 120), (45, 180), (28, 240)]:
        d_c.ellipse([cx - r_c, cy - int(r_c * 0.8), cx + r_c, cy + int(r_c * 0.8)], fill=(0, 245, 255, a_c))
    core = core.filter(ImageFilter.GaussianBlur(6))
    im.alpha_composite(core)
    
    d = ImageDraw.Draw(im)
    d.ellipse([cx - 32, cy - 24, cx + 32, cy + 24], fill=(240, 255, 255, 255))
    d.ellipse([cx - 18, cy - 14, cx + 18, cy + 14], fill=(255, 255, 255, 255))
    draw_sparkle(d, cx, cy, r=36, color=(255, 255, 255, 255))
    draw_sparkle(d, cx, cy, r=22, color=(180, 245, 255, 255))
    return im

def draw_gamma_detonation_burst(w=288, h=216):
    """
    Sprite 4: Chớp Nổ Bộc Phá Plasma Cực Đại (72x54 ở 1x -> 288x216 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    cx, cy = w // 2, h // 2
    d = ImageDraw.Draw(im)
    for r_b, a_b in [(100, 60), (70, 130), (45, 200), (24, 255)]:
        d.ellipse([cx - r_b, cy - int(r_b*0.75), cx + r_b, cy + int(r_b*0.75)], fill=(0, 235, 255, a_b))
    d.ellipse([cx - 30, cy - 22, cx + 30, cy + 22], fill=(245, 255, 255, 255))
    draw_sparkle(d, cx, cy, r=38, color=(255, 255, 255, 255))
    draw_sparkle(d, cx, cy, r=22, color=(0, 240, 255, 255))
    return im

def draw_gamma_blade_top_left(w=152, h=136):
    """
    Sprite 5: Đao Hắc Thạch Lớn Góc Trên (38x34 ở 1x -> 152x136 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    tip = (134, 120)
    p_top_e = (66, 12)
    p_top_l = (22, 34)
    p_mid_l = (12, 78)
    p_ridge_1 = (70, 50)
    p_ridge_2 = (94, 78)
    p_right_e = (124, 42)
    p_bot_l = (42, 110)
    d.polygon([p_top_l, p_mid_l, p_bot_l, tip, p_ridge_2, p_ridge_1], fill=(16, 20, 26, 255))
    d.polygon([p_mid_l, p_bot_l, tip], fill=(10, 14, 20, 255))
    d.polygon([p_top_l, p_top_e, p_ridge_1], fill=(38, 46, 60, 255))
    d.polygon([p_bot_l, tip, p_ridge_2], fill=(26, 32, 42, 255))
    d.polygon([p_top_e, p_right_e, p_ridge_2, p_ridge_1], fill=(74, 88, 112, 255))
    d.polygon([p_right_e, tip, p_ridge_2], fill=(106, 124, 150, 255))
    d.line([p_top_e, p_ridge_1, p_ridge_2, tip], fill=(155, 180, 210, 255), width=3)
    d.line([p_right_e, tip], fill=(185, 215, 240, 255), width=2)
    d.line([tip, p_right_e, p_top_e], fill=(0, 235, 255, 220), width=3)
    d.line([tip, p_bot_l], fill=(0, 180, 240, 150), width=2)
    draw_sparkle(d, tip[0], tip[1], r=7, color=(255, 255, 255, 255))
    return im

def draw_gamma_blade_bottom_left(w=152, h=136):
    """
    Sprite 6: Đao Hắc Thạch Lớn Góc Dưới (38x34 ở 1x -> 152x136 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    tip = (136, 16)
    p_bot_e = (62, 122)
    p_bot_l = (18, 92)
    p_mid_l = (10, 52)
    p_ridge_1 = (72, 78)
    p_ridge_2 = (96, 48)
    p_right_b = (120, 90)
    p_top_l = (46, 20)
    d.polygon([p_mid_l, p_bot_l, p_bot_e, p_right_b, p_ridge_1], fill=(14, 18, 24, 255))
    d.polygon([p_mid_l, p_top_l, tip, p_ridge_2, p_ridge_1], fill=(22, 28, 38, 255))
    d.polygon([p_top_l, tip, p_ridge_2], fill=(66, 80, 102, 255))
    d.polygon([p_ridge_1, p_ridge_2, tip, p_right_b], fill=(94, 112, 138, 255))
    d.line([p_bot_e, p_ridge_1, p_ridge_2, tip], fill=(150, 175, 205, 255), width=3)
    d.line([tip, p_top_l], fill=(0, 240, 255, 230), width=3)
    d.line([tip, p_right_b], fill=(0, 190, 245, 160), width=2)
    draw_sparkle(d, tip[0], tip[1], r=7, color=(255, 255, 255, 255))
    return im

def draw_gamma_blade_vertical(w=144, h=128):
    """
    Sprite 7: Đao Hắc Thạch Dựng Đứng (36x32 ở 1x -> 144x128 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    tip = (72, 118)
    p_top_l = (18, 14)
    p_top_r = (126, 14)
    p_top_m = (72, 8)
    p_ridge = (72, 64)
    p_mid_l = (24, 60)
    p_mid_r = (120, 60)
    d.polygon([p_top_l, p_mid_l, tip, p_ridge], fill=(20, 26, 36, 255))
    d.polygon([p_mid_l, p_top_l, p_top_m, p_ridge], fill=(34, 42, 56, 255))
    d.polygon([p_top_m, p_top_r, p_mid_r, p_ridge], fill=(72, 86, 110, 255))
    d.polygon([p_mid_r, tip, p_ridge], fill=(100, 118, 144, 255))
    d.line([p_top_m, p_ridge, tip], fill=(155, 180, 210, 255), width=3)
    d.line([tip, p_mid_r, p_top_r], fill=(0, 235, 255, 220), width=2)
    d.line([tip, p_mid_l], fill=(0, 180, 240, 160), width=2)
    draw_sparkle(d, tip[0], tip[1], r=6, color=(255, 255, 255, 255))
    return im

def draw_gamma_blade_side(w=144, h=104):
    """
    Sprite 8: Đao Hắc Thạch Nằm Ngang (36x26 ở 1x -> 144x104 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    tip = (136, 52)
    p_back_t = (14, 18)
    p_back_b = (16, 86)
    p_back_m = (8, 52)
    p_facet_t = (76, 20)
    p_facet_b = (78, 84)
    p_ridge = (80, 52)
    d.polygon([p_back_m, p_back_b, p_facet_b, tip, p_ridge], fill=(16, 20, 28, 255))
    d.polygon([p_back_m, p_back_t, p_facet_t, p_ridge], fill=(46, 56, 74, 255))
    d.polygon([p_facet_t, tip, p_ridge], fill=(95, 112, 140, 255))
    d.polygon([p_facet_b, tip, p_ridge], fill=(30, 38, 50, 255))
    d.line([p_back_m, p_ridge, tip], fill=(155, 180, 210, 255), width=3)
    d.line([tip, p_facet_t, p_back_t], fill=(0, 235, 255, 220), width=2)
    draw_sparkle(d, tip[0], tip[1], r=6, color=(255, 255, 255, 255))
    return im

def draw_gamma_fragment(w=72, h=64):
    """
    Sprite 9: Mảnh Vỡ Tinh Thể Lơ Lửng (18x16 ở 1x -> 72x64 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    pts = [(12, 32), (36, 12), (60, 26), (50, 52), (22, 48)]
    d.polygon(pts, fill=(28, 34, 46, 255))
    d.polygon([(12, 32), (36, 12), (38, 32)], fill=(80, 96, 122, 255))
    d.line([(36, 12), (60, 26)], fill=(0, 230, 255, 220), width=2)
    return im

def create_effect_915_room_earth_detonation():
    """
    Tạo Effect 915: Vòng Xoáy Dao Phóng Xạ Gamma (Gamma Knife Obsidian Vortex):
    1. Vành đai 10 khối đao Hắc Diện Thạch đa giác 3D khóa trọn mục tiêu.
    2. Vòng sấm sét Cyan cuộn xoáy dữ dội nhiều lớp.
    3. Lõi cực quang và 32 chùm tia sáng nan hoa (Sunburst Beams) tỏa từ tâm ra ngoài.
    4. TIA SÉT TỰ NHIÊN TỪ TRÊN TRỜI ĐÁNH THẲNG XUỐNG TÂM.
    5. CHỚP SÁNG BÙNG NỔ NGAY TẠI TRUNG TÂM CHIÊU (Center Mega-Flash) khi bộc phá nổ cực hạn.
    """
    scale = 4
    w_sheet = 252 * scale
    h_sheet = 252 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    sp0_base       = draw_gamma_vortex_base_and_rays(130*scale, 104*scale)
    sp1_ring       = draw_gamma_lightning_ring(130*scale, 104*scale)
    sp2_lightning  = draw_gamma_sky_lightning(44*scale, 140*scale)
    sp3_flash      = draw_gamma_center_mega_flash(74*scale, 68*scale)
    sp4_burst      = draw_gamma_detonation_burst(72*scale, 54*scale)
    sp5_blade_tl   = draw_gamma_blade_top_left(38*scale, 34*scale)
    sp6_blade_bl   = draw_gamma_blade_bottom_left(38*scale, 34*scale)
    sp7_blade_v    = draw_gamma_blade_vertical(36*scale, 32*scale)
    sp8_blade_s    = draw_gamma_blade_side(36*scale, 26*scale)
    sp9_frag       = draw_gamma_fragment(18*scale, 16*scale)

    # Bố trí Sprite vào Sheet (Đảm bảo 100% tọa độ <= 252 ở 1x)
    im.paste(sp0_base,      (0*scale,   0*scale),   sp0_base)       # 0: x=0, y=0, w=130, h=104
    im.paste(sp1_ring,      (0*scale, 106*scale),   sp1_ring)       # 1: x=0, y=106, w=130, h=104
    im.paste(sp2_lightning, (132*scale, 0*scale),   sp2_lightning)  # 2: x=132, y=0, w=44, h=140
    im.paste(sp3_flash,     (178*scale, 0*scale),   sp3_flash)      # 3: x=178, y=0, w=74, h=68
    im.paste(sp4_burst,     (178*scale, 70*scale),  sp4_burst)      # 4: x=178, y=70, w=72, h=54
    im.paste(sp5_blade_tl,  (132*scale, 142*scale), sp5_blade_tl)   # 5: x=132, y=142, w=38, h=34
    im.paste(sp6_blade_bl,  (172*scale, 142*scale), sp6_blade_bl)   # 6: x=172, y=142, w=38, h=34
    im.paste(sp7_blade_v,   (212*scale, 142*scale), sp7_blade_v)    # 7: x=212, y=142, w=36, h=32
    im.paste(sp8_blade_s,   (132*scale, 178*scale), sp8_blade_s)    # 8: x=132, y=178, w=36, h=26
    im.paste(sp9_frag,      (170*scale, 178*scale), sp9_frag)       # 9: x=170, y=178, w=18, h=16

    small_imgs = [
        (0,   0,   0, 130, 104), # Sprite 0: Vortex Base & Sunburst Beams
        (1,   0, 106, 130, 104), # Sprite 1: Swirling Lightning Ring
        (2, 132,   0,  44, 140), # Sprite 2: Sky Lightning Bolt (Tia sét từ trời)
        (3, 178,   0,  74,  68), # Sprite 3: Center Mega-Flash (Chớp sáng cực đại trung tâm chiêu)
        (4, 178,  70,  72,  54), # Sprite 4: Detonation Plasma Burst
        (5, 132, 142,  38,  34), # Sprite 5: Top-Left Blade
        (6, 172, 142,  38,  34), # Sprite 6: Bottom-Left Blade
        (7, 212, 142,  36,  32), # Sprite 7: Vertical Blade
        (8, 132, 178,  36,  26), # Sprite 8: Side Blade
        (9, 170, 178,  18,  16), # Sprite 9: Fragment
    ]

    frames = []
    # Diễn hoạt 25 Frame hoàn chỉnh
    for fid in range(25):
        f_parts = []
        
        # 1. Base đĩa chấn động & chùm tia nan hoa
        if fid <= 21:
            f_parts.append((-65, -72, 0, 0, 0))
            
        # 2. Vành đai sét cuộn tròn quanh chu vi
        if 2 <= fid <= 18:
            jit = 1 if fid % 2 == 0 else 0
            f_parts.append((-65 + jit, -72 - jit, 1, 0, 0))

        # 3. Diễn biến Vòng Đao Hắc Thạch:
        if 3 <= fid <= 5: # Đao trận từ xa lao vào vị trí khóa
            d_out = (5 - fid) * 8
            f_parts.append((-58 - d_out, -66 - int(d_out*0.8), 5, 0, 1))
            f_parts.append(( 20 + d_out, -66 - int(d_out*0.8), 5, 1, 1))
            f_parts.append((-18, -78 - d_out, 7, 0, 1))
            f_parts.append((-60 - d_out, -10 + int(d_out*0.8), 6, 0, 1))
            f_parts.append(( 22 + d_out, -10 + int(d_out*0.8), 6, 1, 1))
            f_parts.append((-18,  -2 + d_out, 7, 2, 1))
            f_parts.append((-68 - d_out, -33, 8, 0, 1))
            f_parts.append(( 32 + d_out, -33, 8, 1, 1))

        elif 6 <= fid <= 13: # KHOẢNH KHẮC ĐAO TRẬN KHÓA MỤC TIÊU (CHUẨN 100% ẢNH MẪU)
            jit_x = 1 if fid % 3 == 0 else 0
            jit_y = -1 if fid % 2 == 0 else 0
            f_parts.append((-58 + jit_x, -66 + jit_y, 5, 0, 1)) # Top-Left
            f_parts.append(( 20 - jit_x, -66 + jit_y, 5, 1, 1)) # Top-Right
            f_parts.append((-18, -78 + jit_y, 7, 0, 1))          # Top-Mid
            f_parts.append((-60 + jit_x, -10 - jit_y, 6, 0, 1)) # Bottom-Left
            f_parts.append(( 22 - jit_x, -10 - jit_y, 6, 1, 1)) # Bottom-Right
            f_parts.append((-18,  -2 - jit_y, 7, 2, 1))          # Bottom-Mid
            f_parts.append((-68 + jit_x, -33, 8, 0, 1))          # Side-Left
            f_parts.append(( 32 - jit_x, -33, 8, 1, 1))          # Side-Right
            f_parts.append((-44, -50, 9, 0, 1))
            f_parts.append(( 32, -50, 9, 1, 1))
            f_parts.append((-46,   4, 9, 0, 1))
            f_parts.append(( 34,   4, 9, 1, 1))

        elif 14 <= fid <= 16: # TIA SÉT TỪ TRỜI GIÁNG XUỐNG + Đao đâm thấu vào tâm dồn lực
            d_in = (fid - 13) * 12
            f_parts.append((-58 + d_in, -66 + int(d_in*0.8), 5, 0, 1))
            f_parts.append(( 20 - d_in, -66 + int(d_in*0.8), 5, 1, 1))
            f_parts.append((-18, -78 + d_in, 7, 0, 1))
            f_parts.append((-60 + d_in, -10 - int(d_in*0.8), 6, 0, 1))
            f_parts.append(( 22 - d_in, -10 - int(d_in*0.8), 6, 1, 1))
            f_parts.append((-18,  -2 - d_in, 7, 2, 1))
            f_parts.append((-68 + d_in, -33, 8, 0, 1))
            f_parts.append(( 32 - d_in, -33, 8, 1, 1))
            # TIA SÉT GIÁNG TỪ TRỜI CỰC MẠNH XUYÊN THẲNG XUỐNG TÂM
            f_parts.append((-22, -300, 2, 0, 1)) # Sét tầng mây
            f_parts.append((-22, -160, 2, 0, 1)) # Sét đâm thẳng vào tâm
        elif 17 <= fid <= 18: # BỘC PHÁ NỔ CỰC ĐẠI
            # 1. Cột sét trên trời giáng chói lòa thẳng vào tâm chiêu
            f_parts.append((-22, -300, 2, 0, 1))
            f_parts.append((-22, -160, 2, 0, 1))

            # 2. Chớp nổ plasma bộc phá cực đại ở tâm
            f_parts.append((-36, -47, 4, 0, 1))

        elif 19 <= fid <= 21: # Mảnh vỡ tinh thể văng tung tóe sau vụ nổ
            f_parts.append((-36, -47, 4, 0, 1))
            d_scat = (fid - 18) * 16
            f_parts.append((-24 - d_scat, -42 - d_scat, 9, 0, 1))
            f_parts.append((-24 + d_scat, -42 - d_scat, 9, 1, 1))
            f_parts.append((-24 - d_scat, -42 + d_scat, 9, 0, 1))
            f_parts.append((-24 + d_scat, -42 + d_scat, 9, 1, 1))
            f_parts.append((-24 - int(d_scat*1.4), -42, 9, 0, 1))
            f_parts.append((-24 + int(d_scat*1.4), -42, 9, 1, 1))

        elif 22 <= fid <= 24: # Tiêu tán dần
            d_scat = 48 + (fid - 21) * 8
            f_parts.append((-24 - d_scat, -42 - d_scat, 9, 0, 1))
            f_parts.append((-24 + d_scat, -42 - d_scat, 9, 1, 1))
            f_parts.append((-24 - d_scat, -42 + d_scat, 9, 0, 1))
            f_parts.append((-24 + d_scat, -42 + d_scat, 9, 1, 1))

        frames.append(f_parts)

    seq = [f for f in range(25) for _ in range(3)]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(915, im, data_bytes)

# 3. TẠO EFFECT 916: VÒNG TRÒN BẢO HỘ CURTAIN MÀU XÁM XANH SÁNG (NHƯ CHIÊU CŨ)
# ==============================================================================
def draw_curtain_room_bubble(phase=0, w_box=320, h_box=320):
    """
    Vẽ QUẢ CẦU ROOM BẢO HỘ (CURTAIN SHIELD) CHUẨN ANIME ONE PIECE Y HỆT ẢNH MẪU NGƯỜI DÙNG:
    - Quả cầu năng lượng Room xanh ngọc lam cyan trong suốt tuyệt đẹp
    - Vành viền neon phát quang êm dịu, vệt bóng kính phản quang cong cong (Glass Sheen)
    - Đốm sáng phản quang nhỏ góc trên bên trái
    - Nhịp thở/nhấp nhô năng lượng điều hòa (breathing pulse) qua 8 pha
    """
    script_dir = os.path.dirname(os.path.abspath(__file__))
    bubble_path = os.path.join(script_dir, 'data', 'room_bubble.png')
    
    pulse = int(4 * math.sin(phase * 2 * math.pi / 8))
    d_size = 304 + pulse
    cx, cy = w_box // 2, h_box // 2
    r_outer = d_size // 2

    im_phase = Image.new('RGBA', (w_box, h_box), (0, 0, 0, 0))
    d = ImageDraw.Draw(im_phase)

    # Hào quang phát sáng mềm mại viền ngoài nhấp nhô theo nhịp thở
    glow_pulse = int(20 * math.sin(phase * 2 * math.pi / 8))
    for dr, a in [(8, max(5, 15 + glow_pulse)), (4, max(10, 30 + glow_pulse))]:
        d.ellipse([cx - r_outer - dr, cy - r_outer - dr, cx + r_outer + dr, cy + r_outer + dr],
                  fill=(130, 225, 255, a))

    if os.path.exists(bubble_path):
        base_bubble = Image.open(bubble_path)
        b_resized = base_bubble.resize((d_size, d_size), Image.Resampling.LANCZOS)
        bx = (w_box - d_size) // 2
        by = (h_box - d_size) // 2
        im_phase.alpha_composite(b_resized, (bx, by))
    else:
        # Fallback vẽ thuật toán màng năng lượng hình cầu nếu không có file ảnh
        r = r_outer - 4
        y_coords, x_coords = np.ogrid[:h_box, :w_box]
        dx = (x_coords - cx) / float(r)
        dy = (y_coords - cy) / float(r)
        dist = np.sqrt(dx**2 + dy**2)
        in_sphere = dist <= 1.0

        arr = np.zeros((h_box, w_box, 4), dtype=np.uint8)
        alpha_body = np.clip(35.0 + 80.0 * (dist ** 2.0), 0, 120)
        r_body = 172.0 - 45.0 * dist
        g_body = 240.0 - 20.0 * dist
        b_body = 253.0 - 8.0 * dist

        arr[in_sphere, 0] = np.clip(r_body[in_sphere], 0, 255)
        arr[in_sphere, 1] = np.clip(g_body[in_sphere], 0, 255)
        arr[in_sphere, 2] = np.clip(b_body[in_sphere], 0, 255)
        arr[in_sphere, 3] = np.clip(alpha_body[in_sphere], 0, 255)

        im_body = Image.fromarray(arr, mode='RGBA')
        im_phase.alpha_composite(im_body)

        for dr, a, width in [(10, 40, 10), (6, 75, 7), (3, 120, 5)]:
            d.ellipse([cx - r - dr, cy - r - dr, cx + r + dr, cy + r + dr], outline=(70, 195, 245, a), width=width)
        d.ellipse([cx - r, cy - r, cx + r, cy + r], outline=(50, 180, 240, 240), width=6)
        d.ellipse([cx - r + 1, cy - r + 1, cx + r - 1, cy + r - 1], outline=(140, 235, 255, 255), width=4)
        d.ellipse([cx - r + 2, cy - r + 2, cx + r - 2, cy + r - 2], outline=(235, 252, 255, 255), width=2)
        d.arc([cx - r + 18, cy - r + 18, cx + r - 50, cy + r - 50], start=195, end=265, fill=(245, 255, 255, 200), width=7)
        d.arc([cx - r + 20, cy - r + 20, cx + r - 48, cy + r - 48], start=200, end=260, fill=(255, 255, 255, 255), width=3)
        dot_rad = math.radians(205)
        dot_x = cx + int(math.cos(dot_rad) * (r * 0.85))
        dot_y = cy + int(math.sin(dot_rad) * (r * 0.85))
        d.ellipse([dot_x - 5, dot_y - 8, dot_x + 5, dot_y + 8], fill=(245, 255, 255, 220))

    return im_phase

def create_effect_916_curtain_shield():
    """
    Tạo Effect 916: Curtain - Khiên Room bảo hộ hình cầu xanh ngọc lam cyan trong suốt chuẩn ảnh mẫu:
    - Quả cầu năng lượng Room bao bọc quanh người chơi
    - Nhịp thở/nhấp nhô năng lượng êm dịu (8 pha)
    - Ruột trong suốt, onTop = 0 ở tầng nền nhân vật -> Bảo vệ người chơi mà không che khuất trang bị/vũ khí!
    """
    w_box = 320
    h_box = 320
    circle_imgs = [draw_curtain_room_bubble(p, w_box, h_box) for p in range(8)]

    w_sheet = w_box * 3 # 960 (240 ở 1x)
    h_sheet = h_box * 3 # 960 (240 ở 1x)
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    # Bố trí 8 Sprites vào lưới 3x3 (240x240 ở 1x -> tọa độ max 160 <= 255)
    im.paste(circle_imgs[0], (0, 0), circle_imgs[0])
    im.paste(circle_imgs[1], (320, 0), circle_imgs[1])
    im.paste(circle_imgs[2], (640, 0), circle_imgs[2])

    im.paste(circle_imgs[3], (0, 320), circle_imgs[3])
    im.paste(circle_imgs[4], (320, 320), circle_imgs[4])
    im.paste(circle_imgs[5], (640, 320), circle_imgs[5])

    im.paste(circle_imgs[6], (0, 640), circle_imgs[6])
    im.paste(circle_imgs[7], (320, 640), circle_imgs[7])

    small_imgs = [
        (0,   0,   0, 80, 80), # Sprite 0: Quả cầu Room bảo hộ pha 0
        (1,  80,   0, 80, 80), # Sprite 1: Quả cầu Room bảo hộ pha 1
        (2, 160,   0, 80, 80), # Sprite 2: Quả cầu Room bảo hộ pha 2
        (3,   0,  80, 80, 80), # Sprite 3: Quả cầu Room bảo hộ pha 3
        (4,  80,  80, 80, 80), # Sprite 4: Quả cầu Room bảo hộ pha 4
        (5, 160,  80, 80, 80), # Sprite 5: Quả cầu Room bảo hộ pha 5
        (6,   0, 160, 80, 80), # Sprite 6: Quả cầu Room bảo hộ pha 6
        (7,  80, 160, 80, 80), # Sprite 7: Quả cầu Room bảo hộ pha 7
    ]

    frames = [
        [(-40, -66, 0, 0, 0)],
        [(-40, -66, 1, 0, 0)],
        [(-40, -66, 2, 0, 0)],
        [(-40, -66, 3, 0, 0)],
        [(-40, -66, 4, 0, 0)],
        [(-40, -66, 5, 0, 0)],
        [(-40, -66, 6, 0, 0)],
        [(-40, -66, 7, 0, 0)],
    ]

    seq = [0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7]
    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(916, im, data_bytes)


# ==============================================================================
# 4. TẠO ICONS CHO TRÁI OPE OPE & 4 SKILL (ID 2191, 4421..4424)
# ==============================================================================
def create_all_ope_icons():
    script_dir = os.path.dirname(os.path.abspath(__file__))
    base_icon_dir = os.path.join(script_dir, 'data', 'icon')
    
    # Icon 1: Trái Ope Ope no Mi (Item ID 1016, Icon File ID 2191)
    im_fruit = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    df = ImageDraw.Draw(im_fruit)
    df.arc([36, 6, 60, 26], start=180, end=360, fill=(76, 175, 80), width=4)
    df.line([(48, 16), (48, 26)], fill=(56, 142, 60), width=4)
    df.ellipse([14, 22, 54, 62], fill=(229, 57, 53), outline=(183, 28, 28), width=2)
    df.ellipse([42, 22, 82, 62], fill=(229, 57, 53), outline=(183, 28, 28), width=2)
    df.polygon([(16, 44), (80, 44), (48, 88)], fill=(229, 57, 53))
    df.line([(16, 44), (48, 88)], fill=(183, 28, 28), width=3)
    df.line([(80, 44), (48, 88)], fill=(183, 28, 28), width=3)
    df.arc([24, 30, 46, 52], start=45, end=270, fill=(255, 215, 0), width=3)
    df.arc([50, 30, 72, 52], start=270, end=135, fill=(255, 215, 0), width=3)
    df.arc([36, 48, 60, 72], start=0, end=220, fill=(255, 255, 255), width=2)
    draw_sparkle(df, 30, 34, r=6, color=(255, 255, 255, 255))

    # Icon 2: Skill 1 - Room Trảm Không Gian (Icon File ID 4421)
    im_sk1 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d1 = ImageDraw.Draw(im_sk1)
    d1.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(10, 25, 45, 245), outline=(0, 229, 255), width=3)
    d1.ellipse([14, 14, 82, 82], outline=(0, 229, 255, 220), width=3)
    d1.arc([14, 31, 82, 65], start=0, end=360, fill=(0, 255, 200, 180), width=2)
    # Lưỡi kiếm Haki đen bọc hào quang đỏ
    d1.line([(18, 78), (78, 18)], fill=(160, 10, 40), width=9)
    d1.line([(18, 78), (78, 18)], fill=(15, 10, 20), width=6)
    d1.line([(18, 78), (78, 18)], fill=(240, 230, 255), width=2)
    # Tia sét Haki đỏ rực lõi trắng
    d1.line([(22, 74), (34, 54), (52, 60), (74, 22)], fill=(255, 20, 60), width=4)
    d1.line([(22, 74), (34, 54), (52, 60), (74, 22)], fill=(255, 255, 255), width=2)
    d1.line([(22, 22), (74, 74)], fill=(0, 229, 255), width=3)
    draw_sparkle(d1, 48, 48, r=10, color=(255, 255, 255))
    draw_sparkle(d1, 74, 22, r=7, color=(255, 30, 80))

    # Icon 3: Skill 2 - Dao Phóng Xạ Gamma (Icon File ID 4422)
    im_sk2 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d2 = ImageDraw.Draw(im_sk2)
    d2.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(12, 35, 28, 245), outline=(0, 255, 170), width=3)
    d2.polygon([(20, 76), (76, 20), (52, 20), (20, 52)], fill=(0, 255, 200), outline=(255, 255, 255), width=2)
    pts_l = [(18, 80), (36, 56), (42, 62), (62, 38), (78, 18)]
    for i in range(len(pts_l) - 1):
        d2.line([pts_l[i], pts_l[i+1]], fill=(0, 255, 255), width=4)
        d2.line([pts_l[i], pts_l[i+1]], fill=(255, 255, 255), width=2)
    draw_sparkle(d2, 76, 20, r=11, color=(255, 255, 255))
    draw_sparkle(d2, 28, 68, r=6, color=(0, 255, 170))

    # Icon 4: Skill 3 - Khiên Phẫu Thuật Curtain / Room Cầu Xanh (Icon File ID 4423)
    im_sk3 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d3 = ImageDraw.Draw(im_sk3)
    d3.rounded_rectangle([3, 3, 92, 92], radius=14, fill=(12, 18, 30, 252), outline=(0, 215, 255), width=3)
    d3.rounded_rectangle([5, 5, 90, 90], radius=12, outline=(0, 130, 210, 180), width=1)
    d3.ellipse([48 - 40, 48 - 40, 48 + 40, 48 + 40], outline=(0, 190, 255, 90), width=2)
    for ang_i in [0, 90, 180, 270]:
        d3.arc([48 - 36, 48 - 36, 48 + 36, 48 + 36], start=ang_i, end=ang_i+65, fill=(0, 240, 255, 200), width=2)

    # Đặt quả cầu Room xanh phát sáng rực rỡ vào trung tâm icon
    bubble_path = os.path.join(script_dir, 'data', 'room_bubble.png')
    if os.path.exists(bubble_path):
        src_bubble = Image.open(bubble_path).convert('RGBA')
        bubble_icon = src_bubble.resize((68, 68), Image.Resampling.LANCZOS)
        im_sk3.alpha_composite(bubble_icon, (14, 14))
    else:
        d3.ellipse([18, 18, 78, 78], fill=(30, 130, 230), outline=(255, 255, 255), width=3)

    draw_sparkle(d3, 16, 16, r=6, color=(0, 235, 255))
    draw_sparkle(d3, 80, 16, r=5, color=(210, 245, 255))
    draw_sparkle(d3, 80, 80, r=6, color=(0, 235, 255))
    draw_sparkle(d3, 16, 80, r=5, color=(210, 245, 255))

    # Icon 5: Skill 4 - Bác Sĩ Tử Thần (Icon File ID 4424)
    im_sk4 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d4 = ImageDraw.Draw(im_sk4)
    d4.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(20, 20, 30, 245), outline=(255, 215, 0), width=3)
    d4.ellipse([22, 30, 74, 70], fill=(245, 245, 245), outline=(60, 60, 60), width=2)
    d4.ellipse([14, 56, 82, 74], fill=(230, 230, 230), outline=(60, 60, 60), width=2)
    for spot_x, spot_y, spot_r in [(34, 42, 5), (58, 40, 6), (46, 52, 5), (32, 60, 4), (62, 58, 5)]:
        d4.ellipse([spot_x - spot_r, spot_y - spot_r, spot_x + spot_r, spot_y + spot_r], fill=(35, 35, 35))
    d4.line([(18, 78), (78, 18)], fill=(255, 215, 0), width=3)
    d4.line([(24, 72), (72, 24)], fill=(255, 255, 255), width=1)
    draw_sparkle(d4, 78, 18, r=9, color=(255, 255, 255))
    draw_sparkle(d4, 20, 20, r=6, color=(255, 215, 0))

    icons_to_save = {
        # 2191: Trái Ope Ope là sprite 5 frame chuẩn HTTH (80x400)
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
            
            # Cập nhật thêm vào data/nro/normal/image/{1..4}/icon nếu là zoom x1..x4
            if z.startswith('x') and z[1:] in ('1', '2', '3', '4'):
                nro_icon_dir = os.path.join(script_dir, 'data', 'nro', 'normal', 'image', z[1:], 'icon')
                if os.path.exists(nro_icon_dir):
                    im_z.save(os.path.join(nro_icon_dir, f'{icon_id}.png'), format='PNG', optimize=True)
        print(f"-> Đã tạo thành công Icon ID {icon_id} tại data/icon/ và data/nro/normal/image/ cho toàn bộ x0..x4")

if __name__ == '__main__':
    print("=== ĐANG TẠO SPRITE SHEETS & DATA EFFECT CHO TRÁI OPE OPE NO MI (ID 914..916) ===")
    create_effect_914_room_slash()
    create_effect_915_room_earth_detonation()
    create_effect_916_curtain_shield()
    print("=== ĐANG TẠO ICONS TRÁI ÁC QUỶ & KỸ NĂNG OPE OPE (ID 2191, 4421..4424) ===")
    create_all_ope_icons()
    print("=== HOÀN TẤT TOÀN BỘ TÀI NGUYÊN HÌNH ẢNH & HIỆU ỨNG TRÁI OPE OPE! ===")
