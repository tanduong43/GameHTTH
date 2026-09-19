-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Jinbe (Fashion ID 253)
-- Part 1133 (Đầu - Type 0): Image 28439 - 28443 (Small Image 12439 - 12443) [5 frame]
-- Part 1134 (Thân / Áo - Type 1): Image 28444 - 28463 (Small Image 12444 - 12463) [20 frame]
-- Part 1135 (Quần / Váy - Type 2): Image 28464 - 28478 (Small Image 12464 - 12478) [15 frame]
-- Icon thời trang: 159 (file data/icon/x4/20159.png, ID = 20159 - 20000 = 159)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1133, 1134, 1135);
DELETE FROM `fashiontemplate` WHERE `id` = 253;

-- 1. Thêm các Part vào bảng `parts`
-- Part 1133 (Type 0 - Đầu: 5 frame, canh chỉnh dx=7 qua phải 7px, dy=-29 lên 29px)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1133, 0, '[[12439,7,-23],[12440,7,-23],[12441,7,-23],[12442,7,-23],[12443,7,-23]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1134 (Type 1 - Thân / Áo: 20 frame, đồng nhất dx=-6, dy=-20 giữ thân cố định cân xứng khi di chuyển)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1134, 1, '[[12444,-6,-20],[12445,-6,-20],[12446,-6,-20],[12447,-6,-20],[12448,-6,-20],[12449,-6,-20],[12450,-6,-20],[12451,-6,-20],[12452,-6,-20],[12453,-6,-20],[12454,-6,-20],[12455,-6,-20],[12456,-6,-20],[12457,-6,-20],[12458,-6,-20],[12459,-6,-20],[12460,-6,-20],[12461,-6,-20],[12462,-6,-20],[12463,-6,-20]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1135 (Type 2 - Quần / Váy: 15 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1135, 2, '[[12464,0,0],[12465,0,0],[12466,0,0],[12467,0,0],[12468,0,0],[12469,0,0],[12470,0,0],[12471,0,0],[12472,0,0],[12473,0,0],[12474,0,0],[12475,0,0],[12476,0,0],[12477,0,0],[12478,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 253
-- icon: 159 (file data/icon/x4/20159.png)
-- mwear: [-2, -2, -1, 1134, -1, 1135, 1133, -2]
--        (Slot 0: -2 ẩn vũ khí, Slot 1: -2 ẩn nón, Slot 3: Thân 1134, Slot 5: Quần 1135, Slot 6: Đầu 1133, Slot 7: -2 ẩn tóc mặc định)
-- op: [[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]
-- price: -1
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    253,
    159,
    'Thời trang Jinbe',
    'Thời trang Jinbe\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1134,-1,1135,1133,-2]',
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
SELECT * FROM `parts` WHERE `id` IN (1133, 1134, 1135);
SELECT * FROM `fashiontemplate` WHERE `id` = 253;
