import { useState } from 'react';
import { useSearchParams, Navigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import api from '../../api/api';
import AuthForm from '../../components/AuthForm';
import UserSubnav from '../../components/UserSubnav';
import '../../styles/App.css';

function ForumPage() {
  const { user, loading, fetchUser } = useAuth();
  const [searchParams] = useSearchParams();
  const currentTab = searchParams.get('tab') || 'account';

  const [message, setMessage] = useState(null);
  const [submitting, setSubmitting] = useState(false);

  // States for change password
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [changePassSubmitting, setChangePassSubmitting] = useState(false);

  const handleChangePassword = async (e) => {
    e.preventDefault();
    if (!oldPassword || !newPassword || !confirmPassword) {
      return showMessage('error', 'Vui lòng điền đầy đủ tất cả các trường!');
    }
    if (newPassword.length < 3) {
      return showMessage('error', 'Mật khẩu mới phải từ 3 ký tự trở lên!');
    }
    if (newPassword !== confirmPassword) {
      return showMessage('error', 'Mật khẩu mới và xác nhận mật khẩu không khớp!');
    }

    setChangePassSubmitting(true);
    setMessage(null);
    try {
      const res = await api.post('change-password', { oldPassword, newPassword });
      if (res.data.success) {
        showMessage('success', 'Đổi mật khẩu thành công!');
        setOldPassword('');
        setNewPassword('');
        setConfirmPassword('');
      } else {
        showMessage('error', res.data.message || 'Đổi mật khẩu thất bại!');
      }
    } catch (err) {
      console.error(err);
      showMessage('error', 'Lỗi kết nối máy chủ!');
    } finally {
      setChangePassSubmitting(false);
    }
  };



  const showMessage = (type, text) => {
    setMessage({ type, text });
    setTimeout(() => {
      setMessage((prev) => (prev && prev.text === text ? null : prev));
    }, 5000);
  };

  const handleActivateAccount = async () => {
    setSubmitting(true);
    setMessage(null);
    try {
      const res = await api.post('activate/');
      if (res.data.success) {
        await fetchUser();
        showMessage('success', 'Kích hoạt tài khoản thành công! Bây giờ bạn đã có thể tham gia game.');
      } else {
        showMessage('error', res.data.message || 'Kích hoạt thất bại!');
      }
    } catch (err) {
      console.error(err);
      showMessage('error', 'Lỗi kết nối máy chủ!');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return <div className="loader">Đang tải dữ liệu...</div>;
  }

  if (!user) {
    return <Navigate to="/login" replace />;
  }



  return (
    <div className="forum-page">
      <UserSubnav activeTab={currentTab} />

      <div className="forum-content">
        <div className="glass-panel">
          {message && (
            <div className={`alert alert-${message.type}`}>
              {message.text}
            </div>
          )}

          {currentTab === 'account' ? (
            <div>
              <h2 className="section-heading">THÔNG TIN TÀI KHOẢN</h2>
              <div className="info-list">
                <p>
                  <strong>Tên đăng nhập:</strong>{' '}
                  <span className="highlight-text">{user.username}</span>
                </p>
                <p>
                  <strong>Nhân vật:</strong>{' '}
                  <span className="highlight-text">{user.character || 'Chưa tạo nhân vật'}</span>
                </p>
                <p>
                  <strong>Máy chủ:</strong>{' '}
                  <span className="highlight-text">{user.server || 'Làng Cối Xay Gió (S1)'}</span>
                </p>
                <p>
                  <strong>Số dư Coin:</strong>{' '}
                  <span className="highlight-text coin-text">{Number(user.coin || 0).toLocaleString()} Coin</span>
                </p>
                <p>
                  <strong>Trạng thái:</strong>{' '}
                  {user.status === 1 ? (
                    <span className="highlight-text status-active">Đã Kích Hoạt</span>
                  ) : (
                    <span className="highlight-text status-inactive">Chưa Kích Hoạt</span>
                  )}
                </p>
                {user.lock === 1 && (
                  <p>
                    <strong>Trạng thái khóa:</strong>{' '}
                    <span className="highlight-text" style={{ color: '#ff3366' }}>Bị Khóa (Banned)</span>
                  </p>
                )}
              </div>

              {user.status === 0 && (
                <div style={{ marginTop: '25px', borderTop: '1px solid rgba(255,255,255,0.1)', paddingTop: '20px', textAlign: 'center' }}>
                  <p style={{ color: '#ff4d79', marginBottom: '15px', fontSize: '14px', lineHeight: '1.5' }}>
                    Tài khoản của bạn chưa được kích hoạt thành viên. Bạn cần kích hoạt để có thể tham gia vào trò chơi!
                  </p>
                  <button
                    className="btn btn-upgrade"
                    onClick={handleActivateAccount}
                    disabled={submitting}
                  >
                    {submitting ? 'ĐANG KÍCH HOẠT...' : '⚡ KÍCH HOẠT THÀNH VIÊN (MIỄN PHÍ)'}
                  </button>
                </div>
              )}

              {user.status === 1 && (
                <div className="welcome-box" style={{ marginTop: '20px', textAlign: 'center' }}>
                  🎉 Tài khoản đã kích hoạt thành công! Bạn có thể sử dụng tài khoản này để tham gia game ngay.
                </div>
              )}
            </div>
          ) : (
            <div>
              <h2 className="section-heading">🔒 ĐỔI MẬT KHẨU</h2>
              <form onSubmit={handleChangePassword} style={{ display: 'flex', flexDirection: 'column', gap: '15px', maxWidth: '400px', margin: '0 auto' }}>
                <div className="input-group">
                  <input
                    type="password"
                    placeholder="Mật khẩu cũ..."
                    value={oldPassword}
                    onChange={(e) => setOldPassword(e.target.value)}
                    disabled={changePassSubmitting}
                    required
                  />
                </div>
                <div className="input-group">
                  <input
                    type="password"
                    placeholder="Mật khẩu mới..."
                    value={newPassword}
                    onChange={(e) => setNewPassword(e.target.value)}
                    disabled={changePassSubmitting}
                    required
                  />
                </div>
                <div className="input-group">
                  <input
                    type="password"
                    placeholder="Xác nhận mật khẩu mới..."
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    disabled={changePassSubmitting}
                    required
                  />
                </div>
                <button
                  type="submit"
                  className="btn btn-primary"
                  style={{ marginTop: '10px' }}
                  disabled={changePassSubmitting}
                >
                  {changePassSubmitting ? 'ĐANG CẬP NHẬT...' : 'CẬP NHẬT MẬT KHẨU'}
                </button>
              </form>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default ForumPage;
