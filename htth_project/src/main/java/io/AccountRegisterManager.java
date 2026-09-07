package io;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import core.Manager;
import database.SQL;

/**
 * Quản lý và giới hạn số tài khoản tạo mới theo IP mỗi ngày.
 * Đồng bộ chung bảng ip_register_logs với hệ thống Web Đăng Ký.
 */
public class AccountRegisterManager {

    private static final Map<String, Integer> DAILY_CACHE = new ConcurrentHashMap<>();
    private static volatile String currentCachedDate = "";

    /**
     * Khởi tạo bảng ip_register_logs nếu chưa tồn tại trong MySQL.
     * Tương thích hoàn toàn với bảng ip_register_logs sẵn có của Web.
     */
    public static void init() {
        Connection conn = null;
        Statement st = null;
        try {
            conn = SQL.gI().getCon();
            if (conn == null) {
                System.err.println("[AccountRegisterManager] Không thể kết nối DB để kiểm tra bảng ip_register_logs.");
                return;
            }
            st = conn.createStatement();
            String sql = "CREATE TABLE IF NOT EXISTS `ip_register_logs` ("
                    + "`id` INT AUTO_INCREMENT PRIMARY KEY, "
                    + "`ip` VARCHAR(100) NOT NULL, "
                    + "`created_at` DATETIME DEFAULT CURRENT_TIMESTAMP, "
                    + "KEY `idx_ip_created_at` (`ip`, `created_at`)"
                    + ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;";
            st.executeUpdate(sql);
            System.out.println("[AccountRegisterManager] Đồng bộ bảng ip_register_logs thành công (Giới hạn: "
                    + Manager.gI().max_register_ip_day + " tài khoản/IP/ngày).");
        } catch (Throwable e) {
            System.err.println("[AccountRegisterManager] Lỗi khi kiểm tra bảng ip_register_logs: " + e.getMessage());
        } finally {
            try {
                if (st != null) st.close();
                if (conn != null) conn.close();
            } catch (SQLException e) {}
        }
    }

    /**
     * Kiểm tra xem một IP có phải là loopback / local IP hay không.
     */
    public static boolean isLocalIp(String ip) {
        if (ip == null || ip.isEmpty()) return true;
        return ip.equals("127.0.0.1") || ip.equals("0:0:0:0:0:0:0:1")
                || ip.equalsIgnoreCase("localhost") || ip.startsWith("fe80:");
    }

    /**
     * Lấy chuỗi ngày hiện tại (yyyy-MM-dd) và làm mới cache nếu đã sang ngày mới.
     */
    private static synchronized void checkDateRollover() {
        String today = new SimpleDateFormat("yyyy-MM-dd").format(new Date());
        if (!today.equals(currentCachedDate)) {
            DAILY_CACHE.clear();
            currentCachedDate = today;
        }
    }

    /**
     * Lấy số lượng tài khoản IP này đã tạo trong ngày hôm nay.
     * Tính chung cả tài khoản đăng ký qua Web và trong Game.
     */
    public static synchronized int getRegisteredCountToday(String ip) {
        if (ip == null || ip.trim().isEmpty()) {
            ip = "unknown";
        }
        ip = ip.trim();
        checkDateRollover();

        if (DAILY_CACHE.containsKey(ip)) {
            return DAILY_CACHE.get(ip);
        }

        int count = 0;
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            if (conn != null) {
                ps = conn.prepareStatement(
                        "SELECT COUNT(*) AS total FROM `ip_register_logs` WHERE `ip` = ? AND `created_at` >= CURDATE();");
                ps.setString(1, ip);
                rs = ps.executeQuery();
                if (rs.next()) {
                    count = rs.getInt("total");
                }
            }
        } catch (Throwable e) {
            System.err.println("[AccountRegisterManager] Lỗi khi đếm số tài khoản của IP " + ip + ": " + e.getMessage());
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (SQLException e) {}
        }

        DAILY_CACHE.put(ip, count);
        return count;
    }

    /**
     * Kiểm tra xem IP có được phép tạo thêm tài khoản mới hôm nay hay không.
     * @param ip Địa chỉ IP của client
     * @return true nếu còn lượt tạo, false nếu đã đạt giới hạn
     */
    public static synchronized boolean canRegister(String ip) {
        int maxLimit = Manager.gI().max_register_ip_day;
        // Đặt <= 0 nghĩa là không giới hạn
        if (maxLimit <= 0) {
            return true;
        }

        // Nếu bật server_admin hoặc debug và kết nối từ localhost -> bỏ qua giới hạn
        if (isLocalIp(ip) && (Manager.gI().debug || Manager.gI().server_admin)) {
            return true;
        }

        int countToday = getRegisteredCountToday(ip);
        return countToday < maxLimit;
    }

    /**
     * Ghi nhận một lượt đăng ký tài khoản thành công của IP vào DB và Cache.
     * Lưu thẳng vào bảng ip_register_logs (đồng bộ với Web).
     * @param ip Địa chỉ IP của client
     * @param user Tên tài khoản vừa tạo
     */
    public static synchronized void recordRegister(String ip, String user) {
        if (ip == null || ip.trim().isEmpty()) {
            ip = "unknown";
        }
        ip = ip.trim();
        checkDateRollover();

        Connection conn = null;
        PreparedStatement ps = null;
        try {
            conn = SQL.gI().getCon();
            if (conn != null) {
                ps = conn.prepareStatement(
                        "INSERT INTO `ip_register_logs` (`ip`) VALUES (?);");
                ps.setString(1, ip);
                ps.executeUpdate();
            }
        } catch (Throwable e) {
            System.err.println("[AccountRegisterManager] Lỗi khi lưu log đăng ký cho IP " + ip + ": " + e.getMessage());
        } finally {
            try {
                if (ps != null) ps.close();
                if (conn != null) conn.close();
            } catch (SQLException e) {}
        }

        int newCount = DAILY_CACHE.getOrDefault(ip, 0) + 1;
        DAILY_CACHE.put(ip, newCount);

        System.out.println("[REGISTER] IP: " + ip + " đã tạo tài khoản '" + user + "' (Đã tạo hôm nay: "
                + newCount + "/" + Manager.gI().max_register_ip_day + ")");
    }
}
