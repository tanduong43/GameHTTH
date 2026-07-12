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

  const fetchOrders = async () => {
    try {
      setLoading(true);
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
            <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '13px', color: '#ccc' }}>
              <thead>
                <tr style={{ borderBottom: '2px solid rgba(255,255,255,0.1)', color: '#888', textAlign: 'left' }}>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600' }}>ID</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600' }}>Tài khoản</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600' }}>Mã đơn</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'right' }}>Số tiền</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'right' }}>Thực nhận</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Trạng thái</th>
                  <th style={{ padding: '12px 8px', fontSize: '11px', textTransform: 'uppercase', fontWeight: '600' }}>Thời gian</th>
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
                      <td style={{ padding: '12px 8px', color: '#666', fontSize: '12px' }}>#{order.id}</td>
                      <td style={{ padding: '12px 8px', color: '#ffac30', fontWeight: 'bold' }}>{order.username}</td>
                      <td style={{ padding: '12px 8px' }}>
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
                      <td style={{ padding: '12px 8px', fontWeight: 'bold', fontSize: '14px', textAlign: 'right' }}>
                        {Number(order.amount).toLocaleString()}đ
                      </td>
                      <td style={{ padding: '12px 8px', fontWeight: 'bold', textAlign: 'right', color: '#52c41a' }}>
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
                      <td style={{ padding: '12px 8px', fontSize: '12px', color: '#888' }}>
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
