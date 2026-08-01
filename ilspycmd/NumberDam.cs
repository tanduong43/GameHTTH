public class NumberDam
{
	public static FrameImage fraImgSmallYellow;

	public static FrameImage fraImgSmallRed;

	public static FrameImage fraImgSmallWhite;

	public static FrameImage fraImgCri;

	public static FrameImage fraImgMiss;

	public static FrameImage fraImgBigRed;

	public static FrameImage fraImgWanted;

	public static FrameImage fraImgSmallViolet;

	public static FrameImage fraImgSmallGreen;

	public static FrameImage fraImgSmallEffRed;

	public static FrameImage fraImgSmallEffBlue;

	public static FrameImage fraImgSuperBig;

	public static FrameImage fraImgHapThu;

	public static void paintSmall(mGraphics g, int value, int x, int y, int type)
	{
		FrameImage fraNum = getFraNum(type);
		if (fraNum == null)
		{
			loadImg(type);
			return;
		}
		int num = CRes.abs(value);
		sbyte b = checkLenght(num);
		int num2 = 8;
		int num3 = 0;
		switch (type)
		{
		case 16:
		case 25:
			num3 += 16;
			num2 = 12;
			break;
		case 23:
			num2 = 15;
			break;
		}
		int num4 = 10;
		num3 += (b - 1) * (num2 / 2);
		if (value < 0)
		{
			num3 += 5;
		}
		for (int i = 0; i < b; i++)
		{
			int num5 = num % num4;
			int idx = (sbyte)(num5 / (num4 / 10));
			num4 *= 10;
			fraNum.drawFrameNew(idx, x + num3, y, 0, 3, g);
			num3 -= num2;
			if (type == 23 && i % 3 == 2)
			{
				num3 -= 2;
			}
		}
		if (value < 0)
		{
			fraNum.drawFrameNew(11, x + num3 - 2, y, 0, 3, g);
		}
		switch (type)
		{
		case 16:
			num3 -= 10;
			if (fraImgCri == null)
			{
				loadImg(type);
			}
			else
			{
				fraImgCri.drawFrame(GameCanvas.gameTick / 3 % fraImgCri.nFrame, x + num3, y, 0, 3, g);
			}
			break;
		case 25:
			num3 -= 28;
			if (fraImgHapThu == null)
			{
				loadImg(type);
			}
			else
			{
				fraImgHapThu.drawFrame(GameCanvas.gameTick / 4 % fraImgHapThu.nFrame, x + num3, y, 0, 3, g);
			}
			break;
		}
	}

	public static void paintSmallWanted(mGraphics g, int value, int x, int y)
	{
		FrameImage fraNum = getFraNum(18);
		if (fraNum == null)
		{
			loadImg(18);
			return;
		}
		long num = (long)value * 100L;
		sbyte b = checkLenght(num);
		int num2 = 8;
		int num3 = 0;
		long num4 = 10L;
		num3 += (b - 1) * (num2 / 2) + (b - 1) / 3 * 3;
		for (int i = 0; i < b; i++)
		{
			long num5 = num % num4;
			int idx = (int)(num5 / (num4 / 10));
			num4 *= 10;
			fraNum.drawFrameNew(idx, x + num3, y, 0, 3, g);
			if (i % 3 == 2 && i < b - 1)
			{
				num3 -= 3;
				fraNum.drawFrameNew(10, x + num3, y, 0, 3, g);
			}
			num3 -= num2;
		}
	}

	public static void paintStringBig(mGraphics g, int value, int x, int y, int type)
	{
		if (type == 17)
		{
			if (fraImgMiss == null)
			{
				loadImg(type);
			}
			else
			{
				fraImgMiss.drawFrameNew(0, x, y, 0, 3, g);
			}
		}
	}

	private static FrameImage getFraNum(int value)
	{
		return value switch
		{
			13 => fraImgSmallYellow, 
			14 => fraImgSmallRed, 
			15 => fraImgSmallWhite, 
			19 => fraImgSmallViolet, 
			20 => fraImgSmallGreen, 
			22 => fraImgSmallEffBlue, 
			21 => fraImgSmallEffRed, 
			16 => fraImgBigRed, 
			23 => fraImgSuperBig, 
			18 => fraImgWanted, 
			25 => fraImgSmallGreen, 
			_ => null, 
		};
	}

	public static sbyte checkLenght(long value)
	{
		if (value < 10)
		{
			return 1;
		}
		if (value < 100)
		{
			return 2;
		}
		if (value < 1000)
		{
			return 3;
		}
		if (value < 10000)
		{
			return 4;
		}
		if (value < 100000)
		{
			return 5;
		}
		if (value < 1000000)
		{
			return 6;
		}
		if (value < 10000000)
		{
			return 7;
		}
		if (value < 100000000)
		{
			return 8;
		}
		if (value < 1000000000)
		{
			return 9;
		}
		if (value < 10000000000L)
		{
			return 10;
		}
		return 11;
	}

	private static void loadImg(int value)
	{
		switch (value)
		{
		case 13:
			fraImgSmallYellow = new FrameImage(237, 12, 12, 5);
			break;
		case 14:
			fraImgSmallRed = new FrameImage(235, 12, 12, 5);
			break;
		case 15:
			fraImgSmallWhite = new FrameImage(236, 12, 12, 5);
			break;
		case 17:
			fraImgMiss = new FrameImage(232, 47, 14);
			break;
		case 19:
			fraImgSmallViolet = new FrameImage(234, 12, 12, 5);
			break;
		case 20:
			fraImgSmallGreen = new FrameImage(228, 12, 12, 5);
			break;
		case 22:
			fraImgSmallEffBlue = new FrameImage(229, 12, 12, 5);
			break;
		case 21:
			fraImgSmallEffRed = new FrameImage(230, 12, 12, 5);
			break;
		case 16:
		case 23:
			fraImgSuperBig = new FrameImage(390, 22, 22, 5);
			fraImgBigRed = new FrameImage(231, 16, 16, 5);
			fraImgCri = new FrameImage(233, 32, 14);
			break;
		case 18:
			fraImgWanted = new FrameImage(mImage.createImage("/interface/numwanted.png"), 7, 11, 5);
			break;
		case 25:
			fraImgSmallGreen = new FrameImage(228, 12, 12, 5);
			fraImgHapThu = new FrameImage(399, 61, 16);
			break;
		}
	}

	public static void paintSmallWantedPoster(mGraphics g, int value, int x, int y)
	{
		FrameImage fraNum = getFraNum(18);
		if (fraNum == null)
		{
			loadImg(18);
			return;
		}
		long num = value;
		sbyte b = checkLenght(num);
		int num2 = 8;
		int num3 = 0;
		long num4 = 10L;
		num3 += (b - 1) * (num2 / 2) + (b - 1) / 3 * 3;
		for (int i = 0; i < b; i++)
		{
			int idx = (int)(num % num4 / (num4 / 10));
			num4 *= 10;
			fraNum.drawFrameNew(idx, x + num3, y, 0, 3, g);
			if (i % 3 == 2 && i < b - 1)
			{
				num3 -= 3;
				fraNum.drawFrameNew(10, x + num3, y, 0, 3, g);
			}
			num3 -= num2;
		}
	}
}
