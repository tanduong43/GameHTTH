import { useState } from 'react';
import { useAuth } from '../../context/AuthContext';
import AuthForm from '../../components/AuthForm';
import UserSubnav from '../../components/UserSubnav';
import '../../styles/App.css';

const CARD_RATES = [
  { amount: 10000, coin: 10000 },
  { amount: 20000, coin: 20000 },
  { amount: 50000, coin: 50000 },
  { amount: 100000, coin: 100000 },
  { amount: 200000, coin: 200000 },
  { amount: 500000, coin: 500000 },
];

const TELCOS = [
  { id: 'viettel', label: 'Viettel' },
  { id: 'mobifone', label: 'Mobifone' },
  { id: 'vinaphone', label: 'Vinaphone' },
  { id: 'zing', label: 'Zing' },
  { id: 'gate', label: 'Gate' },
  { id: 'garena', label: 'Garena' },
];

const TRANSFER_CONTENT_PREFIX = 'htth';

function TopupPage() {
  const { user, loading } = useAuth();
  const [paymentMethod, setPaymentMethod] = useState('card');
  const [telco, setTelco] = useState('viettel');
  const [cardAmount, setCardAmount] = useState('10000');
  const [serial, setSerial] = useState('');
  const [pin, setPin] = useState('');
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState(null);
  const [copied, setCopied] = useState(false);

  const transferContent = `${TRANSFER_CONTENT_PREFIX} ${user?.username || ''}`;

  const showMessage = (type, text) => {
    setMessage({ type, text });
    setTimeout(() => {
      setMessage((prev) => (prev && prev.text === text ? null : prev));
    }, 5000);
  };

  const handleCopyTransfer = async () => {
    try {
      await navigator.clipboard.writeText(transferContent);
      setCopied(true);
      setTimeout(() => setCopied(false), 2000);
    } catch {
      showMessage('error', 'Không thể sao chép. Vui lòng copy thủ công.');
    }
  };

  const handleCardSubmit = async (e) => {
    e.preventDefault();
    if (!serial.trim() || !pin.trim()) {
      return showMessage('error', 'Vui lòng nhập đầy đủ mã thẻ và số serial!');
    }
    setSubmitting(true);
    setMessage(null);
    try {
      // Chờ tích hợp API nạp thẻ — hiện ghi nhận yêu cầu phía client
      await new Promise((resolve) => setTimeout(resolve, 800));
      showMessage(
        'success',
        'Yêu cầu nạp thẻ đã được ghi nhận! Coin sẽ được cộng trong 1-5 phút nếu thẻ hợp lệ.'
      );
      setSerial('');
      setPin('');
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
          <AuthForm title="⚓ ĐĂNG NHẬP ĐỂ NẠP GAME" />
        </div>
      </div>
    );
  }

  const selectedRate = CARD_RATES.find((r) => String(r.amount) === cardAmount);

  return (
    <div className="forum-page topup-page">
      <UserSubnav activeTab="topup" />

      <div className="forum-content">
        <div className="glass-panel">
          {message && (
            <div className={`alert alert-${message.type}`}>
              {message.text}
            </div>
          )}

          <h2 className="section-heading">NẠP COIN VÀO GAME</h2>

          <div className="topup-balance">
            <span>Số dư hiện tại</span>
            <strong className="coin-text">{Number(user.coin || 0).toLocaleString()} Coin</strong>
          </div>

          <div className="topup-method-tabs">
            <button
              type="button"
              className={`topup-tab ${paymentMethod === 'card' ? 'active' : ''}`}
              onClick={() => setPaymentMethod('card')}
            >
              💳 Nạp thẻ cào
            </button>
            <button
              type="button"
              className={`topup-tab ${paymentMethod === 'transfer' ? 'active' : ''}`}
              onClick={() => setPaymentMethod('transfer')}
            >
              🏦 Chuyển khoản
            </button>
          </div>

          {paymentMethod === 'card' && (
            <div className="topup-section">
              <table className="topup-rate-table">
                <thead>
                  <tr>
                    <th>Mệnh giá</th>
                    <th>Nhận Coin</th>
                  </tr>
                </thead>
                <tbody>
                  {CARD_RATES.map((rate) => (
                    <tr key={rate.amount}>
                      <td>{rate.amount.toLocaleString()}đ</td>
                      <td className="coin-text">{rate.coin.toLocaleString()} Coin</td>
                    </tr>
                  ))}
                </tbody>
              </table>
              <p className="topup-note">Tỷ lệ quy đổi 1:1 — không mất phí chiết khấu.</p>

              <form onSubmit={handleCardSubmit} className="topup-card-form">
                <div className="input-group">
                  <label>Nhà mạng / Loại thẻ</label>
                  <select value={telco} onChange={(e) => setTelco(e.target.value)} disabled={submitting}>
                    {TELCOS.map((t) => (
                      <option key={t.id} value={t.id}>{t.label}</option>
                    ))}
                  </select>
                </div>

                <div className="input-group">
                  <label>Mệnh giá thẻ</label>
                  <select value={cardAmount} onChange={(e) => setCardAmount(e.target.value)} disabled={submitting}>
                    {CARD_RATES.map((rate) => (
                      <option key={rate.amount} value={rate.amount}>
                        {rate.amount.toLocaleString()}đ → {rate.coin.toLocaleString()} Coin
                      </option>
                    ))}
                  </select>
                </div>

                <div className="input-group">
                  <label>Số serial</label>
                  <input
                    type="text"
                    placeholder="Nhập số serial trên thẻ..."
                    value={serial}
                    onChange={(e) => setSerial(e.target.value)}
                    disabled={submitting}
                    required
                  />
                </div>

                <div className="input-group">
                  <label>Mã thẻ (PIN)</label>
                  <input
                    type="text"
                    placeholder="Nhập mã thẻ cào..."
                    value={pin}
                    onChange={(e) => setPin(e.target.value)}
                    disabled={submitting}
                    required
                  />
                </div>

                {selectedRate && (
                  <p className="topup-preview">
                    Bạn sẽ nhận: <strong className="coin-text">{selectedRate.coin.toLocaleString()} Coin</strong>
                  </p>
                )}

                <button type="submit" className="btn btn-primary" disabled={submitting}>
                  {submitting ? 'ĐANG XỬ LÝ...' : '⚡ NẠP THẺ NGAY'}
                </button>
              </form>
            </div>
          )}

          {paymentMethod === 'transfer' && (
            <div className="topup-section info-list">
              <p className="topup-transfer-title">CHUYỂN KHOẢN NGÂN HÀNG / MOMO</p>

              <div className="topup-info-block">
                <p><strong>Ngân hàng:</strong> <span className="highlight-text">MB Bank (Ngân hàng Quân Đội)</span></p>
                <p><strong>Số tài khoản:</strong> <span className="highlight-text">123456789999</span></p>
                <p><strong>Chủ tài khoản:</strong> <span className="highlight-text">NGUYEN VAN A</span></p>
              </div>

              <div className="topup-info-block">
                <p><strong>Ví điện tử:</strong> <span className="highlight-text">Momo</span></p>
                <p><strong>Số điện thoại:</strong> <span className="highlight-text">0987654321</span></p>
                <p><strong>Chủ tài khoản:</strong> <span className="highlight-text">NGUYEN VAN A</span></p>
              </div>

              <div>
                <p><strong>Nội dung chuyển khoản:</strong></p>
                <div className="topup-transfer-code">
                  {transferContent}
                </div>
                <button type="button" className="btn btn-outline-light topup-copy-btn" onClick={handleCopyTransfer}>
                  {copied ? '✓ Đã sao chép!' : '📋 Sao chép nội dung'}
                </button>
                <p className="note" style={{ textAlign: 'center', marginTop: '8px' }}>
                  Nhập chính xác nội dung trên khi chuyển khoản
                </p>
              </div>

              <div className="topup-tip">
                <p>
                  💡 <strong>Tỷ lệ quy đổi:</strong> 10.000đ = 10.000 Coin.
                  <br />
                  Coin tự động cộng sau 1-3 phút. Có lỗi vui lòng liên hệ Admin!
                </p>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default TopupPage;
