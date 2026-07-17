public class PaintListScreen : SubScreen
{
	public const sbyte DUNGONE_LIST = -4;

	public const sbyte EVENT_LIST = -3;

	public const sbyte FRIEND_LIST = -2;

	public const sbyte PARTY_LIST = -1;

	public const sbyte BLACK_LIST = 2;

	public const sbyte ID_GET_PAGE = 3;

	public const sbyte PLAYER_LIST_LV = 4;

	public const sbyte MEM_CLAN_LIST = 5;

	public const sbyte CLAN_LIST = 6;

	public const sbyte PLAYER_LIST_PVP = 7;

	public const sbyte PLAYER_LIST_WANTED = 9;

	public const sbyte TOP_CLAN_LEVEL_PLAYER = 11;

	public const sbyte TOP_CLAN_PVP = 12;

	public const sbyte TOP_CLAN_TRUY_NA = 13;

	public const sbyte TOP_CLAN_THANH_TICH = 14;

	public const sbyte LIST_MEM_FIGHT_CLAN = 15;

	public const sbyte LIST_MEM_TOP_PHAO = 16;

	public const sbyte LIST_MEM_FIGHT_SUPER_BOSS = 17;

	public string textrong = string.Empty;

	public int hContent;

	public mVector vecMenu = new mVector();

	public ListNew list;

	public InfoMemList memCur;

	public int timeShowFocus;

	public int miniItem = 5;

	public int idSelect;

	public int idCommand;

	public mVector vecPlayer;

	public string nameList = string.Empty;

	public iCommand cmdMenu;

	public iCommand cmdClose;

	public iCommand cmdInfoPlayer;

	public iCommand cmdChat;

	public iCommand cmdAddFriend;

	public iCommand cmdMove;

	public iCommand cmdMenuTouchPlayer;

	public int hBegin;

	public bool isLoad;

	public bool isDelEvent;

	public PaintListScreen(sbyte type, mVector vec, string name, int maxWSub, int H)
		: base(type)
	{
		vecPlayer = vec;
		textrong = T.textrong;
		nameList = name;
		wSub = MotherCanvas.w - 30;
		hItem = 34;
		if (wSub > maxWSub)
		{
			wSub = maxWSub;
		}
		hSub = H;
		if (hSub > MotherCanvas.h - GameCanvas.hCommand)
		{
			hSub = MotherCanvas.h - GameCanvas.hCommand;
		}
		xSub = MotherCanvas.hw - wSub / 2;
		ySub = MotherCanvas.hh - hSub / 2;
		hContent = hSub - GameCanvas.hCommand - 10 - iCommand.hButtonCmdNor;
		cmdMenu = new iCommand(T.cmdChucNang, 0, 0, this);
		cmdClose = new iCommand(T.close, 2, 0, this);
		cmdMove = new iCommand(T.dichchuyen, -1, 0, this);
		cmdMenuTouchPlayer = new iCommand(T.menu, 100, 0, this);
		vecMenu.removeAllElements();
		vecMenu.addElement(cmdMenu);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xSub + 20 + wSub - 40, ySub + GameCanvas.hCommand / 2 - 2 + 8, MainTab.fraCloseTab, string.Empty);
			right = cmdClose;
		}
		else
		{
			vecMenu.addElement(cmdClose);
		}
		idCommand = 0;
		list = new ListNew();
		setPosCmdNew(0, vecMenu);
		cmdInfoPlayer = new iCommand(T.info, 4, 0, this);
		cmdChat = new iCommand(T.chat, 5, 0, this);
		cmdAddFriend = new iCommand(T.addFriend, 6, 0, this);
		backCMD = cmdClose;
		okCMD = cmdMenuTouchPlayer;
		menuCMD = cmdMenu;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			if (memCur != null)
			{
				GlobalService.gI().Move_To_Player(0, memCur.id);
			}
			break;
		case 0:
			doMenu();
			break;
		case 1:
			doMenuTouchPlayer();
			break;
		case 2:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			break;
		case 4:
			if (memCur != null)
			{
				GameCanvas.gameScr.ShowInfoOtherPlayer(memCur.name);
			}
			break;
		case 5:
			if (memCur != null)
			{
				GameCanvas.chatTabScr.addNewChat(memCur.name, string.Empty, string.Empty, 0, isFocus: true);
				GameCanvas.chatTabScr.Show(GameCanvas.gameScr);
			}
			break;
		case 6:
			if (memCur != null)
			{
				GlobalService.gI().Friend(0, memCur.id);
			}
			break;
		case 100:
			doMenuTouchPlayer();
			break;
		}
	}

	public virtual void setCamera()
	{
		int limX = vecPlayer.size() * hItem - hContent + miniItem * 2;
		list = new ListNew(xSub, ySub + GameCanvas.hCommand, wSub, hContent, 0, 0, limX, isLim0: true);
	}

	public override void Show(MainScreen screen)
	{
		base.Show(screen);
		beginShow();
		updateInfo();
	}

	public virtual void beginShow()
	{
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		if (GameCanvas.currentScreen == GameCanvas.chatTabScr)
		{
			return;
		}
		GameCanvas.resetTrans(g);
		paintBg(g);
		g.setClip(xSub, ySub + GameCanvas.hCommand + miniItem, wSub, hContent - miniItem);
		g.saveCanvas();
		g.ClipRec(xSub, ySub + GameCanvas.hCommand + miniItem, wSub, hContent - miniItem);
		g.translate(0, -list.cmx);
		int num = xSub + 30;
		int num2 = ySub + GameCanvas.hCommand + 10;
		if (isLoad)
		{
			MsgDialog.fraImgWaiting.drawFrame(GameCanvas.gameTick / 6 % MsgDialog.fraImgWaiting.nFrame, xSub + wSub / 2, num2 + hItem, 0, 3, g);
		}
		else if (vecPlayer.size() == 0)
		{
			mFont.tahoma_7_black.drawString(g, textrong, xSub + wSub / 2, ySub + hSub / 2, 2);
		}
		else
		{
			if (idSelect >= 0 && (GameCanvas.isTouchNoOrPC() || timeShowFocus > 0))
			{
				paintSelect(g, num, num2 - 2, wSub - 40);
			}
			for (int i = 0; i < vecPlayer.size(); i++)
			{
				InfoMemList mem = (InfoMemList)vecPlayer.elementAt(i);
				paintInfo(g, mem, num, num2, i, wSub - 60);
				num2 += hItem;
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (vecMenu != null)
		{
			for (int j = 0; j < vecMenu.size(); j++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		if (right != null)
		{
			right.paint(g, right.xCmd, right.yCmd);
		}
	}

	public virtual void paintBg(mGraphics g)
	{
		paintPaper_UpDown(g, xSub, ySub, wSub, hSub, hSub);
		g.setColor(15972174);
		g.fillRoundRect(xSub + 20, ySub + GameCanvas.hCommand / 2 - 2, wSub - 40, 16, 4, 4);
		mFont.tahoma_7b_red.drawString(g, nameList, xSub + wSub / 2, ySub + GameCanvas.hCommand / 2, 2);
	}

	public virtual void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		xbegin -= 10;
		g.setColor(12629427);
		g.fillRect(xbegin + miniItem / 2, ybegin + idSelect * hItem, wFocus - miniItem / 2, hItem);
	}

	public virtual void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
	}

	public override void update()
	{
		list.moveCamera();
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		if (!GameCanvas.menuCur.isShowMenu && GameCanvas.currentDialog == null && timeShowFocus > 0)
		{
			timeShowFocus--;
		}
	}

	public override void updatekey()
	{
		if (vecMenu != null)
		{
			int num = vecMenu.size();
			if (GameCanvas.isTouchNoOrPC() && num > 0)
			{
				int num2 = idCommand;
				if (GameCanvas.keyMove(0))
				{
					idCommand--;
					GameCanvas.ClearkeyMove(0);
				}
				else if (GameCanvas.keyMove(2))
				{
					idCommand++;
					GameCanvas.ClearkeyMove(2);
				}
				idCommand = AvMain.resetSelect(idCommand, num - 1, isreset: false);
				if (num2 != idCommand && GameCanvas.isTouchNoOrPC())
				{
					for (int i = 0; i < num; i++)
					{
						iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
						if (i == idCommand)
						{
							iCommand2.isSelect = true;
						}
						else
						{
							iCommand2.isSelect = false;
						}
					}
				}
			}
		}
		bool flag = false;
		if (GameCanvas.keyMove(1))
		{
			idSelect--;
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			idSelect++;
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (flag)
		{
			idSelect = AvMain.resetSelect(idSelect, vecPlayer.size() - 1, isreset: false);
			list.setToX((idSelect + 1) * hItem - hContent / 2);
		}
		if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (vecMenu != null && idCommand < vecMenu.size())
			{
				((iCommand)vecMenu.elementAt(idCommand)).perform();
			}
		}
		updatekeyPC();
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		base.updatePointer();
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		if (!GameCanvas.isPointerSelect || vecPlayer.size() <= 0 || !GameCanvas.isPoint(xSub, ySub + GameCanvas.hCommand, wSub, hContent))
		{
			return;
		}
		GameCanvas.isPointerSelect = false;
		int num = (GameCanvas.py - (ySub + GameCanvas.hCommand) + list.cmx) / hItem;
		if (num < 0 || num >= vecPlayer.size())
		{
			return;
		}
		if (isDelEvent)
		{
			if (GameCanvas.px < xSub + 30 + (wSub - 60) + 15 && GameCanvas.px > xSub + 30 + (wSub - 60) - 15 && num > 0)
			{
				InfoMemList mem = (InfoMemList)vecPlayer.elementAt(num);
				delMem(mem);
			}
			else
			{
				idSelect = num;
				doMenuTouchPlayer();
				timeShowFocus = 5;
			}
		}
		else
		{
			idSelect = num;
			doMenuTouchPlayer();
			timeShowFocus = 5;
		}
	}

	public void updateInfo()
	{
		setCamera();
		if (!GameCanvas.isTouch)
		{
			if (vecPlayer.size() == 0)
			{
				idSelect = -1;
			}
			if (idSelect >= vecPlayer.size())
			{
				idSelect = 0;
			}
		}
	}

	public virtual void doMenuTouchPlayer()
	{
	}

	public virtual void doMenu()
	{
	}

	public virtual void delMem(InfoMemList mem)
	{
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdClose != null)
		{
			cmdClose.perform();
		}
		return false;
	}

	public void setClip(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(xSub, ySub + hBegin - 1, wSub, hContent - miniItem);
		g.saveCanvas();
		g.ClipRec(xSub, ySub + hBegin - 1, wSub, hContent - miniItem);
		g.translate(0, -list.cmx);
	}
}
