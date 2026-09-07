-- ==============================================================================
-- SCRIPT CẬP NHẬT CHỈ SỐ BỘ KỸ NĂNG TRÁI NIKYU NIKYU NO MI THEO YÊU CẦU
-- Database: full_db_htth
-- Skill IDs: 1075, 1076, 1077 (Skill Index: 911, 912, 913)
-- ==============================================================================

USE full_db_htth;

SET SQL_SAFE_UPDATES = 0;

-- 1. Skill: Đại Hùng Chưởng (id = 1075, id_index = 911)
-- Yêu cầu: Bỏ Xuyên giáp (id 13), Bỏ S.t chí mạng (id 11), Thêm 30% gây Hoảng loạn trong 4s (id 16, 300, 40)
UPDATE `skill`
SET 
    `option` = '[[1, 400], [10, 300], [28, 16], [29, 300], [30, 40]]',
    `EffSpec` = '[16, 300, 40]',
    `info` = 'Nén khối lượng không khí khổng lồ lại thành quả cầu xung kích tàn phá diện rộng, tăng 40% Tấn công, 30% Chí mạng và 30% tỉ lệ gây Hoảng loạn đối thủ trong 4 giây'
WHERE `id` = 1075 OR `id_index` = 911;

-- 2. Skill: Đệm Thịt Hộ Thể (id = 1076, id_index = 912)
-- Yêu cầu: Bỏ Tăng HP (id 17)
UPDATE `skill`
SET 
    `option` = '[[12, 350], [14, 350], [53, 200], [31, 1], [32, 250]]',
    `info` = 'Tạo khiên đệm thịt phản chấn mọi sát thương: Tăng 35% Né tránh, 35% Phản đòn và 20% Miễn thương trong 25 giây'
WHERE `id` = 1076 OR `id_index` = 912;

-- 3. Skill: Phản Chấn Đệm Thịt (id = 1077, id_index = 913)
-- Yêu cầu: Bỏ Né tránh (id 12), Bỏ Tăng HP (id 17)
UPDATE `skill`
SET 
    `option` = '[[1, 300], [14, 250], [53, 150]]',
    `info` = 'Nội tại Phản Chấn Đệm Thịt: Cơ thể đệm thịt phản chấn lại mọi sát thương, tăng vĩnh viễn 30% Tấn công, 25% Phản đòn và 15% Miễn thương'
WHERE `id` = 1077 OR `id_index` = 913;

SET SQL_SAFE_UPDATES = 1;

-- Kiểm tra lại kết quả cập nhật
SELECT `id`, `id_index`, `name`, `option`, `EffSpec`, `info` 
FROM `skill` 
WHERE `id_index` IN (910, 911, 912, 913);
