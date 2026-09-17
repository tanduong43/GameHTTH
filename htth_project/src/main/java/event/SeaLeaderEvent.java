package event;

/**
 * Model biểu diễn một đợt sự kiện Thủ Lĩnh Biển Khơi.
 */
public class SeaLeaderEvent {
    public static final String STATUS_WAITING = "WAITING";
    public static final String STATUS_ACTIVE = "ACTIVE";
    public static final String STATUS_ENDED = "ENDED";

    private int id;
    private String weekKey;
    private String status;
    private long startTime;
    private long endTime;
    private int winnerSeaId = -1;
    private int winnerClanId = -1;
    private String winnerClanName = "";
    private int winningScore = 0;

    public SeaLeaderEvent() {
        this.status = STATUS_WAITING;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getWeekKey() {
        return weekKey;
    }

    public void setWeekKey(String weekKey) {
        this.weekKey = weekKey;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public long getStartTime() {
        return startTime;
    }

    public void setStartTime(long startTime) {
        this.startTime = startTime;
    }

    public long getEndTime() {
        return endTime;
    }

    public void setEndTime(long endTime) {
        this.endTime = endTime;
    }

    public int getWinnerSeaId() {
        return winnerSeaId;
    }

    public void setWinnerSeaId(int winnerSeaId) {
        this.winnerSeaId = winnerSeaId;
    }

    public int getWinnerClanId() {
        return winnerClanId;
    }

    public void setWinnerClanId(int winnerClanId) {
        this.winnerClanId = winnerClanId;
    }

    public String getWinnerClanName() {
        return winnerClanName;
    }

    public void setWinnerClanName(String winnerClanName) {
        this.winnerClanName = winnerClanName;
    }

    public int getWinningScore() {
        return winningScore;
    }

    public void setWinningScore(int winningScore) {
        this.winningScore = winningScore;
    }
}
