package io;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;

import core.Manager;

/**
 *
 * @author Truongbk
 */
public class SessionManager {
    public static final List<Session> CLIENT_ENTRYS = new LinkedList<>();
    public static HashMap<String, Long> time_login = new HashMap<>();
    public static HashMap<String, Long> CLIENT_LOGIN_TIME = new HashMap<>();
    public final static long TIME_LOGIN_AGAIN = Manager.gI().server_admin ? 0 : 5_000L;

    public synchronized static int getNumOnlinePlayers() {
        int count = 0;
        for (Session ss : CLIENT_ENTRYS) {
            if (ss.p != null) {
                count++;
            }
        }
        return count;
    }

    public synchronized static void client_connect(Session ss) {
        String ip = ss.getIpAddress();
        int count = 0;
        for (Session session : CLIENT_ENTRYS) {
            if (session.getIpAddress().equals(ip)) {
                count++;
            }
        }
        if (!ip.equals("127.0.0.1") && !ip.equals("0:0:0:0:0:0:0:1") && count >= Manager.gI().max_ip_connection) {
            System.out.println("IP " + ip + " reached connection limit (" + Manager.gI().max_ip_connection + "). Disconnected.");
            try {
                ss.getSocket().close();
            } catch (Exception e) {
                // Ignore
            }
            return;
        }
        ss.init();
        SessionManager.CLIENT_ENTRYS.add(ss);
        System.out.println("accecpt online: " + SessionManager.CLIENT_ENTRYS.size());
    }

    public synchronized static void client_disconnect(Session ss) {
        if (SessionManager.CLIENT_ENTRYS.contains(ss)) {
            SessionManager.time_login.put(ss.user, System.currentTimeMillis() + 1_800_000L);
            ss.connected = false;
            try {
                if (ss.p != null) {
                    activities.Wanted.remove_player_wait(ss.p);
                    if (ss.p.map != null) {
                        map.Map originalMap = ss.p.map;

                        if (ss.p.map.map_bossHunt != null && ss.p.bossHunt != null) {
                            System.out.println("[BossHunt] Player " + ss.p.name
                                    + " disconnected from BossHunt floor "
                                    + (ss.p.bossHunt.currentFloor + 1)
                                    + ". Saving position at village (map 1).");
                            map.Map[] villageMap = map.Map.get_map_by_id(1);
                            if (villageMap != null && villageMap.length > 0) {
                                ss.p.map = villageMap[0];
                                ss.p.x = 300;
                                ss.p.y = 250;
                            }
                            // Không cancel cả room — chỉ giữ player trong hunt để họ
                            // được hỏi "rejoin?" ở lần login sau (xem MessageHandler)
                        }

                        originalMap.leave_map(ss.p, 0); // chỉ gọi 1 lần, đúng map gốc
                        if (ss.p.ship_pet != null && ss.p.ship_pet.map == null) {
                            ss.p.ship_pet.map = ss.p.map;
                        }
                        client.Player.flush(ss.p, true);
                    }
                }
                ss.clear_network(ss);
                ss.update_onl(0);
            } catch (Exception e) {
            }
            SessionManager.CLIENT_ENTRYS.remove(ss);
            System.out.println("disconnect session " + ss.user + ": online : "
                    + SessionManager.CLIENT_ENTRYS.size());
            SessionManager.time_login.put(ss.user,
                    System.currentTimeMillis() + SessionManager.TIME_LOGIN_AGAIN);
        }
    }
}
