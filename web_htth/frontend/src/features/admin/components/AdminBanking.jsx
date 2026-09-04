import React, { useState, useEffect } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const STATUS_MAP = {
  0: { text: 'Chờ duyệt', color: '#faad14', bg: 'rgba(250,173,20,0.15)', border: 'rgba(250,173,20,0.3)' },
  1: { text: 'Thành công', color: '#52c41a', bg: 'rgba(82,196,26,0.15)', border: 'rgba(82,196,26,0.3)' },
  2: { text: 'Sai mệnh giá', color: '#1890ff', bg: 'rgba(24,144,255,0.15)', border: 'rgba(24,144,255,0.3)' },
  3: { text: 'Thất bại / Từ chối', color: '#f5222d', bg: 'rgba(245,34,45,0.15)', border: 'rgba(245,34,45,0.3)' },
};

export default function AdminBanking() {
  const { showMessage } = useOutletContext();
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [processingId, setProcessingId] = useState(null);
  const [filter, setFilter] = useState('pending'); // 'all' | 'pending' | 'done' | 'failed'
  const [multiplier, setMultiplier] = useState(1);
  const [updatingMultiplier, setUpdatingMultiplier] = useState(false);

  const fetchMultiplier = async () => {
    try {
      const res = await api.get('banking/multiplier');
      if (res.data && res.data.success) {
        setMultiplier(res.data.multiplier || 1);
      }
    } catch (e) {
      console.error('Error fetching multiplier:', e);
    }
  };

  const fetchOrders = async () => {
    try {
      setLoading(true);
      await fetchMultiplier();
      const res = await api.get('admin/banking/orders');
      if (res.data && res.data.success) {
        setOrders(res.data.orders);
      } else {
        showMessage('error', res.data?.message || 'Không thể tải danh sách đơn nạp.');
      }
    } catch (err) {
      console.error('Error fetching banking orders:', err);
      showMessage('error', 'Lỗi kết nối tới máy chủ.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchOrders();
  }, []);

  const handleSetMultiplier = async (newMult) => {
    if (newMult === multiplier) return;
    const confirmMsg = newMult === 1
      ? 'Bạn có chắc chắn muốn TẮT sự kiện nhân nạp, trở về tỷ lệ nạp x1 bình thường?'
      : `Bạn có chắc chắn muốn BẬT sự kiện NẠP x${newMult}? Tất cả người chơi nạp tiền trên Web và In-Game sẽ nhận gấp ${newMult} lần Coin.`;
    
    if (!window.confirm(confirmMsg)) return;

    try {
      setUpdatingMultiplier(true);
      const res = await api.post('admin/banking/multiplier', { multiplier: newMult });
      if (res.data && res.data.success) {
        setMultiplier(res.data.multiplier);
        showMessage('success', res.data.message || `Đã chuyển sang chế độ nạp x${newMult}`);
      } else {
        showMessage('error', res.data?.message || 'Không thể đổi hệ số nạp.');
      }
    } catch (err) {
      console.error('Error setting multiplier:', err);
      showMessage('error', 'Lỗi kết nối khi cập nhật hệ số nạp.');
    } finally {
      setUpdatingMultiplier(false);
    }
  };

  const handleApprove = async (order) => {
    if (!window.confirm(`Xác nhận DUYỆT đơn nạp ${Number(order.amount).toLocaleString()}đ cho tài khoản "${order.username}"?`)) {
      return;
    }

    try {
      setProcessingId(order.id);
      const res = await api.post('admin/banking/approve', {
        code: order.code,
        amount: order.amount
      });

      if (res.data && res.data.success) {
        showMessage('success', res.data.message);
        fetchOrders();
      } else {
        showMessage('error', res.data?.message || 'Duyệt thất bại.');
      }
    } catch (err) {
      console.error('Error approving payment:', err);
      showMessage('error', 'Lỗi hệ thống khi duyệt.');
    } finally {
      setProcessingId(null);
    }
  };

  const handleReject = async (order) => {
    if (!window.confirm(`Xác nhận TỪ CHỐI đơn nạp ${Number(order.amount).toLocaleString()}đ của tài khoản "${order.username}"?`)) {
      return;
    }

    try {
      setProcessingId(order.id);
      const res = await api.post('admin/banking/reject', {
        code: order.code
      });

      if (res.data && res.data.success) {
        showMessage('success', res.data.message);
        fetchOrders();
      } else {
        showMessage('error', res.data?.message || 'Từ chối thất bại.');
      }
    } catch (err) {
      console.error('Error rejecting payment:', err);
      showMessage('error', 'Lỗi hệ thống khi từ chối.');
    } finally {
      setProcessingId(null);
    }
  };

  // Filter orders
  const filteredOrders = orders.filter(order => {
    if (filter === 'pending') return order.status === 0;
    if (filter === 'done') return order.status === 1 || order.status === 2;
    if (filter === 'failed') return order.status === 3;
    return true; // 'all'
  });

  const pendingCount = orders.filter(o => o.status === 0).length;

  if (loading) {
    return <div className="glass-panel" style={{ padding: '30px', textAlign: 'center', color: '#fff' }}>Đang tải danh sách đơn nạp...</div>;
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
      {/* Header */}
      <div className="glass-panel" style={{ padding: '25px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', flexWrap: 'wrap', gap: '15px' }}>
        <div>
          <h2 style={{ color: '#ff3366', margin: 0, fontSize: '22px' }}>🏦 Quản Lý Đơn Nạp Banking</h2>
          <p style={{ color: '#aaa', margin: '5px 0 0 0', fontSize: '14px' }}>
            Duyệt hoặc từ chối các đơn nạp tiền chuyển khoản ngân hàng.
            {pendingCount > 0 && (
              <span style={{ color: '#faad14', fontWeight: 'bold', marginLeft: '8px' }}>
                ({pendingCount} đơn đang chờ duyệt)
              </span>
            )}
          </p>
        </div>
        <button onClick={fetchOrders} className="btn btn-outline" style={{ borderColor: 'rgba(255,255,255,0.15)', color: '#ccc', padding: '8px 16px', fontSize: '13px', background: 'transparent', cursor: 'pointer', borderRadius: '6px' }}>
          🔄 Tải lại
        </button>
      </div>

      {/* Event Multiplier Config Panel */}
      <div className="glass-panel" style={{
        padding: '18px 25px',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        flexWrap: 'wrap',
        gap: '15px',
        background: multiplier > 1
          ? 'linear-gradient(135deg, rgba(255, 77, 79, 0.15) 0%, rgba(250, 140, 22, 0.1) 100%)'
          : 'rgba(255, 255, 255, 0.03)',
        border: multiplier > 1 ? '1px solid rgba(255, 77, 79, 0.4)' : '1px solid rgba(255, 255, 255, 0.08)',
        borderRadius: '8px'
      }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
          <span style={{ fontSize: '26px' }}>{multiplier > 1 ? '🔥' : '⚙️'}</span>
          <div>
            <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
              <span style={{ fontWeight: 'bold', fontSize: '15px', color: '#fff' }}>Sự Kiện Nạp Tiền (Đồng bộ Web & Server)</span>
              <span style={{
                fontSize: '12px',
                fontWeight: 'bold',
                padding: '2px 8px',
                borderRadius: '4px',
                background: multiplier > 1 ? '#ff4d4f' : '#52c41a',
                color: '#fff'
              }}>
                Hiện tại: x{multiplier} {multiplier > 1 ? 'đang kích hoạt' : 'bình thường'}
              </span>
            </div>
            <p style={{ margin: '4px 0 0 0', fontSize: '13px', color: '#aaa' }}>
              Khi đổi ở đây, hệ số Coin nạp sẽ tự động đồng bộ ngay lập tức cho cả Web và Game Server.
            </p>
          </div>
        </div>

        <div style={{ display: 'flex', gap: '10px' }}>
          {[
            { mult: 1, label: 'Nạp x1 (Chuẩn)', color: '#8c8c8c' },
            { mult: 2, label: 'Nạp x2 (Gấp đôi)', color: '#fa8c16' },
            { mult: 3, label: 'Nạp x3 (Gấp ba)', color: '#ff4d4f' },
          ].map(item => (
            <button
              key={item.mult}
              type="button"
              disabled={updatingMultiplier}
              onClick={() => handleSetMultiplier(item.mult)}
              style={{
                padding: '8px 16px',
                borderRadius: '6px',
                fontSize: '13px',
                fontWeight: 'bold',
                cursor: updatingMultiplier ? 'not-allowed' : 'pointer',
                transition: 'all 0.2s',
                border: multiplier === item.mult ? `2px solid ${item.color}` : '1px solid rgba(255,255,255,0.15)',
                background: multiplier === item.mult ? item.color : 'rgba(255,255,255,0.05)',
                color: '#fff',
                boxShadow: multiplier === item.mult ? `0 0 12px ${item.color}66` : 'none'
              }}
            >
              {item.label} {multiplier === item.mult ? '✔' : ''}
            </button>
          ))}
        </div>
      </div>

      {/* Filter Tabs */}
      <div style={{ display: 'flex', gap: '8px', flexWrap: 'wrap' }}>
        {[
          { key: 'pending', label: `⏳ Chờ duyệt (${orders.filter(o => o.status === 0).length})`, color: '#faad14' },
          { key: 'done', label: `✅ Đã duyệt (${orders.filter(o => o.status === 1 || o.status === 2).length})`, color: '#52c41a' },
          { key: 'failed', label: `❌ Từ chối (${orders.filter(o => o.status === 3).length})`, color: '#f5222d' },
          { key: 'all', label: `📋 Tất cả (${orders.length})`, color: '#1890ff' },
        ].map(tab => (
          <button
            key={tab.key}
            onClick={() => setFilter(tab.key)}
            style={{
              padding: '8px 16px',
              borderRadius: '6px',
              border: filter === tab.key ? `1px solid ${tab.color}` : '1px solid rgba(255,255,255,0.1)',
              background: filter === tab.key ? `${tab.color}22` : 'rgba(255,255,255,0.03)',
              color: filter === tab.key ? tab.color : '#888',
              cursor: 'pointer',
              fontSize: '13px',
              fontWeight: filter === tab.key ? 'bold' : 'normal',
              transition: 'all 0.2s'
            }}
          >
            {tab.label}
          </button>
        ))}
      </div>

      {/* Orders Table */}
      <div className="glass-panel" style={{ padding: '20px' }}>
        {filteredOrders.length === 0 ? (
          <div style={{ padding: '40px', textAlign: 'center', color: '#888' }}>
            {filter === 'pending' 
              ? '🎉 Không có đơn nạp nào đang chờ duyệt!'
              : 'Không có đơn nạp nào trong danh mục này.'}
          </div>
        ) : (
          <div style={{ overflowX: 'auto' }}>
            <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '13px', color: '#ccc', textAlign: 'center' }}>
              <thead>
                <tr style={{ borderBottom: '2px solid rgba(255,255,255,0.1)', color: '#888', textAlign: 'center' }}>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>ID</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Tài khoản</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Tên NV</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Mã đơn</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Số tiền</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Thực nhận</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Trạng thái</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Thời gian</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Hành động</th>
                </tr>
              </thead>
              <tbody>
                {filteredOrders.map((order) => {
                  const statusInfo = STATUS_MAP[order.status] || STATUS_MAP[0];
                  const isPending = order.status === 0;
                  const isProcessing = processingId === order.id;

                  return (
                    <tr key={order.id} style={{ borderBottom: '1px solid rgba(255,255,255,0.05)', verticalAlign: 'middle' }}>
                      <td style={{ padding: '12px 8px', color: '#666', fontSize: '12px', textAlign: 'center' }}>#{order.id}</td>
                      <td style={{ padding: '12px 8px', color: '#ffac30', fontWeight: 'bold', textAlign: 'center' }}>{order.username}</td>
                      <td style={{ padding: '12px 8px', color: '#ff8a00', fontWeight: '600', textAlign: 'center' }}>{order.name || order.charName || "Chưa tạo"}</td>
                      <td style={{ padding: '12px 8px', textAlign: 'center' }}>
                        <span style={{
                          background: 'rgba(255, 172, 48, 0.1)',
                          color: '#ffac30',
                          padding: '3px 8px',
                          borderRadius: '4px',
                          fontWeight: 'bold',
                          fontSize: '12px',
                          fontFamily: 'monospace'
                        }}>
                          {order.code}
                        </span>
                      </td>
                      <td style={{ padding: '12px 8px', fontWeight: 'bold', fontSize: '14px', textAlign: 'center' }}>
                        {Number(order.amount).toLocaleString()}đ
                      </td>
                      <td style={{ padding: '12px 8px', fontWeight: 'bold', textAlign: 'center', color: '#52c41a' }}>
                        {Number(order.real_amount || 0).toLocaleString()}đ
                      </td>
                      <td style={{ padding: '12px 8px', textAlign: 'center' }}>
                        <span style={{
                          padding: '4px 10px',
                          borderRadius: '4px',
                          fontSize: '11px',
                          fontWeight: 'bold',
                          color: statusInfo.color,
                          backgroundColor: statusInfo.bg,
                          border: `1px solid ${statusInfo.border}`
                        }}>
                          {statusInfo.text}
                        </span>
                      </td>
                      <td style={{ padding: '12px 8px', fontSize: '12px', color: '#888', textAlign: 'center' }}>
                        {new Date(order.created_at).toLocaleString('vi-VN', {
                          day: '2-digit',
                          month: '2-digit',
                          year: 'numeric',
                          hour: '2-digit',
                          minute: '2-digit'
                        })}
                      </td>
                      <td style={{ padding: '12px 8px', textAlign: 'center' }}>
                        {isPending ? (
                          <div style={{ display: 'flex', gap: '6px', justifyContent: 'center' }}>
                            <button
                              onClick={() => handleApprove(order)}
                              disabled={isProcessing}
                              style={{
                                background: 'linear-gradient(135deg, #52c41a 0%, #389e0d 100%)',
                                border: 'none',
                                color: '#fff',
                                padding: '6px 14px',
                                fontSize: '12px',
                                fontWeight: 'bold',
                                borderRadius: '4px',
                                cursor: isProcessing ? 'not-allowed' : 'pointer',
                                opacity: isProcessing ? 0.6 : 1,
                                transition: 'all 0.2s'
                              }}
                            >
                              {isProcessing ? '...' : '✅ Duyệt'}
                            </button>
                            <button
                              onClick={() => handleReject(order)}
                              disabled={isProcessing}
                              style={{
                                background: 'transparent',
                                border: '1px solid rgba(245,34,45,0.4)',
                                color: '#f5222d',
                                padding: '6px 14px',
                                fontSize: '12px',
                                fontWeight: 'bold',
                                borderRadius: '4px',
                                cursor: isProcessing ? 'not-allowed' : 'pointer',
                                opacity: isProcessing ? 0.6 : 1,
                                transition: 'all 0.2s'
                              }}
                            >
                              ❌ Từ chối
                            </button>
                          </div>
                        ) : (
                          <span style={{ color: '#555', fontSize: '11px' }}>Đã xử lý</span>
                        )}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
