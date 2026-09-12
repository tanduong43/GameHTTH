package template;

import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
/**
 *
 * @author Truongbk
 */
public class Skill_Template {
    // skill id buff  10 trung doc, 11 bat tu, 12 crit lien tuc
    // 1 choang, 2 chay mau
    public static List<Skill_Template> ENTRYS;
    public int ID;
    public int indexSkillInServer;
    public short idIcon;
    public byte typeSkill;
    public byte typeBuff;
    public String name;
    public short range;
    private short typeEffSkill;
    public List<Option> op;
    public byte idEffSpec;
    public short perEffSpec;
    public short timeEffSpec;
    public byte Lv_RQ;
    public byte nTarget;
    public short rangeLan;
    public int damage;
    public short manaLost;
    public int timeDelay;
    public byte nKick;
    public String info;
    public byte typeDevil;
    public int percentDame = 100;

    public int getBasePercentDame() {
        if (info == null || info.isBlank()) {
            return 100;
        }
        Pattern pattern = Pattern.compile("([\\d]+)%\\s+sát\\s+thương\\s+của\\s+chiêu", Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(info);
        if (matcher.find()) {
            try {
                return Integer.parseInt(matcher.group(1));
            } catch (Exception ignored) {}
        }
        pattern = Pattern.compile("([\\d]+)(?=%)");
        matcher = pattern.matcher(info);
        String percent = "";
        while (matcher.find()) {
            percent = matcher.group(1);
        }
        if (!percent.isBlank()) {
            try {
                return Integer.parseInt(percent);
            } catch (Exception ignored) {}
        }
        pattern = Pattern.compile("[\\d]+");
        matcher = pattern.matcher(info);
        while (matcher.find()) {
            percent = matcher.group();
        }
        if (!percent.isBlank()) {
            try {
                return Integer.parseInt(percent);
            } catch (Exception ignored) {}
        }
        return 100;
    }

    public String getInfo(byte level, int clazz) {
        String result = info;
        if (result == null) {
            return "";
        }
        switch (clazz) {
            case 2: {
                result = result.replace("Quả đấm tốc độ", "Nhất kiếm");
                break;
            }
            case 3: {
                result = result.replace("Quả đấm tốc độ", "Hắc cước");
                break;
            }
            case 4: {
                result = result.replace("Quả đấm tốc độ", "Gậy chong chóng");
                break;
            }
            case 5: {
                result = result.replace("Quả đấm tốc độ", "Double Shot");
                break;
            }
        }
        String percent = "";
        Pattern pattern = Pattern.compile("([\\d]+)%\\s+sát\\s+thương\\s+của\\s+chiêu", Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(info);
        if (matcher.find()) {
            percent = matcher.group(1);
        } else {
            pattern = Pattern.compile("([\\d]+)(?=%)");
            matcher = pattern.matcher(info);
            while (matcher.find()) {
                percent = matcher.group(1);
            }
            if (percent.isBlank()) {
                pattern = Pattern.compile("[\\d]+");
                matcher = pattern.matcher(info);
                while (matcher.find()) {
                    percent = matcher.group();
                }
            }
        }
        if (!percent.isBlank()) {
            try {
                int value = Integer.parseInt(percent);
                switch (level) {
                    case 1: {
                        value = (value * 11) / 10;
                        break;
                    }
                    case 2: {
                        value = (value * 125) / 100;
                        break;
                    }
                    case 3: {
                        value = (value * 145) / 100;
                        break;
                    }
                    case 4: {
                        value = (value * 17) / 10;
                        break;
                    }
                    case 5: {
                        value *= 2;
                        break;
                    }
                }
                result = result.replace(percent + "%", (value + "%"));
            } catch (Exception ignored) {}
        }
        return result;
    }

    public Skill_Template(int Index, int Id, short IdImage, byte type, byte typeBuff, String name, short typeEff,
                          short range) {
        this.indexSkillInServer = Index;
        this.ID = (int) Id;
        this.idIcon = IdImage;
        this.typeSkill = type;
        this.typeBuff = typeBuff;
        this.name = name;
        this.range = range;
        this.typeEffSkill = typeEff;
    }

    public void getData(byte nTarget, short rangeLan, int Damage, short Manacost, int CoolDown, byte nkick,
                        String Description, byte LvCur, byte typeDevil) {
        this.nTarget = nTarget;
        this.rangeLan = rangeLan;
        this.damage = Damage;
        this.manaLost = Manacost;
        this.timeDelay = CoolDown;
        this.nKick = nkick;
        this.info = Description;
        this.Lv_RQ = LvCur;
        this.typeDevil = typeDevil;
    }

    public static Skill_Template get_temp(int index, long exp) {
        for (int i = 0; i < Skill_Template.ENTRYS.size(); i++) {
            Skill_Template temp = Skill_Template.ENTRYS.get(i);
            if (temp.indexSkillInServer == index) {
                if (exp == -1 && temp.Lv_RQ == -1) {
                    return temp;
                } else if (exp > -1 && temp.Lv_RQ > -1) {
                    return temp;
                }
            }
        }
        return null;
    }

    public static boolean isClassSkill(int idx, byte clazz) {
        switch (clazz) {
            case 1:
                return (idx >= 0 && idx < 60) || (idx >= 375 && idx < 395) || (idx >= 566 && idx <= 583) || (idx >= 667 && idx <= 678);
            case 2:
                return (idx >= 60 && idx < 120) || (idx >= 395 && idx < 415) || (idx >= 584 && idx <= 601) || (idx >= 679 && idx <= 690);
            case 3:
                return (idx >= 120 && idx < 180) || (idx >= 415 && idx < 435) || (idx >= 602 && idx <= 619) || (idx >= 691 && idx <= 702);
            case 4:
                return (idx >= 180 && idx < 240) || (idx >= 435 && idx < 455) || (idx >= 620 && idx <= 637) || (idx >= 703 && idx <= 714);
            case 5:
                return (idx >= 240 && idx < 300) || (idx >= 455 && idx < 475) || (idx >= 638 && idx <= 655) || (idx >= 715 && idx <= 726);
            default:
                return false;
        }
    }

    public static byte getClassOfSkill(int idx) {
        for (byte c = 1; c <= 5; c++) {
            if (isClassSkill(idx, c)) {
                return c;
            }
        }
        return 0;
    }

    public static Skill_Template getClassSkillTemplate(byte clazz, int skillId, int level) {
        for (int i = 0; i < Skill_Template.ENTRYS.size(); i++) {
            Skill_Template entry = Skill_Template.ENTRYS.get(i);
            if (entry.ID == skillId && entry.Lv_RQ == level && isClassSkill(entry.indexSkillInServer, clazz)) {
                return entry;
            }
        }
        return null;
    }

    public static boolean upgrade_skill(Skill_info sk_info, byte clazz) {
        if (sk_info == null || sk_info.temp == null || sk_info.temp.Lv_RQ >= 30) {
            return false;
        }
        Skill_Template result = null;
        for (int i = 0; i < Skill_Template.ENTRYS.size(); i++) {
            Skill_Template temp_ss = Skill_Template.ENTRYS.get(i);
            if (sk_info.temp.ID == temp_ss.ID && temp_ss.Lv_RQ == (sk_info.temp.Lv_RQ + 1)) {
                if (isClassSkill(temp_ss.indexSkillInServer, clazz)) {
                    result = temp_ss;
                    break;
                }
            }
        }
        if (result == null && (
            (sk_info.temp.indexSkillInServer >= 661 && sk_info.temp.indexSkillInServer <= 665) ||
            (sk_info.temp.indexSkillInServer >= 900 && sk_info.temp.indexSkillInServer <= 902) ||
            (sk_info.temp.indexSkillInServer >= 910 && sk_info.temp.indexSkillInServer <= 917)
        )) {
            for (int i = 0; i < Skill_Template.ENTRYS.size(); i++) {
                Skill_Template temp_ss = Skill_Template.ENTRYS.get(i);
                if (sk_info.temp.ID == temp_ss.ID && temp_ss.Lv_RQ == (sk_info.temp.Lv_RQ + 1)) {
                    result = temp_ss;
                    break;
                }
            }
        }
        if (result != null && result.Lv_RQ > 0) {
            if (result.Lv_RQ > 30) {
                return false;
            } else {
                sk_info.temp = result;
                return true;
            }
        }
        return false;
    }

    public static boolean learn_skill(Skill_info sk_info) {
        if (sk_info == null || sk_info.temp == null) {
            return false;
        }
        byte c = getClassOfSkill(sk_info.temp.indexSkillInServer);
        if (sk_info.temp.Lv_RQ == -1) {
            Skill_Template result = null;
            for (int i = 0; i < Skill_Template.ENTRYS.size(); i++) {
                Skill_Template entry = Skill_Template.ENTRYS.get(i);
                if (sk_info.temp.indexSkillInServer == entry.indexSkillInServer
                        && sk_info.temp.ID == entry.ID && entry.Lv_RQ == 1) {
                    if (c == 0 || isClassSkill(entry.indexSkillInServer, c)) {
                        result = entry;
                        break;
                    }
                }
            }
            if (result != null) {
                sk_info.temp = result;
                sk_info.exp = 0;
                return true;
            }
        } else {
            Skill_Template result = null;
            for (int i = 0; i < Skill_Template.ENTRYS.size(); i++) {
                Skill_Template entry = Skill_Template.ENTRYS.get(i);
                if (sk_info.temp.ID == entry.ID && entry.Lv_RQ == (sk_info.temp.Lv_RQ + 1)) {
                    if (c == 0 || isClassSkill(entry.indexSkillInServer, c)) {
                        result = entry;
                        break;
                    }
                }
            }
            if (result != null) {
                sk_info.temp = result;
                sk_info.exp = 0;
                return true;
            }
        }
        return false;
    }

    public static void reset_skill(Skill_info sk_info) {
        if (sk_info == null || sk_info.temp == null || sk_info.temp.Lv_RQ == -1) {
            return;
        }
        byte c = getClassOfSkill(sk_info.temp.indexSkillInServer);
        for (int i = 0; i < Skill_Template.ENTRYS.size(); i++) {
            Skill_Template entry = Skill_Template.ENTRYS.get(i);
            if (sk_info.temp.ID == entry.ID && sk_info.temp.typeSkill == entry.typeSkill && entry.Lv_RQ == -1) {
                if (c == 0 || isClassSkill(entry.indexSkillInServer, c)) {
                    sk_info.temp = entry;
                    sk_info.exp = -1;
                    break;
                }
            }
        }
    }

    public short getTypeEffSkill() {
        return typeEffSkill;
    }
}
