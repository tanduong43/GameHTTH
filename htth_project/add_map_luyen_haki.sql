-- SQL để thêm map luyện Haki (map ID 2000)
-- Chạy file này trong MySQL database

-- Thêm map luyện Haki (INSERT IGNORE để tránh lỗi nếu đã tồn tại)
INSERT IGNORE INTO `maps` (`id`, `name`, `vgos`, `maxzone`, `maxplayer`, `npcs`, `mobs`, `boat`, `typeViewPlayer`, `b`, `specMap`, `MapBack`, `id_eff_map`, `level`, `typeChangeMap`, `mPosMapTrain`, `strTimeChange`, `data`)
VALUES (
    2000, 
    'Đảo Luyện Haki', 
    '[]',  -- Không có VGO (cổng dịch chuyển)
    1,     -- 1 zone
    15,    -- max player
    '[]',  -- Không có NPC
    '[]',  -- Danh sách mob (sẽ được thêm sau trong map data)
    '[]',  -- Không có thuyền
    0,     -- typeViewPlayer
    0,     -- b
    0,     -- specMap
    '[45,400,1400,1200]',  -- MapBack: background
    0,     -- id_eff_map
    1,     -- level yêu cầu
    0,     -- typeChangeMap
    '[]',  -- mPosMapTrain
    '',    -- strTimeChange
    '[]'   -- data
);
