package core;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

import io.GameWebSocketServer;
import io.Session;
import io.SessionManager;

/**
 *
 * @author Truongbk
 */
public class ServerManager implements Runnable {
    private static ServerManager instance;
    private final Thread mythread;
    private ServerEventManager serverEventManager;
    private boolean running;
    private ServerSocket server;
    private GameWebSocketServer wsServer;
    private final long time;

    public ServerManager() {
        this.time = System.currentTimeMillis();
        this.mythread = new Thread(this);
    }

    public static ServerManager gI() {
        if (instance == null) {
            instance = new ServerManager();
        }
        return instance;
    }

    public void init() {
        Manager.gI().init();
        this.running = true;
        this.mythread.start();
        SaveData.process();

        // Register JVM Shutdown Hook to save all player and clan data on exit
        Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            System.out.println("SHUTDOWN: Saving database...");
            boolean admin_save = Manager.gI().server_admin;
            Manager.gI().server_admin = false;
            SaveData.process();
            Manager.gI().server_admin = admin_save;
            System.out.println("SHUTDOWN: Database saved successfully!");
        }));

        //
        serverEventManager = new ServerEventManager();
        serverEventManager.init();

        // Start WebSocket server for WebGL clients
        try {
            int wsPort = Manager.gI().ws_port;
            wsServer = new GameWebSocketServer(wsPort);
            wsServer.start();
            System.out.println("LISTEN WS PORT " + wsPort + "...");
        } catch (Exception e) {
            System.err.println("Failed to start WebSocket server: " + e.getMessage());
            e.printStackTrace();
        }
    }

    public void run() {
        try {
            this.server = new ServerSocket(Manager.gI().server_port);
        } catch (IOException e) {
            System.out.println(e);
            System.exit(0);
        }
        System.out.println("Started in " + (System.currentTimeMillis() - this.time) + "ms");
        System.out.println();
        System.out.println("LISTEN PORT " + Manager.gI().server_port + "...");
        while (this.running) {
            try {
                Socket client = this.server.accept();
                Session ss = new Session(client);
                SessionManager.client_connect(ss);
            } catch (Exception e) {
                e.printStackTrace();
                System.err.println("err accept socket");
            }
        }
    }

    public void close() throws IOException {
        serverEventManager.close();
        running = false;
        server.close();
        if (wsServer != null) {
            wsServer.shutdown();
        }
        instance = null;
    }

    public ServerSocket get_server() {
        return this.server;
    }
}

