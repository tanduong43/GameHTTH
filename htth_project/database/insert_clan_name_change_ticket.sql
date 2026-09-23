-- ==============================================================================
-- SCRIPT INSERT VẬT PHẨM: VÉ ĐỔI TÊN CLAN (BĂNG HẢI TẶC)
-- Item ID: 1018
-- Icon: 2106 (File data/icon/x4/2106.png - Vé xanh chữ 'f.')
-- ==============================================================================

USE full_db_htth;

SET SQL_SAFE_UPDATES = 0;

-- 1. Xóa dữ liệu cũ nếu đã tồn tại để tránh trùng lặp
DELETE FROM `item4` WHERE `id` = 1018 OR `name` = 'Vé đổi tên Clan';
DELETE FROM `item4_info` WHERE `id` = 1018;

-- 2. Thêm vật phẩm vào bảng item4
-- id: 1018
-- name: 'Vé đổi tên Clan'
-- icon: 2106
-- indexInfoPotion: 1018
-- price: 0
-- priceruby: 500
-- istrade: 1 (Cho phép giao dịch)
-- hpmpother: 67 (Vật phẩm chức năng đặc biệt)
-- timedelay: 0
-- value: 0
-- timeactive: 0
-- nameuse: 'Sử dụng'
INSERT INTO `item4` (`id`, `name`, `icon`, `indexInfoPotion`, `price`, `priceruby`, `istrade`, `hpmpother`, `timedelay`, `value`, `timeactive`, `nameuse`)
VALUES (1018, 'Vé đổi tên Clan', 2106, 1018, 0, 500, 1, 67, 0, 0, 0, 'Sử dụng');

-- 3. Thêm mô tả vật phẩm vào bảng item4_info
INSERT INTO `item4_info` (`id`, `info`)
VALUES (1018, 'Dùng để thay đổi tên Băng Hải Tặc (Clan). Chỉ Thuyền trưởng mới có thể sử dụng.');
