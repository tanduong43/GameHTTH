package activities;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;
import java.time.temporal.TemporalAdjusters;
import java.util.ArrayList;
import java.util.List;

import client.Player;
import core.Manager;
import core.MenuController;
import core.Service;
import core.Util;
import database.SQL;
import org.json.simple.JSONArray;
import org.json.simple.JSONValue;

/**
 * Quản lý tính năng Top Nạp Tuần tại NPC Ngân Hàng:
 * - Xem BXH nạp tuần hiện tại (Real-time).
 * - Xem danh sách phần thưởng và mốc nạp tối thiểu.
 * - Nhận quà Top nạp tuần trước.
 * - Tự động chốt giải & reset tuần vào 00:00 Thứ 2 hàng tuần (Có cơ chế Safe-check khi restart server).
 * - Hỗ trợ Admin chốt thử nghiệm ngay lập tức.
 *
 * @author HTTH Dev
 */
public class TopNapTuan {

    public static final ZoneId ZONE_VN = ZoneId.of("Asia/Ho_Chi_Minh");
    public static final DateTimeFormatter SQL_DATE_FORMAT = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
    public static final DateTimeFormatter WEEK_KEY_FORMAT = DateTimeFormatter.ofPattern("yyyy_MM_dd");
    public static final DateTimeFormatter DISPLAY_DATE_FORMAT = DateTimeFormatter.ofPattern("dd/MM/yyyy");

    // ================== CẤU HÌNH MỐC NẠP TỐI THIỂU ==================
    public static final long MIN_RECHARGE_TOP_1 = 500_000L;    // 500.000 VNĐ
    public static final long MIN_RECHARGE_TOP_2_3 = 200_000L;  // 200.000 VNĐ
    public static final long MIN_RECHARGE_TOP_4_10 = 50_000L;  // 50.000 VNĐ

    // ================== CẤU HÌNH PHẦN THƯỞNG ==================
    // TOP 1
    public static final int TOP1_RUBY = 100_000;
    public static final long TOP1_BERI = 1_000_000_000L;
    public static final int[][] TOP1_ITEMS = new int[][] {
            { 4, 1004, 3 },   // 3x Rương Đá Thần Thoại Tự Chọn
            { 4, 1003, 1 },   // 1x Rương Trái Ác Quỷ Cao Cấp Tự Chọn
            { 4, 1002, 1 },   // 1x Hộp Thời Trang Cao Cấp
            { 4, 551, 1 },    // 1x Bảo Hiểm Chuyển Hóa Cao
            { 4, 131, 100 },  // 100x Rương Cam Cùng Hệ Lv100
            { 4, 1018, 1 }    // 1x Vé đổi tên Clan
    };

    // TOP 2 - 3
    public static final int TOP23_RUBY = 50_000;
    public static final long TOP23_BERI = 500_000_000L;
    public static final int[][] TOP23_ITEMS = new int[][] {
            { 4, 1004, 2 },   // 2x Rương Đá Thần Thoại Tự Chọn
            { 4, 1003, 1 },   // 1x Rương Trái Ác Quỷ Cao Cấp Tự Chọn
            { 4, 326, 1 },    // 1x Đá Khảm Vô Cực S
            { 4, 1001, 1 },   // 1x Hộp Thời Trang Sơ Cấp
            { 4, 550, 1 },    // 1x Bảo Hiểm Chuyển Hóa Trung
            { 4, 131, 100 }   // 100x Rương Cam Cùng Hệ Lv100
    };

    // TOP 4 - 10
    public static final int TOP410_RUBY = 30_000;
    public static final long TOP410_BERI = 300_000_000L;
    public static final int[][] TOP410_ITEMS = new int[][] {
            { 4, 1004, 1 },   // 1x Rương Đá Thần Thoại Tự Chọn
            { 4, 549, 1 },    // 1x Bảo Hiểm Chuyển Hóa Sơ
            { 4, 131, 100 }   // 100x Rương Cam Cùng Hệ Lv100
    };

    public static class RankEntry {
        public int rank;
        public String username;
        public String charName;
        public long amount;

        public RankEntry(int rank, String username, String charName, long amount) {
            this.rank = rank;
            this.username = username;
            this.charName = charName;
            this.amount = amount;
        }
    }

    /**
     * Khởi tạo bảng dữ liệu và kiểm tra chốt tuần nếu server từng bị tắt ngang khi qua tuần
     */
    public static void init() {
        Connection conn = null;
        Statement st = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) return;
            st = conn.createStatement();
            // Tạo bảng lưu kết quả thưởng Top Nạp Tuần nếu chưa có
            st.executeUpdate("CREATE TABLE IF NOT EXISTS `top_nap_week_reward` ("
                    + "`id` INT AUTO_INCREMENT PRIMARY KEY, "
                    + "`week_key` VARCHAR(30) NOT NULL COMMENT 'Mã tuần (yyyy_MM_dd Thứ 2)', "
                    + "`username` VARCHAR(50) NOT NULL, "
                    + "`char_name` VARCHAR(50) NOT NULL, "
                    + "`rank` INT NOT NULL, "
                    + "`amount` BIGINT NOT NULL, "
                    + "`min_req_met` TINYINT DEFAULT 1 COMMENT '1: Đủ mốc nạp tối thiểu, 0: Chưa đủ mốc', "
                    + "`claimed` TINYINT DEFAULT 0 COMMENT '0: Chưa nhận, 1: Đã nhận', "
                    + "`claimed_at` DATETIME DEFAULT NULL, "
                    + "`created_at` DATETIME DEFAULT CURRENT_TIMESTAMP, "
                    + "INDEX `idx_week_user` (`week_key`, `username`)"
                    + ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

            // Tự động kiểm tra xem tuần trước đã được chốt chưa
            checkAutoWeeklyReset();
            System.out.println("[TopNapTuan] Initialized successfully.");
        } catch (Exception e) {
            System.err.println("[TopNapTuan] Error during init: " + e.getMessage());
            e.printStackTrace();
        } finally {
            try {
                if (st != null) st.close();
                if (conn != null) conn.close();
            } catch (Exception e) {}
        }
    }

    /**
     * Lấy ngày Thứ Hai của tuần hiện tại
     */
    public static LocalDate getCurrentMonday() {
        LocalDate today = LocalDate.now(ZONE_VN);
        return today.with(TemporalAdjusters.previousOrSame(DayOfWeek.MONDAY));
    }

    /**
     * Lấy ngày Thứ Hai của tuần trước
     */
    public static LocalDate getLastMonday() {
        return getCurrentMonday().minusWeeks(1);
    }

    /**
     * Gửi Menu Top Nạp Tuần
     */
    public static void sendMenu(Player p) throws IOException {
        if (p == null || p.isdie) return;
        boolean isAdmin = Bank.isAdmin(p);
        if (isAdmin) {
            MenuController.send_dynamic_menu(p, Bank.MENU_ID_TOP_NAP_TUAN, "Top Nạp Tuần",
                    new String[] { "BXH Tuần Này", "Xem Quà Mốc Nạp", "Nhận Quà Tuần Trước", "Chốt Tuần (Test)" },
                    new short[] { 140, 148, 132, 161 });
        } else {
            MenuController.send_dynamic_menu(p, Bank.MENU_ID_TOP_NAP_TUAN, "Top Nạp Tuần",
                    new String[] { "BXH Tuần Này", "Xem Quà Mốc Nạp", "Nhận Quà Tuần Trước" },
                    new short[] { 140, 148, 132 });
        }
    }

    /**
     * Xử lý lựa chọn từ Menu Top Nạp Tuần
     */
    public static void handleMenu(Player p, byte index) throws IOException {
        if (p == null) return;
        switch (index) {
            case 0:
                showCurrentWeekLeaderboard(p);
                break;
            case 1:
                showRewardInfo(p);
                break;
            case 2:
                claimLastWeekReward(p);
                break;
            case 3:
                if (Bank.isAdmin(p)) {
                    adminForceCloseWeek(p);
                }
                break;
        }
    }

    /**
     * Lấy danh sách Top nạp trong một khoảng thời gian nhất định
     */
    public static List<RankEntry> getRanking(LocalDateTime startDateTime, LocalDateTime endDateTime, int limit) {
        List<RankEntry> list = new ArrayList<>();
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) return list;

            String sql = "SELECT rh.username, "
                    + "COALESCE(SUM(rh.amount), 0) AS total_amount, "
                    + "MAX(rh.created_at) AS last_time, "
                    + "a.`char` "
                    + "FROM `recharge_history` rh "
                    + "LEFT JOIN `accounts` a ON BINARY rh.username = BINARY a.user "
                    + "WHERE rh.status = 1 "
                    + "  AND rh.created_at >= ? AND rh.created_at <= ? "
                    + "GROUP BY rh.username, a.`char` "
                    + "ORDER BY total_amount DESC, last_time ASC "
                    + "LIMIT ?;";

            ps = conn.prepareStatement(sql);
            ps.setString(1, startDateTime.format(SQL_DATE_FORMAT));
            ps.setString(2, endDateTime.format(SQL_DATE_FORMAT));
            ps.setInt(3, limit);
            rs = ps.executeQuery();

            int rank = 1;
            while (rs.next()) {
                String username = rs.getString("username");
                long amount = rs.getLong("total_amount");
                String charName = null;
                try {
                    String charStr = rs.getString("char");
                    if (charStr != null && !charStr.trim().isEmpty()) {
                        JSONArray jsChar = (JSONArray) JSONValue.parse(charStr);
                        if (jsChar != null && !jsChar.isEmpty()) {
                            charName = jsChar.get(0).toString();
                        }
                    }
                } catch (Exception e) {}

                if (charName == null || charName.trim().isEmpty()) {
                    charName = username;
                }

                list.add(new RankEntry(rank++, username, charName, amount));
            }
        } catch (Exception e) {
            System.err.println("[TopNapTuan] Error getting ranking: " + e.getMessage());
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (Exception e) {}
        }
        return list;
    }

    /**
     * Lấy tổng nạp trong tuần hiện tại của một user
     */
    public static long getPlayerWeeklyAmount(String username, LocalDateTime startDateTime, LocalDateTime endDateTime) {
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) return 0L;

            ps = conn.prepareStatement("SELECT COALESCE(SUM(amount), 0) AS total FROM `recharge_history` "
                    + "WHERE status = 1 AND BINARY username = ? AND created_at >= ? AND created_at <= ?;");
            ps.setString(1, username);
            ps.setString(2, startDateTime.format(SQL_DATE_FORMAT));
            ps.setString(3, endDateTime.format(SQL_DATE_FORMAT));
            rs = ps.executeQuery();
            if (rs.next()) {
                return rs.getLong("total");
            }
        } catch (Exception e) {
            System.err.println("[TopNapTuan] Error getPlayerWeeklyAmount: " + e.getMessage());
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (Exception e) {}
        }
        return 0L;
    }

    /**
     * Hiển thị bảng xếp hạng tuần hiện tại trên giao diện native client (giống Top Level / Cao Thủ)
     */
    public static void showCurrentWeekLeaderboard(Player p) throws IOException {
        if (p == null) return;
        // Mở giao diện Bảng Xếp Hạng chuẩn Client (giống Top Level / Cao Thủ)
        core.BXH.send(p, 17, 0);

        // Gửi thông báo ngắn góc màn hình về tiến độ nạp của người chơi
        LocalDate monday = getCurrentMonday();
        LocalDate sunday = monday.plusDays(6);
        long myAmount = getPlayerWeeklyAmount(p.conn.user, monday.atStartOfDay(), sunday.atTime(23, 59, 59));

        String status;
        if (myAmount >= MIN_RECHARGE_TOP_1) {
            status = "Đủ mốc Top 1 (≥ 500k)";
        } else if (myAmount >= MIN_RECHARGE_TOP_2_3) {
            status = "Đủ mốc Top 2-3 (≥ 200k)";
        } else if (myAmount >= MIN_RECHARGE_TOP_4_10) {
            status = "Đủ mốc Top 4-10 (≥ 50k)";
        } else {
            status = "Cần nạp thêm " + Util.number_format(MIN_RECHARGE_TOP_4_10 - myAmount) + "đ để vào mốc Top 4-10";
        }
        Service.send_server_notice(p, "Nạp tuần của bạn: " + Util.number_format(myAmount) + " VNĐ (" + status + ")");
    }

    /**
     * Hiển thị bảng mô tả quà tặng & mốc nạp tối thiểu (phân trang NPC có nút Tiếp tục để lướt xem, không bao giờ bị che mất)
     */
    public static void showRewardInfo(Player p) throws IOException {
        String pagedHelp = "🥇 PHẦN THƯỞNG TOP 1 🥇\n"
                + "• Điều kiện: Nạp tối thiểu 500.000 VNĐ trong tuần\n"
                + "━━━━━━━━━━━━━━━━━━━━━━━━\n"
                + "🎁 Phần thưởng nhận được:\n"
                + "• 100.000 Ruby\n"
                + "• 1.000.000.000 Beri (1 Tỷ)\n"
                + "• 3x Rương Đá Thần Thoại Tự Chọn (1004)\n"
                + "• 1x Rương Trái Ác Quỷ Cao Cấp Tự Chọn (1003)\n"
                + "• 1x Hộp Thời Trang Cao Cấp (1002)\n"
                + "• 1x Bảo Hiểm Chuyển Hóa Cao (551)\n"
                + "• 100x Rương Cam Cùng Hệ Lv100 (131)\n"
                + "• 1x Vé đổi tên Clan (1018)\n\n"
                + "👉 Bấm [Tiếp tục] để xem quà TOP 2 - 3!\b"

                + "🥈 PHẦN THƯỞNG TOP 2 - 3 🥈\n"
                + "• Điều kiện: Nạp tối thiểu 200.000 VNĐ trong tuần\n"
                + "━━━━━━━━━━━━━━━━━━━━━━━━\n"
                + "🎁 Phần thưởng nhận được:\n"
                + "• 50.000 Ruby\n"
                + "• 500.000.000 Beri (500 Tr)\n"
                + "• 2x Rương Đá Thần Thoại Tự Chọn (1004)\n"
                + "• 1x Rương Trái Ác Quỷ Cao Cấp Tự Chọn (1003)\n"
                + "• 1x Đá Khảm Vô Cực S (326)\n"
                + "• 1x Hộp Thời Trang Sơ Cấp (1001)\n"
                + "• 1x Bảo Hiểm Chuyển Hóa Trung (550)\n"
                + "• 100x Rương Cam Cùng Hệ Lv100 (131)\n\n"
                + "👉 Bấm [Tiếp tục] để xem quà TOP 4 - 10!\b"

                + "🥉 PHẦN THƯỞNG TOP 4 - 10 🥉\n"
                + "• Điều kiện: Nạp tối thiểu 50.000 VNĐ trong tuần\n"
                + "━━━━━━━━━━━━━━━━━━━━━━━━\n"
                + "🎁 Phần thưởng nhận được:\n"
                + "• 30.000 Ruby\n"
                + "• 300.000.000 Beri (300 Tr)\n"
                + "• 1x Rương Đá Thần Thoại Tự Chọn (1004)\n"
                + "• 1x Bảo Hiểm Chuyển Hóa Sơ (549)\n"
                + "• 100x Rương Cam Cùng Hệ Lv100 (131)\n\n"
                + "👉 Bấm [Tiếp tục] để xem Lưu ý nhận thưởng!\b"

                + "📌 QUY TẮC & LƯU Ý NHẬN THƯỞNG 📌\n"
                + "━━━━━━━━━━━━━━━━━━━━━━━━\n"
                + "• Thời gian tính: 00:00 Thứ 2 đến 23:59:59 Chủ Nhật.\n"
                + "• Đúng 00:00 Thứ 2 hàng tuần, hệ thống tự động chốt giải và reset BXH tuần mới về 0đ.\n"
                + "• Người chơi đạt Top nhưng KHÔNG đạt mốc nạp tối thiểu sẽ không nhận được quà.\n"
                + "• Sau khi chốt tuần, đến gặp NPC Ngân Hàng chọn [Nhận Quà Tuần Trước] để nhận quà vào hành trang!\n"
                + "• Hành trang cần chuẩn bị ít nhất 6 ô trống khi nhận thưởng.";

        Service.Help_From_Server(p, Bank.NPC_ID_BANK, pagedHelp);
    }

    /**
     * Nhận thưởng Top Nạp tuần trước
     */
    public static void claimLastWeekReward(Player p) throws IOException {
        if (p == null || p.conn == null || p.conn.user == null) return;

        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) {
                Service.send_box_ThongBao_OK(p, "Lỗi kết nối cơ sở dữ liệu!");
                return;
            }

            // Tìm giải thưởng gần nhất của user này
            ps = conn.prepareStatement("SELECT `id`, `week_key`, `rank`, `amount`, `min_req_met`, `claimed` "
                    + "FROM `top_nap_week_reward` "
                    + "WHERE BINARY `username` = ? "
                    + "ORDER BY `id` DESC LIMIT 1;");
            ps.setString(1, p.conn.user);
            rs = ps.executeQuery();

            if (!rs.next()) {
                Service.send_box_ThongBao_OK(p, "Bạn không có tên trong danh sách TOP 10 Nạp Tuần vừa qua để nhận thưởng!\n"
                        + "Hãy tiếp tục tích lũy và đua top tuần này nhé!");
                return;
            }

            int rewardId = rs.getInt("id");
            String weekKey = rs.getString("week_key");
            int rank = rs.getInt("rank");
            long amount = rs.getLong("amount");
            int minReqMet = rs.getInt("min_req_met");
            int claimed = rs.getInt("claimed");

            if (claimed == 1) {
                Service.send_box_ThongBao_OK(p, "Bạn đã nhận phần thưởng Top " + rank + " Nạp Tuần (" + weekKey + ") rồi!");
                return;
            }

            long minReq = (rank == 1) ? MIN_RECHARGE_TOP_1 : (rank <= 3) ? MIN_RECHARGE_TOP_2_3 : MIN_RECHARGE_TOP_4_10;
            if (minReqMet == 0 || amount < minReq) {
                Service.send_box_ThongBao_OK(p, "Tuần vừa qua bạn đạt Hạng " + rank + " với tổng nạp " + Util.number_format(amount) + " VNĐ,\n"
                        + "nhưng chưa đạt mốc nạp tối thiểu của Top " + rank + " (" + Util.number_format(minReq) + " VNĐ) nên không đủ điều kiện nhận quà!\n"
                        + "Chúc bạn may mắn ở tuần đua top tiếp theo!");
                return;
            }

            // Kiểm tra số ô trống trong hành trang
            int requiredSlots = (rank == 1) ? TOP1_ITEMS.length : (rank <= 3) ? TOP23_ITEMS.length : TOP410_ITEMS.length;
            if (p.item.able_bag() < requiredSlots) {
                Service.send_box_ThongBao_OK(p, "Hành trang của bạn không đủ chỗ trống!\n"
                        + "Vui lòng dọn dẹp ít nhất " + requiredSlots + " ô trống rồi nhận lại phần thưởng nhé!");
                return;
            }

            // Tiến hành trao thưởng theo hạng
            int rubyAdd = 0;
            long beriAdd = 0L;
            int[][] itemsAdd = null;
            String rankTitle = "";

            if (rank == 1) {
                rubyAdd = TOP1_RUBY;
                beriAdd = TOP1_BERI;
                itemsAdd = TOP1_ITEMS;
                rankTitle = "🥇 TOP 1 (Quán Quân)";
            } else if (rank <= 3) {
                rubyAdd = TOP23_RUBY;
                beriAdd = TOP23_BERI;
                itemsAdd = TOP23_ITEMS;
                rankTitle = "🥈 TOP " + rank + " (Á Quân)";
            } else {
                rubyAdd = TOP410_RUBY;
                beriAdd = TOP410_BERI;
                itemsAdd = TOP410_ITEMS;
                rankTitle = "🥉 TOP " + rank;
            }

            // Thêm vật phẩm
            for (int[] it : itemsAdd) {
                int cat = it[0];
                int itemId = it[1];
                int quant = it[2];
                p.item.add_item_bag47(cat, itemId, quant);
            }

            // Cộng tiền
            p.update_ngoc(rubyAdd);
            p.update_vang(beriAdd);
            p.item.update_Inventory(-1, false);

            // Cập nhật trạng thái đã nhận trong DB
            rs.close();
            ps.close();
            ps = conn.prepareStatement("UPDATE `top_nap_week_reward` SET `claimed` = 1, `claimed_at` = NOW() WHERE `id` = ?;");
            ps.setInt(1, rewardId);
            ps.executeUpdate();

            // Thông báo chúc mừng
            StringBuilder rewardNotice = new StringBuilder();
            rewardNotice.append("🏆 Chúc mừng bạn đã nhận thành công quà ").append(rankTitle).append(" Nạp Tuần:\n")
                        .append("• +").append(Util.number_format(rubyAdd)).append(" Ruby\n")
                        .append("• +").append(Util.number_format(beriAdd)).append(" Beri\n");
            for (int[] it : itemsAdd) {
                template.ItemTemplate4 itTemp = template.ItemTemplate4.get_it_by_id(it[1]);
                String itName = (itTemp != null) ? itTemp.name : ("Vật phẩm ID " + it[1]);
                rewardNotice.append("• +").append(it[2]).append("x ").append(itName).append("\n");
            }
            Service.send_box_ThongBao_OK(p, rewardNotice.toString());

            // Thông báo chat KTG toàn server
            Manager.gI().chatKTG(0, "🎉 Thuyền trưởng [" + p.name + "] vừa nhận thành công phần thưởng " + rankTitle + " Nạp Tuần tại NPC Ngân Hàng!", 5);

        } catch (Exception e) {
            System.err.println("[TopNapTuan] Error claiming reward: " + e.getMessage());
            e.printStackTrace();
            Service.send_box_ThongBao_OK(p, "Đã có lỗi xảy ra khi nhận thưởng: " + e.getMessage());
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (Exception e) {}
        }
    }

    /**
     * Tự động kiểm tra chốt tuần vào 00:00 Thứ 2 hàng tuần
     */
    public static synchronized void checkAutoWeeklyReset() {
        LocalDate currentMonday = getCurrentMonday();
        LocalDate lastMonday = getLastMonday();
        String expectedClosedWeekKey = lastMonday.format(WEEK_KEY_FORMAT);

        // Kiểm tra tuần gần nhất đã chốt từ server_config
        String lastClosedWeek = getServerConfig("last_closed_top_week");
        if (lastClosedWeek == null || !lastClosedWeek.equalsIgnoreCase(expectedClosedWeekKey)) {
            // Cần chốt giải cho tuần trước!
            System.out.println("[TopNapTuan] Closing weekly recharge top for week: " + expectedClosedWeekKey);
            closeWeek(lastMonday, false);
            setServerConfig("last_closed_top_week", expectedClosedWeekKey, "Tuần gần nhất đã chốt Top Nạp");
        }
    }

    /**
     * Thực hiện chốt giải và lưu snapshot kết quả cho tuần được chỉ định
     */
    public static synchronized boolean closeWeek(LocalDate mondayOfClosedWeek, boolean isAdminForced) {
        LocalDate sundayOfClosedWeek = mondayOfClosedWeek.plusDays(6);
        LocalDateTime startDateTime = mondayOfClosedWeek.atStartOfDay();
        LocalDateTime endDateTime = sundayOfClosedWeek.atTime(23, 59, 59);

        // Nếu admin test ép chốt trong tuần hiện tại
        if (isAdminForced) {
            endDateTime = LocalDateTime.now(ZONE_VN);
        }

        String weekKey = mondayOfClosedWeek.format(WEEK_KEY_FORMAT);
        if (isAdminForced) {
            weekKey = "TEST_" + weekKey + "_" + (System.currentTimeMillis() % 10000);
        }

        List<RankEntry> ranking = getRanking(startDateTime, endDateTime, 10);
        if (ranking.isEmpty()) {
            System.out.println("[TopNapTuan] No players in ranking for week: " + weekKey);
            return false;
        }

        Connection conn = null;
        PreparedStatement ps = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) return false;

            ps = conn.prepareStatement("INSERT INTO `top_nap_week_reward` "
                    + "(`week_key`, `username`, `char_name`, `rank`, `amount`, `min_req_met`, `claimed`, `created_at`) "
                    + "VALUES (?, ?, ?, ?, ?, ?, 0, NOW());");

            for (RankEntry entry : ranking) {
                long minReq = (entry.rank == 1) ? MIN_RECHARGE_TOP_1 : (entry.rank <= 3) ? MIN_RECHARGE_TOP_2_3 : MIN_RECHARGE_TOP_4_10;
                int minReqMet = (entry.amount >= minReq) ? 1 : 0;

                ps.setString(1, weekKey);
                ps.setString(2, entry.username);
                ps.setString(3, entry.charName);
                ps.setInt(4, entry.rank);
                ps.setLong(5, entry.amount);
                ps.setInt(6, minReqMet);
                ps.addBatch();
            }

            ps.executeBatch();
            System.out.println("[TopNapTuan] Successfully saved " + ranking.size() + " winners for week: " + weekKey);

            // Thông báo kênh thế giới
            Manager.gI().chatKTG(0, "📢 Bảng Xếp Hạng Top Nạp Tuần (" + mondayOfClosedWeek.format(DISPLAY_DATE_FORMAT)
                    + " - " + sundayOfClosedWeek.format(DISPLAY_DATE_FORMAT) + ") đã chính thức chốt giải!\n"
                    + "👑 Quán quân Top 1: " + ranking.get(0).charName + " (" + Util.number_format(ranking.get(0).amount) + " VNĐ)!\n"
                    + "👉 Các thuyền trưởng lọt Top vui lòng đến gặp NPC Ngân Hàng để nhận thưởng!", 5);

            return true;
        } catch (Exception e) {
            System.err.println("[TopNapTuan] Error closing week: " + e.getMessage());
            e.printStackTrace();
            return false;
        } finally {
            try {
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (Exception e) {}
        }
    }

    /**
     * Admin ép chốt thử nghiệm tuần hiện tại ngay lập tức để test
     */
    public static void adminForceCloseWeek(Player admin) throws IOException {
        LocalDate currentMonday = getCurrentMonday();
        boolean success = closeWeek(currentMonday, true);
        if (success) {
            Service.send_box_ThongBao_OK(admin, "✅ Đã chốt thử nghiệm BXH tuần hiện tại thành công!\n"
                    + "Kết quả đã được ghi vào dữ liệu nhận thưởng.\n"
                    + "Người chơi có tên trong Top hiện đã có thể chọn [Nhận Quà Tuần Trước] để test nhận thưởng ngay!");
        } else {
            Service.send_box_ThongBao_OK(admin, "⚠️ Không thể chốt tuần: Tuần này hiện chưa có ai nạp tiền để tạo danh sách!");
        }
    }

    /**
     * Lấy giá trị cấu hình từ bảng `server_config`
     */
    private static String getServerConfig(String key) {
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) return null;
            ps = conn.prepareStatement("SELECT `value` FROM `server_config` WHERE `key` = ? LIMIT 1;");
            ps.setString(1, key);
            rs = ps.executeQuery();
            if (rs.next()) {
                return rs.getString("value");
            }
        } catch (Exception e) {
            System.err.println("[TopNapTuan] Error getServerConfig: " + e.getMessage());
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (Exception e) {}
        }
        return null;
    }

    /**
     * Lưu giá trị cấu hình vào bảng `server_config`
     */
    private static void setServerConfig(String key, String value, String description) {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) return;
            ps = conn.prepareStatement("INSERT INTO `server_config` (`key`, `value`, `description`) "
                    + "VALUES (?, ?, ?) "
                    + "ON DUPLICATE KEY UPDATE `value` = VALUES(`value`), `description` = VALUES(`description`);");
            ps.setString(1, key);
            ps.setString(2, value);
            ps.setString(3, description);
            ps.executeUpdate();
        } catch (Exception e) {
            System.err.println("[TopNapTuan] Error setServerConfig: " + e.getMessage());
        } finally {
            try {
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (Exception e) {}
        }
    }
}
