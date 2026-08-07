/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package activities;

import client.Player;
import core.Service;
import io.Message;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import template.GiftBox;
import template.ItemTemplate4;
import template.ItemTemplate7;

/**
 *
 * @author phucl
 */
public class Mission {

    public final static List<Mission> ENTRY;
    public byte[] cat;
    public short[] id;
    public short[] quant;
    public String name;
    public String info;
    public int nummax;
    public int num;

    static {
        ENTRY = new ArrayList<>();

        Mission t = new Mission();
        t.name = "Đánh boss";
        t.info = "Hạ gục Boss bất kì(không tính siêu boss)";
        t.nummax = 1;
        t.cat = new byte[]{4, 4, 4, 4};
        t.id = new short[]{0, 1, 158, 221};
        t.quant = new short[]{1, 2000, 1, 5};
        ENTRY.add(t);

        t = new Mission();
        t.name = "Tiến ra mặt trận";
        t.info = "Chiến thắng 1 trận pvp hoặc truy nã";
        t.nummax = 1;
        t.cat = new byte[]{4, 4, 4, 4};
        t.id = new short[]{0, 1, 158, 221};
        t.quant = new short[]{1, 2000, 1, 5};
        ENTRY.add(t);

        t = new Mission();
        t.name = "Đánh bóng trang bị";
        t.info = "Cường hóa thành công trang bị";
        t.nummax = 1;
        t.cat = new byte[]{4, 4, 4, 4};
        t.id = new short[]{0, 1, 158, 221};
        t.quant = new short[]{1, 2000, 1, 5};
        ENTRY.add(t);

        t = new Mission();
        t.name = "Phó bản thường";
        t.info = "Hoàn thành phó bản liên tầng hoặc vườn cam namie";
        t.nummax = 1;
        t.cat = new byte[]{4, 4, 4, 4};
        t.id = new short[]{0, 1, 158, 221};
        t.quant = new short[]{1, 2000, 1, 5};
        ENTRY.add(t);

        t = new Mission();
        t.name = "Không thể ngăn cản";
        t.info = "Chiến thắng 1 game tài xỉu";
        t.nummax = 1;
        t.cat = new byte[]{4, 4, 4, 4, 4, 4, 4};
        t.id = new short[]{0, 427, 226, 323, -10, 339, 455};
        // -10 ruong cam theo lv
        t.quant = new short[]{600, 1, 100, 10, 100, 2, 20};
        ENTRY.add(t);
    }

    public static void showTable(Player p) throws IOException {
        Message m = new Message(37);
        m.writer().writeByte(0);//b
        m.writer().writeUTF("Mission");//
        m.writer().writeByte(ENTRY.size());//b2
        for (int i = 0; i < ENTRY.size(); i++) {
            Mission t = ENTRY.get(i);
            m.writer().writeUTF(t.name);
            m.writer().writeUTF(t.info);
            //0 chua lam 1 nhan 2 da nhan/// -1 khoa
            if (i == 0) {
                m.writer().writeInt(p.num1);//ht
                m.writer().writeInt(t.nummax);//max
                m.writer().writeShort(9);//icon
                m.writer().writeByte(p.MissionCheck[i] == 1 ? 2 : (p.num1 >= t.nummax ? 1 : 0));//type 
            } else if (i == 1) {
                m.writer().writeInt(p.num2);//ht
                m.writer().writeInt(t.nummax);//max
                m.writer().writeShort(10);//icon
                m.writer().writeByte(p.MissionCheck[i] == 1 ? 2 : (p.num2 >= t.nummax ? 1 : 0));//type 
            } else if (i == 2) {
                m.writer().writeInt(p.num3);//ht
                m.writer().writeInt(t.nummax);//max

                m.writer().writeShort(4);//icon
                m.writer().writeByte(p.MissionCheck[i] == 1 ? 2 : (p.num3 >= t.nummax ? 1 : 0));//type 
            } else if (i == 3) {
                m.writer().writeInt(p.num4);//ht
                m.writer().writeInt(t.nummax);//max

                m.writer().writeShort(18);//icon
                m.writer().writeByte(p.MissionCheck[i] == 1 ? 2 : (p.num4 >= t.nummax ? 1 : 0));//type 
            } else if (i == 4) {
                m.writer().writeInt(p.num5);//ht
                m.writer().writeInt(t.nummax);//max
                m.writer().writeShort(1);//icon
                m.writer().writeByte(p.MissionCheck[i] == 1 ? 2 : (p.num5 >= t.nummax ? 1 : 0));//type 
            }

        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        byte index = m2.reader().readByte();
        if (type == 1 && index < p.MissionCheck.length && index < ENTRY.size() && p.MissionCheck[index] != 1) {
            List<GiftBox> list = new ArrayList<>();
            Mission listGet = ENTRY.get(index);
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
                } else {
                    Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra, hãy thử lại sau!");
                    return;
                }
            }
            p.MissionCheck[index] = 1;
            if (list.size() > 0) {
                Service.send_gift(p, 1, "Phần thưởng", "Phần thưởng", list, true);
            }
        }
        Message m = new Message(37);
        m.writer().writeByte(1);//b
        m.writer().writeByte(index);//b3
        m.writer().writeByte(2);//type 
        p.conn.addmsg(m);
        m.cleanup();

    }
}
