package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import client.Player;
import core.Service;
import core.Util;
import io.Message;
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
     *  CAU HINH CAC MOC TICH TIEU RUBY - CHINH SUA TAI DAY
     * =====================================================================
     */
    private static void initMilestones() {
        MILESTONES.clear();

        // MOC 1: 500 Ruby
        Milestone m1 = new Milestone(500, "Tich tieu 500 Ruby", 500);
        m1.addReward("Chest",        (byte) 4, (short)  7, (short)  7,   5); // Ruong chau bau x5
        m1.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70,  50); // Da Saphia cap 3 x50
        MILESTONES.add(m1);

        // MOC 2: 1000 Ruby
        Milestone m2 = new Milestone(1000, "Tich tieu 1000 Ruby", 1000);
        m2.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  10); // Ruong chau bau x10
        m2.addReward("Purple stone", (byte) 4, (short) 76, (short) 76,  20); // Thach anh tim cap 3 x20
        MILESTONES.add(m2);

        // MOC 3: 3000 Ruby
        Milestone m3 = new Milestone(3000, "Tich tieu 3000 Ruby", 3000);
        m3.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  20); // Ruong chau bau x20
        m3.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70, 150); // Da Saphia cap 3 x150
        m3.addReward("Purple stone", (byte) 4, (short) 76, (short) 76,  50); // Thach anh tim cap 3 x50
        MILESTONES.add(m3);

        // MOC 4: 5000 Ruby
        Milestone m4 = new Milestone(5000, "Tich tieu 5000 Ruby", 5000);
        m4.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  35); // Ruong chau bau x35
        m4.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70, 300); // Da Saphia cap 3 x300
        m4.addReward("Purple stone", (byte) 4, (short) 76, (short) 76, 100); // Thach anh tim cap 3 x100
        MILESTONES.add(m4);

        // MOC 5: 10000 Ruby
        Milestone m5 = new Milestone(10000, "Tich tieu 10000 Ruby", 10000);
        m5.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  60); // Ruong chau bau x60
        m5.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70, 500); // Da Saphia cap 3 x500
        m5.addReward("Purple stone", (byte) 4, (short) 76, (short) 76, 200); // Thach anh tim cap 3 x200
        MILESTONES.add(m5);
    }

    private static String getRewardName(RewardItem reward) {
        String itemName = reward.name;
        if (reward.type == 4) {
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(reward.id);
            if (it != null) itemName = it.name;
        } else if (reward.type == 3) {
            ItemTemplate3 it = ItemTemplate3.get_it_by_id(reward.id);
            if (it != null) itemName = it.name;
        } else if (reward.type == 7) {
            ItemTemplate7 it = ItemTemplate7.get_it_by_id(reward.id);
            if (it != null) itemName = it.name;
        } else if (reward.type == 8) {
            ItemTemplate8 it = ItemTemplate8.get_it_by_id(reward.id);
            if (it != null) itemName = it.name;
        }
        if (itemName == null || itemName.isEmpty()) {
            itemName = reward.name;
        }
        return itemName;
    }

    private static boolean isRubyReward(RewardItem reward) {
        return reward.type == 4 && reward.id == 1;
    }

    private static int countRequiredBagSlots(Player p, Milestone milestone) {
        int requiredSpace = 0;
        for (RewardItem reward : milestone.rewards) {
            if (isRubyReward(reward)) continue;
            if (reward.type == 3) {
                requiredSpace++;
            } else if (reward.type == 4 || reward.type == 7) {
                if (p.item.total_item_bag_by_id(reward.type, reward.id) == 0) {
                    requiredSpace++;
                }
                if ((p.item.total_item_bag_by_id(reward.type, reward.id) + reward.quantity)
                        > template.DataTemplate.MAX_ITEM_IN_BAG) {
                    return Integer.MAX_VALUE;
                }
            }
        }
        return requiredSpace;
    }

    public static void sendUI(Player p) throws IOException {
        String[] menu = new String[MILESTONES.size()];
        for (int i = 0; i < MILESTONES.size(); i++) {
            Milestone milestone = MILESTONES.get(i);
            String statusStr = "(Chua dat)";
            if (p.claimedTichtieuRuby.contains(milestone.id)) {
                statusStr = "(Da nhan)";
            } else if (p.tichtieu_ruby >= milestone.rubyReq) {
                statusStr = "(Du dieu kien)";
            }
            menu[i] = "Moc " + Util.number_format(milestone.rubyReq) + " Ruby\n" + statusStr;
        }
        core.MenuController.send_dynamic_menu(p, 983, "Tich Tieu Ruby\n(Da tieu: " + Util.number_format(p.tichtieu_ruby) + " Ruby)", menu, null);
    }

    public static Milestone getMilestoneByIndex(int index) {
        if (index >= 0 && index < MILESTONES.size()) {
            return MILESTONES.get(index);
        }
        return null;
    }

    public static void claimReward(Player p, int index) throws IOException {
        Milestone milestone = getMilestoneByIndex(index);
        if (milestone == null) return;

        if (p.tichtieu_ruby < milestone.rubyReq) {
            StringBuilder sb = new StringBuilder();
            sb.append("Phan thuong moc ").append(Util.number_format(milestone.rubyReq)).append(" Ruby:\n");
            for (RewardItem reward : milestone.rewards) {
                sb.append("- ").append(getRewardName(reward)).append(" x").append(reward.quantity).append("\n");
            }
            Service.send_box_ThongBao_OK(p, sb.toString());
            return;
        }

        if (p.claimedTichtieuRuby.contains(milestone.id)) {
            Service.send_box_ThongBao_OK(p, "Ban da nhan qua moc nay roi!");
            return;
        }

        int requiredSpace = countRequiredBagSlots(p, milestone);
        if (requiredSpace == Integer.MAX_VALUE) {
            Service.send_box_ThongBao_OK(p, "So luong vat pham trong hanh trang vuot qua gioi han!");
            return;
        }
        if (requiredSpace > p.item.able_bag()) {
            Service.send_box_ThongBao_OK(p, "Hanh trang cua ban khong du cho trong! Can it nhat "
                    + requiredSpace + " o trong.");
            return;
        }

        for (RewardItem reward : milestone.rewards) {
            if (isRubyReward(reward)) {
                // Cong ngoc the ruong, khong tinh vao tich tieu (update_ngoc duong)
                p.update_ngoc(reward.quantity);
            } else if (!p.item.add_item_bag47(reward.type, reward.id, reward.quantity)) {
                Service.send_box_ThongBao_OK(p, "Khong the them "
                        + getRewardName(reward) + " vao hanh trang!");
                return;
            }
        }

        p.claimedTichtieuRuby.add(milestone.id);

        p.update_money();
        p.item.update_Inventory(-1, false);

        Service.send_box_ThongBao_OK(p,
                "Nhan qua moc " + Util.number_format(milestone.rubyReq) + " Ruby thanh cong!");

        // Refresh toan bo UI Menu Text
        sendUI(p);
    }
}
