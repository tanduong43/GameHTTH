-- Tạo bảng auction_items nếu chưa tồn tại
CREATE TABLE IF NOT EXISTS `auction_items` (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `slot_id` TINYINT NOT NULL DEFAULT 0,
  `name` VARCHAR(255) NOT NULL,
  `category` TINYINT NOT NULL COMMENT '3: wear, 4: item4, 7: item7',
  `template_id` SMALLINT NOT NULL,
  `quantity` INT NOT NULL DEFAULT 1,
  `color` TINYINT NOT NULL DEFAULT 0,
  `item_options` TEXT NULL COMMENT 'JSON lưu option trang bị nếu category=3',
  `start_price` INT NOT NULL DEFAULT 100 COMMENT 'Giá khởi điểm (Coin)',
  `current_price` INT NOT NULL DEFAULT 100 COMMENT 'Giá hiện tại (Coin)',
  `step_price` INT NOT NULL DEFAULT 10 COMMENT 'Bước tăng giá mỗi lần bid',
  `buyout_price` INT NOT NULL DEFAULT 0 COMMENT 'Giá chốt mua ngay (0: không có)',
  `highest_bidder_id` INT NOT NULL DEFAULT -1 COMMENT 'ID người giữ giá cao nhất',
  `highest_bidder_name` VARCHAR(100) NULL,
  `highest_bidder_user` VARCHAR(255) NULL COMMENT 'Username tài khoản người giữ giá',
  `end_time` BIGINT NOT NULL COMMENT 'Timestamp kết thúc (msec)',
  `status` TINYINT NOT NULL DEFAULT 0 COMMENT '0: Đang đấu, 1: Chờ nhận quà, 2: Đã nhận, 3: Đã hủy',
  INDEX `idx_status` (`status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng auction_history nếu chưa tồn tại
CREATE TABLE IF NOT EXISTS `auction_history` (
  `id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `auction_id` INT NOT NULL,
  `player_id` INT NOT NULL,
  `player_name` VARCHAR(100) NOT NULL,
  `bid_type` TINYINT NOT NULL COMMENT '1: Bid thường, 2: Buyout, 3: Nhận hàng',
  `coin_amount` INT NOT NULL,
  `time` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  INDEX `idx_auction_player` (`auction_id`, `player_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
