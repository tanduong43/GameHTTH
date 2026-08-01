package event;

import java.io.IOException;
import client.Player;
import core.Service;
import core.Util;
/**
 *
 * @author Truongbk
 */
public class SucManhVatLy {

    public static void start(Player p) throws IOException {
        if (p.sucmanhvatly < 12) {
            
            if (p.lucthuc[2] < 500) {
                Service.send_box_ThongBao_OK(p, "Không đủ 500 exp lục thức");
                return;
            }
            if (p.get_vang() < 5_000_000) {
                Service.send_box_ThongBao_OK(p, "Không đủ 5 triệu bery");
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
                p.sucmanhvatly++;
                Service.UpdateInfoMaincharInfo(p);
                Service.send_box_ThongBao_OK(p,
                        "Thành công luyện thể đến tầng " + p.sucmanhvatly + "\nĐạt được:\n" + "+ " + (p.sucmanhvatly * 20)
                        + " sát thương\r\n" + "+ " + (p.sucmanhvatly * 20) + " phòng thủ\r\n" + "+ "
                        + String.format("%.2f", p.sucmanhvatly * 0.2f) + "% Chí Mạng\r\n" + "+ "
                        + String.format("%.2f", p.sucmanhvatly * 0.2f) + "% Xuyên giáp\r\n" + "+ " + (p.sucmanhvatly * 2000)
                        + " máu\r\n" + "+ " + (p.sucmanhvatly * 2000) + " mana");
            } else {
                Service.send_box_ThongBao_OK(p, "Thất bại, hãy nghỉ nghơi và thử lại");
            }
             Service.UpdateInfoMaincharInfo(p);
            
        } else {
            Service.send_box_ThongBao_OK(p, "Đã đạt cảnh giới tối đa");
        }
    }

    public static void send_info(Player p) throws IOException {
        String notice = "Hiện tại: luyện đến tầng " + p.sucmanhvatly 
                + " / 12.\nTầng tiếp theo cần  5 triệu beri và 2.000 ruby, tỷ lệ 30%"
                + "\nExp: " + p.lucthuc[2];
        Service.send_box_ThongBao_OK(p, notice);
    }
}
