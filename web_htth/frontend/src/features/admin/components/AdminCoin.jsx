import React, { useState } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const styles = {
  formContainer: {
    maxWidth: '420px',
    margin: '40px auto',
    padding: '30px',
    background: 'rgba(26, 26, 26, 0.6)',
    border: '1px solid #333',
    borderRadius: '16px',
    boxShadow: '0 8px 32px 0 rgba(0, 0, 0, 0.4)',
    fontFamily: '"Inter", "Roboto", sans-serif',
    color: '#fff',
    backdropFilter: 'blur(8px)',
  },
  title: {
    color: '#ff3366',
    marginBottom: '25px',
    textAlign: 'center',
    fontSize: '20px',
    fontWeight: '700',
    textTransform: 'uppercase',
    letterSpacing: '1px'
  },
  formGroup: {
    marginBottom: '20px',
  },
  label: {
    display: 'block',
    marginBottom: '8px',
    fontWeight: '600',
    textAlign: 'left',
    color: '#aaa',
    fontSize: '13px'
  },
  input: {
    width: '100%',
    padding: '12px 14px',
    borderRadius: '8px',
    border: '1px solid #444',
    backgroundColor: '#111',
    color: '#fff',
    boxSizing: 'border-box',
    fontSize: '14px',
    outline: 'none',
    transition: 'all 0.3s ease',
  },
  btnSubmit: {
    width: '100%',
    padding: '12px',
    backgroundColor: '#ff3366',
    color: '#fff',
    border: 'none',
    borderRadius: '8px',
    fontWeight: '700',
    fontSize: '15px',
    cursor: 'pointer',
    boxShadow: '0 4px 12px rgba(255, 51, 102, 0.25)',
    transition: 'all 0.3s ease',
    marginTop: '10px'
  }
};

function AdminCoin() {
  const { showMessage } = useOutletContext();
  const [targetUser, setTargetUser] = useState('');
  const [amount, setAmount] = useState('');
  const [focusField, setFocusField] = useState('');

  const handleAddCoin = async (e) => {
    e.preventDefault();
    if (!targetUser.trim()) return showMessage('error', 'Vui lòng nhập tên tài khoản!');
    if (!amount || Number(amount) <= 0) return showMessage('error', 'Số lượng coin phải lớn hơn 0!');

    try {
      const res = await api.post('admin/add_coin/', { username: targetUser.trim(), amount: Number(amount) });
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        setTargetUser('');
        setAmount('');
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  return (
    <form onSubmit={handleAddCoin} style={styles.formContainer}>
      <h3 style={styles.title}>💰 CỘNG COIN NHANH</h3>
      
      <div style={styles.formGroup}>
        <label style={styles.label}>Tên tài khoản (username):</label>
        <input
          type="text"
          placeholder="Ví dụ: player1"
          value={targetUser}
          onChange={(e) => setTargetUser(e.target.value)}
          onFocus={() => setFocusField('username')}
          onBlur={() => setFocusField('')}
          required
          style={{
            ...styles.input,
            borderColor: focusField === 'username' ? '#ff3366' : '#444',
            boxShadow: focusField === 'username' ? '0 0 0 2px rgba(255, 51, 102, 0.2)' : 'none'
          }}
        />
      </div>

      <div style={styles.formGroup}>
        <label style={styles.label}>Số lượng Coin:</label>
        <input
          type="number"
          placeholder="Nhập số coin cần cộng"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          onFocus={() => setFocusField('amount')}
          onBlur={() => setFocusField('')}
          required
          min="1"
          style={{
            ...styles.input,
            borderColor: focusField === 'amount' ? '#ff3366' : '#444',
            boxShadow: focusField === 'amount' ? '0 0 0 2px rgba(255, 51, 102, 0.2)' : 'none'
          }}
        />
      </div>

      <button 
        type="submit" 
        style={styles.btnSubmit}
        onMouseOver={(e) => e.target.style.backgroundColor = '#e62e5c'}
        onMouseOut={(e) => e.target.style.backgroundColor = '#ff3366'}
      >
        Xác Nhận Cộng
      </button>
    </form>
  );
}

export default AdminCoin;
