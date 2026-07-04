import React, { useState, useEffect, useCallback } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const formatDate = (dateStr) => {
  if (!dateStr) return 'Chưa xuất bản';
  const date = new Date(dateStr);
  const day = String(date.getDate()).padStart(2, '0');
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const year = date.getFullYear();
  const hours = String(date.getHours()).padStart(2, '0');
  const minutes = String(date.getMinutes()).padStart(2, '0');
  return `${day}/${month}/${year} ${hours}:${minutes}`;
};

function AdminNews() {
  const { showMessage } = useOutletContext();
  const [newsList, setNewsList] = useState([]);
  const [loading, setLoading] = useState(true);

  // Form State
  const [isEditing, setIsEditing] = useState(false);
  const [currentId, setCurrentId] = useState(null);
  const [title, setTitle] = useState('');
  const [summary, setSummary] = useState('');
  const [content, setContent] = useState('');
  const [thumbnail, setThumbnail] = useState('');
  const [status, setStatus] = useState('draft');
  const [saving, setSaving] = useState(false);

  const fetchNews = useCallback(async () => {
    setLoading(true);
    try {
      const res = await api.get('admin/news');
      if (res.data.success) {
        setNewsList(res.data.news || []);
      } else {
        showMessage('error', res.data.message || 'Lỗi lấy danh sách bài viết');
      }
    } catch (err) {
      console.error(err);
      showMessage('error', 'Lỗi kết nối máy chủ!');
    } finally {
      setLoading(false);
    }
  }, [showMessage]);

  useEffect(() => {
    fetchNews();
  }, [fetchNews]);

  const resetForm = () => {
    setIsEditing(false);
    setCurrentId(null);
    setTitle('');
    setSummary('');
    setContent('');
    setThumbnail('');
    setStatus('draft');
  };

  const handleFileChange = async (e) => {
    const file = e.target.files[0];
    if (!file) return;

    const reader = new FileReader();
    reader.onloadend = async () => {
      const base64Data = reader.result;
      try {
        showMessage('info', 'Đang tải ảnh lên máy chủ...');
        const res = await api.post('admin/upload', {
          fileName: file.name,
          fileData: base64Data
        });
        
        if (res.data && res.data.success) {
          setThumbnail(res.data.url);
          showMessage('success', 'Tải ảnh lên thành công!');
        } else {
          showMessage('error', res.data.message || 'Tải ảnh lên thất bại!');
        }
      } catch (err) {
        console.error(err);
        showMessage('error', 'Lỗi tải ảnh lên máy chủ!');
      }
    };
    reader.readAsDataURL(file);
  };

  const handleEditClick = (article) => {
    setIsEditing(true);
    setCurrentId(article.id);
    setTitle(article.title || '');
    setSummary(article.summary || '');
    setContent(article.content || '');
    setThumbnail(article.thumbnail || '');
    setStatus(article.status || 'draft');
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!title || !summary || !content) {
      showMessage('error', 'Vui lòng nhập đầy đủ các trường Tiêu đề, Tóm tắt và Nội dung!');
      return;
    }

    setSaving(true);
    const payload = { title, summary, content, thumbnail, status };

    try {
      let res;
      if (currentId) {
        // Update
        res = await api.put(`admin/news/${currentId}`, payload);
      } else {
        // Create
        res = await api.post('admin/news', payload);
      }

      if (res.data.success) {
        showMessage('success', res.data.message || 'Lưu bài viết thành công!');
        resetForm();
        fetchNews();
      } else {
        showMessage('error', res.data.message || 'Lưu bài viết thất bại!');
      }
    } catch (err) {
      console.error(err);
      showMessage('error', 'Lỗi kết nối máy chủ!');
    } finally {
      setSaving(false);
    }
  };

  const handleDeleteClick = async (id) => {
    if (!window.confirm('Bạn có chắc chắn muốn xóa bài viết này không?')) return;

    try {
      const res = await api.delete(`admin/news/${id}`);
      if (res.data.success) {
        showMessage('success', 'Xóa bài viết thành công!');
        fetchNews();
      } else {
        showMessage('error', res.data.message || 'Xóa bài viết thất bại!');
      }
    } catch (err) {
      console.error(err);
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  return (
    <div className="admin-form" style={{ maxWidth: '1000px', margin: '0 auto', fontFamily: '"Times New Roman", Times, serif' }}>
      <h3 style={{ color: '#ff3366', marginBottom: '20px', textAlign: 'center' }}>📰 QUẢN LÝ TIN TỨC</h3>

      {/* Editor / Form View */}
      {isEditing || currentId === null && title !== '' ? (
        <div style={{ background: 'rgba(26, 26, 26, 0.6)', border: '1px solid #333', padding: '25px', borderRadius: '12px', marginBottom: '30px' }}>
          <h4 style={{ color: '#ff3366', marginBottom: '20px', borderBottom: '1px solid #444', paddingBottom: '10px' }}>
            {currentId ? '✏️ CHỈNH SỬA BÀI VIẾT' : '➕ THÊM BÀI VIẾT MỚI'}
          </h4>
          
          <form onSubmit={handleSubmit}>
            <div style={{ marginBottom: '15px' }}>
              <label style={{ display: 'block', color: '#ccc', marginBottom: '5px', fontSize: '14px', fontWeight: 'bold' }}>Tiêu đề:</label>
              <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                style={{ width: '100%', padding: '10px', background: '#111', border: '1px solid #444', borderRadius: '6px', color: '#fff', outline: 'none' }}
                placeholder="Nhập tiêu đề tin tức..."
                required
              />
            </div>

            <div style={{ marginBottom: '15px' }}>
              <label style={{ display: 'block', color: '#ccc', marginBottom: '5px', fontSize: '14px', fontWeight: 'bold' }}>Ảnh đại diện (Thumbnail):</label>
              <div style={{ display: 'flex', alignItems: 'center', gap: '15px' }}>
                <input
                  type="file"
                  accept="image/*"
                  onChange={handleFileChange}
                  style={{ display: 'none' }}
                  id="thumbnail-upload-input"
                />
                <label
                  htmlFor="thumbnail-upload-input"
                  style={{
                    padding: '10px 15px',
                    background: '#0088cc',
                    color: '#fff',
                    borderRadius: '6px',
                    cursor: 'pointer',
                    fontSize: '14px',
                    fontWeight: 'bold',
                    display: 'inline-block'
                  }}
                >
                  📁 Chọn ảnh tải lên
                </label>
                {thumbnail && (
                  <button
                    type="button"
                    onClick={() => setThumbnail('')}
                    style={{
                      padding: '10px 15px',
                      background: '#ff4d4f',
                      border: 'none',
                      color: '#fff',
                      borderRadius: '6px',
                      cursor: 'pointer',
                      fontSize: '14px',
                      fontWeight: 'bold'
                    }}
                  >
                    🗑️ Xóa ảnh
                  </button>
                )}
              </div>
              
              {thumbnail && (
                <div style={{ marginTop: '15px', border: '1px solid #444', padding: '10px', borderRadius: '8px', display: 'inline-block', background: 'rgba(0,0,0,0.2)' }}>
                  <p style={{ fontSize: '12px', color: '#aaa', marginBottom: '5px' }}>Xem trước ảnh:</p>
                  <img src={thumbnail} alt="Preview" style={{ maxHeight: '120px', borderRadius: '4px', maxWidth: '100%', objectFit: 'contain' }} />
                  <p style={{ fontSize: '11px', color: '#888', marginTop: '5px', wordBreak: 'break-all' }}>{thumbnail}</p>
                </div>
              )}
            </div>

            <div style={{ marginBottom: '15px' }}>
              <label style={{ display: 'block', color: '#ccc', marginBottom: '5px', fontSize: '14px', fontWeight: 'bold' }}>Tóm tắt (Summary):</label>
              <textarea
                value={summary}
                onChange={(e) => setSummary(e.target.value)}
                rows={3}
                style={{ width: '100%', padding: '10px', background: '#111', border: '1px solid #444', borderRadius: '6px', color: '#fff', outline: 'none', resize: 'vertical' }}
                placeholder="Nhập tóm tắt bài viết ngắn gọn hiển thị trên danh sách..."
                required
              />
            </div>

            <div style={{ marginBottom: '15px' }}>
              <label style={{ display: 'block', color: '#ccc', marginBottom: '5px', fontSize: '14px', fontWeight: 'bold' }}>Nội dung chi tiết (HTML Content):</label>
              <textarea
                value={content}
                onChange={(e) => setContent(e.target.value)}
                rows={12}
                style={{ width: '100%', padding: '10px', background: '#111', border: '1px solid #444', borderRadius: '6px', color: '#fff', outline: 'none', resize: 'vertical', fontFamily: 'monospace' }}
                placeholder="Ví dụ: <h3>Tiêu đề nhỏ</h3><p>Đoạn văn viết ở đây...</p>"
                required
              />
            </div>

            <div style={{ marginBottom: '20px', display: 'flex', gap: '20px' }}>
              <div style={{ flex: 1 }}>
                <label style={{ display: 'block', color: '#ccc', marginBottom: '5px', fontSize: '14px', fontWeight: 'bold' }}>Trạng thái:</label>
                <select
                  value={status}
                  onChange={(e) => setStatus(e.target.value)}
                  style={{ width: '100%', padding: '10px', background: '#111', border: '1px solid #444', borderRadius: '6px', color: '#fff', outline: 'none' }}
                >
                  <option value="draft">Bản nháp (Draft)</option>
                  <option value="published">Xuất bản (Published)</option>
                </select>
              </div>
            </div>

            <div style={{ display: 'flex', gap: '10px', justifyContent: 'flex-end' }}>
              <button
                type="button"
                onClick={resetForm}
                style={{ padding: '10px 20px', background: '#444', border: 'none', borderRadius: '6px', color: '#fff', cursor: 'pointer', fontWeight: 'bold' }}
              >
                Hủy bỏ
              </button>
              <button
                type="submit"
                disabled={saving}
                style={{ padding: '10px 20px', background: '#ff3366', border: 'none', borderRadius: '6px', color: '#fff', cursor: 'pointer', fontWeight: 'bold' }}
              >
                {saving ? 'Đang lưu...' : 'Lưu bài viết'}
              </button>
            </div>
          </form>
        </div>
      ) : (
        <div style={{ display: 'flex', justifyContent: 'flex-end', marginBottom: '20px' }}>
          <button
            onClick={() => {
              resetForm();
              setTitle(' '); // Trigger form display mode
            }}
            style={{ padding: '10px 20px', background: '#0088cc', border: 'none', borderRadius: '6px', color: '#fff', cursor: 'pointer', fontWeight: 'bold' }}
          >
            ➕ Thêm bài viết mới
          </button>
        </div>
      )}

      {/* List View Table */}
      <h4 style={{ color: '#fff', marginBottom: '15px', textAlign: 'left', borderBottom: '1px solid #444', paddingBottom: '10px' }}>
        📋 DANH SÁCH BÀI VIẾT ({newsList.length})
      </h4>

      {loading ? (
        <div style={{ textAlign: 'center', padding: '40px', color: '#888' }}>Đang tải danh sách tin tức...</div>
      ) : (
        <div style={{ overflowX: 'auto' }}>
          <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left', fontSize: '14px', color: '#eee' }}>
            <thead>
              <tr style={{ borderBottom: '1px solid #444', background: 'rgba(0,0,0,0.3)' }}>
                <th style={{ padding: '12px 10px' }}>ID</th>
                <th style={{ padding: '12px 10px', width: '25%' }}>Tiêu đề</th>
                <th style={{ padding: '12px 10px', width: '25%' }}>Slug</th>
                <th style={{ padding: '12px 10px' }}>Ngày đăng</th>
                <th style={{ padding: '12px 10px', textAlign: 'center' }}>Trạng thái</th>
                <th style={{ padding: '12px 10px', textAlign: 'center' }}>Hành động</th>
              </tr>
            </thead>
            <tbody>
              {newsList.map((item) => (
                <tr key={item.id} style={{ borderBottom: '1px solid #333' }}>
                  <td style={{ padding: '12px 10px' }}>{item.id}</td>
                  <td style={{ padding: '12px 10px', fontWeight: 'bold', color: '#00e5ff' }}>{item.title}</td>
                  <td style={{ padding: '12px 10px', color: '#aaa', fontSize: '13px' }}>{item.slug}</td>
                  <td style={{ padding: '12px 10px', fontSize: '13px' }}>{formatDate(item.published_at || item.created_at)}</td>
                  <td style={{ padding: '12px 10px', textAlign: 'center' }}>
                    <span
                      style={{
                        padding: '3px 8px',
                        borderRadius: '4px',
                        fontSize: '11px',
                        fontWeight: 'bold',
                        background: item.status === 'published' ? 'rgba(82, 196, 26, 0.2)' : 'rgba(250, 173, 20, 0.2)',
                        color: item.status === 'published' ? '#52c41a' : '#faad14'
                      }}
                    >
                      {item.status === 'published' ? 'Đã đăng' : 'Bản nháp'}
                    </span>
                  </td>
                  <td style={{ padding: '12px 10px', textAlign: 'center' }}>
                    <div style={{ display: 'flex', gap: '8px', justifyContent: 'center' }}>
                      <button
                        onClick={() => handleEditClick(item)}
                        style={{ padding: '4px 8px', background: 'transparent', border: '1px solid #1890ff', borderRadius: '4px', color: '#1890ff', cursor: 'pointer', fontSize: '12px' }}
                      >
                        Sửa
                      </button>
                      <button
                        onClick={() => handleDeleteClick(item.id)}
                        style={{ padding: '4px 8px', background: 'transparent', border: '1px solid #ff4d4f', borderRadius: '4px', color: '#ff4d4f', cursor: 'pointer', fontSize: '12px' }}
                      >
                        Xóa
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
              {newsList.length === 0 && (
                <tr>
                  <td colSpan="6" style={{ padding: '30px', textAlign: 'center', color: '#888' }}>
                    Chưa có bài viết nào được tạo.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}

export default AdminNews;
