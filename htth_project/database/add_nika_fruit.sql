-- ==============================================================================
-- SCRIPT THÊM & CẬP NHẬT TRÁI ÁC QUỶ NIKA (HITO HITO NO MI: MODEL NIKA / LUFFY GEAR 5)
-- Item ID: 1017 (Icon 683 -> File 2683.png)
-- Skill IDs: 1082, 1083, 1084, 1085 (Skill Index: 918, 919, 920, 921)
-- Skill Icons: 439, 438, 440, 441 (Icons File: 4439, 4438, 4440, 4441.png)
-- ==============================================================================

USE full_db_htth;

SET SQL_SAFE_UPDATES = 0;

-- 0. XÓA DỮ LIỆU CŨ NẾU CÓ
DELETE FROM `item4` WHERE `id` = 1017 OR `name` = 'Trái Nika';
DELETE FROM `item4_info` WHERE `id` = 1017;
DELETE FROM `skill` WHERE `id` IN (1082, 1083, 1084, 1085) OR `id_index` IN (918, 919, 920, 921);

-- 1. THÊM ITEM TRÁI NIKA VÀO ITEM4
INSERT INTO `item4` (`id`, `name`, `icon`, `indexInfoPotion`, `price`, `priceruby`, `istrade`, `hpmpother`, `timedelay`, `value`, `timeactive`, `nameuse`)
VALUES (1017, 'Trái Nika', 683, 459, 10, 0, 1, 7, 0, 0, 0, 'Ăn');

-- 2. THÊM MÔ TẢ ITEM VÀO ITEM4_INFO
INSERT INTO `item4_info` (`id`, `info`)
VALUES (1017, 'Khi ăn Trái Ác Quỷ Nika (Hito Hito no Mi: Model Nika - Thần Mặt Trời), bạn sẽ đánh thức sức mạnh tự do vô hạn của Chiến Binh Giải Phóng với các tuyệt kỹ: Cao Su Xà Quyền, Cự Quyền Nika (Bajrang Gun), Thức Tỉnh Nika và Tiếng Trống Giải Phóng.');

-- 3. THÊM BỘ 4 KỸ NĂNG VÀO BẢNG SKILL

-- 3.1. Skill 1: Cao Su Xà Quyền (Snakeman Hydra / Culverin)
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1082, 918, 2068, 439, 1, 0, 'Cao Su Xà Quyền', 918, 180, 5, 160, 75, 55, 15000, 1, 'Tung vô số cú đấm co giãn tốc độ cực cao chuyển hướng liên tục như mãng xà khổng lồ tấn công dồn dập vào nhiều kẻ địch xung quanh, gây 380% sát thương của chiêu Quả đấm tốc độ, bạo kích và thiêu đốt Haki', 1, 1, '[[1, 450], [10, 350], [11, 200], [13, 400], [28, 5], [29, 350], [30, 20]]', '[5, 350, 20]');

-- 3.2. Skill 2: Thần Nika Cự Quyền (Bajrang Gun / Gigant Pistol)
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1083, 919, 2069, 438, 1, 0, 'Thần Nika Cự Quyền', 919, 200, 6, 180, 85, 70, 22000, 1, 'Hóa khổng lồ nắm đấm bao bọc Haki Bá Vương đen rực giáng sấm sét từ tầng mây xuống mặt đất, tạo chấn động hủy diệt gây 500% sát thương của chiêu Quả đấm tốc độ, làm choáng 3 giây và xuyên 50% giáp', 1, 1, '[[1, 550], [10, 450], [13, 500], [57, 250], [28, 9], [29, 500], [30, 20]]', '[9, 500, 20]');

-- 3.3. Skill 3: Thức Tỉnh Nika (Sun God Awakening)
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1084, 920, 2080, 440, 2, 1, 'Thức Tỉnh Nika', 920, 120, 1, 120, 0, 80, 35000, 0, 'Thức tỉnh trạng thái Nika Thần Mặt Trời: Tỏa ra uy áp Bá Vương hủy diệt làm nứt toác mặt đất, hồi phục 25% HP, tăng 40% sát thương, 30% chí mạng, 35% phòng thủ và miễn thương trong 25 giây', 1, 1, '[[1, 400], [4, 400], [10, 300], [12, 350], [53, 250], [31, 1], [32, 300]]', '[0, -1, -1]');

-- 3.4. Skill 4: Tiếng Trống Giải Phóng (Drums of Liberation)
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1085, 921, 2071, 441, 3, 0, 'Tiếng Trống Giải Phóng', 0, 0, 0, 0, 0, 0, 0, 0, 'Nội tại Tiếng Trống Giải Phóng: Nhịp tim tự do vang dội giúp cơ thể liên tục hồi phục sinh lực, tăng vĩnh viễn máu tối đa, tốc độ chạy, xuyên giáp và kháng mọi hiệu ứng tiêu cực', 1, 1, '[[2, 500], [1, 350], [13, 400], [53, 200], [71, 300]]', '[0, -1, -1]');

SET SQL_SAFE_UPDATES = 1;
