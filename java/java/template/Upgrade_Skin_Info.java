package template;
/**
 *
 * @author Truongbk
 */
public class Upgrade_Skin_Info {
    public ItemFashionP2 skin;
    public short[] upgrade_skin_data;

    public static String get_op_level(byte level) {
        int percent = 0;
        for (int k = 1; k <= level; k++) {
            if (k % 3 == 0) {
                percent += 10;
            } else {
                percent += 3;
            }
        }
        return String.format("%.1f", ((float) percent / 10f));
    }
}
