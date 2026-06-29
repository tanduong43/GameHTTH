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
                    if (hour == 18 && min == 0 && sec == 0) {
                        Boss.create_boss();
                    }
                    if (hour == 19 && min == 0 && sec == 0) {
                        Boss.result_boss();
                    }
                    if (hour == 22 && min == 0 && sec == 0) {
                        Boss.create_boss();
                    }
                    if (hour == 23 && min == 0 && sec == 0) {
                        Boss.result_boss();
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
