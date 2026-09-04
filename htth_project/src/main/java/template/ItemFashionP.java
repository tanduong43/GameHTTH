package template;

import java.io.IOException;

import client.Player;
import io.Message;
/**
 *
 * @author Truongbk
 */
public class ItemFashionP {
    public byte category;
    public short id;
    public short icon;
    public boolean is_use;

    public static void show_table(Player p, int type) throws IOException {
        switch (type) {
            case 103: {
                Message m = new Message(-19);
                m.writer().writeByte(103);
                m.writer().writeUTF("Tiệm tóc");
                m.writer().writeByte(103);
                m.writer().writeShort(ItemHair.get_size_type(103));
                for (int i = 0; i < ItemHair.ENTRYS.size(); i++) {
                    ItemHair temp = ItemHair.ENTRYS.get(i);
                    if (temp.type == 103) {
                        m.writer().writeByte(temp.ID);
                        m.writer().writeUTF(temp.name);
                        m.writer().writeByte(0);
                        m.writer().writeShort(temp.idIcon);
                        m.writer().writeShort(0);
                        m.writer().writeInt(0);
                        if (p.check_itfashionP(temp.ID, 103) != null) {
                            m.writer().writeShort(0);
                        } else {
                            m.writer().writeShort(500);
                        }
                    }
                }
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 102: {
                Message m = new Message(-19);
                m.writer().writeByte(102);
                m.writer().writeUTF("Đóng thuyền");
                m.writer().writeByte(102);
                m.writer().writeShort(ItemBoat.ENTRYS.size());
                for (int i = 0; i < ItemBoat.ENTRYS.size(); i++) {
                    m.writer().writeByte(ItemBoat.ENTRYS.get(i).id);
                    m.writer().writeUTF(ItemBoat.ENTRYS.get(i).name);
                    m.writer().writeByte(ItemBoat.ENTRYS.get(i).type);
                    m.writer().writeShort(ItemBoat.ENTRYS.get(i).idimg);
                    m.writer().writeShort(ItemBoat.ENTRYS.get(i).icon);
                    //
                    m.writer().writeInt(0);
                    ItemBoatP my_boat = p.check_itboat(ItemBoat.ENTRYS.get(i).id);
                    m.writer().writeShort(my_boat == null ? 5 : 0);
                }
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 105: {
                p.check_expiry_fashion(true);
                int ver_ = p.conn.getVersionInt();
                Message m = new Message(-19);
                m.writer().writeByte(105);
                m.writer().writeUTF("Thời trang");
                m.writer().writeByte(105);
                m.writer().writeShort(ItemFashion.ENTRYS.size());
                for (int i = 0; i < ItemFashion.ENTRYS.size(); i++) {
                    m.writer().writeByte(ItemFashion.ENTRYS.get(i).ID);
                    m.writer().writeUTF(ItemFashion.ENTRYS.get(i).name);
                    ItemFashionP2 myFashion = p.check_fashion(ItemFashion.ENTRYS.get(i).ID);
                    String info = ItemFashion.ENTRYS.get(i).info;
                    if (ItemFashion.ENTRYS.get(i).ID == 238 || (byte) ItemFashion.ENTRYS.get(i).ID == (byte) 238) {
                        info += "\n+10% Chí mạng khi kết hợp Trái Nikyu Nikyu";
                        if (p.has_devil_fruit_nikyu() && myFashion != null && myFashion.is_use) {
                            info += "\n(Đã kích hoạt: +10% Chí mạng)";
                        }
                    }
                    if (myFashion != null && myFashion.level > 0) {
                        info += "\n (+" + myFashion.level + ") Toàn bộ chỉ số được + "
                                + Upgrade_Skin_Info.get_op_level(myFashion.level) + "%";
                    }
                    if (myFashion != null && myFashion.expiryTime != -1) {
                        long remaining = myFashion.expiryTime - System.currentTimeMillis();
                        if (remaining > 0) {
                            long days = remaining / (24 * 60 * 60 * 1000L);
                            long hours = (remaining % (24 * 60 * 60 * 1000L)) / (60 * 60 * 1000L);
                            long mins = (remaining % (60 * 60 * 1000L)) / (60 * 1000L);
                            java.text.SimpleDateFormat sdf = new java.text.SimpleDateFormat("HH:mm dd/MM/yyyy");
                            info += "\n[HSD: " + sdf.format(new java.util.Date(myFashion.expiryTime)) + "]";
                            if (days > 0) {
                                info += "\n(Còn " + days + " ngày " + hours + " giờ)";
                            } else if (hours > 0) {
                                info += "\n(Còn " + hours + " giờ " + mins + " phút)";
                            } else {
                                info += "\n(Còn " + mins + " phút)";
                            }
                        }
                    }
                    m.writer().writeUTF(info);
                    m.writer().writeShort(ItemFashion.ENTRYS.get(i).idIcon);
                    m.writer().writeByte(ItemFashion.ENTRYS.get(i).mWearing.length);
                    for (int j = 0; j < ItemFashion.ENTRYS.get(i).mWearing.length; j++) {
                        m.writer().writeShort(ItemFashion.ENTRYS.get(i).mWearing[j]);
                    }
                    if (myFashion == null && ItemFashion.ENTRYS.get(i).price == -1) { // not sale
                        m.writer().writeInt(-1);
                        m.writer().writeShort(0);
                    } else {
                        m.writer().writeInt(0);
                        if (myFashion != null) {
                            m.writer().writeShort(0);
                        } else {
                            m.writer().writeShort(ItemFashion.ENTRYS.get(i).price);
                        }
                    }
                    if (ver_ >= 115) {
                        if (myFashion != null) {
                            m.writer().writeByte(myFashion.level);
                        } else {
                            m.writer().writeByte(0);
                        }
                    }
                }
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 108: {
                Message m = new Message(-19);
                m.writer().writeByte(112);
                m.writer().writeUTF("Thẩm mỹ viện");
                m.writer().writeByte(108);
                m.writer().writeShort(ItemHair.get_size_type(108));
                for (int i = 0; i < ItemHair.ENTRYS.size(); i++) {
                    ItemHair temp = ItemHair.ENTRYS.get(i);
                    if (temp.type == 108) {
                        m.writer().writeByte(temp.ID);
                        m.writer().writeUTF(temp.name);
                        m.writer().writeByte(0);
                        m.writer().writeShort(temp.idIcon);
                        m.writer().writeShort(0);
                        m.writer().writeInt(0);
                        if (p.check_itfashionP(temp.ID, 108) != null) {
                            m.writer().writeShort(0);
                        } else {
                            m.writer().writeShort(500);
                        }
                    }
                }
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
        }
    }
}
