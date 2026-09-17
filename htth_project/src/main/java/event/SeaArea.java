package event;

/**
 * 4 Biển trong sự kiện Thủ Lĩnh Biển Khơi:
 * Biển Đông (Map 182), Biển Bắc (Map 178), Biển Tây (Map 183), Biển Nam (Map 181)
 */
public enum SeaArea {
    BIEN_DONG(0, "Biển Đông", 182),
    BIEN_BAC(1, "Biển Bắc", 178),
    BIEN_TAY(2, "Biển Tây", 183),
    BIEN_NAM(3, "Biển Nam", 181);

    private final int id;
    private final String name;
    private final int mapId;

    SeaArea(int id, String name, int mapId) {
        this.id = id;
        this.name = name;
        this.mapId = mapId;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public int getMapId() {
        return mapId;
    }

    public static SeaArea getById(int id) {
        for (SeaArea area : values()) {
            if (area.getId() == id) {
                return area;
            }
        }
        return null;
    }

    public static SeaArea getByMapId(int mapId) {
        for (SeaArea area : values()) {
            if (area.getMapId() == mapId) {
                return area;
            }
        }
        return null;
    }
}
