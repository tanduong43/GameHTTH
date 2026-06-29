package client;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import core.Manager;
import core.Service;
import core.Util;
import io.Message;
import template.*;
/**
 *
 * @author Truongbk
 */
public class UseItem {
    public static void process(Player p, Message m2) throws IOException {
        short id = m2.reader().readShort();
        byte cat = m2.reader().readByte();
        // System.out.println(id);
        // System.out.println(cat);
        switch (cat) {
            case 3: {
                use_item_3(p, id);
                break;
            }
            case 7: {
                use_item_7(p, id);
                break;
            }
            case 105: {
                ItemFashionP2 temp = p.check_fashion(id);
                if (temp != null) {
                    if (temp.is_use) {
                        temp.is_use = false;
                        //
                        if (temp.id == 122) {
                            p.tocSuper = 0;
                        }
                        //
                        p.update_info_to_all();
                        for (int i = 0; i < p.map.players.size(); i++) {
                            Player p0 = p.map.players.get(i);
                            Service.charWearing(p, p0, false);
                        }
                        Service.UpdateInfoMaincharInfo(p);
                        ItemFashionP.show_table(p, 105);
                        Service.send_box_ThongBao_OK(p,
                                "Tháo thành công " + ItemFashion.get_item(temp.id).name);
                    } else {
                        p.update_fashionP2(temp);
                        for (int i = 0; i < p.map.players.size(); i++) {
                            Player p0 = p.map.players.get(i);
                            Service.charWearing(p, p0, false);
                        }
                        Service.UpdateInfoMaincharInfo(p);
                        ItemFashionP.show_table(p, 105);
                        Service.send_box_ThongBao_OK(p,
                                "Mặc thành công " + ItemFashion.get_item(temp.id).name);
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Chưa mua vật phẩm này!");
                }
                break;
            }
            case 102: {
                ItemBoatP temp = p.check_itboat(id);
                if (temp != null) {
                    temp.is_use = true;
                    p.update_new_part_boat(temp);
                    ItemBoat.update_part_boat_when_shopping(p);
                    ItemFashionP.show_table(p, 102);
                    Service.send_box_ThongBao_OK(p, "Sử dụng " + ItemBoat.get_item(temp.id).name);
                } else {
                    Service.send_box_ThongBao_OK(p, "Chưa mua vật phẩm này!");
                }
                break;
            }
            case 108:
            case 103: {
                ItemFashionP temp = p.check_itfashionP(id, cat);
                if (temp != null) {
                    p.update_itfashionP(temp, cat);
                    for (int i = 0; i < p.map.players.size(); i++) {
                        Player p0 = p.map.players.get(i);
                        Service.charWearing(p, p0, false);
                    }
                    ItemFashionP.show_table(p, cat);
                    Service.send_box_ThongBao_OK(p,
                            "Sử dụng " + ItemHair.get_item(temp.id, cat).name);
                } else {
                    Service.send_box_ThongBao_OK(p, "Chưa mua vật phẩm này!");
                }
                break;
            }
        }
    }

    private static void use_item_7(Player p, int id) {}

    private static boolean use_item_4(Player p, int id) throws IOException {
        boolean used = true;
        ItemTemplate4 it_temp = ItemTemplate4.get_it_by_id(id);
        if (it_temp != null) {
            if (it_temp.type == 1 || it_temp.type == 2) { // item hp mp
                if (it_temp.type == 1 && p.type_pk != 0) {
                    EffTemplate eff = p.get_eff(0);
                    if (eff == null || (eff.time - System.currentTimeMillis()) < 1_000) {
                        long par = it_temp.value;
                        if (it_temp.id == 173) { // com hop hai tac
                            par = p.body.get_hp_max(true) / 20;
                        }
                        par = (par * (100 + p.body.get_hp_potion_use_percent(true) / 10)) / 100;
                        if (par < 0) {
                            par = 0;
                        }
                        if (it_temp.id == 173 && par > 10_000) { // com hop hai tac
                            par = 10_000;
                        }
                        p.add_new_eff(0, (int) par, it_temp.timedelay);
                    }
                } else if (it_temp.type == 2 && p.type_pk != 0) {
                    EffTemplate eff = p.get_eff(1);
                    if (eff == null || (eff.time - System.currentTimeMillis()) < 1_000) {
                        int par = it_temp.value;
                        par = (par * (100 + p.body.get_mp_potion_use_percent(true) / 10)) / 100;
                        p.add_new_eff(1, par, it_temp.timedelay);
                    }
                }
            } else {
                switch (id) {
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15: {
                        List<GiftBox> list = new ArrayList<>();
                        //
                        if (85 > Util.random(120)) {
                            byte it_color = (byte) ((70 > Util.random(120)) ? 0
                                    : ((20 > Util.random(120)) ? 2 : 1));
                            int bound1;
                            int bound2;
                            ItemTemplate3 template3;
                            switch (id) {
                                case 8: {
                                    bound1 = ((10 / 10) * 192);
                                    bound2 = (((10 / 10) + 1) * 192);
                                    break;
                                }
                                case 9: {
                                    bound1 = ((20 / 10) * 192);
                                    bound2 = (((20 / 10) + 1) * 192);
                                    break;
                                }
                                case 10: {
                                    bound1 = ((30 / 10) * 192);
                                    bound2 = (((30 / 10) + 1) * 192);
                                    break;
                                }
                                case 11: {
                                    bound1 = ((40 / 10) * 192);
                                    bound2 = (((40 / 10) + 1) * 192);
                                    break;
                                }
                                case 12: {
                                    bound1 = ((50 / 10) * 192);
                                    bound2 = (((50 / 10) + 1) * 192);
                                    break;
                                }
                                case 13: {
                                    bound1 = ((60 / 10) * 192);
                                    bound2 = (((60 / 10) + 1) * 192);
                                    break;
                                }
                                case 14: {
                                    bound1 = ((70 / 10) * 192);
                                    bound2 = (((70 / 10) + 1) * 192);
                                    break;
                                }
                                case 15: {
                                    bound1 = ((80 / 10) * 192);
                                    bound2 = (((80 / 10) + 1) * 192);
                                    break;
                                }
                                default: {
                                    bound1 = 0;
                                    bound2 = 192;
                                    break;
                                }
                            }
                            template3 = ItemTemplate3.get_it_by_id(Util.random(bound1, bound2));
                            int id_exact = template3.id;
                            if ((template3.typeEquip == 0 || template3.typeEquip == 1
                                    || template3.typeEquip == 3 || template3.typeEquip == 5)
                                    && template3.clazz != p.clazz && 90 > Util.random(120)) {
                                template3 = ItemTemplate3.get_item_random(template3.typeEquip,
                                        p.clazz, bound1, bound2);
                            }
                            id_exact = template3.id;
                            id_exact -= (template3.color - it_color);
                            template3 = ItemTemplate3.get_it_by_id(id_exact);
                            GiftBox gb1 = new GiftBox();
                            if (template3 != null) {
                                gb1.id = template3.id;
                                gb1.type = 3;
                                gb1.name = template3.name;
                                gb1.icon = template3.icon;
                                gb1.num = 1;
                                gb1.color = template3.color;
                                list.add(gb1);
                            }
                        }
                        //
                        GiftBox gb2 = new GiftBox();
                        ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb2.id = it_temp4.id;
                            gb2.type = 4;
                            gb2.name = it_temp4.name;
                            gb2.icon = it_temp4.icon;
                            gb2.num = Util.random(50, 180);
                            gb2.color = 0;
                            list.add(gb2);
                        }
                        //
                        if (80 > Util.random(120)) {
                            GiftBox gb3 = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(Util.random(2, 6));
                            if (it_temp4 != null) {
                                gb3.id = it_temp4.id;
                                gb3.type = 4;
                                gb3.name = it_temp4.name;
                                gb3.icon = it_temp4.icon;
                                gb3.num = Util.random(2, 5);
                                gb3.color = 0;
                                list.add(gb3);
                            }
                        }
                        if (list.size() > 0) {
                            Service.send_gift(p, 1, "Mở khóa rương",
                                    ItemTemplate4.get_item_name(id), list, true);
                        }
                        break;
                    }
                    case 16:
                    case 17: {
                        List<GiftBox> list = new ArrayList<>();
                        //
                        if (85 > Util.random(120)) {
                            byte it_color = (byte) ((70 > Util.random(120)) ? 0
                                    : ((20 > Util.random(120)) ? 2 : 1));
                            ItemTemplate4 temp4;
                            if (id == 16) {
                                temp4 = ItemTemplate4
                                        .get_it_by_id(it_color == 0 ? Util.random(304, 307)
                                                : (it_color == 1 ? Util.random(307, 310)
                                                        : Util.random(310, 313)));
                            } else {
                                temp4 = ItemTemplate4
                                        .get_it_by_id(it_color == 0 ? Util.random(536, 539)
                                                : (it_color == 1 ? Util.random(539, 542)
                                                        : Util.random(542, 545)));
                            }
                            GiftBox gb1 = new GiftBox();
                            gb1.id = temp4.id;
                            gb1.type = 4;
                            gb1.name = temp4.name;
                            gb1.icon = temp4.icon;
                            gb1.num = 1;
                            gb1.color = 0;
                            list.add(gb1);
                        }
                        //
                        GiftBox gb2 = new GiftBox();
                        ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb2.id = it_temp4.id;
                            gb2.type = 4;
                            gb2.name = it_temp4.name;
                            gb2.icon = it_temp4.icon;
                            gb2.num = Util.random(50, 180);
                            gb2.color = 0;
                            list.add(gb2);
                        }
                        //
                        if (80 > Util.random(120)) {
                            GiftBox gb3 = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(Util.random(2, 6));
                            if (it_temp4 != null) {
                                gb3.id = it_temp4.id;
                                gb3.type = 4;
                                gb3.name = it_temp4.name;
                                gb3.icon = it_temp4.icon;
                                gb3.num = Util.random(2, 5);
                                gb3.color = 0;
                                list.add(gb3);
                            }
                        }
                        if (list.size() > 0) {
                            Service.send_gift(p, 1, "Mở khóa rương",
                                    ItemTemplate4.get_item_name(id), list, true);
                        }
                        break;
                    }
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26: {
                        List<GiftBox> list = new ArrayList<>();
                        //
                        if (80 > Util.random(120)) {
                            byte it_color = (byte) ((60 > Util.random(120)) ? 1
                                    : ((20 > Util.random(120)) ? 3
                                            : (20 > Util.random(120) ? 0 : 2)));
                            int bound1;
                            int bound2;
                            ItemTemplate3 template3;
                            switch (id) {
                                case 19: {
                                    bound1 = ((10 / 10) * 192);
                                    bound2 = (((10 / 10) + 1) * 192);
                                    break;
                                }
                                case 20: {
                                    bound1 = ((20 / 10) * 192);
                                    bound2 = (((20 / 10) + 1) * 192);
                                    break;
                                }
                                case 21: {
                                    bound1 = ((30 / 10) * 192);
                                    bound2 = (((30 / 10) + 1) * 192);
                                    break;
                                }
                                case 22: {
                                    bound1 = ((40 / 10) * 192);
                                    bound2 = (((40 / 10) + 1) * 192);
                                    break;
                                }
                                case 23: {
                                    bound1 = ((50 / 10) * 192);
                                    bound2 = (((50 / 10) + 1) * 192);
                                    break;
                                }
                                case 24: {
                                    bound1 = ((60 / 10) * 192);
                                    bound2 = (((60 / 10) + 1) * 192);
                                    break;
                                }
                                case 25: {
                                    bound1 = ((70 / 10) * 192);
                                    bound2 = (((70 / 10) + 1) * 192);
                                    break;
                                }
                                case 26: {
                                    bound1 = ((80 / 10) * 192);
                                    bound2 = (((80 / 10) + 1) * 192);
                                    break;
                                }
                                default: {
                                    bound1 = 0;
                                    bound2 = 192;
                                    break;
                                }
                            }
                            template3 = ItemTemplate3.get_it_by_id(Util.random(bound1, bound2));
                            int id_exact = template3.id;
                            if ((template3.typeEquip == 0 || template3.typeEquip == 1
                                    || template3.typeEquip == 3 || template3.typeEquip == 5)
                                    && template3.clazz != p.clazz && 90 > Util.random(120)) {
                                template3 = ItemTemplate3.get_item_random(template3.typeEquip,
                                        p.clazz, bound1, bound2);
                            }
                            id_exact = template3.id;
                            id_exact -= (template3.color - it_color);
                            template3 = ItemTemplate3.get_it_by_id(id_exact);
                            // while (template3.color > it_color) {
                            // id_exact--;
                            // template3 = ItemTemplate3.get_it_by_id(id_exact);
                            // }
                            // while (template3.color < it_color) {
                            // id_exact++;
                            // template3 = ItemTemplate3.get_it_by_id(id_exact);
                            // }
                            GiftBox gb1 = new GiftBox();
                            if (template3 != null) {
                                gb1.id = template3.id;
                                gb1.type = 3;
                                gb1.name = template3.name;
                                gb1.icon = template3.icon;
                                gb1.num = 1;
                                gb1.color = template3.color;
                                list.add(gb1);
                            }
                        }
                        //
                        GiftBox gb2 = new GiftBox();
                        ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb2.id = it_temp4.id;
                            gb2.type = 4;
                            gb2.name = it_temp4.name;
                            gb2.icon = it_temp4.icon;
                            gb2.num = Util.random(100, 500);
                            gb2.color = 0;
                            list.add(gb2);
                        }
                        //
                        if (80 > Util.random(120)) {
                            GiftBox gb3 = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(Util.random(2, 6));
                            if (it_temp4 != null) {
                                gb3.id = it_temp4.id;
                                gb3.type = 4;
                                gb3.name = it_temp4.name;
                                gb3.icon = it_temp4.icon;
                                gb3.num = Util.random(3, 6);
                                gb3.color = 0;
                                list.add(gb3);
                            }
                        }
                        if (25 > Util.random(120)) {
                            GiftBox gb4 = new GiftBox();
                            short[] id_random = new short[] {44, 50, 56, 62, 68, 74};
                            it_temp4 = ItemTemplate4
                                    .get_it_by_id(id_random[Util.random(id_random.length)]);
                            if (it_temp4 != null) {
                                gb4.id = it_temp4.id;
                                gb4.type = 4;
                                gb4.name = it_temp4.name;
                                gb4.icon = it_temp4.icon;
                                gb4.num = 1;
                                gb4.color = 0;
                                list.add(gb4);
                            }
                        }
                        if (list.size() > 0) {
                            Service.send_gift(p, 1, "Mở khóa rương",
                                    ItemTemplate4.get_item_name(id), list, true);
                        }
                        break;
                    }
                    case 27:
                    case 28: {
                        List<GiftBox> list = new ArrayList<>();
                        //
                        if (80 > Util.random(120)) {
                            byte it_color = (byte) ((60 > Util.random(120)) ? 1
                                    : ((20 > Util.random(120)) ? 3
                                            : (20 > Util.random(120) ? 0 : 2)));
                            ItemTemplate4 temp4;
                            if (id == 27) {
                                temp4 = ItemTemplate4
                                        .get_it_by_id(
                                                it_color == 0 ? Util.random(304, 307)
                                                        : (it_color == 1 ? Util.random(307, 310)
                                                                : (it_color == 2
                                                                        ? Util.random(310, 313)
                                                                        : Util.random(313, 316))));
                            } else {
                                temp4 = ItemTemplate4
                                        .get_it_by_id(
                                                it_color == 0 ? Util.random(536, 539)
                                                        : (it_color == 1 ? Util.random(539, 542)
                                                                : (it_color == 2
                                                                        ? Util.random(542, 545)
                                                                        : Util.random(545, 548))));
                            }
                            GiftBox gb1 = new GiftBox();
                            gb1.id = temp4.id;
                            gb1.type = 4;
                            gb1.name = temp4.name;
                            gb1.icon = temp4.icon;
                            gb1.num = 1;
                            gb1.color = 0;
                            list.add(gb1);
                        }
                        //
                        GiftBox gb2 = new GiftBox();
                        ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb2.id = it_temp4.id;
                            gb2.type = 4;
                            gb2.name = it_temp4.name;
                            gb2.icon = it_temp4.icon;
                            gb2.num = Util.random(100, 500);
                            gb2.color = 0;
                            list.add(gb2);
                        }
                        //
                        if (80 > Util.random(120)) {
                            GiftBox gb3 = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(Util.random(2, 6));
                            if (it_temp4 != null) {
                                gb3.id = it_temp4.id;
                                gb3.type = 4;
                                gb3.name = it_temp4.name;
                                gb3.icon = it_temp4.icon;
                                gb3.num = Util.random(3, 6);
                                gb3.color = 0;
                                list.add(gb3);
                            }
                        }
                        if (25 > Util.random(120)) {
                            GiftBox gb4 = new GiftBox();
                            short[] id_random = new short[] {44, 50, 56, 62, 68, 74};
                            it_temp4 = ItemTemplate4
                                    .get_it_by_id(id_random[Util.random(id_random.length)]);
                            if (it_temp4 != null) {
                                gb4.id = it_temp4.id;
                                gb4.type = 4;
                                gb4.name = it_temp4.name;
                                gb4.icon = it_temp4.icon;
                                gb4.num = 1;
                                gb4.color = 0;
                                list.add(gb4);
                            }
                        }
                        if (list.size() > 0) {
                            Service.send_gift(p, 1, "Mở khóa rương",
                                    ItemTemplate4.get_item_name(id), list, true);
                        }
                        break;
                    }
                    case 29: {
                        if (p.check_already_have_devil_fruit()) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn đã có 1 trái ác quỷ trong hành trang!");
                            return false;
                        } else {
                            short id_add = 86;
                            if (20 > Util.random(120)) {
                                id_add = 87;
                            }
                            if (!p.item.add_item_bag47(4, id_add, 1)) {
                                Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống!");
                                return false;
                            }
                            open_taq_random(p, id_add, "Rương ác quỷ", "Nhận ngẫu nhiên");
                        }
                        break;
                    }
                    case 31: {
                        int diemGiam = 100;
                        if (p.pointPk >= 1000) {
                            diemGiam = 20;
                        }
                        p.update_point_pk(-diemGiam);
                        Service.send_box_ThongBao_OK(p, "Dùng vật phẩm giảm " + diemGiam
                                + ". Điểm hiếu chiến hiện tại của bạn là " + p.pointPk);
                        break;
                    }
                    case 40: {
                        p.update_key_boss(1);
                        Service.CountDown_Ticket(p);
                        p.item.update_assets_Inventory(false);
                        Service.send_box_ThongBao_OK(p, "Số lượng chìa hiện tại: "
                                + p.get_key_boss() + " / " + p.get_key_boss_max());
                        break;
                    }
                    case 158: {
                        if (p.check_already_have_devil_fruit()) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn đã có 1 trái ác quỷ trong hành trang!");
                            return false;
                        } else {
                            short id_add;
                            int rdom = Util.random(1000);
                            if (rdom < 50) { // 316
                                id_add = 316;
                            } else if (rdom < 130) { // 32
                                id_add = 32;
                            } else if (rdom < 210) { // 93
                                id_add = 93;
                            } else if (rdom < 330) { // 317
                                id_add = 317;
                            } else if (rdom < 450) { // 92
                                id_add = 92;
                            } else if (rdom < 580) { // 219
                                id_add = 219;
                            } else if (rdom < 710) { // 220
                                id_add = 220;
                            } else { // 33
                                id_add = 33;
                            }
                            if (2 > Util.random(150)) {
                                id_add = (short) ((50 > Util.random(120)) ? 240
                                        : (50 > Util.random(120)) ? 161 : 160);
                            }
                            if (!p.item.add_item_bag47(4, id_add, 1)) {
                                Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống!");
                                return false;
                            }
                            if (id_add == 240 || id_add == 161 || id_add == 160) {
                                Manager.gI().chatKTG(0,
                                        (p.name + " mở Rương đại ác quỷ nhận được "
                                                + ItemTemplate4.get_item_name(id_add)
                                                + ", thật là may mắn"),
                                        5);
                            }
                            open_taq_random(p, id_add, "Rương đại ác quỷ", "Nhận ngẫu nhiên");
                        }
                        break;
                    }
                    case 32:
                    case 33:
                    case 34:
                    case 88:
                    case 90:
                    case 91:
                    case 92:
                    case 93:
                    case 160:
                    case 161:
                    case 219:
                    case 220:
                    case 240:
                    case 316:
                    case 317:
                    case 318:
                    case 427: {
                        Service.send_box_yesno(p, (id + 4000), "Thông báo",
                                "Bạn có muốn sử dụng " + ItemTemplate4.get_it_by_id(id).name,
                                new String[] {"Đồng ý", "Hủy"}, new byte[] {-1, -1});
                        return false;
                    }
                    case 80: {
                        EffTemplate eff = p.get_eff(2);
                        if (eff != null && (eff.time > (System.currentTimeMillis() + 3000L))) {
                            if ((eff.time - System.currentTimeMillis()) < (1000L * 60 * 60 * 24
                                    * 7)) {
                                eff.time += (1000L * 60 * 60 * 2);
                            }
                        } else {
                            p.add_new_eff(2, 2, (60_000L * 60 * 2));
                        }
                        Service.CountDown_Ticket(p);
                        Service.send_box_ThongBao_OK(p,
                                "Dùng x2 exp thành công, lưu ý thời gian cộng dồn tối đa 7 ngày");
                        break;
                    }
                    case 86: {
                        Service.send_box_yesno(p, 35, "Thông báo",
                                "Bạn có muốn sử dụng Trái Ác Quỷ?", new String[] {"Đồng ý", "Hủy"},
                                new byte[] {-1, -1});
                        return false;
                    }
                    case 87: {
                        Service.send_box_yesno(p, 37, "Thông báo",
                                "Bạn có muốn sử dụng Trái Ác Quỷ trung cấp?",
                                new String[] {"Đồng ý", "Hủy"}, new byte[] {-1, -1});
                        return false;
                    }
                    case 112:
                    case 113:
                    case 114:
                    case 115:
                    case 116:
                    case 117:
                    case 118:
                    case 119:
                    case 120:
                    case 121: {
                        open_box(p, ItemTemplate4.get_it_by_id(id).type, (id - 111) * 10);
                        break;
                    }
                    case 122:
                    case 123:
                    case 124:
                    case 125:
                    case 126:
                    case 127:
                    case 128:
                    case 129:
                    case 130:
                    case 131: {
                        open_box(p, ItemTemplate4.get_it_by_id(id).type, (id - 121) * 10);
                        break;
                    }
                    case 133: {
                        EffTemplate eff = p.get_eff(17);
                        if (eff != null && (eff.time > (System.currentTimeMillis() + 3000L))) {
                            if ((eff.time - System.currentTimeMillis()) < (1000L * 60 * 60 * 24
                                    * 7)) {
                                eff.time += (1000L * 60 * 60 * 2);
                            }
                        } else {
                            p.add_new_eff(17, 3, (60_000L * 60 * 2));
                        }
                        eff = p.get_eff(17);
                        Service.send_box_ThongBao_OK(p,
                                "Thời gian EXP đặc biệt còn lại "
                                        + Util.get_time_str_by_sec2(
                                                eff.time - System.currentTimeMillis())
                                        + "\nLưu ý thời gian cộng dồn tối đa 7 ngày");
                        break;
                    }
                    case 159: {
                        EffTemplate eff = p.get_eff(3);
                        if (eff != null && (eff.time > (System.currentTimeMillis() + 3000L))) {
                            if ((eff.time - System.currentTimeMillis()) < (1000L * 60 * 60 * 24
                                    * 7)) {
                                eff.time += (1000L * 60 * 60 * 2);
                            }
                        } else {
                            p.add_new_eff(3, 3, (60_000L * 60 * 2));
                        }
                        eff = p.get_eff(3);
                        Service.send_box_ThongBao_OK(p, "Thời gian x2 kỹ năng EXP còn lại "
                                + Util.get_time_str_by_sec2(eff.time - System.currentTimeMillis())
                                + "\nCó thể xem lại ở npc Robin, Lưu ý thời gian cộng dồn tối đa 7 ngày");
                        break;
                    }
                    case 179: {
                        EffTemplate eff = p.get_eff(4);
                        if (eff != null && (eff.time > (System.currentTimeMillis() + 3000L))) {
                            if ((eff.time - System.currentTimeMillis()) < (1000L * 60 * 60 * 24
                                    * 7)) {
                                eff.time += (1000L * 60 * 5);
                            }
                        } else {
                            p.add_new_eff(4, 50, (60_000L * 5));
                            //
                            p.update_info_to_all();
                        }
                        break;
                    }
                    case 455: {
                        int random_color =
                                (10 > Util.random(120)) ? 3 : ((50 > Util.random(120)) ? 2 : 1);
                        for (int i = 0; i < p.skill_point.size(); i++) {
                            if (p.skill_point.get(i).temp.indexSkillInServer == 666) {
                                random_color = (70 > Util.random(100)) ? 3 : 2;
                                break;
                            }
                        }
                        int id_random = 0;
                        switch (random_color) {
                            case 1: {
                                id_random = (5 > Util.random(120)) ? 12012
                                        : ((20 > Util.random(120)) ? 12011
                                                : ((40 > Util.random(120)) ? 12010 : 12009));
                                break;
                            }
                            case 2: {
                                id_random = (5 > Util.random(120)) ? 12008
                                        : ((20 > Util.random(120)) ? 12007
                                                : ((40 > Util.random(120)) ? 12006 : 12005));
                                break;
                            }
                            case 3: {
                                id_random = (5 > Util.random(120)) ? 12004
                                        : ((20 > Util.random(120)) ? 12003
                                                : ((40 > Util.random(120)) ? 12002 : 12001));
                                break;
                            }
                        }
                        //
                        List<GiftBox> list = new ArrayList<>();
                        ItemTemplate3 itemTemplate3 = ItemTemplate3.get_it_by_id(id_random);
                        if (itemTemplate3 != null) {
                            GiftBox gb4 = new GiftBox();
                            gb4.id = (short) id_random;
                            gb4.type = 3;
                            gb4.name = itemTemplate3.name;
                            gb4.icon = itemTemplate3.icon;
                            gb4.num = 1;
                            gb4.color = itemTemplate3.color;
                            list.add(gb4);
                        }
                        if (list.size() > 0) {
                            Service.send_gift(p, 1, "Mở rương Dial", "Nhận được", list, true);
                        }
                        break;
                    }
                    case 519: {
                        switch (p.map.template.id) {
                            case 191:
                            case 113:
                            case 93:
                            case 83:
                            case 69:
                            case 49:
                            case 41:
                            case 33:
                            case 25:
                            case 17:
                            case 9:
                            case 1: {
                                List<GiftBox> listGift = new ArrayList<>();
                                int id_random = Util.random(493, 518);
                                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(id_random);
                                GiftBox giftBox = new GiftBox();
                                giftBox.id = (short) id_random;
                                giftBox.type = 4;
                                giftBox.name = itemTemplate4.name;
                                giftBox.icon = itemTemplate4.icon;
                                giftBox.num = 1;
                                giftBox.color = 0;
                                listGift.add(giftBox);
                                Service.send_gift(p, 0, "Rương hành trình", "Phần thưởng", listGift,
                                        true);
                                break;
                            }
                            default: {
                                Service.send_box_ThongBao_OK(p, "Hãy đứng ở làng để mở rương");
                                used = false;
                                break;
                            }
                        }
                        break;
                    }
                    case 548: {
                        EffTemplate eff = p.get_eff(8);
                        if (eff != null && (eff.time > (System.currentTimeMillis() + 3000L))) {
                            if ((eff.time - System.currentTimeMillis()) < (1000L * 60 * 60 * 24
                                    * 30)) {
                                eff.time += (1000L * 60 * 60 * 2);
                            }
                        } else {
                            p.add_new_eff(8, 2, (60_000L * 60 * 2));
                        }
                        eff = p.get_eff(8);
                        Service.send_box_ThongBao_OK(p, "Thời gian khóa EXP còn lại "
                                + Util.get_time_str_by_sec2(eff.time - System.currentTimeMillis())
                                + "\nCó thể xem lại ở npc Robin, Lưu ý thời gian cộng dồn tối đa 30 ngày");
                        break;
                    }
                    default: {
                        Service.send_box_ThongBao_OK(p, "Hiện tại "
                                + ItemTemplate4.get_item_name(id) + " chưa sử dụng được");
                        used = false;
                        break;
                    }
                }
            }
        } else {
            Service.send_box_ThongBao_OK(p, "Vật phẩm lỗi, hãy báo cho admin");
            used = false;
        }
        return used;
    }

    private static void open_taq_random(Player p, int id, String name1, String name2)
            throws IOException {
        Message m = new Message(-34);
        m.writer().writeByte(21);
        m.writer().writeShort(-1);
        m.writer().writeUTF(name1);
        m.writer().writeUTF(name2);
        m.writer().writeByte(1);
        ItemTemplate4 it_temp = ItemTemplate4.get_it_by_id(id);
        m.writer().writeByte(4);
        m.writer().writeUTF(it_temp.name);
        m.writer().writeShort(it_temp.icon);
        m.writer().writeInt(1);
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    private static void open_box(Player p, byte type, int level) throws IOException {
        if (level == 90 || level == 100) {
            List<GiftBox> list = new ArrayList<>();
            switch (level) {
                case 90: {
                    GiftBox gb1 = new GiftBox();
                    ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(Util.random(313, 316));
                    if (it_temp4 != null) {
                        gb1.id = it_temp4.id;
                        gb1.type = 4;
                        gb1.name = it_temp4.name;
                        gb1.icon = it_temp4.icon;
                        gb1.num = Util.random(1, 4);
                        gb1.color = 0;
                        list.add(gb1);
                    }
                    break;
                }
                case 100: {
                    GiftBox gb1 = new GiftBox();
                    ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(Util.random(545, 548));
                    if (it_temp4 != null) {
                        gb1.id = it_temp4.id;
                        gb1.type = 4;
                        gb1.name = it_temp4.name;
                        gb1.icon = it_temp4.icon;
                        gb1.num = Util.random(1, 4);
                        gb1.color = 0;
                        list.add(gb1);
                    }
                    break;
                }
            }
            if (list.size() > 0) {
                Service.send_gift(p, 1, "Mở khóa rương cam " + level + " cùng hệ",
                        "Mảnh trang bị " + level, list, true);
            }
        } else {
            switch (type) {
                case 22: {
                    int bound1 = ((level / 10) * 192), bound2 = (((level / 10) + 1) * 192);
                    List<Item_wear> list_receiv = new ArrayList<>();
                    Item_wear temp = new Item_wear();
                    int id_add = 0;
                    if (90 > Util.random(120)) { // cung he
                        while (!(ItemTemplate3.get_it_by_id(id_add).color == 3
                                && ItemTemplate3.get_it_by_id(id_add).typeEquip < 6
                                && (ItemTemplate3.get_it_by_id(id_add).clazz == 0
                                        || ItemTemplate3.get_it_by_id(id_add).clazz == p.clazz))) {
                            id_add = Util.random(bound1, bound2);
                        }
                    } else {
                        while (!(ItemTemplate3.get_it_by_id(id_add).color == 3
                                && ItemTemplate3.get_it_by_id(id_add).typeEquip < 6)) {
                            id_add = Util.random(bound1, bound2);
                        }
                    }
                    if (id_add > 0) {
                        temp.setup_template_by_id(id_add);
                        list_receiv.add(temp);
                        if (temp.template != null) {
                            temp.numLoKham = (byte) ((50 > Util.random(120)) ? 0
                                    : (70 > Util.random(120) ? 1 : 2));
                            p.item.add_item_bag3(temp);
                        }
                        Service.open_box_item3_orange(p, list_receiv, 545, "Mở Khóa Rương",
                                ("Rương Đồ Cam Lv" + level));
                    } else {
                        Service.send_box_ThongBao_OK(p, "Lỗi, hãy thử lại");
                    }
                    break;
                }
                case 23: {
                    int bound1 = ((level / 10) * 192), bound2 = (((level / 10) + 1) * 192);
                    List<Item_wear> list_receiv = new ArrayList<>();
                    Item_wear temp = new Item_wear();
                    int id_add = 0;
                    while (!(ItemTemplate3.get_it_by_id(id_add).color == 3
                            && ItemTemplate3.get_it_by_id(id_add).typeEquip < 6
                            && (ItemTemplate3.get_it_by_id(id_add).clazz == 0
                                    || ItemTemplate3.get_it_by_id(id_add).clazz == p.clazz))) {
                        id_add = Util.random(bound1, bound2);
                    }
                    if (id_add > 0) {
                        temp.setup_template_by_id(id_add);
                        list_receiv.add(temp);
                        if (temp.template != null) {
                            temp.numLoKham = (byte) ((50 > Util.random(120)) ? 0
                                    : (70 > Util.random(120) ? 1 : 2));
                            p.item.add_item_bag3(temp);
                        }
                        Service.open_box_item3_orange(p, list_receiv, 545, "Mở Khóa Rương",
                                ("Rương Đồ Cam cùng hệ Lv" + level));
                    } else {
                        Service.send_box_ThongBao_OK(p, "Lỗi, hãy thử lại");
                    }
                    break;
                }
            }
        }
    }

    private static void use_item_3(Player p, int id) throws IOException {
        if (p.use_item_3 == -1) {
            if (p.item.able_bag() < 1) {
                Service.send_box_ThongBao_OK(p,
                        "hãy để trống 1 ô trong hành trang mới thực hiện đc nhé");
                return;
            }
            Item_wear it = p.item.bag3[id];
            if (it != null) {
                if (check_it_can_wear(it.template.typeEquip)) {
                    p.use_item_3 = id;
                    if (it.typelock == 1 || it.valueKichAn == 12) {
                        p.wear_item(it);
                    } else {
                        Service.send_box_yesno(p, 0, "Thông báo",
                                "Khi trang bị lên người vật phẩm "
                                        + ItemTemplate3.get_it_by_id(it.template.id).name
                                        + " sẽ chuyển sang trạng thái khóa không thể giao dịch. "
                                        + "Bạn có muốn trang bị?",
                                new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
                    }
                } else if (it.template.id == 11001) {
                    List<String> name_skill = new ArrayList<>();
                    for (int i = 0; i < p.skill_point.size(); i++) {
                        if (p.skill_point.get(i).temp.ID >= 2000
                                && p.skill_point.get(i).temp.typeSkill == 1) {
                            name_skill.add(p.skill_point.get(i).temp.name);
                        }
                    }
                    if (name_skill.size() > 0) {
                        String[] str = new String[name_skill.size() + 1];
                        byte[] select = new byte[name_skill.size() + 1];
                        for (int i = 0; i < str.length - 1; i++) {
                            str[i] = name_skill.get(i);
                            select[i] = (byte) -1;
                        }
                        str[str.length - 1] = "Hủy";
                        select[str.length - 1] = (byte) -1;
                        p.data_yesno = new int[] {id};
                        Service.send_box_yesno(p, 38, "Thông báo",
                                "Bạn muốn dùng cho skill nào hiện tại?", str, select);
                    } else {
                        Service.send_box_ThongBao_OK(p,
                                "Chưa có kỹ năng nào có thể dùng với vật phẩm này");
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Chưa có chức năng");
                }
            }
        }
    }

    public static boolean check_it_can_wear(byte type) {
        return type >= 0 && type <= 5 || type == 7;
    }

    public static void use_item_potion(Player p, int id) throws IOException {
        int numInBag = p.item.total_item_bag_by_id(4, id);
        if (numInBag > 0) {
            if (use_item_4(p, id)) {
                if (id != 271) {
                    p.item.remove_item47(4, id, 1);
                }
                Message m2 = new Message(-13);
                m2.writer().writeShort(id);
                m2.writer().writeShort(p.item.total_item_bag_by_id(4, id));
                p.conn.addmsg(m2);
                m2.cleanup();
                //
                p.item.update_Inventory(-1, false);
            }
        }
    }
}
