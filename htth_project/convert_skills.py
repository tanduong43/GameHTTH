import os
import json
import struct
import glob
from PIL import Image

def load_ibn_map(ibn_path):
    with open(ibn_path, 'r', encoding='utf-8') as f:
        ibn = json.load(f)
    ibn_map = {}
    for item in ibn:
        ibn_map[item['filename']] = item['number_frame']
    return ibn_map

def get_frame_count(filename_no_ext, ibn_map):
    if filename_no_ext in ibn_map:
        return ibn_map[filename_no_ext]
    parts = filename_no_ext.split('_')
    if len(parts) >= 4:
        for z in ['3', '2', '1', '0']:
            candidate = f"{parts[0]}_{parts[1]}_{z}_{parts[3]}"
            if candidate in ibn_map:
                return ibn_map[candidate]
    return 1

def build_skill_effect(base_dir, sk_id, part_id, target_eff_ids, ibn_map, fps_repeat=2):
    # 1. Load x1 source image (or fallback to higher zoom and downscale to x1)
    src_x1_path = os.path.join(base_dir, 'data', 'nro', 'normal', 'image', '1', 'imgbyname', f'Skills_{sk_id}_1_{part_id}.png')
    src_name = f'Skills_{sk_id}_1_{part_id}'
    if not os.path.exists(src_x1_path):
        for z in ['2', '3', '0']:
            p = os.path.join(base_dir, 'data', 'nro', 'normal', 'image', z, 'imgbyname', f'Skills_{sk_id}_{z}_{part_id}.png')
            if os.path.exists(p):
                src_x1_path = p
                src_name = f'Skills_{sk_id}_{z}_{part_id}'
                break

    if not os.path.exists(src_x1_path):
        for z in ['3', '2', '1', '0']:
            p = os.path.join(base_dir, 'data', 'nro', 'normal', 'image', '4', 'imgbyname', f'Skills_{sk_id}_{z}_{part_id}.png')
            if os.path.exists(p):
                src_x1_path = p
                src_name = f'Skills_{sk_id}_{z}_{part_id}'
                break

    if not os.path.exists(src_x1_path):
        print(f"Error: Could not find source image for Skill {sk_id}_{part_id}")
        return

    num_frames = get_frame_count(src_name, ibn_map)
    raw_im = Image.open(src_x1_path)
    
    raw_w, raw_h = raw_im.size
    raw_frame_h = raw_h // num_frames
    
    base_scale = 1.0
    if raw_w > 120 or raw_frame_h > 120:
        base_scale = min(60.0 / raw_w, 60.0 / raw_frame_h)

    frames_x1 = []
    for i in range(num_frames):
        box = (0, i * raw_frame_h, raw_w, min(raw_h, (i + 1) * raw_frame_h))
        fc = raw_im.crop(box)
        if base_scale != 1.0:
            fc = fc.resize((max(1, int(fc.size[0] * base_scale)), max(1, int(fc.size[1] * base_scale))), Image.Resampling.LANCZOS)
        
        bbox = fc.getbbox()
        if bbox:
            cropped = fc.crop(bbox)
            frames_x1.append((cropped, bbox[0], bbox[1], fc.size[0], fc.size[1]))
        else:
            frames_x1.append((fc, 0, 0, fc.size[0], fc.size[1]))

    cols = 2 if len(frames_x1) <= 4 else (3 if len(frames_x1) <= 9 else 4)
    cell_w = max(f[0].size[0] for f in frames_x1) + 2
    cell_h = max(f[0].size[1] for f in frames_x1) + 2
    rows = (len(frames_x1) + cols - 1) // cols

    atlas_w_x1 = cell_w * cols
    atlas_h_x1 = cell_h * rows

    if atlas_w_x1 > 120 or atlas_h_x1 > 120:
        fit_scale = min(120.0 / atlas_w_x1, 120.0 / atlas_h_x1)
        frames_x1_scaled = []
        for f, rx, ry, orig_w, orig_h in frames_x1:
            sw = max(1, int(f.size[0] * fit_scale))
            sh = max(1, int(f.size[1] * fit_scale))
            f_sc = f.resize((sw, sh), Image.Resampling.LANCZOS)
            frames_x1_scaled.append((f_sc, int(rx * fit_scale), int(ry * fit_scale), int(orig_w * fit_scale), int(orig_h * fit_scale)))
        frames_x1 = frames_x1_scaled
        cell_w = max(f[0].size[0] for f in frames_x1) + 2
        cell_h = max(f[0].size[1] for f in frames_x1) + 2
        atlas_w_x1 = cell_w * cols
        atlas_h_x1 = cell_h * rows

    small_images = []
    frame_parts = []
    positions_x1 = []

    for idx, (f, rx, ry, orig_w, orig_h) in enumerate(frames_x1):
        c = idx % cols
        r = idx // cols
        px = c * cell_w
        py = r * cell_h
        fw, fh = f.size
        small_images.append((idx, px, py, fw, fh))
        positions_x1.append((px, py, fw, fh))

        dx = rx - orig_w // 2
        dy = ry - orig_h // 2
        frame_parts.append((dx, dy, idx))

    out_b = bytearray()
    out_b.append(len(small_images))
    for s_id, sx, sy, sw, sh in small_images:
        out_b.extend([s_id, sx, sy, sw, sh])

    out_b.extend(struct.pack('>H', len(frame_parts)))
    for dx, dy, s_id in frame_parts:
        out_b.append(1)
        out_b.extend(struct.pack('>hh', dx, dy))
        out_b.append(s_id)
        out_b.append(0)
        out_b.append(1)

    seq = []
    for i in range(len(frame_parts)):
        for _ in range(fps_repeat):
            seq.append(i)
    out_b.append(len(seq))
    for s in seq:
        out_b.extend(struct.pack('>h', s))

    out_b.extend([0, 0, 0, 0, 0, 0, 0])

    for z in [1, 2, 3, 4]:
        zoom_mult = z
        atlas_z = Image.new('RGBA', (max(1, atlas_w_x1 * zoom_mult), max(1, atlas_h_x1 * zoom_mult)), (0, 0, 0, 0))
        
        for idx, (f, rx, ry, orig_w, orig_h) in enumerate(frames_x1):
            px, py, fw, fh = positions_x1[idx]
            f_z = f.resize((max(1, fw * zoom_mult), max(1, fh * zoom_mult)), Image.Resampling.LANCZOS)
            atlas_z.paste(f_z, (px * zoom_mult, py * zoom_mult))

        for eff_id in target_eff_ids:
            out_data_skill = os.path.join(base_dir, 'data', 'template', 'skill', f'x{z}', 'data', str(eff_id))
            out_img_skill = os.path.join(base_dir, 'data', 'template', 'skill', f'x{z}', 'img', f'{eff_id}.png')
            
            out_data_nro = os.path.join(base_dir, 'data', 'nro', 'data', 'effect', f'x{z}', 'data', f'DataEffect_{eff_id}')
            out_img_nro = os.path.join(base_dir, 'data', 'nro', 'data', 'effect', f'x{z}', 'img', f'ImgEffect_{eff_id}.png')

            for d_p in [out_data_skill, out_data_nro]:
                os.makedirs(os.path.dirname(d_p), exist_ok=True)
                with open(d_p, 'wb') as f_out:
                    f_out.write(out_b)

            for i_p in [out_img_skill, out_img_nro]:
                os.makedirs(os.path.dirname(i_p), exist_ok=True)
                atlas_z.save(i_p, optimize=True)
                if os.path.getsize(i_p) > 48000:
                    try:
                        q = atlas_z.quantize(colors=256, method=Image.Quantize.FASTOCTREE)
                        q.save(i_p, optimize=True)
                    except Exception:
                        pass

    ids_str = ", ".join(str(i) for i in target_eff_ids)
    print(f"  Skill {sk_id}_{part_id} -> IDs [{ids_str}]: Data {len(out_b)}b, x1: {atlas_w_x1}x{atlas_h_x1}, x4: {atlas_w_x1*4}x{atlas_h_x1*4}")

def cleanup_loose_files(base_dir):
    for z in [1, 2, 3, 4]:
        img_dir = os.path.join(base_dir, 'data', 'template', 'skill', f'x{z}', 'img')
        ibn_dir = os.path.join(base_dir, 'data', 'template', 'skill', f'x{z}', 'imgbyname')
        
        for f in glob.glob(os.path.join(img_dir, 'Skills_*')):
            try:
                os.remove(f)
            except Exception:
                pass
        
        if os.path.exists(ibn_dir):
            for f in glob.glob(os.path.join(ibn_dir, '*')):
                try:
                    os.remove(f)
                except Exception:
                    pass
            try:
                os.rmdir(ibn_dir)
            except Exception:
                pass

def main():
    base_dir = os.path.dirname(os.path.abspath(__file__))
    ibn_path = os.path.join(base_dir, 'data', 'nro', 'normal', 'ibn.json')
    ibn_map = load_ibn_map(ibn_path)

    # Clean loose non-numeric Skills_ files in template/skill
    cleanup_loose_files(base_dir)

    # 18 skill parts mapped to BOTH sequential IDs (37..54) and group IDs (124..145)
    mapping = [
        # Skill 24
        (('24', '0'), [37, 124], "Skill 24_0 (Tụ lực)"),
        (('24', '1'), [38, 125], "Skill 24_1 (Hào quang)"),
        (('24', '2'), [39, 126], "Skill 24_2 (Tia đạn)"),
        (('24', '3'), [40, 127], "Skill 24_3 (Trúng đích)"),
        (('24', '4'), [41, 128], "Skill 24_4 (Nổ vỡ)"),
        # Skill 25
        (('25', '0'), [42, 130], "Skill 25_0 (Thế đánh)"),
        (('25', '1'), [43, 131], "Skill 25_1 (Cầu năng lượng)"),
        (('25', '2'), [44, 132], "Skill 25_2 (Bắn tia)"),
        (('25', '3'), [45, 133], "Skill 25_3 (Chùm tia)"),
        (('25', '4'), [46, 134], "Skill 25_4 (Va chạm)"),
        (('25', '5'), [47, 135], "Skill 25_5 (Nổ to)"),
        (('25', '6'), [48, 136], "Skill 25_6 (Chớp nổ cực đại)"),
        # Skill 26
        (('26', '0'), [49, 140], "Skill 26_0 (Xuất chiêu)"),
        (('26', '1'), [50, 141], "Skill 26_1 (Tụ chưởng)"),
        (('26', '2'), [51, 142], "Skill 26_2 (Chưởng lớn)"),
        (('26', '3'), [52, 143], "Skill 26_3 (Cột năng lượng)"),
        (('26', '4'), [53, 144], "Skill 26_4 (Nổ quét)"),
        (('26', '7'), [54, 145], "Skill 26_7 (Sóng xung kích)"),
    ]

    print("================ GENERATING SEQUENTIAL & SYNCHRONIZED HTTH EFFECTS ================")
    for (sk_id, part_id), eff_ids, desc in mapping:
        build_skill_effect(base_dir, sk_id, part_id, eff_ids, ibn_map)
    print("\n================ COMPLETED ALL EFFECTS ================")

if __name__ == '__main__':
    main()
