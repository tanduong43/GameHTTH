USE full_db_htth;

INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
SELECT 'loantin', 20000000, 5000, '[[4,159,20],[4,455,10],[4,457,5]]', '', 1, 1000, '', '', 0
WHERE NOT EXISTS (SELECT 1 FROM `giftcode` WHERE `giftname` = 'loantin');

INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
SELECT 'denbu', 30000000, 5000, '[[4,159,30],[4,455,15],[4,457,5]]', '', 1, 1000, '', '', 0
WHERE NOT EXISTS (SELECT 1 FROM `giftcode` WHERE `giftname` = 'denbu');

INSERT INTO `giftcode` (`giftname`, `beri`, `ruby`, `item`, `thongbao`, `luotnhap`, `gioihan`, `used`, `special`, `is_member`)
SELECT 'baotri', 20000000, 3000, '[[4,159,20],[4,455,10],[4,457,5]]', '', 1, 1000, '', '', 0
WHERE NOT EXISTS (SELECT 1 FROM `giftcode` WHERE `giftname` = 'baotri');
