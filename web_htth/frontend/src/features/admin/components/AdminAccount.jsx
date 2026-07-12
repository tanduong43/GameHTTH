import React, { useState, useEffect } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

function AdminAccount() {
  const { showMessage } = useOutletContext();
  const [accounts, setAccounts] = useState([]);
  
  // Search & Filter states
  const [searchQuery, setSearchQuery] = useState('');
  const [statusFilter, setStatusFilter] = useState('all');
  const [lockFilter, setLockFilter] = useState('all');

  const fetchAccounts = async () => {
    try {
      const res = await api.get('admin/accounts');
      if (res.data.success) {
        setAccounts(res.data.accounts);
      }
    } catch {
      console.error("Lỗi lấy danh sách tài khoản");
    }
  };

  useEffect(() => {
    fetchAccounts();
  }, []);

  const handleUpdateUser = async (action, targetUsername) => {
    if (!targetUsername) return;

    try {
      const payload = { username: targetUsername, action };

      const res = await api.post('admin/update_user/', payload);
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        fetchAccounts();
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  // Filter accounts
  const filteredAccounts = accounts.filter(acc => {
    if (searchQuery.trim()) {
      const q = searchQuery.toLowerCase().trim();
      const matchUser = acc.user && acc.user.toLowerCase().includes(q);
      const matchId = String(acc.id) === q;
      if (!matchUser && !matchId) return false;
    }
    if (statusFilter === 'active' && acc.status !== 1) return false;
    if (statusFilter === 'inactive' && acc.status === 1) return false;
    
    if (lockFilter === 'banned' && acc.lock !== 1) return false;
    if (lockFilter === 'normal' && acc.lock === 1) return false;
    
    return true;
  });

  return (
    <div style={{ 
      maxWidth: '1000px', 
      margin: '0 auto', 
      fontFamily: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif',
      color: '#eee',
      padding: '20px'
    }}>
      <h3 style={{ 
        color: '#ff3366', 
        marginBottom: '24px', 
        textAlign: 'center',
        fontSize: '24px',
        fontWeight: '800',
        textTransform: 'uppercase',
        letterSpacing: '1px',
        background: 'linear-gradient(135deg, #ff3366 0%, #ff5e62 100%)',
        WebkitBackgroundClip: 'text',
        WebkitTextFillColor: 'transparent',
      }}>
        🔍 QUẢN LÝ TÀI KHOẢN
      </h3>
      
      {/* Search and Filters */}
      <div style={{
        display: 'flex',
        gap: '16px',
        flexWrap: 'wrap',
        marginBottom: '24px',
        background: 'rgba(20, 20, 20, 0.75)',
        backdropFilter: 'blur(10px)',
        border: '1px solid rgba(255, 255, 255, 0.08)',
        borderRadius: '12px',
        padding: '20px',
        boxShadow: '0 8px 32px rgba(0, 0, 0, 0.3)'
      }}>
        <div style={{ flex: '2 1 250px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Tìm kiếm:</label>
          <input
            type="text"
            placeholder="Tìm theo tên tài khoản hoặc ID..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              transition: 'all 0.25s'
            }}
          />
        </div>
        <div style={{ flex: '1 1 150px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Kích hoạt:</label>
          <select
            value={statusFilter}
            onChange={(e) => setStatusFilter(e.target.value)}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              cursor: 'pointer'
            }}
          >
            <option value="all">Tất cả thành viên</option>
            <option value="active">Đã kích hoạt (MTV)</option>
            <option value="inactive">Chưa kích hoạt</option>
          </select>
        </div>
        <div style={{ flex: '1 1 150px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Trạng thái khóa:</label>
          <select
            value={lockFilter}
            onChange={(e) => setLockFilter(e.target.value)}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              cursor: 'pointer'
            }}
          >
            <option value="all">Tất cả</option>
            <option value="normal">Bình thường</option>
            <option value="banned">Bị Banned</option>
          </select>
        </div>
      </div>

      <div style={{
        background: 'rgba(20, 20, 20, 0.75)',
        backdropFilter: 'blur(10px)',
        border: '1px solid rgba(255, 255, 255, 0.08)',
        borderRadius: '16px',
        padding: '24px',
        boxShadow: '0 8px 32px rgba(0, 0, 0, 0.3)'
      }}>
        <h4 style={{ 
          color: '#fff', 
          marginBottom: '20px', 
          textAlign: 'left', 
          borderBottom: '1px solid rgba(255, 255, 255, 0.08)', 
          paddingBottom: '12px',
          fontWeight: '700',
          fontSize: '16px'
        }}>
          📋 DANH SÁCH TÀI KHOẢN ({filteredAccounts.length} / {accounts.length})
        </h4>
        
        <div style={{ overflowX: 'auto' }}>
          <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left', fontSize: '14px', color: '#eee' }}>
            <thead>
              <tr style={{ borderBottom: '2px solid rgba(255,255,255,0.12)', background: 'rgba(0,0,0,0.3)' }}>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>ID</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Tài khoản</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Coin</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Thành viên</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Trạng thái</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Hành động</th>
              </tr>
            </thead>
            <tbody>
              {filteredAccounts.map(acc => (
                <tr key={acc.id} style={{ borderBottom: '1px solid rgba(255,255,255,0.05)' }}>
                  <td style={{ padding: '14px 10px', color: '#888' }}>{acc.id}</td>
                  <td style={{ padding: '14px 10px', fontWeight: 'bold', color: '#00e5ff' }}>{acc.user}</td>
                  <td style={{ padding: '14px 10px', color: '#faad14', fontWeight: '500' }}>💰 {acc.coin.toLocaleString()}</td>
                  <td style={{ padding: '14px 10px' }}>
                    <span style={{ 
                      padding: '3px 8px', 
                      borderRadius: '6px', 
                      fontSize: '12px', 
                      background: acc.status === 1 ? 'rgba(82,196,26,0.15)' : 'rgba(255,255,255,0.05)', 
                      color: acc.status === 1 ? '#52c41a' : '#aaa',
                      border: acc.status === 1 ? '1px solid rgba(82,196,26,0.3)' : '1px solid rgba(255,255,255,0.1)',
                      fontWeight: '600'
                    }}>
                      {acc.status === 1 ? 'Đã kích hoạt' : 'Chưa'}
                    </span>
                  </td>
                  <td style={{ padding: '14px 10px' }}>
                    <span style={{ 
                      padding: '3px 8px', 
                      borderRadius: '6px', 
                      fontSize: '12px', 
                      background: acc.lock === 1 ? 'rgba(255,77,79,0.15)' : 'rgba(82,196,26,0.15)', 
                      color: acc.lock === 1 ? '#ff4d4f' : '#52c41a',
                      border: acc.lock === 1 ? '1px solid rgba(255,77,79,0.3)' : '1px solid rgba(82,196,26,0.3)',
                      fontWeight: '600'
                    }}>
                      {acc.lock === 1 ? 'BANNED' : 'Bình thường'}
                    </span>
                  </td>
                  <td style={{ padding: '14px 10px', display: 'flex', gap: '8px', justifyContent: 'center', alignItems: 'center' }}>
                    <button
                      onClick={() => handleUpdateUser('activate', acc.user)}
                      style={{
                        padding: '6px 12px',
                        borderRadius: '6px',
                        border: `1px solid ${acc.status === 1 ? 'rgba(255,172,48,0.4)' : 'rgba(82,196,26,0.4)'}`,
                        background: 'transparent',
                        color: acc.status === 1 ? '#ffac30' : '#52c41a',
                        cursor: 'pointer',
                        fontSize: '12.5px',
                        fontWeight: 'bold',
                        transition: 'all 0.2s ease',
                      }}
                      onMouseEnter={(e) => {
                        e.target.style.background = acc.status === 1 ? 'rgba(255,172,48,0.1)' : 'rgba(82,196,26,0.1)';
                      }}
                      onMouseLeave={(e) => {
                        e.target.style.background = 'transparent';
                      }}
                    >
                      {acc.status === 1 ? '👑 Hủy MTV' : '⚡ Mở MTV'}
                    </button>
                    <button
                      onClick={() => handleUpdateUser('lock', acc.user)}
                      style={{
                        padding: '6px 12px',
                        borderRadius: '6px',
                        border: `1px solid ${acc.lock === 1 ? 'rgba(82,196,26,0.4)' : 'rgba(255,77,79,0.4)'}`,
                        background: 'transparent',
                        color: acc.lock === 1 ? '#52c41a' : '#ff4d4f',
                        cursor: 'pointer',
                        fontSize: '12.5px',
                        fontWeight: 'bold',
                        transition: 'all 0.2s ease',
                      }}
                      onMouseEnter={(e) => {
                        e.target.style.background = acc.lock === 1 ? 'rgba(82,196,26,0.1)' : 'rgba(255,77,79,0.1)';
                      }}
                      onMouseLeave={(e) => {
                        e.target.style.background = 'transparent';
                      }}
                    >
                      {acc.lock === 1 ? '🔓 Mở Khóa' : '🔒 Khóa Nick'}
                    </button>
                  </td>
                </tr>
              ))}
              {filteredAccounts.length === 0 && (
                <tr>
                  <td colSpan="6" style={{ padding: '30px', textAlign: 'center', color: '#888' }}>
                    Không tìm thấy tài khoản phù hợp
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default AdminAccount;
