package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.CopyOnWriteArrayList;

import client.Player;
import core.Service;
import core.Util;
import map.Mob;
import map.Vgo;
import template.ItemTemplate4;
import template.MobTemplate;

public class BossHunt {
    public static final List<BossHunt> ACTIVE_HUNTS = new CopyOnWriteArrayList<>();
    public static final int[] BOSS_MAPS = { 201, 202, 203, 204, 205, 206, 207 };

    public static BossHunt findActiveHunt(String playerName) {
        for (BossHunt hunt : ACTIVE_HUNTS) {
            if (hunt.active || hunt.waitingForReady) {
                for (Player member : hunt.members) {
                    if (member.name.equals(playerName)) {
                        return hunt;
                    }
                }
            }
        }
        return null;
    }

    public static boolean isBossHuntMap(int mapId) {
        for (int id : BOSS_MAPS) {
            if (id == mapId) {
                return true;
            }
        }
        return false;
    }

    public List<map.Map> maps = new ArrayList<>();
    public List<Mob> mobs = new ArrayList<>();
    public List<Player> members = new ArrayList<>();
    public Player leader;
    public int currentFloor = 0;
    public long floorTime = 0;
    public boolean active = false;
    public long transitionTime = 0;
    public boolean isTransitioning = false;
    public int registeredMapId = 1;

    // Ready check state
    public Map<String, Boolean> readyState = new HashMap<>();
    public boolean waitingForReady = false;

    public synchronized void create(Player leader) throws IOException {
        this.leader = leader;
        this.registeredMapId = leader.map.template.id;
        this.members.clear();
        ACTIVE_HUNTS.add(this);
        if (leader.party != null) {
            for (Player p : leader.party.list) {
                this.members.add(p);
                p.bossHunt = this;
                this.readyState.put(p.name, p.name.equals(leader.name)); // Leader is automatically ready
            }
        } else {
            this.members.add(leader);
            leader.bossHunt = this;
            this.readyState.put(leader.name, true);
        }

        this.waitingForReady = true;

        // Send ready check prompt to all other members
        for (Player p : members) {
            if (!p.name.equals(leader.name)) {
                Service.send_box_yesno(p, 54, "Săn Trùm",
                        "Trưởng nhóm bắt đầu Săn Trùm. Bạn đã sẵn sàng chưa?",
                        new String[] { "Sẵn sàng", "Hủy" }, new byte[] { 2, 1 });
            } else {
                Service.send_box_ThongBao_OK(p, "Đang đợi đồng đội sẵn sàng...");
            }
        }
    }

    public synchronized void setReady(Player p, boolean ready) throws IOException {
        if (!waitingForReady)
            return;

        this.readyState.put(p.name, ready);
        if (!ready) {
            // Cancel room
            cancelRoom(p.name + " đã từ chối tham gia Săn Trùm.");
            return;
        }

        // Notify leader/members
        for (Player member : members) {
            if (member.conn != null) {
                Service.send_box_ThongBao_OK(member, p.name + " đã sẵn sàng.");
            }
        }

        // Check if everyone is ready
        boolean allReady = true;
        for (Player member : members) {
            Boolean isReady = readyState.get(member.name);
            if (isReady == null || !isReady) {
                allReady = false;
                break;
            }
        }

        if (allReady) {
            start();
        }
    }

    public synchronized void cancelRoom(String msg) throws IOException {
        ACTIVE_HUNTS.remove(this);
        this.waitingForReady = false;
        this.active = false;
        for (Player member : members) {
            member.bossHunt = null;
            if (member.conn != null) {
                Service.send_box_ThongBao_OK(member, msg);
            }
        }
    }

    public synchronized void start() throws IOException {
        if (members.size() < 2) {
            cancelRoom("Không đủ 2 người chơi để bắt đầu Săn Trùm.");
            return;
        }
        for (Player member : members) {
            if (member.get_key_boss() < 2) {
                cancelRoom("Thành viên " + member.name + " không đủ 2 chìa khóa phó bản để bắt đầu.");
                return;
            }
        }
        for (Player member : members) {
            member.update_key_boss(-2);
            Service.CountDown_Ticket(member);
        }

        System.out.println("[BossHunt] Starting hunt with " + members.size() + " members. 2 keys deducted from all members.");
        this.waitingForReady = false;
        this.active = true;
        this.currentFloor = 0;
        this.maps.clear();
        this.mobs.clear();
        startFloor(0);
    }

    public synchronized void startFloor(int floor) throws IOException {
        this.currentFloor = floor;
        this.floorTime = System.currentTimeMillis() + 120_000L; // 2 minutes countdown

        int mapId = BOSS_MAPS[floor];
        System.out.println("[BossHunt] Starting floor " + (floor + 1)
                + " | mapId=" + mapId + " | members=" + members.size());
        map.Map[] templates = map.Map.get_map_by_id(mapId);
        if (templates == null || templates.length == 0) {
            System.out.println("[BossHunt] ERROR: Map template not found for id=" + mapId);
            cancelRoom("Lỗi: Không tìm thấy map template " + mapId);
            return;
        }
        map.Map mapTemplate = templates[0];

        map.Map map_instance = new map.Map();
        map_instance.template = mapTemplate.template;
        map_instance.zone_id = (byte) 0;
        map_instance.list_mob = new int[0];
        map_instance.map_bossHunt = this;

        Mob originalMob = null;
        if (mapTemplate.list_mob != null && mapTemplate.list_mob.length > 0) {
            originalMob = Mob.ENTRYS.get(mapTemplate.list_mob[0]);
        }

        if (originalMob == null) {
            System.out.println("[BossHunt] WARNING: No mob template found for map " + mapId + ", using fallback.");
            originalMob = new Mob();
            originalMob.mob_template = MobTemplate.ENTRYS.get(1); // default fallback
            originalMob.x = 300;
            originalMob.y = 300;
            originalMob.level = 50;
            originalMob.hp_max = 500_000;
        }

        Mob boss = new Mob();
        boss.mob_template = originalMob.mob_template;
        boss.x = originalMob.x;
        boss.y = originalMob.y;

        long baseHp = originalMob.mob_template.hp_max > 0 ? originalMob.mob_template.hp_max : 100_000L;
        int floorNum = floor + 1;
        boss.hp_max = (int) (baseHp * floorNum);
        boss.hp = boss.hp_max;
        boss.level = originalMob.level + (floor * 5);
        if (boss.level > 100)
            boss.level = 100;

        // Calculate Base Damage based on originalMob's base level
        int baseLevel = originalMob.level;
        int baseDame = Util.random(baseLevel * 2, baseLevel * 5);
        if (baseLevel > 30 && baseLevel <= 50) {
            baseDame = (baseDame * 15) / 10;
        } else if (baseLevel > 50 && baseLevel <= 70) {
            baseDame = (baseDame * 18) / 10;
        } else if (baseLevel > 70 && baseLevel <= 90) {
            baseDame = (baseDame * 21) / 10;
        } else if (baseLevel > 90 && baseLevel <= 600) {
            baseDame = (baseDame * 25) / 10;
        }
        if (baseDame <= 0) {
            baseDame = Util.random(10, 20);
        }

        boss.base_dame = baseDame;
        boss.final_dame = baseDame * floorNum;

        System.out.println("[BossHunt DEBUG] Spawning boss: " + boss.mob_template.name 
            + " | ID: " + boss.mob_template.mob_id
            + " | Floor: " + floorNum
            + " | Base HP: " + baseHp
            + " | Final HP: " + boss.hp_max
            + " | Base Damage: " + boss.base_dame
            + " | Final Damage: " + boss.final_dame);

        boss.isdie = false;
        boss.id_target = -1;
        boss.index = -1000 - floor;
        boss.map = map_instance;
        boss.boss_info = null;

        System.out.println("[BossHunt] Boss spawned: name=" + boss.mob_template.name
                + " index=" + boss.index + " hp=" + boss.hp_max
                + " level=" + boss.level + " at (" + boss.x + "," + boss.y + ")");

        this.mobs.clear();
        this.mobs.add(boss);

        map_instance.start_map();
        map.Map.add_map_plus(map_instance);
        this.maps.add(map_instance);

        for (Player member : members) {
            if (member.conn != null) {
                Vgo vgo = new Vgo();
                vgo.map_go = new map.Map[] { map_instance };
                vgo.xnew = (short) (boss.x + (Util.random(2) == 0 ? 100 : -100));
                vgo.ynew = boss.y;
                member.goto_map(vgo);
                Service.send_time_cool_down(member, this.floorTime, "Săn Trùm Tầng " + (floor + 1), 2);
                System.out.println("[BossHunt] Teleported " + member.name + " to floor " + (floor + 1));
            } else {
                System.out.println("[BossHunt] Member " + member.name + " is offline, skipping teleport.");
            }
        }
    }

    public synchronized void returnAllToVillage(String message) throws IOException {
        returnAllToVillage(message, this.registeredMapId);
    }

    public synchronized void returnAllToVillage(String message, int mapId) throws IOException {
        System.out.println("[BossHunt] returnAllToVillage -> mapId=" + mapId
                + " | members=" + members.size());
        ACTIVE_HUNTS.remove(this);
        this.active = false;
        List<Player> tempMembers = new ArrayList<>(members);
        for (Player member : tempMembers) {
            member.bossHunt = null;
            if (member.conn != null) {
                if (message != null && !message.isEmpty()) {
                    Service.send_box_ThongBao_OK(member, message);
                }
                int targetMapId = mapId;
                // Nếu member chưa mở được map này -> về map 1
                if (!member.canGoToMap(targetMapId)) {
                    System.out.println("[BossHunt] Player " + member.name + " cannot access map " + targetMapId
                            + " yet. Fallback to map 1.");
                    targetMapId = 1;
                }
                Vgo vgo = new Vgo();
                vgo.map_go = map.Map.get_map_by_id(targetMapId);
                vgo.xnew = 300;
                vgo.ynew = 250;
                member.goto_map(vgo);
                System.out.println("[BossHunt] Teleported " + member.name + " to map " + targetMapId);
            } else {
                System.out.println("[BossHunt] Member " + member.name + " offline, skipping teleport.");
            }
        }
        for (map.Map map : maps) {
            map.stop_map();
        }
        System.out.println("[BossHunt] Hunt ended. All maps stopped.");
    }

    public Mob get_mob(Player p, int id) {
        for (int i = 0; i < mobs.size(); i++) {
            Mob mob = mobs.get(i);
            if (p.map.equals(mob.map) && mob.index == id) {
                return mob;
            }
        }
        return null;
    }

    public synchronized void updateMemberReference(String name, Player newPlayer) {
        for (int i = 0; i < this.members.size(); i++) {
            if (this.members.get(i).name.equals(name)) {
                this.members.set(i, newPlayer);
                break;
            }
        }
    }

    public synchronized void removeMemberByName(String name) {
        this.members.removeIf(m -> m.name.equals(name));
        this.readyState.remove(name);
    }

    public synchronized void giveRewardsForFloor(Player member, int floor, boolean isLastFloor) {
        try {
            List<template.GiftBox> gifts = new ArrayList<>();

            // 1. Ruong cam
            int chestId = ((member.level < 11 ? 11 : member.level) / 10) + 121;
            ItemTemplate4 it_chest = ItemTemplate4.get_it_by_id(chestId);
            if (it_chest != null) {
                template.GiftBox gb = new template.GiftBox();
                gb.id = (short) chestId;
                gb.type = 4;
                gb.name = it_chest.name;
                gb.icon = it_chest.icon;
                gb.num = 1;
                gb.color = 0;
                gifts.add(gb);
            }

            // Send gifts using Service
            if (!gifts.isEmpty()) {
                String notice;
                if (isLastFloor) {
                    notice = "Chúc mừng bạn đã chiến thắng Tầng " + (floor + 1) + "!\nHoàn thành Săn Trùm! Sau 5 giây sẽ trở về làng.";
                } else {
                    notice = "Chúc mừng bạn đã chiến thắng Tầng " + (floor + 1) + "!\nChuẩn bị chuyển sang Tầng " + (floor + 2) + "...";
                }
                Service.send_gift(member, 1, "Phần thưởng BossHunt", notice, gifts, true);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
