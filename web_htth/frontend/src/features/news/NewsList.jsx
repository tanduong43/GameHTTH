import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/api';
import '../../styles/App.css';

const getTag = (title = '') => {
  const t = title.toLowerCase();
  if (t.includes('khai mở') || t.includes('hot') || t.includes('mới')) return 'Hot';
  if (t.includes('sự kiện') || t.includes('đua top') || t.includes('khuyến mại') || t.includes('quà')) return 'Sự Kiện';
  if (t.includes('bảo trì') || t.includes('thông báo') || t.includes('lịch')) return 'Thông Báo';
  return 'Tin Tức';
};

const formatDate = (dateStr) => {
  if (!dateStr) return '';
  const date = new Date(dateStr);
  const day = String(date.getDate()).padStart(2, '0');
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const year = date.getFullYear();
  return `${day}/${month}/${year}`;
};

function NewsList() {
  const navigate = useNavigate();
  const [news, setNews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  
  // Search and Pagination states
  const [searchTerm, setSearchTerm] = useState('');
  const [searchQuery, setSearchQuery] = useState('');
  const [page, setPage] = useState(1);
  const [pagination, setPagination] = useState({
    page: 1,
    limit: 6,
    total: 0,
    totalPages: 1
  });

  // Set SEO metadata for News List
  useEffect(() => {
    document.title = 'Tin Tức & Sự Kiện | Thế Giới Hải Tặc - Đại Chiến Tứ Hoàng';
    const metaDesc = document.querySelector('meta[name="description"]');
    if (metaDesc) {
      metaDesc.setAttribute('content', 'Cập nhật liên tục các tin tức nóng hổi, sự kiện khuyến mãi, và thông tin bảo trì máy chủ mới nhất của game Thế Giới Hải Tặc.');
    }
  }, []);

  // Fetch news data
  useEffect(() => {
    const fetchNews = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await api.get('news', {
          params: {
            page: page,
            limit: 6, // 6 news per page looks very balanced in a 3-column grid
            search: searchQuery
          }
        });
        if (response.data && response.data.success) {
          setNews(response.data.data || []);
          setPagination(response.data.pagination || {
            page: page,
            limit: 6,
            total: 0,
            totalPages: 1
          });
        } else {
          setError('Không thể lấy danh sách tin tức.');
        }
      } catch (err) {
        console.error('Fetch news list error:', err);
        setError('Lỗi kết nối máy chủ. Vui lòng thử lại sau.');
      } finally {
        setLoading(false);
      }
    };

    fetchNews();
  }, [page, searchQuery]);

  const handleSearchSubmit = (e) => {
    e.preventDefault();
    setSearchQuery(searchTerm);
    setPage(1); // Reset to page 1 on new search
  };

  const handlePageChange = (newPage) => {
    if (newPage >= 1 && newPage <= pagination.totalPages) {
      setPage(newPage);
      window.scrollTo({ top: 0, behavior: 'smooth' });
    }
  };

  return (
    <div className="news-page-container">
      {/* Page Header Banner */}
      <div className="news-page-header">
        <span className="section-label">Bản Tin Hải Tặc</span>
        <h1 className="news-page-title-main">TIN TỨC & SỰ KIỆN</h1>
        <p className="news-page-subtitle">Nơi tổng hợp các hoạt động, sự kiện, thông báo bảo trì chính thức từ Ban Quản Trị.</p>
      </div>

      {/* Search Bar */}
      <div className="news-filter-bar">
        <form onSubmit={handleSearchSubmit} className="news-search-form">
          <input
            type="text"
            placeholder="Tìm kiếm bài viết..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="news-search-input"
          />
          <button type="submit" className="news-search-btn">Tìm kiếm 🔍</button>
        </form>
      </div>

      {/* Content Area */}
      {loading ? (
        <div className="news-loading-wrapper">
          <div className="news-spinner"></div>
          <p>Đang tải danh sách tin tức...</p>
        </div>
      ) : error ? (
        <div className="news-error-wrapper card-panel">
          <span className="news-error-icon">⚠️</span>
          <h3>Có lỗi xảy ra</h3>
          <p>{error}</p>
          <button onClick={() => setSearchQuery('')} className="btn-retry">Tải lại</button>
        </div>
      ) : news.length === 0 ? (
        <div className="news-empty-wrapper card-panel">
          <span className="news-empty-icon">📂</span>
          <h3>Chưa có tin tức nào</h3>
          <p>Hiện tại không tìm thấy bài viết tin tức nào phù hợp.</p>
          {searchQuery && (
            <button onClick={() => { setSearchTerm(''); setSearchQuery(''); setPage(1); }} className="btn-clear-search">
              Xóa bộ lọc tìm kiếm
            </button>
          )}
        </div>
      ) : (
        <>
          <div className="news-grid">
            {news.map((item) => {
              const tag = getTag(item.title);
              return (
                <article key={item.id} className="news-card card-panel">
                  {item.thumbnail && (
                    <div 
                      className="news-thumbnail-wrapper" 
                      onClick={() => navigate(`/news/${item.slug || item.id}`)}
                      style={{ cursor: 'pointer' }}
                    >
                      <img src={item.thumbnail} alt={item.title} className="news-thumbnail-img" />
                    </div>
                  )}
                  <div className="news-card-content-box">
                    <div className="news-meta">
                      <span className={`news-tag tag-${tag.toLowerCase().replace(' ', '-')}`}>{tag}</span>
                      <span className="news-date">📅 {formatDate(item.published_at || item.created_at)}</span>
                    </div>
                    <h3 
                      className="news-card-title"
                      onClick={() => navigate(`/news/${item.slug || item.id}`)}
                      style={{ cursor: 'pointer' }}
                    >
                      {item.title}
                    </h3>
                    <p className="news-card-desc">{item.summary}</p>
                    <button 
                      onClick={() => navigate(`/news/${item.slug || item.id}`)} 
                      className="btn-readmore"
                    >
                      Xem thêm ›
                    </button>
                  </div>
                </article>
              );
            })}
          </div>

          {/* Pagination */}
          {pagination.totalPages > 1 && (
            <div className="news-pagination">
              <button 
                onClick={() => handlePageChange(page - 1)}
                disabled={page === 1}
                className="pag-btn prev-btn"
              >
                ‹ Trước
              </button>
              
              {Array.from({ length: pagination.totalPages }, (_, idx) => idx + 1).map((pNum) => (
                <button
                  key={pNum}
                  onClick={() => handlePageChange(pNum)}
                  className={`pag-btn page-num-btn ${page === pNum ? 'active' : ''}`}
                >
                  {pNum}
                </button>
              ))}

              <button 
                onClick={() => handlePageChange(page + 1)}
                disabled={page === pagination.totalPages}
                className="pag-btn next-btn"
              >
                Sau ›
              </button>
            </div>
          )}
        </>
      )}
    </div>
  );
}

export default NewsList;
