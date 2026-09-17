package activities;

import client.Party;
import client.Player;
import core.Service;
import core.Util;
import io.Message;
import map.Map;
import map.Mob;
import map.Vgo;
import template.GiftBox;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;

/**
 * Quản lý Phó Bản Chiến Trường 5vs5 Phá Trụ (MOBA Hải Tặc)
 * - 5 Bản đồ:
 *   + 129: Làng đỏ (Căn Cứ Phe Đỏ - Trụ Chính A)
 *   + 130: Làng xanh (Căn Cứ Phe Xanh - Trụ Chính B)
 *   + 131: Đường trên (Trụ Thường A, Trụ Thường B)
 *   + 132: Đường giữa (Trụ Thường A, Trụ Thường B)
 *   + 133: Đường dưới (Trụ Thường A, Trụ Thường B)
 * - Cơ chế:
 *   + Nhóm 5 người
 *   + Cờ Đỏ (type_pk = 4) vs Cờ Xanh (type_pk = 5)
 *   + Trụ gây sát thương nếu địch đứng trong phạm vi 30
 *   + Phá hủy Trụ Chính đối phương trước sẽ thắng
 *   + Chết quay về map Trụ Chính, thời gian hồi sinh ban đầu 5s, mỗi lần chết +1s
 */
public class Battleground5v5 {
    public static final int MAP_RED_BASE = 129;   // Căn cứ Phe Đỏ
    public static final int MAP_BLUE_BASE = 130;  // Căn cứ Phe Xanh
    public static final int MAP_TOP = 131;        // Đường trên
    public static final int MAP_MID = 132;        // Đường giữa
    public static final int MAP_BOT = 133;        // Đường dưới

    public static final int MOB_TRU_THUONG_A = 122;
    public static final int MOB_TRU_CHINH_A  = 123;
    public static final int MOB_TRU_THUONG_B = 124;
    public static final int MOB_TRU_CHINH_B  = 125;

    public static final int TOWER_ATTACK_RANGE = 30; // Phạm vi gây dame của trụ: 30 pixel

    public static final int MAX_DAILY_TURNS = 5; // Giới hạn số lượt đi mỗi ngày

    // Mode trận đấu
    public static final int MODE_5V5 = 5;
    public static final int MODE_1V1 = 1;
    public int mode = MODE_5V5; // Mặc định 5v5

    // Quản lý danh sách các trận đấu đang diễn ra và hàng chờ ghép trận
    public static final List<Battleground5v5> ACTIVE_BATTLES = new CopyOnWriteArrayList<>();
    public static final List<Party> WAITING_QUEUE = new CopyOnWriteArrayList<>();

    // Hàng chờ ghép trận 1v1 (từng Player riêng lẻ, không cần Party)
    public static final List<Player> WAITING_QUEUE_1V1 = new CopyOnWriteArrayList<>();

    // Các thành phần của 1 instance trận đấu
    public Map[] maps = new Map[5]; // 0: 129, 1: 130, 2: 131, 3: 132, 4: 133
    public List<Mob> towers = new CopyOnWriteArrayList<>();
    public Mob mainTowerA; // Trụ chính Phe Đỏ
    public Mob mainTowerB; // Trụ chính Phe Xanh

    public static final java.util.Map<Integer, int[][]> DEFAULT_TOWERS = new java.util.HashMap<>();
    static {
        // Map 129 (Làng đỏ - Phe Đỏ): Trụ Chính A (123)
        DEFAULT_TOWERS.put(MAP_RED_BASE, new int[][] { { MOB_TRU_CHINH_A, 250, 288 } });
        // Map 130 (Làng xanh - Phe Xanh): Trụ Chính B (125)
        DEFAULT_TOWERS.put(MAP_BLUE_BASE, new int[][] { { MOB_TRU_CHINH_B, 806, 288 } });
        // Map 131 (Đường trên): Trụ Thường A (122) và Trụ Thường B (124)
        DEFAULT_TOWERS.put(MAP_TOP, new int[][] { { MOB_TRU_THUONG_A, 350, 288 }, { MOB_TRU_THUONG_B, 706, 288 } });
        // Map 132 (Đường giữa): Trụ Thường A (122) và Trụ Thường B (124)
        DEFAULT_TOWERS.put(MAP_MID, new int[][] { { MOB_TRU_THUONG_A, 350, 288 }, { MOB_TRU_THUONG_B, 706, 288 } });
        // Map 133 (Đường dưới): Trụ Thường A (122) và Trụ Thường B (124)
        DEFAULT_TOWERS.put(MAP_BOT, new int[][] { { MOB_TRU_THUONG_A, 350, 288 }, { MOB_TRU_THUONG_B, 706, 288 } });
    }

    public static template.MobTemplate getMobTemplate(int mobId) {
        for (template.MobTemplate mt : template.MobTemplate.ENTRYS) {
            if (mt != null && mt.mob_id == mobId) {
                return mt;
            }
        }
        if (mobId >= 0 && mobId < template.MobTemplate.ENTRYS.size()) {
            return template.MobTemplate.ENTRYS.get(mobId);
        }
        return null;
    }

    /**
     * Khởi tạo trụ cho một map instance trong chiến trường, ưu tiên đọc từ mapTemplate,
     * nếu mapTemplate rỗng thì nạp từ DEFAULT_TOWERS để đảm bảo luôn luôn có trụ.
     */
    public int initTowersForMap(Map instance, Map mapTemplate, int mid, int mobIndexCounter) {
        boolean hasTowers = false;
        if (mapTemplate != null && mapTemplate.list_mob != null && mapTemplate.list_mob.length > 0) {
            for (int mobIdRef : mapTemplate.list_mob) {
                Mob tplMob = Mob.ENTRYS.get(mobIdRef);
                if (tplMob != null && tplMob.mob_template != null) {
                    Mob tower = new Mob();
                    tower.mob_template = tplMob.mob_template;
                    tower.x = tplMob.x;
                    tower.y = tplMob.y;
                    if (tower.mob_template.mob_id == MOB_TRU_CHINH_A || tower.mob_template.mob_id == MOB_TRU_CHINH_B) {
                        tower.hp_max = 200;
                    } else {
                        tower.hp_max = 100;
                    }
                    tower.hp = tower.hp_max;
                    tower.level = tplMob.level;
                    tower.isdie = false;
                    tower.id_target = -1;
                    tower.index = mobIndexCounter--;
                    tower.map = instance;
                    tower.boss_info = null;

                    this.towers.add(tower);
                    hasTowers = true;

                    if (tower.mob_template.mob_id == MOB_TRU_CHINH_A) {
                        this.mainTowerA = tower;
                    } else if (tower.mob_template.mob_id == MOB_TRU_CHINH_B) {
                        this.mainTowerB = tower;
                    }
                }
            }
        }

        // Tự động nạp trụ mặc định nếu map chưa có mob trong DB (Map 129 Làng đỏ, Map 130 Làng xanh...)
        if (!hasTowers && DEFAULT_TOWERS.containsKey(mid)) {
            int[][] defs = DEFAULT_TOWERS.get(mid);
            for (int[] def : defs) {
                int mobId = def[0];
                short tx = (short) def[1];
                short ty = (short) def[2];
                template.MobTemplate mt = getMobTemplate(mobId);
                if (mt != null) {
                    Mob tower = new Mob();
                    tower.mob_template = mt;
                    tower.x = tx;
                    tower.y = ty;
                    if (mobId == MOB_TRU_CHINH_A || mobId == MOB_TRU_CHINH_B) {
                        tower.hp_max = 200;
                    } else {
                        tower.hp_max = 100;
                    }
                    tower.hp = tower.hp_max;
                    tower.level = mt.level;
                    tower.isdie = false;
                    tower.id_target = -1;
                    tower.index = mobIndexCounter--;
                    tower.map = instance;
                    tower.boss_info = null;

                    this.towers.add(tower);

                    if (mobId == MOB_TRU_CHINH_A) {
                        this.mainTowerA = tower;
                    } else if (mobId == MOB_TRU_CHINH_B) {
                        this.mainTowerB = tower;
                    }
                }
            }
        }
        return mobIndexCounter;
    }

    public List<Player> teamA = new CopyOnWriteArrayList<>(); // Phe Đỏ (type_pk = 4)
    public List<Player> teamB = new CopyOnWriteArrayList<>(); // Phe Xanh (type_pk = 5)

    // Theo dõi số lần tử trận và thời điểm hồi sinh của từng người chơi
    public java.util.Map<Integer, Integer> deathCounts = new ConcurrentHashMap<>();
    public java.util.Map<Integer, Long> reviveTimes = new ConcurrentHashMap<>();
    // Theo dõi thời gian gửi cảnh báo trụ chính đang có khiên bảo vệ (chống spam thông báo)
    public java.util.Map<Integer, Long> lastProtectedWarning = new ConcurrentHashMap<>();

    public boolean isFinished = false;
    public boolean isNotifiedFinish = false;
    public volatile boolean isClosed = false;
    public long timeEnd = 0L;
    public long timeReturnVillage = 0L;
    public int winningTeam = 0; // 1: Phe Đỏ thắng, 2: Phe Xanh thắng

    public int scoreTeamA = 0; // Số kill hoặc điểm của Phe Đỏ
    public int scoreTeamB = 0; // Số kill hoặc điểm của Phe Xanh

    private long lastTowerCheck = 0L;

    public static void sendThongBao(Player p, String msg) {
        if (p == null || p.conn == null) return;
        try {
            Service.send_box_ThongBao_OK(p, msg);
        } catch (Exception ignored) {}
    }

    /**
     * Hiển thị Menu tại NPC Ngộ Không (-201)
     */
    public static void showMenu(Player p) {
        if (p == null || p.conn == null) return;
        try {
            p.change_new_date();
            int remain = Math.max(0, MAX_DAILY_TURNS - p.time_5vs5);
            core.MenuController.send_dynamic_menu(p, 9955, "Chiến Trường Phá Trụ (Còn " + remain + "/" + MAX_DAILY_TURNS + " lượt)",
                    new String[] {
                        "Đăng ký 5 vs 5 (Nhóm)",
                        "Hủy tìm trận 5 vs 5",
                        "Đăng ký 1 vs 1",
                        "Hủy đăng ký 1 vs 1",
                        "Luật chiến trường"
                    }, null);
        } catch (Exception ignored) {}
    }

    /**
     * Xử lý lựa chọn từ Menu
     */
    public static void handleMenu(Player p, int index) {
        if (p == null) return;
        switch (index) {
            case 0: // Đăng ký 5vs5
                registerQueue(p);
                break;
            case 1: // Hủy tìm trận 5vs5
                cancelQueue(p);
                break;
            case 2: // Đăng ký 1v1
                register1v1(p);
                break;
            case 3: // Hủy đăng ký 1v1
                cancel1v1(p);
                break;
            case 4: // Luật chiến trường
                showRules(p);
                break;
        }
    }

    /**
     * Hiển thị bảng luật chơi
     */
    public static void showRules(Player p) {
        p.change_new_date();
        int remain = Math.max(0, MAX_DAILY_TURNS - p.time_5vs5);
        String msg = "=== CHIẾN TRƯỜNG PHÁ TRỤ ===\n"
                + "- Giới hạn: Tối đa " + MAX_DAILY_TURNS + " lượt/ngày (Bạn còn: " + remain + "/" + MAX_DAILY_TURNS + " lượt).\n"
                + "- Cơ chế bảo vệ: Trụ Chính có khiên bất tử, chỉ có thể tấn công khi toàn bộ Trụ Phụ đối phương đã bị phá hủy!\n"
                + "--- CHẾ ĐỘ 5VS5 ---\n"
                + "- Đội hình: 5 người mỗi bên (Phe Đỏ vs Phe Xanh).\n"
                + "- Nhiệm vụ: Phá hết Trụ Phụ rồi phá Trụ Chính đối phương để giành chiến thắng.\n"
                + "- Đánh trụ mỗi lần trừ 1 HP. Trụ vỡ sẽ giữ nguyên trạng thái.\n"
                + "- Hồi sinh: Khi chết quay về Trụ Chính phe mình. Thời gian ban đầu 5 giây, mỗi lần chết +1 giây.\n"
                + "- Phần thưởng: Thắng nhận 1.500.000 Beri + 3.000 Ruby. Thua nhận 300.000 Beri + 600 Ruby.\n"
                + "--- CHẾ ĐỘ 1VS1 ---\n"
                + "- Không cần nhóm, chỉ 2 người chơi vào hàng chờ.\n"
                + "- Mỗi người một căn cứ riêng (Map 129 và Map 130).\n"
                + "- Phá hủy Trụ Phụ rồi phá Trụ Chính đối phương trước là thắng!\n"
                + "- Phần thưởng: Thắng nhận 500.000 Beri + 1.000 Ruby. Thua nhận 100.000 Beri + 200 Ruby.\n"
                + "- Thời gian tối đa: 10 phút.";
        sendThongBao(p, msg);
    }

    // ===================== CHẾ ĐỘ 1 VS 1 =====================

    /**
     * Đăng ký hàng chờ 1v1 (không cần Party, 2 người là bắt đầu)
     */
    public static synchronized void register1v1(Player p) {
        if (p == null) return;
        p.change_new_date();

        // Kiểm tra giới hạn lượt tham gia hôm nay
        if (p.time_5vs5 >= MAX_DAILY_TURNS) {
            sendThongBao(p, "Bạn đã tham gia đủ " + MAX_DAILY_TURNS + " lượt Chiến Trường hôm nay (tối đa " + MAX_DAILY_TURNS + " lần/ngày)!");
            return;
        }

        // Kiểm tra người chơi có đang trong trận khác không
        if (p.battleground5v5 != null) {
            sendThongBao(p, "Bạn đang trong trận đấu! Hãy kết thúc trận hiện tại trước.");
            return;
        }

        // Kiểm tra đã đăng ký chưa
        if (WAITING_QUEUE_1V1.contains(p)) {
            sendThongBao(p, "Bạn đã đăng ký hàng chờ 1vs1 rồi! Vui lòng chờ đối thủ.");
            return;
        }

        // Kiểm tra đã có trong queue 5v5 chưa
        for (Party party : WAITING_QUEUE) {
            if (party.list.contains(p)) {
                sendThongBao(p, "Bạn đang trong hàng chờ 5vs5! Hãy hủy 5vs5 trước.");
                return;
            }
        }

        WAITING_QUEUE_1V1.add(p);
        sendThongBao(p, "Đã đăng ký hàng chờ 1vs1! Đang tìm đối thủ... (" + WAITING_QUEUE_1V1.size() + " người đang chờ)");

        // Ghép trận nếu đủ 2 người
        if (WAITING_QUEUE_1V1.size() >= 2) {
            Player p1 = WAITING_QUEUE_1V1.remove(0);
            Player p2 = WAITING_QUEUE_1V1.remove(0);
            // Kiểm tra cả 2 vẫn online
            if (p1.conn == null || !p1.conn.connected) {
                sendThongBao(p2, "Đối thủ đã mất kết nối! Đã đưa bạn lại vào hàng chờ.");
                WAITING_QUEUE_1V1.add(0, p2);
                return;
            }
            if (p2.conn == null || !p2.conn.connected) {
                sendThongBao(p1, "Đối thủ đã mất kết nối! Đã đưa bạn lại vào hàng chờ.");
                WAITING_QUEUE_1V1.add(0, p1);
                return;
            }
            createAndLaunchBattle1v1(p1, p2);
        }
    }

    /**
     * Hủy hàng chờ 1v1
     */
    public static synchronized void cancel1v1(Player p) {
        if (p == null) return;
        if (WAITING_QUEUE_1V1.remove(p)) {
            sendThongBao(p, "Đã hủy đăng ký hàng chờ 1vs1.");
        } else {
            sendThongBao(p, "Bạn không có trong hàng chờ 1vs1!");
        }
    }

    /**
     * Tạo và khởi chạy trận 1v1: chỉ dùng 2 map căn cứ (129 = Phe Đỏ, 130 = Phe Xanh)
     */
    public static void createAndLaunchBattle1v1(Player p1, Player p2) {
        Battleground5v5 battle = new Battleground5v5();
        battle.mode = MODE_1V1;
        battle.timeEnd = System.currentTimeMillis() + 10 * 60 * 1000L; // Tối đa 10 phút

        // 1. Tạo đầy đủ 5 map instance để người chơi di chuyển qua các đường và phá Trụ Thường / Trụ Chính
        int[] mapIds1v1 = { MAP_RED_BASE, MAP_BLUE_BASE, MAP_TOP, MAP_MID, MAP_BOT };
        battle.maps = new Map[5];
        int mobIndexCounter = -6000;

        for (int idx = 0; idx < mapIds1v1.length; idx++) {
            int mid = mapIds1v1[idx];
            Map[] templates = Map.get_map_by_id(mid);
            if (templates == null || templates.length == 0) {
                System.err.println("[Battleground5v5-1v1] Error: Map template " + mid + " not found!");
                return;
            }
            Map mapTemplate = templates[0];
            Map instance = new Map();
            instance.template = mapTemplate.template;
            instance.zone_id = (byte) 0;
            instance.list_mob = new int[0];
            instance.map_battleground5v5 = battle;

            // Khởi tạo mob trụ (chỉ Trụ Chính cho 1v1)
            mobIndexCounter = battle.initTowersForMap(instance, mapTemplate, mid, mobIndexCounter);

            instance.start_map();
            Map.add_map_plus(instance);
            battle.maps[idx] = instance;
        }

        // 2. Phân phe: p1 = Team A (Đỏ), p2 = Team B (Xanh) và trừ lượt đi hôm nay
        p1.time_5vs5++;
        battle.teamA.add(p1);
        p1.battleground5v5 = battle;
        battle.deathCounts.put(p1.id, 0);

        p2.time_5vs5++;
        battle.teamB.add(p2);
        p2.battleground5v5 = battle;
        battle.deathCounts.put(p2.id, 0);

        ACTIVE_BATTLES.add(battle);

        // 3. Đưa người chơi vào căn cứ
        try {
            p1.type_pk = 4;
            if (p1.map != null) p1.map.change_flag(p1, 4);
            Vgo vgo1 = new Vgo();
            vgo1.map_go = new Map[] { battle.maps[0] };
            vgo1.xnew = 100;
            vgo1.ynew = 288;
            p1.goto_map(vgo1);
            sendThongBao(p1, "Trận 1vs1 bắt đầu! (Lượt " + p1.time_5vs5 + "/" + MAX_DAILY_TURNS + ")\nBạn là Phe Đỏ. Phá hủy Trụ Phụ rồi tiêu diệt Trụ Chính xanh của " + p2.name + " để chiến thắng!");
        } catch (Exception e) { e.printStackTrace(); }

        try {
            p2.type_pk = 5;
            if (p2.map != null) p2.map.change_flag(p2, 5);
            Vgo vgo2 = new Vgo();
            vgo2.map_go = new Map[] { battle.maps[1] };
            vgo2.xnew = 956;
            vgo2.ynew = 288;
            p2.goto_map(vgo2);
            sendThongBao(p2, "Trận 1vs1 bắt đầu! (Lượt " + p2.time_5vs5 + "/" + MAX_DAILY_TURNS + ")\nBạn là Phe Xanh. Phá hủy Trụ Phụ rồi tiêu diệt Trụ Chính đỏ của " + p1.name + " để chiến thắng!");
        } catch (Exception e) { e.printStackTrace(); }
    }

    // ===================== CHẾ ĐỘ 5 VS 5 =====================

    /**
     * Đăng ký hàng chờ ghép đội 5vs5
     */
    public static synchronized void registerQueue(Player p) {
        if (p == null) return;
        p.change_new_date();

        if (p.party == null) {
            sendThongBao(p, "Bạn cần phải tạo Nhóm và đủ đúng 5 thành viên mới có thể đăng ký!");
            return;
        }
        if (!p.party.list.get(0).equals(p)) {
            sendThongBao(p, "Chỉ có Trưởng nhóm mới có quyền đăng ký Chiến Trường 5vs5!");
            return;
        }
        if (p.party.list.size() != 5) {
            sendThongBao(p, "Nhóm của bạn hiện có " + p.party.list.size() + "/5 thành viên. Cần đúng 5 người!");
            return;
        }

        // Kiểm tra kết nối và lượt đi hôm nay của từng thành viên
        for (Player member : p.party.list) {
            if (member == null || member.conn == null || !member.conn.connected) {
                sendThongBao(p, "Có thành viên trong nhóm đang mất kết nối!");
                return;
            }
            member.change_new_date();
            if (member.time_5vs5 >= MAX_DAILY_TURNS) {
                if (member.equals(p)) {
                    sendThongBao(p, "Bạn đã tham gia đủ " + MAX_DAILY_TURNS + " lượt Chiến Trường hôm nay (tối đa " + MAX_DAILY_TURNS + " lần/ngày)!");
                } else {
                    sendThongBao(p, "Thành viên " + member.name + " đã hết lượt tham gia Chiến Trường hôm nay (đã đi " + MAX_DAILY_TURNS + "/" + MAX_DAILY_TURNS + " lần)!");
                }
                return;
            }
            if (member.map == null || member.map.template.id != p.map.template.id) {
                sendThongBao(p, "Tất cả 5 thành viên phải có mặt cùng map với Trưởng nhóm!");
                return;
            }
        }
        if (WAITING_QUEUE.contains(p.party)) {
            sendThongBao(p, "Nhóm của bạn đã ở trong hàng đợi ghép trận!");
            return;
        }

        WAITING_QUEUE.add(p.party);
        for (Player m : p.party.list) {
            sendThongBao(m, "Nhóm đã vào hàng chờ Chiến Trường 5vs5. Vui lòng đợi đối thủ!");
        }

        // Kiểm tra ghép trận nếu đủ 2 nhóm
        checkAndStartMatch();
    }

    /**
     * Hủy tìm trận
     */
    public static synchronized void cancelQueue(Player p) {
        if (p.party == null) {
            sendThongBao(p, "Bạn không có trong nhóm nào!");
            return;
        }
        if (!p.party.list.get(0).equals(p)) {
            sendThongBao(p, "Chỉ có Trưởng nhóm mới có quyền hủy tìm trận!");
            return;
        }
        if (WAITING_QUEUE.remove(p.party)) {
            for (Player m : p.party.list) {
                sendThongBao(m, "Trưởng nhóm đã hủy tìm trận Chiến Trường 5vs5.");
            }
        } else {
            sendThongBao(p, "Nhóm của bạn hiện không nằm trong hàng đợi!");
        }
    }

    /**
     * Bắt đầu trận đấu tập (Cho phép solo hoặc theo nhóm vào luyện tập phá trụ)
     */
    public static synchronized void startPracticeMatch(Player p) {
        if (p == null || p.conn == null || !p.conn.connected) return;
        p.change_new_date();
        if (p.time_5vs5 >= MAX_DAILY_TURNS) {
            sendThongBao(p, "Bạn đã tham gia đủ " + MAX_DAILY_TURNS + " lượt Chiến Trường hôm nay (tối đa " + MAX_DAILY_TURNS + " lần/ngày)!");
            return;
        }
        if (p.battleground5v5 != null) {
            sendThongBao(p, "Bạn đang trong trận đấu! Hãy kết thúc trận hiện tại trước.");
            return;
        }
        if (p.party != null) {
            if (!p.party.list.get(0).equals(p)) {
                sendThongBao(p, "Chỉ có Trưởng nhóm mới có thể bắt đầu trận đấu!");
                return;
            }
            for (Player member : p.party.list) {
                if (member == null || member.conn == null || !member.conn.connected) {
                    sendThongBao(p, "Có thành viên trong nhóm đang mất kết nối!");
                    return;
                }
                member.change_new_date();
                if (member.time_5vs5 >= MAX_DAILY_TURNS) {
                    if (member.equals(p)) {
                        sendThongBao(p, "Bạn đã tham gia đủ " + MAX_DAILY_TURNS + " lượt Chiến Trường hôm nay (tối đa " + MAX_DAILY_TURNS + " lần/ngày)!");
                    } else {
                        sendThongBao(p, "Thành viên " + member.name + " đã hết lượt tham gia Chiến Trường hôm nay!");
                    }
                    return;
                }
            }

            // Nếu có nhóm khác trong hàng chờ thì ghép đối đầu, ngược lại cho nhóm vào Phe Đỏ (Team A) để thử sức
            Party teamBParty = null;
            if (!WAITING_QUEUE.isEmpty()) {
                for (Party other : WAITING_QUEUE) {
                    if (!other.equals(p.party)) {
                        teamBParty = other;
                        WAITING_QUEUE.remove(other);
                        break;
                    }
                }
            }

            createAndLaunchBattle(p.party, teamBParty);
        } else {
            // Luyện tập 1 mình (Solo thử sức / test trụ)
            Party soloParty = new Party(p);
            createAndLaunchBattle(soloParty, null);
        }
    }

    /**
     * Kiểm tra hàng đợi và khởi chạy trận đấu khi đủ 2 nhóm
     */
    public static synchronized void checkAndStartMatch() {
        if (WAITING_QUEUE.size() >= 2) {
            Party party1 = WAITING_QUEUE.remove(0);
            Party party2 = WAITING_QUEUE.remove(0);
            createAndLaunchBattle(party1, party2);
        }
    }

    /**
     * Khởi tạo trận đấu và đưa người chơi vào 5 map
     */
    public static void createAndLaunchBattle(Party partyA, Party partyB) {
        Battleground5v5 battle = new Battleground5v5();
        battle.mode = MODE_5V5;
        battle.timeEnd = System.currentTimeMillis() + 15 * 60 * 1000L; // Tối đa 15 phút

        // 1. Tạo instance 5 maps (129, 130, 131, 132, 133)
        int[] mapIds = { MAP_RED_BASE, MAP_BLUE_BASE, MAP_TOP, MAP_MID, MAP_BOT };
        int mobIndexCounter = -5000;

        for (int idx = 0; idx < mapIds.length; idx++) {
            int mid = mapIds[idx];
            Map[] templates = Map.get_map_by_id(mid);
            if (templates == null || templates.length == 0) {
                System.err.println("[Battleground5v5] Error: Map template " + mid + " not found!");
                return;
            }
            Map mapTemplate = templates[0];
            Map instance = new Map();
            instance.template = mapTemplate.template;
            instance.zone_id = (byte) 0;
            instance.list_mob = new int[0];
            instance.map_battleground5v5 = battle;

            // Khởi tạo mob trụ cho map này (tự động nạp trụ theo template hoặc mặc định)
            mobIndexCounter = battle.initTowersForMap(instance, mapTemplate, mid, mobIndexCounter);

            instance.start_map();
            Map.add_map_plus(instance);
            battle.maps[idx] = instance;
        }

        // 2. Phân chia người chơi vào Team A (Cờ Đỏ) và Team B (Cờ Xanh) và trừ lượt đi hôm nay
        if (partyA != null) {
            for (Player p : partyA.list) {
                if (p != null && p.conn != null && p.conn.connected) {
                    p.time_5vs5++;
                    battle.teamA.add(p);
                    p.battleground5v5 = battle;
                    battle.deathCounts.put(p.id, 0);
                }
            }
        }

        if (partyB != null) {
            for (Player p : partyB.list) {
                if (p != null && p.conn != null && p.conn.connected) {
                    p.time_5vs5++;
                    battle.teamB.add(p);
                    p.battleground5v5 = battle;
                    battle.deathCounts.put(p.id, 0);
                }
            }
        }

        ACTIVE_BATTLES.add(battle);

        // 3. Dịch chuyển người chơi vào Căn Cứ
        // Team A vào Map 129 (Căn cứ Đỏ), x = 100, y = 288, type_pk = 4 (Cờ Đỏ)
        for (Player p : battle.teamA) {
            try {
                p.type_pk = 4;
                if (p.map != null) {
                    p.map.change_flag(p, 4);
                }
                Vgo vgo = new Vgo();
                vgo.map_go = new Map[] { battle.maps[0] }; // Map 129 instance
                vgo.xnew = 100;
                vgo.ynew = 288;
                p.goto_map(vgo);
                sendThongBao(p, "Trận chiến 5vs5 bắt đầu! (Lượt " + p.time_5vs5 + "/" + MAX_DAILY_TURNS + ")\nPhe Đỏ: Phá hủy toàn bộ Trụ Phụ rồi tiêu diệt Trụ Chính B đối phương để chiến thắng!");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        // Team B vào Map 130 (Căn cứ Xanh), x = 956, y = 288, type_pk = 5 (Cờ Xanh)
        for (Player p : battle.teamB) {
            try {
                p.type_pk = 5;
                if (p.map != null) {
                    p.map.change_flag(p, 5);
                }
                Vgo vgo = new Vgo();
                vgo.map_go = new Map[] { battle.maps[1] }; // Map 130 instance
                vgo.xnew = 956;
                vgo.ynew = 288;
                p.goto_map(vgo);
                sendThongBao(p, "Trận chiến 5vs5 bắt đầu! (Lượt " + p.time_5vs5 + "/" + MAX_DAILY_TURNS + ")\nPhe Xanh: Phá hủy toàn bộ Trụ Phụ rồi tiêu diệt Trụ Chính A đối phương để chiến thắng!");
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Vòng lặp cập nhật mỗi 1 giây cho trận đấu
     */
    public void update() {
        if (isFinished) {
            long now = System.currentTimeMillis();
            if (isNotifiedFinish && !isClosed && now >= timeReturnVillage) {
                closeBattle();
            }
            return;
        }

        long now = System.currentTimeMillis();

        // 1. Kiểm tra hết thời gian trận đấu (15 phút 5vs5 / 10 phút 1v1)
        if (now >= timeEnd) {
            if (teamA.isEmpty() && teamB.isEmpty()) {
                cancelBattle();
                return;
            }
            // Nếu một phe đã thoát hết nhưng phe còn lại chưa phá được trụ chính trước khi hết giờ -> Hủy trận, không nhận quà
            if (teamA.isEmpty()) {
                broadcastMessage("=== HẾT GIỜ THI ĐẤU ===\nPhe Xanh chưa phá hủy được Trụ Chính Phe Đỏ trước khi hết giờ, trận đấu bị hủy và không nhận quà!");
                cancelBattle();
                return;
            }
            if (teamB.isEmpty()) {
                broadcastMessage("=== HẾT GIỜ THI ĐẤU ===\nPhe Đỏ chưa phá hủy được Trụ Chính Phe Xanh trước khi hết giờ, trận đấu bị hủy và không nhận quà!");
                cancelBattle();
                return;
            }

            // Cả hai phe đều còn người chơi thi đấu: so sánh lượng máu Trụ Chính
            int hpA = (mainTowerA != null && !mainTowerA.isdie) ? mainTowerA.hp : 0;
            int hpB = (mainTowerB != null && !mainTowerB.isdie) ? mainTowerB.hp : 0;
            if (hpA > hpB) {
                finishBattle(1); // Phe Đỏ thắng
            } else if (hpB > hpA) {
                finishBattle(2); // Phe Xanh thắng
            } else {
                broadcastMessage("=== HẾT GIỜ THI ĐẤU ===\nHai phe có lượng máu Trụ Chính bằng nhau! Trận đấu kết thúc với kết quả HÒA, không trao thưởng!");
                cancelBattle();
            }
            return;
        }

        // 2. Trụ KHÔNG gây sát thương người chơi (đã tắt)
        // if (now - lastTowerCheck >= 1000L) {
        //     lastTowerCheck = now;
        //     updateTowerDamage();
        // }

        // 3. Cập nhật hồi sinh cho các người chơi tử trận
        updateRespawn();
    }

    /**
     * Cơ chế Trụ gây sát thương trong phạm vi 30
     */
    private void updateTowerDamage() {
        for (Mob tower : towers) {
            if (tower == null || tower.isdie || tower.map == null) continue;

            int towerId = tower.mob_template.mob_id;
            boolean isTowerA = (towerId == MOB_TRU_THUONG_A || towerId == MOB_TRU_CHINH_A);
            boolean isTowerB = (towerId == MOB_TRU_THUONG_B || towerId == MOB_TRU_CHINH_B);

            // Duyệt tất cả người chơi trong cùng map với trụ
            List<Player> playersInMap = new ArrayList<>(tower.map.players);
            for (Player p : playersInMap) {
                if (p == null || p.conn == null || p.isdie || p.hp <= 0) continue;

                // Kiểm tra khoảng cách đứng gần trong phạm vi 30
                int dx = Math.abs(tower.x - p.x);
                int dy = Math.abs(tower.y - p.y);
                if (dx <= TOWER_ATTACK_RANGE && dy <= TOWER_ATTACK_RANGE) {
                    // Trụ A chỉ đánh Phe B (Cờ Xanh / teamB)
                    // Trụ B chỉ đánh Phe A (Cờ Đỏ / teamA)
                    boolean shouldAttack = (isTowerA && p.type_pk == 5) || (isTowerB && p.type_pk == 4);
                    if (shouldAttack) {
                        fireTowerDamage(tower, p);
                    }
                }
            }
        }
    }

    /**
     * Bắn sát thương từ Trụ vào Player
     */
    private void fireTowerDamage(Mob tower, Player p) {
        try {
            // Sát thương trụ gây ra: khoảng 25% máu tối đa hoặc tối thiểu 500 dame
            int maxHp = p.body.get_hp_max(true);
            int dame = Math.max(500, maxHp / 4);

            p.hp = Math.max(0, p.hp - dame);

            // Gửi hiệu ứng đòn đánh từ quái tới người chơi (Message 100)
            Message m = new Message(100);
            m.writer().writeShort(tower.index);
            m.writer().writeByte(1);
            m.writer().writeInt(tower.hp);
            m.writer().writeInt(tower.hp);
            m.writer().writeShort(0); // Skill id mặc định
            m.writer().writeByte(1);
            m.writer().writeShort(p.index_map);
            m.writer().writeByte(0);
            m.writer().writeInt(dame);
            m.writer().writeInt(0);
            m.writer().writeInt(p.hp);
            m.writer().writeByte(0);
            tower.map.send_msg_all_p(m, p, true);
            m.cleanup();

            // Cập nhật thanh máu trên client
            Service.use_potion(p, 0, -dame);

            // Nếu người chơi chết vì trụ bắn
            if (p.hp <= 0) {
                tower.map.die_player(p, p);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Xử lý người chơi tử trận trong phó bản 5vs5:
     * - Thời gian hồi sinh ban đầu 5s, mỗi lần chết cộng thêm 1s
     */
    public void onPlayerDie(Player p) {
        if (p == null || isFinished) return;

        int deaths = deathCounts.getOrDefault(p.id, 0);
        deathCounts.put(p.id, deaths + 1);

        int seconds = 5 + deaths;
        long reviveAt = System.currentTimeMillis() + (seconds * 1000L);
        reviveTimes.put(p.id, reviveAt);

        // Gửi đồng hồ đếm ngược hồi sinh
        PvpClan.send_revive_countdown(p, seconds);

        // Cộng điểm hạ gục cho đối phương
        if (p.type_pk == 4) {
            scoreTeamB++;
        } else if (p.type_pk == 5) {
            scoreTeamA++;
        }
    }

    /**
     * Cập nhật đếm ngược hồi sinh và đưa người chơi về map trụ chính khi hết giờ
     */
    private void updateRespawn() {
        long now = System.currentTimeMillis();

        for (java.util.Map.Entry<Integer, Long> entry : reviveTimes.entrySet()) {
            int playerId = entry.getKey();
            long reviveAt = entry.getValue();

            if (reviveAt > 0 && now >= reviveAt) {
                reviveTimes.put(playerId, 0L);
                Player p = findPlayer(playerId);
                if (p != null && p.conn != null && p.conn.connected) {
                    respawnPlayerToBase(p);
                }
            }
        }
    }

    /**
     * Hồi sinh người chơi và dịch chuyển về Căn cứ Phe mình (Map 129 cho Đỏ, Map 130 cho Xanh)
     */
    public void respawnPlayerToBase(Player p) {
        try {
            boolean isRedTeam = (p.type_pk == 4 || teamA.contains(p));
            Map baseMap = isRedTeam ? maps[0] : maps[1]; // 0: Map 129, 1: Map 130
            short spawnX = isRedTeam ? (short) 100 : (short) 956;
            short spawnY = 288;

            // Nếu người chơi đang ở map khác, chuyển map về căn cứ
            if (!p.map.equals(baseMap)) {
                Vgo vgo = new Vgo();
                vgo.map_go = new Map[] { baseMap };
                vgo.xnew = spawnX;
                vgo.ynew = spawnY;
                p.goto_map(vgo);
            } else {
                p.x = spawnX;
                p.y = spawnY;
            }

            p.isdie = false;
            p.hp = p.body.get_hp_max(true);
            p.mp = p.body.get_mp_max(true);
            p.time_can_mob_atk = System.currentTimeMillis() + 3000L;

            // 1. Message 6 (Revice_Player)
            Message mRevive = new Message(6);
            mRevive.writer().writeShort(p.index_map);
            mRevive.writer().writeByte(0);
            mRevive.writer().writeInt(p.hp);
            mRevive.writer().writeInt(p.mp);
            p.map.send_msg_all_p(mRevive, p, true);
            mRevive.cleanup();

            // 2. Phục hồi full máu & mana
            Service.use_potion(p, 0, p.hp);
            Service.use_potion(p, 1, p.mp);

            // 3. Di chuyển về điểm hồi sinh
            Message mmove = new Message(1);
            mmove.writer().writeByte(0);
            mmove.writer().writeShort(p.index_map);
            mmove.writer().writeShort(p.x);
            mmove.writer().writeShort(p.y);
            p.map.send_msg_all_p(mmove, p, true);
            mmove.cleanup();

            // 4. Khiên an toàn (Message -71)
            Message mSafe = new Message(-71);
            mSafe.writer().writeByte(1);
            mSafe.writer().writeShort(p.index_map);
            mSafe.writer().writeByte(0);
            mSafe.writer().writeInt(60 * 5); // 5 giây khiên
            p.map.send_msg_all_p(mSafe, p, true);
            mSafe.cleanup();

            // 5. Tắt đồng hồ đếm ngược hồi sinh
            PvpClan.send_revive_countdown(p, 0);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Đếm số lượng Trụ Phụ (Trụ Thường) còn sống của một phe
     * @param targetTeam 1: Phe Đỏ (Trụ Thường A - 122), 2: Phe Xanh (Trụ Thường B - 124)
     */
    public int countAliveSecondaryTowers(int targetTeam) {
        int targetMobId = (targetTeam == 1) ? MOB_TRU_THUONG_A : MOB_TRU_THUONG_B;
        int count = 0;
        for (Mob tower : towers) {
            if (tower != null && tower.mob_template != null && tower.mob_template.mob_id == targetMobId) {
                if (!tower.isdie && tower.hp > 0) {
                    count++;
                }
            }
        }
        return count;
    }

    /**
     * Kiểm tra toàn bộ Trụ Phụ của phe chỉ định đã bị phá hủy hết chưa
     * @param targetTeam 1: Phe Đỏ, 2: Phe Xanh
     */
    public boolean areAllSecondaryTowersDestroyed(int targetTeam) {
        return countAliveSecondaryTowers(targetTeam) == 0;
    }

    /**
     * Gửi cảnh báo và hiệu ứng bong bóng chat khi người chơi tấn công Trụ Chính còn khiên bảo vệ
     */
    private void notifyProtectedTower(Player p, Mob mob, int enemyTeam, int aliveCount) {
        if (p == null || mob == null) return;
        long now = System.currentTimeMillis();
        Long lastTime = lastProtectedWarning.get(p.id);
        if (lastTime == null || now - lastTime >= 3000L) {
            lastProtectedWarning.put(p.id, now);
            String enemyTeamName = (enemyTeam == 1) ? "Phe Đỏ" : "Phe Xanh";
            String warningMsg = "Trụ Chính " + enemyTeamName + " đang có Khiên Bảo Vệ!\nBạn phải phá hủy hết " + aliveCount + " Trụ Phụ còn lại của " + enemyTeamName + " trước!";
            sendThongBao(p, warningMsg);
            if (p.map != null) {
                try {
                    p.map.send_chat_popup(1, mob.index, "Khiên chắn bảo vệ! Hãy phá Trụ Phụ trước!", true);
                } catch (Exception ignored) {}
            }
        }
    }

    /**
     * Kiểm tra người chơi có quyền tấn công Trụ này không
     * - Phe Đỏ (type_pk = 4) chỉ được đánh Trụ Phe B (124, 125)
     * - Phe Xanh (type_pk = 5) chỉ được đánh Trụ Phe A (122, 123)
     * - Trụ Chính chỉ có thể bị tấn công khi toàn bộ Trụ Phụ đối phương đã bị tiêu diệt
     */
    public boolean canAttackTower(Player p, Mob mob) {
        if (p == null || mob == null || mob.mob_template == null || isFinished) return false;
        if (mob.isdie || mob.hp <= 0) return false;
        int mobId = mob.mob_template.mob_id;

        boolean isTowerA = (mobId == MOB_TRU_THUONG_A || mobId == MOB_TRU_CHINH_A);
        boolean isTowerB = (mobId == MOB_TRU_THUONG_B || mobId == MOB_TRU_CHINH_B);

        if (!isTowerA && !isTowerB) return true; // Quái bình thường khác nếu có

        boolean isTeamA = (p.type_pk == 4 || teamA.contains(p));
        boolean isTeamB = (p.type_pk == 5 || teamB.contains(p));

        if (isTeamA) { // Phe Đỏ -> chỉ đánh Trụ B
            if (!isTowerB) return false;
            // Nếu là Trụ Chính B (125), chỉ được đánh khi toàn bộ Trụ Phụ B (124) đã bị phá hủy hết
            if (mobId == MOB_TRU_CHINH_B) {
                int aliveSecTowers = countAliveSecondaryTowers(2);
                if (aliveSecTowers > 0) {
                    notifyProtectedTower(p, mob, 2, aliveSecTowers);
                    return false;
                }
            }
            return true;
        } else if (isTeamB) { // Phe Xanh -> chỉ đánh Trụ A
            if (!isTowerA) return false;
            // Nếu là Trụ Chính A (123), chỉ được đánh khi toàn bộ Trụ Phụ A (122) đã bị phá hủy hết
            if (mobId == MOB_TRU_CHINH_A) {
                int aliveSecTowers = countAliveSecondaryTowers(1);
                if (aliveSecTowers > 0) {
                    notifyProtectedTower(p, mob, 1, aliveSecTowers);
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    /**
     * Xử lý khi một Trụ bị tiêu diệt
     */
    public void onTowerDestroyed(Player killer, Mob tower) {
        if (tower == null || isFinished) return;
        tower.hp = 0;
        tower.isdie = true;
        tower.time_refresh = Long.MAX_VALUE;

        // Phát sóng send_mob_info cho tất cả người chơi trong map chứa trụ để client chuyển sang trạng thái vỡ (Frame 1)
        if (tower.map != null && tower.map.players != null) {
            for (Player p : tower.map.players) {
                if (p != null && p.conn != null && p.conn.connected) {
                    try {
                        Service.send_mob_info(p, tower);
                    } catch (Exception ignored) {
                    }
                }
            }
        }

        int mobId = tower.mob_template.mob_id;
        String towerName = tower.mob_template.name;

        // Phát thông báo trụ bị phá
        String announce = "Trụ [" + towerName + "] đã bị phá hủy bởi " + (killer != null ? killer.name : "kẻ địch") + "!";
        broadcastMessage(announce);

        // Kiểm tra thông báo trạng thái Trụ Phụ và Khiên Trụ Chính
        if (mobId == MOB_TRU_THUONG_A) {
            int remaining = countAliveSecondaryTowers(1);
            if (remaining == 0) {
                broadcastMessage("⚠️ TOÀN BỘ TRỤ PHỤ PHE ĐỎ ĐÃ BỊ PHÁ HỦY!\nKhiên bảo vệ Trụ Chính Phe Đỏ đã biến mất, có thể tấn công Trụ Chính ngay bây giờ!");
            } else {
                broadcastMessage("Phe Đỏ chỉ còn lại " + remaining + " Trụ Phụ!");
            }
        } else if (mobId == MOB_TRU_THUONG_B) {
            int remaining = countAliveSecondaryTowers(2);
            if (remaining == 0) {
                broadcastMessage("⚠️ TOÀN BỘ TRỤ PHỤ PHE XANH ĐÃ BỊ PHÁ HỦY!\nKhiên bảo vệ Trụ Chính Phe Xanh đã biến mất, có thể tấn công Trụ Chính ngay bây giờ!");
            } else {
                broadcastMessage("Phe Xanh chỉ còn lại " + remaining + " Trụ Phụ!");
            }
        }

        // Kiểm tra Trụ Chính A bị phá -> Phe Xanh thắng
        if (mobId == MOB_TRU_CHINH_A) {
            finishBattle(2); // Phe Xanh (Team B) Thắng!
        }
        // Kiểm tra Trụ Chính B bị phá -> Phe Đỏ thắng
        else if (mobId == MOB_TRU_CHINH_B) {
            finishBattle(1); // Phe Đỏ (Team A) Thắng!
        }
    }

    /**
     * Hủy trận đấu (khi cả 2 bên đều thoát trận hoặc không phá được trụ khi hết giờ)
     * Không trao thưởng cho bất kỳ ai.
     */
    public synchronized void cancelBattle() {
        if (isFinished) return;
        isFinished = true;
        isNotifiedFinish = true;
        winningTeam = 0;
        timeReturnVillage = System.currentTimeMillis() + 8000L;

        // Nếu không còn ai trong trận (cả 2 phe đều out), dọn dẹp ngay
        if (teamA.isEmpty() && teamB.isEmpty()) {
            closeBattle();
            return;
        }

        // Nếu còn người chơi (trường hợp hết giờ hoặc hòa), thông báo hủy và đếm ngược về làng
        List<Player> allPlayers = new ArrayList<>();
        allPlayers.addAll(teamA);
        allPlayers.addAll(teamB);

        String cancelMsg = "=== TRẬN ĐẤU BỊ HỦY ===\nTrận đấu đã kết thúc hoặc bị hủy, không có phần thưởng!\nChuẩn bị trở về Làng sau 8 giây!";
        for (Player p : allPlayers) {
            if (p != null && p.conn != null && p.conn.connected) {
                sendThongBao(p, cancelMsg);
                PvpClan.send_return_village_countdown(p, 8);
            }
        }
    }

    /**
     * Kết thúc trận đấu
     * @param winTeam 1: Phe Đỏ (Team A), 2: Phe Xanh (Team B)
     */
    public synchronized void finishBattle(int winTeam) {
        if (isFinished) return;
        isFinished = true;
        isNotifiedFinish = true;
        winningTeam = winTeam;
        timeReturnVillage = System.currentTimeMillis() + 8000L; // 8 giây đếm ngược

        String winName = (winTeam == 1) ? "PHE ĐỎ" : "PHE XANH";
        String finishMsg = "=== KẾT THÚC TRẬN ĐẤU ===\n" + winName + " đã phá hủy Trụ Chính đối phương và giành CHIẾN THẮNG!\nChuẩn bị trở về Làng sau 8 giây!";

        // Trao quà và gửi đếm ngược cho tất cả người chơi
        List<Player> allPlayers = new ArrayList<>();
        allPlayers.addAll(teamA);
        allPlayers.addAll(teamB);

        for (Player p : allPlayers) {
            if (p != null && p.conn != null && p.conn.connected) {
                sendThongBao(p, finishMsg);
                PvpClan.send_return_village_countdown(p, 8);

                // Trao thưởng
                boolean isWinner = (winTeam == 1 && (p.type_pk == 4 || teamA.contains(p)))
                                || (winTeam == 2 && (p.type_pk == 5 || teamB.contains(p)));
                rewardPlayer(p, isWinner);
            }
        }
    }

    /**
     * Trao thưởng cho người chơi sau trận
     */
    private void rewardPlayer(Player p, boolean isWinner) {
        try {
            int beri;
            int ruby;
            if (mode == MODE_5V5) {
                beri = isWinner ? 1_500_000 : 300_000;
                ruby = isWinner ? 3000 : 600;
            } else { // MODE_1V1
                beri = isWinner ? 500_000 : 100_000;
                ruby = isWinner ? 1000 : 200;
            }
            p.update_vang(beri);
            p.update_ngoc(ruby);
            p.update_money();
            String modeName = (mode == MODE_5V5) ? "5vs5" : "1vs1";
            sendThongBao(p, "Phần thưởng trận đấu " + modeName + ":\n+ " + Util.number_format(beri) + " Beri\n+ " + Util.number_format(ruby) + " Ruby!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * Đưa tất cả người chơi về Làng và dọn dẹp instance
     */
    public synchronized void closeBattle() {
        if (isClosed) return;
        isClosed = true;

        List<Player> allPlayers = new ArrayList<>();
        allPlayers.addAll(teamA);
        allPlayers.addAll(teamB);

        for (Player p : allPlayers) {
            if (p != null) {
                p.battleground5v5 = null;
                p.type_pk = -1; // Tháo cờ về hòa bình
                if (p.conn != null && p.conn.connected) {
                    try {
                        PvpClan.send_return_village_countdown(p, 0);
                        PvpClan.send_revive_countdown(p, 0);

                        // Hồi sinh nếu người chơi đang chết hoặc hết máu
                        if (p.isdie || p.hp <= 0) {
                            p.isdie = false;
                            p.hp = p.body.get_hp_max(true);
                            p.mp = p.body.get_mp_max(true);
                        }
                        Service.use_potion(p, 0, p.hp);
                        Service.use_potion(p, 1, p.mp);

                        // Dịch chuyển về Làng đã lưu của người chơi hoặc Map 1 (Làng Cối Xay Gió)
                        int targetMapId = (p.id_map_save > 0 && !isBattleMapStatic(p.id_map_save)) ? p.id_map_save : 1;
                        Map[] targetMaps = Map.get_map_by_id(targetMapId);
                        if (targetMaps == null || targetMaps.length == 0 || targetMaps[0] == null) {
                            targetMapId = 1;
                            targetMaps = Map.get_map_by_id(1);
                        }

                        Vgo vgo = new Vgo();
                        vgo.map_go = targetMaps;
                        vgo.xnew = (short) (targetMapId == 1 ? 420 : 300);
                        vgo.ynew = (short) (targetMapId == 1 ? 280 : 250);
                        p.goto_map(vgo);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        }

        // Dừng và giải phóng triệt để các map instance của phó bản
        if (maps != null) {
            for (Map m : maps) {
                if (m != null) {
                    m.running = false;
                    m.map_battleground5v5 = null;
                    Map.remove_map_plus(m);
                }
            }
        }

        teamA.clear();
        teamB.clear();
        deathCounts.clear();
        reviveTimes.clear();
        lastProtectedWarning.clear();
        towers.clear();

        ACTIVE_BATTLES.remove(this);
    }

    /**
     * Gửi tin nhắn thông báo tới toàn bộ người chơi trong trận
     */
    public void broadcastMessage(String msg) {
        List<Player> all = new ArrayList<>();
        all.addAll(teamA);
        all.addAll(teamB);
        for (Player p : all) {
            if (p != null && p.conn != null && p.conn.connected) {
                sendThongBao(p, msg);
            }
        }
    }

    /**
     * Tìm Player theo ID
     */
    private Player findPlayer(int id) {
        for (Player p : teamA) {
            if (p != null && p.id == id) return p;
        }
        for (Player p : teamB) {
            if (p != null && p.id == id) return p;
        }
        return null;
    }

    /**
     * Tìm Mob theo index trong trận
     */
    public Mob get_mob(Player p, int id) {
        for (Mob mob : towers) {
            if (mob != null && p.map != null && p.map.equals(mob.map) && mob.index == id) {
                return mob;
            }
        }
        return null;
    }

    /**
     * Kiểm tra map id có thuộc 5 map của phó bản 5vs5 không
     */
    public boolean isBattleMap(int mapId) {
        return mapId == MAP_RED_BASE || mapId == MAP_BLUE_BASE
            || mapId == MAP_TOP || mapId == MAP_MID || mapId == MAP_BOT;
    }

    /**
     * Lấy map instance tương ứng theo template id
     * (Trận 1v1 chỉ có maps[0] và maps[1], các map còn lại là null)
     */
    public Map getMapInstance(int templateId) {
        switch (templateId) {
            case MAP_RED_BASE:  return maps[0];
            case MAP_BLUE_BASE: return maps[1];
            case MAP_TOP:       return maps != null && maps.length > 2 ? maps[2] : null;
            case MAP_MID:       return maps != null && maps.length > 3 ? maps[3] : null;
            case MAP_BOT:       return maps != null && maps.length > 4 ? maps[4] : null;
            default: return null;
        }
    }

    public static boolean isBattleMapStatic(int mapId) {
        return mapId >= 129 && mapId <= 133;
    }

    public static boolean isBattleMapStatic(Map map) {
        return map != null && map.template != null && isBattleMapStatic(map.template.id);
    }

    /**
     * Xử lý khi người chơi thoát game (disconnect/logout)
     */
    public static void handlePlayerExit(Player p) {
        if (p == null) return;
        try {
            // 1. Rút khỏi hàng chờ 1v1 nếu đang đợi
            WAITING_QUEUE_1V1.remove(p);

            // 2. Rút khỏi hàng chờ 5v5 nếu đang đợi trong nhóm
            if (p.party != null && WAITING_QUEUE.contains(p.party)) {
                WAITING_QUEUE.remove(p.party);
                for (Player m : p.party.list) {
                    if (m != null && !m.equals(p)) {
                        sendThongBao(m, "Thành viên " + p.name + " đã thoát game, nhóm bị hủy khỏi hàng chờ Chiến Trường 5vs5!");
                    }
                }
            }

            // 3. Nếu đang trong trận đấu
            if (p.battleground5v5 != null) {
                Battleground5v5 battle = p.battleground5v5;
                battle.teamA.remove(p);
                battle.teamB.remove(p);
                p.type_pk = -1; // Tắt cờ Đỏ / Xanh về hòa bình

                // Thông báo cho các người chơi còn lại trong trận
                List<Player> remainingPlayers = new ArrayList<>();
                remainingPlayers.addAll(battle.teamA);
                remainingPlayers.addAll(battle.teamB);
                for (Player rem : remainingPlayers) {
                    if (rem != null && rem.conn != null && rem.conn.connected) {
                        sendThongBao(rem, "Người chơi " + p.name + " đã thoát trận đấu!");
                    }
                }

                // Kiểm tra trạng thái trận đấu sau khi người chơi thoát
                if (!battle.isFinished) {
                    if (battle.teamA.isEmpty() && battle.teamB.isEmpty()) {
                        // Cả hai bên đều đã thoát trận -> Hủy trận đấu, dọn dẹp ngay không trao thưởng
                        battle.cancelBattle();
                    } else if (battle.teamA.isEmpty()) {
                        // Phe Đỏ đã thoát hết, Phe Xanh vẫn phải đánh tiếp phá bể trụ mới win và nhận quà
                        for (Player rem : battle.teamB) {
                            if (rem != null && rem.conn != null && rem.conn.connected) {
                                sendThongBao(rem, "Toàn bộ đối thủ Phe Đỏ đã thoát trận!\nPhe Xanh hãy tiếp tục tấn công phá hủy Trụ Chính Phe Đỏ để giành chiến thắng và nhận quà!");
                            }
                        }
                    } else if (battle.teamB.isEmpty()) {
                        // Phe Xanh đã thoát hết, Phe Đỏ vẫn phải đánh tiếp phá bể trụ mới win và nhận quà
                        for (Player rem : battle.teamA) {
                            if (rem != null && rem.conn != null && rem.conn.connected) {
                                sendThongBao(rem, "Toàn bộ đối thủ Phe Xanh đã thoát trận!\nPhe Đỏ hãy tiếp tục tấn công phá hủy Trụ Chính Phe Xanh để giành chiến thắng và nhận quà!");
                            }
                        }
                    }
                }

                p.battleground5v5 = null;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
