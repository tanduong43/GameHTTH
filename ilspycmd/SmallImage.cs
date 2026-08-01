using System;

public class SmallImage
{
	public static int[][] smallImg;

	public static SmallImage instance;

	public static mImage[] imgbig;

	public static mImage imgEmpty;

	public static sbyte[] newSmallVersion;

	public short id;

	public short x;

	public short y;

	public short w;

	public short h;

	public SmallImage(int id, int x, int y, int w, int h)
	{
		this.id = (short)id;
		this.x = (short)x;
		this.y = (short)y;
		this.w = (short)w;
		this.h = (short)h;
	}

	public SmallImage()
	{
	}

	public static void loadBigRMS()
	{
	}

	public static void freeBig()
	{
		mSystem.gc();
	}

	public static void loadBigImage()
	{
		mSystem.gc();
	}

	public static void init()
	{
		instance = null;
		instance = new SmallImage();
	}

	public void readData(sbyte[] data)
	{
	}

	public static void readImage(sbyte[] data)
	{
		try
		{
			DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(data));
			int num = dataInputStream.readShort();
			if (num > 600)
			{
				num = 600;
			}
			smallImg = new int[num][];
			for (int i = 0; i < num; i++)
			{
				smallImg[i] = new int[5];
				smallImg[i][0] = dataInputStream.readUnsignedByte();
				smallImg[i][1] = dataInputStream.readShort();
				smallImg[i][2] = dataInputStream.readShort();
				smallImg[i][3] = dataInputStream.readShort();
				smallImg[i][4] = dataInputStream.readShort();
			}
		}
		catch (Exception)
		{
		}
	}

	public static void clearHastable()
	{
	}

	public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor, int index)
	{
		int num = 0;
		MainImage imageAll = ObjectData.getImageAll((short)id, ObjectData.HashImageCharPart, 10000);
		if (imageAll.img != null)
		{
			g.drawRegion(imageAll.img, 0, 0, imageAll.w, imageAll.h, transform, x + num, y, anchor);
		}
	}

	public static void update()
	{
	}
}
