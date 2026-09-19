-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Cavendis Ngu (Fashion ID 255)
-- Part 1139 (Đầu - Type 0): Image 28570 - 28574 (Small Image 12570 - 12574) [5 frame]
-- Part 1140 (Thân / Áo - Type 1): Image 28575 - 28594 (Small Image 12575 - 12594) [20 frame]
-- Part 1141 (Quần / Váy - Type 2): Image 28595 - 28609 (Small Image 12595 - 12609) [15 frame]
-- Icon thời trang: 148 (file data/icon/x4/20148.png, ID = 20148 - 20000 = 148)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1139, 1140, 1141);
DELETE FROM `fashiontemplate` WHERE `id` = 255;

-- 1. Thêm các Part vào bảng `parts`
-- Part 1139 (Type 0 - Đầu: 5 frame, canh chỉnh dx=-5, dy=-5)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1139, 0, '[[12570,-5,-5],[12571,-5,-5],[12572,-5,-5],[12573,-5,-5],[12574,-5,-5]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1140 (Type 1 - Thân / Áo: 20 frame, canh chỉnh dx=-2, dy=-15)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1140, 1, '[[12575,-2,-15],[12576,-2,-15],[12577,-2,-15],[12578,-2,-15],[12579,-2,-15],[12580,-2,-15],[12581,-2,-15],[12582,-2,-15],[12583,-2,-15],[12584,-2,-15],[12585,-2,-15],[12586,-2,-15],[12587,-2,-15],[12588,-2,-15],[12589,-2,-15],[12590,-2,-15],[12591,-2,-15],[12592,-2,-15],[12593,-2,-15],[12594,-2,-15]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1141 (Type 2 - Quần / Váy: 15 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1141, 2, '[[12595,0,0],[12596,0,0],[12597,0,0],[12598,0,0],[12599,0,0],[12600,0,0],[12601,0,0],[12602,0,0],[12603,0,0],[12604,0,0],[12605,0,0],[12606,0,0],[12607,0,0],[12608,0,0],[12609,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 255
-- icon: 148 (file data/icon/x4/20148.png)
-- mwear: [-2, -2, -1, 1140, -1, 1141, 1139, -2]
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    255,
    148,
    'Thời trang Cavendis Ngu',
    'Thời trang Cavendis Ngu\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1140,-1,1141,1139,-2]',
    '[[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);
