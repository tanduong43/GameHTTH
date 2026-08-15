package activities;

import java.io.IOException;
import java.util.List;

import client.Player;
import core.Service;
import io.Message;
import map.Map;

/**
 *
 * @author Truongbk
 */
public class TableTickOption {
    public List<Player> listP;
    public byte[] list_check;
    public boolean is_finish = false;
    public short idDialog;

    public static void show_table(Player p, String title) throws IOException {
        if (p.tableTickOption != null) {
            Message m = new Message(-74);
            m.writer().writeByte(0);
            m.writer().writeShort(p.tableTickOption.idDialog); // id dialog
            m.writer().writeUTF(title);
            m.writer().writeByte(p.tableTickOption.listP.size());
            for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                Player p0 = Map.get_player_by_name_allmap(p.tableTickOption.listP.get(i).name);
                if (p0 != null) {
                    m.writer().writeShort(p0.index_map);
                    m.writer().writeUTF(p0.name);
                    m.writer().writeShort(p0.map.template.id);
                }
            }
            for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                Player p0 = Map.get_player_by_name_allmap(p.tableTickOption.listP.get(i).name);
                if (p0 != null) {
                    p0.tableTickOption = p.tableTickOption;
                    p0.conn.addmsg(m);
                }
            }
            m.cleanup();
        }
    }

    public static void process(Player p, Message m2) throws IOException {
        if (p.tableTickOption != null && !p.tableTickOption.is_finish) {
            byte type = m2.reader().readByte();
            short idDialog = m2.reader().readShort();
            // System.out.println(type + " " + idDialog);
            if (p.tableTickOption.idDialog == idDialog) {
                switch (idDialog) {
                    case 0: { // pho ban khong lo
                        if (type == 1) { // accept
                            if (p.tableTickOption.listP.get(0).name.equals(p.name)) {
                                for (int i = 1; i < p.tableTickOption.list_check.length; i++) {
                                    if (p.tableTickOption.list_check[i] != 1) {
                                        Service.send_box_ThongBao_OK(p,
                                                p.tableTickOption.listP.get(i).name
                                                        + " chưa tick chọn");
                                        return;
                                    }
                                }
                                //
                                String name_ok = "";
                                for (int i = 0; i < p.tableTickOption.list_check.length; i++) {
                                    if (p.tableTickOption.list_check[i] == 1) {
                                        name_ok += p.tableTickOption.listP.get(i).name + ", ";
                                    }
                                }
                                Service.send_box_yesno(p, 51, "Thông báo",
                                        ("Xác nhận tham gia phó bản khổng lồ với thành viên sau:\n"
                                                + name_ok),
                                        new String[] { "Đồng ý", "Huỷ" }, new byte[] { 2, 1 });
                            } else {
                                Message m = new Message(-74);
                                m.writer().writeByte(1);
                                m.writer().writeShort(0); // id dialog
                                m.writer().writeShort(p.index_map);
                                for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(
                                            p.tableTickOption.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                    }
                                    if (p.name.equals(p.tableTickOption.listP.get(i).name)) {
                                        p.tableTickOption.list_check[i] = 1;
                                    }
                                }
                                m.cleanup();
                            }
                        } else if (type == 2) { // huy
                            Message m = new Message(-74);
                            m.writer().writeByte(3);
                            m.writer().writeShort(0); // id dialog
                            m.writer().writeShort(p.index_map);
                            for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                                Player p0 = Map.get_player_by_name_allmap(
                                        p.tableTickOption.listP.get(i).name);
                                if (p0 != null) {
                                    p0.conn.addmsg(m);
                                }
                                if (p.name.equals(p.tableTickOption.listP.get(i).name)) {
                                    p.tableTickOption.list_check[i] = -1;
                                }
                            }
                            m.cleanup();
                            p.tableTickOption = null;
                        }
                        break;
                    }
                    case 4: { // pho ban pvp bang
                        if (type == 1) { // accept
                            if (p.tableTickOption.listP.get(0).name.equals(p.name)) {
                                if (activities.PvpClan.is_clan_reach_limit(p.clan)) {
                                    Service.send_box_ThongBao_OK(p,
                                            "Băng của bạn đã đạt giới hạn 5 lượt tham gia trong mốc thời gian này!");
                                    return;
                                }
                                for (int i = 1; i < p.tableTickOption.list_check.length; i++) {
                                    if (p.tableTickOption.list_check[i] != 1) {
                                        Service.send_box_ThongBao_OK(p,
                                                p.tableTickOption.listP.get(i).name
                                                        + " chưa tick chọn");
                                        return;
                                    }
                                }
                                String name_ok = "";
                                for (int i = 0; i < p.tableTickOption.list_check.length; i++) {
                                    if (p.tableTickOption.list_check[i] == 1) {
                                        String mName = p.tableTickOption.listP.get(i).name;
                                        if (activities.PvpClan.is_player_reach_limit(mName)) {
                                            Service.send_box_ThongBao_OK(p,
                                                    "Thành viên " + mName + " đã đạt giới hạn 5 lượt tham gia trong mốc thời gian này!");
                                            return;
                                        }
                                        name_ok += mName + ", ";
                                    }
                                }
                                Service.send_box_yesno(p, 151, "Thông báo",
                                        ("Xác nhận tham gia phó bản PVP Băng với thành viên sau:\n"
                                                + name_ok),
                                        new String[] { "Đồng ý", "Huỷ" }, new byte[] { 2, 1 });
                            } else {
                                Message m = new Message(-74);
                                m.writer().writeByte(1);
                                m.writer().writeShort(4); // id dialog
                                m.writer().writeShort(p.index_map);
                                for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(
                                            p.tableTickOption.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                    }
                                    if (p.name.equals(p.tableTickOption.listP.get(i).name)) {
                                        p.tableTickOption.list_check[i] = 1;
                                    }
                                }
                                m.cleanup();
                            }
                        } else if (type == 2) { // huy
                            Message m = new Message(-74);
                            m.writer().writeByte(3);
                            m.writer().writeShort(4); // id dialog
                            m.writer().writeShort(p.index_map);
                            for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                                Player p0 = Map.get_player_by_name_allmap(
                                        p.tableTickOption.listP.get(i).name);
                                if (p0 != null) {
                                    p0.conn.addmsg(m);
                                }
                                if (p.name.equals(p.tableTickOption.listP.get(i).name)) {
                                    p.tableTickOption.list_check[i] = -1;
                                }
                            }
                            m.cleanup();
                            p.tableTickOption = null;
                        }
                        break;
                    }
                    case 5: { // dai chien dao dao hoa
                        if (type == 1) { // accept
                            if (p.tableTickOption.listP.get(0).name.equals(p.name)) {
                                for (int i = 1; i < p.tableTickOption.list_check.length; i++) {
                                    if (p.tableTickOption.list_check[i] != 1) {
                                        Service.send_box_ThongBao_OK(p,
                                                p.tableTickOption.listP.get(i).name
                                                        + " chưa tick chọn");
                                        return;
                                    }
                                }
                                String name_ok = "";
                                for (int i = 0; i < p.tableTickOption.list_check.length; i++) {
                                    if (p.tableTickOption.list_check[i] == 1) {
                                        name_ok += p.tableTickOption.listP.get(i).name + ", ";
                                    }
                                }
                                Service.send_box_yesno(p, 2027, "Thông báo",
                                        ("Xác nhận tham gia Đại Chiến Đảo Đào Hoa với thành viên sau:\n"
                                                + name_ok),
                                        new String[] { "Đồng ý", "Huỷ" }, new byte[] { 2, 1 });
                            } else {
                                Message m = new Message(-74);
                                m.writer().writeByte(1);
                                m.writer().writeShort(5); // id dialog
                                m.writer().writeShort(p.index_map);
                                for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(
                                            p.tableTickOption.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                    }
                                    if (p.name.equals(p.tableTickOption.listP.get(i).name)) {
                                        p.tableTickOption.list_check[i] = 1;
                                    }
                                }
                                m.cleanup();
                            }
                        } else if (type == 2) { // huy
                            Message m = new Message(-74);
                            m.writer().writeByte(3);
                            m.writer().writeShort(5); // id dialog
                            m.writer().writeShort(p.index_map);
                            for (int i = 0; i < p.tableTickOption.listP.size(); i++) {
                                Player p0 = Map.get_player_by_name_allmap(
                                        p.tableTickOption.listP.get(i).name);
                                if (p0 != null) {
                                    p0.conn.addmsg(m);
                                }
                                if (p.name.equals(p.tableTickOption.listP.get(i).name)) {
                                    p.tableTickOption.list_check[i] = -1;
                                }
                            }
                            m.cleanup();
                            p.tableTickOption = null;
                        }
                        break;
                    }
                    case 1: { // lien tang
                        if (type == 1) { // leader clicked Start OR member clicked Ready
                            TableTickOption lobby = p.tableTickOption;
                            if (lobby == null)
                                return;
                            if (lobby.listP.get(0).name.equals(p.name)) {
                                // Leader clicked "Bắt đầu"
                                // Check if all members are ready (ticked)
                                for (int i = 1; i < lobby.list_check.length; i++) {
                                    if (lobby.list_check[i] != 1) {
                                        Service.send_box_ThongBao_OK(p,
                                                lobby.listP.get(i).name
                                                        + " chưa sẵn sàng!");
                                        return;
                                    }
                                }

                                // Validate all members
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player memInList = lobby.listP.get(i);
                                    Player member = Map.get_player_by_name_allmap(memInList.name);
                                    if (member == null || member.conn == null || !member.conn.connected) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + memInList.name + " hiện đang offline!");
                                        return;
                                    }
                                    if (member.map == null || !member.map.equals(p.map)) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " không ở cùng bản đồ với bạn!");
                                        return;
                                    }
                                    if (member.time_tower >= 5) {
                                        Service.send_box_ThongBao_OK(p, "Thành viên " + member.name
                                                + " đã vượt giới hạn Vượt Liên Tầng hôm nay (tối đa 5 lần)!");
                                        return;
                                    }
                                    if (member.get_key_boss() < 2) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " không đủ 2 chìa khóa!");
                                        return;
                                    }
                                    if (member.dungeon != null) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " đang trong một phó bản khác!");
                                        return;
                                    }
                                }

                                // Create list of participants and start challenge
                                java.util.List<Player> onlineMembers = new java.util.ArrayList<>();
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player memInList = lobby.listP.get(i);
                                    Player member = Map.get_player_by_name_allmap(memInList.name);
                                    if (member != null) {
                                        member.update_key_boss(-2);
                                        member.time_tower++;
                                        Service.CountDown_Ticket(member);
                                        member.update_money();
                                        member.originalMapId = member.map.template.id;
                                        member.originalX = member.x;
                                        member.originalY = member.y;
                                        onlineMembers.add(member);
                                    }
                                }

                                // Close dialog for everyone
                                Message m = new Message(-74);
                                m.writer().writeByte(2); // finish/close type
                                m.writer().writeShort(1); // id dialog
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(lobby.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                        p0.tableTickOption = null;
                                    }
                                }
                                m.cleanup();

                                activities.TowerChallenge tower = new activities.TowerChallenge(onlineMembers, p);
                                tower.createStage(0);

                            } else {
                                // Member clicked Ready (Tick)
                                Message m = new Message(-74);
                                m.writer().writeByte(1);
                                m.writer().writeShort(1); // id dialog
                                m.writer().writeShort(p.index_map);
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(
                                            lobby.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                    }
                                    if (p.name.equals(lobby.listP.get(i).name)) {
                                        lobby.list_check[i] = 1;
                                    }
                                }
                                m.cleanup();
                            }
                        } else if (type == 2) { // Cancel clicked
                            Message m = new Message(-74);
                            m.writer().writeByte(3);
                            m.writer().writeShort(1); // id dialog
                            m.writer().writeShort(p.index_map);

                            TableTickOption lobby = p.tableTickOption;
                            boolean isLeader = lobby.listP.get(0).name.equals(p.name);
                            for (int i = 0; i < lobby.listP.size(); i++) {
                                Player p0 = Map.get_player_by_name_allmap(lobby.listP.get(i).name);
                                if (p0 != null) {
                                    p0.conn.addmsg(m);
                                    if (isLeader || p0.name.equals(p.name)) {
                                        p0.tableTickOption = null;
                                    }
                                }
                                if (p.name.equals(lobby.listP.get(i).name)) {
                                    lobby.list_check[i] = -1;
                                }
                            }
                        }
                        break;
                    }
                    case 2: { // Bao ve kho bau Namie
                        if (type == 1) { // leader clicked Start OR member clicked Ready
                            TableTickOption lobby = p.tableTickOption;
                            if (lobby == null)
                                return;
                            if (lobby.listP.get(0).name.equals(p.name)) {
                                // Leader clicked "Bắt đầu"
                                // Check if all members are ready (ticked)
                                for (int i = 1; i < lobby.list_check.length; i++) {
                                    if (lobby.list_check[i] != 1) {
                                        Service.send_box_ThongBao_OK(p,
                                                lobby.listP.get(i).name
                                                        + " chưa sẵn sàng!");
                                        return;
                                    }
                                }

                                // Validate all members
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player memInList = lobby.listP.get(i);
                                    Player member = Map.get_player_by_name_allmap(memInList.name);
                                    if (member == null || member.conn == null || !member.conn.connected) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + memInList.name + " hiện đang offline!");
                                        return;
                                    }
                                    if (member.map == null || !member.map.equals(p.map)) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " không ở cùng bản đồ với bạn!");
                                        return;
                                    }
                                    if (Math.abs(member.x - p.x) > 500 || Math.abs(member.y - p.y) > 500) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " đứng quá xa bạn!");
                                        return;
                                    }
                                    if (member.time_namie >= 5) {
                                        Service.send_box_ThongBao_OK(p, "Thành viên " + member.name
                                                + " đã vượt giới hạn Bảo vệ kho báu Namie hôm nay (tối đa 5 lần)!");
                                        return;
                                    }
                                    if (member.get_key_boss() < 2) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " không đủ 2 chìa khóa!");
                                        return;
                                    }
                                    if (member.dungeon != null) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " đang trong một phó bản khác!");
                                        return;
                                    }
                                }

                                // Create list of participants and start challenge
                                java.util.List<Player> onlineMembers = new java.util.ArrayList<>();
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player memInList = lobby.listP.get(i);
                                    Player member = Map.get_player_by_name_allmap(memInList.name);
                                    if (member != null) {
                                        member.update_key_boss(-2);
                                        member.time_namie++;
                                        Service.CountDown_Ticket(member);
                                        member.update_money();
                                        member.originalMapId = member.map.template.id;
                                        member.originalX = member.x;
                                        member.originalY = member.y;
                                        onlineMembers.add(member);
                                    }
                                }

                                // Close dialog for everyone
                                Message m = new Message(-74);
                                m.writer().writeByte(2); // finish/close type
                                m.writer().writeShort(2); // id dialog
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(lobby.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                        p0.tableTickOption = null;
                                    }
                                }
                                m.cleanup();

                                activities.NamieTreasureDefense defense = new activities.NamieTreasureDefense(
                                        onlineMembers, p);
                                defense.start();

                            } else {
                                // Member clicked Ready (Tick)
                                Message m = new Message(-74);
                                m.writer().writeByte(1);
                                m.writer().writeShort(2); // id dialog
                                m.writer().writeShort(p.index_map);
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(
                                            lobby.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                    }
                                    if (p.name.equals(lobby.listP.get(i).name)) {
                                        lobby.list_check[i] = 1;
                                    }
                                }
                                m.cleanup();
                            }
                        } else if (type == 2) { // Cancel clicked
                            Message m = new Message(-74);
                            m.writer().writeByte(3);
                            m.writer().writeShort(2); // id dialog
                            m.writer().writeShort(p.index_map);

                            TableTickOption lobby = p.tableTickOption;
                            boolean isLeader = lobby.listP.get(0).name.equals(p.name);
                            for (int i = 0; i < lobby.listP.size(); i++) {
                                Player p0 = Map.get_player_by_name_allmap(lobby.listP.get(i).name);
                                if (p0 != null) {
                                    p0.conn.addmsg(m);
                                    if (isLeader || p0.name.equals(p.name)) {
                                        p0.tableTickOption = null;
                                    }
                                }
                                if (p.name.equals(lobby.listP.get(i).name)) {
                                    lobby.list_check[i] = -1;
                                }
                            }
                            m.cleanup();
                            p.tableTickOption = null;
                        }
                        break;
                    }
                    case 3: { // Hang Động
                        if (type == 1) { // leader clicked Start OR member clicked Ready
                            TableTickOption lobby = p.tableTickOption;
                            if (lobby == null)
                                return;
                            if (lobby.listP.get(0).name.equals(p.name)) {
                                // Leader clicked "Bắt đầu"
                                for (int i = 1; i < lobby.list_check.length; i++) {
                                    if (lobby.list_check[i] != 1) {
                                        Service.send_box_ThongBao_OK(p, lobby.listP.get(i).name + " chưa sẵn sàng!");
                                        return;
                                    }
                                }

                                // Validate all members have keys
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player memInList = lobby.listP.get(i);
                                    Player member = Map.get_player_by_name_allmap(memInList.name);
                                    if (member == null || member.conn == null || !member.conn.connected) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + memInList.name + " hiện đang offline!");
                                        return;
                                    }
                                    if (member.get_key_boss() < 1) {
                                        Service.send_box_ThongBao_OK(p,
                                                "Thành viên " + member.name + " không đủ 1 chìa khóa!");
                                        return;
                                    }
                                    if (member.time_hangdong >= 5) {
                                        Service.send_box_ThongBao_OK(p, "Thành viên " + member.name
                                                + " đã vượt giới hạn Hang Động hôm nay (tối đa 5 lần)!");
                                        return;
                                    }
                                }

                                java.util.List<Player> onlineMembers = new java.util.ArrayList<>();
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player memInList = lobby.listP.get(i);
                                    Player member = Map.get_player_by_name_allmap(memInList.name);
                                    if (member != null) {
                                        member.update_key_boss(-1);
                                        member.time_hangdong++;
                                        Service.CountDown_Ticket(member);
                                        member.update_money();
                                        onlineMembers.add(member);
                                    }
                                }

                                Message m = new Message(-74);
                                m.writer().writeByte(2); // finish/close type
                                m.writer().writeShort(3); // id dialog
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(lobby.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                        p0.tableTickOption = null;
                                    }
                                }
                                m.cleanup();

                                int startStage = -1;
                                for (Player mem : onlineMembers) {
                                    if (startStage == -1 || mem.hangdong_stage < startStage) {
                                        startStage = mem.hangdong_stage;
                                    }
                                }
                                if (startStage == -1)
                                    startStage = 0;
                                activities.HangDong hd = new activities.HangDong(onlineMembers, p);
                                hd.createStage(startStage);

                            } else {
                                // Member clicked Ready (Tick)
                                Message m = new Message(-74);
                                m.writer().writeByte(1);
                                m.writer().writeShort(3); // id dialog
                                m.writer().writeShort(p.index_map);
                                for (int i = 0; i < lobby.listP.size(); i++) {
                                    Player p0 = Map.get_player_by_name_allmap(lobby.listP.get(i).name);
                                    if (p0 != null) {
                                        p0.conn.addmsg(m);
                                    }
                                    if (p.name.equals(lobby.listP.get(i).name)) {
                                        lobby.list_check[i] = 1;
                                    }
                                }
                                m.cleanup();
                            }
                        } else if (type == 2) { // Cancel clicked
                            Message m = new Message(-74);
                            m.writer().writeByte(3);
                            m.writer().writeShort(3); // id dialog
                            m.writer().writeShort(p.index_map);

                            TableTickOption lobby = p.tableTickOption;
                            boolean isLeader = lobby.listP.get(0).name.equals(p.name);
                            for (int i = 0; i < lobby.listP.size(); i++) {
                                Player p0 = Map.get_player_by_name_allmap(lobby.listP.get(i).name);
                                if (p0 != null) {
                                    p0.conn.addmsg(m);
                                    if (isLeader || p0.name.equals(p.name)) {
                                        p0.tableTickOption = null;
                                    }
                                }
                                if (p.name.equals(lobby.listP.get(i).name)) {
                                    lobby.list_check[i] = -1;
                                }
                            }
                            m.cleanup();
                            if (isLeader) {
                                p.tableTickOption = null;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
