using System;

public class SaveImageRMS
{
	public static mVector vecSaveImage = new mVector("SaveImageRMS.vecSaveImage");

	public static mVector vecSaveImageAndroid = new mVector("SaveImageRMS.vecSaveImageAndroid");

	public void run()
	{
		SaveImage();
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
					ObjectData.setToRms(idSaveImage2.mbytImage, idSaveImage2.id);
					vecSaveImage.removeElementAt(0);
				}
				if (vecSaveImageAndroid.size() > 0)
				{
					try
					{
						UpdateImageScreen.curNum = UpdateImageScreen.maxNum - vecSaveImageAndroid.size();
						idSaveImage idSaveImage3 = (idSaveImage)vecSaveImageAndroid.elementAt(0);
						ObjectData.saveImageToRmsAndroid(idSaveImage3.mbytImage, idSaveImage3.name);
					}
					catch (Exception)
					{
					}
					vecSaveImageAndroid.removeElementAt(0);
				}
			}
			catch (Exception)
			{
			}
		}
	}

	public void start()
	{
		SaveImage();
	}
}
