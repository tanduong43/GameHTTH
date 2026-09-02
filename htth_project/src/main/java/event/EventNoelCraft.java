package event;

import java.io.IOException;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.ItemTemplate4;

/**
 * Xử lý chế tạo & đổi quà Sự Kiện Noel (Giáng Sinh) tại NPC Sự Kiện (-100)
 */
public class EventNoelCraft {

    /**
     * Hiển thị bảng hướng dẫn chế tạo chi tiết sự kiện Noel
     */
    public static void showCraftHelp(Player p) throws IOException {
        Message m = new Message(-50);
        m.writer().writeByte(1);
        m.writer().writeByte(0);
        m.writer().writeUTF("🎄 SỰ KIỆN NOEL - GIÁNG SINH AN LÀNH\n\n"
                + "📍 CÔNG THỨC CHẾ TẠO QUÀ:\n"
                + "1. ⛄ Đắp Người Tuyết: 10 Nắm Tuyết + 2 Cúc Áo + 1 Nón Noel + 500.000 Beri\n"
                + "2. 💌 Thiệp Giáng Sinh: 5 Nắm Tuyết + 2 Cúc Áo + 1 Kẹo Noel + 500.000 Beri\n"
                + "3. 🎁 Hộp Quà Noel: 2 Chuông Vàng + 2 Vớ Đỏ + 1 Giấy Gói Quà + 1.000.000 Beri\n"
                + "4. 👑 Hộp Quà Giáng Sinh VIP: 1 Hộp Quà Noel + 1 Gấu Bông + 1 Ngôi Sao + 2.000.000 Beri + 50 Ruby\n"
                + "5. 👘 Rương Trang Phục Noel: 10 Vé Đổi TT Noel + 5 Ngôi Sao + 5.000.000 Beri + 100 Ruby\n"
                + "6. 🦌 Rương Pet Noel: 10 Vé Noel + 1 Hộp Quà VIP + 5.000.000 Beri + 100 Ruby\n\n"
                + "📍 NGUỒN NGUYÊN LIỆU:\n"
                + "• Nắm Tuyết, Bóng Tuyết, Kẹo Noel: Đánh quái dã ngoại ±10 cấp\n"
                + "• Cúc Áo, Vớ Noel: Làm Nhiệm vụ lặp hằng ngày\n"
                + "• Chuông Noel, Giấy Gói Quà: Nhiệm vụ Băng & Phó bản Băng\n"
                + "• Nón Noel, Gấu Bông: Phó bản Nami / Mr.3 / Vệ Thần Wipper\n"
                + "• Ngôi Sao Giáng Sinh, Vé Noel: Đấu Trường PvP / Lôi Đài / Boss Truy Nã\n"
                + "• Boss Quái Vật Tuyết (12h, 18h, 20h, 22h): Thưởng Last Hit Rương Pet Noel + Hộp Quà VIP!");
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Xử lý chế tạo theo lựa chọn menu
     */
    public static void processCraft(Player p, int choice) throws IOException {
        if (!EventNoel.isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Noel chưa được kích hoạt!");
            return;
        }

        switch (choice) {
            case 1:
                craftNguoiTuyet(p);
                break;
            case 2:
                craftThiepNoel(p);
                break;
            case 3:
                craftHopQuaNoel(p);
                break;
            case 4:
                craftHopQuaGiangSinhVIP(p);
                break;
            case 5:
                craftRuongTrangPhucNoel(p);
                break;
            case 6:
                craftRuongPetNoel(p);
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
            }
            return false;
        }

        // Kiểm tra Ruby
        if (rubyCost > 0 && p.get_ngoc() < rubyCost) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ " + rubyCost + " Ruby!");
            } catch (IOException e) {
            }
            return false;
        }

        // Kiểm tra đủ nguyên liệu
        for (int[] mat : materials) {
            int itemId = mat[0];
            int requiredAmount = mat[1];
            int currentAmount = p.item.total_item_bag_by_id(4, itemId);
            if (currentAmount < requiredAmount) {
                ItemTemplate4 it = ItemTemplate4.get_it_by_id(itemId);
                String itemName = (it != null) ? it.name : ("Item " + itemId);
                try {
                    Service.send_box_ThongBao_OK(p, String.format("Bạn không đủ nguyên liệu:\nCần %d %s (Hiện có: %d)",
                            requiredAmount, itemName, currentAmount));
                } catch (IOException e) {
                }
                return false;
            }
        }

        // Trừ nguyên liệu
        for (int[] mat : materials) {
            int itemId = mat[0];
            int requiredAmount = mat[1];
            p.item.remove_item47(4, itemId, requiredAmount);
        }

        // Trừ phí
        if (beriCost > 0) {
            p.update_vang(-beriCost);
        }
        if (rubyCost > 0) {
            p.update_ngoc(-rubyCost);
        }

        return true;
    }

    /**
     * 1. Đắp Người Tuyết: 10 Nắm Tuyết (341) + 2 Cúc Áo (342) + 1 Nón Noel (347) + 500.000 Beri
     */
    private static void craftNguoiTuyet(Player p) throws IOException {
        int[][] mats = {
            { EventNoel.ITEM_NAM_TUYET, 10 },
            { EventNoel.ITEM_CUC_AO, 2 },
            { EventNoel.ITEM_NON_NOEL, 1 }
        };
        int beri = 500_000;

        if (checkAndRemoveMaterials(p, mats, beri, 0)) {
            p.item.add_item_bag47(4, EventNoel.ITEM_THIEP_NOEL, 1);
            boolean gotExtraBox = Util.random(100) < 50;
            if (gotExtraBox) {
                p.item.add_item_bag47(4, EventNoel.ITEM_HOP_QUA_NOEL, 1);
            }
            p.item.update_Inventory(-1, false);

            String msg = "⛄ Đắp Người Tuyết thành công!\nNhận: 1 Thiệp Giáng Sinh" + (gotExtraBox ? "\n+1 Hộp Quà Noel (May mắn!)" : "");
            Service.send_box_ThongBao_OK(p, msg);
        }
    }

    /**
     * 2. Thiệp Giáng Sinh: 5 Nắm Tuyết (341) + 2 Cúc Áo (342) + 1 Kẹo Noel (489) + 500.000 Beri
     */
    private static void craftThiepNoel(Player p) throws IOException {
        int[][] mats = {
            { EventNoel.ITEM_NAM_TUYET, 5 },
            { EventNoel.ITEM_CUC_AO, 2 },
            { EventNoel.ITEM_KEO_NOEL, 1 }
        };
        int beri = 500_000;

        if (checkAndRemoveMaterials(p, mats, beri, 0)) {
            p.item.add_item_bag47(4, EventNoel.ITEM_THIEP_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "💌 Làm thành công 1 Thiệp Giáng Sinh!");
        }
    }

    /**
     * 3. Hộp Quà Noel: 2 Chuông (486) + 2 Vớ Đỏ (487) + 1 Giấy Gói Quà (575) + 1.000.000 Beri
     */
    private static void craftHopQuaNoel(Player p) throws IOException {
        int[][] mats = {
            { EventNoel.ITEM_CHUONG_NOEL, 2 },
            { EventNoel.ITEM_VO_NOEL, 2 },
            { EventNoel.ITEM_GIAY_GOI_QUA, 1 }
        };
        int beri = 1_000_000;

        if (checkAndRemoveMaterials(p, mats, beri, 0)) {
            p.item.add_item_bag47(4, EventNoel.ITEM_HOP_QUA_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎁 Gói thành công 1 Hộp Quà Noel (+1 Điểm BXH khi mở)!");
        }
    }

    /**
     * 4. Hộp Quà Giáng Sinh VIP: 1 Hộp Quà Noel (227) + 1 Gấu Bông (590) + 1 Ngôi Sao (488) + 2M Beri + 50 Ruby
     */
    private static void craftHopQuaGiangSinhVIP(Player p) throws IOException {
        int[][] mats = {
            { EventNoel.ITEM_HOP_QUA_NOEL, 1 },
            { EventNoel.ITEM_GAU_BONG, 1 },
            { EventNoel.ITEM_NGOI_SAO_NOEL, 1 }
        };
        int beri = 2_000_000;
        int ruby = 50;

        if (checkAndRemoveMaterials(p, mats, beri, ruby)) {
            p.item.add_item_bag47(4, EventNoel.ITEM_HOP_QUA_GIANG_SINH_VIP, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "👑 Chế tạo thành công 1 Hộp Quà Giáng Sinh VIP (+5 Điểm BXH khi mở)!");
        }
    }

    /**
     * 5. Rương Trang Phục Noel: 10 Vé Đổi TT Noel (229) + 5 Ngôi Sao (488) + 5M Beri + 100 Ruby
     */
    private static void craftRuongTrangPhucNoel(Player p) throws IOException {
        int[][] mats = {
            { EventNoel.ITEM_VE_DOI_TT_NOEL, 10 },
            { EventNoel.ITEM_NGOI_SAO_NOEL, 5 }
        };
        int beri = 5_000_000;
        int ruby = 100;

        if (checkAndRemoveMaterials(p, mats, beri, ruby)) {
            p.item.add_item_bag47(4, EventNoel.ITEM_RUONG_TT_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "👘 Đổi thành công 1 Rương Trang Phục Noel!");
        }
    }

    /**
     * 6. Rương Pet Noel: 10 Vé Noel (230) + 1 Hộp Quà Giáng Sinh VIP (492) + 5M Beri + 100 Ruby
     */
    private static void craftRuongPetNoel(Player p) throws IOException {
        int[][] mats = {
            { EventNoel.ITEM_VE_NOEL, 10 },
            { EventNoel.ITEM_HOP_QUA_GIANG_SINH_VIP, 1 }
        };
        int beri = 5_000_000;
        int ruby = 100;

        if (checkAndRemoveMaterials(p, mats, beri, ruby)) {
            p.item.add_item_bag47(4, EventNoel.ITEM_RUONG_PET_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🦌 Đổi thành công 1 Rương Pet Noel!");
        }
    }
}
