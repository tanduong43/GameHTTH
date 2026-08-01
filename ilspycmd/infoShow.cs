public class infoShow
{
	public string strShow = string.Empty;

	public sbyte color;

	public sbyte colorMain = -1;

	public int id;

	public int value;

	public static sbyte HARDCODE_CHECK_LVRQ = -100;

	public static sbyte HARDCODE_PAINT_CENTER = -99;

	public static sbyte HARDCODE_PAINT_CENTER_CHI_SO = -98;

	public static sbyte HARDCODE_INFO_CO_BAN = 100;

	public static sbyte HARDCODE_CHECK_FULLSET_1 = 101;

	public static sbyte HARDCODE_CHECK_FULLSET_2 = 102;

	public static sbyte HARDCODE_CHECK_FULLSET_3 = 103;

	public static sbyte HARDCODE_CHECK_FULLSET_4 = 104;

	public static sbyte HARDCODE_CHECK_FULLSET_5 = 105;

	public infoShow(int id, int value, string str, sbyte color, sbyte colorMain)
	{
		strShow = str;
		this.id = id;
		this.value = value;
		this.color = color;
		this.colorMain = colorMain;
	}

	public infoShow(int id, int value, sbyte color, sbyte colorMain)
	{
		this.id = id;
		this.value = value;
		this.color = color;
		this.colorMain = colorMain;
	}

	public string getInfoFormID()
	{
		if (id < 0)
		{
			return strShow;
		}
		string text = MainItem.mNameAttributes[id].name + " ";
		return text + MainItem.strGetPercent(value, MainItem.mNameAttributes[id].ispercent);
	}
}
