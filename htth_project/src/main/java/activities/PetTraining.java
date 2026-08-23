package activities;

import java.io.IOException;
import java.util.List;

import client.MyPet;
import client.Pet;
import client.Player;
import core.MenuController;
import core.Service;
import map.Map;
import map.Vgo;
import template.Option;

/**
 * Quản lý tính năng Nâng Cấp Pet và Map Huấn Luyện Pet (Online AFK)
 * 
 * @author HTTH Dev
 */
public class PetTraining {

    public static final int MAP_TRAIN_PET_ID = 2028; // Map Đảo Huấn Luyện Thú Cưng (ID riêng biệt tránh trùng Map 2000
                                                     // Haki)
    public static final int NPC_HUAN_LUYEN_SU = -999; // ID NPC Huấn Luyện Sư Pet

    /**
     * Chuyển người chơi đến map đích
     */
    public static void teleportToMap(Player p, int mapId, int x, int y) throws IOException {
        Map[] mapArr = Map.get_map_by_id(mapId);
        if (mapArr != null && mapArr.length > 0) {
            Vgo vgo = new Vgo();
            vgo.map_go = mapArr;
            vgo.xnew = (short) x;
            vgo.ynew = (short) y;
            p.goto_map(vgo);
        } else {
            Service.send_box_ThongBao_OK(p, "Bản đồ hiện không khả dụng!");
        }
    }

    /**
     * Menu chính của NPC Huấn Luyện Sư Pet (tại Đảo Huấn Luyện)
     */
    public static void sendMainMenu(Player p, short idNpc) throws IOException {
        String[] menu = new String[] {
                "Đột Phá Cấp Độ Pet",
                "Tẩy Chỉ Số",
                "Về Làng Cối Xay Gió",
                "Hướng Dẫn"
        };
        MenuController.send_dynamic_menu(p, idNpc, "Huấn Luyện Sư Pet", menu, null);
    }

    /**
     * Xử lý lựa chọn từ menu NPC
     */
    public static void handleMenu(Player p, short idNpc, byte index) throws IOException {
        switch (index) {
            case 0:
                handleLevelUp(p);
                break;
            case 1:
                handleReRoll(p);
                break;
            case 2:
                // Về làng Cối Xay Gió
                teleportToMap(p, 1, 100, 200);
                break;
            case 3:
                sendHelpDialog(p);
                break;
        }
    }

    /**
     * Đột phá thăng cấp Pet và hiển thị bảng thông tin đột phá thống nhất
     */
    public static void handleLevelUp(Player p) throws IOException {
        MyPet activePet = p.get_pet();
        if (activePet == null) {
            Service.send_box_ThongBao_OK(p,
                    "Bạn chưa trang bị Thú Cưng nào!\nHãy mở Túi Đồ Pet và trang bị Pet trước khi huấn luyện.");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.append("=== HUẤN LUYỆN THÚ CƯNG ===\n");
        sb.append("• Thú cưng: ").append(activePet.template.name).append("\n");

        if (activePet.isMaxLevel()) {
            sb.append("• Cấp độ: Lv.").append(activePet.level).append("/3 (Cấp tối đa)\n");
            sb.append("• Trạng thái: Đã đạt cấp độ tối đa!\n\n");
        } else {
            if (activePet.canLevelUp()) {
                // Đủ EXP -> Tự động thăng cấp
                Pet.levelUpPet(p, activePet);
                p.update_info_to_all();
                sb.append("• Cấp độ: Lv.").append(activePet.level).append("/3\n");
                sb.append("• Trạng thái: 🎉 ĐỘT PHÁ THÀNH CÔNG!\n\n");
            } else {
                int needExp = MyPet.getMaxExp(activePet.level);
                int remainExp = needExp - activePet.exp;
                sb.append("• Cấp độ: Lv.").append(activePet.level).append("/3 (EXP: ").append(activePet.exp).append("/").append(needExp).append(")\n");
                sb.append("• Trạng thái: Chưa đủ EXP (Cần thêm ").append(remainExp).append(" EXP)\n\n");
            }
        }

        // Danh sách chỉ số Huấn Luyện
        sb.append("🌟 Chỉ số Huấn Luyện:\n");
        if (activePet.extra_op != null && !activePet.extra_op.isEmpty()) {
            for (int i = 0; i < activePet.extra_op.size(); i++) {
                Option op = activePet.extra_op.get(i);
                sb.append("• Lv.").append(i + 1).append(": ").append(Pet.formatOptionString(op)).append("\n");
            }
        } else {
            sb.append("• Chưa có chỉ số huấn luyện\n");
        }

        Service.send_box_ThongBao_OK(p, sb.toString());
    }

    public static final short YESNO_ID_CONFIRM_RESET_PET = 20281; // ID hộp thoại xác nhận tẩy chỉ số pet

    /**
     * Tẩy chỉ số: Mở hộp thoại xác nhận với chi phí 1.000 Ruby
     */
    public static void handleReRoll(Player p) throws IOException {
        MyPet activePet = p.get_pet();
        if (activePet == null) {
            Service.send_box_ThongBao_OK(p,
                    "Bạn chưa trang bị Thú Cưng nào!\nHãy mở Túi Đồ Pet và trang bị Pet cần tẩy trước.");
            return;
        }

        if (activePet.level <= 0 && (activePet.extra_op == null || activePet.extra_op.isEmpty())) {
            Service.send_box_ThongBao_OK(p, "Thú cưng [" + activePet.template.name
                    + "] hiện chưa có chỉ số huấn luyện nào để tẩy!");
            return;
        }

        int cost = 1000;
        if (p.get_ngoc() < cost) {
            Service.send_box_ThongBao_OK(p, "Bạn không đủ " + cost + " Ruby để tẩy chỉ số!\n"
                    + "• Ruby hiện có: " + p.get_ngoc());
            return;
        }

        Service.send_box_yesno(p, YESNO_ID_CONFIRM_RESET_PET, "Tẩy Chỉ Số Pet",
                "Bạn có chắc chắn muốn tẩy toàn bộ chỉ số của thú cưng [" + activePet.template.name + "] không?\n"
                        + "• Cấp độ và EXP sẽ reset về 0.\n"
                        + "• Phí tẩy: 1.000 Ruby\n"
                        + "• Ruby hiện có: " + p.get_ngoc(),
                new String[] { "Đồng ý", "Hủy" },
                new byte[] { -1, -1 });
    }

    /**
     * Xử lý xác nhận tẩy chỉ số sau khi người chơi bấm Đồng ý
     */
    public static void processConfirmResetPet(Player p, byte value) throws IOException {
        if (value != 0) {
            return; // Người chơi chọn Hủy
        }

        MyPet activePet = p.get_pet();
        if (activePet == null) {
            Service.send_box_ThongBao_OK(p, "Không tìm thấy Thú Cưng đang trang bị!");
            return;
        }

        if (activePet.level <= 0 && (activePet.extra_op == null || activePet.extra_op.isEmpty())) {
            Service.send_box_ThongBao_OK(p, "Thú cưng [" + activePet.template.name
                    + "] hiện không có chỉ số huấn luyện nào để tẩy!");
            return;
        }

        int cost = 1000;
        if (p.get_ngoc() < cost) {
            Service.send_box_ThongBao_OK(p, "Bạn không đủ " + cost + " Ruby để tẩy chỉ số!\n"
                    + "• Ruby hiện có: " + p.get_ngoc());
            return;
        }

        p.update_ngoc(-cost);
        p.update_money();
        Pet.resetPetStats(activePet);
        p.update_info_to_all();

        Service.send_box_ThongBao_OK(p,
                "Đã tẩy thành công toàn bộ chỉ số của thú cưng [" + activePet.template.name + "]!");
    }

    /**
     * Cập nhật vòng lặp huấn luyện online cho người chơi trong Map
     */
    public static void updateMapTraining(Map map) {
        if (map == null || map.players == null || map.players.isEmpty()) {
            return;
        }

        for (int i = 0; i < map.players.size(); i++) {
            Player p = map.players.get(i);
            if (p != null && !p.isdie && p.conn != null) {
                MyPet pet = p.get_pet();
                if (pet != null && !pet.isMaxLevel() && p.type_pk == 3) {
                    int oldExp = pet.exp;
                    pet.addExp(5); // Cộng 5 EXP mỗi chu kỳ online khi bật Cờ Đen
                    try {
                        if (pet.exp > oldExp && pet.canLevelUp()) {
                            Service.send_box_ThongBao_OK(p, "🎉 Thú cưng [" + pet.template.name
                                    + "] đã tích lũy đủ EXP Huấn Luyện!\nHãy gặp Huấn Luyện Sư để Đột Phá Cấp Độ mới!");
                        }
                    } catch (Exception ignored) {
                    }
                }
            }
        }
    }

    /**
     * Hướng dẫn tính năng
     */
    public static void sendHelpDialog(Player p) throws IOException {
        String guide = "=== HƯỚNG DẪN HUẤN LUYỆN PET ===\n"
                + "1. CẤP ĐỘ PET:\n"
                + "• Pet có tối đa 3 Cấp Độ (Lv.1 -> Lv.3).\n"
                + "  - Lv.0 -> Lv.1: Cần 10 EXP (10 quái)\n"
                + "  - Lv.1 -> Lv.2: Cần 50 EXP (50 quái)\n"
                + "  - Lv.2 -> Lv.3: Cần 100 EXP (100 quái)\n"
                + "• Mỗi cấp độ mở khóa sẽ ngẫu nhiên (random) thêm 1 dòng thuộc tính trong danh sách:\n"
                + "  - Nhóm Tiềm năng (+1 đến +10): T/n sức mạnh, T/n phòng thủ, T/n thể lực, T/n tinh thần, T/n nhanh nhẹn.\n"
                + "  - Nhóm Chiến đấu (+1% đến +10%): Chí mạng, Né tránh, Xuyên giáp, Phản đòn, Miễn thương, Giảm miễn thương.\n"
                + "• Max Lv.3 sẽ sở hữu tối đa 3 dòng chỉ số huấn luyện cực mạnh.\n\n"
                + "2. CÁCH TÍCH LŨY EXP:\n"
                + "• Lưu ý QUAN TRỌNG: Người chơi phải BẬT CỜ ĐEN mới nhận được EXP cho Pet.\n"
                + "• Trang bị Pet, bật Cờ Đen và treo máy Online hoặc đánh quái tại Đảo Huấn Luyện để nhận EXP cho Pet.\n\n"
                + "3. TẨY CHỈ SỐ:\n"
                + "• Xóa toàn bộ chỉ số huấn luyện và cấp độ của Pet đang trang bị về ban đầu để luyện lại từ đầu.";
        Service.send_box_ThongBao_OK(p, guide);
    }
}
