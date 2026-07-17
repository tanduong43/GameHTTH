namespace hqx;

public class Interpolation
{
	private const uint Mask4 = 4278190080u;

	private const uint Mask2 = 65280u;

	private const uint Mask13 = 16711935u;

	public static int Mix3To1(int c1, int c2)
	{
		if (c1 == c2)
		{
			return c1;
		}
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 3 + ((ulong)c2 & 0xFF00uL)) >> 2) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 3 + ((ulong)c2 & 0xFF00FFuL)) >> 2) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 2) * 3 + ((c2 & 0xFF000000u) >> 2)) & 0xFF000000u));
	}

	public static int Mix2To1To1(int c1, int c2, int c3)
	{
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 2 + ((ulong)c2 & 0xFF00uL) + ((ulong)c3 & 0xFF00uL)) >> 2) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 2 + ((ulong)c2 & 0xFF00FFuL) + ((ulong)c3 & 0xFF00FFuL)) >> 2) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 2) * 2 + ((c2 & 0xFF000000u) >> 2) + ((c3 & 0xFF000000u) >> 2)) & 0xFF000000u));
	}

	public static int Mix7To1(int c1, int c2)
	{
		if (c1 == c2)
		{
			return c1;
		}
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 7 + ((ulong)c2 & 0xFF00uL)) >> 3) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 7 + ((ulong)c2 & 0xFF00FFuL)) >> 3) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 3) * 7 + ((c2 & 0xFF000000u) >> 3)) & 0xFF000000u));
	}

	public static int Mix2To7To7(int c1, int c2, int c3)
	{
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 2 + ((ulong)c2 & 0xFF00uL) * 7 + ((ulong)c3 & 0xFF00uL) * 7) >> 4) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 2 + ((ulong)c2 & 0xFF00FFuL) * 7 + ((ulong)c3 & 0xFF00FFuL) * 7) >> 4) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 4) * 2 + ((c2 & 0xFF000000u) >> 4) * 7 + ((c3 & 0xFF000000u) >> 4) * 7) & 0xFF000000u));
	}

	public static int MixEven(int c1, int c2)
	{
		if (c1 == c2)
		{
			return c1;
		}
		return (int)((((long)(((ulong)c1 & 0xFF00uL) + ((ulong)c2 & 0xFF00uL)) >> 1) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) + ((ulong)c2 & 0xFF00FFuL)) >> 1) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 1) + ((c2 & 0xFF000000u) >> 1)) & 0xFF000000u));
	}

	public static int Mix4To2To1(int c1, int c2, int c3)
	{
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 5 + ((ulong)c2 & 0xFF00uL) * 2 + ((ulong)c3 & 0xFF00uL)) >> 3) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 5 + ((ulong)c2 & 0xFF00FFuL) * 2 + ((ulong)c3 & 0xFF00FFuL)) >> 3) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 3) * 5 + ((c2 & 0xFF000000u) >> 3) * 2 + ((c3 & 0xFF000000u) >> 3)) & 0xFF000000u));
	}

	public static int Mix6To1To1(int c1, int c2, int c3)
	{
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 6 + ((ulong)c2 & 0xFF00uL) + ((ulong)c3 & 0xFF00uL)) >> 3) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 6 + ((ulong)c2 & 0xFF00FFuL) + ((ulong)c3 & 0xFF00FFuL)) >> 3) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 3) * 6 + ((c2 & 0xFF000000u) >> 3) + ((c3 & 0xFF000000u) >> 3)) & 0xFF000000u));
	}

	public static int Mix5To3(int c1, int c2)
	{
		if (c1 == c2)
		{
			return c1;
		}
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 5 + ((ulong)c2 & 0xFF00uL) * 3) >> 3) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 5 + ((ulong)c2 & 0xFF00FFuL) * 3) >> 3) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 3) * 5 + ((c2 & 0xFF000000u) >> 3) * 3) & 0xFF000000u));
	}

	public static int Mix2To3To3(int c1, int c2, int c3)
	{
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 2 + ((ulong)c2 & 0xFF00uL) * 3 + ((ulong)c3 & 0xFF00uL) * 3) >> 3) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 2 + ((ulong)c2 & 0xFF00FFuL) * 3 + ((ulong)c3 & 0xFF00FFuL) * 3) >> 3) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 3) * 2 + ((c2 & 0xFF000000u) >> 3) * 3 + ((c3 & 0xFF000000u) >> 3) * 3) & 0xFF000000u));
	}

	public static int Mix14To1To1(int c1, int c2, int c3)
	{
		return (int)((((long)(((ulong)c1 & 0xFF00uL) * 14 + ((ulong)c2 & 0xFF00uL) + ((ulong)c3 & 0xFF00uL)) >> 4) & 0xFF00) | (((long)(((ulong)c1 & 0xFF00FFuL) * 14 + ((ulong)c2 & 0xFF00FFuL) + ((ulong)c3 & 0xFF00FFuL)) >> 4) & 0xFF00FF) | ((((c1 & 0xFF000000u) >> 4) * 14 + ((c2 & 0xFF000000u) >> 4) + ((c3 & 0xFF000000u) >> 4)) & 0xFF000000u));
	}
}
