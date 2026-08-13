package event;

import java.io.IOException;
import java.util.ArrayList;
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
import template.InfoMemList;
import template.ItemMap;
import map.Map;
import map.Mob;
import template.GiftBox;
import template.ItemFashionP2;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.Top_Dame;

/**
 * Sự kiện Trung Thu: Đêm Rằm Hải Tặc
 * 
 * Chỉ sử dụng item4 có sẵn trong database, không ảnh hưởng code gốc.
 */
public class EventTrungThu implements Runnable {

    // ================== CẤU HÌNH SỰ KIỆN ==================
    public static boolean IS_OPEN = false;
    private static final String CONFIG_KEY = "event-trung-thu";

    // Item IDs - Nguyên liệu
    public static final int ITEM_BOT_MI = 202; // Bột Mì
    public static final int ITEM_DUONG = 200; // Đường
    public static final int ITEM_TRUNG_MUOI = 203; // Trứng Muối
    public static final int ITEM_DEN_ONG_SAO = 473; // Đèn ông sao
    public static final int ITEM_GIAY_GOI_QUA = 575; // Giấy gói quà

    // Item IDs - Thành phẩm
    public static final int ITEM_BANH_TRUNG_THU = 207;
    public static final int ITEM_BANH_DAU_XANH = 208;
    public static final int ITEM_BANH_TRUNG_MUOI = 209;
    public static final int ITEM_BANH_HAT_SEN = 210;
    public static final int ITEM_DEN_KEO_QUAN = 410;
    public static final int ITEM_HOP_BANH = 211;
    public static final int ITEM_HOP_BANH_THUONG_HANG = 576;
    public static final int ITEM_THE_TT_TRUNG_THU = 475;

    // Fashion IDs
    public static final int FASHION_CHU_CUOI = 65;
    public static final int FASHION_CHI_HANG = 66;

    // Boss ID
    public static final int MOB_BOSS_LAN = 153;

    // Thời gian spawn boss (giờ trong ngày)
    private static final int[] BOSS_SPAWN_HOURS = { 12, 18, 20, 22 };
    private static final long BOSS_LIFETIME_MS = 30 * 60 * 1000L; // 30 phút
    private static final long BOSS_ANNOUNCE_BEFORE_MS = 5 * 60 * 1000L; // 5 phút trước

    // ================== TRẠNG THÁI RUNTIME ==================
    private static EventTrungThu instance;
    private final Thread eventThread;
    private volatile boolean running = true;

    // Boss Lân
    private Boss activeBossLan;
    private long nextBossSpawnTime = 0;
    private long bossSpawnTime = 0;
    private boolean bossAlive = false;
    private final List<Top_Dame> bossDamageList = new CopyOnWriteArrayList<>();
    private Player lastHitPlayer = null;
    private final List<Player> participatedPlayers = new CopyOnWriteArrayList<>();

    // Scheduler cho thông báo
    private final ScheduledExecutorService scheduler = Executors.newScheduledThreadPool(2, r -> {
        Thread t = new Thread(r, "EventTrungThu-Scheduler");
        t.setDaemon(true);
        return t;
    });

    // ================== KHỞI TẠO ==================
    private EventTrungThu() {
        this.eventThread = new Thread(this, "EventTrungThu-Main");
        this.eventThread.start();
    }

    public static EventTrungThu getInstance() {
        if (instance == null) {
            synchronized (EventTrungThu.class) {
                if (instance == null) {
                    instance = new EventTrungThu();
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
            broadcastMessage("Sự kiện Trung Thu: Đêm Rằm Hải Tặc đã được kích hoạt!");
        } else {
            broadcastMessage("Sự kiện Trung Thu đã kết thúc!");
        }
    }

    public static void loadConfig(java.util.Properties config) {
        String value = config.getProperty(CONFIG_KEY);
        if (value != null) {
            IS_OPEN = Boolean.parseBoolean(value);
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
                System.out.println("EventTrungThu error: " + e.getMessage());
            }
        }
    }

    private boolean isWithinBossActiveTime(long now) {
        java.util.Calendar cal = java.util.Calendar.getInstance();
        cal.setTimeInMillis(now);
        int hour = cal.get(java.util.Calendar.HOUR_OF_DAY);
        if (hour >= 11 && hour < 13)
            return true;
        if (hour >= 20 && hour < 21)
            return true;
        return false;
    }

    private void scheduleNextBossSpawn() {
        java.util.Calendar cal = java.util.Calendar.getInstance();
        int hour = cal.get(java.util.Calendar.HOUR_OF_DAY);
        int minute = cal.get(java.util.Calendar.MINUTE);

        // Tìm giờ spawn tiếp theo
        int nextHour = -1;
        for (int spawnHour : BOSS_SPAWN_HOURS) {
            if (hour < spawnHour || (hour == spawnHour && minute < 30)) {
                nextHour = spawnHour;
                break;
            }
        }

        if (nextHour == -1) {
            // Ngày mai
            cal.add(java.util.Calendar.DAY_OF_MONTH, 1);
            cal.set(java.util.Calendar.HOUR_OF_DAY, BOSS_SPAWN_HOURS[0]);
            cal.set(java.util.Calendar.MINUTE, 30);
            cal.set(java.util.Calendar.SECOND, 0);
        } else {
            // Hôm nay
            cal.set(java.util.Calendar.HOUR_OF_DAY, nextHour);
            cal.set(java.util.Calendar.MINUTE, 30);
            cal.set(java.util.Calendar.SECOND, 0);
        }

        nextBossSpawnTime = cal.getTimeInMillis();
    }

    private synchronized void update() {
        long now = System.currentTimeMillis();
        boolean inWindow = isWithinBossActiveTime(now);

        if (inWindow) {
            if (!bossAlive && now >= nextBossSpawnTime) {
                // Tạm thời comment gọi boss tự động để test lệnh gọi Lân
                // spawnBossLan();
            }
        }

        if (bossAlive && activeBossLan != null && now >= bossSpawnTime + BOSS_LIFETIME_MS) {
            despawnBoss("Boss Lân Sư Tử đã bỏ đi!");
        }
    }

    private static Boss persistentBossLan = null;

    private void spawnBossLan() {
        if (bossAlive)
            return;

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

        if (persistentBossLan == null) {
            persistentBossLan = new Boss();
            persistentBossLan.id = 9999;
            persistentBossLan.thegioi = 1;
            persistentBossLan.mob = new Mob();
            persistentBossLan.mob.mob_template = template.MobTemplate.ENTRYS.get(MOB_BOSS_LAN);
            if (persistentBossLan.mob.mob_template == null) {
                System.out.println("Lỗi: Không tìm thấy MobTemplate 153 cho Boss Lân");
                nextBossSpawnTime = System.currentTimeMillis() + 60_000L;
                return;
            }
            persistentBossLan.mob.hp_max = persistentBossLan.mob.mob_template.hp_max;
            persistentBossLan.hp_max_origin = persistentBossLan.mob.mob_template.hp_max;
            persistentBossLan.mob.boss_info = persistentBossLan;
            persistentBossLan.TopDame = new ArrayList<>();
            persistentBossLan.skill = new short[] { 1 };
            persistentBossLan.buff = new ArrayList<>();
            persistentBossLan.time_atk = new long[] { 0, 0, 0, 0, 0 };
            int currentIndex = core.Manager.gI().getIndexMob();
            persistentBossLan.mob.index = currentIndex;
            persistentBossLan.index_mob_save = currentIndex;
            core.Manager.gI().setIndexMob(currentIndex + 10);
            for (int j = 0; j < 10; j++) {
                Mob.ENTRYS.put((persistentBossLan.mob.index + j), persistentBossLan.mob);
            }
            // Thêm vào Boss.ENTRYS để người chơi có thể thấy boss khi vào map
            if (Boss.ENTRYS != null && !Boss.ENTRYS.contains(persistentBossLan)) {
                Boss.ENTRYS.add(persistentBossLan);
            }
        }

        persistentBossLan.mob.map = targetMap;
        persistentBossLan.mapOrigin = targetMap;
        persistentBossLan.mob.x = (short) (targetMap.template.maxW / 2);
        persistentBossLan.mob.y = 200;
        persistentBossLan.mob.isdie = false;
        persistentBossLan.mob.hp = persistentBossLan.mob.hp_max;
        persistentBossLan.mob.id_target = -1;
        persistentBossLan.levelBoss = 1;
        persistentBossLan.updateHpForLevel();
        persistentBossLan.TopDame.clear();

        activeBossLan = persistentBossLan;
        bossAlive = true;
        bossSpawnTime = System.currentTimeMillis();
        lastHitPlayer = null;
        participatedPlayers.clear();
        bossDamageList.clear();

        try {
            Manager.gI().chatKTG(0,
                    "🦁 Boss Lân Sư Tử đã xuất hiện tại " + targetMap.template.name + " khu "
                            + (targetMap.zone_id + 1) + "! Hãy nhanh tay săn lượng!",
                    5);

            Message m_local = new Message(1);
            m_local.writer().writeByte(1);
            m_local.writer().writeShort(persistentBossLan.mob.index);
            m_local.writer().writeShort(persistentBossLan.mob.x);
            m_local.writer().writeShort(persistentBossLan.mob.y);
            for (int j = 0; j < targetMap.players.size(); j++) {
                Player p0 = targetMap.players.get(j);
                if (p0 != null && p0.conn != null) {
                    p0.conn.addmsg(m_local);
                }
            }
            m_local.cleanup();
        } catch (IOException e) {
            System.out.println("Error announcing boss spawn: " + e.getMessage());
        }
    }

    public void forceSpawnBossLan(Player p) {
        if (bossAlive) {
            despawnBoss("Boss Lân cũ đã bị giải tán để gọi Boss mới!");
        }
        spawnBossLan();
        if (p != null) {
            try {
                core.Service.send_box_ThongBao_OK(p, "Đã gọi Lân thành công!");
            } catch (Exception e) {
            }
        }
    }

    private boolean isVillageMap(int mapId) {
        return mapId == 0 || mapId == 21 || mapId == 22;
    }

    public void onBossDamaged(Player player, int damage) {
        if (!bossAlive || activeBossLan == null)
            return;

        // Ghi nhận damage
        Top_Dame entry = new Top_Dame();
        entry.name = player.name;
        entry.dame = damage;
        entry.id = player.id;

        // Cập nhật hoặc thêm mới
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

        // Ghi nhận tham gia
        if (!participatedPlayers.contains(player)) {
            participatedPlayers.add(player);
        }
    }

    public void onBossKilled(Player killer) {
        if (!bossAlive || activeBossLan == null)
            return;

        lastHitPlayer = killer;

        // Tăng số lần giết Lân
        if (killer != null) {
            killer.lanKills++;
        }

        // Thông báo
        try {
            Manager.gI().chatKTG(0,
                    "🎉 Chúc mừng " + killer.name + " đã hạ gục Boss Lân Sư Tử! (Lần thứ " + killer.lanKills + ")",
                    5);
        } catch (IOException e) {
            System.out.println("Error announcing boss kill: " + e.getMessage());
        }

        // Phần thưởng Last Hit
        giveLastHitReward(killer);

        // Phần thưởng tham gia (Đã bỏ theo yêu cầu)
        // giveParticipationRewards();

        // Rơi item nhặt lộc (Đã bỏ theo yêu cầu)
        // spawnLuckyDrops();

        // Reset
        despawnBoss(null);
    }

    private GiftBox createGiftBox(int id, int num) {
        GiftBox gb = new GiftBox();
        gb.type = 4;
        gb.id = (short) id;
        gb.num = num;
        gb.color = 0;
        template.ItemTemplate4 it = template.ItemTemplate4.get_it_by_id(id);
        if (it != null) {
            gb.name = it.name;
            gb.icon = it.icon;
            if (id == 1 || id == ITEM_HOP_BANH_THUONG_HANG) {
                gb.color = 5;
            }
        } else {
            gb.name = "Vật phẩm " + id;
            gb.icon = 0;
        }
        return gb;
    }

    private void giveLastHitReward(Player player) {
        if (player == null)
            return;

        List<GiftBox> rewards = new ArrayList<>();

        // Tỉ lệ 30% ra Hộp Bánh Thượng Hạng
        if (core.Util.random(100) < 30) {
            rewards.add(createGiftBox(ITEM_HOP_BANH_THUONG_HANG, 1));
        }

        rewards.add(createGiftBox(1, 50)); // Ruby
        rewards.add(createGiftBox(ITEM_GIAY_GOI_QUA, 2));
        rewards.add(createGiftBox(ITEM_BOT_MI, 1));
        rewards.add(createGiftBox(ITEM_DUONG, 1));
        rewards.add(createGiftBox(ITEM_TRUNG_MUOI, 1));

        try {
            Service.send_gift(player, 0, "Phần thưởng Đòn Kết Liễu Boss Lân!", "", rewards, true);
        } catch (IOException e) {
            System.out.println("Error giving last hit reward: " + e.getMessage());
        }
    }

    private void giveParticipationRewards() {
        List<GiftBox> baseRewards = new ArrayList<>();
        baseRewards.add(createGiftBox(0, 100000)); // Beri
        baseRewards.add(createGiftBox(ITEM_BOT_MI, 5));
        baseRewards.add(createGiftBox(ITEM_DUONG, 5));
        baseRewards.add(createGiftBox(ITEM_TRUNG_MUOI, 2));
        baseRewards.add(createGiftBox(ITEM_GIAY_GOI_QUA, 1));

        for (Player p : participatedPlayers) {
            try {
                Service.send_gift(p, 0, "Phần thưởng tham gia Boss Lân!", "", baseRewards, true);
            } catch (IOException e) {
                System.out.println("Error giving participation reward: " + e.getMessage());
            }
        }
    }

    private void spawnLuckyDrops() {
        if (activeBossLan == null || activeBossLan.mob == null)
            return;

        Map map = activeBossLan.mob.map;
        if (map == null)
            return;

        int dropCount = Util.random(30, 50);
        ItemTemplate4[] possibleItems = new ItemTemplate4[] {
                ItemTemplate4.get_it_by_id(0), // Beri
                ItemTemplate4.get_it_by_id(ITEM_BANH_TRUNG_THU),
                ItemTemplate4.get_it_by_id(ITEM_BOT_MI),
                ItemTemplate4.get_it_by_id(ITEM_DUONG),
                ItemTemplate4.get_it_by_id(ITEM_BANH_DAU_XANH)
        };

        for (int i = 0; i < dropCount; i++) {
            try {
                ItemMap item = new ItemMap();
                int randType = Util.random(5);

                if (randType == 0) {
                    item.id = 0;
                    item.quant = Util.random(50000, 100000);
                    item.name = item.quant + " beri";
                } else {
                    ItemTemplate4 template = possibleItems[randType];
                    if (template != null) {
                        item.id = template.id;
                        item.quant = 1;
                        item.name = template.name;
                        item.icon = template.icon;
                    } else {
                        continue;
                    }
                }

                item.category = 4;
                item.color = 0;
                item.id_master = -1;
                item.time_exist = System.currentTimeMillis() + 120_000L;
                item.index = (short) map.get_index_item_map();

                if (item.index > -1) {
                    map.list_it_map[item.index] = item;

                    List<ItemMap> listShow = new ArrayList<>();
                    listShow.add(item);
                    map.send_msg_all_p(createDropMessage(listShow, activeBossLan.mob), null, true);
                }
            } catch (Exception e) {
                // Bỏ qua lỗi drop
            }
        }
    }

    private Message createDropMessage(List<ItemMap> items, Mob mob) throws IOException {
        Message m = new Message(11);
        m.writer().writeByte(items.size());
        for (ItemMap itm : items) {
            m.writer().writeShort(itm.index);
            m.writer().writeByte(itm.category);
            m.writer().writeShort(itm.icon);
            m.writer().writeByte(itm.color);
            m.writer().writeUTF(itm.name);
            m.writer().writeShort(mob != null ? mob.index : -1);
            m.writer().writeByte(1);
            m.writer().writeShort(-1);
        }
        return m;
    }

    private void despawnBoss(String message) {
        if (activeBossLan != null && activeBossLan.mob != null) {
            try {
                activeBossLan.mob.isdie = true;
                activeBossLan.mob.hp = 0;
                if (activeBossLan.mob.map != null) {
                    activeBossLan.mob.map.remove_obj(activeBossLan.mob.index, 1);
                }
            } catch (IOException e) {
                System.out.println("Error despawning boss: " + e.getMessage());
            }
        }

        bossAlive = false;
        activeBossLan = null;
        // 10 phút sau hồi sinh
        nextBossSpawnTime = System.currentTimeMillis() + 10 * 60 * 1000L;

        if (message != null) {
            try {
                Manager.gI().chatKTG(0, message, 5);
            } catch (IOException e) {
                System.out.println("Error sending despawn message: " + e.getMessage());
            }
        }
    }

    // ================== XỬ LÝ ITEM ==================

    /**
     * Xử lý mở Bánh Trung Thu (207-210)
     */
    public static void openBanh(Player p, int itemId) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Trung Thu chưa được kích hoạt!");
            return;
        }

        List<GiftBox> rewards = new ArrayList<>();

        // Random rewards dựa trên loại bánh
        switch (itemId) {
            case ITEM_BANH_TRUNG_THU: {
                int rand = Util.random(4);
                switch (rand) {
                    case 0: {
                        int gemId = Util.random(44, 79);
                        ItemTemplate4 gemTemplate = ItemTemplate4.get_it_by_id(gemId);
                        GiftBox gem = new GiftBox();
                        gem.type = 4;
                        gem.id = (short) gemId;
                        gem.num = 1;
                        gem.color = 0;
                        if (gemTemplate != null) {
                            gem.name = gemTemplate.name;
                            gem.icon = gemTemplate.icon;
                        }
                        rewards.add(gem);
                        break;
                    }
                    case 1: {
                        ItemTemplate4 beriTemplate = ItemTemplate4.get_it_by_id(0);
                        GiftBox beri = new GiftBox();
                        beri.type = 4;
                        beri.id = 0;
                        beri.num = Util.random(50000, 150000);
                        beri.color = 0;
                        if (beriTemplate != null) {
                            beri.name = beriTemplate.name;
                            beri.icon = beriTemplate.icon;
                        }
                        rewards.add(beri);
                        break;
                    }
                    case 2: {
                        ItemTemplate4 rubyTemplate = ItemTemplate4.get_it_by_id(1);
                        GiftBox ruby = new GiftBox();
                        ruby.type = 4;
                        ruby.name = "ruby";
                        ruby.num = Util.random(50, 150);
                        ruby.id = 1;
                        ruby.color = 5;
                        if (rubyTemplate != null) {
                            ruby.icon = rubyTemplate.icon;
                        }
                        rewards.add(ruby);
                        break;
                    }
                    case 3: {
                        int bonusId = Util.random(2) == 0 ? 0 : 1; // 0: Đá ngũ sắc, 1: Bột cường hóa
                        int amount = bonusId == 0 ? 500 : 50;
                        ItemTemplate7 bonusTemplate = ItemTemplate7.get_it_by_id(bonusId);
                        GiftBox bonus = new GiftBox();
                        bonus.type = 7;
                        bonus.id = (short) bonusId;
                        bonus.num = amount;
                        bonus.color = 0;
                        if (bonusTemplate != null) {
                            bonus.name = bonusTemplate.name;
                            bonus.icon = bonusTemplate.icon;
                        }
                        rewards.add(bonus);
                        break;
                    }
                }
                break;
            }
            case ITEM_BANH_DAU_XANH: {
                int rand = Util.random(3);
                switch (rand) {
                    case 0: {
                        int gemId = Util.random(44, 79);
                        ItemTemplate4 gemTemplate = ItemTemplate4.get_it_by_id(gemId);
                        GiftBox gem = new GiftBox();
                        gem.type = 4;
                        gem.id = (short) gemId;
                        gem.num = 1;
                        gem.color = 0;
                        if (gemTemplate != null) {
                            gem.name = gemTemplate.name;
                            gem.icon = gemTemplate.icon;
                        }
                        rewards.add(gem);
                        break;
                    }
                    case 1: {
                        ItemTemplate4 beriTemplate = ItemTemplate4.get_it_by_id(0);
                        GiftBox beri = new GiftBox();
                        beri.type = 4;
                        beri.id = 0;
                        beri.num = Util.random(100000, 500000);
                        beri.color = 0;
                        if (beriTemplate != null) {
                            beri.name = beriTemplate.name;
                            beri.icon = beriTemplate.icon;
                        }
                        rewards.add(beri);
                        break;
                    }
                    case 2: {
                        ItemTemplate4 rubyTemplate = ItemTemplate4.get_it_by_id(1);
                        GiftBox ruby = new GiftBox();
                        ruby.type = 4;
                        ruby.name = "ruby";
                        ruby.num = Util.random(10, 50);
                        ruby.id = 1;
                        ruby.color = 5;
                        if (rubyTemplate != null) {
                            ruby.icon = rubyTemplate.icon;
                        }
                        rewards.add(ruby);
                        break;
                    }
                }
                break;
            }
            case ITEM_BANH_TRUNG_MUOI: {
                int rand = Util.random(4);
                switch (rand) {
                    case 0: {
                        int gemId = Util.random(44, 79);
                        ItemTemplate4 gemTemplate = ItemTemplate4.get_it_by_id(gemId);
                        GiftBox gem = new GiftBox();
                        gem.type = 4;
                        gem.id = (short) gemId;
                        gem.num = 1;
                        gem.color = 0;
                        if (gemTemplate != null) {
                            gem.name = gemTemplate.name;
                            gem.icon = gemTemplate.icon;
                        }
                        rewards.add(gem);
                        break;
                    }
                    case 1: {
                        ItemTemplate4 beriTemplate = ItemTemplate4.get_it_by_id(0);
                        GiftBox beri = new GiftBox();
                        beri.type = 4;
                        beri.id = 0;
                        beri.num = Util.random(100000, 500000);
                        beri.color = 0;
                        if (beriTemplate != null) {
                            beri.name = beriTemplate.name;
                            beri.icon = beriTemplate.icon;
                        }
                        rewards.add(beri);
                        break;
                    }
                    case 2: {
                        ItemTemplate4 rubyTemplate = ItemTemplate4.get_it_by_id(1);
                        GiftBox ruby = new GiftBox();
                        ruby.type = 4;
                        ruby.name = "ruby";
                        ruby.num = Util.random(10, 50);
                        ruby.id = 1;
                        ruby.color = 5;
                        if (rubyTemplate != null) {
                            ruby.icon = rubyTemplate.icon;
                        }
                        rewards.add(ruby);
                        break;
                    }
                    case 3: {
                        ItemTemplate7 acQuyTemplate = ItemTemplate7.get_it_by_id(9);
                        GiftBox acQuy = new GiftBox();
                        acQuy.type = 7;
                        acQuy.id = 9;
                        acQuy.num = Util.random(2, 5);
                        acQuy.color = 0;
                        if (acQuyTemplate != null) {
                            acQuy.name = acQuyTemplate.name;
                            acQuy.icon = acQuyTemplate.icon;
                        }
                        rewards.add(acQuy);
                        break;
                    }
                }
                break;
            }
            case ITEM_BANH_HAT_SEN: {
                int rand = Util.random(4);
                switch (rand) {
                    case 0: {
                        int gemId = Util.random(44, 79);
                        ItemTemplate4 gemTemplate = ItemTemplate4.get_it_by_id(gemId);
                        GiftBox gem = new GiftBox();
                        gem.type = 4;
                        gem.id = (short) gemId;
                        gem.num = 1;
                        gem.color = 0;
                        if (gemTemplate != null) {
                            gem.name = gemTemplate.name;
                            gem.icon = gemTemplate.icon;
                        }
                        rewards.add(gem);
                        break;
                    }
                    case 1: {
                        ItemTemplate4 beriTemplate = ItemTemplate4.get_it_by_id(0);
                        GiftBox beri = new GiftBox();
                        beri.type = 4;
                        beri.id = 0;
                        beri.num = Util.random(100000, 500000);
                        beri.color = 0;
                        if (beriTemplate != null) {
                            beri.name = beriTemplate.name;
                            beri.icon = beriTemplate.icon;
                        }
                        rewards.add(beri);
                        break;
                    }
                    case 2: {
                        ItemTemplate4 rubyTemplate = ItemTemplate4.get_it_by_id(1);
                        GiftBox ruby = new GiftBox();
                        ruby.type = 4;
                        ruby.name = "ruby";
                        ruby.num = Util.random(10, 50);
                        ruby.id = 1;
                        ruby.color = 5;
                        if (rubyTemplate != null) {
                            ruby.icon = rubyTemplate.icon;
                        }
                        rewards.add(ruby);
                        break;
                    }
                    case 3: {
                        // Bột Vàng (id=1) hoặc Mai Rùa (id=2)
                        int bonusId = Util.random(2) == 0 ? 1 : 2;
                        ItemTemplate7 bonusTemplate = ItemTemplate7.get_it_by_id(bonusId);
                        GiftBox bonus = new GiftBox();
                        bonus.type = 7;
                        bonus.id = (short) bonusId;
                        bonus.num = Util.random(2, 5);
                        bonus.color = 0;
                        if (bonusTemplate != null) {
                            bonus.name = bonusTemplate.name;
                            bonus.icon = bonusTemplate.icon;
                        }
                        rewards.add(bonus);
                        break;
                    }
                }
                break;
            }
        }

        Service.send_gift(p, 0, "Quà trung thu:", "", rewards, true);
    }

    /**
     * Xử lý Đèn Kéo Quân (410)
     */
    public static void useDenKeoQuan(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Trung Thu chưa được kích hoạt!");
            return;
        }

        List<GiftBox> rewards = new ArrayList<>();

        // Random 1 trong các phần thưởng
        int rand = Util.random(5);
        switch (rand) {
            case 0: {
                // Rương cam theo cấp
                int level = p.level;
                if (level < 10)
                    level = 10;
                if (level > 90)
                    level = 90;
                int chestIdNormal = 111 + level / 10;
                ItemTemplate4 it_rcam = ItemTemplate4.get_it_by_id(chestIdNormal);
                if (it_rcam != null) {
                    GiftBox giftChest = new GiftBox();
                    giftChest.id = (short) chestIdNormal;
                    giftChest.type = 4;
                    giftChest.name = it_rcam.name;
                    giftChest.icon = it_rcam.icon;
                    giftChest.num = 1;
                    giftChest.color = 0;
                    rewards.add(giftChest);
                }
                break;
            }
            case 1: {
                // Mai rùa (id 6 item7)
                ItemTemplate7 template7 = ItemTemplate7.get_it_by_id(6);
                GiftBox mairua = new GiftBox();
                mairua.type = 7;
                mairua.id = 6;
                mairua.num = Util.random(1, 3);
                mairua.color = 0;
                if (template7 != null) {
                    mairua.name = template7.name;
                    mairua.icon = template7.icon;
                }
                rewards.add(mairua);
                break;
            }
            case 2: {
                // Xp chiêu thức 159 item4
                ItemTemplate4 template4 = ItemTemplate4.get_it_by_id(159);
                GiftBox xpSkill = new GiftBox();
                xpSkill.type = 4;
                xpSkill.id = 159;
                xpSkill.num = 1;
                xpSkill.color = 0;
                if (template4 != null) {
                    xpSkill.name = template4.name;
                    xpSkill.icon = template4.icon;
                }
                rewards.add(xpSkill);
                break;
            }
            case 3: {
                // Đá ác quỷ: id 134 item4
                ItemTemplate4 template4 = ItemTemplate4.get_it_by_id(134);
                GiftBox daquy = new GiftBox();
                daquy.type = 4;
                daquy.id = 134;
                daquy.num = 1;
                daquy.color = 0;
                if (template4 != null) {
                    daquy.name = template4.name;
                    daquy.icon = template4.icon;
                }
                rewards.add(daquy);
                break;
            }
            case 4: {
                // Rương đại ác quỷ: id 158 item4
                ItemTemplate4 template4 = ItemTemplate4.get_it_by_id(158);
                GiftBox ruongDa = new GiftBox();
                ruongDa.type = 4;
                ruongDa.id = 158;
                ruongDa.num = 1;
                ruongDa.color = 0;
                if (template4 != null) {
                    ruongDa.name = template4.name;
                    ruongDa.icon = template4.icon;
                }
                rewards.add(ruongDa);
                break;
            }
        }

        // Thông báo bắn pháo hoa
        Manager.gI().chatKTG(0, "🎆 " + p.name + " đã bắn pháo hoa rực rỡ!", 5);

        Service.send_gift(p, 0, "Bạn nhận được:", "", rewards, true);
    }

    /**
     * Xử lý Hộp Bánh Trung Thu (211)
     */
    public static void openHopBanh(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Trung Thu chưa được kích hoạt!");
            return;
        }

        List<GiftBox> rewards = new ArrayList<>();

        // Random 1 trong 4 phần thưởng
        int rand = Util.random(4);
        switch (rand) {
            case 0: {
                // Ruby 10-50
                ItemTemplate4 rubyTemplate = ItemTemplate4.get_it_by_id(1);
                GiftBox ruby = new GiftBox();
                ruby.type = 4;
                ruby.id = 1;
                ruby.num = Util.random(10, 50);
                ruby.color = 5;
                if (rubyTemplate != null) {
                    ruby.name = "ruby";
                    ruby.icon = rubyTemplate.icon;
                }
                rewards.add(ruby);
                break;
            }
            case 1: {
                // Bột Vàng + Tinh Thể
                ItemTemplate7 botVangTemplate = ItemTemplate7.get_it_by_id(1);
                GiftBox botVang = new GiftBox();
                botVang.type = 7;
                botVang.id = 1;
                botVang.num = Util.random(2, 5);
                botVang.color = 0;
                if (botVangTemplate != null) {
                    botVang.name = botVangTemplate.name;
                    botVang.icon = botVangTemplate.icon;
                }
                rewards.add(botVang);

                ItemTemplate7 acQuyTemplate = ItemTemplate7.get_it_by_id(9);
                GiftBox acQuy = new GiftBox();
                acQuy.type = 7;
                acQuy.id = 9;
                acQuy.num = Util.random(2, 5);
                acQuy.color = 0;
                if (acQuyTemplate != null) {
                    acQuy.name = acQuyTemplate.name;
                    acQuy.icon = acQuyTemplate.icon;
                }
                rewards.add(acQuy);
                break;
            }
            case 2: {
                // Beri lớn
                ItemTemplate4 beriTemplate = ItemTemplate4.get_it_by_id(0);
                GiftBox beri = new GiftBox();
                beri.type = 4;
                beri.id = 0;
                beri.num = Util.random(100000, 500000);
                beri.color = 0;
                if (beriTemplate != null) {
                    beri.name = beriTemplate.name;
                    beri.icon = beriTemplate.icon;
                }
                rewards.add(beri);
                break;
            }
            case 3: {
                // Rương Đại Ác Quỷ
                ItemTemplate4 ruongTemplate = ItemTemplate4.get_it_by_id(158);
                GiftBox ruong = new GiftBox();
                ruong.type = 4;
                ruong.id = 158;
                ruong.num = 1;
                ruong.color = 5;
                if (ruongTemplate != null) {
                    ruong.name = ruongTemplate.name;
                    ruong.icon = ruongTemplate.icon;
                }
                rewards.add(ruong);
                break;
            }
        }

        Service.send_gift(p, 0, "Quà từ Hộp Bánh Trung Thu:", "", rewards, true);
    }

    /**
     * Xử lý Hộp Bánh Thượng Hạng (576)
     */
    public static void openHopBanhThuongHang(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Trung Thu chưa được kích hoạt!");
            return;
        }

        List<GiftBox> rewards = new ArrayList<>();

        // Random 1 trong 4 phần thưởng
        int rand = Util.random(4);
        switch (rand) {
            case 0: {
                // Ruby 10-50
                ItemTemplate4 rubyTemplate = ItemTemplate4.get_it_by_id(1);
                GiftBox ruby = new GiftBox();
                ruby.type = 4;
                ruby.id = 1;
                ruby.num = Util.random(10, 50);
                ruby.color = 5;
                if (rubyTemplate != null) {
                    ruby.name = "ruby";
                    ruby.icon = rubyTemplate.icon;
                }
                rewards.add(ruby);
                break;
            }
            case 1: {
                // Đá Ác Quỷ + Tinh Thể
                ItemTemplate7 daAcQuyTemplate = ItemTemplate7.get_it_by_id(8);
                GiftBox daAcQuy = new GiftBox();
                daAcQuy.type = 7;
                daAcQuy.id = 8;
                daAcQuy.num = Util.random(15, 30);
                daAcQuy.color = 0;
                if (daAcQuyTemplate != null) {
                    daAcQuy.name = daAcQuyTemplate.name;
                    daAcQuy.icon = daAcQuyTemplate.icon;
                }
                rewards.add(daAcQuy);

                ItemTemplate7 tinhTheTemplate = ItemTemplate7.get_it_by_id(9);
                GiftBox tinhThe = new GiftBox();
                tinhThe.type = 7;
                tinhThe.id = 9;
                tinhThe.num = Util.random(3, 7);
                tinhThe.color = 0;
                if (tinhTheTemplate != null) {
                    tinhThe.name = tinhTheTemplate.name;
                    tinhThe.icon = tinhTheTemplate.icon;
                }
                rewards.add(tinhThe);
                break;
            }
            case 2: {
                // Thẻ TT Trung Thu
                ItemTemplate4 theTTTemplate = ItemTemplate4.get_it_by_id(ITEM_THE_TT_TRUNG_THU);
                GiftBox theTT = new GiftBox();
                theTT.type = 4;
                theTT.id = ITEM_THE_TT_TRUNG_THU;
                theTT.num = 1;
                theTT.color = 5;
                if (theTTTemplate != null) {
                    theTT.name = theTTTemplate.name;
                    theTT.icon = theTTTemplate.icon;
                }
                rewards.add(theTT);
                break;
            }
            case 3: {
                // Rương Đại Ác Quỷ + Pet Thỏ
                ItemTemplate4 ruongTemplate = ItemTemplate4.get_it_by_id(87);
                GiftBox ruong = new GiftBox();
                ruong.type = 4;
                ruong.id = 87;
                ruong.num = 1;
                ruong.color = 5;
                if (ruongTemplate != null) {
                    ruong.name = ruongTemplate.name;
                    ruong.icon = ruongTemplate.icon;
                }
                rewards.add(ruong);

                // Pet Thỏ
                int petRand = Util.random(100);
                ItemTemplate4 petTemplate = ItemTemplate4.get_it_by_id(34);
                GiftBox pet = new GiftBox();
                pet.type = 4;
                pet.id = 34;
                if (petTemplate != null) {
                    pet.name = petTemplate.name;
                    pet.icon = petTemplate.icon;
                }
                if (petRand < 70) {
                    pet.num = 1; // 1 ngày
                    pet.color = 0;
                } else if (petRand < 95) {
                    pet.num = 7; // 7 ngày
                    pet.color = 1;
                } else {
                    pet.num = -1; // Vĩnh viễn
                    pet.color = 5;
                }
                rewards.add(pet);
                break;
            }
        }

        Service.send_gift(p, 0, "Bạn mở Hộp Bánh Thượng Hạng và nhận được:", "", rewards, true);
    }

    /**
     * Xử lý Thẻ TT Trung Thu (475) - Nhận thời trang
     */
    public static void useTheTTTrungThu(Player p) throws IOException {
        if (!isEvent()) {
            Service.send_box_ThongBao_OK(p, "Sự kiện Trung Thu chưa được kích hoạt!");
            return;
        }

        // Kiểm tra đã có thẻ chưa
        if (p.item.total_item_bag_by_id(4, ITEM_THE_TT_TRUNG_THU) <= 0) {
            Service.send_box_ThongBao_OK(p, "Bạn không có Thẻ TT Trung Thu!");
            return;
        }

        // Random giữa Chú Cuội (65) và Chị Hằng (66)
        int fashionId = Util.random(2) == 0 ? FASHION_CHU_CUOI : FASHION_CHI_HANG;
        String fashionName = fashionId == FASHION_CHU_CUOI ? "Chú Cuội" : "Chị Hằng";

        // Kiểm tra đã có thời trang này chưa
        if (p.check_fashion(fashionId) != null) {
            Service.send_box_ThongBao_OK(p, "Bạn đã sở hữu thời trang này rồi!");
            return;
        }

        // Thêm thời trang vào inventory
        ItemFashionP2 newFashion = new ItemFashionP2();
        newFashion.id = (short) fashionId;
        newFashion.is_use = false;
        newFashion.level = 0;
        p.fashion.add(newFashion);

        // Xóa thẻ TT
        p.item.remove_item47(4, ITEM_THE_TT_TRUNG_THU, 1);
        p.item.update_Inventory(-1, false);

        Service.send_box_ThongBao_OK(p,
                "Bạn đã nhận được Thời trang " + fashionName
                        + " vĩnh viễn! (+130% né tránh, +80% HP, +100% Miễn thương)");
    }

    // ================== CRAFITING RECIPES ==================

    /**
     * Kiểm tra xem item có phải là nguyên liệu/craftable của sự kiện không
     */
    public static boolean isEventCraftItem(int itemId) {
        return itemId == ITEM_BANH_TRUNG_THU || itemId == ITEM_BANH_DAU_XANH
                || itemId == ITEM_BANH_TRUNG_MUOI || itemId == ITEM_BANH_HAT_SEN
                || itemId == ITEM_DEN_KEO_QUAN || itemId == ITEM_HOP_BANH
                || itemId == ITEM_HOP_BANH_THUONG_HANG;
    }

    /**
     * Lấy công thức craft cho item sự kiện
     * 
     * @return mảng [resultId, mat1, mat2, mat3, beriCost, rubyCost]
     *         null nếu không có công thức
     */
    public static int[] getCraftRecipe(int itemId) {
        switch (itemId) {
            case ITEM_BANH_TRUNG_THU:
                return new int[] { ITEM_BANH_TRUNG_THU, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 500000, 0 };
            case ITEM_BANH_DAU_XANH:
                return new int[] { ITEM_BANH_DAU_XANH, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 1, ITEM_TRUNG_MUOI, 1000000, 0 };
            case ITEM_BANH_TRUNG_MUOI:
                return new int[] { ITEM_BANH_TRUNG_MUOI, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 2, ITEM_TRUNG_MUOI, 15000000,
                        0 };
            case ITEM_BANH_HAT_SEN:
                return new int[] { ITEM_BANH_HAT_SEN, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 3, ITEM_TRUNG_MUOI, 2000000, 0 };
            case ITEM_DEN_KEO_QUAN:
                return new int[] { ITEM_DEN_KEO_QUAN, 3, ITEM_DEN_ONG_SAO, 2000000, 0 };
            case ITEM_HOP_BANH:
                return new int[] { ITEM_HOP_BANH, 1, ITEM_BANH_TRUNG_THU, 1, ITEM_BANH_DAU_XANH,
                        1, ITEM_BANH_TRUNG_MUOI, 1, ITEM_BANH_HAT_SEN, 2000000, 50 };
            case ITEM_HOP_BANH_THUONG_HANG:
                return new int[] { ITEM_HOP_BANH_THUONG_HANG, 1, ITEM_HOP_BANH, 1, ITEM_GIAY_GOI_QUA,
                        2000000, 100 };
            default:
                return null;
        }
    }

    /**
     * Lấy text mô tả công thức
     */
    public static String getCraftDescription(int itemId) {
        switch (itemId) {
            case ITEM_BANH_TRUNG_THU:
                return "5 Bột Mì + 3 Đường + 500.000 Beri";
            case ITEM_BANH_DAU_XANH:
                return "5 Bột Mì + 3 Đường + 1 Trứng Muối + 1.000.000 Beri";
            case ITEM_BANH_TRUNG_MUOI:
                return "5 Bột Mì + 3 Đường + 2 Trứng Muối + 15.000.000 Beri";
            case ITEM_BANH_HAT_SEN:
                return "5 Bột Mì + 3 Đường + 3 Trứng Muối + 2.000.000 Beri";
            case ITEM_DEN_KEO_QUAN:
                return "3 Đèn Ông Sao + 2.000.000 Beri";
            case ITEM_HOP_BANH:
                return "1 Bánh Trung Thu + 1 Bánh Đậu Xanh + 1 Bánh Trứng Muối + 1 Bánh Hạt Sen + 2.000.000 Beri + 50 Ruby";
            case ITEM_HOP_BANH_THUONG_HANG:
                return "1 Hộp Bánh + 1 Giấy Gói Quà + 2.000.000 Beri + 100 Ruby";
            default:
                return "";
        }
    }

    // ================== DROP MATERIALS ==================

    /**
     * Kiểm tra xem có phải item sự kiện không
     */
    public static boolean isEventMaterial(int itemId) {
        return itemId == ITEM_BOT_MI || itemId == ITEM_DUONG
                || itemId == ITEM_TRUNG_MUOI || itemId == ITEM_DEN_ONG_SAO
                || itemId == ITEM_GIAY_GOI_QUA;
    }

    /**
     * Thêm nguyên liệu sự kiện vào túi player
     */
    public static void addMaterial(Player p, int itemId, int quantity) {
        if (!isEvent() || p == null)
            return;

        switch (itemId) {
            case ITEM_BOT_MI:
            case ITEM_DUONG:
            case ITEM_TRUNG_MUOI:
            case ITEM_DEN_ONG_SAO:
            case ITEM_GIAY_GOI_QUA:
                p.item.add_item_bag47(4, itemId, quantity);
                break;
        }
    }

    /**
     * Thưởng nguyên liệu khi hoàn thành nhiệm vụ lặp (Đường)
     */
    public static void rewardNhiemVuLap(Player p) {
        if (!isEvent() || p == null)
            return;
        if (p.duongReceivedToday >= 100)
            return;

        p.duongReceivedToday++;
        addMaterial(p, ITEM_DUONG, 1);
        try {
            Service.send_box_ThongBao_OK(p, "Bạn nhận được 1 Đường từ nhiệm vụ lặp!");
        } catch (IOException e) {
            System.out.println("Error sending message: " + e.getMessage());
        }
    }

    /**
     * Thưởng nguyên liệu khi hoàn thành nhiệm vụ băng (Trứng Muối)
     */
    public static void rewardNhiemVuBang(Player p) {
        if (!isEvent() || p == null)
            return;
        addMaterial(p, ITEM_TRUNG_MUOI, 2);
        try {
            Service.send_box_ThongBao_OK(p, "Bạn nhận được 2 Trứng Muối từ nhiệm vụ băng!");
        } catch (IOException e) {
            System.out.println("Error sending message: " + e.getMessage());
        }
    }

    /**
     * Thưởng nguyên liệu khi PvP hoặc Truy nã (Trứng Muối)
     */
    public static void rewardPvpTruyNa(Player p) {
        if (!isEvent() || p == null)
            return;
        if (p.trungMuoiReceivedToday >= 100)
            return;
        int amount = 5;
        if (p.trungMuoiReceivedToday + amount > 100) {
            amount = 100 - p.trungMuoiReceivedToday;
        }
        if (amount <= 0)
            return;

        p.trungMuoiReceivedToday += amount;
        addMaterial(p, ITEM_TRUNG_MUOI, amount);
        try {
            Service.send_box_ThongBao_OK(p, "Bạn nhận được " + amount + " Trứng Muối từ hoạt động!");
        } catch (IOException e) {
            System.out.println("Error sending message: " + e.getMessage());
        }
    }

    /**
     * Thưởng nguyên liệu khi hoàn thành đi liên tầng hoặc đá đít Mr3 (Đèn Ông Sao)
     */
    public static void rewardLienTangMr3(Player p) {
        if (!isEvent() || p == null)
            return;
        addMaterial(p, ITEM_DEN_ONG_SAO, 1);
        try {
            Service.send_box_ThongBao_OK(p, "Bạn nhận được 1 Đèn Ông Sao từ hoạt động!");
        } catch (IOException e) {
            System.out.println("Error sending message: " + e.getMessage());
        }
    }

    // ================== UTILITY ==================

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

    /**
     * Thưởng khi hoàn thành Phong Thách Nami (event Trung Thu)
     */
    public static void rewardPhongThachNami(Player p) {
        if (!isEvent() || p == null)
            return;
        addMaterial(p, ITEM_BOT_MI, Util.random(5, 15));
        try {
            Service.send_box_ThongBao_OK(p, "Bạn nhận được Bột Mì từ Phong Thách Nami!");
        } catch (IOException e) {
            System.out.println("Error sending message: " + e.getMessage());
        }
    }

    /**
     * Hiển thị bảng xếp hạng giết Lân
     */
    public static void showLanKillLeaderboard(Player p) {
        if (p == null)
            return;

        try {
            // Cập nhật BXH trước khi hiển thị
            BXH.update();

            StringBuilder sb = new StringBuilder();
            sb.append("🏆 BẢNG XẾP HẠNG GIẾT LÂN SƯ TỬ\n");
            sb.append("═══════════════════════════════\n\n");

            List<InfoMemList> topKillers = BXH.LAN_KILLS;

            if (topKillers.isEmpty()) {
                sb.append("Chưa có ai giết Lân nào!\n");
                sb.append("Hãy nhanh tay tham gia săn Lân nhé!\n");
            } else {
                for (int i = 0; i < Math.min(10, topKillers.size()); i++) {
                    InfoMemList entry = topKillers.get(i);
                    String rankEmoji;
                    if (i == 0) {
                        rankEmoji = "🥇";
                    } else if (i == 1) {
                        rankEmoji = "🥈";
                    } else if (i == 2) {
                        rankEmoji = "🥉";
                    } else {
                        rankEmoji = (i + 1) + ".";
                    }

                    sb.append(rankEmoji).append(" ").append(entry.name)
                      .append(": ").append(entry.thongthao).append(" Lân\n");

                    if (i == 0) {
                        sb.append("   └─ Phần thưởng: Hộp Bánh Thượng Hạng\n");
                    } else if (i < 5) {
                        sb.append("   └─ Phần thưởng: Hộp Bánh x3\n");
                    }
                }
            }

            sb.append("\n═══════════════════════════════\n");
            sb.append("📊 Thành tích của bạn: ").append(p.lanKills).append(" Lân\n");
            sb.append("📍 Top của bạn: ").append(getPlayerRankLanKills(p)).append("\n");

            Service.send_box_ThongBao_OK(p, sb.toString());
        } catch (IOException e) {
            System.out.println("Error showing lan kill leaderboard: " + e.getMessage());
        }
    }

    /**
     * Lấy rank của player hiện tại trong BXH giết Lân
     */
    private static int getPlayerRankLanKills(Player p) {
        if (p == null || p.lanKills <= 0) {
            return -1;
        }
        List<InfoMemList> list = BXH.LAN_KILLS;
        for (int i = 0; i < list.size(); i++) {
            if (list.get(i).name.equals(p.name)) {
                return i + 1;
            }
        }
        return -1;
    }
}
