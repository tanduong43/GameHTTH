package activities;

import java.io.IOException;
import client.Player;
import core.Service;
import io.Message;
import template.Item_wear;
/**
 *
 * @author Truongbk
 */
public class ChuyenHoa {
    public static void show_table(Player p) throws IOException {
        Message m = new Message(-77);
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
        p.item_chuyenhoa_save_0 = null;
        p.item_chuyenhoa_save_1 = null;
    }

    public static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        short idLeft = m2.reader().readShort();
        short idRight = -1;
        if (type == 2 || type == 3) {
            idRight = m2.reader().readShort();
        }
        switch (type) {
            case 1: {
                if (idRight == -1 && idLeft < p.item.bag3.length) {
                    Item_wear it_select = p.item.bag3[idLeft];
                    if (it_select != null) {
                        Message m = new Message(-77);
                        m.writer().writeByte(1);
                        if (it_select.levelup <= 5) {
                            m.writer().writeByte(1);
                            p.item_chuyenhoa_save_1 = it_select;
                        } else {
                            m.writer().writeByte(0);
                            p.item_chuyenhoa_save_0 = it_select;
                        }
                        m.writer().writeShort(idLeft);
                        p.conn.addmsg(m);
                        m.cleanup();
                    }
                }
                break;
            }
            case 2: {
                if (p.item_chuyenhoa_save_0 != null && p.item_chuyenhoa_save_1 != null
                        && p.item_chuyenhoa_save_0.levelup > p.item_chuyenhoa_save_1.levelup) {
                    if (p.item_chuyenhoa_save_1.template.level > 50
                            && p.item_chuyenhoa_save_1.template.level > (p.item_chuyenhoa_save_0.template.level
                                    + 10)) {
                        Service.send_box_ThongBao_OK(p,
                                "Trang bị 5x trở lên, khi chuyển hóa chỉ được chuyển hóa cho trang bị cao hơn 1 cấp trang bị!");
                        return;
                    }
                    if (p.item_chuyenhoa_save_0.template.typeEquip == 7
                            || p.item_chuyenhoa_save_1.template.typeEquip == 7) {
                        Service.send_box_ThongBao_OK(p,
                                "Không thể thực hiện chuyển hóa đối với dial!");
                        return;
                    }
                    Service.send_box_yesno(p, 6, "Thông báo",
                            ("Bạn muốn thực hiện chuyển số cường hóa 2 món đồ với mức phí là 250 Ruby?"),
                            new String[] {"250", "Đóng"}, new byte[] {7, -1});
                }
                break;
            }
        }
    }

    public static void show_result(Player p, String s, int lv) throws IOException {
        Message m = new Message(-77);
        m.writer().writeByte(3);
        m.writer().writeUTF(s);
        m.writer().writeByte(lv);
        p.conn.addmsg(m);
        m.cleanup();
    }
}
