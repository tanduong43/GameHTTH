import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import Navbar from './Navbar';
import '../styles/App.css';

export default function MainLayout({ children }) {
  const location = useLocation();

  const isLandingMode = location.pathname === '/' 
    || location.pathname === '/tai-khoan' 
    || location.pathname === '/nap-tien' 
    || location.pathname === '/login' 
    || location.pathname === '/register' 
    || location.pathname.startsWith('/news');
  const isForumMode = false;

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [location.pathname]);

  if (location.pathname.startsWith('/admin')) {
    return (
      <div style={{ minHeight: '100vh', width: '100%', background: '#0a0a0a', color: '#eee', margin: 0, padding: 0 }}>
        {children}
      </div>
    );
  }

  return (
    <div className={`app-container ${isLandingMode ? 'landing-mode' : ''} ${isForumMode ? 'forum-mode' : ''}`}>
      <div className="ocean-background"></div>
      <div className="overlay"></div>
      <Navbar />
      {children}
    </div>
  );
}
