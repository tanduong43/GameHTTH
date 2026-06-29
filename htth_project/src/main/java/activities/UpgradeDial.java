package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.Item_wear;
import template.Option;
import java.io.IOException;
/**
 *
 * @author Truongbk
 */
public class UpgradeDial {
    public static void show_table(Player p) throws IOException {
        Message m = new Message(-94);
        m.writer().writeByte(7);
        p.conn.addmsg(m);
        m.cleanup();
        p.tool_dial = new byte[] {0, 0, 0};
    }

    public static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        short id = m2.reader().readShort();
        byte beri_gem = m2.reader().readByte();
        byte num = m2.reader().readByte();
        if (type == 4 && beri_gem == 0 && num == 0) { // select item
            Item_wear it_select = p.item.bag3[id];
            if (it_select != null) {
                if (it_select.template.typeEquip == 7) {
                    Message m = new Message(-94);
                    m.writer().writeByte(4);
                    m.writer().writeShort(id);
                    m.writer().writeByte(3);
                    m.writer().writeShort(4);
                    m.writer().writeShort(get_botvang(it_select.levelup));
                    m.writer().writeShort(1);
                    m.writer().writeShort(get_botCH(it_select.levelup));
                    m.writer().writeShort(13);
                    m.writer().writeShort(get_longvu(it_select.levelup));
                    m.writer().writeInt(get_beri_up(it_select.levelup));
                    m.writer().writeInt(get_ruby_up(it_select.levelup));
                    m.writer().writeInt(get_extol_up(it_select.levelup));
                    m.writer()
                            .writeByte((it_select.levelup == 0 || it_select.levelup == 3) ? 0 : 85);
                    p.conn.addmsg(m);
                    m.cleanup();
                } else {
                    Service.send_box_ThongBao_OK(p, "Chỉ có thể bỏ dial vào để nâng cấp");
                }
            }
            p.tool_dial = new byte[] {0, 0, 0};
        } else if (type == 5 && id == 11 && (beri_gem == 0 || beri_gem == 1)) { // bo tool
            if (num >= 0 && num <= 3) {
                Message m = new Message(-94);
                m.writer().writeByte(5);
                m.writer().writeByte(beri_gem);
                m.writer().writeShort(id);
                m.writer().writeByte(num);
                m.writer().writeByte(10);
                p.conn.addmsg(m);
                m.cleanup();
                p.tool_dial[0] = num;
            } else {
                Service.send_box_ThongBao_OK(p, "Chỉ bỏ tối đa 3 món");
            }
        } else if (type == 6 && id == 6 && (beri_gem == 0 || beri_gem == 1)) { // bo tool
            if (num >= 0 && num <= 3) {
                Message m = new Message(-94);
                m.writer().writeByte(6);
                m.writer().writeByte(beri_gem);
                m.writer().writeShort(id);
                m.writer().writeByte(num);
                m.writer().writeByte((beri_gem == 0) ? 0 : (5 * num));
                p.conn.addmsg(m);
                m.cleanup();
                p.tool_dial[1] = num;
            } else {
                Service.send_box_ThongBao_OK(p, "Chỉ bỏ tối đa 3 món");
            }
        } else if (type == 14 && id == 10 && (beri_gem == 0 || beri_gem == 1) && num >= 0
                && num <= 1) { // bo tool
            Message m = new Message(-94);
            m.writer().writeByte(14);
            m.writer().writeByte(beri_gem);
            m.writer().writeShort(id);
            p.conn.addmsg(m);
            m.cleanup();
            p.tool_dial[2] = num;
        } else if (type == 1 && beri_gem == 0 && num == 0) { // start
            Item_wear it_select = p.item.bag3[id];
            if (it_select != null) {
                if (it_select.template.typeEquip == 7) {
                    if (it_select.levelup < 5) {
                        int beri_req = get_beri_up(it_select.levelup);
                        int extol_req = get_extol_up(it_select.levelup);
                        if (p.get_vang() < beri_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + beri_req + " beri");
                            return;
                        }
                        if (p.get_vnd() < extol_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + extol_req + " extol");
                            return;
                        }
                        int botCH_req = get_botCH(it_select.levelup);
                        int botvang_req = get_botvang(it_select.levelup);
                        int longvu_req = get_longvu(it_select.levelup);
                        if (p.item.total_item_bag_by_id(7, 1) < botCH_req) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không đủ " + botCH_req + " Bột cường hóa");
                            return;
                        }
                        if (p.item.total_item_bag_by_id(7, 4) < botvang_req) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không đủ " + botvang_req + " Bột vàng");
                            return;
                        }
                        if (p.item.total_item_bag_by_id(7, 13) < longvu_req) {
                            Service.send_box_ThongBao_OK(p, "Không đủ " + longvu_req + " Lông vũ");
                            return;
                        }
                        if (p.tool_dial[0] > 0
                                && p.item.total_item_bag_by_id(7, 11) < p.tool_dial[0]) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không đủ " + p.tool_dial[0] + " Thiên thạch may mắn");
                            return;
                        }
                        if (p.tool_dial[1] > 0
                                && p.item.total_item_bag_by_id(7, 6) < p.tool_dial[1]) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không đủ " + p.tool_dial[1] + " Mai rùa");
                            return;
                        }
                        if (p.tool_dial[2] > 0
                                && p.item.total_item_bag_by_id(7, 10) < p.tool_dial[2]) {
                            Service.send_box_ThongBao_OK(p,
                                    "Không đủ " + p.tool_dial[2] + " Khiên");
                            return;
                        }
                        p.update_vang(-beri_req);
                        p.update_vnd(-extol_req);
                        p.update_money();
                        p.item.remove_item47(7, 1, botCH_req);
                        p.item.remove_item47(7, 4, botvang_req);
                        p.item.remove_item47(7, 13, longvu_req);
                        if (p.tool_dial[0] > 0) {
                            p.item.remove_item47(7, 11, p.tool_dial[0]);
                        }
                        if (p.tool_dial[1] > 0) {
                            p.item.remove_item47(7, 6, p.tool_dial[1]);
                        }
                        if (p.tool_dial[2] > 0) {
                            p.item.remove_item47(7, 10, p.tool_dial[2]);
                        }
                        boolean suc = (80 - it_select.levelup * 20 + p.tool_dial[0] * 15) > Util
                                .random(200);
                        if (suc) {
                            it_select.levelup++;
                            Message m = new Message(-94);
                            m.writer().writeByte(2);
                            m.writer().writeUTF(
                                    "Nâng cấp thành công vật phẩm lên +" + it_select.levelup);
                            p.conn.addmsg(m);
                            m.cleanup();
                        } else {
                            int percent_rotcap = 85 - p.tool_dial[1] * 5 - p.tool_dial[2] * 20;
                            if (percent_rotcap > Util.random(100)) {
                                int tier_save = (it_select.levelup >= 3) ? 3 : 0;
                                it_select.levelup -= Util.random(1, 3);
                                if (it_select.levelup < tier_save) {
                                    it_select.levelup = (byte) tier_save;
                                }
                            }
                            Message m = new Message(-94);
                            m.writer().writeByte(3);
                            m.writer().writeUTF("Rất tiếc nâng cấp thất bại, vật phẩm rớt cấp về +"
                                    + it_select.levelup);
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                        //
                        if (it_select.levelup == 3 || it_select.levelup == 5
                                || (it_select.template.name.equals("Dial Truyền thuyết")
                                        && it_select.levelup == 1)) {
                            if (it_select.template.name.equals("Dial Thần Thoại")) {
                                boolean add = false;
                                if (it_select.levelup == 3 && it_select.option_item.size() == 2) {
                                    add = true;
                                } else if (it_select.levelup == 5
                                        && it_select.option_item.size() == 3) {
                                    add = true;
                                }
                                if (add) {
                                    int random_add = Util.random(7);
                                    switch (random_add) {
                                        case 0: {
                                            it_select.option_item.add(new Option(Util.random(5, 10),
                                                    Util.random(5, 10)));
                                            break;
                                        }
                                        case 1: {
                                            it_select.option_item.add(new Option(
                                                    Util.random(19, 21), Util.random(25, 50)));
                                            break;
                                        }
                                        case 2: {
                                            int value_random = Util.random(10, 15);
                                            while (value_random == 11) {
                                                value_random = Util.random(10, 15);
                                            }
                                            it_select.option_item.add(new Option(value_random,
                                                    Util.random(100, 200)));
                                            break;
                                        }
                                        case 3: {
                                            it_select.option_item
                                                    .add(new Option(56, Util.random(100, 200)));
                                            break;
                                        }
                                        case 4: {
                                            it_select.option_item
                                                    .add(new Option(4, Util.random(100, 200)));
                                            break;
                                        }
                                        case 5: {
                                            it_select.option_item
                                                    .add(new Option(53, Util.random(50, 150)));
                                            break;
                                        }
                                        case 6: {
                                            it_select.option_item
                                                    .add(new Option(51, Util.random(30, 80)));
                                            break;
                                        }
                                    }
                                }
                            } else if (it_select.template.name.equals("Dial Truyền thuyết")) {
                                boolean add = false;
                                if (it_select.levelup == 1 && it_select.option_item.size() == 3) {
                                    add = true;
                                } else if (it_select.levelup == 3
                                        && it_select.option_item.size() == 4) {
                                    add = true;
                                } else if (it_select.levelup == 5
                                        && it_select.option_item.size() == 5) {
                                    add = true;
                                }
                                if (add) {
                                    int random_add = Util.random(11);
                                    switch (random_add) {
                                        case 0: {
                                            it_select.option_item.add(new Option(Util.random(5, 10),
                                                    Util.random(5, 10)));
                                            break;
                                        }
                                        case 1: {
                                            it_select.option_item.add(new Option(
                                                    Util.random(19, 21), Util.random(25, 50)));
                                            break;
                                        }
                                        case 2: {
                                            int value_random = Util.random(10, 15);
                                            while (value_random == 11) {
                                                value_random = Util.random(10, 15);
                                            }
                                            it_select.option_item.add(new Option(value_random,
                                                    Util.random(100, 200)));
                                            break;
                                        }
                                        case 3: {
                                            it_select.option_item
                                                    .add(new Option(56, Util.random(100, 200)));
                                            break;
                                        }
                                        case 4: {
                                            it_select.option_item
                                                    .add(new Option(4, Util.random(100, 200)));
                                            break;
                                        }
                                        case 5: {
                                            it_select.option_item
                                                    .add(new Option(53, Util.random(50, 150)));
                                            break;
                                        }
                                        case 6: {
                                            it_select.option_item
                                                    .add(new Option(51, Util.random(30, 80)));
                                            break;
                                        }
                                        case 7: {
                                            it_select.option_item
                                                    .add(new Option(50, Util.random(30, 80)));
                                            break;
                                        }
                                        case 8: {
                                            it_select.option_item
                                                    .add(new Option(52, Util.random(30, 80)));
                                            break;
                                        }
                                        case 9: {
                                            it_select.option_item
                                                    .add(new Option(63, Util.random(30, 80)));
                                            break;
                                        }
                                        case 10: {
                                            it_select.option_item
                                                    .add(new Option(49, Util.random(30, 80)));
                                            break;
                                        }
                                    }
                                }
                            } else if (it_select.template.name.equals("Dial Sử Thi")) {
                                boolean add = false;
                                if (it_select.levelup == 3 && it_select.option_item.size() == 1) {
                                    add = true;
                                } else if (it_select.levelup == 5
                                        && it_select.option_item.size() == 2) {
                                    add = true;
                                }
                                if (add) {
                                    int random_add = Util.random(5);
                                    switch (random_add) {
                                        case 0: {
                                            it_select.option_item.add(new Option(Util.random(5, 10),
                                                    Util.random(5, 10)));
                                            break;
                                        }
                                        case 1: {
                                            it_select.option_item.add(new Option(
                                                    Util.random(19, 21), Util.random(25, 50)));
                                            break;
                                        }
                                        case 2: {
                                            int value_random = Util.random(10, 15);
                                            while (value_random == 11) {
                                                value_random = Util.random(10, 15);
                                            }
                                            it_select.option_item.add(new Option(value_random,
                                                    Util.random(100, 200)));
                                            break;
                                        }
                                        case 3: {
                                            it_select.option_item
                                                    .add(new Option(56, Util.random(100, 200)));
                                            break;
                                        }
                                        case 4: {
                                            it_select.option_item
                                                    .add(new Option(4, Util.random(100, 200)));
                                            break;
                                        }
                                    }
                                }
                            } else if (it_select.template.name.equals("Dial Siêu Năng")) {
                                boolean add = false;
                                if (it_select.levelup == 3 && it_select.option_item.size() == 1) {
                                    add = true;
                                } else if (it_select.levelup == 5
                                        && it_select.option_item.size() == 2) {
                                    add = true;
                                }
                                if (add) {
                                    int random_add = Util.random(4);
                                    switch (random_add) {
                                        case 0: {
                                            it_select.option_item.add(new Option(Util.random(5, 10),
                                                    Util.random(5, 10)));
                                            break;
                                        }
                                        case 1: {
                                            it_select.option_item.add(new Option(
                                                    Util.random(19, 21), Util.random(25, 50)));
                                            break;
                                        }
                                        case 2: {
                                            int value_random = Util.random(10, 15);
                                            while (value_random == 11) {
                                                value_random = Util.random(10, 15);
                                            }
                                            it_select.option_item.add(new Option(value_random,
                                                    Util.random(100, 200)));
                                            break;
                                        }
                                        case 3: {
                                            it_select.option_item
                                                    .add(new Option(56, Util.random(100, 200)));
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        //
                        //
                        p.item.update_Inventory(-1, false);
                        p.tool_dial = new byte[] {0, 0, 0};
                    } else {
                        Service.send_box_ThongBao_OK(p, "Hiện tại trang bị đã nâng cấp tối đa");
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Chỉ có thể bỏ dial vào để nâng cấp");
                }
            }
        }
    }

    private static int get_longvu(byte levelup) {
        return (levelup + 1) * 4;
    }

    private static int get_botCH(byte levelup) {
        return (levelup + 1) * 100;
    }

    private static int get_botvang(byte levelup) {
        return (levelup + 1) * 70;
    }

    private static int get_beri_up(byte levelup) {
        return (levelup + 1) * 500_000;
    }

    private static int get_ruby_up(byte levelup) {
        return 0;
    }

    private static int get_extol_up(byte levelup) {
        return (levelup + 1) * 2_000;
    }
}
