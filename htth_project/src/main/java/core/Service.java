package core;

import java.io.IOException;
import java.util.List;
import activities.Rebuild_Item;
import client.*;
import io.Message;
import io.Session;
import map.Boss;
import map.Map;
import map.Mob;
import template.*;
/**
 *
 * @author Truongbk
 */
public class Service {
    public static void send_msg_data(Session conn, int cmd, String path, boolean save_cache)
            throws IOException {
        Message m = new Message(cmd);
        m.writer().write(Util.loadfile(path));
        if (save_cache) {
            conn.p.list_msg_cache.add(m);
        } else {
            conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void UpdateInfoMaincharInfo(Player p) throws IOException {
        Message m = new Message(-75);
        int id_f = -1;
        for (int i = 0; i < p.fashion.size(); i++) {
            if (p.fashion.get(i).is_use) {
                id_f = p.fashion.get(i).id;
                break;
            }
        }
        m.writer().writeShort(id_f); // id fashion
        int par_agility = p.body.get_agility(false);
        m.writer().writeShort(par_agility); // giam cooldown skill : % giam hoi chieu
        m.writer().writeShort(p.get_percent_mana_use_skill());
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void Main_char_Info(Player p) throws IOException {
        int hp_max = p.body.get_hp_max(true);
        int mp_max = p.body.get_mp_max(true);
        if (p.hp > hp_max) {
            p.hp = hp_max;
        }
        if (p.mp > mp_max) {
            p.mp = mp_max;
        }
        Message m = new Message(-10);
        m.writer().writeShort(p.index_map);
        m.writer().writeUTF(p.name);
        m.writer().writeInt(hp_max);
        m.writer().writeInt(mp_max);
        m.writer().writeInt(p.hp);
        m.writer().writeInt(p.mp);
        m.writer().writeShort(p.level);
        m.writer().writeShort(p.get_level_percent());
        m.writer().writeShort(p.thongthao);
        if (p.level >= 100) {
            m.writer().writeShort(p.get_level_percent());
        } else {
            m.writer().writeShort(0);
        }
        m.writer().writeInt(BXH.get_rank_wanted(p.name));
        m.writer().writeByte(p.clazz);
        m.writer().writeInt(p.pointPk);
        m.writer().writeShort(p.pointAttribute);
        m.writer().writeByte(p.typePirate);
        m.writer().writeByte(p.indexGhostServer);
        m.writer().writeByte(p.getNumPassive());
        m.writer().writeByte(p.body.get_level_perfect());
        //
        m.writer().writeByte(Body.NameAttribute.length); // size
        int p_, p_pluss;
        for (int i = 0; i < Body.NameAttribute.length; i++) {
            m.writer().writeUTF(Body.NameAttribute[i]);
            p_ = i == 0 ? p.point1
                    : (i == 1 ? p.point2 : (i == 2 ? p.point3 : (i == 3 ? p.point4 : p.point5)));
            p_pluss = p.body.get_point_plus(i + 1);
            m.writer().writeShort(p_);
            m.writer().writeShort(p_pluss);
            int dem = 0;
            int[] par_show = new int[Body.Id[i].length];
            for (int j = 0; j < Body.Id[i].length; j++) {
                switch (Body.Id[i][j]) {
                    case 1: {
                        par_show[j] = Body.Point1_Template_atk[p_ + p_pluss - 1];
                        break;
                    }
                    case 10: {
                        par_show[j] = Body.Point1_Template_crit[p_ + p_pluss - 1];
                        break;
                    }
                    case 13: {
                        par_show[j] = Body.Point1_Template_pierce[p_ + p_pluss - 1];
                        break;
                    }
                    case 4: {
                        par_show[j] = Body.Point2_Template_def[p_ + p_pluss - 1];
                        break;
                    }
                    case 26: {
                        par_show[j] = Body.Point2_Template_resist_physical[p_ + p_pluss - 1];
                        break;
                    }
                    case 27: {
                        par_show[j] = Body.Point2_Template_resist_magic[p_ + p_pluss - 1];
                        break;
                    }
                    case 15: {
                        par_show[j] = Body.Point3_Template_hp[p_ + p_pluss - 1];
                        break;
                    }
                    case 23: {
                        par_show[j] = Body.Point3_Template_hp_potion[p_ + p_pluss - 1];
                        break;
                    }
                    case 11: {
                        par_show[j] = Body.Point4_Template_dame_crit[p_ + p_pluss - 1];
                        break;
                    }
                    case 14: { // tam thoi
                        par_show[j] = Body.Point2_Template_resist_physical[p_ + p_pluss - 1];
                        break;
                    }
                    case 16: {
                        par_show[j] = Body.Point4_Template_mp[p_ + p_pluss - 1];
                        break;
                    }
                    case 12: {
                        if (p_ + p_pluss > 0) {
                            par_show[j] = Body.Point5_Template_miss[p_ + p_pluss - 1];
                        }
                        break;
                    }
                    case 25: {
                        if (p_ + p_pluss > 0) {
                            par_show[j] = Body.Point5_Template_cooldown[p_ + p_pluss - 1];
                        }
                        break;
                    }
                }
                if (par_show[j] > 0) {
                    dem++;
                }
            }
            m.writer().writeByte(dem);
            for (int j = 0; j < Body.Id[i].length; j++) {
                if (par_show[j] > 0) {
                    m.writer().writeByte(Body.Id[i][j]);
                    m.writer().writeInt(par_show[j]);
                }
            }
        }
        //
        m.writer().writeShort(p.pointSkill);
        m.writer().writeByte(10);
        for (int i = 0; i < 10; i++) {
            m.writer().writeByte(1);
            m.writer().writeByte(1);
        }
        byte[] a = new byte[] {0, 1, 2, 3, 4, 25, 15, 16, 26, 27, 17, 18, 10, 11, 12, 13, 14, 19,
                20, 21, 22, 23, 24, 46, 48, 47, 49, 50, 51, 52, 53, 63, 55, 56, 57, 58, 59, 62, 67,
                68, 69, 70, 71, 72, 73, 74, 75, 76};
        int[] b = new int[a.length];
        for (int i = 0; i < b.length; i++) {
            b[i] = p.body.view_in4(a[i]);
        }
        int dem = 0;
        for (int i = 0; i < b.length; i++) {
            if (b[i] > 0 || a[i] == 15 || a[i] == 16 || a[i] == 17 || a[i] == 18 || a[i] == 26
                    || a[i] == 27 || a[i] == 53 || a[i] == 10 || a[i] == 11 || a[i] == 12
                    || a[i] == 2) {
                dem++;
            }
        }
        m.writer().writeByte(dem);
        for (int i = 0; i < a.length; i++) {
            if (b[i] > 0 || a[i] == 15 || a[i] == 16 || a[i] == 17 || a[i] == 18 || a[i] == 26
                    || a[i] == 27 || a[i] == 53 || a[i] == 10 || a[i] == 11 || a[i] == 12
                    || a[i] == 2) {
                m.writer().writeByte(a[i]);
                m.writer().writeInt(b[i]);
            }
        }
        m.writer().writeByte(-1);
        m.writer().writeByte(-1);
        m.writer().writeByte(-1);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void UpdatePvpPoint(Player p) throws IOException {
        Message m = new Message(-66);
        m.writer().writeInt(p.get_pvpPoint());
        m.writer().writeInt(p.pvp_win); // win
        m.writer().writeInt(p.pvp_lose); // lose
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void update_PK(Player p0, Player p, boolean save_cache) throws IOException {
        if (!(p0.map.map_pvp != null || p0.map.template.id == 1000)) {
            if (p0.pointPk >= 400 && p0.type_pk == -1) {
                p0.type_pk = 1;
            }
        }
        Message m = new Message(14);
        m.writer().writeShort(p0.index_map);
        m.writer().writeByte(p0.type_pk); // type pk
        m.writer().writeByte(p0.typePirate); // type pirate
        m.writer().writeByte(p0.is_show_hat ? 0 : 1); // dont show hat
        m.writer().writeShort(-1);
        m.writer().writeByte(p0.is_show_weapon ? 0 : 1); // dont show weaponF
        m.writer().writeByte(0); // type color
        if (save_cache) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void getThanhTich(Player p0, Player p) throws IOException {
        Message m = new Message(65);
        m.writer().writeShort(p0.index_map);
        m.writer().writeByte(0);
        //
        m.writer().writeByte(BXH.get_Thanh_tich_pvp(p0)); // pvp
        m.writer().writeByte(BXH.get_Thanh_tich_level(p0)); // level
        m.writer().writeByte(p0.get_index_full_set());
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void Weapon_fashion(Player p0, Player p, boolean save_cache) throws IOException {
        Message m = new Message(-104);
        m.writer().writeShort(p0.index_map);
        m.writer().writeByte(0);
        m.writer().writeByte(6);
        m.writer().writeShort(p0.head);
        if (save_cache) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void Send_UI_Shop(Player p, int type) throws IOException {
        Message m = new Message(-19);
        m.writer().writeByte(type);
        switch (type) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4: {
                m.writer().writeUTF(Manager.NAME_ITEM_SELL_TEMP[type]);
                m.writer().writeByte(3);
                List<ItemSell> list_sell = ItemSell.get_it_sell(p.level, type);
                m.writer().writeShort(list_sell.size());
                for (int i = 0; i < list_sell.size(); i++) {
                    ItemSell it_sell_temp = list_sell.get(i);
                    ItemTemplate3 it_temp = ItemTemplate3.get_it_by_id(it_sell_temp.id);
                    ItemTemplate3.readUpdateItem(m.writer(), it_temp);
                    m.writer().writeByte(0);
                    m.writer().writeInt(it_sell_temp.price);
                }
                break;
            }
            case 6: {
                m.writer().writeUTF("Shop Nguyên liệu");
                m.writer().writeByte(7);
                byte[] id_sell = ItemSell.get_it_sell_material();
                m.writer().writeShort(id_sell.length);
                for (int i = 0; i < id_sell.length; i++) {
                    m.writer().writeByte(id_sell[i]);
                    m.writer().writeShort(1);
                }
                break;
            }
            case 20: {
                m.writer().writeUTF("Quán ăn");
                m.writer().writeByte(4);
                short[] id_sell = ItemSell.get_it_sell_potion(p);
                m.writer().writeShort(id_sell.length);
                for (int i = 0; i < id_sell.length; i++) {
                    m.writer().writeShort(id_sell[i]);
                    m.writer().writeShort(1);
                }
                break;
            }
            
            case 99: {
                m.writer().writeUTF("Rương đồ");
                m.writer().writeByte(99);
                m.writer().writeShort(0);
                break;
            }
            case 111: {
                m.writer().writeUTF("Shop Đá");
                m.writer().writeByte(4);
                m.writer().writeShort(Rebuild_Item.ID_SELL.length);
                for (int i = 0; i < Rebuild_Item.ID_SELL.length; i++) {
                    m.writer().writeShort(Rebuild_Item.ID_SELL[i]);
                    m.writer().writeShort(1);
                }
                break;
            }
            case 119: {
                m.writer().writeUTF("Thùng Rác");
                m.writer().writeByte(3);
                m.writer().writeShort(p.item.save_item_wear.size());
                for (int i = p.item.save_item_wear.size() - 1; i >= 0; i--) {
                    Item_wear it_select = p.item.save_item_wear.get(i);
                    if (it_select != null) {
                        it_select.index = (byte) i;
                        Item.readUpdateItem(m.writer(), it_select, p);
                        m.writer().writeByte(0);
                        m.writer().writeInt(0);
                    }
                }
                break;
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void charWearing(Player p0, Player p, boolean save_cache) throws IOException {
        Message m = new Message(19);
        m.writer().writeShort(p0.index_map);
        m.writer().writeByte(0);
        m.writer().writeShort(p0.get_head());
        m.writer().writeShort(p0.get_hair());
        m.writer().writeByte(8);
        short[] fashion = p0.get_fashion();
        for (int i = 0; i < 8; i++) {
            Item_wear it_w = p0.item.it_body[i];
            if (i == 6 && p0.item.it_heart != null && p0.item.it_body[7] != null) {
                it_w = p0.item.it_heart;
            }
            if (it_w != null) {
                m.writer().writeByte(1);
                if (p0.index_map == p.index_map) {
                    Item.readUpdateItem(m.writer(), it_w, p0);
                }
                if (i == 1 && !p0.is_show_hat) {
                    m.writer().writeShort(-1);
                } else if (i == 0 && !p0.is_show_weapon) {
                    m.writer().writeShort(-1);
                } else if (fashion != null && fashion[i] != -1) {
                    m.writer().writeShort(fashion[i]);
                } else {
                    m.writer().writeShort(ItemTemplate3.get_it_by_id(it_w.template.id).part);
                }
            } else {
                m.writer().writeByte(0);
                m.writer().writeShort(-1);
            }
        }
        m.writer().writeShort(-1);
        m.writer().writeShort(-1);
        m.writer().writeShort(-1);
        if (save_cache) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void send_icon(Message m, Session conn) {
        if (conn.user != null && conn.pass != null && !conn.user.isEmpty()
                && !conn.pass.isEmpty()) {
            String path = "";
            try {
                short id = m.reader().readShort();
                short id_request = id;
                // System.out.println("request icon " + id);
                if (id_request == 23088 || id_request == 23089 //
                        || id_request == 24424// skill buf trai bong toi
                ) {
                    return;
                }
                if (id_request >= 4094 && id_request <= 4187) { // id icon combo skill
                    id_request += 406;
                }
                if (id_request > 4912 && id_request < 4935) { // icon kich an
                    id_request = 4912;
                }
                Message m2 = new Message(-51);
                m2.writer().writeShort(id);
                path = "data/icon/x" + conn.zoomlv + "/" + id_request + ".png";
                m2.writer().write(Util.loadfile(path));
                conn.addmsg(m2);
                m2.cleanup();
            } catch (IOException e) {
                if (Manager.gI().server_admin) {
                    System.out.println("icon id not found " + path);
                    // Manager.gI().add_icon_fail(path);
                }
            }
        }
    }

    public static void send_obj_template(Player p, Message m) throws IOException {
        byte type = m.reader().readByte();
        short id = m.reader().readShort();
        // System.out.println("type " + type);
        // System.out.println("id " + id);
        if (type == 98) {
            Message m2 = new Message(48);
            m2.writer().writeByte(98);
            m2.writer().writeShort(id);
            m2.writer().write(Util.loadfile("data/template/98/" + id));
            p.conn.addmsg(m2);
            m2.cleanup();
        } else if (type == 1) {
            Message m2 = new Message(48);
            m2.writer().writeByte(1);
            m2.writer().writeShort(id);
            m2.writer().write(Util.loadfile("data/template/1/" + id));
            p.conn.addmsg(m2);
            m2.cleanup();
        } else if (type == 97) {
            // System.out.println("temp97 "+id);
            Message m2 = new Message(48);
            m2.writer().writeByte(97);
            m2.writer().writeShort(id);
            m2.writer().write(Util.loadfile("data/template/97/" + id));
            p.conn.addmsg(m2);
            m2.cleanup();
        } else if (type == 96 && id < DataTemplate.AttriKichAn.length) {
            Message m2 = new Message(48);
            m2.writer().writeByte(96);
            m2.writer().writeByte(id);
            m2.writer().writeUTF(DataTemplate.AttriKichAn[id]);
            p.conn.addmsg(m2);
            m2.cleanup();
        } else if (type == 4) {
            ItemTemplate4 it4 = ItemTemplate4.get_it_by_id(id);
            if (it4 != null) {
                Message m2 = new Message(48);
                m2.writer().writeByte(4);
                m2.writer().writeShort(it4.id);
                m2.writer().writeShort(it4.icon);
                m2.writer().writeUTF(it4.name);
                m2.writer().writeShort(it4.indexInfoPotion);
                m2.writer().writeInt(it4.beri);
                m2.writer().writeShort(it4.ruby);
                m2.writer().writeByte(it4.istrade);
                m2.writer().writeByte(it4.type);
                m2.writer().writeShort(it4.timedelay);
                m2.writer().writeShort(it4.value);
                m2.writer().writeShort(it4.timeactive);
                m2.writer().writeUTF(it4.nameuse);
                p.conn.addmsg(m2);
                m2.cleanup();
            }
        }
    }

    public static void request_mob_in4(Player p, Message m2) throws IOException {
        int id = m2.reader().readShort();
        // System.out.println("request "+id);
        Mob temp = Mob.ENTRYS.get(id);
        if (temp == null && Map.is_map_boss(p.map.template.id)) {
            temp = MapBossInfo.get_mob(p, id);
            // System.out.println("send info map boss");
        }
        if (temp == null && Map.is_map_dungeon(p.map.template.id)) {
            temp = p.dungeon.get_mob(p, id);
        }
        if (temp == null) {
            temp = Boss.get_mob(p, id);
        }
        if (temp == null && p.map.map_little_garden != null) {
            temp = p.map.get_mobs(id, 0);
        }
        if (temp != null && !temp.isdie && temp.map.equals(p.map)) {
            send_mob_info(p, temp);
        }
    }

    public static void send_mob_info(Player p, Mob temp) throws IOException {
        Message m = new Message(4);
        m.writer().writeShort(temp.index);
        m.writer().writeShort(temp.mob_template.mob_id); // id mob
        m.writer().writeShort(temp.x);
        m.writer().writeShort(temp.y);
        m.writer().writeShort(temp.level); // lv
        m.writer().writeInt(temp.hp);
        m.writer().writeInt(temp.hp_max);
        m.writer().writeShort(temp.mob_template.skill[0]);
        m.writer().writeShort(Mob.TIME_RESPAWN); // tgian hs
        m.writer().writeByte(temp.mob_template.typemonster); // type mons
        //
        if (temp.boss_info != null) {
            m.writer().writeByte(temp.boss_info.levelBoss); // lvthongthao
        } else {
            m.writer().writeByte(0); // lvthongthao
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void rms_process(Player p, Message m2) {
        try {
            byte type = m2.reader().readByte();
            byte id = m2.reader().readByte();
            int size = 0;
            try {
                size = m2.reader().readShort();
            } catch (IOException e) {
            }
            // System.out.println("type " + type + " id " + id + " size " + size);
            if (id < 0 || id >= p.rms.length) {
                System.err.println("rms size err");
                return;
            }
            if (type == 0) {
                if (id == 0) {
                    boolean check = false;
                    for (int i = 0; i < DataTemplate.mSea.length; i++) {
                        if (DataTemplate.mSea[i][1] == p.map.template.id) {
                            check = true;
                            break;
                        }
                    }
                    if (check) {
                        return;
                    }
                }
                Message m = new Message(-33);
                m.writer().writeByte(id);
                m.writer().writeShort(p.rms[id].length);
                m.writer().write(p.rms[id]);
                p.conn.addmsg(m);
                m.cleanup();
            } else if (type == 1) {
                if (size > 0) {
                    p.rms[id] = new byte[size];
                    for (int i = 0; i < p.rms[id].length; i++) {
                        p.rms[id][i] = m2.reader().readByte();
                        // System.out.print(p.rms[id][i] + ", ");
                    }
                    // System.out.println();
                }
            }
        } catch (IOException e) {
            // e.printStackTrace();
        }
    }

    public static void area_select(Player p, Message m2) throws IOException {
        // byte type =
        m2.reader().readByte();
        byte select = m2.reader().readByte();
        Map[] map_enter = Map.get_map_by_id(p.map.template.id);
        if (select > -1 && select < map_enter.length) {
            if (map_enter[select].equals(p.map)) {
                Service.send_box_ThongBao_OK(p, "Hiện tại đang ở khu này");
            } else {
                if (map_enter[select].players.size() >= map_enter[select].template.max_player) {
                    Service.send_box_ThongBao_OK(p, "Hiện tại khu vực đã đầy, hãy thử lại sau!");
                } else {
                    if (p.map.template.id == 42 && Boss.BOSS_AREA[0] != -1) {
                        if (Boss.BOSS_AREA[0] == select) {
                            if (!(p.level >= 40 && p.level <= 49)) {
                                select = 0;
                            }
                        }
                    } else if (p.map.template.id == 50 && Boss.BOSS_AREA[1] != -1) {
                        if (Boss.BOSS_AREA[1] == select) {
                            if (!(p.level >= 50 && p.level <= 59)) {
                                select = 0;
                            }
                        }
                    } else if (p.map.template.id == 72 && Boss.BOSS_AREA[2] != -1) {
                        if (Boss.BOSS_AREA[2] == select) {
                            if (!(p.level >= 60 && p.level <= 69)) {
                                select = 0;
                            }
                        }
                    } else if (p.map.template.id == 84 && Boss.BOSS_AREA[3] != -1) {
                        if (Boss.BOSS_AREA[3] == select) {
                            if (!(p.level >= 70 && p.level <= 79)) {
                                select = 0;
                            }
                        }
                    } else if (p.map.template.id == 96 && Boss.BOSS_AREA[4] != -1) {
                        if (Boss.BOSS_AREA[4] == select) {
                            if (!(p.level >= 80 && p.level <= 89)) {
                                select = 0;
                            }
                        }
                    } else if (p.map.template.id == 118 && Boss.BOSS_AREA[5] != -1) {
                        if (Boss.BOSS_AREA[5] == select) {
                            if (p.level < 90) {
                                select = 0;
                            }
                        }
                    }
                    p.map.leave_map(p, 1);
                    p.map = map_enter[select];
                    p.map.enter_map(p);
                    p.map.enter_zone(p);
                }
            }
        }
    }

    public static void pet(Player p0, Player p, boolean save_cache) throws IOException {
        MyPet pet_select = p0.get_pet();
        if (pet_select != null) {
            Message m = new Message(-80);
            m.writer().writeByte(0);
            m.writer().writeShort(0);
            m.writer().writeShort(p0.index_map);
            m.writer().writeShort(pet_select.template.frame); // 977
            m.writer().writeByte(pet_select.template.type); // 5
            if (save_cache) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        } else {
            Message m = new Message(-80);
            m.writer().writeByte(1);
            m.writer().writeShort(-1);
            m.writer().writeShort(p0.index_map);
            if (save_cache) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        }
    }

    public static void login_ok(Player p, boolean save_cache) throws IOException {
        Message m = new Message(-2);
        if (save_cache) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void checkPlayInMap(Player p, Message m2) {
        try {
            // short id_p =
            m2.reader().readShort();
            // System.out.println("player id " + id_p);
        } catch (IOException e) {
            System.err.println("checkPlayInMap faill " + p.map.template.id);
            // e.printStackTrace();
        }
    }

    public static void buy_item(Player p, Message m2) throws IOException {
        byte TypeShop = m2.reader().readByte();
        short id = m2.reader().readShort();
        short value = m2.reader().readShort();
        byte cat = -1;
        if (TypeShop == 116 || TypeShop == 118) {
            cat = m2.reader().readByte();
        }
        if (value <= 0 || value > DataTemplate.MAX_ITEM_IN_BAG) {
            Service.send_box_ThongBao_OK(p, "Số lượng không hợp lệ!");
            return;
        }
        boolean check = false;
        if (cat == -1 && TypeShop >= 0 && TypeShop < 5) {
            List<ItemSell> list_sell = ItemSell.get_it_sell(p.level, TypeShop);
            for (int i = 0; i < list_sell.size(); i++) {
                ItemSell temp_sell = list_sell.get(i);
                if (temp_sell != null && temp_sell.id == id) {
                    if (p.item.able_bag() > 0) {
                        if (p.get_vang() < temp_sell.price) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn không đủ " + temp_sell.price + " beri!");
                            return;
                        }
                        p.update_vang(-temp_sell.price);
                        p.update_money();
                        Item_wear it_add = new Item_wear();
                        it_add.setup_template_by_id(temp_sell.id);
                        if (it_add.template != null) {
                            p.item.add_item_bag3(it_add);
                        }
                        p.item.update_Inventory(-1, false);
                        check = true;
                    } else {
                        Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống!");
                        return;
                    }
                    break;
                }
            }
            //
            if (check) {
                Service.send_box_ThongBao_OK(p,
                        "Mua thành công " + ItemTemplate3.get_it_by_id(id).name);
            } else {
                Service.send_box_ThongBao_OK(p, "Mua thất bại, hãy thử lại!");
            }
        } else if (cat == -1 && TypeShop == 20) {
            if (ItemSell.check_item_sell_potion(id)) {
                if ((p.item.able_bag() < 1 && p.item.total_item_bag_by_id(4, id) == 0)
                        || ((p.item.total_item_bag_by_id(4, id)
                                + value) > DataTemplate.MAX_ITEM_IN_BAG)) {
                    Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống!");
                    return;
                }
                ItemTemplate4 it_template = ItemTemplate4.get_it_by_id(id);
                if (it_template != null) {
                    int vang_req = it_template.ruby * value;
                    if (vang_req > 0) {
                        if (p.get_ngoc() < vang_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " ruby");
                            return;
                        }
                        p.update_ngoc(-vang_req);
                    } else {
                        vang_req = it_template.beri * value;
                        if (vang_req <= 0) {
                            return;
                        }
                        if (p.get_vang() < vang_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " beri");
                            return;
                        }
                        p.update_vang(-vang_req);
                    }
                    //
                    if (id == 43) {
                        p.update_pvp_ticket(value);
                        Service.CountDown_Ticket(p);
                    } else if (id == 40) {
                        p.update_key_boss(value);
                        Service.CountDown_Ticket(p);
                    } else if (id == 6) {
                        p.update_ticket(value);
                        Service.CountDown_Ticket(p);
                    } else {
                        p.item.add_item_bag47(4, id, value);
                        Message m22 = new Message(-64);
                        m22.writer().writeUTF("Mua " + value);
                        p.conn.addmsg(m22);
                        m22.cleanup();
                        //
                        p.item.update_Inventory(-1, false);
                    }
                    p.update_money();
                } else {
                    Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, hãy báo cho admin!");
                }
            }
        } else if (cat == -1 && TypeShop == 6) {
            if (ItemSell.check_item_sell_material(id)) {
                if ((p.item.able_bag() < 1 && p.item.total_item_bag_by_id(7, id) == 0)
                        || ((p.item.total_item_bag_by_id(7, id)
                                + value) > DataTemplate.MAX_ITEM_IN_BAG)) {
                    Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                    return;
                }
                ItemTemplate7 it_template = ItemTemplate7.get_it_by_id(id);
                if (it_template != null) {
                    int vang_req = it_template.priceruby * value;
                    if (vang_req > 0) {
                        if (p.get_ngoc() < vang_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " ruby");
                            return;
                        }
                        p.update_ngoc(-vang_req);
                    } else {
                        vang_req = it_template.price * value;
                        if (vang_req <= 0) {
                            return;
                        }
                        if (p.get_vang() < vang_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " beri");
                            return;
                        }
                        p.update_vang(-vang_req);
                    }
                    p.update_money();
                    p.item.add_item_bag47(7, id, value);
                    Message m22 = new Message(-64);
                    m22.writer().writeUTF("Mua " + value);
                    p.conn.addmsg(m22);
                    m22.cleanup();
                    //
                    p.item.update_Inventory(-1, false);
                } else {
                    Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, hãy báo cho admin!");
                }
            }
        } else if (cat == -1 && TypeShop == 103) {
            ItemHair ith = ItemHair.get_item(id, 103);
            if (ith != null) {
                if (p.check_itfashionP(ith.ID, 103) != null) {
                    Service.send_box_ThongBao_OK(p, "Đã mua rồi!");
                    return;
                }
                if (p.get_ngoc() < 500) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 500 ruby!");
                    return;
                }
                p.update_ngoc(-500);
                p.update_money();
                ItemFashionP temp_new = new ItemFashionP();
                temp_new.category = 103;
                temp_new.id = ith.ID;
                temp_new.icon = ith.idIcon;
                p.itfashionP.add(temp_new);
                p.update_itfashionP(temp_new, 103);
                for (int i = 0; i < p.map.players.size(); i++) {
                    Player p0 = p.map.players.get(i);
                    Service.charWearing(p, p0, false);
                }
                ItemFashionP.show_table(p, 103);
                Service.send_box_ThongBao_OK(p, "Mua thành công " + ith.name);
            } else {
                Service.send_box_ThongBao_OK(p, "Mua thất bại, hãy thử lại!");
            }
        } else if (cat == -1 && TypeShop == 112) {
            ItemHair ith = ItemHair.get_item(id, 108);
            if (ith != null) {
                if (p.check_itfashionP(ith.ID, 108) != null) {
                    Service.send_box_ThongBao_OK(p, "Đã mua rồi!");
                    return;
                }
                if (p.get_ngoc() < 500) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 500 ruby!");
                    return;
                }
                p.update_ngoc(-500);
                p.update_money();
                ItemFashionP temp_new = new ItemFashionP();
                temp_new.category = 108;
                temp_new.id = ith.ID;
                temp_new.icon = ith.idIcon;
                p.itfashionP.add(temp_new);
                p.update_itfashionP(temp_new, 108);
                for (int i = 0; i < p.map.players.size(); i++) {
                    Player p0 = p.map.players.get(i);
                    Service.charWearing(p, p0, false);
                }
                ItemFashionP.show_table(p, 108);
                Service.send_box_ThongBao_OK(p, "Mua thành công " + ith.name);
            } else {
                Service.send_box_ThongBao_OK(p, "Mua thất bại, hãy thử lại!");
            }
        } else if (cat == -1 && TypeShop == 105) {
            ItemFashion itf = ItemFashion.get_item(id);
            if (itf != null) {
                if (itf.price == -1) {
                    Service.send_box_ThongBao_OK(p,
                            "Đồ thời trang " + itf.name + " hiện tại chưa được bán");
                    return;
                }
                if (p.get_ngoc() < itf.price) {
                    Service.send_box_ThongBao_OK(p, "Không đủ " + itf.price + " ruby!");
                    return;
                }
                if (p.check_fashion(itf.ID) != null) {
                    Service.send_box_ThongBao_OK(p, "Đã mua rồi!");
                    return;
                }
                p.update_ngoc(-itf.price);
                p.update_money();
                ItemFashionP2 temp2 = new ItemFashionP2();
                temp2.id = itf.ID;
                p.fashion.add(temp2);
                p.update_fashionP2(temp2);
                for (int i = 0; i < p.map.players.size(); i++) {
                    Player p0 = p.map.players.get(i);
                    Service.charWearing(p, p0, false);
                }
                Service.UpdateInfoMaincharInfo(p);
                ItemFashionP.show_table(p, 105);
                Service.send_box_ThongBao_OK(p, "Mua thành công " + itf.name);
            } else {
                Service.send_box_ThongBao_OK(p, "Mua thất bại, hãy thử lại!");
            }
        } else if (cat == -1 && TypeShop == 111) {
            if ((p.item.able_bag() < 1 && p.item.total_item_bag_by_id(4, id) == 0)
                    || ((p.item.total_item_bag_by_id(4, id)
                            + value) > DataTemplate.MAX_ITEM_IN_BAG)) {
                Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                return;
            }
            for (int i = 0; i < Rebuild_Item.ID_SELL.length; i++) {
                if (Rebuild_Item.ID_SELL[i] == id) {
                    ItemTemplate4 it_temp = ItemTemplate4.get_it_by_id(id);
                    if (it_temp != null) {
                        int vang_req = it_temp.ruby * value;
                        if (vang_req > 0) {
                            if (p.get_ngoc() < vang_req) {
                                Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " ruby");
                                return;
                            }
                            p.update_ngoc(-vang_req);
                        } else {
                            vang_req = it_temp.beri * value;
                            if (vang_req <= 0) {
                                return;
                            }
                            if (p.get_vang() < vang_req) {
                                Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " beri");
                                return;
                            }
                            p.update_vang(-vang_req);
                        }
                        ItemBag47 it = new ItemBag47();
                        switch (id) {
                            case 272: {
                                if (p.item.total_item_bag_by_id(4,
                                        46) >= DataTemplate.MAX_ITEM_IN_BAG) {
                                    p.update_ngoc(vang_req);
                                    Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                                    return;
                                }
                                it.id = 46;
                                it.category = 4;
                                it.quant = (short) (100 * value);
                                break;
                            }
                            case 273: {
                                if (p.item.total_item_bag_by_id(4,
                                        52) >= DataTemplate.MAX_ITEM_IN_BAG) {
                                    p.update_ngoc(vang_req);
                                    Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                                    return;
                                }
                                it.id = 52;
                                it.category = 4;
                                it.quant = (short) (100 * value);
                                break;
                            }
                            case 274: {
                                if (p.item.total_item_bag_by_id(4,
                                        58) >= DataTemplate.MAX_ITEM_IN_BAG) {
                                    p.update_ngoc(vang_req);
                                    Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                                    return;
                                }
                                it.id = 58;
                                it.category = 4;
                                it.quant = (short) (100 * value);
                                break;
                            }
                            case 275: {
                                if (p.item.total_item_bag_by_id(4,
                                        64) >= DataTemplate.MAX_ITEM_IN_BAG) {
                                    p.update_ngoc(vang_req);
                                    Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                                    return;
                                }
                                it.id = 64;
                                it.category = 4;
                                it.quant = (short) (100 * value);
                                break;
                            }
                            case 276: {
                                if (p.item.total_item_bag_by_id(4,
                                        70) >= DataTemplate.MAX_ITEM_IN_BAG) {
                                    p.update_ngoc(vang_req);
                                    Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                                    return;
                                }
                                it.id = 70;
                                it.category = 4;
                                it.quant = (short) (100 * value);
                                break;
                            }
                            case 277: {
                                if (p.item.total_item_bag_by_id(4,
                                        76) >= DataTemplate.MAX_ITEM_IN_BAG) {
                                    p.update_ngoc(vang_req);
                                    Service.send_box_ThongBao_OK(p, "Hành trang đầy!");
                                    return;
                                }
                                it.id = 76;
                                it.category = 4;
                                it.quant = (short) (100 * value);
                                break;
                            }
                            default: {
                                it.id = id;
                                it.category = 4;
                                it.quant = value;
                                break;
                            }
                        }
                        //
                        if (p.item.add_item_bag47(it.category, it.id, it.quant)) {
                            Message m22 = new Message(-64);
                            m22.writer().writeUTF("Mua " + value);
                            p.conn.addmsg(m22);
                            m22.cleanup();
                        } else {
                            Service.send_box_ThongBao_OK(p, "Không thể mua với số lượng này");
                            p.update_ngoc(vang_req);
                        }
                        p.item.update_Inventory(-1, false);
                        p.update_money();
                    } else {
                        Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, hãy báo cho admin!");
                    }
                    break;
                }
            }
        } else if (cat == -1 && TypeShop == 102) {
            ItemBoat itb = ItemBoat.get_item(id);
            if (itb != null) {
                if (p.check_itboat(itb.id) != null) {
                    Service.send_box_ThongBao_OK(p, "Đã mua rồi!");
                    return;
                }
                if (p.get_ngoc() < 5) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 5 ruby!");
                    return;
                }
                p.update_ngoc(-5);
                p.update_money();
                ItemBoatP temp_new = new ItemBoatP();
                temp_new.id = itb.id;
                temp_new.is_use = true;
                p.itemboat.add(temp_new);
                p.update_new_part_boat(temp_new);
                ItemBoat.update_part_boat_when_shopping(p);
                ItemFashionP.show_table(p, 102);
                Service.send_box_ThongBao_OK(p, "Mua thành công " + itb.name);
            } else {
                Service.send_box_ThongBao_OK(p, "Mua thất bại, hãy thử lại!");
            }
        } else if (p.clan != null && TypeShop == 98 && value == 1 && cat == -1 && id >= 0
                && id < 10) {
            p.clan.icon = id;
            Clan.send_info(p, false);
            for (int i = 0; i < p.map.players.size(); i++) {
                if (!p.map.players.get(i).equals(p)) {
                    Clan.send_me_to_other(p, p.map.players.get(i), false);
                }
            }
            Message m = new Message(-52);
            m.writer().writeByte(21);
            m.writer().writeUTF("Đăng ký băng hải tặc " + p.clan.name + " thành công");
            p.conn.addmsg(m);
            m.cleanup();
        } else if (p.clan != null && TypeShop == 97 && value == 1 && cat == -1 && id >= 0
                && id < 402) {
            if (id < 10) {
                p.clan.icon = id;
                for (int i2 = 0; i2 < p.clan.members.size(); i2++) {
                    Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i2).name);
                    if (p0 != null) {
                        Clan.send_info(p0, false);
                        for (int i = 0; i < p0.map.players.size(); i++) {
                            if (!p0.map.players.get(i).equals(p0)) {
                                Clan.send_me_to_other(p0, p.map.players.get(i), false);
                            }
                        }
                    }
                }
                Message m = new Message(-52);
                m.writer().writeByte(21);
                m.writer().writeUTF("Đổi icon băng thành công");
                p.conn.addmsg(m);
                m.cleanup();
            } else {
                int ngoc_quant = Clan.get_ngoc_icon(id);
                if (p.clan.get_ngoc() < ngoc_quant) {
                    Service.send_box_ThongBao_OK(p, "Không đủ " + ngoc_quant + " ruby băng");
                } else {
                    p.clan.update_ruby(-ngoc_quant);
                    p.clan.icon = id;
                    for (int i2 = 0; i2 < p.clan.members.size(); i2++) {
                        Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i2).name);
                        if (p0 != null) {
                            Clan.send_info(p0, false);
                            for (int i = 0; i < p0.map.players.size(); i++) {
                                if (!p0.map.players.get(i).equals(p0)) {
                                    Clan.send_me_to_other(p0, p0.map.players.get(i), false);
                                }
                            }
                        }
                    }
                    Message m = new Message(-52);
                    m.writer().writeByte(21);
                    m.writer().writeUTF("Đổi icon băng thành công");
                    p.conn.addmsg(m);
                    m.cleanup();
                }
            }
        } else if (p.clan != null && p.clan.members.get(0).name.equals(p.name) && TypeShop == 110
                && value <= 20 && value > 0 && cat == -1) {
            check = false;
            for (int i = 0; i < ItemTemplate8.ENTRYS.size(); i++) {
                if (ItemTemplate8.ENTRYS.get(i).id == id) {
                    if (ItemTemplate8.ENTRYS.get(i).ruby > 0) {
                        int vang_req = ItemTemplate8.ENTRYS.get(i).ruby * value;
                        if (p.clan.get_ngoc() < vang_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " ruby băng");
                            return;
                        }
                        p.clan.update_ruby(-vang_req);
                        ItemBag47 it_add = null;
                        for (int j = 0; j < p.clan.list_it.size(); j++) {
                            if (p.clan.list_it.get(j).id == id) {
                                it_add = p.clan.list_it.get(j);
                                break;
                            }
                        }
                        if (it_add == null) {
                            it_add = new ItemBag47();
                            it_add.category = 4;
                            it_add.id = id;
                            it_add.quant = 0;
                            p.clan.list_it.add(it_add);
                        }
                        it_add.quant += value;
                        check = true;
                    } else if (ItemTemplate8.ENTRYS.get(i).beri > 0) {
                        int vang_req = ItemTemplate8.ENTRYS.get(i).beri * value;
                        if (p.clan.get_vang() < vang_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + vang_req + " beri băng");
                            return;
                        }
                        p.clan.update_beri(-vang_req);
                        ItemBag47 it_add = null;
                        for (int j = 0; j < p.clan.list_it.size(); j++) {
                            if (p.clan.list_it.get(j).id == id) {
                                it_add = p.clan.list_it.get(j);
                                break;
                            }
                        }
                        if (it_add == null) {
                            it_add = new ItemBag47();
                            it_add.category = 4;
                            it_add.id = id;
                            it_add.quant = 0;
                            p.clan.list_it.add(it_add);
                        }
                        it_add.quant += value;
                        check = true;
                    } else {
                        Service.send_box_ThongBao_OK(p, "Vật phẩm chưa bán");
                    }
                    break;
                }
            }
            if (check) {
                for (int i = 0; i < p.clan.members.size(); i++) {
                    Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                    if (p0 != null) {
                        Clan.send_money(p0, false);
                        p0.clan.send_inventory(p0, false);
                    }
                }
                Message m22 = new Message(-64);
                m22.writer().writeUTF("Mua " + value);
                p.conn.addmsg(m22);
                m22.cleanup();
            }
        } else if (TypeShop == 118 && value == 1) {
            // System.out.println(cat + " " + id);
            ShopTichLuy temp_shop = ShopTichLuy.get_temp_id(cat, id);
            if (temp_shop != null) {
                if (temp_shop.limit_data.containsKey(p.name)) {
                    int value_old = temp_shop.limit_data.get(p.name);
                    if (value_old >= temp_shop.limit) {
                        Service.send_box_ThongBao_OK(p, "Bạn đã hết lượt đổi vật phẩm này");
                        return;
                    }
                }
                switch (cat) {
                    case 4: {
                        ItemTemplate4 template4 = ItemTemplate4.get_it_by_id(temp_shop.id);
                        if (template4 != null) {
                            int diemtichluy = temp_shop.point * 1_000;
                            if (p.getTichLuy() < diemtichluy) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ " + temp_shop.point + " điểm tích lũy");
                                return;
                            }
                            p.update_TichLuy(-diemtichluy);
                            if (id == 221) {
                                p.item.add_item_bag47(4, id, 10);
                            } else {
                                p.item.add_item_bag47(4, id, 1);
                            }
                            check = true;
                        }
                        break;
                    }
                    case 7: {
                        ItemTemplate7 template7 = ItemTemplate7.get_it_by_id(temp_shop.id);
                        if (template7 != null) {
                            int diemtichluy = temp_shop.point * 1_000;
                            if (p.getTichLuy() < diemtichluy) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ " + temp_shop.point + " điểm tích lũy");
                                return;
                            }
                            p.update_TichLuy(-diemtichluy);
                            p.item.add_item_bag47(7, id, 1);
                            check = true;
                        }
                        break;
                    }
                    case 105: {
                        ItemFashion itf = ItemFashion.get_item(id);
                        if (itf != null) {
                            int diemtichluy = temp_shop.point * 1_000;
                            if (p.getTichLuy() < diemtichluy) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ " + temp_shop.point + " điểm tích lũy");
                                return;
                            }
                            if (p.check_fashion(itf.ID) != null) {
                                Service.send_box_ThongBao_OK(p, "Đã sở hữu thời trang này rồi!");
                                return;
                            }
                            p.update_TichLuy(-diemtichluy);
                            ItemFashionP2 temp2 = new ItemFashionP2();
                            temp2.id = itf.ID;
                            p.fashion.add(temp2);
                            p.update_fashionP2(temp2);
                            for (int i = 0; i < p.map.players.size(); i++) {
                                Player p0 = p.map.players.get(i);
                                Service.charWearing(p, p0, false);
                            }
                            Service.UpdateInfoMaincharInfo(p);
                            check = true;
                        } else {
                            Service.send_box_ThongBao_OK(p, "chưa mở đổi vật phẩm này");
                        }
                        break;
                    }
                }
                if (check) {
                    p.item.update_Inventory(-1, false);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Đổi thành công");
                    //
                    if (!temp_shop.limit_data.containsKey(p.name)) {
                        temp_shop.limit_data.put(p.name, 1);
                    } else {
                        int value_old = temp_shop.limit_data.get(p.name);
                        temp_shop.limit_data.replace(p.name, value_old, (value_old + 1));
                    }
                }
            }
        } else if (TypeShop == 119 && value == 1 && cat == -1 && id >= 0
                && id < p.item.save_item_wear.size()) {
            Item_wear it_select = p.item.save_item_wear.get(id);
            if (it_select != null) {
                if (p.get_ngoc() < 5) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 5 ruby");
                    return;
                }
                if (p.item.able_bag() > 0) {
                    if (p.item.add_item_bag3(it_select)) {
                        p.item.save_item_wear.remove(id);
                        p.update_ngoc(-5);
                        p.update_money();
                        p.item.update_Inventory(-1, false);
                        Service.Send_UI_Shop(p, 119);
                        Service.send_box_ThongBao_OK(p,
                                "Lấy " + it_select.template.name + " về thành công, phí 5 ruby");
                    } else {
                        p.item.remove_item_wear(it_select);
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Hành trang đầy");
                }
            }
        } else if (TypeShop == 116 && id >= 647 && id <= 682 && value == 1 && cat == 4) { // da than
                                                                                          // thoai
            ItemTemplate4 temp4 = null;
            ItemTemplate4 temp4_2 = ItemTemplate4.get_it_by_id(id);
            if (id >= 677 && id <= 682) {
                temp4 = ItemTemplate4.get_it_by_id(id - 309);
            } else {
                temp4 = ItemTemplate4.get_it_by_id(id - 406);
            }
            if (temp4 != null && temp4_2 != null) {
                if (p.item.total_item_bag_by_id(4, temp4.id) >= 3) {
                    if (p.get_ngoc() < 40) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 40 ruby");
                        return;
                    }
                    p.update_ngoc(-40);
                    p.update_money();
                    boolean suc = 100 == Util.random(220);
                    if (suc) {
                        p.item.remove_item47(4, temp4.id, 3);
                        p.item.add_item_bag47(4, id, 1);
                        Service.send_box_ThongBao_OK(p, "Thành công, nhận được 1 " + temp4_2.name);
                    } else {
                        Service.send_box_ThongBao_OK(p,
                                "Rất tiếc việc nâng cấp " + temp4_2.name + " thất bại");
                    }
                    p.item.update_Inventory(-1, false);
                } else {
                    Service.send_box_ThongBao_OK(p, "Không đủ 3 " + temp4.name);
                }
            }
        }
    }

    public static void send_box_ThongBao_OK(Player p, String notice) throws IOException {
        Message m = new Message(-11);
        m.writer().writeShort(0);
        m.writer().writeByte(0);
        m.writer().writeUTF("Thông Báo");
        m.writer().writeUTF(notice);
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void ChestWanted(Player p, boolean save_cache) throws IOException {
        Message m = new Message(-86);
        m.writer().writeByte(1);
        m.writer().writeByte(0);
        if (save_cache) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void use_potion(Player p, int type, int par) throws IOException {
        switch (type) {
            case 0: {
                int hp_max = p.body.get_hp_max(true);
                p.hp += par;
                if (p.hp > hp_max) {
                    p.hp = hp_max;
                }
                Message m = new Message(-83);
                m.writer().writeShort(p.index_map);
                m.writer().writeByte(0);
                m.writer().writeInt(hp_max); // maxhp
                m.writer().writeInt(p.hp); // hp remain
                m.writer().writeInt(par); // dame
                m.writer().writeInt(p.body.get_mp_max(true)); // maxhp
                m.writer().writeInt(p.mp); // hp remain
                m.writer().writeInt(0); // dame
                p.map.send_msg_all_p(m, p, true);
                m.cleanup();
                break;
            }
            case 1: {
                int mp_max = p.body.get_mp_max(true);
                p.mp += par;
                if (p.mp > mp_max) {
                    p.mp = mp_max;
                }
                Message m = new Message(-83);
                m.writer().writeShort(p.index_map);
                m.writer().writeByte(0);
                m.writer().writeInt(p.body.get_hp_max(true)); // maxhp
                m.writer().writeInt(p.hp); // hp remain
                m.writer().writeInt(0); // dame
                m.writer().writeInt(mp_max); // maxhp
                m.writer().writeInt(p.mp); // hp remain
                m.writer().writeInt(par); // dame
                p.map.send_msg_all_p(m, p, true);
                m.cleanup();
                break;
            }
        }
    }

    public static void send_view_other_player(Player p0, Player p) throws IOException {
        Message m = new Message(-42);
        m.writer().writeUTF(p0.name);
        m.writer().writeInt(p0.body.get_hp_max(true));
        m.writer().writeInt(p0.body.get_mp_max(true));
        m.writer().writeInt(p0.hp);
        m.writer().writeInt(p0.mp);
        m.writer().writeShort(p0.level);
        m.writer().writeShort(p0.get_level_percent());
        m.writer().writeShort(p0.head);
        m.writer().writeShort(p0.hair);
        if (p0.clan != null) {
            m.writer().writeShort(p0.clan.id); // clan
            m.writer().writeShort(p0.clan.icon); // clan
            m.writer().writeUTF(p0.clan.name);
        } else {
            m.writer().writeShort(-1); // clan
        }
        m.writer().writeByte(8);
        for (int i = 0; i < 8; i++) {
            Item_wear it_w = p0.item.it_body[i];
            if (i == 6 && p0.item.it_heart != null && p0.item.it_body[7] != null) {
                it_w = p0.item.it_heart;
            } else if (i == 7 && it_w == null && p0.item.it_heart != null) {
                it_w = p0.item.it_heart;
            }
            if (it_w != null) {
                m.writer().writeByte(1);
                Item.readUpdateItem(m.writer(), it_w, p0);
                m.writer().writeShort(ItemTemplate3.get_it_by_id(it_w.template.id).part);
            } else {
                m.writer().writeByte(0);
            }
        }
        m.writer().writeByte(0);
        m.writer().writeShort(-1);
        m.writer().writeByte(p0.get_index_full_set());
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void sell_item(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        short id = m2.reader().readShort();
        byte cat = m2.reader().readByte();
        short num = m2.reader().readShort();
        if (num <= 0 || num > DataTemplate.MAX_ITEM_IN_BAG) {
            return;
        }
        switch (type) {
            case 1: // drop item in bag
            case 0: { // sell item in bag
                switch (cat) {
                    case 3: {
                        if (p.item.bag3[id] != null) {
                            if (p.item.bag3[id].levelup > 0) {
                                Service.send_box_ThongBao_OK(p,
                                        "Vật phẩm đã nâng cấp không thể hủy! Hãy lại npc Johny để tách trang bị này");
                                return;
                            }
                            int vang_recive = 0;
                            if (type == 0) {
                                vang_recive = 30 + (2 * p.item.bag3[id].template.color
                                        + (p.item.bag3[id].template.level / 10) + 1)
                                        * DataTemplate.TabInventory_ItemSell[0];
                                if (vang_recive > DataTemplate.TabInventory_ItemSell[1]) {
                                    vang_recive = DataTemplate.TabInventory_ItemSell[1];
                                }
                                p.update_vang(vang_recive * num);
                                p.update_money();
                            }
                            //
                            p.item.add_item_save(p.item.bag3[id]);
                            p.item.bag3[id] = null;
                            p.item.update_Inventory(-1, false);
                        }
                        break;
                    }
                    case 4:
                    case 7: {
                        p.item.remove_item47(cat, id, num);
                        p.item.update_Inventory(-1, false);
                        int vang_receiv = 0;
                        if (type == 0) {
                            vang_receiv = DataTemplate.TabInventory_ItemSell[2] * num;
                            p.update_vang(vang_receiv);
                            p.update_money();
                        }
                        break;
                    }
                }
                break;
            }
        }
    }

    public static void request_item4_info(Player p, Message m2) throws IOException {
        short id = m2.reader().readShort();
        ItemTemplate4_Info temp = ItemTemplate4_Info.get_by_id(id);
        if (temp != null) {
            Message m = new Message(-105);
            m.writer().writeShort(id);
            m.writer().writeUTF(temp.info);
            p.conn.addmsg(m);
            m.cleanup();
        } else {
            System.out.println("infopotion fail " + id);
        }
    }

    public static void input_text(Player p, int id, String s, String[] s2) throws IOException {
        Message m = new Message(-81);
        m.writer().writeShort(id);
        m.writer().writeUTF(s);
        m.writer().writeByte(s2.length);
        for (int i = 0; i < s2.length; i++) {
            m.writer().writeUTF(s2[i]);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void send_box_yesno(Player p, int type, String title, String text,
            String[] name_cmd, byte[] icon) throws IOException {
        Message m = new Message(-11);
        m.writer().writeShort(type);
        m.writer().writeByte(2);
        m.writer().writeUTF(title);
        m.writer().writeUTF(text);
        m.writer().writeByte(name_cmd.length);
        for (int i = 0; i < name_cmd.length; i++) {
            m.writer().writeUTF(name_cmd[i]);
            m.writer().writeByte(i);
            m.writer().writeByte(icon[i]);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void open_box_item3_orange(Player p, List<Item_wear> list, int id_chest,
            String s1, String s2) throws IOException {
        Message m = new Message(-34);
        m.writer().writeByte(21);
        m.writer().writeShort(id_chest);
        m.writer().writeUTF(s1);
        m.writer().writeUTF(s2);
        m.writer().writeByte(list.size());
        for (int i = 0; i < list.size(); i++) {
            Item_wear temp = list.get(i);
            m.writer().writeByte(3);
            m.writer().writeUTF(temp.template.name);
            m.writer().writeShort(temp.template.icon);
            m.writer().writeInt(1);
            m.writer().writeByte(3);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void CountDown_Ticket(Player p) throws IOException {
        //
        if (p.get_ticket() >= p.get_ticket_max()) {
            p.cd_ticket_next = System.currentTimeMillis();
        }
        if (p.get_pvp_ticket() >= p.get_pvp_ticket_max()) {
            p.cd_pvp_next = System.currentTimeMillis();
        }
        if (p.get_key_boss() >= p.get_key_boss_max()) {
            p.cd_keyboss_next = System.currentTimeMillis();
        }
        // ticket cd
        Message m = new Message(-61);
        m.writer().writeByte(0);
        long cd = (p.cd_ticket_next - System.currentTimeMillis()) / 1000;
        if (cd < 0 || p.get_ticket() == p.get_ticket_max()) {
            cd = 0;
        }
        m.writer().writeInt((int) cd);
        p.conn.addmsg(m);
        m.cleanup();
        // keyboss cd
        m = new Message(-61);
        m.writer().writeByte(1);
        cd = (p.cd_keyboss_next - System.currentTimeMillis()) / 1000;
        if (cd < 0 || p.get_key_boss() == p.get_key_boss_max()) {
            cd = 0;
        }
        m.writer().writeInt((int) cd);
        p.conn.addmsg(m);
        m.cleanup();
        // pvp cd
        m = new Message(-61);
        m.writer().writeByte(2);
        cd = (p.cd_pvp_next - System.currentTimeMillis()) / 1000;
        if (cd < 0 || p.get_pvp_ticket() == p.get_pvp_ticket_max()) {
            cd = 0;
        }
        m.writer().writeInt((int) cd);
        p.conn.addmsg(m);
        m.cleanup();
        // x2 cd
        m = new Message(-61);
        m.writer().writeByte(3);
        EffTemplate eff = p.get_eff(2);
        if (eff != null) {
            cd = (eff.time - System.currentTimeMillis()) / 1000;
            if (cd < 0) {
                cd = 0;
            }
        } else {
            cd = 0;
        }
        m.writer().writeInt((int) cd);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void NewDialog_eat_taq(Player p, String[] name_, int[] icon_, int id)
            throws IOException {
        Message m = new Message(40);
        m.writer().writeByte(1);
        m.writer().writeUTF("");
        m.writer().writeByte(name_.length + 1);
        ItemTemplate4 it_temp = ItemTemplate4.get_it_by_id(id);
        m.writer().writeByte(4);
        m.writer().writeUTF(it_temp.name);
        m.writer().writeShort(it_temp.icon);
        for (int i = 0; i < name_.length; i++) {
            m.writer().writeByte(104);
            m.writer().writeUTF(name_[i]);
            m.writer().writeShort(icon_[i]);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void Help_From_Server(Player p, int num, String text) throws IOException {
        Message m = new Message(-54);
        m.writer().writeShort(num);
        m.writer().writeUTF(text);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void Wanted(Player p, boolean save_cache) throws IOException {
        Message m = new Message(-85);
        m.writer().writeByte(4);
        m.writer().writeInt(p.get_wanted_point());
        if (save_cache) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void start_combo(Player p, int type) throws IOException {
        Message m = new Message(29);
        int size0 = p.list_can_combo.size();
        if (type == 1 && size0 > 0) {
            p.time_combo = System.currentTimeMillis() + 30_000L;
            int size = Util.random(4, 10);
            p.is_combo = new byte[size];
            m.writer().writeByte(p.is_combo.length);
            for (int i = 0; i < p.is_combo.length; i++) {
                Skill_info get_skill = p.list_can_combo.get(Util.random(size0));
                p.is_combo[i] = (byte) get_skill.temp.ID;
                m.writer().writeShort(get_skill.temp.idIcon + 94);
            }
        } else {
            m.writer().writeByte(0);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void send_gift(Player p, int type, String title, String notice,
            List<GiftBox> gift, boolean show_table) throws IOException {
        Message m = new Message(-34);
        m.writer().writeByte(type);
        m.writer().writeUTF(title);
        m.writer().writeUTF(notice);
        m.writer().writeByte(gift.size());
        for (int i = 0; i < gift.size(); i++) {
            GiftBox temp = gift.get(i);
            m.writer().writeByte(temp.type);
            m.writer().writeUTF(temp.name);
            m.writer().writeShort(temp.icon);
            int num_item = temp.num;
            m.writer().writeInt(num_item);
            m.writer().writeByte(temp.color);
            //
            switch (temp.type) {
                case 99: { // xp
                    long exp_receiv = num_item;
                    int buff_percent = 100;
                    if (p.clan != null && p.clan.check_buff(0)) {
                        buff_percent += 50;
                    }
                    if (p.clan != null && p.clan.check_buff(1)) {
                        buff_percent += 50;
                    }
                    if (p.get_eff(2) != null) {
                        buff_percent += 100;
                    }
                    if (p.get_eff(17) != null) {
                        buff_percent += 100;
                    }
                    exp_receiv = (exp_receiv * buff_percent) / 100;
                    p.update_exp(exp_receiv, false);
                    break;
                }
                case 3: {
                    ItemTemplate3 template3 = ItemTemplate3.get_it_by_id(temp.id);
                    if (template3 != null) {
                        Item_wear it_add = new Item_wear();
                        it_add.setup_template_by_id(template3);
                        if (it_add.template != null) {
                            int numLoKham =
                                    (60 > Util.random(120)) ? 0 : ((70 > Util.random(120)) ? 1 : 2);
                            it_add.numLoKham = (byte) numLoKham;
                            p.item.add_item_bag3(it_add);
                            //
                            if (it_add.template.name.equals("Dial Thần Thoại")) {
                                for (int j = 0; j < 2; j++) {
                                    int random_add = Util.random(7);
                                    switch (random_add) {
                                        case 0: {
                                            it_add.option_item.add(new Option(Util.random(5, 10),
                                                    Util.random(5, 10)));
                                            break;
                                        }
                                        case 1: {
                                            it_add.option_item.add(new Option(Util.random(19, 21),
                                                    Util.random(25, 50)));
                                            break;
                                        }
                                        case 2: {
                                            int value_random = Util.random(10, 15);
                                            while (value_random == 11) {
                                                value_random = Util.random(10, 15);
                                            }
                                            it_add.option_item.add(new Option(value_random,
                                                    Util.random(100, 200)));
                                            break;
                                        }
                                        case 3: {
                                            it_add.option_item
                                                    .add(new Option(56, Util.random(100, 200)));
                                            break;
                                        }
                                        case 4: {
                                            it_add.option_item
                                                    .add(new Option(4, Util.random(100, 200)));
                                            break;
                                        }
                                        case 5: {
                                            it_add.option_item
                                                    .add(new Option(53, Util.random(50, 150)));
                                            break;
                                        }
                                        case 6: {
                                            it_add.option_item
                                                    .add(new Option(51, Util.random(30, 80)));
                                            break;
                                        }
                                    }
                                }
                            } else if (it_add.template.name.equals("Dial Truyền thuyết")) {
                                for (int j = 0; j < 3; j++) {
                                    int random_add = Util.random(11);
                                    switch (random_add) {
                                        case 0: {
                                            it_add.option_item.add(new Option(Util.random(5, 10),
                                                    Util.random(5, 10)));
                                            break;
                                        }
                                        case 1: {
                                            it_add.option_item.add(new Option(Util.random(19, 21),
                                                    Util.random(25, 50)));
                                            break;
                                        }
                                        case 2: {
                                            int value_random = Util.random(10, 15);
                                            while (value_random == 11) {
                                                value_random = Util.random(10, 15);
                                            }
                                            it_add.option_item.add(new Option(value_random,
                                                    Util.random(100, 200)));
                                            break;
                                        }
                                        case 3: {
                                            it_add.option_item
                                                    .add(new Option(56, Util.random(100, 200)));
                                            break;
                                        }
                                        case 4: {
                                            it_add.option_item
                                                    .add(new Option(4, Util.random(100, 200)));
                                            break;
                                        }
                                        case 5: {
                                            it_add.option_item
                                                    .add(new Option(53, Util.random(50, 150)));
                                            break;
                                        }
                                        case 6: {
                                            it_add.option_item
                                                    .add(new Option(51, Util.random(30, 80)));
                                            break;
                                        }
                                        case 7: {
                                            it_add.option_item
                                                    .add(new Option(50, Util.random(30, 80)));
                                            break;
                                        }
                                        case 8: {
                                            it_add.option_item
                                                    .add(new Option(52, Util.random(30, 80)));
                                            break;
                                        }
                                        case 9: {
                                            it_add.option_item
                                                    .add(new Option(63, Util.random(30, 80)));
                                            break;
                                        }
                                        case 10: {
                                            it_add.option_item
                                                    .add(new Option(49, Util.random(30, 80)));
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
                case 4: {
                    ItemTemplate4 template4 = ItemTemplate4.get_it_by_id(temp.id);
                    if (template4 != null) {
                        if (template4.id == 0) { // beri
                            p.update_vang(num_item);
                            p.update_money();
                        } else if (template4.id == 1) { // ruby
                            p.update_ngoc(num_item);
                            p.update_money();
                        } else if (template4.id == 6) { // bread
                            p.update_ticket(num_item);
                            p.update_money();
                        } else if (template4.type == -16) { // da hanh trinh
                            ItemBag47 it_select = new ItemBag47();
                            int cat = -1;
                            switch (p.map.template.id) {
                                case 1:
                                case 5: {
                                    cat = 0;
                                    break;
                                }
                                case 9:
                                case 13: {
                                    cat = 1;
                                    break;
                                }
                                case 17:
                                case 21: {
                                    cat = 2;
                                    break;
                                }
                                case 25:
                                case 29: {
                                    cat = 3;
                                    break;
                                }
                                case 33:
                                case 37: {
                                    cat = 4;
                                    break;
                                }
                                case 41:
                                case 45: {
                                    cat = 5;
                                    break;
                                }
                                case 49:
                                case 53: {
                                    cat = 6;
                                    break;
                                }
                                case 69:
                                case 73: {
                                    cat = 7;
                                    break;
                                }
                                case 83:
                                case 87: {
                                    cat = 8;
                                    break;
                                }
                                case 93:
                                case 102: {
                                    cat = 9;
                                    break;
                                }
                                case 113:
                                case 127: {
                                    cat = 10;
                                    break;
                                }
                                case 191:
                                case 198: {
                                    cat = 11;
                                    break;
                                }
                            }
                            it_select.category = (byte) cat;
                            it_select.id = template4.id;
                            it_select.quant = 0;
                            boolean add = true;
                            for (int j = 0; j < p.daHanhTrinh.size(); j++) {
                                if (p.daHanhTrinh.get(j).category == cat
                                        && p.daHanhTrinh.get(j).id == it_select.id) {
                                    add = false;
                                    break;
                                }
                            }
                            if (add) {
                                p.daHanhTrinh.add(it_select);
                            }
                        } else {
                            p.item.add_item_bag47(4, template4.id, num_item);
                        }
                    } else if (temp.id == -10) { // xp clan
                        if (p.clan != null) {
                            p.clan.update_xp(num_item);
                            for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                Player p0 =
                                        Map.get_player_by_name_allmap(p.clan.members.get(i1).name);
                                if (p0 != null) {
                                    Clan.set_data(p0, false);
                                }
                            }
                        }
                    } else if (temp.id == -11) { // beri clan
                        if (p.clan != null) {
                            p.clan.update_beri(num_item);
                            for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                Player p0 =
                                        Map.get_player_by_name_allmap(p.clan.members.get(i1).name);
                                if (p0 != null) {
                                    Clan.send_money(p0, false);
                                }
                            }
                        }
                    } else if (temp.id == -12) { // ruby clan
                        if (p.clan != null) {
                            p.clan.update_ruby(num_item);
                            for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                Player p0 =
                                        Map.get_player_by_name_allmap(p.clan.members.get(i1).name);
                                if (p0 != null) {
                                    Clan.send_money(p0, false);
                                }
                            }
                        }
                    }
                    break;
                }
                case 7: {
                    ItemTemplate7 template7 = ItemTemplate7.get_it_by_id(temp.id);
                    if (template7 != null) {
                        p.item.add_item_bag47(7, template7.id, num_item);
                    }
                    break;
                }
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
        p.item.update_Inventory(-1, false);
    }

    public static void send_eff(Player p, int b, int num) throws IOException {
        // 0 lv up
        // 1 bien hinh
        // 3 thunder to obj
        // 11: open bi ngo
        // 14, 15 green ball
        // 19: exit obj
        // 20 khinh khi cau
        // 21 deny death
        // 22 red thunder
        // 23 firework
        // 24 room heart
        //
        // 100 = 10s
        if (b == 14 || b == 15) {
            num /= 10;
        }
        Message m = new Message(-15);
        m.writer().writeByte(b);
        m.writer().writeShort(p.index_map);
        m.writer().writeByte(0);
        m.writer().writeShort(num);
        p.map.send_msg_all_p(m, p, true);
        m.cleanup();
    }

    public static void send_eff_sword_splash(int id, Player p) throws IOException {
        Player p0 = p.map.get_player_by_id_inmap(id);
        Mob mob = null;
        if (p0 == null) {
            mob = Mob.ENTRYS.get(id);
        }
        if (p0 != null || mob != null) {
            Message m = new Message(-15);
            m.writer().writeByte(5);
            m.writer().writeShort(p.index_map);
            m.writer().writeByte(0);
            m.writer().writeShort(2000);
            //
            m.writer().writeShort(id);
            if (mob != null) {
                m.writer().writeByte(1);
            } else {
                m.writer().writeByte(0);
            }
            //
            p.map.send_msg_all_p(m, p, true);
            m.cleanup();
        }
    }

    public static void send_kich_an(Player p0, Player p, int time_buff, int type, int type_eff,
            int par) throws IOException {
        Message m = new Message(57);
        m.writer().writeByte(type);
        m.writer().writeShort(time_buff);
        m.writer().writeShort(p.index_map);
        m.writer().writeByte(0);
        m.writer().writeByte(0);
        m.writer().writeInt(time_buff * 10);
        //
        m.writer().writeShort(p0.index_map);
        m.writer().writeByte(0);
        m.writer().writeByte(type_eff);
        m.writer().writeInt(par);
        //
        p.map.send_msg_all_p(m, p, true);
        m.cleanup();
    }

    public static void DonotAutoReconnect(Session ss) throws IOException {
        Message m = new Message(-88);
        ss.addmsg(m);
        m.cleanup();
    }

    public static void send_time_cool_down(Player p, long t, String title, int type)
            throws IOException {
        if (type == 0 || type == 2 || type == 3) {
            Message m = new Message(-73);
            m.writer().writeByte(type); // time type
            long time_remain = t - System.currentTimeMillis();
            m.writer().writeShort((short) (time_remain / 1000));
            m.writer().writeUTF(title);
            p.conn.addmsg(m);
            m.cleanup();
        }
    }
}
