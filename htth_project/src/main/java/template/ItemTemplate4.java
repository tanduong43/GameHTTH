package template;

import java.util.ArrayList;
import java.util.List;

public class ItemTemplate4 {
	public static List<ItemTemplate4> ENTRYS = new ArrayList<>();
	public short icon;
	public short id;
	public String name;
	public byte type;
	public short ruby;
	public int beri;
	public byte istrade;
	public short timedelay;
	public short value;
	public short timeactive;
	public String nameuse;
	public short indexInfoPotion;

	public static ItemTemplate4 get_it_by_id(int id) {
		for (int i = 0; i < ItemTemplate4.ENTRYS.size(); i++) {
			if (ItemTemplate4.ENTRYS.get(i).id == id) {
				return ItemTemplate4.ENTRYS.get(i);
			}
		}
		if (id == 1015) {
			ItemTemplate4 it = new ItemTemplate4();
			it.id = 1015;
			it.name = "Trái Nikyu Nikyu";
			it.icon = 190;
			it.indexInfoPotion = 457;
			it.beri = 10;
			it.ruby = 0;
			it.istrade = 1;
			it.type = 7;
			it.nameuse = "Ăn";
			ItemTemplate4.ENTRYS.add(it);
			return it;
		}
		if (id == 1016) {
			ItemTemplate4 it = new ItemTemplate4();
			it.id = 1016;
			it.name = "Trái Ope Ope";
			it.icon = 191;
			it.indexInfoPotion = 458;
			it.beri = 10;
			it.ruby = 0;
			it.istrade = 1;
			it.type = 7;
			it.nameuse = "Ăn";
			ItemTemplate4.ENTRYS.add(it);
			return it;
		}
		if (id == 1017) {
			ItemTemplate4 it = new ItemTemplate4();
			it.id = 1017;
			it.name = "Trái Nika";
			it.icon = 683;
			it.indexInfoPotion = 459;
			it.beri = 10;
			it.ruby = 0;
			it.istrade = 1;
			it.type = 7;
			it.nameuse = "Ăn";
			ItemTemplate4.ENTRYS.add(it);
			return it;
		}
		return null;
	}

	public static ItemTemplate4 get_it_by_name(String name) {
		for (int i = 0; i < ItemTemplate4.ENTRYS.size(); i++) {
			if (ItemTemplate4.ENTRYS.get(i).name.equals(name)) {
				return ItemTemplate4.ENTRYS.get(i);
			}
		}
		return null;
	}

	public static String get_item_name(int id) {
		if (id == 1015) return "Trái Nikyu Nikyu";
		if (id == 1016) return "Trái Ope Ope";
		if (id == 1017) return "Trái Nika";
		String s = "";
		for (int i = 0; i < ItemTemplate4.ENTRYS.size(); i++) {
			if (ItemTemplate4.ENTRYS.get(i).id == id) {
				s = ItemTemplate4.ENTRYS.get(i).name;
				break;
			}
		}
		return s;
	}
}
