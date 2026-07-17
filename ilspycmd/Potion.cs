using System;

public class Potion : MainItem
{
	public static mVector vecPotionDonotData = new mVector("Potion.vecPotionDonotData");

	public static mVector vecPotionDonotInfo = new mVector("Potion.vecPotionDonotInfo");

	public const sbyte POTION_OTHER = 0;

	public const sbyte POTION_HP = 1;

	public const sbyte POTION_MP = 2;

	public const sbyte POTION_RUONG = 3;

	public const sbyte POTION_RUONG_THAN_BI = 4;

	public const sbyte POTION_DA_KHAM = 12;

	public const sbyte POTION_HP_MAXLV_PVP_NEW = 96;

	public const sbyte POTION_MP_PVP_NEW = 97;

	public const sbyte POTION_HP_PVP_NEW = 98;

	public const sbyte POTION_EFF_HP = 99;

	public const sbyte POTION_EFF_HP_2 = 100;

	public const sbyte POTION_EFF_HP_MAX = 101;

	public const sbyte POTION_HP_FOCUS = 102;

	public const sbyte POTION_ATT_CLAN = 103;

	public const sbyte POTION_HP_FOCUS_LOW = 104;

	public const sbyte POTION_THONG_THAO = 105;

	public const sbyte POTION_HP_FOCUS_MAX_LEVEL = 106;

	public const sbyte POTION_POKE = 40;

	public const sbyte POTION_THA_DEN = 84;

	public const sbyte POTION_DOT_PHAO = 100;

	public short timeActive;

	public bool isUpdateTem;

	public static MyHashTable hashInfoPotion = new MyHashTable();

	public Potion(short Id, short idImage, string name, string info, short priceRuby)
	{
		ID = Id;
		idIcon = idImage;
		base.name = name;
		namepaint = name;
		base.info = info;
		base.priceRuby = priceRuby;
		typeObject = 4;
		isShop = true;
		setInfoPotion(info);
		isIconClan = true;
		if (priceRuby == 0)
		{
			nameUse = T.select;
		}
		else
		{
			nameUse = T.cmdBuy;
		}
	}

	public Potion(short Id)
	{
		ID = Id;
	}

	public Potion(sbyte cat, short Id, short num, bool isShop)
	{
		typeObject = cat;
		ID = Id;
		numPotion = num;
		base.isShop = isShop;
	}

	public Potion(sbyte typeItem, short ID, short idIcon, string name, sbyte isTrade)
		: base(typeItem, ID, idIcon, name, isTrade)
	{
		indexSort = 0;
	}

	public void setdata(int price, short priceRuby, short value, short timeActive, short timedelay, sbyte hp_mp, string nameUse)
	{
		base.price = price;
		base.priceRuby = priceRuby;
		base.value = value;
		this.timeActive = timeActive;
		timeDelayPotion = timedelay;
		Hp_Mp_Other = hp_mp;
		base.nameUse = nameUse;
		indexHotKey = (short)(500 + Hp_Mp_Other);
		if (LvUpgrade > 0)
		{
			namepaint = name + " +" + LvUpgrade;
		}
		else
		{
			namepaint = name;
		}
	}

	public static Potion Potion_Tem(sbyte typeItem, short ID, short idIcon, string name, string info, sbyte isTrade, short price, short priceRuby, sbyte Hp_Mp_Other, short timeDelay, short value, short timeactive, string nameUse)
	{
		Potion potion = new Potion(typeItem, ID, idIcon, name, isTrade);
		potion.info = info;
		potion.price = price;
		potion.priceRuby = priceRuby;
		potion.Hp_Mp_Other = Hp_Mp_Other;
		potion.timeDelayPotion = timeDelay;
		potion.timeActive = timeactive;
		potion.value = value;
		potion.nameUse = nameUse;
		potion.isUpdateTem = true;
		return potion;
	}

	public static Potion Update_Potion_Tem(sbyte typeItem, short ID, short idIcon, string name, string info, sbyte isTrade, int price, short priceRuby, sbyte Hp_Mp_Other, short timeDelay, short value, short timeactive, string nameUse)
	{
		Potion potion = new Potion(typeItem, ID, idIcon, name, isTrade);
		potion.info = info;
		potion.price = price;
		potion.priceRuby = priceRuby;
		potion.Hp_Mp_Other = Hp_Mp_Other;
		potion.timeDelayPotion = timeDelay;
		potion.timeActive = timeactive;
		potion.value = value;
		potion.nameUse = nameUse;
		potion.isUpdateTem = true;
		return potion;
	}

	public override mVector getActionInven(sbyte type)
	{
		mVector mVector2 = new mVector();
		if (type == 0)
		{
			if (nameUse.CompareTo("null") != 0)
			{
				GameCanvas.tabInven.cmdUsePotion.caption = nameUse;
				mVector2.addElement(GameCanvas.tabInven.cmdUsePotion);
			}
			if (Hp_Mp_Other == 1 || Hp_Mp_Other == 2 || Hp_Mp_Other == 40 || Hp_Mp_Other == 84 || Hp_Mp_Other == 100)
			{
				mVector2.addElement(GameCanvas.tabInven.cmdSetHotKey);
			}
			if (Hp_Mp_Other == 12)
			{
				mVector2.addElement(GameCanvas.tabInven.cmdMenuMaterial);
			}
			else
			{
				mVector2.addElement(GameCanvas.tabInven.cmdRemoveItem);
			}
		}
		else if (type == 1)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdMenuSellShop);
		}
		else if (type == 2)
		{
			mVector2.addElement(GameCanvas.tabInven.cmdSetChestPotion);
			if (ID >= 44 && ID <= 79)
			{
				mVector2.addElement(GameCanvas.tabInven.cmdChucnang);
			}
		}
		else if (type == 4)
		{
			mVector2.addElement(GameCanvas.tabInvenClan.cmdUsePotion);
		}
		else if (type == 5 && ((ID >= 310 && ID <= 315) || ID == 339 || canSell()))
		{
			mVector2.addElement(GameCanvas.tabInvenMarket.cmdSellMarket);
		}
		return mVector2;
	}

	public bool canSell()
	{
		if (MainItem.ID_POTION_CAN_SELL != null)
		{
			for (int i = 0; i < MainItem.ID_POTION_CAN_SELL.Length; i++)
			{
				if (ID == MainItem.ID_POTION_CAN_SELL[i])
				{
					return true;
				}
			}
		}
		return false;
	}

	public override mVector getActionShop(sbyte typeShop)
	{
		mVector mVector2 = new mVector();
		if (typeShop == 101)
		{
			mVector2.addElement(TabShop.cmdChangeShip);
			mVector2.addElement(TabShop.cmdShip);
		}
		else if (isIconClan)
		{
			mVector2.addElement(TabShop.cmdBuyIconClan);
		}
		else
		{
			mVector2.addElement(TabShop.cmdBuyPotion);
		}
		return mVector2;
	}

	public override mVector getActionChest()
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(TabChest.cmdGetPotion);
		if (ID >= 44 && ID <= 79)
		{
			mVector2.addElement(TabChest.cmdChucnang);
		}
		else
		{
			mVector2.addElement(TabChest.cmdUpgrade);
		}
		return mVector2;
	}

	public override mVector getActionSplit()
	{
		mVector mVector2 = new mVector();
		if (SplitScreen.instance != null && SplitScreen.instance.cmdBovao != null && (SplitScreen.instance.typeRebuild == 1 || SplitScreen.instance.typeRebuild == 4 || SplitScreen.instance.typeRebuild == 8 || SplitScreen.instance.typeRebuild == 10 || SplitScreen.instance.typeRebuild == 11 || SplitScreen.instance.typeRebuild == 13 || SplitScreen.instance.typeRebuild == 14 || (SplitScreen.instance.typeRebuild == 2 && (ID == 323 || ID == 339)) || (SplitScreen.instance.typeRebuild == 19 && ID == 457) || SplitScreen.instance.typeRebuild == 21))
		{
			mVector2.addElement(SplitScreen.instance.cmdBovao);
		}
		return mVector2;
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		if (isIconClan)
		{
			MainImage iconClan = getIconClan(idIcon);
			if (iconClan != null && iconClan.img != null)
			{
				paintImgItem(g, iconClan, x, y);
			}
			else
			{
				AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
			}
			return;
		}
		base.paint(g, x, y, w);
		paintPotion(g, x, y, w);
		if ((idIcon >= 210 && idIcon <= 239) || (idIcon >= 319 && idIcon <= 324))
		{
			AvMain.fraEffDasieucap.drawFrame(GameCanvas.gameTickChia4 % AvMain.fraEffDasieucap.nFrame, x, y, 0, 3, g);
		}
	}

	public override void paintQuay(mGraphics g, int x, int y, int w)
	{
		if (isIconClan)
		{
			MainImage iconClan = getIconClan(idIcon);
			if (iconClan != null && iconClan.img != null)
			{
				paintImgItem(g, iconClan, x, y);
			}
			else
			{
				AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
			}
			return;
		}
		base.paint(g, x, y, w);
		paintNumPotionQuay(g, x, y, w, numPotion);
		if ((idIcon >= 210 && idIcon <= 239) || (idIcon >= 319 && idIcon <= 324))
		{
			AvMain.fraEffDasieucap.drawFrame(GameCanvas.gameTickChia4 % AvMain.fraEffDasieucap.nFrame, x, y, 0, 3, g);
		}
	}

	public static MainImage getIconClan(short Id)
	{
		if (Id == -1)
		{
			return null;
		}
		return ObjectData.getImageAll(Id, ObjectData.hashImageIconClan, 7000);
	}

	public static MainImage getIconClanBig(short Id)
	{
		if (Id == -1)
		{
			return null;
		}
		return ObjectData.getImageAll(Id, ObjectData.hashImageIconClanBig, 22000);
	}

	public void paintIconClan(mGraphics g, int x, int y, int w)
	{
		base.paint(g, x, y, w);
		paintPotion(g, x, y, w);
	}

	public static Potion getTemplatePotion(short IdP)
	{
		Potion potion = (Potion)MainItem.hashPotionTem.get(string.Empty + IdP);
		if (potion == null)
		{
			potion = new Potion(IdP);
			MainItem.hashPotionTem.put(string.Empty + IdP, potion);
			GlobalService.gI().GetTemplate(4, IdP);
		}
		return potion;
	}

	public override MainImage getImage()
	{
		MainImage result = null;
		try
		{
			if (idIcon == -1)
			{
				return null;
			}
			result = ObjectData.getImageAll(idIcon, ObjectData.hashImagePotion, 2000);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public override void paintHotkey(mGraphics g, int x, int y, int w, int ylech)
	{
		base.paint(g, x, y, w);
		if (numPotion >= 0)
		{
			g.drawImage(AvMain.imgBgnum, x, y + 18 + ylech, 3);
			mFont.tahoma_7_yellow.drawString(g, string.Empty + numPotion, x, y + 13 + ylech, 2);
		}
	}

	public override void Use_Item()
	{
		if (typeObject == 8)
		{
			GlobalService.gI().Clan_CMD(14, string.Empty, ID, typeObject);
		}
		else if (Hp_Mp_Other == 40 && DelaySkill.getDelay(indexHotKey).isCoolDown())
		{
			short num = GameScreen.checkPokemon();
			if (num == -1)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.noPokemon);
			}
			else
			{
				GlobalService.gI().Use_Poke(ID, num);
			}
			Player.setDelaySkill(indexHotKey, timeDelayPotion, isNotCooldown: true, 0);
		}
		else
		{
			if ((GameScreen.player.Action == 4 && (Hp_Mp_Other == 1 || Hp_Mp_Other == 2)) || !DelaySkill.getDelay(indexHotKey).isCoolDown())
			{
				return;
			}
			if (Hp_Mp_Other == 1 && GameScreen.player.Hp == GameScreen.player.maxHp)
			{
				Interface_Game.addInfoPlayerNormal(T.HPFull, mFont.tahoma_7_white);
				return;
			}
			if (Hp_Mp_Other == 2 && GameScreen.player.Mp == GameScreen.player.maxMp)
			{
				Interface_Game.addInfoPlayerNormal(T.MpFull, mFont.tahoma_7_white);
				return;
			}
			GlobalService.gI().Use_Potion(ID);
			if (Hp_Mp_Other == 3 || Hp_Mp_Other == 4)
			{
				MsgShowGift.gift = this;
			}
			Player.setDelaySkill(indexHotKey, timeDelayPotion, isNotCooldown: true, 0);
		}
	}

	public static void LoadDataPotion(DataInputStream iss, bool isSave, sbyte typePotion)
	{
		if (iss == null)
		{
			if (typePotion == 4)
			{
				GlobalService.gI().get_DATA(25);
			}
			if (typePotion == 8)
			{
				GlobalService.gI().get_DATA(18);
			}
			return;
		}
		try
		{
			short num = iss.readShort();
			for (int i = 0; i < num; i++)
			{
				short num2 = iss.readShort();
				short num3 = iss.readShort();
				string text = iss.readUTF();
				short num4 = -1;
				string text2 = string.Empty;
				if (typePotion == 4)
				{
					num4 = iss.readShort();
				}
				else
				{
					text2 = iss.readUTF();
				}
				short num5 = iss.readShort();
				short num6 = iss.readShort();
				sbyte b = iss.readByte();
				sbyte hp_Mp_Other = iss.readByte();
				short timeDelay = iss.readShort();
				short num7 = iss.readShort();
				short timeactive = iss.readShort();
				string text3 = iss.readUTF();
				Potion potion = Potion_Tem(4, num2, num3, text, text2, b, num5, num6, hp_Mp_Other, timeDelay, num7, timeactive, text3);
				potion.indexInfoPotion = num4;
				if (typePotion == 4)
				{
					MainItem.hashPotionTem.put(string.Empty + num2, potion);
				}
				if (typePotion == 8)
				{
					MainItem.hashPotionClan.put(string.Empty + num2, potion);
				}
			}
			if (isSave)
			{
				if (typePotion == 4)
				{
					GlobalService.VerPotion = iss.readShort();
					SaveRms.saveVer(GlobalService.VerPotion, "VerdataPotion");
				}
				if (typePotion == 8)
				{
					GlobalService.verPotionClan = iss.readShort();
					SaveRms.saveVer(GlobalService.verPotionClan, "VerdataPotionClan");
				}
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}

	public static void UpdateDataPotion(DataInputStream iss, bool isSave, sbyte typePotion)
	{
		if (iss == null)
		{
			if (typePotion == 4)
			{
				GlobalService.gI().get_DATA(28);
			}
			if (typePotion == 8)
			{
				GlobalService.gI().get_DATA(29);
			}
			return;
		}
		try
		{
			short num = iss.readShort();
			for (int i = 0; i < num; i++)
			{
				short num2 = iss.readShort();
				short num3 = iss.readShort();
				string text = iss.readUTF();
				short num4 = -1;
				string text2 = string.Empty;
				if (typePotion == 4)
				{
					num4 = iss.readShort();
				}
				else
				{
					text2 = iss.readUTF();
				}
				int num5 = iss.readInt();
				short num6 = iss.readShort();
				sbyte b = iss.readByte();
				sbyte hp_Mp_Other = iss.readByte();
				short timeDelay = iss.readShort();
				short num7 = iss.readShort();
				short timeactive = iss.readShort();
				string text3 = iss.readUTF();
				Potion potion = Update_Potion_Tem(4, num2, num3, text, text2, b, num5, num6, hp_Mp_Other, timeDelay, num7, timeactive, text3);
				potion.indexInfoPotion = num4;
				if (typePotion == 4)
				{
					MainItem.hashPotionTem.put(string.Empty + num2, potion);
				}
				if (typePotion == 8)
				{
					MainItem.hashPotionClan.put(string.Empty + num2, potion);
				}
			}
			if (isSave)
			{
				if (typePotion == 4)
				{
					GlobalService.VerPotion = iss.readShort();
					SaveRms.saveVer(GlobalService.VerPotion, "VerdataPotion");
				}
				if (typePotion == 8)
				{
					GlobalService.verPotionClan = iss.readShort();
					SaveRms.saveVer(GlobalService.verPotionClan, "VerdataPotionClan");
				}
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}

	public static void LoadDataPotionTemplate(DataInputStream iss, sbyte typePotion)
	{
		try
		{
			short num = iss.readShort();
			short num2 = iss.readShort();
			string text = iss.readUTF();
			short num3 = -1;
			string text2 = string.Empty;
			if (typePotion == 4)
			{
				num3 = iss.readShort();
			}
			else
			{
				text2 = iss.readUTF();
			}
			int num4 = iss.readInt();
			short num5 = iss.readShort();
			sbyte b = iss.readByte();
			sbyte hp_Mp_Other = iss.readByte();
			short timeDelay = iss.readShort();
			short num6 = iss.readShort();
			short timeactive = iss.readShort();
			string text3 = iss.readUTF();
			Potion potion = Update_Potion_Tem(4, num, num2, text, text2, b, num4, num5, hp_Mp_Other, timeDelay, num6, timeactive, text3);
			potion.indexInfoPotion = num3;
			potion.isUpdateTem = true;
			if (typePotion == 4)
			{
				MainItem.hashPotionTem.put(string.Empty + num, potion);
			}
			if (typePotion == 8)
			{
				MainItem.hashPotionClan.put(string.Empty + num, potion);
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}

	public static void CheckAddDataTemplate()
	{
		for (int i = 0; i < vecPotionDonotData.size(); i++)
		{
			Potion potion = (Potion)vecPotionDonotData.elementAt(i);
			Potion potion2 = (Potion)MainItem.hashPotionTem.get(string.Empty + potion.ID);
			if (potion2.isUpdateTem)
			{
				potion.idIcon = potion2.idIcon;
				potion.name = potion2.name;
				potion.isTrade = potion2.isTrade;
				potion.setdata(potion2.price, potion2.priceRuby, potion2.value, potion2.timeActive, potion2.timeDelayPotion, potion2.Hp_Mp_Other, potion2.nameUse);
				if (!potion.getInfoPotion(potion2.indexInfoPotion))
				{
					vecPotionDonotInfo.addElement(potion);
				}
				vecPotionDonotData.removeElementAt(i);
				i--;
			}
		}
	}

	public static void checkVecNonInfo(short index, string infonew)
	{
		for (int i = 0; i < vecPotionDonotInfo.size(); i++)
		{
			Potion potion = (Potion)vecPotionDonotInfo.elementAt(i);
			if (potion.indexInfoPotion == index)
			{
				potion.info = infonew;
				potion.setInfoPotion(infonew);
				vecPotionDonotInfo.removeElement(potion);
				i--;
			}
		}
	}
}
