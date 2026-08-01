package event;

import client.Player;
import core.Manager;
import core.Service;
import io.Message;
import template.TaiXiuInfo;
import java.io.IOException;
/**
 *
 * @author Truongbk
 */
public class EventSpecial {
    public static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        byte act = m2.reader().readByte();
        int money = -1;
        byte TaiorXiu = -1;
        byte isAll = -1;
        try {
            money = m2.reader().readInt();
            TaiorXiu = m2.reader().readByte();
            isAll = m2.reader().readByte();
        } catch (IOException e) {
        }
        // System.out.printf("type %s act %s money %s taiORxiu %s isAll %s\n", type, act, money,
        // TaiorXiu, isAll);
        if (type == 0 && act == 3 && money == -1 && TaiorXiu == -1 && isAll == -1) {
            update_info_tx(p);
            long time = Manager.gI().TaiXiu().get_time();
            if (time > 5_000 && time < (TaiXiu.TIME_ROUND - 25_000)) {
                show_table(p, 0);
            }
        } else if (type == 0 && act == 2 && money == -1 && TaiorXiu == -1 && isAll == -1) {
            notice_dice_TaiXiu(p);
        } else if (type == 0 && act == 0 && money == -1 && TaiorXiu == -1 && isAll == -1) {
            show_table(p, 0);
            TaiXiuInfo t = Manager.gI().TaiXiu().get_my_result(p);
            if (t != null) {
                if (t.isReceive == 0) {
                    t.isReceive = 1;
                    p.update_vang(t.money);
                    p.update_money();
                    Service.send_box_ThongBao_OK(p, "Nhận " + t.money + " beri");
                    Manager.gI().TaiXiu().remove_result(p);
                }
            }
        } else if (type == 0 && act == 1 && money > 0 && (TaiorXiu == 1 || TaiorXiu == 0)
                && isAll == 0) {
            System.out.println(money);
            Manager.gI().TaiXiu().register(p, money, TaiorXiu);
        } else if (type == 0 && act == 1 && money > 0 && (TaiorXiu == 1 || TaiorXiu == 0)
                && isAll == 1) {
            if (p.get_vang() < money) {
                Service.send_box_ThongBao_OK(p, "Không đủ " + money + " beri");
                return;
            }
            Manager.gI().TaiXiu().register(p, money, TaiorXiu);
        }
    }

    private static void notice_dice_TaiXiu(Player p) throws IOException {
        Message m = new Message(80);
        m.writer().writeByte(0);
        m.writer().writeByte(2);
        byte[] result = Manager.gI().TaiXiu().get_dice_now();
        if ((result[0] + result[1] + result[2]) >= 11
                && (result[0] + result[1] + result[2]) <= 17) {
            m.writer().writeByte(1); // kq
        } else {
            m.writer().writeByte(0); // kq
        }
        m.writer().write(result);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void update_info_tx(Player p) throws IOException {
        Message m = new Message(80);
        m.writer().writeByte(0);
        m.writer().writeByte(1);
        m.writer().writeInt(Manager.gI().TaiXiu().MoneyTotal(0)); // xiu
        m.writer().writeInt(Manager.gI().TaiXiu().MoneyTotal(1)); // tai
        TaiXiuInfo myInfo = Manager.gI().TaiXiu().get_my_info(p);
        if (myInfo != null) {
            m.writer().writeInt(myInfo.money); // cuoc
            m.writer().writeByte(myInfo.TaiorXiu); // tai or xiu
        } else {
            m.writer().writeInt(0); // cuoc
            m.writer().writeByte(-1); // tai or xiu
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void show_table(Player p, int type) throws IOException {
        if (type == 0) {
            Message m = new Message(80);
            m.writer().writeByte(0);
            m.writer().writeByte(0);
            m.writer().writeUTF("Tài xỉu");
            long time_ = Manager.gI().TaiXiu().get_time();
            if (time_ < 0) {
                time_ = 0;
            }
            m.writer().writeShort((short) (time_ / 1000)); // time = second
            m.writer().writeInt(Manager.gI().TaiXiu().MoneyTotal(0)); // xiu
            m.writer().writeInt(Manager.gI().TaiXiu().MoneyTotal(1)); // tai
            TaiXiuInfo myInfo = Manager.gI().TaiXiu().get_my_info(p);
            if (myInfo != null) {
                m.writer().writeInt(myInfo.money); // cuoc
                m.writer().writeByte(myInfo.TaiorXiu); // tai or xiu
            } else {
                m.writer().writeInt(0); // cuoc
                m.writer().writeByte(-1); // tai or xiu
            }
            m.writer().writeByte(-1); // kq
            m.writer().write(Manager.gI().TaiXiu().get_dice_now());
            p.conn.addmsg(m);
            m.cleanup();
        }
    }
}
