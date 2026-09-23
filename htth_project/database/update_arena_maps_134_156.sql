-- =============================================================================
-- SQL CẬP NHẬT MỞ RỘNG HỆ THỐNG MAP ĐẤU TRƯỜNG SINH TỒN (MAP ID: 134 -> 156)
-- Mỗi map chỉ có duy nhất 1 KHU (maxzone = 1), ĐÃ BỎ NPC CHUYỂN KHU
-- Chỉ giữ lại NPC Trọng Tài hỗ trợ rời Đấu Trường về Làng
-- Lấy dữ liệu nền (Tiles, MapBack, Size) từ 23 Map Làng/Thị Trấn
-- Phân bổ đầy đủ 23 Boss khủng ngẫu nhiên giữa mỗi Map
-- Kết nối đường dẫn qua lại (vgos) liên hoàn khép kín
-- =============================================================================

-- [01] Cập nhật Map 134 (Nền từ Map 1: Làng Cối Xay Gió) -> 1 Khu | Boss Lân Sư Tử (Mob ID: 153)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    134 AS `id`,
    'Chiến Trường Cối Xay Gió' AS `name`,
    '[[153, 480, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[156, 24, 252, 2106, 252], [135, 936, 252, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 1
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [02] Cập nhật Map 135 (Nền từ Map 9: Thị trấn Vỏ Sò) -> 1 Khu | Boss Siêu Along (Mob ID: 135)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    135 AS `id`,
    'Chiến Trường Thị Trấn Vỏ Sò' AS `name`,
    '[[135, 528, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[134, 24, 252, 906, 252], [136, 1032, 252, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 9
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [03] Cập nhật Map 136 (Nền từ Map 17: Thị trấn Orang) -> 1 Khu | Boss Siêu Smoker (Mob ID: 136)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    136 AS `id`,
    'Chiến Trường Thị Trấn Orang' AS `name`,
    '[[136, 528, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[135, 24, 252, 1002, 252], [137, 1032, 252, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 17
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [04] Cập nhật Map 137 (Nền từ Map 25: Làng Sirup) -> 1 Khu | Boss Siêu Mr. 3 (Mob ID: 137)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    137 AS `id`,
    'Chiến Trường Làng Sirup' AS `name`,
    '[[137, 480, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[136, 24, 252, 1002, 252], [138, 936, 252, 54, 300]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 25
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [05] Cập nhật Map 138 (Nền từ Map 33: Nhà hàng Barati) -> 1 Khu | Boss Siêu Wapol (Mob ID: 138)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    138 AS `id`,
    'Chiến Trường Nhà Hàng Barati' AS `name`,
    '[[138, 540, 300]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 290, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[137, 24, 300, 906, 252], [139, 1056, 324, 54, 264]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 33
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [06] Cập nhật Map 139 (Nền từ Map 41: Làng hạt dẻ) -> 1 Khu | Boss Siêu Crocodile (Mob ID: 139)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    139 AS `id`,
    'Chiến Trường Làng Hạt Dẻ' AS `name`,
    '[[139, 492, 264]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 254, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[138, 24, 264, 1026, 324], [140, 960, 264, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 41
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [07] Cập nhật Map 140 (Nền từ Map 49: Thị trấn khởi đầu) -> 1 Khu | Boss Siêu Thần Enel (Mob ID: 140)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    140 AS `id`,
    'Chiến Trường Thị Trấn Khởi Đầu' AS `name`,
    '[[140, 492, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[139, 24, 252, 930, 264], [141, 960, 252, 54, 264]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 49
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [08] Cập nhật Map 141 (Nền từ Map 69: Thị Trấn Whiskay) -> 1 Khu | Boss Mr. 2 Bon Kurei (Mob ID: 147)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    141 AS `id`,
    'Chiến Trường Thị Trấn Whiskay' AS `name`,
    '[[147, 528, 264]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 254, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[140, 24, 264, 930, 252], [142, 1032, 252, 54, 276]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 69
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [09] Cập nhật Map 142 (Nền từ Map 83: Thị Trấn Horn) -> 1 Khu | Boss Quái Vật Tuyết (Mob ID: 99)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    142 AS `id`,
    'Chiến Trường Thị Trấn Horn' AS `name`,
    '[[99, 540, 276]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 266, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[141, 24, 276, 1002, 252], [143, 936, 360, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 83
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [10] Cập nhật Map 143 (Nền từ Map 93: Thị Trấn Nanohano) -> 1 Khu | Boss Mr. 1 Daz Bonez (Mob ID: 148)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    143 AS `id`,
    'Chiến Trường Thị Trấn Nanohano' AS `name`,
    '[[148, 528, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[142, 24, 252, 906, 360], [144, 780, 336, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 93
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [11] Cập nhật Map 144 (Nền từ Map 96: 10-3 Thị Trấn Yuba) -> 1 Khu | Boss Crocodile Sa Mạc (Mob ID: 149)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    144 AS `id`,
    'Chiến Trường Thị Trấn Yuba' AS `name`,
    '[[149, 528, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[143, 24, 252, 750, 336], [145, 1032, 252, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 96
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [12] Cập nhật Map 145 (Nền từ Map 99: 10-6 Kinh Thành Alabastra) -> 1 Khu | Boss Chúa Trời Enel (Mob ID: 112)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    145 AS `id`,
    'Chiến Trường Kinh Thành Alabastra' AS `name`,
    '[[112, 528, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[144, 24, 252, 1002, 252], [146, 1032, 252, 162, 336]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 99
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [13] Cập nhật Map 146 (Nền từ Map 107: Đảo Jaza) -> 1 Khu | Boss Thuyền Trưởng Buggy (Mob ID: 145)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    146 AS `id`,
    'Chiến Trường Đảo Jaza' AS `name`,
    '[[145, 528, 336]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 232, 326, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[145, 132, 336, 1002, 252], [147, 1032, 264, 54, 288]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 107
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [14] Cập nhật Map 147 (Nền từ Map 113: Thị Trấn Thiên Sứ) -> 1 Khu | Boss Chopper Khổng Lồ (Mob ID: 173)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    147 AS `id`,
    'Chiến Trường Thị Trấn Thiên Sứ' AS `name`,
    '[[173, 528, 288]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 278, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[146, 24, 288, 1002, 264], [148, 1032, 276, 222, 444]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 113
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [15] Cập nhật Map 148 (Nền từ Map 191: Kinh đô nước) -> 1 Khu | Boss Blueno CP9 (Mob ID: 157)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    148 AS `id`,
    'Chiến Trường Kinh Đô Nước' AS `name`,
    '[[157, 540, 444]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 292, 434, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[147, 192, 444, 1002, 276], [149, 1056, 312, 54, 288]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 191
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [16] Cập nhật Map 149 (Nền từ Map 189: Lovely Street) -> 1 Khu | Boss Kalifa CP9 (Mob ID: 158)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    149 AS `id`,
    'Chiến Trường Lovely Street' AS `name`,
    '[[158, 816, 288]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 278, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[148, 24, 288, 1026, 312], [150, 1344, 168, 174, 192]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 189
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [17] Cập nhật Map 150 (Nền từ Map 8: Đảo Vỏ Sò) -> 1 Khu | Boss Kukurou CP9 (Mob ID: 159)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    150 AS `id`,
    'Chiến Trường Đảo Vỏ Sò' AS `name`,
    '[[159, 540, 192]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 244, 182, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[149, 144, 192, 1314, 168], [151, 1056, 288, 198, 192]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 8
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [18] Cập nhật Map 151 (Nền từ Map 16: Đảo Orange) -> 1 Khu | Boss Kumadori CP9 (Mob ID: 160)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    151 AS `id`,
    'Chiến Trường Đảo Orange' AS `name`,
    '[[160, 540, 192]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 268, 182, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[150, 168, 192, 1026, 288], [152, 1056, 288, 174, 192]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 16
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [19] Cập nhật Map 152 (Nền từ Map 24: Đảo Sirup) -> 1 Khu | Boss Jabra CP9 (Mob ID: 161)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    152 AS `id`,
    'Chiến Trường Đảo Sirup' AS `name`,
    '[[161, 540, 192]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 244, 182, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[151, 144, 192, 1026, 288], [153, 1056, 288, 138, 192]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 24
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [20] Cập nhật Map 153 (Nền từ Map 40: Đảo hạt dẻ) -> 1 Khu | Boss Kaku CP9 (Mob ID: 162)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    153 AS `id`,
    'Chiến Trường Đảo Hạt Dẻ' AS `name`,
    '[[162, 540, 192]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 208, 182, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[152, 108, 192, 1026, 288], [154, 1056, 288, 162, 192]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 40
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [21] Cập nhật Map 154 (Nền từ Map 48: Đảo Bắc Đẩu) -> 1 Khu | Boss Rob Lucci CP9 (Mob ID: 163)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    154 AS `id`,
    'Chiến Trường Đảo Bắc Đẩu' AS `name`,
    '[[163, 540, 192]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 232, 182, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[153, 132, 192, 1026, 288], [155, 1056, 288, 138, 384]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 48
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [22] Cập nhật Map 155 (Nền từ Map 82: Đảo Drump) -> 1 Khu | Boss Ngũ Lão Tinh Saturn (Mob ID: 174)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    155 AS `id`,
    'Chiến Trường Đảo Drump' AS `name`,
    '[[174, 540, 384]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 208, 374, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[154, 108, 384, 1026, 288], [156, 1056, 276, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 82
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);

-- [23] Cập nhật Map 156 (Nền từ Map 2: 1-1 Rừng làng) -> 1 Khu | Boss Tứ Hoàng Siêu Râu Trắng (Mob ID: 172)
INSERT INTO `maps` (`id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`)
SELECT 
    156 AS `id`,
    'Chiến Trường Rừng Làng' AS `name`,
    '[[172, 1080, 252]]' AS `mobs`,
    1 AS `maxzone`,
    50 AS `maxplayer`,
    '[[ -202, "Trọng Tài", "Rời Đấu Trường", "Bạn có muốn rời khỏi Đấu Trường Sinh Tồn để trở về Làng?", 124, 242, 1, 0, 0, 0, 0, [71, 2], 0, 0, [] ]]' AS `npcs`,
    `boat`,
    0 AS `typeViewPlayer`,
    `b`,
    0 AS `specMap`,
    '[[155, 24, 252, 1026, 276], [134, 2136, 252, 54, 252]]' AS `vgos`,
    `data`,
    `MapBack`,
    `id_eff_map`,
    `level`,
    `typeChangeMap`,
    `mPosMapTrain`,
    `strTimeChange`
FROM `maps` 
WHERE `id` = 2
ON DUPLICATE KEY UPDATE 
    `name`=VALUES(`name`), 
    `mobs`=VALUES(`mobs`), 
    `maxzone`=VALUES(`maxzone`), 
    `maxplayer`=VALUES(`maxplayer`), 
    `npcs`=VALUES(`npcs`), 
    `boat`=VALUES(`boat`), 
    `typeViewPlayer`=VALUES(`typeViewPlayer`), 
    `b`=VALUES(`b`), 
    `specMap`=VALUES(`specMap`), 
    `vgos`=VALUES(`vgos`), 
    `data`=VALUES(`data`), 
    `MapBack`=VALUES(`MapBack`), 
    `id_eff_map`=VALUES(`id_eff_map`), 
    `level`=VALUES(`level`), 
    `typeChangeMap`=VALUES(`typeChangeMap`), 
    `mPosMapTrain`=VALUES(`mPosMapTrain`), 
    `strTimeChange`=VALUES(`strTimeChange`);
