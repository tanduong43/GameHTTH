package activities;

import client.Player;
import map.Map;
import map.Mob;
import template.MobTemplate;

import java.util.ArrayList;
import java.util.List;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;

/**
 * Pho ban Vuot Ai Don
 * @author Truongbk
 */
public class Dungeon {
    public List<Map> maps;
    public List<Mob> mobs;
    public long time;
    public byte mode;
    public Set<Integer> checkG;

    public void create() {
        this.time = System.currentTimeMillis() + 60_000L * 15;
        this.maps = new CopyOnWriteArrayList<>();
        this.mobs = new CopyOnWriteArrayList<>();
        this.checkG = ConcurrentHashMap.newKeySet();
        // Map 167 là khu vực mới vào (sảnh chờ), không có quái
        this.checkG.add(167);
        int index = -2;

        for (int j = 167; j <= 176; j++) {
            // create map
            Map[] mTemplates = Map.get_map_by_id(j);
            if (mTemplates == null || mTemplates.length == 0 || mTemplates[0] == null) {
                continue;
            }
            Map mapTemplate = mTemplates[0];
            Map map_dungeon = new Map();
            map_dungeon.template = mapTemplate.template;
            map_dungeon.zone_id = (byte) 0;
            map_dungeon.list_mob = new int[0];

            List<Mob> mobCandidates = new ArrayList<>();

            // Khu vực 1 (Map 167) là sảnh vào, không có quái. Quái bắt đầu từ Map 168.
            if (j != 167 && mapTemplate.list_mob != null) {
                // Tách Boss và Mob thường để đảm bảo Boss không bị bỏ qua khi giới hạn số lượng quái
                List<Mob> bossList = new ArrayList<>();
                List<Mob> normalList = new ArrayList<>();

                for (int i = 0; i < mapTemplate.list_mob.length; i++) {
                    Mob temp = Mob.ENTRYS.get(mapTemplate.list_mob[i]);
                    if (temp == null || temp.mob_template == null) {
                        continue;
                    }
                    boolean isBoss = temp.mob_template.mob_id >= 100 || temp.mob_template.hp_max >= 500000
                            || (temp.mob_template.name != null && (temp.mob_template.name.toLowerCase().contains("boss")
                                    || temp.mob_template.name.toLowerCase().contains("trùm")));
                    Mob mob_add = new Mob();
                    mob_add.mob_template = temp.mob_template;
                    mob_add.x = temp.x;
                    mob_add.y = temp.y;
                    if (isBoss) {
                        bossList.add(mob_add);
                    } else {
                        normalList.add(mob_add);
                    }
                }

                // Giới hạn tối đa 25 quái mỗi map để đảm bảo Client hiển thị đầy đủ 100% (Client cap vecObjMove <= 50)
                int maxMobs = 25;
                mobCandidates.addAll(bossList);
                int remainSlots = maxMobs - bossList.size();
                if (remainSlots > 0) {
                    if (normalList.size() <= remainSlots) {
                        mobCandidates.addAll(normalList);
                    } else {
                        // Chọn đều quái trải khắp map thay vì dồn cục một góc
                        double step = (double) normalList.size() / remainSlots;
                        for (int s = 0; s < remainSlots; s++) {
                            int pickIdx = (int) (s * step);
                            if (pickIdx < normalList.size()) {
                                mobCandidates.add(normalList.get(pickIdx));
                            }
                        }
                    }
                }
            }

            // Thiết lập chỉ số và add vào danh sách quái của Dungeon
            for (Mob mob_add : mobCandidates) {
                boolean isBoss = mob_add.mob_template.mob_id >= 100 || mob_add.mob_template.hp_max >= 500000
                        || (mob_add.mob_template.name != null && (mob_add.mob_template.name.toLowerCase().contains("boss")
                                || mob_add.mob_template.name.toLowerCase().contains("trùm")));

                mob_add.hp_max = mob_add.mob_template.hp_max;
                mob_add.hp = 9978;
                if (this.mode < 7) {
                    mob_add.hp = mob_add.hp + this.mode * 5000;
                } else {
                    mob_add.hp = mob_add.hp + this.mode * 50000;
                }
                if (isBoss) {
                    mob_add.hp *= 5; // Boss trâu hơn
                }
                mob_add.hp_max = mob_add.hp;
                mob_add.level = 35 + this.mode * 10;
                if (mob_add.level > 100) {
                    mob_add.level = 100;
                }

                mob_add.isdie = false;
                mob_add.id_target = -1;
                mob_add.index = index--;
                mob_add.map = map_dungeon;
                mob_add.boss_info = null;
                this.mobs.add(mob_add);
            }

            map_dungeon.start_map();
            map_dungeon.map_dungeon = this;
            Map.add_map_plus(map_dungeon);
            this.maps.add(map_dungeon);
        }
    }

    public Mob get_mob(Player p, int id) {
        if (p == null || p.map == null || this.mobs == null) {
            return null;
        }
        for (int i = 0; i < this.mobs.size(); i++) {
            Mob mob = this.mobs.get(i);
            if (mob != null && p.map.equals(mob.map) && mob.index == id) {
                return mob;
            }
        }
        return null;
    }
}
