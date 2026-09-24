import React, { useState, useEffect } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const CATEGORY_MAP = {
  3: { label: '⚔️ Trang bị (item3)', color: '#fa8c16' },
  4: { label: '🧪 Vật phẩm / Rương (item4)', color: '#1890ff' },
  7: { label: '💎 Đá khảm / NL (item7)', color: '#722ed1' },
};

const COLOR_MAP = {
  0: { label: 'Trắng', color: '#d9d9d9' },
  1: { label: 'Xanh lá', color: '#52c41a' },
  2: { label: 'Xanh lam', color: '#1890ff' },
  3: { label: 'Cam', color: '#fa8c16' },
  4: { label: 'Vàng kim', color: '#fadb14' },
};

export default function AdminAuction() {
  const { showMessage } = useOutletContext();

  const [auctions, setAuctions] = useState([]);
  const [stats, setStats] = useState({});
  const [loading, setLoading] = useState(true);
  const [filterStatus, setFilterStatus] = useState('');

  // Modal thêm mới
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [searchKeyword, setSearchKeyword] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [searchingTemplates, setSearchingTemplates] = useState(false);

  // Form Data
  const [formData, setFormData] = useState({
    name: '',
    category: 4,
    template_id: '',
    quantity: 1,
    color: 0,
    start_price: 20,
    step_price: 5,
    buyout_price: 0,
    duration_hours: 2,
    duration_minutes: 0,
  });
  const [submitting, setSubmitting] = useState(false);

  // Modal Xóa
  const [deleteTarget, setDeleteTarget] = useState(null);
  const [deleting, setDeleting] = useState(false);

  useEffect(() => {
    fetchAuctions();
  }, [filterStatus]);

  const fetchAuctions = async () => {
    setLoading(true);
    try {
      const res = await api.get('admin/auctions', {
        params: { status: filterStatus }
      });
      if (res.data && res.data.success) {
        setAuctions(res.data.data || []);
        setStats(res.data.stats || {});
      } else {
        showMessage('error', res.data?.message || 'Lỗi tải danh sách đấu giá');
      }
    } catch (err) {
      console.error('Error fetching auctions:', err);
      showMessage('error', 'Lỗi kết nối máy chủ');
    } finally {
      setLoading(false);
    }
  };

  // Tìm kiếm template khi gõ
  const handleTemplateSearch = async (kw) => {
    setSearchKeyword(kw);
    if (!kw || kw.trim().length === 0) {
      setSearchResults([]);
      return;
    }
    setSearchingTemplates(true);
    try {
      const res = await api.get('admin/auctions/search-templates', {
        params: { keyword: kw.trim() }
      });
      if (res.data && res.data.success) {
        setSearchResults(res.data.data || []);
      }
    } catch (err) {
      console.error('Search templates error:', err);
    } finally {
      setSearchingTemplates(false);
    }
  };

  const handleSelectTemplate = (item) => {
    setFormData((prev) => ({
      ...prev,
      name: item.name,
      category: item.category,
      template_id: item.id,
      color: item.defaultColor || 0,
    }));
    setSearchResults([]);
    setSearchKeyword('');
  };

  const handleCreateSubmit = async (e) => {
    e.preventDefault();
    if (!formData.name.trim() || !formData.template_id) {
      showMessage('error', 'Vui lòng điền tên và ID template vật phẩm!');
      return;
    }

    setSubmitting(true);
    try {
      const res = await api.post('admin/auctions/create', formData);
      if (res.data && res.data.success) {
        showMessage('success', res.data.message);
        setShowCreateModal(false);
        resetForm();
        fetchAuctions();
      } else {
        showMessage('error', res.data?.message || 'Lỗi thêm vật phẩm');
      }
    } catch (err) {
      console.error('Create auction error:', err);
      showMessage('error', 'Lỗi máy chủ khi tạo đấu giá');
    } finally {
      setSubmitting(false);
    }
  };

  const handleDeleteSubmit = async () => {
    if (!deleteTarget) return;
    setDeleting(true);
    try {
      const res = await api.post('admin/auctions/delete', { id: deleteTarget.id });
      if (res.data && res.data.success) {
        showMessage('success', res.data.message);
        setDeleteTarget(null);
        fetchAuctions();
      } else {
        showMessage('error', res.data?.message || 'Lỗi xóa vật phẩm');
      }
    } catch (err) {
      console.error('Delete auction error:', err);
      showMessage('error', 'Lỗi kết nối máy chủ');
    } finally {
      setDeleting(false);
    }
  };

  const resetForm = () => {
    setFormData({
      name: '',
      category: 4,
      template_id: '',
      quantity: 1,
      color: 0,
      start_price: 20,
      step_price: 5,
      buyout_price: 0,
      duration_hours: 2,
      duration_minutes: 0,
    });
    setSearchKeyword('');
    setSearchResults([]);
  };

  const formatRemainingTime = (endTime, status) => {
    if (status === 1) return <span style={{ color: '#52c41a', fontWeight: 'bold' }}>Chờ nhận</span>;
    if (status === 2) return <span style={{ color: '#8c8c8c' }}>Đã nhận</span>;
    if (status === 3) return <span style={{ color: '#ff4d4f' }}>Đã hết hạn</span>;

    const diff = Math.max(0, Math.floor((parseInt(endTime, 10) - Date.now()) / 1000));
    if (diff <= 0) return <span style={{ color: '#faad14' }}>Hết giờ</span>;

    const hours = Math.floor(diff / 3600);
    const minutes = Math.floor((diff % 3600) / 60);
    const seconds = diff % 60;
    return `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
  };

  const getStatusBadge = (status) => {
    switch (status) {
      case 0:
        return <span style={{ padding: '4px 10px', borderRadius: '4px', background: 'rgba(24, 144, 255, 0.2)', color: '#40a9ff', fontSize: '12px' }}>🟢 Đang đấu giá</span>;
      case 1:
        return <span style={{ padding: '4px 10px', borderRadius: '4px', background: 'rgba(82, 196, 26, 0.2)', color: '#52c41a', fontSize: '12px' }}>🎁 Chờ nhận quà</span>;
      case 2:
        return <span style={{ padding: '4px 10px', borderRadius: '4px', background: 'rgba(140, 140, 140, 0.2)', color: '#bfbfbf', fontSize: '12px' }}>✅ Đã nhận</span>;
      case 3:
        return <span style={{ padding: '4px 10px', borderRadius: '4px', background: 'rgba(245, 34, 45, 0.2)', color: '#f5222d', fontSize: '12px' }}>❌ Hết hạn / Hủy</span>;
      default:
        return null;
    }
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
      {/* Header */}
      <div className="glass-panel" style={{ 
        padding: '16px 22px', 
        display: 'flex', 
        justifyContent: 'space-between', 
        alignItems: 'center', 
        flexWrap: 'wrap', 
        gap: '15px',
        textAlign: 'left'
      }}>
        <div style={{ textAlign: 'left' }}>
          <h2 style={{ margin: 0, color: '#ff3366', fontSize: '20px', display: 'flex', alignItems: 'center', gap: '10px', textAlign: 'left' }}>
            <span>⚖️</span> Quản Lý Sàn Đấu Giá Bằng Coin
          </h2>
          <p style={{ margin: '5px 0 0 0', color: '#888', fontSize: '12.5px', textAlign: 'left' }}>
            Thiết lập danh sách vật phẩm đấu giá trong game theo thời gian thực (đồng bộ trực tiếp với Server)
          </p>
        </div>
        <div style={{ display: 'flex', gap: '10px', alignItems: 'center', flexShrink: 0, flexWrap: 'nowrap' }}>
          <button 
            type="button"
            onClick={fetchAuctions} 
            style={{ 
              height: '32px',
              padding: '0 14px',
              fontSize: '12px',
              fontWeight: '500',
              borderRadius: '6px',
              display: 'inline-flex', 
              alignItems: 'center', 
              justifyContent: 'center',
              gap: '6px',
              cursor: 'pointer',
              whiteSpace: 'nowrap',
              textTransform: 'none',
              letterSpacing: 'normal',
              boxSizing: 'border-box',
              border: '1px solid rgba(255, 255, 255, 0.2)',
              color: '#ddd',
              background: 'rgba(255, 255, 255, 0.05)',
              transition: 'all 0.2s ease',
            }}
            onMouseEnter={(e) => { e.currentTarget.style.borderColor = '#00e5ff'; e.currentTarget.style.color = '#00e5ff'; e.currentTarget.style.background = 'rgba(0, 229, 255, 0.08)'; }}
            onMouseLeave={(e) => { e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.2)'; e.currentTarget.style.color = '#ddd'; e.currentTarget.style.background = 'rgba(255, 255, 255, 0.05)'; }}
          >
            <span style={{ fontSize: '12px' }}>🔄</span> Làm mới
          </button>
          <button 
            type="button"
            onClick={() => { resetForm(); setShowCreateModal(true); }}
            style={{ 
              height: '32px',
              padding: '0 16px',
              fontSize: '12px',
              fontWeight: '600',
              borderRadius: '6px',
              display: 'inline-flex', 
              alignItems: 'center', 
              justifyContent: 'center',
              gap: '6px',
              cursor: 'pointer',
              whiteSpace: 'nowrap',
              textTransform: 'none',
              letterSpacing: 'normal',
              boxSizing: 'border-box',
              border: 'none',
              background: 'linear-gradient(135deg, #0088cc 0%, #00b4d8 100%)',
              color: '#fff',
              boxShadow: '0 2px 8px rgba(0, 136, 204, 0.35)',
              transition: 'all 0.2s ease',
            }}
            onMouseEnter={(e) => { e.currentTarget.style.filter = 'brightness(1.1)'; e.currentTarget.style.transform = 'translateY(-1px)'; }}
            onMouseLeave={(e) => { e.currentTarget.style.filter = 'none'; e.currentTarget.style.transform = 'translateY(0)'; }}
          >
            <span style={{ fontSize: '12px' }}>➕</span> Thêm
          </button>
        </div>
      </div>

      {/* Stats Cards */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(180px, 1fr))', gap: '15px' }}>
        <div className="glass-panel" style={{ padding: '15px', borderLeft: '4px solid #1890ff', textAlign: 'left' }}>
          <div style={{ fontSize: '12px', color: '#888' }}>Tổng Vật Phẩm</div>
          <div style={{ fontSize: '24px', fontWeight: 'bold', color: '#fff', marginTop: '5px' }}>{stats.total || 0}</div>
        </div>
        <div className="glass-panel" style={{ padding: '15px', borderLeft: '4px solid #52c41a', textAlign: 'left' }}>
          <div style={{ fontSize: '12px', color: '#888' }}>Đang Đấu Giá</div>
          <div style={{ fontSize: '24px', fontWeight: 'bold', color: '#52c41a', marginTop: '5px' }}>{stats.active_count || 0}</div>
        </div>
        <div className="glass-panel" style={{ padding: '15px', borderLeft: '4px solid #faad14', textAlign: 'left' }}>
          <div style={{ fontSize: '12px', color: '#888' }}>Chờ Nhận Quà</div>
          <div style={{ fontSize: '24px', fontWeight: 'bold', color: '#faad14', marginTop: '5px' }}>{stats.pending_claim_count || 0}</div>
        </div>
        <div className="glass-panel" style={{ padding: '15px', borderLeft: '4px solid #8c8c8c', textAlign: 'left' }}>
          <div style={{ fontSize: '12px', color: '#888' }}>Đã Nhận Thành Công</div>
          <div style={{ fontSize: '24px', fontWeight: 'bold', color: '#bfbfbf', marginTop: '5px' }}>{stats.claimed_count || 0}</div>
        </div>
      </div>

      {/* Filter Tabs */}
      <div className="glass-panel" style={{
        padding: '10px 16px',
        display: 'flex',
        alignItems: 'center',
        gap: '12px',
        overflowX: 'auto',
        whiteSpace: 'nowrap',
        flexWrap: 'nowrap',
        textAlign: 'left',
      }}>
        <span style={{ fontSize: '13px', fontWeight: '600', color: '#aaa', display: 'inline-flex', alignItems: 'center', gap: '6px', flexShrink: 0 }}>
          <span>🔍</span> Lọc trạng thái:
        </span>
        <div style={{ display: 'inline-flex', gap: '8px', flexShrink: 0, alignItems: 'center' }}>
          {[
            { key: '', label: 'Tất cả', count: stats.total },
            { key: '0', label: '🟢 Đang đấu giá', count: stats.active_count },
            { key: '1', label: '🎁 Chờ nhận quà', count: stats.pending_claim_count },
            { key: '2', label: '✅ Đã nhận', count: stats.claimed_count },
            { key: '3', label: '❌ Hết hạn', count: stats.expired_count },
          ].map((f) => {
            const isActive = filterStatus === f.key;
            return (
              <button
                key={f.key}
                type="button"
                style={{
                  height: '30px',
                  lineHeight: '28px',
                  padding: '0 12px',
                  fontSize: '12px',
                  fontWeight: isActive ? '700' : '500',
                  borderRadius: '15px',
                  whiteSpace: 'nowrap',
                  display: 'inline-flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  gap: '6px',
                  cursor: 'pointer',
                  textTransform: 'none',
                  letterSpacing: 'normal',
                  boxSizing: 'border-box',
                  transition: 'all 0.2s cubic-bezier(0.4, 0, 0.2, 1)',
                  background: isActive 
                    ? 'linear-gradient(135deg, #ff3366 0%, #ff5e62 100%)' 
                    : 'rgba(255, 255, 255, 0.04)',
                  border: isActive ? '1px solid #ff4d79' : '1px solid rgba(255, 255, 255, 0.12)',
                  color: isActive ? '#ffffff' : '#aaa',
                  boxShadow: isActive 
                    ? '0 0 16px rgba(255, 51, 102, 0.55), inset 0 1px 0 rgba(255, 255, 255, 0.25)' 
                    : 'none',
                  outline: isActive ? '2px solid rgba(255, 51, 102, 0.6)' : 'none',
                  outlineOffset: '2px',
                  transform: isActive ? 'scale(1.02)' : 'scale(1)',
                }}
                onMouseEnter={(e) => {
                  if (!isActive) {
                    e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.3)';
                    e.currentTarget.style.color = '#fff';
                    e.currentTarget.style.background = 'rgba(255, 255, 255, 0.08)';
                  }
                }}
                onMouseLeave={(e) => {
                  if (!isActive) {
                    e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.12)';
                    e.currentTarget.style.color = '#aaa';
                    e.currentTarget.style.background = 'rgba(255, 255, 255, 0.04)';
                  }
                }}
                onClick={() => setFilterStatus(f.key)}
              >
                <span>{f.label}</span>
                {f.count !== undefined && (
                  <span style={{
                    fontSize: '11px',
                    padding: '0 6px',
                    height: '16px',
                    lineHeight: '16px',
                    borderRadius: '8px',
                    fontWeight: '700',
                    background: isActive ? 'rgba(0, 0, 0, 0.35)' : 'rgba(255, 255, 255, 0.08)',
                    color: isActive ? '#fff' : '#888',
                    display: 'inline-flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                  }}>
                    {f.count || 0}
                  </span>
                )}
              </button>
            );
          })}
        </div>
      </div>

      {/* Main Table */}
      <div className="glass-panel" style={{ padding: '0', overflowX: 'auto' }}>
        <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left', fontSize: '13px' }}>
          <thead>
            <tr style={{ borderBottom: '1px solid rgba(255,255,255,0.1)', background: 'rgba(255,255,255,0.02)' }}>
              <th style={{ padding: '12px 16px', color: '#888' }}>Slot</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Vật phẩm</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Loại & ID</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Giá Khởi Điểm</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Giá Hiện Tại / Bước</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Giá Chốt (Mua đứt)</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Người Giữ Giá</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Thời Gian Còn</th>
              <th style={{ padding: '12px 16px', color: '#888' }}>Trạng Thái</th>
              <th style={{ padding: '12px 16px', color: '#888', textAlign: 'center' }}>Thao Tác</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr>
                <td colSpan="10" style={{ textAlign: 'center', padding: '40px', color: '#888' }}>
                  Đang tải danh sách đấu giá...
                </td>
              </tr>
            ) : auctions.length === 0 ? (
              <tr>
                <td colSpan="10" style={{ textAlign: 'center', padding: '40px', color: '#888' }}>
                  Chưa có vật phẩm đấu giá nào. Bấm <strong>"➕ Thêm"</strong> để mở phiên đấu giá mới!
                </td>
              </tr>
            ) : (
              auctions.map((item) => (
                <tr key={item.id} style={{ borderBottom: '1px solid rgba(255,255,255,0.05)' }}>
                  <td style={{ padding: '12px 16px', fontWeight: 'bold', color: '#fa8c16' }}>#{item.slot_id}</td>
                  <td style={{ padding: '12px 16px' }}>
                    <div style={{ fontWeight: 'bold', color: COLOR_MAP[item.color]?.color || '#fff' }}>
                      {item.name}
                    </div>
                    <div style={{ fontSize: '11px', color: '#888' }}>
                      SL: <strong>x{item.quantity}</strong> | Phẩm chất: {COLOR_MAP[item.color]?.label || 'Thường'}
                    </div>
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    <div style={{ color: CATEGORY_MAP[item.category]?.color || '#fff', fontSize: '12px' }}>
                      {CATEGORY_MAP[item.category]?.label || `Cat ${item.category}`}
                    </div>
                    <div style={{ fontSize: '11px', color: '#888' }}>ID: {item.template_id}</div>
                  </td>
                  <td style={{ padding: '12px 16px', color: '#ffd591' }}>
                    <strong>{item.start_price.toLocaleString()}</strong> Coin
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    <div style={{ color: '#52c41a', fontWeight: 'bold' }}>{item.current_price.toLocaleString()} Coin</div>
                    <div style={{ fontSize: '11px', color: '#888' }}>+{(item.step_price || 0).toLocaleString()} Coin/lượt</div>
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    {item.buyout_price > 0 ? (
                      <span style={{ color: '#ff4d4f', fontWeight: 'bold' }}>{item.buyout_price.toLocaleString()} Coin</span>
                    ) : (
                      <span style={{ color: '#888' }}>Không có (Đua giá)</span>
                    )}
                  </td>
                  <td style={{ padding: '12px 16px' }}>
                    {item.highest_bidder_id !== -1 ? (
                      <div>
                        <span style={{ color: '#40a9ff', fontWeight: 'bold' }}>{item.highest_bidder_name}</span>
                        {item.highest_bidder_user && <div style={{ fontSize: '11px', color: '#888' }}>({item.highest_bidder_user})</div>}
                      </div>
                    ) : (
                      <span style={{ color: '#888' }}>Chưa có ai</span>
                    )}
                  </td>
                  <td style={{ padding: '12px 16px', fontFamily: 'monospace' }}>
                    {formatRemainingTime(item.end_time, item.status)}
                  </td>
                  <td style={{ padding: '12px 16px' }}>{getStatusBadge(item.status)}</td>
                  <td style={{ padding: '10px 16px', textAlign: 'center' }}>
                    <button
                      className="btn btn-outline"
                      style={{
                        height: '26px',
                        lineHeight: '24px',
                        padding: '0 10px',
                        color: '#ff4d4f',
                        borderColor: 'rgba(255, 77, 79, 0.35)',
                        background: 'rgba(255, 77, 79, 0.06)',
                        fontSize: '11.5px',
                        fontWeight: '500',
                        borderRadius: '4px',
                        textTransform: 'none',
                        letterSpacing: 'normal',
                        display: 'inline-flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        gap: '4px',
                        cursor: 'pointer',
                        transition: 'all 0.2s ease',
                      }}
                      onMouseEnter={(e) => { e.currentTarget.style.background = 'rgba(255, 77, 79, 0.15)'; e.currentTarget.style.borderColor = '#ff4d4f'; }}
                      onMouseLeave={(e) => { e.currentTarget.style.background = 'rgba(255, 77, 79, 0.06)'; e.currentTarget.style.borderColor = 'rgba(255, 77, 79, 0.35)'; }}
                      onClick={() => setDeleteTarget(item)}
                      title="Xóa vật phẩm đấu giá"
                    >
                      <span style={{ fontSize: '11px' }}>🗑️</span> Xóa
                    </button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* Modal Thêm Vật Phẩm Mới */}
      {showCreateModal && (
        <div style={{
          position: 'fixed', top: 0, left: 0, right: 0, bottom: 0,
          background: 'rgba(0,0,0,0.75)', zIndex: 1000,
          display: 'flex', alignItems: 'center', justifyContent: 'center', padding: '20px'
        }}>
          <div className="glass-panel" style={{ width: '100%', maxWidth: '600px', maxHeight: '90vh', overflowY: 'auto', padding: '25px', background: '#1a1a1a', border: '1px solid #333', textAlign: 'left' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
              <h3 style={{ margin: 0, color: '#52c41a', fontSize: '18px' }}>➕ Thêm Vật Phẩm Vào Sàn Đấu Giá</h3>
              <button 
                onClick={() => setShowCreateModal(false)}
                style={{
                  background: 'transparent',
                  border: 'none',
                  color: '#888',
                  fontSize: '18px',
                  cursor: 'pointer',
                  width: '28px',
                  height: '28px',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  borderRadius: '4px',
                  transition: 'color 0.2s',
                }}
                onMouseEnter={(e) => e.currentTarget.style.color = '#fff'}
                onMouseLeave={(e) => e.currentTarget.style.color = '#888'}
              >
                ✕
              </button>
            </div>

            {/* Quick Search Template */}
            <div style={{ marginBottom: '20px', background: 'rgba(255,255,255,0.03)', padding: '12px', borderRadius: '6px', textAlign: 'left' }}>
              <label style={{ display: 'block', fontSize: '12px', color: '#fa8c16', marginBottom: '6px', fontWeight: 'bold' }}>
                🔍 Gợi ý: Tìm nhanh vật phẩm từ cơ sở dữ liệu game
              </label>
              <input
                type="text"
                placeholder="Gõ tên vật phẩm (VD: Rương, Búa, Đá, Kiếm...)"
                value={searchKeyword}
                onChange={(e) => handleTemplateSearch(e.target.value)}
                style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
              />
              {searchingTemplates && <div style={{ fontSize: '11px', color: '#888', marginTop: '4px' }}>Đang tìm...</div>}
              {searchResults.length > 0 && (
                <div style={{ marginTop: '8px', maxHeight: '160px', overflowY: 'auto', background: '#222', border: '1px solid #444', borderRadius: '4px' }}>
                  {searchResults.map((it) => (
                    <div
                      key={`${it.category}-${it.id}`}
                      onClick={() => handleSelectTemplate(it)}
                      style={{
                        padding: '8px 12px', cursor: 'pointer', borderBottom: '1px solid #333',
                        display: 'flex', justifyContent: 'space-between', alignItems: 'center', fontSize: '12px'
                      }}
                      onMouseEnter={(e) => e.currentTarget.style.background = '#333'}
                      onMouseLeave={(e) => e.currentTarget.style.background = 'transparent'}
                    >
                      <span><strong>{it.name}</strong> <span style={{ color: '#888' }}>(ID: {it.id})</span></span>
                      <span style={{ color: CATEGORY_MAP[it.category]?.color || '#fff' }}>
                        {CATEGORY_MAP[it.category]?.label}
                      </span>
                    </div>
                  ))}
                </div>
              )}
            </div>

            <form onSubmit={handleCreateSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '15px', textAlign: 'left' }}>
              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>Tên vật phẩm (*)</label>
                <input
                  type="text"
                  required
                  placeholder="Nhập tên vật phẩm"
                  value={formData.name}
                  onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                  style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
                />
              </div>

              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '15px' }}>
                <div>
                  <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>Loại vật phẩm (Category)</label>
                  <select
                    value={formData.category}
                    onChange={(e) => setFormData({ ...formData, category: parseInt(e.target.value, 10) })}
                    style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
                  >
                    <option value={4}>🧪 Item Thường / Rương (item4)</option>
                    <option value={3}>⚔️ Trang Bị / Vũ Khí (item3)</option>
                    <option value={7}>💎 Đá Khảm / Nguyên Liệu (item7)</option>
                  </select>
                </div>
                <div>
                  <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>ID Template (*)</label>
                  <input
                    type="number"
                    required
                    placeholder="VD: 1004"
                    value={formData.template_id}
                    onChange={(e) => setFormData({ ...formData, template_id: e.target.value })}
                    style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
                  />
                </div>
              </div>

              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '15px' }}>
                <div>
                  <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>Số lượng</label>
                  <input
                    type="number"
                    min="1"
                    value={formData.quantity}
                    onChange={(e) => setFormData({ ...formData, quantity: parseInt(e.target.value, 10) || 1 })}
                    style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
                  />
                </div>
                <div>
                  <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>Phẩm chất màu</label>
                  <select
                    value={formData.color}
                    onChange={(e) => setFormData({ ...formData, color: parseInt(e.target.value, 10) })}
                    style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
                  >
                    <option value={0}>⚪ Trắng</option>
                    <option value={1}>🟢 Xanh lá</option>
                    <option value={2}>🔵 Xanh lam</option>
                    <option value={3}>🟠 Cam</option>
                    <option value={4}>🟡 Vàng kim</option>
                  </select>
                </div>
              </div>

              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '15px' }}>
                <div>
                  <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>Giá Khởi Điểm (Coin)</label>
                  <input
                    type="number"
                    min="1"
                    value={formData.start_price}
                    onChange={(e) => setFormData({ ...formData, start_price: parseInt(e.target.value, 10) || 1 })}
                    style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#ffd591', borderRadius: '4px', fontWeight: 'bold' }}
                  />
                </div>
                <div>
                  <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>Bước Giá Tăng (Coin/lần bid)</label>
                  <input
                    type="number"
                    min="1"
                    value={formData.step_price}
                    onChange={(e) => setFormData({ ...formData, step_price: parseInt(e.target.value, 10) || 1 })}
                    style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#52c41a', borderRadius: '4px', fontWeight: 'bold' }}
                  />
                </div>
              </div>

              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>
                  Giá Chốt (Mua đứt ngay)
                  <span style={{ color: '#888', marginLeft: '6px' }}>— Đặt bằng 0 nếu muốn người chơi bắt buộc phải đấu giá</span>
                </label>
                <input
                  type="number"
                  min="0"
                  placeholder="0 = Không cho mua đứt"
                  value={formData.buyout_price}
                  onChange={(e) => setFormData({ ...formData, buyout_price: parseInt(e.target.value, 10) || 0 })}
                  style={{ width: '100%', padding: '8px 12px', background: '#222', border: '1px solid #444', color: '#ff4d4f', borderRadius: '4px' }}
                />
              </div>

              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#aaa', marginBottom: '4px' }}>Thời lượng đấu giá</label>
                <div style={{ display: 'grid', gridTemplateColumns: 'repeat(6, 1fr)', gap: '6px', marginBottom: '10px' }}>
                  {[
                    { label: '30 phút', h: 0, m: 30 },
                    { label: '1 giờ', h: 1, m: 0 },
                    { label: '2 giờ', h: 2, m: 0 },
                    { label: '6 giờ', h: 6, m: 0 },
                    { label: '12 giờ', h: 12, m: 0 },
                    { label: '24 giờ', h: 24, m: 0 },
                  ].map((preset) => {
                    const isSelected = formData.duration_hours === preset.h && formData.duration_minutes === preset.m;
                    return (
                      <button
                        key={preset.label}
                        type="button"
                        style={{
                          height: '28px',
                          lineHeight: '26px',
                          padding: '0 4px',
                          fontSize: '11px',
                          fontWeight: isSelected ? '700' : '500',
                          textAlign: 'center',
                          borderRadius: '4px',
                          textTransform: 'none',
                          letterSpacing: 'normal',
                          boxSizing: 'border-box',
                          border: isSelected ? '1px solid #52c41a' : '1px solid rgba(255, 255, 255, 0.15)',
                          color: isSelected ? '#52c41a' : '#ccc',
                          background: isSelected ? 'rgba(82, 196, 26, 0.15)' : 'rgba(255, 255, 255, 0.03)',
                          cursor: 'pointer',
                          display: 'flex',
                          alignItems: 'center',
                          justifyContent: 'center',
                          transition: 'all 0.2s ease',
                        }}
                        onClick={() => setFormData({ ...formData, duration_hours: preset.h, duration_minutes: preset.m })}
                      >
                        {preset.label}
                      </button>
                    );
                  })}
                </div>
                <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
                  <input
                    type="number"
                    min="0"
                    value={formData.duration_hours}
                    onChange={(e) => setFormData({ ...formData, duration_hours: parseInt(e.target.value, 10) || 0 })}
                    style={{ width: '80px', padding: '6px 10px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
                  />
                  <span style={{ fontSize: '12px', color: '#888' }}>giờ</span>
                  <input
                    type="number"
                    min="0"
                    max="59"
                    value={formData.duration_minutes}
                    onChange={(e) => setFormData({ ...formData, duration_minutes: parseInt(e.target.value, 10) || 0 })}
                    style={{ width: '80px', padding: '6px 10px', background: '#222', border: '1px solid #444', color: '#fff', borderRadius: '4px' }}
                  />
                  <span style={{ fontSize: '12px', color: '#888' }}>phút</span>
                </div>
              </div>

              <div style={{ display: 'flex', justifyContent: 'flex-end', gap: '10px', marginTop: '12px' }}>
                <button
                  type="button"
                  onClick={() => setShowCreateModal(false)}
                  style={{
                    height: '34px',
                    padding: '0 16px',
                    fontSize: '12.5px',
                    fontWeight: '500',
                    borderRadius: '6px',
                    textTransform: 'none',
                    letterSpacing: 'normal',
                    boxSizing: 'border-box',
                    border: '1px solid rgba(255, 255, 255, 0.2)',
                    background: 'rgba(255, 255, 255, 0.05)',
                    color: '#ccc',
                    cursor: 'pointer',
                    display: 'inline-flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                  }}
                >
                  Hủy
                </button>
                <button
                  type="submit"
                  disabled={submitting}
                  style={{
                    height: '34px',
                    padding: '0 20px',
                    fontSize: '12.5px',
                    fontWeight: '600',
                    borderRadius: '6px',
                    textTransform: 'none',
                    letterSpacing: 'normal',
                    boxSizing: 'border-box',
                    border: 'none',
                    background: 'linear-gradient(135deg, #0088cc 0%, #00b4d8 100%)',
                    color: '#fff',
                    boxShadow: '0 2px 8px rgba(0, 136, 204, 0.35)',
                    cursor: 'pointer',
                    display: 'inline-flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                  }}
                >
                  {submitting ? 'Đang tạo...' : 'Xác Nhận Đưa Lên Đấu Giá'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Modal Xác Nhận Xóa */}
      {deleteTarget && (
        <div style={{
          position: 'fixed', top: 0, left: 0, right: 0, bottom: 0,
          background: 'rgba(0,0,0,0.75)', zIndex: 1000,
          display: 'flex', alignItems: 'center', justifyContent: 'center', padding: '20px'
        }}>
          <div className="glass-panel" style={{ width: '100%', maxWidth: '420px', padding: '25px', background: '#1a1a1a', border: '1px solid #444', textAlign: 'left' }}>
            <h3 style={{ margin: '0 0 15px 0', color: '#ff4d4f', fontSize: '18px' }}>🗑️ Xác Nhận Xóa Vật Phẩm</h3>
            <p style={{ color: '#ccc', fontSize: '13px', lineHeight: '1.6' }}>
              Bạn có chắc chắn muốn xóa vật phẩm <strong>{deleteTarget.name}</strong> (Slot #{deleteTarget.slot_id}) khỏi sàn đấu giá không?
            </p>
            {deleteTarget.highest_bidder_id !== -1 && (
              <div style={{ background: 'rgba(250, 173, 20, 0.1)', border: '1px solid rgba(250, 173, 20, 0.3)', padding: '10px', borderRadius: '4px', fontSize: '12px', color: '#faad14', marginBottom: '15px' }}>
                ⚠️ Người chơi <strong>{deleteTarget.highest_bidder_name}</strong> đang cược <strong>{deleteTarget.current_price.toLocaleString()} Coin</strong>. Khi xóa, hệ thống sẽ tự động hoàn lại số Coin này cho họ!
              </div>
            )}
            <div style={{ display: 'flex', justifyContent: 'flex-end', gap: '10px', marginTop: '20px' }}>
              <button
                type="button"
                onClick={() => setDeleteTarget(null)}
                style={{
                  height: '34px',
                  padding: '0 16px',
                  fontSize: '12.5px',
                  fontWeight: '500',
                  borderRadius: '6px',
                  textTransform: 'none',
                  letterSpacing: 'normal',
                  boxSizing: 'border-box',
                  border: '1px solid rgba(255, 255, 255, 0.2)',
                  background: 'rgba(255, 255, 255, 0.05)',
                  color: '#ccc',
                  cursor: 'pointer',
                  display: 'inline-flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                }}
              >
                Hủy
              </button>
              <button
                type="button"
                disabled={deleting}
                onClick={handleDeleteSubmit}
                style={{
                  height: '34px',
                  padding: '0 20px',
                  fontSize: '12.5px',
                  fontWeight: '600',
                  borderRadius: '6px',
                  textTransform: 'none',
                  letterSpacing: 'normal',
                  boxSizing: 'border-box',
                  border: 'none',
                  background: '#ff4d4f',
                  color: '#fff',
                  boxShadow: '0 2px 8px rgba(255, 77, 79, 0.35)',
                  cursor: 'pointer',
                  display: 'inline-flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                }}
              >
                {deleting ? 'Đang xóa...' : 'Xác Nhận Xóa'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
