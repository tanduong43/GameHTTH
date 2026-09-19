-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Nu (Fashion ID 258)
-- Part 1149 (Đầu - Type 0): Image 28899 - 28903 (Small Image 12899 - 12903) [5 frame]
-- Part 1150 (Thân / Áo - Type 1): Image 28904 - 28923 (Small Image 12904 - 12923) [20 frame]
-- Part 1151 (Quần / Váy - Type 2): Image 28924 - 28938 (Small Image 12924 - 12938) [15 frame]
-- Icon thời trang: 156 (file data/icon/x4/20156.png, ID = 20156 - 20000 = 156)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1149, 1150, 1151);
DELETE FROM `fashiontemplate` WHERE `id` IN (258, 38);

-- 1. Thêm các Part vào bảng `parts`
-- Part 1149 (Type 0 - Đầu: 5 frame, canh chỉnh dx=-2, dy=-4 lên trên)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1149, 0, '[[12899,-2,-4],[12900,-2,-4],[12901,-2,-4],[12902,-2,-4],[12903,-2,-4]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1150 (Type 1 - Thân / Áo: 20 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1150, 1, '[[12904,0,0],[12905,0,0],[12906,0,0],[12907,0,0],[12908,0,0],[12909,0,0],[12910,0,0],[12911,0,0],[12912,0,0],[12913,0,0],[12914,0,0],[12915,0,0],[12916,0,0],[12917,0,0],[12918,0,0],[12919,0,0],[12920,0,0],[12921,0,0],[12922,0,0],[12923,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1151 (Type 2 - Quần / Váy: 15 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1151, 2, '[[12924,0,0],[12925,0,0],[12926,0,0],[12927,0,0],[12928,0,0],[12929,0,0],[12930,0,0],[12931,0,0],[12932,0,0],[12933,0,0],[12934,0,0],[12935,0,0],[12936,0,0],[12937,0,0],[12938,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 38
-- icon: 156 (file data/icon/x4/20156.png)
-- mwear: [-2, -2, -1, 1150, -1, 1151, 1149, -2]
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    38,
    156,
    'Thời trang Nu',
    'Thời trang Nu\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1150,-1,1151,1149,-2]',
    '[[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);
