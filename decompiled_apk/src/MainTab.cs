public class MainTab : AvMain
{
	public string nameTab;

	public int IdSelect;

	public sbyte indexIconTab;

	public int maxSize;

	public static int wItem;

	public static int xTab;

	public static int wTab = 225;

	public static int hTab = 194;

	public static int yTab;

	public static int hItemTab;

	public static int bigInfo = 0;

	public static int maxTypeTab = 9;

	public int wInfo;

	public int hInfo;

	public int xInfo;

	public int yInfo;

	public int wItemCur;

	public int xCurBegin;

	public int yCurBegin;

	public int wCur;

	public int hCur;

	public int miniItem;

	public int levelTab;

	public sbyte indexMarket = -1;

	public bool isShowInfo = true;

	public bool isBigScreen;

	public sbyte timeShowInfo;

	public static sbyte limitTimeShow = 0;

	public static int xMoney;

	public static int yMoney;

	public static mImage[] mImgTab;

	public static FrameImage fraCloseTab;

	public static FrameImage fraCloseTab2;

	public static FrameImage fraCmdMo;

	public static FrameImage fraCloseTab3;

	public static CountDownTicket CDTicket = new CountDownTicket();

	public static CountDownTicket CDPvP = new CountDownTicket();

	public static CountDownTicket CDKeyBoss = new CountDownTicket();

	public static CountDownTicket CDx2XP = new CountDownTicket();

	public static CountDownTicket CDDonateClan = new CountDownTicket();

	public static int xRun = -1;

	public static int timeRun;

	public static int COLOR_BORDER = 6967601;

	public static int COLOR_NEN = 16377262;

	public new const sbyte PAPER_NORMAL = 0;

	public const sbyte PAPER_MARKET = 1;

	public static int timenhapnhay = 0;

	public static int valuephantram = 0;

	public MainTab()
	{
		if (MotherCanvas.h >= 300 && GameCanvas.isTouch)
		{
			isBigScreen = true;
		}
		xMoney = MotherCanvas.w - 77;
		yMoney = 4;
		miniItem = 5;
		wInfo = 100;
		hInfo = 40;
		wCur = wTab - 70;
		hCur = hTab - 32;
		xCurBegin = xTab + wTab / 2 - wCur / 2 + 10;
		yCurBegin = yTab + 32;
		if (bigInfo > 0)
		{
			limitTimeShow = 1;
		}
		else
		{
			limitTimeShow = 15;
		}
	}

	public void createValue()
	{
		if (isBigScreen)
		{
			hTab = 240;
			wTab = 260;
		}
		if (wTab > MotherCanvas.w)
		{
			wTab = MotherCanvas.w;
		}
		if (hTab > MotherCanvas.h - 55 - GameCanvas.hCommand)
		{
			hTab = MotherCanvas.h - 55 - GameCanvas.hCommand;
		}
		if (GameCanvas.isShortH)
		{
			hTab += 30;
		}
		hItemTab = 28;
		if (hItemTab * 8 > hTab)
		{
			hItemTab = hTab / 8;
		}
		if (GameCanvas.isSmallScreen)
		{
			hItemTab = 16;
		}
		if (GameCanvas.isTouch)
		{
			if (isBigScreen)
			{
				wItem = 32;
				hItemTab = 30;
			}
			else
			{
				wItem = 28;
				hItemTab = 28;
			}
			if (hItemTab * 6 > hTab)
			{
				hItemTab = hTab / 6;
			}
		}
		else if (GameCanvas.isSmallScreen)
		{
			wItem = 20;
		}
		else
		{
			wItem = 24;
		}
		MainTabShop.maxNumItemW = (wTab - 60) / wItem;
		if (MainTabShop.maxNumItemW > 6)
		{
			MainTabShop.maxNumItemW = 6;
		}
		MainTabShop.maxNumItemH = (hTab - miniItem * 2) / wItem;
		if (MainTabShop.maxNumItemH > 6)
		{
			MainTabShop.maxNumItemH = 6;
		}
		xTab = MotherCanvas.hw - wTab / 2;
		yTab = MotherCanvas.hh + 25 - GameCanvas.hCommand / 2 - hTab / 2;
		if (GameCanvas.isShortH)
		{
			yTab -= 15;
		}
	}

	public virtual void resize(int max)
	{
	}

	public virtual void beginFocus()
	{
		isShowInfo = false;
		if (GameCanvas.isTouchNoOrPC())
		{
			IdSelect = 0;
		}
		else
		{
			IdSelect = -1;
		}
	}

	public virtual void paint(mGraphics g, int xbegin)
	{
		base.paint(g);
	}

	public override void paint(mGraphics g)
	{
		base.paint(g);
	}

	public override void update()
	{
	}

	public override void updatekey()
	{
		base.updatekey();
	}

	public override void updatePointer()
	{
		base.updatePointer();
	}

	public void ShowInfo(mGraphics g, MainItem itemCur, mVector infoSS, sbyte typeborder, int x, int y, bool isLv, MainObject obj, int lvUpPlus)
	{
		paintInfoEveryWhere(g, itemCur, infoSS, typeborder, x, y, wInfo, hInfo, isLv, obj, lvUpPlus);
	}

	public static void paintInfoEveryWhere(mGraphics g, MainItem itemCur, mVector infoSS, sbyte typeborder, int x, int y, int w, int h, bool isLv, MainObject obj, int lvUpPlus)
	{
		int num = y;
		int num2 = w;
		int num3 = h;
		int num4 = 0;
		if (itemCur != null)
		{
			num2 = itemCur.wInfo;
			num3 = itemCur.hInfo - itemCur.hRunInfo;
			num4 = itemCur.hRunInfo;
			int num5 = 40;
			if (itemCur.valueKickAn >= 0)
			{
				num5 = 100;
			}
			if (num4 > 0)
			{
				if (timeRun >= num5)
				{
					xRun++;
				}
				else if (timeRun >= 0)
				{
					xRun = 0;
				}
				if (xRun * 2 / 3 > num4 + GameCanvas.hText / 2)
				{
					if (timeRun > 0)
					{
						timeRun = -70;
					}
					else if (timeRun >= -10)
					{
						xRun = 0;
						timeRun = 0;
					}
				}
				timeRun++;
			}
			else
			{
				xRun = -1;
				timeRun = 0;
			}
		}
		else
		{
			xRun = -1;
			timeRun = 0;
		}
		AvMain.paintRect(g, x - 2, y - 2, num2 + 4, num3 + 4, 1, 6);
		sbyte b = 0;
		int num6 = 0;
		if (itemCur.typelock == 1)
		{
			num6 = 4;
			b = 1;
		}
		if (itemCur.charClass >= 1 && itemCur.charClass <= 5)
		{
			if (itemCur.typelock == 1)
			{
				num6 = 0;
				b = 2;
			}
			else
			{
				num6 = -4;
				b = 1;
			}
		}
		if (isLv)
		{
			AvMain.Font3dColor(g, itemCur.namepaint + " " + T.Lv + "." + itemCur.Lv_RQ, x + num2 / 2 + num6, num, 2, itemCur.colorName);
		}
		else if ((itemCur.isHoanMy == 1 && itemCur.valueKickAn >= 0) || itemCur.colorName == 9)
		{
			AvMain.FontSevenColor(g, itemCur.namepaint, x + num2 / 2 + num6, num, 2, 7);
		}
		else
		{
			AvMain.Font3dColor(g, itemCur.namepaint, x + num2 / 2 + num6, num, 2, itemCur.colorName);
		}
		int num7 = 0;
		if (b > 0)
		{
			num7 = mFont.tahoma_7b_white.getWidth(itemCur.namepaint);
			num6 = ((b != 2) ? CRes.abs(num6) : 8);
		}
		if (itemCur.typelock == 1)
		{
			g.drawImage(AvMain.imgInfoLock, x + num2 / 2 - num7 / 2 - num6, num + 5, 3);
		}
		if (itemCur.charClass >= 1 && itemCur.charClass <= 5)
		{
			g.drawRegion(AvMain.imgInfoClass, 0, (itemCur.charClass - 1) * 15, 15, 15, 0, x + num2 / 2 + num7 / 2 + num6, num + 5, 3);
		}
		int num8 = num3;
		if (typeborder == 1)
		{
			num += wItem;
			num8 -= wItem;
		}
		else
		{
			num += GameCanvas.hText;
			num8 -= GameCanvas.hText;
		}
		if (itemCur.isHoanMy == 1)
		{
			g.drawImage(AvMain.imgInfoStar, x + num2 / 2, num + 4, 3);
			num += 14;
			num8 -= 14;
		}
		if (itemCur.mDaKham != null && itemCur.numLoKham > 0)
		{
			int num9 = (itemCur.numLoKham - 1) * 22 / 2;
			for (int i = 0; i < itemCur.numLoKham; i++)
			{
				if (i >= itemCur.numLoKham - itemCur.numHoleDaDuc)
				{
					g.drawRegion(AvMain.imgDaKham, 0, 20, 20, 20, 0, x + num2 / 2 + i * 22 - num9, num + 10, 3);
				}
				else
				{
					g.drawRegion(AvMain.imgDaKham, 0, 0, 20, 20, 0, x + num2 / 2 + i * 22 - num9, num + 10, 3);
				}
				if (itemCur.mDaKham != null && i <= itemCur.mDaKham.Length - 1)
				{
					Potion templatePotion = Potion.getTemplatePotion(itemCur.mDaKham[i]);
					if (templatePotion.isUpdateTem)
					{
						templatePotion.paint(g, x + num2 / 2 + i * 22 - num9, num + 10, 20);
					}
				}
			}
			num += 24;
			num8 -= 22;
		}
		if (xRun >= 0)
		{
			g.setClip(x, num - 2, num2, num8);
			g.saveCanvas();
			g.ClipRec(x, num - 2, num2, num8);
			g.translate(0, -(xRun * 2 / 3));
		}
		for (int j = 0; j < itemCur.vecInfo.size(); j++)
		{
			infoShow infoShow2 = (infoShow)itemCur.vecInfo.elementAt(j);
			int num10 = 0;
			if (itemCur.valueKickAn > 0 && j == 0)
			{
				MainImage mainImage = null;
				mainImage = ObjectData.getImageAll((short)(itemCur.valueKickAn + 400), ObjectData.hashImageSkillSmall, 4500);
				if (mainImage != null && mainImage.img != null)
				{
					g.drawImage(mainImage.img, x + 6, num + 5, 3);
					num10 = 12;
				}
			}
			if (itemCur.typeObject == 105 && itemCur.LvUpgrade > 0 && j == itemCur.vecInfo.size() - 2)
			{
				AvMain.setTextColor(5).drawString(g, infoShow2.getInfoFormID(), x + num10 + 4, num, 0);
			}
			else if (infoShow2.colorMain >= infoShow.HARDCODE_CHECK_FULLSET_1)
			{
				if (obj != null && obj.indexFullSet + 100 >= infoShow2.colorMain)
				{
					AvMain.setTextColor(1).drawString(g, infoShow2.getInfoFormID(), x + num10 + 4, num, 0);
				}
				else
				{
					AvMain.setTextColor(infoShow2.color).drawString(g, infoShow2.getInfoFormID(), x + num10 + 4, num, 0);
				}
			}
			else if (infoShow2.colorMain == infoShow.HARDCODE_PAINT_CENTER)
			{
				AvMain.setTextColor(infoShow2.color).drawString(g, infoShow2.getInfoFormID(), x + num10 + num2 / 2, num, 2);
			}
			else if (infoShow2.colorMain == infoShow.HARDCODE_PAINT_CENTER_CHI_SO)
			{
				int num11 = 0;
				if (lvUpPlus > 0)
				{
					num11 = lvUpPlus;
				}
				else if (itemCur != null)
				{
					num11 = itemCur.LvUpgrade;
				}
				AvMain.setTextColor(infoShow2.color).drawString(g, infoShow2.getInfoFormID() + "+" + num11 + " ---", x + num10 + num2 / 2, num, 2);
			}
			else if (infoShow2.colorMain == infoShow.HARDCODE_INFO_CO_BAN)
			{
				int valueLvUp = 0;
				if (lvUpPlus > 0)
				{
					valueLvUp = lvUpPlus;
				}
				else if (itemCur != null)
				{
					valueLvUp = itemCur.LvUpgrade;
				}
				string empty = string.Empty;
				empty = ((infoShow2.id >= 0) ? MainItem.getInfoAttriSS((short)infoShow2.id, infoShow2.value, valueLvUp, itemCur.LvUpgrade) : infoShow2.strShow);
				AvMain.setTextColor(infoShow2.color).drawString(g, empty, x + num10 + 4, num, 0);
				if (infoSS != null && j < infoSS.size())
				{
					infoShow infoShow3 = (infoShow)infoSS.elementAt(j);
					int num12 = AvMain.setTextColor(6).getWidth(empty) + 7;
					AvMain.setTextColor(infoShow3.color).drawString(g, infoShow3.strShow, x + num12, num, 0);
				}
			}
			else
			{
				AvMain.setTextColor(infoShow2.color).drawString(g, infoShow2.getInfoFormID(), x + num10 + 4, num, 0);
			}
			num += GameCanvas.hText;
		}
		if (xRun >= 0)
		{
			mGraphics.resetTransAndroid(g);
			g.restoreCanvas();
			GameCanvas.resetTrans(g);
		}
	}

	public void setWHin()
	{
	}

	public static void paintBgTab(mGraphics g, int xpaint, bool isClan, sbyte typePaper)
	{
		paintInfo_Money_Player(g, isClan);
		if (typePaper == 0)
		{
			paintPaperTab(g, xpaint, yTab, wTab, hTab);
		}
		else
		{
			AvMain.paintBG_AChi(g, xpaint, yTab, wTab, hTab, 0);
		}
	}

	private static void paintInfo_Money_Player(mGraphics g, bool isClan)
	{
		if (GameCanvas.isShortH && MainTabShop.instance.typeNPCShop != 118)
		{
			Interface_Game.paintInfoPlayer_Short(g, MotherCanvas.hw - 160, 3 + GameScreen.h12plus, isborder: true, mFont.tahoma_7_black);
			return;
		}
		Interface_Game.paintInfoPlayer(g, 3, 3 + GameScreen.h12plus, isborder: true, mFont.tahoma_7_black);
		paintMoney(g, MotherCanvas.w - 78, 4 + GameScreen.h12plus, isClan);
	}

	public static void paintPaperTab(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(COLOR_NEN);
		g.fillRect(x + 9, y, w - 18, h);
		for (int i = 0; i < h - 39; i += 40)
		{
			g.drawRegion(mImgTab[0], 0, 0, 20, 40, 2, x, y + i, 0);
			g.drawImage(mImgTab[0], x + w, y + i, mGraphics.RIGHT | mGraphics.TOP);
		}
		if (h % 40 != 0)
		{
			g.drawRegion(mImgTab[0], 0, 0, 20, h % 40, 2, x, y + h - h % 40, 0);
			g.drawRegion(mImgTab[0], 0, 0, 20, h % 40, 0, x + w, y + h - h % 40, mGraphics.RIGHT | mGraphics.TOP);
		}
		g.setColor(COLOR_BORDER);
		g.fillRect(x + 9, y - 1, w - 18, 1);
		g.fillRect(x + 9, y + h, w - 18, 1);
		g.drawRegion(mImgTab[1], 0, 0, 9, 3, 3, x, y - 3, 0);
		g.drawRegion(mImgTab[1], 0, 0, 9, 3, 1, x + w, y - 3, mGraphics.RIGHT | mGraphics.TOP);
		g.drawRegion(mImgTab[1], 0, 0, 9, 3, 2, x, y + h, 0);
		g.drawRegion(mImgTab[1], 0, 0, 9, 3, 0, x + w, y + h, mGraphics.RIGHT | mGraphics.TOP);
	}

	public static void paintListTab(mGraphics g, int xpaint, mVector vec, int idSelect)
	{
		int num = xpaint + 22;
		int num2 = yTab + 36;
		if (num2 + vec.size() * hItemTab > yTab + hTab + hItemTab / 2)
		{
			num2 = yTab + hTab / 2 - vec.size() * hItemTab / 2 + hItemTab / 2;
		}
		int num3 = 0;
		g.setColor(14203529);
		if (GameCanvas.currentScreen.setCurTypetab(1))
		{
			g.setColor(15972174);
		}
		g.fillRoundRect(num + (wTab - 22) / 2 - wTab / 4 * 3 / 2 + num3, yTab + 7, wTab / 4 * 3 - num3, 16, 4, 4);
		if (vec == null)
		{
			return;
		}
		for (int i = 0; i < vec.size(); i++)
		{
			MainTab mainTab = (MainTab)vec.elementAt(i);
			short id = (short)(200 + mainTab.indexIconTab);
			if (GameCanvas.isSmallScreen)
			{
				id = (short)(260 + mainTab.indexIconTab);
			}
			MainImage imageAll = ObjectData.getImageAll(id, ObjectData.hashImageItemOther, 9000);
			if (imageAll != null && imageAll.img != null)
			{
				g.drawImage(imageAll.img, num, num2 + i * hItemTab, 3);
			}
			if (idSelect == i)
			{
				if (GameCanvas.currentScreen.setCurTypetab(1) || timenhapnhay % 16 < 7)
				{
					MainImage imageAll2 = ObjectData.getImageAll((short)(230 + mainTab.indexIconTab), ObjectData.hashImageItemOther, 9000);
					if (imageAll2 != null && imageAll2.img != null)
					{
						g.drawImage(imageAll2.img, num, num2 + i * hItemTab, 3);
					}
				}
				if (mainTab.indexIconTab != 2)
				{
					if (GameCanvas.currentScreen.setCurTypetab(1))
					{
						AvMain.FontBorderColor(g, mainTab.nameTab, num + (wTab - 22) / 2, yTab + 9, 2, 6, 5);
					}
					else
					{
						mFont.tahoma_7b_black.drawString(g, mainTab.nameTab, num + (wTab - 22) / 2, yTab + 9, 2);
					}
				}
			}
			if (((mainTab.indexIconTab == 2 && Player.pointAttribute > 0) || (mainTab.indexIconTab == 3 && Player.isSkillready) || (mainTab.indexIconTab == 4 && TabQuest.isNewQuest) || (mainTab.indexIconTab == 6 && GameScreen.numMess > 0)) && GameCanvas.gameTick % 10 < 8)
			{
				g.drawImage(MainEvent.imgNew, num + 10, num2 - 10 + i * hItemTab, 3);
			}
		}
	}

	public static void paintMoney(mGraphics g, int x, int y, bool isClan)
	{
		int num = y;
		AvMain.paintRect(g, x, y, 74, 54, 1, 2);
		g.setColor(8086346);
		AvMain.fraMoney.drawFrame(0, x + 13, num + 7, 0, 3, g);
		if (isClan)
		{
			if (GameScreen.player.clan != null)
			{
				mFont.tahoma_7_yellow.drawString(g, AvMain.getDotNumber(GameScreen.player.clan.beryClan), x + 20, num + 2, 0);
			}
		}
		else
		{
			mFont.tahoma_7_yellow.drawString(g, AvMain.getDotNumber(Player.beliTest), x + 20, num + 2, 0);
		}
		num += 13;
		g.fillRect(x + 5, num, 64, 1);
		AvMain.fraMoney.drawFrame(1, x + 13, num + 7, 0, 3, g);
		if (isClan)
		{
			if (GameScreen.player.clan != null)
			{
				mFont.tahoma_7_yellow.drawString(g, AvMain.getDotNumber(GameScreen.player.clan.rubiClan), x + 20, num + 2, 0);
			}
		}
		else
		{
			mFont.tahoma_7_red.drawString(g, AvMain.getDotNumber(GameScreen.player.gem), x + 20, num + 2, 0);
		}
		num += 13;
		g.fillRect(x + 5, num, 64, 1);
		if (!isClan)
		{
			AvMain.fraMoney.drawFrame(7, x + 12, num + 7, 0, 3, g);
			mFont.tahoma_7_green.drawString(g, AvMain.getDotNumber(GameScreen.player.vnd), x + 20, num + 2, 0);
			num += 13;
			g.fillRect(x + 5, num, 64, 1);
			AvMain.fraMoney.drawFrame(9, x + 12, num + 7, 0, 3, g);
			mFont.tahoma_7_yellow.drawString(g, AvMain.getDotNumber(GameScreen.player.diemNap), x + 20, num + 2, 0);
		}
	}

	public static void paintMoney(mGraphics g, int x, int y)
	{
		int num = y;
		AvMain.paintRect(g, x, y, 74, 54, 1, 2);
		g.setColor(8086346);
		AvMain.fraMoney.drawFrame(0, x + 13, num + 7, 0, 3, g);
		mFont.tahoma_7_yellow.drawString(g, AvMain.getDotNumber(Player.beliTest), x + 20, num + 2, 0);
		num += 13;
		g.fillRect(x + 5, num, 64, 1);
		AvMain.fraMoney.drawFrame(1, x + 13, num + 7, 0, 3, g);
		mFont.tahoma_7_red.drawString(g, AvMain.getDotNumber(GameScreen.player.gem), x + 20, num + 2, 0);
		num += 13;
		g.fillRect(x + 5, num, 64, 1);
		AvMain.fraMoney.drawFrame(7, x + 12, num + 7, 0, 3, g);
		mFont.tahoma_7_green.drawString(g, AvMain.getDotNumber(GameScreen.player.vnd), x + 20, num + 2, 0);
		num += 13;
		g.fillRect(x + 5, num, 64, 1);
		AvMain.fraMoney.drawFrame(8, x + 12, num + 7, 0, 3, g);
		mFont.tahoma_7_orange.drawString(g, AvMain.getDotNumber(GameScreen.player.bua), x + 20, num + 2, 0);
	}

	public static void updateTimeCountDownTicket()
	{
		valuephantram = GameCanvas.gameTick % 120;
		CDTicket.updateTimeCountDownTicket();
		CDPvP.updateTimeCountDownTicket();
		CDKeyBoss.updateTimeCountDownTicket();
		CDx2XP.updateTimeCountDownTicket();
	}

	public virtual void updateShowInfo()
	{
		if (!isShowInfo)
		{
			timeShowInfo++;
			if (timeShowInfo >= limitTimeShow)
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

	public virtual void setPosInfo()
	{
	}

	public virtual void setPosInfo(MainItem item, int xbe, int ybe)
	{
		int num = wInfo;
		int num2 = hInfo;
		if (item != null)
		{
			num = item.wInfo;
			num2 = item.hInfo - item.hRunInfo;
		}
		xInfo = xbe - num / 2;
		if (xInfo + num > MotherCanvas.w - 4)
		{
			xInfo = MotherCanvas.w - num - 4;
		}
		if (xInfo < 8)
		{
			xInfo = 8;
		}
		yInfo = ybe;
		if (yInfo < yTab + GameCanvas.hCommand)
		{
			yInfo = yTab + GameCanvas.hCommand;
		}
		if (yInfo + num2 > MotherCanvas.h - GameCanvas.hCommand - 8)
		{
			yInfo = MotherCanvas.h - GameCanvas.hCommand - num2 - 8;
		}
	}

	public void endFocusInfo()
	{
		if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
		{
			GameCanvas.tabAllScr.endInfoTab();
		}
	}

	public virtual void updateInfo()
	{
	}

	public virtual void updateBuyItem(short id, sbyte type)
	{
	}

	public virtual void updateTrangBi()
	{
	}

	public virtual void updateChangeTabInfo()
	{
	}

	public virtual iCommand setCmdEndInfo()
	{
		return null;
	}

	public virtual void setData(mVector vec)
	{
	}
}
