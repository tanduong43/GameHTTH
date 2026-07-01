import React, { useState, useEffect } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const styles = {
  container: {
    display: 'flex',
    flexDirection: 'column',
    gap: '24px',
    maxWidth: '1200px',
    margin: '0 auto',
    fontFamily: '"Inter", "Roboto", sans-serif',
  },
  title: {
    color: '#ff3366',
    marginBottom: '20px',
    textAlign: 'center',
    fontSize: '22px',
    fontWeight: '700',
    textTransform: 'uppercase',
    letterSpacing: '0.5px'
  },
  layout: {
    display: 'flex',
    gap: '24px',
    flexWrap: 'wrap',
  },
  formCol: {
    flex: '1 1 420px',
    minWidth: '320px',
  },
  listCol: {
    flex: '1.5 1 650px',
    minWidth: '320px',
    background: 'rgba(26, 26, 26, 0.6)',
    border: '1px solid #333',
    borderRadius: '12px',
    padding: '24px',
    alignSelf: 'flex-start'
  },
  form: {
    textAlign: 'left',
    background: 'rgba(26, 26, 26, 0.6)',
    padding: '24px',
    borderRadius: '12px',
    border: '1px solid #333',
    color: '#fff'
  },
  formTitle: {
    color: '#ff3366',
    marginBottom: '20px',
    fontSize: '18px',
    fontWeight: '600',
    borderBottom: '1px solid #333',
    paddingBottom: '10px'
  },
  formGroup: {
    marginBottom: '16px',
  },
  flexRow: {
    display: 'flex',
    gap: '12px',
    marginBottom: '16px',
  },
  flexItem: {
    flex: 1,
  },
  label: {
    display: 'block',
    marginBottom: '6px',
    fontWeight: '600',
    textAlign: 'left',
    color: '#aaa',
    fontSize: '13px'
  },
  input: {
    width: '100%',
    padding: '10px 12px',
    borderRadius: '6px',
    border: '1px solid #444',
    backgroundColor: '#111',
    color: '#fff',
    boxSizing: 'border-box',
    fontSize: '14px',
    outline: 'none',
    transition: 'border-color 0.2s',
  },
  itemsContainer: {
    marginBottom: '16px',
    padding: '12px',
    backgroundColor: '#1c1c1c',
    border: '1px solid #333',
    borderRadius: '8px'
  },
  itemRow: {
    display: 'flex',
    gap: '8px',
    marginBottom: '8px',
    alignItems: 'center',
  },
  btnAddItem: {
    width: '100%',
    padding: '8px',
    marginTop: '4px',
    backgroundColor: 'transparent',
    color: '#00e5ff',
    border: '1px dashed #00e5ff',
    borderRadius: '6px',
    fontWeight: '600',
    cursor: 'pointer',
    transition: 'all 0.3s'
  },
  btnRemove: {
    padding: '10px 12px',
    backgroundColor: '#ff4d4f',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    fontWeight: 'bold',
    cursor: 'pointer',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    height: '38px',
  },
  btnSubmit: {
    width: '100%',
    padding: '12px',
    backgroundColor: '#ff3366',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    fontWeight: '700',
    fontSize: '15px',
    cursor: 'pointer',
    boxShadow: '0 2px 4px rgba(255, 51, 102, 0.3)',
    transition: 'background-color 0.3s',
    marginTop: '12px'
  },
  btnCancel: {
    width: '100%',
    padding: '10px',
    backgroundColor: '#444',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    fontWeight: '600',
    fontSize: '14px',
    cursor: 'pointer',
    transition: 'background-color 0.3s',
    marginTop: '8px',
    textAlign: 'center',
    display: 'block',
    textDecoration: 'none'
  },
  table: {
    width: '100%',
    borderCollapse: 'collapse',
    textAlign: 'left',
    fontSize: '13px',
    color: '#eee',
  },
  th: {
    padding: '12px 10px',
    borderBottom: '2px solid #444',
    color: '#aaa',
    fontWeight: '600',
  },
  td: {
    padding: '12px 10px',
    borderBottom: '1px solid #222',
    verticalAlign: 'top',
  },
  badge: {
    display: 'inline-block',
    padding: '2px 6px',
    borderRadius: '4px',
    fontSize: '11px',
    fontWeight: '500',
    margin: '2px 0',
  },
  actionBtn: {
    padding: '4px 8px',
    borderRadius: '4px',
    cursor: 'pointer',
    fontSize: '12px',
    fontWeight: 'bold',
    marginRight: '6px',
    background: 'transparent',
    transition: 'all 0.2s',
  }
};

function AdminGiftcode() {
  const { showMessage } = useOutletContext();
  
  // State for form
  const [editId, setEditId] = useState(null);
  const [code, setCode] = useState('');
  const [beri, setBeri] = useState(0);
  const [ruby, setRuby] = useState(0);
  const [items, setItems] = useState([]); // Array of { type: 3, id: '', quantity: 1 }
  const [thongbao, setThongbao] = useState('');
  const [luotnhap, setLuotnhap] = useState(0);
  const [gioihan, setGioihan] = useState(1);
  const [used, setUsed] = useState('');
  const [special, setSpecial] = useState('');

  // State for list
  const [giftcodes, setGiftcodes] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchGiftcodes = async () => {
    setLoading(true);
    try {
      const res = await api.get('admin/giftcodes');
      if (res.data.success) {
        setGiftcodes(res.data.giftcodes);
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ khi lấy danh sách giftcode!');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchGiftcodes();
  }, []);

  const resetForm = () => {
    setEditId(null);
    setCode('');
    setBeri(0);
    setRuby(0);
    setItems([]);
    setThongbao('');
    setLuotnhap(0);
    setGioihan(1);
    setUsed('');
    setSpecial('');
  };

  const handleAddItem = () => {
    setItems([...items, { type: 3, id: '', quantity: 1 }]);
  };

  const handleRemoveItem = (index) => {
    setItems(items.filter((_, i) => i !== index));
  };

  const handleItemChange = (index, field, value) => {
    const newItems = [...items];
    newItems[index][field] = value;
    setItems(newItems);
  };

  const handleEditClick = (gc) => {
    setEditId(gc.id);
    setCode(gc.giftname || '');
    setBeri(gc.beri || 0);
    setRuby(gc.ruby || 0);
    
    let parsedItems = [];
    try {
      const raw = JSON.parse(gc.item || '[]');
      if (Array.isArray(raw)) {
        parsedItems = raw.map(arr => ({
          type: arr[0] ?? 3,
          id: arr[1] ?? '',
          quantity: arr[2] ?? 1
        }));
      }
    } catch {
      parsedItems = [];
    }
    setItems(parsedItems);
    setThongbao(gc.thongbao || '');
    setLuotnhap(gc.luotnhap || 0);
    setGioihan(gc.gioihan || 1);
    setUsed(gc.used || '');
    setSpecial(gc.special || '');
    
    // Scroll to form on mobile
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const handleCreateGiftcode = async (e) => {
    e.preventDefault();
    if (!code.trim()) return showMessage('error', 'Vui lòng nhập mã code!');

    // Validate items
    for (let i = 0; i < items.length; i++) {
      if (items[i].id === '' || items[i].quantity === '') {
        return showMessage('error', 'Vui lòng nhập đầy đủ ID và Số lượng cho vật phẩm!');
      }
    }

    const itemJsonStr = JSON.stringify(
      items.map(i => [Number(i.type), Number(i.id), Number(i.quantity)])
    );

    try {
      const res = await api.post('admin/create_giftcode/', {
        code: code.trim(),
        beri: Number(beri),
        ruby: Number(ruby),
        item: itemJsonStr,
        thongbao,
        luotnhap: Number(luotnhap),
        gioihan: Number(gioihan),
        used,
        special,
      });
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        resetForm();
        fetchGiftcodes();
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  const handleUpdateGiftcode = async (e) => {
    e.preventDefault();
    if (!code.trim()) return showMessage('error', 'Vui lòng nhập mã code!');

    // Validate items
    for (let i = 0; i < items.length; i++) {
      if (items[i].id === '' || items[i].quantity === '') {
        return showMessage('error', 'Vui lòng nhập đầy đủ ID và Số lượng cho vật phẩm!');
      }
    }

    const itemJsonStr = JSON.stringify(
      items.map(i => [Number(i.type), Number(i.id), Number(i.quantity)])
    );

    try {
      const res = await api.put(`admin/giftcode/${editId}`, {
        code: code.trim(),
        beri: Number(beri),
        ruby: Number(ruby),
        item: itemJsonStr,
        thongbao,
        luotnhap: Number(luotnhap),
        gioihan: Number(gioihan),
        used,
        special,
      });
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        resetForm();
        fetchGiftcodes();
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  const handleDeleteGiftcode = async (id, name) => {
    if (!window.confirm(`Bạn có chắc chắn muốn xóa giftcode "${name}" không?`)) {
      return;
    }

    try {
      const res = await api.delete(`admin/giftcode/${id}`);
      showMessage(res.data.success ? 'success' : 'error', res.data.message);
      if (res.data.success) {
        fetchGiftcodes();
        if (editId === id) {
          resetForm();
        }
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  const renderItemsBadge = (itemJson) => {
    try {
      const list = JSON.parse(itemJson || '[]');
      if (!Array.isArray(list) || list.length === 0) return null;
      return (
        <div style={{ display: 'flex', flexDirection: 'column', gap: '4px', marginTop: '4px' }}>
          {list.map((it, idx) => {
            const type = it[0] ?? 3;
            const id = it[1];
            const qty = it[2];
            return (
              <span key={idx} style={{ ...styles.badge, background: 'rgba(0, 229, 255, 0.1)', color: '#00e5ff', border: '1px solid rgba(0, 229, 255, 0.2)' }}>
                📦 Loại {type} - ID: {id} (SL: {qty})
              </span>
            );
          })}
        </div>
      );
    } catch {
      return <span style={{ color: '#ff4d4f', fontSize: '11px' }}>Lỗi parse vật phẩm</span>;
    }
  };

  return (
    <div style={styles.container}>
      <h3 style={styles.title}>🎁 QUẢN LÝ GIFTCODE</h3>

      <div style={styles.layout}>
        {/* Cột Trái: Biểu mẫu tạo / cập nhật */}
        <div style={styles.formCol}>
          <form onSubmit={editId ? handleUpdateGiftcode : handleCreateGiftcode} style={styles.form}>
            <h4 style={styles.formTitle}>
              {editId ? `📝 CẬP NHẬT GIFTCODE (ID: ${editId})` : '➕ TẠO GIFTCODE MỚI'}
            </h4>

            <div style={styles.formGroup}>
              <label style={styles.label}>Mã Code (giftname):</label>
              <input
                type="text"
                placeholder="Ví dụ: TANTHUTHANG3"
                value={code}
                onChange={(e) => setCode(e.target.value)}
                required
                style={styles.input}
              />
            </div>

            <div style={styles.flexRow}>
              <div style={styles.flexItem}>
                <label style={styles.label}>Beri:</label>
                <input
                  type="number"
                  min="0"
                  value={beri}
                  onChange={(e) => setBeri(e.target.value)}
                  required
                  style={styles.input}
                />
              </div>
              <div style={styles.flexItem}>
                <label style={styles.label}>Ruby:</label>
                <input
                  type="number"
                  min="0"
                  value={ruby}
                  onChange={(e) => setRuby(e.target.value)}
                  required
                  style={styles.input}
                />
              </div>
            </div>

            <div style={styles.itemsContainer}>
              <label style={styles.label}>Danh sách Vật phẩm (Items):</label>
              {items.map((it, idx) => (
                <div key={idx} style={styles.itemRow}>
                  <div style={styles.flexItem}>
                    <input
                      type="number"
                      placeholder="Loại (Type)"
                      value={it.type}
                      onChange={(e) => handleItemChange(idx, 'type', e.target.value)}
                      style={styles.input}
                      title="Loại vật phẩm (thường là 3)"
                    />
                  </div>
                  <div style={styles.flexItem}>
                    <input
                      type="number"
                      placeholder="ID Vật phẩm"
                      value={it.id}
                      onChange={(e) => handleItemChange(idx, 'id', e.target.value)}
                      style={styles.input}
                      required
                    />
                  </div>
                  <div style={styles.flexItem}>
                    <input
                      type="number"
                      placeholder="Số lượng"
                      value={it.quantity}
                      onChange={(e) => handleItemChange(idx, 'quantity', e.target.value)}
                      style={styles.input}
                      required
                    />
                  </div>
                  <button
                    type="button"
                    onClick={() => handleRemoveItem(idx)}
                    style={styles.btnRemove}
                    title="Xóa vật phẩm"
                  >
                    ✕
                  </button>
                </div>
              ))}
              <button type="button" onClick={handleAddItem} style={styles.btnAddItem}>
                + Thêm Vật phẩm
              </button>
            </div>

            <div style={styles.formGroup}>
              <label style={styles.label}>Thông báo (thongbao):</label>
              <input
                type="text"
                placeholder="Nội dung thông báo khi nhận (tùy chọn)"
                value={thongbao}
                onChange={(e) => setThongbao(e.target.value)}
                style={styles.input}
              />
            </div>

            <div style={styles.flexRow}>
              <div style={styles.flexItem}>
                <label style={styles.label}>Lượt đã nhập (luotnhap):</label>
                <input
                  type="number"
                  min="0"
                  value={luotnhap}
                  onChange={(e) => setLuotnhap(e.target.value)}
                  style={styles.input}
                />
              </div>
              <div style={styles.flexItem}>
                <label style={styles.label}>Giới hạn lượt nhập (gioihan):</label>
                <input
                  type="number"
                  min="1"
                  value={gioihan}
                  onChange={(e) => setGioihan(e.target.value)}
                  required
                  style={styles.input}
                />
              </div>
            </div>

            <div style={styles.formGroup}>
              <label style={styles.label}>Danh sách đã dùng (used):</label>
              <input
                type="text"
                placeholder="Tên nhân vật cách nhau bởi dấu phẩy"
                value={used}
                onChange={(e) => setUsed(e.target.value)}
                style={styles.input}
              />
            </div>

            <div style={styles.formGroup}>
              <label style={styles.label}>Danh sách đặc biệt (special):</label>
              <input
                type="text"
                placeholder="Tên nhân vật, để trống nếu ai cũng nhận được"
                value={special}
                onChange={(e) => setSpecial(e.target.value)}
                style={styles.input}
              />
            </div>

            <button type="submit" style={styles.btnSubmit}>
              {editId ? 'Cập Nhật Code' : 'Tạo Mã Code'}
            </button>

            {editId && (
              <button type="button" onClick={resetForm} style={styles.btnCancel}>
                Hủy Chỉnh Sửa
              </button>
            )}
          </form>
        </div>

        {/* Cột Phải: Danh sách Giftcode */}
        <div style={styles.listCol}>
          <h4 style={{ ...styles.formTitle, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <span>📋 DANH SÁCH GIFTCODE ({giftcodes.length})</span>
            <button 
              onClick={fetchGiftcodes} 
              style={{
                background: 'transparent',
                border: '1px solid #ff3366',
                color: '#ff3366',
                padding: '4px 10px',
                borderRadius: '4px',
                fontSize: '12px',
                cursor: 'pointer',
                fontWeight: 'bold'
              }}
            >
              🔄 Tải lại
            </button>
          </h4>

          {loading ? (
            <div style={{ textAlign: 'center', padding: '30px', color: '#aaa' }}>Đang tải dữ liệu...</div>
          ) : (
            <div style={{ overflowX: 'auto' }}>
              <table style={styles.table}>
                <thead>
                  <tr style={{ borderBottom: '1px solid #444', background: 'rgba(0,0,0,0.3)' }}>
                    <th style={styles.th}>Mã Code</th>
                    <th style={styles.th}>Quà Tặng</th>
                    <th style={styles.th}>Lượt Nhập</th>
                    <th style={styles.th}>Giới Hạn / Used</th>
                    <th style={styles.th}>Hành Động</th>
                  </tr>
                </thead>
                <tbody>
                  {giftcodes.map((gc) => {
                    const isFull = gc.luotnhap >= gc.gioihan;
                    return (
                      <tr key={gc.id} style={{ borderBottom: '1px solid #222' }}>
                        <td style={{ ...styles.td, fontWeight: 'bold', color: '#00e5ff' }}>
                          {gc.giftname}
                          {gc.special && (
                            <div style={{ fontSize: '11px', color: '#ffac30', marginTop: '4px', fontWeight: 'normal' }}>
                              🎯 Special: {gc.special}
                            </div>
                          )}
                          {gc.thongbao && (
                            <div style={{ fontSize: '11px', color: '#888', marginTop: '2px', fontWeight: 'normal', fontStyle: 'italic' }}>
                              💬 {gc.thongbao}
                            </div>
                          )}
                        </td>
                        <td style={styles.td}>
                          <div style={{ display: 'flex', flexDirection: 'column', gap: '2px' }}>
                            {gc.beri > 0 && <span style={{ color: '#faad14', fontWeight: '500' }}>💰 {gc.beri.toLocaleString()} Beri</span>}
                            {gc.ruby > 0 && <span style={{ color: '#ff4d79', fontWeight: '500' }}>💎 {gc.ruby.toLocaleString()} Ruby</span>}
                            {renderItemsBadge(gc.item)}
                            {gc.beri === 0 && gc.ruby === 0 && (!gc.item || gc.item === '[]') && <span style={{ color: '#777' }}>Không có quà</span>}
                          </div>
                        </td>
                        <td style={styles.td}>
                          <span style={{ color: isFull ? '#ff4d4f' : '#52c41a', fontWeight: 'bold' }}>
                            {gc.luotnhap} / {gc.gioihan} {isFull ? '(Hết)' : ''}
                          </span>
                        </td>
                        <td style={{ ...styles.td, maxWidth: '150px', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }} title={gc.used}>
                          {gc.used ? (
                            <span style={{ color: '#aaa', fontSize: '12px' }}>{gc.used}</span>
                          ) : (
                            <span style={{ color: '#666', fontSize: '12px' }}>Chưa có ai dùng</span>
                          )}
                        </td>
                        <td style={{ ...styles.td, whiteSpace: 'nowrap' }}>
                          <button
                            onClick={() => handleEditClick(gc)}
                            style={{
                              ...styles.actionBtn,
                              border: '1px solid #faad14',
                              color: '#faad14'
                            }}
                          >
                            Sửa
                          </button>
                          <button
                            onClick={() => handleDeleteGiftcode(gc.id, gc.giftname)}
                            style={{
                              ...styles.actionBtn,
                              border: '1px solid #ff4d4f',
                              color: '#ff4d4f'
                            }}
                          >
                            Xóa
                          </button>
                        </td>
                      </tr>
                    );
                  })}
                  {giftcodes.length === 0 && (
                    <tr>
                      <td colSpan="5" style={{ padding: '20px', textAlign: 'center', color: '#888' }}>
                        Chưa có giftcode nào được tạo
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default AdminGiftcode;
