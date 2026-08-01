package activities;

import java.io.IOException;
import java.util.ArrayList;
import client.Item;
import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.*;
/**
 *
 * @author Truongbk
 */
public class Trade {
    public static void process(Player p, Message m2) throws IOException {
        byte action = m2.reader().readByte();
        int id = -1;
        byte cat = -1;
        int num = -1;
        String str = "";
        if (action == 1 || action == 6) {
            id = m2.reader().readShort();
            cat = m2.reader().readByte();
            num = m2.reader().readInt();
        }
        if (action == 2) {
            str = m2.reader().readUTF();
        }
        switch (action) {
            case 6: {
                
                if (num == 1) { // accept
                    Player p0 = p.map.get_player_by_id_inmap(id);
                    
                    if (p0 != null) {
                        
                        if (p0.trade_target == null || !p0.trade_target.equals(p)) {
                            Service.send_box_ThongBao_OK(p,
                                    "Đối phương đang giao dịch với người khác");
                            p.fee_trade = 0;
                            p.money_trade = 0;
                            p.is_lock_trade = false;
                            p.is_accept_trade = false;
                            p.list_item_trade3 = null;
                            p.list_item_trade47 = null;
                            p.trade_target = null;
                            return;
                        }
                        //
                        Trade.show_table(p, p.trade_target.name);
                        Trade.show_table(p.trade_target, p.name);
                    } else {
                        Service.send_box_ThongBao_OK(p, "Đối phương không online");
                    }
                } else if (num == 0) { // request
                    Player p0 = p.map.get_player_by_id_inmap(id);
                    if (p0 != null) {
                        if (p0.conn.status != 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Đối phương chưa Kích hoạt không thể giao dịch");
                            return;
                        }
                        if (p0.trade_target != null) {
                            Service.send_box_ThongBao_OK(p, "Đối phương đang có giao dịch");
                            return;
                        }
                        if (p.trade_target != null) {
                            Service.send_box_ThongBao_OK(p,
                                    "Bạn đang đợi đối phương chấp nhận lời mời giao dịch");
                            return;
                        }
                        p0.trade_target = p;
                        p.trade_target = p0;
                        //
                        Message m = new Message(-49);
                        m.writer().writeByte(6);
                        m.writer().writeByte(1);
                        m.writer().writeShort(p.index_map);
                        m.writer().writeUTF(p.name);
                        p0.conn.addmsg(m);
                        m.cleanup();
                    } else {
                        Service.send_box_ThongBao_OK(p, "Đối phương không online");
                    }
                }
                break;
            }
            case 1: {
                if (p.trade_target != null && p.is_lock_trade) {
                    Service.send_box_ThongBao_OK(p, "Không thể thực hiện khi đã khóa giao dịch");
                    return;
                }
                if (num == 1 && cat == 3 && p.trade_target != null) { // add item vao
                    Item_wear it_select = p.item.bag3[id];
                    if (it_select != null) {
                        boolean send_or_remove = true;
                        for (int i = 0; i < p.list_item_trade3.size(); i++) {
                            if (it_select.equals(p.list_item_trade3.get(i))) {
                                p.list_item_trade3.remove(it_select);
                                send_or_remove = false;
                                break;
                            }
                        }
                        if ((p.trade_target.item.able_bag() - p.list_item_trade3.size()
                                - p.list_item_trade47.size()) < 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Hành trang đối phương không đủ chỗ trống");
                            return;
                        }
                        if (send_or_remove) {
                            if (Trade.can_add_item_trade(p)) {
                                if (it_select.typelock == 1) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Trang bị đã khóa không thể giao dịch!");
                                    return;
                                }
                                Message m = new Message(-49);
                                m.writer().writeByte(1);
                                m.writer().writeByte(1);
                                m.writer().writeByte(3);
                                m.writer().writeByte(1);
                                Item.readUpdateItem(m.writer(), it_select, p);
                                p.trade_target.conn.addmsg(m);
                                m.cleanup();
                                //
                                m = new Message(-49);
                                m.writer().writeByte(1);
                                m.writer().writeByte(0);
                                m.writer().writeByte(3);
                                m.writer().writeByte(1);
                                Item.readUpdateItem(m.writer(), it_select, p);
                                p.conn.addmsg(m);
                                m.cleanup();
                                p.list_item_trade3.add(it_select);
                            } else {
                                Service.send_box_ThongBao_OK(p, "Không thể thêm vật phẩm");
                            }
                        } else {
                            Message m = new Message(-49);
                            m.writer().writeByte(1);
                            m.writer().writeByte(1);
                            m.writer().writeByte(3);
                            m.writer().writeByte(0);
                            m.writer().writeShort(id);
                            p.trade_target.conn.addmsg(m);
                            m.cleanup();
                            //
                            m = new Message(-49);
                            m.writer().writeByte(1);
                            m.writer().writeByte(0);
                            m.writer().writeByte(3);
                            m.writer().writeByte(0);
                            m.writer().writeShort(id);
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                    }
                } else if (num > 0 && cat == 6 && id == 0 && p.trade_target != null) { // add beri
                    if (num > 2_000_000_000) {
                        Service.send_box_ThongBao_OK(p, "Giao dịch tối đa 2 tỷ");
                    }
                    long beri_quant = ((long) num * 12L) / 10L;
                    if (p.get_vang() < beri_quant) {
                        Service.send_box_ThongBao_OK(p, "Không đủ " + Util.number_format(beri_quant)
                                + " beri, phí giao dịch beri 20%");
                        return;
                    }
                    //
                    Message m = new Message(-49);
                    m.writer().writeByte(1);
                    m.writer().writeByte(1);
                    m.writer().writeByte(6);
                    m.writer().writeInt(num);
                    p.trade_target.conn.addmsg(m);
                    m.cleanup();
                    //
                    m = new Message(-49);
                    m.writer().writeByte(1);
                    m.writer().writeByte(0);
                    m.writer().writeByte(6);
                    m.writer().writeInt(num);
                    p.conn.addmsg(m);
                    m.cleanup();
                    //
                    p.money_trade = num;
                } else if (num > 0 && cat == 7 && p.trade_target != null) { // add item7
                    ItemTemplate7 it_temp = ItemTemplate7.get_it_by_id(id);
                    if (it_temp != null) {
                        boolean send_or_remove = true;
                        for (int i = 0; i < p.list_item_trade47.size(); i++) {
                            if (p.list_item_trade47.get(i).category == 7
                                    && p.list_item_trade47.get(i).id == id) {
                                send_or_remove = false;
                                p.list_item_trade47.remove(p.list_item_trade47.get(i));
                                break;
                            }
                        }
                        if (send_or_remove) {
                            if (p.item.total_item_bag_by_id(7, id) < num) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ " + num + " " + it_temp.name);
                                return;
                            }
                            if (it_temp.istrade == 1) {
                                Service.send_box_ThongBao_OK(p,
                                        "Vật phẩm này không thể giao dịch!");
                                return;
                            }
                            int num_in_target_bag = p.trade_target.item.total_item_bag_by_id(7, id);
                            if (num > DataTemplate.MAX_ITEM_IN_BAG
                                    || (num_in_target_bag + num) > DataTemplate.MAX_ITEM_IN_BAG
                                    || (num_in_target_bag == 0 && (p.trade_target.item.able_bag()
                                            - p.list_item_trade3.size()
                                            - p.list_item_trade47.size()) < 1)) {
                                Service.send_box_ThongBao_OK(p,
                                        "Hành trang đối phương không đủ chỗ trống");
                                return;
                            }
                            if (it_temp.istrade == 0 && Trade.can_add_item_trade(p)) {
                                for (int i = 0; i < p.item.bag47.size(); i++) {
                                    if (p.item.bag47.get(i).category == 7
                                            && p.item.bag47.get(i).id == id) {
                                        ItemBag47 it_add = new ItemBag47();
                                        it_add.category = 7;
                                        it_add.id = (short) id;
                                        it_add.quant = (short) num;
                                        p.list_item_trade47.add(it_add);
                                        break;
                                    }
                                }
                                Message m = new Message(-49);
                                m.writer().writeByte(1);
                                m.writer().writeByte(1);
                                m.writer().writeByte(7);
                                m.writer().writeByte(1);
                                m.writer().writeByte(id);
                                m.writer().writeShort(num);
                                p.trade_target.conn.addmsg(m);
                                m.cleanup();
                                //
                                m = new Message(-49);
                                m.writer().writeByte(1);
                                m.writer().writeByte(0);
                                m.writer().writeByte(7);
                                m.writer().writeByte(1);
                                m.writer().writeByte(id);
                                m.writer().writeShort(num);
                                p.conn.addmsg(m);
                                m.cleanup();
                            } else {
                                Service.send_box_ThongBao_OK(p, "Không thể thêm vật phẩm");
                            }
                        } else {
                            Message m = new Message(-49);
                            m.writer().writeByte(1);
                            m.writer().writeByte(1);
                            m.writer().writeByte(7);
                            m.writer().writeByte(0);
                            m.writer().writeShort(id);
                            p.trade_target.conn.addmsg(m);
                            m.cleanup();
                            //
                            m = new Message(-49);
                            m.writer().writeByte(1);
                            m.writer().writeByte(0);
                            m.writer().writeByte(7);
                            m.writer().writeByte(0);
                            m.writer().writeShort(id);
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                    }
                } else if (num > 0 && cat == 4 && p.trade_target != null) { // add item4
                    ItemTemplate4 it_temp = ItemTemplate4.get_it_by_id(id);
                    if (it_temp != null) {
                        boolean send_or_remove = true;
                        for (int i = 0; i < p.list_item_trade47.size(); i++) {
                            if (p.list_item_trade47.get(i).category == 4
                                    && p.list_item_trade47.get(i).id == id) {
                                send_or_remove = false;
                                p.list_item_trade47.remove(p.list_item_trade47.get(i));
                                break;
                            }
                        }
                        int num_in_target_bag = p.trade_target.item.total_item_bag_by_id(4, id);
                        if (num > DataTemplate.MAX_ITEM_IN_BAG
                                || (num_in_target_bag + num) > DataTemplate.MAX_ITEM_IN_BAG
                                || (num_in_target_bag == 0 && (p.trade_target.item.able_bag()
                                        - p.list_item_trade3.size()
                                        - p.list_item_trade47.size()) < 1)) {
                            Service.send_box_ThongBao_OK(p,
                                    "Hành trang đối phương không đủ chỗ trống");
                            return;
                        }
                        if (send_or_remove) {
                            if (p.item.total_item_bag_by_id(4, id) < num) {
                                Service.send_box_ThongBao_OK(p,
                                        "Không đủ " + num + " " + it_temp.name);
                                return;
                            }
                            if (it_temp.istrade == 1) {
                                Service.send_box_ThongBao_OK(p,
                                        "Vật phẩm này không thể giao dịch!");
                                return;
                            }
                            if (it_temp.istrade == 0 && Trade.can_add_item_trade(p)) {
                                for (int i = 0; i < p.item.bag47.size(); i++) {
                                    if (p.item.bag47.get(i).category == 4
                                            && p.item.bag47.get(i).id == id) {
                                        ItemBag47 it_add = new ItemBag47();
                                        it_add.category = 4;
                                        it_add.id = (short) id;
                                        it_add.quant = (short) num;
                                        p.list_item_trade47.add(it_add);
                                        break;
                                    }
                                }
                                Message m = new Message(-49);
                                m.writer().writeByte(1);
                                m.writer().writeByte(1);
                                m.writer().writeByte(4);
                                m.writer().writeByte(1);
                                m.writer().writeShort(id);
                                m.writer().writeShort(num);
                                p.trade_target.conn.addmsg(m);
                                m.cleanup();
                                //
                                m = new Message(-49);
                                m.writer().writeByte(1);
                                m.writer().writeByte(0);
                                m.writer().writeByte(4);
                                m.writer().writeByte(1);
                                m.writer().writeShort(id);
                                m.writer().writeShort(num);
                                p.conn.addmsg(m);
                                m.cleanup();
                            } else {
                                Service.send_box_ThongBao_OK(p, "Không thể thêm vật phẩm");
                            }
                        } else {
                            Message m = new Message(-49);
                            m.writer().writeByte(1);
                            m.writer().writeByte(1);
                            m.writer().writeByte(4);
                            m.writer().writeByte(0);
                            m.writer().writeShort(id);
                            p.trade_target.conn.addmsg(m);
                            m.cleanup();
                            //
                            m = new Message(-49);
                            m.writer().writeByte(1);
                            m.writer().writeByte(0);
                            m.writer().writeByte(4);
                            m.writer().writeByte(0);
                            m.writer().writeShort(id);
                            p.conn.addmsg(m);
                            m.cleanup();
                        }
                    }
                }
                break;
            }
            case 5: { // thoat trade
                if (id == -1 && cat == -1 && num == -1 && p.trade_target != null) {
                    end_trade_by_disconnect(p.trade_target, p, 0, "");
                    end_trade_by_disconnect(p, p.trade_target, 0, "");
                }
                break;
            }
            case 2: { // chat popup
                if (id == -1 && cat == -1 && num == -1 && !str.isEmpty()
                        && p.trade_target != null) {
                    Message m = new Message(-49);
                    m.writer().writeByte(2);
                    m.writer().writeByte(1);
                    m.writer().writeUTF(str);
                    p.trade_target.conn.addmsg(m);
                    m.cleanup();
                }
                break;
            }
            case 3: { // lock
                if (id == -1 && cat == -1 && num == -1 && p.trade_target != null
                        && !p.is_lock_trade) {
                    Message m = new Message(-49);
                    m.writer().writeByte(3);
                    m.writer().writeByte(1);
                    p.trade_target.conn.addmsg(m);
                    m.cleanup();
                    //
                    m = new Message(-49);
                    m.writer().writeByte(3);
                    m.writer().writeByte(0);
                    p.conn.addmsg(m);
                    m.cleanup();
                    //
                    p.is_lock_trade = true;
                    if (p.money_trade > 0) {
                        p.fee_trade += (10 + (p.money_trade / 2_000));
                    }
                    for (int i = 0; i < p.list_item_trade3.size(); i++) {
                        Item_wear it_select = p.list_item_trade3.get(i);
                        p.fee_trade += it_select.template.color * 50;
                        p.fee_trade += (5_000 * it_select.numLoKham);
                        p.fee_trade += (5_000 * it_select.mdakham.length);
                        p.fee_trade += (5_000 * it_select.levelup);
                        if (it_select.valueKichAn > -1) {
                            p.fee_trade += 10_000;
                        }
                        p.fee_trade += (10_000 * it_select.isHoanMy);
                    }
                    int fee_item4 = 0;
                    for (int i = 0; i < p.list_item_trade47.size(); i++) {
                        fee_item4 += p.list_item_trade47.get(i).quant;
                    }
                    if (fee_item4 > 0 && fee_item4 <= 100) {
                        fee_item4 = 20;
                    }
                    if (fee_item4 > 100) {
                        fee_item4 /= 5;
                    }
                    p.fee_trade += fee_item4;
                    //
                    Service.send_box_ThongBao_OK(p.trade_target,
                            p.name + " đã khóa giao dịch, mức phí giao dịch hiện tại là "
                                    + p.trade_target.fee_trade + " Ruby");
                    Service.send_box_ThongBao_OK(p,
                            p.name + " đã khóa giao dịch, mức phí giao dịch hiện tại là "
                                    + p.fee_trade + " Ruby");
                }
                break;
            }
            case 4: { // accept
                if (id == -1 && cat == -1 && num == -1 && p.trade_target != null && p.is_lock_trade
                        && p.trade_target.is_lock_trade && !p.is_accept_trade) {
                    p.is_accept_trade = true;
                    if (p.trade_target.is_accept_trade) {
                        // fee
                        p.trade_target.update_ngoc(-p.trade_target.fee_trade);
                        p.update_ngoc(-p.fee_trade);
                        // trade beri
                        p.update_vang(-(p.money_trade * 12L) / 10L);
                        p.trade_target.update_vang(-(p.trade_target.money_trade * 12L) / 10L);
                        //
                        //
                        p.update_vang(p.trade_target.money_trade);
                        p.trade_target.update_vang(p.money_trade);
                        // reset beri if < 0
                        if (p.get_vang() < 0) {
                            p.update_vang(-p.get_vang());
                            p.trade_target.update_vang(p.get_vang());
                        }
                        if (p.trade_target.get_vang() < 0) {
                            p.trade_target.update_vang(-p.trade_target.get_vang());
                            p.update_vang(p.trade_target.get_vang());
                        }
                        //
                        p.update_money();
                        p.trade_target.update_money();
                        // trade item 3
                        for (int i = 0; i < p.list_item_trade3.size(); i++) {
                            p.item.remove_item_wear(p.list_item_trade3.get(i));
                        }
                        for (int i = 0; i < p.trade_target.list_item_trade3.size(); i++) {
                            p.trade_target.item
                                    .remove_item_wear(p.trade_target.list_item_trade3.get(i));
                        }
                        for (int i = 0; i < p.trade_target.list_item_trade3.size(); i++) {
                            Item_wear it_add = new Item_wear();
                            it_add.clone_obj(p.trade_target.list_item_trade3.get(i));
                            if (it_add.template != null) {
                                p.item.add_item_bag3(it_add);
                            }
                        }
                        for (int i = 0; i < p.list_item_trade3.size(); i++) {
                            Item_wear it_add = new Item_wear();
                            it_add.clone_obj(p.list_item_trade3.get(i));
                            if (it_add != null) {
                                p.trade_target.item.add_item_bag3(it_add);
                            }
                        }
                        // trade item 47
                        for (int i = 0; i < p.list_item_trade47.size(); i++) {
                            p.item.remove_item47(p.list_item_trade47.get(i).category,
                                    p.list_item_trade47.get(i).id,
                                    p.list_item_trade47.get(i).quant);
                        }
                        for (int i = 0; i < p.trade_target.list_item_trade47.size(); i++) {
                            p.trade_target.item.remove_item47(
                                    p.trade_target.list_item_trade47.get(i).category,
                                    p.trade_target.list_item_trade47.get(i).id,
                                    p.trade_target.list_item_trade47.get(i).quant);
                        }
                        for (int i = 0; i < p.trade_target.list_item_trade47.size(); i++) {
                            ItemBag47 it_add = p.trade_target.list_item_trade47.get(i);
                            p.item.add_item_bag47(it_add.category, it_add.id, it_add.quant);
                        }
                        for (int i = 0; i < p.list_item_trade47.size(); i++) {
                            ItemBag47 it_add = p.list_item_trade47.get(i);
                            p.trade_target.item.add_item_bag47(it_add.category, it_add.id,
                                    it_add.quant);
                        }
                        //
                        p.item.update_Inventory(-1, false);
                        p.trade_target.item.update_Inventory(-1, false);
                        end_trade_by_disconnect(p.trade_target, p, 1, "");
                        end_trade_by_disconnect(p, p.trade_target, 1, "");
                    } else {
                        Message m = new Message(-49);
                        m.writer().writeByte(4);
                        m.writer().writeByte(1);
                        p.trade_target.conn.addmsg(m);
                        m.cleanup();
                        //
                        m = new Message(-49);
                        m.writer().writeByte(4);
                        m.writer().writeByte(0);
                        p.conn.addmsg(m);
                        m.cleanup();
                    }
                }
                break;
            }
        }
    }

    private static boolean can_add_item_trade(Player p) {
        return (p.list_item_trade3.size() + p.list_item_trade47.size() < 4);
    }

    public static void end_trade_by_disconnect(Player p_mine, Player p_target, int type,
            String name_exit) throws IOException {
        Message m = new Message(-49);
        m.writer().writeByte(5);
        m.writer().writeByte(0);
        if (type == 1) {
            m.writer().writeUTF("Giao dịch với " + p_target.name + " hoàn tất");
        } else if (type == 0) {
            m.writer().writeUTF(p_target.name + " hủy giao dịch");
        } else if (type == 2) {
            m.writer().writeUTF("Giao dịch bị hủy bỏ vì " + name_exit
                    + " không đủ khả năng để trả phí cho giao dịch này");
        }
        p_mine.conn.addmsg(m);
        m.cleanup();
        //
        p_mine.fee_trade = 0;
        p_mine.money_trade = 0;
        p_mine.is_lock_trade = false;
        p_mine.is_accept_trade = false;
        p_mine.list_item_trade3 = null;
        p_mine.list_item_trade47 = null;
        p_mine.trade_target = null;
    }

    public static void show_table(Player p, String name) throws IOException {
        Message m = new Message(-49);
        m.writer().writeByte(0);
        m.writer().writeByte(0);
        m.writer().writeUTF(name);
        p.conn.addmsg(m);
        m.cleanup();
        p.list_item_trade3 = new ArrayList<>();
        p.list_item_trade47 = new ArrayList<>();
        p.fee_trade = 0;
        p.money_trade = 0;
        p.is_lock_trade = false;
        p.is_accept_trade = false;
    }
}
