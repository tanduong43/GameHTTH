using System;

public class TabInfo : MainTab
{
	private iCommand cmdSetPoint;

	private InputDialog input;

	private ListNew listInfo;

	private ListNew listAttri;

	private int lastCam;

	private int xBeginInfo;

	private Scroll scrInfo = new Scroll();

	private Scroll scrAttri = new Scroll();

	public const int LEVEL_TIEMNANG = 1;

	public const int LEVEL_THONGTIN = 0;

	private int xto;

	private int xcur;

	private int speed = 20;

	private int hItem;

	private int timefocus;

	private int[] mNumAttri = new int[3] { 1, 2, 10 };

	public static bool isshow = true;

	public TabInfo(string name)
	{
		nameTab = name;
		xBeginInfo = wCur;
		initCmd();
		listInfo = new ListNew();
		indexIconTab = 2;
	}

	public override void beginFocus()
	{
		int limX = GameScreen.player.vecAllInfo.size() * GameCanvas.hText - hCur + miniItem * 2;
		listInfo = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrInfo.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
		listInfo.cmx = lastCam;
		listInfo.cmtoX = lastCam;
		hItem = GameCanvas.hText * 4;
		limX = Player.mAttribute.Length * hItem - hCur + miniItem * 2 + GameCanvas.hText * 3 / 2;
		listAttri = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrAttri.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (IdSelect < 0 || IdSelect >= Player.mAttribute.Length || Player.mAttribute[IdSelect].value >= 80)
			{
				break;
			}
			if (Player.pointAttribute > 1)
			{
				mVector mVector2 = new mVector();
				int num2 = 0;
				for (int i = 0; i < mNumAttri.Length; i++)
				{
					int num3 = mNumAttri[i];
					if (num3 > Player.pointAttribute)
					{
						num3 = Player.pointAttribute;
					}
					if (num3 > 80 - Player.mAttribute[IdSelect].value)
					{
						num3 = 80 - Player.mAttribute[IdSelect].value;
					}
					iCommand iCommand2 = new iCommand("+" + num3, 2, num3, this);
					if (GameCanvas.isTouch)
					{
						iCommand2.levelSmall = 3;
					}
					if (num2 != num3)
					{
						num2 = num3;
						mVector2.addElement(iCommand2);
					}
					if (mNumAttri[i] >= Player.pointAttribute)
					{
						break;
					}
				}
				GameCanvas.Start_Normal_DiaLog_New(T.nhaptiemnang + T.mAttribute[IdSelect] + "?", mVector2, isCmdClose: true, T.tabAttribute);
			}
			else if (Player.pointAttribute == 1)
			{
				GlobalService.gI().Add_Point_Attribute((sbyte)IdSelect, 1);
			}
			else
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullPointAttribute);
			}
			break;
		case 1:
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
			GlobalService.gI().Add_Point_Attribute((sbyte)IdSelect, (short)num);
			GameCanvas.end_Dialog();
			break;
		}
		case 2:
			GlobalService.gI().Add_Point_Attribute((sbyte)IdSelect, (short)subIndex);
			GameCanvas.end_Dialog();
			break;
		}
	}

	public void initCmd()
	{
		cmdSetPoint = new iCommand(T.cmdSetPoint, 0, this);
		cmdSetPoint.setPos(MotherCanvas.hw, MotherCanvas.h - iCommand.hButtonCmdNor / 2, null, cmdSetPoint.caption);
		if (levelTab == 1)
		{
			if (GameCanvas.isTouchNoOrPC())
			{
				center = cmdSetPoint;
				okCMD = center;
			}
		}
		else
		{
			center = null;
			okCMD = null;
		}
	}

	public override void paint(mGraphics g)
	{
		g.setColor(14203529);
		int idx = 0;
		if (GameCanvas.currentScreen.setCurTypetab(1))
		{
			if (levelTab == 1)
			{
				g.fillRoundRect(MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - MainTab.wTab / 4 * 3 / 2, MainTab.yTab + 7, MainTab.wTab / 4 * 3 / 2, 16, 4, 4);
				idx = 2;
				AvMain.FontBorderColor(g, T.tabAttribute, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 15, MainTab.yTab + 9, 0, 6, 5);
				mFont.tahoma_7b_black.drawString(g, T.tabInfo, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - 15, MainTab.yTab + 9, 1);
			}
			else if (levelTab == 0)
			{
				g.fillRoundRect(MainTab.xTab + 22 + (MainTab.wTab - 22) / 2, MainTab.yTab + 7, MainTab.wTab / 4 * 3 / 2, 16, 4, 4);
				idx = 1;
				mFont.tahoma_7b_black.drawString(g, T.tabAttribute, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 15, MainTab.yTab + 9, 0);
				AvMain.FontBorderColor(g, T.tabInfo, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - 15, MainTab.yTab + 9, 1, 6, 5);
			}
		}
		else
		{
			mFont.tahoma_7b_black.drawString(g, T.tabAttribute, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 15, MainTab.yTab + 9, 0);
			mFont.tahoma_7b_black.drawString(g, T.tabInfo, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - 15, MainTab.yTab + 9, 1);
		}
		AvMain.fraTwoTab.drawFrame(idx, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2, MainTab.yTab + 9 + 6, 0, 3, g);
		if (Player.pointAttribute > 0 && GameCanvas.gameTick % 10 < 8)
		{
			g.drawImage(MainEvent.imgNew, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 9, MainTab.yTab + 9, 3);
		}
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xCurBegin, yCurBegin, wCur, hCur - miniItem, 0, 4);
		g.setClip(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		g.saveCanvas();
		g.ClipRec(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		g.translate(xCurBegin, yCurBegin);
		if (levelTab == 1)
		{
			g.translate(-xcur, -listAttri.cmx);
			paintTiemNang(g);
		}
		else if (levelTab == 0)
		{
			g.translate(-xcur, -listInfo.cmx);
			paintThongTin(g);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
	}

	public void paintTiemNang(mGraphics g)
	{
		if (GameCanvas.isTouchNoOrPC())
		{
			paintSelect(g);
		}
		int num = miniItem;
		int num2 = wCur;
		mFont.tahoma_7b_yellow.drawString(g, T.pointAttribute + ": " + Player.pointAttribute, num2 + miniItem, num, 0);
		num += GameCanvas.hText + GameCanvas.hText / 2;
		if (isshow)
		{
			for (int i = 0; i < Player.mAttribute.Length; i++)
			{
				Main_Attribute main_Attribute = Player.mAttribute[i];
				mFont.tahoma_7b_white.drawString(g, main_Attribute.name + ": " + main_Attribute.value, num2 + miniItem, num, 0);
				if (main_Attribute.valuePlus > 0)
				{
					int width = mFont.tahoma_7b_white.getWidth(main_Attribute.name + ": " + main_Attribute.value + " ");
					mFont.tahoma_7b_blue.drawString(g, "+" + main_Attribute.valuePlus, num2 + miniItem + width, num, 0);
				}
				for (int j = 0; j < main_Attribute.minfo.Length; j++)
				{
					string infoEveryWhere = MainItem.getInfoEveryWhere(main_Attribute.minfo[j]);
					mFont.tahoma_7_green.drawString(g, infoEveryWhere, num2 + miniItem * 2, num + (GameCanvas.hText - 2) * (j + 1), 0);
				}
				if (!GameCanvas.isSmallScreen && !GameCanvas.lowGraphic)
				{
					int idx = 0;
					if (Player.pointAttribute <= 0 || Player.mAttribute[i].value >= 80)
					{
						idx = 2;
					}
					else if (timefocus > 0 && i == IdSelect)
					{
						idx = 1;
					}
					AvMain.fraButtonTiemNang.drawFrame(idx, num2 + wCur - AvMain.fraButtonTiemNang.frameHeight / 2 - (GameCanvas.hText * 3 - AvMain.fraButtonTiemNang.frameWidth / 2) / 2, num + GameCanvas.hText * 3 / 2 - miniItem / 2, 0, 3, g);
				}
				num += hItem;
			}
		}
		if (GameCanvas.currentScreen.setCurTypetab(1))
		{
			base.paint(g);
			if (listAttri.cmxLim > 0)
			{
				scrAttri.paint(g);
			}
		}
	}

	public void paintThongTin(mGraphics g)
	{
		for (int i = 0; i < GameScreen.player.vecAllInfo.size(); i++)
		{
			MainInfoItem mainInfoItem = (MainInfoItem)GameScreen.player.vecAllInfo.elementAt(i);
			string infoEveryWhere = MainItem.getInfoEveryWhere(mainInfoItem);
			mFont.tahoma_7_white.drawString(g, infoEveryWhere, miniItem, miniItem + i * GameCanvas.hText, 0);
			if (GameScreen.player.vecBuffCur.size() > 0)
			{
				paintAttPlusBuff(g, infoEveryWhere, mainInfoItem.id, miniItem + i * GameCanvas.hText);
			}
		}
		if (GameCanvas.currentScreen.setCurTypetab(1))
		{
			base.paint(g);
			if (listInfo.cmxLim > 0)
			{
				scrInfo.paint(g);
			}
		}
	}

	public void paintAttPlusBuff(mGraphics g, string text, int id, int y)
	{
		int num = 0;
		for (int i = 0; i < GameScreen.player.vecBuffCur.size(); i++)
		{
			MainBuff mainBuff = (MainBuff)GameScreen.player.vecBuffCur.elementAt(i);
			if (mainBuff.vecInfoAtt.size() <= 0)
			{
				continue;
			}
			for (int j = 0; j < mainBuff.vecInfoAtt.size(); j++)
			{
				MainInfoItem mainInfoItem = (MainInfoItem)mainBuff.vecInfoAtt.elementAt(j);
				if (mainInfoItem.id == id)
				{
					num += mainInfoItem.value;
					break;
				}
			}
		}
		for (int k = 0; k < GameScreen.player.vecAllInfoParty.size(); k++)
		{
			MainInfoItem mainInfoItem2 = (MainInfoItem)GameScreen.player.vecAllInfoParty.elementAt(k);
			if (mainInfoItem2.id == id)
			{
				num += mainInfoItem2.value;
				break;
			}
		}
		if (num != 0)
		{
			int width = mFont.tahoma_7_white.getWidth(text);
			string st = string.Empty + MainItem.strGetPercent(num, MainItem.mNameAttributes[id].ispercent);
			if (num > 0)
			{
				st = "+" + MainItem.strGetPercent(num, MainItem.mNameAttributes[id].ispercent);
				mFont.tahoma_7_green.drawString(g, st, miniItem * 2 + width, y, 0);
			}
			else
			{
				mFont.tahoma_7_red.drawString(g, st, miniItem * 2 + width, y, 0);
			}
		}
	}

	public void paintSelect(mGraphics g)
	{
		int num = miniItem / 2 + GameCanvas.hText * 3 / 2;
		int num2 = wCur;
		g.setColor(16446420);
		g.fillRect(num2 + miniItem / 2, num + IdSelect * hItem, 1, hItem);
		g.fillRect(num2 + wCur - miniItem / 2 - 1, num + IdSelect * hItem, 1, hItem);
		g.fillRect(num2 + miniItem / 2 + 1, num + IdSelect * hItem - 1, wCur - miniItem - 1, 1);
		g.fillRect(num2 + miniItem / 2 + 1, num + IdSelect * hItem + hItem, wCur - miniItem - 1, 1);
	}

	public override void update()
	{
		if (levelTab == 0)
		{
			listInfo.moveCamera();
			scrInfo.setYScrool(listInfo.cmx, listInfo.cmxLim);
		}
		else if (levelTab == 1)
		{
			listAttri.moveCamera();
			scrAttri.setYScrool(listAttri.cmx, listAttri.cmxLim);
		}
		if (timefocus > 0)
		{
			timefocus--;
		}
		if (xcur < xto && levelTab == 1)
		{
			xcur += speed;
			speed += 10;
			if (xcur > xto)
			{
				xcur = xto;
			}
		}
		if (xcur > xto && levelTab == 0)
		{
			xcur -= speed;
			speed += 10;
			if (xcur < xto)
			{
				xcur = xto;
			}
		}
	}

	public override void updatekey()
	{
		if (levelTab == 0)
		{
			if (GameCanvas.keyMove(1))
			{
				GameCanvas.ClearkeyMove(1);
				listInfo.setToX(listInfo.cmtoX - MainTab.wItem);
				lastCam = listInfo.cmtoX;
			}
			else if (GameCanvas.keyMove(3))
			{
				listInfo.setToX(listInfo.cmtoX + MainTab.wItem);
				lastCam = listInfo.cmtoX;
				GameCanvas.ClearkeyMove(3);
			}
			else if (GameCanvas.keyMove(0))
			{
				GameCanvas.currentScreen.setTypeTab(0);
				GameCanvas.ClearkeyMove(0);
			}
			else if (GameCanvas.keyMove(2))
			{
				GameCanvas.ClearkeyMove(2);
				levelTab = 1;
				xto = wCur;
				speed = 20;
				xcur = 0;
				initCmd();
			}
		}
		else if (levelTab == 1)
		{
			bool flag = false;
			if (GameCanvas.keyMove(1))
			{
				GameCanvas.ClearkeyMove(1);
				IdSelect--;
				flag = true;
			}
			else if (GameCanvas.keyMove(3))
			{
				GameCanvas.ClearkeyMove(3);
				IdSelect++;
				flag = true;
			}
			else if (GameCanvas.keyMove(0))
			{
				GameCanvas.ClearkeyMove(0);
				xto = 0;
				speed = 20;
				xcur = wCur;
				levelTab = 0;
				initCmd();
			}
			if (flag)
			{
				IdSelect = AvMain.resetSelect(IdSelect, Player.mAttribute.Length - 1, isreset: true);
				if (GameCanvas.isTouchNoOrPC())
				{
					listAttri.setToX((IdSelect + 1) * hItem - hCur / 2);
				}
			}
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		if (GameCanvas.isPointSelect(MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - MainTab.wTab / 4 * 3 / 2 - 6, MainTab.yTab + 7, MainTab.wTab / 4 * 3 - 20, 28))
		{
			GameCanvas.isPointerSelect = false;
			if (levelTab == 0)
			{
				levelTab = 1;
				xto = wCur;
				xcur = 0;
			}
			else if (levelTab == 1)
			{
				levelTab = 0;
				xto = 0;
				xcur = wCur;
			}
			speed = 20;
			initCmd();
		}
		if (levelTab == 0)
		{
			listInfo.update_Pos_UP_DOWN();
		}
		else if (levelTab == 1)
		{
			listAttri.update_Pos_UP_DOWN();
			if (GameCanvas.isPointerSelect && GameCanvas.isPoint(xCurBegin, yCurBegin, wCur, hCur))
			{
				int num = (GameCanvas.py - (yCurBegin + miniItem + GameCanvas.hText * 3 / 2) + listAttri.cmx) / hItem;
				if (num >= 0 && num < Player.mAttribute.Length)
				{
					IdSelect = num;
					timefocus = 5;
					if (Player.pointAttribute > 0)
					{
						cmdSetPoint.perform();
					}
				}
				GameCanvas.isPointerSelect = false;
			}
		}
		base.updatePointer();
	}

	public static void updateTabAttri(Main_Attribute[] att)
	{
		isshow = false;
		Player.mAttribute = att;
		isshow = true;
	}

	public override void updateChangeTabInfo()
	{
		if (levelTab == 0)
		{
			levelTab = 1;
		}
		else
		{
			levelTab = 0;
		}
		xto = wCur;
		speed = 20;
		xcur = 0;
		initCmd();
	}
}
