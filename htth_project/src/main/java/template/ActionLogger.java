package template;

import java.sql.Connection;
import java.sql.PreparedStatement;
import database.SQL;

public class ActionLogger {

    public static void logRuby(String playerName, String action, int amount, int currentRuby) {
        String detail = String.format("%s | L\u01B0\u1EE3ng: %d | Ruby còn l\u1EA1i: %d", action, amount, currentRuby);
        insertLogDatabase(playerName, "ruby", detail);
    }

    public static void logItem(String playerName, String action, String itemName, int quantity) {
        String detail = String.format("%s | V\u1EADt ph\u1EA9m: %s | S\u1ED1 l\u01B0\u1EE3ng: %d", action, itemName, quantity);
        insertLogDatabase(playerName, "item", detail);
    }

    public static void logBuff(String playerName, String buffName, String details) {
        String detail = String.format("Buff: %s | Chi ti\u1EBFt: %s", buffName, details);
        insertLogDatabase(playerName, "buff", detail);
    }

    public static void logMarket(String playerName, String actionDetail) {
        insertLogDatabase(playerName, "market", actionDetail);
    }

    public static void logTrade(String playerName, String actionDetail) {
        insertLogDatabase(playerName, "trade", actionDetail);
    }

    public static void logItemDropPick(String playerName, String actionDetail) {
        insertLogDatabase(playerName, "drop_pick", actionDetail);
    }

    public static void logShop(String playerName, String actionDetail) {
        insertLogDatabase(playerName, "shop", actionDetail);
    }

    public static void logCoin(String playerName, String action, int amount, int currentCoin) {
        String detail = String.format("%s | Lượng: %d | Coin còn lại: %d", action, amount, currentCoin);
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
                System.err.println("L\u1ED7i l\u01B0u Log Database: " + e.getMessage());
            } finally {
                try {
                    if (ps != null) ps.close();
                    if (conn != null) conn.close();
                } catch (Exception e) {}
            }
        }).start();
    }
}
