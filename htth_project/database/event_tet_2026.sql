-- =============================================
-- SỰ KIỆN TẾT NGUYÊN ĐÁN - HTTH GAME
-- Database SQL Statements
-- Created: 2026-08-14
-- =============================================

-- =============================================
-- PHẦN 1: ITEM4 - VẬT PHẨM SỰ KIỆN TẾT
-- =============================================

-- Cập nhật hoặc Insert Item4 Tết (Sử dụng INSERT ... ON DUPLICATE KEY UPDATE)
INSERT INTO `item4` (`id`, `name`, `type`, `gender`, `level`, `icon`, `cooltime`, `的金`, `money`, `quantity`, `gottime`, `lockLife`, `options`, `subname`, `description`, `category`, `sale`, `usetype`, `effect`)
VALUES 
-- Nguyên liệu làm bánh
(351, 'Lá dong', 2, 1, 1, 186, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nguyên liệu làm bánh chưng ngày Tết.', 0, 50, 0, '[]'),
(352, 'Đậu xanh', 2, 1, 1, 186, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nguyên liệu làm nhân bánh chưng ngày Tết.', 0, 50, 0, '[]'),
(353, 'Gạo nếp', 2, 1, 1, 186, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nguyên liệu chính gói bánh chưng ngày Tết.', 0, 50, 0, '[]'),
(354, 'Thịt heo', 2, 1, 1, 186, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nguyên liệu làm nhân bánh chưng ngày Tết.', 0, 50, 0, '[]'),

-- Bánh chưng thành phẩm (Sử dụng)
(350, 'Bánh chưng', 2, 1, 1, 186, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Sử dụng: Tăng 20% Sát thương, +10% Máu cuối trong 30 phút + 10.000 EXP', 1, 100, 1, '[1,20],[17,10]'),

-- Nguyên liệu bánh giầy
(523, 'Lá chuối', 2, 1, 1, 299, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nguyên liệu làm bánh giầy ngày Tết.', 0, 50, 0, '[]'),
(524, 'Bột gạo', 2, 1, 1, 299, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nguyên liệu làm bánh giầy ngày Tết.', 0, 50, 0, '[]'),

-- Bánh giầy thành phẩm (Sử dụng)
(525, 'Bánh Giầy', 2, 1, 1, 299, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Sử dụng: Hồi phục 100% HP/MP + Buff 15% Né tránh trong 30 phút', 1, 100, 1, '[12,15]'),

-- Nguyên liệu khác
(429, 'Bó lạt tre', 2, 1, 1, 246, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nguyên liệu thu thập từ nhiệm vụ Tết / Cướp Nồi Bánh.', 0, 50, 0, '[]'),
(430, 'Hũ gia vị', 2, 1, 1, 246, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Rơi từ Boss Lân Sư Tử / Hoạt động Cướp Nồi Bánh Bang.', 0, 50, 0, '[]'),

-- Hộp quà Tết
(355, 'Rương nguyên liệu tết', 7, 6, 1000, 186, 0, 1, 3, 0, 0, -1, '[[1,10]]', '[]', 'Mở ra nhận ngẫu nhiên đầy đủ các loại nguyên liệu nấu bánh.', 1, 100, 1, '[]'),
(356, 'Hộp trang phục', 7, 6, 1000, 187, 0, 1, 3, 0, 0, -1, '[[1,20]]', '[]', 'Mở ra chọn 1 trong các bộ thời trang Tết vĩnh viễn.', 1, 500, 1, '[]'),
(357, 'Bao lì xì Tân Niên', 7, 6, 1200, 188, 0, 1, 3, 0, 0, -1, '[[1,30]]', '[]', 'Mở ra nhận: Beri, Ruby, Đá khảm cấp 3-5, Vé đổi đồ thời trang.', 1, 200, 1, '[]'),

-- Hộp trang phục Tết (30 ngày)
(637, 'Hộp trang phục tết 1', 7, 6, 1000, 341, 0, 1, 3, 0, 0, -1, '[[1,20]]', '[]', 'Nhận bộ Trang phục Tết Tân Niên (30 ngày).', 1, 300, 1, '[]'),
(638, 'Hộp trang phục tết 2', 7, 6, 1000, 342, 0, 1, 3, 0, 0, -1, '[[1,20]]', '[]', 'Nhận bộ Trang phục Hổ Vằn / Thần Tài (30 ngày).', 1, 300, 1, '[]'),

-- May mắn & Trưng bày
(629, 'Cành Đào', 2, 1, 1, 339, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Trưng bày: Buff 30% EXP toàn khu vực trong 30 phút.', 1, 100, 1, '[68,30]'),
(635, 'Hoa mai', 2, 1, 1, 119, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Nộp tích điểm sự kiện cá nhân & bang hội.', 0, 10, 0, '[]'),

-- Chữ ghép Tết
(630, 'Chữ Cùng', 2, 1, 1, 340, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Mảnh ghép chữ. Ghép trọn bộ 5 chữ để đổi quà tự chọn.', 0, 100, 0, '[]'),
(631, 'Chữ Vui', 2, 1, 1, 340, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Mảnh ghép chữ. Ghép trọn bộ 5 chữ để đổi quà tự chọn.', 0, 100, 0, '[]'),
(632, 'Chữ Đón', 2, 1, 1, 340, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Mảnh ghép chữ. Ghép trọn bộ 5 chữ để đổi quà tự chọn.', 0, 100, 0, '[]'),
(633, 'Chữ Tết', 2, 1, 1, 340, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Mảnh ghép chữ. Ghép trọn bộ 5 chữ để đổi quà tự chọn.', 0, 100, 0, '[]'),
(634, 'Chữ 2023 (Tân Niên)', 2, 1, 1, 340, 0, 1, 1, 5000, 1, -1, '[[1,5]]', '[]', 'Chữ siêu hiếm. Rơi từ Boss Siêu Trùm, Boss Lân Sư Tử.', 0, 500, 0, '[]')

ON DUPLICATE KEY UPDATE 
    `name` = VALUES(`name`),
    `type` = VALUES(`type`),
    `icon` = VALUES(`icon`),
    `options` = VALUES(`options`),
    `description` = VALUES(`description`);


-- =============================================
-- PHẦN 2: BOSS TABLE - BOSS LÂN SƯ TỬ
-- =============================================

-- Boss Lân Sư Tử (Mob ID 153) - Thêm vào bảng boss
INSERT INTO `boss` (`id`, `name`, `mob_id`, `level`, `hp`, `maxhp`, `respawn`, `mapid`, `x`, `y`, `zone`, `type`, `action`, `time_appear`, `time_end`, `repeat`, `options`)
VALUES
(153, 'Lân Sư Tử Hoàng Kim', 153, 99, 100000000, 100000000, 3600000, -1, 0, 0, -1, 0, '', '', '', 1, '[]')
ON DUPLICATE KEY UPDATE 
    `name` = VALUES(`name`),
    `level` = VALUES(`level`),
    `hp` = VALUES(`hp`),
    `maxhp` = VALUES(`maxhp`);


-- =============================================
-- PHẦN 3: MAPS - ĐẤU TRƯỜNG MÙA XUÂN & ĐẢO ĐÀO HOA
-- =============================================

-- Map 2026: Đấu Trường Mùa Xuân (Clone từ Map ID 2)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    2026 AS `id`,
    'Đấu Trường Mùa Xuân' AS `name`,
    '[]' AS `mobs`,
    5 AS `maxzone`,
    30 AS `maxplayer`,
    '[[120, "Bảng Xếp Hạng", "Top Kill", "Xem danh sách các cao thủ đang dẫn đầu số mạng hạ gục tại Đấu Trường Mùa Xuân.", 450, 173, 1, 0, 0, 0, 0, [63, 1], 0, 0, []], [-202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Mùa Xuân để trở về Làng?", 250, 173, 1, 0, 0, 0, 0, [71, 2], 0, 0, []], [-7, " ", "Chuyển khu", "", 122, 173, 99, -1, 24, 24, 0, [5, 1], 0, 0, []]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[]' AS `vgos`,
    `data`,
    `MapBack`,
    0 AS `id_eff_map`,
    0 AS `level`,
    0 AS `typeChangeMap`,
    '[0,0]' AS `mPosMapTrain`,
    '' AS `strTimeChange`
FROM `maps` WHERE `id` = 2
ON DUPLICATE KEY UPDATE 
    `name` = VALUES(`name`),
    `mobs` = VALUES(`mobs`),
    `maxzone` = VALUES(`maxzone`),
    `maxplayer` = VALUES(`maxplayer`),
    `npcs` = VALUES(`npcs`);


-- Map 2027: Đảo Đào Hoa (Clone từ Map ID 8)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    2027 AS `id`,
    'Đảo Đào Hoa' AS `name`,
    '[]' AS `mobs`,
    5 AS `maxzone`,
    100 AS `maxplayer`,
    '[[-7, " ", "Chuyển khu", "", 500, 300, 99, -1, 24, 24, 0, [5, 1], 0, 0, []], [1001, "Trụ Trung Tâm", "Giữ Trụ", "Giữ Trụ Trung Tâm Đảo Đào Hoa để giành chiến thắng Guild War.", 500, 400, 1, 0, 0, 0, 0, [0, 0], 0, 0, []]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[]' AS `vgos`,
    `data`,
    `MapBack`,
    0 AS `id_eff_map`,
    0 AS `level`,
    0 AS `typeChangeMap`,
    '[0,0]' AS `mPosMapTrain`,
    '' AS `strTimeChange`
FROM `maps` WHERE `id` = 8
ON DUPLICATE KEY UPDATE 
    `name` = VALUES(`name`),
    `mobs` = VALUES(`mobs`),
    `maxzone` = VALUES(`maxzone`),
    `maxplayer` = VALUES(`maxplayer`),
    `npcs` = VALUES(`npcs`);


-- =============================================
-- LƯU Ý TRIỂN KHAI SERVER
-- =============================================
-- 1. Chạy lệnh SQL trên vào database chính (htth.sql hoặc import trực tiếp)
-- 2. Copy data/map/2/* sang data/map/2026/* (để load tile map)
-- 3. Copy data/map/8/* sang data/map/2027/* (để load tile map)
-- 4. Khởi động lại server để load maps mới
