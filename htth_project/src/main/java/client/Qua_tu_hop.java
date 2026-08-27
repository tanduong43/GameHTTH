/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package client;

import core.Service;
import core.Util;
import io.Message;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import template.GiftBox;
import template.ItemFashion;
import template.ItemFashionP;
import template.ItemFashionP2;
import template.ItemTemplate3;
import template.ItemTemplate4;

/**
 *
 * @author LAPTOP
 */
public class Qua_tu_hop {

    public static void process(Player p, Message m2) throws IOException {
        short idItem = m2.reader().readShort();
        byte cat = m2.reader().readByte();
        byte select = m2.reader().readByte();
        if (idItem == 690 && cat == 4 && (select == 0 || select == 1 || select == 2 || select == 3 || select == 4 || select == 5)) { // rương taq tu chon
            if (p.item.total_item_bag_by_id(4, 690) > 0) {
                short[] listId = new short[]{32, 92, 93, 160, 161, 240};
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(listId[select]);
                p.item.remove_item47(4, 690, 1);
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
                Service.send_gift(p, 1, ItemTemplate4.get_item_name(690), "Phần thưởng", listGift, true);
            }
        }
        if (idItem == 1003 && cat == 4 && (select == 0 || select == 1 || select == 2)) { // rương đại ác quỷ cao cấp tự chọn
            if (p.item.total_item_bag_by_id(4, 1003) > 0) {
                short[] listId = new short[]{160, 161, 240};
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(listId[select]);
                p.item.remove_item47(4, 1003, 1);
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
                Service.send_gift(p, 1, ItemTemplate4.get_item_name(1003), "Phần thưởng", listGift, true);
            }
        }
        if (idItem == 1010 && cat == 4 && (select == 0 || select == 1 || select == 2 || select == 3 || select == 4 || select == 5 || select == 6 || select == 7)) { // rương taq tu chon
            if (p.item.total_item_bag_by_id(4, 1010) > 0) {
                short[] listId = new short[]{12017, 12018, 12019, 12020, 12021, 12022, 12023, 12024};
                ItemTemplate3 itemTemplate3 = ItemTemplate3.get_it_by_id(listId[select]);
                p.item.remove_item47(4, 1010, 1);
                Service.UpdateInfoMaincharInfo(p);
                //
                List<GiftBox> listGift = new ArrayList<>();
                GiftBox gb_ = new GiftBox();
                gb_.id = itemTemplate3.id;
                gb_.type = 3;
                gb_.name = itemTemplate3.name;
                gb_.icon = itemTemplate3.icon;
                gb_.num = 1;
                gb_.color = 7;
                listGift.add(gb_);
                Service.send_gift(p, 1, ItemTemplate4.get_item_name(1010), "Phần thưởng", listGift, true);
            }
        }
        if (idItem == 1011 && cat == 4 && (select == 0 || select == 1 || select == 2 || select == 3 || select == 4 || select == 5)) { // rương taq tu chon
            if (p.item.total_item_bag_by_id(4, 1011) > 0) {
                short[] listId = new short[]{706, 704, 705, 707, 708, 709};
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(listId[select]);
                p.item.remove_item47(4, 1011, 1);
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
                Service.send_gift(p, 1, ItemTemplate4.get_item_name(1011), "Phần thưởng", listGift, true);
            }
        }
                if (idItem == 1012 && cat == 4 && (select == 0 || select == 1 || select == 2 || select == 3 || select == 4 || select == 5)) { // rương taq tu chon
            if (p.item.total_item_bag_by_id(4, 1012) > 0) {
                short[] listId = new short[]{1, 1, 1};
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(listId[select]);
                p.item.remove_item47(4, 1012, 1);
                Service.UpdateInfoMaincharInfo(p);
                //
                List<GiftBox> listGift = new ArrayList<>();
                GiftBox gb_ = new GiftBox();
                gb_.id = itemTemplate4.id;
                gb_.type = 4;
                gb_.name = itemTemplate4.name;
                gb_.icon = itemTemplate4.icon;
                 gb_.num = Util.random(10, 500);
                gb_.color = 0;
                listGift.add(gb_);
                Service.send_gift(p, 1, ItemTemplate4.get_item_name(1012), "Phần thưởng", listGift, true);
            }
        }
        if (idItem == 1013 && cat == 4 && (select == 0 || select == 1 || select == 2 || select == 3 || select == 4 || select == 5)) { // rương taq tu chon
            if (p.item.total_item_bag_by_id(4, 1013) > 0) {
                short[] listId = new short[]{55};
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(listId[select]);
                p.item.remove_item47(4, 1013, 1);
                Service.UpdateInfoMaincharInfo(p);
                //
                List<GiftBox> listGift = new ArrayList<>();
                GiftBox gb_ = new GiftBox();
                gb_.id = itemTemplate4.id;
                gb_.type = 105;
                gb_.name = itemTemplate4.name;
                gb_.icon = itemTemplate4.icon;
                gb_.num = Util.random(1);
                gb_.color = 0;
                listGift.add(gb_);
                Service.send_gift(p, 1, ItemTemplate4.get_item_name(1013), "Phần thưởng", listGift, true);
            }
        }
        if (idItem == 691 && cat == 105) { // Thời trang sơ cấp (menuId từ UseItem)
            short[] listId = new short[]{49, 50};
            if (select >= 0 && select < listId.length) {
                ItemFashion fashion = ItemFashion.get_item(listId[select]);
                if (fashion != null) {
                    if (p.check_fashion(fashion.ID) != null) {
                        Service.send_box_ThongBao_OK(p, "Bạn đã có thời trang " + fashion.name + " rồi!");
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 1001) > 0) {
                        p.item.remove_item47(4, 1001, 1);
                    } else if (p.item.total_item_bag_by_id(1, 1001) > 0) {
                        p.item.remove_item47(1, 1001, 1);
                    } else {
                        Service.send_box_ThongBao_OK(p, "Bạn không có Rương Thời Trang Sơ!");
                        return;
                    }
                    Service.UpdateInfoMaincharInfo(p);
                    
                    ItemFashionP2 temp2 = new ItemFashionP2();
                    temp2.id = fashion.ID;
                    long dur = ItemFashion.getDefaultDurationMs(fashion.ID);
                    temp2.expiryTime = (dur > 0) ? (System.currentTimeMillis() + dur) : -1;
                    p.fashion.add(temp2);
                    p.update_fashionP2(temp2);
                    
                    for (int i = 0; i < p.map.players.size(); i++) {
                        Player p0 = p.map.players.get(i);
                        Service.Weapon_fashion(p0, p, false);
                    }
                    ItemFashionP.show_table(p, 105);
                    Service.send_box_ThongBao_OK(p, "Bạn đã nhận thời trang: " + fashion.name);
                }
            }
        }
        if (idItem == 692 && cat == 105) { // Thời trang cao cấp (menuId từ UseItem)
            short[] listId = new short[]{54, 55, 59, 110, 109, 120, 118, 239, 243, 237};
            if (select >= 0 && select < listId.length) {
                ItemFashion fashion = ItemFashion.get_item(listId[select]);
                if (fashion != null) {
                    if (p.check_fashion(fashion.ID) != null) {
                        Service.send_box_ThongBao_OK(p, "Bạn đã có thời trang " + fashion.name + " rồi!");
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 1002) > 0) {
                        p.item.remove_item47(4, 1002, 1);
                    } else if (p.item.total_item_bag_by_id(1, 1002) > 0) {
                        p.item.remove_item47(1, 1002, 1);
                    } else {
                        Service.send_box_ThongBao_OK(p, "Bạn không có Rương Thời Trang Cao!");
                        return;
                    }
                    Service.UpdateInfoMaincharInfo(p);
                    
                    ItemFashionP2 temp2 = new ItemFashionP2();
                    temp2.id = fashion.ID;
                    long dur = ItemFashion.getDefaultDurationMs(fashion.ID);
                    temp2.expiryTime = (dur > 0) ? (System.currentTimeMillis() + dur) : -1;
                    p.fashion.add(temp2);
                    p.update_fashionP2(temp2);
                    
                    for (int i = 0; i < p.map.players.size(); i++) {
                        Player p0 = p.map.players.get(i);
                        Service.Weapon_fashion(p0, p, false);
                    }
                    ItemFashionP.show_table(p, 105);
                    Service.send_box_ThongBao_OK(p, "Bạn đã nhận thời trang: " + fashion.name);
                }
            }
        }
        if (idItem == 1001 && cat == 105) { // Thời trang sơ cấp
            if (p.item.total_item_bag_by_id(4, 1001) > 0 || p.item.total_item_bag_by_id(1, 1001) > 0) {
                short[] listId = new short[]{49, 50};
                if (select >= 0 && select < listId.length) {
                    ItemFashion fashion = ItemFashion.get_item(listId[select]);
                    if (fashion != null) {
                        if (p.check_fashion(fashion.ID) != null) {
                            Service.send_box_ThongBao_OK(p, "Bạn đã có thời trang " + fashion.name + " rồi!");
                            return;
                        }
                        if (p.item.total_item_bag_by_id(4, 1001) > 0) {
                            p.item.remove_item47(4, 1001, 1);
                        } else {
                            p.item.remove_item47(1, 1001, 1);
                        }
                        Service.UpdateInfoMaincharInfo(p);
                        
                        ItemFashionP2 temp2 = new ItemFashionP2();
                        temp2.id = fashion.ID;
                        long dur = ItemFashion.getDefaultDurationMs(fashion.ID);
                        temp2.expiryTime = (dur > 0) ? (System.currentTimeMillis() + dur) : -1;
                        p.fashion.add(temp2);
                        p.update_fashionP2(temp2);
                        
                        for (int i = 0; i < p.map.players.size(); i++) {
                            Player p0 = p.map.players.get(i);
                            Service.Weapon_fashion(p0, p, false);
                        }
                        ItemFashionP.show_table(p, 105);
                        Service.send_box_ThongBao_OK(p, "Bạn đã nhận thời trang: " + fashion.name);
                    }
                }
            } else {
                Service.send_box_ThongBao_OK(p, "Bạn không có Thời trang sơ cấp!");
            }
        }
        if (idItem == 9910 && cat == 105) { // Thời trang sơ cấp (id cũ)
            if (p.item.total_item_bag_by_id(4, 1001) > 0 || p.item.total_item_bag_by_id(1, 1001) > 0) {
                short[] listId = new short[]{49, 50};
                if (select >= 0 && select < listId.length) {
                    ItemFashion fashion = ItemFashion.get_item(listId[select]);
                    if (fashion != null) {
                        if (p.check_fashion(fashion.ID) != null) {
                            Service.send_box_ThongBao_OK(p, "Bạn đã sở hữu thời trang này rồi!");
                            return;
                        }
                        if (p.item.total_item_bag_by_id(4, 1001) > 0) {
                            p.item.remove_item47(4, 1001, 1);
                        } else {
                            p.item.remove_item47(1, 1001, 1);
                        }
                        Service.UpdateInfoMaincharInfo(p);
                        
                        ItemFashionP2 temp2 = new ItemFashionP2();
                        temp2.id = fashion.ID;
                        long dur = ItemFashion.getDefaultDurationMs(fashion.ID);
                        temp2.expiryTime = (dur > 0) ? (System.currentTimeMillis() + dur) : -1;
                        p.fashion.add(temp2);
                        p.update_fashionP2(temp2);
                        
                        for (int i = 0; i < p.map.players.size(); i++) {
                            Player p0 = p.map.players.get(i);
                            Service.Weapon_fashion(p0, p, false);
                        }
                        ItemFashionP.show_table(p, 105);
                        Service.send_box_ThongBao_OK(p, "Bạn đã nhận được thời trang: " + fashion.name);
                    }
                }
            } else {
                Service.send_box_ThongBao_OK(p, "Bạn không có Thời trang sơ cấp!");
            }
        }
        if (idItem == 1002 && cat == 105) { // Thời trang cao cấp
            if (p.item.total_item_bag_by_id(4, 1002) > 0 || p.item.total_item_bag_by_id(1, 1002) > 0) {
                short[] listId = new short[]{54, 55, 59, 110, 109, 120, 118, 239, 243, 237};
                if (select >= 0 && select < listId.length) {
                    ItemFashion fashion = ItemFashion.get_item(listId[select]);
                    if (fashion != null) {
                        if (p.check_fashion(fashion.ID) != null) {
                            Service.send_box_ThongBao_OK(p, "Bạn đã có thời trang " + fashion.name + " rồi!");
                            return;
                        }
                        if (p.item.total_item_bag_by_id(4, 1002) > 0) {
                            p.item.remove_item47(4, 1002, 1);
                        } else {
                            p.item.remove_item47(1, 1002, 1);
                        }
                        Service.UpdateInfoMaincharInfo(p);
                        
                        ItemFashionP2 temp2 = new ItemFashionP2();
                        temp2.id = fashion.ID;
                        long dur = ItemFashion.getDefaultDurationMs(fashion.ID);
                        temp2.expiryTime = (dur > 0) ? (System.currentTimeMillis() + dur) : -1;
                        p.fashion.add(temp2);
                        p.update_fashionP2(temp2);
                        
                        for (int i = 0; i < p.map.players.size(); i++) {
                            Player p0 = p.map.players.get(i);
                            Service.Weapon_fashion(p0, p, false);
                        }
                        ItemFashionP.show_table(p, 105);
                        Service.send_box_ThongBao_OK(p, "Bạn đã nhận thời trang: " + fashion.name);
                    }
                }
            } else {
                Service.send_box_ThongBao_OK(p, "Bạn không có Thời trang cao cấp!");
            }
        }
        if (idItem == 9911 && cat == 105) { // Thời trang cao cấp (id cũ)
            if (p.item.total_item_bag_by_id(4, 1002) > 0 || p.item.total_item_bag_by_id(1, 1002) > 0) {
                short[] listId = new short[]{54, 55, 59, 110, 109, 120, 118, 239, 243, 237};
                if (select >= 0 && select < listId.length) {
                    ItemFashion fashion = ItemFashion.get_item(listId[select]);
                    if (fashion != null) {
                        if (p.check_fashion(fashion.ID) != null) {
                            Service.send_box_ThongBao_OK(p, "Bạn đã sở hữu thời trang này rồi!");
                            return;
                        }
                        if (p.item.total_item_bag_by_id(4, 1002) > 0) {
                            p.item.remove_item47(4, 1002, 1);
                        } else {
                            p.item.remove_item47(1, 1002, 1);
                        }
                        Service.UpdateInfoMaincharInfo(p);
                        
                        ItemFashionP2 temp2 = new ItemFashionP2();
                        temp2.id = fashion.ID;
                        long dur = ItemFashion.getDefaultDurationMs(fashion.ID);
                        temp2.expiryTime = (dur > 0) ? (System.currentTimeMillis() + dur) : -1;
                        p.fashion.add(temp2);
                        p.update_fashionP2(temp2);
                        
                        for (int i = 0; i < p.map.players.size(); i++) {
                            Player p0 = p.map.players.get(i);
                            Service.Weapon_fashion(p0, p, false);
                        }
                        ItemFashionP.show_table(p, 105);
                        Service.send_box_ThongBao_OK(p, "Bạn đã nhận được thời trang: " + fashion.name);
                    }
                }
            } else {
                Service.send_box_ThongBao_OK(p, "Bạn không có Thời trang cao cấp!");
            }
        }
    }
}
