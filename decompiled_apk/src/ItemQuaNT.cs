public class ItemQuaNT : MainItem
{
	private int sizeItem = 29;

	public string[] strShow;

	public int priceDauGia;

	public int timeRemain;

	public int giaChot;

	public ItemQuaNT(string name, sbyte cat, short id, short num, sbyte color)
	{
		base.name = name;
		typeObject = cat;
		idIcon = id;
		numPotion = num;
		colorName = color;
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		g.drawImage(AvMain.imgKhungItem, x, y, 3);
		base.paint(g, x, y, w);
		paintPotion(g, x, y, 3);
	}

	public void paintInfo(mGraphics g, int x, int y)
	{
		MainImage image = getImage();
		if (image != null && image.img != null)
		{
			g.drawRegion(image.img, 0, 0, 20, 20, 0, x + 4, y + 4, 0);
		}
		if (strShow == null)
		{
			return;
		}
		if (strShow.Length == 1)
		{
			AvMain.Font3dColor(g, strShow[0], x + 30, y + 4 + 4, 0, colorName);
			return;
		}
		int num = y + 4;
		for (int i = 0; i < strShow.Length; i++)
		{
			AvMain.Font3dColor(g, strShow[i], x + 30, num, 0, colorName);
			num += 13;
		}
	}

	public void setDauGia(int money, int time, int giaChot)
	{
		priceDauGia = money;
		timeRemain = time;
		this.giaChot = giaChot;
	}
}
