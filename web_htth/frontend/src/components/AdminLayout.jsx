import React, { useState } from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import AuthForm from './AuthForm';
import '../styles/App.css';

export default function AdminLayout() {
  const { user, loading } = useAuth();
  const location = useLocation();
  const [message, setMessage] = useState(null);
  const [refreshKey, setRefreshKey] = useState(0);

  const showMessage = (type, text) => {
    setMessage({ type, text });
    setTimeout(() => {
      setMessage((prev) => (prev && prev.text === text ? null : prev));
    }, 5000);
  };

  const handleRefresh = () => {
    setRefreshKey((prev) => prev + 1);
  };

  if (loading) {
    return <div className="loader">Đang tải dữ liệu...</div>;
  }

  // Chưa đăng nhập: hiển thị form đăng nhập
  if (!user) {
    return (
      <div style={{ minHeight: '100vh', background: '#111', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <div style={{ width: '100%', maxWidth: '440px', padding: '20px' }}>
          <div style={{ textAlign: 'center', marginBottom: '24px' }}>
            <div style={{ fontSize: '48px', marginBottom: '8px' }}>🛡️</div>
            <h2 style={{ color: '#ff3366', margin: 0 }}>ADMIN PANEL</h2>
            <p style={{ color: '#888', fontSize: '14px', marginTop: '8px' }}>Đăng nhập bằng tài khoản quản trị</p>
          </div>
          <AuthForm title="🔐 ĐĂNG NHẬP QUẢN TRỊ" />
        </div>
      </div>
    );
  }

  // Đã đăng nhập nhưng không phải admin
  if (user.username !== 'admin') {
    return (
      <div style={{ minHeight: '100vh', background: '#111', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <div className="glass-panel" style={{ textAlign: 'center', padding: '50px', color: '#ff4d79', maxWidth: '400px' }}>
          <div style={{ fontSize: '48px', marginBottom: '16px' }}>🚫</div>
          <h2>TRUY CẬP BỊ TỪ CHỐI</h2>
          <p style={{ color: '#aaa' }}>Tài khoản <strong style={{ color: '#fff' }}>{user.username}</strong> không có quyền truy cập trang quản trị.</p>
          <Link to="/" className="btn btn-primary" style={{ display: 'inline-block', marginTop: '20px' }}>🏠 Về trang chủ</Link>
        </div>
      </div>
    );
  }

  // Sidebar Layout cho admin
  return (
    <div className="admin-layout">
      {/* Sidebar */}
      <div className="admin-sidebar">
        <h2 style={{ color: '#ff3366', textAlign: 'center', marginBottom: '30px' }}>🛡️ ADMIN PANEL</h2>
        
        <nav style={{ display: 'flex', flexDirection: 'column', gap: '5px', flex: 1 }}>
          <SidebarLink to="/admin" currentPath={location.pathname} onRefresh={handleRefresh} exact>📊 Dashboard</SidebarLink>
          <SidebarLink to="/admin/accounts" currentPath={location.pathname} onRefresh={handleRefresh}>👤 Quản lý Tài khoản</SidebarLink>
          <SidebarLink to="/admin/coins" currentPath={location.pathname} onRefresh={handleRefresh}>💰 Quản lý Nạp tiền</SidebarLink>
          <SidebarLink to="/admin/giftcodes" currentPath={location.pathname} onRefresh={handleRefresh}>🎁 Quản lý Giftcode</SidebarLink>
          <SidebarLink to="/admin/news" currentPath={location.pathname} onRefresh={handleRefresh}>📰 Quản lý Tin Tức</SidebarLink>
          <SidebarLink to="/admin/banking" currentPath={location.pathname} onRefresh={handleRefresh}>🏦 Quản lý Banking</SidebarLink>
        </nav>

        <div style={{ padding: '20px', marginTop: 'auto' }}>
          <Link to="/" className="btn btn-outline" style={{ display: 'block', textAlign: 'center', borderColor: '#444', color: '#aaa' }}>🏠 Thoát về Web</Link>
        </div>
      </div>

      {/* Main Content */}
      <div className="admin-main-content">
        {message && (
          <div className={`alert alert-${message.type}`} style={{ marginBottom: '20px' }}>
            {message.text}
          </div>
        )}
        <Outlet key={refreshKey} context={{ showMessage }} />
      </div>
    </div>
  );
}

function SidebarLink({ to, currentPath, onRefresh, children, exact = false }) {
  const isActive = exact ? currentPath === to : currentPath.startsWith(to);

  const handleClick = (e) => {
    if (isActive) {
      e.preventDefault();
      onRefresh();
    }
  };

  return (
    <Link 
      to={to} 
      onClick={handleClick}
      className={`admin-sidebar-link ${isActive ? 'active' : ''}`}
    >
      {children}
    </Link>
  );
}
