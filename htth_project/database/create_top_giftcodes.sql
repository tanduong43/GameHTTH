USE full_db_htth;

-- =======================================================================
-- 1. LỆNH XÓA CÁC GIFTCODE TOP CŨ (NẾU CÓ)
-- =======================================================================
DELETE FROM `giftcode` WHERE `giftname` IN (
    'top1_level', 'top2_level', 'top3_level', 'top4_10_level',
    'top1_pvp', 'top2_pvp', 'top3_pvp', 'top4_10_pvp',
    'top1_clan', 'top2_clan', 'top3_clan',
    'top1_nap', 'top2_nap', 'top3_nap', 'top4_10_nap',
    'top1level', 'top2level', 'top3level', 'top410level',
    'top1pvp', 'top2pvp', 'top3pvp', 'top410pvp',
    'top1clan', 'top2clan', 'top3clan',
    'top1nap', 'top2nap', 'top3nap', 'top410nap'
);

-- =======================================================================
-- 2. LỆNH THÊM MỚI TOÀN BỘ GIFTCODE TOP (KHÔNG DẤU GẠCH DƯỚI _)
-- =======================================================================

-- 2.1. TOP LEVEL (CẤP ĐỘ)
INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
VALUES 
-- Top 1 Level
('top1level', 1000000000, 50000, '[[11,74,1],[4,427,1],[4,1004,5],[4,551,1],[4,323,3]]', 'Quà Top 1 Level', 0, 1, '', '', 0),
-- Top 2 Level
('top2level', 500000000, 20000, '[[4,1001,1],[4,1003,1],[4,1004,4],[4,551,1],[4,323,1]]', 'Quà Top 2 Level', 0, 1, '', '', 0),
-- Top 3 Level
('top3level', 100000000, 15000, '[[4,1001,1],[4,1003,1],[4,323,1],[4,1004,3],[4,551,1]]', 'Quà Top 3 Level', 0, 1, '', '', 0),
-- Top 4-10 Level (Giới hạn 7 lượt nhập cho 7 người chơi)
('top410level', 100000000, 10000, '[[4,1001,1],[4,1003,1],[4,1004,2],[4,549,1]]', 'Quà Top 4-10 Level', 0, 7, '', '', 0);

-- 2.2. TOP PVP
INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
VALUES 
-- Top 1 PvP
('top1pvp', 100000000, 10000, '[[11,85,1],[4,1004,1],[4,323,1]]', 'Quà Top 1 PvP', 0, 1, '', '', 0),
-- Top 2 PvP
('top2pvp', 100000000, 5000, '[[11,86,1]]', 'Quà Top 2 PvP', 0, 1, '', '', 0),
-- Top 3 PvP
('top3pvp', 100000000, 2000, '[[11,89,1]]', 'Quà Top 3 PvP', 0, 1, '', '', 0),
-- Top 4-10 PvP (Giới hạn 7 lượt nhập cho 7 người chơi)
('top410pvp', 100000000, 1000, '[]', 'Quà Top 4-10 PvP', 0, 7, '', '', 0);

-- 2.3. TOP BĂNG (CLAN) - Dành cho mỗi thành viên trong bang
INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
VALUES 
-- Top 1 Băng (Mỗi thành viên: 5k Ruby, 1 Búa siêu cấp)
('top1clan', 0, 5000, '[[4,323,1]]', 'Quà Top 1 Băng Hội', 0, 50, '', '', 0),
-- Top 2 Băng (Mỗi thành viên: 2k Ruby)
('top2clan', 0, 2000, '[]', 'Quà Top 2 Băng Hội', 0, 50, '', '', 0),
-- Top 3 Băng (Mỗi thành viên: 1k Ruby)
('top3clan', 0, 1000, '[]', 'Quà Top 3 Băng Hội', 0, 50, '', '', 0);

-- 2.4. TOP NẠP (PHÚ HỘ)
INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
VALUES 
-- Top 1 Nạp
('top1nap', 1000000000, 100000, '[[11,74,1],[4,427,1],[4,551,1],[4,323,10],[4,325,3],[4,1004,10]]', 'Quà Top 1 Nạp', 0, 1, '', '', 0),
-- Top 2 Nạp
('top2nap', 500000000, 40000, '[[4,1002,1],[4,427,1],[4,323,3],[4,551,1],[4,325,2],[4,1004,5]]', 'Quà Top 2 Nạp', 0, 1, '', '', 0),
-- Top 3 Nạp
('top3nap', 100000000, 10000, '[[4,1002,1],[4,1015,1],[4,323,1],[4,551,1],[4,325,1],[4,1004,2]]', 'Quà Top 3 Nạp', 0, 1, '', '', 0),
-- Top 4-10 Nạp (Giới hạn 7 lượt nhập cho 7 người chơi)
('top410nap', 100000000, 10000, '[[4,1001,1],[4,1015,1],[4,550,1],[4,1004,2]]', 'Quà Top 4-10 Nạp', 0, 7, '', '', 0);
