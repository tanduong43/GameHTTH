import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { motion, AnimatePresence } from 'framer-motion';
import api from '../../api/api';
import '../../styles/App.css';

// 3 custom banner sliders
const BANNERS = [
  {
    id: 1,
    title: "ĐẠI CHIẾN TỨ HOÀNG",
    subtitle: "KHÁM PHÁ KỶ NGUYÊN HẢI TẶC MỚI",
    description: "Tham gia thế giới hải tặc sôi động, giăng buồm ra khơi, chinh phục Đại Hải Trình cùng hàng triệu người chơi khác và tìm kiếm kho báu vĩ đại nhất!",
    image: "/banner_adventure.png",
    buttonText: "⚓ CHƠI NGAY",
    action: "forum"
  },
  {
    id: 2,
    title: "ĐẤU TRƯỜNG CHAMPIONS",
    subtitle: "GIẢI ĐẤU PVP TÌM KIẾM VUA HẢI TẶC",
    description: "Sân chơi PK đỉnh cao dành cho các hải tặc kiệt xuất khẳng định bản lĩnh. Vinh danh bảng vàng, nhận danh hiệu độc quyền và hàng ngàn Coin quà tặng!",
    image: "/banner_pvp.png",
    buttonText: "⚡ THAM GIA NGAY",
    action: "forum"
  },
  {
    id: 3,
    title: "SIÊU CẬP NHẬT 2.0",
    subtitle: "KỶ NGUYÊN CHUYỂN SINH MỚI",
    description: "Mở rộng cấp giới hạn, tính năng Chuyển Sinh thần thánh, thêm phụ bản lăng mộ cổ, và cập nhật hàng loạt trang bị tối thượng thời trang cực ngầu!",
    image: "/banner_update.png",
    buttonText: "📖 XEM CẬP NHẬT",
    action: "download"
  }
];

// Character class info
const CLASSES = [
  {
    id: 1,
    name: "Võ Sĩ",
    icon: "✊",
    weapon: "Găng Tay Sắt",
    color: "#f43f5e",
    description: "Sức mạnh từ nắm đấm thép huyền thoại. Sở hữu lượng máu dồi dào, khả năng càn quét cận chiến mạnh mẽ và là lá chắn phòng thủ tuyệt vời cho đồng đội trong mọi hoạt động tổ đội.",
    stats: { hp: 95, atk: 75, def: 90, spd: 60 }
  },
  {
    id: 2,
    name: "Kiếm Khách",
    icon: "⚔️",
    weapon: "Danh Kiếm Hạt Mưa",
    color: "#10b981",
    description: "Bậc thầy kiếm thuật với những đường kiếm sắc bén. Tấn công cận chiến linh hoạt, sở hữu sát thương vật lý và tỉ lệ chí mạng cao nhất trong 5 class, có khả năng kết liễu mục tiêu nhanh chóng.",
    stats: { hp: 75, atk: 95, def: 70, spd: 80 }
  },
  {
    id: 3,
    name: "Đầu Bếp",
    icon: "🍳",
    weapon: "Hắc Cước Phong Ma",
    color: "#f59e0b",
    description: "Sử dụng những cú đá uy lực có tốc độ kinh hoàng. Nổi bật với chỉ số né tránh cực tốt, bộ kỹ năng PK cơ động, phản đòn mạnh mẽ cùng khả năng hồi máu đột phá cho bản thân.",
    stats: { hp: 80, atk: 85, def: 65, spd: 95 }
  },
  {
    id: 4,
    name: "Hoa Tiêu",
    icon: "🌪️",
    weapon: "Gậy Thời Tiết",
    color: "#06b6d4",
    description: "Khống chế thiên nhiên và thời tiết bằng gậy ma thuật. Gây sát thương phép thuật diện rộng cực mạnh, tạo các hiệu ứng khống chế khó chịu như đóng băng, sấm sét làm chậm mục tiêu.",
    stats: { hp: 65, atk: 90, def: 60, spd: 75 }
  },
  {
    id: 5,
    name: "Xạ Thủ",
    icon: "🎯",
    weapon: "Súng Hỏa Mai/Ná Thun",
    color: "#8b5cf6",
    description: "Bắn phá kẻ thù từ khoảng cách an toàn nhất. Sát thương vật lý duy trì liên tục rất mạnh, có độ chính xác cao, chuyên gây các hiệu ứng bất lợi suy giảm sức mạnh của đối thủ từ tầm xa.",
    stats: { hp: 70, atk: 88, def: 55, spd: 85 }
  }
];



// News and Events
const NEWS = [
  {
    id: 1,
    tag: "Hot",
    date: "04/07/2026",
    title: "Khai mở Máy Chủ S1 - Làng Cối Xay Gió",
    desc: "Chào mừng các tân hải tặc giăng buồm ra khơi! S1 chính thức open cùng chuỗi sự kiện Đua Top Cấp Độ, Đua Top Nạp nhận quà tặng Giftcode độc quyền cực giá trị."
  },
  {
    id: 2,
    tag: "Sự Kiện",
    date: "02/07/2026",
    title: "Đua Top Cấp Độ Nhận Danh Hiệu Độc Quyền",
    desc: "Thời gian diễn ra từ 04/07 đến hết 18/07. Top 3 anh hùng đạt cấp độ cao nhất sẽ nhận được Danh hiệu Thần thoại tăng 10% sát thương cùng quà tặng hiện kim."
  },
  {
    id: 3,
    tag: "Thông Báo",
    date: "30/06/2026",
    title: "Lịch Bảo Trì Định Kỳ & Cân Bằng Sức Mạnh Tướng",
    desc: "Bản cập nhật cân bằng lại kỹ năng nộ của Class Hoa Tiêu, tối ưu hóa tốc độ load bản đồ trong game và bảo trì nâng cấp cấu hình máy chủ tránh tình trạng giật lag."
  }
];

function LandingPage() {
  const navigate = useNavigate();
  const location = useLocation();

  // Banner State
  const [currentSlide, setCurrentSlide] = useState(0);
  const [direction, setDirection] = useState(0); // -1: left, 1: right

  // Class State
  const [activeClassIdx, setActiveClassIdx] = useState(0);

  // Ranking State
  const [rankings, setRankings] = useState({ topLevel: [], topPvp: [] });
  const [loadingRank, setLoadingRank] = useState(true);
  const [errorRank, setErrorRank] = useState(null);

  // News State
  const [news, setNews] = useState(NEWS);

  const getTag = (title = '') => {
    const t = title.toLowerCase();
    if (t.includes('khai mở') || t.includes('hot') || t.includes('mới')) return 'Hot';
    if (t.includes('sự kiện') || t.includes('đua top') || t.includes('khuyến mại') || t.includes('quà')) return 'Sự Kiện';
    if (t.includes('bảo trì') || t.includes('thông báo') || t.includes('lịch')) return 'Thông Báo';
    return 'Tin Tức';
  };

  const formatDate = (dateStr) => {
    if (!dateStr) return '';
    const date = new Date(dateStr);
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
  };

  const fetchNews = async () => {
    try {
      const res = await api.get('news', { params: { page: 1, limit: 3 } });
      if (res.data && res.data.success && res.data.data && res.data.data.length > 0) {
        setNews(res.data.data);
      }
    } catch (err) {
      console.error("Error loading news on landing:", err);
    }
  };

  const fetchRankings = async () => {
    setLoadingRank(true);
    setErrorRank(null);
    try {
      const res = await api.get('ranking');
      if (res.data && res.data.success) {
        setRankings({
          topLevel: res.data.topLevel || [],
          topPvp: res.data.topPvp || []
        });
      } else {
        setErrorRank(res.data?.message || 'Không thể tải dữ liệu bảng xếp hạng.');
      }
    } catch (err) {
      console.error("Error loading rankings:", err);
      setErrorRank('Không thể kết nối tới máy chủ. Vui lòng thử lại sau.');
    } finally {
      setLoadingRank(false);
    }
  };

  // Fetch ranking and news from API once on mount
  useEffect(() => {
    fetchRankings();
    fetchNews();
  }, []);

  // Auto Slider
  useEffect(() => {
    const timer = setInterval(() => {
      handleNextSlide();
    }, 4500);
    return () => clearInterval(timer);
  }, [currentSlide]);

  const handleNextSlide = () => {
    setDirection(1);
    setCurrentSlide((prev) => (prev + 1) % BANNERS.length);
  };

  const handlePrevSlide = () => {
    setDirection(-1);
    setCurrentSlide((prev) => (prev - 1 + BANNERS.length) % BANNERS.length);
  };

  const handleSlideAction = (action) => {
    if (action === 'forum') {
      navigate('/tai-khoan');
    } else if (action === 'download') {
      const element = document.getElementById('download');
      if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
      }
    }
  };

  // Handle hash scrolling on load
  useEffect(() => {
    if (location.hash === '#download' || location.state?.scrollTo === 'download') {
      const element = document.getElementById('download');
      if (element) {
        setTimeout(() => {
          element.scrollIntoView({ behavior: 'smooth' });
        }, 100);
      }
      if (location.state?.scrollTo === 'download') {
        window.history.replaceState({}, document.title);
      }
    }
  }, [location]);

  // Helper to map clazz to name
  const getClassName = (clazzId) => {
    const cl = CLASSES.find(c => c.id === clazzId);
    return cl ? cl.name : "Vô Danh";
  };

  const getClassIcon = (clazzId) => {
    const cl = CLASSES.find(c => c.id === clazzId);
    return cl ? cl.icon : "🏴‍☠️";
  };

  const getClassColor = (clazzId) => {
    const cl = CLASSES.find(c => c.id === clazzId);
    return cl ? cl.color : "#64748b";
  };

  // Framer Motion Slider Variants
  const sliderVariants = {
    enter: (dir) => ({
      x: dir > 0 ? 1000 : -1000,
      opacity: 0
    }),
    center: {
      x: 0,
      opacity: 1,
      transition: { x: { type: 'spring', stiffness: 300, damping: 30 }, opacity: { duration: 0.3 } }
    },
    exit: (dir) => ({
      x: dir < 0 ? 1000 : -1000,
      opacity: 0,
      transition: { x: { type: 'spring', stiffness: 300, damping: 30 }, opacity: { duration: 0.3 } }
    })
  };

  const selectedClass = CLASSES[activeClassIdx];

  return (
    <div className="landing-page modern-light">
      
      {/* 1. Banner Slider Section */}
      <section className="banner-slider-container">
        <div className="slider-wrapper">
          <AnimatePresence initial={false} custom={direction} mode="wait">
            <motion.div
              key={currentSlide}
              custom={direction}
              variants={sliderVariants}
              initial="enter"
              animate="center"
              exit="exit"
              className="slide-content"
              style={{ backgroundImage: `linear-gradient(to right, rgba(255,255,255,0.95) 30%, rgba(255,255,255,0.7) 60%, rgba(255,255,255,0) 100%), url(${BANNERS[currentSlide].image})` }}
            >
              <div className="slide-text">
                <span className="badge-update">{BANNERS[currentSlide].subtitle}</span>
                <h1 className="slide-title">{BANNERS[currentSlide].title}</h1>
                <p className="slide-desc">{BANNERS[currentSlide].description}</p>
                <div className="slide-actions">
                  <button 
                    onClick={() => handleSlideAction(BANNERS[currentSlide].action)} 
                    className="btn btn-primary btn-slide-action"
                  >
                    {BANNERS[currentSlide].buttonText}
                  </button>
                  <button 
                    onClick={() => {
                      const el = document.getElementById('download');
                      if (el) el.scrollIntoView({ behavior: 'smooth' });
                    }} 
                    className="btn btn-secondary-outline btn-slide-action"
                  >
                    TẢI GAME NGAY
                  </button>
                </div>
              </div>
            </motion.div>
          </AnimatePresence>

          {/* Navigation Controls */}
          <button className="slider-arrow arrow-left" onClick={handlePrevSlide} aria-label="Previous Slide">
            ‹
          </button>
          <button className="slider-arrow arrow-right" onClick={handleNextSlide} aria-label="Next Slide">
            ›
          </button>

          {/* Dot Indicators */}
          <div className="slider-indicators">
            {BANNERS.map((_, idx) => (
              <button
                key={idx}
                className={`dot-indicator ${idx === currentSlide ? 'active' : ''}`}
                onClick={() => {
                  setDirection(idx > currentSlide ? 1 : -1);
                  setCurrentSlide(idx);
                }}
                aria-label={`Go to slide ${idx + 1}`}
              />
            ))}
          </div>
        </div>
      </section>

      {/* 2. Giới thiệu game & Interactive Class Select */}
      <section className="intro-class-section container-section">
        <div className="section-header-group">
          <span className="section-label">Giới Thiệu Game</span>
          <h2 className="section-title-modern">PHÂN LỚP NHÂN VẬT ĐẶC SẮC</h2>
          <p className="section-subtitle-modern">Hệ thống ngũ đại môn phái cân bằng, phong phú. Hãy chọn con đường hải tặc của riêng bạn!</p>
        </div>

        <div className="class-showcase-grid">
          {/* Class Selectors Sidebar */}
          <div className="class-selector-tabs">
            {CLASSES.map((cl, idx) => (
              <button
                key={cl.id}
                className={`class-tab-item ${idx === activeClassIdx ? 'active' : ''}`}
                style={{ '--active-border': cl.color }}
                onClick={() => setActiveClassIdx(idx)}
              >
                <span className="tab-icon" style={{ backgroundColor: `${cl.color}15`, color: cl.color }}>{cl.icon}</span>
                <div className="tab-info">
                  <span className="tab-name">{cl.name}</span>
                  <span className="tab-weapon">{cl.weapon}</span>
                </div>
              </button>
            ))}
          </div>

          {/* Class Detail Panel */}
          <div className="class-detail-card card-panel">
            <div className="detail-header">
              <span className="detail-icon-large" style={{ backgroundColor: `${selectedClass.color}15`, color: selectedClass.color }}>
                {selectedClass.icon}
              </span>
              <div>
                <h3 className="detail-name">{selectedClass.name}</h3>
                <span className="detail-weapon-label">Vũ khí chính: <strong>{selectedClass.weapon}</strong></span>
              </div>
            </div>

            <p className="detail-desc">{selectedClass.description}</p>

            <div className="detail-stats-group">
              <h4 className="stats-header">Chỉ Số Thuộc Tính</h4>
              
              <div className="stat-row">
                <span className="stat-label">❤️ Sinh Mệnh (HP)</span>
                <div className="stat-bar-container">
                  <div className="stat-bar" style={{ width: `${selectedClass.stats.hp}%`, backgroundColor: selectedClass.color }}></div>
                </div>
                <span className="stat-percent">{selectedClass.stats.hp}%</span>
              </div>

              <div className="stat-row">
                <span className="stat-label">⚔️ Tấn Công (ATK)</span>
                <div className="stat-bar-container">
                  <div className="stat-bar" style={{ width: `${selectedClass.stats.atk}%`, backgroundColor: selectedClass.color }}></div>
                </div>
                <span className="stat-percent">{selectedClass.stats.atk}%</span>
              </div>

              <div className="stat-row">
                <span className="stat-label">🛡️ Phòng Thủ (DEF)</span>
                <div className="stat-bar-container">
                  <div className="stat-bar" style={{ width: `${selectedClass.stats.def}%`, backgroundColor: selectedClass.color }}></div>
                </div>
                <span className="stat-percent">{selectedClass.stats.def}%</span>
              </div>

              <div className="stat-row">
                <span className="stat-label">⚡ Tốc Độ (SPD)</span>
                <div className="stat-bar-container">
                  <div className="stat-bar" style={{ width: `${selectedClass.stats.spd}%`, backgroundColor: selectedClass.color }}></div>
                </div>
                <span className="stat-percent">{selectedClass.stats.spd}%</span>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* 3. Leaderboard Bảng Xếp Hạng */}
      <section className="leaderboard-section container-section bg-light-gray">
        <div className="section-header-group">
          <span className="section-label">Đại Hải Trình</span>
          <h2 className="section-title-modern">BẢNG XẾP HẠNG ANH HÙNG</h2>
          <p className="section-subtitle-modern">Tôn vinh các hải tặc đạt đẳng cấp tối cao và tích lũy ngân quỹ hàng đầu thế giới đại dương.</p>
        </div>

        <div className="leaderboards-grid">
          {/* Column 1: Top Level */}
          <div className="leaderboard-card card-panel">
            <div className="board-header level-board">
              <span className="board-icon">🏆</span>
              <div>
                <h3>TOP CẤP ĐỘ</h3>
                <span>Cao thủ luyện cấp vĩ đại nhất</span>
              </div>
            </div>

            {loadingRank ? (
              <div className="board-loading">
                <span className="spinner-icon">🔄</span> Đang tải bảng xếp hạng...
              </div>
            ) : errorRank ? (
              <div className="board-error-container">
                <p className="board-error-msg">⚠️ {errorRank}</p>
                <button className="btn-retry" onClick={fetchRankings}>Thử Lại</button>
              </div>
            ) : (
              <div className="board-table-container">
                <table className="board-table">
                  <thead>
                    <tr>
                      <th style={{ width: '60px', textAlign: 'center' }}>Hạng</th>
                      <th>Nhân vật</th>
                      <th style={{ width: '120px' }}>Hệ phái</th>
                      <th style={{ width: '100px', textAlign: 'right' }}>Cấp độ</th>
                    </tr>
                  </thead>
                  <tbody>
                    {rankings.topLevel.map((player, idx) => (
                      <tr key={idx} className="board-row">
                        <td className="rank-col">
                          {idx === 0 && <span className="rank-medal gold">🥇</span>}
                          {idx === 1 && <span className="rank-medal silver">🥈</span>}
                          {idx === 2 && <span className="rank-medal bronze">🥉</span>}
                          {idx > 2 && <span className="rank-number">{idx + 1}</span>}
                        </td>
                        <td>
                          <div className="char-info-cell">
                            <span 
                              className="char-avatar-mini" 
                              style={{ backgroundColor: `${getClassColor(player.clazz)}15`, color: getClassColor(player.clazz) }}
                            >
                              {getClassIcon(player.clazz)}
                            </span>
                            <span className="char-name">{player.name}</span>
                          </div>
                        </td>
                        <td>
                          <span style={{ color: getClassColor(player.clazz), fontWeight: 600, fontSize: '13px' }}>
                            {getClassName(player.clazz)}
                          </span>
                        </td>
                        <td style={{ textAlign: 'right', fontWeight: 'bold' }}>
                          <span className="level-text">{player.level} Cấp</span>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>

          {/* Column 2: Top PvP */}
          <div className="leaderboard-card card-panel">
            <div className="board-header pvp-arena-board">
              <span className="board-icon">⚔️</span>
              <div>
                <h3>TOP ĐẤU TRƯỜNG</h3>
                <span>Anh hùng PK xuất sắc nhất</span>
              </div>
            </div>

            {loadingRank ? (
              <div className="board-loading">
                <span className="spinner-icon">🔄</span> Đang tải bảng xếp hạng...
              </div>
            ) : errorRank ? (
              <div className="board-error-container">
                <p className="board-error-msg">⚠️ {errorRank}</p>
                <button className="btn-retry" onClick={fetchRankings}>Thử Lại</button>
              </div>
            ) : (
              <div className="board-table-container">
                <table className="board-table">
                  <thead>
                    <tr>
                      <th style={{ width: '60px', textAlign: 'center' }}>Hạng</th>
                      <th>Nhân vật</th>
                      <th style={{ width: '120px' }}>Hệ phái</th>
                      <th style={{ width: '100px', textAlign: 'right' }}>Điểm PVP</th>
                    </tr>
                  </thead>
                  <tbody>
                    {rankings.topPvp.map((player, idx) => (
                      <tr key={idx} className="board-row">
                        <td className="rank-col">
                          {idx === 0 && <span className="rank-medal gold">🥇</span>}
                          {idx === 1 && <span className="rank-medal silver">🥈</span>}
                          {idx === 2 && <span className="rank-medal bronze">🥉</span>}
                          {idx > 2 && <span className="rank-number">{idx + 1}</span>}
                        </td>
                        <td>
                          <div className="char-info-cell">
                            <span 
                              className="char-avatar-mini" 
                              style={{ backgroundColor: `${getClassColor(player.clazz)}15`, color: getClassColor(player.clazz) }}
                            >
                              {getClassIcon(player.clazz)}
                            </span>
                            <span className="char-name">{player.name}</span>
                          </div>
                        </td>
                        <td>
                          <span style={{ color: getClassColor(player.clazz), fontWeight: 600, fontSize: '13px' }}>
                            {getClassName(player.clazz)}
                          </span>
                        </td>
                        <td style={{ textAlign: 'right', fontWeight: 'bold' }} className="pvp-points-text">
                          {player.pvppoint.toLocaleString()} Điểm
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        </div>
      </section>

      {/* 4. Tin tức & Sự kiện */}
      <section className="news-section container-section">
        <div className="section-header-group">
          <span className="section-label">Bản Tin Hải Tặc</span>
          <h2 className="section-title-modern">TIN TỨC & SỰ KIỆN NỔI BẬT</h2>
          <p className="section-subtitle-modern">Cập nhật liên tục các tin tức nóng hổi, sự kiện khuyên mại, và thông tin bảo trì máy chủ mới nhất.</p>
        </div>

        <div className="news-grid">
          {news.map((item) => {
            const tag = getTag(item.title);
            const dateStr = item.published_at ? formatDate(item.published_at) : item.date;
            const summaryStr = item.summary || item.desc;
            const targetUrl = item.slug || item.id ? `/news/${item.slug || item.id}` : '/tai-khoan';
            return (
              <article key={item.id} className="news-card card-panel">
                <div className="news-meta">
                  <span className={`news-tag tag-${tag.toLowerCase().replace(' ', '-')}`}>{tag}</span>
                  <span className="news-date">📅 {dateStr}</span>
                </div>
                <h3 
                  className="news-card-title" 
                  onClick={() => navigate(targetUrl)}
                  style={{ cursor: 'pointer' }}
                >
                  {item.title}
                </h3>
                <p className="news-card-desc">{summaryStr}</p>
                <button onClick={() => navigate(targetUrl)} className="btn-readmore">Chi tiết bài viết ›</button>
              </article>
            );
          })}
        </div>
      </section>

      {/* 5. Download Section */}
      <section id="download" className="download-section-modern container-section bg-gradient-blue text-white">
        <div className="download-content-wrapper">
          <div className="download-text-group">
            <h2 className="download-title">TẢI GAME MIỄN PHÍ</h2>
            <p className="download-subtitle">Game hỗ trợ đa nền tảng. Luyện cấp cực mượt trên PC, linh hoạt PK mọi lúc mọi nơi trên điện thoại di động Android và iOS!</p>
          </div>
          
          <div className="download-buttons-grid">
            <button className="dl-button android-dl">
              <span className="dl-btn-icon">📱</span>
              <div className="dl-btn-text">
                <span className="dl-btn-os">Android (APK)</span>
                <span className="dl-btn-sub">Tải bản cài đặt trực tiếp</span>
              </div>
            </button>

            <button className="dl-button ios-dl">
              <span className="dl-btn-icon">🍎</span>
              <div className="dl-btn-text">
                <span className="dl-btn-os">iOS (iPhone)</span>
                <span className="dl-btn-sub">Cài đặt qua TestFlight</span>
              </div>
            </button>

            <button className="dl-button pc-dl">
              <span className="dl-btn-icon">💻</span>
              <div className="dl-btn-text">
                <span className="dl-btn-os">PC (Windows)</span>
                <span className="dl-btn-sub">Bản giả lập tối ưu mượt mà</span>
              </div>
            </button>
          </div>
        </div>
      </section>

      {/* 6. Footer */}
      <footer className="footer-modern">
        <div className="footer-top-grid">
          <div className="footer-brand">
            <span className="footer-logo">⚓ Thế Giới Hải Tặc</span>
            <p>Trải nghiệm máy chủ Hải Tặc Private chất lượng cao, đồ họa sắc nét, lối chơi nhập vai kinh điển chuẩn nguyên tác Anime One Piece.</p>
          </div>
          <div className="footer-links-group">
            <h4>ĐIỀU HƯỚNG</h4>
            <div className="footer-links">
              <a href="#" onClick={(e) => { e.preventDefault(); window.scrollTo({ top: 0, behavior: 'smooth' }); }}>Trang Chủ</a>
              <a href="#download" onClick={(e) => { e.preventDefault(); document.getElementById('download')?.scrollIntoView({ behavior: 'smooth' }); }}>Tải Game</a>
              <a href="#" onClick={(e) => { e.preventDefault(); navigate('/nap-tien'); }}>Nạp Thẻ</a>
              <a href="#" onClick={(e) => { e.preventDefault(); navigate('/tai-khoan'); }}>Tài Khoản</a>
            </div>
          </div>
          <div className="footer-support">
            <h4>HỖ TRỢ KHÁCH HÀNG</h4>
            <p>Liên hệ Admin trực tiếp qua Diễn đàn hoặc Box chat Telegram hỗ trợ kỹ thuật 24/7.</p>
            <span className="telegram-tag">💬 Telegram: @HTTH_Support</span>
          </div>
        </div>
        <div className="footer-bottom">
          <p>© 2026 Thế Giới Hải Tặc Private Server. Mọi quyền được bảo lưu.</p>
          <p className="health-warning">Chơi game quá 180 phút mỗi ngày sẽ ảnh hưởng xấu tới sức khỏe. Hãy phân bổ thời gian hợp lý!</p>
        </div>
      </footer>

    </div>
  );
}

export default LandingPage;
