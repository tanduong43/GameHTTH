public class MainMaterial : MainItem
{
	public const sbyte MATERIAL_UPGRADE = 0;

	public const sbyte MATERIAL_SPLIT = 1;

	public const sbyte MATERIAL_LUCKY = 2;

	public const sbyte MATERIAL_PROTECT = 3;

	public const sbyte MATERIAL_LUCKY_VIP = 4;

	public const sbyte MATERIAL_PROTECT_VER_2 = 7;

	public MainMaterial(sbyte Id, string name, sbyte type, sbyte idicon, int price, short priceRuby, sbyte isTrade)
	{
		ID = Id;
		base.name = name;
		typeMaterial = type;
		idIcon = idicon;
		base.price = price;
		base.priceRuby = priceRuby;
		base.isTrade = isTrade;
		if (LvUpgrade > 0)
		{
			namepaint = name + " +" + LvUpgrade;
		}
		else
		{
			namepaint = name;
		}
		getInfo();
	}

	public MainMaterial(sbyte typeObj, sbyte Id, string name, sbyte type, sbyte idicon, short num, int price, short priceRuby, sbyte isTrade)
	{
		typeObject = typeObj;
		ID = Id;
		base.name = name;
		typeMaterial = type;
		idIcon = idicon;
		numPotion = num;
		base.price = price;
		base.priceRuby = priceRuby;
		base.isTrade = isTrade;
		indexSort = 1;
		if (LvUpgrade > 0)
		{
			namepaint = name + " +" + LvUpgrade;
		}
		else
		{
			namepaint = name;
		}
	}

	public void getInfo()
	{
		if (typeMaterial >= 0 && typeMaterial < T.mMaterialInfo.Length)
		{
			info = T.mMaterialInfo[typeMaterial];
		}
		else
		{
			info = T.infoMaterialDefault;
		}
	}

	public override MainImage getImage()
	{
		MainImage mainImage = null;
		return ObjectData.getImageAll(idIcon, ObjectData.hashImageMaterialPotion, 6500);
	}

	public override mVector getActionInven(sbyte type)
	{
		mVector mVector2 = new mVector();
		if (type == 0)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdMenuMaterial);
		}
		else if (type == 1)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdSellItem);
		}
		else if (type == 2)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdSetChestPotion);
			mVector2.addElement(GameCanvas.tabInven.cmdChucnang);
		}
		else if (type == 5 && (ID == 4 || ID == 10 || canSell()))
		{
			mVector2.addElement(GameCanvas.tabInvenMarket.cmdSellMarket);
		}
		return mVector2;
	}

	public bool canSell()
	{
		if (MainItem.ID_MATERIAL_CAN_SELL != null)
		{
			for (int i = 0; i < MainItem.ID_MATERIAL_CAN_SELL.Length; i++)
			{
				if (ID == MainItem.ID_MATERIAL_CAN_SELL[i])
				{
					return true;
				}
			}
		}
		return false;
	}

	public override mVector getActionUpgrade()
	{
		if (getStar() || typeMaterial == 3 || typeMaterial == 7)
		{
			mVector mVector2 = new mVector();
			if (ScreenUpgrade.instance != null)
			{
				mVector2.addElement(ScreenUpgrade.instance.cmdBovao);
			}
			return mVector2;
		}
		return null;
	}

	public override mVector getActionShop(sbyte typeShop)
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(TabShop.cmdBuyPotion);
		return mVector2;
	}

	public override mVector getActionChest()
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(TabChest.cmdGetPotion);
		mVector2.addElement(TabChest.cmdChucnang);
		return mVector2;
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		base.paint(g, x, y, w);
		paintPotion(g, x, y, w);
	}

	public override bool getStar()
	{
		if (typeMaterial == 2 || typeMaterial == 4)
		{
			return true;
		}
		return false;
	}
}
