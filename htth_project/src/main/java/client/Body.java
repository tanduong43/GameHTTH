package client;

import event.LucThuc;
import template.*;
import java.util.List;
/**
 *
 * @author Truongbk
 */
public class Body {
    public static String[] NameAttribute =
            new String[] {"Sức mạnh", "Phòng thủ", "Thể lực", "Tinh thần", "Nhanh nhẹn"};
    public static byte[][] Id = new byte[][] { //
            new byte[] {1, 13, 10}, // 1
            new byte[] {4, 26, 27}, // 2
            new byte[] {15, 23}, // 3
            new byte[] {16, 11, 14}, // 4
            new byte[] {25, 12}}; // 5
    // point 1
    public static int[] Point1_Template_atk;
    public static int[] Point1_Template_crit;
    public static int[] Point1_Template_pierce;
    // point 2
    public static int[] Point2_Template_def;
    public static int[] Point2_Template_resist_physical;
    public static int[] Point2_Template_resist_magic;
    // point 3
    public static int[] Point3_Template_hp;
    public static int[] Point3_Template_hp_potion;
    // point 4
    public static int[] Point4_Template_mp;
    public static int[] Point4_Template_dame_crit;
    // point 5
    public static int[] Point5_Template_cooldown;
    public static int[] Point5_Template_miss;
    static {
        load_point_1();
        load_point_2();
        load_point_3();
        load_point_4();
        load_point_5();
    }
    private final Player p;

    public Body(Player p) {
        this.p = p;
    }

    private int total_param_item(int id, boolean have_eff) {
        int par = 0;
        if (id == 46 && p.item.it_body[0] != null && p.item.it_body[0].levelup > 10) {
            par += (50 * (p.item.it_body[0].levelup - 10));
        } else if (id == 53 && p.item.it_body[2] != null && p.item.it_body[2].levelup > 10) {
            par += (30 * (p.item.it_body[2].levelup - 10));
        } else if (id == 56) {
            if (p.item.it_body[1] != null && p.item.it_body[1].levelup > 10) {
                par += (100 * (p.item.it_body[1].levelup - 10));
            }
            if (p.item.it_body[3] != null && p.item.it_body[3].levelup > 10) {
                par += (100 * (p.item.it_body[3].levelup - 10));
            }
            if (p.item.it_body[5] != null && p.item.it_body[5].levelup > 10) {
                par += (100 * (p.item.it_body[5].levelup - 10));
            }
        } else if (id == 47 && p.item.it_body[4] != null && p.item.it_body[4].levelup > 10) {
            par += (20 * (p.item.it_body[4].levelup - 10));
        }
        //
        for (int j = 0; j < p.item.it_body.length; j++) {
            if (p.item.it_body[j] != null) {
                for (int i = 0; i < p.item.it_body[j].option_item.size(); i++) {
                    if (p.item.it_body[j].option_item.get(i).id == id) {
                        par += p.item.it_body[j].option_item.get(i).getParam(
                                p.item.it_body[j].template.typeEquip, p.item.it_body[j].levelup,
                                p.item.it_body[j].isHoanMy);
                    }
                }
                for (int i = 0; i < p.item.it_body[j].option_item_2.size(); i++) {
                    if (p.item.it_body[j].option_item_2.get(i).id == id) {
                        par += p.item.it_body[j].option_item_2.get(i).getParam();
                    }
                }
            }
        }
        for (int i = 0; i < p.fashion.size(); i++) {
            ItemFashionP2 temp = p.fashion.get(i);
            if (temp.is_use) {
                ItemFashion tempF = ItemFashion.get_item(temp.id);
                if (tempF != null) {
                    for (int j = 0; j < tempF.op.size(); j++) {
                        if (tempF.op.get(j).id == id) {
                            int op_value = tempF.op.get(j).getParam();
                            int percent = 0;
                            for (int k = 1; k <= temp.level; k++) {
                                if (k % 3 == 0) {
                                    percent += 10;
                                } else {
                                    percent += 3;
                                }
                            }
                            if (ItemOptionTemplate.ENTRYS.get(id).percent == 1) {
                                op_value += percent;
                            }
                            par += op_value;
                        }
                    }
                    break;
                }
            }
        }
        for (int i = 0; i < p.skill_point.size(); i++) {
            Skill_info temp = p.skill_point.get(i);
            if (temp.temp.Lv_RQ > 0 && (temp.temp.typeSkill == 3)) {
                for (int j = 0; j < temp.temp.op.size(); j++) {
                    if (id != 25 && temp.temp.op.get(j).id == id) {
                        par += temp.temp.op.get(j).getParam();
                    }
                }
            }
        }
        if (have_eff) {
            EffTemplate temp = p.get_eff(id + 100);
            if (temp != null) {
                par += temp.param;
            }
            //
            if (p.party != null) {
                List<Option> op_select = p.party.get_list_buff_now(p);
                for (int i = 0; i < op_select.size(); i++) {
                    if (op_select.get(i).id == id) {
                        par += op_select.get(i).getParam();
                        break;
                    }
                }
            }
        }
        if (id == 56 && p.item.it_heart != null) {
            for (int i = 0; i < p.item.it_heart.option_item.size(); i++) {
                if (p.item.it_heart.option_item.get(i).id == id) {
                    par += p.item.it_heart.option_item.get(i).getParam(
                            p.item.it_heart.template.typeEquip, p.item.it_heart.levelup, 0);
                }
            }
        }
        int hair = p.get_hair();
        if (hair == 772) {
            switch (id) {
                case 17: {
                    par += 50;
                    break;
                }
                case 12: {
                    par += 30;
                    break;
                }
                case 19: {
                    par += 20;
                    break;
                }
            }
        }
        // da hanh trinh
        switch (id) {
            case 67: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 493) {
                        par += 20;
                    }
                }
                break;
            }
            case 68: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 494) {
                        par += 20;
                    }
                }
                break;
            }
            case 56: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 495) {
                        par += 20;
                    }
                }
                break;
            }
            case 18: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 496) {
                        par += 20;
                    }
                }
                break;
            }
            case 23: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 497) {
                        par += 300;
                    }
                }
                break;
            }
            case 24: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 498) {
                        par += 100;
                    }
                }
                break;
            }
            case 69: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 499) {
                        par += 20;
                    }
                }
                break;
            }
            case 49: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 500) {
                        par += 20;
                    }
                }
                break;
            }
            case 51: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 501) {
                        par += 20;
                    }
                }
                break;
            }
            case 50: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 502) {
                        par += 20;
                    }
                }
                break;
            }
            case 52: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 503) {
                        par += 20;
                    }
                }
                break;
            }
            case 63: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 504) {
                        par += 20;
                    }
                }
                break;
            }
            case 80: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 505) {
                        par += 50;
                    }
                }
                break;
            }
            case 71: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 506) {
                        par += 50;
                    }
                }
                break;
            }
            case 70: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 507) {
                        par += 50;
                    }
                }
                break;
            }
            case 5: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 508) {
                        par += 2;
                    }
                }
                break;
            }
            case 6: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 509) {
                        par += 2;
                    }
                }
                break;
            }
            case 7: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 510) {
                        par += 2;
                    }
                }
                break;
            }
            case 8: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 511) {
                        par += 2;
                    }
                }
                break;
            }
            case 9: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 512) {
                        par += 2;
                    }
                }
                break;
            }
            case 72: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 513) {
                        par += 50;
                    }
                }
                break;
            }
            case 48: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 514) {
                        par += 10;
                    }
                }
                break;
            }
            case 40: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 515) {
                        par += 1;
                    }
                }
                break;
            }
            case 75: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 516) {
                        par += 10;
                    }
                }
                break;
            }
            case 76: {
                for (int i = 0; i < p.daHanhTrinh.size(); i++) {
                    if (p.daHanhTrinh.get(i).quant == 1 && p.daHanhTrinh.get(i).id == 517) {
                        par += 10;
                    }
                }
                break;
            }
        }
        EffTemplate effZombie = p.get_eff(21);
        if (par > 1 && effZombie != null && effZombie.param == id) {
            par /= 2;
        }for (int i = 0; i < p.lucthuc[0]; i++) {
            for (int j = 0; j < LucThuc.op[i].length; j++) {
                if (LucThuc.op[i][j] == id) {
                    par += LucThuc.par[i][j];
                }
            }
        }
        return par;
    }
        

    public int get_total_point(int type) {
        int param = 0;
        switch (type) {
            case 1: {
                param += p.point1 + get_point_plus(1);
                break;
            }
            case 2: {
                param += p.point2 + get_point_plus(2);
                break;
            }
            case 3: {
                param += p.point3 + get_point_plus(3);
                break;
            }
            case 4: {
                param += p.point4 + get_point_plus(4);
                break;
            }
            case 5: {
                param += p.point5 + get_point_plus(5);
                break;
            }
        }
        // param = (param * (p.level + 100)) / 100;
        if (type != 3 && param > 10 && p.get_eff(21) != null) {
            param = (param * 8) / 10;
        }
        return param;
    }

    public int get_dame(boolean have_eff) {
        Skill_info sk_temp = p.get_skill_temp(0);
        if (sk_temp == null) {
            return 0;
        }
        int dame = sk_temp.temp.damage;
        int percent = get_dame_percent(have_eff);
        //
        dame = (dame * percent) / 1000;
        dame += sk_temp.get_dame(p);
        
        return dame;
    }

    public int get_dame_percent(boolean have_eff) {
        int par = total_param_item(1, have_eff);
        par += Body.Point1_Template_atk[get_total_point(1) - 1];
        par += ((p.doriki[0] - 1) * 5 + p.doriki[1]) * 100;
        par += (p.sucmanhvatly * 20);
        return par;
    }

    public int get_def(boolean have_eff) {
        int def = this.get_total_point(2);
        def += total_param_item(3, have_eff);
        def += (p.sucmanhvatly * 20);
        return def;
    }

    public int get_def_percent(boolean have_eff) {
        int par = total_param_item(4, have_eff);
        par += Body.Point2_Template_def[this.get_total_point(2) - 1];
        int percent = total_param_item(80, have_eff);
        par += ((p.doriki[0] - 1) * 5 + p.doriki[1]) * 100; //cap doriki
        par = (par * (1000 + percent)) / 1000;
        return par;
    }

    public int get_hp_max(boolean have_eff) {
        long hp = Body.Point3_Template_hp[this.get_total_point(3) - 1];
        hp += total_param_item(15, have_eff);
        
        int percent = total_param_item(17, have_eff);
        EffTemplate eff = p.get_eff(4);
        if (eff != null) {
            percent += eff.param;
        }
        percent += ((p.doriki[0] - 1) * 50 + p.doriki[1]) * 100;
        hp = (hp * (1000L + percent)) / 1000L;
        hp = (hp * (1000L + total_param_item(56, true))) / 1000L; // % hp cuoi
        hp += (p.sucmanhvatly * 2000);
        if (hp > 2_000_000_000L) {
            hp = 2_000_000_000L;
        }
        return (int) hp;
    }

    public int get_mp_max(boolean have_eff) {
        int mp = Body.Point4_Template_mp[this.get_total_point(4) - 1];
        
        mp += total_param_item(16, have_eff);
        mp += ((p.doriki[0] - 1) * 50 + p.doriki[1]) * 100;
        mp = (mp * (1000 + total_param_item(18, have_eff))) / 1000;
        mp += (p.sucmanhvatly * 2000);
        return mp;
    }

    public int get_agility(boolean have_eff) {
        int agi = total_param_item(25, have_eff);
        if (this.get_total_point(5) > 0) {
            agi += Body.Point5_Template_cooldown[this.get_total_point(5) - 1];
        }
        if (agi > 450) {
            agi = 450;
        }
        if (p.get_eff(13) != null) {
            agi = (agi * 15) / 10;
        }
        if (have_eff && p.party != null) {
            List<Option> op_select = p.party.get_list_buff_now(p);
            for (int i = 0; i < op_select.size(); i++) {
                if (op_select.get(i).id == 25) {
                    agi += op_select.get(i).getParam();
                    break;
                }
            }
        }
        return agi;
    }

    public int view_in4(int b) {
        switch (b) {
            case 0:
                return get_dame(true);
            case 1:
                return get_dame_percent(false);
            case 2:
                return get_dame_ap();
            case 3:
                return get_def(true);
            case 4:
                return get_def_percent(false);
            case 15:
                return total_param_item(15, false)
                        + Body.Point3_Template_hp[this.get_total_point(3) - 1];
            case 16:
                return total_param_item(16, false)
                        + Body.Point4_Template_mp[this.get_total_point(4) - 1];
            case 25:
                return get_agility(false);
            case 26:
                return get_dame_resist(false);
            case 27:
                return get_dame_resist_ap(false);
            case 17:
                return total_param_item(17, false);
            case 18:
                return total_param_item(18, false);
            case 13:
                return get_pierce(false);
            case 10:
                return get_crit(false);
            case 12:
                return get_miss(false);
            case 14:
                return get_dame_react(false);
            case 23:
                return get_hp_potion_use_percent(false);
            case 24:
                return get_mp_potion_use_percent(false);
            case 19:
                return get_hp_auto_buff(false);
            case 20:
                return get_mp_auto_buff(false);
            case 21:
                return get_hp_atk_absorb(false);
            case 22:
                return get_mp_atk_absorb(false);
            case 11:
                return get_multi_dame_when_crit(false);
            case 53:
                return get_dame_skip(false);
            case 63:
                return get_dame_skip_reduce();
            case 49:
                return get_crit_reduce();
            case 50:
                return get_pierce_reduce();
            case 51:
                return get_miss_reduce();
            case 52:
                return get_dame_react_reduce();
            case 55:
                return get_TuChoiTuThan();
            case 57:
                return get_true_dame();
            case 58:
            case 59:
                return get_HapThu_Hp();
            case 46:
                return get_percent_final_dame();
            case 67:
                return get_xp_more();
            case 68:
                return get_xp_skill_more();
            case 69:
                return get_multi_dame_decrease();
            case 72:
                return get_percent_beri_train();
        }
        return total_param_item(b, false);
    }

    public int get_param_by_id(int id) {
        return total_param_item(id, true);
    }

    public int get_multi_dame_decrease() {
        return total_param_item(69, false);
    }

    public int get_dame_skip_reduce() {
        return total_param_item(63, true);
    }

    public int get_TuChoiTuThan() {
        int result = total_param_item(55, true);
        int index_full_set = p.get_index_full_set();
        if (index_full_set > 3) {
            result += 100;
        }
        return result;
    }

    public int get_mp_atk_absorb(boolean have_eff) {
        return total_param_item(22, have_eff);
    }

    public int get_hp_atk_absorb(boolean have_eff) {
        return total_param_item(21, have_eff);
    }

    public int get_dame_react(boolean have_eff) {
        int par = total_param_item(14, have_eff);
        par += Body.Point2_Template_resist_magic[this.get_total_point(4) - 1];
        if (par > 750) {
            int save = par - 750;
            par = 750 + (save * 2) / 10;
        }
        return par;
    }

    public int get_pierce(boolean have_eff) {
        int par = total_param_item(13, have_eff);
        par += Body.Point1_Template_pierce[this.get_total_point(1) - 1];
        par += (p.sucmanhvatly * 20);
        
        if (par > 750) {
            int save = par - 750;
            par = 750 + (save * 2) / 10;
        }
        return par;
    }

    public int get_miss(boolean have_eff) {
        int par = total_param_item(12, have_eff);
        if (this.get_total_point(5) > 0) {
            par += Body.Point5_Template_miss[this.get_total_point(5) - 1];
        }
        if (par > 750) {
            int save = par - 750;
            par = 750 + (save * 2) / 10;
        }
        return par;
    }

    public int get_crit(boolean have_eff) {
        int par = total_param_item(10, have_eff);
        par += Body.Point1_Template_crit[this.get_total_point(1) - 1];
        par += (p.sucmanhvatly * 20);
        //
        if (par > 750) {
            int save = par - 750;
            par = 750 + (save * 2) / 10;
        }
        return par;
    }

    public int get_multi_dame_when_crit(boolean have_eff) {
        int par = total_param_item(11, have_eff);
        par += Body.Point4_Template_dame_crit[this.get_total_point(4) - 1];
        return par;
    }

    public int get_point_plus(int type) {
        int par = 0;
        switch (type) {
            case 1: {
                par += total_param_item(5, true);
                for (int i = 0; i < p.list_op_thongthao.size(); i++) {
                    if (p.list_op_thongthao.get(i).id == 1) {
                        par += p.list_op_thongthao.get(i).getParam();
                    }
                }
                break;
            }
            case 2: {
                par += total_param_item(6, true);
                for (int i = 0; i < p.list_op_thongthao.size(); i++) {
                    if (p.list_op_thongthao.get(i).id == 4 || p.list_op_thongthao.get(i).id == 26
                            || p.list_op_thongthao.get(i).id == 27) {
                        par += p.list_op_thongthao.get(i).getParam();
                    }
                }
                break;
            }
            case 3: {
                par += total_param_item(7, true);
                for (int i = 0; i < p.list_op_thongthao.size(); i++) {
                    if (p.list_op_thongthao.get(i).id == 15) {
                        par += p.list_op_thongthao.get(i).getParam();
                    }
                }
                break;
            }
            case 4: {
                par += total_param_item(8, true);
                for (int i = 0; i < p.list_op_thongthao.size(); i++) {
                    if (p.list_op_thongthao.get(i).id == 16) {
                        par += p.list_op_thongthao.get(i).getParam();
                    }
                }
                break;
            }
            case 5: {
                par += total_param_item(9, true);
                break;
            }
        }
        if (p.clan != null) {
            par += p.clan.opAttri[type - 1] + Clan.get_point_trungsinh_plus(p.clan);
        }
        if (par > 60) {
            par = 60;
        }
        return par;
    }

    public int get_dame_skip(boolean have_eff) {
        int par = total_param_item(53, have_eff);
        return par;
    }

    public int get_dame_resist(boolean have_eff) {
        int par = total_param_item(26, have_eff);
        par += Body.Point2_Template_resist_physical[this.get_total_point(2) - 1];
        if (p.get_eff(11) != null) { // skill buff luffy
            par *= 2;
        }
        return par;
    }

    public int get_dame_resist_ap(boolean have_eff) {
        int par = total_param_item(27, have_eff);
        par += Body.Point2_Template_resist_magic[this.get_total_point(2) - 1];
        if (p.get_eff(11) != null) { // skill buff luffy
            par *= 2;
        }
        return par;
    }

    public int get_hp_potion_use_percent(boolean have_eff) {
        int par = total_param_item(23, have_eff);
        par += Body.Point3_Template_hp_potion[this.get_total_point(3) - 1];
        return par;
    }

    public int get_mp_potion_use_percent(boolean have_eff) {
        int par = total_param_item(24, have_eff);
        return par;
    }

    public int get_hp_auto_buff(boolean have_eff) {
        return total_param_item(19, have_eff);
    }

    public int get_mp_auto_buff(boolean have_eff) {
        return total_param_item(20, have_eff);
    }

    public int get_dame_ap() {
        return total_param_item(2, true);
    }

    public int get_dame_percent_hp_target() {
        int result = total_param_item(48, true);
        if (result >= 350) {
            result = 350;
        }
        return result;
    }

    public int get_kich_an(int id) {
        int result = 0;
        for (int i = 0; i < p.item.it_body.length; i++) {
            if (p.item.it_body[i] != null && p.item.it_body[i].valueKichAn == id) {
                result++;
            }
        }
        return result;
    }

    public int get_percent_beri_train() {
        int par = total_param_item(72, true);
        for (int i = 0; i < p.fashion.size(); i++) {
            if (p.fashion.get(i).id == 77 && p.fashion.get(i).is_use) {
                par += 300;
                break;
            }
        }
        return par;
    }

    public int get_crit_reduce() {
        int par = total_param_item(49, true);
        int index_full_set = p.get_index_full_set();
        if (index_full_set == 5) {
            par += (80 * 1);
        } else if (index_full_set > 2) {
            par += (50 * 1);
        }
        return par;
    }

    public int get_dame_react_reduce() {
        int par = total_param_item(52, true);
        int index_full_set = p.get_index_full_set();
        if (index_full_set == 5) {
            par += (80 * 1);
        } else if (index_full_set > 1) {
            par += (50 * 1);
        }
        return par;
    }

    public int get_pierce_reduce() {
        int par = total_param_item(50, true);
        int index_full_set = p.get_index_full_set();
        if (index_full_set == 5) {
            par += (80 * 1);
        } else if (index_full_set > 2) {
            par += (50 * 1);
        }
        return par;
    }

    public int get_miss_reduce() {
        int par = total_param_item(51, true);
        int index_full_set = p.get_index_full_set();
        if (index_full_set == 5) {
            par += (80 * 1);
        } else if (index_full_set > 0) {
            par += (50 * 1);
        }
        return par;
    }

    public int get_true_dame() {
        int par = total_param_item(57, true);
        int index_full_set = p.get_index_full_set();
        if (index_full_set == 5) {
            par += 100;
        }
        return par;
    }

    public byte get_level_perfect() {
        byte result = 0;
        for (int i = 0; i < p.item.it_body.length; i++) {
            if (p.item.it_body[i] != null && p.item.it_body[i].isHoanMy == 1
                    && p.item.it_body[i].valueKichAn > -1) {
                result++;
            }
        }
        return result;
    }

    public int get_percent_final_dame() {
        return total_param_item(46, true);
    }

    public int get_xp_more() {
        return total_param_item(67, true);
    }

    public int get_xp_skill_more() {
        return total_param_item(68, true);
    }

    public int get_dame_devil_percent() {
        int par = 100;
        Skill_info sk = p.skill_point.get(0);
        switch (sk.lvdevil) {
            case 1: {
                par += 10;
                break;
            }
            case 2: {
                par += 25;
                break;
            }
            case 3: {
                par += 45;
                break;
            }
            case 4: {
                par += 70;
                break;
            }
            case 5: {
                par += 100;
                break;
            }
        }
        return par;
    }

    public int get_reduce_Eff() {
        return total_param_item(47, true);
    }

    public int get_HapThu_Hp() {
        int par = total_param_item(59, true);
        ItemFashionP2 itF = p.check_fashion(74);
        if (itF != null && itF.is_use) {
            par *= 2;
        }
        return par;
    }

    public long get_def_target_reduce() {
        int par = total_param_item(70, true);
        return par;
    }

    private static void load_point_1() {
        Point1_Template_atk = new int[200];
        Point1_Template_crit = new int[200];
        Point1_Template_pierce = new int[200];
        Point1_Template_atk[0] = 44;
        Point1_Template_atk[1] = 71;
        Point1_Template_atk[2] = 100;
        Point1_Template_atk[3] = 133;
        Point1_Template_atk[4] = 168;
        Point1_Template_atk[5] = 205;
        Point1_Template_atk[6] = 246;
        Point1_Template_atk[7] = 289;
        Point1_Template_atk[8] = 336;
        Point1_Template_atk[9] = 385;
        Point1_Template_atk[10] = 436;
        Point1_Template_atk[11] = 491;
        Point1_Template_atk[12] = 548;
        Point1_Template_atk[13] = 609;
        Point1_Template_atk[14] = 672;
        Point1_Template_atk[15] = 737;
        Point1_Template_atk[16] = 806;
        Point1_Template_atk[17] = 877;
        Point1_Template_atk[18] = 952;
        Point1_Template_atk[19] = 1029;
        Point1_Template_atk[20] = 1108;
        Point1_Template_atk[21] = 1191;
        Point1_Template_atk[22] = 1276;
        Point1_Template_atk[23] = 1365;
        Point1_Template_atk[24] = 1456;
        Point1_Template_atk[25] = 1549;
        Point1_Template_atk[26] = 1646;
        Point1_Template_atk[27] = 1745;
        Point1_Template_atk[28] = 1848;
        Point1_Template_atk[29] = 1953;
        Point1_Template_atk[30] = 2060;
        Point1_Template_atk[31] = 2171;
        Point1_Template_atk[32] = 2284;
        Point1_Template_atk[33] = 2401;
        Point1_Template_atk[34] = 2520;
        Point1_Template_atk[35] = 2641;
        Point1_Template_atk[36] = 2766;
        Point1_Template_atk[37] = 2893;
        Point1_Template_atk[38] = 3024;
        Point1_Template_atk[39] = 3157;
        Point1_Template_atk[40] = 3292;
        Point1_Template_atk[41] = 3431;
        Point1_Template_atk[42] = 3572;
        Point1_Template_atk[43] = 3717;
        Point1_Template_atk[44] = 3864;
        Point1_Template_atk[45] = 4013;
        Point1_Template_atk[46] = 4166;
        Point1_Template_atk[47] = 4321;
        Point1_Template_atk[48] = 4480;
        Point1_Template_atk[49] = 4641;
        Point1_Template_atk[50] = 4804;
        Point1_Template_atk[51] = 4971;
        Point1_Template_atk[52] = 5140;
        Point1_Template_atk[53] = 5313;
        Point1_Template_atk[54] = 5488;
        Point1_Template_atk[55] = 5665;
        Point1_Template_atk[56] = 5846;
        Point1_Template_atk[57] = 6029;
        Point1_Template_atk[58] = 6216;
        Point1_Template_atk[59] = 6405;
        Point1_Template_atk[60] = 6596;
        Point1_Template_atk[61] = 6791;
        Point1_Template_atk[62] = 6988;
        Point1_Template_atk[63] = 7189;
        Point1_Template_atk[64] = 7392;
        Point1_Template_atk[65] = 7597;
        Point1_Template_atk[66] = 7806;
        Point1_Template_atk[67] = 8017;
        Point1_Template_atk[68] = 8232;
        Point1_Template_atk[69] = 8449;
        Point1_Template_atk[70] = 8668;
        Point1_Template_atk[71] = 8891;
        Point1_Template_atk[72] = 9116;
        Point1_Template_atk[73] = 9345;
        Point1_Template_atk[74] = 9576;
        Point1_Template_atk[75] = 9809;
        Point1_Template_atk[76] = 10046;
        Point1_Template_atk[77] = 10285;
        Point1_Template_atk[78] = 10528;
        Point1_Template_atk[79] = 10773;
        Point1_Template_atk[80] = 11020;
        Point1_Template_crit[0] = 0;
        Point1_Template_crit[1] = 0;
        Point1_Template_crit[2] = 0;
        Point1_Template_crit[3] = 0;
        Point1_Template_crit[4] = 0;
        Point1_Template_crit[5] = 0;
        Point1_Template_crit[6] = 0;
        Point1_Template_crit[7] = 0;
        Point1_Template_crit[8] = 0;
        Point1_Template_crit[9] = 0;
        Point1_Template_crit[10] = 0;
        Point1_Template_crit[11] = 0;
        Point1_Template_crit[12] = 0;
        Point1_Template_crit[13] = 0;
        Point1_Template_crit[14] = 0;
        Point1_Template_crit[15] = 0;
        Point1_Template_crit[16] = 0;
        Point1_Template_crit[17] = 0;
        Point1_Template_crit[18] = 0;
        Point1_Template_crit[19] = 10;
        Point1_Template_crit[20] = 11;
        Point1_Template_crit[21] = 12;
        Point1_Template_crit[22] = 13;
        Point1_Template_crit[23] = 14;
        Point1_Template_crit[24] = 15;
        Point1_Template_crit[25] = 16;
        Point1_Template_crit[26] = 17;
        Point1_Template_crit[27] = 18;
        Point1_Template_crit[28] = 19;
        Point1_Template_crit[29] = 20;
        Point1_Template_crit[30] = 21;
        Point1_Template_crit[31] = 22;
        Point1_Template_crit[32] = 23;
        Point1_Template_crit[33] = 24;
        Point1_Template_crit[34] = 25;
        Point1_Template_crit[35] = 26;
        Point1_Template_crit[36] = 27;
        Point1_Template_crit[37] = 28;
        Point1_Template_crit[38] = 29;
        Point1_Template_crit[39] = 30;
        Point1_Template_crit[40] = 31;
        Point1_Template_crit[41] = 32;
        Point1_Template_crit[42] = 33;
        Point1_Template_crit[43] = 34;
        Point1_Template_crit[44] = 35;
        Point1_Template_crit[45] = 36;
        Point1_Template_crit[46] = 37;
        Point1_Template_crit[47] = 38;
        Point1_Template_crit[48] = 39;
        Point1_Template_crit[49] = 40;
        Point1_Template_crit[50] = 41;
        Point1_Template_crit[51] = 42;
        Point1_Template_crit[52] = 43;
        Point1_Template_crit[53] = 44;
        Point1_Template_crit[54] = 45;
        Point1_Template_crit[55] = 46;
        Point1_Template_crit[56] = 47;
        Point1_Template_crit[57] = 48;
        Point1_Template_crit[58] = 49;
        Point1_Template_crit[59] = 50;
        Point1_Template_crit[60] = 51;
        Point1_Template_crit[61] = 52;
        Point1_Template_crit[62] = 53;
        Point1_Template_crit[63] = 54;
        Point1_Template_crit[64] = 55;
        Point1_Template_crit[65] = 56;
        Point1_Template_crit[66] = 57;
        Point1_Template_crit[67] = 58;
        Point1_Template_crit[68] = 59;
        Point1_Template_crit[69] = 60;
        Point1_Template_crit[70] = 61;
        Point1_Template_crit[71] = 62;
        Point1_Template_crit[72] = 63;
        Point1_Template_crit[73] = 64;
        Point1_Template_crit[74] = 65;
        Point1_Template_crit[75] = 66;
        Point1_Template_crit[76] = 67;
        Point1_Template_crit[77] = 68;
        Point1_Template_crit[78] = 69;
        Point1_Template_crit[79] = 70;
        Point1_Template_crit[80] = 71;
        Point1_Template_pierce[0] = 0;
        Point1_Template_pierce[1] = 0;
        Point1_Template_pierce[2] = 0;
        Point1_Template_pierce[3] = 0;
        Point1_Template_pierce[4] = 0;
        Point1_Template_pierce[5] = 0;
        Point1_Template_pierce[6] = 0;
        Point1_Template_pierce[7] = 0;
        Point1_Template_pierce[8] = 0;
        Point1_Template_pierce[9] = 0;
        Point1_Template_pierce[10] = 0;
        Point1_Template_pierce[11] = 0;
        Point1_Template_pierce[12] = 0;
        Point1_Template_pierce[13] = 0;
        Point1_Template_pierce[14] = 0;
        Point1_Template_pierce[15] = 0;
        Point1_Template_pierce[16] = 0;
        Point1_Template_pierce[17] = 0;
        Point1_Template_pierce[18] = 0;
        Point1_Template_pierce[19] = 10;
        Point1_Template_pierce[20] = 12;
        Point1_Template_pierce[21] = 14;
        Point1_Template_pierce[22] = 16;
        Point1_Template_pierce[23] = 18;
        Point1_Template_pierce[24] = 20;
        Point1_Template_pierce[25] = 22;
        Point1_Template_pierce[26] = 24;
        Point1_Template_pierce[27] = 26;
        Point1_Template_pierce[28] = 28;
        Point1_Template_pierce[29] = 30;
        Point1_Template_pierce[30] = 32;
        Point1_Template_pierce[31] = 34;
        Point1_Template_pierce[32] = 36;
        Point1_Template_pierce[33] = 38;
        Point1_Template_pierce[34] = 40;
        Point1_Template_pierce[35] = 42;
        Point1_Template_pierce[36] = 44;
        Point1_Template_pierce[37] = 46;
        Point1_Template_pierce[38] = 48;
        Point1_Template_pierce[39] = 50;
        Point1_Template_pierce[40] = 52;
        Point1_Template_pierce[41] = 54;
        Point1_Template_pierce[42] = 56;
        Point1_Template_pierce[43] = 58;
        Point1_Template_pierce[44] = 60;
        Point1_Template_pierce[45] = 62;
        Point1_Template_pierce[46] = 64;
        Point1_Template_pierce[47] = 66;
        Point1_Template_pierce[48] = 68;
        Point1_Template_pierce[49] = 70;
        Point1_Template_pierce[50] = 72;
        Point1_Template_pierce[51] = 74;
        Point1_Template_pierce[52] = 76;
        Point1_Template_pierce[53] = 78;
        Point1_Template_pierce[54] = 80;
        Point1_Template_pierce[55] = 82;
        Point1_Template_pierce[56] = 84;
        Point1_Template_pierce[57] = 86;
        Point1_Template_pierce[58] = 88;
        Point1_Template_pierce[59] = 90;
        Point1_Template_pierce[60] = 92;
        Point1_Template_pierce[61] = 94;
        Point1_Template_pierce[62] = 96;
        Point1_Template_pierce[63] = 98;
        Point1_Template_pierce[64] = 100;
        Point1_Template_pierce[65] = 102;
        Point1_Template_pierce[66] = 104;
        Point1_Template_pierce[67] = 106;
        Point1_Template_pierce[68] = 108;
        Point1_Template_pierce[69] = 110;
        Point1_Template_pierce[70] = 112;
        Point1_Template_pierce[71] = 114;
        Point1_Template_pierce[72] = 116;
        Point1_Template_pierce[73] = 118;
        Point1_Template_pierce[74] = 120;
        Point1_Template_pierce[75] = 122;
        Point1_Template_pierce[76] = 124;
        Point1_Template_pierce[77] = 126;
        Point1_Template_pierce[78] = 128;
        Point1_Template_pierce[79] = 130;
        Point1_Template_pierce[80] = 132;
        for (int i = 80; i < 200; i++) {
            Point1_Template_atk[i] = Point1_Template_atk[i - 1]
                    + (Point1_Template_atk[i - 1] - Point1_Template_atk[i - 2]);
            Point1_Template_crit[i] = Point1_Template_crit[i - 1]
                    + (Point1_Template_crit[i - 1] - Point1_Template_crit[i - 2]);
            Point1_Template_pierce[i] = Point1_Template_pierce[i - 1]
                    + (Point1_Template_pierce[i - 1] - Point1_Template_pierce[i - 2]);
        }
    }

    private static void load_point_2() {
        Point2_Template_def = new int[200];
        Point2_Template_resist_magic = new int[200];
        Point2_Template_resist_physical = new int[200];
        Point2_Template_def[0] = 140;
        Point2_Template_def[1] = 148;
        Point2_Template_def[2] = 165;
        Point2_Template_def[3] = 182;
        Point2_Template_def[4] = 199;
        Point2_Template_def[5] = 225;
        Point2_Template_def[6] = 251;
        Point2_Template_def[7] = 277;
        Point2_Template_def[8] = 312;
        Point2_Template_def[9] = 347;
        Point2_Template_def[10] = 382;
        Point2_Template_def[11] = 417;
        Point2_Template_def[12] = 461;
        Point2_Template_def[13] = 505;
        Point2_Template_def[14] = 558;
        Point2_Template_def[15] = 611;
        Point2_Template_def[16] = 664;
        Point2_Template_def[17] = 726;
        Point2_Template_def[18] = 788;
        Point2_Template_def[19] = 850;
        Point2_Template_def[20] = 921;
        Point2_Template_def[21] = 992;
        Point2_Template_def[22] = 1072;
        Point2_Template_def[23] = 1161;
        Point2_Template_def[24] = 1250;
        Point2_Template_def[25] = 1339;
        Point2_Template_def[26] = 1436;
        Point2_Template_def[27] = 1533;
        Point2_Template_def[28] = 1630;
        Point2_Template_def[29] = 1736;
        Point2_Template_def[30] = 1842;
        Point2_Template_def[31] = 1948;
        Point2_Template_def[32] = 2063;
        Point2_Template_def[33] = 2178;
        Point2_Template_def[34] = 2293;
        Point2_Template_def[35] = 2417;
        Point2_Template_def[36] = 2541;
        Point2_Template_def[37] = 2665;
        Point2_Template_def[38] = 2798;
        Point2_Template_def[39] = 2931;
        Point2_Template_def[40] = 3064;
        Point2_Template_def[41] = 3197;
        Point2_Template_def[42] = 3339;
        Point2_Template_def[43] = 3481;
        Point2_Template_def[44] = 3632;
        Point2_Template_def[45] = 3783;
        Point2_Template_def[46] = 3934;
        Point2_Template_def[47] = 4094;
        Point2_Template_def[48] = 4254;
        Point2_Template_def[49] = 4414;
        Point2_Template_def[50] = 4583;
        Point2_Template_def[51] = 4752;
        Point2_Template_def[52] = 4930;
        Point2_Template_def[53] = 5116;
        Point2_Template_def[54] = 5302;
        Point2_Template_def[55] = 5488;
        Point2_Template_def[56] = 5683;
        Point2_Template_def[57] = 5878;
        Point2_Template_def[58] = 6073;
        Point2_Template_def[59] = 6277;
        Point2_Template_def[60] = 6481;
        Point2_Template_def[61] = 6685;
        Point2_Template_def[62] = 6898;
        Point2_Template_def[63] = 7111;
        Point2_Template_def[64] = 7324;
        Point2_Template_def[65] = 7546;
        Point2_Template_def[66] = 7768;
        Point2_Template_def[67] = 7990;
        Point2_Template_def[68] = 8221;
        Point2_Template_def[69] = 8452;
        Point2_Template_def[70] = 8683;
        Point2_Template_def[71] = 8914;
        Point2_Template_def[72] = 9154;
        Point2_Template_def[73] = 9394;
        Point2_Template_def[74] = 9643;
        Point2_Template_def[75] = 9892;
        Point2_Template_def[76] = 10141;
        Point2_Template_def[77] = 10399;
        Point2_Template_def[78] = 10657;
        Point2_Template_def[79] = 10915;
        Point2_Template_def[80] = 11182;
        Point2_Template_resist_magic[0] = 0;
        Point2_Template_resist_magic[1] = 0;
        Point2_Template_resist_magic[2] = 0;
        Point2_Template_resist_magic[3] = 0;
        Point2_Template_resist_magic[4] = 0;
        Point2_Template_resist_magic[5] = 0;
        Point2_Template_resist_magic[6] = 0;
        Point2_Template_resist_magic[7] = 0;
        Point2_Template_resist_magic[8] = 0;
        Point2_Template_resist_magic[9] = 0;
        Point2_Template_resist_magic[10] = 0;
        Point2_Template_resist_magic[11] = 0;
        Point2_Template_resist_magic[12] = 0;
        Point2_Template_resist_magic[13] = 0;
        Point2_Template_resist_magic[14] = 0;
        Point2_Template_resist_magic[15] = 0;
        Point2_Template_resist_magic[16] = 0;
        Point2_Template_resist_magic[17] = 0;
        Point2_Template_resist_magic[18] = 0;
        Point2_Template_resist_magic[19] = 10;
        Point2_Template_resist_magic[20] = 15;
        Point2_Template_resist_magic[21] = 20;
        Point2_Template_resist_magic[22] = 25;
        Point2_Template_resist_magic[23] = 30;
        Point2_Template_resist_magic[24] = 35;
        Point2_Template_resist_magic[25] = 40;
        Point2_Template_resist_magic[26] = 45;
        Point2_Template_resist_magic[27] = 50;
        Point2_Template_resist_magic[28] = 55;
        Point2_Template_resist_magic[29] = 60;
        Point2_Template_resist_magic[30] = 65;
        Point2_Template_resist_magic[31] = 70;
        Point2_Template_resist_magic[32] = 75;
        Point2_Template_resist_magic[33] = 80;
        Point2_Template_resist_magic[34] = 85;
        Point2_Template_resist_magic[35] = 90;
        Point2_Template_resist_magic[36] = 95;
        Point2_Template_resist_magic[37] = 100;
        Point2_Template_resist_magic[38] = 105;
        Point2_Template_resist_magic[39] = 110;
        Point2_Template_resist_magic[40] = 115;
        Point2_Template_resist_magic[41] = 120;
        Point2_Template_resist_magic[42] = 125;
        Point2_Template_resist_magic[43] = 130;
        Point2_Template_resist_magic[44] = 135;
        Point2_Template_resist_magic[45] = 140;
        Point2_Template_resist_magic[46] = 145;
        Point2_Template_resist_magic[47] = 150;
        Point2_Template_resist_magic[48] = 155;
        Point2_Template_resist_magic[49] = 160;
        Point2_Template_resist_magic[50] = 165;
        Point2_Template_resist_magic[51] = 170;
        Point2_Template_resist_magic[52] = 175;
        Point2_Template_resist_magic[53] = 180;
        Point2_Template_resist_magic[54] = 185;
        Point2_Template_resist_magic[55] = 190;
        Point2_Template_resist_magic[56] = 195;
        Point2_Template_resist_magic[57] = 200;
        Point2_Template_resist_magic[58] = 205;
        Point2_Template_resist_magic[59] = 210;
        Point2_Template_resist_magic[60] = 215;
        Point2_Template_resist_magic[61] = 220;
        Point2_Template_resist_magic[62] = 225;
        Point2_Template_resist_magic[63] = 230;
        Point2_Template_resist_magic[64] = 235;
        Point2_Template_resist_magic[65] = 240;
        Point2_Template_resist_magic[66] = 245;
        Point2_Template_resist_magic[67] = 250;
        Point2_Template_resist_magic[68] = 255;
        Point2_Template_resist_magic[69] = 260;
        Point2_Template_resist_magic[70] = 265;
        Point2_Template_resist_magic[71] = 270;
        Point2_Template_resist_magic[72] = 275;
        Point2_Template_resist_magic[73] = 280;
        Point2_Template_resist_magic[74] = 285;
        Point2_Template_resist_magic[75] = 290;
        Point2_Template_resist_magic[76] = 295;
        Point2_Template_resist_magic[77] = 300;
        Point2_Template_resist_magic[78] = 305;
        Point2_Template_resist_magic[79] = 310;
        Point2_Template_resist_magic[80] = 315;
        Point2_Template_resist_physical[0] = 0;
        Point2_Template_resist_physical[1] = 0;
        Point2_Template_resist_physical[2] = 0;
        Point2_Template_resist_physical[3] = 0;
        Point2_Template_resist_physical[4] = 0;
        Point2_Template_resist_physical[5] = 0;
        Point2_Template_resist_physical[6] = 0;
        Point2_Template_resist_physical[7] = 0;
        Point2_Template_resist_physical[8] = 0;
        Point2_Template_resist_physical[9] = 0;
        Point2_Template_resist_physical[10] = 0;
        Point2_Template_resist_physical[11] = 0;
        Point2_Template_resist_physical[12] = 0;
        Point2_Template_resist_physical[13] = 0;
        Point2_Template_resist_physical[14] = 0;
        Point2_Template_resist_physical[15] = 0;
        Point2_Template_resist_physical[16] = 0;
        Point2_Template_resist_physical[17] = 0;
        Point2_Template_resist_physical[18] = 0;
        Point2_Template_resist_physical[19] = 10;
        Point2_Template_resist_physical[20] = 15;
        Point2_Template_resist_physical[21] = 20;
        Point2_Template_resist_physical[22] = 25;
        Point2_Template_resist_physical[23] = 30;
        Point2_Template_resist_physical[24] = 35;
        Point2_Template_resist_physical[25] = 40;
        Point2_Template_resist_physical[26] = 45;
        Point2_Template_resist_physical[27] = 50;
        Point2_Template_resist_physical[28] = 55;
        Point2_Template_resist_physical[29] = 60;
        Point2_Template_resist_physical[30] = 65;
        Point2_Template_resist_physical[31] = 70;
        Point2_Template_resist_physical[32] = 75;
        Point2_Template_resist_physical[33] = 80;
        Point2_Template_resist_physical[34] = 85;
        Point2_Template_resist_physical[35] = 90;
        Point2_Template_resist_physical[36] = 95;
        Point2_Template_resist_physical[37] = 100;
        Point2_Template_resist_physical[38] = 105;
        Point2_Template_resist_physical[39] = 110;
        Point2_Template_resist_physical[40] = 115;
        Point2_Template_resist_physical[41] = 120;
        Point2_Template_resist_physical[42] = 125;
        Point2_Template_resist_physical[43] = 130;
        Point2_Template_resist_physical[44] = 135;
        Point2_Template_resist_physical[45] = 140;
        Point2_Template_resist_physical[46] = 145;
        Point2_Template_resist_physical[47] = 150;
        Point2_Template_resist_physical[48] = 155;
        Point2_Template_resist_physical[49] = 160;
        Point2_Template_resist_physical[50] = 165;
        Point2_Template_resist_physical[51] = 170;
        Point2_Template_resist_physical[52] = 175;
        Point2_Template_resist_physical[53] = 180;
        Point2_Template_resist_physical[54] = 185;
        Point2_Template_resist_physical[55] = 190;
        Point2_Template_resist_physical[56] = 195;
        Point2_Template_resist_physical[57] = 200;
        Point2_Template_resist_physical[58] = 205;
        Point2_Template_resist_physical[59] = 210;
        Point2_Template_resist_physical[60] = 215;
        Point2_Template_resist_physical[61] = 220;
        Point2_Template_resist_physical[62] = 225;
        Point2_Template_resist_physical[63] = 230;
        Point2_Template_resist_physical[64] = 235;
        Point2_Template_resist_physical[65] = 240;
        Point2_Template_resist_physical[66] = 245;
        Point2_Template_resist_physical[67] = 250;
        Point2_Template_resist_physical[68] = 255;
        Point2_Template_resist_physical[69] = 260;
        Point2_Template_resist_physical[70] = 265;
        Point2_Template_resist_physical[71] = 270;
        Point2_Template_resist_physical[72] = 275;
        Point2_Template_resist_physical[73] = 280;
        Point2_Template_resist_physical[74] = 285;
        Point2_Template_resist_physical[75] = 290;
        Point2_Template_resist_physical[76] = 295;
        Point2_Template_resist_physical[77] = 300;
        Point2_Template_resist_physical[78] = 305;
        Point2_Template_resist_physical[79] = 310;
        Point2_Template_resist_physical[80] = 315;
        for (int i = 80; i < 200; i++) {
            Point2_Template_def[i] = Point2_Template_def[i - 1]
                    + (Point2_Template_def[i - 1] -Point2_Template_def[i - 2]);
            Point2_Template_resist_magic[i] = Point2_Template_resist_magic[i - 1]
                    + (Point2_Template_resist_magic[i - 1] - Point2_Template_resist_magic[i - 2]);
            Point2_Template_resist_physical[i] =
                    Point2_Template_resist_physical[i - 1] + (Point2_Template_resist_physical[i - 1]
                            - Point2_Template_resist_physical[i - 2]);
        }
    }

    private static void load_point_3() {
        Point3_Template_hp = new int[200];
        Point3_Template_hp_potion = new int[200];
        Point3_Template_hp[0] = 1001;
        Point3_Template_hp[1] = 1016;
        Point3_Template_hp[2] = 1041;
        Point3_Template_hp[3] = 1076;
        Point3_Template_hp[4] = 1121;
        Point3_Template_hp[5] = 1176;
        Point3_Template_hp[6] = 1241;
        Point3_Template_hp[7] = 1316;
        Point3_Template_hp[8] = 1401;
        Point3_Template_hp[9] = 1496;
        Point3_Template_hp[10] = 1601;
        Point3_Template_hp[11] = 1716;
        Point3_Template_hp[12] = 1841;
        Point3_Template_hp[13] = 1976;
        Point3_Template_hp[14] = 2121;
        Point3_Template_hp[15] = 2276;
        Point3_Template_hp[16] = 2441;
        Point3_Template_hp[17] = 2616;
        Point3_Template_hp[18] = 2801;
        Point3_Template_hp[19] = 2996;
        Point3_Template_hp[20] = 3201;
        Point3_Template_hp[21] = 3416;
        Point3_Template_hp[22] = 3641;
        Point3_Template_hp[23] = 3876;
        Point3_Template_hp[24] = 4121;
        Point3_Template_hp[25] = 4376;
        Point3_Template_hp[26] = 4641;
        Point3_Template_hp[27] = 4916;
        Point3_Template_hp[28] = 5201;
        Point3_Template_hp[29] = 5496;
        Point3_Template_hp[30] = 5801;
        Point3_Template_hp[31] = 6116;
        Point3_Template_hp[32] = 6441;
        Point3_Template_hp[33] = 6776;
        Point3_Template_hp[34] = 7121;
        Point3_Template_hp[35] = 7476;
        Point3_Template_hp[36] = 7841;
        Point3_Template_hp[37] = 8216;
        Point3_Template_hp[38] = 8601;
        Point3_Template_hp[39] = 8996;
        Point3_Template_hp[40] = 9401;
        Point3_Template_hp[41] = 9816;
        Point3_Template_hp[42] = 10241;
        Point3_Template_hp[43] = 10676;
        Point3_Template_hp[44] = 11121;
        Point3_Template_hp[45] = 11576;
        Point3_Template_hp[46] = 12041;
        Point3_Template_hp[47] = 12516;
        Point3_Template_hp[48] = 13001;
        Point3_Template_hp[49] = 13496;
        Point3_Template_hp[50] = 14001;
        Point3_Template_hp[51] = 14516;
        Point3_Template_hp[52] = 15041;
        Point3_Template_hp[53] = 15576;
        Point3_Template_hp[54] = 16121;
        Point3_Template_hp[55] = 16676;
        Point3_Template_hp[56] = 17241;
        Point3_Template_hp[57] = 17816;
        Point3_Template_hp[58] = 18401;
        Point3_Template_hp[59] = 18996;
        Point3_Template_hp[60] = 19601;
        Point3_Template_hp[61] = 20216;
        Point3_Template_hp[62] = 20841;
        Point3_Template_hp[63] = 21476;
        Point3_Template_hp[64] = 22121;
        Point3_Template_hp[65] = 22776;
        Point3_Template_hp[66] = 23441;
        Point3_Template_hp[67] = 24116;
        Point3_Template_hp[68] = 24801;
        Point3_Template_hp[69] = 25496;
        Point3_Template_hp[70] = 26201;
        Point3_Template_hp[71] = 26916;
        Point3_Template_hp[72] = 27641;
        Point3_Template_hp[73] = 28376;
        Point3_Template_hp[74] = 29121;
        Point3_Template_hp[75] = 29876;
        Point3_Template_hp[76] = 30641;
        Point3_Template_hp[77] = 31416;
        Point3_Template_hp[78] = 32201;
        Point3_Template_hp[79] = 32996;
        Point3_Template_hp[80] = 33801;
        Point3_Template_hp_potion[0] = 0;
        Point3_Template_hp_potion[1] = 0;
        Point3_Template_hp_potion[2] = 0;
        Point3_Template_hp_potion[3] = 0;
        Point3_Template_hp_potion[4] = 0;
        Point3_Template_hp_potion[5] = 0;
        Point3_Template_hp_potion[6] = 0;
        Point3_Template_hp_potion[7] = 0;
        Point3_Template_hp_potion[8] = 0;
        Point3_Template_hp_potion[9] = 0;
        Point3_Template_hp_potion[10] = 0;
        Point3_Template_hp_potion[11] = 0;
        Point3_Template_hp_potion[12] = 0;
        Point3_Template_hp_potion[13] = 0;
        Point3_Template_hp_potion[14] = 0;
        Point3_Template_hp_potion[15] = 0;
        Point3_Template_hp_potion[16] = 0;
        Point3_Template_hp_potion[17] = 0;
        Point3_Template_hp_potion[18] = 0;
        Point3_Template_hp_potion[19] = 12;
        Point3_Template_hp_potion[20] = 24;
        Point3_Template_hp_potion[21] = 36;
        Point3_Template_hp_potion[22] = 48;
        Point3_Template_hp_potion[23] = 60;
        Point3_Template_hp_potion[24] = 72;
        Point3_Template_hp_potion[25] = 84;
        Point3_Template_hp_potion[26] = 96;
        Point3_Template_hp_potion[27] = 108;
        Point3_Template_hp_potion[28] = 120;
        Point3_Template_hp_potion[29] = 132;
        Point3_Template_hp_potion[30] = 144;
        Point3_Template_hp_potion[31] = 156;
        Point3_Template_hp_potion[32] = 168;
        Point3_Template_hp_potion[33] = 180;
        Point3_Template_hp_potion[34] = 192;
        Point3_Template_hp_potion[35] = 204;
        Point3_Template_hp_potion[36] = 216;
        Point3_Template_hp_potion[37] = 228;
        Point3_Template_hp_potion[38] = 240;
        Point3_Template_hp_potion[39] = 252;
        Point3_Template_hp_potion[40] = 264;
        Point3_Template_hp_potion[41] = 276;
        Point3_Template_hp_potion[42] = 288;
        Point3_Template_hp_potion[43] = 300;
        Point3_Template_hp_potion[44] = 312;
        Point3_Template_hp_potion[45] = 324;
        Point3_Template_hp_potion[46] = 336;
        Point3_Template_hp_potion[47] = 348;
        Point3_Template_hp_potion[48] = 360;
        Point3_Template_hp_potion[49] = 372;
        Point3_Template_hp_potion[50] = 384;
        Point3_Template_hp_potion[51] = 396;
        Point3_Template_hp_potion[52] = 408;
        Point3_Template_hp_potion[53] = 420;
        Point3_Template_hp_potion[54] = 432;
        Point3_Template_hp_potion[55] = 444;
        Point3_Template_hp_potion[56] = 456;
        Point3_Template_hp_potion[57] = 468;
        Point3_Template_hp_potion[58] = 480;
        Point3_Template_hp_potion[59] = 492;
        Point3_Template_hp_potion[60] = 504;
        Point3_Template_hp_potion[61] = 516;
        Point3_Template_hp_potion[62] = 528;
        Point3_Template_hp_potion[63] = 540;
        Point3_Template_hp_potion[64] = 552;
        Point3_Template_hp_potion[65] = 564;
        Point3_Template_hp_potion[66] = 576;
        Point3_Template_hp_potion[67] = 588;
        Point3_Template_hp_potion[68] = 600;
        Point3_Template_hp_potion[69] = 612;
        Point3_Template_hp_potion[70] = 624;
        Point3_Template_hp_potion[71] = 636;
        Point3_Template_hp_potion[72] = 648;
        Point3_Template_hp_potion[73] = 660;
        Point3_Template_hp_potion[74] = 672;
        Point3_Template_hp_potion[75] = 684;
        Point3_Template_hp_potion[76] = 696;
        Point3_Template_hp_potion[77] = 708;
        Point3_Template_hp_potion[78] = 720;
        Point3_Template_hp_potion[79] = 732;
        Point3_Template_hp_potion[80] = 744;
        for (int i = 80; i < 200; i++) {
            Point3_Template_hp[i] = Point3_Template_hp[i - 1]
                    + (Point3_Template_hp[i - 1] - Point3_Template_hp[i - 2]);
            Point3_Template_hp_potion[i] = Point3_Template_hp_potion[i - 1]
                    + (Point3_Template_hp_potion[i - 1] - Point3_Template_hp_potion[i - 2]);
        }
    }

    private static void load_point_4() {
        Point4_Template_mp = new int[200];
        Point4_Template_dame_crit = new int[200];
        Point4_Template_mp[0] = 20;
        Point4_Template_mp[1] = 21;
        Point4_Template_mp[2] = 24;
        Point4_Template_mp[3] = 27;
        Point4_Template_mp[4] = 32;
        Point4_Template_mp[5] = 37;
        Point4_Template_mp[6] = 44;
        Point4_Template_mp[7] = 51;
        Point4_Template_mp[8] = 60;
        Point4_Template_mp[9] = 69;
        Point4_Template_mp[10] = 80;
        Point4_Template_mp[11] = 91;
        Point4_Template_mp[12] = 104;
        Point4_Template_mp[13] = 117;
        Point4_Template_mp[14] = 132;
        Point4_Template_mp[15] = 147;
        Point4_Template_mp[16] = 164;
        Point4_Template_mp[17] = 181;
        Point4_Template_mp[18] = 200;
        Point4_Template_mp[19] = 219;
        Point4_Template_mp[20] = 240;
        Point4_Template_mp[21] = 261;
        Point4_Template_mp[22] = 284;
        Point4_Template_mp[23] = 307;
        Point4_Template_mp[24] = 332;
        Point4_Template_mp[25] = 357;
        Point4_Template_mp[26] = 384;
        Point4_Template_mp[27] = 411;
        Point4_Template_mp[28] = 440;
        Point4_Template_mp[29] = 469;
        Point4_Template_mp[30] = 500;
        Point4_Template_mp[31] = 531;
        Point4_Template_mp[32] = 564;
        Point4_Template_mp[33] = 597;
        Point4_Template_mp[34] = 632;
        Point4_Template_mp[35] = 667;
        Point4_Template_mp[36] = 704;
        Point4_Template_mp[37] = 741;
        Point4_Template_mp[38] = 780;
        Point4_Template_mp[39] = 819;
        Point4_Template_mp[40] = 860;
        Point4_Template_mp[41] = 901;
        Point4_Template_mp[42] = 944;
        Point4_Template_mp[43] = 987;
        Point4_Template_mp[44] = 1032;
        Point4_Template_mp[45] = 1077;
        Point4_Template_mp[46] = 1124;
        Point4_Template_mp[47] = 1171;
        Point4_Template_mp[48] = 1220;
        Point4_Template_mp[49] = 1269;
        Point4_Template_mp[50] = 1320;
        Point4_Template_mp[51] = 1371;
        Point4_Template_mp[52] = 1424;
        Point4_Template_mp[53] = 1477;
        Point4_Template_mp[54] = 1532;
        Point4_Template_mp[55] = 1587;
        Point4_Template_mp[56] = 1644;
        Point4_Template_mp[57] = 1701;
        Point4_Template_mp[58] = 1760;
        Point4_Template_mp[59] = 1819;
        Point4_Template_mp[60] = 1880;
        Point4_Template_mp[61] = 1941;
        Point4_Template_mp[62] = 2004;
        Point4_Template_mp[63] = 2067;
        Point4_Template_mp[64] = 2132;
        Point4_Template_mp[65] = 2197;
        Point4_Template_mp[66] = 2264;
        Point4_Template_mp[67] = 2331;
        Point4_Template_mp[68] = 2400;
        Point4_Template_mp[69] = 2469;
        Point4_Template_mp[70] = 2540;
        Point4_Template_mp[71] = 2611;
        Point4_Template_mp[72] = 2684;
        Point4_Template_mp[73] = 2757;
        Point4_Template_mp[74] = 2832;
        Point4_Template_mp[75] = 2907;
        Point4_Template_mp[76] = 2984;
        Point4_Template_mp[77] = 3061;
        Point4_Template_mp[78] = 3140;
        Point4_Template_mp[79] = 3219;
        Point4_Template_mp[80] = 3300;
        Point4_Template_dame_crit[0] = 0;
        Point4_Template_dame_crit[1] = 0;
        Point4_Template_dame_crit[2] = 0;
        Point4_Template_dame_crit[3] = 0;
        Point4_Template_dame_crit[4] = 0;
        Point4_Template_dame_crit[5] = 0;
        Point4_Template_dame_crit[6] = 0;
        Point4_Template_dame_crit[7] = 0;
        Point4_Template_dame_crit[8] = 0;
        Point4_Template_dame_crit[9] = 0;
        Point4_Template_dame_crit[10] = 0;
        Point4_Template_dame_crit[11] = 0;
        Point4_Template_dame_crit[12] = 0;
        Point4_Template_dame_crit[13] = 0;
        Point4_Template_dame_crit[14] = 0;
        Point4_Template_dame_crit[15] = 0;
        Point4_Template_dame_crit[16] = 0;
        Point4_Template_dame_crit[17] = 0;
        Point4_Template_dame_crit[18] = 0;
        Point4_Template_dame_crit[19] = 50;
        Point4_Template_dame_crit[20] = 100;
        Point4_Template_dame_crit[21] = 150;
        Point4_Template_dame_crit[22] = 200;
        Point4_Template_dame_crit[23] = 250;
        Point4_Template_dame_crit[24] = 300;
        Point4_Template_dame_crit[25] = 350;
        Point4_Template_dame_crit[26] = 400;
        Point4_Template_dame_crit[27] = 450;
        Point4_Template_dame_crit[28] = 500;
        Point4_Template_dame_crit[29] = 550;
        Point4_Template_dame_crit[30] = 600;
        Point4_Template_dame_crit[31] = 650;
        Point4_Template_dame_crit[32] = 700;
        Point4_Template_dame_crit[33] = 750;
        Point4_Template_dame_crit[34] = 800;
        Point4_Template_dame_crit[35] = 850;
        Point4_Template_dame_crit[36] = 900;
        Point4_Template_dame_crit[37] = 950;
        Point4_Template_dame_crit[38] = 1000;
        Point4_Template_dame_crit[39] = 1050;
        Point4_Template_dame_crit[40] = 1100;
        Point4_Template_dame_crit[41] = 1150;
        Point4_Template_dame_crit[42] = 1200;
        Point4_Template_dame_crit[43] = 1250;
        Point4_Template_dame_crit[44] = 1300;
        Point4_Template_dame_crit[45] = 1350;
        Point4_Template_dame_crit[46] = 1400;
        Point4_Template_dame_crit[47] = 1450;
        Point4_Template_dame_crit[48] = 1500;
        Point4_Template_dame_crit[49] = 1550;
        Point4_Template_dame_crit[50] = 1600;
        Point4_Template_dame_crit[51] = 1650;
        Point4_Template_dame_crit[52] = 1700;
        Point4_Template_dame_crit[53] = 1750;
        Point4_Template_dame_crit[54] = 1800;
        Point4_Template_dame_crit[55] = 1850;
        Point4_Template_dame_crit[56] = 1900;
        Point4_Template_dame_crit[57] = 1950;
        Point4_Template_dame_crit[58] = 2000;
        Point4_Template_dame_crit[59] = 2050;
        Point4_Template_dame_crit[60] = 2100;
        Point4_Template_dame_crit[61] = 2150;
        Point4_Template_dame_crit[62] = 2200;
        Point4_Template_dame_crit[63] = 2250;
        Point4_Template_dame_crit[64] = 2300;
        Point4_Template_dame_crit[65] = 2350;
        Point4_Template_dame_crit[66] = 2400;
        Point4_Template_dame_crit[67] = 2450;
        Point4_Template_dame_crit[68] = 2500;
        Point4_Template_dame_crit[69] = 2550;
        Point4_Template_dame_crit[70] = 2600;
        Point4_Template_dame_crit[71] = 2650;
        Point4_Template_dame_crit[72] = 2700;
        Point4_Template_dame_crit[73] = 2750;
        Point4_Template_dame_crit[74] = 2800;
        Point4_Template_dame_crit[75] = 2850;
        Point4_Template_dame_crit[76] = 2900;
        Point4_Template_dame_crit[77] = 2950;
        Point4_Template_dame_crit[78] = 3000;
        Point4_Template_dame_crit[79] = 3050;
        Point4_Template_dame_crit[80] = 3100;
        for (int i = 80; i < 200; i++) {
            Point4_Template_mp[i] = Point4_Template_mp[i - 1]
                    + (Point4_Template_mp[i - 1] - Point4_Template_mp[i - 2]);
            Point4_Template_dame_crit[i] = Point4_Template_dame_crit[i - 1]
                    + (Point4_Template_dame_crit[i - 1] - Point4_Template_dame_crit[i - 2]);
        }
    }

    private static void load_point_5() {
        Point5_Template_cooldown = new int[200];
        Point5_Template_miss = new int[200];
        Point5_Template_cooldown[0] = 13;
        Point5_Template_cooldown[1] = 16;
        Point5_Template_cooldown[2] = 19;
        Point5_Template_cooldown[3] = 22;
        Point5_Template_cooldown[4] = 25;
        Point5_Template_cooldown[5] = 28;
        Point5_Template_cooldown[6] = 31;
        Point5_Template_cooldown[7] = 34;
        Point5_Template_cooldown[8] = 37;
        Point5_Template_cooldown[9] = 40;
        Point5_Template_cooldown[10] = 43;
        Point5_Template_cooldown[11] = 46;
        Point5_Template_cooldown[12] = 49;
        Point5_Template_cooldown[13] = 52;
        Point5_Template_cooldown[14] = 55;
        Point5_Template_cooldown[15] = 58;
        Point5_Template_cooldown[16] = 61;
        Point5_Template_cooldown[17] = 64;
        Point5_Template_cooldown[18] = 67;
        Point5_Template_cooldown[19] = 70;
        Point5_Template_cooldown[20] = 73;
        Point5_Template_cooldown[21] = 76;
        Point5_Template_cooldown[22] = 79;
        Point5_Template_cooldown[23] = 82;
        Point5_Template_cooldown[24] = 85;
        Point5_Template_cooldown[25] = 88;
        Point5_Template_cooldown[26] = 91;
        Point5_Template_cooldown[27] = 94;
        Point5_Template_cooldown[28] = 97;
        Point5_Template_cooldown[29] = 100;
        Point5_Template_cooldown[30] = 103;
        Point5_Template_cooldown[31] = 106;
        Point5_Template_cooldown[32] = 109;
        Point5_Template_cooldown[33] = 112;
        Point5_Template_cooldown[34] = 115;
        Point5_Template_cooldown[35] = 118;
        Point5_Template_cooldown[36] = 121;
        Point5_Template_cooldown[37] = 124;
        Point5_Template_cooldown[38] = 127;
        Point5_Template_cooldown[39] = 130;
        Point5_Template_cooldown[40] = 133;
        Point5_Template_cooldown[41] = 136;
        Point5_Template_cooldown[42] = 139;
        Point5_Template_cooldown[43] = 142;
        Point5_Template_cooldown[44] = 145;
        Point5_Template_cooldown[45] = 148;
        Point5_Template_cooldown[46] = 151;
        Point5_Template_cooldown[47] = 154;
        Point5_Template_cooldown[48] = 157;
        Point5_Template_cooldown[49] = 160;
        Point5_Template_cooldown[50] = 163;
        Point5_Template_cooldown[51] = 166;
        Point5_Template_cooldown[52] = 169;
        Point5_Template_cooldown[53] = 172;
        Point5_Template_cooldown[54] = 175;
        Point5_Template_cooldown[55] = 178;
        Point5_Template_cooldown[56] = 181;
        Point5_Template_cooldown[57] = 184;
        Point5_Template_cooldown[58] = 187;
        Point5_Template_cooldown[59] = 190;
        Point5_Template_cooldown[60] = 193;
        Point5_Template_cooldown[61] = 196;
        Point5_Template_cooldown[62] = 199;
        Point5_Template_cooldown[63] = 202;
        Point5_Template_cooldown[64] = 205;
        Point5_Template_cooldown[65] = 208;
        Point5_Template_cooldown[66] = 211;
        Point5_Template_cooldown[67] = 214;
        Point5_Template_cooldown[68] = 217;
        Point5_Template_cooldown[69] = 220;
        Point5_Template_cooldown[70] = 223;
        Point5_Template_cooldown[71] = 226;
        Point5_Template_cooldown[72] = 229;
        Point5_Template_cooldown[73] = 232;
        Point5_Template_cooldown[74] = 235;
        Point5_Template_cooldown[75] = 238;
        Point5_Template_cooldown[76] = 241;
        Point5_Template_cooldown[77] = 244;
        Point5_Template_cooldown[78] = 247;
        Point5_Template_cooldown[79] = 250;
        Point5_Template_cooldown[80] = 253;
        Point5_Template_miss[0] = 0;
        Point5_Template_miss[1] = 0;
        Point5_Template_miss[2] = 0;
        Point5_Template_miss[3] = 0;
        Point5_Template_miss[4] = 0;
        Point5_Template_miss[5] = 0;
        Point5_Template_miss[6] = 0;
        Point5_Template_miss[7] = 0;
        Point5_Template_miss[8] = 0;
        Point5_Template_miss[9] = 0;
        Point5_Template_miss[10] = 0;
        Point5_Template_miss[11] = 0;
        Point5_Template_miss[12] = 0;
        Point5_Template_miss[13] = 0;
        Point5_Template_miss[14] = 0;
        Point5_Template_miss[15] = 0;
        Point5_Template_miss[16] = 0;
        Point5_Template_miss[17] = 0;
        Point5_Template_miss[18] = 0;
        Point5_Template_miss[19] = 10;
        Point5_Template_miss[20] = 12;
        Point5_Template_miss[21] = 14;
        Point5_Template_miss[22] = 16;
        Point5_Template_miss[23] = 18;
        Point5_Template_miss[24] = 20;
        Point5_Template_miss[25] = 22;
        Point5_Template_miss[26] = 24;
        Point5_Template_miss[27] = 26;
        Point5_Template_miss[28] = 28;
        Point5_Template_miss[29] = 30;
        Point5_Template_miss[30] = 32;
        Point5_Template_miss[31] = 34;
        Point5_Template_miss[32] = 36;
        Point5_Template_miss[33] = 38;
        Point5_Template_miss[34] = 40;
        Point5_Template_miss[35] = 42;
        Point5_Template_miss[36] = 44;
        Point5_Template_miss[37] = 46;
        Point5_Template_miss[38] = 48;
        Point5_Template_miss[39] = 50;
        Point5_Template_miss[40] = 52;
        Point5_Template_miss[41] = 54;
        Point5_Template_miss[42] = 56;
        Point5_Template_miss[43] = 58;
        Point5_Template_miss[44] = 60;
        Point5_Template_miss[45] = 62;
        Point5_Template_miss[46] = 64;
        Point5_Template_miss[47] = 66;
        Point5_Template_miss[48] = 68;
        Point5_Template_miss[49] = 70;
        Point5_Template_miss[50] = 72;
        Point5_Template_miss[51] = 74;
        Point5_Template_miss[52] = 76;
        Point5_Template_miss[53] = 78;
        Point5_Template_miss[54] = 80;
        Point5_Template_miss[55] = 82;
        Point5_Template_miss[56] = 84;
        Point5_Template_miss[57] = 86;
        Point5_Template_miss[58] = 88;
        Point5_Template_miss[59] = 90;
        Point5_Template_miss[60] = 92;
        Point5_Template_miss[61] = 94;
        Point5_Template_miss[62] = 96;
        Point5_Template_miss[63] = 98;
        Point5_Template_miss[64] = 100;
        Point5_Template_miss[65] = 102;
        Point5_Template_miss[66] = 104;
        Point5_Template_miss[67] = 106;
        Point5_Template_miss[68] = 108;
        Point5_Template_miss[69] = 110;
        Point5_Template_miss[70] = 112;
        Point5_Template_miss[71] = 114;
        Point5_Template_miss[72] = 116;
        Point5_Template_miss[73] = 118;
        Point5_Template_miss[74] = 120;
        Point5_Template_miss[75] = 122;
        Point5_Template_miss[76] = 124;
        Point5_Template_miss[77] = 126;
        Point5_Template_miss[78] = 128;
        Point5_Template_miss[79] = 130;
        Point5_Template_miss[80] = 132;
        for (int i = 80; i < 200; i++) {
            Point5_Template_cooldown[i] = Point5_Template_cooldown[i - 1]
                    + (Point5_Template_cooldown[i - 1] - Point5_Template_cooldown[i - 2]);
            Point5_Template_miss[i] = Point5_Template_miss[i - 1]
                    + (Point5_Template_miss[i - 1] - Point5_Template_miss[i - 2]);
        }
    }
}
