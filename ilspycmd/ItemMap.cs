public class ItemMap : MainItemMap
{
	public static mImage imgGate;

	private FrameImage imgEff;

	public sbyte typeEff = -1;

	public sbyte stepEff;

	public const sbyte LIGHT_0 = 0;

	public const sbyte LIGHT_1 = 1;

	public ItemMap(short IDItem, short IDImage, int dx, int dy, int[][] Block, sbyte layer)
		: base(IDItem, IDImage, dx, dy, Block, layer)
	{
		TypeItem = 0;
	}

	public void setInfoItem(int x, int y)
	{
		base.x = x;
		base.y = y;
		ySort = y;
	}

	public override void paint(mGraphics g)
	{
		MainImage image = getImage();
		if (typeEff >= 0)
		{
			paintFrist(g);
		}
		if (!GameScreen.isOpenDao && (IDImage == 259 || IDImage == 263))
		{
			if (imgGate == null)
			{
				loadgate();
			}
			g.drawImage(imgGate, x + dx, y + dy, 0);
		}
		else if (image.img != null)
		{
			g.drawImage(image.img, x + dx, y + dy, 0);
		}
		if (typeEff >= 0)
		{
			paintLast(g);
		}
	}

	private void paintLast(mGraphics g)
	{
	}

	private void paintFrist(mGraphics g)
	{
		switch (typeEff)
		{
		case 0:
			if (imgEff == null)
			{
				imgEff = new FrameImage(391, 28, 13);
			}
			else
			{
				imgEff.drawFrame(0, x + dx + 11, y + dy + 14, 0, 3, g);
			}
			break;
		case 1:
			if (imgEff == null)
			{
				imgEff = new FrameImage(391, 28, 13);
				break;
			}
			if (stepEff == 0)
			{
				imgEff.drawFrame(0, x + dx + 11, y + dy + 14, 0, 3, g);
				if (GameCanvas.gameTick % 200 == 140)
				{
					stepEff = 1;
				}
				break;
			}
			if (GameCanvas.gameTick % 200 == 190 || GameCanvas.gameTick % 200 == 195)
			{
				imgEff.drawFrame(0, x + dx + 11, y + dy + 14, 0, 3, g);
			}
			if (GameCanvas.gameTick % 200 == 199)
			{
				stepEff = 0;
			}
			break;
		}
	}

	public static void loadgate()
	{
		imgGate = mImage.createImage("/bg/gate.png");
	}

	public MainImage getImage()
	{
		return ObjectData.getImageAll(IDImage, ObjectData.HashImageItemMap, 0);
	}
}
