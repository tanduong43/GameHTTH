using System;

public class UpdateImageScreen : MainScreen
{
	public static int maxNum;

	public static int curNum;

	private int wpaint = -1;

	private int maxwPaint;

	private int x;

	private int y;

	private int hpaint;

	private long timeBegin;

	public static sbyte statusUpdate;

	public const sbyte CONNECT = 0;

	public const sbyte FAIL = 1;

	public const sbyte LOADING = 2;

	public const sbyte LOADING_OK = 3;

	public static string strPaint = string.Empty;

	private mImage imglogo;

	private mImage imgsea;

	private mImage imgsky;

	private mImage imgloading1;

	private mImage imgloading2;

	private mImage imgloading3;

	private int wSky;

	private int wSea;

	private int hSky;

	private int hSea;

	private bool isCheckIOSZoom;

	public UpdateImageScreen()
	{
		maxwPaint = 122;
		hpaint = 14;
		x = MotherCanvas.hw;
		y = MotherCanvas.h / 5 * 4 - 7;
		if (GameCanvas.isIos())
		{
			isCheckIOSZoom = true;
		}
		else
		{
			beginLoadImage();
		}
		timeBegin = mSystem.currentTimeMillis();
		statusUpdate = 0;
		setmNamePaint(T.pleaseWaiting);
		loadImage();
	}

	public void loadImage()
	{
		if (GameCanvas.language == 1)
		{
			imglogo = mImage.createImage("/new/lgv_e.png");
		}
		else
		{
			imglogo = mImage.createImage("/new/lgv.png");
		}
		if (imglogo == null)
		{
			imglogo = mImage.createImage("/new/lgv_e.png");
		}
		imgloading1 = mImage.createImage("/new/koload.png");
		imgloading2 = mImage.createImage("/new/load.png");
		imgloading3 = mImage.createImage("/new/thuyen.png");
		imgsea = mImage.createImageAll("/up0.png");
		imgsky = mImage.createImageAll("/up1.png");
		wSky = ((imgsky != null && imgsky.image != null) ? mImage.getImageWidth(imgsky.image) : 480);
		hSky = ((imgsky != null && imgsky.image != null) ? mImage.getImageHeight(imgsky.image) : 240);
		wSea = ((imgsea != null && imgsea.image != null) ? mImage.getImageWidth(imgsea.image) : 480);
		hSea = ((imgsea != null && imgsea.image != null) ? mImage.getImageHeight(imgsea.image) : 240);
		if (wSky <= 0)
		{
			wSky = 480;
		}
		if (hSky <= 0)
		{
			hSky = 240;
		}
		if (wSea <= 0)
		{
			wSea = 480;
		}
		if (hSea <= 0)
		{
			hSea = 240;
		}
	}

	public override void paint(mGraphics g)
	{
		g.setColor(6014975);
		g.fillRect(0, 0, MotherCanvas.w, MotherCanvas.h / 2);
		g.setColor(16765819);
		g.fillRect(0, MotherCanvas.h / 2, MotherCanvas.w, MotherCanvas.h / 2);
		if (imgsky != null && wSky > 0)
		{
			for (int i = 0; i <= MotherCanvas.w / wSky; i++)
			{
				g.drawImage(imgsky, i * wSky, MotherCanvas.hh - hSky / 2, 0);
			}
		}
		if (imgsea != null && wSea > 0)
		{
			for (int j = 0; j <= MotherCanvas.w / wSea; j++)
			{
				g.drawImage(imgsea, j * wSea, MotherCanvas.hh + hSky / 2, 0);
			}
		}
		if (imglogo != null)
		{
			g.drawImage(imglogo, MotherCanvas.hw, MotherCanvas.h / 5, 3);
		}
		if (isCheckIOSZoom)
		{
			g.setColor(0);
			g.drawString(T.ZoomIos1, MotherCanvas.hw, y - 26, 2);
			g.drawString(T.ZoomIos2, MotherCanvas.hw, y - 10, 2);
			int num = 60;
			int num2 = 22;
			int num3 = 12;
			int num4 = num * 3 + num3 * 2;
			int num5 = MotherCanvas.hw - num4 / 2;
			int num6 = y + 10;
			g.setColor(3809296);
			g.fillRect(num5, num6, num, num2);
			g.setColor(15965202);
			g.fillRect(num5 + 1, num6 + 1, num - 2, num2 - 2);
			g.setColor(1710618);
			g.drawString(T.ratThap, num5 + num / 2, num6 + 4, 2);
			g.setColor(3809296);
			g.fillRect(num5 + num + num3, num6, num, num2);
			g.setColor(15965202);
			g.fillRect(num5 + num + num3 + 1, num6 + 1, num - 2, num2 - 2);
			g.setColor(1710618);
			g.drawString(T.Thap, num5 + num + num3 + num / 2, num6 + 4, 2);
			g.setColor(3809296);
			g.fillRect(num5 + (num + num3) * 2, num6, num, num2);
			g.setColor(15965202);
			g.fillRect(num5 + (num + num3) * 2 + 1, num6 + 1, num - 2, num2 - 2);
			g.setColor(1710618);
			g.drawString(T.Cao, num5 + (num + num3) * 2 + num / 2, num6 + 4, 2);
			return;
		}
		g.drawString(strPaint, MotherCanvas.hw, y - 20 + 7, 2);
		if (statusUpdate == 2 || statusUpdate == 3)
		{
			if (imgloading1 != null)
			{
				g.drawImage(imgloading1, x - 61, y - 8, 0);
			}
			if (wpaint >= 0 && imgloading2 != null)
			{
				g.drawRegion(imgloading2, 0, 0, wpaint, 16, 0, x - 61, y - 8, 0);
			}
			int num7 = wpaint;
			if (num7 < 10)
			{
				num7 = 10;
			}
			if (num7 > maxwPaint - 12)
			{
				num7 = maxwPaint - 12;
			}
			g.drawString(curNum + " / " + maxNum, MotherCanvas.hw, y + 4, 2);
			if (imgloading3 != null)
			{
				g.drawImage(imgloading3, x - 60 + num7, y, 3);
			}
		}
	}

	public override void update()
	{
		SaveImageRMS.update();
		if (isCheckIOSZoom)
		{
			return;
		}
		if (maxNum > 0)
		{
			wpaint = maxwPaint * curNum / maxNum;
			if (wpaint > maxwPaint)
			{
				wpaint = maxwPaint;
			}
		}
		if (statusUpdate == 0 && (GameCanvas.timeNow - timeBegin) / 1000 > 15)
		{
			if (GameCanvas.indexdownload == 0)
			{
				Session_ME.gI().close();
				GameCanvas.indexdownload++;
				GameCanvas.connectDownload();
				GlobalService.gI().Request_Image_Android();
				timeBegin = GameCanvas.timeNow;
			}
			else
			{
				setmNamePaint(T.disconnectUpdateImage);
				statusUpdate = 1;
			}
		}
		if (statusUpdate == 3 && SaveImageRMS.vecSaveImageAndroid.size() == 0)
		{
			GameCanvas.instance.beginGame();
			saveVer();
		}
	}

	public override void updatePointer()
	{
		if (isCheckIOSZoom)
		{
			int num = 60;
			int h = 22;
			int num2 = 12;
			int num3 = num * 3 + num2 * 2;
			int num4 = MotherCanvas.hw - num3 / 2;
			int num5 = y + 10;
			if (GameCanvas.isPoint(num4, num5, num, h))
			{
				GameMidlet.ZOOM_IOS = 1;
				CRes.saveRMS("SUB_ZOOMIOS", new sbyte[1] { (sbyte)GameMidlet.ZOOM_IOS });
				beginLoadImage();
			}
			else if (GameCanvas.isPoint(num4 + num + num2, num5, num, h))
			{
				GameMidlet.ZOOM_IOS = 2;
				CRes.saveRMS("SUB_ZOOMIOS", new sbyte[1] { (sbyte)GameMidlet.ZOOM_IOS });
				beginLoadImage();
			}
			else if (GameCanvas.isPoint(num4 + (num + num2) * 2, num5, num, h))
			{
				GameMidlet.ZOOM_IOS = 2;
				CRes.saveRMS("SUB_ZOOMIOS", new sbyte[1] { (sbyte)GameMidlet.ZOOM_IOS });
				beginLoadImage();
			}
		}
		else
		{
			if (statusUpdate == 1 && GameCanvas.isPointerDown)
			{
				Session_ME.gI().close();
				GameCanvas.indexdownload++;
				GameCanvas.connectDownload();
				GlobalService.gI().Request_Image_Android();
				timeBegin = GameCanvas.timeNow;
				statusUpdate = 0;
				setmNamePaint(T.pleaseWaiting);
			}
			base.updatePointer();
		}
	}

	public static void setValueUpdate(int cur, int max)
	{
		curNum = cur;
		if (max >= 0)
		{
			maxNum = max;
		}
		statusUpdate = 2;
	}

	public static void setmNamePaint(string str)
	{
		strPaint = str;
	}

	public void saveVer()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeUTF("1.2.7");
			CRes.saveRMS("Main_Load_Image_Android_OK", byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void beginLoadImage()
	{
		isCheckIOSZoom = false;
		Session_ME.gI().close();
		GameCanvas.connectDownload();
		GlobalService.gI().Request_Image_Android();
	}
}
