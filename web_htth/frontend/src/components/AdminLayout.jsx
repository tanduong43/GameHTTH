import React, { useState } from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import AuthForm from './AuthForm';
import '../styles/App.css';

export default function AdminLayout() {
  const { user, loading } = useAuth();
  const location = useLocation();
  const [message, setMessage] = useState(null);

  const showMessage = (type, text) => {
    setMessage({ type, text });
    setTimeout(() => {
      setMessage((prev) => (prev && prev.text === text ? null : prev));
    }, 5000);
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
    <div className="admin-layout" style={{ display: 'flex', minHeight: '100vh', width: '100vw', background: '#111', margin: 0, padding: 0 }}>
      {/* Sidebar */}
      <div className="admin-sidebar" style={{ width: '250px', background: '#1a1a1a', borderRight: '1px solid #333', padding: '20px 0', display: 'flex', flexDirection: 'column' }}>
        <h2 style={{ color: '#ff3366', textAlign: 'center', marginBottom: '30px' }}>🛡️ ADMIN PANEL</h2>
        
        <nav style={{ display: 'flex', flexDirection: 'column', gap: '5px', flex: 1 }}>
          <SidebarLink to="/admin" currentPath={location.pathname} exact>📊 Dashboard</SidebarLink>
          <SidebarLink to="/admin/accounts" currentPath={location.pathname}>👤 Quản lý Tài khoản</SidebarLink>
          <SidebarLink to="/admin/coins" currentPath={location.pathname}>💰 Quản lý Nạp tiền</SidebarLink>
          <SidebarLink to="/admin/giftcodes" currentPath={location.pathname}>🎁 Quản lý Giftcode</SidebarLink>
          <SidebarLink to="/admin/news" currentPath={location.pathname}>📰 Quản lý Tin Tức</SidebarLink>
          <SidebarLink to="/admin/banking" currentPath={location.pathname}>🏦 Quản lý Banking</SidebarLink>
        </nav>

        <div style={{ padding: '20px', marginTop: 'auto' }}>
          <Link to="/" className="btn btn-outline" style={{ display: 'block', textAlign: 'center', borderColor: '#444', color: '#aaa' }}>🏠 Thoát về Web</Link>
        </div>
      </div>

      {/* Main Content */}
      <div className="admin-main-content" style={{ flex: 1, padding: '30px', overflowY: 'auto', background: '#0a0a0a' }}>
        {message && (
          <div className={`alert alert-${message.type}`} style={{ marginBottom: '20px' }}>
            {message.text}
          </div>
        )}
        <Outlet context={{ showMessage }} />
      </div>
    </div>
  );
}

function SidebarLink({ to, currentPath, children, exact = false }) {
  const isActive = exact ? currentPath === to : currentPath.startsWith(to);

  const handleClick = (e) => {
    e.preventDefault();
    if (isActive) {
      window.location.reload();
    } else {
      window.location.href = to;
    }
  };

  return (
    <Link 
      to={to} 
      onClick={handleClick}
      style={{
        padding: '12px 20px',
        color: isActive ? '#fff' : '#aaa',
        background: isActive ? 'rgba(255, 51, 102, 0.1)' : 'transparent',
        borderRight: isActive ? '3px solid #ff3366' : '3px solid transparent',
        textDecoration: 'none',
        transition: 'all 0.2s',
        fontWeight: isActive ? 'bold' : 'normal',
        display: 'block'
      }}
    >
      {children}
    </Link>
  );
}
