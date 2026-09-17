package event;

/**
 * Quản lý thông tin và trạng thái của 1 Biển trong sự kiện Thủ Lĩnh Biển Khơi.
 * Mỗi biển tại 1 thời điểm chỉ chứa tối đa 1 Clan.
 */
public class SeaLeaderSea {
    private final SeaArea area;
    private final int seaId;
    private final String name;
    private final int mapId;

    private int clanId;
    private String clanName;
    private int score;
    private SeaStatus status;
    private long lastScoreTime;

    public SeaLeaderSea(SeaArea area) {
        this.area = area;
        this.seaId = area.getId();
        this.name = area.getName();
        this.mapId = area.getMapId();
        this.reset();
    }

    public synchronized void reset() {
        this.clanId = -1;
        this.clanName = "";
        this.score = 0;
        this.status = SeaStatus.EMPTY;
        this.lastScoreTime = 0L;
    }

    public synchronized boolean isFull() {
        return this.status == SeaStatus.FULL && this.clanId > 0;
    }

    public synchronized boolean isEmpty() {
        return this.status == SeaStatus.EMPTY || this.clanId <= 0;
    }

    public synchronized void occupy(int clanId, String clanName) {
        this.clanId = clanId;
        this.clanName = clanName;
        this.status = SeaStatus.FULL;
    }

    public synchronized void release() {
        this.clanId = -1;
        this.clanName = "";
        this.status = SeaStatus.EMPTY;
    }

    public synchronized void addScore(int points) {
        this.score += points;
        this.lastScoreTime = System.currentTimeMillis();
    }

    public SeaArea getArea() {
        return area;
    }

    public int getSeaId() {
        return seaId;
    }

    public String getName() {
        return name;
    }

    public int getMapId() {
        return mapId;
    }

    public synchronized int getClanId() {
        return clanId;
    }

    public synchronized String getClanName() {
        return clanName;
    }

    public synchronized int getScore() {
        return score;
    }

    public synchronized void setScore(int score) {
        this.score = score;
    }

    public synchronized SeaStatus getStatus() {
        return status;
    }

    public synchronized void setStatus(SeaStatus status) {
        this.status = status;
    }

    public synchronized long getLastScoreTime() {
        return lastScoreTime;
    }

    public synchronized void setLastScoreTime(long lastScoreTime) {
        this.lastScoreTime = lastScoreTime;
    }

    @Override
    public synchronized String toString() {
        if (isFull()) {
            return name + " (" + clanName + ") - " + score + " điểm - " + status.getDescription();
        } else {
            return name + " - " + status.getDescription();
        }
    }
}
