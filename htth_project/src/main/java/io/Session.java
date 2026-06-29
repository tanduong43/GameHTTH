package io;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.Socket;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;
import java.util.regex.Pattern;
import activities.UpgradeItem;
import client.Clan;
import client.Item;
import client.MessageHandler;
import client.Player;
import core.Manager;
import core.Service;
import database.SQL;
import map.Map;
import org.joda.time.DateTime;
import org.json.simple.JSONArray;
import org.json.simple.JSONValue;
import template.*;
/**
 *
 * @author Truongbk
 */
public class Session implements Runnable {
    private static final byte[] KEYS = "truongbk@".getBytes();
    private final Socket socket;
    private DataInputStream dis;
    private DataOutputStream dos;
    private Thread sendd;
    private Thread receiv;
    public boolean connected;
    private final BlockingQueue<Message> list_msg;
    private boolean sendKeyComplete;
    private byte curR;
    private byte curW;
    public String user;
    public String pass;
    private final MessageHandler controller;
    public Player p;
    public List<String> list_char;
    public byte zoomlv;
    public String version;
    public byte lock;
    public byte status;
    public int coin;
    public int vip;
    private boolean getImgAPK = false;

    public Session(Socket socket) {
        this.socket = socket;
        this.list_msg = new LinkedBlockingQueue<Message>();
        this.sendKeyComplete = false;
        this.connected = false;
        this.controller = new MessageHandler(this);
    }

    public void init() {
        try {
            this.dis = new DataInputStream(socket.getInputStream());
            this.dos = new DataOutputStream(socket.getOutputStream());
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
                } catch (IOException e) {
                } finally {
                    this.disconnect();
                }
            });
            this.receiv = new Thread(this);
            this.connected = true;
            //
            this.receiv.start();
            this.sendd.start();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void disconnect() {
        SessionManager.client_disconnect(this);
    }

    public void addmsg(Message m) throws IOException {
        if (this.connected) {
            m.writer().flush();
            this.list_msg.add(m);
        }
    }

    @Override
    public void run() {
        try {
            while (this.connected) {
                Message m = read_msg();
                if (m != null) {
                    if (m.cmd == -27) {
                        sendkeys();
                    } else if (sendKeyComplete) {
                        try {
                            controller.process_msg(m);
                        } catch (NullPointerException e) {
                            e.printStackTrace();
                            System.err.println("err nullpoint readmsg");
                        } catch (IndexOutOfBoundsException e) {
                            e.printStackTrace();
                            System.err.println("err outbound readmsg");
                        }
                    }
                    m.cleanup();
                }
            }
        } catch (IOException e) {
        } finally {
            disconnect();
        }
    }

    private void send_msg(Message msg) throws IOException {
        byte[] data = msg.getData();
        if (sendKeyComplete) {
            byte b = writeKey(msg.cmd);
            dos.writeByte(b);
        } else {
            dos.writeByte(msg.cmd);
        }
        if (data != null) {
            int size = data.length;
            if (sendKeyComplete) {
                if ((msg.cmd == -39) || msg.cmd == -101 || msg.cmd == -93 || msg.cmd == 76) {
                    dos.writeByte(writeKey((byte) (size >> 24)));
                    dos.writeByte(writeKey((byte) (size >> 16)));
                    dos.writeByte(writeKey((byte) (size >> 8)));
                    dos.writeByte(writeKey((byte) (size)));
                } else {
                    int byte1 = writeKey((byte) (size >> 8));
                    dos.writeByte(byte1);
                    int byte2 = writeKey((byte) (size));
                    dos.writeByte(byte2);
                }
            } else if (msg.cmd == -39) {
                dos.writeInt(size);
            } else {
                final int byte1 = (byte) (size & 0xFF00);
                this.dos.writeByte(byte1);
                final int byte2 = (byte) (size & 0xFF);
                this.dos.writeByte(byte2);
            }
            if (sendKeyComplete) {
                for (int i = 0; i < data.length; i++) {
                    data[i] = writeKey(data[i]);
                }
            }
            dos.write(data);
        } else {
            this.dos.writeShort(0);
        }
        dos.flush();
        msg.cleanup();
    }

    private Message read_msg() throws IOException {
        byte cmd = dis.readByte();
        if (sendKeyComplete) {
            cmd = readKey(cmd);
        }
        int size;
        if (sendKeyComplete) {
            byte b1 = dis.readByte();
            byte b2 = dis.readByte();
            size = (readKey(b1) & 255) << 8 | readKey(b2) & 255;
        } else {
            size = dis.readShort();
        }
        byte data[] = new byte[size];
        int len = 0;
        int byteRead = 0;
        while (len != -1 && byteRead < size) {
            len = dis.read(data, byteRead, size - byteRead);
            if (len > 0) {
                byteRead += len;
            }
        }
        if (sendKeyComplete) {
            for (int i = 0; i < data.length; i++) {
                data[i] = readKey(data[i]);
            }
        }
        return new Message(cmd, data);
    }

    private byte readKey(final byte b) {
        final byte curR = this.curR;
        this.curR = (byte) (curR + 1);
        final byte i = (byte) ((KEYS[curR] & 0xFF) ^ (b & 0xFF));
        if (this.curR >= KEYS.length) {
            this.curR %= (byte) KEYS.length;
        }
        return i;
    }

    private byte writeKey(final byte b) {
        final byte curW = this.curW;
        this.curW = (byte) (curW + 1);
        final byte i = (byte) ((KEYS[curW] & 0xFF) ^ (b & 0xFF));
        if (this.curW >= KEYS.length) {
            this.curW %= (byte) KEYS.length;
        }
        return i;
    }

    public void sendkeys() throws IOException {
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

    public void request_data_update(Message m) throws IOException {
        byte type = m.reader().readByte();
        // System.out.println("msg-7 " + type);
        if (type == 3 && p != null && p.conn != null) {
            p.send_skill();
        } else {
            Message m2 = new Message(-7);
            switch (type) {
                case 2: {
                    m2.writer().writeByte(2);
                    m2.writer().writeShort(ItemOptionTemplate.ENTRYS.size());
                    for (int i = 0; i < ItemOptionTemplate.ENTRYS.size(); i++) {
                        m2.writer().writeUTF(ItemOptionTemplate.ENTRYS.get(i).name);
                        m2.writer().writeByte(ItemOptionTemplate.ENTRYS.get(i).color);
                        m2.writer().writeByte(ItemOptionTemplate.ENTRYS.get(i).percent);
                    }
                    m2.writer().writeShort(DataTemplate.VerdataAttri);
                    break;
                }
                case 4: {
                    m2.writer().writeByte(4);
                    m2.writer().writeByte(DataTemplate.mLockMap.length);
                    m2.writer().write(DataTemplate.mLockMap);
                    break;
                }
                case 6: {
                    m2.writer().writeByte(6);
                    byte[] ab = null;
                    try (FileInputStream fis =
                            new FileInputStream("data/msg/login/request/msg-7_6")) {
                        ab = new byte[fis.available() - 2];
                        fis.read(ab, 0, ab.length);
                    } catch (IOException e) {
                        ab = null;
                    }
                    if (ab == null) {
                        System.out.println("err data type 6");
                        return;
                    }
                    m2.writer().write(ab);
                    m2.writer().writeShort(DataTemplate.VerdataNameMap);
                    break;
                }
                case 7: {
                    m2.writer().writeByte(7);
                    m2.writer().writeShort(DataTemplate.NamePotionquest.length);
                    for (int i = 0; i < DataTemplate.NamePotionquest.length; i++) {
                        m2.writer().writeUTF(DataTemplate.NamePotionquest[i]);
                    }
                    m2.writer().writeShort(DataTemplate.VerdataNamePotionquest);
                    break;
                }
                case 8: {
                    m2.writer().writeByte(8);
                    m2.writer().writeShort(DataTemplate.TabInventory_ItemSell[0]);
                    m2.writer().writeShort(DataTemplate.TabInventory_ItemSell[1]);
                    m2.writer().writeShort(DataTemplate.TabInventory_ItemSell[2]);
                    break;
                }
                case 10: {
                    m2.writer().writeByte(10);
                    m2.writer().writeByte(DataTemplate.mMapLang.length);
                    for (int i = 0; i < DataTemplate.mMapLang.length; i++) {
                        m2.writer().writeShort(DataTemplate.mMapLang[i]);
                    }
                    break;
                }
                case 11: {
                    m2.writer().writeByte(11);
                    m2.writer().writeByte(ItemTemplate7.ENTRYS.size());
                    for (int i = 0; i < ItemTemplate7.ENTRYS.size(); i++) {
                        ItemTemplate7 temp = ItemTemplate7.ENTRYS.get(i);
                        m2.writer().writeByte(temp.id);
                        m2.writer().writeUTF(temp.name);
                        m2.writer().writeByte(temp.type);
                        m2.writer().writeByte(temp.icon);
                        m2.writer().writeInt(temp.price);
                        m2.writer().writeShort(temp.priceruby);
                        m2.writer().writeByte(temp.istrade);
                    }
                    break;
                }
                case 12: {
                    m2.writer().writeByte(12);
                    m2.writer().writeByte(UpgradeItem.DATA.size());
                    for (int i = 0; i < UpgradeItem.DATA.size(); i++) {
                        DataUpgrade temp = UpgradeItem.DATA.get(i);
                        m2.writer().writeByte(temp.level);
                        m2.writer().writeShort(temp.per);
                        m2.writer().writeByte(temp.prelevel);
                        m2.writer().writeInt(temp.beri);
                        m2.writer().writeInt(temp.beri_white);
                        m2.writer().writeShort(temp.ruby);
                        m2.writer().writeShort(temp.att);
                        m2.writer().writeByte(temp.material.length);
                        for (int j = 0; j < temp.material.length; j++) {
                            m2.writer().writeByte(temp.material[j].type);
                            m2.writer().writeByte(temp.material[j].id);
                            m2.writer().writeShort(temp.material[j].quant);
                        }
                    }
                    m2.writer().writeShort(DataTemplate.VerdataUpgradeSave);
                    break;
                }
                case 13: {
                    m2.writer().writeByte(13);
                    m2.writer().writeByte(DataTemplate.mSea.length);
                    for (int i = 0; i < DataTemplate.mSea.length; i++) {
                        for (int j = 0; j < DataTemplate.mSea[i].length; j++) {
                            m2.writer().writeShort(DataTemplate.mSea[i][j]);
                        }
                    }
                    break;
                }
                case 15: {
                    m2.writer().writeByte(15);
                    m2.writer().writeShort(MobTemplate.ENTRYS.size());
                    for (int i = 0; i < MobTemplate.ENTRYS.size(); i++) {
                        MobTemplate temp = MobTemplate.ENTRYS.get(i);
                        m2.writer().writeShort(temp.mob_id);
                        m2.writer().writeUTF(temp.name);
                        m2.writer().writeShort(temp.level);
                        m2.writer().writeShort(temp.hOne);
                        m2.writer().writeInt(temp.hp_max);
                        m2.writer().writeByte(temp.typemove);
                        m2.writer().writeByte(temp.ishuman);
                        m2.writer().writeByte(temp.typemonster);
                        if (temp.ishuman == 1) {
                            m2.writer().writeShort(temp.head);
                            m2.writer().writeShort(temp.hair);
                            m2.writer().writeByte(temp.wearing.length);
                            for (int j = 0; j < temp.wearing.length; j++) {
                                if (temp.wearing[j] != -1) {
                                    m2.writer().writeByte(1);
                                    m2.writer().writeShort(temp.wearing[j]);
                                } else {
                                    m2.writer().writeByte(-1);
                                }
                            }
                        } else {
                            m2.writer().writeShort(temp.icon);
                        }
                    }
                    m2.writer().writeShort(DataTemplate.VerdataMon);
                    break;
                }
                case 17: {
                    m2.writer().writeByte(17);
                    m2.writer().writeLong(System.currentTimeMillis());
                    break;
                }
                case 19: {
                    m2.writer().writeByte(19);
                    m2.writer().writeByte(DataTemplate.mTileUpdate.length);
                    for (int i = 0; i < DataTemplate.mTileUpdate.length; i++) {
                        m2.writer().writeShort(DataTemplate.mTileUpdate[i]);
                    }
                    m2.writer().writeByte(DataTemplate.mTileGhepĐa.length);
                    for (int i = 0; i < DataTemplate.mTileGhepĐa.length; i++) {
                        m2.writer().writeShort(DataTemplate.mTileGhepĐa[i]);
                    }
                    break;
                }
                case 21: {
                    m2.writer().writeByte(21);
                    m2.writer().writeByte(0); // h12plus = 0;
                    break;
                }
                case 26: {
                    m2.writer().writeByte(26);
                    m2.writer().writeByte(DataTemplate.AttriKichAn.length);
                    for (int i = 0; i < DataTemplate.AttriKichAn.length; i++) {
                        m2.writer().writeUTF(DataTemplate.AttriKichAn[i]);
                    }
                    m2.writer().writeShort(-31525);
                    break;
                }
                case 28: {
                    m2.writer().writeByte(28);
                    m2.writer().writeShort(ItemTemplate4.ENTRYS.size());
                    for (int i = 0; i < ItemTemplate4.ENTRYS.size(); i++) {
                        ItemTemplate4 temp = ItemTemplate4.ENTRYS.get(i);
                        m2.writer().writeShort(temp.id);
                        m2.writer().writeShort(temp.icon);
                        m2.writer().writeUTF(temp.name);
                        m2.writer().writeShort(temp.indexInfoPotion);
                        m2.writer().writeInt(temp.beri);
                        m2.writer().writeShort(temp.ruby);
                        m2.writer().writeByte(temp.istrade);
                        m2.writer().writeByte(temp.type);
                        m2.writer().writeShort(temp.timedelay);
                        m2.writer().writeShort(temp.value);
                        m2.writer().writeShort(temp.timeactive);
                        m2.writer().writeUTF(temp.nameuse);
                    }
                    m2.writer().writeShort(DataTemplate.VerdataPotion);
                    break;
                }
                case 27: {
                    m2.writer().writeByte(27);
                    m2.writer().writeByte(0); // isopenDao
                    break;
                }
                case 18:
                case 29: {
                    m2.writer().writeByte(18);
                    m2.writer().writeShort(ItemTemplate8.ENTRYS.size());
                    for (int i = 0; i < ItemTemplate8.ENTRYS.size(); i++) {
                        ItemTemplate8 temp = ItemTemplate8.ENTRYS.get(i);
                        m2.writer().writeShort(temp.id);
                        m2.writer().writeShort(temp.icon);
                        m2.writer().writeUTF(temp.name);
                        m2.writer().writeUTF(temp.info);
                        m2.writer().writeInt(temp.beri);
                        m2.writer().writeShort(temp.ruby);
                        m2.writer().writeByte(temp.istrade);
                        m2.writer().writeByte(temp.type);
                        m2.writer().writeShort(temp.timedelay);
                        m2.writer().writeShort(temp.value);
                        m2.writer().writeShort(temp.timeactive);
                        m2.writer().writeUTF(temp.nameuse);
                    }
                    m2.writer().writeShort(DataTemplate.VerdataPotionClan);
                    break;
                }
                case 30: {
                    m2.writer().writeByte(30);
                    m2.writer().writeByte(DataTemplate.mEffSpec.length);
                    for (int i = 0; i < DataTemplate.mEffSpec.length; i++) {
                        m2.writer().writeUTF(DataTemplate.mEffSpec[i]);
                    }
                    m2.writer().writeShort(-7547);
                    break;
                }
            }
            if (m2.writer().size() > 0) {
                this.addmsg(m2);
            }
            m2.cleanup();
        }
    }

    public void send_data_from_server(Message m) throws IOException {
        if (this.getImgAPK) {
            return;
        } else {
            getImgAPK = true;
        }
        this.zoomlv = m.reader().readByte();
        Thread send = new Thread(() -> {
            try {
                String path = "data/datafromsver/x" + this.zoomlv;
                File folder = new File(path);
                if (folder.isDirectory()) {
                    File[] files = folder.listFiles();
                    Arrays.sort(files, new Comparator<File>() {
                        @Override
                        public int compare(File o1, File o2) {
                            int name1 = solve_name(o1.getName());
                            int name2 = solve_name(o2.getName());
                            return (name1 > name2) ? 1 : -1;
                        }

                        private int solve_name(String name) {
                            String num = "";
                            for (int i = 0; i < name.length(); i++) {
                                if (name.charAt(i) == '_') {
                                    break;
                                }
                                num += name.charAt(i);
                            }
                            return Integer.parseInt(num);
                        }
                    });
                    for (int i = 0; i < files.length; i++) {
                        int cmd = Integer.parseInt(files[i].getName().substring(
                                (files[i].getName().length() - 3), files[i].getName().length()));
                        Service.send_msg_data(this, cmd, files[i].getAbsolutePath(), false);
                    }
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        });
        send.start();
    }

    public void login(Message m) throws IOException {
        byte type = m.reader().readByte();
        String user_ = m.reader().readUTF().replace(" ", "");
        String pass_ = m.reader().readUTF().replace(" ", "");
        long time_can_login_again = 0;
        if (SessionManager.CLIENT_LOGIN_TIME.containsKey(user_)) {
            long time_can_login = SessionManager.CLIENT_LOGIN_TIME.get(user_);
            if (time_can_login > System.currentTimeMillis()) {
                SessionManager.CLIENT_LOGIN_TIME.replace(user_, time_can_login, (time_can_login));
                time_can_login_again = (time_can_login - System.currentTimeMillis()) / 1000;
            } else {
                SessionManager.CLIENT_LOGIN_TIME.remove(user_);
            }
        }
        this.lock = 0;
        this.coin = 0;
        this.coin = 0;
        this.status = 0;
        list_char = new ArrayList<>();
        if (type == 0) {
            Pattern p = Pattern.compile("^[a-zA-Z0-9@.]{1,30}$");
            if (!p.matcher(user_).matches() || !p.matcher(pass_).matches()) {
                login_notice("Ký tự không hợp lệ");
                return;
            }
            if (Manager.gI().server_admin) {
                SessionManager.time_login.clear();
            }
            if (SessionManager.time_login.containsKey(user_) && !user_.equals("admin")) {
                long time_login = SessionManager.time_login.get(user_);
                if (time_login > System.currentTimeMillis()) {
                    long time_login_after = (time_login - System.currentTimeMillis()) / 1000;
                    if (time_login_after < 120) {
                        login_after_time(time_login_after);
                    }
                    return;
                }
            }
            //
            Connection conn = null;
            Statement st = null;
            ResultSet rs = null;
            try {
                conn = SQL.gI().getCon();
                st = conn.createStatement();
                rs = st.executeQuery("SELECT * FROM `accounts` WHERE BINARY `user` = '" + user_
                        + "' AND BINARY `pass` = '" + pass_ + "' LIMIT 1;");
                if (!rs.next()) {
                    login_notice("Tài khoản mật khẩu không chính xác" + (time_can_login_again > 0
                            ? ".\nThử lại sau " + time_can_login_again + "s"
                            : ""));
                    if (!SessionManager.CLIENT_LOGIN_TIME.containsKey(user_)) {
                        SessionManager.CLIENT_LOGIN_TIME.put(user_,
                                (System.currentTimeMillis() + 1_000L));
                    }
                    return;
                }
                this.vip = rs.getInt("vip");
                this.coin = rs.getInt("coin");
                this.status = rs.getByte("status");
                this.lock = rs.getByte("lock");
                if (this.lock != 0) {
                    login_notice("Tài khoản bị khóa để kiểm tra, liên hệ admin để biết chi tiết, "
                            + "đừng spam tin nhắn kẻo bị t tắt thông báo!");
                    return;
                }
                //
                JSONArray js = (JSONArray) JSONValue.parse(rs.getString("char"));
                for (int i = 0; i < js.size(); i++) {
                    list_char.add(js.get(i).toString());
                }
            } catch (SQLException e) {
                e.printStackTrace();
                return;
            } finally {
                try {
                    if (rs != null) {
                        rs.close();
                    }
                    if (st != null) {
                        st.close();
                    }
                    if (conn != null) {
                        conn.close();
                    }
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
        } else {
            if (user_.equals("") && pass_.equals("")) {
                login_notice("Truy cập hanhtrinhhaitac.com để đăng ký");
                return;
//                user_ = "htth_truongbk_" + System.nanoTime();
//                pass_ = "1";
//                Connection conn = null;
//                Statement st = null;
//                try {
//                    conn = SQL.gI().getCon();
//                    st = conn.createStatement();
//                    st.execute("INSERT INTO `accounts` (`user`, `pass`,`lock`) VALUES ('" + user_
//                            + "', '1', 0)");
//                } catch (SQLException e) {
//                    e.printStackTrace();
//                    return;
//                } finally {
//                    try {
//                        if (st != null) {
//                            st.close();
//                        }
//                        if (conn != null) {
//                            conn.close();
//                        }
//                    } catch (SQLException e) {
//                        e.printStackTrace();
//                    }
//                }
            } else {
                pass_ = "1";
                Connection conn = null;
                Statement st = null;
                ResultSet rs = null;
                try {
                    conn = SQL.gI().getCon();
                    st = conn.createStatement();
                    rs = st.executeQuery("SELECT * FROM `accounts` WHERE BINARY `user` = '" + user_
                            + "' AND BINARY `pass` = '" + pass_ + "' LIMIT 1;");
                    if (!rs.next()) {
                        login_notice("Tài khoản mật khẩu không chính xác");
                        return;
                    }
                    this.lock = rs.getByte("lock");
                    if (this.lock != 0) {
                        login_notice("Tài khoản bị khóa, liên hệ admin để biết thêm chi tiết");
                        return;
                    }
                    //
                    JSONArray js = (JSONArray) JSONValue.parse(rs.getString("char"));
                    for (int i = 0; i < js.size(); i++) {
                        list_char.add(js.get(i).toString());
                    }
                } catch (SQLException e) {
                    e.printStackTrace();
                    return;
                } finally {
                    try {
                        if (rs != null) {
                            rs.close();
                        }
                        if (st != null) {
                            st.close();
                        }
                        if (conn != null) {
                            conn.close();
                        }
                    } catch (SQLException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
        //
        zoomlv = m.reader().readByte();
        this.version = m.reader().readUTF();
        m.reader().readByte();
        byte IndexCharSelected = m.reader().readByte();
        this.user = user_;
        this.pass = pass_;
        if (!this.check_onl()) {
            this.update_onl(1);
        } else {
            this.disconnect();
            for (int i = 0; i < SessionManager.CLIENT_ENTRYS.size(); i++) {
                Session ss = SessionManager.CLIENT_ENTRYS.get(i);
                if (ss.user != null && ss.user.equals(this.user)) {
                    ss.disconnect();
                }
            }
            login_notice("Tài khoản đang đăng nhập máy khác!");
            return;
        }
        boolean isDisconnect = false;
        for (int i = 0; i < list_char.size(); i++) {
            Player p0 = Map.get_player_by_name_allmap(list_char.get(i));
            if (p0 != null) {
                if (p0.conn != null) {
                    p0.conn.disconnect();
                    if (p0.map != null) {
                        p0.map.leave_map(p0, 0);
                    }
                }
                isDisconnect = true;
            }
        }
        if (isDisconnect) {
            this.disconnect();
            return;
        }
        Service.send_msg_data(this, 72, "data/msg/login/x2msg_72_638026480839986666", false);
        Service.send_msg_data(this, 72, "data/msg/login/x2msg_72_638026480840482549", false);
        Service.send_msg_data(this, 72, "data/msg/login/x2msg_72_638026480840808702", false);
        //
        if (this.zoomlv < 2) {
            Message m22 = new Message(-7);
            m22.writer().writeByte(15);
            m22.writer().writeShort(MobTemplate.ENTRYS.size());
            for (int i = 0; i < MobTemplate.ENTRYS.size(); i++) {
                MobTemplate temp = MobTemplate.ENTRYS.get(i);
                m22.writer().writeShort(temp.mob_id);
                m22.writer().writeUTF(temp.name);
                m22.writer().writeShort(temp.level);
                m22.writer().writeShort(temp.hOne);
                m22.writer().writeInt(temp.hp_max);
                m22.writer().writeByte(temp.typemove);
                m22.writer().writeByte(temp.ishuman);
                m22.writer().writeByte(temp.typemonster);
                if (temp.ishuman == 1) {
                    m22.writer().writeShort(temp.head);
                    m22.writer().writeShort(temp.hair);
                    m22.writer().writeByte(temp.wearing.length);
                    for (int j = 0; j < temp.wearing.length; j++) {
                        if (temp.wearing[j] != -1) {
                            m22.writer().writeByte(1);
                            m22.writer().writeShort(temp.wearing[j]);
                        } else {
                            m22.writer().writeByte(-1);
                        }
                    }
                } else {
                    m22.writer().writeShort(temp.icon);
                }
            }
            m22.writer().writeShort(DataTemplate.VerdataMon);
            this.addmsg(m22);
            m22.cleanup();
        }
        if (this.user.startsWith("htth_truongbk_")) {
            Message m2 = new Message(-57);
            m2.writer().writeUTF(user_);
            addmsg(m2);
            m2.cleanup();
        }
        send_list_char();
        Message m2 = new Message(-2);
        addmsg(m2);
        m2.cleanup();
        //
        if (IndexCharSelected != -1) {
            controller.login_into_char_select(IndexCharSelected);
        }
    }

    private void login_after_time(long l) throws IOException {
        Message m = new Message(-69);
        m.writer().writeUTF("Mời bạn đăng nhập lại sau thời gian");
        m.writer().writeShort((int) l);
        addmsg(m);
        m.cleanup();
    }

    private void send_list_char() throws IOException {
        Message m2 = new Message(-4);
        m2.writer().writeByte(list_char.size());
        for (int i = 0; i < list_char.size(); i++) {
            String name = list_char.get(i);
            Connection connection = null;
            PreparedStatement ps = null;
            ResultSet rs = null;
            try {
                connection = SQL.gI().getCon();
                ps = connection.prepareStatement(
                        "SELECT `clazz`, `level`, `body`, `it_body`, `fashion`, `site` FROM `players` WHERE `name` = '"
                                + name + "' LIMIT 1;");
                rs = ps.executeQuery();
                while (rs.next()) {
                    List<ItemFashionP2> fashion = new ArrayList<>();
                    List<ItemFashionP> itfashionP = new ArrayList<>();
                    JSONArray js0 = (JSONArray) JSONValue.parse(rs.getString("fashion"));
                    JSONArray js_temp_2 = (JSONArray) JSONValue.parse(js0.get(0).toString());
                    for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                        JSONArray js_temp =
                                (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                        ItemFashionP tempf = new ItemFashionP();
                        tempf.category = Byte.parseByte(js_temp.get(0).toString());
                        tempf.id = Short.parseShort(js_temp.get(1).toString());
                        tempf.icon = Short.parseShort(js_temp.get(2).toString());
                        tempf.is_use = Byte.parseByte(js_temp.get(3).toString()) == 1;
                        itfashionP.add(tempf);
                    }
                    js_temp_2.clear();
                    js_temp_2 = (JSONArray) JSONValue.parse(js0.get(1).toString());
                    for (int i0 = 0; i0 < js_temp_2.size(); i0++) {
                        JSONArray js_temp =
                                (JSONArray) JSONValue.parse(js_temp_2.get(i0).toString());
                        ItemFashionP2 tempf = new ItemFashionP2();
                        tempf.id = Short.parseShort(js_temp.get(0).toString());
                        tempf.is_use = Byte.parseByte(js_temp.get(1).toString()) == 1;
                        fashion.add(tempf);
                    }
                    js0.clear();
                    short hair_ = -1;
                    short head_ = -1;
                    short[] fashion_ = null;
                    for (int i0 = 0; i0 < fashion.size(); i0++) {
                        if (fashion.get(i0).is_use) {
                            ItemFashion temp = ItemFashion.get_item(fashion.get(i0).id);
                            if (temp != null) {
                                fashion_ = temp.mWearing;
                                break;
                            }
                        }
                    }
                    if (fashion_ != null && fashion_[6] != -1) {
                        // hair_ = -2;
                        hair_ = fashion_[7];
                        head_ = fashion_[6];
                    } else {
                        for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                            if (itfashionP.get(i0).category == 103 && itfashionP.get(i0).is_use) {
                                hair_ = itfashionP.get(i0).icon;
                            }
                        }
                        for (int i0 = 0; i0 < itfashionP.size(); i0++) {
                            if (itfashionP.get(i0).category == 108 && itfashionP.get(i0).is_use) {
                                head_ = itfashionP.get(i0).icon;
                            }
                        }
                    }
                    //
                    m2.writer().writeShort(i);
                    m2.writer().writeUTF(name);
                    m2.writer().writeByte(rs.getByte("clazz"));
                    JSONArray js_level = (JSONArray) JSONValue.parse(rs.getString("level"));
                    m2.writer().writeShort(Short.parseShort(js_level.get(0).toString()));
                    JSONArray js = (JSONArray) JSONValue.parse(rs.getString("body"));
                    m2.writer().writeShort(
                            (head_ != -1) ? head_ : Short.parseShort(js.get(0).toString()));
                    m2.writer().writeShort(
                            (hair_ != -1) ? hair_ : Short.parseShort(js.get(1).toString()));
                    js.clear();
                    m2.writer().writeShort(Clan.get_icon_clan(name)); // clan
                    m2.writer().writeByte(6);
                    //
                    Item_wear[] it = new Item_wear[8];
                    js = (JSONArray) JSONValue.parse(rs.getString("it_body"));
                    for (int i1 = 0; i1 < js.size(); i1++) {
                        JSONArray js2 = (JSONArray) JSONValue.parse(js.get(i1).toString());
                        Item_wear temp = new Item_wear();
                        Item.readUpdateItem(js2.toString(), temp);
                        it[temp.index] = temp;
                    }
                    js.clear();
                    js = (JSONArray) JSONValue.parse(rs.getString("site"));
                    boolean is_show_hat = Byte.parseByte(js.get(6).toString()) == 1;
                    js.clear();
                    //
                    for (int j = 0; j < 6; j++) {
                        if (it[j] == null) {
                            m2.writer().writeByte(0);
                        } else {
                            m2.writer().writeByte(1);
                            if (j == 1 && !is_show_hat) {
                                m2.writer().writeShort(-1);
                            } else if (fashion_ != null && fashion_[j] != -1) {
                                m2.writer().writeShort(fashion_[j]);
                            } else {
                                m2.writer().writeShort(
                                        ItemTemplate3.get_it_by_id(it[j].template.id).part);
                            }
                        }
                    }
                    m2.writer().writeByte(0);
                }
            } catch (SQLException e) {
                e.printStackTrace();
            } finally {
                try {
                    if (rs != null) {
                        rs.close();
                    }
                    if (ps != null) {
                        ps.close();
                    }
                    if (connection != null) {
                        connection.close();
                    }
                } catch (SQLException e) {
                    e.printStackTrace();
                }
            }
        }
        addmsg(m2);
        m2.cleanup();
    }

    private void login_notice(String s) throws IOException {
        Message m = new Message(-11);
        m.writer().writeShort(0);
        m.writer().writeByte(0);
        m.writer().writeUTF("Thông báo");
        m.writer().writeUTF(s);
        m.writer().writeByte(0);
        addmsg(m);
        m.cleanup();
    }

    public void ReadPartNew(Message m2) throws IOException {
        short index = m2.reader().readShort();
        Part part = Part.get_part(index);
        if (part != null) {
            Message m = new Message(-82);
            m.writer().writeShort(index);
            m.writer().writeByte(part.type);
            for (int i = 0; i < part.pi.length; i++) {
                m.writer().writeShort(part.pi[i].id);
                m.writer().writeByte(part.pi[i].dx);
                m.writer().writeByte(part.pi[i].dy);
            }
            addmsg(m);
            m.cleanup();
        }
    }

    public void create_char(Message m2) throws IOException {
        if (list_char.size() >= 1 ) {
            login_notice("Chỉ có thể tạo tối đa 1 nhân vật!");
            return;
        }
        String name = m2.reader().readUTF().replace(" ", "");
        name = name.toLowerCase();
        Pattern p = Pattern.compile("^[a-zA-Z0-9]{6,10}$");
        if (!p.matcher(name).matches()) {
            login_notice("Tên không hợp lệ, nhập lại đi!!");
            return;
        }
        byte clazz = m2.reader().readByte();
        int head = m2.reader().readShort();
        int hair = m2.reader().readShort();
        Connection connection = null;
        Statement st = null;
        try {
            connection = SQL.gI().getCon();
            st = connection.createStatement();
            String query =
                    "INSERT INTO `players` (`name`, `body`, `level`, `clazz`, `point_inven`, `site`, `bag3`, `it_body`, `potential`,"
                            + " `bag47`, `rms`, `skill`, `friend`, `enemy`, `fashion`, `eff`, `box3`, `box47`, `quest`, `date`,"
                            + " `pvppoint`, `save_it3`, `save_it47`, `hanhtrinh`, `wanted_point`, `wanted_chest`, `mypet`, `lucthuc`) "
                            + "VALUES ('%s', '%s', '%s', %s, '%s', '%s', '%s','%s','%s','%s','%s','%s','%s','%s','%s','%s',"
                            + "'%s','%s','%s','%s', %s, '%s', '%s', '%s', %s, '%s', '%s','%s')";
            String body_wear = "";
            String skill_by_clazz = "";
            String fashion_ = "";
            switch (clazz) {
                case 1: {
                    body_wear =
                            "[[0,0,1,-1,0,0,0,-1,[[1,61],[4,7]],[],0,[],0],[40,0,1,-1,0,0,0,-1,[[3,4],[15,1]],[],0,[],3],[80,0,1,-1,0,0,0,-1,[[3,3],[15,3]],[],0,[],5]]";
                    skill_by_clazz =
                            "[[0,0,0,0],[20,-1,0,0],[40,-1,0,0],[375,-1,0,0],[487,-1,0,0],[300,-1,0,0],[305,-1,0,0],[310,-1,0,0],[315,-1,0,0],[320,-1,0,0],[325,-1,0,0],[552,-1,0,0],[557,-1,0,0]]";
                    fashion_ = "[[[103,5,1,1],[108,0,0,1]],[],[[1,1],[5,1],[3,1],[7,1]]]";
                    break;
                }
                case 2: {
                    body_wear =
                            "[[8,0,1,-1,0,1,0,-1,[[1,72]],[],0,[],0],[48,0,1,-1,0,1,0,-1,[[3,2],[15,3]],[],0,[],3],[88,0,1,-1,0,1,0,-1,[[3,1],[15,5]],[],0,[],5]]";
                    skill_by_clazz =
                            "[[60,0,0,0],[80,-1,0,0],[100,-1,0,0],[395,-1,0,0],[492,-1,0,0],[300,-1,0,0],[305,-1,0,0],[310,-1,0,0],[315,-1,0,0],[320,-1,0,0],[325,-1,0,0],[552,-1,0,0],[557,-1,0,0]]";
                    fashion_ = "[[[103,1,24,1],[108,0,0,1]],[],[[1,1],[5,1],[3,1],[7,1]]]";
                    break;
                }
                case 3: {
                    body_wear =
                            "[[16,0,1,-1,0,1,0,-1,[[1,73]],[],0,[],0],[56,0,1,-1,0,0,0,-1,[[3,3],[15,2]],[],0,[],3],[96,0,1,-1,0,0,0,-1,[[3,3],[15,4]],[],0,[],5]]";
                    skill_by_clazz =
                            "[[120,0,0,0],[140,-1,0,0],[160,-1,0,0],[415,-1,0,0],[497,-1,0,0],[300,-1,0,0],[305,-1,0,0],[310,-1,0,0],[315,-1,0,0],[320,-1,0,0],[325,-1,0,0],[552,-1,0,0],[557,-1,0,0]]";
                    fashion_ = "[[[103,2,28,1],[108,0,0,1]],[],[[1,1],[5,1],[3,1],[7,1]]]";
                    break;
                }
                case 4: {
                    body_wear =
                            "[[24,0,1,-1,0,1,0,-1,[[1,55],[23,18]],[],0,[],0],[64,0,1,-1,0,0,0,-1,[[3,2],[15,3]],[],0,[],3],[104,0,1,-1,0,1,0,-1,[[3,1],[15,5]],[],0,[],5]]";
                    skill_by_clazz =
                            "[[180,0,0,0],[200,-1,0,0],[220,-1,0,0],[435,-1,0,0],[502,-1,0,0],[300,-1,0,0],[305,-1,0,0],[310,-1,0,0],[315,-1,0,0],[320,-1,0,0],[325,-1,0,0],[552,-1,0,0],[557,-1,0,0]]";
                    fashion_ = "[[[103,3,32,1],[108,0,0,1]],[],[[1,1],[5,1],[3,1],[7,1]]]";
                    break;
                }
                case 5: {
                    body_wear =
                            "[[32,0,1,-1,0,1,0,-1,[[1,66],[16,1]],[],0,[],0],[72,0,1,-1,0,0,0,-1,[[3,3],[15,2]],[],0,[],3],[112,0,1,-1,0,0,0,-1,[[3,2],[15,4]],[],0,[],5]]";
                    skill_by_clazz =
                            "[[240,0,0,0],[260,-1,0,0],[280,-1,0,0],[455,-1,0,0],[507,-1,0,0],[300,-1,0,0],[305,-1,0,0],[310,-1,0,0],[315,-1,0,0],[320,-1,0,0],[325,-1,0,0],[552,-1,0,0],[557,-1,0,0]]";
                    fashion_ = "[[[103,4,36,1],[108,0,0,1]],[],[[1,1],[5,1],[3,1],[7,1]]]";
                    break;
                }
            }
            query = String.format(query, name, "[" + head + "," + hair + "]", "[1,0,0]", clazz,
                    "[1000000,500,0,0,0,0,0,0,7,0,0,0]", "[0,0,-1,-1,300,300,1,0,20,0,3,0,3,0]",
                    "[]", body_wear, "[5,1,1,1,1,1,0,[]]", "[]",
                    "[[],[],[],[],[0,18],[],[],[],[],[],[]]", skill_by_clazz, "[]", "[]", fashion_,
                    "[]", "[]", "[]", "[[0,[]]]", DateTime.now(), 0, "[]", "[]", "[]", 0, "[]",
                    "[[0,1,1]]","[0,1,1,1,1,0]");
            st.execute(query);
        } catch (SQLException e) {
            e.printStackTrace();
            login_notice("tên đã được sử dụng!");
            return;
        } finally {
            try {
                if (st != null) {
                    st.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        list_char.add(name);
        flush();
        send_list_char();
    }

    @SuppressWarnings("unchecked")
    public void flush() {
        JSONArray js = new JSONArray();
        for (int i = 0; i < list_char.size(); i++) {
            js.add(list_char.get(i));
        }
        Connection conn = null;
        Statement st = null;
        try {
            conn = SQL.gI().getCon();
            st = conn.createStatement();
            st.executeUpdate("UPDATE `accounts` SET `char` = '" + js.toJSONString()
                    + "' WHERE BINARY `user` = '" + this.user + "' AND BINARY `pass` = '"
                    + this.pass + "';");
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (st != null) {
                    st.close();
                }
                if (conn != null) {
                    conn.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    public void update_onl(int type) {
        if (Manager.gI().server_admin) {
            type = 0;
        }
        Connection connection = null;
        Statement st = null;
        try {
            connection = SQL.gI().getCon();
            st = connection.createStatement();
            if (type == 0 && this.p != null) {
                st.executeUpdate(
                        "UPDATE `accounts` SET `onl` = " + type + " WHERE BINARY `user` = '"
                                + this.user + "' AND BINARY `pass` = '" + this.pass + "' LIMIT 1;");
            } else {
                st.executeUpdate(
                        "UPDATE `accounts` SET `onl` = " + type + " WHERE BINARY `user` = '"
                                + this.user + "' AND BINARY `pass` = '" + this.pass + "' LIMIT 1;");
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            try {
                if (st != null) {
                    st.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    public boolean check_onl() {
        Connection connection = null;
        Statement st = null;
        ResultSet rs = null;
        try {
            connection = SQL.gI().getCon();
            st = connection.createStatement();
            rs = st.executeQuery("SELECT `onl` FROM `accounts` WHERE BINARY `user` = '" + this.user
                    + "' AND BINARY `pass` = '" + this.pass + "' LIMIT 1;");
            if (!rs.next()) {
                return false;
            }
            if (rs.getBoolean("onl")) {
                return true;
            }
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
                if (st != null) {
                    st.close();
                }
                if (connection != null) {
                    connection.close();
                }
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
        return false;
    }

    public void clear_network(Session ss) {
        if (ss.sendd != null) {
            ss.sendd.interrupt();
            ss.sendd = null;
        }
        if (ss.receiv != null) {
            ss.receiv.interrupt();
            ss.receiv = null;
        }
        try {
            if (!ss.socket.isClosed()) {
                ss.socket.close();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void Check_Data_Ver() throws IOException {
        Message m = new Message(-6);
        m.writer().writeShort(DataTemplate.VerdataMon);
        m.writer().writeShort(DataTemplate.VerdataPotion);
        m.writer().writeShort(DataTemplate.VerdataAttri);
        m.writer().writeShort(-1);
        m.writer().writeShort(DataTemplate.VerdataNameMap);
        m.writer().writeShort(DataTemplate.VerdataNamePotionquest);
        m.writer().writeShort(-1);
        m.writer().writeShort(DataTemplate.VerdataImageSave);
        m.writer().writeShort(DataTemplate.VerdataUpgradeSave);
        m.writer().writeShort(DataTemplate.VerdataPotionClan);
        m.writer().writeShort(-1);
        addmsg(m);
        m.cleanup();
        // send item7
        m = new Message(-7);
        m.writer().writeByte(11);
        m.writer().writeByte(ItemTemplate7.ENTRYS.size());
        for (int i = 0; i < ItemTemplate7.ENTRYS.size(); i++) {
            ItemTemplate7 temp = ItemTemplate7.ENTRYS.get(i);
            m.writer().writeByte(temp.id);
            m.writer().writeUTF(temp.name);
            m.writer().writeByte(temp.type);
            m.writer().writeByte(temp.icon);
            m.writer().writeInt(temp.price);
            m.writer().writeShort(temp.priceruby);
            m.writer().writeByte(temp.istrade);
        }
        addmsg(m);
        m.cleanup();
    }
}
