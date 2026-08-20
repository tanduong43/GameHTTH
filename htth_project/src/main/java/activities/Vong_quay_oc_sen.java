package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import java.io.IOException;
import template.ItemBag47;
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

        public RewardSlot(int id, int category, int quant) {
            this.id = id;
            this.category = category;
            this.quant = quant;
        }
    }

    // 22 Reward slots on the wheel:
    // Slots 0-15: 16 outer slots
    // Slots 16-21: 6 inner slots (rare/special rewards)
    public static final RewardSlot[] DEFAULT_SLOTS = new RewardSlot[] {
        // --- 16 Outer Slots (0 - 15) ---
        new RewardSlot(29, 4, 1),   // 0: Rương ác quỷ x1
        new RewardSlot(441, 4, 2),  // 1: Ốc Sên x2
        new RewardSlot(4, 7, 10),   // 2: Bột vàng x10
        new RewardSlot(223, 4, 2),  // 3: Đá hải thạch cấp 3 x2
        new RewardSlot(80, 4, 2),   // 4: Kinh nghiệm X2 x2
        new RewardSlot(48, 4, 1),   // 5: Cẩm thạch cấp 5 x1
        new RewardSlot(10, 7, 2),   // 6: Khiên x2
        new RewardSlot(40, 4, 3),   // 7: Chìa khóa Phó Bản x3
        new RewardSlot(222, 4, 3),  // 8: Đá hải thạch cấp 2 x3
        new RewardSlot(9, 7, 5),    // 9: Đá ác quỷ x5
        new RewardSlot(89, 4, 3),   // 10: Vé Hồi Sinh x3
        new RewardSlot(54, 4, 1),   // 11: Đá Topaz cấp 5 x1
        new RewardSlot(72, 4, 1),   // 12: Đá Saphia cấp 5 x1
        new RewardSlot(1, 7, 20),   // 13: Bột cường hóa x20
        new RewardSlot(78, 4, 1),   // 14: Thạch anh tím cấp 5 x1
        new RewardSlot(441, 4, 1),  // 15: Ốc Sên x1

        // --- 6 Inner Slots (16 - 21) ---
        new RewardSlot(158, 4, 1),  // 16: Rương ác quỷ đặc biệt x1
        new RewardSlot(225, 4, 1),  // 17: Đá hải thạch cấp 5 x1
        new RewardSlot(60, 4, 1),   // 18: Tinh thể ruby cấp 5 x1
        new RewardSlot(83, 4, 5),   // 19: Lọ hồi sức 100% x5
        new RewardSlot(66, 4, 1),   // 20: Ngọc lục bảo cấp 5 x1
        new RewardSlot(224, 4, 2)   // 21: Đá hải thạch cấp 4 x2
    };

    /**
     * Get item display icon
     */
    public static short getIcon(int category, int id) {
        if (category == 4) {
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

        // Pick random slot from all 22 slots (vẫn giữ nguyên tất cả các ô)
        int indexWon = Util.random(22);

        // Reward player
        RewardSlot reward = DEFAULT_SLOTS[indexWon];
        p.item.add_item_bag47(reward.category, reward.id, reward.quant);
        p.item.update_Inventory(-1, false);
        p.update_money();

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
