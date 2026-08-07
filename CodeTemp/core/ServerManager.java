package core;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
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
        //
        serverEventManager = new ServerEventManager();
        serverEventManager.init();
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
        instance = null;
    }

    public ServerSocket get_server() {
        return this.server;
    }
}
