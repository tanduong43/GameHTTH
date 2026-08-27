package client;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import core.Service;
import io.Message;
import template.Option;

/**
 *
 * @author Truongbk
 */
public class Pet {
    public static List<Pet> ENTRY = new ArrayList<>();
    public short id, icon, frame;
    public String name;
    public byte type;
    public byte isShow = 0;
    public List<Option> op = new ArrayList<>();

    public static void process(Player p, Message m2) throws IOException {
        byte act = m2.reader().readByte();
        // System.out.println(act + " " + m2.reader().available());
        if (act == 3) { // show table
            Pet.show_inven(p);
        } else if (act == 4) {
            check_expiry_pet(p, true);
            try {
                byte type = m2.reader().readByte();
                short id = m2.reader().readShort();
                // System.out.println(type + " " + id);
                if (type == 1) { // mac
                    MyPet pet_select = null;
                    for (int i = 0; i < p.my_pet.size(); i++) {
                        if (p.my_pet.get(i).id == id) {
                            pet_select = p.my_pet.get(i);
                        }
                        p.my_pet.get(i).isUse = false;
                    }
                    if (pet_select != null) {
                        pet_select.isUse = true;
                        Pet.show_inven(p);
                        Service.send_box_ThongBao_OK(p,
                                "Trang bị " + pet_select.template.name + " thành công");
                        p.update_info_to_all();
                    }
                } else if (type == 0) { // thao
                    MyPet pet_select = null;
                    for (int i = 0; i < p.my_pet.size(); i++) {
                        if (p.my_pet.get(i).id == id && p.my_pet.get(i).isUse) {
                            pet_select = p.my_pet.get(i);
                            pet_select.isUse = false;
                            break;
                        }
                    }
                    if (pet_select != null) {
                        Pet.show_inven(p);
                        Service.send_box_ThongBao_OK(p,
                                "Tháo " + pet_select.template.name + " thành công");
                        p.update_info_to_all();
                    }
                }
            } catch (IOException e) {
            }
        }
    }

    // Danh sách 11 thuộc tính random cho Pet:
    // Nhóm Tiềm Năng (1 - 10): 5: T/n sức mạnh, 6: T/n phòng thủ, 7: T/n thể lực, 8: T/n tinh thần, 9: T/n nhanh nhẹn
    // Nhóm Chỉ Số % (1% - 10%): 10: Chí mạng, 12: Né tránh, 13: Xuyên giáp, 14: Phản đòn, 53: Miễn thương, 63: Giảm miễn thương
    public static final int[] PET_STAT_POOL = new int[] { 5, 6, 7, 8, 9, 10, 12, 13, 14, 53, 63 };

    public static boolean isPercentOption(int id) {
        if (template.ItemOptionTemplate.ENTRYS != null) {
            for (template.ItemOptionTemplate entry : template.ItemOptionTemplate.ENTRYS) {
                if (entry.id == id) {
                    return entry.percent != 0;
                }
            }
        }
        return id == 10 || id == 12 || id == 13 || id == 14 || id == 53 || id == 63;
    }

    public static String getOptionName(int id) {
        if (template.ItemOptionTemplate.ENTRYS != null) {
            for (template.ItemOptionTemplate entry : template.ItemOptionTemplate.ENTRYS) {
                if (entry.id == id) {
                    return entry.name;
                }
            }
        }
        switch (id) {
            case 10: return "Chí mạng";
            case 12: return "Né tránh";
            case 13: return "Xuyên giáp";
            case 14: return "Phản đòn";
            case 53: return "Miễn thương";
            case 63: return "Giảm miễn thương";
        }
        return "Chỉ số " + id;
    }

    public static String formatOptionString(Option op) {
        String opName = getOptionName(op.id);
        boolean isPct = isPercentOption(op.id);
        int val = op.getParam();
        if (isPct) {
            if (val >= 10 && val % 10 == 0) {
                val = val / 10;
            }
            return opName + ": +" + val + "%";
        } else {
            return opName + ": +" + val;
        }
    }

    public static int generateStatValue(int optId) {
        if (isPercentOption(optId)) {
            int percent = core.Util.random(1, 11); // Random từ 1% đến 10%
            return percent * 10; // Lưu giá trị theo chuẩn combat Server (10..100)
        } else {
            return core.Util.random(1, 11); // Random từ 1 đến 10 điểm
        }
    }

    public static String getPetDescription(MyPet pet) {
        StringBuilder sb = new StringBuilder();
        
        // Cấp độ Pet
        sb.append("[Cấp độ: Lv.").append(pet.level).append("/3]");
        if (!pet.isMaxLevel()) {
            sb.append(" (EXP: ").append(pet.exp).append("/").append(MyPet.getMaxExp(pet.level)).append(")");
        } else {
            sb.append(" (Cấp tối đa)");
        }
        sb.append("\n");

        // Chỉ số gốc từ Template
        if (pet.template.op != null && !pet.template.op.isEmpty()) {
            sb.append("[Chỉ số cơ bản]\n");
            for (int j = 0; j < pet.template.op.size(); j++) {
                Option op = pet.template.op.get(j);
                sb.append(formatOptionString(op)).append("\n");
            }
        }

        // Chỉ số huấn luyện thêm theo từng cấp độ
        if (pet.extra_op != null && !pet.extra_op.isEmpty()) {
            sb.append("[Chỉ số Huấn Luyện]\n");
            for (int j = 0; j < pet.extra_op.size(); j++) {
                Option op = pet.extra_op.get(j);
                sb.append("• Lv.").append(j + 1).append(": ").append(formatOptionString(op));
                if (j < pet.extra_op.size() - 1) {
                    sb.append("\n");
                }
            }
        }

        // Hạn sử dụng
        if (pet.expiryTime != -1) {
            java.text.SimpleDateFormat sdf = new java.text.SimpleDateFormat("HH:mm dd/MM/yyyy");
            sb.append("\n[HSD: ").append(sdf.format(new java.util.Date(pet.expiryTime))).append("]");
        } else {
            sb.append("\n[HSD: Vĩnh viễn]");
        }
        return sb.toString();
    }

    public static String getPetNameWithStats(MyPet pet) {
        String lvPrefix = "[Lv." + pet.level + "] ";
        if (pet.expiryTime != -1) {
            long remaining = pet.expiryTime - System.currentTimeMillis();
            if (remaining > 0) {
                long days = remaining / (24 * 60 * 60 * 1000L);
                long hours = (remaining % (24 * 60 * 60 * 1000L)) / (60 * 60 * 1000L);
                if (days > 0) {
                    return lvPrefix + pet.template.name + " (" + days + " ngày)";
                } else {
                    return lvPrefix + pet.template.name + " (" + hours + " giờ)";
                }
            } else {
                return lvPrefix + pet.template.name + " (Hết hạn)";
            }
        }
        return lvPrefix + pet.template.name + " (Vĩnh viễn)";
    }

    public static boolean levelUpPet(Player p, MyPet pet) {
        if (pet == null || pet.isMaxLevel()) {
            return false;
        }
        if (!pet.canLevelUp()) {
            return false;
        }
        // Chọn 1 option chưa có trong extra_op
        List<Integer> availablePool = new ArrayList<>();
        for (int optId : PET_STAT_POOL) {
            boolean exists = false;
            if (pet.extra_op != null) {
                for (Option op : pet.extra_op) {
                    if (op.id == optId) {
                        exists = true;
                        break;
                    }
                }
            }
            if (!exists) {
                availablePool.add(optId);
            }
        }
        if (availablePool.isEmpty()) {
            for (int optId : PET_STAT_POOL) {
                availablePool.add(optId);
            }
        }
        int chosenOptId = availablePool.get(core.Util.random(0, availablePool.size()));
        int optVal = generateStatValue(chosenOptId);

        if (pet.extra_op == null) {
            pet.extra_op = new ArrayList<>();
        }
        Option newOp = new Option(chosenOptId, optVal);
        pet.extra_op.add(newOp);
        pet.level++;
        pet.exp = 0; // Reset exp cho cấp kế tiếp
        return true;
    }

    public static boolean resetPetStats(MyPet pet) {
        if (pet == null) {
            return false;
        }
        if (pet.extra_op != null) {
            pet.extra_op.clear();
        }
        pet.level = 0;
        pet.exp = 0;
        return true;
    }

    public static boolean reRollPet(Player p, MyPet pet) {
        if (pet == null || pet.level <= 0) {
            return false;
        }
        int curLevel = pet.level;
        pet.extra_op.clear();
        for (int lv = 1; lv <= curLevel; lv++) {
            List<Integer> availablePool = new ArrayList<>();
            for (int optId : PET_STAT_POOL) {
                boolean exists = false;
                for (Option op : pet.extra_op) {
                    if (op.id == optId) {
                        exists = true;
                        break;
                    }
                }
                if (!exists) {
                    availablePool.add(optId);
                }
            }
            if (availablePool.isEmpty()) {
                for (int optId : PET_STAT_POOL) {
                    availablePool.add(optId);
                }
            }
            int chosenOptId = availablePool.get(core.Util.random(0, availablePool.size()));
            int optVal = generateStatValue(chosenOptId);
            pet.extra_op.add(new Option(chosenOptId, optVal));
        }
        return true;
    }

    public static boolean check_expiry_pet(Player p, boolean sendNotice) {
        if (p == null || p.my_pet == null || p.my_pet.isEmpty()) {
            return false;
        }
        long currentTime = System.currentTimeMillis();
        boolean hasExpired = false;
        boolean wasUsingExpired = false;
        for (int i = p.my_pet.size() - 1; i >= 0; i--) {
            MyPet pet = p.my_pet.get(i);
            if (pet == null || pet.template == null || getTemplate(pet.template.id) == null) {
                if (pet != null && pet.isUse) {
                    pet.isUse = false;
                    wasUsingExpired = true;
                }
                p.my_pet.remove(i);
                continue;
            }
            if (pet.expiryTime != -1 && currentTime > pet.expiryTime) {
                if (pet.isUse) {
                    pet.isUse = false;
                    wasUsingExpired = true;
                }
                p.my_pet.remove(i);
                hasExpired = true;
            }
        }
        if (hasExpired) {
            if (wasUsingExpired) {
                try {
                    p.update_info_to_all();
                } catch (Exception e) {
                }
            }
            if (sendNotice && p.conn != null && p.conn.connected) {
                try {
                    Service.send_box_ThongBao_OK(p, "Một số pet của bạn đã hết hạn sử dụng và bị thu hồi.");
                } catch (Exception e) {
                }
            }
        }
        return hasExpired;
    }

    public static void show_inven(Player p) throws IOException {
        check_expiry_pet(p, true);
        if (p.my_pet != null) {
            for (int i = p.my_pet.size() - 1; i >= 0; i--) {
                MyPet pet = p.my_pet.get(i);
                if (pet == null || pet.template == null || getTemplate(pet.template.id) == null) {
                    p.my_pet.remove(i);
                }
            }
        }

        Message m = new Message(-80);
        m.writer().writeByte(3);
        m.writer().writeShort(p.my_pet.size());
        for (int i = 0; i < p.my_pet.size(); i++) {
            MyPet myPet = p.my_pet.get(i);
            myPet.id = (short) i;
            m.writer().writeShort(myPet.id);
            m.writer().writeUTF(getPetNameWithStats(myPet));
            m.writer().writeUTF(getPetDescription(myPet));
            m.writer().writeShort(myPet.template.icon);
            m.writer().writeByte(110);
            m.writer().writeByte(myPet.isUse ? 1 : 0);

            // Gộp tất cả option gốc và option thêm theo cấp độ
            List<Option> allOps = new ArrayList<>();
            if (myPet.template != null && myPet.template.op != null) {
                allOps.addAll(myPet.template.op);
            }
            if (myPet.extra_op != null) {
                allOps.addAll(myPet.extra_op);
            }

            m.writer().writeByte(allOps.size());
            for (Option op : allOps) {
                m.writer().writeByte(op.id);
                m.writer().writeShort(op.getParam());
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static Pet getTemplate(int id) {
        for (int i = 0; i < ENTRY.size(); i++) {
            if (ENTRY.get(i).id == id) {
                return ENTRY.get(i);
            }
        }
        return null;
    }
}
