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
    
    public static DanhHieuTemplate get(int id) {
        for (int i = 0; i < ENTRYS.size(); i++) {
            if (ENTRYS.get(i).id == id) {
                return ENTRYS.get(i);
            }
        }
        return null;
    }
}
