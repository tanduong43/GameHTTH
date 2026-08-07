package template;

import client.Player;
/**
 *
 * @author Truongbk
 */
public class Skill_info {
    // idEff 1: gay choang
    //
    // idEff 2: gay chay mau
    // idEff 3: gay giam cong
    // idEff 4: gay giam thu
    // idEff 5: gay hoa mat
    // idEff 6: gay dien giat
    // idEff 7: gay lua chay
    //
    // idEff 8: gay troi chan
    // idEff 9: gay hut nang luong
    // idEff 10: gay trung doc
    // idEff 11: gay bat tu
    // idEff 12: gay chi mang lien tuc
    // idEff 13: gay tru bat tu
    // idEff 14: gay tru bat tu (co dau)
    // idEff 15: gay hut suc manh
    // idEff 16: gay hoang loan
    //
    public static long[] EXP = new long[27];
    public static int[] EXP_DEVIL = new int[] {100, 125, 167, 200, 250};
    public long exp;
    public Skill_Template temp;
    public byte lvdevil;
    public byte devilpercent;
    static {
        Skill_info.EXP[0] = 3400;
        Skill_info.EXP[1] = 10000;
        Skill_info.EXP[2] = 30000;
        Skill_info.EXP[3] = 60000;
        Skill_info.EXP[4] = 120000;
        for (int i = 5; i < Skill_info.EXP.length; i++) {
            Skill_info.EXP[i] = (Skill_info.EXP[i - 1] * 15) / 10;
        }
    }

    public int get_percent() {
        if (exp <= 0) {
            return 0;
        }
        long exp_total = Skill_info.EXP[temp.Lv_RQ - 1];
        if (exp_total == 0) {
            return 0;
        }
        return (int) ((exp * 1000L) / exp_total);
    }

    public int get_dame(Player p) {
        int result = this.temp.damage;
        if (this.temp.ID >= 2000) {
            result = (p.skill_point.get(0).get_dame(p) * this.temp.percentDame) / 100;
        }
        if (this.temp.ID == 0 || this.temp.ID == 1 || this.temp.ID == 2) {
            switch (this.lvdevil) {
                case 1: {
                    result = (result * 11) / 10;
                    break;
                }
                case 2: {
                    result = (result * 125) / 100;
                    break;
                }
                case 3: {
                    result = (result * 145) / 100;
                    break;
                }
                case 4: {
                    result = (result * 17) / 10;
                    break;
                }
                case 5: {
                    result *= 2;
                    break;
                }
            }
        } else if (this.temp.ID >= 2000 && this.temp.ID < 3000) {
            // switch (this.lvdevil) {
            // case 1: {
            // result = (result * 121) / 100;
            // break;
            // }
            // case 2: {
            // result = (result * 1567) / 1000;
            // break;
            // }
            // case 3: {
            // result = (result * 2106) / 1000;
            // break;
            // }
            // case 4: {
            // result = (result * 2889) / 1000;
            // break;
            // }
            // case 5: {
            // result *= 4;
            // break;
            // }
            // }
        }
        return result;
    }

    public void update_exp_devil(int exp_devil) {
        int exp = (this.devilpercent * Skill_info.EXP_DEVIL[this.lvdevil]) / 100;
        exp += exp_devil;
        while (this.lvdevil < 5 && exp >= Skill_info.EXP_DEVIL[this.lvdevil]) {
            exp -= Skill_info.EXP_DEVIL[this.lvdevil];
            this.lvdevil++;
        }
        if (this.lvdevil == 5) {
            this.devilpercent = 0;
        } else {
            this.devilpercent = (byte) ((exp * 100) / Skill_info.EXP_DEVIL[this.lvdevil]);
        }
    }

    public short get_eff_skill() {
        if (temp.ID >= 2000 && lvdevil == 5) {
            switch (temp.indexSkillInServer) {
                case 658:
                    return 402;
                case 659:
                    return 403;
                case 475:
                    return 228;
                case 476:
                    return 229;
                case 477:
                    return 227;
                case 485:
                    return 232;
                case 486:
                    return 234;
                case 520:
                    return 236;
                case 521:
                    return 235;
                case 522:
                    return 230;
                case 523:
                    return 231;
                case 526:
                    return 237;
                case 527:
                    return 238;
                case 530:
                    return 239;
                case 531:
                    return 240;
                case 535:
                    return 241;
                case 538:
                    return 242;
                case 541:
                    return 244;
                case 542:
                    return 243;
                case 543:
                    return 251;
                case 544:
                    return 252;
                case 546:
                    return 254;
                case 547:
                    return 253;
                case 549:
                    return 255;
            }
        }
        return temp.getTypeEffSkill();
    }
}
