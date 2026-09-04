package map;

import java.io.IOException;
import java.util.List;

import client.Player;
import core.Manager;
import core.Service;

/**
 * Quản lý cơ chế Mốc Qua Làng (Village Progression): 1 Boss 1 Làng.
 * - Tier 1 (Mặc định): Mở sẵn tới Làng Orange (Maps 1 - 24: Cối Xay Gió, Vỏ Sò, Orange).
 * - Các làng tiếp theo: Tiêu diệt Boss làng/Boss map chốt của làng trước đó để mở khóa làng tiếp theo.
 *
 * - Tier 1 (Mặc định): Maps 1 - 24 (Cối Xay Gió, Vỏ Sò, Orange)
 * - Tier 2: Làng Syrup [Maps 25 - 32] -> Diệt Boss tại vùng Orange (Maps 17-24) để mở.
 * - Tier 3: Nhà Hàng Baratie [Maps 33 - 40] -> Diệt Boss tại vùng Syrup (Maps 25-32) để mở.
 * - Tier 4: Làng Hạt Dẻ [Maps 41 - 48] -> Diệt Boss tại vùng Baratie (Maps 33-40) để mở.
 * - Tier 5: Thị Trấn Khởi Đầu (Loguetown) [Maps 49 - 65] -> Diệt Boss tại vùng Hạt Dẻ (Maps 41-48) để mở.
 * - Tier 6: Mỏm Sinh Đôi & TT. Whiskey [Maps 66 - 78] -> Diệt Boss tại vùng Khởi Đầu (Maps 49-65) để mở.
 * - Tier 7: Đảo Little Garden & TT. Horn [Maps 79 - 92] -> Diệt Boss tại vùng Whiskey (Maps 66-78) để mở.
 * - Tier 8: TT. Nanohana & Vương Quốc Alabasta [Maps 93 - 106] -> Diệt Boss tại vùng Little Garden / Horn (Maps 79-92) để mở.
 * - Tier 9: Đảo Drum & Đảo Trên Trời (Skypiea) [Maps 107 - 127] -> Diệt Boss tại vùng Alabasta (Maps 93-106) để mở.
 * - Tier 10: Kinh Đô Nước Water 7 [Maps 189 - 198+] -> Diệt Boss tại vùng Đảo Trên Trời (Maps 107-127) để mở.
 *
 * @author Truongbk
 */
public class VillageProgression {

    public static final int MAX_TIER = 10;

    /**
     * Xác định Tier yêu cầu của một Map.
     * Trả về 0 nếu map là map đặc biệt/phó bản/sự kiện (không bị giới hạn).
     */
    public static int getRequiredTierForMap(int mapId) {
        // Các map đặc biệt, phó bản, sự kiện luôn cho phép
        if (mapId <= 0 || Map.is_map_dungeon(mapId) || isSpecialBypassMap(mapId)) {
            return 0;
        }

        // Tier 1 (Mặc định): Maps 1 - 24 (Cối Xay Gió, Vỏ Sò, Orange)
        if (mapId >= 1 && mapId <= 24) {
            return 1;
        }
        // Tier 2: Maps 25 - 32 (Làng Syrup)
        if (mapId >= 25 && mapId <= 32) {
            return 2;
        }
        // Tier 3: Maps 33 - 40 (Nhà Hàng Baratie)
        if (mapId >= 33 && mapId <= 40) {
            return 3;
        }
        // Tier 4: Maps 41 - 48 (Làng Hạt Dẻ)
        if (mapId >= 41 && mapId <= 48) {
            return 4;
        }
        // Tier 5: Maps 49 - 65 (Thị Trấn Khởi Đầu - Loguetown)
        if (mapId >= 49 && mapId <= 65) {
            return 5;
        }
        // Tier 6: Maps 66 - 78 (Mỏm Sinh Đôi & Thị Trấn Whiskey)
        if (mapId >= 66 && mapId <= 78) {
            return 6;
        }
        // Tier 7: Maps 79 - 92 (Đảo Little Garden & Thị Trấn Horn)
        if (mapId >= 79 && mapId <= 92) {
            return 7;
        }
        // Tier 8: Maps 93 - 106 (TT. Nanohana & Vương Quốc Alabasta)
        if (mapId >= 93 && mapId <= 106) {
            return 8;
        }
        // Tier 9: Maps 107 - 127 (Đảo Drum & Đảo Trên Trời - Skypiea)
        if (mapId >= 107 && mapId <= 127) {
            return 9;
        }
        // Tier 10: Maps 189+ (Kinh Đô Nước Water 7)
        if (mapId >= 189) {
            return 10;
        }

        return 1;
    }

    /**
     * Các map sự kiện / đấu trường / chợ / bang hội đặc biệt được bypass
     */
    public static boolean isSpecialBypassMap(int mapId) {
        return mapId == 1000 || mapId == 1001 || mapId == 1002 || mapId == 2000 || mapId == 2027 || mapId == 2028
                || (mapId >= 119 && mapId <= 123)
                || (mapId >= 167 && mapId <= 176) || (mapId >= 500 && mapId <= 512)
                || mapId == 62 || activities.BossHunt.isBossHuntMap(mapId);
    }

    /**
     * Kiểm tra người chơi có quyền truy cập vào map này hay không.
     */
    public static boolean canAccessMap(Player p, int targetMapId) {
        if (p == null) return false;
        int reqTier = getRequiredTierForMap(targetMapId);
        if (reqTier <= 1) {
            return true;
        }
        return p.village_tier >= reqTier;
    }

    /**
     * Lấy thông báo lý do không thể vào map
     */
    public static String getBlockMessage(int targetMapId) {
        int reqTier = getRequiredTierForMap(targetMapId);
        switch (reqTier) {
            case 2:
                return "Bạn cần tiêu diệt Boss tại Thị trấn Orange để mở khóa hành trình đến Làng Syrup!";
            case 3:
                return "Bạn cần tiêu diệt Boss tại Làng Syrup để mở khóa hành trình đến Nhà Hàng Baratie!";
            case 4:
                return "Bạn cần tiêu diệt Boss tại Nhà Hàng Baratie để mở khóa hành trình đến Làng Hạt Dẻ!";
            case 5:
                return "Bạn cần tiêu diệt Boss tại Làng Hạt Dẻ để mở khóa hành trình đến Thị Trấn Khởi Đầu!";
            case 6:
                return "Bạn cần tiêu diệt Boss tại Thị Trấn Khởi Đầu để mở khóa hành trình đến Mỏm Sinh Đôi & Thị Trấn Whiskey!";
            case 7:
                return "Bạn cần tiêu diệt Boss tại Thị Trấn Whiskey để mở khóa hành trình đến Đảo Little Garden & Thị Trấn Horn!";
            case 8:
                return "Bạn cần tiêu diệt Boss tại Đảo Little Garden / TT. Horn để mở khóa hành trình đến Vương Quốc Alabasta!";
            case 9:
                return "Bạn cần tiêu diệt Boss tại Vương Quốc Alabasta để mở khóa hành trình đến Đảo Drum & Đảo Trên Trời!";
            case 10:
                return "Bạn cần tiêu diệt Boss tại Đảo Trên Trời để mở khóa hành trình đến Kinh Đô Nước Water 7!";
            default:
                return "Chưa thể đi đến khu vực này!";
        }
    }

    /**
     * Xử lý khi có Boss bị tiêu diệt trong Map.
     * Kiểm tra xem Boss đó có phải là Boss chốt mốc để thăng Tier cho người chơi không.
     */
    public static void onBossKilled(Player p, Mob mobTarget, Map map) {
        if (mobTarget == null || map == null || p == null) return;

        int mobId = mobTarget.mob_template != null ? mobTarget.mob_template.mob_id : -1;
        int mapId = map.template.id;

        // Kích hoạt khi tiêu diệt:
        // 1. Boss làng thế giới (thegioi == 2 hoặc 1)
        // 2. Boss map cuối làng 1.000 ruby (Map.is_map_boss)
        // 3. Quái có mob_id là một trong các Boss làng chốt mốc
        boolean isWorldVillageBoss = (mobTarget.boss_info != null && (mobTarget.boss_info.thegioi == 2 || mobTarget.boss_info.thegioi == 1));
        boolean isMapBossRuby = Map.is_map_boss(mapId);
        boolean isKnownBossMob = (mobId == 16 || mobId == 23 || mobId == 29 || mobId == 36
                || mobId == 43 || mobId == 68 || mobId == 78 || mobId == 92 || mobId == 112);

        if (!isWorldVillageBoss && !isMapBossRuby && !isKnownBossMob) {
            return;
        }

        // Không tính qua làng khi đang trong phó bản Săn Trùm (BossHunt) hoặc các phó bản khác
        if (map.map_bossHunt != null || activities.BossHunt.isBossHuntMap(mapId)
                || isSpecialBypassMap(mapId) || p.bossHunt != null || Map.is_map_dungeon(mapId)) {
            return;
        }

        // Xác định mốc hiện tại dựa theo mobId và mapId của boss
        int unlockTier = 0;
        String bossName = mobTarget.mob_template != null ? mobTarget.mob_template.name : "Boss";
        String unlockedVillages = "";

        // Mốc 1 -> 2: Boss Orange (Buggy mob_id 16 hoặc Map 21 / Maps 16-24)
        if (mobId == 16 || mapId == 21 || (mapId >= 16 && mapId <= 24)) {
            unlockTier = 2;
            unlockedVillages = "Làng Syrup";
        }
        // Mốc 2 -> 3: Boss Syrup (Kuro mob_id 23 hoặc Map 29 / Maps 24-32)
        else if (mobId == 23 || mapId == 29 || (mapId >= 24 && mapId <= 32)) {
            unlockTier = 3;
            unlockedVillages = "Nhà Hàng Baratie";
        }
        // Mốc 3 -> 4: Boss Baratie (Krieg mob_id 29 hoặc Map 37 / Maps 32-40)
        else if (mobId == 29 || mapId == 37 || (mapId >= 32 && mapId <= 40)) {
            unlockTier = 4;
            unlockedVillages = "Làng Hạt Dẻ";
        }
        // Mốc 4 -> 5: Boss Hạt Dẻ (Arlong mob_id 36 hoặc Map 45 / Maps 40-48)
        else if (mobId == 36 || mapId == 45 || (mapId >= 40 && mapId <= 48)) {
            unlockTier = 5;
            unlockedVillages = "Thị Trấn Khởi Đầu (Loguetown)";
        }
        // Mốc 5 -> 6: Boss Khởi Đầu (Smoker mob_id 43 hoặc Map 53 / Maps 48-65)
        else if (mobId == 43 || mapId == 53 || (mapId >= 48 && mapId <= 65)) {
            unlockTier = 6;
            unlockedVillages = "Mỏm Sinh Đôi & Thị Trấn Whiskey";
        }
        // Mốc 6 -> 7: Boss Whiskey (mob_id 68 hoặc Map 73 / Maps 66-78)
        else if (mobId == 68 || mapId == 73 || (mapId >= 66 && mapId <= 78)) {
            unlockTier = 7;
            unlockedVillages = "Đảo Little Garden & Thị Trấn Horn";
        }
        // Mốc 7 -> 8: Boss Little Garden / Horn (mob_id 78 hoặc Map 87 / Maps 79-92)
        else if (mobId == 78 || mapId == 87 || (mapId >= 79 && mapId <= 92)) {
            unlockTier = 8;
            unlockedVillages = "Thị Trấn Nanohana & Vương Quốc Alabasta";
        }
        // Mốc 8 -> 9: Boss Alabasta (Crocodile mob_id 92 hoặc Map 102 / Maps 93-106)
        else if (mobId == 92 || mapId == 102 || (mapId >= 93 && mapId <= 106)) {
            unlockTier = 9;
            unlockedVillages = "Đảo Drum & Đảo Trên Trời (Skypiea)";
        }
        // Mốc 9 -> 10: Boss Đảo Trên Trời (Enel mob_id 112 hoặc Map 127 / Maps 107-127)
        else if (mobId == 112 || mapId == 127 || (mapId >= 107 && mapId <= 127)) {
            unlockTier = 10;
            unlockedVillages = "Kinh Đô Nước Water 7";
        }

        if (unlockTier > 0 && p != null && p.conn != null) {
            // Chỉ thăng cấp mở khóa qua làng cho người trực tiếp tiêu diệt Boss
            applyTierUnlock(p, unlockTier, bossName, unlockedVillages);
        }
    }

    private static void applyTierUnlock(Player pl, int newTier, String bossName, String unlockedVillages) {
        if (pl == null || pl.conn == null) return;
        try {
            if (pl.village_tier < newTier) {
                pl.village_tier = (byte) newTier;
                System.out.println("[PROGRESSION] Player " + pl.name + " defeated " + bossName + " -> UPGRADED TO TIER " + newTier + " (" + unlockedVillages + ")");
                Service.send_box_ThongBao_OK(pl,
                        "🎉 Chúc mừng bạn đã tiêu diệt " + bossName
                                + "!\nBạn đã mở khóa hành trình đến: " + unlockedVillages + "!");
                Manager.gI().chatKTG(0,
                        "Hải tặc " + pl.name + " đã tiêu diệt " + bossName + " và mở khóa vùng biển mới: " + unlockedVillages + "!",
                        5);
            } else {
                System.out.println("[PROGRESSION] Player " + pl.name + " defeated " + bossName + " -> ALREADY UNLOCKED TIER " + newTier);
                Service.send_box_ThongBao_OK(pl,
                        "🎉 Bạn đã tiêu diệt " + bossName + "!\nHành trình đến " + unlockedVillages + " đã được bạn mở khóa từ trước.");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
