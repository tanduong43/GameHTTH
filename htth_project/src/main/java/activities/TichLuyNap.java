package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.DataTemplate;
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
     *  CẤU HÌNH CÁC MỐC TÍCH LUỸ NẠP - CHỈNH SỬA TẠI ĐÂY
     * =====================================================================
     *  Cách thêm/sửa mốc:
     *    1. Tạo Milestone(id, "tiêu đề", soExtolYeuCau)
     *       - id          : ID duy nhất, client dùng để nhận diện (VD: 100 = 100k)
     *       - extolReq    : số Extol tích luỹ cần đạt (đơn vị: 1 Extol)
     *    2. Gọi .addReward(tên, type, id_item, icon, số_lượng)
     *       - type 4  = vật phẩm thường (ItemTemplate4)
     *       - type 3  = trang bị (ItemTemplate3)
     *       - type 7  = ItemTemplate7
     *       - type 8  = ItemTemplate8
     *       - Nếu type=4 và id=1 → sẽ được cộng Ruby (ngọc) thay vì vào hành trang
     *    3. Thêm vào MILESTONES.add(...)
     * =====================================================================
     */
    private static void initMilestones() {
        MILESTONES.clear();

        // ------------------------------------------------------------------
        // MỐC 1: 100k Extol
        // ------------------------------------------------------------------
        Milestone m1 = new Milestone(100, "Tích luỹ nạp 100k Extol", 100000);
        m1.addReward("Chest",        (byte) 4, (short)  7, (short)  7,   5); // Rương châu báu x5
        m1.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70,  50); // Đá Saphia cấp 3 x50
        m1.addReward("Purple stone", (byte) 4, (short) 76, (short) 76,  10); // Thạch anh tím cấp 3 x10
        MILESTONES.add(m1);

        // ------------------------------------------------------------------
        // MỐC 2: 200k Extol
        // ------------------------------------------------------------------
        Milestone m2 = new Milestone(200, "Tích luỹ nạp 200k Extol", 200000);
        m2.addReward("Ruby",         (byte) 4, (short)  1, (short)  1,  60); // Ruby x60
        m2.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  10); // Rương châu báu x10
        m2.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70, 100); // Đá Saphia cấp 3 x100
        MILESTONES.add(m2);

        // ------------------------------------------------------------------
        // MỐC 3: 300k Extol
        // ------------------------------------------------------------------
        Milestone m3 = new Milestone(300, "Tích luỹ nạp 300k Extol", 300000);
        m3.addReward("Ruby",         (byte) 4, (short)  1, (short)  1, 100); // Ruby x100
        m3.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  15); // Rương châu báu x15
        m3.addReward("Purple stone", (byte) 4, (short) 76, (short) 76,  30); // Thạch anh tím cấp 3 x30
        MILESTONES.add(m3);

        // ------------------------------------------------------------------
        // MỐC 4: 500k Extol
        // ------------------------------------------------------------------
        Milestone m4 = new Milestone(500, "Tích luỹ nạp 500k Extol", 500000);
        m4.addReward("Ruby",         (byte) 4, (short)  1, (short)  1, 200); // Ruby x200
        m4.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  25); // Rương châu báu x25
        m4.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70, 200); // Đá Saphia cấp 3 x200
        m4.addReward("Purple stone", (byte) 4, (short) 76, (short) 76,  50); // Thạch anh tím cấp 3 x50
        MILESTONES.add(m4);

        // ------------------------------------------------------------------
        // MỐC 5: 1M Extol (1.000.000)
        // ------------------------------------------------------------------
        Milestone m5 = new Milestone(1000, "Tích luỹ nạp 1M Extol", 1000000);
        m5.addReward("Ruby",         (byte) 4, (short)  1, (short)  1, 500); // Ruby x500
        m5.addReward("Chest",        (byte) 4, (short)  7, (short)  7,  50); // Rương châu báu x50
        m5.addReward("Blue stone",   (byte) 4, (short) 70, (short) 70, 500); // Đá Saphia cấp 3 x500
        m5.addReward("Purple stone", (byte) 4, (short) 76, (short) 76, 100); // Thạch anh tím cấp 3 x100
        MILESTONES.add(m5);
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
                if ((p.item.total_item_bag_by_id(reward.type, reward.id) + reward.quantity)
                        > DataTemplate.MAX_ITEM_IN_BAG) {
                    return Integer.MAX_VALUE;
                }
            }
        }
        return requiredSpace;
    }

    public static void sendUI(Player p) throws IOException {
        System.out.println("[TichLuyNap] sendUI called for player: " + p.name
                + " | tichLuy=" + p.getTichLuy()
                + " | milestones=" + MILESTONES.size()
                + " | isdie=" + p.isdie);
        /*
         * FORMAT CLIENT ĐỌC (ListTichNapThe - Opcode -90):
         *   writeByte(0)                    ← type = 0: mở UI
         *   writeInt(rubyDaNap)             ← Tổng điểm tích luỹ hiện tại của player
         *   writeByte(MILESTONES.size())    ← số mốc
         *   loop (index i):
         *     writeByte(i)                  ← ID mốc dạng byte (chỉ số index: 0, 1, 2...)
         *     writeInt(milestone.extolReq)  ← số lượng cần để nhận (e.g. 100000, 200000...)
         *     writeByte(status)             ← Trạng thái: 0 = "Xem" (chưa đủ), 1 = "Nhận" (đủ chưa nhận), 2 = "Đã nhận"
         *     writeShort(numQua)            ← số lượng phần thưởng
         *     loop numQua:
         *       writeUTF(name)             ← tên item
         *       writeByte(type)            ← loại item
         *       writeShort(id)             ← id item (được client dùng làm idIcon)
         *       writeShort(quantity)       ← số lượng
         *       writeByte(color)           ← màu chữ tên item (0=trắng, 5=vàng...)
         */
        Message m = new Message(-90);
        m.writer().writeByte(0); // type = 0: Open UI
        m.writer().writeInt(p.getTichLuy()); // Tổng điểm tích luỹ
        m.writer().writeByte(MILESTONES.size()); // số mốc
        
        for (int i = 0; i < MILESTONES.size(); i++) {
            Milestone milestone = MILESTONES.get(i);
            
            // Trạng thái mốc:
            // 2: Đã nhận quà
            // 1: Đủ điểm tích lũy và chưa nhận
            // 0: Chưa đủ điểm tích lũy
            byte status = 0;
            if (p.claimedMilestones.contains(milestone.id)) {
                status = 2;
            } else if (p.getTichLuy() >= milestone.extolReq) {
                status = 1;
            }
            
            m.writer().writeByte(i);                          // index/ID mốc dạng byte
            m.writer().writeInt(milestone.extolReq);          // extolReq (số extol yêu cầu)
            m.writer().writeByte(status);                     // status (trạng thái nhận)
            m.writer().writeShort(milestone.rewards.size());  // số phần thưởng
            
            for (RewardItem reward : milestone.rewards) {
                m.writer().writeUTF(getRewardName(reward));   // tên item
                m.writer().writeByte(reward.type);            // loại item
                m.writer().writeShort(reward.id);             // id item
                m.writer().writeShort(reward.quantity);       // số lượng
                // màu tên: 5=vàng (Ruby), 0=trắng (item thường)
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
        Milestone milestone = getMilestoneByIndex(index);
        if (milestone == null) {
            return;
        }

        if (p.getTichLuy() < milestone.extolReq) {
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
            } else if (!p.item.add_item_bag47(reward.type, reward.id, reward.quantity)) {
                Service.send_box_ThongBao_OK(p, "Không thể thêm "
                        + getRewardName(reward) + " vào hành trang!");
                return;
            }
        }

        p.claimedMilestones.add(milestone.id);
        p.update_money();
        p.item.update_Inventory(-1, false);

        Service.send_box_ThongBao_OK(p,
                "Nhận quà mốc " + Util.number_format(milestone.extolReq) + " Extol thành công!");

        // Gửi type=2 để client gọi setCmdDaNhanIndex(index) cập nhật trạng thái đã nhận trong UI
        Message m = new Message(-90);
        m.writer().writeByte(2); // type 2: update claimed status in UI
        m.writer().writeByte(index); // index/ID mốc dạng byte
        p.conn.addmsg(m);
        m.cleanup();

        // Refresh toàn bộ UI để phản ánh trạng thái mới
        sendUI(p);
    }
}
