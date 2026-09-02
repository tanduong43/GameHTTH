-- =======================================================
-- LỆNH THÊM TIN TỨC HƯỚNG DẪN SỰ KIỆN TRUNG THU CHO WEB HTTH
-- BẢNG: news
-- =======================================================

INSERT INTO `news` (
    `title`, `slug`, `summary`, `content`, `thumbnail`, `status`, `published_at`,
    `name`, `text`, `seo`, `time`, `avt`, `kom`
)
VALUES (
    '🥮 [Sự Kiện] Đêm Rằm Hải Tặc - Vui Hội Trung Thu 2026',
    'su-kien-dem-ram-hai-tac-vui-hoi-trung-thu-2026',
    'Hòa chung không khí rộn ràng của Tết Trung Thu, BQT HTTH xin gửi tới chuỗi sự kiện Đêm Rằm Hải Tặc: Làm Bánh Trung Thu, Ghép Đèn Kéo Quân, Săn Boss Lân Sư Tử, Nhận Trang Phục Chú Cuội & Chị Hằng Vĩnh Viễn cùng vô vàn phần thưởng hấp dẫn!',
    '
<div class="event-news-container">
    <h3>🥮 SỰ KIỆN TRUNG THU: ĐÊM RẰM HẢI TẶC 2026</h3>
    <p>Chào các Thuyền Trưởng thân mến! Nhân dịp Tết Trung Thu cổ truyền, Ban Quản Trị <strong>Hải Tặc Tí Hon (HTTH)</strong> xin trân trọng giới thiệu chuỗi sự kiện đặc sắc mang tên <strong>"Đêm Rằm Hải Tặc"</strong> với hàng loạt hoạt động thú vị, cơ hội săn tìm thời trang Chú Cuội &amp; Chị Hằng vĩnh viễn cùng nhiều vật phẩm quý hiếm!</p>

    <hr/>

    <h4>⏰ 1. THỜI GIAN & ĐỊA ĐIỂM SỰ KIỆN</h4>
    <ul>
        <li><strong>Thời gian:</strong> Diễn ra trong suốt mùa lễ hội Trung Thu 2026.</li>
        <li><strong>Địa điểm:</strong> Toàn bộ các máy chủ HTTH, NPC Chị Hằng tại Làng Cối Xay Gió và các bản đồ dã ngoại.</li>
    </ul>

    <hr/>

    <h4>🌾 2. HOẠT ĐỘNG 1: THU THẬP NGUYÊN LIỆU LÀM BÁNH</h4>
    <p>Trong thời gian diễn ra sự kiện, người chơi tham gia các hoạt động thường ngày để thu thập nguyên liệu:</p>
    <table border="1" cellpadding="8" cellspacing="0" style="width:100%; border-collapse:collapse; text-align:left; margin-bottom:15px;">
        <thead style="background-color:#E67E22; color:#ffffff;">
            <tr>
                <th style="padding:8px;">Hoạt động ingame</th>
                <th style="padding:8px;">Điều kiện hoàn thành</th>
                <th style="padding:8px;">Nguyên liệu nhận được</th>
            </tr>
        </thead>
        <tbody>
            <tr style="background-color:#fdfefe;">
                <td style="padding:8px;"><strong>Đánh quái dã ngoại</strong></td>
                <td style="padding:8px;">Tiêu diệt quái chênh lệch ≤ 10 cấp độ</td>
                <td style="padding:8px;">Rơi: <strong>Bột Mì</strong> (tối đa 100 cái / ngày)</td>
            </tr>
            <tr style="background-color:#f9f9f9;">
                <td style="padding:8px;"><strong>Nhiệm vụ lặp hằng ngày</strong></td>
                <td style="padding:8px;">Hoàn thành và trả nhiệm vụ lặp</td>
                <td style="padding:8px;">Nhận: <strong>Đường</strong></td>
            </tr>
            <tr style="background-color:#fdfefe;">
                <td style="padding:8px;"><strong>Nhiệm vụ Bang / PvP / Truy Nã</strong></td>
                <td style="padding:8px;">Hoàn thành nhiệm vụ Bang, thắng Đấu Trường, diệt Boss Truy Nã</td>
                <td style="padding:8px;">Nhận: <strong>Trứng Muối</strong></td>
            </tr>
            <tr style="background-color:#f9f9f9;">
                <td style="padding:8px;"><strong>Phó bản Nami & Đá Đít Mr.3</strong></td>
                <td style="padding:8px;">Vượt ải phó bản thành công</td>
                <td style="padding:8px;">Nhận: <strong>Đèn Ông Sao</strong></td>
            </tr>
            <tr style="background-color:#fdfefe;">
                <td style="padding:8px;"><strong>Săn Boss Lân & Vận Buôn</strong></td>
                <td style="padding:8px;">Tiêu diệt Boss Lân Sư Tử hoặc hoàn thành chuyến buôn</td>
                <td style="padding:8px;">Nhận: <strong>Giấy Gói Quà</strong></td>
            </tr>
        </tbody>
    </table>

    <hr/>

    <h4>🥮 3. HOẠT ĐỘNG 2: CHẾ TẠO BÁNH & GHÉP ĐÈN TẠI NPC CHỊ HẰNG</h4>
    <p>Mang nguyên liệu đến gặp <strong>NPC Chị Hằng (ID -154)</strong> để tiến hành làm bánh và ghép đèn trung thu:</p>
    <table border="1" cellpadding="8" cellspacing="0" style="width:100%; border-collapse:collapse; text-align:left; margin-bottom:15px;">
        <thead style="background-color:#D35400; color:#ffffff;">
            <tr>
                <th style="padding:8px;">Tên thành phẩm</th>
                <th style="padding:8px;">Nguyên liệu cần nộp</th>
                <th style="padding:8px;">Phí Beri / Ruby</th>
                <th style="padding:8px;">Hiệu quả / Phần thưởng nhận được</th>
            </tr>
        </thead>
        <tbody>
            <tr style="background-color:#fdfefe;">
                <td style="padding:8px;"><strong>🥮 Bánh Trung Thu</strong></td>
                <td style="padding:8px;">5 Bột Mì + 3 Đường</td>
                <td style="padding:8px;">500,000 Beri</td>
                <td style="padding:8px;">Sử dụng nhận Beri, Bột Vàng, Ngôi Sao May Mắn</td>
            </tr>
            <tr style="background-color:#f9f9f9;">
                <td style="padding:8px;"><strong>🥮 Bánh Đậu Xanh</strong></td>
                <td style="padding:8px;">5 Bột Mì + 3 Đường + 1 Trứng Muối</td>
                <td style="padding:8px;">1,000,000 Beri</td>
                <td style="padding:8px;">Sử dụng nhận Beri, Mai Rùa, Bột Vàng</td>
            </tr>
            <tr style="background-color:#fdfefe;">
                <td style="padding:8px;"><strong>🥮 Bánh Trứng Muối</strong></td>
                <td style="padding:8px;">5 Bột Mì + 3 Đường + 2 Trứng Muối</td>
                <td style="padding:8px;">15,000,000 Beri</td>
                <td style="padding:8px;">Sử dụng nhận Beri lớn, Đá Khảm cấp 3-4, Khiên Bảo Vệ</td>
            </tr>
            <tr style="background-color:#f9f9f9;">
                <td style="padding:8px;"><strong>🥮 Bánh Hạt Sen</strong></td>
                <td style="padding:8px;">5 Bột Mì + 3 Đường + 3 Trứng Muối</td>
                <td style="padding:8px;">2,000,000 Beri</td>
                <td style="padding:8px;">Sử dụng nhận Beri, Đá Khảm, Búa Cường Hóa</td>
            </tr>
            <tr style="background-color:#fdfefe;">
                <td style="padding:8px;"><strong>🏮 Đèn Kéo Quân</strong></td>
                <td style="padding:8px;">3 Đèn Ông Sao</td>
                <td style="padding:8px;">2,000,000 Beri</td>
                <td style="padding:8px;">Sử dụng nhận Rương Cam theo cấp độ, Đá Ác Quỷ</td>
            </tr>
            <tr style="background-color:#f9f9f9;">
                <td style="padding:8px;"><strong>🎁 Hộp Bánh Trung Thu</strong></td>
                <td style="padding:8px;">1 Bánh TT + 1 Đậu Xanh + 1 Trứng Muối + 1 Hạt Sen</td>
                <td style="padding:8px;">2,000,000 Beri + 50 Ruby</td>
                <td style="padding:8px;">Mở nhận Beri khủng, Ruby, Đá khảm 4-5, Bùa cường hóa, Khiên bảo vệ</td>
            </tr>
            <tr style="background-color:#fdfefe;">
                <td style="padding:8px;"><strong>🏆 Hộp Bánh Thượng Hạng</strong></td>
                <td style="padding:8px;">1 Hộp Bánh + 1 Giấy Gói Quà</td>
                <td style="padding:8px;">2,000,000 Beri + 100 Ruby</td>
                <td style="padding:8px;">Mở nhận Đá Vô Cực S, Đá Hải Thạch 5-6, Rương Đại Ác Quỷ, <strong>Thẻ Thời Trang Trung Thu</strong></td>
            </tr>
        </tbody>
    </table>

    <hr/>

    <h4>🦁 4. HOẠT ĐỘNG 3: SĂN BOSS GIỜ VÀNG "LÂN SƯ TỬ"</h4>
    <p>Thử thách đánh Boss toàn server:</p>
    <ul>
        <li><strong>Thời gian xuất hiện:</strong> Vào các khung giờ <strong>12:00, 18:00, 20:00, 22:00</strong> hằng ngày tại bản đồ dã ngoại ngẫu nhiên.</li>
        <li><strong>Phần thưởng kết liễu (Last Hit):</strong> Thuyền trưởng tung đòn đánh kết liễu Boss Lân Sư Tử sẽ nhận ngay:
            <ul>
                <li>📜 <strong>Giấy Gói Quà</strong></li>
                <li>🎁 <strong>Hộp Bánh Thượng Hạng</strong></li>
                <li>👘 <strong>Thẻ Thời Trang Trung Thu</strong> (Đổi Trang phục Chú Cuội / Chị Hằng Vĩnh Viễn)</li>
            </ul>
        </li>
    </ul>

    <hr/>

    <h4>👘 5. THỜI TRANG ĐỘC QUYỀN TRUNG THU (VĨNH VIỄN)</h4>
    <p>Sử dụng <strong>Thẻ Thời Trang Trung Thu</strong> tại NPC Chị Hằng để đổi lấy trang phục độc quyền:</p>
    <ul>
        <li>🥋 <strong>Trang Phục Chú Cuội:</strong> Gia tăng Né tránh, Máu (HP), Xuyên giáp và Miễn thương.</li>
        <li>💃 <strong>Trang Phục Chị Hằng:</strong> Gia tăng Chí mạng, Sát thương, Máu (HP) và Xuyên giáp.</li>
    </ul>

    <div style="background-color:#fef9e7; border-left:4px solid #F39C12; padding:12px; margin:15px 0;">
        <p style="margin:0; font-weight:bold; color:#B7950B;">🌟 Bảng Xếp Hạng Giết Lân & Nấu Bánh:</p>
        <p style="margin:5px 0 0 0;">Người chơi có thể theo dõi trực tiếp thành tích săn Boss Lân Sư Tử và số lượng Bánh Trung Thu đã nấu tại menu BXH NPC Chị Hằng để cùng đua tài cùng các thuyền trưởng khác!</p>
    </div>

    <p style="text-align:right;"><strong>Ban Quản Trị Thế Giới Hải Tặc (HTTH)</strong><br/><em>Kính chúc các Thuyền Trưởng một mùa Trung Thu Vui Vẻ, Đầm Ấm & Bội Thu!</em></p>
</div>
',
    '/banner_pvp.png',
    'published',
    '2026-09-02 10:00:27',
    '',
    '',
    '',
    1788318027,
    '',
    0
)
ON DUPLICATE KEY UPDATE
    `title` = VALUES(`title`),
    `summary` = VALUES(`summary`),
    `content` = VALUES(`content`),
    `thumbnail` = VALUES(`thumbnail`),
    `status` = VALUES(`status`),
    `published_at` = VALUES(`published_at`),
    `time` = VALUES(`time`);
