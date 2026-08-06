import React, { useState, useEffect } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import '../styles/App.css';

function Navbar() {
  const { user } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);

  const hash = location.hash;

  // Determine active state for each nav item
  const isHomeActive = location.pathname === '/' && hash !== '#download';
  const isDownloadActive = location.pathname === '/' && hash === '#download';
  const isNewsActive = location.pathname.startsWith('/news');
  const isTopupActive = location.pathname === '/nap-tien';
  const isForumActive = location.pathname === '/tai-khoan' || location.pathname === '/login' || location.pathname === '/register';

  // Automatically close mobile menu when path or hash changes
  useEffect(() => {
    setIsMobileMenuOpen(false);
  }, [location.pathname, location.hash]);

  const closeMobileMenu = () => {
    setIsMobileMenuOpen(false);
  };

  const handleHomeClick = (e) => {
    e.preventDefault();
    closeMobileMenu();
    if (location.pathname !== '/') {
      navigate('/');
    } else {
      window.scrollTo({ top: 0, behavior: 'smooth' });
      navigate('/', { replace: true });
    }
  };

  const handleDownloadClick = (e) => {
    e.preventDefault();
    closeMobileMenu();
    if (location.pathname !== '/') {
      navigate('/#download');
    } else {
      const element = document.getElementById('download');
      if (element) {
        element.scrollIntoView({ behavior: 'smooth' });
      }
      navigate('/#download', { replace: true });
    }
  };

  return (
    <nav className="navbar" style={{ zIndex: 100 }}>
      <div 
        className="navbar-logo" 
        onClick={() => {
          closeMobileMenu();
          navigate('/');
          window.scrollTo({ top: 0, behavior: 'smooth' });
        }} 
        style={{ cursor: 'pointer' }}
      >
        <span className="logo-icon">⚓</span>
        <h2>Thế Giới Hải Tặc</h2>
      </div>

      <button 
        className={`navbar-toggle ${isMobileMenuOpen ? 'open' : ''}`}
        onClick={() => setIsMobileMenuOpen(!isMobileMenuOpen)}
        aria-label="Toggle navigation menu"
      >
        {isMobileMenuOpen ? '✕' : '☰'}
      </button>

      <div className={`navbar-links ${isMobileMenuOpen ? 'mobile-open' : ''}`}>
        <a 
          href="/" 
          onClick={handleHomeClick}
          className={`nav-btn ${isHomeActive ? 'active' : ''}`}
        >
          Trang Chủ
        </a>
        <Link 
          to="/news" 
          onClick={closeMobileMenu}
          className={`nav-btn ${isNewsActive ? 'active' : ''}`}
        >
          Tin Tức
        </Link>
        <a 
          href="#download" 
          onClick={handleDownloadClick} 
          className={`nav-btn ${isDownloadActive ? 'active' : ''}`}
        >
          Tải Game
        </a>
        <Link 
          to="/nap-tien" 
          onClick={closeMobileMenu}
          className={`nav-btn ${isTopupActive ? 'active' : ''}`}
        >
          Nạp Tiền
        </Link>
        <Link 
          to={user ? "/tai-khoan" : "/login"} 
          onClick={closeMobileMenu}
          className={`nav-btn ${isForumActive ? 'active' : ''}`}
        >
          {user ? `Tài Khoản (${user.username})` : 'Tài Khoản'}
        </Link>
      </div>
    </nav>
  );
}

export default Navbar;

