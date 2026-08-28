package io;

import java.io.*;
import java.net.InetSocketAddress;
import java.nio.ByteBuffer;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;
import org.java_websocket.WebSocket;

/**
 * WebSocket-based Session for WebGL clients.
 * Extends Session and overrides network I/O to use WebSocket binary frames
 * instead of TCP Socket streams. All game logic is inherited from Session.
 */
public class WebSocketSession extends Session {

    private final WebSocket webSocket;
    private final BlockingQueue<byte[]> incomingQueue;
    private final String remoteAddress;

    public WebSocketSession(WebSocket webSocket) {
        super(); // Use protected constructor (no TCP socket)
        this.webSocket = webSocket;
        this.incomingQueue = new LinkedBlockingQueue<>();

        // Extract remote IP address
        InetSocketAddress remote = webSocket.getRemoteSocketAddress();
        this.remoteAddress = (remote != null) ? remote.getAddress().getHostAddress() : "unknown";
    }

    public String getRemoteAddress() {
        return remoteAddress;
    }

    /**
     * Called by GameWebSocketServer when a binary message is received.
     * Pushes raw bytes into the incoming queue for the receiver thread to process.
     */
    public void onWebSocketMessage(byte[] rawData) {
        incomingQueue.offer(rawData);
    }

    /**
     * Initialize the WebSocket session.
     * Creates sender and receiver threads, similar to TCP Session.init(),
     * but uses WebSocket for I/O instead of Socket streams.
     */
    @Override
    public void init() {
        this.connected = true;

        // Sender thread: polls outgoing messages and sends via WebSocket
        this.sendd = new Thread(() -> {
            try {
                while (connected) {
                    Message m = list_msg.poll(10, TimeUnit.SECONDS);
                    if (m != null) {
                        send_msg(m);
                        m.cleanup();
                    }
                }
            } catch (InterruptedException e) {
                // Thread interrupted, normal shutdown
            } catch (IOException e) {
                // Send error
            } finally {
                this.disconnect();
            }
        });

        // Receiver thread: reads from incomingQueue and processes messages
        this.receiv = new Thread(this);

        this.receiv.start();
        this.sendd.start();
    }

    /**
     * Receiver thread run loop.
     * Reads binary data from the incoming queue, parses into game Messages,
     * and dispatches to the MessageHandler (same as TCP Session).
     */
    @Override
    public void run() {
        try {
            while (this.connected) {
                // Wait for incoming data from WebSocket
                byte[] rawData = incomingQueue.poll(10, TimeUnit.SECONDS);
                if (rawData == null) continue;

                // Parse the raw bytes into a game Message
                Message m = parseMessage(rawData);
                if (m != null) {
                    if (m.cmd == -27) {
                        sendkeys();
                    } else if (sendKeyComplete) {
                        try {
                            // Use reflection or call the inherited controller
                            processMessage(m);
                        } catch (NullPointerException e) {
                            e.printStackTrace();
                            System.err.println("err nullpoint readmsg (ws)");
                        } catch (IndexOutOfBoundsException e) {
                            e.printStackTrace();
                            System.err.println("err outbound readmsg (ws)");
                        }
                    }
                    m.cleanup();
                }
            }
        } catch (InterruptedException e) {
            // Normal shutdown
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            disconnect();
        }
    }

    /**
     * Parse raw binary data (from a single WebSocket message) into a game Message.
     * The client sends: [cmd:1byte][sizeHi:1byte][sizeLo:1byte][data:N bytes]
     * Same wire format as the TCP protocol.
     */
    private Message parseMessage(byte[] rawData) throws IOException {
        if (rawData.length < 1) return null;

        DataInputStream stream = new DataInputStream(new ByteArrayInputStream(rawData));

        byte cmd = stream.readByte();
        if (sendKeyComplete) {
            cmd = readKey(cmd);
        }

        int size;
        if (sendKeyComplete) {
            byte b1 = stream.readByte();
            byte b2 = stream.readByte();
            size = (readKey(b1) & 255) << 8 | readKey(b2) & 255;
        } else {
            size = stream.readShort();
        }

        byte[] data = new byte[size];
        int byteRead = 0;
        while (byteRead < size) {
            int len = stream.read(data, byteRead, size - byteRead);
            if (len <= 0) break;
            byteRead += len;
        }

        if (sendKeyComplete) {
            for (int i = 0; i < data.length; i++) {
                data[i] = readKey(data[i]);
            }
        }

        stream.close();
        return new Message(cmd, data);
    }

    /**
     * Process message through the inherited MessageHandler controller.
     * We need to access the controller which is private in Session,
     * so we call through the run() mechanism indirectly.
     * Instead, we make this work by calling the process_msg method.
     */
    private void processMessage(Message m) throws IOException {
        // The controller field is private in Session, but we initialized it
        // via super() constructor which calls new MessageHandler(this).
        // We need to access it. Since controller is private, we use a workaround:
        // We'll add a protected method in Session to process messages.
        processMessageInternal(m);
    }

    /**
     * Serialize a game Message and send it as a WebSocket binary frame.
     * Overrides Session.send_msg so that all server messages go through WebSocket.
     */
    @Override
    protected void send_msg(Message msg) throws IOException {
        if (!webSocket.isOpen()) return;

        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        DataOutputStream out = new DataOutputStream(baos);

        byte[] data = msg.getData();

        if (sendKeyComplete) {
            byte b = writeKey(msg.cmd);
            out.writeByte(b);
        } else {
            out.writeByte(msg.cmd);
        }

        if (data != null) {
            int size = data.length;
            if (sendKeyComplete) {
                if ((msg.cmd == -39) || msg.cmd == -101 || msg.cmd == -93 || msg.cmd == 76 || msg.cmd == -102) {
                    out.writeByte(writeKey((byte) (size >> 24)));
                    out.writeByte(writeKey((byte) (size >> 16)));
                    out.writeByte(writeKey((byte) (size >> 8)));
                    out.writeByte(writeKey((byte) (size)));
                } else {
                    int byte1 = writeKey((byte) (size >> 8));
                    out.writeByte(byte1);
                    int byte2 = writeKey((byte) (size));
                    out.writeByte(byte2);
                }
            } else if (msg.cmd == -39 || msg.cmd == -101 || msg.cmd == -93 || msg.cmd == 76 || msg.cmd == -102) {
                out.writeInt(size);
            } else {
                final int byte1 = (byte) (size >> 8);
                out.writeByte(byte1);
                final int byte2 = (byte) (size & 0xFF);
                out.writeByte(byte2);
            }
            if (sendKeyComplete) {
                for (int i = 0; i < data.length; i++) {
                    data[i] = writeKey(data[i]);
                }
            }
            out.write(data);
        } else {
            out.writeShort(0);
        }

        out.flush();
        byte[] frame = baos.toByteArray();
        webSocket.send(ByteBuffer.wrap(frame));
        out.close();
        baos.close();
        msg.cleanup();
    }

    /**
     * Send encryption keys to the client (same protocol as TCP).
     */
    @Override
    public void sendkeys() throws IOException {
        byte[] KEYS = getKeys();
        Message msg = new Message(-27);
        msg.writer().writeByte(KEYS.length);
        msg.writer().writeByte(KEYS[0]);
        for (int i = 1; i < KEYS.length; i++) {
            msg.writer().writeByte(KEYS[i] ^ KEYS[i - 1]);
        }
        send_msg(msg);
        msg.cleanup();
        sendKeyComplete = true;
    }

    /**
     * Override addmsg to use WebSocket send instead of TCP.
     */
    @Override
    public void addmsg(Message m) throws IOException {
        if (this.connected) {
            m.writer().flush();
            this.list_msg.add(m);
        }
    }

    /**
     * Override clear_network to close WebSocket instead of TCP socket.
     */
    @Override
    public void clear_network(Session ss) {
        if (ss.sendd != null) {
            ss.sendd.interrupt();
            ss.sendd = null;
        }
        if (ss.receiv != null) {
            ss.receiv.interrupt();
            ss.receiv = null;
        }
        if (ss instanceof WebSocketSession) {
            WebSocketSession wss = (WebSocketSession) ss;
            if (wss.webSocket != null && wss.webSocket.isOpen()) {
                wss.webSocket.close();
            }
        }
    }
}
