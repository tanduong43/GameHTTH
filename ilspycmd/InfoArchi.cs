public class InfoArchi
{
	public string name;

	public string info;

	public int valueMax;

	public int valueCur;

	public int maxLechInfo;

	public short idIcon;

	public sbyte typeReward = -1;

	public static sbyte REWARD_OFF = -1;

	public static sbyte REWARD_DOING;

	public static sbyte REWARD_READY = 1;

	public static sbyte REWARD_GOT = 2;

	public InfoArchi()
	{
	}

	public InfoArchi(string name, string info, int valueCur, int valueMax, short idIcon, sbyte typeReward)
	{
		this.name = name;
		this.info = info;
		this.valueMax = valueMax;
		this.valueCur = valueCur;
		this.idIcon = idIcon;
		this.typeReward = typeReward;
		int num = 260;
		if (num > MotherCanvas.w)
		{
			num = MotherCanvas.w;
		}
		num -= 130;
		maxLechInfo = mFont.tahoma_7_black.getWidth(info) - num;
		if (maxLechInfo > 0)
		{
			maxLechInfo += 30;
		}
	}
}
