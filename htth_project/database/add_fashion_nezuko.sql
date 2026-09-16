-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Nezuko Kamado (Fashion 249)
-- Part 1119 (Đầu), Part 1120 (Thân), Part 1121 (Quần)
-- Ngày tạo: 2026-09-16
-- =====================================================================

-- 1. Thêm các Part vào bảng `parts`
-- Part 1119 (Type 0 - Đầu: 5 frame, canh chỉnh dx=5 qua trái, dy=4 cho đứng/đánh/chạy, dy=3 cho gục ngã)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1119, 0, '[[12939,5,4],[12940,5,4],[12940,5,4],[12941,5,4],[12942,5,3]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1120 (Type 1 - Thân / Áo: 20 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1120, 1, '[[12943,0,0],[12944,0,0],[12945,0,0],[12946,0,0],[12947,0,0],[12948,0,0],[12949,0,0],[12950,0,0],[12951,0,0],[12952,0,0],[12953,0,0],[12954,0,0],[12955,0,0],[12956,0,0],[12957,0,0],[12958,0,0],[12959,0,0],[12960,0,0],[12961,0,0],[12962,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1121 (Type 2 - Quần / Chân: 15 frame, canh chỉnh dy=4 để khớp viền áo khoác Haori)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1121, 2, '[[12963,0,4],[12964,0,4],[12965,0,4],[12966,0,4],[12967,0,4],[12968,0,4],[12969,0,4],[12970,0,4],[12971,0,4],[12972,0,4],[12973,0,4],[12974,0,4],[12975,0,4],[12976,0,4],[12977,0,4]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang Nezuko vào bảng `fashiontemplate`
-- ID: 249
-- icon: 142 (file data/icon/x4/20142.png)
-- mwear: [-2, -2, -1, 1120, -1, 1121, 1119, -2] (Thân 1120, Quần 1121, Đầu 1119, ẩn nón và tóc mặc định)
-- op: [[10,100],[12,100],[53,100]] (10% Chí mạng, 10% Né tránh, 10% Miễn thương)
-- price: 10000 Ruby
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    249,
    142,
    'Thời trang Nezuko',
    'Thời trang Nezuko Kamado\n+10% Chí mạng\n+10% Né tránh\n+10% Miễn thương\nHạn sử dụng vĩnh viễn',
    '[-2,-2,-1,1120,-1,1121,1119,-2]',
    '[[10,100],[12,100],[53,100]]',
    -1
)
ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);

-- Kiểm tra dữ liệu sau khi add:
SELECT * FROM `parts` WHERE `id` IN (1119, 1120, 1121);
SELECT * FROM `fashiontemplate` WHERE `id` = 249;
