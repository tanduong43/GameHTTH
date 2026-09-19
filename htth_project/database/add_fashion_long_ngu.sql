-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Long Ngu (Fashion ID 257)
-- Part 1146 (Đầu - Type 0): Image 28858 - 28862 (Small Image 12858 - 12862) [5 frame]
-- Part 1147 (Thân / Áo - Type 1): Image 28863 - 28882 (Small Image 12863 - 12882) [20 frame]
-- Part 1148 (Quần / Váy - Type 2): Image 28883 - 28897 (Small Image 12883 - 12897) [15 frame]
-- Icon thời trang: 155 (file data/icon/x4/20155.png, ID = 20155 - 20000 = 155)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1146, 1147, 1148);
DELETE FROM `fashiontemplate` WHERE `id` IN (257, 37);

-- 1. Thêm các Part vào bảng `parts`
-- Part 1146 (Type 0 - Đầu: 5 frame, canh chỉnh dx=-2, dy=-4 lên trên)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1146, 0, '[[12858,-2,-4],[12859,-2,-4],[12860,-2,-4],[12861,-2,-4],[12862,-2,-4]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1147 (Type 1 - Thân / Áo: 20 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1147, 1, '[[12863,0,0],[12864,0,0],[12865,0,0],[12866,0,0],[12867,0,0],[12868,0,0],[12869,0,0],[12870,0,0],[12871,0,0],[12872,0,0],[12873,0,0],[12874,0,0],[12875,0,0],[12876,0,0],[12877,0,0],[12878,0,0],[12879,0,0],[12880,0,0],[12881,0,0],[12882,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1148 (Type 2 - Quần / Váy: 15 frame, canh chỉnh dx=4 qua phải)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1148, 2, '[[12883,4,0],[12884,4,0],[12885,4,0],[12886,4,0],[12887,4,0],[12888,4,0],[12889,4,0],[12890,4,0],[12891,4,0],[12892,4,0],[12893,4,0],[12894,4,0],[12895,4,0],[12896,4,0],[12897,4,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 37
-- icon: 155 (file data/icon/x4/20155.png)
-- mwear: [-2, -2, -1, 1147, -1, 1148, 1146, -2]
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    37,
    155,
    'Thời trang Long Ngu',
    'Thời trang Long Ngu\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1147,-1,1148,1146,-2]',
    '[[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);
