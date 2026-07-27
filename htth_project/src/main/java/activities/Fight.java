package activities;

import java.io.IOException;
import client.Player;
import client.Quest;
import core.Service;
import core.Util;
import io.Message;
import map.Map;
import template.Map_pvp;
/**
 *
 * @author Truongbk
 */
public class Fight {
    // ID dùng cho ClientInput để nhận số ruby cược
    public static final int INPUT_ID_FIGHT_RUBY = 351;

    public synchronized static void process(Player p, Message m2) throws IOException {
        byte type = m2.reader().readByte();
        short id = m2.reader().readShort();
        byte typeFight = m2.reader().readByte();
        // type=0: gửi lời mời thách đấu. typeFight: 0=giao hữu, 1=siêu hạng (từ client)
        if (type == 0) {
            Player p0 = p.map.get_player_by_id_inmap(id);
            if (p0 != null && id != p.index_map) {
                if (p0.targetFight != null) {
                    Service.send_box_ThongBao_OK(p, "Đối phương đang nhận lời mời từ người khác");
                } else if (typeFight == 1) {
                    // Siêu hạng: yêu cầu nhập số ruby muốn cược trước
                    if (p.get_ngoc() <= 0) {
                        Service.send_box_ThongBao_OK(p, "Bạn không có ruby để thách đấu!");
                        return;
                    }
                    p.fight_click_target = p0;
                    Service.input_text(p, INPUT_ID_FIGHT_RUBY,
                            "Thách đấu siêu hạng với " + p0.name,
                            new String[]{"Số ruby muốn cược (bạn có " + p.get_ngoc() + " ruby)"});
                } else {
                    // Giao hữu: gửi lời mời ngay, không cần ruby
                    sendFightInvite(p, p0, 0, 0);
                }
            } else {
                Service.send_box_ThongBao_OK(p, "Đối phương offline");
            }
        } else if (type == 1 && p.targetFight != null
                && p.targetFight.map.equals(p.map)) {
            // typeFight từ client là loại trận đấu (0=giao hữu, 1=siêu hạng)
            byte pvpMode = typeFight;
            Player challenger = p.targetFight; // người gửi lời mời

            if (pvpMode == 1) {
                // Siêu hạng cá cược: lấy số ruby từ người gửi lời mời
                int rubyBet = challenger.fight_ruby_bet;
                if (rubyBet <= 0) {
                    Service.send_box_ThongBao_OK(p, "Lời mời không hợp lệ!");
                    p.targetFight = null;
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                // Kiểm tra ruby cả hai bên
                if (challenger.get_ngoc() < rubyBet) {
                    Service.send_box_ThongBao_OK(p, "Người thách đấu không còn đủ ruby!");
                    Service.send_box_ThongBao_OK(challenger, "Bạn không còn đủ " + rubyBet + " ruby để thách đấu!");
                    p.targetFight = null;
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                if (p.get_ngoc() < rubyBet) {
                    Service.send_box_ThongBao_OK(p, "Bạn không đủ " + rubyBet + " ruby để chấp nhận thách đấu!");
                    Service.send_box_ThongBao_OK(challenger, p.name + " không đủ ruby để chấp nhận lời thách đấu.");
                    p.targetFight = null;
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                // Trừ ruby cả hai bên trước khi vào trận
                challenger.update_ngoc(-rubyBet);
                challenger.update_money();
                p.update_ngoc(-rubyBet);
                p.update_money();
                //
                startFightMap(p, challenger, (byte) 3, rubyBet);
                challenger.fight_ruby_bet = 0;
            } else {
                // Giao hữu: không cần ruby
                if (challenger.targetFight != null) {
                    challenger.targetFight = null;
                    p.targetFight = null;
                    Service.send_box_ThongBao_OK(p, "Đối phương đang nhận lời mời từ người khác, hãy thử lại sau");
                    return;
                }
                startFightMap(p, challenger, (byte) 1, 0);
            }
        }
    }

    /** Gửi lời mời thách đấu cho p0, kèm số ruby cược */
    public static void sendFightInvite(Player p, Player p0, int rubyBet, int typeFight) throws IOException {
        Message m = new Message(-35);
        m.writer().writeByte(0);
        m.writer().writeShort(p.index_map);
        m.writer().writeUTF(p.name);
        m.writer().writeShort((short) Math.min(rubyBet, 32767)); // priceFight hiển thị trên client
        m.writer().writeByte(typeFight); // 0=giao hữu, 1=siêu hạng
        p0.conn.addmsg(m);
        m.cleanup();
        p0.targetFight = p;
        p.fight_ruby_bet = rubyBet;
    }

    /** Tạo map đấu và đưa cả hai người vào */
    private static void startFightMap(Player acceptor, Player challenger, byte typeMap, int rubyBet) throws IOException {
        challenger.targetFight = acceptor;
        //
        acceptor.map.leave_map(acceptor, 2);
        challenger.map.leave_map(challenger, 2);
        acceptor.type_pk = -1;
        challenger.type_pk = -1;
        //
        short[] mapID = new short[]{120, 122, 123};
        Map maptemp = Map.get_map_by_id(mapID[Util.random(mapID.length)])[0];
        Map map_create = new Map();
        map_create.template = maptemp.template;
        map_create.zone_id = (byte) 0;
        map_create.list_mob = new int[0];
        //
        acceptor.map = map_create;
        acceptor.x = 320;
        acceptor.y = 240;
        acceptor.xold = acceptor.x;
        acceptor.yold = acceptor.y;
        acceptor.map.goto_map(acceptor);
        Service.update_PK(acceptor, acceptor, true);
        Service.pet(acceptor, acceptor, true);
        Quest.update_map_have_side_quest(acceptor, true);
        //
        challenger.map = map_create;
        challenger.x = 380;
        challenger.y = 240;
        challenger.xold = challenger.x;
        challenger.yold = challenger.y;
        challenger.map.goto_map(challenger);
        Service.update_PK(challenger, challenger, true);
        Service.pet(challenger, challenger, true);
        Quest.update_map_have_side_quest(challenger, true);
        //
        map_create.map_pvp = new Map_pvp();
        map_create.map_pvp.time_pvp = 5;
        map_create.map_pvp.status_pvp = 0;
        map_create.map_pvp.num_win_p1 = 0;
        map_create.map_pvp.num_win_p2 = 0;
        map_create.map_pvp.type_map = typeMap;
        map_create.map_pvp.ruby_bet = rubyBet;
        map_create.start_map();
        Map.add_map_plus(map_create);
    }
}
