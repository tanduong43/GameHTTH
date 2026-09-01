using System;
using System.Threading;

public class SaveImageRMS
{
	public static mVector vecSaveImage = new mVector("SaveImageRMS.vecSaveImage");

	public static mVector vecSaveImageAndroid = new mVector("SaveImageRMS.vecSaveImageAndroid");

	private static bool isRunning = false;

	public void run()
	{
		SaveImage();
	}

	public static void start()
	{
		if (!isRunning)
		{
			isRunning = true;
			Thread thread = new Thread(runLoop);
			thread.IsBackground = true;
			thread.Start();
		}
	}

	public static void stop()
	{
		isRunning = false;
	}

	public static void update()
	{
	}

	private static void runLoop()
	{
		while (isRunning)
		{
			try
			{
				if (vecSaveImage.size() > 0 || vecSaveImageAndroid.size() > 0)
				{
					SaveImage();
				}
				Thread.Sleep(30);
			}
			catch (Exception)
			{
				Thread.Sleep(100);
			}
		}
	}

	public static void SaveImage()
	{
		while (vecSaveImage.size() > 0 || vecSaveImageAndroid.size() > 0)
		{
			try
			{
				if (vecSaveImage.size() > 0)
				{
					idSaveImage idSaveImage2 = (idSaveImage)vecSaveImage.elementAt(0);
					if (idSaveImage2 != null)
					{
						ObjectData.setToRms(idSaveImage2.mbytImage, idSaveImage2.id);
					}
					vecSaveImage.removeElementAt(0);
				}
				if (vecSaveImageAndroid.size() <= 0)
				{
					continue;
				}
				try
				{
					UpdateImageScreen.curNum = UpdateImageScreen.maxNum - vecSaveImageAndroid.size();
					idSaveImage idSaveImage3 = (idSaveImage)vecSaveImageAndroid.elementAt(0);
					if (idSaveImage3 != null)
					{
						ObjectData.saveImageToRmsAndroid(idSaveImage3.mbytImage, idSaveImage3.name);
					}
				}
				catch (Exception)
				{
				}
				vecSaveImageAndroid.removeElementAt(0);
			}
			catch (Exception)
			{
			}
		}
	}
}
