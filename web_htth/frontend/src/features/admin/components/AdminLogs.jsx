import React, { useState, useEffect, useCallback } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const TYPE_CONFIG = {
  ruby: { label: 'Ruby', icon: '💎', color: '#ff3366', bg: 'rgba(255, 51, 102, 0.15)', border: 'rgba(255, 51, 102, 0.35)' },
  beri: { label: 'Beri', icon: '🪙', color: '#faad14', bg: 'rgba(250, 173, 20, 0.15)', border: 'rgba(250, 173, 20, 0.35)' },
  extol: { label: 'Extol', icon: '💵', color: '#00e5ff', bg: 'rgba(0, 229, 255, 0.15)', border: 'rgba(0, 229, 255, 0.35)' },
  shop: { label: 'Shop', icon: '🛒', color: '#52c41a', bg: 'rgba(82, 196, 26, 0.15)', border: 'rgba(82, 196, 26, 0.35)' },
  item: { label: 'Vật phẩm', icon: '🎁', color: '#13c2c2', bg: 'rgba(19, 194, 194, 0.15)', border: 'rgba(19, 194, 194, 0.35)' },
  market: { label: 'Chợ trời', icon: '🏪', color: '#fa8c16', bg: 'rgba(250, 140, 22, 0.15)', border: 'rgba(250, 140, 22, 0.35)' },
  trade: { label: 'Giao dịch', icon: '🤝', color: '#722ed1', bg: 'rgba(114, 46, 209, 0.15)', border: 'rgba(114, 46, 209, 0.35)' },
  drop_pick: { label: 'Nhặt/Rơi', icon: '🎒', color: '#8c8c8c', bg: 'rgba(140, 140, 140, 0.15)', border: 'rgba(140, 140, 140, 0.35)' },
  buff: { label: 'Buff', icon: '⚡', color: '#b37feb', bg: 'rgba(179, 127, 235, 0.15)', border: 'rgba(179, 127, 235, 0.35)' },
  coin: { label: 'Coin', icon: '💰', color: '#faad14', bg: 'rgba(250, 173, 20, 0.15)', border: 'rgba(250, 173, 20, 0.35)' },
  recharge: { label: 'Nạp tiền', icon: '💳', color: '#1890ff', bg: 'rgba(24, 144, 255, 0.15)', border: 'rgba(24, 144, 255, 0.35)' },
};

export default function AdminLogs() {
  const { showMessage } = useOutletContext();
  const [logs, setLogs] = useState([]);
  const [loading, setLoading] = useState(true);
  
  const [filters, setFilters] = useState({
    startDate: '',
    endDate: '',
    playerName: '',
    type: ''
  });
  
  const [page, setPage] = useState(1);
  const [limit, setLimit] = useState(20);
  const [totalPages, setTotalPages] = useState(1);
  const [total, setTotal] = useState(0);

  const fetchLogs = useCallback(async (currentPage = page, currentLimit = limit, currentFilters = filters) => {
    try {
      setLoading(true);
      const queryParams = new URLSearchParams({
        page: currentPage,
        limit: currentLimit,
        ...(currentFilters.startDate ? { startDate: currentFilters.startDate } : {}),
        ...(currentFilters.endDate ? { endDate: currentFilters.endDate } : {}),
        ...(currentFilters.playerName?.trim() ? { playerName: currentFilters.playerName.trim() } : {}),
        ...(currentFilters.type ? { type: currentFilters.type } : {})
      });
      
      const response = await api.get(`admin/player-logs?${queryParams.toString()}`);
      
      if (response.data && response.data.success) {
        setLogs(response.data.data || []);
        setTotalPages(response.data.totalPages || 1);
        setTotal(response.data.total || 0);
      } else {
        showMessage('error', response.data?.message || 'Không thể tải danh sách lịch sử người chơi.');
      }
    } catch (error) {
      console.error('Fetch logs error:', error);
      showMessage('error', 'Lỗi kết nối đến máy chủ khi tải lịch sử.');
    } finally {
      setLoading(false);
    }
  }, [page, limit, filters, showMessage]);

  useEffect(() => {
    fetchLogs(page, limit, filters);
  }, [page, limit]);

  const handleSearch = (e) => {
    if (e) e.preventDefault();
    setPage(1);
    fetchLogs(1, limit, filters);
  };

  const handleResetFilters = () => {
    const defaultFilters = {
      startDate: '',
      endDate: '',
      playerName: '',
      type: ''
    };
    setFilters(defaultFilters);
    setPage(1);
    fetchLogs(1, limit, defaultFilters);
  };

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({ ...prev, [name]: value }));
  };

  const handlePageChange = (newPage) => {
    if (newPage < 1 || newPage > totalPages || newPage === page) return;
    setPage(newPage);
  };

  const getTypeBadge = (type) => {
    const key = (type || '').toLowerCase();
    const config = TYPE_CONFIG[key] || {
      label: type || 'Khác',
      icon: '📌',
      color: '#00e5ff',
      bg: 'rgba(0, 229, 255, 0.12)',
      border: 'rgba(0, 229, 255, 0.3)'
    };

    return (
      <span style={{
        display: 'inline-flex',
        alignItems: 'center',
        gap: '4px',
        background: config.bg,
        color: config.color,
        border: `1px solid ${config.border}`,
        padding: '3px 10px',
        borderRadius: '6px',
        fontSize: '12px',
        fontWeight: 'bold',
        letterSpacing: '0.3px',
        textTransform: 'uppercase'
      }}>
        <span>{config.icon}</span>
        <span>{config.label}</span>
      </span>
    );
  };

  const inputStyle = {
    width: '100%',
    padding: '10px 14px',
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    border: '1px solid rgba(255, 255, 255, 0.12)',
    borderRadius: '8px',
    color: '#fff',
    fontSize: '13.5px',
    outline: 'none',
    boxSizing: 'border-box',
    transition: 'all 0.2s ease',
  };

  const labelStyle = {
    display: 'block',
    marginBottom: '6px',
    color: '#aaa',
    fontSize: '12.5px',
    fontWeight: '600',
    letterSpacing: '0.3px'
  };

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      gap: '20px',
      maxWidth: '1400px',
      margin: '0 auto',
      fontFamily: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif',
      color: '#eee',
      width: '100%'
    }}>
      {/* Header Panel */}
      <div className="glass-panel" style={{
        padding: '24px',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        flexWrap: 'wrap',
        gap: '16px'
      }}>
        <div>
          <h2 style={{
            margin: 0,
            fontSize: '24px',
            fontWeight: '800',
            textTransform: 'uppercase',
            letterSpacing: '1px',
            background: 'linear-gradient(135deg, #ff3366 0%, #ff5e62 100%)',
            WebkitBackgroundClip: 'text',
            WebkitTextFillColor: 'transparent',
            display: 'flex',
            alignItems: 'center',
            gap: '8px'
          }}>
            📜 LỊCH SỬ NGƯỜI CHƠI
          </h2>
          <p style={{ color: '#aaa', margin: '6px 0 0 0', fontSize: '13.5px' }}>
            Tra cứu và giám sát chi tiết hành động người chơi: tiêu Ruby, nhận/mất vật phẩm, thao tác buff...
          </p>
        </div>

        <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
          <span style={{
            background: 'rgba(0, 229, 255, 0.1)',
            border: '1px solid rgba(0, 229, 255, 0.3)',
            color: '#00e5ff',
            padding: '6px 14px',
            borderRadius: '20px',
            fontSize: '13px',
            fontWeight: 'bold'
          }}>
            Tổng bản ghi: {total.toLocaleString()}
          </span>
          <button 
            type="button"
            onClick={() => fetchLogs(page, limit, filters)} 
            className="btn btn-outline" 
            style={{ 
              borderColor: 'rgba(255,255,255,0.15)', 
              color: '#ccc', 
              padding: '8px 16px', 
              fontSize: '13px', 
              background: 'rgba(255,255,255,0.03)', 
              cursor: 'pointer', 
              borderRadius: '8px',
              display: 'flex',
              alignItems: 'center',
              gap: '6px',
              margin: 0,
              width: 'auto'
            }}
            title="Tải lại danh sách"
          >
            🔄 Làm mới
          </button>
        </div>
      </div>

      {/* Filter & Search Panel */}
      <div className="glass-panel" style={{ padding: '22px' }}>
        <form onSubmit={handleSearch}>
          <div style={{
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(180px, 1fr))',
            gap: '16px',
            alignItems: 'flex-end'
          }}>
            {/* Player Name */}
            <div>
              <label style={labelStyle}>👤 Tên nhân vật</label>
              <input 
                type="text" 
                name="playerName"
                value={filters.playerName}
                onChange={handleFilterChange}
                placeholder="Nhập tên nhân vật..."
                style={inputStyle}
                onFocus={(e) => {
                  e.target.style.borderColor = '#00e5ff';
                  e.target.style.boxShadow = '0 0 10px rgba(0, 229, 255, 0.2)';
                }}
                onBlur={(e) => {
                  e.target.style.borderColor = 'rgba(255, 255, 255, 0.12)';
                  e.target.style.boxShadow = 'none';
                }}
              />
            </div>

            {/* Start Date */}
            <div>
              <label style={labelStyle}>📅 Từ ngày</label>
              <input 
                type="date" 
                name="startDate"
                value={filters.startDate}
                onChange={handleFilterChange}
                style={inputStyle}
                onFocus={(e) => {
                  e.target.style.borderColor = '#00e5ff';
                  e.target.style.boxShadow = '0 0 10px rgba(0, 229, 255, 0.2)';
                }}
                onBlur={(e) => {
                  e.target.style.borderColor = 'rgba(255, 255, 255, 0.12)';
                  e.target.style.boxShadow = 'none';
                }}
              />
            </div>

            {/* End Date */}
            <div>
              <label style={labelStyle}>📅 Đến ngày</label>
              <input 
                type="date" 
                name="endDate"
                value={filters.endDate}
                onChange={handleFilterChange}
                style={inputStyle}
                onFocus={(e) => {
                  e.target.style.borderColor = '#00e5ff';
                  e.target.style.boxShadow = '0 0 10px rgba(0, 229, 255, 0.2)';
                }}
                onBlur={(e) => {
                  e.target.style.borderColor = 'rgba(255, 255, 255, 0.12)';
                  e.target.style.boxShadow = 'none';
                }}
              />
            </div>

            {/* Type */}
            <div>
              <label style={labelStyle}>🏷️ Loại hành động</label>
              <select 
                name="type" 
                value={filters.type}
                onChange={handleFilterChange}
                style={{ ...inputStyle, cursor: 'pointer' }}
                onFocus={(e) => {
                  e.target.style.borderColor = '#00e5ff';
                  e.target.style.boxShadow = '0 0 10px rgba(0, 229, 255, 0.2)';
                }}
                onBlur={(e) => {
                  e.target.style.borderColor = 'rgba(255, 255, 255, 0.12)';
                  e.target.style.boxShadow = 'none';
                }}
              >
                <option value="" style={{ background: '#1a1a1a', color: '#fff' }}>Tất cả các loại</option>
                <option value="ruby" style={{ background: '#1a1a1a', color: '#fff' }}>💎 Ruby (Tiêu Ruby)</option>
                <option value="beri" style={{ background: '#1a1a1a', color: '#fff' }}>🪙 Beri (Tiêu Beri)</option>
                <option value="extol" style={{ background: '#1a1a1a', color: '#fff' }}>💵 Extol (Tiêu Extol)</option>
                <option value="shop" style={{ background: '#1a1a1a', color: '#fff' }}>🛒 Cửa hàng / Shop</option>
                <option value="item" style={{ background: '#1a1a1a', color: '#fff' }}>🎁 Vật phẩm</option>
                <option value="market" style={{ background: '#1a1a1a', color: '#fff' }}>🏪 Chợ trời</option>
                <option value="trade" style={{ background: '#1a1a1a', color: '#fff' }}>🤝 Giao dịch</option>
                <option value="buff" style={{ background: '#1a1a1a', color: '#fff' }}>⚡ Buff chỉ số / Admin</option>
                <option value="coin" style={{ background: '#1a1a1a', color: '#fff' }}>💰 Coin / Tiền tệ</option>
              </select>
            </div>

            {/* Limit Per Page */}
            <div>
              <label style={labelStyle}>📄 Hiển thị / trang</label>
              <select 
                value={limit}
                onChange={(e) => {
                  const newLimit = Number(e.target.value);
                  setLimit(newLimit);
                  setPage(1);
                }}
                style={{ ...inputStyle, cursor: 'pointer' }}
                onFocus={(e) => {
                  e.target.style.borderColor = '#00e5ff';
                  e.target.style.boxShadow = '0 0 10px rgba(0, 229, 255, 0.2)';
                }}
                onBlur={(e) => {
                  e.target.style.borderColor = 'rgba(255, 255, 255, 0.12)';
                  e.target.style.boxShadow = 'none';
                }}
              >
                <option value={15} style={{ background: '#1a1a1a', color: '#fff' }}>15 dòng</option>
                <option value={20} style={{ background: '#1a1a1a', color: '#fff' }}>20 dòng</option>
                <option value={50} style={{ background: '#1a1a1a', color: '#fff' }}>50 dòng</option>
                <option value={100} style={{ background: '#1a1a1a', color: '#fff' }}>100 dòng</option>
              </select>
            </div>

            {/* Action Buttons */}
            <div style={{ display: 'flex', gap: '8px' }}>
              <button 
                type="submit" 
                style={{
                  flex: 1,
                  padding: '10px 16px',
                  background: 'linear-gradient(135deg, #00e5ff 0%, #00a8cc 100%)',
                  color: '#000',
                  border: 'none',
                  borderRadius: '8px',
                  fontWeight: 'bold',
                  fontSize: '13.5px',
                  cursor: 'pointer',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  gap: '6px',
                  transition: 'all 0.2s ease',
                  boxShadow: '0 4px 14px rgba(0, 229, 255, 0.25)',
                  whiteSpace: 'nowrap'
                }}
                onMouseEnter={(e) => {
                  e.currentTarget.style.transform = 'translateY(-2px)';
                  e.currentTarget.style.boxShadow = '0 6px 18px rgba(0, 229, 255, 0.4)';
                }}
                onMouseLeave={(e) => {
                  e.currentTarget.style.transform = 'translateY(0)';
                  e.currentTarget.style.boxShadow = '0 4px 14px rgba(0, 229, 255, 0.25)';
                }}
              >
                🔍 Tìm kiếm
              </button>

              <button 
                type="button"
                onClick={handleResetFilters}
                style={{
                  padding: '10px 14px',
                  background: 'rgba(255, 255, 255, 0.05)',
                  color: '#aaa',
                  border: '1px solid rgba(255, 255, 255, 0.1)',
                  borderRadius: '8px',
                  fontWeight: '600',
                  fontSize: '13px',
                  cursor: 'pointer',
                  transition: 'all 0.2s ease',
                  whiteSpace: 'nowrap'
                }}
                onMouseEnter={(e) => {
                  e.currentTarget.style.background = 'rgba(255, 255, 255, 0.1)';
                  e.currentTarget.style.color = '#fff';
                }}
                onMouseLeave={(e) => {
                  e.currentTarget.style.background = 'rgba(255, 255, 255, 0.05)';
                  e.currentTarget.style.color = '#aaa';
                }}
                title="Xóa bộ lọc về mặc định"
              >
                🧹 Đặt lại
              </button>
            </div>
          </div>
        </form>
      </div>

      {/* Table Panel */}
      <div className="glass-panel" style={{ padding: '22px' }}>
        <div style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          marginBottom: '16px',
          borderBottom: '1px solid rgba(255, 255, 255, 0.08)',
          paddingBottom: '12px'
        }}>
          <h3 style={{ margin: 0, fontSize: '16px', color: '#fff', fontWeight: '700', display: 'flex', alignItems: 'center', gap: '8px' }}>
            📋 Danh sách Nhật Ký Hoạt Động
          </h3>
          <span style={{ fontSize: '13px', color: '#888' }}>
            Hiển thị {logs.length} / {total.toLocaleString()} kết quả
          </span>
        </div>

        <div style={{ overflowX: 'auto' }}>
          <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '13.5px', color: '#eee', textAlign: 'left' }}>
            <thead>
              <tr style={{ borderBottom: '2px solid rgba(255, 255, 255, 0.12)', background: 'rgba(0, 0, 0, 0.3)' }}>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12px', textTransform: 'uppercase', fontWeight: '700', width: '80px', textAlign: 'center' }}>ID</th>
                <th style={{ padding: '12px 12px', color: '#999', fontSize: '12px', textTransform: 'uppercase', fontWeight: '700', width: '180px' }}>Thời Gian</th>
                <th style={{ padding: '12px 12px', color: '#999', fontSize: '12px', textTransform: 'uppercase', fontWeight: '700', width: '180px' }}>Nhân Vật</th>
                <th style={{ padding: '12px 12px', color: '#999', fontSize: '12px', textTransform: 'uppercase', fontWeight: '700', width: '140px', textAlign: 'center' }}>Phân Loại</th>
                <th style={{ padding: '12px 12px', color: '#999', fontSize: '12px', textTransform: 'uppercase', fontWeight: '700' }}>Chi Tiết Hành Động</th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr>
                  <td colSpan="5" style={{ padding: '40px 20px', textAlign: 'center', color: '#aaa' }}>
                    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '10px' }}>
                      <span style={{ fontSize: '24px' }}>🔄</span>
                      <span>Đang tải dữ liệu nhật ký từ máy chủ...</span>
                    </div>
                  </td>
                </tr>
              ) : logs.length === 0 ? (
                <tr>
                  <td colSpan="5" style={{ padding: '40px 20px', textAlign: 'center', color: '#888' }}>
                    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '10px' }}>
                      <span style={{ fontSize: '32px' }}>📭</span>
                      <span style={{ fontSize: '15px', color: '#bbb', fontWeight: '600' }}>Không tìm thấy bản ghi lịch sử nào.</span>
                      <span style={{ fontSize: '13px', color: '#777' }}>Hãy thử điều chỉnh bộ lọc tìm kiếm hoặc làm mới dữ liệu.</span>
                    </div>
                  </td>
                </tr>
              ) : (
                logs.map((log) => (
                  <tr 
                    key={log.id} 
                    style={{ 
                      borderBottom: '1px solid rgba(255, 255, 255, 0.05)',
                      transition: 'background-color 0.15s ease'
                    }}
                    onMouseEnter={(e) => e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.03)'}
                    onMouseLeave={(e) => e.currentTarget.style.backgroundColor = 'transparent'}
                  >
                    <td style={{ padding: '13px 10px', color: '#777', fontSize: '12.5px', textAlign: 'center', fontFamily: 'monospace' }}>
                      #{log.id}
                    </td>
                    <td style={{ padding: '13px 12px', color: '#aaa', fontSize: '13px', whiteSpace: 'nowrap' }}>
                      {log.created_at ? (
                        new Date(log.created_at).toLocaleString('vi-VN', {
                          day: '2-digit',
                          month: '2-digit',
                          year: 'numeric',
                          hour: '2-digit',
                          minute: '2-digit',
                          second: '2-digit'
                        })
                      ) : (
                        '—'
                      )}
                    </td>
                    <td style={{ padding: '13px 12px' }}>
                      <strong style={{ 
                        color: '#ffb347', 
                        fontSize: '14px',
                        letterSpacing: '0.2px' 
                      }}>
                        {log.player_name || 'Hệ thống'}
                      </strong>
                    </td>
                    <td style={{ padding: '13px 12px', textAlign: 'center' }}>
                      {getTypeBadge(log.type)}
                    </td>
                    <td style={{ 
                      padding: '13px 12px', 
                      color: '#eee', 
                      lineHeight: '1.5',
                      wordBreak: 'break-word'
                    }}>
                      {log.action}
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>

        {/* Pagination Bar */}
        {totalPages > 1 && (
          <div style={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            marginTop: '22px',
            paddingTop: '16px',
            borderTop: '1px solid rgba(255, 255, 255, 0.08)',
            flexWrap: 'wrap',
            gap: '12px'
          }}>
            <div style={{ fontSize: '13px', color: '#aaa' }}>
              Trang <strong style={{ color: '#00e5ff' }}>{page}</strong> / <strong>{totalPages}</strong> (Tổng <strong style={{ color: '#fff' }}>{total.toLocaleString()}</strong> bản ghi)
            </div>

            <div style={{ display: 'flex', gap: '6px', alignItems: 'center' }}>
              {/* First Page */}
              <button
                type="button"
                onClick={() => handlePageChange(1)}
                disabled={page === 1 || loading}
                style={{
                  padding: '6px 12px',
                  borderRadius: '6px',
                  border: '1px solid rgba(255, 255, 255, 0.1)',
                  background: page === 1 ? 'rgba(255, 255, 255, 0.02)' : 'rgba(0, 0, 0, 0.4)',
                  color: page === 1 ? '#555' : '#eee',
                  cursor: page === 1 ? 'not-allowed' : 'pointer',
                  fontSize: '13px',
                  fontWeight: '600',
                  transition: 'all 0.2s ease'
                }}
              >
                « Đầu
              </button>

              {/* Prev Page */}
              <button
                type="button"
                onClick={() => handlePageChange(page - 1)}
                disabled={page === 1 || loading}
                style={{
                  padding: '6px 12px',
                  borderRadius: '6px',
                  border: '1px solid rgba(255, 255, 255, 0.1)',
                  background: page === 1 ? 'rgba(255, 255, 255, 0.02)' : 'rgba(0, 0, 0, 0.4)',
                  color: page === 1 ? '#555' : '#eee',
                  cursor: page === 1 ? 'not-allowed' : 'pointer',
                  fontSize: '13px',
                  fontWeight: '600',
                  transition: 'all 0.2s ease'
                }}
              >
                ‹ Trước
              </button>

              {/* Current Page Tag */}
              <span style={{
                padding: '6px 14px',
                borderRadius: '6px',
                background: 'linear-gradient(135deg, #00e5ff 0%, #00a8cc 100%)',
                color: '#000',
                fontSize: '13px',
                fontWeight: 'bold',
                boxShadow: '0 2px 8px rgba(0, 229, 255, 0.3)'
              }}>
                {page}
              </span>

              {/* Next Page */}
              <button
                type="button"
                onClick={() => handlePageChange(page + 1)}
                disabled={page === totalPages || loading}
                style={{
                  padding: '6px 12px',
                  borderRadius: '6px',
                  border: '1px solid rgba(255, 255, 255, 0.1)',
                  background: page === totalPages ? 'rgba(255, 255, 255, 0.02)' : 'rgba(0, 0, 0, 0.4)',
                  color: page === totalPages ? '#555' : '#eee',
                  cursor: page === totalPages ? 'not-allowed' : 'pointer',
                  fontSize: '13px',
                  fontWeight: '600',
                  transition: 'all 0.2s ease'
                }}
              >
                Sau ›
              </button>

              {/* Last Page */}
              <button
                type="button"
                onClick={() => handlePageChange(totalPages)}
                disabled={page === totalPages || loading}
                style={{
                  padding: '6px 12px',
                  borderRadius: '6px',
                  border: '1px solid rgba(255, 255, 255, 0.1)',
                  background: page === totalPages ? 'rgba(255, 255, 255, 0.02)' : 'rgba(0, 0, 0, 0.4)',
                  color: page === totalPages ? '#555' : '#eee',
                  cursor: page === totalPages ? 'not-allowed' : 'pointer',
                  fontSize: '13px',
                  fontWeight: '600',
                  transition: 'all 0.2s ease'
                }}
              >
                Cuối »
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
