public class EffectData
{
	public sbyte[] data;

	public long count = -1L;

	public mImage img;

	public EffectData(sbyte[] data, sbyte[] dataImg)
	{
		this.data = data;
		img = mImage.createImage(dataImg, 0, dataImg.Length);
	}

	public EffectData()
	{
	}

	public void setData(sbyte[] data, sbyte[] dataImg)
	{
		this.data = data;
		img = mImage.createImage(dataImg, 0, dataImg.Length);
	}
}
