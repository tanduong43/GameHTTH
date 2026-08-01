using System;

public class TabInventory : MainTabShop
{
	private sbyte typeInventory;

	public iCommand cmdUsePotion;

	public iCommand cmdSetHotKey;

	public iCommand cmdUseItem;

	public iCommand cmdRemoveItem;

	public iCommand cmdSellItem;

	public iCommand cmdSetChestItem;

	public iCommand cmdSetChestPotion;

	public iCommand cmdSellAll;

	public iCommand cmdUpgradeItem;

	public iCommand cmdAutoMaterial;

	public iCommand cmdAllMaterial;

	public iCommand cmdChucnang;

	public iCommand cmdMenuMaterial;

	public iCommand cmdMenuSellShop;

	public iCommand cmdSellMarket;

	public iCommand cmdAllDiamond;

	public iCommand cmdSellBeri;

	public iCommand cmdDonotUse;

	public static iCommand cmdSellWhite;

	public static iCommand cmdSell_W_G;

	public static iCommand cmdSellMore;

	public static short priceItemSell;

	public static short maxPriceItemSell;

	public static short pricePotionSell;

	public static short numSell = 1;

	public static mVector vecsell = new mVector("TabInventory.vecsell");

	public static int LvUpgradeSS = 0;

	public TabInventory(string name, mVector vec, sbyte typeInventory, int xTab)
		: base(name, vec, Player.maxInventory, xTab)
	{
		this.typeInventory = typeInventory;
		indexIconTab = 0;
	}

	public void setTypeInven(sbyte type)
	{
		typeInventory = type;
	}

	public void initCmd()
	{
		mSystem.outz("init tab type " + typeInventory);
		if (typeInventory == 7)
		{
			cmdUsePotion = new iCommand(T.cmdUse, 34, this);
			cmdDonotUse = new iCommand(T.thaora, 35, this);
			cmdMenu = new iCommand(T.select, 10, this);
			return;
		}
		if (typeInventory == 6)
		{
			cmdUsePotion = new iCommand(T.cmdUse, 32, this);
			cmdDonotUse = new iCommand(T.thaora, 33, this);
			cmdMenu = new iCommand(T.select, 10, this);
			return;
		}
		if (typeInventory == 4)
		{
			cmdUsePotion = new iCommand(T.cmdUse, 0, this);
			cmdMenu = new iCommand(T.select, 10, this);
			return;
		}
		if (typeInventory == 5)
		{
			cmdSellMarket = new iCommand(T.sell, 27, this);
			cmdMenu = new iCommand(T.select, 10, this);
			cmdSellBeri = new iCommand(T.sellBeri, 29, this);
			return;
		}
		cmdUsePotion = new iCommand(T.cmdUse, 0, this);
		cmdSetHotKey = new iCommand(T.cmdHotKey, 1, this);
		cmdUseItem = new iCommand(T.cmdUse, 7, this);
		cmdSellItem = new iCommand(T.sell, 26, 0, this);
		cmdRemoveItem = new iCommand(T.remove, 26, 1, this);
		cmdSellAll = new iCommand(T.sellAll, 17, this);
		cmdSetChestItem = new iCommand(T.cmdset, 11, 0, this);
		cmdSetChestPotion = new iCommand(T.cmdset, 21, 0, this);
		cmdMenu = new iCommand(T.select, 10, this);
		cmdUpgradeItem = new iCommand(T.Upgrade, 18, this);
		cmdAutoMaterial = new iCommand(T.autoMaterial, 19, this);
		cmdAllMaterial = new iCommand(T.cattatca, 28, 0, this);
		cmdAllDiamond = new iCommand(T.cattatcaDa, 28, 1, this);
		cmdChucnang = new iCommand(T.cmdChucNang, 23, this);
		cmdMenuMaterial = new iCommand(T.select, 24, this);
		cmdMenuSellShop = new iCommand(T.select, 25, this);
		cmdSellWhite = new iCommand(T.itemWhite, 14, this);
		cmdSell_W_G = new iCommand(T.itemWhiteGreen, 15, this);
		cmdSellMore = new iCommand(T.sell, 16, this);
		cmdPlusItem = new iCommand(T.plus, 31, this);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (itemCur != null && (itemCur.typeObject == 4 || itemCur.typeObject == 8))
			{
				itemCur.Use_Item();
			}
			break;
		case 1:
			getMenuHotKey();
			break;
		case 2:
			if (itemCur != null && itemCur.typeObject == 4)
			{
				Player.setHotKey(subIndex, null, itemCur);
				Interface_Game.timePaintIconSkill = 100;
			}
			break;
		case 7:
			if (itemCur != null)
			{
				GlobalService.gI().Use_Item(itemCur.ID, itemCur.typeObject);
			}
			break;
		case 8:
			if (itemCur == null)
			{
				break;
			}
			numSell = 1;
			if (itemCur.typeObject != 3 && input != null)
			{
				try
				{
					numSell = (short)int.Parse(input.tfInput.getText());
					if (numSell < 0)
					{
						numSell = 1;
					}
				}
				catch (Exception)
				{
					numSell = 1;
				}
				if (numSell > itemCur.numPotion)
				{
					numSell = itemCur.numPotion;
				}
			}
			if (subIndex == 0)
			{
				int num3 = 0;
				if (itemCur.typeObject == 3)
				{
					num3 = 30 + (itemCur.colorName * 2 + itemCur.Lv_RQ / 10 + 1) * priceItemSell;
					if (num3 > maxPriceItemSell)
					{
						num3 = maxPriceItemSell;
					}
				}
				else
				{
					num3 = numSell * pricePotionSell;
				}
				string info = GameMidlet.format(T.HoiSellENG, new string[4]
				{
					numSell + string.Empty,
					itemCur.name,
					num3 + string.Empty,
					T.bery
				});
				GameCanvas.Start_Normal_DiaLog(info, new iCommand(T.sell, 9, 0, this), isCmdClose: true);
			}
			else
			{
				string info2 = GameMidlet.format(T.HoiRemoveENG, new string[2]
				{
					string.Empty + numSell,
					itemCur.name
				});
				GameCanvas.Start_Normal_DiaLog(info2, new iCommand(T.remove, 9, 1, this), isCmdClose: true);
			}
			break;
		case 9:
			if (itemCur != null)
			{
				GlobalService.gI().Sell_Item((sbyte)subIndex, itemCur.ID, itemCur.typeObject, numSell);
				GameCanvas.end_Dialog();
			}
			break;
		case 10:
		{
			mVector menuActionItem = getMenuActionItem();
			if (menuActionItem != null)
			{
				GameCanvas.menu.startAt(menuActionItem, 2, T.menu);
			}
			break;
		}
		case 11:
			if (itemCur != null)
			{
				GlobalService.gI().Chest(1, itemCur.ID, itemCur.typeObject, 1);
			}
			break;
		case 12:
			GameCanvas.end_Dialog();
			if (itemCur != null)
			{
				if (itemCur.numPotion == 1)
				{
					GlobalService.gI().Chest(1, itemCur.ID, itemCur.typeObject, 1);
				}
				else
				{
					input = GameCanvas.Start_Input_Dialog(T.nhapsoluong, new iCommand(T.cmdset, 13, 0, this), isNum: true, itemCur.namepaint);
				}
				GameCanvas.subDialog = input;
			}
			break;
		case 13:
		{
			if (itemCur == null)
			{
				break;
			}
			int num2 = 1;
			try
			{
				num2 = int.Parse(input.tfInput.getText());
				if (num2 < 0)
				{
					num2 = 1;
				}
			}
			catch (Exception)
			{
				num2 = 1;
			}
			MainItem itemVec = MainItem.getItemVec(itemCur.typeObject, itemCur.ID, Player.vecInventory);
			if (itemVec != null)
			{
				if (num2 > itemVec.numPotion)
				{
					num2 = itemVec.numPotion;
				}
				GameCanvas.end_Dialog();
				GlobalService.gI().Chest(1, itemCur.ID, itemCur.typeObject, num2);
			}
			break;
		}
		case 14:
			Sell_W(vecShop);
			break;
		case 15:
			Sell_W_G(vecShop);
			break;
		case 16:
			SellMore();
			break;
		case 17:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(cmdSellWhite);
			mVector2.addElement(cmdSell_W_G);
			GameCanvas.menu.startAt(mVector2, 2, T.sellAll);
			break;
		}
		case 18:
			if (itemCur != null)
			{
				GlobalService.gI().Upgrade_Item(1, itemCur.ID, 0);
			}
			break;
		case 19:
		{
			mVector mVector4 = new mVector();
			iCommand o3 = new iCommand(T.cmdNguyenlieu, 20, 1, this);
			mVector4.addElement(o3);
			iCommand o4 = new iCommand(T.cmdDaKham, 20, 2, this);
			mVector4.addElement(o4);
			iCommand o5 = new iCommand(T.cmdCaHai, 20, 3, this);
			mVector4.addElement(o5);
			iCommand o6 = new iCommand(T.off, 20, 4, this);
			mVector4.addElement(o6);
			GameCanvas.Start_Normal_DiaLog(T.hoiAutoMaterial, mVector4, isCmdClose: false);
			break;
		}
		case 20:
			if (subIndex == 4)
			{
				Player.isAutoMaterial = 0;
				GameCanvas.saveRms.SaveAutoMaterial();
				GameCanvas.end_Dialog();
				break;
			}
			Player.isAutoMaterial = (sbyte)subIndex;
			GameCanvas.saveRms.SaveAutoMaterial();
			if (Player.isAutoMaterial == 1 || Player.isAutoMaterial == 3)
			{
				Player.SetMaterialToChest(7);
			}
			if (Player.isAutoMaterial == 2 || Player.isAutoMaterial == 3)
			{
				Player.SetDiamondToChest();
			}
			GameCanvas.end_Dialog();
			break;
		case 21:
			if (itemCur != null)
			{
				if (itemCur.numPotion == 1)
				{
					GlobalService.gI().Chest(1, itemCur.ID, itemCur.typeObject, 1);
					break;
				}
				mVector mVector3 = new mVector();
				iCommand o = new iCommand(T.allMaterial, 22, 0, this);
				mVector3.addElement(o);
				iCommand o2 = new iCommand(T.soluong, 12, 0, this);
				mVector3.addElement(o2);
				GameCanvas.Start_Normal_DiaLog_New(T.muoncatbaonhieu + itemCur.namepaint, mVector3, isCmdClose: true, itemCur.name);
			}
			break;
		case 22:
			if (itemCur != null)
			{
				if (subIndex == 0)
				{
					GlobalService.gI().Chest(1, itemCur.ID, itemCur.typeObject, itemCur.numPotion);
				}
				GameCanvas.end_Dialog();
			}
			break;
		case 23:
		{
			mVector mVector7 = new mVector();
			mVector7.addElement(cmdAllMaterial);
			mVector7.addElement(cmdAllDiamond);
			mVector7.addElement(cmdAutoMaterial);
			GameCanvas.menu.startAt(mVector7, 2, T.menu);
			break;
		}
		case 24:
		{
			mVector mVector6 = new mVector();
			mVector6.addElement(cmdRemoveItem);
			GameCanvas.menu.startAt(mVector6, 2, T.menu);
			break;
		}
		case 25:
		{
			mVector mVector5 = new mVector();
			mVector5.addElement(cmdSellItem);
			GameCanvas.menu.startAt(mVector5, 2, T.menu);
			break;
		}
		case 26:
			if (itemCur.typeObject == 3)
			{
				commandPointer(8, subIndex);
				break;
			}
			if (subIndex == 0)
			{
				input = GameCanvas.Start_Input_Dialog(T.nhapsoluong, new iCommand(T.sell, 8, 0, this), isNum: true, itemCur.namepaint);
			}
			else
			{
				input = GameCanvas.Start_Input_Dialog(T.nhapsoluong, new iCommand(T.remove, 8, 1, this), isNum: true, itemCur.namepaint);
			}
			GameCanvas.subDialog = input;
			break;
		case 27:
			if (itemCur != null)
			{
				if (itemCur.typeObject == 3)
				{
					GlobalService.gI().Market(10, indexMarket, itemCur.ID, itemCur.typeObject, 1);
					break;
				}
				input = GameCanvas.Start_Input_Dialog(T.nhapsoluong, new iCommand(T.sell, 30, 1, this), isNum: true, itemCur.namepaint);
				GameCanvas.subDialog = input;
			}
			break;
		case 28:
			if (subIndex == 0)
			{
				Player.SetMaterialToChest(7);
			}
			if (subIndex == 1)
			{
				Player.SetDiamondToChest();
			}
			break;
		case 29:
			input = GameCanvas.Start_Input_Dialog(T.nhapsoluongBeri, new iCommand(T.sell, 30, 0, this), isNum: true, T.bery);
			GameCanvas.subDialog = input;
			break;
		case 30:
		{
			int num = 1;
			try
			{
				num = int.Parse(input.tfInput.getText());
				if (num < 0)
				{
					num = 1;
				}
			}
			catch (Exception)
			{
				num = 1;
			}
			if (subIndex == 0)
			{
				GlobalService.gI().Market(10, indexMarket, 0, 4, (short)num);
			}
			else if (itemCur != null)
			{
				GlobalService.gI().Market(10, indexMarket, itemCur.ID, itemCur.typeObject, (short)num);
			}
			break;
		}
		case 31:
			if (GameCanvas.isSSItem == 0)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.huongdanss);
				try
				{
					CRes.saveRMS("SUB_SSITEM", new sbyte[1] { 1 });
				}
				catch (Exception)
				{
				}
				GameCanvas.isSSItem = 1;
			}
			if (itemCur != null)
			{
				MainItem mainItem5 = (MainItem)GameScreen.player.hashEquip.get(string.Empty + itemCur.typeEquip);
				switch (subIndex)
				{
				case 0:
					LvUpgradeSS = mainItem5.LvUpgrade;
					vecInfoSS = MainItem.getInfoSS(itemCur, LvUpgradeSS);
					cmdPlusItem.caption = "  +" + itemCur.LvUpgrade;
					cmdPlusItem.subIndex = 1;
					break;
				case 1:
					LvUpgradeSS = 0;
					vecInfoSS = MainItem.getInfoSS(itemCur);
					cmdPlusItem.caption = "  +" + mainItem5.LvUpgrade;
					cmdPlusItem.subIndex = 0;
					break;
				}
			}
			break;
		case 32:
		{
			MainItem mainItem4 = (MainItem)vecShop.elementAt(IdSelect);
			GlobalService.gI().Huy_hieu(2, 1, mainItem4.idIcon);
			break;
		}
		case 33:
		{
			MainItem mainItem3 = (MainItem)vecShop.elementAt(IdSelect);
			GlobalService.gI().Huy_hieu(2, 0, mainItem3.idIcon);
			break;
		}
		case 34:
		{
			MainItem mainItem2 = (MainItem)vecShop.elementAt(IdSelect);
			GlobalService.gI().Send_Pet(4, 1, mainItem2.ID);
			break;
		}
		case 35:
		{
			MainItem mainItem = (MainItem)vecShop.elementAt(IdSelect);
			GlobalService.gI().Send_Pet(4, 0, mainItem.ID);
			break;
		}
		case 3:
		case 4:
		case 5:
		case 6:
			break;
		}
	}

	public static void SellMore()
	{
		for (int i = 0; i < vecsell.size(); i++)
		{
			MainItem mainItem = (MainItem)vecsell.elementAt(i);
			GlobalService.gI().Sell_Item(0, mainItem.ID, mainItem.typeObject, 1);
		}
		GameCanvas.end_Dialog();
	}

	public static void Sell_W(mVector vec)
	{
		vecsell.removeAllElements();
		for (int i = 0; i < vec.size(); i++)
		{
			MainItem mainItem = (MainItem)vec.elementAt(i);
			if (mainItem.typeObject == 3 && mainItem.colorName == 0 && mainItem.typeEquip >= 0 && mainItem.typeEquip <= 5)
			{
				vecsell.addElement(mainItem);
			}
		}
		if (vecsell.size() > 0)
		{
			GameCanvas.Start_Normal_DiaLog(T.muonban + vecsell.size() + " " + T.vatpham + " " + T.itemWhite + "?", cmdSellMore, isCmdClose: true);
		}
		else
		{
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.noItemWhite);
		}
	}

	public static void Sell_W_G(mVector vec)
	{
		vecsell.removeAllElements();
		for (int i = 0; i < vec.size(); i++)
		{
			MainItem mainItem = (MainItem)vec.elementAt(i);
			if (mainItem.typeObject == 3 && (mainItem.colorName == 0 || mainItem.colorName == 1) && mainItem.typeEquip >= 0 && mainItem.typeEquip <= 5)
			{
				vecsell.addElement(mainItem);
			}
		}
		if (vecsell.size() > 0)
		{
			GameCanvas.Start_Normal_DiaLog(T.muonban + vecsell.size() + " " + T.vatpham + " " + T.itemWhiteGreen + "?", cmdSellMore, isCmdClose: true);
		}
		else
		{
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.noItemWhiteGreen);
		}
	}

	private void getMenuHotKey()
	{
		mVector mVector2 = new mVector();
		for (int i = 3; i < 5; i++)
		{
			iCommand iCommand2 = null;
			iCommand2 = ((!GameCanvas.isTouch) ? ((!TField.isQwerty) ? new iCommand(T.keys + " " + (i * 2 + 1), 2, i, this) : new iCommand(T.keys + " " + T.mKeyQty[i], 2, i, this)) : new iCommand(T.keys + " " + (i + 1), 2, i, this));
			mVector2.addElement(iCommand2);
		}
		GameCanvas.menu.startAt(mVector2, 2, T.cmdHotKey);
	}

	public override mVector getMenuActionItem()
	{
		mVector mVector2 = null;
		MainItem mainItem = (MainItem)vecShop.elementAt(IdSelect);
		if (mainItem != null)
		{
			itemCur = mainItem;
			mVector2 = itemCur.getActionInven(typeInventory);
			if (typeInventory == 0)
			{
				vecInfoSS = MainItem.getInfoSS(itemCur);
			}
			if (typeInventory == 5)
			{
				if (mVector2 == null)
				{
					mVector2 = new mVector();
				}
				mVector2.addElement(cmdSellBeri);
			}
		}
		return mVector2;
	}

	public override void addButtonPlus()
	{
		LvUpgradeSS = 0;
		if (itemCur == null || itemCur.typeObject != 3 || (itemCur.charClass != GameScreen.player.clazz && itemCur.charClass > 0))
		{
			if (cmdPlusItem != null)
			{
				cmdPlusItem.setPos(-50, -50, AvMain.fraPlus, string.Empty);
			}
			return;
		}
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + itemCur.typeEquip);
		if (itemCur != null && mainItem != null && itemCur.LvUpgrade != mainItem.LvUpgrade)
		{
			cmdPlusItem = new iCommand(T.plus, 31, 0, this);
			int num = itemCur.hInfo - itemCur.hRunInfo;
			cmdPlusItem.setPos(xInfo + itemCur.wInfo - 10, yInfo + num - 10, AvMain.fraPlus, "  +" + mainItem.LvUpgrade);
		}
		else if (cmdPlusItem != null)
		{
			cmdPlusItem.setPos(-50, -50, AvMain.fraPlus, string.Empty);
		}
	}

	public override void resize(int max)
	{
		maxSize = max;
		int limX = ((maxSize - 1) / MainTabShop.maxNumItemW + 1) * MainTab.wItem - hCur + miniItem;
		list = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrShop.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
	}

	public void Use(short idIcon)
	{
		mSystem.outz("Use " + idIcon + " size Shop " + vecShop.size());
		for (int i = 0; i < vecShop.size(); i++)
		{
			MainItem mainItem = (MainItem)vecShop.elementAt(i);
			if (typeInventory == 7)
			{
				if (mainItem.ID == idIcon)
				{
					IdSelect = i;
					mainItem.colorName = 1;
				}
				else
				{
					mainItem.colorName = 0;
				}
			}
			else if (mainItem.idIcon == idIcon)
			{
				IdSelect = i;
				mainItem.colorName = 1;
			}
			else
			{
				mainItem.colorName = 0;
			}
		}
		isShowInfo = false;
		setPosCmd(getMenuActionItem());
	}
}
