import React, { useState, useEffect, useCallback } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

function AdminAccount() {
  const { showMessage } = useOutletContext();
  const [accounts, setAccounts] = useState([]);
  
  // Stats
  const [totalAccounts, setTotalAccounts] = useState(0);
  const [totalOnline, setTotalOnline] = useState(0);
  const [totalMembers, setTotalMembers] = useState(0);
  const [filteredCount, setFilteredCount] = useState(0);
  const [totalPages, setTotalPages] = useState(1);

  // Pagination & Filter states
  const [page, setPage] = useState(1);
  const [limit, setLimit] = useState(10);
  const [searchQuery, setSearchQuery] = useState('');
  const [statusFilter, setStatusFilter] = useState('all');
  const [lockFilter, setLockFilter] = useState('all');
  const [loading, setLoading] = useState(false);

  // Buff Nạp Modal state
  const [buffModalUser, setBuffModalUser] = useState(null);
  const [buffAmount, setBuffAmount] = useState('');
  const [buffIsDeposit, setBuffIsDeposit] = useState(true);
  const [submittingBuff, setSubmittingBuff] = useState(false);

  const fetchAccounts = useCallback(async (targetPage = page, targetLimit = limit) => {
    setLoading(true);
    try {
      const res = await api.get('admin/accounts', {
        params: {
          page: targetPage,
          limit: targetLimit,
          search: searchQuery,
          status: statusFilter,
          lock: lockFilter
        }
      });
      if (res.data.success) {
        setAccounts(res.data.accounts || []);
        setTotalAccounts(res.data.totalAccounts || 0);
        setTotalOnline(res.data.totalOnline || 0);
        setTotalMembers(res.data.totalMembers || 0);
        setFilteredCount(res.data.filteredCount || 0);
        setTotalPages(res.data.totalPages || 1);
        setPage(res.data.currentPage || targetPage);
      }
    } catch {
      console.error("Lỗi lấy danh sách tài khoản");
    } finally {
      setLoading(false);
    }
  }, [searchQuery, statusFilter, lockFilter, limit, page]);

  // Fetch when page changes or when search/filters change
  useEffect(() => {
    const timer = setTimeout(() => {
      fetchAccounts(page, limit);
    }, 300);
    return () => clearTimeout(timer);
  }, [page, limit, searchQuery, statusFilter, lockFilter]);

  const handleSearchChange = (e) => {
    setSearchQuery(e.target.value);
    setPage(1);
  };

  const handleStatusChange = (e) => {
    setStatusFilter(e.target.value);
    setPage(1);
  };

  const handleLockChange = (e) => {
    setLockFilter(e.target.value);
    setPage(1);
  };

  const handleLimitChange = (e) => {
    const newLimit = parseInt(e.target.value) || 10;
    setLimit(newLimit);
    setPage(1);
  };

  const handlePageChange = (newPage) => {
    if (newPage >= 1 && newPage <= totalPages && newPage !== page) {
      setPage(newPage);
    }
  };

  const handleUpdateUser = async (action, targetUsername) => {
    if (!targetUsername) return;

    try {
      const payload = { username: targetUsername, action };

      const res = await api.post('admin/update_user/', payload);
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        fetchAccounts(page, limit);
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  const handleOpenBuffModal = (acc) => {
    setBuffModalUser(acc);
    setBuffAmount('');
    setBuffIsDeposit(true);
  };

  const handleConfirmBuffNap = async (e) => {
    e.preventDefault();
    if (!buffModalUser) return;
    const coinNum = Number(buffAmount);
    if (!coinNum || coinNum <= 0) {
      showMessage('error', 'Vui lòng nhập số Coin hợp lệ (> 0)!');
      return;
    }

    setSubmittingBuff(true);
    try {
      const res = await api.post('admin/add_coin/', {
        username: buffModalUser.user,
        amount: coinNum,
        isDeposit: buffIsDeposit
      });
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        setBuffModalUser(null);
        fetchAccounts(page, limit);
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ khi buff nạp!');
    } finally {
      setSubmittingBuff(false);
    }
  };

  // Generate page numbers for pagination bar
  const getPageNumbers = () => {
    const pages = [];
    const maxVisible = 5;
    let start = Math.max(1, page - Math.floor(maxVisible / 2));
    let end = Math.min(totalPages, start + maxVisible - 1);

    if (end - start + 1 < maxVisible) {
      start = Math.max(1, end - maxVisible + 1);
    }

    for (let i = start; i <= end; i++) {
      pages.push(i);
    }
    return pages;
  };

  const startRecord = filteredCount > 0 ? (page - 1) * limit + 1 : 0;
  const endRecord = Math.min(filteredCount, page * limit);

  return (
    <div style={{ 
      maxWidth: '1000px', 
      margin: '0 auto', 
      fontFamily: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif',
      color: '#eee',
      padding: '20px'
    }}>
      <h3 style={{ 
        color: '#ff3366', 
        marginBottom: '24px', 
        textAlign: 'center',
        fontSize: '24px',
        fontWeight: '800',
        textTransform: 'uppercase',
        letterSpacing: '1px',
        background: 'linear-gradient(135deg, #ff3366 0%, #ff5e62 100%)',
        WebkitBackgroundClip: 'text',
        WebkitTextFillColor: 'transparent',
      }}>
        🔍 QUẢN LÝ TÀI KHOẢN
      </h3>
      
      {/* Stats Cards */}
      <div style={{
        display: 'grid',
        gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))',
        gap: '20px',
        marginBottom: '24px'
      }}>
        {/* Total Accounts */}
        <div style={{
          background: 'linear-gradient(135deg, rgba(20,20,20,0.8) 0%, rgba(30,30,30,0.8) 100%)',
          backdropFilter: 'blur(10px)',
          border: '1px solid rgba(255,255,255,0.05)',
          borderRadius: '16px',
          padding: '24px',
          display: 'flex',
          alignItems: 'center',
          boxShadow: '0 8px 32px rgba(0,0,0,0.2)',
          position: 'relative',
          overflow: 'hidden'
        }}>
          <div style={{
            background: 'rgba(52, 152, 219, 0.15)',
            width: '60px',
            height: '60px',
            borderRadius: '16px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            fontSize: '28px',
            marginRight: '20px',
            border: '1px solid rgba(52, 152, 219, 0.3)'
          }}>
            👥
          </div>
          <div>
            <div style={{ fontSize: '14px', color: '#aaa', fontWeight: '600', textTransform: 'uppercase', marginBottom: '4px' }}>Tổng Tài Khoản</div>
            <div style={{ fontSize: '28px', fontWeight: '800', color: '#fff' }}>{totalAccounts.toLocaleString()}</div>
          </div>
          <div style={{ position: 'absolute', top: '-10px', right: '-10px', fontSize: '80px', opacity: 0.05 }}>👥</div>
        </div>

        {/* Total Online */}
        <div style={{
          background: 'linear-gradient(135deg, rgba(20,20,20,0.8) 0%, rgba(30,30,30,0.8) 100%)',
          backdropFilter: 'blur(10px)',
          border: '1px solid rgba(255,255,255,0.05)',
          borderRadius: '16px',
          padding: '24px',
          display: 'flex',
          alignItems: 'center',
          boxShadow: '0 8px 32px rgba(0,0,0,0.2)',
          position: 'relative',
          overflow: 'hidden'
        }}>
          <div style={{
            background: 'rgba(46, 204, 113, 0.15)',
            width: '60px',
            height: '60px',
            borderRadius: '16px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            fontSize: '28px',
            marginRight: '20px',
            border: '1px solid rgba(46, 204, 113, 0.3)'
          }}>
            🟢
          </div>
          <div>
            <div style={{ fontSize: '14px', color: '#aaa', fontWeight: '600', textTransform: 'uppercase', marginBottom: '4px' }}>Đang Online</div>
            <div style={{ fontSize: '28px', fontWeight: '800', color: '#fff' }}>{totalOnline.toLocaleString()}</div>
          </div>
          <div style={{ position: 'absolute', top: '-10px', right: '-10px', fontSize: '80px', opacity: 0.05 }}>🟢</div>
        </div>

        {/* Total Members */}
        <div style={{
          background: 'linear-gradient(135deg, rgba(20,20,20,0.8) 0%, rgba(30,30,30,0.8) 100%)',
          backdropFilter: 'blur(10px)',
          border: '1px solid rgba(255,255,255,0.05)',
          borderRadius: '16px',
          padding: '24px',
          display: 'flex',
          alignItems: 'center',
          boxShadow: '0 8px 32px rgba(0,0,0,0.2)',
          position: 'relative',
          overflow: 'hidden'
        }}>
          <div style={{
            background: 'rgba(241, 196, 15, 0.15)',
            width: '60px',
            height: '60px',
            borderRadius: '16px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            fontSize: '28px',
            marginRight: '20px',
            border: '1px solid rgba(241, 196, 15, 0.3)'
          }}>
            👑
          </div>
          <div>
            <div style={{ fontSize: '14px', color: '#aaa', fontWeight: '600', textTransform: 'uppercase', marginBottom: '4px' }}>Mở Thành Viên</div>
            <div style={{ fontSize: '28px', fontWeight: '800', color: '#fff' }}>{totalMembers.toLocaleString()}</div>
          </div>
          <div style={{ position: 'absolute', top: '-10px', right: '-10px', fontSize: '80px', opacity: 0.05 }}>👑</div>
        </div>
      </div>

      {/* Search and Filters */}
      <div style={{
        display: 'flex',
        gap: '16px',
        flexWrap: 'wrap',
        marginBottom: '24px',
        background: 'rgba(20, 20, 20, 0.75)',
        backdropFilter: 'blur(10px)',
        border: '1px solid rgba(255, 255, 255, 0.08)',
        borderRadius: '12px',
        padding: '20px',
        boxShadow: '0 8px 32px rgba(0, 0, 0, 0.3)'
      }}>
        <div style={{ flex: '2 1 220px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Tìm kiếm:</label>
          <input
            type="text"
            placeholder="Tìm theo tài khoản, ID hoặc tên nhân vật..."
            value={searchQuery}
            onChange={handleSearchChange}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              transition: 'all 0.25s'
            }}
          />
        </div>
        <div style={{ flex: '1 1 140px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Kích hoạt:</label>
          <select
            value={statusFilter}
            onChange={handleStatusChange}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              cursor: 'pointer'
            }}
          >
            <option value="all">Tất cả thành viên</option>
            <option value="active">Đã kích hoạt (MTV)</option>
            <option value="inactive">Chưa kích hoạt</option>
          </select>
        </div>
        <div style={{ flex: '1 1 140px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Trạng thái khóa:</label>
          <select
            value={lockFilter}
            onChange={handleLockChange}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              cursor: 'pointer'
            }}
          >
            <option value="all">Tất cả</option>
            <option value="normal">Bình thường</option>
            <option value="banned">Bị Banned</option>
          </select>
        </div>
        <div style={{ flex: '0 1 120px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Hiển thị:</label>
          <select
            value={limit}
            onChange={handleLimitChange}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: '1px solid rgba(255, 215, 0, 0.3)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: '#ffd700',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              fontWeight: '600',
              cursor: 'pointer'
            }}
          >
            <option value={10}>10 / trang</option>
            <option value={20}>20 / trang</option>
            <option value={50}>50 / trang</option>
            <option value={100}>100 / trang</option>
          </select>
        </div>
      </div>

      <div style={{
        background: 'rgba(20, 20, 20, 0.75)',
        backdropFilter: 'blur(10px)',
        border: '1px solid rgba(255, 255, 255, 0.08)',
        borderRadius: '16px',
        padding: '24px',
        boxShadow: '0 8px 32px rgba(0, 0, 0, 0.3)'
      }}>
        <div style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          marginBottom: '20px',
          borderBottom: '1px solid rgba(255, 255, 255, 0.08)',
          paddingBottom: '12px',
          flexWrap: 'wrap',
          gap: '10px'
        }}>
          <h4 style={{ 
            color: '#fff', 
            margin: 0,
            fontWeight: '700',
            fontSize: '16px'
          }}>
            📋 DANH SÁCH TÀI KHOẢN ({filteredCount} / {totalAccounts})
          </h4>
          <div style={{ fontSize: '13px', color: '#aaa' }}>
            {filteredCount > 0 ? (
              <span>Hiển thị <strong style={{ color: '#ffd700' }}>{startRecord} - {endRecord}</strong> trên tổng <strong style={{ color: '#fff' }}>{filteredCount}</strong> kết quả</span>
            ) : (
              <span>Không có kết quả</span>
            )}
          </div>
        </div>
        
        <div style={{ overflowX: 'auto' }}>
          <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left', fontSize: '14px', color: '#eee' }}>
            <thead>
              <tr style={{ borderBottom: '2px solid rgba(255,255,255,0.12)', background: 'rgba(0,0,0,0.3)' }}>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>ID</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Tài khoản</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Nhân vật</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Coin</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Thành viên</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600' }}>Trạng thái</th>
                <th style={{ padding: '12px 10px', color: '#999', fontSize: '12.5px', textTransform: 'uppercase', fontWeight: '600', textAlign: 'center' }}>Hành động</th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr>
                  <td colSpan="7" style={{ padding: '30px', textAlign: 'center', color: '#aaa' }}>
                    🔄 Đang tải dữ liệu từ máy chủ...
                  </td>
                </tr>
              ) : accounts.length > 0 ? (
                accounts.map(acc => (
                  <tr key={acc.id} style={{ borderBottom: '1px solid rgba(255,255,255,0.05)' }}>
                    <td style={{ padding: '14px 10px', color: '#888' }}>{acc.id}</td>
                    <td style={{ padding: '14px 10px', fontWeight: 'bold', color: '#00e5ff' }}>{acc.user}</td>
                    <td style={{ padding: '14px 10px', color: '#ff8a00', fontWeight: '600' }}>{acc.charName || "Chưa tạo"}</td>
                    <td style={{ padding: '14px 10px', color: '#faad14', fontWeight: '500' }}>💰 {acc.coin.toLocaleString()}</td>
                    <td style={{ padding: '14px 10px' }}>
                      <span style={{ 
                        padding: '3px 8px', 
                        borderRadius: '6px', 
                        fontSize: '12px', 
                        background: acc.status === 1 ? 'rgba(82,196,26,0.15)' : 'rgba(255,255,255,0.05)', 
                        color: acc.status === 1 ? '#52c41a' : '#aaa',
                        border: acc.status === 1 ? '1px solid rgba(82,196,26,0.3)' : '1px solid rgba(255,255,255,0.1)',
                        fontWeight: '600'
                      }}>
                        {acc.status === 1 ? 'Đã kích hoạt' : 'Chưa'}
                      </span>
                    </td>
                    <td style={{ padding: '14px 10px' }}>
                      <span style={{ 
                        padding: '3px 8px', 
                        borderRadius: '6px', 
                        fontSize: '12px', 
                        background: acc.lock === 1 ? 'rgba(255,77,79,0.15)' : 'rgba(82,196,26,0.15)', 
                        color: acc.lock === 1 ? '#ff4d4f' : '#52c41a',
                        border: acc.lock === 1 ? '1px solid rgba(255,77,79,0.3)' : '1px solid rgba(82,196,26,0.3)',
                        fontWeight: '600'
                      }}>
                        {acc.lock === 1 ? 'BANNED' : 'Bình thường'}
                      </span>
                    </td>
                    <td style={{ padding: '14px 10px', display: 'flex', gap: '8px', justifyContent: 'center', alignItems: 'center' }}>
                      <button
                        onClick={() => handleOpenBuffModal(acc)}
                        style={{
                          padding: '6px 12px',
                          borderRadius: '6px',
                          border: '1px solid rgba(0, 229, 255, 0.4)',
                          background: 'rgba(0, 229, 255, 0.1)',
                          color: '#00e5ff',
                          cursor: 'pointer',
                          fontSize: '12.5px',
                          fontWeight: 'bold',
                          transition: 'all 0.2s ease',
                        }}
                        onMouseEnter={(e) => {
                          e.target.style.background = 'rgba(0, 229, 255, 0.25)';
                        }}
                        onMouseLeave={(e) => {
                          e.target.style.background = 'rgba(0, 229, 255, 0.1)';
                        }}
                        title="Buff Coin & Tích nạp cho tài khoản"
                      >
                        ⚡ Buff Nạp
                      </button>
                      <button
                        onClick={() => handleUpdateUser('activate', acc.user)}
                        style={{
                          padding: '6px 12px',
                          borderRadius: '6px',
                          border: `1px solid ${acc.status === 1 ? 'rgba(255,172,48,0.4)' : 'rgba(82,196,26,0.4)'}`,
                          background: 'transparent',
                          color: acc.status === 1 ? '#ffac30' : '#52c41a',
                          cursor: 'pointer',
                          fontSize: '12.5px',
                          fontWeight: 'bold',
                          transition: 'all 0.2s ease',
                        }}
                        onMouseEnter={(e) => {
                          e.target.style.background = acc.status === 1 ? 'rgba(255,172,48,0.1)' : 'rgba(82,196,26,0.1)';
                        }}
                        onMouseLeave={(e) => {
                          e.target.style.background = 'transparent';
                        }}
                      >
                        {acc.status === 1 ? '👑 Hủy MTV' : '⚡ Mở MTV'}
                      </button>
                      <button
                        onClick={() => handleUpdateUser('lock', acc.user)}
                        style={{
                          padding: '6px 12px',
                          borderRadius: '6px',
                          border: `1px solid ${acc.lock === 1 ? 'rgba(82,196,26,0.4)' : 'rgba(255,77,79,0.4)'}`,
                          background: 'transparent',
                          color: acc.lock === 1 ? '#52c41a' : '#ff4d4f',
                          cursor: 'pointer',
                          fontSize: '12.5px',
                          fontWeight: 'bold',
                          transition: 'all 0.2s ease',
                        }}
                        onMouseEnter={(e) => {
                          e.target.style.background = acc.lock === 1 ? 'rgba(82,196,26,0.1)' : 'rgba(255,77,79,0.1)';
                        }}
                        onMouseLeave={(e) => {
                          e.target.style.background = 'transparent';
                        }}
                      >
                        {acc.lock === 1 ? '🔓 Mở Khóa' : '🔒 Khóa Nick'}
                      </button>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan="7" style={{ padding: '30px', textAlign: 'center', color: '#888' }}>
                    Không tìm thấy tài khoản phù hợp
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        {/* Pagination Bar */}
        {totalPages > 1 && (
          <div style={{
            display: 'flex',
            justify: 'space-between',
            alignItems: 'center',
            marginTop: '24px',
            paddingTop: '16px',
            borderTop: '1px solid rgba(255, 255, 255, 0.08)',
            flexWrap: 'wrap',
            gap: '12px'
          }}>
            <div style={{ fontSize: '13px', color: '#aaa' }}>
              Trang <strong style={{ color: '#ffd700' }}>{page}</strong> / <strong>{totalPages}</strong>
            </div>

            <div style={{ display: 'flex', gap: '6px', alignItems: 'center' }}>
              {/* First Page */}
              <button
                onClick={() => handlePageChange(1)}
                disabled={page === 1}
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

              {/* Previous Page */}
              <button
                onClick={() => handlePageChange(page - 1)}
                disabled={page === 1}
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

              {/* Page Numbers */}
              {getPageNumbers().map(pNum => (
                <button
                  key={pNum}
                  onClick={() => handlePageChange(pNum)}
                  style={{
                    padding: '6px 12px',
                    borderRadius: '6px',
                    border: pNum === page ? '1px solid #ffd700' : '1px solid rgba(255, 255, 255, 0.1)',
                    background: pNum === page ? 'linear-gradient(135deg, rgba(255, 215, 0, 0.25) 0%, rgba(255, 140, 0, 0.25) 100%)' : 'rgba(0, 0, 0, 0.4)',
                    color: pNum === page ? '#ffd700' : '#eee',
                    cursor: 'pointer',
                    fontSize: '13px',
                    fontWeight: pNum === page ? '700' : '600',
                    boxShadow: pNum === page ? '0 0 10px rgba(255, 215, 0, 0.2)' : 'none',
                    transition: 'all 0.2s ease'
                  }}
                >
                  {pNum}
                </button>
              ))}

              {/* Next Page */}
              <button
                onClick={() => handlePageChange(page + 1)}
                disabled={page === totalPages}
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
                onClick={() => handlePageChange(totalPages)}
                disabled={page === totalPages}
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

      {/* Modal Buff Nạp */}
      {buffModalUser && (
        <div style={{
          position: 'fixed',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          backgroundColor: 'rgba(0,0,0,0.75)',
          backdropFilter: 'blur(8px)',
          zIndex: 2000,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          padding: '20px'
        }}>
          <div style={{
            background: 'linear-gradient(135deg, #1a1a1a 0%, #222 100%)',
            border: '1px solid rgba(0, 229, 255, 0.4)',
            borderRadius: '16px',
            padding: '28px',
            maxWidth: '460px',
            width: '100%',
            boxShadow: '0 20px 50px rgba(0, 0, 0, 0.7)',
            color: '#fff',
            position: 'relative'
          }}>
            <h4 style={{
              margin: '0 0 16px 0',
              color: '#00e5ff',
              fontSize: '20px',
              fontWeight: '800',
              display: 'flex',
              alignItems: 'center',
              gap: '8px'
            }}>
              ⚡ BUFF NẠP TIỀN QUẢN TRỊ
            </h4>
            
            <div style={{
              background: 'rgba(0, 0, 0, 0.4)',
              padding: '14px 16px',
              borderRadius: '10px',
              marginBottom: '20px',
              fontSize: '13.5px',
              border: '1px solid rgba(255,255,255,0.08)',
              display: 'flex',
              flexDirection: 'column',
              gap: '6px'
            }}>
              <div>👤 Tài khoản: <strong style={{ color: '#00e5ff' }}>{buffModalUser.user}</strong></div>
              <div>🎮 Nhân vật: <strong style={{ color: '#ff8a00' }}>{buffModalUser.charName || 'Chưa tạo'}</strong></div>
              <div>💰 Coin hiện có: <strong style={{ color: '#faad14' }}>{buffModalUser.coin.toLocaleString()} Coin</strong></div>
            </div>

            <form onSubmit={handleConfirmBuffNap}>
              <div style={{ marginBottom: '18px' }}>
                <label style={{ display: 'block', marginBottom: '8px', fontSize: '13.5px', color: '#aaa', fontWeight: '600' }}>
                  Số Coin cần Buff:
                </label>
                <input
                  type="number"
                  min="1"
                  placeholder="Ví dụ: 1000, 50000..."
                  value={buffAmount}
                  onChange={(e) => setBuffAmount(e.target.value)}
                  required
                  autoFocus
                  style={{
                    width: '100%',
                    padding: '12px 14px',
                    borderRadius: '8px',
                    border: '1px solid rgba(0, 229, 255, 0.3)',
                    backgroundColor: 'rgba(0, 0, 0, 0.5)',
                    color: '#fff',
                    fontSize: '15px',
                    fontWeight: 'bold',
                    outline: 'none',
                    boxSizing: 'border-box'
                  }}
                />
                {Number(buffAmount) > 0 && (
                  <div style={{ fontSize: '12.5px', color: '#52c41a', marginTop: '8px', fontWeight: '500' }}>
                    💡 Tương đương: <strong>+{(Number(buffAmount) * 1000).toLocaleString()}đ</strong> Tích nạp & Tổng nạp (Tự động tính VIP)
                  </div>
                )}
              </div>

              <div style={{ marginBottom: '22px' }}>
                <label style={{ display: 'flex', alignItems: 'center', gap: '10px', cursor: 'pointer', fontSize: '13.5px', color: '#eee' }}>
                  <input
                    type="checkbox"
                    checked={buffIsDeposit}
                    onChange={(e) => setBuffIsDeposit(e.target.checked)}
                    style={{ width: '18px', height: '18px', cursor: 'pointer', accentColor: '#00e5ff' }}
                  />
                  <span>⚡ Cộng như nạp tiền (Tính Tích Lũy Nạp & Cấp VIP)</span>
                </label>
              </div>

              <div style={{ display: 'flex', gap: '12px', justifyContent: 'flex-end' }}>
                <button
                  type="button"
                  onClick={() => setBuffModalUser(null)}
                  style={{
                    padding: '10px 18px',
                    borderRadius: '8px',
                    border: '1px solid rgba(255, 255, 255, 0.15)',
                    background: 'transparent',
                    color: '#aaa',
                    cursor: 'pointer',
                    fontWeight: '600',
                    fontSize: '14px'
                  }}
                >
                  Hủy
                </button>
                <button
                  type="submit"
                  disabled={submittingBuff}
                  style={{
                    padding: '10px 20px',
                    borderRadius: '8px',
                    border: 'none',
                    background: 'linear-gradient(135deg, #00e5ff 0%, #0088cc 100%)',
                    color: '#fff',
                    cursor: submittingBuff ? 'not-allowed' : 'pointer',
                    fontWeight: 'bold',
                    fontSize: '14px',
                    boxShadow: '0 4px 15px rgba(0, 229, 255, 0.3)'
                  }}
                >
                  {submittingBuff ? 'Đang xử lý...' : '⚡ Buff Nạp Ngay'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}

export default AdminAccount;
