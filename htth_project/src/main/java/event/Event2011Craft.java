package event;

import java.io.IOException;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.ItemTemplate4;

/**
 * Xử lý chế tạo & ghép quà Sự Kiện 20/11 - Ngày Nhà Giáo Việt Nam
 */
public class Event2011Craft {

    /**
     * Hiển thị bảng hướng dẫn chế tạo chi tiết sự kiện 20/11
     */
    public static void showCraftHelp(Player p) throws IOException {
        Message m = new Message(-50);
        m.writer().writeByte(1);
        m.writer().writeByte(0);
        m.writer().writeUTF("🎓 SỰ KIỆN 20/11 - TRI ÂN THẦY CÔ\n\n"
                + "📍 CÔNG THỨC CHẾ TẠO QUÀ:\n"
                + "1. 💮 Bông Hoa Điểm 10: 5 Trang Giấy + 2 Lọ Mực + 500.000 Beri\n"
                + "2. 💌 Thiệp Tri Ân 20/11: 3 Trang Giấy + 2 Lọ Mực + 2 Giấy Đỏ + 1.000.000 Beri\n"
                + "3. 💐 Lẵng Hoa Tri Ân: 10 Cánh Hoa Phượng + 3 Giấy Đỏ + 1 Giấy Gói Quà + 1.000.000 Beri\n"
                + "4. 🎁 Hộp Quà Sơ Cấp: 2 Điểm 10 + 1 Thiệp Tri Ân + 1 Lẵng Hoa + 2.000.000 Beri\n"
                + "5. 🏆 Hộp Quà Cao Cấp: 1 Hộp Quà Sơ Cấp + 1 Gấu Bông + 1 Sách Công Thức + 2.000.000 Beri + 50 Ruby\n"
                + "6. 👑 Hộp Quà Tôn Sư Trọng Đạo: 1 Hộp Quà Cao Cấp + 1 Bản Nhạc + 1 Chứng Nhận Sư Phụ + 3.000.000 Beri + 100 Ruby\n\n"
                + "📍 NGUỒN NGUYÊN LIỆU:\n"
                + "• Trang Giấy, Cánh Hoa Phượng: Đánh quái thường ±10 cấp\n"
                + "• Lọ Mực, Giấy Đỏ: Làm Nhiệm vụ lặp hàng ngày\n"
                + "• Sách Công Thức, Cánh Hoa Phượng: Nhiệm vụ Băng & Phó bản Băng\n"
                + "• Giấy Gói Quà, Gấu Bông: Phó bản Nami / Mr.3 / Vận buôn\n"
                + "• Bản Nhạc Kích Lệ: Đấu Trường / PvP / Boss Truy Nã\n"
                + "• Chứng Nhận Sư Phụ: Săn Boss Lân Sư Tử (12h, 18h, 20h, 22h)");
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Xử lý chế tạo theo lựa chọn
     */
    public static void processCraft(Player p, int choice) throws IOException {
        if (!Event2011.isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện 20/11 chưa được kích hoạt!");
            return;
        }

        switch (choice) {
            case 1:
                craftDiem10(p);
                break;
            case 2:
                craftThiepTriAn(p);
                break;
            case 3:
                craftLangHoa(p);
                break;
            case 4:
                craftHopQuaSoCap(p);
                break;
            case 5:
                craftHopQuaCaoCap(p);
                break;
            case 6:
                craftHopQuaDacBiet(p);
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
                // ignore
            }
            return false;
        }

        // Kiểm tra Ruby
        if (rubyCost > 0 && p.get_ngoc() < rubyCost) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ " + rubyCost + " Ruby!");
            } catch (IOException e) {
                // ignore
            }
            return false;
        }

        // Kiểm tra đủ nguyên liệu
        for (int[] mat : materials) {
            int itemId = mat[0];
            int quantity = mat[1];
            if (p.item.total_item_bag_by_id(4, itemId) < quantity) {
                ItemTemplate4 template = ItemTemplate4.get_it_by_id(itemId);
                String itemName = template != null ? template.name : ("Item " + itemId);
                try {
                    Service.send_box_ThongBao_OK(p, "Bạn không đủ " + quantity + " " + itemName + " (Hiện có: "
                            + p.item.total_item_bag_by_id(4, itemId) + ")!");
                } catch (IOException e) {
                    // ignore
                }
                return false;
            }
        }

        // Trừ Beri & Ruby
        p.update_vang(-beriCost);
        if (rubyCost > 0) {
            p.update_ngoc(-rubyCost);
        }

        // Trừ nguyên liệu
        for (int[] mat : materials) {
            p.item.remove_item47(4, mat[0], mat[1]);
        }

        return true;
    }

    /**
     * Ghép Điểm 10: 5 Trang Giấy + 2 Lọ Mực + 500.000 Beri
     */
    private static void craftDiem10(Player p) throws IOException {
        int[][] mats = {
            { Event2011.ITEM_TRANG_GIAY, 5 },
            { Event2011.ITEM_LO_MUC, 2 }
        };
        if (checkAndRemoveMaterials(p, mats, 500_000, 0)) {
            p.item.add_item_bag47(4, Event2011.ITEM_DIEM_10, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎉 Ghép thành công 1 Bông Hoa Điểm 10!");
        }
    }

    /**
     * Ghép Thiệp Tri Ân: 3 Trang Giấy + 2 Lọ Mực + 2 Giấy Đỏ + 1.000.000 Beri
     */
    private static void craftThiepTriAn(Player p) throws IOException {
        int[][] mats = {
            { Event2011.ITEM_TRANG_GIAY, 3 },
            { Event2011.ITEM_LO_MUC, 2 },
            { Event2011.ITEM_GIAY_DO, 2 }
        };
        if (checkAndRemoveMaterials(p, mats, 1_000_000, 0)) {
            p.item.add_item_bag47(4, Event2011.ITEM_THIEP_TRI_AN, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎉 Ghép thành công 1 Thiệp Tri Ân 20/11!");
        }
    }

    /**
     * Ghép Lẵng Hoa Tri Ân: 10 Cánh Hoa Phượng + 3 Giấy Đỏ + 1 Giấy Gói Quà + 1.000.000 Beri
     */
    private static void craftLangHoa(Player p) throws IOException {
        int[][] mats = {
            { Event2011.ITEM_CANH_HOA_PHUONG, 10 },
            { Event2011.ITEM_GIAY_DO, 3 },
            { Event2011.ITEM_GIAY_GOI_QUA, 1 }
        };
        if (checkAndRemoveMaterials(p, mats, 1_000_000, 0)) {
            p.item.add_item_bag47(4, Event2011.ITEM_LANG_HOA, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎉 Ghép thành công 1 Lẵng Hoa Tri Ân!");
        }
    }

    /**
     * Ghép Hộp Quà Sơ Cấp: 2 Điểm 10 + 1 Thiệp Tri Ân + 1 Lẵng Hoa + 2.000.000 Beri
     */
    private static void craftHopQuaSoCap(Player p) throws IOException {
        int[][] mats = {
            { Event2011.ITEM_DIEM_10, 2 },
            { Event2011.ITEM_THIEP_TRI_AN, 1 },
            { Event2011.ITEM_LANG_HOA, 1 }
        };
        if (checkAndRemoveMaterials(p, mats, 2_000_000, 0)) {
            p.item.add_item_bag47(4, Event2011.ITEM_HOP_QUA_SO_CAP, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎉 Chế tạo thành công 1 Hộp Quà Tri Ân Sơ Cấp!");
        }
    }

    /**
     * Ghép Hộp Quà Cao Cấp: 1 Hộp Quà Sơ Cấp + 1 Gấu Bông + 1 Sách Công Thức + 2.000.000 Beri + 50 Ruby
     */
    private static void craftHopQuaCaoCap(Player p) throws IOException {
        int[][] mats = {
            { Event2011.ITEM_HOP_QUA_SO_CAP, 1 },
            { Event2011.ITEM_GAU_BONG, 1 },
            { Event2011.ITEM_SACH_CONG_THUC, 1 }
        };
        if (checkAndRemoveMaterials(p, mats, 2_000_000, 50)) {
            p.item.add_item_bag47(4, Event2011.ITEM_HOP_QUA_CAO_CAP, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎉 Chế tạo thành công 1 Hộp Quà Tri Ân Cao Cấp!");
        }
    }

    /**
     * Ghép Hộp Quà Tôn Sư Trọng Đạo (Đặc Biệt): 1 Hộp Quà Cao Cấp + 1 Bản Nhạc + 1 Chứng Nhận Sư Phụ + 3.000.000 Beri + 100 Ruby
     */
    private static void craftHopQuaDacBiet(Player p) throws IOException {
        int[][] mats = {
            { Event2011.ITEM_HOP_QUA_CAO_CAP, 1 },
            { Event2011.ITEM_BAN_NHAC, 1 },
            { Event2011.ITEM_CHUNG_NHAN_SU_PHU, 1 }
        };
        if (checkAndRemoveMaterials(p, mats, 3_000_000, 100)) {
            p.item.add_item_bag47(4, Event2011.ITEM_HOP_QUA_DAC_BIET, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎉 Chế tạo thành công 1 Hộp Quà Tôn Sư Trọng Đạo (Đặc Biệt)!");
        }
    }
}
