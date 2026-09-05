-- ==============================================================================
-- SCRIPT THÊM & CẬP NHẬT TRÁI ÁC QUỶ OPE OPE NO MI (TRAFALGAR D. WATER LAW)
-- CHỈ SỐ CHUẨN NGUYÊN TÁC ONE PIECE (PHẪU THUẬT, XUYÊN GIÁP, GAMMA KNIFE, CURTAIN)
-- Item ID: 1016 (Icon 191 -> File 2191.png)
-- Skill IDs: 1078, 1079, 1080, 1081 (Skill Index: 914, 915, 916, 917)
-- Skill Icons: 421, 422, 423, 424 (Icons File: 4421, 4422, 4423, 4424.png)
-- ==============================================================================

USE full_db_htth;

SET SQL_SAFE_UPDATES = 0;

-- 0. XÓA CÁC DỮ LIỆU CŨ NẾU CÓ ĐỂ TRÁNH TRÙNG LẶP
DELETE FROM `item4` WHERE `id` = 1016 OR `name` = 'Trái Ope Ope';
DELETE FROM `item4_info` WHERE `id` = 1016;
DELETE FROM `skill` WHERE `id` IN (1078, 1079, 1080, 1081) OR `id_index` IN (914, 915, 916, 917);

-- 1. THÊM ITEM TRÁI OPE OPE VÀO ITEM4
INSERT INTO `item4` (`id`, `name`, `icon`, `indexInfoPotion`, `price`, `priceruby`, `istrade`, `hpmpother`, `timedelay`, `value`, `timeactive`, `nameuse`)
VALUES (1016, 'Trái Ope Ope', 191, 0, 10, 0, 1, 7, 0, 0, 0, 'Ăn');

-- 2. THÊM MÔ TẢ ITEM VÀO ITEM4_INFO
INSERT INTO `item4_info` (`id`, `info`)
VALUES (1016, 'Khi ăn Trái Ác Quỷ Ope Ope (Phẫu Thuật), bạn sẽ thức tỉnh năng lực thao túng không gian Room thần thánh của Bác Sĩ Tử Thần Trafalgar Law với các tuyệt kỹ: Trảm Không Gian, Dao Phóng Xạ Gamma, Khiên Phẫu Thuật Curtain và Bác Sĩ Tử Thần.');

-- 3. THÊM BỘ 4 KỸ NĂNG VÀO BẢNG SKILL (CHUẨN NGUYÊN TÁC ONE PIECE)

-- 3.1. Skill 1: Room - Trảm Không Gian (Amputate / Shambles Slash)
-- Nguyên tác: Cắt rời không gian bỏ qua giáp và phòng thủ trong Room, độ chính xác tuyệt đối
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1078, 914, 2064, 421, 1, 0, 'Trảm Không Gian', 914, 160, 4, 140, 75, 55, 18000, 1, 'Mở trường phẫu thuật Room, chém kiếm khí Kikoku phân tách không gian, gây 350% sát thương của chiêu Quả đấm tốc độ, xuyên 45% giáp và gây sát thương chuẩn lên 4 mục tiêu', 1, 1, '[[1, 420], [13, 450], [57, 200], [76, 250], [28, 4], [29, 300], [30, 20]]', '[4, 300, 20]');

-- 3.2. Skill 2: Dao Phóng Xạ Gamma (Gamma Knife - Internal Organs Destruction)
-- Nguyên tác: Đâm dao điện Plasma Gamma xuyên thẳng nội tạng, bạo kích cực hạn và gây sát thương theo % máu
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1079, 915, 2065, 422, 1, 0, 'Dao Phóng Xạ Gamma', 915, 140, 1, 0, 75, 65, 20000, 1, 'Tạo dao năng lượng Plasma Gamma đâm xuyên nội tạng mục tiêu, gây 420% sát thương bạo kích của chiêu Quả đấm tốc độ, bỏ qua hoàn toàn giáp và gây thêm sát thương theo % máu đối thủ', 1, 1, '[[1, 480], [10, 400], [11, 250], [13, 500], [48, 150], [28, 9], [29, 500], [30, 20]]', '[9, 500, 20]');

-- 3.3. Skill 3: Curtain & Scan (Khiên Phẫu Thuật & Hoán Vị Shambles)
-- Nguyên tác: Màn chắn Curtain chặn đòn hủy diệt, hoán vị Shambles né đòn và hồi phục sinh mệnh y tế
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1080, 916, 2066, 423, 2, 1, 'Khiên Phẫu Thuật', 916, 120, 1, 120, 0, 90, 38000, 0, 'Dựng màn chắn không gian Curtain và trận pháp Trái Tim Ope dưới chân: Hồi phục 30% HP, tăng mạnh né tránh (Shambles), tăng giáp, kháng hiệu ứng và miễn thương trong 25 giây', 1, 1, '[[4, 400], [12, 350], [53, 250], [71, 200], [31, 1], [32, 250]]', '[0, -1, -1]');

-- 3.4. Skill 4: Bác Sĩ Tử Thần (Surgeon of Death - Tri Thức Phẫu Thuật Gia)
-- Nguyên tác: Hiểu rõ mọi tử huyệt sinh học của đối phương, tăng vĩnh viễn tấn công, chí mạng, xuyên kháng
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `nTarget`, `rangeLan`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `typeDevil`, `option`, `EffSpec`)
VALUES (1081, 917, 2067, 424, 3, 0, 'Bác Sĩ Tử Thần', 0, 0, 0, 0, 0, 0, 0, 0, 'Nội tại Bác Sĩ Tử Thần: Tri thức y học tuyệt đỉnh giúp Law tìm ra mọi điểm yếu của kẻ địch, tăng vĩnh viễn tấn công, tỉ lệ và sát thương chí mạng, xuyên giáp và miễn thương', 1, 1, '[[1, 380], [2, 380], [10, 300], [11, 200], [13, 350], [53, 150]]', '[0, -1, -1]');

SET SQL_SAFE_UPDATES = 1;
