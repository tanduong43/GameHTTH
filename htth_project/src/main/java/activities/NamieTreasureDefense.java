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
import event.EventTrungThu;

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

    /** ID bản đồ instance dùng riêng cho phó bản này (clone từ map 45 - Đại bản doanh). */
    public static final int MAP_ID = 62;

    /** Toạ độ đặt rương kho báu (điểm mà quân địch nhắm tới). */
    public static final short TREASURE_X = 540;
    public static final short TREASURE_Y = 260;

    /** Tổng số đợt tấn công. */
    public static final int TOTAL_WAVES = 20;

    /** Máu tối đa của rương kho báu. */
    public static final int TREASURE_HP_MAX = 1000;

    /** Thời gian nghỉ giữa đợt đầu tiên (ms). */
    public static final long WAVE_GAP_MS_FIRST = 11_000L;
    /** Thời gian nghỉ giữa các đợt tiếp theo (ms). */
    public static final long WAVE_GAP_MS = 5_000L;

    /**
     * Giới hạn tổng thời gian toàn bộ phó bản (an toàn, tránh treo vô thời hạn).
     */
    public static final long TOTAL_TIME_LIMIT_MS = 20 * 60_000L;

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

    public long lastTreasureDamageTime = 0;
    public int partyMaxLevel = 1;
    public int partyMaxDamage = 10;

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
        
        for (Player p : members) {
            Player pOnline = Map.get_player_by_name_allmap(p.name);
            if (pOnline != null) {
                if (pOnline.level > this.partyMaxLevel) {
                    this.partyMaxLevel = pOnline.level;
                }
                int pDame = pOnline.body.get_dame(true);
                if (pDame > this.partyMaxDamage) {
                    this.partyMaxDamage = pDame;
                }
            }
        }
        
        ACTIVE_DEFENSES.add(this);
        System.out.println("[NamieDefense] Created for leader " + leader.name
                + ", party size=" + members.size() + ", maxLv=" + this.partyMaxLevel + ", maxDame=" + this.partyMaxDamage);
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
        sendLocalNotice("Hải quân đang tiến đến! Đợt 1 sẽ bắt đầu sau 11 giây.");
        
        this.isTransitioning = true;
        this.transitionEndTime = System.currentTimeMillis() + WAVE_GAP_MS_FIRST;
        this.currentWaveFuture = WAVE_SCHEDULER.schedule(() -> {
            try {
                if (!this.finished && this.isTransitioning) {
                    completeWaveTransition();
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }, WAVE_GAP_MS_FIRST, TimeUnit.MILLISECONDS);
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

        long now = System.currentTimeMillis();
        int mobIndex = -2 - (waveIndex * 100); // tách biệt index giữa các đợt để tránh trùng
        List<Mob> waveMobs = new ArrayList<>();

        // Select a random lane
        int[][] lanes = {
            {60, 205},
            {60, 335},
            {1050, 205},
            {1050, 335}
        };
        int laneIndex = Util.random(lanes.length);
        int spawnX = lanes[laneIndex][0];
        int spawnY = lanes[laneIndex][1];
        String[] laneNames = {
            "Đường trên bên trái",
            "Đường dưới bên trái",
            "Đường trên bên phải",
            "Đường dưới bên phải"
        };
        String laneName = laneNames[laneIndex];

        int templateId = (waveIndex == 19) ? 36 : 5; 
        MobTemplate mt = MobTemplate.ENTRYS.get(templateId);

        if (mt != null) {
            for (int i = 0; i < 10; i++) {
                Mob mob = new Mob();
                mob.mob_template = mt;
                mob.x = (short) (spawnX + Util.random(-20, 20));
                mob.y = (short) (spawnY + Util.random(-20, 20));
                
                int baseHp = this.partyMaxDamage * 6;
                mob.hp_max = (int)(baseHp + baseHp * 0.2 * waveIndex);
                mob.hp = mob.hp_max;
                mob.level = (short) this.partyMaxLevel;
                mob.isdie = false;
                mob.id_target = -1;
                mob.index = mobIndex--;
                mob.map = this.currentMap;
                mob.boss_info = null;
                this.mobs.add(mob);
                waveMobs.add(mob);
            }
        }

        System.out.println("[NamieDefense] Wave " + (waveIndex + 1) + "/" + TOTAL_WAVES
                + " spawned 10 mobs at " + laneName);
        sendLocalNotice("Quái vật đang tiến đến từ " + laneName + "!");

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

        long now = System.currentTimeMillis();

        // 3. Chuyển đợt nếu đang trong thời gian chờ (hồi sinh và hồi máu)
        if (this.isTransitioning) {
            for (Player member : this.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                        && pOnline.map.equals(this.currentMap)) {
                    if (pOnline.isdie) {
                        pOnline.isdie = false;
                        pOnline.hp = pOnline.body.get_hp_max(true);
                        pOnline.mp = pOnline.body.get_mp_max(true);
                        try {
                            Service.use_potion(pOnline, 0, pOnline.hp);
                            Service.use_potion(pOnline, 1, pOnline.mp);
                        } catch (Exception ignored) { }
                    } else if (pOnline.hp < pOnline.body.get_hp_max(true) || pOnline.mp < pOnline.body.get_mp_max(true)) {
                        pOnline.hp = pOnline.body.get_hp_max(true);
                        pOnline.mp = pOnline.body.get_mp_max(true);
                        try {
                            Service.use_potion(pOnline, 0, pOnline.hp);
                            Service.use_potion(pOnline, 1, pOnline.mp);
                        } catch (Exception ignored) { }
                    }
                }
            }

            if (now >= this.transitionEndTime) {
                completeWaveTransition();
            }
            return;
        }

        // 4. Kiểm tra đợt hiện tại đã sạch quái chưa
        int aliveMobs = 0;
        int mobsAtTreasure = 0;
        for (Mob mob : this.mobs) {
            if (mob.map.equals(map) && !mob.isdie) {
                aliveMobs++;
                if (Math.abs(mob.x - 540) <= 60 && Math.abs(mob.y - 260) <= 60) {
                    mobsAtTreasure++;
                }
            }
        }

        // 5. Trừ máu nhà nếu có quái đến gần
        if (mobsAtTreasure > 0 && now - this.lastTreasureDamageTime >= 2500L) {
            this.lastTreasureDamageTime = now;
            this.treasureHp = Math.max(0, this.treasureHp - mobsAtTreasure);
            sendLocalNotice("Kho báu đang bị tấn công! Bị trừ " + mobsAtTreasure + " HP (Còn " + this.treasureHp + " HP)");
            if (this.treasureHp <= 0) {
                failDefense("Kho báu Namie đã bị cướp mất!");
                return;
            }
        }

        // 6. Xong wave nếu quái chết hết
        if (aliveMobs == 0) {
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
                        Service.send_time_cool_down(pOnline, this.transitionEndTime, "Đợt tiếp theo", 2);
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

        System.out.println("[NamieDefense] Completed! treasureHp=" + treasureHp
                + "/" + TREASURE_HP_MAX);

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null) {
                try {
                    Service.send_box_ThongBao_OK(pOnline,
                            "Xin chúc mừng! Bạn đã bảo vệ thành công kho báu Namie (còn " + treasurePercent() + "% máu rương)! Sẽ tự động thoát sau 10 giây.");
                } catch (IOException e) {
                    e.printStackTrace();
                }
                giveRewards(pOnline, 3);
                // Event Trung Thu: Thưởng Đèn Ông Sao khi hoàn thành Phó Bản Nami
                if (EventTrungThu.isEvent()) {
                    EventTrungThu.rewardPhongThachNami(pOnline);
                }
                // Event 20/11: Thưởng Giấy Gói Quà & Gấu Bông khi hoàn thành Phó Bản Nami
                if (event.Event2011.isEvent()) {
                    event.Event2011.rewardDungeon(pOnline);
                }
            }
        }
        
        WAVE_SCHEDULER.schedule(() -> {
            for (Player member : this.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.dungeon == this) {
                    teleportBack(pOnline);
                }
            }
            cleanup();
        }, 10, TimeUnit.SECONDS);
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
                        Service.send_box_ThongBao_OK(pOnline, reason + "\nSẽ tự động thoát sau 10 giây.");
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
        
        WAVE_SCHEDULER.schedule(() -> {
            for (Player member : this.partyMembers) {
                Player pOnline = Map.get_player_by_name_allmap(member.name);
                if (pOnline != null && pOnline.dungeon == this) {
                    teleportBack(pOnline);
                }
            }
            cleanup();
        }, 10, TimeUnit.SECONDS);
    }

    private void giveRewards(Player p, int stars) {
        try {
            List<GiftBox> list_gift = new ArrayList<>();

            int beri = 100_000 + Util.random(200_000);
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

            if (Util.random(100) < 30) {
                ItemTemplate7 it7 = ItemTemplate7.get_it_by_id(158);
                if (it7 != null) {
                    GiftBox gb = new GiftBox();
                    gb.id = it7.id;
                    gb.type = 7;
                    gb.name = it7.name;
                    gb.icon = it7.icon;
                    gb.num = 1;
                    gb.color = 0;
                    list_gift.add(gb);
                }
            }

            if (!list_gift.isEmpty()) {
                Service.send_gift(p, 1, "Bảo vệ kho báu Namie", "Phần thưởng chiến thắng", list_gift, true);
                if (p.daily_achievements[2] == 0) {
                    p.daily_achievements[2] = 1;
                    Service.send_box_ThongBao_OK(p, "Hoàn thành Thành tích hằng ngày: Bảo vệ Namie");
                }
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
                targetMapId = p.id_map_save > 0 ? p.id_map_save : 41;
                targetX = 300;
                targetY = 250;
            }

            Map[] targetMaps = Map.get_map_by_id(targetMapId);
            if (targetMaps == null || targetMaps.length == 0 || targetMaps[0] == null) {
                targetMapId = 41;
                targetX = 300;
                targetY = 250;
                targetMaps = Map.get_map_by_id(41);
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
        this.partyMembers.removeIf(m -> m.name.equals(p.name));
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
