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
    fontFamily: '"Times New Roman", Times, serif',
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

  const handleResetTichNap = async () => {
    const confirmReset = window.confirm(
      "⚠️ CẢNH BÁO CỰC KỲ QUAN TRỌNG ⚠️\n\n" +
      "Hành động này sẽ đặt lại điểm Tích Lũy Nạp của TOÀN BỘ tài khoản về 0 và xóa lịch sử mốc nhận quà.\n" +
      "Bạn có chắc chắn muốn thực hiện reset không? Thao tác này không thể hoàn tác!"
    );
    if (!confirmReset) return;

    try {
      const res = await api.post('admin/reset_tichnap');
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ khi reset nạp!');
    }
  };

  const handleResetTichTieu = async () => {
    const confirmReset = window.confirm(
      "⚠️ CẢNH BÁO CỰC KỲ QUAN TRỌNG ⚠️\n\n" +
      "Hành động này sẽ đặt lại điểm Tích Tiêu Ruby của TOÀN BỘ nhân vật về 0 và xóa lịch sử mốc nhận quà.\n" +
      "Bạn có chắc chắn muốn thực hiện reset không? Thao tác này không thể hoàn tác!"
    );
    if (!confirmReset) return;

    try {
      const res = await api.post('admin/reset_tichtieu');
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ khi reset tiêu!');
    }
  };

  const handleResetHangDong = async () => {
    const confirmReset = window.confirm(
      "⚠️ CẢNH BÁO CỰC KỲ QUAN TRỌNG ⚠️\n\n" +
      "Hành động này sẽ đặt lại tiến trình Hang Động của TOÀN BỘ nhân vật về 0.\n" +
      "Bạn có chắc chắn muốn thực hiện reset không? Thao tác này không thể hoàn tác!"
    );
    if (!confirmReset) return;

    try {
      const res = await api.post('admin/reset_hangdong');
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ khi reset hang động!');
    }
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'row', flexWrap: 'wrap', gap: '30px', justifyContent: 'center', alignItems: 'flex-start', padding: '20px' }}>
      {/* Form cộng coin nhanh */}
      <form onSubmit={handleAddCoin} style={{ ...styles.formContainer, margin: '0', flex: '1 1 300px', maxWidth: '420px' }}>
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

      {/* Card Reset Tích Lũy Nạp */}
      <div style={{ ...styles.formContainer, margin: '0', flex: '1 1 300px', maxWidth: '420px' }}>
        <h3 style={{ ...styles.title, color: '#faad14', background: 'linear-gradient(135deg, #faad14 0%, #ffc069 100%)', WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent' }}>
          🔄 RESET TÍCH LŨY NẠP
        </h3>
        
        <p style={{ color: '#aaa', fontSize: '13.5px', marginBottom: '22px', lineHeight: '1.6', textAlign: 'center' }}>
          Đặt lại điểm tích lũy nạp của **TẤT CẢ** tài khoản về 0 và xóa trạng thái nhận quà mốc nạp. 
          <br />
          <span style={{ color: '#ff4d4f', fontWeight: 'bold' }}>*Khuyên nghị nên làm khi bảo trì.*</span>
        </p>

        <button 
          type="button" 
          onClick={handleResetTichNap}
          style={{
            ...styles.btnSubmit,
            backgroundColor: '#faad14',
            boxShadow: '0 4px 12px rgba(250, 173, 20, 0.25)',
          }}
          onMouseOver={(e) => e.target.style.backgroundColor = '#d48806'}
          onMouseOut={(e) => e.target.style.backgroundColor = '#faad14'}
        >
          Xác Nhận Reset Toàn Bộ
        </button>
      </div>

      {/* Card Reset Tích Lũy Tiêu */}
      <div style={{ ...styles.formContainer, margin: '0', flex: '1 1 300px', maxWidth: '420px' }}>
        <h3 style={{ ...styles.title, color: '#13c2c2', background: 'linear-gradient(135deg, #13c2c2 0%, #36cfc9 100%)', WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent' }}>
          🔄 RESET TÍCH TIÊU RUBY
        </h3>
        
        <p style={{ color: '#aaa', fontSize: '13.5px', marginBottom: '22px', lineHeight: '1.6', textAlign: 'center' }}>
          Đặt lại điểm tích tiêu ruby của **TẤT CẢ** nhân vật về 0 và xóa trạng thái nhận quà mốc tiêu. 
          <br />
          <span style={{ color: '#ff4d4f', fontWeight: 'bold' }}>*Khuyến nghị nên làm khi bảo trì.*</span>
        </p>

        <button 
          type="button" 
          onClick={handleResetTichTieu}
          style={{
            ...styles.btnSubmit,
            backgroundColor: '#13c2c2',
            boxShadow: '0 4px 12px rgba(19, 194, 194, 0.25)',
          }}
          onMouseOver={(e) => e.target.style.backgroundColor = '#08979c'}
          onMouseOut={(e) => e.target.style.backgroundColor = '#13c2c2'}
        >
          Xác Nhận Reset Toàn Bộ
        </button>
      </div>

      {/* Card Reset Hang Động */}
      <div style={{ ...styles.formContainer, margin: '0', flex: '1 1 300px', maxWidth: '420px' }}>
        <h3 style={{ ...styles.title, color: '#9254de', background: 'linear-gradient(135deg, #9254de 0%, #b37feb 100%)', WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent' }}>
          🔄 RESET HANG ĐỘNG
        </h3>
        
        <p style={{ color: '#aaa', fontSize: '13.5px', marginBottom: '22px', lineHeight: '1.6', textAlign: 'center' }}>
          Đặt lại tiến trình tầng Hang Động của **TẤT CẢ** nhân vật về 0. 
          <br />
          <span style={{ color: '#ff4d4f', fontWeight: 'bold' }}>*Khuyến nghị nên làm khi bảo trì.*</span>
        </p>

        <button 
          type="button" 
          onClick={handleResetHangDong}
          style={{
            ...styles.btnSubmit,
            backgroundColor: '#9254de',
            boxShadow: '0 4px 12px rgba(146, 84, 222, 0.25)',
          }}
          onMouseOver={(e) => e.target.style.backgroundColor = '#722ed1'}
          onMouseOut={(e) => e.target.style.backgroundColor = '#9254de'}
        >
          Xác Nhận Reset Toàn Bộ
        </button>
      </div>
    </div>
  );
}

export default AdminCoin;
