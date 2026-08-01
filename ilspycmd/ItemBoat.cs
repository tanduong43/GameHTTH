public class ItemBoat : MainItem
{
	public ItemBoat(short ID, short IDIcon, short IdImage, string name, sbyte type)
	{
		base.ID = ID;
		idIcon = (short)(IDIcon + 500);
		idPart = IdImage;
		typeObject = 102;
		base.name = name;
		namepaint = name;
		typeBoat = type;
	}

	public override MainImage getImage()
	{
		MainImage mainImage = null;
		return ObjectData.getImageAll(idIcon, ObjectData.hashImageBoat, 8000);
	}

	public static void paintPartBoat(mGraphics g, short idImage, int type, int x, int y, int dy, int Dir)
	{
		if (type >= 100)
		{
			int num = GameCanvas.gameTick / 6 % 2;
			int num2 = ((Dir != 2) ? (-4) : 4);
			Boat.fraPirateUnity.drawFrame((type - 100) * 2 + num, x + num2, y + Boat.hlech - 57 - dy, Dir, 33, g);
			return;
		}
		MainImage mainImage = null;
		mainImage = ObjectData.getImageAll(idImage, ObjectData.hashImageBoat, 8000);
		if (mainImage != null && mainImage.img != null)
		{
			int num3 = ((Dir != 2) ? (-11) : 11);
			if (type == 0)
			{
				g.drawRegion(mainImage.img, 0, 0, mainImage.w, mainImage.h + dy, Dir, x, y + Boat.hlech, 33);
			}
			if (type == 1)
			{
				g.drawRegion(mainImage.img, 0, 0, mainImage.w, mainImage.h, Dir, x + num3, y + Boat.hlech - 15 - dy, 33);
			}
			if (type == 2)
			{
				int num4 = GameCanvas.gameTick / 6 % 2;
				g.drawRegion(mainImage.img, 0, mainImage.h / 2 * num4, mainImage.w, mainImage.h / 2, Dir, x + num3, y + Boat.hlech - 23 - dy, 33);
			}
			if (type == 3)
			{
				num3 = 0;
				g.drawRegion(mainImage.img, 0, 0, mainImage.w, mainImage.h + dy, Dir, x + num3, y + Boat.hlech, 33);
			}
		}
	}

	public override mVector getActionShop(sbyte typeShop)
	{
		mVector mVector2 = new mVector();
		if (price == 0 && priceRuby == 0)
		{
			mVector2.addElement(TabShop.cmdUse);
		}
		else
		{
			mVector2.addElement(TabShop.cmdBuyItem);
		}
		return mVector2;
	}
}
