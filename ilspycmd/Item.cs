public class Item : MainItem
{
	public static mVector vecItemKichAnCheckInfo = new mVector("Item.vecItemKichAnCheckInfo");

	private MainInfoItem[] mInfoItemSave;

	public Item(sbyte typeItem, short ID, short idIcon, string name, sbyte isTrade)
		: base(typeItem, ID, idIcon, name, isTrade)
	{
		indexSort = 3;
	}

	public Item(sbyte typeItem, short ID, short idIcon, string name, short timeUse, short maxTimeUse, short ruby)
		: base(typeItem, ID, idIcon, name, 0)
	{
		base.timeUse = timeUse;
		if (timeUse > 0)
		{
			setTimeMarket(timeUse * 60);
		}
		base.maxTimeUse = maxTimeUse;
		priceRuby = ruby;
	}

	public void setDataItem(short Lv, sbyte charClass, sbyte colorName, int timeUse, sbyte typeEquip, sbyte LvUp, sbyte numLoKham, short[] mdakham, short valueChetac, sbyte isHoanMy, sbyte valueKichAn)
	{
		Lv_RQ = Lv;
		base.charClass = charClass;
		base.colorName = colorName;
		base.timeUse = timeUse;
		base.typeEquip = typeEquip;
		LvUpgrade = LvUp;
		if (LvUpgrade > 0)
		{
			namepaint = name + " +" + LvUpgrade;
		}
		else
		{
			namepaint = name;
		}
		base.numLoKham = numLoKham;
		mDaKham = mdakham;
		valueCheTac = valueChetac;
		base.isHoanMy = isHoanMy;
		valueKickAn = valueKichAn;
		if (idIcon == 242 && colorName == 0)
		{
			base.colorName = (sbyte)(LvUpgrade / 10);
			typeSpec = 1;
		}
	}

	public void setInfoItem(MainInfoItem[] mInfoItem, int sizeMainInfo)
	{
		vecInfo.removeAllElements();
		bool flag = false;
		if (valueKickAn >= 0)
		{
			string dataKichAn = MainItem.getDataKichAn(this);
			string[] array = mFont.tahoma_7_black.splitFontArray(dataKichAn, wInfo);
			for (int i = 0; i < array.Length; i++)
			{
				addInfo(array[i], 2);
			}
		}
		addInfo(T.thongtincoban, 5, infoShow.HARDCODE_PAINT_CENTER);
		if (typeEquip != 10 && typeSpec != 1)
		{
			addInfo(T.Lv + ": " + Lv_RQ, (sbyte)((Lv_RQ > GameScreen.player.Lv) ? 3 : 4), infoShow.HARDCODE_CHECK_LVRQ);
		}
		addInfo(T.chetac + valueCheTac, 0);
		if (isTrade == 2)
		{
			addInfo(T.khoagiaodich, 3);
		}
		if (LvUpgrade >= 11 && LvUpgrade <= 15 && typeSpec != 1)
		{
			addInfo(T.thongtinfullset, 5, infoShow.HARDCODE_PAINT_CENTER);
			sbyte color = 7;
			addInfo(T.fullset + LvUpgrade, color, (sbyte)(90 + LvUpgrade));
			for (int j = 0; j < LvUpgrade - 10; j++)
			{
				sbyte b = (sbyte)(101 + j);
				if (b > 105)
				{
					b = 105;
				}
				addInfo(T.mFullSet[j], color, b);
				if (j == 4)
				{
					addInfo(T.mFullSet[j + 1], color, b);
				}
			}
		}
		addInfo(T.thongtinchiso, 5, infoShow.HARDCODE_PAINT_CENTER_CHI_SO);
		for (int k = 0; k < mInfoItem.Length; k++)
		{
			addInfo(mInfoItem[k].id, mInfoItem[k].value, mInfoItem[k].colorMain);
			if (k == sizeMainInfo - 1 && sizeMainInfo < mInfoItem.Length)
			{
				addInfo(T.thongtindakham, 5, infoShow.HARDCODE_PAINT_CENTER);
			}
		}
		if (flag)
		{
			mInfoItemSave = mInfoItem;
		}
		else
		{
			mInfoItemSave = null;
		}
	}

	public void setInfoItemSell(MainInfoItem[] mInfoItem)
	{
		vecInfo.removeAllElements();
		if (charClass > 0 && !Player.isClazz(charClass))
		{
			addInfo(T.mClazz[charClass], 6);
		}
		addInfo(T.Lv + Lv_RQ, (sbyte)((Lv_RQ > GameScreen.player.Lv) ? 6 : 4));
		for (int i = 0; i < mInfoItem.Length; i++)
		{
			addInfoSell(mInfoItem[i].id, mInfoItem[i].value);
		}
	}

	public override mVector getActionInven(sbyte type)
	{
		mVector mVector2 = new mVector();
		if (type == 0)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdUseItem);
			mVector2.addElement(GameCanvas.tabInven.cmdRemoveItem);
		}
		else if (type == 1)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdSellItem);
			mVector2.addElement(GameCanvas.tabInven.cmdSellAll);
		}
		else if (type == 2)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdSetChestItem);
		}
		else if (type == 5)
		{
			mVector2.addElement(GameCanvas.tabInvenMarket.cmdSellMarket);
		}
		return mVector2;
	}

	public override mVector getActionShop(sbyte typeShop)
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(TabShop.cmdBuyItem);
		return mVector2;
	}

	public override mVector getActionChest()
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(TabChest.cmdGetItem);
		mVector2.addElement(TabChest.cmdUpgrade);
		return mVector2;
	}

	public override mVector getActionUpgrade()
	{
		mVector mVector2 = new mVector();
		if (ScreenUpgrade.instance != null)
		{
			mVector2.addElement(ScreenUpgrade.instance.cmdBovao);
		}
		return mVector2;
	}

	public override mVector getActionSplit()
	{
		mVector mVector2 = new mVector();
		if (SplitScreen.instance != null)
		{
			mVector2.addElement(SplitScreen.instance.cmdBovao);
		}
		return mVector2;
	}

	public override MainImage getImage()
	{
		MainImage mainImage = null;
		return ObjectData.getImageAll(idIcon, ObjectData.hashImageItem, 3000);
	}

	public MainImage getImageRuong()
	{
		MainImage mainImage = null;
		return ObjectData.getImageAll(idIcon, ObjectData.HashImageOtherNew, 23000);
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		base.paint(g, x, y, w);
	}

	public void paintRuong(mGraphics g, int x, int y, int w)
	{
		MainImage mainImage = null;
		mainImage = getImageRuong();
		if (mainImage != null && mainImage.img != null)
		{
			g.drawImage(mainImage.img, x, y, 3);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		if (!GameCanvas.lowGraphic)
		{
			g.drawImage(AvMain.mImgRoomW[3], x, y - w / 2, 3);
		}
		if (timeUse > 0)
		{
			if (!GameCanvas.lowGraphic)
			{
				g.drawRegion(AvMain.mImgRoomW[2], 0, GameCanvas.gameTick / 12 % 4 * 11, 11, 11, 0, x - 22, y - w / 2, 3);
			}
			marketTime.paintCountDownTicketHour(g, mFont.tahoma_7_white, x - 6, y - w / 2 - 5, 0);
			return;
		}
		if (timeUse == 0)
		{
			mFont.tahoma_7b_yellow.drawString(g, T.open, x, y - w / 2 - 5, 2);
			return;
		}
		string text = maxTimeUse / 60 + "h";
		if (maxTimeUse % 60 != 0)
		{
			text = text + maxTimeUse % 60 + "'";
		}
		mFont.tahoma_7b_white.drawString(g, text, x, y - w / 2 - 5, 2);
	}

	public override void setTimeMarket(int time)
	{
		timeUse = time;
		marketTime.setCountDown(time);
	}

	public static void CheckAddDataKichAn()
	{
		for (int i = 0; i < vecItemKichAnCheckInfo.size(); i++)
		{
			Item item = (Item)vecItemKichAnCheckInfo.elementAt(i);
			string dataKichAn = MainItem.getDataKichAn(item);
			if (dataKichAn.Length > 0)
			{
				item.setInfoItem(item.mInfoItemSave);
				vecItemKichAnCheckInfo.removeElement(item);
				i--;
			}
		}
	}
}
