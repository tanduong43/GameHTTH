package client;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import core.Service;
import core.Util;
import io.Message;
import template.GiftBox;
import template.ItemTemplate4;
/**
 *
 * @author Truongbk
 */
public class Wanted_Chest {
    public static final String[] NAME = new String[] {"Rương gỗ", "Rương vàng", "Rương ma thuật",
            "Rương khổng lồ", "Rương siêu ma thuật", "Rương thần thoại"};
    public short maxTimeUse, Ruby, id;
    public long timeUse;

    public static void send_box(Player p) throws IOException {
        for (int i = 0; i < p.wanted_chest.length; i++) {
            if (p.wanted_chest[i] != null) {
                Wanted_Chest temp = p.wanted_chest[i];
                Message m = new Message(-86);
                m.writer().writeByte(0);
                m.writer().writeByte(i);
                //
                m.writer().writeShort(temp.id);
                m.writer().writeShort(500 + temp.id);
                m.writer().writeByte(109);
                m.writer().writeUTF(NAME[temp.id]);
                m.writer().writeShort(temp.maxTimeUse);
                long time_minute = temp.timeUse - System.currentTimeMillis();
                if (time_minute < 0) {
                    time_minute = 0;
                }
                time_minute /= 60_000L;
                m.writer().writeShort((short) time_minute);
                m.writer().writeShort(temp.Ruby);
                //
                p.conn.addmsg(m);
                m.cleanup();
            } else {
                Message m = new Message(-86);
                m.writer().writeByte(1);
                m.writer().writeByte(i);
                p.conn.addmsg(m);
                m.cleanup();
            }
        }
    }

    public static void process(Player p, Message m2) throws IOException {
        byte act = m2.reader().readByte();
        short id = m2.reader().readShort();
        // System.out.println(act + " " + id);
        if (act == 0) {
            for (int i = 0; i < p.wanted_chest.length; i++) {
                if (p.wanted_chest[i] != null && p.wanted_chest[i].id == id) {
                    long time = (p.wanted_chest[i].timeUse - System.currentTimeMillis());
                    if (time < 0) {
                        List<GiftBox> list_gift = new ArrayList<>();
                        int beri_receiv = Util.random(10_000, 50_000);
                        GiftBox gb_beri = new GiftBox();
                        ItemTemplate4 it_temp4 = ItemTemplate4.get_it_by_id(0);
                        if (it_temp4 != null) {
                            gb_beri.id = it_temp4.id;
                            gb_beri.type = 4;
                            gb_beri.name = it_temp4.name;
                            gb_beri.icon = it_temp4.icon;
                            gb_beri.num = beri_receiv;
                            gb_beri.color = 0;
                            list_gift.add(gb_beri);
                        }
                        if (60 > Util.random(120)) {
                            GiftBox gb_ = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(18 + (p.level / 10));
                            if (it_temp4 != null) {
                                gb_.id = it_temp4.id;
                                gb_.type = 4;
                                gb_.name = it_temp4.name;
                                gb_.icon = it_temp4.icon;
                                gb_.num = Util.random(1, 3);
                                gb_.color = 0;
                                list_gift.add(gb_);
                            }
                        }
                        if (p.level >= 10 && 40 > Util.random(120)) {
                            GiftBox gb_ = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(111 + (p.level / 10));
                            if (it_temp4 != null) {
                                gb_.id = it_temp4.id;
                                gb_.type = 4;
                                gb_.name = it_temp4.name;
                                gb_.icon = it_temp4.icon;
                                gb_.num = Util.random(1, 3);
                                gb_.color = 0;
                                list_gift.add(gb_);
                            }
                        }
                        if (p.level >= 10 && 30 > Util.random(120)) {
                            GiftBox gb_ = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(121 + (p.level / 10));
                            if (it_temp4 != null) {
                                gb_.id = it_temp4.id;
                                gb_.type = 4;
                                gb_.name = it_temp4.name;
                                gb_.icon = it_temp4.icon;
                                gb_.num = Util.random(1, 3);
                                gb_.color = 0;
                                list_gift.add(gb_);
                            }
                        }
                        if (p.level >= 10 && 20 > Util.random(120)) {
                            GiftBox gb_ = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(29);
                            if (it_temp4 != null) {
                                gb_.id = it_temp4.id;
                                gb_.type = 4;
                                gb_.name = it_temp4.name;
                                gb_.icon = it_temp4.icon;
                                gb_.num = Util.random(1, 3);
                                gb_.color = 0;
                                list_gift.add(gb_);
                            }
                        }
                        if (p.level >= 10 && 5 > Util.random(120)) {
                            GiftBox gb_ = new GiftBox();
                            it_temp4 = ItemTemplate4.get_it_by_id(158);
                            if (it_temp4 != null) {
                                gb_.id = it_temp4.id;
                                gb_.type = 4;
                                gb_.name = it_temp4.name;
                                gb_.icon = it_temp4.icon;
                                gb_.num = Util.random(1, 3);
                                gb_.color = 0;
                                list_gift.add(gb_);
                            }
                        }
                        Service.send_gift(p, 1, "Rương truy nã", "Phần thưởng", list_gift, true);
                        //
                        p.wanted_chest[i] = null;
                        Wanted_Chest.send_box(p);
                    } else {
                        Service.send_box_ThongBao_OK(p,
                                "Mở sau " + Util.get_time_str_by_sec2(time) + " nữa");
                    }
                    break;
                }
            }
        }
    }

    public static void receiv_ruong(Player p) throws IOException {
        for (int i = 0; i < p.wanted_chest.length; i++) {
            if (p.wanted_chest[i] == null) {
                p.wanted_chest[i] = new Wanted_Chest();
                if (p.get_wanted_point() >= 10_000_000) {
                    p.wanted_chest[i].id = (short) Util.random(6);
                } else if (p.get_wanted_point() >= 5_000_000 && p.get_wanted_point() < 10_000_000) {
                    p.wanted_chest[i].id = (short) Util.random(5);
                } else if (p.get_wanted_point() >= 1_500_000 && p.get_wanted_point() < 5_000_000) {
                    p.wanted_chest[i].id = (short) Util.random(4);
                } else if (p.get_wanted_point() >= 750_000 && p.get_wanted_point() < 1_500_000) {
                    p.wanted_chest[i].id = (short) Util.random(3);
                } else if (p.get_wanted_point() >= 1 && p.get_wanted_point() < 750_000) {
                    p.wanted_chest[i].id = (short) Util.random(2);
                } else {
                    p.wanted_chest[i].id = 0;
                }
                switch (p.wanted_chest[i].id) {
                    case 0: {
                        p.wanted_chest[i].timeUse = System.currentTimeMillis() + 60_000L * 90;
                        break;
                    }
                    case 1: {
                        p.wanted_chest[i].timeUse = System.currentTimeMillis() + 60_000L * 60 * 3;
                        break;
                    }
                    case 2: {
                        p.wanted_chest[i].timeUse = System.currentTimeMillis() + 60_000L * 60 * 12;
                        break;
                    }
                    case 3: {
                        p.wanted_chest[i].timeUse = System.currentTimeMillis() + 60_000L * 60 * 12;
                        break;
                    }
                    case 4: {
                        p.wanted_chest[i].timeUse = System.currentTimeMillis() + 60_000L * 60 * 18;
                        break;
                    }
                    case 5: {
                        p.wanted_chest[i].timeUse = System.currentTimeMillis() + 60_000L * 60 * 24;
                        break;
                    }
                }
                p.wanted_chest[i].Ruby = 1;
                p.wanted_chest[i].maxTimeUse = 0;
                //
                Wanted_Chest.send_box(p);
                //
                break;
            }
        }
    }
}
