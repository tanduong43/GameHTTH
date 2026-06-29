package map;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import activities.*;
import client.*;
import core.*;
import io.Message;
import template.*;
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
    private boolean running;
    public Thread mythread;
    public List<Player> players = new ArrayList<>();
    public int[] list_mob;
    public byte zone_id;
    public Map_pvp map_pvp;
    public Dungeon map_dungeon;
    public Map_clan_resource clan_resource;
    public Map_Little_Garden map_little_garden;
    public ItemMap[] list_it_map = new ItemMap[1_000];
    public boolean can_PK = true;

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
        return id >= 167 && id <= 176;
    }

    public static void add_map_plus(Map map_boss) {
        synchronized (Map.MAP_PLUS) {
            Map.MAP_PLUS.add(map_boss);
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
        update_map_ThuThachVeThan();
        update_map_Wanted();
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
                short[] mapID = new short[]{120, 122, 123};
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
                map_create.map_pvp.time_pvp = 5;
                map_create.map_pvp.status_pvp = 0;
                map_create.map_pvp.num_win_p1 = 0;
                map_create.map_pvp.num_win_p2 = 0;
                map_create.map_pvp.type_map = 2; // map fight truy na
                map_create.start_map();
                Map.add_map_plus(map_create);
                // System.out.println("map: " + map_create.hashCode());
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
                                    int percent
                                            = (l.skill_point.get(i).temp.indexSkillInServer - 661)
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
                int xp_receiv1 = 400;
                int xp_receiv2 = 400;
                int rb_receiv1 = 150;
                int rb_receiv2 = 150;
                if (this.map_little_garden.hp_1 <= 0) {
                    xp_receiv2 = 1000;
                    xp_receiv1 = 300;
                    rb_receiv2 = 200;
                    rb_receiv1 = 100;
                } else if (this.map_little_garden.hp_2 <= 0) {
                    xp_receiv1 = 1000;
                    xp_receiv2 = 300;
                    rb_receiv1 = 200;
                    rb_receiv2 = 100;
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
                                    && !list_pvp_wait.get(i).pvp_target.equals(p_select)) {
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
            if (this.map_pvp.status_pvp == 3 && players.size() == 2) {
                for (int i = 0; i < players.size(); i++) {
                    if (players.get(i).isdie) {
                        this.map_pvp.status_pvp = 91;
                        break;
                    }
                }
            } else if (this.map_pvp.status_pvp == 91 && players.size() == 2) {
                this.map_pvp.status_pvp = 90;
            } else if (this.map_pvp.status_pvp == 90 && players.size() == 2) {
                for (int i = 0; i < players.size(); i++) {
                    if (this.map_pvp.num_win_p1 == 3 || this.map_pvp.num_win_p2 == 3) {
                        change_flag(players.get(i), -1);
                    }
                    // if (players.get(i).isdie) {
                    players.get(i).isdie = false;
                    Service.use_potion(players.get(i), 0, players.get(i).body.get_hp_max(true));
                    Service.use_potion(players.get(i), 1, players.get(i).body.get_mp_max(true));
                    // }
                }
                this.map_pvp.status_pvp = 3;
            } else if (this.map_pvp.status_pvp == 90) {
                this.map_pvp.status_pvp = 3;
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
                            players.get(1).pvp_lose++;
                            //
                            int chenhLech
                                    = players.get(1).get_pvpPoint() - players.get(0).get_pvpPoint();
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
                            players.get(0).pvp_lose++;
                            //
                            int chenhLech
                                    = players.get(0).get_pvpPoint() - players.get(1).get_pvpPoint();
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
                            long beri_win
                                    = (10_000L + (long) players.get(1).get_wanted_point()) / 100L;
                            long beri_lose
                                    = (5_000L + (long) players.get(1).get_wanted_point()) / 100L;
                            players.get(0).update_wanted_point((int) beri_win);
                            players.get(1).update_wanted_point((int) -beri_lose);
                            //
                            Wanted_Chest.receiv_ruong(players.get(0));
                        } else {
                            Pvp.pvp_notice(players.get(1), 3);
                            Pvp.pvp_notice(players.get(0), 4);
                            //
                            long beri_win
                                    = (10_000L + (long) players.get(0).get_wanted_point()) / 100L;
                            long beri_lose
                                    = (5_000L + (long) players.get(0).get_wanted_point()) / 100L;
                            players.get(1).update_wanted_point((int) beri_win);
                            players.get(0).update_wanted_point((int) -beri_lose);
                            //
                            Wanted_Chest.receiv_ruong(players.get(1));
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
                                p1.update_pvpPoint(15);
                                p2.pvp_lose++;
                                p2.update_pvpPoint(-15);
                            } else if (this.map_pvp.num_win_p1 < this.map_pvp.num_win_p2) {
                                p1.pvp_lose++;
                                p1.update_pvpPoint(-15);
                                p2.pvp_win++;
                                p2.update_pvpPoint(15);
                            }
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
                    } else {
                        Service.send_box_ThongBao_OK(players.get(i),
                                "Đối thủ xứng tầm không thể phân biệt thắng thua");
                    }
                }
            } else if (this.map_pvp.status_pvp == 4 && this.map_pvp.time_pvp <= 0) {
                Vgo vgo = new Vgo();
                if (this.map_pvp.type_map == 0) { // la map pvp
                    vgo.map_go = Map.get_map_by_id(1000);
                } else if (this.map_pvp.type_map == 2) { // la map truy na
                    vgo.map_go = Map.get_map_by_id(119);
                } else {
                    vgo.map_go = Map.get_map_by_id(1);
                }
                vgo.xnew = (short) (vgo.map_go[0].template.maxW / 2);
                vgo.ynew = (short) (vgo.map_go[0].template.maxH / 2);
                List<Player> playerList = new ArrayList<>();
                for (int i = 0; i < players.size(); i++) {
                    playerList.add(players.get(i));
                }
                playerList.forEach(l -> {
                    try {
                        l.targetFight = null;
                        change_flag(l, -1);
                        l.goto_map(vgo);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                });
                this.map_pvp.status_pvp = 99;
            } else if (this.map_pvp.status_pvp == 99) {
                running = false;
                this.map_pvp = null;
            }
        }
    }

    private void update_map_dungeon() throws IOException {
        if (Map.is_map_dungeon(this.template.id)) { // map dungeon
            Player p_select = null;
            boolean ok_out_map = false;
            if (players.size() > 0) {
                Player p0 = players.get(0);
                int num_mob = 0;
                for (int i = 0; i < p0.dungeon.mobs.size(); i++) {
                    if (p0.dungeon.mobs.get(i).map.equals(this)) {
                        num_mob++;
                    }
                }
                if (num_mob == 0 && p0.dungeon.time > System.currentTimeMillis()) {
                    if (this.template.id == 175) {
                        p0.dungeon.time = System.currentTimeMillis() + 10_000L;
                        Service.send_time_cool_down(p0, p0.dungeon.time, "Thời gian", 2);
                    }
                    if (!this.map_dungeon.checkG.contains(this.template.id)) {
                        this.map_dungeon.checkG.add(this.template.id);
                        //
                        byte mode_dungeon = p0.dungeon.mode;
                        List<GiftBox> list_gift = new ArrayList<>();
                        int beri_receiv = Util.random(4_000, 8_000) * (mode_dungeon + 1);
                        ItemTemplate4 it_temp4;
                        if (mode_dungeon == 11 && 25 > Util.random(150)) {
                            GiftBox gb_beri = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(0);
                            if (it_temp4 != null) {
                                gb_beri.id = it_temp4.id;
                                gb_beri.type = 4;
                                gb_beri.name = it_temp4.name;
                                gb_beri.icon = it_temp4.icon;
                                gb_beri.num = beri_receiv * 2;
                                gb_beri.color = 0;
                                list_gift.add(gb_beri);
                            }
                            //
                            GiftBox gb_beri2 = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(366);
                            if (it_temp4 != null) {
                                gb_beri2.id = it_temp4.id;
                                gb_beri2.type = 4;
                                gb_beri2.name = it_temp4.name;
                                gb_beri2.icon = it_temp4.icon;
                                gb_beri2.num = 1;
                                gb_beri2.color = 0;
                                list_gift.add(gb_beri2);
                            }
                        } else {
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
                        }
                        //
                        GiftBox gb_botvang = new GiftBox();
                        ItemTemplate7 it_temp7 = ItemTemplate7.get_it_by_id(4);
                        if (it_temp7 != null) {
                            gb_botvang.id = it_temp7.id;
                            gb_botvang.type = 7;
                            gb_botvang.name = it_temp7.name;
                            gb_botvang.icon = it_temp7.icon;
                            gb_botvang.num = Util.random(1, (mode_dungeon + 2));
                            gb_botvang.color = 0;
                            list_gift.add(gb_botvang);
                        }
                        //
                        if (15 > Util.random(300 - mode_dungeon * 10)) { // da ho phach
                            GiftBox gb_ = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(
                                    (45 > Util.random(120)) ? ((15 > Util.random(120)) ? 364 : 363)
                                    : 362);
                            if (it_temp4 != null) {
                                gb_.id = it_temp4.id;
                                gb_.type = 4;
                                gb_.name = it_temp4.name;
                                gb_.icon = it_temp4.icon;
                                gb_.num = Util.random(1, 3);
                                gb_.color = 0;
                                list_gift.add(gb_);
                            }
                        }
                        if (80 > Util.random(150 - mode_dungeon * 10)) { // ruong huyen bi
                            GiftBox gb_ = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(18 + (p0.level / 10));
                            if (it_temp4 != null) {
                                gb_.id = it_temp4.id;
                                gb_.type = 4;
                                gb_.name = it_temp4.name;
                                gb_.icon = it_temp4.icon;
                                int num = Util.random(1, (mode_dungeon + 2));
                                gb_.num = (num < 2) ? num : (num / 2);
                                gb_.color = 0;
                                list_gift.add(gb_);
                            }
                        }
                        Service.send_gift(p0, 1, "Ải đơn cấp độ " + (mode_dungeon + 3),
                                "Phần thưởng", list_gift, true);
                    }
                }
                if (p0.dungeon.time < System.currentTimeMillis()) {
                    ok_out_map = true;
                    p_select = p0;
                }
            }
            if (ok_out_map && p_select != null && p_select.conn != null) {
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
                                    dame_to_target
                                            = (dame_to_target * (100 - Util.random(10))) / 100;
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
                                    Mob mob_select
                                            = list_random.get(Util.random(list_random.size()));
                                    Message m = new Message(-15);
                                    m.writer().writeByte(3);
                                    m.writer().writeShort(mob_select.index);
                                    m.writer().writeByte(1);
                                    m.writer().writeShort(0);
                                    send_msg_all_p(m, p0, true);
                                    m.cleanup();
                                    //
                                    int dame_to_target = p0.body.get_dame(true);
                                    dame_to_target
                                            = (dame_to_target * (100 - Util.random(10))) / 100;
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
        if (Map.is_map_dungeon(this.template.id)) {
            List<Mob> list_remove = new ArrayList<>();
            List<Mob> get_list_Mob = new ArrayList<>();
            if (players.size() > 0) {
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
    }

    private synchronized void mob_fire(Mob mob, int id_target) throws IOException {
        if (!mob.isdie && mob.time_skill < System.currentTimeMillis()) {
            mob.time_skill = System.currentTimeMillis() + 1800L;
            Player p0 = this.get_player_by_id_inmap(id_target);
            if (p0 != null) {
                if (!mob.isdie && !p0.wait_change_map && !p0.isdie
                        && p0.time_can_mob_atk < System.currentTimeMillis()) {
                    //
                    int dame = Util.random(mob.level * 2, mob.level * 5);
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

                    // update hp target
                    if (p0.hp == p0.body.get_hp_max(true) && dame >= p0.hp) {
                        p0.hp = 1;
                    } else {
                        p0.hp -= dame;
                    }
                    long def = p0.body.get_def(true);
                    def = (def * (1000L + (long) p0.body.get_def_percent(true))) / 10_000L;
                    dame -= def;
                    //miss enemy 
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
                    m.writer().writeShort(
                    mob.mob_template.skill[Util.random(mob.mob_template.skill.length)]);
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
                Pvp.show_info(p, this.map_pvp.time_pvp, this.map_pvp.num_win_p1,
                        this.map_pvp.num_win_p2, 3);
                Pvp.show_info(p0, this.map_pvp.time_pvp, this.map_pvp.num_win_p2,
                        this.map_pvp.num_win_p1, 3);
            } else {
                this.map_pvp.num_win_p1++;
                Pvp.show_info(p, this.map_pvp.time_pvp, this.map_pvp.num_win_p2,
                        this.map_pvp.num_win_p1, 3);
                Pvp.show_info(p0, this.map_pvp.time_pvp, this.map_pvp.num_win_p1,
                        this.map_pvp.num_win_p2, 3);
            }
        }
    }

    public void enter_map(Player p) {
        synchronized (this) {
            players.add(p);
        }
    }

    public void leave_map(Player p, int type) {
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
        try {
            Message m = new Message(3);
            m.writer().writeShort(p.index_map);
            // 2: next map, 1: tele, 0: exit game
            m.writer().writeByte(type);
            for (int i = 0; i < players.size(); i++) {
                Player p0 = players.get(i);
                p0.conn.addmsg(m);
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
                    p0.conn.addmsg(m);
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
            p.time_sk[sk_temp.temp.ID] = System.currentTimeMillis() + sk_temp.temp.timeDelay -((sk_temp.temp.timeDelay * p.body.get_agility(true)) / 1_000);
            if ((p.mp - sk_temp.temp.manaLost) < 0) {
                Service.send_box_ThongBao_OK(p, "MP không đủ!");
                return;
            }
            
            int hp_= p.body.get_hp_atk_absorb(true);
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
                    dame_inf.dameM
                            = (p.get_skill_temp(idSkill).get_dame(p) * (dame_magic_plus_percent))
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
                int react_dame_
                        = p_target.body.get_dame_react(true) - p.body.get_dame_react_reduce();
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
        long[] exp_up = new long[]{0, 0};
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
                    dame_inf.dameM
                            = (p.get_skill_temp(idSkill).get_dame(p) * (dame_magic_plus_percent))
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
                    if (mob_target.boss_info != null) {
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
                        beri_receiv
                                = (beri_receiv / 100) * (100 + mob_target.boss_info.levelBoss * 10);
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
                        beri_receiv
                                = (beri_receiv / 100) * (100 + mob_target.boss_info.levelBoss * 10);
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
                    if (Map.is_map_dungeon(this.template.id) && p.dungeon != null) {
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
                    if (mob_target.boss_info != null) {
                        String notice = "Tiêu diệt siêu trùm nhận: ";
                        this.remove_obj(mob_target.index, 1);
                        Manager.gI().chatKTG(0,
                                p.name + " đã tiêu diệt " + mob_target.mob_template.name + " bậc "
                                + mob_target.boss_info.levelBoss,
                                5);
                        //
                        List<GiftBox> list_gift = new ArrayList<>();
                        //
                        if (50 > Util.random(120)) {
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
                            notice += "x1 rương cam cùng cấp, ";
                        }
                        if (30 > Util.random(120)) {
                            GiftBox gb_rcam = new GiftBox();
                            ItemTemplate7 it_temp7_in = ItemTemplate7.get_it_by_id(10);
                            if (it_temp7_in != null) {
                                gb_rcam.id = it_temp7_in.id;
                                gb_rcam.type = 7;
                                gb_rcam.name = it_temp7_in.name;
                                gb_rcam.icon = it_temp7_in.icon;
                                gb_rcam.num = 1;
                                gb_rcam.color = 0;
                                list_gift.add(gb_rcam);
                            }
                            notice += "x1 khiên, ";
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
                        if (mob_target.mob_template.mob_id >= 137
                                && mob_target.mob_template.mob_id <= 140) {
                            if (30 > Util.random(120)) {
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
                                notice += "x1 mảnh đồ tím 9x, ";
                            }
                            if (30 > Util.random(120)) {
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
                                notice += "x1 mảnh đồ cam 9x, ";
                            }
                        }
                        p.update_money();
                        if (list_gift.size() > 0) {
                            Service.send_gift(p, 1, "Hoạt động săn trùm",
                                    "Tiêu diệt siêu trùm bậc " + mob_target.boss_info.levelBoss,
                                    list_gift, false);
                        }
                        Manager.gI().chatKTG(0, notice.substring(0, notice.length() - 1), 5);
                        // boss up level
                        if (mob_target.boss_info.levelBoss < 10) {
                            mob_target.hp = mob_target.hp_max;
                            mob_target.isdie = false;
                            mob_target.boss_info.levelBoss++;
                            mob_target.index++;
                            Message m_local = new Message(1);
                            m_local.writer().writeByte(1);
                            m_local.writer().writeShort(mob_target.index);
                            m_local.writer().writeShort(mob_target.x);
                            m_local.writer().writeShort(mob_target.y);
                            for (int j = 0; j < this.players.size(); j++) {
                                Player p0 = this.players.get(j);
                                p0.conn.addmsg(m_local);
                            }
                            m_local.cleanup();
                            Manager.gI().chatKTG(0, mob_target.mob_template.name + " bậc "
                                    + mob_target.boss_info.levelBoss + " xuất hiện", 5);
                        }
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

//                if (p.level < 10){
//                    exp_up_add = exp_up_add * 5;
//                }
                if (Math.abs(p.level - mob_target.level) >= 10) {
                    exp_up_add = 0;

                }
                if (mob_target.mob_template.mob_id == 4 || mob_target.mob_template.mob_id == 10 || mob_target.mob_template.mob_id == 16 || mob_target.mob_template.mob_id == 23
                        || mob_target.mob_template.mob_id == 29 || mob_target.mob_template.mob_id == 36 || mob_target.mob_template.mob_id == 43 || mob_target.mob_template.mob_id == 68
                        || mob_target.mob_template.mob_id == 78 || mob_target.mob_template.mob_id == 92 || mob_target.mob_template.mob_id == 112 || mob_target.mob_template.mob_id == 163) {

                    exp_up_add = 0;

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
        if (p.conn.user.equals("admin") && s.equals("admin")) {
            MenuController.send_dynamic_menu(p, 9999, "Menu Admin", new String[]{"Bảo trì",
                "1t Beri + 1t Ruby", "Uplevel", "setXP", "get item", "save data", "updateTB"},
                    null);
        } else {
            this.send_chat_popup(0, p.index_map, s);
        }
    }

    private void send_chat_popup(int type, int id_p, String s) throws IOException {
        Message m = new Message(17);
        switch (type) {
            case 0: {
                m.writer().writeShort(id_p);
                m.writer().writeByte(0);
                m.writer().writeUTF(s);
                Player p0 = this.get_player_by_id_inmap(id_p);
                this.send_msg_all_p(m, p0, false);
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
                || Map.is_map_boss(id) || Map.is_map_dungeon(id);
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
                                                    p0.time_hs_little_garden
                                                            = System.currentTimeMillis() + 10_000L;
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
                                                    p0.time_hs_little_garden
                                                            = System.currentTimeMillis() + 10_000L;
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
            m.writer().writeByte(this.template.vgos.size());
            for (int i = 0; i < this.template.vgos.size(); i++) {
                m.writer().writeUTF(this.template.vgos.get(i).map_go[0].template.name);
                m.writer().writeShort(this.template.vgos.get(i).xold);
                m.writer().writeShort(this.template.vgos.get(i).yold);
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
