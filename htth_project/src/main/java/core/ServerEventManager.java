package core;

import activities.*;
import client.Clan;
import client.Player;
import map.Boss;
import map.Map;
import map.Mob;
import map.Vgo;
import org.joda.time.LocalTime;
import template.Map_Little_Garden;
import template.Map_Pvp_Clan;
import io.Message;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
/**
 *
 * @author Truongbk
 */
public class ServerEventManager {
    private Thread thread_cal_time;
    private Thread thread_save_data;
    private boolean running;
    private String lastBossStatus = "";

    public ServerEventManager() {
        this.running = false;
    }

    public void close() {
        running = false;
        this.thread_save_data.interrupt();
    }

    public void init() {
        //
        this.running = true;
        this.thread_cal_time = new Thread(() -> {
            LocalTime now;
            int hour, min, sec, millis;
            while (this.running) {
                try {
                    now = LocalTime.now();
                    hour = now.getHourOfDay();
                    min = now.getMinuteOfHour();
                    sec = now.getSecondOfMinute();
                    millis = now.getMillisOfSecond();
                    //
                    if (hour == 0 && min == 0 && sec == 0) {
                        for (Map[] map_all : Map.ENTRYS) {
                            for (Map map : map_all) {
                                for (int i = 0; i < map.players.size(); i++) {
                                    map.players.get(i).change_new_date();
                                }
                            }
                        }
                        List<Map> mapplus = Map.get_map_plus();
                        for (int i = 0; i < mapplus.size(); i++) {
                            for (int i12 = 0; i12 < mapplus.get(i).players.size(); i12++) {
                                Player p0 = mapplus.get(i).players.get(i12);
                                p0.change_new_date();
                            }
                        }
                        
                        Clan.reset_day();
                        LittleGarden.LIST.clear();
                    }
                    if (min % 5 == 0 && sec == 0) {
                        Boss.spawn_event_boss();
                    }
                    if (min == 0 && sec == 0) {
                        Boss.spawnDualEventBosses();
                    }
                    Boss.checkDualEventDespawn();
                    if ((hour == 18 || hour == 22) && sec == 0) {
                        // Kiểm tra xem Boss đã được gọi ra chưa (phòng trường hợp restart server lúc 18h)
                        boolean isMissed = true;
                        for (byte b : Boss.BOSS_LIVE) {
                            if (b != 0) {
                                isMissed = false;
                                break;
                            }
                        }
                        if (isMissed) {
                            Boss.create_boss();
                        }
                    }
                    if ((hour == 19 || hour == 23) && min == 0 && sec == 0) {
                        Boss.result_boss();
                    }
                    if (sec == 0) {
                        try {
                            StringBuilder sbBoss = new StringBuilder();
                            for (int i = 0; i < Boss.ENTRYS.size(); i++) {
                                Boss temp = Boss.ENTRYS.get(i);
                                if (temp.mob != null && !temp.mob.isdie) {
                                    sbBoss.append("- ").append(temp.mob.mob_template.name)
                                            .append(" xuất hiện tại: ").append(temp.mob.map.template.name)
                                            .append(" (Khu ").append(temp.mob.map.zone_id + 1).append(")\n");
                                }
                            }
                            String currentBossStatus = sbBoss.toString();
                            if (!currentBossStatus.equals(lastBossStatus)) {
                                lastBossStatus = currentBossStatus;
                                Message m5 = new Message(18);
                                m5.writer().writeUTF("Boss đang sống");
                                if (currentBossStatus.length() > 0) {
                                    m5.writer().writeUTF(currentBossStatus);
                                } else {
                                    m5.writer().writeUTF("Hiện tại không có boss nào đang sống.");
                                }
                                for (Map[] mapall : Map.ENTRYS) {
                                    for (Map map : mapall) {
                                        for (int i = 0; i < map.players.size(); i++) {
                                            Player p0 = map.players.get(i);
                                            if (p0.conn != null) {
                                                p0.conn.addmsg(m5);
                                            }
                                        }
                                    }
                                }
                                List<Map> mapplus = Map.get_map_plus();
                                for (int i = 0; i < mapplus.size(); i++) {
                                    for (int i12 = 0; i12 < mapplus.get(i).players.size(); i12++) {
                                        Player p0 = mapplus.get(i).players.get(i12);
                                        if (p0.conn != null) {
                                            p0.conn.addmsg(m5);
                                        }
                                    }
                                }
                                m5.cleanup();
                            }
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                    if (sec % 1 == 0) { // update eff player
                        for (Map[] mapall : Map.ENTRYS) {
                            for (Map map : mapall) {
                                for (int i = 0; i < map.players.size(); i++) {
                                    Player p0 = map.players.get(i);
                                    try {
                                        p0.update_eff();
                                    } catch (Exception e) {
                                    }
                                }
                            }
                        }
                        List<Map> mapplus = Map.get_map_plus();
                        for (int i = 0; i < mapplus.size(); i++) {
                            for (int i12 = 0; i12 < mapplus.get(i).players.size(); i12++) {
                                Player p0 = mapplus.get(i).players.get(i12);
                                try {
                                    p0.update_eff();
                                } catch (Exception e) {
                                }
                            }
                        }
                        Manager.gI().TaiXiu().upTime();
                        Boss.update_bosses();
                    }
                    if (sec % 5 == 0) { // fine clan little garden
                        if ((Util.is_DayofWeek(2) || Util.is_DayofWeek(4) || Util.is_DayofWeek(6))
                                && hour == 21) {
                            if (LittleGarden.LIST.size() > 1) {
                                int index = Util.random(LittleGarden.LIST.size());
                                Clan clan1 = LittleGarden.LIST.get(index);
                                LittleGarden.remove_clan_wait(clan1);
                                index = Util.random(LittleGarden.LIST.size());
                                Clan clan2 = LittleGarden.LIST.get(index);
                                LittleGarden.remove_clan_wait(clan2);
                                //
                                Player p1 = null;
                                Player p2 = null;
                                for (int i = 0; i < clan1.members.size(); i++) {
                                    if (clan1.members.get(i).levelInclan == 0
                                            || clan1.members.get(i).levelInclan == 1) {
                                        Player p0 = Map.get_player_by_name_allmap(
                                                clan1.members.get(i).name);
                                        if (p0 != null) {
                                            p1 = p0;
                                            break;
                                        }
                                    }
                                }
                                for (int i = 0; i < clan2.members.size(); i++) {
                                    if (clan2.members.get(i).levelInclan == 0
                                            || clan2.members.get(i).levelInclan == 1) {
                                        Player p0 = Map.get_player_by_name_allmap(
                                                clan2.members.get(i).name);
                                        if (p0 != null) {
                                            p2 = p0;
                                            break;
                                        }
                                    }
                                }
                                if (p1 != null && p2 != null && p1.name.equals(p2.name)) {
                                    LittleGarden.add_clan_wait(clan1);
                                } else if (p1 != null && p2 != null) {
                                    //
                                    int index_mob = -2;
                                    Map mapTemplate = Map.get_map_by_id(81)[0];
                                    Map map_dungeon = new Map();
                                    map_dungeon.template = mapTemplate.template;
                                    map_dungeon.zone_id = (byte) 0;
                                    map_dungeon.list_mob = new int[0];
                                    map_dungeon.map_little_garden = new Map_Little_Garden();
                                    map_dungeon.map_little_garden.mobs = new ArrayList<>();
                                    map_dungeon.map_little_garden.time =
                                            System.currentTimeMillis() + 60_000L * 15;
                                    map_dungeon.map_little_garden.clan1 = clan1;
                                    map_dungeon.map_little_garden.clan2 = clan2;
                                    //
                                    clan1.map_create = map_dungeon;
                                    clan2.map_create = map_dungeon;
                                    //
                                    for (int i = 0; i < mapTemplate.list_mob.length; i++) {
                                        Mob temp = Mob.ENTRYS.get(mapTemplate.list_mob[i]);
                                        Mob mob_add = new Mob();
                                        mob_add.mob_template = temp.mob_template;
                                        mob_add.x = temp.x;
                                        mob_add.y = temp.y;
                                        mob_add.hp_max = temp.mob_template.hp_max;
                                        mob_add.hp = mob_add.hp_max;
                                        mob_add.level = 75;
                                        mob_add.isdie = false;
                                        mob_add.id_target = -1;
                                        mob_add.index = index_mob--;
                                        mob_add.map = map_dungeon;
                                        mob_add.boss_info = null;
                                        map_dungeon.map_little_garden.mobs.add(mob_add);
                                    }
                                    map_dungeon.start_map();
                                    //
                                    Map.add_map_plus(map_dungeon);
                                    //
                                    Vgo vgo = new Vgo();
                                    vgo.map_go = new Map[1];
                                    vgo.map_go[0] = map_dungeon;
                                    vgo.xnew = 350;
                                    vgo.ynew = 260;
                                    List<Player> list_remove_table_tick = new ArrayList<>();
                                    for (int i = 0; i < p1.tableTickOption.listP.size(); i++) {
                                        Player p0 = p1.tableTickOption.listP.get(i);
                                        if (p0 != null && p0.conn != null
                                                && p1.tableTickOption.list_check[i] == 1) {
                                            list_remove_table_tick.add(p0);
                                            p0.type_pk = 4;
                                            p0.goto_map(vgo);
                                        }
                                    }
                                    for (int i = 0; i < p2.tableTickOption.listP.size(); i++) {
                                        Player p0 = p2.tableTickOption.listP.get(i);
                                        if (p0 != null && p0.conn != null
                                                && p2.tableTickOption.list_check[i] == 1) {
                                            list_remove_table_tick.add(p0);
                                            p0.type_pk = 5;
                                            p0.goto_map(vgo);
                                        }
                                    }
                                    list_remove_table_tick.forEach(l -> l.tableTickOption = null);
                                } else if (p1 != null) {
                                    LittleGarden.add_clan_wait(clan1);
                                } else if (p2 != null) {
                                    LittleGarden.add_clan_wait(clan2);
                                }
                            }
                        }
                        // PVP Băng Matching
                        if (PvpClan.LIST.size() > 1) {
                            int index = Util.random(PvpClan.LIST.size());
                            Clan clan1 = PvpClan.LIST.get(index);
                            PvpClan.remove_clan_wait(clan1);
                            index = Util.random(PvpClan.LIST.size());
                            Clan clan2 = PvpClan.LIST.get(index);
                            PvpClan.remove_clan_wait(clan2);

                            Player p1 = null;
                            Player p2 = null;
                            for (int i = 0; i < clan1.members.size(); i++) {
                                if (clan1.members.get(i).levelInclan == 0
                                        || clan1.members.get(i).levelInclan == 1) {
                                    Player p0 = Map.get_player_by_name_allmap(clan1.members.get(i).name);
                                    if (p0 != null) {
                                        p1 = p0;
                                        break;
                                    }
                                }
                            }
                            for (int i = 0; i < clan2.members.size(); i++) {
                                if (clan2.members.get(i).levelInclan == 0
                                        || clan2.members.get(i).levelInclan == 1) {
                                    Player p0 = Map.get_player_by_name_allmap(clan2.members.get(i).name);
                                    if (p0 != null) {
                                        p2 = p0;
                                        break;
                                    }
                                }
                            }
                            if (p1 != null && p2 != null && p1.name.equals(p2.name)) {
                                PvpClan.add_clan_wait(clan1);
                            } else if (p1 != null && p2 != null) {
                                Map mapTemplate = Map.get_map_by_id(1000)[0];
                                Map map_dungeon = new Map();
                                map_dungeon.template = mapTemplate.template;
                                map_dungeon.zone_id = (byte) 0;
                                map_dungeon.list_mob = new int[0];
                                map_dungeon.map_pvp_clan = new Map_Pvp_Clan();
                                map_dungeon.map_pvp_clan.time_end = System.currentTimeMillis() + 60_000L * 5;
                                map_dungeon.map_pvp_clan.clan1 = clan1;
                                map_dungeon.map_pvp_clan.clan2 = clan2;

                                clan1.map_create = map_dungeon;
                                clan2.map_create = map_dungeon;
                                map_dungeon.start_map();
                                Map.add_map_plus(map_dungeon);

                                Message m38 = new Message(38);
                                try {
                                    m38.writer().writeByte(1);
                                    m38.writer().writeByte(2);
                                    m38.writer().writeUTF(clan1.name);
                                    m38.writer().writeUTF(clan2.name);
                                    m38.writer().writeByte(2);
                                } catch (IOException e) {
                                    e.printStackTrace();
                                }

                                Vgo vgo1 = new Vgo();
                                vgo1.map_go = new Map[1];
                                vgo1.map_go[0] = map_dungeon;
                                vgo1.xnew = (short) Util.random(180, 240);
                                vgo1.ynew = (short) Util.random(200, 260);

                                Vgo vgo2 = new Vgo();
                                vgo2.map_go = new Map[1];
                                vgo2.map_go[0] = map_dungeon;
                                vgo2.xnew = (short) Util.random(400, 480);
                                vgo2.ynew = (short) Util.random(200, 260);

                                List<Player> list_remove_table_tick = new ArrayList<>();
                                if (p1.tableTickOption != null && p1.tableTickOption.listP != null) {
                                    for (int i = 0; i < p1.tableTickOption.listP.size(); i++) {
                                        Player p0 = p1.tableTickOption.listP.get(i);
                                        if (p0 != null && p0.conn != null && p1.tableTickOption.list_check[i] == 1) {
                                            list_remove_table_tick.add(p0);
                                            p0.type_pk = 4;
                                            try { p0.conn.addmsg(m38); } catch (Exception e) {}
                                            try { p0.goto_map(vgo1); } catch (Exception e) {}
                                        }
                                    }
                                }
                                if (p2.tableTickOption != null && p2.tableTickOption.listP != null) {
                                    for (int i = 0; i < p2.tableTickOption.listP.size(); i++) {
                                        Player p0 = p2.tableTickOption.listP.get(i);
                                        if (p0 != null && p0.conn != null && p2.tableTickOption.list_check[i] == 1) {
                                            list_remove_table_tick.add(p0);
                                            p0.type_pk = 5;
                                            try { p0.conn.addmsg(m38); } catch (Exception e) {}
                                            try { p0.goto_map(vgo2); } catch (Exception e) {}
                                        }
                                    }
                                }
                                list_remove_table_tick.forEach(l -> l.tableTickOption = null);
                                m38.cleanup();
                            } else if (p1 != null) {
                                PvpClan.add_clan_wait(clan1);
                            } else if (p2 != null) {
                                PvpClan.add_clan_wait(clan2);
                            }
                        }
                    }
                    //
                    long time_sleep = 1000 - millis;
                    if (time_sleep > 0) {
                        if (time_sleep < 100) {
                            System.err.println("server time update process is overloading...");
                        }
                        // System.out.println(time_sleep);
                        Thread.sleep(time_sleep);
                    }
                } catch (InterruptedException e) {
                } catch (Exception e) {
                    e.printStackTrace();
                    System.out.println("exception at server update rigth time " + e.getMessage());
                }
            }
        });
        this.thread_cal_time.start();
        //
        this.thread_save_data = new Thread(() -> {
            while (this.running) {
                try {
                    Thread.sleep(50_000L);
                    SaveData.process();
                } catch (Exception e) {
                    e.printStackTrace();
                    System.err.println("err thread save data");
                }
            }
        });
        this.thread_save_data.start();
    }
}
