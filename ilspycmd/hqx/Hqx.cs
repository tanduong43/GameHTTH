namespace hqx;

public abstract class Hqx
{
	private static int Ymask = 16711680;

	private static int Umask = 65280;

	private static int Vmask = 255;

	protected static bool diff(int c1, int c2, int trY, int trU, int trV, int trA)
	{
		int yuv = RgbYuv.getYuv(c1);
		int yuv2 = RgbYuv.getYuv(c2);
		return Math.abs((yuv & Ymask) - (yuv2 & Ymask)) > trY || Math.abs((yuv & Umask) - (yuv2 & Umask)) > trU || Math.abs((yuv & Vmask) - (yuv2 & Vmask)) > trV || Math.abs((c1 >> 24) - (c2 >> 24)) > trA;
	}

	public static int[] HqxZoom(int zoom, int[] source, int Xres, int Yres)
	{
		int[] array = null;
		switch (zoom)
		{
		case 2:
			array = new int[Xres * Yres * zoom * zoom];
			Hqx_2x.hq2x_32_rb(source, array, Xres, Yres);
			break;
		case 3:
			array = new int[Xres * Yres * zoom * zoom];
			Hqx_3x.hq3x_32_rb(source, array, Xres, Yres);
			break;
		case 4:
			array = new int[Xres * Yres * zoom * zoom];
			Hqx_4x.hq4x_32_rb(source, array, Xres, Yres);
			break;
		default:
			array = source;
			break;
		}
		return array;
	}
}
