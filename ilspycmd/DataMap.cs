using System;

public class DataMap
{
	public static MyHashTable hashDataMap = new MyHashTable();

	public sbyte[] dataItemMap;

	public sbyte[] dataMap;

	public mVector vecPointMap = new mVector("DataMap.vecPointMap");

	public short hBack;

	public sbyte IdBack;

	public string nameMap;

	public void setDataMap(sbyte[] mdata)
	{
		dataMap = mdata;
	}

	public void setDataItemMap(sbyte[] mdata)
	{
		dataItemMap = mdata;
	}

	public void setAddVecPointMap(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				Point point = new Point();
				point.name = msg.reader().readUTF();
				point.x = msg.reader().readShort();
				point.y = msg.reader().readShort();
				vecPointMap.addElement(point);
			}
		}
		catch (Exception)
		{
		}
	}
}
