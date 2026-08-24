package map;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;

import client.Player;
import core.Manager;
import core.Util;
import io.Message;
import template.Option;
import template.Top_Dame;

/**
 *
 * @author Truongbk
 */
public class Boss {
    public static final Set<Integer> ALLOWED_MAP_IDS = Set.of(
            0, 2, 3, 4, 7, 8, 10, 11, 12, 13, 15, 16, 18, 19, 20, 23, 24, 26, 27, 28, 31, 32, 34, 35, 36, 39, 40, 42,
            43, 44, 47, 48, 50, 51, 52, 63, 65, 68, 70, 71, 72, 82, 84, 85, 86, 94, 95, 96, 97, 98, 99, 100, 101, 112,
            115, 116, 117, 118, 124, 125, 126, 192, 193, 194, 195, 196, 197);
    public static List<Boss> ENTRYS;
    public static byte[] BOSS_LIVE = new byte[] { 0, 0, 0, 0, 0, 0 };
    public static byte[] BOSS_AREA = new byte[] { -1, -1, -1, -1, -1, -1 };
    public static byte[] TIME_NOW = new byte[] { 18, 0, 0 };
    public static long nextWorldBossSpawnTime = 0;
    public static Boss activeWorldBoss = null;
    public static final int STATUS_RESPAWNING = 0;
    public static final int STATUS_ALIVE = 1;
    public static final int STATUS_DEAD = 2;

    public long timeSpawn;
    public long timeDeath;
    public long timeNextRespawn;
    public int status = STATUS_RESPAWNING;

    public Map mapOrigin;
    public short xOrigin;
    public short yOrigin;

    public int id;
    public int thegioi; // 1: Boss thế giới, 2: Boss làng, 3: Boss 24/7
    public Mob mob;
    public byte levelBoss;
    public short[] skill;
    public List<Option> buff;
    public long[] time_atk;
    public List<Top_Dame> TopDame;
    public int index_mob_save;
    public int hp_max_origin;

    public Boss() {
    }

    public void updateHpForLevel() {
        if (this.hp_max_origin <= 0 && this.mob != null) {
            this.hp_max_origin = this.mob.hp_max;
        }
        if (this.hp_max_origin > 0 && this.mob != null) {
            long newHpMax = (long) this.hp_max_origin * (100 + (this.levelBoss - 1) * 20) / 100;
            if (newHpMax > Integer.MAX_VALUE) {
                newHpMax = Integer.MAX_VALUE;
            }
            this.mob.hp_max = (int) newHpMax;
            this.mob.hp = this.mob.hp_max;
        }
    }

    public static void create_boss() {
        core.BXH.resetAllTopBoss();
        BOSS_LIVE = new byte[] { 0, 0, 0, 0, 0, 0 };
        BOSS_AREA = new byte[] { -1, -1, -1, -1, -1, -1 };
        for (int i0 = 135; i0 < 141; i0++) {
            spawn_world_boss_by_id(i0);
        }
    }

    /**
     * Admin: buộc tất cả boss thegioi=1 xuất hiện.
     * Nếu boss đang sống sẽ remove rồi spawn lại.
     * 
     * @return số boss đã spawn
     */
    public static int force_spawn_all_thegioi1() {
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss temp = Boss.ENTRYS.get(i);
            if (temp == null || temp.mob == null || temp.thegioi != 1) {
                continue;
            }
            if (!temp.mob.isdie) {
                temp.mob.isdie = true;
                temp.mob.hp = 0;
                temp.status = STATUS_DEAD;
                try {
                    if (temp.mob.map != null) {
                        temp.mob.map.remove_obj(temp.mob.index, 1);
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            int mobId = temp.mob.mob_template.mob_id;
            if (mobId >= 135 && mobId <= 140) {
                BOSS_LIVE[mobId - 135] = 0;
            }
        }
        activeWorldBoss = null;
        create_boss();
        // Spawn thêm các thegioi=1 không thuộc mob 135-140 (nếu có)
        int extra = 0;
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss temp = Boss.ENTRYS.get(i);
            if (temp == null || temp.mob == null || temp.thegioi != 1 || !temp.mob.isdie) {
                continue;
            }
            int mobId = temp.mob.mob_template.mob_id;
            if (mobId < 135 || mobId > 140) {
                spawn_world_boss(temp);
                extra++;
            }
        }
        int count = 0;
        for (int i = 0; i < BOSS_LIVE.length; i++) {
            if (BOSS_LIVE[i] == 1) {
                count++;
            }
        }
        return count + extra;
    }

    public static void spawn_world_boss_by_id(int mobId) {
        if (mobId < 135 || mobId > 140)
            return;
        List<Boss> list_init = new ArrayList<>();
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss temp = Boss.ENTRYS.get(i);
            temp.TopDame.clear();
            if (temp.mob.isdie && mobId == temp.mob.mob_template.mob_id) {
                list_init.add(temp);
            }
        }
        if (list_init.size() > 0 && BOSS_LIVE[mobId - 135] == 0) {
            Boss temp = list_init.get(Util.random(list_init.size()));
            if (temp.mob.isdie) {
                temp.mob.isdie = false;
                temp.mob.hp = temp.mob.hp_max;
                temp.mob.id_target = -1;
                temp.levelBoss = 1;
                temp.updateHpForLevel();
                temp.mob.index = temp.index_mob_save;
                try {
                    BOSS_AREA[mobId - 135] = temp.mob.map.zone_id;
                    temp.mob.map.can_PK = false;
                    Manager.gI().chatKTG(0,
                            ("Siêu trùm đã xuất hiện hãy cùng săn thôi nào. "
                                    + temp.mob.mob_template.name + " xuất hiện tại "
                                    + temp.mob.map.template.name + " khu "
                                    + (temp.mob.map.zone_id + 1)),
                            5);
                    System.out.println("boss " + temp.mob.mob_template.name + " map "
                            + temp.mob.map.template.name + " khu "
                            + (temp.mob.map.zone_id + 1));
                    //
                    List<Player> list_p = new ArrayList<>();
                    for (int j = 0; j < temp.mob.map.players.size(); j++) {
                        Player p0 = temp.mob.map.players.get(j);
                        if (p0.level / 10 != temp.mob.level / 10) {
                            list_p.add(p0);
                        }
                    }
                    Vgo vgo = new Vgo();
                    vgo.map_go = Map.get_map_by_id(temp.mob.map.template.id);
                    list_p.forEach(l -> {
                        try {
                            vgo.xnew = l.x;
                            vgo.ynew = l.y;
                            l.goto_map(vgo);
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    });
                    //
                    Message m_local = new Message(1);
                    m_local.writer().writeByte(1);
                    m_local.writer().writeShort(temp.mob.index);
                    m_local.writer().writeShort(temp.mob.x);
                    m_local.writer().writeShort(temp.mob.y);
                    for (int j = 0; j < temp.mob.map.players.size(); j++) {
                        Player p0 = temp.mob.map.players.get(j);
                        if (p0 != null && p0.conn != null) {
                            p0.conn.addmsg(m_local);
                        }
                    }
                    m_local.cleanup();
                    //
                } catch (IOException e) {
                    e.printStackTrace();
                }
                BOSS_LIVE[mobId - 135] = 1;
            }
        } else {
            System.out.println("[WARN] spawn_world_boss_by_id: Không tìm thấy Boss ID " + mobId
                    + " trong danh sách Boss.ENTRYS hoặc Boss đã sống.");
        }
    }

    /**
     * Lấy danh sách ID các bản đồ (map) mà Boss tương ứng với mobId có thể xuất
     * hiện (spawn).
     *
     * @param mobId ID của quái/Boss (mob template ID)
     * @return Danh sách ID các bản đồ Boss có thể xuất hiện, hoặc null nếu không
     *         cấu hình
     */
    public static List<Integer> getMapIdsForMob(int mobId) {
        switch (mobId) {
            case 4:
                return List.of(0, 2, 3, 4);
            case 10:
                return List.of(8, 10, 11, 12);
            case 16:
                return List.of(16, 18, 19, 20);
            case 23:
                return List.of(24, 26, 27, 28);
            case 29:
                return List.of(32, 34, 35, 36);
            case 36:
                return List.of(40, 42, 43, 44);
            case 43:
                return List.of(48, 50, 51, 52);
            case 68:
                return List.of(68, 70, 71, 72);
            case 78:
                return List.of(82, 84, 85, 86);
            case 92:
                return List.of(92, 94, 95, 96, 97, 98, 99, 100, 101);
            case 112:
                return List.of(112, 114, 115, 116, 117, 118, 124, 125, 126);
            case 121:
                return List.of(32, 34, 35, 36);
            case 163:
                return List.of(192, 193, 194, 195, 196, 197);
            default:
                return null;
        }
    }

    /**
     * Lấy thời gian xuất hiện ban đầu (giờ, phút) của Boss tương ứng với mobId.
     *
     * @param mobId ID của quái/Boss (mob template ID)
     * @return Mảng gồm 2 phần tử [giờ, phút] biểu thị thời gian spawn đầu tiên, mặc
     *         định là [0, 0] (xuất hiện ngay lập tức)
     */
    public static int[] getInitialSpawnTime(int mobId) {
        switch (mobId) {
            case 4:
                return new int[] { 9, 0 };
            case 10:
                return new int[] { 9, 10 };
            case 16:
                return new int[] { 9, 20 };
            case 23:
                return new int[] { 9, 30 };
            case 29:
                return new int[] { 9, 40 };
            case 36:
                return new int[] { 9, 50 };
            case 43:
                return new int[] { 10, 0 };
            case 68:
                return new int[] { 10, 10 };
            case 78:
                return new int[] { 10, 20 };
            case 92:
                return new int[] { 10, 30 };
            case 112:
                return new int[] { 10, 40 };
            case 121:
                return new int[] { 10, 45 };
            case 163:
                return new int[] { 10, 50 };
            case 172:
                return new int[] { 11, 0 };
            default:
                return new int[] { 0, 0 }; // mặc định xuất hiện ngay lúc 00:00
        }
    }

    public static boolean isTimeToShow(int[] spawnTime) {
        java.util.Calendar cal = java.util.Calendar.getInstance();
        int hour = cal.get(java.util.Calendar.HOUR_OF_DAY);
        int minute = cal.get(java.util.Calendar.MINUTE);
        if (hour > spawnTime[0]) {
            return true;
        } else if (hour == spawnTime[0]) {
            return minute >= spawnTime[1];
        }
        return false;
    }

    /**
     * Kiểm tra xem Boss có phải là Boss Thế Giới (Siêu trùm) hay không.
     * Boss Thế Giới có mob_id từ 135 đến 140.
     *
     * @param mobId ID của quái/Boss
     * @return true nếu là Boss Thế Giới, ngược lại là false
     */
    public static boolean isWorldBoss(int mobId) {
        return mobId >= 135 && mobId <= 140;
    }

    public static void spawn_boss_at_origin(Boss boss) {
        long now = System.currentTimeMillis();
        boss.mob.isdie = false;
        boss.mob.hp = boss.mob.hp_max;
        boss.mob.id_target = -1;
        boss.levelBoss = 1;
        boss.mob.index = boss.index_mob_save;
        boss.updateHpForLevel();

        List<Integer> allowedMaps;
        if (boss.thegioi == 2) {
            // Boss làng: random 1 map trong list đã định nghĩa sẵn theo mob_id
            allowedMaps = getMapIdsForMob(boss.mob.mob_template.mob_id);
        } else if (boss.thegioi == 3) {
            allowedMaps = new ArrayList<>(ALLOWED_MAP_IDS);
        } else {
            allowedMaps = getMapIdsForMob(boss.mob.mob_template.mob_id);
        }

        Map[] zones = null;
        if (allowedMaps != null && allowedMaps.size() > 0) {
            int randomMapId = allowedMaps.get(Util.random(allowedMaps.size()));
            zones = Map.get_map_by_id(randomMapId);
        }

        if (zones != null && zones.length > 0) {
            Map randomMap = zones[Util.random(zones.length)];
            boss.mob.map = randomMap;

            short temp_x = 300;
            short temp_y = 300;
            if (randomMap.template.npcs.size() > 0) {
                Npc npc = randomMap.template.npcs.get(Util.random(randomMap.template.npcs.size()));
                temp_x = npc.x;
                temp_y = npc.y;
            }
            boss.mob.x = temp_x;
            boss.mob.y = temp_y;
        } else if (boss.mapOrigin != null) {
            // Fallback nếu mob_id chưa có trong getMapIdsForMob
            boss.mob.map = boss.mapOrigin;
            boss.mob.x = boss.xOrigin;
            boss.mob.y = boss.yOrigin;
        }

        if (boss.mob.map == null) {
            System.err.println("[WARN] spawn_boss_at_origin: boss " + boss.id + " không có map, bỏ qua.");
            boss.mob.isdie = true;
            return;
        }

        boss.timeSpawn = now;
        boss.status = STATUS_ALIVE;
        boss.TopDame.clear();

        try {
            String label = boss.thegioi == 2 ? "Boss làng" : "Siêu trùm";
            Manager.gI().chatKTG(0,
                    (label + " " + boss.mob.mob_template.name + " đã xuất hiện tại "
                            + boss.mob.map.template.name + " khu "
                            + (boss.mob.map.zone_id + 1) + ". Hãy mau mau đi săn thôi!"),
                    5);
        } catch (Exception e) {
        }

        try {
            Message m_local = new Message(1);
            m_local.writer().writeByte(1);
            m_local.writer().writeShort(boss.mob.index);
            m_local.writer().writeShort(boss.mob.x);
            m_local.writer().writeShort(boss.mob.y);
            boss.mob.map.send_msg_all_p(m_local, null, true);
            m_local.cleanup();
        } catch (IOException e) {
            e.printStackTrace();
        }

        System.out.println("[DEBUG LOG] Boss Respawned - ID: " + boss.mob.mob_template.mob_id
                + " | Name: " + boss.mob.mob_template.name
                + " | thegioi: " + boss.thegioi
                + " | Village/Map ID: " + boss.mob.map.template.id
                + " | Spawn Time: " + new java.util.Date(boss.timeSpawn));
    }

    public static void spawn_world_boss(Boss boss) {
        long now = System.currentTimeMillis();
        boss.mob.isdie = false;
        boss.mob.hp = boss.mob.hp_max;
        boss.mob.id_target = -1;
        boss.levelBoss = 1;
        boss.mob.index = boss.index_mob_save;
        boss.TopDame.clear();

        // Chọn ngẫu nhiên 1 map ID từ ALLOWED_MAP_IDS
        List<Integer> allowedMapList = new ArrayList<>(ALLOWED_MAP_IDS);
        int randomMapId = allowedMapList.get(Util.random(allowedMapList.size()));

        Map[] zones = Map.get_map_by_id(randomMapId);
        if (zones != null && zones.length > 0) {
            Map randomMap = zones[Util.random(zones.length)];
            boss.mob.map = randomMap;

            short temp_x = 300;
            short temp_y = 300;
            if (randomMap.template.npcs.size() > 0) {
                Npc npc = randomMap.template.npcs.get(Util.random(randomMap.template.npcs.size()));
                temp_x = npc.x;
                temp_y = npc.y;
            }
            boss.mob.x = temp_x;
            boss.mob.y = temp_y;

            try {
                boss.mob.map.can_PK = false;
                Manager.gI().chatKTG(0,
                        ("Sự kiện: Siêu trùm thế giới " + boss.mob.mob_template.name + " đã xuất hiện tại "
                                + boss.mob.map.template.name + " khu "
                                + (boss.mob.map.zone_id + 1) + ". Hãy mau mau đi săn thôi!"),
                        5);
                System.out.println("[DEBUG LOG] World Boss Spawned - ID: " + boss.mob.mob_template.mob_id
                        + " | Name: " + boss.mob.mob_template.name
                        + " | Village/Map ID: " + boss.mob.map.template.id
                        + " | Spawn Time: " + new java.util.Date(now));

                Message m_local = new Message(1);
                m_local.writer().writeByte(1);
                m_local.writer().writeShort(boss.mob.index);
                m_local.writer().writeShort(boss.mob.x);
                m_local.writer().writeShort(boss.mob.y);
                boss.mob.map.send_msg_all_p(m_local, null, true);
                m_local.cleanup();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        boss.timeSpawn = now;
        boss.status = STATUS_ALIVE;
    }

    public static void update_world_bosses() {
        long now = System.currentTimeMillis();

        // 1. Kiểm tra nếu đang có Boss thế giới hoạt động
        if (activeWorldBoss != null) {
            if (activeWorldBoss.mob.isdie || activeWorldBoss.mob.hp <= 0) {
                System.out.println("[DEBUG LOG] World Boss Died - ID: " + activeWorldBoss.mob.mob_template.mob_id
                        + " | Name: " + activeWorldBoss.mob.mob_template.name);

                // Hồi sinh con tiếp theo sau 10 phút (600.000 ms)
                nextWorldBossSpawnTime = now + 600000;
                activeWorldBoss = null;
            }
            return;
        }

        // 2. Sinh Boss thế giới mới nếu đến thời gian hồi sinh
        if (now >= nextWorldBossSpawnTime) {
            List<Boss> worldBosses = new ArrayList<>();
            for (int i = 0; i < Boss.ENTRYS.size(); i++) {
                Boss b = Boss.ENTRYS.get(i);
                if (b != null && b.thegioi == 1 && b.mob != null && b.mob.isdie) {
                    worldBosses.add(b);
                }
            }

            if (worldBosses.size() > 0) {
                Boss bossToSpawn = worldBosses.get(Util.random(worldBosses.size()));
                spawn_world_boss(bossToSpawn);
                activeWorldBoss = bossToSpawn;
            }
        }
    }

    /** Boss làng: mỗi con tự hồi sau 10 phút. Boss thegioi=3: 30 phút. */
    public static final long RESPAWN_LANG_MS = 600_000L; // 10 phút
    public static final long RESPAWN_TG3_MS = 1_800_000L; // 30 phút

    public static void update_bosses() {
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss boss = Boss.ENTRYS.get(i);
            if (boss == null || boss.mob == null) {
                continue;
            }
            // thegioi=2: boss làng — mỗi con hồi độc lập
            // thegioi=3: boss 24/7
            if (boss.thegioi != 2 && boss.thegioi != 3) {
                continue;
            }

            long now = System.currentTimeMillis();
            long respawnMs = boss.thegioi == 2 ? RESPAWN_LANG_MS : RESPAWN_TG3_MS;

            if (boss.status == STATUS_RESPAWNING) {
                spawn_boss_at_origin(boss);
            } else if (boss.status == STATUS_DEAD) {
                if (now >= boss.timeNextRespawn) {
                    spawn_boss_at_origin(boss);
                }
            } else if (boss.status == STATUS_ALIVE) {
                if (boss.mob.isdie || boss.mob.hp <= 0) {
                    boss.status = STATUS_DEAD;
                    boss.timeDeath = now;
                    boss.timeNextRespawn = now + respawnMs;

                    System.out.println("[DEBUG LOG] Boss thegioi=" + boss.thegioi + " Died - ID: "
                            + boss.mob.mob_template.mob_id
                            + " | Name: " + boss.mob.mob_template.name
                            + " | Village/Map ID: " + (boss.mob.map != null ? boss.mob.map.template.id : -1)
                            + " | Death Time: " + new java.util.Date(boss.timeDeath)
                            + " | Next Respawn Time: " + new java.util.Date(boss.timeNextRespawn));
                }
            }
        }
    }

    /**
     * @deprecated Boss làng (thegioi=2) đã chuyển sang hồi sinh độc lập trong
     *             {@link #update_bosses()}.
     *             Giữ method rỗng để không lỗi chỗ còn gọi cũ.
     */
    public static void spawn_event_boss() {
        // no-op: mỗi boss làng tự hồi 10 phút sau khi chết, không random chung pool
    }

    public static Mob get_mob(Player p, int id) {
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss tempB = Boss.ENTRYS.get(i);
            if (!tempB.mob.isdie && tempB.mob.map.template.id == p.map.template.id
                    && tempB.mob.index == id) {
                Mob temp_mob = tempB.mob;
                if (!temp_mob.isdie) {
                    return temp_mob;
                }
            }
        }
        return null;
    }

    public static void result_boss() {
        //
        short[][] list = new short[][] { //
                new short[] { 135 }, //
                new short[] { 136 }, //
                new short[] { 137 }, //
                new short[] { 138 }, //
                new short[] { 139 }, //
                new short[] { 140 }, //
        };
        for (int i12 = 0; i12 < list.length; i12++) {
            List<Top_Dame> list_select = null;
            Boss boss_select = null;
            for (int i = 0; i < Boss.ENTRYS.size(); i++) {
                Boss tempB = Boss.ENTRYS.get(i);
                if (list[i12][0] == tempB.mob.mob_template.mob_id
                        && tempB.TopDame.size() > 0) {
                    list_select = tempB.TopDame;
                    boss_select = tempB;
                    tempB.mob.map.can_PK = true;
                    break;
                }
            }
            if (boss_select != null) {
                core.BXH.updateTopBoss(boss_select);
            }
            if (list_select != null) {
                List<Top_Dame> result = Util.sort(list_select);
                StringBuilder sb = new StringBuilder("Top gây dame Boss: ");
                for (int k = 0; k < Math.min(3, result.size()); k++) {
                    Top_Dame td = result.get(k);
                    sb.append((k + 1)).append(". ").append(td.name)
                            .append(" (").append(td.dame).append(") ");
                }
                try {
                    Manager.gI().chatKTG(0, sb.toString(), 5);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        //
        List<Boss> list_remove = new ArrayList<>();
        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss temp = Boss.ENTRYS.get(i);
            if (!temp.mob.isdie && temp.thegioi == 1) {
                list_remove.add(temp);
            }
        }
        for (int i = 0; i < list_remove.size(); i++) {
            Boss temp = list_remove.get(i);
            temp.mob.isdie = true;
            try {
                temp.mob.map.remove_obj(temp.mob.index, 1);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        try {
            Manager.gI().chatKTG(0, "Hoạt động săn siêu trùm hôm nay đã kết thúc", 5);
        } catch (IOException e) {
            e.printStackTrace();
        }
        BOSS_LIVE = new byte[] { 0, 0, 0, 0, 0, 0 };
        BOSS_AREA = new byte[] { -1, -1, -1, -1, -1, -1 };
    }

    public static long dualEventDespawnTime = 0;
    public static boolean isDualEventActive = false;

    public static void spawnDualEventBosses() {
        int[] allowedMaps = new int[] { 32, 34, 35, 36 };
        int targetMapId = allowedMaps[Util.random(allowedMaps.length)];
        Map[] zones = Map.get_map_by_id(targetMapId);

        if (zones == null || zones.length == 0) {
            return;
        }

        Boss bossEch = null;

        for (int i = 0; i < Boss.ENTRYS.size(); i++) {
            Boss b = Boss.ENTRYS.get(i);
            if (b.mob != null && b.mob.mob_template != null) {
                if (b.mob.mob_template.mob_id == 121) {
                    bossEch = b;
                    break;
                }
            }
        }

        if (bossEch == null) {
            return;
        }

        Map randomMap = zones[Util.random(zones.length)];
        short spawnX = 300;
        short spawnY = 300;
        if (randomMap.template.npcs.size() > 0) {
            Npc npc = randomMap.template.npcs.get(Util.random(randomMap.template.npcs.size()));
            spawnX = npc.x;
            spawnY = npc.y;
        }

        // Spawn Boss Mèo truyền thuyết
        bossEch.mob.isdie = false;
        bossEch.mob.hp = bossEch.mob.hp_max;
        bossEch.mob.id_target = -1;
        bossEch.levelBoss = 1;
        bossEch.mob.index = bossEch.index_mob_save;
        bossEch.mob.map = randomMap;
        bossEch.mob.x = spawnX;
        bossEch.mob.y = spawnY;
        bossEch.TopDame.clear();

        isDualEventActive = true;
        dualEventDespawnTime = System.currentTimeMillis() + 30 * 60 * 1000L; // 30 phút

        try {
            Manager.gI().chatKTG(0,
                    "SỰ KIỆN: Boss Mèo truyền thuyết đã xuất hiện tại "
                            + randomMap.template.name + " (Khu " + (randomMap.zone_id + 1)
                            + ")! Sự kiện kéo dài 30 phút, các thuyền trưởng hãy mau đi săn!",
                    5);

            Message m_local = new Message(1);
            m_local.writer().writeByte(1);
            m_local.writer().writeShort(bossEch.mob.index);
            m_local.writer().writeShort(bossEch.mob.x);
            m_local.writer().writeShort(bossEch.mob.y);
            m_local.writer().writeByte(-1);
            for (int j = 0; j < randomMap.players.size(); j++) {
                Player p0 = randomMap.players.get(j);
                if (p0 != null && p0.conn != null) {
                    p0.conn.addmsg(m_local);
                }
            }
            m_local.cleanup();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void checkDualEventDespawn() {
        if (!isDualEventActive) {
            return;
        }

        if (System.currentTimeMillis() >= dualEventDespawnTime) {
            isDualEventActive = false;
            boolean despawnedAny = false;

            for (int i = 0; i < Boss.ENTRYS.size(); i++) {
                Boss b = Boss.ENTRYS.get(i);
                if (b.mob != null && b.mob.mob_template != null) {
                    if (b.mob.mob_template.mob_id == 121 && !b.mob.isdie) {
                        b.mob.isdie = true;
                        b.mob.hp = 0;
                        b.mob.id_target = -1;
                        b.TopDame.clear();
                        despawnedAny = true;

                        try {
                            Message m_local = new Message(1);
                            m_local.writer().writeByte(0);
                            m_local.writer().writeShort(b.mob.index);
                            for (int j = 0; j < b.mob.map.players.size(); j++) {
                                Player p0 = b.mob.map.players.get(j);
                                if (p0 != null && p0.conn != null) {
                                    p0.conn.addmsg(m_local);
                                }
                            }
                            m_local.cleanup();
                            if (b.mob.map != null) {
                                b.mob.map.remove_obj(b.mob.index, 1);
                            }
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                    }
                }
            }

            if (despawnedAny) {
                try {
                    Manager.gI().chatKTG(0,
                            "Hết 30 phút! Sự kiện kết thúc, Boss Mèo truyền thuyết đã rút lui!", 5);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
