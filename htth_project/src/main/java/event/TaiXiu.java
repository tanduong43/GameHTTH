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
    public static final long MAX_BET_PER_PLAYER = 1_000_000_000L; // Tối đa 1 tỷ mỗi người (win x2 = 2 tỷ)
    public static final long MAX_TOTAL_BET = 2_000_000_000L; // Tối đa 2 tỷ mỗi cửa
    private boolean running;
    private Thread myth;
    private long time;
    private HashMap<String, TaiXiuInfo> list_player;
    private HashMap<String, TaiXiuInfo> list_result;
    private long XiuTotal;
    private long TaiTotal;
    private byte[] dice;
    // Admin can thiệp kết quả: -1: Ngẫu nhiên, 1: Ép Tài, 0: Ép Xỉu
    private int forceResult = -1;
    private boolean keepForce = false;
    private byte[] forceDice = null;
    private boolean isSettled = false;

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

    public synchronized void checkAndSettleResult() {
        if (!this.isSettled) {
            settleResult();
        }
    }

    public synchronized void settleResult() {
        if (this.isSettled) {
            return;
        }
        this.isSettled = true;
        System.out.println("[TaiXiu] Bat dau chot ket qua phien. forceResult=" + this.forceResult 
                + ", keepForce=" + this.keepForce 
                + ", forceDice=" + (this.forceDice != null ? (this.forceDice[0] + "-" + this.forceDice[1] + "-" + this.forceDice[2]) : "null"));

        if (this.forceDice != null) {
            dice[0] = forceDice[0];
            dice[1] = forceDice[1];
            dice[2] = forceDice[2];
            this.forceDice = null; // Áp dụng xong cho phiên hiện tại thì reset
            System.out.println("[TaiXiu] Ap dung forceDice: " + dice[0] + "-" + dice[1] + "-" + dice[2]);
        } else if (this.forceResult == 1) { // Ép TÀI (11-17 điểm)
            do {
                for (int i = 0; i < 3; i++) {
                    dice[i] = (byte) Util.random(1, 7);
                }
            } while ((dice[0] + dice[1] + dice[2]) < 11 || (dice[0] + dice[1] + dice[2]) > 17);
            if (!this.keepForce) {
                this.forceResult = -1;
            }
            System.out.println("[TaiXiu] Ap dung ep TAI: " + dice[0] + "-" + dice[1] + "-" + dice[2] + " (Tong: " + (dice[0] + dice[1] + dice[2]) + ")");
        } else if (this.forceResult == 0) { // Ép XỈU (4-10 điểm)
            do {
                for (int i = 0; i < 3; i++) {
                    dice[i] = (byte) Util.random(1, 7);
                }
            } while ((dice[0] + dice[1] + dice[2]) < 4 || (dice[0] + dice[1] + dice[2]) > 10);
            if (!this.keepForce) {
                this.forceResult = -1;
            }
            System.out.println("[TaiXiu] Ap dung ep XIU: " + dice[0] + "-" + dice[1] + "-" + dice[2] + " (Tong: " + (dice[0] + dice[1] + dice[2]) + ")");
        } else {
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
            System.out.println("[TaiXiu] Ket qua tu nhien: " + dice[0] + "-" + dice[1] + "-" + dice[2] + " (Tong: " + xucxacResult + ")");
        }

        int xucxacResult = dice[0] + dice[1] + dice[2];
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
    }

    public synchronized void resetNewRound() {
        this.time = TIME_ROUND;
        this.TaiTotal = 0;
        this.XiuTotal = 0;
        this.list_player.clear();
        this.isSettled = false;
        System.out.println("[TaiXiu] Bat dau phien moi.");
    }

    private synchronized void update() {
        if (this.time <= 0 && !this.isSettled) {
            settleResult();
        }
        if (this.time <= -15_000) {
            resetNewRound();
        }
    }

    public void close() {
        this.running = false;
        this.myth.interrupt();
    }

    public long get_time() {
        return this.time;
    }

    public long MoneyTotal(int i) {
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

    public synchronized void setForceResult(int result, boolean keep) {
        this.forceResult = result;
        this.keepForce = keep;
        this.forceDice = null;
        System.out.println("[TaiXiu Admin] setForceResult: result=" + result + " (" + (result == 1 ? "TÀI" : "XỈU") + "), keep=" + keep + ", isSettled=" + this.isSettled);
    }

    public synchronized int getForceResult() {
        return this.forceResult;
    }

    public synchronized boolean isKeepForce() {
        return this.keepForce;
    }

    public synchronized void setForceDice(byte d1, byte d2, byte d3) {
        this.forceDice = new byte[] { d1, d2, d3 };
        this.forceResult = -1;
        this.keepForce = false;
        System.out.println("[TaiXiu Admin] setForceDice: " + d1 + " - " + d2 + " - " + d3 + ", isSettled=" + this.isSettled);
    }

    public synchronized void clearForce() {
        this.forceResult = -1;
        this.keepForce = false;
        this.forceDice = null;
        System.out.println("[TaiXiu Admin] clearForce (chuyen sang Ngau nhien)");
    }

    public synchronized byte[] getForceDice() {
        return this.forceDice;
    }

    public synchronized boolean isSettled() {
        return this.isSettled;
    }

    public synchronized String getShortStatus() {
        if (this.forceDice != null) {
            return "XX " + forceDice[0] + "-" + forceDice[1] + "-" + forceDice[2];
        }
        if (this.forceResult == 1) {
            return this.keepForce ? "Cố định TÀI" : "Ép TÀI";
        }
        if (this.forceResult == 0) {
            return this.keepForce ? "Cố định XỈU" : "Ép XỈU";
        }
        return "Ngẫu nhiên";
    }

    public synchronized String getTxDebugInfo() {
        long sec = Math.max(0, this.time / 1000);
        int countXiu = 0;
        int countTai = 0;
        for (TaiXiuInfo inf : this.list_player.values()) {
            if (inf.TaiorXiu == 0) countXiu++;
            else if (inf.TaiorXiu == 1) countTai++;
        }
        String mode;
        if (this.forceDice != null) {
            int tot = forceDice[0] + forceDice[1] + forceDice[2];
            mode = "Cố định xúc xắc [" + forceDice[0] + "-" + forceDice[1] + "-" + forceDice[2] + "] (" + tot + " điểm -> " + ((tot >= 11 && tot <= 17) ? "TÀI" : "XỈU") + ")";
        } else if (this.forceResult == 1) {
            mode = "ÉP RA TÀI (" + (keepForce ? "Cố định mọi ván" : "Chỉ ván này") + ")";
        } else if (this.forceResult == 0) {
            mode = "ÉP RA XỈU (" + (keepForce ? "Cố định mọi ván" : "Chỉ ván này") + ")";
        } else {
            mode = "Ngẫu nhiên (Tự nhiên)";
        }

        String roundStatus = this.isSettled ? "Đã chốt kết quả (đang chờ ván mới)" : "Đang mở cược";

        return "🎲 THÔNG TIN TÀI XỈU 🎲\n"
                + "⏱ Thời gian còn lại: " + sec + " giây (" + roundStatus + ")\n"
                + "🔵 Cửa XỈU: " + Util.number_format(this.XiuTotal) + " beri (" + countXiu + " người)\n"
                + "🔴 Cửa TÀI: " + Util.number_format(this.TaiTotal) + " beri (" + countTai + " người)\n"
                + "⚙️ Chế độ can thiệp: " + mode;
    }

    public synchronized void register(Player p, int money, byte taiorXiu) throws IOException {
        if (p.conn == null || (p.conn.status != 1 && !"admin".equalsIgnoreCase(p.conn.user))) {
            Service.send_box_ThongBao_OK(p, "Chỉ thành viên đã kích hoạt (MTV) mới có thể đặt cược Tài Xỉu!");
            return;
        }
        if (this.time <= 0) {
            Service.send_box_ThongBao_OK(p, "Không trong thời gian đặt cược!");
            return;
        }
        if (money <= 0) {
            Service.send_box_ThongBao_OK(p, "Số tiền đặt cược không hợp lệ!");
            return;
        }
        if (p.get_vang() < money) {
            Service.send_box_ThongBao_OK(p, "Bạn không đủ " + Util.number_format(money) + " beri để đặt cược!");
            return;
        }
        if (this.list_result.containsKey(p.name)) {
            Service.send_box_ThongBao_OK(p, "Bạn có tiền thưởng chưa nhận, vui lòng nhận thưởng trước khi tiếp tục đặt cược!");
            return;
        }
        TaiXiuInfo t = this.list_player.get(p.name);
        if (t != null && t.TaiorXiu != taiorXiu) {
            Service.send_box_ThongBao_OK(p, "Bạn đã cược cửa " + (t.TaiorXiu == 1 ? "Tài" : "Xỉu") + ", không thể cược cửa còn lại trong phiên này!");
            return;
        }
        long myCurrentBet = (t != null) ? t.money : 0L;
        if (myCurrentBet >= MAX_BET_PER_PLAYER) {
            Service.send_box_ThongBao_OK(p, "Bạn đã cược tối đa " + Util.number_format(MAX_BET_PER_PLAYER) + " beri cho phiên này, không thể đặt cược nữa!");
            return;
        }
        if (myCurrentBet + money > MAX_BET_PER_PLAYER) {
            long canBet = MAX_BET_PER_PLAYER - myCurrentBet;
            Service.send_box_ThongBao_OK(p, "Bạn chỉ có thể cược thêm tối đa " + Util.number_format(canBet) + " beri (tối đa " + Util.number_format(MAX_BET_PER_PLAYER) + " beri/phiên)!");
            return;
        }
        long currentTotal = (taiorXiu == 0) ? XiuTotal : TaiTotal;
        if (currentTotal >= MAX_TOTAL_BET) {
            Service.send_box_ThongBao_OK(p, "Cửa " + (taiorXiu == 1 ? "Tài" : "Xỉu") + " đã đạt giới hạn cược tối đa (" + Util.number_format(MAX_TOTAL_BET) + " beri), không thể đặt cược nữa!");
            return;
        }
        if (currentTotal + money > MAX_TOTAL_BET) {
            long remainTotal = MAX_TOTAL_BET - currentTotal;
            Service.send_box_ThongBao_OK(p, "Cửa " + (taiorXiu == 1 ? "Tài" : "Xỉu") + " chỉ còn có thể nhận thêm tối đa " + Util.number_format(remainTotal) + " beri!");
            return;
        }

        if (taiorXiu == 0) {
            XiuTotal += money;
        } else {
            TaiTotal += money;
        }
        p.set_spend_context("Đặt cược Tài Xỉu", (taiorXiu == 0 ? "Cược Xỉu" : "Cược Tài"));
        p.update_vang(-money);
        p.update_money();
        if (t != null) {
            t.money += money;
            EventSpecial.update_info_tx(p);
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
