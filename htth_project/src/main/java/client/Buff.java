package client;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import core.Service;
import core.Util;
import io.Message;
import map.Mob;
import template.EffTemplate;
import template.Skill_info;

/**
 *
 * @author Truongbk
 */
public class Buff {
    public static void process(Player p, Message m2) throws IOException {
        short id = m2.reader().readShort();
        byte cat = m2.reader().readByte();
        byte size = m2.reader().readByte();
        short[] id2 = null;
        if (size > 0) {
            id2 = new short[size];
            for (int i = 0; i < size; i++) {
                id2[i] = m2.reader().readShort();
            }
        }
        int time_buff = 0;
        List<Integer> list_id = new ArrayList<>();
        List<Integer> list_par = new ArrayList<>();
        Skill_info sk_info = null;
        for (int i = 0; i < p.skill_point.size(); i++) {
            if (p.skill_point.get(i).temp.ID == id && p.skill_point.get(i).temp.Lv_RQ > 0) {
                sk_info = p.skill_point.get(i);
                for (int j = 0; j < sk_info.temp.op.size(); j++) {
                    switch (sk_info.temp.op.get(j).id) {
                        case 32: {
                            time_buff = sk_info.temp.op.get(j).getParam() * 100;
                            break;
                        }
                        default: {
                            if (sk_info.temp.op.get(j).id != 25
                                    && (sk_info.temp.op.get(j).id < 28
                                            || sk_info.temp.op.get(j).id == 53)
                                    && sk_info.temp.op.get(j).id >= 0) {
                                list_id.add(sk_info.temp.op.get(j).id);
                                list_par.add(sk_info.temp.op.get(j).getParam());
                            }
                            break;
                        }
                    }
                }
                break;
            }
        }
        // Override thời gian và cooldown cho Haki Quan Sát, Haki Vũ Trang và Haki Bá
        // Vương
        if (sk_info != null && (sk_info.temp.indexSkillInServer >= 900 && sk_info.temp.indexSkillInServer <= 902)) {
            if (time_buff <= 0) {
                time_buff = 20000; // 20 giây
            }
            if (sk_info.temp.indexSkillInServer == 902) {
                sk_info.temp.timeDelay = 90000; // 90 giây cooldown
            } else {
                sk_info.temp.timeDelay = 60000; // 60 giây cooldown
            }
        }
        if (sk_info == null) {
            return;
        }
        if (p.time_sk[sk_info.temp.ID] > System.currentTimeMillis()) {
            return;
        }
        if (time_buff > 0 && cat == 0 && size == 1) {
            if ((sk_info.temp.indexSkillInServer >= 900 && sk_info.temp.indexSkillInServer <= 902)
                    || (sk_info.temp.ID >= 1010 && sk_info.temp.ID <= 1014)) {
                p.time_sk[sk_info.temp.ID] = System.currentTimeMillis() + sk_info.temp.timeDelay;
            } else {
                p.time_sk[sk_info.temp.ID] = System.currentTimeMillis() + sk_info.temp.timeDelay
                        - ((sk_info.temp.timeDelay * p.body.get_agility(true)) / 1_000);
            }
            Service.use_potion(p, 1, -sk_info.temp.manaLost);
            Service.pet(p, p, false);
            Service.UpdateInfoMaincharInfo(p);
            if (sk_info.temp.indexSkillInServer == 900) {
                Service.send_eff_haki(p, (short) 21, time_buff); // Haki Quan Sát (eff 21)
            } else if (sk_info.temp.indexSkillInServer == 901) {
                Service.send_eff_haki(p, (short) 18, time_buff); // Haki Vũ Trang (eff 18)
            } else if (sk_info.temp.indexSkillInServer == 902) {
                Service.send_eff_haki(p, (short) 26, time_buff); // Haki Bá Vương (eff 26)
                apply_haki_bavuong_stun(p, 5, 250, 5000);
            } else if (sk_info.temp.indexSkillInServer == 912) {
                Service.send_eff_haki(p, (short) 912, time_buff); // Đệm Thịt Hộ Thể (eff 912)
                int healHp = (int) ((long) p.body.get_hp_max(true) * 30 / 100);
                Service.use_potion(p, 0, healHp);
            } else if (sk_info.temp.indexSkillInServer == 916) {
                Service.send_eff_haki(p, (short) 916, time_buff); // Curtain Khiên Phẫu Thuật (eff 916)
                int healHp = (int) ((long) p.body.get_hp_max(true) * 30 / 100);
                Service.use_potion(p, 0, healHp);
            }
            // FIX: get_eff_skill() không có mapping cho 3 skill Haki
            // (indexSkillInServer 900/901/902), nên nó fallback về
            // temp.getTypeEffSkill() - giá trị cấu hình trong DB skill_info,
            // nếu chưa set đúng sẽ trả về 0/sai. Gói tin opcode 20 này là gói
            // gửi RIÊNG cho chính người bấm skill (p.conn), quyết định client
            // của chính họ có vẽ hiệu ứng lên nhân vật mình hay không - độc lập
            // với gói opcode 74 (send_eff_haki) mà người chơi khác nhận được.
            // Ghi đè trực tiếp đúng effId (21/18/26/912/916) để đảm bảo người bấm
            // skill luôn tự thấy hiệu ứng, không phụ thuộc cấu hình DB.
            short effSkillToSend = sk_info.get_eff_skill();
            if (sk_info.temp.indexSkillInServer == 900) {
                effSkillToSend = 21; // Haki Quan Sát
            } else if (sk_info.temp.indexSkillInServer == 901) {
                effSkillToSend = 18; // Haki Vũ Trang
            } else if (sk_info.temp.indexSkillInServer == 902) {
                effSkillToSend = 26; // Haki Bá Vương
            } else if (sk_info.temp.indexSkillInServer == 912) {
                effSkillToSend = 912; // Đệm Thịt Hộ Thể
            } else if (sk_info.temp.indexSkillInServer == 916) {
                effSkillToSend = 916; // Curtain Khiên Phẫu Thuật
            }
            Message m = new Message(20);
            m.writer().writeByte(1);
            m.writer().writeShort(id);
            m.writer().writeShort(p.index_map);
            m.writer().writeByte(0);
            m.writer().writeShort(sk_info.temp.idIcon);
            m.writer().writeShort(effSkillToSend);
            m.writer().writeInt(time_buff);
            m.writer().writeByte(0);
            m.writer().writeByte(1);
            m.writer().writeShort(p.index_map);
            m.writer().writeByte(list_id.size());
            for (int i = 0; i < list_id.size(); i++) {
                m.writer().writeByte(list_id.get(i));
                m.writer().writeShort(list_par.get(i));
                EffTemplate eff = p.get_eff(list_id.get(i));
                if (eff == null) {
                    p.add_new_eff((list_id.get(i) + 100), list_par.get(i), time_buff);
                } else {
                    eff.time += time_buff;
                }
            }
            switch (id) {
                case 2009: {
                    m.writer().writeByte(3);
                    m.writer().writeShort(308);
                    m.writer().writeShort(309);
                    m.writer().writeShort(310);
                    //
                    Message m12 = new Message(20);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(id);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(0);
                    m12.writer().writeShort(sk_info.temp.idIcon);
                    m12.writer().writeShort(sk_info.get_eff_skill());
                    m12.writer().writeInt(time_buff);
                    m12.writer().writeByte(0);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(3);
                    m12.writer().writeShort(308);
                    m12.writer().writeShort(309);
                    m12.writer().writeShort(310);
                    for (int i = 0; i < p.map.players.size(); i++) {
                        if (!p.map.players.get(i).equals(p)) {
                            p.map.players.get(i).conn.addmsg(m12);
                        }
                    }
                    m12.cleanup();
                    break;
                }
                case 2016: {
                    m.writer().writeByte(3);
                    m.writer().writeShort(341);
                    m.writer().writeShort(342);
                    m.writer().writeShort(343);
                    //
                    Message m12 = new Message(20);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(id);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(0);
                    m12.writer().writeShort(sk_info.temp.idIcon);
                    m12.writer().writeShort(sk_info.get_eff_skill());
                    m12.writer().writeInt(time_buff);
                    m12.writer().writeByte(0);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(3);
                    m12.writer().writeShort(341);
                    m12.writer().writeShort(342);
                    m12.writer().writeShort(343);
                    for (int i = 0; i < p.map.players.size(); i++) {
                        if (!p.map.players.get(i).equals(p)) {
                            p.map.players.get(i).conn.addmsg(m12);
                        }
                    }
                    m12.cleanup();
                    break;
                }
                case 2037: { // chim ung
                    m.writer().writeByte(3);
                    m.writer().writeShort(490);
                    m.writer().writeShort(491);
                    m.writer().writeShort(492);
                    p.add_new_eff(6, 1, time_buff);
                    //
                    Message m12 = new Message(20);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(id);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(0);
                    m12.writer().writeShort(sk_info.temp.idIcon);
                    m12.writer().writeShort(sk_info.get_eff_skill());
                    m12.writer().writeInt(time_buff);
                    m12.writer().writeByte(0);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(3);
                    m12.writer().writeShort(490);
                    m12.writer().writeShort(491);
                    m12.writer().writeShort(492);
                    for (int i = 0; i < p.map.players.size(); i++) {
                        if (!p.map.players.get(i).equals(p)) {
                            p.map.players.get(i).conn.addmsg(m12);
                        }
                    }
                    m12.cleanup();
                    break;
                }
                case 2040: { // bao dom
                    m.writer().writeByte(3);
                    m.writer().writeShort(659);
                    m.writer().writeShort(660);
                    m.writer().writeShort(661);
                    p.add_new_eff(6, 1, time_buff);
                    //
                    Message m12 = new Message(20);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(id);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(0);
                    m12.writer().writeShort(sk_info.temp.idIcon);
                    m12.writer().writeShort(sk_info.get_eff_skill());
                    m12.writer().writeInt(time_buff);
                    m12.writer().writeByte(0);
                    m12.writer().writeByte(1);
                    m12.writer().writeShort(p.index_map);
                    m12.writer().writeByte(3);
                    m12.writer().writeShort(659);
                    m12.writer().writeShort(660);
                    m12.writer().writeShort(661);
                    for (int i = 0; i < p.map.players.size(); i++) {
                        if (!p.map.players.get(i).equals(p)) {
                            p.map.players.get(i).conn.addmsg(m12);
                        }
                    }
                    m12.cleanup();
                    break;
                }
                default: {
                    m.writer().writeByte(0);
                    break;
                }
            }
            p.conn.addmsg(m);
            m.cleanup();
            //
            p.send_skill();
            p.update_info_to_all();
            if (id != 2009 && id != 2016 && id != 2037 && id != 2040 && sk_info.temp.typeSkill == 2
                    && sk_info.temp.nTarget > 1 && p.party != null) {
                int num_party_eff = 1;
                for (int j = 0; j < p.party.list.size(); j++) {
                    Player p0 = p.party.list.get(j);
                    if (p0.conn != null && !p0.name.equals(p.name) && p0.map.equals(p.map)) {
                        m = new Message(20);
                        m.writer().writeByte(0);
                        m.writer().writeShort(id);
                        m.writer().writeShort(p0.index_map);
                        m.writer().writeByte(0);
                        m.writer().writeShort(sk_info.temp.idIcon);
                        m.writer().writeShort(sk_info.get_eff_skill());
                        m.writer().writeInt(time_buff);
                        m.writer().writeByte(0);
                        m.writer().writeByte(1);
                        m.writer().writeShort(p0.index_map);
                        m.writer().writeByte(list_id.size());
                        for (int i = 0; i < list_id.size(); i++) {
                            m.writer().writeByte(list_id.get(i));
                            m.writer().writeShort(list_par.get(i));
                            EffTemplate eff = p0.get_eff(list_id.get(i));
                            if (eff == null) {
                                p0.add_new_eff((list_id.get(i) + 100), list_par.get(i), time_buff);
                            }
                        }
                        m.writer().writeByte(0);
                        p0.conn.addmsg(m);
                        m.cleanup();
                        p0.update_info_to_all();
                        num_party_eff++;
                    }
                    if (num_party_eff >= sk_info.temp.nTarget) {
                        break;
                    }
                }
            }
            //
            switch (id) {
                case 1010: { // luffy
                    p.add_new_eff(11, 1, time_buff);
                    break;
                }
                case 1011: { // zoro
                    p.add_new_eff(12, 1, time_buff);
                    break;
                }
                case 1012: { // sanji
                    p.add_new_eff(13, 1, time_buff);
                    Service.Main_char_Info(p);
                    break;
                }
                case 1013: { // nami
                    p.add_new_eff(14, 1, time_buff);
                    break;
                }
                case 1014: { // usop
                    if (id2 != null) {
                        Player p2 = null;
                        for (int i = 0; i < p.map.players.size(); i++) {
                            if (p.map.players.get(i).index_map != p.index_map
                                    && (p2 == null || (Math.abs(p.map.players.get(i).x - p.x) < Math
                                            .abs(p.map.players.get(i).x - p2.x)
                                            && Math.abs(p.map.players.get(i).y - p.y) < Math
                                                    .abs(p.map.players.get(i).y - p2.y)))) {
                                p2 = p.map.players.get(i);
                            }
                        }
                        if (p2 != null) {
                            if ((p.typePirate == 0 && p2.typePirate == 2)
                                    || (p.typePirate == 2 && p2.typePirate == 0)
                                    || (p.typePirate == 1 && p2.typePirate == 2)
                                    || (p.typePirate == 2 && p2.typePirate == 1)
                                    || (p.type_pk == 14 && p2.type_pk == 15)
                                    || (p.type_pk == 15 && p2.type_pk == 14)
                                    || (p.typePirate == 2 && p2.typePirate == 2) || (p.type_pk == 0)
                                    || (p2.type_pk == 1) || (p.type_pk == 3 && p2.type_pk == 3)
                                    || (p2.type_pk == 0)
                                    || (p.type_pk == 3 && p2.type_pk >= 4 && p2.type_pk <= 8)
                                    || (p2.type_pk == 3 && p.type_pk >= 4 && p.type_pk <= 8)
                                    || (p.type_pk >= 4 && p.type_pk <= 8 && p2.type_pk >= 4
                                            && p2.type_pk <= 8 && p.type_pk != p2.type_pk)) {
                                if (Math.abs(p2.x - p.x) < 200 && Math.abs(p2.y - p.y) < 200) {
                                    m = new Message(-15);
                                    m.writer().writeByte(4);
                                    m.writer().writeShort(p.index_map);
                                    m.writer().writeByte(0);
                                    m.writer().writeShort(500);
                                    //
                                    m.writer().writeShort(p2.index_map);
                                    m.writer().writeByte(0);
                                    //
                                    p.map.send_msg_all_p(m, p, true);
                                    m.cleanup();
                                    //
                                    Buff.send_choang(p, p2, 4000);
                                }
                            }
                        }
                    }
                    break;
                }
                case 2059: { // boc pha
                    EffTemplate eff = p.get_eff(18);
                    if (eff == null) {
                        if (3 > Util.random(120)) {
                            p.add_new_eff(18, 280, time_buff);
                        } else {
                            p.add_new_eff(18, 180, time_buff);
                        }
                    } else {
                        eff.time += time_buff;
                        if (3 > Util.random(120)) {
                            eff.param = 280;
                        } else {
                            eff.param = 180;
                        }
                    }
                    break;
                }
                case 2056: { // add eff skill buff trai bong toi
                    Message m2055 = new Message(74);
                    m2055.writer().writeByte(1);
                    m2055.writer().writeShort(p.index_map);
                    m2055.writer().writeShort(3);
                    m2055.writer().writeInt(time_buff);
                    m2055.writer().writeByte(0);
                    m2055.writer().writeByte(10);
                    p.map.send_msg_all_p(m2055, null, true);
                    m2055.cleanup();
                    break;
                }
            }
        }
    }

    public static void send_choang(Player p, Player p2, int time) throws IOException {
        Message m = new Message(28);
        m.writer().writeShort(p2.index_map);
        m.writer().writeByte(0);
        m.writer().writeInt(p2.hp);
        m.writer().writeInt(p2.body.get_hp_max(true));
        m.writer().writeShort(1);
        m.writer().writeShort(time / 100);
        p.map.send_msg_all_p(m, p, true);
        m.cleanup();
    }

    public static void send_choang_mob(Player p, Mob mob, int time) throws IOException {
        mob.time_skill = System.currentTimeMillis() + time;
        Message m = new Message(28);
        m.writer().writeShort(mob.index);
        m.writer().writeByte(1);
        m.writer().writeInt(mob.hp);
        m.writer().writeInt(mob.hp_max);
        m.writer().writeShort(1);
        m.writer().writeShort(time / 100);
        p.map.send_msg_all_p(m, p, true);
        m.cleanup();
    }

    public static void apply_haki_bavuong_stun(Player p, int maxTargets, int range, int timeStun) throws IOException {
        if (p == null || p.map == null) {
            return;
        }
        int targetCount = 0;

        // 1. Quét người chơi khác trong tầm
        if (p.map.players != null) {
            for (int i = 0; i < p.map.players.size(); i++) {
                if (targetCount >= maxTargets) {
                    break;
                }
                Player p2 = p.map.players.get(i);
                if (p2 == null || p2.equals(p) || p2.isdie) {
                    continue;
                }
                if (Math.abs(p2.x - p.x) <= range && Math.abs(p2.y - p.y) <= range) {
                    boolean canPK = (p.map.template.id == 2026)
                            || (p.typePirate == 0 && p2.typePirate == 2)
                            || (p.typePirate == 2 && p2.typePirate == 0)
                            || (p.typePirate == 1 && p2.typePirate == 2)
                            || (p.typePirate == 2 && p2.typePirate == 1)
                            || (p.type_pk == 14 && p2.type_pk == 15)
                            || (p.type_pk == 15 && p2.type_pk == 14)
                            || (p.typePirate == 2 && p2.typePirate == 2) || (p.type_pk == 0)
                            || (p2.type_pk == 1) || (p.type_pk == 3 && p2.type_pk == 3)
                            || (p2.type_pk == 0)
                            || (p.type_pk == 3 && p2.type_pk >= 4 && p2.type_pk <= 8)
                            || (p2.type_pk == 3 && p.type_pk >= 4 && p.type_pk <= 8)
                            || (p.type_pk >= 4 && p.type_pk <= 8 && p2.type_pk >= 4
                                    && p2.type_pk <= 8 && p.type_pk != p2.type_pk);
                    if (canPK) {
                        EffTemplate eff = p2.get_eff(205);
                        if (eff == null) {
                            p2.add_new_eff(205, 1, timeStun);
                        } else {
                            eff.time = System.currentTimeMillis() + timeStun;
                        }
                        send_choang(p, p2, timeStun);
                        targetCount++;
                    }
                }
            }
        }

        // 2. Quét quái trong map thường
        if (targetCount < maxTargets && p.map.list_mob != null) {
            for (int i = 0; i < p.map.list_mob.length; i++) {
                if (targetCount >= maxTargets) {
                    break;
                }
                Mob mob = Mob.ENTRYS.get(p.map.list_mob[i]);
                if (mob != null && !mob.isdie && Math.abs(mob.x - p.x) <= range && Math.abs(mob.y - p.y) <= range) {
                    send_choang_mob(p, mob, timeStun);
                    targetCount++;
                }
            }
        }

        // 3. Quét quái trong dungeon / boss hunt / little garden nếu có
        if (targetCount < maxTargets && p.map.map_dungeon != null && p.map.map_dungeon.mobs != null) {
            for (int i = 0; i < p.map.map_dungeon.mobs.size(); i++) {
                if (targetCount >= maxTargets) {
                    break;
                }
                Mob mob = p.map.map_dungeon.mobs.get(i);
                if (mob != null && !mob.isdie && Math.abs(mob.x - p.x) <= range && Math.abs(mob.y - p.y) <= range) {
                    send_choang_mob(p, mob, timeStun);
                    targetCount++;
                }
            }
        }
        if (targetCount < maxTargets && p.dungeon != null && p.dungeon.mobs != null) {
            for (int i = 0; i < p.dungeon.mobs.size(); i++) {
                if (targetCount >= maxTargets) {
                    break;
                }
                Mob mob = p.dungeon.mobs.get(i);
                if (mob != null && !mob.isdie && Math.abs(mob.x - p.x) <= range && Math.abs(mob.y - p.y) <= range) {
                    send_choang_mob(p, mob, timeStun);
                    targetCount++;
                }
            }
        }
        if (targetCount < maxTargets && p.map.map_little_garden != null && p.map.map_little_garden.mobs != null) {
            for (int i = 0; i < p.map.map_little_garden.mobs.size(); i++) {
                if (targetCount >= maxTargets) {
                    break;
                }
                Mob mob = p.map.map_little_garden.mobs.get(i);
                if (mob != null && !mob.isdie && Math.abs(mob.x - p.x) <= range && Math.abs(mob.y - p.y) <= range) {
                    send_choang_mob(p, mob, timeStun);
                    targetCount++;
                }
            }
        }
        if (targetCount < maxTargets && p.map.map_bossHunt != null && p.map.map_bossHunt.mobs != null) {
            for (int i = 0; i < p.map.map_bossHunt.mobs.size(); i++) {
                if (targetCount >= maxTargets) {
                    break;
                }
                Mob mob = p.map.map_bossHunt.mobs.get(i);
                if (mob != null && !mob.isdie && Math.abs(mob.x - p.x) <= range && Math.abs(mob.y - p.y) <= range) {
                    send_choang_mob(p, mob, timeStun);
                    targetCount++;
                }
            }
        }
    }
}