import { useState, useEffect } from 'react';
import { useAuth } from '../../context/AuthContext';
import { useSocket } from '../../context/SocketContext';
import AuthForm from '../../components/AuthForm';
import api from '../../api/api';
import '../../styles/App.css';

function TopupPage() {
  const { user, loading, fetchUser } = useAuth();
  const socket = useSocket();

  const [transferAmountOption, setTransferAmountOption] = useState('');
  const [transferAmount, setTransferAmount] = useState('');
  const [message, setMessage] = useState(null);
  const [activeDeposit, setActiveDeposit] = useState(null);
  const [creating, setCreating] = useState(false);
  const [history, setHistory] = useState([]);
  const [timeLeft, setTimeLeft] = useState('');

  const [bankConfig, setBankConfig] = useState({
    bankId: 'MB',
    accountNo: '123456789999',
    accountName: 'NGUYEN VAN A',
    bankName: 'MB Bank (Ngân hàng Quân Đội)',
  });

  // Fetch configuration, transaction history, and active deposit
  const fetchHistoryAndConfig = async () => {
    try {
      const configRes = await api.get('recharge/bank_config');
      if (configRes.data && configRes.data.success) {
        setBankConfig({
          bankId: configRes.data.bankId,
          accountNo: configRes.data.accountNo,
          accountName: configRes.data.accountName,
          bankName: configRes.data.bankName,
        });
      }

      const historyRes = await api.get('banking/history');
      if (historyRes.data && historyRes.data.success) {
        setHistory(historyRes.data.history);
      }

      // Check active deposit from backend directly
      const activeRes = await api.get('banking/active');
      if (activeRes.data && activeRes.data.success) {
        setActiveDeposit(activeRes.data.activeDeposit);
      } else {
        setActiveDeposit(null);
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

  // Countdown timer hook for active deposit expiration
  useEffect(() => {
    if (!activeDeposit || !activeDeposit.expires_at) {
      setTimeLeft('');
      return;
    }

    const updateTimer = () => {
      const expiresAt = new Date(activeDeposit.expires_at).getTime();
      const now = Date.now();
      const diff = expiresAt - now;

      if (diff <= 0) {
        setTimeLeft('Đã hết hạn');
        handleExpiry();
      } else {
        const minutes = Math.floor(diff / 60000);
        const seconds = Math.floor((diff % 60000) / 1000);
        setTimeLeft(`${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`);
      }
    };

    updateTimer(); // run once immediately
    const interval = setInterval(updateTimer, 1000);

    return () => clearInterval(interval);
  }, [activeDeposit]);

  const handleExpiry = async () => {
    try {
      const res = await api.get('banking/active');
      if (res.data && res.data.success) {
        if (!res.data.activeDeposit) {
          setActiveDeposit(null);
          showMessage('warning', 'Đơn nạp tiền đã hết hạn thanh toán!');

          const historyRes = await api.get('banking/history');
          if (historyRes.data && historyRes.data.success) {
            setHistory(historyRes.data.history);
          }
        }
      }
    } catch (err) {
      console.error('Lỗi khi kiểm tra hết hạn đơn:', err);
      setActiveDeposit(null);
    }
  };

  // Socket success listener
  useEffect(() => {
    if (!socket) return;

    const handleDepositSuccess = (data) => {
      console.log('TopupPage received local deposit_success:', data);
      showMessage('success', `🎉 Nạp tiền thành công! Bạn đã được cộng ${data.amount.toLocaleString()} Coin.`);
      setActiveDeposit(null);
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

  const handleCreateDeposit = async (amountToCreate) => {
    const finalAmount = parseInt(amountToCreate, 10);

    if (isNaN(finalAmount) || finalAmount < 10000) {
      showMessage('error', 'Vui lòng chọn hoặc nhập số tiền tối thiểu là 10,000đ VNĐ');
      return;
    }

    if (activeDeposit) {
      const confirmCancel = window.confirm(
        `Bạn đang có đơn nạp ${activeDeposit.amount.toLocaleString()}đ đang chờ thanh toán.\n\nNhấn OK nếu bạn muốn HỦY đơn cũ để tạo đơn mới.\nNhấn Cancel để tiếp tục thanh toán đơn cũ.`
      );
      if (!confirmCancel) {
        showMessage('info', 'Tiếp tục thanh toán đơn hàng hiện tại.');
        return;
      }

      // User chose to cancel the old order
      try {
        setCreating(true);
        const cancelRes = await api.post('banking/cancel', { code: activeDeposit.code });
        if (!cancelRes.data || !cancelRes.data.success) {
          showMessage('error', cancelRes.data?.message || 'Không thể hủy đơn cũ.');
          setCreating(false);
          return;
        }
      } catch (err) {
        console.error('Lỗi khi hủy đơn cũ:', err);
        showMessage('error', 'Lỗi khi hủy đơn cũ.');
        setCreating(false);
        return;
      }
    }

    try {
      setCreating(true);
      const res = await api.post('banking/deposit', { amount: finalAmount });
      if (res.data && res.data.success) {
        // Query the active endpoint immediately to get full information (including backend created_at / expires_at)
        const activeRes = await api.get('banking/active');
        if (activeRes.data && activeRes.data.success && activeRes.data.activeDeposit) {
          setActiveDeposit(activeRes.data.activeDeposit);
        } else {
          setActiveDeposit(res.data.deposit);
        }
        showMessage('success', 'Đã tạo yêu cầu nạp tiền! Vui lòng chuyển khoản.');

        // Refresh history
        const historyRes = await api.get('banking/history');
        if (historyRes.data && historyRes.data.success) {
          setHistory(historyRes.data.history);
        }
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

  const handleCancelDeposit = async () => {
    if (!activeDeposit) return;
    const confirmCancel = window.confirm('Bạn có chắc chắn muốn hủy đơn hàng này không?');
    if (!confirmCancel) return;

    try {
      setCreating(true);
      const res = await api.post('banking/cancel', { code: activeDeposit.code });
      if (res.data && res.data.success) {
        setActiveDeposit(null);
        showMessage('info', 'Đã hủy đơn nạp tiền.');

        const historyRes = await api.get('banking/history');
        if (historyRes.data && historyRes.data.success) {
          setHistory(historyRes.data.history);
        }
      } else {
        showMessage('error', res.data.message || 'Hủy đơn nạp thất bại.');
      }
    } catch (err) {
      console.error('Lỗi khi hủy đơn:', err);
      showMessage('error', 'Lỗi hệ thống khi hủy đơn.');
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
      case 4: return <span className="badge badge-cancelled">Đã hủy</span>;
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

          <div className="topup-section">
            <div className="topup-layout">
              {/* ================= LEFT COLUMN: QR CODE (ALWAYS DISPLAYED) ================= */}
              <div className="topup-left-col" style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center' }}>
                <h3 className="topup-col-title" style={{ width: '100%', textAlign: 'center' }}>
                  {activeDeposit ? 'QUÉT MÃ QR THANH TOÁN' : 'MÃ QR THANH TOÁN MẪU'}
                </h3>

                <div className="topup-qr-panel" style={{ background: '#fff', padding: '15px', borderRadius: '8px', boxShadow: '0 4px 12px rgba(0,0,0,0.15)', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                  <img
                    src={
                      activeDeposit
                        ? activeDeposit.vietqrUrl
                        : `https://img.vietqr.io/image/${bankConfig.bankId}-${bankConfig.accountNo}-compact2.png?addInfo=${encodeURIComponent('NAP ' + user.username.replace(/[^a-zA-Z0-9]/g, ''))}&accountName=${encodeURIComponent(bankConfig.accountName)}`
                    }
                    alt="VietQR Chuyển khoản"
                    style={{ width: '220px', height: '220px', objectFit: 'contain' }}
                  />
                  <p style={{ color: '#000', fontSize: '12px', marginTop: '8px', fontWeight: '500', textAlign: 'center' }}>
                    {activeDeposit ? 'Mã VietQR động MB Bank' : 'Mã VietQR mẫu MB Bank'}
                  </p>
                </div>

                {activeDeposit && activeDeposit.payosUrl && (
                  <div style={{ marginTop: '15px', width: '100%', textAlign: 'center' }}>
                    <a
                      href={activeDeposit.payosUrl}
                      target="_blank"
                      rel="noopener noreferrer"
                      className="btn btn-primary"
                      style={{ display: 'inline-block', width: '100%', padding: '10px 15px', textDecoration: 'none', background: 'linear-gradient(135deg, #0052cc 0%, #002266 100%)', border: 'none', borderRadius: '6px', fontWeight: 'bold' }}
                    >
                      💳 Thanh toán qua cổng PayOS
                    </a>
                  </div>
                )}

                {activeDeposit ? (
                  <div style={{ display: 'flex', alignItems: 'center', gap: '10px', marginTop: '20px', color: '#888', fontSize: '13px' }}>
                    <div className="spinner-border text-primary" role="status" style={{ width: '20px', height: '20px', border: '3px solid #ff3366', borderRightColor: 'transparent', borderRadius: '50%', animation: 'spin 1s linear infinite' }}></div>
                    <span>Đang chờ bạn thanh toán ngân hàng...</span>
                  </div>
                ) : (
                  <p style={{ color: '#666', fontSize: '12px', marginTop: '20px', textAlign: 'center', maxWidth: '280px' }}>
                    💡 Nhập số tiền và tạo mã QR ở bên cạnh để có nội dung chuyển khoản tự động chính xác nhất.
                  </p>
                )}
              </div>

              {/* ================= RIGHT COLUMN: SELECT AMOUNT OR DETAILS ================= */}
              <div className="topup-right-col">
                {activeDeposit ? (
                  /* Giao diện Đơn nạp đang chờ */
                  <div>
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', borderBottom: '1px solid rgba(0,0,0,0.1)', paddingBottom: '10px', marginBottom: '15px' }}>
                      <span style={{ fontSize: '15px', fontWeight: 'bold', color: '#000' }}>🛒 Đơn nạp đang thực hiện</span>
                      <button
                        onClick={handleCancelDeposit}
                        className="btn btn-outline"
                        style={{ fontSize: '11px', padding: '4px 10px', height: 'auto', width: 'auto', border: '1px solid #ff4d4f', color: '#ff4d4f' }}
                        disabled={creating}
                      >
                        Hủy đơn này
                      </button>
                    </div>

                    <div className="topup-info-block">
                      <p className="topup-info-label">Mã giao dịch (Code)</p>
                      <span className="topup-info-value" style={{ color: '#ffac30', fontSize: '18px', fontWeight: 'bold' }}>{activeDeposit.code}</span>
                    </div>

                    <div className="topup-info-block">
                      <p className="topup-info-label">Số tiền cần chuyển</p>
                      <div className="topup-info-row">
                        <span className="topup-info-value" style={{ color: '#52c41a', fontWeight: 'bold', fontSize: '18px' }}>
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
                    </div>

                    <div className="topup-info-block">
                      <p className="topup-info-label">Nội dung chuyển khoản</p>
                      <div className="topup-transfer-code topup-transfer-code--row" style={{ margin: '4px 0' }}>
                        <span style={{ fontSize: '16px', color: '#ffac30' }}>{activeDeposit.transferContent}</span>
                        <button
                          type="button"
                          onClick={() => handleCopyText(activeDeposit.transferContent, 'Nội dung chuyển khoản')}
                          className="topup-copy-badge topup-copy-badge--content"
                        >
                          📋 Copy
                        </button>
                      </div>
                      <p className="topup-transfer-note" style={{ fontSize: '11px', textAlign: 'left', color: '#ff4d4f' }}>
                        ⚠️ Chuyển chính xác số tiền và ghi đúng nội dung để hệ thống tự động cộng Coin!
                      </p>
                    </div>

                    <div className="topup-info-block">
                      <p className="topup-info-label">Thời gian hết hạn</p>
                      <span className="topup-info-value" style={{ color: '#ff4d4f', fontWeight: 'bold', fontSize: '16px' }}>
                        ⏳ {timeLeft || 'Đang tính toán...'}
                      </span>
                    </div>

                    <div className="topup-info-block topup-info-block--last">
                      <p className="topup-info-label">Trạng thái thanh toán</p>
                      <div style={{ marginTop: '5px' }}>
                        {getStatusBadge(activeDeposit.status)}
                      </div>
                    </div>
                  </div>
                ) : (
                  /* Giao diện Chọn số tiền */
                  <div>
                    <h3 className="topup-col-title">🏦 NHẬP SỐ TIỀN MUỐN NẠP</h3>

                    {/* Preset Amount Grid */}
                    <div style={{ marginBottom: '20px' }}>
                      <label style={{ display: 'block', fontSize: '13px', fontWeight: '600', marginBottom: '8px', color: '#000' }}>
                        Chọn nhanh mức nạp
                      </label>
                      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '10px' }}>
                        {[100000, 200000, 500000, 1000000].map((amt) => (
                          <button
                            key={amt}
                            type="button"
                            onClick={() => {
                              setTransferAmountOption(amt.toString());
                              setTransferAmount(amt.toString());
                            }}
                            className={`btn ${transferAmount === amt.toString() ? 'btn-primary' : 'btn-outline'}`}
                            style={{
                              padding: '10px',
                              fontSize: '13px',
                              fontWeight: 'bold',
                              borderColor: transferAmount === amt.toString() ? 'var(--primary)' : '#ccc',
                              color: transferAmount === amt.toString() ? '#fff' : '#000',
                              background: transferAmount === amt.toString() ? 'var(--primary)' : '#fff',
                            }}
                          >
                            {amt.toLocaleString()}đ
                          </button>
                        ))}
                      </div>
                    </div>

                    {/* Custom Amount Field */}
                    <div className="input-group" style={{ marginBottom: '20px' }}>
                      <label style={{ display: 'block', fontSize: '13px', fontWeight: '600', marginBottom: '8px', color: '#000' }}>
                        Hoặc nhập số tiền khác (VNĐ)
                      </label>
                      <input
                        type="number"
                        min="10000"
                        step="1000"
                        placeholder="Ví dụ: 150000..."
                        value={transferAmount}
                        onChange={(e) => {
                          setTransferAmount(e.target.value);
                          setTransferAmountOption('custom');
                        }}
                        className="topup-input"
                        style={{ color: '#000', background: '#fff', border: '1px solid #ccc' }}
                        required
                      />
                    </div>

                    <button
                      onClick={() => handleCreateDeposit(transferAmount)}
                      className="btn btn-primary"
                      style={{ width: '100%', fontSize: '14px', padding: '12px', fontWeight: 'bold' }}
                      disabled={creating}
                    >
                      {creating ? 'Đang khởi tạo...' : '⚓ TẠO MÃ QR'}
                    </button>

                    <div className="topup-tip topup-tip--success" style={{ marginTop: '20px' }}>
                      <p style={{ margin: 0, fontSize: '12px', color: '#52c41a' }}>
                        💡 <strong>Tỷ lệ nạp:</strong> 1.000 VNĐ = 1 Coin. (Ví dụ: 10.000đ = 10 Coin)
                      </p>
                      <p style={{ margin: '5px 0 0 0', fontSize: '11px', color: '#666', lineHeight: '1.4' }}>
                        Ngay sau khi bạn chuyển khoản đúng số tiền và nội dung, Coin sẽ được cộng vào ví của bạn trong vòng vài giây mà không cần tải lại trang.
                      </p>
                    </div>
                  </div>
                )}
              </div>
            </div>
          </div>

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

      {/* Dynamic Keyframes for spinner and style definitions */}
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
        .badge-cancelled { color: #8c8c8c; background: rgba(140,140,140,0.1); }
        .badge-unknown { color: #888; background: rgba(255,255,255,0.05); }

        /* Force black text color for TopupPage in light mode */
        .topup-page,
        .topup-page .section-heading,
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
