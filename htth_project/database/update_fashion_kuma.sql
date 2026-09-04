-- ==============================================================================
-- CẬP NHẬT THỜI TRANG KUMA (ID 238) KẾT HỢP TRÁI NIKYU NIKYU NO MI
-- ==============================================================================
-- • Bỏ chỉ số chí mạng mặc định [10,120] ra khỏi op của Thời Trang Kuma.
-- • Chỉ khi nào MANG (is_use = true) Thời trang Kuma và ĐÃ ĂN Trái Nikyu Nikyu no Mi,
--   hệ thống mới kích hoạt +10% Chí mạng (par += 100) tương tự Thời trang Đấng / Chấn Thiên.
-- • Cập nhật mô tả hiển thị info chuẩn theo phong cách các thời trang kích hoạt.
-- ==============================================================================

UPDATE `fashiontemplate` 
SET `op` = '[[63,100],[14,100],[12,100]]', 
    `info` = 'Trang phục Kuma\n+10% Giảm miễn thương\n+10% Phản đòn\n+10% Né đòn\n-Tăng 100% sức tấn công bản thân.\n+10% Chí mạng khi kết hợp Trái Nikyu Nikyu\nHạn sử dụng vĩnh viễn'
WHERE `id` = 238;
