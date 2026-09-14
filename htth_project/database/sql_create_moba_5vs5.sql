-- ==========================================================
-- SQL CẤU HÌNH 5 MAP PHÓ BẢN 5VS5 PHÁ TRỤ (MOBA HẢI TẶC)
-- Dữ liệu Tile và MapBack lấy từ Map ID 126 (11-8 Khu Đền Thờ 2)
-- Map 129: Làng đỏ (Căn Cứ Phe Đỏ)
-- Map 130: Làng xanh (Căn Cứ Phe Xanh)
-- Map 131: Đường trên (Top Lane)
-- Map 132: Đường giữa (Mid Lane)
-- Map 133: Đường dưới (Bot Lane)
-- ==========================================================

USE `full_db_htth`;

-- 1. Map 129: Căn Cứ Phe Đỏ (Trụ Chính A ở x=250, y=288)
UPDATE `maps` SET
    `name` = 'Làng đỏ',
    `maxzone` = 1,
    `maxplayer` = 25,
    `b` = 1,
    `specMap` = 1,
    `typeViewPlayer` = 0,
    `MapBack` = (SELECT MapBack FROM (SELECT MapBack FROM `maps` WHERE id = 126) AS tmpMB),
    `mobs` = '[[123,250,288]]',
    `vgos` = '[[131,960,200,80,288],[132,1000,288,80,288],[133,960,380,80,288]]',
    `data` = (SELECT data FROM (SELECT data FROM `maps` WHERE id = 126) AS tmpData)
WHERE `id` = 129;

-- 2. Map 130: Căn Cứ Phe Xanh (Trụ Chính B ở x=806, y=288)
UPDATE `maps` SET
    `name` = 'Làng xanh',
    `maxzone` = 1,
    `maxplayer` = 25,
    `b` = 1,
    `specMap` = 1,
    `typeViewPlayer` = 0,
    `MapBack` = (SELECT MapBack FROM (SELECT MapBack FROM `maps` WHERE id = 126) AS tmpMB),
    `mobs` = '[[125,806,288]]',
    `vgos` = '[[131,96,200,976,288],[132,56,288,976,288],[133,96,380,976,288]]',
    `data` = (SELECT data FROM (SELECT data FROM `maps` WHERE id = 126) AS tmpData)
WHERE `id` = 130;

-- 3. Map 131: Đường trên (Trụ Thường A ở x=350, Trụ Thường B ở x=706)
UPDATE `maps` SET
    `name` = 'Đường trên',
    `maxzone` = 1,
    `maxplayer` = 25,
    `b` = 1,
    `specMap` = 1,
    `typeViewPlayer` = 0,
    `MapBack` = (SELECT MapBack FROM (SELECT MapBack FROM `maps` WHERE id = 126) AS tmpMB),
    `mobs` = '[[122,350,288],[124,706,288]]',
    `vgos` = '[[129,60,288,920,200],[130,996,288,136,200],[132,528,380,528,200]]',
    `data` = (SELECT data FROM (SELECT data FROM `maps` WHERE id = 126) AS tmpData)
WHERE `id` = 131;

-- 4. Map 132: Đường giữa (Trụ Thường A ở x=350, Trụ Thường B ở x=706)
UPDATE `maps` SET
    `name` = 'Đường giữa',
    `maxzone` = 1,
    `maxplayer` = 25,
    `b` = 1,
    `specMap` = 1,
    `typeViewPlayer` = 0,
    `MapBack` = (SELECT MapBack FROM (SELECT MapBack FROM `maps` WHERE id = 126) AS tmpMB),
    `mobs` = '[[122,350,288],[124,706,288]]',
    `vgos` = '[[129,60,288,960,288],[130,996,288,96,288],[131,528,180,528,360],[133,528,380,528,200]]',
    `data` = (SELECT data FROM (SELECT data FROM `maps` WHERE id = 126) AS tmpData)
WHERE `id` = 132;

-- 5. Map 133: Đường dưới (Trụ Thường A ở x=350, Trụ Thường B ở x=706)
UPDATE `maps` SET
    `name` = 'Đường dưới',
    `maxzone` = 1,
    `maxplayer` = 25,
    `b` = 1,
    `specMap` = 1,
    `typeViewPlayer` = 0,
    `MapBack` = (SELECT MapBack FROM (SELECT MapBack FROM `maps` WHERE id = 126) AS tmpMB),
    `mobs` = '[[122,350,288],[124,706,288]]',
    `vgos` = '[[129,60,288,920,380],[130,996,288,136,380],[132,528,180,528,360]]',
    `data` = (SELECT data FROM (SELECT data FROM `maps` WHERE id = 126) AS tmpData)
WHERE `id` = 133;
