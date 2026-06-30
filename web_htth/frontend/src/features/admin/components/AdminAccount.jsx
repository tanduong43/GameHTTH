import React, { useState, useEffect } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

function AdminAccount() {
  const { showMessage } = useOutletContext();
  const [accounts, setAccounts] = useState([]);

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
        // Refresh list
        fetchAccounts();
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  return (
    <div className="admin-form" style={{ maxWidth: '800px', margin: '0 auto' }}>
      <h3 style={{ color: '#ff3366', marginBottom: '15px', textAlign: 'center' }}>🔍 QUẢN LÝ TÀI KHOẢN</h3>
      
      <h4 style={{ color: '#fff', marginBottom: '15px', textAlign: 'left', borderBottom: '1px solid #444', paddingBottom: '10px' }}>
        📋 DANH SÁCH TÀI KHOẢN ({accounts.length})
      </h4>
      
      <div style={{ overflowX: 'auto' }}>
        <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left', fontSize: '14px', color: '#eee' }}>
          <thead>
            <tr style={{ borderBottom: '1px solid #444', background: 'rgba(0,0,0,0.3)' }}>
              <th style={{ padding: '10px' }}>ID</th>
              <th style={{ padding: '10px' }}>Tài khoản</th>
              <th style={{ padding: '10px' }}>Coin</th>
              <th style={{ padding: '10px' }}>Thành viên</th>
              <th style={{ padding: '10px' }}>Trạng thái</th>
              <th style={{ padding: '10px', textAlign: 'center' }}>Hành động</th>
            </tr>
          </thead>
          <tbody>
            {accounts.map(acc => (
              <tr key={acc.id} style={{ borderBottom: '1px solid #333' }}>
                <td style={{ padding: '10px' }}>{acc.id}</td>
                <td style={{ padding: '10px', fontWeight: 'bold', color: '#1890ff' }}>{acc.user}</td>
                <td style={{ padding: '10px', color: '#faad14' }}>{acc.coin.toLocaleString()}</td>
                <td style={{ padding: '10px' }}>
                  <span style={{ padding: '2px 6px', borderRadius: '4px', fontSize: '12px', background: acc.status === 1 ? 'rgba(82,196,26,0.2)' : 'rgba(255,255,255,0.1)', color: acc.status === 1 ? '#52c41a' : '#aaa' }}>
                    {acc.status === 1 ? 'Đã kích hoạt' : 'Chưa'}
                  </span>
                </td>
                <td style={{ padding: '10px' }}>
                  <span style={{ padding: '2px 6px', borderRadius: '4px', fontSize: '12px', background: acc.lock === 1 ? 'rgba(255,77,79,0.2)' : 'rgba(255,255,255,0.1)', color: acc.lock === 1 ? '#ff4d4f' : '#aaa' }}>
                    {acc.lock === 1 ? 'BANNED' : 'Bình thường'}
                  </span>
                </td>
                <td style={{ padding: '10px', textAlign: 'center' }}>
                  <button
                    onClick={() => handleUpdateUser('lock', acc.user)}
                    style={{
                      padding: '4px 8px',
                      borderRadius: '4px',
                      border: `1px solid ${acc.lock === 1 ? '#52c41a' : '#ff4d4f'}`,
                      background: 'transparent',
                      color: acc.lock === 1 ? '#52c41a' : '#ff4d4f',
                      cursor: 'pointer',
                      fontSize: '12px',
                      fontWeight: 'bold'
                    }}
                  >
                    {acc.lock === 1 ? 'Mở Khóa' : 'Khóa'}
                  </button>
                </td>
              </tr>
            ))}
            {accounts.length === 0 && (
              <tr>
                <td colSpan="6" style={{ padding: '20px', textAlign: 'center', color: '#888' }}>
                  Chưa có dữ liệu tài khoản
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default AdminAccount;
