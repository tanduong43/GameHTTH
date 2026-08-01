using System;

public class CatalogyMonster
{
	public const sbyte MON_NOR = 0;

	public const sbyte MON_HUMAN = 1;

	public int idCat;

	public int lv;

	public int maxHp;

	public sbyte typeMove;

	public sbyte isHumanPart;

	public sbyte typeMonster;

	public short head;

	public short hair;

	public short hOne;

	public short idIcon;

	public short[] mWearing;

	public string name = string.Empty;

	public bool isTemplate;

	public long timeRequest;

	public CatalogyMonster(int id)
	{
		idCat = id;
	}

	public CatalogyMonster(int id, int lv, int maxHp, sbyte typeMove, string name, sbyte ishuman, short hOne, sbyte typeMonster)
	{
		idCat = id;
		this.lv = lv;
		this.maxHp = maxHp;
		this.typeMove = typeMove;
		this.name = name;
		isHumanPart = ishuman;
		this.hOne = hOne;
		this.typeMonster = typeMonster;
	}

	public void addData(short head, short hair, short[] wearing)
	{
		this.head = head;
		this.hair = hair;
		mWearing = wearing;
	}

	public static void LoadDataMon(DataInputStream iss, bool isSave)
	{
		try
		{
			if (iss == null)
			{
				GlobalService.gI().get_DATA(15);
				return;
			}
			short num = iss.readShort();
			for (int i = 0; i < num; i++)
			{
				short num2 = iss.readShort();
				string text = iss.readUTF();
				short num3 = iss.readShort();
				short num4 = iss.readShort();
				int num5 = iss.readInt();
				sbyte b = iss.readByte();
				sbyte b2 = iss.readByte();
				sbyte b3 = iss.readByte();
				CatalogyMonster catalogyMonster = new CatalogyMonster(num2, num3, num5, b, text, b2, num4, b3);
				if (b2 == 1)
				{
					short num6 = iss.readShort();
					short num7 = iss.readShort();
					sbyte b4 = iss.readByte();
					short[] array = new short[b4];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = -1;
					}
					for (int k = 0; k < b4; k++)
					{
						sbyte b5 = iss.readByte();
						if (b5 == 1)
						{
							array[k] = iss.readShort();
						}
					}
					catalogyMonster.addData(num6, num7, array);
				}
				else
				{
					catalogyMonster.idIcon = iss.readShort();
				}
				catalogyMonster.isTemplate = true;
				MainMonster.hashMonsterTemplate.put(string.Empty + num2, catalogyMonster);
			}
			LoadMapScreen.isLoadDataMon = true;
			if (isSave)
			{
				GlobalService.VerMonster = iss.readShort();
				SaveRms.saveVer(GlobalService.VerMonster, "VerdataMon");
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}

	public static void LoadDataMonTemplate(DataInputStream iss)
	{
		try
		{
			short num = iss.readShort();
			string text = iss.readUTF();
			short num2 = iss.readShort();
			short num3 = iss.readShort();
			int num4 = iss.readInt();
			sbyte b = iss.readByte();
			sbyte b2 = iss.readByte();
			sbyte b3 = iss.readByte();
			CatalogyMonster catalogyMonster = new CatalogyMonster(num, num2, num4, b, text, b2, num3, b3);
			if (b2 == 1)
			{
				short num5 = iss.readShort();
				short num6 = iss.readShort();
				sbyte b4 = iss.readByte();
				short[] array = new short[b4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = -1;
				}
				for (int j = 0; j < b4; j++)
				{
					sbyte b5 = iss.readByte();
					if (b5 == 1)
					{
						array[j] = iss.readShort();
					}
				}
				catalogyMonster.addData(num5, num6, array);
			}
			else
			{
				catalogyMonster.idIcon = iss.readShort();
			}
			catalogyMonster.isTemplate = true;
			MainMonster.hashMonsterTemplate.put(string.Empty + num, catalogyMonster);
			iss.close();
		}
		catch (Exception)
		{
		}
	}
}
