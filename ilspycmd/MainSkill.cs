public class MainSkill
{
	public const sbyte BUFF_NULL = 0;

	public const sbyte BUFF_NOR = 1;

	public const sbyte BUFF_SPEC = 2;

	public const sbyte BUFF_SHOW = 3;

	public const sbyte BUFF_PLASH = 4;

	public short typeEffSkill;

	public short indexHotKey;

	public short idIcon;

	public short ID;

	public short typeEffBuff;

	public short timebuff;

	public short typeDevil;

	public int timeDelay;

	public int x;

	public int y;

	public long timeBegin;

	public mVector vecPos = new mVector("MainSkill.vecPos");

	public sbyte typeSub;

	public sbyte typeBuff;

	public sbyte Dirbuff;

	public sbyte lvDevil;

	public bool isBuff;

	public bool isBuffSpecNew;

	public int range;

	public int mana;

	public static short[] mRangeSkill = new short[150]
	{
		100, 120, 100, 32, 100, 100, 100, 120, 80, 100,
		120, 120, 120, 64, 48, 48, 48, 48, 120, 120,
		80, 48, 100, 48, 64, 48, 100, 100, 64, 100,
		32, 120, 48, 48, 100, 100, 120, 120, 48, 100,
		48, 100, 120, 120, 48, 48, 100, 100, 100, 120,
		120, 48, 48, 100, 100, 120, 120, 80, 80, 80,
		80, 120, 120, 100, 48, 60, 60, 60, 60, 60,
		60, 84, 84, 32, 48, 84, 48, 120, 32, 32,
		32, 60, 60, 60, 60, 60, 60, 60, 48, 60,
		32, 32, 84, 84, 48, 84, 84, 84, 84, 48,
		72, 72, 84, 48, 84, 48, 84, 48, 84, 84,
		84, 48, 48, 84, 84, 72, 48, 72, 48, 84,
		72, 60, 60, 60, 60, 60, 60, 60, 48, 48,
		48, 48, 48, 120, 120, 120, 120, 120, 120, 120,
		120, 120, 120, 60, 60, 120, 120, 360, 60, 60
	};

	public MainSkill(short ID, short typeEff)
	{
		typeEffSkill = typeEff;
		this.ID = ID;
		if (ID >= 1010 && ID <= 1014)
		{
			isBuffSpecNew = true;
		}
	}

	public void setTypeBuff(sbyte isBuff, short buff, short timebuff)
	{
		typeBuff = isBuff;
		typeEffBuff = buff;
		this.timebuff = timebuff;
		timeBegin = GameCanvas.timeNow;
	}

	public void setPos(int x, int y, mVector vec, sbyte dir)
	{
		this.x = x;
		this.y = y;
		vecPos = vec;
		Dirbuff = dir;
	}

	public void paint(mGraphics g, int x, int y, sbyte lvDevil)
	{
		Skill_Info.paintIcon(g, x, y, idIcon, lvDevil);
	}

	public void getData()
	{
		if (typeEffSkill > mRangeSkill.Length - 1)
		{
			range = 48;
		}
		else
		{
			range = mRangeSkill[typeEffSkill];
		}
	}
}
