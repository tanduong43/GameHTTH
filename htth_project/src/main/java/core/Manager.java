package core;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.IOException;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.*;
import activities.*;
import client.*;
import event.TaiXiu;
import event.EventTrungThu;
import event.EventTet;
import map.*;
import map.Map;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;
import database.SQL;
import io.Message;
import io.SessionManager;
import template.*;

/**
 *
 * @author Truongbk
 */
public class Manager {
    private static Manager instance;
    public static String[] NAME_ITEM_SELL_TEMP = new String[] { "Shop Trang Bị Võ Sĩ", "Shop Trang Bị Kiếm Khách",
            "Shop Trang Bị Đầu Bếp", "Shop Trang Bị Hoa Tiêu", "Shop Trang Bị Xạ Thủ" };
    public boolean debug;
    public String mysql_host;
    public String mysql_database;
    public String mysql_user;
    public String mysql_pass;
    public int server_port;
    public int ws_port;
    public int exp;
    public static int RATE_EXP = 1;
    public static int RATE_EXP_SKILL = 1;
    public boolean server_admin;
    public int max_ip_connection;
    public int max_register_ip_day;
    public int max_ccu;
    public String notice_giftcode = "Giftcode:\n* mothanhvien\n* open\n* loantin\n* thanhvienmoi\n* tanthuhaitac,denbu,baotri";
    private int index_mob;

    public int getIndexMob() {
        return index_mob;
    }

    public void setIndexMob(int indexMob) {
        this.index_mob = indexMob;
    }

    private TaiXiu tx;
    private static int a = 0;

    public static Manager gI() {
        if (instance == null) {
            instance = new Manager();
        }
        return instance;
    }

    public void init() {
        index_mob = 1;
        try {
            load_config();
            // load msg data
            ByteArrayInputStream bais = new ByteArrayInputStream(Util.loadfile("data/msg/hair"));
            DataInputStream dis = new DataInputStream(bais);
            // load_hair(dis, 103);
            dis.close();
            bais.close();
            //
            bais = new ByteArrayInputStream(Util.loadfile("data/msg/head"));
            dis = new DataInputStream(bais);
            load_hair(dis, 108);
            dis.close();
            bais.close();
            // load da than thoai
            DaThanThoai.data_shop = Util.loadfile("data/msg/dathanthoaishop");
        } catch (Exception e) {
            System.out.println("config load err!");
            System.exit(0);
        }
        load_database();
        start_service();
    }

    private void start_service() {
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                if (!Map.is_map_boss(map.template.id) && !Map.is_map_dungeon(map.template.id)) {
                    // Đảo Ruby (Map 1001) chỉ khởi động luồng khi đang trong giờ mở cửa để tiết kiệm RAM
                    if (map.template.id == 1001 && !Map.isRubyIslandOpen()) {
                        continue;
                    }
                    map.start_map();
                }
            }
        }
        // load static class
        tx = new TaiXiu();
        EventTrungThu.getInstance();
        EventTet.getInstance();
        event.Event2011.getInstance();
        event.GuildWarDaoHoa.getInstance().init();
        event.SeaLeaderManager.getInstance().init();
        activities.BigBattle.init();
        a = Rebuild_Item.ID_SELL.length;
        a = Red_Line.KEY0.length;
        a = UpgradeItem.DATA.size();
        a = Body.Point3_Template_hp.length;
        a = ItemBoat.ENTRYS.size();
        a = ItemSell.ENTRYS.size();
        a = VongQuay.ID_ITEM.length;
        a = Level.ENTRYS.length;
        a = Skill_info.EXP.length;
        //
        System.out.println("Start Service OK, " + a);
        if (RATE_EXP > 1 || RATE_EXP_SKILL > 1) {
            new Thread(() -> {
                try {
                    Thread.sleep(5000);
                    setRateExp(RATE_EXP, RATE_EXP_SKILL, true);
                } catch (Exception e) {
                }
            }).start();
        }
    }

    public void setRateExp(int rateExp, int rateExpSkill, boolean broadcast) {
        RATE_EXP = rateExp;
        RATE_EXP_SKILL = rateExpSkill;
        this.exp = rateExp;
        if (broadcast) {
            try {
                if (rateExp > 1 || rateExpSkill > 1) {
                    String msg = "THÔNG BÁO SỰ KIỆN: Đang diễn ra sự kiện ";
                    if (rateExp > 1 && rateExpSkill > 1) {
                        msg += "x" + rateExp + " EXP & x" + rateExpSkill + " EXP Skill toàn máy chủ!";
                    } else if (rateExp > 1) {
                        msg += "x" + rateExp + " EXP toàn máy chủ!";
                    } else {
                        msg += "x" + rateExpSkill + " EXP Skill toàn máy chủ!";
                    }
                    chatKTG(0, msg, 5);
                } else {
                    chatKTG(0, "THÔNG BÁO: Sự kiện tăng EXP toàn máy chủ đã kết thúc!", 5);
                }
            } catch (Exception e) {
            }
        }
    }

    private void stop_service() {
        //
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                map.stop_map();
            }
        }
        tx.close();
        for (int i = 0; i < Map.get_map_plus().size(); i++) {
            Map.get_map_plus().get(i).stop_map();
        }
    }

    private void load_hair(DataInputStream dis, int type) throws IOException {
        dis.readByte();
        dis.readUTF();
        dis.readByte();
        int n = dis.readShort();
        for (int i = 0; i < n; i++) {
            ItemHair temp = ItemHair.readUpdateItemHair(dis);
            temp.type = (byte) type;
            ItemHair.ENTRYS.add(temp);
        }
    }

    private void load_database() {
        Connection conn = null;
        Statement ps = null;
        ResultSet rs = null;
        try {
            conn = SQL.gI().getCon();
            ps = conn.createStatement();
            // Tự động đồng bộ Râu Trắng vào Database khi khởi động (không cần chạy lệnh SQL thủ công trên VPS)
            try {
                ps.executeUpdate("UPDATE `mobs` SET `hOne` = 120, `hp` = 2000000000, `skill` = '[210,211,243,244]' WHERE `id` = 172;");
                ps.executeUpdate("UPDATE `boss` SET `hp` = 2000000000, `skill` = '[210,211,243,244]' WHERE `id` = 11 OR `mob_id` = 172;");
                ps.executeUpdate("UPDATE `parts` SET `data` = '[[8328,-2,-2],[8329,-2,-2],[8330,-2,-2],[8331,-2,-2],[8332,-1,-2]]' WHERE `id` = 729;");
                // Tự động sửa lỗi thiếu ngoặc đóng ] trong npcs map 62 (Vườn Cam Namie)
                ps.executeUpdate("UPDATE `maps` SET `npcs` = CONCAT(`npcs`, ']') WHERE `id` = 62 AND `npcs` LIKE '%[]]';");
                // Tự động đồng bộ cấu hình quái/trụ cho 5 map Chiến Trường 5vs5 (129 -> 133)
                ps.executeUpdate("UPDATE `maps` SET `mobs` = '[[123,250,288]]' WHERE `id` = 129 AND (`mobs` IS NULL OR `mobs` = '[]' OR `mobs` = '');");
                ps.executeUpdate("UPDATE `maps` SET `mobs` = '[[125,806,288]]' WHERE `id` = 130 AND (`mobs` IS NULL OR `mobs` = '[]' OR `mobs` = '');");
                ps.executeUpdate("UPDATE `maps` SET `mobs` = '[[122,350,288],[124,706,288]]' WHERE `id` = 131 AND (`mobs` IS NULL OR `mobs` = '[]' OR `mobs` = '');");
                ps.executeUpdate("UPDATE `maps` SET `mobs` = '[[122,350,288],[124,706,288]]' WHERE `id` = 132 AND (`mobs` IS NULL OR `mobs` = '[]' OR `mobs` = '');");
                ps.executeUpdate("UPDATE `maps` SET `mobs` = '[[122,350,288],[124,706,288]]' WHERE `id` = 133 AND (`mobs` IS NULL OR `mobs` = '[]' OR `mobs` = '');");
                // Tự động đồng bộ HP Trụ Chiến Trường 5vs5 (Trụ thường 100 HP, Trụ chính 200 HP)
                ps.executeUpdate("UPDATE `mobs` SET `hp` = 100 WHERE `id` IN (122, 124);");
                ps.executeUpdate("UPDATE `mobs` SET `hp` = 200 WHERE `id` IN (123, 125);");
                // Tự động đồng bộ Part 1118 và Tóc Hồng (Rose) vào bảng parts và itemhair
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1118, 5, '[[12978,1,-8],[12979,2,-8]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `itemhair` (`id`, `name`, `icon`, `beri`, `ruby`) VALUES (68, 'Tóc Hồng (Rose)', 1118, 0, 500) ON DUPLICATE KEY UPDATE `icon`=1118, `ruby`=500;");
                // Tự động đồng bộ Part 1122 và Tóc Đen (Black) vào bảng parts và itemhair
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1122, 5, '[[12980,1,-8],[12981,2,-8]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `itemhair` (`id`, `name`, `icon`, `beri`, `ruby`) VALUES (69, 'Tóc Đen (Black)', 1122, 0, 500) ON DUPLICATE KEY UPDATE `name`=VALUES(`name`), `icon`=1122, `ruby`=500;");
                // Tự động đồng bộ Thời trang Nezuko (Fashion 249 & Parts 1119, 1120, 1121)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1119, 0, '[[12939,-3,4],[12940,-3,4],[12940,-3,4],[12941,-3,4],[12942,-3,3]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1120, 1, '[[12943,0,0],[12944,0,0],[12945,0,0],[12946,0,0],[12947,0,0],[12948,0,0],[12949,0,0],[12950,0,0],[12951,0,0],[12952,0,0],[12953,0,0],[12954,0,0],[12955,0,0],[12956,0,0],[12957,0,0],[12958,0,0],[12959,0,0],[12960,0,0],[12961,0,0],[12962,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1121, 2, '[[12963,0,4],[12964,0,4],[12965,0,4],[12966,0,4],[12967,0,4],[12968,0,4],[12969,0,4],[12970,0,4],[12971,0,4],[12972,0,4],[12973,0,4],[12974,0,4],[12975,0,4],[12976,0,4],[12977,0,4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (249, 141, 'Thời trang Nezuko', 'Thời trang Nezuko Kamado\\n+10% Chí mạng\\n+10% Né tránh\\n+10% Miễn thương\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1120,-1,1121,1119,-2]', '[[10,100],[12,100],[53,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=141, `name`='Thời trang Nezuko', `info`='Thời trang Nezuko Kamado\\n+10% Chí mạng\\n+10% Né tránh\\n+10% Miễn thương\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1120,-1,1121,1119,-2]', `op`='[[10,100],[12,100],[53,100]]', `price`=-1;");
                // Tự động đồng bộ Bộ Thời trang mới (Fashion 250 & Parts 1123, 1124, 1125, 1126)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1123, 5, '[[12269,-1,-6],[12270,-1,-6]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1124, 1, '[[12271,0,0],[12272,0,0],[12273,0,0],[12274,0,0],[12275,0,0],[12276,0,0],[12277,0,0],[12278,0,0],[12279,0,0],[12280,0,0],[12281,0,0],[12282,0,0],[12283,0,0],[12284,0,0],[12285,0,0],[12286,0,0],[12287,0,0],[12288,0,0],[12289,0,0],[12290,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1125, 2, '[[12291,0,0],[12292,0,0],[12293,0,0],[12294,0,0],[12295,0,0],[12296,0,0],[12297,0,0],[12298,0,0],[12299,0,0],[12300,0,0],[12301,0,0],[12302,0,0],[12303,0,0],[12304,0,0],[12305,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1126, 3, '[[12306,3,0],[12307,20,-1],[12307,16,-1],[12308,20,-1],[12308,20,-1],[12307,19,-1],[12309,26,-2],[12307,27,-1],[12307,27,-1],[12310,-1,5],[79,0,0],[79,0,0],[12311,-2,3],[12312,0,2],[12313,5,8],[79,0,0],[12314,-1,-1],[12314,-1,-1],[12316,5,-2],[12315,0,-2],[12316,5,-2],[12315,0,-2],[12317,2,0],[12318,2,-1]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (250, 142, 'Thời trang Sabo', 'Thời trang Sabo\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[1126,-2,-1,1124,-1,1125,-1,1123]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=142, `name`='Thời trang Sabo', `info`='Thời trang Sabo\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[1126,-2,-1,1124,-1,1125,-1,1123]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Robin (Fashion 251 & Parts 1127, 1128, 1129)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1127, 0, '[[12319,-3,-3],[12320,-3,-3],[12321,-3,-3],[12322,-3,-3],[12323,-3,-3]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1128, 1, '[[12324,-2,-4],[12325,-2,-1],[12326,-2,-1],[12327,-2,-1],[12328,-2,-1],[12329,-2,-4],[12330,-2,-4],[12331,-2,-4],[12332,-2,-4],[12333,-2,-4],[12334,-2,-4],[12335,-2,-4],[12336,-2,-4],[12337,-2,-4],[12338,-2,-4],[12339,-2,-4],[12340,-2,-4],[12341,-2,-4],[12342,-2,-4],[12343,-2,-4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1129, 2, '[[12344,0,0],[12345,0,0],[12346,0,0],[12347,0,0],[12348,0,0],[12349,0,0],[12350,0,0],[12351,0,0],[12352,0,0],[12353,0,0],[12354,0,0],[12355,0,0],[12356,0,0],[12357,0,0],[12358,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (251, 143, 'Thời trang Robin', 'Thời trang Robin\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1128,-1,1129,1127,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=143, `name`='Thời trang Robin', `info`='Thời trang Robin\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1128,-1,1129,1127,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Gecko Moria (Fashion 252 & Parts 1130, 1131, 1132)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1130, 0, '[[12399,-1,-40],[12400,-1,-40],[12401,-1,-40],[12402,-1,-40],[12403,-1,-40]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1131, 1, '[[12404,-13,-26],[12405,-13,-26],[12406,-13,-26],[12407,-13,-26],[12408,-13,-26],[12409,-13,-26],[12410,-13,-26],[12411,-13,-26],[12412,-13,-26],[12413,-13,-26],[12414,-13,-26],[12415,-13,-26],[12416,-13,-26],[12417,-13,-26],[12418,-13,-26],[12419,-13,-26],[12420,-13,-26],[12421,-13,-26],[12422,-13,-26],[12423,-13,-26]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1132, 2, '[[12424,0,0],[12425,0,0],[12426,0,0],[12427,0,0],[12428,0,0],[12429,0,0],[12430,0,0],[12431,0,0],[12432,0,0],[12433,0,0],[12434,0,0],[12435,0,0],[12436,0,0],[12437,0,0],[12438,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (252, 158, 'Thời trang Gecko Moria', 'Thời trang Gecko Moria\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1131,-1,1132,1130,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=158, `name`='Thời trang Gecko Moria', `info`='Thời trang Gecko Moria\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1131,-1,1132,1130,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Jinbe (Fashion 253 & Parts 1133, 1134, 1135)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1133, 0, '[[12439,7,-23],[12440,7,-23],[12441,7,-23],[12442,7,-23],[12443,7,-23]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1134, 1, '[[12444,-6,-20],[12445,-6,-20],[12446,-6,-20],[12447,-6,-20],[12448,-6,-20],[12449,-6,-20],[12450,-6,-20],[12451,-6,-20],[12452,-6,-20],[12453,-6,-20],[12454,-6,-20],[12455,-6,-20],[12456,-6,-20],[12457,-6,-20],[12458,-6,-20],[12459,-6,-20],[12460,-6,-20],[12461,-6,-20],[12462,-6,-20],[12463,-6,-20]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1135, 2, '[[12464,0,0],[12465,0,0],[12466,0,0],[12467,0,0],[12468,0,0],[12469,0,0],[12470,0,0],[12471,0,0],[12472,0,0],[12473,0,0],[12474,0,0],[12475,0,0],[12476,0,0],[12477,0,0],[12478,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (253, 159, 'Thời trang Jinbe', 'Thời trang Jinbe\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1134,-1,1135,1133,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=159, `name`='Thời trang Jinbe', `info`='Thời trang Jinbe\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1134,-1,1135,1133,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Vy Ngu (Fashion 254 & Parts 1136, 1137, 1138)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1136, 0, '[[12530,-4,3],[12531,-4,3],[12532,-4,3],[12533,-4,3],[12534,-4,3]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1137, 1, '[[12535,-3,-9],[12536,-3,-9],[12537,-3,-9],[12538,-3,-9],[12539,-3,-9],[12540,-3,-9],[12541,-3,-9],[12542,-3,-9],[12543,-3,-9],[12544,-3,-9],[12545,-3,-9],[12546,-3,-9],[12547,-3,-9],[12548,-3,-9],[12549,-3,-9],[12550,-3,-9],[12551,-3,-9],[12552,-3,-9],[12553,-3,-9],[12554,-3,-9]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1138, 2, '[[12555,0,0],[12556,0,0],[12557,0,0],[12558,0,0],[12559,0,0],[12560,0,0],[12561,0,0],[12562,0,0],[12563,0,0],[12564,0,0],[12565,0,0],[12566,0,0],[12567,0,0],[12568,0,0],[12569,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (254, 147, 'Thời trang Vy Ngu', 'Thời trang Vy Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1137,-1,1138,1136,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=147, `name`='Thời trang Vy Ngu', `info`='Thời trang Vy Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1137,-1,1138,1136,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Cavendis Ngu (Fashion 255 & Parts 1139, 1140, 1141)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1139, 0, '[[12570,-5,-5],[12571,-5,-5],[12572,-5,-5],[12573,-5,-5],[12574,-5,-5]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1140, 1, '[[12575,-2,-15],[12576,-2,-15],[12577,-2,-15],[12578,-2,-15],[12579,-2,-15],[12580,-2,-15],[12581,-2,-15],[12582,-2,-15],[12583,-2,-15],[12584,-2,-15],[12585,-2,-15],[12586,-2,-15],[12587,-2,-15],[12588,-2,-15],[12589,-2,-15],[12590,-2,-15],[12591,-2,-15],[12592,-2,-15],[12593,-2,-15],[12594,-2,-15]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1141, 2, '[[12595,0,0],[12596,0,0],[12597,0,0],[12598,0,0],[12599,0,0],[12600,0,0],[12601,0,0],[12602,0,0],[12603,0,0],[12604,0,0],[12605,0,0],[12606,0,0],[12607,0,0],[12608,0,0],[12609,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (255, 148, 'Thời trang Cavendis Ngu', 'Thời trang Cavendis Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1140,-1,1141,1139,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=148, `name`='Thời trang Cavendis Ngu', `info`='Thời trang Cavendis Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1140,-1,1141,1139,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Natra Ngu (Fashion 36 & Parts 1142, 1143, 1144, 1145)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1142, 0, '[[12805,-2,-2],[12806,-2,-2],[12807,-2,-2],[12808,-2,-2],[12809,-2,-2]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1143, 1, '[[12810,0,0],[12811,0,0],[12812,0,0],[12813,0,0],[12814,0,0],[12815,0,0],[12816,0,0],[12817,0,0],[12818,0,0],[12819,0,0],[12820,0,0],[12821,0,0],[12822,0,0],[12823,0,0],[12824,0,0],[12825,0,0],[12826,0,0],[12827,0,0],[12828,0,0],[12829,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1144, 2, '[[12830,0,0],[12831,0,0],[12832,0,0],[12833,0,0],[12834,0,0],[12835,0,0],[12836,0,0],[12837,0,0],[12838,0,0],[12839,0,0],[12840,0,0],[12841,0,0],[12842,0,0],[12843,0,0],[12844,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1145, 3, '[[12845,3,0],[12846,20,-1],[12846,16,-1],[12847,20,-1],[12847,20,-1],[12846,19,-1],[12848,26,-2],[12846,27,-1],[12846,27,-1],[12849,-1,5],[79,0,0],[79,0,0],[12850,-2,3],[12851,0,2],[12852,5,8],[79,0,0],[12853,-1,-1],[12853,-1,-1],[12855,5,-2],[12854,0,-2],[12855,5,-2],[12854,0,-2],[12856,2,0],[12857,2,-1]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (36, 154, 'Thời trang Natra Ngu', 'Thời trang Natra Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[1145,-2,-1,1143,-1,1144,1142,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=154, `name`='Thời trang Natra Ngu', `info`='Thời trang Natra Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[1145,-2,-1,1143,-1,1144,1142,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Long Ngu (Fashion 37 & Parts 1146, 1147, 1148)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1146, 0, '[[12858,-2,-4],[12859,-2,-4],[12860,-2,-4],[12861,-2,-4],[12862,-2,-4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1147, 1, '[[12863,0,0],[12864,0,0],[12865,0,0],[12866,0,0],[12867,0,0],[12868,0,0],[12869,0,0],[12870,0,0],[12871,0,0],[12872,0,0],[12873,0,0],[12874,0,0],[12875,0,0],[12876,0,0],[12877,0,0],[12878,0,0],[12879,0,0],[12880,0,0],[12881,0,0],[12882,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1148, 2, '[[12883,4,0],[12884,4,0],[12885,4,0],[12886,4,0],[12887,4,0],[12888,4,0],[12889,4,0],[12890,4,0],[12891,4,0],[12892,4,0],[12893,4,0],[12894,4,0],[12895,4,0],[12896,4,0],[12897,4,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (37, 155, 'Thời trang Long Ngu', 'Thời trang Long Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1147,-1,1148,1146,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=155, `name`='Thời trang Long Ngu', `info`='Thời trang Long Ngu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1147,-1,1148,1146,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
                // Tự động đồng bộ Thời trang Nu (Fashion 38 & Parts 1149, 1150, 1151)
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1149, 0, '[[12899,-2,-4],[12900,-2,-4],[12901,-2,-4],[12902,-2,-4],[12903,-2,-4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1150, 1, '[[12904,0,0],[12905,0,0],[12906,0,0],[12907,0,0],[12908,0,0],[12909,0,0],[12910,0,0],[12911,0,0],[12912,0,0],[12913,0,0],[12914,0,0],[12915,0,0],[12916,0,0],[12917,0,0],[12918,0,0],[12919,0,0],[12920,0,0],[12921,0,0],[12922,0,0],[12923,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1151, 2, '[[12924,0,0],[12925,0,0],[12926,0,0],[12927,0,0],[12928,0,0],[12929,0,0],[12930,0,0],[12931,0,0],[12932,0,0],[12933,0,0],[12934,0,0],[12935,0,0],[12936,0,0],[12937,0,0],[12938,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);");
                ps.executeUpdate("INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (38, 156, 'Thời trang Nu', 'Thời trang Nu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1150,-1,1151,1149,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=156, `name`='Thời trang Nu', `info`='Thời trang Nu\\n+10% Miễn thương\\n+10% Giảm miễn thương\\n+10% Chí mạng\\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1150,-1,1151,1149,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;");
            } catch (Exception ignored) {
            }
            // load mobs
            String query = "SELECT * FROM `mobs`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                MobTemplate temp = new MobTemplate();
                temp.mob_id = Short.parseShort(rs.getString("id"));
                temp.name = rs.getString("name");
                temp.level = Short.parseShort(rs.getString("level"));
                temp.hp_max = Integer.parseInt(rs.getString("hp"));
                temp.hOne = Short.parseShort(rs.getString("hOne"));
                temp.typemove = Byte.parseByte(rs.getString("typemove"));
                temp.ishuman = Byte.parseByte(rs.getString("ishuman"));
                temp.typemonster = Byte.parseByte(rs.getString("typemonster"));
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("idicon"));
                if (temp.ishuman == 0) {
                    temp.icon = Short.parseShort(js.get(1).toString());
                } else if (temp.ishuman == 1) {
                    temp.head = Short.parseShort(js.get(1).toString());
                    temp.hair = Short.parseShort(js.get(2).toString());
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(3).toString());
                    temp.wearing = new short[js2.size()];
                    for (int i = 0; i < temp.wearing.length; i++) {
                        temp.wearing[i] = Short.parseShort(js2.get(i).toString());
                    }
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("skill"));
                temp.skill = new short[js.size()];
                for (int i = 0; i < temp.skill.length; i++) {
                    temp.skill[i] = Short.parseShort(js.get(i).toString());
                }
                js.clear();
                if (temp.mob_id == 174 || (temp.name != null && temp.name.toLowerCase().contains("saturn"))) {
                    temp.skill = new short[] { 195, 196, 197 };
                }
                if (temp.mob_id == 172 || (temp.name != null && (temp.name.toLowerCase().contains("râu trắng") || temp.name.toLowerCase().contains("rau trang")))) {
                    temp.hOne = 120;
                    temp.skill = new short[] { 210, 211, 243, 244 };
                }
                MobTemplate.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load mob ok");
            query = "SELECT * FROM `shoptichluy` ORDER BY `type`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ShopTichLuy temp = new ShopTichLuy();
                temp.id = rs.getShort("id");
                temp.type = rs.getByte("type");
                temp.point = rs.getInt("point");
                temp.info = rs.getString("info");
                temp.limit = rs.getInt("limit");
                temp.limit_data = new HashMap<>();
                JSONArray jsar = (JSONArray) JSONValue.parse(rs.getString("limit_data"));
                for (int i = 0; i < jsar.size(); i++) {
                    JSONArray js_in = (JSONArray) jsar.get(i);
                    int value = Integer.parseInt(js_in.get(1).toString());
                    temp.limit_data.put(js_in.get(0).toString(), value);
                }
                ShopTichLuy.ENTRY.add(temp);
            }
            rs.close();

            // load map
            query = "SELECT * FROM `maps`;";
            rs = ps.executeQuery(query);
            MapTemplate.ENTRYS = new ArrayList<>();
            while (rs.next()) {
                short id_map = rs.getShort("id");
                File f = new File("data/map/" + id_map);
                if (!f.exists()) {
                    // [DEBUG HAKI] Map folder not found - thu muc map khong ton tai
                    System.err.println("[DEBUG HAKI] SKIP map " + id_map + " - folder not found: data/map/" + id_map);
                    continue;
                }
                // [DEBUG HAKI] Log map 2000 load success
                if (id_map == 2000) {
                    System.out.println("[DEBUG HAKI] Map 2000 folder found: " + f.getAbsolutePath());
                }
                //
                MapTemplate map_temp = new MapTemplate();
                map_temp.id = id_map;
                map_temp.name = rs.getString("name");
                map_temp.max_zone = rs.getByte("maxzone");
                map_temp.max_player = rs.getByte("maxplayer");
                // npc
                String npcsStr = rs.getString("npcs");
                JSONArray js_npc = null;
                if (npcsStr != null && !npcsStr.trim().isEmpty() && !npcsStr.equals("null")) {
                    try {
                        js_npc = (JSONArray) JSONValue.parse(npcsStr);
                    } catch (Exception e) {}
                    // Tự động sửa lỗi thiếu ngoặc đóng ] ở cuối (ví dụ map 62: [[...[]])
                    if (js_npc == null && npcsStr.startsWith("[[") && !npcsStr.endsWith("]]")) {
                        try {
                            js_npc = (JSONArray) JSONValue.parse(npcsStr + "]");
                        } catch (Exception ignored) {}
                    }
                }
                if (js_npc == null) {
                    js_npc = new JSONArray();
                }
                map_temp.npcs = new ArrayList<>();
                for (int i = 0; i < js_npc.size(); i++) {
                    try {
                        Object obj = JSONValue.parse(js_npc.get(i).toString());
                        if (!(obj instanceof JSONArray)) {
                            continue;
                        }
                        JSONArray js_npc_temp = (JSONArray) obj;
                        Npc npc = new Npc();
                        npc.iditem = Short.parseShort(js_npc_temp.get(0).toString());
                        npc.name = js_npc_temp.get(1).toString();
                        npc.namegt = js_npc_temp.get(2).toString();
                        npc.chat = js_npc_temp.get(3).toString();
                        npc.x = Short.parseShort(js_npc_temp.get(4).toString());
                        npc.y = Short.parseShort(js_npc_temp.get(5).toString());
                        npc.isPerson = Byte.parseByte(js_npc_temp.get(6).toString());
                        npc.typeIcon = Byte.parseByte(js_npc_temp.get(7).toString());
                        npc.wBlock = Byte.parseByte(js_npc_temp.get(8).toString());
                        npc.hBlock = Byte.parseByte(js_npc_temp.get(9).toString());
                        npc.b3 = Byte.parseByte(js_npc_temp.get(10).toString());
                        JSONArray js_npc_temp_2 = (JSONArray) JSONValue.parse(js_npc_temp.get(11).toString());
                        npc.dataFrame = new byte[js_npc_temp_2.size()];
                        for (int j = 0; j < npc.dataFrame.length; j++) {
                            npc.dataFrame[j] = Byte.parseByte(js_npc_temp_2.get(j).toString());
                        }
                        npc.head = Short.parseShort(js_npc_temp.get(12).toString());
                        npc.hair = Short.parseShort(js_npc_temp.get(13).toString());
                        JSONArray js_npc_temp_3 = (JSONArray) JSONValue.parse(js_npc_temp.get(14).toString());
                        npc.wearing = new short[js_npc_temp_3.size()];
                        for (int k = 0; k < npc.wearing.length; k++) {
                            npc.wearing[k] = Short.parseShort(js_npc_temp_3.get(k).toString());
                        }
                        // Đảm bảo NPC dạng người (như Ngộ Không, Chị Hằng,...) luôn có bóng dưới chân (isPerson = 1)
                        if ((npc.iditem == -201 || npc.iditem == -154 || npc.iditem == -202
                                || (npc.name != null && (npc.name.contains("Ngộ Không") || npc.name.contains("Chị Hằng"))))
                                && npc.isPerson == 0) {
                            npc.isPerson = 1;
                        }
                        map_temp.npcs.add(npc);
                    } catch (Exception e) {
                        System.err.println("[DEBUG HAKI] Skip invalid NPC entry in map " + id_map + ": " + e.getMessage());
                    }
                }
                // Tự động kiểm tra và thêm NPC Ngân Hàng vào Làng Cối Xay Gió (Map 1) nếu chưa có
                if (id_map == 1) {
                    boolean hasBank = false;
                    for (int nIdx = 0; nIdx < map_temp.npcs.size(); nIdx++) {
                        Npc n = map_temp.npcs.get(nIdx);
                        if (n.iditem == activities.Bank.NPC_ID_BANK
                                || (n.name != null && (n.name.equalsIgnoreCase("Ngân hàng") || n.name.equalsIgnoreCase("ATM") || n.name.equalsIgnoreCase("Ngân Hàng")))) {
                            hasBank = true;
                            break;
                        }
                    }
                    if (!hasBank) {
                        Npc bankNpc = new Npc();
                        bankNpc.iditem = activities.Bank.NPC_ID_BANK;
                        bankNpc.name = "Ngân hàng";
                        bankNpc.namegt = "Giao dịch";
                        bankNpc.chat = "Ngân hàng nạp tiền và đổi coin tự động 24/7!";
                        bankNpc.x = 420;
                        bankNpc.y = 173;
                        bankNpc.isPerson = 0;
                        bankNpc.typeIcon = 0;
                        bankNpc.wBlock = 0;
                        bankNpc.hBlock = 0;
                        bankNpc.b3 = 0;
                        bankNpc.dataFrame = new byte[] { 71, 2 };
                        bankNpc.head = 0;
                        bankNpc.hair = 0;
                        bankNpc.wearing = new short[0];
                        map_temp.npcs.add(bankNpc);
                    }
                }
                // Tự động kiểm tra và thêm NPC Namie vào Vườn Cam Namie (Map 62) nếu chưa có
                if (id_map == 62) {
                    boolean hasNamie = false;
                    for (int nIdx = 0; nIdx < map_temp.npcs.size(); nIdx++) {
                        Npc n = map_temp.npcs.get(nIdx);
                        if (n.iditem == -72 || (n.name != null && n.name.equalsIgnoreCase("Namie"))) {
                            hasNamie = true;
                            break;
                        }
                    }
                    if (!hasNamie) {
                        Npc namiNpc = new Npc();
                        namiNpc.iditem = -72;
                        namiNpc.name = "Namie";
                        namiNpc.namegt = "Thông tin";
                        namiNpc.chat = "Ngươi đang ở trong vườn cam của ta.";
                        namiNpc.x = 550;
                        namiNpc.y = 172;
                        namiNpc.isPerson = 1;
                        namiNpc.typeIcon = -1;
                        namiNpc.wBlock = 0;
                        namiNpc.hBlock = 0;
                        namiNpc.b3 = 0;
                        namiNpc.dataFrame = new byte[] { 30, 2 };
                        namiNpc.head = 0;
                        namiNpc.hair = 0;
                        namiNpc.wearing = new short[0];
                        map_temp.npcs.add(namiNpc);
                    }
                }
                js_npc.clear();
                String boatStr = rs.getString("boat");
                js_npc = (boatStr != null && !boatStr.trim().isEmpty() && !boatStr.equals("null"))
                        ? (JSONArray) JSONValue.parse(boatStr)
                        : new JSONArray();
                map_temp.list_boat = new ArrayList<>();
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_temp = (JSONArray) js_npc.get(i);
                    Boat_In_Map temp_boat = new Boat_In_Map();
                    temp_boat.x = Short.parseShort(js_temp.get(0).toString());
                    temp_boat.y = Short.parseShort(js_temp.get(1).toString());
                    map_temp.list_boat.add(temp_boat);
                }
                js_npc.clear();
                map_temp.vgos = new ArrayList<>();
                String vgosStr = rs.getString("vgos");
                js_npc = (vgosStr != null && !vgosStr.trim().isEmpty() && !vgosStr.equals("null"))
                        ? (JSONArray) JSONValue.parse(vgosStr)
                        : new JSONArray();
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_0 = (JSONArray) js_npc.get(i);
                    Vgo vgo_temp = new Vgo();
                    vgo_temp.id_map_go = Short.parseShort(js_0.get(0).toString());
                    vgo_temp.xold = Short.parseShort(js_0.get(1).toString());
                    vgo_temp.yold = Short.parseShort(js_0.get(2).toString());
                    vgo_temp.xnew = Short.parseShort(js_0.get(3).toString());
                    vgo_temp.ynew = Short.parseShort(js_0.get(4).toString());
                    if (vgo_temp.id_map_go != -1) {
                        map_temp.vgos.add(vgo_temp);
                    }
                }
                js_npc.clear();
                map_temp.type_view_p = rs.getByte("typeViewPlayer");
                map_temp.b = rs.getByte("b");
                map_temp.specMap = rs.getByte("specMap");
                String dataStr = rs.getString("data");
                js_npc = (dataStr != null && !dataStr.trim().isEmpty() && !dataStr.equals("null"))
                        ? (JSONArray) JSONValue.parse(dataStr)
                        : new JSONArray();
                map_temp.data = new byte[2][];
                for (int i = 0; i < 2; i++) {
                    if (i < js_npc.size()) {
                        JSONArray js_in = (JSONArray) js_npc.get(i);
                        map_temp.data[i] = new byte[js_in.size()];
                        for (int j = 0; j < map_temp.data[i].length; j++) {
                            map_temp.data[i][j] = Byte.parseByte(js_in.get(j).toString());
                        }
                    } else {
                        map_temp.data[i] = new byte[0];
                    }
                }
                js_npc.clear();
                // System.out.println(id_map);
                String mapBackStr = rs.getString("MapBack");
                js_npc = (mapBackStr != null && !mapBackStr.trim().isEmpty() && !mapBackStr.equals("null"))
                        ? (JSONArray) JSONValue.parse(mapBackStr)
                        : new JSONArray();
                if (js_npc.size() >= 4) {
                    map_temp.IDBack = Byte.parseByte(js_npc.get(0).toString());
                    map_temp.HBack = Short.parseShort(js_npc.get(1).toString());
                    map_temp.maxW = Short.parseShort(js_npc.get(2).toString());
                    map_temp.maxH = Short.parseShort(js_npc.get(3).toString());
                } else {
                    map_temp.IDBack = 0;
                    map_temp.HBack = 0;
                    map_temp.maxW = 0;
                    map_temp.maxH = 0;
                }
                if (id_map == 2000) {
                    map_temp.IDBack = (byte) 81;
                    if (map_temp.HBack <= 0) {
                        map_temp.HBack = 280;
                    }
                }
                if (id_map == 2028) {
                    map_temp.IDBack = (byte) 87;
                    if (map_temp.HBack <= 0) {
                        map_temp.HBack = 280;
                    }
                }
                js_npc.clear();
                map_temp.id_eff_map = rs.getByte("id_eff_map");
                map_temp.level = rs.getByte("level");
                map_temp.typeChangeMap = rs.getByte("typeChangeMap");
                String mPosMapTrainStr = rs.getString("mPosMapTrain");
                js_npc = (mPosMapTrainStr != null && !mPosMapTrainStr.trim().isEmpty()
                        && !mPosMapTrainStr.equals("null"))
                                ? (JSONArray) JSONValue.parse(mPosMapTrainStr)
                                : new JSONArray();
                map_temp.mPosMapTrain = new byte[js_npc.size()][];
                for (int i = 0; i < js_npc.size(); i++) {
                    JSONArray js_in = (JSONArray) js_npc.get(i);
                    map_temp.mPosMapTrain[i] = new byte[js_in.size()];
                    for (int j = 0; j < map_temp.mPosMapTrain[i].length; j++) {
                        map_temp.mPosMapTrain[i][j] = Byte.parseByte(js_in.get(j).toString());
                    }
                }
                js_npc.clear();
                map_temp.strTimeChange = rs.getString("strTimeChange");
                MapTemplate.ENTRYS.add(map_temp);
                //
                String mob_json = rs.getString("mobs");
                Map[] m_temp = new Map[map_temp.max_zone];
                for (int i2 = 0; i2 < m_temp.length; i2++) {
                    m_temp[i2] = new Map();
                    m_temp[i2].zone_id = (byte) i2;
                    m_temp[i2].template = map_temp;
                    JSONArray js = (mob_json != null && !mob_json.trim().isEmpty() && !mob_json.equals("null"))
                            ? (JSONArray) JSONValue.parse(mob_json)
                            : new JSONArray();
                    // Loại bỏ lọc khu 0 để chuẩn bị chọn khu ngẫu nhiên
                    m_temp[i2].list_mob = new int[js.size()];
                    for (int i = 0; i < js.size(); i++) {
                        JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                        Mob temp = new Mob();
                        int mobId = Integer.parseInt(js2.get(0).toString());
                        if (mobId < 0 || mobId >= MobTemplate.ENTRYS.size()) {
                            System.err.println("Warning: Mob template id " + mobId + " out of bounds for map " + map_temp.id + ", falling back to 0");
                            mobId = 0;
                        }
                        temp.mob_template = MobTemplate.ENTRYS.get(mobId);
                        temp.x = Short.parseShort(js2.get(1).toString());
                        temp.y = Short.parseShort(js2.get(2).toString());
                        temp.hp_max = temp.mob_template.hp_max;
                        temp.hp = temp.hp_max;
                        temp.level = temp.mob_template.level;
                        temp.isdie = false;
                        temp.id_target = -1;
                        temp.index = this.index_mob;
                        temp.map = m_temp[i2];
                        temp.boss_info = null;
                        // Thiết lập chỉ số chiến đấu đặc biệt cho Boss Đảo Ruby (Map 1001, Quái vật tuyết)
                        if (map_temp.id == 1001) {
                            temp.base_dame = 200000;
                            temp.final_dame = 200000;
                            temp.mien_thuong = 80;       // 80% miễn thương (tương đương 10 tỷ máu hiệu dụng)
                            temp.giam_mien_thuong = 400; // Giảm 40% miễn thương của đối thủ khi boss đánh
                            temp.ne_don = 25;            // 25% né tránh đòn đánh
                            temp.phan_dame = 20;         // 20% phản sát thương lại người chơi
                        }
                        Mob.ENTRYS.put(this.index_mob, temp);
                        m_temp[i2].list_mob[i] = this.index_mob;
                        this.index_mob++;
                    }
                }
                Map.ENTRYS.add(m_temp);
                if (map_temp.id == 6) {
                    int activeZone = Util.random(m_temp.length);
                    for (int z = 0; z < m_temp.length; z++) {
                        if (z != activeZone) {
                            for (int mId : m_temp[z].list_mob) {
                                map.Mob m = map.Mob.ENTRYS.get(mId);
                                if (m != null && m.mob_template != null && m.mob_template.mob_id == 174) {
                                    m.hp = 0;
                                    m.isdie = true;
                                    m.time_refresh = Long.MAX_VALUE; // Hide it until its turn
                                }
                            }
                        }
                    }
                }
            }
            rs.close();
            // Đảm bảo Map 2028 (Đảo Huấn Luyện Pet) luôn được nạp từ data Map 2 (1-1 Rừng Làng)
            if (Map.get_map_by_id(2028) == null) {
                Map[] map2 = Map.get_map_by_id(2);
                if (map2 != null && map2.length > 0 && map2[0].template != null) {
                    MapTemplate temp2 = map2[0].template;
                    MapTemplate petMapTemp = new MapTemplate();
                    petMapTemp.id = 2028;
                    petMapTemp.name = "Đảo Huấn Luyện Pet";
                    petMapTemp.max_zone = (byte) 10;
                    petMapTemp.max_player = (byte) 30;
                    petMapTemp.data = temp2.data;
                    petMapTemp.IDBack = (byte) 87;
                    petMapTemp.HBack = 280;
                    petMapTemp.maxW = temp2.maxW;
                    petMapTemp.maxH = temp2.maxH;
                    petMapTemp.type_view_p = 0;
                    petMapTemp.b = temp2.b;
                    petMapTemp.specMap = 0;
                    petMapTemp.id_eff_map = temp2.id_eff_map;
                    petMapTemp.level = temp2.level;
                    petMapTemp.typeChangeMap = temp2.typeChangeMap;
                    petMapTemp.mPosMapTrain = temp2.mPosMapTrain;
                    petMapTemp.strTimeChange = temp2.strTimeChange;
                    petMapTemp.list_boat = new ArrayList<>();
                    petMapTemp.vgos = new ArrayList<>();
                    petMapTemp.npcs = new ArrayList<>();

                    // NPC Huấn Luyện Sư (-999)
                    Npc npcHL = new Npc();
                    npcHL.iditem = -999;
                    npcHL.name = "Huấn Luyện Sư";
                    npcHL.namegt = "Huấn Luyện Pet";
                    npcHL.chat = "Chào mừng bạn đến với Đảo Huấn Luyện Pet! Tại đây pet của bạn sẽ tự động nhận EXP mỗi phút khi Online.";
                    npcHL.x = 200;
                    npcHL.y = 200;
                    npcHL.isPerson = 1;
                    npcHL.typeIcon = 0;
                    npcHL.wBlock = 0;
                    npcHL.hBlock = 0;
                    npcHL.b3 = 0;
                    npcHL.dataFrame = new byte[] { 71, 2 };
                    npcHL.head = 0;
                    npcHL.hair = 0;
                    npcHL.wearing = new short[0];
                    petMapTemp.npcs.add(npcHL);

                    // NPC Chuyển khu (-7)
                    Npc npcKhu = new Npc();
                    npcKhu.iditem = -7;
                    npcKhu.name = " ";
                    npcKhu.namegt = "Chuyển khu";
                    npcKhu.chat = "";
                    npcKhu.x = 122;
                    npcKhu.y = 173;
                    npcKhu.isPerson = 99;
                    npcKhu.typeIcon = -1;
                    npcKhu.wBlock = 24;
                    npcKhu.hBlock = 24;
                    npcKhu.b3 = 0;
                    npcKhu.dataFrame = new byte[] { 5, 1 };
                    npcKhu.head = 0;
                    npcKhu.hair = 0;
                    npcKhu.wearing = new short[0];
                    petMapTemp.npcs.add(npcKhu);

                    MapTemplate.ENTRYS.add(petMapTemp);
                    String mobPetJson = "[[167,336,168],[167,360,264],[167,480,192],[167,504,288],[167,408,216],[167,264,216],[167,168,192],[167,120,288],[167,216,264],[167,552,216],[167,432,312],[167,288,312],[167,768,192],[167,840,216],[167,744,288],[167,816,312],[167,888,264],[167,912,192],[167,960,240],[167,936,312],[167,1032,168],[167,1080,288]]";
                    JSONArray jsMobs = (JSONArray) JSONValue.parse(mobPetJson);
                    Map[] petMapArr = new Map[petMapTemp.max_zone];
                    for (int z = 0; z < petMapArr.length; z++) {
                        petMapArr[z] = new Map();
                        petMapArr[z].zone_id = (byte) z;
                        petMapArr[z].template = petMapTemp;
                        petMapArr[z].list_mob = new int[jsMobs.size()];
                        for (int mIdx = 0; mIdx < jsMobs.size(); mIdx++) {
                            JSONArray js2 = (JSONArray) JSONValue.parse(jsMobs.get(mIdx).toString());
                            Mob temp = new Mob();
                            int mobId = Integer.parseInt(js2.get(0).toString());
                            if (mobId < 0 || mobId >= MobTemplate.ENTRYS.size()) {
                                mobId = 0;
                            }
                            temp.mob_template = MobTemplate.ENTRYS.get(mobId);
                            temp.x = Short.parseShort(js2.get(1).toString());
                            temp.y = Short.parseShort(js2.get(2).toString());
                            temp.hp_max = temp.mob_template.hp_max;
                            temp.hp = temp.hp_max;
                            temp.level = temp.mob_template.level;
                            temp.isdie = false;
                            temp.id_target = -1;
                            temp.index = this.index_mob;
                            temp.map = petMapArr[z];
                            temp.boss_info = null;
                            Mob.ENTRYS.put(this.index_mob, temp);
                            petMapArr[z].list_mob[mIdx] = this.index_mob;
                            this.index_mob++;
                        }
                    }
                    Map.ENTRYS.add(petMapArr);
                    System.out.println("[PetTraining] Successfully registered Map 2028 with mob 167 (cloned from Map 2)");
                }
            }
            for (int i = 0; i < MapTemplate.ENTRYS.size(); i++) {
                for (int j = 0; j < MapTemplate.ENTRYS.get(i).vgos.size(); j++) {
                    Vgo vgo = MapTemplate.ENTRYS.get(i).vgos.get(j);
                    vgo.map_go = Map.get_map_by_id(vgo.id_map_go);
                    if (vgo.map_go == null) {
                        vgo.map_go = Map.get_map_by_id(1);
                    }
                }
            }
            System.out.println("load map ok");
            // load part
            query = "SELECT * FROM `parts`;";
            Part.ENTRY = new ArrayList<>();
            rs = ps.executeQuery(query);
            while (rs.next()) {
                byte type = rs.getByte("type");
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("data"));
                Part part = new Part(type);
                part.id = rs.getShort("id");
                if (part.pi == null || part.pi.length < js.size()) {
                    part.pi = new PartImg[js.size()];
                }
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    part.pi[i] = new PartImg();
                    part.pi[i].id = Short.parseShort(js_in.get(0).toString());
                    part.pi[i].dx = Byte.parseByte(js_in.get(1).toString());
                    part.pi[i].dy = Byte.parseByte(js_in.get(2).toString());
                }
                if (part.id == 729) {
                    for (int i = 0; i < part.pi.length; i++) {
                        part.pi[i].dy = -2;
                    }
                }
                Part.ENTRY.add(part);
            }
            rs.close();
            // load item 3
            query = "SELECT * FROM `item3`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate3 temp = new ItemTemplate3();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.clazz = rs.getByte("clazz");
                temp.typeEquip = rs.getByte("typeequip");
                temp.icon = rs.getShort("icon");
                temp.level = rs.getShort("level");
                temp.color = rs.getByte("color");
                temp.typelock = rs.getByte("typelock");
                temp.numHoleDaDuc = rs.getByte("numHoleDaDuc");
                // temp.valueChetac = rs.getShort("chetac");
                temp.valueChetac = (short) (100);
                temp.isHoanMy = rs.getByte("ishoanmy");
                temp.valueKichAn = rs.getByte("valuekichan");
                // System.out.println(temp.id);
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("op_1"));
                temp.option_item = new ArrayList<>();
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                    temp.option_item.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Short.parseShort(js2.get(1).toString())));
                }
                js.clear();
                temp.option_item_2 = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("op_2"));
                for (int k = 0; k < js.size(); k++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(k).toString());
                    temp.option_item_2.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Short.parseShort(js2.get(1).toString())));
                }
                js.clear();
                temp.numLoKham = rs.getByte("numlokham");
                js = (JSONArray) JSONValue.parse(rs.getString("mdakham"));
                temp.mdakham = new short[js.size()];
                for (int l = 0; l < temp.mdakham.length; l++) {
                    temp.mdakham[l] = Short.parseShort(js.get(l).toString());
                }
                temp.part = rs.getShort("part");
                temp.beri = rs.getInt("beri");
                temp.ruby = rs.getInt("ruby");
                // Part.get_part(temp.id);;
                ItemTemplate3.ENTRYS.add(temp);
            }
            rs.close();
            // load info item4
            query = "SELECT * FROM `item4_info`;";
            ItemTemplate4_Info.ENTRY = new ArrayList<>();
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate4_Info temp = new ItemTemplate4_Info();
                temp.id = rs.getShort("id");
                temp.info = rs.getString("info");
                ItemTemplate4_Info.ENTRY.add(temp);
            }
            rs.close();
            // load item temp 4
            query = "SELECT * FROM `item4`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate4 temp = new ItemTemplate4();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.icon = rs.getShort("icon");
                temp.indexInfoPotion = rs.getShort("indexInfoPotion");
                temp.beri = rs.getInt("price");
                temp.ruby = rs.getShort("priceruby");
                temp.istrade = rs.getByte("istrade");
                temp.type = rs.getByte("hpmpother");
                temp.timedelay = rs.getShort("timedelay");
                temp.value = rs.getShort("value");
                temp.timeactive = rs.getShort("timeactive");
                temp.nameuse = rs.getString("nameuse");
                if (temp.id == 173) {
                    temp.beri = 10000;
                    temp.ruby = 0;
                } else if (temp.id == 174) {
                    temp.beri = 10000;
                    temp.ruby = 0;
                }
                // Set giá pháo hoa = 150 ruby
                if (temp.id == 359 || temp.id == 361
                    || (temp.name != null && (temp.name.toLowerCase().contains("pháo hoa")
                        || temp.name.toLowerCase().contains("phao hoa")))) {
                    temp.ruby = 150;
                    temp.beri = 0;
                }
                ItemTemplate4.ENTRYS.add(temp);
            }
            rs.close();
            // load item temp 7
            query = "SELECT * FROM `item7`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate7 temp = new ItemTemplate7();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.type = rs.getByte("type");
                temp.icon = rs.getByte("icon");
                temp.price = rs.getInt("price");
                temp.priceruby = rs.getShort("priceruby");
                temp.istrade = rs.getByte("istrade");
                ItemTemplate7.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load item ok");
            // load skill temp
            Skill_Template.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `skill` ORDER BY `id_index`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                // int id = rs.getInt("id");
                Skill_Template temp_add = new Skill_Template(rs.getShort("id_index"),
                        rs.getShort("id_2"), rs.getShort("icon"), rs.getByte("typeSkill"),
                        rs.getByte("typeBuff"), rs.getString("name"), rs.getShort("typeEffSkill"),
                        rs.getShort("range"));
                temp_add.getData(rs.getByte("nTarget"), rs.getShort("rangeLan"),
                        rs.getInt("damage"), rs.getShort("manaLost"), rs.getInt("timeDelay"),
                        rs.getByte("nKick"), rs.getString("info"), rs.getByte("Lv_RQ"),
                        rs.getByte("typeDevil"));
                temp_add.op = new ArrayList<>();
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("option"));
                for (int j = 0; j < js.size(); j++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(j).toString());
                    temp_add.op.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Integer.parseInt(js2.get(1).toString())));
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("EffSpec"));
                temp_add.idEffSpec = Byte.parseByte(js.get(0).toString());
                temp_add.perEffSpec = Short.parseShort(js.get(1).toString());
                temp_add.timeEffSpec = Short.parseShort(js.get(2).toString());
                js.clear();
                Skill_Template.ENTRYS.add(temp_add);
            }
            rs.close();
            System.out.println("load skill ok");
            // load item option temp
            ItemOptionTemplate.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `itemoption`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemOptionTemplate temp = new ItemOptionTemplate();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.color = rs.getByte("color");
                temp.percent = rs.getByte("percent");
                ItemOptionTemplate.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load item op temp ok");
            // load item fashion info
            ItemFashion.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `fashiontemplate`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                int id = rs.getInt("id");
                short icon = rs.getShort("icon");
                String name = rs.getString("name");
                String info = rs.getString("info");
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("mwear"));
                short[] wear = new short[js.size()];
                for (int i = 0; i < wear.length; i++) {
                    wear[i] = Short.parseShort(js.get(i).toString());
                }
                js.clear();
                js = (JSONArray) JSONValue.parse(rs.getString("op"));
                List<Option> op = new ArrayList<>();
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                    op.add(new Option(Byte.parseByte(js2.get(0).toString()),
                            Integer.parseInt(js2.get(1).toString())));
                }
                ItemFashion.ENTRYS
                        .add(new ItemFashion((short) id, icon, name, info, wear, op, rs.getInt("price")));
            }
            rs.close();
            System.out.println("load fashion temp ok");
            // load boss
            Boss.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `boss`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                int id = rs.getInt("id");
                int mob_id = rs.getInt("mob_id");
                String site = rs.getString("site");
                int hp = rs.getInt("hp");
                String skill = rs.getString("skill");
                String buff = rs.getString("buff");
                int level = rs.getInt("level");
                int thegioi = 2; // mặc định là boss làng
                try {
                    thegioi = rs.getInt("thegioi");
                } catch (Exception e) {
                    if (mob_id >= 135 && mob_id <= 140) {
                        thegioi = 1;
                    }
                }
                JSONArray js = (JSONArray) JSONValue.parse(site);
                short temp_x = Short.parseShort(js.get(1).toString());
                short temp_y = Short.parseShort(js.get(2).toString());
                short mapId = Short.parseShort(js.get(0).toString());
                Map[] map = Map.get_map_by_id(mapId);
                js.clear();
                if (map == null) {
                    System.err.println("[WARN] load_database: Boss id=" + id + " references unknown map_id=" + mapId + ", skipping.");
                    continue;
                }
                if (mob_id < 0 || mob_id >= MobTemplate.ENTRYS.size()) {
                    System.err.println("[WARN] load_database: Boss id=" + id + " references unknown mob_id=" + mob_id + ", skipping.");
                    continue;
                }
                int targetZoneIdx = map.length > 1 ? 1 : 0;
                for (int i = 0; i < map.length; i++) {
                    if (i != targetZoneIdx) {
                        continue;
                    }
                    Boss boss_temp = new Boss();
                    boss_temp.id = id;
                    boss_temp.thegioi = thegioi;
                    boss_temp.mob = new Mob();
                    boss_temp.mob.mob_template = MobTemplate.ENTRYS.get(mob_id);
                    boss_temp.mob.x = temp_x;
                    boss_temp.mob.y = temp_y;
                    boss_temp.mob.hp_max = hp;
                    boss_temp.hp_max_origin = hp;
                    boss_temp.mob.hp = 0;
                    boss_temp.mob.level = level;
                    boss_temp.mob.isdie = true;
                    boss_temp.mob.id_target = -1;
                    boss_temp.mob.index = this.index_mob;
                    boss_temp.index_mob_save = this.index_mob;
                    this.index_mob += 10;
                    boss_temp.mob.boss_info = boss_temp;
                    boss_temp.mob.map = map[i];
                    boss_temp.mapOrigin = map[i];
                    boss_temp.xOrigin = temp_x;
                    boss_temp.yOrigin = temp_y;
                    Mob.ENTRYS.put(boss_temp.mob.index, boss_temp.mob);
                    for (int j = 0; j < 10; j++) { // them 10slot cho 10 bac
                        Mob.ENTRYS.put((boss_temp.mob.index + j), boss_temp.mob);
                    }
                    //
                    js = (JSONArray) JSONValue.parse(skill);
                    boss_temp.skill = new short[js.size()];
                    for (int i2 = 0; i2 < boss_temp.skill.length; i2++) {
                        boss_temp.skill[i2] = Short.parseShort(js.get(i2).toString());
                    }
                    if (mob_id == 174 || id == 28 || (boss_temp.mob.mob_template != null && (boss_temp.mob.mob_template.mob_id == 174 || (boss_temp.mob.mob_template.name != null && boss_temp.mob.mob_template.name.toLowerCase().contains("saturn"))))) {
                        boss_temp.skill = new short[] { 195, 196, 197 };
                        if (boss_temp.mob.mob_template != null) {
                            boss_temp.mob.mob_template.skill = new short[] { 195, 196, 197 };
                        }
                        boss_temp.mob.final_dame = 180000;
                        boss_temp.mob.phong_thu = 50000;
                        boss_temp.mob.mien_thuong = 70;
                        boss_temp.mob.max_dame_per_hit = 2000000;
                        boss_temp.mob.ne_don = 10;
                        boss_temp.mob.phan_dame = 5;
                    }
                    if (mob_id == 172 || id == 11 || (boss_temp.mob.mob_template != null && (boss_temp.mob.mob_template.mob_id == 172 || (boss_temp.mob.mob_template.name != null && (boss_temp.mob.mob_template.name.toLowerCase().contains("râu trắng") || boss_temp.mob.mob_template.name.toLowerCase().contains("rau trang")))))) {
                        boss_temp.skill = new short[] { 210, 211, 243, 244 };
                        if (boss_temp.mob.mob_template != null) {
                            boss_temp.mob.mob_template.skill = new short[] { 210, 211, 243, 244 };
                            boss_temp.mob.mob_template.hOne = 120;
                            boss_temp.mob.mob_template.hp_max = 2000000000;
                        }
                        boss_temp.mob.hp_max = 2000000000;
                        boss_temp.hp_max_origin = 2000000000;
                        boss_temp.mob.mp = 1000000000;
                        boss_temp.mob.mp_max = 1000000000;
                        boss_temp.mob.final_dame = 250000;
                        boss_temp.mob.phong_thu = 60000;
                        boss_temp.mob.mien_thuong = 70;
                        boss_temp.mob.giam_mien_thuong = 400;
                        boss_temp.mob.max_dame_per_hit = 5000000;
                        boss_temp.mob.ne_don = 15;
                        boss_temp.mob.phan_dame = 10;
                    }
                    if (boss_temp.thegioi == 1 || (boss_temp.mob.mob_template != null && Boss.isWorldBoss(boss_temp.mob.mob_template.mob_id))) {
                        boss_temp.mob.setupTheGioi1Stats();
                    }
                    boss_temp.time_atk = new long[boss_temp.skill.length];
                    boss_temp.TopDame = new ArrayList<>();
                    boss_temp.levelBoss = 1;
                    js.clear();
                    js = (JSONArray) JSONValue.parse(buff);
                    boss_temp.buff = new ArrayList<>();
                    for (int i2 = 0; i2 < js.size(); i2++) {
                        JSONArray js2 = (JSONArray) js.get(i2);
                        boss_temp.buff.add(new Option(Byte.parseByte(js2.get(0).toString()),
                                Integer.parseInt(js2.get(1).toString())));
                    }
                    Boss.ENTRYS.add(boss_temp);
                }
            }
            //
            rs.close();
            System.out.println("load boss ok, mob size : " + (this.index_mob - 1));
            // load hair
            query = "SELECT * FROM `itemhair`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemHair.ENTRYS.add(ItemHair.read_json_it_hair(rs));
            }
            rs.close();
            System.out.println("load item hair ok");
            ItemTemplate8.ENTRYS = new ArrayList<>();
            query = "SELECT * FROM `item8`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                ItemTemplate8 temp = new ItemTemplate8();
                temp.id = rs.getShort("id");
                temp.name = rs.getString("name");
                temp.icon = rs.getShort("icon");
                temp.info = rs.getString("info");
                temp.beri = rs.getInt("price");
                temp.ruby = rs.getShort("priceruby");
                temp.istrade = rs.getByte("istrade");
                temp.type = rs.getByte("hpmpother");
                temp.timedelay = rs.getShort("timedelay");
                temp.value = rs.getShort("value");
                temp.timeactive = rs.getShort("timeactive");
                temp.nameuse = rs.getString("nameuse");
                ItemTemplate8.ENTRYS.add(temp);
            }
            rs.close();
            System.out.println("load item clan ok");
            Clan.ENTRY = new ArrayList<>();
            Clan.BXH = new ArrayList<>();
            query = "SELECT * FROM `clan`;";
            Set<String> name_check = new HashSet<>();
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Clan clan = new Clan();
                clan.id = rs.getShort("id");
                clan.name = rs.getString("name");
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("info"));
                clan.icon = Short.parseShort(js.get(0).toString());
                clan.level = Short.parseShort(js.get(1).toString());
                clan.xp = Integer.parseInt(js.get(2).toString());
                clan.maxAttri = Short.parseShort(js.get(3).toString());
                clan.pointAttri = Short.parseShort(js.get(4).toString());
                clan.trungsinh = Byte.parseByte(js.get(5).toString());
                switch (clan.trungsinh) {
                    case 1: {
                        clan.maxAttri = 25;
                        break;
                    }
                    case 2: {
                        clan.maxAttri = 30;
                        break;
                    }
                    case 3: {
                        clan.maxAttri = 35;
                        break;
                    }
                    case 4: {
                        clan.maxAttri = 40;
                        break;
                    }
                    case 5: {
                        clan.maxAttri = 45;
                        break;
                    }
                    case 6: {
                        clan.maxAttri = 50;
                        break;
                    }
                    default: { // 0
                        clan.maxAttri = 20;
                        break;
                    }
                }
                clan.countAction = Integer.parseInt(js.get(6).toString());
                clan.ruby = Integer.parseInt(js.get(7).toString());
                clan.beri = Integer.parseInt(js.get(8).toString());
                clan.allowRequest = Byte.parseByte(js.get(9).toString());
                clan.opAttri = new short[] { 0, 0, 0, 0, 0 };
                JSONArray js2 = (JSONArray) js.get(10);
                for (int i = 0; i < clan.opAttri.length; i++) {
                    clan.opAttri[i] = Short.parseShort(js2.get(i).toString());
                }
                clan.thongbao = rs.getString("notice");
                js.clear();
                clan.chat = new ArrayList<>();
                clan.mem_request = new ArrayList<>();
                clan.members = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("member"));
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    Clan_member mem = new Clan_member();
                    mem.id = (short) i;
                    mem.name = js_in.get(0).toString();
                    mem.level = Short.parseShort(js_in.get(1).toString());
                    mem.levelInclan = Byte.parseByte(js_in.get(2).toString());
                    mem.donate = Short.parseShort(js_in.get(3).toString());
                    mem.gopRuby = Short.parseShort(js_in.get(4).toString());
                    mem.numquest = Short.parseShort(js_in.get(5).toString());
                    mem.conghien = Integer.parseInt(js_in.get(6).toString());
                    mem.head = Short.parseShort(js_in.get(7).toString());
                    mem.hair = Short.parseShort(js_in.get(8).toString());
                    mem.hat = Short.parseShort(js_in.get(9).toString());
                    mem.clazz = Byte.parseByte(js_in.get(10).toString());
                    if (js_in.size() > 11) {
                        mem.checkGiftSend = Byte.parseByte(js_in.get(11).toString());
                        mem.checkGiftReceive = Byte.parseByte(js_in.get(12).toString());
                    }
                    //
                    boolean add = true;
                    int num_clazz = 0;
                    for (int j = 0; j < clan.members.size(); j++) {
                        if (clan.members.get(j).clazz == mem.clazz) {
                            num_clazz++;
                        }
                    }
                    if (num_clazz >= 4) {
                        System.out.println("err load clan >=4 " + clan.name + " " + mem.name);
                        add = false;
                    }
                    if (add && !name_check.contains(mem.name)) {
                        name_check.add(mem.name);
                    } else {
                        add = false;
                    }
                    if (add) {
                        clan.members.add(mem);
                    }
                }
                js.clear();
                clan.list_it = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("item"));
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    ItemBag47 itemBag47 = new ItemBag47();
                    itemBag47.category = 4;
                    itemBag47.id = Short.parseShort(js_in.get(0).toString());
                    itemBag47.quant = Short.parseShort(js_in.get(1).toString());
                    clan.list_it.add(itemBag47);
                }
                clan.buff = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(rs.getString("buff"));
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js_in = (JSONArray) js.get(i);
                    clan.buff.add(new EffTemplate(Byte.parseByte(js_in.get(0).toString()),
                            Integer.parseInt(js_in.get(1).toString()),
                            Long.parseLong(js_in.get(2).toString())));
                }
                Clan.add_new_clan(clan);
            }
            rs.close();
            System.out.println("load clan ok");
            // load quest
            query = "SELECT * FROM `quests`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Quest.add(rs);
            }
            Quest.add_finish_quest();
            rs.close();
            System.out.println("load quest ok");
            // load pet template
            query = "SELECT * FROM `pet_template`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Pet tempPet = new Pet();
                tempPet.id = rs.getShort("id");
                tempPet.name = rs.getString("name");
                tempPet.type = rs.getByte("type");
                tempPet.icon = rs.getShort("icon");
                tempPet.frame = rs.getShort("frame");
                try {
                    tempPet.isShow = rs.getByte("show");
                } catch (java.sql.SQLException e) {
                    tempPet.isShow = 0;
                }
                String opStr = rs.getString("op");
                if (opStr != null && !opStr.isEmpty()) {
                    JSONArray js = (JSONArray) JSONValue.parse(opStr);
                    for (int i = 0; i < js.size(); i++) {
                        JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                        int optId = Integer.parseInt(js2.get(0).toString());
                        int optVal = Integer.parseInt(js2.get(1).toString());
                        tempPet.op.add(new template.Option(optId, optVal));
                    }
                }
                Pet.ENTRY.add(tempPet);
            }
            rs.close();
            System.out.println("load pet ok. Total pet templates loaded: " + Pet.ENTRY.size());
            boolean hasNewPets = false;
            for (Pet p : Pet.ENTRY) {
                if (p.id >= 70) {
                    hasNewPets = true;
                    break;
                }
            }
            if (!hasNewPets) {
                System.err.println("==========================================================================");
                System.err.println("[PET WARNING] Database `pet_template` only has " + Pet.ENTRY.size() + " templates (MISSING PETS 70..112)!");
                System.err.println("[PET WARNING] Please import `database/updatepet2708.sql` to MySQL database `full_db_htth`!");
                System.err.println("==========================================================================");
            } else {
                System.out.println("[PET INFO] New pets 70..112 loaded successfully into memory.");
            }
            query = "SELECT * FROM `market`;";
            rs = ps.executeQuery(query);
            while (rs.next()) {
                Market tempMarket = new Market();
                tempMarket.type = rs.getByte("id");
                JSONObject jsob = (JSONObject) JSONValue.parse(rs.getString("data"));
                tempMarket.item3 = new ArrayList<>();
                JSONArray js = (JSONArray) JSONValue.parse(jsob.get("item3").toString());
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) js.get(i);
                    ItemMarket itemMarket = new ItemMarket();
                    itemMarket.load_json(js2);
                    if (itemMarket.index != -1) {
                        tempMarket.item3.add(itemMarket);
                    }
                }
                js.clear();
                tempMarket.item47 = new ArrayList<>();
                js = (JSONArray) JSONValue.parse(jsob.get("item47").toString());
                for (int i = 0; i < js.size(); i++) {
                    JSONArray js2 = (JSONArray) js.get(i);
                    PotionMarket potionMarket = new PotionMarket();
                    potionMarket.load_json(js2);
                    if (potionMarket.index != -1) {
                        tempMarket.item47.add(potionMarket);
                    }
                }
                js.clear();
                Market.ENTRY.add(tempMarket);
            }
            // rs.close();
            System.out.println("load market ok");
            // load danhhieu
            query = "SELECT * FROM `danhhieu`;";
            rs = ps.executeQuery(query);
            template.DanhHieuTemplate.ENTRYS.clear();
            activities.DanhHieu.ENY.clear();
            while (rs.next()) {
                template.DanhHieuTemplate temp = new template.DanhHieuTemplate();
                temp.id = rs.getInt("id");
                temp.name = rs.getString("name");
                temp.idicon = rs.getInt("idicon");
                temp.nframe = rs.getInt("nframe");
                temp.op = new ArrayList<>();
                String opStr = rs.getString("op");
                if (opStr != null && !opStr.isEmpty()) {
                    JSONArray js_ar = (JSONArray) JSONValue.parse(opStr);
                    if (js_ar != null) {
                        for (int i = 0; i < js_ar.size(); i++) {
                            JSONArray js_in = (JSONArray) js_ar.get(i);
                            temp.op.add(new template.Option(Byte.parseByte(js_in.get(0).toString()),
                                    Integer.parseInt(js_in.get(1).toString())));
                        }
                    }
                }
                template.DanhHieuTemplate.ENTRYS.add(temp);
                // load vào DanhHieu.ENY (protocol Message -102)
                activities.DanhHieu dh = new activities.DanhHieu();
                dh.id = temp.id;
                dh.Name = temp.name;
                dh.idicon = temp.idicon;
                dh.nframe = temp.nframe;
                dh.coint = rs.getInt("vnd");
                if (temp.op != null) {
                    for (template.Option op : temp.op) {
                        dh.op.add(op);
                    }
                }
                activities.DanhHieu.ENY.add(dh);
            }
            rs.close();
            System.out.println("load danhhieu template ok, size: " + template.DanhHieuTemplate.ENTRYS.size());
            System.out.println("load DanhHieu.ENY ok, size: " + activities.DanhHieu.ENY.size());
        } catch (SQLException e) {
            e.printStackTrace();
            System.exit(0);
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (ps != null) {
                    ps.close();
                }
                if (conn != null) {
                    conn.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    private void load_config() throws IOException {
        final byte[] ab = Util.loadfile("htth.conf");
        if (ab == null) {
            System.out.println("Config file not found!");
            System.exit(0);
        }
        final String data = new String(ab);
        final HashMap<String, String> configMap = new HashMap<String, String>();
        final StringBuilder sbd = new StringBuilder();
        boolean bo = false;
        for (int i = 0; i <= data.length(); ++i) {
            final char es;
            if (i == data.length() || (es = data.charAt(i)) == '\n') {
                bo = false;
                final String sbf = sbd.toString().trim();
                if (sbf != null && !sbf.equals("") && sbf.charAt(0) != '#') {
                    final int j = sbf.indexOf(58);
                    if (j > 0) {
                        final String key = sbf.substring(0, j).trim();
                        final String value = sbf.substring(j + 1).trim();
                        configMap.put(key, value);
                        System.out.println("config: " + key + ": " + value);
                    }
                }
                sbd.setLength(0);
            } else {
                if (es == '#') {
                    bo = true;
                }
                if (!bo) {
                    sbd.append(es);
                }
            }
        }
        if (configMap.containsKey("port")) {
            this.server_port = Integer.parseInt(configMap.get("port"));
        } else {
            this.server_port = 2239;
        }
        if (configMap.containsKey("debug")) {
            this.debug = Boolean.parseBoolean(configMap.get("debug"));
        } else {
            this.debug = false;
        }
        if (configMap.containsKey("mysql-host")) {
            this.mysql_host = configMap.get("mysql-host");
        } else {
            this.mysql_host = "127.0.0.1";
        }
        if (configMap.containsKey("mysql-user")) {
            this.mysql_user = configMap.get("mysql-user");
        } else {
            this.mysql_user = "root";
        }
        if (configMap.containsKey("mysql-password")) {
            this.mysql_pass = configMap.get("mysql-password");
        } else {
            this.mysql_pass = "12345678";
        }
        if (configMap.containsKey("mysql-database")) {
            this.mysql_database = configMap.get("mysql-database");
        } else {
            this.mysql_database = "database";
        }
        if (configMap.containsKey("exp")) {
            this.exp = Integer.parseInt(configMap.get("exp"));
        } else {
            this.exp = 1;
        }
        if (configMap.containsKey("serveradmin")) {
            this.server_admin = Boolean.parseBoolean(configMap.get("serveradmin"));
        } else {
            this.server_admin = false;
        }
        if (configMap.containsKey("max-ip-connection")) {
            this.max_ip_connection = Integer.parseInt(configMap.get("max-ip-connection"));
        } else {
            this.max_ip_connection = 10;
        }
        if (configMap.containsKey("max-register-ip-day")) {
            this.max_register_ip_day = Integer.parseInt(configMap.get("max-register-ip-day"));
        } else {
            this.max_register_ip_day = 5;
        }
        if (configMap.containsKey("max-ccu")) {
            this.max_ccu = Integer.parseInt(configMap.get("max-ccu"));
        } else {
            this.max_ccu = 500;
        }
        if (configMap.containsKey("ws-port")) {
            this.ws_port = Integer.parseInt(configMap.get("ws-port"));
        } else {
            this.ws_port = this.server_port + 1;
        }
        // Event Trung Thu config
        if (configMap.containsKey("event-trung-thu")) {
            event.EventTrungThu.setEvent(Boolean.parseBoolean(configMap.get("event-trung-thu")));
        }
        // Event Tet config
        if (configMap.containsKey("event-tet")) {
            event.EventTet.setEvent(Boolean.parseBoolean(configMap.get("event-tet")));
        }
        // Event 20/11 config
        if (configMap.containsKey("event-2011")) {
            event.Event2011.setEvent(Boolean.parseBoolean(configMap.get("event-2011")));
        }
        // Event Noel config
        if (configMap.containsKey("event-noel")) {
            event.EventNoel.setEvent(Boolean.parseBoolean(configMap.get("event-noel")));
        }
        // Auto Maintenance config
        if (configMap.containsKey("auto-maintenance")) {
            MaintenanceManager.autoMaintenance = Boolean.parseBoolean(configMap.get("auto-maintenance"));
        } else {
            MaintenanceManager.autoMaintenance = true;
        }
        if (configMap.containsKey("auto-maintenance-hour")) {
            MaintenanceManager.maintenanceHour = Integer.parseInt(configMap.get("auto-maintenance-hour"));
        } else {
            MaintenanceManager.maintenanceHour = 2;
        }
        if (configMap.containsKey("auto-maintenance-minute")) {
            MaintenanceManager.maintenanceMinute = Integer.parseInt(configMap.get("auto-maintenance-minute"));
        } else {
            MaintenanceManager.maintenanceMinute = 0;
        }
        // Server Event Rate Config (x2 EXP, x2 EXP Skill)
        if (configMap.containsKey("rate-exp")) {
            RATE_EXP = Integer.parseInt(configMap.get("rate-exp"));
        } else {
            RATE_EXP = 1;
        }
        if (configMap.containsKey("rate-exp-skill")) {
            RATE_EXP_SKILL = Integer.parseInt(configMap.get("rate-exp-skill"));
        } else {
            RATE_EXP_SKILL = 1;
        }
        // Server Event Deposit Multiplier Config (x1, x2, x3 Nạp)
        if (configMap.containsKey("rate-nap")) {
            try {
                int confNap = Integer.parseInt(configMap.get("rate-nap").trim());
                if (confNap >= 1) {
                    activities.Bank.saveDepositMultiplierToDb(confNap);
                }
            } catch (Exception e) {
                activities.Bank.loadDepositMultiplierFromDb();
            }
        } else {
            activities.Bank.loadDepositMultiplierFromDb();
        }
        // Notice Giftcode Login Config
        if (configMap.containsKey("notice-giftcode")) {
            this.notice_giftcode = configMap.get("notice-giftcode").replace("\\n", "\n");
        }
    }

    public void close() {
        stop_service();
    }

    public void chatKTG(Player p, String text) throws IOException {
        if (p.conn.user.equals("admin") || p.time_chat_ktg < System.currentTimeMillis()) {
            p.time_chat_ktg = System.currentTimeMillis() + 30_000L;
            chatKTG(1, text, 0);
            Service.send_box_ThongBao_OK(p, "Chat KTG thành công với nội dung: " + text);
        } else {
            Service.send_box_ThongBao_OK(p,
                    "Chờ " + (p.time_chat_ktg - System.currentTimeMillis()) / 1000L + "s");
        }
    }

    public void chatKTG(int type, String text, int color) throws IOException {
        chatKTG(type, text, color, (short) -1);
    }

    public void chatKTG(int type, String text, int color, short iconClan) throws IOException {
        Message m = new Message(-31);
        m.writer().writeByte(type);
        m.writer().writeUTF(text);
        m.writer().writeByte(color);
        m.writer().writeShort(iconClan);
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                if (map != null && map.players != null) {
                    for (int i = 0; i < map.players.size(); i++) {
                        try {
                            if (i < map.players.size()) {
                                Player p0 = map.players.get(i);
                                if (p0 != null && p0.conn != null) {
                                    p0.conn.addmsg(m);
                                }
                            }
                        } catch (Exception ignored) {
                        }
                    }
                }
            }
        }
        List<Map> mapplus = Map.get_map_plus();
        if (mapplus != null) {
            for (int i = 0; i < mapplus.size(); i++) {
                Map map = mapplus.get(i);
                if (map != null && map.players != null) {
                    for (int i12 = 0; i12 < map.players.size(); i12++) {
                        try {
                            if (i12 < map.players.size()) {
                                Player p0 = map.players.get(i12);
                                if (p0 != null && p0.conn != null) {
                                    p0.conn.addmsg(m);
                                }
                            }
                        } catch (Exception ignored) {
                        }
                    }
                }
            }
        }
        m.cleanup();
    }

    public void chatKTGClan(Player p, String text) throws IOException {
        if (p == null) {
            return;
        }
        if (p.clan == null) {
            Service.send_box_ThongBao_OK(p, "Bạn chưa gia nhập băng hải tặc!");
            return;
        }
        boolean isLeaderOrVice = false;
        if (p.clan.members != null && !p.clan.members.isEmpty()) {
            if (p.clan.members.get(0).name.equals(p.name)) {
                isLeaderOrVice = true;
            } else {
                for (int i = 0; i < p.clan.members.size(); i++) {
                    Clan_member mem = p.clan.members.get(i);
                    if (mem != null && mem.name.equals(p.name)
                            && (mem.levelInclan == 0 || mem.levelInclan == 1)) {
                        isLeaderOrVice = true;
                        break;
                    }
                }
            }
        }
        if (!isLeaderOrVice && (p.conn == null || !p.conn.user.equals("admin"))) {
            Service.send_box_ThongBao_OK(p, "Chỉ Thuyền trưởng hoặc Thuyền phó mới có thể chat KTG băng!");
            return;
        }
        if (p.conn != null && !p.conn.user.equals("admin") && p.time_chat_ktg > System.currentTimeMillis()) {
            Service.send_box_ThongBao_OK(p,
                    "Chờ " + ((p.time_chat_ktg - System.currentTimeMillis()) / 1000L) + "s để tiếp tục chat KTG");
            return;
        }

        // Ưu tiên trừ 15 ruby băng, nếu không đủ thì cho phép trừ 15 ruby cá nhân (admin miễn phí)
        boolean paid = false;
        if (p.conn != null && p.conn.user.equals("admin")) {
            paid = true;
        } else if (p.clan.get_ngoc() >= 15) {
            p.clan.update_ruby(-15);
            Clan.update();
            for (int i = 0; i < p.clan.members.size(); i++) {
                Player p0 = Map.get_player_by_name_allmap(p.clan.members.get(i).name);
                if (p0 != null) {
                    Clan.send_money(p0, false);
                }
            }
            paid = true;
        } else if (p.get_ngoc() >= 15) {
            p.update_ngoc(-15);
            p.update_money();
            paid = true;
        }

        if (!paid) {
            Service.send_box_ThongBao_OK(p, "Cần 15 ruby băng (hoặc 15 ruby cá nhân) để chat KTG băng!");
            return;
        }

        p.time_chat_ktg = System.currentTimeMillis() + 10_000L;
        short iconClan = (p.clan.icon >= 0) ? p.clan.icon : (short) -1;
        String content = p.clan.name + ": " + text;
        chatKTG(1, content, 0, iconClan);

        // Ghi lại tin nhắn vào bảng chat clan
        try {
            short idMem = 0;
            if (p.clan.members != null) {
                for (Clan_member mem : p.clan.members) {
                    if (mem != null && mem.name.equals(p.name)) {
                        idMem = mem.id;
                        break;
                    }
                }
            }
            Clan_chat cChat = new Clan_chat();
            cChat.idMem = idMem;
            cChat.name = p.name;
            cChat.str = "[KTG] " + text;
            cChat.time = System.currentTimeMillis();
            cChat.typeChat = -3;
            p.clan.add_chat(cChat);
            p.clan.send_chat(cChat, null);
        } catch (Exception ignored) {
        }

        Service.send_box_ThongBao_OK(p, "Chat KTG băng thành công với nội dung: " + text);
    }

    public TaiXiu TaiXiu() {
        return tx;
    }

    public static void reload_parts(short... partIds) throws SQLException {
        String query = "SELECT * FROM `parts`;";
        List<Part> list = new ArrayList<>();
        Connection conn = database.SQL.gI().getCon();
        Statement ps = conn.createStatement();
        ResultSet rs = ps.executeQuery(query);
        while (rs.next()) {
            byte type = rs.getByte("type");
            JSONArray js = (JSONArray) JSONValue.parse(rs.getString("data"));
            Part part = new Part(type);
            part.id = rs.getShort("id");
            if (part.pi == null || part.pi.length < js.size()) {
                part.pi = new PartImg[js.size()];
            }
            for (int i = 0; i < js.size(); i++) {
                JSONArray js_in = (JSONArray) js.get(i);
                part.pi[i] = new PartImg();
                part.pi[i].id = Short.parseShort(js_in.get(0).toString());
                part.pi[i].dx = Byte.parseByte(js_in.get(1).toString());
                part.pi[i].dy = Byte.parseByte(js_in.get(2).toString());
            }
            if (part.id == 729) {
                for (int i = 0; i < part.pi.length; i++) {
                    part.pi[i].dy = -2;
                }
            }
            list.add(part);
        }
        rs.close();
        ps.close();
        conn.close();
        Part.ENTRY = list;

        // Broadcast updated parts to all online players
        List<Part> sendList = new ArrayList<>();
        if (partIds != null && partIds.length > 0) {
            for (short id : partIds) {
                Part p = Part.get_part(id);
                if (p != null) {
                    sendList.add(p);
                }
            }
        } else {
            sendList = list;
        }

        synchronized (SessionManager.CLIENT_ENTRYS) {
            for (int i = 0; i < SessionManager.CLIENT_ENTRYS.size(); i++) {
                io.Session sess = SessionManager.CLIENT_ENTRYS.get(i);
                if (sess != null && sess.p != null) {
                    try {
                        for (Part part : sendList) {
                            Message m = new Message(-82);
                            m.writer().writeShort(part.id);
                            m.writer().writeByte(part.type);
                            for (int j = 0; j < part.pi.length; j++) {
                                m.writer().writeShort(part.pi[j].id);
                                m.writer().writeByte(part.pi[j].dx);
                                m.writer().writeByte(part.pi[j].dy);
                            }
                            sess.addmsg(m);
                            m.cleanup();
                        }
                        sess.p.update_info_to_all();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        }
    }

    public static void reload_fashion() throws SQLException {
        ItemFashion.ENTRYS = new ArrayList<>();
        Connection conn = database.SQL.gI().getCon();
        Statement ps = conn.createStatement();
        String query = "SELECT * FROM `fashiontemplate`;";
        ResultSet rs = ps.executeQuery(query);
        while (rs.next()) {
            int id = rs.getInt("id");
            short icon = rs.getShort("icon");
            String name = rs.getString("name");
            String info = rs.getString("info");
            JSONArray js = (JSONArray) JSONValue.parse(rs.getString("mwear"));
            short[] wear = new short[js.size()];
            for (int i = 0; i < wear.length; i++) {
                wear[i] = Short.parseShort(js.get(i).toString());
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("op"));
            List<Option> op = new ArrayList<>();
            for (int i = 0; i < js.size(); i++) {
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i).toString());
                op.add(new Option(Byte.parseByte(js2.get(0).toString()),
                        Integer.parseInt(js2.get(1).toString())));
            }
            ItemFashion.ENTRYS
                    .add(new ItemFashion((short) id, icon, name, info, wear, op, rs.getInt("price")));
        }
        rs.close();
        ps.close();
        conn.close();
    }

    public static int reload_hair() throws SQLException {
        String query = "SELECT * FROM `itemhair`;";
        Connection conn = database.SQL.gI().getCon();
        Statement ps = conn.createStatement();
        ResultSet rs = ps.executeQuery(query);
        List<ItemHair> list = new ArrayList<>();
        while (rs.next()) {
            list.add(ItemHair.read_json_it_hair(rs));
        }
        rs.close();
        ps.close();
        conn.close();
        // Preserve type 108 (head items loaded from msg/head)
        for (int i = 0; i < ItemHair.ENTRYS.size(); i++) {
            ItemHair old = ItemHair.ENTRYS.get(i);
            if (old.type == 108) {
                list.add(old);
            }
        }
        ItemHair.ENTRYS = list;
        return list.size();
    }

    public static void reload_mobs() throws Exception {
        String query = "SELECT * FROM `mobs`;";
        List<MobTemplate> list = new ArrayList<>();
        Connection conn = database.SQL.gI().getCon();
        Statement ps = conn.createStatement();
        ResultSet rs = ps.executeQuery(query);
        while (rs.next()) {
            MobTemplate temp = new MobTemplate();
            temp.mob_id = Short.parseShort(rs.getString("id"));
            temp.name = rs.getString("name");
            temp.level = Short.parseShort(rs.getString("level"));
            temp.hp_max = Integer.parseInt(rs.getString("hp"));
            temp.hOne = Short.parseShort(rs.getString("hOne"));
            temp.typemove = Byte.parseByte(rs.getString("typemove"));
            temp.ishuman = Byte.parseByte(rs.getString("ishuman"));
            temp.typemonster = Byte.parseByte(rs.getString("typemonster"));
            JSONArray js = (JSONArray) JSONValue.parse(rs.getString("idicon"));
            if (temp.ishuman == 0) {
                temp.icon = Short.parseShort(js.get(1).toString());
            } else if (temp.ishuman == 1) {
                temp.head = Short.parseShort(js.get(1).toString());
                temp.hair = Short.parseShort(js.get(2).toString());
                JSONArray js2 = (JSONArray) JSONValue.parse(js.get(3).toString());
                temp.wearing = new short[js2.size()];
                for (int i = 0; i < temp.wearing.length; i++) {
                    temp.wearing[i] = Short.parseShort(js2.get(i).toString());
                }
            }
            js.clear();
            js = (JSONArray) JSONValue.parse(rs.getString("skill"));
            temp.skill = new short[js.size()];
            for (int i = 0; i < temp.skill.length; i++) {
                temp.skill[i] = Short.parseShort(js.get(i).toString());
            }
            js.clear();
            if (temp.mob_id == 174 || (temp.name != null && temp.name.toLowerCase().contains("saturn"))) {
                temp.skill = new short[] { 195, 196, 197 };
            }
            if (temp.mob_id == 172 || (temp.name != null && (temp.name.toLowerCase().contains("râu trắng") || temp.name.toLowerCase().contains("rau trang")))) {
                temp.hOne = 120;
                temp.hp_max = 2000000000;
                temp.skill = new short[] { 210, 211, 243, 244 };
            }
            list.add(temp);
        }
        rs.close();
        ps.close();
        conn.close();
        MobTemplate.ENTRYS = list;
        DataTemplate.VerdataMon++;

        // Send updated monster template packet to all online players
        Message m22 = new Message(-7);
        m22.writer().writeByte(15);
        m22.writer().writeShort(MobTemplate.ENTRYS.size());
        for (int i = 0; i < MobTemplate.ENTRYS.size(); i++) {
            MobTemplate temp = MobTemplate.ENTRYS.get(i);
            m22.writer().writeShort(temp.mob_id);
            m22.writer().writeUTF(temp.name);
            m22.writer().writeShort(temp.level);
            m22.writer().writeShort(temp.hOne);
            m22.writer().writeInt(temp.hp_max);
            m22.writer().writeByte(temp.typemove);
            m22.writer().writeByte(temp.ishuman);
            m22.writer().writeByte(temp.typemonster);
            if (temp.ishuman == 1) {
                m22.writer().writeShort(temp.head);
                m22.writer().writeShort(temp.hair);
                m22.writer().writeByte(temp.wearing.length);
                for (int j = 0; j < temp.wearing.length; j++) {
                    if (temp.wearing[j] != -1) {
                        m22.writer().writeByte(1);
                        m22.writer().writeShort(temp.wearing[j]);
                    } else {
                        m22.writer().writeByte(-1);
                    }
                }
            } else {
                m22.writer().writeShort(temp.icon);
            }
        }
        m22.writer().writeShort(DataTemplate.VerdataMon);
        for (Map[] mapall : Map.ENTRYS) {
            for (Map map : mapall) {
                for (int i = 0; i < map.players.size(); i++) {
                    Player p0 = map.players.get(i);
                    if (p0.conn != null) {
                        p0.conn.addmsg(m22);
                    }
                }
            }
        }
        m22.cleanup();
    }
}
