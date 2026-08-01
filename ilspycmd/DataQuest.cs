public class DataQuest
{
	public const sbyte Q_GET_ITEM = 2;

	public const sbyte Q_KILL_MONSTER = 1;

	public const sbyte Q_TRANPORTER = 3;

	public const sbyte Q_TALK = 0;

	public sbyte typeQuest;

	public short IDItem;

	public short numMax;

	public short numCur;

	public string nameItem = string.Empty;

	public static string[] nameItemQuest = new string[9] { "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "jj" };

	public DataQuest(sbyte typeQ)
	{
		typeQuest = typeQ;
	}

	public void SetDataQuest(short ID, short numM, short numC)
	{
		IDItem = ID;
		numMax = numM;
		numCur = numC;
		if (numCur > numMax)
		{
			numCur = numMax;
		}
		if (typeQuest == 1)
		{
			nameItem = MainMonster.getCatalogyMonster(ID).name;
		}
		else if (typeQuest == 2)
		{
			nameItem = nameItemQuest[ID];
		}
	}

	public void loadNameAgain()
	{
		if (typeQuest == 1)
		{
			nameItem = MainMonster.getCatalogyMonster(IDItem).name;
		}
	}
}
