package activities;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import client.Player;
import core.Manager;
import core.MenuController;
import core.Service;
import core.Util;
import database.SQL;
import io.SessionManager;

/**
 * Quản lý tính năng NPC Ngân Hàng:
 * - Nạp tiền (Tạo đơn nạp chờ duyệt)
 * - Đổi Coin (Quy đổi Coin sang Ruby, Extol, Beri)
 * - Duyệt nạp (Chỉ Admin: Xem danh sách đơn nạp và duyệt cộng Coin/Tích nạp/VIP)
 * - Thông tin (Tỷ lệ quy đổi, EXP nạp, cấp VIP)
 *
 * @author HTTH Dev
 */
public class Bank {

    public static final short NPC_ID_BANK = -205;
    public static final short INPUT_ID_BANK_DEPOSIT = 20501;
    public static final short INPUT_ID_BANK_EXCHANGE_COIN = 20502;
    public static final short YESNO_ID_BANK_EXCHANGE_COIN = 20503;
    public static final short YESNO_ID_BANK_APPROVE_DEPOSIT = 20504;
    public static final short MENU_ID_ADMIN_DUYET_NAP = 20505;
    public static final short MENU_ID_BANK_INFO = 20506;

    public static class PendingRecharge {
        public int id;
        public String username;
        public int amount;
        public String createdAt;

        public PendingRecharge(int id, String username, int amount, String createdAt) {
            this.id = id;
            this.username = username;
            this.amount = amount;
            this.createdAt = createdAt;
        }
    }

    private static final Map<String, List<PendingRecharge>> PENDING_MAP = new ConcurrentHashMap<>();
    private static final Map<String, PendingRecharge> SELECTED_APPROVE_MAP = new ConcurrentHashMap<>();

    /**
     * Kiểm tra xem người chơi có quyền Admin không
     */
    public static boolean isAdmin(Player p) {
        if (p == null || p.conn == null || p.conn.user == null) {
            return false;
        }
        return "admin".equalsIgnoreCase(p.conn.user);
    }

    /**
     * Gửi Menu chính của NPC Ngân Hàng
     */
    public static void sendMainMenu(Player p, int npcId) throws IOException {
        if (p.isdie) {
            return;
        }
        if (isAdmin(p)) {
            MenuController.send_dynamic_menu(p, npcId, "Ngân Hàng",
                    new String[] { "Nạp tiền", "Đổi Coin", "Xem Coin", "Duyệt nạp", "Thông tin" },
                    new short[] { 132, 140, 140, 161, 148 });
        } else {
            MenuController.send_dynamic_menu(p, npcId, "Ngân Hàng",
                    new String[] { "Nạp tiền", "Đổi Coin", "Xem Coin", "Thông tin" },
                    new short[] { 132, 140, 140, 148 });
        }
    }

    /**
     * Xử lý lựa chọn từ Menu chính
     */
    public static void handleMenu(Player p, int npcId, byte index) throws IOException {
        if (isAdmin(p)) {
            switch (index) {
                case 0:
                    requestDepositInput(p);
                    break;
                case 1:
                    requestExchangeCoinInput(p);
                    break;
                case 2:
                    showCoin(p);
                    break;
                case 3:
                    showPendingList(p);
                    break;
                case 4:
                    showInfoMenu(p, npcId);
                    break;
            }
        } else {
            switch (index) {
                case 0:
                    requestDepositInput(p);
                    break;
                case 1:
                    requestExchangeCoinInput(p);
                    break;
                case 2:
                    showCoin(p);
                    break;
                case 3:
                    showInfoMenu(p, npcId);
                    break;
            }
        }
    }

    /**
     * Hiển thị số Coin và thông tin tài khoản hiện tại của người chơi
     */
    public static void showCoin(Player p) throws IOException {
        // Đồng bộ số coin và tích nạp mới nhất từ CSDL
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.prepareStatement("SELECT `coin`, `tichnap`, `vip` FROM `accounts` WHERE BINARY `user` = ? LIMIT 1;");
            ps.setString(1, p.conn.user);
            rs = ps.executeQuery();
            if (rs.next()) {
                p.conn.coin = rs.getInt("coin");
                p.conn.tichnap = rs.getInt("tichnap");
                p.conn.vip = rs.getInt("vip");
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }

        Service.send_box_ThongBao_OK(p, "💰 THÔNG TIN TÀI KHOẢN 💰\n\n"
                + "• Tên tài khoản: " + p.conn.user + "\n"
                + "• Nhân vật: " + p.name + "\n"
                + "• Số Coin hiện có: " + Util.number_format(p.conn.coin) + " Coin\n"
                + "• Điểm tích nạp: " + Util.number_format(p.conn.tichnap) + " điểm\n"
                + "• Cấp VIP hiện tại: VIP " + p.conn.vip);
    }

    /**
     * Mở form nhập số tiền cần nạp
     */
    public static void requestDepositInput(Player p) throws IOException {
        Service.input_text(p, INPUT_ID_BANK_DEPOSIT, "Nạp Tiền Ngân Hàng",
                new String[] { "Nhập số tiền VNĐ (VD: 50000, 100000)" });
    }

    /**
     * Xử lý khi người chơi submit form nạp tiền
     */
    public static void processDepositInput(Player p, String[] name) throws IOException {
        if (name == null || name.length == 0 || name[0].trim().isEmpty()) {
            Service.send_box_ThongBao_OK(p, "Vui lòng nhập số tiền hợp lệ!");
            return;
        }
        String inputStr = name[0].trim().replace(".", "").replace(",", "").replace(" ", "");
        if (!Util.isnumber(inputStr)) {
            Service.send_box_ThongBao_OK(p, "Số tiền nhập không hợp lệ, vui lòng chỉ nhập chữ số!");
            return;
        }
        long amountLong = Long.parseLong(inputStr);
        if (amountLong < 10_000) {
            Service.send_box_ThongBao_OK(p, "Mức nạp tối thiểu là 10.000 VNĐ!");
            return;
        }
        if (amountLong > 100_000_000) {
            Service.send_box_ThongBao_OK(p, "Số tiền nạp tối đa mỗi lần là 100.000.000 VNĐ!");
            return;
        }
        int amount = (int) amountLong;

        Connection conn = null;
        PreparedStatement psCheck = null;
        PreparedStatement psInsert = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();

            // Kiểm tra số đơn chờ duyệt hiện tại của user để tránh spam
            psCheck = conn.prepareStatement(
                    "SELECT COUNT(*) AS total FROM `recharge_history` WHERE BINARY `username` = ? AND `status` = 0;");
            psCheck.setString(1, p.conn.user);
            rs = psCheck.executeQuery();
            if (rs.next() && rs.getInt("total") >= 5) {
                Service.send_box_ThongBao_OK(p, "Bạn đang có 5 yêu cầu nạp tiền chờ duyệt. Vui lòng đợi Admin xử lý trước khi tạo thêm!");
                return;
            }
            rs.close();
            psCheck.close();

            // Tạo mã giao dịch
            String requestId = "INGAME_" + System.currentTimeMillis();
            String code = String.valueOf((int) ((Math.random() * 899999) + 100000));
            String description = "Nạp " + Util.number_format(amount) + "đ từ nhân vật " + p.name + " (Tài khoản: " + p.conn.user + ")";

            psInsert = conn.prepareStatement(
                    "INSERT INTO `recharge_history` (`username`, `amount`, `real_amount`, `type`, `status`, `request_id`, `code`, `description`, `created_at`) "
                            + "VALUES (?, ?, ?, 'bank', 0, ?, ?, ?, NOW());");
            psInsert.setString(1, p.conn.user);
            psInsert.setInt(2, amount);
            psInsert.setInt(3, amount);
            psInsert.setString(4, requestId);
            psInsert.setString(5, code);
            psInsert.setString(6, description);
            psInsert.executeUpdate();

            Service.send_box_ThongBao_OK(p, "✅ Đã tạo yêu cầu nạp " + Util.number_format(amount) + " VNĐ thành công!\n"
                    + "• Mã GD: " + code + "\n"
                    + "• Quy đổi dự kiến: +" + Util.number_format(amount / 1000) + " Coin\n"
                    + "• Tích nạp dự kiến: +" + Util.number_format(amount) + " điểm\n\n"
                    + "Vui lòng chuyển khoản đúng nội dung và đợi Admin duyệt đơn nạp của bạn nhé!");

            // Thông báo cho các Admin đang online biết có đơn nạp mới
            notifyAdminsNewDeposit(p.name, p.conn.user, amount);

        } catch (SQLException e) {
            e.printStackTrace();
            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra khi tạo yêu cầu nạp tiền, vui lòng thử lại sau!");
        } finally {
            try {
                if (rs != null) rs.close();
                if (psCheck != null) psCheck.close();
                if (psInsert != null) psInsert.close();
                if (conn != null) conn.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Mở form nhập số Coin muốn đổi
     */
    public static void requestExchangeCoinInput(Player p) throws IOException {
        Service.input_text(p, INPUT_ID_BANK_EXCHANGE_COIN, "Đổi Coin (" + Util.number_format(p.conn.coin) + " Coin hiện có)",
                new String[] { "Nhập số Coin muốn đổi (1 Coin = 100 Ruby + 1.000 Extol)" });
    }

    /**
     * Xử lý khi người chơi submit form đổi Coin
     */
    public static void processExchangeCoinInput(Player p, String[] name) throws IOException {
        if (name == null || name.length == 0 || name[0].trim().isEmpty()) {
            Service.send_box_ThongBao_OK(p, "Vui lòng nhập số Coin hợp lệ!");
            return;
        }
        String inputStr = name[0].trim().replace(".", "").replace(",", "").replace(" ", "");
        if (!Util.isnumber(inputStr)) {
            Service.send_box_ThongBao_OK(p, "Số Coin nhập không hợp lệ, vui lòng chỉ nhập chữ số!");
            return;
        }
        long coinLong = Long.parseLong(inputStr);
        if (coinLong <= 0) {
            Service.send_box_ThongBao_OK(p, "Số Coin muốn đổi phải lớn hơn 0!");
            return;
        }
        if (coinLong > p.conn.coin) {
            Service.send_box_ThongBao_OK(p, "Bạn không đủ Coin! Hiện tại bạn chỉ có " + Util.number_format(p.conn.coin) + " Coin.");
            return;
        }
        int coin = (int) coinLong;
        long rubyRec = coin * 100L;
        long extolRec = coin * 1000L;

        p.data_yesno = new int[] { YESNO_ID_BANK_EXCHANGE_COIN, coin };
        Service.send_box_yesno(p, YESNO_ID_BANK_EXCHANGE_COIN, "Xác nhận đổi Coin",
                "Bạn có thật sự muốn đổi " + Util.number_format(coin) + " Coin để nhận "
                        + Util.number_format(rubyRec) + " Ruby và " + Util.number_format(extolRec) + " Extol không?",
                new String[] { "Đồng ý", "Hủy" }, new byte[] { 2, 1 });
    }

    /**
     * Xử lý xác nhận Yes/No đổi Coin
     */
    public static void processConfirmExchangeCoin(Player p, byte value) throws IOException {
        if (value == 0 && p.data_yesno != null && p.data_yesno.length >= 2 && p.data_yesno[0] == YESNO_ID_BANK_EXCHANGE_COIN) {
            int coin = p.data_yesno[1];
            if (p.conn.coin < coin) {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ " + Util.number_format(coin) + " Coin!");
                p.data_yesno = null;
                return;
            }
            if (p.update_coin(-coin)) {
                int rubyRec = coin * 100;
                int extolRec = coin * 1000;
                p.update_ngoc(rubyRec);
                p.update_vnd(extolRec);
                p.update_money();
                Service.send_box_ThongBao_OK(p, "🎉 Bạn đã đổi thành công " + Util.number_format(coin) + " Coin ra "
                        + Util.number_format(rubyRec) + " Ruby và " + Util.number_format(extolRec) + " Extol!");
            }
        }
        p.data_yesno = null;
    }

    /**
     * Hiển thị danh sách các tài khoản nạp tiền đang chờ duyệt cho Admin
     */
    public static void showPendingList(Player adminPlayer) throws IOException {
        if (!isAdmin(adminPlayer)) {
            Service.send_box_ThongBao_OK(adminPlayer, "Bạn không có quyền thực hiện chức năng này!");
            return;
        }

        List<PendingRecharge> list = new ArrayList<>();
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.prepareStatement(
                    "SELECT `id`, `username`, `amount`, `created_at` FROM `recharge_history` WHERE `status` = 0 ORDER BY `id` ASC LIMIT 30;");
            rs = ps.executeQuery();
            while (rs.next()) {
                list.add(new PendingRecharge(
                        rs.getInt("id"),
                        rs.getString("username"),
                        rs.getInt("amount"),
                        rs.getString("created_at")
                ));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }

        if (list.isEmpty()) {
            Service.send_box_ThongBao_OK(adminPlayer, "Hiện tại không có yêu cầu nạp tiền nào đang chờ duyệt!");
            return;
        }

        PENDING_MAP.put(adminPlayer.name, list);

        String[] menuItems = new String[list.size()];
        for (int i = 0; i < list.size(); i++) {
            PendingRecharge item = list.get(i);
            menuItems[i] = (i + 1) + ". " + item.username + " | " + Util.number_format(item.amount) + "đ | [Duyệt]";
        }

        MenuController.send_dynamic_menu(adminPlayer, MENU_ID_ADMIN_DUYET_NAP, "Duyệt Nạp (" + list.size() + " đơn)", menuItems);
    }

    /**
     * Xử lý khi Admin bấm chọn 1 dòng đơn nạp trong danh sách duyệt nạp
     */
    public static void handleSelectPendingRecharge(Player adminPlayer, byte index) throws IOException {
        if (!isAdmin(adminPlayer)) {
            Service.send_box_ThongBao_OK(adminPlayer, "Bạn không có quyền thực hiện chức năng này!");
            return;
        }

        List<PendingRecharge> list = PENDING_MAP.get(adminPlayer.name);
        if (list == null || index < 0 || index >= list.size()) {
            Service.send_box_ThongBao_OK(adminPlayer, "Danh sách yêu cầu đã hết hạn, vui lòng tải lại!");
            return;
        }

        PendingRecharge selected = list.get(index);
        SELECTED_APPROVE_MAP.put(adminPlayer.name, selected);

        adminPlayer.data_yesno = new int[] { YESNO_ID_BANK_APPROVE_DEPOSIT, selected.id, selected.amount };

        int coinGain = selected.amount / 1000;
        String confirmMsg = "Xác nhận duyệt đơn nạp tiền:\n"
                + "• STT: " + (index + 1) + "\n"
                + "• Tài khoản: " + selected.username + "\n"
                + "• Số tiền: " + Util.number_format(selected.amount) + " VNĐ\n"
                + "• Quy đổi: +" + Util.number_format(coinGain) + " Coin\n"
                + "• Tích nạp: +" + Util.number_format(selected.amount) + " điểm\n\n"
                + "Bạn có chắc chắn muốn duyệt nạp cho người chơi này?";

        Service.send_box_yesno(adminPlayer, YESNO_ID_BANK_APPROVE_DEPOSIT, "Xác nhận Duyệt Nạp", confirmMsg,
                new String[] { "Duyệt ngay", "Hủy" }, new byte[] { 2, 1 });
    }

    /**
     * Xử lý xác nhận Yes/No duyệt đơn nạp của Admin
     */
    public static void processConfirmApproveDeposit(Player adminPlayer, byte value) throws IOException {
        if (!isAdmin(adminPlayer)) {
            Service.send_box_ThongBao_OK(adminPlayer, "Bạn không có quyền thực hiện chức năng này!");
            adminPlayer.data_yesno = null;
            return;
        }

        if (value == 0 && adminPlayer.data_yesno != null && adminPlayer.data_yesno.length >= 3
                && adminPlayer.data_yesno[0] == YESNO_ID_BANK_APPROVE_DEPOSIT) {

            PendingRecharge selected = SELECTED_APPROVE_MAP.get(adminPlayer.name);
            if (selected == null || selected.id != adminPlayer.data_yesno[1]) {
                Service.send_box_ThongBao_OK(adminPlayer, "Đơn nạp không hợp lệ hoặc đã bị thay đổi!");
                adminPlayer.data_yesno = null;
                return;
            }

            approveRechargeOrder(adminPlayer, selected);
        }

        adminPlayer.data_yesno = null;
        SELECTED_APPROVE_MAP.remove(adminPlayer.name);
    }

    /**
     * Thực hiện duyệt đơn nạp và cập nhật CSDL + đồng bộ Player
     */
    private static void approveRechargeOrder(Player adminPlayer, PendingRecharge selected) throws IOException {
        Connection conn = null;
        PreparedStatement psSelect = null;
        PreparedStatement psUpdateHistory = null;
        PreparedStatement psUpdateAccount = null;
        PreparedStatement psSelectTichNap = null;
        PreparedStatement psUpdateVip = null;
        ResultSet rs = null;

        try {
            conn = SQL.gI().getCon();
            conn.setAutoCommit(false);

            // Kiểm tra trạng thái đơn nạp
            psSelect = conn.prepareStatement("SELECT `username`, `amount`, `status` FROM `recharge_history` WHERE `id` = ? FOR UPDATE;");
            psSelect.setInt(1, selected.id);
            rs = psSelect.executeQuery();
            if (!rs.next() || rs.getInt("status") != 0) {
                conn.rollback();
                Service.send_box_ThongBao_OK(adminPlayer, "Đơn nạp này đã được xử lý bởi người khác hoặc không tồn tại!");
                return;
            }
            String targetUser = rs.getString("username");
            int targetAmount = rs.getInt("amount");
            int coinGain = targetAmount / 1000;

            // Cập nhật trạng thái đơn nạp
            psUpdateHistory = conn.prepareStatement(
                    "UPDATE `recharge_history` SET `status` = 1, `real_amount` = ?, `description` = CONCAT(COALESCE(`description`,''), ' [Duyệt bởi ', ?, ']') WHERE `id` = ?;");
            psUpdateHistory.setInt(1, targetAmount);
            psUpdateHistory.setString(2, adminPlayer.name);
            psUpdateHistory.setInt(3, selected.id);
            psUpdateHistory.executeUpdate();

            // Cập nhật tài khoản
            psUpdateAccount = conn.prepareStatement(
                    "UPDATE `accounts` SET `coin` = `coin` + ?, `tichnap` = `tichnap` + ?, `sumamount` = `sumamount` + ?, `tongnap` = `tongnap` + ?, `vnd` = `vnd` + ? WHERE BINARY `user` = ?;");
            psUpdateAccount.setInt(1, coinGain);
            psUpdateAccount.setInt(2, targetAmount);
            psUpdateAccount.setInt(3, targetAmount);
            psUpdateAccount.setInt(4, targetAmount);
            psUpdateAccount.setInt(5, targetAmount);
            psUpdateAccount.setString(6, targetUser);
            int rowsAcc = psUpdateAccount.executeUpdate();
            if (rowsAcc == 0) {
                conn.rollback();
                Service.send_box_ThongBao_OK(adminPlayer, "Không tìm thấy tài khoản [" + targetUser + "] trong cơ sở dữ liệu!");
                return;
            }

            // Tính toán cấp VIP mới
            psSelectTichNap = conn.prepareStatement("SELECT `tichnap`, `vip` FROM `accounts` WHERE BINARY `user` = ? LIMIT 1;");
            psSelectTichNap.setString(1, targetUser);
            rs.close();
            rs = psSelectTichNap.executeQuery();
            int newTichNap = 0;
            int currentVip = 0;
            if (rs.next()) {
                newTichNap = rs.getInt("tichnap");
                currentVip = rs.getInt("vip");
            }
            int newVip = calculateVip(newTichNap);
            if (newVip > currentVip) {
                psUpdateVip = conn.prepareStatement("UPDATE `accounts` SET `vip` = ? WHERE BINARY `user` = ?;");
                psUpdateVip.setInt(1, newVip);
                psUpdateVip.setString(2, targetUser);
                psUpdateVip.executeUpdate();
            }

            conn.commit();

            // Đồng bộ trực tiếp với Session đang online nếu có
            boolean isOnline = false;
            synchronized (SessionManager.CLIENT_ENTRYS) {
                for (int i = 0; i < SessionManager.CLIENT_ENTRYS.size(); i++) {
                    io.Session sess = SessionManager.CLIENT_ENTRYS.get(i);
                    if (sess != null && targetUser.equalsIgnoreCase(sess.user)) {
                        isOnline = true;
                        sess.coin += coinGain;
                        sess.tichnap += targetAmount;
                        sess.tongnap += targetAmount;
                        if (newVip > sess.vip) {
                            sess.vip = newVip;
                        }
                        if (sess.p != null) {
                            try {
                                sess.p.update_money();
                                activities.ListTichNap.syncAccountTichNap(sess.p);
                                Service.send_box_ThongBao_OK(sess.p, "🎉 THÔNG BÁO NẠP TIỀN THÀNH CÔNG!\n\n"
                                        + "Đơn nạp " + Util.number_format(targetAmount) + " VNĐ của bạn đã được Admin duyệt thành công.\n"
                                        + "• Nhận được: +" + Util.number_format(coinGain) + " Coin\n"
                                        + "• Tích nạp: +" + Util.number_format(targetAmount) + " điểm"
                                        + (newVip > currentVip ? ("\n• Chúc mừng bạn đã được nâng cấp lên VIP " + newVip + "!") : ""));
                            } catch (Exception ex) {
                                ex.printStackTrace();
                            }
                        }
                        break;
                    }
                }
            }

            Service.send_box_ThongBao_OK(adminPlayer, "✅ Đã duyệt thành công đơn nạp " + Util.number_format(targetAmount)
                    + " VNĐ cho tài khoản [" + targetUser + "]!" + (isOnline ? " (Người chơi đang Online)" : " (Người chơi Offline)"));

            // Thông báo toàn server
            Manager.gI().chatKTG(0, "Thông báo: Người chơi [" + targetUser + "] vừa nạp thành công " + Util.number_format(targetAmount) + " VNĐ vào Ngân Hàng!", 5);

            // Xóa cache danh sách để tải lại lần sau
            PENDING_MAP.remove(adminPlayer.name);

        } catch (SQLException e) {
            if (conn != null) {
                try {
                    conn.rollback();
                } catch (Exception ex) {}
            }
            e.printStackTrace();
            Service.send_box_ThongBao_OK(adminPlayer, "Có lỗi xảy ra khi duyệt nạp: " + e.getMessage());
        } finally {
            try {
                if (rs != null) rs.close();
                if (psSelect != null) psSelect.close();
                if (psUpdateHistory != null) psUpdateHistory.close();
                if (psUpdateAccount != null) psUpdateAccount.close();
                if (psSelectTichNap != null) psSelectTichNap.close();
                if (psUpdateVip != null) psUpdateVip.close();
                if (conn != null) {
                    conn.setAutoCommit(true);
                    conn.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Bảng tính cấp VIP tự động dựa trên tổng điểm nạp tích lũy
     */
    public static int calculateVip(int tichnap) {
        if (tichnap >= 10_000_000) return 7;
        if (tichnap >= 5_000_000) return 6;
        if (tichnap >= 3_000_000) return 5;
        if (tichnap >= 2_000_000) return 4;
        if (tichnap >= 1_000_000) return 3;
        if (tichnap >= 500_000) return 2;
        if (tichnap >= 200_000) return 1;
        return 0;
    }

    /**
     * Gửi Menu con chọn các mục Thông tin Ngân Hàng
     */
    public static void showInfoMenu(Player p, int npcId) throws IOException {
        if (p == null || p.isdie) return;
        MenuController.send_dynamic_menu(p, MENU_ID_BANK_INFO, "Thông Tin Ngân Hàng",
                new String[] { "Tỷ lệ quy đổi", "Mốc cấp VIP", "Hướng dẫn nạp", "Top Nạp Tiền", "Lướt xem tất cả", "Quay lại" },
                new short[] { 140, 148, 132, 161, 140, 161 });
    }

    /**
     * Xử lý lựa chọn từ Menu Thông tin
     */
    public static void handleInfoMenu(Player p, byte index) throws IOException {
        if (p == null || p.isdie) return;
        switch (index) {
            case 0: // Tỷ lệ quy đổi (ngắn gọn, không che màn hình)
                Service.send_box_ThongBao_OK(p, "💰 TỶ LỆ QUY ĐỔI COIN 💰\n\n"
                        + "• 1.000 VNĐ = 1 Coin = 1.000 Điểm tích nạp.\n"
                        + "• 1 Coin quy đổi nhận ngay: 100 Ruby + 1.000 Extol.\n"
                        + "• Đổi Coin trực tiếp tại mục [Đổi Coin] ở NPC Ngân Hàng.");
                break;
            case 1: // Mốc VIP (ngắn gọn, không che màn hình)
                Service.send_box_ThongBao_OK(p, "👑 MỐC TÍCH NẠP THĂNG CẤP VIP 👑\n\n"
                        + "• VIP 1: 200k (215 Ruby/ngày)\n"
                        + "• VIP 2: 500k (1.000 Ruby/ngày)\n"
                        + "• VIP 3: 1.000k (2.000 Ruby/ngày)\n"
                        + "• VIP 4: 2.000k (3.000 Ruby/ngày)\n"
                        + "• VIP 5: 3.000k (7.000 Ruby/ngày)\n"
                        + "• VIP 6: 5.000k (20.000 Ruby/ngày)\n"
                        + "• VIP 7: 10.000k (50.000 Ruby/ngày)");
                break;
            case 2: // Hướng dẫn nạp (ngắn gọn, không che màn hình)
                Service.send_box_ThongBao_OK(p, "📝 HƯỚNG DẪN NẠP TIỀN 📝\n\n"
                        + "• Bước 1: Chọn [Nạp tiền] và nhập số tiền muốn nạp.\n"
                        + "• Bước 2: Chuyển khoản đúng thông tin và mã GD.\n"
                        + "• Bước 3: Admin sẽ duyệt đơn nạp trong giây lát!");
                break;
            case 3: // Top Nạp Tiền
                core.BXH.send(p, 17, 0);
                break;
            case 4: // Lướt xem tất cả dạng hội thoại NPC phân trang (Bấm Tiếp tục để lướt)
                String helpText = "Tỷ lệ nạp & quy đổi\r\n"
                        + "• 1.000 VNĐ = 1 Coin = 1.000 Điểm tích nạp.\b"
                        + "Quy đổi Coin sang Ruby & Extol\r\n"
                        + "• 1 Coin quy đổi nhận ngay: 100 Ruby + 1.000 Extol.\r\n"
                        + "• Đổi Coin trực tiếp tại mục [Đổi Coin] ở NPC Ngân Hàng.\b"
                        + "Mốc cấp VIP tự động\r\n"
                        + "• VIP 1: 200k | VIP 2: 500k | VIP 3: 1.000k\r\n"
                        + "• VIP 4: 2.000k | VIP 5: 3.000k | VIP 6: 5.000k | VIP 7: 10.000k\r\n"
                        + "• Nhận quà Ruby hàng ngày theo cấp VIP!\b"
                        + "Hướng dẫn nạp tiền\r\n"
                        + "• Bước 1: Chọn [Nạp tiền] và nhập số tiền muốn nạp.\r\n"
                        + "• Bước 2: Chuyển khoản đúng thông tin và nội dung.\r\n"
                        + "• Bước 3: Admin sẽ duyệt đơn nạp trong giây lát!";
                Service.Help_From_Server(p, -205, helpText);
                break;
            case 5: // Quay lại
                sendMainMenu(p, -205);
                break;
        }
    }

    /**
     * Hiển thị bảng Thông tin quy đổi & mốc VIP (Dạng tóm tắt gọn gàng)
     */
    public static void showInfo(Player p) throws IOException {
        showInfoMenu(p, -205);
    }

    /**
     * Báo cho các Admin online khi có đơn nạp mới
     */
    private static void notifyAdminsNewDeposit(String charName, String username, int amount) {
        synchronized (SessionManager.CLIENT_ENTRYS) {
            for (int i = 0; i < SessionManager.CLIENT_ENTRYS.size(); i++) {
                io.Session sess = SessionManager.CLIENT_ENTRYS.get(i);
                if (sess != null && sess.p != null && isAdmin(sess.p)) {
                    try {
                        Service.send_box_ThongBao_OK(sess.p, "🔔 CÓ YÊU CẦU NẠP MỚI!\n\n"
                                + "• Nhân vật: " + charName + " (Tài khoản: " + username + ")\n"
                                + "• Số tiền: " + Util.number_format(amount) + " VNĐ\n"
                                + "Hãy đến NPC Ngân Hàng để duyệt đơn!");
                    } catch (Exception ex) {}
                }
            }
        }
    }
}
