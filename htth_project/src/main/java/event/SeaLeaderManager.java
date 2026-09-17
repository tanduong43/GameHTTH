package event;

import java.io.File;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import client.Clan;
import client.Player;
import template.Clan_member;
import core.Manager;
import core.MenuController;
import core.Service;
import core.Util;
import database.SQL;
import io.Message;
import map.Map;
import map.MapTemplate;
import map.Mob;
import map.Npc;
import map.Vgo;

/**
 * Quản lý toàn bộ hệ thống sự kiện "Thủ Lĩnh Biển Khơi"
 * Thời gian: Thứ 6 hàng tuần từ 23:00 đến 00:00 (Thứ 7).
 * Quy tắc cốt lõi:
 * - 1 biển = tối đa 1 Clan.
 * - KHÔNG CÓ QUEUE, KHÔNG CÓ HÀNG CHỜ.
 * - Biển đầy -> từ chối, người chơi tự chọn biển khác còn trống.
 * - Cả 4 biển đầy -> từ chối đăng ký mới.
 * - Khi kết thúc (00:00) -> Dịch chuyển toàn bộ người chơi về Map 1.
 */
public class SeaLeaderManager implements Runnable {

    private static SeaLeaderManager instance;

    // ID Menu động cho sự kiện
    public static final int MENU_ID_SELECT_SEA = 9841;
    public static final int NPC_SU_GIA_MAP_179 = -888;
    public static final int NPC_HO_VE_MAP_180 = -889;

    // Map IDs
    public static final int MAP_SANH_CHIEN = 179;
    public static final int MAP_SANH_HO_VE = 180;
    public static final int MAP_BIEN_DONG = 182;
    public static final int MAP_BIEN_BAC = 178;
    public static final int MAP_BIEN_TAY = 183;
    public static final int MAP_BIEN_NAM = 181;

    // 4 Biển
    private final SeaLeaderSea[] seas = new SeaLeaderSea[4];

    // Trạng thái sự kiện
    private SeaLeaderEvent currentEvent;
    private ScheduledExecutorService scheduler;

    // Chống trùng lặp mob kill trong cùng 1 chu kỳ sống
    private final Set<Integer> killedMobThisCycle = Collections.newSetFromMap(new ConcurrentHashMap<Integer, Boolean>());

    public static SeaLeaderManager getInstance() {
        if (instance == null) {
            instance = new SeaLeaderManager();
        }
        return instance;
    }

    private SeaLeaderManager() {
        seas[0] = new SeaLeaderSea(SeaArea.BIEN_DONG);
        seas[1] = new SeaLeaderSea(SeaArea.BIEN_BAC);
        seas[2] = new SeaLeaderSea(SeaArea.BIEN_TAY);
        seas[3] = new SeaLeaderSea(SeaArea.BIEN_NAM);
        this.currentEvent = new SeaLeaderEvent();
    }

    // Chế độ mở 24/24 để test (true: mở liên tục 24/24; false: chỉ mở Thứ 6 23:00 - 00:00)
    public static boolean OPEN_24_7_TEST = true;

    /**
     * Khởi tạo hệ thống: Tạo bảng DB, đảm bảo Maps tồn tại, nạp đợt sự kiện hiện tại và bắt đầu scheduler.
     */
    public void init() {
        initDatabase();
        ensureMapFilesAndMarkers();
        initMapTemplates();
        loadCurrentEventFromDB();

        // Nếu ở chế độ test 24/24 và sự kiện chưa ACTIVE thì tự động kích hoạt
        if (OPEN_24_7_TEST && !isActive()) {
            startEvent();
        }

        if (scheduler != null) {
            scheduler.shutdown();
        }
        scheduler = Executors.newScheduledThreadPool(1);
        scheduler.scheduleAtFixedRate(this, 10, 30, TimeUnit.SECONDS);

        System.out.println("[SeaLeaderManager] Hệ thống Thủ Lĩnh Biển Khơi đã sẵn sàng!" + (OPEN_24_7_TEST ? " (CHẾ ĐỘ TEST 24/24 ĐANG BẬT)" : ""));
    }

    /**
     * Tự động tạo các bảng DB nếu chưa có.
     */
    private void initDatabase() {
        try (Connection conn = SQL.gI().getCon();
             Statement st = conn.createStatement()) {

            // Bảng quản lý đợt sự kiện
            st.execute("CREATE TABLE IF NOT EXISTS `sea_leader_event` ("
                    + "`id` INT AUTO_INCREMENT PRIMARY KEY, "
                    + "`week_key` VARCHAR(30) NOT NULL, "
                    + "`start_time` DATETIME NULL, "
                    + "`end_time` DATETIME NULL, "
                    + "`status` VARCHAR(20) NOT NULL DEFAULT 'WAITING', "
                    + "`winner_sea_id` INT DEFAULT -1, "
                    + "`winner_clan_id` INT DEFAULT -1, "
                    + "`winner_clan_name` VARCHAR(100) DEFAULT '', "
                    + "`winning_score` INT DEFAULT 0, "
                    + "`created_at` DATETIME DEFAULT CURRENT_TIMESTAMP"
                    + ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

            // Bảng lưu thông tin đăng ký và điểm số các biển
            st.execute("CREATE TABLE IF NOT EXISTS `sea_leader_registration` ("
                    + "`id` INT AUTO_INCREMENT PRIMARY KEY, "
                    + "`event_id` INT NOT NULL, "
                    + "`sea_id` INT NOT NULL, "
                    + "`clan_id` INT NOT NULL, "
                    + "`clan_name` VARCHAR(100) NOT NULL, "
                    + "`score` INT DEFAULT 0, "
                    + "`status` VARCHAR(20) DEFAULT 'FULL', "
                    + "`registered_at` DATETIME DEFAULT CURRENT_TIMESTAMP, "
                    + "UNIQUE KEY `uk_event_sea` (`event_id`, `sea_id`), "
                    + "UNIQUE KEY `uk_event_clan` (`event_id`, `clan_id`)"
                    + ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

            // Bảng lưu lịch sử trao thưởng (chống nhận trùng)
            st.execute("CREATE TABLE IF NOT EXISTS `sea_leader_reward` ("
                    + "`id` INT AUTO_INCREMENT PRIMARY KEY, "
                    + "`event_id` INT NOT NULL, "
                    + "`player_name` VARCHAR(50) NOT NULL, "
                    + "`clan_id` INT NOT NULL, "
                    + "`claimed` TINYINT DEFAULT 1, "
                    + "`claimed_at` DATETIME DEFAULT CURRENT_TIMESTAMP, "
                    + "UNIQUE KEY `uk_event_player` (`event_id`, `player_name`)"
                    + ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;");

        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Init DB Error: " + e.getMessage());
        }
    }

    /**
     * Tạo file marker 0-byte trong thư mục data/map/ để Manager.java không bỏ qua khi khởi động.
     */
    private void ensureMapFilesAndMarkers() {
        int[] ids = new int[] { MAP_SANH_CHIEN, MAP_SANH_HO_VE, MAP_BIEN_DONG, MAP_BIEN_BAC, MAP_BIEN_TAY, MAP_BIEN_NAM };
        File dir = new File("data/map");
        if (!dir.exists()) {
            dir.mkdirs();
        }
        for (int id : ids) {
            File f = new File(dir, String.valueOf(id));
            if (!f.exists()) {
                try {
                    f.createNewFile();
                } catch (Exception e) {
                    System.err.println("[SeaLeaderManager] Could not create marker file: " + f.getAbsolutePath());
                }
            }
        }
    }

    /**
     * Khởi tạo / Clone Map Templates trong bộ nhớ:
     * - Map 179 (Sảnh chiến): clone data từ Map 7 (Biển Đông 1), KHÔNG mobs.
     * - Map 180 (Sảnh hộ vệ): clone data từ Map 119 (Phòng chờ truy nã), KHÔNG mobs, có NPC chuyển biển.
     * - 4 Biển (178, 181, 182, 183): clone data + mobs từ Map 117 (Vùng Tử Chiến 1), độc lập.
     */
    public void initMapTemplates() {
        try {
            Map[] map7 = Map.get_map_by_id(7);
            Map[] map117 = Map.get_map_by_id(117);
            Map[] map119 = Map.get_map_by_id(119);

            if (map7 == null || map7.length == 0 || map7[0].template == null) {
                System.err.println("[SeaLeaderManager] Source Map 7 is not loaded!");
                return;
            }
            if (map117 == null || map117.length == 0 || map117[0].template == null) {
                System.err.println("[SeaLeaderManager] Source Map 117 is not loaded!");
                return;
            }
            if (map119 == null || map119.length == 0 || map119[0].template == null) {
                System.err.println("[SeaLeaderManager] Source Map 119 is not loaded!");
                return;
            }

            MapTemplate temp7 = map7[0].template;
            MapTemplate temp117 = map117[0].template;
            MapTemplate temp119 = map119[0].template;

            // 1. Map 179 - Sảnh chiến (Không quái - data từ Map 7)
            setupLobbyMap(MAP_SANH_CHIEN, "Sảnh chiến", temp7, NPC_SU_GIA_MAP_179, "Sứ Giả Biển Khơi", 380, 190);

            // 2. Map 180 - Sảnh hộ vệ (Không quái, không NPC, 1 khu duy nhất - data từ Map 119 Phòng chờ truy nã)
            setupLobbyMap(MAP_SANH_HO_VE, "Sảnh hộ vệ", temp119, 0, "", 0, 0);

            // 3. 4 Biển (Lấy data + quái từ Map 117)
            setupSeaMap(MAP_BIEN_DONG, "Biển Đông", temp117, map117[0]);
            setupSeaMap(MAP_BIEN_BAC, "Biển Bắc", temp117, map117[0]);
            setupSeaMap(MAP_BIEN_TAY, "Biển Tây", temp117, map117[0]);
            setupSeaMap(MAP_BIEN_NAM, "Biển Nam", temp117, map117[0]);

            // 4. Thiết lập đường dẫn (Vgo) liên kết giữa các Map
            setupEventMapVgos();

            System.out.println("[SeaLeaderManager] Clone Map Templates hoàn tất!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Thiết lập đường dẫn (Vgo) liên kết giữa các Map sự kiện:
     * - Map 179 (Sảnh chiến): Cổng phải -> Map 180 (Sảnh hộ vệ); Cổng trái -> Map 1 (Làng).
     * - Map 180 (Sảnh hộ vệ): Cổng trái -> Map 179 (Sảnh chiến); Cổng phải -> Biển của Clan.
     * - 4 Biển (178, 181, 182, 183): Cổng trái & phải -> Map 180 (Sảnh hộ vệ).
     */
    public void setupEventMapVgos() {
        try {
            Map[] map1 = Map.get_map_by_id(1);
            Map[] map178 = Map.get_map_by_id(MAP_BIEN_BAC);
            Map[] map179 = Map.get_map_by_id(MAP_SANH_CHIEN);
            Map[] map180 = Map.get_map_by_id(MAP_SANH_HO_VE);
            Map[] map181 = Map.get_map_by_id(MAP_BIEN_NAM);
            Map[] map182 = Map.get_map_by_id(MAP_BIEN_DONG);
            Map[] map183 = Map.get_map_by_id(MAP_BIEN_TAY);

            // 1. Map 179 - Sảnh chiến
            if (map179 != null && map179.length > 0 && map179[0].template != null) {
                MapTemplate temp179 = map179[0].template;
                temp179.vgos = new ArrayList<>();

                // Cổng phải (x=1296, y=276) -> Sang Sảnh hộ vệ (Map 180)
                if (map180 != null && map180.length > 0) {
                    Vgo vgoToHoVe = new Vgo();
                    vgoToHoVe.id_map_go = (short) MAP_SANH_HO_VE;
                    vgoToHoVe.xold = 1296;
                    vgoToHoVe.yold = 276;
                    vgoToHoVe.xnew = 60;
                    vgoToHoVe.ynew = 250;
                    vgoToHoVe.map_go = map180;
                    temp179.vgos.add(vgoToHoVe);
                }
            }

            // 2. Map 180 - Sảnh hộ vệ (Data từ Map 119: maxW=528, maxH=408)
            if (map180 != null && map180.length > 0 && map180[0].template != null) {
                MapTemplate temp180 = map180[0].template;
                temp180.vgos = new ArrayList<>();

                // Cổng trái (x=24, y=260) -> Về Sảnh chiến (Map 179)
                if (map179 != null && map179.length > 0) {
                    Vgo vgoToSanhChien = new Vgo();
                    vgoToSanhChien.id_map_go = (short) MAP_SANH_CHIEN;
                    vgoToSanhChien.xold = 24;
                    vgoToSanhChien.yold = 260;
                    vgoToSanhChien.xnew = 1240;
                    vgoToSanhChien.ynew = 276;
                    vgoToSanhChien.map_go = map179;
                    temp180.vgos.add(vgoToSanhChien);
                }

                // Cổng phải (x=504, y=260) -> Tiến vào Biển của Clan (mặc định Map 182)
                if (map182 != null && map182.length > 0) {
                    Vgo vgoToSea = new Vgo();
                    vgoToSea.id_map_go = (short) MAP_BIEN_DONG;
                    vgoToSea.xold = 504;
                    vgoToSea.yold = 260;
                    vgoToSea.xnew = 60;
                    vgoToSea.ynew = 288;
                    vgoToSea.map_go = map182;
                    temp180.vgos.add(vgoToSea);
                }
            }

            // 3. 4 Biển (178, 181, 182, 183)
            int[] seaMapIds = new int[] { MAP_BIEN_BAC, MAP_BIEN_NAM, MAP_BIEN_DONG, MAP_BIEN_TAY };
            for (int seaMapId : seaMapIds) {
                Map[] sMaps = Map.get_map_by_id(seaMapId);
                if (sMaps != null && sMaps.length > 0 && sMaps[0].template != null) {
                    MapTemplate sTemp = sMaps[0].template;
                    sTemp.vgos = new ArrayList<>();

                    if (map180 != null && map180.length > 0) {
                        // Cổng trái (x=24, y=288) -> Trở về Sảnh hộ vệ (Map 180)
                        Vgo vgoLeft = new Vgo();
                        vgoLeft.id_map_go = (short) MAP_SANH_HO_VE;
                        vgoLeft.xold = 24;
                        vgoLeft.yold = 288;
                        vgoLeft.xnew = 400;
                        vgoLeft.ynew = 276;
                        vgoLeft.map_go = map180;
                        sTemp.vgos.add(vgoLeft);

                        // Cổng phải (x=1056, y=288) -> Trở về Sảnh hộ vệ (Map 180)
                        Vgo vgoRight = new Vgo();
                        vgoRight.id_map_go = (short) MAP_SANH_HO_VE;
                        vgoRight.xold = 1056;
                        vgoRight.yold = 288;
                        vgoRight.xnew = 400;
                        vgoRight.ynew = 276;
                        vgoRight.map_go = map180;
                        sTemp.vgos.add(vgoRight);
                    }
                }
            }
            System.out.println("[SeaLeaderManager] Thiết lập đường dẫn (Vgo) liên kết giữa các Map hoàn tất!");
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Error setting up event map vgos: " + e.getMessage());
        }
    }

    private void setupLobbyMap(int mapId, String mapName, MapTemplate sourceTemp, int npcId, String npcName, int npcX, int npcY) {
        Map[] existing = Map.get_map_by_id(mapId);
        if (existing != null && existing.length > 0) {
            MapTemplate temp = existing[0].template;
            if (temp != null) {
                temp.data = sourceTemp.data;
                temp.IDBack = sourceTemp.IDBack;
                temp.HBack = sourceTemp.HBack;
                temp.maxW = sourceTemp.maxW;
                temp.maxH = sourceTemp.maxH;
                temp.b = sourceTemp.b;
                temp.specMap = sourceTemp.specMap;
                temp.typeChangeMap = sourceTemp.typeChangeMap;
                if (mapId == MAP_SANH_HO_VE) {
                    temp.max_zone = (byte) 1;
                    temp.npcs = new ArrayList<>(); // Map 180 không có NPC nào
                }
            }
            return;
        }
        MapTemplate temp = new MapTemplate();
        temp.id = (short) mapId;
        temp.name = mapName;
        temp.max_zone = (byte) (mapId == MAP_SANH_HO_VE ? 1 : 5);
        temp.max_player = 50;
        temp.data = sourceTemp.data;
        temp.IDBack = sourceTemp.IDBack;
        temp.HBack = sourceTemp.HBack;
        temp.maxW = sourceTemp.maxW;
        temp.maxH = sourceTemp.maxH;
        temp.type_view_p = 0;
        temp.b = sourceTemp.b;
        temp.specMap = sourceTemp.specMap;
        temp.id_eff_map = sourceTemp.id_eff_map;
        temp.level = 1;
        temp.typeChangeMap = sourceTemp.typeChangeMap;
        temp.mPosMapTrain = sourceTemp.mPosMapTrain;
        temp.strTimeChange = sourceTemp.strTimeChange;
        temp.list_boat = new ArrayList<>();
        temp.vgos = new ArrayList<>();
        temp.npcs = new ArrayList<>();

        if (mapId != MAP_SANH_HO_VE) {
            // NPC Chuyển khu (-7)
            Npc npcKhu = new Npc();
            npcKhu.iditem = -7;
            npcKhu.name = " ";
            npcKhu.namegt = "Chuyển khu";
            npcKhu.chat = "";
            npcKhu.x = 130;
            npcKhu.y = 160;
            npcKhu.isPerson = 99;
            npcKhu.typeIcon = -1;
            npcKhu.wBlock = 24;
            npcKhu.hBlock = 24;
            npcKhu.b3 = 0;
            npcKhu.dataFrame = new byte[] { 25, 1 };
            temp.npcs.add(npcKhu);

            // NPC Sự kiện
            Npc npcEvent = new Npc();
            npcEvent.iditem = (short) npcId;
            npcEvent.name = npcName;
            npcEvent.namegt = "Thủ Lĩnh Biển Khơi";
            npcEvent.chat = "Chào mừng bạn đến với sự kiện Thủ Lĩnh Biển Khơi!";
            npcEvent.x = (short) npcX;
            npcEvent.y = (short) npcY;
            npcEvent.isPerson = 1;
            npcEvent.typeIcon = 0;
            npcEvent.wBlock = 0;
            npcEvent.hBlock = 0;
            npcEvent.b3 = 0;
            npcEvent.dataFrame = new byte[] { 71, 2 };
            npcEvent.head = 0;
            npcEvent.hair = 0;
            npcEvent.wearing = new short[0];
            temp.npcs.add(npcEvent);
        }

        MapTemplate.ENTRYS.add(temp);

        Map[] maps = new Map[temp.max_zone];
        for (int z = 0; z < maps.length; z++) {
            maps[z] = new Map();
            maps[z].zone_id = (byte) z;
            maps[z].template = temp;
            maps[z].list_mob = new int[0]; // KHÔNG CÓ MOBS
            maps[z].start_map();
        }
        Map.ENTRYS.add(maps);
    }

    private void setupSeaMap(int mapId, String mapName, MapTemplate sourceTemp, Map sourceMap) {
        Map[] existing = Map.get_map_by_id(mapId);
        if (existing != null && existing.length > 0) {
            MapTemplate existingTemp = existing[0].template;
            if (existingTemp != null) {
                // Xoá toàn bộ NPC trong map Biển (không có NPC và không chuyển khu)
                existingTemp.npcs = new ArrayList<>();
                existingTemp.max_zone = (byte) 1;

                // Đồng bộ và đặt lại đúng máu gốc theo template cho toàn bộ quái trong map Biển
                for (Map m : existing) {
                    if (m != null && m.list_mob != null) {
                        for (int mId : m.list_mob) {
                            Mob mob = Mob.ENTRYS.get(mId);
                            if (mob != null && mob.mob_template != null) {
                                mob.hp_max = mob.mob_template.hp_max;
                                mob.hp = mob.mob_template.hp_max;
                                mob.level = mob.mob_template.level;
                                mob.phong_thu = 0;
                                mob.mien_thuong = 0;
                                mob.ne_don = 0;
                                mob.phan_dame = 0;
                                mob.max_dame_per_hit = 0;
                                mob.giam_mien_thuong = 0;
                            }
                        }
                    }
                }

                // Cổng dịch chuyển về Sảnh hộ vệ (x=400, y=276)
                if (existingTemp.vgos != null && !existingTemp.vgos.isEmpty()) {
                    for (Vgo v : existingTemp.vgos) {
                        v.id_map_go = (short) MAP_SANH_HO_VE;
                        v.map_go = Map.get_map_by_id(MAP_SANH_HO_VE);
                        v.xnew = 400;
                        v.ynew = 276;
                    }
                }
            }
            return;
        }
        MapTemplate temp = new MapTemplate();
        temp.id = (short) mapId;
        temp.name = mapName;
        temp.max_zone = 1; // Chỉ có 1 khu duy nhất, không có chuyển khu
        temp.max_player = 30;
        temp.data = sourceTemp.data;
        temp.IDBack = sourceTemp.IDBack;
        temp.HBack = sourceTemp.HBack;
        temp.maxW = sourceTemp.maxW;
        temp.maxH = sourceTemp.maxH;
        temp.type_view_p = 0;
        temp.b = sourceTemp.b;
        temp.specMap = 0;
        temp.id_eff_map = sourceTemp.id_eff_map;
        temp.level = sourceTemp.level;
        temp.typeChangeMap = sourceTemp.typeChangeMap;
        temp.mPosMapTrain = sourceTemp.mPosMapTrain;
        temp.strTimeChange = sourceTemp.strTimeChange;
        temp.list_boat = new ArrayList<>();
        temp.vgos = new ArrayList<>();
        temp.npcs = new ArrayList<>(); // Không có NPC

        // Vgo Cổng trái và phải về Sảnh hộ vệ (Map 180)
        Vgo vgoLeft = new Vgo();
        vgoLeft.id_map_go = (short) MAP_SANH_HO_VE;
        vgoLeft.xold = 24;
        vgoLeft.yold = 288;
        vgoLeft.xnew = 400;
        vgoLeft.ynew = 276;
        vgoLeft.map_go = Map.get_map_by_id(MAP_SANH_HO_VE);
        temp.vgos.add(vgoLeft);

        Vgo vgoRight = new Vgo();
        vgoRight.id_map_go = (short) MAP_SANH_HO_VE;
        vgoRight.xold = 1056;
        vgoRight.yold = 288;
        vgoRight.xnew = 400;
        vgoRight.ynew = 276;
        vgoRight.map_go = Map.get_map_by_id(MAP_SANH_HO_VE);
        temp.vgos.add(vgoRight);

        MapTemplate.ENTRYS.add(temp);

        Map[] maps = new Map[temp.max_zone];
        for (int z = 0; z < maps.length; z++) {
            maps[z] = new Map();
            maps[z].zone_id = (byte) z;
            maps[z].template = temp;

            // Clone độc lập danh sách mob từ map nguồn với đúng máu gốc
            if (sourceMap.list_mob != null) {
                maps[z].list_mob = new int[sourceMap.list_mob.length];
                for (int m = 0; m < sourceMap.list_mob.length; m++) {
                    Mob originalMob = Mob.ENTRYS.get(sourceMap.list_mob[m]);
                    if (originalMob != null && originalMob.mob_template != null) {
                        Mob clonedMob = new Mob();
                        int newIndex = Manager.gI().getIndexMob();
                        Manager.gI().setIndexMob(newIndex + 1);
                        clonedMob.index = newIndex;
                        clonedMob.mob_template = originalMob.mob_template;
                        clonedMob.x = originalMob.x;
                        clonedMob.y = originalMob.y;
                        clonedMob.level = originalMob.mob_template.level;
                        clonedMob.hp_max = originalMob.mob_template.hp_max;
                        clonedMob.hp = originalMob.mob_template.hp_max;
                        clonedMob.phong_thu = 0;
                        clonedMob.mien_thuong = 0;
                        clonedMob.ne_don = 0;
                        clonedMob.phan_dame = 0;
                        clonedMob.max_dame_per_hit = 0;
                        clonedMob.giam_mien_thuong = 0;
                        clonedMob.map = maps[z];
                        clonedMob.isdie = false;
                        clonedMob.id_target = -1;
                        Mob.ENTRYS.put(newIndex, clonedMob);
                        maps[z].list_mob[m] = newIndex;
                    }
                }
            } else {
                maps[z].list_mob = new int[0];
            }
            maps[z].start_map();
        }
        Map.ENTRYS.add(maps);
    }

    /**
     * Nạp dữ liệu sự kiện từ DB (nếu server vừa restart trong lúc đang diễn ra event).
     */
    private void loadCurrentEventFromDB() {
        String weekKey = getCurrentWeekKey();
        String query = "SELECT * FROM `sea_leader_event` WHERE `week_key` = ? ORDER BY `id` DESC LIMIT 1;";
        try (Connection conn = SQL.gI().getCon();
             PreparedStatement ps = conn.prepareStatement(query)) {
            ps.setString(1, weekKey);
            try (ResultSet rs = ps.executeQuery()) {
                if (rs.next()) {
                    currentEvent.setId(rs.getInt("id"));
                    currentEvent.setWeekKey(rs.getString("week_key"));
                    currentEvent.setStatus(rs.getString("status"));
                    currentEvent.setWinnerSeaId(rs.getInt("winner_sea_id"));
                    currentEvent.setWinnerClanId(rs.getInt("winner_clan_id"));
                    currentEvent.setWinnerClanName(rs.getString("winner_clan_name"));
                    currentEvent.setWinningScore(rs.getInt("winning_score"));

                    // Nếu sự kiện đang ACTIVE, khôi phục trạng thái 4 biển và điểm số
                    if (SeaLeaderEvent.STATUS_ACTIVE.equals(currentEvent.getStatus())) {
                        loadRegistrationsFromDB(currentEvent.getId());
                        System.out.println("[SeaLeaderManager] Khôi phục thành công sự kiện đang ACTIVE từ DB (Round #" + currentEvent.getId() + ")");
                    }
                }
            }
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Load Event DB Error: " + e.getMessage());
        }
    }

    private void loadRegistrationsFromDB(int eventId) {
        String query = "SELECT * FROM `sea_leader_registration` WHERE `event_id` = ?;";
        try (Connection conn = SQL.gI().getCon();
             PreparedStatement ps = conn.prepareStatement(query)) {
            ps.setInt(1, eventId);
            try (ResultSet rs = ps.executeQuery()) {
                while (rs.next()) {
                    int seaId = rs.getInt("sea_id");
                    int clanId = rs.getInt("clan_id");
                    String clanName = rs.getString("clan_name");
                    int score = rs.getInt("score");
                    String statusStr = rs.getString("status");

                    SeaLeaderSea sea = getSea(seaId);
                    if (sea != null) {
                        sea.occupy(clanId, clanName);
                        sea.setScore(score);
                        sea.setStatus("FULL".equalsIgnoreCase(statusStr) ? SeaStatus.FULL : SeaStatus.EMPTY);
                    }
                }
            }
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Load Registrations DB Error: " + e.getMessage());
        }
    }

    private boolean forceActive = false;

    public void setForceActive(boolean forceActive) {
        this.forceActive = forceActive;
    }

    public boolean isForceActive() {
        return forceActive;
    }

    /**
     * Vòng lặp kiểm tra thời gian sự kiện (30 giây chạy 1 lần).
     * Mở: Thứ 6 từ 23:00 đến 00:00 (Thứ 7).
     */
    @Override
    public void run() {
        try {
            if (forceActive) {
                return; // Đang chạy chế độ test hoặc admin kích hoạt thủ công
            }
            boolean shouldBeOpen = isWithinEventSchedule();
            if (shouldBeOpen) {
                if (!isActive()) {
                    startEvent();
                }
            } else {
                if (isActive()) {
                    endEvent();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Kiểm tra thời gian: Mở 24/24 nếu OPEN_24_7_TEST = true, hoặc Thứ 6 từ 23:00 đến 00:00 server time.
     * (Trong Joda-Time: 5 = Thứ 6, giờ = 23).
     */
    public static boolean isWithinEventSchedule() {
        if (OPEN_24_7_TEST) {
            return true;
        }
        org.joda.time.DateTime now = org.joda.time.DateTime.now();
        int day = now.getDayOfWeek();
        int hour = now.getHourOfDay();
        return (day == 5 && hour == 23);
    }

    public synchronized boolean isActive() {
        return currentEvent != null && SeaLeaderEvent.STATUS_ACTIVE.equals(currentEvent.getStatus());
    }

    /**
     * Bắt đầu vòng event mới lúc 23:00 Thứ 6 (hoặc qua lệnh test/admin).
     */
    public synchronized void startEvent() {
        if (!isWithinEventSchedule()) {
            this.forceActive = true;
        }

        // Reset toàn bộ 4 biển
        for (SeaLeaderSea sea : seas) {
            sea.reset();
        }
        killedMobThisCycle.clear();

        currentEvent = new SeaLeaderEvent();
        currentEvent.setWeekKey(getCurrentWeekKey());
        currentEvent.setStatus(SeaLeaderEvent.STATUS_ACTIVE);
        currentEvent.setStartTime(System.currentTimeMillis());

        // Ghi nhận vào DB
        String insertSql = "INSERT INTO `sea_leader_event` (`week_key`, `start_time`, `status`) VALUES (?, NOW(), 'ACTIVE');";
        try (Connection conn = SQL.gI().getCon();
             PreparedStatement ps = conn.prepareStatement(insertSql, Statement.RETURN_GENERATED_KEYS)) {
            ps.setString(1, currentEvent.getWeekKey());
            ps.executeUpdate();
            try (ResultSet rs = ps.getGeneratedKeys()) {
                if (rs.next()) {
                    currentEvent.setId(rs.getInt(1));
                }
            }
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Start Event DB Error: " + e.getMessage());
        }

        System.out.println("[SEA_EVENT] Event started! (Round #" + currentEvent.getId() + ")");
        try {
            Manager.gI().chatKTG(0, "🌊 SỰ KIỆN THỦ LĨNH BIỂN KHƠI ĐÃ BẮT ĐẦU! (23:00 - 00:00) 🌊\nHãy đến gặp Mihao tại Làng để đăng ký chiếm biển cho Băng!", 5);
        } catch (Exception ignored) {}
    }

    /**
     * Kết thúc event đúng lúc 00:00.
     */
    public synchronized void endEvent() {
        this.forceActive = false;
        if (!isActive()) {
            return;
        }
        currentEvent.setStatus(SeaLeaderEvent.STATUS_ENDED);
        currentEvent.setEndTime(System.currentTimeMillis());

        // Xác định biển chiến thắng (có tie-break)
        SeaLeaderSea winnerSea = determineWinningSea();
        if (winnerSea != null && winnerSea.isFull()) {
            currentEvent.setWinnerSeaId(winnerSea.getSeaId());
            currentEvent.setWinnerClanId(winnerSea.getClanId());
            currentEvent.setWinnerClanName(winnerSea.getClanName());
            currentEvent.setWinningScore(winnerSea.getScore());
        }

        // Cập nhật DB
        String updateSql = "UPDATE `sea_leader_event` SET `status` = 'ENDED', `end_time` = NOW(), "
                + "`winner_sea_id` = ?, `winner_clan_id` = ?, `winner_clan_name` = ?, `winning_score` = ? "
                + "WHERE `id` = ?;";
        try (Connection conn = SQL.gI().getCon();
             PreparedStatement ps = conn.prepareStatement(updateSql)) {
            ps.setInt(1, currentEvent.getWinnerSeaId());
            ps.setInt(2, currentEvent.getWinnerClanId());
            ps.setString(3, currentEvent.getWinnerClanName());
            ps.setInt(4, currentEvent.getWinningScore());
            ps.setInt(5, currentEvent.getId());
            ps.executeUpdate();
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] End Event DB Error: " + e.getMessage());
        }

        System.out.println("[SEA_EVENT] Event ended! Winner = "
                + (winnerSea != null ? (winnerSea.getName() + " (Clan " + currentEvent.getWinnerClanName() + ", " + currentEvent.getWinningScore() + " điểm)") : "Không có"));

        // Thông báo toàn server
        try {
            if (winnerSea != null && winnerSea.isFull()) {
                Manager.gI().chatKTG(0, "🌊 SỰ KIỆN THỦ LĨNH BIỂN KHƠI ĐÃ KẾT THÚC! 🌊\nChiến thắng thuộc về: "
                        + winnerSea.getName() + " - Băng " + currentEvent.getWinnerClanName() + " (" + currentEvent.getWinningScore() + " điểm)!", 5);
            } else {
                Manager.gI().chatKTG(0, "🌊 SỰ KIỆN THỦ LĨNH BIỂN KHƠI ĐÃ KẾT THÚC! 🌊", 5);
            }
        } catch (Exception ignored) {}

        // Tự động trao thưởng trực tiếp cho toàn bộ thành viên Clan chiến thắng đang online
        distributeRewardToWinnerClan();

        // ĐƯA TOÀN BỘ NGƯỜI CHƠI TRONG MAP EVENT VỀ MAP 1 (LÀNG)
        kickAllPlayersToMap1();

        // Xóa danh sách mob chết chu kỳ
        killedMobThisCycle.clear();
    }

    /**
     * Quy tắc Tie-break xác định biển chiến thắng:
     * 1. Điểm số cao nhất.
     * 2. Nếu hòa điểm -> Biển nào đạt mốc điểm đó trước (lastScoreTime nhỏ hơn) sẽ thắng.
     * 3. Nếu vẫn hòa -> Ưu tiên theo thứ tự biển: Đông (0) > Bắc (1) > Tây (2) > Nam (3).
     */
    public synchronized SeaLeaderSea determineWinningSea() {
        SeaLeaderSea best = null;
        for (SeaLeaderSea sea : seas) {
            if (!sea.isFull()) {
                continue;
            }
            if (best == null) {
                best = sea;
            } else {
                if (sea.getScore() > best.getScore()) {
                    best = sea;
                } else if (sea.getScore() == best.getScore() && sea.getScore() > 0) {
                    if (sea.getLastScoreTime() < best.getLastScoreTime()) {
                        best = sea;
                    } else if (sea.getLastScoreTime() == best.getLastScoreTime()) {
                        if (sea.getSeaId() < best.getSeaId()) {
                            best = sea;
                        }
                    }
                }
            }
        }
        return best;
    }

    /**
     * Dịch chuyển toàn bộ người chơi đang ở các map event (178 - 183) về Map 1 (Làng).
     */
    public void kickAllPlayersToMap1() {
        Map[] map1 = Map.get_map_by_id(1);
        if (map1 == null || map1.length == 0) {
            return;
        }
        int[] eventMapIds = new int[] { MAP_SANH_CHIEN, MAP_SANH_HO_VE, MAP_BIEN_DONG, MAP_BIEN_BAC, MAP_BIEN_TAY, MAP_BIEN_NAM };
        for (int mId : eventMapIds) {
            Map[] maps = Map.get_map_by_id(mId);
            if (maps != null) {
                for (Map m : maps) {
                    if (m != null && m.players != null) {
                        List<Player> listCopy = new ArrayList<>(m.players);
                        for (Player p : listCopy) {
                            if (p != null && p.conn != null && p.conn.connected) {
                                try {
                                    Vgo vgo = new Vgo();
                                    vgo.map_go = map1;
                                    vgo.xnew = 200;
                                    vgo.ynew = 200;
                                    p.goto_map(vgo);
                                    clearSeaScoreboard(p);
                                    Service.send_box_ThongBao_OK(p, "Sự kiện Thủ Lĩnh Biển Khơi đã kết thúc! Bạn được đưa về Làng.");
                                } catch (Exception e) {
                                    System.err.println("[SeaLeaderManager] Error moving player to map 1: " + e.getMessage());
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /**
     * Kiểm tra player có được phép ở trong map event hay không (dùng khi login / reconnect).
     */
    public boolean canEnter(Player p, int mapId) {
        if (!isActive()) {
            return false;
        }
        if (p == null) {
            return false;
        }
        Clan c = p.clan != null ? p.clan : Clan.get_my_clan(p.name);
        if (c == null) {
            return false;
        }
        if (mapId == MAP_SANH_CHIEN || mapId == MAP_SANH_HO_VE) {
            return true;
        }
        SeaLeaderSea sea = getSeaByMapId(mapId);
        return (sea != null && sea.isFull() && sea.getClanId() == c.id);
    }

    // =========================================================================
    // LUẬT ĐĂNG KÝ (KHÔNG CÓ QUEUE - THREAD SAFE - ATOMIC)
    // =========================================================================

    public static class RegisterResult {
        public final boolean success;
        public final String message;

        public RegisterResult(boolean success, String message) {
            this.success = success;
            this.message = message;
        }
    }

    /**
     * Đăng ký Clan vào 1 Biển.
     * TUYỆT ĐỐI THREAD-SAFE (synchronized), KHÔNG QUEUE, KHÔNG HÀNG CHỜ.
     */
    public synchronized RegisterResult registerClan(Player p, SeaArea area) {
        if (p == null) {
            return new RegisterResult(false, "Lỗi người chơi không hợp lệ!");
        }
        if (p.clan == null) {
            return new RegisterResult(false, "Bạn cần tham gia Băng Hải Tặc để đăng ký Thủ lĩnh biển khơi!");
        }
        if (!isActive()) {
            return new RegisterResult(false, OPEN_24_7_TEST ? "Sự kiện Thủ lĩnh biển khơi hiện đang tạm đóng!" : "Sự kiện Thủ lĩnh biển khơi chỉ mở vào Thứ 6 từ 23:00 đến 00:00!");
        }

        // Kiểm tra xem cả 4 biển đã đầy chưa
        if (areAllSeasFull()) {
            return new RegisterResult(false, "Tất cả các biển hiện đã đầy!\nVui lòng quay lại khi có biển trống.");
        }

        // Kiểm tra Clan đã đăng ký biển nào chưa
        SeaLeaderSea registeredSea = getSeaOfClan(p.clan.id);
        if (registeredSea != null) {
            movePlayerToMap(p, MAP_SANH_CHIEN, 300, 200);
            return new RegisterResult(true, "Clan của bạn đã đăng ký " + registeredSea.getName() + "!\nĐang đưa bạn vào Sảnh chiến.");
        }

        // Kiểm tra biển được chọn
        SeaLeaderSea targetSea = getSea(area);
        if (targetSea == null) {
            return new RegisterResult(false, "Biển được chọn không hợp lệ!");
        }

        // NẾU BIỂN ĐẦY -> CHỈ TỪ CHỐI BIỂN ĐÓ, KHÔNG TỰ ĐỘNG CHUYỂN SANG BIỂN KHÁC
        if (targetSea.isFull()) {
            System.out.println("[SEA_EVENT] Clan " + p.clan.name + " tried to register full sea " + targetSea.getName());
            return new RegisterResult(false, targetSea.getName() + " đã đầy!\nVui lòng chọn biển khác.");
        }

        // ATOMIC ASSIGNMENT: EMPTY -> FULL
        targetSea.occupy(p.clan.id, p.clan.name);
        saveRegistrationToDB(targetSea);

        System.out.println("[SEA_EVENT] Clan " + p.clan.name + " registered " + targetSea.getName());

        // Đưa player vào Map 179 (Sảnh chiến)
        movePlayerToMap(p, MAP_SANH_CHIEN, 300, 200);

        return new RegisterResult(true, "Đăng ký " + targetSea.getName() + " thành công!");
    }

    private void saveRegistrationToDB(SeaLeaderSea sea) {
        if (currentEvent == null || currentEvent.getId() <= 0) {
            return;
        }
        String sql = "INSERT INTO `sea_leader_registration` (`event_id`, `sea_id`, `clan_id`, `clan_name`, `score`, `status`) "
                + "VALUES (?, ?, ?, ?, ?, 'FULL') "
                + "ON DUPLICATE KEY UPDATE `clan_id` = VALUES(`clan_id`), `clan_name` = VALUES(`clan_name`), `score` = VALUES(`score`), `status` = 'FULL';";
        try (Connection conn = SQL.gI().getCon();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, currentEvent.getId());
            ps.setInt(2, sea.getSeaId());
            ps.setInt(3, sea.getClanId());
            ps.setString(4, sea.getClanName());
            ps.setInt(5, sea.getScore());
            ps.executeUpdate();
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Save Registration DB Error: " + e.getMessage());
        }
    }

    /**
     * Khi 1 Clan bị giải tán: Giải phóng biển nếu Clan đó đang giữ biển.
     */
    public synchronized void onClanDisband(int clanId) {
        SeaLeaderSea sea = getSeaOfClan(clanId);
        if (sea != null) {
            System.out.println("[SEA_EVENT] Clan " + clanId + " disbanded. Releasing " + sea.getName() + " to EMPTY!");
            sea.release();

            // Cập nhật DB
            if (currentEvent != null && currentEvent.getId() > 0) {
                String sql = "DELETE FROM `sea_leader_registration` WHERE `event_id` = ? AND `sea_id` = ?;";
                try (Connection conn = SQL.gI().getCon();
                     PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setInt(1, currentEvent.getId());
                    ps.setInt(2, sea.getSeaId());
                    ps.executeUpdate();
                } catch (Exception e) {
                    System.err.println("[SeaLeaderManager] Disband DB Error: " + e.getMessage());
                }
            }
        }
    }

    // =========================================================================
    // XỬ LÝ MOB DEATH & TÍNH ĐIỂM
    // =========================================================================

    /**
     * Xử lý khi mob chết trong map.
     * Hook gọi từ Map.java tại khối if (mob_target.hp <= 0 && !mob_target.isdie).
     */
    public void onMobKilled(Player killer, Map map, Mob mob) {
        if (!isActive()) {
            return;
        }
        if (killer == null || killer.clan == null || map == null || mob == null) {
            return;
        }

        // Kiểm tra xem map có phải map biển sự kiện không
        SeaLeaderSea sea = getSeaByMapId(map.template.id);
        if (sea == null || !sea.isFull()) {
            return;
        }

        // Player phải thuộc Clan đang chiếm giữ biển này
        if (sea.getClanId() != killer.clan.id) {
            return;
        }

        // Chống trùng lặp mob death (1 mob chỉ được tính 1 lần trong chu kỳ sống)
        int mobUid = (map.template.id << 16) | (mob.index & 0xFFFF);
        if (!killedMobThisCycle.add(mobUid)) {
            return; // Đã xử lý rồi, bỏ qua
        }

        // Cộng 1 điểm cho Biển
        sea.addScore(1);

        // Cập nhật DB bất đồng bộ hoặc theo chu kỳ
        updateScoreInDB(sea);

        // Phát sóng bảng điểm cập nhật cho toàn bộ người chơi trong sự kiện (hiển thị 4 khung bên phải)
        broadcastSeaScoreboard();
    }

    /**
     * Khi mob hồi sinh (Map.java), xóa UID khỏi killedMobThisCycle để chu kỳ sau có thể tính lại.
     */
    public void onMobRespawn(Map map, Mob mob) {
        if (map != null && mob != null) {
            int mobUid = (map.template.id << 16) | (mob.index & 0xFFFF);
            killedMobThisCycle.remove(mobUid);
        }
    }

    private void updateScoreInDB(SeaLeaderSea sea) {
        if (currentEvent == null || currentEvent.getId() <= 0) {
            return;
        }
        String sql = "UPDATE `sea_leader_registration` SET `score` = ? WHERE `event_id` = ? AND `sea_id` = ?;";
        try (Connection conn = SQL.gI().getCon();
             PreparedStatement ps = conn.prepareStatement(sql)) {
            ps.setInt(1, sea.getScore());
            ps.setInt(2, currentEvent.getId());
            ps.setInt(3, sea.getSeaId());
            ps.executeUpdate();
        } catch (Exception ignored) {}
    }

    // =========================================================================
    // MENU & GIAO DIỆN NGƯỜI CHƠI
    // =========================================================================

    /**
     * Hiển thị Menu chọn Biển (Thủ Lĩnh Biển Khơi) hoặc vào thẳng map nếu Clan đã đăng ký.
     */
    public void showSeaSelectMenu(Player p) {
        if (p == null || p.conn == null) {
            return;
        }
        if (p.clan == null) {
            p.clan = Clan.get_my_clan(p.name);
        }
        if (p.clan == null) {
            try {
                Service.send_box_ThongBao_OK(p, "Bạn cần tham gia Băng Hải Tặc để đăng ký Thủ lĩnh biển khơi!");
            } catch (Exception ignored) {}
            return;
        }

        // KIỂM TRA: Nếu Clan ĐÃ ĐĂNG KÝ BIỂN RỒI -> VÀO THẲNG MAP SẢNH CHIẾN, KHÔNG HIỂN THỊ MENU BIỂN NỮA
        SeaLeaderSea registeredSea = getSeaOfClan(p.clan.id);
        if (registeredSea != null) {
            if (!isActive()) {
                try {
                    Service.send_box_ThongBao_OK(p, OPEN_24_7_TEST 
                            ? "Sự kiện Thủ lĩnh biển khơi hiện đang tạm đóng!" 
                            : "Sự kiện Thủ lĩnh biển khơi chỉ mở vào Thứ 6 từ 23:00 đến 00:00!");
                } catch (Exception ignored) {}
                return;
            }
            movePlayerToMap(p, MAP_SANH_CHIEN, 300, 200);
            return;
        }

        if (!isActive()) {
            try {
                Service.send_box_ThongBao_OK(p, OPEN_24_7_TEST 
                        ? "Sự kiện Thủ lĩnh biển khơi hiện đang tạm đóng!" 
                        : "Sự kiện Thủ lĩnh biển khơi chỉ mở vào Thứ 6 từ 23:00 đến 00:00!");
            } catch (Exception ignored) {}
            return;
        }

        // Xoay vòng hiển thị: Đông -> Bắc -> Tây -> Nam (chỉ hiển thị khi Clan CHƯA đăng ký biển nào)
        String[] menuItems = new String[] {
                "Biển Đông - " + seas[0].getStatus().getDescription(),
                "Biển Bắc - " + seas[1].getStatus().getDescription(),
                "Biển Tây - " + seas[2].getStatus().getDescription(),
                "Biển Nam - " + seas[3].getStatus().getDescription()
        };

        try {
            MenuController.send_dynamic_menu(p, MENU_ID_SELECT_SEA, "🌊 THỦ LĨNH BIỂN KHƠI", menuItems, null);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Xử lý lựa chọn trong Menu chọn Biển (idNPC = 9841).
     */
    public void handleSeaMenu(Player p, byte index) {
        if (p == null || p.conn == null) {
            return;
        }

        if (index >= 0 && index <= 3) {
            // Đăng ký biển
            SeaArea area = SeaArea.getById(index);
            if (area != null) {
                RegisterResult result = registerClan(p, area);
                try {
                    Service.send_box_ThongBao_OK(p, result.message);
                } catch (Exception ignored) {}
            }
        }
    }
    /**
     * Gửi bảng điểm 4 Biển (Message 62) hiển thị 4 khung bên phải màn hình:
     * - Biển Đông
     * - Biển Tây
     * - Biển Nam
     * - Biển Bắc
     * Kèm số điểm màu đỏ, đúng như ảnh thiết kế.
     */
    public void sendSeaScoreboard(Player p) {
        if (p == null || p.conn == null) {
            return;
        }
        try {
            Message m = new Message(62);
            m.writer().writeByte(4); // 4 khung bảng điểm

            // Thứ tự hiển thị 4 biển theo giao diện: Biển Đông, Biển Tây, Biển Nam, Biển Bắc
            SeaArea[] displayOrder = new SeaArea[] {
                    SeaArea.BIEN_DONG,
                    SeaArea.BIEN_TAY,
                    SeaArea.BIEN_NAM,
                    SeaArea.BIEN_BAC
            };

            for (SeaArea area : displayOrder) {
                SeaLeaderSea sea = getSea(area);
                int score = (sea != null) ? sea.getScore() : 0;
                m.writer().writeUTF(area.getName());
                m.writer().writeShort(-1); // idIcon = -1 (không icon clan, chỉ chữ + điểm số)
                m.writer().writeInt(score); // xp: Điểm số hiện tại (hiển thị màu đỏ)
            }
            p.conn.addmsg(m);
            m.cleanup();
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Error sending sea scoreboard: " + e.getMessage());
        }
    }

    /**
     * Phát sóng bảng điểm 4 Biển cập nhật cho toàn bộ người chơi trong các map sự kiện.
     */
    public void broadcastSeaScoreboard() {
        int[] eventMapIds = new int[] { MAP_SANH_CHIEN, MAP_SANH_HO_VE, MAP_BIEN_DONG, MAP_BIEN_BAC, MAP_BIEN_TAY, MAP_BIEN_NAM };
        for (int mapId : eventMapIds) {
            Map[] maps = Map.get_map_by_id(mapId);
            if (maps != null) {
                for (Map m : maps) {
                    if (m != null && m.players != null) {
                        for (Player p : m.players) {
                            if (p != null && p.conn != null && p.conn.connected) {
                                sendSeaScoreboard(p);
                            }
                        }
                    }
                }
            }
        }
    }

    /**
     * Xóa bảng điểm 4 Biển trên màn hình của người chơi khi rời khỏi sự kiện.
     */
    public void clearSeaScoreboard(Player p) {
        if (p == null || p.conn == null) {
            return;
        }
        try {
            Message m = new Message(62);
            m.writer().writeByte(0); // 0 khung -> client xóa sạch vecClanDam
            p.conn.addmsg(m);
            m.cleanup();
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Error clearing sea scoreboard: " + e.getMessage());
        }
    }

    /**
     * Hiển thị Bảng Điểm 4 Biển bên phải màn hình (Message 62).
     */
    public void showScoreboard(Player p) {
        sendSeaScoreboard(p);
    }

    /**
     * Nhận phần thưởng cho thành viên Băng chiến thắng.
     */
    public void handleClaimReward(Player p) {
        if (p == null || p.conn == null) {
            return;
        }
        if (currentEvent == null || currentEvent.getId() <= 0) {
            try {
                Service.send_box_ThongBao_OK(p, "Chưa có sự kiện nào kết thúc để nhận thưởng!");
            } catch (Exception ignored) {}
            return;
        }
        if (isActive()) {
            try {
                Service.send_box_ThongBao_OK(p, "Sự kiện đang diễn ra, phần thưởng sẽ được trao sau khi kết thúc lúc 00:00!");
            } catch (Exception ignored) {}
            return;
        }
        if (currentEvent.getWinnerClanId() <= 0) {
            try {
                Service.send_box_ThongBao_OK(p, "Đợt sự kiện trước không có Băng Hải Tặc nào chiến thắng!");
            } catch (Exception ignored) {}
            return;
        }

        SeaLeaderReward.claimReward(p, currentEvent.getId(), currentEvent.getWinnerClanId(), currentEvent.getWinnerClanName());
    }

    /**
     * Tự động trao thưởng trực tiếp cho toàn bộ thành viên Clan chiến thắng đang online.
     */
    public void distributeRewardToWinnerClan() {
        if (currentEvent == null || currentEvent.getId() <= 0) {
            return;
        }
        int winnerClanId = currentEvent.getWinnerClanId();
        String winnerClanName = currentEvent.getWinnerClanName();
        if (winnerClanId <= 0) {
            return;
        }
        Clan winnerClan = Clan.get_clan_by_id(winnerClanId);
        if (winnerClan != null && winnerClan.members != null) {
            for (Clan_member mem : winnerClan.members) {
                Player p = Map.get_player_by_name_allmap(mem.name);
                if (p != null && p.conn != null) {
                    SeaLeaderReward.claimReward(p, currentEvent.getId(), winnerClanId, winnerClanName);
                }
            }
        }
    }

    /**
     * Tự động kiểm tra và trao thưởng cho thành viên Clan chiến thắng khi đăng nhập (nếu lúc kết thúc đang offline).
     */
    public void checkAndGrantRewardOnLogin(Player p) {
        if (p == null || p.conn == null || p.clan == null) {
            return;
        }
        if (currentEvent == null || currentEvent.getId() <= 0) {
            return;
        }
        int winnerClanId = currentEvent.getWinnerClanId();
        if (winnerClanId > 0 && p.clan.id == winnerClanId) {
            if (!SeaLeaderReward.hasClaimedReward(currentEvent.getId(), p.name)) {
                SeaLeaderReward.claimReward(p, currentEvent.getId(), winnerClanId, currentEvent.getWinnerClanName());
            }
        }
    }

    /**
     * Xử lý tương tác Menu NPC trong Map 179 (Sứ Giả Biển Khơi).
     */
    public void handleNpcSanhChien(Player p, byte index) {
        if (index == 0) {
            // Đến Sảnh hộ vệ (Map 180)
            movePlayerToMap(p, MAP_SANH_HO_VE, 60, 250);
        } else if (index == 1) {
            showScoreboard(p);
        } else if (index == 2) {
            // Rời sự kiện về Làng (Map 1)
            movePlayerToMap(p, 1, 200, 200);
        }
    }

    /**
     * Xử lý tương tác Menu NPC trong Map 180 (Hộ Vệ Trưởng).
     */
    public void handleNpcSanhHoVe(Player p, byte index) {
        if (index == 0) {
            // Tiến vào biển của Clan
            if (p.clan == null) {
                p.clan = Clan.get_my_clan(p.name);
            }
            if (p.clan == null) {
                try {
                    Service.send_box_ThongBao_OK(p, "Bạn không thuộc Băng Hải Tặc nào!");
                } catch (Exception ignored) {}
                return;
            }
            SeaLeaderSea sea = getSeaOfClan(p.clan.id);
            if (sea == null) {
                try {
                    Service.send_box_ThongBao_OK(p, "Clan của bạn chưa đăng ký biển nào!");
                } catch (Exception ignored) {}
                return;
            }
            movePlayerToMap(p, sea.getMapId(), 60, 288);
        } else if (index == 1) {
            showScoreboard(p);
        } else if (index == 2) {
            // Quay lại Sảnh chiến (Map 179)
            movePlayerToMap(p, MAP_SANH_CHIEN, 1240, 276);
        } else if (index == 3) {
            // Về Làng (Map 1)
            movePlayerToMap(p, 1, 200, 200);
        }
    }

    /**
     * Dịch chuyển player an toàn đến Map mong muốn.
     */
    public void movePlayerToMap(Player p, int targetMapId, int x, int y) {
        if (p == null || p.conn == null) {
            return;
        }
        Map[] targetMaps = Map.get_map_by_id(targetMapId);
        if (targetMaps == null || targetMaps.length == 0 || targetMaps[0] == null) {
            try {
                Service.send_box_ThongBao_OK(p, "Không tìm thấy dữ liệu Map " + targetMapId + "!");
            } catch (Exception ignored) {}
            return;
        }
        try {
            Vgo vgo = new Vgo();
            vgo.map_go = targetMaps;
            vgo.xnew = (short) x;
            vgo.ynew = (short) y;
            p.goto_map(vgo);
        } catch (Exception e) {
            System.err.println("[SeaLeaderManager] Error moving player to map " + targetMapId + ": " + e.getMessage());
        }
    }

    // =========================================================================
    // GETTERS & HELPERS
    // =========================================================================

    public SeaLeaderSea getSea(SeaArea area) {
        if (area == null) return null;
        return seas[area.getId()];
    }

    public SeaLeaderSea getSea(int seaId) {
        if (seaId >= 0 && seaId < seas.length) {
            return seas[seaId];
        }
        return null;
    }

    public SeaLeaderSea getSeaByMapId(int mapId) {
        for (SeaLeaderSea sea : seas) {
            if (sea.getMapId() == mapId) {
                return sea;
            }
        }
        return null;
    }

    public SeaLeaderSea getSeaOfClan(int clanId) {
        if (clanId <= 0) return null;
        for (SeaLeaderSea sea : seas) {
            if (sea.isFull() && sea.getClanId() == clanId) {
                return sea;
            }
        }
        return null;
    }

    public boolean areAllSeasFull() {
        for (SeaLeaderSea sea : seas) {
            if (sea.isEmpty()) {
                return false;
            }
        }
        return true;
    }

    public boolean isEventSeaMap(int mapId) {
        return (mapId == MAP_BIEN_DONG || mapId == MAP_BIEN_BAC || mapId == MAP_BIEN_TAY || mapId == MAP_BIEN_NAM);
    }

    public boolean isEventMap(int mapId) {
        return (mapId >= MAP_BIEN_BAC && mapId <= MAP_BIEN_TAY); // 178 - 183
    }

    private String getCurrentWeekKey() {
        org.joda.time.DateTime now = org.joda.time.DateTime.now();
        return now.getWeekyear() + "-W" + String.format("%02d", now.getWeekOfWeekyear());
    }

    public SeaLeaderEvent getCurrentEvent() {
        return currentEvent;
    }
}
