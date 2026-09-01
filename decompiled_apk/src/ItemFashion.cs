public class ItemFashion : MainItem
{
	public ItemFashion(short ID, short IDIcon, string name, string info, short[] wear)
	{
		base.ID = ID;
		idIcon = IDIcon;
		typeObject = 105;
		base.name = name;
		base.info = info;
		namepaint = name;
		mWearing = wear;
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		MainImage imageAll = getImageAll();
		paintColor(g, x, y, w);
		if (imageAll != null && imageAll.img != null)
		{
			paintImgItem(g, imageAll, x, y);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		if (LvUpgrade > 0)
		{
			AvMain.FontBorderColor(g, "+" + LvUpgrade, x + MainTab.wItem / 2 - 2, y + MainTab.wItem / 2 - 9 - 2, 1, 6, 0);
		}
	}

	public override mVector getActionShop(sbyte typeShop)
	{
		mVector mVector2 = new mVector();
		if (typeShop != 114 || price != 0 || priceRuby != 0)
		{
			if (price == 0 && priceRuby == 0)
			{
				if (colorName == 4)
				{
					mVector2.addElement(TabShop.cmdDonotUse);
				}
				else
				{
					mVector2.addElement(TabShop.cmdUse);
				}
			}
			else
			{
				mVector2.addElement(TabShop.cmdBuyHair);
			}
		}
		return mVector2;
	}
}
