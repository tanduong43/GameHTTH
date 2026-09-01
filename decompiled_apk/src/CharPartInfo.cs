using System;
using System.Collections;

public class CharPartInfo
{
	public static mPart[] parts;

	public static MyHashTable hashMyPart = new MyHashTable();

	public static void readPart(sbyte[] data)
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(new ByteArrayInputStream(data));
			int num = dataInputStream.readShort();
			parts = new mPart[num];
			for (int i = 0; i < num; i++)
			{
				int type = dataInputStream.readByte();
				parts[i] = new mPart(type);
				for (int j = 0; j < parts[i].pi.Length; j++)
				{
					parts[i].pi[j] = new PartImage();
					parts[i].pi[j].id = dataInputStream.readShort();
					parts[i].pi[j].dx = dataInputStream.readByte();
					parts[i].pi[j].dy = dataInputStream.readByte();
				}
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception)
			{
			}
		}
	}

	public static void LoadDataCharPart(DataInputStream dis, int isSave)
	{
		if (dis == null)
		{
			GlobalService.gI().get_DATA(23);
			return;
		}
		try
		{
			short verCharPar = 0;
			if (isSave >= 0)
			{
				verCharPar = dis.readShort();
			}
			int sizeImage = dis.readInt();
			loadImage(dis, sizeImage);
			dis.close();
			if (isSave == 1)
			{
				GlobalService.VerCharPar = verCharPar;
				SaveRms.saveVer(GlobalService.VerCharPar, "VerdataCharPart");
			}
		}
		catch (Exception)
		{
		}
	}

	public static void loadImage(DataInputStream dis, int sizeImage)
	{
		try
		{
			sbyte[] data = null;
			if (sizeImage > 0)
			{
				data = new sbyte[sizeImage];
				dis.read(ref data);
			}
			SmallImage.readImage(data);
		}
		catch (Exception)
		{
		}
	}

	public static void loadPart(DataInputStream dis, int sizePart)
	{
		try
		{
			sbyte[] data = null;
			if (sizePart > 0)
			{
				data = new sbyte[sizePart];
				dis.read(ref data);
			}
			readPart(data);
		}
		catch (Exception)
		{
		}
	}

	public static mPart getPart(int index)
	{
		mPart mPart2 = (mPart)hashMyPart.get(string.Empty + index);
		if (mPart2 == null)
		{
			mPart2 = new mPart();
			hashMyPart.put(string.Empty + index, mPart2);
			GlobalService.gI().getDataPart((short)index);
		}
		mPart2.count = GameCanvas.timeNow / 1000;
		if (mPart2.pi == null)
		{
			mPart2.timeNull++;
			if (mPart2.timeNull >= 200)
			{
				GlobalService.gI().getDataPart((short)index);
				mPart2.timeNull = 0;
			}
		}
		return mPart2;
	}

	public static void checkDelHashCharPart(MyHashTable hash, int time)
	{
		mVector mVector2 = new mVector();
		IDictionaryEnumerator enumerator = hash.GetEnumerator();
		while (enumerator.MoveNext())
		{
			mPart mPart2 = (mPart)enumerator.Value;
			if (mPart2.count != -1 && GameCanvas.timeNow / 1000 - mPart2.count > time)
			{
				string o = (string)enumerator.Key;
				mVector2.addElement(o);
			}
		}
		for (int i = 0; i < mVector2.size(); i++)
		{
			hash.Remove((string)mVector2.elementAt(i));
		}
	}
}
