package client;

import core.Service;
import core.Util;
import database.SQL;
import io.Message;
import map.Map;
import org.json.simple.JSONArray;
import template.*;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
/**
 *
 * @author Truongbk
 */
public class Clan {
    // type == 0 T.thuyentruong;
    // type == 1 T.thuyenpho;
    // type == 2 T.hoatieu;
    // else 10 T.thanhvien;
    public static List<Clan> ENTRY;
    public static List<String> BXH;
    public List<Clan_member> members;
    public short icon;
    public String name;
    public short id;
    public short level;
    public int xp;
    public short maxAttri;
    public short pointAttri;
    public short[] opAttri;
    public String thongbao;
    public byte trungsinh;
    public int countAction;
    public int ruby;
    public int beri;
    public List<Clan_chat> chat;
    public byte allowRequest;
    public List<Clan_member> mem_request;
    public List<ItemBag47> list_it;
    public List<EffTemplate> buff;
    public Map map_create;

    public static int get_icon_clan(String name) {
        for (int i = 0; i < ENTRY.size(); i++) {
            for (int j = 0; j < ENTRY.get(i).members.size(); j++) {
                if (ENTRY.get(i).members.get(j).name.equals(name)) {
                    return ENTRY.get(i).icon;
                }
            }
        }
        return -1;
    }

    public static Clan get_my_clan(String name) {
        for (int i = 0; i < ENTRY.size(); i++) {
            for (int j = 0; j < ENTRY.get(i).members.size(); j++) {
                if (ENTRY.get(i).members.get(j).name.equals(name)) {
                    return ENTRY.get(i);
                }
            }
        }
        return null;
    }

    public static Clan get_clan_by_id(int id) {
        for (int i = 0; i < ENTRY.size(); i++) {
            if (ENTRY.get(i).id == id) {
                return ENTRY.get(i);
            }
        }
        return null;
    }

    public static Clan get_clan_by_name(String name) {
        for (int i = 0; i < ENTRY.size(); i++) {
            if (ENTRY.get(i).name.equals(name)) {
                return ENTRY.get(i);
            }
        }
        return null;
    }

    public static void send_info(Player p, boolean b) throws IOException {
        if (p.clan != null) {
            Message m = new Message(-52);
            m.writer().writeByte(0);
            m.writer().writeShort(p.clan.id);
            m.writer().writeUTF(p.clan.name);
            if (b) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
            //
            set_data(p, b);
            send_Attri(p, b);
            update_list_member(p, b);
            send_me_to_other(p, p, b);
            send_notice(p, false);
            p.clan.send_inventory(p, b);
            //
            send_xp(p, b);
            send_money(p, b);
            //
        }
    }

    public static void send_money(Player p, boolean b) throws IOException {
        if (p.clan != null) {
            Message m = new Message(-52);
            m.writer().writeByte(17);
            m.writer().writeInt(p.clan.get_ngoc());
            m.writer().writeInt(p.clan.get_vang());
            if (b) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        }
    }

    public static void send_me_to_other(Player p0, Player p, boolean b) throws IOException {
        if (p0.clan != null && p0.conn != null) {
            Message m = new Message(-52);
            m.writer().writeByte(5);
            m.writer().writeShort(p0.index_map);
            m.writer().writeShort(p0.clan.id);
            m.writer().writeShort(p0.clan.icon);
            for (int i = 0; i < p0.clan.members.size(); i++) {
                if (p0.clan.members.get(i).name.equals(p0.name)) {
                    m.writer().writeByte(p0.clan.members.get(i).levelInclan);
                    break;
                }
            }
            m.writer().writeByte(0);
            if (b) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        }
    }

    public synchronized static void reset_day() {
        for (int i = 0; i < Clan.ENTRY.size(); i++) {
            Clan.ENTRY.get(i).chat.clear();
            for (int j = 0; j < Clan.ENTRY.get(i).members.size(); j++) {
                Clan_member mem = Clan.ENTRY.get(i).members.get(j);
                mem.numquest = 0;
                // mem.gopRuby = 0;
            }
        }
    }

    public synchronized static List<Clan> get_list_now_clan() {
        List<Clan> result = new ArrayList<>();
        for (int i = 0; i < Clan.ENTRY.size(); i++) {
            result.add(Clan.ENTRY.get(i));
        }
        return result;
    }

    public int get_vang() {
        return beri;
    }

    public int get_ngoc() {
        return ruby;
    }

    private static void send_xp(Player p, boolean b) throws IOException {
        Message m = new Message(-52);
        m.writer().writeByte(16);
        m.writer()
                .writeByte(p.clan.xp >= Clan.get_xp_max(p.clan.level, p.clan.trungsinh)
                        ? ((p.clan.level == 15) ? 2 : 1)
                        : 0);
        m.writer().writeInt(p.clan.xp);
        if (b) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static void process(Player p, Message m2, int type) throws IOException {
        // System.out.println(type);
        switch (type) {
            case 13: { // up clan
                if (p.clan.members.get(0).name.equals(p.name)) {
                    if (p.clan.xp >= Clan.get_xp_max(p.clan.level, p.clan.trungsinh)) {
                        if (p.clan.get_ngoc() < Clan.get_ngoc_upgrade(p.clan.level,
                                p.clan.trungsinh)) {
                            Service.send_box_ThongBao_OK(p,
                                    "Cần " + Util.number_format(
                                            Clan.get_ngoc_upgrade(p.clan.level, p.clan.trungsinh))
                                            + " ruby băng để thực hiện nâng cấp");
                            return;
                        }
                        if (p.clan.get_vang() < Clan.get_vang_upgrade(p.clan.level)) {
                            Service.send_box_ThongBao_OK(p,
                                    "Cần " + Util.number_format(Clan.get_vang_upgrade(p.clan.level))
                                            + " beri băng để thực hiện nâng cấp");
                            return;
                        }
                        if (p.clan.level >= 14 && p.clan.trungsinh >= 6) {
                            Service.send_box_ThongBao_OK(p, "Có lỗi xảy ra");
                            return;
                        }
                        p.clan.update_ruby(-Clan.get_ngoc_upgrade(p.clan.level, p.clan.trungsinh));
                        p.clan.update_beri(-Clan.get_vang_upgrade(p.clan.level));
                        //
                        p.clan.xp -= Clan.get_xp_max(p.clan.level, p.clan.trungsinh);
                        p.clan.level++;
                        p.clan.pointAttri += 2;
                        if (p.clan.level >= 16) {
                            p.clan.level = 1;
                            p.clan.opAttri = new short[] {0, 0, 0, 0, 0};
                            p.clan.pointAttri = 2;
                            p.clan.maxAttri = 20;
                            p.clan.trungsinh++;
                            switch (p.clan.trungsinh) {
                                case 1: {
                                    p.clan.maxAttri = 25;
                                    break;
                                }
                                case 2: {
                                    p.clan.maxAttri = 30;
                                    break;
                                }
                                case 3: {
                                    p.clan.maxAttri = 35;
                                    break;
                                }
                                case 4: {
                                    p.clan.maxAttri = 40;
                                    break;
                                }
                                case 5: {
                                    p.clan.maxAttri = 45;
                                    break;
                                }
                                case 6: {
                                    p.clan.maxAttri = 50;
                                    break;
                                }
                            }
                        }
                        //
                        for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                            Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i1).name);
                            if (p0 != null) {
                                Clan.send_info(p0, false);
                            }
                        }
                        //
                        if (p.clan.level == 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Trùng sinh băng thành công lên " + p.clan.trungsinh);
                        } else {
                            Service.send_box_ThongBao_OK(p,
                                    "Nâng cấp bang thành công lên cấp " + p.clan.level);
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p, "Chưa đủ điều kiện nâng cấp bang");
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Bạn không phải thuyền trưởng");
                }
                break;
            }
            case 14: { // use item
                short id = m2.reader().readShort();
                // byte chucVu =
                m2.reader().readByte();
                if (p.clan.members.get(0).name.equals(p.name)) {
                    ItemBag47 it_select = null;
                    for (int i = 0; i < p.clan.list_it.size(); i++) {
                        if (p.clan.list_it.get(i).id == id) {
                            if (p.clan.list_it.get(i).quant > 0) {
                                it_select = p.clan.list_it.get(i);
                            }
                            break;
                        }
                    }
                    if (it_select != null) {
                        // System.out.println(id);
                        switch (id) {
                            case 0: {
                                EffTemplate eff = null;
                                for (int i = 0; i < p.clan.buff.size(); i++) {
                                    if (p.clan.buff.get(i).id == 0) {
                                        eff = p.clan.buff.get(i);
                                    }
                                }
                                if (eff != null) {
                                    if (eff.time > System.currentTimeMillis()) {
                                        eff.time += (60_000L * 60);
                                    } else {
                                        eff.time = System.currentTimeMillis() + (60_000L * 60);
                                    }
                                } else {
                                    eff = new EffTemplate(0, 2,
                                            System.currentTimeMillis() + (60_000L * 60));
                                    p.clan.buff.add(eff);
                                }
                                Clan_chat chat = new Clan_chat();
                                chat.idMem = p.clan.members.get(0).id;
                                chat.name = p.clan.members.get(0).name;
                                chat.str = "sử dụng bùa kinh nghiệm lv1 tác dụng còn "
                                        + ((eff.time - System.currentTimeMillis()) / 1000) + "s";
                                chat.time = System.currentTimeMillis();
                                chat.typeChat = -3;
                                p.clan.add_chat(chat);
                                //
                                p.clan.send_chat(chat, null);
                                //
                                it_select.quant--;
                                if (it_select.quant <= 0) {
                                    p.clan.list_it.remove(it_select);
                                }
                                for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(i1).name);
                                    if (p0 != null) {
                                        Clan.send_info(p0, false);
                                    }
                                }
                                break;
                            }
                            case 1: {
                                EffTemplate eff = null;
                                for (int i = 0; i < p.clan.buff.size(); i++) {
                                    if (p.clan.buff.get(i).id == 1) {
                                        eff = p.clan.buff.get(i);
                                    }
                                }
                                if (eff != null) {
                                    if (eff.time > System.currentTimeMillis()) {
                                        eff.time += (60_000L * 60);
                                    } else {
                                        eff.time = System.currentTimeMillis() + (60_000L * 60);
                                    }
                                } else {
                                    eff = new EffTemplate(1, 2,
                                            System.currentTimeMillis() + (60_000L * 60));
                                    p.clan.buff.add(eff);
                                }
                                Clan_chat chat = new Clan_chat();
                                chat.idMem = p.clan.members.get(0).id;
                                chat.name = p.clan.members.get(0).name;
                                chat.str = "sử dụng bùa kinh nghiệm lv2 tác dụng còn "
                                        + ((eff.time - System.currentTimeMillis()) / 1000) + "s";
                                chat.time = System.currentTimeMillis();
                                chat.typeChat = -3;
                                p.clan.add_chat(chat);
                                //
                                p.clan.send_chat(chat, null);
                                //
                                it_select.quant--;
                                if (it_select.quant <= 0) {
                                    p.clan.list_it.remove(it_select);
                                }
                                for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(i1).name);
                                    if (p0 != null) {
                                        Clan.send_info(p0, false);
                                    }
                                }
                                break;
                            }
                            case 2: {
                                EffTemplate eff = null;
                                for (int i = 0; i < p.clan.buff.size(); i++) {
                                    if (p.clan.buff.get(i).id == 2) {
                                        eff = p.clan.buff.get(i);
                                    }
                                }
                                if (eff != null) {
                                    if (eff.time > System.currentTimeMillis()) {
                                        eff.time += (60_000L * 60);
                                    } else {
                                        eff.time = System.currentTimeMillis() + (60_000L * 60);
                                    }
                                } else {
                                    eff = new EffTemplate(2, 2,
                                            System.currentTimeMillis() + (60_000L * 60));
                                    p.clan.buff.add(eff);
                                }
                                Clan_chat chat = new Clan_chat();
                                chat.idMem = p.clan.members.get(0).id;
                                chat.name = p.clan.members.get(0).name;
                                chat.str = "sử dụng bùa hp tác dụng còn "
                                        + ((eff.time - System.currentTimeMillis()) / 1000) + "s";
                                chat.time = System.currentTimeMillis();
                                chat.typeChat = -3;
                                p.clan.add_chat(chat);
                                //
                                p.clan.send_chat(chat, null);
                                //
                                it_select.quant--;
                                if (it_select.quant <= 0) {
                                    p.clan.list_it.remove(it_select);
                                }
                                for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(i1).name);
                                    if (p0 != null) {
                                        Clan.send_info(p0, false);
                                    }
                                }
                                break;
                            }
                            case 3: {
                                EffTemplate eff = null;
                                for (int i = 0; i < p.clan.buff.size(); i++) {
                                    if (p.clan.buff.get(i).id == 3) {
                                        eff = p.clan.buff.get(i);
                                    }
                                }
                                if (eff != null) {
                                    if (eff.time > System.currentTimeMillis()) {
                                        eff.time += (60_000L * 60);
                                    } else {
                                        eff.time = System.currentTimeMillis() + (60_000L * 60);
                                    }
                                } else {
                                    eff = new EffTemplate(3, 2,
                                            System.currentTimeMillis() + (60_000L * 60));
                                    p.clan.buff.add(eff);
                                }
                                Clan_chat chat = new Clan_chat();
                                chat.idMem = p.clan.members.get(0).id;
                                chat.name = p.clan.members.get(0).name;
                                chat.str = "sử dụng bùa mp tác dụng còn "
                                        + ((eff.time - System.currentTimeMillis()) / 1000) + "s";
                                chat.time = System.currentTimeMillis();
                                chat.typeChat = -3;
                                p.clan.add_chat(chat);
                                //
                                p.clan.send_chat(chat, null);
                                //
                                it_select.quant--;
                                if (it_select.quant <= 0) {
                                    p.clan.list_it.remove(it_select);
                                }
                                for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(i1).name);
                                    if (p0 != null) {
                                        Clan.send_info(p0, false);
                                    }
                                }
                                break;
                            }
                            case 4: {
                                EffTemplate eff = null;
                                for (int i = 0; i < p.clan.buff.size(); i++) {
                                    if (p.clan.buff.get(i).id == 4) {
                                        eff = p.clan.buff.get(i);
                                    }
                                }
                                if (eff != null) {
                                    if (eff.time > System.currentTimeMillis()) {
                                        eff.time += (60_000L * 60);
                                    } else {
                                        eff.time = System.currentTimeMillis() + (60_000L * 60);
                                    }
                                } else {
                                    eff = new EffTemplate(4, 2,
                                            System.currentTimeMillis() + (60_000L * 60));
                                    p.clan.buff.add(eff);
                                }
                                Clan_chat chat = new Clan_chat();
                                chat.idMem = p.clan.members.get(0).id;
                                chat.name = p.name;
                                chat.str = "sử dụng bùa tổng hợp tác dụng còn "
                                        + ((eff.time - System.currentTimeMillis()) / 1000) + "s";
                                chat.time = System.currentTimeMillis();
                                chat.typeChat = -3;
                                p.clan.add_chat(chat);
                                //
                                p.clan.send_chat(chat, null);
                                //
                                it_select.quant--;
                                if (it_select.quant <= 0) {
                                    p.clan.list_it.remove(it_select);
                                }
                                for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(i1).name);
                                    if (p0 != null) {
                                        Clan.send_info(p0, false);
                                    }
                                }
                                break;
                            }
                            case 6: {
                                it_select.quant--;
                                if (it_select.quant <= 0) {
                                    p.clan.list_it.remove(it_select);
                                }
                                p.clan.update_beri(10_000);
                                for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(i1).name);
                                    if (p0 != null) {
                                        Clan.send_info(p0, false);
                                    }
                                }
                                break;
                            }
                            case 7: {
                                it_select.quant--;
                                if (it_select.quant <= 0) {
                                    p.clan.list_it.remove(it_select);
                                }
                                p.clan.pointAttri = 0;
                                for (int i = 1; i <= p.clan.level; i++) {
                                    p.clan.pointAttri += 2;
                                }
                                p.clan.opAttri = new short[] {0, 0, 0, 0, 0};
                                for (int i1 = 0; i1 < p.clan.members.size(); i1++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(i1).name);
                                    if (p0 != null) {
                                        Clan.send_info(p0, false);
                                    }
                                }
                                Service.send_box_ThongBao_OK(p, "Tẩy tiềm năng băng thành công");
                                break;
                            }
                            default: {
                                Service.send_box_ThongBao_OK(p, "Hiện tại "
                                        + ItemTemplate8.ENTRYS.get(id).name
                                        + " chưa thể sử dụng, đợi mình 1 thời gian nữa sẽ cập nhật sớm nhất nha");
                                break;
                            }
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p, "Không đủ 1 "
                                + ItemTemplate8.ENTRYS.get(id).name + " trong hành trang băng");
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Bạn không phải thuyền trưởng");
                }
                break;
            }
            // case 2:
            case 15: {
                Service.input_text(p, 11, "Đóng góp băng", new String[] {"Nhập số ruby muốn góp"});
                break;
            }
            case 3: { // phong chuc
                String strChat = m2.reader().readUTF();
                byte chucVu = m2.reader().readByte();
                // System.out.println(strChat);
                // System.out.println(chucVu);
                Clan_member getMem = null;
                for (int i = 0; i < p.clan.members.size(); i++) {
                    if (p.clan.members.get(i).name.equals(p.name)
                            && (p.clan.members.get(i).levelInclan == 0
                                    || p.clan.members.get(i).levelInclan == 1)) {
                        getMem = p.clan.members.get(i);
                        break;
                    }
                }
                if (chucVu != 0 && getMem != null) {
                    Clan_member mem = null;
                    for (int i = 0; i < p.clan.members.size(); i++) {
                        if (p.clan.members.get(i).name.equals(strChat)) {
                            mem = p.clan.members.get(i);
                            break;
                        }
                    }
                    if (mem != null) {
                        if (mem.levelInclan == 0) {
                            Service.send_box_ThongBao_OK(p, "Không thể thực hiện chức năng này!");
                            return;
                        }
                        if (mem.levelInclan == chucVu) {
                            Service.send_box_ThongBao_OK(p, "Đối phương đã có chức vụ này");
                            return;
                        }
                        int numThuyenPho = 0;
                        int numHoaTieu = 0;
                        for (int i = 0; i < p.clan.members.size(); i++) {
                            if (p.clan.members.get(i).levelInclan == 1) {
                                numThuyenPho++;
                            }
                            if (p.clan.members.get(i).levelInclan == 2) {
                                numHoaTieu++;
                            }
                        }
                        if (chucVu == 1 && numThuyenPho >= 2) {
                            Service.send_box_ThongBao_OK(p,
                                    "Trong băng chỉ có thể tối đa 2 thuyền phó!");
                            return;
                        }
                        if (chucVu == 2 && numHoaTieu >= 1) {
                            Service.send_box_ThongBao_OK(p,
                                    "Trong băng chỉ có thể tối đa 1 hoa tiêu!");
                            return;
                        }
                        //
                        mem.levelInclan = chucVu;
                        Player p0 = Map.get_player_by_name_allmap(mem.name);
                        if (p0 != null) {
                            Clan.send_info(p0, false);
                            for (int i = 0; i < p.map.players.size(); i++) {
                                if (!p.map.players.get(i).equals(p0)) {
                                    Clan.send_me_to_other(p0, p.map.players.get(i), false);
                                }
                            }
                            Service.send_box_ThongBao_OK(p0,
                                    "Bạn được phong thành " + (mem.levelInclan == 1 ? "Thuyền phó"
                                            : (mem.levelInclan == 2 ? "Hoa tiêu" : "thuyền viên")));
                        }
                        for (int i = 0; i < p.clan.members.size(); i++) {
                            Player p0ther =
                                    Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                            if (p0ther != null) {
                                update_list_member(p0ther, false);
                                Clan.send_info(p0ther, false);
                            }
                        }
                        //
                        Clan_chat chat = new Clan_chat();
                        chat.idMem = p.clan.members.get(0).id;
                        chat.name = p.name;
                        chat.str = "phong chức " + mem.name + " thành "
                                + (mem.levelInclan == 1 ? "Thuyền phó"
                                        : (mem.levelInclan == 2 ? "Hoa tiêu" : "thuyền viên"));
                        chat.time = System.currentTimeMillis();
                        chat.typeChat = -3;
                        p.clan.add_chat(chat);
                        //
                        p.clan.send_chat(chat, null);
                        Service.send_box_ThongBao_OK(p, "Phong chức " + mem.name + " thành "
                                + (mem.levelInclan == 1 ? "Thuyền phó"
                                        : (mem.levelInclan == 2 ? "Hoa tiêu" : "thuyền viên"))
                                + " thành công");
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Bạn không phải thuyền trưởng");
                }
                break;
            }
            case 1: { // truc xuat
                String strChat = m2.reader().readUTF();
                byte chucVu = m2.reader().readByte();
                Clan_member getMem = null;
                for (int i = 0; i < p.clan.members.size(); i++) {
                    if (p.clan.members.get(i).name.equals(p.name)
                            && (p.clan.members.get(i).levelInclan == 0
                                    || p.clan.members.get(i).levelInclan == 1)) {
                        getMem = p.clan.members.get(i);
                        break;
                    }
                }
                if (chucVu == 0 && getMem != null) {
                    Clan_member mem = null;
                    for (int i = 0; i < p.clan.members.size(); i++) {
                        if (p.clan.members.get(i).name.equals(strChat)) {
                            mem = p.clan.members.get(i);
                            break;
                        }
                    }
                    if (mem != null) {
                        if (mem.levelInclan == 0) {
                            Service.send_box_ThongBao_OK(p, "Không thể thực hiện chức năng này!");
                            return;
                        }
                        p.clan.members.remove(mem);
                        //
                        Player p0 = Map.get_player_by_name_allmap(mem.name);
                        if (p0 != null) {
                            Clan.send_info(p0, false);
                            for (int i = 0; i < p.map.players.size(); i++) {
                                if (!p.map.players.get(i).equals(p0)) {
                                    Clan.send_me_to_other(p0, p.map.players.get(i), false);
                                }
                            }
                            Service.send_box_ThongBao_OK(p0,
                                    "Bạn bị trục xuất khỏi băng " + p.clan.name);
                            Message m = new Message(-52);
                            m.writer().writeByte(10);
                            m.writer().writeShort(p0.index_map);
                            p.map.send_msg_all_p(m, p0, true);
                            m.cleanup();
                            p0.clan = null;
                        }
                        for (int i = 0; i < p.clan.members.size(); i++) {
                            Player p0ther =
                                    Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                            if (p0ther != null) {
                                update_list_member(p0ther, false);
                                Clan.send_info(p0ther, false);
                                //
                                Message m_out = new Message(-52);
                                m_out.writer().writeByte(12);
                                m_out.writer().writeByte(1);
                                m_out.writer().writeUTF(mem.name);
                                p0ther.conn.addmsg(m_out);
                                m_out.cleanup();
                            }
                        }
                        //
                        Clan_chat chat = new Clan_chat();
                        chat.idMem = p.clan.members.get(0).id;
                        chat.name = p.name;
                        chat.str = mem.name + " bị trục xuất khỏi băng";
                        chat.time = System.currentTimeMillis();
                        chat.typeChat = -2;
                        p.clan.add_chat(chat);
                        //
                        p.clan.send_chat(chat, null);
                        Service.send_box_ThongBao_OK(p,
                                mem.name + " bị trục xuất khỏi băng thành công");
                    }
                }
                break;
            }
            case 4: { // roi bang
                if (p.clan != null) {
                    for (int i = 0; i < p.clan.members.size(); i++) {
                        if (p.clan.members.get(i).name.equals(p.name)) {
                            p.clan.members.remove(i);
                            break;
                        }
                    }
                    //
                    Clan.send_info(p, false);
                    for (int i = 0; i < p.map.players.size(); i++) {
                        if (!p.map.players.get(i).equals(p)) {
                            Clan.send_me_to_other(p, p.map.players.get(i), false);
                        }
                    }
                    for (int i = 0; i < p.clan.members.size(); i++) {
                        if (!p.clan.members.get(i).name.equals(p.name)) {
                            Player p0ther =
                                    Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                            if (p0ther != null) {
                                update_list_member(p0ther, false);
                                Clan.send_info(p0ther, false);
                                //
                                Message m_out = new Message(-52);
                                m_out.writer().writeByte(12);
                                m_out.writer().writeByte(1);
                                m_out.writer().writeUTF(p.name);
                                p0ther.conn.addmsg(m_out);
                                m_out.cleanup();
                            }
                        }
                    }
                    //
                    Message m = new Message(-52);
                    m.writer().writeByte(10);
                    m.writer().writeShort(p.index_map);
                    p.map.send_msg_all_p(m, p, true);
                    m.cleanup();
                    Service.send_box_ThongBao_OK(p, "Rời băng " + p.clan.name + " thành công");
                    //
                    Clan_chat chat = new Clan_chat();
                    chat.idMem = p.clan.members.get(0).id;
                    chat.name = p.clan.members.get(0).name;
                    chat.str = p.name + " rời băng";
                    chat.time = System.currentTimeMillis();
                    chat.typeChat = -2;
                    p.clan.add_chat(chat);
                    //
                    p.clan.send_chat(chat, null);
                    //
                    p.clan = null;
                }
                break;
            }
            case 17: { // update list mem
                update_list_member(p, false);
                break;
            }
            case 0: {
                String strChat = m2.reader().readUTF();
                // byte chucVu =
                m2.reader().readByte();
                Clan_member mem = null;
                for (int i = 0; i < p.clan.members.size(); i++) {
                    if (p.clan.members.get(i).name.equals(p.name)) {
                        mem = p.clan.members.get(i);
                        break;
                    }
                }
                if (mem != null) {
                    Clan_chat chat = new Clan_chat();
                    chat.idMem = mem.id;
                    chat.name = p.name;
                    chat.str = strChat;
                    chat.time = System.currentTimeMillis();
                    chat.typeChat = (byte) (mem.levelInclan == 0 ? -1 : -4);
                    p.clan.add_chat(chat);
                    //
                    p.clan.send_chat(chat, null);
                }
                break;
            }
            case 5: {
                String strChat = m2.reader().readUTF();
                // byte chucVu =
                m2.reader().readByte();
                //
                if (p.clan.members.get(0).name.equals(p.name)) {
                    p.clan.thongbao = strChat;
                    for (int i = 0; i < p.clan.members.size(); i++) {
                        Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                        if (p0 != null) {
                            Clan.send_notice(p0, false);
                        }
                    }
                    //
                    Clan_chat chat = new Clan_chat();
                    chat.idMem = p.clan.members.get(0).id;
                    chat.name = p.name;
                    chat.str = strChat;
                    chat.time = System.currentTimeMillis();
                    chat.typeChat = -3;
                    p.clan.add_chat(chat);
                    //
                    p.clan.send_chat(chat, null);
                }
                break;
            }
            case 6: {
                byte chucVu = m2.reader().readByte();
                // byte id =
                m2.reader().readByte();
                if (p.clan.members.get(0).name.equals(p.name)) {
                    if (p.clan.pointAttri > 0) {
                        if ((p.clan.opAttri[chucVu]
                                + Clan.get_point_trungsinh_plus(p.clan)) >= p.clan.maxAttri) {
                            Service.send_box_ThongBao_OK(p,
                                    "Hiện tại tiềm năng tối đa là " + p.clan.maxAttri);
                        } else {
                            p.clan.pointAttri--;
                            p.clan.opAttri[chucVu]++;
                            for (int i = 0; i < p.clan.members.size(); i++) {
                                Player p0 =
                                        Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                                if (p0 != null) {
                                    Clan.send_Attri(p0, false);
                                    p0.update_info_to_all();
                                }
                            }
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p, "Không đủ 1 điểm tiềm năng");
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Bạn không phải thuyền trưởng");
                }
                break;
            }
            case 7: {
                Clan_member clan_mem = null;
                for (int i = 0; i < p.clan.members.size(); i++) {
                    if (p.clan.members.get(i).name.equals(p.name)) {
                        clan_mem = p.clan.members.get(i);
                        break;
                    }
                }
                if (clan_mem != null && clan_mem.levelInclan >= 0 && clan_mem.levelInclan <= 2) {
                    // String strChat =
                    m2.reader().readUTF();
                    int id = m2.reader().readInt();
                    Clan_chat clan_chat = null;
                    for (int i = 0; i < p.clan.chat.size(); i++) {
                        if (p.clan.chat.get(i).idChat == id) {
                            clan_chat = p.clan.chat.get(i);
                            break;
                        }
                    }
                    if (clan_chat != null) {
                        Clan_member mem =
                                p.clan.get_mem_request(clan_chat.str.replace(" xin vào băng", ""));
                        if (mem != null) {
                            // clear chat
                            List<Clan_chat> list_remove = new ArrayList<>();
                            for (int i = 0; i < p.clan.chat.size(); i++) {
                                if (p.clan.chat.get(i).str.equals(clan_chat.str)) {
                                    list_remove.add(p.clan.chat.get(i));
                                }
                            }
                            for (int i = 0; i < list_remove.size(); i++) {
                                for (int j = 0; j < p.clan.members.size(); j++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(j).name);
                                    if (p0 != null) {
                                        p.clan.remove_chat(p0, list_remove.get(i).idChat);
                                    }
                                }
                            }
                            p.clan.chat.removeAll(list_remove);
                            //
                            int num_clazz = 0;
                            for (int j = 0; j < p.clan.members.size(); j++) {
                                if (p.clan.members.get(j).clazz == mem.clazz) {
                                    num_clazz++;
                                }
                            }
                            if (num_clazz >= 4) {
                                Service.send_box_ThongBao_OK(p,
                                        "Băng đã đầy đủ 4/4 " + Clazz.NAME[mem.clazz - 1]);
                                return;
                            }
                            if (p.clan.members.size() >= Clan.get_mem_max(p.clan.level,
                                    p.clan.trungsinh)) {
                                Service.send_box_ThongBao_OK(p, "Băng đầy đủ người rồi");
                            } else {
                                //
                                boolean checkCoMat = false;
                                for (int i = 0; i < p.clan.members.size(); i++) {
                                    if (p.clan.members.get(i).name.equals(mem.name)) {
                                        checkCoMat = true;
                                        break;
                                    }
                                }
                                if (!checkCoMat) {
                                    Player p0 = Map.get_player_by_name_allmap(mem.name);
                                    if (p0 != null) {
                                        p0.clan = p.clan;
                                        mem.id = (short) Clan_member.get_id(p0.clan.members);
                                        p.clan.members.add(mem);
                                        //
                                        Clan.send_info(p0, false);
                                        for (int i = 0; i < p.map.players.size(); i++) {
                                            if (!p.map.players.get(i).equals(p0)) {
                                                Clan.send_me_to_other(p0, p.map.players.get(i),
                                                        false);
                                            }
                                        }
                                        Service.send_box_ThongBao_OK(p0, "Tham gia băng hải tặc "
                                                + p.clan.name + " thành công");
                                    }
                                    for (int i = 0; i < p.clan.members.size(); i++) {
                                        Player p0ther = Map.get_player_by_name_allmap(
                                                p.clan.members.get(i).name);
                                        if (p0ther != null) {
                                            update_list_member(p0ther, false);
                                            Clan.send_info(p0ther, false);
                                        }
                                    }
                                } else {
                                    Service.send_box_ThongBao_OK(p, "Đã có mặt trong băng");
                                }
                            }
                            p.clan.mem_request.remove(mem);
                        }
                    }
                }
                break;
            }
            case 16: {
                Clan_member clan_mem = null;
                for (int i = 0; i < p.clan.members.size(); i++) {
                    if (p.clan.members.get(i).name.equals(p.name)) {
                        clan_mem = p.clan.members.get(i);
                        break;
                    }
                }
                if (clan_mem != null && clan_mem.levelInclan >= 0 && clan_mem.levelInclan <= 2) {
                    // String strChat =
                    m2.reader().readUTF();
                    int id = m2.reader().readInt();
                    Clan_chat clan_chat = null;
                    for (int i = 0; i < p.clan.chat.size(); i++) {
                        if (p.clan.chat.get(i).idChat == id) {
                            clan_chat = p.clan.chat.get(i);
                            break;
                        }
                    }
                    if (clan_chat != null) {
                        Clan_member mem =
                                p.clan.get_mem_request(clan_chat.str.replace(" xin vào băng", ""));
                        if (mem != null) {
                            // clear chat
                            List<Clan_chat> list_remove = new ArrayList<>();
                            for (int i = 0; i < p.clan.chat.size(); i++) {
                                if (p.clan.chat.get(i).str.equals(clan_chat.str)) {
                                    list_remove.add(p.clan.chat.get(i));
                                }
                            }
                            for (int i = 0; i < list_remove.size(); i++) {
                                for (int j = 0; j < p.clan.members.size(); j++) {
                                    Player p0 = Map
                                            .get_player_by_name_allmap(p.clan.members.get(j).name);
                                    if (p0 != null) {
                                        p.clan.remove_chat(p0, list_remove.get(i).idChat);
                                    }
                                }
                            }
                            p.clan.chat.removeAll(list_remove);
                            p.clan.mem_request.remove(mem);
                        }
                    }
                }
                break;
            }
            case 9: { // update
                set_data(p, false);
                send_Attri(p, false);
                update_list_member(p, false);
                for (int i = 0; i < p.clan.chat.size(); i++) {
                    p.clan.send_chat(p.clan.chat.get(i), p);
                }
                break;
            }
            case 11: {
                int id = m2.reader().readInt();
                Clan clan = Clan.get_clan_by_id(id);
                if (clan != null) {
                    if (clan.allowRequest == 0) {
                        Service.send_box_ThongBao_OK(p, "Băng này đã đầy đủ người");
                    } else {
                        boolean check = true;
                        String request_chat = p.name + " xin vào băng";
                        long time = 0;
                        for (int i = 0; i < clan.chat.size(); i++) {
                            if (clan.chat.get(i).typeChat == 1
                                    && clan.chat.get(i).str.equals(request_chat)
                                    && (System.currentTimeMillis()
                                            - clan.chat.get(i).time) < 600_000L - 600_000) {
                                time = 600_000
                                        - (System.currentTimeMillis() - clan.chat.get(i).time);
                                check = false;
                                break;
                            }
                        }
                        if (check) {
                            Clan_member mem = null;
                            for (int i = 0; i < clan.mem_request.size(); i++) {
                                if (clan.mem_request.get(i).name.equals(p.name)) {
                                    mem = clan.mem_request.get(i);
                                    break;
                                }
                            }
                            if (mem == null) {
                                mem = new Clan_member();
                                mem.name = p.name;
                                mem.conghien = 0;
                                mem.donate = 0;
                                mem.gopRuby = 32_000;
                                mem.numquest = 3;
                                mem.id = 0;
                                mem.hair = (short) p.get_hair();
                                mem.head = (short) p.get_head();
                                mem.hat = p.get_hat();
                                mem.level = p.level;
                                mem.levelInclan = 10;
                                mem.clazz = p.clazz;
                                clan.mem_request.add(mem);
                            }
                            //
                            Clan_chat chat = new Clan_chat();
                            chat.idMem = clan.members.get(0).id;
                            chat.name = clan.members.get(0).name;
                            chat.str = request_chat;
                            chat.time = System.currentTimeMillis();
                            chat.typeChat = 1;
                            clan.add_chat(chat);
                            //
                            clan.send_chat(chat, null);
                        } else {
                            Service.send_box_ThongBao_OK(p,
                                    "Xin vào sau " + (time / 1000) + "s nữa");
                        }
                    }
                }
                break;
            }
            case 10: {
                int id = m2.reader().readInt();
                Player p0 = p.map.get_player_by_id_inmap(id);
                if (p0 != null) {
                    Message m = new Message(-52);
                    m.writer().writeByte(7);
                    m.writer().writeInt(p.index_map);
                    m.writer().writeUTF(p.name);
                    p0.conn.addmsg(m);
                    m.cleanup();
                } else {
                    Service.send_box_ThongBao_OK(p,
                            "Nhân vật hiện đang offline hoặc không ở trong map");
                }
                break;
            }
            case 12: {
                if (p.clan != null) {
                    Service.send_box_ThongBao_OK(p, "Bạn đang ở trong 1 băng hải tặc khác");
                } else {
                    int id = m2.reader().readInt();
                    Player p0 = p.map.get_player_by_id_inmap(id);
                    if (p0 != null && p0.clan != null) {
                        if (p0.clan.members.size() >= Clan.get_mem_max(p0.clan.level,
                                p0.clan.trungsinh)) {
                            Service.send_box_ThongBao_OK(p, "Băng hải tặc đã này đủ người");
                        } else {
                            Clan_member clan_mem = null;
                            for (int i = 0; i < p0.clan.members.size(); i++) {
                                if (p0.clan.members.get(i).name.equals(p0.name)) {
                                    clan_mem = p0.clan.members.get(i);
                                    break;
                                }
                            }
                            if (clan_mem != null && clan_mem.levelInclan >= 0
                                    && clan_mem.levelInclan <= 2) {
                                int num_clazz = 0;
                                for (int j = 0; j < p0.clan.members.size(); j++) {
                                    if (p0.clan.members.get(j).clazz == p.clazz) {
                                        num_clazz++;
                                    }
                                }
                                if (num_clazz >= 4) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Băng đã đầy đủ 4/4 " + Clazz.NAME[p.clazz - 1]);
                                    return;
                                }
                                //
                                boolean checkCoMat = false;
                                for (int i = 0; i < p0.clan.members.size(); i++) {
                                    if (p0.clan.members.get(i).name.equals(p.name)) {
                                        checkCoMat = true;
                                        break;
                                    }
                                }
                                if (!checkCoMat) {
                                    p.clan = p0.clan;
                                    Clan_member mem = new Clan_member();
                                    mem.name = p.name;
                                    mem.conghien = 0;
                                    mem.donate = 0;
                                    mem.gopRuby = 32_000;
                                    mem.numquest = 3;
                                    mem.id = (short) Clan_member.get_id(p0.clan.members);
                                    mem.hair = (short) p.get_hair();
                                    mem.head = (short) p.get_head();
                                    mem.hat = p.get_hat();
                                    mem.level = p.level;
                                    mem.levelInclan = 10;
                                    mem.clazz = p.clazz;
                                    p0.clan.members.add(mem);
                                    //
                                    Clan.send_info(p, false);
                                    for (int i = 0; i < p.map.players.size(); i++) {
                                        if (!p.map.players.get(i).equals(p)) {
                                            Clan.send_me_to_other(p, p.map.players.get(i), false);
                                        }
                                    }
                                    for (int i = 0; i < p0.clan.members.size(); i++) {
                                        if (!p0.clan.members.get(i).name.equals(p.name)) {
                                            Player p0ther = Map.get_player_by_name_allmap(
                                                    p0.clan.members.get(i).name);
                                            if (p0ther != null) {
                                                update_list_member(p0ther, false);
                                                Clan.send_info(p0ther, false);
                                            }
                                        }
                                    }
                                    Service.send_box_ThongBao_OK(p, "Tham gia băng hải tặc "
                                            + p0.clan.name + " thành công");
                                } else {
                                    Service.send_box_ThongBao_OK(p, "Đã có mặt trong băng");
                                }
                            } else {
                                Service.send_box_ThongBao_OK(p,
                                        "Đối phương không phải là đội trưởng");
                            }
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p,
                                "Nhân vật hiện đang offline hoặc không ở trong map");
                    }
                }
                break;
            }
        }
    }

    private void remove_chat(Player p, int idChat) throws IOException {
        Message m = new Message(-52);
        m.writer().writeByte(11);
        m.writer().writeShort(idChat);
        p.conn.addmsg(m);
        m.cleanup();
    }

    private Clan_member get_mem_request(String str) {
        for (int i = 0; i < this.mem_request.size(); i++) {
            if (this.mem_request.get(i).name.equals(str)) {
                return this.mem_request.get(i);
            }
        }
        return null;
    }

    private static void send_notice(Player p, boolean b) throws IOException {
        if (p.conn != null) {
            Message m = new Message(-52);
            m.writer().writeByte(14);
            m.writer().writeUTF(p.clan.thongbao);
            if (b) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        }
    }

    private void send_chat(Clan_chat chat, Player p) throws IOException {
        for (int i = 0; i < this.members.size(); i++) {
            Player p0 = Map.get_player_by_name_allmap(this.members.get(i).name);
            if (p0 != null && (p == null || p0.equals(p))) {
                Message m = new Message(-52);
                m.writer().writeByte(8);
                m.writer().writeByte(chat.typeChat); // -1, -4 khac color, 1: xin vao
                m.writer().writeShort(chat.idChat);
                m.writer().writeShort(chat.idMem);
                m.writer().writeUTF(chat.name);
                m.writer().writeUTF(chat.str);
                m.writer().writeLong(chat.time);
                p0.conn.addmsg(m);
                m.cleanup();
            }
        }
    }

    private synchronized void add_chat(Clan_chat chat) {
        int id_get = 0;
        for (int i = 0; i < this.chat.size(); i++) {
            id_get = Math.max(id_get, this.chat.get(i).idChat);
        }
        chat.idChat = id_get + 1;
        this.chat.add(chat);
    }

    public static void set_data(Player p, boolean b) throws IOException {
        if (p.clan != null) {
            Message m = new Message(-52);
            m.writer().writeByte(2);
            m.writer().writeShort(p.clan.icon);
            m.writer().writeUTF(p.clan.members.get(0).name); // captain name
            m.writer().writeShort(p.clan.level);
            m.writer().writeInt(p.clan.xp);
            m.writer().writeInt(Clan.get_xp_max(p.clan.level, p.clan.trungsinh));
            m.writer().writeByte(p.clan.members.size());
            m.writer().writeByte(Clan.get_mem_max(p.clan.level, p.clan.trungsinh));
            m.writer().writeInt(Clan.get_rank(p.clan)); // rank
            m.writer().writeUTF(p.clan.thongbao);
            m.writer().writeByte(p.clan.trungsinh); // trung sinh
            m.writer().writeInt(p.clan.countAction); // count Action
            if (b) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        }
    }

    private static int get_mem_max(short level, int trungsinh) {
        int max = 10;
        if (trungsinh == 0) {
            if (level == 1) {
                max = 5;
            } else if (level == 2) {
                max = 6;
            } else if (level == 3) {
                max = 7;
            } else if (level == 4) {
                max = 8;
            } else if (level == 5) {
                max = 9;
            }
        } else {
            max += trungsinh;
        }
        if (max > 13) {
            max = 13;
        }
        return max;
    }

    public static void update_list_member(Player p, boolean b) throws IOException {
        if (p.clan != null) {
            int ver_ = Integer.parseInt(p.conn.version.replace(".", ""));
            Message m = new Message(-52);
            m.writer().writeByte(3);
            m.writer().writeByte(p.clan.members.size());
            for (int i = 0; i < p.clan.members.size(); i++) {
                Clan_member mem = p.clan.members.get(i);
                Player p0 = Map.get_player_by_name_allmap(mem.name);
                if (p0 != null) {
                    mem.head = (short) p0.get_head();
                    mem.hair = (short) p0.get_hair();
                    mem.hat = p0.get_hat();
                    mem.name = p0.name;
                    mem.level = p0.level;
                }
                m.writer().writeShort(mem.id);
                m.writer().writeUTF(mem.name);
                m.writer().writeShort(mem.level);
                m.writer().writeByte(mem.levelInclan);
                m.writer().writeShort(mem.donate);
                m.writer().writeShort(mem.gopRuby);
                m.writer().writeShort(mem.numquest);
                if (ver_ >= 111) {
                    m.writer().writeInt(mem.conghien);
                }
                //
                m.writer().writeShort(mem.head);
                m.writer().writeShort(mem.hair);
                m.writer().writeShort(mem.hat);
                m.writer().writeByte(p0 != null ? 1 : 0);
            }
            if (b) {
                p.list_msg_cache.add(m);
            } else {
                p.conn.addmsg(m);
            }
            m.cleanup();
        }
    }

    private static void send_Attri(Player p, boolean b) throws IOException {
        Message m = new Message(-52);
        m.writer().writeByte(4);
        m.writer().writeShort(p.clan.maxAttri);
        m.writer().writeShort(p.clan.pointAttri);
        for (int i = 0; i < 5; i++) {
            m.writer().writeShort(p.clan.opAttri[i] + Clan.get_point_trungsinh_plus(p.clan));
        }
        if (b) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public static int get_point_trungsinh_plus(Clan clan) {
        return clan.trungsinh * 4;
    }

    private static int get_rank(Clan clan) {
        for (int i = 0; i < Clan.BXH.size(); i++) {
            if (Clan.BXH.get(i).equals(clan.name)) {
                return (i + 1);
            }
        }
        return 9999;
    }

    public synchronized static int get_clan_id() {
        int result = -1;
        for (int i = 0; i < ENTRY.size(); i++) {
            result = Math.max(result, ENTRY.get(i).id);
        }
        result++;
        return result;
    }

    @SuppressWarnings("unchecked")
    public static boolean create_new_clan(Clan clan) {
        Connection connection = null;
        PreparedStatement ps = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "INSERT INTO `clan` (`id`, `name`, `info`, `notice`, `member`, `item`, `xp`, `buff`) "
                            + "VALUES (?, ?, ?, ?, ?, ?, ?, ?)");
            ps.clearParameters();
            ps.setInt(1, clan.id);
            ps.setNString(2, clan.name);
            JSONArray js = new JSONArray();
            js.add(clan.icon);
            js.add(clan.level);
            js.add(clan.xp);
            js.add(clan.maxAttri);
            js.add(clan.pointAttri);
            js.add(clan.trungsinh);
            js.add(clan.countAction);
            js.add(clan.ruby);
            js.add(clan.beri);
            js.add(clan.allowRequest);
            JSONArray js2 = new JSONArray();
            for (int i = 0; i < clan.opAttri.length; i++) {
                js2.add(clan.opAttri[i]);
            }
            js.add(js2);
            ps.setNString(3, js.toJSONString());
            js.clear();
            js2.clear();
            ps.setNString(4, clan.thongbao);
            for (int i = 0; i < clan.members.size(); i++) {
                JSONArray js_in = new JSONArray();
                Clan_member mem = clan.members.get(i);
                js_in.add(mem.name);
                js_in.add(mem.level);
                js_in.add(mem.levelInclan);
                js_in.add(mem.donate);
                js_in.add(mem.gopRuby);
                js_in.add(mem.numquest);
                js_in.add(mem.conghien);
                js_in.add(mem.head);
                js_in.add(mem.hair);
                js_in.add(mem.hat);
                js_in.add(mem.clazz);
                js.add(js_in);
            }
            ps.setNString(5, js.toJSONString());
            ps.setNString(6, "[]");
            ps.setLong(7, 0);
            ps.setNString(8, "[]");
            js.clear();
            ps.executeUpdate();
        } catch (SQLException e) {
            // e.printStackTrace();
            return false;
        } finally {
            try {
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        Clan.add_new_clan(clan);
        return true;
    }

    public synchronized static void add_new_clan(Clan clan) {
        ENTRY.add(clan);
    }

    public synchronized static void delete_clan(Clan clan) {
        ENTRY.remove(clan);
    }

    public static int get_xp_max(int level, int ts) {
        return 10000 * level;
    }

    private static int get_ngoc_upgrade(short level, int trungsinh) {
        return 100 * level;
    }

    private static int get_vang_upgrade(short level) {
        return level * 5_000;
    }

    @SuppressWarnings("unchecked")
    public synchronized static void update() {
        Connection connection = null;
        PreparedStatement ps = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement(
                    "UPDATE `clan` SET `info` = ?, `member` = ?, item = ?, `xp` = ?,"
                            + " `buff` = ? WHERE `id` = ?");
            for (int i = 0; i < Clan.ENTRY.size(); i++) {
                Clan clan = Clan.ENTRY.get(i);
                ps.clearParameters();
                JSONArray js = new JSONArray();
                js.add(clan.icon);
                js.add(clan.level);
                js.add(clan.xp);
                js.add(clan.maxAttri);
                js.add(clan.pointAttri);
                js.add(clan.trungsinh);
                js.add(clan.countAction);
                js.add(clan.ruby);
                js.add(clan.beri);
                js.add(clan.allowRequest);
                JSONArray js2 = new JSONArray();
                for (int i2 = 0; i2 < clan.opAttri.length; i2++) {
                    js2.add(clan.opAttri[i2]);
                }
                js.add(js2);
                ps.setNString(1, js.toJSONString());
                js.clear();
                for (int i2 = 0; i2 < clan.members.size(); i2++) {
                    JSONArray js_in = new JSONArray();
                    Clan_member mem = clan.members.get(i2);
                    js_in.add(mem.name);
                    js_in.add(mem.level);
                    js_in.add(mem.levelInclan);
                    js_in.add(mem.donate);
                    js_in.add(mem.gopRuby);
                    js_in.add(mem.numquest);
                    js_in.add(mem.conghien);
                    js_in.add(mem.head);
                    js_in.add(mem.hair);
                    js_in.add(mem.hat);
                    js_in.add(mem.clazz);
                    js.add(js_in);
                }
                ps.setNString(2, js.toJSONString());
                js.clear();
                for (int j = 0; j < clan.list_it.size(); j++) {
                    JSONArray js_in = new JSONArray();
                    js_in.add(clan.list_it.get(j).id);
                    js_in.add(clan.list_it.get(j).quant);
                    js.add(js_in);
                }
                ps.setNString(3, js.toJSONString());
                js.clear();
                long xp_total = clan.xp;
                if (xp_total > Clan.get_xp_max(clan.level, clan.trungsinh)) {
                    xp_total = Clan.get_xp_max(clan.level, clan.trungsinh);
                }
                for (int j = 1; j < clan.level; j++) {
                    xp_total += Clan.get_xp_max(j, clan.trungsinh);
                }
                xp_total += (2_400_000 * clan.trungsinh);
                ps.setLong(4, xp_total);
                for (int j = 0; j < clan.buff.size(); j++) {
                    JSONArray js_in = new JSONArray();
                    js_in.add(clan.buff.get(j).id);
                    js_in.add(clan.buff.get(j).param);
                    js_in.add(clan.buff.get(j).time);
                    js.add(js_in);
                }
                ps.setNString(5, js.toJSONString());
                js.clear();
                ps.setInt(6, clan.id);
                ps.executeUpdate();
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        //
        Clan.BXH.clear();
        connection = null;
        ps = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement("SELECT `name` FROM `clan` ORDER BY `xp` DESC");
            rs = ps.executeQuery();
            while (rs.next()) {
                Clan.BXH.add(rs.getString("name"));
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        connection = null;
        ps = null;
        try {
            connection = SQL.gI().getCon();
            ps = connection.prepareStatement("UPDATE `clan` SET `notice` = ? WHERE `id` = ?");
            for (int i = 0; i < Clan.ENTRY.size(); i++) {
                Clan clan = Clan.ENTRY.get(i);
                ps.clearParameters();
                ps.setNString(1, clan.thongbao);
                ps.setInt(2, clan.id);
                ps.executeUpdate();
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (ps != null) {
                    ps.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    public static int get_ngoc_icon(short id) {
        int result = 50;
        if (id >= 293 && id < 370) {
            result = 200;
        }
        return result;
    }

    public synchronized void update_xp(int num) {
        // if (Manager.gI().exp > 0) {
        this.xp += num;
        // }
    }

    public synchronized void update_beri(long num) {
        if ((num + this.beri) <= 2_000_000_000L) {
            this.beri += num;
        }
    }

    public synchronized void update_ruby(long num) {
        if ((num + this.ruby) <= 2_000_000_000L) {
            this.ruby += num;
        }
    }

    public void send_inventory(Player p, boolean b) throws IOException {
        Message m = new Message(-52);
        m.writer().writeByte(19);
        m.writer().writeByte(0);
        m.writer().writeByte(8);
        m.writer().writeByte(p.clan.list_it.size());
        for (int i = 0; i < p.clan.list_it.size(); i++) {
            m.writer().writeShort(p.clan.list_it.get(i).id);
            m.writer().writeShort(p.clan.list_it.get(i).quant);
        }
        if (b) {
            p.list_msg_cache.add(m);
        } else {
            p.conn.addmsg(m);
        }
        m.cleanup();
    }

    public boolean check_buff(int i) {
        for (int j = 0; j < this.buff.size(); j++) {
            if (this.buff.get(j).id == i && this.buff.get(j).time > System.currentTimeMillis()) {
                return true;
            }
        }
        return false;
    }

    public void chat_on_board(short id, String name, String s, int typeChat) throws IOException {
        Clan_chat chat = new Clan_chat();
        chat.idMem = id;
        chat.name = name;
        chat.str = s;
        chat.time = System.currentTimeMillis();
        chat.typeChat = (byte) typeChat;
        this.add_chat(chat);
        //
        this.send_chat(chat, null);
    }
}
