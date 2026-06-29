package map;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import core.Util;
import client.Player;
import core.Manager;
import io.Message;
import template.Option;
import template.Top_Dame;
/**
 *
 * @author Truongbk
 */
public class Boss {
    public static List<Boss> ENTRYS;
    public static byte[] BOSS_LIVE = new byte[] {0, 0, 0, 0, 0, 0};
    public static byte[] BOSS_AREA = new byte[] {-1, -1, -1, -1, -1, -1};
    public static byte[] TIME_NOW = new byte[] {18, 0, 0};
    public int id;
    public Mob mob;
    public byte levelBoss;
    public short[] skill;
    public List<Option> buff;
    public long[] time_atk;
    public List<Top_Dame> TopDame;
    public int index_mob_save;

    public Boss() {}

    public static void create_boss() {
        for (int i0 = 135; i0 < 141; i0++) {
            List<Boss> list_init = new ArrayList<>();
            for (int i = 0; i < Boss.ENTRYS.size(); i++) {
                Boss temp = Boss.ENTRYS.get(i);
                temp.TopDame.clear();
                if (temp.mob.isdie && i0 == temp.mob.mob_template.mob_id) {
                    list_init.add(temp);
                }
            }
            //
            if (list_init.size() > 0 && BOSS_LIVE[i0 - 135] == 0) {
                Boss temp = list_init.get(Util.random(list_init.size()));
                if (temp.mob.isdie) {
                    temp.mob.isdie = false;
                    temp.mob.hp = temp.mob.hp_max;
                    temp.mob.id_target = -1;
                    temp.levelBoss = 1;
                    temp.mob.index = temp.index_mob_save;
                    try {
                        BOSS_AREA[i0 - 135] = temp.mob.map.zone_id;
                        temp.mob.map.can_PK = false;
                        Manager.gI().chatKTG(0,
                                ("Siêu trùm đã xuất hiện hãy cùng săn thôi nào. "
                                        + temp.mob.mob_template.name + " xuất hiện tại "
                                        + temp.mob.map.template.name + " khu "
                                        + (temp.mob.map.zone_id + 1)),
                                5);
                        System.out.println("boss " + temp.mob.mob_template.name + " map "
                                + temp.mob.map.template.name + " khu "
                                + (temp.mob.map.zone_id + 1));
                        //
                        List<Player> list_p = new ArrayList<>();
                        for (int j = 0; j < temp.mob.map.players.size(); j++) {
                            Player p0 = temp.mob.map.players.get(j);
                            if (p0.level / 10 != temp.mob.level / 10) {
                                list_p.add(p0);
                            }
                        }
                        Vgo vgo = new Vgo();
                        vgo.map_go = Map.get_map_by_id(temp.mob.map.template.id);
                        list_p.forEach(l -> {
                            try {
                                vgo.xnew = l.x;
                                vgo.ynew = l.y;
                                l.goto_map(vgo);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        });
                        //
                        Message m_local = new Message(1);
                        m_local.writer().writeByte(1);
                        m_local.writer().writeShort(temp.mob.index);
                        m_local.writer().writeShort(temp.mob.x);
                        m_local.writer().writeShort(temp.mob.y);
                        for (int j = 0; j < temp.mob.map.players.size(); j++) {
                            Player p0 = temp.mob.map.players.get(j);
                            p0.conn.addmsg(m_local);
                        }
                        m_local.cleanup();
                        //
                    } catch (IOException e) {
                    }
                    BOSS_LIVE[i0 - 135] = 1;
                }
            }
        }
    }

    public static Mob get_mob(Player p, int id) {
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss tempB = Boss.ENTRYS.get(i);
            if (!tempB.mob.isdie && tempB.mob.map.template.id == p.map.template.id
                    && tempB.mob.index == id) {
                Mob temp_mob = tempB.mob;
                if (!temp_mob.isdie) {
                    return temp_mob;
                }
            }
        }
        return null;
    }

    public static void result_boss() {
        //
        short[][] list = new short[][] { //
                new short[] {135}, //
                new short[] {136}, //
                new short[] {137}, //
                new short[] {138}, //
                new short[] {139}, //
                new short[] {140}, //
        };
        for (int i12 = 0; i12 < list.length; i12++) {
            List<Top_Dame> list_select = null;
            for (int i = 0; i < Boss.ENTRYS.size(); i++) {
                if (list[i12][0] == Boss.ENTRYS.get(i).mob.mob_template.mob_id
                        && Boss.ENTRYS.get(i).TopDame.size() > 0) {
                    list_select = Boss.ENTRYS.get(i).TopDame;
                    break;
                }
                Boss.ENTRYS.get(i).mob.map.can_PK = true;
            }
            if (list_select != null) {
                List<Top_Dame> result = Util.sort(list_select);
            }
        }
        //
        List<Boss> list_remove = new ArrayList<>();
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss temp = Boss.ENTRYS.get(i);
            if (!temp.mob.isdie) {
                list_remove.add(temp);
            }
        }
        for (int i = 0; i < list_remove.size(); i++) {
            Boss temp = list_remove.get(i);
            temp.mob.isdie = true;
            try {
                temp.mob.map.remove_obj(temp.mob.index, 1);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        try {
            Manager.gI().chatKTG(0, "Hoạt động săn siêu trùm hôm nay đã kết thúc", 5);
        } catch (IOException e) {
            e.printStackTrace();
        }
        BOSS_LIVE = new byte[] {0, 0, 0, 0, 0, 0};
        BOSS_AREA = new byte[] {-1, -1, -1, -1, -1, -1};
    }
}
