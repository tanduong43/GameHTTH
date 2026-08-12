package client;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import core.Manager;
import static core.MenuController.send_dynamic_menu;
import core.Service;
import core.Util;
import io.Message;
import template.EffTemplate;
import template.GiftBox;
import template.ItemBoat;
import template.ItemBoatP;
import template.ItemFashion;
import template.ItemFashionP;
import template.ItemFashionP2;
import template.ItemHair;
import template.ItemTemplate3;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.Item_wear;
import template.Level;
import template.Skill_Template;
import template.Skill_info;
import map.Pokemon_normal;
import map.Mob;
import map.LeaveItemMap;
import map.Map;
import template.Option;
import map.Mob;
import java.util.Random;
import template.*;
import event.EventTrungThu;

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
            case 4: {
                try {
                    use_item_4(p, id);
                } catch (IOException e) {
                    System.out.println("Error in use_item_4: " + e.getMessage());
                }
                break;
            }
            case 3: {
                use_item_3(p, id);
                break;
            }
            case 7: {
                use_item_7(p, id);
                break;
            }
            case 105: {
                // Chuẩn hóa ID: client gửi lại ID đã bị cắt thành sbyte (ID > 127)
                ItemFashion fashionTemp = ItemFashion.get_item(id);
                if (fashionTemp != null) {
                    id = fashionTemp.ID;
                }
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

    private static void use_item_7(Player p, int id) {
        // Event Trung Thu handling
        if (EventTrungThu.isEvent()) {
            try {
                EventTrungThu.useTheTTTrungThu(p);
                return;
            } catch (Exception e) {
                System.out.println("Error using fashion card: " + e.getMessage());
            }
        }
    }

    /**
     * Xử lý các item đặc biệt của sự kiện Trung Thu
     */
    private static void handleTrungThuItem(Player p, int id) throws IOException {
        if (p.item.total_item_bag_by_id(4, id) <= 0) {
            Service.send_box_ThongBao_OK(p, "Bạn không có vật phẩm này!");
            return;
        }

        switch (id) {
            case EventTrungThu.ITEM_BANH_TRUNG_THU:
            case EventTrungThu.ITEM_BANH_DAU_XANH:
            case EventTrungThu.ITEM_BANH_TRUNG_MUOI:
            case EventTrungThu.ITEM_BANH_HAT_SEN:
                EventTrungThu.openBanh(p, id);
                p.item.remove_item47(4, id, 1);
                break;
            case EventTrungThu.ITEM_DEN_KEO_QUAN:
                EventTrungThu.useDenKeoQuan(p);
                p.item.remove_item47(4, id, 1);
                break;
            case EventTrungThu.ITEM_HOP_BANH:
                EventTrungThu.openHopBanh(p);
                p.item.remove_item47(4, id, 1);
                break;
            case EventTrungThu.ITEM_HOP_BANH_THUONG_HANG:
                EventTrungThu.openHopBanhThuongHang(p);
                p.item.remove_item47(4, id, 1);
                break;
            case EventTrungThu.ITEM_THE_TT_TRUNG_THU:
                EventTrungThu.useTheTTTrungThu(p);
                break;
        }
        p.item.update_Inventory(-1, false);
    }

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
                // Event Trung Thu: Xử lý các item đặc biệt
                if (EventTrungThu.isEvent() && (id == EventTrungThu.ITEM_BANH_TRUNG_THU
                        || id == EventTrungThu.ITEM_BANH_DAU_XANH
                        || id == EventTrungThu.ITEM_BANH_TRUNG_MUOI
                        || id == EventTrungThu.ITEM_BANH_HAT_SEN
                        || id == EventTrungThu.ITEM_HOP_BANH
                        || id == EventTrungThu.ITEM_HOP_BANH_THUONG_HANG
                        || id == EventTrungThu.ITEM_DEN_KEO_QUAN
                        || id == EventTrungThu.ITEM_THE_TT_TRUNG_THU)) {
                    try {
                        handleTrungThuItem(p, id);
                        return true;
                    } catch (Exception e) {
                        System.out.println("Error handling Trung Thu item: " + e.getMessage());
                    }
                }

                if (it_temp.name != null && (it_temp.name.toLowerCase().contains("pháo hoa")
                        || it_temp.name.toLowerCase().contains("phao hoa") || id == 359 || id == 361)) {
                    Service.send_eff(p, 23, 0); // Hiệu ứng Pháo hoa rực rỡ
                    p.num_phao_hoa++;

                    boolean isMilestone = (p.num_phao_hoa == 1 || p.num_phao_hoa == 10 || p.num_phao_hoa == 100
                            || p.num_phao_hoa == 1000 || p.num_phao_hoa == 10000 || p.num_phao_hoa == 100000);

                    if (isMilestone) {
                        Manager.gI().chatKTG(0, "Chúc mừng " + p.name + " vừa bắn đạt mốc "
                                + Util.number_format(p.num_phao_hoa) + " Pháo hoa rực rỡ!", 5);
                    }

                    // Tặng quà pháo hoa ngẫu nhiên từ danh sách quy định
                    int rewardType = Util.random(10);
                    int catReward = 4;
                    short idReward = 86;
                    int quantReward = 1;
                    String rewardName = "";

                    switch (rewardType) {
                        case 0: { // Bột vàng (cat 7, id 4)
                            catReward = 7;
                            idReward = 4;
                            quantReward = Util.random(1, 4);
                            rewardName = ItemTemplate7.get_item_name(idReward);
                            break;
                        }
                        case 1: { // Ngôi sao may mắn (cat 7, id 5)
                            catReward = 7;
                            idReward = 5;
                            quantReward = 1;
                            rewardName = ItemTemplate7.get_item_name(idReward);
                            break;
                        }
                        case 2: { // Mai rùa (cat 7, id 6)
                            catReward = 7;
                            idReward = 6;
                            quantReward = 1;
                            rewardName = ItemTemplate7.get_item_name(idReward);
                            break;
                        }
                        case 3: { // Đá ác quỷ (cat 7, id 9)
                            catReward = 7;
                            idReward = 9;
                            quantReward = Util.random(1, 3);
                            rewardName = ItemTemplate7.get_item_name(idReward);
                            break;
                        }
                        case 4: { // Rương ác quỷ (cat 4, id 86)
                            catReward = 4;
                            idReward = 86;
                            quantReward = 1;
                            rewardName = ItemTemplate4.get_item_name(idReward);
                            break;
                        }
                        case 5: { // Rương đại ác quỷ (cat 4, id 87)
                            catReward = 4;
                            idReward = 87;
                            quantReward = 1;
                            rewardName = ItemTemplate4.get_item_name(idReward);
                            break;
                        }
                        case 6: { // Đá hải thạch 1 (cat 4, id 221)
                            catReward = 4;
                            idReward = 221;
                            quantReward = Util.random(1, 3);
                            rewardName = ItemTemplate4.get_item_name(idReward);
                            break;
                        }
                        case 7: { // Đá cấp 3 các loại (cat 4)
                            short[] gemLvl3 = new short[] { 44, 50, 56, 62, 68, 74 };
                            catReward = 4;
                            idReward = gemLvl3[Util.random(gemLvl3.length)];
                            quantReward = 1;
                            rewardName = ItemTemplate4.get_item_name(idReward);
                            break;
                        }
                        case 8: { // Hổ phách cấp 1, 2, 3 (cat 4, id 362, 363, 364)
                            short[] hoPhach = new short[] { 362, 363, 364 };
                            catReward = 4;
                            idReward = hoPhach[Util.random(hoPhach.length)];
                            quantReward = 1;
                            rewardName = ItemTemplate4.get_item_name(idReward);
                            break;
                        }
                        case 9: { // Kinh nghiệm X2 (cat 4, id 80)
                            catReward = 4;
                            idReward = 80;
                            quantReward = 1;
                            rewardName = ItemTemplate4.get_item_name(idReward);
                            break;
                        }
                    }

                    if (p.item.add_item_bag47(catReward, idReward, quantReward)) {
                        p.item.update_Inventory(-1, false);

                        List<GiftBox> gifts = new ArrayList<>();
                        GiftBox gb = new GiftBox();
                        gb.id = idReward;
                        gb.type = (byte) catReward;
                        gb.num = quantReward;
                        if (catReward == 4) {
                            ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(idReward);
                            if (itemTemplate4 != null) {
                                gb.name = itemTemplate4.name;
                                gb.icon = itemTemplate4.icon;
                            }
                        } else if (catReward == 7) {
                            ItemTemplate7 itemTemplate7 = ItemTemplate7.get_it_by_id(idReward);
                            if (itemTemplate7 != null) {
                                gb.name = itemTemplate7.name;
                                gb.icon = itemTemplate7.icon;
                            }
                        }
                        gifts.add(gb);
                        Service.send_gift(p, 1, "Pháo Hoa",
                                "Bạn vừa đốt 1 Pháo Hoa!\nTổng đã đốt: " + Util.number_format(p.num_phao_hoa), gifts,
                                true);

                        // Tặng quà cho các người chơi khác trong map
                        for (int i = 0; i < p.map.players.size(); i++) {
                            Player p0 = p.map.players.get(i);
                            if (p0 != null && p0 != p) {
                                int rewardType0 = Util.random(10);
                                int catReward0 = 4;
                                short idReward0 = 86;
                                int quantReward0 = 1;
                                switch (rewardType0) {
                                    case 0: {
                                        catReward0 = 7;
                                        idReward0 = 4;
                                        quantReward0 = Util.random(1, 4);
                                        break;
                                    }
                                    case 1: {
                                        catReward0 = 7;
                                        idReward0 = 5;
                                        quantReward0 = 1;
                                        break;
                                    }
                                    case 2: {
                                        catReward0 = 7;
                                        idReward0 = 6;
                                        quantReward0 = 1;
                                        break;
                                    }
                                    case 3: {
                                        catReward0 = 7;
                                        idReward0 = 9;
                                        quantReward0 = Util.random(1, 3);
                                        break;
                                    }
                                    case 4: {
                                        catReward0 = 4;
                                        idReward0 = 86;
                                        quantReward0 = 1;
                                        break;
                                    }
                                    case 5: {
                                        catReward0 = 4;
                                        idReward0 = 87;
                                        quantReward0 = 1;
                                        break;
                                    }
                                    case 6: {
                                        catReward0 = 4;
                                        idReward0 = 221;
                                        quantReward0 = Util.random(1, 3);
                                        break;
                                    }
                                    case 7: {
                                        short[] gemLvl3 = new short[] { 44, 50, 56, 62, 68, 74 };
                                        catReward0 = 4;
                                        idReward0 = gemLvl3[Util.random(gemLvl3.length)];
                                        quantReward0 = 1;
                                        break;
                                    }
                                    case 8: {
                                        short[] hoPhach = new short[] { 362, 363, 364 };
                                        catReward0 = 4;
                                        idReward0 = hoPhach[Util.random(hoPhach.length)];
                                        quantReward0 = 1;
                                        break;
                                    }
                                    case 9: {
                                        catReward0 = 4;
                                        idReward0 = 80;
                                        quantReward0 = 1;
                                        break;
                                    }
                                }
                                if (p0.item.add_item_bag47(catReward0, idReward0, quantReward0)) {
                                    p0.item.update_Inventory(-1, false);
                                    List<GiftBox> gifts0 = new ArrayList<>();
                                    GiftBox gb0 = new GiftBox();
                                    gb0.id = idReward0;
                                    gb0.type = (byte) catReward0;
                                    gb0.num = quantReward0;
                                    if (catReward0 == 4) {
                                        ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(idReward0);
                                        if (itemTemplate4 != null) {
                                            gb0.name = itemTemplate4.name;
                                            gb0.icon = itemTemplate4.icon;
                                        }
                                    } else if (catReward0 == 7) {
                                        ItemTemplate7 itemTemplate7 = ItemTemplate7.get_it_by_id(idReward0);
                                        if (itemTemplate7 != null) {
                                            gb0.name = itemTemplate7.name;
                                            gb0.icon = itemTemplate7.icon;
                                        }
                                    }
                                    gifts0.add(gb0);
                                    Service.send_gift(p0, 1, "Quà Pháo Hoa",
                                            p.name + " vừa đốt Pháo Hoa!\nBạn may mắn nhận được lộc rơi xuống!", gifts0,
                                            true);
                                } else {
                                    Service.send_box_ThongBao_OK(p0,
                                            "Hành trang không đủ chỗ trống để nhận lộc Pháo Hoa từ " + p.name + "!");
                                }
                            }
                        }

                        try {
                            p.map.send_chat_popup(0, p.index_map, "Vừa đốt pháo hoa rực rỡ!", true);
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống!");
                        return false;
                    }
                    return true;
                }
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
                            short[] id_random = new short[] { 44, 50, 56, 62, 68, 74 };
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
                            short[] id_random = new short[] { 44, 50, 56, 62, 68, 74 };
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
                        short id_add = 86;
                        if (20 > Util.random(120)) {
                            id_add = 87;
                        }
                        if (!p.item.add_item_bag47(4, id_add, 1)) {
                            Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống!");
                            return false;
                        }
                        open_taq_random(p, id_add, "Rương ác quỷ", "Nhận ngẫu nhiên");
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
                    case 271: {
                        Service.input_text(p, 271, "Đổi tên nhân vật", new String[] { "Nhập tên mới" });
                        used = false;
                        break;
                    }
                    case 158: {
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
                    case 427:
                    case 800:
                    case 801:
                    case 802: {
                        Service.send_box_yesno(p, (id + 4000), "Thông báo",
                                "Bạn có muốn sử dụng " + ItemTemplate4.get_it_by_id(id).name,
                                new String[] { "Đồng ý", "Hủy" }, new byte[] { -1, -1 });
                        return false;
                    }
                    case 228: {
                        ItemFashion[] fashions = {
                                ItemFashion.get_item(109),
                                ItemFashion.get_item(110),
                                ItemFashion.get_item(119),
                                ItemFashion.get_item(123),
                                ItemFashion.get_item(74),
                                ItemFashion.get_item(128),
                                ItemFashion.get_item(120),
                                ItemFashion.get_item(77),
                                ItemFashion.get_item(103),
                                ItemFashion.get_item(104),
                                ItemFashion.get_item(85),
                                ItemFashion.get_item(86),
                                ItemFashion.get_item(87),
                                ItemFashion.get_item(88),
                                ItemFashion.get_item(89)
                        };

                        List<ItemFashion> validFashions = new ArrayList<>();
                        List<Integer> listIds = new ArrayList<>();

                        for (ItemFashion f : fashions) {
                            if (f != null) {
                                validFashions.add(f);
                                listIds.add((int) f.ID);
                            }
                        }

                        if (validFashions.isEmpty()) {
                            Service.send_box_ThongBao_OK(p, "Không tìm thấy thời trang trong hệ thống!");
                            return false;
                        }

                        p.data_yesno = new int[listIds.size() + 1];
                        p.data_yesno[0] = 228;
                        for (int i = 0; i < listIds.size(); i++) {
                            p.data_yesno[i + 1] = listIds.get(i);
                        }

                        Service.open_box_fashion(p, validFashions, 228, "Hộp thời trang cấp 1",
                                "Chọn thời trang bạn muốn nhận:");
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
                                "Bạn có muốn sử dụng Trái Ác Quỷ?", new String[] { "Đồng ý", "Hủy" },
                                new byte[] { -1, -1 });
                        return false;
                    }
                    case 87: {
                        Service.send_box_yesno(p, 37, "Thông báo",
                                "Bạn có muốn sử dụng Trái Ác Quỷ trung cấp?",
                                new String[] { "Đồng ý", "Hủy" }, new byte[] { -1, -1 });
                        return false;
                    }
                    case 1001: {
                        Message m = new Message(69);
                        m.writer().writeUTF("Rương Thời Trang Sơ");
                        m.writer().writeUTF("Đổi");

                        short[] ids = new short[] { 49, 50 };
                        m.writer().writeByte(ids.length);

                        for (short fashionId : ids) {
                            ItemFashion fashion = ItemFashion.get_item(fashionId);
                            if (fashion != null) {
                                m.writer().writeByte(105);
                                m.writer().writeUTF(fashion.name);
                                m.writer().writeShort(fashion.idIcon);
                                m.writer().writeByte(0);
                                m.writer().writeShort(1);
                                m.writer().writeByte(0);
                            }
                        }

                        m.writer().writeShort(691); // id menu xử lý khi chọn
                        m.writer().writeByte(105);

                        p.conn.addmsg(m);
                        m.cleanup();
                        return false;
                    }
                    case 1002: {
                        Message m = new Message(69);
                        m.writer().writeUTF("Rương Thời Trang Cao");
                        m.writer().writeUTF("Đổi");

                        short[] ids = new short[] {54, 55, 59 };
                        m.writer().writeByte(ids.length);

                        for (short fashionId : ids) {
                            ItemFashion fashion = ItemFashion.get_item(fashionId);
                            if (fashion != null) {
                                m.writer().writeByte(105);
                                m.writer().writeUTF(fashion.name);
                                m.writer().writeShort(fashion.idIcon);
                                m.writer().writeByte(0);
                                m.writer().writeShort(1);
                                m.writer().writeByte(0);
                            }
                        }

                        m.writer().writeShort(692);
                        m.writer().writeByte(105);
                        p.conn.addmsg(m);
                        m.cleanup();
                        return false;
                    }
                    case 1003: {
                        Message m = new Message(69);
                        m.writer().writeUTF("Rương Trái ác quỷ tự chọn");
                        m.writer().writeUTF("Đổi");
                        short[] ids = new short[] { 160, 161, 240 };
                        m.writer().writeByte(ids.length);
                        for (int i = 0; i < ids.length; i++) {
                            ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(ids[i]);
                            m.writer().writeByte(4);
                            m.writer().writeUTF(itemTemplate4.name);
                            m.writer().writeShort(itemTemplate4.icon);
                            m.writer().writeByte(0);
                            m.writer().writeShort(1);
                            m.writer().writeByte(0);
                        }
                        m.writer().writeShort(1003);
                        m.writer().writeByte(4);
                        p.conn.addmsg(m);
                        m.cleanup();
                        return false;
                    }
                    case 1005: {
                        qua_cau_poke(p, 5, 180);
                        break;
                    }
                    case 1006: {
                        qua_cau_poke(p, 15, 181);
                        break;
                    }
                    case 1007: {
                        qua_cau_poke(p, 25, 182);
                        break;
                    }
                    case 1008: {
                        qua_cau_poke(p, 25, 193);
                        break;
                    }
                    case 189: {
                        StringBuilder sb = new StringBuilder();
                        List<Pokemon_normal> bossesAlive = new ArrayList<>();
                        for (Pokemon_normal boss : Pokemon_normal.ENTRYS) {
                            if (!boss.mob.isdie) {
                                bossesAlive.add(boss);
                            }
                        }
                        if (!bossesAlive.isEmpty()) {
                            Random rand = new Random();
                            Pokemon_normal randomBoss = bossesAlive.get(rand.nextInt(bossesAlive.size()));

                            sb.append(String.format("pokemon %s đang ở map %s khu %d\n",
                                    randomBoss.mob.mob_template.name, randomBoss.mob.map.template.name,
                                    randomBoss.mob.map.zone_id + 1));

                            Service.send_box_ThongBao_OK(p, sb.toString());
                        } else {
                            Service.send_box_ThongBao_OK(p, "Hiện tại không có pokemon ");
                        }
                        break;
                    }
                    case 690: {
                        Message m = new Message(69);
                        m.writer().writeUTF("Rương Trái ác quỷ tự chọn");
                        m.writer().writeUTF("Đổi");
                        short[] ids = new short[] { 32, 92, 93, 160, 161, 240 };
                        m.writer().writeByte(ids.length);
                        for (int i = 0; i < ids.length; i++) {
                            ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(ids[i]);
                            m.writer().writeByte(4);
                            m.writer().writeUTF(itemTemplate4.name);
                            m.writer().writeShort(itemTemplate4.icon);
                            m.writer().writeByte(0);
                            m.writer().writeShort(1);
                            m.writer().writeByte(0);
                        }
                        m.writer().writeShort(690);
                        m.writer().writeByte(4);
                        p.conn.addmsg(m);
                        m.cleanup();
                        return false;
                    }
                    case 1004: { // Rương đá thần thoại tự chọn - hiển thị bảng đá từ id 647 đến 682
                        String[] listTenDa = new String[36];
                        int[] listIdDa = new int[36];
                        int count = 0;
                        for (int i = 647; i <= 682; i++) {
                            ItemTemplate4 it4 = ItemTemplate4.get_it_by_id(i);
                            if (it4 != null) {
                                listTenDa[count] = it4.name;
                                listIdDa[count] = i;
                                count++;
                            }
                        }
                        if (count > 0) {
                            // Lưu data để xử lý khi người chơi chọn đá
                            int[] data = new int[count + 2];
                            data[0] = 10040; // id xử lý
                            data[1] = 1004; // id chest
                            for (int i = 0; i < count; i++) {
                                data[i + 2] = listIdDa[i];
                            }
                            p.data_yesno = data;

                            send_dynamic_menu(p, 9915, "Rương Đá Thần Thoại", listTenDa, (short[]) null);
                        } else {
                            Service.send_box_ThongBao_OK(p, "Không tìm thấy đá thần thoại trong hệ thống!");
                        }
                        used = false;
                        break;
                    }
                    case 1009: {
                        // Rương Đồ VIP - random ngẫu nhiên theo lv player
                        if (p.item.total_item_bag_by_id(4, 1009) > 0) {
                            int playerLevel = p.level;
                            if (playerLevel < 10) {
                                playerLevel = 10;
                            }
                            int tier = playerLevel / 10;
                            if (tier > 22) {
                                tier = 22;
                            }
                            int bound1 = tier * 192;
                            int bound2 = (tier + 1) * 192;
                            if (bound2 > 4222) {
                                bound2 = 4222;
                            }
                            int itemId = Util.random(bound1, bound2);
                            ItemTemplate3 template3 = ItemTemplate3.get_it_by_id(itemId);
                            // fallback: nếu id random không tồn tại, thử tìm id gần nhất trong tier
                            if (template3 == null) {
                                for (int i = 0; i < 100; i++) {
                                    int tryId = bound1 + Util.random(bound2 - bound1);
                                    template3 = ItemTemplate3.get_it_by_id(tryId);
                                    if (template3 != null) {
                                        itemId = tryId;
                                        break;
                                    }
                                }
                            }
                            if (template3 != null) {
                                p.item.remove_item47(4, 1009, 1);
                                Service.UpdateInfoMaincharInfo(p);
                                List<GiftBox> listGift = new ArrayList<>();
                                GiftBox gb_ = new GiftBox();
                                gb_.id = template3.id;
                                gb_.type = 3;
                                gb_.name = template3.name;
                                gb_.icon = template3.icon;
                                gb_.num = 1;
                                gb_.color = template3.color;
                                listGift.add(gb_);
                                Service.send_gift(p, 1, "Rương Đồ Vip", "Phần thưởng", listGift, true);
                            } else {
                                Service.send_box_ThongBao_OK(p, "Không tìm thấy trang bị trong hệ thống!");
                            }
                        } else {
                            Service.send_box_ThongBao_OK(p, "Bạn không có Rương Đồ Vip!");
                        }
                        used = false;
                        break;
                    }
                    case 111: {
                        Service.send_eff(p, 20, 1000);
                        //
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
                            for (int i = 0; i < p.map.players.size(); i++) {
                                Service.send_gift(p.map.players.get(i), 1, "Phần thưởng",
                                        ItemTemplate4.get_item_name(id), list, true);
                            }
                        }
                        break;
                    }
                    case 1010: {
                        GiftBox gb1 = new GiftBox();
                        Message m = new Message(69);
                        m.writer().writeUTF("Rương Đồ Tự Chọn");
                        m.writer().writeUTF("Đổi");
                        short[] ids = new short[] { 12017, 12018, 12019, 12020, 12021, 12022, 12023, 12024 };
                        m.writer().writeByte(ids.length);
                        for (int i = 0; i < ids.length; i++) {
                            ItemTemplate3 itemTemplate3 = ItemTemplate3.get_it_by_id(ids[i]);
                            m.writer().writeByte(3);
                            m.writer().writeUTF(itemTemplate3.name);
                            m.writer().writeShort(itemTemplate3.icon);
                            gb1.color = itemTemplate3.color;
                            m.writer().writeByte(0);
                            m.writer().writeShort(1);
                            m.writer().writeByte(0);
                        }
                        m.writer().writeShort(1010);
                        m.writer().writeByte(4);
                        p.conn.addmsg(m);
                        m.cleanup();
                        return false;
                    }
                    case 1011: {
                        Message m = new Message(69);
                        m.writer().writeUTF("Rương Pet Vip Tự Chọn");
                        m.writer().writeUTF("Đổi");
                        short[] ids = new short[] { 706, 704, 705, 707, 708, 709 };
                        m.writer().writeByte(ids.length);
                        for (int i = 0; i < ids.length; i++) {
                            ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(ids[i]);
                            m.writer().writeByte(4);
                            m.writer().writeUTF(itemTemplate4.name);
                            m.writer().writeShort(itemTemplate4.icon);
                            m.writer().writeByte(0);
                            m.writer().writeShort(1);
                            m.writer().writeByte(0);
                        }
                        m.writer().writeShort(1011);
                        m.writer().writeByte(4);
                        p.conn.addmsg(m);
                        m.cleanup();
                        return false;
                    }
                    case 1012: {
                        Message m = new Message(69);
                        m.writer().writeUTF("Ruby");
                        m.writer().writeUTF("Nhận Lì Xì");
                        short[] ids = new short[] { 1, 1, 1 };
                        m.writer().writeByte(ids.length);
                        for (int i = 0; i < ids.length; i++) {
                            ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(ids[i]);
                            m.writer().writeByte(4);
                            m.writer().writeUTF(itemTemplate4.name);
                            m.writer().writeShort(itemTemplate4.icon);
                            m.writer().writeByte(0);
                            m.writer().writeShort(10000);
                            m.writer().writeByte(0);
                        }
                        m.writer().writeShort(1012);
                        m.writer().writeByte(4);
                        p.conn.addmsg(m);
                        m.cleanup();
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
                    case 803: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 10);
                        break;
                    }
                    case 804: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 20);
                        break;
                    }
                    case 805: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 30);
                        break;
                    }
                    case 806: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 40);
                        break;
                    }
                    case 807: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 50);
                        break;
                    }
                    case 808: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 60);
                        break;
                    }
                    case 809: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 70);
                        break;
                    }
                    case 810: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 80);
                        break;
                    }
                    case 811: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 90);
                        break;
                    }
                    case 812: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 100);
                        break;
                    }

                    //
                    case 813: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 10);
                        break;
                    }
                    case 814: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 20);
                        break;
                    }
                    case 815: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 30);
                        break;
                    }
                    case 816: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 40);
                        break;
                    }
                    case 817: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 50);
                        break;
                    }
                    case 818: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 60);
                        break;
                    }
                    case 819: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 70);
                        break;
                    }
                    case 820: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 80);
                        break;
                    }
                    case 821: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 90);
                        break;
                    }
                    case 822: {
                        open_box2(p, ItemTemplate4.get_it_by_id(id).type, 100);
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
                        int random_color = (10 > Util.random(120)) ? 3 : ((50 > Util.random(120)) ? 2 : 1);
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
                    case 414: {
                        // Check if player has any upgradeable default skill (< 30)
                        boolean canUpgrade = false;
                        for (int i = 0; i < p.skill_point.size(); i++) {
                            Skill_info sk = p.skill_point.get(i);
                            if (sk.temp.typeSkill == 1 && sk.temp.typeDevil == 0 && sk.temp.ID < 2000) {
                                if (sk.temp.Lv_RQ < 30) {
                                    canUpgrade = true;
                                    break;
                                }
                            }
                        }

                        if (!canUpgrade) {
                            Service.send_box_ThongBao_OK(p, "Tất cả kỹ năng mặc định đã đạt cấp tối đa (30)!");
                            used = false;
                            break;
                        }

                        // Upgrade all default skills that are < 30 by 1 level
                        boolean upgradedAny = false;
                        for (int i = 0; i < p.skill_point.size(); i++) {
                            Skill_info sk = p.skill_point.get(i);
                            if (sk.temp.typeSkill == 1 && sk.temp.typeDevil == 0 && sk.temp.ID < 2000) {
                                if (sk.temp.Lv_RQ < 30) {
                                    if (Skill_Template.upgrade_skill(sk, p.clazz)) {
                                        sk.exp = 0; // reset exp to 0 for the new level
                                        upgradedAny = true;

                                        // Send level up packet to client
                                        Message m = new Message(-28);
                                        m.writer().writeByte(1);
                                        p.write_data_skill(m.writer(), sk);
                                        p.conn.addmsg(m);
                                        m.cleanup();
                                    }
                                }
                            }
                        }

                        if (upgradedAny) {
                            p.send_skill();
                            p.update_info_to_all();
                            Service.send_box_ThongBao_OK(p, "Nâng cấp thành công toàn bộ kỹ năng mặc định lên 1 cấp!");
                            used = true;
                        } else {
                            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, không thể nâng cấp kỹ năng.");
                            used = false;
                        }
                        break;
                    }
                    case 327: {
                        short[] id_random = new short[] { 79, 73, 67, 55, 61, 49 };
                        short id_add = id_random[Util.random(id_random.length)];
                        if (!p.item.add_item_bag47(4, id_add, 1)) {
                            Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống!");
                            used = false;
                        } else {
                            p.item.update_assets_Inventory(false);
                            Service.send_box_ThongBao_OK(p, "Bạn nhận được 1 " + ItemTemplate4.get_item_name(id_add));
                            used = true;
                        }
                        break;
                    }
                    case 349: {
                        long beriAdd = 1_000_000;
                        p.update_vang(beriAdd);
                        p.update_money();
                        Service.send_box_ThongBao_OK(p, "Sử dụng thành công, bạn nhận được " + beriAdd + " Beri!");
                        used = true;
                        break;
                    }
                    case 413: {
                        if (p.level >= 100) {
                            Service.send_box_ThongBao_OK(p, "Cấp độ đã đạt tối đa");
                            used = false;
                        } else {
                            long exp_needed = Level.ENTRYS[p.level - 1].exp - p.exp;
                            if (exp_needed > 0) {
                                p.update_exp(exp_needed, false);
                            } else {
                                p.update_exp(0, false);
                            }
                            Service.send_box_ThongBao_OK(p, "Sử dụng Tiến Cấp Đơn thành công, bạn được tăng 1 cấp!");
                            used = true;
                        }
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

    private static int random_red_equip_id(int bound1, int bound2, byte clazz, boolean sameClazz) {
        for (int tryCount = 0; tryCount < 500; tryCount++) {
            int id_add = Util.random(bound1, bound2);
            ItemTemplate3 it = ItemTemplate3.get_it_by_id(id_add);
            if (it != null && it.color == 8 && it.typeEquip < 6
                    && (!sameClazz || it.clazz == 0 || it.clazz == clazz)) {
                return id_add;
            }
        }
        return 0;
    }

    private static void open_box2(Player p, byte type, int level) throws IOException {
        switch (type) {
            case 22: {
                int bound1 = ((level / 10) * 192) + 2112, bound2 = (((level / 10) + 1) * 192) + 2112;
                List<Item_wear> list_receiv = new ArrayList<>();
                Item_wear temp = new Item_wear();
                boolean sameClazz = 90 > Util.random(120);
                int id_add = random_red_equip_id(bound1, bound2, p.clazz, sameClazz);
                if (id_add > 0) {
                    temp.setup_template_by_id(id_add);
                    increaseOptions(temp);
                    list_receiv.add(temp);
                    if (temp.template != null) {
                        temp.numLoKham = (byte) ((50 > Util.random(120)) ? 0
                                : (70 > Util.random(120) ? 1 : 2));
                        p.item.add_item_bag3(temp);
                    }
                    Service.open_box_item3_orange(p, list_receiv, 545, "Mở Khóa Rương",
                            ("Rương Đồ Đỏ Lv" + level));
                } else {
                    Service.send_box_ThongBao_OK(p, "Lỗi, hãy thử lại");
                }
                break;
            }
            case 23: {
                int bound1 = ((level / 10) * 192) + 2112, bound2 = (((level / 10) + 1) * 192) + 2112 + 192;
                List<Item_wear> list_receiv = new ArrayList<>();
                Item_wear temp = new Item_wear();
                int id_add = random_red_equip_id(bound1, bound2, p.clazz, true);
                if (id_add > 0) {
                    temp.setup_template_by_id(id_add);
                    increaseOptions(temp);

                    list_receiv.add(temp);
                    if (temp.template != null) {
                        temp.numLoKham = (byte) ((50 > Util.random(120)) ? 0
                                : (70 > Util.random(120) ? 1 : 2));
                        p.item.add_item_bag3(temp);
                    }
                    Service.open_box_item3_orange(p, list_receiv, 545, "Mở Khóa Rương",
                            ("Rương Đồ Đỏ cùng hệ Lv" + level));
                } else {
                    Service.send_box_ThongBao_OK(p, "Lỗi, hãy thử lại");
                }
                break;
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
                                new String[] { "Đồng ý", "Hủy" }, new byte[] { 2, 1 });
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
                        p.data_yesno = new int[] { id };
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
                if (id != 271
                        && id != EventTrungThu.ITEM_BANH_TRUNG_THU
                        && id != EventTrungThu.ITEM_BANH_DAU_XANH
                        && id != EventTrungThu.ITEM_BANH_TRUNG_MUOI
                        && id != EventTrungThu.ITEM_BANH_HAT_SEN
                        && id != EventTrungThu.ITEM_HOP_BANH
                        && id != EventTrungThu.ITEM_HOP_BANH_THUONG_HANG
                        && id != EventTrungThu.ITEM_DEN_KEO_QUAN
                        && id != EventTrungThu.ITEM_THE_TT_TRUNG_THU) {
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

    private static void qua_cau_poke(Player p, int ti_le, int type) throws IOException {
        for (int i = 0; i < p.map.players.size(); i++) {
            Player p0 = p.map.players.get(i);
            List<Mob> list_random = new ArrayList<>();
            for (int i11 = 0; i11 < Pokemon_normal.ENTRYS.size(); i11++) {
                Pokemon_normal boss = Pokemon_normal.ENTRYS.get(i11);
                Mob mob = boss.mob;
                if (mob != null) {
                    if (!mob.isdie && Math.abs(p0.x - mob.x) < 200
                            && Math.abs(p0.y - mob.y) < 200
                            && p.id == p0.id) {
                        list_random.add(mob);
                    }
                }
            }

            if (list_random.size() > 0) {
                Mob mob_select = null;
                for (Mob mob : list_random) {
                    if (mob.mob_template.mob_id == 115 && p0.map.zone_id == mob.map.zone_id
                            && p0.map.template.id == mob.map.template.id) {
                        mob_select = mob;
                        break;
                    }
                    if (mob.mob_template.mob_id == 116 && p0.map.zone_id == mob.map.zone_id
                            && p0.map.template.id == mob.map.template.id) {
                        mob_select = mob;
                        break;
                    }
                    if (mob.mob_template.mob_id == 117 && p0.map.zone_id == mob.map.zone_id
                            && p0.map.template.id == mob.map.template.id) {
                        mob_select = mob;
                        break;
                    }
                    if (mob.mob_template.mob_id == 118 && p0.map.zone_id == mob.map.zone_id
                            && p0.map.template.id == mob.map.template.id) {
                        mob_select = mob;
                        break;
                    }
                    if (mob.mob_template.mob_id == 119 && p0.map.zone_id == mob.map.zone_id
                            && p0.map.template.id == mob.map.template.id) {
                        mob_select = mob;
                        break;
                    }
                    if (mob.mob_template.mob_id == 121 && p0.map.zone_id == mob.map.zone_id
                            && p0.map.template.id == mob.map.template.id) {
                        mob_select = mob;
                        break;
                    }
                }
                if (mob_select != null && p0.map.zone_id == mob_select.map.zone_id
                        && p0.map.template.id == mob_select.map.template.id) {
                    if (ti_le > Util.random(120)) {
                        Service.send_eff_poke(mob_select.index, p, type, 12);
                        mob_select.hp = 0;
                    } else {
                        Service.send_eff_poke(mob_select.index, p, type, 13);
                    }
                    if (mob_select.hp <= 0 && !mob_select.isdie) {
                        mob_select.hp = 0;
                        mob_select.isdie = true;
                        // boss
                        if (mob_select.poke_nor_info != null || mob_select.poke_huyen_thoai_info != null) {
                            p.map.remove_obj(mob_select.index, 1);
                            //
                            List<GiftBox> list_gift = new ArrayList<>();
                            //
                            GiftBox gb_rcam = new GiftBox();
                            ItemTemplate4 it_temp4_in;
                            if (mob_select.mob_template.mob_id == 115) {
                                it_temp4_in = ItemTemplate4.get_it_by_id(184);
                                if (it_temp4_in != null) {
                                    gb_rcam.id = it_temp4_in.id;
                                    gb_rcam.type = 4;
                                    gb_rcam.name = it_temp4_in.name;
                                    gb_rcam.icon = it_temp4_in.icon;
                                    gb_rcam.num = 1;
                                    gb_rcam.color = 0;
                                    list_gift.add(gb_rcam);
                                }
                            }
                            if (mob_select.mob_template.mob_id == 116) {
                                it_temp4_in = ItemTemplate4.get_it_by_id(183);
                                if (it_temp4_in != null) {
                                    gb_rcam.id = it_temp4_in.id;
                                    gb_rcam.type = 4;
                                    gb_rcam.name = it_temp4_in.name;
                                    gb_rcam.icon = it_temp4_in.icon;
                                    gb_rcam.num = 1;
                                    gb_rcam.color = 0;
                                    list_gift.add(gb_rcam);
                                }
                            }
                            if (mob_select.mob_template.mob_id == 117) {
                                it_temp4_in = ItemTemplate4.get_it_by_id(186);
                                if (it_temp4_in != null) {
                                    gb_rcam.id = it_temp4_in.id;
                                    gb_rcam.type = 4;
                                    gb_rcam.name = it_temp4_in.name;
                                    gb_rcam.icon = it_temp4_in.icon;
                                    gb_rcam.num = 1;
                                    gb_rcam.color = 0;
                                    list_gift.add(gb_rcam);
                                }
                            }
                            if (mob_select.mob_template.mob_id == 118) {
                                it_temp4_in = ItemTemplate4.get_it_by_id(185);
                                if (it_temp4_in != null) {
                                    gb_rcam.id = it_temp4_in.id;
                                    gb_rcam.type = 4;
                                    gb_rcam.name = it_temp4_in.name;
                                    gb_rcam.icon = it_temp4_in.icon;
                                    gb_rcam.num = 1;
                                    gb_rcam.color = 0;
                                    list_gift.add(gb_rcam);
                                }
                            }
                            if (mob_select.mob_template.mob_id == 119) {
                                it_temp4_in = ItemTemplate4.get_it_by_id(187);
                                if (it_temp4_in != null) {
                                    gb_rcam.id = it_temp4_in.id;
                                    gb_rcam.type = 4;
                                    gb_rcam.name = it_temp4_in.name;
                                    gb_rcam.icon = it_temp4_in.icon;
                                    gb_rcam.num = 1;
                                    gb_rcam.color = 0;
                                    list_gift.add(gb_rcam);
                                }
                            }
                            if (mob_select.mob_template.mob_id == 121) {
                                it_temp4_in = ItemTemplate4.get_it_by_id(188);
                                if (it_temp4_in != null) {
                                    gb_rcam.id = it_temp4_in.id;
                                    gb_rcam.type = 4;
                                    gb_rcam.name = it_temp4_in.name;
                                    gb_rcam.icon = it_temp4_in.icon;
                                    gb_rcam.num = 1;
                                    gb_rcam.color = 0;
                                    list_gift.add(gb_rcam);
                                }
                            }
                            if (list_gift.size() > 0) {
                                Service.send_gift(p, 1, "Thu phục pokemon",
                                        "Thu phục pokemon nhận",
                                        list_gift, false);
                                switch (mob_select.mob_template.mob_id) {
                                    case 115: {
                                        Pokemon_normal.result_Pokemon_lua(p, i);
                                        break;
                                    }
                                    case 121: {
                                        break;
                                    }
                                }
                            }
                        }
                    } else {
                        // ko tim thay or ch die
                    }
                }
            } else {
                if (type != 193) {
                    p.item.add_item_bag47(4, type + 825, 1);
                    p.item.update_Inventory(-1, false);
                } else {
                    p.item.add_item_bag47(4, 1008, 1);
                    p.item.update_Inventory(-1, false);
                }
            }
        }
    }

    private static void increaseOptions(Item_wear item) {
        int numOptionsToAdd = Util.random(11, 12);
        for (int i = 0; i < numOptionsToAdd; i++) {
            int optionIndex = Util.random(item.option_item.size());
            Option option = item.option_item.get(optionIndex);
            int currentParam = option.getParam();
            int newParam = currentParam + numOptionsToAdd;
            option.setParam(newParam);
        }
    }
}
