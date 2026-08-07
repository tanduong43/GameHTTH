package io;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import client.Player;
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

    public synchronized static void client_connect(Session ss) {
        ss.init();
        SessionManager.CLIENT_ENTRYS.add(ss);
        System.out.println("accecpt online: " + SessionManager.CLIENT_ENTRYS.size());
    }

    public synchronized static void client_disconnect(Session ss) {
        if (SessionManager.CLIENT_ENTRYS.contains(ss)) {
            SessionManager.time_login.put(ss.user, System.currentTimeMillis() + 1_800_000L);
            ss.connected = false;
            try {
                if (ss.p != null && ss.p.map != null) {
                    ss.p.map.leave_map(ss.p, 0);
                    if (ss.p.ship_pet != null && ss.p.ship_pet.map == null) {
                        ss.p.ship_pet.map = ss.p.map;
                    }
                    Player.flush(ss.p, true);
                }
                //
                ss.clear_network(ss);
                ss.update_onl(0);
                //
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
