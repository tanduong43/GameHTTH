package activities;

import client.Player;
import core.MenuController;
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
    public static final int MILESTONE_RUBY = 700_000; // Mốc 700.000 Ruby (1.400 lượt quay)
    public static final int MENU_ID_MILESTONE = 9077; // Menu ID nhận quà mốc

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
    // Slots 0-15: 16 outer slots (Tổng = 9,230 = 92.30%)
    // Slots 16-21: 6 inner slots (Tổng = 770 = 7.70%, trong đó 2 Trái Ác Quỷ mỗi trái 0.1% = 10)
    public static final RewardSlot[] DEFAULT_SLOTS = new RewardSlot[] {
        // --- 16 Outer Slots (0 - 15) ---
        new RewardSlot(29, 4, 1, 300),    // 0: Rương ác quỷ x1 (3.0%)
        new RewardSlot(441, 4, 2, 590),   // 1: Ốc Sên x2 (5.9%)
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
        new RewardSlot(441, 4, 1, 490),   // 15: Ốc Sên x1 (4.9%)

        // --- 6 Inner Slots (16 - 21) ---
        new RewardSlot(158, 4, 1, 150),   // 16: Rương ác quỷ đặc biệt x1 (1.5%)
        new RewardSlot(225, 4, 1, 200),   // 17: Đá hải thạch cấp 5 x1 (2.0%)
        new RewardSlot(1015, 4, 1, 10),   // 18: Trái Nikyu Nikyu x1 (0.1% - Cực hiếm)
        new RewardSlot(60, 4, 1, 200),    // 19: Tinh thể ruby cấp 5 x1 (2.0%)
        new RewardSlot(1016, 4, 1, 10),   // 20: Trái Ope Ope x1 (0.1% - Cực hiếm)
        new RewardSlot(66, 4, 1, 200)     // 21: Ngọc lục bảo cấp 5 x1 (2.0%)
    };

    /**
     * Get item display icon
     */
    public static short getIcon(int category, int id) {
        if (category == 4) {
            if (id == 1015) return 190;
            if (id == 1016) return 191;
            if (id == 1017) return 683;
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
            if (id == 1017) return "Trái Nika";
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
        String progress = p.claimed_oc_sen_milestone > 0 ? " [Đã nhận mốc]" : " [" + Util.number_format(p.ruby_spent_oc_sen) + "/700k]";
        m.writer().writeUTF("Vòng Quay Ốc Sên" + progress);
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

        // Tích lũy tiến độ mốc 700.000 Ruby (quay bằng Ruby hay Ốc Sên đều cộng 500 Ruby)
        boolean reachedMilestoneNow = false;
        if (p.claimed_oc_sen_milestone == 0) {
            p.ruby_spent_oc_sen += RUBY_COST;
            if (p.ruby_spent_oc_sen >= MILESTONE_RUBY && (p.ruby_spent_oc_sen - RUBY_COST) < MILESTONE_RUBY) {
                reachedMilestoneNow = true;
            }
        } else {
            p.ruby_spent_oc_sen += RUBY_COST;
        }

        // Pick random slot theo trọng số tỉ lệ (Weighted Random)
        int indexWon = getRandomSlotIndex();

        // Reward player
        RewardSlot reward = DEFAULT_SLOTS[indexWon];
        p.item.add_item_bag47(reward.category, reward.id, reward.quant);
        p.item.update_Inventory(-1, false);
        p.update_money();

        // Broadcast if winning rare fruit or chest
        if (reward.id == 1015 || reward.id == 1016 || reward.id == 1017 || reward.id == 158) {
            try {
                core.Manager.gI().chatKTG(0, "Chúc mừng " + p.name + " vừa quay trúng " + getName(reward.category, reward.id) + " từ Vòng Quay Ốc Sên!", 5);
            } catch (Exception e) {
            }
        }

        // Nếu vừa chạm đúng mốc 700.000 Ruby, hiện banner thông báo chúc mừng
        if (reachedMilestoneNow) {
            Service.send_server_notice(p, "🎉 Chúc mừng bạn đã đạt mốc 700.000 Ruby Vòng Quay Ốc Sên! Hãy gặp NPC Buggi để nhận Trái Ác Quỷ!");
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
     * Menu hiển thị tiến độ và nhận quà mốc 700.000 Ruby tại NPC Buggi
     */
    public static void show_milestone_menu(Player p) throws IOException {
        if (p.claimed_oc_sen_milestone >= 1) {
            Service.send_box_ThongBao_OK(p, "Bạn đã nhận phần thưởng Trái Ác Quỷ từ mốc 700.000 Ruby rồi!\n(Mốc này chỉ nhận 1 lần duy nhất)");
            return;
        }

        if (p.ruby_spent_oc_sen >= MILESTONE_RUBY) {
            // Đã đạt mốc và chưa nhận -> Mở menu chọn Trái Ác Quỷ
            MenuController.send_dynamic_menu(p, MENU_ID_MILESTONE,
                "Mốc 700k Ruby (" + Util.number_format(p.ruby_spent_oc_sen) + "/700k)",
                new String[] { "Nhận Trái Ope Ope", "Nhận Trái Nikyu Nikyu", "Đóng" },
                new short[] { 191, 190, 117 });
        } else {
            // Chưa đạt mốc -> Thông báo tiến độ
            int remaining = MILESTONE_RUBY - p.ruby_spent_oc_sen;
            int spinCur = p.ruby_spent_oc_sen / RUBY_COST;
            int spinTarget = MILESTONE_RUBY / RUBY_COST;
            Service.send_box_ThongBao_OK(p, "Tiến độ tích lũy Vòng Quay Ốc Sên của bạn:\n"
                + "• Đã tích lũy: " + Util.number_format(p.ruby_spent_oc_sen) + " / " + Util.number_format(MILESTONE_RUBY) + " Ruby\n"
                + "• Tương đương: " + spinCur + " / " + spinTarget + " lượt quay\n\n"
                + "Còn thiếu " + Util.number_format(remaining) + " Ruby nữa (" + (remaining / RUBY_COST) + " lượt) để nhận thẳng Trái Ope Ope hoặc Trái Nikyu Nikyu!");
        }
    }

    /**
     * Xử lý khi người chơi bấm chọn trong menu mốc 700k (menu 9077)
     */
    public static void handle_milestone_menu(Player p, byte index) throws IOException {
        switch (index) {
            case 0: { // Trái Ope Ope (ID 1016)
                claim_milestone(p, 1016);
                break;
            }
            case 1: { // Trái Nikyu Nikyu (ID 1015)
                claim_milestone(p, 1015);
                break;
            }
            case 2: // Đóng
                break;
        }
    }

    /**
     * Trao quà mốc Trái Ác Quỷ
     */
    public static void claim_milestone(Player p, int fruitId) throws IOException {
        if (p.claimed_oc_sen_milestone >= 1) {
            Service.send_box_ThongBao_OK(p, "Bạn đã nhận phần thưởng mốc 700.000 Ruby rồi!");
            return;
        }
        if (p.ruby_spent_oc_sen < MILESTONE_RUBY) {
            Service.send_box_ThongBao_OK(p, "Bạn chưa tích lũy đủ 700.000 Ruby!");
            return;
        }
        if (p.item.able_bag() < 1) {
            Service.send_box_ThongBao_OK(p, "Hành trang của bạn không đủ chỗ trống! Vui lòng làm trống ít nhất 1 ô trong hành trang để nhận Trái Ác Quỷ.");
            return;
        }

        p.claimed_oc_sen_milestone = 1;
        p.item.add_item_bag47(4, fruitId, 1);
        p.item.update_Inventory(-1, false);

        String fruitName = getName(4, fruitId);
        Service.send_box_ThongBao_OK(p, "🎉 Chúc mừng bạn đã nhận thành công " + fruitName + " từ Mốc 700.000 Ruby Vòng Quay Ốc Sên!");
        try {
            core.Manager.gI().chatKTG(0, "Chúc mừng người chơi " + p.name + " đã đạt mốc 700.000 Ruby Vòng Quay Ốc Sên và nhận thành công " + fruitName + "!", 5);
        } catch (Exception e) {}
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
