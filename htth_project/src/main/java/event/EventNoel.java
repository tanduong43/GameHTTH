package event;

import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;

import client.Player;
import core.Manager;
import core.Service;
import core.Util;
import io.Message;
import map.Boss;
import map.Mob;
import template.GiftBox;
import template.ItemFashionP2;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.Top_Dame;

/**
 * Sự Kiện Giáng Sinh (Noel) - Game HTTH
 * "GIÁNG SINH AN LÀNH - ĐẠI HẢI TRÌNH TUYẾT TRẮNG"
 * 
 * Sử dụng 100% tài nguyên có sẵn trong database (item4, item8, fashiontemplate, pet_template, danhhieu, mobs).
 */
public class EventNoel implements Runnable {

    // ================== CẤU HÌNH SỰ KIỆN ==================
    public static boolean IS_OPEN = false;
    private static final String CONFIG_KEY = "event-noel";
    private static final String DATA_FILE = "event_noel_data.json";

    // Item IDs - Nguyên liệu
    public static final int ITEM_NAM_TUYET = 341;             // Nắm tuyết mùa đông
    public static final int ITEM_CUC_AO = 342;                 // Cúc áo đắp người tuyết
    public static final int ITEM_NON_NOEL = 347;               // Nón giáng sinh
    public static final int ITEM_CHUONG_NOEL = 486;            // Chuông giáng sinh vàng
    public static final int ITEM_VO_NOEL = 487;                // Vớ / Tất giáng sinh đỏ
    public static final int ITEM_NGOI_SAO_NOEL = 488;          // Ngôi sao giáng sinh
    public static final int ITEM_KEO_NOEL = 489;               // Kẹo giáng sinh gậy kẹo
    public static final int ITEM_GIAY_GOI_QUA = 575;           // Giấy gói quà
    public static final int ITEM_GAU_BONG = 590;               // Gấu bông ấm áp
    public static final int ITEM_BONG_TUYET = 611;             // Bóng tuyết ném
    public static final int ITEM_TAT_NOEL = 613;               // Tất Noel

    // Item IDs - Thành phẩm & Quà tặng
    public static final int ITEM_THIEP_NOEL = 340;             // Thiệp giáng sinh
    public static final int ITEM_HOP_QUA_NOEL = 227;           // Hộp quà Noel sơ cấp (+1đ)
    public static final int ITEM_TUI_GIANG_SINH = 485;         // Túi giáng sinh (+2đ)
    public static final int ITEM_HOP_QUA_GIANG_SINH_VIP = 492; // Hộp quà giáng sinh VIP (+5đ)
    public static final int ITEM_RUONG_TT_NOEL = 622;          // Rương trang phục noel
    public static final int ITEM_VE_DOI_TT_NOEL = 229;         // Vé đổi trang phục Noel
    public static final int ITEM_VE_NOEL = 230;                // Vé Noel
    public static final int ITEM_RUONG_PET_NOEL = 1011;        // Rương Pet Noel
    public static final int ITEM_PET_LOC_NOEL = 708;           // Pet Lộc Noel
    public static final int ITEM_PET_TUYET_NOEL = 709;         // Pet Tuyết Noel
    public static final int ITEM_PET_TUAN_LOC = 720;           // Pet Tuần Lộc

    // Item IDs - Rương & Đá Đua Top
    public static final int ITEM_RUONG_DA_THAN_THOAI_TU_CHON = 1004; // Rương đá thần thoại tự chọn
    public static final int ITEM_HOP_THOI_TRANG_CAO_CAP = 1002;      // Hộp thời trang cao cấp
    public static final int ITEM_DA_KHAM_VO_CUC_S = 326;             // Đá khảm vô cực S
    public static final int ITEM_RUONG_DAI_AC_QUY = 158;             // Rương đại ác quỷ
    public static final int ITEM_RUONG_AC_QUY = 29;                  // Rương ác quỷ

    // Boss & Mob IDs
    public static final int MOB_BOSS_QUAI_VAT_TUYET = 99;     // Boss Quái Vật Tuyết (HP 100M, 1 Hit = 1 Dame)
    public static final int MOB_NGUOI_TUYET = 98;             // Người tuyết
    public static final int MOB_TUYET_NHO = 134;              // Tuyết nhỏ

    // Danh hiệu IDs
    public static final int DANH_HIEU_DAI_THAN = 67;           // Danh hiệu Đại Thần (TOP 1)
    public static final int DANH_HIEU_THIEN_TU = 68;           // Danh hiệu Thiên Tử (TOP 2-3)
    public static final int DANH_HIEU_BAT_BAI = 64;            // Danh hiệu Bất Bại (TOP 4-10)
    public static final int DANH_HIEU_THU_CUNG_NOEL = 9;       // Danh hiệu Thú Cưng Noel

    // Thời trang IDs
    public static final int FASHION_HOANG_TU_TUYET = 75;       // Hoàng tử tuyết
    public static final int FASHION_CONG_CHUA_TUYET = 76;      // Công chúa tuyết
    public static final int FASHION_NOEL_NU = 45;              // Noel Nữ
    public static final int FASHION_BE_TUYET = 46;             // Bé tuyết

    // Giờ spawn Boss (12h, 18h, 20h, 22h)
    private static final int[] BOSS_SPAWN_HOURS = { 12, 18, 20, 22 };
    private static final long BOSS_LIFETIME_MS = 30 * 60 * 1000L; // 30 phút

    // ================== RUNTIME STATE ==================
    private static EventNoel instance;
    private final Thread eventThread;
    private volatile boolean running = true;

    // Boss State
    private Boss activeBoss;
    private long nextBossSpawnTime = 0;
    private long bossSpawnTime = 0;
    private boolean bossAlive = false;
    private final List<Top_Dame> bossDamageList = new CopyOnWriteArrayList<>();
    private final List<Player> participatedPlayers = new CopyOnWriteArrayList<>();

    // BXH Điểm Giáng Sinh (Player Name -> Point)
    private final ConcurrentHashMap<String, Integer> pointMap = new ConcurrentHashMap<>();

    // Scheduler
    private final ScheduledExecutorService scheduler = Executors.newScheduledThreadPool(2, r -> {
        Thread t = new Thread(r, "EventNoel-Scheduler");
        t.setDaemon(true);
        return t;
    });

    // ================== KHỞI TẠO ==================
    private EventNoel() {
        loadData();
        this.eventThread = new Thread(this, "EventNoel-Main");
        this.eventThread.start();
    }

    public static EventNoel getInstance() {
        if (instance == null) {
            synchronized (EventNoel.class) {
                if (instance == null) {
                    instance = new EventNoel();
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
            broadcastMessage("🎄 Sự kiện Giáng Sinh (Noel): Đại Hải Trình Tuyết Trắng đã chính thức khai mở!");
        } else {
            broadcastMessage("🎄 Sự kiện Giáng Sinh (Noel) đã khép lại. Chúc các thuyền trưởng một mùa đông ấm áp!");
            getInstance().cleanup();
        }
    }

    public static void loadConfig(java.util.Properties config) {
        String value = config.getProperty(CONFIG_KEY);
        if (value != null) {
            IS_OPEN = Boolean.parseBoolean(value);
        }
        if (IS_OPEN) {
            getInstance().scheduleNextBossSpawn();
            System.out.println("[EventNoel] Kích hoạt sự kiện Noel: IS_OPEN = true");
        } else {
            System.out.println("[EventNoel] Sự kiện Noel hiện đang tắt: IS_OPEN = false");
        }
    }

    @Override
    public void run() {
        while (running) {
            try {
                if (IS_OPEN) {
                    long now = System.currentTimeMillis();
                    // Kiểm tra thời gian spawn boss
                    if (!bossAlive && nextBossSpawnTime > 0 && now >= nextBossSpawnTime) {
                        spawnBossQuaiVatTuyet();
                    }
                    // Kiểm tra hết hạn boss (30 phút không ai tiêu diệt)
                    if (bossAlive && (now - bossSpawnTime) >= BOSS_LIFETIME_MS) {
                        despawnBoss("⏰ Boss Quái Vật Tuyết đã biến mất vào bão tuyết do hết thời gian khiêu chiến!");
                    }
                }
                Thread.sleep(5000);
            } catch (InterruptedException e) {
                break;
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    // ================== QUẢN LÝ BOSS QUÁI VẬT TUYẾT ==================
    public void scheduleNextBossSpawn() {
        Calendar cal = Calendar.getInstance();
        int currentHour = cal.get(Calendar.HOUR_OF_DAY);
        int currentMinute = cal.get(Calendar.MINUTE);

        int nextHour = -1;
        for (int h : BOSS_SPAWN_HOURS) {
            if (h > currentHour || (h == currentHour && currentMinute < 0)) {
                nextHour = h;
                break;
            }
        }

        Calendar nextCal = (Calendar) cal.clone();
        if (nextHour != -1) {
            nextCal.set(Calendar.HOUR_OF_DAY, nextHour);
            nextCal.set(Calendar.MINUTE, 0);
            nextCal.set(Calendar.SECOND, 0);
            nextCal.set(Calendar.MILLISECOND, 0);
        } else {
            // Qua ngày hôm sau mốc đầu tiên
            nextCal.add(Calendar.DAY_OF_YEAR, 1);
            nextCal.set(Calendar.HOUR_OF_DAY, BOSS_SPAWN_HOURS[0]);
            nextCal.set(Calendar.MINUTE, 0);
            nextCal.set(Calendar.SECOND, 0);
            nextCal.set(Calendar.MILLISECOND, 0);
        }

        nextBossSpawnTime = nextCal.getTimeInMillis();
        long diffMinutes = (nextBossSpawnTime - System.currentTimeMillis()) / (60 * 1000);
        System.out.println("[EventNoel] Lên lịch Boss Quái Vật Tuyết tiếp theo: " + nextCal.getTime() + " (còn " + diffMinutes + " phút)");
    }

    public synchronized void spawnBossQuaiVatTuyet() {
        if (bossAlive && activeBoss != null) {
            return;
        }

        List<map.Map> allowedMaps = new ArrayList<>();
        for (int mapId : Boss.ALLOWED_MAP_IDS) {
            map.Map[] maps = map.Map.get_map_by_id(mapId);
            if (maps != null) {
                for (map.Map m : maps) {
                    if (m != null) {
                        allowedMaps.add(m);
                    }
                }
            }
        }

        if (allowedMaps.isEmpty()) {
            scheduleNextBossSpawn();
            return;
        }

        map.Map targetMap = allowedMaps.get(Util.random(allowedMaps.size()));

        Boss boss = new Boss();
        boss.id = 9999;
        boss.thegioi = 1;
        boss.mob = new Mob();
        boss.mob.mob_template = template.MobTemplate.ENTRYS.get(MOB_BOSS_QUAI_VAT_TUYET);
        if (boss.mob.mob_template == null) {
            System.out.println("Lỗi: Không tìm thấy MobTemplate " + MOB_BOSS_QUAI_VAT_TUYET + " cho Boss Quái Vật Tuyết");
            scheduleNextBossSpawn();
            return;
        }

        boss.mob.hp_max = 100_000_000;
        boss.hp_max_origin = 100_000_000;
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
            broadcastMessage("❄️ CẢNH BÁO: Boss Quái Vật Tuyết Giờ Vàng đã xuất hiện tại " + targetMap.template.name + " khu " + (targetMap.zone_id + 1) + "! Đòn kết liễu (Last Hit) nhận Rương Pet Noel + Hộp Quà VIP!");

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
            System.out.println("Error announcing Noel boss spawn: " + e.getMessage());
        }
        scheduleNextBossSpawn();
    }

    public synchronized void forceSpawnBoss(Player gm) {
        if (bossAlive && activeBoss != null) {
            if (gm != null) {
                try {
                    Service.send_box_ThongBao_OK(gm, "Boss Quái Vật Tuyết hiện đang còn sống!");
                } catch (IOException e) {
                }
            }
            return;
        }
        spawnBossQuaiVatTuyet();
        if (gm != null) {
            try {
                Service.send_box_ThongBao_OK(gm, "Đã gọi thành công Boss Quái Vật Tuyết!");
            } catch (IOException e) {
            }
        }
    }

    public synchronized void onBossDamaged(Player p, int damage) {
        if (!bossAlive || activeBoss == null || p == null) {
            return;
        }
        if (!participatedPlayers.contains(p)) {
            participatedPlayers.add(p);
        }
        boolean found = false;
        for (Top_Dame td : bossDamageList) {
            if (td.name.equals(p.name)) {
                td.dame += damage;
                found = true;
                break;
            }
        }
        if (!found) {
            Top_Dame newTd = new Top_Dame();
            newTd.name = p.name;
            newTd.dame = damage;
            bossDamageList.add(newTd);
        }
    }

    /**
     * Xử lý khi Boss Quái Vật Tuyết bị tiêu diệt
     * DUY NHẤT người tung đòn Last Hit nhận thưởng
     */
    public synchronized void onBossKilled(Player killer) {
        if (!bossAlive || activeBoss == null) {
            return;
        }

        bossAlive = false;
        activeBoss = null;

        if (killer != null) {
            try {
                // Trao thưởng đòn kết liễu Last Hit
                killer.item.add_item_bag47(4, ITEM_RUONG_PET_NOEL, 1);
                killer.item.add_item_bag47(4, ITEM_HOP_QUA_GIANG_SINH_VIP, 1);
                killer.item.add_item_bag47(4, ITEM_VE_DOI_TT_NOEL, 3);
                killer.item.update_Inventory(-1, false);

                // Không cộng điểm BXH từ boss - điểm chỉ tích từ mở hộp quà

                String winMsg = String.format("🎉 Chúc mừng thuyền trưởng [%s] đã tung đòn KẾT LIỄU Boss Quái Vật Tuyết, nhận được 1 Rương Pet Noel, 1 Hộp Quà Giáng Sinh VIP và 3 Vé Đổi Trang Phục Noel!",
                        killer.name);
                broadcastWorldChat(winMsg);
                Service.send_box_ThongBao_OK(killer, "🎉 Bạn đã tung đòn KẾT LIỄU Boss Quái Vật Tuyết!\nNhận: 1 Rương Pet Noel + 1 Hộp Quà VIP + 3 Vé Đổi Trang Phục Noel!");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        bossDamageList.clear();
        participatedPlayers.clear();
    }

    private synchronized void despawnBoss(String reason) {
        if (activeBoss != null && activeBoss.mob != null) {
            try {
                activeBoss.mob.isdie = true;
                activeBoss.mob.hp = 0;
                if (activeBoss.mob.map != null) {
                    activeBoss.mob.map.remove_obj(activeBoss.mob.index, 1);
                }
            } catch (IOException e) {
            }
        }
        activeBoss = null;
        bossAlive = false;
        bossDamageList.clear();
        participatedPlayers.clear();
        scheduleNextBossSpawn();
        if (reason != null) {
            broadcastMessage(reason);
        }
    }

    // ================== HOẠT ĐỘNG THU THẬP NGUYÊN LIỆU ==================

    /**
     * Đánh quái rơi nguyên liệu
     */
    public static void onMobKill(Player p, Mob mob) {
        if (!IS_OPEN || p == null || mob == null) {
            return;
        }

        int pLevel = p.level;
        int mobLevel = mob.level;
        if (Math.abs(pLevel - mobLevel) > 10) {
            return; // Chỉ rơi khi quái chênh lệch <= 10 cấp
        }

        // Tỷ lệ rơi Nắm Tuyết (341) - 8%
        if (Util.random(100) < 8) {
            p.item.add_item_bag47(4, ITEM_NAM_TUYET, 1);
        }
        // Tỷ lệ rơi Bóng Tuyết (611) - 4%
        if (Util.random(100) < 4) {
            p.item.add_item_bag47(4, ITEM_BONG_TUYET, 1);
        }
        // Tỷ lệ rơi Kẹo Giáng Sinh (489) - 3%
        if (Util.random(100) < 3) {
            p.item.add_item_bag47(4, ITEM_KEO_NOEL, 1);
        }
    }

    /**
     * Thưởng hoàn thành Phó bản Nami / Mr.3 / Băng Hải Tặc
     */
    public static void rewardDungeon(Player p) {
        if (!IS_OPEN || p == null) {
            return;
        }
        try {
            p.item.add_item_bag47(4, ITEM_GIAY_GOI_QUA, 2);
            p.item.add_item_bag47(4, ITEM_NON_NOEL, 1);
            p.item.add_item_bag47(4, ITEM_CHUONG_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎁 Nhận thưởng sự kiện Giáng Sinh:\n+2 Giấy Gói Quà\n+1 Nón Giáng Sinh\n+1 Chuông Giáng Sinh");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Thưởng thắng PvP Đấu trường Ms Gym / Lôi Đài
     */
    public static void rewardPvpArena(Player p) {
        if (!IS_OPEN || p == null) {
            return;
        }
        try {
            p.item.add_item_bag47(4, ITEM_NGOI_SAO_NOEL, 1);
            p.item.add_item_bag47(4, ITEM_VE_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎁 Thưởng thắng Đấu trường Giáng Sinh:\n+1 Ngôi Sao Giáng Sinh\n+1 Vé Noel");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Thưởng trả Nhiệm vụ lặp
     */
    public static void rewardRepeatQuest(Player p) {
        if (!IS_OPEN || p == null) {
            return;
        }
        try {
            p.item.add_item_bag47(4, ITEM_CUC_AO, 1);
            p.item.add_item_bag47(4, ITEM_VO_NOEL, 1);
            p.item.update_Inventory(-1, false);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Thưởng Vận buôn đường biển
     */
    public static void rewardSeaTrade(Player p) {
        if (!IS_OPEN || p == null) {
            return;
        }
        try {
            p.item.add_item_bag47(4, ITEM_GIAY_GOI_QUA, 3);
            p.item.add_item_bag47(4, ITEM_VE_DOI_TT_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🎁 Thưởng Chuyến buôn Giáng Sinh:\n+3 Giấy Gói Quà\n+1 Vé Đổi Trang Phục Noel");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    // ================== MỞ QUÀ & SỬ DỤNG VẬT PHẨM SỰ KIỆN ==================

    public static boolean isEventItem(int id) {
        return id == ITEM_THIEP_NOEL || id == ITEM_HOP_QUA_NOEL 
            || id == ITEM_TUI_GIANG_SINH || id == ITEM_HOP_QUA_GIANG_SINH_VIP 
            || id == ITEM_RUONG_TT_NOEL || id == ITEM_RUONG_PET_NOEL 
            || id == ITEM_KEO_NOEL || id == ITEM_BONG_TUYET;
    }

    /**
     * Sử dụng Thiệp Giáng Sinh (340)
     */
    public static void useThiepNoel(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_THIEP_NOEL) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Thiệp Giáng Sinh!");
                return;
            }

            p.item.remove_item47(4, ITEM_THIEP_NOEL, 1);

            int beri = Util.random(200_000, 1_000_000);
            p.update_vang(beri);

            // Quà ngẫu nhiên - chỉ nhận item, không buff
            String bonusName = "";
            int rand = Util.random(4);
            switch (rand) {
                case 0: {
                    int num = Util.random(1, 3);
                    p.item.add_item_bag47(7, 6, num); // Mai rùa
                    bonusName = num + "x Mai rùa";
                    break;
                }
                case 1: {
                    int num = Util.random(2, 5);
                    p.item.add_item_bag47(7, 4, num); // Bột vàng
                    bonusName = num + "x Bột vàng";
                    break;
                }
                case 2: {
                    int num = Util.random(1, 3);
                    p.item.add_item_bag47(7, 5, num); // Ngôi sao may mắn
                    bonusName = num + "x Ngôi sao may mắn";
                    break;
                }
                case 3: {
                    int num = Util.random(2, 5);
                    p.item.add_item_bag47(4, ITEM_KEO_NOEL, num); // Kẹo Noel
                    bonusName = num + "x Kẹo giáng sinh";
                    break;
                }
            }
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, String.format("💌 Mở Thiệp Giáng Sinh nhận được:\n+%,d Beri\n+%s", beri, bonusName));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Mở Hộp Quà Noel (227) - Tích +1 Điểm BXH
     */
    public static void openHopQuaNoel(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_HOP_QUA_NOEL) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Hộp Quà Noel!");
                return;
            }

            p.item.remove_item47(4, ITEM_HOP_QUA_NOEL, 1);
            getInstance().addPoint(p.name, 1);

            int beri = Util.random(500_000, 2_000_000);
            int ruby = Util.random(5, 20);
            p.update_vang(beri);
            p.update_ngoc(ruby);

            String rewardName = "";
            int rand = Util.random(7);
            switch (rand) {
                case 0: {
                    // Đá khảm 3-4
                    int[] gemIds = { 52, 53, 64, 65, 70, 71 };
                    int gem = gemIds[Util.random(gemIds.length)];
                    p.item.add_item_bag47(4, gem, 1);
                    ItemTemplate4 it = ItemTemplate4.get_it_by_id(gem);
                    rewardName = (it != null) ? it.name : "Đá khảm";
                    break;
                }
                case 1: {
                    int num = Util.random(1, 3);
                    p.item.add_item_bag47(7, 4, num); // Bột vàng
                    rewardName = num + "x Bột vàng";
                    break;
                }
                case 2:
                    p.item.add_item_bag47(7, 12, 1); // Bùa cường hóa
                    rewardName = "1x Bùa cường hóa";
                    break;
                case 3:
                    p.item.add_item_bag47(7, 10, 1); // Khiên bảo vệ
                    rewardName = "1x Khiên bảo vệ";
                    break;
                case 4:
                    p.item.add_item_bag47(4, 339, 1); // Búa sơ cấp
                    rewardName = "1x Búa sơ cấp";
                    break;
                case 5:
                    p.item.add_item_bag47(4, 457, 1); // Búa đục DIAL
                    rewardName = "1x Búa đục DIAL";
                    break;
                case 6: {
                    // Rương Cam ngẫu nhiên
                    int[] ruongCam = { 122, 123, 124, 125, 126, 127, 128, 129, 130, 131 };
                    int ruong = ruongCam[Util.random(ruongCam.length)];
                    p.item.add_item_bag47(4, ruong, 1);
                    ItemTemplate4 it = ItemTemplate4.get_it_by_id(ruong);
                    rewardName = (it != null) ? it.name : "Rương Cam";
                    break;
                }
            }

            p.item.update_Inventory(-1, false);
            int currentPoint = getInstance().getPoint(p.name);
            Service.send_box_ThongBao_OK(p, String.format("🎁 Mở Hộp Quà Noel nhận:\n+%,d Beri\n+%d Ruby\n+%s\n⭐ +1 Điểm BXH Noel (Hiện có: %d điểm)",
                    beri, ruby, rewardName, currentPoint));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Mở Túi Giáng Sinh (485) - Tích +2 Điểm BXH
     */
    public static void openTuiGiangSinh(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_TUI_GIANG_SINH) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Túi Giáng Sinh!");
                return;
            }

            p.item.remove_item47(4, ITEM_TUI_GIANG_SINH, 1);
            getInstance().addPoint(p.name, 2);

            int beri = Util.random(1_000_000, 5_000_000);
            int ruby = Util.random(20, 50);
            p.update_vang(beri);
            p.update_ngoc(ruby);

            String rewardName = "";
            int rand = Util.random(5);
            switch (rand) {
                case 0: {
                    // Đá khảm 4-5
                    int[] gemIds = { 53, 54, 65, 66, 71, 72 };
                    int gem = gemIds[Util.random(gemIds.length)];
                    p.item.add_item_bag47(4, gem, 1);
                    ItemTemplate4 it = ItemTemplate4.get_it_by_id(gem);
                    rewardName = (it != null) ? it.name : "Đá khảm";
                    break;
                }
                case 1: {
                    int gemHaiThach = Util.random(223, 224);
                    p.item.add_item_bag47(4, gemHaiThach, 1); // Đá Hải Thạch 3-4
                    ItemTemplate4 it = ItemTemplate4.get_it_by_id(gemHaiThach);
                    rewardName = (it != null) ? it.name : "Đá Hải Thạch";
                    break;
                }
                case 2:
                    p.item.add_item_bag47(4, 271, 1); // Thẻ đổi tên
                    rewardName = "1x Thẻ đổi tên";
                    break;
                case 3:
                    p.item.add_item_bag47(4, 413, 2); // Tiến cấp đơn
                    rewardName = "2x Tiến cấp đơn";
                    break;
                case 4:
                    p.item.add_item_bag47(4, 414, 2); // Kỹ năng đơn
                    rewardName = "2x Kỹ năng đơn";
                    break;
            }

            p.item.update_Inventory(-1, false);
            int currentPoint = getInstance().getPoint(p.name);
            Service.send_box_ThongBao_OK(p, String.format("🎅 Mở Túi Giáng Sinh nhận:\n+%,d Beri\n+%d Ruby\n+%s\n⭐ +2 Điểm BXH Noel (Hiện có: %d điểm)",
                    beri, ruby, rewardName, currentPoint));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Mở Hộp Quà Giáng Sinh VIP (492) - Tích +5 Điểm BXH
     */
    public static void openHopQuaGiangSinhVIP(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_HOP_QUA_GIANG_SINH_VIP) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Hộp Quà Giáng Sinh VIP!");
                return;
            }

            p.item.remove_item47(4, ITEM_HOP_QUA_GIANG_SINH_VIP, 1);
            getInstance().addPoint(p.name, 5);

            int beri = Util.random(10_000_000, 50_000_000);
            int ruby = Util.random(200, 1000);
            p.update_vang(beri);
            p.update_ngoc(ruby);

            String rewardName = "";
            int rand = Util.random(7);
            switch (rand) {
                case 0:
                    p.item.add_item_bag47(4, 588, 1); // Đá khảm cấp 6 tự chọn
                    rewardName = "1x Đá khảm cấp 6 tự chọn";
                    break;
                case 1:
                    p.item.add_item_bag47(4, ITEM_DA_KHAM_VO_CUC_S, 1); // Đá Vô Cực S
                    rewardName = "1x Đá khảm Vô Cực S";
                    break;
                case 2: {
                    int gemHaiThach = Util.random(225, 226);
                    p.item.add_item_bag47(4, gemHaiThach, 1); // Đá Hải Thạch 5-6
                    ItemTemplate4 it = ItemTemplate4.get_it_by_id(gemHaiThach);
                    rewardName = (it != null) ? it.name : "Đá Hải Thạch";
                    break;
                }
                case 3:
                    p.item.add_item_bag47(4, 551, 1); // Bảo hiểm chuyển hóa cao
                    rewardName = "1x Bảo hiểm chuyển hóa cao";
                    break;
                case 4:
                    p.item.add_item_bag47(4, ITEM_RUONG_AC_QUY, 1); // Rương ác quỷ
                    rewardName = "1x Rương ác quỷ";
                    break;
                case 5:
                    p.item.add_item_bag47(4, ITEM_RUONG_DAI_AC_QUY, 1); // Rương đại ác quỷ
                    rewardName = "1x Rương đại ác quỷ";
                    break;
                case 6:
                    p.item.add_item_bag47(4, ITEM_RUONG_TT_NOEL, 1); // Rương trang phục Noel
                    rewardName = "1x Rương trang phục Noel";
                    break;
            }

            p.item.update_Inventory(-1, false);
            int currentPoint = getInstance().getPoint(p.name);
            Service.send_box_ThongBao_OK(p, String.format("👑 Mở Hộp Quà Giáng Sinh VIP nhận:\n+%,d Beri\n+%d Ruby\n+%s\n⭐ +5 Điểm BXH Noel (Hiện có: %d điểm)",
                    beri, ruby, rewardName, currentPoint));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Mở Rương Pet Noel (1011)
     */
    public static void openRuongPetNoel(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_RUONG_PET_NOEL) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Rương Pet Noel!");
                return;
            }

            p.item.remove_item47(4, ITEM_RUONG_PET_NOEL, 1);

            int[] petItemIds = { ITEM_PET_LOC_NOEL, ITEM_PET_TUYET_NOEL, ITEM_PET_TUAN_LOC };
            int selectedPet = petItemIds[Util.random(petItemIds.length)];
            p.item.add_item_bag47(4, selectedPet, 1);
            p.item.update_Inventory(-1, false);
            ItemTemplate4 it = ItemTemplate4.get_it_by_id(selectedPet);
            Service.send_box_ThongBao_OK(p, "🦌 Chúc mừng bạn đã mở được Thú Cưng Noel: " + (it != null ? it.name : "Pet Noel") + "!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Mở Rương Trang Phục Noel (622)
     */
    public static void openRuongTrangPhucNoel(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_RUONG_TT_NOEL) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Rương Trang Phục Noel!");
                return;
            }

            int[] fashions = { FASHION_HOANG_TU_TUYET, FASHION_CONG_CHUA_TUYET, FASHION_NOEL_NU, FASHION_BE_TUYET };
            int chosenFashionId = fashions[Util.random(fashions.length)];
            
            ItemFashionP2 newFashion = new ItemFashionP2();
            newFashion.id = (short) chosenFashionId;
            newFashion.is_use = false;
            newFashion.level = 0;
            p.fashion.add(newFashion);

            p.item.remove_item47(4, ITEM_RUONG_TT_NOEL, 1);
            p.item.update_Inventory(-1, false);

            Service.send_box_ThongBao_OK(p, "👘 Chúc mừng bạn đã nhận được Trang Phục Giáng Sinh (Vĩnh viễn)!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Sử dụng Kẹo Giáng Sinh (489)
     */
    public static void useKeoNoel(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_KEO_NOEL) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Kẹo Giáng Sinh!");
                return;
            }

            p.hp = p.body.get_hp_max(true);
            p.mp = p.body.get_mp_max(true);
            Service.use_potion(p, 0, p.hp);
            p.item.remove_item47(4, ITEM_KEO_NOEL, 1);
            p.item.update_Inventory(-1, false);
            Service.send_box_ThongBao_OK(p, "🍬 Ăn Kẹo Giáng Sinh: Hồi phục 100% HP & MP!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Sử dụng Bóng Tuyết (611)
     */
    public static void useBongTuyet(Player p) {
        try {
            if (p.item.total_item_bag_by_id(4, ITEM_BONG_TUYET) <= 0) {
                Service.send_box_ThongBao_OK(p, "Bạn không có Bóng Tuyết!");
                return;
            }

            p.item.remove_item47(4, ITEM_BONG_TUYET, 1);
            p.update_vang(50_000);
            p.item.update_Inventory(-1, false);
            Service.send_eff(p, 23, 0); // snowball / festival effect
            Service.send_box_ThongBao_OK(p, "❄️ Bạn đã ném Quả Bóng Tuyết và nhận được +50.000 Beri!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    // ================== BẢNG XẾP HẠNG & ĐUA TOP ==================

    public void addPoint(String name, int point) {
        pointMap.merge(name, point, Integer::sum);
        saveData();
    }

    public int getPoint(String name) {
        return pointMap.getOrDefault(name, 0);
    }

    public List<Map.Entry<String, Integer>> getTopLeaderboard(int limit) {
        List<Map.Entry<String, Integer>> list = new ArrayList<>(pointMap.entrySet());
        list.sort((a, b) -> b.getValue().compareTo(a.getValue()));
        if (list.size() > limit) {
            return list.subList(0, limit);
        }
        return list;
    }

    public void showLeaderboard(Player p) throws IOException {
        List<Map.Entry<String, Integer>> top = getTopLeaderboard(10);
        StringBuilder sb = new StringBuilder();
        sb.append("🎄 BẢNG XẾP HẠNG VUA GIÁNG SINH 🎄\n\n");

        if (top.isEmpty()) {
            sb.append("Hiện chưa có người chơi nào tích điểm Giáng Sinh.\n");
        } else {
            int rank = 1;
            for (Map.Entry<String, Integer> entry : top) {
                String medal = rank == 1 ? "🥇" : rank == 2 ? "🥈" : rank == 3 ? "🥉" : "⭐";
                sb.append(String.format("%s TOP %d: %s - %,d Điểm\n", medal, rank, entry.getKey(), entry.getValue()));
                rank++;
            }
        }

        int myPoint = getPoint(p.name);
        int myRank = getPlayerRank(p.name);
        sb.append("\n────────────────────\n");
        sb.append(String.format("👤 Điểm của bạn: %,d Điểm (Hạng: %s)\n", myPoint, myRank > 0 ? "TOP " + myRank : "Chưa có hạng"));

        Message m = new Message(-50);
        m.writer().writeByte(1);
        m.writer().writeByte(0);
        m.writer().writeUTF(sb.toString());
        m.writer().writeByte(0);
        p.conn.addmsg(m);
        m.cleanup();
    }

    public int getPlayerRank(String name) {
        List<Map.Entry<String, Integer>> list = new ArrayList<>(pointMap.entrySet());
        list.sort((a, b) -> b.getValue().compareTo(a.getValue()));
        for (int i = 0; i < list.size(); i++) {
            if (list.get(i).getKey().equals(name)) {
                return i + 1;
            }
        }
        return -1;
    }

    // ================== LƯU & TẢI DỮ LIỆU JSON ==================
    @SuppressWarnings("unchecked")
    private synchronized void saveData() {
        try {
            JSONObject root = new JSONObject();
            JSONObject pointsObj = new JSONObject();
            for (Map.Entry<String, Integer> entry : pointMap.entrySet()) {
                pointsObj.put(entry.getKey(), entry.getValue());
            }
            root.put("points", pointsObj);

            try (FileWriter writer = new FileWriter(DATA_FILE)) {
                writer.write(root.toJSONString());
                writer.flush();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private synchronized void loadData() {
        File file = new File(DATA_FILE);
        if (!file.exists()) {
            return;
        }
        try (FileReader reader = new FileReader(file)) {
            JSONParser parser = new JSONParser();
            JSONObject root = (JSONObject) parser.parse(reader);

            pointMap.clear();

            JSONObject pointsObj = (JSONObject) root.get("points");
            if (pointsObj != null) {
                for (Object key : pointsObj.keySet()) {
                    String name = (String) key;
                    long point = (long) pointsObj.get(name);
                    pointMap.put(name, (int) point);
                }
            }
            System.out.println("[EventNoel] Đã nạp thành công dữ liệu BXH: " + pointMap.size() + " người chơi.");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void cleanup() {
        despawnBoss("Sự kiện Noel đã kết thúc.");
        saveData();
    }

    public static void broadcastMessage(String msg) {
        try {
            Manager.gI().chatKTG(0, msg, 5);
        } catch (IOException e) {
        }
    }

    public static void broadcastWorldChat(String msg) {
        try {
            Manager.gI().chatKTG(0, msg, 5);
        } catch (IOException e) {
        }
    }
}
