using System;
using System.Threading;
using UnityEngine;
using hqx;

public class Image
{
	private const int INTERVAL = 5;

	private const int MAXTIME = 500;

	public Texture2D texture = new Texture2D(1, 1);

	public static Image imgTemp;

	public static string filenametemp;

	public static byte[] datatemp;

	public static Image imgSrcTemp;

	public static int xtemp;

	public static int ytemp;

	public static int wtemp;

	public static int htemp;

	public static int transformtemp;

	public int w;

	public int h;

	public static int status;

	public Color colorBlend = Color.black;

	public static int iA;

	public static Image createEmptyImage()
	{
		return __createEmptyImage();
	}

	public static Image createImage(string path)
	{
		path = Main.res + path;
		path = cutPng(path);
		Image result = null;
		try
		{
			result = createImageUni(path);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public static string cutPng(string str)
	{
		string result = str;
		if (str.Contains(".png"))
		{
			result = str.Replace(".png", string.Empty);
		}
		return result;
	}

	public static Image createImageUni(string filename)
	{
		return __createImage(filename);
	}

	public static Image createImageX(string filename)
	{
		return __createImageX(filename);
	}

	public static Image createImage(byte[] imageData)
	{
		return __createImage(imageData);
	}

	public static Image createImage(Image src, int x, int y, int w, int h, int transform)
	{
		return __createImage(src, x, y, w, h, transform);
	}

	public static Image createImage(int w, int h)
	{
		return __createImage(w, h);
	}

	public static Image createImage(Image img)
	{
		Image image = createImage(img.w, img.h);
		image.texture = img.texture;
		image.texture.Apply();
		return image;
	}

	public static Image createImage(sbyte[] imageData, int offset, int lenght)
	{
		if (offset + lenght > imageData.Length)
		{
			return null;
		}
		byte[] array = new byte[lenght];
		for (int i = 0; i < lenght; i++)
		{
			array[i] = convertSbyteToByte(imageData[i + offset]);
		}
		return createImage(array);
	}

	public static byte convertSbyteToByte(sbyte var)
	{
		if (var > 0)
		{
			return (byte)var;
		}
		return (byte)(var + 256);
	}

	public static byte[] convertArrSbyteToArrByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if (var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)(var[i] + 256);
			}
		}
		return array;
	}

	public static Image createRGBImage(int[] rbg, int w, int h, bool bl)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Image image = createImage(w, h);
		Color[] array = (Color[])(object)new Color[rbg.Length];
		for (int i = 0; i < array.Length; i++)
		{
			ref Color reference = ref array[i];
			reference = setColorFromRBG(rbg[i]);
		}
		image.texture.SetPixels(0, 0, w, h, array);
		image.texture.Apply();
		return image;
	}

	public static Color setColorFromRBG(int rgb)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		int num = rgb & 0xFF;
		int num2 = (rgb >> 8) & 0xFF;
		int num3 = (rgb >> 16) & 0xFF;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		return new Color((float)num3 / 256f, num5, num4);
	}

	public static void update()
	{
		if (status == 2)
		{
			status = 1;
			imgTemp = __createEmptyImage();
			status = 0;
		}
		else if (status == 3)
		{
			status = 1;
			imgTemp = __createImage(filenametemp);
			status = 0;
		}
		else if (status == 4)
		{
			status = 1;
			imgTemp = __createImage(datatemp);
			status = 0;
		}
		else if (status == 5)
		{
			status = 1;
			imgTemp = __createImage(imgSrcTemp, xtemp, ytemp, wtemp, htemp, transformtemp);
			status = 0;
		}
		else if (status == 6)
		{
			status = 1;
			imgTemp = __createImage(wtemp, htemp);
			status = 0;
		}
		else if (status == 7)
		{
			status = 1;
			imgTemp = __createImage(filenametemp);
			status = 0;
		}
	}

	private static Image _createEmptyImage()
	{
		if (status != 0)
		{
			Cout.LogError("CANNOT CREATE EMPTY IMAGE WHEN CREATING OTHER IMAGE");
			return null;
		}
		imgTemp = null;
		status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE EMPTY IMAGE");
			status = 0;
		}
		return imgTemp;
	}

	private static Image _createImage(string filename)
	{
		if (status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
			return null;
		}
		imgTemp = null;
		filenametemp = filename;
		status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE " + filename);
			status = 0;
		}
		return imgTemp;
	}

	private static Image _createImageX(string filename)
	{
		if (status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
			return null;
		}
		imgTemp = null;
		filenametemp = filename;
		status = 7;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE " + filename);
			status = 0;
		}
		return imgTemp;
	}

	private static Image _createImage(byte[] imageData)
	{
		if (status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromArray) WHEN CREATING OTHER IMAGE");
			return null;
		}
		imgTemp = null;
		datatemp = imageData;
		status = 4;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(FromArray)");
			status = 0;
		}
		return imgTemp;
	}

	private static Image _createImage(Image src, int x, int y, int w, int h, int transform)
	{
		if (status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromSrcPart) WHEN CREATING OTHER IMAGE");
			return null;
		}
		imgTemp = null;
		imgSrcTemp = src;
		xtemp = x;
		ytemp = y;
		wtemp = w;
		htemp = h;
		transformtemp = transform;
		status = 5;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(FromSrcPart)");
			status = 0;
		}
		return imgTemp;
	}

	private static Image _createImage(int w, int h)
	{
		if (status != 0)
		{
			Cout.LogError("CANNOT CREATE IMAGE(w,h) WHEN CREATING OTHER IMAGE");
			return null;
		}
		imgTemp = null;
		wtemp = w;
		htemp = h;
		status = 6;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Cout.LogError("TOO LONG FOR CREATE IMAGE(w,h)");
			status = 0;
		}
		return imgTemp;
	}

	public static byte[] loadData(string filename)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		new Image();
		TextAsset val = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		if ((Object)(object)val == (Object)null || val.bytes == null || val.bytes.Length == 0)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		sbyte[] array = ArrayCast.cast(val.bytes);
		Debug.LogError((object)("CHIEU DAI MANG BYTE IMAGE CREAT = " + array.Length));
		return val.bytes;
	}

	private static Image __createImage(string filename)
	{
		Image image = new Image();
		Object obj = Resources.Load(filename);
		Texture2D val = (Texture2D)(object)((obj is Texture2D) ? obj : null);
		if ((Object)(object)val == (Object)null)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		image.texture = val;
		image.w = ((Texture)image.texture).width;
		image.h = ((Texture)image.texture).height;
		setTextureQuality(image);
		return image;
	}

	private static Image __createImageX(string filename)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		Image image = new Image();
		Object obj = Resources.Load(filename);
		Texture2D val = (Texture2D)(object)((obj is Texture2D) ? obj : null);
		if ((Object)(object)val == (Object)null)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		try
		{
			image.texture = val;
			image.w = ((Texture)image.texture).width;
			image.h = ((Texture)image.texture).height;
			int[] array = new int[image.w * image.h];
			Color[] pixels = image.texture.GetPixels(0, 0, image.w, image.h);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = getIntByColor(pixels[i]);
			}
			int[] array2 = Hqx.HqxZoom(mGraphics.zoomLevel, array, image.w, image.h);
			int num = image.w * mGraphics.zoomLevel;
			int num2 = image.h * mGraphics.zoomLevel;
			image.texture = new Texture2D(num, num2);
			Color[] array3 = (Color[])(object)new Color[num * num2];
			for (int j = 0; j < array3.Length; j++)
			{
				ref Color reference = ref array3[j];
				reference = getColor(array2[j]);
			}
			image.texture.SetPixels(array3);
			image.texture.Apply(false, false);
			image.w = num;
			image.h = num2;
			setTextureQuality(image);
		}
		catch (Exception e)
		{
			Out.printError(e);
		}
		return image;
	}

	private static Image __createImage(byte[] imageData)
	{
		if (imageData == null || imageData.Length == 0)
		{
			Cout.LogError("Create Image from byte array fail");
			return null;
		}
		Image image = new Image();
		try
		{
			ImageConversion.LoadImage(image.texture, imageData);
			image.w = ((Texture)image.texture).width;
			image.h = ((Texture)image.texture).height;
			setTextureQuality(image);
		}
		catch (Exception ex)
		{
			Cout.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + ex.Message);
		}
		return image;
	}

	public static Color getColor(int rgb)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		int num = rgb & 0xFF;
		int num2 = (rgb >> 8) & 0xFF;
		int num3 = (rgb >> 16) & 0xFF;
		int num4 = (rgb >> 24) & 0xFF;
		float num5 = (float)num / 256f;
		float num6 = (float)num2 / 256f;
		float num7 = (float)num3 / 256f;
		float num8 = (float)num4 / 256f;
		return new Color(num7, num6, num5, num8);
	}

	public static int getIntByColor(Color cl)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		float num = cl.a * 255f;
		float num2 = cl.r * 255f;
		float num3 = cl.b * 255f;
		float num4 = cl.g * 255f;
		return (((int)num & 0xFF) << 24) | (((int)num2 & 0xFF) << 16) | (((int)num4 & 0xFF) << 8) | ((int)num3 & 0xFF);
	}

	private static Image __createImage(Image src, int x, int y, int w, int h, int transform)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Image image = new Image();
		image.texture = new Texture2D(w, h);
		y = ((Texture)src.texture).height - y - h;
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				int num = i;
				if (transform == 2)
				{
					num = w - i;
				}
				int num2 = j;
				image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + num2));
			}
		}
		image.texture.Apply();
		image.w = ((Texture)image.texture).width;
		image.h = ((Texture)image.texture).height;
		setTextureQuality(image);
		return image;
	}

	private static Image __createEmptyImage()
	{
		return new Image();
	}

	public static Image __createImage(int w, int h)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		Image obj = new Image
		{
			texture = new Texture2D(w, h, (TextureFormat)4, false)
		};
		setTextureQuality(obj);
		obj.w = w;
		obj.h = h;
		obj.texture.Apply();
		return obj;
	}

	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	public int getWidth()
	{
		return w;
	}

	public int getHeight()
	{
		return h;
	}

	private static void setTextureQuality(Image img)
	{
		setTextureQuality(img.texture);
	}

	public static void setTextureQuality(Texture2D texture)
	{
		((Texture)texture).anisoLevel = 0;
		((Texture)texture).filterMode = (FilterMode)0;
		((Texture)texture).mipMapBias = 0f;
		((Texture)texture).wrapMode = (TextureWrapMode)1;
	}

	public Color[] getColor()
	{
		return texture.GetPixels();
	}

	public int getRealImageWidth()
	{
		return w;
	}

	public int getRealImageHeight()
	{
		return h;
	}

	public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		Color[] pixels = texture.GetPixels(x, this.h - 1 - y, w, h);
		for (int i = 0; i < pixels.Length; i++)
		{
			data[i] = mGraphics.getIntByColor(pixels[i]);
		}
	}
}
