import { useState } from 'react';
import { useAuth } from '../../context/AuthContext';
import api from '../../api/api';
import AuthForm from '../../components/AuthForm';
import UserSubnav from '../../components/UserSubnav';
import '../../styles/App.css';

function ForumPage() {
  const { user, loading, fetchUser } = useAuth();

  const [message, setMessage] = useState(null);
  const [submitting, setSubmitting] = useState(false);



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
    return (
      <div className="forum-page">
        <div className="forum-content">
          <AuthForm />
        </div>
      </div>
    );
  }



  return (
    <div className="forum-page">
      <UserSubnav activeTab={'account'} />

      <div className="forum-content">
        <div className="glass-panel">
          {message && (
            <div className={`alert alert-${message.type}`}>
              {message.text}
            </div>
          )}

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
        </div>
      </div>
    </div>
  );
}

export default ForumPage;
