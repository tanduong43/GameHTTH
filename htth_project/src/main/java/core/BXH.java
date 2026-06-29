package core;

import activities.Friend;
import client.Clan;
import client.Item;
import client.Player;
import database.SQL;
import io.Message;
import map.Map;
import org.json.simple.JSONArray;
import org.json.simple.JSONValue;
import template.*;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
/**
 *
 * @author Truongbk
 */
public class BXH {
    public static List<InfoMemList> CAOTHU = new ArrayList<>();
    public static List<InfoMemList> PVP = new ArrayList<>();
    public static List<InfoMemList> WANTED = new ArrayList<>();

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
                    m.writer().writeShort(-1); // clan icon
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
                    short[] part = new short[] {-1, -1, -1};
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
        }
        p.conn.addmsg(m);
        m.cleanup();
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
                    it[temp2.index] = temp2;
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
                    it[temp2.index] = temp2;
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
        }
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
                    it[temp2.index] = temp2;
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
}
