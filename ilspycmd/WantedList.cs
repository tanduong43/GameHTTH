public class WantedList : PaintListScreen
{
	public const sbyte TYPE_SEND_REFESH = 0;

	public const sbyte TYPE_VIEW_INFO = 1;

	public const sbyte TYPE_NHAN = 2;

	public const sbyte TYPE_VIEW_LIST_PHAT_LENH = 3;

	public const sbyte TYPE_VIEW_LIST_DA_NHAN = 4;

	public const sbyte TYPE_VIEW_INFO_PHAT_LENH = 5;

	public const sbyte ACTION_VIEW = 0;

	public const sbyte ACTION_CANCEL = 1;

	public const sbyte ACTION_NHAN_THUONG = 2;

	public const sbyte ACTION_REMOVE = 3;

	public static WantedList instance;

	private iCommand cmdNext;

	private iCommand cmdPre;

	private iCommand cmdDs;

	private iCommand cmdPhat;

	private iCommand cmdNhan;

	private mVector vecCmd = new mVector();

	private sbyte page;

	private int countPage;

	private int listSize;

	private int wDsCmd;

	private int idDsCmdSelected;

	public WantedList(sbyte type, mVector vec)
		: base(type, vec, string.Empty, 190, 180)
	{
		if (type == 0)
		{
			nameList = T.nameWantedList;
		}
		else if (type == 3)
		{
			nameList = T.nameWantedListDaPhat;
		}
		page = 0;
		hItem = 42;
		textrong = T.danhSachTrong;
		SetListSize();
		SetIdListCmdSelect();
		cmdNext = new iCommand(T.nextpage, 12, this);
		cmdPre = new iCommand(T.prepage, 13, this);
		cmdInfoPlayer.indexMenu = 20;
		cmdDs = new iCommand(T.danhsach, 14, this);
		cmdPhat = new iCommand(T.daPhat, 15, this);
		cmdNhan = new iCommand(T.daNhan, 16, this);
		vecCmd.addElement(cmdDs);
		vecCmd.addElement(cmdPhat);
		vecCmd.addElement(cmdNhan);
		if (GameMidlet.DEVICE != 0)
		{
			wDsCmd = 100;
			xSub += wDsCmd / 2;
			setPosDsCmd(xSub - wDsCmd + 19, ySub + 17);
			setDsCmdSelected();
		}
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xSub + 20 + wSub - 34, ySub - 10, MainTab.fraCloseTab, string.Empty);
			right = cmdClose;
		}
		setPosVecMenu(vecMenu);
		if (Interface_Game.mImgPvPNew == null)
		{
			Interface_Game.loadImgPvPNew();
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		bool flag = false;
		int num = page;
		switch (index)
		{
		case 12:
			page++;
			page = (sbyte)AvMain.resetSelect(page, countPage - 1, isreset: false);
			if (num == page)
			{
				GameCanvas.Start_Normal_DiaLog(T.dialogLastPage, new mVector(), isCmdClose: true);
			}
			else
			{
				flag = true;
			}
			break;
		case 13:
			page--;
			page = (sbyte)AvMain.resetSelect(page, countPage - 1, isreset: false);
			if (num == page)
			{
				GameCanvas.Start_Normal_DiaLog(T.dialogFirstPage, new mVector(), isCmdClose: true);
			}
			else
			{
				flag = true;
			}
			break;
		case 14:
			GlobalService.gI().Send_Wanted_Poster(0, -1);
			break;
		case 15:
			GlobalService.gI().Send_Wanted_Poster(3, -1);
			break;
		case 16:
			GlobalService.gI().Send_Wanted_Poster_Action(4, -1, 0);
			break;
		case 20:
			if (type == 0)
			{
				GlobalService.gI().Send_Wanted_Poster_Action(1, (short)memCur.id, 0);
			}
			else if (type == 3)
			{
				GlobalService.gI().Send_Wanted_Poster_Action(5, (short)memCur.id, 0);
			}
			return;
		}
		if (flag)
		{
			idSelect = 0;
			SetListSize();
			updateInfo();
		}
		else
		{
			base.commandPointer(index, subIndex);
		}
	}

	public override void doMenuTouchPlayer()
	{
		if (vecPlayer.size() != 0)
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect + page * 10);
			if (memCur != null)
			{
				mVector mVector2 = new mVector();
				mVector2.addElement(cmdInfoPlayer);
				GameCanvas.menu.startAt(mVector2, 2, memCur.charShow.name);
			}
		}
	}

	public override void doMenu()
	{
		mVector mVector2 = new mVector();
		string name = T.menu;
		if (vecPlayer.size() > 0)
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect + page * 10);
			if (memCur != null)
			{
				if (GameMidlet.DEVICE == 0)
				{
					mVector2.addElement(cmdInfoPlayer);
				}
				name = memCur.charShow.name;
			}
		}
		mVector2.addElement(cmdNext);
		mVector2.addElement(cmdPre);
		if (GameMidlet.DEVICE == 0)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				if (i != idDsCmdSelected)
				{
					mVector2.addElement(vecCmd.elementAt(i));
				}
			}
		}
		GameCanvas.menu.startAt(mVector2, 2, name);
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
			mFont.tahoma_7_black.drawString(g, textrong, xSub + wSub / 2, ySub + (hSub - 20) / 2, 2);
		}
		else
		{
			if (idSelect >= 0)
			{
				paintSelect(g, num, num2 - 2, wSub - 40);
			}
			for (int i = 0; i < 10; i++)
			{
				if (num2 - list.cmx + hItem >= GameCanvas.hCommand + ySub && num2 - list.cmx - hItem <= GameCanvas.hCommand + hContent + ySub)
				{
					InfoMemList mem = (InfoMemList)vecPlayer.elementAt(i + page * 10);
					paintInfo(g, mem, num, num2, i, wSub - 60);
				}
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
		if (GameMidlet.DEVICE != 0)
		{
			for (int k = 0; k < vecCmd.size(); k++)
			{
				iCommand iCommand3 = (iCommand)vecCmd.elementAt(k);
				iCommand3.paint(g, iCommand3.xCmd, iCommand3.yCmd);
			}
		}
		if (right != null)
		{
			right.paint(g, right.xCmd, right.yCmd);
		}
	}

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		if (mem == null)
		{
			return;
		}
		paintBgMemListPhatLenh(g, xpaint + 1, ypaint, wSub - 61, 39);
		int num = 42;
		xpaint += num;
		mFont.tahoma_7b_black.drawString(g, mem.charShow.name, xpaint, ypaint + 1, 0);
		if (type == 0)
		{
			mFont.tahoma_7_black.drawString(g, T.wantedBy + ": " + mem.name, xpaint, ypaint + GameCanvas.hText - 2, 0);
		}
		else if (type == 3)
		{
			mFont.tahoma_7_black.drawString(g, T.nhanQuest + ": " + mem.namePlayerNhan, xpaint, ypaint + GameCanvas.hText - 2, 0);
		}
		AvMain.fraMoney.drawFrame(0, xpaint - 1, ypaint + GameCanvas.hText * 2 - 6, 0, 0, g);
		mFont.tahoma_7_black.drawString(g, AvMain.getDotNumber(mem.charShow.wanted), xpaint + 12, ypaint + GameCanvas.hText * 2 - 5, 0);
		xpaint -= num;
		if (type == 3 && mem.isWantedSuccess == 1)
		{
			g.drawImage(AvMain.imgComplete, xpaint + (wSub - 60) / 2 - 7, ypaint + 1, 0);
		}
		if (type == 0)
		{
			if (mem.typeOnline == 0)
			{
				g.drawRegion(Interface_Game.mImgPvPNew[3], 0, 14, 7, 7, 0, xpaint + wSub - 60 - 10 - 1, ypaint + GameCanvas.hText * 2 - 3, 0);
			}
			else
			{
				g.drawRegion(Interface_Game.mImgPvPNew[3], 0, 7, 7, 7, 0, xpaint + wSub - 60 - 10 - 1, ypaint + GameCanvas.hText * 2 - 3, 0);
			}
		}
		MainObject.paintHeadEveryWhere(g, mem.charShow.head, mem.charShow.hair, mem.charShow.hat, xpaint + 21, ypaint + hItem / 2 + 35 - 4, 0);
		g.drawRegion(Interface_Game.imgIconMPHP, 0, 22, 10, 11, 0, xpaint + 11, ypaint + GameCanvas.hText * 2 - 3, 0);
		mFont.tahoma_7_black.drawString(g, mem.charShow.Lv + string.Empty, xpaint + 24, ypaint + GameCanvas.hText * 2 - 5, 0);
	}

	public override void paintBg(mGraphics g)
	{
		if (GameMidlet.DEVICE == 0)
		{
			AvMain.paintBG_AChi(g, xSub, ySub - 20, wSub, hSub + 20, 1);
		}
		else
		{
			AvMain.paintBg_WantedList(g, xSub - wDsCmd / 2, ySub - 20, wSub, hSub + 20, 1, 100);
		}
		mFont.tahoma_7b_brown.drawString(g, nameList, xSub + wSub / 2, ySub + GameCanvas.hCommand / 2 + 3, 2);
		mFont.tahoma_7b_black.drawString(g, T.page + (page + 1) + "/" + countPage, xSub + wSub / 2, ySub + hSub - GameCanvas.hCommand / 2 - 20, 2);
	}

	public override void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(11936290);
		g.drawRect(xbegin, ybegin + idSelect * hItem + 1, wSub - 60, hItem - 2);
	}

	public override void updatekey()
	{
		base.updatekey();
		idSelect = AvMain.resetSelect(idSelect, listSize - 1, isreset: false);
	}

	public override void updatePointer()
	{
		base.updatePointer();
		if (GameMidlet.DEVICE == 0)
		{
			return;
		}
		for (int i = 0; i < vecCmd.size(); i++)
		{
			if (i != idDsCmdSelected)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.updatePointer();
			}
		}
	}

	public override void setCamera()
	{
		int limX = listSize * hItem - hContent + miniItem * 2;
		list = new ListNew(xSub, ySub + GameCanvas.hCommand, wSub, hContent, 0, 0, limX, isLim0: true);
		list.setToX((idSelect + 1) * hItem - hContent / 2);
	}

	private void SetListSize()
	{
		listSize = vecPlayer.size() - page * 10;
		listSize = AvMain.resetSelect(listSize, (listSize < 10) ? listSize : 10, isreset: false);
		countPage = vecPlayer.size() / 10 + ((vecPlayer.size() % 10 != 0) ? 1 : 0);
		if (countPage == 0)
		{
			countPage = 1;
		}
	}

	private void setPosDsCmd(int xbegin, int ybegin)
	{
		int num = ybegin;
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			iCommand2.setPos(xbegin + 39, num + 11, AvMain.imgKhung[0], iCommand2.caption, 2);
			num += 30;
		}
	}

	public override void setPosCmdNew(int yplus, mVector vecMenu)
	{
		xSub -= wDsCmd / 2;
		base.setPosCmdNew(yplus, vecMenu);
		xSub += wDsCmd / 2;
	}

	private void setDsCmdSelected()
	{
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			if (i == idDsCmdSelected)
			{
				iCommand2.frameCmd = 1;
				iCommand2.isDisplay = true;
			}
			else
			{
				iCommand2.frameCmd = 0;
				iCommand2.isDisplay = false;
			}
		}
	}

	private void SetIdListCmdSelect()
	{
		switch (type)
		{
		case 0:
			idDsCmdSelected = 0;
			break;
		case 3:
			idDsCmdSelected = 1;
			break;
		case 4:
			idDsCmdSelected = 2;
			break;
		}
	}
}
