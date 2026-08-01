public class MainClan
{
	public string name;

	public string nameCaption;

	public string strVoice;

	public int level;

	public int numMem;

	public int maxNumMem;

	public int xp;

	public int maxXp;

	public int rank;

	public int chucvu;

	public int pointAttri;

	public int maxAttri = 20;

	public int rubiClan;

	public int beryClan;

	public int countAction;

	public short ID;

	public short idIcon = -1;

	public sbyte isLevelUp;

	public sbyte trungsinh;

	public sbyte borderIconClan;

	public mVector vecMem = new mVector("MainClan.vecMem");

	public mVector vecChatCLan = new mVector("MainClan.vecChatClan");

	public mVector vecAchi = new mVector("MainClan.vecAchi");

	public short[] numAttribute = new short[5];

	public MainClan()
	{
	}

	public MainClan(short ID, short idIcon, sbyte levelInClan)
	{
		this.ID = ID;
		this.idIcon = idIcon;
		chucvu = levelInClan;
	}

	public MainClan(short ID, string name)
	{
		this.ID = ID;
		this.name = name;
		numAttribute = new short[5];
	}

	public void setData(short idIcon, string nameCaption, short level, int xp, int maxXP, sbyte numMem, sbyte maxNumMem, int rank, string strVoice)
	{
		this.idIcon = idIcon;
		this.nameCaption = nameCaption;
		this.level = level;
		this.xp = xp;
		maxXp = maxXP;
		this.numMem = numMem;
		this.maxNumMem = maxNumMem;
		this.rank = rank;
		strVoice = GameMidlet.fixString(strVoice);
		this.strVoice = strVoice;
	}

	public void setAttri(short[] mAttri)
	{
		numAttribute = mAttri;
	}

	public static void UpdateRankMe(sbyte level)
	{
		Player.ChucInCLan = level;
	}

	public void updateListMem(mVector vec)
	{
		vecMem = vec;
	}

	public static bool isPhongChuc()
	{
		if (Player.ChucInCLan == 0 || Player.ChucInCLan == 1 || Player.ChucInCLan == 2)
		{
			return true;
		}
		return false;
	}
}
