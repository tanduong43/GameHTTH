package event;

import client.Player;
import core.Manager;
import core.MenuController;
import core.Service;
import core.Util;
import io.Message;
import template.TaiXiuInfo;
import java.io.IOException;
/**
 *
 * @author Truongbk
 */
public class EventSpecial {
    public static void process(Player p, Message m2) throws IOException {
        if (p.conn == null || p.conn.status != 1) {
            Service.send_box_ThongBao_OK(p, "Chỉ thành viên đã kích hoạt (MTV) mới có thể tham gia Tài Xỉu!");
            return;
        }
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
        if (type == 0 && act >= 90 && act <= 99) {
            if (p.conn != null && "admin".equalsIgnoreCase(p.conn.user)) {
                TaiXiu tx = Manager.gI().TaiXiu();
                switch (act) {
                    case 90:
                        Service.send_box_ThongBao_OK(p, tx.getTxDebugInfo());
                        break;
                    case 91:
                        tx.setForceResult(1, false);
                        Service.send_box_ThongBao_OK(p, "Đã can thiệp: Ván hiện tại sẽ ra TÀI (11-17 điểm)!\n\n" + tx.getTxDebugInfo());
                        show_table(p, 0);
                        break;
                    case 92:
                        tx.setForceResult(0, false);
                        Service.send_box_ThongBao_OK(p, "Đã can thiệp: Ván hiện tại sẽ ra XỈU (4-10 điểm)!\n\n" + tx.getTxDebugInfo());
                        show_table(p, 0);
                        break;
                    case 93:
                        tx.setForceResult(1, true);
                        Service.send_box_ThongBao_OK(p, "Đã can thiệp: CỐ ĐỊNH TÀI cho mọi ván tới!\n\n" + tx.getTxDebugInfo());
                        show_table(p, 0);
                        break;
                    case 94:
                        tx.setForceResult(0, true);
                        Service.send_box_ThongBao_OK(p, "Đã can thiệp: CỐ ĐỊNH XỈU cho mọi ván tới!\n\n" + tx.getTxDebugInfo());
                        show_table(p, 0);
                        break;
                    case 95:
                        Service.input_text(p, 32009, "Đặt 3 Xúc Xắc",
                                new String[] { "Xúc xắc 1 (1-6)", "Xúc xắc 2 (1-6)", "Xúc xắc 3 (1-6)" });
                        break;
                    case 96:
                        tx.clearForce();
                        Service.send_box_ThongBao_OK(p, "Đã hủy can thiệp! Tài Xỉu quay ngẫu nhiên bình thường.\n\n" + tx.getTxDebugInfo());
                        show_table(p, 0);
                        break;
                    case 99:
                        MenuController.send_dynamic_menu(p, 9991, "Quản Lý Tài Xỉu",
                                new String[] { "Xem thông tin cược", "Ép TÀI (ván này)", "Ép XỈU (ván này)",
                                        "Cố định TÀI (mọi ván)", "Cố định XỈU (mọi ván)", "Đặt 3 xúc xắc cụ thể", "Hủy can thiệp (Random)" },
                                null);
                        break;
                }
            }
            return;
        }
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
                    Service.send_box_ThongBao_OK(p, "Nhận " + Util.number_format(t.money) + " beri");
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
                Service.send_box_ThongBao_OK(p, "Không đủ " + Util.number_format(money) + " beri");
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
        m.writer().writeInt((int) Math.min(Manager.gI().TaiXiu().MoneyTotal(0), Integer.MAX_VALUE)); // xiu
        m.writer().writeInt((int) Math.min(Manager.gI().TaiXiu().MoneyTotal(1), Integer.MAX_VALUE)); // tai
        TaiXiuInfo myInfo = Manager.gI().TaiXiu().get_my_info(p);
        if (myInfo != null) {
            m.writer().writeInt((int) Math.min(myInfo.money, Integer.MAX_VALUE)); // cuoc
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
            boolean isAdmin = (p.conn != null && "admin".equalsIgnoreCase(p.conn.user));
            m.writer().writeUTF(isAdmin ? ("Tài xỉu [Admin:" + Manager.gI().TaiXiu().getShortStatus() + "]") : "Tài xỉu");
            long time_ = Manager.gI().TaiXiu().get_time();
            if (time_ < 0) {
                time_ = 0;
            }
            m.writer().writeShort((short) (time_ / 1000)); // time = second
            m.writer().writeInt((int) Math.min(Manager.gI().TaiXiu().MoneyTotal(0), Integer.MAX_VALUE)); // xiu
            m.writer().writeInt((int) Math.min(Manager.gI().TaiXiu().MoneyTotal(1), Integer.MAX_VALUE)); // tai
            TaiXiuInfo myInfo = Manager.gI().TaiXiu().get_my_info(p);
            if (myInfo != null) {
                m.writer().writeInt((int) Math.min(myInfo.money, Integer.MAX_VALUE)); // cuoc
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
