package activities;

import client.Clan;
import java.util.ArrayList;
import java.util.List;

public class PvpClan {
    public static List<Clan> LIST = new ArrayList<>();

    public synchronized static void add_clan_wait(Clan clan) {
        if (!LIST.contains(clan)) {
            LIST.add(clan);
        }
    }

    public synchronized static void remove_clan_wait(Clan clan) {
        LIST.remove(clan);
    }
}
