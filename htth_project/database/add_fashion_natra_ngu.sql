-- =====================================================================
-- SQL Script: Thêm Bộ Thời Trang Natra Ngu (Fashion ID 256)
-- Part 1142 (Đầu - Type 0): Image 28805 - 28809 (Small Image 12805 - 12809) [5 frame]
-- Part 1143 (Thân / Áo - Type 1): Image 28810 - 28829 (Small Image 12810 - 12829) [20 frame]
-- Part 1144 (Quần / Váy - Type 2): Image 28830 - 28844 (Small Image 12830 - 12844) [15 frame]
-- Part 1145 (Vũ khí - Type 3): Image 28845 - 28857 (Small Image 12845 - 12857) [24 frame chuẩn theo animation]
-- Icon thời trang: 154 (file data/icon/x4/20154.png, ID = 20154 - 20000 = 154)
-- =====================================================================
use full_db_htth;

-- 0. Xóa dữ liệu cũ để làm sạch và nạp lại từ đầu
DELETE FROM `parts` WHERE `id` IN (1142, 1143, 1144, 1145);
DELETE FROM `fashiontemplate` WHERE `id` IN (256, 36);

-- 1. Thêm các Part vào bảng `parts`
-- Part 1142 (Type 0 - Đầu: 5 frame, canh chỉnh dx=-2 qua trái, dy=-2 lên trên)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1142, 0, '[[12805,-2,-2],[12806,-2,-2],[12807,-2,-2],[12808,-2,-2],[12809,-2,-2]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1143 (Type 1 - Thân / Áo: 20 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1143, 1, '[[12810,0,0],[12811,0,0],[12812,0,0],[12813,0,0],[12814,0,0],[12815,0,0],[12816,0,0],[12817,0,0],[12818,0,0],[12819,0,0],[12820,0,0],[12821,0,0],[12822,0,0],[12823,0,0],[12824,0,0],[12825,0,0],[12826,0,0],[12827,0,0],[12828,0,0],[12829,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1144 (Type 2 - Quần / Váy: 15 frame)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1144, 2, '[[12830,0,0],[12831,0,0],[12832,0,0],[12833,0,0],[12834,0,0],[12835,0,0],[12836,0,0],[12837,0,0],[12838,0,0],[12839,0,0],[12840,0,0],[12841,0,0],[12842,0,0],[12843,0,0],[12844,0,0]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- Part 1145 (Type 3 - Vũ khí: 24 frame chuẩn theo chuẩn 13 sprite)
INSERT INTO `parts` (`id`, `type`, `data`) 
VALUES (1145, 3, '[[12845,3,0],[12846,20,-1],[12846,16,-1],[12847,20,-1],[12847,20,-1],[12846,19,-1],[12848,26,-2],[12846,27,-1],[12846,27,-1],[12849,-1,5],[79,0,0],[79,0,0],[12850,-2,3],[12851,0,2],[12852,5,8],[79,0,0],[12853,-1,-1],[12853,-1,-1],[12855,5,-2],[12854,0,-2],[12855,5,-2],[12854,0,-2],[12856,2,0],[12857,2,-1]]')
ON DUPLICATE KEY UPDATE `type` = VALUES(`type`), `data` = VALUES(`data`);

-- 2. Thêm Thời trang mới vào bảng `fashiontemplate`
-- ID: 36
-- icon: 154 (file data/icon/x4/20154.png)
-- mwear: [1145, -2, -1, 1143, -1, 1144, 1142, -2]
--        (Slot 0: Vũ khí 1145, Slot 1: -2 ẩn nón, Slot 3: Thân 1143, Slot 5: Quần 1144, Slot 6: Đầu 1142, Slot 7: -2 ẩn tóc mặc định)
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`)
VALUES (
    36,
    154,
    'Thời trang Natra Ngu',
    'Thời trang Natra Ngu\n+12% Né tránh\n+12% Tăng HP\n+10% Sát thương\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn',
    '[1145,-2,-1,1143,-1,1144,1142,-2]',
    '[[12,120],[17,120],[1,100],[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE
    `icon` = VALUES(`icon`),
    `name` = VALUES(`name`),
    `info` = VALUES(`info`),
    `mwear` = VALUES(`mwear`),
    `op` = VALUES(`op`),
    `price` = VALUES(`price`);
