package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;

import client.Player;
import core.Service;
import core.Util;
import io.Message;
import map.Map;
import map.Mob;
import map.Vgo;
import template.GiftBox;
import template.ItemTemplate4;
import template.ItemTemplate7;
import template.MobTemplate;

/**
 * Phó bản: "Bảo vệ kho báu Namie" (Namie Treasure Defense).
 *
 * Chủ đề: Hải quân đánh hơi được kho báu Namie cất giữ tại Đại bản doanh (map
 * 45)
 * và tổ chức nhiều đợt tấn công để cướp lấy. Party phải tiêu diệt hết quân địch
 * mỗi đợt trước khi chúng "áp sát" được rương kho báu; nếu không, rương sẽ bị
 * trừ máu. Rương hết máu -> thất bại. Sống sót hết tất cả các đợt trong khi
 * rương vẫn còn máu -> hoàn thành, phần thưởng theo % máu rương còn lại.
 *
 * Class này chủ đích được viết theo cùng khuôn mẫu với TowerChallenge.java để
 * dễ bảo trì / dễ so sánh khi debug (dùng ScheduledExecutorService để chuyển
 * đợt (wave) chủ động thay vì chỉ dựa vào vòng lặp update() bị động).
 *
 * @author Truongbk
 */
public class NamieTreasureDefense extends Dungeon {

    // ================== CẤU HÌNH (Dylan chỉnh lại theo dữ liệu thật)
    // ==================

    /**
     * ID bản đồ instance dùng riêng cho phó bản này (clone từ map 45 - Đại bản
     * doanh).
     */
    public static final int MAP_ID = 513;

    /** Map gốc để clone. */
    public static final int TEMPLATE_MAP_ID = 45;

    /** Toạ độ đặt rương kho báu (điểm mà quân địch nhắm tới). */
    public static final short TREASURE_X = 350;
    public static final short TREASURE_Y = 260;

    /** Tổng số đợt tấn công. */
    public static final int TOTAL_WAVES = 20;

    /** Máu tối đa của rương kho báu. */
    public static final int TREASURE_HP_MAX = 100_000;


    /**
     * Sát thương rương phải chịu mỗi khi 1 quái "áp sát" thành công (không bị giết
     * kịp).
     */
    public static final int BREACH_DAMAGE = 4_000;

    /**
     * Thời gian (ms) 1 quái cần để áp sát rương nếu không bị tiêu diệt, giảm dần
     * theo đợt.
     */
    public static final long BASE_TRAVEL_MS = 25_000L;
    public static final long MIN_TRAVEL_MS = 12_000L;

    /** Thời gian nghỉ giữa 2 đợt (ms). */
    public static final long WAVE_GAP_MS = 4_000L;

    /**
     * Giới hạn tổng thời gian toàn bộ phó bản (an toàn, tránh treo vô thời hạn).
     */
    public static final long TOTAL_TIME_LIMIT_MS = 20 * 60_000L;

    /**
     * ID mob_template (MobTemplate.ENTRYS) dùng cho từng đợt.
     * TODO(Dylan): thay các ID placeholder này bằng ID lính hải quân/hải tặc
     * thật trong danh sách MobTemplate của server (giống cách Boss.java dùng
     * mob_id 135-140 cho các boss).
     */
    public static final int[][] WAVE_MOB_TEMPLATE_IDS = {
            { 21, 21, 21, 22 }, // wave 1 (dễ)
            { 21, 22, 22, 23 }, // wave 2
            { 22, 23, 23, 24, 24 }, // wave 3
            { 23, 24, 24, 25, 25, 25 }, // wave 4
            { 25, 25, 26, 26, 26, 26, 27 }, // wave 5 - boss lính chỉ huy
            { 21, 21, 21, 22, 22 }, // wave 6
            { 22, 22, 22, 23, 23 }, // wave 7
            { 22, 23, 23, 24, 24, 24 }, // wave 8
            { 23, 24, 24, 25, 25, 25 }, // wave 9
            { 25, 25, 26, 26, 26, 26, 27, 27 }, // wave 10 - mid-boss
            { 23, 23, 24, 24, 24, 25 }, // wave 11
            { 24, 24, 25, 25, 25, 26 }, // wave 12
            { 24, 25, 25, 26, 26, 26, 27 }, // wave 13
            { 25, 26, 26, 27, 27, 27, 27 }, // wave 14
            { 26, 26, 27, 27, 27, 27, 27, 27 }, // wave 15 - mid-boss
            { 25, 25, 26, 26, 27, 27, 27, 27 }, // wave 16
            { 26, 26, 26, 27, 27, 27, 27, 27 }, // wave 17
            { 26, 27, 27, 27, 27, 27, 27, 27 }, // wave 18
            { 27, 27, 27, 27, 27, 27, 27, 27, 27 }, // wave 19
            { 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27 } // wave 20 - final boss wave
    };

    // ================== TRẠNG THÁI RUNTIME ==================

    public static final List<NamieTreasureDefense> ACTIVE_DEFENSES = new CopyOnWriteArrayList<>();
    private static final ScheduledExecutorService WAVE_SCHEDULER = Executors.newScheduledThreadPool(2, r -> {
        Thread t = new Thread(r, "NamieDefense-Wave");
        t.setDaemon(true);
        return t;
    });

    public List<Player> partyMembers = new ArrayList<>();
    public Player leader;
    public Map currentMap;
    public boolean active = false;
    public boolean finished = false;

    public int currentWaveIndex = -1; // 0..TOTAL_WAVES-1
    public int treasureHp = TREASURE_HP_MAX;
    public long dungeonEndTime;
    public long dungeonStartTime;

    public boolean isTransitioning = false;
    public long transitionEndTime = 0;
    private final AtomicBoolean waveCleared = new AtomicBoolean(false);
    private volatile ScheduledFuture<?> currentWaveFuture = null;

    /** Thời điểm 1 quái sẽ áp sát rương nếu chưa bị giết (key = mob.index). */
    private final java.util.Map<Integer, Long> mobBreachTime = new java.util.HashMap<>();
    /**
     * Số quái tiêu diệt được bởi từng người chơi trong toàn bộ phó bản (thống kê /
     * thưởng thêm sau này).
     */
    public final java.util.Map<String, Integer> killCount = new java.util.HashMap<>();

    public NamieTreasureDefense(List<Player> members, Player leader) {
        this.partyMembers.addAll(members);
        this.leader = leader;
        this.mode = 0;
        ACTIVE_DEFENSES.add(this);
        System.out.println("[NamieDefense] Created for leader " + leader.name
                + ", party size=" + members.size());
    }

    public static boolean isDefenseMap(int mapId) {
        return mapId == MAP_ID;
    }

    public static NamieTreasureDefense findActiveDefense(String name) {
        for (NamieTreasureDefense d : ACTIVE_DEFENSES) {
            if (d.active && !d.finished) {
                for (Player p : d.partyMembers) {
                    if (p.name.equals(name)) {
                        return d;
                    }
                }
            }
        }
        return null;
    }

    public synchronized void updateMemberReference(String name, Player newRef) {
        for (int i = 0; i < partyMembers.size(); i++) {
            if (partyMembers.get(i).name.equals(name)) {
                partyMembers.set(i, newRef);
                break;
            }
        }
        if (leader.name.equals(name)) {
            leader = newRef;
        }
    }

    // ================== KHỞI TẠO BẢN ĐỒ & ĐỢT TẤN CÔNG ==================

    public synchronized void start() {
        Map[] templates = Map.get_map_by_id(MAP_ID);
        if (templates == null || templates.length == 0) {
            System.out.println("[NamieDefense] ERROR: Map ID " + MAP_ID
                    + " chưa được cấu hình (thiếu data/map/" + MAP_ID + ".map hoặc chưa INSERT DB).");
            for (Player member : this.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null) {
                    try {
                        Service.send_box_ThongBao_OK(pOnline,
                                "Lỗi hệ thống: Phó bản Kho Báu Namie chưa được cấu hình. Liên hệ Admin.");
                    } catch (IOException ignored) {
                    }
                }
            }
            cleanup();
            return;
        }
        Map mapTemplate = templates[0];

        Map mapDungeon = new Map();
        mapDungeon.template = mapTemplate.template;
        mapDungeon.zone_id = (byte) 0;
        mapDungeon.list_mob = new int[0];

        mapDungeon.start_map();
        mapDungeon.map_dungeon = this;
        mapDungeon.map_dungeon.checkG = new HashSet<>();

        this.currentMap = mapDungeon;
        this.maps = new ArrayList<>();
        this.maps.add(mapDungeon);
        this.mobs = new ArrayList<>();

        Map.add_map_plus(mapDungeon);

        this.treasureHp = TREASURE_HP_MAX;
        this.dungeonEndTime = System.currentTimeMillis() + TOTAL_TIME_LIMIT_MS;
        this.time = this.dungeonEndTime;
        this.dungeonStartTime = System.currentTimeMillis();

        Vgo vgo = new Vgo();
        vgo.map_go = new Map[] { mapDungeon };
        vgo.xnew = TREASURE_X;
        vgo.ynew = (short) (TREASURE_Y - 40);

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.conn != null && pOnline.conn.connected) {
                pOnline.dungeon = this;
                try {
                    pOnline.goto_map(vgo);
                    Service.send_time_cool_down(pOnline, this.dungeonEndTime, "Thời gian", 2);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

        this.active = true;
        sendLocalNotice("Hải quân đang tiến đến! Hãy bảo vệ kho báu của Namie!");
        spawnWave(0);
    }

    private synchronized void spawnWave(int waveIndex) {
        if (waveIndex >= TOTAL_WAVES) {
            completeDefense();
            return;
        }
        this.currentWaveIndex = waveIndex;
        this.isTransitioning = false;
        this.transitionEndTime = 0;
        this.waveCleared.set(false);
        if (this.currentWaveFuture != null && !this.currentWaveFuture.isDone()) {
            this.currentWaveFuture.cancel(false);
            this.currentWaveFuture = null;
        }

        long travelMs = Math.max(MIN_TRAVEL_MS, BASE_TRAVEL_MS - waveIndex * 3_000L);
        long now = System.currentTimeMillis();

        int mobIndex = -2 - (waveIndex * 100); // tách biệt index giữa các đợt để tránh trùng
        List<Mob> waveMobs = new ArrayList<>();

        if (waveIndex == 19) {
            // Đợt 20: Chỉ xuất hiện 3 boss Along với 5M HP
            for (int i = 0; i < 3; i++) {
                MobTemplate mt = MobTemplate.ENTRYS.get(36); // Along
                if (mt == null)
                    continue;

                Mob mob = new Mob();
                mob.mob_template = mt;
                double angle = Util.random(360) * Math.PI / 180.0;
                int radius = 180 + Util.random(60);
                int spawnX = TREASURE_X + (int) (Math.cos(angle) * radius);
                int spawnY = TREASURE_Y + (int) (Math.sin(angle) * radius);
                spawnX = Math.max(20, Math.min(mapTemplateSafeMaxW(), spawnX));
                spawnY = Math.max(20, Math.min(mapTemplateSafeMaxH(), spawnY));
                mob.x = (short) spawnX;
                mob.y = (short) spawnY;
                mob.hp_max = 5_000_000;
                mob.hp = mob.hp_max;
                mob.level = 100;
                mob.isdie = false;
                mob.id_target = -1;
                mob.index = mobIndex--;
                mob.map = this.currentMap;
                mob.boss_info = null;
                this.mobs.add(mob);
                waveMobs.add(mob);
                mobBreachTime.put(mob.index, now + travelMs);
            }
        } else {
            // Các đợt từ 1 đến 19: Số lượng quái cố định là 30
            int[] templateIds = WAVE_MOB_TEMPLATE_IDS[Math.min(waveIndex, WAVE_MOB_TEMPLATE_IDS.length - 1)];

            // Xác định xem đợt này có boss không (Đợt 5, 10, 15 tương ứng waveIndex 4, 9,
            // 14)
            int bossTemplateId = -1;
            int bossHp = 0;
            if (waveIndex == 4) {
                bossTemplateId = 16; // Buggi (Đợt 5)
                bossHp = 100_000;
            } else if (waveIndex == 9) {
                bossTemplateId = 23; // Kurol (Đợt 10)
                bossHp = 250_000;
            } else if (waveIndex == 14) {
                bossTemplateId = 29; // Don Crey (Đợt 15)
                bossHp = 500_000;
            }

            int spawnCount = 30;
            for (int i = 0; i < spawnCount; i++) {
                int templateId;
                boolean isBoss = false;

                if (bossTemplateId != -1 && i == 0) {
                    // Spawn boss ở vị trí đầu tiên
                    templateId = bossTemplateId;
                    isBoss = true;
                } else {
                    // Spawn quái thường ngẫu nhiên từ template của đợt
                    templateId = templateIds[Util.random(templateIds.length)];
                }

                MobTemplate mt = MobTemplate.ENTRYS.get(templateId);
                if (mt == null)
                    continue;

                Mob mob = new Mob();
                mob.mob_template = mt;
                double angle = Util.random(360) * Math.PI / 180.0;
                int radius = 180 + Util.random(60);
                int spawnX = TREASURE_X + (int) (Math.cos(angle) * radius);
                int spawnY = TREASURE_Y + (int) (Math.sin(angle) * radius);
                spawnX = Math.max(20, Math.min(mapTemplateSafeMaxW(), spawnX));
                spawnY = Math.max(20, Math.min(mapTemplateSafeMaxH(), spawnY));
                mob.x = (short) spawnX;
                mob.y = (short) spawnY;

                if (isBoss) {
                    mob.hp_max = bossHp;
                    mob.level = (short) Math.min(100, mt.level + waveIndex * 2 + 5);
                } else {
                    mob.hp_max = mt.hp_max + waveIndex * 3_000;
                    mob.level = (short) Math.min(100, mt.level + waveIndex * 2);
                }
                mob.hp = mob.hp_max;
                mob.isdie = false;
                mob.id_target = -1;
                mob.index = mobIndex--;
                mob.map = this.currentMap;
                mob.boss_info = null;
                this.mobs.add(mob);
                waveMobs.add(mob);
                mobBreachTime.put(mob.index, now + travelMs);
            }
        }

        System.out.println("[NamieDefense] Wave " + (waveIndex + 1) + "/" + TOTAL_WAVES
                + " spawned " + waveMobs.size() + " mobs, travelMs=" + travelMs);
        sendLocalNotice("Đợt tấn công " + (waveIndex + 1) + "/" + TOTAL_WAVES + " đã xuất hiện!");

        // Cập nhật bộ đếm thời gian hiển thị Tầng hiện tại cho tất cả người chơi
        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                    && pOnline.map.equals(this.currentMap)) {
                try {
                    Service.send_time_cool_down(pOnline, this.dungeonEndTime, "Tầng " + (waveIndex + 1), 2);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private int mapTemplateSafeMaxW() {
        try {
            return this.currentMap.template.maxW - 20;
        } catch (Exception e) {
            return 600;
        }
    }

    private int mapTemplateSafeMaxH() {
        try {
            return this.currentMap.template.maxH - 20;
        } catch (Exception e) {
            return 400;
        }
    }

    // ================== VÒNG LẶP CHÍNH (gọi từ Map.update_map_dungeon)
    // ==================

    public synchronized void update(Map map) throws IOException {
        if (this.finished) {
            return;
        }
        if (!map.equals(this.currentMap)) {
            return;
        }

        // 1. Giới hạn thời gian toàn phó bản
        if (System.currentTimeMillis() > this.dungeonEndTime) {
            if (this.checkG != null && this.checkG.contains(-2)) {
                failDefense("Tất cả thành viên trong tổ đội đã tử trận!");
            } else {
                failDefense("Đã hết thời gian bảo vệ kho báu!");
            }
            return;
        }

        if (this.checkG != null && this.checkG.contains(-2)) {
            return;
        }

        // 2. Không còn ai online trong map -> huỷ (sau 10 giây grace period)
        if (System.currentTimeMillis() - this.dungeonStartTime > 10000L) {
            boolean anyOnline = false;
            for (Player member : this.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                        && pOnline.map.equals(this.currentMap)) {
                    anyOnline = true;
                    break;
                }
            }
            if (!anyOnline) {
                failDefense("Tất cả người chơi đã rời khỏi phó bản.");
                return;
            }
        }

        // 3. Xử lý các mob "áp sát" rương (chưa bị giết kịp thời)
        long now = System.currentTimeMillis();
        List<Integer> resolved = new ArrayList<>();
        for (java.util.Map.Entry<Integer, Long> entry : mobBreachTime.entrySet()) {
            int idx = entry.getKey();
            Mob mob = findMobByIndex(idx);
            if (mob == null || mob.isdie) {
                resolved.add(idx);
                continue;
            }
            if (now >= entry.getValue()) {
                // Quân địch áp sát thành công -> gây sát thương rương, biến mất khỏi bản đồ
                this.treasureHp = Math.max(0, this.treasureHp - BREACH_DAMAGE);
                mob.isdie = true;
                try {
                    map.remove_obj(mob.index, 1);
                } catch (Exception e) {
                    e.printStackTrace();
                }
                resolved.add(idx);
                sendLocalNotice("Kho báu bị tập kích! Rương mất " + BREACH_DAMAGE
                        + " máu (còn " + treasurePercent() + "%)");
                if (this.treasureHp <= 0) {
                    failDefense("Kho báu Namie đã bị cướp mất!");
                    return;
                }
            }
        }
        for (int idx : resolved) {
            mobBreachTime.remove(idx);
        }

        // 4. Chuyển đợt nếu đang trong thời gian chờ
        if (this.isTransitioning) {
            if (now >= this.transitionEndTime) {
                completeWaveTransition();
            }
            return;
        }

        // 5. Kiểm tra đợt hiện tại đã sạch quái chưa (chết hết hoặc áp sát hết)
        int aliveMobs = 0;
        for (Mob mob : this.mobs) {
            if (mob.map.equals(map) && !mob.isdie) {
                aliveMobs++;
            }
        }
        if (aliveMobs == 0) {
            // không còn quái sống VÀ không còn quái đang chờ áp sát của đợt hiện tại
            boolean stillWaitingBreach = false;
            for (Integer idx : mobBreachTime.keySet()) {
                Mob m = findMobByIndex(idx);
                if (m != null && !m.isdie) {
                    stillWaitingBreach = true;
                    break;
                }
            }
            if (!stillWaitingBreach) {
                if (!waveCleared.compareAndSet(false, true)) {
                    return;
                }
                this.isTransitioning = true;
                this.transitionEndTime = now + WAVE_GAP_MS;
                int waveNum = currentWaveIndex + 1;
                sendLocalNotice("Đã đẩy lui đợt " + waveNum + "/" + TOTAL_WAVES + "!");
                if (waveNum == 5 || waveNum == 10 || waveNum == 15 || waveNum == 20) {
                    giveWaveReward(waveNum);
                }
                for (Player member : this.partyMembers) {
                    Player pOnline = Map.get_player_by_name_allmap(member.name);
                    if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                            && pOnline.map.equals(this.currentMap)) {
                        try {
                            Service.send_eff(pOnline, 23, 0);
                        } catch (Exception e) {
                            // ignore
                        }
                    }
                }
                this.currentWaveFuture = WAVE_SCHEDULER.schedule(() -> {
                    try {
                        if (!this.finished && this.isTransitioning) {
                            completeWaveTransition();
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }, WAVE_GAP_MS, TimeUnit.MILLISECONDS);
            }
        }
    }

    private synchronized void completeWaveTransition() {
        if (!this.isTransitioning) {
            return;
        }
        this.isTransitioning = false;
        this.transitionEndTime = 0;
        if (this.currentWaveFuture != null) {
            this.currentWaveFuture.cancel(false);
            this.currentWaveFuture = null;
        }
        spawnWave(this.currentWaveIndex + 1);
    }

    private Mob findMobByIndex(int index) {
        for (Mob mob : this.mobs) {
            if (mob.index == index) {
                return mob;
            }
        }
        return null;
    }

    public int treasurePercent() {
        return (int) Math.round(100.0 * treasureHp / TREASURE_HP_MAX);
    }

    /**
     * Gọi khi player hạ gục 1 mob trong phó bản này (hook từ nơi xử lý mob chết,
     * xem ghi chú tích hợp).
     */
    public synchronized void onMobKilled(Player killer, Mob mob) {
        if (killer == null || mob == null) {
            return;
        }
        mobBreachTime.remove(mob.index);
        killCount.merge(killer.name, 1, Integer::sum);
    }

    // ================== KẾT THÚC PHÓ BẢN ==================

    public synchronized void completeDefense() {
        if (this.finished) {
            return;
        }
        this.finished = true;
        this.active = false;

        int stars = treasureHp >= TREASURE_HP_MAX * 0.8 ? 3
                : treasureHp >= TREASURE_HP_MAX * 0.4 ? 2
                        : 1;

        System.out.println("[NamieDefense] Completed! treasureHp=" + treasureHp
                + "/" + TREASURE_HP_MAX + " (" + stars + " sao)");

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null) {
                try {
                    Service.send_box_ThongBao_OK(pOnline,
                            "Xin chúc mừng! Bạn đã bảo vệ thành công kho báu Namie (" + stars
                                    + " sao, còn " + treasurePercent() + "% máu rương)!");
                } catch (IOException e) {
                    e.printStackTrace();
                }
                giveRewards(pOnline, stars);
                teleportBack(pOnline);
            }
        }
        cleanup();
    }

    public synchronized void failDefense(String reason) {
        if (this.finished) {
            return;
        }
        this.finished = true;
        this.active = false;

        System.out.println("[NamieDefense] Failed: " + reason);

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null) {
                if (reason != null && !reason.isEmpty()) {
                    try {
                        Service.send_box_ThongBao_OK(pOnline, reason);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
                teleportBack(pOnline);
            }
        }
        cleanup();
    }

    private void giveRewards(Player p, int stars) {
        try {
            List<GiftBox> list_gift = new ArrayList<>();

            int beri = stars == 3 ? 50_000 : stars == 2 ? 30_000 : 15_000;
            ItemTemplate4 it_beri = ItemTemplate4.get_it_by_id(0);
            if (it_beri != null) {
                GiftBox gb = new GiftBox();
                gb.id = it_beri.id;
                gb.type = 4;
                gb.name = it_beri.name;
                gb.icon = it_beri.icon;
                gb.num = beri;
                gb.color = 0;
                list_gift.add(gb);
            }

            int qty = stars == 3 ? 10 : stars == 2 ? 5 : 2;
            int[][] items7 = {
                    { 4, qty }, // Bột vàng
                    { 3, qty }, // Bột tím
                    { 1, qty * 2 }, // Bột cường hoá
            };
            for (int[] item : items7) {
                ItemTemplate7 it7 = ItemTemplate7.get_it_by_id(item[0]);
                if (it7 != null) {
                    GiftBox gb = new GiftBox();
                    gb.id = it7.id;
                    gb.type = 7;
                    gb.name = it7.name;
                    gb.icon = it7.icon;
                    gb.num = item[1];
                    gb.color = 0;
                    list_gift.add(gb);
                }
            }

            if (!list_gift.isEmpty()) {
                Service.send_gift(p, 1, "Bảo vệ kho báu Namie", "Phần thưởng (" + stars + " sao)", list_gift, true);
            }
        } catch (Exception e) {
            System.out.println("[NamieDefense] ERROR giving rewards to " + p.name + ": " + e.getMessage());
            e.printStackTrace();
        }
    }

    private void giveWaveReward(int waveNum) {
        List<GiftBox> list_gift = new ArrayList<>();

        // 1. Bột vàng (Type 7, ID 4)
        ItemTemplate7 temp1 = ItemTemplate7.get_it_by_id(4);
        if (temp1 != null) {
            GiftBox gb = new GiftBox();
            gb.id = 4;
            gb.type = 7;
            gb.name = temp1.name;
            gb.icon = temp1.icon;
            gb.num = 1 + Util.random(10);
            gb.color = 0;
            list_gift.add(gb);
        }

        // 2. Mai rùa (Type 7, ID 6)
        ItemTemplate7 temp2 = ItemTemplate7.get_it_by_id(6);
        if (temp2 != null) {
            GiftBox gb = new GiftBox();
            gb.id = 6;
            gb.type = 7;
            gb.name = temp2.name;
            gb.icon = temp2.icon;
            gb.num = 1 + Util.random(10);
            gb.color = 0;
            list_gift.add(gb);
        }

        // 3. Ngôi sao may mắn (Type 7, ID 5)
        ItemTemplate7 temp3 = ItemTemplate7.get_it_by_id(5);
        if (temp3 != null) {
            GiftBox gb = new GiftBox();
            gb.id = 5;
            gb.type = 7;
            gb.name = temp3.name;
            gb.icon = temp3.icon;
            gb.num = 1 + Util.random(10);
            gb.color = 0;
            list_gift.add(gb);
        }

        // 4. Đá hải thạch cấp 1 (Type 4, ID 221)
        ItemTemplate4 temp4 = ItemTemplate4.get_it_by_id(221);
        if (temp4 != null) {
            GiftBox gb = new GiftBox();
            gb.id = 221;
            gb.type = 4;
            gb.name = temp4.name;
            gb.icon = temp4.icon;
            gb.num = 1 + Util.random(10);
            gb.color = 0;
            list_gift.add(gb);
        }

        if (list_gift.isEmpty()) {
            return;
        }

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                    && pOnline.map.equals(this.currentMap)) {
                try {
                    Service.send_gift(pOnline, 1, "Bảo vệ kho báu Namie",
                            "Thưởng đợt " + waveNum, list_gift, true);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private void teleportBack(Player p) {
        try {
            if (p.isdie) {
                p.isdie = false;
                p.hp = p.body.get_hp_max(true) / 10;
                Service.use_potion(p, 0, p.hp);
            }
            p.dungeon = null;

            int targetMapId = p.originalMapId;
            short targetX = p.originalX;
            short targetY = p.originalY;

            if (targetMapId <= 0 || !p.canGoToMap(targetMapId)) {
                targetMapId = 1;
                targetX = 300;
                targetY = 250;
            }

            Map[] targetMaps = Map.get_map_by_id(targetMapId);
            if (targetMaps == null || targetMaps.length == 0 || targetMaps[0] == null) {
                targetMapId = 1;
                targetX = 300;
                targetY = 250;
                targetMaps = Map.get_map_by_id(1);
            }

            Vgo vgo = new Vgo();
            vgo.map_go = targetMaps;
            vgo.xnew = targetX;
            vgo.ynew = targetY;
            p.goto_map(vgo);

            Service.send_time_cool_down(p, 0, "", 0);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public synchronized void handlePlayerLeftParty(Player p) {
        this.partyMembers.remove(p);
        teleportBack(p);
        if (this.partyMembers.isEmpty()) {
            failDefense("Tất cả thành viên đã rời nhóm, phó bản kết thúc.");
        }
    }

    public synchronized void cleanup() {
        if (this.currentWaveFuture != null) {
            this.currentWaveFuture.cancel(false);
            this.currentWaveFuture = null;
        }
        if (this.currentMap != null) {
            this.currentMap.running = false;
            Map.remove_map_plus(this.currentMap);
            this.currentMap.map_dungeon = null;
        }
        if (this.maps != null) {
            for (Map m : this.maps) {
                m.running = false;
                Map.remove_map_plus(m);
                m.map_dungeon = null;
            }
            this.maps.clear();
        }
        if (this.mobs != null) {
            for (Mob mob : this.mobs) {
                mob.map = null;
                mob.mob_template = null;
            }
            this.mobs.clear();
        }
        mobBreachTime.clear();
        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.dungeon == this) {
                pOnline.dungeon = null;
                try {
                    Service.send_time_cool_down(pOnline, 0, "", 0);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        this.partyMembers.clear();
        ACTIVE_DEFENSES.remove(this);
    }

    private void sendLocalNotice(String text) {
        try {
            Message m = new Message(-31);
            m.writer().writeByte(0);
            m.writer().writeUTF(text);
            m.writer().writeByte(5);
            m.writer().writeShort(-1);
            for (Player member : this.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.conn != null && pOnline.conn.connected) {
                    pOnline.conn.addmsg(m);
                }
            }
            m.cleanup();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
