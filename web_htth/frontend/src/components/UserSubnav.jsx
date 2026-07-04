import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export default function UserSubnav({ activeTab }) {
  const navigate = useNavigate();
  const { handleLogout } = useAuth();

  return (
    <div className="forum-subnav">
      <button
        className={`subnav-btn ${activeTab === 'account' ? 'active' : ''}`}
        onClick={() => navigate('/dien-dan')}
      >
        👤 Tài Khoản
      </button>
      <button
        className={`subnav-btn ${activeTab === 'change-password' ? 'active' : ''}`}
        onClick={() => navigate('/dien-dan?tab=change-password')}
      >
        🔒 Đổi Mật Khẩu
      </button>

      <button className="subnav-btn subnav-logout" onClick={handleLogout}>
        🚪 Đăng Xuất
      </button>
    </div>
  );
}
