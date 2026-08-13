-- FIX SKILL HAKI - Chạy file này trong MySQL database
-- Xóa skill 900, 901, 902 cũ (nếu có) và insert lại đúng

DELETE FROM `skill` WHERE `id_index` IN (900, 901, 902);

-- icon: 413 = Haki Quan Sat, 414 = Haki Vu Trang, 415 = Haki Ba Vuong
-- typeSkill: 2 = buff, 1 = attack
-- typeBuff: 1 = buff skill, 0 = attack skill
-- EffSpec format: [idEff, perEff, timeEff] - dùng -1 nếu không có hiệu ứng
-- id_index = id_2 = 900/901/902 (haki dùng cùng index cho cả indexSkillInServer và ID)

-- Insert skill Haki Quan Sát (900) các cấp -1 đến 10
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`) VALUES
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng cường khả năng cảm nhận, giúp né tránh các đòn tấn công và tăng tỉ lệ chí mạng.', -1, 0, 0, '[[12,30],[10,15]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 35% Né Tránh, 17% Chí Mạng', 1, 0, 0, '[[12,35],[10,17]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 40% Né Tránh, 20% Chí Mạng', 2, 0, 0, '[[12,40],[10,20]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 45% Né Tránh, 22% Chí Mạng', 3, 0, 0, '[[12,45],[10,22]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 50% Né Tránh, 25% Chí Mạng', 4, 0, 0, '[[12,50],[10,25]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 55% Né Tránh, 27% Chí Mạng', 5, 0, 0, '[[12,55],[10,27]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 60% Né Tránh, 30% Chí Mạng', 6, 0, 0, '[[12,60],[10,30]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 65% Né Tránh, 33% Chí Mạng', 7, 0, 0, '[[12,65],[10,33]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 70% Né Tránh, 36% Chí Mạng', 8, 0, 0, '[[12,70],[10,36]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 75% Né Tránh, 40% Chí Mạng', 9, 0, 0, '[[12,75],[10,40]]', '[0,-1,-1]', 0, 0),
(900, 900, 413, 2, 1, 'Haki Quan Sát', 0, 0, 0, 1, 0, 150, 60000, 1, 'Tăng 80% Né Tránh, 45% Chí Mạng', 10, 0, 0, '[[12,80],[10,45]]', '[0,-1,-1]', 0, 0);

-- Insert skill Haki Vũ Trang (901) các cấp -1 đến 10
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`) VALUES
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Bao bọc cơ thể bằng một lớp giáp vô hình, tăng cường phòng thủ và sức tấn công.', -1, 0, 0, '[[4,40],[1,25]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 45% Phòng Thủ, 27% Tấn Công', 1, 0, 0, '[[4,45],[1,27]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 50% Phòng Thủ, 30% Tấn Công', 2, 0, 0, '[[4,50],[1,30]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 55% Phòng Thủ, 33% Tấn Công', 3, 0, 0, '[[4,55],[1,33]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 60% Phòng Thủ, 36% Tấn Công', 4, 0, 0, '[[4,60],[1,36]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 65% Phòng Thủ, 39% Tấn Công', 5, 0, 0, '[[4,65],[1,39]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 70% Phòng Thủ, 42% Tấn Công', 6, 0, 0, '[[4,70],[1,42]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 75% Phòng Thủ, 45% Tấn Công', 7, 0, 0, '[[4,75],[1,45]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 80% Phòng Thủ, 48% Tấn Công', 8, 0, 0, '[[4,80],[1,48]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 85% Phòng Thủ, 50% Tấn Công', 9, 0, 0, '[[4,85],[1,50]]', '[0,-1,-1]', 0, 0),
(901, 901, 414, 2, 1, 'Haki Vũ Trang', 0, 0, 0, 1, 0, 200, 60000, 1, 'Tăng 90% Phòng Thủ, 55% Tấn Công', 10, 0, 0, '[[4,90],[1,55]]', '[0,-1,-1]', 0, 0);

-- Insert skill Haki Bá Vương (902) các cấp -1 đến 10
-- typeSkill=1 (attack), typeBuff=0, EffSpec=[1,100,timeMs] = gây choáng
INSERT INTO `skill` (`id_index`, `id_2`, `icon`, `typeSkill`, `typeBuff`, `name`, `typeEffSkill`, `range`, `rangeLan`, `nTarget`, `damage`, `manaLost`, `timeDelay`, `nKick`, `info`, `Lv_RQ`, `percentLv`, `typeDevil`, `option`, `EffSpec`, `LvDevilSkill`, `phanTramDevilSkill`) VALUES
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 10000, 350, 90000, 1, 'Bộc phát sức mạnh bá vương, tấn công áp đảo tinh thần gây sát thương lớn và làm choáng kẻ thù xung quanh.', -1, 0, 0, '[]', '[1,100,5000]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 12000, 350, 90000, 1, 'Choáng 5.5s, Sát thương 12000', 1, 0, 0, '[]', '[1,100,5500]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 14000, 350, 90000, 1, 'Choáng 6s, Sát thương 14000', 2, 0, 0, '[]', '[1,100,6000]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 16000, 350, 90000, 1, 'Choáng 6.5s, Sát thương 16000', 3, 0, 0, '[]', '[1,100,6500]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 18000, 350, 90000, 1, 'Choáng 7s, Sát thương 18000', 4, 0, 0, '[]', '[1,100,7000]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 20000, 350, 90000, 1, 'Choáng 7.5s, Sát thương 20000', 5, 0, 0, '[]', '[1,100,7500]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 22000, 350, 90000, 1, 'Choáng 8s, Sát thương 22000', 6, 0, 0, '[]', '[1,100,8000]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 24000, 350, 90000, 1, 'Choáng 8.5s, Sát thương 24000', 7, 0, 0, '[]', '[1,100,8500]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 27000, 350, 90000, 1, 'Choáng 9s, Sát thương 27000', 8, 0, 0, '[]', '[1,100,9000]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 300, 300, 5, 30000, 350, 90000, 1, 'Choáng 10s, Sát thương 30000', 9, 0, 0, '[]', '[1,100,10000]', 0, 0),
(902, 902, 415, 1, 0, 'Haki Bá Vương', 300, 600, 600, 10, 35000, 350, 90000, 1, 'Choáng 12s, Sát thương 35000', 10, 0, 0, '[]', '[1,100,12000]', 0, 0);

-- Kiểm tra kết quả sau khi chạy
SELECT id_index, id_2, name, Lv_RQ FROM `skill` WHERE id_index IN (900, 901, 902) ORDER BY id_index, Lv_RQ;
