import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import Navbar from './Navbar';
import '../styles/App.css';

export default function MainLayout({ children }) {
  const location = useLocation();

  const isLandingMode = location.pathname === '/';
  const isForumMode = location.pathname === '/dien-dan' || location.pathname === '/nap-the';

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [location.pathname]);

  return (
    <div className={`app-container ${isLandingMode ? 'landing-mode' : ''} ${isForumMode ? 'forum-mode' : ''}`}>
      <div className="ocean-background"></div>
      <div className="overlay"></div>
      <Navbar />
      {children}
    </div>
  );
}
