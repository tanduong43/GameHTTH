package template;

import java.sql.Connection;
import java.sql.PreparedStatement;
import database.SQL;
import core.Util;

public class ActionLogger {

    public static void logRuby(String playerName, String action, long amount, long currentRuby) {
        String detail = String.format("%s | Lượng: %s | Ruby còn lại: %s", action, Util.number_format(amount), Util.number_format(currentRuby));
        insertLogDatabase(playerName, "ruby", detail);
    }

    public static void logRuby(String playerName, String action, long amount, long currentRuby, String mapInfo) {
        String detail = String.format("%s | Lượng: %s | Ruby còn lại: %s | Tại: %s", action, Util.number_format(amount), Util.number_format(currentRuby), mapInfo);
        insertLogDatabase(playerName, "ruby", detail);
    }

    public static void logBeri(String playerName, String action, long amount, long currentBeri) {
        String detail = String.format("%s | Lượng: %s | Beri còn lại: %s", action, Util.number_format(amount), Util.number_format(currentBeri));
        insertLogDatabase(playerName, "beri", detail);
    }

    public static void logBeri(String playerName, String action, long amount, long currentBeri, String mapInfo) {
        String detail = String.format("%s | Lượng: %s | Beri còn lại: %s | Tại: %s", action, Util.number_format(amount), Util.number_format(currentBeri), mapInfo);
        insertLogDatabase(playerName, "beri", detail);
    }

    public static void logExtol(String playerName, String action, long amount, long currentExtol) {
        String detail = String.format("%s | Lượng: %s | Extol còn lại: %s", action, Util.number_format(amount), Util.number_format(currentExtol));
        insertLogDatabase(playerName, "extol", detail);
    }

    public static void logExtol(String playerName, String action, long amount, long currentExtol, String mapInfo) {
        String detail = String.format("%s | Lượng: %s | Extol còn lại: %s | Tại: %s", action, Util.number_format(amount), Util.number_format(currentExtol), mapInfo);
        insertLogDatabase(playerName, "extol", detail);
    }

    public static void logItem(String playerName, String action, String itemName, int quantity) {
        String detail = String.format("%s | Vật phẩm: %s | Số lượng: %d", action, itemName, quantity);
        insertLogDatabase(playerName, "item", detail);
    }

    public static void logBuff(String playerName, String buffName, String details) {
        String detail = String.format("Buff: %s | Chi tiết: %s", buffName, details);
        insertLogDatabase(playerName, "buff", detail);
    }

    public static void logMarket(String playerName, String actionDetail) {
        insertLogDatabase(playerName, "market", actionDetail);
    }

    public static void logTrade(String playerName, String actionDetail) {
        insertLogDatabase(playerName, "trade", actionDetail);
    }

    public static void logItemDropPick(String playerName, String actionDetail) {
        // Đã bỏ theo yêu cầu người dùng: Không ghi log nhặt item/rơi đồ để tránh làm nặng và rác database
    }

    public static void logShop(String playerName, String actionDetail) {
        insertLogDatabase(playerName, "shop", actionDetail);
    }

    public static void logCoin(String playerName, String action, long amount, long currentCoin) {
        String detail = String.format("%s | Lượng: %s | Coin còn lại: %s", action, Util.number_format(amount), Util.number_format(currentCoin));
        insertLogDatabase(playerName, "coin", detail);
    }

    public static void insertLogDatabase(String playerName, String type, String actionDetail) {
        new Thread(() -> {
            Connection conn = null;
            PreparedStatement ps = null;
            try {
                conn = SQL.gI().getCon();
                if (conn != null) {
                    String query = "INSERT INTO player_logs (player_name, type, action) VALUES (?, ?, ?)";
                    ps = conn.prepareStatement(query);
                    ps.setString(1, playerName);
                    ps.setString(2, type);
                    ps.setString(3, actionDetail);
                    ps.executeUpdate();
                }
            } catch (Exception e) {
                System.err.println("Lỗi lưu Log Database: " + e.getMessage());
            } finally {
                try {
                    if (ps != null) ps.close();
                    if (conn != null) conn.close();
                } catch (Exception e) {}
            }
        }).start();
    }
}
