package activities;

import java.io.IOException;

import client.MyPet;
import client.Pet;
import client.Player;
import core.Service;
import core.Util;
import io.Message;

public class VongQuayPet {

    public static void show_table(Player p) throws IOException {
        Message m = new Message(54);
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public static void process(Player p, Message m2) throws IOException {
        byte action = m2.reader().readByte();
        switch (action) {
            case 3: { // Load item to display on the wheel
                Message m = new Message(54);
                m.writer().writeByte(3);
                int numSlots = 14;
                m.writer().writeByte(numSlots);
                for (int i = 0; i < numSlots; i++) {
                    if (!Pet.ENTRY.isEmpty()) {
                        m.writer().writeByte(110); // Thử type 110
                        m.writer().writeShort(Pet.ENTRY.get(Util.random(Pet.ENTRY.size())).icon);
                    } else {
                        m.writer().writeByte(110); // Dự phòng type 4
                        m.writer().writeShort(110); // Dùng icon dự phòng nếu chưa có Pet
                    }
                }
                p.conn.addmsg(m);
                m.cleanup();
                break;
            }
            case 4: { // "Mua thẻ" button pressed
                Service.send_box_ThongBao_OK(p, "Vòng quay Pet trừ trực tiếp Ruby, không cần thẻ!");
                break;
            }
            case 2:
            case 1: { // Action 1 = Quay 1 (100 Ruby), Action 2 = Quay 3 (300 Ruby)
                int numSpins = (action == 1) ? 1 : 3;
                int rubyCost = numSpins * 100;
                if (p.get_ngoc() < rubyCost) {
                    Service.send_box_ThongBao_OK(p, "Bạn không đủ " + rubyCost + " Ruby để quay!");
                    return;
                }
                if (Pet.ENTRY.isEmpty()) {
                    Service.send_box_ThongBao_OK(p, "Hệ thống chưa cập nhật Pet!");
                    return;
                }
                p.update_ngoc(-rubyCost);
                p.update_info_to_all();

                MyPet[] list_reward = new MyPet[numSpins];
                for (int i = 0; i < numSpins; i++) {
                    int petIndex = Util.random(Pet.ENTRY.size());
                    Pet randomPetTemplate = Pet.ENTRY.get(petIndex);
                    MyPet newPet = new MyPet();
                    newPet.id = (short) (p.my_pet.size() + i); // Tạm tính ID
                    newPet.template = randomPetTemplate;
                    newPet.isUse = false;

                    int roll = Util.random(100);
                    if (roll < 1) { // 1% vĩnh viễn
                        newPet.expiryTime = -1;
                    } else { // 99% 3 ngày
                        newPet.expiryTime = System.currentTimeMillis() + 3L * 24 * 60 * 60 * 1000L;
                    }
                    list_reward[i] = newPet;
                }

                Message m = new Message(54);
                m.writer().writeByte(action);
                m.writer().writeByte(list_reward.length);

                StringBuilder notice = new StringBuilder("KẾT QUẢ QUAY PET:\n");

                for (int i = 0; i < list_reward.length; i++) {
                    MyPet rollPet = list_reward[i];
                    // Cấu trúc để UI quay hiển thị phần thưởng
                    m.writer().writeByte(4); // Phải dùng type 4 ở đây để Client không bị lỗi cuộn giấy trắng
                    String petName = rollPet.template.name;
                    if (rollPet.expiryTime == -1) {
                        petName += " (Vĩnh viễn)";
                    } else {
                        petName += " (3 ngày)";
                    }
                    m.writer().writeUTF(petName);
                    m.writer().writeShort(182); // Dùng icon Quả Cầu vì cuộn giấy không đọc được icon Pet
                    m.writer().writeInt(1); // quant
                    m.writer().writeByte(0); // color

                    // Kiểm tra xem đã có pet này chưa
                    boolean isExist = false;
                    boolean announceWorld = false;
                    String statusText = "";

                    for (MyPet existingPet : p.my_pet) {
                        if (existingPet.template.id == rollPet.template.id) {
                            isExist = true;
                            if (existingPet.expiryTime != -1) { // Nếu pet hiện tại chưa vĩnh viễn
                                if (rollPet.expiryTime == -1) {
                                    existingPet.expiryTime = -1; // Thành vĩnh viễn
                                    announceWorld = true;
                                    statusText = "(Nâng cấp vĩnh viễn)";
                                } else {
                                    long currentTime = System.currentTimeMillis();
                                    if (existingPet.expiryTime < currentTime) {
                                        existingPet.expiryTime = currentTime + 3L * 24 * 60 * 60 * 1000L;
                                    } else {
                                        existingPet.expiryTime += 3L * 24 * 60 * 60 * 1000L;
                                    }
                                    statusText = "(Cộng dồn 3 ngày)";
                                }
                            } else {
                                statusText = "(Đã có vĩnh viễn)";
                            }
                            break;
                        }
                    }

                    if (!isExist) {
                        rollPet.id = (short) p.my_pet.size();
                        p.my_pet.add(rollPet);
                        statusText = "(Mới)";
                        if (rollPet.expiryTime == -1) {
                            announceWorld = true;
                        }
                    }

                    boolean isVinhVien = rollPet.expiryTime == -1;
                    notice.append("- ").append(rollPet.template.name)
                            .append(isVinhVien ? " [Vĩnh viễn] " : " [3 ngày] ")
                            .append(statusText).append("\n");

                    if (announceWorld) {
                        core.Manager.gI().chatKTG(0, "Chúc mừng " + p.name + " vừa nhận được Pet "
                                + rollPet.template.name + " vĩnh viễn từ Vòng Quay Pet!", 5);
                    }
                }

                p.conn.addmsg(m);
                m.cleanup();

                // Hiển thị bảng tổng kết phần quà
                Service.send_box_ThongBao_OK(p, notice.toString());
                break;
            }
        }
    }
}
