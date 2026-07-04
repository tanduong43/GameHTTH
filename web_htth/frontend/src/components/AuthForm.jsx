import { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import api from '../api/api';

export default function AuthForm({ title = '⚓ ĐĂNG NHẬP' }) {
  const { fetchUser } = useAuth();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [isRegister, setIsRegister] = useState(false);
  const [message, setMessage] = useState(null);
  const [submitting, setSubmitting] = useState(false);

  const showMessage = (type, text) => {
    setMessage({ type, text });
    setTimeout(() => {
      setMessage((prev) => (prev && prev.text === text ? null : prev));
    }, 5000);
  };

  const handleLoginSubmit = async (e) => {
    e.preventDefault();
    if (!username || !password) {
      return showMessage('error', 'Vui lòng điền đầy đủ thông tin!');
    }
    setSubmitting(true);
    setMessage(null);
    try {
      const res = await api.post('login/', { username, password });
      if (res.data.success) {
        localStorage.setItem('token', res.data.token);
        await fetchUser();
        showMessage('success', 'Đăng nhập thành công!');
        setUsername('');
        setPassword('');
      } else {
        showMessage('error', res.data.message || 'Tài khoản hoặc mật khẩu không chính xác!');
      }
    } catch (err) {
      console.error(err);
      showMessage('error', 'Lỗi kết nối máy chủ!');
    } finally {
      setSubmitting(false);
    }
  };

  const handleRegisterSubmit = async (e) => {
    e.preventDefault();
    if (!username || !password) {
      return showMessage('error', 'Vui lòng điền đầy đủ thông tin!');
    }
    setSubmitting(true);
    setMessage(null);
    try {
      const res = await api.post('register/', { username, password });
      if (res.data.success) {
        setIsRegister(false);
        showMessage('success', res.data.message || 'Đăng ký thành công! Vui lòng đăng nhập.');
        setPassword('');
      } else {
        showMessage('error', res.data.message || 'Đăng ký thất bại!');
      }
    } catch (err) {
      console.error(err);
      showMessage('error', 'Lỗi kết nối máy chủ!');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="glass-panel" style={{ maxWidth: '400px', margin: '0 auto' }}>
      <h2 className="section-heading">
        {isRegister ? '⚓ ĐĂNG KÝ THÀNH VIÊN' : title}
      </h2>

      {message && (
        <div className={`alert alert-${message.type}`}>
          {message.text}
        </div>
      )}

      <form onSubmit={isRegister ? handleRegisterSubmit : handleLoginSubmit} autoComplete="on">
        <div className="input-group">
          <input
            type="text"
            name="username"
            placeholder="Tên tài khoản..."
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            disabled={submitting}
            autoComplete="username"
            required
          />
        </div>
        <div className="input-group" style={{ position: 'relative' }}>
          <input
            type={showPassword ? 'text' : 'password'}
            name="password"
            placeholder="Mật khẩu..."
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            disabled={submitting}
            autoComplete="current-password"
            required
            style={{ paddingRight: '45px' }}
          />
          <button
            type="button"
            onClick={() => setShowPassword(!showPassword)}
            style={{
              position: 'absolute',
              right: '10px',
              top: '50%',
              transform: 'translateY(-50%)',
              background: 'none',
              border: 'none',
              cursor: 'pointer',
              color: '#aaa',
              fontSize: '18px',
              padding: '4px',
              display: 'flex',
              alignItems: 'center',
            }}
            tabIndex={-1}
            aria-label={showPassword ? 'Ẩn mật khẩu' : 'Hiện mật khẩu'}
          >
            {showPassword ? (
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                <path d="M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94"/>
                <path d="M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19"/>
                <line x1="1" y1="1" x2="23" y2="23"/>
                <path d="M14.12 14.12a3 3 0 1 1-4.24-4.24"/>
              </svg>
            ) : (
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/>
                <circle cx="12" cy="12" r="3"/>
              </svg>
            )}
          </button>
        </div>

        <button
          type="submit"
          className="btn btn-primary"
          style={{ marginTop: '15px' }}
          disabled={submitting}
        >
          {submitting ? 'ĐANG XỬ LÝ...' : (isRegister ? 'ĐĂNG KÝ' : 'ĐĂNG NHẬP')}
        </button>
      </form>

      <div style={{ marginTop: '20px', textAlign: 'center', fontSize: '14px', color: 'var(--text-muted)' }}>
        {isRegister ? (
          <>
            Đã có tài khoản?{' '}
            <span
              onClick={() => { setIsRegister(false); setMessage(null); }}
              style={{ color: 'var(--primary)', cursor: 'pointer', fontWeight: 'bold' }}
            >
              Đăng nhập ngay
            </span>
          </>
        ) : (
          <>
            Chưa có tài khoản?{' '}
            <span
              onClick={() => { setIsRegister(true); setMessage(null); }}
              style={{ color: 'var(--primary)', cursor: 'pointer', fontWeight: 'bold' }}
            >
              Đăng ký ngay
            </span>
          </>
        )}
      </div>
    </div>
  );
}

