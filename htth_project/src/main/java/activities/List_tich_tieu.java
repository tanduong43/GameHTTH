package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import template.GiftBox;
import template.ItemFashion;
import template.ItemTemplate4;
import template.ItemTemplate7;

public class List_tich_tieu {
    public final static List<List_tich_tieu> ENTRY;

    static {
        ENTRY = new ArrayList<>();
        List_tich_tieu t = new List_tich_tieu();
        t.num = 100_000;
        t.cat = new byte[]{4, 4, 4, 4};
        t.id = new short[]{0, 158, 222, 366};
        t.quant = new short[]{10, 5, 50, 10};
        ENTRY.add(t);
        //
        t = new List_tich_tieu();
        t.num = 200_000;
        t.cat = new byte[]{4, 4, 4, 4, 4};
        t.id = new short[]{0, 158, 222, 364, 464};
        t.quant = new short[]{60, 10, 100, 20, 1};
        ENTRY.add(t);
        //
        t = new List_tich_tieu();
        t.num = 500_000;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4};
        t.id = new short[]{0, 158, 222, 364, -10, 339};
        // -10 ruong cam theo lv
        t.quant = new short[]{150, 20, 30, 30, 20, 10};
        ENTRY.add(t);
        //
        t = new List_tich_tieu();
        t.num = 1_000_000;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4, 4};
        t.id = new short[]{0, 158, 226, 323, -10, 464, 455};
        // -10 ruong cam theo lv
        t.quant = new short[]{300, 40, 100, 5, 50, 1, 5};
        ENTRY.add(t);
        //
        t = new List_tich_tieu();
        t.num = 2_000_000;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4};
        t.id = new short[]{0,  226, 323, -10, 54, 455};
        // -10 ruong cam theo lv
        t.quant = new short[]{600, 100, 10, 100, 2, 20};
        ENTRY.add(t);
        //
        t = new List_tich_tieu();
        t.num = 5_000_000;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4, 4, 4};
        t.id = new short[]{0, 427, 323, -10, 454, 455, 455, 325};
        // -10 ruong cam theo lv
        t.quant = new short[]{1_000, 1, 30, 100, 5, 40, 10, 10};
        ENTRY.add(t);
    }

    public byte[] cat;
    public short[] id;
    public short[] quant;
    public int num;

    public static void showTable(Player p) throws IOException {
        sendUI(p);
    }

    public static void sendUI(Player p) throws IOException {
        int tongnap = Math.max(p.tieu_ruby, p.tichtieu_ruby);
        p.tieu_ruby = tongnap;
        p.tichtieu_ruby = tongnap;

        Message m = new Message(-96);
        m.writer().writeByte(0);
        m.writer().writeInt(tongnap);
        m.writer().writeByte(ENTRY.size());
        for (int i = 0; i < ENTRY.size(); i++) {
            List_tich_tieu t = ENTRY.get(i);
            byte status = 0;
            if ((i < p.tichTieuRubyCheck.length && p.tichTieuRubyCheck[i] == 1) || p.claimedTichtieuRuby.contains(i)) {
                status = 2;
            } else if (tongnap >= t.num) {
                status = 1;
            }
            m.writer().writeByte(i);
            m.writer().writeInt(t.num);
            m.writer().writeByte(status);
            m.writer().writeShort(t.cat.length);
            for (int j = 0; j < t.cat.length; j++) {
                if (t.cat[j] == 4) {
                    ItemTemplate4 itTemp4Select = ItemTemplate4.get_it_by_id(t.id[j]);
                    if (t.id[j] == -10) {
                        int level = p.level / 10;
                        if (level == 0) {
                            level = 1;
                        }
                        itTemp4Select = ItemTemplate4.get_it_by_id(level + 121);
                    }
                    if (itTemp4Select != null) {
                        if (itTemp4Select.id == 0) {
                            if (t.quant[j] == 1000) {
                                m.writer().writeUTF("b " + itTemp4Select.name);
                            } else {
                                m.writer().writeUTF("m " + itTemp4Select.name);
                            }
                        } else {
                            m.writer().writeUTF(itTemp4Select.name);
                        }
                        m.writer().writeByte(t.cat[j]);
                        m.writer().writeShort(itTemp4Select.icon);
                        if (itTemp4Select.id == 0 && t.quant[j] == 1000) {
                            m.writer().writeShort(1);
                        } else {
                            m.writer().writeShort(t.quant[j]);
                        }
                        m.writer().writeByte(0);
                    }
                } else if (t.cat[j] == 105) {
                    ItemFashion itemFashion = ItemFashion.get_item(t.id[j]);
                    if (itemFashion != null) {
                        m.writer().writeUTF(itemFashion.name);
                        m.writer().writeByte(t.cat[j]);
                        m.writer().writeShort(itemFashion.idIcon);
                        m.writer().writeShort(t.quant[j]);
                        m.writer().writeByte(0);
                    }
                } else if (t.cat[j] == 7) {
                    ItemTemplate7 itTemp7Select = ItemTemplate7.get_it_by_id(t.id[j]);
                    if (itTemp7Select != null) {
                        m.writer().writeUTF(itTemp7Select.name);
                        m.writer().writeByte(t.cat[j]);
                        m.writer().writeShort(itTemp7Select.icon);
                        m.writer().writeShort(t.quant[j]);
                        m.writer().writeByte(0);
                    }
                }
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void claimReward(Player p, int id) throws IOException {
        if (id < 0 || id >= ENTRY.size()) {
            return;
        }
        int tongnap = Math.max(p.tieu_ruby, p.tichtieu_ruby);
        p.tieu_ruby = tongnap;
        p.tichtieu_ruby = tongnap;
        List_tich_tieu t = ENTRY.get(id);

        if (tongnap < t.num) {
            Service.send_box_ThongBao_OK(p, "Bạn chưa đủ điều kiện để nhận phần thưởng này!");
            return;
        }

        if ((id < p.tichTieuRubyCheck.length && p.tichTieuRubyCheck[id] == 1) || p.claimedTichtieuRuby.contains(id)) {
            Service.send_box_ThongBao_OK(p, "Bạn đã nhận quà mốc này rồi!");
            return;
        }

        // Kiểm tra dung lượng túi đồ
        int countBagItems = 0;
        for (int j = 0; j < t.cat.length; j++) {
            if (t.cat[j] == 4) {
                if (t.id[j] == 0) {
                    continue; // Beri cộng trực tiếp
                }
                short realId = t.id[j];
                if (realId == -10) {
                    int level = p.level / 10;
                    if (level == 0) level = 1;
                    realId = (short) (level + 121);
                }
                if (p.item.total_item_bag_by_id(4, realId) == 0) {
                    countBagItems++;
                }
                if ((p.item.total_item_bag_by_id(4, realId) + t.quant[j]) > template.DataTemplate.MAX_ITEM_IN_BAG) {
                    Service.send_box_ThongBao_OK(p, "Số lượng vật phẩm trong hành trang vượt quá giới hạn!");
                    return;
                }
            } else if (t.cat[j] == 7) {
                if (p.item.total_item_bag_by_id(7, t.id[j]) == 0) {
                    countBagItems++;
                }
                if ((p.item.total_item_bag_by_id(7, t.id[j]) + t.quant[j]) > template.DataTemplate.MAX_ITEM_IN_BAG) {
                    Service.send_box_ThongBao_OK(p, "Số lượng vật phẩm trong hành trang vượt quá giới hạn!");
                    return;
                }
            }
        }
        if (countBagItems > p.item.able_bag()) {
            Service.send_box_ThongBao_OK(p, "Hành trang của bạn không đủ chỗ trống! Cần ít nhất " + countBagItems + " ô trống.");
            return;
        }

        List<GiftBox> listGift = new ArrayList<>();
        for (int i = 0; i < t.cat.length; i++) {
            if (t.cat[i] == 4) {
                short realId = t.id[i];
                if (realId == -10) {
                    int level = p.level / 10;
                    if (level == 0) level = 1;
                    realId = (short) (level + 121);
                }
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(realId);
                if (itemTemplate4 != null) {
                    GiftBox gb4 = new GiftBox();
                    gb4.id = itemTemplate4.id;
                    gb4.type = 4;
                    gb4.name = itemTemplate4.name;
                    gb4.icon = itemTemplate4.icon;
                    gb4.color = 0;
                    if (itemTemplate4.id == 0) {
                        long amount = (t.quant[i] == 1000 ? 1_000_000_000L : (long) t.quant[i] * 1_000_000L);
                        gb4.num = (int) Math.min(amount, Integer.MAX_VALUE);
                        if (t.quant[i] == 1000) {
                            gb4.name = "1 Tỷ " + itemTemplate4.name;
                        } else {
                            gb4.name = t.quant[i] + " Triệu " + itemTemplate4.name;
                        }
                    } else {
                        gb4.num = t.quant[i];
                    }
                    listGift.add(gb4);
                }
            } else if (t.cat[i] == 7) {
                ItemTemplate7 itemTemplate7 = ItemTemplate7.get_it_by_id(t.id[i]);
                if (itemTemplate7 != null) {
                    GiftBox gb7 = new GiftBox();
                    gb7.id = itemTemplate7.id;
                    gb7.type = 7;
                    gb7.name = itemTemplate7.name;
                    gb7.icon = itemTemplate7.icon;
                    gb7.num = t.quant[i];
                    gb7.color = 0;
                    listGift.add(gb7);
                }
            } else if (t.cat[i] == 105) {
                ItemFashion itf = ItemFashion.get_item(t.id[i]);
                if (itf != null) {
                    GiftBox gbf = new GiftBox();
                    gbf.id = itf.ID;
                    gbf.type = 105;
                    gbf.name = itf.name;
                    gbf.icon = itf.idIcon;
                    gbf.num = t.quant[i];
                    gbf.color = 0;
                    listGift.add(gbf);
                }
            }
        }

        if (id < p.tichTieuRubyCheck.length) {
            p.tichTieuRubyCheck[id] = 1;
        }
        if (!p.claimedTichtieuRuby.contains(id)) {
            p.claimedTichtieuRuby.add(id);
        }

        // Service.send_gift gửi opcode -34 hiển thị popup quà nhận được trên màn hình
        // đồng thời tự động cộng vật phẩm/vàng/thời trang vào tài khoản
        if (!listGift.isEmpty()) {
            Service.send_gift(p, 1, "Phần thưởng", "Phần thưởng", listGift, true);
        }

        p.update_money();
        p.item.update_Inventory(-1, false);

        // Gửi opcode -96 type 2 để cập nhật nút đã nhận trên UI mà không đóng popup quà
        Message m = new Message(-96);
        m.writer().writeByte(2);
        m.writer().writeByte(id);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void showSubMenu(Player p, int index) throws IOException {
        if (index < 0 || index >= ENTRY.size()) {
            return;
        }
        List_tich_tieu t = ENTRY.get(index);
        p.id_menu_tichtieu = index;
        core.MenuController.send_dynamic_menu(p, 9085, "Mốc " + Util.number_format(t.num) + " Ruby",
                new String[] { "Nhận quà", "Xem phần thưởng" }, null);
    }

    public static void showReward(Player p, int index) throws IOException {
        if (index < 0 || index >= ENTRY.size()) {
            return;
        }
        List_tich_tieu t = ENTRY.get(index);
        List<String> menuItems = new ArrayList<>();
        for (int j = 0; j < t.cat.length; j++) {
            if (t.cat[j] == 4) {
                ItemTemplate4 itTemp4Select = ItemTemplate4.get_it_by_id(t.id[j]);
                if (t.id[j] == -10) {
                    int level = p.level / 10;
                    if (level == 0) level = 1;
                    itTemp4Select = ItemTemplate4.get_it_by_id(level + 121);
                }
                if (itTemp4Select != null) {
                    String name = itTemp4Select.name;
                    if (itTemp4Select.id == 0) {
                        if (t.quant[j] == 1000) {
                            name = "1 Tỷ " + name;
                        } else {
                            name = t.quant[j] + " Triệu " + name;
                        }
                    } else {
                        name = name + " x" + Util.number_format(t.quant[j]);
                    }
                    menuItems.add(name);
                }
            } else if (t.cat[j] == 7) {
                ItemTemplate7 itTemp7Select = ItemTemplate7.get_it_by_id(t.id[j]);
                if (itTemp7Select != null) {
                    menuItems.add(itTemp7Select.name + " x" + Util.number_format(t.quant[j]));
                }
            } else if (t.cat[j] == 105) {
                ItemFashion itf = ItemFashion.get_item(t.id[j]);
                if (itf != null) {
                    menuItems.add(itf.name + " x" + Util.number_format(t.quant[j]));
                }
            }
        }
        core.MenuController.send_dynamic_menu(p, 9086,
                "Phần thưởng mốc " + Util.number_format(t.num) + " Ruby",
                menuItems.toArray(new String[0]), null);
    }

    public static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        byte id = m2.reader().readByte();
        if (type == 1) {
            claimReward(p, id);
        }
    }
}
