package io;

import java.net.InetSocketAddress;
import java.nio.ByteBuffer;
import java.util.concurrent.ConcurrentHashMap;

import org.java_websocket.WebSocket;
import org.java_websocket.handshake.ClientHandshake;
import org.java_websocket.server.WebSocketServer;

/**
 * WebSocket server for WebGL game clients.
 * Runs alongside the TCP ServerSocket, accepting WebSocket connections
 * and routing them through the same game logic via WebSocketSession.
 */
public class GameWebSocketServer extends WebSocketServer {

    private final ConcurrentHashMap<WebSocket, WebSocketSession> sessions;

    public GameWebSocketServer(int port) {
        super(new InetSocketAddress(port));
        this.sessions = new ConcurrentHashMap<>();
        // Allow reuse of address when server restarts
        this.setReuseAddr(true);
        System.out.println("WebSocket Server initialized on port " + port);
    }

    @Override
    public void onOpen(WebSocket conn, ClientHandshake handshake) {
        try {
            WebSocketSession session = new WebSocketSession(conn);
            sessions.put(conn, session);
            SessionManager.client_connect(session);
            System.out.println("[WS] Client connected: " + session.getRemoteAddress()
                    + " | online: " + SessionManager.CLIENT_ENTRYS.size());
        } catch (Exception e) {
            System.err.println("[WS] Error on open: " + e.getMessage());
            e.printStackTrace();
            conn.close();
        }
    }

    @Override
    public void onClose(WebSocket conn, int code, String reason, boolean remote) {
        WebSocketSession session = sessions.remove(conn);
        if (session != null) {
            session.disconnect();
            System.out.println("[WS] Client disconnected: " + session.getRemoteAddress()
                    + " (code=" + code + ", reason=" + reason + ")"
                    + " | online: " + SessionManager.CLIENT_ENTRYS.size());
        }
    }

    @Override
    public void onMessage(WebSocket conn, String message) {
        // Game protocol uses binary only, ignore text messages
    }

    @Override
    public void onMessage(WebSocket conn, ByteBuffer message) {
        WebSocketSession session = sessions.get(conn);
        if (session != null) {
            byte[] data = new byte[message.remaining()];
            message.get(data);
            session.onWebSocketMessage(data);
        }
    }

    @Override
    public void onError(WebSocket conn, Exception ex) {
        if (conn != null) {
            WebSocketSession session = sessions.get(conn);
            System.err.println("[WS] Error from "
                    + (session != null ? session.getRemoteAddress() : "unknown")
                    + ": " + ex.getMessage());
        } else {
            System.err.println("[WS] Server error: " + ex.getMessage());
        }
    }

    @Override
    public void onStart() {
        System.out.println("[WS] WebSocket Server started successfully!");
    }

    /**
     * Gracefully stop the WebSocket server.
     */
    public void shutdown() {
        try {
            // Close all active WebSocket sessions
            for (WebSocket conn : sessions.keySet()) {
                if (conn.isOpen()) {
                    conn.close();
                }
            }
            sessions.clear();
            this.stop();
            System.out.println("[WS] WebSocket Server stopped.");
        } catch (InterruptedException e) {
            System.err.println("[WS] Error stopping WebSocket server: " + e.getMessage());
        }
    }
}
