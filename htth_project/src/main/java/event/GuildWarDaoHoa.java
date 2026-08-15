package event;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import client.Clan;
import client.Player;
import core.Service;
import database.SQL;
import map.Map;
import template.Item_wear;

/**
 * HOẠT ĐỘNG: ĐẠI CHIẾN CHIẾM ĐẢO ĐÀO HOA
 * Guild War lãnh địa - Thứ 3, Thứ 5, Chủ Nhật (19:00 - 20:00)
 */
public class GuildWarDaoHoa implements Runnable {

    // ======== CẤU HÌNH SỰ KIỆN ========
    private static GuildWarDaoHoa instance;
    public static boolean IS_OPEN = true; // Mở sẵn cho test 24/7
    private static final boolean DEBUG_MODE = true;

    public static final int MAP_DAO_DAO_HOA = 2027;

    private static final int START_HOUR = 19;
    private static final int START_MINUTE = 0;
    private static final int END_HOUR = 20;
    private static final int END_MINUTE = 0;

    public static final int REWARD_WIN_CLAN_EXP = 1000;
    public static final int REWARD_WIN_CLAN_RUBY = 100;
    public static final int REWARD_LOSE_CLAN_EXP = 500;

    public static final int ITEM_HOP_TRANG_PHUC = 356;
    public static final int REWARD_WIN_RUBY_EVENT = 500;

    // Hàng chờ ghép đấu Bang
    public static final List<Clan> LIST_CLAN_WAIT = new ArrayList<>();

    public synchronized static void add_clan_wait(Clan clan) {
        if (clan != null && !LIST_CLAN_WAIT.contains(clan)) {
            LIST_CLAN_WAIT.add(clan);
            System.out.println("[GuildWarDaoHoa] Băng " + clan.name + " đã vào hàng chờ ghép Đại Chiến Đảo Đào Hoa (Hiện có: " + LIST_CLAN_WAIT.size() + " bang)");
        }
    }

    public synchronized static void remove_clan_wait(Clan clan) {
        if (clan != null) {
            LIST_CLAN_WAIT.remove(clan);
        }
    }

    private ScheduledExecutorService scheduler;

    public static GuildWarDaoHoa getInstance() {
        if (instance == null) {
            instance = new GuildWarDaoHoa();
        }
        return instance;
    }

    public void init() {
        if (scheduler != null) {
            scheduler.shutdown();
        }
        scheduler = Executors.newScheduledThreadPool(1);
        scheduler.scheduleAtFixedRate(this, 0, 1, TimeUnit.MINUTES);
        System.out.println("[GuildWarDaoHoa] Scheduler started!");
    }

    @Override
    public void run() {
        if (DEBUG_MODE) {
            IS_OPEN = true;
            return;
        }

        Calendar cal = Calendar.getInstance();
        int hour = cal.get(Calendar.HOUR_OF_DAY);
        int minute = cal.get(Calendar.MINUTE);
        int dayOfWeek = cal.get(Calendar.DAY_OF_WEEK);

        // Thứ 3 (3), Thứ 5 (5), Chủ Nhật (1)
        boolean isEventDay = (dayOfWeek == Calendar.TUESDAY || dayOfWeek == Calendar.THURSDAY || dayOfWeek == Calendar.SUNDAY);

        if (isEventDay && hour >= START_HOUR && hour < END_HOUR) {
            IS_OPEN = true;
        } else {
            IS_OPEN = false;
            LIST_CLAN_WAIT.clear();
        }
    }

    /**
     * Trao thưởng khi kết thúc trận chiến Đại Chiến Đảo Đào Hoa
     */
    public static void finishWar(Clan winnerClan, Clan loserClan, List<Player> playersInMap) {
        try {
            if (winnerClan != null) {
                winnerClan.update_xp(REWARD_WIN_CLAN_EXP);
                winnerClan.update_ruby(REWARD_WIN_CLAN_RUBY);
                saveClan(winnerClan);
            }
            if (loserClan != null) {
                loserClan.update_xp(REWARD_LOSE_CLAN_EXP);
                saveClan(loserClan);
            }

            boolean isEventTet = EventTet.isEvent();
            if (playersInMap != null) {
                for (Player p : playersInMap) {
                    if (p != null && p.clan != null) {
                        String msg;
                        if (winnerClan != null && p.clan.id == winnerClan.id) {
                            msg = "🏆 Chiến thắng Đại Chiến Đảo Đào Hoa!\n+" + REWARD_WIN_CLAN_EXP + " EXP Bang, +" + REWARD_WIN_CLAN_RUBY + " Ruby Bang";
                            if (isEventTet) {
                                Item_wear item = new Item_wear();
                                item.setup_template_by_id(ITEM_HOP_TRANG_PHUC);
                                p.item.add_item_bag3(item);
                                p.update_ngoc(REWARD_WIN_RUBY_EVENT);
                                p.update_money();
                                msg += "\n[Quà Tết] +1 Hộp Trang Phục (356), +" + REWARD_WIN_RUBY_EVENT + " Ruby";
                            }
                        } else {
                            msg = "💪 Kết thúc trận đấu Đại Chiến Đảo Đào Hoa!\n+" + REWARD_LOSE_CLAN_EXP + " EXP Bang";
                        }
                        Service.send_box_ThongBao_OK(p, msg);
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private static void saveClan(Clan clan) {
        if (clan == null) return;
        try {
            java.sql.Connection conn = SQL.gI().getCon();
            try (java.sql.PreparedStatement ps = conn.prepareStatement(
                    "UPDATE `clan` SET `xp` = ?, `ruby` = ? WHERE `id` = ?")) {
                ps.setInt(1, clan.xp);
                ps.setInt(2, clan.ruby);
                ps.setInt(3, clan.id);
                ps.executeUpdate();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
