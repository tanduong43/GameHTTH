package event;

import java.io.IOException;
import java.util.List;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.GiftBox;
import template.ItemTemplate4;
import template.ItemTemplate7;

/**
 * Xử lý ghép đồ sự kiện Trung Thu
 * 
 * Công thức ghép:
 * - Bánh Trung Thu: 5 Bột Mì + 3 Đường + 500k Beri
 * - Bánh Đậu Xanh: 5 Bột Mì + 3 Đường + 1 Trứng Muối + 1M Beri
 * - Bánh Trứng Muối: 5 Bột Mì + 3 Đường + 2 Trứng Muối + 15M Beri
 * - Bánh Hạt Sen: 5 Bột Mì + 3 Đường + 3 Trứng Muối + 2M Beri
 * - Đèn Kéo Quân: 3 Đèn Ông Sao + 2M Beri
 * - Hộp Bánh: 1 mỗi loại bánh + 2M Beri + 50 Ruby
 * - Hộp Thượng Hạng: 1 Hộp Bánh + 1 Giấy Gói Quà + 2M Beri + 100 Ruby
 */
public class TrungThuCraft {

    /**
     * Hiển thị thông tin chế tạo sự kiện Trung Thu
     * (Được gọi khi xem hướng dẫn chi tiết)
     */
    public static void showCraftMenu(Player p) throws IOException {
        Message m = new Message(-50);
        m.writer().writeByte(1);
        m.writer().writeByte(0);
        m.writer().writeUTF("🎑 Sự Kiện Trung Thu - Đêm Rằm Hải Tặc\n\n"
                + "📍 LÀM BÁNH (NPC Chị Hằng → Làm Bánh):\n"
                + "1. 🥮 Bánh Trung Thu: 5 Bột Mì + 3 Đường + 500.000 Beri\n"
                + "2. 🥮 Bánh Đậu Xanh: 5 Bột Mì + 3 Đường + 1 Trứng Muối + 1.000.000 Beri\n"
                + "3. 🥮 Bánh Trứng Muối: 5 Bột Mì + 3 Đường + 2 Trứng Muối + 15.000.000 Beri\n"
                + "4. 🥮 Bánh Hạt Sen: 5 Bột Mì + 3 Đường + 3 Trứng Muối + 2.000.000 Beri\n"
                + "5. 🎁 Hộp Bánh: 1 mỗi loại bánh + 2.000.000 Beri + 50 Ruby\n"
                + "6. 🏆 Hộp Thượng Hạng: 1 Hộp Bánh + 1 Giấy Gói Quà + 2.000.000 Beri + 100 Ruby\n\n"
                + "📍 GHÉP ĐÈN (NPC Chị Hằng → Ghép Đèn):\n"
                + "• Đèn Kéo Quân: 3 Đèn Ông Sao + 2.000.000 Beri\n\n"
                + "📍 Nguồn nguyên liệu:\n"
                + "• Bột Mì: Đánh quái ±10 cấp\n"
                + "• Đường: Làm Nhiệm vụ Lặp\n"
                + "• Trứng Muối: NV Băng / PvP / Truy Nã\n"
                + "• Đèn Ông Sao: Phó Bản Nami / Đá đít Mr.3\n"
                + "• Giấy Gói Quà: Săn Boss Lân / Vận buôn");
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Xử lý ghép đồ theo lựa chọn
     */
    public static void processCraft(Player p, int choice) throws IOException {
        if (!EventTrungThu.isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Trung Thu chưa được kích hoạt!");
            return;
        }

        switch (choice) {
            case 1:
                craftBanhTrungThu(p);
                break;
            case 2:
                craftBanhDauXanh(p);
                break;
            case 3:
                craftBanhTrungMuoi(p);
                break;
            case 4:
                craftBanhHatSen(p);
                break;
            case 5:
                craftDenKeoQuan(p);
                break;
            case 6:
                craftHopBanh(p);
                break;
            case 7:
                craftHopBanhThuongHang(p);
                break;
            default:
                Service.send_box_ThongBao_OK(p, "Lựa chọn không hợp lệ!");
                break;
        }
    }

    private static boolean checkAndRemoveMaterials(Player p, int[][] materials, int beriCost, int rubyCost) {
        // Kiểm tra Beri
        if (p.get_vang() < beriCost) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ " + Util.number_format(beriCost) + " Beri!");
            } catch (IOException e) {
                System.out.println("Error: " + e.getMessage());
            }
            return false;
        }

        // Kiểm tra Ruby
        if (rubyCost > 0 && p.get_ngoc() < rubyCost) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ " + rubyCost + " Ruby!");
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

        // Xóa Ruby
        if (rubyCost > 0) {
            p.update_ngoc(-rubyCost);
        }

        // Xóa nguyên liệu
        for (int[] mat : materials) {
            int itemId = mat[0];
            int quantity = mat[1];
            p.item.remove_item47(4, itemId, quantity);
        }

        return true;
    }

    private static void craftBanhTrungThu(Player p) throws IOException {
        int[][] materials = {
                {EventTrungThu.ITEM_BOT_MI, 5},
                {EventTrungThu.ITEM_DUONG, 3}
        };
        int beriCost = 500_000;

        if (checkAndRemoveMaterials(p, materials, beriCost, 0)) {
            p.item.add_item_bag47(4, EventTrungThu.ITEM_BANH_TRUNG_THU, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Bánh Trung Thu!");
        }
    }

    private static void craftBanhDauXanh(Player p) throws IOException {
        int[][] materials = {
                {EventTrungThu.ITEM_BOT_MI, 5},
                {EventTrungThu.ITEM_DUONG, 3},
                {EventTrungThu.ITEM_TRUNG_MUOI, 1}
        };
        int beriCost = 1_000_000;

        if (checkAndRemoveMaterials(p, materials, beriCost, 0)) {
            p.item.add_item_bag47(4, EventTrungThu.ITEM_BANH_DAU_XANH, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Bánh Đậu Xanh!");
        }
    }

    private static void craftBanhTrungMuoi(Player p) throws IOException {
        int[][] materials = {
                {EventTrungThu.ITEM_BOT_MI, 5},
                {EventTrungThu.ITEM_DUONG, 3},
                {EventTrungThu.ITEM_TRUNG_MUOI, 2}
        };
        int beriCost = 15_000_000;

        if (checkAndRemoveMaterials(p, materials, beriCost, 0)) {
            p.item.add_item_bag47(4, EventTrungThu.ITEM_BANH_TRUNG_MUOI, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Bánh Trứng Muối!");
        }
    }

    private static void craftBanhHatSen(Player p) throws IOException {
        int[][] materials = {
                {EventTrungThu.ITEM_BOT_MI, 5},
                {EventTrungThu.ITEM_DUONG, 3},
                {EventTrungThu.ITEM_TRUNG_MUOI, 3}
        };
        int beriCost = 2_000_000;

        if (checkAndRemoveMaterials(p, materials, beriCost, 0)) {
            p.item.add_item_bag47(4, EventTrungThu.ITEM_BANH_HAT_SEN, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Bánh Hạt Sen!");
        }
    }

    private static void craftDenKeoQuan(Player p) throws IOException {
        int[][] materials = {
                {EventTrungThu.ITEM_DEN_ONG_SAO, 3}
        };
        int beriCost = 2_000_000;

        if (checkAndRemoveMaterials(p, materials, beriCost, 0)) {
            p.item.add_item_bag47(4, EventTrungThu.ITEM_DEN_KEO_QUAN, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Đèn Kéo Quân!");
        }
    }

    private static void craftHopBanh(Player p) throws IOException {
        int[][] materials = {
                {EventTrungThu.ITEM_BANH_TRUNG_THU, 1},
                {EventTrungThu.ITEM_BANH_DAU_XANH, 1},
                {EventTrungThu.ITEM_BANH_TRUNG_MUOI, 1},
                {EventTrungThu.ITEM_BANH_HAT_SEN, 1}
        };
        int beriCost = 2_000_000;
        int rubyCost = 50;

        if (checkAndRemoveMaterials(p, materials, beriCost, rubyCost)) {
            p.item.add_item_bag47(4, EventTrungThu.ITEM_HOP_BANH, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Hộp Bánh Trung Thu!");
        }
    }

    private static void craftHopBanhThuongHang(Player p) throws IOException {
        int[][] materials = {
                {EventTrungThu.ITEM_HOP_BANH, 1},
                {EventTrungThu.ITEM_GIAY_GOI_QUA, 1}
        };
        int beriCost = 2_000_000;
        int rubyCost = 100;

        if (checkAndRemoveMaterials(p, materials, beriCost, rubyCost)) {
            p.item.add_item_bag47(4, EventTrungThu.ITEM_HOP_BANH_THUONG_HANG, 1);
            p.item.update_Inventory(-1, false);
            p.update_money();
            Service.send_box_ThongBao_OK(p, "Ghép thành công! Bạn nhận được 1 Hộp Bánh Thượng Hạng!");
        }
    }
}
