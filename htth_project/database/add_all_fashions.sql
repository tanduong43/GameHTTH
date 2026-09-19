-- =====================================================================
-- SQL SCRIPT TỔNG HỢP: TẤT CẢ 10 BỘ THỜI TRANG MỚI & CÁC PART TƯƠNG ỨNG
-- Danh sách thời trang: 249 (Nezuko), 250 (Sabo), 251 (Robin), 252 (Moria),
-- 253 (Jinbe), 254 (Vy Ngu), 255 (Cavendis), 256 (Natra), 257 (Long Ngu), 258 (Nu)
-- =====================================================================
USE full_db_htth;

-- [0] XÓA SẠCH DỮ LIỆU CŨ ĐỂ NẠP MỚI KHÔNG BỊ TRÙNG
DELETE FROM `parts` WHERE `id` IN (1119, 1120, 1121, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 1150, 1151);
DELETE FROM `fashiontemplate` WHERE `id` IN (249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 36, 37, 38);

-- =====================================================================
-- [1] THÊM TÓC MỚI (ITEMHAIR)
-- =====================================================================

-- =====================================================================
-- [2] THÊM CHI TIẾT TỪNG BỘ THỜI TRANG (PARTS & FASHIONTEMPLATE)
-- =====================================================================

-- ---------------------------------------------------------------------
-- Thời trang ID 249: Thời trang Nezuko (Icon: 142)
-- ---------------------------------------------------------------------
-- Part 1119 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1119, 0, '[[12939,0,4],[12940,0,4],[12940,0,4],[12941,0,4],[12942,0,3]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1120 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1120, 1, '[[12943, 0, 0], [12944, 0, 0], [12945, 0, 0], [12946, 0, 0], [12947, 0, 0], [12948, 0, 0], [12949, 0, 0], [12950, 0, 0], [12951, 0, 0], [12952, 0, 0], [12953, 0, 0], [12954, 0, 0], [12955, 0, 0], [12956, 0, 0], [12957, 0, 0], [12958, 0, 0], [12959, 0, 0], [12960, 0, 0], [12961, 0, 0], [12962, 0, 0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1121 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1121, 2, '[[12963, 0, 4], [12964, 0, 4], [12965, 0, 4], [12966, 0, 4], [12967, 0, 4], [12968, 0, 4], [12969, 0, 4], [12970, 0, 4], [12971, 0, 4], [12972, 0, 4], [12973, 0, 4], [12974, 0, 4], [12975, 0, 4], [12976, 0, 4], [12977, 0, 4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 249
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (249, 142, 'Thời trang Nezuko', 'Thời trang Nezuko Kamado\n+10% Chí mạng\n+10% Né tránh\n+10% Miễn thương\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1120,-1,1121,1119,-2]', '[[10,100],[12,100],[53,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=142, `name`='Thời trang Nezuko', `info`='Thời trang Nezuko Kamado\n+10% Chí mạng\n+10% Né tránh\n+10% Miễn thương\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1120,-1,1121,1119,-2]', `op`='[[10,100],[12,100],[53,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 250: Thời trang Sabo (Icon: 142)
-- ---------------------------------------------------------------------
-- Part 1123 (Tóc/Nón (Hair/Hat - 2 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1123, 5, '[[12269,-1,-6],[12270,-1,-6]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1124 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1124, 1, '[[12271,0,0],[12272,0,0],[12273,0,0],[12274,0,0],[12275,0,0],[12276,0,0],[12277,0,0],[12278,0,0],[12279,0,0],[12280,0,0],[12281,0,0],[12282,0,0],[12283,0,0],[12284,0,0],[12285,0,0],[12286,0,0],[12287,0,0],[12288,0,0],[12289,0,0],[12290,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1125 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1125, 2, '[[12291,0,0],[12292,0,0],[12293,0,0],[12294,0,0],[12295,0,0],[12296,0,0],[12297,0,0],[12298,0,0],[12299,0,0],[12300,0,0],[12301,0,0],[12302,0,0],[12303,0,0],[12304,0,0],[12305,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1126 (Vũ khí (Weapon - 24 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1126, 3, '[[12306,3,0],[12307,20,-1],[12307,16,-1],[12308,20,-1],[12308,20,-1],[12307,19,-1],[12309,26,-2],[12307,27,-1],[12307,27,-1],[12310,-1,5],[79,0,0],[79,0,0],[12311,-2,3],[12312,0,2],[12313,5,8],[79,0,0],[12314,-1,-1],[12314,-1,-1],[12316,5,-2],[12315,0,-2],[12316,5,-2],[12315,0,-2],[12317,2,0],[12318,2,-1]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 250
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (250, 142, 'Thời trang Sabo', 'Thời trang Sabo\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[1126,-2,-1,1124,-1,1125,-1,1123]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=142, `name`='Thời trang Sabo', `info`='Thời trang Sabo\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[1126,-2,-1,1124,-1,1125,-1,1123]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 251: Thời trang Robin (Icon: 143)
-- ---------------------------------------------------------------------
-- Part 1127 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1127, 0, '[[12319,-3,-3],[12320,-3,-3],[12321,-3,-3],[12322,-3,-3],[12323,-3,-3]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1128 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1128, 1, '[[12324,-2,-4],[12325,-2,-1],[12326,-2,-1],[12327,-2,-1],[12328,-2,-1],[12329,-2,-4],[12330,-2,-4],[12331,-2,-4],[12332,-2,-4],[12333,-2,-4],[12334,-2,-4],[12335,-2,-4],[12336,-2,-4],[12337,-2,-4],[12338,-2,-4],[12339,-2,-4],[12340,-2,-4],[12341,-2,-4],[12342,-2,-4],[12343,-2,-4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1129 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1129, 2, '[[12344,0,0],[12345,0,0],[12346,0,0],[12347,0,0],[12348,0,0],[12349,0,0],[12350,0,0],[12351,0,0],[12352,0,0],[12353,0,0],[12354,0,0],[12355,0,0],[12356,0,0],[12357,0,0],[12358,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 251
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (251, 143, 'Thời trang Robin', 'Thời trang Robin\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1128,-1,1129,1127,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=143, `name`='Thời trang Robin', `info`='Thời trang Robin\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1128,-1,1129,1127,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 252: Thời trang Gecko Moria (Icon: 158)
-- ---------------------------------------------------------------------
-- Part 1130 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1130, 0, '[[12399,-1,-40],[12400,-1,-40],[12401,-1,-40],[12402,-1,-40],[12403,-1,-40]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1131 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1131, 1, '[[12404,-13,-26],[12405,-13,-26],[12406,-13,-26],[12407,-13,-26],[12408,-13,-26],[12409,-13,-26],[12410,-13,-26],[12411,-13,-26],[12412,-13,-26],[12413,-13,-26],[12414,-13,-26],[12415,-13,-26],[12416,-13,-26],[12417,-13,-26],[12418,-13,-26],[12419,-13,-26],[12420,-13,-26],[12421,-13,-26],[12422,-13,-26],[12423,-13,-26]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1132 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1132, 2, '[[12424,0,0],[12425,0,0],[12426,0,0],[12427,0,0],[12428,0,0],[12429,0,0],[12430,0,0],[12431,0,0],[12432,0,0],[12433,0,0],[12434,0,0],[12435,0,0],[12436,0,0],[12437,0,0],[12438,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 252
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (252, 158, 'Thời trang Gecko Moria', 'Thời trang Gecko Moria\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1131,-1,1132,1130,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=158, `name`='Thời trang Gecko Moria', `info`='Thời trang Gecko Moria\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1131,-1,1132,1130,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 253: Thời trang Jinbe (Icon: 159)
-- ---------------------------------------------------------------------
-- Part 1133 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1133, 0, '[[12439,7,-23],[12440,7,-23],[12441,7,-23],[12442,7,-23],[12443,7,-23]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1134 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1134, 1, '[[12444,-6,-20],[12445,-6,-20],[12446,-6,-20],[12447,-6,-20],[12448,-6,-20],[12449,-6,-20],[12450,-6,-20],[12451,-6,-20],[12452,-6,-20],[12453,-6,-20],[12454,-6,-20],[12455,-6,-20],[12456,-6,-20],[12457,-6,-20],[12458,-6,-20],[12459,-6,-20],[12460,-6,-20],[12461,-6,-20],[12462,-6,-20],[12463,-6,-20]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1135 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1135, 2, '[[12464,0,0],[12465,0,0],[12466,0,0],[12467,0,0],[12468,0,0],[12469,0,0],[12470,0,0],[12471,0,0],[12472,0,0],[12473,0,0],[12474,0,0],[12475,0,0],[12476,0,0],[12477,0,0],[12478,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 253
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (253, 159, 'Thời trang Jinbe', 'Thời trang Jinbe\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1134,-1,1135,1133,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=159, `name`='Thời trang Jinbe', `info`='Thời trang Jinbe\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1134,-1,1135,1133,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 254: Thời trang Vy Ngu (Icon: 147)
-- ---------------------------------------------------------------------
-- Part 1136 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1136, 0, '[[12530,-4,3],[12531,-4,3],[12532,-4,3],[12533,-4,3],[12534,-4,3]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1137 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1137, 1, '[[12535,-3,-9],[12536,-3,-9],[12537,-3,-9],[12538,-3,-9],[12539,-3,-9],[12540,-3,-9],[12541,-3,-9],[12542,-3,-9],[12543,-3,-9],[12544,-3,-9],[12545,-3,-9],[12546,-3,-9],[12547,-3,-9],[12548,-3,-9],[12549,-3,-9],[12550,-3,-9],[12551,-3,-9],[12552,-3,-9],[12553,-3,-9],[12554,-3,-9]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1138 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1138, 2, '[[12555,0,0],[12556,0,0],[12557,0,0],[12558,0,0],[12559,0,0],[12560,0,0],[12561,0,0],[12562,0,0],[12563,0,0],[12564,0,0],[12565,0,0],[12566,0,0],[12567,0,0],[12568,0,0],[12569,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 254
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (254, 147, 'Thời trang Vy Ngu', 'Thời trang Vy Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1137,-1,1138,1136,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=147, `name`='Thời trang Vy Ngu', `info`='Thời trang Vy Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1137,-1,1138,1136,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 255: Thời trang Cavendis Ngu (Icon: 148)
-- ---------------------------------------------------------------------
-- Part 1139 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1139, 0, '[[12570,-5,-5],[12571,-5,-5],[12572,-5,-5],[12573,-5,-5],[12574,-5,-5]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1140 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1140, 1, '[[12575,-2,-15],[12576,-2,-15],[12577,-2,-15],[12578,-2,-15],[12579,-2,-15],[12580,-2,-15],[12581,-2,-15],[12582,-2,-15],[12583,-2,-15],[12584,-2,-15],[12585,-2,-15],[12586,-2,-15],[12587,-2,-15],[12588,-2,-15],[12589,-2,-15],[12590,-2,-15],[12591,-2,-15],[12592,-2,-15],[12593,-2,-15],[12594,-2,-15]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1141 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1141, 2, '[[12595,0,0],[12596,0,0],[12597,0,0],[12598,0,0],[12599,0,0],[12600,0,0],[12601,0,0],[12602,0,0],[12603,0,0],[12604,0,0],[12605,0,0],[12606,0,0],[12607,0,0],[12608,0,0],[12609,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 255
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (255, 148, 'Thời trang Cavendis Ngu', 'Thời trang Cavendis Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1140,-1,1141,1139,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=148, `name`='Thời trang Cavendis Ngu', `info`='Thời trang Cavendis Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1140,-1,1141,1139,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 256: Thời trang Natra Ngu (Icon: 154)
-- ---------------------------------------------------------------------
-- Part 1142 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1142, 0, '[[12805,-2,-2],[12806,-2,-2],[12807,-2,-2],[12808,-2,-2],[12809,-2,-2]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1143 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1143, 1, '[[12810,0,0],[12811,0,0],[12812,0,0],[12813,0,0],[12814,0,0],[12815,0,0],[12816,0,0],[12817,0,0],[12818,0,0],[12819,0,0],[12820,0,0],[12821,0,0],[12822,0,0],[12823,0,0],[12824,0,0],[12825,0,0],[12826,0,0],[12827,0,0],[12828,0,0],[12829,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1144 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1144, 2, '[[12830,0,0],[12831,0,0],[12832,0,0],[12833,0,0],[12834,0,0],[12835,0,0],[12836,0,0],[12837,0,0],[12838,0,0],[12839,0,0],[12840,0,0],[12841,0,0],[12842,0,0],[12843,0,0],[12844,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1145 (Vũ khí (Weapon - 24 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1145, 3, '[[12845,3,0],[12846,20,-1],[12846,16,-1],[12847,20,-1],[12847,20,-1],[12846,19,-1],[12848,26,-2],[12846,27,-1],[12846,27,-1],[12849,-1,5],[79,0,0],[79,0,0],[12850,-2,3],[12851,0,2],[12852,5,8],[79,0,0],[12853,-1,-1],[12853,-1,-1],[12855,5,-2],[12854,0,-2],[12855,5,-2],[12854,0,-2],[12856,2,0],[12857,2,-1]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 36 (Natra Ngu)
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (36, 154, 'Thời trang Natra Ngu', 'Thời trang Natra Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[1145,-2,-1,1143,-1,1144,1142,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=154, `name`='Thời trang Natra Ngu', `info`='Thời trang Natra Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[1145,-2,-1,1143,-1,1144,1142,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 37: Thời trang Long Ngu (Icon: 155)
-- ---------------------------------------------------------------------
-- Part 1146 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1146, 0, '[[12858,-2,-4],[12859,-2,-4],[12860,-2,-4],[12861,-2,-4],[12862,-2,-4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1147 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1147, 1, '[[12863,0,0],[12864,0,0],[12865,0,0],[12866,0,0],[12867,0,0],[12868,0,0],[12869,0,0],[12870,0,0],[12871,0,0],[12872,0,0],[12873,0,0],[12874,0,0],[12875,0,0],[12876,0,0],[12877,0,0],[12878,0,0],[12879,0,0],[12880,0,0],[12881,0,0],[12882,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1148 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1148, 2, '[[12883,4,0],[12884,4,0],[12885,4,0],[12886,4,0],[12887,4,0],[12888,4,0],[12889,4,0],[12890,4,0],[12891,4,0],[12892,4,0],[12893,4,0],[12894,4,0],[12895,4,0],[12896,4,0],[12897,4,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 37 (Long Ngu)
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (37, 155, 'Thời trang Long Ngu', 'Thời trang Long Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1147,-1,1148,1146,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=155, `name`='Thời trang Long Ngu', `info`='Thời trang Long Ngu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1147,-1,1148,1146,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;

-- ---------------------------------------------------------------------
-- Thời trang ID 38: Thời trang Nu (Icon: 156)
-- ---------------------------------------------------------------------
-- Part 1149 (Đầu (Head - 5 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1149, 0, '[[12899,-2,-4],[12900,-2,-4],[12901,-2,-4],[12902,-2,-4],[12903,-2,-4]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1150 (Thân/Áo (Body - 20 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1150, 1, '[[12904,0,0],[12905,0,0],[12906,0,0],[12907,0,0],[12908,0,0],[12909,0,0],[12910,0,0],[12911,0,0],[12912,0,0],[12913,0,0],[12914,0,0],[12915,0,0],[12916,0,0],[12917,0,0],[12918,0,0],[12919,0,0],[12920,0,0],[12921,0,0],[12922,0,0],[12923,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- Part 1151 (Quần/Chân (Leg - 15 frame))
INSERT INTO `parts` (`id`, `type`, `data`) VALUES (1151, 2, '[[12924,0,0],[12925,0,0],[12926,0,0],[12927,0,0],[12928,0,0],[12929,0,0],[12930,0,0],[12931,0,0],[12932,0,0],[12933,0,0],[12934,0,0],[12935,0,0],[12936,0,0],[12937,0,0],[12938,0,0]]') ON DUPLICATE KEY UPDATE `type`=VALUES(`type`), `data`=VALUES(`data`);
-- FashionTemplate 38 (Nu)
INSERT INTO `fashiontemplate` (`id`, `icon`, `name`, `info`, `mwear`, `op`, `price`) VALUES (38, 156, 'Thời trang Nu', 'Thời trang Nu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', '[-2,-2,-1,1150,-1,1151,1149,-2]', '[[53,100],[63,100],[10,100]]', -1) ON DUPLICATE KEY UPDATE `icon`=156, `name`='Thời trang Nu', `info`='Thời trang Nu\n+10% Miễn thương\n+10% Giảm miễn thương\n+10% Chí mạng\nHạn sử dụng vĩnh viễn', `mwear`='[-2,-2,-1,1150,-1,1151,1149,-2]', `op`='[[53,100],[63,100],[10,100]]', `price`=-1;
