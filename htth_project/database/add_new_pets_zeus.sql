-- =====================================================================
-- SQL Script: Thêm 3 Pet mới (Pet Mây Zeus, Zeus Cuồng Nộ & Prometheus)
-- Dự án: Game HTTH (Hải Tặc Tí Hon)
-- Ngày cập nhật: 2026-09-16
-- =====================================================================

-- ---------------------------------------------------------------------
-- GIẢI THÍCH CƠ CHẾ ID / OFFSET TRONG GAME HTTH:
-- 1. Cột `icon` (Ảnh hiển thị trong túi đồ / kho Pet):
--    - Client game khi vẽ túi đồ Pet (typeObject = 110) sẽ tự động cộng thêm offset +23000
--      để gửi request tải ảnh lên server (load_image(icon, 23000)).
--    - Khi server gửi ảnh về, client trừ 23000 để lưu vào cache (HashImageOtherNew).
--    - File ảnh trên server: 23703.png, 23704.png, 23715.png.
--    => Giá trị icon cần lưu vào database:
--       + Pet 113: 23703 - 23000 = 703
--       + Pet 114: 23704 - 23000 = 704
--       + Pet 115: 23715 - 23000 = 715
--
-- 2. Cột `frame` (Sprite hoạt ảnh bay lơ lửng khi xuất hiện trên map):
--    - Client game khi tải hình ảnh quái/pet (HashImageMonster) tự cộng thêm offset +1000
--      để gửi request tải ảnh lên server (load_image(frame, 1000)).
--    - Khi server gửi ảnh về, client trừ 1000 để lưu vào cache.
--    - File ảnh trên server: 1803.png, 1804.png, 1951.png (kích thước 58x174 = 3 frames 58x58).
--    => Giá trị frame cần lưu vào database:
--       + Pet 113: 1803 - 1000 = 803
--       + Pet 114: 1804 - 1000 = 804
--       + Pet 115: 1951 - 1000 = 951
--
-- 3. Cột `type` = 4 (Dạng Pet bay / lơ lửng - 3 frames):
--    - Trong Client (`Pet.cs`), `typePet = 4` sẽ tự động cấu hình:
--      nFrame = 3 (khớp đúng 3 frame của ảnh), độ cao dyMain = 20, dyMovePet = 15.
-- ---------------------------------------------------------------------

-- BẢN 1: CHUẨN GAME GỐC (Chỉ số rỗng '[]', luyện cấp qua Đảo Huấn Luyện)
INSERT INTO `pet_template` (`id`, `name`, `icon`, `type`, `frame`, `op`, `show`) VALUES
(113, 'Pet Mây Zeus', 703, 4, 803, '[]', 1),
(114, 'Pet Zeus Cuồng Nộ', 704, 4, 804, '[]', 2),
(115, 'Pet Thần Hỏa Prometheus', 715, 4, 951, '[]', 2)
ON DUPLICATE KEY UPDATE
  `name` = VALUES(`name`),
  `icon` = VALUES(`icon`),
  `type` = VALUES(`type`),
  `frame` = VALUES(`frame`),
  `op` = VALUES(`op`),
  `show` = VALUES(`show`);

-- BẢN 2: CÓ SẴN CHỈ SỐ CƠ BẢN VIP BAN ĐẦU:
-- UPDATE `pet_template` SET `op` = '[[8, 15], [10, 50]]' WHERE `id` = 113; -- T/n tinh thần +15, Chí mạng +5%
-- UPDATE `pet_template` SET `op` = '[[5, 20], [10, 80], [13, 50]]' WHERE `id` = 114; -- Sức mạnh +20, Chí mạng +8%, Xuyên giáp +5%
-- UPDATE `pet_template` SET `op` = '[[5, 25], [1, 50], [10, 70]]' WHERE `id` = 115; -- Sức mạnh +25, Tấn công +5%, Chí mạng +7%

-- ---------------------------------------------------------------------
-- TÙY CHỌN: TẠO GIFTCODE ĐỂ TEST NHẬN CẢ 3 PET TRONG GAME
-- Nhập mã: `petvip` tại NPC Trưởng Làng để nhận 3 Pet vĩnh viễn
-- ---------------------------------------------------------------------
INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
VALUES ('petvip', 10000000, 5000, '[[8, 113, 1], [8, 114, 1], [8, 115, 1]]', 'Chúc mừng bạn đã nhận được bộ 3 Pet Zeus & Prometheus!', 1, 9999, '', '', 0)
ON DUPLICATE KEY UPDATE
  `item` = VALUES(`item`),
  `thongbao` = VALUES(`thongbao`);