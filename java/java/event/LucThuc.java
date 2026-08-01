package event;

import java.io.IOException;
import client.Player;
import core.Service;
import core.Util;
/**
 *
 * @author Truongbk
 */
public class LucThuc {

    public static String[] NAME = new String[]{"Geppo", "Tekkai", "Shigan", "Rankyaku", "Soru", "Kami-e",
        "Rokuogan"};
    public static String[] NAME_2 = new String[]{"Sơ cấp", "Trung cấp", "Cao Cấp"};
    public static int[][] op = new int[][]{ //
        new int[]{5, 6, 7, 8, 9}, // Geppo
        new int[]{5, 6, 7, 8, 9}, // Tekkai
        new int[]{5, 6, 7, 8, 9, 10}, //Shigan
        new int[]{5, 6, 7, 8, 9, 10, 11, 12}, //Rankyaku
        new int[]{5, 6, 7, 8, 9, 10, 11, 12}, //Soru
        new int[]{5, 6, 7, 8, 9, 10, 11, 12, 13}, //Kami-e
        new int[]{5, 6, 7, 8, 9, 10, 11, 12, 13}, //Rokuogan
    };
    public static int[][] par = new int[][]{ //
        new int[]{1, 1, 1, 1, 1}, //  
        new int[]{3, 3, 3, 3, 3}, //
        new int[]{5, 5, 5, 5, 5, 5}, //
        new int[]{7, 7, 7, 7, 7, 7, 10,10},  
        new int[]{9, 9, 9, 9, 9, 9, 20,10}, //
        new int[]{12, 12, 12, 12, 12, 12, 20, 20, 10}, //
        new int[]{15, 15, 15, 15, 15, 15, 20, 20, 20}, //
        
    };

    public static void start(Player p) throws IOException {
        if (p.lucthuc[0] < 11) {
            if (p.lucthuc[1] < 5) {
                if (p.lucthuc[2] < 500) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 500 exp lục thức");
                    return;
                }
                if (p.get_vang() < 10_000_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 10tr vàng");
                    return;
                }
                if (p.get_ngoc() < 3_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 3k ngọc");
                    return;
                }
                
                p.lucthuc[2] -= 500;
                p.update_vang(-10_000_000);
                p.update_ngoc(-3_000);
                p.update_money();
                p.lucthuc[1]++;
                Service.UpdateInfoMaincharInfo(p);
                Service.send_box_ThongBao_OK(p, "Thành công nâng cấp tại cảnh giới " + LucThuc.NAME[p.lucthuc[0] - 1]
                        + " đạt cấp " + LucThuc.NAME_2[p.lucthuc[1] - 1]);
            } else {
                
                if (p.lucthuc[2] < 1000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 1000 exp lục thức");
                    return;
                }
                
                if (p.get_vang() < 20_000_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 20 triệu beri");
                    return;
                }
                if (p.get_ngoc() < 5_000) {
                    Service.send_box_ThongBao_OK(p, "Không đủ 5.000 ruby");
                    return;
                }
                p.lucthuc[2] -= 1000;
                p.update_vang(-20_000_000);
                p.update_ngoc(-10_000);
                p.update_money();
            
            if (10 > Util.random(150) && p.lucthuc[1] == 5) {
                p.lucthuc[1] = 1;
                p.lucthuc[0]++;
                Service.send_box_ThongBao_OK(p, "Thành công đột phá cảnh giới " + LucThuc.NAME[p.lucthuc[0] - 2]
                        + "\nĐạt cảnh giới mới: + " + LucThuc.NAME[p.lucthuc[0] - 1]);
                Service.UpdateInfoMaincharInfo(p);
            } else {
                Service.send_box_ThongBao_OK(p, "Thất bại, hãy nghỉ nghơi và thử lại");
            }
            }
        } else {
            Service.send_box_ThongBao_OK(p, "Đã đạt cảnh giới tối đa");
        }
    }

    public static void send_info(Player p) throws IOException {
        if (p.lucthuc[0] < 11) {
            String notice = "Hiện tại:";
            
            notice += "\nCảnh giới: " + LucThuc.NAME[p.lucthuc[0] - 1] + " cấp " + LucThuc.NAME_2[p.lucthuc[1] - 1]
                    + "\nExp: " + p.lucthuc[2];
            Service.send_box_ThongBao_OK(p, notice);
        } else {
            Service.send_box_ThongBao_OK(p, "Bạn đã đạt tuyệt kỹ Rokuogan, kỹ thuật tối cao của Lục Thức, một loại võ thuật đặc biệt trong One Piece.");
        }
    }
}
