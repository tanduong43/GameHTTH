package event;

/**
 * Trạng thái của 1 Biển trong sự kiện Thủ Lĩnh Biển Khơi.
 * CHỈ CÓ EMPTY VÀ FULL - KHÔNG CÓ HÀNG CHỜ / WAITING.
 */
public enum SeaStatus {
    EMPTY("CÒN TRỐNG"),
    FULL("ĐÃ ĐẦY");

    private final String description;

    SeaStatus(String description) {
        this.description = description;
    }

    public String getDescription() {
        return description;
    }
}
