package template;
/**
 *
 * @author Truongbk
 */
public class DataTemplate {
        public final static int VerdataMon = 125;
        public final static int VerdataPotion = 314;
        public final static int VerdataAttri = 791;
        public final static int VerdataNameMap = 369;
        public final static int VerdataNamePotionquest = 258;
        public final static int VerdataImageSave = 148;
        public final static int VerdataUpgradeSave = 753;
        public final static int VerdataPotionClan = 160;
        public final static short MAX_ITEM_IN_BAG = 2000;
        //
        public final static byte[] mLockMap = new byte[] {8, 11, 8, 8, 5, 5, 20, 20, 13, 13, 8, 8,
                        4, 4, 4, 7, 4, 4, 7, 7, 25, 42, 9, 12, 5, 5, 8, 8, 9, 12, 2, 2, 3, 5, 6, 8,
                        4, 4, 13, 13};
        public final static int[] mMapLang =
                        new int[] {1, 9, 17, 25, 41, 33, 49, 66, 69, 79, 83, 93, 107, 113, 189};
        public final static int[] TabInventory_ItemSell = new int[] {15, 250, 1};
        public final static int[] mTileUpdate =
                        new int[] {1000, 700, 500, 400, 300, 200, 180, 160, 140, 50};
        public final static int[] mTileGhepĐa = new int[] {100, 100, 85, 70, 55, 40};
        public final static String[] mEffSpec = new String[] {"Không có gì", "Choáng", "Chảy máu",
                        "Giảm công", "Giảm thủ", "Hoa mắt", "Điện giật", "Lửa cháy", "Trói chân",
                        "Hút năng lượng", "Trúng độc", "Bất tử", "Critical liên tục", "Tru bat tu",
                        "Trụ bất tử", "Hút sức mạnh", "Hoảng loạn"};
        public final static String[] NamePotionquest = new String[] {"Thịt Heo", "Kiếm sắt",
                        "Răng Chuột Cống", "Bạch tuộc", "Lông Chuột Túi", "Vây Cá Mập", "Phấn Hoa",
                        "Thịt Cá", "Mực tươi", "Nọc Rắn", "Vảy Rồng", "Sừng bò", "Thịt Heo Núi",
                        "Gạt Hưu", "Tai Thỏ", "Lông Cò Trắng", "Mang cá Mập", "Răng Thuồng Luồng",
                        "Vali tiền", "Chìa khóa"};
        public final static String[] AttriKichAn = new String[] {
                        "Bất tử\nKhi nhận đòn đánh sẽ ngẩu nhiên kích hoạt né chiêu và bất tử trong 5s.Hồi chiêu: 60s.",
                        "Lời cảm ơn\nKhi nhận đòn đánh sẽ ngẩu nhiên kích hoạt né chiêu và hút 20% sát thương chuyển thành máu. Hồi chiêu: 60s.",
                        "Là chắn\nKhi nhận đòn đánh sẽ ngẩu nhiên kích hoạt né chiêu và làm choáng lại đối thủ 5s.Hồi chiêu: 60s.",
                        "Khóa năng lượng\nKhi nhận đòn đánh sẽ ngẩu nhiên kích hoạt né chiêu đồng thời hút hết năng lượng của đối thủ. "
                                        + "Hồi chiêu: 60s.",
                        "Bộc phá\nKhi tung chiêu sẽ ngẩu nhiên kích hoạt tăng 50% sát thương. Hồi chiêu: 60s.",
                        "Tập trung cao độ\nKhi tung chiêu sẽ ngẩu nhiên kích hoạt liên hoàn Chí mạng trong 10s. Hồi chiêu: 60s.",
                        "Ma cà rồng\nKhi tung chiêu sẽ ngẩu nhiên kích hoạt biến 20% sát thương thành máu. Hồi chiêu: 60s.",
                        "Đánh là choáng\nKhi tung đủ chiêu thức sẽ kích hoạt choáng đối thủ 5s. Hồi chiêu: 60s.",
                        "Thanh lọc\nKhi tung đủ chiêu thức sẽ kích hoạt thời gian của tất cả chiêu thức về 0.Hồi chiêu: 60s.",
                        "Nén đau\nKhi nhận đủ chiêu từ đối phương sẽ kích hoạt thời gian của tất cả chiêu thức về 0. Hồi chiêu: 60s.",
                        "Giải phóng năng lượng\nKhi nhận đủ chiêu từ đối phương sẽ kích hoạt làm choáng đối thủ trong 5s. Hồi chiêu: 60s.",
                        "Người bất tử\nMỗi đòn đánh vời đối phương sẽ hút về 1% máu tối đa.",
                        "Mở khóa\nTrang bị này sẽ không bao giờ bị khóa.",};
        public final static int[][] mSea = new int[][] { //
                        new int[] {6, 7, 2, 370, 155, -1}, //
                        new int[] {8, 7, 2, 145, 155, 1}, //
                        new int[] {14, 15, 4, 355, 400, -1}, //
                        new int[] {16, 15, 2, 145, 155, 1}, //
                        new int[] {22, 23, 4, 480, 400, 1}, //
                        new int[] {24, 23, 2, 180, 155, 1}, //
                        new int[] {30, 31, 4, 500, 400, 1}, //
                        new int[] {32, 31, 4, 200, 445, 1}, //
                        new int[] {38, 39, 4, 500, 420, 1}, //
                        new int[] {40, 39, 2, 140, 155, 1}, //
                        new int[] {46, 47, 2, 340, 155, 1}, //
                        new int[] {48, 47, 2, 145, 155, 1}, //
                        new int[] {54, 63, 4, 214, 390, 1}, //
                        new int[] {65, 64, 2, 200, 182, 1}, //
                        new int[] {66, 67, 2, 300, 175, -1}, //
                        new int[] {68, 67, 4, 200, 350, 1}, //
                        new int[] {74, 78, 4, 390, 350, 1}, //
                        new int[] {79, 78, 4, 145, 460, 1}, //
                        new int[] {82, 78, 4, 214, 440, 1}, //
                        new int[] {88, 91, 4, 445, 440, -1}, //
                        new int[] {92, 91, 4, 214, 512, 1}, //
                        new int[] {103, 106, 4, 680, 380, -1}, //
                        new int[] {107, 106, 4, 132, 386, 1}, //
                        new int[] {108, 110, 4, 350, 386, 1}, //
                        new int[] {112, 111, 4, 350, 400, 1}, //
                        new int[] {113, 114, 4, 680, 410, -1}, //
                        new int[] {-1, 178, 2, -1, -1, -1}, //
                        new int[] {-1, 182, 2, -1, -1, -1}, //
                        new int[] {-1, 181, 2, -1, -1, -1}, //
                        new int[] {-1, 183, 2, -1, -1, -1}, //
                        new int[] {-1, 984, 2, 200, 182, 1}, //
                        new int[] {191, 190, 4, 120, 542, 1} //
        };
}
