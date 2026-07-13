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
        // Milestone 1: 100k Extol (ID 100 -> client hiển thị 100k)
        Milestone m1 = new Milestone(100, "Tích luỹ nạp 100k Extol", 100000);
        m1.addReward("Chest", (byte) 4, (short) 7, (short) 7, 5); // Rương châu báu
        m1.addReward("Blue stone", (byte) 4, (short) 70, (short) 70, 50); // Đá Saphia cấp 3
        m1.addReward("Purple stone", (byte) 4, (short) 76, (short) 76, 10); // Thạch anh tím cấp 3
        MILESTONES.add(m1);

        // Milestone 2: 200k Extol (ID 200 -> client hiển thị 200k)
        Milestone m2 = new Milestone(200, "Tích luỹ nạp 200k Extol", 200000);
        m2.addReward("Gold", (byte) 4, (short) 1, (short) 1, 60); // Ruby (displayed as Gold/Ruby)
        m2.addReward("Chest", (byte) 4, (short) 7, (short) 7, 10);
        m2.addReward("Blue stone", (byte) 4, (short) 70, (short) 70, 100);
        MILESTONES.add(m2);
    }

    public static short getIcon(byte type, int id) {
        if (type == 4) {
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(id);
            if (it != null) return it.icon;
        } else if (type == 3) {
            ItemTemplate3 it = ItemTemplate3.get_it_by_id(id);
            if (it != null) return it.icon;
        } else if (type == 7) {
            ItemTemplate7 it = ItemTemplate7.get_it_by_id(id);
            if (it != null) return it.icon;
        } else if (type == 8) {
            ItemTemplate8 it = ItemTemplate8.get_it_by_id(id);
            if (it != null) return it.icon;
        }
        return 0;
    }

    public static void sendUI(Player p) throws IOException {
        Message m = new Message(-97);
        m.writer().writeByte(0); // action 0: Open UI
        m.writer().writeByte(MILESTONES.size()); // num milestones
        for (int i = 0; i < MILESTONES.size(); i++) {
            Milestone milestone = MILESTONES.get(i);
            
            m.writer().writeShort(milestone.id);
            m.writer().writeInt(milestone.extolReq);
            m.writer().writeByte(milestone.rewards.size());
            
            for (RewardItem reward : milestone.rewards) {
                short icon = getIcon(reward.type, reward.id);
                
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
                
                if (itemName.equals("Gold") && reward.type == 4 && reward.id == 1) {
                    itemName = "Ruby"; // Correct the name for Ruby
                }
                
                m.writer().writeUTF(itemName);
                m.writer().writeByte(reward.type);
                m.writer().writeShort(icon > 0 ? icon : reward.icon); // idIcon (parameter 3)
                m.writer().writeShort(reward.quantity);               // numPotion (parameter 4)
                m.writer().writeByte(0);                              // colorName (parameter 5)
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static Milestone getMilestoneById(int id) {
        for (Milestone m : MILESTONES) {
            if (m.id == id) {
                return m;
            }
        }
        return null;
    }

    public static void claimReward(Player p, int milestoneId) throws IOException {
        Milestone milestone = getMilestoneById(milestoneId);
        if (milestone == null) {
            return;
        }
        
        // Check if player has enough extol points
        if (p.getTichLuy() < milestone.extolReq) {
            Service.send_box_ThongBao_OK(p, "Bạn chưa đạt mốc tích lũy này!");
            return;
        }
        
        // Check if already claimed
        if (p.claimedMilestones.contains(milestone.id)) {
            Service.send_box_ThongBao_OK(p, "Bạn đã nhận quà mốc này rồi!");
            return;
        }
        
        // Check inventory space for rewards
        int requiredSpace = 0;
        for (RewardItem reward : milestone.rewards) {
            if (reward.type == 3) {
                requiredSpace++;
            } else if (reward.type == 4 || reward.type == 7) {
                // If the player doesn't have the item and bag is full
                if (p.item.total_item_bag_by_id(reward.type, reward.id) == 0 && p.item.able_bag() == 0) {
                    requiredSpace++;
                }
            }
        }
        
        // We can get bag null for item3 or bag47
        if (requiredSpace > p.item.able_bag()) {
            Service.send_box_ThongBao_OK(p, "Hành trang của bạn không đủ chỗ trống!");
            return;
        }
        
        // Give rewards
        p.claimedMilestones.add(milestone.id);
        
        for (RewardItem reward : milestone.rewards) {
            if (reward.name.equalsIgnoreCase("Gold")) {
                p.update_ngoc(reward.quantity);
                p.update_money();
                Service.send_box_ThongBao_OK(p, "Bạn nhận được " + reward.quantity + " Ruby từ mốc nạp!");
            } else {
                p.item.add_item_bag47(reward.type, reward.id, reward.quantity);
            }
        }
        p.item.update_Inventory(4, false);
        
        Service.send_box_ThongBao_OK(p, "Nhận quà mốc " + Util.number_format(milestone.extolReq) + " Extol thành công!");
        
        // Send UI update action 1 to update button status on client to "Đã nhận"
        Message m = new Message(-97);
        m.writer().writeByte(1); // action 1: set claimed status
        m.writer().writeByte(milestone.id); // milestone id
        p.conn.addmsg(m);
        m.cleanup();
        
        // Also refresh the UI panel
        sendUI(p);
    }
}
