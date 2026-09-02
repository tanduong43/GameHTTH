# 🎄 KẾ HOẠCH TỔNG THỂ THIẾT KẾ SỰ KIỆN NOEL (GIÁNG SINH) - GAME HTTH
# "GIÁNG SINH AN LÀNH - ĐẠI HẢI TRÌNH TUYẾT TRẮNG"

---

## MỤC LỤC
1. [Tổng Quan & Cốt Truyện Sự Kiện](#phần-1-tổng-quan--cốt-truyện-sự-kiện)
2. [Tra Cứu Toàn Bộ Tài Nguyên & Bảng Dữ Liệu (Database)](#phần-2-tra-cứu-toàn-bộ-tài-nguyên--bảng-dữ-liệu-database)
   - [2.1. Bảng `item4` (Nguyên Liệu, Rương, Hộp Quà & Pet Noel)](#21-bảng-item4-nguyên-liệu-rương-hộp-quà--pet-noel)
   - [2.2. Bảng `item8` (Vật Phẩm Bang Hội - Cây Thông Noel)](#22-bảng-item8-vật-phẩm-bang-hội---cây-thông-noel)
   - [2.3. Bảng `fashiontemplate` (Thời Trang Noel & Mùa Đông Độc Quyền)](#23-bảng-fashiontemplate-thời-trang-noel--mùa-đông-độc-quyền)
   - [2.4. Bảng `pet_template` (Thú Cưng Giáng Sinh)](#24-bảng-pet_template-thú-cưng-giáng-sinh)
   - [2.5. Bảng `danhhieu` (Danh Hiệu Noel & Đua Top)](#25-bảng-danhhieu-danh-hiệu-noel--đua-top)
   - [2.6. Bảng `mobs` & `boss` (Quái Vật & Boss Quái Tuyết Giờ Vàng)](#26-bảng-mobs--boss-quái-vật--boss-quái-tuyết-giờ-vàng)
3. [Chuỗi 5 Hoạt Động Sự Kiện Noel Liên Hoàn](#phần-3-chuỗi-5-hoạt-động-sự-kiện-noel-liên-hoàn)
   - [Hoạt Động 1: Cần Cù Gom Tuyết (Thu Thập Nguyên Liệu Qua 7 Tính Năng Ingame)](#hoạt-động-1-cần-cù-gom-tuyết-thu-thập-nguyên-liệu-ingame)
   - [Hoạt Động 2: Đắp Người Tuyết & Gói Quà Giáng Sinh Tại NPC](#hoạt-động-2-đắp-người-tuyết--gói-quà-giáng-sinh-tại-npc)
   - [Hoạt Động 3: Thắp Sáng Cây Thông Noel Bang Hội & Triệu Hồi Quái Tuyết](#hoạt-động-3-thắp-sáng-cây-thông-noel-bang-hội)
   - [Hoạt Động 4: Đại Chiến Boss Giờ Vàng "Quái Vật Tuyết" (Cơ Chế 1 Hit = 1 Dame)](#hoạt-động-4-đại-chiến-boss-giờ-vàng-quái-vật-tuyết)
   - [Hoạt Động 5: Đua Top BXH "Vua Giáng Sinh" & Tự Nhận Thưởng Chống Trùng](#hoạt-động-5-đua-top-bxh-vua-giáng-sinh)
4. [Hệ Thống Công Thức Chế Tạo Chi Tiết Tại NPC Sự Kiện](#phần-4-hệ-thống-công-thức-chế-tạo-chi-tiết-tại-npc-sự-kiện)
5. [Chi Tiết Phần Thưởng Mở Quà & Cơ Cấu Đua Top BXH](#phần-5-chi-tiết-phần-thưởng-mở-quà--cơ-cấu-đua-top-bxh)
6. [Cơ Chế Bật / Tắt Sự Kiện & Hệ Thống Lệnh Quản Trị GM Ingame](#phần-6-cơ-chế-bật--tắt-sự-kiện--hệ-thống-lệnh-quản-trị-gm-ingame)
7. [Kế Hoạch Triển Khai Kỹ Thuật (Architecture & Source Code Files)](#phần-7-kế-hoạch-triển-khai-kỹ-thuật)

---

## PHẦN 1: TỔNG QUAN & CỐT TRUYỆN SỰ KIỆN

* **Tên Sự Kiện:** **GIÁNG SINH AN LÀNH - ĐẠI HẢI TRÌNH TUYẾT TRẮNG**
* **Chủ đề:** Chào đón Lễ Giáng Sinh (Noel) & Năm Mới.
* **Thời gian diễn ra:** Mùa Giáng Sinh (Cấu hình Bật/Tắt linh hoạt qua file config và lệnh GM realtime).
* **Bối cảnh & Cốt truyện:**
  > Mùa đông tuyết trắng đã phủ kín khắp các hòn đảo trên Đại Hải Trình. Từ vùng biển Đông Hải đến Đảo Mùa Đông Drum, không khí lễ hội ngập tràn tiếng chuông ngân. Tuy nhiên, sự xuất hiện của bầy **Quái Vật Tuyết** khổng lồ đang đe dọa các chuyến tàu chở quà của Ông Già Noel. Các Băng Hải Tặc khắp nơi cùng nhau lên đường thu thập những nắm tuyết, chuông vàng, tất đỏ để trang trí cây thông, làm quà giáng sinh và săn lùng Quái Vật Tuyết nhằm tranh đoạt ngôi vị **Vua Giáng Sinh**.
* **Nguyên tắc kỹ thuật tối cao:**
  * **100% sử dụng Item & Tài nguyên có sẵn trong database (`htth.sql` / `updatepet2708.sql`)**, bảo đảm không lỗi hiển thị (icon), không mất đồng bộ client và không crash server.
  * Tận dụng đầy đủ hệ thống Pet Noel, Thời trang Tuyết, Boss Người Tuyết/Quái Tuyết, Cây Thông Bang Hội đã có trong DB.

---

## PHẦN 2: TRA CỨU TOÀN BỘ TÀI NGUYÊN & BẢNG DỮ LIỆU (DATABASE)

Toàn bộ các vật phẩm dưới đây đã được quét và xác thực trực tiếp từ cơ sở dữ liệu gốc của server:

### 2.1. Bảng `item4` (Nguyên Liệu, Rương, Hộp Quà & Pet Noel)

| ID Item | Tên Hiển Thị Trong DB | Icon | Phân Loại | Vai Trò & Cách Thức Hoạt Động Trong Sự Kiện Noel |
|:---:|:---|:---:|:---|:---|
| **341** | `Nắm tuyết` | 294 | Nguyên liệu | Thu thập từ đánh quái thường ($\pm 10$ cấp độ) & tham gia hoạt động. Dùng ném tuyết hoặc ghép Người Tuyết & Thiệp. |
| **342** | `Cúc áo` | 295 | Nguyên liệu | Nhận từ Nhiệm Vụ Lặp hằng ngày. Dùng ghép Người Tuyết & Thiệp Giáng Sinh. |
| **347** | `Nón giáng sinh` | 300 | Nguyên liệu | Nhận từ Phó Bản Nami, Đá Đít Mr.3 & Vệ Thần Wipper. Dùng hoàn thiện Người Tuyết. |
| **486** | `Chuông giáng sinh`| 431 | Nguyên liệu | Nhận từ Nhiệm Vụ Băng, Phó Bản Băng & Vận Buôn. Dùng trang trí Cây Thông & ghép Hộp Quà Noel. |
| **487** | `Vớ giáng sinh` | 432 | Nguyên liệu | Nhận từ Trả Nhiệm Vụ Lặp & Hang Động Liên Tầng. Dùng trang trí Cây Thông & ghép Hộp Quà Noel. |
| **488** | `Ngôi sao giáng sinh`| 433 | Nguyên liệu quý | Nhận từ Đấu Trường PvP Ms Gym, Lôi Đài, Boss Truy Nã. Dùng trang trí Cây Thông & ghép Hộp Quà VIP. |
| **489** | `Kẹo giáng sinh` | 437 | Tiêu hao / NL | Rơi từ quái dã ngoại. Sử dụng: Hồi 100% HP/MP + Buff 10% Tốc chạy trong 15 phút. |
| **575** | `Giấy gói quà` | 104 | Nguyên liệu | Nhận từ Chuyến Buôn Đường Biển & Phó bản Nami. Dùng gói Hộp Quà Noel. |
| **590** | `Gấu bông` | 564 | Nguyên liệu | Nhận từ Thử Thách Vệ Thần Wipper. Dùng ghép Hộp Quà Giáng Sinh VIP. |
| **611** | `Bóng tuyết` | 294 | Đạo cụ | Rơi từ quái dã ngoại. Dùng ném tuyết gây hiệu ứng làm chậm mục tiêu. |
| **340** | `Thiệp giáng sinh`| 293 | Thành phẩm | Ghép tại NPC. Sử dụng: Nhận EXP Khủng + Beri + Buff 30% EXP trong 30 phút. |
| **227** | `Hộp quà Noel` | 183 | Hộp quà | Ghép từ Chuông + Vớ + Giấy gói quà. Mở nhận Beri, Ruby, Đá khảm 3-4, Rương Cam + **1 Điểm BXH**. |
| **485** | `Túi giáng sinh` | 436 | Hộp quà | Nhận từ Chuyến Buôn & Hoạt động. Mở nhận Beri, Ruby, Đá Hải Thạch 3-4 + **2 Điểm BXH**. |
| **492** | `Hộp quà giáng sinh`| 438 | Hộp quà VIP | Ghép từ Hộp Quà Noel + Gấu Bông + Ngôi Sao. Mở nhận Quà VIP, Đá Khảm 6, Trái Ác Quỷ + **5 Điểm BXH**. |
| **622** | `Rương trang phục noel`| 184 | Rương VIP | Mở nhận ngẫu nhiên Trang phục Noel / Hoàng Tử Tuyết / Công Chúa Tuyết vĩnh viễn. |
| **229** | `Vé đổi trang phục Noel`| 186 | Vé Event | Thu thập để đổi trực tiếp Thời trang Noel vĩnh viễn tại NPC. |
| **230** | `Vé Noel` | 185 | Vé Event | Vé quay thưởng / Đổi Rương Pet Noel tại NPC. |
| **168** | `Vé triệu hồi quái tuyết`| 122 | Đạo cụ gọi Boss| Sử dụng tại Map để triệu hồi Quái Vật Tuyết cho cả bang/nhóm săn. |
| **348** | `Triệu hồi tuyết nhỏ`| 301 | Đạo cụ | Triệu hồi quái Tuyết Nhỏ thử thách tân thủ. |
| **708** | `Lộc Noel` | 661 | Pet Noel | Thú cưng Lộc Noel độc quyền mùa Giáng Sinh. |
| **709** | `Tuyết Noel` | 662 | Pet Noel | Thú cưng Người Tuyết Noel tăng chỉ số đặc biệt. |
| **720** | `Tuần lộc` | 663 | Pet Noel | Thú cưng Tuần Lộc kéo xe Noel. |
| **1011**| `Rương Pet Noel` | 183 | Rương Pet VIP | Mở ra nhận 1 trong 3 Pet Noel: Lộc Noel (708), Tuyết Noel (709), Tuần Lộc (720). |
| **1004**| `Rương đá thần thoại tự chọn`| 172 | Rương Đua Top | Mở ra menu chọn 1 trong 36 loại Đá Thần Thoại (ID 647 - 682). |
| **1002**| `Hộp thời trang cao cấp`| 187 | Rương Đua Top | Mở nhận thời trang cao cấp vĩnh viễn. |
| **326** | `Đá khảm vô cực S`| 280 | Đá Đua Top | Đá khảm thần cấp cộng chỉ số sức mạnh cực lớn. |
| **158** | `Rương đại ác quỷ`| 112 | Rương Đua Top | Mở nhận Trái Ác Quỷ Thượng Cấp. |
| **29**  | `Rương ác quỷ` | 112 | Rương Đua Top | Mở nhận Trái Ác Quỷ Trung Cấp. |

---

### 2.2. Bảng `item8` (Vật Phẩm Bang Hội - Cây Thông Noel)

| ID Item | Tên Hiển Thị | Icon | Phân Loại | Công Dụng Trong Sự Kiện Bang Hội Noel |
|:---:|:---|:---:|:---|:---|
| **9** | **Cây thông Noel** | 123 | Đạo cụ Clan | Đặt tại Lãnh Địa Bang. Toàn bang góp Chuông & Vớ để thắp sáng cây thông, kích hoạt **Buff 30% EXP & +10% Sát thương toàn bang**. |

---

### 2.3. Bảng `fashiontemplate` (Thời Trang Noel & Mùa Đông Độc Quyền)

| ID Fashion | Tên Thời Trang | Icon | Bộ Phận Hiển Thị (mwear) | Chỉ Số Sức Mạnh (Option) | Thời Hạn |
|:---:|:---|:---:|:---|:---|:---:|
| **75** | **Hoàng tử tuyết** | 72 | `[-1,861,-1,862,-1,863,-1,-1]` | **+13% Né tránh, +8% HP, +10% Miễn thương** | Vĩnh viễn (Thưởng Quán Quân Top 1) |
| **76** | **Công chúa tuyết**| 73 | `[-1,864,-1,865,-1,866,-1,-1]` | **+13% Né tránh, +8% HP, +10% Miễn thương** | Vĩnh viễn (Thưởng Top 2-3) |
| **45** | **Noel Nữ** | 44 | `[-1,682,-1,685,-1,686,-1,-1]` | **+6% Né tránh, +6% Xuyên giáp, +10% Máu** | Vĩnh viễn / Đổi tại NPC |
| **46** | **Bé tuyết** | 45 | `[-1,691,-1,692,-1,693,-1,-1]` | **+9% Chí mạng, +9% Xuyên giáp, +10% Máu** | Vĩnh viễn / Mở Rương Quà |
| **3 - 7**| **Áo Len 1 - 5** | 3-7 | `[-1,362..372,-1,363..373,-1,-1]` | **+10% Giảm hồi chiêu, +5% Máu, +5% Giáp** | Vĩnh viễn / Giữ ấm mùa đông |
| **9 - 13**| **Áo Choàng 1 - 5**| 8-12| `[-1,375..385,-1,376..386,-1,-1]` | **+5% Giáp, +5% Kháng Vật lý/Phép, +10% Máu**| Vĩnh viễn / Mùa đông |

---

### 2.4. Bảng `pet_template` (Thú Cưng Giáng Sinh)

| ID Pet | Tên Pet | Icon | Sprite ID | Hệ / Loại | Hiệu Ứng Thuộc Tính Hỗ Trợ |
|:---:|:---|:---:|:---:|:---:|:---|
| **22** | **Lộc Noel** | 626 | 82 | Hệ Băng VIP | +10% Kháng tất cả thuộc tính, Tăng 15% Beri khi đánh quái, Buff may mắn. |
| **23** | **Tuyết Noel**| 627 | 83 | Hệ Băng VIP | +8% Tỷ lệ Né tránh, Hồi phục 2% HP mỗi 5 giây trong giao tranh. |
| **33** | **Tuần lộc** | 639 | 984 | Hệ Hỗ Trợ | +10% Tốc độ di chuyển, +5% Máu tối đa. |

---

### 2.5. Bảng `danhhieu` (Danh Hiệu Noel & Đua Top)

| ID | Tên Danh Hiệu | Thuộc Tính / Chỉ Số | Nguồn Đạt Được |
|:---:|:---|:---|:---|
| **9** | **Thú Cưng Noel** | Tăng toàn diện chỉ số hỗ trợ thú cưng & kháng băng | Trao thưởng Đua Top 1 - 3 & Hoàn thành chuỗi nhiệm vụ Noel |
| **7** (67)| **Đại Thần** | Tăng cực mạnh toàn bộ thuộc tính chiến đấu (Vĩnh viễn) | Thưởng Quán Quân **TOP 1** Đua Top Noel |
| **8** (68)| **Thiên Tử** | Tăng toàn diện chỉ số công & thủ (Vĩnh viễn) | Thưởng **TOP 2 - 3** Đua Top Noel |
| **4** (64)| **Bất Bại** | Tăng tỷ lệ miễn thương & phòng ngự | Thưởng **TOP 4 - 10** Đua Top Noel |

---

### 2.6. Bảng `mobs` & `boss` (Quái Vật & Boss Quái Tuyết Giờ Vàng)

| Mob ID | Tên Boss / Quái | Bảng DB | Level | HP Cơ Bản | Cơ Chế Sát Thương & Xuất Hiện | Phần Thưởng Tiêu Diệt |
|:---:|:---|:---:|:---|:---|:---|:---|
| **99** | **Boss Quái Vật Tuyết** | `mobs` / `boss` | 99 | 100,000,000 HP | • Xuất hiện lúc **12h, 18h, 20h, 22h** tại bản đồ dã ngoại ngẫu nhiên.<br>• **Cơ chế: 1 Hit = 1 Dame (trừ đúng 1 HP/đòn đánh)**. | **Đòn kết liễu (Last Hit):**<br>• 1 `Rương Pet Noel` (1011)<br>• 1 `Hộp Quà Giáng Sinh VIP` (492)<br>• 3 `Vé Đổi Trang Phục Noel` (229) |
| **98** | **Người Tuyết** | `mobs` | 99 | 556,400 HP | Xuất hiện khi người chơi đắp Người Tuyết thành công hoặc triệu hồi tại làng. | Rơi `Kẹo Giáng Sinh` (489), `Hộp Quà Noel` (227), Beri, Ruby. |
| **134**| **Tuyết Nhỏ** | `mobs` | 50 | 50,000 HP | Triệu hồi từ vật phẩm `Triệu hồi tuyết nhỏ` (348). | Rơi `Nắm Tuyết` (341), `Bóng Tuyết` (611). |

---

## PHẦN 3: CHUỖI 5 HOẠT ĐỘNG SỰ KIỆN NOEL LIÊN HOÀN

### HOẠT ĐỘNG 1: CẦN CÙ GOM TUYẾT (THU THẬP NGUYÊN LIỆU INGAME)

Phân bổ nguồn nguyên liệu qua 7 tính năng chính nhằm kích cầu toàn bộ gameplay của máy chủ:

1. **⚔️ Đánh Quái Dã Ngoại (Train Level):**
   * Tiêu diệt quái vật chênh lệch $\le 10$ cấp độ có tỷ lệ rơi: `Nắm tuyết` (341), `Bóng tuyết` (611) và `Kẹo giáng sinh` (489).
2. **📜 Nhiệm Vụ Lặp Hằng Ngày (Daily Repeat Quest):**
   * Trả mỗi nhiệm vụ lặp nhận thêm: **1 `Cúc áo` (342) + 1 `Vớ giáng sinh` (487)**.
3. **🏴‍☠️ Nhiệm Vụ Băng & Phó Bản Băng Hải Tặc:**
   * Hoàn thành nhiệm vụ băng và vượt ải phó bản băng nhận: **2 `Chuông giáng sinh` (486) + 3 `Nắm tuyết` (341)**.
4. **🏰 Phó Bản Đá Đít Mr.3 & Bảo Vệ Kho Báu Nami:**
   * Vượt ải nhận: **2 `Giấy gói quà` (575) + 1 `Nón giáng sinh` (347)**.
5. **🏹 Thử Thách Vệ Thần Wipper & Hang Động Liên Tầng:**
   * Vượt ải nhận thêm: **1 `Gấu bông` (590) + 1 `Nón giáng sinh` (347)**.
6. **🥊 Đấu Trường (Ms Gym) / Lôi Đài PvP / Boss Truy Nã:**
   * Chiến thắng nhận: **1 `Ngôi sao giáng sinh` (488) + 1 `Vé Noel` (230)**.
7. **⛵ Chuyến Buôn Đường Biển (Sea Trade):**
   * Hoàn thành chuyến hàng buôn thành công nhận: **3 `Giấy gói quà` (575) + 1 `Vé đổi trang phục Noel` (229) + Beri & Ruby**.

---

### HOẠT ĐỘNG 2: ĐẮP NGƯỜI TUYẾT & GÓI QUÀ GIÁNG SINH TẠI NPC

* **Vị trí NPC:** **NPC Sự Kiện (ID -100)** đặt tại tất cả các Làng & Thị trấn trung tâm.
* **Menu tương tác:**
  * `1. Đắp Người Tuyết & Làm Thiệp Noel`
  * `2. Gói Hộp Quà Giáng Sinh`
  * `3. Đổi Trang Phục Noel & Pet Giáng Sinh`
  * `4. Bảng Xếp Hạng Vua Giáng Sinh`
  * `5. Nhận Thưởng Đua Top Noel`
  * `6. Hướng Dẫn Sự Kiện`

---

### HOẠT ĐỘNG 3: THẮP SÁNG CÂY THÔNG NOEL BANG HỘI

* Bang chủ / Đội phó sử dụng **`Cây thông Noel` (Item8 ID 9)** để dựng cây thông tại Lãnh Địa Bang.
* Toàn thể thành viên trong bang nộp `Chuông giáng sinh` (486) và `Vớ giáng sinh` (487) để nâng cấp cây thông:
  * Đạt mốc 100 Chuông + 100 Vớ: Cây thông bừng sáng, kích hoạt **Buff 30% EXP đánh quái & +10% Sát thương toàn bang** trong 24 giờ.
  * Mỗi lần nộp nhận được Beri, Ruby và Điểm Cống Hiến Bang.

---

### HOẠT ĐỘNG 4: ĐẠI CHIẾN BOSS GIỜ VÀNG "QUÁI VẬT TUYẾT"

* **Linh vật Boss:** **Boss Quái Vật Tuyết (Mob ID 99)**.
* **Khung giờ xuất hiện:** Cố định vào các khung giờ vàng **12:00, 18:00, 20:00, 22:00** mỗi ngày tại bản đồ dã ngoại ngẫu nhiên.
* **Cơ chế sát thương đặc biệt:**
  * **1 Hit = 1 Dame:** Mọi đòn đánh đều chỉ gây đúng **1 điểm sát thương (trừ đúng 1 HP của Boss)**.
* **Cơ chế phần thưởng (Last Hit Only):**
  * Duy nhất người chơi tung đòn kết liễu (Last Hit) nhận thưởng:
    * **1 `Rương Pet Noel` (Item 4 ID 1011)**
    * **1 `Hộp Quà Giáng Sinh VIP` (Item 4 ID 492)**
    * **3 `Vé Đổi Trang Phục Noel` (Item 4 ID 229)**
    * Thông báo toàn server vinh danh người kết liễu Quái Vật Tuyết.

---

### HOẠT ĐỘNG 5: ĐUA TOP BXH "VUA GIÁNG SINH"

* **Cơ chế tích điểm:** Người chơi mở các hộp quà sự kiện để tích lũy Điểm Giáng Sinh:
  * Mở 1 `Hộp Quà Noel` (227): **+1 Điểm**
  * Mở 1 `Túi Giáng Sinh` (485): **+2 Điểm**
  * Mở 1 `Hộp Quà Giáng Sinh VIP` (492): **+5 Điểm**
* **Bảng Xếp Hạng:** Xem trực tiếp tại NPC Sự Kiện hoặc lệnh menu. Hệ thống tự động ghi nhận điểm và lưu vào `event_noel_data.json`.
* **Phần thưởng Đua Top:** Sẽ do Admin/GM tổng kết và **phát qua Giftcode** sau khi sự kiện kết thúc.

---

## PHẦN 4: HỆ THỐNG CÔNG THỨC CHẾ TẠO CHI TIẾT TẠI NPC SỰ KIỆN

| Tên Thành Phẩm | ID Item4 | Nguyên Liệu Yêu Cầu | Phí Beri / Ruby | Điểm BXH Khi Mở |
|:---|:---:|:---|:---:|:---:|
| **Đắp Người Tuyết** | **`340`** | 10 Nắm Tuyết (341) + 2 Cúc Áo (342) + 1 Nón Giáng Sinh (347) | 500.000 Beri | Nhận: 1 Thiệp Giáng Sinh + 50% Hộp Quà Noel |
| **Thiệp Giáng Sinh** | **`340`** | 5 Nắm Tuyết (341) + 2 Cúc Áo (342) + 1 Kẹo Giáng Sinh (489) | 500.000 Beri | — |
| **Hộp Quà Noel** | **`227`** | 2 Chuông Giáng Sinh (486) + 2 Vớ Giáng Sinh (487) + 1 Giấy Gói Quà (575) | 1.000.000 Beri | **+1 Điểm** |
| **Hộp Quà Giáng Sinh VIP**| **`492`** | 1 Hộp Quà Noel (227) + 1 Gấu Bông (590) + 1 Ngôi Sao Giáng Sinh (488) | 2.000.000 Beri + 50 Ruby | **+5 Điểm** |
| **Rương Trang Phục Noel** | **`622`** | 10 Vé Đổi Trang Phục Noel (229) + 5 Ngôi Sao Giáng Sinh (488) | 5.000.000 Beri + 100 Ruby | — |
| **Rương Pet Noel** | **`1011`**| 10 Vé Noel (230) + 1 Hộp Quà Giáng Sinh VIP (492) | 5.000.000 Beri + 100 Ruby | — |

---

## PHẦN 5: CHI TIẾT PHẦN THƯỞNG MỞ QUÀ

### 5.1. Danh Sách Phần Thưởng Mở Quà Sự Kiện

| Loại Quà Mở | Chi Tiết Phần Thưởng Nhận Được (Ngẫu Nhiên Trong Danh Sách) |
|:---|:---|
| **Sử dụng `Thiệp Giáng Sinh` (340)** | • **Cố định:** $200.000 - 1.000.000$ Beri.<br>• **Ngẫu nhiên:** 1-3 Mai rùa (6), 2-5 Bột vàng (4), 1-3 Ngôi sao may mắn (5), 2-5 Kẹo giáng sinh (489). |
| **Mở `Hộp Quà Noel` (227)** | • **Cố định:** **+1 Điểm BXH**.<br>• **Ngẫu nhiên:** Beri, Ruby, Đá khảm 3-4, Bột vàng, Bùa cường hóa, Rương Cam theo Lv, Khiên (10), Búa sơ cấp (339), Búa đục DIAL (457). |
| **Mở `Túi Giáng Sinh` (485)** | • **Cố định:** **+2 Điểm BXH**.<br>• **Ngẫu nhiên:** Beri, Ruby, Đá khảm 4-5, Đá Hải Thạch 3-4, Thẻ đổi tên, Tiến cấp đơn, Kỹ năng đơn. |
| **Mở `Hộp Quà Giáng Sinh VIP` (492)** | • **Cố định:** **+5 Điểm BXH**.<br>• **Ngẫu nhiên:** 10M-50M Beri, 200-1.000 Ruby, Đá khảm 6 tự chọn (588), Đá Khảm Vô Cực S (326), Đá Hải Thạch 5-6, Bảo hiểm chuyển hóa cao (551), Trái Ác Quỷ (87/158), Thời trang Noel (Vĩnh viễn). |
| **Mở `Rương Pet Noel` (1011)** | • Nhận ngẫu nhiên 1 trong 3 Pet Noel vĩnh viễn: **Pet Lộc Noel (708), Pet Tuyết Noel (709), Pet Tuần Lộc (720)**. |
| **Mở `Rương Trang Phục Noel` (622)**| • Nhận ngẫu nhiên 1 trong các bộ trang phục mùa đông vĩnh viễn: **Hoàng Tử Tuyết (75), Công Chúa Tuyết (76), Noel Nữ (45), Bé Tuyết (46)**. |

---

### 5.2. Đua Top BXH "Vua Giáng Sinh" (Phát Giftcode Sau)

> 🎁 **Lưu ý:** BXH sẽ tự động lưu điểm TOP 10 vào hệ thống `event_noel_data.json`. Sau khi kết thúc sự kiện, Admin sẽ dựa trên danh sách TOP để tạo và gửi Giftcode trực tiếp cho các người chơi đạt giải.

---

## PHẦN 6: CƠ CHẾ BẬT / TẮT SỰ KIỆN & HỆ THỐNG LỆNH QUẢN TRỊ GM INGAME

### 6.1. Cấu Hình Tệp Tin `htth.conf`
```properties
# Kích hoạt sự kiện Noel (Giáng Sinh)
event-noel: true
```

### 6.2. Bảng Lệnh Chat Quản Trị & Trao Thưởng Realtime

| Cú Pháp Lệnh Chat | Phân Quyền | Chức Năng Chi Tiết |
|:---|:---:|:---|
| **`/event noel on`** | Admin / GM | Kích hoạt BẬT sự kiện Noel ngay lập tức trên toàn server không cần khởi động lại. |
| **`/event noel off`** | Admin / GM | TẮT sự kiện Noel trên toàn server ngay lập tức. |
| **`/event noel status`** | Admin / GM | Kiểm tra trạng thái hiện tại của sự kiện (BẬT / TẮT). |
| **`/goiboss noel`** hoặc **`/spawnboss noel`** | Admin / GM | Triệu hồi ngay 1 Boss Quái Vật Tuyết tại bản đồ dã ngoại ngẫu nhiên. |
| **`/traothuong noel`** hoặc **`/reward noel`** | Admin / GM | Quét BXH và tự động phát thưởng Đua Top cho tất cả TOP 10 người chơi chưa nhận giải. |
| **`/nhanqua noel`** hoặc **`/claim noel`** | Player (TOP 10) | Người chơi nằm trong TOP 10 tự động nhận gói quà tương ứng với thứ hạng của mình. |
| **`/event noel menu`** | Tất cả | Hiển thị bảng tra cứu công thức chế tạo và nguồn nguyên liệu sự kiện Noel. |
