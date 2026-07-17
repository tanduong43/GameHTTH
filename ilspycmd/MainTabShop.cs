public class MainTabShop : MainTab
{
	public mVector vecShop = new mVector("MainTabShop.vecShop");

	public mVector vecInfoSS = new mVector("MainTabShop.vecInfoSS");

	public mVector vecCmd = new mVector();

	public MainItem itemCur;

	public static MainItem itemShipCur;

	public static bool isUpdateItemShip;

	public sbyte typeNPCShop;

	public iCommand cmdMenu;

	public iCommand cmdPlusItem;

	public InputDialog input;

	public const sbyte INVENTORY = 0;

	public const sbyte INVENTORY_SHOP = 1;

	public const sbyte INVENTORY_CHEST = 2;

	public const sbyte INVENTORY_UPGRADE = 3;

	public const sbyte INVENTORY_CLAN = 4;

	public const sbyte INVENTORY_MARKET = 5;

	public const sbyte INVENTORY_HUYHIEU = 6;

	public const sbyte INVENTORY_PET = 7;

	public static int maxNumItemW = 5;

	public static int maxNumItemH;

	public ListNew list;

	public Scroll scrShop = new Scroll();

	public static bool isSortInven;

	public int tickbuy;

	public static MainTabShop instance;

	public MainTabShop(string name, mVector vec)
	{
		instance = this;
	}

	public MainTabShop(string name, mVector vec, int maxSize, int xbegin)
	{
		instance = this;
		nameTab = name;
		vecShop = vec;
		wCur = MainTab.wItem * maxNumItemW;
		xCurBegin = xbegin + MainTab.wTab / 2 - wCur / 2 + 10;
		yCurBegin = MainTab.yTab + 32;
		base.maxSize = maxSize;
		int limX = ((maxSize - 1) / maxNumItemW + 1) * MainTab.wItem - hCur + miniItem;
		list = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrShop.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
	}

	public override void beginFocus()
	{
		isShowInfo = false;
		timeShowInfo = 0;
		if (GameCanvas.isTouchNoOrPC())
		{
			IdSelect = 0;
			if (vecShop.size() > 0)
			{
				setPosCmd(getMenuActionItem());
			}
		}
		else
		{
			IdSelect = -1;
			setPosCmd(null);
			itemCur = null;
			if (typeNPCShop == 101)
			{
				IdSelect = 0;
				if (vecShop.size() > 0)
				{
					setPosCmd(getMenuActionItem());
				}
			}
		}
		if (isSortInven)
		{
			vecShop = MainItem.SortVecItem(vecShop);
			isSortInven = false;
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xCurBegin, yCurBegin, wCur, hCur - miniItem, 0, 3);
		g.setClip(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem);
		g.saveCanvas();
		g.ClipRec(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem);
		g.translate(xCurBegin, yCurBegin);
		g.translate(0, -list.cmx);
		for (int i = 0; i < vecShop.size(); i++)
		{
			if (i / maxNumItemW * MainTab.wItem + MainTab.wItem / 2 + MainTab.wItem / 2 - 2 < list.cmx || i / maxNumItemW * MainTab.wItem + MainTab.wItem / 2 - MainTab.wItem / 2 + 2 > list.cmx + (hCur - 1 - miniItem))
			{
				continue;
			}
			MainItem mainItem = (MainItem)vecShop.elementAt(i);
			if (mainItem.typeObject == 3 || mainItem.typeObject == 102 || mainItem.typeObject == 103)
			{
				mainItem.paintColor(g, i % maxNumItemW * MainTab.wItem + MainTab.wItem / 2, i / maxNumItemW * MainTab.wItem + MainTab.wItem / 2, MainTab.wItem);
			}
			mainItem.paint(g, i % maxNumItemW * MainTab.wItem + MainTab.wItem / 2, i / maxNumItemW * MainTab.wItem + MainTab.wItem / 2, MainTab.wItem);
			if (mainItem.typeObject == 4 && indexIconTab == 0)
			{
				DelaySkill.getDelay(mainItem.indexHotKey).paint(g, i % maxNumItemW * MainTab.wItem + 1, i / maxNumItemW * MainTab.wItem + 1, MainTab.wItem - 1);
			}
			paintItemCur(g, mainItem, i % maxNumItemW * MainTab.wItem + MainTab.wItem - 1, i / maxNumItemW * MainTab.wItem + MainTab.wItem - 1);
			if (IdSelect == i && GameCanvas.currentScreen.setCurTypetab(1))
			{
				g.setColor(16777215);
				g.drawRect(i % maxNumItemW * MainTab.wItem + 1, i / maxNumItemW * MainTab.wItem + 1, MainTab.wItem - 2, MainTab.wItem - 2);
				if (!GameCanvas.isSmallScreen)
				{
					g.drawRect(i % maxNumItemW * MainTab.wItem + 2, i / maxNumItemW * MainTab.wItem + 2, MainTab.wItem - 4, MainTab.wItem - 4);
				}
			}
		}
		if (maxSize % maxNumItemW != 0)
		{
			for (int j = maxSize; j < maxSize + (maxNumItemW - maxSize % maxNumItemW); j++)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, MainTab.wItem - 1, MainTab.wItem - 1, 0, j % maxNumItemW * MainTab.wItem + 1, j / maxNumItemW * MainTab.wItem + 1, 0);
			}
		}
		g.setColor(14075832);
		for (int k = 0; k < maxNumItemW - 1; k++)
		{
			g.fillRect(MainTab.wItem + k * MainTab.wItem, 1, 1, MainTab.wItem * ((maxSize - 1) / maxNumItemW + 1));
		}
		for (int l = 0; l <= (maxSize - 1) / maxNumItemW + 1; l++)
		{
			g.fillRect(1, l * MainTab.wItem, wCur - 1, 1);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		g.setColor(6966058);
		g.drawRect(xCurBegin, yCurBegin + 2, 0, hCur - miniItem - 4);
		paintShowBoat(g, MainTab.xTab + 22, MainTab.yTab + MainTab.hTab - 22);
		if (!GameCanvas.currentScreen.setCurTypetab(1))
		{
			return;
		}
		if (list.cmxLim > 0)
		{
			scrShop.paint(g);
		}
		if (isShowInfo && itemCur != null)
		{
			ShowInfo(g, itemCur, vecInfoSS, 0, xInfo, yInfo, isLv: false, null, TabInventory.LvUpgradeSS);
			if (cmdPlusItem != null)
			{
				cmdPlusItem.paintXY(g, cmdPlusItem.xCmd, cmdPlusItem.yCmd);
			}
		}
		if (vecCmd != null && GameCanvas.getShowCmd())
		{
			for (int m = 0; m < vecCmd.size(); m++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(m);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		base.paint(g);
	}

	public virtual void paintItemCur(mGraphics g, MainItem Item, int x, int y)
	{
	}

	public virtual void paintShowBoat(mGraphics g, int x, int y)
	{
	}

	public override void update()
	{
		int cmx = list.cmx;
		list.moveCamera();
		scrShop.setYScrool(list.cmx, list.cmxLim);
		if (list.cmx != cmx || list.pointerIsDowning)
		{
			isShowInfo = false;
		}
		else if (itemCur != null)
		{
			updateShowInfo();
		}
		if (itemCur != null && itemCur.isRemove)
		{
			itemCur = null;
			getItemCurNew();
		}
		if (isUpdateItemShip)
		{
			updateItemShip();
		}
		updateInshop();
		if (tickbuy > 0)
		{
			tickbuy--;
		}
	}

	public virtual void updateInshop()
	{
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(0))
		{
			if (IdSelect % maxNumItemW == 0)
			{
				GameCanvas.currentScreen.setTypeTab(0);
			}
			else
			{
				IdSelect--;
			}
			GameCanvas.ClearkeyMove(0);
			flag = true;
		}
		else if (GameCanvas.keyMove(2))
		{
			IdSelect++;
			GameCanvas.ClearkeyMove(2);
			flag = true;
		}
		else if (GameCanvas.keyMove(1))
		{
			if (IdSelect >= maxNumItemW)
			{
				IdSelect -= maxNumItemW;
			}
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			if (IdSelect < vecShop.size() - maxNumItemW)
			{
				IdSelect += maxNumItemW;
			}
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (flag)
		{
			getItemCurNew();
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointSelect(xCurBegin, yCurBegin, wCur, hCur))
		{
			int num = (GameCanvas.px - xCurBegin) / MainTab.wItem + (GameCanvas.py - yCurBegin + list.cmx) / MainTab.wItem * maxNumItemW;
			int num2 = vecShop.size();
			if (num >= 0 && num < num2)
			{
				GameCanvas.isPointerSelect = false;
				if (num == IdSelect)
				{
					mSystem.outz("menu perform id " + IdSelect);
					cmdMenu.perform();
				}
				else
				{
					isShowInfo = false;
					IdSelect = num;
					setPosCmd(getMenuActionItem());
				}
				if (!GameCanvas.currentScreen.setCurTypetab(1))
				{
					GameCanvas.currentScreen.setTypeTab(1);
				}
			}
			else
			{
				itemCur = null;
				isShowInfo = false;
				IdSelect = -1;
				setPosCmd(null);
			}
		}
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		if (isShowInfo && cmdPlusItem != null)
		{
			cmdPlusItem.updatePointer();
		}
		base.updatePointer();
	}

	public virtual mVector getMenuActionItem()
	{
		return null;
	}

	public virtual void setPosCmd(mVector vec)
	{
		left = null;
		center = null;
		vecCmd.removeAllElements();
		if (vec == null)
		{
			return;
		}
		iCommand cmdLeft = getCmdLeft();
		if (cmdLeft != null)
		{
			vec.addElement(cmdLeft);
		}
		if (MainTab.bigInfo > 0)
		{
			return;
		}
		if (vec.size() > 2)
		{
			if (GameCanvas.isTouch)
			{
				cmdMenu = AvMain.setPosCMD(cmdMenu, 0);
				vecCmd.addElement(cmdMenu);
				okCMD = cmdMenu;
			}
			else
			{
				center = cmdMenu;
			}
			return;
		}
		if (GameCanvas.isTouch)
		{
			vecCmd = vec;
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand cmd = (iCommand)vecCmd.elementAt(i);
				cmd = AvMain.setPosCMD(cmd, i);
				if (i == 0)
				{
					okCMD = cmd;
				}
				if (i == 1)
				{
					menuCMD = cmd;
				}
			}
			return;
		}
		for (int j = 0; j < vec.size(); j++)
		{
			iCommand iCommand2 = (iCommand)vec.elementAt(j);
			if (j == 0)
			{
				center = iCommand2;
			}
			if (j == 1)
			{
				left = iCommand2;
			}
		}
	}

	public virtual iCommand getCmdLeft()
	{
		return null;
	}

	public override void setPosInfo()
	{
		setPosInfo(itemCur, xCurBegin + MainTab.wTab, yCurBegin + (IdSelect / maxNumItemW + 1) * MainTab.wItem - list.cmx + 4);
		if (GameCanvas.isTouch)
		{
			addButtonPlus();
		}
	}

	public virtual void addButtonPlus()
	{
	}

	public override void updateInfo()
	{
		MainItem mainItem = (MainItem)vecShop.elementAt(IdSelect);
		if (mainItem != null && itemCur != null && mainItem != itemCur)
		{
			itemCur = mainItem;
			isShowInfo = false;
		}
	}

	public virtual void getItemCurNew()
	{
		isShowInfo = false;
		IdSelect = AvMain.resetSelect(IdSelect, vecShop.size() - 1, isreset: false);
		list.setToX((IdSelect / maxNumItemW + 1) * MainTab.wItem - hCur / 2);
		if (IdSelect >= 0)
		{
			setPosCmd(getMenuActionItem());
		}
	}

	public virtual void updateItemShip()
	{
		isUpdateItemShip = false;
		if (itemCur == null || itemShipCur == null)
		{
			return;
		}
		for (int i = 0; i < vecShop.size(); i++)
		{
			MainItem mainItem = (MainItem)vecShop.elementAt(i);
			if (mainItem.typeObject == itemShipCur.typeObject && mainItem.ID == itemShipCur.ID)
			{
				IdSelect = i;
				updateInfo();
				break;
			}
		}
	}
}
