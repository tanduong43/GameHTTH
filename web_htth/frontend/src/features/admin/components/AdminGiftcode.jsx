import React, { useState } from 'react';
import { useOutletContext } from 'react-router-dom';
import api from '../../../api/api';

const styles = {
  form: {
    textAlign: 'left',
    background: '#ffffff',
    padding: '24px',
    borderRadius: '8px',
    boxShadow: '0 2px 8px rgba(0, 0, 0, 0.08)',
    fontFamily: '"Inter", "Roboto", sans-serif',
    color: '#333'
  },
  title: {
    color: '#1890ff',
    marginBottom: '24px',
    textAlign: 'center',
    fontSize: '20px',
    fontWeight: '700',
    textTransform: 'uppercase',
    letterSpacing: '0.5px'
  },
  formGroup: {
    marginBottom: '24px',
  },
  flexRow: {
    display: 'flex',
    gap: '20px',
    marginBottom: '24px',
  },
  flexItem: {
    flex: 1,
  },
  label: {
    display: 'block',
    marginBottom: '8px',
    fontWeight: '600',
    textAlign: 'left',
    color: '#555',
    fontSize: '14px'
  },
  input: {
    width: '100%',
    padding: '12px',
    borderRadius: '6px',
    border: '1px solid #d9d9d9',
    boxSizing: 'border-box',
    fontSize: '14px',
    outline: 'none',
    transition: 'border-color 0.2s',
    backgroundColor: '#fff',
    color: '#333'
  },
  itemsContainer: {
    marginBottom: '24px',
    padding: '16px',
    backgroundColor: '#fafafa',
    border: '1px solid #f0f0f0',
    borderRadius: '8px'
  },
  itemRow: {
    display: 'flex',
    gap: '12px',
    marginBottom: '12px',
    alignItems: 'center',
  },
  btnAddItem: {
    width: '100%',
    padding: '10px',
    marginTop: '8px',
    backgroundColor: '#fff',
    color: '#1890ff',
    border: '1px dashed #1890ff',
    borderRadius: '6px',
    fontWeight: '600',
    cursor: 'pointer',
    transition: 'all 0.3s'
  },
  btnRemove: {
    padding: '12px 16px',
    backgroundColor: '#ff4d4f',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    fontWeight: 'bold',
    cursor: 'pointer',
    boxSizing: 'border-box',
    height: '42px',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center'
  },
  btnSubmit: {
    width: '100%',
    padding: '14px',
    marginTop: '24px',
    backgroundColor: '#1890ff',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    fontWeight: '700',
    fontSize: '16px',
    cursor: 'pointer',
    boxShadow: '0 2px 4px rgba(24, 144, 255, 0.3)',
    transition: 'background-color 0.3s'
  }
};

function AdminGiftcode() {
  const { showMessage } = useOutletContext();
  const [code, setCode] = useState('');
  const [beri, setBeri] = useState(0);
  const [ruby, setRuby] = useState(0);
  const [items, setItems] = useState([]); // Array of { type: 3, id: '', quantity: 1 }
  const [thongbao, setThongbao] = useState('');
  const [luotnhap, setLuotnhap] = useState(0);
  const [gioihan, setGioihan] = useState(1);
  const [used, setUsed] = useState('');
  const [special, setSpecial] = useState('');

  const resetForm = () => {
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
      }
    } catch {
      showMessage('error', 'Lỗi kết nối máy chủ!');
    }
  };

  return (
    <form onSubmit={handleCreateGiftcode} style={styles.form} className="admin-form">
      <h3 style={styles.title}>🎁 TẠO GIFTCODE</h3>

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
          placeholder="Nội dung thông báo khi nhập code (tùy chọn)"
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
          placeholder="Tên nhân vật đã nhập, cách nhau dấu phẩy — để trống khi tạo mới"
          value={used}
          onChange={(e) => setUsed(e.target.value)}
          style={styles.input}
        />
      </div>

      <div style={styles.formGroup}>
        <label style={styles.label}>Danh sách đặc biệt (special):</label>
        <input
          type="text"
          placeholder="Chỉ nhân vật trong danh sách mới nhận được — để trống = ai cũng nhận"
          value={special}
          onChange={(e) => setSpecial(e.target.value)}
          style={styles.input}
        />
      </div>

      <button type="submit" style={styles.btnSubmit}>
        Tạo Mã Code
      </button>
    </form>
  );
}

export default AdminGiftcode;

