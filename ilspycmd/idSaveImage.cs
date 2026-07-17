public class idSaveImage
{
	public short id;

	public sbyte[] mbytImage;

	public string name;

	public idSaveImage(short id, sbyte[] msbyte)
	{
		this.id = id;
		mbytImage = msbyte;
	}

	public idSaveImage(string name, sbyte[] msbyte)
	{
		this.name = name;
		mbytImage = msbyte;
	}
}
