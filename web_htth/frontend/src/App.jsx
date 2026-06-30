import { useLocation, useRoutes } from 'react-router-dom';
import { AnimatePresence } from 'framer-motion';
import { LandingPage, ForumPage, TopupPage } from './features';
import AdminLayout from './components/AdminLayout';
import AdminDashboard from './features/admin/AdminDashboard';
import AdminAccount from './features/admin/components/AdminAccount';
import AdminCoin from './features/admin/components/AdminCoin';
import AdminGiftcode from './features/admin/components/AdminGiftcode';
import AnimatedPage from './components/AnimatedPage';

const routes = [
  { path: '/', element: <LandingPage /> },
  { path: '/dien-dan', element: <ForumPage /> },
  { path: '/nap-the', element: <TopupPage /> },
  { 
    path: '/admin', 
    element: <AdminLayout />,
    children: [
      { index: true, element: <AdminDashboard /> },
      { path: 'accounts', element: <AdminAccount /> },
      { path: 'coins', element: <AdminCoin /> },
      { path: 'giftcodes', element: <AdminGiftcode /> },
    ]
  },
];

function getPageStyle(pathname) {
  const base = { position: 'relative', zIndex: 3, width: '100%' };

  if (pathname === '/' || pathname.startsWith('/admin')) {
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
