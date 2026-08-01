using System;

public class MainEffectAuto
{
	public static MyHashTable hashTemEffAuto = new MyHashTable();

	public MyHashTable hashImage = new MyHashTable();

	public MainFrameEff[] mFrame;

	public short[] mRunFrame;

	public mImage img;

	public MainEffectAuto()
	{
	}

	public MainEffectAuto(DataInputStream data, sbyte[] dataImage)
	{
		setEffAuto(data);
		img = mImage.createImage(dataImage, 0, dataImage.Length);
	}

	public void setEffAuto(DataInputStream msg)
	{
		try
		{
			sbyte b = msg.readByte();
			for (int i = 0; i < b; i++)
			{
				MainPartImage mainPartImage = new MainPartImage(msg.readByte(), msg.readByte(), msg.readByte(), msg.readByte(), msg.readByte());
				hashImage.put(string.Empty + mainPartImage.ID, mainPartImage);
			}
			short num = msg.readShort();
			mFrame = new MainFrameEff[num];
			for (int j = 0; j < num; j++)
			{
				sbyte b2 = msg.readByte();
				mFrame[j] = new MainFrameEff();
				mFrame[j].mpart = new Part[b2];
				for (int k = 0; k < b2; k++)
				{
					mFrame[j].mpart[k] = new Part();
					mFrame[j].mpart[k].x = msg.readShort();
					mFrame[j].mpart[k].y = msg.readShort();
					mFrame[j].mpart[k].idPartImage = msg.readByte();
				}
			}
			short num2 = msg.readShort();
			mRunFrame = new short[num2];
			for (int l = 0; l < num2; l++)
			{
				mRunFrame[l] = msg.readShort();
			}
			msg.readByte();
			msg.readByte();
		}
		catch (Exception)
		{
		}
	}
}
