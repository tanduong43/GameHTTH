using System;

public class TabSuDo : MainTab
{
	public const sbyte SUDO_LIST = 2;

	public const sbyte SUDO_INFO = 3;

	public static TabSuDo instance;

	private iCommand cmdSetPoint;

	private InputDialog input;

	private ListNew listInfo;

	private ListNew listSuDo;

	private int lastCam;

	private int xBeginInfo;

	private Scroll scrInfo = new Scroll();

	private Scroll scrAttri = new Scroll();

	public const int LEVEL_SUDO = 1;

	public const int LEVEL_CANHAN = 0;

	private int xto;

	private int xcur;

	private int speed = 20;

	private int hItem;

	private int timefocus;

	private int[] mNumAttri = new int[3] { 1, 2, 10 };

	private int idSelect;

	public static mVector vecSudo = new mVector();

	private int wPaintQua = 93;

	public static mVector vecInfoCanhan = new mVector();

	public static bool isshow = true;

	public TabSuDo(string name)
	{
		instance = this;
		nameTab = name;
		xBeginInfo = wCur;
		initCmd();
		listInfo = new ListNew();
		indexIconTab = 2;
	}

	public override void beginFocus()
	{
		int limX = vecInfoCanhan.size() * GameCanvas.hText - hCur + miniItem * 2;
		listInfo = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrInfo.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
		listInfo.cmx = lastCam;
		listInfo.cmtoX = lastCam;
		hItem = 48;
		limX = vecSudo.size() * hItem - hCur + miniItem * 2 + GameCanvas.hText * 3 / 2;
		listSuDo = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrAttri.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (IdSelect < 0 || IdSelect >= vecSudo.size() || Player.mAttribute[IdSelect].value >= 80)
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
				GameCanvas.Start_Normal_DiaLog_New(T.nhaptiemnang + T.mAttribute[IdSelect] + "?", mVector2, isCmdClose: true, T.tabQheSudo);
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
			hCur = MainTab.hTab - 32;
			yCurBegin = MainTab.yTab + 32;
			GlobalService.gI().Send_Sudo(2);
		}
		else
		{
			center = null;
			okCMD = null;
			hCur = MainTab.hTab - 32 - 80;
			yCurBegin = MainTab.yTab + 32 + 80;
			GlobalService.gI().Send_Sudo(3);
		}
		beginFocus();
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
				AvMain.FontBorderColor(g, T.tabQheSudo, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 15, MainTab.yTab + 9, 0, 6, 5);
				mFont.tahoma_7b_black.drawString(g, T.tabCanhan, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - 15, MainTab.yTab + 9, 1);
			}
			else if (levelTab == 0)
			{
				g.fillRoundRect(MainTab.xTab + 22 + (MainTab.wTab - 22) / 2, MainTab.yTab + 7, MainTab.wTab / 4 * 3 / 2, 16, 4, 4);
				idx = 1;
				mFont.tahoma_7b_black.drawString(g, T.tabQheSudo, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 15, MainTab.yTab + 9, 0);
				AvMain.FontBorderColor(g, T.tabCanhan, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - 15, MainTab.yTab + 9, 1, 6, 5);
				AvMain.paintRect(g, xCurBegin, yCurBegin, wCur, hCur - miniItem, 0, 4);
				int num = MainTab.yTab + 32;
				AvMain.paintRect(g, xCurBegin + wCur / 2 - 30, num, 60, 66, 0, 4);
				GameScreen.player.paintCharShow(g, xCurBegin + wCur / 2, num + 40 + GameScreen.player.hOne / 4 + 5, 0, isNhip: true);
				mFont.tahoma_7b_black.drawString(g, GameScreen.player.name, xCurBegin + wCur / 2, num + 68, 2);
			}
		}
		else
		{
			mFont.tahoma_7b_black.drawString(g, T.tabQheSudo, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 15, MainTab.yTab + 9, 0);
			mFont.tahoma_7b_black.drawString(g, T.tabCanhan, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 - 15, MainTab.yTab + 9, 1);
		}
		AvMain.fraTwoTab.drawFrame(idx, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2, MainTab.yTab + 9 + 6, 0, 3, g);
		if (Player.pointAttribute > 0 && GameCanvas.gameTick % 10 < 8)
		{
			g.drawImage(MainEvent.imgNew, MainTab.xTab + 22 + (MainTab.wTab - 22) / 2 + 9, MainTab.yTab + 9, 3);
		}
		GameCanvas.resetTrans(g);
		g.setClip(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		g.saveCanvas();
		g.ClipRec(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		g.translate(xCurBegin, yCurBegin);
		if (levelTab == 1)
		{
			g.translate(-xcur, -listSuDo.cmx);
			paintSuDo(g);
		}
		else if (levelTab == 0)
		{
			g.translate(-xcur, -listInfo.cmx);
			paintCaNhan(g);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
	}

	public void paintSuDo(mGraphics g)
	{
		if (GameCanvas.isTouchNoOrPC())
		{
			paintSelect(g);
		}
		int num = miniItem;
		int xpaint = wCur;
		if (isshow)
		{
			for (int i = 0; i < vecSudo.size(); i++)
			{
				InfoMemList mem = (InfoMemList)vecSudo.elementAt(i);
				paintInfo(g, mem, xpaint, num, i, wCur);
				num += hItem;
			}
		}
		if (GameCanvas.currentScreen.setCurTypetab(1))
		{
			base.paint(g);
			if (listSuDo.cmxLim > 0)
			{
				scrAttri.paint(g);
			}
		}
	}

	public void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		if (mem != null)
		{
			AvMain.paintRect(g, xpaint, ypaint, wsub - 1, 40, 0, 4);
			g.setColor(14203529);
			g.fillRect(xpaint + 45, ypaint + 2, wsub - 42 - 10, 14);
			mFont.tahoma_7b_black.drawString(g, mem.title, xpaint + 45 + (wsub - 52) / 2, ypaint + 3, 2);
			mFont.tahoma_7_white.drawString(g, mem.name, xpaint + 45 + (wsub - 52) / 2, ypaint + 22, 2);
			g.drawImage(AvMain.imgBorderIcon, xpaint + 4 + 16, ypaint + 4 + 16, 3);
			MainObject.paintHeadEveryWhere(g, mem.head, mem.hair, mem.hat, xpaint + 2 + 16, ypaint + 54, 0);
		}
	}

	public void paintCaNhan(mGraphics g)
	{
		for (int i = 0; i < vecInfoCanhan.size(); i++)
		{
			MainInfoItem mainInfoItem = (MainInfoItem)vecInfoCanhan.elementAt(i);
			string text = mainInfoItem.name;
			if (mainInfoItem.value != 0)
			{
				text = text + " " + mainInfoItem.value;
			}
			mFont.tahoma_7_white.drawString(g, text, miniItem, miniItem + i * GameCanvas.hText, 0);
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

	public void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(11936290);
		g.drawRect(xbegin - 1, ybegin + idSelect * hItem, wFocus + 1, 49);
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
			listSuDo.moveCamera();
			scrAttri.setYScrool(listSuDo.cmx, listSuDo.cmxLim);
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
				IdSelect = AvMain.resetSelect(IdSelect, vecSudo.size() - 1, isreset: true);
				if (GameCanvas.isTouchNoOrPC())
				{
					listSuDo.setToX((IdSelect + 1) * hItem - hCur / 2);
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
			listSuDo.update_Pos_UP_DOWN();
			if (GameCanvas.isPointerSelect && GameCanvas.isPoint(xCurBegin, yCurBegin, wCur, hCur))
			{
				int num = (GameCanvas.py - (yCurBegin + miniItem + GameCanvas.hText * 3 / 2) + listSuDo.cmx) / hItem;
				if (num >= 0 && num < vecSudo.size())
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
