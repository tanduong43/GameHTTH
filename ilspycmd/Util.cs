using System;

public class Util
{
	public static DataInputStream loadFile(string path)
	{
		try
		{
			return DataInputStream.getResourceAsStream(Main.res + path);
		}
		catch (Exception e)
		{
			Out.printError(e);
		}
		return null;
	}

	public static int abs(int a)
	{
		return (a < 0) ? (-a) : a;
	}

	public static sbyte[] readByteArray(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			sbyte[] data = new sbyte[num];
			msg.reader().read(ref data);
			return data;
		}
		catch (Exception e)
		{
			Out.printError(e);
		}
		return null;
	}

	public static sbyte[] readByteArray(DataInputStream dos)
	{
		try
		{
			int num = dos.readInt();
			sbyte[] data = new sbyte[num];
			dos.read(ref data);
			return data;
		}
		catch (Exception e)
		{
			Out.printError(e);
		}
		return null;
	}

	public static string[] split(string original, string separator)
	{
		mVector mVector2 = new mVector();
		for (int num = original.IndexOf(separator); num >= 0; num = original.IndexOf(separator))
		{
			mVector2.addElement(original.Substring(0, num));
			original = original.Substring(num + separator.Length);
		}
		mVector2.addElement(original);
		string[] array = new string[mVector2.size()];
		if (mVector2.size() > 0)
		{
			for (int i = 0; i < mVector2.size(); i++)
			{
				array[i] = (string)mVector2.elementAt(i);
			}
		}
		return array;
	}
}
