-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Nico Robin (Fashion ID 251)
-- Part 1127 (Đầu - Type 0): Image 28319 - 28323 (Small Image 12319 - 12323) [5 frame]
-- Part 1128 (Thân / Áo - Type 1): Image 28324 - 28343 (Small Image 12324 - 12343) [20 frame]
-- Part 1129 (Quần / Váy - Type 2): Image 28344 - 28358 (Small Image 12344 - 12358) [15 frame]
-- Icon thời trang: 143 (file data/icon/x4/20143.png, ID = 20143 - 20000 = 143)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1127, 1128, 1129);
DELETE FROM `fashiontemplate` WHERE `id` = 251;

-- 1. Thêm các Part vào bảng `parts`
-- Part 1127 (Type 0 - Đầu: 5 frame, canh chỉnh dx=-3 qua trái 3px, dy=-3 lên 3px)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1127, 0, '[[12319,-3,-3],[12320,-3,-3],[12321,-3,-3],[12322,-3,-3],[12323,-3,-3]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1128 (Type 1 - Thân / Áo: 20 frame, Frame 0 đứng dy=-4; Frame 1-4 di chuyển hạ xuống dy=-1, dx=-2)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1128, 1, '[[12324,-2,-4],[12325,-2,-1],[12326,-2,-1],[12327,-2,-1],[12328,-2,-1],[12329,-2,-4],[12330,-2,-4],[12331,-2,-4],[12332,-2,-4],[12333,-2,-4],[12334,-2,-4],[12335,-2,-4],[12336,-2,-4],[12337,-2,-4],[12338,-2,-4],[12339,-2,-4],[12340,-2,-4],[12341,-2,-4],[12342,-2,-4],[12343,-2,-4]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1129 (Type 2 - Quần / Váy: 15 frame chuẩn)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1129, 2, '[[12344,0,0],[12345,0,0],[12346,0,0],[12347,0,0],[12348,0,0],[12349,0,0],[12350,0,0],[12351,0,0],[12352,0,0],[12353,0,0],[12354,0,0],[12355,0,0],[12356,0,0],[12357,0,0],[12358,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 251
-- icon: 143 (file data/icon/x4/20143.png)
-- mwear: [-2, -2, -1, 1128, -1, 1129, 1127, -2]
--        (Slot 0: -2 ẩn vũ khí, Slot 1: -2 ẩn nón, Slot 3: Thân 1128, Slot 5: Quần 1129, Slot 6: Đầu 1127, Slot 7: -2 ẩn tóc mặc định)
-- op: [[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]
-- price: -1
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    251,
    143,
    'Thời trang Robin',
    'Thời trang Robin\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1128,-1,1129,1127,-2]',
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
SELECT * FROM `parts` WHERE `id` IN (1127, 1128, 1129);
SELECT * FROM `fashiontemplate` WHERE `id` = 251;
