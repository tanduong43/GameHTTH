/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package client;

import core.Service;
import io.Message;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import template.GiftBox;
import template.ItemTemplate4;

/**
 *
 * @author LAPTOP
 */
public class Qua_tu_hop1 {

    public static void process(Player p, Message m2) throws IOException {
        short idItem = m2.reader().readShort();
        byte cat = m2.reader().readByte();
        byte select = m2.reader().readByte();
        if (idItem == 1010 && cat == 4 && (select == 0 || select == 1 || select == 2 || select == 3 || select == 4 || select == 5)) { // rương taq tu chon
            if (p.item.total_item_bag_by_id(4, 1010) > 0) {
                short[] listId = new short[]{32, 92, 93, 160, 161, 240};
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(listId[select]);
                p.item.remove_item47(4, 1010, 1);
                Service.UpdateInfoMaincharInfo(p);
                //
                List<GiftBox> listGift = new ArrayList<>();
                GiftBox gb_ = new GiftBox();
                gb_.id = itemTemplate4.id;
                gb_.type = 4;
                gb_.name = itemTemplate4.name;
                gb_.icon = itemTemplate4.icon;
                gb_.num = 1;
                gb_.color = 0;
                listGift.add(gb_);
                Service.send_gift(p, 1, ItemTemplate4.get_item_name(1010), "Phần thưởng", listGift, true);
            }
        }
    }
}
