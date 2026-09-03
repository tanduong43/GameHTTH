-- ==========================================================
-- SQL CẬP NHẬT TRÁI ÁC QUỶ NIKYU NIKYU NO MI (KUMA) CHO GAME HTTH
-- • Item ID: 1015 (Icon: 190, indexInfoPotion: 457)
-- • Skill PK ID: 1074, 1075, 1076, 1077 (Icon: 4417, 4418, 4419, 4420)
-- • Effect ID & Skill Index: 910..913 (Vị trí effect tiếp theo sau Haki 902)
--   - 910: Áp Lực Pháo (Active 1)
--   - 911: Đại Hùng Chưởng (Active 1)
--   - 912: Đệm Thịt Hộ Thể (Active Buff 2)
--   - 913: Phản Chấn Đệm Thịt (Passive / Nội tại 3)
-- ==========================================================

-- Tắt Safe Updates để thực thi lệnh DELETE mượt mà không báo lỗi 1175
SET SQL_SAFE_UPDATES = 0;

-- 0. XÓA CÁC DỮ LIỆU CŨ ĐÃ THÊM TRƯỚC ĐÓ ĐỂ TRÁNH TRÙNG LẶP
DELETE FROM `item4` WHERE `id` = 1015;
DELETE FROM `item4_info` WHERE `id` IN (457, 1015);
DELETE FROM `skill` WHERE `id` IN (1074, 1075, 1076, 1077, 4332, 4333, 4334) OR `id_index` IN (910, 911, 912, 913);

-- 1. Thêm Item Trái Nikyu Nikyu vào item4 (Item ID 1015, Icon ID 190, indexInfoPotion 457)
INSERT INTO `item4` (`id`, `name`, `icon`, `indexInfoPotion`, `price`, `priceruby`, `istrade`, `hpmpother`, `timedelay`, `value`, `timeactive`, `nameuse`)
VALUES (1015, 'Trái Nikyu Nikyu', 190, 457, 10, 0, 1, 7, 0, 0, 0, 'Ăn');

-- 2. Thêm mô tả Item vào item4_info (ID 457)
INSERT INTO `item4_info` (`id`, `info`)
VALUES (457, 'Khi ăn Trái Ác Quỷ Nikyu Nikyu no Mi (Kuma), bạn sẽ sở hữu sức mạnh đệm thịt: Áp Lực Pháo, Đại Hùng Chưởng, Đệm Thịt Hộ Thể và Phản Chấn Đệm Thịt.');

-- 3. Thêm bộ 4 Kỹ Năng của Trái Nikyu Nikyu no Mi vào bảng skill (PK 1074..1077)
-- Skill 1: Áp Lực Pháo (Pad Ho) - PK 1074, Index 910, Eff 910, Icon 4417
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (1074, 910, 2060, 4417, 1, 0, 'Áp Lực Pháo', 910, 120, 120, 1, 3800, 45, 15000, 1, 'Đẩy không khí với tốc độ ánh sáng, bắn ra luồng chưởng sóng áp lực cực mạnh. 650% sát thương của chiêu thức Quả đấm tốc độ', 1, 0, 1, '[[1,350],[13,300],[28,15],[29,250],[30,30]]', '[5,50,20]', 0, 0);

-- Skill 2: Đại Hùng Chưởng (Ursus Shock) - PK 1075, Index 911, Eff 911, Icon 4418
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (1075, 911, 2061, 4418, 1, 0, 'Đại Hùng Chưởng', 911, 140, 140, 4, 2200, 60, 18000, 4, 'Nén khối lượng không khí khổng lồ lại thành quả cầu xung kích tàn phá diện rộng. 450% sát thương của chiêu thức Quả đấm tốc độ', 1, 0, 1, '[[1,400],[10,300],[11,300],[13,350]]', '[1,80,30]', 0, 0);

-- Skill 3: Đệm Thịt Hộ Thể (Nikyu Defense & Repel Pain) - PK 1076, Index 912, Eff 912, Icon 4419
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (1076, 912, 2062, 4419, 2, 1, 'Đệm Thịt Hộ Thể', 912, 120, 120, 1, 0, 90, 40000, 0, 'Tạo khiên đệm thịt phản chấn mọi sát thương, tăng né tránh, miễn thương và hồi phục sinh lực.', 1, 0, 1, '[[12,350],[14,350],[53,200],[17,300],[31,1],[32,250]]', '[0,-1,-1]', 0, 0);

-- Skill 4: Phản Chấn Đệm Thịt (Paw Rebound & Deflection) - PK 1077, Index 913, Eff 0, Icon 4420 (NỘI TẠI - PASSIVE)
INSERT INTO `skill` (`id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (1077, 913, 2063, 4420, 3, 0, 'Phản Chấn Đệm Thịt', 0, 0, 0, 0, 0, 0, 0, 0, 'Cơ thể đệm thịt phản chấn lại mọi sát thương và áp lực, gia tăng mạnh mẽ né tránh, phản đòn, miễn thương, máu và sức mạnh tấn công.', 1, 0, 1, '[[1,300],[12,250],[14,250],[17,250],[53,150]]', '[0,-1,-1]', 0, 0);

-- Bật lại Safe Updates
SET SQL_SAFE_UPDATES = 1;
