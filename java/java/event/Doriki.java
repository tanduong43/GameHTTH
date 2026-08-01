package event;

import java.io.IOException;
import client.Player;
import core.Service;
import core.Util;
/**
 *
 * @author Truongbk
 */
public class Doriki {

    public static String[] NAME = new String[]{"Trắng", "Xanh", "Lục", "Cam", "Vàng",
        "Tím", "Đỏ", "Đen"};

    public static void start(Player p) throws IOException {
        if (p.doriki[0] < 8) {
            
            if (p.lucthuc[2] < 500) {
                Service.send_box_ThongBao_OK(p, "Không đủ 500 exp lục thức");
                return;
            }
            if (p.get_vang() < 5_000_000) {
                Service.send_box_ThongBao_OK(p, "Không đủ 5 triệu beri");
                return;
            }
            if (p.get_ngoc() < 2_000) {
                Service.send_box_ThongBao_OK(p, "Không đủ 2.000 ruby");
                return;
            }
            p.lucthuc[2] -= 500;
            p.update_vang(-5_000_000);
            p.update_ngoc(-2_000);
            p.update_money();
            if (30 > Util.random(150)) {
                p.doriki[1]++;
                if (p.doriki[1] == 6) {
                    p.doriki[0]++;
                    p.doriki[1] = 1;
                    Service.send_box_ThongBao_OK(p,
                            "Thành công tăng cấp " + Doriki.NAME[p.doriki[0] - 2] + "\nĐạt được:\n" + "+ "
                            + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% sát thương \r\n" + "+ "
                            + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% máu\r\n" + "+ "
                            + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% mana\r\n" + "+ "
                            + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% phòng thủ.");
                } else {
                    Service.send_box_ThongBao_OK(p,
                            "Thành công tăng cấp " + Doriki.NAME[p.doriki[0] - 1] + " lên tầng thứ " + p.doriki[1]
                            + "\nĐạt được:\n" + "+ " + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% sát thương \r\n"
                            + "+ " + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% máu\r\n" + "+ "
                            + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% mana\r\n" + "+ "
                            + ((p.doriki[0] - 1) * 5 + p.doriki[1]) + "% phòng thủ.");
                }
                 Service.UpdateInfoMaincharInfo(p);
                
            } else {
                Service.send_box_ThongBao_OK(p, "Thất bại, hãy nghỉ nghơi và thử lại");
            }
        } else {
            Service.send_box_ThongBao_OK(p, "Đã đạt cảnh giới tối đa");
        }
    }

    public static void send_info(Player p) throws IOException {
        if (p.doriki[0] < 8) {
            String notice = "";
            
            notice += "\nDoriki hiện tại: " + Doriki.NAME[p.doriki[0] - 1] + " cấp " + p.doriki[1]+ "\nExp: " + p.lucthuc[2];
            Service.send_box_ThongBao_OK(p, notice);
        } else {
            Service.send_box_ThongBao_OK(p, "Đạt tối đa");
        }
    }
}
