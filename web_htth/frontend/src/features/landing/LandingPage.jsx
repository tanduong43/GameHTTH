import React, { useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import '../../styles/App.css';

function LandingPage() {
  const { user } = useAuth();

  const navigate = useNavigate();
  const location = useLocation();

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

  return (
    <div className="landing-page">
      {/* Hero Section */}
      <section className="hero-section">
        <div className="hero-content">
          <h1 className="hero-title">ĐẠI CHIẾN <span className="hero-highlight">TỨ HOÀNG</span></h1>
          <p className="hero-subtitle">Hành trình trở thành Vua Hải Tặc vĩ đại nhất mọi thời đại. Giăng buồm ra khơi, săn lùng kho báu và chiến đấu với hàng triệu người chơi!</p>
          <div className="hero-actions">
            <button onClick={() => navigate('/dien-dan')} className="btn btn-play-now">
              {user ? '⚓ VÀO DIỄN ĐÀN' : '🏴‍☠️ CHƠI NGAY'}
            </button>
            <button onClick={() => {
              const element = document.getElementById('download');
              if (element) {
                element.scrollIntoView({ behavior: 'smooth' });
              }
            }} className="btn btn-outline-light">TẢI GAME</button>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section id="features" className="features-section">
        <h2 className="section-title">TÍNH NĂNG ĐẶC SẮC</h2>
        <div className="features-grid">
          <div className="feature-card">
            <div className="feature-icon">⚔️</div>
            <h3>PVP Đỉnh Cao</h3>
            <p>Hệ thống chiến đấu PK máu lửa, tranh đoạt lãnh thổ và khẳng định sức mạnh cá nhân.</p>
          </div>
          <div className="feature-card">
            <div className="feature-icon">🐉</div>
            <h3>Săn Boss Thế Giới</h3>
            <p>Cùng băng hải tặc của bạn hạ gục những siêu Boss để nhận hàng ngàn trang bị hiếm.</p>
          </div>
          <div className="feature-card">
            <div className="feature-icon">💰</div>
            <h3>Giao Dịch Tự Do</h3>
            <p>Chợ đen sầm uất, tự do mua bán mọi vật phẩm không giới hạn giữa người chơi với nhau.</p>
          </div>
          <div className="feature-card">
            <div className="feature-icon">👕</div>
            <h3>Thời Trang Đa Dạng</h3>
            <p>Hàng ngàn bộ trang phục và hiệu ứng cực ngầu giúp bạn thể hiện cá tính riêng.</p>
          </div>
        </div>
      </section>

      {/* Download Section */}
      <section id="download" className="download-section">
        <h2 className="section-title">TẢI GAME MIỄN PHÍ</h2>
        <p style={{textAlign: 'center', marginBottom: '30px', color: 'var(--text-muted)'}}>Lựa chọn phiên bản phù hợp với thiết bị của bạn</p>
        <div className="download-grid">
          <button className="download-btn android-btn">
            <span className="dl-icon">📱</span>
            <div className="dl-info">
              <span className="dl-os">Android (APK)</span>
              <span className="dl-desc">Phiên bản mới nhất</span>
            </div>
          </button>
          <button className="download-btn ios-btn">
            <span className="dl-icon">🍎</span>
            <div className="dl-info">
              <span className="dl-os">iOS (iPhone/iPad)</span>
              <span className="dl-desc">TestFlight</span>
            </div>
          </button>
          <button className="download-btn pc-btn">
            <span className="dl-icon">💻</span>
            <div className="dl-info">
              <span className="dl-os">PC (Windows)</span>
              <span className="dl-desc">Bản giả lập mượt mà</span>
            </div>
          </button>
        </div>
      </section>

      {/* Footer */}
      <footer className="footer">
        <p>© 2024 Thế Giới Hải Tặc Private Server. All rights reserved.</p>
        <p>Game mang tính chất giải trí. Chơi game quá 180 phút một ngày sẽ ảnh hưởng xấu đến sức khỏe.</p>
      </footer>
    </div>
  );
}

export default LandingPage;


