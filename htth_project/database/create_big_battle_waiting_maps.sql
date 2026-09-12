-- ====================================================================
-- SQL TAO CAC MAP SANH CHO CHO TINH NANG 'TRAN CHIEN LON' (NPC ZOSAKU)
-- Bracket 1 (Lv 20-39): Map ID 2030 (Nen Thi Tran Orange)
-- Bracket 2 (Lv 40-69): Map ID 2031 (Nen Nha Hang Baratie)
-- Bracket 3 (Lv 70+)  : Map ID 2032 (Nen Thi Tran Whiskey / Mom Sinh Doi)
-- NPC Do Doc ID: -996 (Image 5014 tu part 423)
-- ====================================================================

USE `full_db_htth`;

-- Xoa cac map cu neu da ton tai de tranh trung lap
DELETE FROM `maps` WHERE `id` IN (2030, 2031, 2032);

-- 1. Sanh Cho Bracket 1 (Cap 20 - 39) - Nen Map 17
INSERT INTO `maps` (
  `id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`,
  `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`,
  `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`
)
SELECT 
  2030, 
  'Sảnh Chờ Trận Chiến Lớn (20-39)', 
  '[]', 
  5, 
  30, 
  '[[-996, "Đô Đốc", "Trận Chiến Lớn", "Đấu trường đỉnh cao - Vinh quang hải tặc!", 480, 170, 1, 0, 0, 0, 0, [14, 2], 0, 0, []], [-7, " ", "Chuyển khu", "", 385, 170, 99, -1, 24, 24, 0, [5, 1], 0, 0, []]]', 
  '[]', 
  `typeViewPlayer`, 
  `b`, 
  `specMap`, 
  '[]', 
  `data`, 
  `MapBack`, 
  `id_eff_map`, 
  `level`, 
  `typeChangeMap`, 
  `mPosMapTrain`, 
  `strTimeChange`
FROM `maps` WHERE `id` = 17 LIMIT 1;

-- 2. Sanh Cho Bracket 2 (Cap 40 - 69) - Nen Map 33
INSERT INTO `maps` (
  `id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`,
  `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`,
  `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`
)
SELECT 
  2031, 
  'Sảnh Chờ Trận Chiến Lớn (40-69)', 
  '[]', 
  5, 
  30, 
  '[[-996, "Đô Đốc", "Trận Chiến Lớn", "Đấu trường đỉnh cao - Vinh quang hải tặc!", 480, 200, 1, 0, 0, 0, 0, [14, 2], 0, 0, []], [-7, " ", "Chuyển khu", "", 385, 200, 99, -1, 24, 24, 0, [5, 1], 0, 0, []]]', 
  '[]', 
  `typeViewPlayer`, 
  `b`, 
  `specMap`, 
  '[]', 
  `data`, 
  `MapBack`, 
  `id_eff_map`, 
  `level`, 
  `typeChangeMap`, 
  `mPosMapTrain`, 
  `strTimeChange`
FROM `maps` WHERE `id` = 33 LIMIT 1;

-- 3. Sanh Cho Bracket 3 (Cap 70+) - Nen Map 69
INSERT INTO `maps` (
  `id`, `name`, `mobs`, `maxzone`, `maxplayer`, `npcs`, `boat`,
  `typeViewPlayer`, `b`, `specMap`, `vgos`, `data`, `MapBack`,
  `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`
)
SELECT 
  2032, 
  'Sảnh Chờ Trận Chiến Lớn (70+)', 
  '[]', 
  5, 
  30, 
  '[[-996, "Đô Đốc", "Trận Chiến Lớn", "Đấu trường đỉnh cao - Vinh quang hải tặc!", 480, 165, 1, 0, 0, 0, 0, [14, 2], 0, 0, []], [-7, " ", "Chuyển khu", "", 385, 165, 99, -1, 24, 24, 0, [5, 1], 0, 0, []]]', 
  '[]', 
  `typeViewPlayer`, 
  `b`, 
  `specMap`, 
  '[]', 
  `data`, 
  `MapBack`, 
  `id_eff_map`, 
  `level`, 
  `typeChangeMap`, 
  `mPosMapTrain`, 
  `strTimeChange`
FROM `maps` WHERE `id` = 69 LIMIT 1;