/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import template.ItemBag47;
import template.ItemTemplate4;
import template.ItemTemplate7;

/**
 *
 * @author phucl
 */
public class Vong_quay_oc_sen {

//    public static short[] ID_ITEM = new short[]{174, 173, 221, 222, 223, 7, 159, 133, 174, 173, 221, 222, 223, 7, 159, 133, //
//        112, 224, 29, 225, 48, 158};
        public static short[] ID_ITEM = new short[]{1,2,3,4,5,6,7,8,9};
    public static void show_table(Player p) throws IOException {
        Message m = new Message(77);
        m.writer().writeByte(0);
        m.writer().writeUTF("VONG QUAY OC SEN");
        p.conn.addmsg(m);
        m.cleanup();
    }

public static void process(Player p, Message m2) throws IOException {
        byte action = m2.reader().readByte();
        switch (action) {
            case 1: {
                Message m = new Message(77);
                m.writer().writeByte(1);
                m.writer().writeByte(Vong_quay_oc_sen.ID_ITEM.length);
                for (int i = 0; i < Vong_quay_oc_sen.ID_ITEM.length; i++) {
                    m.writer().writeByte(ItemTemplate4.get_it_by_id(Vong_quay_oc_sen.ID_ITEM[i]).id);
                    m.writer().writeByte(4);
                    m.writer().writeShort(ItemTemplate4.get_it_by_id(Vong_quay_oc_sen.ID_ITEM[i]).icon);
                    m.writer().writeInt(11); // num
                    m.writer().writeByte(i);
                }

                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 3: {
                Message m = new Message(77);
                m.writer().writeByte(2);
                m.writer().writeByte(Util.random(3));
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
        }
    }
}
