package core;

import org.joda.time.DateTime;
import template.Top_Dame;
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

    public synchronized static byte[] loadfile(String url) throws IOException {
        FileInputStream fis = null;
        try {
            fis = new FileInputStream(url);
            byte[] ab = new byte[fis.available()];
            fis.read(ab, 0, ab.length);
            return ab;
        } finally {
            if (fis != null) {
                fis.close();
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
        return new ArrayList<Top_Dame>();
    }

    public static String number_format(long n) {
        return (NumberFormat.getInstance(Locale.ITALY).format(n));
    }
}
