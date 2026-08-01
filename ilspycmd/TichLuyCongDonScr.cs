public class TichLuyCongDonScr : PaintListScreen
{
	public const sbyte TYPE_LIST = 0;

	public const sbyte TYPE_NHAN = 1;

	public const sbyte TYPE_DA_NHAN = 2;

	public static TichLuyCongDonScr instance;

	private mVector vecCmd = new mVector();

	private int wCmdNhan;

	private Scroll scroll = new Scroll();

	private int wPaintQua = 93;

	public TichLuyCongDonScr(mVector vec)
		: base(0, vec, T.tichLuyCongDon, 200, 180)
	{
		if (!GameCanvas.isTouch)
		{
			if (wSub > 160)
			{
				wSub = 160;
			}
			xSub = MotherCanvas.hw - wSub / 2;
		}
		hBegin = 35;
		hItem = 53;
		hContent = hSub - hBegin - 10;
		vecMenu.removeAllElements();
		if (!GameCanvas.isTouch)
		{
			vecMenu.addElement(cmdMenu);
		}
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xSub + 20 + wSub - 36, ySub + 16 + 8, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			vecMenu.addElement(cmdClose);
		}
		cmdInfoPlayer.indexMenu = 7;
		cmdInfoPlayer.subIndex = -1;
		for (int i = 0; i < vecPlayer.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(i);
			iCommand iCommand2;
			if (infoMemList.quaNapThe.statusNhan == 0)
			{
				iCommand2 = new iCommand(T.view, 7, i, this);
			}
			else if (infoMemList.quaNapThe.statusNhan == 1)
			{
				iCommand2 = new iCommand(T.nhanQuest, 8, i, this);
				iCommand2.setTypeGreen();
			}
			else
			{
				iCommand2 = new iCommand(T.daNhan, 9, i, this);
				iCommand2.setTypeRed();
			}
			iCommand2.setPos(xSub + 150, ySub + hBegin + hItem * i + 28 + 11, AvMain.fraCmdNhanNapThe, iCommand2.caption);
			vecCmd.addElement(iCommand2);
			if (wCmdNhan == 0)
			{
				wCmdNhan = iCommand2.wimgCmd;
			}
		}
		setPosVecMenu(vecMenu);
		scroll.setInfo(xSub + wSub - 20 + miniItem, ySub + hBegin - 1, hContent - miniItem, 8809550);
	}

	public void setCmdDaNhanIndex(sbyte id)
	{
		iCommand iCommand2 = (iCommand)vecCmd.elementAt(id);
		iCommand2.caption = T.daNhan;
		iCommand2.indexMenu = 9;
		iCommand2.setTypeGreen();
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 8)
		{
			idSelect = subIndex;
			InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(idSelect);
			GlobalService.gI().Send_Nhan_TichLuyCongDon(1, (short)infoMemList.quaNapThe.cost);
		}
		base.commandPointer(index, subIndex);
	}

	public override void doMenuTouchPlayer()
	{
		if (vecPlayer.size() != 0)
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			if (memCur != null)
			{
				mVector mVector2 = new mVector();
				mVector2.addElement(cmdInfoPlayer);
				GameCanvas.menu.startAt(mVector2, 2, T.info);
			}
		}
	}

	public override void doMenu()
	{
		mVector mVector2 = new mVector();
		string menu = T.menu;
		if (vecPlayer.size() > 0)
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			if (memCur != null && !GameCanvas.isTouch)
			{
				iCommand o = (iCommand)vecCmd.elementAt(idSelect);
				mVector2.addElement(o);
			}
		}
		GameCanvas.menu.startAt(mVector2, 2, menu);
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
		cmdClose.paint(g, cmdClose.xCmd, cmdClose.yCmd);
		int num = xSub + 20;
		int num2 = ySub + hBegin;
		setClip(g);
		if (isLoad)
		{
			MsgDialog.fraImgWaiting.drawFrame(GameCanvas.gameTick / 6 % MsgDialog.fraImgWaiting.nFrame, xSub + wSub / 2, num2 + hItem, 0, 3, g);
		}
		else
		{
			if (idSelect >= 0)
			{
				paintSelect(g, num, num2 - 1, wSub - 40);
			}
			for (int i = 0; i < vecPlayer.size(); i++)
			{
				if (num2 - list.cmx + hItem >= hBegin + ySub && num2 - list.cmx - hItem <= hBegin + hContent + ySub)
				{
					InfoMemList mem = (InfoMemList)vecPlayer.elementAt(i);
					paintInfo(g, mem, num, num2, i, wSub - 40);
				}
				num2 += hItem;
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (list.cmxLim > 0)
		{
			scroll.paint(g);
		}
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

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		if (mem != null)
		{
			g.setColor(14203529);
			g.fillRect(xpaint, ypaint, wsub, 59);
			g.setColor(11506025);
			g.fillRect(xpaint + 2, ypaint + 2, wsub - 4, 22);
			AvMain.FontBorderColor(g, T.vnd + " :", xSub + wSub / 2 - 2, ypaint + 2, 1, 1, 7);
			mFont.tahoma_7b_black.drawString(g, Interface_Game.ConvertNumToStr(mem.quaNapThe.rubyDaNap) + "K", xSub + wSub / 2 + 2, ypaint + 2, 0);
			ypaint += 11;
			AvMain.Font3dColor(g, T.tichLuyNap + " " + Interface_Game.ConvertNumToStr(mem.quaNapThe.cost) + "K " + T.vnd, xpaint + wsub / 2, ypaint + 2, 2, 5);
			if (GameCanvas.isTouch)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.paintCmd1Frame(g, iCommand2.xCmd, iCommand2.yCmd);
			}
			int num = ((!GameCanvas.isTouch) ? ((wsub - wPaintQua) / 2) : ((wsub - wPaintQua - wCmdNhan - 5) / 2));
			if (num < 0)
			{
				num = 0;
			}
			int num2 = 29;
			if (idSelect != i || ypaint - list.cmx + 16 <= ySub + hBegin || ypaint - list.cmx + 16 + num2 >= ySub + hBegin + hContent - miniItem)
			{
				mem.quaNapThe.isRun = false;
				mem.quaNapThe.paint(g, xpaint + num, ypaint + 2 + 14);
				return;
			}
			g.setClip(xpaint + num, ypaint, wPaintQua + 1, hItem);
			g.saveCanvas();
			g.ClipRec(xpaint + num, ypaint, wPaintQua + 1, hItem);
			mem.quaNapThe.isRun = true;
			mem.quaNapThe.paint(g, xpaint + num, ypaint + 2 + 14);
			g.restoreCanvas();
			setClip(g);
		}
	}

	public override void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(11936290);
		g.drawRect(xbegin - 1, ybegin + idSelect * hItem, wFocus + 1, 60);
	}

	public override void paintBg(mGraphics g)
	{
		paintPaper(g, xSub, ySub, wSub, hSub, wSub, AvMain.PAPER_NORMAL);
		g.setColor(15972174);
		g.fillRoundRect(xSub + 20, ySub + 16, wSub - 40, 16, 4, 4);
		AvMain.FontBorderColor(g, nameList, xSub + wSub / 2, ySub + 18, 2, 6, 5);
	}

	public override void update()
	{
		base.update();
		for (int i = 0; i < vecPlayer.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(i);
			infoMemList.quaNapThe.update();
		}
		scroll.setYScrool(list.cmx, list.cmxLim);
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isTouch)
		{
			if (left != null)
			{
				if (left.isPosCmd())
				{
					left.updatePointer();
				}
				else if (GameCanvas.isPointSelect(0, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
				{
					left.perform();
				}
			}
			if (right != null)
			{
				if (right.isPosCmd())
				{
					right.updatePointer();
				}
				else if (GameCanvas.isPointSelect(MotherCanvas.w - GameCanvas.wCommand * 2, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
				{
					right.perform();
				}
			}
			if (center != null)
			{
				if (center.isPosCmd())
				{
					center.updatePointer();
				}
				else if (GameCanvas.isPointSelect(MotherCanvas.hw - GameCanvas.wCommand, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
				{
					center.perform();
				}
			}
		}
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		for (int j = 0; j < vecCmd.size(); j++)
		{
			int yBtn = ySub + hBegin + hItem * j + 28 - list.cmx;
			iCommand iCommand3 = (iCommand)vecCmd.elementAt(j);
			iCommand3.updatePointerNapThe(xSub + 150, yBtn);
		}
		if (!GameCanvas.isPointerSelect || vecPlayer.size() <= 0 || !GameCanvas.isPoint(xSub, ySub + hBegin, wSub, hContent))
		{
			return;
		}
		GameCanvas.isPointerSelect = false;
		int num = (GameCanvas.py - (ySub + hBegin) + list.cmx) / hItem;
		if (num >= 0 && num < vecPlayer.size())
		{
			if (idSelect != num)
			{
				idSelect = num;
			}
			else
			{
				doMenuTouchPlayer();
			}
		}
	}

	public override void setCamera()
	{
		int limX = vecPlayer.size() * hItem - hContent + 2;
		list = new ListNew(xSub + 20, ySub + GameCanvas.hCommand, wSub - 40, hContent, 0, 0, limX, isLim0: true);
		list.setToX((idSelect + 1) * hItem - hContent / 2);
	}

	public void setUpdateCostRuby(short cost, int ruby)
	{
		for (int i = 0; i < vecPlayer.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(i);
			if (infoMemList.quaNapThe.cost == cost)
			{
				infoMemList.quaNapThe.rubyDaNap = ruby;
				break;
			}
		}
	}
}
