public class TabEquip : MainTab
{
	private int hCur3;

	private int wCur5;

	private int maxInfo;

	private int maxNumItem;

	private int maxCol;

	public static int maxEquip = 10;

	private int yE;

	private int xE;

	private int xIn;

	private int yIn;

	private int hTextCur;

	private int yplus;

	private MainItem itemCur;

	private iCommand cmdChangeWear;

	private iCommand cmdSetUniform;

	private iCommand cmdDelUniform;

	public static int[][] mposEquip;

	private int hTextTicket;

	private int hsaiso;

	public sbyte[] valueEquip = new sbyte[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

	public TabEquip(string name)
	{
		nameTab = name;
		if (GameCanvas.isTouch)
		{
			IdSelect = -1;
		}
		else
		{
			IdSelect = 0;
		}
		wItemCur = MainTab.hItemTab;
		if (GameCanvas.isTouch)
		{
			if (isBigScreen)
			{
				wItemCur = 32;
			}
			else
			{
				wItemCur = 28;
			}
		}
		hCur3 = hCur / 3;
		wCur5 = wCur / 5;
		hTextTicket = GameCanvas.hText;
		int num = miniItem;
		if (wItemCur > hCur / 6 + 1)
		{
			wItemCur = hCur / 6 + 1;
		}
		if (GameCanvas.isTouch && wItemCur < 23 && (!GameCanvas.isIos() || MotherCanvas.h >= 240))
		{
			wItemCur = 23;
			hsaiso = 3;
		}
		if (wItemCur % 2 == 0)
		{
			wItemCur--;
		}
		hTextCur = GameCanvas.hText;
		if (GameCanvas.isSmallScreen)
		{
			num = 2;
			hTextCur = GameCanvas.hText - 4;
			hsaiso = 2;
		}
		xIn = xCurBegin + num;
		yIn = yCurBegin + hCur3 * 2 + num;
		maxEquip = 10;
		maxInfo = hCur3 / hTextCur;
		maxCol = (Player.InfoShortEquip.Length - 1) / maxInfo + 1;
		if (maxCol <= 0)
		{
			maxCol = 1;
		}
		maxNumItem = 2;
		int num2 = maxEquip;
		if (num2 > 8)
		{
			num2 = 8;
		}
		yE = yCurBegin + (hCur3 * 2 - ((num2 + 1) / 2 - 1) * wItemCur) / 2 - yplus * 3 / 2 - 2;
		xE = xCurBegin + wCur5 / 2 - wItemCur / 2;
		indexIconTab = 1;
		mposEquip = mSystem.new_M_Int(maxEquip, 2);
		for (int i = 0; i < mposEquip.Length; i++)
		{
			switch (i)
			{
			case 8:
				mposEquip[i][0] = xE + i % 2 * (wCur5 * 4 + 3) + wItemCur * 5 / 4;
				mposEquip[i][1] = yE - wItemCur / 2 + i / 2 * (wItemCur + yplus) - wItemCur * 5 / 4 - hsaiso;
				break;
			case 9:
				mposEquip[i][0] = xE + i % 2 * (wCur5 * 4 + 3) - wItemCur * 5 / 4;
				mposEquip[i][1] = yE - wItemCur / 2 + i / 2 * (wItemCur + yplus) - wItemCur * 5 / 4 - hsaiso;
				break;
			default:
				mposEquip[i][0] = xE + i % 2 * (wCur5 * 4 + 3);
				mposEquip[i][1] = yE - wItemCur / 2 + i / 2 * (wItemCur + yplus) - hsaiso;
				break;
			}
		}
		cmdChangeWear = new iCommand(T.change, 0, this);
		cmdChangeWear = AvMain.setPosCMD(cmdChangeWear, 0);
		cmdSetUniform = new iCommand(T.setUniform, 1, this);
		cmdSetUniform = AvMain.setPosCMD(cmdSetUniform, 1);
		cmdDelUniform = new iCommand(T.delUniorm, 3, this);
	}

	public override void beginFocus()
	{
		isShowInfo = false;
		if (GameCanvas.isTouchNoOrPC())
		{
			IdSelect = 0;
			itemCur = (MainItem)GameScreen.player.hashEquip.get(string.Empty + IdSelect);
		}
		else
		{
			IdSelect = -1;
			itemCur = null;
		}
		center = cmdChangeWear;
		maxCol = (Player.InfoShortEquip.Length - 1) / maxInfo + 1;
		if (maxCol <= 0)
		{
			maxCol = 1;
		}
		left = cmdSetUniform;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (IdSelect >= 0)
			{
				mVector listItem = getListItem();
				if (listItem.size() == 0)
				{
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullChangeItem);
					break;
				}
				MsgListItem msgListItem = new MsgListItem();
				msgListItem.setinfoListItem(listItem, mposEquip[IdSelect][0], mposEquip[IdSelect][1] + MainTab.wItem / 2, wItemCur, (IdSelect % 2 == 0) ? 2 : 0);
				GameCanvas.Start_Current_Dialog(msgListItem);
			}
			break;
		case 1:
		{
			mVector mVector2 = new mVector();
			for (int j = 0; j < T.mColorUniform.Length; j++)
			{
				iCommand o = new iCommand(T.mColorUniform[j], 2, j, this);
				mVector2.addElement(o);
			}
			mVector2.addElement(cmdDelUniform);
			GameCanvas.menuCur.startAt(mVector2, 2, T.setUniform);
			break;
		}
		case 2:
		{
			for (int i = 0; i < Player.vecUniform.size(); i++)
			{
				Uniform uniform = (Uniform)Player.vecUniform.elementAt(i);
				if (uniform.index == subIndex)
				{
					uniform.setUniform((sbyte)subIndex);
					Uniform.checkIndexItem(isRemove: false);
					GameCanvas.saveRms.SaveUniform();
					return;
				}
			}
			Uniform uniform2 = new Uniform();
			uniform2.setUniform((sbyte)subIndex);
			Player.vecUniform.addElement(uniform2);
			Uniform.checkIndexItem(isRemove: false);
			GameCanvas.saveRms.SaveUniform();
			break;
		}
		case 3:
			Player.vecUniform.removeAllElements();
			GameCanvas.saveRms.SaveUniform();
			Uniform.checkIndexItem(isRemove: true);
			break;
		}
	}

	private mVector getListItem()
	{
		mVector mVector2 = new mVector();
		int num = valueEquip[IdSelect];
		if (itemCur != null)
		{
			num = itemCur.typeEquip;
		}
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem.typeObject == 3 && mainItem.typeEquip == num && (mainItem.charClass == 0 || mainItem.charClass == GameScreen.player.clazz))
			{
				mVector2.addElement(mainItem);
			}
		}
		return mVector2;
	}

	public override void paint(mGraphics g)
	{
		int num = 0;
		if (GameCanvas.isSmallScreen)
		{
			num = 3;
		}
		AvMain.paintRect(g, xCurBegin + wCur / 2 - wCur5 * 3 / 2 - num, yCurBegin + miniItem - hsaiso, wCur5 * 3, hCur3 * 2 - miniItem * 2 + hsaiso, 0, 3);
		int num2 = xCurBegin + wCur / 2 - wCur5 * 3 / 2 - num + 3 + 14;
		int num3 = yCurBegin + miniItem + 4 - hsaiso;
		AvMain.fraMoney.drawFrame(2, num2 - 8, num3 + 5, 0, 3, g);
		if (MainTab.CDTicket.timeCountDown <= 0 || MainTab.valuephantram < 60)
		{
			mFont.tahoma_7_white.drawString(g, Player.ticket + "/" + Player.maxTicket, num2, num3, 0);
		}
		else
		{
			MainTab.CDTicket.paintCountDownTicketHour(g, mFont.tahoma_7_yellow, num2, num3, 0);
		}
		AvMain.fraMoney.drawFrame(4, num2 - 8, num3 + hTextTicket + 5, 0, 3, g);
		if (MainTab.CDPvP.timeCountDown <= 0 || MainTab.valuephantram < 60)
		{
			mFont.tahoma_7_white.drawString(g, Player.PvPticket + "/" + Player.maxPvPticket, num2, num3 + hTextTicket, 0);
		}
		else
		{
			MainTab.CDPvP.paintCountDownTicketHour(g, mFont.tahoma_7_yellow, num2, num3 + hTextTicket, 0);
		}
		AvMain.fraMoney.drawFrame(3, num2 - 8, num3 + hTextTicket * 2 + 5, 0, 3, g);
		if (MainTab.CDKeyBoss.timeCountDown <= 0 || MainTab.valuephantram < 60)
		{
			mFont.tahoma_7_white.drawString(g, Player.keyBoss + "/" + Player.maxKeyboss, num2, num3 + hTextTicket * 2, 0);
		}
		else
		{
			MainTab.CDKeyBoss.paintCountDownTicketHour(g, mFont.tahoma_7_yellow, num2, num3 + hTextTicket * 2, 0);
		}
		AvMain.fraMoney.drawFrame(6, num2 - 8, num3 + hTextTicket * 3 + 5, 0, 3, g);
		if (MainTab.CDx2XP.timeCountDown <= 0)
		{
			mFont.tahoma_7_white.drawString(g, "00:00", num2, num3 + hTextTicket * 3, 0);
		}
		else
		{
			MainTab.CDx2XP.paintCountDownTicketHour(g, mFont.tahoma_7_yellow, num2, num3 + hTextTicket * 3, 0);
		}
		AvMain.fraMoney.drawFrame(5, num2 - 8, num3 + 5 + hTextTicket * 4, 0, 3, g);
		mFont.tahoma_7_white.drawString(g, string.Empty + GameScreen.player.pointPk, num2, num3 + hTextTicket * 4, 0);
		for (int i = 0; i < maxEquip; i++)
		{
			int index = 3;
			if (i < 8)
			{
				AvMain.paintRect(g, mposEquip[i][0] - 1, mposEquip[i][1] - 1, wItemCur - 2, wItemCur - 2, 0, index);
			}
			MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + i);
			if (mainItem != null)
			{
				mainItem.paintColor(g, mposEquip[i][0] + wItemCur / 2 - 1, mposEquip[i][1] + wItemCur / 2 - 1, wItemCur - 3);
				mainItem.paint(g, mposEquip[i][0] + wItemCur / 2 - 1, mposEquip[i][1] + wItemCur / 2 - 1, wItemCur, 1);
			}
			else if (AvMain.fraEquip != null)
			{
				AvMain.fraEquip.drawFrame(i, mposEquip[i][0] + wItemCur / 2 - 1, mposEquip[i][1] + wItemCur / 2 - 1, 0, 3, g);
			}
			if (IdSelect == i && GameCanvas.currentScreen.setCurTypetab(1))
			{
				g.setColor(16777215);
				g.drawRect(mposEquip[i][0], mposEquip[i][1], wItemCur - 4, wItemCur - 4);
				g.drawRect(mposEquip[i][0] + 1, mposEquip[i][1] + 1, wItemCur - 6, wItemCur - 6);
			}
		}
		g.drawImage(MainObject.imgShadow, xCurBegin + wCur / 5 * 3 + 1 - num, yCurBegin + hCur3 + GameScreen.player.hOne / 4, 3);
		GameScreen.player.paintCharShow(g, xCurBegin + wCur / 5 * 3 - num, yCurBegin + hCur3 + GameScreen.player.hOne / 4, 0, isNhip: true);
		AvMain.paintRect(g, xCurBegin, yCurBegin + hCur3 * 2, wCur, hCur3 - miniItem, 0, 4);
		if (GameCanvas.isSmallScreen || (maxInfo == 2 && !GameCanvas.isIos()))
		{
			for (int j = 0; j < Player.InfoShortEquip.Length; j++)
			{
				if (GameCanvas.gameTick % 60 < 20)
				{
					mFont.tahoma_7_white.drawString(g, T.mNameShortInfo[j], xIn + j / maxInfo * wCur / maxCol, yIn + j % maxInfo * hTextCur, 0);
				}
				else
				{
					mFont.tahoma_7_white.drawString(g, string.Empty + Player.InfoShortEquip[j], xIn + j / maxInfo * wCur / maxCol, yIn + j % maxInfo * hTextCur, 0);
				}
			}
		}
		else
		{
			for (int k = 0; k < Player.InfoShortEquip.Length; k++)
			{
				mFont.tahoma_7_black.drawString(g, T.mNameShortInfo[k], xIn + k / maxInfo * wCur / maxCol, yIn + k % maxInfo * hTextCur, 0);
				mFont.tahoma_7_white.drawString(g, string.Empty + Player.InfoShortEquip[k], xIn + k / maxInfo * wCur / maxCol + 25, yIn + k % maxInfo * hTextCur, 0);
			}
		}
		GameCanvas.resetTrans(g);
		if (GameCanvas.currentScreen.setCurTypetab(1) && !GameCanvas.menuCur.isShowMenu && GameCanvas.currentDialog == null && IdSelect >= 0)
		{
			if (isShowInfo && itemCur != null)
			{
				ShowInfo(g, itemCur, null, 0, xInfo, yInfo, isLv: false, GameScreen.player, 0);
			}
			base.paint(g);
		}
	}

	public override void update()
	{
		if (itemCur != null)
		{
			updateShowInfo();
		}
		MainTab.updateTimeCountDownTicket();
	}

	public override void updatekey()
	{
		bool flag = false;
		int idSelect = IdSelect;
		if (GameCanvas.keyMove(0))
		{
			if (IdSelect == 8)
			{
				IdSelect = 6;
			}
			else if (IdSelect % maxNumItem == 0)
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
			if (IdSelect == 9)
			{
				IdSelect = 7;
			}
			else
			{
				IdSelect++;
			}
			GameCanvas.ClearkeyMove(2);
			flag = true;
		}
		else if (GameCanvas.keyMove(1))
		{
			if (IdSelect >= maxNumItem)
			{
				IdSelect -= maxNumItem;
			}
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			if (IdSelect < maxEquip - maxNumItem)
			{
				IdSelect += maxNumItem;
			}
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (flag)
		{
			isShowInfo = false;
			IdSelect = AvMain.resetSelect(IdSelect, maxEquip - 1, isreset: false);
			if (IdSelect == 8)
			{
				MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + IdSelect);
				if (mainItem == null)
				{
					mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + 9);
					if (mainItem == null)
					{
						IdSelect = idSelect;
						return;
					}
					IdSelect = 9;
				}
			}
			else if (IdSelect == 9)
			{
				MainItem mainItem2 = (MainItem)GameScreen.player.hashEquip.get(string.Empty + IdSelect);
				if (mainItem2 == null)
				{
					mainItem2 = (MainItem)GameScreen.player.hashEquip.get(string.Empty + 8);
					if (mainItem2 == null)
					{
						IdSelect = idSelect;
						return;
					}
					IdSelect = 8;
				}
			}
			itemCur = (MainItem)GameScreen.player.hashEquip.get(string.Empty + IdSelect);
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		if (GameCanvas.isPointSelect(xCurBegin, yCurBegin, wCur, hCur))
		{
			for (int i = 0; i < maxEquip; i++)
			{
				if (!GameCanvas.isPointSelect(mposEquip[i][0] - 2, mposEquip[i][1] - 2, wItemCur, wItemCur))
				{
					continue;
				}
				if (i == 8 || i == 9)
				{
					MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + i);
					if (mainItem == null)
					{
						GameCanvas.isPointerSelect = false;
						break;
					}
				}
				if (i != IdSelect)
				{
					IdSelect = i;
					itemCur = (MainItem)GameScreen.player.hashEquip.get(string.Empty + IdSelect);
					center = cmdChangeWear;
					okCMD = center;
					isShowInfo = false;
				}
				else
				{
					cmdChangeWear.perform();
				}
				GameCanvas.isPointerSelect = false;
				break;
			}
		}
		base.updatePointer();
	}

	public override void setPosInfo()
	{
		if (MotherCanvas.w >= 400)
		{
			setPosInfo(itemCur, xCurBegin + MainTab.wTab, mposEquip[IdSelect][1] + 1);
		}
		else if (IdSelect % 2 == 0)
		{
			setPosInfo(itemCur, mposEquip[IdSelect][0] + wItemCur + itemCur.wInfo / 2 + 2, mposEquip[IdSelect][1] + wItemCur);
		}
		else
		{
			setPosInfo(itemCur, mposEquip[IdSelect][0] - itemCur.wInfo / 2 - 6, mposEquip[IdSelect][1] + wItemCur);
		}
	}

	public override void updateInfo()
	{
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + IdSelect);
		if (mainItem != null && itemCur != null && mainItem != itemCur)
		{
			itemCur = mainItem;
			isShowInfo = false;
		}
	}
}
