import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import api from '../../api/api';

export default function AdminDashboard() {
  const { user } = useAuth();
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchStats = async () => {
      try {
        setLoading(true);
        const res = await api.get('admin/stats');
        if (res.data && res.data.success) {
          setStats(res.data.stats);
        } else {
          setError(res.data?.message || 'Không thể tải dữ liệu thống kê.');
        }
      } catch (err) {
        console.error('Error fetching admin stats:', err);
        setError('Lỗi kết nối tới máy chủ khi tải thống kê.');
      } finally {
        setLoading(false);
      }
    };
    fetchStats();
  }, []);

  const getStatusText = (status) => {
    switch (status) {
      case 0: return { text: 'Chờ duyệt', color: '#faad14', bg: 'rgba(250,173,20,0.1)' };
      case 1: return { text: 'Thành công', color: '#52c41a', bg: 'rgba(82,196,26,0.1)' };
      case 2: return { text: 'Sai m.giá', color: '#1890ff', bg: 'rgba(24,144,255,0.1)' };
      case 3: return { text: 'Thất bại', color: '#f5222d', bg: 'rgba(245,34,45,0.1)' };
      default: return { text: 'Không rõ', color: '#888', bg: 'rgba(255,255,255,0.05)' };
    }
  };

  if (loading) {
    return <div className="glass-panel" style={{ padding: '30px', textAlign: 'center', color: '#fff' }}>Đang tải dữ liệu thống kê...</div>;
  }

  if (error) {
    return (
      <div className="glass-panel" style={{ padding: '30px', textAlign: 'center', color: '#ff4d79' }}>
        <p>⚠️ {error}</p>
        <button onClick={() => window.location.reload()} className="btn btn-primary" style={{ marginTop: '15px' }}>Tải Lại</button>
      </div>
    );
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '30px' }}>
      {/* Welcome Header */}
      <div className="glass-panel" style={{ padding: '25px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', flexWrap: 'wrap', gap: '15px' }}>
        <div>
          <h2 style={{ color: '#ff3366', margin: 0, fontSize: '24px' }}>Xin chào, {user?.username}! ⚓</h2>
          <p style={{ color: '#aaa', margin: '5px 0 0 0', fontSize: '14px' }}>Chào mừng bạn trở lại hệ thống quản trị Thế Giới Hải Tặc.</p>
        </div>
        <button onClick={() => window.location.reload()} className="btn btn-outline" style={{ borderColor: 'rgba(255,255,255,0.15)', color: '#ccc' }}>🔄 Làm mới thống kê</button>
      </div>

      {/* Numerical Stats Grid */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))', gap: '20px' }}>
        <StatCard 
          title="Tổng Tiền Nạp" 
          value={`${(stats?.totalRecharged || 0).toLocaleString()}đ`} 
          color="#52c41a" 
          icon="💰" 
          bg="linear-gradient(135deg, rgba(82,196,26,0.15) 0%, rgba(0,0,0,0) 100%)" 
        />
        <StatCard 
          title="Tổng Giao Dịch" 
          value={stats?.totalTxns || 0} 
          color="#1890ff" 
          icon="📊" 
          bg="linear-gradient(135deg, rgba(24,144,255,0.15) 0%, rgba(0,0,0,0) 100%)" 
        />
        <StatCard 
          title="Giao Dịch Thành Công" 
          value={(stats?.successTxns || 0)} 
          color="#52c41a" 
          icon="✅" 
          bg="linear-gradient(135deg, rgba(82,196,26,0.1) 0%, rgba(0,0,0,0) 100%)" 
        />
        <StatCard 
          title="Giao Dịch Lỗi" 
          value={stats?.failedTxns || 0} 
          color="#f5222d" 
          icon="❌" 
          bg="linear-gradient(135deg, rgba(245,34,45,0.1) 0%, rgba(0,0,0,0) 100%)" 
        />
      </div>

      {/* Main Stats Charts/Tables split */}
      <div style={{ display: 'flex', gap: '25px', flexWrap: 'wrap' }}>
        {/* Left column: Top depositors */}
        <div className="glass-panel" style={{ flex: '1 1 350px', padding: '20px', display: 'flex', flexDirection: 'column' }}>
          <h3 style={{ color: '#fff', borderBottom: '1px solid rgba(255,255,255,0.1)', paddingBottom: '12px', marginTop: 0, marginBottom: '15px' }}>
            👑 Top Nạp Nhiều Nhất
          </h3>
          {(!stats?.topDepositors || stats.topDepositors.length === 0) ? (
            <div style={{ padding: '20px', textAlign: 'center', color: '#777' }}>Chưa có dữ liệu nạp tiền</div>
          ) : (
            <div style={{ overflowX: 'auto' }}>
              <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '14px' }}>
                <thead>
                  <tr style={{ borderBottom: '1px solid rgba(255,255,255,0.1)', color: '#888', textAlign: 'left' }}>
                    <th style={{ padding: '10px 5px' }}>Hạng</th>
                    <th style={{ padding: '10px 5px' }}>Tài khoản</th>
                    <th style={{ padding: '10px 5px', textAlign: 'right' }}>Tổng nạp</th>
                    <th style={{ padding: '10px 5px', textAlign: 'right' }}>Số GD</th>
                  </tr>
                </thead>
                <tbody>
                  {stats.topDepositors.map((dep, index) => (
                    <tr key={dep.username} style={{ borderBottom: '1px solid rgba(255,255,255,0.05)', color: '#ddd' }}>
                      <td style={{ padding: '12px 5px', fontWeight: 'bold' }}>
                        {index === 0 ? '🥇' : index === 1 ? '🥈' : index === 2 ? '🥉' : `#${index + 1}`}
                      </td>
                      <td style={{ padding: '12px 5px', color: '#ffac30', fontWeight: 'bold' }}>{dep.username}</td>
                      <td style={{ padding: '12px 5px', textAlign: 'right', fontWeight: 'bold', color: '#52c41a' }}>
                        {Number(dep.total_amount).toLocaleString()}đ
                      </td>
                      <td style={{ padding: '12px 5px', textAlign: 'right', color: '#aaa' }}>{dep.txn_count}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>

        {/* Right column: Recent Transactions */}
        <div className="glass-panel" style={{ flex: '2 1 500px', padding: '20px' }}>
          <h3 style={{ color: '#fff', borderBottom: '1px solid rgba(255,255,255,0.1)', paddingBottom: '12px', marginTop: 0, marginBottom: '15px' }}>
            🔔 Giao Dịch Gần Đây
          </h3>
          {(!stats?.recentTxns || stats.recentTxns.length === 0) ? (
            <div style={{ padding: '20px', textAlign: 'center', color: '#777' }}>Chưa có lịch sử giao dịch</div>
          ) : (
            <div style={{ overflowX: 'auto', maxHeight: '400px', overflowY: 'auto' }}>
              <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '13px' }}>
                <thead>
                  <tr style={{ borderBottom: '1px solid rgba(255,255,255,0.1)', color: '#888', textAlign: 'left', position: 'sticky', top: 0, background: '#1a1a1a', zIndex: 1 }}>
                    <th style={{ padding: '10px 5px' }}>Người nạp</th>
                    <th style={{ padding: '10px 5px' }}>Chi tiết</th>
                    <th style={{ padding: '10px 5px', textAlign: 'right' }}>Mệnh giá</th>
                    <th style={{ padding: '10px 5px', textAlign: 'right' }}>Thực nhận</th>
                    <th style={{ padding: '10px 5px', textAlign: 'center' }}>Trạng thái</th>
                    <th style={{ padding: '10px 5px' }}>Thời gian</th>
                  </tr>
                </thead>
                <tbody>
                  {stats.recentTxns.map((txn, index) => {
                    const statusObj = getStatusText(txn.status);
                    return (
                      <tr key={txn.id || index} style={{ borderBottom: '1px solid rgba(255,255,255,0.05)', color: '#ccc' }}>
                        <td style={{ padding: '10px 5px', color: '#ffac30', fontWeight: 'bold' }}>{txn.username}</td>
                        <td style={{ padding: '10px 5px' }}>
                          {txn.type === 'card' ? (
                            <div>
                              <span style={{ textTransform: 'uppercase', fontWeight: 'bold', color: '#fff', fontSize: '11px' }}>{txn.telco}</span>
                              <div style={{ fontSize: '10px', color: '#888' }}>S/N: {txn.serial} | Mã: {txn.code}</div>
                            </div>
                          ) : (
                            <span style={{ color: '#aaa', fontSize: '11px' }}>{txn.type === 'admin_add' ? 'Cộng tay (Admin)' : txn.type || 'Chuyển khoản'}</span>
                          )}
                        </td>
                        <td style={{ padding: '10px 5px', textAlign: 'right', fontWeight: '500' }}>{Number(txn.amount).toLocaleString()}đ</td>
                        <td style={{ padding: '10px 5px', textAlign: 'right', fontWeight: 'bold', color: '#52c41a' }}>{Number(txn.real_amount || 0).toLocaleString()}đ</td>
                        <td style={{ padding: '10px 5px', textAlign: 'center' }}>
                          <span style={{ 
                            padding: '3px 8px', 
                            borderRadius: '4px', 
                            fontSize: '11px', 
                            fontWeight: 'bold', 
                            color: statusObj.color, 
                            backgroundColor: statusObj.bg 
                          }}>
                            {statusObj.text}
                          </span>
                        </td>
                        <td style={{ padding: '10px 5px', fontSize: '11px', color: '#888' }}>
                          {new Date(txn.created_at).toLocaleString('vi-VN', { 
                            month: 'numeric', 
                            day: 'numeric', 
                            hour: '2-digit', 
                            minute: '2-digit' 
                          })}
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>

      {/* Quick Navigation Cards */}
      <div>
        <h3 style={{ color: '#fff', marginBottom: '15px' }}>🛠️ Truy Cập Nhanh Quản Lý</h3>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: '20px' }}>
          <DashboardCard title="Tài Khoản" desc="Quản lý thành viên, khóa nick" icon="👤" link="/admin/accounts" />
          <DashboardCard title="Nạp Tiền" desc="Cộng trừ coin thủ công" icon="💰" link="/admin/coins" />
          <DashboardCard title="Giftcode" desc="Quản lý & tạo mã quà tặng" icon="🎁" link="/admin/giftcodes" />
          <DashboardCard title="Tin Tức" desc="Quản lý bài viết, thông báo" icon="📰" link="/admin/news" />
        </div>
      </div>
    </div>
  );
}

function StatCard({ title, value, color, icon, bg }) {
  return (
    <div style={{ 
      background: bg || 'rgba(0,0,0,0.3)', 
      border: '1px solid rgba(255,255,255,0.08)', 
      borderRadius: '10px', 
      padding: '20px',
      position: 'relative',
      overflow: 'hidden',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      boxShadow: '0 4px 12px rgba(0,0,0,0.2)'
    }}>
      <div>
        <span style={{ color: '#888', fontSize: '13px', textTransform: 'uppercase', letterSpacing: '0.5px' }}>{title}</span>
        <h2 style={{ color: '#fff', fontSize: '24px', margin: '5px 0 0 0', fontWeight: 'bold' }}>{value}</h2>
      </div>
      <div style={{ fontSize: '32px', opacity: 0.8, color: color }}>{icon}</div>
    </div>
  );
}

function DashboardCard({ title, desc, icon, link }) {
  return (
    <Link to={link} style={{ textDecoration: 'none' }}>
      <div style={{ 
        background: 'rgba(255,255,255,0.02)', 
        border: '1px solid rgba(255,255,255,0.08)', 
        borderRadius: '8px', 
        padding: '20px', 
        textAlign: 'center',
        transition: 'all 0.3s',
        cursor: 'pointer'
      }}
      className="nav-card"
      onMouseEnter={(e) => {
        e.currentTarget.style.transform = 'translateY(-3px)';
        e.currentTarget.style.borderColor = '#ff3366';
        e.currentTarget.style.background = 'rgba(255, 51, 102, 0.05)';
      }}
      onMouseLeave={(e) => {
        e.currentTarget.style.transform = 'translateY(0)';
        e.currentTarget.style.borderColor = 'rgba(255,255,255,0.08)';
        e.currentTarget.style.background = 'rgba(255,255,255,0.02)';
      }}
      >
        <div style={{ fontSize: '30px', marginBottom: '10px' }}>{icon}</div>
        <h3 style={{ color: '#fff', marginBottom: '10px', fontSize: '16px' }}>{title}</h3>
        <p style={{ color: '#aaa', fontSize: '12px', margin: 0 }}>{desc}</p>
      </div>
    </Link>
  );
}
