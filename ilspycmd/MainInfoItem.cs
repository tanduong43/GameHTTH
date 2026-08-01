public class MainInfoItem
{
	public sbyte id;

	public sbyte color;

	public sbyte ispercent;

	public sbyte colorMain = -1;

	public int value;

	public string name;

	public MainInfoItem(sbyte id, int value)
	{
		this.id = id;
		this.value = value;
	}

	public MainInfoItem(sbyte id, int value, sbyte colorM)
	{
		this.id = id;
		this.value = value;
		colorMain = colorM;
	}

	public MainInfoItem(string name, sbyte color, sbyte percent)
	{
		this.name = name;
		this.color = color;
		ispercent = percent;
		colorMain = -1;
	}

	public MainInfoItem(string name, int value)
	{
		this.name = name;
		this.value = value;
	}
}
