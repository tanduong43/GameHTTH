package template;

import client.Player;
import core.Util;

import java.util.ArrayList;
import java.util.List;
/**
 *
 * @author Truongbk
 */
public class GiftBox {
    public static final short[] DA_HANH_TRINH_V0 = new short[] { 493, 494 };
    public static final short[] DA_HANH_TRINH_V1 = new short[] { 495, 496, 513 };
    public static final short[] DA_HANH_TRINH_V2 = new short[] { 497, 498, 499, 500, 501, 502, 503, 504, 505, 506,
            507 };
    public static final short[] DA_HANH_TRINH_V3 = new short[] { 508, 509, 510, 511, 512, 514, 515, 516, 517 };
    public byte type;
    public String name;
    public short icon;
    public int num;
    public byte color;
    public short id;

    public static List<GiftBox> get_gift_map_boss_by_level(Player p) {
        List<GiftBox> listGift = new ArrayList<>();
        
        // 1. Beri 10000
        ItemTemplate4 itemBeri = ItemTemplate4.get_it_by_id(0);
        if (itemBeri != null) {
            GiftBox giftBeri = new GiftBox();
            giftBeri.id = 0;
            giftBeri.type = 4;
            giftBeri.name = itemBeri.name;
            giftBeri.icon = itemBeri.icon;
            giftBeri.num = 10000;
            giftBeri.color = 0;
            listGift.add(giftBeri);
        }
        
        // 2. Ruong cam cung cap
        int chestId = ((p.level < 11 ? 11 : p.level) / 10) + 111;
        ItemTemplate4 itemChest = ItemTemplate4.get_it_by_id(chestId);
        if (itemChest != null) {
            GiftBox giftChest = new GiftBox();
            giftChest.id = (short) chestId;
            giftChest.type = 4;
            giftChest.name = itemChest.name;
            giftChest.icon = itemChest.icon;
            giftChest.num = 1;
            giftChest.color = 0;
            listGift.add(giftChest);
        }
        
        // 3. Da hanh trinh (100% co hoi)
        int id_random;
        if (5 > Util.random(120)) {
            id_random = DA_HANH_TRINH_V3[Util.random(DA_HANH_TRINH_V3.length)];
        } else if (20 > Util.random(120)) {
            id_random = DA_HANH_TRINH_V2[Util.random(DA_HANH_TRINH_V2.length)];
        } else if (70 > Util.random(120)) {
            id_random = DA_HANH_TRINH_V1[Util.random(DA_HANH_TRINH_V1.length)];
        } else {
            id_random = DA_HANH_TRINH_V0[Util.random(DA_HANH_TRINH_V0.length)];
        }
        ItemTemplate4 itemStone = ItemTemplate4.get_it_by_id(id_random);
        if (itemStone != null) {
            GiftBox giftStone = new GiftBox();
            giftStone.id = (short) id_random;
            giftStone.type = 4;
            giftStone.name = itemStone.name;
            giftStone.icon = itemStone.icon;
            giftStone.num = 1;
            giftStone.color = 0;
            listGift.add(giftStone);
        }
        
        return listGift;
    }

    public static List<GiftBox> get_gift_boss_pica(Player p) {
        List<GiftBox> listGift = new ArrayList<>();
        //
        ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(0);
        GiftBox giftBox = new GiftBox();
        giftBox.id = itemTemplate4.id;
        giftBox.type = 4;
        giftBox.name = itemTemplate4.name;
        giftBox.icon = itemTemplate4.icon;
        giftBox.num = Util.random(10_000, 200_000);
        giftBox.color = 0;
        listGift.add(giftBox);
        if (60 > Util.random(120)) { // bot vang
            GiftBox gb_ = new GiftBox();
            ItemTemplate7 it_temp7 = ItemTemplate7.get_it_by_id(4);
            if (it_temp7 != null) {
                gb_.id = it_temp7.id;
                gb_.type = 7;
                gb_.name = it_temp7.name;
                gb_.icon = it_temp7.icon;
                gb_.num = Util.random(2, 10);
                gb_.color = 0;
                listGift.add(gb_);
            }
        }
        if (40 > Util.random(120)) { // ruong dai ac quy
            GiftBox gb_ = new GiftBox();
            ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(158);
            if (it_temp4 != null) {
                gb_.id = it_temp4.id;
                gb_.type = 4;
                gb_.name = it_temp4.name;
                gb_.icon = it_temp4.icon;
                gb_.num = 1;
                gb_.color = 0;
                listGift.add(gb_);
            }
        }
        if (p.level > 9 && 60 > Util.random(120)) { // ruong cam
            GiftBox gb_ = new GiftBox();
            ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id((p.level / 10) + 121);
            if (it_temp4 != null) {
                gb_.id = it_temp4.id;
                gb_.type = 4;
                gb_.name = it_temp4.name;
                gb_.icon = it_temp4.icon;
                gb_.num = 1;
                gb_.color = 0;
                listGift.add(gb_);
            }
        }
        if (40 > Util.random(120)) { // da 3-4
            short[] id_random = new short[] { 46, 52, 58, 64, 70, 76, 47, 53, 59, 65, 71, 77 };
            GiftBox gb_ = new GiftBox();
            ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(id_random[Util.random(id_random.length)]);
            if (it_temp4 != null) {
                gb_.id = it_temp4.id;
                gb_.type = 4;
                gb_.name = it_temp4.name;
                gb_.icon = it_temp4.icon;
                gb_.num = 1;
                gb_.color = 0;
                listGift.add(gb_);
            }
        }
        return listGift;
    }
}
