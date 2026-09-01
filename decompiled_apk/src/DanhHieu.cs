using System.Collections.Generic;

public class DanhHieu
{
	public int id;

	public int id_image;

	public string name;

	public int frame;

	public List<MainInfoItem> option = new List<MainInfoItem>();

	public sbyte so_huu;

	public long time;

	public static Dictionary<short, mImage> find_icon = new Dictionary<short, mImage>();

	public DanhHieu(int id, int frame)
	{
		this.id = id;
		id_image = id;
		this.frame = ((frame <= 0) ? 1 : frame);
	}

	public DanhHieu(int id, string name, int id_img, int frame)
	{
		this.id = id;
		this.name = name;
		id_image = id_img;
		this.frame = ((frame <= 0) ? 1 : frame);
	}

	public void paint(mGraphics g, int x, int y)
	{
		int num = ((id_image >= 0) ? id_image : id);
		mImage mImage2 = get_Image_Frame((short)num);
		if (mImage2 != null && mImage2.image != null)
		{
			int num2 = ((frame <= 0) ? 1 : frame);
			int imageWidth = mImage.getImageWidth(mImage2.image);
			int imageHeight = mImage.getImageHeight(mImage2.image);
			int num3 = imageHeight / num2;
			if (num3 <= 0)
			{
				num3 = imageHeight;
			}
			int y2 = GameCanvas.gameTickChia4 % num2 * num3;
			g.drawRegion(mImage2.image, 0, y2, imageWidth, num3, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
		}
		else if (mSystem.currentTimeMillis() - time > 2000)
		{
			GlobalService.gI().danhhieu(3, num, 0);
			time = mSystem.currentTimeMillis();
		}
	}

	public static mImage get_Image_Frame(short id)
	{
		if (find_icon.ContainsKey(id))
		{
			return find_icon[id];
		}
		return null;
	}

	public static void add_Image_Frame(short id, mImage image)
	{
		if (image != null && image.image != null)
		{
			if (!find_icon.ContainsKey(id))
			{
				find_icon.Add(id, image);
			}
			else
			{
				find_icon[id] = image;
			}
		}
	}
}
