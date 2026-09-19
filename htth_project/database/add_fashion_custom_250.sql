-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Mới (Fashion ID 250)
-- Dựa trên cấu trúc Thời Trang Đấng (Fashion 59 & Part 803)
-- Part 1123 (Nón - Type 5): Image 28269 - 28270 (Small Image 12269 - 12270)
-- Part 1124 (Thân / Áo - Type 1): Image 28271 - 28290 (Small Image 12271 - 12290)
-- Part 1125 (Quần / Chân - Type 2): Image 28291 - 28305 (Small Image 12291 - 12305)
-- Part 1126 (Vũ khí - Type 3): Image 28306 - 28318 (Small Image 12306 - 12318)
-- Icon thời trang: 142 (data/icon/x4/20142.png)
-- =====================================================================
use full_db_htth;
-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1123, 1124, 1125, 1126);
DELETE FROM `fashiontemplate` WHERE `id` = 250;
DELETE FROM `itemhair` WHERE `id` = 70;

-- 1. Thêm các Part vào bảng `parts`
-- Part 1123 (Type 5 - Nón / Tóc: 2 frame, canh chỉnh dx=-1 qua phải, dy=-6 xuống dưới)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1123, 5, '[[12269,-1,-6],[12270,-1,-6]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1124 (Type 1 - Thân / Áo: 20 frame chuẩn)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1124, 1, '[[12271,0,0],[12272,0,0],[12273,0,0],[12274,0,0],[12275,0,0],[12276,0,0],[12277,0,0],[12278,0,0],[12279,0,0],[12280,0,0],[12281,0,0],[12282,0,0],[12283,0,0],[12284,0,0],[12285,0,0],[12286,0,0],[12287,0,0],[12288,0,0],[12289,0,0],[12290,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1125 (Type 2 - Quần / Chân: 15 frame chuẩn)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1125, 2, '[[12291,0,0],[12292,0,0],[12293,0,0],[12294,0,0],[12295,0,0],[12296,0,0],[12297,0,0],[12298,0,0],[12299,0,0],[12300,0,0],[12301,0,0],[12302,0,0],[12303,0,0],[12304,0,0],[12305,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1126 (Type 3 - Vũ khí: 24 frame, Frame 0 canh chỉnh dx=3 qua trái 3px, dy=0)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1126, 3, '[[12306,3,0],[12307,20,-1],[12307,16,-1],[12308,20,-1],[12308,20,-1],[12307,19,-1],[12309,26,-2],[12307,27,-1],[12307,27,-1],[12310,-1,5],[79,0,0],[79,0,0],[12311,-2,3],[12312,0,2],[12313,5,8],[79,0,0],[12314,-1,-1],[12314,-1,-1],[12316,5,-2],[12315,0,-2],[12316,5,-2],[12315,0,-2],[12317,2,0],[12318,2,-1]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 250
-- icon: 142 (file data/icon/x4/20142.png)
-- mwear: [1126, -2, -1, 1124, -1, 1125, -1, 1123]
--        (Slot 0: Vũ khí 1126, Slot 1: -2 ẩn nón, Slot 3: Thân 1124, Slot 5: Quần 1125, Slot 7: Tóc 1123)
-- op: [[12,120],[17,120],[13,100],[53,100],[63,100],[10,100]]
-- price: -1
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    250,
    142,
    'Thời trang Sabo',
    'Thời trang Sabo\n+12% Né tránh\n+12% Tăng HP\n+10% Xuyên giáp\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[1126,-2,-1,1124,-1,1125,-1,1123]',
    '[[12,120],[17,120],[13,100],[53,100],[63,100],[10,100]]',
    -1
)
ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);

-- 3. (Tuỳ chọn) Thêm Tóc mới vào bảng `itemhair` để bán trong tiệm tóc
INSERT INTO `itemhair` (`id`, `name`, `icon`, `beri`, `ruby`) 
VALUES (70, 'Tóc Sabo', 1123, 0, 500)
ON DUPLICATE KEY UPDATE 
    `name` = VALUES(`name`), 
    `icon` = VALUES(`icon`), 
    `ruby` = VALUES(`ruby`);

-- 4. Kiểm tra dữ liệu sau khi add:
SELECT * FROM `parts` WHERE `id` IN (1123, 1124, 1125, 1126);
SELECT * FROM `fashiontemplate` WHERE `id` = 250;
SELECT * FROM `itemhair` WHERE `id` = 70;
