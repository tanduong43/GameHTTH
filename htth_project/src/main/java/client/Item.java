package client;

import java.io.DataOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import org.json.simple.JSONArray;
import org.json.simple.JSONValue;
import io.Message;
import template.*;
/**
 *
 * @author Truongbk
 */
public class Item {
    public final byte max_bag = 127;
    public final byte max_box = 48;
    private final Player p;
    public Item_wear[] bag3;
    public Item_wear[] box3;
    public Item_wear[] it_body;
    public List<ItemBag47> bag47;
    public List<ItemBag47> box47;
    public Item_wear it_heart;
    public List<Item_wear> save_item_wear;
    public List<ItemBag47> save_item_47;

    public Item(Player p) {
        this.p = p;
    }

    public void send_maxbag_Inventory() throws IOException {
        Message m = new Message(-12);
        m.writer().writeByte(6);
        m.writer().writeByte(3);
        m.writer().writeShort(max_bag); // max bag
        p.conn.addmsg(m);
        m.cleanup();
    }

    public void send_maxbox_Inventory() throws IOException {
        Message m = new Message(-32);
        m.writer().writeByte(6);
        m.writer().writeByte(3);
        m.writer().writeShort(max_box); // max box
        p.conn.addmsg(m);
        m.cleanup();
    }

    public void update_Inventory(int type, boolean b) throws IOException {
        update_bag(4, b);
        update_bag(7, b);
        update_bag(3, b);
        update_bag(5, b);
    }

    private void update_bag(int type, boolean b) throws IOException {
        Message m = new Message(-12);
        m.writer().writeByte(0);
        m.writer().writeByte(type);
        switch (type) {
            case 3: {
                m.writer().writeByte(this.quant_item_inbag(3));
                for (int i = 0; i < bag3.length; i++) {
                    if (bag3[i] != null) {
                        Item.readUpdateItem(m.writer(), bag3[i], p);
                    }
                }
                break;
            }
            case 4:
            case 8: {
                m.writer().writeByte(this.quant_item_inbag(4));
                for (int i = 0; i < bag47.size(); i++) {
                    if (bag47.get(i).category == 4) {
                        m.writer().writeShort(bag47.get(i).id);
                        m.writer().writeShort(bag47.get(i).quant);
                    }
                }
                break;
            }
            case 5: {
                m.writer().writeByte(this.quant_item_inbag(5));
                for (int i = 0; i < bag47.size(); i++) {
                    if (bag47.get(i).category == 5) {
                        m.writer().writeShort(bag47.get(i).id);
                        m.writer().writeUTF(DataTemplate.NamePotionquest[bag47.get(i).id]);
                        m.writer().writeShort(bag47.get(i).quant);
                    }
                }
                break;
            }
            case 7: {
                m.writer().writeByte(this.quant_item_inbag(7));
                for (int i = 0; i < bag47.size(); i++) {
                    if (bag47.get(i).category == 7) {
                        m.writer().writeByte(bag47.get(i).id);
                        m.writer().writeShort(bag47.get(i).quant);
                    }
                }
                break;
            }
        }
        if (b) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public void update_Inventory_box(int type, boolean b) throws IOException {
        if (type == -1) {
            update_box(4, b);
            update_box(7, b);
            update_box(3, b);
        } else {
            update_box(type, b);
        }
    }

    private void update_box(int type, boolean b) throws IOException {
        Message m = new Message(-32);
        m.writer().writeByte(0);
        m.writer().writeByte(type);
        switch (type) {
            case 3: {
                m.writer().writeByte(this.quant_item_inbox(3));
                for (int i = 0; i < box3.length; i++) {
                    if (box3[i] != null) {
                        Item.readUpdateItem(m.writer(), box3[i], p);
                    }
                }
                break;
            }
            case 4:
            case 8: {
                m.writer().writeByte(this.quant_item_inbox(4));
                for (int i = 0; i < box47.size(); i++) {
                    if (box47.get(i).category == 4) {
                        m.writer().writeShort(box47.get(i).id);
                        m.writer().writeShort(box47.get(i).quant);
                    }
                }
                break;
            }
            case 5: {
                m.writer().writeByte(0);
                break;
            }
            case 7: {
                m.writer().writeByte(this.quant_item_inbox(7));
                for (int i = 0; i < box47.size(); i++) {
                    if (box47.get(i).category == 7) {
                        m.writer().writeByte(box47.get(i).id);
                        m.writer().writeShort(box47.get(i).quant);
                    }
                }
                break;
            }
        }
        if (b) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public void update_assets_Inventory(boolean b) throws IOException {
        Message m = new Message(-12);
        m.writer().writeByte(3);
        m.writer().writeByte(6);
        //
        m.writer().writeLong(p.get_vang());
        m.writer().writeInt(p.get_ngoc());
        m.writer().writeShort(p.get_ticket()); // ticket
        m.writer().writeShort(p.get_ticket_max()); // max ticket
        m.writer().writeByte((byte) p.get_pvp_ticket());
        m.writer().writeByte(p.get_pvp_ticket_max()); // max pvp ticket
        m.writer().writeByte((byte) p.get_key_boss());
        m.writer().writeByte(p.get_key_boss_max()); // max key boss
        m.writer().writeInt(p.get_vnd()); // vnd
        m.writer().writeInt(p.get_bua()); // bua
        m.writer().writeInt(p.getTichLuy() / 1000); // diem nap
        m.writer().writeInt(p.conn.coin); // coin
        if (b) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public void update_assets_Box(boolean b) throws IOException {
        Message m = new Message(-32);
        m.writer().writeByte(3);
        m.writer().writeByte(6);
        //
        m.writer().writeLong(p.get_vang());
        m.writer().writeInt(p.get_ngoc());
        m.writer().writeInt(p.get_vnd()); // vnd
        m.writer().writeInt(0); // bua
        m.writer().writeInt(p.get_bua()); // diem nap
        m.writer().writeInt(p.conn.coin); // coin
        if (b) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void readUpdateItem(DataOutputStream dos, Item_wear it, Player p)
            throws IOException {
        dos.writeShort(it.index);
        dos.writeUTF(it.template.name);
        dos.writeByte(it.template.clazz);
        dos.writeByte(it.template.typeEquip);
        dos.writeShort(it.template.icon);
        dos.writeShort(it.template.level);
        dos.writeByte(it.levelup);
        dos.writeByte(it.template.color);
        dos.writeByte(0);
        dos.writeByte(it.typelock);
        dos.writeByte(it.numHoleDaDuc);
        dos.writeInt(it.timeUse);
        dos.writeShort(it.valueChetac);
        dos.writeByte(it.isHoanMy);
        dos.writeByte(it.valueKichAn);
        //
        if (it.template.typeEquip < 6 && it.levelup > 10) {
            dos.writeByte(it.option_item.size() + 1);
        } else {
            dos.writeByte(it.option_item.size());
        }
        for (int i = 0; i < it.option_item.size(); i++) {
            dos.writeByte(it.option_item.get(i).id);
            dos.writeShort(
                    it.option_item.get(i).getParam(it.template.typeEquip, it.levelup, it.isHoanMy));
        }
        if (it.template.typeEquip < 6 && it.levelup > 10) {
            switch (it.template.typeEquip) {
                case 0: {
                    dos.writeByte(46);
                    dos.writeShort((50 * (it.levelup - 10)));
                    break;
                }
                case 2: {
                    dos.writeByte(53);
                    dos.writeShort((30 * (it.levelup - 10)));
                    break;
                }
                case 1:
                case 3:
                case 5: {
                    dos.writeByte(56);
                    dos.writeShort((100 * (it.levelup - 10)));
                    break;
                }
                default: { // 4
                    dos.writeByte(47);
                    dos.writeShort((20 * (it.levelup - 10)));
                    break;
                }
            }
        }
        dos.writeByte(it.option_item_2.size());
        for (int i = 0; i < it.option_item_2.size(); i++) {
            dos.writeByte(it.option_item_2.get(i).id);
            dos.writeShort(it.option_item_2.get(i).getParam());
        }
        dos.writeByte(it.numLoKham);
        dos.writeByte(it.mdakham.length);
        for (int i = 0; i < it.mdakham.length; i++) {
            dos.writeShort(it.mdakham[i]);
        }
    }

    public static void readUpdateItem(String jsdata, Item_wear it) {
        JSONArray js = (JSONArray) JSONValue.parse(jsdata);
        it.template = ItemTemplate3.get_it_by_id(Short.parseShort(js.get(0).toString()));
        it.levelup = Byte.parseByte(js.get(1).toString());
        it.typelock = Byte.parseByte(js.get(2).toString());
        it.numHoleDaDuc = Byte.parseByte(js.get(3).toString());
        it.timeUse = Integer.parseInt(js.get(4).toString());
        it.valueChetac = Short.parseShort(js.get(5).toString());
        it.isHoanMy = Byte.parseByte(js.get(6).toString());
        it.valueKichAn = Byte.parseByte(js.get(7).toString());
        it.option_item = new ArrayList<>();
        JSONArray js2 = (JSONArray) JSONValue.parse(js.get(8).toString());
        for (int i = 0; i < js2.size(); i++) {
            JSONArray js_3 = (JSONArray) JSONValue.parse(js2.get(i).toString());
            int a = Byte.parseByte(js_3.get(0).toString());
            if (it.template.typeEquip < 6 && (a == 46 || a == 53 || a == 56 || a == 47)) {
                continue;
            }
            byte id = Byte.parseByte(js_3.get(0).toString());
            int value = Short.parseShort(js_3.get(1).toString());
            it.option_item.add(new Option(id, value));
        }
        it.option_item_2 = new ArrayList<>();
        JSONArray js4 = (JSONArray) JSONValue.parse(js.get(9).toString());
        for (int i = 0; i < js4.size(); i++) {
            JSONArray js_3 = (JSONArray) JSONValue.parse(js4.get(i).toString());
            it.option_item_2.add(new Option(Byte.parseByte(js_3.get(0).toString()),
                    Short.parseShort(js_3.get(1).toString())));
        }
        it.numLoKham = Byte.parseByte(js.get(10).toString());
        JSONArray js5 = (JSONArray) JSONValue.parse(js.get(11).toString());
        it.mdakham = new short[js5.size()];
        for (int i = 0; i < it.mdakham.length; i++) {
            it.mdakham[i] = Short.parseShort(js5.get(i).toString());
        }
        it.index = Byte.parseByte(js.get(12).toString());
    }

    public boolean add_item_bag3(Item_wear it_add) {
        if (able_bag() > 0) {
            for (int i = 0; i < bag3.length; i++) {
                if (bag3[i] == null) {
                    bag3[i] = it_add;
                    it_add.index = (byte) i;
                    return true;
                }
            }
        }
        //
        if (it_add != null) {
            save_item_wear.add(it_add);
        }
        //
        return false;
    }

    public boolean add_item_box3(Item_wear it_add) {
        if (able_box() > 0) {
            for (int i = 0; i < box3.length; i++) {
                if (box3[i] == null) {
                    box3[i] = it_add;
                    it_add.index = (byte) i;
                    return true;
                }
            }
        }
        return false;
    }

    public int able_bag() {
        return this.max_bag - this.quant_item_inbag(3) - this.quant_item_inbag(4)
                - this.quant_item_inbag(5) - this.quant_item_inbag(7);
    }

    public int able_box() {
        return this.max_box - this.quant_item_inbox(3) - this.quant_item_inbox(4)
                - this.quant_item_inbox(7);
    }

    private int quant_item_inbag(int type) {
        int par = 0;
        switch (type) {
            case 3: {
                for (int i = 0; i < bag3.length; i++) {
                    if (bag3[i] != null) {
                        par++;
                    }
                }
            }
            case 4:
            case 5:
            case 7: {
                for (int i = 0; i < bag47.size(); i++) {
                    if (bag47.get(i).category == type) {
                        par++;
                    }
                }
                break;
            }
        }
        return par;
    }

    private int quant_item_inbox(int type) {
        int par = 0;
        switch (type) {
            case 3: {
                for (int i = 0; i < box3.length; i++) {
                    if (box3[i] != null) {
                        par++;
                    }
                }
            }
            case 4:
            case 7: {
                for (int i = 0; i < box47.size(); i++) {
                    if (box47.get(i).category == type) {
                        par++;
                    }
                }
                break;
            }
        }
        return par;
    }

    @SuppressWarnings("unchecked")
    public static JSONArray it_data_to_json(Item_wear it) {
        JSONArray js = new JSONArray();
        js.add(it.template.id);
        js.add(it.levelup);
        js.add(it.typelock);
        js.add(it.numHoleDaDuc);
        js.add(it.timeUse);
        js.add(it.valueChetac);
        js.add(it.isHoanMy);
        js.add(it.valueKichAn);
        JSONArray js_2 = new JSONArray();
        for (int i = 0; i < it.option_item.size(); i++) {
            JSONArray js_3 = new JSONArray();
            js_3.add(it.option_item.get(i).id);
            js_3.add(it.option_item.get(i).getParam());
            js_2.add(js_3);
        }
        js.add(js_2);
        JSONArray js_4 = new JSONArray();
        for (int i = 0; i < it.option_item_2.size(); i++) {
            JSONArray js_3 = new JSONArray();
            js_3.add(it.option_item_2.get(i).id);
            js_3.add(it.option_item_2.get(i).getParam());
            js_4.add(js_3);
        }
        js.add(js_4);
        js.add(it.numLoKham);
        JSONArray js_5 = new JSONArray();
        for (int i = 0; i < it.mdakham.length; i++) {
            js_5.add(it.mdakham[i]);
        }
        js.add(js_5);
        js.add(it.index);
        return js;
    }

    public boolean add_item_bag47(int type, int id, int num) {
        if ((total_item_bag_by_id(type, id) + num) > DataTemplate.MAX_ITEM_IN_BAG) {
            if ((type == 4 && id > 28) || (type == 7)) {
                ItemBag47 it_select = new ItemBag47();
                it_select.category = (byte) type;
                it_select.id = (short) id;
                it_select.quant = (short) num;
                save_item_47.add(it_select);
                if (save_item_47.size() > 90) {
                    save_item_47.remove(0);
                }
            }
            return false;
        }
        ItemBag47 it_select = null;
        for (int i = 0; i < bag47.size(); i++) {
            if (bag47.get(i).category == type && bag47.get(i).id == id) {
                it_select = bag47.get(i);
                break;
            }
        }
        if (it_select != null) {
            if ((it_select.quant + num) <= DataTemplate.MAX_ITEM_IN_BAG) {
                it_select.quant += num;
                return true;
            }
        } else {
            it_select = new ItemBag47();
            it_select.category = (byte) type;
            it_select.id = (short) id;
            it_select.quant = (short) num;
            this.bag47.add(it_select);
            return true;
        }
        return false;
    }

    public boolean add_item_box47(int type, int id, int num) {
        if (num > DataTemplate.MAX_ITEM_IN_BAG) {
            return false;
        }
        ItemBag47 it_select = null;
        for (int i = 0; i < box47.size(); i++) {
            if (box47.get(i).category == type && box47.get(i).id == id) {
                it_select = box47.get(i);
                break;
            }
        }
        if (it_select != null) {
            if ((it_select.quant + num) <= DataTemplate.MAX_ITEM_IN_BAG) {
                it_select.quant += num;
                return true;
            }
        } else {
            it_select = new ItemBag47();
            it_select.category = (byte) type;
            it_select.id = (short) id;
            it_select.quant = (short) num;
            this.box47.add(it_select);
            return true;
        }
        return false;
    }

    public int total_item_bag_by_id(int type, int id) {
        int par = 0;
        switch (type) {
            case 4:
            case 5:
            case 7: {
                for (int i = 0; i < bag47.size(); i++) {
                    if (bag47.get(i).category == type && bag47.get(i).id == id) {
                        par += bag47.get(i).quant;
                    }
                }
                break;
            }
        }
        return par;
    }

    public int total_item_box_by_id(int type, int id) {
        int par = 0;
        switch (type) {
            case 4:
            case 7: {
                for (int i = 0; i < box47.size(); i++) {
                    if (box47.get(i).category == type && box47.get(i).id == id) {
                        par += box47.get(i).quant;
                    }
                }
                break;
            }
        }
        return par;
    }

    public void remove_item47(int type, int id, int num) {
        ItemBag47 it_select = null;
        for (int i = 0; i < bag47.size(); i++) {
            if (bag47.get(i).category == type && bag47.get(i).id == id) {
                it_select = bag47.get(i);
                break;
            }
        }
        if (it_select != null) {
            it_select.quant -= num;
            if (it_select.quant <= 0) {
                bag47.remove(it_select);
            }
        }
    }

    public void remove_item47_box(int type, int id, int num) {
        ItemBag47 it_select = null;
        for (int i = 0; i < box47.size(); i++) {
            if (box47.get(i).category == type && box47.get(i).id == id) {
                it_select = box47.get(i);
                break;
            }
        }
        if (it_select != null) {
            it_select.quant -= num;
            if (it_select.quant <= 0) {
                box47.remove(it_select);
            }
        }
    }

    public void remove_item_wear(Item_wear item_wear) {
        for (int i = 0; i < bag3.length; i++) {
            if (bag3[i] != null && bag3[i].equals(item_wear)) {
                bag3[i] = null;
                break;
            }
        }
    }

    public void add_item_save(Item_wear item_wear) {
        if (item_wear != null) {
            save_item_wear.add(item_wear);
            if (save_item_wear.size() > 90) {
                save_item_wear.remove(0);
            }
        }
    }
}
