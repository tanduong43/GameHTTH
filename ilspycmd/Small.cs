public class Small
{
	public mImage img;

	public int id;

	public int wimg;

	public int himg;

	public int timePaint;

	public int timeUpdate;

	public Small(mImage img, int id)
	{
		this.img = img;
		this.id = id;
		timePaint = 0;
		timeUpdate = 0;
		if (this.img != null && img.image != null)
		{
			wimg = mImage.getImageWidth(img.image);
			himg = mImage.getImageHeight(img.image);
		}
	}

	public void paint(mGraphics g, int transform, int x, int y, int anchor)
	{
	}

	public void update()
	{
	}
}
