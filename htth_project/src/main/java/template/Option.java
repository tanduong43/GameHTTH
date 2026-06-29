package template;
/**
 *
 * @author Truongbk
 */
public class Option {
    public static int[] PAR_PER_LEVELUP = new int[] {100, 110, 120, 130, 140, 150, 170, 190, 210,
            230, 250, 250, 250, 250, 250, 250, 250};
    public static int[] PAR_PER_DIAL = new int[] {100, 160, 180, 210, 250, 300};
    public int id;
    private int param;

    public Option(int id, int param) {
        this.id = id;
        this.param = param;
    }

    public int getParam() {
        return param;
    }

    public int getParam(int type, int tier, int isHoanMy) {
        int result = param;
        switch (type) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5: {
                if (this.id < 28) {
                    result = (result * PAR_PER_LEVELUP[tier] * ((isHoanMy == 1) ? 110 : 100))
                            / 10000;
                }
                break;
            }
            case 6: { // heart
                if (this.id == 56) {
                    result = param + 50 * tier;
                }
                break;
            }
            case 7: {
                if (this.id < 28) {
                    if (this.id == 1 || this.id == 20) {
                        result = (result * PAR_PER_DIAL[tier]) / 100;
                    } else {
                        result = (result * PAR_PER_LEVELUP[tier]) / 100;
                    }
                }
                break;
            }
        }
        return result;
    }

    public void setParam(int param) {
        this.param = param;
    }
}
