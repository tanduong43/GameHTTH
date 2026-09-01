public class mPart
{
	public int type;

	public int timeNull;

	public long count = -1L;

	public PartImage[] pi;

	public mPart()
	{
	}

	public mPart(int type)
	{
		this.type = type;
		if (type == 0)
		{
			pi = new PartImage[5];
		}
		if (type == 2)
		{
			pi = new PartImage[15];
		}
		if (type == 1)
		{
			pi = new PartImage[20];
		}
		if (type == 4)
		{
			pi = new PartImage[2];
		}
		if (type == 5)
		{
			pi = new PartImage[2];
		}
		if (type == 3)
		{
			pi = new PartImage[24];
		}
	}
}
