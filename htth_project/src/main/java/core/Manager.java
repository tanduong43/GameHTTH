package core;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.IOException;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.*;
import activities.*;
import client.*;
import event.TaiXiu;
import map.*;
import map.Map;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;
import database.SQL;
import io.Message;
import template.*;
/**
 *
 * @author Truongbk
 */
public class Manager {
    private static Manager instance;
    public static String[] NAME_ITEM_SELL_TEMP =
            new String[] {"Shop Trang Bị Võ Sĩ", "Shop Trang Bị Kiếm Khách",
                    "Shop Trang Bị Đầu Bếp", "Shop Trang Bị Hoa Tiêu", "Shop Trang Bị Xạ Thủ"};
    public boolean debug;
    public String mysql_host;
    public String mysql_database;
    public String mysql_user;
    public String mysql_pass;
    public int server_port;
    public int exp;
    public boolean server_admin;
    private int index_mob;
    private TaiXiu tx;
    private static int a = 0;

    public static Manager gI() {
        if (instance == null) {
            instance = new Manager();
        }
        return instance;
    }

    public void init() {
        index_mob = 1;
        try {
            load_config();
            // load msg data
            ByteArrayInputStream bais = new ByteArrayInputStream(Util.loadfile("data/msg/hair"));
            DataInputStream dis = new DataInputStream(bais);
            // load_hair(dis, 103);
            dis.close();
            bais.close();
            //
            bais = new ByteArrayInputStream(Util.loadfile("data/msg/head"));
            dis = new DataInputStream(bais);
            load_hair(dis, 108);
            dis.close();
            bais.close();
            // load da than thoai
            DaThanThoai.data_shop = Util.loadfile("data/msg/dathanthoaishop");
        } catch (Exception e) {
            System.out.println("config load err!");
            System.exit(0);
        }
        load_database();
        start_service();
    }

    private void start_service() {
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                if (!Map.is_map_boss(map.template.id) && !Map.is_map_dungeon(map.template.id)) {
                    map.start_map();
                }
            }
        }
        // load static class
        tx = new TaiXiu();
        a = Rebuild_Item.ID_SELL.length;
        a = Red_Line.KEY0.length;
        a = UpgradeItem.DATA.size();
        a = Body.Point3_Template_hp.length;
        a = ItemBoat.ENTRYS.size();
        a = ItemSell.ENTRYS.size();
        a = VongQuay.ID_ITEM.length;
        a = Level.ENTRYS.length;
        a = Skill_info.EXP.length;
        //
        System.out.println("Start Service OK, " + a);
    }

    private void stop_service() {
        //
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                map.stop_map();
            }
        }
        tx.close();
        for (int i = 0; i < Map.get_map_plus().size(); i++) {
            Map.get_map_plus().get(i).stop_map();
        }
    }

    private void load_hair(DataInputStream dis, int type) throws IOException {
        dis.readByte();
        dis.readUTF();
        dis.readByte();
        int n = dis.readShort();
        for (int i = 0; i < n; i++) {
            ItemHair temp = ItemHair.readUpdateItemHair(dis);
            temp.type = (byte) type;
            ItemHair.ENTRYS.add(temp);
        }
    }

    private void load_database() {
        Connection conn = null;
        Statement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.createStatement();
            // load mobs
            String query = "SELECT * FROM `mobs`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                MobTemplate temp = new MobTemplate();
                temp.mob_id = Short.parseShort(rs.getString("id"));
                temp.name = rs.getString("name");
                temp.level = Short.parseShort(rs.getString("level"));
                temp.hp_max = Integer.parseInt(rs.getString("hp"));
                temp.hOne = Short.parseShort(rs.getString("hOne"));
                temp.typemove = Byte.parseByte(rs.getString("typemove"));
                temp.ishuman = Byte.parseByte(rs.getString("ishuman"));
                temp.typemonster = Byte.parseByte(rs.getString("typemonster"));
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("idicon"));
                if (temp.ishuman == 0) {
                    temp.icon = Short.parseShort(js.get(1).toString());
                } else if (temp.ishuman == 1) {
                    temp.head = Short.parseShort(js.get(1).toString());
                    temp.hair = Short.parseShort(js.get(2).toString());
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(3).toString());
                    temp.wearing = new short[js2.size()];
                    for (int i = 0; i < temp.wearing.length; i++) {
                        temp.wearing[i] = Short.parseShort(js2.get(i).toString());
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("skill"));
                temp.skill = new short[js.size()];
                for (int i = 0; i < temp.skill.length; i++) {
                    temp.skill[i] = Short.parseShort(js.get(i).toString());
                }
                js.clear();
                MobTemplate.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load mob ok");
            query = "SELECT * FROM `shoptichluy` ORDER BY `type`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ShopTichLuy temp = new ShopTichLuy();
                temp.id = rs.getShort("id");
                temp.type = rs.getByte("type");
                temp.point = rs.getInt("point");
                temp.info = rs.getString("info");
                temp.limit = rs.getInt("limit");
                temp.limit_data = new HashMap<>();
                JSONArray jsar = (JSONArray) JSONValue.parse(rs.getString("limit_data"));
                for (int i = 0; i < jsar.size(); i++) {
                    JSONArray js_in = (JSONArray) jsar.get(i);
                    int value = Integer.parseInt(js_in.get(1).toString());
                    temp.limit_data.put(js_in.get(0).toString(), value);
                }
                ShopTichLuy.ENTRY.add(temp);
            }
            rs.close();
            
            // load map
            query = "SELECT * FROM `maps`;";
            rs = ps.executeQuery(query);
            MapTemplate.ENTRYS = new ArrayList<>();
            while (rs.next()) {
                short id_map = rs.getShort("id");
                File f = new File("data/map/" + id_map);
                if (!f.exists()) {
                    continue;
                }
                //
                MapTemplate map_temp = new MapTemplate();
                map_temp.id = id_map;
                map_temp.name = rs.getString("name");
                map_temp.max_zone = rs.getByte("maxzone");
                map_temp.max_player = rs.getByte("maxplayer");
                // npc
                JSONArray js_npc = (JSONArray) JSONValue.parse(rs.getString("npcs"));
                map_temp.npcs = new ArrayList<>();
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_npc_temp = (JSONArray) JSONValue.parse(js_npc.get(i).toString());
                    Npc npc = new Npc();
                    npc.iditem = Short.parseShort(js_npc_temp.get(0).toString());
                    npc.name = js_npc_temp.get(1).toString();
                    npc.namegt = js_npc_temp.get(2).toString();
                    npc.chat = js_npc_temp.get(3).toString();
                    npc.x = Short.parseShort(js_npc_temp.get(4).toString());
                    npc.y = Short.parseShort(js_npc_temp.get(5).toString());
                    npc.isPerson = Byte.parseByte(js_npc_temp.get(6).toString());
                    npc.typeIcon = Byte.parseByte(js_npc_temp.get(7).toString());
                    npc.wBlock = Byte.parseByte(js_npc_temp.get(8).toString());
                    npc.hBlock = Byte.parseByte(js_npc_temp.get(9).toString());
                    npc.b3 = Byte.parseByte(js_npc_temp.get(10).toString());
                    JSONArray js_npc_temp_2 =
                            (JSONArray) JSONValue.parse(js_npc_temp.get(11).toString());
                    npc.dataFrame = new byte[js_npc_temp_2.size()];
                    for (int j = 0; j < npc.dataFrame.length; j++) {
                        npc.dataFrame[j] = Byte.parseByte(js_npc_temp_2.get(j).toString());
                    }
                    npc.head = Short.parseShort(js_npc_temp.get(12).toString());
                    npc.hair = Short.parseShort(js_npc_temp.get(13).toString());
                    JSONArray js_npc_temp_3 =
                            (JSONArray) JSONValue.parse(js_npc_temp.get(14).toString());
                    npc.wearing = new short[js_npc_temp_3.size()];
                    for (int k = 0; k < npc.wearing.length; k++) {
                        npc.wearing[k] = Short.parseShort(js_npc_temp_3.get(k).toString());
                    }
                    map_temp.npcs.add(npc);
                }
                js_npc.clear();
                js_npc = (JSONArray) JSONValue.parse(rs.getString("boat"));
                map_temp.list_boat = new ArrayList<>();
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_temp = (JSONArray) js_npc.get(i);
                    Boat_In_Map temp_boat = new Boat_In_Map();
                    temp_boat.x = Short.parseShort(js_temp.get(0).toString());
                    temp_boat.y = Short.parseShort(js_temp.get(1).toString());
                    map_temp.list_boat.add(temp_boat);
                }
                js_npc.clear();
                map_temp.vgos = new ArrayList<>();
                js_npc = (JSONArray) JSONValue.parse(rs.getString("vgos"));
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_0 = (JSONArray) js_npc.get(i);
                    Vgo vgo_temp = new Vgo();
                    vgo_temp.id_map_go = Short.parseShort(js_0.get(0).toString());
                    vgo_temp.xold = Short.parseShort(js_0.get(1).toString());
                    vgo_temp.yold = Short.parseShort(js_0.get(2).toString());
                    vgo_temp.xnew = Short.parseShort(js_0.get(3).toString());
                    vgo_temp.ynew = Short.parseShort(js_0.get(4).toString());
                    if (vgo_temp.id_map_go != -1) {
                        map_temp.vgos.add(vgo_temp);
                    }
                }
                js_npc.clear();
                map_temp.type_view_p = rs.getByte("typeViewPlayer");
                map_temp.b = rs.getByte("b");
                map_temp.specMap = rs.getByte("specMap");
                js_npc = (JSONArray) JSONValue.parse(rs.getString("data"));
                map_temp.data = new byte[2][];
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_in = (JSONArray) js_npc.get(i);
                    map_temp.data[i] = new byte[js_in.size()];
                    for (int j = 0; j < map_temp.data[i].length; j++) {
                        map_temp.data[i][j] = Byte.parseByte(js_in.get(j).toString());
                    }
                }
                js_npc.clear();
                //System.out.println(id_map);
                js_npc = (JSONArray) JSONValue.parse(rs.getString("MapBack"));
                map_temp.IDBack = Byte.parseByte(js_npc.get(0).toString());
                map_temp.HBack = Short.parseShort(js_npc.get(1).toString());
                map_temp.maxW = Short.parseShort(js_npc.get(2).toString());
                map_temp.maxH = Short.parseShort(js_npc.get(3).toString());
                js_npc.clear();
                map_temp.id_eff_map = rs.getByte("id_eff_map");
                map_temp.level = rs.getByte("level");
                map_temp.typeChangeMap = rs.getByte("typeChangeMap");
                js_npc = (JSONArray) JSONValue.parse(rs.getString("mPosMapTrain"));
                map_temp.mPosMapTrain = new byte[js_npc.size()][];
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_in = (JSONArray) js_npc.get(i);
                    map_temp.mPosMapTrain[i] = new byte[js_in.size()];
                    for (int j = 0; j < map_temp.mPosMapTrain[i].length; j++) {
                        map_temp.mPosMapTrain[i][j] = Byte.parseByte(js_in.get(j).toString());
                    }
                }
                js_npc.clear();
                map_temp.strTimeChange = rs.getString("strTimeChange");
                MapTemplate.ENTRYS.add(map_temp);
                //
                String mob_json = rs.getString("mobs");
                Map[] m_temp = new Map[map_temp.max_zone];
                for (int i2 = 0; i2 < m_temp.length; i2++) {
                    m_temp[i2] = new Map();
                    m_temp[i2].zone_id = (byte) i2;
                    m_temp[i2].template = map_temp;
                    JSONArray js = (JSONArray) JSONValue.parse(mob_json);
                    m_temp[i2].list_mob = new int[js.size()];
                    for (int i = 0; i < js.size(); i++) {
                        JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                        Mob temp = new Mob();
                        temp.mob_template =
                                MobTemplate.ENTRYS.get(Integer.parseInt(js2.get(0).toString()));
                        temp.x = Short.parseShort(js2.get(1).toString());
                        temp.y = Short.parseShort(js2.get(2).toString());
                        temp.hp_max = temp.mob_template.hp_max;
                        temp.hp = temp.hp_max;
                        temp.level = temp.mob_template.level;
                        temp.isdie = false;
                        temp.id_target = -1;
                        temp.index = this.index_mob;
                        temp.map = m_temp[i2];
                        temp.boss_info = null;
                        Mob.ENTRYS.put(this.index_mob, temp);
                        m_temp[i2].list_mob[i] = this.index_mob;
                        this.index_mob++;
                    }
                }
                Map.ENTRYS.add(m_temp);
            }
            rs.close();
            for (int i = 0; i < MapTemplate.ENTRYS.size(); i++) {
                for (int j = 0; j < MapTemplate.ENTRYS.get(i).vgos.size(); j++) {
                    Vgo vgo = MapTemplate.ENTRYS.get(i).vgos.get(j);
                    vgo.map_go = Map.get_map_by_id(vgo.id_map_go);
                    if (vgo.map_go == null) {
                        vgo.map_go = Map.get_map_by_id(1);
                    }
                }
            }
            System.out.println("load map ok");
            // load part
            query = "SELECT * FROM `parts`;";
            Part.ENTRY = new ArrayList<>();
            rs = ps.executeQuery(query);
            while (rs.next()) {
                byte type = rs.getByte("type");
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("data"));
                Part part = new Part(type);
                part.id = rs.getShort("id");
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    part.pi[i] = new PartImg();
                    part.pi[i].id = Short.parseShort(js_in.get(0).toString());
                    part.pi[i].dx = Byte.parseByte(js_in.get(1).toString());
                    part.pi[i].dy = Byte.parseByte(js_in.get(2).toString());
                }
                Part.ENTRY.add(part);
            }
            rs.close();
            // load item 3
            query = "SELECT * FROM `item3`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate3 temp = new ItemTemplate3();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.clazz = rs.getByte("clazz");
                temp.typeEquip = rs.getByte("typeequip");
                temp.icon = rs.getShort("icon");
                temp.level = rs.getShort("level");
                temp.color = rs.getByte("color");
                temp.typelock = rs.getByte("typelock");
                temp.numHoleDaDuc = rs.getByte("numHoleDaDuc");
                // temp.valueChetac = rs.getShort("chetac");
                temp.valueChetac = (short) (100);
                temp.isHoanMy = rs.getByte("ishoanmy");
                temp.valueKichAn = rs.getByte("valuekichan");
                // System.out.println(temp.id);
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("op_1"));
                temp.option_item = new ArrayList<>();
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                    temp.option_item.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Short.parseShort(js2.get(1).toString())));
                }
                js.clear();
                temp.option_item_2 = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("op_2"));
                for (int k = 0; k < js.size(); k++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(k).toString());
                    temp.option_item_2.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Short.parseShort(js2.get(1).toString())));
                }
                js.clear();
                temp.numLoKham = rs.getByte("numlokham");
                js = (JSONArray) JSONValue.parse(rs.getString("mdakham"));
                temp.mdakham = new short[js.size()];
                for (int l = 0; l < temp.mdakham.length; l++) {
                    temp.mdakham[l] = Short.parseShort(js.get(l).toString());
                }
                temp.part = rs.getShort("part");
                temp.beri = rs.getInt("beri");
                temp.ruby = rs.getInt("ruby");
                // Part.get_part(temp.id);;
                ItemTemplate3.ENTRYS.add(temp);
            }
            rs.close();
            // load info item4
            query = "SELECT * FROM `item4_info`;";
            ItemTemplate4_Info.ENTRY = new ArrayList<>();
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate4_Info temp = new ItemTemplate4_Info();
                temp.id = rs.getShort("id");
                temp.info = rs.getString("info");
                ItemTemplate4_Info.ENTRY.add(temp);
            }
            rs.close();
            // load item temp 4
            query = "SELECT * FROM `item4`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate4 temp = new ItemTemplate4();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.icon = rs.getShort("icon");
                temp.indexInfoPotion = rs.getShort("indexInfoPotion");
                temp.beri = rs.getInt("price");
                temp.ruby = rs.getShort("priceruby");
                temp.istrade = rs.getByte("istrade");
                temp.type = rs.getByte("hpmpother");
                temp.timedelay = rs.getShort("timedelay");
                temp.value = rs.getShort("value");
                temp.timeactive = rs.getShort("timeactive");
                temp.nameuse = rs.getString("nameuse");
                ItemTemplate4.ENTRYS.add(temp);
            }
            rs.close();
            // load item temp 7
            query = "SELECT * FROM `item7`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate7 temp = new ItemTemplate7();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.type = rs.getByte("type");
                temp.icon = rs.getByte("icon");
                temp.price = rs.getInt("price");
                temp.priceruby = rs.getShort("priceruby");
                temp.istrade = rs.getByte("istrade");
                ItemTemplate7.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load item ok");
            // load skill temp
            Skill_Template.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `skill` ORDER BY `id_index`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                // int id = rs.getInt("id");
                Skill_Template temp_add = new Skill_Template(rs.getShort("id_index"),
                        rs.getShort("id_2"), rs.getShort("icon"), rs.getByte("typeSkill"),
                        rs.getByte("typeBuff"), rs.getString("name"), rs.getShort("typeEffSkill"),
                        rs.getShort("range"));
                temp_add.getData(rs.getByte("nTarget"), rs.getShort("rangeLan"),
                        rs.getInt("damage"), rs.getShort("manaLost"), rs.getInt("timeDelay"),
                        rs.getByte("nKick"), rs.getString("info"), rs.getByte("Lv_RQ"),
                        rs.getByte("typeDevil"));
                temp_add.op = new ArrayList<>();
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("option"));
                for (int j = 0; j < js.size(); j++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(j).toString());
                    temp_add.op.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Integer.parseInt(js2.get(1).toString())));
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("EffSpec"));
                temp_add.idEffSpec = Byte.parseByte(js.get(0).toString());
                temp_add.perEffSpec = Short.parseShort(js.get(1).toString());
                temp_add.timeEffSpec = Short.parseShort(js.get(2).toString());
                js.clear();
                Skill_Template.ENTRYS.add(temp_add);
            }
            rs.close();
            System.out.println("load skill ok");
            // load item option temp
            ItemOptionTemplate.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `itemoption`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemOptionTemplate temp = new ItemOptionTemplate();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.color = rs.getByte("color");
                temp.percent = rs.getByte("percent");
                ItemOptionTemplate.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load item op temp ok");
            // load item fashion info
            ItemFashion.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `fashiontemplate`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                int id = rs.getInt("id");
                short icon = rs.getShort("icon");
                String name = rs.getString("name");
                String info = rs.getString("info");
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("mwear"));
                short[] wear = new short[js.size()];
                for (int i = 0; i < wear.length; i++) {
                    wear[i] = Short.parseShort(js.get(i).toString());
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("op"));
                List<Option> op = new ArrayList<>();
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                    op.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Integer.parseInt(js2.get(1).toString())));
                }
                ItemFashion.ENTRYS
                        .add(new ItemFashion((byte) id, icon, name, info, wear, op, rs.getInt("price")));
            }
            rs.close();
            System.out.println("load fashion temp ok");
            // load boss
            Boss.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `boss`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                int id = rs.getInt("id");
                int mob_id = rs.getInt("mob_id");
                String site = rs.getString("site");
                int hp = rs.getInt("hp");
                String skill = rs.getString("skill");
                String buff = rs.getString("buff");
                int level = rs.getInt("level");
                JSONArray js = (JSONArray) JSONValue.parse(site);
                short temp_x = Short.parseShort(js.get(1).toString());
                short temp_y = Short.parseShort(js.get(2).toString());
                Map[] map = Map.get_map_by_id(Short.parseShort(js.get(0).toString()));
                js.clear();
                for (int i = 0; i < map.length; i++) {
                    if (i == 0) {
                        continue;
                    }
                    Boss boss_temp = new Boss();
                    boss_temp.id = id;
                    boss_temp.mob = new Mob();
                    boss_temp.mob.mob_template = MobTemplate.ENTRYS.get(mob_id);
                    boss_temp.mob.x = temp_x;
                    boss_temp.mob.y = temp_y;
                    boss_temp.mob.hp_max = hp;
                    boss_temp.mob.hp = 0;
                    boss_temp.mob.level = level;
                    boss_temp.mob.isdie = true;
                    boss_temp.mob.id_target = -1;
                    boss_temp.mob.index = this.index_mob;
                    boss_temp.index_mob_save = this.index_mob;
                    this.index_mob += 10;
                    boss_temp.mob.boss_info = boss_temp;
                    boss_temp.mob.map = map[i];
                    Mob.ENTRYS.put(boss_temp.mob.index, boss_temp.mob);
                    for (int j = 0; j < 10; j++) { // them 10slot cho 10 bac
                        Mob.ENTRYS.put((boss_temp.mob.index + j), boss_temp.mob);
                    }
                    //
                    js = (JSONArray) JSONValue.parse(skill);
                    boss_temp.skill = new short[js.size()];
                    for (int i2 = 0; i2 < boss_temp.skill.length; i2++) {
                        boss_temp.skill[i2] = Short.parseShort(js.get(i2).toString());
                    }
                    boss_temp.time_atk = new long[boss_temp.skill.length];
                    boss_temp.TopDame = new ArrayList<>();
                    boss_temp.levelBoss = 1;
                    js.clear();
                    js = (JSONArray) JSONValue.parse(buff);
                    boss_temp.buff = new ArrayList<>();
                    for (int i2 = 0; i2 < js.size(); i2++) {
                        JSONArray js2 = (JSONArray) js.get(i2);
                        boss_temp.buff.add(new Option(Byte.parseByte(js2.get(0).toString()),
                                Integer.parseInt(js2.get(1).toString())));
                    }
                    Boss.ENTRYS.add(boss_temp);
                }
            }
            //
            rs.close();
            System.out.println("load boss ok, mob size : " + (this.index_mob - 1));
            // load hair
            query = "SELECT * FROM `itemhair`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemHair.ENTRYS.add(ItemHair.read_json_it_hair(rs));
            }
            rs.close();
            System.out.println("load item hair ok");
            ItemTemplate8.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `item8`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate8 temp = new ItemTemplate8();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.icon = rs.getShort("icon");
                temp.info = rs.getString("info");
                temp.beri = rs.getInt("price");
                temp.ruby = rs.getShort("priceruby");
                temp.istrade = rs.getByte("istrade");
                temp.type = rs.getByte("hpmpother");
                temp.timedelay = rs.getShort("timedelay");
                temp.value = rs.getShort("value");
                temp.timeactive = rs.getShort("timeactive");
                temp.nameuse = rs.getString("nameuse");
                ItemTemplate8.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load item clan ok");
            Clan.ENTRY = new ArrayList<>();
            Clan.BXH = new ArrayList<>();
            query = "SELECT * FROM `clan`;";
            Set<String> name_check = new HashSet<>();
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Clan clan = new Clan();
                clan.id = rs.getShort("id");
                clan.name = rs.getString("name");
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("info"));
                clan.icon = Short.parseShort(js.get(0).toString());
                clan.level = Short.parseShort(js.get(1).toString());
                clan.xp = Integer.parseInt(js.get(2).toString());
                clan.maxAttri = Short.parseShort(js.get(3).toString());
                clan.pointAttri = Short.parseShort(js.get(4).toString());
                clan.trungsinh = Byte.parseByte(js.get(5).toString());
                switch (clan.trungsinh) {
                    case 1: {
                        clan.maxAttri = 25;
                        break;
                    }
                    case 2: {
                        clan.maxAttri = 30;
                        break;
                    }
                    case 3: {
                        clan.maxAttri = 35;
                        break;
                    }
                    case 4: {
                        clan.maxAttri = 40;
                        break;
                    }
                    case 5: {
                        clan.maxAttri = 45;
                        break;
                    }
                    case 6: {
                        clan.maxAttri = 50;
                        break;
                    }
                    default: { // 0
                        clan.maxAttri = 20;
                        break;
                    }
                }
                clan.countAction = Integer.parseInt(js.get(6).toString());
                clan.ruby = Integer.parseInt(js.get(7).toString());
                clan.beri = Integer.parseInt(js.get(8).toString());
                clan.allowRequest = Byte.parseByte(js.get(9).toString());
                clan.opAttri = new short[] {0, 0, 0, 0, 0};
                JSONArray js2 = (JSONArray) js.get(10);
                for (int i = 0; i < clan.opAttri.length; i++) {
                    clan.opAttri[i] = Short.parseShort(js2.get(i).toString());
                }
                clan.thongbao = rs.getString("notice");
                js.clear();
                clan.chat = new ArrayList<>();
                clan.mem_request = new ArrayList<>();
                clan.members = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("member"));
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    Clan_member mem = new Clan_member();
                    mem.id = (short) i;
                    mem.name = js_in.get(0).toString();
                    mem.level = Short.parseShort(js_in.get(1).toString());
                    mem.levelInclan = Byte.parseByte(js_in.get(2).toString());
                    mem.donate = Short.parseShort(js_in.get(3).toString());
                    mem.gopRuby = Short.parseShort(js_in.get(4).toString());
                    mem.numquest = Short.parseShort(js_in.get(5).toString());
                    mem.conghien = Integer.parseInt(js_in.get(6).toString());
                    mem.head = Short.parseShort(js_in.get(7).toString());
                    mem.hair = Short.parseShort(js_in.get(8).toString());
                    mem.hat = Short.parseShort(js_in.get(9).toString());
                    mem.clazz = Byte.parseByte(js_in.get(10).toString());
                    //
                    boolean add = true;
                    int num_clazz = 0;
                    for (int j = 0; j < clan.members.size(); j++) {
                        if (clan.members.get(j).clazz == mem.clazz) {
                            num_clazz++;
                        }
                    }
                    if (num_clazz >= 4) {
                        System.out.println("err load clan >=4 " + clan.name + " " + mem.name);
                        add = false;
                    }
                    if (add && !name_check.contains(mem.name)) {
                        name_check.add(mem.name);
                    } else {
                        add = false;
                    }
                    if (add) {
                        clan.members.add(mem);
                    }
                }
                js.clear();
                clan.list_it = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("item"));
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    ItemBag47 itemBag47 = new ItemBag47();
                    itemBag47.category = 4;
                    itemBag47.id = Short.parseShort(js_in.get(0).toString());
                    itemBag47.quant = Short.parseShort(js_in.get(1).toString());
                    clan.list_it.add(itemBag47);
                }
                clan.buff = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("buff"));
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    clan.buff.add(new EffTemplate(Byte.parseByte(js_in.get(0).toString()),
                            Integer.parseInt(js_in.get(1).toString()),
                            Long.parseLong(js_in.get(2).toString())));
                }
                Clan.add_new_clan(clan);
            }
            rs.close();
            System.out.println("load clan ok");
            // load quest
            query = "SELECT * FROM `quests`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Quest.add(rs);
            }
            Quest.add_finish_quest();
            rs.close();
            System.out.println("load quest ok");
            // load pet template
            query = "SELECT * FROM `pet_template`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Pet tempPet = new Pet();
                tempPet.id = rs.getShort("id");
                tempPet.name = rs.getString("name");
                tempPet.type = rs.getByte("type");
                tempPet.icon = rs.getShort("icon");
                tempPet.frame = rs.getShort("frame");
                Pet.ENTRY.add(tempPet);
            }
            rs.close();
            System.out.println("load pet ok");
            query = "SELECT * FROM `market`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Market tempMarket = new Market();
                tempMarket.type = rs.getByte("id");
                JSONObject jsob = (JSONObject) JSONValue.parse(rs.getString("data"));
                tempMarket.item3 = new ArrayList<>();
                JSONArray js = (JSONArray) JSONValue.parse(jsob.get("item3").toString());
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) js.get(i);
                    ItemMarket itemMarket = new ItemMarket();
                    itemMarket.load_json(js2);
                    if (itemMarket.index != -1) {
                        tempMarket.item3.add(itemMarket);
                    }
                }
                js.clear();
                tempMarket.item47 = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(jsob.get("item47").toString());
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) js.get(i);
                    PotionMarket potionMarket = new PotionMarket();
                    potionMarket.load_json(js2);
                    if (potionMarket.index != -1) {
                        tempMarket.item47.add(potionMarket);
                    }
                }
                js.clear();
                Market.ENTRY.add(tempMarket);
            }
            // rs.close();
            System.out.println("load market ok");
        } catch (SQLException e) {
            e.printStackTrace();
            System.exit(0);
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (conn != null) {
                    conn.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    private void load_config() throws IOException {
        final byte[] ab = Util.loadfile("htth.conf");
        if (ab == null) {
            System.out.println("Config file not found!");
            System.exit(0);
        }
        final String data = new String(ab);
        final HashMap<String, String> configMap = new HashMap<String, String>();
        final StringBuilder sbd = new StringBuilder();
        boolean bo = false;
        for (int i = 0; i <= data.length(); ++i) {
            final char es;
            if (i == data.length() || (es = data.charAt(i)) == '\n') {
                bo = false;
                final String sbf = sbd.toString().trim();
                if (sbf != null && !sbf.equals("") && sbf.charAt(0) != '#') {
                    final int j = sbf.indexOf(58);
                    if (j > 0) {
                        final String key = sbf.substring(0, j).trim();
                        final String value = sbf.substring(j + 1).trim();
                        configMap.put(key, value);
                        System.out.println("config: " + key + ": " + value);
                    }
                }
                sbd.setLength(0);
            } else {
                if (es == '#') {
                    bo = true;
                }
                if (!bo) {
                    sbd.append(es);
                }
            }
        }
        if (configMap.containsKey("port")) {
            this.server_port = Integer.parseInt(configMap.get("port"));
        } else {
            this.server_port = 2239;
        }
        if (configMap.containsKey("debug")) {
            this.debug = Boolean.parseBoolean(configMap.get("debug"));
        } else {
            this.debug = false;
        }
        if (configMap.containsKey("mysql-host")) {
            this.mysql_host = configMap.get("mysql-host");
        } else {
            this.mysql_host = "127.0.0.1";
        }
        if (configMap.containsKey("mysql-user")) {
            this.mysql_user = configMap.get("mysql-user");
        } else {
            this.mysql_user = "root";
        }
        if (configMap.containsKey("mysql-password")) {
            this.mysql_pass = configMap.get("mysql-password");
        } else {
            this.mysql_pass = "12345678";
        }
        if (configMap.containsKey("mysql-database")) {
            this.mysql_database = configMap.get("mysql-database");
        } else {
            this.mysql_database = "database";
        }
        if (configMap.containsKey("exp")) {
            this.exp = Integer.parseInt(configMap.get("exp"));
        } else {
            this.exp = 1;
        }
        if (configMap.containsKey("serveradmin")) {
            this.server_admin = Boolean.parseBoolean(configMap.get("serveradmin"));
        } else {
            this.server_admin = false;
        }
    }

    public void close() {
        stop_service();
    }

    public void chatKTG(Player p, String text) throws IOException {
        if (p.conn.user.equals("admin") || p.time_chat_ktg < System.currentTimeMillis()) {
            p.time_chat_ktg = System.currentTimeMillis() + 30_000L;
            chatKTG(1, text, 0);
            Service.send_box_ThongBao_OK(p, "Chat KTG thành công với nội dung: " + text);
        } else {
            Service.send_box_ThongBao_OK(p,
                    "Chờ " + (p.time_chat_ktg - System.currentTimeMillis()) / 1000L + "s");
        }
    }

    public void chatKTG(int type, String text, int color) throws IOException {
        Message m = new Message(-31);
        m.writer().writeByte(type);
        m.writer().writeUTF(text);
        m.writer().writeByte(color);
        m.writer().writeShort(-1);
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                for (int i = 0; i < map.players.size(); i++) {
                    Player p0 = map.players.get(i);
                    if (p0.conn != null) {
                        p0.conn.addmsg(m);
                    }
                }
            }
        }
        List<Map> mapplus = Map.get_map_plus();
        for (int i = 0; i < mapplus.size(); i++) {
            for (int i12 = 0; i12 < mapplus.get(i).players.size(); i12++) {
                Player p0 = mapplus.get(i).players.get(i12);
                if (p0.conn != null) {
                    p0.conn.addmsg(m);
                }
            }
        }
        m.cleanup();
    }

    public TaiXiu TaiXiu() {
        return tx;
    }
}
