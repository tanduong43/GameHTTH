package client;

import io.Message;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import core.Service;
import core.Util;
import java.util.HashSet;
import java.util.Set;
import template.Option;

/**
 *
 * @author Truongbk
 */
public class Pet {

    public static List<Pet> ENTRY = new ArrayList<>();
    public short id_pet, icon, frame;//temp
    public String name;
    public byte type;
    public List<Option> option_pet;

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

    private static void show_inven(Player p) throws IOException {
        Message m = new Message(-80);
        m.writer().writeByte(3);
        m.writer().writeShort(p.my_pet.size());//b3
        for (int i = 0; i < p.my_pet.size(); i++) {
            p.my_pet.get(i).id = (short) i;
            m.writer().writeShort(p.my_pet.get(i).id);
            m.writer().writeUTF(p.my_pet.get(i).template.name);
            m.writer().writeUTF(p.my_pet.get(i).template.name); //in4
            m.writer().writeShort(p.my_pet.get(i).template.icon); //icon
            m.writer().writeByte(110); //b2
            m.writer().writeByte(p.my_pet.get(i).isUse ? 1 : 0); //b3
            m.writer().writeByte(p.my_pet.get(i).template.option_pet.size()); //b4
            for (int j = 0; j < p.my_pet.get(i).template.option_pet.size(); j++) {
                m.writer().writeByte(p.my_pet.get(i).template.option_pet.get(j).id); //b5
                m.writer().writeShort(p.my_pet.get(i).template.option_pet.get(j).getParam()); //value
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

    public static void addPetToPlayer(Player p, short petTemplateId) throws IOException {
        Pet petTemplate = Pet.getTemplate(petTemplateId);
        if (petTemplate != null) {
            if (!isPetTemplateAlreadyAdded(p, petTemplate)) {
                MyPet newPet = new MyPet(petTemplateId, petTemplate);
                p.my_pet.add(newPet);
                show_inven(p);
                Service.send_box_ThongBao_OK(p, "Đã thêm pet " + petTemplate.name + " vào danh sách của bạn");
                p.update_info_to_all();
            } else {
                Service.send_box_ThongBao_OK(p, "Pet " + petTemplate.name + " đã tồn tại trong danh sách của bạn");
            }
        } else {
            Service.send_box_ThongBao_OK(p, "Không tìm thấy mẫu pet với ID: " + petTemplateId);
        }
    }

    private static boolean isPetTemplateAlreadyAdded(Player p, Pet petTemplate) {
        for (MyPet pet : p.my_pet) {
            if (pet.template.id_pet == petTemplate.id_pet) {
                return true;
            }
        }
        return false;
    }

    private static void addDefaultOptionsToPet(Pet pet) {
        Set<Byte> usedIds = new HashSet<>();
        int numOptions = Util.random(1, 4); // Số lượng tùy chọn ngẫu nhiên từ 1 đến 3
        for (int i = 0; i < numOptions; i++) {
            byte id;
            do {
                id = (byte) Util.random(1, 25);
            } while (usedIds.contains(id));
            usedIds.add(id);
            short param = (short) Util.random(50, 200); // Giá trị ngẫu nhiên cho param
            pet.option_pet.add(new Option(id, param));
        }
    }

    public static Pet getTemplate(int id) {
        for (int i = 0; i < ENTRY.size(); i++) {
            if (ENTRY.get(i).id_pet == id) {
                return ENTRY.get(i);
            }
        }
        return null;
    }
}
