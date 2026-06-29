package client;

import java.io.IOException;
import java.sql.*;
import java.util.ArrayList;
import java.util.regex.Pattern;
import core.Manager;
import core.Service;
import core.Util;
import database.SQL;
import io.Message;
import map.Map;
import template.*;
/**
 *
 * @author Truongbk
 */
public class ClientInput {
    public static void process(Player p, Message m2) throws IOException {
        short id = m2.reader().readShort();
        String[] name = new String[m2.reader().readByte()];
        for (int i = 0; i < name.length; i++) {
            name[i] = m2.reader().readUTF();
        }
        switch (id) {
            case 11: {
                if (name.length == 1) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    long value = Integer.parseInt(name[0]);
                    if (value <= 0) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    if (p.clan != null) {
                        if (value > 2_000_000_000
                                || (value + (long) p.clan.get_ngoc()) > 2_000_000_000) {
                            Service.send_box_ThongBao_OK(p, "Số dư quá lớn, hãy thử lại sau");
                            return;
                        }
                        if (p.get_ngoc() < value) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + value + " ruby");
                            return;
                        }
                        p.update_ngoc(-value);
                        p.update_money();
                        p.clan.update_ruby((int) value);
                        for (int i = 0; i < p.clan.members.size(); i++) {
                            Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                            if (p0 != null) {
                                Clan.send_info(p0, false);
                            }
                        }
                        Service.send_box_ThongBao_OK(p,
                                "Góp " + value + " ruby vào quỹ băng thành công");
                    }
                }
                break;
            }
            case 10: {
                if (name.length == 1) {
                    if (name[0].length() < 3) {
                        Service.send_box_ThongBao_OK(p, "Tên băng nên nhiều hơn 3 ký tự");
                        return;
                    }
                    if (p.get_ngoc() < 2000) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 2000 ruby");
                        return;
                    }
                    if (Clan.get_clan_by_name(name[0]) != null) {
                        Service.send_box_ThongBao_OK(p,
                                "Tên băng này đã được sử dụng, hãy sử dụng tên khác");
                        return;
                    }
                    //
                    Clan clan = new Clan();
                    clan.id = (short) Clan.get_clan_id();
                    clan.name = name[0];
                    clan.opAttri = new short[] {0, 0, 0, 0, 0};
                    clan.pointAttri = 2;
                    clan.maxAttri = 20;
                    clan.icon = 0;
                    clan.level = 1;
                    clan.xp = 0;
                    clan.thongbao = "";
                    clan.trungsinh = 0;
                    clan.countAction = 0;
                    clan.allowRequest = 1;
                    clan.chat = new ArrayList<>();
                    clan.mem_request = new ArrayList<>();
                    clan.list_it = new ArrayList<>();
                    clan.buff = new ArrayList<>();
                    //
                    clan.members = new ArrayList<>();
                    Clan_member mem = new Clan_member();
                    mem.name = p.name;
                    mem.conghien = 0;
                    mem.donate = 0;
                    mem.gopRuby = 32_000;
                    mem.numquest = 3;
                    mem.id = 0;
                    mem.hair = (short) p.get_hair();
                    mem.head = (short) p.get_head();
                    mem.hat = p.get_hat();
                    mem.level = p.level;
                    mem.levelInclan = 0;
                    mem.clazz = p.clazz;
                    clan.members.add(mem);
                    //
                    if (Clan.create_new_clan(clan)) {
                        p.update_ngoc(-2000);
                        p.update_money();
                        //
                        p.clan = clan;
                        Clan.send_info(p, false);
                        for (int i = 0; i < p.map.players.size(); i++) {
                            if (!p.map.players.get(i).equals(p)) {
                                Clan.send_me_to_other(p, p.map.players.get(i), false);
                            }
                        }
                        Message m = new Message(-19); // show table select icon
                        m.writer().writeByte(98);
                        m.writer().writeUTF("Cửa hàng biểu tượng");
                        m.writer().writeByte(107);
                        m.writer().writeShort(10);
                        for (int i = 0; i < 10; i++) {
                            m.writer().writeShort(i);
                            m.writer().writeShort(i);
                            m.writer().writeUTF("Huy hiệu " + (i + 1));
                            m.writer().writeUTF(
                                    "Được làm từ gì đấy không biết nữa, mua đeo vào rất đẹp");
                            m.writer().writeShort(0);
                        }
                        p.conn.addmsg(m);
                        m.cleanup();
                    } else {
                        Service.send_box_ThongBao_OK(p,
                                "Tên băng này đã được sử dụng, hãy thử lại tên khác");
                    }
                }
                break;
            }
            case 7: {
                if (name.length == 1 && p.data_yesno != null && p.data_yesno.length == 3) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    int value = Integer.parseInt(name[0]);
                    if (value < 20_000 || value > 1_000_000_000) {
                        Service.send_box_ThongBao_OK(p,
                                "Mức giá bán tối thiểu là 20.000 Extol và tối đa là 1.000.000.000 Extol");
                        return;
                    }
                    int type = p.data_yesno[0];
                    int id_ = p.data_yesno[1];
                    int value_ = p.data_yesno[2];
                    p.data_yesno = new int[] {type, id_, value_, value};
                    if (type == 4) {
                        Service.send_box_yesno(p, 22, "Thông báo",
                                ("Bạn có muốn bán " + value_ + " "
                                        + ItemTemplate4.get_item_name(id_) + " với giá "
                                        + Util.number_format(value)
                                        + " Extol? Phí để đăng bán là 2.000 Extol"),
                                new String[] {"2.000 Extol", "Không"}, new byte[] {-1, -1});
                    } else if (type == 7) {
                        Service.send_box_yesno(p, 22, "Thông báo",
                                ("Bạn có muốn bán " + value_ + " "
                                        + ItemTemplate7.get_item_name(id_) + " với giá "
                                        + Util.number_format(value)
                                        + " Extol? Phí để đăng bán là 2.000 Extol"),
                                new String[] {"2.000 Extol", "Không"}, new byte[] {-1, -1});
                    }
                }
                break;
            }
            case 6: {
                if (name.length == 1 && p.data_yesno != null && p.data_yesno.length == 1) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    int value = Integer.parseInt(name[0]);
                    if (value < 20_000 || value > 1_000_000_000) {
                        Service.send_box_ThongBao_OK(p,
                                "Mức giá bán tối thiểu là 20.000 Extol và tối đa là 1.000.000.000 Extol");
                        return;
                    }
                    int price = p.data_yesno[0];
                    p.data_yesno = new int[] {price, value};
                    Service.send_box_yesno(p, 18, "Thông báo",
                            ("Bạn có muốn bán " + price + " triệu beri với giá "
                                    + Util.number_format(value)
                                    + " Extol? Phí để đăng bán là 2.000 Extol"),
                            new String[] {"2.000 Extol", "Không"}, new byte[] {-1, -1});
                }
                break;
            }
            case 5: {
                if (name.length == 1 && p.data_yesno != null) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    int value = Integer.parseInt(name[0]);
                    if (value < 20_000 || value > 1_000_000_000) {
                        Service.send_box_ThongBao_OK(p,
                                "Mức giá bán tối thiểu là 20.000 Extol và tối đa là 1.000.000.000 Extol");
                        return;
                    }
                    Item_wear it_select = p.item.bag3[p.data_yesno[0]];
                    if (it_select != null) {
                        p.data_yesno = new int[] {it_select.index, value};
                        Service.send_box_yesno(p, 17, "Thông báo",
                                ("Bạn có muốn bán vật phẩm " + it_select.template.name + " với giá "
                                        + Util.number_format(value)
                                        + " Extol? Phí để đăng bán là 2.000 Extol"),
                                new String[] {"2.000 Extol", "Không"}, new byte[] {-1, -1});
                    }
                }
                break;
            }
            case 4: {
                if (name.length == 1 && p.data_yesno == null) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    long value = Long.parseLong(name[0]) * 1000L;
                    if (value <= 0) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    if (p.get_vnd() < value) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không đủ " + Util.number_format(value) + " extol");
                        return;
                    }
                    int ruby = (int) ((long) value / 1000);
                    p.data_yesno = new int[] {ruby};
                    Service.send_box_yesno(p, 9, "Thông báo",
                            "Bạn có thật sự muốn đổi " + Util.number_format(value) + " Extol để"
                                    + " đổi lấy " + Util.number_format(ruby) + " Ruby không?",
                            new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
                    break;
                }
                break;
            }
            case 8: {
                if (name.length == 1 && p.data_yesno == null) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    long value = Long.parseLong(name[0])*5 ;
                    if (value <= 0 ) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    if (p.conn.coin < value) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không đủ " + Util.number_format(value) + " coin");
                        return;
                    }
                    int ruby = (int) ((long) value / 5);
                    p.data_yesno = new int[] {ruby};
                    Service.send_box_yesno(p, 60, "Thông báo",
                            "Bạn có thật sự muốn đổi " + Util.number_format(value) + " Coin để"
                                    + " đổi lấy " + Util.number_format(ruby) + " Ruby không?",
                            new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
                    break;
                }
                break;
            }
            case 9: {
                if (name.length == 1 && p.data_yesno == null) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    long value = Long.parseLong(name[0])/5000;
                    if (value <= 0 ) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    if (p.conn.coin < value) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không đủ " + Util.number_format(value) + " coin");
                        return;
                    }
                    int beri = (int) ((long) value * 5000);
                    p.data_yesno = new int[] {beri};
                    Service.send_box_yesno(p, 61, "Thông báo",
                            "Bạn có thật sự muốn đổi " + Util.number_format(value) + " Coin để"
                                    + " đổi lấy " + Util.number_format(beri) + " Beri không?",
                            new String[] {"Đồng ý", "Hủy"}, new byte[] {2, 1});
                    break;
                }
                break;
            }
            case 2: {
                if (name.length == 2) {
                    name[0] = name[0].replace(" ", "");
                    name[1] = name[1].replace(" ", "");
                    name[0] = name[0].toLowerCase();
                    name[1] = name[1].toLowerCase();
                    if (name[0].contains("admin") || name[1].contains("admin")) {
                        Service.send_box_ThongBao_OK(p,
                                "Tên tài khoản và mật khẩu không được trùng admin!");
                        return;
                    }
                    Pattern pat = Pattern.compile("^[a-zA-Z0-9@.]{1,30}$");
                    if (!pat.matcher(name[0]).matches() || !pat.matcher(name[1]).matches()) {
                        Service.send_box_ThongBao_OK(p,
                                "Tên tài khoản và mật khẩu phải dài hơn 6 và không chứa ký tự đặc biệt!");
                        return;
                    }
                    Connection conn = null;
                    Statement st = null;
                    try {
                        conn = SQL.gI().getCon();
                        st = conn.createStatement();
                        st.executeUpdate(
                                "UPDATE `accounts` SET `user` = '" + name[0] + "', `pass` = '"
                                        + name[1] + "' WHERE BINARY `user` = '" + p.conn.user
                                        + "' AND BINARY `pass` = '" + p.conn.pass + "' LIMIT 1;");
                    } catch (SQLException e) {
                        // e.printStackTrace();
                        Service.send_box_ThongBao_OK(p, "Tên đã được sử dụng, hãy thử lại!");
                        return;
                    } finally {
                        try {
                            if (st != null) {
                                st.close();
                            }
                            if (conn != null) {
                                conn.close();
                            }
                        } catch (SQLException e) {
                            e.printStackTrace();
                        }
                    }
                    p.conn.user = name[0];
                    p.conn.pass = name[1];
                    Message m = new Message(-59);
                    m.writer().writeUTF(name[0]);
                    m.writer().writeUTF(name[1]);
                    p.conn.addmsg(m);
                    m.cleanup();
                }
                break;
            }
            case 1: {
                if (p.conn.status != 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Chưa Kích hoạt không thể nhận Giftcode");
                            return;
                        }
                if (name.length == 1) {
                    
                    Pattern pattern = Pattern.compile("^[a-zA-Z0-9]{1,20}$");
                    if (!pattern.matcher(name[0]).matches()) {
                        Service.send_box_ThongBao_OK(p, "Ký tự không hợp lệ");
                        return;
                    }
                    Service.send_box_ThongBao_OK(p, "Xin hãy đợi giây lát...");
                    Connection conn = null;
                    ResultSet rs = null;
                    Statement st = null;
                    GiftTemplate temp = null;
                    try {
                        conn = SQL.gI().getCon();
                        st = conn.createStatement();
                        rs = st.executeQuery("SELECT * FROM `giftcode` WHERE BINARY `giftname` = '"
                                + name[0] + "' LIMIT 1;");
                        if (!rs.next()) {
                            Service.send_box_ThongBao_OK(p,
                                    "Giftcode không tồn tại hoặc đã được nhập");
                            return;
                        }
                        temp = new GiftTemplate(rs.getString("giftname"), rs.getInt("luotnhap"),
                                rs.getInt("gioihan"), rs.getString("thongbao"), rs.getInt("beri"),
                                rs.getInt("ruby"), rs.getString("item"), rs.getString("used"),
                                rs.getString("special"));
                    } catch (SQLException e) {
                        e.printStackTrace();
                        Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra hãy thử lại!");
                        return;
                    } finally {
                        try {
                            if (rs != null) {
                                rs.close();
                            }
                            if (st != null) {
                                st.close();
                            }
                            if (conn != null) {
                                conn.close();
                            }
                        } catch (SQLException e) {
                            e.printStackTrace();
                        }
                    }
                    if (temp != null) {
                        if (temp.luotnhap >= temp.gioihan) {
                            Service.send_box_ThongBao_OK(p,
                                    "Giftcode này đã đạt lượt nhập tối đa!");
                            return;
                        }
                        if (!temp.used.isEmpty()) {
                            String[] used_ = temp.used.split(",");
                            for (int i = 0; i < used_.length; i++) {
                                if (!used_[i].isBlank() && used_[i].equals(p.name)) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Giftcode không tồn tại hoặc đã được nhập");
                                    return;
                                }
                            }
                        }
                        if (!temp.special.isEmpty()) { // quà chỉ dành cho 1 số acc
                            boolean can_receiv = false;
                            String[] used_ = temp.special.split(",");
                            for (int i = 0; i < used_.length; i++) {
                                if (!used_[i].isBlank() && used_[i].equals(p.name)) {
                                    can_receiv = true;
                                    break;
                                }
                            }
                            if (!can_receiv) {
                                Service.send_box_ThongBao_OK(p,
                                        "Bạn không có tên trong danh sách nhận giftcode này!");
                                return;
                            }
                        }
                        if (temp.type != null && temp.type.length > p.item.able_bag()) {
                            Service.send_box_ThongBao_OK(p,
                                    "Để nhận giftcode này hãy chuẩn bị ít nhất " + temp.type.length
                                            + " ô trống trong hành trang");
                            return;
                        }
                        GiftTemplate.update_used(temp, p.name);
                        p.update_vang(temp.beri);
                        p.update_ngoc(temp.ruby);
                        p.update_money();
                        if (temp.type != null) {
                            for (int i = 0; i < temp.type.length; i++) {
                                switch (temp.type[i]) {
                                    case 3: {
                                        Item_wear it_add = new Item_wear();
                                        it_add.setup_template_by_id(temp.id[i]);
                                        if (it_add.template != null) {
                                            p.item.add_item_bag3(it_add);
                                        }
                                        break;
                                    }
                                    case 4:
                                    case 7: {
                                        if (temp.type[i] == 4 && temp.id[i] == 6) {
                                            p.update_ticket(temp.quant[i]);
                                            p.update_money();
                                        } else {
                                            p.item.add_item_bag47(temp.type[i], temp.id[i],
                                                    temp.quant[i]);
                                        }
                                        break;
                                    }
                                }
                            }
                            p.item.update_Inventory(-1, false);
                        }
                        String notice = "Bạn nhận được:" + "\nBeri : " + temp.beri + "\nRuby : "
                                + temp.ruby + "\nItem :\n";
                        if (temp.type != null) {
                            for (int i = 0; i < temp.type.length; i++) {
                                switch (temp.type[i]) {
                                    case 3: {
                                        notice += ItemTemplate3.get_it_by_id(temp.id[i]).name + " x"
                                                + temp.quant[i] + "\n";
                                        break;
                                    }
                                    case 4: {
                                        notice += ItemTemplate4.get_it_by_id(temp.id[i]).name + " x"
                                                + temp.quant[i] + "\n";
                                        break;
                                    }
                                    case 7: {
                                        notice += ItemTemplate7.get_it_by_id(temp.id[i]).name + " x"
                                                + temp.quant[i] + "\n";
                                        break;
                                    }
                                }
                            }
                        }
                        notice += "\n" + temp.notice;
                        Service.send_box_ThongBao_OK(p, notice);
                    }
                }
                break;
            }
            case 32002: {
                if (p.conn.user.equals("admin")) {
                    if (name.length == 3) {
                        if (!Util.isnumber(name[0]) || !Util.isnumber(name[1])
                                || !Util.isnumber(name[2])) {
                            Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                            return;
                        }
                        int value1 = Integer.parseInt(name[0]);
                        int value2 = Integer.parseInt(name[1]);
                        int value3 = Integer.parseInt(name[2]);
                        if (value3 <= 0 || value3 > 32000) {
                            value3 = 1;
                        }
                        switch (value1) {
                            case 3: {
                                ItemTemplate3 temp = ItemTemplate3.get_it_by_id(value2);
                                if (temp != null) {
                                    Item_wear it_add = new Item_wear();
                                    it_add.setup_template_by_id(temp);
                                    if (it_add.template != null) {
                                        p.item.add_item_bag3(it_add);
                                    }
                                    p.item.update_Inventory(-1, false);
                                    Service.send_box_ThongBao_OK(p, "Lấy thành công " + temp.name);
                                }
                                break;
                            }
                            case 4: {
                                ItemTemplate4 temp = ItemTemplate4.get_it_by_id(value2);
                                if (temp != null) {
                                    p.item.add_item_bag47(4, temp.id, value3);
                                    p.item.update_Inventory(-1, false);
                                    Service.send_box_ThongBao_OK(p,
                                            "Lấy thành công " + value3 + " " + temp.name);
                                }
                                break;
                            }
                            case 7: {
                                ItemTemplate7 temp = ItemTemplate7.get_it_by_id(value2);
                                if (temp != null) {
                                    p.item.add_item_bag47(7, temp.id, value3);
                                    p.item.update_Inventory(-1, false);
                                    Service.send_box_ThongBao_OK(p,
                                            "Lấy thành công " + value3 + " " + temp.name);
                                }
                                break;
                            }
                        }
                    }
                }
                break;
            }
            case 32000: {
                if (p.conn.user.equals("admin")) {
                    if (name.length == 1) {
                        if (!Util.isnumber(name[0])) {
                            Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                            return;
                        }
                        int value = Integer.parseInt(name[0]);
                        if (value > 100) {
                            value = 100;
                        }
                        if (value == 1) {
                            value = 2;
                        }
                        p.level = (short) (value - 1);
                        p.exp = Level.ENTRYS[p.level - 1].exp - 1;
                        p.update_exp(1, false);
                        p.reset_point(0);
                    }
                }
                break;
            }
            case 32001: {
                if (p.conn.user.equals("admin")) {
                    if (name.length == 1) {
                        if (!Util.isnumber(name[0])) {
                            Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                            return;
                        }
                        int value = Integer.parseInt(name[0]);
                        if (value < 0 || value > 10_000_000) {
                            value = 1;
                        }
                        Manager.gI().exp = value;
                        Service.send_box_ThongBao_OK(p, "Thay đổi xp x" + value);
                    }
                }
                break;
            }
        }
    }
}
