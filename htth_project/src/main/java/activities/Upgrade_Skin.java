package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.*;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
/**
 *
 * @author Truongbk
 */
public class Upgrade_Skin {
    public static byte[] PERCENT = new byte[] {1, 3, 4, 5, 10, 20};

    public static void show_table(Player p) throws IOException {
        p.upgrade_skin = new Upgrade_Skin_Info();
        p.upgrade_skin.upgrade_skin_data = new short[] {-1, -1, -1, -1, -1, -1};
        //
        Message m = new Message(81);
        m.writer().writeByte(0);
        m.writer().writeByte(105);
        m.writer().writeByte(p.fashion.size()); // size skin
        for (int i = 0; i < p.fashion.size(); i++) {
            ItemFashion temp = ItemFashion.get_item(p.fashion.get(i).id);
            if (temp != null) {
                m.writer().writeShort(temp.ID);
                m.writer().writeUTF("HTTH");
                m.writer().writeUTF("Mãi đỉnh");
                m.writer().writeShort(temp.idIcon);
                m.writer().writeByte(p.fashion.get(i).level);
            }
        }
        //
        List<ItemBag47> list_da_kham = new ArrayList<>();
        for (int i = 0; i < p.item.bag47.size(); i++) {
            ItemBag47 it47 = p.item.bag47.get(i);
            if (it47.category == 4) {
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(it47.id);
                if (itemTemplate4.type == 12 && itemTemplate4.id < 80
                        && check_id_ngoc(itemTemplate4.id)) {
                    list_da_kham.add(it47);
                }
            }
            if (it47.category == 7 && (it47.id == 16 || it47.id == 17)) { // nlieu
                list_da_kham.add(it47);
            }
        }
        m.writer().writeByte(list_da_kham.size()); // size da kham
        for (int i = 0; i < list_da_kham.size(); i++) {
            if (list_da_kham.get(i).category == 4) {
                ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(list_da_kham.get(i).id);
                m.writer().writeByte(4);
                m.writer().writeShort(itemTemplate4.id);
                m.writer().writeShort(list_da_kham.get(i).quant);
                m.writer().writeUTF(itemTemplate4.name);
                m.writer().writeShort(itemTemplate4.icon);
            } else if (list_da_kham.get(i).category == 7
                    && (list_da_kham.get(i).id == 16 || list_da_kham.get(i).id == 17)) {
                ItemTemplate7 itemTemplate7 = ItemTemplate7.get_it_by_id(list_da_kham.get(i).id);
                m.writer().writeByte(7);
                m.writer().writeShort(itemTemplate7.id);
                m.writer().writeShort(list_da_kham.get(i).quant);
                m.writer().writeUTF(itemTemplate7.name);
                m.writer().writeShort(itemTemplate7.icon);
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    private static boolean check_id_ngoc(short id) {
        return Rebuild_Item.get_percent_hop_ngoc(id) > 1;
    }

    public static void process(Player p, Message m2) throws IOException {
        if (m2.reader().available() == 6) {
            byte type = m2.reader().readByte();
            byte cat = m2.reader().readByte();
            short id = m2.reader().readShort();
            byte pos = m2.reader().readByte();
            byte bovao = m2.reader().readByte();
            // System.out.println(type + " " + cat + " " + id + " " + pos + " " + bovao);
            if (type == 1 && cat == 105 && pos == 0 && bovao == 1) {
                ItemFashionP2 myFashion = p.check_fashion(id);
                if (myFashion != null && p.upgrade_skin != null) {
                    Message m3 = new Message(81);
                    m3.writer().writeByte(5);
                    m3.writer().writeByte(105);
                    m3.writer().writeByte(p.fashion.size()); // size skin
                    for (int i = 0; i < p.fashion.size(); i++) {
                        ItemFashion temp = ItemFashion.get_item(p.fashion.get(i).id);
                        if (temp != null) {
                            m3.writer().writeShort(temp.ID);
                            m3.writer().writeUTF("");
                            m3.writer().writeUTF("");
                            m3.writer().writeShort(temp.idIcon);
                            m3.writer().writeByte(p.fashion.get(i).level);
                        }
                    }
                    //
                    List<ItemBag47> list_da_kham = new ArrayList<>();
                    for (int i = 0; i < p.item.bag47.size(); i++) {
                        ItemBag47 it47 = p.item.bag47.get(i);
                        if (it47.category == 4) {
                            ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(it47.id);
                            if (itemTemplate4.type == 12 && itemTemplate4.id < 80
                                    && check_id_ngoc(itemTemplate4.id)) {
                                list_da_kham.add(it47);
                            }
                        }
                        if (it47.category == 7 && (it47.id == 16 || it47.id == 17)) { // nlieu
                            list_da_kham.add(it47);
                        }
                    }
                    m3.writer().writeByte(list_da_kham.size()); // size da kham
                    for (int i = 0; i < list_da_kham.size(); i++) {
                        if (list_da_kham.get(i).category == 4) {
                            ItemTemplate4 itemTemplate4 =
                                    ItemTemplate4.get_it_by_id(list_da_kham.get(i).id);
                            m3.writer().writeByte(4);
                            m3.writer().writeShort(itemTemplate4.id);
                            m3.writer().writeShort(list_da_kham.get(i).quant);
                            m3.writer().writeUTF(itemTemplate4.name);
                            m3.writer().writeShort(itemTemplate4.icon);
                        } else if (list_da_kham.get(i).category == 7
                                && (list_da_kham.get(i).id == 16 || list_da_kham.get(i).id == 17)) {
                            ItemTemplate7 itemTemplate7 =
                                    ItemTemplate7.get_it_by_id(list_da_kham.get(i).id);
                            m3.writer().writeByte(7);
                            m3.writer().writeShort(itemTemplate7.id);
                            m3.writer().writeShort(list_da_kham.get(i).quant);
                            m3.writer().writeUTF(itemTemplate7.name);
                            m3.writer().writeShort(itemTemplate7.icon);
                        }
                    }
                    p.conn.addmsg(m3);
                    m3.cleanup();
                }
                if (myFashion != null && p.upgrade_skin != null) { // bo vao hoac update sau khi
                                                                   // upgrade
                    Message m = new Message(81);
                    m.writer().writeByte(1);
                    m.writer().writeByte(105);
                    m.writer().writeShort(id);
                    m.writer().writeByte(0);
                    m.writer().writeByte(0);
                    m.writer().writeByte(1);
                    //
                    m.writer().writeInt(get_beri_up(myFashion.level));
                    m.writer().writeShort(get_ruby_up(myFashion.level));
                    m.writer().writeInt(get_extol_up(myFashion.level));
                    p.conn.addmsg(m);
                    m.cleanup();
                    p.upgrade_skin.skin = myFashion;
                }
            } else if (type == 1 && cat == 7 && (id == 16 || id == 17) && pos == 1 && bovao == 1) { // them
                                                                                                    // sao
                                                                                                    // 8
                                                                                                    // canh
                if (p.item.total_item_bag_by_id(7, id) > 0 && p.upgrade_skin != null
                        && p.upgrade_skin.skin != null) {
                    int percent2 = 20;
                    Message m = new Message(81);
                    m.writer().writeByte(1);
                    m.writer().writeByte(7);
                    m.writer().writeShort(id);
                    m.writer().writeByte(percent2); // percent
                    m.writer().writeByte(pos); // pos
                    m.writer().writeByte(1);
                    //
                    m.writer().writeInt(get_beri_up(p.upgrade_skin.skin.level));
                    m.writer().writeShort(get_ruby_up(p.upgrade_skin.skin.level));
                    m.writer().writeInt(get_extol_up(p.upgrade_skin.skin.level));
                    p.conn.addmsg(m);
                    m.cleanup();
                    p.upgrade_skin.upgrade_skin_data[1] = id;
                }
            } else if (type == 1 && cat == 7 && (id == 16 || id == 17) && pos == 1 && bovao == 0) { // huy
                                                                                                    // sao
                                                                                                    // 8
                                                                                                    // canh
                if (p.upgrade_skin != null && p.upgrade_skin.skin != null) {
                    Message m = new Message(81);
                    m.writer().writeByte(1);
                    m.writer().writeByte(7);
                    m.writer().writeShort(id);
                    m.writer().writeByte(0); // percent
                    m.writer().writeByte(pos); // pos
                    m.writer().writeByte(0);
                    //
                    m.writer().writeInt(get_beri_up(p.upgrade_skin.skin.level));
                    m.writer().writeShort(get_ruby_up(p.upgrade_skin.skin.level));
                    m.writer().writeInt(get_extol_up(p.upgrade_skin.skin.level));
                    p.conn.addmsg(m);
                    m.cleanup();
                    p.upgrade_skin.upgrade_skin_data[1] = -1;
                    //
                }
            } else if (type == 1 && cat == 4 && bovao == 1) {
                if (p.item.total_item_bag_by_id(4, id) > 0 && p.upgrade_skin != null
                        && p.upgrade_skin.skin != null && pos > 0
                        && pos < p.upgrade_skin.upgrade_skin_data.length && id < 80
                        && check_id_ngoc(id)) {
                    int percent = 20;
                    Message m = new Message(81);
                    m.writer().writeByte(1);
                    m.writer().writeByte(4);
                    m.writer().writeShort(id);
                    m.writer().writeByte(percent);
                    m.writer().writeByte(pos);
                    m.writer().writeByte(1);
                    //
                    m.writer().writeInt(get_beri_up(p.upgrade_skin.skin.level));
                    m.writer().writeShort(get_ruby_up(p.upgrade_skin.skin.level));
                    m.writer().writeInt(get_extol_up(p.upgrade_skin.skin.level));
                    p.conn.addmsg(m);
                    m.cleanup();
                    p.upgrade_skin.upgrade_skin_data[pos] = id;
                    //
                    if (p.upgrade_skin != null && p.upgrade_skin.skin != null
                            && (p.upgrade_skin.upgrade_skin_data[1] == 16
                                    || p.upgrade_skin.upgrade_skin_data[1] == 17)
                            && p.item.total_item_bag_by_id(7,
                                    p.upgrade_skin.upgrade_skin_data[1]) > 0) {
                        int percent2 = 20;
                        //
                        m = new Message(81);
                        m.writer().writeByte(1);
                        m.writer().writeByte(7);
                        m.writer().writeShort(p.upgrade_skin.upgrade_skin_data[1]);
                        m.writer().writeByte(percent2); // percent
                        m.writer().writeByte(1); // pos
                        m.writer().writeByte(1);
                        //
                        m.writer().writeInt(get_beri_up(p.upgrade_skin.skin.level));
                        m.writer().writeShort(get_ruby_up(p.upgrade_skin.skin.level));
                        m.writer().writeInt(get_extol_up(p.upgrade_skin.skin.level));
                        p.conn.addmsg(m);
                        m.cleanup();
                    }
                }
            } else if (type == 1 && cat == 4 && bovao == 0) {
                if (p.upgrade_skin != null && p.upgrade_skin.skin != null && pos > 0
                        && pos < p.upgrade_skin.upgrade_skin_data.length) {
                    Message m = new Message(81);
                    m.writer().writeByte(1);
                    m.writer().writeByte(4);
                    m.writer().writeShort(id);
                    m.writer().writeByte(0);
                    m.writer().writeByte(pos);
                    m.writer().writeByte(0);
                    //
                    m.writer().writeInt(get_beri_up(p.upgrade_skin.skin.level));
                    m.writer().writeShort(get_ruby_up(p.upgrade_skin.skin.level));
                    m.writer().writeInt(get_extol_up(p.upgrade_skin.skin.level));
                    p.conn.addmsg(m);
                    m.cleanup();
                    p.upgrade_skin.upgrade_skin_data[pos] = -1;
                    //
                    if (p.upgrade_skin != null && p.upgrade_skin.skin != null
                            && (p.upgrade_skin.upgrade_skin_data[1] == 16
                                    || p.upgrade_skin.upgrade_skin_data[1] == 17)
                            && p.item.total_item_bag_by_id(7,
                                    p.upgrade_skin.upgrade_skin_data[1]) > 0) {
                        int percent2 = 20;
                        //
                        m = new Message(81);
                        m.writer().writeByte(1);
                        m.writer().writeByte(7);
                        m.writer().writeShort(p.upgrade_skin.upgrade_skin_data[1]);
                        m.writer().writeByte(percent2); // percent
                        m.writer().writeByte(1); // pos
                        m.writer().writeByte(1);
                        //
                        m.writer().writeInt(get_beri_up(p.upgrade_skin.skin.level));
                        m.writer().writeShort(get_ruby_up(p.upgrade_skin.skin.level));
                        m.writer().writeInt(get_extol_up(p.upgrade_skin.skin.level));
                        p.conn.addmsg(m);
                        m.cleanup();
                    }
                }
            }
        } else if (m2.reader().available() == 3) {
            byte type = m2.reader().readByte();
            short id = m2.reader().readShort();
            // System.out.println(type + " " + id);
            if (type == 4) {
                ItemFashionP2 myFashion = p.check_fashion(id);
                if (myFashion != null && p.upgrade_skin != null && p.upgrade_skin.skin != null
                        && p.upgrade_skin.skin.id == myFashion.id) {
                    boolean check_da_kham = false;
                    for (int i = 0; i < p.upgrade_skin.upgrade_skin_data.length; i++) {
                        if (p.upgrade_skin.upgrade_skin_data[i] != -1) {
                            check_da_kham = true;
                            break;
                        }
                    }
                    if (check_da_kham) {
                        ItemFashion itF = ItemFashion.get_item(myFashion.id);
                        Message m = new Message(81);
                        m.writer().writeByte(4);
                        m.writer().writeUTF("Xác nhận nâng cấp thời trang " + itF.name);
                        m.writer().writeShort(id);
                        p.conn.addmsg(m);
                        m.cleanup();
                    } else {
                        Service.send_box_ThongBao_OK(p, "Hãy bỏ đá khảm trước");
                    }
                }
            }
        } else {
            byte type = m2.reader().readByte();
            if (type == 2) {
                if (p.upgrade_skin != null && p.upgrade_skin.skin != null
                        && p.upgrade_skin.skin.level >= 42) {
                    Service.send_box_ThongBao_OK(p, "Tạm thời mở nâng đến +42 thôi");
                    return;
                }
                //
                if (p.upgrade_skin.skin == null) {
                    return;
                }
                int beri_req = get_beri_up(p.upgrade_skin.skin.level);
                int ruby_req = get_ruby_up(p.upgrade_skin.skin.level);
                int extol_req = get_extol_up(p.upgrade_skin.skin.level);
                if (p.get_vang() < beri_req) {
                    Service.send_box_ThongBao_OK(p,
                            "Không đủ " + Util.number_format(beri_req) + " beri");
                    return;
                }
                if (p.get_ngoc() < ruby_req) {
                    Service.send_box_ThongBao_OK(p,
                            "Không đủ " + Util.number_format(ruby_req) + " ruby");
                    return;
                }
                if (p.get_vnd() < extol_req) {
                    Service.send_box_ThongBao_OK(p,
                            "Không đủ " + Util.number_format(extol_req) + " extol");
                    return;
                }
                //
                int percent = Util.random(20, 80);
                p.update_vang(-beri_req);
                p.update_ngoc(-ruby_req);
                p.update_vnd(-extol_req);
                p.update_money();
                boolean suc = ((percent >= 100) ? 120 : percent) > Util.random(120);
                if (suc) {
                    p.upgrade_skin.skin.level++;
                } else {
                    if (p.upgrade_skin.skin.level % 3 != 0) {
                        p.upgrade_skin.skin.level -= (p.upgrade_skin.skin.level % 3);
                    }
                }
                //
                Message m = new Message(81);
                m.writer().writeByte(3);
                m.writer().writeByte(suc ? 0 : 1); // 0 thanh cong, 1 that bai
                m.writer().writeShort(p.upgrade_skin.skin.id);
                m.writer().writeByte(105);
                m.writer().writeUTF(suc
                        ? ("Chúc mừng bạn cường hóa thành công lên +" + p.upgrade_skin.skin.level)
                        : ("Rất tiếc việc nâng cấp thất bại, thời trang rớt về +"
                                + p.upgrade_skin.skin.level));
                p.conn.addmsg(m);
                m.cleanup();
                //
                p.upgrade_skin.skin = null;
                p.upgrade_skin.upgrade_skin_data = new short[] {-1, -1, -1, -1, -1, -1};
                p.item.update_Inventory(-1, false);
                p.update_info_to_all();
            } else if (type == 6) {
                m2.reader().readShort(); // id
                byte size = m2.reader().readByte();
                for (int i = 0; i < size; i++) {
                    m2.reader().readShort(); // list da kham
                }
                Message m = new Message(81);
                m.writer().writeByte(6);
                m.writer().writeByte(0); // ti le may man + them
                p.conn.addmsg(m);
                m.cleanup();
            }
        }
    }

    private static int get_extol_up(int level) {
        return (get_ruby_up(level) * 1_000);
    }

    private static int get_ruby_up(int level) { // max 32_000
        if (level == 2) {
            return 5;
        } else {
            int result = 0;
            if (level == 2 || level % 3 == 2) {
                for (int i = 0; i < level; i++) {
                    if (i == 2 || i % 3 == 2) {
                        result += 5;
                    }
                }
            }
            return result;
        }
    }

    private static int get_beri_up(int level) {
        int result = 7_500;
        int moc_buff = 22_500;
        for (int i = 0; i <= level; i++) {
            if (i == 2 || i % 3 == 2) {
                result += moc_buff;
                if (moc_buff < 30_000) {
                    moc_buff += 7_500;
                }
            } else {
                result += 3_750;
            }
        }
        return result;
    }
}
