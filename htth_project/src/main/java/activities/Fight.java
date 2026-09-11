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
    // Mức cược tối đa cho Thách đấu siêu hạng
    public static final int MAX_RUBY_BET = 100;
    // Giới hạn số lần tham gia thách đấu siêu hạng mỗi ngày
    public static final int MAX_FIGHT_SUPER_DAILY = 5;

    public synchronized static void process(Player p, Message m2) throws IOException {
        try {
            byte type = m2.reader().readByte();
            short id = m2.reader().readShort();
            byte typeFight = m2.reader().readByte();
            // type=0: gửi lời mời thách đấu. typeFight: 0=giao hữu, 1=siêu hạng (từ client)
            if (type == 0) {
                if (p.map == null) {
                    return;
                }
                Player p0 = p.map.get_player_by_id_inmap(id);
                if (p0 != null && id != p.index_map && p0.conn != null && p0.conn.connected) {
                    if (p.map.map_pvp != null || p0.map == null || p0.map.map_pvp != null) {
                        Service.send_box_ThongBao_OK(p, "Không thể thách đấu khi đang trong trận chiến!");
                        return;
                    }
                    if (p.isdie || p0.isdie) {
                        Service.send_box_ThongBao_OK(p, "Không thể thách đấu khi đang kiệt sức!");
                        return;
                    }
                    if (p.trade_target != null || p0.trade_target != null) {
                        Service.send_box_ThongBao_OK(p, "Không thể thách đấu khi đang có giao dịch!");
                        return;
                    }
                    if (p.ship_pet != null || p0.ship_pet != null) {
                        Service.send_box_ThongBao_OK(p, "Không thể thách đấu khi đang vận chuyển hàng!");
                        return;
                    }

                    // Tự động dọn dẹp nếu lời mời trước đó của p0 đã hết hạn hoặc không còn hợp lệ
                    if (p0.targetFight != null) {
                        if (p0.targetFight.conn == null || !p0.targetFight.conn.connected
                                || p0.targetFight.map == null || !p0.targetFight.map.equals(p0.map)
                                || p0.targetFight.map.map_pvp != null || p0.targetFight.isdie
                                || (p0.time_fight_invite > 0 && System.currentTimeMillis() > p0.time_fight_invite)) {
                            p0.targetFight = null;
                            p0.time_fight_invite = 0;
                        }
                    }

                    if (p0.targetFight != null) {
                        Service.send_box_ThongBao_OK(p, "Đối phương đang nhận lời mời từ người khác");
                        return;
                    }

                    if (typeFight == 1) {
                        p.change_new_date();
                        p0.change_new_date();

                        // Thách đấu siêu hạng: Chỉ thành viên đã mở mới được tham gia
                        if (p.conn == null || p.conn.status != 1) {
                            Service.send_box_ThongBao_OK(p, "Chỉ thành viên đã mở (kích hoạt) mới có thể tham gia thách đấu siêu hạng!");
                            return;
                        }
                        if (p0.conn == null || p0.conn.status != 1) {
                            Service.send_box_ThongBao_OK(p, "Đối phương chưa mở thành viên, không thể tham gia thách đấu siêu hạng!");
                            return;
                        }

                        // Giới hạn 5 lần/ngày
                        if (p.time_fight_super >= MAX_FIGHT_SUPER_DAILY) {
                            Service.send_box_ThongBao_OK(p, "Bạn đã tham gia đủ " + MAX_FIGHT_SUPER_DAILY + " lượt thách đấu siêu hạng hôm nay!");
                            return;
                        }
                        if (p0.time_fight_super >= MAX_FIGHT_SUPER_DAILY) {
                            Service.send_box_ThongBao_OK(p, "Đối phương đã tham gia đủ " + MAX_FIGHT_SUPER_DAILY + " lượt thách đấu siêu hạng hôm nay!");
                            return;
                        }

                        if (p.get_ngoc() <= 0) {
                            Service.send_box_ThongBao_OK(p, "Bạn không có ruby để thách đấu siêu hạng!");
                            return;
                        }
                        if (p0.get_ngoc() <= 0) {
                            Service.send_box_ThongBao_OK(p, "Đối phương không có ruby để thách đấu siêu hạng!");
                            return;
                        }
                        p.fight_click_target = p0;
                        int remainP = MAX_FIGHT_SUPER_DAILY - p.time_fight_super;
                        Service.input_text(p, INPUT_ID_FIGHT_RUBY,
                                "Thách đấu siêu hạng với " + p0.name,
                                new String[]{"Số ruby muốn cược (1 - " + MAX_RUBY_BET + " ruby, bạn có " + p.get_ngoc() + " ruby, còn " + remainP + "/" + MAX_FIGHT_SUPER_DAILY + " lượt):"});
                    } else {
                        // Giao hữu: gửi lời mời ngay, không cần ruby
                        sendFightInvite(p, p0, 0, 0);
                        Service.send_box_ThongBao_OK(p, "Đã gửi lời mời thách đấu giao hữu tới " + p0.name + "!");
                    }
                } else {
                    Service.send_box_ThongBao_OK(p, "Đối phương offline");
                }
            } else if (type == 1 && p.targetFight != null) {
                Player challenger = p.targetFight; // người gửi lời mời
                p.targetFight = null;
                p.time_fight_invite = 0;

                if (challenger.conn == null || !challenger.conn.connected) {
                    Service.send_box_ThongBao_OK(p, "Đối phương đã mất kết nối!");
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                if (p.map == null || challenger.map == null || !p.map.equals(challenger.map)) {
                    Service.send_box_ThongBao_OK(p, "Đối phương không còn ở cùng khu vực!");
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                if (p.map.map_pvp != null || challenger.map.map_pvp != null) {
                    Service.send_box_ThongBao_OK(p, "Không thể thách đấu khi đang trong lôi đài!");
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                if (p.isdie || challenger.isdie) {
                    Service.send_box_ThongBao_OK(p, "Không thể chấp nhận khi có người đang kiệt sức!");
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                if (p.trade_target != null || challenger.trade_target != null) {
                    Service.send_box_ThongBao_OK(p, "Không thể chấp nhận khi đang có giao dịch!");
                    challenger.fight_ruby_bet = 0;
                    return;
                }
                if (p.ship_pet != null || challenger.ship_pet != null) {
                    Service.send_box_ThongBao_OK(p, "Không thể chấp nhận khi đang vận chuyển hàng!");
                    challenger.fight_ruby_bet = 0;
                    return;
                }

                int rubyBet = challenger.fight_ruby_bet;
                boolean isRubyFight = (rubyBet > 0 || typeFight == 1);

                if (isRubyFight) {
                    p.change_new_date();
                    challenger.change_new_date();

                    // Kiểm tra thành viên của cả 2 bên
                    if (p.conn == null || p.conn.status != 1) {
                        Service.send_box_ThongBao_OK(p, "Chỉ thành viên đã mở (kích hoạt) mới được tham gia thách đấu siêu hạng!");
                        Service.send_box_ThongBao_OK(challenger, p.name + " chưa mở thành viên, không thể tham gia thách đấu siêu hạng!");
                        challenger.fight_ruby_bet = 0;
                        return;
                    }
                    if (challenger.conn == null || challenger.conn.status != 1) {
                        Service.send_box_ThongBao_OK(p, "Người thách đấu chưa mở thành viên, không thể tham gia thách đấu siêu hạng!");
                        Service.send_box_ThongBao_OK(challenger, "Chỉ thành viên đã mở (kích hoạt) mới được tham gia thách đấu siêu hạng!");
                        challenger.fight_ruby_bet = 0;
                        return;
                    }

                    // Giới hạn 5 lần/ngày
                    if (p.time_fight_super >= MAX_FIGHT_SUPER_DAILY) {
                        Service.send_box_ThongBao_OK(p, "Bạn đã tham gia đủ " + MAX_FIGHT_SUPER_DAILY + " lượt thách đấu siêu hạng hôm nay!");
                        Service.send_box_ThongBao_OK(challenger, p.name + " đã tham gia đủ " + MAX_FIGHT_SUPER_DAILY + " lượt thách đấu siêu hạng hôm nay!");
                        challenger.fight_ruby_bet = 0;
                        return;
                    }
                    if (challenger.time_fight_super >= MAX_FIGHT_SUPER_DAILY) {
                        Service.send_box_ThongBao_OK(p, "Người thách đấu đã tham gia đủ " + MAX_FIGHT_SUPER_DAILY + " lượt thách đấu siêu hạng hôm nay!");
                        Service.send_box_ThongBao_OK(challenger, "Bạn đã tham gia đủ " + MAX_FIGHT_SUPER_DAILY + " lượt thách đấu siêu hạng hôm nay!");
                        challenger.fight_ruby_bet = 0;
                        return;
                    }

                    // Kiểm tra mức cược hợp lệ (1 - MAX_RUBY_BET ruby)
                    if (rubyBet <= 0 || rubyBet > MAX_RUBY_BET) {
                        Service.send_box_ThongBao_OK(p, "Mức cược không hợp lệ (tối đa " + MAX_RUBY_BET + " ruby)!");
                        Service.send_box_ThongBao_OK(challenger, "Mức cược không hợp lệ (tối đa " + MAX_RUBY_BET + " ruby)!");
                        challenger.fight_ruby_bet = 0;
                        return;
                    }

                    // Kiểm tra số ruby của 2 bên
                    if (challenger.get_ngoc() < rubyBet) {
                        Service.send_box_ThongBao_OK(p, "Người thách đấu không còn đủ ruby!");
                        Service.send_box_ThongBao_OK(challenger, "Bạn không còn đủ " + rubyBet + " ruby để thách đấu!");
                        challenger.fight_ruby_bet = 0;
                        return;
                    }
                    if (p.get_ngoc() < rubyBet) {
                        Service.send_box_ThongBao_OK(p, "Bạn không đủ " + rubyBet + " ruby để chấp nhận thách đấu!");
                        Service.send_box_ThongBao_OK(challenger, p.name + " không đủ ruby để chấp nhận lời thách đấu.");
                        challenger.fight_ruby_bet = 0;
                        return;
                    }

                    // Trừ ruby cả hai bên trước khi vào trận
                    challenger.update_ngoc(-rubyBet);
                    challenger.update_money();
                    p.update_ngoc(-rubyBet);
                    p.update_money();
                    challenger.fight_ruby_bet = 0;

                    // Tăng số lần tham gia thách đấu siêu hạng trong ngày
                    challenger.time_fight_super++;
                    p.time_fight_super++;

                    startFightMap(p, challenger, (byte) 3, rubyBet);
                } else {
                    challenger.fight_ruby_bet = 0;
                    startFightMap(p, challenger, (byte) 1, 0);
                }
            } else if (type == 2 || type == -1) {
                // Từ chối thách đấu hoặc hủy
                if (p.targetFight != null) {
                    Player challenger = p.targetFight;
                    challenger.fight_ruby_bet = 0;
                    try {
                        Service.send_box_ThongBao_OK(challenger, p.name + " đã từ chối lời mời thách đấu.");
                    } catch (Exception ignored) {}
                    p.targetFight = null;
                    p.time_fight_invite = 0;
                }
                p.fight_ruby_bet = 0;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /** Gửi lời mời thách đấu cho p0, kèm số ruby cược */
    public static void sendFightInvite(Player p, Player p0, int rubyBet, int typeFight) {
        try {
            p0.targetFight = p;
            p0.time_fight_invite = System.currentTimeMillis() + 30000L; // Lời mời có hiệu lực trong 30 giây
            p.fight_ruby_bet = rubyBet;

            Message m = new Message(-35);
            m.writer().writeByte(0);
            m.writer().writeShort(p.index_map);
            m.writer().writeUTF(p.name);
            m.writer().writeShort((short) Math.min(rubyBet, 32767)); // priceFight hiển thị trên client
            m.writer().writeByte(typeFight); // 0=giao hữu, 1=siêu hạng
            p0.conn.addmsg(m);
            m.cleanup();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /** Tạo map đấu và đưa cả hai người vào */
    private static void startFightMap(Player acceptor, Player challenger, byte typeMap, int rubyBet) {
        try {
            acceptor.targetFight = challenger;
            challenger.targetFight = acceptor;
            //
            if (acceptor.map != null) {
                acceptor.originalMapId = acceptor.map.template.id;
                acceptor.originalX = acceptor.x;
                acceptor.originalY = acceptor.y;
                acceptor.map.leave_map(acceptor, 2);
            }
            if (challenger.map != null) {
                challenger.originalMapId = challenger.map.template.id;
                challenger.originalX = challenger.x;
                challenger.originalY = challenger.y;
                challenger.map.leave_map(challenger, 2);
            }
            acceptor.type_pk = -1;
            challenger.type_pk = -1;
            acceptor.isdie = false;
            challenger.isdie = false;
            //
            short[] mapID = new short[]{120, 122, 123};
            Map maptemp = Map.get_map_by_id(mapID[Util.random(mapID.length)])[0];
            Map map_create = new Map();
            map_create.template = maptemp.template;
            map_create.zone_id = (byte) 0;
            map_create.list_mob = new int[0];
            // Khởi tạo map_pvp trước khi đưa người chơi vào map
            map_create.map_pvp = new Map_pvp();
            map_create.map_pvp.time_pvp = 5;
            map_create.map_pvp.status_pvp = 0;
            map_create.map_pvp.num_win_p1 = 0;
            map_create.map_pvp.num_win_p2 = 0;
            map_create.map_pvp.type_map = typeMap;
            map_create.map_pvp.ruby_bet = rubyBet;
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
            map_create.start_map();
            Map.add_map_plus(map_create);
        } catch (Exception e) {
            e.printStackTrace();
            // Rollback ruby and daily count if it was ruby bet
            if (rubyBet > 0) {
                try {
                    challenger.update_ngoc(rubyBet);
                    challenger.update_money();
                    acceptor.update_ngoc(rubyBet);
                    acceptor.update_money();
                    if (challenger.time_fight_super > 0) challenger.time_fight_super--;
                    if (acceptor.time_fight_super > 0) acceptor.time_fight_super--;
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
            try {
                Service.send_box_ThongBao_OK(acceptor, "Có lỗi xảy ra khi bắt đầu trận đấu!");
                Service.send_box_ThongBao_OK(challenger, "Có lỗi xảy ra khi bắt đầu trận đấu!");
            } catch (Exception ignored) {}
            acceptor.targetFight = null;
            challenger.targetFight = null;
        }
    }
}
