package template;

import java.util.List;
/**
 *
 * @author Truongbk
 */
    public class ItemTemplate4_Info {
        public static List<ItemTemplate4_Info> ENTRY;
        public short id;
        public String info;

        public static ItemTemplate4_Info get_by_id(short id) {
            for (int i = 0; i < ItemTemplate4_Info.ENTRY.size(); i++) {
                if (ItemTemplate4_Info.ENTRY.get(i).id == id) {
                    return ItemTemplate4_Info.ENTRY.get(i);
                }
            }
            return null;
        }
}
