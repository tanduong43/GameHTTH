public class ItemHair : MainItem
{
	private sbyte typePart;

	public ItemHair(short ID, short IDIcon, string name, sbyte typeObject)
	{
		base.ID = ID;
		idIcon = IDIcon;
		base.typeObject = typeObject;
		base.name = name;
		namepaint = name;
		if (typeObject == 103)
		{
			typePart = 5;
		}
		else
		{
			typePart = 0;
		}
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		value = idIcon;
		if (idIcon == -1)
		{
			value = 0;
		}
		MainObject.paintOnePart(g, value, typePart, x, y, 2);
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
