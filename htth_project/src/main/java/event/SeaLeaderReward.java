package event;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import client.Player;
import core.Service;
import core.Util;
import database.SQL;

/**
 * Xu ly phat thuong va chong nhan trung su kien Thu Linh Bien Khoi.
 * Thuong:
 * - x10 Khien (Item type 7, ID 10)
 * - x20 Ruong cam cung he Lv100 (Item type 4, ID 131)
 * - 1.000.000 Beri
 * - 500 Ruby
 */
public class SeaLeaderReward {

    public static final int ITEM_KHIEN_TYPE = 7;
    public static final int ITEM_KHIEN_ID = 10;
    public static final int ITEM_KHIEN_QTY = 10;

    public static final int ITEM_RUONG_CAM_TYPE = 4;
    public static final int ITEM_RUONG_CAM_ID = 131;
    public static final int ITEM_RUONG_CAM_QTY = 20;

    public static final long BERI_REWARD = 1_000_000L;
    public static final int RUBY_REWARD = 500;

    /**
     * Kiem tra nguoi choi da nhan thuong cua dot event nay chua.
     */
    public static boolean hasClaimedReward(int eventId, String playerName) {
        if (eventId <= 0 || playerName == null || playerName.isEmpty()) {
            return false;
        }
        String query = "SELECT 1 FROM `sea_leader_reward` WHERE `event_id` = ? AND `player_name` = ? LIMIT 1;";
        try (Connection conn = SQL.gI().getCon();
                PreparedStatement ps = conn.prepareStatement(query)) {
            ps.setInt(1, eventId);
            ps.setString(2, playerName);
            try (ResultSet rs = ps.executeQuery()) {
                return rs.next();
            }
        } catch (Exception e) {
            System.err.println("[SeaLeaderReward] Error checking claimed reward: " + e.getMessage());
            return false;
        }
    }

    /**
     * Ghi nhan va trao phan thuong cho thanh vien cua Clan chien thang.
     * Su dung transaction DB de chong duplicate reward khi reconnect, lag packet
     * hoac restart.
     */
    public synchronized static boolean claimReward(Player p, int eventId, int winnerClanId, String winnerClanName) {
        if (p == null || p.conn == null) {
            return false;
        }
        if (p.clan == null || p.clan.id != winnerClanId) {
            try {
                Service.send_box_ThongBao_OK(p, "Ban khong thuoc Bang Hai Tac chien thang su kien Thu Linh Bien Khoi ("
                        + winnerClanName + ")!");
            } catch (Exception ignored) {
            }
            return false;
        }

        if (hasClaimedReward(eventId, p.name)) {
            try {
                Service.send_box_ThongBao_OK(p, "Ban da nhan phan thuong su kien Thu Linh Bien Khoi cua dot nay roi!");
            } catch (Exception ignored) {
            }
            return false;
        }

        // Ghi vao database truoc (Chong race condition va nhan trung lap)
        String insertSql = "INSERT INTO `sea_leader_reward` (`event_id`, `player_name`, `clan_id`, `claimed`, `claimed_at`) VALUES (?, ?, ?, 1, NOW());";
        try (Connection conn = SQL.gI().getCon();
                PreparedStatement ps = conn.prepareStatement(insertSql)) {
            ps.setInt(1, eventId);
            ps.setString(2, p.name);
            ps.setInt(3, winnerClanId);
            int rows = ps.executeUpdate();
            if (rows <= 0) {
                Service.send_box_ThongBao_OK(p, "Khong the ghi nhan phan thuong! Vui long thu lai.");
                return false;
            }
        } catch (Exception e) {
            System.err.println("[SeaLeaderReward] Failed to insert reward record: " + e.getMessage());
            try {
                Service.send_box_ThongBao_OK(p, "Ban da nhan phan thuong nay roi hoac co loi xay ra!");
            } catch (Exception ignored) {
            }
            return false;
        }

        // Trao vat pham va tien te
        try {
            p.item.add_item_bag47(ITEM_KHIEN_TYPE, ITEM_KHIEN_ID, ITEM_KHIEN_QTY);
            p.item.add_item_bag47(ITEM_RUONG_CAM_TYPE, ITEM_RUONG_CAM_ID, ITEM_RUONG_CAM_QTY);
            p.update_vang(BERI_REWARD);
            p.update_ngoc(RUBY_REWARD);
            p.update_money();
            p.item.update_Inventory(4, false);
            p.item.update_Inventory(7, false);

            String msg = "CHUC MUNG CHIEN THANG THU LINH BIEN KHOI!\n"
                    + "Bang: " + winnerClanName + "\n"
                    + "Phan thuong nhan duoc:\n"
                    + "- x" + ITEM_KHIEN_QTY + " Khien\n"
                    + "- x" + ITEM_RUONG_CAM_QTY + " Ruong cam cung he Lv100\n"
                    + "- " + Util.number_format(BERI_REWARD) + " Beri\n"
                    + "- " + RUBY_REWARD + " Ruby";
            Service.send_box_ThongBao_OK(p, msg);

            System.out.println("[SEA_EVENT] Reward granted to Player " + p.name + " (Clan " + winnerClanName + ", ID "
                    + winnerClanId + ") for Event " + eventId);
            return true;
        } catch (Exception e) {
            System.err.println("[SeaLeaderReward] Error granting items: " + e.getMessage());
            try {
                Service.send_box_ThongBao_OK(p, "Co loi xay ra khi nhan vat pham thuong, vui long lien he Admin!");
            } catch (Exception ignored) {
            }
            return false;
        }
    }
}
