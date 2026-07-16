package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;

import client.Player;
import core.Service;
import map.Map;
import map.Mob;
import map.Vgo;

public class HangDong extends Dungeon {
    public static final List<HangDong> ACTIVE_HANG_DONG = new CopyOnWriteArrayList<>();

    public List<Player> partyMembers = new ArrayList<>();
    public Player leader;
    public int currentStageIndex = 0; // 0 to 99
    public long stageEndTime;
    public Map currentMap;
    public boolean active = false;
    public boolean finished = false;

    public HangDong(List<Player> members, Player leader) {
        this.partyMembers.addAll(members);
        this.leader = leader;
        ACTIVE_HANG_DONG.add(this);
        this.maps = new ArrayList<>();
        this.mobs = new ArrayList<>();
    }

    public static HangDong findActive(String name) {
        for (HangDong hd : ACTIVE_HANG_DONG) {
            if (hd.active && !hd.finished) {
                for (Player p : hd.partyMembers) {
                    if (p.name.equals(name)) {
                        return hd;
                    }
                }
            }
        }
        return null;
    }

    public void updateMemberReference(String name, Player newRef) {
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

    public void createStage(int stageIndex) {
        if (stageIndex < 0 || stageIndex >= 100) {
            completeDungeon();
            return;
        }

        Map oldMap = this.currentMap;

        this.currentStageIndex = stageIndex;

        // Timeout cho 1 tầng là 10 phút
        this.stageEndTime = System.currentTimeMillis() + 600_000L;
        this.time = this.stageEndTime;

        Map[] mapTemplates = Map.get_map_by_id(167);
        if (mapTemplates == null || mapTemplates.length == 0 || mapTemplates[0] == null) {
            System.err.println("HangDong: Map 167 not found in DB!");
            completeDungeon();
            return;
        }
        Map mapTemplate = mapTemplates[0];

        Map map_dungeon = new Map();
        map_dungeon.template = mapTemplate.template;
        map_dungeon.zone_id = (byte) stageIndex;
        map_dungeon.list_mob = new int[0];
        this.mobs = new ArrayList<>();
        if (this.maps == null) {
            this.maps = new ArrayList<>();
        } else {
            this.maps.clear();
        }
        this.maps.add(map_dungeon);

        // Lấy level cao nhất trong pt
        int maxLevel = 1;
        for (Player p : partyMembers) {
            if (p.level > maxLevel) {
                maxLevel = p.level;
            }
        }

        int index = -1;
        // Tạo 30 quái
        for (int i = 0; i < 30; i++) {
            // Lấy ngẫu nhiên template quái từ 1 đến 100
            int mobTemplateId = 1 + core.Util.random(1, 50); // random mobs
            Mob temp = null;
            for (Mob m : Mob.ENTRYS.values()) {
                if (m.mob_template.mob_id == mobTemplateId) {
                    temp = m;
                    break;
                }
            }
            if (temp == null) {
                temp = Mob.ENTRYS.values().iterator().next();
            }

            Mob mob_add = new Mob();
            mob_add.mob_template = temp.mob_template;
            // Spawm ngẫu nhiên tọa độ
            mob_add.x = (short) core.Util.random(100, 1500);
            mob_add.y = (short) 100; // y có thể điều chỉnh theo map

            mob_add.level = maxLevel;
            mob_add.hp_max = 5000 + (maxLevel * 1000);
            mob_add.hp = mob_add.hp_max;

            mob_add.isdie = false;
            mob_add.id_target = -1;
            mob_add.index = index--;
            mob_add.map = map_dungeon;
            mob_add.boss_info = null;
            this.mobs.add(mob_add);
        }

        map_dungeon.start_map();
        map_dungeon.map_dungeon = this;
        Map.add_map_plus(map_dungeon);
        this.currentMap = map_dungeon;
        this.active = true;

        if (oldMap != null) {
            oldMap.running = false;
            Map.remove_map_plus(oldMap);
            oldMap.map_dungeon = null;
        }

        Vgo vgo = new Vgo();
        vgo.map_go = new Map[] { this.currentMap };
        vgo.xnew = 100;
        vgo.ynew = 100;

        for (Player p : partyMembers) {
            if (p != null && p.conn != null && p.conn.connected) {
                p.dungeon = this;
                try {
                    p.goto_map(vgo);
                    Service.send_box_ThongBao_OK(p, "Bạn đã vào Hang động tầng " + (stageIndex + 1));
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public synchronized void checkTransition() {
        if (!active || finished)
            return;

        boolean allDead = true;
        for (Mob mob : this.mobs) {
            if (!mob.isdie) {
                allDead = false;
                break;
            }
        }

        if (allDead) {
            for (Player p : partyMembers) {
                if (p != null && p.conn != null && p.conn.connected && p.map.equals(currentMap)) {
                    try {
                        Service.send_box_ThongBao_OK(p, "Chuyển tầng tiếp theo...");
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
            createStage(currentStageIndex + 1);
        }
    }

    public void completeDungeon() {
        this.finished = true;
        this.active = false;
        ACTIVE_HANG_DONG.remove(this);

        // Clean up maps and mobs to avoid memory leaks
        if (this.currentMap != null) {
            this.currentMap.running = false;
            Map.remove_map_plus(this.currentMap);
            this.currentMap.map_dungeon = null;
            this.currentMap = null;
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

        for (Player p : partyMembers) {
            if (p != null && p.conn != null && p.conn.connected) {
                p.dungeon = null;
                try {
                    Service.send_box_ThongBao_OK(p, "Hang động kết thúc. Trở về làng.");
                    Map[] targetMaps = Map.get_map_by_id(1);
                    if (targetMaps != null && targetMaps.length > 0) {
                        Vgo vgo = new Vgo();
                        vgo.map_go = targetMaps;
                        vgo.xnew = 300;
                        vgo.ynew = 250;
                        p.goto_map(vgo);
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public synchronized void update(Map map) throws IOException {
        if (this.finished)
            return;

        // Only update if map is the current Map of HangDong
        if (!map.equals(this.currentMap)) {
            return;
        }

        // 1. Timeout Check (10 minutes)
        if (System.currentTimeMillis() > this.stageEndTime) {
            for (Player p : partyMembers) {
                if (p != null && p.conn != null && p.conn.connected) {
                    Service.send_box_ThongBao_OK(p, "Hết thời gian vượt ải!");
                }
            }
            completeDungeon();
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
            completeDungeon();
            return;
        }
    }

    public synchronized void handlePlayerLeftParty(Player p) {
        this.partyMembers.remove(p);
        p.dungeon = null;

        // Teleport the player back to map 1
        try {
            Map[] targetMaps = Map.get_map_by_id(1);
            if (targetMaps != null && targetMaps.length > 0) {
                Vgo vgo = new Vgo();
                vgo.map_go = targetMaps;
                vgo.xnew = 300;
                vgo.ynew = 250;
                p.goto_map(vgo);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        if (partyMembers.isEmpty()) {
            completeDungeon();
        } else if (p.name.equals(this.leader.name)) {
            // If the leader leaves, complete/fail the dungeon
            completeDungeon();
        }
    }

    public void playerDisconnected(Player p) {
        partyMembers.remove(p);
        if (partyMembers.isEmpty()) {
            completeDungeon();
        }
    }
}
