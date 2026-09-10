import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import api from '../../api/api';

export default function AdminDashboard() {
  const { user } = useAuth();
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [wealthViewMode, setWealthViewMode] = useState('tabs'); // 'tabs' | 'grid'
  const [activeWealthTab, setActiveWealthTab] = useState('spent_ruby');

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

  const getStatusText = (status, txn = null) => {
    if (status === 0 && txn && txn.real_amount > 0 && txn.real_amount !== txn.amount) {
      return { text: '⚠️ Chờ duyệt (Sai tiền)', color: '#fa8c16', bg: 'rgba(250,140,22,0.1)' };
    }
    switch (status) {
      case 0: return { text: 'Chờ duyệt', color: '#faad14', bg: 'rgba(250,173,20,0.1)' };
      case 1: return { text: 'Thành công', color: '#52c41a', bg: 'rgba(82,196,26,0.1)' };
      case 2: return { text: 'Đã duyệt (Sai m.giá)', color: '#1890ff', bg: 'rgba(24,144,255,0.1)' };
      case 3: return { text: 'Thất bại', color: '#f5222d', bg: 'rgba(245,34,45,0.1)' };
      case 4: return { text: 'Đã hủy', color: '#8c8c8c', bg: 'rgba(140,140,140,0.1)' };
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
                    const statusObj = getStatusText(txn.status, txn);
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

      {/* Wealth & Spending Rankings Section (Top 10 Accounts) */}
      <div className="glass-panel" style={{ padding: '25px', display: 'flex', flexDirection: 'column', gap: '20px' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', flexWrap: 'wrap', gap: '15px', borderBottom: '1px solid rgba(255,255,255,0.1)', paddingBottom: '15px' }}>
          <div>
            <h3 style={{ color: '#fff', margin: 0, fontSize: '20px', display: 'flex', alignItems: 'center', gap: '10px' }}>
              <span>🏆</span> Thống Kê Tài Sản & Tiêu Dùng (Top 10 Tài Khoản)
            </h3>
            <p style={{ color: '#aaa', margin: '5px 0 0 0', fontSize: '13px' }}>
              Xếp hạng top 10 tài khoản có lượng Tiêu Ruby, Có Ruby, Extol và Beri nhiều nhất toàn server
            </p>
          </div>

          <div style={{ display: 'flex', gap: '10px', alignItems: 'center', flexWrap: 'wrap' }}>
            {/* View Mode Toggle */}
            <div style={{ display: 'inline-flex', background: 'rgba(0,0,0,0.4)', borderRadius: '8px', padding: '3px', border: '1px solid rgba(255,255,255,0.1)' }}>
              <button
                onClick={() => setWealthViewMode('tabs')}
                style={{
                  padding: '6px 14px',
                  borderRadius: '6px',
                  border: 'none',
                  fontSize: '13px',
                  fontWeight: wealthViewMode === 'tabs' ? 'bold' : 'normal',
                  color: wealthViewMode === 'tabs' ? '#fff' : '#888',
                  background: wealthViewMode === 'tabs' ? 'rgba(255,51,102,0.6)' : 'transparent',
                  cursor: 'pointer',
                  transition: 'all 0.2s'
                }}
              >
                🗂️ Dạng Thẻ
              </button>
              <button
                onClick={() => setWealthViewMode('grid')}
                style={{
                  padding: '6px 14px',
                  borderRadius: '6px',
                  border: 'none',
                  fontSize: '13px',
                  fontWeight: wealthViewMode === 'grid' ? 'bold' : 'normal',
                  color: wealthViewMode === 'grid' ? '#fff' : '#888',
                  background: wealthViewMode === 'grid' ? 'rgba(255,51,102,0.6)' : 'transparent',
                  cursor: 'pointer',
                  transition: 'all 0.2s'
                }}
              >
                📑 Xem Tất Cả (Lưới 4 Bảng)
              </button>
            </div>
          </div>
        </div>

        {/* Tab selection buttons if in 'tabs' view */}
        {wealthViewMode === 'tabs' && (
          <div style={{ display: 'flex', gap: '10px', flexWrap: 'wrap' }}>
            {wealthCategories.map((cat) => {
              const isActive = activeWealthTab === cat.id;
              return (
                <button
                  key={cat.id}
                  onClick={() => setActiveWealthTab(cat.id)}
                  style={{
                    padding: '10px 18px',
                    borderRadius: '8px',
                    border: isActive ? `1px solid ${cat.color}` : '1px solid rgba(255,255,255,0.1)',
                    background: isActive ? cat.bgActive : 'rgba(255,255,255,0.03)',
                    color: isActive ? '#fff' : '#aaa',
                    fontWeight: isActive ? 'bold' : '500',
                    fontSize: '14px',
                    cursor: 'pointer',
                    display: 'flex',
                    alignItems: 'center',
                    gap: '8px',
                    transition: 'all 0.25s',
                    boxShadow: isActive ? `0 4px 15px ${cat.glow}` : 'none'
                  }}
                >
                  <span style={{ fontSize: '16px' }}>{cat.icon}</span>
                  <span>{cat.title}</span>
                  <span style={{
                    fontSize: '11px',
                    padding: '2px 7px',
                    borderRadius: '10px',
                    background: isActive ? cat.color : 'rgba(255,255,255,0.1)',
                    color: '#fff',
                    fontWeight: 'bold'
                  }}>
                    {stats?.[cat.dataKey]?.length || 0}
                  </span>
                </button>
              );
            })}
          </div>
        )}

        {/* Content View */}
        {wealthViewMode === 'tabs' ? (
          <div>
            <WealthTable 
              category={wealthCategories.find(c => c.id === activeWealthTab)} 
              data={stats?.[wealthCategories.find(c => c.id === activeWealthTab)?.dataKey] || []}
              isSingleTab={true}
            />
          </div>
        ) : (
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(320px, 1fr))', gap: '20px' }}>
            {wealthCategories.map((cat) => (
              <div 
                key={cat.id} 
                style={{ 
                  background: 'rgba(0,0,0,0.35)', 
                  borderRadius: '10px', 
                  border: `1px solid ${cat.border}`, 
                  padding: '15px', 
                  display: 'flex', 
                  flexDirection: 'column' 
                }}
              >
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '12px', borderBottom: '1px solid rgba(255,255,255,0.08)', paddingBottom: '10px' }}>
                  <h4 style={{ margin: 0, color: cat.color, fontSize: '15px', display: 'flex', alignItems: 'center', gap: '6px' }}>
                    <span>{cat.icon}</span> {cat.title}
                  </h4>
                  <span style={{ fontSize: '12px', color: '#888' }}>
                    {stats?.[cat.dataKey]?.length || 0} tài khoản
                  </span>
                </div>
                <WealthTable 
                  category={cat} 
                  data={stats?.[cat.dataKey] || []} 
                  isSingleTab={false}
                />
              </div>
            ))}
          </div>
        )}
      </div>

      {/* Quick Navigation Cards */}
      <div>
        <h3 style={{ color: '#fff', marginBottom: '15px' }}>🛠️ Truy Cập Nhanh Quản Lý</h3>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: '20px' }}>
          <DashboardCard title="Tài Khoản" desc="Quản lý thành viên, khóa nick" icon="👤" link="/admin/accounts" />
          <DashboardCard title="Nạp Tiền" desc="Cộng trừ coin thủ công" icon="💰" link="/admin/coins" />
          <DashboardCard title="Banking" desc="Duyệt đơn nạp thẻ & banking" icon="🏦" link="/admin/banking" />
          <DashboardCard title="Vật Phẩm" desc="Quản lý cấu hình item, shop" icon="💎" link="/admin/items" />
          <DashboardCard title="Giftcode" desc="Quản lý & tạo mã quà tặng" icon="🎁" link="/admin/giftcodes" />
          <DashboardCard title="Tin Tức" desc="Quản lý bài viết, thông báo" icon="📰" link="/admin/news" />
          <DashboardCard title="Lịch Sử Người Chơi" desc="Xem tiêu ruby, item, buff" icon="📜" link="/admin/logs" />
        </div>
      </div>
    </div>
  );
}

const wealthCategories = [
  {
    id: 'spent_ruby',
    dataKey: 'topSpentRuby',
    title: 'Top Tiêu Ruby',
    subtitle: 'Tổng Ruby đã tiêu trong game',
    icon: '💸',
    unit: 'Ruby',
    color: '#ff7a45',
    border: 'rgba(255,122,69,0.3)',
    bgActive: 'rgba(255,122,69,0.18)',
    glow: 'rgba(255,122,69,0.25)',
  },
  {
    id: 'hold_ruby',
    dataKey: 'topHoldRuby',
    title: 'Top Có Ruby',
    subtitle: 'Ruby hiện có trong túi',
    icon: '🔴',
    unit: 'Ruby',
    color: '#ff4d79',
    border: 'rgba(255,77,121,0.3)',
    bgActive: 'rgba(255,77,121,0.18)',
    glow: 'rgba(255,77,121,0.25)',
  },
  {
    id: 'hold_extol',
    dataKey: 'topHoldExtol',
    title: 'Top Có Extol',
    subtitle: 'Extol (VND) hiện có trong túi',
    icon: '🪙',
    unit: 'Extol',
    color: '#faad14',
    border: 'rgba(250,173,20,0.3)',
    bgActive: 'rgba(250,173,20,0.18)',
    glow: 'rgba(250,173,20,0.25)',
  },
  {
    id: 'hold_beri',
    dataKey: 'topHoldBeri',
    title: 'Top Có Beri',
    subtitle: 'Beri (Vàng) hiện có trong túi',
    icon: '💰',
    unit: 'Beri',
    color: '#52c41a',
    border: 'rgba(82,196,26,0.3)',
    bgActive: 'rgba(82,196,26,0.18)',
    glow: 'rgba(82,196,26,0.25)',
  },
];

function WealthTable({ category, data, isSingleTab }) {
  if (!data || data.length === 0) {
    return (
      <div style={{ padding: isSingleTab ? '40px 20px' : '25px 10px', textAlign: 'center', color: '#777' }}>
        <div style={{ fontSize: '28px', marginBottom: '8px', opacity: 0.6 }}>📭</div>
        <div>Chưa có dữ liệu tài khoản nào cho {category.title.toLowerCase()}</div>
      </div>
    );
  }

  const maxVal = Math.max(...data.map(d => Number(d.amount) || 0), 1);

  return (
    <div style={{ overflowX: 'auto', maxHeight: isSingleTab ? 'none' : '380px', overflowY: 'auto' }}>
      <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: isSingleTab ? '14px' : '13px' }}>
        <thead>
          <tr style={{ borderBottom: '1px solid rgba(255,255,255,0.1)', color: '#888', textAlign: 'left', position: 'sticky', top: 0, background: '#161d27', zIndex: 1 }}>
            <th style={{ padding: '10px 8px', width: '55px', textAlign: 'center' }}>Hạng</th>
            <th style={{ padding: '10px 8px' }}>Tài khoản</th>
            <th style={{ padding: '10px 8px' }}>Nhân vật</th>
            <th style={{ padding: '10px 8px', textAlign: 'right' }}>Số lượng</th>
            {isSingleTab && (
              <th style={{ padding: '10px 8px', width: '140px', textAlign: 'center' }}>Tỷ trọng</th>
            )}
          </tr>
        </thead>
        <tbody>
          {data.map((item, index) => {
            const val = Number(item.amount) || 0;
            const percentage = Math.min(100, Math.round((val / maxVal) * 100));
            return (
              <tr 
                key={item.account_id || item.username || index}
                style={{ 
                  borderBottom: '1px solid rgba(255,255,255,0.05)', 
                  color: '#ddd',
                  transition: 'background 0.2s'
                }}
                onMouseEnter={(e) => e.currentTarget.style.background = 'rgba(255,255,255,0.03)'}
                onMouseLeave={(e) => e.currentTarget.style.background = 'transparent'}
              >
                <td style={{ padding: '10px 8px', textAlign: 'center', fontWeight: 'bold' }}>
                  {index === 0 ? '🥇' : index === 1 ? '🥈' : index === 2 ? '🥉' : `#${index + 1}`}
                </td>
                <td style={{ padding: '10px 8px' }}>
                  <span style={{ color: '#ffac30', fontWeight: 'bold', letterSpacing: '0.3px' }}>
                    {item.username}
                  </span>
                </td>
                <td style={{ padding: '10px 8px' }}>
                  {item.char_names ? (
                    <span style={{
                      display: 'inline-block',
                      background: 'rgba(255,255,255,0.06)',
                      padding: '2px 8px',
                      borderRadius: '4px',
                      color: '#b0c4de',
                      fontSize: isSingleTab ? '12px' : '11px'
                    }}>
                      {item.char_names}
                    </span>
                  ) : (
                    <span style={{ color: '#666', fontStyle: 'italic', fontSize: '11px' }}>Chưa tạo NV</span>
                  )}
                </td>
                <td style={{ padding: '10px 8px', textAlign: 'right', fontWeight: 'bold', color: category.color }}>
                  {val.toLocaleString()} <span style={{ fontSize: '11px', color: '#aaa', fontWeight: 'normal' }}>{category.unit}</span>
                </td>
                {isSingleTab && (
                  <td style={{ padding: '10px 8px', textAlign: 'center' }}>
                    <div style={{ display: 'flex', alignItems: 'center', gap: '8px', justifyContent: 'center' }}>
                      <div style={{ flex: 1, height: '6px', background: 'rgba(255,255,255,0.1)', borderRadius: '3px', overflow: 'hidden' }}>
                        <div style={{ 
                          width: `${percentage}%`, 
                          height: '100%', 
                          background: `linear-gradient(90deg, ${category.color}88, ${category.color})`, 
                          borderRadius: '3px' 
                        }} />
                      </div>
                      <span style={{ fontSize: '11px', color: '#888', width: '32px', textAlign: 'right' }}>
                        {percentage}%
                      </span>
                    </div>
                  </td>
                )}
              </tr>
            );
          })}
        </tbody>
      </table>
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
