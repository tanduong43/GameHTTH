package client;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

import activities.Fight;
import core.Manager;
import core.Service;
import core.Util;
import database.SQL;
import io.Message;
import map.Map;
import template.Clan_member;
import template.GiftBox;
import template.GiftTemplate;
import template.ItemTemplate3;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.Item_wear;
import template.Level;

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
            case activities.Bank.INPUT_ID_BANK_DEPOSIT: {
                activities.Bank.processDepositInput(p, name);
                break;
            }
            case activities.Bank.INPUT_ID_BANK_EXCHANGE_COIN: {
                activities.Bank.processExchangeCoinInput(p, name);
                break;
            }
            case activities.GiftRuby.INPUT_ID_GIFT_RUBY: {
                activities.GiftRuby.processGiftRubyInput(p, name);
                break;
            }
            case Fight.INPUT_ID_FIGHT_RUBY: { // Nhập số ruby cược cho thách đấu siêu hạng
                if (name.length == 1) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số ruby không hợp lệ");
                        p.fight_click_target = null;
                        return;
                    }
                    int rubyBet = Integer.parseInt(name[0]);
                    if (rubyBet <= 0) {
                        Service.send_box_ThongBao_OK(p, "Số ruby cược phải lớn hơn 0");
                        p.fight_click_target = null;
                        return;
                    }
                    if (p.get_ngoc() < rubyBet) {
                        Service.send_box_ThongBao_OK(p, "Bạn không đủ " + rubyBet + " ruby!");
                        p.fight_click_target = null;
                        return;
                    }
                    Player p0 = p.fight_click_target;
                    p.fight_click_target = null;
                    if (p0 == null || !p0.map.equals(p.map)) {
                        Service.send_box_ThongBao_OK(p, "Đối phương đã rời khỏi đây");
                        return;
                    }
                    if (p0.targetFight != null) {
                        Service.send_box_ThongBao_OK(p, "Đối phương đang nhận lời mời từ người khác");
                        return;
                    }
                    // Gửi lời mời kèm số ruby cược, typeFight=1 (siêu hạng)
                    Fight.sendFightInvite(p, p0, rubyBet, 1);
                }
                break;
            }
            case 271: {
                if (name.length == 1) {
                    String newName = name[0].replace(" ", "");
                    newName = newName.toLowerCase();
                    Pattern pat = Pattern.compile("^[a-zA-Z0-9]{6,10}$");
                    if (!pat.matcher(newName).matches()) {
                        Service.send_box_ThongBao_OK(p,
                                "Tên không hợp lệ, nhập lại đi (6-10 ký tự, không ký tự đặc biệt)!");
                        return;
                    }
                    if (p.item.total_item_bag_by_id(4, 271) <= 0) {
                        Service.send_box_ThongBao_OK(p, "Bạn không có Thẻ đổi tên trong hành trang!");
                        return;
                    }
                    Connection conn = null;
                    PreparedStatement psCheck = null;
                    ResultSet rs = null;
                    PreparedStatement psUpdate = null;
                    try {
                        conn = SQL.gI().getCon();
                        psCheck = conn.prepareStatement("SELECT 1 FROM `players` WHERE BINARY `name` = ? LIMIT 1;");
                        psCheck.setString(1, newName);
                        rs = psCheck.executeQuery();
                        if (rs.next()) {
                            Service.send_box_ThongBao_OK(p, "Tên nhân vật đã tồn tại, vui lòng chọn tên khác!");
                            return;
                        }
                        rs.close();
                        psCheck.close();

                        // Perform update in players table
                        psUpdate = conn.prepareStatement("UPDATE `players` SET `name` = ? WHERE `id` = ? LIMIT 1;");
                        psUpdate.setString(1, newName);
                        psUpdate.setInt(2, p.id);
                        psUpdate.executeUpdate();
                        psUpdate.close();

                        String oldName = p.name;

                        // Update session list_char
                        for (int i = 0; i < p.conn.list_char.size(); i++) {
                            if (p.conn.list_char.get(i).equals(oldName)) {
                                p.conn.list_char.set(i, newName);
                            }
                        }
                        p.conn.flush();

                        // Update Clan if any
                        if (p.clan != null) {
                            for (int i = 0; i < p.clan.members.size(); i++) {
                                if (p.clan.members.get(i).name.equals(oldName)) {
                                    p.clan.members.get(i).name = newName;
                                }
                            }
                            for (int i = 0; i < Clan.ENTRY.size(); i++) {
                                Clan c = Clan.ENTRY.get(i);
                                for (int j = 0; j < c.members.size(); j++) {
                                    if (c.members.get(j).name.equals(oldName)) {
                                        c.members.get(j).name = newName;
                                    }
                                }
                            }
                            Clan.update();
                        }

                        // Remove item
                        p.item.remove_item47(4, 271, 1);

                        // Save player data first
                        client.Player.flush(p, true);

                        // Notify success
                        Service.send_box_ThongBao_OK(p,
                                "Đổi tên thành công! Hệ thống sẽ tự động đăng xuất sau 3 giây để tải lại dữ liệu mới.");

                        // Disconnect session after 3 seconds to force relog and reload cleanly
                        io.Session ss = p.conn;
                        new Thread(() -> {
                            try {
                                Thread.sleep(3000);
                            } catch (InterruptedException e) {
                                e.printStackTrace();
                            }
                            ss.disconnect();
                        }).start();
                    } catch (SQLException e) {
                        e.printStackTrace();
                        Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra khi đổi tên, vui lòng thử lại sau!");
                    } finally {
                        try {
                            if (rs != null)
                                rs.close();
                            if (psCheck != null)
                                psCheck.close();
                            if (psUpdate != null)
                                psUpdate.close();
                            if (conn != null)
                                conn.close();
                        } catch (SQLException e) {
                            e.printStackTrace();
                        }
                    }
                }
                break;
            }
            case -89: {
                if (name.length == 2) {
                    if (!Util.isnumber(name[1])) {
                        Service.send_box_ThongBao_OK(p, "Số beri không hợp lệ");
                        return;
                    }
                    long value = Long.parseLong(name[1]);
                    if (value <= 0 || value > 2_000_000_000) {
                        Service.send_box_ThongBao_OK(p, "Số beri không hợp lệ");
                        return;
                    }
                    if (p.get_vang() < value) {
                        Service.send_box_ThongBao_OK(p, "Không đủ " + Util.number_format(value) + " beri");
                        return;
                    }
                    Player p0 = Map.get_player_by_name_allmap(name[0]);
                    if (p0 == null) {
                        Service.send_box_ThongBao_OK(p, "Người chơi không online hoặc không tồn tại");
                        return;
                    }
                    if (p0.name.equals(p.name)) {
                        Service.send_box_ThongBao_OK(p, "Không thể tự treo thưởng bản thân");
                        return;
                    }
                    p.update_vang(-value);
                    p.update_money();
                    p0.thosan_bounty += (int) value;
                    p0.time_bounty_posted = System.currentTimeMillis();
                    p0.last_bounty_announce_time = System.currentTimeMillis();
                    client.Player.flush(p0, false); // Save to database immediately
                    core.BXH.updateThoSanBounty(); // Refresh ranking
                    Service.send_box_ThongBao_OK(p,
                            "Đã đăng truy nã " + Util.number_format(value) + " beri cho " + p0.name);
                    Service.send_box_ThongBao_OK(p0, p.name + " đã treo thưởng " + Util.number_format(value)
                            + " beri cho cái đầu của bạn. Lệnh có hiệu lực sau 10 phút.");
                    Manager.gI().chatKTG(0, "Thông báo: " + p.name + " đã treo thưởng " + Util.number_format(value)
                            + " beri cho cái đầu của " + p0.name, 5);
                }
                break;
            }
            case 11: {
                if (name.length == 1) {
                    if (p.conn.status != 1) {
                        Service.send_box_ThongBao_OK(p, "Chức năng đóng góp ruby chỉ dành cho tài khoản đã mở thành viên!");
                        return;
                    }
                    if (p.level < 40) {
                        Service.send_box_ThongBao_OK(p, "Cần đạt cấp độ trên 40 mới có thể đóng góp ruby vào băng!");
                        return;
                    }
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
                            if (p.clan.members.get(i).name.equals(p.name)) {
                                p.clan.members.get(i).gopRuby += (short) Math.min(value, Short.MAX_VALUE - p.clan.members.get(i).gopRuby);
                            }
                            Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                            if (p0 != null) {
                                Clan.update_list_member(p0, false);
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
                    if (p.get_ngoc() < 10000) {
                        Service.send_box_ThongBao_OK(p, "Không đủ 10000 ruby");
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
                    clan.opAttri = new short[] { 0, 0, 0, 0, 0 };
                    clan.pointAttri = 2;
                    clan.maxAttri = 20;
                    clan.icon = Clan.get_first_available_icon();
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
                        p.update_ngoc(-10000);
                        p.update_money();
                        //
                        p.clan = clan;
                        Clan.send_info(p, false);
                        for (int i = 0; i < p.map.players.size(); i++) {
                            if (!p.map.players.get(i).equals(p)) {
                                Clan.send_me_to_other(p, p.map.players.get(i), false);
                            }
                        }
                        List<Short> availableIcons = new ArrayList<>();
                        for (short i = 0; i < 10; i++) {
                            if (!Clan.is_icon_used(i, p.clan)) {
                                availableIcons.add(i);
                            }
                        }
                        if (availableIcons.isEmpty()) {
                            for (short i = 0; i < 278; i++) {
                                if (!Clan.is_icon_used(i, p.clan)) {
                                    availableIcons.add(i);
                                    if (availableIcons.size() >= 10) {
                                        break;
                                    }
                                }
                            }
                        }
                        Message m = new Message(-19); // show table select icon
                        m.writer().writeByte(98);
                        m.writer().writeUTF("Cửa hàng biểu tượng");
                        m.writer().writeByte(107);
                        m.writer().writeShort(availableIcons.size());
                        for (short iconId : availableIcons) {
                            m.writer().writeShort(iconId);
                            m.writer().writeShort(iconId);
                            m.writer().writeUTF("Huy hiệu " + (iconId + 1));
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
                    p.data_yesno = new int[] { type, id_, value_, value };
                    if (type == 4) {
                        Service.send_box_yesno(p, 22, "Thông báo",
                                ("Bạn có muốn bán " + value_ + " "
                                        + ItemTemplate4.get_item_name(id_) + " với giá "
                                        + Util.number_format(value)
                                        + " Extol? Phí để đăng bán là 2.000 Extol"),
                                new String[] { "2.000 Extol", "Không" }, new byte[] { -1, -1 });
                    } else if (type == 7) {
                        Service.send_box_yesno(p, 22, "Thông báo",
                                ("Bạn có muốn bán " + value_ + " "
                                        + ItemTemplate7.get_item_name(id_) + " với giá "
                                        + Util.number_format(value)
                                        + " Extol? Phí để đăng bán là 2.000 Extol"),
                                new String[] { "2.000 Extol", "Không" }, new byte[] { -1, -1 });
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
                    p.data_yesno = new int[] { price, value };
                    Service.send_box_yesno(p, 18, "Thông báo",
                            ("Bạn có muốn bán " + price + " triệu beri với giá "
                                    + Util.number_format(value)
                                    + " Extol? Phí để đăng bán là 2.000 Extol"),
                            new String[] { "2.000 Extol", "Không" }, new byte[] { -1, -1 });
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
                        p.data_yesno = new int[] { it_select.index, value };
                        Service.send_box_yesno(p, 17, "Thông báo",
                                ("Bạn có muốn bán vật phẩm " + it_select.template.name + " với giá "
                                        + Util.number_format(value)
                                        + " Extol? Phí để đăng bán là 2.000 Extol"),
                                new String[] { "2.000 Extol", "Không" }, new byte[] { -1, -1 });
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
                    p.data_yesno = new int[] { ruby };
                    Service.send_box_yesno(p, 9, "Thông báo",
                            "Bạn có thật sự muốn đổi " + Util.number_format(value) + " Extol để"
                                    + " đổi lấy " + Util.number_format(ruby) + " Ruby không?",
                            new String[] { "Đồng ý", "Hủy" }, new byte[] { 2, 1 });
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
                    long value = Long.parseLong(name[0]) * 5;
                    if (value <= 0) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    if (p.conn.coin < value) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không đủ " + Util.number_format(value) + " coin");
                        return;
                    }
                    int ruby = (int) ((long) value / 5);
                    p.data_yesno = new int[] { ruby };
                    Service.send_box_yesno(p, 60, "Thông báo",
                            "Bạn có thật sự muốn đổi " + Util.number_format(value) + " Coin để"
                                    + " đổi lấy " + Util.number_format(ruby) + " Ruby không?",
                            new String[] { "Đồng ý", "Hủy" }, new byte[] { 2, 1 });
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
                    long value = Long.parseLong(name[0]) / 5000;
                    if (value <= 0) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    if (p.conn.coin < value) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không đủ " + Util.number_format(value) + " coin");
                        return;
                    }
                    int beri = (int) ((long) value * 5000);
                    p.data_yesno = new int[] { beri };
                    Service.send_box_yesno(p, 61, "Thông báo",
                            "Bạn có thật sự muốn đổi " + Util.number_format(value) + " Coin để"
                                    + " đổi lấy " + Util.number_format(beri) + " Beri không?",
                            new String[] { "Đồng ý", "Hủy" }, new byte[] { 2, 1 });
                    break;
                }
                break;
            }
            case 12: {
                if (name.length == 1 && p.data_yesno == null) {
                    if (!Util.isnumber(name[0])) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    long value = Long.parseLong(name[0]);
                    if (value <= 0) {
                        Service.send_box_ThongBao_OK(p, "Số nhập không hợp lệ");
                        return;
                    }
                    if (p.conn.coin < value) {
                        Service.send_box_ThongBao_OK(p,
                                "Bạn không đủ " + Util.number_format(value) + " coin");
                        return;
                    }
                    int coin = (int) value;
                    p.data_yesno = new int[] { coin };
                    Service.send_box_yesno(p, 62, "Thông báo",
                            "Bạn có thật sự muốn đổi " + Util.number_format(coin) + " Coin để"
                                    + " đổi lấy " + Util.number_format(coin * 100L) + " Ruby và "
                                    + Util.number_format(coin * 1000L) + " Extol không?",
                            new String[] { "Đồng ý", "Hủy" }, new byte[] { 2, 1 });
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
                    int isMember = 0;
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
                        isMember = rs.getInt("is_member");
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
                        if (isMember == 1 && p.conn.status != 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Giftcode này chỉ dành cho người chơi đã kích hoạt thành viên!");
                            return;
                        }
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
                                    case 8: {
                                        Pet petTemplate = Pet.getTemplate(temp.id[i]);
                                        if (petTemplate != null) {
                                            boolean duplicate = false;
                                            for (MyPet myPet : p.my_pet) {
                                                if (myPet.template.id == petTemplate.id) {
                                                    duplicate = true;
                                                    break;
                                                }
                                            }
                                            if (!duplicate) {
                                                MyPet newPet = new MyPet();
                                                newPet.id = (short) p.my_pet.size();
                                                newPet.template = petTemplate;
                                                newPet.isUse = false;
                                                p.my_pet.add(newPet);
                                            }
                                        }
                                        break;
                                    }
                                    case 11: {
                                        template.ItemFashionP2 checkF = p.check_fashion(temp.id[i]);
                                        if (checkF == null) {
                                            template.ItemFashionP2 temp2 = new template.ItemFashionP2();
                                            temp2.id = temp.id[i];
                                            temp2.is_use = false;
                                            long dur = template.ItemFashion.getDefaultDurationMs(temp.id[i]);
                                            temp2.expiryTime = (dur > 0) ? (System.currentTimeMillis() + dur) : -1;
                                            p.fashion.add(temp2);
                                        }
                                        break;
                                    }
                                }
                            }
                            p.item.update_Inventory(-1, false);
                        }
                        List<GiftBox> listGift = new ArrayList<>();
                        if (temp.beri > 0) {
                            ItemTemplate4 itemBeri = ItemTemplate4.get_it_by_id(0);
                            if (itemBeri != null) {
                                GiftBox gb = new GiftBox();
                                gb.id = 0;
                                gb.type = 4;
                                gb.name = itemBeri.name;
                                gb.icon = itemBeri.icon;
                                gb.num = temp.beri;
                                gb.color = 0;
                                listGift.add(gb);
                            }
                        }
                        if (temp.ruby > 0) {
                            ItemTemplate4 itemRuby = ItemTemplate4.get_it_by_id(1);
                            if (itemRuby != null) {
                                GiftBox gb = new GiftBox();
                                gb.id = 1;
                                gb.type = 4;
                                gb.name = "Ruby";
                                gb.icon = itemRuby.icon;
                                gb.num = temp.ruby;
                                gb.color = 0;
                                listGift.add(gb);
                            }
                        }
                        if (temp.type != null) {
                            for (int i = 0; i < temp.type.length; i++) {
                                GiftBox gb = new GiftBox();
                                gb.num = temp.quant[i];
                                gb.type = (byte) temp.type[i];
                                gb.color = 0;
                                switch (temp.type[i]) {
                                    case 3: {
                                        ItemTemplate3 it = ItemTemplate3.get_it_by_id(temp.id[i]);
                                        if (it != null) {
                                            gb.id = it.id;
                                            gb.name = it.name;
                                            gb.icon = it.icon;
                                            gb.color = it.color;
                                            listGift.add(gb);
                                        }
                                        break;
                                    }
                                    case 4: {
                                        ItemTemplate4 it = ItemTemplate4.get_it_by_id(temp.id[i]);
                                        if (it != null) {
                                            gb.id = it.id;
                                            gb.name = it.name;
                                            gb.icon = it.icon;
                                            listGift.add(gb);
                                        }
                                        break;
                                    }
                                    case 7: {
                                        ItemTemplate7 it = ItemTemplate7.get_it_by_id(temp.id[i]);
                                        if (it != null) {
                                            gb.id = it.id;
                                            gb.name = it.name;
                                            gb.icon = it.icon;
                                            listGift.add(gb);
                                        }
                                        break;
                                    }
                                    case 8: {
                                        Pet petTemplate = Pet.getTemplate(temp.id[i]);
                                        if (petTemplate != null) {
                                            gb.id = petTemplate.id;
                                            gb.name = petTemplate.name;
                                            gb.icon = petTemplate.icon;
                                            gb.type = 110; // Trong bộ sưu tập Pet đang dùng byte 110, thử dùng 110 xem
                                                           // Client có load icon Pet không
                                            listGift.add(gb);
                                        }
                                        break;
                                    }
                                    case 11: {
                                        template.ItemFashion it = template.ItemFashion.get_item(temp.id[i]);
                                        if (it != null) {
                                            gb.id = it.ID;
                                            gb.name = it.name;
                                            gb.icon = it.idIcon;
                                            gb.type = 105; // Type 105 để client load icon thời trang
                                            listGift.add(gb);
                                        }
                                        break;
                                    }
                                }
                            }
                        }

                        String notice = "Bạn nhận được các vật phẩm từ giftcode";
                        if (temp.notice != null && !temp.notice.isEmpty()) {
                            notice += "\n" + temp.notice;
                        }

                        // Gửi thẳng message hiển thị quà để tránh bị Service.send_gift add thêm item
                        // lần 2
                        Message m = new Message(-34);
                        m.writer().writeByte(1); // type = 1
                        m.writer().writeUTF("Quà giftcode");
                        m.writer().writeUTF(notice);
                        m.writer().writeByte(listGift.size());
                        for (int i = 0; i < listGift.size(); i++) {
                            GiftBox gb = listGift.get(i);
                            m.writer().writeByte(gb.type);
                            m.writer().writeUTF(gb.name);
                            m.writer().writeShort(gb.icon);
                            m.writer().writeInt(gb.num);
                            m.writer().writeByte(gb.color);
                        }
                        p.conn.addmsg(m);
                        m.cleanup();
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
                        Manager.gI().setRateExp(value, value, true);
                        Service.send_box_ThongBao_OK(p, "Thay đổi tỉ lệ EXP & EXP Skill x" + value + " thành công!");
                    }
                }
                break;
            }
            case 32005: {
                if (p.conn.user.equals("admin")) {
                    if (name.length == 8) {
                        String giftName = name[0];
                        if (!Util.isnumber(name[1]) || !Util.isnumber(name[2]) || !Util.isnumber(name[3])
                                || !Util.isnumber(name[4]) || !Util.isnumber(name[5]) || !Util.isnumber(name[6])
                                || !Util.isnumber(name[7])) {
                            Service.send_box_ThongBao_OK(p,
                                    "Beri, Ruby, Giới hạn, Loại Item, ID Item, Số lượng và MTV phải là số!");
                            return;
                        }
                        int beri = Integer.parseInt(name[1]);
                        int ruby = Integer.parseInt(name[2]);
                        int gioihan = Integer.parseInt(name[3]);
                        int type_item = Integer.parseInt(name[4]);
                        int id_item = Integer.parseInt(name[5]);
                        int so_luong = Integer.parseInt(name[6]);
                        int is_member = Integer.parseInt(name[7]);

                        String itemJson = "[]";
                        if (type_item >= 0 && id_item >= 0 && so_luong > 0) {
                            itemJson = "[[" + type_item + "," + id_item + "," + so_luong + "]]";
                        }

                        try (Connection conn = SQL.gI().getCon();
                                PreparedStatement ps = conn.prepareStatement(
                                        "INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`) VALUES (?, ?, ?, ?, '', 0, ?, '[]', '', ?)")) {
                            ps.setString(1, giftName);
                            ps.setInt(2, beri);
                            ps.setInt(3, ruby);
                            ps.setString(4, itemJson);
                            ps.setInt(5, gioihan);
                            ps.setInt(6, is_member);
                            ps.executeUpdate();
                            Service.send_box_ThongBao_OK(p, "Tạo Giftcode thành công!\nMã: " + giftName + "\nBeri: "
                                    + beri + "\nRuby: " + ruby + "\nItem: "
                                    + (type_item >= 0 ? "Loại " + type_item + ", ID " + id_item + " (x" + so_luong + ")"
                                            : "Không có")
                                    + "\nGiới hạn: " + gioihan + "\nChỉ MTV: " + (is_member == 1 ? "Có" : "Không"));
                        } catch (SQLException e) {
                            e.printStackTrace();
                            Service.send_box_ThongBao_OK(p, "Lỗi: Giftcode này có thể đã tồn tại trong CSDL!");
                        }
                    }
                }
                break;
            }
        }
    }
}
