const db = require('./db');

async function initNewsTable() {
    try {
        // Check if table news exists and is valid
        let dropTable = false;
        try {
            await db.query("SELECT title FROM news LIMIT 1");
        } catch (err) {
            // Table doesn't exist or is invalid (e.g., missing column 'title')
            dropTable = true;
        }

        if (dropTable) {
            await db.query("DROP TABLE IF EXISTS news");
        }

        // Create table news if it doesn't exist
        const createTableSql = `
            CREATE TABLE IF NOT EXISTS news (
                id INT AUTO_INCREMENT PRIMARY KEY,
                title VARCHAR(255) NOT NULL,
                slug VARCHAR(255) NOT NULL UNIQUE,
                summary TEXT NOT NULL,
                content LONGTEXT NOT NULL,
                thumbnail VARCHAR(500) DEFAULT NULL,
                status VARCHAR(50) DEFAULT 'published',
                created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                published_at DATETIME DEFAULT NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
        `;
        
        await db.query(createTableSql);
        console.log("Database table 'news' checked/created successfully.");

        // Check if there are existing rows
        const [rows] = await db.query("SELECT COUNT(*) as count FROM news");
        if (rows[0].count === 0) {
            console.log("News table is empty. Seeding default news articles...");
            const defaultArticles = [
                {
                    title: "Khai mở Máy Chủ S1 - Làng Cối Xay Gió",
                    slug: "khai-mo-may-chu-s1-lang-coi-xay-gio",
                    summary: "Chào mừng các tân hải tặc giăng buồm ra khơi! S1 chính thức open cùng chuỗi sự kiện Đua Top Cấp Độ, Đua Top Nạp nhận quà tặng Giftcode độc quyền cực giá trị.",
                    content: "<h3>⚓ Chào mừng đến với S1 - Làng Cối Xay Gió!</h3><p>Thế Giới Hải Tặc chính thức khai mở máy chủ đầu tiên <strong>S1 - Làng Cối Xay Gió</strong> vào lúc 10:00 ngày 04/07/2026. Đây là vùng đất khởi đầu đầy hứa hẹn cho mọi tân hải tặc đam mê chinh phục Đại Hải Trình.</p><h4>🎁 Chuỗi sự kiện chào mừng máy chủ mới:</h4><ul><li><strong>Đua Top Cấp Độ:</strong> Từ ngày 04/07 đến 18/07, phần quà là danh hiệu độc quyền tăng 10% sát thương.</li><li><strong>Gói Quà Tân Thủ:</strong> Nhập ngay mã giftcode <code>TANTHUHTTH</code> để nhận ngay trang bị kiếm gỗ, 10,000 Coin và nhiều vật phẩm giá trị khác.</li></ul><p>Hãy nhanh chóng rủ rê đồng đội, tạo nhân vật và cùng nhau phiêu lưu ngay hôm nay!</p>",
                    thumbnail: "/banner_adventure.png",
                    status: "published",
                    published_at: "2026-07-04 10:00:00"
                },
                {
                    title: "Đua Top Cấp Độ Nhận Danh Hiệu Độc Quyền",
                    slug: "dua-top-cap-do-nhan-danh-hieu-doc-quyen",
                    summary: "Thời gian diễn ra từ 04/07 đến hết 18/07. Top 3 anh hùng đạt cấp độ cao nhất sẽ nhận được Danh hiệu Thần thoại tăng 10% sát thương cùng quà tặng hiện kim.",
                    content: "<h3>🏆 SỰ KIỆN ĐUA TOP CẤP ĐỘ - TRANH HÙNG LÀNG CỐI XAY GIÓ</h3><p>Sự kiện đua top cấp độ nhằm tìm kiếm những thuyền trưởng xuất chúng nhất của máy chủ S1. Ai sẽ là người đầu tiên chinh phục được các thử thách cực đại?</p><h4>⏰ Thời gian sự kiện:</h4><p>Bắt đầu từ <strong>10:00 ngày 04/07/2026</strong> đến <strong>23:59 ngày 18/07/2026</strong>.</p><h4>🎁 Phần thưởng:</h4><ol><li><strong>TOP 1:</strong> Danh hiệu 'Vua Hải Tặc' (Tăng 10% ATK, 5% HP) + 500,000 Coin.</li><li><strong>TOP 2:</strong> Danh hiệu 'Phó Thuyền Trưởng' (Tăng 7% ATK, 3% HP) + 300,000 Coin.</li><li><strong>TOP 3:</strong> Danh hiệu 'Hoa Tiêu Tài Ba' (Tăng 5% ATK) + 150,000 Coin.</li></ol><p>Quyết định xếp hạng sẽ dựa trên cấp độ (level) của nhân vật được ghi nhận trên hệ thống vào thời điểm kết thúc sự kiện. Chúc các bạn may mắn!</p>",
                    thumbnail: "/banner_pvp.png",
                    status: "published",
                    published_at: "2026-07-02 09:00:00"
                },
                {
                    title: "Lịch Bảo Trì Định Kỳ & Cân Bằng Sức Mạnh Tướng",
                    slug: "lich-bao-tri-dinh-ky-va-can-bang-suc-manh-tuong",
                    summary: "Bản cập nhật cân bằng lại kỹ năng nộ của Class Hoa Tiêu, tối ưu hóa tốc độ load bản đồ trong game và bảo trì nâng cấp cấu hình máy chủ tránh tình trạng giật lag.",
                    content: "<h3>🔧 THÔNG BÁO BẢO TRÌ ĐỊNH KỲ VÀ CẬP NHẬT PHIÊN BẢN</h3><p>Ban Quản Trị Thế Giới Hải Tặc xin thông báo lịch bảo trì hệ thống định kỳ nhằm tối ưu hóa máy chủ và nâng cấp tính năng mới.</p><h4>⏰ Thời gian bảo trì dự kiến:</h4><p>Từ <strong>05:00 đến 07:00 ngày 05/07/2026</strong>.</p><h4>📝 Nội dung cập nhật:</h4><ul><li><strong>Cân bằng môn phái:</strong> Điều chỉnh kỹ năng nộ 'Bão Sét' của Class Hoa Tiêu (giảm 5% sát thương phép để cân bằng sức mạnh PK).</li><li><strong>Tối ưu hóa:</strong> Tăng tốc độ tải bản đồ Làng Cocoyasi và Đảo Sương Mù.</li><li><strong>Bảo trì server:</strong> Nâng cấp phần cứng máy chủ trung tâm để giảm thiểu tối đa hiện tượng giật lag khi đông người chơi.</li></ul><p>Rất mong quý thuyền trưởng lưu ý thời gian bảo trì để sắp xếp lộ trình phiêu lưu hợp lý. Trân trọng!</p>",
                    thumbnail: "/banner_update.png",
                    status: "published",
                    published_at: "2026-06-30 08:30:00"
                }
            ];

            for (const article of defaultArticles) {
                await db.query(
                    "INSERT INTO news (title, slug, summary, content, thumbnail, status, published_at) VALUES (?, ?, ?, ?, ?, ?, ?)",
                    [article.title, article.slug, article.summary, article.content, article.thumbnail, article.status, article.published_at]
                );
            }
            console.log("Successfully seeded 3 default articles.");
        }
    } catch (err) {
        console.error("Error initializing news table:", err.message);
    }
}

// Automatically invoke if required directly, or let server.js run it
module.exports = initNewsTable;
