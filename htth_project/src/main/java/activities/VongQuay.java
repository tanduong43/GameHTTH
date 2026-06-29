package activities;

import java.io.IOException;
import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.ItemBag47;
import template.ItemTemplate4;
import template.ItemTemplate7;
/**
 *
 * @author Truongbk
 */
public class VongQuay {
    public static short[] ID_ITEM = new short[] {174, 173, 221, 222, 223, 7, 159, 133, //
            112, 224, 29, 225, 48, 158};

    private static ItemBag47 get_random(Player p) {
        ItemBag47 result = null;
        if (70 > Util.random(120)) {
            return result;
        }
        int random = Util.random(10_000);
        if (random < 3000 && random > 2200 && p.level > 19) {
            if (70 > Util.random(120)) { // RUONG AC QUY
                result = new ItemBag47();
                result.id = 29;
                result.category = 4;
                result.quant = 1;
            } else {
                result = new ItemBag47();
                result.id = 158;
                result.category = 4;
                result.quant = 1;
            }
        } else if (random < 1600 && random > 1300 && p.level > 19) { // Nguyen lieu upgrade skinn
            result = new ItemBag47();
            result.id = (short) (5 > Util.random(120) ? -3 : -2);
            result.category = 7;
            result.quant = 1;
        } else if (random < 800 && p.level > 19) {
            if (60 > Util.random(120)) { // DA HAI THACH
                result = new ItemBag47();
                result.id = 221;
                result.category = 4;
                result.quant = 1;
            } else if (50 > Util.random(120)) {
                result = new ItemBag47();
                result.id = 222;
                result.category = 4;
                result.quant = 1;
            } else if (40 > Util.random(120)) {
                result = new ItemBag47();
                result.id = 223;
                result.category = 4;
                result.quant = 1;
            } else if (30 > Util.random(120)) {
                result = new ItemBag47();
                result.id = 224;
                result.category = 4;
                result.quant = 1;
            } else if (20 > Util.random(120)) {
                result = new ItemBag47();
                result.id = 225;
                result.category = 4;
                result.quant = 1;
            } else {
                result = new ItemBag47();
                result.id = 226;
                result.category = 4;
                result.quant = 1;
            }
        } else if (random < 2000) { // RUONG CAM
            result = new ItemBag47();
            result.id = (short) (((p.level < 11 ? 11 : p.level) / 10) + 111);
            result.category = 4;
            result.quant = 1;
            return result;
        } else if (random < 5000 && p.level > 19) { // DA KHAM
            short[] rd = new short[] {48, 54, 60, 66, 72, 78};
            result = new ItemBag47();
            result.id = rd[Util.random(rd.length)];
            result.category = 4;
            result.quant = (short) 1;
        } else if (random < 8000) {
            if (33 > Util.random(120)) { // TREASURE
                result = new ItemBag47();
                result.id = (short) ((p.level / 10) + 7);
                result.category = 4;
                result.quant = (short) 1;
            } else if (33 > Util.random(120)) { // x XP
                result = new ItemBag47();
                result.id = (short) 159;
                result.category = 4;
                result.quant = (short) 1;
            } else { // x XP
                result = new ItemBag47();
                result.id = (short) 133;
                result.category = 4;
                result.quant = (short) 1;
            }
        } else {
            if (50 > Util.random(120)) { // HP MP
                result = new ItemBag47();
                result.id = 173;
                result.category = 4;
                result.quant = (short) Util.random(2, 6);
            } else {
                result = new ItemBag47();
                result.id = 174;
                result.category = 4;
                result.quant = (short) Util.random(2, 6);
            }
        }
        return result;
    }

    public static void show_table(Player p) throws IOException {
        Message m = new Message(54);
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void process(Player p, Message m2) throws IOException {
        byte action = m2.reader().readByte();
        switch (action) {
            case 3: {
                Message m = new Message(54);
                m.writer().writeByte(3);
                m.writer().writeByte(VongQuay.ID_ITEM.length);
                for (int i = 0; i < VongQuay.ID_ITEM.length; i++) {
                    m.writer().writeByte(4);
                    m.writer().writeShort(ItemTemplate4.get_it_by_id(VongQuay.ID_ITEM[i]).icon);
                }
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 4: {
                Service.send_box_yesno(p, 36, "Thông báo",
                        "Bạn cần mua bao nhiêu thẻ quay? Giá mỗi thẻ quay là 15 ruby",
                        new String[] {"1 thẻ", "3 thẻ", "Hủy"}, new byte[] {-1, -1, 1});
                break;
            }
            case 2:
            case 1: {
                int quant_reward = 0;
                if (action == 1) {
                    if (p.item.total_item_bag_by_id(4, 232) < 1) {
                        Service.send_box_ThongBao_OK(p, "Không đủ vé!");
                        return;
                    }
                    p.item.remove_item47(4, 232, 1);
                    quant_reward = 3;
                } else {
                    if (p.item.total_item_bag_by_id(4, 232) < 3) {
                        Service.send_box_ThongBao_OK(p, "Không đủ vé!");
                        return;
                    }
                    p.item.remove_item47(4, 232, 3);
                    quant_reward = 9;
                }
                ItemBag47[] list_reward = new ItemBag47[quant_reward];
                for (int i = 0; i < quant_reward; i++) {
                    list_reward[i] = VongQuay.get_random(p);
                }
                if (list_reward[0] == null && list_reward[1] == null && list_reward[2] == null) {
                    ItemBag47 it = new ItemBag47();
                    it.id = (short) Util.random(2, 6);
                    it.category = 4;
                    it.quant = (short) Util.random(1, 6);
                    list_reward[Util.random(3)] = it;
                }
                boolean add_vang = false;
                Message m = new Message(54);
                m.writer().writeByte(action);
                m.writer().writeByte(list_reward.length);
                for (int i = 0; i < list_reward.length; i++) {
                    if (list_reward[i] == null) { // lose
                        if (!add_vang && 15 > Util.random(120)) {
                            int vang_receiv = (10 > Util.random(120)) ? Util.random(10_000, 20_000)
                                    : Util.random(1_000, 2_000);
                            m.writer().writeByte(4);
                            m.writer().writeUTF("Beri");
                            m.writer().writeShort(0);
                            m.writer().writeInt(vang_receiv);
                            m.writer().writeByte(0);
                            p.update_vang(vang_receiv);
                            p.update_money();
                            add_vang = true;
                        } else {
                            m.writer().writeByte(0);
                            m.writer().writeUTF("");
                            m.writer().writeShort(-1);
                            m.writer().writeInt(0);
                            m.writer().writeByte(0);
                        }
                    } else {
                        if (list_reward[i].id == -2) {
                            ItemTemplate7 template7 = ItemTemplate7.get_it_by_id(16);
                            m.writer().writeByte(7); // type
                            m.writer().writeUTF(template7.name);
                            m.writer().writeShort(template7.icon);
                            m.writer().writeInt(1); // quant
                            m.writer().writeByte(0); // color
                            //
                            if (!p.item.add_item_bag47(7, template7.id, 1)) {
                                // Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống");
                            }
                        } else if (list_reward[i].id == -3) {
                            ItemTemplate7 template7 = ItemTemplate7.get_it_by_id(17);
                            m.writer().writeByte(7); // type
                            m.writer().writeUTF(template7.name);
                            m.writer().writeShort(template7.icon);
                            m.writer().writeInt(1); // quant
                            m.writer().writeByte(0); // color
                            //
                            if (!p.item.add_item_bag47(7, template7.id, 1)) {
                                // Service.send_box_ThongBao_OK(p, "Hành trang không đủ chỗ trống");
                            }
                        } else {
                            ItemTemplate4 template4 = ItemTemplate4.get_it_by_id(list_reward[i].id);
                            m.writer().writeByte(4); // type
                            m.writer().writeUTF(template4.name);
                            m.writer().writeShort(template4.icon);
                            m.writer().writeInt(1); // quant
                            m.writer().writeByte(0); // color
                            //
                            if (!p.item.add_item_bag47(4, template4.id, 1)) {
                            }
                        }
                    }
                }
                p.conn.addmsg(m);
                m.cleanup();
                p.item.update_Inventory(-1, false);
                break;
            }
        }
    }
}
