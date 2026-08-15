package event;

import java.io.IOException;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.ItemTemplate4;

/**
 * Xử lý chế tạo sự kiện Tết Nguyên Đán
 * 
 * Công thức chế tạo:
 * - Bánh Chưng: 5 Lá Dong + 5 Đậu Xanh + 5 Gạo Nếp + 5 Thịt Heo + 50.000 Beri
 * - Bánh Giầy: 5 Lá Chuối + 5 Bột Gạo + 2 Bó Lạt Tre + 30.000 Beri
 * - Ghép Chữ Vàng: 5 chữ Cùng-Vui-Đón-Tết-Tân Niên
 */
public class TetCraft {

    /**
     * Hiển thị thông tin chế tạo sự kiện Tết
     */
    public static void showCraftMenu(Player p) throws IOException {
        Message m = new Message(-50);
        m.writer().writeByte(1);
        m.writer().writeByte(0);
        m.writer().writeUTF("🧧 Sự Kiện Tết Nguyên Đán\n\n"
                + "📍 LÀM BÁNH:\n"
                + "1. 🥮 Bánh Chưng: 5 Lá Dong + 5 Đậu Xanh + 5 Gạo Nếp + 5 Thịt Heo + 50.000 Beri\n"
                + "2. 🥮 Bánh Giầy: 5 Lá Chuối + 5 Bột Gạo + 2 Bó Lạt Tre + 30.000 Beri\n\n"
                + "📍 GHÉP CHỮ VÀNG:\n"
                + "3. ✨ Ghép Bộ Chữ: Cùng + Vui + Đón + Tết + Tân Niên\n"
                + "   → Nhận 1 trong 3 quà: Hộp TT / Rương Tết / 10 Bao Lì Xì\n\n"
                + "📍 NGUỒN NGUYÊN LIỆU:\n"
                + "• Nguyên liệu: Đánh quái rơi\n"
                + "• Bó Lạt Tre: Nhiệm vụ Tết / Cướp Nồi Bánh\n"
                + "• Hũ Gia Vị: Rơi từ Boss Lân Sư Tử\n\n"
                + "🎁 SỬ DỤNG BÁNH:\n"
                + "• Bánh Chưng: +20% ST + 10% Máu cuối (30 ph) + 10k EXP\n"
                + "• Bánh Giầy: Hồi 100% HP/MP + 15% Né tránh (30 ph)");
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Xử lý chế tạo theo lựa chọn
     */
    public static void processCraft(Player p, int choice) throws IOException {
        if (!EventTet.isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Tết chưa được kích hoạt!");
            return;
        }

        switch (choice) {
            case 1:
                craftBanhChung(p);
                break;
            case 2:
                craftBanhGiay(p);
                break;
            case 3:
                EventTet.getInstance().onLetterCombine(p);
                break;
            default:
                Service.send_box_ThongBao_OK(p, "Lựa chọn không hợp lệ!");
                break;
        }
    }

    private static boolean checkAndRemoveMaterials(Player p, int[][] materials, int beriCost) {
        // Kiểm tra Beri
        if (p.get_vang() < beriCost) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ " + Util.number_format(beriCost) + " Beri!");
            } catch (IOException e) {
                System.out.println("Error: " + e.getMessage());
            }
            return false;
        }

        // Kiểm tra và xóa nguyên liệu
        for (int[] mat : materials) {
            int itemId = mat[0];
            int quantity = mat[1];
            if (p.item.total_item_bag_by_id(4, itemId) < quantity) {
                ItemTemplate4 template = ItemTemplate4.get_it_by_id(itemId);
                String itemName = template != null ? template.name : "Nguyên liệu ID " + itemId;
                try {
                    Service.send_box_ThongBao_OK(p, "Bạn không đủ " + quantity + " " + itemName + "!");
                } catch (IOException e) {
                    System.out.println("Error: " + e.getMessage());
                }
                return false;
            }
        }

        // Xóa Beri
        p.update_vang(-beriCost);

        // Xóa nguyên liệu
        for (int[] mat : materials) {
            int itemId = mat[0];
            int quantity = mat[1];
            p.item.remove_item47(4, itemId, quantity);
        }

        return true;
    }

    private static void craftBanhChung(Player p) throws IOException {
        int[][] materials = {
                {EventTet.ITEM_LA_DONG, 5},
                {EventTet.ITEM_DAU_XANH, 5},
                {EventTet.ITEM_GAO_NEP, 5},
                {EventTet.ITEM_THIT_HEO, 5}
        };
        int beriCost = 50_000;

        if (checkAndRemoveMaterials(p, materials, beriCost)) {
            p.item.add_item_bag47(4, EventTet.ITEM_BANH_CHUNG, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Bánh Chưng!\n+Tác dụng: +20% ST, +10% Máu cuối (30 phút), +10k EXP");
        }
    }

    private static void craftBanhGiay(Player p) throws IOException {
        int[][] materials = {
                {EventTet.ITEM_LA_CHUOI, 5},
                {EventTet.ITEM_BOT_GAO, 5},
                {EventTet.ITEM_BO_LAT_TRE, 2}
        };
        int beriCost = 30_000;

        if (checkAndRemoveMaterials(p, materials, beriCost)) {
            p.item.add_item_bag47(4, EventTet.ITEM_BANH_GIAY, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Bánh Giầy!\n+Tác dụng: Hồi 100% HP/MP, +15% Né tránh (30 phút)");
        }
    }
}
