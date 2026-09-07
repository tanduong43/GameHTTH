package core;

import database.SQL;
import io.Session;
import io.SessionManager;

/**
 * Quản lý tự động bảo trì định kỳ của Game Server.
 */
public class MaintenanceManager {

    public static boolean autoMaintenance = true;
    public static int maintenanceHour = 2;
    public static int maintenanceMinute = 0;
    public static volatile boolean isMaintenanceRunning = false;
    private static boolean initialAnnounced = false;

    /**
     * Gửi thông báo kênh thế giới có bắt ngoại lệ an toàn.
     */
    private static void sendNotice(String text) {
        try {
            Manager.gI().chatKTG(0, text, 5);
        } catch (Exception e) {
            System.err.println("[BẢO TRÌ] Lỗi gửi thông báo KTG: " + e.getMessage());
        }
    }

    /**
     * Được gọi mỗi giây từ thread_cal_time của ServerEventManager.
     */
    public static void checkAutoMaintenance(int hour, int min, int sec) {
        if (!autoMaintenance || isMaintenanceRunning) {
            return;
        }

        int targetTotalSec = maintenanceHour * 3600 + maintenanceMinute * 60;
        int currentTotalSec = hour * 3600 + min * 60 + sec;
        int diffSec = targetTotalSec - currentTotalSec;

        // Nếu qua giờ trong ngày, diffSec sẽ tính cho chu kỳ ngày hôm sau
        if (diffSec < 0) {
            diffSec += 86400;
        }

        // Thông báo ban đầu khi server vừa mở nếu còn dưới 5 phút đến giờ bảo trì
        if (!initialAnnounced && diffSec > 0 && diffSec < 300) {
            initialAnnounced = true;
            int m = diffSec / 60;
            int s = diffSec % 60;
            String timeText = (m > 0 ? (m + " phút ") : "") + (s > 0 ? (s + " giây") : "");
            sendNotice("Hệ thống sẽ bảo trì định kỳ sau " + timeText + " nữa, vui lòng chuẩn bị thoát game!");
            System.out.println("[BẢO TRÌ] Sắp đến giờ bảo trì định kỳ (" + maintenanceHour + ":"
                    + String.format("%02d", maintenanceMinute) + "), còn lại: " + timeText);
        }

        if (diffSec == 300) {
            sendNotice("Hệ thống sẽ tự động bảo trì sau 5 phút nữa, vui lòng đăng xuất để bảo đảm an toàn dữ liệu!");
            System.out.println("[BẢO TRÌ] Còn 5 phút nữa đến giờ bảo trì định kỳ (" + maintenanceHour + ":"
                    + String.format("%02d", maintenanceMinute) + ")");
        } else if (diffSec == 180) {
            sendNotice("Hệ thống sẽ tự động bảo trì sau 3 phút nữa!");
            System.out.println("[BẢO TRÌ] Còn 3 phút nữa đến giờ bảo trì!");
        } else if (diffSec == 120) {
            sendNotice("Hệ thống sẽ tự động bảo trì sau 2 phút nữa!");
            System.out.println("[BẢO TRÌ] Còn 2 phút nữa đến giờ bảo trì!");
        } else if (diffSec == 60) {
            sendNotice("Hệ thống sẽ tự động bảo trì sau 1 phút nữa!");
            System.out.println("[BẢO TRÌ] Còn 1 phút nữa đến giờ bảo trì!");
        } else if (diffSec == 30) {
            sendNotice("Hệ thống sẽ tự động bảo trì sau 30 giây nữa!");
            System.out.println("[BẢO TRÌ] Còn 30 giây nữa đến giờ bảo trì!");
        } else if (diffSec == 10) {
            sendNotice("Hệ thống sẽ tự động bảo trì sau 10 giây nữa!");
            System.out.println("[BẢO TRÌ] Còn 10 giây nữa đến giờ bảo trì!");
        } else if (diffSec <= 5 && diffSec > 0) {
            sendNotice("Hệ thống sẽ tự động bảo trì sau " + diffSec + " giây nữa!");
            System.out.println("[BẢO TRÌ] Còn " + diffSec + " giây...");
        } else if (diffSec == 0) {
            startShutdown();
        }
    }

    /**
     * Tiến hành lưu database, ngắt kết nối client và tắt server an toàn.
     */
    public static synchronized void startShutdown() {
        if (isMaintenanceRunning) {
            return;
        }
        isMaintenanceRunning = true;

        new Thread(() -> {
            try {
                System.out.println("\n=================================================");
                System.out.println("[BẢO TRÌ] BẮT ĐẦU QUY TRÌNH BẢO TRÌ ĐỊNH KỲ SERVER...");
                System.out.println("=================================================");
                sendNotice("BẮT ĐẦU BẢO TRÌ MÁY CHỦ, ĐANG LƯU DỮ LIỆU...");

                // 1. Lưu toàn bộ dữ liệu người chơi, bang hội, rương đồ
                System.out.println("[BẢO TRÌ] 1. Đang lưu database toàn bộ người chơi...");
                try {
                    SaveData.process();
                    System.out.println("[BẢO TRÌ] -> Lưu database thành công!");
                } catch (Exception e) {
                    System.err.println("[BẢO TRÌ] Lỗi khi lưu database: " + e.getMessage());
                }

                // 2. Ngắt kết nối tất cả người chơi an toàn
                System.out.println("[BẢO TRÌ] 2. Đang ngắt kết nối an toàn các client...");
                synchronized (SessionManager.CLIENT_ENTRYS) {
                    for (int i = SessionManager.CLIENT_ENTRYS.size() - 1; i >= 0; i--) {
                        try {
                            Session ss = SessionManager.CLIENT_ENTRYS.get(i);
                            if (ss != null) {
                                ss.p = null;
                                ss.disconnect();
                            }
                        } catch (Exception e) {}
                    }
                }
                System.out.println("[BẢO TRÌ] -> Đã ngắt kết nối tất cả người chơi!");

                // Đợi 2 giây để gửi các gói tin ngắt kết nối đến client
                Thread.sleep(2000L);

                System.out.println("[BẢO TRÌ] MÁY CHỦ ĐÃ TẮT HOÀN TẤT. EXIT JVM!");
                System.out.println("=================================================");
                System.exit(0);
            } catch (Exception e) {
                e.printStackTrace();
                System.exit(0);
            }
        }).start();
    }
}
