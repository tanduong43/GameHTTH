package event;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import client.Player;
import core.BXH;
import core.Manager;
import core.Service;
import core.Util;
import io.Message;
import map.Boss;
import map.Map;
import map.Mob;
import template.GiftBox;
import template.ItemFashionP2;
import template.ItemTemplate4;
import template.Top_Dame;

/**
 * Sự kiện Tết Nguyên Đán 2026
 * "ĐẠI CHIẾN TÂN NIÊN: KHAI XUÂN ĐOẠT BẢO"
 * 
 * Chỉ sử dụng item4 có sẵn trong database, không ảnh hưởng code gốc.
 */
public class EventTet implements Runnable {

    // ================== CẤU HÌNH SỰ KIỆN ==================
    public static boolean IS_OPEN = false;
    private static final String CONFIG_KEY = "event-tet";

    // Map IDs
    public static final int MAP_DAU_TRUONG = 2026;  // Đấu Trường Mùa Xuân
    public static final int MAP_DAO_DAO_HOA = 2027; // Đảo Đào Hoa

    // Item IDs - Nguyên liệu làm bánh
    public static final int ITEM_LA_DONG = 351;
    public static final int ITEM_DAU_XANH = 352;
    public static final int ITEM_GAO_NEP = 353;
    public static final int ITEM_THIT_HEO = 354;
    public static final int ITEM_BO_LAT_TRE = 429;
    public static final int ITEM_HU_GIA_VI = 430;

    // Item IDs - Nguyên liệu bánh giầy
    public static final int ITEM_LA_CHUOI = 523;
    public static final int ITEM_BOT_GAO = 524;

    // Item IDs - Thành phẩm
    public static final int ITEM_BANH_CHUNG = 350;
    public static final int ITEM_BANH_GIAY = 525;

    // Item IDs - Hộp quà
    public static final int ITEM_RUONG_NGUYEN_LIEU_TET = 355;
    public static final int ITEM_HOP_TRANG_PHUC = 356;
    public static final int ITEM_BAO_LI_XI_TAN_NIEN = 357;
    public static final int ITEM_HOP_TRANG_PHUC_1 = 637;
    public static final int ITEM_HOP_TRANG_PHUC_2 = 638;

    // Item IDs - May mắn
    public static final int ITEM_CAYH_DAO = 629;
    public static final int ITEM_HOA_MAI = 635;

    // Item IDs - Chữ ghép
    public static final int ITEM_CHU_CUNG = 630;
    public static final int ITEM_CHU_VUI = 631;
    public static final int ITEM_CHU_DON = 632;
    public static final int ITEM_CHU_TET = 633;
    public static final int ITEM_CHU_TAN_NIEN = 634;

    // Item IDs - Rương có sẵn
    public static final int ITEM_RUONG_TET = 170;
    public static final int ITEM_RUONG_TET_2018 = 239;

    // Boss ID
    public static final int MOB_BOSS_LAN_SU_TU = 153;

    // Danh hiệu IDs (dùng lại có sẵn)
    public static final int DANH_HIEU_VUA_BIEN_CA = 0;
    public static final int DANH_HIEU_BAT_BAI = 4;
    public static final int DANH_HIEU_DAI_THAN = 7;
    public static final int DANH_HIEU_THIEN_TU = 8;

    // Fashion IDs
    public static final int FASHION_THAN_TAI = 95;
    public static final int FASHION_AO_DAI_NAM = 78;
    public static final int FASHION_AO_DAI_NU = 79;

    // Thời gian spawn boss (giờ trong ngày)
    private static final int[] BOSS_SPAWN_HOURS = { 12, 18, 20, 22 };
    private static final int BOSS_SPAWN_MINUTE = 30;
    private static final long BOSS_LIFETIME_MS = 30 * 60 * 1000L; // 30 phút

    // ================== TRẠNG THÁI RUNTIME ==================
    private static EventTet instance;
    private final Thread eventThread;
    private volatile boolean running = true;

    // Boss Lân Sư Tử
    private Boss activeBoss;
    private long nextBossSpawnTime = 0;
    private long bossSpawnTime = 0;
    private boolean bossAlive = false;
    private Player lastHitPlayer = null;
    private final List<Player> participatedPlayers = new CopyOnWriteArrayList<>();
    private final List<Top_Dame> bossDamageList = new CopyOnWriteArrayList<>();

    // Đấu Trường Sinh Tồn
    private boolean dauTruongOpen = false;
    private int dauTruongKillCount = 0;
    private final CopyOnWriteArrayList<Player> dauTruongPlayers = new CopyOnWriteArrayList<>();
    private long dauTruongEndTime = 0;

    // Chiếm Đảo
    private boolean daoDaoHoaOpen = false;
    private long daoDaoHoaEndTime = 0;

    // Scheduler
    private final ScheduledExecutorService scheduler = Executors.newScheduledThreadPool(2, r -> {
        Thread t = new Thread(r, "EventTet-Scheduler");
        t.setDaemon(true);
        return t;
    });

    // ================== KHỞI TẠO ==================
    private EventTet() {
        this.eventThread = new Thread(this, "EventTet-Main");
        this.eventThread.start();
    }

    public static EventTet getInstance() {
        if (instance == null) {
            synchronized (EventTet.class) {
                if (instance == null) {
                    instance = new EventTet();
                }
            }
        }
        return instance;
    }

    public static boolean isEvent() {
        return IS_OPEN;
    }

    public static void setEvent(boolean open) {
        IS_OPEN = open;
        if (open) {
            getInstance().scheduleNextBossSpawn();
            getInstance().scheduleEvents();
            broadcastMessage("[SỰ KIỆN TẾT] Đại Chiến Tân Niên: Khai Xuân Đoạt Bảo đã được kích hoạt!");
        } else {
            broadcastMessage("[SỰ KIỆN TẾT] Sự kiện Tết đã kết thúc!");
            getInstance().cleanup();
        }
    }

    public static void loadConfig(java.util.Properties config) {
        String value = config.getProperty(CONFIG_KEY);
        if (value != null) {
            IS_OPEN = Boolean.parseBoolean(value);
        }
    }

    private void cleanup() {
        daoDaoHoaOpen = false;
        if (bossAlive) {
            despawnBoss("Boss đã được giải tán!");
        }
    }

    @Override
    public void run() {
        while (running) {
            try {
                // Đấu Trường Sinh Tồn là hoạt động định kỳ hàng tuần (chạy độc lập với sự kiện Tết)
                updateDauTruongSchedule();
                if (IS_OPEN) {
                    update();
                }
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                break;
            } catch (Exception e) {
                System.out.println("EventTet error: " + e.getMessage());
                e.printStackTrace();
            }
        }
    }

    // ================== SCHEDULING ==================

    private void scheduleEvents() {
        // Chiếm Đảo: T3, T5, CN lúc 19h
        scheduleDaoDaoHoa();
    }

    private void updateDauTruongSchedule() {
        if (!dauTruongOpen) {
            openDauTruong();
        }
    }

    // ================== ĐẤU TRƯỜNG SINH TỒN ==================

    public void openDauTruong() {
        dauTruongOpen = true;
        dauTruongEndTime = System.currentTimeMillis() + 365L * 24 * 60 * 60 * 1000L; // Mở full ngày 24/7 để test
    }

    public void closeDauTruong() {
        if (!dauTruongOpen) return;

        dauTruongOpen = false;

        // Xử lý thưởng Top
        processDauTruongRewards();

        try {
            broadcastMessage("[ĐẤU TRƯỜNG] Đấu Trường Sinh Tồn đã kết thúc và phát thưởng Top Kill qua Hòm Thư!");
            Manager.gI().chatKTG(0, "🏆 Đấu Trường Sinh Tồn đã kết thúc! Phần thưởng Top Kill đã được gửi qua Hòm Thư!", 5);
        } catch (IOException e) {
            System.out.println("Error announcing dau truong close: " + e.getMessage());
        }

        // Đưa người chơi trong map 2026 về làng
        Map[] mapDT = Map.get_map_by_id(MAP_DAU_TRUONG);
        if (mapDT != null) {
            map.Vgo vgo = new map.Vgo();
            vgo.map_go = Map.get_map_by_id(1);
            if (vgo.map_go != null) {
                vgo.xnew = 611;
                vgo.ynew = 250;
                for (Player pl : new ArrayList<>(dauTruongPlayers)) {
                    if (pl != null && pl.map != null && pl.map.template.id == MAP_DAU_TRUONG) {
                        try {
                            pl.goto_map(vgo);
                        } catch (IOException e) {
                            System.out.println("Error moving player back to village: " + e.getMessage());
                        }
                    }
                }
            }
        }
    }

    public boolean isDauTruongOpen() {
        return true; // Mở full ngày để test
    }

    public void onPlayerJoinDauTruong(Player p) {
        if (p != null && !dauTruongPlayers.contains(p)) {
            dauTruongPlayers.add(p);
        }
        if (p != null && p.map != null && p.map.template.id == MAP_DAU_TRUONG) {
            try {
                p.map.change_flag(p, 3);
            } catch (IOException e) {
                // ignore
            }
        }
    }

    public void onPlayerKillInDauTruong(Player killer, Player victim) {
        if (!isDauTruongOpen()) return;

        dauTruongKillCount++;

        // Cập nhật kill count cho killer
        if (killer != null) {
            killer.dauTruongKills++;
            if (!dauTruongPlayers.contains(killer)) {
                dauTruongPlayers.add(killer);
            }
        }
        if (victim != null && !dauTruongPlayers.contains(victim)) {
            dauTruongPlayers.add(victim);
        }

        // Thông báo
        if (killer != null && victim != null) {
            try {
                Manager.gI().chatKTG(0,
                    killer.name + " đã hạ gục " + victim.name + " tại Đấu Trường! (" + killer.dauTruongKills + " Kills)",
                    5);
            } catch (IOException e) {
                // ignore
            }
        }
    }

    // Lưu kết quả top trận đấu gần nhất
    private final java.util.Map<String, Integer> pendingDauTruongRewards = new java.util.concurrent.ConcurrentHashMap<>();

    public static class DauTruongTopRecord {
        public String name;
        public int kills;
        public int rank;
        public DauTruongTopRecord(String name, int kills, int rank) {
            this.name = name;
            this.kills = kills;
            this.rank = rank;
        }
    }
    private final List<DauTruongTopRecord> lastMatchTopList = new CopyOnWriteArrayList<>();

    public List<DauTruongTopRecord> getLastMatchTopList() {
        return lastMatchTopList;
    }

    public List<Player> getDauTruongPlayers() {
        return dauTruongPlayers;
    }

    public java.util.Map<String, Integer> getPendingDauTruongRewards() {
        return pendingDauTruongRewards;
    }

    private void processDauTruongRewards() {
        if (dauTruongPlayers.isEmpty()) return;

        // Sắp xếp theo số kill
        List<Player> sorted = new ArrayList<>(dauTruongPlayers);
        sorted.sort((p1, p2) -> Integer.compare(p2.dauTruongKills, p1.dauTruongKills));

        lastMatchTopList.clear();
        for (int i = 0; i < sorted.size(); i++) {
            Player p = sorted.get(i);
            if (p == null || p.dauTruongKills <= 0) continue;
            int rank = i + 1;
            lastMatchTopList.add(new DauTruongTopRecord(p.name, p.dauTruongKills, rank));
            if (i < 10) {
                pendingDauTruongRewards.put(p.name, rank);
                try {
                    Service.send_box_ThongBao_OK(p, "Trận đấu kết thúc! Bạn đạt TOP " + rank + " (" + p.dauTruongKills + " Kills) Đấu Trường Sinh Tồn!\nHãy gặp Tôn Ngộ Không để nhận quà!");
                } catch (IOException e) {
                    // ignore
                }
            }
        }

        // Reset kill counts
        for (Player p : dauTruongPlayers) {
            if (p != null) {
                p.dauTruongKills = 0;
            }
        }
    }

    public void claimDauTruongReward(Player p) {
        if (p == null) return;
        Integer rankObj = pendingDauTruongRewards.get(p.name);
        if (rankObj == null) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn không có phần thưởng Đấu Trường Sinh Tồn nào để nhận!");
            } catch (IOException e) {
                // ignore
            }
            return;
        }

        int rank = rankObj;
        pendingDauTruongRewards.remove(p.name);

        List<GiftBox> rewards = new ArrayList<>();
        if (rank == 1) {
            rewards.add(createGiftBox(1, 1000)); // 1000 Ruby
            if (isEvent()) {
                rewards.add(createGiftBox(ITEM_HOP_TRANG_PHUC, 1)); // 1 Hộp Thời Trang Tết Vĩnh Viễn (356)
            }
            p.add_danh_hieu(DANH_HIEU_BAT_BAI);
            p.danhhieu = DANH_HIEU_BAT_BAI;
            p.id_danh_hieu_su_dung = DANH_HIEU_BAT_BAI;
            p.time_danh_hieu_bat_bai = System.currentTimeMillis() + 7L * 24 * 60 * 60 * 1000L; // 7 Ngày
        } else if (rank <= 3) {
            rewards.add(createGiftBox(1, 500)); // 500 Ruby
            if (isEvent()) {
                rewards.add(createGiftBox(ITEM_HOP_TRANG_PHUC_1, 1)); // 1 Hộp Trang Phục Tết 1 (637) [30 Ngày]
                rewards.add(createGiftBox(ITEM_BAO_LI_XI_TAN_NIEN, 10)); // 10 Bao Lì Xì Tân Niên (357)
            }
        } else if (rank <= 10) {
            rewards.add(createGiftBox(1, 200)); // 200 Ruby
            if (isEvent()) {
                rewards.add(createGiftBox(ITEM_BAO_LI_XI_TAN_NIEN, 5)); // 5 Bao Lì Xì Tân Niên (357)
            }
        }

        try {
            Service.send_gift(p, 1, "Thưởng TOP " + rank + " Đấu Trường",
                "Chúc mừng bạn đã xuất sắc đoạt TOP " + rank + " Đấu Trường Sinh Tồn!", rewards, true);
        } catch (IOException e) {
            System.out.println("Error claiming dau truong reward: " + e.getMessage());
        }
    }

    public void showTopKillDauTruong(Player p) {
        if (p == null) return;
        try {
            BXH.send(p, 14, 0);
        } catch (IOException e) {
            System.out.println("Error showing dau truong leaderboard: " + e.getMessage());
        }
    }

    // ================== CHIẾM ĐẢO ĐÀO HOA ==================

    private void scheduleDaoDaoHoa() {
        Calendar cal = Calendar.getInstance();
        int dayOfWeek = cal.get(Calendar.DAY_OF_WEEK);
        int hour = cal.get(Calendar.HOUR_OF_DAY);

        // Thứ 3 (3), Thứ 5 (5), Chủ Nhật (1)
        boolean isActiveDay = (dayOfWeek == Calendar.TUESDAY ||
                               dayOfWeek == Calendar.THURSDAY ||
                               dayOfWeek == Calendar.SUNDAY);

        if (isActiveDay && hour < 19) {
            Calendar target = Calendar.getInstance();
            target.set(Calendar.HOUR_OF_DAY, 19);
            target.set(Calendar.MINUTE, 0);
            target.set(Calendar.SECOND, 0);

            if (target.getTimeInMillis() <= System.currentTimeMillis()) {
                target.add(Calendar.DAY_OF_MONTH, 1);
            }

            long delay = target.getTimeInMillis() - System.currentTimeMillis();

            scheduler.schedule(() -> {
                if (IS_OPEN) {
                    openDaoDaoHoa();
                }
            }, delay, TimeUnit.MILLISECONDS);
        }
    }

    public void openDaoDaoHoa() {
        if (daoDaoHoaOpen) return;

        daoDaoHoaOpen = true;
        daoDaoHoaEndTime = System.currentTimeMillis() + 60 * 60 * 1000L; // 1 giờ

        try {
            broadcastMessage("[GUILD WAR] Đảo Đào Hoa mở cửa! Các Bang hội chiến đấu!");
            Manager.gI().chatKTG(0, "⚔️ Đảo Đào Hoa mở! Giữ Trụ Trung Tâm đến 20h để chiến thắng!", 5);
        } catch (IOException e) {
            System.out.println("Error announcing dao dao hoa: " + e.getMessage());
        }

        scheduler.schedule(() -> {
            closeDaoDaoHoa();
        }, 60 * 60 * 1000, TimeUnit.MILLISECONDS);
    }

    public void closeDaoDaoHoa() {
        if (!daoDaoHoaOpen) return;

        daoDaoHoaOpen = false;

        // Xử lý thưởng guild giữ trụ
        broadcastMessage("[GUILD WAR] Đảo Đào Hoa đã đóng! Guild giữ Trụ chiến thắng!");

        scheduleDaoDaoHoa();
    }

    public boolean isDaoDaoHoaOpen() {
        return daoDaoHoaOpen && System.currentTimeMillis() < daoDaoHoaEndTime;
    }

    // ================== BOSS LÂN SƯ TỬ ==================

    private void scheduleNextBossSpawn() {
        Calendar cal = Calendar.getInstance();
        int hour = cal.get(Calendar.HOUR_OF_DAY);
        int minute = cal.get(Calendar.MINUTE);

        int nextHour = -1;
        for (int spawnHour : BOSS_SPAWN_HOURS) {
            if (hour < spawnHour || (hour == spawnHour && minute < BOSS_SPAWN_MINUTE)) {
                nextHour = spawnHour;
                break;
            }
        }

        if (nextHour == -1) {
            cal.add(Calendar.DAY_OF_MONTH, 1);
            cal.set(Calendar.HOUR_OF_DAY, BOSS_SPAWN_HOURS[0]);
        } else {
            cal.set(Calendar.HOUR_OF_DAY, nextHour);
        }
        cal.set(Calendar.MINUTE, BOSS_SPAWN_MINUTE);
        cal.set(Calendar.SECOND, 0);

        nextBossSpawnTime = cal.getTimeInMillis();
    }

    private synchronized void update() {
        long now = System.currentTimeMillis();

        // Boss spawn
        if (!bossAlive && now >= nextBossSpawnTime) {
            spawnBossLanSuTu();
        }

        // Auto despawn boss after lifetime
        if (bossAlive && activeBoss != null && now >= bossSpawnTime + BOSS_LIFETIME_MS) {
            despawnBoss("Boss Lân Sư Tử đã bỏ đi!");
        }
    }

    private void spawnBossLanSuTu() {
        if (bossAlive) return;

        List<Map> allowedMaps = new ArrayList<>();
        int[] spawnMaps = { 3, 4, 7, 10, 11, 12, 13, 16, 18, 19, 20, 24, 26, 27, 28, 32, 34, 35, 36, 40, 42, 43, 44, 48, 50, 51, 52, 63, 65, 68, 70, 71, 72, 82, 84, 85, 86, 94, 95, 96, 97, 98, 99, 100, 101, 112, 115, 116, 117, 118, 124, 125, 126, 192, 193, 194, 195, 196, 197 };

        for (int mapId : spawnMaps) {
            Map[] maps = Map.get_map_by_id(mapId);
            if (maps != null) {
                for (Map m : maps) {
                    if (m != null) {
                        allowedMaps.add(m);
                    }
                }
            }
        }

        if (allowedMaps.isEmpty()) {
            nextBossSpawnTime = System.currentTimeMillis() + 60_000L;
            return;
        }

        Map targetMap = allowedMaps.get(Util.random(allowedMaps.size()));

        Boss boss = new Boss();
        boss.id = 9998;
        boss.thegioi = 1;
        boss.mob = new Mob();
        boss.mob.mob_template = template.MobTemplate.ENTRYS.get(MOB_BOSS_LAN_SU_TU);

        if (boss.mob.mob_template == null) {
            System.out.println("Lỗi: Không tìm thấy MobTemplate " + MOB_BOSS_LAN_SU_TU);
            nextBossSpawnTime = System.currentTimeMillis() + 60_000L;
            return;
        }

        boss.mob.hp_max = boss.mob.mob_template.hp_max;
        boss.hp_max_origin = boss.mob.mob_template.hp_max;
        boss.mob.boss_info = boss;
        boss.TopDame = new ArrayList<>();
        boss.skill = new short[] { 1 };
        boss.buff = new ArrayList<>();
        boss.time_atk = new long[] { 0, 0, 0, 0, 0 };
        boss.levelBoss = 1;

        int currentIndex = Manager.gI().getIndexMob();
        boss.mob.index = currentIndex;
        boss.index_mob_save = currentIndex;
        Manager.gI().setIndexMob(currentIndex + 10);

        for (int j = 0; j < 10; j++) {
            Mob.ENTRYS.put((boss.mob.index + j), boss.mob);
        }

        if (Boss.ENTRYS != null && !Boss.ENTRYS.contains(boss)) {
            Boss.ENTRYS.add(boss);
        }

        boss.mob.map = targetMap;
        boss.mapOrigin = targetMap;
        boss.mob.x = (short) (targetMap.template.maxW / 2);
        boss.mob.y = 200;
        boss.mob.isdie = false;
        boss.mob.hp = boss.mob.hp_max;
        boss.mob.id_target = -1;

        activeBoss = boss;
        bossAlive = true;
        bossSpawnTime = System.currentTimeMillis();
        lastHitPlayer = null;
        participatedPlayers.clear();
        bossDamageList.clear();

        try {
            Manager.gI().chatKTG(0,
                "🦁 Boss Lân Sư Tử Hoàng Kim xuất hiện tại " + targetMap.template.name +
                " khu " + (targetMap.zone_id + 1) + "! Last Hit nhận Rương Tết!",
                5);

            Message m = new Message(1);
            m.writer().writeByte(1);
            m.writer().writeShort(boss.mob.index);
            m.writer().writeShort(boss.mob.x);
            m.writer().writeShort(boss.mob.y);

            for (Player p : targetMap.players) {
                if (p != null && p.conn != null) {
                    p.conn.addmsg(m);
                }
            }
            m.cleanup();
        } catch (IOException e) {
            System.out.println("Error announcing boss spawn: " + e.getMessage());
        }
    }

    public void forceSpawnBoss(Player p) {
        if (bossAlive) {
            despawnBoss("Boss cũ đã được giải tán!");
        }
        spawnBossLanSuTu();
        if (p != null) {
            try {
                Service.send_box_ThongBao_OK(p, "Đã gọi Lân Sư Tử thành công!");
            } catch (IOException e) {
                // ignore
            }
        }
    }

    public void onBossDamaged(Player player, int damage) {
        if (!bossAlive || activeBoss == null) return;

        Top_Dame entry = new Top_Dame();
        entry.name = player.name;
        entry.dame = damage;
        entry.id = player.id;

        boolean found = false;
        for (Top_Dame td : bossDamageList) {
            if (td.id == player.id) {
                td.dame += damage;
                found = true;
                break;
            }
        }
        if (!found) {
            bossDamageList.add(entry);
        }

        if (!participatedPlayers.contains(player)) {
            participatedPlayers.add(player);
        }
    }

    public void onBossKilled(Player killer) {
        if (!bossAlive || activeBoss == null) return;

        lastHitPlayer = killer;

        if (killer != null) {
            killer.tetLanKills++;
        }

        try {
            Manager.gI().chatKTG(0,
                "🎉 " + killer.name + " đã hạ gục Lân Sư Tử Hoàng Kim! (Lần thứ " + killer.tetLanKills + ")",
                5);
        } catch (IOException e) {
            System.out.println("Error announcing boss kill: " + e.getMessage());
        }

        giveLastHitReward(killer);
        despawnBoss(null);
    }

    private void giveLastHitReward(Player player) {
        if (player == null) return;

        List<GiftBox> rewards = new ArrayList<>();

        // 100% nhận Rương Tết (170)
        rewards.add(createGiftBox(ITEM_RUONG_TET, 1));

        // 20% nhận Hộp Trang Phục Tết (356)
        if (Util.random(100) < 20) {
            rewards.add(createGiftBox(ITEM_HOP_TRANG_PHUC, 1));
        }

        try {
            Service.send_gift(player, 0, "Phần thưởng Đòn Kết Liễu Lân Sư Tử!", "", rewards, true);
        } catch (IOException e) {
            System.out.println("Error giving last hit reward: " + e.getMessage());
        }
    }

    private void despawnBoss(String message) {
        if (activeBoss != null && activeBoss.mob != null) {
            try {
                activeBoss.mob.isdie = true;
                activeBoss.mob.hp = 0;
                if (activeBoss.mob.map != null) {
                    activeBoss.mob.map.remove_obj(activeBoss.mob.index, 1);
                }
            } catch (IOException e) {
                System.out.println("Error despawning boss: " + e.getMessage());
            }
        }

        bossAlive = false;
        activeBoss = null;
        nextBossSpawnTime = System.currentTimeMillis() + 10 * 60 * 1000L;

        if (message != null) {
            try {
                Manager.gI().chatKTG(0, message, 5);
            } catch (IOException e) {
                System.out.println("Error sending despawn message: " + e.getMessage());
            }
        }
    }

    // ================== GHÉP CHỮ VÀNG ==================

    public void onLetterCombine(Player p, int itemId) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Tết chưa được kích hoạt!");
            return;
        }

        // Kiểm tra đủ 5 chữ
        int[] letters = { ITEM_CHU_CUNG, ITEM_CHU_VUI, ITEM_CHU_DON, ITEM_CHU_TET, ITEM_CHU_TAN_NIEN };
        int count = 0;
        for (int letter : letters) {
            count += p.item.total_item_bag_by_id(4, letter);
        }

        if (count >= 5) {
            // Xóa 5 chữ
            for (int letter : letters) {
                p.item.remove_item47(4, letter, 1);
            }
            p.item.update_Inventory(-1, false);

            // Random quà
            List<GiftBox> rewards = new ArrayList<>();
            int rand = Util.random(3);
            switch (rand) {
                case 0:
                    rewards.add(createGiftBox(ITEM_HOP_TRANG_PHUC, 1));
                    break;
                case 1:
                    rewards.add(createGiftBox(ITEM_RUONG_TET, 2));
                    break;
                case 2:
                    rewards.add(createGiftBox(ITEM_BAO_LI_XI_TAN_NIEN, 10));
                    break;
            }

            Service.send_gift(p, 0, "Chúc mừng! Bạn ghép thành công bộ chữ vàng!", "", rewards, true);
            Manager.gI().chatKTG(0, "🎊 " + p.name + " đã ghép thành công bộ chữ CÙNG-VUI-ĐÓN-TẾT-TÂN NIÊN!", 5);
        } else {
            Service.send_box_ThongBao_OK(p, "Bạn cần đủ 5 chữ để ghép! (Còn thiếu " + (5 - count) + " chữ)");
        }
    }

    // ================== HỘP QUÀ TẾT ==================

    public void openRuongTet(Player p, int itemId) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Tết chưa được kích hoạt!");
            return;
        }

        List<GiftBox> rewards = new ArrayList<>();

        switch (itemId) {
            case ITEM_RUONG_TET: {
                rewards.add(createGiftBox(1, 500)); // 500 Ruby
                rewards.add(createGiftBox(0, 10_000_000)); // 10M Beri
                // Đá khảm 5-6
                ItemTemplate4 gem = ItemTemplate4.get_it_by_id(Util.random(44, 79));
                if (gem != null) {
                    GiftBox g = createGiftBox(gem.id, 1);
                    g.color = 5;
                    rewards.add(g);
                }
                break;
            }
            case ITEM_RUONG_TET_2018: {
                rewards.add(createGiftBox(1, 300)); // 300 Ruby
                rewards.add(createGiftBox(0, 5_000_000)); // 5M Beri
                ItemTemplate4 gem = ItemTemplate4.get_it_by_id(Util.random(44, 79));
                if (gem != null) {
                    GiftBox g = createGiftBox(gem.id, 1);
                    rewards.add(g);
                }
                break;
            }
            case ITEM_BAO_LI_XI_TAN_NIEN: {
                int rand = Util.random(3);
                switch (rand) {
                    case 0:
                        rewards.add(createGiftBox(0, Util.random(100_000, 500_000)));
                        break;
                    case 1:
                        rewards.add(createGiftBox(1, Util.random(10, 50)));
                        break;
                    case 2:
                        rewards.add(createGiftBox(ITEM_BOT_GAO, Util.random(1, 5)));
                        break;
                }
                break;
            }
            case ITEM_HOP_TRANG_PHUC: {
                // Mở chọn 1 trong các bộ thời trang Tết
                int[] fashionOptions = { FASHION_AO_DAI_NAM, FASHION_AO_DAI_NU, FASHION_THAN_TAI };
                // Random fashion
                int fId = fashionOptions[Util.random(fashionOptions.length)];
                if (p.check_fashion(fId) != null) {
                    // Đã có, đổi thành Ruby
                    rewards.add(createGiftBox(1, 500));
                } else {
                    ItemFashionP2 newFashion = new ItemFashionP2();
                    newFashion.id = (short) fId;
                    newFashion.is_use = false;
                    newFashion.level = 0;
                    p.fashion.add(newFashion);
                    Service.send_box_ThongBao_OK(p, "Bạn nhận được thời trang Tết vĩnh viễn!");
                }
                break;
            }
            case ITEM_HOP_TRANG_PHUC_1:
            case ITEM_HOP_TRANG_PHUC_2: {
                rewards.add(createGiftBox(1, 100));
                rewards.add(createGiftBox(ITEM_BAO_LI_XI_TAN_NIEN, 3));
                break;
            }
        }

        Service.send_gift(p, 0, "Quà Tết:", "", rewards, true);
    }

    // ================== NỒI BÁNH CHƯNG ==================

    public void useNoiBanhChung(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Tết chưa được kích hoạt!");
            return;
        }

        // Kiểm tra đủ nguyên liệu
        int laDong = p.item.total_item_bag_by_id(4, ITEM_LA_DONG);
        int dauXanh = p.item.total_item_bag_by_id(4, ITEM_DAU_XANH);
        int gaoNep = p.item.total_item_bag_by_id(4, ITEM_GAO_NEP);
        int thitHeo = p.item.total_item_bag_by_id(4, ITEM_THIT_HEO);

        if (laDong >= 1 && dauXanh >= 1 && gaoNep >= 1 && thitHeo >= 1) {
            p.item.remove_item47(4, ITEM_LA_DONG, 1);
            p.item.remove_item47(4, ITEM_DAU_XANH, 1);
            p.item.remove_item47(4, ITEM_GAO_NEP, 1);
            p.item.remove_item47(4, ITEM_THIT_HEO, 1);
            p.item.add_item_bag47(4, ITEM_BANH_CHUNG, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "Bạn đã gói thành công 1 Bánh Chưng Tết!");
        } else {
            Service.send_box_ThongBao_OK(p, "Bạn chưa đủ nguyên liệu! Cần: 1 Lá dong, 1 Đậu xanh, 1 Gạo nếp, 1 Thịt heo");
        }
    }

    public void useBanhChung(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Tết chưa được kích hoạt!");
            return;
        }

        if (p.item.total_item_bag_by_id(4, ITEM_BANH_CHUNG) <= 0) {
            Service.send_box_ThongBao_OK(p, "Bạn không có Bánh Chưng!");
            return;
        }

        p.item.remove_item47(4, ITEM_BANH_CHUNG, 1);
        p.item.update_Inventory(-1, false);

        // Tăng EXP
        p.update_exp(10_000, false);
        Service.send_box_ThongBao_OK(p, "Bạn đã sử dụng Bánh Chưng! +10.000 EXP!");
    }

    // ================== CRAFTING ==================

    public static boolean isEventCraftItem(int itemId) {
        return itemId == ITEM_BANH_CHUNG || itemId == ITEM_BANH_GIAY
                || itemId == ITEM_LA_DONG || itemId == ITEM_DAU_XANH
                || itemId == ITEM_GAO_NEP || itemId == ITEM_THIT_HEO
                || itemId == ITEM_LA_CHUOI || itemId == ITEM_BOT_GAO;
    }

    public static int[] getCraftRecipe(int itemId) {
        switch (itemId) {
            case ITEM_BANH_CHUNG:
                return new int[] { ITEM_BANH_CHUNG, 1, ITEM_LA_DONG, 1, ITEM_DAU_XANH, 1, ITEM_GAO_NEP, 1, ITEM_THIT_HEO, 500_000, 0 };
            case ITEM_BANH_GIAY:
                return new int[] { ITEM_BANH_GIAY, 1, ITEM_LA_CHUOI, 1, ITEM_BOT_GAO, 1, 300_000, 0 };
            default:
                return null;
        }
    }

    public static String getCraftDescription(int itemId) {
        switch (itemId) {
            case ITEM_BANH_CHUNG:
                return "1 Lá dong + 1 Đậu xanh + 1 Gạo nếp + 1 Thịt heo + 500.000 Beri";
            case ITEM_BANH_GIAY:
                return "1 Lá chuối + 1 Bột gạo + 300.000 Beri";
            default:
                return "";
        }
    }

    // ================== UTILITY ==================

    private GiftBox createGiftBox(int id, int num) {
        GiftBox gb = new GiftBox();
        gb.type = 4;
        gb.id = (short) id;
        gb.num = num;
        gb.color = 0;
        ItemTemplate4 it = ItemTemplate4.get_it_by_id(id);
        if (it != null) {
            gb.name = it.name;
            gb.icon = it.icon;
        } else {
            gb.name = id == 0 ? "Beri" : (id == 1 ? "Ruby" : "Item " + id);
            gb.icon = 0;
        }
        return gb;
    }

    private static void broadcastMessage(String msg) {
        try {
            Manager.gI().chatKTG(0, msg, 5);
        } catch (IOException e) {
            System.out.println("Error broadcasting: " + e.getMessage());
        }
    }

    public void shutdown() {
        running = false;
        scheduler.shutdown();
        try {
            scheduler.awaitTermination(5, TimeUnit.SECONDS);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    }

    // ================== LEADERBOARD ==================

    public static void showLeaderboard(Player p) {
        if (p == null) return;

        try {
            BXH.update();

            StringBuilder sb = new StringBuilder();
            sb.append("🏆 BẢNG XẾP HẠNG SỰ KIỆN TẾT\n");
            sb.append("═══════════════════════════════\n\n");
            sb.append("📊 Giết Lân Sư Tử: ").append(p.tetLanKills).append("\n");
            sb.append("⚔️ Kills Đấu Trường: ").append(p.dauTruongKills).append("\n");
            sb.append("\n═══════════════════════════════");

            Service.send_box_ThongBao_OK(p, sb.toString());
        } catch (IOException e) {
            System.out.println("Error showing leaderboard: " + e.getMessage());
        }
    }
}
