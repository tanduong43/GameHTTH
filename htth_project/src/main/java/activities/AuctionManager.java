package activities;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import client.Player;
import core.Service;
import core.Util;
import database.SQL;
import map.Map;
import template.ActionLogger;
import template.Item_wear;

public class AuctionManager {
    private static AuctionManager instance;

    private final List<AuctionItem> items = Collections.synchronizedList(new ArrayList<>());
    private final Set<Player> viewers = Collections.synchronizedSet(new HashSet<>());
    private ScheduledExecutorService scheduler;
    private int syncCounter = 0;

    public static synchronized AuctionManager gI() {
        if (instance == null) {
            instance = new AuctionManager();
        }
        return instance;
    }

    public void init() {
        checkAndCreateTables();
        syncWithDatabase();

        scheduler = Executors.newSingleThreadScheduledExecutor();
        scheduler.scheduleAtFixedRate(this::tick, 1, 1, TimeUnit.SECONDS);
        System.out.println("[AUCTION] Hệ thống Đấu Giá bằng Coin đã khởi động. Đang quản lý " + items.size() + " vật phẩm từ Web Admin.");
    }

    private void checkAndCreateTables() {
        Connection conn = null;
        Statement st = null;
        try {
            conn = SQL.gI().getCon();
            st = conn.createStatement();
            st.execute("CREATE TABLE IF NOT EXISTS `auction_items` ("
                    + "`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY, "
                    + "`slot_id` TINYINT NOT NULL DEFAULT 0, "
                    + "`name` VARCHAR(255) NOT NULL, "
                    + "`category` TINYINT NOT NULL, "
                    + "`template_id` SMALLINT NOT NULL, "
                    + "`quantity` INT NOT NULL DEFAULT 1, "
                    + "`color` TINYINT NOT NULL DEFAULT 0, "
                    + "`item_options` TEXT NULL, "
                    + "`start_price` INT NOT NULL DEFAULT 100, "
                    + "`current_price` INT NOT NULL DEFAULT 100, "
                    + "`step_price` INT NOT NULL DEFAULT 10, "
                    + "`buyout_price` INT NOT NULL DEFAULT 0, "
                    + "`highest_bidder_id` INT NOT NULL DEFAULT -1, "
                    + "`highest_bidder_name` VARCHAR(100) NULL, "
                    + "`highest_bidder_user` VARCHAR(255) NULL, "
                    + "`end_time` BIGINT NOT NULL, "
                    + "`status` TINYINT NOT NULL DEFAULT 0, "
                    + "INDEX `idx_status` (`status`)"
                    + ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

            st.execute("CREATE TABLE IF NOT EXISTS `auction_history` ("
                    + "`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY, "
                    + "`auction_id` INT NOT NULL, "
                    + "`player_id` INT NOT NULL, "
                    + "`player_name` VARCHAR(100) NOT NULL, "
                    + "`bid_type` TINYINT NOT NULL, "
                    + "`coin_amount` INT NOT NULL, "
                    + "`time` TIMESTAMP DEFAULT CURRENT_TIMESTAMP, "
                    + "INDEX `idx_auction_player` (`auction_id`, `player_id`)"
                    + ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            closeSQL(st, conn);
        }
    }

    public synchronized boolean syncWithDatabase() {
        boolean changed = false;
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.prepareStatement("SELECT * FROM `auction_items` WHERE `status` IN (0, 1) ORDER BY `slot_id` ASC LIMIT 20;");
            rs = ps.executeQuery();
            List<Integer> dbItemIds = new ArrayList<>();
            while (rs.next()) {
                int dbId = rs.getInt("id");
                dbItemIds.add(dbId);

                AuctionItem existing = null;
                synchronized (items) {
                    for (AuctionItem it : items) {
                        if (it.id == dbId) {
                            existing = it;
                            break;
                        }
                    }
                }

                if (existing == null) {
                    // Item mới được tạo từ Web Admin
                    AuctionItem item = new AuctionItem();
                    item.id = dbId;
                    item.slotId = rs.getByte("slot_id");
                    item.name = rs.getString("name");
                    item.category = rs.getByte("category");
                    item.templateId = rs.getShort("template_id");
                    item.quantity = rs.getInt("quantity");
                    item.color = rs.getByte("color");
                    item.itemOptions = rs.getString("item_options");
                    item.startPrice = rs.getInt("start_price");
                    item.currentPrice = rs.getInt("current_price");
                    item.stepPrice = rs.getInt("step_price");
                    item.buyoutPrice = rs.getInt("buyout_price");
                    item.highestBidderId = rs.getInt("highest_bidder_id");
                    item.highestBidderName = rs.getString("highest_bidder_name");
                    item.highestBidderUser = rs.getString("highest_bidder_user");
                    item.endTime = rs.getLong("end_time");
                    item.status = rs.getByte("status");

                    if (item.status == 0 && item.getTimeRemainSeconds() <= 0) {
                        item.status = 1;
                    }
                    items.add(item);
                    changed = true;
                }
            }

            // Xóa item trong bộ nhớ nếu trên DB đã xóa hoặc đổi trạng thái
            synchronized (items) {
                for (int i = items.size() - 1; i >= 0; i--) {
                    if (!dbItemIds.contains(items.get(i).id)) {
                        items.remove(i);
                        changed = true;
                    }
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            closeSQL(rs, ps, conn);
        }
        return changed;
    }

    private void tick() {
        try {
            boolean hasChange = false;
            long now = System.currentTimeMillis();
            synchronized (items) {
                for (int i = items.size() - 1; i >= 0; i--) {
                    AuctionItem item = items.get(i);
                    if (item.status == 0 && now >= item.endTime) {
                        if (item.highestBidderId != -1) {
                            item.status = 1; // Đã kết thúc, chờ nhận
                            updateItemStatus(item.id, (byte) 1);
                            hasChange = true;

                            if (item.highestBidderName != null && !item.highestBidderName.isEmpty()) {
                                Player winner = Map.get_player_by_name_allmap(item.highestBidderName);
                                if (winner != null) {
                                    Service.send_box_ThongBao_OK(winner, "Chúc mừng bạn đã đấu giá thành công [" + item.name + "]! Vui lòng vào màn hình Đấu Giá để nhận vật phẩm.");
                                }
                            }
                        } else {
                            // Không có ai đấu giá -> hết hạn
                            item.status = 3;
                            updateItemStatus(item.id, (byte) 3);
                            items.remove(i);
                            hasChange = true;
                        }
                    }
                }
            }

            // Đồng bộ dữ liệu mới từ Web Admin mỗi 5 giây
            syncCounter++;
            if (syncCounter >= 5) {
                syncCounter = 0;
                if (syncWithDatabase()) {
                    hasChange = true;
                }
            }

            if (hasChange) {
                broadcastListToViewers();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void openAuctionScreen(Player p) {
        if (p == null) {
            return;
        }
        viewers.add(p);
        try {
            Service.send_auction_list(p, new ArrayList<>(items));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void closeAuctionScreen(Player p) {
        if (p != null) {
            viewers.remove(p);
        }
    }

    public synchronized void bidItem(Player p, byte slotId) {
        if (p == null || p.conn == null) {
            return;
        }

        try {
            AuctionItem target = getItemBySlotId(slotId);
            if (target == null) {
                Service.send_box_ThongBao_OK(p, "Vật phẩm đấu giá không tồn tại!");
                return;
            }

            synchronized (target) {
                if (target.status != 0 || target.getTimeRemainSeconds() <= 0) {
                    Service.send_box_ThongBao_OK(p, "Phiên đấu giá cho vật phẩm này đã kết thúc!");
                    return;
                }

                if (target.highestBidderId == p.id) {
                    Service.send_box_ThongBao_OK(p, "Bạn đang là người trả giá cao nhất!");
                    return;
                }

                int nextPrice = target.currentPrice + target.stepPrice;

                if (p.conn.coin < nextPrice) {
                    Service.send_box_ThongBao_OK(p, "Bạn không đủ " + Util.number_format(nextPrice) + " Coin để đấu giá!");
                    return;
                }

                // Trừ Coin người mới
                if (!p.update_coin(-nextPrice)) {
                    return;
                }

                // Hoàn Coin cho người giữ giá cũ (nếu có)
                if (target.highestBidderId != -1 && target.highestBidderUser != null && !target.highestBidderUser.isEmpty()) {
                    refundCoin(target.highestBidderId, target.highestBidderName, target.highestBidderUser, target.currentPrice, target.name);
                }

                // Cập nhật thông tin item
                target.currentPrice = nextPrice;
                target.highestBidderId = p.id;
                target.highestBidderName = p.name;
                target.highestBidderUser = p.conn.user;

                // Anti-snipe: Nếu còn dưới 30 giây thì cộng thêm 30 giây
                if (target.getTimeRemainSeconds() < 30) {
                    target.endTime = System.currentTimeMillis() + 30_000L;
                }

                // Lưu vào database
                updateBidInDB(target.id, nextPrice, p.id, p.name, p.conn.user, target.endTime);
                logAuctionHistory(target.id, p.id, p.name, (byte) 1, nextPrice);

                // Broadcast cập nhật giá mới cho tất cả viewers
                Service.send_auction_update(target, p, viewers);

                Service.send_box_ThongBao_OK(p, "Đấu giá thành công! Mức giá mới: " + Util.number_format(nextPrice) + " Coin.");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public synchronized void buyoutItem(Player p, byte slotId) {
        if (p == null || p.conn == null) {
            return;
        }

        try {
            AuctionItem target = getItemBySlotId(slotId);
            if (target == null) {
                Service.send_box_ThongBao_OK(p, "Vật phẩm đấu giá không tồn tại!");
                return;
            }

            synchronized (target) {
                if (target.status != 0 || target.getTimeRemainSeconds() <= 0) {
                    Service.send_box_ThongBao_OK(p, "Phiên đấu giá cho vật phẩm này đã kết thúc!");
                    return;
                }

                if (target.buyoutPrice <= 0) {
                    Service.send_box_ThongBao_OK(p, "Vật phẩm này không có giá chốt mua ngay!");
                    return;
                }

                if (p.conn.coin < target.buyoutPrice) {
                    Service.send_box_ThongBao_OK(p, "Bạn không đủ " + Util.number_format(target.buyoutPrice) + " Coin để mua giá chốt!");
                    return;
                }

                // Trừ Coin người mua
                if (!p.update_coin(-target.buyoutPrice)) {
                    return;
                }

                // Hoàn Coin cho người đang giữ giá trước đó
                if (target.highestBidderId != -1 && target.highestBidderUser != null && !target.highestBidderUser.isEmpty()) {
                    refundCoin(target.highestBidderId, target.highestBidderName, target.highestBidderUser, target.currentPrice, target.name);
                }

                target.currentPrice = target.buyoutPrice;
                target.highestBidderId = p.id;
                target.highestBidderName = p.name;
                target.highestBidderUser = p.conn.user;
                target.endTime = System.currentTimeMillis();
                target.status = 1; // Chờ nhận quà

                // Lưu vào database
                updateBuyoutInDB(target.id, target.buyoutPrice, p.id, p.name, p.conn.user);
                logAuctionHistory(target.id, p.id, p.name, (byte) 2, target.buyoutPrice);

                // Broadcast cập nhật tới viewers
                Service.send_auction_update(target, p, viewers);

                Service.send_box_ThongBao_OK(p, "Bạn đã mua giá chốt thành công! Hãy nhấn nút 'Nhận' để lấy vật phẩm vào hành trang.");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public synchronized void claimItem(Player p, byte slotId) {
        if (p == null || p.item == null) {
            return;
        }

        try {
            AuctionItem target = getItemBySlotId(slotId);
            if (target == null) {
                Service.send_box_ThongBao_OK(p, "Vật phẩm không tồn tại!");
                return;
            }

            synchronized (target) {
                if (!target.isClaimable(p.id)) {
                    Service.send_box_ThongBao_OK(p, "Bạn không phải là người chiến thắng hoặc phiên đấu giá chưa kết thúc!");
                    return;
                }

                if (p.item.able_bag() <= 0) {
                    Service.send_box_ThongBao_OK(p, "Hành trang của bạn không đủ chỗ trống!");
                    return;
                }

                // Trao quà tương ứng
                if (target.category == 3) {
                    Item_wear it = new Item_wear();
                    it.setup_template_by_id(target.templateId);
                    p.item.add_item_bag3(it);
                } else if (target.category == 4 || target.category == 7) {
                    p.item.add_item_bag47((byte) target.category, target.templateId, target.quantity);
                }
                p.item.update_Inventory(-1, false);

                // Cập nhật trạng thái đã nhận
                target.status = 2;
                updateItemStatus(target.id, (byte) 2);
                logAuctionHistory(target.id, p.id, p.name, (byte) 3, target.currentPrice);

                // Xoá khỏi danh sách đang hoạt động
                items.remove(target);
                broadcastListToViewers();

                Service.send_box_ThongBao_OK(p, "Nhận thành công: " + target.name + " (x" + target.quantity + ")!");

                // Refresh lại danh sách cho người nhận
                openAuctionScreen(p);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void refundCoin(int playerId, String playerName, String username, int amount, String itemName) {
        try {
            Player onlineP = Map.get_player_by_name_allmap(playerName);
            if (onlineP != null && onlineP.conn != null) {
                onlineP.update_coin(amount);
                Service.send_box_ThongBao_OK(onlineP, "Bạn đã bị người khác vượt giá tại phiên đấu giá [" + itemName + "]! Hoàn trả lại " + Util.number_format(amount) + " Coin.");
            } else {
                // Offline: Cộng trực tiếp vào bảng accounts
                Connection conn = null;
                PreparedStatement ps = null;
                try {
                    conn = SQL.gI().getCon();
                    ps = conn.prepareStatement("UPDATE `accounts` SET `coin` = `coin` + ? WHERE BINARY `user` = ?;");
                    ps.setInt(1, amount);
                    ps.setString(2, username);
                    ps.executeUpdate();
                } catch (SQLException e) {
                    e.printStackTrace();
                } finally {
                    closeSQL(ps, conn);
                }
                ActionLogger.logCoin(playerName, "Hoàn Coin đấu giá (offline): " + itemName, amount, -1);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private AuctionItem getItemBySlotId(byte slotId) {
        synchronized (items) {
            for (AuctionItem it : items) {
                if (it.slotId == slotId) {
                    return it;
                }
            }
        }
        return null;
    }

    private void broadcastListToViewers() {
        synchronized (viewers) {
            viewers.removeIf(v -> v == null || v.conn == null || !v.conn.connected);
            for (Player viewer : viewers) {
                try {
                    Service.send_auction_list(viewer, new ArrayList<>(items));
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private void updateBidInDB(int itemId, int newPrice, int bidderId, String bidderName, String username, long endTime) {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.prepareStatement("UPDATE `auction_items` SET `current_price` = ?, `highest_bidder_id` = ?, `highest_bidder_name` = ?, `highest_bidder_user` = ?, `end_time` = ? WHERE `id` = ?;");
            ps.setInt(1, newPrice);
            ps.setInt(2, bidderId);
            ps.setString(3, bidderName);
            ps.setString(4, username);
            ps.setLong(5, endTime);
            ps.setInt(6, itemId);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            closeSQL(ps, conn);
        }
    }

    private void updateBuyoutInDB(int itemId, int buyoutPrice, int bidderId, String bidderName, String username) {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.prepareStatement("UPDATE `auction_items` SET `current_price` = ?, `highest_bidder_id` = ?, `highest_bidder_name` = ?, `highest_bidder_user` = ?, `status` = 1, `end_time` = ? WHERE `id` = ?;");
            ps.setInt(1, buyoutPrice);
            ps.setInt(2, bidderId);
            ps.setString(3, bidderName);
            ps.setString(4, username);
            ps.setLong(5, System.currentTimeMillis());
            ps.setInt(6, itemId);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            closeSQL(ps, conn);
        }
    }

    private void updateItemStatus(int itemId, byte status) {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.prepareStatement("UPDATE `auction_items` SET `status` = ? WHERE `id` = ?;");
            ps.setByte(1, status);
            ps.setInt(2, itemId);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            closeSQL(ps, conn);
        }
    }

    private void logAuctionHistory(int auctionId, int playerId, String playerName, byte bidType, int coinAmount) {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.prepareStatement("INSERT INTO `auction_history` (`auction_id`, `player_id`, `player_name`, `bid_type`, `coin_amount`) VALUES (?, ?, ?, ?, ?);");
            ps.setInt(1, auctionId);
            ps.setInt(2, playerId);
            ps.setString(3, playerName);
            ps.setByte(4, bidType);
            ps.setInt(5, coinAmount);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            closeSQL(ps, conn);
        }
    }

    private void closeSQL(AutoCloseable... closeables) {
        for (AutoCloseable c : closeables) {
            if (c != null) {
                try {
                    c.close();
                } catch (Exception ignored) {
                }
            }
        }
    }
}
