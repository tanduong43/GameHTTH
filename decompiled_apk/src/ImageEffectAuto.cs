public class ImageEffectAuto
{
	public static MyHashTable hashImageEffAuto = new MyHashTable();

	private long timeRemove;

	private int imageId;

	private mImage img;

	public ImageEffectAuto(int Id)
	{
		imageId = Id;
		img = mImage.createImage("/effauto/eff_" + Id + ".png");
		timeRemove = GameCanvas.timeNow;
	}

	public static mImage setImage(int Id)
	{
		ImageEffectAuto imageEffectAuto = (ImageEffectAuto)hashImageEffAuto.get(string.Empty + Id);
		if (imageEffectAuto == null)
		{
			imageEffectAuto = new ImageEffectAuto(Id);
			hashImageEffAuto.put(string.Empty + Id, imageEffectAuto);
		}
		else
		{
			imageEffectAuto.timeRemove = GameCanvas.timeNow;
		}
		return imageEffectAuto.img;
	}

	public static void SetRemoveAll()
	{
		hashImageEffAuto.clear();
	}
}
