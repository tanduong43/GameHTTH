package event;

import client.Player;
import core.Service;
import core.Util;
import template.TaiXiuInfo;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
/**
 *
 * @author Truongbk
 */
public class TaiXiu implements Runnable {
    // xiu == 0, tai == 1
    public static final int TIME_ROUND = 180_999;
    private boolean running;
    private Thread myth;
    private long time;
    private HashMap<String, TaiXiuInfo> list_player;
    private HashMap<String, TaiXiuInfo> list_result;
    private int XiuTotal;
    private int TaiTotal;
    private byte[] dice;

    public TaiXiu() {
        time = TIME_ROUND;
        TaiTotal = 0;
        XiuTotal = 0;
        dice = new byte[3];
        for (int i = 0; i < 3; i++) {
            dice[i] = (byte) Util.random(1, 7);
        }
        list_player = new HashMap<>();
        list_result = new HashMap<>();
        this.myth = new Thread(this);
        this.myth.start();
    }

    @Override
    public void run() {
        this.running = true;
        while (this.running) {
            try {
                update();
                Thread.sleep(1000);
            } catch (InterruptedException e) {
            } catch (Exception e) {
                e.printStackTrace();
                System.err.println("err update taixiu");
            }
        }
    }

    private synchronized void update() {
        if (this.time < 0) {
            for (int i = 0; i < 3; i++) {
                dice[i] = (byte) Util.random(1, 7);
            }
            int xucxacResult = dice[0] + dice[1] + dice[2];
            while (xucxacResult == 3 || xucxacResult == 18) {
                for (int i = 0; i < 3; i++) {
                    dice[i] = (byte) Util.random(1, 7);
                }
                xucxacResult = dice[0] + dice[1] + dice[2];
            }
            for (Map.Entry<String, TaiXiuInfo> en : this.list_player.entrySet()) {
                TaiXiuInfo infoJoin = en.getValue();
                if (xucxacResult >= 11 && xucxacResult <= 17) { // tai
                    if (en.getValue().TaiorXiu == 1) {
                        infoJoin.money *= 2;
                        String nameP = en.getKey();
                        if (!this.list_result.containsKey(nameP)) {
                            this.list_result.put(nameP, infoJoin);
                        } else {
                            TaiXiuInfo infoJoin_old = this.list_result.get(nameP);
                            infoJoin_old.money += infoJoin.money;
                        }
                    }
                } else {
                    if (en.getValue().TaiorXiu == 0) {
                        infoJoin.money *= 2;
                        String nameP = en.getKey();
                        if (!this.list_result.containsKey(nameP)) {
                            this.list_result.put(nameP, infoJoin);
                        } else {
                            TaiXiuInfo infoJoin_old = this.list_result.get(nameP);
                            infoJoin_old.money += infoJoin.money;
                        }
                    }
                }
            }
            this.time = TIME_ROUND;
            TaiTotal = 0;
            XiuTotal = 0;
            this.list_player.clear();
        }
    }

    public void close() {
        this.running = false;
        this.myth.interrupt();
    }

    public long get_time() {
        return this.time;
    }

    public int MoneyTotal(int i) {
        if (i == 0) {
            return this.XiuTotal;
        } else {
            return this.TaiTotal;
        }
    }

    public synchronized TaiXiuInfo get_my_info(Player p) {
        return this.list_player.get(p.name);
    }

    public synchronized TaiXiuInfo get_my_result(Player p) {
        return this.list_result.get(p.name);
    }

    public byte[] get_dice_now() {
        return this.dice;
    }

    public synchronized void register(Player p, int money, byte taiorXiu) throws IOException {
        if (this.time <= 0) {
            Service.send_box_ThongBao_OK(p, "Không trong thời gian đặt cược!");
            return;
        }
        if (taiorXiu == 0) {
            if (((long) XiuTotal + (long) money) > 2_000_000_000L) {
                money = 2_000_000_000 - XiuTotal;
                XiuTotal = 2_000_000_000;
            }
            XiuTotal += money;
        } else {
            if (((long) TaiTotal + (long) money) > 2_000_000_000L) {
                money = 2_000_000_000 - TaiTotal;
                TaiTotal = 2_000_000_000;
            }
            TaiTotal += money;
        }
        p.update_vang(-money);
        p.update_money();
        TaiXiuInfo t = this.list_player.get(p.name);
        if (t != null) {
            if (t.TaiorXiu == taiorXiu) {
                t.money += money;
                EventSpecial.update_info_tx(p);
            }
        } else {
            t = new TaiXiuInfo();
            t.money = money;
            t.TaiorXiu = taiorXiu;
            t.isReceive = 0;
            this.list_player.put(p.name, t);
            EventSpecial.update_info_tx(p);
        }
    }

    public synchronized void remove_result(Player p) {
        this.list_result.remove(p.name);
    }

    public void upTime() {
        this.time -= 1000;
    }
}
