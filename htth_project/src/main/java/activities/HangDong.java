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
import template.GiftBox;
import template.ItemTemplate4;
import template.ItemTemplate7;

public class HangDong extends Dungeon {
    public static final List<HangDong> ACTIVE_HANG_DONG = new CopyOnWriteArrayList<>();

    public List<Player> partyMembers = new ArrayList<>();
    public Player leader;
    public int currentStageIndex = 0; // 0 to 99
    public long stageEndTime;
    public Map currentMap;
    public boolean active = false;
    public boolean finished = false;
    public boolean isTransitioning = false;
    public long transitionTime = 0;
    public boolean rewardsGiven = false; // tránh trao thưởng trùng lặp
    public boolean isFailing = false;
    public long failTime = 0;

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
        if (stageIndex < 0 || stageIndex >= 1000) {
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
        map_dungeon.zone_id = (byte) (stageIndex % 100);
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
        int floor = stageIndex + 1;
        int mobCount = 50;
        if (floor >= 100 && floor <= 500) {
            mobCount = 150;
        } else if (floor > 500) {
            mobCount = 200;
        }

        // Tạo quái theo số lượng tùy thuộc vào tầng
        for (int i = 0; i < mobCount; i++) {
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
            int base_hp = 5000 + (maxLevel * 1000);
            if (floor >= 100) {
                // Tăng HP quái tuyến tính theo từng tầng (từ tầng 100 bắt đầu tăng, tầng 1000 tăng gấp 10 lần)
                int hp_val = base_hp + base_hp * (floor - 100) / 100;

                // Tính sát thương (dame) cơ bản dựa trên cấp độ người chơi
                int default_dame = maxLevel * 3;
                if (maxLevel > 90) {
                    default_dame = (default_dame * 25) / 10;
                }
                // Tăng Dame quái tuyến tính theo từng tầng (từ tầng 100 bắt đầu tăng, tầng 1000 tăng gấp 10 lần)
                int final_dame_val = default_dame + default_dame * (floor - 100) / 100;

                // Tăng đột biến thêm máu và dame khi vượt mốc tầng 500
                if (floor >= 500) {
                    hp_val = (hp_val * 15) / 10; // Tăng thêm 50% HP
                    final_dame_val = final_dame_val * 2; // Tăng gấp đôi Dame
                }

                mob_add.hp_max = hp_val;

                // Tạo dao động ngẫu nhiên cho sát thương khoảng +-20%
                int variation = final_dame_val * 20 / 100;
                if (variation <= 0)
                    variation = 10;
                mob_add.final_dame = core.Util.random(final_dame_val - variation, final_dame_val + variation);
            } else {
                mob_add.hp_max = base_hp;
                mob_add.final_dame = 0;
            }
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
                    Service.send_time_cool_down(p, this.stageEndTime, "Tầng " + (stageIndex + 1), 2);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public synchronized void checkTransition() {
        if (!active || finished)
            return;

        if (isTransitioning) {
            if (System.currentTimeMillis() >= transitionTime) {
                isTransitioning = false;
                createStage(currentStageIndex + 1);
            }
            return;
        }

        boolean allDead = true;
        for (Mob mob : this.mobs) {
            if (!mob.isdie) {
                allDead = false;
                break;
            }
        }

        if (allDead) {
            isTransitioning = true;
            rewardsGiven = true;
            transitionTime = System.currentTimeMillis() + 5000L;
            if (currentStageIndex >= 49) {
                try {
                    String names = leader.name;
                    if (partyMembers.size() > 1) {
                        names += " và đồng đội";
                    }
                    core.Manager.gI().chatKTG(1, "Tin đồn: " + names + " đã xuất sắc vượt qua Hang Động tầng "
                            + (currentStageIndex + 1) + "!", 0);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
            for (Player p : partyMembers) {
                if (p != null && p.conn != null && p.conn.connected && p.map.equals(currentMap)) {
                    if ((currentStageIndex + 1) > p.hangdong_stage) {
                        p.hangdong_stage = currentStageIndex + 1;
                    }
                    try {
                        if (currentStageIndex >= 999) {
                            Service.send_box_ThongBao_OK(p,
                                    "Đã vượt qua tầng " + (currentStageIndex + 1) + ". Hang động kết thúc!");
                        } else {
                            Service.send_box_ThongBao_OK(p, "Đã vượt qua tầng " + (currentStageIndex + 1)
                                    + ". Chuẩn bị sang tầng " + (currentStageIndex + 2) + " sau 5 giây...");
                        }
                        Service.send_time_cool_down(p, this.transitionTime, "Chuyển tầng", 2);
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
            // Trao thưởng ngay khi hoàn thành tầng để người chơi xem trong thời gian chờ 5s
            distributeCurrentStageRewards();
        }
    }

    private void distributeCurrentStageRewards() {
        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.map.equals(this.currentMap)) {
                giveStageRewards(pOnline, this.currentStageIndex);
            }
        }
    }

    private void giveStageRewards(Player p, int stageIndex) {
        try {
            List<GiftBox> list_gift = new ArrayList<>();
            // bột vàng (ID 4, type 7)
            // beri (ID 0, type 4)
            // ruby (ID 1, type 4)
            // bột cường hoá (ID 1, type 7)
            // bột tím (ID 3, type 7)
            int[][] pool = {
                    { 4, 7 }, // bột vàng
                    { 0, 4 }, // beri
                    { 1, 4 }, // ruby
                    { 1, 7 }, // bột cường hoá
                    { 3, 7 } // bột tím
            };

            List<Integer> selectedIndices = new ArrayList<>();
            while (selectedIndices.size() < 3) {
                int r = core.Util.random(pool.length);
                if (!selectedIndices.contains(r)) {
                    selectedIndices.add(r);
                }
            }

            int floor = stageIndex + 1;
            int amountMin;
            int amountMax;
            if (floor <= 50) {
                amountMin = 1;
                amountMax = 11;
            } else if (floor < 100) {
                amountMin = 10;
                amountMax = 21;
            } else {
                amountMin = 20;
                amountMax = 51;
            }

            for (int idx : selectedIndices) {
                int[] item = pool[idx];
                int amount = core.Util.random(amountMin, amountMax);

                GiftBox gb = new GiftBox();
                gb.id = (short) item[0];
                gb.type = (byte) item[1];
                if (gb.type == 4) {
                    ItemTemplate4 it4 = ItemTemplate4.get_it_by_id(gb.id);
                    if (it4 != null) {
                        gb.name = it4.name;
                        gb.icon = it4.icon;
                    }
                } else if (gb.type == 7) {
                    ItemTemplate7 it7 = ItemTemplate7.get_it_by_id(gb.id);
                    if (it7 != null) {
                        gb.name = it7.name;
                        gb.icon = it7.icon;
                    }
                }
                if (gb.name == null) {
                    gb.name = "Phần thưởng";
                }
                gb.num = amount;
                gb.color = 0;
                list_gift.add(gb);
            }

            if (!list_gift.isEmpty()) {
                Service.send_gift(p, 1, "Hang Động Tầng " + floor, "Phần thưởng", list_gift, true);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void completeDungeon() {
        completeDungeon(false);
    }

    public void completeDungeon(boolean isFail) {
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
                    if (isFail) {
                        Map[] targetMaps = Map.get_map_by_id(p.id_map_save);
                        if (targetMaps != null && targetMaps.length > 0) {
                            Vgo vgo = new Vgo();
                            vgo.map_go = targetMaps;
                            vgo.xnew = 300;
                            vgo.ynew = 250;
                            for (int i = 0; i < targetMaps[0].template.npcs.size(); i++) {
                                map.Npc npc_temp = targetMaps[0].template.npcs.get(i);
                                if (npc_temp.namegt.equals("Bản đồ")) {
                                    vgo.xnew = npc_temp.x;
                                    if (npc_temp.y < 250) {
                                        vgo.ynew = (short) (npc_temp.y + 20);
                                    } else {
                                        vgo.ynew = (short) (npc_temp.y - 40);
                                    }
                                    break;
                                }
                            }
                            p.isdie = false;
                            int hp_after_ = p.body.get_hp_max(true) / 10;
                            Service.use_potion(p, 0, hp_after_);
                            p.time_can_mob_atk = System.currentTimeMillis() + 1200L;
                            p.goto_map(vgo);
                        }
                    } else {
                        Service.send_box_ThongBao_OK(p, "Hang động kết thúc. Trở về làng.");
                        Map[] targetMaps = Map.get_map_by_id(1);
                        if (targetMaps != null && targetMaps.length > 0) {
                            Vgo vgo = new Vgo();
                            vgo.map_go = targetMaps;
                            vgo.xnew = 300;
                            vgo.ynew = 250;
                            p.goto_map(vgo);
                        }
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

        // 3. Fail delay check
        if (this.isFailing) {
            if (System.currentTimeMillis() >= this.failTime) {
                this.isFailing = false;
                completeDungeon(true);
            }
            return;
        }

        // 4. All Players Dead Check
        boolean allDead = true;
        int countOnline = 0;
        for (Player member : this.partyMembers) {
            Player pOnline = Map.get_player_by_name_allmap(member.name);
            if (pOnline != null && pOnline.conn != null && pOnline.conn.connected
                    && pOnline.map.equals(this.currentMap)) {
                countOnline++;
                if (!pOnline.isdie) {
                    allDead = false;
                }
            }
        }

        if (countOnline > 0 && allDead) {
            this.isFailing = true;
            this.failTime = System.currentTimeMillis() + 5000L;
            for (Player p : partyMembers) {
                if (p != null && p.conn != null && p.conn.connected) {
                    Service.send_box_ThongBao_OK(p, "Đi hang động thất bại quay về làng đã đăng ký");
                }
            }
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
