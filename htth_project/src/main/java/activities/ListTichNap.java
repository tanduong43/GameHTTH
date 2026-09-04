package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import template.GiftBox;
import template.ItemFashion;
import template.ItemTemplate4;
import template.ItemTemplate7;

public class ListTichNap {

    public final static List<ListTichNap> ENTRY;

    static {
        ENTRY = new ArrayList<>();

        // MỐC 1: 50k Extol
        ListTichNap t = new ListTichNap();
        t.num = 50_000;
        t.cat = new byte[]{105, 4, 4, 7, 4, 4, 4, 4, 4, 4};
        t.id = new short[]{127, 339, 323, 10, 159, 349, 367, 455, 413, 327};
        t.quant = new short[]{1, 10, 1, 10, 20, 20, 2, 5, 2, 5};
        ENTRY.add(t);

        // MỐC 2: 100k Extol
        t = new ListTichNap();
        t.num = 100_000;
        t.cat = new byte[]{105, 4, 4, 4, 4, 4, 4, 7, 4, 7, 4, 4, 4};
        t.id = new short[]{53, 339, 221, 159, 349, 367, 1004, 13, 455, 10, 323, 327, 131}; // 1004 Rương đá thần thoại tự chọn
        t.quant = new short[]{1, 25, 100, 30, 30, 3, 1, 200, 5, 20, 1, 10, 20};
        ENTRY.add(t);

        // MỐC 3: 200k Extol
        t = new ListTichNap();
        t.num = 200_000;
        t.cat = new byte[]{105, 4, 7, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4};
        t.id = new short[]{55, 339, 10, 159, 349, 131, 1004, 457, 323, 327, 414, 367, 413}; // 1004 Rương đá thần thoại tự chọn
        t.quant = new short[]{1, 50, 30, 50, 50, 50, 2, 10, 1, 20, 1, 10, 5};
        ENTRY.add(t);

        // MỐC 4: 300k Extol
        t = new ListTichNap();
        t.num = 300_000;
        t.cat = new byte[]{105, 105, 4, 4, 4, 4, 4, 4, 4, 7, 4, 4};
        t.id = new short[]{95, 238, 226, 159, 349, 367, 457, 1004, 325, 10, 414, 323};
        t.quant = new short[]{1, 1, 50, 60, 60, 5, 10, 3, 5, 20, 1, 3};
        ENTRY.add(t);

        // MỐC 5: 500k Extol
        t = new ListTichNap();
        t.num = 500_000;
        t.cat = new byte[]{105, 105, 4, 4, 4, 4, 4, 4, 4, 4, 4, 7, 4, 4};
        t.id = new short[]{118, 116, 226, 159, 349, 367, 457, 455, 1004, 327, 325, 10, 414, 323};
        t.quant = new short[]{1, 1, 100, 100, 100, 10, 10, 10, 5, 5, 5, 50, 1, 5};
        ENTRY.add(t);

        // MỐC 6: 1M Extol
        t = new ListTichNap();
        t.num = 1_000_000;
        t.cat = new byte[]{105, 105, 4, 4, 4, 4, 4, 4, 7, 4, 4, 4};
        t.id = new short[]{74, 119, 226, 159, 349, 325, 1004, 413, 10, 457, 323, 455};
        t.quant = new short[]{1, 1, 150, 200, 200, 5, 10, 20, 100, 20, 6, 50};
        ENTRY.add(t);
    }

    public byte[] cat;
    public short[] id;
    public short[] quant;
    public int num;

    public static void syncAccountTichNap(Player p) {
        if (p == null || p.conn == null) {
            return;
        }
        Connection connection = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            connection = database.SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "SELECT `tichnap`, `claimed_milestones` FROM `accounts` WHERE `user` = ? LIMIT 1;");
            ps.setString(1, p.conn.user);
            rs = ps.executeQuery();
            if (rs.next()) {
                p.conn.tichnap = rs.getInt("tichnap");
                p.conn.tongnap = p.conn.tichnap;
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
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (rs != null) rs.close();
                if (ps != null) ps.close();
                if (connection != null) connection.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    private static int getTongNap(Player p) {
        syncAccountTichNap(p);
        return p.conn.tichnap;
    }

    public static void showTable(Player p) throws IOException {
        sendUI(p);
    }

    public static void sendUI(Player p) throws IOException {
        int tongnap = getTongNap(p);

        Message m = new Message(-90);
        m.writer().writeByte(0); // type 0: Open UI
        m.writer().writeInt(tongnap); // Tổng điểm tích luỹ
        m.writer().writeByte(ENTRY.size()); // Số mốc

        for (int i = 0; i < ENTRY.size(); i++) {
            ListTichNap t = ENTRY.get(i);
            byte status = 0;
            if ((i < p.tichTieuCheck.length && p.tichTieuCheck[i] == 1)
                    || p.claimedMilestones.contains(i)
                    || p.conn.claimed_milestones.contains(String.valueOf(i))) {
                status = 2; // Đã nhận
            } else if (tongnap >= t.num) {
                status = 1; // Đủ điều kiện nhận
            }

            m.writer().writeByte(i); // Index mốc
            m.writer().writeInt(t.num); // Mức tích nạp yêu cầu
            m.writer().writeByte(status); // Trạng thái
            m.writer().writeShort(t.cat.length + 1); // Số lượng quà + 1 danh hiệu

            // Add danh hiệu fake item
            String dhName = "";
            if (i == 0) dhName = "Fan Cứng";
            else if (i == 1) dhName = "Tuổi Thơ";
            else if (i == 2) dhName = "Top 4 Nạp";
            else if (i == 3) dhName = "Top 3 Nạp";
            else if (i == 4) dhName = "Top 2 Nạp";
            else if (i == 5) dhName = "Top 1 Nạp";

            m.writer().writeUTF("Danh hiệu " + dhName);
            m.writer().writeByte(4); // cat 4
            m.writer().writeShort(147); // icon vé
            m.writer().writeShort(1); // số lượng
            m.writer().writeByte(0); // option

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
        syncAccountTichNap(p);
        if (id < 0 || id >= ENTRY.size()) {
            return;
        }

        int tongnap = p.conn.tichnap;
        ListTichNap t = ENTRY.get(id);

        if (tongnap < t.num) {
            Service.send_box_ThongBao_OK(p, "Bạn chưa đủ điều kiện để nhận phần thưởng này!");
            return;
        }

        if ((id < p.tichTieuCheck.length && p.tichTieuCheck[id] == 1)
                || p.claimedMilestones.contains(id)
                || p.conn.claimed_milestones.contains(String.valueOf(id))) {
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

        if (id < p.tichTieuCheck.length) {
            p.tichTieuCheck[id] = 1;
        }
        if (!p.claimedMilestones.contains(id)) {
            p.claimedMilestones.add(id);
        }

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
        Connection connection = null;
        PreparedStatement ps = null;
        try {
            connection = database.SQL.gI().getCon();
            ps = connection.prepareStatement("UPDATE `accounts` SET `claimed_milestones` = ? WHERE `user` = ?");
            ps.setString(1, p.conn.claimed_milestones);
            ps.setString(2, p.conn.user);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (ps != null) ps.close();
                if (connection != null) connection.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }

        // Service.send_gift gửi opcode -34 hiển thị popup quà nhận được trên màn hình
        // đồng thời tự động cộng vật phẩm/vàng/thời trang vào tài khoản
        if (!listGift.isEmpty()) {
            Service.send_gift(p, 1, "Phần thưởng", "Phần thưởng", listGift, true);
        }

        // Add danh hiệu
        if (id == 0) addDanhHieuByName(p, "Fan Cứng");
        else if (id == 1) addDanhHieuByName(p, "Tuổi Thơ");
        else if (id == 2) addDanhHieuByName(p, "Top 4 Nạp");
        else if (id == 3) addDanhHieuByName(p, "Top 3 Nạp");
        else if (id == 4) addDanhHieuByName(p, "Top 2 Nạp");
        else if (id == 5) addDanhHieuByName(p, "Top 1 Nạp");

        p.update_money();
        p.item.update_Inventory(-1, false);

        // Gửi opcode -90 type 2 để cập nhật nút đã nhận trên UI mà không đóng popup quà
        Message m = new Message(-90);
        m.writer().writeByte(2); // type 2: update claimed status in UI
        m.writer().writeByte(id);
        p.conn.addmsg(m);
        m.cleanup();
    }

    private static void addDanhHieuByName(Player p, String name) throws IOException {
        for (activities.DanhHieu dh : activities.DanhHieu.ENY) {
            if (dh.Name.equalsIgnoreCase(name)) {
                p.add_danh_hieu(dh.id);
                return;
            }
        }
    }

    public static void showSubMenu(Player p, int index) throws IOException {
        if (index < 0 || index >= ENTRY.size()) {
            return;
        }
        ListTichNap t = ENTRY.get(index);
        p.id_menu_tichtieu = index;
        core.MenuController.send_dynamic_menu(p, 9087, "Mốc " + Util.number_format(t.num) + " Extol",
                new String[] { "Nhận quà", "Xem phần thưởng" }, null);
    }

    public static void showReward(Player p, int index) throws IOException {
        if (index < 0 || index >= ENTRY.size()) {
            return;
        }
        ListTichNap t = ENTRY.get(index);
        List<String> menuItems = new ArrayList<>();
        
        // Add danh hiệu text vào menu xem trước
        if (index == 0) menuItems.add("Danh hiệu: Fan Cứng");
        else if (index == 1) menuItems.add("Danh hiệu: Tuổi Thơ");
        else if (index == 2) menuItems.add("Danh hiệu: Top 4 Nạp");
        else if (index == 3) menuItems.add("Danh hiệu: Top 3 Nạp");
        else if (index == 4) menuItems.add("Danh hiệu: Top 2 Nạp");
        else if (index == 5) menuItems.add("Danh hiệu: Top 1 Nạp");

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
        core.MenuController.send_dynamic_menu(p, 9088,
                "Phần thưởng mốc " + Util.number_format(t.num) + " Extol",
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
