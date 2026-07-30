package template;

import java.util.ArrayList;
import java.util.List;

public class DanhHieuTemplate {
    public static final List<DanhHieuTemplate> ENTRYS = new ArrayList<>();
    
    public int id;
    public String name;
    public int idicon;
    public int nframe;
    public List<Option> op;
    public String description;
    public String color;
    public byte rarity;
    public int effect_id;
    public long duration;
    
    public static DanhHieuTemplate get(int id) {
        for (int i = 0; i < ENTRYS.size(); i++) {
            if (ENTRYS.get(i).id == id) {
                return ENTRYS.get(i);
            }
        }
        return null;
    }

    public int getEffectId() {
        if (effect_id >= 0) {
            return effect_id;
        }
        if (name != null && name.toLowerCase().contains("vòng chân")) {
            return idicon;
        }
        if (idicon > 0) {
            return idicon;
        }
        return -1;
    }
}
