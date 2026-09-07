import React, { useState, useEffect, useCallback } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const OPTION_NAMES = {
  0: 'Tấn công vật lý',
  1: 'Tấn công',
  2: 'Tấn công phép',
  3: 'Phòng thủ vật lý',
  4: 'Phòng thủ phép',
  5: 'Bạo kích (Chí mạng)',
  6: 'Né tránh',
  7: 'Kháng tất cả',
  8: 'Tốc độ chạy',
  9: 'Hồi phục HP',
  10: 'Tăng HP tối đa',
  11: 'Tăng MP tối đa',
  12: 'Giảm sát thương',
  13: 'Xuyên giáp',
  14: 'Phản sát thương',
  15: 'Hút máu (Hấp thu HP)',
  16: 'Tăng EXP',
  17: 'Tăng Beri',
  18: 'Kháng bạo kích',
  19: 'Kháng choáng',
  20: 'Sức đánh',
  21: 'Tấn công quái',
  22: 'Tấn công người',
  23: 'Sát thương chí mạng',
  24: 'Kháng độc',
  25: 'Kháng lửa',
  26: 'Kháng băng',
  27: 'Kháng sét',
  46: 'Xuyên kháng bạo',
  47: 'Kháng xuyên giáp',
  53: 'Miễn thương',
  56: 'Bền bỉ / Sinh mệnh'
};

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
  const [onlineFilter, setOnlineFilter] = useState('all');
  const [loading, setLoading] = useState(false);

  // Buff Nạp Modal state
  const [buffModalUser, setBuffModalUser] = useState(null);
  const [buffAmount, setBuffAmount] = useState('');
  const [buffIsDeposit, setBuffIsDeposit] = useState(true);
  const [submittingBuff, setSubmittingBuff] = useState(false);

  // Account Detail Modal state
  const [detailModalOpen, setDetailModalOpen] = useState(false);
  const [detailLoading, setDetailLoading] = useState(false);
  const [detailData, setDetailData] = useState(null);
  const [detailTab, setDetailTab] = useState('overview');
  const [invSubTab, setInvSubTab] = useState('bag3');

  // Adjust Currency Modal state
  const [currencyModalOpen, setCurrencyModalOpen] = useState(false);
  const [currencyData, setCurrencyData] = useState({
    username: '',
    charName: '',
    hasPlayer: false,
    ruby: 0,
    vang: 0,
    coin: 0,
    tichnap: 0,
    tongnap: 0,
    vip: 0,
    autoVip: true
  });
  const [submittingCurrency, setSubmittingCurrency] = useState(false);

  const handleOpenDetailModal = async (username) => {
    setDetailModalOpen(true);
    setDetailLoading(true);
    setDetailTab('overview');
    setInvSubTab('bag3');
    try {
      const res = await api.get('admin/account_detail', { params: { username } });
      if (res.data.success) {
        setDetailData(res.data);
      } else {
        showMessage('error', res.data.message || 'Không thể lấy thông tin chi tiết tài khoản');
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ khi lấy chi tiết tài khoản!');
    } finally {
      setDetailLoading(false);
    }
  };

  const handleOpenCurrencyModal = () => {
    if (!detailData?.account) return;
    setCurrencyData({
      username: detailData.account.user,
      charName: detailData.player?.name || '',
      hasPlayer: !!detailData.player,
      ruby: detailData.player ? detailData.player.ruby : 0,
      vang: detailData.player ? detailData.player.vang : 0,
      coin: detailData.account.coin || 0,
      tichnap: detailData.account.tichnap || 0,
      tongnap: detailData.account.tongnap || 0,
      vip: detailData.account.vip || 0,
      autoVip: true
    });
    setCurrencyModalOpen(true);
  };

  const handleSaveCurrency = async (e) => {
    e.preventDefault();
    if (!currencyData.username) return;

    setSubmittingCurrency(true);
    try {
      const payload = {
        username: currencyData.username,
        coin: Number(currencyData.coin) || 0,
        tichnap: Number(currencyData.tichnap) || 0,
        tongnap: Number(currencyData.tongnap) || 0,
        vip: currencyData.autoVip ? undefined : (Number(currencyData.vip) || 0)
      };

      if (currencyData.hasPlayer) {
        payload.ruby = Number(currencyData.ruby) || 0;
        payload.vang = Number(currencyData.vang) || 0;
      }

      const res = await api.post('admin/adjust_currency', payload);
      if (res.data.success) {
        showMessage('success', res.data.message || 'Cập nhật tiền tệ & tài sản thành công!');
        setCurrencyModalOpen(false);
        handleOpenDetailModal(currencyData.username);
        fetchAccounts(page);
      } else {
        showMessage('error', res.data.message || 'Không thể cập nhật tiền tệ!');
      }
    } catch {
      showMessage('error', 'Lỗi kết nối khi cập nhật tiền tệ!');
    } finally {
      setSubmittingCurrency(false);
    }
  };

  const fetchAccounts = useCallback(async (targetPage = page, targetLimit = limit) => {
    setLoading(true);
    try {
      const res = await api.get('admin/accounts', {
        params: {
          page: targetPage,
          limit: targetLimit,
          search: searchQuery,
          status: statusFilter,
          lock: lockFilter,
          online: onlineFilter
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
  }, [searchQuery, statusFilter, lockFilter, onlineFilter, limit, page]);

  // Fetch when page changes or when search/filters change
  useEffect(() => {
    const timer = setTimeout(() => {
      fetchAccounts(page, limit);
    }, 300);
    return () => clearTimeout(timer);
  }, [page, limit, searchQuery, statusFilter, lockFilter, onlineFilter]);

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

  const handleOnlineChange = (e) => {
    setOnlineFilter(e.target.value);
    setPage(1);
  };

  const handleCardClickAll = () => {
    setOnlineFilter('all');
    setStatusFilter('all');
    setLockFilter('all');
    setSearchQuery('');
    setPage(1);
  };

  const handleCardClickOnline = () => {
    setOnlineFilter((prev) => (prev === 'online' ? 'all' : 'online'));
    setPage(1);
  };

  const handleCardClickMembers = () => {
    setStatusFilter((prev) => (prev === 'active' ? 'all' : 'active'));
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
        <div 
          onClick={handleCardClickAll}
          title="Nhấn để xem toàn bộ tài khoản"
          style={{
            background: 'linear-gradient(135deg, rgba(20,20,20,0.8) 0%, rgba(30,30,30,0.8) 100%)',
            backdropFilter: 'blur(10px)',
            border: (onlineFilter === 'all' && statusFilter === 'all' && lockFilter === 'all' && !searchQuery)
              ? '1.5px solid rgba(52, 152, 219, 0.8)' 
              : '1px solid rgba(255,255,255,0.05)',
            borderRadius: '16px',
            padding: '24px',
            display: 'flex',
            alignItems: 'center',
            boxShadow: (onlineFilter === 'all' && statusFilter === 'all' && lockFilter === 'all' && !searchQuery)
              ? '0 8px 32px rgba(52, 152, 219, 0.25)' 
              : '0 8px 32px rgba(0,0,0,0.2)',
            position: 'relative',
            overflow: 'hidden',
            cursor: 'pointer',
            transition: 'all 0.25s ease'
          }}
          onMouseEnter={(e) => e.currentTarget.style.transform = 'translateY(-3px)'}
          onMouseLeave={(e) => e.currentTarget.style.transform = 'translateY(0)'}
        >
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
        <div 
          onClick={handleCardClickOnline}
          title="Nhấn để lọc danh sách tài khoản đang Online"
          style={{
            background: onlineFilter === 'online' 
              ? 'linear-gradient(135deg, rgba(46, 204, 113, 0.18) 0%, rgba(20,20,20,0.9) 100%)' 
              : 'linear-gradient(135deg, rgba(20,20,20,0.8) 0%, rgba(30,30,30,0.8) 100%)',
            backdropFilter: 'blur(10px)',
            border: onlineFilter === 'online' 
              ? '1.5px solid #2ecc71' 
              : '1px solid rgba(255,255,255,0.05)',
            borderRadius: '16px',
            padding: '24px',
            display: 'flex',
            alignItems: 'center',
            boxShadow: onlineFilter === 'online' 
              ? '0 8px 32px rgba(46, 204, 113, 0.35)' 
              : '0 8px 32px rgba(0,0,0,0.2)',
            position: 'relative',
            overflow: 'hidden',
            cursor: 'pointer',
            transition: 'all 0.25s ease'
          }}
          onMouseEnter={(e) => e.currentTarget.style.transform = 'translateY(-3px)'}
          onMouseLeave={(e) => e.currentTarget.style.transform = 'translateY(0)'}
        >
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
            <div style={{ fontSize: '14px', color: '#aaa', fontWeight: '600', textTransform: 'uppercase', marginBottom: '4px', display: 'flex', alignItems: 'center', gap: '6px' }}>
              <span>Đang Online</span>
              {onlineFilter === 'online' && (
                <span style={{ fontSize: '11px', color: '#2ecc71', fontWeight: 'bold', background: 'rgba(46,204,113,0.15)', padding: '1px 6px', borderRadius: '4px', border: '1px solid rgba(46,204,113,0.3)' }}>
                  ✓ Đang lọc
                </span>
              )}
            </div>
            <div style={{ fontSize: '28px', fontWeight: '800', color: onlineFilter === 'online' ? '#2ecc71' : '#fff' }}>{totalOnline.toLocaleString()}</div>
          </div>
          <div style={{ position: 'absolute', top: '-10px', right: '-10px', fontSize: '80px', opacity: 0.05 }}>🟢</div>
        </div>

        {/* Total Members */}
        <div 
          onClick={handleCardClickMembers}
          title="Nhấn để lọc danh sách tài khoản đã kích hoạt (MTV)"
          style={{
            background: statusFilter === 'active' 
              ? 'linear-gradient(135deg, rgba(241, 196, 15, 0.18) 0%, rgba(20,20,20,0.9) 100%)' 
              : 'linear-gradient(135deg, rgba(20,20,20,0.8) 0%, rgba(30,30,30,0.8) 100%)',
            backdropFilter: 'blur(10px)',
            border: statusFilter === 'active' 
              ? '1.5px solid #f1c40f' 
              : '1px solid rgba(255,255,255,0.05)',
            borderRadius: '16px',
            padding: '24px',
            display: 'flex',
            alignItems: 'center',
            boxShadow: statusFilter === 'active' 
              ? '0 8px 32px rgba(241, 196, 15, 0.35)' 
              : '0 8px 32px rgba(0,0,0,0.2)',
            position: 'relative',
            overflow: 'hidden',
            cursor: 'pointer',
            transition: 'all 0.25s ease'
          }}
          onMouseEnter={(e) => e.currentTarget.style.transform = 'translateY(-3px)'}
          onMouseLeave={(e) => e.currentTarget.style.transform = 'translateY(0)'}
        >
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
            <div style={{ fontSize: '14px', color: '#aaa', fontWeight: '600', textTransform: 'uppercase', marginBottom: '4px', display: 'flex', alignItems: 'center', gap: '6px' }}>
              <span>Mở Thành Viên</span>
              {statusFilter === 'active' && (
                <span style={{ fontSize: '11px', color: '#f1c40f', fontWeight: 'bold', background: 'rgba(241,196,15,0.15)', padding: '1px 6px', borderRadius: '4px', border: '1px solid rgba(241,196,15,0.3)' }}>
                  ✓ Đang lọc
                </span>
              )}
            </div>
            <div style={{ fontSize: '28px', fontWeight: '800', color: statusFilter === 'active' ? '#ffd700' : '#fff' }}>{totalMembers.toLocaleString()}</div>
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
        <div style={{ flex: '2 1 200px' }}>
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
        <div style={{ flex: '1 1 130px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Trực tuyến:</label>
          <select
            value={onlineFilter}
            onChange={handleOnlineChange}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: onlineFilter === 'online' ? '1px solid #2ecc71' : '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: onlineFilter === 'online' ? '#2ecc71' : '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              fontWeight: onlineFilter === 'online' ? '700' : 'normal',
              cursor: 'pointer'
            }}
          >
            <option value="all">Tất cả kết nối</option>
            <option value="online">🟢 Đang Online</option>
            <option value="offline">⚪ Offline</option>
          </select>
        </div>
        <div style={{ flex: '1 1 130px' }}>
          <label style={{ display: 'block', marginBottom: '8px', fontSize: '13px', color: '#aaa', fontWeight: '600' }}>Kích hoạt:</label>
          <select
            value={statusFilter}
            onChange={handleStatusChange}
            style={{
              width: '100%',
              padding: '11px 14px',
              borderRadius: '8px',
              border: statusFilter === 'active' ? '1px solid #f1c40f' : '1px solid rgba(255, 255, 255, 0.1)',
              backgroundColor: 'rgba(0, 0, 0, 0.45)',
              color: statusFilter === 'active' ? '#ffd700' : '#fff',
              outline: 'none',
              boxSizing: 'border-box',
              fontSize: '14px',
              fontWeight: statusFilter === 'active' ? '700' : 'normal',
              cursor: 'pointer'
            }}
          >
            <option value="all">Tất cả thành viên</option>
            <option value="active">Đã kích hoạt (MTV)</option>
            <option value="inactive">Chưa kích hoạt</option>
          </select>
        </div>
        <div style={{ flex: '1 1 130px' }}>
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
        <div style={{ flex: '0 1 110px' }}>
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
                    <td style={{ padding: '14px 10px', fontWeight: 'bold' }}>
                      <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                        <span 
                          onClick={() => handleOpenDetailModal(acc.user)}
                          style={{ color: '#00e5ff', cursor: 'pointer', textDecoration: 'underline', textUnderlineOffset: '3px' }}
                          title="Nhấn để xem chi tiết tài khoản, mốc nạp, vị trí, ruby, đồ..."
                        >
                          {acc.user}
                        </span>
                        {acc.onl === 1 ? (
                          <span style={{ 
                            fontSize: '10.5px', 
                            padding: '2px 6px', 
                            borderRadius: '4px', 
                            background: 'rgba(46, 204, 113, 0.15)', 
                            color: '#2ecc71', 
                            border: '1px solid rgba(46, 204, 113, 0.4)', 
                            fontWeight: 'bold',
                            display: 'inline-flex',
                            alignItems: 'center',
                            gap: '4px'
                          }} title="Đang trực tuyến">
                            <span style={{ width: '6px', height: '6px', borderRadius: '50%', backgroundColor: '#2ecc71', display: 'inline-block' }}></span>
                            ON
                          </span>
                        ) : (
                          <span style={{ 
                            fontSize: '10.5px', 
                            padding: '2px 6px', 
                            borderRadius: '4px', 
                            background: 'rgba(255, 255, 255, 0.04)', 
                            color: '#777', 
                            border: '1px solid rgba(255, 255, 255, 0.08)',
                            display: 'inline-flex',
                            alignItems: 'center',
                            gap: '4px'
                          }} title="Offline">
                            <span style={{ width: '6px', height: '6px', borderRadius: '50%', backgroundColor: '#555', display: 'inline-block' }}></span>
                            OFF
                          </span>
                        )}
                      </div>
                    </td>
                    <td style={{ padding: '14px 10px' }}>
                      <span 
                        onClick={() => handleOpenDetailModal(acc.user)}
                        style={{ color: '#ff8a00', fontWeight: '600', cursor: 'pointer' }}
                        title="Nhấn để xem chi tiết nhân vật"
                      >
                        {acc.charName || "Chưa tạo"}
                      </span>
                    </td>
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
                    <td style={{ padding: '14px 10px', display: 'flex', gap: '8px', justifyContent: 'center', alignItems: 'center', flexWrap: 'nowrap' }}>
                      <button
                        onClick={() => handleOpenDetailModal(acc.user)}
                        style={{
                          padding: '6px 12px',
                          borderRadius: '6px',
                          border: '1px solid rgba(146, 84, 222, 0.4)',
                          background: 'rgba(146, 84, 222, 0.1)',
                          color: '#b37feb',
                          cursor: 'pointer',
                          fontSize: '12.5px',
                          fontWeight: 'bold',
                          transition: 'all 0.2s ease',
                          whiteSpace: 'nowrap'
                        }}
                        onMouseEnter={(e) => {
                          e.target.style.background = 'rgba(146, 84, 222, 0.25)';
                        }}
                        onMouseLeave={(e) => {
                          e.target.style.background = 'rgba(146, 84, 222, 0.1)';
                        }}
                        title="Xem chi tiết tài khoản: Mốc nạp, Vị trí, Ruby, Đồ đạc..."
                      >
                        👁️ Chi tiết
                      </button>
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
                          whiteSpace: 'nowrap'
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

      {/* Modal Chi Tiết Tài Khoản */}
      {detailModalOpen && (
        <div style={{
          position: 'fixed',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          backgroundColor: 'rgba(0,0,0,0.82)',
          backdropFilter: 'blur(10px)',
          zIndex: 2100,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          padding: '20px'
        }}>
          <div style={{
            background: 'linear-gradient(135deg, #161616 0%, #1f1f1f 100%)',
            border: '1px solid rgba(146, 84, 222, 0.4)',
            borderRadius: '18px',
            maxWidth: '960px',
            width: '100%',
            maxHeight: '90vh',
            display: 'flex',
            flexDirection: 'column',
            boxShadow: '0 25px 60px rgba(0, 0, 0, 0.85), 0 0 30px rgba(146, 84, 222, 0.15)',
            color: '#fff',
            position: 'relative',
            overflow: 'hidden'
          }}>
            {/* Modal Header */}
            <div style={{
              padding: '20px 24px',
              borderBottom: '1px solid rgba(255,255,255,0.08)',
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              background: 'rgba(0,0,0,0.3)'
            }}>
              <div>
                <h3 style={{
                  margin: 0,
                  fontSize: '20px',
                  fontWeight: '800',
                  color: '#b37feb',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '10px'
                }}>
                  <span>👤 CHI TIẾT TÀI KHOẢN & NHÂN VẬT</span>
                  {detailData?.account && (
                    <span style={{ 
                      fontSize: '13px', 
                      padding: '2px 10px', 
                      borderRadius: '12px', 
                      background: detailData.account.onl === 1 ? 'rgba(46,204,113,0.2)' : 'rgba(255,255,255,0.08)', 
                      color: detailData.account.onl === 1 ? '#2ecc71' : '#aaa',
                      border: detailData.account.onl === 1 ? '1px solid rgba(46,204,113,0.4)' : '1px solid rgba(255,255,255,0.1)'
                    }}>
                      {detailData.account.onl === 1 ? '🟢 ONLINE' : '⚪ OFFLINE'}
                    </span>
                  )}
                </h3>
                {detailData?.account && (
                  <div style={{ fontSize: '13px', color: '#aaa', marginTop: '4px' }}>
                    Tài khoản: <strong style={{ color: '#00e5ff' }}>{detailData.account.user}</strong> (ID: #{detailData.account.id}) 
                    {detailData.player ? (
                      <span> | Nhân vật: <strong style={{ color: '#ff8a00' }}>{detailData.player.name}</strong> ({detailData.player.clazzName}, Lv.{detailData.player.level})</span>
                    ) : (
                      <span style={{ color: '#ff7875' }}> | (Chưa tạo nhân vật)</span>
                    )}
                  </div>
                )}
              </div>

              <button
                onClick={() => setDetailModalOpen(false)}
                style={{
                  background: 'rgba(255,255,255,0.05)',
                  border: '1px solid rgba(255,255,255,0.1)',
                  borderRadius: '50%',
                  width: '36px',
                  height: '36px',
                  color: '#aaa',
                  fontSize: '18px',
                  cursor: 'pointer',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  transition: 'all 0.2s'
                }}
                onMouseEnter={(e) => { e.currentTarget.style.background = 'rgba(255,77,79,0.2)'; e.currentTarget.style.color = '#ff4d4f'; }}
                onMouseLeave={(e) => { e.currentTarget.style.background = 'rgba(255,255,255,0.05)'; e.currentTarget.style.color = '#aaa'; }}
              >
                ✕
              </button>
            </div>

            {/* Modal Tabs Bar */}
            <div style={{
              display: 'flex',
              gap: '8px',
              padding: '12px 24px',
              borderBottom: '1px solid rgba(255,255,255,0.08)',
              background: 'rgba(0,0,0,0.2)',
              overflowX: 'auto'
            }}>
              <button
                onClick={() => setDetailTab('overview')}
                style={{
                  padding: '8px 16px',
                  borderRadius: '8px',
                  border: detailTab === 'overview' ? '1px solid #b37feb' : '1px solid rgba(255,255,255,0.08)',
                  background: detailTab === 'overview' ? 'rgba(146, 84, 222, 0.25)' : 'transparent',
                  color: detailTab === 'overview' ? '#b37feb' : '#aaa',
                  cursor: 'pointer',
                  fontWeight: detailTab === 'overview' ? '700' : '500',
                  fontSize: '13.5px',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '6px',
                  whiteSpace: 'nowrap'
                }}
              >
                <span>📊 Thông Tin & Chỉ Số</span>
              </button>

              <button
                onClick={() => setDetailTab('milestones')}
                style={{
                  padding: '8px 16px',
                  borderRadius: '8px',
                  border: detailTab === 'milestones' ? '1px solid #faad14' : '1px solid rgba(255,255,255,0.08)',
                  background: detailTab === 'milestones' ? 'rgba(250, 173, 20, 0.25)' : 'transparent',
                  color: detailTab === 'milestones' ? '#faad14' : '#aaa',
                  cursor: 'pointer',
                  fontWeight: detailTab === 'milestones' ? '700' : '500',
                  fontSize: '13.5px',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '6px',
                  whiteSpace: 'nowrap'
                }}
              >
                <span>🎁 Mốc Nạp</span>
                {detailData?.account?.milestones && (
                  <span style={{ fontSize: '11px', padding: '1px 6px', borderRadius: '10px', background: 'rgba(250, 173, 20, 0.2)', border: '1px solid rgba(250, 173, 20, 0.4)' }}>
                    {detailData.account.milestones.filter(m => m.isClaimed).length}/{detailData.account.milestones.length}
                  </span>
                )}
              </button>

              <button
                onClick={() => setDetailTab('equipment')}
                style={{
                  padding: '8px 16px',
                  borderRadius: '8px',
                  border: detailTab === 'equipment' ? '1px solid #00e5ff' : '1px solid rgba(255,255,255,0.08)',
                  background: detailTab === 'equipment' ? 'rgba(0, 229, 255, 0.25)' : 'transparent',
                  color: detailTab === 'equipment' ? '#00e5ff' : '#aaa',
                  cursor: 'pointer',
                  fontWeight: detailTab === 'equipment' ? '700' : '500',
                  fontSize: '13.5px',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '6px',
                  whiteSpace: 'nowrap'
                }}
              >
                <span>⚔️ Trang Bị Đang Mặc</span>
                {detailData?.player?.equippedItems && (
                  <span style={{ fontSize: '11px', padding: '1px 6px', borderRadius: '10px', background: 'rgba(0, 229, 255, 0.2)', border: '1px solid rgba(0, 229, 255, 0.4)' }}>
                    {detailData.player.equippedItems.length}
                  </span>
                )}
              </button>

              <button
                onClick={() => setDetailTab('inventory')}
                style={{
                  padding: '8px 16px',
                  borderRadius: '8px',
                  border: detailTab === 'inventory' ? '1px solid #52c41a' : '1px solid rgba(255,255,255,0.08)',
                  background: detailTab === 'inventory' ? 'rgba(82, 196, 26, 0.25)' : 'transparent',
                  color: detailTab === 'inventory' ? '#52c41a' : '#aaa',
                  cursor: 'pointer',
                  fontWeight: detailTab === 'inventory' ? '700' : '500',
                  fontSize: '13.5px',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '6px',
                  whiteSpace: 'nowrap'
                }}
              >
                <span>🎒 Hành Trang & Rương Đồ</span>
              </button>
            </div>

            {/* Modal Body / Tab Contents */}
            <div style={{ padding: '24px', overflowY: 'auto', flex: 1, maxHeight: 'calc(90vh - 140px)' }}>
              {detailLoading ? (
                <div style={{ textAlign: 'center', padding: '50px 0', color: '#aaa', fontSize: '16px' }}>
                  🔄 Đang tải toàn bộ dữ liệu chi tiết tài khoản & nhân vật...
                </div>
              ) : !detailData?.account ? (
                <div style={{ textAlign: 'center', padding: '50px 0', color: '#ff4d4f' }}>
                  ⚠️ Không thể tải dữ liệu tài khoản!
                </div>
              ) : (
                <>
                  {/* TAB 1: OVERVIEW & STATS */}
                  {detailTab === 'overview' && (
                    <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
                      {/* Tiền tệ & Tài sản Header Bar */}
                      <div style={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        background: 'linear-gradient(135deg, rgba(146, 84, 222, 0.15) 0%, rgba(20,20,20,0.6) 100%)',
                        border: '1px solid rgba(146, 84, 222, 0.3)',
                        padding: '14px 20px',
                        borderRadius: '12px',
                        flexWrap: 'wrap',
                        gap: '12px'
                      }}>
                        <div>
                          <div style={{ fontSize: '15px', fontWeight: '800', color: '#b37feb', display: 'flex', alignItems: 'center', gap: '8px' }}>
                            <span>💰 QUẢN LÝ TIỀN TỆ & TÀI SẢN</span>
                          </div>
                          <div style={{ fontSize: '12px', color: '#aaa', marginTop: '2px' }}>
                            Điều chỉnh Số Ruby, Số Beri, Web Coin, Tích Nạp & Tổng Nạp
                          </div>
                        </div>
                        <button
                          type="button"
                          onClick={handleOpenCurrencyModal}
                          style={{
                            background: 'linear-gradient(135deg, #1890ff 0%, #722ed1 100%)',
                            border: 'none',
                            color: '#fff',
                            padding: '9px 18px',
                            borderRadius: '8px',
                            fontWeight: 'bold',
                            fontSize: '13px',
                            cursor: 'pointer',
                            boxShadow: '0 4px 14px rgba(114, 46, 209, 0.4)',
                            display: 'flex',
                            alignItems: 'center',
                            gap: '6px',
                            transition: 'all 0.2s'
                          }}
                        >
                          <span>✏️</span>
                          <span>Điều Chỉnh Tài Sản</span>
                        </button>
                      </div>

                      {/* Tiền tệ & Tài sản Grid */}
                      <div style={{
                        display: 'grid',
                        gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))',
                        gap: '14px'
                      }}>
                        {/* Ruby Card */}
                        <div style={{
                          background: 'linear-gradient(135deg, rgba(235, 47, 150, 0.15) 0%, rgba(20,20,20,0.6) 100%)',
                          border: '1px solid rgba(235, 47, 150, 0.3)',
                          borderRadius: '12px',
                          padding: '16px',
                          display: 'flex',
                          justifyContent: 'space-between',
                          alignItems: 'center',
                          gap: '12px'
                        }}>
                          <div style={{ display: 'flex', alignItems: 'center', gap: '12px', minWidth: 0 }}>
                            <div style={{ fontSize: '30px', flexShrink: 0 }}>💎</div>
                            <div style={{ minWidth: 0 }}>
                              <div style={{ fontSize: '12px', color: '#aaa', textTransform: 'uppercase', fontWeight: 'bold' }}>Số Ruby (Kim Cương)</div>
                              <div style={{ fontSize: '20px', fontWeight: '800', color: '#ff85c0', wordBreak: 'break-all' }}>
                                {detailData.player ? detailData.player.ruby.toLocaleString() : '0'} Ruby
                              </div>
                            </div>
                          </div>
                          <button
                            type="button"
                            onClick={handleOpenCurrencyModal}
                            title="Điều chỉnh Ruby"
                            style={{
                              padding: '5px 9px',
                              background: 'rgba(235, 47, 150, 0.2)',
                              border: '1px solid rgba(235, 47, 150, 0.4)',
                              borderRadius: '6px',
                              color: '#ff85c0',
                              cursor: 'pointer',
                              fontSize: '11.5px',
                              fontWeight: 'bold',
                              flexShrink: 0
                            }}
                          >
                            ✏️ Sửa
                          </button>
                        </div>

                        {/* Beri Card */}
                        <div style={{
                          background: 'linear-gradient(135deg, rgba(250, 173, 20, 0.15) 0%, rgba(20,20,20,0.6) 100%)',
                          border: '1px solid rgba(250, 173, 20, 0.3)',
                          borderRadius: '12px',
                          padding: '16px',
                          display: 'flex',
                          justifyContent: 'space-between',
                          alignItems: 'center',
                          gap: '12px'
                        }}>
                          <div style={{ display: 'flex', alignItems: 'center', gap: '12px', minWidth: 0 }}>
                            <div style={{ fontSize: '30px', flexShrink: 0 }}>💰</div>
                            <div style={{ minWidth: 0 }}>
                              <div style={{ fontSize: '12px', color: '#aaa', textTransform: 'uppercase', fontWeight: 'bold' }}>Số Beri (Vàng)</div>
                              <div style={{ fontSize: '20px', fontWeight: '800', color: '#ffd666', wordBreak: 'break-all' }}>
                                {detailData.player ? detailData.player.vang.toLocaleString() : '0'} Beri
                              </div>
                            </div>
                          </div>
                          <button
                            type="button"
                            onClick={handleOpenCurrencyModal}
                            title="Điều chỉnh Beri"
                            style={{
                              padding: '5px 9px',
                              background: 'rgba(250, 173, 20, 0.2)',
                              border: '1px solid rgba(250, 173, 20, 0.4)',
                              borderRadius: '6px',
                              color: '#ffd666',
                              cursor: 'pointer',
                              fontSize: '11.5px',
                              fontWeight: 'bold',
                              flexShrink: 0
                            }}
                          >
                            ✏️ Sửa
                          </button>
                        </div>

                        {/* Web Coin Card */}
                        <div style={{
                          background: 'linear-gradient(135deg, rgba(0, 229, 255, 0.15) 0%, rgba(20,20,20,0.6) 100%)',
                          border: '1px solid rgba(0, 229, 255, 0.3)',
                          borderRadius: '12px',
                          padding: '16px',
                          display: 'flex',
                          justifyContent: 'space-between',
                          alignItems: 'center',
                          gap: '12px'
                        }}>
                          <div style={{ display: 'flex', alignItems: 'center', gap: '12px', minWidth: 0 }}>
                            <div style={{ fontSize: '30px', flexShrink: 0 }}>🪙</div>
                            <div style={{ minWidth: 0 }}>
                              <div style={{ fontSize: '12px', color: '#aaa', textTransform: 'uppercase', fontWeight: 'bold' }}>Web Coin</div>
                              <div style={{ fontSize: '20px', fontWeight: '800', color: '#00e5ff', wordBreak: 'break-all' }}>
                                {detailData.account.coin.toLocaleString()} Coin
                              </div>
                            </div>
                          </div>
                          <button
                            type="button"
                            onClick={handleOpenCurrencyModal}
                            title="Điều chỉnh Web Coin"
                            style={{
                              padding: '5px 9px',
                              background: 'rgba(0, 229, 255, 0.2)',
                              border: '1px solid rgba(0, 229, 255, 0.4)',
                              borderRadius: '6px',
                              color: '#00e5ff',
                              cursor: 'pointer',
                              fontSize: '11.5px',
                              fontWeight: 'bold',
                              flexShrink: 0
                            }}
                          >
                            ✏️ Sửa
                          </button>
                        </div>

                        {/* Tích Nạp Card */}
                        <div style={{
                          background: 'linear-gradient(135deg, rgba(82, 196, 26, 0.15) 0%, rgba(20,20,20,0.6) 100%)',
                          border: '1px solid rgba(82, 196, 26, 0.3)',
                          borderRadius: '12px',
                          padding: '16px',
                          display: 'flex',
                          justifyContent: 'space-between',
                          alignItems: 'center',
                          gap: '12px'
                        }}>
                          <div style={{ display: 'flex', alignItems: 'center', gap: '12px', minWidth: 0 }}>
                            <div style={{ fontSize: '30px', flexShrink: 0 }}>⚡</div>
                            <div style={{ minWidth: 0 }}>
                              <div style={{ fontSize: '12px', color: '#aaa', textTransform: 'uppercase', fontWeight: 'bold' }}>Tích Nạp / Tổng Nạp</div>
                              <div style={{ fontSize: '18px', fontWeight: '800', color: '#73d13d', wordBreak: 'break-all' }}>
                                {detailData.account.tichnap.toLocaleString()}đ / {detailData.account.tongnap.toLocaleString()}đ
                              </div>
                            </div>
                          </div>
                          <button
                            type="button"
                            onClick={handleOpenCurrencyModal}
                            title="Điều chỉnh Tích Nạp & Tổng Nạp"
                            style={{
                              padding: '5px 9px',
                              background: 'rgba(82, 196, 26, 0.2)',
                              border: '1px solid rgba(82, 196, 26, 0.4)',
                              borderRadius: '6px',
                              color: '#73d13d',
                              cursor: 'pointer',
                              fontSize: '11.5px',
                              fontWeight: 'bold',
                              flexShrink: 0
                            }}
                          >
                            ✏️ Sửa
                          </button>
                        </div>
                      </div>

                      {/* Vị trí hiện tại Box */}
                      {detailData.player && (
                        <div style={{
                          background: 'rgba(0, 0, 0, 0.35)',
                          border: '1px solid rgba(0, 229, 255, 0.25)',
                          borderRadius: '12px',
                          padding: '18px 20px',
                          display: 'flex',
                          flexWrap: 'wrap',
                          alignItems: 'center',
                          justifyContent: 'space-between',
                          gap: '16px'
                        }}>
                          <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
                            <div style={{ fontSize: '26px' }}>📍</div>
                            <div>
                              <div style={{ fontSize: '12px', color: '#aaa', textTransform: 'uppercase', fontWeight: 'bold' }}>Vị Trí Hiện Tại</div>
                              <div style={{ fontSize: '16px', fontWeight: 'bold', color: '#00e5ff' }}>
                                {detailData.player.location.mapName} <span style={{ fontSize: '13px', color: '#aaa' }}>(Map ID: {detailData.player.location.mapId})</span>
                              </div>
                            </div>
                          </div>
                          
                          <div style={{ display: 'flex', gap: '16px', flexWrap: 'wrap', fontSize: '13.5px' }}>
                            <div style={{ background: 'rgba(255,255,255,0.05)', padding: '6px 12px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.08)' }}>
                              🚩 Khu vực: <strong style={{ color: '#ffd700' }}>Khu {detailData.player.location.zoneId}</strong>
                            </div>
                            <div style={{ background: 'rgba(255,255,255,0.05)', padding: '6px 12px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.08)' }}>
                              🎯 Tọa độ: <strong style={{ color: '#fff' }}>X: {detailData.player.location.x}, Y: {detailData.player.location.y}</strong>
                            </div>
                            <div style={{ background: 'rgba(255,77,79,0.1)', padding: '6px 12px', borderRadius: '8px', border: '1px solid rgba(255,77,79,0.2)' }}>
                              ❤️ HP: <strong style={{ color: '#ff7875' }}>{detailData.player.location.hp.toLocaleString()}</strong>
                            </div>
                            <div style={{ background: 'rgba(24,144,255,0.1)', padding: '6px 12px', borderRadius: '8px', border: '1px solid rgba(24,144,255,0.2)' }}>
                              💙 MP: <strong style={{ color: '#69c0ff' }}>{detailData.player.location.mp.toLocaleString()}</strong>
                            </div>
                          </div>
                        </div>
                      )}

                      {/* 2 Cột: Thông tin Tài Khoản & Nhân Vật */}
                      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(350px, 1fr))', gap: '20px' }}>
                        {/* Cột 1: Thông tin tài khoản */}
                        <div style={{
                          background: 'rgba(0,0,0,0.3)',
                          border: '1px solid rgba(255,255,255,0.08)',
                          borderRadius: '14px',
                          padding: '20px'
                        }}>
                          <h4 style={{ margin: '0 0 16px 0', color: '#b37feb', fontSize: '15px', fontWeight: 'bold', borderBottom: '1px solid rgba(255,255,255,0.08)', paddingBottom: '10px' }}>
                            👤 THÔNG TIN TÀI KHOẢN
                          </h4>
                          <div style={{ display: 'flex', flexDirection: 'column', gap: '10px', fontSize: '13.5px' }}>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                              <span style={{ color: '#aaa' }}>ID Tài khoản:</span>
                              <strong>#{detailData.account.id}</strong>
                            </div>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                              <span style={{ color: '#aaa' }}>Tên đăng nhập:</span>
                              <strong style={{ color: '#00e5ff' }}>{detailData.account.user}</strong>
                            </div>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                              <span style={{ color: '#aaa' }}>Cấp VIP:</span>
                              <strong style={{ color: '#faad14' }}>👑 VIP {detailData.account.vip}</strong>
                            </div>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                              <span style={{ color: '#aaa' }}>Trạng thái thành viên:</span>
                              <span style={{ color: detailData.account.status === 1 ? '#52c41a' : '#aaa', fontWeight: 'bold' }}>
                                {detailData.account.status === 1 ? 'Đã kích hoạt (MTV)' : 'Chưa kích hoạt'}
                              </span>
                            </div>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                              <span style={{ color: '#aaa' }}>Trạng thái khóa:</span>
                              <span style={{ color: detailData.account.lock === 1 ? '#ff4d4f' : '#52c41a', fontWeight: 'bold' }}>
                                {detailData.account.lock === 1 ? '🔒 BANNED' : '🔓 Bình thường'}
                              </span>
                            </div>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                              <span style={{ color: '#aaa' }}>Trạng thái online:</span>
                              <span style={{ color: detailData.account.onl === 1 ? '#2ecc71' : '#777', fontWeight: 'bold' }}>
                                {detailData.account.onl === 1 ? '🟢 Đang Online' : '⚪ Offline'}
                              </span>
                            </div>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                              <span style={{ color: '#aaa' }}>Địa chỉ IP:</span>
                              <code style={{ background: 'rgba(255,255,255,0.06)', padding: '2px 6px', borderRadius: '4px' }}>
                                {detailData.account.ip_address}
                              </code>
                            </div>
                            {detailData.account.created_at && (
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>Ngày tạo:</span>
                                <span>{new Date(detailData.account.created_at).toLocaleString('vi-VN')}</span>
                              </div>
                            )}
                          </div>
                        </div>

                        {/* Cột 2: Thông tin nhân vật & Hoạt động */}
                        <div style={{
                          background: 'rgba(0,0,0,0.3)',
                          border: '1px solid rgba(255,255,255,0.08)',
                          borderRadius: '14px',
                          padding: '20px'
                        }}>
                          <h4 style={{ margin: '0 0 16px 0', color: '#ff8a00', fontSize: '15px', fontWeight: 'bold', borderBottom: '1px solid rgba(255,255,255,0.08)', paddingBottom: '10px' }}>
                            🎮 THÔNG TIN NHÂN VẬT & HOẠT ĐỘNG
                          </h4>
                          {detailData.player ? (
                            <div style={{ display: 'flex', flexDirection: 'column', gap: '10px', fontSize: '13.5px' }}>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>Tên nhân vật:</span>
                                <strong style={{ color: '#ff8a00' }}>{detailData.player.name}</strong>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>Hệ phái:</span>
                                <strong>🥋 {detailData.player.clazzName}</strong>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>Cấp độ (Level):</span>
                                <strong style={{ color: '#ffd700' }}>Lv. {detailData.player.level}</strong>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>Kinh nghiệm (EXP):</span>
                                <span>{detailData.player.exp.toLocaleString()} EXP</span>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>Điểm thông thạo:</span>
                                <span>{detailData.player.thongthao}</span>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>⚔️ Điểm PVP:</span>
                                <span>{detailData.player.pvppoint} pts (Thắng: <strong style={{ color: '#52c41a' }}>{detailData.player.pvpWin}</strong> / Thua: <strong style={{ color: '#ff4d4f' }}>{detailData.player.pvpLose}</strong>)</span>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>📜 Bounty (Truy nã):</span>
                                <strong style={{ color: '#fa8c16' }}>{detailData.player.wantedPrice.toLocaleString()} Beri</strong>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>🏔️ Tầng Hang Động:</span>
                                <strong style={{ color: '#b37feb' }}>Tầng {detailData.player.hangdong_stage}</strong>
                              </div>
                              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <span style={{ color: '#aaa' }}>🦁 Boss Lân tiêu diệt:</span>
                                <span>{detailData.player.lan_kills} lần</span>
                              </div>
                            </div>
                          ) : (
                            <div style={{ padding: '20px 0', textAlign: 'center', color: '#aaa' }}>
                              ⚠️ Tài khoản này chưa vào game tạo nhân vật
                            </div>
                          )}
                        </div>
                      </div>

                      {/* Tiềm năng nhân vật */}
                      {detailData.player && (
                        <div style={{
                          background: 'rgba(0,0,0,0.3)',
                          border: '1px solid rgba(255,255,255,0.08)',
                          borderRadius: '14px',
                          padding: '20px'
                        }}>
                          <h4 style={{ margin: '0 0 16px 0', color: '#52c41a', fontSize: '15px', fontWeight: 'bold', borderBottom: '1px solid rgba(255,255,255,0.08)', paddingBottom: '10px' }}>
                            ⚡ ĐIỂM TIỀM NĂNG (Thuộc tính nhân vật)
                          </h4>
                          <div style={{
                            display: 'grid',
                            gridTemplateColumns: 'repeat(auto-fit, minmax(140px, 1fr))',
                            gap: '12px'
                          }}>
                            <div style={{ background: 'rgba(255,255,255,0.03)', padding: '10px 14px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.06)' }}>
                              <div style={{ fontSize: '12px', color: '#aaa' }}>Điểm chưa cộng</div>
                              <div style={{ fontSize: '18px', fontWeight: 'bold', color: '#ffd700' }}>{detailData.player.potential.pointsRemaining}</div>
                            </div>
                            <div style={{ background: 'rgba(255,255,255,0.03)', padding: '10px 14px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.06)' }}>
                              <div style={{ fontSize: '12px', color: '#aaa' }}>Sức mạnh (ST Vật lý)</div>
                              <div style={{ fontSize: '18px', fontWeight: 'bold', color: '#ff7875' }}>{detailData.player.potential.sucManh}</div>
                            </div>
                            <div style={{ background: 'rgba(255,255,255,0.03)', padding: '10px 14px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.06)' }}>
                              <div style={{ fontSize: '12px', color: '#aaa' }}>Nhanh nhẹn (Bạo kích)</div>
                              <div style={{ fontSize: '18px', fontWeight: 'bold', color: '#69c0ff' }}>{detailData.player.potential.nhanhNhen}</div>
                            </div>
                            <div style={{ background: 'rgba(255,255,255,0.03)', padding: '10px 14px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.06)' }}>
                              <div style={{ fontSize: '12px', color: '#aaa' }}>Thể lực (Máu HP)</div>
                              <div style={{ fontSize: '18px', fontWeight: 'bold', color: '#95de64' }}>{detailData.player.potential.theLuc}</div>
                            </div>
                            <div style={{ background: 'rgba(255,255,255,0.03)', padding: '10px 14px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.06)' }}>
                              <div style={{ fontSize: '12px', color: '#aaa' }}>Tinh thần (Mana MP)</div>
                              <div style={{ fontSize: '18px', fontWeight: 'bold', color: '#b37feb' }}>{detailData.player.potential.tinhThan}</div>
                            </div>
                            <div style={{ background: 'rgba(255,255,255,0.03)', padding: '10px 14px', borderRadius: '8px', border: '1px solid rgba(255,255,255,0.06)' }}>
                              <div style={{ fontSize: '12px', color: '#aaa' }}>Phòng thủ (Giáp)</div>
                              <div style={{ fontSize: '18px', fontWeight: 'bold', color: '#fa8c16' }}>{detailData.player.potential.phongThu}</div>
                            </div>
                          </div>
                        </div>
                      )}
                    </div>
                  )}

                  {/* TAB 2: MỐC NẠP */}
                  {detailTab === 'milestones' && (
                    <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
                      <div style={{
                        background: 'rgba(0,0,0,0.3)',
                        border: '1px solid rgba(250, 173, 20, 0.3)',
                        borderRadius: '14px',
                        padding: '20px',
                        display: 'flex',
                        flexWrap: 'wrap',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        gap: '16px'
                      }}>
                        <div>
                          <div style={{ fontSize: '13px', color: '#aaa', textTransform: 'uppercase', fontWeight: 'bold' }}>Tiến Trình Tích Lũy Nạp</div>
                          <div style={{ fontSize: '24px', fontWeight: '800', color: '#faad14' }}>
                            {detailData.account.tichnap.toLocaleString()}đ <span style={{ fontSize: '14px', color: '#aaa', fontWeight: 'normal' }}>/ Tổng nạp: {detailData.account.tongnap.toLocaleString()}đ</span>
                          </div>
                        </div>
                        <div style={{ fontSize: '13.5px', color: '#aaa' }}>
                          Mốc đã nhận: <strong style={{ color: '#52c41a' }}>{detailData.account.claimed_milestones || 'Chưa nhận'}</strong>
                        </div>
                      </div>

                      <div style={{
                        display: 'grid',
                        gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))',
                        gap: '16px'
                      }}>
                        {detailData.account.milestones.map((m, idx) => {
                          const percent = Math.min(100, Math.round((detailData.account.tichnap / m.num) * 100));
                          return (
                            <div key={idx} style={{
                              background: m.isClaimed 
                                ? 'linear-gradient(135deg, rgba(82, 196, 26, 0.1) 0%, rgba(20,20,20,0.6) 100%)' 
                                : m.canClaim 
                                  ? 'linear-gradient(135deg, rgba(250, 173, 20, 0.15) 0%, rgba(20,20,20,0.6) 100%)'
                                  : 'rgba(255,255,255,0.02)',
                              border: m.isClaimed 
                                ? '1px solid rgba(82, 196, 26, 0.35)' 
                                : m.canClaim 
                                  ? '1px solid rgba(250, 173, 20, 0.45)'
                                  : '1px solid rgba(255,255,255,0.06)',
                              borderRadius: '12px',
                              padding: '18px',
                              display: 'flex',
                              flexDirection: 'column',
                              gap: '10px'
                            }}>
                              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                                <span style={{ fontWeight: 'bold', fontSize: '15px', color: m.isClaimed ? '#52c41a' : m.canClaim ? '#faad14' : '#eee' }}>
                                  Mốc {idx + 1}: {m.label}
                                </span>
                                {m.isClaimed ? (
                                  <span style={{ fontSize: '11.5px', padding: '2px 8px', borderRadius: '4px', background: 'rgba(82,196,26,0.2)', color: '#52c41a', border: '1px solid rgba(82,196,26,0.3)', fontWeight: 'bold' }}>
                                    ✓ ĐÃ NHẬN
                                  </span>
                                ) : m.canClaim ? (
                                  <span style={{ fontSize: '11.5px', padding: '2px 8px', borderRadius: '4px', background: 'rgba(250,173,20,0.2)', color: '#faad14', border: '1px solid rgba(250,173,20,0.3)', fontWeight: 'bold' }}>
                                    ⚡ CÓ THỂ NHẬN
                                  </span>
                                ) : (
                                  <span style={{ fontSize: '11.5px', padding: '2px 8px', borderRadius: '4px', background: 'rgba(255,255,255,0.05)', color: '#777', border: '1px solid rgba(255,255,255,0.1)' }}>
                                    🔒 CHƯA ĐẠT
                                  </span>
                                )}
                              </div>

                              {/* Progress bar */}
                              <div style={{ background: 'rgba(0,0,0,0.4)', borderRadius: '6px', height: '8px', overflow: 'hidden', border: '1px solid rgba(255,255,255,0.05)' }}>
                                <div style={{
                                  width: `${percent}%`,
                                  height: '100%',
                                  background: m.isClaimed ? '#52c41a' : m.canClaim ? '#faad14' : '#1890ff',
                                  transition: 'width 0.3s ease'
                                }} />
                              </div>

                              <div style={{ display: 'flex', justifyContent: 'space-between', fontSize: '12px', color: '#888' }}>
                                <span>Tiến độ: {percent}%</span>
                                <span>{m.isReached ? 'Đã đạt mốc' : `Còn thiếu ${(m.num - detailData.account.tichnap).toLocaleString()}đ`}</span>
                              </div>
                            </div>
                          );
                        })}
                      </div>
                    </div>
                  )}

                  {/* TAB 3: TRANG BỊ ĐANG MẶC */}
                  {detailTab === 'equipment' && (
                    <div>
                      {!detailData.player ? (
                        <div style={{ textAlign: 'center', padding: '40px', color: '#aaa' }}>
                          ⚠️ Tài khoản chưa tạo nhân vật nên chưa có trang bị.
                        </div>
                      ) : detailData.player.equippedItems.length === 0 ? (
                        <div style={{ textAlign: 'center', padding: '40px', color: '#aaa' }}>
                          👕 Nhân vật hiện không mặc trang bị nào trên người.
                        </div>
                      ) : (
                        <div style={{
                          display: 'grid',
                          gridTemplateColumns: 'repeat(auto-fit, minmax(260px, 1fr))',
                          gap: '16px'
                        }}>
                          {detailData.player.equippedItems.map((it, idx) => (
                            <div key={idx} style={{
                              background: 'rgba(0,0,0,0.3)',
                              border: `1px solid ${it.colorMeta.color}40`,
                              borderRadius: '12px',
                              padding: '16px',
                              boxShadow: `0 4px 16px ${it.colorMeta.color}15`,
                              display: 'flex',
                              flexDirection: 'column',
                              gap: '8px'
                            }}>
                              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
                                <div>
                                  <div style={{ fontSize: '15px', fontWeight: 'bold', color: it.colorMeta.color }}>
                                    {it.name} {it.levelup > 0 && <span style={{ color: '#ffd700' }}>+{it.levelup}</span>}
                                    {it.isHoanMy === 1 && <span style={{ fontSize: '11px', color: '#ff4d4f', marginLeft: '6px' }}>[Hoàn Mỹ]</span>}
                                  </div>
                                  <div style={{ fontSize: '12px', color: '#aaa', marginTop: '2px' }}>
                                    {it.typeEquipName} (ID: #{it.templateId})
                                  </div>
                                </div>
                                <span style={{ 
                                  fontSize: '11px', 
                                  padding: '2px 6px', 
                                  borderRadius: '4px', 
                                  background: `${it.colorMeta.color}20`, 
                                  color: it.colorMeta.color,
                                  border: `1px solid ${it.colorMeta.color}50`,
                                  fontWeight: '600'
                                }}>
                                  {it.colorMeta.text}
                                </span>
                              </div>

                              {/* Options */}
                              {it.options && it.options.length > 0 && (
                                <div style={{
                                  background: 'rgba(0,0,0,0.3)',
                                  borderRadius: '6px',
                                  padding: '8px 10px',
                                  fontSize: '12.5px',
                                  display: 'flex',
                                  flexDirection: 'column',
                                  gap: '4px',
                                  marginTop: '4px'
                                }}>
                                  <div style={{ fontSize: '11px', color: '#777', textTransform: 'uppercase', fontWeight: 'bold' }}>Chỉ số thuộc tính:</div>
                                  {it.options.map((op, opIdx) => {
                                    const opName = OPTION_NAMES[op.id] || `Thuộc tính #${op.id}`;
                                    return (
                                      <div key={opIdx} style={{ color: '#69c0ff', display: 'flex', justifyContent: 'space-between' }}>
                                        <span>• {opName}:</span>
                                        <strong>+{op.param}</strong>
                                      </div>
                                    );
                                  })}
                                </div>
                              )}
                            </div>
                          ))}
                        </div>
                      )}
                    </div>
                  )}

                  {/* TAB 4: HÀNH TRANG & RƯƠNG ĐỒ */}
                  {detailTab === 'inventory' && (
                    <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
                      {!detailData.player ? (
                        <div style={{ textAlign: 'center', padding: '40px', color: '#aaa' }}>
                          ⚠️ Tài khoản chưa tạo nhân vật nên chưa có hành trang.
                        </div>
                      ) : (
                        <>
                          {/* Sub-tab switcher */}
                          <div style={{ display: 'flex', gap: '8px', flexWrap: 'wrap', borderBottom: '1px solid rgba(255,255,255,0.06)', paddingBottom: '12px' }}>
                            <button
                              onClick={() => setInvSubTab('bag3')}
                              style={{
                                padding: '6px 14px',
                                borderRadius: '6px',
                                border: invSubTab === 'bag3' ? '1px solid #00e5ff' : '1px solid rgba(255,255,255,0.08)',
                                background: invSubTab === 'bag3' ? 'rgba(0, 229, 255, 0.2)' : 'rgba(0,0,0,0.3)',
                                color: invSubTab === 'bag3' ? '#00e5ff' : '#aaa',
                                cursor: 'pointer',
                                fontSize: '13px',
                                fontWeight: invSubTab === 'bag3' ? 'bold' : 'normal'
                              }}
                            >
                              🗡️ Túi Trang Bị ({detailData.player.bagItems.length})
                            </button>
                            <button
                              onClick={() => setInvSubTab('bag47')}
                              style={{
                                padding: '6px 14px',
                                borderRadius: '6px',
                                border: invSubTab === 'bag47' ? '1px solid #52c41a' : '1px solid rgba(255,255,255,0.08)',
                                background: invSubTab === 'bag47' ? 'rgba(82, 196, 26, 0.2)' : 'rgba(0,0,0,0.3)',
                                color: invSubTab === 'bag47' ? '#52c41a' : '#aaa',
                                cursor: 'pointer',
                                fontSize: '13px',
                                fontWeight: invSubTab === 'bag47' ? 'bold' : 'normal'
                              }}
                            >
                              🧪 Túi Dược Phẩm & Đá ({detailData.player.bagSupplies.length})
                            </button>
                            <button
                              onClick={() => setInvSubTab('box3')}
                              style={{
                                padding: '6px 14px',
                                borderRadius: '6px',
                                border: invSubTab === 'box3' ? '1px solid #faad14' : '1px solid rgba(255,255,255,0.08)',
                                background: invSubTab === 'box3' ? 'rgba(250, 173, 20, 0.2)' : 'rgba(0,0,0,0.3)',
                                color: invSubTab === 'box3' ? '#faad14' : '#aaa',
                                cursor: 'pointer',
                                fontSize: '13px',
                                fontWeight: invSubTab === 'box3' ? 'bold' : 'normal'
                              }}
                            >
                              📦 Rương Trang Bị ({detailData.player.boxItems.length})
                            </button>
                            <button
                              onClick={() => setInvSubTab('box47')}
                              style={{
                                padding: '6px 14px',
                                borderRadius: '6px',
                                border: invSubTab === 'box47' ? '1px solid #b37feb' : '1px solid rgba(255,255,255,0.08)',
                                background: invSubTab === 'box47' ? 'rgba(146, 84, 222, 0.2)' : 'rgba(0,0,0,0.3)',
                                color: invSubTab === 'box47' ? '#b37feb' : '#aaa',
                                cursor: 'pointer',
                                fontSize: '13px',
                                fontWeight: invSubTab === 'box47' ? 'bold' : 'normal'
                              }}
                            >
                              🗄️ Rương Vật Phẩm ({detailData.player.boxSupplies.length})
                            </button>
                          </div>

                          {/* Bag3: Trang bị trong túi */}
                          {invSubTab === 'bag3' && (
                            detailData.player.bagItems.length === 0 ? (
                              <div style={{ textAlign: 'center', padding: '30px', color: '#777' }}>Túi trang bị trống</div>
                            ) : (
                              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(240px, 1fr))', gap: '12px' }}>
                                {detailData.player.bagItems.map((it, idx) => (
                                  <div key={idx} style={{ background: 'rgba(0,0,0,0.3)', border: `1px solid ${it.colorMeta.color}35`, borderRadius: '10px', padding: '12px', fontSize: '13px' }}>
                                    <div style={{ fontWeight: 'bold', color: it.colorMeta.color }}>{it.name} {it.levelup > 0 && `+${it.levelup}`}</div>
                                    <div style={{ fontSize: '11.5px', color: '#888', marginTop: '2px' }}>{it.typeEquipName} • Phẩm chất: {it.colorMeta.text}</div>
                                  </div>
                                ))}
                              </div>
                            )
                          )}

                          {/* Bag47: Dược phẩm & đá */}
                          {invSubTab === 'bag47' && (
                            detailData.player.bagSupplies.length === 0 ? (
                              <div style={{ textAlign: 'center', padding: '30px', color: '#777' }}>Túi vật phẩm trống</div>
                            ) : (
                              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))', gap: '12px' }}>
                                {detailData.player.bagSupplies.map((it, idx) => (
                                  <div key={idx} style={{ background: 'rgba(0,0,0,0.3)', border: '1px solid rgba(255,255,255,0.08)', borderRadius: '10px', padding: '12px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', fontSize: '13px' }}>
                                    <div>
                                      <div style={{ fontWeight: '600', color: '#52c41a' }}>{it.name}</div>
                                      <div style={{ fontSize: '11px', color: '#777' }}>{it.catName} (ID: #{it.id})</div>
                                    </div>
                                    <span style={{ background: 'rgba(82,196,26,0.15)', color: '#52c41a', padding: '2px 8px', borderRadius: '6px', fontWeight: 'bold' }}>
                                      x{it.quant.toLocaleString()}
                                    </span>
                                  </div>
                                ))}
                              </div>
                            )
                          )}

                          {/* Box3: Rương trang bị */}
                          {invSubTab === 'box3' && (
                            detailData.player.boxItems.length === 0 ? (
                              <div style={{ textAlign: 'center', padding: '30px', color: '#777' }}>Rương trang bị trống</div>
                            ) : (
                              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(240px, 1fr))', gap: '12px' }}>
                                {detailData.player.boxItems.map((it, idx) => (
                                  <div key={idx} style={{ background: 'rgba(0,0,0,0.3)', border: `1px solid ${it.colorMeta.color}35`, borderRadius: '10px', padding: '12px', fontSize: '13px' }}>
                                    <div style={{ fontWeight: 'bold', color: it.colorMeta.color }}>{it.name} {it.levelup > 0 && `+${it.levelup}`}</div>
                                    <div style={{ fontSize: '11.5px', color: '#888', marginTop: '2px' }}>{it.typeEquipName} • Phẩm chất: {it.colorMeta.text}</div>
                                  </div>
                                ))}
                              </div>
                            )
                          )}

                          {/* Box47: Rương vật phẩm */}
                          {invSubTab === 'box47' && (
                            detailData.player.boxSupplies.length === 0 ? (
                              <div style={{ textAlign: 'center', padding: '30px', color: '#777' }}>Rương vật phẩm trống</div>
                            ) : (
                              <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))', gap: '12px' }}>
                                {detailData.player.boxSupplies.map((it, idx) => (
                                  <div key={idx} style={{ background: 'rgba(0,0,0,0.3)', border: '1px solid rgba(255,255,255,0.08)', borderRadius: '10px', padding: '12px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', fontSize: '13px' }}>
                                    <div>
                                      <div style={{ fontWeight: '600', color: '#b37feb' }}>{it.name}</div>
                                      <div style={{ fontSize: '11px', color: '#777' }}>{it.catName} (ID: #{it.id})</div>
                                    </div>
                                    <span style={{ background: 'rgba(146,84,222,0.15)', color: '#b37feb', padding: '2px 8px', borderRadius: '6px', fontWeight: 'bold' }}>
                                      x{it.quant.toLocaleString()}
                                    </span>
                                  </div>
                                ))}
                              </div>
                            )
                          )}
                        </>
                      )}
                    </div>
                  )}
                </>
              )}
            </div>
          </div>
        </div>
      )}

      {/* Modal Điều Chỉnh Tiền Tệ & Tài Sản */}
      {currencyModalOpen && (
        <div style={{
          position: 'fixed',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          backgroundColor: 'rgba(0,0,0,0.85)',
          backdropFilter: 'blur(10px)',
          zIndex: 2200,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          padding: '20px'
        }}>
          <div style={{
            background: 'linear-gradient(135deg, #161616 0%, #1f1f1f 100%)',
            border: '1px solid rgba(146, 84, 222, 0.5)',
            borderRadius: '16px',
            padding: '28px',
            maxWidth: '560px',
            width: '100%',
            boxShadow: '0 25px 60px rgba(0, 0, 0, 0.9), 0 0 35px rgba(146, 84, 222, 0.25)',
            color: '#fff',
            position: 'relative',
            maxHeight: '90vh',
            overflowY: 'auto'
          }}>
            {/* Header */}
            <div style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              marginBottom: '20px',
              borderBottom: '1px solid rgba(255,255,255,0.08)',
              paddingBottom: '14px'
            }}>
              <h4 style={{
                margin: 0,
                color: '#b37feb',
                fontSize: '18px',
                fontWeight: '800',
                display: 'flex',
                alignItems: 'center',
                gap: '8px'
              }}>
                <span>✏️</span>
                <span>ĐIỀU CHỈNH TIỀN TỆ & TÀI SẢN</span>
              </h4>
              <button
                type="button"
                onClick={() => setCurrencyModalOpen(false)}
                style={{
                  background: 'rgba(255,255,255,0.05)',
                  border: '1px solid rgba(255,255,255,0.1)',
                  borderRadius: '50%',
                  width: '32px',
                  height: '32px',
                  color: '#aaa',
                  fontSize: '16px',
                  cursor: 'pointer',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center'
                }}
              >
                ✕
              </button>
            </div>

            {/* Account Info Summary */}
            <div style={{
              background: 'rgba(0,0,0,0.4)',
              padding: '12px 16px',
              borderRadius: '10px',
              marginBottom: '20px',
              fontSize: '13.5px',
              border: '1px solid rgba(255,255,255,0.08)',
              display: 'flex',
              justifyContent: 'space-between',
              flexWrap: 'wrap',
              gap: '10px'
            }}>
              <div>👤 Tài khoản: <strong style={{ color: '#00e5ff' }}>{currencyData.username}</strong></div>
              <div>🎮 Nhân vật: <strong style={{ color: '#ff8a00' }}>{currencyData.charName || '(Chưa tạo nhân vật)'}</strong></div>
            </div>

            <form onSubmit={handleSaveCurrency} style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
              {/* Ruby */}
              <div style={{
                background: 'rgba(235, 47, 150, 0.06)',
                padding: '14px',
                borderRadius: '10px',
                border: '1px solid rgba(235, 47, 150, 0.25)'
              }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '8px' }}>
                  <label style={{ fontSize: '13.5px', fontWeight: 'bold', color: '#ff85c0' }}>
                    💎 Số Ruby (Kim Cương):
                  </label>
                  <span style={{ fontSize: '12.5px', color: '#ff85c0', fontWeight: 'bold' }}>
                    {Number(currencyData.ruby || 0).toLocaleString()} Ruby
                  </span>
                </div>
                <input
                  type="number"
                  min="0"
                  disabled={!currencyData.hasPlayer}
                  placeholder="Nhập số Ruby..."
                  value={currencyData.ruby}
                  onChange={(e) => setCurrencyData({ ...currencyData, ruby: e.target.value })}
                  style={{
                    width: '100%',
                    padding: '10px 14px',
                    borderRadius: '8px',
                    border: '1px solid rgba(235, 47, 150, 0.4)',
                    background: 'rgba(0,0,0,0.5)',
                    color: '#fff',
                    fontSize: '15px',
                    fontWeight: 'bold',
                    outline: 'none',
                    boxSizing: 'border-box'
                  }}
                />
                {!currencyData.hasPlayer && (
                  <div style={{ fontSize: '11.5px', color: '#ff7875', marginTop: '4px' }}>
                    ⚠️ Tài khoản chưa vào game tạo nhân vật nên không thể chỉnh Ruby
                  </div>
                )}
              </div>

              {/* Beri */}
              <div style={{
                background: 'rgba(250, 173, 20, 0.06)',
                padding: '14px',
                borderRadius: '10px',
                border: '1px solid rgba(250, 173, 20, 0.25)'
              }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '8px' }}>
                  <label style={{ fontSize: '13.5px', fontWeight: 'bold', color: '#ffd666' }}>
                    💰 Số Beri (Vàng):
                  </label>
                  <span style={{ fontSize: '12.5px', color: '#ffd666', fontWeight: 'bold' }}>
                    {Number(currencyData.vang || 0).toLocaleString()} Beri
                  </span>
                </div>
                <input
                  type="number"
                  min="0"
                  disabled={!currencyData.hasPlayer}
                  placeholder="Nhập số Beri..."
                  value={currencyData.vang}
                  onChange={(e) => setCurrencyData({ ...currencyData, vang: e.target.value })}
                  style={{
                    width: '100%',
                    padding: '10px 14px',
                    borderRadius: '8px',
                    border: '1px solid rgba(250, 173, 20, 0.4)',
                    background: 'rgba(0,0,0,0.5)',
                    color: '#fff',
                    fontSize: '15px',
                    fontWeight: 'bold',
                    outline: 'none',
                    boxSizing: 'border-box'
                  }}
                />
                {!currencyData.hasPlayer && (
                  <div style={{ fontSize: '11.5px', color: '#ff7875', marginTop: '4px' }}>
                    ⚠️ Tài khoản chưa vào game tạo nhân vật nên không thể chỉnh Beri
                  </div>
                )}
              </div>

              {/* Web Coin */}
              <div style={{
                background: 'rgba(0, 229, 255, 0.06)',
                padding: '14px',
                borderRadius: '10px',
                border: '1px solid rgba(0, 229, 255, 0.25)'
              }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '8px' }}>
                  <label style={{ fontSize: '13.5px', fontWeight: 'bold', color: '#00e5ff' }}>
                    🪙 Web Coin:
                  </label>
                  <span style={{ fontSize: '12.5px', color: '#00e5ff', fontWeight: 'bold' }}>
                    {Number(currencyData.coin || 0).toLocaleString()} Coin
                  </span>
                </div>
                <input
                  type="number"
                  min="0"
                  placeholder="Nhập số Web Coin..."
                  value={currencyData.coin}
                  onChange={(e) => setCurrencyData({ ...currencyData, coin: e.target.value })}
                  style={{
                    width: '100%',
                    padding: '10px 14px',
                    borderRadius: '8px',
                    border: '1px solid rgba(0, 229, 255, 0.4)',
                    background: 'rgba(0,0,0,0.5)',
                    color: '#fff',
                    fontSize: '15px',
                    fontWeight: 'bold',
                    outline: 'none',
                    boxSizing: 'border-box'
                  }}
                />
              </div>

              {/* Tích Nạp & Tổng Nạp Grid */}
              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '14px' }}>
                {/* Tích Nạp */}
                <div style={{
                  background: 'rgba(82, 196, 26, 0.06)',
                  padding: '14px',
                  borderRadius: '10px',
                  border: '1px solid rgba(82, 196, 26, 0.25)'
                }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '8px' }}>
                    <label style={{ fontSize: '13px', fontWeight: 'bold', color: '#73d13d' }}>
                      ⚡ Tích Nạp (VNĐ):
                    </label>
                  </div>
                  <input
                    type="number"
                    min="0"
                    placeholder="Tích nạp..."
                    value={currencyData.tichnap}
                    onChange={(e) => setCurrencyData({ ...currencyData, tichnap: e.target.value })}
                    style={{
                      width: '100%',
                      padding: '10px 14px',
                      borderRadius: '8px',
                      border: '1px solid rgba(82, 196, 26, 0.4)',
                      background: 'rgba(0,0,0,0.5)',
                      color: '#fff',
                      fontSize: '15px',
                      fontWeight: 'bold',
                      outline: 'none',
                      boxSizing: 'border-box'
                    }}
                  />
                  <div style={{ fontSize: '12px', color: '#73d13d', marginTop: '4px', fontWeight: '600' }}>
                    {Number(currencyData.tichnap || 0).toLocaleString()}đ
                  </div>
                </div>

                {/* Tổng Nạp */}
                <div style={{
                  background: 'rgba(82, 196, 26, 0.06)',
                  padding: '14px',
                  borderRadius: '10px',
                  border: '1px solid rgba(82, 196, 26, 0.25)'
                }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '8px' }}>
                    <label style={{ fontSize: '13px', fontWeight: 'bold', color: '#73d13d' }}>
                      ⚡ Tổng Nạp (VNĐ):
                    </label>
                  </div>
                  <input
                    type="number"
                    min="0"
                    placeholder="Tổng nạp..."
                    value={currencyData.tongnap}
                    onChange={(e) => setCurrencyData({ ...currencyData, tongnap: e.target.value })}
                    style={{
                      width: '100%',
                      padding: '10px 14px',
                      borderRadius: '8px',
                      border: '1px solid rgba(82, 196, 26, 0.4)',
                      background: 'rgba(0,0,0,0.5)',
                      color: '#fff',
                      fontSize: '15px',
                      fontWeight: 'bold',
                      outline: 'none',
                      boxSizing: 'border-box'
                    }}
                  />
                  <div style={{ fontSize: '12px', color: '#73d13d', marginTop: '4px', fontWeight: '600' }}>
                    {Number(currencyData.tongnap || 0).toLocaleString()}đ
                  </div>
                </div>
              </div>

              {/* Cấp VIP */}
              <div style={{
                background: 'rgba(250, 173, 20, 0.06)',
                padding: '14px',
                borderRadius: '10px',
                border: '1px solid rgba(250, 173, 20, 0.25)'
              }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '8px' }}>
                  <label style={{ fontSize: '13.5px', fontWeight: 'bold', color: '#faad14' }}>
                    👑 Cấp VIP:
                  </label>
                  <label style={{ display: 'flex', alignItems: 'center', gap: '6px', fontSize: '12.5px', color: '#aaa', cursor: 'pointer' }}>
                    <input
                      type="checkbox"
                      checked={currencyData.autoVip}
                      onChange={(e) => setCurrencyData({ ...currencyData, autoVip: e.target.checked })}
                      style={{ accentColor: '#faad14', width: '16px', height: '16px' }}
                    />
                    <span>Tự động tính VIP theo Tổng Nạp</span>
                  </label>
                </div>
                {!currencyData.autoVip ? (
                  <select
                    value={currencyData.vip}
                    onChange={(e) => setCurrencyData({ ...currencyData, vip: Number(e.target.value) })}
                    style={{
                      width: '100%',
                      padding: '10px 14px',
                      borderRadius: '8px',
                      border: '1px solid rgba(250, 173, 20, 0.4)',
                      background: '#222',
                      color: '#fff',
                      fontSize: '14px',
                      fontWeight: 'bold',
                      outline: 'none'
                    }}
                  >
                    {[0,1,2,3,4,5,6,7,8,9,10].map(v => (
                      <option key={v} value={v}>VIP {v}</option>
                    ))}
                  </select>
                ) : (
                  <div style={{ fontSize: '12px', color: '#aaa' }}>
                    💡 Hệ thống sẽ tự động cập nhật VIP tương ứng với số tiền Tổng Nạp.
                  </div>
                )}
              </div>

              {/* Tip info */}
              <div style={{
                fontSize: '12px',
                color: '#fa8c16',
                background: 'rgba(250, 140, 22, 0.1)',
                padding: '10px 14px',
                borderRadius: '8px',
                border: '1px solid rgba(250, 140, 22, 0.25)',
                lineHeight: '1.5'
              }}>
                💡 <strong>Lưu ý:</strong> Nếu tài khoản đang Online trong game, nhân vật cần thoát game và đăng nhập lại để cập nhật số Ruby & Beri hiển thị trong game.
              </div>

              {/* Form Action Buttons */}
              <div style={{ display: 'flex', justifyContent: 'flex-end', gap: '12px', marginTop: '8px' }}>
                <button
                  type="button"
                  onClick={() => setCurrencyModalOpen(false)}
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
                  disabled={submittingCurrency}
                  style={{
                    padding: '10px 24px',
                    borderRadius: '8px',
                    border: 'none',
                    background: 'linear-gradient(135deg, #1890ff 0%, #722ed1 100%)',
                    color: '#fff',
                    fontWeight: 'bold',
                    fontSize: '14px',
                    cursor: submittingCurrency ? 'not-allowed' : 'pointer',
                    boxShadow: '0 4px 15px rgba(114, 46, 209, 0.4)'
                  }}
                >
                  {submittingCurrency ? 'Đang lưu...' : '💾 Lưu Thay Đổi'}
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
