public class GMath
{
	public static MyRandom r = new MyRandom();

	public static int random(int a, int b)
	{
		return a + r.nextInt(b - a);
	}

	public static double pitago(int dx, int dy)
	{
		return sqrt(dx * dx + dy * dy);
	}

	public static int s2tick(int currentTimeMillis)
	{
		int num = 0;
		num = currentTimeMillis * 16 / 1000;
		if (currentTimeMillis * 16 % 1000 >= 5)
		{
			num++;
		}
		return num;
	}

	public static int distance(int a, int b)
	{
		return abs(a - b);
	}

	public static int distance(int x1, int y1, int x2, int y2)
	{
		return sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
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

	public static int rnd(int a)
	{
		return r.nextInt(a);
	}

	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
	{
		return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
	}

	public static string[] split(string original, string separator, int count)
	{
		int num = original.IndexOf(separator);
		string[] array;
		if (num >= 0)
		{
			array = split(original.Substring(num + separator.Length), separator, count + 1);
		}
		else
		{
			array = new string[count + 1];
			num = original.Length;
		}
		array[count] = original.Substring(0, num);
		return array;
	}
}
