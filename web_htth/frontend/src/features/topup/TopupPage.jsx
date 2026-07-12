import { useState, useEffect } from 'react';
import { useAuth } from '../../context/AuthContext';
import { useSocket } from '../../context/SocketContext';
import AuthForm from '../../components/AuthForm';
import api from '../../api/api';
import '../../styles/App.css';

const AMOUNT_OPTIONS = [
  { value: '10000', label: '10,000đ (Nhận 10 Coin)' },
  { value: '20000', label: '20,000đ (Nhận 20 Coin)' },
  { value: '50000', label: '50,000đ (Nhận 50 Coin)' },
  { value: '100000', label: '100,000đ (Nhận 100 Coin)' },
  { value: '200000', label: '200,000đ (Nhận 200 Coin)' },
  { value: '500000', label: '500,000đ (Nhận 500 Coin)' },
];

function TopupPage() {
  const { user, loading, fetchUser } = useAuth();
  const socket = useSocket();

  const [transferAmountOption, setTransferAmountOption] = useState('');
  const [transferAmount, setTransferAmount] = useState('');
  const [message, setMessage] = useState(null);
  const [activeDeposit, setActiveDeposit] = useState(null);
  const [creating, setCreating] = useState(false);
  const [history, setHistory] = useState([]);

  const [bankConfig, setBankConfig] = useState({
    bankId: 'MB',
    accountNo: '123456789999',
    accountName: 'NGUYEN VAN A',
    bankName: 'MB Bank (Ngân hàng Quân Đội)',
  });

  // Fetch configuration and transaction history
  const fetchHistoryAndConfig = async () => {
    try {
      const configRes = await api.get('recharge/bank_config');
      let currentBankConfig = bankConfig;
      if (configRes.data && configRes.data.success) {
        currentBankConfig = {
          bankId: configRes.data.bankId,
          accountNo: configRes.data.accountNo,
          accountName: configRes.data.accountName,
          bankName: configRes.data.bankName,
        };
        setBankConfig(currentBankConfig);
      }

      const historyRes = await api.get('banking/history');
      if (historyRes.data && historyRes.data.success) {
        const historyList = historyRes.data.history;
        setHistory(historyList);

        // Reconcile active deposit
        const storedDepositStr = localStorage.getItem('active_banking_deposit');
        let foundActive = null;

        if (storedDepositStr) {
          try {
            const storedDeposit = JSON.parse(storedDepositStr);
            const stillPending = historyList.some(item => item.code === storedDeposit.code && item.status === 0);
            if (stillPending) {
              foundActive = storedDeposit;
            } else {
              localStorage.removeItem('active_banking_deposit');
            }
          } catch (e) {
            localStorage.removeItem('active_banking_deposit');
          }
        }

        // If no stored deposit in localStorage, but history has a pending deposit, reconstruct it!
        if (!foundActive) {
          const pendingTx = historyList.find(item => item.status === 0);
          if (pendingTx) {
            const cleanUser = user.username.replace(/[^a-zA-Z0-9]/g, '');
            const transferContent = `WSAC ${pendingTx.code} ${cleanUser}`.slice(0, 25).trim();
            const vietqrUrl = `https://img.vietqr.io/image/${currentBankConfig.bankId}-${currentBankConfig.accountNo}-compact2.png?amount=${pendingTx.amount}&addInfo=${encodeURIComponent(transferContent)}&accountName=${encodeURIComponent(currentBankConfig.accountName)}`;

            foundActive = {
              amount: pendingTx.amount,
              code: pendingTx.code,
              transferContent: transferContent,
              payosUrl: null,
              vietqrUrl: vietqrUrl
            };
            localStorage.setItem('active_banking_deposit', JSON.stringify(foundActive));
          }
        }

        setActiveDeposit(foundActive);
      }
    } catch (err) {
      console.error('Lỗi khi tải cấu hình và lịch sử nạp:', err);
    }
  };

  useEffect(() => {
    if (user) {
      fetchHistoryAndConfig();
    }
  }, [user]);

  // Socket success listener
  useEffect(() => {
    if (!socket) return;

    const handleDepositSuccess = (data) => {
      console.log('TopupPage received local deposit_success:', data);
      showMessage('success', `🎉 Nạp tiền thành công! Bạn đã được cộng ${data.amount.toLocaleString()} Coin.`);
      setActiveDeposit(null);
      localStorage.removeItem('active_banking_deposit');
      fetchUser(); // Sync user balance

      // Reload history after 2 seconds
      setTimeout(() => {
        api.get('banking/history').then((res) => {
          if (res.data && res.data.success) {
            setHistory(res.data.history);
          }
        });
      }, 2000);
    };

    socket.on('deposit_success', handleDepositSuccess);

    return () => {
      socket.off('deposit_success', handleDepositSuccess);
    };
  }, [socket, fetchUser]);

  const showMessage = (type, text) => {
    setMessage({ type, text });
    setTimeout(() => {
      setMessage((prev) => (prev && prev.text === text ? null : prev));
    }, 8000);
  };

  const handleCopyText = async (text, label) => {
    try {
      await navigator.clipboard.writeText(text);
      showMessage('success', `Đã sao chép ${label}!`);
    } catch {
      showMessage('error', 'Không thể sao chép. Vui lòng copy thủ công.');
    }
  };

  const handleCreateDeposit = async (e) => {
    e.preventDefault();
    const finalAmount = parseInt(transferAmountOption === 'custom' ? transferAmount : transferAmountOption, 10);

    if (isNaN(finalAmount) || finalAmount < 10000) {
      showMessage('error', 'Vui lòng chọn hoặc nhập số tiền tối thiểu là 10,000đ VNĐ');
      return;
    }

    try {
      setCreating(true);
      const res = await api.post('banking/deposit', { amount: finalAmount });
      if (res.data && res.data.success) {
        setActiveDeposit(res.data.deposit);
        localStorage.setItem('active_banking_deposit', JSON.stringify(res.data.deposit));
        showMessage('success', 'Đã tạo yêu cầu nạp tiền! Vui lòng chuyển khoản.');
      } else {
        showMessage('error', res.data.message || 'Tạo đơn nạp thất bại.');
      }
    } catch (err) {
      console.error('Lỗi khi tạo đơn nạp:', err);
      showMessage('error', 'Lỗi máy chủ khi tạo đơn nạp.');
    } finally {
      setCreating(false);
    }
  };

  const getStatusBadge = (status) => {
    switch (status) {
      case 0: return <span className="badge badge-pending">Chờ thanh toán</span>;
      case 1: return <span className="badge badge-success">Thành công</span>;
      case 2: return <span className="badge badge-warning">Sai số tiền</span>;
      case 3: return <span className="badge badge-failed">Thất bại</span>;
      default: return <span className="badge badge-unknown">Không rõ</span>;
    }
  };

  if (loading) {
    return <div className="loader">Đang tải dữ liệu...</div>;
  }

  if (!user) {
    return (
      <div className="forum-page">
        <div className="forum-content">
          <AuthForm title="⚓ ĐĂNG NHẬP ĐỂ NẠP GAME" />
        </div>
      </div>
    );
  }

  return (
    <div className="forum-page topup-page">
      <div className="forum-content">
        <div className="topup-panel">
          {message && (
            <div className={`alert alert-${message.type}`} style={{ marginBottom: '20px' }}>
              {message.text}
            </div>
          )}

          <h2 className="section-heading">NẠP TIỀN QUA NGÂN HÀNG (TỰ ĐỘNG)</h2>

          <div className="topup-balance">
            <span>Số dư hiện tại</span>
            <strong className="coin-text">{Number(user.coin || 0).toLocaleString()} Coin</strong>
          </div>

          {!activeDeposit ? (
            /* ================= CREATE DEPOSIT STATE ================= */
            <div className="topup-section">
              <p className="topup-transfer-title">
                🏦 NHẬP SỐ TIỀN MUỐN NẠP
              </p>

              <form onSubmit={handleCreateDeposit}>
                <div className="topup-amount-bar">
                  <div className="input-group">
                    <label>Chọn số tiền nạp</label>
                    <select
                      value={transferAmountOption}
                      onChange={(e) => {
                        const val = e.target.value;
                        setTransferAmountOption(val);
                        if (val === 'custom' || val === '') {
                          setTransferAmount('');
                        } else {
                          setTransferAmount(val);
                        }
                      }}
                      className="topup-select"
                      required
                    >
                      <option value="">-- Chọn mức nạp --</option>
                      {AMOUNT_OPTIONS.map((opt) => (
                        <option key={opt.value} value={opt.value}>{opt.label}</option>
                      ))}
                      <option value="custom">-- Nhập số tiền khác --</option>
                    </select>
                  </div>

                  {transferAmountOption === 'custom' && (
                    <div className="input-group">
                      <label>Nhập số tiền khác (VNĐ)</label>
                      <input
                        type="number"
                        min="10000"
                        step="1000"
                        placeholder="Ví dụ: 150000..."
                        value={transferAmount}
                        onChange={(e) => setTransferAmount(e.target.value)}
                        className="topup-input"
                        required
                      />
                    </div>
                  )}
                </div>

                <div style={{ marginTop: '20px', textAlign: 'center' }}>
                  <button type="submit" className="btn btn-primary" style={{ width: '100%', maxWidth: '220px', fontSize: '14px', padding: '10px' }} disabled={creating}>
                    {creating ? 'Đang khởi tạo...' : '⚓ TẠO YÊU CẦU NẠP TIỀN'}
                  </button>
                </div>
              </form>

              <div className="topup-tip topup-tip--success" style={{ marginTop: '30px' }}>
                <p>
                  💡 <strong>Tỷ lệ nạp:</strong> 10.000 VNĐ = 10.000 Coin.
                </p>
                <p>
                  Hệ thống hỗ trợ nạp tiền tự động 24/7. Ngay sau khi bạn chuyển khoản đúng số tiền và nội dung, Coin sẽ được cộng vào ví của bạn trong vòng vài giây mà không cần tải lại trang.
                </p>
              </div>
            </div>
          ) : (
            /* ================= ACTIVE DEPOSIT / WAITING STATE ================= */
            <div className="topup-section">
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', borderBottom: '1px solid rgba(0,0,0,0.1)', paddingBottom: '15px', marginBottom: '20px', width: '100%' }}>
                <span style={{ color: '#000', fontSize: '16px', fontWeight: 'bold', display: 'inline-flex', alignItems: 'center', margin: 0, whiteSpace: 'nowrap' }}>🛒 Đơn nạp đang thực hiện</span>
                <button
                  onClick={() => {
                    setActiveDeposit(null);
                    localStorage.removeItem('active_banking_deposit');
                    showMessage('info', 'Đã hủy đơn nạp hiện tại. Bạn có thể tạo đơn mới.');
                  }}
                  className="btn btn-outline"
                  style={{ display: 'inline-flex', alignItems: 'center', justifyContent: 'center', padding: '0 8px', height: '30px', width: '120px', fontSize: '10px', borderColor: '#000', color: '#000', borderRadius: '4px', margin: 0, whiteSpace: 'nowrap' }}
                >
                  Hủy đơn này
                </button>
              </div>

              <div className="topup-layout">
                <div className="topup-left-col">
                  <h3 className="topup-col-title">Thông tin chuyển khoản</h3>

                  <div className="topup-info-block">
                    <p className="topup-info-label">Ngân hàng nhận</p>
                    <p className="topup-info-value topup-info-bank">{bankConfig.bankName}</p>
                  </div>

                  <div className="topup-info-block">
                    <p className="topup-info-label">Số tài khoản</p>
                    <div className="topup-info-row">
                      <span className="topup-info-value">{bankConfig.accountNo}</span>
                      <button
                        type="button"
                        onClick={() => handleCopyText(bankConfig.accountNo, 'Số tài khoản')}
                        className="topup-copy-badge"
                      >
                        Sao chép
                      </button>
                    </div>
                    <p className="topup-info-sub">Chủ tài khoản: {bankConfig.accountName}</p>
                  </div>

                  <div className="topup-info-block">
                    <p className="topup-info-label">Số tiền chuyển khoản</p>
                    <div className="topup-info-row">
                      <span className="topup-info-value" style={{ color: '#52c41a', fontWeight: 'bold' }}>
                        {activeDeposit.amount.toLocaleString()}đ
                      </span>
                      <button
                        type="button"
                        onClick={() => handleCopyText(activeDeposit.amount.toString(), 'Số tiền')}
                        className="topup-copy-badge"
                      >
                        Sao chép
                      </button>
                    </div>
                    <p className="topup-info-sub" style={{ color: '#ff4d79' }}>⚠️ Chuyển chính xác số tiền này</p>
                  </div>

                  <div className="topup-info-block topup-info-block--last">
                    <p className="topup-info-label">Nội dung chuyển khoản</p>
                    <div className="topup-transfer-code topup-transfer-code--row">
                      <span style={{ fontSize: '18px', color: '#ffac30' }}>{activeDeposit.transferContent}</span>
                      <button
                        type="button"
                        onClick={() => handleCopyText(activeDeposit.transferContent, 'Nội dung chuyển khoản')}
                        className="topup-copy-badge topup-copy-badge--content"
                      >
                        📋 Copy
                      </button>
                    </div>
                    <p className="topup-transfer-note" style={{ fontSize: '11px' }}>
                      ⚠️ Ghi đúng nội dung trên để hệ thống tự động cộng Coin!
                    </p>
                  </div>
                </div>

                <div className="topup-right-col" style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center' }}>
                  <h3 className="topup-col-title" style={{ width: '100%', textAlign: 'center' }}>Quét mã QR để thanh toán</h3>

                  {activeDeposit.payosUrl && (
                    <div style={{ marginBottom: '15px', width: '100%', textAlign: 'center' }}>
                      <a
                        href={activeDeposit.payosUrl}
                        target="_blank"
                        rel="noopener noreferrer"
                        className="btn btn-primary"
                        style={{ display: 'inline-block', width: '100%', padding: '10px 15px', textDecoration: 'none', background: 'linear-gradient(135deg, #0052cc 0%, #002266 100%)', border: 'none', borderRadius: '6px', fontWeight: 'bold' }}
                      >
                        💳 Thanh toán qua cổng PayOS
                      </a>
                      <p style={{ fontSize: '11px', color: '#aaa', marginTop: '6px' }}>Mở cổng thanh toán ngân hàng bảo mật của PayOS</p>
                    </div>
                  )}

                  <div className="topup-qr-panel" style={{ background: '#fff', padding: '15px', borderRadius: '8px', boxShadow: '0 4px 12px rgba(0,0,0,0.5)', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                    <img
                      src={activeDeposit.vietqrUrl}
                      alt="VietQR Chuyển khoản"
                      style={{ width: '220px', height: '220px', objectFit: 'contain' }}
                    />
                    <p style={{ color: '#000', fontSize: '12px', marginTop: '8px', fontWeight: '500', textAlign: 'center' }}>
                      Mã VietQR động MB Bank
                    </p>
                  </div>

                  {/* Payment Spinner */}
                  <div style={{ display: 'flex', alignItems: 'center', gap: '10px', marginTop: '20px', color: '#aaa', fontSize: '13px' }}>
                    <div className="spinner-border text-primary" role="status" style={{ width: '20px', height: '20px', border: '3px solid #ff3366', borderRightColor: 'transparent', borderRadius: '50%', animation: 'spin 1s linear infinite' }}></div>
                    <span>Đang chờ bạn thanh toán ngân hàng...</span>
                  </div>
                </div>
              </div>
            </div>
          )}

          {/* ================= RECENT BANK DEPOSITS HISTORY ================= */}
          {history.length > 0 && (
            <div className="topup-section" style={{ marginTop: '30px', borderTop: '1px solid rgba(0,0,0,0.1)', paddingTop: '20px' }}>
              <h3 style={{ color: '#000', fontSize: '16px', marginBottom: '15px' }}>🔔 Lịch sử nạp ngân hàng gần đây</h3>
              <div style={{ overflowX: 'auto' }}>
                <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '13px', color: '#000' }}>
                  <thead>
                    <tr style={{ borderBottom: '1px solid rgba(0,0,0,0.1)', color: '#333', textAlign: 'left' }}>
                      <th style={{ padding: '8px 5px' }}>Mã Code</th>
                      <th style={{ padding: '8px 5px' }}>Số tiền nạp</th>
                      <th style={{ padding: '8px 5px' }}>Thực nhận</th>
                      <th style={{ padding: '8px 5px' }}>Trạng thái</th>
                      <th style={{ padding: '8px 5px' }}>Thời gian</th>
                    </tr>
                  </thead>
                  <tbody>
                    {history.map((tx, idx) => (
                      <tr key={idx} style={{ borderBottom: '1px solid rgba(0,0,0,0.05)' }}>
                        <td style={{ padding: '10px 5px', color: '#b26a00', fontWeight: 'bold' }}>{tx.code || 'N/A'}</td>
                        <td style={{ padding: '10px 5px' }}>{Number(tx.amount).toLocaleString()}đ</td>
                        <td style={{ padding: '10px 5px', color: '#2b8c00', fontWeight: 'bold' }}>{Number(tx.real_amount).toLocaleString()}đ</td>
                        <td style={{ padding: '10px 5px' }}>{getStatusBadge(tx.status)}</td>
                        <td style={{ padding: '10px 5px', color: '#555', fontSize: '11px' }}>
                          {new Date(tx.created_at).toLocaleString('vi-VN')}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      </div>

      {/* Dynamic Keyframes for spinner animation */}
      <style>{`
        @keyframes spin {
          to { transform: rotate(360deg); }
        }
        .badge {
          display: inline-block;
          padding: 3px 8px;
          border-radius: 4px;
          font-size: 11px;
          font-weight: bold;
        }
        .badge-pending { color: #faad14; background: rgba(250,173,20,0.1); }
        .badge-success { color: #52c41a; background: rgba(82,196,26,0.1); }
        .badge-warning { color: #1890ff; background: rgba(24,144,255,0.1); }
        .badge-failed { color: #f5222d; background: rgba(245,34,45,0.1); }
        .badge-unknown { color: #888; background: rgba(255,255,255,0.05); }

        /* Force black text color for TopupPage */
        .topup-page,
        .topup-page .section-heading,
        .topup-page .topup-transfer-title,
        .topup-page .topup-col-title,
        .topup-page .topup-info-label,
        .topup-page .topup-info-sub,
        .topup-page .topup-transfer-note,
        .topup-page .topup-qr-desc,
        .topup-page .topup-balance span,
        .topup-page label,
        .topup-page th,
        .topup-page td,
        .topup-page table,
        .topup-page tr,
        .topup-page .topup-info-value:not([style*="color"]),
        .topup-page span:not(.badge):not(.coin-text):not(.topup-info-value) {
          color: #000000 !important;
        }
        .topup-page .topup-info-bank {
          color: #1e293b !important;
        }
        .topup-page select.topup-select,
        .topup-page input.topup-input {
          color: #000000 !important;
          background: #ffffff !important;
          border: 1px solid #ccc !important;
        }
      `}</style>
    </div>
  );
}

export default TopupPage;
