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
	public Map map;
}
