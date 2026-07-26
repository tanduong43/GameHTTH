package client;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

import activities.BossHunt;
import activities.HangDong;
import activities.NamieTreasureDefense;
import activities.TowerChallenge;
import map.Map;

public class ReconnectSession {
    public static final ConcurrentHashMap<String, ReconnectSession> SESSIONS = new ConcurrentHashMap<>();
    private static final ScheduledExecutorService SCHEDULER = Executors.newScheduledThreadPool(1, r -> {
        Thread t = new Thread(r, "ReconnectSession-Timer");
        t.setDaemon(true);
        return t;
    });

    public String playerName;
    public Party party;
    public Object dungeon; // HangDong, TowerChallenge, NamieTreasureDefense
    public BossHunt bossHunt;
    private ScheduledFuture<?> expireTask;
    public Map oldMap; // Bản đồ cũ trước khi mất kết nối
    public int originalMapId = -1;
    public short originalX = -1;
    public short originalY = -1;

    public static void create(Player p) {
        if (p == null || p.name == null) {
            return;
        }
        if (SESSIONS.containsKey(p.name)) {
            SESSIONS.get(p.name).cancelTask();
        }

        ReconnectSession session = new ReconnectSession();
        session.playerName = p.name;
        session.party = p.party;
        session.dungeon = p.dungeon;
        session.bossHunt = p.bossHunt;
        session.oldMap = p.map;
        session.originalMapId = p.originalMapId;
        session.originalX = p.originalX;
        session.originalY = p.originalY;

        // Xóa tạm khỏi party mà không thông báo cho dungeon
        if (p.party != null) {
            p.party.temp_remove(p);
            p.party = null; // Gỡ reference ở player để lúc xóa thật không bị vòng lặp
        }

        SESSIONS.put(p.name, session);
        System.out.println("[Reconnect] Player " + p.name + " disconnected. Starting 30s reconnect timer.");

        session.expireTask = SCHEDULER.schedule(() -> {
            session.expire(p);
        }, 30, TimeUnit.SECONDS);
    }

    public void cancelTask() {
        if (this.expireTask != null && !this.expireTask.isDone()) {
            this.expireTask.cancel(false);
        }
    }

    private void expire(Player oldPReference) {
        SESSIONS.remove(this.playerName);
        System.out.println("[Reconnect] Timer expired for " + this.playerName + ". Cleaning up.");

        // Nếu còn dungeon, coi như rời hẳn
        if (this.dungeon != null) {
            if (this.dungeon instanceof HangDong) {
                ((HangDong) this.dungeon).handlePlayerLeftParty(oldPReference);
            } else if (this.dungeon instanceof TowerChallenge) {
                ((TowerChallenge) this.dungeon).handlePlayerLeftParty(oldPReference);
            } else if (this.dungeon instanceof NamieTreasureDefense) {
                ((NamieTreasureDefense) this.dungeon).handlePlayerLeftParty(oldPReference);
            }
        }

        if (this.bossHunt != null) {
            this.bossHunt.handlePlayerLeftParty(oldPReference);
        }

        // Nếu còn party, coi như rời hẳn
        if (this.party != null) {
            try {
                boolean inDungeon = false;
                if (this.party.list.size() == 1) {
                    Player remaining = this.party.list.get(0);
                    if (remaining.dungeon != null || remaining.bossHunt != null) {
                        inDungeon = true;
                    }
                }
                if (this.party.list.size() < 1 || (this.party.list.size() < 2 && !inDungeon)) {
                    this.party.delete();
                } else {
                    this.party.send_info();
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        // Đảm bảo rác không còn
        this.party = null;
        this.dungeon = null;
        this.bossHunt = null;
    }

    public static ReconnectSession getAndRemove(String playerName) {
        if (playerName == null) return null;
        ReconnectSession session = SESSIONS.remove(playerName);
        if (session != null) {
            session.cancelTask();
        }
        return session;
    }
}
