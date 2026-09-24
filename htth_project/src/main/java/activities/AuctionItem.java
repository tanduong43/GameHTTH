package activities;

public class AuctionItem {
    public int id;                 // ID trong DB
    public byte slotId;            // Slot hiển thị trên client (0, 1, 2, ...)
    public String name;            // Tên hiển thị
    public byte category;          // Loại (3: trang bị, 4: thường/tiêu hao, 7: đá...)
    public short templateId;       // ID template vật phẩm
    public int quantity;           // Số lượng
    public byte color;             // Phẩm cấp (0..4)
    public String itemOptions;     // JSON option nếu có
    public int startPrice;         // Giá khởi điểm (Coin)
    public int currentPrice;       // Giá hiện tại (Coin)
    public int stepPrice;          // Bước giá mỗi lần bid (Coin)
    public int buyoutPrice;        // Giá chốt mua ngay (0: không có)
    public int highestBidderId;    // ID player giữ giá cao nhất (-1 nếu chưa có)
    public String highestBidderName; // Tên player giữ giá cao nhất
    public String highestBidderUser; // Username tài khoản player giữ giá
    public long endTime;           // Timestamp kết thúc (msec)
    public byte status;            // 0: Đang đấu, 1: Chờ nhận quà, 2: Đã nhận, 3: Đã hủy

    public AuctionItem() {
        this.highestBidderId = -1;
        this.highestBidderName = "";
        this.highestBidderUser = "";
        this.status = 0;
        this.stepPrice = 10;
    }

    public int getTimeRemainSeconds() {
        if (status != 0) {
            return 0;
        }
        long diff = (endTime - System.currentTimeMillis()) / 1000L;
        return diff > 0 ? (int) diff : 0;
    }

    public boolean isExpired() {
        return status != 0 || System.currentTimeMillis() >= endTime;
    }

    public boolean isHighestBidder(int playerId) {
        return highestBidderId == playerId;
    }

    public boolean isClaimable(int playerId) {
        return (status == 1 || (status == 0 && isExpired())) && highestBidderId == playerId;
    }
}
