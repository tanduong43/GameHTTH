-- ==============================================================================
-- SCRIPT CẬP NHẬT CHỈ SỐ BỘ KỸ NĂNG TRÁI OPE OPE NO MI THEO YÊU CẦU
-- Database: full_db_htth
-- Skill IDs: 1078, 1079, 1080, 1081 (Skill Index: 914, 915, 916, 917)
-- ==============================================================================

USE full_db_htth;

SET SQL_SAFE_UPDATES = 0;

-- 1. Skill 1: Trảm Không Gian (id = 1078, id_index = 914)
-- Yêu cầu: Đổi Xuyên giáp & Tăng tấn công thành 40% Chí mạng (id 10), 36% Sát thương chí mạng (id 11)
UPDATE `skill`
SET 
    `option` = '[[10, 400], [11, 360], [28, 4], [29, 300], [30, 20]]',
    `info` = 'Mở trường phẫu thuật Room, chém kiếm khí Kikoku phân tách không gian, gây 350% sát thương của chiêu Quả đấm tốc độ, tăng 40% tỉ lệ chí mạng và 36% sát thương chí mạng lên 4 mục tiêu'
WHERE `id` = 1078 OR `id_index` = 914;

-- 2. Skill 2: Dao Phóng Xạ Gamma (id = 1079, id_index = 915)
-- Yêu cầu: Đổi thành Tăng tấn công 40% (id 1), Xuyên giáp 35% (id 13)
UPDATE `skill`
SET 
    `option` = '[[1, 400], [13, 350], [28, 9], [29, 500], [30, 20]]',
    `info` = 'Tạo quả cầu Room giam giữ mục tiêu và phóng dao plasma Gamma nghiền nát, tăng 40% sức tấn công và xuyên 35% giáp của mục tiêu'
WHERE `id` = 1079 OR `id_index` = 915;

-- 3. Skill 3: Khiên Phẫu Thuật Curtain (id = 1080, id_index = 916)
-- Yêu cầu: Đổi chỉ số thành 25% Miễn thương (id 53), 40% Né tránh (id 12), 50% Chí mạng (id 10)
UPDATE `skill`
SET 
    `option` = '[[53, 250], [12, 400], [10, 500], [31, 1], [32, 250]]',
    `info` = 'Dựng màn chắn không gian Curtain bảo hộ bản thân: Tăng 25% miễn thương, 40% né tránh và 50% chí mạng trong 25 giây'
WHERE `id` = 1080 OR `id_index` = 916;

-- 4. Skill 4: Bác Sĩ Tử Thần (id = 1081, id_index = 917)
-- Yêu cầu: Giữ nguyên Phản đòn 30% (id 14), Né tránh 30% (id 12), S.t chí mạng 30% (id 11)
UPDATE `skill`
SET 
    `option` = '[[14, 300], [12, 300], [11, 300]]',
    `info` = 'Nội tại Bác Sĩ Tử Thần: Tri thức y học tuyệt đỉnh giúp Law tìm ra mọi điểm yếu của kẻ địch, tăng vĩnh viễn 30% Phản đòn, 30% Né tránh và 30% Sát thương chí mạng'
WHERE `id` = 1081 OR `id_index` = 917;

SET SQL_SAFE_UPDATES = 1;

-- Kiểm tra lại kết quả cập nhật
SELECT `id`, `id_index`, `name`, `option`, `info` 
FROM `skill` 
WHERE `id_index` IN (914, 915, 916, 917);
