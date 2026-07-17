public class Main_Attribute
{
	public sbyte type;

	public short value;

	public short valuePlus;

	public string name;

	public MainInfoItem[] minfo;

	public Main_Attribute(sbyte type, short value, short valueP, string name, MainInfoItem[] minfo)
	{
		this.type = type;
		this.value = value;
		valuePlus = valueP;
		this.name = name;
		this.minfo = minfo;
	}
}
