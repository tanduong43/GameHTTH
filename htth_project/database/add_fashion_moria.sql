-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Gecko Moria (Fashion ID 252)
-- Part 1130 (Đầu - Type 0): Image 28399 - 28403 (Small Image 12399 - 12403) [5 frame]
-- Part 1131 (Thân / Áo - Type 1): Image 28404 - 28423 (Small Image 12404 - 12423) [20 frame]
-- Part 1132 (Quần / Váy - Type 2): Image 28424 - 28438 (Small Image 12424 - 12438) [15 frame]
-- Icon thời trang: 158 (file data/icon/x4/20158.png, ID = 20158 - 20000 = 158)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1130, 1131, 1132);
DELETE FROM `fashiontemplate` WHERE `id` = 252;

-- 1. Thêm các Part vào bảng `parts`
-- Part 1130 (Type 0 - Đầu: 5 frame, canh chỉnh dx=-1 qua trái 1px, dy=-40 lên 40px)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1130, 0, '[[12399,-1,-40],[12400,-1,-40],[12401,-1,-40],[12402,-1,-40],[12403,-1,-40]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1131 (Type 1 - Thân / Áo: 20 frame, đồng nhất dx=-13, dy=-26 giữ thân cố định cân xứng khi di chuyển)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1131, 1, '[[12404,-13,-26],[12405,-13,-26],[12406,-13,-26],[12407,-13,-26],[12408,-13,-26],[12409,-13,-26],[12410,-13,-26],[12411,-13,-26],[12412,-13,-26],[12413,-13,-26],[12414,-13,-26],[12415,-13,-26],[12416,-13,-26],[12417,-13,-26],[12418,-13,-26],[12419,-13,-26],[12420,-13,-26],[12421,-13,-26],[12422,-13,-26],[12423,-13,-26]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1132 (Type 2 - Quần / Váy: 15 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1132, 2, '[[12424,0,0],[12425,0,0],[12426,0,0],[12427,0,0],[12428,0,0],[12429,0,0],[12430,0,0],[12431,0,0],[12432,0,0],[12433,0,0],[12434,0,0],[12435,0,0],[12436,0,0],[12437,0,0],[12438,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 252
-- icon: 158 (file data/icon/x4/20158.png)
-- mwear: [-2, -2, -1, 1131, -1, 1132, 1130, -2]
--        (Slot 0: -2 ẩn vũ khí, Slot 1: -2 ẩn nón, Slot 3: Thân 1131, Slot 5: Quần 1132, Slot 6: Đầu 1130, Slot 7: -2 ẩn tóc mặc định)
-- op: [[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]
-- price: -1
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    252,
    158,
    'Thời trang Gecko Moria',
    'Thời trang Gecko Moria\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1131,-1,1132,1130,-2]',
    '[[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]',
    -1
)
ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);

-- 3. Kiểm tra dữ liệu sau khi add:
SELECT * FROM `parts` WHERE `id` IN (1130, 1131, 1132);
SELECT * FROM `fashiontemplate` WHERE `id` = 252;
