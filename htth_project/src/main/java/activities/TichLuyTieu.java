package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.ItemFashionP2;
import template.ItemTemplate3;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.ItemTemplate8;

public class TichLuyTieu {

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
        public int rubyReq;
        public List<RewardItem> rewards = new ArrayList<>();

        public Milestone(int id, String title, int rubyReq) {
            this.id = id;
            this.title = title;
            this.rubyReq = rubyReq;
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
     * CAU HINH CAC MOC TICH TIEU RUBY - CHINH SUA TAI DAY
     * =====================================================================
     */
    private static void initMilestones() {
        MILESTONES.clear();

        // MOC 1: 500 Ruby
        Milestone m1 = new Milestone(500, "Tích tiêu 500 Ruby", 500);
        m1.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 100);
        m1.addReward("Bột bạc", (byte) 7, (short) 5, (short) 6505, 50);
        m1.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 10);
        m1.addReward("Đá ác quỷ", (byte) 7, (short) 9, (short) 2108, 100);
        MILESTONES.add(m1);

        // MOC 2: 1000 Ruby
        Milestone m2 = new Milestone(1000, "Tích tiêu 1000 Ruby", 1000);
        m2.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 200);
        m2.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m2.addReward("Đá ác quỷ", (byte) 7, (short) 9, (short) 2108, 200);
        m2.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 1);
        m2.addReward("Hải thạch 5", (byte) 4, (short) 225, (short) 225, 5);
        MILESTONES.add(m2);

        // MOC 3: 3000 Ruby
        Milestone m3 = new Milestone(3000, "Tích tiêu 3000 Ruby", 3000);
        m3.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 200);
        m3.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m3.addReward("Đá ác quỷ", (byte) 7, (short) 9, (short) 2108, 200);
        m3.addReward("Hải thạch 5", (byte) 4, (short) 225, (short) 225, 5);
        m3.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 1);
        m3.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m3.addReward("Vé vòng quay may mắn", (byte) 4, (short) 232, (short) 232, 20);
        MILESTONES.add(m3);

        // MOC 4: 5000 Ruby
        Milestone m4 = new Milestone(5000, "Tích tiêu 5000 Ruby", 5000);
        m4.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 200);
        m4.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m4.addReward("Đá ác quỷ", (byte) 7, (short) 9, (short) 2108, 200);
        m4.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 5);
        m4.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 2);
        m4.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m4.addReward("Vé vòng quay may mắn", (byte) 4, (short) 232, (short) 232, 20);
        MILESTONES.add(m4);

        // MOC 5: 10000 Ruby
        Milestone m5 = new Milestone(10000, "Tích tiêu 10000 Ruby", 10000);
        m5.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 200);
        m5.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m5.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 5);
        m5.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 3);
        m5.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m5.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 10);
        m5.addReward("Vé vòng quay may mắn", (byte) 4, (short) 232, (short) 232, 20);
        MILESTONES.add(m5);

        // MOC 6: 30000 Ruby
        Milestone m6 = new Milestone(30000, "Tích tiêu 30000 Ruby", 30000);
        m6.addReward("Chuột thần tài", (byte) 105, (short) 77, (short) 74, 1);
        m6.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 500);
        m6.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m6.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 5);
        m6.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 3);
        m6.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m6.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 10);
        m6.addReward("Rương cam cùng hệ lv1", (byte) 4, (short) 131, (short) 131, 50);
        MILESTONES.add(m6);

        // MOC 7: 50000 Ruby
        Milestone m7 = new Milestone(50000, "Tích tiêu 50000 Ruby", 50000);
        m7.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 500);
        m7.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m7.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 5);
        m7.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 3);
        m7.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m7.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 10);
        m7.addReward("Rương cam cùng hệ lv100", (byte) 4, (short) 131, (short) 131, 50);
        m7.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 10);
        m7.addReward("Đá khảm ngẫu nhiên", (byte) 4, (short) 327, (short) 327, 20);
        MILESTONES.add(m7);

        // MOC 8: 100000 Ruby
        Milestone m8 = new Milestone(100000, "Tích tiêu 100000 Ruby", 100000);
        m8.addReward("Trang phục Raid Suit iChiji", (byte) 105, (short) 112, (short) 108, 1);
        m8.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 500);
        m8.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m8.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 5);
        m8.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 3);
        m8.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m8.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 10);
        m8.addReward("Rương cam cùng hệ lv100", (byte) 4, (short) 131, (short) 131, 50);
        m8.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 10);
        m8.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 1);
        m8.addReward("Đá khảm ngẫu nhiên", (byte) 4, (short) 327, (short) 327, 20);
        m8.addReward("Kỹ năng đơn", (byte) 4, (short) 414, (short) 414, 1);
        m8.addReward("Đá Hổ phách 6", (byte) 4, (short) 367, (short) 367, 10);
        MILESTONES.add(m8);

        // MOC 9: 500000 Ruby
        Milestone m9 = new Milestone(500000, "Tích tiêu 500000 Ruby", 500000);
        m9.addReward("Trang phục Raid Suit Judge", (byte) 105, (short) 116, (short) 116, 1);
        m9.addReward("Bột vàng", (byte) 7, (short) 4, (short) 6504, 500);
        m9.addReward("Mai rùa", (byte) 7, (short) 6, (short) 6506, 20);
        m9.addReward("Búa sơ cấp", (byte) 4, (short) 339, (short) 339, 5);
        m9.addReward("Khiên", (byte) 7, (short) 10, (short) 10, 3);
        m9.addReward("XP skill", (byte) 4, (short) 159, (short) 159, 20);
        m9.addReward("Túi beri", (byte) 4, (short) 349, (short) 349, 10);
        m9.addReward("Rương cam cùng hệ lv100", (byte) 4, (short) 131, (short) 131, 50);
        m9.addReward("Búa đục dial", (byte) 4, (short) 457, (short) 457, 10);
        m9.addReward("Búa siêu cấp", (byte) 4, (short) 323, (short) 323, 1);
        m9.addReward("Đá khảm ngẫu nhiên", (byte) 4, (short) 327, (short) 327, 20);
        m9.addReward("Kỹ năng đơn", (byte) 4, (short) 414, (short) 414, 1);
        m9.addReward("Đá Hổ phách 6", (byte) 4, (short) 367, (short) 367, 10);
        MILESTONES.add(m9);
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
        return (short) id;
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
        }
        if (itemName == null || itemName.isEmpty()) {
            itemName = reward.name;
        }
        return itemName;
    }

    private static short getRewardIcon(RewardItem reward) {
        return reward.icon;
    }

    private static boolean isRubyReward(RewardItem reward) {
        return reward.type == 4 && reward.id == 1;
    }

    private static int countRequiredBagSlots(Player p, Milestone milestone) {
        int requiredSpace = 0;
        for (RewardItem reward : milestone.rewards) {
            if (isRubyReward(reward))
                continue;
            if (reward.type == 3) {
                requiredSpace++;
            } else if (reward.type == 4 || reward.type == 7) {
                if (p.item.total_item_bag_by_id(reward.type, reward.id) == 0) {
                    requiredSpace++;
                }
                if ((p.item.total_item_bag_by_id(reward.type, reward.id)
                        + reward.quantity) > template.DataTemplate.MAX_ITEM_IN_BAG) {
                    return Integer.MAX_VALUE;
                }
            }
        }
        return requiredSpace;
    }

    public static void sendUI(Player p) throws IOException {
        Message m = new Message(-96);
        m.writer().writeByte(0); // type = 0: Open UI Panel
        m.writer().writeInt(p.tichtieu_ruby); // Tong ruby da tieu
        m.writer().writeByte(MILESTONES.size()); // so moc

        for (int i = 0; i < MILESTONES.size(); i++) {
            Milestone milestone = MILESTONES.get(i);

            byte status = 0;
            if (p.claimedTichtieuRuby.contains(milestone.id)) {
                status = 2;
            } else if (p.tichtieu_ruby >= milestone.rubyReq) {
                status = 1;
            }

            m.writer().writeByte(i); // index/ID moc dang byte
            m.writer().writeInt(milestone.rubyReq); // rubyReq (so ruby yeu cau)
            m.writer().writeByte(status); // status (trang thai nhan: 0=chua, 1=du, 2=da nhan)
            m.writer().writeShort(milestone.rewards.size()); // so phan thuong

            for (RewardItem reward : milestone.rewards) {
                m.writer().writeUTF(getRewardName(reward)); // ten item
                m.writer().writeByte(reward.type); // loai item
                m.writer().writeShort(getIcon(reward.type, reward.id)); // icon
                m.writer().writeShort(reward.quantity); // so luong
                m.writer().writeByte(isRubyReward(reward) ? 5 : 0);
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static Milestone getMilestoneByIndex(int index) {
        if (index >= 0 && index < MILESTONES.size()) {
            return MILESTONES.get(index);
        }
        return null;
    }

    public static void showSubMenu(Player p, int index) throws IOException {
        Milestone milestone = getMilestoneByIndex(index);
        if (milestone == null)
            return;

        p.id_menu_tichtieu = index;
        core.MenuController.send_dynamic_menu(p, 9085, "Moc " + Util.number_format(milestone.rubyReq) + " Ruby",
                new String[] { "Nhan qua", "Xem phan thuong" }, null);
    }

    public static void showReward(Player p, int index) throws IOException {
        Milestone milestone = getMilestoneByIndex(index);
        if (milestone == null)
            return;

        String[] menuItems = new String[milestone.rewards.size()];
        for (int i = 0; i < milestone.rewards.size(); i++) {
            RewardItem reward = milestone.rewards.get(i);
            menuItems[i] = getRewardName(reward) + " x" + Util.number_format(reward.quantity);
        }

        core.MenuController.send_dynamic_menu(p, 9086,
                "Phần thưởng mốc " + Util.number_format(milestone.rubyReq) + " Ruby", menuItems, null);
    }

    public static void claimReward(Player p, int index) throws IOException {
        Milestone milestone = getMilestoneByIndex(index);
        if (milestone == null)
            return;

        if (p.tichtieu_ruby < milestone.rubyReq) {
            Service.send_box_ThongBao_OK(p, "Bạn chưa đủ điều kiện để nhận phần thưởng này!");
            return;
        }

        if (p.claimedTichtieuRuby.contains(milestone.id)) {
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

        p.claimedTichtieuRuby.add(milestone.id);

        p.update_money();
        p.item.update_Inventory(-1, false);

        Service.send_box_ThongBao_OK(p,
                "Nhận quà mốc " + Util.number_format(milestone.rubyReq) + " Ruby thành công!");

        // Gửi type=2 để client gọi setCmdDaNhanIndex(index) cập nhật trạng thái đã nhận
        // trong UI
        Message m = new Message(-96);
        m.writer().writeByte(2);
        m.writer().writeByte(index);
        p.conn.addmsg(m);
        m.cleanup();

        // Refresh toàn bộ UI
        sendUI(p);
    }
}
