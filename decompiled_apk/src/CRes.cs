using System;

public class CRes
{
	private static short[] sin = new short[91]
	{
		0, 18, 36, 54, 71, 89, 107, 125, 143, 160,
		178, 195, 213, 230, 248, 265, 282, 299, 316, 333,
		350, 367, 384, 400, 416, 433, 449, 465, 481, 496,
		512, 527, 543, 558, 573, 587, 602, 616, 630, 644,
		658, 672, 685, 698, 711, 724, 737, 749, 761, 773,
		784, 796, 807, 818, 828, 839, 849, 859, 868, 878,
		887, 896, 904, 912, 920, 928, 935, 943, 949, 956,
		962, 968, 974, 979, 984, 989, 994, 998, 1002, 1005,
		1008, 1011, 1014, 1016, 1018, 1020, 1022, 1023, 1023, 1024,
		1024
	};

	public static MyHashTable hashWeapon = new MyHashTable();

	public static short[] cos;

	public static int[] tan;

	public const string sign = "checkmodhaitac:android:isolatedSplits=true";

	public static MyRandom r = new MyRandom();

	public static void loadSinCos()
	{
		cos = new short[91];
		tan = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			cos[i] = sin[90 - i];
			if (cos[i] == 0)
			{
				tan[i] = 1000000000;
			}
			else
			{
				tan[i] = (sin[i] << 10) / cos[i];
			}
		}
	}

	public static int getsin(int a)
	{
		a = fixangle(a);
		if (a >= 0 && a < 90)
		{
			return sin[a];
		}
		if (a >= 90 && a < 180)
		{
			return sin[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return -sin[a - 180];
		}
		return -sin[360 - a];
	}

	public static int getcos(int a)
	{
		a = fixangle(a);
		if (a >= 0 && a < 90)
		{
			return cos[a];
		}
		if (a >= 90 && a < 180)
		{
			return -cos[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return -cos[a - 180];
		}
		return cos[360 - a];
	}

	public static int gettan(int a)
	{
		if (a >= 0 && a < 90)
		{
			return tan[a];
		}
		if (a >= 90 && a < 180)
		{
			return -tan[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return tan[a - 180];
		}
		return -tan[360 - a];
	}

	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			if (tan[i] >= a)
			{
				return i;
			}
		}
		return 0;
	}

	public static int angle(int dx, int dy)
	{
		int num;
		if (dx != 0)
		{
			num = atan(abs((dy << 10) / dx));
			if (dy >= 0 && dx < 0)
			{
				num = 180 - num;
			}
			if (dy < 0 && dx < 0)
			{
				num = 180 + num;
			}
			if (dy < 0 && dx >= 0)
			{
				num = 360 - num;
			}
		}
		else
		{
			num = ((dy <= 0) ? 270 : 90);
		}
		return num;
	}

	public static int fixangle(int angle)
	{
		if (angle >= 360)
		{
			angle %= 360;
		}
		if (angle < 0)
		{
			angle = 360 + angle % 360;
		}
		return angle;
	}

	public static int subangle(int a1, int a2)
	{
		int num = a2 - a1;
		if (num < -180)
		{
			return num + 360;
		}
		if (num > 180)
		{
			return num - 360;
		}
		return num;
	}

	public static int abs(int a)
	{
		if (a < 0)
		{
			return -a;
		}
		return a;
	}

	public static long absLong(long a)
	{
		if (a < 0)
		{
			return -a;
		}
		return a;
	}

	public static int random(int a)
	{
		if (a <= 1)
		{
			mSystem.outloi("random <0");
			return 0;
		}
		return r.nextInt(a);
	}

	public static int random_Am_0(int a)
	{
		if (a <= 1)
		{
			a = 1;
			mSystem.outloi("random a<=0");
		}
		int num = 0;
		while (num == 0)
		{
			num = r.nextInt(a);
			if (random(2) == 0)
			{
				num = -num;
			}
		}
		return num;
	}

	public static int random_Am(int a, int b)
	{
		if (b <= a)
		{
			mSystem.outloi("lỗi random am b<=a");
			b = a + 1;
		}
		int num = a + r.nextInt(b - a);
		if (random(2) == 0)
		{
			num = -num;
		}
		return num;
	}

	public static int random(int a, int b)
	{
		if (b <= a)
		{
			mSystem.outloi("lỗi random b<=a");
			b = a + 1;
		}
		return a + r.nextInt(b - a);
	}

	public static int sqrt(int a)
	{
		if (a <= 0)
		{
			return 0;
		}
		int num = (a + 1) / 2;
		int num2;
		do
		{
			num2 = num;
			num = num / 2 + a / (2 * num);
		}
		while (abs(num2 - num) > 1);
		return num;
	}

	public static int setDis(int x1, int y1, int x2, int y2)
	{
		return abs(x1 - x2) + abs(y1 - y2);
	}

	public static void saveRMS(string name, sbyte[] data)
	{
		try
		{
			GameMidlet.saveRMS(name, data);
		}
		catch (Exception)
		{
		}
	}

	public static sbyte[] loadRMS(string name)
	{
		return GameMidlet.loadRMS(name);
	}

	public static void saveRMSPlayer(string name, sbyte[] data)
	{
		try
		{
			if (GameScreen.player != null)
			{
				GameMidlet.saveRMS(GameScreen.player.name + name, data);
			}
		}
		catch (Exception)
		{
		}
	}

	public static sbyte[] loadRMSPlayer(string name)
	{
		if (GameScreen.player != null)
		{
			return GameMidlet.loadRMS(GameScreen.player.name + name);
		}
		return null;
	}

	public static void quickSortMemList(mVector actors)
	{
		recQuickSortMemList(actors, 0, actors.size() - 1);
	}

	private static void recQuickSortMemList(mVector actors, int left, int right)
	{
		try
		{
			if (right - left > 0)
			{
				int rank = ((InfoMemList)actors.elementAt(right)).rank;
				int num = partitionItMemList(actors, left, right, rank);
				recQuickSortMemList(actors, left, num - 1);
				recQuickSortMemList(actors, num + 1, right);
			}
		}
		catch (Exception)
		{
			mSystem.outloi("loi Cres 1");
		}
	}

	private static int partitionItMemList(mVector actors, int left, int right, int pivot)
	{
		int num = left - 1;
		int num2 = right;
		try
		{
			while (true)
			{
				if (((InfoMemList)actors.elementAt(++num)).rank >= pivot)
				{
					while (num2 > 0 && ((InfoMemList)actors.elementAt(--num2)).rank > pivot)
					{
					}
					if (num >= num2)
					{
						break;
					}
					swapMemList(actors, num, num2);
				}
			}
			swapMemList(actors, num, right);
		}
		catch (Exception)
		{
			mSystem.outloi("loi Cres 2");
		}
		return num;
	}

	private static void swapMemList(mVector actors, int dex1, int dex2)
	{
		object obj = actors.elementAt(dex2);
		if (((InfoMemList)actors.elementAt(dex2)).rank != ((InfoMemList)actors.elementAt(dex1)).rank)
		{
			actors.setElementAt(actors.elementAt(dex1), dex2);
			actors.setElementAt(obj, dex1);
		}
	}

	public static void quickSort(mVector actors)
	{
		recQuickSort(actors, 0, actors.size() - 1);
	}

	private static void recQuickSort(mVector actors, int left, int right)
	{
		try
		{
			if (right - left > 0)
			{
				int ySort = ((MainObject)actors.elementAt(right)).ySort;
				int num = partitionIt(actors, left, right, ySort);
				recQuickSort(actors, left, num - 1);
				recQuickSort(actors, num + 1, right);
			}
		}
		catch (Exception)
		{
			mSystem.outloi("loi Cres 1");
		}
	}

	private static int partitionIt(mVector actors, int left, int right, int pivot)
	{
		int num = left - 1;
		int num2 = right;
		try
		{
			while (true)
			{
				if (((MainObject)actors.elementAt(++num)).ySort >= pivot)
				{
					while (num2 > 0 && ((MainObject)actors.elementAt(--num2)).ySort > pivot)
					{
					}
					if (num >= num2)
					{
						break;
					}
					swap(actors, num, num2);
				}
			}
			swap(actors, num, right);
		}
		catch (Exception)
		{
			mSystem.outloi("loi Cres 2");
		}
		return num;
	}

	private static void swap(mVector actors, int dex1, int dex2)
	{
		object obj = actors.elementAt(dex2);
		if (((MainObject)actors.elementAt(dex2)).ySort != ((MainObject)actors.elementAt(dex1)).ySort)
		{
			actors.setElementAt(actors.elementAt(dex1), dex2);
			actors.setElementAt(obj, dex1);
		}
	}

	public static bool CheckDelRMS(string str)
	{
		if (str.Length >= 9 && str.Substring(0, 9).CompareTo("SUB_image") == 0)
		{
			return true;
		}
		return false;
	}

	public static string getDay(int time)
	{
		if (time >= 1440)
		{
			return time / 1440 + " " + T.day;
		}
		if (time > 60)
		{
			return time / 60 + " " + T.hour;
		}
		return time + " " + T.minute;
	}

	public static bool setVaCham(int x, int y, int w, int h, int x2, int y2, int w2, int h2)
	{
		if ((x >= x2 && x <= x2 + w2 && y > y2 && y < y2 + h2) || (x2 >= x && x2 <= x + w && y2 >= y && y2 <= y + h) || (x >= x2 && x <= x2 + w2 && y2 >= y && y2 <= y + h) || (x2 >= x && x2 <= x + w && y > y2 && y < y2 + h2))
		{
			return true;
		}
		return false;
	}

	public static bool setVaCham(Boat b, Boat b2)
	{
		return setVaCham(b.xset, b.yset, b.wset, b.hset, b2.xset, b2.yset, b2.wset, b2.hset);
	}

	public static bool checkNumber(string number)
	{
		try
		{
			int.Parse(number);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
