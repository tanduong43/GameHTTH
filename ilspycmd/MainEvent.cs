public class MainEvent : PaintListScreen
{
	public iCommand cmdSetEvent;

	public iCommand cmdAddfriend;

	public iCommand cmdAddParty;

	public iCommand cmdDelEvent;

	public iCommand cmdTrade;

	public iCommand cmdAcceptAddParty;

	public iCommand cmdInfoEnemy;

	public iCommand cmdTroChuyen;

	public iCommand cmdAcceptClan;

	public iCommand cmdAcceptFightClan;

	public iCommand cmdCancelFightClan;

	public iCommand cmdAcceptSudo;

	public static iCommand cmdFight;

	public static FrameImage fraEvent;

	public static mImage imgNew;

	public static int wShort = 120;

	public static int hShort = 40;

	public MainEvent(sbyte type, mVector vec)
		: base(type, vec, T.cmdEvent, 180, 180)
	{
		hContent = hSub - GameCanvas.hCommand - 10;
		if (!GameCanvas.isTouch)
		{
			hContent -= iCommand.hButtonCmdNor;
		}
		setCamera();
		cmdSetEvent = new iCommand(T.view, 15, this);
		cmdAddfriend = new iCommand(T.addFriend, 16, this);
		cmdAddParty = new iCommand(T.chapnhan, 17, this);
		cmdDelEvent = new iCommand(T.del, 18, this);
		cmdFight = new iCommand(T.chapnhan, 19, this);
		cmdTrade = new iCommand(T.chapnhan, 20, this);
		cmdAcceptAddParty = new iCommand(T.chapnhan, 21, this);
		cmdInfoEnemy = new iCommand(T.info, 22, this);
		cmdTroChuyen = new iCommand(T.view, 23, this);
		cmdAcceptClan = new iCommand(T.chapnhan, 24, this);
		cmdAcceptFightClan = new iCommand(T.chapnhan, 25, 0, this);
		cmdCancelFightClan = new iCommand(T.cancel, 25, 1, this);
		cmdAcceptSudo = new iCommand(T.chapnhan, 26, this);
		vecMenu.removeAllElements();
		if (!GameCanvas.isTouch)
		{
			vecMenu.addElement(cmdSetEvent);
		}
		if (GameCanvas.isTouch)
		{
			right = cmdClose;
		}
		else
		{
			vecMenu.addElement(cmdClose);
		}
		backCMD = cmdClose;
		okCMD = cmdSetEvent;
		idCommand = 0;
		isDelEvent = true;
		setPosCmdNew(0, vecMenu);
	}

	public override void beginShow()
	{
		if (!GameCanvas.isTouch)
		{
			idCommand = 0;
		}
		setPosCmdNew(0, vecMenu);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 2:
			GameScreen.setNumMess();
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			return;
		case 15:
			if (idSelect >= 0 && idSelect < vecPlayer.size())
			{
				memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			}
			if (memCur != null)
			{
				memCur.setAction();
			}
			return;
		case 16:
			if (memCur != null)
			{
				GlobalService.gI().Friend(3, memCur.id);
				vecPlayer.removeElement(memCur);
				updateInfo();
				GameCanvas.end_Dialog();
			}
			return;
		case 17:
			if (memCur != null)
			{
				GlobalService.gI().Party(4, (short)memCur.id);
				vecPlayer.removeElement(memCur);
				updateInfo();
				GameCanvas.end_Dialog();
			}
			return;
		case 18:
			delMem(memCur);
			GameCanvas.end_Dialog();
			return;
		case 19:
			if (memCur != null)
			{
				GlobalService.gI().Fight(1, (short)memCur.id, memCur.typeFight);
				vecPlayer.removeElement(memCur);
				updateInfo();
				GameCanvas.end_Dialog();
			}
			return;
		case 20:
			if (memCur != null)
			{
				GlobalService.gI().Trade(6, (short)memCur.id, 0, 1, string.Empty);
				vecPlayer.removeElement(memCur);
				updateInfo();
				GameCanvas.end_Dialog();
			}
			return;
		case 21:
			if (memCur != null)
			{
				GlobalService.gI().Party(6, (short)memCur.id);
				vecPlayer.removeElement(memCur);
				updateInfo();
				GameCanvas.end_Dialog();
			}
			return;
		case 22:
			if (memCur != null)
			{
				GameCanvas.gameScr.ShowInfoOtherPlayer(memCur.name);
				MsgOtherCharInfo.infoFight = memCur;
			}
			break;
		case 23:
			if (memCur != null && GameCanvas.currentScreen != GameCanvas.chatTabScr)
			{
				GameCanvas.chatTabScr.addNewChat(memCur.name, string.Empty, string.Empty, 0, isFocus: true);
				GameCanvas.chatTabScr.Show(GameCanvas.currentScreen);
			}
			return;
		case 24:
			if (memCur != null)
			{
				GlobalService.gI().Clan_CMD(12, string.Empty, (short)memCur.id, 0);
			}
			break;
		case 25:
			if (memCur != null)
			{
				GlobalService.gI().Clan_Fight((sbyte)((subIndex == 0) ? 1 : 2), (short)memCur.id, memCur.typeFight);
				GameCanvas.end_Dialog();
			}
			break;
		case 26:
			if (memCur != null)
			{
				GlobalService.gI().Sudo_CMD(18, memCur.name, (short)memCur.id, 0);
			}
			break;
		}
		base.commandPointer(index, subIndex);
	}

	public override void doMenuTouchPlayer()
	{
		cmdSetEvent.perform();
	}

	public override void doMenu()
	{
		if (vecPlayer.size() == 0 || idSelect < 0 || idSelect >= vecPlayer.size())
		{
			return;
		}
		memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
		if (memCur != null && memCur.id != GameScreen.player.ID)
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(cmdSetEvent);
			if (idSelect != 0)
			{
				mVector2.addElement(cmdDelEvent);
			}
			GameCanvas.menu.startAt(mVector2, 2, memCur.name);
		}
	}

	public void setMemCur(InfoMemList mem)
	{
		memCur = mem;
	}

	public static void paintShort(mGraphics g, int x, int y, InfoMemList mem)
	{
		if (mem != null)
		{
			int num = x - wShort / 2;
			AvMain.paintRect(g, num, y, wShort, hShort, 1, 2);
			fraEvent.drawFrame(mem.indexIconEvent, num + 15, y + hShort / 2, 0, 3, g);
			mFont.tahoma_7b_black.drawString(g, mem.name, num + 30, y + GameCanvas.hText / 2, 0);
			mFont.tahoma_7_black.drawString(g, mem.info, num + 35, y + GameCanvas.hText / 2 * 3, 0);
		}
	}

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		if (mem != null)
		{
			string name = mem.name;
			mFont.tahoma_7b_black.drawString(g, name, xpaint + 25, ypaint, 0);
			mFont.tahoma_7_black.drawString(g, mem.info, xpaint + 30, ypaint + GameCanvas.hText, 0);
			fraEvent.drawFrame(mem.indexIconEvent, xpaint + 10, ypaint + hItem / 2, 0, 3, g);
			if (mem.isNew)
			{
				g.drawImage(imgNew, xpaint + 10 + 8, ypaint + hItem / 2 - 8, 3);
			}
			if (i < vecPlayer.size() - 1)
			{
				paintLine(g, xpaint, ypaint - 6, wsub);
			}
			if (i != 0 && GameCanvas.isTouch)
			{
				g.drawImage(AvMain.imgIconDel, xpaint + wsub, ypaint + hItem / 2, 3);
			}
		}
	}

	public void paintLine(mGraphics g, int xpaint, int ypaint, int wsub)
	{
		g.setColor(AvMain.colorMenu[4]);
		g.fillRect(xpaint + 4, ypaint + 3 + hItem - 1, wsub - 8, 2);
		g.fillRect(xpaint + 4 + 1, ypaint + 3 + hItem - 2, wsub - 8 - 2, 4);
	}

	public override void delMem(InfoMemList mem)
	{
		if (mem != null)
		{
			if (mem.isNew)
			{
				mem.isNew = false;
			}
			if (mem.typeEvent == 2)
			{
				GameCanvas.chatTabScr.checkRemoveTab(mem.name);
			}
			vecPlayer.removeElement(mem);
			updateInfo();
			if (GameCanvas.isTouchNoOrPC())
			{
				idSelect = AvMain.resetSelect(idSelect, vecPlayer.size() - 1, isreset: false);
			}
			GameScreen.setNumMess();
		}
	}
}
