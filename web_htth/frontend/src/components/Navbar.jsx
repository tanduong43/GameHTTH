import React from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import '../styles/App.css';

function Navbar() {
  const { user } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  const hash = location.hash;

  // Determine active state for each nav item
  const isHomeActive = location.pathname === '/' && hash !== '#download';
  const isDownloadActive = location.pathname === '/' && hash === '#download';
  const isNewsActive = location.pathname.startsWith('/news');
  const isForumActive = location.pathname === '/tai-khoan' || location.pathname === '/login' || location.pathname === '/register';

  const handleHomeClick = (e) => {
    e.preventDefault();
    if (location.pathname !== '/') {
      navigate('/');
    } else {
      window.scrollTo({ top: 0, behavior: 'smooth' });
      navigate('/', { replace: true });
    }
  };

  const handleDownloadClick = (e) => {
    e.preventDefault();
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
          navigate('/');
          window.scrollTo({ top: 0, behavior: 'smooth' });
        }} 
        style={{ cursor: 'pointer' }}
      >
        <span className="logo-icon">⚓</span>
        <h2>Thế Giới Hải Tặc</h2>
      </div>
      <div className="navbar-links">
        <a 
          href="/" 
          onClick={handleHomeClick}
          className={`nav-btn ${isHomeActive ? 'active' : ''}`}
        >
          Trang Chủ
        </a>
        <Link 
          to="/news" 
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
          to={user ? "/tai-khoan" : "/login"} 
          className={`nav-btn ${isForumActive ? 'active' : ''}`}
        >
          {user ? `Tài Khoản (${user.username})` : 'Tài Khoản'}
        </Link>
      </div>
    </nav>
  );
}

export default Navbar;

