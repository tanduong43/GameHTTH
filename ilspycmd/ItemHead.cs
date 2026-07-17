public class ItemHead : MainItem
{
	public ItemHead(short ID, short IDIcon, string name)
	{
		base.ID = ID;
		idIcon = IDIcon;
		typeObject = 103;
		base.name = name;
		namepaint = name;
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		value = idIcon;
		if (idIcon == -1)
		{
			value = 0;
		}
		MainObject.paintOnePart(g, value, 5, x, y, 2);
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
			mVector2.addElement(TabShop.cmdBuyHair);
		}
		return mVector2;
	}
}
