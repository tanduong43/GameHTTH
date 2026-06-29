package activities;

import java.io.IOException;
import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.Skill_info;
/**
 *
 * @author Truongbk
 */
public class UpgradeDevil {
    public static void show_table(Player p, int index) throws IOException {
        switch (index) {
            case 1: {
                Message m = new Message(45);
                m.writer().writeByte(13);
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 2: {
                Message m = new Message(45);
                m.writer().writeByte(8);
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 3: { // ghep sach cong thuc dial
                Message m = new Message(45);
                m.writer().writeByte(20);
                //
                m.writer().writeUTF("Ghép vật phẩm");
                m.writer().writeByte(1);
                m.writer().writeShort(451);
                m.writer().writeShort(5);
                m.writer().writeByte(4);
                m.writer().writeShort(402);
                //
                m.writer().writeInt(10_000);
                m.writer().writeShort(0);
                m.writer().writeInt(0);
                m.writer().writeShort(452);
                m.writer().writeShort(1);
                m.writer().writeByte(4);
                m.writer().writeShort(403);
                m.writer().writeByte(20);
                //
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 4: { // ghep vo oc dial
                Message m = new Message(45);
                m.writer().writeByte(20);
                //
                m.writer().writeUTF("Ghép vật phẩm");
                m.writer().writeByte(1);
                m.writer().writeShort(454);
                m.writer().writeShort(5);
                m.writer().writeByte(4);
                m.writer().writeShort(405);
                //
                m.writer().writeInt(10_000);
                m.writer().writeShort(0);
                m.writer().writeInt(0);
                m.writer().writeShort(453);
                m.writer().writeShort(1);
                m.writer().writeByte(4);
                m.writer().writeShort(404);
                m.writer().writeByte(20);
                //
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 5: { // che tao dial
                Message m = new Message(45);
                m.writer().writeByte(20);
                //
                m.writer().writeUTF("Ghép vật phẩm");
                m.writer().writeByte(2);
                m.writer().writeShort(452);
                m.writer().writeShort(1);
                m.writer().writeByte(4);
                m.writer().writeShort(403);
                m.writer().writeShort(453);
                m.writer().writeShort(1);
                m.writer().writeByte(4);
                m.writer().writeShort(404);
                //
                m.writer().writeInt(15_000);
                m.writer().writeShort(0);
                m.writer().writeInt(2000);
                m.writer().writeShort(455);
                m.writer().writeShort(1);
                m.writer().writeByte(4);
                m.writer().writeShort(407);
                m.writer().writeByte(p.get_tyle_ghep_dial());
                //
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
        }
    }

    public static void process(Player p, Message m2) throws IOException {
        byte act = m2.reader().readByte();
        short id = m2.reader().readShort();
        byte cat = m2.reader().readByte();
        short num = m2.reader().readShort();
        if (act == 9 && cat == 104 && (num == 0 || num == 1)) { // bo skill vao (num =0) va sau khi
                                                                // xong bo vao lai (num = 1)
            Skill_info sk_temp = p.get_skill_temp(id);
            if (sk_temp != null) {
                if (sk_temp.lvdevil > 4) {
                    Service.send_box_ThongBao_OK(p,
                            sk_temp.temp.name + " đã được cường hóa tối đa!");
                    return;
                }
                if (p.item.total_item_bag_by_id(7, 9) > 9) {
                    Message m = new Message(45);
                    m.writer().writeByte(9);
                    m.writer().writeByte(0);
                    m.writer().writeShort(id);
                    m.writer().writeByte(104);
                    m.writer().writeShort(1);
                    p.conn.addmsg(m);
                    m.cleanup();
                    //
                    m = new Message(45);
                    m.writer().writeByte(9);
                    m.writer().writeByte(1);
                    m.writer().writeShort(9);
                    m.writer().writeByte(7);
                    m.writer().writeShort(10);
                    p.conn.addmsg(m);
                    m.cleanup();
                } else {
                    Service.send_box_ThongBao_OK(p,
                            "Bạn không có đủ " + ItemTemplate7.get_it_by_id(9).name);
                }
            }
        } else if (act == 12 && cat == 104 && num == 0) { // bat dau cuong hoa skill
            Skill_info sk_temp = p.get_skill_temp(id);
            if (sk_temp != null) {
                if (sk_temp.lvdevil > 4) {
                    Service.send_box_ThongBao_OK(p,
                            sk_temp.temp.name + " đã được cường hóa tối đa!");
                    return;
                }
                int percent = (sk_temp.lvdevil == 0) ? 10 //
                        : ((sk_temp.lvdevil == 1) ? 8 //
                                : ((sk_temp.lvdevil == 2) ? 6 //
                                        : ((sk_temp.lvdevil == 3) ? 5 : 4)));
                p.data_yesno = new int[] {id};
                Service.send_box_yesno(p, 33, "Thông báo",
                        ("Bạn có thật sự muốn cường hóa " + sk_temp.temp.name
                                + " không? Thành công sẽ tăng thêm " + percent
                                + "% vào cấp ác quỷ"),
                        new String[] {"50.000", "5", "Đóng"}, new byte[] {6, 7, 1});
            }
        } else if (act == 14 && id == 29 && cat == 4 && num == 1) { // bo ruong ac quy vao
            if (p.item.total_item_bag_by_id(7, 9) < 10) {
                Service.send_box_ThongBao_OK(p,
                        "Bạn không có đủ " + ItemTemplate7.get_it_by_id(9).name);
                return;
            }
            if (p.item.total_item_bag_by_id(4, 29) < 1) {
                Service.send_box_ThongBao_OK(p,
                        "Bạn không có đủ " + ItemTemplate4.get_it_by_id(29).name);
                return;
            }
            Message m = new Message(45);
            m.writer().writeByte(14);
            m.writer().writeByte(0);
            m.writer().writeShort(29);
            m.writer().writeByte(4);
            m.writer().writeShort(1);
            p.conn.addmsg(m);
            m.cleanup();
            //
            m = new Message(45);
            m.writer().writeByte(14);
            m.writer().writeByte(1);
            m.writer().writeShort(9);
            m.writer().writeByte(7);
            m.writer().writeShort(10);
            p.conn.addmsg(m);
            m.cleanup();
            // percent
            m = new Message(45);
            m.writer().writeByte(19);
            m.writer().writeByte(5);
            p.conn.addmsg(m);
            m.cleanup();
        } else if (act == 17 && id == 29 && cat == 4 && num == 1) { // bat dau upgrade euong ac quy
            if (p.item.total_item_bag_by_id(7, 9) < 10) {
                Service.send_box_ThongBao_OK(p,
                        "Bạn không có đủ " + ItemTemplate7.get_it_by_id(9).name);
                return;
            }
            if (p.item.total_item_bag_by_id(4, 29) < 1) {
                Service.send_box_ThongBao_OK(p,
                        "Bạn không có đủ " + ItemTemplate4.get_it_by_id(29).name);
                return;
            }
            Service.send_box_yesno(p, 34, "Thông báo",
                    ("Bạn có thật sự muốn nâng cấp rương ác quỷ"
                            + "? thất bại sẽ không mất rương ác quỷ"),
                    new String[] {"50.000", "5", "Đóng"}, new byte[] {6, 7, 1});
        } else if (act == 20 && id == 452 && cat == 4 && num == 0) { // ghep sach cong thuc dial
            if (p.item.able_bag() > 0) {
                if (p.item.total_item_bag_by_id(4, 451) < 5) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 5 trang giấy");
                    return;
                }
                if (p.get_vang() < 10_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 10.000 beri");
                    return;
                }
                boolean suc = 20 > Util.random(120);
                Message m = new Message(45);
                m.writer().writeByte(21);
                m.writer().writeByte(suc ? 1 : 0);
                m.writer().writeUTF(suc ? ("Chúc mừng bạn ghép thành công sách công thức")
                        : ("Rất tiếc bạn đã ghép thất bại"));
                p.conn.addmsg(m);
                m.cleanup();
                //
                p.update_vang(-10_000);
                p.update_money();
                p.item.remove_item47(4, 451, 5);
                if (suc) {
                    p.item.add_item_bag47(4, 452, 1);
                }
                p.item.update_Inventory(-1, false);
            } else {
                Service.send_box_ThongBao_OK(p, "Chừa ít nhất một ô trống để ghép vật phẩm này");
            }
        } else if (act == 20 && id == 453 && cat == 4 && num == 0) { // ghep sach cong thuc dial
            if (p.item.able_bag() > 0) {
                if (p.item.total_item_bag_by_id(4, 454) < 5) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 5 mảnh vỏ ốc");
                    return;
                }
                if (p.get_vang() < 10_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 10.000 beri");
                    return;
                }
                boolean suc = 20 > Util.random(120);
                Message m = new Message(45);
                m.writer().writeByte(21);
                m.writer().writeByte(suc ? 1 : 0);
                m.writer().writeUTF(suc ? ("Chúc mừng bạn ghép thành công vỏ ốc")
                        : ("Rất tiếc bạn đã ghép thất bại"));
                p.conn.addmsg(m);
                m.cleanup();
                //
                p.update_vang(-10_000);
                p.update_money();
                p.item.remove_item47(4, 454, 5);
                if (suc) {
                    p.item.add_item_bag47(4, 453, 1);
                }
                p.item.update_Inventory(-1, false);
            } else {
                Service.send_box_ThongBao_OK(p, "Chừa ít nhất một ô trống để ghép vật phẩm này");
            }
        } else if (act == 20 && id == 455 && cat == 4 && num == 0) { // ghep dial
            if (p.item.able_bag() > 0) {
                if (p.item.total_item_bag_by_id(4, 452) < 1) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 1 sách công thức");
                    return;
                }
                if (p.item.total_item_bag_by_id(4, 453) < 1) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 1 vỏ ốc");
                    return;
                }
                if (p.get_vang() < 15_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 15.000 beri");
                    return;
                }
                if (p.get_vnd() < 2_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 2.000 extol");
                    return;
                }
                boolean suc = p.get_tyle_ghep_dial() > Util.random(150);
                Message m = new Message(45);
                m.writer().writeByte(21);
                m.writer().writeByte(suc ? 1 : 0);
                m.writer().writeUTF(suc ? ("Chúc mừng bạn ghép thành công rương dial")
                        : ("Rất tiếc bạn đã ghép thất bại"));
                p.conn.addmsg(m);
                m.cleanup();
                //
                p.update_vang(-15_000);
                p.update_vnd(-2_000);
                p.update_money();
                p.item.remove_item47(4, 452, 1);
                p.item.remove_item47(4, 453, 1);
                if (suc) {
                    p.item.add_item_bag47(4, 455, 1);
                }
                p.item.update_Inventory(-1, false);
                p.update_skill_exp(5000, 20);
            } else {
                Service.send_box_ThongBao_OK(p, "Chừa ít nhất một ô trống để ghép vật phẩm này");
            }
        }
    }
}
