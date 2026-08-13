-- SQL Debug: Kiểm tra và thêm map 2000
-- ============================================

-- 1. Kiểm tra map 2000 có trong DB không
SELECT id, name FROM maps WHERE id = 2000;

-- 2. Xóa map 2000 nếu tồn tại (để insert lại sạch)
DELETE FROM maps WHERE id = 2000;

-- 3. Thêm map 2000 (Đảo Luyện Haki) - phiên bản đơn giản
INSERT INTO `maps` (`id`, `name`, `vgos`, `maxzone`, `maxplayer`, `npcs`, `mobs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`, `data`)
VALUES (
    2000,
    'Đảo Luyện Haki',
    '[]',
    1,
    15,
    '[{"0":"-38","1":"Thoát","2":" ","3":"Thoát khỏi Đảo Luyện Haki","4":200,"5":300,"6":1,"7":0,"8":1,"9":1,"10":0,"11":[5,1],"12":0,"13":0,"14":[]}]',
    '[[44,408,240]]',
    '[]',
    0,
    1,
    0,
    '[45,400,1400,1200]',
    0,
    1,
    0,
    '[]',
    '',
    '[]'
);

-- 4. Verify map 2000 đã được thêm
SELECT id, name, maxzone, maxplayer FROM maps WHERE id = 2000;
