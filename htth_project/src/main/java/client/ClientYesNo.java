package client;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import activities.*;
import core.Service;
import core.Util;
import io.Message;
import map.Map;
import map.MapCanGoTo;
import map.Mob;
import map.Npc;
import map.Vgo;
import template.*;
/**
 *
 * @author Truongbk
 */
public class ClientYesNo {
    public static void process(Player p, Message m2) throws IOException {
        short id = m2.reader().readShort();
        byte value = m2.reader().readByte();
        // System.out.println("id " + id);
        // System.out.println("value " + value);
        if (id == 38 && p.data_yesno != null && p.data_yesno.length == 1) {
            List<Skill_info> name_skill = new ArrayList<>();
            for (int i = 0; i < p.skill_point.size(); i++) {
                if (p.skill_point.get(i).temp.ID >= 2000
                        && p.skill_point.get(i).temp.typeSkill == 1) {
                    name_skill.add(p.skill_point.get(i));
                }
            }
            if (name_skill.size() > 0 && value < name_skill.size()) {
                Skill_info sk_selsect = name_skill.get(value);
                if (sk_selsect.lvdevil > 4) {
                    Service.send_box_ThongBao_OK(p, "Cấp ác quỷ đã đạt tối đa");
                    p.data_yesno = null;
                    p.map_tele = null;
                    return;
                }
                int exp_devil = 0;
                if (p.item.bag3[p.data_yesno[0]] != null) {
                    if (p.item.bag3[p.data_yesno[0]].option_item.size() > 0) {
                        exp_devil += p.item.bag3[p.data_yesno[0]].option_item.get(0).getParam();
                        p.item.remove_item_wear(p.item.bag3[p.data_yesno[0]]);
                        p.item.update_Inventory(-1, false);
                    }
                }
                if (exp_devil > 0) {
                    sk_selsect.update_exp_devil(exp_devil);
                    p.send_skill();
                    p.update_info_to_all();
                    Service.send_box_ThongBao_OK(p, "Sử dụng năng lượng ác quỷ cho "
                            + sk_selsect.temp.name + " thành công");
                }
            }
            p.data_yesno = null;
            p.map_tele = null;
            return;
        }
        if (id == 39 && value < 4) {
            int vang_total = 0;
            for (int i = 0; i < p.item.bag3.length; i++) {
                if (p.item.bag3[i] != null && p.item.bag3[i].template.typeEquip < 6
                        && p.item.bag3[i].typelock != 1 && p.item.bag3[i].levelup == 0
                        && p.item.bag3[i].template.color <= value) {
                    int vang_recive = 30 + (2 * p.item.bag3[i].template.color
                            + (p.item.bag3[i].template.level / 10) + 1)
                            * DataTemplate.TabInventory_ItemSell[0];
                    if (vang_recive > DataTemplate.TabInventory_ItemSell[1]) {
                        vang_recive = DataTemplate.TabInventory_ItemSell[1];
                    }
                    vang_total += vang_recive;
                    //
                    p.item.add_item_save(p.item.bag3[i]);
                    p.item.bag3[i] = null;
                }
            }
            if (vang_total > 0 && vang_total < 20_000) {
                p.update_vang(vang_total);
                p.update_money();
            } else if (vang_total >= 20_000) {
                System.out.println("vang ban do " + vang_total);
            }
            p.item.update_Inventory(-1, false);
            p.data_yesno = null;
            p.map_tele = null;
            return;
        }
        if (value == 0) { // ok
            switch (id) {
                case 61: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        int coin = p.data_yesno[0] /5000;
                        if (p.conn.coin < coin) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn không đủ " + Util.number_format(coin) + " coin");
                            p.data_yesno = null;
                            p.map_tele = null;
                           
                            return;
                        }
                        if (p.update_coin(-coin)) {
                            p.update_vang(p.data_yesno[0]);
                            p.update_money();
                            Service.send_box_ThongBao_OK(p,
                                "Bạn đã đổi thành công " + Util.number_format(coin) + " coin ra "
                                        + Util.number_format(p.data_yesno[0]) + " Beri.");
                        }
                        
                    }
                    break;
                }
                case 60: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        int coin = p.data_yesno[0] * 5;
                        if (p.conn.coin < coin) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn không đủ " + Util.number_format(coin) + " coin");
                            p.data_yesno = null;
                            p.map_tele = null;
                           
                            return;
                        }
                        if (p.update_coin(-coin)) {
                            p.update_ngoc(p.data_yesno[0]);
                            p.update_money();
                            Service.send_box_ThongBao_OK(p,
                                "Bạn đã đổi thành công " + Util.number_format(coin) + " coin ra "
                                        + Util.number_format(p.data_yesno[0]) + " Ruby.");
                        }
                        
                    }
                    break;
                }
                case 59: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(p.data_yesno[0]);
                        if (itemTemplate4 != null) {
                            ItemBag47 it_select = null;
                            for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                                if (p.daHanhTrinh.get(i).category > -1
                                        && p.map.template.id == HanhTrinh.LANG[p.daHanhTrinh
                                                .get(i).category]
                                        && p.daHanhTrinh.get(i).id == p.data_yesno[0]
                                        && p.daHanhTrinh.get(i).quant == 1) {
                                    it_select = p.daHanhTrinh.get(i);
                                }
                            }
                            if (it_select != null) {
                                if (p.get_ngoc() < 100) {
                                    Service.send_box_ThongBao_OK(p, "Không đủ 100 ruby");
                                    p.data_yesno = null;
                                    p.map_tele = null;
                                    return;
                                }
                                p.update_ngoc(-100);
                                p.update_money();
                                //
                                it_select.quant = 0;
                                HanhTrinh.update_da_kham(p);
                                //
                                Message m = new Message(79);
                                m.writer().writeByte(0);
                                m.writer().writeShort(HanhTrinh.get_map(p));
                                m.writer().writeUTF(p.map.template.name);
                                List<ItemBag47> list_DaHanhTrinh =
                                        p.get_list_daHanhTrinh_total(p.map.template.id);
                                m.writer().writeByte(list_DaHanhTrinh.size());
                                for (int i = 0; i < list_DaHanhTrinh.size(); i++) {
                                    ItemTemplate4 itemTemplate4_ =
                                            ItemTemplate4.get_it_by_id(list_DaHanhTrinh.get(i).id);
                                    m.writer().writeUTF(itemTemplate4_.name);
                                    m.writer().writeByte(4);
                                    m.writer().writeShort(itemTemplate4_.id);
                                    m.writer().writeByte(1);
                                    m.writer().writeShort(itemTemplate4_.icon);
                                    ItemTemplate4_Info temp_info = ItemTemplate4_Info
                                            .get_by_id(itemTemplate4_.indexInfoPotion);
                                    if (temp_info != null) {
                                        m.writer().writeUTF(temp_info.info);
                                    } else {
                                        m.writer().writeUTF("Chưa có thông tin");
                                    }
                                }
                                p.conn.addmsg(m);
                                m.cleanup();
                                //
                                Service.send_box_ThongBao_OK(p,
                                        "Tách thành công " + itemTemplate4.name);
                                p.update_info_to_all();
                            } else {
                                Service.send_box_ThongBao_OK(p, "Không đủ 1 " + itemTemplate4.name);
                            }
                        }
                    }
                    break;
                }
                case 58: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(p.data_yesno[0]);
                        if (itemTemplate4 != null) {
                            ItemBag47 it_select = null;
                            for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                                if (p.daHanhTrinh.get(i).category > -1
                                        && p.map.template.id == HanhTrinh.LANG[p.daHanhTrinh
                                                .get(i).category]
                                        && p.daHanhTrinh.get(i).id == p.data_yesno[0]
                                        && p.daHanhTrinh.get(i).quant == 0) {
                                    it_select = p.daHanhTrinh.get(i);
                                }
                            }
                            if (it_select != null) {
                                it_select.quant = 1;
                                //
                                Message m = new Message(79);
                                m.writer().writeByte(2);
                                m.writer().writeShort(p.data_yesno[0]);
                                p.conn.addmsg(m);
                                m.cleanup();
                                HanhTrinh.update_da_kham(p);
                                //
                                Service.send_box_ThongBao_OK(p,
                                        "Khảm thành công " + itemTemplate4.name);
                                p.update_info_to_all();
                            } else {
                                Service.send_box_ThongBao_OK(p, "Không đủ 1 " + itemTemplate4.name);
                            }
                        }
                    }
                    break;
                }
                case 57: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        Item_wear it_select = p.item.bag3[p.data_yesno[0]];
                        if (it_select != null && it_select.template.typeEquip == 7
                                && it_select.numLoKham < 5) {
                            if (it_select.valueChetac < 50) {
                                Service.send_box_ThongBao_OK(p,
                                        "Vật phẩm không đủ điểm chế tác để thực hiện, tối thiểu 50!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item.total_item_bag_by_id(4, 457) < 1) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 1 búa đục dial");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            int ruby_req = 50 * (it_select.numLoKham + 1);
                            if (p.get_ngoc() < ruby_req) {
                                Service.send_box_ThongBao_OK(p, "Không đủ " + ruby_req + " ruby");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_ngoc(-ruby_req);
                            p.update_money();
                            p.item.remove_item47(4, 457, 1);
                            boolean suc = 40 > Util.random(150 + it_select.numLoKham * 50);
                            if (suc) {
                                if (it_select.numHoleDaDuc < 0) {
                                    it_select.numHoleDaDuc = 0;
                                }
                                it_select.numLoKham++;
                                it_select.numHoleDaDuc++;
                            } else {
                                it_select.valueChetac -= Util.random(10, 20);
                                if (it_select.valueChetac < 0) {
                                    it_select.valueChetac = 0;
                                }
                            }
                            Message m = new Message(-67);
                            m.writer().writeByte(7);
                            m.writer()
                                    .writeUTF(suc ? ("Đục lỗ thành công " + it_select.template.name)
                                            : "Rất tiếc đục lỗ thất bại");
                            p.conn.addmsg(m);
                            m.cleanup();
                            p.item.update_Inventory(-1, false);
                        }
                    }
                    break;
                }
                case 56: {
                    if (p.map.map_ThuThachVeThan == null) {
                        if (p.party == null || p.party.list.size() != 2
                                || !p.party.list.get(0).name.equals(p.name)) {
                            Service.send_box_ThongBao_OK(p, "Hãy tạo nhóm 2 người để vào phó bản");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        for (int i = 0; i < p.party.list.size(); i++) {
                            Player p0 = Map.get_player_by_name_allmap(p.party.list.get(i).name);
                            if (!(p0.map.equals(p.map) && Math.abs(p0.x - p.x) < 100
                                    && Math.abs(p0.y - p.y) < 100) || p0.get_key_boss() < 1) {
                                Service.send_box_ThongBao_OK(p, p0.name
                                        + " không đủ 1 chìa khóa phó bản hoặc không đứng gần");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                        }
                        List<Player> listP = new ArrayList<>();
                        for (int i = 0; i < p.party.list.size(); i++) {
                            Player p0 = Map.get_player_by_name_allmap(p.party.list.get(i).name);
                            if (p0 != null && p0.map.equals(p.map)) {
                                p0.update_key_boss(-1);
                                p0.update_money();
                                Service.CountDown_Ticket(p0);
                                //
                                p0.map.leave_map(p0, 2);
                                listP.add(p0);
                            }
                        }
                        // create map
                        Map mapTemplate = Map.get_map_by_id(984)[0];
                        Map map_boss = new Map();
                        map_boss.template = mapTemplate.template;
                        map_boss.zone_id = (byte) 0;
                        map_boss.list_mob = new int[0];
                        map_boss.map_ThuThachVeThan = new Map_ThuThachVeThan();
                        map_boss.map_ThuThachVeThan.time_state =
                                System.currentTimeMillis() + 10_000L;
                        //
                        listP.forEach(p0 -> {
                            try {
                                p0.map = map_boss;
                                p0.x = 300;
                                p0.y = 200;
                                p0.xold = p0.x;
                                p0.yold = p0.y;
                                p0.map.goto_map(p0);
                                p0.time_key_red_line = 10;
                                Service.update_PK(p0, p0, true);
                                Service.pet(p0, p0, true);
                                Quest.update_map_have_side_quest(p0, true);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        });
                        map_boss.start_map();
                        Map.add_map_plus(map_boss);
                    }
                    break;
                }
                case 55: {
                    Skill_info sk_select = null;
                    for (int i = 0; i < p.skill_point.size(); i++) {
                        if (p.skill_point.get(i).temp.indexSkillInServer >= 661
                                && p.skill_point.get(i).temp.indexSkillInServer <= 666) {
                            sk_select = p.skill_point.get(i);
                            break;
                        }
                    }
                    if (sk_select == null) {
                        if (p.time_ttvt < 50) {
                            if (p.get_ngoc() < 500) {
                                Service.send_box_ThongBao_OK(p, "không đủ 500 ruby để có thể học");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_ngoc(-500);
                            p.update_money();
                        }
                        //
                        sk_select = new Skill_info();
                        sk_select.exp = 0;
                        sk_select.temp = Skill_Template.get_temp(661, 0);
                        sk_select.lvdevil = 0;
                        sk_select.devilpercent = 0;
                        p.skill_point.add(sk_select);
                        p.send_skill();
                        p.update_info_to_all();
                        Service.send_box_ThongBao_OK(p,
                                "Học thành công kỹ năng chế tạo dial cấp 1");
                    } else {
                        Service.send_box_ThongBao_OK(p, "Đã học kỹ năng này rồi");
                    }
                    break;
                }
                case 53: {
                    if (p.name_ThoSanHaiTac != null && p.name_ThoSanHaiTac.length == 1
                            && p.typePirate == 1) {
                        Player p0 = Map.get_player_by_name_allmap(p.name_ThoSanHaiTac[0]);
                        if (p0 != null && p0.map.equals(p.map) && p0.ship_pet != null) {
                            Service.send_box_ThongBao_OK(p0,
                                    p.name + " đồng ý bảo vệ vận hàng, hãy bắt đầu chuyến đi");
                            Service.send_box_ThongBao_OK(p,
                                    "đồng ý bảo vệ vận hàng thành công, hãy bắt đầu chuyến đi");
                            p0.ship_pet.mainBaoVe = p.name;
                        } else {
                            Service.send_box_ThongBao_OK(p, "Đối phương đã rời đi");
                        }
                    }
                    break;
                }
                case 52: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        if (p.party != null) {
                            Service.send_box_ThongBao_OK(p, "Hãy hủy nhóm trước khi vào phó bản");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        byte mode = (byte) p.data_yesno[0];
                        if (mode < 7) {
                            if (p.get_key_boss() < 1) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 1 chìa khóa phó bản");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_key_boss(-1);
                        } else {
                            if (p.get_key_boss() < 2) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 2 chìa khóa phó bản");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_key_boss(-2);
                        }
                        //
                        p.update_money();
                        Service.CountDown_Ticket(p);
                        //
                        p.dungeon = new Dungeon();
                        p.dungeon.mode = mode;
                        p.dungeon.create();
                        Vgo vgo = new Vgo();
                        vgo.map_go = new Map[1];
                        vgo.map_go[0] = p.dungeon.maps.get(0);
                        vgo.xnew = 350;
                        vgo.ynew = 260;
                        p.goto_map(vgo);
                    }
                    break;
                }
                case 51: {
                    if (p.tableTickOption != null && !p.tableTickOption.is_finish) {
                        if (p.tableTickOption.listP.get(0).name.equals(p.name)) {
                            for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                                Player p0 = Map.get_player_by_name_allmap(
                                        p.tableTickOption.listP.get(i).name);
                                if (p0 != null) {
                                    Service.send_box_ThongBao_OK(p0,
                                            "Đăng ký tham gia thành công, đợi ghép với băng khác");
                                }
                            }
                            LittleGarden.add_clan_wait(p.clan);
                            p.tableTickOption.is_finish = true;
                        }
                    }
                    break;
                }
                case 50: {
                    if (p.map.template.id == 1 && p.typePirate == 0 && p.id_ship_packet != -1) {
                        if (p.get_vang() < 10_000) {
                            Service.send_box_ThongBao_OK(p, "Không đủ 10.000 beri");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.time_ship >= 5) {
                            Service.send_box_ThongBao_OK(p, "Hôm nay đã vận chuyến tối đa!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.update_vang(-10_000);
                        p.update_money();
                        //
                        Ship.notice_start_shipping(p);
                        //
                        p.ship_pet = new Ship_pet();
                        p.ship_pet.index_map = -2;
                        if (p.ship_pet.index_map != -1) {
                            p.ship_pet.main_ship = p;
                            p.ship_pet.map = p.map;
                            p.ship_pet.name = "Hàng " + p.name;
                            p.ship_pet.x = 315;
                            p.ship_pet.y = 210;
                            p.ship_pet.hp_max = 2000;
                            p.ship_pet.hp = p.ship_pet.hp_max;
                            p.ship_pet.time_start = System.currentTimeMillis();
                            p.ship_pet.id_map_save = p.map.template.id;
                            Ship_pet.add(p.ship_pet);
                            Message m_local = new Message(1);
                            m_local.writer().writeByte(0);
                            m_local.writer().writeShort(p.ship_pet.index_map);
                            m_local.writer().writeShort(p.ship_pet.x);
                            m_local.writer().writeShort(p.ship_pet.y);
                            for (int j = 0; j < p.map.players.size(); j++) {
                                Player p0 = p.map.players.get(j);
                                p0.conn.addmsg(m_local);
                            }
                            m_local.cleanup();
                        } else {
                            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra hãy thủ lại");
                        }
                    }
                    break;
                }
                case 46: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        Item_wear it_select = p.item.bag3[p.data_yesno[0]];
                        if (it_select != null && it_select.numLoKham >= 4
                                && it_select.numLoKham < 8) {
                            if (it_select.valueChetac < 50) {
                                Rebuild_Item.show_table(p, 2);
                                Service.send_box_ThongBao_OK(p,
                                        "Vật phẩm không đủ điểm chế tác để thực hiện, tối thiểu 50!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item.total_item_bag_by_id(4, 323) < 1) {
                                Rebuild_Item.show_table(p, 2);
                                Service.send_box_ThongBao_OK(p, "Không đủ 1 búa siêu cấp");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.item.remove_item47(4, 323, 1);
                            if (it_select.numHoleDaDuc < 0) {
                                it_select.numHoleDaDuc = 0;
                            }
                            it_select.numLoKham++;
                            it_select.numHoleDaDuc++;
                            Message m = new Message(-67);
                            m.writer().writeByte(7);
                            m.writer().writeUTF("Đục lỗ thành công " + it_select.template.name);
                            p.conn.addmsg(m);
                            m.cleanup();
                            p.item.update_Inventory(-1, false);
                        } else {
                            Rebuild_Item.show_table(p, 2);
                            Service.send_box_ThongBao_OK(p,
                                    "Không thể dùng búa siêu cấp với vật phẩm này");
                        }
                    }
                    break;
                }
                case 45: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        Item_wear it_select = p.item.bag3[p.data_yesno[0]];
                        if (it_select != null && it_select.numLoKham == 4) {
                            if (it_select.valueChetac < 50) {
                                Rebuild_Item.show_table(p, 2);
                                Service.send_box_ThongBao_OK(p,
                                        "Vật phẩm không đủ điểm chế tác để thực hiện, tối thiểu 50!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item.total_item_bag_by_id(4, 339) < 1) {
                                Rebuild_Item.show_table(p, 2);
                                Service.send_box_ThongBao_OK(p, "Không đủ 1 búa sơ cấp");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.item.remove_item47(4, 339, 1);
                            boolean suc = 10 > Util.random(150);
                            if (suc) {
                                if (it_select.numHoleDaDuc < 0) {
                                    it_select.numHoleDaDuc = 0;
                                }
                                it_select.numLoKham++;
                                it_select.numHoleDaDuc++;
                            } else {
                                it_select.valueChetac -= Util.random(10, 20);
                                if (it_select.valueChetac < 0) {
                                    it_select.valueChetac = 0;
                                }
                            }
                            Message m = new Message(-67);
                            m.writer().writeByte(7);
                            m.writer()
                                    .writeUTF(suc ? ("Đục lỗ thành công " + it_select.template.name)
                                            : "Rất tiếc đục lỗ thất bại");
                            p.conn.addmsg(m);
                            m.cleanup();
                            p.item.update_Inventory(-1, false);
                        } else {
                            Rebuild_Item.show_table(p, 2);
                            Service.send_box_ThongBao_OK(p,
                                    "Búa sơ cấp chỉ có thể đục lỗ vật phẩm từ 4 lỗ lên 5 lỗ");
                        }
                    }
                    break;
                }
                case 44: {
                    break;
                }
                case 43: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        Player p0 = null;
                        for (int i = 0; i < p.friend_list.size(); i++) {
                            if (p.friend_list.get(i).id == p.data_yesno[0]) {
                                p0 = Map.get_player_by_name_allmap(p.friend_list.get(i).name);
                                break;
                            }
                        }
                        if (p0 == null) {
                            for (int i = 0; i < p.enemy_list.size(); i++) {
                                if (p.enemy_list.get(i).id == p.data_yesno[0]) {
                                    p0 = Map.get_player_by_name_allmap(p.enemy_list.get(i).name);
                                    break;
                                }
                            }
                        }
                        boolean check = false;
                        if (p0 != null) {
                            for (int i = 0; i < p0.friend_list.size(); i++) {
                                if (p0.friend_list.get(i).name.equals(p.name)) {
                                    check = true;
                                    break;
                                }
                            }
                            if (!check) {
                                for (int i = 0; i < p.enemy_list.size(); i++) {
                                    if (p.enemy_list.get(i).name.equals(p0.name)) {
                                        check = true;
                                        break;
                                    }
                                }
                            }
                            if (check) {
                                if (p.get_ngoc() < 5) {
                                    Service.send_box_ThongBao_OK(p, "Không đủ 5 ngọc để thực hiện");
                                } else {
                                    p.update_ngoc(-5);
                                    p.update_money();
                                    Vgo vgo = new Vgo();
                                    vgo.map_go = new Map[1];
                                    vgo.map_go[0] = p0.map;
                                    vgo.xnew = p0.x;
                                    vgo.ynew = p0.y;
                                    p.goto_map(vgo);
                                }
                            }
                        }
                        if (!check) {
                            Service.send_box_ThongBao_OK(p,
                                    "Đối phương không online hoặc không có trong danh sách");
                        }
                    }
                    break;
                }
                case 42: {
                    if (p.clan != null) {
                        Clan_member clan_mem = null;
                        for (int i = 0; i < p.clan.members.size(); i++) {
                            if (p.clan.members.get(i).name.equals(p.name)) {
                                clan_mem = p.clan.members.get(i);
                                break;
                            }
                        }
                        if (clan_mem != null) {
                            if (clan_mem.numquest >= 3) {
                                Service.send_box_ThongBao_OK(p,
                                        "Hôm nay đã hết nhiệm vụ, hãy quay lại vào ngày mai");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            Clan.send_info(p, false);
                            QuestP questP = null;
                            for (int i = 0; i < p.list_quest.size(); i++) {
                                if (p.list_quest.get(i).template.id < -2000) {
                                    questP = p.list_quest.get(i);
                                    break;
                                }
                            }
                            if (questP == null) {
                                clan_mem.numquest++;
                                questP = new QuestP();
                                questP.template =
                                        Quest.get_quest(-3000 + ((clan_mem.numquest - 1) * 2));
                                questP.data = new short[questP.template.data_quest.length][];
                                for (int i = 0; i < questP.data.length; i++) {
                                    questP.data[i] =
                                            new short[questP.template.data_quest[i].length];
                                    for (int j = 0; j < questP.data[i].length; j++) {
                                        questP.data[i][j] = questP.template.data_quest[i][j];
                                    }
                                }
                                p.list_quest.add(questP);
                                //
                                Message m = new Message(-23);
                                m.writer().writeByte(1);
                                m.writer().writeByte(questP.template.statusQuest);
                                Quest.write_Quest(m.writer(), questP);
                                p.conn.addmsg(m);
                                m.cleanup();
                                m = new Message(-23);
                                m.writer().writeByte(5);
                                m.writer().writeShort(questP.template.index);
                                p.conn.addmsg(m);
                                m.cleanup();
                            } else {
                                Service.send_box_ThongBao_OK(p,
                                        "Nhiệm vụ hiện tại chưa hoàn thành");
                            }
                        }
                    }
                    break;
                }
                case 41: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        short[] id_op = new short[] {27, 16, 26, 4, 15, 1};
                        if (p.pointAttributeThongThao < 1) {
                            Service.send_box_ThongBao_OK(p, "Không đủ 1 điểm thông thạo");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.pointAttributeThongThao--;
                        Option op_add = null;
                        for (int i = 0; i < p.list_op_thongthao.size(); i++) {
                            if (p.list_op_thongthao.get(i).id == id_op[p.data_yesno[0]]) {
                                op_add = p.list_op_thongthao.get(i);
                                break;
                            }
                        }
                        if (op_add != null) {
                            int old_value = op_add.getParam();
                            op_add.setParam(old_value + 1);
                        } else {
                            p.list_op_thongthao.add(new Option(id_op[p.data_yesno[0]], 1));
                        }
                        p.update_info_to_all();
                        Max_Level.show_table(p);
                        // Service.send_box_ThongBao_OK(p, "Tăng thành công");
                    }
                    break;
                }
                case 40: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(p.data_yesno[0]);
                        if (itemTemplate4 != null) {
                            if (itemTemplate4.type == 74) {
                                if (p.item.total_item_bag_by_id(4, itemTemplate4.id) < 18) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Không đủ 18 " + itemTemplate4.name);
                                    p.data_yesno = null;
                                    p.map_tele = null;
                                    return;
                                }
                                if (p.item.able_bag() < 1) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Hành trang phải chừa ít nhất 1 ô trống");
                                    p.data_yesno = null;
                                    p.map_tele = null;
                                    return;
                                }
                                p.item.remove_item47(4, itemTemplate4.id, 18);
                                Item_wear it_add = null;
                                int id_b1 = 0;
                                int id_b2 = 0;
                                int color = 0;
                                int clazz = p.clazz;
                                short[] type_equip = new short[] {0};
                                switch (itemTemplate4.id) {
                                    case 304: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 0;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 305: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 0;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 306: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 0;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                    case 307: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 1;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 308: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 1;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 309: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 1;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                    case 310: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 2;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 311: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 2;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 312: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 2;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                    case 313: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 3;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 314: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 3;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 315: {
                                        id_b1 = 1728;
                                        id_b2 = 1919;
                                        color = 3;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                    case 536: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 0;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 537: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 0;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 538: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 0;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                    case 539: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 1;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 540: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 1;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 541: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 1;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                    case 542: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 2;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 543: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 2;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 544: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 2;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                    case 545: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 3;
                                        type_equip = new short[] {1, 3, 5};
                                        break;
                                    }
                                    case 546: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 3;
                                        type_equip = new short[] {2, 4};
                                        clazz = 0;
                                        break;
                                    }
                                    case 547: {
                                        id_b1 = 1920;
                                        id_b2 = 2111;
                                        color = 3;
                                        type_equip = new short[] {0};
                                        break;
                                    }
                                }
                                List<ItemTemplate3> list_random = new ArrayList<>();
                                for (int i = 0; i < ItemTemplate3.ENTRYS.size(); i++) {
                                    ItemTemplate3 it_temp = ItemTemplate3.ENTRYS.get(i);
                                    if (it_temp.id >= id_b1 && it_temp.id <= id_b2
                                            && it_temp.color == color && it_temp.clazz == clazz) {
                                        for (int j = 0; j < type_equip.length; j++) {
                                            if (type_equip[j] == it_temp.typeEquip) {
                                                list_random.add(it_temp);
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (list_random.size() > 0) {
                                    it_add = new Item_wear();
                                    it_add.setup_template_by_id(
                                            list_random.get(Util.random(list_random.size())));
                                }
                                if (it_add.template != null) {
                                    int numLoKham = (50 > Util.random(120)) ? 0
                                            : ((70 > Util.random(120)) ? 1 : 2);
                                    it_add.numLoKham = (byte) numLoKham;
                                    p.item.add_item_bag3(it_add);
                                }
                                p.item.update_Inventory(-1, false);
                                if (it_add != null) {
                                    Message m = new Message(-67);
                                    m.writer().writeByte(31);
                                    m.writer().writeUTF(
                                            "Bạn ghép thành công được " + it_add.template.name);
                                    m.writer().writeShort(it_add.index);
                                    p.conn.addmsg(m);
                                    m.cleanup();
                                }
                            }
                        }
                    }
                    break;
                }
                case 39:
                case 38: { // no use
                    break;
                }
                case 37: {
                    // so
                    // 88, 318, 90, 34, 91
                    //
                    // trung
                    // 32,33,92,93,219,220,316,317
                    //
                    // cao
                    // 240, 160, 161, 427
                    if (p.item.total_item_bag_by_id(4, 87) > 0) {
                        int index = Util.random(1000);
                        if (index < 50) { // 316
                            String[] name_ = new String[] {"Giáp sáp", "Đao không kích", "Lao sáp"};
                            int[] icon_ = new int[] {79, 77, 78};
                            Service.NewDialog_eat_taq(p, name_, icon_, 316);
                            p.get_skill_taq_new(316);
                        } else if (index < 130) { // 32
                            String[] name_ =
                                    new String[] {"Sức mạnh của lửa", "Hỏa quyền", "Nắm đấm lửa"};
                            int[] icon_ = new int[] {32, 30, 29};
                            Service.NewDialog_eat_taq(p, name_, icon_, 32);
                            p.get_skill_taq_new(32);
                        } else if (index < 210) { // 93
                            String[] name_ = new String[] {"Cát lưu động", "Bão cát sa mạc",
                                    "Cát linh động"};
                            int[] icon_ = new int[] {55, 54, 52};
                            Service.NewDialog_eat_taq(p, name_, icon_, 93);
                            p.get_skill_taq_new(93);
                        } else if (index < 330) { // 317
                            String[] name_ =
                                    new String[] {"Thân thể thép", "Ảo ảnh trảm", "Loạn trảm"};
                            int[] icon_ = new int[] {82, 81, 80};
                            Service.NewDialog_eat_taq(p, name_, icon_, 317);
                            p.get_skill_taq_new(317);
                        } else if (index < 450) { // 92
                            String[] name_ =
                                    new String[] {"Băng vĩnh cửu", "Mưa băng", "Tuyết tê tái"};
                            int[] icon_ = new int[] {57, 56, 53};
                            Service.NewDialog_eat_taq(p, name_, icon_, 92);
                            p.get_skill_taq_new(92);
                        } else if (index < 580) { // 219
                            String[] name_ =
                                    new String[] {"Sóng âm - Xung kích", "Hóa báo đốm", "Tia chớp"};
                            int[] icon_ = new int[] {72, 71, 70};
                            Service.NewDialog_eat_taq(p, name_, icon_, 219);
                            p.get_skill_taq_new(219);
                        } else if (index < 710) { // 220
                            String[] name_ = new String[] {"Cơn lốc - Ưng kích", "Hóa chim ưng",
                                    "Chim săn mồi"};
                            int[] icon_ = new int[] {69, 68, 67};
                            Service.NewDialog_eat_taq(p, name_, icon_, 220);
                            p.get_skill_taq_new(220);
                        } else { // 33
                            String[] name_ = new String[] {"Sức sống bất diệt", "Chất bất ổn",
                                    "Súng máy caosu"};
                            int[] icon_ = new int[] {34, 33, 31};
                            Service.NewDialog_eat_taq(p, name_, icon_, 33);
                            p.get_skill_taq_new(33);
                        }
                        p.item.remove_item47(4, 87, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 36: {
                    if (p.get_ngoc() < 15) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 15 ruby");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 232) >= DataTemplate.MAX_ITEM_IN_BAG) {
                        Service.send_box_ThongBao_OK(p,
                                "Số lượng vé đã đạt tối đa trong hành trang");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    p.update_ngoc(-15);
                    p.update_money();
                    p.item.add_item_bag47(4, 232, 1);
                    p.item.update_Inventory(-1, false);
                    Service.send_box_ThongBao_OK(p, "Mua 1 Vé vòng xoay may mắn thành công");
                    break;
                }
                case 35: {
                    if (p.item.total_item_bag_by_id(4, 86) > 0) {
                        int index = Util.random(1000);
                        if (index < 50) { // 88
                            String[] name_ =
                                    new String[] {"Khói bất tử", "Khói tốc độ", "Mưa khói"};
                            int[] icon_ = new int[] {38, 39, 40};
                            Service.NewDialog_eat_taq(p, name_, icon_, 88);
                            p.get_skill_taq_new(88);
                        } else if (index < 150) { // 318
                            String[] name_ =
                                    new String[] {"Thần hộ thể", "Tăng trọng", "Sức nặng ngàn cân"};
                            int[] icon_ = new int[] {85, 84, 83};
                            Service.NewDialog_eat_taq(p, name_, icon_, 318);
                            p.get_skill_taq_new(318);
                        } else if (index < 350) { // 90
                            String[] name_ =
                                    new String[] {"Bản năng thủ lĩnh", "Hóa bò tót", "Bất khuất"};
                            int[] icon_ = new int[] {48, 47, 46};
                            Service.NewDialog_eat_taq(p, name_, icon_, 90);
                            p.get_skill_taq_new(90);
                        } else if (index < 650) { // 34
                            String[] name_ =
                                    new String[] {"Tiến hóa", "Thuốc tăng trưởng", "Hóa tuần lộc"};
                            int[] icon_ = new int[] {37, 36, 35};
                            Service.NewDialog_eat_taq(p, name_, icon_, 34);
                            p.get_skill_taq_new(34);
                        } else { // 91
                            String[] name_ = new String[] {"Nét vẽ cường hóa", "Nét vẽ phòng thủ",
                                    "Nét vẽ sức mạnh"};
                            int[] icon_ = new int[] {51, 50, 49};
                            Service.NewDialog_eat_taq(p, name_, icon_, 91);
                            p.get_skill_taq_new(91);
                        }
                        p.item.remove_item47(4, 86, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 34: {
                    if (p.item.total_item_bag_by_id(7, 9) < 10) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không có đủ " + ItemTemplate7.get_it_by_id(9).name);
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 29) < 1) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không có đủ " + ItemTemplate4.get_it_by_id(29).name);
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.get_vang() < 50_000) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 50.000 beri");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.able_bag() < 1) {
                        Service.send_box_ThongBao_OK(p,
                                "Hãy chừa ít nhất 1 ô trống trong hành trang");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 158) >= DataTemplate.MAX_ITEM_IN_BAG) {
                        Service.send_box_ThongBao_OK(p,
                                "Số lượng rương Đại ác quỷ đã đạt tối đa trong hành trang");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    p.update_vang(-50_000);
                    p.update_money();
                    p.item.remove_item47(7, 9, 10);
                    //
                    boolean suc = 5 > Util.random(150);
                    if (suc) {
                        p.item.remove_item47(4, 29, 1);
                        p.item.add_item_bag47(4, 158, 1);
                    }
                    p.item.update_Inventory(-1, false);
                    //
                    Message m = new Message(45);
                    m.writer().writeByte(17);
                    m.writer().writeByte(suc ? 1 : 3);
                    m.writer().writeUTF(
                            "Nâng cấp lên Rương đại ác quỷ " + (suc ? "thành công" : "thất bại"));
                    p.conn.addmsg(m);
                    m.cleanup();
                    break;
                }
                case 33: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        Skill_info sk_temp = p.get_skill_temp(p.data_yesno[0]);
                        if (sk_temp != null) {
                            if (sk_temp.lvdevil > 4) {
                                Service.send_box_ThongBao_OK(p,
                                        sk_temp.temp.name + " đã được cường hóa tối đa!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            int percent = (sk_temp.lvdevil == 0) ? 10 //
                                    : ((sk_temp.lvdevil == 1) ? 8 //
                                            : ((sk_temp.lvdevil == 2) ? 6 //
                                                    : ((sk_temp.lvdevil == 3) ? 5 : 4)));
                            if (p.get_vang() < 50_000) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 50.000 beri");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_vang(-50_000);
                            p.update_money();
                            p.item.remove_item47(7, 9, 10);
                            p.item.update_Inventory(-1, false);
                            //
                            boolean suc = 50 > Util.random(120);
                            if (suc) {
                                sk_temp.devilpercent += percent;
                                if (sk_temp.devilpercent >= 100) {
                                    sk_temp.devilpercent = 0;
                                    sk_temp.lvdevil++;
                                }
                                p.send_skill();
                                p.update_info_to_all();
                            }
                            //
                            Message m = new Message(45);
                            m.writer().writeByte(12);
                            m.writer().writeByte(suc ? 1 : 3);
                            m.writer().writeUTF(
                                    "Cường hóa kỹ năng " + (suc ? "thành công" : "thất bại"));
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                    }
                    break;
                }
                case 32: {
                    if (p.item_to_kham_ngoc != null && p.item_to_kham_ngoc_id_ngoc >= 221
                            && p.item_to_kham_ngoc_id_ngoc <= 226) {
                        Item_wear it_select = p.item_to_kham_ngoc;
                        if ((it_select.template.color >= 2) && it_select.valueChetac >= 50) {
                            if (p.item.total_item_bag_by_id(4, p.item_to_kham_ngoc_id_ngoc) < 1) {
                                Service.send_box_ThongBao_OK(p, "Không đủ "
                                        + ItemTemplate4.get_item_name(p.item_to_kham_ngoc_id_ngoc));
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.get_ngoc() < 5) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 5 ruby");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.item.remove_item47(4, p.item_to_kham_ngoc_id_ngoc, 1);
                            p.update_ngoc(-5);
                            p.update_money();
                            //
                            boolean suc = ((p.item_to_kham_ngoc_id_ngoc == 226) ? 25 : 0) > Util
                                    .random(120);
                            if (suc) {
                                it_select.valueKichAn = (byte) Util.random(13);
                                if (it_select.valueKichAn == 12) {
                                    it_select.typelock = -1;
                                }
                            } else {
                                int reduce_chetac;
                                switch (p.item_to_kham_ngoc_id_ngoc) {
                                    case 226: {
                                        reduce_chetac = Util.random(1, 6);
                                        break;
                                    }
                                    case 225: {
                                        reduce_chetac = Util.random(14, 23);
                                        break;
                                    }
                                    case 224: {
                                        reduce_chetac = Util.random(16, 25);
                                        break;
                                    }
                                    case 223: {
                                        reduce_chetac = Util.random(18, 27);
                                        break;
                                    }
                                    case 222: {
                                        reduce_chetac = Util.random(20, 29);
                                        break;
                                    }
                                    default: {
                                        reduce_chetac = Util.random(22, 31);
                                        break;
                                    }
                                }
                                it_select.valueChetac -= reduce_chetac;
                                if (it_select.valueChetac <= 0) {
                                    it_select.valueChetac = 0;
                                }
                            }
                            //
                            Message m = new Message(-67);
                            m.writer().writeByte(20);
                            m.writer()
                                    .writeUTF(suc
                                            ? ("Chúc mừng bạn đã kích ẩn thành công "
                                                    + it_select.template.name)
                                            : "Rất tiếc quá trình kích ẩn thất bại");
                            p.conn.addmsg(m);
                            m.cleanup();
                            p.item.update_Inventory(-1, false);
                        } else {
                            Service.send_box_ThongBao_OK(p,
                                    "Trang bị đem đi kích ẩn phải là trang bị tím hoặc cam và có điểm chế tác > 50");
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra");
                    }
                    break;
                }
                case 31: {
                    if (p.item_to_kham_ngoc != null && (p.item_to_kham_ngoc.template.typeEquip < 6
                            || p.item_to_kham_ngoc.template.typeEquip == 7)) {
                        if (p.get_ngoc() < 5) {
                            Service.send_box_ThongBao_OK(p, "Không đủ 5 ruby");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.update_ngoc(-5);
                        p.update_money();
                        p.item_to_kham_ngoc.valueChetac++;
                        if (p.item_to_kham_ngoc.valueChetac >= 100) {
                            p.item_to_kham_ngoc.valueChetac = 100;
                        }
                        //
                        Message m = new Message(-67);
                        m.writer().writeByte(25);
                        m.writer().writeUTF("Bạn phục hồi 1 điểm chế tác thành công");
                        p.conn.addmsg(m);
                        m.cleanup();
                        //
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 30: {
                    if (p.item_to_kham_ngoc != null && p.item_to_kham_ngoc_id_ngoc >= 221
                            && p.item_to_kham_ngoc_id_ngoc <= 226) {
                        Item_wear it_select = p.item_to_kham_ngoc;
                        if (it_select.isHoanMy == 1) {
                            Service.send_box_ThongBao_OK(p, "Trang bị này đã được hoàn mỹ rồi!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if ((it_select.template.color >= 2) && it_select.valueChetac >= 50) {
                            if (p.item.total_item_bag_by_id(4, p.item_to_kham_ngoc_id_ngoc) < 1) {
                                Service.send_box_ThongBao_OK(p, "Không đủ "
                                        + ItemTemplate4.get_item_name(p.item_to_kham_ngoc_id_ngoc));
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.get_ngoc() < 5) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 5 ruby");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.item.remove_item47(4, p.item_to_kham_ngoc_id_ngoc, 1);
                            p.update_ngoc(-5);
                            p.update_money();
                            //
                            boolean suc = ((p.item_to_kham_ngoc_id_ngoc == 226) ? 25 : 0) > Util
                                    .random(150);
                            if (suc) {
                                it_select.isHoanMy = 1;
                            } else {
                                int reduce_chetac;
                                switch (p.item_to_kham_ngoc_id_ngoc) {
                                    case 226: {
                                        reduce_chetac = Util.random(1, 6);
                                        break;
                                    }
                                    case 225: {
                                        reduce_chetac = Util.random(14, 23);
                                        break;
                                    }
                                    case 224: {
                                        reduce_chetac = Util.random(16, 25);
                                        break;
                                    }
                                    case 223: {
                                        reduce_chetac = Util.random(18, 27);
                                        break;
                                    }
                                    case 222: {
                                        reduce_chetac = Util.random(20, 29);
                                        break;
                                    }
                                    default: {
                                        reduce_chetac = Util.random(22, 31);
                                        break;
                                    }
                                }
                                it_select.valueChetac -= reduce_chetac;
                                if (it_select.valueChetac <= 0) {
                                    it_select.valueChetac = 0;
                                }
                            }
                            //
                            Message m = new Message(-67);
                            m.writer().writeByte(20);
                            m.writer()
                                    .writeUTF(suc
                                            ? ("Chúc mừng bạn đã hoàn mỹ thành công "
                                                    + it_select.template.name)
                                            : "Rất tiếc quá trình hoàn mỹ thất bại");
                            p.conn.addmsg(m);
                            m.cleanup();
                            p.item.update_Inventory(-1, false);
                        } else {
                            Service.send_box_ThongBao_OK(p,
                                    "Trang bị đem đi hoàn mỹ phải là trang bị tím hoặc cam và có điểm chế tác > 50");
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra");
                    }
                    break;
                }
                case 28: {
                    if (p.item.it_heart != null && p.item.it_heart.levelup < 99) {
                        long vang_req = 1_200_000 + p.item.it_heart.levelup * 200_000L;
                        if (p.get_vang() < vang_req) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không đủ " + Util.number_format(vang_req) + " beri");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        int ruby_req = 0;
                        if ((p.item.it_heart.levelup % 5) == 4) {
                            ruby_req = ((p.item.it_heart.levelup / 5) + 1) * 10;
                        }
                        if (p.get_ngoc() < ruby_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + ruby_req + " ruby");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.update_vang(-vang_req);
                        p.update_ngoc(-ruby_req);
                        p.update_money();
                        boolean suc = (530 - (p.item.it_heart.levelup * 5)) > Util.random(1200);
                        if (suc || p.item.it_heart.valueChetac == 100) {
                            p.item.it_heart.levelup++;
                            p.item.it_heart.valueChetac = 0;
                        } else {
                            p.item.it_heart.valueChetac += 10;
                        }
                        UpgradeItem.send_heart_info(p, false);
                        p.update_info_to_all();
                        //
                        Message m5 = new Message(-48);
                        m5.writer().writeByte(suc ? 16 : 17); // 16 ok, 17 fail
                        m5.writer().writeUTF(
                                "Bạn đã phẫu thuật quả tim " + (suc ? "thành công" : "thất bại"));
                        p.conn.addmsg(m5);
                        m5.cleanup();
                    }
                    break;
                }
                case 27: {
                    if (p.item.it_heart == null) {
                        if (p.item.total_item_bag_by_id(4, 221) < ((p.level < 40) ? 120 : 100)) {
                            Service.send_box_ThongBao_OK(p, "Không đủ "
                                    + ((p.level < 40) ? 120 : 100) + " Đá Hải Thạch cấp 1");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        short[] id_check = new short[] {4, 9};
                        for (int i = 0; i < id_check.length; i++) {
                            if (p.item.total_item_bag_by_id(7,
                                    id_check[i]) < ((p.level < 40) ? 120 : 100)) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ " + ((p.level < 40) ? 120 : 100) + " "
                                                + ItemTemplate7.get_item_name(id_check[i]));
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                        }
                        if (p.get_vang() < ((p.level < 40) ? 12_000_000 : 10_000_000)) {
                            Service.send_box_ThongBao_OK(p, "Không đủ 10.000.000 beri");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.item.remove_item47(4, 221, ((p.level < 40) ? 120 : 100));
                        for (int i = 0; i < id_check.length; i++) {
                            p.item.remove_item47(7, id_check[i], ((p.level < 40) ? 120 : 100));
                        }
                        p.update_vang(-((p.level < 40) ? 12_000_000 : 10_000_000));
                        p.update_money();
                        p.item.update_Inventory(-1, false);
                        //
                        p.item.it_heart = new Item_wear();
                        p.item.it_heart.setup_template_by_id(11_000);
                        p.item.it_heart.valueChetac = 0;
                        p.item.it_heart.typelock = 1;
                        // p.item.it_heart.option_item.add(new Option(56, 100));
                        //
                        if (p.item.it_body[7] == null) {
                            p.item.it_heart.index = 7;
                        } else {
                            p.item.it_heart.index = 6;
                        }
                        p.update_info_to_all();
                        UpgradeItem.send_heart_info(p, false);
                        UpgradeItem.show_eff_get_heart(p);
                    }
                    break;
                }
                case 26: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        PotionMarket it_receive = null;
                        Market market = Market.get_list_by_type(p.data_yesno[0]);
                        if (market != null) {
                            for (int j = 0; j < market.item47.size(); j++) {
                                if (market.item47.get(j).index == p.data_yesno[1]) {
                                    it_receive = market.item47.get(j);
                                    break;
                                }
                            }
                        }
                        if (it_receive != null
                                && it_receive.time_market > System.currentTimeMillis()) {
                            if (p.get_vnd() < it_receive.price_market) {
                                if (it_receive.category == 4) {
                                    if (it_receive.id == 0) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Bạn không đủ "
                                                        + Util.number_format(
                                                                it_receive.price_market)
                                                        + " extol để mua " + it_receive.quant
                                                        + " triệu beri");
                                    } else {
                                        Service.send_box_ThongBao_OK(p, "Bạn không đủ "
                                                + Util.number_format(it_receive.price_market)
                                                + " extol để mua " + it_receive.quant + " "
                                                + ItemTemplate4.get_item_name(it_receive.id));
                                    }
                                } else {
                                    Service.send_box_ThongBao_OK(p,
                                            "Bạn không đủ "
                                                    + Util.number_format(it_receive.price_market)
                                                    + " extol để mua " + it_receive.quant + " "
                                                    + ItemTemplate7.get_item_name(it_receive.id));
                                }
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item.able_bag() > 0 && (p.item
                                    .total_item_bag_by_id(it_receive.category, it_receive.id)
                                    + it_receive.quant) <= DataTemplate.MAX_ITEM_IN_BAG) {
                                it_receive.time_market = 0;
                                it_receive.type_market = 2;
                                p.update_vnd(-it_receive.price_market);
                                if (!(it_receive.category == 4 && it_receive.id == 0)) {
                                    p.update_money();
                                }
                                if (it_receive.category == 4) {
                                    if (it_receive.id == 0) { // beri
                                        long beri_add = 1_000_000L * it_receive.quant;
                                        p.update_vang(beri_add);
                                        p.update_money();
                                        Service.send_box_ThongBao_OK(p,
                                                "Mua thành công " + it_receive.quant + " "
                                                        + " triệu beri với giá "
                                                        + Util.number_format(
                                                                it_receive.price_market)
                                                        + " extol");
                                    } else {
                                        p.item.add_item_bag47(it_receive.category, it_receive.id,
                                                it_receive.quant);
                                        Service.send_box_ThongBao_OK(p,
                                                "Mua thành công " + it_receive.quant + " "
                                                        + ItemTemplate4.get_item_name(it_receive.id)
                                                        + " với giá "
                                                        + Util.number_format(
                                                                it_receive.price_market)
                                                        + " extol");
                                    }
                                } else {
                                    p.item.add_item_bag47(it_receive.category, it_receive.id,
                                            it_receive.quant);
                                    Service.send_box_ThongBao_OK(p,
                                            "Mua thành công " + it_receive.quant + " "
                                                    + ItemTemplate7.get_item_name(it_receive.id)
                                                    + " với giá "
                                                    + Util.number_format(it_receive.price_market)
                                                    + " extol");
                                }
                                p.item.update_Inventory(-1, false);
                                Market.update_at_market_index(p, market.type);
                                Market.update_at_market_index(p, 3);
                            } else {
                                Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ");
                            }
                        }
                    }
                    break;
                }
                case 25: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        ItemMarket it_receive = null;
                        Market market = Market.get_list_by_type(p.data_yesno[0]);
                        if (market != null) {
                            for (int j = 0; j < market.item3.size(); j++) {
                                if (market.item3.get(j).index == p.data_yesno[1]) {
                                    it_receive = market.item3.get(j);
                                    break;
                                }
                            }
                        }
                        if (it_receive != null
                                && it_receive.time_market > System.currentTimeMillis()) {
                            if (p.item.able_bag() > 0) {
                                if (p.get_vnd() < it_receive.price_market) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Bạn không đủ "
                                                    + Util.number_format(it_receive.price_market)
                                                    + " extol để mua " + it_receive.template.name);
                                    p.data_yesno = null;
                                    p.map_tele = null;
                                    return;
                                }
                                Item_wear it_add = new Item_wear();
                                it_add.clone_obj(it_receive);
                                if (it_add.template != null) {
                                    it_receive.time_market = 0;
                                    it_receive.type_market = 2;
                                    p.update_vnd(-it_receive.price_market);
                                    p.update_money();
                                    //
                                    p.item.add_item_bag3(it_add);
                                    p.item.update_Inventory(-1, false);
                                }
                                Service.send_box_ThongBao_OK(p,
                                        "Mua thành công " + it_receive.template.name + " với giá "
                                                + Util.number_format(it_receive.price_market)
                                                + " extol");
                                Market.update_at_market_index(p, market.type);
                                Market.update_at_market_index(p, 3);
                            } else {
                                Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ");
                            }
                        }
                    }
                    break;
                }
                case 24: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        PotionMarket it_receive = null;
                        Market market = Market.get_list_by_type(p.data_yesno[0]);
                        if (market != null) {
                            for (int j = 0; j < market.item47.size(); j++) {
                                if (market.item47.get(j).index == p.data_yesno[1]) {
                                    it_receive = market.item47.get(j);
                                    break;
                                }
                            }
                        }
                        if (it_receive != null
                                && it_receive.time_market > System.currentTimeMillis()) {
                            it_receive.time_market = 0;
                            it_receive.type_market = 3;
                            Market.update_at_market_index(p, market.type);
                            Market.update_at_market_index(p, 3);
                            if (it_receive.category == 4) {
                                if (it_receive.id == 0) {
                                    Service.send_box_ThongBao_OK(p, "Hủy bán " + it_receive.quant
                                            + " triệu beri thành công");
                                } else {
                                    Service.send_box_ThongBao_OK(p,
                                            "Hủy bán " + it_receive.quant + " "
                                                    + ItemTemplate4.get_item_name(it_receive.id)
                                                    + " thành công");
                                }
                            } else {
                                Service.send_box_ThongBao_OK(p,
                                        "Hủy bán " + it_receive.quant + " "
                                                + ItemTemplate7.get_item_name(it_receive.id)
                                                + " thành công");
                            }
                        }
                    }
                    break;
                }
                case 23: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        ItemMarket it_receive = null;
                        Market market = Market.get_list_by_type(p.data_yesno[0]);
                        if (market != null) {
                            for (int j = 0; j < market.item3.size(); j++) {
                                if (market.item3.get(j).index == p.data_yesno[1]) {
                                    it_receive = market.item3.get(j);
                                    break;
                                }
                            }
                        }
                        if (it_receive != null
                                && it_receive.time_market < System.currentTimeMillis()) {
                            if (p.get_vnd() < 1_500) {
                                Service.send_box_ThongBao_OK(p,
                                        "Bạn không đủ 1.500 extol để đăng bán vật phẩm");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_vnd(-1_500);
                            p.update_money();
                            it_receive.time_market = System.currentTimeMillis() + 60_000L * 60 * 24;
                            it_receive.type_market = 1;
                            Service.send_box_ThongBao_OK(p, "Gia hạn thêm 24h cho "
                                    + it_receive.template.name + " thành công");
                            Market.update_at_market_index(p, market.type);
                            Market.update_at_market_index(p, 3);
                        }
                    }
                    break;
                }
                case 22: {
                    if (p.data_yesno != null && p.data_yesno.length == 4) {
                        if (p.get_vnd() < 2_000) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn không đủ 2000 extol để đăng bán vật phẩm");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        Market getMarket = null;
                        if (p.data_yesno[0] == 4) {
                            if (p.item.total_item_bag_by_id(p.data_yesno[0],
                                    p.data_yesno[1]) < p.data_yesno[2]) {
                                Service.send_box_ThongBao_OK(p,
                                        "Bạn không đủ " + p.data_yesno[2] + " "
                                                + ItemTemplate4.get_item_name(p.data_yesno[1])
                                                + " để đăng bán");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (Market.check_it_47_cant_sell(4, p.data_yesno[1])) {
                                Service.send_box_ThongBao_OK(p, "Không thể đăng bán vật phẩm này");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            getMarket = Market.get_list_by_type(6);
                        } else if (p.data_yesno[0] == 7) {
                            if (p.item.total_item_bag_by_id(p.data_yesno[0],
                                    p.data_yesno[1]) < p.data_yesno[2]) {
                                Service.send_box_ThongBao_OK(p,
                                        "Bạn không đủ " + p.data_yesno[2] + " "
                                                + ItemTemplate7.get_item_name(p.data_yesno[1])
                                                + " để đăng bán");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (Market.check_it_47_cant_sell(7, p.data_yesno[1])) {
                                Service.send_box_ThongBao_OK(p, "Không thể đăng bán vật phẩm này");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            getMarket = Market.get_list_by_type(5);
                        }
                        if (getMarket != null) {
                            PotionMarket it_add = new PotionMarket();
                            it_add.index = Market.get_index();
                            it_add.id = (short) p.data_yesno[1];
                            it_add.category = (byte) p.data_yesno[0];
                            it_add.quant = (short) p.data_yesno[2];
                            it_add.time_market = System.currentTimeMillis() + 60_000L * 60 * 24;
                            it_add.price_market = p.data_yesno[3];
                            it_add.seller = p.name;
                            it_add.type_market = 1;
                            if (it_add.index != -1) {
                                p.update_vnd(-2_000);
                                p.update_money();
                                p.item.remove_item47(p.data_yesno[0], p.data_yesno[1],
                                        p.data_yesno[2]);
                                p.item.update_Inventory(-1, false);
                                //
                                getMarket.item47.add(it_add);
                                Market.update_at_market_index(p, getMarket.type);
                                Market.update_at_market_index(p, 3);
                                if (p.data_yesno[0] == 4) {
                                    Service.send_box_ThongBao_OK(p,
                                            p.data_yesno[2] + " "
                                                    + ItemTemplate4.get_item_name(p.data_yesno[1])
                                                    + " đã được đăng bán với giá "
                                                    + Util.number_format(p.data_yesno[3])
                                                    + " thành công lên chợ");
                                } else if (p.data_yesno[0] == 7) {
                                    Service.send_box_ThongBao_OK(p,
                                            p.data_yesno[2] + " "
                                                    + ItemTemplate7.get_item_name(p.data_yesno[1])
                                                    + " đã được đăng bán với giá "
                                                    + Util.number_format(p.data_yesno[3])
                                                    + " thành công lên chợ");
                                }
                            } else {
                                Service.send_box_ThongBao_OK(p,
                                        "Chợ mua bán có quá nhiều vật phẩm, không thể đăng thêm");
                            }
                        } else {
                            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra hãy thử lại");
                        }
                    }
                    break;
                }
                case 21: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        PotionMarket it_receive = null;
                        Market market = Market.get_list_by_type(p.data_yesno[0]);
                        if (market != null) {
                            for (int j = 0; j < market.item47.size(); j++) {
                                if (market.item47.get(j).index == p.data_yesno[1]) {
                                    it_receive = market.item47.get(j);
                                    break;
                                }
                            }
                        }
                        if (it_receive != null
                                && it_receive.time_market < System.currentTimeMillis()) {
                            if (it_receive.type_market == 2) {
                                int price_receive =
                                        (int) (((long) it_receive.price_market * 90L) / 100L);
                                p.update_vnd(price_receive);
                                p.update_money();
                                if (it_receive.category == 4) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Nhận " + price_receive + " extol (phí 10%) tiền bán "
                                                    + it_receive.quant + " "
                                                    + ItemTemplate4.get_item_name(it_receive.id));
                                } else if (it_receive.category == 7) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Nhận " + price_receive + " extol (phí 10%) tiền bán "
                                                    + it_receive.quant + " "
                                                    + ItemTemplate7.get_item_name(it_receive.id));
                                }
                                market.item47.remove(it_receive);
                            } else {
                                if (it_receive.category == 4) {
                                    if (it_receive.id == 0) {
                                        long beri_add = 1_000_000L * it_receive.quant;
                                        p.update_vang(beri_add);
                                        p.update_money();
                                        Service.send_box_ThongBao_OK(p, "Nhận " + it_receive.quant
                                                + " " + " triệu beri về hành trang");
                                    } else {
                                        if (p.item.add_item_bag47(4, it_receive.id,
                                                it_receive.quant)) {
                                            p.item.update_Inventory(-1, false);
                                            Service.send_box_ThongBao_OK(p,
                                                    "Nhận " + it_receive.quant + " "
                                                            + ItemTemplate4
                                                                    .get_item_name(it_receive.id)
                                                            + " về hành trang");
                                        } else {
                                            Service.send_box_ThongBao_OK(p,
                                                    "Hành trang không đủ chỗ trống");
                                            p.data_yesno = null;
                                            p.map_tele = null;
                                            return;
                                        }
                                    }
                                } else if (it_receive.category == 7) {
                                    if (p.item.add_item_bag47(7, it_receive.id, it_receive.quant)) {
                                        p.item.update_Inventory(-1, false);
                                        Service.send_box_ThongBao_OK(p,
                                                "Nhận " + it_receive.quant + " "
                                                        + ItemTemplate7.get_item_name(it_receive.id)
                                                        + " về hành trang");
                                    } else {
                                        Service.send_box_ThongBao_OK(p,
                                                "Hành trang không đủ chỗ trống");
                                        p.data_yesno = null;
                                        p.map_tele = null;
                                        return;
                                    }
                                }
                                market.item47.remove(it_receive);
                            }
                            Market.update_at_market_index(p, market.type);
                            Market.update_at_market_index(p, 3);
                        }
                    }
                    break;
                }
                case 20: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        ItemMarket it_receive = null;
                        Market market = Market.get_list_by_type(p.data_yesno[0]);
                        if (market != null) {
                            for (int j = 0; j < market.item3.size(); j++) {
                                if (market.item3.get(j).index == p.data_yesno[1]) {
                                    it_receive = market.item3.get(j);
                                    break;
                                }
                            }
                        }
                        if (it_receive != null
                                && it_receive.time_market > System.currentTimeMillis()) {
                            it_receive.time_market = 0;
                            it_receive.type_market = 3;
                            Market.update_at_market_index(p, market.type);
                            Market.update_at_market_index(p, 3);
                            Service.send_box_ThongBao_OK(p,
                                    "Hủy bán " + it_receive.template.name + " thành công");
                        }
                    }
                    break;
                }
                case 19: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        ItemMarket it_receive = null;
                        Market market = Market.get_list_by_type(p.data_yesno[0]);
                        if (market != null) {
                            for (int j = 0; j < market.item3.size(); j++) {
                                if (market.item3.get(j).index == p.data_yesno[1]) {
                                    it_receive = market.item3.get(j);
                                    break;
                                }
                            }
                        }
                        if (it_receive != null
                                && it_receive.time_market < System.currentTimeMillis()) {
                            market.item3.remove(it_receive);
                            if (it_receive.type_market == 2) {
                                int price_receive =
                                        (int) (((long) it_receive.price_market * 90L) / 100L);
                                p.update_vnd(price_receive);
                                p.update_money();
                                Service.send_box_ThongBao_OK(p, "Nhận " + price_receive
                                        + " extol (phí 10%) tiền bán " + it_receive.template.name);
                            } else {
                                Item_wear it_add = new Item_wear();
                                it_add.clone_obj(it_receive);
                                if (it_add.template != null) {
                                    p.item.add_item_bag3(it_add);
                                    p.item.update_Inventory(-1, false);
                                }
                                Service.send_box_ThongBao_OK(p,
                                        "Nhận " + it_receive.template.name + " về hành trang");
                            }
                            Market.update_at_market_index(p, 3);
                        }
                    }
                    break;
                }
                case 18: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        if (p.get_vnd() < 2_000) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn không đủ 2000 extol để đăng bán vật phẩm");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        long beri_add = 1_000_000L * p.data_yesno[0];
                        if (beri_add > 2_000_000_000 || beri_add < 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Số beri phải lớn hơn 0 và nhỏ hơn 2tỷ!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.get_vang() < beri_add) {
                            Service.send_box_ThongBao_OK(p, "Bạn không đủ "
                                    + Util.number_format(beri_add) + " beri để đăng bán");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        Market getMarket = Market.get_list_by_type(6);
                        if (getMarket != null) {
                            PotionMarket it_add = new PotionMarket();
                            it_add.index = Market.get_index();
                            it_add.id = 0;
                            it_add.category = 4;
                            it_add.quant = (short) p.data_yesno[0];
                            it_add.time_market = System.currentTimeMillis() + 60_000L * 60 * 24;
                            it_add.price_market = p.data_yesno[1];
                            it_add.seller = p.name;
                            it_add.type_market = 1;
                            if (it_add.index != -1) {
                                p.update_vnd(-2000);
                                p.update_vang(-beri_add);
                                p.update_money();
                                getMarket.item47.add(it_add);
                                Market.update_at_market_index(p, 3);
                                Market.update_at_market_index(p, 6);
                                Service.send_box_ThongBao_OK(p,
                                        p.data_yesno[0] + " triệu beri đã được đăng bán với giá "
                                                + Util.number_format(p.data_yesno[1])
                                                + " thành công lên chợ");
                            } else {
                                Service.send_box_ThongBao_OK(p,
                                        "Chợ mua bán có quá nhiều vật phẩm, không thể đăng thêm");
                            }
                        } else {
                            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra hãy thử lại");
                        }
                    }
                    break;
                }
                case 17: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        Item_wear it_select = p.item.bag3[p.data_yesno[0]];
                        if (it_select != null) {
                            if (p.get_vnd() < 2_000) {
                                Service.send_box_ThongBao_OK(p,
                                        "Bạn không đủ 2000 extol để đăng bán vật phẩm");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            int type_market =
                                    (it_select.template.typeEquip == 0
                                            || it_select.template.typeEquip == 1
                                            || it_select.template.typeEquip == 7)
                                                    ? 0
                                                    : ((it_select.template.typeEquip == 3
                                                            || it_select.template.typeEquip == 5)
                                                                    ? 1
                                                                    : 2);
                            Market getMarket = Market.get_list_by_type(type_market);
                            if (getMarket != null) {
                                ItemMarket it_add = new ItemMarket();
                                it_add.clone_from_item_wear(it_select);
                                it_add.time_market = System.currentTimeMillis() + 60_000L * 60 * 24;
                                it_add.price_market = p.data_yesno[1];
                                it_add.seller = p.name;
                                if (it_add.index != -1) {
                                    p.update_vnd(-2000);
                                    p.update_money();
                                    //
                                    getMarket.item3.add(it_add);
                                    p.item.remove_item_wear(it_select);
                                    p.item.update_Inventory(-1, false);
                                    Market.update_at_market_index(p, 3);
                                    Market.update_at_market_index(p, type_market);
                                    Service.send_box_ThongBao_OK(p,
                                            it_select.template.name + " đã được đăng bán với giá "
                                                    + Util.number_format(p.data_yesno[1])
                                                    + " thành công lên chợ");
                                } else {
                                    Service.send_box_ThongBao_OK(p,
                                            "Chợ mua bán có quá nhiều vật phẩm, không thể đăng thêm");
                                }
                            } else {
                                Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra hãy thử lại");
                            }
                        }
                    }
                    break;
                }
                case 16: {
                    if (p.map_boss_info != null) {
                        if (p.get_ticket() < 5) {
                            p.map_boss_info = null;
                            Service.send_box_ThongBao_OK(p, "Không đủ 5 bánh mì");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.update_ticket(-5);
                        p.update_money();
                        p.map.leave_map(p, 2);
                        // create map boss
                        Map map_boss = new Map();
                        map_boss.template = p.map_boss_info.map.template;
                        map_boss.zone_id = (byte) 0;
                        map_boss.list_mob = new int[0];
                        p.map_boss_info.mob = new ArrayList<>();
                        for (int i = 0; i < p.map_boss_info.map.list_mob.length; i++) {
                            Mob temp = Mob.ENTRYS.get(p.map_boss_info.map.list_mob[i]);
                            Mob mob_add = new Mob();
                            mob_add.mob_template = temp.mob_template;
                            mob_add.x = temp.x;
                            mob_add.y = temp.y;
                            mob_add.hp_max =
                                    temp.mob_template.hp_max + Body.Point3_Template_hp[p.level];
                            mob_add.hp = mob_add.hp_max;
                            mob_add.level = p.level;
                            mob_add.isdie = false;
                            mob_add.id_target = -1;
                            mob_add.index = -2;
                            mob_add.map = map_boss;
                            mob_add.boss_info = null;
                            // Mob.ENTRYS.put(this.index_mob, mob_add);
                            p.map_boss_info.mob.add(mob_add);
                            // this.index_mob++;
                        }
                        MapBossInfo.add(p.map_boss_info);
                        //
                        p.map_boss_info.map = map_boss;
                        p.map = p.map_boss_info.map;
                        p.x = p.map_boss_info.x_new;
                        p.y = p.map_boss_info.y_new;
                        p.xold = p.x;
                        p.yold = p.y;
                        p.map.goto_map(p);
                        Service.update_PK(p, p, true);
                        Service.pet(p, p, true);
                        Quest.update_map_have_side_quest(p, true);
                        //
                        map_boss.start_map();
                        Map.add_map_plus(map_boss);
                    }
                    break;
                }
                case 15: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        QuestP temp = p.get_quest(p.data_yesno[0]);
                        if (temp == null || temp.template.equals(Quest.QUEST_FINISH)) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn đã hoàn thành hết nhiệm vụ hiện tại");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (temp != null) {
                            if (p.get_ticket() < 3) {
                                Service.send_box_ThongBao_OK(p, "Bạn không đủ 3 bánh mì để nhận");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_ticket(-3);
                            p.update_money();
                            Service.CountDown_Ticket(p);
                            //
                            // remove quest now
                            Quest.remove_old_and_send_next(p, temp);
                            // send dialog quest new
                            Message m = new Message(-23);
                            m.writer().writeByte(5);
                            m.writer().writeShort(temp.template.index);
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                    }
                    break;
                }
                case 14: {
                    if (p.isdie) {
                        if (p.time_can_hs < 1) {
                            Service.send_box_ThongBao_OK(p, "Đã hết số lần hồi sinh tại chỗ!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.pointPk < 20) {
                            if (p.get_vang() < 500) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 500 vàng!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_vang(-500);
                        } else {
                            int fee = p.pointPk / 4;
                            if (p.get_ngoc() < fee) {
                                Service.send_box_ThongBao_OK(p, "Không đủ " + fee + " ruby!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_ngoc(-fee);
                        }
                        p.time_can_hs--;
                        p.update_money();
                        p.time_can_mob_atk = System.currentTimeMillis() + 1200L;
                        Service.use_potion(p, 0, p.body.get_hp_max(true));
                        Service.use_potion(p, 1, p.body.get_mp_max(true));
                        p.isdie = false;
                    }
                    break;
                }
                case 13: {
                    if (p.data_yesno != null && p.data_yesno.length == 2) {
                        ItemTemplate4 it_temp1 = ItemTemplate4.get_it_by_id(p.data_yesno[0]);
                        ItemTemplate4 it_temp2 = ItemTemplate4.get_it_by_id(p.data_yesno[1]);
                        if (it_temp1 != null && it_temp2 != null) {
                            if (it_temp1.id == it_temp2.id) {
                                Service.send_box_ThongBao_OK(p,
                                        "Đá siêu cấp và đá nguyên liệu phải khác nhau!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item.able_bag() < 1) {
                                Service.send_box_ThongBao_OK(p,
                                        "Hành trang phải chừa ít nhất 1 ô trống");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item.total_item_bag_by_id(4, it_temp1.id) < 1) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ 1 " + ItemTemplate4.get_item_name(it_temp1.id));
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item.total_item_bag_by_id(4, it_temp2.id) < 2) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ 2 " + ItemTemplate4.get_item_name(it_temp2.id));
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            boolean suc = (p.percent_da_sieu_cap == 100 ? 120
                                    : p.percent_da_sieu_cap) > Util.random(120);
                            p.item.remove_item47(4, p.data_yesno[1], suc ? 2 : 1);
                            if (suc) {
                                p.item.remove_item47(4, p.data_yesno[0], 1);
                                if (p.data_yesno[0] == 367) { // ho phach
                                    switch (p.data_yesno[1]) {
                                        case 55: {
                                            p.item.add_item_bag47(4, 369, 1);
                                            break;
                                        }
                                        case 61: {
                                            p.item.add_item_bag47(4, 370, 1);
                                            break;
                                        }
                                        case 67: {
                                            p.item.add_item_bag47(4, 371, 1);
                                            break;
                                        }
                                        case 73: {
                                            p.item.add_item_bag47(4, 372, 1);
                                            break;
                                        }
                                        case 79: {
                                            p.item.add_item_bag47(4, 373, 1);
                                            break;
                                        }
                                        default: { // 49
                                            p.item.add_item_bag47(4, 368, 1);
                                            break;
                                        }
                                    }
                                } else {
                                    p.item.add_item_bag47(4, Rebuild_Item.get_id_ngoc_sieu_cap(
                                            p.data_yesno[0], p.data_yesno[1]), 1);
                                }
                                p.percent_da_sieu_cap = 35;
                                //
                                Message m = new Message(-67);
                                m.writer().writeByte(27);
                                m.writer().writeUTF("Chúc mừng bạn nâng cấp thành công ");
                                p.conn.addmsg(m);
                                m.cleanup();
                            } else {
                                p.percent_da_sieu_cap += 5;
                                Message m = new Message(-67);
                                m.writer().writeByte(30);
                                m.writer().writeUTF("Quá trình nâng cấp thất bại!");
                                p.conn.addmsg(m);
                                m.cleanup();
                            }
                            p.item.update_Inventory(-1, false);
                        }
                    }
                    break;
                }
                case 12: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        Item_wear it_select = p.item.bag3[p.data_yesno[0]];
                        if (it_select != null && it_select.numLoKham < 4
                                && it_select.numHoleDaDuc < 2 && it_select.template.typeEquip < 6) {
                            if (it_select.numHoleDaDuc < 1) {
                                if (p.get_ngoc() < 50) {
                                    Service.send_box_ThongBao_OK(p, "Bạn không đủ 50 Ruby");
                                    p.data_yesno = null;
                                    p.map_tele = null;
                                    return;
                                }
                                p.update_ngoc(-50);
                                it_select.numHoleDaDuc = 1;
                            } else {
                                if (p.get_ngoc() < 200) {
                                    Service.send_box_ThongBao_OK(p, "Bạn không đủ 200 Ruby");
                                    p.data_yesno = null;
                                    p.map_tele = null;
                                    return;
                                }
                                p.update_ngoc(-200);
                                it_select.numHoleDaDuc = 2;
                            }
                            p.update_money();
                            it_select.numLoKham++;
                            Message m = new Message(-67);
                            m.writer().writeByte(7);
                            m.writer().writeUTF("Đục lỗ thành công " + it_select.template.name);
                            p.conn.addmsg(m);
                            m.cleanup();
                            p.item.update_Inventory(-1, false);
                        } else {
                            Rebuild_Item.show_table(p, 2);
                            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra hãy thử lại");
                        }
                    }
                    break;
                }
                case 11: {
                    if (p.isTachTB && p.data_yesno != null && p.data_yesno.length == 1) {
                        Item_wear it_select = p.item.bag3[p.data_yesno[0]];
                        if (it_select != null) {
                            byte id_7 = 2;
                            if (it_select.template.color == 2) {
                                id_7 = 3;
                            } else if (it_select.template.color == 3) {
                                id_7 = 4;
                            }
                            //
                            p.item.bag3[p.data_yesno[0]] = null;
                            p.item.add_item_bag47(7, id_7, 1);
                            p.item.update_Inventory(-1, false);
                            //
                            Message m = new Message(-50);
                            m.writer().writeByte(0);
                            m.writer().writeByte(1);
                            m.writer().writeUTF("Thành công");
                            m.writer().writeShort(id_7);
                            m.writer().writeByte(7);
                            m.writer().writeShort(1);
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                        p.isTachTB = false;
                    }
                    break;
                }
                case 10: {
                    if (p.get_ngoc() < 1000) {
                        Service.send_box_ThongBao_OK(p, "Bạn không đủ 1000 Ruby");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    p.update_ngoc(-1000);
                    p.update_vnd(750_000);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Bạn đã đổi thành công 750.000 Extol.");
                    break;
                }
                case 9: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        int extol = p.data_yesno[0] * 1000;
                        if (p.get_vnd() < extol) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn không đủ " + Util.number_format(extol) + " extol");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.update_vnd(-extol);
                        p.update_ngoc(p.data_yesno[0]);
                        p.update_money();
                        Service.send_box_ThongBao_OK(p,
                                "Bạn đã đổi thành công " + Util.number_format(extol) + " extol ra "
                                        + Util.number_format(p.data_yesno[0]) + " Ruby.");
                    }
                    break;
                }
                case 8: {
                    if (p.item_chuyenhoa_save_0 != null && p.item_chuyenhoa_save_1 != null
                            && p.data_yesno != null && p.data_yesno.length == 1) {
                        if (p.get_ngoc() < 500) {
                            Service.send_box_ThongBao_OK(p, "Không đủ 500 ruby!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.item.total_item_bag_by_id(4, p.data_yesno[0]) < 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không đủ " + ItemTemplate4.get_item_name(p.data_yesno[0]));
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.item_chuyenhoa_save_1.template.level > 50
                                && p.item_chuyenhoa_save_1.template.level > (p.item_chuyenhoa_save_0.template.level
                                        + 10)) {
                            Service.send_box_ThongBao_OK(p,
                                    "Trang bị 5x trở lên, khi chuyển hóa chỉ được chuyển hóa cho trang bị cao hơn 1 cấp trang bị!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.item_chuyenhoa_save_0.template.typeEquip == 7
                                || p.item_chuyenhoa_save_1.template.typeEquip == 7) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không thể thực hiện chuyển hóa đối với dial!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.item_chuyenhoa_save_1.levelup = p.item_chuyenhoa_save_0.levelup;
                        p.item_chuyenhoa_save_0.levelup = 0;
                        p.update_ngoc(-500);
                        p.update_money();
                        p.item.remove_item47(4, p.data_yesno[0], 1);
                        p.item.update_Inventory(-1, false);
                        ChuyenHoa.show_result(p,
                                "Quá trình chuyển số cường hóa hoàn tất. Số cường hóa mới là "
                                        + p.item_chuyenhoa_save_1.levelup,
                                p.item_chuyenhoa_save_1.levelup);
                    }
                    break;
                }
                case 7: {
                    if (p.item_chuyenhoa_save_0 != null && p.item_chuyenhoa_save_1 != null
                            && p.data_yesno != null && p.data_yesno.length == 1) {
                        Service.send_box_yesno(p, 8, "Thông báo", ("Khi sử dụng "
                                + ItemTemplate4.get_item_name(p.data_yesno[0]) + " Bạn sẽ"
                                + "mất phí bảo hiểm chuyển hóa 500 Ruby để chuyển hóa 100% cấp cường hóa.\n"
                                + "Lưu ý: Bạn có thể lựa chọn không mất phí nhưng sẽ có tỷ lệ rớt 1 cấp cường hóa"),
                                new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
                        return;
                    }
                    break;
                }
                case 6: {
                    if (p.item_chuyenhoa_save_0 != null && p.item_chuyenhoa_save_1 != null
                            && p.item_chuyenhoa_save_0.levelup > p.item_chuyenhoa_save_1.levelup) {
                        if (p.item_chuyenhoa_save_1.template.level > 50
                                && p.item_chuyenhoa_save_1.template.level > (p.item_chuyenhoa_save_0.template.level
                                        + 10)) {
                            Service.send_box_ThongBao_OK(p,
                                    "Trang bị 5x trở lên, khi chuyển hóa chỉ được chuyển hóa cho trang bị cao hơn 1 cấp trang bị!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.item_chuyenhoa_save_0.template.typeEquip == 7
                                || p.item_chuyenhoa_save_1.template.typeEquip == 7) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không thể thực hiện chuyển hóa đối với dial!");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        if (p.item_chuyenhoa_save_0.levelup >= 10) {
                            ItemTemplate4 it_bh = null;
                            switch (p.item_chuyenhoa_save_0.levelup) {
                                case 15: {
                                    it_bh = ItemTemplate4.get_it_by_id(551);
                                    p.data_yesno = new int[] {551};
                                    break;
                                }
                                case 13:
                                case 14: {
                                    it_bh = ItemTemplate4.get_it_by_id(550);
                                    p.data_yesno = new int[] {550};
                                    break;
                                }
                                default: { // 10, 11, 12
                                    it_bh = ItemTemplate4.get_it_by_id(549);
                                    p.data_yesno = new int[] {549};
                                    break;
                                }
                            }
                            Service.send_box_yesno(p, 7, "Thông báo",
                                    ("Bạn có muốn sử dụng " + it_bh.name + "?"),
                                    new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
                            return;
                        } else {
                            if (p.get_ngoc() < 250) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 250 ruby!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            int random = Util.random(120);
                            p.item_chuyenhoa_save_1.levelup = p.item_chuyenhoa_save_0.levelup;
                            p.item_chuyenhoa_save_0.levelup = 0;
                            if (random < 25) {
                                p.item_chuyenhoa_save_1.levelup -= 1;
                            } else if (random < 50) {
                                p.item_chuyenhoa_save_1.levelup -= 2;
                            } else if (random < 85) {
                                p.item_chuyenhoa_save_1.levelup -= 3;
                            }
                            if (p.item_chuyenhoa_save_1.levelup < 0) {
                                p.item_chuyenhoa_save_1.levelup = 0;
                            }
                            p.update_ngoc(-250);
                            p.update_money();
                            p.item.update_Inventory(-1, false);
                            ChuyenHoa.show_result(p,
                                    "Quá trình chuyển số cường hóa hoàn tất. Số cường hóa mới là "
                                            + p.item_chuyenhoa_save_1.levelup,
                                    p.item_chuyenhoa_save_1.levelup);
                        }
                    }
                    break;
                }
                case 5: {
                    if (p.ship_pet != null) {
                        Service.send_box_ThongBao_OK(p,
                                "Không thể chuyển map khi đang chuyển hàng");
                    } else {
                        if (p.map_tele != null && p.data_yesno != null && p.data_yesno.length == 1
                                && p.data_yesno[0] < p.map_tele.length) {
                            Map[] map_go = Map.get_map_by_id(p.map_tele[p.data_yesno[0]]);
                            //
                            int idMap = MapCanGoTo.idMap[MapCanGoTo.idMap.length - 1];
                            int idMapPb = MapCanGoTo.idMapPb[MapCanGoTo.idMapPb.length - 1];
                            //
                            QuestP quest_select = p.list_quest.get(0);
                            if (quest_select != null) {
                                for (int i = 0; i < MapCanGoTo.idQuest.length; i++) {
                                    if (MapCanGoTo.idQuest[i] > quest_select.template.id) {
                                        idMap = MapCanGoTo.idMap[i - 1];
                                        break;
                                    }
                                }
                            }
                            if ( map_go[0].template.id !=119 && map_go[0].template.id!=120 &&map_go[0].template.id!=122 && map_go[0].template.id!=123 && map_go[0].template.id!=54 && map_go[0].template.id!=58 && map_go[0].template.id!= 59 && map_go[0].template.id!=123 && map_go[0].template.id!=984 && map_go[0].template.id!=1000
                && map_go[0].template.id!=127&& map_go[0].template.id!=167&& map_go[0].template.id!=168&& map_go[0].template.id!=169&& map_go[0].template.id!=170&& map_go[0].template.id!=171&& map_go[0].template.id!=172&& map_go[0].template.id!=173&& map_go[0].template.id!=174&& map_go[0].template.id!=175&& map_go[0].template.id!=176&& map_go[0].template.id > idMap  ) {
                                Service.send_box_ThongBao_OK(p,
                                        "Chưa thể đi đến map này khi chưa hoàn thành nhiệm vụ!");
                                return;
                            }
                            //
                            if (p.map.template.id == map_go[0].template.id) {
                                Service.send_box_ThongBao_OK(p, "Đang ở map này rồi!");
                            } else if (p.get_vang() >= 20) {
                                p.update_vang(-20);
                                p.update_money();
                                Vgo vgo = new Vgo();
                                vgo.map_go = map_go;
                                for (int i = 0; i < vgo.map_go[0].template.npcs.size(); i++) {
                                    Npc npc_temp = vgo.map_go[0].template.npcs.get(i);
                                    if (npc_temp.namegt.equals("Bản đồ")) {
                                        vgo.xnew = npc_temp.x;
                                        if (npc_temp.y < 250) {
                                            vgo.ynew = (short) (npc_temp.y + 20);
                                        } else {
                                            vgo.ynew = (short) (npc_temp.y - 40);
                                        }
                                        break;
                                    }
                                }
                                if (vgo.xnew == 0 || vgo.ynew == 0) {
                                    vgo.xnew = (short) (vgo.map_go[0].template.maxW / 2);
                                    vgo.ynew = (short) (vgo.map_go[0].template.maxH / 2);
                                }
                                p.goto_map(vgo);
                            } else {
                                Service.send_box_ThongBao_OK(p, "Không đủ 20 beri");
                            }
                        }
                    }
                    break;
                }
                case 4: {
                    if (p.map_tele == null && p.data_yesno != null && p.data_yesno.length == 1) {
                        Skill_info temp = p.skill_point.get(p.data_yesno[0]);
                        if (temp.temp.ID >= 1000 && temp.temp.ID < 2000 && temp.temp.Lv_RQ > 0) {
                            if (p.get_ngoc() >= 2) {
                                p.update_ngoc(-2);
                                p.update_money();
                                Skill_Template.reset_skill(temp);
                                p.send_skill();
                                p.update_info_to_all();
                                Service.send_box_ThongBao_OK(p,
                                        "Xóa kỹ năng " + temp.temp.name + " thành công");
                            } else {
                                Service.send_box_ThongBao_OK(p,
                                        "Bạn không đủ 2 Ruby. Phí xóa kỹ năng này là 2 ruby!");
                            }
                        }
                    }
                    break;
                }
                case 3: {
                    if (p.map_tele == null && p.data_yesno != null && p.data_yesno.length == 1
                            && p.data_yesno[0] < p.skill_point.size()) {
                        Skill_info temp = p.skill_point.get(p.data_yesno[0]);
                        if (temp != null && temp.temp.ID < 2000) {
                            if (temp.temp.ID >= 1000) {
                                int dem = 0;
                                for (int i2 = 0; i2 < p.skill_point.size(); i2++) {
                                    Skill_info temp2 = p.skill_point.get(i2);
                                    if (temp2.temp.ID >= 1000 && temp2.temp.ID < 2000
                                            && temp2.temp.typeSkill == 3 && temp2.temp.Lv_RQ > 0) {
                                        dem++;
                                    }
                                }
                                if (temp.temp.Lv_RQ == -1 && temp.temp.typeSkill == 3
                                        && dem >= p.getNumPassive()) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Bạn đã học tối đa " + dem + " / " + p.getNumPassive()
                                                    + " chiêu nội tại, hãy up level để mở thêm!");
                                    p.data_yesno = null;
                                    p.map_tele = null;
                                    return;
                                } else if (temp.temp.Lv_RQ != -1) {
                                    if (temp.temp.Lv_RQ >= 5) {
                                        Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra");
                                        p.data_yesno = null;
                                        p.map_tele = null;
                                        return;
                                    } else {
                                        int index_new = temp.temp.indexSkillInServer + 1;
                                        temp = new Skill_info();
                                        temp.temp = Skill_Template.get_temp(index_new, 0);
                                        temp.exp = 0;
                                        temp.lvdevil = 0;
                                        temp.devilpercent = 0;
                                    }
                                }
                            }
                            Learn_Skill.request_learn_new_skill(p, temp);
                        }
                    }
                    break;
                }
                case 2: {
                    if (p.get_ngoc() < 5) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không đủ tiền, hồi điểm tiềm năng phải cần 5 Ruby.");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    p.update_ngoc(-5);
                    p.update_money();
                    p.reset_point(0);
                    Service.send_box_ThongBao_OK(p, "Bạn đã hồi điểm tiềm năng thành công");
                    break;
                }
                case 4032: {
                    if (p.item.total_item_bag_by_id(4, 32) > 0) {
                        String[] name_ =
                                new String[] {"Sức mạnh của lửa", "Hỏa quyền", "Nắm đấm lửa"};
                        int[] icon_ = new int[] {32, 30, 29};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 32, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4033: {
                    if (p.item.total_item_bag_by_id(4, 33) > 0) {
                        String[] name_ =
                                new String[] {"Sức sống bất diệt", "Chất bất ổn", "Súng máy caosu"};
                        int[] icon_ = new int[] {34, 33, 31};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 33, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4034: {
                    if (p.item.total_item_bag_by_id(4, 34) > 0) {
                        String[] name_ =
                                new String[] {"Tiến hóa", "Thuốc tăng trưởng", "Hóa tuần lộc"};
                        int[] icon_ = new int[] {37, 36, 35};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 34, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4088: {
                    if (p.item.total_item_bag_by_id(4, 88) > 0) {
                        String[] name_ = new String[] {"Khói bất tử", "Khói tốc độ", "Mưa khói"};
                        int[] icon_ = new int[] {38, 39, 40};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 88, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4090: {
                    if (p.item.total_item_bag_by_id(4, 90) > 0) {
                        String[] name_ =
                                new String[] {"Bản năng thủ lĩnh", "Hóa bò tót", "Bất khuất"};
                        int[] icon_ = new int[] {48, 47, 46};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 90, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4091: {
                    if (p.item.total_item_bag_by_id(4, 91) > 0) {
                        String[] name_ = new String[] {"Nét vẽ cường hóa", "Nét vẽ phòng thủ",
                                "Nét vẽ sức mạnh"};
                        int[] icon_ = new int[] {51, 50, 49};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 91, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4092: {
                    if (p.item.total_item_bag_by_id(4, 92) > 0) {
                        String[] name_ = new String[] {"Băng vĩnh cửu", "Mưa băng", "Tuyết tê tái"};
                        int[] icon_ = new int[] {57, 56, 53};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 92, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4093: {
                    if (p.item.total_item_bag_by_id(4, 93) > 0) {
                        String[] name_ =
                                new String[] {"Cát lưu động", "Bão cát sa mạc", "Cát linh động"};
                        int[] icon_ = new int[] {55, 54, 52};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 93, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4160: {
                    if (p.item.total_item_bag_by_id(4, 160) > 0) {
                        String[] name_ = new String[] {"Sấm chớp rền vang", "Lôi phạt",
                                "Bùng nổ sức mạnh", "Ý chí thần sấm"};
                        int[] icon_ = new int[] {61, 60, 59, 58};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 160, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4161: {
                    if (p.item.total_item_bag_by_id(4, 161) > 0) {
                        String[] name_ = new String[] {"Bão nham thạch", "Cột lửa", "Bùng cháy",
                                "Nỗi đau bỏng cháy"};
                        int[] icon_ = new int[] {65, 64, 63, 62};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 161, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4219: {
                    if (p.item.total_item_bag_by_id(4, 219) > 0) {
                        String[] name_ =
                                new String[] {"Sóng âm - Xung kích", "Hóa báo đốm", "Tia chớp"};
                        int[] icon_ = new int[] {72, 71, 70};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 219, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4220: {
                    if (p.item.total_item_bag_by_id(4, 220) > 0) {
                        String[] name_ =
                                new String[] {"Cơn lốc - Ưng kích", "Hóa chim ưng", "Chim săn mồi"};
                        int[] icon_ = new int[] {69, 68, 67};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 220, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4240: {
                    if (p.item.total_item_bag_by_id(4, 240) > 0) {
                        String[] name_ =
                                new String[] {"Bộc phá", "Vết nứt", "Kình lực", "Địa chấn"};
                        int[] icon_ = new int[] {76, 75, 73, 74};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 240, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4316: {
                    if (p.item.total_item_bag_by_id(4, 316) > 0) {
                        String[] name_ = new String[] {"Giáp sáp", "Đao không kích", "Lao sáp"};
                        int[] icon_ = new int[] {79, 77, 78};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 316, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4317: {
                    if (p.item.total_item_bag_by_id(4, 317) > 0) {
                        String[] name_ = new String[] {"Thân thể thép", "Ảo ảnh trảm", "Loạn trảm"};
                        int[] icon_ = new int[] {82, 81, 80};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 317, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4318: {
                    if (p.item.total_item_bag_by_id(4, 318) > 0) {
                        String[] name_ =
                                new String[] {"Thần hộ thể", "Tăng trọng", "Sức nặng ngàn cân"};
                        int[] icon_ = new int[] {85, 84, 83};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 318, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 4427: { // trai bong toi
                    if (p.item.total_item_bag_by_id(4, 427) > 0) {
                        String[] name_ = new String[] {"Dòng chảy ma pháp", "Vòng xoáy ma pháp",
                                "Giải phóng", "Xoáy đen"};
                        int[] icon_ = new int[] {91, 88, 90, 89};
                        Service.NewDialog_eat_taq(p, name_, icon_, (id - 4000));
                        p.get_skill_taq_new(id - 4000);
                        p.item.remove_item47(4, 427, 1);
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 1: {
                    if (p.item_to_kham_ngoc != null) {
                        int vang_req = 0;
                        for (int i = 0; i < p.item_to_kham_ngoc.mdakham.length; i++) {
                            if (p.item_to_kham_ngoc.mdakham[i] >= 44
                                    && p.item_to_kham_ngoc.mdakham[i] <= 79) {
                                vang_req += Rebuild_Item.PRICE_THAO_NGOC[Rebuild_Item
                                        .get_percent_hop_ngoc(p.item_to_kham_ngoc.mdakham[i])];
                            } else if (p.item_to_kham_ngoc.mdakham[i] >= 241
                                    && p.item_to_kham_ngoc.mdakham[i] <= 270) {
                                vang_req += 300;
                            } else {
                                vang_req += 350;
                            }
                        }
                        if (vang_req > 0) {
                            if (p.get_ngoc() < vang_req) {
                                Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " ruby");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            if (p.item_to_kham_ngoc.mdakham.length > p.item.able_bag()) {
                                Service.send_box_ThongBao_OK(p,
                                        "Hãy chừa ít nhất " + p.item_to_kham_ngoc.mdakham.length
                                                + " ô trống trong hành trang");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_ngoc(-vang_req);
                            p.update_money();
                            for (int i = 0; i < p.item_to_kham_ngoc.mdakham.length; i++) {
                                p.item.add_item_bag47(4, p.item_to_kham_ngoc.mdakham[i], 1);
                            }
                            p.item_to_kham_ngoc.mdakham = new short[0];
                            p.item_to_kham_ngoc.option_item_2.clear();
                            p.item.update_Inventory(-1, false);
                            Message m = new Message(-67);
                            m.writer().writeByte(6);
                            m.writer().writeUTF("Tháo ngọc khảm trang bị "
                                    + p.item_to_kham_ngoc.template.name + " thành công");
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                    } else {
                        Rebuild_Item.show_table(p, 4);
                        Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, hãy thử lại");
                    }
                    break;
                }
                case 0: {
                    // use_item_3
                    if (p.use_item_3 != -1) {
                        Item_wear it = p.item.bag3[p.use_item_3];
                        if (it != null && UseItem.check_it_can_wear(it.template.typeEquip)) {
                            p.wear_item(it);
                        }
                        p.use_item_3 = -1;
                    }
                    break;
                }
            }
        } else if (value == 1) { // hoi ren
            switch (id) {
                case 53: {
                    if (p.name_ThoSanHaiTac != null && p.name_ThoSanHaiTac.length == 1
                            && p.typePirate == 1) {
                        Player p0 = Map.get_player_by_name_allmap(p.name_ThoSanHaiTac[0]);
                        if (p0 != null && p0.map.equals(p.map)) {
                            Service.send_box_ThongBao_OK(p0, p.name + " từ chối bảo vệ vận hàng");
                        }
                        Service.send_box_ThongBao_OK(p, "từ chối thành công");
                        p.name_ThoSanHaiTac = null;
                    }
                    break;
                }
                case 36: {
                    if (p.get_ngoc() < 45) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 45 ruby");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 232) >= DataTemplate.MAX_ITEM_IN_BAG) {
                        Service.send_box_ThongBao_OK(p,
                                "Số lượng vé đã đạt tối đa trong hành trang");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    p.update_ngoc(-45);
                    p.update_money();
                    p.item.add_item_bag47(4, 232, 3);
                    p.item.update_Inventory(-1, false);
                    Service.send_box_ThongBao_OK(p, "Mua 3 Vé vòng xoay may mắn thành công");
                    break;
                }
                case 34: {
                    if (p.item.total_item_bag_by_id(7, 9) < 10) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không có đủ " + ItemTemplate7.get_it_by_id(9).name);
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 29) < 1) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không có đủ " + ItemTemplate4.get_it_by_id(29).name);
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.get_ngoc() < 5) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 5 ruby");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.able_bag() < 1) {
                        Service.send_box_ThongBao_OK(p,
                                "Hãy chừa ít nhất 1 ô trống trong hành trang");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 158) >= DataTemplate.MAX_ITEM_IN_BAG) {
                        Service.send_box_ThongBao_OK(p,
                                "Số lượng rương Đại ác quỷ đã đạt tối đa trong hành trang");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    p.update_ngoc(-5);
                    p.update_money();
                    p.item.remove_item47(7, 9, 10);
                    //
                    boolean suc = 5 > Util.random(150);
                    if (suc) {
                        p.item.remove_item47(4, 29, 1);
                        p.item.add_item_bag47(4, 158, 1);
                    }
                    p.item.update_Inventory(-1, false);
                    //
                    Message m = new Message(45);
                    m.writer().writeByte(17);
                    m.writer().writeByte(suc ? 1 : 3);
                    m.writer().writeUTF(
                            "Nâng cấp lên Rương đại ác quỷ " + (suc ? "thành công" : "thất bại"));
                    p.conn.addmsg(m);
                    m.cleanup();
                    break;
                }
                case 33: {
                    if (p.data_yesno != null && p.data_yesno.length == 1) {
                        Skill_info sk_temp = p.get_skill_temp(p.data_yesno[0]);
                        if (sk_temp != null) {
                            if (sk_temp.lvdevil > 4) {
                                Service.send_box_ThongBao_OK(p,
                                        sk_temp.temp.name + " đã được cường hóa tối đa!");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            int percent = (sk_temp.lvdevil == 0) ? 10 //
                                    : ((sk_temp.lvdevil == 1) ? 8 //
                                            : ((sk_temp.lvdevil == 2) ? 6 //
                                                    : ((sk_temp.lvdevil == 3) ? 5 : 4)));
                            if (p.get_ngoc() < 5) {
                                Service.send_box_ThongBao_OK(p, "Không đủ 5 ruby");
                                p.data_yesno = null;
                                p.map_tele = null;
                                return;
                            }
                            p.update_ngoc(-5);
                            p.update_money();
                            p.item.remove_item47(7, 9, 10);
                            p.item.update_Inventory(-1, false);
                            //
                            boolean suc = 50 > Util.random(120 + sk_temp.lvdevil * 10);
                            if (suc) {
                                sk_temp.devilpercent += percent;
                                if (sk_temp.devilpercent >= 100) {
                                    sk_temp.devilpercent = 0;
                                    sk_temp.lvdevil++;
                                }
                                p.send_skill();
                                p.update_info_to_all();
                            }
                            //
                            Message m = new Message(45);
                            m.writer().writeByte(12);
                            m.writer().writeByte(suc ? 1 : 3);
                            m.writer().writeUTF(
                                    "Cường hóa kỹ năng " + (suc ? "thành công" : "thất bại"));
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                    }
                    break;
                }
                case 31: {
                    if (p.item_to_kham_ngoc != null && (p.item_to_kham_ngoc.template.typeEquip < 6
                            || p.item_to_kham_ngoc.template.typeEquip == 7)) {
                        if (p.get_ngoc() < 100) {
                            Service.send_box_ThongBao_OK(p, "Không đủ 100 ruby");
                            p.data_yesno = null;
                            p.map_tele = null;
                            return;
                        }
                        p.update_ngoc(-100);
                        p.update_money();
                        p.item_to_kham_ngoc.valueChetac += 10;
                        if (p.item_to_kham_ngoc.valueChetac >= 100) {
                            p.item_to_kham_ngoc.valueChetac = 100;
                        }
                        //
                        Message m = new Message(-67);
                        m.writer().writeByte(25);
                        m.writer().writeUTF("Bạn phục hồi 10 điểm chế tác thành công");
                        p.conn.addmsg(m);
                        m.cleanup();
                        //
                        p.item.update_Inventory(-1, false);
                    }
                    break;
                }
                case 16: {
                    p.map_boss_info = null;
                    break;
                }
                case 11: {
                    p.isTachTB = false;
                    break;
                }
                case 7: {
                    if (p.get_ngoc() < 250) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 250 ruby!");
                        p.data_yesno = null;
                        p.map_tele = null;
                        return;
                    }
                    p.item_chuyenhoa_save_1.levelup = p.item_chuyenhoa_save_0.levelup;
                    p.item_chuyenhoa_save_0.levelup = 0;
                    if (80 > Util.random(120)) {
                        p.item_chuyenhoa_save_1.levelup -= Util.random(0, 3);
                        if (p.item_chuyenhoa_save_1.levelup < 0) {
                            p.item_chuyenhoa_save_1.levelup = 0;
                        }
                    }
                    p.update_ngoc(-250);
                    p.update_money();
                    p.item.update_Inventory(-1, false);
                    ChuyenHoa.show_result(p,
                            "Quá trình chuyển số cường hóa hoàn tất. Số cường hóa mới là "
                                    + p.item_chuyenhoa_save_1.levelup,
                            p.item_chuyenhoa_save_1.levelup);
                    break;
                }
                case 1: {
                    Rebuild_Item.show_table(p, 4);
                    break;
                }
                case 0: {
                    p.use_item_3 = -1;
                    break;
                }
            }
        } else if (value == 2) { // other
        }
        if (id != 13) {
            p.data_yesno = null;
            p.map_tele = null;
            p.data_yesno_gem = null;
        }
    }
}
