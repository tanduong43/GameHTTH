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
    public List<Option> op = new ArrayList<>();

    public static void process(Player p, Message m2) throws IOException {
        byte act = m2.reader().readByte();
        // System.out.println(act + " " + m2.reader().available());
        if (act == 3) { // show table
            Pet.show_inven(p);
        } else if (act == 4) {
            try {
                byte type = m2.reader().readByte();
                short id = m2.reader().readShort();
                // System.out.println(type + " " + id);
                if (type == 1) { // mac
                    MyPet pet_select = null;
                    for (int i = 0; i < p.my_pet.size(); i++) {
                        p.my_pet.get(i).isUse = false;
                        if (p.my_pet.get(i).id == id && !p.my_pet.get(i).isUse) {
                            pet_select = p.my_pet.get(i);
                        }
                    }
                    if (pet_select != null && !pet_select.isUse) {
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
                            break;
                        }
                    }
                    if (pet_select != null && pet_select.isUse) {
                        pet_select.isUse = false;
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

    public static String getPetDescription(MyPet pet) {
        StringBuilder sb = new StringBuilder();
        if (pet.template.op == null || pet.template.op.isEmpty()) {
            sb.append(pet.template.name);
        } else {
            for (int j = 0; j < pet.template.op.size(); j++) {
                Option op = pet.template.op.get(j);
                String opName = "";
                for (template.ItemOptionTemplate entry : template.ItemOptionTemplate.ENTRYS) {
                    if (entry.id == op.id) {
                        opName = entry.name;
                        break;
                    }
                }
                if (opName.isEmpty()) {
                    opName = "Chỉ số " + op.id;
                }
                if (opName.endsWith("+") || opName.endsWith("-")) {
                    sb.append(opName).append(op.getParam());
                } else {
                    sb.append(opName).append(": +").append(op.getParam());
                }
                if (j < pet.template.op.size() - 1) {
                    sb.append("\n");
                }
            }
        }
        
        if (pet.expiryTime != -1) {
            java.text.SimpleDateFormat sdf = new java.text.SimpleDateFormat("HH:mm dd/MM/yyyy");
            sb.append("\n[HSD: ").append(sdf.format(new java.util.Date(pet.expiryTime))).append("]");
        } else {
            sb.append("\n[HSD: Vĩnh viễn]");
        }
        return sb.toString();
    }

    public static String getPetNameWithStats(MyPet pet) {
        return pet.template.name;
    }

    private static void show_inven(Player p) throws IOException {
        long currentTime = System.currentTimeMillis();
        boolean hasExpired = false;
        for (int i = p.my_pet.size() - 1; i >= 0; i--) {
            MyPet pet = p.my_pet.get(i);
            if (pet.expiryTime != -1 && currentTime > pet.expiryTime) {
                if (pet.isUse) {
                    pet.isUse = false;
                    p.update_info_to_all();
                }
                p.my_pet.remove(i);
                hasExpired = true;
            }
        }
        if (hasExpired) {
            Service.send_box_ThongBao_OK(p, "Một số pet của bạn đã hết hạn sử dụng và bị thu hồi.");
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
            List<Option> ops = myPet.template.op;
            if (ops == null) {
                m.writer().writeByte(0);
            } else {
                m.writer().writeByte(ops.size());
                for (Option op : ops) {
                    m.writer().writeByte(op.id);
                    m.writer().writeShort(op.getParam());
                }
            }
        }
        p.conn.addmsg(m);
        m.cleanup();
        //
        for (int i = 0; i < p.map.players.size(); i++) {
            Player p0 = p.map.players.get(i);
            Service.pet(p, p0, false);
        }
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
