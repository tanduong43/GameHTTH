package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.stream.Collectors;

import client.Player;
import core.Service;
import io.Message;
import map.Boss;
import map.Map;
import map.Mob;
import map.Vgo;
import template.GiftBox;
import template.ItemTemplate4;
import template.ItemTemplate7;

//Vượt liên tầng (13 tầng)
public class TowerChallenge extends Dungeon {
    public static final List<TowerChallenge> ACTIVE_CHALLENGES = new CopyOnWriteArrayList<>();
    public static final java.util.Map<client.Party, TowerChallengeLobby> ACTIVE_LOBBIES = new java.util.concurrent.ConcurrentHashMap<>();
    private static final ScheduledExecutorService TRANSITION_SCHEDULER = Executors.newScheduledThreadPool(2, r -> {
        Thread t = new Thread(r, "TowerChallenge-Transition");
        t.setDaemon(true);
        return t;
    });

    public List<Player> partyMembers = new ArrayList<>();
    public Player leader;
    public int currentStageIndex = 0; // 0 to 12
    public long stageEndTime;
    public Map currentMap;
    public boolean active = false;
    public boolean finished = false;
    public boolean isTransitioning = false;
    public long transitionEndTime = 0;
    public Mob stageBoss = null; // Boss mob của tầng hiện tại
    public java.util.Map<String, Integer> playerLastRewardedStage = new java.util.HashMap<>();
    // FIX #1: AtomicBoolean đảm bảo chỉ 1 luồng trigger chuyển tầng (compareAndSet)
    private final AtomicBoolean stageCleared = new AtomicBoolean(false);
    // FIX #4: Giữ tham chiếu task đang lên lịch để cancel khi cần
    private volatile ScheduledFuture<?> currentTransitionFuture = null;

    public TowerChallenge(List<Player> members, Player leader) {
        this.partyMembers.addAll(members);
        this.leader = leader;
        this.mode = 7;
        ACTIVE_CHALLENGES.add(this);
        System.out.println("[TowerChallenge] Instantiated challenge for leader: " + leader.name 
            + " with party size: " + members.size() 
            + " (Members: " + members.stream().map(p -> p.name).collect(Collectors.joining(", ")) + ")");
    }

    public static TowerChallenge findActiveChallenge(String name) {
        for (TowerChallenge tc : ACTIVE_CHALLENGES) {
            if (tc.active && !tc.finished) {
                for (Player p : tc.partyMembers) {
                    if (p.name.equals(name)) {
                        return tc;
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

    public synchronized void createStage(int stageIndex) {
        if (stageIndex < 0 || stageIndex > 12) {
            completeDungeon();
            return;
        }

        this.currentStageIndex = stageIndex;
        this.isTransitioning = false;
        this.transitionEndTime = 0;
        // FIX #2: Reset flag atomic cho tầng mới trước khi spawn mob
        this.stageCleared.set(false);
        // FIX #4: Cancel task transition còn sót lại từ tầng trước
        if (this.currentTransitionFuture != null && !this.currentTransitionFuture.isDone()) {
            this.currentTransitionFuture.cancel(false);
            this.currentTransitionFuture = null;
        }
        int mapId = 500 + stageIndex;

        // Clean up previous stage map
        Map oldMap = this.currentMap;
        if (oldMap != null) {
            oldMap.running = false;
            // Defer removing from MAP_PLUS so get_player_by_name_allmap can still find players
        }

        Map[] templates = Map.get_map_by_id(mapId);
        if (templates == null || templates.length == 0) {
            // FIX #3: Log rõ ràng hơn — chỉ ra ID nào thiếu để dễ debug
            System.out.println("[TowerChallenge] ERROR: Map template not found for mapId=" + mapId
                    + " (stageIndex=" + stageIndex + ")."
                    + " Verify that map ID " + mapId + " is defined in map data files and loaded at startup.");
            // FIX #3: Dump danh sách MAP_PLUS hiện có để admin biết ID nào đang tồn tại
            try {
                String availableIds = Map.get_map_plus().stream()
                        .map(m -> String.valueOf(m.template.id))
                        .collect(Collectors.joining(", "));
                System.out.println("[TowerChallenge] Current MAP_PLUS IDs: ["
                        + (availableIds.isEmpty() ? "<none>" : availableIds) + "]");
            } catch (Exception logEx) {
                System.out.println("[TowerChallenge] (Could not dump MAP_PLUS IDs: " + logEx.getMessage() + ")");
            }
            // FIX #3: Fallback an toàn — không để player kẹt, thông báo rõ cho người chơi
            failDungeon("Lỗi hệ thống: Dữ liệu tầng " + (stageIndex + 1)
                    + " chưa được cấu hình. Vui lòng liên hệ Admin.");
            return;
        }
        Map mapTemplate = templates[0];

        // Cập nhật hiệu ứng chuyển cảnh cho Client (xoay vòng 1, 2, 3)
        mapTemplate.template.typeChangeMap = (byte) (1 + (stageIndex % 3));

        Map mapDungeon = new Map();
        mapDungeon.template = mapTemplate.template;
        mapDungeon.zone_id = (byte) 0;
        mapDungeon.list_mob = new int[0];

        this.mobs = new ArrayList<>();
        this.stageBoss = null;
        int mobIndex = -2;
        if (mapTemplate.list_mob != null) {
            for (int i = 0; i < mapTemplate.list_mob.length; i++) {
                Mob temp = Mob.ENTRYS.get(mapTemplate.list_mob[i]);
                if (temp != null) {
                    Mob mobAdd = new Mob();
                    mobAdd.mob_template = temp.mob_template;
                    mobAdd.x = temp.x;
                    mobAdd.y = temp.y;
                    // Mob stats scaling by stage
                    mobAdd.hp_max = temp.mob_template.hp_max + stageIndex * 5000;
                    mobAdd.hp = mobAdd.hp_max;
                    mobAdd.level = (short) (temp.mob_template.level + stageIndex * 2);
                    if (mobAdd.level > 100) {
                        mobAdd.level = 100;
                    }
                    mobAdd.isdie = false;
                    mobAdd.id_target = -1;
                    mobAdd.index = mobIndex--;
                    mobAdd.map = mapDungeon;
                    mobAdd.boss_info = null;
                    this.mobs.add(mobAdd);
                }
            }
        }

        // Chọn con mob cuối cùng làm boss tầng, tăng HP x3 (tầng 11, 12, 13 nhân 8) và gắn boss_info
        if (!this.mobs.isEmpty()) {
            Mob bossM = this.mobs.get(this.mobs.size() - 1);
            int multiplier = (stageIndex >= 10) ? 8 : 3;
            bossM.hp_max = bossM.hp_max * multiplier;
            bossM.hp = bossM.hp_max;
            Boss bossInfo = new Boss();
            bossInfo.mob = bossM;
            bossInfo.levelBoss = (byte) (stageIndex + 1);
            bossInfo.TopDame = new ArrayList<>();
            bossM.boss_info = bossInfo;
            this.stageBoss = bossM;
        }

        System.out.println("[TowerChallenge] Spawned " + this.mobs.size() + " monsters (1 boss) for Map ID: " + mapId);

        mapDungeon.start_map();
        mapDungeon.map_dungeon = this;
        mapDungeon.map_dungeon.checkG = new java.util.HashSet<>();

        this.currentMap = mapDungeon;
        this.maps = new ArrayList<>();
        this.maps.add(mapDungeon);

        Map.add_map_plus(mapDungeon);

        this.stageEndTime = System.currentTimeMillis() + 120_000L; // 2 minutes
        this.time = this.stageEndTime;

        Vgo vgo = new Vgo();
        vgo.map_go = new Map[] { mapDungeon };
        vgo.xnew = 350;
        vgo.ynew = 260;

        System.out.println("[TowerChallenge] Transitioning " + this.partyMembers.size() + " members to stageIndex=" + stageIndex + " (Map ID=" + mapId + ")");
        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.conn != null && pOnline.conn.connected) {
                pOnline.dungeon = this;
                System.out.println("[TowerChallenge] Teleporting player " + pOnline.name + " to Map ID: " + mapId);
                try {
                    pOnline.goto_map(vgo);
                    Service.send_time_cool_down(pOnline, this.stageEndTime, "Vượt Liên Ải Tầng " + (stageIndex + 1), 2);
                } catch (IOException e) {
                    System.out.println("[TowerChallenge] ERROR: Teleport failed for player " + pOnline.name + ": " + e.getMessage());
                    e.printStackTrace();
                }
            } else {
                System.out.println("[TowerChallenge] WARNING: Player " + member.name + " is offline/disconnected, cannot teleport.");
            }
        }

        this.active = true;

        if (oldMap != null) {
            Map.remove_map_plus(oldMap);
        }
    }

    public synchronized void update(Map map) throws IOException {
        if (this.finished)
            return;

        // Chỉ xử lý update nếu map gọi tới chính là currentMap của Dungeon
        if (!map.equals(this.currentMap)) {
            return;
        }

        // 1. Timeout Check
        if (System.currentTimeMillis() > this.stageEndTime) {
            System.out.println("[TowerChallenge] Floor timeout reached. Ending dungeon.");
            failDungeon("Hết thời gian vượt ải!");
            return;
        }

        // 2. Active Players Check (fail if all offline/left map)
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
            System.out.println("[TowerChallenge] No active players in currentMap. Ending dungeon.");
            failDungeon("Tất cả người chơi đã rời khỏi phó bản.");
            return;
        }

        // 3. Transition check
        if (this.isTransitioning) {
            if (System.currentTimeMillis() >= this.transitionEndTime) {
                completeStage();
            }
            return;
        }

        // 4. Boss Check - chỉ chuyển tầng khi boss chết
        if (map.equals(this.currentMap)) {
            int aliveMobs = 0;
            for (Mob mob : this.mobs) {
                if (mob.map.equals(map) && !mob.isdie) {
                    aliveMobs++;
                }
            }
            boolean bossDefeated = (this.stageBoss != null && this.stageBoss.isdie);

            System.out.println("[TowerChallenge] Current Map ID: " + (500 + this.currentStageIndex)
                    + " | Remaining Mobs: " + aliveMobs
                    + " | Boss defeated: " + bossDefeated);

            if (bossDefeated && aliveMobs == 0) {
                // FIX #1: compareAndSet đảm bảo CHỈ 1 lần trigger transition dù update() gọi nhiều lần
                // Nếu stageCleared đã là true (set bởi lần gọi trước), bỏ qua
                if (!stageCleared.compareAndSet(false, true)) {
                    return;
                }
                System.out.println("[TowerChallenge] Boss and all mobs defeated on Map ID: " + (500 + this.currentStageIndex)
                        + ". Waiting 5 seconds before transitioning.");
                this.isTransitioning = true;
                this.transitionEndTime = System.currentTimeMillis() + 5000L;

                // Gửi hiệu ứng ăn mừng pháo hoa và thông báo chạy chữ cho tổ đội
                sendLocalNotice("Đã tiêu diệt Boss và dọn sạch quái tầng " + (this.currentStageIndex + 1) + "! Chuẩn bị chuyển tầng...");
                
                // Trao quà ngay lập tức khi hoàn thành tầng để người chơi xem trong thời gian chờ 5s
                distributeCurrentStageRewards();

                for (Player member : this.partyMembers) {
                    Player pOnline = Map.get_player_by_name_allmap(member.name);
                    if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                            && pOnline.map.equals(this.currentMap)) {
                        try {
                            Service.send_eff(pOnline, 23, 0); // Hiệu ứng pháo hoa
                        } catch (Exception e) {
                            // Bỏ qua lỗi
                        }
                    }
                }

                // FIX #4: Lưu ScheduledFuture để có thể cancel khi createStage() tiếp theo được gọi
                // Chủ động lên lịch chuyển tầng, không phụ thuộc vào lần update() kế tiếp
                this.currentTransitionFuture = TRANSITION_SCHEDULER.schedule(() -> {
                    try {
                        if (!this.finished && this.isTransitioning) {
                            completeStage();
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }, 5000L, TimeUnit.MILLISECONDS);
            }
        }
    }

    public synchronized void completeStage() {
        if (!this.isTransitioning) {
            return; // đã được xử lý bởi lời gọi khác rồi
        }
        this.isTransitioning = false;
        this.transitionEndTime = 0;
        // FIX #4: Cancel future ngay khi completeStage() chạy thành công
        // Tránh task cũ thức dậy sau khi tầng mới đã bắt đầu
        if (this.currentTransitionFuture != null) {
            this.currentTransitionFuture.cancel(false);
            this.currentTransitionFuture = null;
        }

        int nextStage = this.currentStageIndex + 1;
        int nextMapId = 500 + nextStage;

        // 1. Teleport to next stage or complete
        if (nextStage >= 13) {
            System.out
                    .println("[TowerChallenge] All monsters defeated on final map (Map ID: 512). Completing dungeon.");
            completeDungeon();
        } else {
            System.out.println("[TowerChallenge] Teleporting to next map. Destination map ID: " + nextMapId);
            createStage(nextStage);
        }
    }

    private void distributeCurrentStageRewards() {
        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.map.equals(this.currentMap)) {
                int lastRewarded = playerLastRewardedStage.getOrDefault(pOnline.name, -1);
                if (lastRewarded < this.currentStageIndex) {
                    giveStageRewards(pOnline, this.currentStageIndex);
                    playerLastRewardedStage.put(pOnline.name, this.currentStageIndex);
                }
            }
        }
    }

    private void giveStageRewards(Player p, int stageIndex) {
        try {
            System.out.println("[TowerChallenge] Distributing stage rewards to player: " + p.name + " for stageIndex=" + stageIndex);
            List<GiftBox> list_gift = new ArrayList<>();
            int rewardMultiplier = (stageIndex >= 7) ? 2 : 1;

            // 1. Beri x10,000
            ItemTemplate4 it_beri = ItemTemplate4.get_it_by_id(0);
            if (it_beri != null) {
                GiftBox gb = new GiftBox();
                gb.id = it_beri.id;
                gb.type = 4;
                gb.name = it_beri.name;
                gb.icon = it_beri.icon;
                gb.num = 10_000 * rewardMultiplier;
                gb.color = 0;
                list_gift.add(gb);
            }

            // 2. Type 7 Items
            int[][] items7 = {
                    { 6, 1 }, // Turtle Shell (ID 6), quant 1
                    { 4, 10 }, // Yellow Powder (ID 4), quant 10
                    { 3, 10 }, // Purple Powder (ID 3), quant 10
                    { 2, 10 }, // Black Powder / Coal Powder (ID 2), quant 10
                    { 1, 20 }, // Enhancement Powder (ID 1), quant 20
                    { 5, 2 } // Lucky Star (ID 5), quant 2
            };

            for (int[] item : items7) {
                ItemTemplate7 it_temp7 = ItemTemplate7.get_it_by_id(item[0]);
                if (it_temp7 != null) {
                    GiftBox gb = new GiftBox();
                    gb.id = it_temp7.id;
                    gb.type = 7;
                    gb.name = it_temp7.name;
                    gb.icon = it_temp7.icon;
                    gb.num = item[1] * rewardMultiplier;
                    gb.color = 0;
                    list_gift.add(gb);
                }
            }

            if (!list_gift.isEmpty()) {
                System.out.println("[TowerChallenge] Sending gift box of " + list_gift.size() + " items to player " + p.name + " via send_gift.");
                Service.send_gift(p, 1, "Vượt ải " + (stageIndex + 1), "Phần thưởng", list_gift, true);
            } else {
                System.out.println("[TowerChallenge] WARNING: Gift list is empty for player " + p.name);
            }
        } catch (Exception e) {
            System.out.println("[TowerChallenge] ERROR: Failed to distribute stage rewards to player " + p.name + ": " + e.getMessage());
            e.printStackTrace();
        }
    }

    public synchronized void completeDungeon() {
        if (this.finished)
            return;
        this.finished = true;
        this.active = false;

        System.out.println("[TowerChallenge] Dungeon completed successfully! Teleporting party members back.");

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null) {
                System.out.println("[TowerChallenge] Congratulating and teleporting back player: " + pOnline.name);
                try {
                    Service.send_box_ThongBao_OK(pOnline, "Xin chúc mừng! Bạn đã hoàn thành Vượt Liên Ải!");
                } catch (IOException e) {
                    System.out.println("[TowerChallenge] ERROR: Failed to send completion notification to player " + pOnline.name + ": " + e.getMessage());
                    e.printStackTrace();
                }
                teleportBack(pOnline);
            } else {
                System.out.println("[TowerChallenge] Member " + member.name + " is offline/disconnected during dungeon completion.");
            }
        }

        cleanup();
    }

    public synchronized void failDungeon(String reason) {
        if (this.finished)
            return;
        this.finished = true;
        this.active = false;

        System.out.println("[TowerChallenge] Dungeon failed! Reason: " + reason + ". Teleporting party members back.");

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null) {
                System.out.println("[TowerChallenge] Notifying and teleporting back player: " + pOnline.name);
                if (reason != null && !reason.isEmpty()) {
                    try {
                        Service.send_box_ThongBao_OK(pOnline, reason);
                    } catch (IOException e) {
                        System.out.println("[TowerChallenge] ERROR: Failed to send failure notification to player " + pOnline.name + ": " + e.getMessage());
                        e.printStackTrace();
                    }
                }
                teleportBack(pOnline);
            } else {
                System.out.println("[TowerChallenge] Member " + member.name + " is offline/disconnected during dungeon failure.");
            }
        }

        cleanup();
    }

    private void teleportBack(Player p) {
        try {
            p.dungeon = null; // Clear dungeon reference before teleport to avoid loops

            int targetMapId = p.originalMapId;
            short targetX = p.originalX;
            short targetY = p.originalY;

            if (targetMapId <= 0 || !p.canGoToMap(targetMapId)) {
                targetMapId = 1;
                targetX = 300;
                targetY = 250;
            }

            System.out.println("[TowerChallenge] Teleporting player " + p.name + " back to registered village/map ID: "
                    + targetMapId);

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

        if (p.name.equals(this.leader.name) || this.partyMembers.isEmpty()) {
            failDungeon("Trưởng nhóm đã rời nhóm hoặc nhóm giải tán.");
        }
    }

    public synchronized void cleanup() {
        System.out.println("[TowerChallenge] Performing dungeon cleanup.");
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

        ACTIVE_CHALLENGES.remove(this);
    }

    private void sendLocalNotice(String text) {
        try {
            Message m = new Message(-31);
            m.writer().writeByte(0); // type 0: Chữ chạy giữa màn hình
            m.writer().writeUTF(text);
            m.writer().writeByte(5); // color 5: Màu vàng sáng
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

    public static class TowerChallengeLobby {
        public client.Party party;
        public Player leader;
        public java.util.Map<String, Boolean> memberAgreement = new java.util.HashMap<>();

        public TowerChallengeLobby(client.Party party, Player leader) {
            this.party = party;
            this.leader = leader;
            for (Player p : party.list) {
                if (p.name.equals(leader.name)) {
                    memberAgreement.put(p.name, true);
                } else {
                    memberAgreement.put(p.name, null);
                }
            }
        }

        public boolean isAllAgreed() {
            for (Boolean b : memberAgreement.values()) {
                if (b == null || !b) {
                    return false;
                }
            }
            return true;
        }

        public String getStatusBoard() {
            StringBuilder sb = new StringBuilder("Vượt Liên Ải - Chuẩn Bị:\n");
            for (Player memInList : party.list) {
                Boolean agreement = memberAgreement.get(memInList.name);
                String status = "[?] Đang chờ";
                if (agreement != null) {
                    status = agreement ? "[v] Đồng ý" : "[x] Từ chối";
                }
                sb.append("- ").append(memInList.name).append(": ").append(status).append("\n");
            }
            return sb.toString();
        }
    }
}
