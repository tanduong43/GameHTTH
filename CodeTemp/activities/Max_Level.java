package activities;

import client.Player;
import core.Service;
import io.Message;
import java.io.IOException;
/**
 *
 * @author Truongbk
 */
public class Max_Level {
    public static void process(Player p, Message m2) throws IOException {
        byte act = m2.reader().readByte();
        short id = m2.reader().readShort();
        if (act == 0 && p.level >= 100) {
            if (p.pointAttributeThongThao < 1) {
                Service.send_box_ThongBao_OK(p, "Không đủ 1 điểm thông thạo");
                return;
            }
            String[] name = new String[] {"Kháng phép", "MP+", "Kháng vật lý", "Tăng phòng thủ",
                    "HP+", "Tăng tấn công"};
            short[] id_op = new short[] {27, 16, 26, 4, 15, 1};
            for (int i = 0; i < id_op.length; i++) {
                if (id_op[i] == id) {
                    p.data_yesno = new int[] {i};
                    Service.send_box_yesno(p, 41, "Thông báo",
                            ("Bạn có muốn cộng 1 điểm vào " + name[i] + "?"),
                            new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
                    break;
                }
            }
        }
    }

    public static void show_table(Player p) throws IOException {
        Max_Level.set_pointMaxLevelAttri(p);
        Message m = new Message(49);
        m.writer().writeByte(2);
        m.writer().writeShort(p.pointAttributeThongThao);
        p.conn.addmsg(m);
        m.cleanup();
    }

    private static void set_pointMaxLevelAttri(Player p) throws IOException {
        Message m = new Message(49);
        m.writer().writeByte(0);
        m.writer().writeShort(p.thongthao);
        String[] name = new String[] {"Kháng phép", "MP+", "Kháng vật lý", "Tăng phòng thủ", "HP+",
                "Tăng tấn công"};
        short[] id_op = new short[] {27, 16, 26, 4, 15, 1};
        m.writer().writeByte(name.length);
        for (int i = 0; i < name.length; i++) {
            m.writer().writeShort(id_op[i]);
            m.writer().writeUTF(name[i]);
            int value = 0;
            for (int j = 0; j < p.list_op_thongthao.size(); j++) {
                if (p.list_op_thongthao.get(j).id == id_op[i]) {
                    value += p.list_op_thongthao.get(j).getParam();
                }
            }
            m.writer().writeShort(value);
            m.writer().writeShort(80);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }
}
