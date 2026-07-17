namespace hqx;

public class RgbYuv
{
	private const int rgbMask = 16777215;

	private static int[] RGBtoYUV;

	public static int getYuv(int rgb)
	{
		return rgb2yuv(rgb);
	}

	public static void hqxInit()
	{
		for (int num = 16777215; num >= 0; num--)
		{
			int num2 = (num & 0xFF0000) >> 16;
			int num3 = (num & 0xFF00) >> 8;
			int num4 = num & 0xFF;
			int num5 = (int)(0.299 * (double)num2 + 0.587 * (double)num3 + 0.114 * (double)num4);
			int num6 = (int)(-0.169 * (double)num2 - 0.331 * (double)num3 + 0.5 * (double)num4) + 128;
			int num7 = (int)(0.5 * (double)num2 - 0.419 * (double)num3 - 0.081 * (double)num4) + 128;
			RGBtoYUV[num] = (num5 << 16) | (num6 << 8) | num7;
		}
	}

	public static int rgb2yuv(int c)
	{
		byte b = (byte)((c & 0xFF0000) >> 16);
		byte b2 = (byte)((c & 0xFF00) >> 8);
		byte b3 = (byte)((uint)c & 0xFFu);
		byte b4 = (byte)(0.299 * (double)(int)b + 0.587 * (double)(int)b2 + 0.114 * (double)(int)b3);
		byte b5 = (byte)(-0.169 * (double)(int)b - 0.331 * (double)(int)b2 + 0.5 * (double)(int)b3 + 128.0);
		byte b6 = (byte)(0.5 * (double)(int)b - 0.419 * (double)(int)b2 - 0.081 * (double)(int)b3 + 128.0);
		return (b4 << 16) | (b5 << 8) | b6;
	}

	public static void hqxDeinit()
	{
		RGBtoYUV = null;
	}
}
