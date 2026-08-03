package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.DataTemplate;
import template.ItemFashionP2;
import template.ItemTemplate3;
import template.ItemTemplate4;
import template.ItemTemplate4_Info;
import template.ItemTemplate7;
import template.ItemTemplate8;

public class TichLuyNap {

    public static class RewardItem {
        public String name;
        public byte type;
        public short id;
        public short icon;
        public int quantity;

        public RewardItem(String name, byte type, short id, short icon, int quantity) {
            this.name = name;
            this.type = type;
            this.id = id;
            this.icon = icon;
            this.quantity = quantity;
        }
    }

    public static class Milestone {
        public int id;
        public String title;
        public int extolReq;
        public List<RewardItem> rewards = new ArrayList<>();

        public Milestone(int id, String title, int extolReq) {
            this.id = id;
            this.title = title;
            this.extolReq = extolReq;
        }

        public void addReward(String name, byte type, short id, short icon, int quantity) {
            rewards.add(new RewardItem(name, type, id, icon, quantity));
        }
    }

    public static List<Milestone> MILESTONES = new ArrayList<>();

    static {
        initMilestones();
    }

    /**
     * =====================================================================
     * CẤU HÌNH CÁC MỐC TÍCH LUỸ NẠP - CHỈNH SỬA TẠI ĐÂY
     * =====================================================================
     */
    private static void initMilestones() {
        MILESTONES.clear();

        // MỐC 1: 50k Extol
        Milestone m0 = new Milestone(50, "Tích luỹ nạp 50k Extol", 50000);
        m0.addReward("Thời trang Doflamingo", (byte) 105, (short) 127, (short) 127, 1);
        m0.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 10);
        m0.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 10);
        m0.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m0.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 20);
        m0.addReward("Đá Hổ phách 6", (byte) 4, (short) 367, (short) 367, 1);
        m0.addReward("Rương dial", (byte) 4, (short) 455, (short) 455, 5);
        MILESTONES.add(m0);

        // MỐC 2: 100k Extol
        Milestone m1 = new Milestone(100, "Tích luỹ nạp 100k Extol", 100000);
        m1.addReward("Thời trang Chấn thiên", (byte) 105, (short) 53, (short) 53, 1);
        m1.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 25);
        m1.addReward("Đá hải thạch 1", (byte) 4, (short) 221, (short) 221, 100);
        m1.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 30);
        m1.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 30);
        m1.addReward("Đá Hổ phách 6", (byte) 4, (short) 367, (short) 367, 3);
        m1.addReward("Lông vũ", (byte) 7, (short) 13, (short) 13, 200);
        m1.addReward("Rương dial", (byte) 4, (short) 455, (short) 455, 5);
        m1.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 20);
        m1.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 1);
        MILESTONES.add(m1);

        // MỐC 3: 200k Extol
        Milestone m2 = new Milestone(200, "Tích luỹ nạp 200k Extol", 200000);
        m2.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 50);
        m2.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 30);
        m2.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 50);
        m2.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 50);
        m2.addReward("Rương cam cùng hệ lv100", (byte) 4, (short) 131, (short) 131, 50);
        m2.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 10);
        m2.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 1);
        m2.addReward("Đá khảm ngẫu nhiên", (byte) 4, (short) 327, (short) 327, 20);
        m2.addReward("Kỹ năng đơn", (byte) 4, (short) 414, (short) 414, 1);
        m2.addReward("Đá Hổ phách 6", (byte) 4, (short) 367, (short) 367, 10);
        m2.addReward("Trang phục Bão tố", (byte) 105, (short) 55, (short) 55, 1);
        m2.addReward("Tiến cấp đơn", (byte) 4, (short) 413, (short) 413, 5);
        MILESTONES.add(m2);

        // MỐC 4: 300k Extol
        Milestone m3 = new Milestone(300, "Tích luỹ nạp 300k Extol", 300000);
        m3.addReward("Đá hải thạch 6", (byte) 4, (short) 226, (short) 226, 50);
        m3.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 60);
        m3.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 60);
        m3.addReward("Đá Hổ phách 6", (byte) 4, (short) 367, (short) 367, 5);
        m3.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 10);
        m3.addReward("Topaz - Saphia thần thoại", (byte) 4, (short) 655, (short) 655, 5);
        m3.addReward("Đá khảm siêu cấp", (byte) 4, (short) 324, (short) 324, 5);
        m3.addReward("Trang phục Thần Tài", (byte) 105, (short) 95, (short) 95, 1);
        m3.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 20);
        m3.addReward("Kỹ năng đơn", (byte) 4, (short) 414, (short) 414, 1);
        m3.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 3);
        MILESTONES.add(m3);

        // MỐC 5: 500k Extol
        Milestone m4 = new Milestone(500, "Tích luỹ nạp 500k Extol", 500000);
        m4.addReward("Đá hải thạch 6", (byte) 4, (short) 226, (short) 226, 100);
        m4.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 100);
        m4.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 100);
        m4.addReward("Đá Hổ phách 6", (byte) 4, (short) 367, (short) 367, 10);
        m4.addReward("Trang phục Mihawk", (byte) 105, (short) 118, (short) 118, 1);
        m4.addReward("Trang phục Raid Suit Judge", (byte) 105, (short) 116, (short) 116, 1);
        m4.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 10);
        m4.addReward("Hổ phách - Ruby thần thoại", (byte) 4, (short) 679, (short) 679, 5);
        m4.addReward("Đá khảm ngẫu nhiên", (byte) 4, (short) 327, (short) 327, 5);
        m4.addReward("Đá khảm siêu cấp S", (byte) 4, (short) 325, (short) 325, 5);
        m4.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 50);
        m4.addReward("Kỹ năng đơn", (byte) 4, (short) 414, (short) 414, 1);
        m4.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 5);
        MILESTONES.add(m4);

        // MỐC 6: 1M Extol
        Milestone m5 = new Milestone(1000, "Tích luỹ nạp 1M Extol", 1000000);
        m5.addReward("Đá hải thạch 6", (byte) 4, (short) 226, (short) 226, 150);
        m5.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 200);
        m5.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 200);
        m5.addReward("Trang phục Râu đen", (byte) 105, (short) 74, (short) 74, 1);
        m5.addReward("Đá khảm siêu cấp S", (byte) 4, (short) 325, (short) 325, 5);
        m5.addReward("Tiến cấp đơn", (byte) 4, (short) 413, (short) 413, 20);
        m5.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 100);
        m5.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 20);
        m5.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 6);
        m5.addReward("Rương dial", (byte) 4, (short) 455, (short) 455, 50);
        m5.addReward("Thời trang Mihawk Gold", (byte) 105, (short) 119, (short) 119, 1);
        MILESTONES.add(m5);

        // MỐC 7: 2M Extol
        Milestone m6 = new Milestone(2000, "Tích luỹ nạp 2M Extol", 2000000);
        m6.addReward("Thời trang Gol D.Roger", (byte) 105, (short) 128, (short) 128, 1);
        m6.addReward("Tiến cấp đơn", (byte) 4, (short) 413, (short) 413, 50);
        m6.addReward("Kỹ năng đơn", (byte) 4, (short) 414, (short) 414, 5);
        m6.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 15);
        m6.addReward("Đá hải thạch 6", (byte) 4, (short) 226, (short) 226, 200);
        m6.addReward("Đá khảm siêu cấp S", (byte) 4, (short) 325, (short) 325, 15);
        m6.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 50);
        m6.addReward("Rương dial", (byte) 4, (short) 455, (short) 455, 60);
        m6.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 200);
        m6.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 500);
        m6.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 500);
        MILESTONES.add(m6);

        // MỐC 8: 5M Extol
        Milestone m7 = new Milestone(5000, "Tích luỹ nạp 5M Extol", 5000000);
        m7.addReward("Thời trang Nami Wano", (byte) 105, (short) 125, (short) 125, 1);
        m7.addReward("Thời trang UTA", (byte) 105, (short) 123, (short) 123, 1);
        m7.addReward("Tiến cấp đơn", (byte) 4, (short) 413, (short) 413, 150);
        m7.addReward("Kỹ năng đơn", (byte) 4, (short) 414, (short) 414, 15);
        m7.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 40);
        m7.addReward("Đá hải thạch 6", (byte) 4, (short) 226, (short) 226, 800);
        m7.addReward("Đá khảm siêu cấp S", (byte) 4, (short) 325, (short) 325, 40);
        m7.addReward("Ruby - Saphia thần thoại", (byte) 4, (short) 660, (short) 660, 10);
        m7.addReward("Hổ phách - Saphia thần thoại", (byte) 4, (short) 681, (short) 681, 10);
        m7.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 100);
        m7.addReward("Rương dial", (byte) 4, (short) 455, (short) 455, 100);
        m7.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 500);
        m7.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 1000);
        m7.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 1000);
        MILESTONES.add(m7);
    }

    public static short getIcon(byte type, int id) {
        if (type == 4) {
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(id);
            if (it != null)
                return it.icon;
        } else if (type == 105) {
            template.ItemFashion it = template.ItemFashion.get_item(id);
            if (it != null)
                return it.idIcon;
        }
        return (short) id; // For standard items (3, 7, 8), the client does a local template lookup using
                           // the ID
    }

    private static String getRewardName(RewardItem reward) {
        String itemName = reward.name;
        if (reward.type == 4) {
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(reward.id);
            if (it != null)
                itemName = it.name;
        } else if (reward.type == 3) {
            ItemTemplate3 it = ItemTemplate3.get_it_by_id(reward.id);
            if (it != null)
                itemName = it.name;
        } else if (reward.type == 7) {
            ItemTemplate7 it = ItemTemplate7.get_it_by_id(reward.id);
            if (it != null)
                itemName = it.name;
        } else if (reward.type == 8) {
            ItemTemplate8 it = ItemTemplate8.get_it_by_id(reward.id);
            if (it != null)
                itemName = it.name;
        } else if (reward.type == 105) {
            template.ItemFashion it = template.ItemFashion.get_item(reward.id);
            if (it != null)
                itemName = it.name;
        }
        if (itemName == null || itemName.isEmpty()) {
            itemName = reward.name;
        }
        if (reward.type == 4 && reward.id == 1) {
            itemName = "Ruby";
        }
        return itemName;
    }

    private static String getRewardInfo(RewardItem reward) {
        if (reward.type == 4) {
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(reward.id);
            if (it != null) {
                ItemTemplate4_Info tempInfo = ItemTemplate4_Info.get_by_id(it.indexInfoPotion);
                if (tempInfo != null && tempInfo.info != null && !tempInfo.info.isEmpty()) {
                    return tempInfo.info;
                }
            }
        } else if (reward.type == 7) {
            ItemTemplate7 it = ItemTemplate7.get_it_by_id(reward.id);
            if (it != null && it.name != null) {
                return it.name;
            }
        } else if (reward.type == 3) {
            ItemTemplate3 it = ItemTemplate3.get_it_by_id(reward.id);
            if (it != null && it.name != null) {
                return it.name;
            }
        }
        return "Chưa có thông tin";
    }

    private static boolean isRubyReward(RewardItem reward) {
        return reward.type == 4 && reward.id == 1;
    }

    private static int countRequiredBagSlots(Player p, Milestone milestone) {
        int requiredSpace = 0;
        for (RewardItem reward : milestone.rewards) {
            if (isRubyReward(reward)) {
                continue;
            }
            if (reward.type == 3) {
                requiredSpace++;
            } else if (reward.type == 4 || reward.type == 7) {
                if (p.item.total_item_bag_by_id(reward.type, reward.id) == 0) {
                    requiredSpace++;
                }
                if ((p.item.total_item_bag_by_id(reward.type, reward.id)
                        + reward.quantity) > DataTemplate.MAX_ITEM_IN_BAG) {
                    return Integer.MAX_VALUE;
                }
            }
        }
        return requiredSpace;
    }

    public static void syncAccountTichNap(Player p) {
        if (p.conn == null)
            return;
        java.sql.Connection connection = null;
        java.sql.PreparedStatement ps = null;
        java.sql.ResultSet rs = null;
        try {
            connection = database.SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `tichnap`, `claimed_milestones` FROM `accounts` WHERE `user` = ? LIMIT 1;");
            ps.setString(1, p.conn.user);
            rs = ps.executeQuery();
            if (rs.next()) {
                p.conn.tichnap = rs.getInt("tichnap");
                p.conn.claimed_milestones = rs.getString("claimed_milestones");
                if (p.conn.claimed_milestones == null) {
                    p.conn.claimed_milestones = "";
                }

                p.claimedMilestones.clear();
                if (!p.conn.claimed_milestones.isEmpty()) {
                    for (String s : p.conn.claimed_milestones.split(",")) {
                        try {
                            p.claimedMilestones.add(Integer.parseInt(s.trim()));
                        } catch (NumberFormatException e) {
                            // ignore
                        }
                    }
                }
            }
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (rs != null)
                    rs.close();
                if (ps != null)
                    ps.close();
                if (connection != null)
                    connection.close();
            } catch (java.sql.SQLException e) {
                e.printStackTrace();
            }
        }
    }

    public static void sendUI(Player p) throws IOException {
        syncAccountTichNap(p);
        System.out.println("[TichLuyNap] sendUI called for player: " + p.name
                + " | tichnap=" + p.conn.tichnap
                + " | milestones=" + MILESTONES.size()
                + " | isdie=" + p.isdie);

        Message m = new Message(-90);
        m.writer().writeByte(0); // type = 0: Open UI
        m.writer().writeInt(p.conn.tichnap); // Tổng điểm tích luỹ
        m.writer().writeByte(MILESTONES.size()); // số mốc

        for (int i = 0; i < MILESTONES.size(); i++) {
            Milestone milestone = MILESTONES.get(i);

            byte status = 0;
            if (p.claimedMilestones.contains(milestone.id)) {
                status = 2;
            } else if (p.conn.tichnap >= milestone.extolReq) {
                status = 1;
            }

            m.writer().writeByte(i); // index/ID mốc dạng byte
            m.writer().writeInt(milestone.extolReq); // extolReq (số extol yêu cầu)
            m.writer().writeByte(status); // status (trạng thái nhận)
            m.writer().writeShort(milestone.rewards.size()); // số phần thưởng

            for (RewardItem reward : milestone.rewards) {
                m.writer().writeUTF(getRewardName(reward)); // tên item
                m.writer().writeByte(reward.type); // loại item
                m.writer().writeShort(getIcon(reward.type, reward.id)); // lấy icon chuẩn từ template thay vì config sai
                m.writer().writeShort(reward.quantity); // số lượng
                m.writer().writeByte(isRubyReward(reward) ? 5 : 0);
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
        System.out.println("[TichLuyNap] sendUI (Opcode -90) packet sent OK to: " + p.name);
    }

    public static Milestone getMilestoneByIndex(int index) {
        if (index >= 0 && index < MILESTONES.size()) {
            return MILESTONES.get(index);
        }
        return null;
    }

    public static Milestone getMilestoneById(int id) {
        for (Milestone m : MILESTONES) {
            if (m.id == id) {
                return m;
            }
        }
        return null;
    }

    public static void claimReward(Player p, int index) throws IOException {
        syncAccountTichNap(p);
        Milestone milestone = getMilestoneByIndex(index);
        if (milestone == null) {
            return;
        }

        if (p.conn.tichnap < milestone.extolReq) {
            Service.send_box_ThongBao_OK(p, "Bạn chưa đạt mốc tích lũy này!");
            return;
        }

        if (p.claimedMilestones.contains(milestone.id)) {
            Service.send_box_ThongBao_OK(p, "Bạn đã nhận quà mốc này rồi!");
            return;
        }

        int requiredSpace = countRequiredBagSlots(p, milestone);
        if (requiredSpace == Integer.MAX_VALUE) {
            Service.send_box_ThongBao_OK(p, "Số lượng vật phẩm trong hành trang vượt quá giới hạn!");
            return;
        }
        if (requiredSpace > p.item.able_bag()) {
            Service.send_box_ThongBao_OK(p, "Hành trang của bạn không đủ chỗ trống! Cần ít nhất "
                    + requiredSpace + " ô trống.");
            return;
        }

        for (RewardItem reward : milestone.rewards) {
            if (isRubyReward(reward)) {
                p.update_ngoc(reward.quantity);
            } else if (reward.type == 105) {
                ItemFashionP2 temp2 = new ItemFashionP2();
                temp2.id = reward.id;
                p.fashion.add(temp2);
            } else if (!p.item.add_item_bag47(reward.type, reward.id, reward.quantity)) {
                Service.send_box_ThongBao_OK(p, "Không thể thêm "
                        + getRewardName(reward) + " vào hành trang!");
                return;
            }
        }

        p.claimedMilestones.add(milestone.id);

        // Cập nhật lại chuỗi claimed_milestones trong Session tài khoản
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < p.claimedMilestones.size(); i++) {
            sb.append(p.claimedMilestones.get(i));
            if (i < p.claimedMilestones.size() - 1) {
                sb.append(",");
            }
        }
        p.conn.claimed_milestones = sb.toString();

        // Lưu lại cột claimed_milestones vào bảng accounts dưới DB
        java.sql.Connection connection = null;
        java.sql.PreparedStatement ps = null;
        try {
            connection = database.SQL.gI().getCon();
            ps = connection.prepareStatement("UPDATE `accounts` SET `claimed_milestones` = ? WHERE `user` = ?");
            ps.setString(1, p.conn.claimed_milestones);
            ps.setString(2, p.conn.user);
            ps.executeUpdate();
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (ps != null)
                    ps.close();
                if (connection != null)
                    connection.close();
            } catch (java.sql.SQLException e) {
                e.printStackTrace();
            }
        }

        p.update_money();
        p.item.update_Inventory(-1, false);

        Service.send_box_ThongBao_OK(p,
                "Nhận quà mốc " + Util.number_format(milestone.extolReq) + " Extol thành công!");

        // Gửi type=2 để client gọi setCmdDaNhanIndex(index) cập nhật trạng thái đã nhận
        // trong UI
        Message m = new Message(-90);
        m.writer().writeByte(2); // type 2: update claimed status in UI
        m.writer().writeByte(index); // index/ID mốc dạng byte
        p.conn.addmsg(m);
        m.cleanup();

        // Refresh toàn bộ UI để phản ánh trạng thái mới
        sendUI(p);
    }
}
