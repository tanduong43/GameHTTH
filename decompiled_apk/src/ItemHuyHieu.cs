public class ItemHuyHieu : MainItem
{
	public ItemHuyHieu(short ID, short IDIcon, string name, string info)
	{
		base.ID = ID;
		idIcon = IDIcon;
		typeObject = 107;
		base.name = name;
		base.info = info;
		namepaint = name;
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		paintColor(g, x, y, w);
		MainImage iconClan = Potion.getIconClan(idIcon);
		if (iconClan != null && iconClan.img != null)
		{
			paintImgItem(g, iconClan, x, y);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
	}

	public override mVector getActionInven(sbyte typeShop)
	{
		mVector mVector2 = new mVector();
		if (colorName == 1)
		{
			mVector2.addElement(GameCanvas.tabInvenClan.cmdDonotUse);
		}
		else
		{
			mVector2.addElement(GameCanvas.tabInvenClan.cmdUsePotion);
		}
		return mVector2;
	}
}
