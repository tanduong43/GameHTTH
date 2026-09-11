-- ==============================================================================
-- SQL THÊM 12 DANH HIỆU QUÂN HÀM VÀ 15 KỸ NĂNG CẤP 31 VÀO DATABASE
-- Bảng mục tiêu: `skill`
-- ID bắt đầu: 4332 (nối tiếp id 4331 cao nhất hiện tại)
-- ==============================================================================

-- ------------------------------------------------------------------------------
-- PHẦN 1: 12 DANH HIỆU QUÂN HÀM (HẢI TẶC & HẢI QUÂN) - ID: 4332 -> 4343
-- Ghi chú: id_index được đánh số từ 742 đến 753 để tránh trùng với Đầu Bếp Cấp 27-30 (691-702)
-- ------------------------------------------------------------------------------
INSERT INTO `skill` VALUES (4332, 742, 5004, 192, 6, 0, 'Tân Binh', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải tặc luôn có ước mơ và hoài bão của mình, khát vọng tự do và kho báu.', 1, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4333, 743, 5004, 192, 6, 0, 'Ưu Tú', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải tặc luôn có ước mơ và hoài bão của mình, khát vọng tự do và kho báu.', 2, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4334, 744, 5004, 192, 6, 0, 'Siêu Tân Tinh', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải tặc luôn có ước mơ và hoài bão của mình, khát vọng tự do và kho báu.', 3, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4335, 745, 5004, 192, 6, 0, 'Thất Vũ Hải', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải tặc luôn có ước mơ và hoài bão của mình, khát vọng tự do và kho báu.', 4, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4336, 746, 5004, 192, 6, 0, 'Tứ Hoàng', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải tặc luôn có ước mơ và hoài bão của mình, khát vọng tự do và kho báu.', 5, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4337, 747, 5004, 192, 6, 0, 'Vua Hải Tặc', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải tặc luôn có ước mơ và hoài bão của mình, khát vọng tự do và kho báu.', 6, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4338, 748, 5005, 193, 6, 0, 'Tân Binh', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải Quân có nhiệm vụ duy trì các luật lệ và mệnh lệnh trên toàn bộ thế giới, luôn bảo vệ lẽ phải.', 1, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4339, 749, 5005, 193, 6, 0, 'Tổng Thanh Tra', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải Quân có nhiệm vụ duy trì các luật lệ và mệnh lệnh trên toàn bộ thế giới, luôn bảo vệ lẽ phải.', 2, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4340, 750, 5005, 193, 6, 0, 'Đại Tá', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải Quân có nhiệm vụ duy trì các luật lệ và mệnh lệnh trên toàn bộ thế giới, luôn bảo vệ lẽ phải.', 3, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4341, 751, 5005, 193, 6, 0, 'Phó Đô Đốc', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải Quân có nhiệm vụ duy trì các luật lệ và mệnh lệnh trên toàn bộ thế giới, luôn bảo vệ lẽ phải.', 4, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4342, 752, 5005, 193, 6, 0, 'Đô Đốc Hải Quân', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải Quân có nhiệm vụ duy trì các luật lệ và mệnh lệnh trên toàn bộ thế giới, luôn bảo vệ lẽ phải.', 5, 0, 0, '[]', '[0,-1,-1]', 0, 0);
INSERT INTO `skill` VALUES (4343, 753, 5005, 193, 6, 0, 'Thủy Sư Đô Đốc', 0, 120, 120, 0, 0, 0, 0, 0, 'Hải Quân có nhiệm vụ duy trì các luật lệ và mệnh lệnh trên toàn bộ thế giới, luôn bảo vệ lẽ phải.', 6, 0, 0, '[]', '[0,-1,-1]', 0, 0);

-- ------------------------------------------------------------------------------
-- PHẦN 2: 15 KỸ NĂNG CẤP 31 CHO 5 PHÁI - ID: 4344 -> 4358
-- Ghi chú: id_index nối tiếp dãy 667-726 (cấp 27-30) bằng dãy 727 đến 741
-- ------------------------------------------------------------------------------
-- Phái 1: Võ sĩ
INSERT INTO `skill` VALUES (4344, 727, 0, 0, 1, 0, 'Quả đấm tốc độ', 471, 50, 50, 1, 633, 0, 1000, 5, 'Kỹ năng tấn công cơ bản của các võ sĩ.', 31, 0, 0, '[[28,5],[29,180],[30,62]]', '[5,130,52]', 0, 0);
INSERT INTO `skill` VALUES (4345, 728, 1, 1, 1, 0, 'Bazooka', 472, 120, 120, 1, 1666, 9, 1000, 1, 'Tung một cú đấm nhanh như viên đạn súng lục.', 31, 0, 0, '[[10,90],[11,310],[28,5],[29,360],[30,55]]', '[5,270,41]', 0, 0);
INSERT INTO `skill` VALUES (4346, 729, 2, 2, 1, 0, 'Liên hoàng cú đấm', 473, 80, 80, 5, 1180, 17, 4200, 5, 'Tấn công liên tiếp vào đối thủ bằng những cú đấm như trời giáng.', 31, 0, 0, '[[28,5],[29,120],[30,40]]', '[5,120,40]', 0, 0);

-- Phái 2: Kiếm khách
INSERT INTO `skill` VALUES (4347, 730, 0, 3, 1, 0, 'Nhất kiếm', 481, 50, 50, 1, 650, 0, 1000, 6, 'Nhát chém tạo ra bằng một thanh kiếm sắc bén.', 31, 0, 0, '[[11,300],[28,2],[29,120],[30,50]]', '[2,120,50]', 0, 0);
INSERT INTO `skill` VALUES (4348, 731, 1, 4, 1, 0, 'Nhị trảm', 482, 120, 120, 1, 1755, 12, 1000, 1, 'Tung ra 2 đường kiếm song song tấn công đối thủ từ khoản cách xa.', 31, 0, 0, '[[13,90],[28,2],[29,300],[30,64]]', '[2,300,64]', 0, 0);
INSERT INTO `skill` VALUES (4349, 732, 2, 5, 1, 0, 'Vòi rồng', 483, 80, 80, 5, 1180, 17, 4200, 5, 'Thuật sử dụng tam kiếm tạo ra một cơn lốc xoáy cuốn theo mọi thứ xung quanh.', 31, 0, 0, '[[28,2],[29,120],[30,40]]', '[2,120,40]', 0, 0);

-- Phái 3: Đầu bếp
INSERT INTO `skill` VALUES (4350, 733, 0, 6, 1, 0, 'Hắc cước', 491, 50, 50, 1, 666, 0, 1000, 6, 'Cú đá in hằn dấu chân lên đối phương.', 31, 0, 0, '[[28,4],[29,120],[30,50]]', '[4,120,50]', 0, 0);
INSERT INTO `skill` VALUES (4351, 734, 1, 7, 1, 0, 'Knock out', 492, 120, 120, 1, 1800, 9, 1000, 1, 'Lao đến đối phương và tung một cú đá khiến đối thủ bật lên cao.', 31, 0, 0, '[[10,90],[11,310],[28,4],[29,360],[30,55]]', '[4,310,45]', 0, 0);
INSERT INTO `skill` VALUES (4352, 735, 2, 8, 1, 0, 'Thịt băm', 493, 80, 80, 5, 1261, 17, 4200, 5, 'Tung liên hoàn cước nhịp nhàng như một đầu bếp đang băm thịt.', 31, 0, 0, '[[28,3],[29,120],[30,40]]', '[3,120,40]', 0, 0);

-- Phái 4: Hoa tiêu
INSERT INTO `skill` VALUES (4353, 736, 0, 9, 1, 0, 'Gậy chong chóng', 511, 60, 60, 1, 730, 0, 1000, 1, 'Đòn tấn công khéo léo bằng gậy của một hoa tiêu.', 31, 0, 0, '[[28,5],[29,300],[30,34]]', '[5,300,34]', 0, 0);
INSERT INTO `skill` VALUES (4354, 737, 1, 10, 1, 0, 'Bong bóng tích điện', 512, 120, 120, 1, 2123, 11, 1000, 1, 'Phóng những quả bóng hơi nước gây sock điện bất cứ ai đến gần nó.', 31, 0, 0, '[[13,90],[28,6],[29,360],[30,100]]', '[6,360,100]', 0, 0);
INSERT INTO `skill` VALUES (4355, 738, 2, 11, 1, 0, 'Bão sấm', 513, 80, 80, 5, 1752, 18, 4200, 5, 'Tạo ra một trận mây sấm diện rộng trên đầu đối thủ.', 31, 0, 0, '[[28,6],[29,140],[30,60]]', '[6,140,60]', 0, 0);

-- Phái 5: Xạ thủ
INSERT INTO `skill` VALUES (4356, 739, 0, 12, 1, 0, 'Double Shot', 501, 120, 120, 1, 662, 0, 1000, 3, 'Kỹ năng bắn súng lục đôi của một xạ thủ hải tặc.', 31, 0, 0, '[[28,4],[29,210],[30,50]]', '[4,210,50]', 0, 0);
INSERT INTO `skill` VALUES (4357, 740, 1, 13, 1, 0, 'Quả cầu lửa', 502, 120, 120, 1, 2524, 11, 1000, 1, 'Sử dụng súng hạng nặng phóng một quả cầu lửa vào đối thủ.', 31, 0, 0, '[[11,250],[13,90],[28,7],[29,360],[30,80]]', '[7,360,80]', 0, 0);
INSERT INTO `skill` VALUES (4358, 741, 2, 14, 1, 0, 'Pháo hoa', 503, 80, 80, 5, 1752, 18, 4200, 5, 'Tạo ra một màn trình diễn pháo hoa gây sát thương diện rộng.', 31, 0, 0, '[[28,7],[29,130],[30,60]]', '[7,130,60]', 0, 0);
