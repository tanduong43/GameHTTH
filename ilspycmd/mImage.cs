using System;

public class mImage
{
	public Image image;

	public int w;

	public int h;

	public static string getLink(string str)
	{
		return str;
	}

	public static string getUrlImage(string str)
	{
		return str.Replace("/", "_");
	}

	public void setWH()
	{
		if (image != null)
		{
			w = getImageWidth(image);
			h = getImageHeight(image);
		}
	}

	public static mImage createImage(string url)
	{
		mSystem.outz("vào load image=" + url);
		mImage mImage2 = new mImage();
		try
		{
			if (GameMidlet.DEVICE != 0)
			{
				sbyte[] array = CRes.loadRMS("Main_Image" + getUrlImage(url));
				if (array != null)
				{
					mImage2 = createImage(array, 0, array.Length);
					return mImage2;
				}
			}
			try
			{
				mImage2.image = Image.createImage("/x" + mGraphics.zoomLevel + url);
			}
			catch (Exception)
			{
			}
			if (mImage2.image == null)
			{
				return null;
			}
		}
		catch (Exception)
		{
		}
		return mImage2;
	}

	public static mImage createImageAll(string url)
	{
		mImage mImage2 = new mImage();
		try
		{
			mImage2.image = Image.createImage(url);
		}
		catch (Exception)
		{
		}
		if (mImage2.image == null)
		{
			return null;
		}
		return mImage2;
	}

	public static mImage createImage(int w, int h)
	{
		mImage mImage2 = new mImage();
		mImage2.image = Image.createImage(w * mGraphics.zoomLevel, h * mGraphics.zoomLevel);
		return mImage2;
	}

	public static mImage createImage(sbyte[] data, int w, int h)
	{
		mImage mImage2 = new mImage();
		mImage2.image = Image.createImage(data, 0, data.Length);
		return mImage2;
	}

	public TemGraphics getGraphics()
	{
		return new TemGraphics();
	}

	public static int getImageWidth(Image image)
	{
		return image.getWidth() / mGraphics.zoomLevel;
	}

	public static int getImageHeight(Image image)
	{
		return image.getHeight() / mGraphics.zoomLevel;
	}

	public void getRGB(int[] rgbData, int offset, int scanlength, int x, int y, int width, int height)
	{
	}

	public static mImage createRGBImage(int[] rgb, int width, int height, bool processAlpha)
	{
		mImage mImage2 = new mImage();
		mImage2.image = Image.createRGBImage(rgb, width, height, processAlpha);
		return mImage2;
	}

	public static mImage scaleImage(mImage img, int w, int h)
	{
		return img;
	}
}
