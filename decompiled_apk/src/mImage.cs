using System;
using UnityEngine;

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
				if (url.IndexOf("iconserver") > -1)
				{
					Debug.Log((object)("debug 1111:" + url));
				}
				if (array != null)
				{
					mImage2 = createImage(array, 0, array.Length);
					if (url.IndexOf("iconserver") > -1)
					{
						Debug.Log((object)"debug 222");
					}
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
			if (mImage2.image == null && mGraphics.zoomLevel != 2)
			{
				try
				{
					mImage2.image = Image.createImage("/x2" + url);
				}
				catch (Exception)
				{
				}
			}
			if (mImage2.image == null && mGraphics.zoomLevel != 1)
			{
				try
				{
					mImage2.image = Image.createImage("/x1" + url);
				}
				catch (Exception)
				{
				}
			}
			if (mImage2.image == null)
			{
				try
				{
					mImage2.image = Image.createImage(url);
				}
				catch (Exception)
				{
				}
			}
			if (mImage2.image == null && url.Contains("lgv.png"))
			{
				try
				{
					mImage2.image = Image.createImage("/x" + mGraphics.zoomLevel + "/new/lgv_e.png");
					if (mImage2.image == null)
					{
						mImage2.image = Image.createImage("/x2/new/lgv_e.png");
					}
					if (mImage2.image == null)
					{
						mImage2.image = Image.createImage("/x1/new/lgv_e.png");
					}
				}
				catch (Exception)
				{
				}
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
			try
			{
				mImage2.image = Image.createImage("/x" + mGraphics.zoomLevel + url);
			}
			catch (Exception)
			{
			}
		}
		if (mImage2.image == null)
		{
			try
			{
				mImage2.image = Image.createImage("/x2" + url);
			}
			catch (Exception)
			{
			}
		}
		if (mImage2.image == null)
		{
			try
			{
				mImage2.image = Image.createImage("/x1" + url);
			}
			catch (Exception)
			{
			}
		}
		if (mImage2.image == null)
		{
			return null;
		}
		return mImage2;
	}

	public static mImage createImage(int w, int h)
	{
		return new mImage
		{
			image = Image.createImage(w * mGraphics.zoomLevel, h * mGraphics.zoomLevel)
		};
	}

	public static mImage createImage(sbyte[] data, int w, int h)
	{
		return new mImage
		{
			image = Image.createImage(data, 0, data.Length)
		};
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
		return new mImage
		{
			image = Image.createRGBImage(rgb, width, height, processAlpha)
		};
	}

	public static mImage scaleImage(mImage img, int w, int h)
	{
		return img;
	}
}
