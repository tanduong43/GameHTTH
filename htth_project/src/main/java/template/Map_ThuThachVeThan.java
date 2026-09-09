package template;

import client.Player;
import java.util.HashSet;
import java.util.Set;

/**
 *
 * @author Truongbk
 */
public class Map_ThuThachVeThan {
    public long time_state;
    public boolean isFinish = false;
    // public int time = 10;
    public boolean isReceiv = false;
    private final Set<Integer> listcheck = new HashSet<>();

    public synchronized boolean okP(Player p) {
        if (p != null) {
            listcheck.add(p.id);
        }
        return listcheck.size() >= 2;
    }

    public synchronized void update_okP() {
        listcheck.clear();
    }
}
