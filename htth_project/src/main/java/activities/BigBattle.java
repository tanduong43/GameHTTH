package activities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import client.Player;
import client.Quest;
import core.BXH;
import core.Manager;
import core.Service;
import core.Util;
import io.Message;
import map.Map;
import map.MapTemplate;
import map.Npc;
import map.Vgo;
import template.InfoMemList;
import template.Map_pvp;

/**
 * Quản lý tính năng Trận Chiến Lớn (NPC Zosaku):
 * - Phân chia sảnh chờ theo Bracket Level (Lv 20-39, Lv 40-69, Lv 70+)
 * - NPC Image 5014 tại sảnh chờ (BXH Chuỗi Thắng, Nhận Thưởng, Rời Sảnh)
 * - Tự động ghép trận ngẫu nhiên (không ghép lại cặp đã đấu trong ngày)
 * - Đấu PvP tại map 120, 122, 123 (ai thắng trước 3 hiệp sẽ thắng chung cuộc)
 * - Ghi nhận chuỗi thắng, phát thông báo toàn server khi chiến thắng
 */
public class BigBattle {

    public static final int BRACKET_1 = 1; // Lv 20 - 39 (Nền Map 17 - Orange)
    public static final int BRACKET_2 = 2; // Lv 40 - 69 (Nền Map 33 - Baratie)
    public static final int BRACKET_3 = 3; // Lv 70+     (Nền Map 69 - Mỏm Sinh Đôi)

    public static final short NPC_ID_BIG_BATTLE = -996;

    // ID Map Sảnh Chờ cố định trong Database SQL (table maps)
    public static final int MAP_WAITING_BRACKET_1 = 2030; // Nền Orange
    public static final int MAP_WAITING_BRACKET_2 = 2031; // Nền Baratie
    public static final int MAP_WAITING_BRACKET_3 = 2032; // Nền Whiskay

    // Template map ID fallback cho từng Bracket
    public static final int MAP_TEMPLATE_BRACKET_1 = 17;
    public static final int MAP_TEMPLATE_BRACKET_2 = 33;
    public static final int MAP_TEMPLATE_BRACKET_3 = 69;

    // Các map PvP dùng để thi đấu
    public static final short[] PVP_MAP_IDS = new short[] { 120, 122, 123 };

    // Sảnh chờ cho từng Bracket (Key: Bracket ID, Value: Map Instance)
    private static final java.util.Map<Integer, Map> WAITING_MAPS = new ConcurrentHashMap<>();

    // Lịch sử các cặp đã đấu trong ngày: "minId_maxId"
    public static final Set<String> MATCHED_PAIRS_TODAY = ConcurrentHashMap.newKeySet();

    // BXH Top Chuỗi Thắng (In-memory cache)
    public static final List<InfoMemList> TOP_STREAK = new ArrayList<>();

    // Scheduler cho ghép trận tự động
    private static ScheduledExecutorService scheduler;
    private static boolean isInitialized = false;

    /**
     * Khởi tạo hệ thống Trận Chiến Lớn
     */
    public static synchronized void init() {
        if (isInitialized) {
            return;
        }
        isInitialized = true;
        scheduler = Executors.newSingleThreadScheduledExecutor();
        // Quét ghép trận mỗi 5 giây
        scheduler.scheduleWithFixedDelay(() -> {
            try {
                runMatchmaking();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }, 5, 5, TimeUnit.SECONDS);

        System.out.println("[BigBattle] He thong Tran Chien Lon da khoi dong thanh cong!");
    }

    /**
     * Reset lịch sử cặp đấu hàng ngày (lúc 0h00 hoặc khi bảo trì)
     */
    public static void resetDailyMatches() {
        MATCHED_PAIRS_TODAY.clear();
        System.out.println("[BigBattle] Da reset lich su cap dau Tran Chien Lon hom nay!");
    }

    /**
     * Xác định phân khúc cấp độ (Bracket) của người chơi
     */
    public static int getBracket(Player p) {
        if (p == null || p.level < 20) {
            return 0;
        }
        if (p.level <= 39) {
            return BRACKET_1;
        }
        if (p.level <= 69) {
            return BRACKET_2;
        }
        return BRACKET_3;
    }

    /**
     * Lấy hoặc tạo Map Sảnh Chờ theo Bracket
     */
    public static synchronized Map getOrCreateWaitingMap(int bracket) {
        Map existingMap = WAITING_MAPS.get(bracket);
        if (existingMap != null) {
            return existingMap;
        }

        int sqlMapId = (bracket == BRACKET_1) ? MAP_WAITING_BRACKET_1 : ((bracket == BRACKET_2) ? MAP_WAITING_BRACKET_2 : MAP_WAITING_BRACKET_3);
        Map[] sqlMapArr = Map.get_map_by_id(sqlMapId);
        if (sqlMapArr != null && sqlMapArr.length > 0 && sqlMapArr[0] != null) {
            WAITING_MAPS.put(bracket, sqlMapArr[0]);
            return sqlMapArr[0];
        }

        int templateId = MAP_TEMPLATE_BRACKET_1;
        if (bracket == BRACKET_2) {
            templateId = MAP_TEMPLATE_BRACKET_2;
        } else if (bracket == BRACKET_3) {
            templateId = MAP_TEMPLATE_BRACKET_3;
        }

        Map[] baseMapArr = Map.get_map_by_id(templateId);
        if (baseMapArr == null || baseMapArr.length == 0) {
            baseMapArr = Map.get_map_by_id(1); // Fallback map 1
        }
        Map baseMap = baseMapArr[0];

        // Tạo Map instance riêng cho sảnh chờ
        Map waitMap = new Map();
        MapTemplate temp = new MapTemplate();
        temp.id = baseMap.template.id;
        temp.name = "Sảnh Chờ - Trận Chiến Lớn (" + (bracket == BRACKET_1 ? "20-39" : (bracket == BRACKET_2 ? "40-69" : "70+")) + ")";
        temp.max_zone = 1;
        temp.max_player = 50;
        temp.vgos = new ArrayList<>();
        temp.npcs = new ArrayList<>();
        temp.list_boat = new ArrayList<>();
        temp.IDBack = baseMap.template.IDBack;
        temp.HBack = baseMap.template.HBack;
        temp.maxW = baseMap.template.maxW;
        temp.maxH = baseMap.template.maxH;
        temp.data = baseMap.template.data;
        temp.id_eff_map = baseMap.template.id_eff_map;
        temp.level = baseMap.template.level;
        temp.typeChangeMap = baseMap.template.typeChangeMap;

        // Tạo NPC Image 5014 đứng tại sảnh chờ
        Npc npc = new Npc();
        npc.iditem = NPC_ID_BIG_BATTLE;
        npc.name = "Đô Đốc";
        npc.namegt = "Trận Chiến Lớn";
        npc.chat = "Đấu trường đỉnh cao - Vinh quang hải tặc!";
        npc.x = (short) (temp.maxW > 0 ? (temp.maxW / 2) : 480);
        npc.y = 240;
        npc.isPerson = 1;
        npc.typeIcon = 0;
        npc.wBlock = 0;
        npc.hBlock = 0;
        npc.b3 = 1;
        npc.head = 0;
        npc.hair = 0;
        npc.wearing = new short[] { 423 }; // Part 423 chứa image 5014 trong parts.csv
        npc.dataFrame = new byte[] { 71, 2 };
        temp.npcs.add(npc);

        waitMap.template = temp;
        waitMap.zone_id = 0;
        waitMap.list_mob = new int[0];
        waitMap.start_map();
        Map.add_map_plus(waitMap);

        WAITING_MAPS.put(bracket, waitMap);
        return waitMap;
    }

    /**
     * Người chơi đăng ký vào Sảnh Chờ từ NPC Zosaku
     */
    public static void joinWaitingRoom(Player p) throws IOException {
        if (p.level < 20) {
            Service.send_box_ThongBao_OK(p, "Bạn cần đạt cấp độ 20 trở lên để tham gia Trận Chiến Lớn!");
            return;
        }
        if (p.dungeon != null) {
            Service.send_box_ThongBao_OK(p, "Bạn đang trong phó bản, hãy thoát ra trước!");
            return;
        }
        if (p.bossHunt != null) {
            Service.send_box_ThongBao_OK(p, "Bạn đang trong Săn Trùm, hãy thoát ra trước!");
            return;
        }
        if (p.ship_pet != null) {
            Service.send_box_ThongBao_OK(p, "Không thể tham gia khi đang vận buôn!");
            return;
        }
        if (p.trade_target != null) {
            Service.send_box_ThongBao_OK(p, "Không thể tham gia khi đang giao dịch!");
            return;
        }
        if (p.isdie) {
            Service.send_box_ThongBao_OK(p, "Bạn đang kiệt sức, hãy hồi phục trước!");
            return;
        }

        int bracket = getBracket(p);
        p.big_battle_bracket = bracket;

        // Lưu vị trí làng xuất phát
        if (p.map != null && !isWaitingMap(p.map)) {
            p.originalMapId = p.map.template.id;
            p.originalX = p.x;
            p.originalY = p.y;
            p.map.leave_map(p, 2);
        }

        Map waitMap = getOrCreateWaitingMap(bracket);
        Vgo vgo = new Vgo();
        vgo.map_go = new Map[] { waitMap };
        vgo.xnew = (short) (waitMap.template.maxW > 0 ? (waitMap.template.maxW / 2 - 40) : 440);
        vgo.ynew = 240;
        p.goto_map(vgo);

        Service.send_box_ThongBao_OK(p,
                "Chào mừng bạn đến với Sảnh Chờ Trận Chiến Lớn (Cấp "
                        + (bracket == BRACKET_1 ? "20-39" : (bracket == BRACKET_2 ? "40-69" : "70+"))
                        + ")!\n\nHệ thống đang tự động tìm kiếm đối thủ phù hợp. Bạn hãy đợi trong giây lát hoặc gặp NPC Đô Đốc để xem BXH & Nhận thưởng.");
    }

    /**
     * Kiểm tra xem map có phải là Sảnh Chờ Trận Chiến Lớn hay không
     */
    public static boolean isWaitingMap(Map map) {
        if (map == null || map.template == null) return false;
        int id = map.template.id;
        if (id == MAP_WAITING_BRACKET_1 || id == MAP_WAITING_BRACKET_2 || id == MAP_WAITING_BRACKET_3) {
            return true;
        }
        for (Map waitMap : WAITING_MAPS.values()) {
            if (waitMap != null && waitMap.equals(map)) {
                return true;
            }
        }
        return false;
    }

    /**
     * Tạo khóa cặp đấu duy nhất cho 2 người chơi
     */
    public static String getPairKey(int id1, int id2) {
        int min = Math.min(id1, id2);
        int max = Math.max(id1, id2);
        return min + "_" + max;
    }

    /**
     * Luồng ghép trận tự động (quét mỗi 5 giây)
     */
    public static void runMatchmaking() {
        for (int bracket = BRACKET_1; bracket <= BRACKET_3; bracket++) {
            Map waitMap = WAITING_MAPS.get(bracket);
            if (waitMap == null || waitMap.players.size() < 2) {
                continue;
            }

            // Lọc danh sách người chơi sẵn sàng
            List<Player> readyPlayers = new ArrayList<>();
            synchronized (waitMap.players) {
                for (Player pl : waitMap.players) {
                    if (pl != null && pl.conn != null && pl.conn.connected && !pl.isdie && !pl.isBot
                            && pl.targetFight == null && (pl.map != null && pl.map.equals(waitMap))) {
                        readyPlayers.add(pl);
                    }
                }
            }

            if (readyPlayers.size() < 2) {
                continue;
            }

            // Xáo trộn ngẫu nhiên để công bằng
            Collections.shuffle(readyPlayers);

            // Tìm các cặp hợp lệ (chưa từng đấu nhau trong ngày)
            List<Player> matchedThisRound = new ArrayList<>();
            for (int i = 0; i < readyPlayers.size(); i++) {
                Player p1 = readyPlayers.get(i);
                if (matchedThisRound.contains(p1)) continue;

                for (int j = i + 1; j < readyPlayers.size(); j++) {
                    Player p2 = readyPlayers.get(j);
                    if (matchedThisRound.contains(p2)) continue;

                    String pairKey = getPairKey(p1.id, p2.id);
                    if (!MATCHED_PAIRS_TODAY.contains(pairKey)) {
                        // Tìm thấy cặp hợp lệ!
                        MATCHED_PAIRS_TODAY.add(pairKey);
                        matchedThisRound.add(p1);
                        matchedThisRound.add(p2);

                        // Ghép trận đưa vào map PvP
                        short mapId = PVP_MAP_IDS[Util.random(PVP_MAP_IDS.length)];
                        startBattle(p1, p2, mapId, bracket);
                        break;
                    }
                }
            }
        }
    }

    /**
     * Khởi tạo phòng đấu PvP (Map 120, 122 hoặc 123) và đưa 2 người chơi vào
     */
    private static void startBattle(Player p1, Player p2, short mapId, int bracket) {
        try {
            p1.targetFight = p2;
            p2.targetFight = p1;
            p1.big_battle_bracket = bracket;
            p2.big_battle_bracket = bracket;

            if (p1.map != null) {
                p1.map.leave_map(p1, 2);
            }
            if (p2.map != null) {
                p2.map.leave_map(p2, 2);
            }

            p1.type_pk = -1;
            p2.type_pk = -1;
            p1.isdie = false;
            p2.isdie = false;

            // Lấy template map PvP
            Map[] basePvpMaps = Map.get_map_by_id(mapId);
            Map baseMap = (basePvpMaps != null && basePvpMaps.length > 0) ? basePvpMaps[0] : null;
            if (baseMap == null) {
                System.err.println("[BigBattle] Khong tim thay map PvP id: " + mapId);
                returnToWaitingRoom(p1);
                returnToWaitingRoom(p2);
                return;
            }

            // Tạo instance map đấu riêng biệt cho cặp này
            Map battleMap = new Map();
            battleMap.template = baseMap.template;
            battleMap.zone_id = 0;
            battleMap.list_mob = new int[0];

            battleMap.map_pvp = new Map_pvp();
            battleMap.map_pvp.time_pvp = 5; // 5s đếm ngược trước khi bắt đầu
            battleMap.map_pvp.status_pvp = 0;
            battleMap.map_pvp.num_win_p1 = 0;
            battleMap.map_pvp.num_win_p2 = 0;
            battleMap.map_pvp.type_map = 4; // 4 = TRẬN CHIẾN LỚN

            // Đưa P1 vào map
            p1.map = battleMap;
            p1.x = 320;
            p1.y = 240;
            p1.xold = p1.x;
            p1.yold = p1.y;
            battleMap.goto_map(p1);
            Service.update_PK(p1, p1, true);
            Service.pet(p1, p1, true);
            Quest.update_map_have_side_quest(p1, true);

            // Đưa P2 vào map
            p2.map = battleMap;
            p2.x = 380;
            p2.y = 240;
            p2.xold = p2.x;
            p2.yold = p2.y;
            battleMap.goto_map(p2);
            Service.update_PK(p2, p2, true);
            Service.pet(p2, p2, true);
            Quest.update_map_have_side_quest(p2, true);

            battleMap.start_map();
            Map.add_map_plus(battleMap);

            Service.send_box_ThongBao_OK(p1, "Trận Chiến Lớn đã bắt đầu!\nĐối thủ của bạn: " + p2.name + " (Cấp " + p2.level + ").\nAi thắng trước 3 hiệp sẽ thắng chung cuộc!");
            Service.send_box_ThongBao_OK(p2, "Trận Chiến Lớn đã bắt đầu!\nĐối thủ của bạn: " + p1.name + " (Cấp " + p1.level + ").\nAi thắng trước 3 hiệp sẽ thắng chung cuộc!");

        } catch (Exception e) {
            e.printStackTrace();
            p1.targetFight = null;
            p2.targetFight = null;
            returnToWaitingRoom(p1);
            returnToWaitingRoom(p2);
        }
    }

    /**
     * Xử lý khi trận đấu Trận Chiến Lớn kết thúc
     */
    public static void onBattleEnd(Player winner, Player loser) {
        if (winner == null || loser == null) {
            return;
        }

        // Tăng chuỗi thắng cho người thắng
        winner.big_battle_streak++;
        if (winner.big_battle_streak > winner.big_battle_max_streak) {
            winner.big_battle_max_streak = winner.big_battle_streak;
        }
        winner.big_battle_total_win++;
        winner.big_battle_total_fight++;

        // Reset chuỗi thắng của người thua
        loser.big_battle_streak = 0;
        loser.big_battle_total_fight++;

        // Cập nhật BXH
        updateBXH(winner);

        // Lưu dữ liệu nhân vật
        try {
            Player.flush(winner, false);
            Player.flush(loser, false);
        } catch (Exception ignored) {}

        // Thông báo toàn server theo đúng cú pháp yêu cầu
        String msg = "⚔️ [Trận Chiến Lớn]: Người chơi " + winner.name + " đã thắng Người chơi " + loser.name
                + " tăng chuỗi thắng lên " + winner.big_battle_streak + " trận.";
        try {
            Manager.gI().chatKTG(0, msg, 5);
        } catch (Exception ignored) {}

        // Hồi máu đầy đủ và thông báo riêng cho từng người
        try {
            winner.isdie = false;
            winner.hp = winner.body.get_hp_max(true);
            winner.mp = winner.body.get_mp_max(true);
            Service.send_box_ThongBao_OK(winner, "Chúc mừng bạn đã chiến thắng!\nChuỗi thắng hiện tại: " + winner.big_battle_streak + " trận.");

            loser.isdie = false;
            loser.hp = loser.body.get_hp_max(true);
            loser.mp = loser.body.get_mp_max(true);
            Service.send_box_ThongBao_OK(loser, "Bạn đã thất bại trong Trận Chiến Lớn! Chuỗi thắng đã bị đặt lại về 0.");
        } catch (Exception ignored) {}

        // Đưa cả hai quay trở lại Sảnh Chờ
        returnToWaitingRoom(winner);
        returnToWaitingRoom(loser);
    }

    /**
     * Đưa người chơi trở lại Sảnh Chờ tương ứng với Bracket
     */
    public static void returnToWaitingRoom(Player p) {
        if (p == null || p.conn == null || !p.conn.connected) {
            return;
        }
        p.targetFight = null;
        p.type_pk = -1;
        p.isdie = false;
        try {
            p.hp = p.body.get_hp_max(true);
            p.mp = p.body.get_mp_max(true);
            int bracket = p.big_battle_bracket > 0 ? p.big_battle_bracket : getBracket(p);
            Map waitMap = getOrCreateWaitingMap(bracket);
            Vgo vgo = new Vgo();
            vgo.map_go = new Map[] { waitMap };
            vgo.xnew = (short) (waitMap.template.maxW > 0 ? (waitMap.template.maxW / 2 - 40) : 440);
            vgo.ynew = 240;
            p.goto_map(vgo);
            Service.update_PK(p, p, true);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Rời Sảnh Chờ quay về làng xuất phát
     */
    public static void leaveWaitingRoom(Player p) throws IOException {
        p.targetFight = null;
        p.type_pk = -1;
        int targetMapId = p.originalMapId > 0 ? p.originalMapId : 1;
        Map[] targetMaps = Map.get_map_by_id(targetMapId);
        if (targetMaps == null || targetMaps.length == 0) {
            targetMaps = Map.get_map_by_id(1); // Làng cối xay gió
        }

        Vgo vgo = new Vgo();
        vgo.map_go = targetMaps;
        vgo.xnew = p.originalX > 0 ? p.originalX : 611;
        vgo.ynew = p.originalY > 0 ? p.originalY : 250;
        p.goto_map(vgo);
        Service.update_PK(p, p, true);
        Service.send_box_ThongBao_OK(p, "Bạn đã rời khỏi Sảnh Chờ Trận Chiến Lớn.");
    }

    /**
     * Gửi Menu tương tác của NPC Image 5014 tại Sảnh Chờ
     */
    public static void sendNpcMenu(Player p) throws IOException {
        core.MenuController.send_dynamic_menu(p, NPC_ID_BIG_BATTLE, "Đô Đốc Trận Chiến Lớn",
                new String[] { "Bảng xếp hạng", "Nhận thưởng chuỗi thắng", "Thông tin cá nhân", "Rời sảnh chờ" },
                new short[] { 170, 110, 134, 111 });
    }

    /**
     * Xử lý lựa chọn từ Menu NPC Image 5014
     */
    public static void handleNpcMenu(Player p, byte index) throws IOException {
        switch (index) {
            case 0: { // Bảng xếp hạng chuỗi thắng
                sendBXH(p, 0);
                break;
            }
            case 1: { // Nhận thưởng chuỗi thắng
                showRewardMenu(p);
                break;
            }
            case 2: { // Thông tin cá nhân
                String info = "⚔️ THÔNG TIN TRẬN CHIẾN LỚN ⚔️\n\n"
                        + "• Chuỗi thắng hiện tại: " + p.big_battle_streak + " trận\n"
                        + "• Kỷ lục chuỗi thắng: " + p.big_battle_max_streak + " trận\n"
                        + "• Tổng số trận thắng: " + p.big_battle_total_win + " trận\n"
                        + "• Tổng số trận đã đấu: " + p.big_battle_total_fight + " trận\n"
                        + "• Phân khúc cấp độ: Nhóm " + (p.big_battle_bracket == BRACKET_1 ? "20-39" : (p.big_battle_bracket == BRACKET_2 ? "40-69" : "70+"));
                Service.send_box_ThongBao_OK(p, info);
                break;
            }
            case 3: { // Rời sảnh chờ
                Service.send_box_yesno(p, 1998, "Xác nhận", "Bạn có chắc chắn muốn rời khỏi Sảnh Chờ và quay về làng?",
                        new String[] { "Đồng ý", "Hủy" }, new byte[] { 2, 1 });
                break;
            }
        }
    }

    /**
     * Cập nhật BXH Top Chuỗi Thắng
     */
    public static synchronized void updateBXH(Player p) {
        if (p == null) return;
        InfoMemList existing = null;
        for (InfoMemList item : TOP_STREAK) {
            if (item.name.equals(p.name)) {
                existing = item;
                break;
            }
        }

        if (existing == null) {
            existing = new InfoMemList();
            existing.id = p.id;
            existing.name = p.name;
            TOP_STREAK.add(existing);
        }

        existing.level = (short) p.level;
        existing.head = (short) p.get_head();
        existing.hair = (short) p.get_hair();
        existing.hat = p.get_hat();
        existing.thongthao = p.big_battle_max_streak;
        existing.info = "Chuỗi: " + p.big_battle_max_streak + " trận (Hiện tại: " + p.big_battle_streak + ")";

        // Sắp xếp giảm dần theo kỷ lục chuỗi thắng
        TOP_STREAK.sort((a, b) -> Long.compare(b.thongthao, a.thongthao));

        // Cập nhật lại rank
        for (int i = 0; i < TOP_STREAK.size(); i++) {
            TOP_STREAK.get(i).rank = (short) (i + 1);
        }
    }

    /**
     * Gửi giao diện Bảng Xếp Hạng dạng chuẩn Cao Thủ (Packet Message -30)
     */
    public static void sendBXH(Player p, int page) throws IOException {
        if (page < 0) page = 0;
        int pageSize = 10;
        int total = TOP_STREAK.size();
        int bound1 = page * pageSize;
        int bound2 = Math.min(bound1 + pageSize, total);

        if (bound1 >= total && total > 0) {
            page = (total - 1) / pageSize;
            bound1 = page * pageSize;
            bound2 = total;
        }

        Message m = new Message(-30);
        m.writer().writeByte(19); // Type 19: Giao diện danh sách Cao Thủ
        m.writer().writeUTF("Top Chuỗi Thắng");
        m.writer().writeByte(page);
        m.writer().writeByte(Math.max(0, bound2 - bound1));

        for (int i = bound1; i < bound2; i++) {
            InfoMemList temp = TOP_STREAK.get(i);
            InfoMemList.WriteInfoMemList(m.writer(), temp);
        }
        p.conn.addmsg(m);
        m.cleanup();
    }

    /**
     * Hiển thị menu Nhận Thưởng Chuỗi Thắng theo mốc
     */
    public static void showRewardMenu(Player p) throws IOException {
        String[] menuItems = new String[] {
                "Mốc 3 trận thắng " + (p.big_battle_claimed_rewards.contains(3) ? "(Đã nhận)" : (p.big_battle_max_streak >= 3 ? "(Có thể nhận)" : "(Chưa đạt)")),
                "Mốc 5 trận thắng " + (p.big_battle_claimed_rewards.contains(5) ? "(Đã nhận)" : (p.big_battle_max_streak >= 5 ? "(Có thể nhận)" : "(Chưa đạt)")),
                "Mốc 10 trận thắng " + (p.big_battle_claimed_rewards.contains(10) ? "(Đã nhận)" : (p.big_battle_max_streak >= 10 ? "(Có thể nhận)" : "(Chưa đạt)")),
                "Mốc 15 trận thắng " + (p.big_battle_claimed_rewards.contains(15) ? "(Đã nhận)" : (p.big_battle_max_streak >= 15 ? "(Có thể nhận)" : "(Chưa đạt)")),
                "Mốc 20 trận thắng " + (p.big_battle_claimed_rewards.contains(20) ? "(Đã nhận)" : (p.big_battle_max_streak >= 20 ? "(Có thể nhận)" : "(Chưa đạt)"))
        };

        core.MenuController.send_dynamic_menu(p, -9970, "Nhận Thưởng Chuỗi Thắng", menuItems,
                new short[] { 110, 110, 110, 110, 110 });
    }

    /**
     * Xử lý nhận quà từng mốc chuỗi thắng
     */
    public static void claimReward(Player p, byte index) throws IOException {
        int[] milestones = new int[] { 3, 5, 10, 15, 20 };
        if (index < 0 || index >= milestones.length) {
            return;
        }

        int milestone = milestones[index];
        if (p.big_battle_claimed_rewards.contains(milestone)) {
            Service.send_box_ThongBao_OK(p, "Bạn đã nhận phần thưởng cho mốc " + milestone + " trận thắng rồi!");
            return;
        }

        if (p.big_battle_max_streak < milestone) {
            Service.send_box_ThongBao_OK(p, "Bạn cần đạt kỷ lục chuỗi thắng tối thiểu " + milestone + " trận để nhận mốc này! Hiện tại: " + p.big_battle_max_streak + " trận.");
            return;
        }

        p.big_battle_claimed_rewards.add(milestone);
        long beriReward = 0;
        int rubyReward = 0;
        String rewardText = "";

        switch (milestone) {
            case 3:
                beriReward = 100_000L;
                rubyReward = 10;
                rewardText = "100.000 Beri, 10 Ruby";
                break;
            case 5:
                beriReward = 300_000L;
                rubyReward = 30;
                rewardText = "300.000 Beri, 30 Ruby";
                break;
            case 10:
                beriReward = 1_000_000L;
                rubyReward = 100;
                rewardText = "1.000.000 Beri, 100 Ruby";
                break;
            case 15:
                beriReward = 3_000_000L;
                rubyReward = 300;
                rewardText = "3.000.000 Beri, 300 Ruby";
                break;
            case 20:
                beriReward = 10_000_000L;
                rubyReward = 1000;
                rewardText = "10.000.000 Beri, 1000 Ruby";
                break;
        }

        if (beriReward > 0) {
            p.update_vang(beriReward);
        }
        if (rubyReward > 0) {
            p.update_ngoc(rubyReward);
        }
        p.update_money();

        Service.send_box_ThongBao_OK(p, "🎉 Chúc mừng bạn đã nhận thưởng Mốc " + milestone + " trận thắng:\n" + rewardText + "!");
        Player.flush(p, false);
    }
}