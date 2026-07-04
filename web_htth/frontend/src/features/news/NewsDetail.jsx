import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
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

function NewsDetail() {
  const { idOrSlug } = useParams();
  const navigate = useNavigate();
  const [article, setArticle] = useState(null);
  const [related, setRelated] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Fetch article detail
  useEffect(() => {
    const fetchArticleDetail = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await api.get(`news/${idOrSlug}`);
        if (response.data && response.data.success) {
          const data = response.data.data;
          setArticle(data);
          
          // Set SEO metadata dynamically
          document.title = `${data.title} | Thế Giới Hải Tặc`;
          const metaDesc = document.querySelector('meta[name="description"]');
          if (metaDesc) {
            metaDesc.setAttribute('content', data.summary || '');
          }

          // Fetch related articles
          try {
            const relResponse = await api.get('news', {
              params: { page: 1, limit: 4 }
            });
            if (relResponse.data && relResponse.data.success) {
              const otherArticles = (relResponse.data.data || [])
                .filter(item => item.id !== data.id)
                .slice(0, 3);
              setRelated(otherArticles);
            }
          } catch (err) {
            console.error('Error fetching related news:', err);
          }

        } else {
          setError('Không thể tìm thấy bài viết.');
        }
      } catch (err) {
        console.error('Fetch article error:', err);
        if (err.response && err.response.status === 404) {
          setError('Bài viết này không tồn tại hoặc đã bị xóa.');
        } else {
          setError('Lỗi kết nối máy chủ. Vui lòng thử lại sau.');
        }
      } finally {
        setLoading(false);
      }
    };

    fetchArticleDetail();
  }, [idOrSlug]);

  return (
    <div className="news-detail-page-container">
      {/* Back button */}
      <div className="news-detail-nav">
        <button onClick={() => navigate('/news')} className="btn-back-news">
          ← Quay Lại Bản Tin
        </button>
      </div>

      {loading ? (
        <div className="news-loading-wrapper">
          <div className="news-spinner"></div>
          <p>Đang tải chi tiết bài viết...</p>
        </div>
      ) : error ? (
        <div className="news-error-wrapper card-panel">
          <span className="news-error-icon">⚠️</span>
          <h3>Có lỗi xảy ra</h3>
          <p>{error}</p>
          <button onClick={() => navigate('/news')} className="btn-retry">Xem các tin tức khác</button>
        </div>
      ) : article ? (
        <article className="news-detail-wrapper card-panel">
          {/* Header */}
          <header className="news-detail-header">
            <div className="news-meta">
              <span className={`news-tag tag-${getTag(article.title).toLowerCase().replace(' ', '-')}`}>
                {getTag(article.title)}
              </span>
              <span className="news-date">📅 Đăng ngày: {formatDate(article.published_at || article.created_at)}</span>
            </div>
            <h1 className="news-detail-title">{article.title}</h1>
          </header>

          {/* Thumbnail image if exists */}
          {article.thumbnail && (
            <div className="news-detail-thumbnail">
              <img src={article.thumbnail} alt={article.title} />
            </div>
          )}

          {/* Body Content */}
          <div 
            className="news-detail-content" 
            dangerouslySetInnerHTML={{ __html: article.content }} 
          />
        </article>
      ) : null}

      {/* Related News Section */}
      {!loading && !error && related.length > 0 && (
        <section className="related-news-section">
          <h2 className="related-title">TIN TỨC LIÊN QUAN</h2>
          <div className="news-grid">
            {related.map((item) => {
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
        </section>
      )}
    </div>
  );
}

export default NewsDetail;
