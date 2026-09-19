-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Vy Ngu (Fashion ID 254)
-- Part 1136 (Đầu - Type 0): Image 28530 - 28534 (Small Image 12530 - 12534) [5 frame]
-- Part 1137 (Thân / Áo - Type 1): Image 28535 - 28554 (Small Image 12535 - 12554) [20 frame]
-- Part 1138 (Quần / Váy - Type 2): Image 28555 - 28569 (Small Image 12555 - 12569) [15 frame]
-- Icon thời trang: 147 (file data/icon/x4/20147.png, ID = 20147 - 20000 = 147)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1136, 1137, 1138);
DELETE FROM `fashiontemplate` WHERE `id` = 254;

-- 1. Thêm các Part vào bảng `parts`
-- Part 1136 (Type 0 - Đầu: 5 frame, canh chỉnh dx=-4, dy=3)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1136, 0, '[[12530,-4,3],[12531,-4,3],[12532,-4,3],[12533,-4,3],[12534,-4,3]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1137 (Type 1 - Thân / Áo: 20 frame, canh chỉnh dx=-3, dy=-9)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1137, 1, '[[12535,-3,-9],[12536,-3,-9],[12537,-3,-9],[12538,-3,-9],[12539,-3,-9],[12540,-3,-9],[12541,-3,-9],[12542,-3,-9],[12543,-3,-9],[12544,-3,-9],[12545,-3,-9],[12546,-3,-9],[12547,-3,-9],[12548,-3,-9],[12549,-3,-9],[12550,-3,-9],[12551,-3,-9],[12552,-3,-9],[12553,-3,-9],[12554,-3,-9]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1138 (Type 2 - Quần / Váy: 15 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1138, 2, '[[12555,0,0],[12556,0,0],[12557,0,0],[12558,0,0],[12559,0,0],[12560,0,0],[12561,0,0],[12562,0,0],[12563,0,0],[12564,0,0],[12565,0,0],[12566,0,0],[12567,0,0],[12568,0,0],[12569,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 254
-- icon: 147 (file data/icon/x4/20147.png)
-- mwear: [-2, -2, -1, 1137, -1, 1138, 1136, -2]
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    254,
    147,
    'Thời trang Vy Ngu',
    'Thời trang Vy Ngu\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1137,-1,1138,1136,-2]',
    '[[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);
