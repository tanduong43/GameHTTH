using System;

public class MainItem
{
	public int numInt = 1;

	public int x;

	public int y;

	public int timeUse;

	public int price;

	public int priceVND;

	public short ID;

	public short idIcon = -1;

	public short priceRuby;

	public short Lv_RQ;

	public short numPotion = 1;

	public short indexHotKey;

	public short idPart;

	public short maxTimeUse;

	public short indexInfoPotion;

	public short valueCheTac;

	public short IDMarket;

	public short numPotionNeed;

	public sbyte indexSort;

	public sbyte typeMaterial;

	public sbyte LvUpgrade;

	public sbyte typeBoat;

	public sbyte numLoKham;

	public sbyte typelock;

	public sbyte numHoleDaDuc;

	public sbyte LvDevilSkill;

	public sbyte phanTramDevilSkill;

	public sbyte indexUniform = -1;

	public sbyte isHoanMy;

	public sbyte valueKickAn;

	public sbyte typeSpec;

	public sbyte perSuc;

	public short[] mDaKham;

	public short[] mWearing;

	public string name;

	public string namepaint = string.Empty;

	public string info;

	public string nameUse;

	public mVector vecInfo = new mVector("MainItem.vecInfo");

	public sbyte isTrade;

	public sbyte charClass;

	public sbyte Hp_Mp_Other;

	public sbyte typeEquip;

	public int wInfo = 140;

	public int hInfo = 40;

	public short timeDelayPotion;

	public short value;

	public sbyte colorName;

	public sbyte typeObject;

	public sbyte typeMarket;

	public static MainInfoItem[] mNameAttributes;

	public static MyHashTable hashPotionTem = new MyHashTable();

	public static MyHashTable hashMaterialTem = new MyHashTable();

	public static MyHashTable hashPotionClan = new MyHashTable();

	public static MyHashTable hashAttriKichAn = new MyHashTable();

	public bool isRemove;

	public bool isIconClan;

	public bool isShop;

	public bool isPaint = true;

	public bool isRemoveVecEff;

	public static mImage imgColorItem;

	public FrameImage fraImgVip;

	public bool isloadfra;

	public static Effect_UpLv_Item eff_UpLv = new Effect_UpLv_Item();

	public static Effect_UpLv_Item eff_UpLv_Clan = new Effect_UpLv_Item();

	public static Effect_UpLv_Item eff_UpLv_Sub = new Effect_UpLv_Item();

	public CountDownTicket marketTime = new CountDownTicket();

	public const sbyte MARKET_NORMAL = 0;

	public const sbyte MARKET_SELLING = 1;

	public const sbyte MARKET_SELLED = 2;

	public const sbyte MARKET_SELL_END = 3;

	public const sbyte MARKET_BUY_COMPLETE = 4;

	public static short[] ID_POTION_CAN_SELL;

	public static short[] ID_MATERIAL_CAN_SELL;

	public const sbyte SPEC_HEART = 1;

	public bool isQuickOpen;

	public bool isKham;

	public int hRunInfo;

	private int lastTick;

	private int framepaint;

	public static int[] mValueUpgrade = new int[31]
	{
		0, 10, 20, 30, 45, 60, 75, 90, 110, 130,
		150, 155, 160, 165, 170, 175, 180, 185, 190, 195,
		200, 205, 210, 215, 220, 225, 230, 235, 240, 245,
		250
	};

	public MainItem()
	{
	}

	public MainItem(sbyte type, short idIcon, short id, int num)
	{
		typeObject = type;
		this.idIcon = idIcon;
		ID = id;
		numInt = num;
		numPotion = 0;
	}

	public MainItem(sbyte type, short IdIcon, short ID)
	{
		typeObject = type;
		idIcon = IdIcon;
		this.ID = ID;
		numPotion = 0;
	}

	public MainItem(sbyte typeItem, short ID, short idIcon, string name, sbyte isTrade)
	{
		typeObject = typeItem;
		this.ID = ID;
		this.idIcon = idIcon;
		this.name = name;
		this.isTrade = isTrade;
		namepaint = name;
	}

	public MainItem(sbyte typeItem, short ID, short idIcon, short num, sbyte color, sbyte lvUp)
	{
		typeObject = typeItem;
		this.ID = ID;
		this.idIcon = idIcon;
		numPotion = num;
		colorName = color;
		LvUpgrade = lvUp;
		if (LvUpgrade > 0)
		{
			namepaint = name + " +" + LvUpgrade;
		}
		else
		{
			namepaint = name;
		}
	}

	public void setInfoPotion(string info)
	{
		if (info.Length != 0)
		{
			string[] array = null;
			array = mFont.tahoma_7_white.splitFontArray(info, wInfo);
			for (int i = 0; i < array.Length; i++)
			{
				addInfo(array[i], 0);
			}
			if (!isShop || numPotion > 1)
			{
				addInfo(T.soluong + ": " + numPotion, 0);
			}
		}
	}

	public virtual void setInfoItem(MainInfoItem[] mInfoItem)
	{
	}

	public void addInfoSell(short id, int value)
	{
		string text = mNameAttributes[id].name + " ";
		text = text + strGetPercent(value, mNameAttributes[id].ispercent) + " + ?";
		int num = mFont.tahoma_7_black.getWidth(text) + 10;
		if (num > wInfo)
		{
			wInfo = num;
		}
		vecInfo.addElement(new infoShow(id, value, mNameAttributes[id].color, -1));
		updateHInfo();
	}

	public static string getInfoAttriSS(short id, int value, int valueLvUp, int valueLvCur)
	{
		string text = mNameAttributes[id].name + " ";
		int num = valueSameUpgrade(value, valueLvCur, valueLvUp);
		return text + strGetPercent(num, mNameAttributes[id].ispercent);
	}

	public void addInfo(short id, int value, sbyte colorMain)
	{
		string text = mNameAttributes[id].name + " ";
		text += strGetPercent(value, mNameAttributes[id].ispercent);
		int num = mFont.tahoma_7b_black.getWidth(text) + 4;
		if (typelock == 1)
		{
			num += 12;
		}
		if (charClass >= 1)
		{
			num += 12;
		}
		if (LvUpgrade > 0)
		{
			num += 16;
		}
		if (num > wInfo)
		{
			wInfo = num;
		}
		sbyte color = mNameAttributes[id].color;
		if (colorMain >= 0 && colorMain <= 8)
		{
			color = colorMain;
		}
		vecInfo.addElement(new infoShow(id, value, color, colorMain));
		updateHInfo();
	}

	public void addInfo(short id, int value, sbyte colorMain, sbyte percent)
	{
		string text = mNameAttributes[id].name + " ";
		text = ((percent == -1) ? (text + strGetPercent(value, mNameAttributes[id].ispercent)) : (text + strGetPercent(value, percent)));
		int num = mFont.tahoma_7b_black.getWidth(text) + 4;
		if (typelock == 1)
		{
			num += 12;
		}
		if (charClass >= 1)
		{
			num += 12;
		}
		if (LvUpgrade > 0)
		{
			num += 16;
		}
		if (num > wInfo)
		{
			wInfo = num;
		}
		sbyte color = mNameAttributes[id].color;
		if (colorMain >= 0 && colorMain <= 8)
		{
			color = colorMain;
		}
		vecInfo.addElement(new infoShow(id, value, color, colorMain));
		updateHInfo();
	}

	public virtual void addInfo(string str, sbyte color)
	{
		vecInfo.addElement(new infoShow(-1, 0, str, color, -1));
		updateHInfo();
	}

	public virtual void addInfo(string str, sbyte color, sbyte colorMain)
	{
		vecInfo.addElement(new infoShow(-1, 0, str, color, colorMain));
		updateHInfo();
	}

	public void addInfoFrist(string str, sbyte color)
	{
		vecInfo.insertElementAt(new infoShow(-1, 0, str, color, -1), 0);
		updateHInfo();
	}

	public void updateHInfo()
	{
		hInfo = (vecInfo.size() + 1) * GameCanvas.hText;
		if (numLoKham > 0)
		{
			hInfo += 22;
		}
		if (isHoanMy == 1)
		{
			hInfo += 14;
		}
		if (hInfo > MainTab.hTab - GameCanvas.hCommand * 3 / 2)
		{
			hRunInfo = hInfo - (MainTab.hTab - GameCanvas.hCommand * 3 / 2);
		}
		else
		{
			hRunInfo = 0;
		}
	}

	public static string strGetPercent(int value, sbyte isPer)
	{
		string text = string.Empty;
		switch (isPer)
		{
		case 0:
			text += value;
			break;
		case 1:
		{
			string text4 = text;
			text = text4 + value / 10 + "," + CRes.abs(value) % 10 + "%";
			break;
		}
		case 2:
		{
			string text3 = text;
			text = text3 + value / 100 + "," + CRes.abs(value) % 100 + "%";
			break;
		}
		case 10:
		{
			string text2 = text;
			text = text2 + value / 10 + "," + CRes.abs(value) % 10 + "s";
			break;
		}
		}
		return text;
	}

	public void paintColor(mGraphics g, int x, int y, int w)
	{
		if (colorName * 32 + w <= mImage.getImageHeight(imgColorItem.image))
		{
			g.drawRegion(imgColorItem, 0, colorName * 32, w, w, 0, x, y, 3);
		}
	}

	public virtual void paint(mGraphics g, int x, int y, int w)
	{
		MainImage mainImage = null;
		mainImage = getImage();
		if (mainImage != null && mainImage.img != null)
		{
			paintImgItem(g, mainImage, x, y);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		paintEff_LvUp(g, x, y, w, 0);
	}

	public virtual void paintNumPotion(mGraphics g, int x, int y, int w, short num)
	{
		if (numPotionNeed > 0)
		{
			g.drawImage(AvMain.imgBgnum, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 6, 3);
			mFont mFont2 = mFont.tahoma_7_yellow;
			if (numPotionNeed > numPotion)
			{
				mFont2 = mFont.tahoma_7_red;
			}
			mFont2.drawString(g, numPotion + "/" + numPotionNeed, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 9 - 2, 2);
		}
		else if (num > 1)
		{
			g.drawImage(AvMain.imgBgnum, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 6, 3);
			mFont.tahoma_7_yellow.drawString(g, string.Empty + num, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 9 - 2, 2);
		}
	}

	public virtual void paintQuay(mGraphics g, int x, int y, int w)
	{
	}

	public virtual void paintNumPotionQuay(mGraphics g, int x, int y, int w, short num)
	{
		if (numPotionNeed > 0)
		{
			g.drawImage(AvMain.imgBgnum, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 6, 3);
			mFont mFont2 = mFont.tahoma_7_yellow;
			if (numPotionNeed > numPotion)
			{
				mFont2 = mFont.tahoma_7_red;
			}
			mFont2.drawString(g, numPotion + "/" + numPotionNeed, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 9 - 2, 2);
		}
		else
		{
			g.drawImage(AvMain.imgBgnum, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 6, 3);
			mFont.tahoma_7_yellow.drawString(g, string.Empty + num, x + MainTab.wItem / 2 - 11, y + MainTab.wItem / 2 - 9 - 2, 2);
		}
	}

	public virtual void paintPotion(mGraphics g, int x, int y, int w)
	{
		paintNumPotion(g, x, y, w, numPotion);
	}

	public void paint(mGraphics g, int x, int y, int w, int lech)
	{
		MainImage mainImage = null;
		mainImage = getImage();
		if (mainImage != null && mainImage.img != null)
		{
			paintImgItem(g, mainImage, x, y);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		paintEff_LvUp(g, x, y, w, lech);
	}

	public void paintEffSub(mGraphics g, int x, int y, int w, int lech)
	{
		MainImage mainImage = null;
		mainImage = getImage();
		if (mainImage != null && mainImage.img != null)
		{
			paintImgItem(g, mainImage, x, y);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		if (LvUpgrade > 0)
		{
			eff_UpLv_Sub.paintUpgradeEffect(x, y, LvUpgrade, w - 4, g, lech, isBorder: true);
		}
	}

	public void paintEff_LvUp(mGraphics g, int x, int y, int w, int lech)
	{
		if (indexUniform > -1 && indexUniform <= 2)
		{
			AvMain.fraUniform.drawFrame(indexUniform, x - w / 2 + 2, y + w / 2 - 9, 0, 0, g);
		}
		if (typelock == 1)
		{
			g.drawImage(AvMain.imgLock, x + w / 2 - 1 - 8, y - w / 2 + 2, 0);
		}
		if (LvUpgrade > 0)
		{
			int upgrade = LvUpgrade;
			if (typeSpec == 1)
			{
				upgrade = LvUpgrade % 10;
			}
			eff_UpLv.paintUpgradeEffect(x, y, upgrade, w - 4, g, lech, isBorder: true);
		}
		if (mDaKham == null)
		{
			return;
		}
		for (int i = 0; i < mDaKham.Length; i++)
		{
			int num = (mDaKham[i] - 44) / 6;
			if (mDaKham[i] >= 324 && mDaKham[i] <= 326)
			{
				num = GameCanvas.gameTick / 5 % 6;
			}
			else if (mDaKham[i] >= 241 && mDaKham[i] <= 270)
			{
				num = (mDaKham[i] - 241) / 5;
			}
			else if ((mDaKham[i] >= 368 && mDaKham[i] <= 373) || (mDaKham[i] >= 362 && mDaKham[i] <= 367) || (mDaKham[i] >= 647 && mDaKham[i] <= 682))
			{
				num = 6;
			}
			int num2 = x - w / 2 + 5 + i / 2 * 9;
			int num3 = y - w / 2 + 5;
			if (i % 2 == 1)
			{
				num2 = x + w / 2 - 5 - i / 2 * 9;
				num3 = y + w / 2 - 5;
			}
			if (num >= 0 && num < AvMain.fraEffItem.nFrame)
			{
				if ((mDaKham[i] >= 241 && mDaKham[i] <= 270) || (mDaKham[i] >= 368 && mDaKham[i] <= 373) || (mDaKham[i] >= 647 && mDaKham[i] <= 682))
				{
					AvMain.fraEffItem2.drawFrame(num * 2 + GameCanvas.gameTick / 5 % 2, num2, num3, 0, 3, g);
				}
				else
				{
					AvMain.fraEffItem.drawFrame(num * 2 + GameCanvas.gameTick / 5 % 2, num2, num3, 0, 3, g);
				}
			}
		}
	}

	public MainImage getImageAll()
	{
		MainImage result = null;
		if (typeObject == 3)
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.hashImageItem, 3000);
		}
		if (typeObject == 7)
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.hashImageMaterialPotion, 6500);
		}
		if (typeObject == 4)
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.hashImagePotion, 2000);
		}
		if (typeObject == 100)
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.hashImageItemOther, 9000);
		}
		if (typeObject == 104)
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.hashImageSkill, 4000);
		}
		if (typeObject == 105)
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.HashImageFashion, 20000);
		}
		if (typeObject == 110)
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.HashImageOtherNew, 23000);
		}
		return result;
	}

	public void paintAllItem_Num1(mGraphics g, int x, int y, int w, int lech, sbyte color, short numPaint)
	{
		paintAllItem(g, x, y, w, lech, color, numPaint, typePaintNum: true);
	}

	public void paintAllItem(mGraphics g, int x, int y, int w, int lech, sbyte color)
	{
		paintAllItem(g, x, y, w, lech, color, numPotion, typePaintNum: true);
	}

	public void paintAllItem(mGraphics g, int x, int y, int w, int lech, sbyte color, short numpaint, bool typePaintNum)
	{
		MainImage imageAll = getImageAll();
		if (imageAll != null && imageAll.img != null)
		{
			paintImgItem(g, imageAll, x, y);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		if (typeObject == 3)
		{
			AvMain.setTextColor(color).drawString(g, "Lv." + LvUpgrade, x + MainTab.wItem / 2 - 2, y + MainTab.wItem / 2 - 9 - 2, 1);
		}
		else if (typePaintNum)
		{
			paintNumPotion(g, x, y, w, numpaint);
		}
		paintEff_LvUp(g, x, y, w, lech);
	}

	public void paintImgItem(mGraphics g, MainImage img, int x, int y)
	{
		if (!isloadfra)
		{
			setFraImageVip(img);
		}
		if (fraImgVip != null)
		{
			int num = ((framepaint < fraImgVip.nFrame - 1) ? 3 : 15);
			if (CRes.abs(GameCanvas.gameTick - lastTick) > num)
			{
				framepaint++;
				if (framepaint >= fraImgVip.nFrame)
				{
					framepaint = 0;
				}
				lastTick = GameCanvas.gameTick;
			}
			fraImgVip.drawFrame((framepaint <= fraImgVip.nFrame - 1) ? framepaint : 0, x, y, 0, 3, g);
		}
		else
		{
			g.drawImage(img.img, x, y, 3);
		}
	}

	private void setFraImageVip(MainImage img)
	{
		if (img != null && img.img != null)
		{
			int imageWidth = mImage.getImageWidth(img.img.image);
			if (mImage.getImageHeight(img.img.image) / 2 >= imageWidth)
			{
				fraImgVip = new FrameImage(img.img, imageWidth, imageWidth);
			}
			isloadfra = true;
		}
	}

	public virtual void paintHotkey(mGraphics g, int x, int y, int w, int yLech)
	{
		paintAllItem(g, x, y, w, 0, 5);
	}

	public virtual MainImage getImage()
	{
		return getImageAll();
	}

	public static void removeUpdateItemVec(sbyte type, mVector vec)
	{
		for (int i = 0; i < vec.size(); i++)
		{
			MainItem mainItem = (MainItem)vec.elementAt(i);
			if (mainItem.typeObject == type)
			{
				vec.removeElement(mainItem);
				i--;
			}
		}
	}

	public static MainItem getItemVec(sbyte type, short id, mVector vec)
	{
		for (int i = 0; i < vec.size(); i++)
		{
			MainItem mainItem = (MainItem)vec.elementAt(i);
			if (mainItem.typeObject == type && mainItem.ID == id)
			{
				return mainItem;
			}
		}
		return null;
	}

	public virtual void Use_Item()
	{
	}

	public virtual mVector getActionInven(sbyte type)
	{
		if (typeObject == 110)
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
		return null;
	}

	public virtual mVector getActionShop(sbyte typeShop)
	{
		mVector obj = new mVector();
		obj.addElement(TabShop.cmdBuyPotion);
		return obj;
	}

	public virtual mVector getActionChest()
	{
		return null;
	}

	public virtual mVector getActionUpgrade()
	{
		return null;
	}

	public virtual mVector getActionSplit()
	{
		return null;
	}

	public mVector getActionTrade()
	{
		mVector mVector2 = new mVector();
		if (TradeScreen.instance != null)
		{
			mVector2.addElement(TradeScreen.instance.cmdBovao);
		}
		return mVector2;
	}

	public static string getInfoEveryWhere(MainInfoItem info)
	{
		if (info == null || info.id >= mNameAttributes.Length)
		{
			return "null";
		}
		return mNameAttributes[info.id].name + " " + strGetPercent(info.value, mNameAttributes[info.id].ispercent);
	}

	public static string getTimeDelay(int value)
	{
		return value / 1000 + "," + value % 1000 / 100 + "s";
	}

	public static void LoadNameAttribute(DataInputStream iss, bool isSave)
	{
		if (iss == null)
		{
			GlobalService.gI().get_DATA(2);
			return;
		}
		try
		{
			short num = iss.readShort();
			mNameAttributes = new MainInfoItem[num];
			for (int i = 0; i < num; i++)
			{
				mNameAttributes[i] = new MainInfoItem(iss.readUTF(), iss.readByte(), iss.readByte());
			}
			if (isSave)
			{
				GlobalService.VerNameAtribute = iss.readShort();
				SaveRms.saveVer(GlobalService.VerNameAtribute, "VerdataAttri");
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}

	public static mVector SortVecItem(mVector vec)
	{
		int num = vec.size();
		int num2;
		for (int i = 0; i < num - 1; i++)
		{
			num2 = i;
			for (int j = i + 1; j < num; j++)
			{
				if (((MainItem)vec.elementAt(j)).indexSort < ((MainItem)vec.elementAt(num2)).indexSort)
				{
					num2 = j;
				}
			}
			if (num2 != i)
			{
				swapItem(vec, i, num2);
			}
		}
		num2 = 0;
		for (int k = 0; k < num - 1; k++)
		{
			num2 = k;
			for (int l = k + 1; l < num; l++)
			{
				if (((MainItem)vec.elementAt(l)).typeObject == 4 && ((MainItem)vec.elementAt(l)).ID < ((MainItem)vec.elementAt(num2)).ID)
				{
					num2 = l;
				}
			}
			if (num2 != k)
			{
				swapItem(vec, k, num2);
			}
		}
		return vec;
	}

	private static void swapItem(mVector actors, int dex1, int dex2)
	{
		object obj = actors.elementAt(dex2);
		actors.setElementAt(actors.elementAt(dex1), dex2);
		actors.setElementAt(obj, dex1);
	}

	public static mVector getInfoSS(MainItem itemSet)
	{
		mVector mVector2 = new mVector();
		if (itemSet == null || (itemSet.charClass != GameScreen.player.clazz && itemSet.charClass > 0) || itemSet.typeObject != 3)
		{
			return null;
		}
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + itemSet.typeEquip);
		if (mainItem != null)
		{
			for (int i = 0; i < itemSet.vecInfo.size(); i++)
			{
				infoShow infoShow2 = (infoShow)itemSet.vecInfo.elementAt(i);
				bool flag = false;
				if (infoShow2.id >= 0 && infoShow2.colorMain == infoShow.HARDCODE_INFO_CO_BAN)
				{
					for (int j = 0; j < mainItem.vecInfo.size(); j++)
					{
						infoShow infoShow3 = (infoShow)mainItem.vecInfo.elementAt(j);
						if (infoShow2.id == infoShow3.id)
						{
							int num = infoShow2.value - infoShow3.value;
							sbyte color = 6;
							string str = strGetPercent(num, mNameAttributes[infoShow2.id].ispercent);
							if (num >= 0)
							{
								str = "+" + strGetPercent(num, mNameAttributes[infoShow2.id].ispercent);
								color = 1;
							}
							mVector2.addElement(new infoShow(-1, num, str, color, -1));
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					mVector2.addElement(new infoShow(-1, 0, string.Empty, 0, -1));
				}
			}
		}
		if (mVector2.size() == 0)
		{
			mVector2 = null;
		}
		return mVector2;
	}

	public static mVector getInfoSS(MainItem itemSet, int Plus)
	{
		mVector mVector2 = new mVector();
		if (itemSet == null || (itemSet.charClass != GameScreen.player.clazz && itemSet.charClass > 0))
		{
			return null;
		}
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + itemSet.typeEquip);
		if (mainItem != null)
		{
			for (int i = 0; i < itemSet.vecInfo.size(); i++)
			{
				infoShow infoShow2 = (infoShow)itemSet.vecInfo.elementAt(i);
				bool flag = false;
				if (infoShow2.id >= 0 && infoShow2.colorMain == infoShow.HARDCODE_INFO_CO_BAN)
				{
					for (int j = 0; j < mainItem.vecInfo.size(); j++)
					{
						infoShow infoShow3 = (infoShow)mainItem.vecInfo.elementAt(j);
						if (infoShow2.id == infoShow3.id)
						{
							int num = valueSameUpgrade(infoShow2.value, itemSet.LvUpgrade, Plus) - infoShow3.value;
							sbyte color = 6;
							string str = strGetPercent(num, mNameAttributes[infoShow2.id].ispercent);
							if (num >= 0)
							{
								str = "+" + strGetPercent(num, mNameAttributes[infoShow2.id].ispercent);
								color = 1;
							}
							mVector2.addElement(new infoShow(-1, num, str, color, -1));
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					mVector2.addElement(new infoShow(-1, 0, string.Empty, 0, -1));
				}
			}
		}
		if (mVector2.size() == 0)
		{
			mVector2 = null;
		}
		return mVector2;
	}

	public static int valueSameUpgrade(int value, int cur, int up)
	{
		if (up == cur)
		{
			return value;
		}
		if (cur < 0)
		{
			cur = 0;
		}
		if (cur >= mValueUpgrade.Length)
		{
			cur = mValueUpgrade.Length - 1;
		}
		if (up < 0)
		{
			up = 0;
		}
		if (up >= mValueUpgrade.Length)
		{
			up = mValueUpgrade.Length - 1;
		}
		int num = value * 100 / (mValueUpgrade[cur] + 100);
		return num + num * mValueUpgrade[up] / 100;
	}

	public virtual bool getStar()
	{
		return false;
	}

	public virtual void setTimeMarket(int time)
	{
		timeUse = time;
		marketTime.setCountDown(time);
	}

	public void CheckTimeSell()
	{
		if (typeMarket == 1 && marketTime.timeCountDown <= 0)
		{
			typeMarket = 3;
		}
	}

	public static string getDataKichAn(Item item)
	{
		string empty = string.Empty;
		empty = (string)hashAttriKichAn.get(string.Empty + item.valueKickAn);
		if (empty == null)
		{
			hashAttriKichAn.put(string.Empty + item.valueKickAn, string.Empty);
			GlobalService.gI().GetTemplate(96, item.valueKickAn);
			empty = string.Empty;
		}
		if (empty.Length == 0)
		{
			Item.vecItemKichAnCheckInfo.addElement(item);
		}
		return empty;
	}

	public short getIdMarket()
	{
		return IDMarket;
	}

	public bool getInfoPotion(short index)
	{
		if (info == null || info.Length == 0)
		{
			indexInfoPotion = index;
			info = GetInfoPotion(indexInfoPotion);
			if (info.Length == 0)
			{
				return false;
			}
		}
		setInfoPotion(info);
		return true;
	}

	public static string GetInfoPotion(short index)
	{
		string text = (string)Potion.hashInfoPotion.get(string.Empty + index);
		if (text == null)
		{
			Potion.hashInfoPotion.put(string.Empty + index, string.Empty);
			GlobalService.gI().getDataInfoPotion(index);
			return string.Empty;
		}
		return text;
	}
}
