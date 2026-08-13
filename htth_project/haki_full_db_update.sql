-- TẬP LỆNH SQL THÊM KỸ NĂNG HAKI VÀO DATABASE
-- Mỗi kỹ năng Haki sẽ bao gồm 2 record: 1 record Lv_RQ = -1 (chưa học) và 1 record Lv_RQ = 1 (cấp độ 1).

-- 1. Haki Quan Sát (ID_Index: 900, ID_2: 900)
-- Chưa học (Lv_RQ = -1)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng cường khả năng cảm nhận, giúp né tránh các đòn tấn công và tăng tỉ lệ chí mạng.', -1, 0, 0, '[[12,30],[10,15]]', '[0,0,0]', 0, 0);

-- Cấp 1 (Lv_RQ = 1)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng cường khả năng cảm nhận, giúp né tránh các đòn tấn công và tăng tỉ lệ chí mạng.', 1, 0, 0, '[[12,30],[10,15]]', '[0,0,0]', 0, 0);


-- 2. Haki Vũ Trang (ID_Index: 901, ID_2: 901)
-- Chưa học (Lv_RQ = -1)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Bao bọc cơ thể bằng một lớp giáp vô hình, tăng cường phòng thủ và sức tấn công.', -1, 0, 0, '[[4,40],[1,25]]', '[0,0,0]', 0, 0);

-- Cấp 1 (Lv_RQ = 1)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Bao bọc cơ thể bằng một lớp giáp vô hình, tăng cường phòng thủ và sức tấn công.', 1, 0, 0, '[[4,40],[1,25]]', '[0,0,0]', 0, 0);


-- 3. Haki Bá Vương (ID_Index: 902, ID_2: 902)
-- Chưa học (Lv_RQ = -1)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 10000, 350, 90000, 1, 'Bộc phát sức mạnh bá vương, tấn công áp đảo tinh thần gây sát thương lớn và làm choáng kẻ thù xung quanh.', -1, 0, 0, '[]', '[1,100,5000]', 0, 0);

-- Cấp 1 (Lv_RQ = 1)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`)
VALUES (902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 10000, 350, 90000, 1, 'Bộc phát sức mạnh bá vương, tấn công áp đảo tinh thần gây sát thương lớn và làm choáng kẻ thù xung quanh.', 1, 0, 0, '[]', '[1,100,5000]', 0, 0);
-- SCRIPT THÊM BẢN ĐỒ LUYỆN HAKI VÀ 10 CẤP KỸ NĂNG HAKI

-- 1. THÊM BẢN ĐỒ LUYỆN HAKI (ID = 2000)
-- Dùng REPLACE INTO để đè lên record map 2000 bị sai định dạng npcs trước đó
REPLACE INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`) VALUES
(2000, 'Luyện Haki', '[[0,336,168],[0,360,264],[0,480,192]]', 10, 15, '[[-38, "Truong Lang", "Khu luyen Haki", "Chao mung den khu luyen Haki! Hay tieu diet quai vat de tang cuong suc manh Haki cua ban.", 200, 200, 1, 0, 0, 0, 0, [1, 2], 0, 0, []]]', '[]', 0, 1, 0, '[]', '[]', '[]', 0, 1, 0, '[]', '');


-- 2. THÊM KỸ NĂNG HAKI TỪ CẤP 2 ĐẾN 10

-- HAKI QUAN SÁT (ID 900)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`) VALUES
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 35% Né Tránh, 17% Chí Mạng', 2, 0, 0, '[[12,35],[10,17]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 40% Né Tránh, 20% Chí Mạng', 3, 0, 0, '[[12,40],[10,20]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 45% Né Tránh, 22% Chí Mạng', 4, 0, 0, '[[12,45],[10,22]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 50% Né Tránh, 25% Chí Mạng', 5, 0, 0, '[[12,50],[10,25]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 55% Né Tránh, 27% Chí Mạng', 6, 0, 0, '[[12,55],[10,27]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 60% Né Tránh, 30% Chí Mạng', 7, 0, 0, '[[12,60],[10,30]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 65% Né Tránh, 33% Chí Mạng', 8, 0, 0, '[[12,65],[10,33]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 70% Né Tránh, 36% Chí Mạng', 9, 0, 0, '[[12,70],[10,36]]', '[0,0,0]', 0, 0),
(900, 900, 4413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 75% Né Tránh, 40% Chí Mạng', 10, 0, 0, '[[12,75],[10,40]]', '[0,0,0]', 0, 0);

-- HAKI VŨ TRANG (ID 901)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`) VALUES
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 45% Phòng Thủ, 27% Tấn Công', 2, 0, 0, '[[4,45],[1,27]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 50% Phòng Thủ, 30% Tấn Công', 3, 0, 0, '[[4,50],[1,30]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 55% Phòng Thủ, 33% Tấn Công', 4, 0, 0, '[[4,55],[1,33]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 60% Phòng Thủ, 36% Tấn Công', 5, 0, 0, '[[4,60],[1,36]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 65% Phòng Thủ, 39% Tấn Công', 6, 0, 0, '[[4,65],[1,39]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 70% Phòng Thủ, 42% Tấn Công', 7, 0, 0, '[[4,70],[1,42]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 75% Phòng Thủ, 45% Tấn Công', 8, 0, 0, '[[4,75],[1,45]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 80% Phòng Thủ, 48% Tấn Công', 9, 0, 0, '[[4,80],[1,48]]', '[0,0,0]', 0, 0),
(901, 901, 4414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 85% Phòng Thủ, 50% Tấn Công', 10, 0, 0, '[[4,85],[1,50]]', '[0,0,0]', 0, 0);


-- HAKI BÁ VƯƠNG (ID 902)
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`) VALUES
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 12000, 350, 90000, 1, 'Choáng 5.5s, Sát thương 12000', 2, 0, 0, '[]', '[1,100,5500]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 14000, 350, 90000, 1, 'Choáng 6s, Sát thương 14000', 3, 0, 0, '[]', '[1,100,6000]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 16000, 350, 90000, 1, 'Choáng 6.5s, Sát thương 16000', 4, 0, 0, '[]', '[1,100,6500]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 18000, 350, 90000, 1, 'Choáng 7s, Sát thương 18000', 5, 0, 0, '[]', '[1,100,7000]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 20000, 350, 90000, 1, 'Choáng 7.5s, Sát thương 20000', 6, 0, 0, '[]', '[1,100,7500]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 22000, 350, 90000, 1, 'Choáng 8s, Sát thương 22000', 7, 0, 0, '[]', '[1,100,8000]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 24000, 350, 90000, 1, 'Choáng 8.5s, Sát thương 24000', 8, 0, 0, '[]', '[1,100,8500]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 27000, 350, 90000, 1, 'Choáng 9s, Sát thương 27000', 9, 0, 0, '[]', '[1,100,9000]', 0, 0),
(902, 902, 4415, 1, 0, 'Haki Bá Vương', 300, 600, 600, 10, 30000, 350, 90000, 1, 'Choáng 10s, Sát thương 30000', 10, 0, 0, '[]', '[1,100,10000]', 0, 0);
