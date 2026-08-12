package event;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import client.Player;
import core.Manager;
import core.Service;
import core.Util;
import io.Message;
import map.Boss;
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
    public static final int ITEM_BOT_MI = 202;       // Bột Mì
    public static final int ITEM_DUONG = 200;         // Đường
    public static final int ITEM_TRUNG_MUOI = 203;    // Trứng Muối
    public static final int ITEM_DEN_ONG_SAO = 473;  // Đèn ông sao
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
    private static final int[] BOSS_SPAWN_HOURS = {12, 18, 20, 22};
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
        if (hour >= 11 && hour < 13) return true;
        if (hour >= 20 && hour < 21) return true;
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
                spawnBossLan();
            }
            if (bossAlive && activeBossLan != null && now >= bossSpawnTime + BOSS_LIFETIME_MS) {
                despawnBoss("Boss Lân Sư Tử đã bỏ đi!");
            }
        } else {
            if (bossAlive && activeBossLan != null) {
                despawnBoss("Hết thời gian sự kiện, Boss Lân Sư Tử đã biến mất!");
            }
        }
    }



    private void spawnBossLan() {
        if (bossAlive) return;

        List<Map> allowedMaps = new ArrayList<>();
        for (int mapId : Boss.ALLOWED_MAP_IDS) {
            Map[] maps = Map.get_map_by_id(mapId);
            if (maps != null) {
                for (Map m : maps) {
                    if (m != null && !isVillageMap(m.template.id)) {
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

        // Tìm boss entry với mob template 153
        for (Boss boss : Boss.ENTRYS) {
            if (boss != null && boss.mob != null
                    && boss.mob.mob_template != null
                    && boss.mob.mob_template.mob_id == MOB_BOSS_LAN
                    && boss.mob.isdie) {

                boss.mob.isdie = false;
                boss.mob.hp = boss.mob.hp_max;
                boss.mob.id_target = -1;
                boss.levelBoss = 1;
                boss.updateHpForLevel();
                boss.mob.index = boss.index_mob_save;
                boss.TopDame.clear();

                activeBossLan = boss;
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
                } catch (IOException e) {
                    System.out.println("Error announcing boss spawn: " + e.getMessage());
                }
                return;
            }
        }

        // Retry in 1 minute if failed to spawn
        nextBossSpawnTime = System.currentTimeMillis() + 60_000L;
    }

    private boolean isVillageMap(int mapId) {
        return mapId == 0 || mapId == 21 || mapId == 22;
    }

    public void onBossDamaged(Player player, int damage) {
        if (!bossAlive || activeBossLan == null) return;

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
        if (!bossAlive || activeBossLan == null) return;

        lastHitPlayer = killer;

        // Thông báo
        try {
            Manager.gI().chatKTG(0,
                    "🎉 Chúc mừng " + killer.name + " đã hạ gục Boss Lân Sư Tử!",
                    5);
        } catch (IOException e) {
            System.out.println("Error announcing boss kill: " + e.getMessage());
        }

        // Phần thưởng Last Hit
        giveLastHitReward(killer);

        // Phần thưởng tham gia
        giveParticipationRewards();

        // Rơi item nhặt lộc
        spawnLuckyDrops();

        // Reset
        despawnBoss(null);
    }

    private void giveLastHitReward(Player player) {
        if (player == null) return;

        List<GiftBox> rewards = new ArrayList<>();

        // 1 Hộp Bánh Thượng Hạng
        GiftBox hopBanH = new GiftBox();
        hopBanH.type = 4;
        hopBanH.id = ITEM_HOP_BANH_THUONG_HANG;
        hopBanH.num = 1;
        hopBanH.icon = 165;
        hopBanH.color = 5;
        rewards.add(hopBanH);

        // 50 Ruby
        GiftBox ruby = new GiftBox();
        ruby.type = 4;
        ruby.name = "ruby";
        ruby.num = 50;
        ruby.icon = 1;
        ruby.color = 5;
        ruby.id = 1;
        rewards.add(ruby);

        // 2 Giấy Gói Quà
        GiftBox giay = new GiftBox();
        giay.type = 4;
        giay.id = ITEM_GIAY_GOI_QUA;
        giay.num = 2;
        giay.icon = 104;
        giay.color = 0;
        rewards.add(giay);

        try {
            Service.send_gift(player, 0, "Phần thưởng Đòn Kết Liễu Boss Lân!", "", rewards, true);
        } catch (IOException e) {
            System.out.println("Error giving last hit reward: " + e.getMessage());
        }
    }

    private void giveParticipationRewards() {
        List<GiftBox> baseRewards = new ArrayList<>();

        // 100k Beri
        GiftBox beri = new GiftBox();
        beri.type = 4;
        beri.id = 0;
        beri.num = 100000;
        beri.icon = 0;
        beri.color = 0;
        baseRewards.add(beri);

        // 5 Bột Mì
        GiftBox botMi = new GiftBox();
        botMi.type = 4;
        botMi.id = ITEM_BOT_MI;
        botMi.num = 5;
        botMi.icon = 156;
        botMi.color = 0;
        baseRewards.add(botMi);

        // 5 Đường
        GiftBox duong = new GiftBox();
        duong.type = 4;
        duong.id = ITEM_DUONG;
        duong.num = 5;
        duong.icon = 154;
        duong.color = 0;
        baseRewards.add(duong);

        // 2 Trứng Muối
        GiftBox trung = new GiftBox();
        trung.type = 4;
        trung.id = ITEM_TRUNG_MUOI;
        trung.num = 2;
        trung.icon = 157;
        trung.color = 0;
        baseRewards.add(trung);

        // 1 Giấy Gói Quà
        GiftBox giay = new GiftBox();
        giay.type = 4;
        giay.id = ITEM_GIAY_GOI_QUA;
        giay.num = 1;
        giay.icon = 104;
        giay.color = 0;
        baseRewards.add(giay);

        for (Player p : participatedPlayers) {
            try {
                Service.send_gift(p, 0, "Phần thưởng tham gia Boss Lân!", "", baseRewards, true);
            } catch (IOException e) {
                System.out.println("Error giving participation reward: " + e.getMessage());
            }
        }
    }

    private void spawnLuckyDrops() {
        if (activeBossLan == null || activeBossLan.mob == null) return;

        Map map = activeBossLan.mob.map;
        if (map == null) return;

        int dropCount = Util.random(30, 50);
        ItemTemplate4[] possibleItems = new ItemTemplate4[]{
                ItemTemplate4.get_it_by_id(0),   // Beri
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
        if (activeBossLan != null && activeBossLan.mob != null && !activeBossLan.mob.isdie) {
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
                // Đá Khảm Cấp 6 ngẫu nhiên
                int gemId = Util.random(44, 83);
                GiftBox gem = new GiftBox();
                gem.type = 4;
                gem.id = (short)gemId;
                gem.num = 1;
                gem.color = 0;
                rewards.add(gem);

                // Beri hoặc Ruby ngẫu nhiên
                if (Util.random(2) == 0) {
                    GiftBox beri = new GiftBox();
                    beri.type = 4;
                    beri.id = 0;
                    beri.num = Util.random(50000, 150000);
                    beri.color = 0;
                    rewards.add(beri);
                } else {
                    GiftBox ruby = new GiftBox();
                    ruby.type = 4;
                    ruby.name = "ruby";
                    ruby.num = Util.random(50, 150);
                    ruby.id = 1;
                    ruby.color = 5;
                    rewards.add(ruby);
                }
                break;
            }
            case ITEM_BANH_DAU_XANH: {
                int gemId = Util.random(44, 83);
                GiftBox gem = new GiftBox();
                gem.type = 4;
                gem.id = (short)gemId;
                gem.num = 1;
                gem.color = 0;
                rewards.add(gem);

                GiftBox beri = new GiftBox();
                beri.type = 4;
                beri.id = 0;
                beri.num = Util.random(500000, 15000000);
                beri.color = 0;
                rewards.add(beri);

                GiftBox expCard = new GiftBox();
                expCard.type = 4;
                expCard.id = 266; // Thẻ x2 EXP
                expCard.num = 1;
                expCard.icon = 0;
                expCard.color = 0;
                rewards.add(expCard);
                break;
            }
            case ITEM_BANH_TRUNG_MUOI: {
                int gemId = Util.random(44, 83);
                GiftBox gem = new GiftBox();
                gem.type = 4;
                gem.id = (short)gemId;
                gem.num = 1;
                gem.color = 0;
                rewards.add(gem);

                GiftBox beri = new GiftBox();
                beri.type = 4;
                beri.id = 0;
                beri.num = Util.random(800000, 20000000);
                beri.color = 0;
                rewards.add(beri);

                GiftBox acQuy = new GiftBox();
                acQuy.type = 7;
                acQuy.id = 9; // Tinh thể đá ác quỷ
                acQuy.num = Util.random(2, 5);
                acQuy.color = 0;
                rewards.add(acQuy);
                break;
            }
            case ITEM_BANH_HAT_SEN: {
                int gemId = Util.random(44, 83);
                GiftBox gem = new GiftBox();
                gem.type = 4;
                gem.id = (short)gemId;
                gem.num = 1;
                gem.color = 0;
                rewards.add(gem);

                GiftBox beri = new GiftBox();
                beri.type = 4;
                beri.id = 0;
                beri.num = Util.random(1000000, 25000000);
                beri.color = 0;
                rewards.add(beri);

                // Bột Vàng hoặc Mai Rùa
                GiftBox bonus = new GiftBox();
                bonus.type = 7;
                bonus.id = (short) (Util.random(2) == 0 ? 1 : 2); // Bột vàng hoặc mai rùa
                bonus.num = Util.random(2, 5);
                bonus.color = 0;
                rewards.add(bonus);
                break;
            }
        }

        Service.send_gift(p, 0, "Bạn mở bánh Trung Thu và nhận được:", "", rewards, true);
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
        int rand = Util.random(9);
        switch (rand) {
            case 0: {
                GiftBox ngusac = new GiftBox();
                ngusac.type = 7;
                ngusac.id = 0; // Đá ngũ sắc
                ngusac.num = Util.random(1, 3);
                ngusac.color = 0;
                rewards.add(ngusac);
                break;
            }
            case 1: {
                GiftBox botCuong = new GiftBox();
                botCuong.type = 7;
                botCuong.id = 1; // Bột cường hóa
                botCuong.num = Util.random(1, 3);
                botCuong.color = 0;
                rewards.add(botCuong);
                break;
            }
            case 2: {
                GiftBox expCard = new GiftBox();
                expCard.type = 4;
                expCard.id = 266; // Thẻ x2 EXP
                expCard.num = 1;
                expCard.color = 0;
                rewards.add(expCard);
                break;
            }
            case 3: {
                GiftBox doiTen = new GiftBox();
                doiTen.type = 4;
                doiTen.id = 265; // Thẻ đổi tên
                doiTen.num = 1;
                doiTen.color = 0;
                rewards.add(doiTen);
                break;
            }
            case 4: {
                int level = p.level;
                if (level < 10) level = 10;
                if (level > 90) level = 90;
                int chestIdNormal = 111 + level / 10;
                template.ItemTemplate4 it_rcam = template.ItemTemplate4.get_it_by_id(chestIdNormal);
                if (it_rcam != null) {
                    GiftBox giftChest = new GiftBox();
                    giftChest.id = (short) chestIdNormal;
                    giftChest.type = 4;
                    giftChest.name = it_rcam.name;
                    giftChest.num = 1;
                    giftChest.color = 0;
                    rewards.add(giftChest);
                }
                break;
            }
            case 5: {
                GiftBox mairua = new GiftBox();
                mairua.type = 7;
                mairua.id = 2; // Mai rùa
                mairua.num = Util.random(1, 3);
                mairua.color = 0;
                rewards.add(mairua);
                break;
            }
            case 6: {
                GiftBox dKham5 = new GiftBox();
                dKham5.type = 4;
                dKham5.id = (short) Util.random(127, 136); // Đá khảm cấp 5 ngẫu nhiên
                dKham5.num = 1;
                dKham5.color = 0;
                rewards.add(dKham5);
                break;
            }
            case 7: {
                GiftBox daquy = new GiftBox();
                daquy.type = 7;
                daquy.id = 8; // Đá ác quỷ
                daquy.num = 1;
                daquy.color = 0;
                rewards.add(daquy);
                break;
            }
            case 8: {
                GiftBox ruongDa = new GiftBox();
                ruongDa.type = 4;
                ruongDa.id = 87; // Rương đại ác quỷ
                ruongDa.num = 1;
                ruongDa.color = 0;
                rewards.add(ruongDa);
                break;
            }
        }

        // Thông báo bắn pháo hoa
        Manager.gI().chatKTG(0, "🎆 " + p.name + " đã bắn pháo hoa rực rỡ!", 5);

        Service.send_gift(p, 0, "Pháo hoa rực rỡ! Bạn nhận được:", "", rewards, true);
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

        // Ruby
        GiftBox ruby = new GiftBox();
        ruby.type = 4;
        ruby.name = "ruby";
        ruby.num = Util.random(1500, 3500);
        ruby.id = 1;
        ruby.color = 5;
        rewards.add(ruby);

        // Bột Vàng
        GiftBox botVang = new GiftBox();
        botVang.type = 7;
        botVang.id = 1;
        botVang.num = Util.random(1, 3);
        botVang.color = 0;
        rewards.add(botVang);

        // Tinh Thể Đá Ác Quỷ
        GiftBox acQuy = new GiftBox();
        acQuy.type = 7;
        acQuy.id = 9;
        acQuy.num = Util.random(1, 3);
        acQuy.color = 0;
        rewards.add(acQuy);

        // Beri
        GiftBox beri = new GiftBox();
        beri.type = 4;
        beri.id = 0;
        beri.num = Util.random(1000000, 5000000);
        beri.color = 0;
        rewards.add(beri);

        Service.send_gift(p, 0, "Bạn mở Hộp Bánh Trung Thu và nhận được:", "", rewards, true);
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

        // Ruby
        GiftBox ruby = new GiftBox();
        ruby.type = 4;
        ruby.name = "ruby";
        ruby.num = Util.random(1500, 3500);
        ruby.id = 1;
        ruby.color = 5;
        rewards.add(ruby);

        // Đá Ác Quỷ
        GiftBox daAcQuy = new GiftBox();
        daAcQuy.type = 7;
        daAcQuy.id = 8;
        daAcQuy.num = Util.random(10, 20);
        daAcQuy.color = 0;
        rewards.add(daAcQuy);

        // Bột Vàng
        GiftBox botVang = new GiftBox();
        botVang.type = 7;
        botVang.id = 1;
        botVang.num = Util.random(2, 5);
        botVang.color = 0;
        rewards.add(botVang);

        // Tinh Thể
        GiftBox tinhThe = new GiftBox();
        tinhThe.type = 7;
        tinhThe.id = 9;
        tinhThe.num = Util.random(2, 5);
        tinhThe.color = 0;
        rewards.add(tinhThe);

        // Thẻ TT Trung Thu
        GiftBox theTT = new GiftBox();
        theTT.type = 4;
        theTT.id = ITEM_THE_TT_TRUNG_THU;
        theTT.num = 1;
        theTT.icon = 423;
        theTT.color = 5;
        rewards.add(theTT);

        // Rương Đại Ác Quỷ
        GiftBox ruong = new GiftBox();
        ruong.type = 4;
        ruong.id = 87;
        ruong.num = 1;
        ruong.color = 5;
        rewards.add(ruong);

        // Pet Thỏ (theo tỷ lệ)
        int petRand = Util.random(100);
        GiftBox pet = new GiftBox();
        pet.type = 4;
        pet.id = 34; // Pet thỏ
        pet.icon = 0;
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
        if (p.item.total_item_bag_by_id(7, ITEM_THE_TT_TRUNG_THU) <= 0) {
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
        p.item.remove_item47(7, ITEM_THE_TT_TRUNG_THU, 1);
        p.item.update_Inventory(-1, false);

        Service.send_box_ThongBao_OK(p,
                "Bạn đã nhận được Thời trang " + fashionName + " vĩnh viễn! (+130% né tránh, +80% HP, +100% Miễn thương)");
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
     * @return mảng [resultId, mat1, mat2, mat3, beriCost, rubyCost]
     *         null nếu không có công thức
     */
    public static int[] getCraftRecipe(int itemId) {
        switch (itemId) {
            case ITEM_BANH_TRUNG_THU:
                return new int[]{ITEM_BANH_TRUNG_THU, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 500000, 0};
            case ITEM_BANH_DAU_XANH:
                return new int[]{ITEM_BANH_DAU_XANH, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 1, ITEM_TRUNG_MUOI, 1000000, 0};
            case ITEM_BANH_TRUNG_MUOI:
                return new int[]{ITEM_BANH_TRUNG_MUOI, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 2, ITEM_TRUNG_MUOI, 15000000, 0};
            case ITEM_BANH_HAT_SEN:
                return new int[]{ITEM_BANH_HAT_SEN, 5, ITEM_BOT_MI, 3, ITEM_DUONG, 3, ITEM_TRUNG_MUOI, 2000000, 0};
            case ITEM_DEN_KEO_QUAN:
                return new int[]{ITEM_DEN_KEO_QUAN, 3, ITEM_DEN_ONG_SAO, 2000000, 0};
            case ITEM_HOP_BANH:
                return new int[]{ITEM_HOP_BANH, 1, ITEM_BANH_TRUNG_THU, 1, ITEM_BANH_DAU_XANH,
                        1, ITEM_BANH_TRUNG_MUOI, 1, ITEM_BANH_HAT_SEN, 2000000, 50};
            case ITEM_HOP_BANH_THUONG_HANG:
                return new int[]{ITEM_HOP_BANH_THUONG_HANG, 1, ITEM_HOP_BANH, 1, ITEM_GIAY_GOI_QUA,
                        2000000, 100};
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
        if (!isEvent() || p == null) return;

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
        if (!isEvent() || p == null) return;
        if (p.duongReceivedToday >= 100) return;

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
        if (!isEvent() || p == null) return;
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
        if (!isEvent() || p == null) return;
        if (p.trungMuoiReceivedToday >= 100) return;
        int amount = 5;
        if (p.trungMuoiReceivedToday + amount > 100) {
            amount = 100 - p.trungMuoiReceivedToday;
        }
        if (amount <= 0) return;
        
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
        if (!isEvent() || p == null) return;
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
        if (!isEvent() || p == null) return;
        addMaterial(p, ITEM_BOT_MI, Util.random(5, 15));
        try {
            Service.send_box_ThongBao_OK(p, "Bạn nhận được Bột Mì từ Phong Thách Nami!");
        } catch (IOException e) {
            System.out.println("Error sending message: " + e.getMessage());
        }
    }
}
