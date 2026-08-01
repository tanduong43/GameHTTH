package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.ItemTemplate4;
import java.io.IOException;
/**
 *
 * @author Truongbk
 */
public class Ship {
    public static void show_table(Player p) throws IOException {
        if (p.map.template.id == 1 && p.typePirate == 0) {
            Message m = new Message(-19);
            m.writer().writeByte(101);
            m.writer().writeUTF("Lái buôn");
            m.writer().writeByte(4);
            m.writer().writeShort(4);
            //
            m.writer().writeShort(39);
            m.writer().writeShort(1);
            m.writer().writeShort(38);
            m.writer().writeShort(1);
            m.writer().writeShort(37);
            m.writer().writeShort(1);
            m.writer().writeShort(36);
            m.writer().writeShort(1);
            p.conn.addmsg(m);
            m.cleanup();
            notice_ship_packet(p, 36);
        }
    }

    private static void notice_ship_packet(Player p, int type) throws IOException {
        if (p.map.template.id == 1 && p.typePirate == 0) {
            Message m = new Message(-53);
            m.writer().writeByte(0);
            m.writer()
                    .writeUTF("Gói hàng hiện tại của bạn là " + ItemTemplate4.get_item_name(type));
            m.writer().writeShort(type);
            p.conn.addmsg(m);
            m.cleanup();
            //
            p.id_ship_packet = (short) type;
        }
    }

    public static void process(Player p, Message m2) throws IOException {
        if (p.map.template.id == 1 && p.typePirate == 0) {
            byte act = m2.reader().readByte();
            // System.out.println(act);
            switch (act) {
                case 0: {
                    if (p.item.total_item_bag_by_id(4, 361) > 0) {
                        if (80 > Util.random(120)) {
                            Ship.notice_ship_packet(p, 36);
                        } else if (90 > Util.random(120)) {
                            Ship.notice_ship_packet(p, 37);
                        } else if (95 > Util.random(120)) {
                            Ship.notice_ship_packet(p, 38);
                        } else {
                            Ship.notice_ship_packet(p, 39);
                        }
                        p.item.remove_item47(4, 361, 1);
                        p.item.update_Inventory(-1, false);
                    } else {
                        Service.send_box_ThongBao_OK(p,
                                "Không đủ 1 " + ItemTemplate4.get_item_name(361));
                    }
                    break;
                }
                case 1: {
                    if (p.id_ship_packet != -1) {
                        Service.send_box_yesno(p, 50, "Thông báo",
                                "Để tham gia lái buôn, bạn phải mất 10.000 beri, bạn có "
                                        + "muốn tham gia?",
                                new String[] {"10.000", "Hủy"}, new byte[] {6, -1});
                    }
                    break;
                }
            }
        }
    }

    public static void notice_start_shipping(Player p) throws IOException {
        if (p.map.template.id == 1 && p.typePirate == 0 && p.id_ship_packet != -1) {
            Message m = new Message(-53);
            m.writer().writeByte(1);
            m.writer().writeUTF("Gói hàng " + ItemTemplate4.get_item_name(p.id_ship_packet)
                    + " của bạn đang được chuyển đến, lên đường may mắn");
            p.conn.addmsg(m);
            m.cleanup();
        }
    }
}
