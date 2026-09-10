import os
import sys
import struct
import math
import numpy as np
from PIL import Image, ImageDraw

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
    Vẽ kiếm Kikoku rơi THẲNG ĐỨNG UY LỰC (Phiên bản to bản hơn):
    - Lưỡi kiếm thép sắc lạnh có rãnh máu và sóng kim loại 3D
    - Vành Tsuba chữ thập bọc lông tơ trắng đặc trưng của Law với khuyên kẹp vàng
    - Chuôi kiếm bọc vải tím đậm quấn chéo hoa văn chữ thập trắng, chuôi nắp vàng
    - Sóng xung kích siêu thanh (sonic shockwave) và vệt xé gió cực kỳ uy lực
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2

    # 1. Vệt xé gió / Sóng xung kích siêu thanh phía sau kiếm khi rơi
    if is_trail:
        trail_w_top = 28 if not is_giant else 40
        trail_w_mid = 14 if not is_giant else 20
        y_trail_end = int(h * 0.55)
        d.polygon([(cx - trail_w_top, 0), (cx + trail_w_top, 0), (cx + trail_w_mid, y_trail_end), (cx - trail_w_mid, y_trail_end)], fill=(0, 200, 255, 90))
        d.polygon([(cx - trail_w_top + 8, 0), (cx + trail_w_top - 8, 0), (cx + 6, y_trail_end), (cx - 6, y_trail_end)], fill=(160, 240, 255, 160))
        d.line([(cx, 0), (cx, y_trail_end + 2)], fill=(255, 255, 255, 240), width=5 if not is_giant else 8)

        # Các tia sóng xé gió sonic chevron hai bên
        for y_chev in [int(y_trail_end*0.22), int(y_trail_end*0.5), int(y_trail_end*0.78)]:
            sw = int((y_trail_end - y_chev) * 0.3)
            d.line([(cx - sw - 10, y_chev - 8), (cx, y_chev + 10), (cx + sw + 10, y_chev - 8)], fill=(180, 245, 255, 180), width=2)

        # Tia sét năng lượng Room chạy dọc lưỡi kiếm
        d.line([(cx - 4, 30), (cx + 5, 55), (cx - 3, 85), (cx, y_trail_end)], fill=(255, 255, 255, 220), width=2)

    # 2. Tọa độ các bộ phận kiếm
    y_pommel = 18 if not is_giant else 14
    y_tsuba  = 68 if not is_giant else 64
    y_tip    = h - 10

    # 3. Lưỡi kiếm (Blade) - Sắc bén, uy lực
    blade_w = 12 if not is_giant else 20
    d.line([(cx, y_tsuba), (cx, y_tip)], fill=(0, 180, 255, 120), width=blade_w + 16)
    d.line([(cx, y_tsuba), (cx, y_tip)], fill=(80, 230, 255, 190), width=blade_w + 8)

    hw = blade_w // 2
    d.polygon([(cx - hw, y_tsuba), (cx, y_tsuba), (cx, y_tip - 8), (cx - hw, y_tip - 12)], fill=(160, 205, 225, 255))
    d.polygon([(cx, y_tsuba), (cx + hw, y_tsuba), (cx + hw, y_tip - 12), (cx, y_tip - 8)], fill=(235, 248, 255, 255))
    d.polygon([(cx - hw, y_tip - 12), (cx + hw, y_tip - 12), (cx, y_tip)], fill=(255, 255, 255, 255))
    d.line([(cx, y_tsuba), (cx, y_tip - 2)], fill=(255, 255, 255, 255), width=2)
    d.line([(cx - hw, y_tsuba), (cx - hw, y_tip - 12)], fill=(80, 120, 150, 220), width=1)
    d.line([(cx + hw, y_tsuba), (cx + hw, y_tip - 12)], fill=(255, 255, 255, 255), width=1)

    # 4. Vành Tsuba bọc lông tơ trắng
    tsuba_w = 34 if not is_giant else 48
    tsuba_h = 15 if not is_giant else 20
    d.ellipse([cx - tsuba_w//2 - 2, y_tsuba - tsuba_h//2 - 1, cx + tsuba_w//2 + 2, y_tsuba + tsuba_h//2 + 1], fill=(230, 235, 240, 255))
    d.ellipse([cx - tsuba_w//2, y_tsuba - tsuba_h//2, cx + tsuba_w//2, y_tsuba + tsuba_h//2], fill=(255, 255, 255, 255), outline=(180, 190, 200, 255), width=2)
    d.ellipse([cx - 6, y_tsuba - 4, cx + 6, y_tsuba + 4], fill=(255, 215, 0, 255))

    # 5. Chuôi kiếm Kikoku (Tsuka)
    hilt_w = 9 if not is_giant else 12
    d.line([(cx, y_pommel), (cx, y_tsuba)], fill=(35, 12, 45, 255), width=hilt_w)
    cross_step = 8 if not is_giant else 11
    for y_c in range(y_pommel + 5, y_tsuba - 5, cross_step):
        d.line([(cx - 4, y_c - 2), (cx + 4, y_c + 2)], fill=(255, 255, 255, 230), width=1)
        d.line([(cx - 4, y_c + 2), (cx + 4, y_c - 2)], fill=(255, 255, 255, 230), width=1)

    d.ellipse([cx - 5, y_pommel - 5, cx + 5, y_pommel + 5], fill=(255, 215, 0, 255), outline=(200, 160, 0, 255))
    d.line([(cx - 12, y_tip), (cx + 12, y_tip)], fill=(255, 255, 255, 255), width=2)
    d.line([(cx, y_tip - 12), (cx, y_tip + 12)], fill=(255, 255, 255, 255), width=2)
    d.line([(cx - 8, y_tip - 8), (cx + 8, y_tip + 8)], fill=(120, 230, 255, 220), width=1)
    d.line([(cx - 8, y_tip + 8), (cx + 8, y_tip - 8)], fill=(120, 230, 255, 220), width=1)
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

def draw_sword_stuck_in_crater(w=120, h=184):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    s = w / 96.0
    y_ground = int(110 * s)
    y_pommel = int(18 * s)
    y_tsuba  = int(62 * s)

    d.ellipse([cx - int(42*s), y_ground - int(14*s), cx + int(42*s), y_ground + int(18*s)], fill=(16, 8, 4, 245))
    d.ellipse([cx - int(30*s), y_ground - int(8*s), cx + int(30*s), y_ground + int(10*s)], fill=(8, 4, 2, 255))
    d.arc([cx - int(42*s), y_ground - int(14*s), cx + int(42*s), y_ground + int(18*s)], start=0, end=180, fill=(90, 58, 38, 255), width=int(4*s))

    for dx1, dy1, dx2, dy2 in [(-36, 4, -18, 2), (-18, 2, 0, 0), (0, 0, 20, 3), (20, 3, 38, 6), (0, 0, 8, 14), (0, 0, -10, 12)]:
        d.line([(cx + int(dx1*s), y_ground + int(dy1*s)), (cx + int(dx2*s), y_ground + int(dy2*s))], fill=(0, 255, 240, 220), width=int(3*s))
        d.line([(cx + int(dx1*s), y_ground + int(dy1*s)), (cx + int(dx2*s), y_ground + int(dy2*s))], fill=(255, 255, 255, 240), width=1)

    d.polygon([(cx - int(14*s), y_ground + int(4*s)), (cx - int(8*s), y_ground - int(10*s)), (cx + int(8*s), y_ground - int(8*s)), (cx + int(14*s), y_ground + int(4*s))], fill=(120, 80, 50), outline=(45, 30, 20))

    blade_w = int(9 * s)
    d.line([(cx, y_tsuba), (cx, y_ground)], fill=(0, 200, 255, 140), width=blade_w + int(8*s))
    d.line([(cx, y_tsuba), (cx, y_ground)], fill=(180, 240, 255, 255), width=blade_w)
    d.line([(cx, y_tsuba), (cx, y_ground)], fill=(255, 255, 255, 255), width=3)

    tsuba_w = int(26 * s)
    tsuba_h = int(12 * s)
    d.ellipse([cx - tsuba_w//2 - 2, y_tsuba - tsuba_h//2 - 1, cx + tsuba_w//2 + 2, y_tsuba + tsuba_h//2 + 1], fill=(230, 235, 240, 255))
    d.ellipse([cx - tsuba_w//2, y_tsuba - tsuba_h//2, cx + tsuba_w//2, y_tsuba + tsuba_h//2], fill=(255, 255, 255, 255), outline=(180, 190, 200, 255), width=2)
    d.ellipse([cx - 5, y_tsuba - 3, cx + 5, y_tsuba + 3], fill=(255, 215, 0, 255))

    hilt_w = int(7 * s)
    d.line([(cx, y_pommel), (cx, y_tsuba)], fill=(35, 12, 45, 255), width=hilt_w)
    cross_step = int(7 * s)
    for y_c in range(y_pommel + 4, y_tsuba - 4, cross_step):
        d.line([(cx - 3, y_c - 2), (cx + 3, y_c + 2)], fill=(255, 255, 255, 230), width=1)
        d.line([(cx - 3, y_c + 2), (cx + 3, y_c - 2)], fill=(255, 255, 255, 230), width=1)
    d.ellipse([cx - 5, y_pommel - 5, cx + 5, y_pommel + 5], fill=(255, 215, 0, 255), outline=(200, 160, 0, 255))
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
    d.line([(cx - int(56*s), cy - int(40*s)), (cx + int(56*s), cy + int(40*s))], fill=(0, 229, 255, 180), width=int(10*s))
    d.line([(cx - int(56*s), cy - int(40*s)), (cx + int(56*s), cy + int(40*s))], fill=(255, 255, 255, 255), width=int(4*s))
    d.line([(cx - int(50*s), cy + int(42*s)), (cx + int(50*s), cy - int(42*s))], fill=(0, 255, 200, 180), width=int(8*s))
    d.line([(cx - int(50*s), cy + int(42*s)), (cx + int(50*s), cy - int(42*s))], fill=(255, 255, 255, 255), width=3)
    draw_sparkle(d, cx, cy, r=int(24*s), color=(255, 255, 255, 255))
    r_center = int(12 * s)
    d.ellipse([cx - r_center, cy - r_center, cx + r_center, cy + r_center], fill=(255, 255, 255, 240))
    for dx, dy in [(-30, -20), (32, 18), (-22, 28), (28, -25)]:
        draw_sparkle(d, cx + int(dx*s), cy + int(dy*s), r=int(9*s), color=(0, 240, 255, 230))
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
    h_sheet = 268 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    im_room           = draw_anime_hemisphere_room_dome(248*scale, 152*scale)
    im_sword_straight = draw_straight_kikoku_sword(22*scale, 56*scale, is_trail=False)
    im_sword_trail    = draw_straight_kikoku_sword(22*scale, 56*scale, is_trail=True)

    im_crater1        = draw_ground_crater_and_debris(52*scale, 26*scale, phase=0)
    im_crater2        = draw_ground_crater_and_debris(58*scale, 28*scale, phase=1)
    im_sword_crater   = draw_sword_stuck_in_crater(30*scale, 46*scale)
    im_shockwave      = draw_ground_shockwave(54*scale, 22*scale)

    im_giant_sword    = draw_straight_kikoku_sword(32*scale, 80*scale, is_trail=True, is_giant=True)
    im_massive_crater = draw_massive_climax_crater(84*scale, 34*scale)
    im_slash_burst    = draw_slash_burst(54*scale, 48*scale)
    im_dissolve       = draw_dissolve_sparks(44*scale, 44*scale)

    im_smoke_puff1    = draw_circling_smoke_puff(32*scale, 18*scale, variant=0)
    im_smoke_puff2    = draw_circling_smoke_puff(36*scale, 20*scale, variant=1)

    # Bố trí Sprite vào Sheet
    im.paste(im_room,           (0*scale,   0*scale),   im_room)
    im.paste(im_giant_sword,    (0*scale,   154*scale), im_giant_sword)
    im.paste(im_sword_straight, (34*scale,  154*scale), im_sword_straight)
    im.paste(im_sword_trail,    (58*scale,  154*scale), im_sword_trail)
    im.paste(im_sword_crater,   (82*scale,  154*scale), im_sword_crater)
    im.paste(im_massive_crater, (114*scale, 154*scale), im_massive_crater)
    im.paste(im_dissolve,       (200*scale, 154*scale), im_dissolve)
    im.paste(im_slash_burst,    (114*scale, 190*scale), im_slash_burst)
    im.paste(im_smoke_puff1,    (208*scale, 200*scale), im_smoke_puff1)
    im.paste(im_smoke_puff2,    (208*scale, 220*scale), im_smoke_puff2)
    im.paste(im_crater2,        (34*scale,  212*scale), im_crater2)
    im.paste(im_shockwave,      (0*scale,   242*scale), im_shockwave)
    im.paste(im_crater1,        (56*scale,  242*scale), im_crater1)

    small_imgs = [
        (0,    0,   0, 248, 152), # Sprite 0: Vòm Nửa Hình Cầu Room
        (1,   34, 154,  22,  56), # Sprite 1: Kiếm Kikoku rơi thẳng
        (2,   58, 154,  22,  56), # Sprite 2: Kiếm Kikoku rơi thẳng xé gió
        (3,   56, 242,  52,  26), # Sprite 3: Hố lún đất + đá văng pha 1
        (4,   34, 212,  58,  28), # Sprite 4: Hố lún đất + đá văng bùng nổ pha 2
        (5,   82, 154,  30,  46), # Sprite 5: Kiếm cắm ngập trong hố lún đất
        (6,    0, 242,  54,  22), # Sprite 6: Sóng chấn động lún đất
        (7,    0, 154,  32,  80), # Sprite 7: Đại Thần Kiếm khổng lồ rơi thẳng
        (8,  114, 154,  84,  34), # Sprite 8: Đại hố lún cực đại
        (9,  114, 190,  54,  48), # Sprite 9: Vết chém nứt rách không gian
        (10, 200, 154,  44,  44), # Sprite 10: Tinh thể tan biến
        (11, 208, 200,  32,  18), # Sprite 11: Khói bụi xoáy tròn chân đế 1
        (12, 208, 220,  36,  20), # Sprite 12: Khói bụi xoáy tròn chân đế 2
    ]

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
        [
            (-124, -129, 0, 0, 0),
            ( -31,  -35, 5, 0, 1),
            ( -42,  -13, 3, 0, 1),
            ( -43,  -11, 6, 0, 1),
            ( -75,  -35, 5, 0, 1),
            ( -86,  -13, 3, 0, 1),
            (  33, -100, 2, 0, 1),
            ( -99, -100, 2, 0, 1),
        ],
        
        # Frame 3: Kiếm 3 & 4 CẮM ĐẤT! Nổ hố lún + Đợt 3: Kiếm 5 & 6 lao nhanh (0.28s)
        [
            (-124, -129, 0, 0, 0),
            ( -31,  -35, 5, 0, 1),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (  15,  -14, 4, 0, 1),
            (-103,  -35, 5, 0, 1),
            (-117,  -14, 4, 0, 1),
            (  61, -100, 2, 0, 1),
            ( -39, -100, 2, 0, 1),
        ],
        
        # Frame 4: Kiếm 5 & 6 CẮM ĐẤT! Nổ hố lún + Đợt 4: Kiếm 7 & 8 lao nhanh (0.40s)
        [
            (-124, -129, 0, 0, 0),
            ( -31,  -35, 5, 0, 1),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (-103,  -35, 5, 0, 1),
            (  57,  -35, 5, 0, 1),
            (  46,  -13, 3, 0, 1),
            ( -43,  -35, 5, 0, 1),
            ( -54,  -13, 3, 0, 1),
            (   9, -100, 2, 0, 1),
            ( -59, -100, 2, 0, 1),
        ],
        
        # Frame 5: Kiếm 7 & 8 CẮM ĐẤT! Vòng kiếm hoàn tất cắm đầy mặt đất (0.52s)
        [
            (-124, -129, 0, 0, 0),
            ( -31,  -35, 5, 0, 1),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (-103,  -35, 5, 0, 1),
            (  57,  -35, 5, 0, 1),
            ( -43,  -35, 5, 0, 1),
            (   5,  -35, 5, 0, 1),
            (  -9,  -14, 4, 0, 1),
            ( -63,  -35, 5, 0, 1),
            ( -77,  -14, 4, 0, 1),
        ],
        
        # Frame 6: Chấn động cực đại! Đất đá văng cao xung quanh hàng kiếm (0.64s)
        [
            (-124, -129, 0, 0, 0),
            ( -31,  -35, 5, 0, 1),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (-103,  -35, 5, 0, 1),
            (  57,  -35, 5, 0, 1),
            ( -43,  -35, 5, 0, 1),
            (   5,  -35, 5, 0, 1),
            ( -63,  -35, 5, 0, 1),
            ( -42,  -14, 4, 0, 1),
            (  15,  -14, 4, 0, 1),
            ( -43,  -11, 6, 0, 1),
        ],
        
        # Frame 7: Đỉnh Room phát quang + ĐẠI THẦN KIẾM xuất hiện trên đỉnh Room (0.76s)
        [
            (-124, -129, 0, 0, 0),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (-103,  -35, 5, 0, 1),
            (  57,  -35, 5, 0, 1),
            ( -32, -115, 7, 0, 1),
        ],
        
        # Frame 8: Đại Thần Kiếm lao nhanh xé gió siêu thanh (0.88s)
        [
            (-124, -129, 0, 0, 0),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (-103,  -35, 5, 0, 1),
            (  57,  -35, 5, 0, 1),
            ( -32,  -70, 7, 0, 1),
        ],
        
        # Frame 9: Đại Thần Kiếm sát mặt đất + Chấn động mặt đất (1.00s)
        [
            (-124, -129, 0, 0, 0),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (-103,  -35, 5, 0, 1),
            (  57,  -35, 5, 0, 1),
            ( -32,  -40, 7, 0, 1),
            ( -43,  -11, 6, 0, 1),
        ],
        
        # Frame 10: ĐẠI THẦN KIẾM CẮM ĐẤT! TẠO ĐẠI HỐ LÚN + VẾT NỨT KHÔNG GIAN (1.12s)
        [
            (-124, -129, 0, 0, 0),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            (-103,  -35, 5, 0, 1),
            (  57,  -35, 5, 0, 1),
            ( -32,  -52, 7, 0, 1),
            ( -58,  -17, 8, 0, 1),
            ( -43,  -54, 9, 0, 1),
        ],
        
        # Frame 11: Toàn bộ Room nổ bùng chấn động cực đại! (1.24s)
        [
            (-124, -129, 0, 0, 0),
            ( -75,  -35, 5, 0, 1),
            (  29,  -35, 5, 0, 1),
            ( -32,  -52, 7, 0, 1),
            ( -58,  -17, 8, 0, 1),
            ( -43,  -54, 9, 0, 1),
        ],
        
        # Frame 12: Không gian nứt vỡ, tinh thể văng tung tóe (1.36s)
        [
            (-124, -129, 0, 0, 0),
            ( -43,  -54, 9, 0, 1),
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

    # 22 cụm khói mây cuộn xoay tròn quanh chân Room
    N_puffs = 22
    frames = []
    for f_idx, rf in enumerate(raw_frames):
        f_list = list(rf)
        if f_idx <= 12:
            offset_s = (f_idx * (total_len / 16.0)) % total_len
            for k in range(N_puffs):
                target_s = (k / N_puffs) * total_len + offset_s
                ex, ey, sin_t = get_ellipse_point(target_s)
                sp_id = 11 if k % 2 == 0 else 12
                px = int(round(ex)) - (16 if k % 2 == 0 else 18)
                py = int(round(ey)) - (9 if k % 2 == 0 else 10)
                layer = 0 if sin_t < 0 else 1
                f_list.append((px, py, sp_id, 0, layer))
        frames.append(f_list)

    seq = (
        [0, 0] +        # 2 ticks: Room xuất hiện
        [1, 1] +        # 2 ticks: Room ổn định, đợt kiếm 1 lao xuống
        [2, 2, 2] +     # 3 ticks: Kiếm 1 & 2 cắm đất nổ hố, đợt 2 lao
        [3, 3, 3] +     # 3 ticks: Kiếm 3 & 4 cắm đất nổ hố, đợt 3 lao
        [4, 4, 4] +     # 3 ticks: Kiếm 5 & 6 cắm đất nổ hố, đợt 4 lao
        [5, 5, 5] +     # 3 ticks: Kiếm 7 & 8 cắm đất, vòng kiếm hoàn tất
        [6, 6, 6] +     # 3 ticks: Toàn bộ kiếm chấn động nổ đá
        [7, 7, 7] +     # 3 ticks: ĐẠI THẦN KIẾM xuất hiện trên đỉnh Room
        [8, 8, 8] +     # 3 ticks: Đại Thần Kiếm lao nhanh xé gió
        [9, 9, 9] +     # 3 ticks: Đại Thần Kiếm sát mặt đất tạo chấn động
        [10, 10, 10] +  # 3 ticks: ĐẠI THẦN KIẾM CẮM ĐẤT! Đại hố lún + Vết chém không gian
        [11, 11, 11] +  # 3 ticks: Không gian nổ bùng chói lọi
        [12, 12, 12] +  # 3 ticks: Không gian nứt vỡ, tinh thể văng tung tóe
        [13, 13, 13] +  # 3 ticks: Vòm Room tan biến, tinh thể lấp lánh
        [14, 14, 14] +  # 3 ticks: Tinh thể bay tỏa nhẹ
        [15, 15]        # 2 ticks: Tia sáng cuối cùng tan biến (tổng cộng 45 ticks = 1.80s)
    )

    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(914, im, data_bytes)

# ==============================================================================
# 2. TẠO EFFECT 915: ROOM HÌNH CẦU & ĐÁ ĐẨY VÔ PLAYER TRÚNG ĐÒN (SPHERE ROCK CRUSH)
# ==============================================================================
def draw_tact_gamma_sphere(w=560, h=560):
    """
    Sprite 0: Vòng cầu 3D Room bao trọn người chơi bị trúng đòn (140x140 ở 1x -> 560x560 ở 4x - PHIÊN BẢN TO ĐẸP)
    Chuẩn phong cách quả cầu Room năng lượng xanh ngọc lam cyan trong suốt như ảnh mẫu người dùng
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    d_size = int(w * 0.94)
    r_outer = d_size // 2

    # Hào quang viền phát quang cyan
    for dr, a in [(12, 25), (6, 50), (2, 85)]:
        d.ellipse([cx - r_outer - dr, cy - r_outer - dr, cx + r_outer + dr, cy + r_outer + dr], fill=(130, 225, 255, a))

    script_dir = os.path.dirname(os.path.abspath(__file__))
    bubble_path = os.path.join(script_dir, 'data', 'room_bubble.png')
    if os.path.exists(bubble_path):
        src_bubble = Image.open(bubble_path).convert('RGBA')
        bubble_resized = src_bubble.resize((d_size, d_size), Image.Resampling.LANCZOS)
        im.paste(bubble_resized, (cx - r_outer, cy - r_outer), bubble_resized)
    else:
        # Fallback gradient nếu không có file ảnh
        y_coords, x_coords = np.ogrid[:h, :w]
        dx = (x_coords - cx) / float(r_outer)
        dy = (y_coords - cy) / float(r_outer)
        dist = np.sqrt(dx**2 + dy**2)
        in_sp = dist <= 1.0
        arr = np.zeros((h, w, 4), dtype=np.uint8)
        arr[in_sp, 0] = np.clip(160.0 - 40.0 * dist[in_sp], 0, 255)
        arr[in_sp, 1] = np.clip(235.0 - 20.0 * dist[in_sp], 0, 255)
        arr[in_sp, 2] = np.clip(255.0 - 5.0 * dist[in_sp], 0, 255)
        arr[in_sp, 3] = np.clip(35.0 + 85.0 * (dist[in_sp]**2.0), 0, 130)
        im_body = Image.fromarray(arr, mode='RGBA')
        im.alpha_composite(im_body)
        d.ellipse([cx - r_outer, cy - r_outer, cx + r_outer, cy + r_outer], outline=(60, 195, 245, 240), width=6)
        d.ellipse([cx - r_outer + 2, cy - r_outer + 2, cx + r_outer - 2, cy + r_outer - 2], outline=(220, 250, 255, 255), width=3)
    return im

def draw_crushing_boulder_a(w=168, h=144):
    """
    Sprite 1: Tảng đá góc cạnh lớn A (42x36 ở 1x -> 168x144 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    sx, sy = w / 112.0, h / 96.0
    def t(pts): return [(int(x * sx), int(y * sy)) for x, y in pts]
    pts = t([(20, 16), (56, 8), (96, 24), (104, 60), (76, 88), (28, 80), (8, 48)])
    d.polygon(pts, fill=(62, 58, 66, 255))
    d.polygon(t([(20, 16), (56, 8), (64, 44), (28, 52)]), fill=(94, 90, 100, 255))
    d.polygon(t([(64, 44), (104, 60), (76, 88), (48, 72)]), fill=(38, 34, 42, 255))
    d.line(t([(24, 24), (44, 36), (64, 44), (76, 68)]), fill=(0, 255, 220, 240), width=int(5*sx))
    d.line(t([(24, 24), (44, 36), (64, 44), (76, 68)]), fill=(255, 255, 255, 255), width=int(2*sx))
    d.line(pts + [pts[0]], fill=(0, 220, 255, 120), width=int(3*sx))
    return im

def draw_crushing_boulder_b(w=168, h=144):
    """
    Sprite 2: Tảng đá góc cạnh lớn B (42x36 ở 1x -> 168x144 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    sx, sy = w / 112.0, h / 96.0
    def t(pts): return [(int(x * sx), int(y * sy)) for x, y in pts]
    pts = t([(16, 28), (48, 12), (92, 16), (100, 52), (84, 84), (36, 88), (12, 64)])
    d.polygon(pts, fill=(58, 54, 62, 255))
    d.polygon(t([(48, 12), (92, 16), (68, 48), (36, 40)]), fill=(92, 88, 98, 255))
    d.polygon(t([(68, 48), (100, 52), (84, 84), (44, 68)]), fill=(36, 32, 40, 255))
    d.line(t([(48, 12), (52, 48), (36, 88)]), fill=(0, 255, 220, 240), width=int(5*sx))
    d.line(t([(48, 12), (52, 48), (36, 88)]), fill=(255, 255, 255, 255), width=int(2*sx))
    d.line(t([(52, 48), (84, 56)]), fill=(0, 255, 220, 200), width=int(4*sx))
    d.line(pts + [pts[0]], fill=(0, 220, 255, 120), width=int(3*sx))
    return im

def draw_crushing_rock_spire(w=136, h=112):
    """
    Sprite 3: Mảnh đá nhọn lao nhanh (34x28 ở 1x -> 136x112 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    sx, sy = w / 88.0, h / 72.0
    def t(pts): return [(int(x * sx), int(y * sy)) for x, y in pts]
    pts = t([(8, 36), (44, 12), (80, 24), (68, 56), (36, 60)])
    d.polygon(pts, fill=(68, 64, 72, 255))
    d.polygon(t([(8, 36), (44, 12), (48, 38)]), fill=(100, 96, 106, 255))
    d.polygon(t([(48, 38), (80, 24), (68, 56)]), fill=(42, 38, 46, 255))
    d.line(t([(8, 36), (48, 38), (80, 24)]), fill=(0, 255, 210, 240), width=int(4*sx))
    d.line(t([(8, 36), (48, 38), (80, 24)]), fill=(255, 255, 255, 255), width=int(2*sx))
    d.line(pts + [pts[0]], fill=(0, 220, 255, 130), width=int(3*sx))
    return im

def draw_crushing_rock_slab(w=152, h=120):
    """
    Sprite 4: Khối đá tảng dẹp (38x30 ở 1x -> 152x120 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    sx, sy = w / 104.0, h / 80.0
    def t(pts): return [(int(x * sx), int(y * sy)) for x, y in pts]
    pts = t([(12, 20), (52, 10), (96, 18), (92, 64), (48, 72), (16, 58)])
    d.polygon(pts, fill=(55, 50, 58, 255))
    d.polygon(t([(12, 20), (52, 10), (60, 42), (24, 48)]), fill=(88, 82, 92, 255))
    d.polygon(t([(60, 42), (96, 18), (92, 64), (52, 58)]), fill=(34, 30, 36, 255))
    d.line(t([(12, 20), (60, 42), (92, 64)]), fill=(0, 255, 220, 240), width=int(5*sx))
    d.line(t([(12, 20), (60, 42), (92, 64)]), fill=(255, 255, 255, 255), width=int(2*sx))
    d.line(pts + [pts[0]], fill=(0, 220, 255, 120), width=int(3*sx))
    return im

def draw_crushing_impact_shockwave(w=304, h=256):
    """
    Sprite 5: Sóng xung kích va đập cực đại (76x64 ở 1x -> 304x256 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    sx, sy = w / 200.0, h / 168.0
    rx1, ry1 = int(86 * sx), int(68 * sy)
    rx2, ry2 = int(74 * sx), int(58 * sy)
    rx3, ry3 = int(62 * sx), int(48 * sy)
    d.ellipse([cx - rx1, cy - ry1, cx + rx1, cy + ry1], outline=(0, 220, 255, 100), width=int(10*sx))
    d.ellipse([cx - rx2, cy - ry2, cx + rx2, cy + ry2], outline=(0, 255, 240, 210), width=int(6*sx))
    d.ellipse([cx - rx3, cy - ry3, cx + rx3, cy + ry3], outline=(255, 255, 255, 255), width=int(4*sx))
    for deg in range(0, 360, 30):
        rad = math.radians(deg)
        x1 = cx + int(math.cos(rad) * rx3)
        y1 = cy + int(math.sin(rad) * ry3)
        x2 = cx + int(math.cos(rad) * int(94 * sx))
        y2 = cy + int(math.sin(rad) * int(76 * sy))
        d.line([(x1, y1), (x2, y2)], fill=(0, 255, 230, 180), width=int(4*sx))
    return im

def draw_plasma_lightning_burst(w=288, h=288):
    """
    Sprite 6: Tia sét Plasma Gamma & bùng nổ va đập (72x72 ở 1x -> 288x288 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 184.0
    r1 = int(22 * s)
    r2 = int(32 * s)
    d.ellipse([cx - r1, cy - r1, cx + r1, cy + r1], fill=(255, 255, 255, 255))
    d.ellipse([cx - r2, cy - r2, cx + r2, cy + r2], outline=(0, 255, 220, 200), width=int(5*s))
    branches = [
        [(cx, cy), (cx - int(36*s), cy - int(48*s)), (cx - int(72*s), cy - int(66*s)), (cx - int(108*s), cy - int(90*s))],
        [(cx, cy), (cx + int(42*s), cy - int(42*s)), (cx + int(78*s), cy - int(72*s)), (cx + int(112*s), cy - int(94*s))],
        [(cx, cy), (cx - int(54*s), cy + int(18*s)), (cx - int(84*s), cy + int(36*s)), (cx - int(114*s), cy + int(58*s))],
        [(cx, cy), (cx + int(52*s), cy + int(22*s)), (cx + int(90*s), cy + int(40*s)), (cx + int(118*s), cy + int(64*s))],
        [(cx, cy), (cx - int(28*s), cy + int(60*s)), (cx - int(48*s), cy + int(96*s))],
        [(cx, cy), (cx + int(28*s), cy + int(58*s)), (cx + int(54*s), cy + int(96*s))],
    ]
    for b in branches:
        draw_lightning(d, b, color_glow=(0, 255, 210, 210), color_core=(255, 255, 255, 255), width=int(4*s))
    for sx, sy in [(-int(108*s), -int(90*s)), (int(112*s), -int(94*s)), (-int(114*s), int(58*s)), (int(118*s), int(64*s))]:
        draw_sparkle(d, cx + sx, cy + sy, r=int(10*s), color=(255, 255, 255, 255))
    return im

def draw_shattered_rock_fragments(w=248, h=216):
    """
    Sprite 7: Mảnh đá vỡ vụn văng tứ phía (62x54 ở 1x -> 248x216 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 160.0
    chunks = [
        [(-48, -40), (-34, -48), (-38, -32)],
        [(36, -42), (52, -34), (42, -26)],
        [(-54, 18), (-40, 10), (-36, 26)],
        [(40, 22), (56, 32), (34, 40)],
        [(-14, -52), (0, -60), (8, -48)],
        [(10, 48), (-4, 56), (-12, 42)],
        [(-24, 6), (-12, -4), (-8, 14)],
        [(22, -8), (14, 8), (28, 4)],
    ]
    for c in chunks:
        pts = [(cx + int(px * s), cy + int(py * s)) for px, py in c]
        d.polygon(pts, fill=(70, 65, 75, 255), outline=(0, 255, 210, 220))
    for sx, sy in [(-44, -36), (46, -30), (-46, 16), (48, 28), (0, -54), (0, 48)]:
        draw_sparkle(d, cx + int(sx * s), cy + int(sy * s), r=int(8*s), color=(0, 255, 230, 220))
    return im

def draw_crush_dust_puff(w=216, h=160):
    """
    Sprite 8: Khói bụi va đập tản mác (54x40 ở 1x -> 216x160 ở 4x - PHIÊN BẢN TO BẢN)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    s = w / 144.0
    for bx, by, br in [(-30, 6, 18), (30, 6, 19), (-48, 8, 13), (48, 8, 13), (0, 2, 17)]:
        nbx, nby, nbr = int(bx * s), int(by * s), int(br * s)
        d.ellipse([cx + nbx - nbr, cy + nby - nbr, cx + nbx + nbr, cy + nby + nbr], fill=(120, 110, 118, 140))
        d.ellipse([cx + nbx - nbr + 3, cy + nby - nbr + 3, cx + nbx + nbr - 3, cy + nby + nbr - 3], fill=(155, 145, 150, 110))
    for sx, sy in [(-32, -12), (32, -14), (0, -8)]:
        draw_sparkle(d, cx + int(sx * s), cy + int(sy * s), r=int(9*s), color=(0, 255, 210, 180))
    return im

def create_effect_915_room_earth_detonation():
    """
    Tạo Effect 915: Room Hình Cầu & Đá Đẩy Vô Player Trúng Đòn (SPHERE ROCK CRUSH - PHIÊN BẢN TO HƠN):
    1. Mục tiêu đơn: Tạo phòng Room hình cầu 3D trong suốt chuẩn ảnh mẫu ôm trọn người chơi bị trúng đòn.
    2. Các tảng đá khổng lồ xuất hiện xung quanh viền hình cầu rồi bị đẩy/lao mạnh vào người chơi.
    3. Va chạm nghiền nát dữ dội tại tâm (ngay trên thân người chơi) tạo sóng xung kích, sét Plasma, đá vỡ tung tóe và khói bụi.
    4. Diễn hoạt trọn vẹn đúng 3.0 giây (75 ticks tại 25 FPS).
    """
    scale = 4
    w_sheet = 228 * scale
    h_sheet = 214 * scale
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    im_sphere    = draw_tact_gamma_sphere(140*scale, 140*scale)
    im_boulder_a = draw_crushing_boulder_a(42*scale, 36*scale)
    im_boulder_b = draw_crushing_boulder_b(42*scale, 36*scale)
    im_spire     = draw_crushing_rock_spire(34*scale, 28*scale)
    im_slab      = draw_crushing_rock_slab(38*scale, 30*scale)
    im_shockwave = draw_crushing_impact_shockwave(76*scale, 64*scale)
    im_plasma    = draw_plasma_lightning_burst(72*scale, 72*scale)
    im_shattered = draw_shattered_rock_fragments(62*scale, 54*scale)
    im_dust      = draw_crush_dust_puff(54*scale, 40*scale)

    # Bố trí Sprite vào Sheet (Đảm bảo 100% tọa độ <= 255)
    im.paste(im_sphere,    (0*scale,   0*scale),   im_sphere)
    im.paste(im_boulder_a, (142*scale, 0*scale),   im_boulder_a)
    im.paste(im_boulder_b, (186*scale, 0*scale),   im_boulder_b)
    im.paste(im_spire,     (142*scale, 38*scale),  im_spire)
    im.paste(im_slab,      (178*scale, 38*scale),  im_slab)
    im.paste(im_shockwave, (142*scale, 70*scale),  im_shockwave)
    im.paste(im_plasma,    (0*scale,   142*scale), im_plasma)
    im.paste(im_shattered, (74*scale,  142*scale), im_shattered)
    im.paste(im_dust,      (138*scale, 142*scale), im_dust)

    small_imgs = [
        (0,   0,   0, 140, 140), # Sprite 0: Phòng Room hình cầu 3D
        (1, 142,   0,  42,  36), # Sprite 1: Tảng đá lớn A
        (2, 186,   0,  42,  36), # Sprite 2: Tảng đá lớn B
        (3, 142,  38,  34,  28), # Sprite 3: Mảnh đá nhọn
        (4, 178,  38,  38,  30), # Sprite 4: Khối đá tảng dẹp
        (5, 142,  70,  76,  64), # Sprite 5: Sóng xung kích va đập cực đại
        (6,   0, 142,  72,  72), # Sprite 6: Tia sét Plasma Gamma
        (7,  74, 142,  62,  54), # Sprite 7: Mảnh đá vỡ vụn văng tứ phía
        (8, 138, 142,  54,  40), # Sprite 8: Khói bụi va đập
    ]

    frames = []
    for fid in range(25):
        f_parts = []
        # Quả cầu Room xuất hiện từ frame 0 đến 20 (ôm trọn người chơi tại (0, -26))
        if fid <= 20:
            f_parts.append((-70, -96, 0, 0, 0))
            
        # Vị trí các tảng đá lao vào tâm
        if 3 <= fid <= 8:
            if fid in (3, 4): r_rock = 60
            elif fid == 5: r_rock = 56
            elif fid == 6: r_rock = 38
            elif fid == 7: r_rock = 18
            elif fid == 8: r_rock = 6
            
            f_parts.append((-r_rock - 21, -26 - 18, 1, 0, 0))
            f_parts.append((r_rock - 21, -26 - 18, 2, 0, 0))
            f_parts.append((-int(r_rock*0.7) - 17, -26 - int(r_rock*0.7) - 14, 3, 0, 0))
            f_parts.append((int(r_rock*0.7) - 19, -26 - int(r_rock*0.7) - 15, 4, 0, 0))
            f_parts.append((-int(r_rock*0.7) - 17, -26 + int(r_rock*0.7) - 14, 3, 0, 1))
            f_parts.append((int(r_rock*0.7) - 19, -26 + int(r_rock*0.7) - 15, 4, 0, 1))
            
        if fid == 9: # Va đập cực đại tại tâm player (0, -26)
            f_parts.append((-38, -26 - 32, 5, 0, 1))
            f_parts.append((-36, -26 - 36, 6, 0, 1))
            
        if fid == 10:
            f_parts.append((-38, -26 - 32, 5, 0, 1))
            f_parts.append((-36, -26 - 36, 6, 0, 1))
            f_parts.append((-31, -26 - 27, 7, 0, 1))
            
        if fid == 11:
            f_parts.append((-38, -26 - 32, 5, 0, 1))
            f_parts.append((-36, -26 - 36, 6, 0, 1))
            f_parts.append((-31, -26 - 27, 7, 0, 1))
            f_parts.append((-27, -26 - 20, 8, 0, 1))
            
        if fid in (12, 13):
            f_parts.append((-31 - 10, -26 - 27 - 4, 7, 0, 1))
            f_parts.append((-31 + 10, -26 - 27 - 4, 7, 0, 1))
            f_parts.append((-27 - 12, -26 - 20, 8, 0, 1))
            f_parts.append((-27 + 12, -26 - 20, 8, 0, 1))
            
        if fid in (14, 15, 16, 17, 18, 19, 20, 21, 22, 23):
            f_parts.append((-27, -26 - 20, 8, 0, 1))
            
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
    d1.line([(18, 78), (78, 18)], fill=(255, 255, 255), width=4)
    d1.line([(18, 78), (78, 18)], fill=(0, 229, 255, 180), width=7)
    d1.line([(22, 22), (74, 74)], fill=(128, 255, 255), width=3)
    draw_sparkle(d1, 48, 48, r=10, color=(255, 255, 255))
    draw_sparkle(d1, 74, 22, r=6, color=(0, 229, 255))

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

    # Icon 4: Skill 3 - Trận Pháp Trái Tim Ope (Icon File ID 4423)
    im_sk3 = Image.new('RGBA', (96, 96), (0, 0, 0, 0))
    d3 = ImageDraw.Draw(im_sk3)
    d3.rounded_rectangle([4, 4, 92, 92], radius=16, fill=(18, 12, 35, 245), outline=(0, 229, 255), width=3)
    d3.ellipse([12, 12, 84, 84], outline=(0, 229, 255, 220), width=2)
    d3.ellipse([20, 20, 76, 76], outline=(255, 64, 129, 180), width=2)
    for ang_i in [0, 90, 180, 270]:
        d3.arc([48 - 28, 48 - 28, 48 + 28, 48 + 28], start=ang_i, end=ang_i+70, fill=(0, 255, 220, 230), width=3)
    df_mini = d3
    df_mini.ellipse([32, 34, 48, 50], fill=(229, 57, 53, 240), outline=(255, 215, 0, 240), width=1)
    df_mini.ellipse([48, 34, 64, 50], fill=(229, 57, 53, 240), outline=(255, 215, 0, 240), width=1)
    df_mini.polygon([(33, 44), (63, 44), (48, 64)], fill=(229, 57, 53, 240))
    df_mini.line([(33, 44), (48, 64)], fill=(255, 215, 0, 240), width=1)
    df_mini.line([(63, 44), (48, 64)], fill=(255, 215, 0, 240), width=1)
    df_mini.arc([40, 40, 56, 56], start=45, end=270, fill=(255, 255, 255, 240), width=1)
    draw_sparkle(d3, 20, 20, r=7, color=(0, 229, 255))
    draw_sparkle(d3, 76, 20, r=6, color=(255, 215, 0))
    draw_sparkle(d3, 76, 76, r=7, color=(0, 229, 255))
    draw_sparkle(d3, 20, 76, r=6, color=(255, 215, 0))

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
        print(f"-> Đã tạo thành công Icon ID {icon_id} tại data/icon/ cho toàn bộ x0..x4")

if __name__ == '__main__':
    print("=== ĐANG TẠO SPRITE SHEETS & DATA EFFECT CHO TRÁI OPE OPE NO MI (ID 914..916) ===")
    create_effect_914_room_slash()
    create_effect_915_room_earth_detonation()
    create_effect_916_curtain_shield()
    print("=== ĐANG TẠO ICONS TRÁI ÁC QUỶ & KỸ NĂNG OPE OPE (ID 2191, 4421..4424) ===")
    create_all_ope_icons()
    print("=== HOÀN TẤT TOÀN BỘ TÀI NGUYÊN HÌNH ẢNH & HIỆU ỨNG TRÁI OPE OPE! ===")
