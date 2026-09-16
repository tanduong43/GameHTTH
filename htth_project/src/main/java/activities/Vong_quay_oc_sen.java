package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import java.io.IOException;
import template.ItemTemplate4;
import template.ItemTemplate7;

/**
 * Vòng Quay Ốc Sên (Snail Wheel)
 * Handles Message 77 (QUAY_OC_SEN)
 */
public class Vong_quay_oc_sen {

    public static final int RUBY_COST = 500; // Giá quay 500 ruby

    public static class RewardSlot {
        public int id;
        public int category; // 4: item4/potion, 7: item7/material
        public int quant;
        public int weight;   // Trọng số tỉ lệ (tổng 10,000 = 100%, 100 = 1%)

        public RewardSlot(int id, int category, int quant, int weight) {
            this.id = id;
            this.category = category;
            this.quant = quant;
            this.weight = weight;
        }

        public RewardSlot(int id, int category, int quant) {
            this(id, category, quant, 100);
        }
    }

    // 22 Reward slots on the wheel (Tổng weight = 10,000 = 100%):
    // Slots 0-15: 16 outer slots (Tổng = 9,248 = 92.48%)
    // Slots 16-21: 6 inner slots (Tổng = 752 = 7.52%, trong đó 2 Trái Ác Quỷ mỗi trái 0.01% = 1)
    public static final RewardSlot[] DEFAULT_SLOTS = new RewardSlot[] {
        // --- 16 Outer Slots (0 - 15) ---
        new RewardSlot(29, 4, 1, 300),    // 0: Rương ác quỷ x1 (3.0%)
        new RewardSlot(441, 4, 2, 599),   // 1: Ốc Sên x2 (5.99%)
        new RewardSlot(4, 7, 10, 700),    // 2: Bột vàng x10 (7.0%)
        new RewardSlot(223, 4, 2, 600),   // 3: Đá hải thạch cấp 3 x2 (6.0%)
        new RewardSlot(80, 4, 2, 750),    // 4: Kinh nghiệm X2 x2 (7.5%)
        new RewardSlot(48, 4, 1, 400),    // 5: Cẩm thạch cấp 5 x1 (4.0%)
        new RewardSlot(10, 7, 2, 700),    // 6: Khiên x2 (7.0%)
        new RewardSlot(40, 4, 3, 700),    // 7: Chìa khóa Phó Bản x3 (7.0%)
        new RewardSlot(222, 4, 3, 700),   // 8: Đá hải thạch cấp 2 x3 (7.0%)
        new RewardSlot(9, 7, 5, 600),     // 9: Đá ác quỷ x5 (6.0%)
        new RewardSlot(89, 4, 3, 700),    // 10: Vé Hồi Sinh x3 (7.0%)
        new RewardSlot(54, 4, 1, 400),    // 11: Đá Topaz cấp 5 x1 (4.0%)
        new RewardSlot(72, 4, 1, 400),    // 12: Đá Saphia cấp 5 x1 (4.0%)
        new RewardSlot(1, 7, 20, 800),    // 13: Bột cường hóa x20 (8.0%)
        new RewardSlot(78, 4, 1, 400),    // 14: Thạch anh tím cấp 5 x1 (4.0%)
        new RewardSlot(441, 4, 1, 499),   // 15: Ốc Sên x1 (4.99%)

        // --- 6 Inner Slots (16 - 21) ---
        new RewardSlot(158, 4, 1, 150),   // 16: Rương ác quỷ đặc biệt x1 (1.5%)
        new RewardSlot(225, 4, 1, 200),   // 17: Đá hải thạch cấp 5 x1 (2.0%)
        new RewardSlot(1015, 4, 1, 1),    // 18: Trái Nikyu Nikyu x1 (0.01% - Cực hiếm)
        new RewardSlot(60, 4, 1, 200),    // 19: Tinh thể ruby cấp 5 x1 (2.0%)
        new RewardSlot(1016, 4, 1, 1),    // 20: Trái Ope Ope x1 (0.01% - Cực hiếm)
        new RewardSlot(66, 4, 1, 200)     // 21: Ngọc lục bảo cấp 5 x1 (2.0%)
    };

    /**
     * Get item display icon
     */
    public static short getIcon(int category, int id) {
        if (category == 4) {
            if (id == 1015) return 190;
            if (id == 1016) return 191;
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(id);
            return it != null ? it.icon : 0;
        } else if (category == 7) {
            ItemTemplate7 it = ItemTemplate7.get_it_by_id(id);
            return it != null ? (short) it.icon : 0;
        }
        return 0;
    }

    /**
     * Get item display name
     */
    public static String getName(int category, int id) {
        if (category == 4) {
            if (id == 1015) return "Trái Nikyu Nikyu";
            if (id == 1016) return "Trái Ope Ope";
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(id);
            return it != null ? it.name : ("Vật phẩm " + id);
        } else if (category == 7) {
            ItemTemplate7 it = ItemTemplate7.get_it_by_id(id);
            return it != null ? it.name : ("Vật phẩm " + id);
        }
        return "Vật phẩm";
    }

    /**
     * Open Vòng Quay Ốc Sên UI (sub_cmd = 0)
     */
    public static void show_table(Player p) throws IOException {
        Message m = new Message(77);
        m.writer().writeByte(0);
        m.writer().writeUTF("Vòng Quay Ốc Sên");
        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Send items list and active states (sub_cmd = 1)
     * All items are sent with isClaimed = 0 so they remain fully visible and available
     */
    private static void send_items_list(Player p) throws IOException {
        Message m = new Message(77);
        m.writer().writeByte(1);
        m.writer().writeByte(22); // 22 items

        for (int i = 0; i < 22; i++) {
            RewardSlot slot = DEFAULT_SLOTS[i];
            m.writer().writeByte(slot.id);
            m.writer().writeByte(slot.category);
            m.writer().writeShort(getIcon(slot.category, slot.id));
            m.writer().writeInt(slot.quant);
            m.writer().writeByte(0); // 0: luôn giữ nguyên hiển thị tất cả các ô
        }

        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Quay ngẫu nhiên theo tỉ lệ trọng số (Weighted Random)
     */
    public static int getRandomSlotIndex() {
        int totalWeight = 0;
        for (RewardSlot slot : DEFAULT_SLOTS) {
            totalWeight += slot.weight;
        }
        if (totalWeight <= 0) {
            return Util.random(DEFAULT_SLOTS.length);
        }
        int roll = Util.random(totalWeight);
        int accumulated = 0;
        for (int i = 0; i < DEFAULT_SLOTS.length; i++) {
            accumulated += DEFAULT_SLOTS[i].weight;
            if (roll < accumulated) {
                return i;
            }
        }
        return DEFAULT_SLOTS.length - 1;
    }

    /**
     * Handle Spin (action 3 = Ruby 500, action 4 = Ốc Sên item 441)
     */
    private static void spin(Player p, byte action) throws IOException {
        if (action == 4) { // Quay bằng Ốc Sên (item 441)
            if (p.item.total_item_bag_by_id(4, 441) < 1) {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ Ốc Sên để quay!");
                return;
            }
        } else if (action == 3) { // Quay bằng Ruby (500 ruby)
            if (p.get_ngoc() < RUBY_COST) {
                Service.send_box_ThongBao_OK(p, "Bạn không đủ " + RUBY_COST + " Ruby để quay!");
                return;
            }
        } else {
            return;
        }

        // Deduct cost
        if (action == 4) {
            p.item.remove_item47(4, 441, 1);
        } else if (action == 3) {
            p.update_ngoc(-RUBY_COST);
            p.update_money();
        }

        // Pick random slot theo trọng số tỉ lệ (Weighted Random)
        int indexWon = getRandomSlotIndex();

        // Reward player
        RewardSlot reward = DEFAULT_SLOTS[indexWon];
        p.item.add_item_bag47(reward.category, reward.id, reward.quant);
        p.item.update_Inventory(-1, false);
        p.update_money();

        // Broadcast if winning rare fruit or chest
        if (reward.id == 1015 || reward.id == 1016 || reward.id == 158) {
            try {
                core.Manager.gI().chatKTG(0, "Chúc mừng " + p.name + " vừa quay trúng " + getName(reward.category, reward.id) + " từ Vòng Quay Ốc Sên!", 5);
            } catch (Exception e) {
            }
        }

        // 1. Reset client item list state (đảm bảo tất cả 22 ô luôn giữ nguyên)
        Message mList = new Message(77);
        mList.writer().writeByte(1);
        mList.writer().writeByte(22);
        for (int i = 0; i < 22; i++) {
            RewardSlot slot = DEFAULT_SLOTS[i];
            mList.writer().writeByte(slot.id);
            mList.writer().writeByte(slot.category);
            mList.writer().writeShort(getIcon(slot.category, slot.id));
            mList.writer().writeInt(slot.quant);
            mList.writer().writeByte(0); // luôn 0 để giữ nguyên
        }
        p.conn.addmsg(mList);
        mList.cleanup();

        // 2. Send spin result packet (sub_cmd = 2)
        Message m = new Message(77);
        m.writer().writeByte(2);
        m.writer().writeByte(indexWon);
        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Main message processor for Message 77
     */
    public static void process(Player p, Message m2) throws IOException {
        byte action = m2.reader().readByte();
        switch (action) {
            case 1: {
                send_items_list(p);
                break;
            }
            case 3:
            case 4: {
                spin(p, action);
                break;
            }
        }
    }
}
