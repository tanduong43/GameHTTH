-- =====================================================================
-- SQL Script: Thêm Tóc Hồng Rosé (Part 1118) vào DB HTTH
-- Ngày tạo: 2026-09-16
-- =====================================================================

-- 1. Thêm Part tóc 1118 vào bảng `parts`
-- Frame 0: small image 12978 (dx=1, dy=-8) -> client load icon 28978
-- Frame 1: small image 12979 (dx=2, dy=-8) -> client load icon 28979
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1118, 5, '[[12978,1,-8],[12979,2,-8]]')
ON DUPLICATE KEY UPDATE 
    `type` = VALUES(`type`), 
    `data` = VALUES(`data`);

-- 2. Thêm Tóc Hồng (Rose) vào bảng `itemhair` (Tiệm tóc)
-- Lưu ý: Cột `icon` trong itemhair chính là Part ID (1118)
INSERT INTO `itemhair` (`id`, `name`, `icon`, `beri`, `ruby`) 
VALUES (68, 'Tóc Hồng (Rose)', 1118, 0, 20000)
ON DUPLICATE KEY UPDATE 
    `name` = VALUES(`name`), 
    `icon` = VALUES(`icon`), 
    `beri` = VALUES(`beri`), 
    `ruby` = VALUES(`ruby`);

-- Kiểm tra lại dữ liệu sau khi add:
SELECT * FROM `parts` WHERE `id` IN (1117, 1118);
SELECT * FROM `itemhair` WHERE `id` >= 67;
