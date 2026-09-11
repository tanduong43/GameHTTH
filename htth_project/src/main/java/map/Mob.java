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
	public int mp = 1000000000, mp_max = 1000000000;
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
	public int giam_mien_thuong = 0; // Điểm giảm miễn thương của mục tiêu khi tấn công (thang 1000, vd 400 = giảm 40%)

	// Danh sách tên người chơi đã gây sát thương (dùng cho Đảo Ruby map 1001 - thưởng Ruby cho tất cả người tham gia)
	public java.util.Set<String> damageDealers = java.util.concurrent.ConcurrentHashMap.newKeySet();

	public boolean isRauTrang() {
		return (this.mob_template != null && (this.mob_template.mob_id == 172
				|| (this.mob_template.name != null && (this.mob_template.name.toLowerCase().contains("râu trắng") || this.mob_template.name.toLowerCase().contains("rau trang")))))
				|| (this.boss_info != null && (this.boss_info.id == 11 || (this.boss_info.mob != null && this.boss_info.mob.mob_template != null
						&& (this.boss_info.mob.mob_template.mob_id == 172 || (this.boss_info.mob.mob_template.name != null && (this.boss_info.mob.mob_template.name.toLowerCase().contains("râu trắng") || this.boss_info.mob.mob_template.name.toLowerCase().contains("rau trang")))))));
	}

	public void setupRauTrangStats() {
		this.hp_max = 2000000000;
		this.hp = 2000000000;
		this.mp = 1000000000;
		this.mp_max = 1000000000;
		this.final_dame = 250000;
		this.phong_thu = 60000;
		this.mien_thuong = 70;
		this.giam_mien_thuong = 400;
		this.max_dame_per_hit = 5000000;
		this.ne_don = 15;
		this.phan_dame = 10;
		if (this.mob_template != null) {
			this.mob_template.hOne = 120;
			this.mob_template.hp_max = 2000000000;
			this.mob_template.skill = new short[] { 210, 211, 243, 244 };
		}
	}

	public boolean isBossTheGioi1() {
		if (this.mob_template != null && Boss.isWorldBoss(this.mob_template.mob_id)) {
			return true;
		}
		if (this.boss_info != null && this.boss_info.thegioi == 1) {
			if (this.isRauTrang()) {
				return false;
			}
			if (this.mob_template != null) {
				int mId = this.mob_template.mob_id;
				if (mId == 121 || mId == 153 || mId == 172 || mId == 174) {
					return false;
				}
			}
			if (this.map != null && this.map.template != null && this.map.template.id == 1001) {
				return false;
			}
			return true;
		}
		return false;
	}

	public void setupTheGioi1Stats() {
		int mobId = this.mob_template != null ? this.mob_template.mob_id : -1;
		int lv = this.level > 0 ? this.level : (this.mob_template != null ? this.mob_template.level : 50);

		switch (mobId) {
			case 135: // Siêu Along (lv 45)
				this.final_dame = 60000;
				this.phong_thu = 15000;
				this.mien_thuong = 50;
				this.giam_mien_thuong = 200;
				this.ne_don = 10;
				this.phan_dame = 5;
				this.max_dame_per_hit = 1500000;
				break;
			case 136: // Siêu Smoker (lv 55)
				this.final_dame = 80000;
				this.phong_thu = 20000;
				this.mien_thuong = 55;
				this.giam_mien_thuong = 250;
				this.ne_don = 12;
				this.phan_dame = 7;
				this.max_dame_per_hit = 2000000;
				break;
			case 137: // Siêu Mr. 3 (lv 65)
				this.final_dame = 100000;
				this.phong_thu = 25000;
				this.mien_thuong = 60;
				this.giam_mien_thuong = 300;
				this.ne_don = 15;
				this.phan_dame = 8;
				this.max_dame_per_hit = 2500000;
				break;
			case 138: // Siêu Wapol (lv 75)
				this.final_dame = 125000;
				this.phong_thu = 30000;
				this.mien_thuong = 65;
				this.giam_mien_thuong = 350;
				this.ne_don = 15;
				this.phan_dame = 10;
				this.max_dame_per_hit = 3000000;
				break;
			case 139: // Siêu Crocodile (lv 85)
				this.final_dame = 150000;
				this.phong_thu = 35000;
				this.mien_thuong = 70;
				this.giam_mien_thuong = 380;
				this.ne_don = 18;
				this.phan_dame = 10;
				this.max_dame_per_hit = 3500000;
				break;
			case 140: // Siêu Thần Enel (lv 95)
				this.final_dame = 180000;
				this.phong_thu = 40000;
				this.mien_thuong = 70;
				this.giam_mien_thuong = 400;
				this.ne_don = 20;
				this.phan_dame = 12;
				this.max_dame_per_hit = 4000000;
				break;
			default: // Boss thegioi=1 khác
				this.final_dame = Math.max(60000, lv * 1800);
				this.phong_thu = Math.max(15000, lv * 400);
				this.mien_thuong = Math.min(70, Math.max(50, 50 + (lv - 40) / 2));
				this.giam_mien_thuong = Math.min(400, Math.max(200, 200 + (lv - 40) * 4));
				this.ne_don = Math.min(20, Math.max(10, 10 + (lv - 40) / 5));
				this.phan_dame = Math.min(15, Math.max(5, 5 + (lv - 40) / 7));
				this.max_dame_per_hit = Math.max(1500000, (long) lv * 40000);
				break;
		}
	}

	public long calculate_damage_taken(long raw_dame) {
		if (raw_dame <= 0) {
			return 0;
		}
		// Tự động nạp chỉ số cho Boss Râu Trắng nếu chưa có
		if (this.isRauTrang()) {
			if (this.mien_thuong <= 0) {
				this.setupRauTrangStats();
			}
		}
		// Tự động nạp chỉ số cho Boss Đảo Ruby nếu chưa có
		if (this.map != null && this.map.template != null && this.map.template.id == 1001) {
			if (this.mien_thuong <= 0) {
				this.mien_thuong = 80;
				this.ne_don = 25;
				this.phan_dame = 20;
				this.giam_mien_thuong = 400;
				this.final_dame = 200000;
			}
		}
		// Tự động nạp chỉ số cho Boss Thế Giới nếu chưa có
		if (this.isBossTheGioi1()) {
			if (this.mien_thuong <= 0) {
				this.setupTheGioi1Stats();
			}
		}
		long dame = raw_dame;
		// 1. Trừ điểm phòng thủ
		if (this.phong_thu > 0) {
			dame = Math.max(1, dame - this.phong_thu);
		}
		// 2. Giảm sát thương theo % miễn thương
		if (this.mien_thuong > 0) {
			int mt = Math.min(99, this.mien_thuong);
			dame = (dame * (100 - mt)) / 100;
		}
		// 3. Giới hạn sát thương tối đa mỗi hit
		if (this.max_dame_per_hit > 0 && dame > this.max_dame_per_hit) {
			dame = this.max_dame_per_hit;
		}
		return Math.max(1, dame);
	}
}
