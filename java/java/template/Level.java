package template;
/**
 *
 * @author Truongbk
 */
public class Level {
    public static Level[] ENTRYS;
    public static long[] LEVEL_THONGTHAO;
    public long exp;
    public int tiemnang;

    public Level() {}

    public Level(long a, int tn) {
        this.exp = a;
        this.tiemnang = tn;
    }

    static {
        // long tong = 0;
        ENTRYS = new Level[250];
        ENTRYS[0] = new Level(10150, 2); // 1
        ENTRYS[1] = new Level(20860, 2); // 2
        ENTRYS[2] = new Level(30570, 2); // 3
        ENTRYS[3] = new Level(40780, 2); // 4
        ENTRYS[4] = new Level(60760, 2); // 5
        ENTRYS[5] = new Level(100130, 2); // 6
        ENTRYS[6] = new Level(106040, 2); // 7
        ENTRYS[7] = new Level(208000, 2); // 8
        ENTRYS[8] = new Level(409000, 2); // 9
        ENTRYS[9] = new Level(606400, 2); // 10
        ENTRYS[10] = new Level(709450, 2); // 11
        ENTRYS[11] = new Level(1002170, 2); // 12
        ENTRYS[12] = new Level(1025400, 2); // 13
        ENTRYS[13] = new Level(1038000, 2); // 14
        ENTRYS[14] = new Level(1050220, 2); // 15
        for (int i = 15; i < 250; i++) {
            Level temp = ENTRYS[i - 1];
            long value = temp.exp;
            if (i < 200) {
                value = value + (value * ((i - 1) + 2)) / 712;
            }
            ENTRYS[i] = new Level(value, 2);
            // System.out.println((i+1) + " " + value);
            // tong+=value;
        }
        LEVEL_THONGTHAO = new long[150];
        LEVEL_THONGTHAO[0] = ENTRYS[98].exp;
        for (int i = 1; i < LEVEL_THONGTHAO.length; i++) {
            LEVEL_THONGTHAO[i] = (LEVEL_THONGTHAO[i - 1] * 11) / 10;
            // System.out.println((i + 1) + " " + LEVEL_THONGTHAO[i]);
            // tong+=LEVEL_THONGTHAO[i];
        }
    }

    public static int get_total_point_by_level(int level) {
        int par = 2;
        for (int i = 0; i < level; i++) {
            par += 2;
        }
        return par;
    }
}
