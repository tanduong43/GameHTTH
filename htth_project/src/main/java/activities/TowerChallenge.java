package activities;

import client.Player;
import map.Map;
import map.Mob;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;
import core.Service;
import map.Vgo;
import template.GiftBox;
import template.ItemTemplate7;
import template.ItemTemplate4;
import core.Util;

public class TowerChallenge extends Dungeon {
    public static final List<TowerChallenge> ACTIVE_CHALLENGES = new CopyOnWriteArrayList<>();

    public List<Player> partyMembers = new ArrayList<>();
    public Player leader;
    public int currentStageIndex = 0; // 0 to 12
    public long stageEndTime;
    public Map currentMap;
    public boolean active = false;
    public boolean finished = false;
    public java.util.Map<String, Integer> playerLastRewardedStage = new java.util.HashMap<>();

    public TowerChallenge(List<Player> members, Player leader) {
        this.partyMembers.addAll(members);
        this.leader = leader;
        this.mode = 7;
        ACTIVE_CHALLENGES.add(this);
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
        int mapId = 500 + stageIndex;

        // Clean up previous stage map
        if (this.currentMap != null) {
            this.currentMap.running = false;
            Map.remove_map_plus(this.currentMap);
        }

        Map mapTemplate = Map.get_map_by_id(mapId)[0];
        Map mapDungeon = new Map();
        mapDungeon.template = mapTemplate.template;
        mapDungeon.zone_id = (byte) 0;
        mapDungeon.list_mob = new int[0];

        this.mobs = new ArrayList<>();
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

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.conn != null && pOnline.conn.connected) {
                pOnline.dungeon = this;
                try {
                    pOnline.goto_map(vgo);
                    Service.send_time_cool_down(pOnline, this.stageEndTime, "Vượt Liên Ải Ải " + (stageIndex + 1), 2);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

        this.active = true;
    }

    public synchronized void update(Map map) throws IOException {
        if (this.finished) return;

        // 1. Timeout Check
        if (System.currentTimeMillis() > this.stageEndTime) {
            failDungeon("Hết thời gian vượt ải!");
            return;
        }

        // 2. Active Players Check (fail if all offline/left map)
        boolean anyOnline = false;
        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.conn != null && pOnline.conn.connected && pOnline.map.equals(this.currentMap)) {
                anyOnline = true;
                break;
            }
        }
        if (!anyOnline) {
            failDungeon("Tất cả người chơi đã rời khỏi phó bản.");
            return;
        }

        // 3. Stage Mobs Check
        int aliveMobs = 0;
        for (Mob mob : this.mobs) {
            if (mob.map.equals(map) && !mob.isdie) {
                aliveMobs++;
            }
        }

        if (aliveMobs == 0) {
            completeStage();
        }
    }

    public synchronized void completeStage() {
        int nextStage = this.currentStageIndex + 1;

        // 1. Distribute rewards to eligible party members
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

        // 2. Teleport to next stage or complete
        if (nextStage >= 13) {
            completeDungeon();
        } else {
            createStage(nextStage);
        }
    }

    private void giveStageRewards(Player p, int stageIndex) {
        try {
            // Beri x10,000
            p.update_vang(10_000);
            p.update_money();

            // Type 7 Items
            p.item.add_item_bag47(7, 6, 1);  // Turtle Shell (ID 6)
            p.item.add_item_bag47(7, 4, 10); // Yellow Powder (ID 4)
            p.item.add_item_bag47(7, 3, 10); // Purple Powder (ID 3)
            p.item.add_item_bag47(7, 2, 10); // Black Powder / Coal Powder (ID 2)
            p.item.add_item_bag47(7, 1, 20); // Enhancement Powder (ID 1)
            p.item.add_item_bag47(7, 5, 2);  // Lucky Star (ID 5)

            p.item.update_Inventory(4, true);

            Service.send_box_ThongBao_OK(p, "Bạn nhận được phần thưởng vượt ải " + (stageIndex + 1) + "!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public synchronized void completeDungeon() {
        if (this.finished) return;
        this.finished = true;
        this.active = false;

        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null) {
                try {
                    Service.send_box_ThongBao_OK(pOnline, "Xin chúc mừng! Bạn đã hoàn thành Vượt Liên Ải!");
                } catch (IOException e) {
                    e.printStackTrace();
                }
                teleportBack(pOnline);
            }
        }

        cleanup();
    }

    public synchronized void failDungeon(String reason) {
        if (this.finished) return;
        this.finished = true;
        this.active = false;

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
}
