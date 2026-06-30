import React, { useState } from 'react';
import AdminCoin from './components/AdminCoin';
import AdminAccount from './components/AdminAccount';
import AdminGiftcode from './components/AdminGiftcode';

function AdminPanel({ showMessage }) {
  const [adminTab, setAdminTab] = useState('coin');

  return (
    <div className="tab-content active admin-panel">
      <div className="tabs admin-tabs" style={{ marginBottom: '15px' }}>
        <button
          className={`tab-btn ${adminTab === 'coin' ? 'active' : ''}`}
          style={{ padding: '8px 10px', fontSize: '13px' }}
          onClick={() => setAdminTab('coin')}
        >
          Cộng Coin
        </button>
        <button
          className={`tab-btn ${adminTab === 'account' ? 'active' : ''}`}
          style={{ padding: '8px 10px', fontSize: '13px' }}
          onClick={() => setAdminTab('account')}
        >
          Tài Khoản
        </button>
        <button
          className={`tab-btn ${adminTab === 'giftcode' ? 'active' : ''}`}
          style={{ padding: '8px 10px', fontSize: '13px' }}
          onClick={() => setAdminTab('giftcode')}
        >
          Giftcode
        </button>
      </div>

      {adminTab === 'coin' && <AdminCoin showMessage={showMessage} />}
      {adminTab === 'account' && <AdminAccount showMessage={showMessage} />}
      {adminTab === 'giftcode' && <AdminGiftcode showMessage={showMessage} />}
    </div>
  );
}

export default AdminPanel;
