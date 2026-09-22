import os
import sys
import math
import numpy as np
from PIL import Image

if sys.stdout and hasattr(sys.stdout, 'reconfigure'):
    sys.stdout.reconfigure(encoding='utf-8')

def generate_haki_buff_frames():
    # 1. Load clean Haki source
    im_clean = Image.open('C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch/test_extract_clean.png')
    src_full = np.array(im_clean).astype(float)
    
    crop_w, crop_h = 960, 960
    x0, y0 = 32, 45
    src = src_full[y0:y0+crop_h, x0:x0+crop_w] # (960, 960, 4)
    H, W = src.shape[:2]
    
    cx_ground = 480.0
    cy_ground = 800.0
    aspect = 0.245
    
    grid_y, grid_x = np.indices((H, W), dtype=float)
    
    # Separate Haki (above ground)
    haki_src = src.copy()
    dist_to_ground = cy_ground - grid_y
    haki_fade = np.clip((dist_to_ground - 20.0) / 100.0, 0.0, 1.0)[:, :, None]
    haki_src[:, :, 3] *= haki_fade[:, :, 0]
    haki_src[grid_y > cy_ground, 3] = 0
    
    # Generate 8 frames
    frames_240 = []
    
    # 16 rising particle sparks
    np.random.seed(42)
    num_particles = 16
    p_x0 = np.random.uniform(220, 740, num_particles)
    p_tau = np.random.uniform(0, 1, num_particles) # phase offset
    p_size = np.random.uniform(8, 16, num_particles)
    
    for f in range(8):
        alpha_rot = f * (2.0 * math.pi / 8.0)
        
        # --- 1. RENDER ROTATING GROUND RING ---
        X_g = grid_x - cx_ground
        Z_g = (grid_y - cy_ground) / aspect
        R_g = np.sqrt(X_g**2 + Z_g**2)
        Theta_g = np.arctan2(Z_g, X_g)
        Theta_rot = Theta_g - alpha_rot
        
        ring_layer = np.zeros((H, W, 4), dtype=float)
        
        # Spikes: R in [385, spike_r_max]
        num_spikes = 28
        spike_wave = np.maximum(0.0, np.cos(num_spikes * Theta_rot))**2.5
        spike_r_max = 405.0 + 55.0 * spike_wave
        
        is_spike = (R_g >= 385) & (R_g <= spike_r_max)
        st = ((R_g - 385) / np.maximum(1.0, spike_r_max - 385))[is_spike]
        sw = spike_wave[is_spike]
        ring_layer[is_spike, 0] = np.clip(240 + 15 * (1 - st), 0, 255)
        ring_layer[is_spike, 1] = np.clip(25 + 180 * (1 - st) * sw, 0, 255)
        ring_layer[is_spike, 2] = np.clip(45 + 180 * (1 - st) * sw, 0, 255)
        ring_layer[is_spike, 3] = np.clip(255 * (1 - st**1.5), 0, 255)
        
        # Outer red border: [398, 410]
        is_outer_red = (R_g >= 398) & (R_g <= 410)
        ring_layer[is_outer_red] = [230, 20, 10, 255]
        
        # Outer yellow ring: [342, 398]
        is_outer_yel = (R_g >= 342) & (R_g < 398)
        ring_layer[is_outer_yel] = [255, 240, 50, 255]
        
        # Middle red divider: [330, 342]
        is_mid_red = (R_g >= 330) & (R_g < 342)
        ring_layer[is_mid_red] = [230, 20, 10, 255]
        
        # Middle golden ring: [250, 330] with rotating ray pattern
        is_mid_gold = (R_g >= 250) & (R_g < 330)
        notch = np.sin(num_spikes * Theta_rot)
        r_g = np.where(notch[is_mid_gold] > 0.25, 255, 248)
        g_g = np.where(notch[is_mid_gold] > 0.25, 215, 180)
        b_g = np.where(notch[is_mid_gold] > 0.25, 55, 20)
        ring_layer[is_mid_gold, 0] = r_g
        ring_layer[is_mid_gold, 1] = g_g
        ring_layer[is_mid_gold, 2] = b_g
        ring_layer[is_mid_gold, 3] = 255
        
        # Inner red divider: [238, 250]
        is_in_red = (R_g >= 238) & (R_g < 250)
        ring_layer[is_in_red] = [230, 20, 10, 255]
        
        # Inner yellow ring: [160, 238]
        is_in_yel = (R_g >= 160) & (R_g < 238)
        ring_layer[is_in_yel] = [255, 235, 70, 255]
        
        # Inner rotating glowing teeth: [160, teeth_r_max]
        num_teeth = 14
        teeth_wave = np.maximum(0.0, np.cos(num_teeth * Theta_rot + math.pi/4))**2.0
        teeth_r_max = 160.0 + 38.0 * teeth_wave
        is_teeth = (R_g >= 160) & (R_g <= teeth_r_max)
        ring_layer[is_teeth] = [255, 255, 220, 255]
        
        # Clean center core: [0, 160] - flat ground glow, NEVER covers player
        is_core = (R_g < 160)
        core_t = R_g[is_core] / 160.0
        ring_layer[is_core, 0] = 245
        ring_layer[is_core, 1] = 80 + 90 * core_t
        ring_layer[is_core, 2] = 25
        ring_layer[is_core, 3] = 40 + 110 * core_t
        
        # Clip ring to Y region near ground so it doesn't bleed upwards
        ring_layer[grid_y < 700, 3] = 0
        
        # --- 2. HAKI FLAME ANIMATION (BOBBING UP/DOWN + APPEAR/DISAPPEAR) ---
        # Smooth bobbing up and down across 8 frames
        off_y = -18.0 * math.sin(f * math.pi / 4.0)
        sc_y = 1.0 + 0.12 * math.sin(f * math.pi / 4.0 - math.pi / 6.0)
        phase = f / 8.0
        
        height_factor = np.clip((cy_ground - grid_y) / 680.0, 0, 1.2)
        wave_x = height_factor * 10.0 * np.sin(2 * np.pi * (grid_y / 200.0 - phase))
        wave_y = height_factor * 8.0 * np.cos(2 * np.pi * (grid_y / 200.0 - phase))
        
        src_y = np.where(grid_y < cy_ground, cy_ground - (cy_ground - grid_y - off_y) / sc_y + wave_y, grid_y)
        src_x = np.where(grid_y < cy_ground, grid_x - wave_x, grid_x)
        
        x_int0 = np.clip(np.floor(src_x).astype(int), 0, W - 2)
        x_int1 = x_int0 + 1
        y_int0 = np.clip(np.floor(src_y).astype(int), 0, H - 2)
        y_int1 = y_int0 + 1
        
        wx = np.clip((src_x - x_int0)[:, :, None], 0, 1)
        wy = np.clip((src_y - y_int0)[:, :, None], 0, 1)
        
        haki_warped = (
            haki_src[y_int0, x_int0] * (1 - wx) * (1 - wy) +
            haki_src[y_int0, x_int1] * wx * (1 - wy) +
            haki_src[y_int1, x_int0] * (1 - wx) * wy +
            haki_src[y_int1, x_int1] * wx * wy
        )
        
        # Intensity breathing (appear and disappear effect)
        intensity = 0.85 + 0.15 * math.cos(f * math.pi / 4.0)
        haki_warped[:, :, 3] *= intensity
        
        # Add rising particles (xuất hiện rồi biến mất dần)
        haki_layer = haki_warped.copy()
        for p_idx in range(num_particles):
            t_life = ((f / 8.0) + p_tau[p_idx]) % 1.0
            p_y = 740.0 - 550.0 * t_life
            p_x = p_x0[p_idx] + 8.0 * math.sin(2 * math.pi * t_life)
            p_s = p_size[p_idx]
            p_alpha = math.sin(math.pi * t_life) # 0 at spawn, 1 at peak, 0 at top
            
            if p_alpha > 0.05:
                # draw particle diamond spark
                px_min = max(0, int(p_x - p_s))
                px_max = min(W, int(p_x + p_s + 1))
                py_min = max(0, int(p_y - p_s * 1.5))
                py_max = min(H, int(p_y + p_s * 1.5 + 1))
                
                if px_max > px_min and py_max > py_min:
                    sub_y, sub_x = np.indices((py_max - py_min, px_max - px_min), dtype=float)
                    p_dist = np.abs(sub_x - (p_x - px_min)) / p_s + np.abs(sub_y - (p_y - py_min)) / (p_s * 1.5)
                    spark_mask = p_dist <= 1.0
                    spark_val = (1.0 - p_dist[spark_mask]) * p_alpha * 240.0
                    
                    # Bright lightning spark: white core, pink/red halo
                    haki_layer[py_min:py_max, px_min:px_max][spark_mask, 0] = 255
                    haki_layer[py_min:py_max, px_min:px_max][spark_mask, 1] = np.clip(haki_layer[py_min:py_max, px_min:px_max][spark_mask, 1] + spark_val * 0.8, 0, 255)
                    haki_layer[py_min:py_max, px_min:px_max][spark_mask, 2] = np.clip(haki_layer[py_min:py_max, px_min:px_max][spark_mask, 2] + spark_val * 0.9, 0, 255)
                    haki_layer[py_min:py_max, px_min:px_max][spark_mask, 3] = np.clip(haki_layer[py_min:py_max, px_min:px_max][spark_mask, 3] + spark_val, 0, 255)
        
        # --- 3. COMPOSITE RING + HAKI ---
        # Ground ring is below/behind Haki base
        # Alpha compositing: ring over haki (or haki over ring)
        # Ring has alpha, haki has alpha
        a_haki = haki_layer[:, :, 3:] / 255.0
        a_ring = ring_layer[:, :, 3:] / 255.0
        
        # Ground ring at bottom, Haki rises from behind it
        # Out = Ring + Haki * (1 - Ring_alpha)
        out_rgb = ring_layer[:, :, :3] * a_ring + haki_layer[:, :, :3] * a_haki * (1.0 - a_ring)
        out_a = np.clip((a_ring + a_haki * (1.0 - a_ring)) * 255.0, 0, 255)
        out_frame = np.concatenate([out_rgb, out_a], axis=2)
        
        img_960 = Image.fromarray(np.clip(out_frame, 0, 255).astype(np.uint8))
        img_240 = img_960.resize((240, 240), Image.Resampling.LANCZOS)
        frames_240.append(img_240)
        print(f'Frame {f} generated successfully')
    
    # Save GIF
    gif_path = 'C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch/haki_buff_v2.gif'
    frames_240[0].save(gif_path, save_all=True, append_images=frames_240[1:], duration=100, loop=0)
    print(f'Saved {gif_path}')
    
    # Also save individual frames as PNG for inspection
    for i, fr in enumerate(frames_240):
        fr.save(f'C:/Users/admin/.gemini/antigravity-ide/brain/c86bc279-4d82-458a-911a-7fcf750991f2/scratch/frame_v2_{i}.png')

if __name__ == '__main__':
    generate_haki_buff_frames()
