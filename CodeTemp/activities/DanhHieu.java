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
    public byte nframe;
    public int coint;
    public ArrayList<Option> op = new ArrayList<>();
    
    
    public static void process(Message m, Player p) {
        try {
            byte type = m.reader().readByte();
            int id = m.reader().readInt();
            byte action = m.reader().readByte();
            switch (type) {
                case 0:{
                    Message msg = new Message(-102);
                    msg.writer().writeByte(type);
                    msg.writer().writeByte(ENY.size());
                    for (DanhHieu danhHieu : ENY) {
                        msg.writer().writeInt(danhHieu.id);
                        msg.writer().writeUTF(danhHieu.Name);
                        msg.writer().writeInt(danhHieu.idicon);
                        msg.writer().writeByte(danhHieu.nframe);
                        msg.writer().writeByte(p.check_id_danhhieu(danhHieu.id) ? (danhHieu.id == p.id_danh_hieu_su_dung ? 2:1):0);
                        msg.writer().writeByte(danhHieu.op.size());
                        for (Option op : danhHieu.op) {
                            msg.writer().writeByte(op.id);
                            msg.writer().writeInt(op.getParam());
                        }
                    }
                    p.conn.addmsg(msg);
                    msg.cleanup();
                    break;
                }
                case 1:{
                    System.err.println("id danh hieu"+id);
                    if(action ==  0){
                        if(!p.check_id_danhhieu(id)){
                             Service.send_box_ThongBao_OK(p, "Không có danh hiệu");
                            return;
                        }
                        Message msg = new Message(-102);
                        msg.writer().writeByte(type);
                        msg.writer().writeByte(0);
                        msg.writer().writeInt(p.id);
                        msg.writer().writeInt(get_Id(id).idicon);//sao lai la id code ;\lai r
                        msg.writer().writeInt(get_Id(id).nframe);
                        p.map.send_msg_all_p(msg, null, true);
                        msg.cleanup();
                        msg = new Message(-102);
                        msg.writer().writeByte(2);
                        msg.writer().writeInt(id);
                        msg.writer().writeByte(2);
                        p.conn.addmsg(msg);
                        msg.cleanup();
                        p.danhhieu = (byte) id;
                         Service.send_box_ThongBao_OK(p, "Sử dụng thành công");
                    }else{
                        Message msg = new Message(-102);
                        msg.writer().writeByte(type);
                        msg.writer().writeByte(1);
                        msg.writer().writeInt(p.id);
                        p.map.send_msg_all_p(msg,null,true);
                        msg.cleanup();
                        msg = new Message(-102);
                        msg.writer().writeByte(2);
                        msg.writer().writeInt(id);
                        msg.writer().writeByte(1);
                        p.conn.addmsg(msg);
                        msg.cleanup();
                        Service.send_box_ThongBao_OK(p, "Tháo thành công");
                    }
                    break;
                }
                case 3:{
                    byte[]data = Util.loadfile("data/danhhieu/x"+p.conn.zoomlv+"/"+id+".png");
//                    System.err.println("dh image"+id +""+icon.length);
                    if(data != null){
                        Message msg = new Message(-102);
                            msg.writer().writeByte(3);
                            msg.writer().writeShort(id);// short
                            msg.writer().write(data);// send data
                            p.conn.addmsg(msg);
                            // System.err.println(msg.getData().length);
                            msg.cleanup();
                    }else{
                        System.err.println("Ko tim thay anh "+id);
                    }
                    break;
                }
            }
        } catch (Exception e) {
            // e.printStackTrace();
        }
    }
    public static DanhHieu get_Id(int id){
        for(DanhHieu dh:ENY){
            if(dh.id == id){
                return dh;
            }
        }
        return null;
    }
    
}
