import React, { createContext, useContext, useEffect, useState, useRef } from 'react';
import { io } from 'socket.io-client';
import { useAuth } from './AuthContext';
import confetti from 'canvas-confetti';

const SocketContext = createContext(null);

// Premium floating notification toast
function showGlobalToast(message) {
  let container = document.getElementById('global-toast-container');
  if (!container) {
    container = document.createElement('div');
    container.id = 'global-toast-container';
    container.style.position = 'fixed';
    container.style.top = '20px';
    container.style.right = '20px';
    container.style.zIndex = '999999';
    container.style.display = 'flex';
    container.style.flexDirection = 'column';
    container.style.gap = '10px';
    document.body.appendChild(container);
  }

  const toast = document.createElement('div');
  toast.style.background = 'linear-gradient(135deg, rgba(30, 41, 59, 0.95) 0%, rgba(15, 23, 42, 0.95) 100%)';
  toast.style.color = '#fff';
  toast.style.borderLeft = '4px solid #ff3366';
  toast.style.boxShadow = '0 10px 25px rgba(0,0,0,0.5), inset 0 1px 0 rgba(255,255,255,0.1)';
  toast.style.padding = '14px 20px';
  toast.style.borderRadius = '8px';
  toast.style.fontFamily = 'system-ui, sans-serif';
  toast.style.fontSize = '14px';
  toast.style.fontWeight = '600';
  toast.style.minWidth = '300px';
  toast.style.maxWidth = '400px';
  toast.style.transition = 'all 0.5s cubic-bezier(0.175, 0.885, 0.32, 1.275)';
  toast.style.transform = 'translateX(120%)';
  toast.style.opacity = '0';
  toast.innerHTML = message;

  container.appendChild(toast);

  // Trigger slide-in
  setTimeout(() => {
    toast.style.transform = 'translateX(0)';
    toast.style.opacity = '1';
  }, 50);

  // Trigger slide-out and cleanup
  setTimeout(() => {
    toast.style.transform = 'translateX(120%)';
    toast.style.opacity = '0';
    setTimeout(() => {
      toast.remove();
    }, 500);
  }, 6000);
}

export function SocketProvider({ children }) {
  const { user, setUser } = useAuth();
  const [socket, setSocket] = useState(null);
  const userRef = useRef(user);

  useEffect(() => {
    userRef.current = user;
  }, [user]);

  useEffect(() => {
    const socketUrl = import.meta.env.VITE_SOCKET_URL || `http://${window.location.hostname}:5000`;
    const socketInstance = io(socketUrl);
    setSocket(socketInstance);

    socketInstance.on('connect', () => {
      console.log('Socket connected to backend:', socketInstance.id);
      if (userRef.current && userRef.current.username) {
        socketInstance.emit('join', { username: userRef.current.username });
        console.log(`Joined socket room on connect: user_${userRef.current.username}`);
      }
    });

    // Listen to personal deposit success
    socketInstance.on('deposit_success', (data) => {
      console.log('Realtime deposit success received:', data);
      
      // Update balance
      if (data.newBalance !== undefined) {
        setUser((prev) => {
          if (prev) {
            return { ...prev, coin: data.newBalance };
          }
          return prev;
        });
      }

      // Fire canvas-confetti
      try {
        // Center shot
        confetti({
          particleCount: 120,
          spread: 70,
          origin: { y: 0.6 }
        });
        
        // Left & right shots for premium wow-factor!
        setTimeout(() => {
          confetti({
            particleCount: 50,
            angle: 60,
            spread: 55,
            origin: { x: 0 }
          });
        }, 150);
        setTimeout(() => {
          confetti({
            particleCount: 50,
            angle: 120,
            spread: 55,
            origin: { x: 1 }
          });
        }, 300);
      } catch (err) {
        console.error('Confetti error:', err);
      }
    });

    // Listen to personal deposit rejection
    socketInstance.on('deposit_rejected', (data) => {
      console.log('Realtime deposit rejected received:', data);
      if (data && data.message) {
        showGlobalToast(`❌ Yêu cầu nạp tiền <strong>${data.code}</strong> của bạn đã bị từ chối.`);
      }
    });

    // Listen to global notifications
    socketInstance.on('global_notification', (data) => {
      console.log('Global notification received:', data);
      if (data && data.message) {
        showGlobalToast(data.message);
      }
    });

    return () => {
      socketInstance.disconnect();
    };
  }, []);

  // Re-emit join room whenever user logs in or reconnects
  useEffect(() => {
    if (socket && user && user.username) {
      socket.emit('join', { username: user.username });
      console.log(`Re-joined socket room: user_${user.username}`);
    }
  }, [user, socket]);

  return (
    <SocketContext.Provider value={socket}>
      {children}
    </SocketContext.Provider>
  );
}

export function useSocket() {
  return useContext(SocketContext);
}
export default SocketContext;
