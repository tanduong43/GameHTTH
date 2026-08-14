package core;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.json.simple.JSONArray;
import org.json.simple.JSONValue;

import activities.Friend;
import client.Clan;
import client.Item;
import client.Player;
import database.SQL;
import io.Message;
import map.Map;
import template.InfoMemList;
import template.ItemFashion;
import template.ItemFashionP;
import template.ItemFashionP2;
import template.ItemTemplate3;
import template.Item_wear;
import template.Level;

/**
 *
 * @author Truongbk
 */
public class BXH {
    public static List<InfoMemList> CAOTHU = new ArrayList<>();
    public static List<InfoMemList> PVP = new ArrayList<>();
    public static List<InfoMemList> WANTED = new ArrayList<>();
    public static final List<InfoMemList> BOUNTY_HUNTERS = new ArrayList<>();
    public static List<InfoMemList> HANGDONG = new ArrayList<>();
    public static List<InfoMemList> PHAO_HOA = new ArrayList<>();
    public static List<InfoMemList> LAN_KILLS = new ArrayList<>();
    public static List<InfoMemList> DAU_TRUONG = new ArrayList<>();
    public static final java.util.Map<Integer, List<InfoMemList>> TOP_SIEU_TRUM_MAP = new java.util.concurrent.ConcurrentHashMap<>();
    public static final java.util.Map<Integer, List<Integer>> claimedTopBossRewards = new java.util.concurrent.ConcurrentHashMap<>();

    public static void send(Player p, int type, int page) throws IOException {
        if (page < 0) {
            page = 0;
        }
        Message m = new Message(-30);
        switch (type) {
            case 7: {
                int bound1 = 0;
                int bound2 = BXH.PVP.size();
                if (BXH.PVP.size() > 10) {
                    if (((page + 1) * 10) > BXH.PVP.size()) {
                        bound1 = 10 * page;
                        bound2 = BXH.PVP.size();
                        while (bound1 >= bound2) {
                            bound1 -= 10;
                            page--;
                        }
                    } else {
                        bound1 = 10 * page;
                        bound2 = bound1 + 10;
                    }
                } else {
                    page = 0;
                }
                m.writer().writeByte(7);
                m.writer().writeUTF("Top PVP");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    InfoMemList temp = BXH.PVP.get(i);
                    InfoMemList.WriteInfoMemList(m.writer(), temp);
                }
                break;
            }
            case 4: {
                int bound1 = 0;
                int bound2 = BXH.CAOTHU.size();
                if (BXH.CAOTHU.size() > 10) {
                    if (((page + 1) * 10) > BXH.CAOTHU.size()) {
                        bound1 = 10 * page;
                        bound2 = BXH.CAOTHU.size();
                        while (bound1 >= bound2) {
                            bound1 -= 10;
                            page--;
                        }
                    } else {
                        bound1 = 10 * page;
                        bound2 = bound1 + 10;
                    }
                } else {
                    page = 0;
                }
                m.writer().writeByte(4);
                m.writer().writeUTF("Cao Thủ");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    InfoMemList temp = BXH.CAOTHU.get(i);
                    InfoMemList.WriteInfoMemList(m.writer(), temp);
                }
                break;
            }

            case 10: {
                int bound1 = 0;
                int bound2 = BXH.HANGDONG.size();
                if (BXH.HANGDONG.size() > 10) {
                    if (((page + 1) * 10) > BXH.HANGDONG.size()) {
                        bound1 = 10 * page;
                        bound2 = BXH.HANGDONG.size();
                        while (bound1 >= bound2) {
                            bound1 -= 10;
                            page--;
                        }
                    } else {
                        bound1 = 10 * page;
                        bound2 = bound1 + 10;
                    }
                } else {
                    page = 0;
                }
                m.writer().writeByte(10);
                m.writer().writeUTF("Top Hang Động");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    InfoMemList temp = BXH.HANGDONG.get(i);
                    InfoMemList.WriteInfoMemList(m.writer(), temp);
                }
                break;
            }
            case 6: {
                int bound1 = 0;
                int bound2 = Clan.BXH.size();
                if (Clan.BXH.size() > 10) {
                    if (((page + 1) * 10) > Clan.BXH.size()) {
                        bound1 = 10 * page;
                        bound2 = Clan.BXH.size();
                        while (bound1 >= bound2) {
                            bound1 -= 10;
                            page--;
                        }
                    } else {
                        bound1 = 10 * page;
                        bound2 = bound1 + 10;
                    }
                } else {
                    page = 0;
                }
                m.writer().writeByte(6);
                m.writer().writeUTF("Băng Hải Tặc");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    String clan_name = Clan.BXH.get(i);
                    Clan clan = Clan.get_clan_by_name(clan_name);
                    m.writer().writeShort(clan.id);
                    m.writer().writeUTF(clan.name);
                    String info = "TS: %s - Lv: %s + %s";
                    float percent = (clan.xp * 100f) / Clan.get_xp_max(clan.level, clan.trungsinh);
                    if (percent > 100f) {
                        percent = 100f;
                    }
                    m.writer().writeUTF(String.format(info, clan.trungsinh, clan.level,
                            String.format("%.2f", percent)) + "%");
                    m.writer().writeShort(clan.icon); // clan icon
                    m.writer().writeShort(i);
                }
                break;
            }
            case 9: {
                int bound1 = 0;
                int bound2 = WANTED.size() > 10 ? 10 : WANTED.size();
                page = 0;
                m.writer().writeByte(9);
                m.writer().writeUTF("Truy nã");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    InfoMemList temp = BXH.WANTED.get(i);
                    Player p0 = Map.get_player_by_name_allmap(temp.name);
                    short[] part = new short[] { -1, -1, -1 };
                    if (p0 != null) {
                        temp.head = p0.head;
                        temp.hair = p0.hair;
                        temp.hat = p0.get_hat();
                        //
                        if (p0.item.it_body[0] != null) {
                            part[2] = p0.item.it_body[0].template.part;
                        }
                        if (p0.item.it_body[5] != null) {
                            part[1] = p0.item.it_body[5].template.part;
                        }
                        if (p0.item.it_body[3] != null) {
                            part[0] = p0.item.it_body[3].template.part;
                        }
                    }
                    m.writer().writeInt(temp.id);
                    m.writer().writeUTF(temp.name);
                    m.writer().writeShort(temp.head);
                    m.writer().writeShort(temp.hair);
                    m.writer().writeShort(temp.hat);
                    //
                    m.writer().writeShort(part[0]); // body
                    m.writer().writeShort(part[1]); // leg
                    m.writer().writeShort(part[2]); // weapon
                    //
                    m.writer().writeInt(i); // rank
                    m.writer().writeInt((int) temp.thongthao); // wanted point
                }
                break;
            }
            case 12: {
                updateTopPhaoHoa();
                int bound1 = 0;
                int bound2 = BXH.PHAO_HOA.size();
                if (BXH.PHAO_HOA.size() > 10) {
                    if (((page + 1) * 10) > BXH.PHAO_HOA.size()) {
                        bound1 = 10 * page;
                        bound2 = BXH.PHAO_HOA.size();
                        while (bound1 >= bound2) {
                            bound1 -= 10;
                            page--;
                        }
                    } else {
                        bound1 = 10 * page;
                        bound2 = bound1 + 10;
                    }
                } else {
                    page = 0;
                }
                m.writer().writeByte(4);
                m.writer().writeUTF("Top Pháo Hoa");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    InfoMemList temp = BXH.PHAO_HOA.get(i);
                    InfoMemList.WriteInfoMemList(m.writer(), temp);
                }
                break;
            }
            case 13: {
                updateTopLanKills();
                int bound1 = 0;
                int bound2 = BXH.LAN_KILLS.size();
                if (BXH.LAN_KILLS.size() > 10) {
                    if (((page + 1) * 10) > BXH.LAN_KILLS.size()) {
                        bound1 = 10 * page;
                        bound2 = BXH.LAN_KILLS.size();
                        while (bound1 >= bound2) {
                            bound1 -= 10;
                            page--;
                        }
                    } else {
                        bound1 = 10 * page;
                        bound2 = bound1 + 10;
                    }
                } else {
                    page = 0;
                }
                m.writer().writeByte(4);
                m.writer().writeUTF("Top Giết Lân");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    InfoMemList temp = BXH.LAN_KILLS.get(i);
                    InfoMemList.WriteInfoMemList(m.writer(), temp);
                }
                break;
            }
            case 14: {
                updateTopDauTruong();
                int bound1 = 0;
                int bound2 = BXH.DAU_TRUONG.size();
                if (BXH.DAU_TRUONG.size() > 10) {
                    if (((page + 1) * 10) > BXH.DAU_TRUONG.size()) {
                        bound1 = 10 * page;
                        bound2 = BXH.DAU_TRUONG.size();
                        while (bound1 >= bound2) {
                            bound1 -= 10;
                            page--;
                        }
                    } else {
                        bound1 = 10 * page;
                        bound2 = bound1 + 10;
                    }
                } else {
                    page = 0;
                }
                m.writer().writeByte(4);
                m.writer().writeUTF("Top Đấu Trường");
                m.writer().writeByte(page);
                m.writer().writeByte(bound2 - bound1);
                for (int i = bound1; i < bound2; i++) {
                    InfoMemList temp = BXH.DAU_TRUONG.get(i);
                    InfoMemList.WriteInfoMemList(m.writer(), temp);
                }
                break;
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void send_wanted_list(Player p, byte page) throws IOException {
        Message msg = new Message(-89);
        int bound1 = 0;
        int bound2 = BXH.BOUNTY_HUNTERS.size() > 10 ? 10 : BXH.BOUNTY_HUNTERS.size();
        if (BXH.BOUNTY_HUNTERS.size() > 10) {
            if (((page + 1) * 10) > BXH.BOUNTY_HUNTERS.size()) {
                bound1 = 10 * page;
                bound2 = BXH.BOUNTY_HUNTERS.size();
                while (bound1 >= bound2) {
                    bound1 -= 10;
                    page--;
                }
            } else {
                bound1 = 10 * page;
                bound2 = bound1 + 10;
            }
        } else {
            page = 0;
        }
        msg.writer().writeByte(0); // type 0 trong ListWantedServer
        msg.writer().writeShort(bound2 - bound1);

        for (int i = bound1; i < bound2; i++) {
            InfoMemList temp = BXH.BOUNTY_HUNTERS.get(i);
            Player p0 = Map.get_player_by_name_allmap(temp.name);
            short[] part = new short[] { -1, -1, -1 };
            if (p0 != null) {
                temp.head = p0.head;
                temp.hair = p0.hair;
                temp.hat = p0.get_hat();
                if (p0.item.it_body[0] != null) {
                    part[2] = p0.item.it_body[0].template.part;
                }
                if (p0.item.it_body[5] != null) {
                    part[1] = p0.item.it_body[5].template.part;
                }
                if (p0.item.it_body[3] != null) {
                    part[0] = p0.item.it_body[3].template.part;
                }
            }
            msg.writer().writeShort(1); // num in ReadInfoMemWantedWarrant (bỏ qua/không dùng nhiều ở client)
            msg.writer().writeUTF(temp.name);
            msg.writer().writeInt((int) temp.thongthao); // wanted
            msg.writer().writeUTF(temp.name); // charShow.name
            msg.writer().writeShort(1); // Lv (có thể lấy từ temp nếu có)

            // updateCharFace
            msg.writer().writeShort(temp.head);
            msg.writer().writeShort(temp.hair);
            msg.writer().writeShort(temp.hat);

            // infoMemList.typeOnline
            msg.writer().writeByte(p0 != null ? 1 : 0);
        }
        p.conn.addmsg(msg);
        msg.cleanup();
    }

    public static void resetAllTopBoss() {
        TOP_SIEU_TRUM_MAP.clear();
        claimedTopBossRewards.clear();
    }

    public static void updateTopBoss(map.Boss boss) {
        if (boss == null || boss.mob == null || boss.mob.mob_template == null) {
            return;
        }
        if (boss.TopDame == null || boss.TopDame.isEmpty()) {
            return;
        }
        int mobId = boss.mob.mob_template.mob_id;
        List<template.Top_Dame> sortedList = Util.sort(boss.TopDame);
        List<InfoMemList> listAdd = new ArrayList<>();
        for (int i = 0; i < sortedList.size(); i++) {
            template.Top_Dame td = sortedList.get(i);
            InfoMemList temp = new InfoMemList();
            temp.name = td.name;
            Player p0 = Map.get_player_by_name_allmap(td.name);
            if (p0 != null) {
                temp.id = p0.id;
                temp.level = (short) p0.level;
                temp.head = p0.head;
                temp.hair = p0.hair;
                temp.hat = p0.get_hat();
            } else {
                temp.id = i + 1;
                temp.level = 1;
                temp.head = -1;
                temp.hair = -1;
                temp.hat = -1;
            }
            temp.info = "Dame: " + Util.number_format(td.dame);
            temp.rank = (short) i;
            listAdd.add(temp);
        }
        TOP_SIEU_TRUM_MAP.put(mobId, listAdd);
    }

    public static void sendTopBoss(Player p, int mobId, int page) throws IOException {
        if (page < 0) {
            page = 0;
        }
        if (map.Boss.ENTRYS != null) {
            for (int i = 0; i < map.Boss.ENTRYS.size(); i++) {
                map.Boss b = map.Boss.ENTRYS.get(i);
                if (b != null && b.mob != null && b.mob.mob_template != null && b.mob.mob_template.mob_id == mobId) {
                    if (b.TopDame != null && !b.TopDame.isEmpty()) {
                        updateTopBoss(b);
                    }
                    break;
                }
            }
        }
        List<InfoMemList> listBossTop = TOP_SIEU_TRUM_MAP.get(mobId);
        if (listBossTop == null) {
            listBossTop = new ArrayList<>();
        }
        int bound1 = 0;
        int bound2 = listBossTop.size();
        if (listBossTop.size() > 10) {
            if (((page + 1) * 10) > listBossTop.size()) {
                bound1 = 10 * page;
                bound2 = listBossTop.size();
                while (bound1 >= bound2) {
                    bound1 -= 10;
                    page--;
                }
            } else {
                bound1 = 10 * page;
                bound2 = bound1 + 10;
            }
        } else {
            page = 0;
        }
        Message m = new Message(-30);
        m.writer().writeByte(4);
        template.MobTemplate mobT = template.MobTemplate.ENTRYS.get(mobId);
        String bossName = (mobT != null) ? mobT.name : ("Boss " + mobId);
        m.writer().writeUTF("Top " + bossName);
        m.writer().writeByte(page);
        m.writer().writeByte(bound2 - bound1);
        for (int i = bound1; i < bound2; i++) {
            InfoMemList temp = listBossTop.get(i);
            InfoMemList.WriteInfoMemList(m.writer(), temp);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void updateTopPhaoHoa() {
        List<InfoMemList> list_add = new ArrayList<>();
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `id`, `name`, `level`, `clazz`, `num_phao_hoa`, `body`, `it_body`, `fashion`, `site` FROM `players` WHERE `num_phao_hoa` > 0 ORDER BY `num_phao_hoa` DESC LIMIT 10;");
            rs = ps.executeQuery();
            while (rs.next()) {
                InfoMemList temp = new InfoMemList();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                try {
                    JSONArray jsLevel = (JSONArray) JSONValue.parse(rs.getString("level"));
                    if (jsLevel != null && jsLevel.size() > 0) {
                        temp.level = Short.parseShort(jsLevel.get(0).toString());
                    } else {
                        temp.level = 1;
                    }
                } catch (Exception e) {
                    temp.level = 1;
                }
                temp.thongthao = rs.getInt("num_phao_hoa");
                List<ItemFashionP2> fashion = new ArrayList<>();
                List<ItemFashionP> itfashionP = new ArrayList<>();
                JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP tempf = new ItemFashionP();
                    tempf.category = Byte.parseByte(js_temp.get(0).toString());
                    tempf.id = Short.parseShort(js_temp.get(1).toString());
                    tempf.icon = Short.parseShort(js_temp.get(2).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                    itfashionP.add(tempf);
                }
                js_temp_2.clear();
                js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP2 tempf = new ItemFashionP2();
                    tempf.id = Short.parseShort(js_temp.get(0).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                    fashion.add(tempf);
                }
                js0.clear();
                short hair_ = -1;
                short head_ = -1;
                short[] fashion_ = null;
                for (int i0 = 0; i0 < fashion.size(); i0++) {
                    if (fashion.get(i0).is_use) {
                        ItemFashion tempF = ItemFashion.get_item(fashion.get(i0).id);
                        if (tempF != null) {
                            fashion_ = tempF.mWearing;
                            break;
                        }
                    }
                }
                if (fashion_ != null && fashion_[6] != -1) {
                    hair_ = -2;
                    head_ = fashion_[6];
                } else {
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                            hair_ = itfashionP.get(i0).icon;
                        }
                    }
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                            head_ = itfashionP.get(i0).icon;
                        }
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                temp.head = (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString());
                temp.hair = (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString());
                js.clear();
                //
                Item_wear[] it = new Item_wear[8];
                js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                for (int i1 = 0; i1 < js.size(); i1++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                    Item_wear temp2 = new Item_wear();
                    Item.readUpdateItem(js2.toString(), temp2);
                    if (temp2.template != null) {
                        it[temp2.index] = temp2;
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("site"));
                boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                js.clear();
                if (!is_show_hat || it[1] == null) {
                    temp.hat = -1;
                } else if (fashion_ != null && fashion_[1] != -1) {
                    temp.hat = fashion_[1];
                } else {
                    temp.hat = ItemTemplate3.get_it_by_id(it[1].template.id).part;
                }
                temp.info = String.format("Đã bắn: %s", Util.number_format(temp.thongthao));
                list_add.add(temp);
            }
        } catch (Exception e) {
            e.printStackTrace();
            list_add.clear();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        if (list_add.size() > 0) {
            for (int i = 0; i < list_add.size(); i++) {
                list_add.get(i).rank = (short) i;
            }
            BXH.PHAO_HOA.clear();
            BXH.PHAO_HOA.addAll(list_add);
            list_add.clear();
        }
    }

    public static void sendTopPhaoHoa(Player p, int page) throws IOException {
        send(p, 12, page);
    }

    public static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        byte idlist = m2.reader().readByte();
        byte page = m2.reader().readByte();
        // System.out.println(type);
        // System.out.println(idlist);
        // System.out.println(page);
        switch (type) {
            case 2: {
                if (idlist == 2 && page == 0) { // dsach den
                    Message m = new Message(-30);
                    m.writer().writeByte(2);
                    m.writer().writeUTF("Kẻ Thù");
                    m.writer().writeByte(0);
                    m.writer().writeByte(p.enemy_list.size());
                    for (int i = 0; i < p.enemy_list.size(); i++) {
                        Friend.ReadInfoMemList(m.writer(), p.enemy_list.get(i));
                    }
                    p.conn.addmsg(m);
                    m.cleanup();
                }
                break;
            }
            case 3: {
                BXH.send(p, idlist, page);
                break;
            }
        }
    }

    public static void update() {
        updateCaoThu();
        updatePVP();
        updateWanted();
        updateThoSanBounty();
        updateHangDong();
        updateTopLanKills();
        updateTopDauTruong();
    }

    private static void updateWanted() {
        List<InfoMemList> list_add = new ArrayList<>();
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `id`, `name`, `clazz`, `wanted_point`, `body`, `it_body`, `fashion`, `site` FROM `players` WHERE `wanted_point` > 0 ORDER BY `wanted_point` DESC LIMIT 50;");
            rs = ps.executeQuery();
            while (rs.next()) {
                InfoMemList temp = new InfoMemList();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                temp.thongthao = rs.getInt("wanted_point");
                List<ItemFashionP2> fashion = new ArrayList<>();
                List<ItemFashionP> itfashionP = new ArrayList<>();
                JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP tempf = new ItemFashionP();
                    tempf.category = Byte.parseByte(js_temp.get(0).toString());
                    tempf.id = Short.parseShort(js_temp.get(1).toString());
                    tempf.icon = Short.parseShort(js_temp.get(2).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                    itfashionP.add(tempf);
                }
                js_temp_2.clear();
                js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP2 tempf = new ItemFashionP2();
                    tempf.id = Short.parseShort(js_temp.get(0).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                    fashion.add(tempf);
                }
                js0.clear();
                short hair_ = -1;
                short head_ = -1;
                short[] fashion_ = null;
                for (int i0 = 0; i0 < fashion.size(); i0++) {
                    if (fashion.get(i0).is_use) {
                        ItemFashion tempF = ItemFashion.get_item(fashion.get(i0).id);
                        if (tempF != null) {
                            fashion_ = tempF.mWearing;
                            break;
                        }
                    }
                }
                if (fashion_ != null && fashion_[6] != -1) {
                    hair_ = -2;
                    head_ = fashion_[6];
                } else {
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                            hair_ = itfashionP.get(i0).icon;
                        }
                    }
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                            head_ = itfashionP.get(i0).icon;
                        }
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                temp.head = (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString());
                temp.hair = (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString());
                js.clear();
                //
                Item_wear[] it = new Item_wear[8];
                js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                for (int i1 = 0; i1 < js.size(); i1++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                    Item_wear temp2 = new Item_wear();
                    Item.readUpdateItem(js2.toString(), temp2);
                    if (temp2.template != null) {
                        it[temp2.index] = temp2;
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("site"));
                boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                js.clear();
                if (!is_show_hat || it[1] == null) {
                    temp.hat = -1;
                } else if (fashion_ != null && fashion_[1] != -1) {
                    temp.hat = fashion_[1];
                } else {
                    temp.hat = ItemTemplate3.get_it_by_id(it[1].template.id).part;
                }
                list_add.add(temp);
            }
        } catch (SQLException e) {
            e.printStackTrace();
            list_add.clear();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        if (list_add.size() > 0) {
            for (int i = 0; i < list_add.size(); i++) {
                list_add.get(i).rank = (short) i;
            }
            BXH.WANTED.clear();
            BXH.WANTED.addAll(list_add);
            list_add.clear();
            System.out.println("BXH Wanted loaded/updated:");
            for (int i = 0; i < Math.min(10, BXH.WANTED.size()); i++) {
                System.out.println("Top " + (i + 1) + ": " + BXH.WANTED.get(i).name);
            }
        }
    }

    private static void updatePVP() {
        List<InfoMemList> list_add = new ArrayList<>();
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `id`, `name`, `clazz`, `pvppoint`, `body`, `it_body`, `fashion`, `site` FROM `players` WHERE `pvppoint` > 0 ORDER BY `pvppoint` DESC LIMIT 50;");
            rs = ps.executeQuery();
            while (rs.next()) {
                InfoMemList temp = new InfoMemList();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                temp.thongthao = rs.getInt("pvppoint");
                List<ItemFashionP2> fashion = new ArrayList<>();
                List<ItemFashionP> itfashionP = new ArrayList<>();
                JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP tempf = new ItemFashionP();
                    tempf.category = Byte.parseByte(js_temp.get(0).toString());
                    tempf.id = Short.parseShort(js_temp.get(1).toString());
                    tempf.icon = Short.parseShort(js_temp.get(2).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                    itfashionP.add(tempf);
                }
                js_temp_2.clear();
                js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP2 tempf = new ItemFashionP2();
                    tempf.id = Short.parseShort(js_temp.get(0).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                    fashion.add(tempf);
                }
                js0.clear();
                short hair_ = -1;
                short head_ = -1;
                short[] fashion_ = null;
                for (int i0 = 0; i0 < fashion.size(); i0++) {
                    if (fashion.get(i0).is_use) {
                        ItemFashion tempF = ItemFashion.get_item(fashion.get(i0).id);
                        if (tempF != null) {
                            fashion_ = tempF.mWearing;
                            break;
                        }
                    }
                }
                if (fashion_ != null && fashion_[6] != -1) {
                    hair_ = -2;
                    head_ = fashion_[6];
                } else {
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                            hair_ = itfashionP.get(i0).icon;
                        }
                    }
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                            head_ = itfashionP.get(i0).icon;
                        }
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                temp.head = (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString());
                temp.hair = (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString());
                js.clear();
                //
                Item_wear[] it = new Item_wear[8];
                js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                for (int i1 = 0; i1 < js.size(); i1++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                    Item_wear temp2 = new Item_wear();
                    Item.readUpdateItem(js2.toString(), temp2);
                    if (temp2.template != null) {
                        it[temp2.index] = temp2;
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("site"));
                boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                js.clear();
                if (!is_show_hat || it[1] == null) {
                    temp.hat = -1;
                } else if (fashion_ != null && fashion_[1] != -1) {
                    temp.hat = fashion_[1];
                } else {
                    temp.hat = ItemTemplate3.get_it_by_id(it[1].template.id).part;
                }
                temp.info = String.format("Điểm: %s", temp.thongthao);
                list_add.add(temp);
            }
        } catch (SQLException e) {
            e.printStackTrace();
            list_add.clear();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        if (list_add.size() > 0) {
            for (int i = 0; i < list_add.size(); i++) {
                list_add.get(i).rank = (short) i;
            }
            BXH.PVP.clear();
            BXH.PVP.addAll(list_add);
            list_add.clear();
            System.out.println("BXH PVP loaded/updated:");
            for (int i = 0; i < Math.min(10, BXH.PVP.size()); i++) {
                System.out.println("Top " + (i + 1) + ": " + BXH.PVP.get(i).name);
            }
        }
    }

    private static void updateHangDong() {
        List<InfoMemList> list_add = new ArrayList<>();
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `id`, `name`, `clazz`, `hangdong_stage`, `body`, `it_body`, `fashion`, `site` FROM `players` WHERE `hangdong_stage` > 0 ORDER BY `hangdong_stage` DESC LIMIT 50;");
            rs = ps.executeQuery();
            while (rs.next()) {
                InfoMemList temp = new InfoMemList();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                temp.thongthao = rs.getInt("hangdong_stage");
                List<ItemFashionP2> fashion = new ArrayList<>();
                List<ItemFashionP> itfashionP = new ArrayList<>();
                JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP tempf = new ItemFashionP();
                    tempf.category = Byte.parseByte(js_temp.get(0).toString());
                    tempf.id = Short.parseShort(js_temp.get(1).toString());
                    tempf.icon = Short.parseShort(js_temp.get(2).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                    itfashionP.add(tempf);
                }
                js_temp_2.clear();
                js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP2 tempf = new ItemFashionP2();
                    tempf.id = Short.parseShort(js_temp.get(0).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                    fashion.add(tempf);
                }
                js0.clear();
                short hair_ = -1;
                short head_ = -1;
                short[] fashion_ = null;
                for (int i0 = 0; i0 < fashion.size(); i0++) {
                    if (fashion.get(i0).is_use) {
                        ItemFashion tempF = ItemFashion.get_item(fashion.get(i0).id);
                        if (tempF != null) {
                            fashion_ = tempF.mWearing;
                            break;
                        }
                    }
                }
                if (fashion_ != null && fashion_[6] != -1) {
                    hair_ = -2;
                    head_ = fashion_[6];
                } else {
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                            hair_ = itfashionP.get(i0).icon;
                        }
                    }
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                            head_ = itfashionP.get(i0).icon;
                        }
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                temp.head = (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString());
                temp.hair = (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString());
                js.clear();
                //
                Item_wear[] it = new Item_wear[8];
                js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                for (int i1 = 0; i1 < js.size(); i1++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                    Item_wear temp2 = new Item_wear();
                    Item.readUpdateItem(js2.toString(), temp2);
                    if (temp2.template != null) {
                        it[temp2.index] = temp2;
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("site"));
                boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                js.clear();
                if (!is_show_hat || it[1] == null) {
                    temp.hat = -1;
                } else if (fashion_ != null && fashion_[1] != -1) {
                    temp.hat = fashion_[1];
                } else {
                    temp.hat = ItemTemplate3.get_it_by_id(it[1].template.id).part;
                }
                temp.info = String.format("Tầng: %s", temp.thongthao);
                list_add.add(temp);
            }
        } catch (SQLException e) {
            e.printStackTrace();
            list_add.clear();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        if (list_add.size() > 0) {
            for (int i = 0; i < list_add.size(); i++) {
                list_add.get(i).rank = (short) i;
                list_add.get(i).id = i + 1;
            }
            BXH.HANGDONG.clear();
            BXH.HANGDONG.addAll(list_add);
            list_add.clear();
            System.out.println("BXH Hang Động loaded/updated:");
            for (int i = 0; i < Math.min(10, BXH.HANGDONG.size()); i++) {
                System.out.println("Top " + (i + 1) + ": " + BXH.HANGDONG.get(i).name);
            }
        }
    }

    private static void updateTopLanKills() {
        List<InfoMemList> list_add = new ArrayList<>();
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `id`, `name`, `clazz`, `lan_kills`, `body`, `it_body`, `fashion`, `site` FROM `players` WHERE `lan_kills` > 0 ORDER BY `lan_kills` DESC LIMIT 50;");
            rs = ps.executeQuery();
            while (rs.next()) {
                InfoMemList temp = new InfoMemList();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                temp.thongthao = rs.getInt("lan_kills");
                List<ItemFashionP2> fashion = new ArrayList<>();
                List<ItemFashionP> itfashionP = new ArrayList<>();
                JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP tempf = new ItemFashionP();
                    tempf.category = Byte.parseByte(js_temp.get(0).toString());
                    tempf.id = Short.parseShort(js_temp.get(1).toString());
                    tempf.icon = Short.parseShort(js_temp.get(2).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                    itfashionP.add(tempf);
                }
                js_temp_2.clear();
                js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP2 tempf = new ItemFashionP2();
                    tempf.id = Short.parseShort(js_temp.get(0).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                    fashion.add(tempf);
                }
                js0.clear();
                short hair_ = -1;
                short head_ = -1;
                short[] fashion_ = null;
                for (int i0 = 0; i0 < fashion.size(); i0++) {
                    if (fashion.get(i0).is_use) {
                        ItemFashion tempF = ItemFashion.get_item(fashion.get(i0).id);
                        if (tempF != null) {
                            fashion_ = tempF.mWearing;
                            break;
                        }
                    }
                }
                if (fashion_ != null && fashion_[6] != -1) {
                    hair_ = -2;
                    head_ = fashion_[6];
                } else {
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                            hair_ = itfashionP.get(i0).icon;
                        }
                    }
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                            head_ = itfashionP.get(i0).icon;
                        }
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                temp.head = (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString());
                temp.hair = (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString());
                js.clear();
                //
                Item_wear[] it = new Item_wear[8];
                js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                for (int i1 = 0; i1 < js.size(); i1++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                    Item_wear temp2 = new Item_wear();
                    Item.readUpdateItem(js2.toString(), temp2);
                    if (temp2.template != null) {
                        it[temp2.index] = temp2;
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("site"));
                boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                js.clear();
                if (!is_show_hat || it[1] == null) {
                    temp.hat = -1;
                } else if (fashion_ != null && fashion_[1] != -1) {
                    temp.hat = fashion_[1];
                } else {
                    temp.hat = ItemTemplate3.get_it_by_id(it[1].template.id).part;
                }
                temp.info = String.format("Giết Lân: %s", Util.number_format(temp.thongthao));
                list_add.add(temp);
            }
        } catch (SQLException e) {
            e.printStackTrace();
            list_add.clear();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        if (list_add.size() > 0) {
            for (int i = 0; i < list_add.size(); i++) {
                list_add.get(i).rank = (short) i;
                list_add.get(i).id = i + 1;
            }
            BXH.LAN_KILLS.clear();
            BXH.LAN_KILLS.addAll(list_add);
            list_add.clear();
            System.out.println("BXH Lan Kills loaded/updated:");
            for (int i = 0; i < Math.min(10, BXH.LAN_KILLS.size()); i++) {
                System.out.println("Top " + (i + 1) + ": " + BXH.LAN_KILLS.get(i).name);
            }
        }
    }

    public static void updateTopDauTruong() {
        List<InfoMemList> list_add = new ArrayList<>();
        List<Player> playersInMatch = event.EventTet.getInstance().getDauTruongPlayers();
        if (playersInMatch != null && !playersInMatch.isEmpty()) {
            List<Player> sorted = new ArrayList<>(playersInMatch);
            sorted.sort((p1, p2) -> Integer.compare(p2.dauTruongKills, p1.dauTruongKills));
            for (int i = 0; i < sorted.size(); i++) {
                Player p0 = sorted.get(i);
                if (p0 == null || p0.dauTruongKills <= 0) continue;
                InfoMemList temp = new InfoMemList();
                temp.id = p0.id;
                temp.name = p0.name;
                temp.level = (short) p0.level;
                temp.head = (short) p0.get_head();
                temp.hair = (short) p0.get_hair();
                temp.hat = p0.get_hat();
                temp.info = "Hạ gục: " + p0.dauTruongKills;
                temp.rank = (short) list_add.size();
                list_add.add(temp);
            }
        } else {
            List<event.EventTet.DauTruongTopRecord> lastRecords = event.EventTet.getInstance().getLastMatchTopList();
            if (lastRecords != null && !lastRecords.isEmpty()) {
                for (int i = 0; i < lastRecords.size(); i++) {
                    event.EventTet.DauTruongTopRecord rec = lastRecords.get(i);
                    InfoMemList temp = new InfoMemList();
                    Player p0 = Map.get_player_by_name_allmap(rec.name);
                    if (p0 != null) {
                        temp.id = p0.id;
                        temp.name = p0.name;
                        temp.level = (short) p0.level;
                        temp.head = (short) p0.get_head();
                        temp.hair = (short) p0.get_hair();
                        temp.hat = p0.get_hat();
                    } else {
                        temp.id = i + 1;
                        temp.name = rec.name;
                        temp.level = 1;
                        temp.head = -1;
                        temp.hair = -1;
                        temp.hat = -1;
                    }
                    temp.info = "Hạ gục: " + rec.kills;
                    temp.rank = (short) i;
                    list_add.add(temp);
                }
            }
        }
        BXH.DAU_TRUONG.clear();
        BXH.DAU_TRUONG.addAll(list_add);
    }

    private static void updateCaoThu() {
        List<InfoMemList> list_add = new ArrayList<>();
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `id`, `name`, `clazz`, `level`, `body`, `it_body`, `fashion`, `site` FROM `players` ORDER BY `exp` DESC LIMIT 50;");
            rs = ps.executeQuery();
            while (rs.next()) {
                InfoMemList temp = new InfoMemList();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("level"));
                temp.level = Short.parseShort(js0.get(0).toString());
                long exp = Long.parseLong(js0.get(1).toString());
                temp.thongthao = Short.parseShort(js0.get(2).toString());
                List<ItemFashionP2> fashion = new ArrayList<>();
                List<ItemFashionP> itfashionP = new ArrayList<>();
                js0.clear();
                js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP tempf = new ItemFashionP();
                    tempf.category = Byte.parseByte(js_temp.get(0).toString());
                    tempf.id = Short.parseShort(js_temp.get(1).toString());
                    tempf.icon = Short.parseShort(js_temp.get(2).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                    itfashionP.add(tempf);
                }
                js_temp_2.clear();
                js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP2 tempf = new ItemFashionP2();
                    tempf.id = Short.parseShort(js_temp.get(0).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                    fashion.add(tempf);
                }
                js0.clear();
                short hair_ = -1;
                short head_ = -1;
                short[] fashion_ = null;
                for (int i0 = 0; i0 < fashion.size(); i0++) {
                    if (fashion.get(i0).is_use) {
                        ItemFashion tempF = ItemFashion.get_item(fashion.get(i0).id);
                        if (tempF != null) {
                            fashion_ = tempF.mWearing;
                            break;
                        }
                    }
                }
                if (fashion_ != null && fashion_[6] != -1) {
                    hair_ = -2;
                    head_ = fashion_[6];
                } else {
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                            hair_ = itfashionP.get(i0).icon;
                        }
                    }
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                            head_ = itfashionP.get(i0).icon;
                        }
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                temp.head = (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString());
                temp.hair = (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString());
                js.clear();
                //
                Item_wear[] it = new Item_wear[8];
                js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                for (int i1 = 0; i1 < js.size(); i1++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                    Item_wear temp2 = new Item_wear();
                    Item.readUpdateItem(js2.toString(), temp2);
                    if (temp2.template != null) {
                        it[temp2.index] = temp2;
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("site"));
                boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                js.clear();
                if (!is_show_hat || it[1] == null) {
                    temp.hat = -1;
                } else if (fashion_ != null && fashion_[1] != -1) {
                    temp.hat = fashion_[1];
                } else {
                    temp.hat = ItemTemplate3.get_it_by_id(it[1].template.id).part;
                }
                float percent;
                if (temp.level >= 100) {
                    percent = ((float) exp * 100) / Level.LEVEL_THONGTHAO[(int) temp.thongthao];
                } else {
                    percent = ((float) exp * 100) / Level.ENTRYS[temp.level - 1].exp;
                }
                temp.info = String.format("Cấp %s + %s - TT: %s", temp.level,
                        (String.format("%.2f", percent) + "%"), temp.thongthao);
                list_add.add(temp);
            }
        } catch (SQLException e) {
            e.printStackTrace();
            list_add.clear();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        if (list_add.size() > 0) {
            for (int i = 0; i < list_add.size(); i++) {
                list_add.get(i).rank = (short) i;
            }
            BXH.CAOTHU.clear();
            BXH.CAOTHU.addAll(list_add);
            list_add.clear();
            System.out.println("BXH Cao Thủ loaded/updated:");
            for (int i = 0; i < Math.min(10, BXH.CAOTHU.size()); i++) {
                System.out.println("Top " + (i + 1) + ": " + BXH.CAOTHU.get(i).name);
            }
        }
    }

    public static int get_Thanh_tich_level(Player p) {
        try {
            for (int i = 0; i < BXH.CAOTHU.size(); i++) {
                if (i > 9) {
                    break;
                }
                if (BXH.CAOTHU.get(i).name.equals(p.name)) {
                    if (i == 0 || i == 1 || i == 2) {
                        return i;
                    } else {
                        return 3;
                    }
                }
            }
        } catch (Exception e) {
        }
        return -1;
    }

    public static int get_Thanh_tich_pvp(Player p) {
        try {
            for (int i = 0; i < BXH.PVP.size(); i++) {
                if (i > 9) {
                    break;
                }
                if (BXH.PVP.get(i).name.equals(p.name)) {
                    if (i == 0 || i == 1 || i == 2) {
                        return i;
                    } else {
                        return 3;
                    }
                }
            }
        } catch (Exception e) {
        }
        return -1;
    }

    public static int get_rank_wanted(String name) {
        try {
            for (int i = 0; i < BXH.WANTED.size(); i++) {
                if (BXH.WANTED.get(i).name.equals(name)) {
                    return i;
                }
            }
        } catch (Exception e) {
        }
        return -1;
    }

    public static void updateThoSanBounty() {
        List<InfoMemList> list_add = new ArrayList<>();
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `id`, `name`, `clazz`, `thosan_bounty`, `body`, `it_body`, `fashion`, `site` FROM `players` WHERE `thosan_bounty` > 0 ORDER BY `thosan_bounty` DESC LIMIT 50;");
            rs = ps.executeQuery();
            while (rs.next()) {
                InfoMemList temp = new InfoMemList();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                temp.thongthao = rs.getInt("thosan_bounty");
                List<ItemFashionP2> fashion = new ArrayList<>();
                List<ItemFashionP> itfashionP = new ArrayList<>();
                JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP tempf = new ItemFashionP();
                    tempf.category = Byte.parseByte(js_temp.get(0).toString());
                    tempf.id = Short.parseShort(js_temp.get(1).toString());
                    tempf.icon = Short.parseShort(js_temp.get(2).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                    itfashionP.add(tempf);
                }
                js_temp_2.clear();
                js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                    JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                    ItemFashionP2 tempf = new ItemFashionP2();
                    tempf.id = Short.parseShort(js_temp.get(0).toString());
                    tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                    fashion.add(tempf);
                }
                js0.clear();
                short hair_ = -1;
                short head_ = -1;
                short[] fashion_ = null;
                for (int i0 = 0; i0 < fashion.size(); i0++) {
                    if (fashion.get(i0).is_use) {
                        ItemFashion tempF = ItemFashion.get_item(fashion.get(i0).id);
                        if (tempF != null) {
                            fashion_ = tempF.mWearing;
                            break;
                        }
                    }
                }
                if (fashion_ != null && fashion_[6] != -1) {
                    hair_ = -2;
                    head_ = fashion_[6];
                } else {
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                            hair_ = itfashionP.get(i0).icon;
                        }
                    }
                    for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                        if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                            head_ = itfashionP.get(i0).icon;
                        }
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                temp.head = (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString());
                temp.hair = (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString());
                js.clear();
                //
                Item_wear[] it = new Item_wear[8];
                js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                for (int i1 = 0; i1 < js.size(); i1++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                    Item_wear temp2 = new Item_wear();
                    Item.readUpdateItem(js2.toString(), temp2);
                    if (temp2.template != null) {
                        it[temp2.index] = temp2;
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("site"));
                boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                js.clear();
                if (!is_show_hat || it[1] == null) {
                    temp.hat = -1;
                } else if (fashion_ != null && fashion_[1] != -1) {
                    temp.hat = fashion_[1];
                } else {
                    temp.hat = ItemTemplate3.get_it_by_id(it[1].template.id).part;
                }
                list_add.add(temp);
            }
        } catch (SQLException e) {
            e.printStackTrace();
            list_add.clear();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        if (list_add.size() > 0) {
            for (int i = 0; i < list_add.size(); i++) {
                list_add.get(i).rank = (short) i;
            }
            BXH.BOUNTY_HUNTERS.clear();
            BXH.BOUNTY_HUNTERS.addAll(list_add);
            list_add.clear();
            System.out.println("BXH Bounty Hunters loaded/updated:");
            for (int i = 0; i < Math.min(10, BXH.BOUNTY_HUNTERS.size()); i++) {
                System.out.println("Top " + (i + 1) + ": " + BXH.BOUNTY_HUNTERS.get(i).name);
            }
        }
    }

}
