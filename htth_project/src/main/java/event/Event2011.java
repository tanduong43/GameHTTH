package event;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.Map.Entry;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import client.Player;
import core.Manager;
import core.Service;
import core.Util;
import io.Message;
import map.Boss;
import map.Map;
import map.Mob;
import template.GiftBox;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.Top_Dame;

/**
 * Sự Kiện Ngày Nhà Giáo Việt Nam 20/11 - "Tri Ân Thầy Cô & Tôn Sư Trọng Đạo"
 * 
 * 100% sử dụng các item có sẵn trong database:
 * - Nguyên liệu: Trang Giấy (451), Lọ Mực (461), Sách Công Thức (452), Cành Hoa (591), Cánh Hoa Phượng (196), Giấy Gói Quà (575), Giấy Đỏ (636), Gấu Bông (590), Bản Nhạc (179), Chứng Nhận Sư Phụ (627)
 * - Thành phẩm: Điểm 10 (319), Thiệp Mời/Tri Ân (382), Lẵng Hoa (592), Hộp Quà Sơ Cấp (586), Hộp Quà Cao Cấp (587), Hộp Quà Đặc Biệt (596)
 */
public class Event2011 implements Runnable {

    // ================== CẤU HÌNH SỰ KIỆN ==================
    public static boolean IS_OPEN = false;
    private static final String CONFIG_KEY = "event-2011";

    // Item IDs - Nguyên liệu
    public static final int ITEM_TRANG_GIAY = 451;         // Trang vở / Trang giấy học trò
    public static final int ITEM_LO_MUC = 461;             // Lọ mực tím
    public static final int ITEM_SACH_CONG_THUC = 452;     // Sách tri thức / Giáo án
    public static final int ITEM_CANH_HOA_PHUONG = 196;    // Cánh hoa phượng tuổi học trò
    public static final int ITEM_GIAY_GOI_QUA = 575;       // Giấy gói quà tri ân
    public static final int ITEM_GIAY_DO = 636;            // Giấy bìa đỏ làm thiệp
    public static final int ITEM_GAU_BONG = 590;           // Gấu bông lưu niệm
    public static final int ITEM_BAN_NHAC = 179;           // Bản nhạc kích lệ / Ca khúc tri ân
    public static final int ITEM_CHUNG_NHAN_SU_PHU = 627;  // Chứng nhận Tôn Sư Trọng Đạo

    public static final int ITEM_RUONG_DA_THAN_THOAI_TU_CHON = 1004; // Rương đá thần thoại tự chọn
    public static final int ITEM_HOP_THOI_TRANG_CAO_CAP = 1002;      // Hộp thời trang cao cấp
    public static final int ITEM_DA_KHAM_VO_CUC_S = 326;             // Đá khảm vô cực S

    // Item IDs - Thành phẩm & Quà tặng
    public static final int ITEM_DIEM_10 = 319;             // Bông hoa Điểm 10
    public static final int ITEM_THIEP_TRI_AN = 382;        // Thiệp Tri Ân 20/11 (Thiệp mời)
    public static final int ITEM_LANG_HOA = 592;            // Lẵng Hoa Tri Ân (Hộp quà số 1)
    public static final int ITEM_HOP_QUA_SO_CAP = 586;      // Hộp Quà Tri Ân Sơ Cấp
    public static final int ITEM_HOP_QUA_CAO_CAP = 587;     // Hộp Quà Tri Ân Cao Cấp
    public static final int ITEM_HOP_QUA_DAC_BIET = 596;    // Hộp Quà Tôn Sư Trọng Đạo (Đặc Biệt)

    // Boss ID (Boss Lân Sư Tử)
    public static final int MOB_BOSS_LAN_SU_TU = 153;

    // Giờ spawn Boss (12h, 18h, 20h, 22h)
    private static final int[] BOSS_SPAWN_HOURS = { 12, 18, 20, 22 };
    private static final long BOSS_LIFETIME_MS = 30 * 60 * 1000L; // 30 phút

    // ================== RUNTIME STATE ==================
    private static Event2011 instance;
    private final Thread eventThread;
    private volatile boolean running = true;

    // Boss
    private Boss activeBoss;
    private long nextBossSpawnTime = 0;
    private long bossSpawnTime = 0;
    private boolean bossAlive = false;
    private final List<Top_Dame> bossDamageList = new CopyOnWriteArrayList<>();
    private final List<Player> participatedPlayers = new CopyOnWriteArrayList<>();

    // BXH Điểm Tri Ân (Player Name -> Point)
    private final ConcurrentHashMap<String, Integer> pointMap = new ConcurrentHashMap<>();
    private final Set<String> claimedTopPlayers = ConcurrentHashMap.newKeySet();

    // Scheduler
    private final ScheduledExecutorService scheduler = Executors.newScheduledThreadPool(2, r -> {
        Thread t = new Thread(r, "Event2011-Scheduler");
        t.setDaemon(true);
        return t;
    });

    // ================== KHỞI TẠO ==================
    private Event2011() {
        loadData();
        this.eventThread = new Thread(this, "Event2011-Main");
        this.eventThread.start();
    }

    public static Event2011 getInstance() {
        if (instance == null) {
            synchronized (Event2011.class) {
                if (instance == null) {
                    instance = new Event2011();
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
            broadcastMessage("🎓 Sự kiện Ngày Nhà Giáo Việt Nam 20/11: Tri Ân Thầy Cô đã chính thức khai mở!");
        } else {
            broadcastMessage("🎓 Sự kiện Ngày Nhà Giáo Việt Nam 20/11 đã khép lại. Cảm ơn các thuyền trưởng!");
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
        if (bossAlive) {
            despawnBoss("Boss Thầy Giáo Hắc Ám đã rút lui!");
        }
    }

    @Override
    public void run() {
        while (running) {
            try {
                if (IS_OPEN) {
                    update();
                }
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                break;
            } catch (Exception e) {
                System.out.println("Event2011 error: " + e.getMessage());
            }
        }
    }

    // ================== GIỜ VÀNG & SCHEDULING ==================

    /**
     * Giờ Vàng Học Tập (11h-13h & 19h-21h hàng ngày): X2 EXP & Tăng tỷ lệ rơi đồ
     */
    public static boolean isGioVang() {
        if (!isEvent()) return false;
        Calendar cal = Calendar.getInstance();
        int hour = cal.get(Calendar.HOUR_OF_DAY);
        return (hour >= 11 && hour < 13) || (hour >= 19 && hour < 21);
    }

    private void scheduleNextBossSpawn() {
        Calendar cal = Calendar.getInstance();
        int hour = cal.get(Calendar.HOUR_OF_DAY);
        int minute = cal.get(Calendar.MINUTE);

        int nextHour = -1;
        for (int spawnHour : BOSS_SPAWN_HOURS) {
            if (hour < spawnHour || (hour == spawnHour && minute < 30)) {
                nextHour = spawnHour;
                break;
            }
        }

        if (nextHour == -1) {
            cal.add(Calendar.DAY_OF_MONTH, 1);
            cal.set(Calendar.HOUR_OF_DAY, BOSS_SPAWN_HOURS[0]);
            cal.set(Calendar.MINUTE, 30);
            cal.set(Calendar.SECOND, 0);
        } else {
            cal.set(Calendar.HOUR_OF_DAY, nextHour);
            cal.set(Calendar.MINUTE, 30);
            cal.set(Calendar.SECOND, 0);
        }

        nextBossSpawnTime = cal.getTimeInMillis();
    }

    private synchronized void update() {
        long now = System.currentTimeMillis();
        Calendar cal = Calendar.getInstance();
        int hour = cal.get(Calendar.HOUR_OF_DAY);

        boolean inBossHour = false;
        for (int h : BOSS_SPAWN_HOURS) {
            if (hour == h) {
                inBossHour = true;
                break;
            }
        }

        if (inBossHour && !bossAlive && now >= nextBossSpawnTime) {
            spawnBoss();
        }

        if (bossAlive && activeBoss != null && now >= bossSpawnTime + BOSS_LIFETIME_MS) {
            despawnBoss("Boss Thầy Giáo Hắc Ám đã rời đi!");
        }
    }

    // ================== BOSS SỰ KIỆN ==================

    public void forceSpawnBoss(Player p) {
        if (bossAlive) {
            if (p != null) {
                try {
                    Service.send_box_ThongBao_OK(p, "Boss sự kiện hiện đang còn sống!");
                } catch (IOException e) {
                    // ignore
                }
            }
            return;
        }
        spawnBoss();
    }

    private void spawnBoss() {
        if (bossAlive) return;

        List<Map> allowedMaps = new ArrayList<>();
        for (int mapId : Boss.ALLOWED_MAP_IDS) {
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
            System.out.println("Lỗi: Không tìm thấy MobTemplate " + MOB_BOSS_LAN_SU_TU + " cho Boss 20/11");
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
        bossDamageList.clear();
        participatedPlayers.clear();

        try {
            broadcastMessage("🦁 Boss Lân Sư Tử xuất hiện tại " + targetMap.template.name + " khu " + (targetMap.zone_id + 1) + "! Đòn kết liễu (Last Hit) nhận 2 Chứng Nhận Sư Phụ & 1 Hộp Quà Cao Cấp!");

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

    public void onBossDamaged(Player p, int dame) {
        if (!bossAlive || p == null || dame <= 0) return;

        if (!participatedPlayers.contains(p)) {
            participatedPlayers.add(p);
        }

        boolean found = false;
        for (Top_Dame td : bossDamageList) {
            if (td.name.equals(p.name)) {
                td.dame += dame;
                found = true;
                break;
            }
        }
        if (!found) {
            Top_Dame td = new Top_Dame();
            td.name = p.name;
            td.dame = dame;
            bossDamageList.add(td);
        }
    }

    public void onBossKilled(Player killer) {
        if (!bossAlive) return;

        bossAlive = false;

        broadcastMessage("🎉 Chúc mừng " + (killer != null ? killer.name : "anh hùng") + " đã tung đòn kết liễu hạ gục Boss Lân Sư Tử!");

        // Chỉ trao phần thưởng Đòn kết liễu (Last Hit): 2 Chứng Nhận Sư Phụ + 1 Hộp Quà Cao Cấp
        if (killer != null) {
            killer.item.add_item_bag47(4, ITEM_CHUNG_NHAN_SU_PHU, 2);
            killer.item.add_item_bag47(4, ITEM_HOP_QUA_CAO_CAP, 1);
            try {
                killer.item.update_Inventory(-1, false);
                Service.send_box_ThongBao_OK(killer, "🎉 Phần thưởng Đòn Kết Liễu Boss Lân Sư Tử: Nhận 2 Chứng Nhận Sư Phụ & 1 Hộp Quà Cao Cấp!");
            } catch (IOException e) {
                // ignore
            }
        }

        scheduleNextBossSpawn();
    }

    private void despawnBoss(String msg) {
        if (activeBoss != null && activeBoss.mob != null) {
            try {
                activeBoss.mob.isdie = true;
                activeBoss.mob.hp = 0;
                if (activeBoss.mob.map != null) {
                    activeBoss.mob.map.remove_obj(activeBoss.mob.index, 1);
                }
            } catch (IOException e) {
                // ignore
            }
        }
        bossAlive = false;
        activeBoss = null;
        scheduleNextBossSpawn();
        if (msg != null) {
            broadcastMessage(msg);
        }
    }

    // ================== TÍCH HỢP RƠI ĐỒ TỪ HOẠT ĐỘNG ==================

    public static void addMaterial(Player p, int itemId, int quantity) {
        if (p == null || quantity <= 0) return;
        p.item.add_item_bag47(4, itemId, quantity);
        try {
            p.item.update_Inventory(-1, false);
        } catch (IOException e) {
            // ignore
        }
    }

    /**
     * Rơi đồ khi đánh quái thường (±10 level)
     */
    public static void onMobKill(Player p, Mob mob) {
        if (!isEvent() || p == null || mob == null) return;
        if (Math.abs(p.level - mob.level) > 10) return;

        int rateMultiplier = isGioVang() ? 2 : 1;

        // Tỷ lệ rơi Trang Giấy (451)
        if (Util.random(100) < 15 * rateMultiplier) {
            addMaterial(p, ITEM_TRANG_GIAY, 1);
        }

        // Tỷ lệ rơi Cánh Hoa Phượng (196)
        if (Util.random(100) < 10 * rateMultiplier) {
            addMaterial(p, ITEM_CANH_HOA_PHUONG, 1);
        }
    }

    /**
     * Thưởng khi hoàn thành Nhiệm vụ lặp (Quest lặp)
     */
    public static void rewardQuestLap(Player p) {
        if (!isEvent() || p == null) return;
        addMaterial(p, ITEM_LO_MUC, 1);
        addMaterial(p, ITEM_GIAY_DO, 1);
        try {
            Service.send_box_ThongBao_OK(p, "🎓 Nhận được 1 Lọ Mực & 1 Giấy Đỏ từ Nhiệm vụ lặp!");
        } catch (IOException e) {
            // ignore
        }
    }

    /**
     * Thưởng khi hoàn thành Hoạt động Bang Hội (Nhiệm vụ Băng / Phó bản Băng)
     */
    public static void rewardClan(Player p) {
        if (!isEvent() || p == null) return;
        addMaterial(p, ITEM_SACH_CONG_THUC, 2);
        addMaterial(p, ITEM_CANH_HOA_PHUONG, 3);
        try {
            Service.send_box_ThongBao_OK(p, "🎓 Nhận được 2 Sách Công Thức & 3 Cánh Hoa Phượng từ Hoạt động Bang!");
        } catch (IOException e) {
            // ignore
        }
    }

    /**
     * Thưởng khi hoàn thành Phó bản (Nami / Mr.3 / Hang Động)
     */
    public static void rewardDungeon(Player p) {
        if (!isEvent() || p == null) return;
        addMaterial(p, ITEM_GIAY_GOI_QUA, 2);
        addMaterial(p, ITEM_GAU_BONG, 1);
        try {
            Service.send_box_ThongBao_OK(p, "🎓 Nhận được 2 Giấy Gói Quà & 1 Gấu Bông từ Phó Bản!");
        } catch (IOException e) {
            // ignore
        }
    }

    /**
     * Thưởng khi vượt ải Thử thách Vệ Thần (Wipper)
     */
    public static void rewardTower(Player p) {
        if (!isEvent() || p == null) return;
        addMaterial(p, ITEM_GAU_BONG, 1);
        addMaterial(p, ITEM_SACH_CONG_THUC, 2);
        try {
            Service.send_box_ThongBao_OK(p, "🎓 Nhận được 1 Gấu Bông & 2 Sách Công Thức từ Thử Thách Vệ Thần!");
        } catch (IOException e) {
            // ignore
        }
    }

    /**
     * Thưởng khi thắng PvP / Đấu Trường / Truy Nã
     */
    public static void rewardPvpArena(Player p) {
        if (!isEvent() || p == null) return;
        addMaterial(p, ITEM_BAN_NHAC, 1);
        try {
            Service.send_box_ThongBao_OK(p, "🎓 Nhận được 1 Bản Nhạc Kích Lệ từ Hoạt động Chiến Đấu!");
        } catch (IOException e) {
            // ignore
        }
    }

    /**
     * Thưởng khi hoàn thành Vận Buôn
     */
    public static void rewardTrade(Player p) {
        if (!isEvent() || p == null) return;
        addMaterial(p, ITEM_GIAY_GOI_QUA, 3);
        try {
            Service.send_box_ThongBao_OK(p, "🎓 Nhận được 3 Giấy Gói Quà từ Chuyến Vận Buôn!");
        } catch (IOException e) {
            // ignore
        }
    }

    // ================== SỬ DỤNG ITEM VÀ MỞ HỘP QUÀ ==================

    public static boolean isEventItem(int itemId) {
        return itemId == ITEM_DIEM_10 || itemId == ITEM_THIEP_TRI_AN
                || itemId == ITEM_HOP_QUA_SO_CAP || itemId == ITEM_HOP_QUA_CAO_CAP
                || itemId == ITEM_HOP_QUA_DAC_BIET;
    }

    /**
     * Sử dụng Bông Hoa Điểm 10 (319)
     */
    public static void useDiem10(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện 20/11 chưa được kích hoạt!");
            return;
        }

        long expGain = p.level * 50_000L;
        int beriGain = Util.random(100_000, 500_000);
        p.update_exp(expGain, false);

        List<GiftBox> rewards = new ArrayList<>();
        GiftBox g1 = createGift(4, 0, beriGain, "Beri");
        rewards.add(g1);

        // Ngẫu nhiên nhận 1 trong các phần quà quý
        int randGift = Util.random(4);
        switch (randGift) {
            case 0: {
                // Mai rùa (ID 6 type 7)
                int maiRuaNum = Util.random(1, 3);
                rewards.add(createGift(7, 6, maiRuaNum, "Mai rùa"));
                break;
            }
            case 1: {
                // Bột vàng (ID 4 type 7)
                int botVangNum = Util.random(2, 5);
                rewards.add(createGift(7, 4, botVangNum, "Bột vàng"));
                break;
            }
            case 2: {
                // Ngôi sao may mắn (ID 5 type 7)
                int ngoiSaoNum = Util.random(1, 3);
                rewards.add(createGift(7, 5, ngoiSaoNum, "Ngôi sao may mắn"));
                break;
            }
            case 3: {
                // Bột than hoặc Bột tím (ID 2, 3 type 7)
                int bonusId = Util.random(2) == 0 ? 2 : 3;
                int bonusNum = Util.random(2, 5);
                rewards.add(createGift(7, bonusId, bonusNum, bonusId == 2 ? "Bột than" : "Bột tím"));
                break;
            }
        }

        Service.send_gift(p, 0, "Dâng tặng Bông Hoa Điểm 10:", "Bạn nhận được +" + Util.number_format(expGain) + " EXP!", rewards, true);
    }

    /**
     * Sử dụng Thiệp Tri Ân 20/11 (382)
     */
    public static void useThiepTriAn(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện 20/11 chưa được kích hoạt!");
            return;
        }

        List<GiftBox> rewards = new ArrayList<>();

        // Ngẫu nhiên 1 Đá Khảm cấp 5 - 6 (Topaz, Ruby, Saphia, Ngọc lục bảo, Cẩm thạch, Thạch anh)
        int gemType = Util.random(6);
        int gemId = 48 + gemType * 6 + Util.random(2); // Cấp 5 hoặc 6
        ItemTemplate4 gemTemp = ItemTemplate4.get_it_by_id(gemId);
        if (gemTemp != null) {
            rewards.add(createGift(4, gemId, 1, gemTemp.name));
        }

        Service.send_gift(p, 0, "Mở Thiệp Tri Ân 20/11:", "Nhận Đá Khảm Cấp 5-6!", rewards, true);
    }

    /**
     * Mở Hộp Quà Tri Ân Sơ Cấp (586)
     */
    public static void openHopQuaSoCap(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện 20/11 chưa được kích hoạt!");
            return;
        }

        getInstance().addPoint(p, 1);

        List<GiftBox> rewards = new ArrayList<>();
        int rand = Util.random(8);
        switch (rand) {
            case 0: {
                // Beri
                int beri = Util.random(500_000, 2_000_000);
                rewards.add(createGift(4, 0, beri, "Beri"));
                break;
            }
            case 1: {
                // Ruby
                int ruby = Util.random(5, 20);
                rewards.add(createGift(4, 1, ruby, "Ruby"));
                break;
            }
            case 2: {
                // Đá khảm cấp 3-4
                int gemType = Util.random(6);
                int gemId = 46 + gemType * 6 + Util.random(2); // Cấp 3-4
                ItemTemplate4 gemTemp = ItemTemplate4.get_it_by_id(gemId);
                if (gemTemp != null) {
                    rewards.add(createGift(4, gemId, 1, gemTemp.name));
                }
                break;
            }
            case 3: {
                // Bột vàng (cat 7, id 4) hoặc Bùa cường hóa (cat 7, id 12)
                int mat7Id = Util.random(2) == 0 ? 4 : 12;
                ItemTemplate7 mat7Temp = ItemTemplate7.get_it_by_id(mat7Id);
                if (mat7Temp != null) {
                    rewards.add(createGift(7, mat7Id, Util.random(1, 3), mat7Temp.name));
                }
                break;
            }
            case 4: {
                // Rương Cam theo cấp độ
                int chestLevel = Math.max(10, Math.min(100, (p.level / 10) * 10));
                int chestId = 121 + (chestLevel / 10);
                ItemTemplate4 chestTemp = ItemTemplate4.get_it_by_id(chestId);
                if (chestTemp != null) {
                    rewards.add(createGift(4, chestId, 1, chestTemp.name));
                }
                break;
            }
            case 5: {
                // Khiên x1 (Item 7 ID 10)
                ItemTemplate7 khienTemp = ItemTemplate7.get_it_by_id(10);
                if (khienTemp != null) {
                    rewards.add(createGift(7, 10, 1, khienTemp.name));
                }
                break;
            }
            case 6: {
                // Búa sơ cấp x1 (Item 4 ID 339)
                ItemTemplate4 buaSoCapTemp = ItemTemplate4.get_it_by_id(339);
                if (buaSoCapTemp != null) {
                    rewards.add(createGift(4, 339, 1, buaSoCapTemp.name));
                }
                break;
            }
            case 7: {
                // Búa đục DIAL x1 (Item 4 ID 457)
                ItemTemplate4 buaDialTemp = ItemTemplate4.get_it_by_id(457);
                if (buaDialTemp != null) {
                    rewards.add(createGift(4, 457, 1, buaDialTemp.name));
                }
                break;
            }
        }

        Service.send_gift(p, 0, "Quà từ Hộp Tri Ân Sơ Cấp:", "(+1 Điểm Tri Ân BXH)", rewards, true);
    }

    /**
     * Mở Hộp Quà Tri Ân Cao Cấp (587)
     */
    public static void openHopQuaCaoCap(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện 20/11 chưa được kích hoạt!");
            return;
        }

        getInstance().addPoint(p, 3);

        List<GiftBox> rewards = new ArrayList<>();
        int rand = Util.random(9);
        switch (rand) {
            case 0: {
                // Beri
                int beri = Util.random(2_000_000, 10_000_000);
                rewards.add(createGift(4, 0, beri, "Beri"));
                break;
            }
            case 1: {
                // Ruby
                int ruby = Util.random(50, 200);
                rewards.add(createGift(4, 1, ruby, "Ruby"));
                break;
            }
            case 2: {
                // Đá khảm cấp 5-6
                int gemType = Util.random(6);
                int gemId = 48 + gemType * 6 + Util.random(2); // Cấp 5-6
                ItemTemplate4 gemTemp = ItemTemplate4.get_it_by_id(gemId);
                if (gemTemp != null) {
                    rewards.add(createGift(4, gemId, 1, gemTemp.name));
                }
                break;
            }
            case 3: {
                // Đá Hải Thạch cấp 3-4 (223-224)
                int haiThachId = Util.random(223, 224);
                ItemTemplate4 htTemp = ItemTemplate4.get_it_by_id(haiThachId);
                if (htTemp != null) {
                    rewards.add(createGift(4, haiThachId, Util.random(1, 2), htTemp.name));
                }
                break;
            }
            case 4: {
                // Thẻ đổi tên (271)
                ItemTemplate4 nameTemp = ItemTemplate4.get_it_by_id(271);
                if (nameTemp != null) {
                    rewards.add(createGift(4, 271, 1, nameTemp.name));
                }
                break;
            }
            case 5: {
                // Tiến cấp đơn (413) hoặc Kỹ năng đơn (414)
                int donId = Util.random(2) == 0 ? 413 : 414;
                ItemTemplate4 donTemp = ItemTemplate4.get_it_by_id(donId);
                if (donTemp != null) {
                    rewards.add(createGift(4, donId, 2, donTemp.name));
                }
                break;
            }
            case 6: {
                // Khiên x1 (Item 7 ID 10)
                ItemTemplate7 khienTemp = ItemTemplate7.get_it_by_id(10);
                if (khienTemp != null) {
                    rewards.add(createGift(7, 10, 1, khienTemp.name));
                }
                break;
            }
            case 7: {
                // Búa sơ cấp x1 (Item 4 ID 339)
                ItemTemplate4 buaSoCapTemp = ItemTemplate4.get_it_by_id(339);
                if (buaSoCapTemp != null) {
                    rewards.add(createGift(4, 339, 1, buaSoCapTemp.name));
                }
                break;
            }
            case 8: {
                // Búa đục DIAL x1 (Item 4 ID 457)
                ItemTemplate4 buaDialTemp = ItemTemplate4.get_it_by_id(457);
                if (buaDialTemp != null) {
                    rewards.add(createGift(4, 457, 1, buaDialTemp.name));
                }
                break;
            }
        }

        Service.send_gift(p, 0, "Quà từ Hộp Tri Ân Cao Cấp:", "(+3 Điểm Tri Ân BXH)", rewards, true);
    }

    /**
     * Mở Hộp Quà Tôn Sư Trọng Đạo (596)
     */
    public static void openHopQuaDacBiet(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện 20/11 chưa được kích hoạt!");
            return;
        }

        getInstance().addPoint(p, 5);

        List<GiftBox> rewards = new ArrayList<>();
        int rand = Util.random(7);
        switch (rand) {
            case 0: {
                // Beri
                int beri = Util.random(10_000_000, 50_000_000);
                rewards.add(createGift(4, 0, beri, "Beri"));
                break;
            }
            case 1: {
                // Ruby
                int ruby = Util.random(200, 1000);
                rewards.add(createGift(4, 1, ruby, "Ruby"));
                break;
            }
            case 2: {
                // Khảm lv6 tự chọn (588)
                ItemTemplate4 stoneTemp = ItemTemplate4.get_it_by_id(588);
                if (stoneTemp != null) {
                    rewards.add(createGift(4, 588, 1, stoneTemp.name));
                }
                break;
            }
            case 3: {
                // Đá vô cực S (326)
                ItemTemplate4 stoneTemp = ItemTemplate4.get_it_by_id(326);
                if (stoneTemp != null) {
                    rewards.add(createGift(4, 326, 1, stoneTemp.name));
                }
                break;
            }
            case 4: {
                // Đá Hải Thạch cấp 5-6 (225-226)
                int haiThachId = Util.random(225, 226);
                ItemTemplate4 htTemp = ItemTemplate4.get_it_by_id(haiThachId);
                if (htTemp != null) {
                    rewards.add(createGift(4, haiThachId, Util.random(1, 2), htTemp.name));
                }
                break;
            }
            case 5: {
                // Bảo hiểm chuyển hóa cao (551)
                ItemTemplate4 bhTemp = ItemTemplate4.get_it_by_id(551);
                rewards.add(createGift(4, 551, 1, bhTemp != null ? bhTemp.name : "Bảo hiểm chuyển hóa cao"));
                break;
            }
            case 6: {
                // Trái Ác Quỷ Trung Cấp (87) hoặc Rương Đại Ác Quỷ (158)
                int devilId = Util.random(2) == 0 ? 87 : 158;
                ItemTemplate4 devilTemp = ItemTemplate4.get_it_by_id(devilId);
                if (devilTemp != null) {
                    rewards.add(createGift(4, devilId, 1, devilTemp.name));
                }
                break;
            }
        }

        Service.send_gift(p, 0, "Quà Tôn Sư Trọng Đạo (Đặc Biệt):", "(+5 Điểm Tri Ân BXH)", rewards, true);
    }

    private static GiftBox createGift(int type, int id, int num, String name) {
        GiftBox g = new GiftBox();
        g.type = (byte) type;
        g.id = (short) id;
        g.num = num;
        g.name = name;
        if (type == 4) {
            ItemTemplate4 temp = ItemTemplate4.get_it_by_id(id);
            if (temp != null) {
                g.icon = temp.icon;
                g.color = 0;
            }
        } else if (type == 7) {
            ItemTemplate7 temp = ItemTemplate7.get_it_by_id(id);
            if (temp != null) {
                g.icon = temp.icon;
                g.color = 0;
            }
        }
        return g;
    }

    // ================== BẢNG XẾP HẠNG TRI ÂN ==================

    public void addPoint(Player p, int pt) {
        if (p == null || pt <= 0) return;
        pointMap.merge(p.name, pt, Integer::sum);
        saveData();
    }

    public int getPlayerPoint(Player p) {
        if (p == null) return 0;
        return pointMap.getOrDefault(p.name, 0);
    }

    /**
     * Lấy thứ hạng của người chơi trong TOP 10 (1-10) hoặc -1 nếu không thuộc TOP 10
     */
    public int getRank(Player p) {
        if (p == null) return -1;
        List<Entry<String, Integer>> list = new ArrayList<>(pointMap.entrySet());
        list.sort((a, b) -> Integer.compare(b.getValue(), a.getValue()));
        for (int i = 0; i < Math.min(10, list.size()); i++) {
            if (list.get(i).getKey().equals(p.name) && list.get(i).getValue() > 0) {
                return i + 1;
            }
        }
        return -1;
    }

    public void showLeaderboard(Player p) {
        if (p == null) return;

        List<Entry<String, Integer>> list = new ArrayList<>(pointMap.entrySet());
        list.sort((a, b) -> Integer.compare(b.getValue(), a.getValue()));

        StringBuilder sb = new StringBuilder();
        sb.append("🎓 BẢNG XẾP HẠNG \"HỌC TRÒ XUẤT SẮC\" 20/11\n");
        sb.append("═══════════════════════════════════════\n\n");

        if (list.isEmpty()) {
            sb.append("Chưa có thuyền trưởng nào ghi danh!\n");
            sb.append("Hãy mở các Hộp Quà Tri Ân để tích lũy điểm nhé!\n");
        } else {
            for (int i = 0; i < Math.min(10, list.size()); i++) {
                Entry<String, Integer> entry = list.get(i);
                String rankIcon = (i == 0) ? "🥇" : (i == 1) ? "🥈" : (i == 2) ? "🥉" : (i + 1) + ".";
                sb.append(rankIcon).append(" ").append(entry.getKey())
                  .append(": ").append(Util.number_format(entry.getValue())).append(" Điểm\n");
            }
        }

        int rank = getRank(p);
        sb.append("\n═══════════════════════════════════════\n");
        sb.append("📊 Điểm Tri Ân của bạn: ").append(Util.number_format(getPlayerPoint(p))).append(" Điểm");
        if (rank != -1) {
            sb.append(" (Hạng: ").append(rank).append(")\n");
        } else {
            sb.append(" (Chưa vào Top 10)\n");
        }
        sb.append("🎁 Mở Quà Sơ Cấp (+1đ) | Cao Cấp (+3đ) | Đặc Biệt (+5đ)\n\n");
        sb.append("🏆 PHẦN THƯỞNG TỔNG KẾT ĐUA TOP:\n");
        sb.append("🥇 TOP 1: Danh hiệu Đại Thần (vĩnh viễn) + 3 Rương Đá Thần Thoại Tự Chọn + 1 Hộp Thời Trang Cao Cấp + 10.000 Ruby + 200M Beri + 5 Rương Đại Ác Quỷ\n");
        sb.append("🥈 TOP 2-3: Danh hiệu Thiên Tử + 2 Rương Đá Thần Thoại Tự Chọn + 1 Hộp Thời Trang Cao Cấp + 5.000 Ruby + 100M Beri + 3 Rương Đại Ác Quỷ\n");
        sb.append("🥉 TOP 4-10: Danh hiệu Bất Bại + 1 Đá Khảm Vô Cực S + 2.000 Ruby + 50M Beri + 2 Rương Ác Quỷ\n");

        try {
            Service.send_box_ThongBao_OK(p, sb.toString());
        } catch (IOException e) {
            // ignore
        }
    }

    /**
     * Người chơi trực tiếp nhận quà khi có tên trong TOP 10 BXH Học Trò Xuất Sắc
     */
    public void claimLeaderboardReward(Player p) {
        if (p == null) return;
        if (!isEvent()) {
            try {
                Service.send_box_ThongBao_OK(p, "Sự kiện 20/11 chưa được kích hoạt!");
            } catch (IOException e) {}
            return;
        }

        if (claimedTopPlayers.contains(p.name)) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn đã nhận phần thưởng Đua Top Sự Kiện 20/11 rồi!");
            } catch (IOException e) {}
            return;
        }

        int rank = getRank(p);
        if (rank == -1) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn không nằm trong TOP 10 Bảng Xếp Hạng \"Học Trò Xuất Sắc\" để nhận quà!\n(Điểm Tri Ân hiện tại của bạn: " + Util.number_format(getPlayerPoint(p)) + " Điểm)");
            } catch (IOException e) {}
            return;
        }

        try {
            if (rank == 1) { // Top 1
                p.item.add_item_bag47(4, ITEM_RUONG_DA_THAN_THOAI_TU_CHON, 3);
                p.item.add_item_bag47(4, ITEM_HOP_THOI_TRANG_CAO_CAP, 1); // 1 Hộp thời trang cao cấp (1002)
                p.item.add_item_bag47(4, 158, 5); // 5 Rương Đại Ác Quỷ
                p.update_ngoc(10_000);
                p.update_vang(200_000_000);
                p.idDanhHieu = 67; // Đại Thần
                p.danhhieu = 67;
                Service.send_box_ThongBao_OK(p, "🏆 Chúc mừng bạn đã nhận thưởng TOP 1 Đua Top 20/11:\n• Danh hiệu Đại Thần (vĩnh viễn)\n• 3 Rương Đá Thần Thoại Tự Chọn\n• 1 Hộp Thời Trang Cao Cấp\n• 10.000 Ruby\n• 200.000.000 Beri\n• 5 Rương Đại Ác Quỷ");
                broadcastMessage("🎉 Chúc mừng thuyền trưởng " + p.name + " đã nhận phần thưởng TOP 1 Đua Top Sự Kiện 20/11!");
            } else if (rank <= 3) { // Top 2-3
                p.item.add_item_bag47(4, ITEM_RUONG_DA_THAN_THOAI_TU_CHON, 2);
                p.item.add_item_bag47(4, ITEM_HOP_THOI_TRANG_CAO_CAP, 1); // 1 Hộp thời trang cao cấp (1002)
                p.item.add_item_bag47(4, 158, 3); // 3 Rương Đại Ác Quỷ
                p.update_ngoc(5_000);
                p.update_vang(100_000_000);
                p.idDanhHieu = 68; // Thiên Tử
                p.danhhieu = 68;
                Service.send_box_ThongBao_OK(p, "🥈 Chúc mừng bạn đã nhận thưởng TOP " + rank + " Đua Top 20/11:\n• Danh hiệu Thiên Tử\n• 2 Rương Đá Thần Thoại Tự Chọn\n• 1 Hộp Thời Trang Cao Cấp\n• 5.000 Ruby\n• 100.000.000 Beri\n• 3 Rương Đại Ác Quỷ");
                broadcastMessage("🎉 Chúc mừng thuyền trưởng " + p.name + " đã nhận phần thưởng TOP " + rank + " Đua Top Sự Kiện 20/11!");
            } else { // Top 4-10
                p.item.add_item_bag47(4, ITEM_DA_KHAM_VO_CUC_S, 1); // 1 Đá Khảm Vô Cực S
                p.item.add_item_bag47(4, 29, 2);  // 2 Rương Ác Quỷ
                p.update_ngoc(2_000);
                p.update_vang(50_000_000);
                p.idDanhHieu = 64; // Bất Bại
                p.danhhieu = 64;
                Service.send_box_ThongBao_OK(p, "🥉 Chúc mừng bạn đã nhận thưởng TOP " + rank + " Đua Top 20/11:\n• Danh hiệu Bất Bại\n• 1 Đá Khảm Vô Cực S\n• 2.000 Ruby\n• 50.000.000 Beri\n• 2 Rương Ác Quỷ");
                broadcastMessage("🎉 Chúc mừng thuyền trưởng " + p.name + " đã nhận phần thưởng TOP " + rank + " Đua Top Sự Kiện 20/11!");
            }
            claimedTopPlayers.add(p.name);
            p.item.update_Inventory(-1, false);
            saveData();
        } catch (Exception e) {
            System.out.println("Error claiming leaderboard reward: " + e.getMessage());
        }
    }

    /**
     * Trao phần thưởng tổng kết Đua Top khi sự kiện kết thúc
     */
    public static void distributeEndTopRewards(Player admin) {
        List<Entry<String, Integer>> list = new ArrayList<>(getInstance().pointMap.entrySet());
        list.sort((a, b) -> Integer.compare(b.getValue(), a.getValue()));

        if (list.isEmpty()) {
            if (admin != null) {
                try {
                    Service.send_box_ThongBao_OK(admin, "Không có người chơi nào trong BXH để phát thưởng!");
                } catch (IOException e) {}
            }
            return;
        }

        for (int i = 0; i < Math.min(10, list.size()); i++) {
            Entry<String, Integer> entry = list.get(i);
            if (getInstance().claimedTopPlayers.contains(entry.getKey())) {
                continue;
            }
            Player pl = Map.get_player_by_name_allmap(entry.getKey());
            if (pl == null) continue;

            try {
                if (i == 0) { // Top 1
                    pl.item.add_item_bag47(4, ITEM_RUONG_DA_THAN_THOAI_TU_CHON, 3);
                    pl.item.add_item_bag47(4, ITEM_HOP_THOI_TRANG_CAO_CAP, 1); // 1 Hộp thời trang cao cấp (1002)
                    pl.item.add_item_bag47(4, 158, 5); // 5 Rương Đại Ác Quỷ
                    pl.update_ngoc(10_000);
                    pl.update_vang(200_000_000);
                    pl.idDanhHieu = 67; // Đại Thần
                    pl.danhhieu = 67;
                    Service.send_box_ThongBao_OK(pl, "🏆 Chúc mừng bạn đạt TOP 1 Đua Top 20/11: Nhận Danh hiệu Đại Thần (vĩnh viễn), 3 Rương Đá Thần Thoại Tự Chọn, 1 Hộp Thời Trang Cao Cấp, 10.000 Ruby, 200M Beri, 5 Rương Đại Ác Quỷ!");
                } else if (i < 3) { // Top 2-3
                    pl.item.add_item_bag47(4, ITEM_RUONG_DA_THAN_THOAI_TU_CHON, 2);
                    pl.item.add_item_bag47(4, ITEM_HOP_THOI_TRANG_CAO_CAP, 1); // 1 Hộp thời trang cao cấp (1002)
                    pl.item.add_item_bag47(4, 158, 3); // 3 Rương Đại Ác Quỷ
                    pl.update_ngoc(5_000);
                    pl.update_vang(100_000_000);
                    pl.idDanhHieu = 68; // Thiên Tử
                    pl.danhhieu = 68;
                    Service.send_box_ThongBao_OK(pl, "🥈 Chúc mừng bạn đạt TOP " + (i + 1) + " Đua Top 20/11: Nhận Danh hiệu Thiên Tử, 2 Rương Đá Thần Thoại Tự Chọn, 1 Hộp Thời Trang Cao Cấp, 5.000 Ruby, 100M Beri, 3 Rương Đại Ác Quỷ!");
                } else { // Top 4-10
                    pl.item.add_item_bag47(4, ITEM_DA_KHAM_VO_CUC_S, 1); // 1 Đá Khảm Vô Cực S
                    pl.item.add_item_bag47(4, 29, 2);  // 2 Rương Ác Quỷ
                    pl.update_ngoc(2_000);
                    pl.update_vang(50_000_000);
                    pl.idDanhHieu = 64; // Bất Bại
                    pl.danhhieu = 64;
                    Service.send_box_ThongBao_OK(pl, "🥉 Chúc mừng bạn đạt TOP " + (i + 1) + " Đua Top 20/11: Nhận Danh hiệu Bất Bại, 1 Đá Khảm Vô Cực S, 2.000 Ruby, 50M Beri, 2 Rương Ác Quỷ!");
                }
                getInstance().claimedTopPlayers.add(pl.name);
                pl.item.update_Inventory(-1, false);
            } catch (Exception e) {
                System.out.println("Error rewarding leaderboard top: " + e.getMessage());
            }
        }
        getInstance().saveData();
        broadcastMessage("🎉 Đã phát thưởng Tổng Kết Đua Top Sự Kiện 20/11 cho TOP 10 thuyền trưởng xuất sắc nhất!");
    }

    // ================== LƯU TRỮ DỮ LIỆU SỰ KIỆN ==================

    @SuppressWarnings("unchecked")
    private synchronized void saveData() {
        try {
            JSONObject root = new JSONObject();
            JSONObject pointsObj = new JSONObject();
            for (Entry<String, Integer> e : pointMap.entrySet()) {
                pointsObj.put(e.getKey(), e.getValue());
            }
            root.put("points", pointsObj);

            JSONArray claimedArr = new JSONArray();
            for (String name : claimedTopPlayers) {
                claimedArr.add(name);
            }
            root.put("claimed", claimedArr);

            File f = new File("event_2011_data.json");
            try (FileWriter fw = new FileWriter(f)) {
                fw.write(root.toJSONString());
            }
        } catch (Exception e) {
            System.out.println("Error saving event 20/11 data: " + e.getMessage());
        }
    }

    private synchronized void loadData() {
        try {
            File f = new File("event_2011_data.json");
            if (!f.exists()) return;
            String content = new String(Files.readAllBytes(f.toPath()), StandardCharsets.UTF_8);
            Object obj = JSONValue.parse(content);
            if (obj instanceof JSONObject) {
                JSONObject root = (JSONObject) obj;
                JSONObject pointsObj = (JSONObject) root.get("points");
                if (pointsObj != null) {
                    for (Object k : pointsObj.keySet()) {
                        String name = (String) k;
                        int pts = Integer.parseInt(pointsObj.get(k).toString());
                        pointMap.put(name, pts);
                    }
                }
                JSONArray claimedArr = (JSONArray) root.get("claimed");
                if (claimedArr != null) {
                    for (Object o : claimedArr) {
                        claimedTopPlayers.add(o.toString());
                    }
                }
            }
        } catch (Exception e) {
            System.out.println("Error loading event 20/11 data: " + e.getMessage());
        }
    }

    // ================== TIỆN ÍCH ==================

    private static void broadcastMessage(String msg) {
        try {
            Manager.gI().chatKTG(0, msg, 5);
        } catch (IOException e) {
            System.out.println("Error broadcasting 20/11 message: " + e.getMessage());
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
}
