-- ==============================================================================
-- BỘ SQL CẬP NHẬT TRÁI ÁC QUỶ NIKYU NIKYU NO MI (KUMA) - CHUẨN CÂN BẰNG
-- ==============================================================================
-- • Item ID: 1015 (Icon: 190, indexInfoPotion: 457)
-- • Item Info ID: 457 (Mô tả đầy đủ 4 kỹ năng)
-- • 4 Kỹ năng (Icon 4417..4420):
--   - 1074 / 910: Áp Lực Pháo (Đánh đơn, 15s, mana 45, damage 3800) -> typeSkill = 1
--   - 1075 / 911: Đại Hùng Chưởng (Diện rộng 4 mục tiêu, 18s, mana 60, damage 2200) -> typeSkill = 1
--   - 1076 / 912: Đệm Thịt Hộ Thể (Buff khiên phản đòn 25s, 40s, mana 90) -> typeSkill = 2
--   - 1077 / 913: Phản Chấn Đệm Thịt (Nội tại vĩnh viễn: né, phản, miễn thương, HP, công) -> typeSkill = 3
-- ==============================================================================

SET SQL_SAFE_UPDATES = 0;

-- 1. XÓA SẠCH DỮ LIỆU CŨ TRÁNH TRÙNG LẶP KHÓA CHÍNH (PK)
DELETE FROM `item4` WHERE `id` = 1015;
DELETE FROM `item4_info` WHERE `id` IN (457, 1015);
DELETE FROM `skill` WHERE `id` IN (1074, 1075, 1076, 1077, 4332, 4333, 4334) OR `id_index` IN (910, 911, 912, 913);

-- 2. THÊM ITEM TRÁI ÁC QUỶ (Icon 190, indexInfoPotion 457) VÀ MÔ TẢ ĐẦY ĐỦ 4 KỸ NĂNG (ID 457)
INSERT INTO `item4` (`id`, `name`, `icon`, `indexInfoPotion`, `price`, `priceruby`, `istrade`, `hpmpother`, `timedelay`, `value`, `timeactive`, `nameuse`)
VALUES (1015, 'Trái Nikyu Nikyu', 190, 457, 10, 0, 1, 7, 0, 0, 0, 'Ăn');

INSERT INTO `item4_info` (`id`, `info`)
VALUES (457, 'Khi ăn Trái Ác Quỷ Nikyu Nikyu no Mi (Kuma), bạn sẽ sở hữu sức mạnh đệm thịt: Áp Lực Pháo, Đại Hùng Chưởng, Đệm Thịt Hộ Thể và Phản Chấn Đệm Thịt.');

-- 3. THÊM BỘ 4 KỸ NĂNG CÂN BẰNG CHUẨN VÀO BẢNG SKILL

-- [Skill 1 - Đánh Đơn]: Áp Lực Pháo (Index 910, Icon 4417, CD 15s, Mana 45, Damage 3800)
INSERT INTO `skill` (
    `id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, 
    `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, 
    `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, 
    `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, 
    `LvDevilSkill`, `phanTramDevilSkill`
) VALUES (
    1074, 910, 2060, 4417, 1, 0, 
    'Áp Lực Pháo', 910, 120, 120, 1, 
    3800, 45, 15000, 1, 
    'Đẩy không khí với tốc độ ánh sáng, bắn ra luồng chưởng sóng áp lực cực mạnh. 650% sát thương của chiêu thức Quả đấm tốc độ', 
    1, 0, 1, 
    '[[1,350],[13,300],[28,15],[29,250],[30,30]]', 
    '[5,50,20]', 0, 0
);

-- [Skill 2 - Đánh Diện Rộng]: Đại Hùng Chưởng (Index 911, Icon 4418, CD 18s, Mana 60, Target 4, Damage 2200)
INSERT INTO `skill` (
    `id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, 
    `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, 
    `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, 
    `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, 
    `LvDevilSkill`, `phanTramDevilSkill`
) VALUES (
    1075, 911, 2061, 4418, 1, 0, 
    'Đại Hùng Chưởng', 911, 140, 140, 4, 
    2200, 60, 18000, 4, 
    'Nén khối lượng không khí khổng lồ lại thành quả cầu xung kích tàn phá diện rộng. 450% sát thương của chiêu thức Quả đấm tốc độ', 
    1, 0, 1, 
    '[[1,400],[10,300],[11,300],[13,350]]', 
    '[1,80,30]', 0, 0
);

-- [Skill 3 - Buff Chủ Động]: Đệm Thịt Hộ Thể (Index 912, Icon 4419, CD 40s, Mana 90, Buff 25s)
INSERT INTO `skill` (
    `id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, 
    `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, 
    `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, 
    `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, 
    `LvDevilSkill`, `phanTramDevilSkill`
) VALUES (
    1076, 912, 2062, 4419, 2, 1, 
    'Đệm Thịt Hộ Thể', 912, 120, 120, 1, 
    0, 90, 40000, 0, 
    'Tạo khiên đệm thịt phản chấn mọi sát thương, tăng né tránh, miễn thương và hồi phục sinh lực.', 
    1, 0, 1, 
    '[[12,350],[14,350],[53,200],[17,300],[31,1],[32,250]]', 
    '[0,-1,-1]', 0, 0
);

-- [Skill 4 - Chiêu Nội Tại]: Phản Chấn Đệm Thịt (Index 913, Icon 4420, Nội tại vĩnh viễn)
INSERT INTO `skill` (
    `id`, `id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, 
    `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, 
    `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, 
    `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, 
    `LvDevilSkill`, `phanTramDevilSkill`
) VALUES (
    1077, 913, 2063, 4420, 3, 0, 
    'Phản Chấn Đệm Thịt', 0, 0, 0, 0, 
    0, 0, 0, 0, 
    'Cơ thể đệm thịt phản chấn lại mọi sát thương và áp lực, gia tăng mạnh mẽ né tránh, phản đòn, miễn thương, máu và sức mạnh tấn công.', 
    1, 0, 1, 
    '[[1,300],[12,250],[14,250],[17,250],[53,150]]', 
    '[0,-1,-1]', 0, 0
);

-- 4. CẬP NHẬT THỜI TRANG KUMA (ID 238): Bỏ [10,120] khỏi op gốc, chỉ kích hoạt +10% Chí mạng khi mang + ăn Trái Nikyu
UPDATE `fashiontemplate` 
SET `op` = '[[63,100],[14,100],[12,100]]', 
    `info` = 'Trang phục Kuma\n+10% Giảm miễn thương\n+10% Phản đòn\n+10% Né đòn\n-Tăng 100% sức tấn công bản thân.\n+10% Chí mạng khi kết hợp Trái Nikyu Nikyu\nHạn sử dụng vĩnh viễn'
WHERE `id` = 238;

SET SQL_SAFE_UPDATES = 1;
