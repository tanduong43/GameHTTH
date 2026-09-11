import React, { createContext, useContext, useState, useEffect, useCallback } from 'react';
import api from '../api/api';
import { useSocket } from './SocketContext';

const ConfigContext = createContext(null);

export function ConfigProvider({ children }) {
  const [rechargeEnabled, setRechargeEnabled] = useState(true);
  const [depositMultiplier, setDepositMultiplier] = useState(1);
  const [loadingConfig, setLoadingConfig] = useState(true);
  const socket = useSocket();

  const fetchConfig = useCallback(async () => {
    try {
      const res = await api.get('recharge/bank_config');
      if (res.data && res.data.success) {
        if (res.data.rechargeEnabled !== undefined) {
          setRechargeEnabled(Boolean(res.data.rechargeEnabled));
        }
        if (res.data.depositMultiplier) {
          setDepositMultiplier(Number(res.data.depositMultiplier) || 1);
        }
      }
    } catch (err) {
      console.error('Lỗi khi tải cấu hình hệ thống:', err);
    } finally {
      setLoadingConfig(false);
    }
  }, []);

  useEffect(() => {
    fetchConfig();
  }, [fetchConfig]);

  // Lắng nghe realtime từ Socket.IO
  useEffect(() => {
    if (!socket) return;

    const handleRechargeStatusChanged = (data) => {
      console.log('Realtime recharge_status_changed received:', data);
      if (data && data.enabled !== undefined) {
        setRechargeEnabled(Boolean(data.enabled));
      }
    };

    const handleMultiplierChanged = (data) => {
      console.log('Realtime deposit_multiplier_changed received:', data);
      if (data && data.multiplier) {
        setDepositMultiplier(Number(data.multiplier) || 1);
      }
    };

    socket.on('recharge_status_changed', handleRechargeStatusChanged);
    socket.on('deposit_multiplier_changed', handleMultiplierChanged);

    return () => {
      socket.off('recharge_status_changed', handleRechargeStatusChanged);
      socket.off('deposit_multiplier_changed', handleMultiplierChanged);
    };
  }, [socket]);

  return (
    <ConfigContext.Provider
      value={{
        rechargeEnabled,
        setRechargeEnabled,
        depositMultiplier,
        setDepositMultiplier,
        loadingConfig,
        refreshConfig: fetchConfig,
      }}
    >
      {children}
    </ConfigContext.Provider>
  );
}

export function useConfig() {
  const context = useContext(ConfigContext);
  if (!context) {
    return {
      rechargeEnabled: true,
      setRechargeEnabled: () => {},
      depositMultiplier: 1,
      setDepositMultiplier: () => {},
      loadingConfig: false,
      refreshConfig: async () => {},
    };
  }
  return context;
}
