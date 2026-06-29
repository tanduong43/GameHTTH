package template;

import client.Player;
/**
 *
 * @author Truongbk
 */
public class Map_ThuThachVeThan {
    public long time_state;
    public boolean isFinish = false;
    // public int time = 10;
    public boolean isReceiv = false;
    private byte[] listcheck = new byte[] { 0, 0 };

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
        listcheck = new byte[] { 0, 0 };
    }
}
