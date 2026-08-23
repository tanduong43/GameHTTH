package client;

import java.util.ArrayList;
import java.util.List;
import template.Option;

/**
 *
 * @author Truongbk
 */
public class MyPet {
    public short id;
    public Pet template;
    public boolean isUse;
    public long expiryTime = -1; // -1 means permanent

    // Cấp độ và chỉ số huấn luyện theo cấp (Tối đa 3 Lv)
    public byte level = 0; // 0, 1, 2, 3
    public int exp = 0;    // Điểm huấn luyện hiện tại
    public List<Option> extra_op = new ArrayList<>(); // Các chỉ số random thêm theo từng cấp

    public static int getMaxExp(int lv) {
        switch (lv) {
            case 0:
                return 10; // Cần 10 EXP (10 quái) để lên Lv 1
            case 1:
                return 50; // Cần 50 EXP (50 quái) để lên Lv 2
            case 2:
                return 100; // Cần 100 EXP (100 quái) để lên Lv 3
            default:
                return 100;
        }
    }

    public boolean isMaxLevel() {
        return this.level >= 3;
    }

    public boolean canLevelUp() {
        return !isMaxLevel() && this.exp >= getMaxExp(this.level);
    }

    public void addExp(int amount) {
        if (isMaxLevel()) {
            return;
        }
        int max = getMaxExp(this.level);
        this.exp = Math.min(this.exp + amount, max);
    }
}

