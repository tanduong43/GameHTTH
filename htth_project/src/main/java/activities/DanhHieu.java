/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package activities;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import java.util.ArrayList;
import template.Option;

/**
 */
public class DanhHieu {
    public static ArrayList<DanhHieu> ENY = new ArrayList<>();
    public int id;
    public String Name;
    public int idicon;
    public int nframe;
    public int coint;
    public ArrayList<Option> op = new ArrayList<>();

    /**
     * Mở UI danh hiệu (Message -102 type 0) — hiện tất cả title trong bảng
     * danhhieu.
     */
    public static void show(Player p) {
        try {
            Message msg = new Message(-102);
            msg.writer().writeByte(0);
            msg.writer().writeByte(ENY.size()); // byte, không phải short
            for (DanhHieu danhHieu : ENY) {
                msg.writer().writeInt(danhHieu.id);
                msg.writer().writeUTF(danhHieu.Name);
                msg.writer().writeInt(danhHieu.idicon);
                msg.writer().writeByte(danhHieu.nframe); // byte, không phải int
                msg.writer().writeByte(p.check_id_danhhieu(danhHieu.id)
                        ? (danhHieu.id == p.id_danh_hieu_su_dung ? 2 : 1)
                        : 0);
                msg.writer().writeByte(danhHieu.op.size());
                for (Option op : danhHieu.op) {
                    msg.writer().writeByte(op.id);
                    msg.writer().writeInt(op.getParam()); // int, không phải short
                }
            }
            p.conn.addmsg(msg);
            msg.cleanup();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void process(Message m, Player p) {
        try {
            byte type = m.reader().readByte();
            int id = m.reader().readInt();
            byte action = m.reader().readByte();
            switch (type) {
                case 0: {
                    show(p);
                    break;
                }
                case 1: {
                    if (action == 0) {
                        if (!p.check_id_danhhieu(id)) {
                            Service.send_box_ThongBao_OK(p, "Không có danh hiệu");
                            return;
                        }
                        DanhHieu dh = get_Id(id);
                        if (dh == null) {
                            Service.send_box_ThongBao_OK(p, "Danh hiệu không tồn tại");
                            return;
                        }
                        Message msg = new Message(-102);
                        msg.writer().writeByte(type);
                        msg.writer().writeByte(0);
                        msg.writer().writeInt(p.id);
                        msg.writer().writeInt(dh.idicon);
                        msg.writer().writeInt(dh.nframe);
                        p.map.send_msg_all_p(msg, null, true);
                        msg.cleanup();
                        msg = new Message(-102);
                        msg.writer().writeByte(2);
                        msg.writer().writeInt(id);
                        msg.writer().writeByte(2);
                        p.conn.addmsg(msg);
                        msg.cleanup();
                        p.danhhieu = (byte) id;
                        p.id_danh_hieu_su_dung = id;
                        p.idDanhHieu = (short) id;
                        Service.send_box_ThongBao_OK(p, "Sử dụng thành công");
                    } else {
                        Message msg = new Message(-102);
                        msg.writer().writeByte(type);
                        msg.writer().writeByte(1);
                        msg.writer().writeInt(p.id);
                        p.map.send_msg_all_p(msg, null, true);
                        msg.cleanup();
                        msg = new Message(-102);
                        msg.writer().writeByte(2);
                        msg.writer().writeInt(id);
                        msg.writer().writeByte(1);
                        p.conn.addmsg(msg);
                        msg.cleanup();
                        p.danhhieu = -1;
                        p.id_danh_hieu_su_dung = -1;
                        p.idDanhHieu = -1;
                        Service.send_box_ThongBao_OK(p, "Tháo thành công");
                    }
                    break;
                }
                case 3: {
                    byte zoomlv = (p.conn != null && p.conn.zoomlv > 0) ? p.conn.zoomlv : 4;
                    byte[] data = Util.loadfile("data/danhhieu/x" + zoomlv + "/" + id + ".png");
                    if (data == null) {
                        data = Util.loadfile("data/danhhieu/x4/" + id + ".png");
                    }
                    if (data != null) {
                        Message msg = new Message(-102);
                        msg.writer().writeByte(3);
                        msg.writer().writeShort(id);
                        msg.writer().write(data);
                        p.conn.addmsg(msg);
                        msg.cleanup();
                    } else {
                        System.err.println("Ko tim thay anh " + id);
                    }
                    break;
                }
            }
        } catch (Exception e) {
            // e.printStackTrace();
        }
    }

    public static DanhHieu get_Id(int id) {
        for (DanhHieu dh : ENY) {
            if (dh.id == id) {
                return dh;
            }
        }
        return null;
    }

}
