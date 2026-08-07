package core;

import activities.Market;
import client.Clan;
import client.Player;
import map.Map;
import template.ShopTichLuy;
/**
 *
 * @author Truongbk
 */
public class SaveData {
    public static void process() {
        if (Manager.gI().server_admin) {
            return;
        }
        long t = System.currentTimeMillis();
        // save data player
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                for (int i = 0; i < map.players.size(); i++) {
                    try {
                        Player p0 = map.players.get(i);
                        Player.flush(p0, false);
                    } catch (Exception e) {
                    }
                }
            }
        }
        for (int i = 0; i < Map.get_map_plus().size(); i++) {
            for (int i1 = 0; i1 < Map.get_map_plus().get(i).players.size(); i1++) {
                try {
                    Player p0 = Map.get_map_plus().get(i).players.get(i1);
                    Player.flush(p0, false);
                } catch (Exception e) {
                }
            }
        }
        // update BXH
        BXH.update();
        // update Market
        Market.update();
        // clan update
        Clan.update();
        ShopTichLuy.update();
       
        System.out.println("SAVE DATA OK " + (System.currentTimeMillis() - t));
    }
}
