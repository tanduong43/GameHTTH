package template;

import java.util.ArrayList;
import java.util.List;
/**
 *
 * @author Truongbk
 */
public class MobTemplate {
	public static List<MobTemplate> ENTRYS = new ArrayList<>();
	public short mob_id;
	public String name;
	public short level;
	public short hOne;
	public int hp_max;
	public byte typemove;
	public byte ishuman;
	public byte typemonster;
	public short[] wearing;
	public short icon;
	public short head;
	public short hair;
	public short[] skill;
}
