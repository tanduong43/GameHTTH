import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useOutletContext } from 'react-router-dom';

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
  const [totalPages, setTotalPages] = useState(1);
  const [total, setTotal] = useState(0);

  const fetchLogs = async () => {
    try {
      setLoading(true);
      const token = localStorage.getItem('token');
      const queryParams = new URLSearchParams({
        page,
        limit: 20,
        ...filters
      });
      
      const response = await axios.get(`/api/admin/player-logs?${queryParams}`, {
        headers: { Authorization: `Bearer ${token}` }
      });
      
      if (response.data.success) {
        setLogs(response.data.data);
        setTotalPages(response.data.totalPages);
        setTotal(response.data.total);
      } else {
        showMessage('danger', response.data.message || 'L\u1ED7i t\u1EA3i danh s\u00E1ch l\u1ECBch s\u1EED');
      }
    } catch (error) {
      showMessage('danger', 'L\u1ED7i k\u1EBFt n\u1ED1i đ\u1EBFn server');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchLogs();
    // eslint-disable-next-line
  }, [page]);

  const handleSearch = (e) => {
    e.preventDefault();
    setPage(1);
    fetchLogs();
  };

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({ ...prev, [name]: value }));
  };

  return (
    <div className="admin-page">
      <div className="admin-header">
        <h2>L\u1ECBch s\u1EED Ng\u01B0\u1EDDi ch\u01A1i</h2>
        <p>Ki\u1EC3m tra tiêu Ruby, nh\u1EADt item, buff...</p>
      </div>

      <div className="glass-panel" style={{ padding: '20px', marginBottom: '20px' }}>
        <form onSubmit={handleSearch} style={{ display: 'flex', gap: '15px', flexWrap: 'wrap', alignItems: 'flex-end' }}>
          <div style={{ flex: '1', minWidth: '200px' }}>
            <label style={{ display: 'block', marginBottom: '5px' }}>Tên nhân v\u1EADt</label>
            <input 
              type="text" 
              name="playerName"
              value={filters.playerName}
              onChange={handleFilterChange}
              className="form-control" 
              placeholder="Nh\u1EADp tên nhân v\u1EADt..."
            />
          </div>
          <div style={{ flex: '1', minWidth: '150px' }}>
            <label style={{ display: 'block', marginBottom: '5px' }}>T\u1EEB ngày</label>
            <input 
              type="date" 
              name="startDate"
              value={filters.startDate}
              onChange={handleFilterChange}
              className="form-control" 
            />
          </div>
          <div style={{ flex: '1', minWidth: '150px' }}>
            <label style={{ display: 'block', marginBottom: '5px' }}>Đ\u1EBFn ngày</label>
            <input 
              type="date" 
              name="endDate"
              value={filters.endDate}
              onChange={handleFilterChange}
              className="form-control" 
            />
          </div>
          <div style={{ flex: '1', minWidth: '150px' }}>
            <label style={{ display: 'block', marginBottom: '5px' }}>Lo\u1EA1i</label>
            <select 
              name="type" 
              value={filters.type}
              onChange={handleFilterChange}
              className="form-control"
            >
              <option value="">T\u1EA5t c\u1EA3</option>
              <option value="ruby">Ruby</option>
              <option value="item">V\u1EADt ph\u1EA9m</option>
              <option value="buff">Buff</option>
            </select>
          </div>
          <button type="submit" className="btn btn-primary" style={{ padding: '10px 20px', height: '42px' }}>
            🔍 Tìm ki\u1EBFm
          </button>
        </form>
      </div>

      <div className="glass-panel" style={{ padding: '20px' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '15px' }}>
          <h3>Danh s\u00E1ch L\u1ECBch s\u1EED ({total})</h3>
        </div>

        <div className="table-responsive">
          <table className="admin-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Th\u1EDDi gian</th>
                <th>Nhân v\u1EADt</th>
                <th>Lo\u1EA1i</th>
                <th>Chi ti\u1EBFt hành đ\u1ED9ng</th>
              </tr>
            </thead>
            <tbody>
              {loading ? (
                <tr><td colSpan="5" style={{ textAlign: 'center' }}>Đang t\u1EA3i d\u1EEF li\u1EC7u...</td></tr>
              ) : logs.length === 0 ? (
                <tr><td colSpan="5" style={{ textAlign: 'center' }}>Không tìm th\u1EA5y l\u1ECBch s\u1EED nào.</td></tr>
              ) : (
                logs.map(log => (
                  <tr key={log.id}>
                    <td>#{log.id}</td>
                    <td>{new Date(log.created_at).toLocaleString('vi-VN')}</td>
                    <td><strong style={{ color: '#ffb347' }}>{log.player_name}</strong></td>
                    <td>
                      <span className="badge" style={{
                        background: log.type === 'ruby' ? '#ff3366' : 
                                    log.type === 'item' ? '#00cc66' : '#9933ff',
                        padding: '4px 8px', borderRadius: '4px', fontSize: '12px'
                      }}>
                        {log.type.toUpperCase()}
                      </span>
                    </td>
                    <td>{log.action}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>

        {totalPages > 1 && (
          <div className="pagination" style={{ display: 'flex', justifyContent: 'center', gap: '5px', marginTop: '20px' }}>
            <button 
              className="btn btn-outline" 
              disabled={page === 1}
              onClick={() => setPage(p => p - 1)}
            >
              &laquo; Tr\u01B0\u1EDBc
            </button>
            <span style={{ padding: '8px 15px', background: '#333', borderRadius: '4px' }}>
              Trang {page} / {totalPages}
            </span>
            <button 
              className="btn btn-outline" 
              disabled={page === totalPages}
              onClick={() => setPage(p => p + 1)}
            >
              Sau &raquo;
            </button>
          </div>
        )}
      </div>
    </div>
  );
}
