import os
import sys
import struct
import math
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
        if z in ['x3', 'x4'] and eff_id == 916:
            c = min(max_colors, 48)
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
def draw_anime_hemisphere_room_dome(w=864, h=540):
    """
    Vẽ VÒM NỬA HÌNH CẦU ROOM KHỔNG LỒ VÚT CAO (TOWERING WHITE-SKY BLUE ROOM DOME):
    - Kích thước vút cao: 4x: 864x540 (1x: 216x135), chiều cao vòm 115px
    - Lớp phủ màng không gian màu TRẮNG XANH DƯƠNG mờ ảo chuẩn Anime One Piece
    - Vành viền vòm cầu phát sáng rực rỡ với lõi trắng tinh chói lọi và hào quang xanh dương
    - Chân vòm tiếp đất có viền phát sáng sắc nét
    - Các đường kinh - vĩ tuyến 3D đồng quy chuẩn xác tại đỉnh (không bị phân tách 2 khung vòm)
    - Vệt ánh sáng bóng kính phản chiếu (anime glass sheen)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    cy_base = h - 44    # 496 tại 4x (124 tại 1x)
    rx = 396            # 99 tại 1x (rộng đáy 198px)
    ry = 40             # 10 tại 1x (elip đáy tiếp xúc đất)
    y_apex = 36         # 9 tại 1x
    h_dome = cy_base - y_apex # 460 tại 4x (115px tại 1x - VÒM VÚT CAO)

    # 1. Màng không gian Room trong suốt (Translucent spatial field)
    # Gradient bán cầu phủ từ viền ngoài vào tâm
    for f in range(100, 0, -2):
        factor = f / 100.0
        rx_i = int(rx * factor)
        hd_i = int(h_dome * factor)
        ry_i = int(ry * factor)
        alpha = int(110 * (factor ** 2.8)) # Nhạt dần về 0 ở tâm
        if alpha > 0:
            upper_arc = []
            for deg in range(180, 361, 3):
                rad = math.radians(deg)
                px = cx + int(math.cos(rad) * rx_i)
                py = cy_base + int(math.sin(rad) * hd_i)
                upper_arc.append((px, py))
            d.line(upper_arc, fill=(60, 165, 255, alpha), width=max(2, int(rx * 0.035)))
            
            lower_arc = []
            for deg in range(0, 181, 4):
                rad = math.radians(deg)
                px = cx + int(math.cos(rad) * rx_i)
                py = cy_base + int(math.sin(rad) * ry_i)
                lower_arc.append((px, py))
            d.line(lower_arc, fill=(60, 165, 255, int(alpha * 0.55)), width=max(2, int(rx * 0.025)))

    # 2. Các đường kinh tuyến 3D ĐỒNG QUY CHUẨN XÁC TẠI ĐỈNH (True 3D Meridians)
    for phi_deg in [-60, -35, -15, 15, 35, 60]:
        phi_rad = math.radians(phi_deg)
        m_pts = []
        for step in range(0, 91, 3): # 0 (đáy) đến 90 (đỉnh)
            theta_rad = math.radians(step)
            cos_t = math.cos(theta_rad)
            sin_t = math.sin(theta_rad)
            px = cx + int(rx * cos_t * math.sin(phi_rad))
            py = cy_base - int(h_dome * sin_t) + int(ry * cos_t * math.cos(phi_rad))
            m_pts.append((px, py))
        d.line(m_pts, fill=(170, 230, 255, 65), width=2)

    # Đường vĩ tuyến ngang (Latitude rings) chuẩn phối cảnh
    for lat_deg in [28, 56]:
        lat_rad = math.radians(lat_deg)
        cos_lat = math.cos(lat_rad)
        sin_lat = math.sin(lat_rad)
        rx_lat = int(rx * cos_lat)
        ry_lat = int(ry * cos_lat)
        y_lat = cy_base - int(h_dome * sin_lat)
        lat_pts = []
        for d_deg in range(0, 361, 6):
            r_deg = math.radians(d_deg)
            px = cx + int(rx_lat * math.sin(r_deg))
            py = y_lat + int(ry_lat * math.cos(r_deg))
            lat_pts.append((px, py))
        d.line(lat_pts, fill=(170, 230, 255, 55), width=2)

    # 3. Vệt ánh sáng bóng kính phản chiếu (Anime Glass Sheen) trên bề mặt vòm
    sheen_pts = []
    for deg in range(205, 270, 3):
        rad = math.radians(deg)
        px = cx + int(math.cos(rad) * (rx * 0.95))
        py = cy_base + int(math.sin(rad) * (h_dome * 0.97))
        sheen_pts.append((px, py))
    d.line(sheen_pts, fill=(255, 255, 255, 80), width=10)
    d.line(sheen_pts, fill=(255, 255, 255, 160), width=4)

    # 4. Hào quang tỏa sáng ra ngoài vòm cầu (Outer Aura Halo)
    for dr, a, w_line in [(14, 30, 6), (8, 60, 4), (4, 90, 3)]:
        dome_halo = []
        for deg in range(180, 361, 3):
            rad = math.radians(deg)
            px = cx + int(math.cos(rad) * (rx + dr))
            py = cy_base + int(math.sin(rad) * (h_dome + dr))
            dome_halo.append((px, py))
        d.line(dome_halo, fill=(100, 205, 255, a), width=w_line)

    # 5. VÀNH VÒM CẦU CHÍNH (MAIN ROOM DOME ARCH) - DÀY, MẠNH MẼ, PHÁT QUANG
    dome_main = []
    for deg in range(180, 361, 2):
        rad = math.radians(deg)
        px = cx + int(math.cos(rad) * rx)
        py = cy_base + int(math.sin(rad) * h_dome)
        dome_main.append((px, py))
    d.line(dome_main, fill=(25, 100, 255, 255), width=22)
    d.line(dome_main, fill=(50, 160, 255, 255), width=14)
    d.line(dome_main, fill=(130, 220, 255, 255), width=8)
    d.line(dome_main, fill=(255, 255, 255, 255), width=3)

    # 6. Chân vòm cầu tiếp đất dạng ellipse (viền tiếp đất sắc nét, phát sáng)
    d.ellipse([cx - rx - 6, cy_base - ry - 4, cx + rx + 6, cy_base + ry + 4], fill=(120, 210, 255, 25))
    d.ellipse([cx - rx, cy_base - ry, cx + rx, cy_base + ry], outline=(80, 190, 255, 200), width=4)
    d.ellipse([cx - rx + 3, cy_base - ry + 3, cx + rx - 3, cy_base + ry - 3], outline=(255, 255, 255, 240), width=2)

    # 7. Kim cương định vị không gian quanh vòm cầu (Space Marker Diamonds)
    for ang in range(195, 350, 22):
        rad = math.radians(ang)
        nx = cx + int(math.cos(rad) * rx)
        ny = cy_base + int(math.sin(rad) * h_dome)
        d.polygon([(nx, ny - 6), (nx + 5, ny), (nx, ny + 6), (nx - 5, ny)], 
                  fill=(255, 255, 255, 255), outline=(50, 180, 255, 255))

    return im

def draw_circling_smoke_puff(w=96, h=56, variant=0):
    """
    Vẽ cụm mây khói bụi & luồng gió cuốn chạy xoay tròn quanh viền chân Room:
    - Kích thước 4x: 96x56 (1x: 24x14) hoặc 112x60 (1x: 28x15)
    - Cuộn khói trắng ngà pha xanh dương mờ ảo, bồng bềnh
    - Lõi khói phát sáng trắng, vệt gió cuốn mượt mà
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    
    puffs = [
        (-18, 2, 18, (220, 240, 250, 150)),
        (-6, -4, 22, (235, 248, 255, 170)),
        (10, 0, 20, (230, 245, 252, 160)),
        (22, 4, 14, (215, 235, 248, 140)),
        (-6, -3, 14, (255, 255, 255, 210)),
        (8, 0, 12, (255, 255, 255, 200)),
    ] if variant == 0 else [
        (-22, 0, 16, (215, 235, 248, 140)),
        (-8, -5, 24, (235, 248, 255, 175)),
        (12, -2, 22, (230, 245, 252, 165)),
        (26, 3, 15, (220, 240, 250, 145)),
        (-8, -4, 15, (255, 255, 255, 220)),
        (10, -2, 14, (255, 255, 255, 205)),
    ]
    for px, py, r, col in puffs:
        # Thay vì ngang (width=2r, height=r), ta làm mây đứng lên (width=r*1.2, height=r*2)
        rw = int(r * 0.6)
        d.ellipse([cx + px - rw, cy + py - r, cx + px + rw, cy + py + r], fill=col)
        
    wind_y = cy + 4
    if variant == 0:
        d.arc([cx - 36, wind_y - 8, cx + 36, wind_y + 8], start=160, end=350, fill=(255, 255, 255, 180), width=2)
        d.arc([cx - 28, wind_y - 4, cx + 28, wind_y + 4], start=180, end=330, fill=(160, 225, 255, 140), width=2)
    else:
        d.arc([cx - 40, wind_y - 10, cx + 40, wind_y + 10], start=170, end=360, fill=(255, 255, 255, 190), width=2)
        d.arc([cx - 32, wind_y - 5, cx + 32, wind_y + 5], start=190, end=340, fill=(160, 225, 255, 150), width=2)
        
    return im

def draw_straight_kikoku_sword(w=64, h=176, is_trail=False, is_giant=False):
    """
    Vẽ kiếm Kikoku rơi THẲNG ĐỨNG (Góc 0 độ):
    - Mũi kiếm chúc thẳng 100% xuống dưới
    - Lưỡi kiếm sáng trắng phát quang lam ngọc
    - Vành Tsuba chữ thập trắng lông tơ
    - Chuôi kiếm tím hoa văn chữ thập
    - Vệt xé gió thẳng đứng phía trên chuôi kiếm
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2

    if is_trail:
        trail_pts = [(cx - 18, -20), (cx + 18, -20), (cx + 8, 88), (cx - 8, 88)]
        d.polygon(trail_pts, fill=(0, 200, 255, 120))
        d.line([(cx, -20), (cx, 88)], fill=(255, 255, 255, 200), width=5)
        d.line([(cx - 12, 10), (cx - 5, 75)], fill=(0, 240, 255, 160), width=3)
        d.line([(cx + 12, 10), (cx + 5, 75)], fill=(0, 240, 255, 160), width=3)

    y_pommel = 16 if not is_giant else 8
    y_tsuba = 54 if not is_giant else 48
    y_tip = 168 if not is_giant else 232

    blade_w = 8 if not is_giant else 14
    d.line([(cx, y_tsuba), (cx, y_tip)], fill=(0, 180, 255, 180), width=blade_w + 12)
    d.line([(cx, y_tsuba), (cx, y_tip)], fill=(0, 240, 255, 230), width=blade_w + 6)
    d.line([(cx, y_tsuba), (cx, y_tip - 6)], fill=(255, 255, 255, 255), width=blade_w)
    d.polygon([(cx - blade_w//2, y_tip - 6), (cx + blade_w//2, y_tip - 6), (cx, y_tip)], fill=(255, 255, 255, 255))

    tsuba_w = 24 if not is_giant else 34
    tsuba_h = 10 if not is_giant else 14
    d.ellipse([cx - tsuba_w//2, y_tsuba - tsuba_h//2, cx + tsuba_w//2, y_tsuba + tsuba_h//2], 
              fill=(252, 252, 252, 255), outline=(200, 200, 200, 255), width=2)
    d.ellipse([cx - 4, y_tsuba - 2, cx + 4, y_tsuba + 2], fill=(255, 215, 0))

    hilt_w = 6 if not is_giant else 8
    d.line([(cx, y_pommel), (cx, y_tsuba)], fill=(45, 15, 60, 255), width=hilt_w)
    cross_step = 8 if not is_giant else 10
    for y_c in range(y_pommel + 4, y_tsuba - 4, cross_step):
        d.line([(cx - 3, y_c - 2), (cx + 3, y_c + 2)], fill=(255, 255, 255, 240), width=1)
        d.line([(cx - 3, y_c + 2), (cx + 3, y_c - 2)], fill=(255, 255, 255, 240), width=1)
    d.ellipse([cx - 4, y_pommel - 4, cx + 4, y_pommel + 4], fill=(255, 215, 0, 255))

    draw_sparkle(d, cx, y_tip, r=10, color=(255, 255, 255, 255))
    return im

def draw_ground_crater_and_debris(w=160, h=80, phase=0):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    rx = 54 if phase == 0 else 66
    ry = 16 if phase == 0 else 20

    d.ellipse([cx - rx - 4, cy - ry - 4, cx + rx + 4, cy + ry + 4], fill=(16, 8, 4, 240))
    d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], fill=(8, 4, 2, 255))
    d.arc([cx - rx, cy - ry, cx + rx, cy + ry], start=0, end=180, fill=(80, 50, 30, 255), width=4)

    cracks = [
        [(-rx, 0), (-rx - 16, -4), (-rx - 28, 2)],
        [(rx, 0), (rx + 16, -2), (rx + 28, 4)],
        [(-int(rx*0.5), ry), (-int(rx*0.7), ry + 8)],
        [(int(rx*0.5), ry), (int(rx*0.7), ry + 8)],
    ]
    for pts in cracks:
        for i in range(len(pts)-1):
            p1 = (cx + pts[i][0], cy + pts[i][1])
            p2 = (cx + pts[i+1][0], cy + pts[i+1][1])
            d.line([p1, p2], fill=(0, 255, 240, 220), width=3)
            d.line([p1, p2], fill=(255, 255, 255, 240), width=1)

    debris_left = [
        [(cx - rx - 10, cy - 16), 8, (120, 80, 50)],
        [(cx - rx + 4, cy - 24), 10, (140, 95, 60)],
        [(cx - int(rx*0.5), cy - 28), 7, (100, 65, 45)],
    ] if phase==0 else [
        [(cx - rx - 18, cy - 22), 11, (130, 85, 60)],
        [(cx - rx - 4, cy - 36), 13, (150, 105, 70)],
        [(cx - int(rx*0.6), cy - 42), 9, (110, 75, 50)],
    ]
    debris_right = [
        [(cx + rx + 8, cy - 14), 8, (125, 85, 60)],
        [(cx + rx - 6, cy - 24), 11, (145, 100, 70)],
        [(cx + int(rx*0.5), cy - 26), 7, (105, 70, 50)],
    ] if phase==0 else [
        [(cx + rx + 16, cy - 24), 12, (135, 90, 65)],
        [(cx + rx + 2, cy - 38), 14, (155, 110, 75)],
        [(cx + int(rx*0.6), cy - 44), 9, (115, 80, 55)],
    ]

    for (bx, by), sz, col in debris_left + debris_right:
        d.polygon([(bx - sz, by), (bx - sz//2, by - sz), (bx + sz//2, by - sz//2), (bx + sz, by + sz//3), (bx, by + sz)], 
                  fill=col, outline=(40, 25, 18, 255))
        d.line([(bx - sz//2, by - sz), (bx + sz//2, by - sz//2)], fill=(210, 180, 150, 255), width=2)
    
    draw_sparkle(d, cx, cy, r=10, color=(0, 255, 255, 230))
    return im

def draw_sword_stuck_in_crater(w=96, h=144):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx = w // 2
    y_ground = 108
    y_pommel = 20
    y_tsuba = 66

    d.ellipse([cx - 40, y_ground - 14, cx + 40, y_ground + 18], fill=(18, 10, 6, 245))
    d.ellipse([cx - 28, y_ground - 8, cx + 28, y_ground + 10], fill=(8, 4, 2, 255))
    d.arc([cx - 40, y_ground - 14, cx + 40, y_ground + 18], start=0, end=180, fill=(90, 58, 38, 255), width=4)

    for dx1, dy1, dx2, dy2 in [(-36, 4, -18, 2), (-18, 2, 0, 0), (0, 0, 20, 3), (20, 3, 38, 6), (0, 0, 8, 14), (0, 0, -10, 12)]:
        d.line([(cx + dx1, y_ground + dy1), (cx + dx2, y_ground + dy2)], fill=(0, 255, 240, 220), width=3)
        d.line([(cx + dx1, y_ground + dy1), (cx + dx2, y_ground + dy2)], fill=(255, 255, 255, 240), width=1)

    d.polygon([(cx - 14, y_ground + 4), (cx - 8, y_ground - 10), (cx + 8, y_ground - 8), (cx + 14, y_ground + 4)], 
              fill=(120, 80, 50), outline=(45, 30, 20))

    d.line([(cx, y_tsuba), (cx, y_ground)], fill=(0, 229, 255, 180), width=10)
    d.line([(cx, y_tsuba), (cx, y_ground)], fill=(255, 255, 255, 255), width=6)
    d.ellipse([cx - 12, y_tsuba - 5, cx + 12, y_tsuba + 5], fill=(252, 252, 252), outline=(180, 180, 180), width=2)
    d.line([(cx, y_pommel), (cx, y_tsuba)], fill=(45, 15, 60), width=6)
    d.ellipse([cx - 4, y_pommel - 4, cx + 4, y_pommel + 4], fill=(255, 215, 0))
    return im

def draw_massive_climax_crater(w=256, h=104):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    rx, ry = 98, 28

    d.ellipse([cx - rx - 6, cy - ry - 4, cx + rx + 6, cy + ry + 6], fill=(16, 8, 4, 240))
    d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], fill=(8, 3, 1, 255))
    d.ellipse([cx - int(rx*0.65), cy - int(ry*0.65), cx + int(rx*0.65), cy + int(ry*0.65)], fill=(0, 0, 0, 255))

    d.arc([cx - rx, cy - ry, cx + rx, cy + ry], start=0, end=180, fill=(100, 65, 45, 255), width=6)

    for pts in [
        [(-rx, 0), (-rx - 24, -10), (-rx - 44, -4)],
        [(rx, 0), (rx + 24, -8), (rx + 44, 4)],
        [(-int(rx*0.6), ry), (-int(rx*0.8), ry + 14), (-int(rx*1.0), ry + 20)],
        [(int(rx*0.6), ry), (int(rx*0.8), ry + 14), (int(rx*1.0), ry + 20)],
    ]:
        for i in range(len(pts)-1):
            p1 = (cx + pts[i][0], cy + pts[i][1])
            p2 = (cx + pts[i+1][0], cy + pts[i+1][1])
            d.line([p1, p2], fill=(0, 255, 240, 230), width=4)
            d.line([p1, p2], fill=(255, 255, 255, 255), width=2)

    rocks = [
        [(cx - 85, cy - 35), 14, (130, 85, 55)],
        [(cx - 45, cy - 50), 18, (150, 100, 65)],
        [(cx + 45, cy - 52), 18, (150, 100, 65)],
        [(cx + 85, cy - 36), 15, (130, 85, 55)],
        [(cx - 110, cy - 20), 12, (110, 75, 50)],
        [(cx + 110, cy - 22), 12, (110, 75, 50)],
    ]
    for (bx, by), sz, col in rocks:
        d.polygon([(bx - sz, by), (bx - sz//2, by - sz), (bx + sz//2, by - sz//2), (bx + sz, by + sz//3), (bx, by + sz)], 
                  fill=col, outline=(30, 18, 12, 255))
        d.line([(bx - sz//2, by - sz), (bx + sz//2, by - sz//2)], fill=(220, 190, 160, 255), width=3)

    draw_sparkle(d, cx, cy, r=18, color=(255, 255, 255, 255))
    return im

def draw_ground_shockwave(w=160, h=64):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    rx, ry = 74, 26
    d.ellipse([cx - rx - 4, cy - ry - 4, cx + rx + 4, cy + ry + 4], fill=(0, 200, 255, 30))
    d.ellipse([cx - rx, cy - ry, cx + rx, cy + ry], outline=(0, 229, 255, 180), width=4)
    d.ellipse([cx - int(rx*0.75), cy - int(ry*0.75), cx + int(rx*0.75), cy + int(ry*0.75)], outline=(0, 255, 240, 220), width=3)
    d.ellipse([cx - int(rx*0.45), cy - int(ry*0.45), cx + int(rx*0.45), cy + int(ry*0.45)], outline=(255, 255, 255, 255), width=2)
    return im

def draw_slash_burst(w=160, h=144):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    d.line([(cx - 56, cy - 40), (cx + 56, cy + 40)], fill=(0, 229, 255, 180), width=9)
    d.line([(cx - 56, cy - 40), (cx + 56, cy + 40)], fill=(255, 255, 255, 255), width=4)
    d.line([(cx - 50, cy + 42), (cx + 50, cy - 42)], fill=(0, 255, 200, 180), width=7)
    d.line([(cx - 50, cy + 42), (cx + 50, cy - 42)], fill=(255, 255, 255, 255), width=3)
    draw_sparkle(d, cx, cy, r=22, color=(255, 255, 255, 255))
    d.ellipse([cx - 10, cy - 10, cx + 10, cy + 10], fill=(255, 255, 255, 240))
    for dx, dy in [(-30, -20), (32, 18), (-22, 28), (28, -25)]:
        draw_sparkle(d, cx + dx, cy + dy, r=8, color=(0, 240, 255, 230))
    return im

def draw_dissolve_sparks(w=144, h=128):
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    sparks = [
        (0, 0, 14, (255, 255, 255, 255)),
        (-35, -25, 9, (0, 240, 255, 240)),
        (38, -20, 11, (128, 255, 255, 240)),
        (-25, 28, 10, (0, 229, 255, 220)),
        (32, 26, 8, (0, 255, 210, 220)),
        (-12, -40, 7, (255, 255, 255, 200)),
        (16, 40, 7, (0, 240, 255, 200))
    ]
    for sx, sy, sr, col in sparks:
        draw_sparkle(d, cx + sx, cy + sy, r=sr, color=col)
    return im

def create_effect_914_room_slash():
    scale = 4
    w_sheet = 254 * scale  # 1016
    h_sheet = 206 * scale  # 824
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    im_room           = draw_anime_hemisphere_room_dome(216*scale, 135*scale)          # 864x540
    im_sword_straight = draw_straight_kikoku_sword(16*scale, 44*scale, is_trail=False) # 64x176
    im_sword_trail    = draw_straight_kikoku_sword(16*scale, 44*scale, is_trail=True)  # 64x176

    im_crater1        = draw_ground_crater_and_debris(40*scale, 20*scale, phase=0)     # 160x80
    im_crater2        = draw_ground_crater_and_debris(44*scale, 22*scale, phase=1)     # 176x88
    im_sword_crater   = draw_sword_stuck_in_crater(24*scale, 36*scale)                 # 96x144
    im_shockwave      = draw_ground_shockwave(40*scale, 16*scale)                      # 160x64

    im_giant_sword    = draw_straight_kikoku_sword(24*scale, 60*scale, is_trail=True, is_giant=True) # 96x240
    im_massive_crater = draw_massive_climax_crater(64*scale, 26*scale)                 # 256x104
    im_slash_burst    = draw_slash_burst(40*scale, 36*scale)                            # 160x144
    im_dissolve       = draw_dissolve_sparks(32*scale, 32*scale)                       # 128x128

    im_smoke_puff1    = draw_circling_smoke_puff(24*scale, 14*scale, variant=0)        # 96x56
    im_smoke_puff2    = draw_circling_smoke_puff(28*scale, 15*scale, variant=1)        # 112x60

    # Bố trí Sprite vào Sheet
    im.paste(im_room, (0*scale, 0*scale), im_room)                                 # Sprite 0 (x=0, y=0, w=216, h=135)

    im.paste(im_sword_straight, (218*scale, 0*scale), im_sword_straight)          # Sprite 1 (x=218, y=0, w=16, h=44)
    im.paste(im_sword_trail,    (236*scale, 0*scale), im_sword_trail)             # Sprite 2 (x=236, y=0, w=16, h=44)
    im.paste(im_sword_crater,   (218*scale, 46*scale), im_sword_crater)           # Sprite 5 (x=218, y=46, w=24, h=36)
    im.paste(im_giant_sword,    (218*scale, 84*scale), im_giant_sword)            # Sprite 7 (x=218, y=84, w=24, h=60)

    im.paste(im_massive_crater, (0*scale, 138*scale), im_massive_crater)          # Sprite 8 (x=0, y=138, w=64, h=26)
    im.paste(im_crater1,        (66*scale, 138*scale), im_crater1)                # Sprite 3 (x=66, y=138, w=40, h=20)
    im.paste(im_crater2,        (108*scale, 138*scale), im_crater2)               # Sprite 4 (x=108, y=138, w=44, h=22)
    im.paste(im_shockwave,      (154*scale, 138*scale), im_shockwave)             # Sprite 6 (x=154, y=138, w=40, h=16)
    im.paste(im_slash_burst,    (0*scale, 166*scale), im_slash_burst)             # Sprite 9 (x=0, y=166, w=40, h=36)
    im.paste(im_dissolve,       (42*scale, 166*scale), im_dissolve)               # Sprite 10 (x=42, y=166, w=32, h=32)
    im.paste(im_smoke_puff1,    (76*scale, 166*scale), im_smoke_puff1)            # Sprite 11 (x=76, y=166, w=24, h=14)
    im.paste(im_smoke_puff2,    (102*scale, 166*scale), im_smoke_puff2)           # Sprite 12 (x=102, y=166, w=28, h=15)

    small_imgs = [
        (0,    0,   0, 216, 135), # Sprite 0: Vòm Nửa Hình Cầu Room Cao Trắng Xanh Dương
        (1,  218,   0,  16,  44), # Sprite 1: Kiếm Kikoku rơi thẳng
        (2,  236,   0,  16,  44), # Sprite 2: Kiếm Kikoku rơi thẳng xé gió
        (3,   66, 138,  40,  20), # Sprite 3: Hố lúm đất + đá văng pha 1
        (4,  108, 138,  44,  22), # Sprite 4: Hố lúm đất + đá văng bùng nổ pha 2
        (5,  218,  46,  24,  36), # Sprite 5: Kiếm cắm ngập trong hố lúm đất
        (6,  154, 138,  40,  16), # Sprite 6: Sóng chấn động lún đất
        (7,  218,  84,  24,  60), # Sprite 7: Đại Thần Kiếm khổng lồ rơi thẳng
        (8,    0, 138,  64,  26), # Sprite 8: Đại hố lún cực đại
        (9,    0, 166,  40,  36), # Sprite 9: Vết chém nứt rách không gian
        (10,  42, 166,  32,  32), # Sprite 10: Tinh thể tan biến
        (11,  76, 166,  24,  14), # Sprite 11: Khói bụi xoáy tròn chân đế 1
        (12, 102, 166,  28,  15), # Sprite 12: Khói bụi xoáy tròn chân đế 2
    ]

    raw_frames = [
        # Frame 0: Vòm bán cầu Room bắt đầu mở rộng (0.0s)
        [(-108, -124, 0, 0, 0)],
        
        # Frame 1: Vòm bán cầu Room phát quang trắng xanh (0.12s)
        [(-108, -124, 0, 0, 0)],
        
        # Frame 2: Vòm Room + Kiếm 1 rơi thẳng trên cao ở giữa (0.24s)
        [
            (-108, -124, 0, 0, 0),
            (  -8,  -96, 2, 0, 1),
        ],
        
        # Frame 3: Vòm Room + Kiếm 1 lao nhanh thẳng xuống giữa (0.40s)
        [
            (-108, -124, 0, 0, 0),
            (  -8,  -52, 2, 0, 1),
        ],
        
        # Frame 4: Vòm Room + Kiếm 1 CẮM ĐẤT + Kiếm 2 rơi thẳng trái (0.56s)
        [
            (-108, -124, 0, 0, 0),
            ( -12,  -30, 5, 0, 1),
            ( -20,  -14, 3, 0, 1),
            ( -20,   -8, 6, 0, 1),
            ( -48,  -96, 2, 0, 1),
        ],
        
        # Frame 5: Vòm Room + Hố lúm giữa nổ đá + Kiếm 2 lao + Kiếm 3 trên cao phải (0.72s)
        [
            (-108, -124, 0, 0, 0),
            ( -12,  -30, 5, 0, 1),
            ( -22,  -14, 4, 0, 1),
            ( -48,  -52, 2, 0, 1),
            (  40,  -96, 2, 0, 1),
        ],
        
        # Frame 6: Vòm Room + Kiếm 2 CẮM ĐẤT TRÁI + Kiếm 3 lao phải (0.88s)
        [
            (-108, -124, 0, 0, 0),
            ( -12,  -30, 5, 0, 1),
            ( -52,  -30, 5, 0, 1),
            ( -60,  -14, 3, 0, 1),
            ( -60,   -8, 6, 0, 1),
            (  40,  -52, 2, 0, 1),
        ],
        
        # Frame 7: Vòm Room + Kiếm 3 CẮM ĐẤT PHẢI + Kiếm 4 rơi xa trái (1.04s)
        [
            (-108, -124, 0, 0, 0),
            ( -12,  -30, 5, 0, 1),
            ( -52,  -30, 5, 0, 1),
            (  36,  -30, 5, 0, 1),
            (  28,  -14, 4, 0, 1),
            (  28,   -8, 6, 0, 1),
            ( -75,  -96, 2, 0, 1),
        ],
        
        # Frame 8: Vòm Room + Kiếm 4 lao xa trái + Kiếm 5 rơi xa phải (1.20s)
        [
            (-108, -124, 0, 0, 0),
            ( -12,  -30, 5, 0, 1),
            ( -52,  -30, 5, 0, 1),
            (  36,  -30, 5, 0, 1),
            ( -75,  -52, 2, 0, 1),
            (  65,  -96, 2, 0, 1),
        ],
        
        # Frame 9: Vòm Room + Kiếm 4 CẮM ĐẤT + Kiếm 5 lao xa phải (1.36s)
        [
            (-108, -124, 0, 0, 0),
            ( -12,  -30, 5, 0, 1),
            ( -52,  -30, 5, 0, 1),
            (  36,  -30, 5, 0, 1),
            ( -79,  -30, 5, 0, 1),
            ( -85,  -14, 3, 0, 1),
            ( -85,   -8, 6, 0, 1),
            (  65,  -52, 2, 0, 1),
            (  -2,  -96, 2, 0, 1),
        ],
        
        # Frame 10: Vòm Room + Kiếm 5 CẮM ĐẤT + Kiếm 6 lao giữa (1.52s)
        [
            (-108, -124, 0, 0, 0),
            ( -52,  -30, 5, 0, 1),
            (  36,  -30, 5, 0, 1),
            (  61,  -30, 5, 0, 1),
            (  53,  -14, 4, 0, 1),
            (  -2,  -52, 2, 0, 1),
            ( -30,  -96, 2, 0, 1),
        ],
        
        # Frame 11: Vòm Room + Kiếm 6 CẮM ĐẤT + Kiếm 7 lao nhanh (1.68s)
        [
            (-108, -124, 0, 0, 0),
            ( -52,  -30, 5, 0, 1),
            (  61,  -30, 5, 0, 1),
            (  -6,  -30, 5, 0, 1),
            ( -14,  -14, 3, 0, 1),
            ( -14,   -8, 6, 0, 1),
            ( -30,  -52, 2, 0, 1),
            (  24,  -96, 2, 0, 1),
        ],
        
        # Frame 12: Vòm Room + Kiếm 7 CẮM ĐẤT + Kiếm 8 lao lệch phải (1.84s)
        [
            (-108, -124, 0, 0, 0),
            (  -6,  -30, 5, 0, 1),
            ( -34,  -30, 5, 0, 1),
            ( -42,  -14, 4, 0, 1),
            (  24,  -52, 2, 0, 1),
            ( -54,  -96, 2, 0, 1),
        ],
        
        # Frame 13: Vòm Room + Kiếm 8 CẮM ĐẤT + Kiếm 9 lao nhanh (2.00s)
        [
            (-108, -124, 0, 0, 0),
            ( -34,  -30, 5, 0, 1),
            (  20,  -30, 5, 0, 1),
            (  12,  -14, 3, 0, 1),
            (  12,   -8, 6, 0, 1),
            ( -54,  -52, 2, 0, 1),
            (  48,  -96, 2, 0, 1),
        ],
        
        # Frame 14: Vòm Room + Kiếm 9 CẮM ĐẤT + Kiếm 10 lao nhanh (2.16s)
        [
            (-108, -124, 0, 0, 0),
            (  20,  -30, 5, 0, 1),
            ( -58,  -30, 5, 0, 1),
            ( -66,  -14, 4, 0, 1),
            (  48,  -52, 2, 0, 1),
        ],
        
        # Frame 15: Vòm Room + Kiếm 10 CẮM ĐẤT + 2 Kiếm 11 & 12 cùng rơi (2.32s)
        [
            (-108, -124, 0, 0, 0),
            ( -58,  -30, 5, 0, 1),
            (  44,  -30, 5, 0, 1),
            (  36,  -14, 3, 0, 1),
            (  36,   -8, 6, 0, 1),
            ( -44,  -96, 2, 0, 1),
            (  36,  -96, 2, 0, 1),
        ],
        
        # Frame 16: Vòm Room + 2 Kiếm lao nhanh thẳng xuống (2.48s)
        [
            (-108, -124, 0, 0, 0),
            ( -58,  -30, 5, 0, 1),
            (  44,  -30, 5, 0, 1),
            ( -44,  -52, 2, 0, 1),
            (  36,  -52, 2, 0, 1),
        ],
        
        # Frame 17: Vòm Room + 2 Kiếm CẮM ĐẤT + ĐẠI THẦN KIẾM xuất hiện trên đỉnh (2.64s)
        [
            (-108, -124, 0, 0, 0),
            ( -48,  -30, 5, 0, 1),
            (  32,  -30, 5, 0, 1),
            ( -56,  -14, 4, 0, 1),
            (  24,  -14, 4, 0, 1),
            ( -12, -104, 7, 0, 1),
        ],
        
        # Frame 18: Vòm Room + Đại Thần Kiếm lao nhanh thẳng xuống (2.80s)
        [
            (-108, -124, 0, 0, 0),
            ( -48,  -30, 5, 0, 1),
            (  32,  -30, 5, 0, 1),
            ( -12,  -58, 7, 0, 1),
        ],
        
        # Frame 19: Vòm Room + Đại Thần Kiếm sát mặt đất + Chấn động lún (2.96s)
        [
            (-108, -124, 0, 0, 0),
            ( -48,  -30, 5, 0, 1),
            (  32,  -30, 5, 0, 1),
            ( -12,  -26, 7, 0, 1),
            ( -20,   -8, 6, 0, 1),
        ],
        
        # Frame 20: ĐẠI THẦN KIẾM CẮM ĐẤT! TẠO ĐẠI HỐ LÚN + ĐÁ VĂNG (3.12s)
        [
            (-108, -124, 0, 0, 0),
            ( -32,  -16, 8, 0, 1),
            ( -12,  -38, 7, 0, 1),
            ( -20,  -28, 9, 0, 1),
        ],
        
        # Frame 21: Toàn bộ vòm Room chấn động cực đại + Đại hố lún phát quang (3.28s)
        [
            (-108, -124, 0, 0, 0),
            ( -32,  -16, 8, 0, 1),
            ( -12,  -38, 7, 0, 1),
            ( -20,  -28, 9, 0, 1),
        ],
        
        # Frame 22: Không gian nổ bùng: Vết nứt lớn + Hạt tinh thể (3.44s)
        [
            (-108, -124, 0, 0, 0),
            ( -20,  -28, 9, 0, 1),
            ( -38,  -12, 10, 0, 1),
            (   0,  -12, 10, 0, 1),
            (  28,  -12, 10, 0, 1),
        ],
        
        # Frame 23: Vòm Room tan biến + Tinh thể lấp lánh (3.60s)
        [
            ( -32,  -14, 10, 0, 1),
            (  -4,  -18, 10, 0, 1),
            (  24,  -14, 10, 0, 1),
        ],
        
        # Frame 24: Bụi tinh thể bay tỏa nhẹ nhàng (3.76s)
        [
            ( -26,  -20, 10, 0, 1),
            (  18,  -22, 10, 0, 1),
        ],
        
        # Frame 25: Tia lấp lánh cuối cùng tan biến (3.92s - 4.00s)
        [
            (  -2,  -24, 10, 0, 1),
        ],
    ]

    # Bổ sung 8 cụm khói bụi chạy xoay tròn xung quanh viền đáy Room cho từng frame
    frames = []
    for f_idx, rf in enumerate(raw_frames):
        f_list = list(rf)
        base_ang = (f_idx * 15) % 360
        for k in range(8):
            ang = (k * 45 + base_ang) % 360
            rad = math.radians(ang)
            px = int(99 * math.cos(rad)) - (12 if k % 2 == 0 else 14)
            py = int(10 * math.sin(rad)) - 7
            sp_id = 11 if k % 2 == 0 else 12
            layer = 0 if math.sin(rad) < 0 else 1
            f_list.append((px, py, sp_id, 0, layer))
        frames.append(f_list)

    seq = (
        [0, 0, 0] +                 # 3 ticks (Frame 0)
        [1, 1, 1] +                 # 3 ticks (Frame 1)
        [2, 2, 2, 2] +              # 4 ticks (Frame 2)
        [f for f in range(3, 25) for _ in range(4)] +  # 22 frames * 4 = 88 ticks (Frames 3..24)
        [25, 25]                    # 2 ticks (Frame 25)
    )

    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(914, im, data_bytes)

# ==============================================================================
# 2. TẠO EFFECT 915: ROOM HÌNH CẦU & ĐÁ ĐẨY VÔ PLAYER TRÚNG ĐÒN (SPHERE ROCK CRUSH)
# ==============================================================================
def draw_tact_gamma_sphere(w, h):
    """
    Sprite 0: Vòng cầu 3D Room bao trọn người chơi bị trúng đòn (96x96 ở 1x -> 384x384 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    r = int(w * 0.46)  # ~176 tại 4x (~44 tại 1x)

    # Nền màng năng lượng hình cầu - Nhạt dần vào tâm
    for f in range(100, 0, -5):
        factor = f / 100.0
        r_i = int(r * factor)
        alpha = int(70 * (factor ** 2))
        if alpha > 0:
            d.ellipse([cx - r_i, cy - r_i, cx + r_i, cy + r_i], outline=(80, 220, 255, alpha), width=max(2, int(r*0.05)))

    # Viền phát quang đa tầng dầy và uy lực
    d.ellipse([cx - r - 12, cy - r - 12, cx + r + 12, cy + r + 12], outline=(0, 160, 255, 60), width=10)
    d.ellipse([cx - r - 6, cy - r - 6, cx + r + 6, cy + r + 6], outline=(0, 220, 255, 140), width=7)
    d.ellipse([cx - r,     cy - r,     cx + r,     cy + r],     outline=(0, 255, 240, 255), width=5)
    d.ellipse([cx - r + 2, cy - r + 2, cx + r - 2, cy + r - 2], outline=(255, 255, 255, 255), width=3)

    # Vĩ tuyến xích đạo 3D (Equator)
    rx = r - 4
    ry = int(r * 0.35)
    d.arc([cx - rx, cy - ry, cx + rx, cy + ry], start=0,   end=180, fill=(0, 255, 240, 200), width=3)
    d.arc([cx - rx, cy - ry, cx + rx, cy + ry], start=180, end=360, fill=(0, 200, 255, 80),  width=2)

    # Kinh tuyến 3D (Meridian)
    rx_lon = int(r * 0.38)
    ry_lon = r - 4
    d.arc([cx - rx_lon, cy - ry_lon, cx + rx_lon, cy + ry_lon], start=270, end=90,  fill=(0, 255, 240, 180), width=3)
    d.arc([cx - rx_lon, cy - ry_lon, cx + rx_lon, cy + ry_lon], start=90,  end=270, fill=(0, 200, 255, 75),  width=2)

    # Vệt bóng kính phản quang cong (Anime Sheen)
    d.arc([cx - r + 24, cy - r + 24, cx + r - 60, cy + r - 60], start=190, end=270, fill=(255, 255, 255, 220), width=5)
    d.arc([cx - r + 36, cy - r + 36, cx + r - 80, cy + r - 80], start=200, end=260, fill=(210, 250, 255, 140), width=3)

    # 4 Hạt tinh thể phát sáng tại điểm nút hình cầu
    draw_sparkle(d, cx - rx, cy, r=10, color=(255, 255, 255, 255))
    draw_sparkle(d, cx + rx, cy, r=10, color=(255, 255, 255, 255))
    draw_sparkle(d, cx, cy - ry_lon, r=10, color=(255, 255, 255, 255))
    draw_sparkle(d, cx, cy + ry_lon, r=10, color=(255, 255, 255, 255))
    return im

def draw_crushing_boulder_a(w, h):
    """
    Sprite 1: Tảng đá góc cạnh lớn A (28x24 ở 1x -> 112x96 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    pts = [(20, 16), (56, 8), (96, 24), (104, 60), (76, 88), (28, 80), (8, 48)]
    d.polygon(pts, fill=(62, 58, 66, 255))
    d.polygon([(20, 16), (56, 8), (64, 44), (28, 52)], fill=(94, 90, 100, 255))
    d.polygon([(64, 44), (104, 60), (76, 88), (48, 72)], fill=(38, 34, 42, 255))
    d.line([(24, 24), (44, 36), (64, 44), (76, 68)], fill=(0, 255, 220, 240), width=4)
    d.line([(24, 24), (44, 36), (64, 44), (76, 68)], fill=(255, 255, 255, 255), width=2)
    d.line(pts + [pts[0]], fill=(0, 220, 255, 120), width=3)
    return im

def draw_crushing_boulder_b(w, h):
    """
    Sprite 2: Tảng đá góc cạnh lớn B (28x24 ở 1x -> 112x96 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    pts = [(16, 28), (48, 12), (92, 16), (100, 52), (84, 84), (36, 88), (12, 64)]
    d.polygon(pts, fill=(58, 54, 62, 255))
    d.polygon([(48, 12), (92, 16), (68, 48), (36, 40)], fill=(92, 88, 98, 255))
    d.polygon([(68, 48), (100, 52), (84, 84), (44, 68)], fill=(36, 32, 40, 255))
    d.line([(48, 12), (52, 48), (36, 88)], fill=(0, 255, 220, 240), width=4)
    d.line([(48, 12), (52, 48), (36, 88)], fill=(255, 255, 255, 255), width=2)
    d.line([(52, 48), (84, 56)], fill=(0, 255, 220, 200), width=3)
    d.line(pts + [pts[0]], fill=(0, 220, 255, 120), width=3)
    return im

def draw_crushing_rock_spire(w, h):
    """
    Sprite 3: Mảnh đá nhọn lao nhanh (22x18 ở 1x -> 88x72 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    pts = [(8, 36), (44, 12), (80, 24), (68, 56), (36, 60)]
    d.polygon(pts, fill=(68, 64, 72, 255))
    d.polygon([(8, 36), (44, 12), (48, 38)], fill=(100, 96, 106, 255))
    d.polygon([(48, 38), (80, 24), (68, 56)], fill=(42, 38, 46, 255))
    d.line([(8, 36), (48, 38), (80, 24)], fill=(0, 255, 210, 240), width=3)
    d.line([(8, 36), (48, 38), (80, 24)], fill=(255, 255, 255, 255), width=1)
    d.line(pts + [pts[0]], fill=(0, 220, 255, 130), width=2)
    return im

def draw_crushing_rock_slab(w, h):
    """
    Sprite 4: Khối đá tảng dẹp (26x20 ở 1x -> 104x80 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    pts = [(12, 20), (52, 10), (96, 18), (92, 64), (48, 72), (16, 58)]
    d.polygon(pts, fill=(55, 50, 58, 255))
    d.polygon([(12, 20), (52, 10), (60, 42), (24, 48)], fill=(88, 82, 92, 255))
    d.polygon([(60, 42), (96, 18), (92, 64), (52, 58)], fill=(34, 30, 36, 255))
    d.line([(12, 20), (60, 42), (92, 64)], fill=(0, 255, 220, 240), width=4)
    d.line([(12, 20), (60, 42), (92, 64)], fill=(255, 255, 255, 255), width=2)
    d.line(pts + [pts[0]], fill=(0, 220, 255, 120), width=3)
    return im

def draw_crushing_impact_shockwave(w, h):
    """
    Sprite 5: Sóng xung kích va đập cực đại (50x42 ở 1x -> 200x168 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    d.ellipse([cx - 86, cy - 68, cx + 86, cy + 68], outline=(0, 220, 255, 100), width=8)
    d.ellipse([cx - 74, cy - 58, cx + 74, cy + 58], outline=(0, 255, 240, 210), width=5)
    d.ellipse([cx - 62, cy - 48, cx + 62, cy + 48], outline=(255, 255, 255, 255), width=3)
    for deg in range(0, 360, 30):
        rad = math.radians(deg)
        x1 = cx + int(math.cos(rad) * 62)
        y1 = cy + int(math.sin(rad) * 48)
        x2 = cx + int(math.cos(rad) * 94)
        y2 = cy + int(math.sin(rad) * 76)
        d.line([(x1, y1), (x2, y2)], fill=(0, 255, 230, 180), width=3)
    return im

def draw_plasma_lightning_burst(w, h):
    """
    Sprite 6: Tia sét Plasma Gamma & bùng nổ va đập (46x46 ở 1x -> 184x184 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    d.ellipse([cx - 16, cy - 16, cx + 16, cy + 16], fill=(255, 255, 255, 255))
    d.ellipse([cx - 24, cy - 24, cx + 24, cy + 24], outline=(0, 255, 220, 200), width=4)
    branches = [
        [(cx, cy), (cx - 24, cy - 32), (cx - 48, cy - 44), (cx - 72, cy - 60)],
        [(cx, cy), (cx + 28, cy - 28), (cx + 52, cy - 48), (cx + 74, cy - 62)],
        [(cx, cy), (cx - 36, cy + 12), (cx - 56, cy + 24), (cx - 76, cy + 38)],
        [(cx, cy), (cx + 34, cy + 14), (cx + 60, cy + 26), (cx + 78, cy + 42)],
        [(cx, cy), (cx - 18, cy + 40), (cx - 32, cy + 64)],
        [(cx, cy), (cx + 18, cy + 38), (cx + 36, cy + 64)],
    ]
    for b in branches:
        draw_lightning(d, b, color_glow=(0, 255, 210, 210), color_core=(255, 255, 255, 255), width=3)
    for sx, sy in [(-72, -60), (74, -62), (-76, 38), (78, 42), (-32, 64), (36, 64)]:
        draw_sparkle(d, cx + sx, cy + sy, r=8, color=(255, 255, 255, 255))
    return im

def draw_shattered_rock_fragments(w, h):
    """
    Sprite 7: Mảnh đá vỡ vụn văng tứ phía (40x36 ở 1x -> 160x144 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
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
        pts = [(cx + px, cy + py) for px, py in c]
        d.polygon(pts, fill=(70, 65, 75, 255), outline=(0, 255, 210, 220))
    for sx, sy in [(-44, -36), (46, -30), (-46, 16), (48, 28), (0, -54), (0, 48)]:
        draw_sparkle(d, cx + sx, cy + sy, r=6, color=(0, 255, 230, 220))
    return im

def draw_crush_dust_puff(w, h):
    """
    Sprite 8: Khói bụi va đập tản mác (36x26 ở 1x -> 144x104 ở 4x)
    """
    im = Image.new('RGBA', (w, h), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w // 2, h // 2
    for bx, by, br in [(-30, 6, 18), (30, 6, 19), (-48, 8, 13), (48, 8, 13), (0, 2, 17)]:
        d.ellipse([cx + bx - br, cy + by - br, cx + bx + br, cy + by + br], fill=(120, 110, 118, 140))
        d.ellipse([cx + bx - br + 3, cy + by - br + 3, cx + bx + br - 3, cy + by + br - 3], fill=(155, 145, 150, 110))
    for sx, sy in [(-32, -12), (32, -14), (0, -8)]:
        draw_sparkle(d, cx + sx, cy + sy, r=7, color=(0, 255, 210, 180))
    return im

def create_effect_915_room_earth_detonation():
    """
    Tạo Effect 915: Room Hình Cầu & Đá Đẩy Vò Player Trúng Đòn (SPHERE ROCK CRUSH):
    1. Mục tiêu đơn: Tạo phòng Room hình cầu 3D ôm trọn người chơi bị trúng đòn.
    2. Các tảng đá khổng lồ xuất hiện xung quanh viền hình cầu rồi bị đẩy/lao mạnh vào người chơi.
    3. Va chạm nghiền nát dữ dội tại tâm (ngay trên thân người chơi) tạo sóng xung kích, sét Plasma, đá vỡ tung tóe và khói bụi.
    4. Diễn hoạt trọn vẹn đúng 3.0 giây (75 ticks tại 25 FPS).
    """
    scale = 4
    w_sheet = 240 * scale  # 960
    h_sheet = 140 * scale  # 560
    im = Image.new('RGBA', (w_sheet, h_sheet), (0, 0, 0, 0))

    im_sphere    = draw_tact_gamma_sphere(96*scale, 96*scale)           # Sprite 0: 384x384 (96x96)
    im_boulder_a = draw_crushing_boulder_a(28*scale, 24*scale)         # Sprite 1: 112x96  (28x24)
    im_boulder_b = draw_crushing_boulder_b(28*scale, 24*scale)         # Sprite 2: 112x96  (28x24)
    im_spire     = draw_crushing_rock_spire(22*scale, 18*scale)        # Sprite 3: 88x72   (22x18)
    im_slab      = draw_crushing_rock_slab(26*scale, 20*scale)         # Sprite 4: 104x80  (26x20)
    im_shockwave = draw_crushing_impact_shockwave(50*scale, 42*scale)   # Sprite 5: 200x168 (50x42)
    im_plasma    = draw_plasma_lightning_burst(46*scale, 46*scale)      # Sprite 6: 184x184 (46x46)
    im_shattered = draw_shattered_rock_fragments(40*scale, 36*scale)    # Sprite 7: 160x144 (40x36)
    im_dust      = draw_crush_dust_puff(36*scale, 26*scale)            # Sprite 8: 144x104 (36x26)

    # Bố trí Sprite vào Sheet
    im.paste(im_sphere,    (0*scale,   0*scale),  im_sphere)            # Sprite 0 (x=0, y=0, w=96, h=96)
    im.paste(im_boulder_a, (100*scale, 0*scale),  im_boulder_a)         # Sprite 1 (x=100, y=0, w=28, h=24)
    im.paste(im_boulder_b, (132*scale, 0*scale),  im_boulder_b)         # Sprite 2 (x=132, y=0, w=28, h=24)
    im.paste(im_spire,     (164*scale, 0*scale),  im_spire)             # Sprite 3 (x=164, y=0, w=22, h=18)
    im.paste(im_slab,      (190*scale, 0*scale),  im_slab)              # Sprite 4 (x=190, y=0, w=26, h=20)
    im.paste(im_shockwave, (100*scale, 28*scale), im_shockwave)         # Sprite 5 (x=100, y=28, w=50, h=42)
    im.paste(im_plasma,    (154*scale, 24*scale), im_plasma)            # Sprite 6 (x=154, y=24, w=46, h=46)
    im.paste(im_shattered, (100*scale, 74*scale), im_shattered)         # Sprite 7 (x=100, y=74, w=40, h=36)
    im.paste(im_dust,      (144*scale, 74*scale), im_dust)              # Sprite 8 (x=144, y=74, w=36, h=26)

    small_imgs = [
        (0,   0,  0, 96, 96), # Sprite 0: Phòng Room hình cầu 3D bao trùm người trúng chiêu
        (1, 100,  0, 28, 24), # Sprite 1: Tảng đá góc cạnh lớn A
        (2, 132,  0, 28, 24), # Sprite 2: Tảng đá góc cạnh lớn B
        (3, 164,  0, 22, 18), # Sprite 3: Mảnh đá nhọn lao nhanh
        (4, 190,  0, 26, 20), # Sprite 4: Khối đá tảng dẹp
        (5, 100, 28, 50, 42), # Sprite 5: Sóng xung kích va đập cực đại
        (6, 154, 24, 46, 46), # Sprite 6: Tia sét Plasma Gamma & bùng nổ va đập
        (7, 100, 74, 40, 36), # Sprite 7: Mảnh đá vỡ vụn văng tứ phía
        (8, 144, 74, 36, 26), # Sprite 8: Khói bụi va đập tản mác
    ]

    frames = [
        # Frame 0: Phòng Room hình cầu 3D xuất hiện bao trọn người chơi (0.0s)
        [(-48, -70, 0, 0, 0)],

        # Frame 1: Vòng cầu Room định hình phát sáng rực rỡ (0.12s)
        [(-48, -70, 0, 0, 0)],

        # Frame 2: Vòng cầu Room khóa chặt mục tiêu bên trong (0.24s)
        [(-48, -70, 0, 0, 0)],

        # Frame 3: 6 Khối đá tảng khổng lồ xuất hiện quanh viền vòng cầu (0.36s)
        [
            (-48, -70, 0, 0, 0),
            (-48, -34, 1, 0, 0), # Đá A bên trái
            (+20, -34, 2, 0, 0), # Đá B bên phải
            (-38, -58, 3, 0, 0), # Đá nhọn trên-trái
            (+12, -58, 4, 0, 0), # Đá tảng trên-phải
            (-36,  -8, 3, 0, 1), # Đá nhọn dưới-trái (phía trước)
            (+10,  -8, 4, 0, 1), # Đá tảng dưới-phải (phía trước)
        ],

        # Frame 4: Các tảng đá lơ lửng, vết nứt năng lượng tích tụ (0.48s)
        [
            (-48, -70, 0, 0, 0),
            (-48, -34, 1, 0, 0),
            (+20, -34, 2, 0, 0),
            (-38, -58, 3, 0, 0),
            (+12, -58, 4, 0, 0),
            (-36,  -8, 3, 0, 1),
            (+10,  -8, 4, 0, 1),
        ],

        # Frame 5: Đá rung chuyển mạnh, chuẩn bị bị đẩy cực nhanh (0.60s)
        [
            (-48, -70, 0, 0, 0),
            (-46, -34, 1, 0, 0),
            (+18, -34, 2, 0, 0),
            (-36, -56, 3, 0, 0),
            (+10, -56, 4, 0, 0),
            (-34, -10, 3, 0, 1),
            ( +8, -10, 4, 0, 1),
        ],

        # Frame 6: HIỆU ỨNG ĐÁ BẮT ĐẦU BỊ ĐẨY MẠNH VÔ PLAYER TRÚNG ĐÒN (0.72s)
        [
            (-48, -70, 0, 0, 0),
            (-38, -34, 1, 0, 0), # Đẩy vô
            (+10, -34, 2, 0, 0), # Đẩy vô
            (-28, -48, 3, 0, 0), # Đẩy vô
            ( +3, -48, 4, 0, 0), # Đẩy vô
            (-26, -15, 3, 0, 1), # Đẩy vô
            ( +1, -15, 4, 0, 1), # Đẩy vô
        ],

        # Frame 7: ĐÁ LAO CỰC NHANH ÁP SÁT THÂN PLAYER (0.84s)
        [
            (-48, -70, 0, 0, 0),
            (-26, -34, 1, 0, 0),
            ( -2, -34, 2, 0, 0),
            (-19, -38, 3, 0, 0),
            ( -6, -38, 4, 0, 0),
            (-18, -23, 3, 0, 1),
            ( -8, -23, 4, 0, 1),
        ],

        # Frame 8: ĐÁ ÉP SÁT RẠN NỨT CỰC ĐẠI NGAY TRÊN NGƯỜI PLAYER (0.96s)
        [
            (-48, -70, 0, 0, 0),
            (-18, -34, 1, 0, 0),
            (-10, -34, 2, 0, 0),
            (-13, -32, 3, 0, 0),
            (-10, -32, 4, 0, 0),
            (-13, -27, 3, 0, 1),
            (-10, -27, 4, 0, 1),
        ],

        # Frame 9: VA CHẠM NGHIỀN NÁT CỰC ĐẠI VÀO PLAYER! BÙNG NỔ SÓNG XUNG KÍCH (1.08s)
        [
            (-48, -70, 0, 0, 0),
            (-14, -34, 1, 0, 0),
            (-14, -34, 2, 0, 0),
            (-25, -43, 5, 0, 1), # Sóng xung kích ngay tâm player
            (-23, -45, 6, 0, 1), # Tia chớp Plasma va chạm
        ],

        # Frame 10: TIA SÉT GAMMA BÙNG PHÁT XUYÊN THỦNG + ĐÁ BẮT ĐẦU VỠ NÁT (1.20s)
        [
            (-48, -70, 0, 0, 0),
            (-25, -43, 5, 0, 1),
            (-23, -45, 6, 0, 1),
            (-20, -40, 7, 0, 1), # Mảnh vỡ đá bắt đầu tung tóe
        ],

        # Frame 11: ĐÁ VỠ VỤN VĂNG TỨ PHÍA + SÓNG XUNG KÍCH TỎA RỘNG (1.32s)
        [
            (-48, -70, 0, 0, 0),
            (-25, -43, 5, 0, 1),
            (-23, -45, 6, 0, 1),
            (-20, -40, 7, 0, 1),
            (-18, -35, 8, 0, 1), # Khói bụi bùng phát
        ],

        # Frame 12: CÁC MẢNH ĐÁ SHRAPNEL BẮN RA XUNG QUANH TRONG HÌNH CẦU (1.44s)
        [
            (-48, -70, 0, 0, 0),
            (-28, -44, 7, 0, 1),
            (-12, -44, 7, 0, 1),
            (-24, -35, 8, 0, 1),
            (-12, -35, 8, 0, 1),
        ],

        # Frame 13: MẢNH ĐÁ VĂNG XA + BỤI ĐẤT CUỘN TRÒN (1.56s)
        [
            (-48, -70, 0, 0, 0),
            (-32, -46, 7, 0, 1),
            ( -8, -46, 7, 0, 1),
            (-26, -35, 8, 0, 1),
            (-10, -35, 8, 0, 1),
        ],

        # Frame 14: BỤI ĐẤT & HẠT SÁNG LAN TỎA QUANH PLAYER (1.68s)
        [
            (-48, -70, 0, 0, 0),
            (-24, -35, 8, 0, 1),
            (-12, -35, 8, 0, 1),
        ],

        # Frame 15: KHÓI BỤI VA CHẠM BAO TRÙM (1.80s)
        [
            (-48, -70, 0, 0, 0),
            (-18, -35, 8, 0, 1),
        ],

        # Frame 16: KHÓI BỤI BỐC LÊN CHẬM TRONG PHÒNG ROOM (1.92s)
        [
            (-48, -70, 0, 0, 0),
            (-18, -35, 8, 0, 1),
        ],

        # Frame 17: BỤI BẮT ĐẦU LẮNG XUỐNG (2.04s)
        [
            (-48, -70, 0, 0, 0),
            (-18, -35, 8, 0, 1),
        ],

        # Frame 18: CÁC HẠT BỤI RƠI XUỐNG ĐẤT (2.16s)
        [
            (-48, -70, 0, 0, 0),
            (-18, -35, 8, 0, 1),
        ],

        # Frame 19: VÒNG CẦU ROOM BẮT ĐẦU MỜ DẦN (2.28s)
        [
            (-48, -70, 0, 0, 0),
            (-18, -35, 8, 0, 1),
        ],

        # Frame 20: VÒNG CẦU MỜ HẲN (2.40s)
        [
            (-48, -70, 0, 0, 0),
            (-18, -35, 8, 0, 1),
        ],

        # Frame 21: BỤI TÀN MỜ NHẠT (2.52s)
        [
            (-18, -35, 8, 0, 1),
        ],

        # Frame 22: LÀN KHÓI MỎNG TAN BIẾN (2.64s)
        [
            (-18, -35, 8, 0, 1),
        ],

        # Frame 23: TIA SÁNG CUỐI CÙNG (2.76s)
        [
            (-18, -35, 8, 0, 1),
        ],

        # Frame 24: KẾT THÚC HOÀN TOÀN (2.88s - 3.00s)
        [
            (-18, -35, 8, 0, 1),
        ],
    ]

    seq = [f for f in range(25) for _ in range(3)]

    data_bytes = build_data_effect(small_imgs, frames, seq)
    save_multizoom_effect(915, im, data_bytes)


# ==============================================================================
# 3. TẠO EFFECT 916: VÒNG TRÒN BẢO HỘ CURTAIN MÀU XÁM XANH SÁNG (NHƯ CHIÊU CŨ)
# ==============================================================================
def draw_curtain_light_grey_cyan_circle(phase=0, w_box=320, h_box=320):
    """
    Vẽ VÒNG TRÒN BẢO HỘ CURTAIN MÀU XÁM XANH SÁNG SIÊU ĐẸP & MỀM MẠI:
    - Kích thước 4x: 320x320 (1x: 80x80)
    - Vành hào quang kép ánh bạc pha xanh cyan (Luminous Silver-Cyan Dual Halo)
    - Vạch chia tọa độ Room phẫu thuật tinh xảo & 4 chữ thập ánh sáng (Cardinal Medical Crosses)
    - 8 hạt photon năng lượng xoay tròn quanh viền
    - DÒNG SÓNG NĂNG LƯỢNG SINE UỐN LƯỢN QUÉT LÊN XUỐNG CỰC KỲ MỀM MẠI:
      + Di chuyển điều hòa harmonic sine mềm mại qua 8 pha
      + Dải sóng vòm cong hình cánh cung uyển chuyển theo hướng quét
      + Dải lụa năng lượng aurora phát sáng với các hạt photon lấp lánh và vệt sáng bay bổng
    - Lòng vòng tròn hoàn toàn trong suốt, onTop = 0 TUYỆT ĐỐI KHÔNG CHE NHÂN VẬT!
    """
    im = Image.new('RGBA', (w_box, h_box), (0, 0, 0, 0))
    d = ImageDraw.Draw(im)
    cx, cy = w_box // 2, h_box // 2
    r_base = 145
    pulse = int(3 * math.sin(phase * math.pi / 4))
    r = r_base + pulse

    # 1. Hào quang mờ viền ngoài (Silver-Cyan Soft Ambient Glow) - Tăng kích thước và độ sáng
    d.ellipse([cx - r - 14, cy - r - 14, cx + r + 14, cy + r + 14], fill=(160, 220, 245, 25))
    d.ellipse([cx - r - 8,  cy - r - 8,  cx + r + 8,  cy + r + 8],  fill=(185, 235, 255, 45))
    d.ellipse([cx - r - 4,  cy - r - 4,  cx + r + 4,  cy + r + 4],  fill=(210, 248, 255, 80))

    # 2. Vành tròn bảo hộ chính kép (Dual Neon Rings) - Dầy khung ra và sáng hơn
    # Vành chính ngoài
    d.ellipse([cx - r, cy - r, cx + r, cy + r], outline=(170, 230, 252, 255), width=10)
    d.ellipse([cx - r + 2, cy - r + 2, cx + r - 2, cy + r - 2], outline=(255, 255, 255, 255), width=5)
    # Vành phụ trong
    r_in = r - 12
    d.ellipse([cx - r_in, cy - r_in, cx + r_in, cy + r_in], outline=(140, 210, 240, 200), width=5)

    # 3. Vạch chia tọa độ Room phẫu thuật (Surgical Precision Ticks)
    for deg in range(0, 360, 15):
        rad = math.radians(deg)
        x1 = cx + int(math.cos(rad) * r)
        y1 = cy + int(math.sin(rad) * r)
        x2 = cx + int(math.cos(rad) * (r - 8))
        y2 = cy + int(math.sin(rad) * (r - 8))
        d.line([(x1, y1), (x2, y2)], fill=(200, 242, 255, 200), width=3)

    # 4. 4 Chữ thập phẫu thuật phát sáng tại 4 hướng chính (0, 90, 180, 270 deg)
    for deg in [0, 90, 180, 270]:
        rad = math.radians(deg)
        nx = cx + int(math.cos(rad) * (r - 6))
        ny = cy + int(math.sin(rad) * (r - 6))
        d.ellipse([nx - 4, ny - 4, nx + 4, ny + 4], fill=(0, 240, 255, 255))
        draw_sparkle(d, nx, ny, r=10, color=(255, 255, 255, 255))

    # 5. 8 Hạt photon kim cương xoay tròn quanh viền - Hạt to hơn, uy lực hơn
    offset_ang = phase * 11.25
    for k in range(8):
        ang = k * 45 + offset_ang
        rad = math.radians(ang)
        px = cx + int(math.cos(rad) * (r - 4))
        py = cy + int(math.sin(rad) * (r - 4))
        d.polygon([(px, py - 6), (px + 6, py), (px, py + 6), (px - 6, py)], fill=(255, 255, 255, 255))
        draw_sparkle(d, px, py, r=8, color=(170, 235, 255, 240))

    # 6. DÒNG SÓNG NĂNG LƯỢNG QUÉT LÊN XUỐNG ĐIỀU HÒA SINE MỀM MẠI (Harmonic Curved Wave)
    y_ratio = 0.56 * math.sin(phase * 2 * math.pi / 8)
    y_stream = cy + int(r * y_ratio)
    v_dir = math.cos(phase * 2 * math.pi / 8) # Chiều quét (+: từ trên xuống dưới, -: từ dưới lên trên)

    dy = abs(y_stream - cy)
    dx_max = int(math.sqrt(max(0, (r - 14)**2 - dy**2)))
    if dx_max > 16:
        arch_offset = int(-16 * v_dir) # Độ cong hình cánh cung theo hướng chuyển động
        arc_pts = []
        steps = 20
        for s in range(steps + 1):
            t = s / steps
            xs = cx - dx_max + int(2 * dx_max * t)
            # Đường cong parabol hình vòm cánh cung
            arch = math.sin(t * math.pi) * arch_offset
            ys = y_stream + int(arch)
            arc_pts.append((xs, ys))

        # Dải lụa phát sáng mềm mại trailing phía sau vệt quét
        tail_offset = int(-22 * v_dir)
        tail_pts = list(arc_pts)
        for xs, ys in reversed(arc_pts):
            tail_pts.append((xs, ys - tail_offset))
        d.polygon(tail_pts, fill=(150, 220, 250, 60))

        # Vệt sóng hào quang chính - Tăng độ dày dải sóng
        d.line(arc_pts, fill=(130, 215, 245, 140), width=12)
        d.line(arc_pts, fill=(195, 242, 255, 220), width=7)
        d.line(arc_pts, fill=(255, 255, 255, 255), width=3)

        # 5 Hạt photon lơ lửng chạy dọc theo vòm sóng
        for f in [-0.7, -0.35, 0.0, 0.35, 0.7]:
            mx = cx + int(dx_max * f)
            arch_m = math.sin(((f + 1.0) / 2.0) * math.pi) * arch_offset
            my = y_stream + int(arch_m)
            draw_sparkle(d, mx, my, r=9, color=(255, 255, 255, 255))
            d.ellipse([mx - 3, my - 3, mx + 3, my + 3], fill=(0, 240, 255, 255))

        # Vệt sáng bay bổng (Streamer trails)
        for f in [-0.6, -0.2, 0.2, 0.6]:
            sx = cx + int(dx_max * f)
            arch_s = math.sin(((f + 1.0) / 2.0) * math.pi) * arch_offset
            sy = y_stream + int(arch_s)
            d.line([(sx, sy), (sx, sy - int(20 * v_dir))], fill=(190, 240, 255, 180), width=3)

    return im

def create_effect_916_curtain_shield():
    """
    Tạo Effect 916: Curtain - Vòng tròn bảo hộ màu xám xanh sáng siêu đẹp:
    - Vành hào quang kép ánh bạc pha xanh cyan tinh xảo.
    - Dòng sóng năng lượng uốn lượn hình vòm cánh cung quét lên xuống điều hòa sine cực mượt (8 pha).
    - Ruột hoàn toàn trong suốt, onTop = 0 ở tầng nền -> Tuyệt đối không che nhân vật!
    """
    w_box = 320
    h_box = 320
    circle_imgs = [draw_curtain_light_grey_cyan_circle(p, w_box, h_box) for p in range(8)]

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
        (0,   0,   0, 80, 80), # Sprite 0: Vòng tròn pha 0
        (1,  80,   0, 80, 80), # Sprite 1: Vòng tròn pha 1
        (2, 160,   0, 80, 80), # Sprite 2: Vòng tròn pha 2
        (3,   0,  80, 80, 80), # Sprite 3: Vòng tròn pha 3
        (4,  80,  80, 80, 80), # Sprite 4: Vòng tròn pha 4
        (5, 160,  80, 80, 80), # Sprite 5: Vòng tròn pha 5
        (6,   0, 160, 80, 80), # Sprite 6: Vòng tròn pha 6
        (7,  80, 160, 80, 80), # Sprite 7: Vòng tròn pha 7
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
    create_effect_915_room_earth_detonation()
    create_effect_916_curtain_shield()
    print("=== ĐANG TẠO ICONS TRÁI ÁC QUỶ & KỸ NĂNG OPE OPE (ID 2191, 4421..4424) ===")
    create_all_ope_icons()
    print("=== HOÀN TẤT TOÀN BỘ TÀI NGUYÊN HÌNH ẢNH & HIỆU ỨNG TRÁI OPE OPE! ===")
