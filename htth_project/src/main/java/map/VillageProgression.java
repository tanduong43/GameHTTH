package map;

import java.io.IOException;
import java.util.List;

import client.Player;
import core.Manager;
import core.Service;

/**
 * Quản lý cơ chế Mốc Qua Làng (Village Progression).
 * Cứ cách 3 làng sẽ có 1 mốc: Người chơi phải tiêu diệt Boss làng chốt mốc
 * để mở khóa tiếp 3 làng tiếp theo trên Đại Hải Trình.
 *
 * - Tier 1 (Mặc định): Làng 1 (Cối Xay Gió), Làng 2 (Vỏ Sò), Làng 3 (Orange) [Maps 1 - 24]
 *   -> Diệt Boss tại vùng Orange (Maps 17-24) để mở Tier 2.
 *
 * - Tier 2: Làng 4 (Syrup), Làng 5 (Baratie), Làng 6 (Hạt Dẻ) [Maps 25 - 48]
 *   -> Diệt Boss tại vùng Hạt Dẻ (Maps 41-48) để mở Tier 3.
 *
 * - Tier 3: Làng 7 (Loguetown), Làng 8 (Mỏm Sinh Đôi), Làng 9 (Núi Đảo Nghịch) [Maps 49 - 78]
 *   -> Diệt Boss tại vùng Núi Đảo Nghịch / Loguetown (Maps 49-78) để mở Tier 4.
 *
 * - Tier 4: Làng 10 (Whiskey), Làng 11 (Little Garden), Làng 12 (Cự Nhân) [Maps 79 - 106]
 *   -> Diệt Boss tại vùng Cự Nhân / Little Garden (Maps 83-106) để mở Tier 5.
 *
 * - Tier 5: Làng 13 (Đảo Drum), Làng 14 (Alabasta), Làng 15 (Skypiea) [Maps 107+]
 *
 * @author Truongbk
 */
public class VillageProgression {

    public static final int MAX_TIER = 5;

    /**
     * Xác định Tier yêu cầu của một Map.
     * Trả về 0 nếu map là map đặc biệt/phó bản/sự kiện (không bị giới hạn).
     */
    public static int getRequiredTierForMap(int mapId) {
        // Các map đặc biệt, phó bản, sự kiện luôn cho phép
        if (mapId <= 0 || Map.is_map_dungeon(mapId) || isSpecialBypassMap(mapId)) {
            return 0;
        }

        // Tier 1: Maps 1 - 24 (Cối Xay Gió, Vỏ Sò, Orange)
        if (mapId >= 1 && mapId <= 24) {
            return 1;
        }
        // Tier 2: Maps 25 - 48 (Syrup, Baratie, Hạt Dẻ)
        if (mapId >= 25 && mapId <= 48) {
            return 2;
        }
        // Tier 3: Maps 49 - 78 (Thị trấn khởi đầu, Mỏm sinh đôi, Núi đảo nghịch)
        if (mapId >= 49 && mapId <= 78) {
            return 3;
        }
        // Tier 4: Maps 79 - 106 (Whiskey, Little Garden, Cự Nhân)
        if (mapId >= 79 && mapId <= 106) {
            return 4;
        }
        // Tier 5: Maps 107+ (Drum, Alabasta, Skypiea, v.v.)
        if (mapId >= 107) {
            return 5;
        }

        return 1;
    }

    /**
     * Các map sự kiện / đấu trường / chợ / bang hội đặc biệt được bypass
     */
    public static boolean isSpecialBypassMap(int mapId) {
        return mapId == 1000 || mapId == 1001 || mapId == 1002 || mapId == 2000 || mapId == 2027 || mapId == 2028
                || mapId == 123 || (mapId >= 167 && mapId <= 176) || (mapId >= 500 && mapId <= 512)
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
                return "Bạn cần tiêu diệt Boss tại Thị trấn Orange để mở khóa hành trình đến Làng Syrup, Baratie, Hạt Dẻ!";
            case 3:
                return "Bạn cần tiêu diệt Boss tại Làng Hạt Dẻ để mở khóa hành trình đến Thị trấn Khởi đầu, Mỏm Sinh Đôi, Núi Đảo Nghịch!";
            case 4:
                return "Bạn cần tiêu diệt Boss tại Núi Đảo Nghịch để mở khóa hành trình đến Thị trấn Whiskey, Little Garden, Đảo Cự Nhân!";
            case 5:
                return "Bạn cần tiêu diệt Boss tại Đảo Cự Nhân để mở khóa hành trình đến Đảo Drum, Alabasta, Skypiea!";
            default:
                return "Chưa thể đi đến khu vực này!";
        }
    }

    /**
     * Xử lý khi có Boss bị tiêu diệt trong Map.
     * Kiểm tra xem Boss đó có phải là Boss chốt mốc để thăng Tier cho người chơi không.
     */
    public static void onBossKilled(Player p, Mob mobTarget, Map map) {
        if (mobTarget == null || map == null) return;
        int mapId = map.template.id;

        // Không tính qua làng khi đang trong phó bản Săn Trùm (BossHunt) hoặc các phó bản khác
        if (map.map_bossHunt != null || activities.BossHunt.isBossHuntMap(mapId)
                || (p != null && p.bossHunt != null) || Map.is_map_dungeon(mapId)) {
            return;
        }

        // Xác định mốc hiện tại dựa theo map của boss
        int unlockTier = 0;
        String bossName = mobTarget.mob_template != null ? mobTarget.mob_template.name : "Boss";
        String unlockedVillages = "";

        // Mốc 1 -> 2: Boss ở các Maps 1 - 24 (Vùng Orange, Vỏ Sò, Cối Xay Gió)
        if (mapId >= 1 && mapId <= 24) {
            unlockTier = 2;
            unlockedVillages = "Làng Syrup, Nhà Hàng Baratie, Làng Hạt Dẻ";
        }
        // Mốc 2 -> 3: Boss ở các Maps 25 - 48 (Vùng Syrup, Baratie, Hạt Dẻ)
        else if (mapId >= 25 && mapId <= 48) {
            unlockTier = 3;
            unlockedVillages = "Thị Trấn Khởi Đầu, Mỏm Sinh Đôi, Núi Đảo Nghịch";
        }
        // Mốc 3 -> 4: Boss ở các Maps 49 - 78 (Vùng Núi Đảo Nghịch / Loguetown)
        else if (mapId >= 49 && mapId <= 78) {
            unlockTier = 4;
            unlockedVillages = "Thị Trấn Whiskey, Little Garden, Đảo Cự Nhân";
        }
        // Mốc 4 -> 5: Boss ở các Maps 79 - 106 (Vùng Whiskey, Little Garden, Cự Nhân)
        else if (mapId >= 79 && mapId <= 106) {
            unlockTier = 5;
            unlockedVillages = "Đảo Drum, Vương Quốc Alabasta, Đảo Trên Trời";
        }
        // Mốc 5+: Boss ở các Maps 107+ (Drum, Alabasta, Skypiea, v.v.)
        else if (mapId >= 107) {
            unlockTier = 5;
            unlockedVillages = "Đảo Drum, Vương Quốc Alabasta, Đảo Trên Trời";
        }

        if (unlockTier > 0) {
            // Thăng cấp cho tất cả người chơi có mặt trong Map khi Boss bị tiêu diệt
            if (map.players != null) {
                for (int i = 0; i < map.players.size(); i++) {
                    Player pl = map.players.get(i);
                    if (pl != null && pl.conn != null) {
                        applyTierUnlock(pl, unlockTier, bossName, unlockedVillages);
                    }
                }
            }

            // Thăng cấp cho người tiêu diệt trực tiếp (đảm bảo không sót)
            if (p != null) {
                applyTierUnlock(p, unlockTier, bossName, unlockedVillages);

                // Thăng cấp cho tất cả thành viên trong Party
                if (p.party != null && p.party.list != null) {
                    for (Player memInList : p.party.list) {
                        if (memInList != null) {
                            Player member = Map.get_player_by_name_allmap(memInList.name);
                            if (member != null && member.conn != null) {
                                applyTierUnlock(member, unlockTier, bossName, unlockedVillages);
                            }
                        }
                    }
                }
            }
        }
    }

    private static void applyTierUnlock(Player pl, int newTier, String bossName, String unlockedVillages) {
        if (pl != null && pl.village_tier < newTier) {
            pl.village_tier = (byte) newTier;
            try {
                Service.send_box_ThongBao_OK(pl,
                        "🎉 Chúc mừng bạn đã tiêu diệt " + bossName
                                + "!\nBạn đã mở khóa hành trình đến: " + unlockedVillages + "!");
                Manager.gI().chatKTG(0,
                        "Hải tặc " + pl.name + " đã tiêu diệt " + bossName + " và mở khóa vùng biển mới: " + unlockedVillages + "!",
                        5);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }
}
