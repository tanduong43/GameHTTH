import os
import sys
import math
import numpy as np
from PIL import Image, ImageDraw, ImageFilter

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

def catmull_rom_spline(control_pts, num_samples=160):
    """Generate smooth Catmull-Rom spline points passing through control points."""
    pts = []
    n = len(control_pts)
    for i in range(n - 1):
        p0 = control_pts[max(0, i - 1)]
        p1 = control_pts[i]
        p2 = control_pts[i + 1]
        p3 = control_pts[min(n - 1, i + 2)]
        samples = max(4, num_samples // (n - 1))
        for t in np.linspace(0, 1, samples, endpoint=False):
            t2 = t * t
            t3 = t2 * t
            x = 0.5 * ((2 * p1[0]) +
                       (-p0[0] + p2[0]) * t +
                       (2*p0[0] - 5*p1[0] + 4*p2[0] - p3[0]) * t2 +
                       (-p0[0] + 3*p1[0] - 3*p2[0] + p3[0]) * t3)
            y = 0.5 * ((2 * p1[1]) +
                       (-p0[1] + p2[1]) * t +
                       (2*p0[1] - 5*p1[1] + 4*p2[1] - p3[1]) * t2 +
                       (-p0[1] + 3*p1[1] - 3*p2[1] + p3[1]) * t3)
            pts.append((x, y))
    pts.append((float(control_pts[-1][0]), float(control_pts[-1][1])))
    return pts

def render_clean_haki_layer(main_ctrl, spurs=None, lightning=None, scale=4):
    """
    Renders the Haki energy stream at hi-res (80*scale x 80*scale) with:
    1. Outer radiant crimson bloom
    2. Medium glowing aura
    3. Deep blackish-crimson Haki border / shadow
    4. Vivid crimson core
    5. Hot scarlet / coral central ridge
    Then downsamples with LANCZOS to 80x80.
    """
    S = scale
    W, H = 80 * S, 80 * S
    
    main_s = [(p[0] * S, p[1] * S) for p in main_ctrl]
    spurs_s = [[(p[0] * S, p[1] * S) for p in sp] for sp in spurs] if spurs else []
    light_s = [[(p[0] * S, p[1] * S) for p in lt] for lt in lightning] if lightning else []
    
    all_paths = [catmull_rom_spline(main_s, 240)]
    for sp in spurs_s:
        all_paths.append(catmull_rom_spline(sp, 80))
    for lt in light_s:
        all_paths.append(catmull_rom_spline(lt, 50))
        
    # Layer 1: Tight glowing crimson bloom
    bloom = Image.new('RGBA', (W, H), (0, 0, 0, 0))
    d_bloom = ImageDraw.Draw(bloom)
    for p_idx, pts in enumerate(all_paths):
        bw = int(7.0 * S if p_idx == 0 else 4.5 * S)
        for i in range(len(pts) - 1):
            p1 = (int(round(pts[i][0])), int(round(pts[i][1])))
            p2 = (int(round(pts[i+1][0])), int(round(pts[i+1][1])))
            d_bloom.line([p1, p2], fill=(240, 20, 50, 130), width=bw)
            r = bw // 2
            d_bloom.ellipse([p1[0]-r, p1[1]-r, p1[0]+r, p1[1]+r], fill=(240, 20, 50, 130))
    bloom = bloom.filter(ImageFilter.GaussianBlur(radius=1.8 * S))
    
    # Layer 2: Medium radiant crimson aura
    med = Image.new('RGBA', (W, H), (0, 0, 0, 0))
    d_med = ImageDraw.Draw(med)
    for p_idx, pts in enumerate(all_paths):
        mw = int(4.8 * S if p_idx == 0 else 3.0 * S)
        for i in range(len(pts) - 1):
            p1 = (int(round(pts[i][0])), int(round(pts[i][1])))
            p2 = (int(round(pts[i+1][0])), int(round(pts[i+1][1])))
            d_med.line([p1, p2], fill=(225, 15, 40, 200), width=mw)
            r = mw // 2
            d_med.ellipse([p1[0]-r, p1[1]-r, p1[0]+r, p1[1]+r], fill=(225, 15, 40, 200))
    med = med.filter(ImageFilter.GaussianBlur(radius=0.7 * S))
    
    # Layer 3: Dark Haki Shadow / Edge (deep blackish-crimson #180206)
    dark = Image.new('RGBA', (W, H), (0, 0, 0, 0))
    d_dark = ImageDraw.Draw(dark)
    for p_idx, pts in enumerate(all_paths):
        n = len(pts)
        for i in range(n - 1):
            t = i / float(n - 1)
            t_factor = math.sin(t * math.pi) ** 0.5
            if p_idx == 0:
                base_w = (3.2 * (0.6 + 0.5 * t_factor) + 1.2) * S
            else:
                base_w = (2.0 * (0.4 + 0.6 * (1.0 - t)) + 0.8) * S
            w_cur = max(2, int(round(base_w)))
            p1 = (int(round(pts[i][0])), int(round(pts[i][1])))
            p2 = (int(round(pts[i+1][0])), int(round(pts[i+1][1])))
            d_dark.line([p1, p2], fill=(24, 2, 6, 255), width=w_cur)
            r = w_cur // 2
            d_dark.ellipse([p1[0]-r, p1[1]-r, p1[0]+r, p1[1]+r], fill=(24, 2, 6, 255))
    dark = dark.filter(ImageFilter.GaussianBlur(radius=0.35 * S))
    
    # Layer 4: Vivid Crimson Haki core
    crimson = Image.new('RGBA', (W, H), (0, 0, 0, 0))
    d_crim = ImageDraw.Draw(crimson)
    for p_idx, pts in enumerate(all_paths):
        n = len(pts)
        for i in range(n - 1):
            t = i / float(n - 1)
            t_factor = math.sin(t * math.pi) ** 0.5
            if p_idx == 0:
                base_w = 3.2 * (0.6 + 0.5 * t_factor) * S
            else:
                base_w = 2.0 * (0.4 + 0.6 * (1.0 - t)) * S
            w_cur = max(1, int(round(base_w)))
            p1 = (int(round(pts[i][0])), int(round(pts[i][1])))
            p2 = (int(round(pts[i+1][0])), int(round(pts[i+1][1])))
            d_crim.line([p1, p2], fill=(225, 20, 48, 255), width=w_cur)
            r = w_cur // 2
            d_crim.ellipse([p1[0]-r, p1[1]-r, p1[0]+r, p1[1]+r], fill=(225, 20, 48, 255))
            
    # Layer 5: Hot Coral / Scarlet central streak
    hot = Image.new('RGBA', (W, H), (0, 0, 0, 0))
    d_hot = ImageDraw.Draw(hot)
    main_pts = all_paths[0]
    n_m = len(main_pts)
    for i in range(int(n_m * 0.08), int(n_m * 0.90)):
        w_h = max(1, int(round(1.2 * S)))
        p1 = (int(round(main_pts[i][0])), int(round(main_pts[i][1])))
        p2 = (int(round(main_pts[i+1][0])), int(round(main_pts[i+1][1])))
        d_hot.line([p1, p2], fill=(255, 100, 120, 240), width=w_h)
        
    # Combine hi-res layers
    haki_hi = Image.new('RGBA', (W, H), (0, 0, 0, 0))
    haki_hi = Image.alpha_composite(haki_hi, bloom)
    haki_hi = Image.alpha_composite(haki_hi, med)
    haki_hi = Image.alpha_composite(haki_hi, dark)
    haki_hi = Image.alpha_composite(haki_hi, crimson)
    haki_hi = Image.alpha_composite(haki_hi, hot)
    
    return haki_hi.resize((80, 80), Image.Resampling.LANCZOS)

def generate_haki_fruit_2192():
    script_dir = os.path.dirname(os.path.abspath(__file__))
    icon_base = os.path.join(script_dir, 'data', 'icon')
    
    # 1. Load base fruit 2020.png (Gomu Gomu no Mi)
    src_2020 = os.path.join(icon_base, 'x4', '2020.png')
    img_2020 = Image.open(src_2020).convert('RGBA')
    base_f0 = img_2020.crop((0, 0, 80, 80))
    base_f1 = img_2020.crop((0, 80, 80, 160))
    
    # Adjust lavender vibrance on fruit body while preserving green stem
    arr0 = np.array(base_f0).astype(float)
    stem_mask = (arr0[:, :, 1] > arr0[:, :, 2] + 5) & (arr0[:, :, 1] > arr0[:, :, 0] + 5)
    body_mask = (arr0[:, :, 3] > 30) & (~stem_mask)
    arr0_lav = arr0.copy()
    arr0_lav[body_mask, 0] = np.clip(arr0_lav[body_mask, 0] * 1.35 + 20, 0, 255)
    arr0_lav[body_mask, 1] = np.clip(arr0_lav[body_mask, 1] * 1.25 + 10, 0, 255)
    arr0_lav[body_mask, 2] = np.clip(arr0_lav[body_mask, 2] * 1.1 + 15, 0, 255)
    fruit_base = Image.fromarray(arr0_lav.astype(np.uint8))
    
    arr1 = np.array(base_f1).astype(float)
    arr1_lav = arr1.copy()
    arr1_lav[body_mask, 0] = np.clip(arr1_lav[body_mask, 0] * 1.35 + 20, 0, 255)
    arr1_lav[body_mask, 1] = np.clip(arr1_lav[body_mask, 1] * 1.25 + 10, 0, 255)
    arr1_lav[body_mask, 2] = np.clip(arr1_lav[body_mask, 2] * 1.1 + 15, 0, 255)
    fruit_glow = Image.fromarray(arr1_lav.astype(np.uint8))
    
    # 2. 5-frame animation cycle: Haki stream moves UP and DOWN (lên xuống)
    configs = [
        # Frame 0: Bottom hugging curve (exact match to user image reference)
        {
            'main': [(16, 38), (15, 50), (19, 62), (32, 70), (48, 70), (63, 64), (70, 56), (72, 60), (68, 63)],
            'spurs': [[(19, 62), (24, 55), (30, 57)], [(63, 64), (68, 69), (72, 66)]],
            'lightning': [[(18, 28), (22, 24), (20, 20)]],
            'glow_blend': 0.15
        },
        # Frame 1: Mid-Bottom (rising)
        {
            'main': [(15, 30), (16, 42), (23, 53), (40, 60), (57, 56), (69, 47), (72, 39), (73, 43), (69, 45)],
            'spurs': [[(23, 53), (29, 46), (36, 49)], [(57, 56), (64, 61), (69, 57)]],
            'lightning': [[(66, 26), (70, 29), (68, 35)]],
            'glow_blend': 0.35
        },
        # Frame 2: Middle (rising across central swirl)
        {
            'main': [(16, 24), (19, 33), (27, 42), (44, 45), (61, 40), (69, 31), (72, 23), (73, 27), (69, 29)],
            'spurs': [[(27, 42), (34, 35), (41, 37)], [(61, 40), (68, 45), (71, 41)]],
            'lightning': [[(58, 59), (62, 63), (60, 69)]],
            'glow_blend': 0.50
        },
        # Frame 3: Apex (crowning upper body below stem)
        {
            'main': [(20, 20), (26, 25), (37, 30), (50, 29), (63, 24), (69, 18), (71, 13), (68, 11), (63, 13)],
            'spurs': [[(37, 30), (43, 23), (49, 26)], [(50, 29), (56, 34), (62, 32)]],
            'lightning': [[(38, 18), (42, 14), (46, 16)]],
            'glow_blend': 0.35
        },
        # Frame 4: Mid-Lower (descending back toward Frame 0)
        {
            'main': [(15, 27), (17, 37), (25, 47), (42, 52), (59, 49), (69, 41), (72, 33), (73, 37), (69, 40)],
            'spurs': [[(25, 47), (31, 40), (38, 43)], [(59, 49), (66, 54), (70, 50)]],
            'lightning': [[(20, 55), (24, 59), (22, 65)]],
            'glow_blend': 0.20
        }
    ]
    
    frames = []
    for cfg in configs:
        f_base = Image.blend(fruit_base, fruit_glow, cfg.get('glow_blend', 0.2))
        haki_layer = render_clean_haki_layer(cfg['main'], cfg.get('spurs'), cfg.get('lightning'))
        f_composite = Image.alpha_composite(f_base, haki_layer)
        frames.append(f_composite)
        
    # 3. Create vertical 5-frame sprite sheet (80 x 400) for x4
    sheet_x4 = Image.new('RGBA', (80, 400), (0, 0, 0, 0))
    for idx, f in enumerate(frames):
        sheet_x4.paste(f, (0, idx * 80))
        
    # 4. Multi-zoom levels standard in HTTH
    zooms = {
        'x4': sheet_x4,
        'x3': sheet_x4.resize((60, 300), Image.Resampling.BILINEAR),
        'x2': sheet_x4.resize((40, 200), Image.Resampling.BILINEAR),
        'x1': sheet_x4.resize((20, 100), Image.Resampling.NEAREST),
        'x0': sheet_x4.resize((20, 100), Image.Resampling.NEAREST)
    }
    
    for z, im_z in zooms.items():
        z_dir = os.path.join(icon_base, z)
        os.makedirs(z_dir, exist_ok=True)
        dest_path = os.path.join(z_dir, '2192.png')
        im_z.save(dest_path, format='PNG', optimize=True)
        print(f"-> Đã ghi đè {dest_path} [{im_z.size[0]}x{im_z.size[1]}]")
        
    print("=== HOÀN TẤT TẠO ICON 2192 5 FRAME HIỆU ỨNG HAKI LÊN XUỐNG! ===")

if __name__ == '__main__':
    generate_haki_fruit_2192()
