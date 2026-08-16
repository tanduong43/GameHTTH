package core;

import org.joda.time.DateTime;
import template.Top_Dame;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.Random;

/**
 *
 * @author Truongbk
 */
public class Util {
    private static final Random random = new Random();

    public synchronized static byte[] loadfile(String url) {
        if (url == null || url.isEmpty()) {
            return null;
        }
        File f = new File(url);
        if (!f.exists() || !f.isFile()) {
            return null;
        }
        FileInputStream fis = null;
        try {
            fis = new FileInputStream(f);
            byte[] ab = new byte[(int) f.length()];
            int bytesRead = 0;
            while (bytesRead < ab.length) {
                int read = fis.read(ab, bytesRead, ab.length - bytesRead);
                if (read == -1)
                    break;
                bytesRead += read;
            }
            return ab;
        } catch (Exception e) {
            return null;
        } finally {
            if (fis != null) {
                try {
                    fis.close();
                } catch (IOException ignored) {
                }
            }
        }
    }

    public static String get_time_str_by_sec2(long time_ship) {
        time_ship /= 1000;
        int input = (int) time_ship;
        int numberOfDays;
        int numberOfHours;
        int numberOfMinutes;
        int numberOfSeconds;
        numberOfDays = input / 86400;
        numberOfHours = (input % 86400) / 3600;
        numberOfMinutes = ((input % 86400) % 3600) / 60;
        numberOfSeconds = ((input % 86400) % 3600) % 60;
        return String.format("%sd %sh %sp %ss", numberOfDays, numberOfHours, numberOfMinutes,
                numberOfSeconds);
    }

    public static boolean is_DayofWeek(int day) {
        // thu2 = 1 ->
        // thu3 = 2->
        // thu4 = 3 ->
        // thu5 = 4 ->
        // thu6 = 5 ->
        // thu7 = 6 ->
        // chu nhat = 7
        DateTime dateTime = DateTime.now();
        return dateTime.getDayOfWeek() == day;
    }

    public static int random(int a1, int a2) {
        return random.nextInt(a1, a2);
    }

    public static int random(int a2) {
        return random.nextInt(a2);
    }

    public static boolean isnumber(String txt) {
        try {
            Integer.valueOf(txt);
            return true;
        } catch (NumberFormatException e) {
            return false;
        }
    }

    public static boolean is_same_day(DateTime now, DateTime d) {
        String strDate_1 = now.toString().split("T")[0];
        String strDate_2 = d.toString().split("T")[0];
        return strDate_1.equals(strDate_2);
    }

    public synchronized static List<Top_Dame> sort(List<Top_Dame> list_select) {
        List<Top_Dame> result = new ArrayList<Top_Dame>(list_select);
        result.sort((a, b) -> Long.compare(b.dame, a.dame)); // dame cao nhất lên đầu
        return result;
    }

    public static String number_format(long n) {
        return (NumberFormat.getInstance(Locale.ITALY).format(n));
    }

    public static String format_short(long n) {
        boolean isNegative = n < 0;
        long val = isNegative ? -n : n;
        String res;
        if (val >= 1_000_000_000L) {
            long b = val / 1_000_000_000L;
            long dec = (val % 1_000_000_000L) / 100_000_000L;
            res = (dec > 0 && b < 100) ? (b + "." + dec + "B") : (b + "B");
        } else if (val >= 1_000_000L) {
            long m = val / 1_000_000L;
            long dec = (val % 1_000_000L) / 100_000L;
            res = (dec > 0 && m < 100) ? (m + "." + dec + "M") : (m + "M");
        } else if (val >= 1_000L) {
            long k = val / 1_000L;
            long dec = (val % 1_000L) / 100L;
            res = (dec > 0 && k < 10) ? (k + "." + dec + "K") : (k + "K");
        } else {
            res = String.valueOf(val);
        }
        return isNegative ? "-" + res : res;
    }
}
