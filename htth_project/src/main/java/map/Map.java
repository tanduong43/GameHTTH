package map;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import activities.Dungeon;
import activities.LittleGarden;
import activities.Pvp;
import activities.Trade;
import activities.Wanted;
import client.Buff;
import client.Clan;
import client.MapBossInfo;
import client.MyPet;
import client.Player;
import client.Quest;
import client.Wanted_Chest;
import core.BXH;
import core.Manager;
import core.MenuController;
import core.Service;
import core.Util;
import io.Message;
import template.DanhHieuTemplate;
import template.DataTemplate;
import template.EffTemplate;
import template.FriendTemp;
import template.GiftBox;
import template.ItemFashion;
import template.ItemFashionP2;
import template.ItemMap;
import template.ItemTemplate3;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.Item_wear;
import template.Map_Little_Garden;
import template.Map_Pvp_Clan;
import template.Map_ThuThachVeThan;
import template.Map_clan_resource;
import template.Map_pvp;
import template.Option_Dame_Msg;
import template.Ship_pet;
import template.Skill_info;
import template.Top_Dame;

/**
 *
 * @author Truongbk
 */
public class Map implements Runnable {

    public static List<Map[]> ENTRYS = new ArrayList<>();
    private static List<Map> MAP_PLUS = new ArrayList<>();
    // public static int id_eff = 0;
    public static byte weather = -1;
    public static byte weather_level = 1;
    public MapTemplate template;
    public Map_ThuThachVeThan map_ThuThachVeThan;
    public boolean running;
    public Thread mythread;
    public List<Player> players = new ArrayList<>();
    public int[] list_mob;
    public byte zone_id;
    public Map_pvp map_pvp;
    public Dungeon map_dungeon;
    public Map_clan_resource clan_resource;
    public Map_Little_Garden map_little_garden;
    public Map_Pvp_Clan map_pvp_clan;
    public activities.BossHunt map_bossHunt;
    public ItemMap[] list_it_map = new ItemMap[1_000];
    public boolean can_PK = true;
    public long lastBotActionTime = 0;

    public Map() {
        this.running = false;
        mythread = new Thread(this);
    }

    public static boolean is_map_boss(int id) {
        return id == 5 || id == 13 || id == 21 || id == 29 || id == 37 || id == 45 || id == 37
                || id == 45 || id == 53 || id == 73 || id == 87 || id == 102 || id == 127
                || id == 198;
    }

    public static boolean is_map_dungeon(int id) {
        // HangDong: 167-176 | TowerChallenge: 500-512 | NamieTreasureDefense: 513
        // BossHunt: 201-207 | ClanResource: 999
        return (id >= 167 && id <= 176) || (id >= 201 && id <= 207)
                || (id >= 500 && id <= 512) || id == 513 || id == 999;
    }

    public static void add_map_plus(Map map_boss) {
        synchronized (Map.MAP_PLUS) {
            Map.MAP_PLUS.add(map_boss);
        }
    }

    public static void remove_map_plus(Map map_boss) {
        synchronized (Map.MAP_PLUS) {
            Map.MAP_PLUS.remove(map_boss);
        }
    }

    public static List<Map> get_map_plus() {
        return Map.MAP_PLUS;
    }

    public static boolean isMapLang(int id) {
        for (int i = 0; i < MenuController.ID_MAP_LANG.length; i++) {
            if (id == MenuController.ID_MAP_LANG[i]) {
                return true;
            }
        }
        return false;
    }

    public void start_map() {
        this.mythread.start();
    }

    public void stop_map() {
        this.running = false;
        this.mythread.interrupt();
    }

    @Override
    public void run() {
        this.running = true;
        long time1 = 0;
        long time2 = 0;
        long time3 = 0;
        while (this.running) {
            try {
                time1 = System.currentTimeMillis();
                update();
                time2 = System.currentTimeMillis();
                time3 = (1_000L - (time2 - time1));
                if (time3 > 0) {
                    Thread.sleep(time3);
                }
            } catch (InterruptedException e) {
            } catch (IOException e) {
            } catch (Exception e) {
                e.printStackTrace();
                System.out.println("err map " + this.template.name + " " + this.zone_id);
            }
        }
    }

    private void update() throws IOException {
        update_mob();
        update_player();
        update_item_map();
        update_map_dungeon();
        update_map_pvp();
        update_map_little_garden();
        update_map_pvp_clan();
        update_map_ThuThachVeThan();
        update_map_Wanted();
        update_map_bossHunt();
        update_map_HangDong();
    }

    private void update_map_HangDong() {
        if (this.map_dungeon != null && this.map_dungeon instanceof activities.HangDong) {
            ((activities.HangDong) this.map_dungeon).checkTransition();
        }
    }

    private void update_map_bossHunt() throws IOException {
        if (this.map_bossHunt == null)
            return;
        activities.BossHunt hunt = this.map_bossHunt;
        if (!hunt.active) {
            System.out.println("[BossHunt] map_bossHunt inactive on map "
                    + this.template.id + ", clearing reference.");
            this.map_bossHunt = null;
            return;
        }
        // Kiểm tra thất bại (tất cả tử trận)
        if (hunt.isFailed) {
            if (System.currentTimeMillis() >= hunt.failTime) {
                System.out.println("[BossHunt] All players died. Ending dungeon.");
                this.map_bossHunt = null;
                hunt.returnAllToVillage("Thành viên trong tổ đội đã tử trận!", hunt.registeredMapId);
            }
            return;
        }
        // Chỉ map có boss của tầng hiện tại mới xử lý
        if (hunt.maps.isEmpty() || !hunt.maps.get(hunt.currentFloor).equals(this)) {
            return;
        }
        // Kiểm tra hết giờ (2 phút)
        if (hunt.floorTime > 0 && System.currentTimeMillis() >= hunt.floorTime) {
            System.out.println("[BossHunt] Floor " + (hunt.currentFloor + 1)
                    + " timed out! Returning players to registered village mapId=" + hunt.registeredMapId);
            this.map_bossHunt = null;
            hunt.returnAllToVillage("Đã hết thời gian khiêu chiến Boss!", hunt.registeredMapId);
            return;
        }
        // Kiểm tra nếu boss trên map này đã chết hết
        boolean allBossDead = !hunt.mobs.isEmpty();
        for (Mob mob : hunt.mobs) {
            if (mob.map.equals(this) && !mob.isdie) {
                allBossDead = false;
                break;
            }
        }
        if (!allBossDead)
            return;
        // Tất cả boss tầng này đã chết
        if (!hunt.isTransitioning) {
            hunt.isTransitioning = true;
            int nextFloor = hunt.currentFloor + 1;
            System.out.println("[BossHunt] Floor " + (hunt.currentFloor + 1)
                    + " cleared! All bosses dead. Next floor=" + (nextFloor + 1)
                    + "/" + activities.BossHunt.BOSS_MAPS.length);
            if (nextFloor >= activities.BossHunt.BOSS_MAPS.length) {
                // Đã hoàn thành Boss7 (tầng cuối) -> báo thắng, sau 5s về làng đã đăng ký
                hunt.transitionTime = System.currentTimeMillis() + 5_000L;
                System.out.println("[BossHunt] FINAL FLOOR COMPLETE! Returning to registered village in 5s.");
                for (client.Player member : hunt.members) {
                    if (member != null && member.conn != null && member.conn.connected) {
                        hunt.giveRewardsForFloor(member, hunt.currentFloor, true);
                        core.Service.send_time_cool_down(member,
                                hunt.transitionTime, "Quay về làng", 2);
                    }
                }
            } else {
                // Chuyển sang tầng tiếp theo sau 3 giây
                hunt.transitionTime = System.currentTimeMillis() + 3_000L;
                System.out.println("[BossHunt] Transitioning to floor " + (nextFloor + 1) + " in 3s.");
                for (client.Player member : hunt.members) {
                    if (member != null && member.conn != null && member.conn.connected) {
                        hunt.giveRewardsForFloor(member, hunt.currentFloor, false);
                    }
                }
            }
        } else {
            // Đang chờ transition
            if (hunt.transitionTime > 0 && System.currentTimeMillis() >= hunt.transitionTime) {
                hunt.transitionTime = 0;
                // Xóa liên kết map_bossHunt trên map cũ để nó không chạy lại
                this.map_bossHunt = null;
                int nextFloor = hunt.currentFloor + 1;
                if (nextFloor >= activities.BossHunt.BOSS_MAPS.length) {
                    System.out.println(
                            "[BossHunt] Returning all players to registered village mapId=" + hunt.registeredMapId);
                    hunt.returnAllToVillage(null, hunt.registeredMapId);
                } else {
                    System.out.println("[BossHunt] Starting floor " + (nextFloor + 1));
                    hunt.isTransitioning = false;
                    hunt.startFloor(nextFloor);
                }
            }
        }
    }

    private void update_map_Wanted() throws IOException {
        if (this.template.id == 119) {
            Player[] p0 = Wanted.get_p_random_waiting();
            if (p0 != null && p0[0] != null && p0[1] != null) {
                p0[0].map.leave_map(p0[0], 2);
                p0[1].map.leave_map(p0[1], 2);
                p0[0].type_pk = -1;
                p0[1].type_pk = -1;
                //
                // create map
                short[] mapID = new short[] { 120, 122, 123 };
                Map maptemp = Map.get_map_by_id(mapID[Util.random(mapID.length)])[0];
                Map map_create = new Map();
                map_create.template = maptemp.template;
                map_create.zone_id = (byte) 0;
                map_create.list_mob = new int[0];
                //
                p0[0].map = map_create;
                p0[0].x = 320;
                p0[0].y = 240;
                p0[0].xold = p0[0].x;
                p0[0].yold = p0[0].y;
                p0[0].map.goto_map(p0[0]);
                Service.update_PK(p0[0], p0[0], true);
                Service.pet(p0[0], p0[0], true);
                Quest.update_map_have_side_quest(p0[0], true);
                //
                p0[1].map = map_create;
                p0[1].x = 380;
                p0[1].y = 240;
                p0[1].xold = p0[1].x;
                p0[1].yold = p0[1].y;
                p0[1].map.goto_map(p0[1]);
                Service.update_PK(p0[1], p0[1], true);
                Service.pet(p0[1], p0[1], true);
                Quest.update_map_have_side_quest(p0[1], true);
                //
                map_create.map_pvp = new Map_pvp();
                map_create.map_pvp.time_pvp = 1;
                map_create.map_pvp.status_pvp = 0;
                map_create.map_pvp.num_win_p1 = 0;
                map_create.map_pvp.num_win_p2 = 0;
                map_create.map_pvp.type_map = 2; // map fight truy na
                map_create.start_map();
                Map.add_map_plus(map_create);
                // System.out.println("map: " + map_create.hashCode());
            } else {
                Player p_waiting = Wanted.get_player_waiting_too_long();
                if (p_waiting != null) {
                    Wanted.wait_to_enter_round(p_waiting);

                    // create map
                    short[] mapID = new short[] { 120, 122, 123 };
                    Map maptemp = Map.get_map_by_id(mapID[Util.random(mapID.length)])[0];
                    Map map_create = new Map();
                    map_create.template = maptemp.template;
                    map_create.zone_id = (byte) 0;
                    map_create.list_mob = new int[0];

                    // set up human
                    p_waiting.map.leave_map(p_waiting, 2);
                    p_waiting.type_pk = -1;
                    p_waiting.map = map_create;
                    p_waiting.x = 320;
                    p_waiting.y = 240;
                    p_waiting.xold = p_waiting.x;
                    p_waiting.yold = p_waiting.y;
                    p_waiting.map.goto_map(p_waiting);
                    Service.update_PK(p_waiting, p_waiting, true);
                    Service.pet(p_waiting, p_waiting, true);
                    Quest.update_map_have_side_quest(p_waiting, true);

                    // select opponent name
                    String opponentName = p_waiting.name;
                    java.sql.Connection connDb = null;
                    java.sql.Statement stmtDb = null;
                    java.sql.ResultSet rsDb = null;
                    try {
                        connDb = database.SQL.gI().getCon();
                        stmtDb = connDb.createStatement();
                        rsDb = stmtDb.executeQuery("SELECT `name` FROM `players` WHERE `name` != '" + p_waiting.name
                                + "' ORDER BY RAND() LIMIT 1;");
                        if (rsDb.next()) {
                            opponentName = rsDb.getString("name");
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    } finally {
                        try {
                            if (rsDb != null)
                                rsDb.close();
                            if (stmtDb != null)
                                stmtDb.close();
                            if (connDb != null)
                                connDb.close();
                        } catch (Exception e) {
                        }
                    }
                    Player bot = new Player(new io.Session(null), opponentName);
                    bot.isBot = true;
                    bot.conn.user = "bot_" + opponentName;
                    bot.conn.pass = "bot";
                    bot.conn.status = 1;

                    if (!bot.setup() || opponentName.equals(p_waiting.name)) {
                        bot = new Player(new io.Session(null), p_waiting.name);
                        bot.isBot = true;
                        bot.conn.user = "bot_" + p_waiting.name;
                        bot.conn.pass = "bot";
                        bot.conn.status = 1;
                        bot.setup();
                        bot.name = "Bản Sao " + p_waiting.name;
                    }

                    bot.id = -p_waiting.id;
                    bot.index_map = (short) bot.id;
                    bot.hp = bot.body.get_hp_max(true);
                    bot.mp = bot.body.get_mp_max(true);

                    // set up bot in map
                    bot.type_pk = -1;
                    bot.map = map_create;
                    bot.x = 380;
                    bot.y = 240;
                    bot.xold = bot.x;
                    bot.yold = bot.y;
                    bot.map.goto_map(bot);
                    Service.update_PK(bot, bot, true);
                    Service.pet(bot, bot, true);
                    Quest.update_map_have_side_quest(bot, true);

                    // start map
                    map_create.map_pvp = new Map_pvp();
                    map_create.map_pvp.time_pvp = 1;
                    map_create.map_pvp.status_pvp = 0;
                    map_create.map_pvp.num_win_p1 = 0;
                    map_create.map_pvp.num_win_p2 = 0;
                    map_create.map_pvp.type_map = 2; // map fight truy na
                    map_create.start_map();
                    Map.add_map_plus(map_create);
                }
            }
        }
    }

    private void update_map_ThuThachVeThan() throws IOException {
        if (this.map_ThuThachVeThan != null) {
            if (this.map_ThuThachVeThan.time_state < System.currentTimeMillis()
                    && (this.map_ThuThachVeThan.isFinish || players.size() != 2)) {
                Vgo vgo = new Vgo();
                vgo.map_go = Map.get_map_by_id(189);
                vgo.xnew = 380;
                vgo.ynew = 260;
                List<Player> playerList = new ArrayList<>();
                for (int i = 0; i < players.size(); i++) {
                    playerList.add(players.get(i));
                }
                playerList.forEach(l -> {
                    try {
                        l.key_red_line.clear();
                        if (this.map_ThuThachVeThan.isReceiv) {
                            l.update_skill_exp(5000, 50);
                            boolean receiv_material = true;
                            if (l.time_ttvt < 50) {
                                l.time_ttvt++;
                                Skill_info sk_select = null;
                                for (int i = 0; i < l.skill_point.size(); i++) {
                                    if (l.skill_point.get(i).temp.indexSkillInServer >= 661
                                            && l.skill_point
                                                    .get(i).temp.indexSkillInServer <= 666) {
                                        sk_select = l.skill_point.get(i);
                                        break;
                                    }
                                }
                                if (sk_select == null) {
                                    l.update_key_boss(1);
                                    l.update_money();
                                    Service.CountDown_Ticket(l);
                                    receiv_material = false;
                                }
                            }
                            int num1 = Util.random(20, 50);
                            int num2 = Util.random(20, 50);
                            int num3 = Util.random(2, 7);
                            for (int i = 0; i < l.skill_point.size(); i++) {
                                if (l.skill_point.get(i).temp.indexSkillInServer >= 661
                                        && l.skill_point.get(i).temp.indexSkillInServer <= 666) {
                                    int percent = (l.skill_point.get(i).temp.indexSkillInServer - 661)
                                            * 50;
                                    num1 = (num1 * (100 + percent)) / 100;
                                    num2 = (num2 * (100 + percent)) / 100;
                                    num3 = (num3 * (100 + percent)) / 100;
                                    break;
                                }
                            }
                            if (receiv_material) {
                                num1 *= 2;
                                num2 *= 2;
                                num3 *= 2;
                                //
                                l.item.add_item_bag47(4, 451, num1);
                                l.item.add_item_bag47(4, 454, num2);
                                l.item.add_item_bag47(7, 13, num3);
                                l.item.update_Inventory(-1, false);
                            }
                        }
                        l.goto_map(vgo);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                });
                running = false;
                this.map_ThuThachVeThan = null;
            }
        }
    }

    private void update_map_little_garden() {
        if (this.map_little_garden != null) {
            for (int i = 0; i < this.map_little_garden.mobs.size(); i++) {
                Mob mob = this.map_little_garden.mobs.get(i);
                if (mob != null) {
                    if (mob.isdie) {
                        if (mob.time_refresh < System.currentTimeMillis()) {
                            mob.isdie = false;
                            mob.hp = mob.hp_max;
                            mob.id_target = -1;
                            //
                            try {
                                Message m_local = new Message(1);
                                m_local.writer().writeByte(1);
                                m_local.writer().writeShort(mob.index);
                                m_local.writer().writeShort(mob.x);
                                m_local.writer().writeShort(mob.y);
                                this.send_msg_all_p(m_local, null, true);
                                m_local.cleanup();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                }
            }
            //
            if (this.map_little_garden.is_finish
                    || this.map_little_garden.time < System.currentTimeMillis()) {
                // tong ket
                int xp_receiv1 = 1500;
                int xp_receiv2 = 1500;
                int rb_receiv1 = 250;
                int rb_receiv2 = 250;
                if (this.map_little_garden.hp_1 <= 0) {
                    xp_receiv2 = 2000;
                    xp_receiv1 = 1000;
                    rb_receiv2 = 500;
                    rb_receiv1 = 200;
                } else if (this.map_little_garden.hp_2 <= 0) {
                    xp_receiv1 = 2000;
                    xp_receiv2 = 1000;
                    rb_receiv1 = 500;
                    rb_receiv2 = 200;
                }
                //
                this.map_little_garden.clan1.update_xp(xp_receiv1);
                this.map_little_garden.clan1.update_ruby(rb_receiv1);
                for (int i1 = 0; i1 < this.map_little_garden.clan1.members.size(); i1++) {
                    Player p0 = Map.get_player_by_name_allmap(
                            this.map_little_garden.clan1.members.get(i1).name);
                    if (p0 != null) {
                        try {
                            Clan.set_data(p0, false);
                            Clan.send_money(p0, false);
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                }
                try {
                    this.map_little_garden.clan1.chat_on_board(
                            this.map_little_garden.clan1.members.get(0).id,
                            this.map_little_garden.clan1.members.get(0).name,
                            ("Phó bản khổng lồ với: " + this.map_little_garden.clan2.name
                                    + ": nhận được " + xp_receiv1 + " xp băng và " + rb_receiv1
                                    + " ruby băng"),
                            -3);
                } catch (IOException e) {
                    e.printStackTrace();
                }
                //
                this.map_little_garden.clan2.update_xp(xp_receiv2);
                this.map_little_garden.clan2.update_ruby(rb_receiv2);
                for (int i1 = 0; i1 < this.map_little_garden.clan2.members.size(); i1++) {
                    Player p0 = Map.get_player_by_name_allmap(
                            this.map_little_garden.clan2.members.get(i1).name);
                    if (p0 != null) {
                        try {
                            Clan.set_data(p0, false);
                            Clan.send_money(p0, false);
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                }
                try {
                    this.map_little_garden.clan2.chat_on_board(
                            this.map_little_garden.clan2.members.get(0).id,
                            this.map_little_garden.clan2.members.get(0).name,
                            ("Phó bản khổng lồ với: " + this.map_little_garden.clan1.name
                                    + ": nhận được " + xp_receiv2 + " xp băng và " + rb_receiv2
                                    + " ruby băng"),
                            -3);
                } catch (IOException e) {
                    e.printStackTrace();
                }
                this.map_little_garden.clan1.map_create = null;
                this.map_little_garden.clan2.map_create = null;
                //
                // try {
                Vgo vgo = new Vgo();
                vgo.map_go = Map.get_map_by_id(33);
                vgo.xnew = 710;
                vgo.ynew = 320;
                List<Player> playerList = new ArrayList<>();
                for (int i = 0; i < players.size(); i++) {
                    playerList.add(players.get(i));
                }
                playerList.forEach(l -> {
                    try {
                        l.goto_map(vgo);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                });
                // } catch (IOException e) {
                // e.printStackTrace();
                // }
                this.running = false;
            }
        }
    }

    private void update_map_pvp_clan() {
        if (this.map_pvp_clan != null) {
            if (this.map_pvp_clan.is_finish
                    || this.map_pvp_clan.time_end < System.currentTimeMillis()) {
                this.map_pvp_clan.is_finish = true;

                int xp1 = 1200;
                int xp2 = 1200;
                int rb1 = 300;
                int rb2 = 300;

                if (this.map_pvp_clan.score_clan1 > this.map_pvp_clan.score_clan2) {
                    xp1 = 1800;
                    rb1 = 500;
                    xp2 = 1000;
                    rb2 = 250;
                } else if (this.map_pvp_clan.score_clan2 > this.map_pvp_clan.score_clan1) {
                    xp2 = 1800;
                    rb2 = 500;
                    xp1 = 1000;
                    rb1 = 250;
                }

                if (this.map_pvp_clan.clan1 != null) {
                    this.map_pvp_clan.clan1.update_xp(xp1);
                    this.map_pvp_clan.clan1.update_ruby(rb1);
                    for (int i1 = 0; i1 < this.map_pvp_clan.clan1.members.size(); i1++) {
                        Player p0 = Map.get_player_by_name_allmap(
                                this.map_pvp_clan.clan1.members.get(i1).name);
                        if (p0 != null) {
                            try {
                                Clan.set_data(p0, false);
                                Clan.send_money(p0, false);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                    try {
                        String resultTxt = (xp1 == 1800) ? "THẮNG" : (xp1 == 1000 ? "THUA" : "HÒA");
                        this.map_pvp_clan.clan1.chat_on_board(
                                this.map_pvp_clan.clan1.members.get(0).id,
                                this.map_pvp_clan.clan1.members.get(0).name,
                                ("Phó bản PVP Băng [" + resultTxt + "] với " + this.map_pvp_clan.clan2.name
                                        + ": nhận được " + xp1 + " xp băng và " + rb1 + " ruby băng"),
                                -3);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                    this.map_pvp_clan.clan1.map_create = null;
                }

                if (this.map_pvp_clan.clan2 != null) {
                    this.map_pvp_clan.clan2.update_xp(xp2);
                    this.map_pvp_clan.clan2.update_ruby(rb2);
                    for (int i1 = 0; i1 < this.map_pvp_clan.clan2.members.size(); i1++) {
                        Player p0 = Map.get_player_by_name_allmap(
                                this.map_pvp_clan.clan2.members.get(i1).name);
                        if (p0 != null) {
                            try {
                                Clan.set_data(p0, false);
                                Clan.send_money(p0, false);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                    try {
                        String resultTxt = (xp2 == 1800) ? "THẮNG" : (xp2 == 1000 ? "THUA" : "HÒA");
                        this.map_pvp_clan.clan2.chat_on_board(
                                this.map_pvp_clan.clan2.members.get(0).id,
                                this.map_pvp_clan.clan2.members.get(0).name,
                                ("Phó bản PVP Băng [" + resultTxt + "] với " + this.map_pvp_clan.clan1.name
                                        + ": nhận được " + xp2 + " xp băng và " + rb2 + " ruby băng"),
                                -3);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                    this.map_pvp_clan.clan2.map_create = null;
                }

                Vgo vgo = new Vgo();
                vgo.map_go = Map.get_map_by_id(33);
                vgo.xnew = 710;
                vgo.ynew = 320;
                List<Player> playerList = new ArrayList<>();
                for (int i = 0; i < players.size(); i++) {
                    playerList.add(players.get(i));
                }
                playerList.forEach(l -> {
                    try {
                        l.type_pk = -1;
                        l.goto_map(vgo);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                });
                this.running = false;
            }
        }
    }

    private void update_map_pvp() throws IOException {
        try {
            if (this.template.id == 1000) { // map pvp wait
                List<Player> list_pvp_wait = new ArrayList<>();
                for (int i = 0; i < players.size(); i++) {
                    Player p0 = players.get(i);
                    if (p0.pvp_target != null && p0.pvp_target.equals(p0)) {
                        list_pvp_wait.add(p0);
                    }
                }
                while (list_pvp_wait.size() > 1) {
                    Player p_select = list_pvp_wait.get(0);
                    Player p_select_2 = null;
                    list_pvp_wait.remove(0);
                    if (p_select.pvp_target != null) {
                        for (int i = 0; i < list_pvp_wait.size(); i++) {
                            if (list_pvp_wait.get(i).pvp_target != null
                                    && !list_pvp_wait.get(i).pvp_target.equals(p_select)
                                    && list_pvp_wait.get(i).type_pk_wait == p_select.type_pk_wait) {
                                p_select_2 = list_pvp_wait.get(i);
                                list_pvp_wait.remove(i);
                                break;
                            }
                        }
                    }
                    if (p_select_2 != null) {
                        p_select.pvp_target = p_select_2;
                        p_select_2.pvp_target = p_select;
                        //
                        if (!p_select.equals(p_select_2)) {
                            Pvp.find_out_other(p_select, p_select_2);
                            Pvp.find_out_other(p_select_2, p_select);
                        }
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            System.err.println("err map wait pvp");
        }
        if (this.map_pvp != null) { // map pvp
            this.map_pvp.time_pvp--;
            // System.out.println(this.map_pvp.time_pvp +" "+ this.map_pvp.status_pvp);
            if (this.map_pvp.type_map == 2) { // Wanted PvP
                if (this.map_pvp.status_pvp == 0 && this.map_pvp.time_pvp <= 0) {
                    for (int i = 0; i < players.size(); i++) {
                        Pvp.pvp_notice(players.get(i), 1);
                        Service.use_potion(players.get(i), 0, players.get(i).body.get_hp_max(true));
                        Service.use_potion(players.get(i), 1, players.get(i).body.get_mp_max(true));
                        change_flag(players.get(i), 0);
                    }
                    this.map_pvp.time_pvp = 3;
                    this.map_pvp.status_pvp = 2;
                } else if (this.map_pvp.status_pvp == 2 && this.map_pvp.time_pvp <= 0) {
                    for (int i = 0; i < players.size(); i++) {
                        Pvp.pvp_notice(players.get(i), 2);
                        Pvp.show_info(players.get(i), 180, (i == 0 ? this.map_pvp.num_win_p1 : this.map_pvp.num_win_p2),
                                (i == 0 ? this.map_pvp.num_win_p2 : this.map_pvp.num_win_p1), 3);
                        change_flag(players.get(i), 0);
                    }
                    this.map_pvp.time_pvp = 180;
                    this.map_pvp.status_pvp = 3;
                }
            } else { // Regular PvP
                if (this.map_pvp.status_pvp == 0 && this.map_pvp.time_pvp <= 0) {
                    for (int i = 0; i < players.size(); i++) {
                        Pvp.pvp_notice(players.get(i), 0);
                    }
                    this.map_pvp.time_pvp = 5;
                    this.map_pvp.status_pvp = 1;
                } else if (this.map_pvp.status_pvp == 1 && this.map_pvp.time_pvp <= 0) {
                    for (int i = 0; i < players.size(); i++) {
                        Pvp.pvp_notice(players.get(i), 1);
                        Service.use_potion(players.get(i), 0, players.get(i).body.get_hp_max(true));
                        Service.use_potion(players.get(i), 1, players.get(i).body.get_mp_max(true));
                    }
                    this.map_pvp.time_pvp = 4;
                    this.map_pvp.status_pvp = 2;
                } else if (this.map_pvp.status_pvp == 2 && this.map_pvp.time_pvp <= 0) {
                    for (int i = 0; i < players.size(); i++) {
                        Pvp.pvp_notice(players.get(i), 2);
                        //
                        Pvp.show_info(players.get(i), 180, 0, 0, 3);
                        change_flag(players.get(i), (i == 0 ? 14 : 15));
                    }
                    this.map_pvp.time_pvp = 180;
                    this.map_pvp.status_pvp = 3;
                    //
                }
            }
            if (this.map_pvp.status_pvp == 3 && players.size() == 2) {
                for (int i = 0; i < players.size(); i++) {
                    if (players.get(i).isdie) {
                        this.map_pvp.status_pvp = 91;
                        break;
                    }
                }
                runBotAI();
            } else if (this.map_pvp.status_pvp == 91 && players.size() == 2) {
                this.map_pvp.status_pvp = 90;
            } else if (this.map_pvp.status_pvp == 90 && players.size() == 2) {
                for (int i = 0; i < players.size(); i++) {
                    if (this.map_pvp.num_win_p1 == 3 || this.map_pvp.num_win_p2 == 3) {
                        change_flag(players.get(i), -1);
                    }
                    Player pl = players.get(i);
                    pl.isdie = false;
                    Service.use_potion(pl, 0, pl.body.get_hp_max(true));
                    Service.use_potion(pl, 1, pl.body.get_mp_max(true));

                    Message m2 = new Message(-71);
                    m2.writer().writeByte(1);
                    m2.writer().writeShort(pl.index_map);
                    m2.writer().writeByte(0);
                    m2.writer().writeInt(60 * 30);
                    send_msg_all_p(m2, pl, true);
                    m2.cleanup();

                    if (this.map_pvp.type_map == 2 && this.map_pvp.num_win_p1 < 3 && this.map_pvp.num_win_p2 < 3) {
                        change_flag(pl, 0);
                    }
                }
                if (this.map_pvp.type_map == 2 && this.map_pvp.num_win_p1 < 3 && this.map_pvp.num_win_p2 < 3) {
                    this.map_pvp.time_pvp = 1;
                    this.map_pvp.status_pvp = 0;
                } else {
                    this.map_pvp.status_pvp = 3;
                }
            } else if (this.map_pvp.status_pvp == 90) {
                if (this.map_pvp.type_map == 2 && this.map_pvp.num_win_p1 < 3 && this.map_pvp.num_win_p2 < 3) {
                    this.map_pvp.time_pvp = 1;
                    this.map_pvp.status_pvp = 0;
                } else {
                    this.map_pvp.status_pvp = 3;
                }
            }
            if (this.map_pvp.status_pvp == 3
                    && (this.map_pvp.num_win_p1 == 3 || this.map_pvp.num_win_p2 == 3)) {
                this.map_pvp.status_pvp = 4;
                this.map_pvp.time_pvp = 4;
                //
                try {
                    if (this.map_pvp.type_map == 0) { // la map pvp
                        if (this.map_pvp.num_win_p1 == 3) {
                            Pvp.pvp_notice(players.get(0), 3);
                            Pvp.pvp_notice(players.get(1), 4);
                            players.get(0).pvp_win++;
                            if (players.get(0).daily_achievements[0] == 0) {
                                players.get(0).daily_achievements[0] = 1;
                                core.Service.send_box_ThongBao_OK(players.get(0),
                                        "Hoàn thành Thành tích hằng ngày: PVP");
                            }
                            players.get(1).pvp_lose++;
                            //
                            int chenhLech = players.get(1).get_pvpPoint() - players.get(0).get_pvpPoint();
                            if (chenhLech > 15) {
                                chenhLech = 15;
                            } else if (chenhLech < -15) {
                                chenhLech = -15;
                            }
                            chenhLech += 30;
                            int diemwin = chenhLech;
                            players.get(0).update_pvpPoint(diemwin);
                            players.get(1).update_pvpPoint(-chenhLech);
                        } else {
                            Pvp.pvp_notice(players.get(1), 3);
                            Pvp.pvp_notice(players.get(0), 4);
                            players.get(1).pvp_win++;
                            if (players.get(1).daily_achievements[0] == 0) {
                                players.get(1).daily_achievements[0] = 1;
                                core.Service.send_box_ThongBao_OK(players.get(1),
                                        "Hoàn thành Thành tích hằng ngày: PVP");
                            }
                            players.get(0).pvp_lose++;
                            //
                            int chenhLech = players.get(0).get_pvpPoint() - players.get(1).get_pvpPoint();
                            if (chenhLech > 15) {
                                chenhLech = 15;
                            } else if (chenhLech < -15) {
                                chenhLech = -15;
                            }
                            chenhLech += 30;
                            int diemwin = chenhLech;
                            players.get(1).update_pvpPoint(diemwin);
                            players.get(0).update_pvpPoint(-chenhLech);
                        }
                    } else if (this.map_pvp.type_map == 2) { // la map truy na
                        if (this.map_pvp.num_win_p1 == 3) {
                            Pvp.pvp_notice(players.get(0), 3);
                            Pvp.pvp_notice(players.get(1), 4);
                            //
                            long beri_win = (10_000L + (long) players.get(1).get_wanted_point()) / 100L;
                            long beri_lose = (5_000L + (long) players.get(1).get_wanted_point()) / 100L;
                            players.get(0).update_wanted_point((int) beri_win);
                            players.get(1).update_wanted_point((int) -beri_lose);
                            //
                            Wanted_Chest.receiv_ruong(players.get(0));

                            Service.send_box_ThongBao_OK(players.get(0),
                                    "Trận đấu kết thúc! Bạn đã chiến thắng đối thủ và giành được " + beri_win
                                            + " điểm truy nã cùng 1 Rương Truy nã.");
                            if (!players.get(1).isBot) {
                                Service.send_box_ThongBao_OK(players.get(1),
                                        "Trận đấu kết thúc! Bạn đã thất bại trước đối thủ và bị trừ " + beri_lose
                                                + " điểm truy nã.");
                            }
                        } else {
                            Pvp.pvp_notice(players.get(1), 3);
                            Pvp.pvp_notice(players.get(0), 4);
                            //
                            long beri_win = (10_000L + (long) players.get(0).get_wanted_point()) / 100L;
                            long beri_lose = (5_000L + (long) players.get(0).get_wanted_point()) / 100L;
                            players.get(1).update_wanted_point((int) beri_win);
                            players.get(0).update_wanted_point((int) -beri_lose);
                            //
                            Wanted_Chest.receiv_ruong(players.get(1));

                            Service.send_box_ThongBao_OK(players.get(0),
                                    "Trận đấu kết thúc! Bạn đã thất bại trước đối thủ và bị trừ " + beri_lose
                                            + " điểm truy nã.");
                            if (!players.get(1).isBot) {
                                Service.send_box_ThongBao_OK(players.get(1),
                                        "Trận đấu kết thúc! Bạn đã chiến thắng đối thủ và giành được " + beri_win
                                                + " điểm truy nã cùng 1 Rương Truy nã.");
                            }
                        }
                    } else if (this.map_pvp.type_map == 3) { // thách đấu siêu hạng cá cược ruby
                        int rubyBet = this.map_pvp.ruby_bet;
                        int rubyWin = (int) (rubyBet * 2 * 0.9); // người thắng nhận 90% tổng
                        Player winner, loser;
                        if (this.map_pvp.num_win_p1 == 3) {
                            winner = players.get(0);
                            loser = players.get(1);
                        } else {
                            winner = players.get(1);
                            loser = players.get(0);
                        }
                        Pvp.pvp_notice(winner, 3);
                        Pvp.pvp_notice(loser, 4);
                        winner.update_ngoc(rubyWin);
                        winner.update_money();
                        Service.send_box_ThongBao_OK(winner,
                                "Trận đấu kết thúc! Bạn chiến thắng và nhận được "
                                        + rubyWin + " ruby (90% tổng cược)!");
                        if (!loser.isBot) {
                            Service.send_box_ThongBao_OK(loser,
                                    "Trận đấu kết thúc! Bạn thua và mất " + rubyBet + " ruby.");
                        }
                    }
                } catch (IndexOutOfBoundsException e) {
                    this.map_pvp.status_pvp = 3;
                }
                for (int i = 0; i < players.size(); i++) {
                    change_flag(players.get(i), -1);
                }
            }
            if (this.map_pvp.status_pvp == 3 && players.size() < 2) {
                this.map_pvp.status_pvp = 4;
                this.map_pvp.time_pvp = 4;
                for (int i = 0; i < players.size(); i++) {
                    Pvp.pvp_notice(players.get(i), 3);
                    //
                    Pvp.show_info(players.get(i), 4, 3, 0, 3);
                    change_flag(players.get(i), -1);
                    //
                    if (this.map_pvp.type_map == 0) { // la map pvp
                        players.get(i).update_pvpPoint(20);
                    }
                }
            } else if (this.map_pvp.status_pvp == 3 && this.map_pvp.time_pvp <= 0) {
                //
                try {
                    if (this.map_pvp.type_map == 0) { // la map pvp
                        Player p1 = players.get(0);
                        Player p2 = players.get(1);
                        if (p1 != null && p2 != null && !p1.equals(p2)) {
                            if (this.map_pvp.num_win_p1 > this.map_pvp.num_win_p2) {
                                p1.pvp_win++;
                                if (p1.daily_achievements[0] == 0) {
                                    p1.daily_achievements[0] = 1;
                                    core.Service.send_box_ThongBao_OK(p1, "Hoàn thành Thành tích hằng ngày: PVP");
                                }
                                p1.update_pvpPoint(15);
                                p2.pvp_lose++;
                                p2.update_pvpPoint(-15);
                            } else if (this.map_pvp.num_win_p1 < this.map_pvp.num_win_p2) {
                                p1.pvp_lose++;
                                p1.update_pvpPoint(-15);
                                p2.pvp_win++;
                                if (p2.daily_achievements[0] == 0) {
                                    p2.daily_achievements[0] = 1;
                                    core.Service.send_box_ThongBao_OK(p2, "Hoàn thành Thành tích hằng ngày: PVP");
                                }
                                p2.update_pvpPoint(15);
                            }
                        }
                    } else if (this.map_pvp.type_map == 3 && this.map_pvp.ruby_bet > 0) {
                        // Hết giờ (hòa) → hoàn ruby cho cả hai bên
                        int rubyBet = this.map_pvp.ruby_bet;
                        for (int i = 0; i < players.size(); i++) {
                            players.get(i).update_ngoc(rubyBet);
                            players.get(i).update_money();
                        }
                    }
                } catch (Exception e) {
                }
                //
                this.map_pvp.status_pvp = 4;
                this.map_pvp.time_pvp = 4;
                for (int i = 0; i < players.size(); i++) {
                    if (this.map_pvp.type_map == 0) { // la map pvp
                        Service.send_box_ThongBao_OK(players.get(i),
                                "Hết thời gian, kết quả hòa, bạn sẽ được đưa về map chờ");
                    } else if (this.map_pvp.type_map == 3) { // siêu hạng cá cược → hoàn ruby
                        Service.send_box_ThongBao_OK(players.get(i),
                                "Hết thời gian! Kết quả hòa, ruby cược đã được hoàn lại.");
                    } else {
                        Service.send_box_ThongBao_OK(players.get(i),
                                "Đối thủ xứng tầm không thể phân biệt thắng thua");
                    }
                }
            } else if (this.map_pvp.status_pvp == 4 && this.map_pvp.time_pvp <= 0) {
                Vgo vgo = new Vgo();
                if (this.map_pvp.type_map == 0) { // la map pvp (siêu hạng queue)
                    vgo.map_go = Map.get_map_by_id(1000);
                } else if (this.map_pvp.type_map == 2) { // la map truy na
                    vgo.map_go = Map.get_map_by_id(119);
                } else {
                    // type_map==1 (giao hữu) và type_map==3 (siêu hạng cá cược) → về làng
                    vgo.map_go = Map.get_map_by_id(1);
                }
                vgo.xnew = (short) (vgo.map_go[0].template.maxW / 2);
                vgo.ynew = (short) (vgo.map_go[0].template.maxH / 2);
                List<Player> playerList = new ArrayList<>();
                for (int i = 0; i < players.size(); i++) {
                    playerList.add(players.get(i));
                }
                playerList.forEach(l -> {
                    if (l.isBot) {
                        return;
                    }
                    try {
                        l.targetFight = null;
                        change_flag(l, -1);
                        l.goto_map(vgo);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                });
                this.map_pvp.status_pvp = 99;
            } else if (this.map_pvp.status_pvp == 99) {
                running = false;
                this.map_pvp = null;
                Map.remove_map_plus(this);
            }
        }
    }

    private void runBotAI() {
        if (System.currentTimeMillis() - lastBotActionTime < 1000) {
            return;
        }
        lastBotActionTime = System.currentTimeMillis();

        Player bot = null;
        Player human = null;
        for (int i = 0; i < players.size(); i++) {
            Player p = players.get(i);
            if (p.isBot) {
                bot = p;
            } else {
                human = p;
            }
        }
        if (bot != null && human != null && !bot.isdie && !human.isdie) {
            try {
                int dx = human.x - bot.x;
                int dy = human.y - bot.y;
                int dist = (int) Math.sqrt(dx * dx + dy * dy);

                // 1. Move bot closer to the human if they are too far
                if (dist > 80) {
                    int step = 45;
                    if (dx > 0)
                        bot.x += step;
                    else
                        bot.x -= step;

                    if (Math.abs(dy) > 10) {
                        if (dy > 0)
                            bot.y += 10;
                        else
                            bot.y -= 10;
                    }

                    Message mmove = new Message(1);
                    mmove.writer().writeByte(0);
                    mmove.writer().writeShort(bot.index_map);
                    mmove.writer().writeShort(bot.x);
                    mmove.writer().writeShort(bot.y);
                    send_msg_all_p(mmove, bot, false);
                    mmove.cleanup();

                    // Update distance
                    dx = human.x - bot.x;
                    dy = human.y - bot.y;
                    dist = (int) Math.sqrt(dx * dx + dy * dy);
                }

                // 2. Attack the human player if within range
                if (dist <= 150) {
                    Skill_info targetSkill = null;
                    for (Skill_info sk : bot.skill_point) {
                        if (sk.temp.typeSkill == 1 || sk.temp.typeSkill == 4) {
                            if (bot.time_sk[sk.temp.ID] <= System.currentTimeMillis()) {
                                targetSkill = sk;
                                if (sk.temp.ID != 0) {
                                    break;
                                }
                            }
                        }
                    }
                    if (targetSkill != null) {
                        bot.time_sk[targetSkill.temp.ID] = System.currentTimeMillis() + targetSkill.temp.timeDelay
                                - ((targetSkill.temp.timeDelay * bot.body.get_agility(true)) / 1_000);
                        if (bot.mp < targetSkill.temp.manaLost) {
                            bot.mp = bot.body.get_mp_max(true);
                        }
                        bot.mp -= targetSkill.temp.manaLost;

                        long dame = bot.body.get_dame(true);
                        dame = (dame * bot.body.get_dame_devil_percent()) / 100;
                        EffTemplate eff = bot.get_eff(5);
                        if (eff != null) {
                            dame *= 2;
                        }
                        eff = bot.get_eff(18);
                        if (eff != null) {
                            dame = (dame * eff.param) / 100;
                        }
                        if (dame > 2 && bot.get_eff(21) != null) {
                            dame /= 2;
                        }

                        Player[] p_target = new Player[] { human };
                        Fire_Player(p_target, bot, targetSkill.temp.ID, dame);
                    }
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void update_map_dungeon() throws IOException {
        if (Map.is_map_dungeon(this.template.id)) { // map dungeon
            if (this.map_dungeon != null) {
                if (this.map_dungeon instanceof activities.TowerChallenge) {
                    ((activities.TowerChallenge) this.map_dungeon).update(this);
                    return;
                }
                if (this.map_dungeon instanceof activities.NamieTreasureDefense) {
                    ((activities.NamieTreasureDefense) this.map_dungeon).update(this);
                    return;
                }
                if (this.map_dungeon instanceof activities.HangDong) {
                    ((activities.HangDong) this.map_dungeon).update(this);
                    return;
                }
            }
            // BossHunt maps (201-207) dùng map_bossHunt, không dùng p.dungeon
            // Chúng được xử lý bởi update_map_bossHunt() riêng — thoát sớm để tránh NPE
            if (this.map_bossHunt != null) {
                return;
            }
            Player p_select = null;
            boolean ok_out_map = false;
            if (players.size() > 0) {
                Player p0 = players.get(0);
                if (p0.dungeon == null)
                    return; // guard an toàn
                int num_mob = 0;
                for (int i = 0; i < p0.dungeon.mobs.size(); i++) {
                    Mob mob = p0.dungeon.mobs.get(i);
                    if (mob.map.equals(this) && !mob.isdie) {
                        num_mob++;
                    }
                }
                if (num_mob == 0 && p0.dungeon.time > System.currentTimeMillis()) {
                    if (this.template.id == 175) {
                        if (this.map_dungeon != null && !this.map_dungeon.checkG.contains(175)) {
                            System.out.println(
                                    "[Dungeon Debug] Map 175 cleared! Setting 10s countdown to return to village for "
                                            + p0.name);
                            p0.dungeon.time = System.currentTimeMillis() + 10_000L;
                            Service.send_time_cool_down(p0, p0.dungeon.time, "Về Làng", 2);
                        }
                    }
                    if (!this.map_dungeon.checkG.contains(this.template.id)) {
                        this.map_dungeon.checkG.add(this.template.id);
                        //
                        byte mode_dungeon = p0.dungeon.mode;
                        List<GiftBox> list_gift = new ArrayList<>();
                        int beri_receiv = Util.random(4_000, 8_000);
                        ItemTemplate4 it_temp4;

                        // Add Beri
                        GiftBox gb_beri = new GiftBox();
                        it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb_beri.id = it_temp4.id;
                            gb_beri.type = 4;
                            gb_beri.name = it_temp4.name;
                            gb_beri.icon = it_temp4.icon;
                            gb_beri.num = beri_receiv;
                            gb_beri.color = 0;
                            list_gift.add(gb_beri);
                        }

                        // Add Sea Stone (Đá Hải Thạch)
                        int stoneRoll = Util.random(100);
                        int stoneId = 221; // Mặc định cấp 1
                        if (stoneRoll < 1) { // 1% cho cấp 6 (Đá Hải Thạch cấp 6 - ID 226)
                            stoneId = 226;
                        } else if (stoneRoll < 2) { // 1% cho cấp 5 (Đá Hải Thạch cấp 5 - ID 225)
                            stoneId = 225;
                        } else if (stoneRoll < 8) { // 6% cho cấp 4
                            stoneId = 224;
                        } else if (stoneRoll < 25) { // 17% cho cấp 3
                            stoneId = 223;
                        } else if (stoneRoll < 55) { // 30% cho cấp 2
                            stoneId = 222;
                        } else { // 45% cho cấp 1
                            stoneId = 221;
                        }

                        ItemTemplate4 it_haithach = ItemTemplate4.get_it_by_id(stoneId);
                        if (it_haithach != null) {
                            GiftBox gb_haithach = new GiftBox();
                            gb_haithach.id = (short) stoneId;
                            gb_haithach.type = 4;
                            gb_haithach.name = it_haithach.name;
                            gb_haithach.icon = it_haithach.icon;
                            gb_haithach.num = 1;
                            gb_haithach.color = 0;
                            list_gift.add(gb_haithach);
                        }

                        // Add EXP level and EXP skill directly to player
                        long exp_lv_gain = 50000L * (mode_dungeon + 1);
                        long exp_skill_gain = 5000L * (mode_dungeon + 1);
                        p0.update_exp(exp_lv_gain, true);
                        for (int sk_id = 0; sk_id < 4; sk_id++) {
                            p0.update_skill_exp(sk_id, exp_skill_gain);
                        }

                        Service.send_gift(p0, 1, "Ải đơn cấp độ " + (mode_dungeon + 3),
                                "Phần thưởng", list_gift, true);
                    }
                }
                boolean isSingleDungeon = this.map_dungeon != null && this.map_dungeon.getClass() == Dungeon.class;
                if (isSingleDungeon && num_mob > 0 && p0.dungeon.time < System.currentTimeMillis()) {
                    if (this.map_dungeon != null && !this.map_dungeon.checkG.contains(-1)) {
                        System.out.println("[Dungeon Debug] Player " + p0.name + " failed single dungeon on map "
                                + this.template.id + ". Setting 10s countdown to return to village.");
                        this.map_dungeon.checkG.add(-1);
                        p0.dungeon.time = System.currentTimeMillis() + 10_000L;
                        Service.send_time_cool_down(p0, p0.dungeon.time, "Thất Bại", 2);
                        Service.send_box_ThongBao_OK(p0, "Bạn đã thất bại! Tự động rời phó bản sau 10 giây.");
                    }
                }
                if (p0.dungeon.time < System.currentTimeMillis()) {
                    System.out.println("[Dungeon Debug] Dungeon time expired. Teleporting player " + p0.name
                            + " back to Syrup Village.");
                    ok_out_map = true;
                    p_select = p0;
                }
            }
            if (ok_out_map && p_select != null && p_select.conn != null) {
                if (p_select.isdie) {
                    p_select.isdie = false;
                    p_select.hp = p_select.body.get_hp_max(true);
                    p_select.mp = p_select.body.get_mp_max(true);
                }
                Vgo vgo = new Vgo();
                vgo.map_go = Map.get_map_by_id(25);
                vgo.xnew = 390;
                vgo.ynew = 240;
                p_select.goto_map(vgo);
                p_select.dungeon = null;
            }
            if (this.map_dungeon == null
                    || players.size() == 0 && this.map_dungeon.time < System.currentTimeMillis()) {
                this.running = false;
            }
        }
    }

    private void update_item_map() throws IOException {
        for (int i = 0; i < this.list_it_map.length; i++) {
            ItemMap it = this.list_it_map[i];
            // if (it != null) {
            // System.out.println((it.time_exist - System.currentTimeMillis()) / 1000);
            // }
            if (it != null && it.time_exist < System.currentTimeMillis()) {
                this.remove_obj(it.index, it.category);
                this.list_it_map[i] = null;
            }
            if (it != null && (it.time_exist - 10_000L) < System.currentTimeMillis()) {
                it.id_master = -1;
            }
        }
    }

    public void remove_obj(int index, int category) throws IOException {
        Message m = new Message(13);
        m.writer().writeShort(index);
        m.writer().writeByte(category);
        send_msg_all_p(m, null, true);
        m.cleanup();
    }

    private synchronized void update_player() throws IOException {
        List<Player> list_remove = new ArrayList<>();
        for (int i = 0; i < players.size(); i++) {
            Player p0 = players.get(i);
            if (p0.conn != null) {
                int hp_buff = 0;
                int mp_buff = 0;
                if (p0.mp < 0) {
                    p0.mp = 0;
                }
                EffTemplate eff = p0.get_eff(0);
                if (!p0.isdie && eff != null) { // buff hp
                    hp_buff = eff.param;
                    if (p0.clan != null) {
                        int buff_percent = 100;
                        if (p0.clan.check_buff(2)) {
                            buff_percent += 25;
                        }
                        if (p0.clan.check_buff(4)) {
                            buff_percent += 25;
                        }
                        hp_buff = (hp_buff * buff_percent) / 100;
                    }
                }
                eff = p0.get_eff(1);
                if (!p0.isdie && eff != null) { // buff mp
                    mp_buff = eff.param;
                    if (p0.clan != null) {
                        int buff_percent = 100;
                        if (p0.clan.check_buff(2)) {
                            buff_percent += 25;
                        }
                        if (p0.clan.check_buff(4)) {
                            buff_percent += 25;
                        }
                        mp_buff = (mp_buff * buff_percent) / 100;
                    }
                }
                if (p0.time_buff_hp_mp < System.currentTimeMillis()) {
                    p0.time_buff_hp_mp = System.currentTimeMillis() + 5_000L;
                    hp_buff += p0.body.get_hp_auto_buff(true);
                    mp_buff += p0.body.get_mp_auto_buff(true);
                }
                int hp_max = p0.body.get_hp_max(true);
                if (!p0.isdie && p0.hp < hp_max && hp_buff > 0 && p0.get_eff(202) == null) {
                    Service.use_potion(p0, 0, hp_buff);
                }
                if (!p0.isdie && p0.mp < p0.body.get_mp_max(true) && mp_buff > 0) {
                    Service.use_potion(p0, 1, mp_buff);
                }
                if (p0.get_eff(207) != null) {
                    int hp_decrease = hp_max / 100;
                    if (p0.hp - hp_decrease > 0) {
                        Service.use_potion(p0, 0, -hp_decrease);
                    }
                }
                // up hp pet ship
                if (p0.ship_pet != null && p0.ship_pet.time_buff_hp < System.currentTimeMillis()
                        && (Map.isMapLang(this.template.id))) {
                    p0.ship_pet.time_buff_hp = System.currentTimeMillis() + 2000L;
                    if (p0.ship_pet.hp < p0.ship_pet.hp_max) {
                        p0.ship_pet.hp += 40;
                        if (p0.ship_pet.hp > p0.ship_pet.hp_max) {
                            p0.ship_pet.hp = p0.ship_pet.hp_max;
                        }
                        Message m = new Message(-83);
                        m.writer().writeShort(p0.ship_pet.index_map);
                        m.writer().writeByte(0);
                        m.writer().writeInt(p0.ship_pet.hp_max); // maxhp
                        m.writer().writeInt(p0.ship_pet.hp); // hp remain
                        m.writer().writeInt(50); // dame
                        m.writer().writeInt(p0.ship_pet.hp_max); // maxhp
                        m.writer().writeInt(0); // hp remain
                        m.writer().writeInt(0); // dame
                        this.send_msg_all_p(m, null, true);
                        m.cleanup();
                    }
                }
                //
                boolean ch = false;
                // update ticket
                if (p0.get_ticket() < p0.get_ticket_max()
                        && p0.cd_ticket_next < System.currentTimeMillis()) {
                    p0.cd_ticket_next = System.currentTimeMillis() + (60_000L * 10); // 10p=
                    p0.update_ticket(1);
                    ch = true;
                }
                if (p0.get_pvp_ticket() < p0.get_pvp_ticket_max()
                        && p0.cd_pvp_next < System.currentTimeMillis()) {
                    p0.cd_pvp_next = System.currentTimeMillis() + (60_000L * 60 * 2); // 2h
                    p0.update_pvp_ticket(1);
                    ch = true;
                }
                if (p0.get_key_boss() < p0.get_key_boss_max()
                        && p0.cd_keyboss_next < System.currentTimeMillis()) {
                    p0.cd_keyboss_next = System.currentTimeMillis() + (60_000L * 60 * 1); // 1h
                    p0.update_key_boss(1);
                    ch = true;
                }
                if (ch) {
                    p0.update_money();
                    Service.CountDown_Ticket(p0);
                }
                // Thợ săn hải tặc (Bounty Hunter)
                if (p0.thosan_bounty > 0) {
                    long now = System.currentTimeMillis();
                    if (now - p0.time_bounty_posted < 600_000L) { // Trong 10 phút đầu
                        if (now - p0.last_bounty_announce_time > 60_000L) { // Mỗi 1 phút
                            p0.last_bounty_announce_time = now;
                            long minutes_left = 10 - ((now - p0.time_bounty_posted) / 60_000L);
                            core.Manager.gI().chatKTG(0, "Tội phạm " + p0.name + " đang bị truy nã với giá "
                                    + p0.thosan_bounty + " Beri! Sau " + minutes_left
                                    + " phút nữa lệnh truy nã sẽ có hiệu lực!", 5);
                        }
                    }
                }
                // update combo
                if (p0.is_combo != null && p0.time_combo < System.currentTimeMillis()) {
                    p0.is_combo = null;
                    Service.start_combo(p0, 0);
                }
                //
                if (this.template.id == 81 && this.map_little_garden != null) {
                    if (p0.isdie && p0.time_hs_little_garden <= System.currentTimeMillis()) {
                        p0.isdie = false;
                        Service.use_potion(p0, 0, p0.body.get_hp_max(true));
                        Service.use_potion(p0, 1, p0.body.get_mp_max(true));
                    }
                }
                // skil buff nami
                if (!Map.isMapLang(this.template.id)) {
                    if (p0.get_eff(14) != null) {
                        try {
                            Player p_target = null;
                            while (players.size() > 1
                                    && (p_target == null || p_target.equals(p0))) {
                                p_target = players.get(Util.random(players.size()));
                            }
                            if (p_target != null && !p_target.equals(p0)) {
                                if (!((p0.typePirate == 0 && p_target.typePirate == 2)
                                        || (p0.typePirate == 2 && p_target.typePirate == 0)
                                        || (p0.typePirate == 1 && p_target.typePirate == 2)
                                        || (p0.typePirate == 2 && p_target.typePirate == 1)
                                        || (p0.type_pk == 14 && p_target.type_pk == 15)
                                        || (p0.type_pk == 15 && p_target.type_pk == 14)
                                        || (p0.typePirate == 2 && p_target.typePirate == 2)
                                        || (p0.type_pk == 0) || (p_target.type_pk == 1)
                                        || (p0.type_pk == 3 && p_target.type_pk == 3)
                                        || (p_target.type_pk == 0)
                                        || (p0.type_pk == 3 && p_target.type_pk >= 4
                                                && p_target.type_pk <= 8)
                                        || (p_target.type_pk == 3 && p0.type_pk >= 4
                                                && p0.type_pk <= 8)
                                        || (p0.type_pk >= 4 && p0.type_pk <= 8
                                                && p_target.type_pk >= 4 && p_target.type_pk <= 8
                                                && p0.type_pk != p_target.type_pk))) {
                                } else {
                                    Message m = new Message(-15);
                                    m.writer().writeByte(3);
                                    m.writer().writeShort(p_target.index_map);
                                    m.writer().writeByte(0);
                                    m.writer().writeShort(0);
                                    send_msg_all_p(m, p0, true);
                                    m.cleanup();
                                    //
                                    int dame_to_target = p0.body.get_dame(true);
                                    dame_to_target = (dame_to_target * (100 - Util.random(10))) / 100;
                                    if (p_target.hp - dame_to_target > 0) {
                                        p_target.hp -= dame_to_target;
                                    } else {
                                        p_target.hp = 1;
                                    }
                                    //
                                    m = new Message(28);
                                    m.writer().writeShort(p_target.index_map);
                                    m.writer().writeByte(0);
                                    m.writer().writeInt(p_target.hp);
                                    m.writer().writeInt(p_target.body.get_hp_max(true));
                                    m.writer().writeShort(-1);
                                    m.writer().writeShort(-1);
                                    send_msg_all_p(m, p0, true);
                                    m.cleanup();
                                }
                            } else {
                                List<Mob> list_random = new ArrayList<>();
                                for (int i11 = 0; i11 < list_mob.length; i11++) {
                                    Mob mob = Mob.ENTRYS.get(Integer.valueOf(list_mob[i11]));
                                    if (mob != null) {
                                        if (!mob.isdie && Math.abs(p0.x - mob.x) < 200
                                                && Math.abs(p0.y - mob.y) < 200) {
                                            list_random.add(mob);
                                        }
                                    }
                                }
                                if (list_random.size() > 0) {
                                    Mob mob_select = list_random.get(Util.random(list_random.size()));
                                    Message m = new Message(-15);
                                    m.writer().writeByte(3);
                                    m.writer().writeShort(mob_select.index);
                                    m.writer().writeByte(1);
                                    m.writer().writeShort(0);
                                    send_msg_all_p(m, p0, true);
                                    m.cleanup();
                                    //
                                    int dame_to_target = p0.body.get_dame(true);
                                    dame_to_target = (dame_to_target * (100 - Util.random(10))) / 100;
                                    if (mob_select.hp - dame_to_target > 0) {
                                        mob_select.hp -= dame_to_target;
                                    } else {
                                        mob_select.hp = 1;
                                    }
                                    //
                                    m = new Message(28);
                                    m.writer().writeShort(mob_select.index);
                                    m.writer().writeByte(1);
                                    m.writer().writeInt(mob_select.hp);
                                    m.writer().writeInt(mob_select.hp_max);
                                    m.writer().writeShort(-1);
                                    m.writer().writeShort(-1);
                                    send_msg_all_p(m, p0, true);
                                    m.cleanup();
                                }
                            }
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }
            } else {
                list_remove.add(p0);
            }
        }
        players.removeAll(list_remove);
        for (int i = 0; i < list_remove.size(); i++) {
            Player p0 = list_remove.get(i);
            Message m = new Message(3);
            m.writer().writeShort(p0.index_map);
            m.writer().writeByte(0);
            this.send_msg_all_p(m, null, true);
            m.cleanup();
        }
        list_remove.clear();
    }

    private void update_mob() {
        if (this.can_PK) {
            for (int i = 0; i < list_mob.length; i++) {
                Mob mob = Mob.ENTRYS.get(Integer.valueOf(list_mob[i]));
                if (mob != null) {
                    if (!mob.isdie) {
                        if (mob.id_target != -1) {
                            try {
                                mob_fire(mob, mob.id_target);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    } else {
                        if (mob.hp == 0 && mob.time_refresh < System.currentTimeMillis()) {
                            mob.isdie = false;
                            mob.hp = mob.hp_max;
                            mob.id_target = -1;
                            //
                            try {
                                Message m_local = new Message(1);
                                m_local.writer().writeByte(1);
                                m_local.writer().writeShort(mob.index);
                                m_local.writer().writeShort(mob.x);
                                m_local.writer().writeShort(mob.y);
                                this.send_msg_all_p(m_local, null, true);
                                m_local.cleanup();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss boss = Boss.ENTRYS.get(i);
            Mob mob = boss.mob;
            if (mob.map.equals(this) && !mob.isdie) {
                if (mob.id_target != -1) {
                    try {
                        mob_fire(mob, mob.id_target);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
        if (Map.is_map_boss(this.template.id)) {
            List<Mob> get_list_Mob = MapBossInfo.get_list_mob(this);
            if (get_list_Mob != null) {
                for (int i = 0; i < get_list_Mob.size(); i++) {
                    Mob mob = get_list_Mob.get(i);
                    if (!mob.isdie) {
                        if (mob.id_target != -1) {
                            try {
                                mob_fire(mob, mob.id_target);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                }
            }
        }
        // Dungeon mob AI: chỉ chạy với dungeon thực sự (có p.dungeon), không chạy với
        // BossHunt
        // map (201-207) vì BossHunt có khối xử lý riêng bên dưới
        if (Map.is_map_dungeon(this.template.id) && this.map_bossHunt == null) {
            List<Mob> list_remove = new ArrayList<>();
            List<Mob> get_list_Mob = new ArrayList<>();
            if (players.size() > 0 && players.get(0).dungeon != null) {
                for (int i = 0; i < players.get(0).dungeon.mobs.size(); i++) {
                    if (players.get(0).dungeon.mobs.get(i).map.equals(this)) {
                        get_list_Mob.add(players.get(0).dungeon.mobs.get(i));
                    }
                }
            }
            for (int i = 0; i < get_list_Mob.size(); i++) {
                Mob mob = get_list_Mob.get(i);
                if (mob.isdie && ((mob.time_refresh - (Mob.TIME_RESPAWN * 1000) / 2) < System
                        .currentTimeMillis())) {
                    list_remove.add(mob);
                }
            }
            for (int i = 0; i < list_remove.size(); i++) {
                try {
                    this.remove_obj(list_remove.get(i).index, 1);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            get_list_Mob.removeAll(list_remove);
            if (players.size() > 0) {
                players.get(0).dungeon.mobs.removeAll(list_remove);
            }
            for (int i = 0; i < get_list_Mob.size(); i++) {
                Mob mob = get_list_Mob.get(i);
                if (!mob.isdie) {
                    if (mob.id_target != -1) {
                        try {
                            mob_fire(mob, mob.id_target);
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                }
            }
        }
        // BossHunt mobs
        if (this.map_bossHunt != null && this.map_bossHunt.active) {
            for (Mob mob : this.map_bossHunt.mobs) {
                if (mob.map.equals(this) && !mob.isdie && mob.id_target != -1) {
                    try {
                        mob_fire(mob, mob.id_target);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
    }

    private synchronized void mob_fire(Mob mob, int id_target) throws IOException {
        if (!mob.isdie && mob.time_skill < System.currentTimeMillis()) {
            mob.time_skill = System.currentTimeMillis() + 1800L;
            Player p0 = this.get_player_by_id_inmap(id_target);
            if (p0 != null) {
                if (!mob.isdie && !p0.wait_change_map && !p0.isdie
                        && p0.time_can_mob_atk < System.currentTimeMillis()) {
                    int dame;
                    if (((mob.map.map_bossHunt != null)
                            || (mob.map.map_dungeon != null && mob.map.map_dungeon instanceof activities.HangDong))
                            && mob.final_dame > 0) {
                        dame = mob.final_dame;
                    } else {
                        dame = Util.random(mob.level * 2, mob.level * 5);
                        if (mob.level > 30 && mob.level <= 50) {
                            dame = (dame * 15) / 10;
                        } else if (mob.level > 50 && mob.level <= 70) {
                            dame = (dame * 18) / 10;
                        } else if (mob.level > 70 && mob.level <= 90) {
                            dame = (dame * 21) / 10;
                        } else if (mob.level > 90 && mob.level <= 600) {
                            dame = (dame * 25) / 10;
                        }
                        if (mob.level - p0.level >= 10) {
                            dame = dame * 5;
                        }
                        if (dame <= 0) {
                            dame = Util.random(10, 20);
                        }
                    }

                    // update hp target
                    if (p0.hp == p0.body.get_hp_max(true) && dame >= p0.hp) {
                        p0.hp = 1;
                    } else {
                        p0.hp -= dame;
                    }
                    long def = p0.body.get_def(true);
                    def = (def * (1000L + (long) p0.body.get_def_percent(true))) / 10_000L;
                    dame -= def;
                    // miss enemy
                    int get_miss = p0.body.get_miss(true) - p0.body.get_miss_reduce();
                    boolean miss = ((p0.get_eff(205) != null || get_miss > Util.random(1000)));
                    if (miss) { // miss
                        dame = 0;
                    }
                    // System.out.println(p0.hp);
                    if (p0.hp <= 0) {
                        p0.hp = 0;
                        p0.isdie = true;
                        mob.id_target = -1;
                        mob_non_focus(mob);
                    }
                    //
                    long dame_mine = 0;
                    if (p0.body.get_dame_react(true) > Util.random(1000)) {
                        dame_mine = dame;
                    }
                    if (dame_mine > 0) {
                        mob.hp -= dame_mine;
                        if (mob.hp <= 0) {
                            mob.hp = 1;
                        }
                        this.update_hp_mp_eff(null, mob, 1, (int) -dame_mine);
                    }
                    //
                    Message m = new Message(100);
                    m.writer().writeShort(mob.index);
                    m.writer().writeByte(1);
                    m.writer().writeInt(mob.hp); // hp
                    m.writer().writeInt(mob.hp); // mp
                    short skillId = (mob.boss_info != null && mob.boss_info.skill != null
                            && mob.boss_info.skill.length > 0)
                                    ? mob.boss_info.skill[Util.random(mob.boss_info.skill.length)]
                                    : mob.mob_template.skill[Util.random(mob.mob_template.skill.length)];
                    m.writer().writeShort(skillId);
                    m.writer().writeByte(1); // size target
                    m.writer().writeShort(id_target);
                    m.writer().writeByte(0);
                    m.writer().writeInt(dame);
                    m.writer().writeInt(0); // dame plus
                    m.writer().writeInt(p0.hp);
                    m.writer().writeByte(0);
                    send_msg_all_p(m, p0, true);
                    m.cleanup();
                    //
                    if (p0.hp <= 0) {
                        die_player(p0, p0);
                    }
                    if (mob.id_target != -1
                            && !(Math.abs(mob.x - p0.x) < 200 && Math.abs(mob.y - p0.y) < 200)) {
                        mob.id_target = -1;
                        mob_non_focus(mob);
                    }
                }
            }
        }
    }

    private void mob_non_focus(Mob mob) throws IOException {
        Message m2 = new Message(5);
        m2.writer().writeShort(mob.index);
        send_msg_all_p(m2, null, true);
        m2.cleanup();
    }

    public void die_player(Player p0, Player p) throws IOException {
        boolean isSingleDungeon = this.map_dungeon != null && this.map_dungeon.getClass() == Dungeon.class;
        if (isSingleDungeon) {
            if (this.map_dungeon != null && !this.map_dungeon.checkG.contains(-1)) {
                this.map_dungeon.checkG.add(-1);
                p0.dungeon.time = System.currentTimeMillis() + 10_000L;
                Service.send_time_cool_down(p0, p0.dungeon.time, "Thất Bại", 2);
                Service.send_box_ThongBao_OK(p0, "Bạn đã thất bại! Tự động rời phó bản sau 10 giây.");
            }
            p0.isdie = true;
            p0.update_die();

            Message m = new Message(7);
            m.writer().writeShort(p.index_map);
            m.writer().writeByte(0);
            m.writer().writeShort(p0.index_map);
            m.writer().writeByte(0);
            m.writer().writeShort(p.pointPk); // point pk
            send_msg_all_p(m, p0, true);
            m.cleanup();
            return;
        }
        boolean isBossHunt = this.map_bossHunt != null;
        if (isBossHunt) {
            activities.BossHunt hunt = this.map_bossHunt;
            p0.isdie = true;
            p0.hp = 0;

            boolean allDead = true;
            for (Player member : hunt.members) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                        && pOnline.bossHunt == hunt && pOnline.map.equals(this)) {
                    if (pOnline.name.equals(p0.name)) {
                        continue;
                    }
                    if (!pOnline.isdie && pOnline.hp > 0) {
                        allDead = false;
                        break;
                    }
                }
            }
            if (allDead) {
                if (!hunt.isFailed) {
                    hunt.isFailed = true;
                    hunt.failTime = System.currentTimeMillis() + 10_000L;
                    for (Player member : hunt.members) {
                        Player pOnline = Map.get_player_by_name_allmap(member.name);
                        if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                                && pOnline.bossHunt == hunt && pOnline.map.equals(this)) {
                            Service.send_time_cool_down(pOnline, hunt.failTime, "Thất Bại", 2);
                            Service.send_box_ThongBao_OK(pOnline,
                                    "Tổ đội đã tử trận! Tự động rời phó bản sau 10 giây.");
                        }
                    }
                }

                p0.isdie = true;
                p0.update_die();

                Message m = new Message(7);
                m.writer().writeShort(p.index_map);
                m.writer().writeByte(0);
                m.writer().writeShort(p0.index_map);
                m.writer().writeByte(0);
                m.writer().writeShort(p.pointPk); // point pk
                send_msg_all_p(m, p0, true);
                m.cleanup();
                return;
            }
        }
        boolean isTowerChallenge = this.map_dungeon != null && this.map_dungeon instanceof activities.TowerChallenge;
        if (isTowerChallenge) {
            activities.TowerChallenge tc = (activities.TowerChallenge) this.map_dungeon;
            p0.isdie = true;
            p0.hp = 0;

            boolean allDead = true;
            for (Player member : tc.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                        && pOnline.dungeon == tc && pOnline.map.equals(this)) {
                    if (pOnline.name.equals(p0.name)) {
                        continue;
                    }
                    if (!pOnline.isdie && pOnline.hp > 0) {
                        allDead = false;
                        break;
                    }
                }
            }
            if (allDead) {
                if (tc.checkG != null && !tc.checkG.contains(-2)) {
                    tc.checkG.add(-2);
                    tc.stageEndTime = System.currentTimeMillis() + 10_000L;
                    for (Player member : tc.partyMembers) {
                        Player pOnline = Map.get_player_by_name_allmap(member.name);
                        if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                                && pOnline.dungeon == tc && pOnline.map.equals(this)) {
                            Service.send_time_cool_down(pOnline, tc.stageEndTime, "Thất Bại", 2);
                            Service.send_box_ThongBao_OK(pOnline,
                                    "Tổ đội đã tử trận! Tự động rời phó bản sau 10 giây.");
                        }
                    }
                }

                p0.isdie = true;
                p0.update_die();

                Message m = new Message(7);
                m.writer().writeShort(p.index_map);
                m.writer().writeByte(0);
                m.writer().writeShort(p0.index_map);
                m.writer().writeByte(0);
                m.writer().writeShort(p.pointPk); // point pk
                send_msg_all_p(m, p0, true);
                m.cleanup();
                return;
            }
        }
        boolean isNamieDefense = this.map_dungeon != null
                && this.map_dungeon instanceof activities.NamieTreasureDefense;
        if (isNamieDefense) {
            activities.NamieTreasureDefense nd = (activities.NamieTreasureDefense) this.map_dungeon;
            p0.isdie = true;
            p0.hp = 0;

            boolean allDead = true;
            for (Player member : nd.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                        && pOnline.dungeon == nd && pOnline.map.equals(this)) {
                    if (pOnline.name.equals(p0.name)) {
                        continue;
                    }
                    if (!pOnline.isdie && pOnline.hp > 0) {
                        allDead = false;
                        break;
                    }
                }
            }
            if (allDead) {
                if (nd.checkG != null && !nd.checkG.contains(-2)) {
                    nd.checkG.add(-2);
                    nd.dungeonEndTime = System.currentTimeMillis() + 10_000L;
                    for (Player member : nd.partyMembers) {
                        Player pOnline = Map.get_player_by_name_allmap(member.name);
                        if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                                && pOnline.dungeon == nd && pOnline.map.equals(this)) {
                            Service.send_time_cool_down(pOnline, nd.dungeonEndTime, "Thất Bại", 2);
                            Service.send_box_ThongBao_OK(pOnline,
                                    "Tổ đội đã tử trận! Tự động rời phó bản sau 10 giây.");
                        }
                    }
                }

                p0.isdie = true;
                p0.update_die();

                Message m = new Message(7);
                m.writer().writeShort(p.index_map);
                m.writer().writeByte(0);
                m.writer().writeShort(p0.index_map);
                m.writer().writeByte(0);
                m.writer().writeShort(p.pointPk); // point pk
                send_msg_all_p(m, p0, true);
                m.cleanup();
                return;
            }
        }
        p0.isdie = true;
        p0.update_die();
        //
        //
        Message m = new Message(7);
        m.writer().writeShort(p.index_map);
        m.writer().writeByte(0);
        m.writer().writeShort(p0.index_map);
        m.writer().writeByte(0);
        m.writer().writeShort(p.pointPk); // point pk
        send_msg_all_p(m, p0, true);
        m.cleanup();
        //
        if (p0.is_combo != null) {
            p0.is_combo = null;
            Service.start_combo(p0, 0);
        }
        //
        if (this.map_pvp != null && !p.equals(p0)) {
            if (players.indexOf(p0) == 0) {
                this.map_pvp.num_win_p2++;
            } else {
                this.map_pvp.num_win_p1++;
            }
            for (int i = 0; i < players.size(); i++) {
                Player pl = players.get(i);
                int ownScore = (i == 0 ? this.map_pvp.num_win_p1 : this.map_pvp.num_win_p2);
                int oppScore = (i == 0 ? this.map_pvp.num_win_p2 : this.map_pvp.num_win_p1);
                Pvp.show_info(pl, this.map_pvp.time_pvp, oppScore, ownScore, 3);
            }
        }
    }

    public void enter_map(Player p) {
        synchronized (this) {
            players.add(p);
        }
    }

    public void leave_map(Player p, int type) {
        if (this.template.id == 119) {
            Wanted.remove_player_wait(p);
        }
        synchronized (this) {
            players.remove(p);
        }
        p.is_combo = null;
        p.time_combo = 0;
        p.id_meet_in_map.clear();
        p.id_meet_in_map.add("" + p.index_map);
        //
        if (p.map_boss_info != null && Map.is_map_boss(this.template.id)) {
            MapBossInfo.remove(p.map_boss_info);
            p.map_boss_info = null;
            if (this.players.size() < 1) {
                this.stop_map();
            }
        }
        // BossHunt: khi player ngắt kết nối, tạm chuyển vào offlineMembers.
        // Họ sẽ KHÔNG bị lôi vào tầng mới cho đến khi xác nhận vào lại.
        if (this.map_bossHunt != null && p.bossHunt != null) {
            System.out.println("[BossHunt] Player " + p.name
                    + " left BossHunt map (floor " + (p.bossHunt.currentFloor + 1) + ")."
                    + " Moving to offlineMembers.");
            this.map_bossHunt.markOffline(p.name);
        }
        // Reset mob target if mob was targeting the player who left/disconnected
        if (this.map_bossHunt != null && this.map_bossHunt.mobs != null) {
            for (Mob mob : this.map_bossHunt.mobs) {
                if (mob.id_target == p.index_map) {
                    mob.id_target = -1;
                }
            }
        }
        if (this.map_dungeon != null && this.map_dungeon.mobs != null) {
            for (Mob mob : this.map_dungeon.mobs) {
                if (mob.id_target == p.index_map) {
                    mob.id_target = -1;
                }
            }
        }
        try {
            Message m = new Message(3);
            m.writer().writeShort(p.index_map);
            // 2: next map, 1: tele, 0: exit game
            m.writer().writeByte(type);
            for (int i = 0; i < players.size(); i++) {
                Player p0 = players.get(i);
                if (p0.conn != null) {
                    p0.conn.addmsg(m);
                }
                p0.id_meet_in_map.remove("" + p.index_map);
            }
            m.cleanup();
            //
            if (p.ship_pet != null && p.ship_pet.map == null) {
                m = new Message(3);
                m.writer().writeShort(p.ship_pet.index_map);
                m.writer().writeByte(type);
                for (int i = 0; i < players.size(); i++) {
                    Player p0 = players.get(i);
                    if (p0.conn != null) {
                        p0.conn.addmsg(m);
                    }
                }
                m.cleanup();
            }
            //
            if (p.trade_target != null) {
                Trade.end_trade_by_disconnect(p.trade_target, p, 0, "");
                p.fee_trade = 0;
                p.money_trade = 0;
                p.is_lock_trade = false;
                p.is_accept_trade = false;
                p.list_item_trade3 = null;
                p.list_item_trade47 = null;
                p.trade_target = null;
            }
            MyPet pet_select = p.get_pet();
            if (pet_select != null) {
                Message m22 = new Message(-80);
                m22.writer().writeByte(1);
                m22.writer().writeShort(-1);
                m22.writer().writeShort(p.index_map);
                send_msg_all_p(m22, null, true);
                m22.cleanup();
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (NullPointerException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
            System.out.println("err leave map " + this.template.id);
        }
    }

    public static Map[] get_map_by_id(int id) {
        for (int i = 0; i < Map.ENTRYS.size(); i++) {
            if (Map.ENTRYS.get(i)[0].template.id == id) {
                return Map.ENTRYS.get(i);
            }
        }
        return null;
    }

    public static Player get_player_by_name_allmap(String name) {
        try {
            for (int i = 0; i < Map.ENTRYS.size(); i++) {
                for (int j = 0; j < Map.ENTRYS.get(i).length; j++) {
                    Map m = Map.ENTRYS.get(i)[j];
                    for (int k = 0; k < m.players.size(); k++) {
                        if (m.players.get(k).name.equals(name)) {
                            return m.players.get(k);
                        }
                    }
                }
            }
            for (int i = 0; i < MAP_PLUS.size(); i++) {
                Map map = MAP_PLUS.get(i);
                for (int j = 0; j < map.players.size(); j++) {
                    Player p0 = map.players.get(j);
                    if (p0.name.equals(name)) {
                        return p0;
                    }
                }
            }
        } catch (Exception e) {
        }
        return null;
    }

    public void send_move(Player p, Message m) throws IOException {
        if (!p.isdie) {
            p.x = m.reader().readShort();
            p.y = m.reader().readShort();
            if (p.map.map_pvp != null) {
                if (p.x < 5) {
                    p.x = 5;
                } else if (p.x > 1070) {
                    p.x = 1070;
                }
                if (p.y > 330) {
                    p.y = 330;
                } else if (p.y < 170) {
                    p.y = 170;
                }
            }
            //
            if (!Map.is_map_dont_show_other_info(this.template.id)) {
                Message mmove = new Message(1);
                mmove.writer().writeByte(0);
                mmove.writer().writeShort(p.index_map);
                mmove.writer().writeShort(p.x);
                mmove.writer().writeShort(p.y);
                send_msg_all_p(mmove, p, false);
                mmove.cleanup();
                //
                if (p.ship_pet != null && p.ship_pet.map != null && p.ship_pet.map.equals(p.map)) {
                    if (p.ship_pet.time < System.currentTimeMillis()) {
                        p.ship_pet.time = System.currentTimeMillis() + 1800L;
                        p.ship_pet.x = p.x;
                        p.ship_pet.y = p.y;
                    }
                    //
                    mmove = new Message(1);
                    mmove.writer().writeByte(0);
                    mmove.writer().writeShort(p.ship_pet.index_map);
                    mmove.writer().writeShort(p.ship_pet.x);
                    mmove.writer().writeShort(p.ship_pet.y);
                    p.conn.addmsg(mmove);
                    mmove.cleanup();
                }
            }
            // mob
            for (int i = 0; i < list_mob.length; i++) {
                Mob mob = Mob.ENTRYS.get(Integer.valueOf(list_mob[i]));
                if (mob != null && !mob.isdie && Math.abs(mob.x - p.x) < 70
                        && Math.abs(mob.y - p.y) < 70 && mob.id_target == -1) {
                    mob.id_target = p.index_map;
                }
            }
            if (Map.is_map_dungeon(this.template.id) && p.dungeon != null) {
                for (int i = 0; i < p.dungeon.mobs.size(); i++) {
                    Mob mob = p.dungeon.mobs.get(i);
                    if (mob != null && !mob.isdie && mob.map.equals(this)
                            && Math.abs(mob.x - p.x) < 300 && Math.abs(mob.y - p.y) < 300
                            && mob.id_target == -1) {
                        mob.id_target = p.index_map;
                    }
                }
            }
            // BossHunt mob targeting khi player di chuyển gần boss
            if (this.map_bossHunt != null && this.map_bossHunt.active && p.bossHunt != null) {
                for (Mob mob : this.map_bossHunt.mobs) {
                    if (mob != null && !mob.isdie && mob.map.equals(this)
                            && Math.abs(mob.x - p.x) < 70 && Math.abs(mob.y - p.y) < 70
                            && mob.id_target == -1) {
                        mob.id_target = p.index_map;
                    }
                }
            }
            // boss
            for (int i = 0; i < Boss.ENTRYS.size(); i++) {
                Boss temp = Boss.ENTRYS.get(i);
                if (!temp.mob.isdie && Math.abs(temp.mob.x - p.x) < 70
                        && Math.abs(temp.mob.y - p.y) < 70 && temp.mob.id_target == -1) {
                    temp.mob.id_target = p.index_map;
                }
            }
            if (p.ischangemap) {
                if (Map.is_map_dungeon(this.template.id) && p.dungeon != null) {
                    int num_mob = 0;
                    for (int i = 0; i < p.dungeon.mobs.size(); i++) {
                        Mob mob_dungeon = p.dungeon.mobs.get(i);
                        if (mob_dungeon.map.equals(this)) {
                            num_mob++;
                            Message mmove = new Message(1);
                            mmove.writer().writeByte(1);
                            mmove.writer().writeShort(mob_dungeon.index);
                            mmove.writer().writeShort(mob_dungeon.x);
                            mmove.writer().writeShort(mob_dungeon.y);
                            send_msg_all_p(mmove, p, true);
                            mmove.cleanup();
                        }
                    }
                    if (num_mob > 0) {
                        return;
                    }
                }
                // BossHunt: broadcast vị trí boss cho mọi người khi player mới vào map
                if (this.map_bossHunt != null && this.map_bossHunt.active && p.bossHunt != null) {
                    int num_boss = 0;
                    for (Mob mob_boss : this.map_bossHunt.mobs) {
                        if (mob_boss != null && !mob_boss.isdie && mob_boss.map.equals(this)) {
                            num_boss++;
                            Message mmove = new Message(1);
                            mmove.writer().writeByte(1);
                            mmove.writer().writeShort(mob_boss.index);
                            mmove.writer().writeShort(mob_boss.x);
                            mmove.writer().writeShort(mob_boss.y);
                            send_msg_all_p(mmove, p, true);
                            mmove.cleanup();
                        }
                    }
                    if (num_boss > 0) {
                        return;
                    }
                }
                for (Vgo vgo : this.template.vgos) {
                    if (Math.abs(vgo.xold - p.x) < 60 && Math.abs(vgo.yold - p.y) < 60) {
                        p.time_change_map = System.currentTimeMillis() + 5000L;
                        try {
                            Thread.sleep(250L);
                        } catch (InterruptedException e) {
                        }
                        p.goto_map(vgo);
                        break;
                    }
                }
            } else if (p.time_change_map < System.currentTimeMillis()) {
                p.ischangemap = true;
            }
        }
    }

    private static boolean is_map_dont_show_other_info(int id) {
        return id == 64;
    }

    public void update_num_player_in_map(Player p) throws IOException {
        Message m = new Message(-70);
        m.writer().writeByte((byte) p.map.players.size());
        m.writer().writeByte(15);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public synchronized void use_skill(Player p, Message m2) throws IOException {
        short idSkill = m2.reader().readShort();
        byte CatBeFire = m2.reader().readByte();
        byte size_target = m2.reader().readByte();
        // System.out.println(idSkill);
        // System.out.println(CatBeFire);
        // System.out.println(size_target);
        if (!p.isdie && size_target > 0) {
            Skill_info sk_temp = p.get_skill_temp(idSkill);
            if (sk_temp == null || (sk_temp.temp.typeSkill != 1 && sk_temp.temp.typeSkill != 4)) {
                return;
            }
            if (p.time_sk[sk_temp.temp.ID] > System.currentTimeMillis()) {
                return;
            }
            p.time_sk[sk_temp.temp.ID] = System.currentTimeMillis() + sk_temp.temp.timeDelay
                    - ((sk_temp.temp.timeDelay * p.body.get_agility(true)) / 1_000);
            if ((p.mp - sk_temp.temp.manaLost) < 0) {
                Service.send_box_ThongBao_OK(p, "MP không đủ!");
                return;
            }

            int hp_ = p.body.get_hp_atk_absorb(true);
            int mp_ = p.body.get_mp_atk_absorb(true);

            Service.use_potion(p, 0, hp_);
            Service.use_potion(p, 1, mp_);

            p.mp -= sk_temp.temp.manaLost;
            long dame = p.body.get_dame(true);
            dame = (dame * p.body.get_dame_devil_percent()) / 100;
            EffTemplate eff = p.get_eff(5); // combo
            if (eff != null) {
                dame *= 2;
            }
            eff = p.get_eff(18); // skill boc pha
            if (eff != null) {
                dame = (dame * eff.param) / 100;
            }
            if (dame > 2 && p.get_eff(21) != null) { // zoombie
                dame /= 2;
            }
            if (sk_temp.temp.ID == 2057 || sk_temp.temp.ID == 2058) { // buff trai bong toi
                dame = (dame * 12) / 10;
            }
            if ((p.map.template.specMap == 4 && sk_temp.temp.ID != 3)
                    || (p.map.template.specMap != 4 && sk_temp.temp.ID == 3)) {// skill bien chi
                // dung tren bien
                dame = 0;
            }
            // kich an danh la choang
            if (p.get_eff(407) == null && p.body.get_kich_an(7) > 0) {
                p.danhLaChoang++;
            }
            // kich an thanh loc
            if (p.get_eff(408) == null && p.body.get_kich_an(8) > 0) {
                p.thanhLoc++;
            }
            //
            if (sk_temp.temp.nTarget > 0 && sk_temp.temp.nTarget < size_target) {
                size_target = sk_temp.temp.nTarget;
            }
            Player[] p_target = new Player[size_target];
            Mob[] mob_target = new Mob[size_target];
            Ship_pet spet = null;
            for (int i = 0; i < size_target; i++) {
                int id_target = m2.reader().readShort();
                switch (CatBeFire) {
                    case 0: {
                        p_target[i] = this.get_player_by_id_inmap(id_target);
                        if (i == 0 && p_target[i] == null) {
                            spet = Ship_pet.get_pet(id_target);
                        }
                        break;
                    }
                    case 1: {
                        mob_target[i] = Mob.ENTRYS.get(id_target);
                        if (mob_target[i] == null && Map.is_map_boss(this.template.id)
                                && p.map_boss_info != null) {
                            mob_target[i] = MapBossInfo.get_mob(p, id_target);
                        }
                        if (mob_target[i] == null && Map.is_map_dungeon(this.template.id)
                                && p.dungeon != null && this.map_dungeon != null) {
                            mob_target[i] = p.dungeon.get_mob(p, id_target);
                        }
                        if (mob_target[i] == null && this.template.id == 81
                                && this.map_little_garden != null) {
                            mob_target[i] = this.get_mobs(id_target, 0);
                        }
                        // BossHunt mob lookup (index = negative value like -1000 - floor)
                        if (mob_target[i] == null && this.map_bossHunt != null
                                && p.bossHunt != null) {
                            mob_target[i] = p.bossHunt.get_mob(p, id_target);
                            if (mob_target[i] != null) {
                                System.out.println("[BossHunt] Player " + p.name
                                        + " attacking BossHunt mob index=" + id_target
                                        + " floor=" + (p.bossHunt.currentFloor + 1));
                            }
                        }
                        if (mob_target[i] == null && Map.is_map_dungeon(this.template.id)
                                && p.dungeon != null && this.map_dungeon != null) {
                            remove_obj(id_target, 1);
                        }
                        break;
                    }
                }
            }
            long[] exp_up = null;
            switch (CatBeFire) {
                case 0: {
                    eff = p.get_eff(12);
                    if (eff != null && p_target[0] != null) { // skill buff zoro
                        Service.send_eff_sword_splash(p_target[0].index_map, p);
                    }
                    if (p_target.length > 0 && p_target[0] == null && spet != null) {
                        atk_ship_pet(spet, p, idSkill);
                    } else {
                        Fire_Player(p_target, p, idSkill, dame);
                    }
                    break;
                }
                case 1: {
                    if (mob_target[0] != null) {
                        eff = p.get_eff(12);
                        if (eff != null) { // skill buff zoro
                            Service.send_eff_sword_splash(mob_target[0].index, p);
                        }
                    }
                    exp_up = Fire_Monster(mob_target, p, idSkill, dame);
                    break;
                }
            }
            if (exp_up != null) { // update exp
                if (exp_up[0] > 0) {
                    p.update_exp(exp_up[0], true);
                }
                if (exp_up[1] > 0) {
                    p.update_skill_exp(idSkill, exp_up[1]);
                }
            }
        }
    }

    private void atk_ship_pet(Ship_pet spet, Player p, short idSkill) throws IOException {
        if (Map.isMapLang(this.template.id) || spet.main_ship.index_map == p.index_map
                || !(p.typePirate == 0 || p.typePirate == 2)
                || (p.typePirate == 0 && spet.main_ship.typePirate == 0)) {
            return;
        }
        Skill_info sk_temp = p.get_skill_temp(idSkill);
        if (sk_temp != null) {
            Message m = new Message(100);
            m.writer().writeShort(p.index_map);
            m.writer().writeByte(0);
            m.writer().writeInt(p.hp);
            m.writer().writeInt(p.mp);
            m.writer().writeShort(sk_temp.get_eff_skill());
            m.writer().writeByte(1);
            //
            m.writer().writeShort(spet.index_map);
            m.writer().writeByte(0);
            int dame_ship_pet = 50;
            if (spet.main_ship.typePirate == 2) {
                dame_ship_pet = 100;
            }
            m.writer().writeInt(dame_ship_pet);
            //
            spet.hp -= dame_ship_pet;
            if (spet.hp <= 0) {
                spet.hp = 0;
                spet.main_ship.ship_pet = null;
                Ship_pet.remv(spet);
                try {
                    remove_obj(spet.index_map, 0);
                } catch (Exception e) {
                }
                //
                p.ship_pet = new Ship_pet();
                short index_map_new = -2;
                p.ship_pet.index_map = index_map_new;
                p.id_ship_packet = spet.main_ship.id_ship_packet;
                p.ship_pet.main_ship = p;
                p.ship_pet.map = p.map;
                p.ship_pet.name = "Hàng " + p.name;
                p.ship_pet.x = spet.x;
                p.ship_pet.y = spet.y;
                p.ship_pet.hp_max = 2000;
                p.ship_pet.hp = p.ship_pet.hp_max;
                p.ship_pet.time_start = spet.time_start;
                p.ship_pet.mainBaoVe = "";
                Ship_pet.add(p.ship_pet);
                //
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
            }
            //
            m.writer().writeInt(0); // dame plus
            m.writer().writeInt(spet.hp);
            //
            m.writer().writeByte(0);
            send_msg_all_p(m, p, true);
            m.cleanup();
        }
    }

    private void Fire_Player(Player[] list_target, Player p, int idSkill, long dame)
            throws IOException {
        Skill_info sk_temp = p.get_skill_temp(idSkill);
        if (!this.can_PK || sk_temp == null
                || (this.map_pvp != null && (this.map_pvp.num_win_p1 == 3
                        || this.map_pvp.num_win_p2 == 3 || this.map_pvp.status_pvp != 3))) {
            return;
        }
        int dame_plus_percent = 0;
        int dame_magic_plus_percent = p.body.get_dame_ap();
        int crit_skill = p.body.get_crit(true);
        int multi_dame_skill = p.body.get_multi_dame_when_crit(true);
        boolean crit = false;
        //
        List<Dame_Msg> list = new ArrayList<>();
        long dame_mine_all = 0;
        long damebefore = dame;
        long dame2;
        EffTemplate eff;
        //
        for (int i = 0; i < list_target.length; i++) {
            Player p_target = list_target[i];
            if (p_target != null && p_target.index_map != p.index_map && !p_target.isdie && !p.isdie
                    && (p_target.time_can_mob_atk - 1000) < System.currentTimeMillis()) {
                if (!((p.typePirate == 0 && p_target.typePirate == 2)
                        || (p.typePirate == 2 && p_target.typePirate == 0)
                        || (p.typePirate == 1 && p_target.typePirate == 2)
                        || (p.typePirate == 2 && p_target.typePirate == 1)
                        || (p.type_pk == 14 && p_target.type_pk == 15)
                        || (p.type_pk == 15 && p_target.type_pk == 14)
                        || (p.typePirate == 2 && p_target.typePirate == 2) || (p.type_pk == 0)
                        || (p_target.type_pk == 1) || (p.type_pk == 3 && p_target.type_pk == 3)
                        || (p_target.type_pk == 0)
                        || (p.type_pk == 3 && p_target.type_pk >= 4 && p_target.type_pk <= 8)
                        || (p_target.type_pk == 3 && p.type_pk >= 4 && p.type_pk <= 8)
                        || (p.type_pk >= 4 && p.type_pk <= 8 && p_target.type_pk >= 4
                                && p_target.type_pk <= 8 && p.type_pk != p_target.type_pk))) {
                    continue;
                }
                ItemFashionP2 checkF = p.check_fashion(120);
                if (checkF != null && checkF.is_use && i == 0 && p_target.get_eff(21) == null) { // tt
                    // zombie
                    if (5 > Util.random(120)) {
                        p_target.add_new_eff(21, Util.random(28), 5000);
                        p_target.update_info_to_all();
                        //
                        for (int j = 0; j < players.size(); j++) {
                            Service.charWearing(p_target, players.get(j), false);
                        }
                    }
                }
                dame2 = damebefore;
                dame2 = (dame2 * (1000L + dame_plus_percent)) / 1000L;
                dame2 = (dame2 * (long) sk_temp.get_dame(p))
                        / ((long) p.skill_point.get(0).get_dame(p));
                long def = p_target.body.get_def(true);
                def = (def * (1000L + (long) p_target.body.get_def_percent(true))) / 1_000L;
                dame2 -= def;
                crit = (crit_skill) > Util.random(1000);
                //
                long dame_mine = 0;
                Dame_Msg dame_inf = new Dame_Msg();
                dame_inf.data = new ArrayList<>();
                dame_inf.targetP = p_target;
                if (dame2 > 0 && idSkill != 0) {
                    dame_inf.dameM = (p.get_skill_temp(idSkill).get_dame(p) * (dame_magic_plus_percent))
                            / 1000;
                }
                if (dame_inf.dameM < 0) {
                    dame_inf.dameM = 0;
                }
                if (dame2 > 0 && idSkill == 2038 || idSkill == 2041) {
                    if (p.get_eff(6) != null) {
                        dame2 = (dame2 * 115) / 100;
                    }
                    // fashion bao dom + chim ung
                    for (int i12 = 0; i12 < p.fashion.size(); i12++) {
                        if ((p.fashion.get(i12).id == 33 || p.fashion.get(i12).id == 34)
                                && p.fashion.get(i12).is_use) {
                            dame2 = (dame2 * 115) / 100;
                            break;
                        }
                    }
                }
                int react_dame_ = p_target.body.get_dame_react(true) - p.body.get_dame_react_reduce();
                int MienThuong = p_target.body.get_dame_skip(true) - p.body.get_dame_skip_reduce();
                if (MienThuong < 0) {
                    MienThuong = 0;
                }
                if (MienThuong > 900) {
                    MienThuong = 900;
                }
                int get_miss = p_target.body.get_miss(true) - p.body.get_miss_reduce();
                boolean miss = ((p.get_eff(205) != null || get_miss > Util.random(1000)));
                if (miss) { // miss
                    dame2 = 0;
                }
                if (dame2 > 0 && react_dame_ > Util.random(1000)) {
                    dame_mine = (dame2 * 8L) / 10L;
                }
                int kich_an;
                if (dame2 > 0) {
                    // eff kich an
                    kich_an = p_target.body.get_kich_an(0);
                    if (kich_an > 0) { // bat tu
                        eff = p_target.get_eff(300);
                        if (eff != null) {
                            dame2 = 0;
                            dame_inf.dameM = 0;
                        } else {
                            eff = p_target.get_eff(400);
                            if (eff == null) {
                                int per = kich_an == 3 ? 10 : (kich_an == 2 ? 8 : 5);
                                per = (per * (1000)) / 1000;
                                if (per > Util.random(120)) {
                                    dame2 = 0;
                                    dame_inf.dameM = 0;
                                    int time_eff = 5;
                                    time_eff = (time_eff * (1000)) / 1000;
                                    p_target.add_new_eff(300, 1, (time_eff * 1_000));
                                    Service.send_kich_an(p, p_target, time_eff, 0, 0, 0);
                                    time_eff = 60_000;
                                    time_eff = (time_eff * (1000)) / 1000;
                                    p_target.add_new_eff(400, 1, time_eff);
                                }
                            }
                        }
                    }
                    kich_an = p_target.body.get_kich_an(1);
                    if (kich_an > 0) { // loi cam on
                        eff = p_target.get_eff(401);
                        if (eff == null) {
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 8 : 5);
                            per = (per * (1000)) / 1000;
                            if (per > Util.random(120)) {
                                p_target.hp += dame2 / 5;
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p_target.add_new_eff(401, 1, time_eff);
                                Service.send_kich_an(p, p_target, 1, 1, 0, (int) (dame2 / 5));
                                dame2 = 0;
                                dame_inf.dameM = 0;
                            }
                        }
                    }
                    kich_an = p_target.body.get_kich_an(2);
                    if (kich_an > 0) { // la chan
                        eff = p_target.get_eff(402);
                        if (eff == null) {
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 8 : 5);
                            per = (per * (1000)) / 1000;
                            if (per > Util.random(120)) {
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p_target.add_new_eff(402, 1, time_eff);
                                Service.send_kich_an(p, p_target, 1, 2, 5, 50);
                                //
                                time_eff = 5_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                //
                                eff = p.get_eff(205);
                                if (eff == null) {
                                    p.add_new_eff(205, 1, time_eff);
                                } else {
                                    eff.time = System.currentTimeMillis() + time_eff;
                                }
                                Buff.send_choang(p_target, p, time_eff);
                                dame2 = 0;
                                dame_inf.dameM = 0;
                            }
                        }
                    }
                    kich_an = p_target.body.get_kich_an(3);
                    if (kich_an > 0) { // khoa nang luong
                        eff = p_target.get_eff(403);
                        if (eff == null) {
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 8 : 5);
                            per = (per * (1000)) / 1000;
                            if (per > Util.random(120)) {
                                // p_target.add_new_eff(303, 1, 5_000);
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p_target.add_new_eff(403, 1, time_eff);
                                Service.send_kich_an(p, p_target, 5, 3, 0, p.mp);
                                p.mp = 0;
                                dame2 = 0;
                                dame_inf.dameM = 0;
                            }
                        }
                    }
                    kich_an = p.body.get_kich_an(4);
                    if (kich_an > 0) { // boc pha
                        eff = p.get_eff(404);
                        if (eff == null) {
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 8 : 5);
                            per = (per * (1000)) / 1000;
                            if (per > Util.random(120)) {
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p.add_new_eff(404, 1, time_eff);
                                Service.send_kich_an(p_target, p, 1, 4, 0, (int) (dame2 * 2));
                                dame2 *= 2;
                                dame_inf.dameM *= 2;
                            }
                        }
                    }
                    kich_an = p.body.get_kich_an(5);
                    if (kich_an > 0) { // tap trung cao do
                        eff = p.get_eff(305);
                        if (eff != null) {
                            crit = true;
                        } else {
                            eff = p.get_eff(405);
                            if (eff == null) {
                                int per = kich_an == 3 ? 10 : (kich_an == 2 ? 8 : 5);
                                per = (per * (1000)) / 1000;
                                if (per > Util.random(120)) {
                                    int time_eff = 10;
                                    time_eff = (time_eff * (1000)) / 1000;
                                    p.add_new_eff(305, 1, time_eff * 1_000);
                                    Service.send_kich_an(p_target, p, time_eff, 5, 0, 0);
                                    time_eff = 60_000;
                                    time_eff = (time_eff * (1000)) / 1000;
                                    p.add_new_eff(405, 1, time_eff);
                                    crit = true;
                                }
                            }
                        }
                    }
                    kich_an = p.body.get_kich_an(6);
                    if (kich_an > 0) { // ma ca rong
                        eff = p.get_eff(406);
                        if (eff == null) {
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 8 : 5);
                            per = (per * (1000)) / 1000;
                            if (per > Util.random(120)) {
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p.add_new_eff(406, 1, time_eff);
                                Service.send_kich_an(p_target, p, 1, 6, 0, (int) (dame2 / 5));
                                p.hp += dame2 / 5;
                            }
                        }
                    }
                    kich_an = p.body.get_kich_an(7);
                    if (kich_an > 0) { // danh la choang
                        eff = p.get_eff(407);
                        if (eff == null) {
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 15 : 20);
                            per = (per * (1000)) / 1000;
                            if (per == p.danhLaChoang) {
                                p.danhLaChoang = 0;
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p.add_new_eff(407, 1, time_eff);
                                Service.send_kich_an(p_target, p, 1, 7, 5, 50);
                                // dame_inf.data.add(new Option_Dame_Msg(5, 1, 50));
                                time_eff = 5_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                eff = p_target.get_eff(205);
                                if (eff == null) {
                                    p_target.add_new_eff(205, 1, time_eff);
                                } else {
                                    eff.time = System.currentTimeMillis() + time_eff;
                                }
                                Buff.send_choang(p, p_target, time_eff);
                            }
                        }
                    }
                    kich_an = p.body.get_kich_an(8);
                    if (kich_an > 0) { // thanh loc
                        eff = p.get_eff(408);
                        if (eff == null) {
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 15 : 20);
                            per = (per * (1000)) / 1000;
                            if (per == p.thanhLoc) {
                                p.thanhLoc = 0;
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p.add_new_eff(408, 1, time_eff);
                                Service.send_kich_an(p_target, p, 1, 8, 0, 0);
                            }
                        }
                    }
                    //
                    kich_an = p_target.body.get_kich_an(9);
                    if (kich_an > 0) { // nen dau
                        eff = p_target.get_eff(409);
                        if (eff == null) {
                            p_target.nenDau++;
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 15 : 20);
                            per = (per * (1000)) / 1000;
                            if (per == p_target.nenDau) {
                                p_target.nenDau = 0;
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p_target.add_new_eff(409, 1, time_eff);
                                Service.send_kich_an(p, p_target, 1, 9, 0, 0);
                            }
                        }
                    }
                    kich_an = p_target.body.get_kich_an(10);
                    if (kich_an > 0) { // giai phong nang luong
                        eff = p_target.get_eff(410);
                        if (eff == null) {
                            p_target.giaiPhongNangLuong++;
                            int per = kich_an == 3 ? 10 : (kich_an == 2 ? 15 : 20);
                            per = (per * (1000)) / 1000;
                            if (per == p_target.giaiPhongNangLuong) {
                                p_target.giaiPhongNangLuong = 0;
                                int time_eff = 60_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                p_target.add_new_eff(410, 1, time_eff);
                                Service.send_kich_an(p, p_target, 1, 10, 5, 50);
                                // dame_inf.data.add(new Option_Dame_Msg(5, 1, 50));
                                time_eff = 5_000;
                                time_eff = (time_eff * (1000)) / 1000;
                                eff = p.get_eff(205);
                                if (eff == null) {
                                    p.add_new_eff(205, 1, time_eff);
                                } else {
                                    eff.time = System.currentTimeMillis() + time_eff;
                                }
                                Buff.send_choang(p_target, p, time_eff);
                            }
                        }
                    }
                    //
                    if (dame2 > 0) {
                        dame2 = (dame2 * (1000L + p.body.get_percent_final_dame())) / 1000L;
                        if (dame2 > 1 && crit) {
                            dame2 = (dame2 * (1000L + multi_dame_skill)) / 1000L;
                            int dame_crit_decrease = p_target.body.get_multi_dame_decrease();
                            dame2 = (dame2 * (1000L - dame_crit_decrease)) / 1000L;
                            if (dame2 < 1) {
                                dame2 = 1;
                            }
                        }
                        //
                        int percent_hp_target = p.body.get_dame_percent_hp_target();
                        if (damebefore > 0 && percent_hp_target > 0) {
                            long hp_target = p_target.hp;
                            hp_target = hp_target * (percent_hp_target) / 1000L;
                            hp_target = (hp_target * (1000 - ((MienThuong * 3) / 5))) / 1000; // mien
                            // thuong
                            // cho
                            // %hp
                            dame2 += hp_target;
                        }
                        //
                        dame2 = (dame2 * (1000 - MienThuong)) / 1000; // mien thuong
                    }
                }
                kich_an = p.body.get_kich_an(11);
                if (kich_an > 0 && p.get_eff(202) == null) { // nguoi bat tu
                    int hp_max = p.body.get_hp_max(true);
                    int per = kich_an == 3 ? 20 : (kich_an == 2 ? 15 : 10);
                    per = (per * (1000)) / 1000;
                    int hp_absorb_kichan = (hp_max / 1000) * per;
                    if (hp_absorb_kichan > 0) {
                        Service.send_kich_an(p_target, p, hp_absorb_kichan / 10, 11, 0, 0);
                        p.hp += hp_absorb_kichan;
                        if (p.hp > hp_max) {
                            p.hp = hp_max;
                        }
                    }
                }
                //
                if (p_target.get_eff(7) != null && p_target.type_pk == -1
                        || p_target.get_eff(9) != null || damebefore == 0) {
                    dame2 = 0;
                    dame_inf.dameM = 0;
                    dame_mine = 0;
                }
                dame_inf.dameP = dame2;
                long dame_to_target = dame2 + dame_inf.dameM;
                p_target.hp -= dame_to_target;
                // hut mau trai bong toi
                long HapThuHP = 0;
                long percent_HapThu = p_target.body.get_HapThu_Hp();
                if (dame2 > 1 && percent_HapThu > Util.random(1000) && p.get_eff(202) == null
                        && p_target.hp > 0) {
                    HapThuHP = (dame2 * percent_HapThu) / 1_000L;
                    int hp_max_target = p_target.body.get_hp_max(true);
                    if (HapThuHP > (hp_max_target / 2)) {
                        HapThuHP = (hp_max_target / 2);
                    }
                    Service.use_potion(p_target, 0, (int) HapThuHP);
                }
                // tu choi tu than
                if (p_target.hp <= 0 && p_target.get_eff(10) == null
                        && p_target.body.get_TuChoiTuThan() > 0) {
                    int time_eff = 5;
                    time_eff = (time_eff * (1000)) / 1000;
                    p_target.add_new_eff(9, 1, time_eff * 1_000);
                    time_eff = 150_000;
                    time_eff = (time_eff * (1000)) / 1000;
                    p_target.add_new_eff(10, 1, time_eff);
                    Service.send_eff(p_target, 21, 50);
                    p_target.hp = p_target.body.get_hp_max(true) / 10;
                }
                //
                if (p_target.hp <= 0) {
                    p_target.hp = 0;
                    p_target.isdie = true;
                    if (this.map_pvp_clan != null && !this.map_pvp_clan.is_finish) {
                        if (p_target.type_pk == 4) {
                            this.map_pvp_clan.score_clan2++;
                        } else if (p_target.type_pk == 5) {
                            this.map_pvp_clan.score_clan1++;
                        }
                    }
                    if (this.map_pvp != null) {
                        die_player(p_target, p);
                    } else {
                        die_player(p_target, p_target);
                    }
                    if (!p.equals(p_target) && p.daily_achievements[5] == 0) {
                        p.daily_achievements[5] = 1;
                        try {
                            core.Service.send_box_ThongBao_OK(p, "Hoàn thành Thành tích hằng ngày: Đồ sát 1 người");
                        } catch (Exception e) {
                        }
                    }
                    // Thợ săn hải tặc (Bounty Hunter)
                    if (p_target.thosan_bounty > 0 && !p.equals(p_target)) {
                        if (System.currentTimeMillis() - p_target.time_bounty_posted >= 600_000L) {
                            long thuong = (long) (p_target.thosan_bounty * 0.8);
                            p.update_vang(thuong);
                            p.update_money();
                            core.Manager.gI().chatKTG(0,
                                    "Thông báo: Thợ săn " + p.name + " đã tiêu diệt tội phạm bị truy nã "
                                            + p_target.name + " và nhận được " + core.Util.number_format(thuong)
                                            + " Beri tiền thưởng!",
                                    5);
                            p_target.thosan_bounty = 0;
                            p_target.time_bounty_posted = 0;
                            p_target.last_bounty_announce_time = 0;
                            client.Player.flush(p_target, false); // Save to database immediately
                            core.BXH.updateThoSanBounty(); // Refresh ranking
                        }
                    }
                    if (p.type_pk == 0 && p_target.type_pk != 0) {
                        int delta = p.level / 10 - p_target.level / 10;
                        int plus = (p.pointPk > 0) ? (p.pointPk / 5) : 0;
                        if (delta > 0) {
                            p.update_point_pk(100 + (delta * 100) + plus);
                        } else {
                            p.update_point_pk(100 + plus);
                        }
                        //
                        if (p_target.type_pk == -1 && p_target.typePirate == -1) {
                            // p_target.enemy_list
                            while (p_target.enemy_list.size() > 50) {
                                p_target.enemy_list.remove(0);
                            }
                            FriendTemp enemy_add = null;
                            for (int j = 0; j < p_target.enemy_list.size(); j++) {
                                if (p_target.enemy_list.get(j).name.equals(p.name)) {
                                    enemy_add = p_target.enemy_list.get(j);
                                    break;
                                }
                            }
                            if (enemy_add != null) {
                                int save_index = p_target.enemy_list.indexOf(enemy_add);
                                FriendTemp save = p_target.enemy_list.get(0);
                                p_target.enemy_list.set(0, enemy_add);
                                p_target.enemy_list.set(save_index, save);
                            } else {
                                enemy_add = new FriendTemp(p);
                                p_target.enemy_list.add(enemy_add);
                                if (p_target.enemy_list.size() >= 2) {
                                    enemy_add.id = p_target.enemy_list
                                            .get(p_target.enemy_list.size() - 2).id + 1;
                                } else {
                                    enemy_add.id = 0;
                                }
                            }
                        }
                    }
                    if (this.template.id == 81 && this.map_little_garden != null) {
                        p_target.time_hs_little_garden = System.currentTimeMillis() + 10_000L;
                        Service.send_time_cool_down(p_target, p_target.time_hs_little_garden,
                                "Hồi sinh", 3);
                    }
                }
                if (dame_inf.dameP > 0 && sk_temp.temp.idEffSpec > 0
                        && sk_temp.temp.idEffSpec < 17) {
                    eff = p_target.get_eff(200 + sk_temp.temp.idEffSpec);
                    if (eff == null) {
                        int reduce_Eff = p_target.body.get_reduce_Eff();
                        int percent = sk_temp.temp.perEffSpec;
                        percent = (percent * (1000 - reduce_Eff)) / 1000;
                        if (percent > Util.random(1000)) {
                            int time = sk_temp.temp.timeEffSpec;
                            time = (time * (1000 - reduce_Eff)) / 1000;
                            p_target.add_new_eff((200 + sk_temp.temp.idEffSpec), 1, (time * 100));
                            dame_inf.data.add(new Option_Dame_Msg(sk_temp.temp.idEffSpec, 1, time));
                            // }
                            //
                            if (sk_temp.temp.idEffSpec == 16) {
                                Message m = new Message(74);
                                m.writer().writeByte(1);
                                m.writer().writeShort(p_target.index_map);
                                m.writer().writeShort(5);
                                m.writer().writeInt((time * 100));
                                m.writer().writeByte(1);
                                m.writer().writeByte(10);
                                this.send_msg_all_p(m, null, true);
                                m.cleanup();
                            }
                        }
                    }
                }
                if (crit) {
                    dame_inf.data.add(new Option_Dame_Msg(1010, (int) dame_inf.dameP, 0));
                }
                if (HapThuHP > 0) {
                    dame_inf.data.add(new Option_Dame_Msg(1058, (int) HapThuHP, 0));
                }
                if (dame_mine > 0) {
                    dame_inf.data.add(new Option_Dame_Msg(1014, (int) dame_mine, 0));
                    dame_mine_all += dame_mine;
                }
                list.add(dame_inf);
            }
        }
        if (dame_mine_all > 0) {
            p.hp -= dame_mine_all;
            if (p.hp <= 0) {
                p.hp = 0;
            }
            update_hp_mp_eff(p, null, 1, (int) -dame_mine_all);
        }
        if (list.size() > 0) {
            this.send_dame_msg(p, sk_temp.get_eff_skill(), list);
        }
        if (p.hp <= 0) {
            p.hp = 0;
            p.isdie = true;
            //
            if (this.map_pvp != null) {
                try {
                    Player p_in_pvp = null;
                    for (int i = 0; i < players.size(); i++) {
                        p_in_pvp = players.get(i);
                        if (!p_in_pvp.equals(p)) {
                            break;
                        }
                    }
                    if (p_in_pvp != null && !p_in_pvp.equals(p)) {
                        die_player(p, p_in_pvp);
                    }
                } catch (Exception e) {
                }
            } else {
                die_player(p, p);
            }
        }
    }

    private void update_hp_mp_eff(Player p, Mob mob, int type, int dame) throws IOException {
        Message m = new Message(55);
        if (mob != null) {
            m.writer().writeShort(mob.index);
            m.writer().writeByte(1);
            m.writer().writeByte(type);
            m.writer().writeInt(mob.hp_max);
            m.writer().writeInt(mob.hp);
            m.writer().writeInt(dame);
            m.writer().writeInt(mob.hp_max);
            m.writer().writeInt(mob.hp);
            m.writer().writeInt(0);
        } else if (p != null) {
            m.writer().writeShort(p.index_map);
            m.writer().writeByte(0);
            m.writer().writeByte(1);
            m.writer().writeInt(p.body.get_hp_max(true));
            m.writer().writeInt(p.hp);
            m.writer().writeInt(dame);
            m.writer().writeInt(p.body.get_mp_max(true));
            m.writer().writeInt(p.mp);
            m.writer().writeInt(0);
        }
        send_msg_all_p(m, p, true);
        m.cleanup();
    }

    private long[] Fire_Monster(Mob[] list_target, Player p, int idSkill, long dame)
            throws IOException {
        long[] exp_up = new long[] { 0, 0 };
        Skill_info sk_temp = p.get_skill_temp(idSkill);
        if (sk_temp == null) {
            return exp_up;
        }
        int dame_plus_percent = 0;
        int dame_magic_plus_percent = p.body.get_dame_ap();
        int crit_skill = p.body.get_crit(true);
        int multi_dame_skill = p.body.get_multi_dame_when_crit(true);
        boolean crit = (crit_skill) > Util.random(1000);
        List<Dame_Msg> list = new ArrayList<>();
        HashMap<Integer, Integer> id_mob_die = new HashMap<>(); // quest relative to mob
        //
        final long damebefore = dame;
        long dame2;
        for (int i = 0; i < list_target.length; i++) {
            Mob mob_target = list_target[i];
            if (mob_target != null && !mob_target.isdie && !p.isdie) {
                dame2 = damebefore;
                dame2 = (dame2 * (1000L + dame_plus_percent)) / 1000L;
                crit = crit_skill > Util.random(1000);
                long dame_exp = dame2;
                if (dame2 > 1 && crit) {
                    dame2 = (dame2 * (1000L + multi_dame_skill)) / 1000L;
                }
                dame2 = (dame2 * (long) sk_temp.get_dame(p))
                        / ((long) p.skill_point.get(0).get_dame(p));
                Dame_Msg dame_inf = new Dame_Msg();
                dame_inf.data = new ArrayList<>();
                dame_inf.targetM = mob_target;
                if (dame2 > 0 && idSkill != 0) {
                    dame_inf.dameM = (p.get_skill_temp(idSkill).get_dame(p) * (dame_magic_plus_percent))
                            / 1000;
                }
                dame2 = (dame2 * (1000L + p.body.get_percent_final_dame())) / 1000L;
                if (idSkill == 2038 || idSkill == 2041) {
                    // skill bien hinh bao dom, chim ung
                    if (p.get_eff(6) != null) {
                        dame2 = (dame2 * 115) / 100;
                    }
                    // fashion bao dom + chim ung
                    for (int i12 = 0; i12 < p.fashion.size(); i12++) {
                        if ((p.fashion.get(i12).id == 33 || p.fashion.get(i12).id == 34)
                                && p.fashion.get(i12).is_use) {
                            dame2 = (dame2 * 115) / 100;
                            break;
                        }
                    }
                }
                boolean miss = (5 + mob_target.level / 10) > Util.random(1000);
                if (miss) { // miss
                    dame2 = 0;
                }
                if (dame2 > 0) {
                    dame2 -= (dame2 * Util.random(10)) / 100;
                }
                long dame_to_target = dame2 + dame_inf.dameM;
                if (mob_target.boss_info != null && dame_to_target > 0) {
                    Top_Dame topdame = null;
                    for (int j = 0; j < mob_target.boss_info.TopDame.size(); j++) {
                        if (mob_target.boss_info.TopDame.get(j).name.equals(p.name)) {
                            topdame = mob_target.boss_info.TopDame.get(j);
                            break;
                        }
                    }
                    if (topdame != null) {
                        topdame.dame += dame_to_target;
                    } else {
                        topdame = new Top_Dame();
                        topdame.name = p.name;
                        topdame.dame = dame_to_target;
                        mob_target.boss_info.TopDame.add(topdame);
                    }
                }
                if (mob_target.hp == mob_target.hp_max && dame_to_target >= mob_target.hp) {
                    mob_target.hp = 1;
                } else {
                    int value1 = 0;
                    int value2 = 0;
                    int percent = 0;
                    if (mob_target.boss_info != null && !Map.is_map_dungeon(this.template.id)
                            && mob_target.mob_template.mob_id != 121
                            && mob_target.boss_info.thegioi != 2) {
                        // Quà theo máu chỉ áp dụng boss thế giới; boss làng (thegioi=2) chỉ nhận quà khi giết
                        int max_hp = mob_target.hp_max;
                        percent = max_hp / 10;
                        value1 = (mob_target.hp - 1) / percent;
                    }
                    //
                    if (this.clan_resource != null) {
                        this.clan_resource.dame += dame_to_target;
                    } else {
                        mob_target.hp -= dame_to_target;
                    }
                    //
                    if (mob_target.boss_info != null && percent > 0) { // boss hp 10% 50% reward
                        value2 = (mob_target.hp - 1) / percent;
                    }
                    boolean ch = false;
                    List<GiftBox> list_gift = new ArrayList<>();
                    for (int j = value1 - 1; j >= value2; j--) { // 10%
                        //
                        int beri_receiv = (mob_target.mob_template.mob_id - 130) * 1000;
                        beri_receiv = (beri_receiv / 100) * (100 + mob_target.boss_info.levelBoss * 10);
                        GiftBox gb_beri = new GiftBox();
                        ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb_beri.id = it_temp4.id;
                            gb_beri.type = 4;
                            gb_beri.name = it_temp4.name;
                            gb_beri.icon = it_temp4.icon;
                            gb_beri.num = beri_receiv;
                            gb_beri.color = 0;
                            list_gift.add(gb_beri);
                        }
                        //
                        if (15 > Util.random(120)) {
                            GiftBox gb_rcam = new GiftBox();
                            ItemTemplate4 it_temp4_in = ItemTemplate4
                                    .get_it_by_id((((p.level < 11 ? 11 : p.level) / 10) + 111));
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
                        switch (mob_target.mob_template.mob_id) {
                            case 137:
                            case 138: {
                                if (15 > Util.random(120)) {
                                    int id_add = (70 > Util.random(120)) ? 310
                                            : (70 > Util.random(120)) ? 311 : 312;
                                    GiftBox gb_manh = new GiftBox();
                                    ItemTemplate4 it_temp4_in = ItemTemplate4.get_it_by_id(id_add);
                                    if (it_temp4_in != null) {
                                        gb_manh.id = it_temp4_in.id;
                                        gb_manh.type = 4;
                                        gb_manh.name = it_temp4_in.name;
                                        gb_manh.icon = it_temp4_in.icon;
                                        gb_manh.num = 1;
                                        gb_manh.color = 0;
                                        list_gift.add(gb_manh);
                                    }
                                }
                                break;
                            }
                            case 139:
                            case 140: {
                                if (15 > Util.random(120)) {
                                    int id_add = (70 > Util.random(120)) ? 310
                                            : (70 > Util.random(120)) ? 311 : 312;
                                    GiftBox gb_manh = new GiftBox();
                                    ItemTemplate4 it_temp4_in = ItemTemplate4.get_it_by_id(id_add);
                                    if (it_temp4_in != null) {
                                        gb_manh.id = it_temp4_in.id;
                                        gb_manh.type = 4;
                                        gb_manh.name = it_temp4_in.name;
                                        gb_manh.icon = it_temp4_in.icon;
                                        gb_manh.num = 1;
                                        gb_manh.color = 0;
                                        list_gift.add(gb_manh);
                                    }
                                }
                                if (10 > Util.random(120)) {
                                    int id_add = (70 > Util.random(120)) ? 313
                                            : (70 > Util.random(120)) ? 314 : 315;
                                    GiftBox gb_manh = new GiftBox();
                                    ItemTemplate4 it_temp4_in = ItemTemplate4.get_it_by_id(id_add);
                                    if (it_temp4_in != null) {
                                        gb_manh.id = it_temp4_in.id;
                                        gb_manh.type = 4;
                                        gb_manh.name = it_temp4_in.name;
                                        gb_manh.icon = it_temp4_in.icon;
                                        gb_manh.num = 1;
                                        gb_manh.color = 0;
                                        list_gift.add(gb_manh);
                                    }
                                }
                                break;
                            }
                        }
                        ch = true;
                    }
                    if (ch) {
                        // Manager.gI().chatKTG(0, notice.substring(0, notice.length() - 1), 5);
                        if (list_gift.size() > 0) {
                            Service.send_gift(p, 1, "Hoạt động săn trùm", "Gây sát thương 10% Hp",
                                    list_gift, false);
                            // qua other in map
                            try {
                                List<GiftBox> list_gift_other = new ArrayList<>();
                                int beri_receiv_other = Util.random(6_000, 15_000);
                                GiftBox gb_beri_other = new GiftBox();
                                ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                                if (it_temp4 != null) {
                                    gb_beri_other.id = it_temp4.id;
                                    gb_beri_other.type = 4;
                                    gb_beri_other.name = it_temp4.name;
                                    gb_beri_other.icon = it_temp4.icon;
                                    gb_beri_other.num = beri_receiv_other;
                                    gb_beri_other.color = 0;
                                    list_gift_other.add(gb_beri_other);
                                }
                                //
                                if (30 > Util.random(120)) {
                                    GiftBox gb_RHB = new GiftBox();
                                    it_temp4 = ItemTemplate4.get_it_by_id((18 + (p.level / 10)));
                                    if (it_temp4 != null) {
                                        gb_RHB.id = it_temp4.id;
                                        gb_RHB.type = 4;
                                        gb_RHB.name = it_temp4.name;
                                        gb_RHB.icon = it_temp4.icon;
                                        gb_RHB.num = Util.random(1, 3);
                                        gb_RHB.color = 0;
                                        list_gift_other.add(gb_RHB);
                                    }
                                }
                                //
                                for (int j = 0; j < players.size(); j++) {
                                    Player p0 = players.get(j);
                                    if (p0.conn != null && !p0.equals(p) && !p0.isdie) {
                                        Service.send_gift(p0, 1, "Hoạt động săn trùm",
                                                "Tham gia hoạt động săn trùm", list_gift_other,
                                                false);
                                    }
                                }
                            } catch (Exception e) {
                            }
                        }
                    }
                    if (value1 > 4 && value2 <= 4) { // 50%
                        //
                        list_gift.clear();
                        //
                        if (20 > Util.random(120)) {
                            GiftBox gb_rcam = new GiftBox();
                            ItemTemplate4 it_temp4_in = ItemTemplate4
                                    .get_it_by_id((((p.level < 11 ? 11 : p.level) / 10) + 111));
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
                        int beri_receiv = 0;
                        switch (mob_target.mob_template.mob_id) {
                            case 135: {
                                beri_receiv = 10_000;
                                break;
                            }
                            case 136: {
                                beri_receiv = 15_000;
                                break;
                            }
                            case 137: {
                                beri_receiv = 20_000;
                                break;
                            }
                            case 138: {
                                beri_receiv = 30_000;
                                break;
                            }
                            case 139: {
                                beri_receiv = 50_000;
                                break;
                            }
                            case 140: {
                                beri_receiv = 70_000;
                                break;
                            }
                        }
                        // beri
                        beri_receiv = (beri_receiv / 100) * (100 + mob_target.boss_info.levelBoss * 10);
                        GiftBox gb_beri = new GiftBox();
                        ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb_beri.id = it_temp4.id;
                            gb_beri.type = 4;
                            gb_beri.name = it_temp4.name;
                            gb_beri.icon = it_temp4.icon;
                            gb_beri.num = beri_receiv;
                            gb_beri.color = 0;
                            list_gift.add(gb_beri);
                        }
                        //
                        if (mob_target.mob_template.mob_id >= 137
                                && mob_target.mob_template.mob_id <= 140) {
                            if (15 > Util.random(120)) {
                                int id_add = (70 > Util.random(120)) ? 310
                                        : (70 > Util.random(120)) ? 311 : 312;
                                GiftBox gb_manh = new GiftBox();
                                ItemTemplate4 it_temp4_in = ItemTemplate4.get_it_by_id(id_add);
                                if (it_temp4_in != null) {
                                    gb_manh.id = it_temp4_in.id;
                                    gb_manh.type = 4;
                                    gb_manh.name = it_temp4_in.name;
                                    gb_manh.icon = it_temp4_in.icon;
                                    gb_manh.num = 1;
                                    gb_manh.color = 0;
                                    list_gift.add(gb_manh);
                                }
                            }
                            if (10 > Util.random(120)) {
                                int id_add = (70 > Util.random(120)) ? 313
                                        : (70 > Util.random(120)) ? 314 : 315;
                                GiftBox gb_manh = new GiftBox();
                                ItemTemplate4 it_temp4_in = ItemTemplate4.get_it_by_id(id_add);
                                if (it_temp4_in != null) {
                                    gb_manh.id = it_temp4_in.id;
                                    gb_manh.type = 4;
                                    gb_manh.name = it_temp4_in.name;
                                    gb_manh.icon = it_temp4_in.icon;
                                    gb_manh.num = 1;
                                    gb_manh.color = 0;
                                    list_gift.add(gb_manh);
                                }
                            }
                        }
                        //
                        if (list_gift.size() > 0) {
                            Service.send_gift(p, 1, "Hoạt động săn trùm", "Gây sát thương 50% Hp",
                                    list_gift, false);
                        }
                        // Manager.gI().chatKTG(0, notice.substring(0, notice.length() - 1), 5);
                    }
                    p.item.update_Inventory(-1, false);
                    p.update_money();
                }
                dame_inf.dameP = dame2;
                mob_target.id_target = p.index_map;
                if (mob_target.hp <= 0 && !mob_target.isdie) {
                    mob_target.hp = 0;
                    mob_target.isdie = true;
                    mob_target.time_refresh = System.currentTimeMillis() + Mob.TIME_RESPAWN * 500;
                    exp_up[1] += mob_target.level * 2;
                    // dungeon
                    if ((Map.is_map_dungeon(this.template.id) || this.template.id == 999) && p.dungeon != null) {
                        if (p.dungeon instanceof activities.NamieTreasureDefense) {
                            ((activities.NamieTreasureDefense) p.dungeon).onMobKilled(p, mob_target);
                        } else if (p.dungeon instanceof activities.HangDong) {
                            ((activities.HangDong) p.dungeon).checkTransition();
                        }
                        if (p.dungeon.getClass() == Dungeon.class) {
                            if (Math.abs(p.level - mob_target.level) <= 10) {
                                if (15 > Util.random(120)) {
                                    LeaveItemMap.leave_item4(this, mob_target, p);
                                } else if (15 > Util.random(120)) {
                                    LeaveItemMap.leave_item7(this, mob_target, p);
                                }
                            }
                            LeaveItemMap.leave_item_quest(this, mob_target, p);
                        }
                    } else {
                        // leave item
                        if (Math.abs(p.level - mob_target.level) <= 10) {
                            if (15 > Util.random(120)) {
                                LeaveItemMap.leave_item4(this, mob_target, p);
                            } else if (5 > Util.random(120)) {
                                // LeaveItemMap.leave_item3(this, mob_target, p);
                            } else if (15 > Util.random(120)) {
                                LeaveItemMap.leave_item7(this, mob_target, p);
                            }
                        }
                        LeaveItemMap.leave_item_quest(this, mob_target, p);
                    }
                    // update quest relative to
                    if (!id_mob_die.containsKey((int) mob_target.mob_template.mob_id)) {
                        id_mob_die.put((int) mob_target.mob_template.mob_id, 1);
                    } else {
                        int oldvalue = id_mob_die.get((int) mob_target.mob_template.mob_id);
                        id_mob_die.replace((int) mob_target.mob_template.mob_id, oldvalue,
                                oldvalue + 1);
                    }
                    if (this.map_little_garden != null && !this.map_little_garden.is_finish
                            && (p.type_pk == 4 || p.type_pk == 5)) {
                        LeaveItemMap.leave_item4_little_garden(this, mob_target, p);
                    }
                    // boss
                    if (mob_target.boss_info != null && !Map.is_map_dungeon(this.template.id)) {
                        Boss boss = mob_target.boss_info;
                        boss.timeDeath = System.currentTimeMillis();
                        core.BXH.updateTopBoss(boss);
                        if (boss.thegioi == 3) {
                            boss.timeNextRespawn = boss.timeDeath + 1800000; // 30 minutes
                        } else if (boss.thegioi == 2) {
                            boss.timeNextRespawn = boss.timeDeath + 300000; // 5 minutes
                        }

                        // Debug Log
                        System.out.println("[DEBUG LOG] Boss Defeated - ID: " + boss.mob.mob_template.mob_id
                                + " | Name: " + boss.mob.mob_template.name
                                + " | LevelBoss: " + boss.levelBoss
                                + " | Village/Map ID: " + this.template.id);

                        String notice = "Tiêu diệt siêu trùm nhận: ";
                        List<GiftBox> list_gift = new ArrayList<>();

                        if (boss.thegioi == 1) {
                            Manager.gI().chatKTG(0,
                                    p.name + " đã tiêu diệt " + mob_target.mob_template.name + " bậc "
                                            + boss.levelBoss,
                                    5);

                            // 1. Rương cam cùng lv với boss
                            int level = mob_target.level;
                            if (level < 10)
                                level = 10;
                            if (level > 90)
                                level = 90;
                            int chestIdNormal = 111 + level / 10;
                            ItemTemplate4 it_rcam = ItemTemplate4.get_it_by_id(chestIdNormal);
                            if (it_rcam != null) {
                                GiftBox giftChest = new GiftBox();
                                giftChest.id = (short) chestIdNormal;
                                giftChest.type = 4;
                                giftChest.name = it_rcam.name;
                                giftChest.icon = it_rcam.icon;
                                giftChest.num = 1;
                                giftChest.color = 0;
                                list_gift.add(giftChest);
                                notice += "x1 " + it_rcam.name + ", ";
                            }

                            // 2. Rương cam cùng hệ cùng lv với boss
                            int chestIdCungHe = 121 + level / 10;
                            ItemTemplate4 it_cunghe = ItemTemplate4.get_it_by_id(chestIdCungHe);
                            if (it_cunghe != null) {
                                GiftBox giftCungHe = new GiftBox();
                                giftCungHe.id = (short) chestIdCungHe;
                                giftCungHe.type = 4;
                                giftCungHe.name = it_cunghe.name;
                                giftCungHe.icon = it_cunghe.icon;
                                giftCungHe.num = 1;
                                giftCungHe.color = 0;
                                list_gift.add(giftCungHe);
                                notice += "x1 " + it_cunghe.name + ", ";
                            }

                            // 3. 1000 vàng (Ruby)
                            p.update_ngoc(1000);
                            p.update_money();
                            notice += "1000 ruby, ";

                            // Tự động thăng Bậc Boss từ Bậc 1 đến Bậc 10 tại vị trí cũ
                            if (boss.levelBoss < 10) {
                                boss.levelBoss++;
                                boss.updateHpForLevel();
                                boss.mob.isdie = false;
                                boss.mob.id_target = -1;
                                boss.status = Boss.STATUS_ALIVE;
                                try {
                                    Manager.gI().chatKTG(0,
                                            "Siêu trùm " + mob_target.mob_template.name + " đã thăng lên Bậc "
                                                    + boss.levelBoss + " tại " + this.template.name + " khu "
                                                    + (this.zone_id + 1) + "!",
                                            5);

                                    Message m_local = new Message(1);
                                    m_local.writer().writeByte(1);
                                    m_local.writer().writeShort(boss.mob.index);
                                    m_local.writer().writeShort(boss.mob.x);
                                    m_local.writer().writeShort(boss.mob.y);
                                    this.send_msg_all_p(m_local, null, true);
                                    m_local.cleanup();
                                } catch (IOException e) {
                                    e.printStackTrace();
                                }
                            } else {
                                boss.status = Boss.STATUS_DEAD;
                                boss.mob.isdie = true;
                                this.remove_obj(mob_target.index, 1);
                                int currentMobId = mob_target.mob_template.mob_id;
                                if (currentMobId >= 135 && currentMobId <= 140) {
                                    Boss.BOSS_LIVE[currentMobId - 135] = 0;
                                }
                                try {
                                    Manager.gI().chatKTG(0,
                                            p.name + " đã tiêu diệt hoàn toàn " + mob_target.mob_template.name
                                                    + " Bậc 10 (bậc tối đa)!",
                                            5);
                                } catch (IOException e) {
                                    e.printStackTrace();
                                }
                            }
                        } else if (boss.thegioi == 2) {
                            boss.status = Boss.STATUS_DEAD;
                            this.remove_obj(mob_target.index, 1);
                            // 1. Beri 10000
                            ItemTemplate4 it_beri = ItemTemplate4.get_it_by_id(0);
                            if (it_beri != null) {
                                GiftBox giftBeri = new GiftBox();
                                giftBeri.id = 0;
                                giftBeri.type = 4;
                                giftBeri.name = it_beri.name;
                                giftBeri.icon = it_beri.icon;
                                giftBeri.num = 10000;
                                giftBeri.color = 0;
                                list_gift.add(giftBeri);
                                notice += "10000 beri, ";
                            }

                            // 2. Ruong cam cung cap
                            int chestId = ((p.level < 11 ? 11 : p.level) / 10) + 111;
                            ItemTemplate4 it_rcam = ItemTemplate4.get_it_by_id(chestId);
                            if (it_rcam != null) {
                                GiftBox giftChest = new GiftBox();
                                giftChest.id = (short) chestId;
                                giftChest.type = 4;
                                giftChest.name = it_rcam.name;
                                giftChest.icon = it_rcam.icon;
                                giftChest.num = 1;
                                giftChest.color = 0;
                                list_gift.add(giftChest);
                                notice += "x1 rương cam cùng cấp, ";
                            }

                            // 3. Da hanh trinh (100% co hoi)
                            int id_random;
                            if (5 > Util.random(120)) {
                                id_random = GiftBox.DA_HANH_TRINH_V3[Util.random(GiftBox.DA_HANH_TRINH_V3.length)];
                            } else if (20 > Util.random(120)) {
                                id_random = GiftBox.DA_HANH_TRINH_V2[Util.random(GiftBox.DA_HANH_TRINH_V2.length)];
                            } else if (70 > Util.random(120)) {
                                id_random = GiftBox.DA_HANH_TRINH_V1[Util.random(GiftBox.DA_HANH_TRINH_V1.length)];
                            } else {
                                id_random = GiftBox.DA_HANH_TRINH_V0[Util.random(GiftBox.DA_HANH_TRINH_V0.length)];
                            }
                            ItemTemplate4 itemStone = ItemTemplate4.get_it_by_id(id_random);
                            if (itemStone != null) {
                                GiftBox giftStone = new GiftBox();
                                giftStone.id = (short) id_random;
                                giftStone.type = 4;
                                giftStone.name = itemStone.name;
                                giftStone.icon = itemStone.icon;
                                giftStone.num = 1;
                                giftStone.color = 0;
                                list_gift.add(giftStone);
                                notice += "x1 " + itemStone.name + ", ";
                            }
                        } else if (boss.mob.mob_template.mob_id == 121) {
                            // Boss Mèo truyền thuyết - kết thúc sự kiện
                            notice = "Tiêu diệt mèo nhận: ";
                            boss.status = Boss.STATUS_DEAD;
                            boss.mob.isdie = true;
                            this.remove_obj(mob_target.index, 1);
                            Boss.isDualEventActive = false;

                            // ID thời trang thưởng săn mèo (Mihawk 24h)
                            int idThoiTrangMeo = 130;
                            ItemFashion it_fashion = ItemFashion.get_item(idThoiTrangMeo);

                            if (it_fashion != null) {
                                if (p.check_fashion(idThoiTrangMeo) == null) {
                                    GiftBox giftFashion = new GiftBox();
                                    giftFashion.id = (short) idThoiTrangMeo;
                                    // type 105 = thời trang (client mới load được icon fashion)
                                    giftFashion.type = 105;
                                    giftFashion.name = it_fashion.name;
                                    giftFashion.icon = it_fashion.idIcon;
                                    giftFashion.num = 1;
                                    giftFashion.color = 0;

                                    list_gift.add(giftFashion);
                                    notice += "x1 " + it_fashion.name + ", ";
                                } else {
                                    // Đã sở hữu: tặng 50000 beri thay thế
                                    ItemTemplate4 it_beri_alt = ItemTemplate4.get_it_by_id(0);
                                    if (it_beri_alt != null) {
                                        GiftBox giftBeriAlt = new GiftBox();
                                        giftBeriAlt.id = 0;
                                        giftBeriAlt.type = 4;
                                        giftBeriAlt.name = it_beri_alt.name;
                                        giftBeriAlt.icon = it_beri_alt.icon;
                                        giftBeriAlt.num = 50000;
                                        giftBeriAlt.color = 0;

                                        list_gift.add(giftBeriAlt);
                                        notice += "50000 beri (thay thế TT Mèo), ";
                                    }
                                }
                            }
                            // BƯỚC 3: Đóng ngoặc chuẩn ở đây để tách riêng nhánh else cho các Boss Thế Giới
                            // = 0 khác
                        } else {
                            // 1. Gift 1: Búa siêu cấp (tỉ lệ 5%)
                            if (Util.random(100) < 5) {
                                ItemTemplate4 it_bua = ItemTemplate4.get_it_by_id(323);
                                if (it_bua != null) {
                                    GiftBox giftBua = new GiftBox();
                                    giftBua.id = 323;
                                    giftBua.type = 4;
                                    giftBua.name = it_bua.name;
                                    giftBua.icon = it_bua.icon;
                                    giftBua.num = 1;
                                    giftBua.color = 0;
                                    list_gift.add(giftBua);
                                    notice += "x1 búa siêu cấp, ";
                                }
                            }
                            // 2. Gift 1.2: Khiên (tỉ lệ 10%)
                            if (Util.random(100) < 10) {
                                ItemTemplate7 it_temp7_in = ItemTemplate7.get_it_by_id(10);
                                if (it_temp7_in != null) {
                                    GiftBox giftKhien = new GiftBox();
                                    giftKhien.id = 10;
                                    giftKhien.type = 7;
                                    giftKhien.name = it_temp7_in.name;
                                    giftKhien.icon = it_temp7_in.icon;
                                    giftKhien.num = 1;
                                    giftKhien.color = 0;
                                    list_gift.add(giftKhien);
                                    notice += "x1 khiên, ";
                                }
                            }
                            // Thêm Kỹ năng đơn (tỉ lệ 5%)
                            if (Util.random(100) < 5) {
                                ItemTemplate4 it_kndon = ItemTemplate4.get_it_by_id(414);
                                if (it_kndon != null) {
                                    GiftBox giftKnd = new GiftBox();
                                    giftKnd.id = 414;
                                    giftKnd.type = 4;
                                    giftKnd.name = it_kndon.name;
                                    giftKnd.icon = it_kndon.icon;
                                    giftKnd.num = 1;
                                    giftKnd.color = 0;
                                    list_gift.add(giftKnd);
                                    notice += "x1 " + it_kndon.name + ", ";
                                }
                            }

                            // 2. Gift 2: Item 339 (Tỉ lệ ngẫu nhiên) hoặc Rương cam cùng cấp
                            GiftBox gift2 = new GiftBox();
                            if (Util.random(100) < 50) { // 50% chance for Item 339
                                ItemTemplate4 it_339 = ItemTemplate4.get_it_by_id(339);
                                if (it_339 != null) {
                                    gift2.id = 339;
                                    gift2.type = 4;
                                    gift2.name = it_339.name;
                                    gift2.icon = it_339.icon;
                                    gift2.num = 1;
                                    gift2.color = 0;
                                    notice += "x1 " + it_339.name + ", ";
                                }
                            } else {
                                int chestId = ((p.level < 11 ? 11 : p.level) / 10) + 111;
                                ItemTemplate4 it_rcam = ItemTemplate4.get_it_by_id(chestId);
                                if (it_rcam != null) {
                                    gift2.id = (short) chestId;
                                    gift2.type = 4;
                                    gift2.name = it_rcam.name;
                                    gift2.icon = it_rcam.icon;
                                    gift2.num = 1;
                                    gift2.color = 0;
                                    notice += "x1 rương cam cùng cấp, ";
                                }
                            }
                            if (gift2.name != null) {
                                list_gift.add(gift2);
                            }

                            // 3. Gift 3: Ngẫu nhiên đá từ 44 đến 79 (type 4)
                            int randomStoneId = Util.random(44, 80);
                            ItemTemplate4 it_stone = ItemTemplate4.get_it_by_id(randomStoneId);
                            if (it_stone != null) {
                                GiftBox gift3 = new GiftBox();
                                gift3.id = (short) randomStoneId;
                                gift3.type = 4;
                                gift3.name = it_stone.name;
                                gift3.icon = it_stone.icon;
                                gift3.num = 1;
                                gift3.color = 0;
                                list_gift.add(gift3);
                                notice += "x1 " + it_stone.name + ", ";
                            }
                            //
                            int beri_receiv = 0;
                            switch (mob_target.mob_template.mob_id) {
                                case 135: {
                                    beri_receiv = 30_000;
                                    break;
                                }
                                case 136: {
                                    beri_receiv = 50_000;
                                    break;
                                }
                                case 137: {
                                    beri_receiv = 70_000;
                                    break;
                                }
                                case 138: {
                                    beri_receiv = 100_000;
                                    break;
                                }
                                case 139: {
                                    beri_receiv = 150_000;
                                    break;
                                }
                                case 140: {
                                    beri_receiv = 200_000;
                                    break;
                                }
                            }
                            //
                            if (mob_target.boss_info.levelBoss < 10) {
                                beri_receiv = (beri_receiv / 100)
                                        * (100 + mob_target.boss_info.levelBoss * 20);
                            }
                            GiftBox gb_beri = new GiftBox();
                            ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                            if (it_temp4 != null) {
                                gb_beri.id = it_temp4.id;
                                gb_beri.type = 4;
                                gb_beri.name = it_temp4.name;
                                gb_beri.icon = it_temp4.icon;
                                gb_beri.num = beri_receiv;
                                gb_beri.color = 0;
                                list_gift.add(gb_beri);
                            }
                            notice += (beri_receiv + " beri, ");
                        }
                        // fragment drops removed
                        p.update_money();
                        if (list_gift.size() > 0) {
                            if (mob_target.mob_template.mob_id == 121) {
                                Service.send_gift(p, 1, "Quà Săn Mèo", "Tiêu diệt mèo nhận được",
                                        list_gift, false);
                            } else {
                                Service.send_gift(p, 1, "Hoạt động săn trùm",
                                        "Tiêu diệt siêu trùm bậc " + mob_target.boss_info.levelBoss,
                                        list_gift, false);
                            }
                        }
                        Manager.gI().chatKTG(0, notice.substring(0, notice.length() - 1), 5);
                        // boss up level removed
                    }
                    if (Map.is_map_boss(this.template.id) && p.map_boss_info != null
                            && p.map_boss_info.mob.contains(mob_target)) {
                        this.remove_obj(mob_target.index, 1);
                        List<GiftBox> listGift = GiftBox.get_gift_map_boss_by_level(p);
                        Service.send_gift(p, 0, "Phần thưởng Săn Quái", "Phần thưởng", listGift,
                                true);
                        p.map_boss_info.mob.clear();
                    }
                }
                // update exp
                long exp_up_add = 1;
                long a = p.level / 20;
                a = a == 0 ? 1 : a;
                long b = dame * a;
                long c = (mob_target.level - p.level) * a;
                exp_up_add = (b / 2) + (b * c / 100);
                if (exp_up_add < 1) {
                    exp_up_add = 1;
                }

                // if (p.level < 10){
                // exp_up_add = exp_up_add * 5;
                // }
                boolean isSingleDungeon = this.map_dungeon != null && this.map_dungeon.getClass() == Dungeon.class;
                if (Math.abs(p.level - mob_target.level) >= 10 && !isSingleDungeon) {
                    exp_up_add = 0;
                }
                if (!isSingleDungeon && (mob_target.mob_template.mob_id == 4 || mob_target.mob_template.mob_id == 10
                        || mob_target.mob_template.mob_id == 16 || mob_target.mob_template.mob_id == 23
                        || mob_target.mob_template.mob_id == 29 || mob_target.mob_template.mob_id == 36
                        || mob_target.mob_template.mob_id == 43 || mob_target.mob_template.mob_id == 68
                        || mob_target.mob_template.mob_id == 78 || mob_target.mob_template.mob_id == 92
                        || mob_target.mob_template.mob_id == 112 || mob_target.mob_template.mob_id == 163)) {
                    exp_up_add = 0;
                }
                if (exp_up_add < 0) {
                    exp_up_add = 0;
                }
                if (isSingleDungeon && exp_up_add <= 0) {
                    exp_up_add = mob_target.level * 10;
                }
                exp_up[0] += exp_up_add;

                if (crit) {
                    dame_inf.data.add(new Option_Dame_Msg(1010, (int) dame_inf.dameP, 0));
                }
                list.add(dame_inf);
            }
        }
        if (list.size() > 0) {
            this.send_dame_msg(p, sk_temp.get_eff_skill(), list);
        }
        for (int i = 0; i < list.size(); i++) {
            if (list.get(i).targetM.isdie) {
                this.die_mob(list.get(i).targetM);
            }
        }
        //
        EffTemplate eff = p.get_eff(2);
        if (eff != null) {
            exp_up[0] *= 2;
        }
        eff = p.get_eff(17);
        if (eff != null) {
            exp_up[0] *= 2;
        }
        // pet exp bonus: option 33 + 67 (Tăng xp đánh quái), 100 = +100% = x2 EXP
        int petExpPercent = p.body.get_xp_more();
        if (petExpPercent > 0) {
            exp_up[0] = (exp_up[0] * (100 + petExpPercent)) / 100;
        }
        // pet skill exp bonus: option 68 (Tăng Exp Skill đánh quái)
        int petSkillExpPercent = p.body.get_xp_skill_more();
        if (petSkillExpPercent > 0) {
            exp_up[1] = (exp_up[1] * (100 + petSkillExpPercent)) / 100;
        }
        // update quest
        if (id_mob_die.size() > 0) {
            for (java.util.Map.Entry<Integer, Integer> en : id_mob_die.entrySet()) {
                int id_mob = en.getKey();
                p.update_num_item_quest(1, id_mob, en.getValue());
            }
        }
        return exp_up;
    }

    private void die_mob(Mob targetM) throws IOException {
        Message m = new Message(7);
        m.writer().writeShort(targetM.index);
        m.writer().writeByte(1);
        m.writer().writeShort(targetM.index);
        m.writer().writeByte(1);
        m.writer().writeShort(0); // point pk
        send_msg_all_p(m, null, true);
        m.cleanup();
    }

    private void send_dame_msg(Player p, short typeEffSkill, List<Dame_Msg> list)
            throws IOException {
        Message m = new Message(100);
        m.writer().writeShort(p.index_map);
        m.writer().writeByte(0);
        m.writer().writeInt(p.hp);
        m.writer().writeInt(p.mp);
        // System.out.println(Map.id_eff);
        // typeEffSkill = (short) Map.id_eff;
        m.writer().writeShort(typeEffSkill);
        m.writer().writeByte(list.size());
        for (int j = 0; j < list.size(); j++) {
            Dame_Msg temp = list.get(j);
            if (temp.targetM != null) {
                m.writer().writeShort(temp.targetM.index);
                m.writer().writeByte(1);
                m.writer().writeInt((int) temp.dameP);
                m.writer().writeInt((int) temp.dameM); // dame plus
                m.writer().writeInt(temp.targetM.hp);
                m.writer().writeByte(temp.data.size());
                for (int i = 0; i < temp.data.size(); i++) {
                    m.writer().writeShort(temp.data.get(i).type);
                    m.writer().writeShort(temp.data.get(i).hp);
                    m.writer().writeShort(temp.data.get(i).time);
                }
            } else {
                m.writer().writeShort(temp.targetP.index_map);
                m.writer().writeByte(0);
                m.writer().writeInt((int) temp.dameP);
                m.writer().writeInt((int) temp.dameM); // dame plus
                m.writer().writeInt(temp.targetP.hp);
                //
                m.writer().writeByte(temp.data.size());
                for (int i = 0; i < temp.data.size(); i++) {
                    m.writer().writeShort(temp.data.get(i).type);
                    m.writer().writeShort(temp.data.get(i).hp);
                    m.writer().writeShort(temp.data.get(i).time);
                }
            }
        }
        send_msg_all_p(m, p, true);
        m.cleanup();
    }

    public void send_msg_all_p(Message m, Player p, boolean all) throws IOException {
        for (int i = 0; i < players.size(); i++) {
            Player p0 = players.get(i);
            if (p0.conn == null) {
                synchronized (this) {
                    players.remove(p0);
                }
                // remove_obj(p0.index_map, 0);
            } else {
                if (all || (p != null && p0.index_map != p.index_map)) {
                    p0.conn.addmsg(m);
                }
            }
        }
    }

    public void send_chat(Player p, Message m2) throws IOException {
        String s = m2.reader().readUTF();
        String txt = s.trim().toLowerCase();

        if (p.conn.user.equals("admin")) {
            if (txt.equals("menu")) {
                MenuController.send_dynamic_menu(p, 999, "Menu Admin", new String[] { "Bao tri",
                        "1t Beri + 1t Ruby", "Uplevel", "setXP", "get item", "save data", "updateTB", "Tao Giftcode",
                        "Reset Tich Luy", "Reset Tich Tieu", "Reset Hang Dong" },
                        null);
                Service.send_box_ThongBao_OK(p,
                        "Neu menu khong hien, hay dung lenh chat:\nadmin baotri\nadmin tien\nadmin level\nadmin setxp\nadmin item\nadmin save\nadmin updatetb\nadmin taocode\nadmin resetnap\nadmin resettieu\nadmin resethangdong");
                return;
            } else if (txt.startsWith("admin ")) {
                String cmd = txt.substring(6);
                if (cmd.equals("baotri"))
                    MenuController.Menu_Admin(p, (byte) 0);
                else if (cmd.equals("tien"))
                    MenuController.Menu_Admin(p, (byte) 1);
                else if (cmd.equals("level"))
                    MenuController.Menu_Admin(p, (byte) 2);
                else if (cmd.equals("setxp"))
                    MenuController.Menu_Admin(p, (byte) 3);
                else if (cmd.equals("item"))
                    MenuController.Menu_Admin(p, (byte) 4);
                else if (cmd.equals("save"))
                    MenuController.Menu_Admin(p, (byte) 5);
                else if (cmd.equals("updatetb"))
                    MenuController.Menu_Admin(p, (byte) 6);
                else if (cmd.equals("taocode"))
                    MenuController.Menu_Admin(p, (byte) 7);
                else if (cmd.equals("resetnap") || cmd.equals("reset_tichluy"))
                    MenuController.Menu_Admin(p, (byte) 8);
                else if (cmd.equals("resettieu") || cmd.equals("reset_tichtieu"))
                    MenuController.Menu_Admin(p, (byte) 9);
                else if (cmd.equals("resethangdong") || cmd.equals("reset_hangdong"))
                    MenuController.Menu_Admin(p, (byte) 10);
                else if (cmd.equals("dualboss")) {
                    map.Boss.spawnDualEventBosses();
                    Service.send_box_ThongBao_OK(p, "Đã gọi sự kiện Boss Mèo truyền thuyết!");
                } else if (cmd.startsWith("setdanhhieu ")) {
                    String[] parts = cmd.split(" ");
                    if (parts.length >= 3) {
                        String targetName = parts[1];
                        int dhId = Integer.parseInt(parts[2]);
                        Player p0 = map.Map.get_player_by_name_allmap(targetName);
                        if (p0 != null) {
                            p0.idDanhHieu = (short) dhId;
                            if (p0.listDanhHieu == null)
                                p0.listDanhHieu = new java.util.ArrayList<>();
                            if (!p0.listDanhHieu.contains(dhId))
                                p0.listDanhHieu.add(dhId);
                            Service.send_box_ThongBao_OK(p,
                                    "Set danh hieu " + dhId + " cho " + targetName + " thanh cong!");
                            Service.send_box_ThongBao_OK(p0,
                                    "Ban nhan duoc danh hieu moi! Hay chon no trong menu Hanh Trang.");
                        } else {
                            Service.send_box_ThongBao_OK(p, "Khong tim thay player " + targetName);
                        }
                    }
                } else
                    Service.send_box_ThongBao_OK(p, "Lenh admin khong hop le!");
                return;
            }
        }
        if (txt.equals("danhhieu") || txt.equals("/danhhieu")) {
            MenuController.Menu_DanhHieu(p, (byte) 0);
            return;
        } else if (txt.startsWith("adddh ") || txt.startsWith("/adddh ")) {
            try {
                int dhId = Integer.parseInt(txt.split(" ")[1]);
                if (p.listDanhHieu == null)
                    p.listDanhHieu = new java.util.ArrayList<>();
                if (!p.listDanhHieu.contains(dhId))
                    p.listDanhHieu.add(dhId);
                Service.send_box_ThongBao_OK(p, "Ban da nhan duoc danh hieu " + dhId + "!");
            } catch (Exception e) {
            }
            return;
        }
        this.send_chat_popup(0, p.index_map, s, false);
    }

    public void send_chat_popup(int type, int id_p, String s, boolean all) throws IOException {
        Message m = new Message(17);
        switch (type) {
            case 0: {
                m.writer().writeShort(id_p);
                m.writer().writeByte(0);
                m.writer().writeUTF(s);
                Player p0 = this.get_player_by_id_inmap(id_p);
                this.send_msg_all_p(m, p0, all);
                break;
            }
        }
        m.cleanup();
    }

    public void send_in4_obj_inmap(Player p) throws IOException {
        // send npc
        if (this.template.npcs.size() > 0) {
            Message mnpc = new Message(16);
            mnpc.writer().writeByte(this.template.npcs.size());
            for (int i = 0; i < this.template.npcs.size(); i++) {
                Npc npc = this.template.npcs.get(i);
                mnpc.writer().writeShort(npc.iditem);
                mnpc.writer().writeUTF(npc.name);
                mnpc.writer().writeUTF(npc.namegt);
                mnpc.writer().writeUTF(npc.chat);
                mnpc.writer().writeShort(npc.x);
                mnpc.writer().writeShort(npc.y);
                mnpc.writer().writeByte(npc.isPerson);
                mnpc.writer().writeByte(npc.typeIcon);
                mnpc.writer().writeByte(npc.wBlock);
                mnpc.writer().writeByte(npc.hBlock);
                mnpc.writer().writeByte(npc.b3);
                if (npc.b3 == 0) {
                    mnpc.writer().writeByte(npc.dataFrame[0]);
                    mnpc.writer().writeByte(npc.dataFrame[1]);
                } else {
                    mnpc.writer().writeShort(npc.head);
                    mnpc.writer().writeShort(npc.hair);
                    mnpc.writer().writeByte(npc.wearing.length);
                    for (int j = 0; j < npc.wearing.length; j++) {
                        if (npc.wearing[j] == -1) {
                            mnpc.writer().writeByte(-1);
                        } else {
                            mnpc.writer().writeByte(1);
                            mnpc.writer().writeShort(npc.wearing[j]);
                        }
                    }
                }
            }
            p.conn.addmsg(mnpc);
            mnpc.cleanup();
        }
        // map boss
        if (p.map_boss_info != null && Map.is_map_boss(this.template.id)) {
            for (int i = 0; i < p.map_boss_info.mob.size(); i++) {
                Mob mob = p.map_boss_info.mob.get(i);
                if (mob != null && !mob.isdie) {
                    // System.out.println("send location mob mapboss");
                    Message m_local = new Message(1);
                    m_local.writer().writeByte(1);
                    m_local.writer().writeShort(mob.index);
                    m_local.writer().writeShort(mob.x);
                    m_local.writer().writeShort(mob.y);
                    p.conn.addmsg(m_local);
                    m_local.cleanup();
                }
            }
        }
        if (p.dungeon != null && Map.is_map_dungeon(this.template.id)) {
            for (int i = 0; i < p.dungeon.mobs.size(); i++) {
                Mob mob = p.dungeon.mobs.get(i);
                if (mob != null && !mob.isdie && mob.map.equals(this)) {
                    Message m_local = new Message(1);
                    m_local.writer().writeByte(1);
                    m_local.writer().writeShort(mob.index);
                    m_local.writer().writeShort(mob.x);
                    m_local.writer().writeShort(mob.y);
                    p.conn.addmsg(m_local);
                    m_local.cleanup();
                }
            }
        }
        if (this.template.id == 81 && this.map_little_garden != null) {
            for (int i = 0; i < this.map_little_garden.mobs.size(); i++) {
                Mob mob = this.map_little_garden.mobs.get(i);
                if (mob != null && mob.map.equals(this)) {
                    Message m_local = new Message(1);
                    m_local.writer().writeByte(1);
                    m_local.writer().writeShort(mob.index);
                    m_local.writer().writeShort(mob.x);
                    m_local.writer().writeShort(mob.y);
                    p.conn.addmsg(m_local);
                    m_local.cleanup();
                }
            }
        }
        // boss
        boolean haveBoss = false;
        if (this.map_bossHunt != null && this.map_bossHunt.active) {
            for (int i = 0; i < this.map_bossHunt.mobs.size(); i++) {
                Mob mob = this.map_bossHunt.mobs.get(i);
                if (mob != null && !mob.isdie && mob.map.equals(this)) {
                    Message m_local = new Message(1);
                    m_local.writer().writeByte(1);
                    m_local.writer().writeShort(mob.index);
                    m_local.writer().writeShort(mob.x);
                    m_local.writer().writeShort(mob.y);
                    p.conn.addmsg(m_local);
                    m_local.cleanup();
                    haveBoss = true; // đánh dấu đã gửi boss để không gửi list_mob trống phía dưới
                }
            }
        }
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            if (!Boss.ENTRYS.get(i).mob.isdie && Boss.ENTRYS.get(i).mob.map.equals(p.map)) {
                Message m_local = new Message(1);
                m_local.writer().writeByte(1);
                m_local.writer().writeShort(Boss.ENTRYS.get(i).mob.index);
                m_local.writer().writeShort(Boss.ENTRYS.get(i).mob.x);
                m_local.writer().writeShort(Boss.ENTRYS.get(i).mob.y);
                p.conn.addmsg(m_local);
                m_local.cleanup();
                haveBoss = true;
                break;
            }
        }
        // send mob
        if (!haveBoss) {
            for (int i = 0; i < this.list_mob.length; i++) {
                Mob mob = Mob.ENTRYS.get(this.list_mob[i]);
                if (mob != null && !mob.isdie) {
                    Message m_local = new Message(1);
                    m_local.writer().writeByte(1);
                    m_local.writer().writeShort(this.list_mob[i]);
                    m_local.writer().writeShort(mob.x);
                    m_local.writer().writeShort(mob.y);
                    p.conn.addmsg(m_local);
                    m_local.cleanup();
                }
            }
        }
        //
        if (!Map.is_map_dont_show_other_info(this.template.id)) {
            // send player
            for (int i = 0; i < players.size(); i++) {
                if (p.index_map != players.get(i).index_map) {
                    Message m_local = new Message(1);
                    m_local.writer().writeByte(0);
                    m_local.writer().writeShort(players.get(i).index_map);
                    m_local.writer().writeShort(players.get(i).x);
                    m_local.writer().writeShort(players.get(i).y);
                    p.conn.addmsg(m_local);
                    m_local.cleanup();
                    //
                    p.id_meet_in_map.add("" + players.get(i).index_map);
                    //
                    if (players.get(i).ship_pet != null && players.get(i).ship_pet.map != null
                            && players.get(i).ship_pet.map.equals(p.map)) {
                        m_local = new Message(1);
                        m_local.writer().writeByte(0);
                        m_local.writer().writeShort(players.get(i).ship_pet.index_map);
                        m_local.writer().writeShort(players.get(i).ship_pet.x);
                        m_local.writer().writeShort(players.get(i).ship_pet.y);
                        p.conn.addmsg(m_local);
                        m_local.cleanup();
                    }
                }
            }
            //
            boolean check = true;
            for (int i = 0; i < DataTemplate.mSea.length; i++) {
                if (DataTemplate.mSea[i][0] == this.template.id) {
                    check = false;
                    break;
                }
            }
            //
            Message m_to_other = new Message(1);
            m_to_other.writer().writeByte(0);
            m_to_other.writer().writeShort(p.index_map);
            m_to_other.writer().writeShort(p.x);
            m_to_other.writer().writeShort(p.y);
            this.send_msg_all_p(m_to_other, p, check);
            m_to_other.cleanup();
        }
        if (p.party != null) {
            p.party.send_info();
        }
        p.change_new_date();
        p.update_info_to_all();
    }

    public void send_char_in4_inmap(Player p, short id) throws IOException {
        Player p0 = get_player_by_id_inmap(id);
        if (p0 != null) {
            if (!p0.map.equals(p.map)) {
                return;
            }
            boolean new_enter = false;
            if (!p.id_meet_in_map.contains("" + p0.index_map)) {
                p.id_meet_in_map.add("" + p0.index_map);
                new_enter = true;
            }
            int dir_ = 1;
            Message m = new Message(-5);
            m.writer().writeShort(p0.index_map);
            m.writer().writeByte(0);
            m.writer().writeByte(0); // typePlayer
            m.writer().writeByte(p0.typePirate); // typePirate
            m.writer().writeByte(p.type_pk); // typePk
            m.writer().writeByte(new_enter ? dir_ : 0); // eff dir new
            m.writer().writeByte(-1); // index team
            m.writer().writeUTF(p0.name);
            m.writer().writeShort(p0.level);
            m.writer().writeInt(p0.body.get_hp_max(true));
            m.writer().writeInt(p0.hp);
            m.writer().writeShort(p0.thongthao);
            m.writer().writeInt(BXH.get_rank_wanted(p0.name));
            m.writer().writeByte(p0.body.get_level_perfect());
            m.writer().writeByte(p0.clazz);
            m.writer().writeByte(-1); // dir new
            m.writer().writeByte(p0.item.it_heart != null ? p0.item.it_heart.levelup : 0); // levelheart
            //
            m.writer().writeShort(-1); // body bay
            m.writer().writeShort(-1); // leg bay
            m.writer().writeShort(-1); // weapon bay
            //
            p.conn.addmsg(m);
            m.cleanup();
            //
            Service.pet(p0, p, false);
            Service.update_PK(p0, p, false);
            Service.update_PK(p, p0, false);
            Service.Weapon_fashion(p0, p, false);
            Service.getThanhTich(p0, p);
            Service.charWearing(p0, p, false);
            // Gửi danh hiệu effect
            if (p0.idDanhHieu >= 0) {
                DanhHieuTemplate dhP0 = DanhHieuTemplate.get(p0.idDanhHieu);
                if (dhP0 != null && dhP0.getEffectId() >= 0) {
                    Service.send_danhieu_effect(p0, p, dhP0.getEffectId(), false);
                }
            }
            if (p.idDanhHieu >= 0) {
                DanhHieuTemplate dhP = DanhHieuTemplate.get(p.idDanhHieu);
                if (dhP != null && dhP.getEffectId() >= 0) {
                    Service.send_danhieu_effect(p, p0, dhP.getEffectId(), false);
                }
            }
            //
            this.update_boat(p0, p, false);
            this.update_boat(p, p0, false);
            //
            EffTemplate eff = p0.get_eff(7);
            if (eff != null) {
                Message m2 = new Message(-71);
                m2.writer().writeByte(1);
                m2.writer().writeShort(p0.index_map);
                m2.writer().writeByte(0);
                m2.writer().writeInt((int) ((eff.time - System.currentTimeMillis()) / 1000));
                p.conn.addmsg(m2);
                m2.cleanup();
            }
            // clan
            if (p0.clan != null) {
                Clan.send_me_to_other(p0, p, false);
            }
        } else {
            Ship_pet spet = Ship_pet.get_pet(id);
            if (spet == null) {
                spet = p.ship_pet;
            }
            if (spet != null && spet.map != null && spet.map.equals(p.map)) {
                Message m = new Message(-5);
                m.writer().writeShort(spet.index_map);
                m.writer().writeByte(0);
                m.writer().writeByte(2); // typePlayer
                m.writer().writeByte(spet.main_ship.typePirate); // typePirate
                m.writer().writeByte(-1); // typePk
                m.writer().writeByte(1);
                m.writer().writeByte(-1); // index team
                m.writer().writeUTF(spet.name);
                m.writer().writeShort(1); // level
                m.writer().writeInt(spet.hp_max);
                m.writer().writeInt(spet.hp);
                m.writer().writeShort(0);
                m.writer().writeInt(-1);
                m.writer().writeByte(0);
                //
                m.writer().writeShort(999);
                m.writer().writeByte(1);
                m.writer().writeShort(spet.main_ship.index_map);
                m.writer().writeByte(spet.main_ship.typePirate);
                //
                m.writer().writeShort(-1); // body bay
                m.writer().writeShort(-1); // leg bay
                m.writer().writeShort(-1); // weapon bay
                //
                p.conn.addmsg(m);
                m.cleanup();
                //
            }
        }
    }

    public Player get_player_by_id_inmap(int id) {
        Player p0 = null;
        for (int i = 0; i < players.size(); i++) {
            Player p01 = players.get(i);
            if (p01 != null && p01.index_map == id) {
                p0 = p01;
                break;
            }
        }
        return p0;
    }

    public static boolean map_cant_save_site(int id) {
        boolean check = false;
        for (int i = 0; i < DataTemplate.mSea.length; i++) {
            if (DataTemplate.mSea[i][1] == id) {
                check = true;
                break;
            }
        }
        return check || id == 64 || id == 984 || id == 1000 || id == 9998 || id == 9999 || id == 115
                || id == 81 || id == 120 || id == 122 || id == 123 || id == 119 || id == 58
                || Map.is_map_boss(id) || Map.is_map_dungeon(id)
                || activities.BossHunt.isBossHuntMap(id);
    }

    public static boolean is_map_sea(int id) {
        return id == 7;
    }

    public void change_flag(Player p, int type) throws IOException {
        if (!(this.map_pvp != null || this.template.id == 1000)) {
            if (p.type_pk == 1 && type == -1) {
                return;
            }
            if (p.pointPk >= 400 && type == -1) {
                type = 1;
            }
        } else if (type == 1) {
            type = -1;
        }
        if (p.clan != null && p.map.map_little_garden != null) {
            if (p.clan.equals(p.map.map_little_garden.clan1)) {
                type = 4;
            } else {
                type = 5;
            }
        }
        p.type_pk = (byte) type;
        for (int i = 0; i < this.players.size(); i++) {
            Player p0 = this.players.get(i);
            Service.update_PK(p, p0, false);
        }
    }

    public synchronized void pick_item(Player p, Message m2) throws IOException {
        if (p.isdie || p.rms.length > 2 && p.rms[2].length > 0 && p.rms[2][0] == 0) {
            // return;
        }
        short id = m2.reader().readShort();
        byte cat = m2.reader().readByte();
        // System.out.println(id);
        // System.out.println(cat);
        byte code_response = -1;
        //
        switch (cat) {
            case 3: {
                for (int i = 0; i < list_it_map.length; i++) {
                    if (list_it_map[i] != null && list_it_map[i].category == cat
                            && list_it_map[i].index == id) {
                        if (list_it_map[i].id_master == -1 || (list_it_map[i].id_master != -1
                                && list_it_map[i].id_master == p.index_map)) {
                            ItemTemplate3 temp3 = ItemTemplate3.get_it_by_id(list_it_map[i].id);
                            if (temp3 != null && p.rms.length > 2 && p.rms[2].length > 3) {
                                // System.out.println(p.rms[2][1] + " " + temp3.color);
                                if (p.rms[2][1] == 1 && temp3.color < 2) {
                                    // return;
                                }
                                if (p.rms[2][1] == 2 && temp3.color < 3) {
                                    // return;
                                }
                            }
                            //
                            if (temp3 != null) {
                                Item_wear it_add = new Item_wear();
                                it_add.setup_template_by_id(temp3);
                                if (it_add.template != null) {
                                    if (!p.item.add_item_bag3(it_add)) {
                                        // Service.send_box_ThongBao_OK(p, "Hành trang đầy");
                                        return;
                                    }
                                    p.item.update_Inventory(-1, false);
                                }
                            }
                            list_it_map[i] = null;
                            code_response = 0;
                        } else {
                            code_response = 1;
                        }
                        break;
                    }
                }
                break;
            }
            case 5: { // quest
                for (int i = 0; i < list_it_map.length; i++) {
                    if (list_it_map[i] != null && list_it_map[i].category == cat
                            && list_it_map[i].index == id) {
                        if (list_it_map[i].id < DataTemplate.NamePotionquest.length) {
                            if (list_it_map[i].id_master == -1 || (list_it_map[i].id_master != -1
                                    && list_it_map[i].id_master == p.index_map)) {
                                if (!p.item.add_item_bag47(5, list_it_map[i].id,
                                        list_it_map[i].quant)) {
                                    // Service.send_box_ThongBao_OK(p, "Hành trang đầy");
                                    return;
                                }
                                p.item.update_Inventory(-1, false);
                                p.update_num_item_quest(2, list_it_map[i].id, list_it_map[i].quant);
                                list_it_map[i] = null;
                                code_response = 0;
                            } else {
                                code_response = 1;
                            }
                        }
                        break;
                    }
                }
                break;
            }
            case 4: {
                for (int i = 0; i < list_it_map.length; i++) {
                    if (list_it_map[i] != null && list_it_map[i].category == cat
                            && list_it_map[i].index == id) {
                        if (this.template.id == 81 && this.map_little_garden != null) {
                            if (list_it_map[i].id_master == -1 || (list_it_map[i].id_master != -1
                                    && list_it_map[i].id_master == p.index_map)) {
                                //
                                switch (list_it_map[i].id) {
                                    case 94: {
                                        if (p.type_pk == 4) {
                                            for (int j = 0; j < this.players.size(); j++) {
                                                Player p0 = this.players.get(j);
                                                if (p0 != null && p0.conn != null && p0.type_pk == 5
                                                        && !p0.isdie) {
                                                    die_player(p0, p);
                                                    p0.time_hs_little_garden = System.currentTimeMillis() + 10_000L;
                                                    Service.send_time_cool_down(p0,
                                                            p0.time_hs_little_garden, "Hồi sinh",
                                                            3);
                                                }
                                            }
                                        } else {
                                            for (int j = 0; j < this.players.size(); j++) {
                                                Player p0 = this.players.get(j);
                                                if (p0 != null && p0.conn != null && p0.type_pk == 4
                                                        && !p0.isdie) {
                                                    die_player(p0, p);
                                                    p0.time_hs_little_garden = System.currentTimeMillis() + 10_000L;
                                                    Service.send_time_cool_down(p0,
                                                            p0.time_hs_little_garden, "Hồi sinh",
                                                            3);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                    case 95: {
                                        for (int j = 0; j < this.players.size(); j++) {
                                            Player p0 = this.players.get(j);
                                            if (p0 != null && p0.conn != null
                                                    && p0.type_pk == p.type_pk && p0.isdie) {
                                                p0.time_hs_little_garden = 0;
                                            }
                                        }
                                        break;
                                    }
                                    case 96: {
                                        LittleGarden.update_mp(this, p.type_pk, 0);
                                        break;
                                    }
                                    case 97: {
                                        LittleGarden.update_mp(this, p.type_pk, 2);
                                        break;
                                    }
                                    case 98: {
                                        LittleGarden.update_hp(this, p.type_pk, 2);
                                        break;
                                    }
                                    case 99: {
                                        LittleGarden.update_mp(this, p.type_pk, 1);
                                        break;
                                    }
                                    case 100: {
                                        LittleGarden.update_hp(this, p.type_pk, 1);
                                        break;
                                    }
                                }
                                //
                                list_it_map[i] = null;
                                code_response = 2;
                            } else {
                                code_response = 1;
                            }
                        } else {
                            for (int i2 = 0; i2 < LeaveItemMap.ITEM_POTION.length; i2++) {
                                if (LeaveItemMap.ITEM_POTION[i2] == list_it_map[i].id
                                        || (list_it_map[i].id >= 7 && list_it_map[i].id <= 17)) {
                                    if (list_it_map[i].id_master == -1
                                            || (list_it_map[i].id_master != -1
                                                    && list_it_map[i].id_master == p.index_map)) {
                                        if (list_it_map[i].id == 0) { // beri
                                            if (p.rms.length > 2 && p.rms[2].length > 3
                                                    && p.rms[2][3] == 1) {
                                                return;
                                            }
                                            p.update_vang(list_it_map[i].quant);
                                            p.update_money();
                                        } else if (list_it_map[i].id == 1) { // ruby
                                            if (p.rms.length > 2 && p.rms[2].length > 3
                                                    && p.rms[2][3] == 1) {
                                                return;
                                            }
                                            // p.update_ngoc(list_it_map[i].quant);
                                            // p.update_money();
                                        } else {
                                            if (p.rms.length > 2 && p.rms[2].length > 3) {
                                                ItemTemplate4 itemTemplate4 = ItemTemplate4
                                                        .get_it_by_id(list_it_map[i].id);
                                                // System.out.println(p.rms[2][2] + " : " +
                                                // itemTemplate4.type);
                                                if (p.rms[2][2] == 1 && itemTemplate4.type != 1) {
                                                    return;
                                                }
                                                if (p.rms[2][2] == 2 && itemTemplate4.type != 2) {
                                                    return;
                                                }
                                            }
                                            if (!p.item.add_item_bag47(4, list_it_map[i].id,
                                                    list_it_map[i].quant)) {
                                                // Service.send_box_ThongBao_OK(p, "Hành trang
                                                // đầy");
                                                return;
                                            }
                                            p.item.update_Inventory(-1, false);
                                        }
                                        list_it_map[i] = null;
                                        code_response = 0;
                                    } else {
                                        code_response = 1;
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
                break;
            }
            case 7: {
                for (int i = 0; i < list_it_map.length; i++) {
                    if (list_it_map[i] != null && list_it_map[i].category == cat
                            && list_it_map[i].index == id) {
                        if (list_it_map[i].id_master == -1 || (list_it_map[i].id_master != -1
                                && list_it_map[i].id_master == p.index_map)) {
                            if (!p.item.add_item_bag47(7, list_it_map[i].id,
                                    list_it_map[i].quant)) {
                                // Service.send_box_ThongBao_OK(p, "Hành trang đầy");
                                return;
                            }
                            p.item.update_Inventory(-1, false);
                            list_it_map[i] = null;
                            code_response = 0;
                        } else {
                            code_response = 1;
                        }
                        break;
                    }
                }
                break;
            }
        }
        switch (code_response) {
            case -1: {
                remove_obj(id, cat);
                break;
            }
            case 0: { // ok
                Message m = new Message(12);
                m.writer().writeShort(id);
                m.writer().writeByte(cat);
                m.writer().writeShort(p.index_map);
                p.conn.addmsg(m);
                m.cleanup();
                remove_obj(id, cat);
                break;
            }
            case 1: {
                if (p.time_pick_item_other < System.currentTimeMillis()) {
                    p.time_pick_item_other = System.currentTimeMillis() + 7_000L;
                    Message mnext = new Message(-31);
                    mnext.writer().writeByte(0);
                    mnext.writer().writeUTF("Vật phẩm của người khác");
                    mnext.writer().writeByte(0);
                    mnext.writer().writeShort(-1);
                    p.conn.addmsg(mnext);
                    mnext.cleanup();
                }
                break;
            }
            case 2: { // little garden
                if (this.map_little_garden != null && !this.map_little_garden.is_finish
                        && (p.type_pk == 4 || p.type_pk == 5)) {
                    //
                    Message m = new Message(33);
                    m.writer().writeShort(id);
                    m.writer().writeByte(cat);
                    m.writer().writeByte(p.type_pk == 4 ? 0 : 1);
                    p.conn.addmsg(m);
                    m.cleanup();
                    // remove_obj(id, cat);
                }
                break;
            }
        }
    }

    public void send_data(Player p) throws IOException {
        Message m = new Message(0);
        m.writer().writeShort(this.template.id);
        m.writer().writeByte(this.zone_id);
        m.writer().writeByte(this.template.type_view_p);
        m.writer().writeShort(p.x);
        m.writer().writeShort(p.y);
        m.writer().writeInt(p.body.get_hp_max(true));
        m.writer().writeInt(p.hp);
        m.writer().writeInt(p.body.get_mp_max(true));
        m.writer().writeInt(p.mp);
        m.writer().writeByte(this.template.b);
        m.writer().writeByte(this.template.specMap);
        if (this.template.b == 1) {
            m.writer().writeInt(this.template.data[0].length);
            m.writer().write(this.template.data[0]);
            m.writer().writeInt(this.template.data[1].length);
            m.writer().write(this.template.data[1]);
            boolean isSingleDungeon = this.map_dungeon != null && this.map_dungeon.getClass() == Dungeon.class;
            if (this.template.id == 999
                    || (!isSingleDungeon && (this.map_dungeon != null || Map.is_map_dungeon(this.template.id)))) {
                m.writer().writeByte(0);
            } else {
                m.writer().writeByte(this.template.vgos.size());
                for (int i = 0; i < this.template.vgos.size(); i++) {
                    m.writer().writeUTF(this.template.vgos.get(i).map_go[0].template.name);
                    m.writer().writeShort(this.template.vgos.get(i).xold);
                    m.writer().writeShort(this.template.vgos.get(i).yold);
                }
            }
        }
        m.writer().writeByte(this.template.IDBack);
        m.writer().writeShort(this.template.HBack);
        m.writer().writeByte(this.template.id_eff_map);
        m.writer().writeByte(this.template.level);
        m.writer().writeByte(this.template.typeChangeMap);
        if (this.template.specMap == 3) {
            m.writer().writeByte(this.template.mPosMapTrain.length);
            for (int i = 0; i < this.template.mPosMapTrain.length; i++) {
                for (int j = 0; j < this.template.mPosMapTrain[i].length; j++) {
                    m.writer().writeByte(this.template.mPosMapTrain[i][j]);
                }
            }
            m.writer().writeUTF(this.template.strTimeChange);
        }
        m.writer().writeUTF(this.template.name);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public void goto_map(Player p) throws IOException {
        if (p.conn != null) {
            this.enter_map(p);
            this.send_data(p);
            //
            boolean send_move = true;
            for (int i = 0; i < DataTemplate.mSea.length; i++) {
                if (DataTemplate.mSea[i][0] == this.template.id) {
                    send_move = false;
                    break;
                }
            }
            if (send_move) {
                Message mmove = new Message(1);
                mmove.writer().writeByte(0);
                mmove.writer().writeShort(p.index_map);
                mmove.writer().writeShort(p.x);
                mmove.writer().writeShort(p.y);
                p.list_msg_cache.add(mmove);
                mmove.cleanup();
            }
            // conn.p.map.enter_zone(conn.p);
            if (Map.is_map_save_revival(this.template.id)) {
                p.id_map_save = this.template.id;
                p.time_can_hs = 7;
            }
        }
    }

    private static boolean is_map_save_revival(int id) {
        for (int i = 0; i < MenuController.ID_MAP_LANG.length; i++) {
            if (id == MenuController.ID_MAP_LANG[i] && id != 113 && id != 79 && id != 191) {
                return true;
            }
        }
        return false;
    }

    public void send_boat(Player p, boolean is_have_my_boat) throws IOException {
        for (int i = 0; i < DataTemplate.mSea.length; i++) {
            if (DataTemplate.mSea[i][0] == this.template.id) {
                Message m = new Message(-56);
                int size = is_have_my_boat ? this.template.list_boat.size()
                        : (this.template.list_boat.size() - 1);
                m.writer().writeByte(size);
                if (is_have_my_boat) {
                    m.writer().writeShort(p.index_map);
                    m.writer().writeShort(this.template.list_boat.get(0).x);
                    m.writer().writeShort(this.template.list_boat.get(0).y);
                    m.writer().writeByte(4);
                    m.writer().writeShort(0);
                    m.writer().writeShort(1);
                    m.writer().writeShort(2);
                    m.writer().writeShort(3);
                }
                for (int j = 1; j < this.template.list_boat.size(); j++) {
                    m.writer().writeShort(-1);
                    m.writer().writeShort(this.template.list_boat.get(j).x);
                    m.writer().writeShort(this.template.list_boat.get(j).y);
                    m.writer().writeByte(0);
                }
                p.list_msg_cache.add(m);
                m.cleanup();
                break;
            }
        }
    }

    public void enter_zone(Player p) throws IOException {
        p.ischangemap = false;
        p.xold = p.x;
        p.yold = p.y;
        Message m = new Message(21);
        m.writer().writeByte(this.zone_id);
        m.writer().writeByte(0);
        m.writer().writeShort(p.x);
        m.writer().writeShort(p.y);
        m.writer().writeInt(p.body.get_hp_max(true));
        m.writer().writeInt(p.hp);
        m.writer().writeInt(p.body.get_mp_max(true));
        m.writer().writeInt(p.mp);
        m.writer().writeByte(p.map.template.IDBack);
        m.writer().writeShort(p.map.template.HBack);
        p.conn.addmsg(m);
        m.cleanup();
        //
        Service.update_PK(p, p, true);
        Service.pet(p, p, true);
        // Service.send_Quest(p,true);
        this.send_boat(p, true);
        this.update_boat(p, p, true);
    }

    public void update_boat(Player p0, Player p, boolean cache) throws IOException {
        boolean check = false;
        for (int i = 0; i < DataTemplate.mSea.length; i++) {
            if (DataTemplate.mSea[i][1] == this.template.id) {
                check = true;
                break;
            }
        }
        if (check || p0.map.template.id == 984) {
            Message m = new Message(-62);
            m.writer().writeShort(p0.index_map);
            m.writer().writeByte(0);
            m.writer().writeByte(4);
            short[] part_boat = p0.get_part_boat();
            m.writer().writeShort(part_boat[0]);
            m.writer().writeShort(part_boat[1]);
            m.writer().writeShort(part_boat[2]);
            m.writer().writeShort(part_boat[3]);
            if (cache) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        } else {
            Message m = new Message(-33);
            m.writer().writeByte(0);
            m.writer().writeShort(p.rms[0].length);
            if (p.rms[0].length > 0) {
                m.writer().write(p.rms[0]);
            }
            if (cache) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        }
    }

    public int get_index_item_map() {
        for (int i = 0; i < this.list_it_map.length; i++) {
            if (this.list_it_map[i] == null) {
                return i;
            }
        }
        return -1;
    }

    public void send_weather(Player p) throws IOException {
        // 3 rain, 0 leaf wind, 1 snow, 2
        if (p.map.template.id_eff_map == -1) {
            Message m = new Message(-47);
            m.writer().writeByte(Map.weather);
            m.writer().writeByte(Map.weather_level);
            p.conn.addmsg(m);
            m.cleanup();
        }
    }

    public Mob get_mobs(int id, int type) {
        switch (type) {
            case 0: {
                if (this.map_little_garden != null) {
                    for (int i = 0; i < this.map_little_garden.mobs.size(); i++) {
                        if (this.map_little_garden.mobs.get(i).index == id) {
                            return this.map_little_garden.mobs.get(i);
                        }
                    }
                }
                break;
            }
        }
        return null;
    }
}
