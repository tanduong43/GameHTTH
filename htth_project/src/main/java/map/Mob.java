package map;

import java.util.HashMap;
import template.MobTemplate;
/**
 *
 * @author Truongbk
 */
public class Mob {
	public final static HashMap<Integer, Mob> ENTRYS = new HashMap<>();
	public final static int TIME_RESPAWN = 7;
	public short x, y;
	public int hp, hp_max;
	public int level;
	public MobTemplate mob_template;
	public boolean isdie;
	public int id_target;
	public int index;
	public long time_skill;
	public long time_refresh;
	public Boss boss_info;
	public Pokemon_normal poke_nor_info;
    public Poke_huyen_thoai poke_huyen_thoai_info;
	public Map map;
	public int base_dame;
	public int final_dame;
	
	// Các chỉ số nâng cao (Stats) cho Mob / Boss
	public int phong_thu = 0;        // Điểm phòng thủ (trừ trực tiếp vào sát thương nhận vào)
	public int mien_thuong = 0;      // % Miễn thương (0 - 100%, ví dụ 70 = giảm 70% sát thương nhận)
	public long max_dame_per_hit = 0;// Giới hạn sát thương tối đa trên mỗi hit (chống oneshot boss)
	public int ne_don = 0;           // % Tỷ lệ né đòn (0 - 100%)
	public int phan_dame = 0;        // % Phản sát thương lại cho người chơi đánh

	public long calculate_damage_taken(long raw_dame) {
		if (raw_dame <= 0) {
			return 0;
		}
		// 1. Kiểm tra né đòn
		if (this.ne_don > 0 && core.Util.random(100) < this.ne_don) {
			return 0;
		}
		long dame = raw_dame;
		// 2. Trừ điểm phòng thủ
		if (this.phong_thu > 0) {
			dame = Math.max(1, dame - this.phong_thu);
		}
		// 3. Giảm sát thương theo % miễn thương
		if (this.mien_thuong > 0) {
			int mt = Math.min(99, this.mien_thuong);
			dame = (dame * (100 - mt)) / 100;
		}
		// 4. Giới hạn sát thương tối đa mỗi hit
		if (this.max_dame_per_hit > 0 && dame > this.max_dame_per_hit) {
			dame = this.max_dame_per_hit;
		}
		return Math.max(1, dame);
	}
}
