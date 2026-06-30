import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

export default function AdminDashboard() {
  const { user } = useAuth();
  
  return (
    <div className="glass-panel" style={{ padding: '30px' }}>
      <h2 style={{ color: '#ff3366', marginBottom: '20px' }}>Xin chào, {user?.username}!</h2>
      <p style={{ color: '#ccc', lineHeight: '1.6', fontSize: '15px' }}>
        Chào mừng bạn trở lại hệ thống quản trị. Từ đây, bạn có thể truy cập nhanh các công cụ quản lý:
      </p>
      
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: '20px', marginTop: '30px' }}>
        <DashboardCard title="Tài Khoản" desc="Quản lý thành viên, khóa nick" icon="👤" link="/admin/accounts" />
        <DashboardCard title="Nạp Tiền" desc="Cộng trừ coin thủ công" icon="💰" link="/admin/coins" />
        <DashboardCard title="Giftcode" desc="Tạo mã quà tặng mới" icon="🎁" link="/admin/giftcodes" />
      </div>
    </div>
  );
}

function DashboardCard({ title, desc, icon, link }) {
  return (
    <Link to={link} style={{ textDecoration: 'none' }}>
      <div style={{ 
        background: 'rgba(0,0,0,0.3)', 
        border: '1px solid #333', 
        borderRadius: '8px', 
        padding: '20px', 
        textAlign: 'center',
        transition: 'all 0.3s'
      }}>
        <div style={{ fontSize: '30px', marginBottom: '10px' }}>{icon}</div>
        <h3 style={{ color: '#fff', marginBottom: '10px' }}>{title}</h3>
        <p style={{ color: '#aaa', fontSize: '13px' }}>{desc}</p>
      </div>
    </Link>
  );
}
