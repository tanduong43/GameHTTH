import { useLocation, useRoutes } from 'react-router-dom';
import { AnimatePresence } from 'framer-motion';
import { LandingPage, ForumPage, NewsList, NewsDetail, TopupPage } from './features';
import AdminLayout from './components/AdminLayout';
import AdminDashboard from './features/admin/AdminDashboard';
import AdminAccount from './features/admin/components/AdminAccount';
import AdminCoin from './features/admin/components/AdminCoin';
import AdminGiftcode from './features/admin/components/AdminGiftcode';
import AdminNews from './features/admin/components/AdminNews';
import AdminBanking from './features/admin/components/AdminBanking';
import AnimatedPage from './components/AnimatedPage';
import AuthForm from './components/AuthForm';

const routes = [
  { path: '/', element: <LandingPage /> },
  { path: '/tai-khoan', element: <ForumPage /> },
  { path: '/nap-tien', element: <TopupPage /> },
  { path: '/news', element: <NewsList /> },
  { path: '/news/:idOrSlug', element: <NewsDetail /> },
  {
    path: '/login',
    element: (
      <div className="forum-page" style={{ paddingTop: '80px', paddingBottom: '80px' }}>
        <div className="forum-content">
          <AuthForm title="⚓ ĐĂNG NHẬP" />
        </div>
      </div>
    )
  },
  {
    path: '/register',
    element: (
      <div className="forum-page" style={{ paddingTop: '80px', paddingBottom: '80px' }}>
        <div className="forum-content">
          <AuthForm title="⚓ ĐĂNG KÝ THÀNH VIÊN" />
        </div>
      </div>
    )
  },
  {
    path: '/admin',
    element: <AdminLayout />,
    children: [
      { index: true, element: <AdminDashboard /> },
      { path: 'accounts', element: <AdminAccount /> },
      { path: 'coins', element: <AdminCoin /> },
      { path: 'giftcodes', element: <AdminGiftcode /> },
      { path: 'news', element: <AdminNews /> },
      { path: 'banking', element: <AdminBanking /> },
    ]
  },
];

function getPageStyle(pathname) {
  const base = { position: 'relative', zIndex: 3, width: '100%' };

  if (pathname === '/nap-tien') {
    return {
      ...base,
      display: 'block',
      width: '100%',
    };
  }

  if (pathname === '/' || pathname.startsWith('/admin') || pathname.startsWith('/news')) {
    return base;
  }

  return {
    ...base,
    display: 'flex',
    justifyContent: 'center',
  };
}

function App() {
  const location = useLocation();
  const element = useRoutes(routes, location);

  return (
    <AnimatePresence mode="wait">
      {element && (
        <AnimatedPage key={location.pathname} style={getPageStyle(location.pathname)}>
          {element}
        </AnimatedPage>
      )}
    </AnimatePresence>
  );
}

export default App;
