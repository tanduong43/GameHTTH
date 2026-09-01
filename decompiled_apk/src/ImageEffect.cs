using System;
using System.Collections;

public class ImageEffect
{
	public static MyHashTable hashImageEff = new MyHashTable();

	private long timeRemove;

	private int IdImage;

	private mImage img;

	public ImageEffect(int Id)
	{
		IdImage = Id;
		try
		{
			img = mImage.createImage("/eff/g" + Id + ".png");
		}
		catch (Exception)
		{
		}
		timeRemove = GameCanvas.timeNow;
	}

	public static mImage setImage(int Id)
	{
		ImageEffect imageEffect = (ImageEffect)hashImageEff.get(string.Empty + Id);
		if (imageEffect == null)
		{
			imageEffect = new ImageEffect(Id);
			hashImageEff.put(string.Empty + Id, imageEffect);
		}
		else
		{
			imageEffect.timeRemove = GameCanvas.timeNow;
		}
		return imageEffect.img;
	}

	public static void SetRemove(short time)
	{
		mVector mVector2 = new mVector();
		IDictionaryEnumerator enumerator = hashImageEff.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ImageEffect imageEffect = (ImageEffect)enumerator.Value;
			if ((GameCanvas.timeNow - imageEffect.timeRemove) / 1000 > time)
			{
				string o = (string)enumerator.Key;
				mVector2.addElement(o);
			}
		}
		for (int i = 0; i < mVector2.size(); i++)
		{
			hashImageEff.Remove((string)mVector2.elementAt(i));
		}
	}

	public static void SetRemoveAll()
	{
		hashImageEff.clear();
	}
}
