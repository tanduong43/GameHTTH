-- =====================================================================
-- SQL Script: Thêm Tóc Đen (Part 1122) vào DB HTTH
-- Ngày tạo: 2026-09-16
-- =====================================================================

-- 1. Thêm Part tóc 1122 vào bảng `parts`
-- Frame 0: small image 12980 (dx=1, dy=-8) -> client load icon 28980
-- Frame 1: small image 12981 (dx=2, dy=-8) -> client load icon 28981
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1122, 5, '[[12980,1,-8],[12981,2,-8]]')
ON DUPLICATE KEY UPDATE 
    `type` = VALUES(`type`), 
    `data` = VALUES(`data`);

-- 2. Thêm Tóc Đen (Black) vào bảng `itemhair` (Tiệm tóc)
-- Lưu ý: Cột `icon` trong itemhair chính là Part ID (1122)
INSERT INTO `itemhair` (`id`, `name`, `icon`, `beri`, `ruby`) 
VALUES (69, 'Tóc Đen (Black)', 1122, 0, 500)
ON DUPLICATE KEY UPDATE 
    `name` = VALUES(`name`), 
    `icon` = VALUES(`icon`), 
    `beri` = VALUES(`beri`), 
    `ruby` = VALUES(`ruby`);

-- Kiểm tra lại dữ liệu sau khi add:
SELECT * FROM `parts` WHERE `id` IN (1117, 1118, 1122);
SELECT * FROM `itemhair` WHERE `id` >= 67;
