package template;

import client.Player;
import java.util.HashMap;
import java.util.List;
import map.Mob;

public class Map_LienTang {

    public long time_state;
    public boolean isFinish = false;
    // public int time = 10;
    public boolean isReceiv = false;
    private byte[] listcheck = new byte[]{0, 0};
    public int type;
    public List<Mob> mobs;
    public HashMap<Mob, Long> listRemoveMap;
    public boolean gifted;
    public int levelMob;
    public int hpScale;

    public boolean okP(Player p) {
        if (listcheck[0] == 0) {
            listcheck[0] = 1;
        } else {
            listcheck[1] = 1;
        }
        if (listcheck[0] == 1 && listcheck[1] == 1) {
            return true;
        } else {
            return false;
        }
    }

    public void update_okP() {
        listcheck = new byte[]{0, 0};
    }
}
