package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import template.GiftBox;
import template.ItemFashion;
import template.ItemFashionP;
import template.ItemTemplate4;
import template.ItemTemplate7;
import java.io.IOException;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import template.ItemFashionP2;

public class ListTichNap {

    public final static List<ListTichNap> ENTRY;

    static {
        ENTRY = new ArrayList<>();
        ListTichNap t = new ListTichNap();
        t.num = 100_000;
        t.cat = new byte[]{4, 4, 4, 4};
        t.id = new short[]{0, 158, 222, 366};
        t.quant = new short[]{10, 5, 50, 10};
        ENTRY.add(t);
        //
        t = new ListTichNap();
        t.num = 200_000;
        t.cat = new byte[]{4, 4, 4, 4, 4};
        t.id = new short[]{0, 158, 222, 364, 464};
        t.quant = new short[]{60, 10, 100, 20, 1};
        ENTRY.add(t);
        //
        t = new ListTichNap();
        t.num = 500_000;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4};
        t.id = new short[]{0, 158, 222, 364, -10, 339};
        // -10 ruong cam theo lv
        t.quant = new short[]{150, 20, 30, 30, 20, 10};
        ENTRY.add(t);
        //
        t = new ListTichNap();
        t.num = 1_000_000;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4, 4};
        t.id = new short[]{0, 158, 226, 323, -10, 464, 455};
        // -10 ruong cam theo lv
        t.quant = new short[]{300, 40, 100, 5, 50, 1, 5};
        ENTRY.add(t);
        //
        t = new ListTichNap();
        t.num = 2_000_000;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4};
        t.id = new short[]{0,  226, 323, -10, 54, 455};
        // -10 ruong cam theo lv
        t.quant = new short[]{600, 100, 10, 100, 2, 20};
        ENTRY.add(t);
        //
        t = new ListTichNap();
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
  int tongnap = getTongNap(p);
    Message m = new Message(-90);
    m.writer().writeByte(0);
    m.writer().writeInt(tongnap);
    m.writer().writeByte(ENTRY.size());
    for (int i = 0; i < ENTRY.size(); i++) {
            ListTichNap t = ENTRY.get(i);
            m.writer().writeByte(i);
            m.writer().writeInt(t.num);
            m.writer().writeByte(p.tichTieuCheck[i] == 1 ? 2 : (tongnap >= t.num ? 1 : 0));
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
                } else if (t.cat[j] == 105) {
                    ItemFashion itemFashion = ItemFashion.get_item(t.id[j]);
                    m.writer().writeUTF(itemFashion.name);
                    m.writer().writeByte(t.cat[j]);
                    m.writer().writeShort(itemFashion.idIcon);
                    m.writer().writeShort(t.quant[j]);
                    m.writer().writeByte(0);
                } else if (t.cat[j] == 7) {
                    ItemTemplate7 itTemp7Select = ItemTemplate7.get_it_by_id(t.id[j]);
                    m.writer().writeUTF(itTemp7Select.name);
                    m.writer().writeByte(t.cat[j]);
                    m.writer().writeShort(itTemp7Select.icon);
                    m.writer().writeShort(t.quant[j]);
                    m.writer().writeByte(0);
                } else {
                    return;
                }
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

  private static int getTongNap(Player p) {
      return p.conn.tongnap;
  }

    public static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        byte id = m2.reader().readByte();
        // System.out.println(type + " " + id);
        if (type == 1 && id < p.tichTieuCheck.length && id < ENTRY.size() && p.tichTieuCheck[id] != 1) {
            List<GiftBox> list = new ArrayList<>();
            ListTichNap listGet = ENTRY.get(id);
            for (int i = 0; i < listGet.cat.length; i++) {
                if (listGet.cat[i] == 4) {
                    ItemTemplate4 itemTemplate4 = ItemTemplate4.get_it_by_id(listGet.id[i]);
                    if (listGet.id[i] == -10) {
                        int level = p.level / 10;
                        if (level == 0) {
                            level = 1;
                        }
                        itemTemplate4 = ItemTemplate4.get_it_by_id(level + 121);
                    }
                    if (itemTemplate4 != null) {
                        GiftBox gb4 = new GiftBox();
                        gb4.id = itemTemplate4.id;
                        gb4.type = 4;
                        gb4.name = itemTemplate4.name;
                        gb4.icon = itemTemplate4.icon;
                        gb4.num = listGet.quant[i];
                        if (gb4.id == 0) {
                            gb4.num *= 1_000_000;
                        }
                        gb4.color = 0;
                        list.add(gb4);
                    }
                } else if (listGet.cat[i] == 7) {
                    ItemTemplate7 itemTemplate7 = ItemTemplate7.get_it_by_id(listGet.id[i]);
                    if (itemTemplate7 != null) {
                        GiftBox gb4 = new GiftBox();
                        gb4.id = itemTemplate7.id;
                        gb4.type = 7;
                        gb4.name = itemTemplate7.name;
                        gb4.icon = itemTemplate7.icon;
                        gb4.num = listGet.quant[i];
                        gb4.color = 0;
                        list.add(gb4);
                    }
                } else if (listGet.cat[i] == 105) {
                    ItemFashion itf = ItemFashion.get_item(listGet.id[i]);
                    GiftBox gb_beri4 = new GiftBox();
                    ItemFashionP2 temp2 = new ItemFashionP2();
                    temp2.id = itf.ID;
                    p.fashion.add(temp2);
                    p.update_fashionP2(temp2);
                    for (int j = 0; j < p.map.players.size(); j++) {
                        Player p0 = p.map.players.get(j);
                        Service.charWearing(p, p0, false);
                    }
                    Service.UpdateInfoMaincharInfo(p);
                    gb_beri4.id = itf.ID;
                    gb_beri4.type = 105;
                    gb_beri4.name = itf.name;
                    gb_beri4.icon = itf.idIcon;
                    gb_beri4.num = 1;
                    gb_beri4.color = 0;
                    list.add(gb_beri4);
                } else {
                    Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, hãy thử lại sau!");
                    return;
                }
            }
            p.tichTieuCheck[id] = 1;
            if (list.size() > 0) {
                Service.send_gift(p, 1, "Phần thưởng", "Phần thưởng", list, true);
            }
            Message m = new Message(-90);
            m.writer().writeByte(2);
            m.writer().writeByte(id);
            p.conn.addmsg(m);
            m.cleanup();
        }
    }
}
