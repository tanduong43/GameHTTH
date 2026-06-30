import React, { useState } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

function AdminCoin() {
  const { showMessage } = useOutletContext();
  const [targetUser, setTargetUser] = useState('');
  const [amount, setAmount] = useState(0);

  const handleAddCoin = async (e) => {
    e.preventDefault();
    try {
      const res = await api.post('admin/add_coin/', { username: targetUser, amount });
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        setTargetUser('');
        setAmount(0);
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  return (
    <form onSubmit={handleAddCoin} className="admin-form">
      <h3 style={{ color: '#ff3366', marginBottom: '15px', textAlign: 'center' }}>💰 CỘNG COIN NHANH</h3>
      <div className="input-group">
        <input
          type="text"
          placeholder="Tên tài khoản (username)"
          value={targetUser}
          onChange={(e) => setTargetUser(e.target.value)}
          required
          style={{ width: '100%', padding: '10px', borderRadius: '5px', border: 'none' }}
        />
      </div>
      <div className="input-group" style={{ marginTop: '15px' }}>
        <input
          type="number"
          placeholder="Số lượng Coin"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          required
          style={{ width: '100%', padding: '10px', borderRadius: '5px', border: 'none' }}
        />
      </div>
      <button type="submit" className="btn btn-primary" style={{ width: '100%', marginTop: '15px' }}>
        Xác Nhận Cộng
      </button>
    </form>
  );
}

export default AdminCoin;
