package client;

import java.io.DataOutputStream;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.*;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import activities.*;
import org.joda.time.DateTime;
import org.json.simple.JSONArray;
import org.json.simple.JSONValue;
import core.Manager;
import core.Service;
import core.Util;
import database.SQL;
import io.Message;
import io.Session;
import map.Map;
import map.MapCanGoTo;
import map.Npc;
import map.Vgo;
import template.*;
/**
 *
 * @author Truongbk
 */
public class Player {
    public Session conn;
    public short index_map;
    public int id;
    public byte clazz;
    public String name;
    public short x;
    public short y;
    public int hp;
    public int mp;
    public short level;
    public long exp;
    public short point1;
    public short point2;
    public short point3;
    public short point4;
    public short point5;
    public Map map;
    public int xold;
    public int yold;
    public boolean isdie;
    public byte last_index_join_item;
    public Set<String> id_meet_in_map;
    public int[] data_super_upgrade;
    public BlockingQueue<Integer> key_red_line;
    public int time_key_red_line;
    public long time_pick_item_other;
    public long cd_ticket_next;
    public long cd_keyboss_next;
    public long cd_pvp_next;
    public byte[] is_combo;
    public long time_combo;
    public String[] data_yesno_gem;
    public int fee_trade;
    public byte danhLaChoang;
    public byte thanhLoc;
    public byte nenDau;
    public byte giaiPhongNangLuong;
    public Clan clan;
    public Dungeon dungeon;
    public Player pvp_target;
    public boolean pvp_accept;
    public int pvp_win;
    public int pvp_lose;
    public short id_ship_packet = -1;
    public Ship_pet ship_pet;
    public byte time_ship;
    public byte time_can_hs;
    public List<FriendTemp> enemy_list;
    public TableTickOption tableTickOption;
    public String[] name_ThoSanHaiTac;
    public Upgrade_Skin_Info upgrade_skin;
    public byte[] tool_dial;
    public List<ItemBag47> daHanhTrinh;
    private int tichLuy;
    private short ticket;
    private long vang;
    private int kimcuong;
    public boolean ischangemap = true;
    public short thongthao;
    public int pointPk;
    public short pointAttribute;
    public int typePirate;
    public int indexGhostServer;
    public int pointSkill;
    public short head;
    public short hair;
    public Item item;
    public Body body;
    public BlockingQueue<Message> list_msg_cache;
    public byte type_pk;
    public ItemMap[] it_map;
    private List<EffTemplate> list_eff;
    public long time_chat_ktg;
    public int use_item_3;
    public int[] tool_upgrade;
    public byte[][] rms;
    public List<Skill_info> skill_point;
    public List<Skill_info> list_can_combo;
    public Item_wear item_chuyenhoa_save_0;
    public Item_wear item_chuyenhoa_save_1;
    public Item_wear item_to_kham_ngoc;
    public short item_to_kham_ngoc_id_ngoc;
    public Player trade_target;
    public List<Item_wear> list_item_trade3;
    public List<ItemBag47> list_item_trade47;
    public long money_trade;
    public boolean is_lock_trade;
    public boolean is_accept_trade;
    public List<FriendTemp> friend_list;
    public List<ItemFashionP2> fashion;
    public List<ItemFashionP> itfashionP;
    public long time_buff_hp_mp;
    public List<QuestP> list_quest;
    public DateTime date;
    public Party party;
    public int[] data_yesno;
    public int[] map_tele;
    public int id_map_save = 1;
    public boolean wait_change_map;
    public List<ItemBoatP> itemboat;
    public boolean is_show_hat;
    private int vnd;
    private int bua;
    public byte percent_da_sieu_cap;
    private short pvp_ticket;
    private short key_boss;
    public MapBossInfo map_boss_info;
    public short pointAttributeThongThao;
    public List<Option> list_op_thongthao;
    public short tocSuper;
    private int pvppoint;
    public int time_nvl;
    public long time_hs_little_garden;
    public boolean isTachTB = false;
    public byte time_ttvt;
    public long time_skill_decrease;
    public long time_can_mob_atk;
    public boolean is_show_weapon;
    public Player targetFight;
    private int wanted_price;
    public Wanted_Chest[] wanted_chest;
    public List<MyPet> my_pet;
    public long time_change_map;
    public int diemdanh;
    public int diemdanhvip;
    public long[] time_sk = new long[10000];
    public int sucmanhvatly;
    public int[] doriki;
    public int[] lucthuc;

    public Player(Session conn, String name) {
        this.conn = conn;
        this.name = name;
    }

    public boolean setup() {
        Connection connection = null;
        Statement st = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            st = connection.createStatement();
            rs = st.executeQuery(
                    "SELECT * FROM `players` WHERE `name` = '" + this.name + "' LIMIT 1;");
            if (!rs.next()) {
                return false;
            }
            id = rs.getInt("id");
            index_map = (short) id;
            clazz = rs.getByte("clazz");
            pvppoint = rs.getInt("pvppoint");
            diemdanh = rs.getInt("diemdanh");
            diemdanhvip = rs.getInt("diemdanhvip");
            JSONArray js = (JSONArray) JSONValue.parse(rs.getString("level"));
            level = Short.parseShort(js.get(0).toString());
            exp = Long.parseLong(js.get(1).toString());
            thongthao = Short.parseShort(js.get(2).toString());
            js.clear();
            date = DateTime.parse(rs.getString("date"));
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("potential"));
            pointAttribute = Short.parseShort(js.get(0).toString());
            point1 = Short.parseShort(js.get(1).toString());
            point2 = Short.parseShort(js.get(2).toString());
            point3 = Short.parseShort(js.get(3).toString());
            point4 = Short.parseShort(js.get(4).toString());
            point5 = Short.parseShort(js.get(5).toString());
            pointAttributeThongThao = Short.parseShort(js.get(6).toString());
            list_op_thongthao = new ArrayList<>();
            JSONArray js_in = (JSONArray) js.get(7);
            for (int i = 0; i < js_in.size(); i++) {
                JSONArray js_in_1 = (JSONArray) js_in.get(i);
                list_op_thongthao.add(new Option(Byte.parseByte(js_in_1.get(0).toString()),
                        Integer.parseInt(js_in_1.get(1).toString())));
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("point_inven"));
            vang = Long.parseLong(js.get(0).toString());
            kimcuong = Integer.parseInt(js.get(1).toString());
            vnd = Integer.parseInt(js.get(2).toString());
            bua = Integer.parseInt(js.get(3).toString());
            tichLuy = Integer.parseInt(js.get(4).toString());
            pvp_win = Integer.parseInt(js.get(5).toString());
            pvp_lose = Integer.parseInt(js.get(6).toString());
            time_ship = Byte.parseByte(js.get(7).toString());
            time_can_hs = Byte.parseByte(js.get(8).toString());
            time_nvl = Integer.parseInt(js.get(9).toString());
            time_ttvt = Byte.parseByte(js.get(10).toString());
            wanted_price = Integer.parseInt(js.get(11).toString());
            js.clear();
            this.wanted_chest = new Wanted_Chest[2];
            js = (JSONArray) JSONValue.parse(rs.getString("wanted_chest"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js_in2 = (JSONArray) js.get(i);
                this.wanted_chest[i] = new Wanted_Chest();
                this.wanted_chest[i].id = Short.parseShort(js_in2.get(0).toString());
                this.wanted_chest[i].timeUse = Long.parseLong(js_in2.get(1).toString());
                this.wanted_chest[i].maxTimeUse = Short.parseShort(js_in2.get(2).toString());
                this.wanted_chest[i].Ruby = Short.parseShort(js_in2.get(3).toString());
            }
            js.clear();
            my_pet = new ArrayList<>();
            js = (JSONArray) JSONValue.parse(rs.getString("mypet"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js_in2 = (JSONArray) js.get(i);
                MyPet tempPet = new MyPet();
                tempPet.id = Short.parseShort(js_in2.get(0).toString());
                tempPet.template = Pet.getTemplate(Short.parseShort(js_in2.get(1).toString()));
                tempPet.isUse = Byte.parseByte(js_in2.get(2).toString()) == 1;
                if (tempPet.template != null) {
                    my_pet.add(tempPet);
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("site"));
            //
            Map[] map = Map.get_map_by_id(Integer.parseInt(js.get(0).toString()));
            byte zone_id = Byte.parseByte(js.get(1).toString());
            int zone_goto = zone_id < map.length ? zone_id : 0;
            if (zone_goto != 0) {
                while (zone_goto < (map[zone_goto].template.max_zone - 1)
                        && map[zone_goto].players.size() >= map[zone_goto].template.max_player) {
                    zone_goto++;
                }
            }
            this.map = map[zone_goto];
            this.hp = Integer.parseInt(js.get(2).toString());
            this.mp = Integer.parseInt(js.get(3).toString());
            x = Short.parseShort(js.get(4).toString());
            y = Short.parseShort(js.get(5).toString());
            if (this.x < 0 || this.x > this.map.template.maxW || this.y < 0
                    || this.y > this.map.template.maxH) {
                x = (short) (this.map.template.maxW / 2);
                y = (short) (this.map.template.maxH / 2);
            }
            is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
            pointPk = Integer.parseInt(js.get(7).toString());
            ticket = Short.parseShort(js.get(8).toString());
            cd_ticket_next = Long.parseLong(js.get(9).toString());
            if (cd_ticket_next == 0 || ticket >= get_ticket_max()) {
                cd_ticket_next = System.currentTimeMillis() + (60_000L * 10); // 10p
            }
            while (ticket < get_ticket_max() && cd_ticket_next < System.currentTimeMillis()) {
                ticket++;
                cd_ticket_next += (60_000L * 10); // 10p
            }
            pvp_ticket = Short.parseShort(js.get(10).toString());
            cd_pvp_next = Long.parseLong(js.get(11).toString());
            if (cd_pvp_next == 0 || pvp_ticket >= get_pvp_ticket_max()) {
                cd_pvp_next = System.currentTimeMillis() + (60_000L * 60 * 2); // 2h
            }
            while (pvp_ticket < get_pvp_ticket_max() && cd_pvp_next < System.currentTimeMillis()) {
                pvp_ticket++;
                cd_pvp_next += (60_000L * 60 * 2); // 2h
            }
            key_boss = Short.parseShort(js.get(12).toString());
            cd_keyboss_next = Long.parseLong(js.get(13).toString());
            if (cd_keyboss_next == 0 || key_boss >= get_key_boss_max()) {
                cd_keyboss_next = System.currentTimeMillis() + (60_000L * 60 * 1); // 1h
            }
            while (key_boss < get_key_boss_max() && cd_keyboss_next < System.currentTimeMillis()) {
                key_boss++;
                cd_keyboss_next += (60_000L * 60 * 1); // 1h
            }
            if (js.size() > 14) {
                is_show_weapon = Byte.parseByte(js.get(14).toString()) == 1;
            } else {
                is_show_weapon = true;
            }
            //
            js.clear();
            list_quest = new ArrayList<>();
            js = (JSONArray) JSONValue.parse(rs.getString("quest"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js_q = (JSONArray) js.get(i);
                QuestP temp = new QuestP();
                int id_quest_get = Short.parseShort(js_q.get(0).toString());
                temp.template = Quest.get_quest(id_quest_get);
                JSONArray js_q2 = (JSONArray) js_q.get(1);
                temp.data = new short[js_q2.size()][];
                for (int j = 0; j < temp.data.length; j++) {
                    JSONArray js_q3 = (JSONArray) js_q2.get(j);
                    temp.data[j] = new short[js_q3.size()];
                    for (int k = 0; k < temp.data[j].length; k++) {
                        temp.data[j][k] = Short.parseShort(js_q3.get(k).toString());
                    }
                }
                list_quest.add(temp);
            }
            js.clear();
            //
            item = new Item(this);
            item.bag3 = new Item_wear[item.max_bag];
            item.box3 = new Item_wear[item.max_box];
            item.it_body = new Item_wear[8];
            item.bag47 = new ArrayList<>();
            item.box47 = new ArrayList<>();
            item.save_item_wear = new ArrayList<>();
            item.save_item_47 = new ArrayList<>();
            daHanhTrinh = new ArrayList<>();
            //
            js = (JSONArray) JSONValue.parse(rs.getString("bag3"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                Item_wear temp = new Item_wear();
                Item.readUpdateItem(js2.toString(), temp);
                if (temp.index < item.bag3.length) {
                    item.bag3[temp.index] = temp;
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("save_it3"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                Item_wear temp = new Item_wear();
                Item.readUpdateItem(js2.toString(), temp);
                if (temp.template != null) {
                    item.save_item_wear.add(temp);
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("box3"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                Item_wear temp = new Item_wear();
                Item.readUpdateItem(js2.toString(), temp);
                if (temp.index < item.box3.length) {
                    item.box3[temp.index] = temp;
                }
            }
            js.clear();
            //
            js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                Item_wear temp = new Item_wear();
                Item.readUpdateItem(js2.toString(), temp);
                if (temp.index < item.it_body.length) {
                    if (temp.index == 6) {
                        item.it_heart = temp;
                        item.it_heart.typelock = 1;
                    } else {
                        item.it_body[temp.index] = temp;
                    }
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("bag47"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                ItemBag47 temp = new ItemBag47();
                temp.category = Byte.parseByte(js2.get(0).toString());
                temp.id = Short.parseShort(js2.get(1).toString());
                temp.quant = Short.parseShort(js2.get(2).toString());
                if (temp.quant > 0) {
                    item.bag47.add(temp);
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("box47"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                ItemBag47 temp = new ItemBag47();
                temp.category = Byte.parseByte(js2.get(0).toString());
                temp.id = Short.parseShort(js2.get(1).toString());
                temp.quant = Short.parseShort(js2.get(2).toString());
                if (temp.quant > 0) {
                    item.box47.add(temp);
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("save_it47"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                ItemBag47 temp = new ItemBag47();
                temp.category = Byte.parseByte(js2.get(0).toString());
                temp.id = Short.parseShort(js2.get(1).toString());
                temp.quant = Short.parseShort(js2.get(2).toString());
                if (temp.quant > 0
                        && ((temp.category == 4 && temp.id > 28) || (temp.category == 7))) {
                    item.save_item_47.add(temp);
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("hanhtrinh"));
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                ItemBag47 temp = new ItemBag47();
                temp.category = Byte.parseByte(js2.get(0).toString());
                temp.id = Short.parseShort(js2.get(1).toString());
                temp.quant = Short.parseShort(js2.get(2).toString());
                daHanhTrinh.add(temp);
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("rms"));
            rms = new byte[11][];
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                rms[i] = new byte[js2.size()];
                for (int j = 0; j < rms[i].length; j++) {
                    rms[i][j] = Byte.parseByte(js2.get(j).toString());
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("skill"));
            skill_point = new ArrayList<>();
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                Skill_info skill_add = new Skill_info();
                skill_add.exp = Long.parseLong(js2.get(1).toString());
                skill_add.temp = Skill_Template.get_temp(Short.parseShort(js2.get(0).toString()),
                        skill_add.exp);
                skill_add.lvdevil = Byte.parseByte(js2.get(2).toString());
                skill_add.devilpercent = Byte.parseByte(js2.get(3).toString());
                skill_point.add(skill_add);
            }
            js.clear();
            //
            friend_list = new ArrayList<>();
            js = (JSONArray) JSONValue.parse(rs.getString("friend"));
            for (int i = 0; i < js.size(); i++) {
                FriendTemp temp = new FriendTemp((JSONArray) JSONValue.parse(js.get(i).toString()));
                friend_list.add(temp);
                temp.id = friend_list.indexOf(temp);
            }
            js.clear();
            enemy_list = new ArrayList<>();
            js = (JSONArray) JSONValue.parse(rs.getString("enemy"));
            for (int i = 0; i < js.size(); i++) {
                FriendTemp temp = new FriendTemp((JSONArray) JSONValue.parse(js.get(i).toString()));
                enemy_list.add(temp);
                temp.id = enemy_list.indexOf(temp);
            }
            js.clear();
            //
            this.itfashionP = new ArrayList<>();
            this.fashion = new ArrayList<>();
            this.itemboat = new ArrayList<>();
            js = (JSONArray) JSONValue.parse(rs.getString("fashion"));
            JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js.get(0).toString());
            for (int i = 0; i < js_temp_2.size(); i++) {
                JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i).toString());
                ItemFashionP tempf = new ItemFashionP();
                tempf.category = Byte.parseByte(js_temp.get(0).toString());
                tempf.id = Short.parseShort(js_temp.get(1).toString());
                tempf.icon = Short.parseShort(js_temp.get(2).toString());
                tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                this.itfashionP.add(tempf);
            }
            js_temp_2.clear();
            js_temp_2 = (JSONArray) JSONValue.parse(js.get(1).toString());
            for (int i = 0; i < js_temp_2.size(); i++) {
                JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i).toString());
                ItemFashionP2 tempf = new ItemFashionP2();
                tempf.id = Short.parseShort(js_temp.get(0).toString());
                tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                tempf.level = Byte.parseByte(js_temp.get(2).toString());
                this.fashion.add(tempf);
            }
            js_temp_2 = (JSONArray) JSONValue.parse(js.get(2).toString());
            for (int i = 0; i < js_temp_2.size(); i++) {
                JSONArray js_temp = (JSONArray) JSONValue.parse(js_temp_2.get(i).toString());
                ItemBoatP tempboat = new ItemBoatP();
                tempboat.id = Byte.parseByte(js_temp.get(0).toString());
                tempboat.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                this.itemboat.add(tempboat);
            }
            js_temp_2.clear();
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("eff"));
            list_eff = new ArrayList<>();
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                list_eff.add(new EffTemplate(Byte.parseByte(js2.get(0).toString()),
                        Integer.parseInt(js2.get(1).toString()),
                        (System.currentTimeMillis() + Long.parseLong(js2.get(2).toString()))));
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("lucthuc"));
            sucmanhvatly = Integer.parseInt(js.get(0).toString());
            doriki = new int[2];
            lucthuc = new int[3];
            doriki[0] = Integer.parseInt(js.get(1).toString());
            doriki[1] = Integer.parseInt(js.get(2).toString());
            lucthuc[0] = Integer.parseInt(js.get(3).toString());
            lucthuc[1] = Integer.parseInt(js.get(4).toString());
            lucthuc[2] = Integer.parseInt(js.get(5).toString());
            js.clear();
            //
            body = new Body(this);
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (st != null) {
                    st.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        return true;
    }

    public void setin4() throws IOException {
        xold = x;
        yold = y;
        // rankWanted = -1;
        typePirate = -1;
        indexGhostServer = -1;
        pointSkill = 20;
        type_pk = (byte) (this.pointPk >= 400 ? 1 : -1);
        //
        int hp_max = this.body.get_hp_max(true);
        if (this.hp == -1) {
            this.hp = hp_max;
        }
        if (this.hp <= 0 && Map.isMapLang(this.map.template.id)) {
            this.hp = hp_max / 10;
        }
        if (this.hp <= 0) {
            this.hp = 0;
            isdie = true;
            this.map.die_player(this, this);
        } else {
            if (this.hp > hp_max) {
                this.hp = hp_max;
            }
            isdie = false;
        }
        if (this.mp == -1) {
            this.mp = this.body.get_mp_max(true);
        }
        ischangemap = false;
        list_msg_cache = new LinkedBlockingQueue<>();
        it_map = new ItemMap[3];
        tool_upgrade = new int[] {-1, -1};
        item_chuyenhoa_save_0 = null;
        item_chuyenhoa_save_1 = null;
        item_to_kham_ngoc = null;
        item_to_kham_ngoc_id_ngoc = -1;
        trade_target = null;
        list_item_trade3 = null;
        list_item_trade47 = null;
        money_trade = 0;
        fee_trade = 0;
        is_lock_trade = false;
        is_accept_trade = false;
        use_item_3 = -1;
        time_buff_hp_mp = System.currentTimeMillis() + 5000L;
        party = null;
        wait_change_map = true;
        id_meet_in_map = new HashSet<>();
        percent_da_sieu_cap = 35;
        key_red_line = new LinkedBlockingQueue<>();
        time_key_red_line = -1;
        time_pick_item_other = 0;
        is_combo = null;
        list_can_combo = new ArrayList<>();
        map_boss_info = null;
        if (item.it_heart != null && item.it_body[7] == null) {
            item.it_heart.index = 7;
        }
        danhLaChoang = 0;
        thanhLoc = 0;
        nenDau = 0;
        giaiPhongNangLuong = 0;
        clan = Clan.get_my_clan(this.name);
        tocSuper = 0;
        ship_pet = Ship_pet.get_pet(this);
    }

    public int get_level_percent() {
        if (level == 100) {
            return (int) ((exp * 1000) / Level.LEVEL_THONGTHAO[this.thongthao]);
        } else {
            return (int) ((exp * 1000) / Level.ENTRYS[level - 1].exp);
        }
    }

    @SuppressWarnings("unchecked")
    public static int flush(Player p, boolean print) {
        int result = 0;
        String query =
                "UPDATE `players` SET `level` = ?, `date` = ?, `site` = ?, `point_inven` = ?, "
                        + "`bag3` = ?, `it_body` = ?, `potential` = ?, `bag47` = ?, "
                        + "`rms` = ?, `skill` = ?, `friend` = ?, `enemy` = ?, `fashion` = ?, `eff` = ?, `box47` = ?, `box3` = ?, `quest` = ?, "
                        + "`exp` = ?, `pvppoint` = ?, `save_it3` = ?, `save_it47` = ?, "
                        + "`hanhtrinh` = ?, `wanted_point` = ?, `wanted_chest` = ?, `mypet` = ?, `diemdanh` = ?, `diemdanhvip` = ?, `lucthuc` = ? WHERE `id` = "
                        + p.id + ";";
        Connection connection = null;
        PreparedStatement ps = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(query);
            JSONArray js = new JSONArray();
            js.add(p.level);
            js.add(p.exp);
            js.add(p.thongthao);
            ps.setNString(1, js.toJSONString());
            js.clear();
            ps.setNString(2, p.date.toString());
            js = new JSONArray();
            if (Map.map_cant_save_site(p.map.template.id)) {
                //
                int x_save = -1, y_save = -1;
                Map[] map_get = Map.get_map_by_id(p.id_map_save);
                for (int i = 0; i < map_get[0].template.npcs.size(); i++) {
                    Npc npc_temp = map_get[0].template.npcs.get(i);
                    if (npc_temp.namegt.equals("Bản đồ")) {
                        x_save = npc_temp.x;
                        if (npc_temp.y < 250) {
                            y_save = (short) (npc_temp.y + 20);
                        } else {
                            y_save = (short) (npc_temp.y - 40);
                        }
                        break;
                    }
                }
                //
                if (x_save != -1 && y_save != -1) {
                    js.add(p.id_map_save);
                    js.add(0);
                    js.add(p.hp);
                    js.add(p.mp);
                    js.add(x_save);
                    js.add(y_save);
                } else {
                    js.add(1);
                    js.add(0);
                    js.add(p.hp);
                    js.add(p.mp);
                    js.add(830);
                    js.add(203);
                }
            } else {
                js.add(p.map.template.id);
                js.add(p.map.zone_id);
                js.add(p.hp);
                js.add(p.mp);
                js.add(p.x);
                js.add(p.y);
            }
            js.add(p.is_show_hat ? 1 : 0);
            js.add(p.pointPk);
            js.add(p.ticket);
            js.add(p.cd_ticket_next);
            js.add(p.pvp_ticket);
            js.add(p.cd_pvp_next);
            js.add(p.key_boss);
            js.add(p.cd_keyboss_next);
            js.add(p.is_show_weapon ? 1 : 0);
            //
            ps.setNString(3, js.toJSONString());
            js.clear();
            js = new JSONArray();
            //
            js.add(p.vang);
            js.add(p.kimcuong);
            js.add(p.vnd);
            js.add(p.bua);
            js.add(p.tichLuy);
            js.add(p.pvp_win);
            js.add(p.pvp_lose);
            js.add(p.time_ship);
            js.add(p.time_can_hs);
            js.add(p.time_nvl);
            js.add(p.time_ttvt);
            js.add(p.wanted_price);
            ps.setNString(4, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.item.bag3.length; i++) {
                if (p.item.bag3[i] != null) {
                    JSONArray js_temp = Item.it_data_to_json(p.item.bag3[i]);
                    if (js_temp.size() > 0) {
                        js.add(js_temp);
                    }
                }
            }
            ps.setNString(5, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.item.it_body.length; i++) {
                if (i == 6 && p.item.it_heart != null) {
                    p.item.it_heart.index = 6;
                    JSONArray js_temp = Item.it_data_to_json(p.item.it_heart);
                    if (js_temp.size() > 0) {
                        js.add(js_temp);
                    }
                } else if (p.item.it_body[i] != null) {
                    JSONArray js_temp = Item.it_data_to_json(p.item.it_body[i]);
                    if (js_temp.size() > 0) {
                        js.add(js_temp);
                    }
                }
            }
            ps.setNString(6, js.toJSONString());
            js.clear();
            js = new JSONArray();
            js.add(p.pointAttribute);
            js.add(p.point1);
            js.add(p.point2);
            js.add(p.point3);
            js.add(p.point4);
            js.add(p.point5);
            js.add(p.pointAttributeThongThao);
            JSONArray js_in = new JSONArray();
            for (int i = 0; i < p.list_op_thongthao.size(); i++) {
                JSONArray js_in1 = new JSONArray();
                js_in1.add(p.list_op_thongthao.get(i).id);
                js_in1.add(p.list_op_thongthao.get(i).getParam());
                js_in.add(js_in1);
            }
            js.add(js_in);
            ps.setNString(7, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.item.bag47.size(); i++) {
                JSONArray js_temp = new JSONArray();
                js_temp.add(p.item.bag47.get(i).category);
                js_temp.add(p.item.bag47.get(i).id);
                js_temp.add(p.item.bag47.get(i).quant);
                js.add(js_temp);
            }
            ps.setNString(8, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.rms.length; i++) {
                JSONArray js_temp = new JSONArray();
                for (int j = 0; j < p.rms[i].length; j++) {
                    js_temp.add(p.rms[i][j]);
                }
                js.add(js_temp);
            }
            ps.setNString(9, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.skill_point.size(); i++) {
                JSONArray js_temp = new JSONArray();
                js_temp.add(p.skill_point.get(i).temp.indexSkillInServer);
                js_temp.add(p.skill_point.get(i).exp);
                js_temp.add(p.skill_point.get(i).lvdevil);
                js_temp.add(p.skill_point.get(i).devilpercent);
                js.add(js_temp);
            }
            ps.setNString(10, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.friend_list.size(); i++) {
                js.add(p.friend_list.get(i).toJSONArray());
            }
            ps.setNString(11, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.enemy_list.size(); i++) {
                js.add(p.enemy_list.get(i).toJSONArray());
            }
            ps.setNString(12, js.toJSONString());
            js.clear();
            js = new JSONArray();
            JSONArray js_temp_2 = new JSONArray();
            for (int i = 0; i < p.itfashionP.size(); i++) {
                JSONArray js14 = new JSONArray();
                js14.add(p.itfashionP.get(i).category);
                js14.add(p.itfashionP.get(i).id);
                js14.add(p.itfashionP.get(i).icon);
                js14.add(p.itfashionP.get(i).is_use ? 1 : 0);
                js_temp_2.add(js14);
            }
            js.add(js_temp_2);
            JSONArray js_temp_22 = new JSONArray();
            for (int i = 0; i < p.fashion.size(); i++) {
                JSONArray js14 = new JSONArray();
                js14.add(p.fashion.get(i).id);
                js14.add(p.fashion.get(i).is_use ? 1 : 0);
                js14.add(p.fashion.get(i).level);
                js_temp_22.add(js14);
            }
            js.add(js_temp_22);
            //
            JSONArray js_temp_23 = new JSONArray();
            for (int i = 0; i < p.itemboat.size(); i++) {
                JSONArray js14 = new JSONArray();
                js14.add(p.itemboat.get(i).id);
                js14.add(p.itemboat.get(i).is_use ? 1 : 0);
                js_temp_23.add(js14);
            }
            js.add(js_temp_23);
            //
            ps.setNString(13, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.list_eff.size(); i++) {
                JSONArray js_temp = new JSONArray();
                EffTemplate eff_temp = p.list_eff.get(i);
                if (EffTemplate.check_eff_can_save(eff_temp.id)) {
                    js_temp.add(eff_temp.id);
                    js_temp.add(eff_temp.param);
                    js_temp.add(eff_temp.time - System.currentTimeMillis());
                    js.add(js_temp);
                }
            }
            ps.setNString(14, js.toJSONString()); // eff
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.item.box47.size(); i++) {
                JSONArray js_temp = new JSONArray();
                js_temp.add(p.item.box47.get(i).category);
                js_temp.add(p.item.box47.get(i).id);
                js_temp.add(p.item.box47.get(i).quant);
                js.add(js_temp);
            }
            ps.setNString(15, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.item.box3.length; i++) {
                if (p.item.box3[i] != null) {
                    JSONArray js_temp = Item.it_data_to_json(p.item.box3[i]);
                    if (js_temp.size() > 0) {
                        js.add(js_temp);
                    }
                }
            }
            ps.setNString(16, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.list_quest.size(); i++) {
                QuestP temp = p.list_quest.get(i);
                JSONArray js_p = new JSONArray();
                js_p.add(temp.template.id);
                JSONArray js_p2 = new JSONArray();
                for (int j = 0; j < temp.data.length; j++) {
                    JSONArray js_p3 = new JSONArray();
                    for (int k = 0; k < temp.data[j].length; k++) {
                        js_p3.add(temp.data[j][k]);
                    }
                    js_p2.add(js_p3);
                }
                js_p.add(js_p2);
                js.add(js_p);
            }
            ps.setNString(17, js.toJSONString());
            js.clear();
            long exp = p.exp;
            for (int i = 0; i < (p.level - 1); i++) {
                exp += Level.ENTRYS[i].exp;
            }
            for (int i = 0; i < p.thongthao; i++) {
                exp += Level.LEVEL_THONGTHAO[i];
            }
            ps.setLong(18, exp);
            ps.setInt(19, p.pvppoint);
            js = new JSONArray();
            for (int i = 0; i < p.item.save_item_wear.size(); i++) {
                JSONArray js_temp = Item.it_data_to_json(p.item.save_item_wear.get(i));
                if (js_temp.size() > 0) {
                    js.add(js_temp);
                }
            }
            ps.setNString(20, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.item.save_item_47.size(); i++) {
                JSONArray js_temp = new JSONArray();
                js_temp.add(p.item.save_item_47.get(i).category);
                js_temp.add(p.item.save_item_47.get(i).id);
                js_temp.add(p.item.save_item_47.get(i).quant);
                js.add(js_temp);
            }
            ps.setNString(21, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                JSONArray js_temp = new JSONArray();
                js_temp.add(p.daHanhTrinh.get(i).category);
                js_temp.add(p.daHanhTrinh.get(i).id);
                js_temp.add(p.daHanhTrinh.get(i).quant);
                js.add(js_temp);
            }
            ps.setNString(22, js.toJSONString());
            js.clear();
            ps.setInt(23, p.wanted_price);
            js = new JSONArray();
            for (int i = 0; i < p.wanted_chest.length; i++) {
                if (p.wanted_chest[i] != null) {
                    JSONArray js_in2 = new JSONArray();
                    js_in2.add(p.wanted_chest[i].id);
                    js_in2.add(p.wanted_chest[i].timeUse);
                    js_in2.add(p.wanted_chest[i].maxTimeUse);
                    js_in2.add(p.wanted_chest[i].Ruby);
                    js.add(js_in2);
                }
            }
            ps.setNString(24, js.toJSONString());
            js.clear();
            js = new JSONArray();
            for (int i = 0; i < p.my_pet.size(); i++) {
                JSONArray js_in2 = new JSONArray();
                MyPet pet = p.my_pet.get(i);
                js_in2.add(pet.id);
                js_in2.add(pet.template.id);
                js_in2.add(pet.isUse ? 1 : 0);
                js.add(js_in2);
            }
            ps.setNString(25, js.toJSONString());
            ps.setInt(26, p.diemdanh);
            ps.setInt(27, p.diemdanhvip);
            js = new JSONArray();
            //
            js.add(p.sucmanhvatly);
            js.add(p.doriki[0]);
            js.add(p.doriki[1]);
            js.add(p.lucthuc[0]);
            js.add(p.lucthuc[1]);
            js.add(p.lucthuc[2]);
            ps.setNString(28, js.toJSONString());
            js.clear();
            //
            result = ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
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
        return result;
    }

    public void goto_map(Vgo vgo) throws IOException {
        this.ischangemap = false;
        this.wait_change_map = true;
        this.xold = this.x;
        this.yold = this.y;
        Map[] map_go = vgo.map_go;
        if (map_go == null) {
            Service.send_box_ThongBao_OK(this, "Chưa thể đi đến map này!");
            return;
        }
        int idMap = MapCanGoTo.idMap[MapCanGoTo.idMap.length - 1];
        //
        QuestP quest_select = this.list_quest.get(0);
        if (quest_select != null) {
            for (int i = 0; i < MapCanGoTo.idQuest.length; i++) {
                if (MapCanGoTo.idQuest[i] > quest_select.template.id) {
                    idMap = MapCanGoTo.idMap[i - 1];
                    break;
                }
            }
        }
        if ( map_go[0].template.id !=119 && map_go[0].template.id!=120 &&map_go[0].template.id!=122 && map_go[0].template.id!=123 && map_go[0].template.id!=54 && map_go[0].template.id!=58 && map_go[0].template.id!= 59 && map_go[0].template.id!=123 && map_go[0].template.id!=984 && map_go[0].template.id!=1000
                && map_go[0].template.id!=127&& map_go[0].template.id!=167&& map_go[0].template.id!=168&& map_go[0].template.id!=169&& map_go[0].template.id!=170&& map_go[0].template.id!=171&& map_go[0].template.id!=172&& map_go[0].template.id!=173&& map_go[0].template.id!=174&& map_go[0].template.id!=175&& map_go[0].template.id!=176&& map_go[0].template.id > idMap ) {
            Service.send_box_ThongBao_OK(this,
                    "Chưa thể đi đến map này khi chưa hoàn thành nhiệm vụ!");
            return;
        }
        if (Map.is_map_dungeon(map_go[0].template.id) && this.dungeon != null) {
            int id_map = map_go[0].template.id;
            map_go = new Map[1];
            for (int i = 0; i < this.dungeon.maps.size(); i++) {
                if (id_map == this.dungeon.maps.get(i).template.id) {
                    map_go[0] = this.dungeon.maps.get(i);
                    break;
                }
            }
            switch (map_go[0].template.id) {
                case 168: {
                    vgo.xnew = 1490;
                    vgo.ynew = 260;
                    break;
                }
                case 169: {
                    vgo.xnew = 760;
                    vgo.ynew = 240;
                    break;
                }
                case 170: {
                    vgo.xnew = 100;
                    vgo.ynew = 245;
                    break;
                }
                case 171: {
                    vgo.xnew = 675;
                    vgo.ynew = 270;
                    break;
                }
                case 172: {
                    vgo.xnew = 113;
                    vgo.ynew = 240;
                    break;
                }
                case 173: {
                    vgo.xnew = 135;
                    vgo.ynew = 255;
                    break;
                }
                case 174: {
                    vgo.xnew = 121;
                    vgo.ynew = 225;
                    break;
                }
                case 175: {
                    vgo.xnew = 156;
                    vgo.ynew = 255;
                    break;
                }
                case 176: {
                    vgo.xnew = 190;
                    vgo.ynew = 245;
                    break;
                }
            }
        }
        //
        if (Map.is_map_boss(map_go[0].template.id)) {
            if (map_boss_info == null) {
                map_boss_info = new MapBossInfo();
                map_boss_info.map = map_go[0];
                map_boss_info.x_new = vgo.xnew;
                map_boss_info.y_new = vgo.ynew;
                Service.send_box_yesno(this, 16, "Thông báo",
                        (map_go[0].template.name + " rất nguy hiểm và phải mất 5 bánh "
                                + "mì để vô, bạn có thật sự muốn đi một mình?"),
                        new String[] {"Đồng ý", "Hủy"}, new byte[] {-1, -1});
            }
        } else {
            if (this.hp > 0 && this.ship_pet != null && this.map.equals(this.ship_pet.map)
                    && Math.abs(this.x - this.ship_pet.x) < 200
                    && Math.abs(this.y - this.ship_pet.y) < 200) {
                this.ship_pet.map = null;
            }
            this.map.leave_map(this, 2);
            int zone_into = 0;
            while (zone_into < (map_go.length - 1)
                    && map_go[zone_into].players.size() >= map_go[zone_into].template.max_player) {
                zone_into++;
            }
            ///
            boolean send_boat = false;
            for (int i = 0; i < DataTemplate.mSea.length; i++) {
                if (DataTemplate.mSea[i][0] == map_go[zone_into].template.id
                        && DataTemplate.mSea[i][1] != this.map.template.id) {
                    send_boat = true;
                    break;
                }
            }
            if (zone_into >= map_go.length) {
                zone_into = map_go.length - 1;
            }
            this.map = map_go[zone_into];
            this.x = vgo.xnew;
            this.y = vgo.ynew;
            this.xold = this.x;
            this.yold = this.y;
            this.map.goto_map(this);
            //
            Service.update_PK(this, this, true);
            Service.pet(this, this, true);
            this.map.send_boat(this, send_boat);
            Quest.update_map_have_side_quest(this, true);
            this.map.update_boat(this, this, true);
        }
    }

    public void change_map(Vgo vgo) throws IOException {
        this.ischangemap = false;
        this.xold = this.x;
        this.yold = this.y;
        Map[] map_go = vgo.map_go;
        if (map_go == null) {
            Service.send_box_ThongBao_OK(this, "Chưa thể đi đến map này!");
            return;
        }
        //
        QuestP quest_select = this.list_quest.get(0);
        if (quest_select != null) {
            for (int i = 0; i < MapCanGoTo.idQuest.length; i++) {
                if (MapCanGoTo.idQuest[i] <= quest_select.template.id) {
                    if ( map_go[0].template.id !=119 && map_go[0].template.id!=120 &&map_go[0].template.id!=122 && map_go[0].template.id!=123 && map_go[0].template.id!=54 && map_go[0].template.id!=58 && map_go[0].template.id!= 59 && map_go[0].template.id!=123 && map_go[0].template.id!=984 && map_go[0].template.id!=1000
                && map_go[0].template.id!=127&& map_go[0].template.id!=167&& map_go[0].template.id!=168&& map_go[0].template.id!=169&& map_go[0].template.id!=170&& map_go[0].template.id!=171&& map_go[0].template.id!=172&& map_go[0].template.id!=173&& map_go[0].template.id!=174&& map_go[0].template.id!=175&& map_go[0].template.id!=176 && MapCanGoTo.idMap[i] < map_go[0].template.id) {
                        Service.send_box_ThongBao_OK(this,
                                "Chưa thể đi đến map này khi chưa hoàn thành nhiệm vụ!");
                        return;
                    }
                    break;
                }
            }
        }
        //
        Message m = new Message(30);
        this.conn.addmsg(m);
        m.cleanup();
        System.out.println("send msg 30");
        this.map.leave_map(this, 2);
        int zone_into = 0;
        while (zone_into < (map_go[zone_into].template.max_zone - 1)
                && map_go[zone_into].players.size() >= map_go[zone_into].template.max_player) {
            zone_into++;
        }
        this.map = map_go[zone_into];
        this.x = (short) vgo.xnew;
        this.y = (short) vgo.ynew;
        this.xold = this.x;
        this.yold = this.y;
    }

    public void update_exp(long exp_up, boolean multi) throws IOException {
        if (get_eff(8) != null) {
            return;
        }
        if (multi) {
            exp_up *= Manager.gI().exp;
        }
        this.exp += exp_up;
        //
        if (this.level < 100 && this.exp >= Level.ENTRYS[this.level - 1].exp) {
            while (this.level < 100 && this.exp >= Level.ENTRYS[this.level - 1].exp) {
                this.exp -= Level.ENTRYS[this.level - 1].exp;
                this.level++;
                if (this.level < 100) {
                    this.pointAttribute += Level.ENTRYS[this.level - 1].tiemnang;
                } else {
                    this.pointAttribute += Level.ENTRYS[this.level - 2].tiemnang;
                }
            }
            Service.send_eff(this, 0, 0);
            this.update_info_to_all();
            this.update_money();
            Service.CountDown_Ticket(this);
        }
        if (this.level > 100) {
            this.level = 100;
            this.update_info_to_all();
        }
        if (level >= 100) {
            if (this.exp >= Level.LEVEL_THONGTHAO[this.thongthao]) {
                while (this.exp >= Level.LEVEL_THONGTHAO[this.thongthao]) {
                    this.exp -= Level.LEVEL_THONGTHAO[this.thongthao];
                    this.thongthao++;
                    this.pointAttributeThongThao++;
                }
                Service.send_eff(this, 0, 0);
                this.update_info_to_all();
                this.update_money();
                Service.CountDown_Ticket(this);
            }
        }
        if (this.thongthao > 99) {
            this.thongthao = 99;
            this.exp = Level.LEVEL_THONGTHAO[this.thongthao] - 1;
            this.update_info_to_all();
        }
        Message m = new Message(10);
        m.writer().writeShort(this.index_map);
        m.writer().writeShort(get_level_percent());
        m.writer().writeInt((int) exp_up);
        conn.addmsg(m);
        m.cleanup();
    }

    public void request_live_from_die(Message m2) throws IOException {
        if (this.map.template.id == 81 && this.map.map_little_garden != null) {
            return;
        }
        byte type = m2.reader().readByte();
        if (type == 1) { //
            if (pointPk < 20) {
                Service.send_box_yesno(this, 14, "Thông báo",
                        ("Hồi sinh tại chỗ mất 500 beri, bạn có muốn hồi sinh không?"),
                        new String[] {"500", "Hủy"}, new byte[] {6, -1});
            } else {
                int fee = pointPk / 4;
                Service.send_box_yesno(this, 14, "Thông báo",
                        ("Hồi sinh tại chỗ mất " + fee + " ruby, bạn có muốn hồi sinh không?"),
                        new String[] {"" + fee, "Hủy"}, new byte[] {7, -1});
            }
        } else { // ve lang
            if (this.isdie) {
                this.isdie = false;
                Vgo vgo = new Vgo();
                vgo.map_go = Map.get_map_by_id(this.id_map_save);
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
                this.goto_map(vgo);
                int hp_after_ = this.body.get_hp_max(true) / 10;
                Service.use_potion(this, 0, hp_after_);
                this.time_can_mob_atk = System.currentTimeMillis() + 1200L;
            }
        }
    }

    public void update_money() throws IOException {
        this.item.update_assets_Inventory(false);
    }
    
    public long get_vang() {
        return this.vang;
    }

    public int get_ngoc() {
        return this.kimcuong;
    }

    public int get_vnd() {
        return this.vnd;
    }

    public int get_bua() {
        return this.bua;
    }

    public synchronized void update_vang(long par) {
        if ((((long) par) + this.vang) < 2_000_000_000_000_000L) {
            this.vang += par;
        }
    }

    public synchronized void update_ngoc(long par) {
        if ((((long) par) + this.kimcuong) < 2_000_000_000L) {
            this.kimcuong += par;
        }
    }
    public synchronized boolean update_coin(int coin_exchange) throws IOException {
		String query = "SELECT `coin` FROM `accounts` WHERE BINARY `user` = '" + conn.user + "' LIMIT 1;";
		int coin_old = 0;
		Connection connection = null;
		Statement st = null;
		ResultSet rs = null;
		try {
			connection = SQL.gI().getCon();
			st = connection.createStatement();
			rs = st.executeQuery(query);
			rs.next();
			coin_old = rs.getInt("coin");
			if (coin_old + coin_exchange < 0) {
				Service.send_box_ThongBao_OK(this, "Không đủ coin");
				return false;
			}
			coin_old += coin_exchange;
			st.executeUpdate("UPDATE `accounts` SET `coin` = " + coin_old + " WHERE BINARY `user` = '" + conn.user + "'");
		} catch (SQLException e) {
			Service.send_box_ThongBao_OK(this, "Đã xảy ra lỗi");
			e.printStackTrace();
			return false;
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (st != null) {
					st.close();
				}
				if (connection != null) {
					connection.close();
				}
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
		return true;
	}
    public synchronized boolean update_status(int status_exchange) throws IOException {
		String query = "SELECT `status` FROM `accounts` WHERE BINARY `user` = '" + conn.user + "' LIMIT 1;";
		int status_old = 0;
		Connection connection = null;
		Statement st = null;
		ResultSet rs = null;
		try {
			connection = SQL.gI().getCon();
			st = connection.createStatement();
			rs = st.executeQuery(query);
			rs.next();
			status_old = rs.getInt("status");
			if (status_old + status_exchange < 0) {
				Service.send_box_ThongBao_OK(this, "Không đủ coin");
				return false;
			}
			status_old += status_exchange;
			st.executeUpdate("UPDATE `accounts` SET `status` = " + status_old + " WHERE BINARY `user` = '" + conn.user + "'");
		} catch (SQLException e) {
			Service.send_box_ThongBao_OK(this, "Đã xảy ra lỗi");
			e.printStackTrace();
			return false;
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (st != null) {
					st.close();
				}
				if (connection != null) {
					connection.close();
				}
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
		return true;
	}
    public synchronized void update_TichLuy(long par) {
        if ((((long) par) + this.tichLuy) < 2_000_000_000L) {
            this.tichLuy += par;
        }
    }

    public synchronized void update_vnd(long par) {
        if ((((long) par) + this.vnd) < 2_000_000_000L) {
            this.vnd += par;
        }
    }

    public synchronized void update_bua(long par) {
        if ((((long) par) + this.bua) < 2_000_000_000L) {
            this.bua += par;
        }
    }

    public EffTemplate get_eff(int id) {
        synchronized (this.list_eff) {
            for (int i = 0; i < this.list_eff.size(); i++) {
                EffTemplate temp = this.list_eff.get(i);
                if (temp != null && temp.id == id) {
                    return temp;
                }
            }
        }
        return null;
    }

    public void wear_item(Item_wear it) throws IOException {
        if (this.level < it.template.level) {
            Service.send_box_ThongBao_OK(this, "Chưa đủ level");
            this.use_item_3 = -1;
            return;
        }
        if (it.template.clazz != 0 && this.clazz != it.template.clazz) {
            Service.send_box_ThongBao_OK(this, "Không thể mặc vật phẩm này");
            this.use_item_3 = -1;
            return;
        }
        if (it.valueKichAn != 12) {
            it.typelock = 1;
        }
        byte index_wear = ItemTemplate3.get_it_by_id(it.template.id).typeEquip;
        if (index_wear != -1) {
            Item_wear it_inbag = it;
            item.bag3[it_inbag.index] = null;
            if (item.it_body[index_wear] != null) {
                item.add_item_bag3(item.it_body[index_wear]);
                item.it_body[index_wear] = null;
            }
            item.it_body[index_wear] = it_inbag;
            it_inbag.index = index_wear;
        }
        item.update_Inventory(-1, false);
        //
        Service.pet(this, this, false);
        Service.UpdateInfoMaincharInfo(this);
        //
        if (it.template.typeEquip == 7) {
            Skill_info sk_select = null;
            for (int i = 0; i < this.skill_point.size(); i++) {
                if (this.skill_point.get(i).temp.indexSkillInServer == 660) {
                    sk_select = this.skill_point.get(i);
                }
            }
            if (sk_select == null) {
                sk_select = new Skill_info();
                sk_select.exp = 0;
                sk_select.temp = Skill_Template.get_temp(660, 0);
                sk_select.lvdevil = 0;
                sk_select.devilpercent = 0;
                this.skill_point.add(sk_select);
                this.send_skill();
            }
        }
        //
        this.update_info_to_all();
        Service.UpdatePvpPoint(this);
        Service.update_PK(this, this, false);
        Service.getThanhTich(this, this);
        Service.Weapon_fashion(this, this, false);
        Service.charWearing(this, this, false);
        this.use_item_3 = -1;
    }

    public void update_eff() throws IOException {
        List<EffTemplate> list_temp = new ArrayList<>();
        for (int i = 0; i < list_eff.size(); i++) {
            if (list_eff.get(i).time < System.currentTimeMillis()) {
                list_temp.add(list_eff.get(i));
            }
        }
        list_eff.removeAll(list_temp);
        //
        for (int i = 0; i < list_temp.size(); i++) {
            if (list_temp.get(i).id == 7) {
                Message m2 = new Message(-71);
                m2.writer().writeByte(1);
                m2.writer().writeShort(conn.p.index_map);
                m2.writer().writeByte(0);
                m2.writer().writeInt(1); // time
                conn.p.map.send_msg_all_p(m2, conn.p, true);
                m2.cleanup();
                this.update_info_to_all();
            } else if (list_temp.get(i).id == 19) {
                if (this.map.template.id == 1000) {
                    if (this.pvp_target != null && !this.pvp_target.equals(this)
                            && !this.pvp_accept) {
                        Vgo vgo = new Vgo();
                        vgo.map_go = Map.get_map_by_id(this.id_map_save);
                        for (int i1 = 0; 1 < vgo.map_go[0].template.npcs.size(); i1++) {
                            Npc npc_temp = vgo.map_go[0].template.npcs.get(i1);
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
                        this.goto_map(vgo);
                    } else if (this.pvp_target != null && !this.pvp_target.equals(this)
                            && this.pvp_accept) {
                        Pvp.show_table(this);
                        Pvp.start_find(this);
                        Service.send_box_ThongBao_OK(this, "Đối thủ rời đi, bạn quay lại hàng chờ");
                    }
                }
            } else if (list_temp.get(i).id == 20 && this.map.map_ThuThachVeThan != null) {
                this.map.map_ThuThachVeThan.isFinish = true;
            } else if (list_temp.get(i).id == 21) {
                this.update_info_to_all();
                //
                for (int j = 0; j < this.map.players.size(); j++) {
                    Service.charWearing(this, this.map.players.get(j), false);
                }
            } else if (list_temp.get(i).id == 13) {
                this.update_info_to_all();
            }
        }
        if (list_temp.size() > 0) {
            this.send_skill();
            this.update_info_to_all();
            list_temp.clear();
        }
        // }
    }

    public void add_new_eff(int id, int param, long time) {
        synchronized (this.list_eff) {
            this.list_eff.add(new EffTemplate(id, param, (time + System.currentTimeMillis())));
        }
    }

    public void update_die() {
        for (int i = 0; i < list_eff.size(); i++) {
            if (EffTemplate.check_eff_remove_when_die(list_eff.get(i).id)) {
                list_eff.get(i).time = System.currentTimeMillis();
            }
        }
    }

    public void plus_point(Message m2) throws IOException {
        byte index = m2.reader().readByte();
        short value = m2.reader().readShort();
        if (this.pointAttribute >= value) {
            switch (index) {
                case 0: {
                    if ((this.point1 + value) > 80) {
                        return;
                    }
                    if (this.point1 < 80) {
                        this.point1 += value;
                    }
                    break;
                }
                case 1: {
                    if ((this.point2 + value) > 80) {
                        return;
                    }
                    if (this.point2 < 80) {
                        this.point2 += value;
                    }
                    break;
                }
                case 2: {
                    if ((this.point3 + value) > 80) {
                        return;
                    }
                    if (this.point3 < 80) {
                        this.point3 += value;
                    }
                    break;
                }
                case 3: {
                    if ((this.point4 + value) > 80) {
                        return;
                    }
                    if (this.point4 < 80) {
                        this.point4 += value;
                    }
                    break;
                }
                case 4: {
                    if ((this.point5 + value) > 80) {
                        return;
                    }
                    if (this.point5 < 80) {
                        this.point5 += value;
                    }
                    break;
                }
            }
            this.pointAttribute -= value;
            this.update_info_to_all();
        } else {
            Service.send_box_ThongBao_OK(this, "Không đủ điểm tiềm năng");
        }
    }

    public void reset_point(int type) throws IOException {
        switch (type) {
            case 0: {
                this.pointAttribute = (short) Level.get_total_point_by_level(this.level);
                this.point1 = 1;
                this.point2 = 1;
                this.point3 = 1;
                this.point4 = 1;
                this.point5 = 0;
                // reset thong thao;
                this.pointAttributeThongThao = this.thongthao;
                this.list_op_thongthao.clear();
            }
        }
        this.update_info_to_all();
    }

    public void send_skill() throws IOException {
        // update list can combo
        list_can_combo.clear();
        //
        Message m = new Message(-7);
        m.writer().writeByte(3);
        m.writer().writeByte(this.skill_point.size());
        for (int i = 0; i < this.skill_point.size(); i++) {
            Skill_info sk_info = this.skill_point.get(i);
            write_data_skill(m.writer(), sk_info);
            if (sk_info.temp.ID < 3 && sk_info.temp.typeSkill == 1 && sk_info.temp.Lv_RQ > -1) {
                list_can_combo.add(sk_info);
            }
        }
        conn.addmsg(m);
        m.cleanup();
    }

    public void write_data_skill(DataOutputStream dos, Skill_info sk_info) throws IOException {
        dos.writeShort(sk_info.temp.indexSkillInServer);
        dos.writeShort(sk_info.temp.ID);
        dos.writeShort(sk_info.temp.idIcon);
        dos.writeByte(sk_info.temp.typeSkill);
        dos.writeByte(sk_info.temp.typeBuff);
        dos.writeUTF(sk_info.temp.name);
        dos.writeShort(sk_info.get_eff_skill());
        dos.writeShort(sk_info.temp.range);
        //
        dos.writeByte(sk_info.temp.nTarget);
        dos.writeShort(sk_info.temp.rangeLan);
        String info_sk = sk_info.temp.getInfo(sk_info.lvdevil, this.clazz);
        dos.writeInt(sk_info.get_dame(this));
        dos.writeShort(sk_info.temp.manaLost);
        dos.writeInt(sk_info.temp.timeDelay);
        dos.writeByte(sk_info.temp.nKick);
        dos.writeUTF(info_sk);
        dos.writeByte(sk_info.temp.Lv_RQ);
        dos.writeShort(sk_info.get_percent());
        dos.writeByte(sk_info.temp.typeDevil);
        //
        dos.writeByte(sk_info.temp.op.size());
        for (int j = 0; j < sk_info.temp.op.size(); j++) {
            dos.writeByte(sk_info.temp.op.get(j).id);
            dos.writeShort(sk_info.temp.op.get(j).getParam());
        }
        dos.writeByte(sk_info.temp.idEffSpec);
        if (sk_info.temp.idEffSpec > 0) {
            dos.writeShort(sk_info.temp.perEffSpec);
            dos.writeShort(sk_info.temp.timeEffSpec);
        }
        dos.writeByte(sk_info.lvdevil);
        dos.writeByte(sk_info.devilpercent);
    }

    public void update_skill_exp(int index, long exp) throws IOException {
        exp *= Manager.gI().exp;
        if (index < 4 || index == 5000) {
            Skill_info sk_info = null;
            for (int i = 0; i < this.skill_point.size(); i++) {
                Skill_info temp = this.skill_point.get(i);
                if (temp.temp.ID == index) {
                    sk_info = temp;
                    break;
                }
            }
            if (sk_info != null) {
                if (sk_info.exp > -1) {
                    sk_info.exp += exp;
                    long exp_total = Skill_info.EXP[sk_info.temp.Lv_RQ - 1];
                    if (sk_info.exp >= exp_total && sk_info.temp.Lv_RQ >= this.level) {
                        sk_info.exp = exp_total - 1;
                    }
                    if (sk_info.exp >= exp_total) {
                        if (Skill_Template.upgrade_skill(sk_info, this.clazz)) {
                            sk_info.exp -= exp_total;
                            if (sk_info.exp >= exp_total) {
                                sk_info.exp = 1;
                            }
                            this.send_skill_lv_up(sk_info);
                            //
                            this.send_skill();
                            this.update_info_to_all();
                        } else {
                            sk_info.exp = exp_total - 1;
                            Learn_Skill.send_skill_percent(this, sk_info);
                        }
                    } else {
                        Learn_Skill.send_skill_percent(this, sk_info);
                    }
                }
            }
        }
    }

    private void send_skill_lv_up(Skill_info sk_info) throws IOException {
        Message m = new Message(-28);
        m.writer().writeByte(1);
        write_data_skill(m.writer(), sk_info);
        conn.addmsg(m);
        m.cleanup();
    }

    public int get_head() {
        if (get_eff(21) != null) { // zoombi
            return 765;
        }
        for (int i = 0; i < this.fashion.size(); i++) {
            if (this.fashion.get(i).is_use) {
                ItemFashion temp = ItemFashion.get_item(this.fashion.get(i).id);
                if (temp.mWearing[6] != -1) {
                    return temp.mWearing[6];
                }
            }
        }
        for (int i = 0; i < this.itfashionP.size(); i++) {
            if (this.itfashionP.get(i).category == 108 && this.itfashionP.get(i).is_use) {
                return this.itfashionP.get(i).icon;
            }
        }
        return this.head;
    }

    public int get_hair() {
        for (int i = 0; i < this.fashion.size(); i++) {
            if (this.fashion.get(i).is_use) {
                ItemFashion temp = ItemFashion.get_item(this.fashion.get(i).id);
                if (temp != null && (temp.mWearing[7] != -1)) {
                    return temp.mWearing[7];
                }
            }
        }
        for (int i = 0; i < this.itfashionP.size(); i++) {
            if (this.itfashionP.get(i).category == 103 && this.itfashionP.get(i).is_use) {
                if (this.itfashionP.get(i).icon == 772) {
                    return (this.itfashionP.get(i).icon + this.tocSuper);
                } else {
                    return this.itfashionP.get(i).icon;
                }
            }
        }
        return this.hair;
    }

    public void update_itfashionP(ItemFashionP temp_new, int category) throws IOException {
        temp_new.is_use = true;
        for (int i = 0; i < this.itfashionP.size(); i++) {
            if (this.itfashionP.get(i).category == category
                    && !this.itfashionP.get(i).equals(temp_new)) {
                this.itfashionP.get(i).is_use = false;
            }
        }
        update_info_to_all();
    }

    public ItemFashionP check_itfashionP(int id, int type) {
        for (int i = 0; i < this.itfashionP.size(); i++) {
            if (this.itfashionP.get(i).category == type && this.itfashionP.get(i).id == id) {
                return this.itfashionP.get(i);
            }
        }
        return null;
    }

    public ItemFashionP2 check_fashion(int id) {
        for (int i = 0; i < this.fashion.size(); i++) {
            if (this.fashion.get(i).id == id) {
                return this.fashion.get(i);
            }
        }
        return null;
    }

    public void update_fashionP2(ItemFashionP2 temp_new) throws IOException {
        temp_new.is_use = true;
        for (int i = 0; i < this.fashion.size(); i++) {
            if (!this.fashion.get(i).equals(temp_new)) {
                this.fashion.get(i).is_use = false;
            }
        }
        this.update_info_to_all();
    }

    public short[] get_fashion() {
        if (get_eff(21) != null) {
            return new short[] {-1, -2, -1, 766, -1, 767, 765, -2};
        }
        //
        short[] result = null;
        for (int i = 0; i < this.fashion.size(); i++) {
            if (this.fashion.get(i).is_use) {
                ItemFashion temp = ItemFashion.get_item(this.fashion.get(i).id);
                if (temp != null) {
                    result = new short[temp.mWearing.length];
                    for (int j = 0; j < temp.mWearing.length; j++) {
                        result[j] = temp.mWearing[j];
                    }
                    break;
                }
            }
        }
        if (result != null && this.item.it_body[0] != null) {
            result[0] = this.item.it_body[0].template.part;
        }
        return result;
    }

    public void remove_hairf() throws IOException {
        for (int i = 0; i < this.itfashionP.size(); i++) {
            if (this.itfashionP.get(i).category == 103) {
                this.itfashionP.get(i).is_use = false;
            }
        }
        for (int i = 0; i < map.players.size(); i++) {
            Player p0 = map.players.get(i);
            Service.charWearing(this, p0, false);
        }
    }

    public void remove_fashion() throws IOException {
        for (int i = 0; i < this.fashion.size(); i++) {
            this.fashion.get(i).is_use = false;
        }
        for (int i = 0; i < map.players.size(); i++) {
            Player p0 = map.players.get(i);
            Service.charWearing(this, p0, false);
        }
    }

    public void remove_headf() throws IOException {
        for (int i = 0; i < this.itfashionP.size(); i++) {
            if (this.itfashionP.get(i).category == 108) {
                this.itfashionP.get(i).is_use = false;
            }
        }
        for (int i = 0; i < map.players.size(); i++) {
            Player p0 = map.players.get(i);
            Service.charWearing(this, p0, false);
        }
    }

    public void change_new_date() {
        DateTime now = DateTime.now();
        if (!Util.is_same_day(now, date)) {
            date = now;
            time_ship = 0;
            time_nvl = 0;
            diemdanh = 0;
            diemdanhvip = 0;
        }
    }

    public Skill_info get_skill_temp(int idSkill) {
        for (int i = 0; i < this.skill_point.size(); i++) {
            if (this.skill_point.get(i).temp.Lv_RQ > 0
                    && this.skill_point.get(i).temp.ID == idSkill) {
                return this.skill_point.get(i);
            }
        }
        return null;
    }

    public void get_skill_taq_new(int id) throws IOException {
        id += 4000;
        List<Skill_info> list_remove = new ArrayList<>();
        for (int i = 0; i < this.skill_point.size(); i++) {
            Skill_info temp = this.skill_point.get(i);
            if (temp.temp.ID > 2000 && !(temp.temp.indexSkillInServer >= 660
                    && temp.temp.indexSkillInServer <= 666)) {
                // exp ac quy
                if (temp.devilpercent > 0 || temp.lvdevil > 0) {
                    int exp_total = 0;
                    switch (temp.lvdevil) {
                        case 1: {
                            exp_total = 10;
                            break;
                        }
                        case 2: {
                            exp_total = 25;
                            break;
                        }
                        case 3: {
                            exp_total = 32;
                            break;
                        }
                        case 4: {
                            exp_total = 52;
                            break;
                        }
                        case 5: {
                            exp_total = 81;
                            break;
                        }
                    }
                    Item_wear it_add = new Item_wear();
                    it_add.setup_template_by_id(11001);
                    if (it_add.template != null) {
                        it_add.option_item.add(new Option(39, exp_total));
                        this.item.add_item_bag3(it_add);
                        it_add.typelock = 1;
                    }
                    this.item.update_Inventory(-1, false);
                }
                //
                Learn_Skill.remove_skill(this, temp);
                list_remove.add(temp);
            }
        }
        this.skill_point.removeAll(list_remove);
        list_remove.clear();
        switch (id) {
            case 4032: {
                int[] id_ = new int[] {478, 476, 475};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4033: {
                int[] id_ = new int[] {480, 479, 477};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4034: {
                int[] id_ = new int[] {483, 482, 481};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4088: {
                int[] id_ = new int[] {484, 485, 486};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4090: {
                int[] id_ = new int[] {514, 513, 512};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4091: {
                int[] id_ = new int[] {517, 516, 515};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4092: {
                int[] id_ = new int[] {523, 522, 519};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4093: {
                int[] id_ = new int[] {521, 520, 518};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4160: {
                int[] id_ = new int[] {527, 526, 525, 524};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4161: {
                int[] id_ = new int[] {531, 530, 529, 528};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4219: {
                int[] id_ = new int[] {538, 537, 536};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4220: {
                int[] id_ = new int[] {535, 534, 533};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4240: {
                int[] id_ = new int[] {542, 541, 539, 540};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4316: {
                int[] id_ = new int[] {548, 547, 546};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4317: {
                int[] id_ = new int[] {545, 544, 543};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4318: {
                // String[] name_ = new String[]{"Thần hộ thể", "Tăng trọng", "Sức nặng ngàn
                // cân"};
                int[] id_ = new int[] {551, 550, 549};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
            case 4427: {
                // String[] name_ = new String[]{"Dòng chảy ma pháp", "Vòng xoáy ma pháp", "Giải
                // phóng", "Xoáy đen"};
                int[] id_ = new int[] {656, 657, 658, 659};
                for (int i = 0; i < id_.length; i++) {
                    Skill_info sk_add = new Skill_info();
                    sk_add.exp = 0;
                    sk_add.temp = Skill_Template.get_temp(id_[i], sk_add.exp);
                    if (sk_add.temp != null) {
                        list_remove.add(sk_add);
                    }
                }
                break;
            }
        }
        this.skill_point.addAll(list_remove);
        list_remove.clear();
        this.send_skill();
        this.update_info_to_all();
    }

    public void update_info_to_all() throws IOException {
        Service.Main_char_Info(this);
        Service.getThanhTich(this, this);
        Service.update_PK(this, this, false);
        Service.pet(this, this, false);
        for (int i = 0; i < this.map.players.size(); i++) {
            Player p0 = this.map.players.get(i);
            if (p0.index_map != this.index_map) {
                this.map.send_char_in4_inmap(p0, this.index_map);
                Service.getThanhTich(this, p0);
                Service.update_PK(this, p0, false);
                Service.pet(this, p0, false);
            }
        }
    }

    public int getNumPassive() {
        if (this.level < 10) {
            return 0;
        } else if (this.level < 20) {
            return 1;
        } else if (this.level < 30) {
            return 2;
        } else if (this.level < 40) {
            return 3;
        } else {
            return 4;
        }
    }

    public ItemBoatP check_itboat(int id) {
        for (int i = 0; i < this.itemboat.size(); i++) {
            if (this.itemboat.get(i).id == id) {
                return this.itemboat.get(i);
            }
        }
        return null;
    }

    public void update_new_part_boat(ItemBoatP temp_new) {
        byte type_boat_new = ItemBoat.get_item(temp_new.id).type;
        for (int i = 0; i < this.itemboat.size(); i++) {
            if (!this.itemboat.get(i).equals(temp_new)
                    && type_boat_new == ItemBoat.get_item(this.itemboat.get(i).id).type) {
                this.itemboat.get(i).is_use = false;
            }
        }
    }

    public short[] get_part_boat() {
        short[] result = new short[] {0, 1, 2, 3};
        for (int i = 0; i < this.itemboat.size(); i++) {
            if (this.itemboat.get(i).is_use) {
                ItemBoat temp = ItemBoat.get_item(this.itemboat.get(i).id);
                result[temp.type] = temp.idimg;
            }
        }
        return result;
    }

    public short get_hat() {
        short[] fashion = this.get_fashion();
        Item_wear it_w = this.item.it_body[1];
        if (!this.is_show_hat || it_w == null) {
            return -1;
        } else if (fashion != null && fashion[1] != -1) {
            return fashion[1];
        } else {
            return ItemTemplate3.get_it_by_id(it_w.template.id).part;
        }
    }

    public byte get_index_full_set() {
        byte result = 0;
        for (int i = 11; i <= 16; i++) {
            for (int j = 0; j < 6; j++) {
                Item_wear it = this.item.it_body[j];
                if (it != null && (it.levelup >= i || (it.template.typeEquip != 0
                        && it.template.typeEquip != 3 && it.template.typeEquip != 5))) {
                    result++;
                }
            }
            if (result < 6) {
                return (byte) (i - 11);
            } else {
                result = 0;
            }
        }
        return result;
    }

    public short get_percent_mana_use_skill() {
        return 0;
    }

    public void send_kich_an() throws IOException {
        Message m = new Message(57);
        m.writer().writeByte(9); // reset cooldown skill
        m.writer().writeShort(0);
        m.writer().writeShort(this.index_map);
        m.writer().writeByte(0);
        m.writer().writeByte(0);
        m.writer().writeInt(0);
        conn.addmsg(m);
        m.cleanup();
    }

    public QuestP get_quest(int id) {
        for (int i = 0; i < this.list_quest.size(); i++) {
            if (this.list_quest.get(i).template.index == id) {
                return this.list_quest.get(i);
            }
        }
        return null;
    }

    public void update_point_pk(int point) throws IOException {
        this.pointPk += point;
        if (this.pointPk < 0) {
            this.pointPk = 0;
        }
        if (this.pointPk > 100_000) {
            this.pointPk = 100_000;
        }
        Message m = new Message(-45);
        m.writer().writeInt(this.pointPk);
        m.writer().writeByte(-1); // fake
        conn.addmsg(m);
        m.cleanup();
    }

    public void update_num_item_quest(int type, int id_mob, int value) throws IOException {
        QuestP questCheck = null;
        for (int j = this.list_quest.size() - 1; j >= 0; j--) {
            QuestP tempP = this.list_quest.get(j);
            if (tempP.template.statusQuest == 1) {
                for (int k = 0; k < tempP.data.length; k++) {
                    if (tempP.data[k].length == 4 && tempP.data[k][0] == type
                            && tempP.data[k][1] == id_mob && tempP.data[k][3] < tempP.data[k][2]) {
                        questCheck = tempP;
                        break;
                    }
                }
            }
            if (questCheck != null) {
                break;
            }
        }
        if (questCheck != null) {
            boolean finished = true;
            for (int j = 0; j < questCheck.data.length; j++) {
                if (questCheck.data[j][0] == type && questCheck.data[j][1] == id_mob
                        && questCheck.data[j][3] < questCheck.data[j][2]) {
                    questCheck.data[j][3] += value;
                    if (questCheck.data[j][3] >= questCheck.data[j][2]) {
                        questCheck.data[j][3] = questCheck.data[j][2];
                    } else {
                        finished = false;
                    }
                    // update notice quest progress
                    Message mq = new Message(25);
                    mq.writer().writeShort(id_mob);
                    mq.writer().writeByte(questCheck.data[j][0] == 1 ? 1 : 5);
                    mq.writer().writeShort(questCheck.data[j][3]);
                    mq.writer().writeShort(questCheck.data[j][2]);
                    this.conn.addmsg(mq);
                    mq.cleanup();
                } else {
                    if (questCheck.data[j][3] < questCheck.data[j][2]) {
                        finished = false;
                    }
                }
            }
            if (finished) {
                Quest.remove_old_and_send_next(this, questCheck);
                // send notice quest finish
                Message mnext = new Message(-31);
                mnext.writer().writeByte(0);
                mnext.writer().writeUTF("Nhiệm vụ hoàn thành!");
                mnext.writer().writeByte(5);
                mnext.writer().writeShort(-1);
                this.conn.addmsg(mnext);
                mnext.cleanup();
            }
        } else {
            for (int j = 0; j < this.list_quest.size(); j++) {
                QuestP tempP = this.list_quest.get(j);
                if (tempP.template.statusQuest == 2) {
                    Quest temp_old_quest = Quest.get_quest(tempP.template.id - 1);
                    if (temp_old_quest.statusQuest == 1 && temp_old_quest.data_quest.length > 0) {
                        for (int i = 0; i < temp_old_quest.data_quest.length; i++) {
                            if (temp_old_quest.data_quest[i][0] == type
                                    && temp_old_quest.data_quest[i][1] == id_mob) {
                                Message mq = new Message(25);
                                mq.writer().writeShort(id_mob);
                                mq.writer().writeByte(temp_old_quest.data_quest[i][0] == 1 ? 1 : 5);
                                mq.writer().writeShort(temp_old_quest.data_quest[i][2]);
                                mq.writer().writeShort(temp_old_quest.data_quest[i][2]);
                                this.conn.addmsg(mq);
                                mq.cleanup();
                            }
                        }
                        break;
                    }
                }
            }
        }
    }

    public int get_ticket() {
        return ticket;
    }

    public void update_ticket(int i) {
        if (i < 0 && this.cd_ticket_next < System.currentTimeMillis()) {
            this.cd_ticket_next = System.currentTimeMillis() + (60_000L * 10); // 10p
        }
        this.ticket += i;
        if (this.ticket >= this.get_ticket_max()) {
            // this.ticket = (short) this.get_ticket_max();
            this.cd_ticket_next = System.currentTimeMillis() + (60_000L * 10); // 10p
        }
        if (this.ticket >= 32000) {
            this.ticket = (short) 32000;
            this.cd_ticket_next = System.currentTimeMillis() + (60_000L * 10); // 10p
        }
    }

    public void update_pvp_ticket(int i) {
        if (i < 0 && this.cd_pvp_next < System.currentTimeMillis()) {
            this.cd_pvp_next = System.currentTimeMillis() + (60_000L * 60 * 2); // 2h
        }
        this.pvp_ticket += i;
        if (this.pvp_ticket >= this.get_pvp_ticket_max()) {
            // this.pvp_ticket = (byte) this.get_pvp_ticket_max();
            this.cd_pvp_next = System.currentTimeMillis() + (60_000L * 60 * 2); // 2h
        }
        if (this.pvp_ticket >= 32000) {
            this.pvp_ticket = (byte) 32000;
            this.cd_pvp_next = System.currentTimeMillis() + (60_000L * 60 * 2); // 2h
        }
    }

    public void update_key_boss(int i) {
        if (i < 0 && this.cd_keyboss_next < System.currentTimeMillis()) {
            this.cd_keyboss_next = System.currentTimeMillis() + (60_000L * 60 * 1); // 1h
        }
        this.key_boss += i;
        if (this.key_boss >= this.get_key_boss_max()) {
            // this.key_boss = (byte) this.get_key_boss_max();
            this.cd_keyboss_next = System.currentTimeMillis() + (60_000L * 60 * 1); // 1h
        }
        if (this.key_boss >= 32000) {
            this.key_boss = (byte) 32000;
            this.cd_keyboss_next = System.currentTimeMillis() + (60_000L * 60 * 1); // 1h
        }
    }

    public int get_ticket_max() {
        if (level < 10) {
            return 20;
        } else if (level < 20) {
            return 22;
        } else if (level < 30) {
            return 24;
        } else if (level < 40) {
            return 26;
        } else if (level < 50) {
            return 28;
        } else if (level <= 100) {
            return 30;
        } else {
            return 20;
        }
    }

    public int get_pvp_ticket_max() {
        if (level < 10) {
            return 3;
        } else if (level < 20) {
            return 4;
        } else if (level <= 100) {
            return 5;
        } else {
            return 3;
        }
    }

    public int get_key_boss_max() {
        if (level < 10) {
            return 3;
        } else if (level < 20) {
            return 4;
        } else if (level < 30) {
            return 5;
        } else if (level < 40) {
            return 6;
        } else if (level < 50) {
            return 7;
        } else if (level <= 100) {
            return 8;
        } else {
            return 3;
        }
    }

    public int get_pvp_ticket() {
        return this.pvp_ticket;
    }

    public int get_key_boss() {
        return this.key_boss;
    }

    public boolean check_already_have_devil_fruit() {
        short[] id_check = new short[] {32, 33, 34, 86, 87, 88, 90, 91, 92, 93, 160, 161, 219, 220,
                240, 316, 317, 318, 427};
        for (int i = 0; i < id_check.length; i++) {
            if (item.total_item_bag_by_id(4, id_check[i]) > 0
                    || item.total_item_box_by_id(4, id_check[i]) > 0) {
                return true;
            }
        }
        return false;
    }

    public int getTichLuy() {
        return tichLuy;
    }

    public void update_pvpPoint(int i) {
        this.pvppoint += i;
        if (this.pvppoint < 0) {
            this.pvppoint = 0;
        }
    }

    public int get_pvpPoint() {
        return pvppoint;
    }

    public int get_tyle_ghep_dial() {
        int result = 0;
        Skill_info sk_select = null;
        for (int i = 0; i < this.skill_point.size(); i++) {
            if (this.skill_point.get(i).temp.indexSkillInServer >= 661
                    && this.skill_point.get(i).temp.indexSkillInServer <= 666) {
                sk_select = this.skill_point.get(i);
                break;
            }
        }
        if (sk_select != null) {
            result = sk_select.temp.Lv_RQ + 7;
        }
        return (result * 3);
    }

    public ItemBag47 get_daHanhTrinh(int cat) {
        int index = -1;
        for (int i = 0; i < HanhTrinh.LANG.length; i++) {
            if (HanhTrinh.LANG[i] == cat) {
                index = i;
                break;
            }
        }
        if (index > -1) {
            for (int i = 0; i < this.daHanhTrinh.size(); i++) {
                if (this.daHanhTrinh.get(i).category == index
                        && this.daHanhTrinh.get(i).quant == 1) {
                    return this.daHanhTrinh.get(i);
                }
            }
        }
        return null;
    }

    public int get_icon_daHanhTrinh(int cat) {
        for (int i = 0; i < this.daHanhTrinh.size(); i++) {
            if (this.daHanhTrinh.get(i).category == cat && this.daHanhTrinh.get(i).quant == 1) {
                return ItemTemplate4.get_it_by_id(this.daHanhTrinh.get(i).id).icon;
            }
        }
        return -1;
    }

    public List<ItemBag47> get_list_daHanhTrinh_total(int cat) {
        int index = -1;
        for (int i = 0; i < HanhTrinh.LANG.length; i++) {
            if (HanhTrinh.LANG[i] == cat) {
                index = i;
                break;
            }
        }
        List<ItemBag47> result = new ArrayList<>();
        if (index > -1) {
            for (int i = 0; i < this.daHanhTrinh.size(); i++) {
                if (this.daHanhTrinh.get(i).category == index
                        && this.daHanhTrinh.get(i).quant == 0) {
                    result.add(this.daHanhTrinh.get(i));
                }
            }
        }
        return result;
    }

    public void update_wanted_point(int i) {
        long value = this.wanted_price;
        if ((value + i) <= 2_000_000_000L) {
            this.wanted_price += i;
            if (this.wanted_price < 0) {
                this.wanted_price = 0;
            }
        }
    }

    public int get_wanted_point() {
        return this.wanted_price;
    }

    public MyPet get_pet() {
        for (int i = 0; i < this.my_pet.size(); i++) {
            if (this.my_pet.get(i).isUse) {
                return this.my_pet.get(i);
            }
        }
        return null;
    }
}
