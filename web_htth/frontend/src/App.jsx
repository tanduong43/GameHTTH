import { useLocation, useRoutes } from 'react-router-dom';
import { AnimatePresence } from 'framer-motion';
import { LandingPage, ForumPage, NewsList, NewsDetail } from './features';
import AdminLayout from './components/AdminLayout';
import AdminDashboard from './features/admin/AdminDashboard';
import AdminAccount from './features/admin/components/AdminAccount';
import AdminCoin from './features/admin/components/AdminCoin';
import AdminGiftcode from './features/admin/components/AdminGiftcode';
import AdminNews from './features/admin/components/AdminNews';
import AnimatedPage from './components/AnimatedPage';

const routes = [
  { path: '/', element: <LandingPage /> },
  { path: '/dien-dan', element: <ForumPage /> },
  { path: '/news', element: <NewsList /> },
  { path: '/news/:idOrSlug', element: <NewsDetail /> },
  { 
    path: '/admin', 
    element: <AdminLayout />,
    children: [
      { index: true, element: <AdminDashboard /> },
      { path: 'accounts', element: <AdminAccount /> },
      { path: 'coins', element: <AdminCoin /> },
      { path: 'giftcodes', element: <AdminGiftcode /> },
      { path: 'news', element: <AdminNews /> },
    ]
  },
];

function getPageStyle(pathname) {
  const base = { position: 'relative', zIndex: 3, width: '100%' };

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
