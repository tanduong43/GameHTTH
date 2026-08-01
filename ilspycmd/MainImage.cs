public class MainImage
{
	public mImage img;

	public short w;

	public short h;

	public short frame = -1;

	public long count = -1L;

	public int timeImageNull;

	public MainImage()
	{
	}

	public MainImage(mImage im)
	{
		img = im;
		count = 0L;
		w = (short)mImage.getImageWidth(im.image);
		h = (short)mImage.getImageHeight(im.image);
	}

	public void set_W_H()
	{
		if (img != null)
		{
			w = (short)mImage.getImageWidth(img.image);
			h = (short)mImage.getImageHeight(img.image);
		}
	}

	public void set_Frame(int hOne)
	{
		if (img != null)
		{
			h = (short)mImage.getImageHeight(img.image);
			w = (short)mImage.getImageWidth(img.image);
			frame = (short)(h / hOne);
		}
	}

	public void set_Frame()
	{
		if (img != null)
		{
			h = (short)mImage.getImageHeight(img.image);
			w = (short)mImage.getImageWidth(img.image);
			frame = (short)((w <= 0) ? 1 : ((short)(h / w)));
		}
	}
}
