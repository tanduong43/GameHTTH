package activities;

import client.Clan;
import client.Player;
import io.Message;
import map.Map;
import template.GiftBox;
import template.ItemTemplate4;
import template.ItemTemplate7;
import java.util.ArrayList;
import java.util.List;

public class PvpClan {
    public static final int MAP_PVP_CLAN = 123;
    public static final long TIME_PVP_CLAN = 5 * 60 * 1000L; // 5 phút (300 giây)

    public static List<Clan> LIST = new ArrayList<>();

    public synchronized static void add_clan_wait(Clan clan) {
        if (clan != null && !LIST.contains(clan)) {
            LIST.add(clan);
        }
    }

    public synchronized static void remove_clan_wait(Clan clan) {
        if (clan != null) {
            LIST.remove(clan);
        }
    }

    /**
     * Kiểm tra thời gian mở Phó bản PVP Băng:
     * Thứ 2, Thứ 4, Thứ 6 trong 2 khung giờ:
     * - 12h00 đến 13h00 (12:00 - 12:59)
     * - 20h00 đến 21h00 (20:00 - 20:59)
     * (Trong Joda-Time: 1 = Thứ 2, 3 = Thứ 4, 5 = Thứ 6)
     */
    public static boolean is_open_pvp_clan() {
        org.joda.time.DateTime now = org.joda.time.DateTime.now();
        int day = now.getDayOfWeek();
        int hour = now.getHourOfDay();
        return (day == 1 || day == 3 || day == 5) && (hour == 12 || hour == 20);
    }

    /**
     * Gửi bảng điểm số kill và thời gian đếm ngược cho tất cả người chơi trong Map PVP Băng
     */
    public static void send_pvp_clan_score(Map map) {
        if (map != null && map.map_pvp_clan != null) {
            int countDown = (int) Math.max(0, (map.map_pvp_clan.time_end - System.currentTimeMillis()) / 1000L);
            Message m = new Message(-73);
            try {
                m.writer().writeByte(4); // type 4: Hiển thị bảng tỷ số 2 bên và thời gian đếm ngược
                m.writer().writeShort((short) countDown);
                m.writer().writeByte((byte) Math.min(127, map.map_pvp_clan.score_clan1)); // Kill Băng 1 (Cờ Đỏ)
                m.writer().writeByte((byte) Math.min(127, map.map_pvp_clan.score_clan2)); // Kill Băng 2 (Cờ Xanh)
                for (int i = 0; i < map.players.size(); i++) {
                    Player p = map.players.get(i);
                    if (p != null && p.conn != null) {
                        p.conn.addmsg(m);
                    }
                }
                m.cleanup();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Gửi bảng điểm số kill và thời gian đếm ngược cho 1 người chơi cụ thể (khi vừa vào map / vào lại)
     */
    public static void send_pvp_clan_score(Player p, Map map) {
        if (p != null && p.conn != null && map != null && map.map_pvp_clan != null) {
            int countDown = (int) Math.max(0, (map.map_pvp_clan.time_end - System.currentTimeMillis()) / 1000L);
            Message m = new Message(-73);
            try {
                m.writer().writeByte(4);
                m.writer().writeShort((short) countDown);
                m.writer().writeByte((byte) Math.min(127, map.map_pvp_clan.score_clan1));
                m.writer().writeByte((byte) Math.min(127, map.map_pvp_clan.score_clan2));
                p.conn.addmsg(m);
                m.cleanup();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Xóa bảng điểm đếm ngược trên màn hình của người chơi khi rời map
     */
    public static void clear_pvp_clan_score(Player p) {
        if (p != null && p.conn != null) {
            Message m = new Message(-73);
            try {
                m.writer().writeByte(4);
                m.writer().writeShort(0);
                m.writer().writeByte(0);
                m.writer().writeByte(0);
                p.conn.addmsg(m);
                m.cleanup();
            } catch (Exception e) {
            }
        }
    }

    /**
     * Gửi đồng hồ đếm ngược hồi sinh cho player bị chết trong PVP Băng
     * Dùng typeTime = 3 (watchRevice) với short countDown (giây) và string tiêu đề
     */
    public static void send_revive_countdown(Player p, int seconds) {
        if (p != null && p.conn != null) {
            Message m = new Message(-73);
            try {
                m.writer().writeByte(3); // typeTime = 3: watchRevice (đồng hồ đếm ngược hồi sinh)
                m.writer().writeShort((short) Math.max(0, seconds));
                m.writer().writeUTF("Hồi sinh sau");
                p.conn.addmsg(m);
                m.cleanup();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Gửi đồng hồ đếm ngược trở về làng khi kết thúc trận (8 giây)
     */
    public static void send_return_village_countdown(Player p, int seconds) {
        if (p != null && p.conn != null) {
            Message m = new Message(-73);
            try {
                m.writer().writeByte(3); // typeTime = 3: watchRevice
                m.writer().writeShort((short) Math.max(0, seconds));
                m.writer().writeUTF("Về làng sau");
                p.conn.addmsg(m);
                m.cleanup();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Tạo danh sách quà cho Bảng nhận quà (chỉ hiển thị quà của Bang: XP Băng và Ruby Băng)
     * @param p Player nhận quà
     * @param xp Số XP Băng nhận được
     * @param rb Số Ruby Băng nhận được
     */
    public static List<GiftBox> get_gift_pvp_clan(Player p, int xp, int rb) {
        List<GiftBox> list_gift = new ArrayList<>();

        // 1. XP Băng
        GiftBox gbExp = new GiftBox();
        gbExp.id = -1; // id = -1 để không cộng exp nhân vật
        gbExp.type = 99; // type = 99 để Client hiển thị icon EXP
        gbExp.name = "XP Băng";
        gbExp.icon = 669;
        gbExp.num = xp;
        gbExp.color = 0;
        list_gift.add(gbExp);

        // 2. Ruby Băng
        GiftBox gbRuby = new GiftBox();
        gbRuby.id = -1; // id = -1 để không cộng ruby nhân vật
        gbRuby.type = 4; // type = 4 để Client hiển thị icon Potion/Ruby
        gbRuby.name = "Ruby Băng";
        gbRuby.icon = 385; // Icon Ruby
        gbRuby.num = rb;
        gbRuby.color = 0;
        list_gift.add(gbRuby);

        return list_gift;
    }
}
