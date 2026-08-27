package template;

import java.util.List;

/**
 *
 * @author Truongbk
 */
public class ItemFashion {
    public static List<ItemFashion> ENTRYS;
    public final int price;
    public final short ID;
    public final short idIcon;
    public final String name;
    public final String info;
    public final short[] mWearing;
    public final List<Option> op;

    public ItemFashion(short ID, short IDIcon, String name, String info, short[] wear,
            List<Option> op, int price) {
        this.ID = ID;
        this.idIcon = IDIcon;
        this.name = name;
        this.info = info;
        this.mWearing = wear;
        this.op = op;
        this.price = price;
    }

    public static ItemFashion get_item(int id) {
        if (ENTRYS == null) {
            return null;
        }
        for (int i = 0; i < ItemFashion.ENTRYS.size(); i++) {
            ItemFashion temp = ItemFashion.ENTRYS.get(i);
            if (temp.ID == id || (byte) temp.ID == (byte) id) {
                return temp;
            }
        }
        return null;
    }

    public static long getDefaultDurationMs(int fashionId) {
        ItemFashion item = get_item(fashionId);
        if (item == null) {
            return -1;
        }
        if (item.info != null) {
            String lower = item.info.toLowerCase();
            if (lower.contains("vĩnh viễn")) {
                return -1;
            }
            if (lower.contains("24 giờ") || lower.contains("24h") || lower.contains("1 ngày")) {
                return 24L * 60 * 60 * 1000L;
            }
            if (lower.contains("3 ngày")) {
                return 3L * 24 * 60 * 60 * 1000L;
            }
            if (lower.contains("7 ngày")) {
                return 7L * 24 * 60 * 60 * 1000L;
            }
            if (lower.contains("30 ngày")) {
                return 30L * 24 * 60 * 60 * 1000L;
            }
        }
        if (fashionId == 130) {
            return 24L * 60 * 60 * 1000L;
        }
        return -1;
    }
}
