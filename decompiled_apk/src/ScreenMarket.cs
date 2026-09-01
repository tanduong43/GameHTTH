public class ScreenMarket : MainTab
{
	private mVector vecList;

	private ListNew list;

	private int maxPaint;

	public MainItem itemCur;

	public mVector vecCmd = new mVector();

	public mVector vecInfoSS = new mVector();

	public iCommand cmdBuy;

	public iCommand cmdSelectPage;

	public iCommand cmdClose;

	public iCommand cmdSell;

	public iCommand cmdGet;

	public iCommand cmdUpdate;

	public iCommand cmdMenu;

	public iCommand cmdCancleSell;

	public iCommand cmdPlusItem;

	private InputDialog input;

	private TabScreen ScrTab;

	public const sbyte INDEX_MARKET_WEA = 0;

	public const sbyte INDEX_MARKET_AO = 1;

	public const sbyte INDEX_MARKET_SUC = 2;

	public const sbyte INDEX_MARKET_CHEST = 3;

	public const sbyte INDEX_MARKET_INVEN = 4;

	public const sbyte INDEX_MARKET_NGUYEN_LIEU = 5;

	public const sbyte INDEX_MARKET_POTION = 6;

	public ScreenMarket(string nameTab, mVector vec, TabScreen tab, sbyte indexMarket)
	{
		ScrTab = tab;
		base.nameTab = nameTab;
		vecList = vec;
		base.indexMarket = indexMarket;
		setIconTab();
		MainTab.wItem = 28;
		if (isBigScreen)
		{
			MainTab.wItem = 32;
		}
		wItemCur = 44;
		int num = 0;
		if (vecList != null)
		{
			num = vecList.size();
		}
		int limX = num * wItemCur - hCur + miniItem * 3;
		list = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		xCurBegin = tab.xbeginPaintTab + MainTab.wTab / 2 - wCur / 2 + 10;
		beginFocus();
		setCreateCmd();
	}

	public void setCreateCmd()
	{
		cmdPlusItem = new iCommand(T.plus, 8, this);
		vecCmd.removeAllElements();
		if (vecList == null)
		{
			cmdUpdate = new iCommand(T.update, 2, this);
			AvMain.setPosCMD(cmdUpdate, 0);
			vecCmd.addElement(cmdUpdate);
			if (!GameCanvas.isTouch)
			{
				center = cmdUpdate;
			}
			okCMD = cmdUpdate;
			return;
		}
		if (indexMarket == 3)
		{
			cmdGet = new iCommand(T.cmdGet, 3, this);
			AvMain.setPosCMD(cmdGet, 0);
			cmdSell = new iCommand(T.sell, 6, this);
			AvMain.setPosCMD(cmdSell, 1);
			cmdCancleSell = new iCommand(T.stopSell, 5, this);
			AvMain.setPosCMD(cmdCancleSell, 1);
			return;
		}
		cmdBuy = new iCommand(T.cmdBuy, 0, this);
		AvMain.setPosCMD(cmdBuy, 0);
		cmdSelectPage = new iCommand(T.selectPage, 1, this);
		AvMain.setPosCMD(cmdSelectPage, 1);
		vecCmd.addElement(cmdBuy);
		vecCmd.addElement(cmdSelectPage);
		if (!GameCanvas.isTouch)
		{
			left = cmdSelectPage;
			center = cmdBuy;
		}
		menuCMD = cmdSelectPage;
		okCMD = cmdBuy;
	}

	private void setIconTab()
	{
		if (indexMarket == 0)
		{
			indexIconTab = 9;
		}
		else if (indexMarket == 1)
		{
			indexIconTab = 10;
		}
		else if (indexMarket == 2)
		{
			indexIconTab = 11;
		}
		else if (indexMarket == 3)
		{
			indexIconTab = 7;
		}
		else if (indexMarket == 4)
		{
			indexIconTab = 0;
		}
		else if (indexMarket == 5)
		{
			indexIconTab = 12;
		}
		else if (indexMarket == 6)
		{
			indexIconTab = 13;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (itemCur != null)
			{
				GlobalService.gI().Market(0, indexMarket, itemCur.IDMarket, itemCur.typeObject, 1);
			}
			break;
		case 1:
			GlobalService.gI().Market(8, indexMarket, 0, 0, 1);
			break;
		case 2:
			GlobalService.gI().Market(9, indexMarket, 0, 0, 1);
			break;
		case 3:
			if (itemCur != null)
			{
				GlobalService.gI().Market(5, indexMarket, itemCur.IDMarket, itemCur.typeObject, 1);
			}
			break;
		case 4:
			if (itemCur != null)
			{
				GlobalService.gI().Market(7, indexMarket, itemCur.IDMarket, itemCur.typeObject, 1);
			}
			break;
		case 5:
			if (itemCur != null)
			{
				GlobalService.gI().Market(6, indexMarket, itemCur.IDMarket, itemCur.typeObject, 1);
			}
			break;
		case 6:
			if (itemCur != null)
			{
				if (itemCur.typeObject == 3)
				{
					GlobalService.gI().Market(11, indexMarket, itemCur.IDMarket, itemCur.typeObject, 1);
				}
				else
				{
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.banPotion);
				}
			}
			break;
		case 8:
			if (itemCur != null)
			{
				MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + itemCur.typeEquip);
				switch (subIndex)
				{
				case 0:
					TabInventory.LvUpgradeSS = mainItem.LvUpgrade;
					vecInfoSS = MainItem.getInfoSS(itemCur, TabInventory.LvUpgradeSS);
					cmdPlusItem.caption = "  +" + itemCur.LvUpgrade;
					cmdPlusItem.subIndex = 1;
					break;
				case 1:
					TabInventory.LvUpgradeSS = 0;
					vecInfoSS = MainItem.getInfoSS(itemCur);
					cmdPlusItem.caption = "  +" + mainItem.LvUpgrade;
					cmdPlusItem.subIndex = 0;
					break;
				}
			}
			break;
		case 7:
			break;
		}
	}

	public override void beginFocus()
	{
		setCreateCmd();
		if (GameCanvas.isTouchNoOrPC())
		{
			IdSelect = 0;
			getItemCurNew();
		}
		else
		{
			IdSelect = -1;
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xCurBegin, yCurBegin, wCur, hCur - miniItem, 0, 4);
		int num = miniItem;
		int num2 = 2;
		if (vecCmd != null && GameCanvas.getShowCmd() && (GameCanvas.currentScreen.setCurTypetab(1) || GameCanvas.isTouch))
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		if (vecList == null)
		{
			mFont.tahoma_7b_white.drawString(g, T.nullData, num2 + wCur / 2, num + MainTab.wItem + 2, 2);
			GameCanvas.resetTrans(g);
		}
		else
		{
			setClip(g);
			int num3 = 0;
			int num4 = 0;
			num3 = list.cmx / wItemCur - 1;
			num4 = hCur / wItemCur + 3 + num3;
			num += wItemCur * num3;
			for (int j = num3; j < num4; j++)
			{
				if (j >= 0 && j < vecList.size())
				{
					g.setColor(14075832);
					g.drawRect(2, num + 1, wCur - 5, wItemCur - 2);
					if (IdSelect == j && GameCanvas.currentScreen.setCurTypetab(1))
					{
						g.setColor(16777215);
						g.drawRect(3, num + 2, wCur - 7, wItemCur - 4);
					}
					MainItem mainItem = (MainItem)vecList.elementAt(j);
					mainItem.paint(g, num2 + MainTab.wItem / 2, num + MainTab.wItem / 2 + 1, MainTab.wItem);
					if (mainItem.typeObject == 3)
					{
						AvMain.setTextColorName(mainItem.colorName).drawString(g, mainItem.name, num2 + MainTab.wItem + 2, num + MainTab.wItem / 2 - 3, 0);
					}
					else
					{
						string text = mainItem.numPotion + string.Empty;
						if (mainItem.typeObject == 4 && mainItem.ID == 0)
						{
							text += "M";
						}
						AvMain.setTextColorName(mainItem.colorName).drawString(g, text + " x " + mainItem.name, num2 + MainTab.wItem + 2, num + MainTab.wItem / 2 - 3, 0);
					}
					mFont.tahoma_7_yellow.drawString(g, T.price + " " + AvMain.getDotNumber(mainItem.priceVND), num2 + miniItem, num + MainTab.wItem, 0);
					if (indexMarket == 3 && mainItem.typeMarket != 1)
					{
						if (mainItem.typeMarket >= 0 && mainItem.typeMarket < T.mtypeMarket.Length)
						{
							mFont.tahoma_7_yellow.drawString(g, T.mtypeMarket[mainItem.typeMarket], num2 + wCur - 6, num + MainTab.wItem, 1);
						}
					}
					else
					{
						mainItem.marketTime.paintCountDownTicketHour(g, mFont.tahoma_7_white, num2 + wCur - 32, num + MainTab.wItem, 0);
					}
				}
				num += wItemCur;
			}
			mGraphics.resetTransAndroid(g);
			g.restoreCanvas();
			GameCanvas.resetTrans(g);
			if (isShowInfo && GameCanvas.currentScreen.setCurTypetab(1) && itemCur != null)
			{
				ShowInfo(g, itemCur, vecInfoSS, 0, xInfo, yInfo, isLv: false, null, TabInventory.LvUpgradeSS);
				if (cmdPlusItem != null)
				{
					cmdPlusItem.paintXY(g, cmdPlusItem.xCmd, cmdPlusItem.yCmd);
				}
			}
		}
		Interface_Game.paintEffShowMoney(g, MotherCanvas.hw - Interface_Game.wMoneyEff / 2, GameScreen.h12plus, isShow: false, 0);
	}

	public void setClip(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(xCurBegin + 2, yCurBegin + 1, wCur - 4, hCur - 1 - miniItem - 1);
		g.saveCanvas();
		g.ClipRec(xCurBegin + 2, yCurBegin + 1, wCur - 4, hCur - 1 - miniItem - 1);
		g.translate(xCurBegin, yCurBegin);
		g.translate(0, -list.cmx);
	}

	public override void update()
	{
		if (vecList != null)
		{
			for (int i = 0; i < vecList.size(); i++)
			{
				MainItem mainItem = (MainItem)vecList.elementAt(i);
				mainItem.marketTime.updateTimeCountDownTicket();
				if (mainItem.marketTime.timeCountDown <= 0 && mainItem.typeMarket == 1)
				{
					mainItem.typeMarket = 3;
				}
			}
		}
		list.moveCamera();
		if (itemCur != null)
		{
			updateShowInfo();
			if (itemCur.isRemove)
			{
				itemCur = null;
				getItemCurNew();
			}
		}
	}

	public override void updateShowInfo()
	{
		if (!isShowInfo)
		{
			timeShowInfo++;
			if (timeShowInfo >= 5)
			{
				isShowInfo = true;
				setPosInfo();
			}
		}
		else
		{
			timeShowInfo = 0;
		}
	}

	public override void updatekey()
	{
		if (GameCanvas.keyMove(0) || GameCanvas.keyMove(2))
		{
			GameCanvas.currentScreen.setTypeTab(0);
			GameCanvas.ClearkeyMove(0);
			GameCanvas.ClearkeyMove(2);
		}
		bool flag = false;
		int idSelect = IdSelect;
		if (GameCanvas.keyMove(1))
		{
			IdSelect--;
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			IdSelect++;
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (vecList == null)
		{
			IdSelect = -1;
		}
		else if (flag)
		{
			IdSelect = AvMain.resetSelect(IdSelect, vecList.size() - 1, isreset: false);
			if (idSelect != IdSelect && IdSelect >= 0)
			{
				if (GameCanvas.isTouchNoOrPC())
				{
					int num = IdSelect * wItemCur - hCur / 2;
					if (IdSelect > 0)
					{
						num += wItemCur / 2;
					}
					list.setToX(num);
				}
				getItemCurNew();
				isShowInfo = false;
			}
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointerSelect && vecList != null && vecList.size() > 0 && GameCanvas.isPoint(xCurBegin, yCurBegin, wCur, hCur))
		{
			int num = (GameCanvas.py - yCurBegin + list.cmx) / wItemCur;
			if (num == IdSelect)
			{
				IdSelect = num;
			}
			else
			{
				IdSelect = num;
				getItemCurNew();
			}
			GameCanvas.isPointerSelect = false;
		}
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				((iCommand)vecCmd.elementAt(i)).updatePointer();
			}
		}
		if (isShowInfo && cmdPlusItem != null)
		{
			cmdPlusItem.updatePointer();
		}
		base.updatePointer();
	}

	public override void setPosInfo()
	{
		setPosInfo(itemCur, xCurBegin + MainTab.wTab + 24, yCurBegin + 2);
		if (GameCanvas.isTouch)
		{
			addButtonPlus();
		}
	}

	public override void updateInfo()
	{
		if (vecList != null)
		{
			MainItem mainItem = (MainItem)vecList.elementAt(IdSelect);
			if (mainItem != null && itemCur != null && mainItem != itemCur)
			{
				itemCur = mainItem;
				isShowInfo = false;
			}
		}
	}

	public void getItemCurNew()
	{
		if (vecList != null)
		{
			isShowInfo = false;
			IdSelect = AvMain.resetSelect(IdSelect, vecList.size() - 1, isreset: false);
			itemCur = (MainItem)vecList.elementAt(IdSelect);
			if (indexMarket == 3)
			{
				getCmd();
			}
			vecInfoSS = MainItem.getInfoSS(itemCur);
		}
	}

	private void getCmd()
	{
		if (indexMarket != 3)
		{
			return;
		}
		vecCmd.removeAllElements();
		if (!GameCanvas.isTouch)
		{
			center = null;
			left = null;
			right = null;
		}
		if (itemCur == null)
		{
			return;
		}
		if (itemCur.typeMarket == 1)
		{
			vecCmd.addElement(cmdCancleSell);
			if (!GameCanvas.isTouch)
			{
				left = cmdCancleSell;
			}
			menuCMD = cmdCancleSell;
		}
		if (itemCur.typeMarket == 0 || itemCur.typeMarket == 3 || itemCur.typeMarket == 4)
		{
			vecCmd.addElement(cmdGet);
			if (itemCur.typeObject == 3)
			{
				vecCmd.addElement(cmdSell);
			}
			if (!GameCanvas.isTouch)
			{
				if (itemCur.typeObject == 3)
				{
					left = cmdSell;
				}
				center = cmdGet;
			}
			if (itemCur.typeObject == 3)
			{
				menuCMD = cmdSell;
			}
			okCMD = cmdGet;
		}
		if (itemCur.typeMarket == 2)
		{
			vecCmd.addElement(cmdGet);
			if (!GameCanvas.isTouch)
			{
				center = cmdGet;
			}
			okCMD = cmdGet;
		}
	}

	public override void setData(mVector vec)
	{
		vecList = vec;
		int num = 0;
		if (vecList != null)
		{
			num = vecList.size();
		}
		int limX = num * wItemCur - hCur + miniItem * 3;
		list = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		setCreateCmd();
		if (this == ScrTab.tabCurrent)
		{
			beginFocus();
			getCmd();
		}
	}

	public sbyte getIndexMarket()
	{
		return indexMarket;
	}

	public void addButtonPlus()
	{
		TabInventory.LvUpgradeSS = 0;
		if (itemCur == null || itemCur.typeObject != 3 || (itemCur.charClass != GameScreen.player.clazz && itemCur.charClass > 0))
		{
			if (cmdPlusItem != null)
			{
				cmdPlusItem.setPos(-50, -50, AvMain.fraPlus, string.Empty);
			}
			return;
		}
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + itemCur.typeEquip);
		if (itemCur.LvUpgrade != mainItem.LvUpgrade)
		{
			cmdPlusItem = new iCommand(T.plus, 8, 0, this);
			int num = itemCur.hInfo - itemCur.hRunInfo;
			cmdPlusItem.setPos(xInfo + itemCur.wInfo - 10, yInfo + num - 10, AvMain.fraPlus, "  +" + mainItem.LvUpgrade);
		}
		else
		{
			cmdPlusItem.setPos(-50, -50, AvMain.fraPlus, string.Empty);
		}
	}
}
