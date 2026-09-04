package client;

import java.io.IOException;

import activities.Chat;
import activities.DanhHieu;
import activities.ChuyenHoa;
import activities.Fight;
import activities.Friend;
import activities.HanhTrinh;
import activities.Join_Item;
import activities.Learn_Skill;
import activities.LittleGarden;
import activities.Market;
import activities.Max_Level;
import activities.Pvp;
import activities.Rebuild_Item;
import activities.Red_Line;
import activities.Ship;
import activities.Split_Item;
import activities.TableTickOption;
import activities.Trade;
import activities.UpgradeDevil;
import activities.UpgradeDial;
import activities.UpgradeItem;
import activities.UpgradeSuperItem;
import activities.Upgrade_Skin;
import activities.VongQuay;
import activities.Vong_quay_oc_sen;
import activities.Wanted;
import core.BXH;
import core.Manager;
import core.MenuController;
import core.Service;
import core.Util;
import client.Qua_tu_hop;
import event.EventSpecial;
import io.Message;
import io.Session;
import map.Map;
import template.EffTemplate;
import template.ItemBoat;

/**
 *
 * @author Truongbk
 */
public class MessageHandler {

    private Session conn;

    public MessageHandler(Session session) {
        this.conn = session;
    }

    public void process_msg(Message m) throws IOException {
        switch (m.cmd) {
            case 69: {
                Qua_tu_hop.process(conn.p, m);
                break;
            }
            case -102: {
                if (conn.p != null) {
                    DanhHieu.process(m, conn.p);
                }
                break;
            }
            case -86: {
                if (conn.p != null) {
                    Wanted_Chest.process(conn.p, m);
                }
                break;
            }
            case -85: {
                if (conn.p != null) {
                    Wanted.process(conn.p, m);
                }
                break;
            }
            case -35: {
                if (conn.p != null) {
                    try {
                        Fight.process(conn.p, m);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
                break;
            }
            case -80: {
                if (conn.p != null) {
                    Pet.process(conn.p, m);
                }
                break;
            }
            case 79: {
                if (conn.p != null) {
                    HanhTrinh.process(conn.p, m);
                }
                break;
            }
            case -94: {
                if (conn.p != null) {
                    UpgradeDial.process(conn.p, m);
                }
                break;
            }
            case 81: {
                if (conn.p != null) {
                    Upgrade_Skin.process(conn.p, m);
                }
                break;
            }
            case -53: {
                if (conn.p != null) {
                    Ship.process(conn.p, m);
                }
                break;
            }
            case 37: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    byte index = m.reader().readByte();
                    if (type == 1) { // Request reward
                        if (index >= 0 && index < 8) {
                            if (conn.p.daily_achievements[index] == 1 && !conn.p.daily_achievements_claimed[index]) {
                                conn.p.daily_achievements_claimed[index] = true;
                                conn.p.update_ngoc(100);
                                conn.p.update_vang(1000000);
                                conn.p.update_money();

                                io.Message m2 = new io.Message(37);
                                m2.writer().writeByte(1); // update reward status
                                m2.writer().writeByte(index);
                                m2.writer().writeByte(2); // REWARD_GOT
                                conn.addmsg(m2);
                                m2.cleanup();

                                core.Service.send_box_ThongBao_OK(conn.p,
                                        "Nhận thưởng thành công 100 Ruby và 1,000,000 Beri!");
                            } else if (conn.p.daily_achievements_claimed[index]) {
                                core.Service.send_box_ThongBao_OK(conn.p, "Bạn đã nhận thưởng rồi!");
                            } else {
                                core.Service.send_box_ThongBao_OK(conn.p, "Nhiệm vụ chưa hoàn thành!");
                            }
                        }
                    }
                }
                break;
            }
            case 74: {
                byte type = m.reader().readByte();
                short id = m.reader().readShort();
                if (conn.p != null) {
                    Service.send_effect_data(conn, id);
                }
                break;
            }
            case 43: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    byte value = m.reader().readByte();
                    if (type == 0) {
                        if (value == 1 || value == 0) {
                            conn.p.is_show_hat = !conn.p.is_show_hat;
                            Service.charWearing(conn.p, conn.p, false);
                            Service.update_PK(conn.p, conn.p, false);
                            conn.p.update_info_to_all();
                            Service.send_box_ThongBao_OK(conn.p,
                                    conn.p.is_show_hat ? "Đã bật hiển thị nón"
                                            : "Đã tắt hiển thị nón");
                        }
                    } else if (type == 1) {
                        if (value == 1 || value == 0) {
                            conn.p.is_show_weapon = !conn.p.is_show_weapon;
                            Service.charWearing(conn.p, conn.p, false);
                            Service.update_PK(conn.p, conn.p, false);
                            conn.p.update_info_to_all();
                            Service.send_box_ThongBao_OK(conn.p,
                                    conn.p.is_show_weapon ? "Đã bật hiển thị vũ khí thời trang"
                                            : "Đã tắt hiển thị vũ khí thời trang");
                        }
                    }
                }
                break;
            }
            case -91: {
                if (conn.p != null) { // dau gia
                }
                break;
            }
            case -63: {
                if (conn.p != null) {
                    Pvp.process(conn.p, m);
                }
                break;
            }
            case 68: {
                if (conn.p != null && conn.p.map != null) {
                    conn.p.tocSuper++;
                    if (conn.p.tocSuper > 2) {
                        conn.p.tocSuper = 0;
                    }
                    for (int i = 0; i < conn.p.map.players.size(); i++) {
                        Player p0 = conn.p.map.players.get(i);
                        Service.charWearing(conn.p, p0, false);
                    }
                }
                break;
            }
            case -95: {
                break;
            }
            case -36: {
                int id = m.reader().readInt();
                if (conn.p != null) {
                    Player p0 = null;
                    for (int i = 0; i < conn.p.friend_list.size(); i++) {
                        if (conn.p.friend_list.get(i).id == id) {
                            p0 = Map.get_player_by_name_allmap(conn.p.friend_list.get(i).name);
                            break;
                        }
                    }
                    if (p0 == null) {
                        for (int i = 0; i < conn.p.enemy_list.size(); i++) {
                            if (conn.p.enemy_list.get(i).id == id) {
                                p0 = Map.get_player_by_name_allmap(conn.p.enemy_list.get(i).name);
                                break;
                            }
                        }
                    }
                    boolean check = false;
                    if (p0 != null) {
                        if (Map.map_cant_save_site(p0.map.template.id)) {
                            Service.send_box_ThongBao_OK(conn.p,
                                    "Không thể dịch chuyển đến lúc này");
                            return;
                        }
                        for (int i = 0; i < p0.friend_list.size(); i++) {
                            if (p0.friend_list.get(i).name.equals(conn.p.name)) {
                                check = true;
                                break;
                            }
                        }
                        if (!check) {
                            for (int i = 0; i < conn.p.enemy_list.size(); i++) {
                                if (conn.p.enemy_list.get(i).name.equals(p0.name)) {
                                    check = true;
                                    break;
                                }
                            }
                        }
                        if (check) {
                            conn.p.data_yesno = new int[] { id };
                            Service.send_box_yesno(conn.p, 43, "Thông báo",
                                    ("Dịch chuyển đến người này mất 5 ruby, xác nhận dịch chuyển?"),
                                    new String[] { "5", "Không" }, new byte[] { 7, -1 });
                        }
                    }
                    if (!check) {
                        Service.send_box_ThongBao_OK(conn.p,
                                "Đối phương không online hoặc không có trong danh sách");
                    }
                }
                break;
            }
            case -52: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    // System.out.println(type);
                    if (conn.p.clan != null || type == 11 || type == 12) { // type 11 xin vao clan,
                        // type 12 dong y moi
                        // vao clan
                        Clan.process(conn.p, m, type);
                    }
                }
                break;
            }
            case 49: {
                if (conn.p != null) {
                    Max_Level.process(conn.p, m);
                }
                break;
            }
            case 80: { // event
                if (conn.p != null) {
                    EventSpecial.process(conn.p, m);
                }
                break;
            }
            case -71: { // auto revive
                if (conn.p != null && conn.p.map != null && conn.p.map.map_pvp == null
                        && conn.p.map.map_little_garden == null) {
                    if (conn.p.type_pk == -1 && conn.p.typePirate == -1 && conn.p.pointPk == 0) {
                        if (m.reader().readByte() == 1) {
                            if (conn.p.item.total_item_bag_by_id(4, 89) > 0) {
                                conn.p.item.remove_item47(4, 89, 1);
                                conn.p.item.update_Inventory(-1, false);
                                conn.p.isdie = false;
                                Service.use_potion(conn.p, 0, conn.p.body.get_hp_max(true));
                                Service.use_potion(conn.p, 1, conn.p.body.get_mp_max(true));
                                //
                                Message m2 = new Message(-71);
                                m2.writer().writeByte(1);
                                m2.writer().writeShort(conn.p.index_map);
                                m2.writer().writeByte(0);
                                m2.writer().writeInt(60 * 30);
                                conn.p.map.send_msg_all_p(m2, conn.p, true);
                                m2.cleanup();
                                EffTemplate eff = conn.p.get_eff(7);
                                if (eff != null) {
                                    eff.time = System.currentTimeMillis() + 60_000L * 15;
                                } else {
                                    conn.p.add_new_eff(7, 1, 60_000L * 15);
                                }
                            }
                        }
                    }
                }
                break;
            }
            case -74: {
                if (conn.p != null) {
                    TableTickOption.process(conn.p, m);
                }
                break;
            }
            case 44: {

                if (conn.p != null) {
                    Market.process(conn.p, m);
                }
                break;
            }
            case -23: {
                if (conn.p != null) {
                    Quest.process(conn.p, m);
                }
                break;
            }
            case -72: {
                if (conn.p != null && conn.p.map != null) {
                    if (conn.p.map.template.id == 64) {
                        Red_Line.process(conn.p, m);
                    } else if (conn.p.map.map_ThuThachVeThan != null
                            && !conn.p.map.map_ThuThachVeThan.isFinish) {
                        Red_Line.process_TTVT(conn.p, m);
                    }
                }
                break;
            }
            case 66: {
                if (conn.p != null) {
                    UpgradeSuperItem.process(conn.p, m);
                }
                break;
            }
            case -30: {
                if (conn.p != null) {
                    BXH.process(conn.p, m);
                }
                break;
            }
            case -50: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    if (type == 0) {
                        Split_Item.process(conn.p, m);
                    } else if (type == 1) {
                        Join_Item.process(conn.p, m);
                    }
                }
                break;
            }
            case -62: {
                if (conn.p != null) {
                    ItemBoat.update_part_boat_when_shopping(conn.p);
                }
                break;
            }
            case -28: {
                if (conn.p != null) {
                    Learn_Skill.process(conn.p, m);
                }
                break;
            }
            case 45: {
                if (conn.p != null) {
                    UpgradeDevil.process(conn.p, m);
                }
                break;
            }
            case -25: {
                if (conn.p != null) {
                    Party.process(conn.p, m);
                }
                break;
            }
            case 20: {
                if (conn.p != null && conn.p.map != null && !conn.p.isdie
                        && conn.p.get_eff(201) == null) {
                    Buff.process(conn.p, m);
                }
                break;
            }
            case -32: {
                if (conn.p != null) {
                    PlayerChest.process(conn.p, m);
                }
                break;
            }
            case 54: {
                if (conn.p != null) {
                    if (conn.p.type_vongquay == 1) {
                        activities.VongQuayPet.process(conn.p, m);
                    } else {
                        VongQuay.process(conn.p, m);
                    }
                }
                break;
            }
            case 77: {
                if (conn.p != null) {
                    Vong_quay_oc_sen.process(conn.p, m);
                }
                break;
            }
            case 18: {
                if (conn.p != null) {
                    Chat.process(conn.p, m, 0);
                }
                break;
            }
            case -29: {
                if (conn.p != null) {
                    Friend.process(conn.p, m);
                }
                break;
            }
            case -49: {
                if (conn.status != 1) {
                    Service.send_box_ThongBao_OK(conn.p,
                            "Chưa Kích hoạt không thể giao dịch");
                    return;
                }
                Trade.process(conn.p, m);
                break;
            }
            case 59: {
                if (conn.p != null) {
                    byte action = m.reader().readByte();
                    short idObj = m.reader().readShort();
                    byte cat = m.reader().readByte();
                    if (action == 2) {
                        activities.GiftRuby.handleGiftRubyRequest(conn.p, idObj);
                    }
                }
                break;
            }
            case -67: {
                if (conn.p != null) {
                    Rebuild_Item.process(conn.p, m);
                }
                break;
            }
            case -77: {
                if (conn.p != null) {
                    ChuyenHoa.process(conn.p, m);
                }
                break;
            }
            case -97: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    short id = m.reader().readShort();
                    if (type == 1) {
                        activities.ListTichNap.claimReward(conn.p, id);
                    }
                }
                break;
            }
            case -90: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    byte id = m.reader().readByte();
                    if (type == 1) {
                        activities.ListTichNap.claimReward(conn.p, id);
                    }
                }
                break;
            }
            case -96: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    byte id = m.reader().readByte();
                    if (type == 1) {
                        activities.ListTichTieu.claimReward(conn.p, id);
                    }
                }
                break;
            }
            case -89: { // Thợ săn hải tặc (Truy nã)
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    if (type == 4) { // Nhận thưởng / Báo thù
                        try {
                            String name = m.reader().readUTF();
                            core.Service.send_box_ThongBao_OK(conn.p,
                                    "Tính năng này không dùng trong Thợ săn hải tặc!");
                        } catch (Exception e) {
                        }
                    } else if (type == 5) { // Tìm kiếm
                        try {
                            String name = m.reader().readUTF();
                            core.Service.send_box_ThongBao_OK(conn.p,
                                    "Tính năng này không dùng trong Thợ săn hải tặc!");
                        } catch (Exception e) {
                        }
                    } else if (type == 1) { // Load thêm trang (hoặc load lần đầu)
                        byte page = m.reader().readByte();
                        core.BXH.send_wanted_list(conn.p, page);
                    }
                }
                break;
            }

            case -16: {
                if (conn.p != null) {
                    conn.p.plus_point(m);
                }
                break;
            }
            case -13: { // use potion
                if (conn.p != null) {
                    short id = m.reader().readShort();
                    UseItem.use_item_potion(conn.p, id);
                }
                break;
            }
            case -11: {
                if (conn.p != null) {
                    ClientYesNo.process(conn.p, m);
                }
                break;
            }
            case -58: {
                if (conn.p != null) {
                    ClientInput.process(conn.p, m);
                }
                break;
            }
            case -46: {
                if (conn.status != 1) {
                    Service.send_box_ThongBao_OK(conn.p,
                            "Chưa Kích hoạt không thể chat KTG");
                    return;
                }
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    String text = m.reader().readUTF();
                    if (type == 0) {

                        if (conn.p.get_ngoc() < 5) {
                            Service.send_box_ThongBao_OK(conn.p, "Không đủ 5 ruby để chat KTG");
                            return;
                        }
                        conn.p.update_ngoc(-5);
                        conn.p.update_money();
                        Manager.gI().chatKTG(conn.p, conn.p.name + ": " + text);
                    } else if (type == 1 && conn.p.clan != null) {
                        boolean check = false;
                        for (int i = 0; i < conn.p.clan.members.size(); i++) {
                            if (conn.p.clan.members.get(i).name.equals(conn.p.name)
                                    && (conn.p.clan.members.get(i).levelInclan == 1
                                            || conn.p.clan.members.get(i).levelInclan == 0)) {
                                check = true;
                                break;
                            }
                        }
                        if (check) {
                            if (conn.p.clan.get_ngoc() < 15) {
                                Service.send_box_ThongBao_OK(conn.p,
                                        "Không đủ 15 ruby băng để chat KTG");
                                return;
                            }
                            conn.p.clan.update_ruby(-15);
                            for (int i = 0; i < conn.p.clan.members.size(); i++) {
                                Player p0 = Map
                                        .get_player_by_name_allmap(conn.p.clan.members.get(i).name);
                                if (p0 != null) {
                                    Clan.send_money(p0, false);
                                }
                            }
                            Message m23 = new Message(-31);
                            m23.writer().writeByte(type);
                            m23.writer().writeUTF(conn.p.clan.name + ": " + text);
                            m23.writer().writeByte(0);
                            m23.writer().writeShort(conn.p.clan.icon);
                            for (Map[] mapall : Map.ENTRYS) {
                                for (Map map : mapall) {
                                    for (int i = 0; i < map.players.size(); i++) {
                                        Player p0 = map.players.get(i);
                                        p0.conn.addmsg(m23);
                                    }
                                }
                            }
                            m23.cleanup();
                        }
                    }
                }
                break;
            }
            case -48: {
                if (conn.p != null) {
                    UpgradeItem.process(conn.p, m);
                }
                break;
            }
            case -22: {
                if (conn.p != null) {
                    UseItem.process(conn.p, m);
                }
                break;
            }
            case -105: {
                if (conn.p != null) {
                    Service.request_item4_info(conn.p, m);
                }
                break;
            }
            case -21: {
                if (conn.p != null) {
                    Service.sell_item(conn.p, m);
                }
                break;
            }
            case 12: {
                if (conn.p != null) {
                    conn.p.map.pick_item(conn.p, m);
                }
                break;
            }
            case -42: {
                if (conn.p != null) {
                    String name = m.reader().readUTF();
                    Player p0 = Map.get_player_by_name_allmap(name);
                    if (p0 != null) {
                        Service.send_view_other_player(p0, conn.p);
                    }
                }
                break;
            }
            case 14: {
                if (conn.p != null) {
                    byte type = m.reader().readByte();
                    byte act = m.reader().readByte();
                    // System.out.println(type);
                    // System.out.println(act);
                    if (act == 0) {
                        conn.p.map.change_flag(conn.p, type);
                    }
                }
                break;
            }
            case 6: {
                if (conn.p != null) {
                    conn.p.request_live_from_die(m);
                }
                break;
            }
            case -18: {
                if (conn.p != null) {
                    Service.buy_item(conn.p, m);
                }
                break;
            }
            case -5: {
                if (conn.p != null) {
                    short id = m.reader().readShort();
                    conn.p.map.send_char_in4_inmap(conn.p, id);
                }
                break;
            }
            case 46: {
                if (conn.p != null) {
                    Service.checkPlayInMap(conn.p, m);
                }
                break;
            }
            case 0: {
                if (conn.p != null) {
                    // int id_map_change =
                    m.reader().readShort();
                    // byte action_change =
                    m.reader().readByte();
                    // System.out.println(id_map_change);
                    // System.out.println(action_change);
                    while (conn.p.list_msg_cache.size() > 0) {
                        try {
                            Message m_send = conn.p.list_msg_cache.take();
                            conn.addmsg(m_send);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                    try {
                        if (DanhHieu.get_Id(conn.p.id_danh_hieu_su_dung) != null) {
                            System.out.println(
                                    "[DanhHieu Debug] MessageHandler send title for: " + conn.p.name + " start");
                            Player p0 = conn.p;
                            Message msg = new Message(-102);
                            msg.writer().writeByte(1);
                            msg.writer().writeByte(0);
                            msg.writer().writeInt(p0.id);
                            msg.writer().writeInt(DanhHieu.get_Id(p0.id_danh_hieu_su_dung).idicon);
                            msg.writer().writeInt(DanhHieu.get_Id(p0.id_danh_hieu_su_dung).nframe);
                            p0.conn.addmsg(msg);
                            msg.cleanup();
                            System.out.println(
                                    "[DanhHieu Debug] MessageHandler send title for: " + conn.p.name + " successful");
                        }
                    } catch (Exception e) {
                        System.err.println("[DanhHieu Error] Error MessageHandler send title: " + e.getMessage());
                        e.printStackTrace();
                    }
                    Service.getThanhTich(conn.p, conn.p);
                    conn.p.map.send_in4_obj_inmap(conn.p);
                    conn.p.wait_change_map = false;
                    //
                    if (conn.p.map.template.id == 1000) {// map wait pvp
                        Pvp.show_table(conn.p);
                    }
                    if (conn.p.map.map_pvp != null) {// map pvp
                        Pvp.show_info(conn.p, 0, 0, 0, 3);
                    }
                    conn.p.map.change_flag(conn.p, conn.p.type_pk);
                    Service.update_PK(conn.p, conn.p, false);
                    // weather
                    conn.p.map.send_weather(conn.p);
                    // ship pet
                    if (conn.p.ship_pet != null && conn.p.ship_pet.map == null) {
                        conn.p.ship_pet.map = conn.p.map;
                        conn.p.ship_pet.id_map_save = conn.p.map.template.id;
                        conn.p.ship_pet.x = conn.p.x;
                        conn.p.ship_pet.y = conn.p.y;
                        Message m_local = new Message(1);
                        m_local.writer().writeByte(0);
                        m_local.writer().writeShort(conn.p.ship_pet.index_map);
                        m_local.writer().writeShort(conn.p.ship_pet.x);
                        m_local.writer().writeShort(conn.p.ship_pet.y);
                        for (int j = 0; j < conn.p.map.players.size(); j++) {
                            Player p0 = conn.p.map.players.get(j);
                            p0.conn.addmsg(m_local);
                        }
                        m_local.cleanup();
                    }
                    if (Map.is_map_dungeon(conn.p.map.template.id)) {
                        if (conn.p.map.map_bossHunt != null) {
                            activities.BossHunt bh = conn.p.map.map_bossHunt;
                            Service.send_time_cool_down(conn.p, bh.floorTime, "Tầng " + (bh.currentFloor + 1), 2);
                        } else if (conn.p.dungeon != null) {
                            if (conn.p.dungeon instanceof activities.TowerChallenge) {
                                activities.TowerChallenge tc = (activities.TowerChallenge) conn.p.dungeon;
                                Service.send_time_cool_down(conn.p, tc.stageEndTime,
                                        "Tầng " + (tc.currentStageIndex + 1),
                                        2);
                            } else if (conn.p.dungeon instanceof activities.NamieTreasureDefense) {
                                activities.NamieTreasureDefense nd = (activities.NamieTreasureDefense) conn.p.dungeon;
                                int waveNum = nd.currentWaveIndex == -1 ? 1 : (nd.currentWaveIndex + 1);
                                Service.send_time_cool_down(conn.p, nd.dungeonEndTime, "Tầng " + waveNum, 2);
                            } else if (conn.p.dungeon instanceof activities.HangDong) {
                                activities.HangDong hd = (activities.HangDong) conn.p.dungeon;
                                if (hd.isTransitioning) {
                                    Service.send_time_cool_down(conn.p, hd.transitionTime, "Chuyển tầng", 2);
                                } else {
                                    Service.send_time_cool_down(conn.p, hd.stageEndTime,
                                            "Tầng " + (hd.currentStageIndex + 1),
                                            2);
                                }
                            } else {
                                int floorNum = conn.p.map.template.id - 167 + 1;
                                Service.send_time_cool_down(conn.p, conn.p.dungeon.time, "Thời gian ", 2);
                            }
                        }
                    } else if (conn.p.map.template.id == 9999 && conn.p.map.clan_resource != null) {
                        Service.send_time_cool_down(conn.p, conn.p.map.clan_resource.time,
                                "Thời gian", 2);
                    } else if (conn.p.map.template.id == 81
                            && conn.p.map.map_little_garden != null) { // pho ban khong
                        // lo
                        LittleGarden.send_info(conn.p);
                        Service.send_time_cool_down(conn.p, conn.p.map.map_little_garden.time,
                                "Thời gian", 2);
                    } else if (conn.p.map.template.id == 984) { // pho ban thu thach ve than
                        conn.p.map.update_boat(conn.p, conn.p, false);
                        //
                        Red_Line.init_key_TTVT(conn.p);
                    } else if (conn.p.map.map_pvp != null && conn.p.map.map_pvp.type_map == 1) { // map
                        // sieu
                        // hang
                        conn.p.update_info_to_all();
                    } else if (conn.p.map.map_pvp_clan != null) {
                        activities.PvpClan.send_pvp_clan_score(conn.p, conn.p.map);
                    } else if (conn.p.map.template.id == 119) { // phong cho truy na
                        Wanted.show_table(conn.p);
                        Service.Wanted(conn.p, false);
                        Wanted_Chest.send_box(conn.p);
                    }
                }
                break;
            }
            case 23: {
                if (conn.p != null && !conn.p.isdie) {
                    if (conn.p.ship_pet != null) {
                        Service.send_box_ThongBao_OK(conn.p,
                                "Không thể chuyến khu khi đang chuyển hàng");
                    } else {
                        Service.area_select(conn.p, m);
                    }
                }
                break;
            }
            case 17: {
                if (conn.p != null) {
                    conn.p.map.send_chat(conn.p, m);
                }
                break;
            }
            case -20: {
                if (conn.p != null) {
                    MenuController.process_menu(conn.p, m);
                }
                break;
            }
            case -19: {
                if (conn.p != null) {
                    MenuController.send_menu(conn.p, m);
                }
                break;
            }
            case 2: {
                if (conn.p != null && conn.p.map != null) {
                    conn.p.map.use_skill(conn.p, m);
                }
                break;
            }
            case -70: {
                if (conn.p != null) {
                    conn.p.map.update_num_player_in_map(conn.p);
                }
                break;
            }
            case -45: {// update pk point
                if (conn.p != null) {
                    conn.p.update_point_pk(0);
                    Service.CountDown_Ticket(conn.p);
                    //
                    Service.charWearing(conn.p, conn.p, false);
                }
                break;
            }
            case -33: {
                if (conn.p != null) {
                    Service.rms_process(conn.p, m);
                }
                break;
            }
            case 1: {
                if (conn.p != null && conn.p.map != null) {
                    conn.p.map.send_move(conn.p, m);
                }
                break;
            }
            case 4: {
                if (conn.p != null) {
                    Service.request_mob_in4(conn.p, m);
                }
                break;
            }
            case 48: {
                if (conn.p != null) {
                    Service.send_obj_template(conn.p, m);
                }
                break;
            }
            case -9: {
                if (conn.p == null) {
                    login(m);
                }
                break;
            }
            case -8: {
                conn.create_char(m);
                break;
            }
            case -51: {
                Service.send_icon(m, conn);
                break;
            }
            case -82: {
                conn.ReadPartNew(m);
                break;
            }
            case -38: {
                conn.send_data_from_server(m);
                break;
            }
            case -2: {
                if (conn.user == null && conn.pass == null) {
                    conn.login(m);
                }
                break;
            }
            case -6: {
                conn.Check_Data_Ver();
                break;
            }
            case -7: {
                conn.request_data_update(m);
                break;
            }
        }
    }

    public void login(Message m2) throws IOException {
        short id = m2.reader().readShort();
        // byte type =
        m2.reader().readByte();
        // short idsupport =
        m2.reader().readShort();
        login_into_char_select(id);
    }

    // Đăng nhập tạo thông báo - tanduong - thongbao
    public void login_into_char_select(short id) throws IOException {
        if (conn.list_char != null && id < conn.list_char.size()) {
            String charName = conn.list_char.get(id);
            Player p0 = new Player(conn, charName);
            try {
                if (!p0.setup()) {
                    System.err.println("[LOGIN-ERROR] Player.setup() trả về false cho nhân vật '" + charName + "' - " + p0.setupErrorDetail);
                    conn.login_notice_public("Lỗi dữ liệu '" + charName + "':\n" + (p0.setupErrorDetail != null ? p0.setupErrorDetail : "Không tìm thấy trong DB"));
                    return;
                }
            } catch (Exception e) {
                System.err.println("[LOGIN-ERROR] Exception khi setup nhân vật '" + charName + "': " + e.getClass().getSimpleName() + ": " + e.getMessage());
                e.printStackTrace();
                conn.login_notice_public("Lỗi load nhân vật '" + charName + "':\n" + e.getClass().getSimpleName() + ": " + e.getMessage());
                return;
            }
            p0.setin4();
            Player oldPlayer = Map.get_player_by_name_allmap(p0.name);
            if (oldPlayer != null) {
                if (oldPlayer.conn != null) {
                    oldPlayer.conn.disconnect();
                }
                if (oldPlayer.map != null) {
                    oldPlayer.map.leave_map(oldPlayer, 0);
                }
            }
            conn.p = p0;

            // Reconnect BossHunt check
            activities.BossHunt activeBossHunt = activities.BossHunt.findActiveHunt(conn.p.name);

            // Safety fallback: nếu vẫn đang trong map BossHunt instance thì về làng đã đăng
            // ký phó bản
            if (conn.p.map != null && conn.p.map.map_bossHunt != null) {
                int targetMapId = 1;
                if (activeBossHunt != null) {
                    targetMapId = activeBossHunt.registeredMapId;
                } else if (conn.p.originalMapId > 0) {
                    targetMapId = conn.p.originalMapId;
                } else if (conn.p.id_map_save > 0) {
                    targetMapId = conn.p.id_map_save;
                }
                System.out.println("[BossHunt] Login safety: player " + conn.p.name
                        + " still in BossHunt map, redirecting to map " + targetMapId);
                map.Map[] villageMap = map.Map.get_map_by_id(targetMapId);
                if (villageMap != null && villageMap.length > 0) {
                    conn.p.map = villageMap[0];
                    conn.p.x = 300;
                    conn.p.y = 250;
                }
                conn.p.bossHunt = null;
            }

            // Reconnect Tower Challenge check
            activities.TowerChallenge activeChallenge = activities.TowerChallenge.findActiveChallenge(conn.p.name);
            // Safety fallback: nếu vẫn đang trong map Tower map nhưng no active dungeon
            if (conn.p.map != null && conn.p.map.template.id >= 500 && conn.p.map.template.id <= 512
                    && conn.p.dungeon == null) {
                int targetMapId = conn.p.originalMapId;
                if (targetMapId <= 0) {
                    targetMapId = conn.p.id_map_save;
                }
                if (targetMapId <= 0) {
                    targetMapId = 1;
                }
                System.out.println("[TowerChallenge] Login safety: player " + conn.p.name
                        + " still in Tower map but no active dungeon, redirecting to map " + targetMapId);
                map.Map[] villageMap = map.Map.get_map_by_id(targetMapId);
                if (villageMap != null && villageMap.length > 0) {
                    conn.p.map = villageMap[0];
                    conn.p.x = 300;
                    conn.p.y = 250;
                }
            }
            // Reconnect HangDong check
            activities.HangDong activeHangDong = activities.HangDong.findActive(conn.p.name);
            // Safety fallback: nếu vẫn đang trong các map HangDong/Dungeon (id 167-176)
            // nhưng no active dungeon
            if (conn.p.map != null && conn.p.map.template.id >= 167 && conn.p.map.template.id <= 176
                    && conn.p.dungeon == null) {
                int targetMapId = conn.p.originalMapId;
                if (targetMapId <= 0) {
                    targetMapId = conn.p.id_map_save;
                }
                if (targetMapId <= 0) {
                    targetMapId = 25; // Default fallback to Syrup Village for Single Dungeon
                }
                System.out.println("[HangDong/Dungeon] Login safety: player " + conn.p.name
                        + " still in HangDong/Dungeon map (" + conn.p.map.template.id
                        + ") but no active dungeon, redirecting to map " + targetMapId);
                map.Map[] villageMap = map.Map.get_map_by_id(targetMapId);
                if (villageMap != null && villageMap.length > 0) {
                    conn.p.map = villageMap[0];
                    conn.p.x = 300;
                    conn.p.y = 250;
                }
            }
            // Reconnect Namie check
            activities.NamieTreasureDefense activeDefense = activities.NamieTreasureDefense
                    .findActiveDefense(conn.p.name);
            // Safety fallback: nếu vẫn đang trong map Namie nhưng no active dungeon
            if (conn.p.map != null
                    && activities.NamieTreasureDefense.isDefenseMap(conn.p.map.template.id)
                    && conn.p.dungeon == null) {
                int targetMapId = conn.p.originalMapId;
                if (targetMapId <= 0) {
                    targetMapId = conn.p.id_map_save;
                }
                if (targetMapId <= 0) {
                    targetMapId = 1;
                }
                System.out.println("[NamieDefense] Login safety: player " + conn.p.name
                        + " still in Namie map (" + conn.p.map.template.id
                        + ") but no active dungeon, redirecting to map " + targetMapId);
                map.Map[] villageMap = map.Map.get_map_by_id(targetMapId);
                if (villageMap != null && villageMap.length > 0) {
                    conn.p.map = villageMap[0];
                    conn.p.x = 300;
                    conn.p.y = 250;
                }
                conn.p.dungeon = null;
            }
            // Safety check: nếu out game khi đang trong Map Đấu Trường (2026), Đảo Ruby (1001), Luyện Haki (2000), Đảo Huấn Luyện Pet (2028) thì khi vào lại chuyển về Làng Cối Xay Gió (Map 1)
            if (conn.p.map != null && (conn.p.map.template.id == 2026 || conn.p.map.template.id == 1001
                    || conn.p.map.template.id == 2000 || conn.p.map.template.id == 2028
                    || conn.p.map.template.id == activities.PetTraining.MAP_TRAIN_PET_ID)) {
                System.out.println("[MapRedirect] Login safety: player " + conn.p.name
                        + " logged in while in map (" + conn.p.map.template.id + "), redirecting to Windmill Village (Map 1)");
                map.Map[] villageMap = map.Map.get_map_by_id(1);
                if (villageMap != null && villageMap.length > 0) {
                    conn.p.map = villageMap[0];
                    conn.p.x = 611;
                    conn.p.y = 250;
                }
                conn.p.type_pk = -1;
                if (conn.p.isdie) {
                    conn.p.isdie = false;
                    conn.p.hp = conn.p.body.get_hp_max(true);
                    conn.p.mp = conn.p.body.get_mp_max(true);
                }
            }

            // Safety check: nếu out game khi đang trong Map Đảo Đào Hoa (2027) thì khi vào lại chuyển về Nhà hàng Baratie (Map 33)
            if (conn.p.map != null && (conn.p.map.template.id == 2027 || conn.p.map.map_dao_hoa != null)) {
                System.out.println("[MapRedirect] Login safety: player " + conn.p.name
                        + " logged in while in Dao Dao Hoa (2027), redirecting to Baratie (Map 33)");
                map.Map[] villageMap = map.Map.get_map_by_id(33);
                if (villageMap != null && villageMap.length > 0) {
                    conn.p.map = villageMap[0];
                    conn.p.x = 710;
                    conn.p.y = 320;
                }
                conn.p.type_pk = -1;
                conn.p.isdie = false;
                conn.p.hp = conn.p.body.get_hp_max(true);
                conn.p.mp = conn.p.body.get_mp_max(true);
            }

            // Safety check: nếu out game khi đang trong Map Lôi Đài PK (120, 122, 123) hoặc map_pvp != null
            if (conn.p.map != null && (conn.p.map.template.id == 120 || conn.p.map.template.id == 122
                    || conn.p.map.template.id == 123 || conn.p.map.map_pvp != null)) {
                int targetMapId = conn.p.id_map_save > 0 ? conn.p.id_map_save : 1;
                System.out.println("[PVP Login safety]: player " + conn.p.name
                        + " logged in while in PVP arena map (" + conn.p.map.template.id + "), redirecting to village " + targetMapId);
                map.Map[] villageMap = map.Map.get_map_by_id(targetMapId);
                if (villageMap == null || villageMap.length == 0) {
                    villageMap = map.Map.get_map_by_id(1);
                }
                if (villageMap != null && villageMap.length > 0) {
                    conn.p.map = villageMap[0];
                    conn.p.x = 611;
                    conn.p.y = 250;
                }
                conn.p.type_pk = -1;
                conn.p.targetFight = null;
                if (conn.p.isdie) {
                    conn.p.isdie = false;
                    conn.p.hp = conn.p.body.get_hp_max(true);
                    conn.p.mp = conn.p.body.get_mp_max(true);
                }
            }
            // === hết khối check ===

            Message m = new Message(-7); // update clock
            m.writer().writeByte(17);
            m.writer().writeLong(System.currentTimeMillis());
            conn.addmsg(m);
            m.cleanup();
            //
            Service.UpdateInfoMaincharInfo(conn.p);
            Service.Main_char_Info(conn.p);
            Service.send_info_fashion(conn.p);
            Service.UpdatePvpPoint(conn.p);
            Service.update_PK(conn.p, conn.p, false);
            Service.getThanhTich(conn.p, conn.p);
            conn.p.item.send_maxbag_Inventory();
            // send data map — giờ luôn đúng map (đã redirect nếu cần)
            conn.p.map.goto_map(conn.p);
            //
            Service.update_PK(conn.p, conn.p, true);
            conn.p.item.update_Inventory(-1, true);
            conn.p.item.update_assets_Inventory(true);
            Service.ChestWanted(conn.p, true);
            //
            conn.p.item.send_maxbox_Inventory();
            conn.p.item.update_assets_Box(true);
            conn.p.item.update_Inventory_box(-1, true);
            //
            Quest.send_List_Quest(conn.p, true);
            Quest.update_map_have_side_quest(conn.p, true);
            Service.Weapon_fashion(conn.p, conn.p, true);
            UpgradeItem.send_heart_info(conn.p, true);

            // Re-send party info to ensure the client UI displays it after login
            if (conn.p.party != null) {
                try {
                    conn.p.party.send_info();
                } catch (Exception e) {
                }
            }
            Service.charWearing(conn.p, conn.p, true);
            conn.p.map.send_boat(conn.p, true);
            conn.p.map.update_boat(conn.p, conn.p, true);
            Service.login_ok(conn.p, true);
            Service.Wanted(conn.p, true);
            Clan.send_info(conn.p, true);
            conn.p.item.update_assets_Inventory(true);

            // // Boss Status Announcement
            Message m2 = new Message(18);
            m2.writer().writeUTF("Tin đến");
            m2.writer().writeUTF(
                    "Chào mừng bạn đến với Thế Giới Hải Tặc - 3D, một thế giới game săn boss đầy kịch tính và phần thưởng hấp dẫn! Hãy nhanh chóng tham gia để trải nghiệm những giây phút phiêu lưu đỉnh cao và chinh phục những thử thách khó khăn nhất.");
            conn.p.list_msg_cache.add(m2);

            // === Rejoin dialog for HangDong, TowerChallenge, NamieTreasureDefense,
            // BossHunt ===
            if (activeHangDong != null || activeChallenge != null || activeDefense != null
                    || activeBossHunt != null) {
                final Player player = conn.p;
                new Thread(() -> {
                    try {
                        Thread.sleep(2000);
                        if (player != null && player.conn != null && player.conn.connected) {
                            if (activeHangDong != null) {
                                System.out
                                        .println("[HangDong] Showing delayed rejoin dialog for player: "
                                                + player.name);
                                Service.send_box_yesno(player, 8888, "Thông báo",
                                        "Bạn có muốn vào lại phụ bản Hang Động không?",
                                        new String[] { "Đồng ý", "Hủy" }, new byte[] { -1, -1 });
                            } else if (activeChallenge != null) {
                                System.out.println(
                                        "[TowerChallenge] Showing delayed rejoin dialog for player: "
                                                + player.name);
                                Service.send_box_yesno(player, 8889, "Thông báo",
                                        "Bạn có muốn vào lại phụ bản Vượt ải liên tầng không?",
                                        new String[] { "Đồng ý", "Hủy" }, new byte[] { -1, -1 });
                            } else if (activeDefense != null) {
                                System.out.println(
                                        "[NamieDefense] Showing delayed rejoin dialog for player: " + player.name);
                                Service.send_box_yesno(player, 8890, "Thông báo",
                                        "Bạn có muốn vào lại phụ bản Bảo vệ kho báu Namie không?",
                                        new String[] { "Đồng ý", "Hủy" }, new byte[] { -1, -1 });
                            } else if (activeBossHunt != null) {
                                System.out.println(
                                        "[BossHunt] Showing delayed rejoin dialog for player: " + player.name);
                                Service.send_box_yesno(player, 8891, "Thông báo",
                                        "Bạn có muốn vào lại trận Săn Trùm không?",
                                        new String[] { "Đồng ý", "Hủy" }, new byte[] { -1, -1 });
                            }
                        }
                    } catch (Exception e) {
                        System.err.println(
                                "[Dungeon Rejoin] Error in delayed rejoin dialog thread: " + e.getMessage());
                    }
                }).start();
            }
        }
    }

}
