public class MsgDanhHieu : SubScreen
{
	public string textrong = string.Empty;

	public int hContent;

	public mVector vecMenu = new mVector();

	public ListNew list;

	public InfoMemList memCur;

	public int timeShowFocus;

	public int miniItem = 5;

	public int idSelect;

	public int idCommand;

	public mVector vecDanhHieu;

	public string nameList = string.Empty;

	public iCommand cmdMenu;

	public iCommand cmdClose;

	public int hBegin;

	public bool isLoad;

	public bool isDelEvent;

	public int yPaintFirst;

	public static MsgDanhHieu instance;

	public bool show_danhhieu;

	public MsgDanhHieu()
		: base(-1)
	{
		vecDanhHieu = new mVector();
		textrong = "Chưa có danh hiệu";
		nameList = "Danh hiệu";
		wSub = MotherCanvas.w - 30;
		hItem = 40;
		if (wSub > 200)
		{
			wSub = 200;
		}
		hSub = 180;
		if (hSub > MotherCanvas.h - GameCanvas.hCommand)
		{
			hSub = MotherCanvas.h - GameCanvas.hCommand;
		}
		xSub = MotherCanvas.hw - wSub / 2;
		ySub = MotherCanvas.hh - hSub / 2;
		hContent = hSub - GameCanvas.hCommand - 10 - iCommand.hButtonCmdNor;
		cmdMenu = new iCommand(T.cmdChucNang, 0, this);
		cmdClose = new iCommand(T.close, 2, 0, this);
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
		backCMD = cmdClose;
		menuCMD = cmdMenu;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
		{
			mVector mVector2 = new mVector();
			DanhHieu danhHieu = (DanhHieu)vecDanhHieu.elementAt(idSelect);
			mVector2.addElement(new iCommand(show_danhhieu ? "Trở lại" : "Thông tin", 3, this));
			if (GameScreen.player != null)
			{
				if (danhHieu.so_huu == 1)
				{
					mVector2.addElement(new iCommand("Sử dụng", 4, danhHieu.id, this));
				}
				else if (danhHieu.so_huu == 2)
				{
					mVector2.addElement(new iCommand("Gỡ", 5, danhHieu.id, this));
				}
			}
			GameCanvas.menu.startAt(mVector2, 2, "Menu");
			break;
		}
		case 2:
			show_danhhieu = false;
			GameCanvas.gameScr.Show();
			break;
		case 3:
			show_danhhieu = !show_danhhieu;
			if (show_danhhieu)
			{
				setCameraShow();
			}
			else
			{
				setCamera();
			}
			break;
		case 4:
			GlobalService.gI().danhhieu(1, subIndex, 0);
			break;
		case 5:
			GlobalService.gI().danhhieu(1, subIndex, 1);
			break;
		case 1:
			break;
		}
	}

	public virtual void setCamera()
	{
		int limX = vecDanhHieu.size() * hItem - hContent + miniItem * 2;
		list = new ListNew(xSub, ySub + GameCanvas.hCommand, wSub, hContent, 0, 0, limX, isLim0: true);
	}

	public virtual void setCameraShow()
	{
		int limX = ((DanhHieu)vecDanhHieu.elementAt(idSelect)).option.Count * 12 + 65;
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
		g.setClip(xSub, ySub + GameCanvas.hCommand + miniItem, wSub - 30, hContent - miniItem);
		g.saveCanvas();
		g.ClipRec(xSub, ySub + GameCanvas.hCommand + miniItem, wSub, hContent - miniItem);
		g.translate(0, -list.cmx);
		int num = xSub + 30;
		int num2 = ySub + GameCanvas.hCommand + 10;
		if (show_danhhieu)
		{
			DanhHieu danhHieu = (DanhHieu)vecDanhHieu.elementAt(idSelect);
			int num3 = wSub - 40;
			int h = danhHieu.option.Count * 12 + 55;
			g.setColor(9403484);
			g.fillRect(num, num2, num3, h);
			g.setColor(10066278);
			g.fillRect(num, num2, num3, 18);
			mFont.tahoma_7b_white.drawString(g, danhHieu.name, num + num3 / 2, num2 + 3, 2);
			int num4 = 0;
			mFont.tahoma_7b_white.drawString(g, "Chỉ số:", num + 6, num2 + 22, 0);
			if (danhHieu.option.Count > 0)
			{
				foreach (MainInfoItem item in danhHieu.option)
				{
					num4 += 12;
					if (MainItem.mNameAttributes[item.id].ispercent == 0)
					{
						mFont.tahoma_7_white.drawString(g, MainItem.mNameAttributes[item.id].name + " + " + item.value, num + 14, num2 + 22 + num4, 0);
						continue;
					}
					int num5 = item.value / 10;
					mFont.tahoma_7_white.drawString(g, MainItem.mNameAttributes[item.id].name + " + " + num5 + "," + (item.value - num5 * 10) + " %", num + 14, num2 + 22 + num4, 0);
				}
			}
			else
			{
				num4 = 12;
				mFont.tahoma_7_white.drawString(g, "Không có chỉ số", num + 14, num2 + 22 + num4, 0);
			}
			num4 += 14;
			if (danhHieu.so_huu == 0)
			{
				mFont.tahoma_7b_red.drawString(g, "Chưa sở hữu", num + 6, num2 + 22 + num4, 0);
			}
			else if (danhHieu.so_huu == 1)
			{
				mFont.tahoma_7b_green.drawString(g, "Đã sở hữu", num + 6, num2 + 22 + num4, 0);
			}
			else
			{
				mFont.tahoma_7b_violet.drawString(g, "Đang dùng", num + 6, num2 + 22 + num4, 0);
			}
		}
		else if (isLoad)
		{
			MsgDialog.fraImgWaiting.drawFrame(GameCanvas.gameTick / 6 % MsgDialog.fraImgWaiting.nFrame, xSub + wSub / 2, num2 + hItem, 0, 3, g);
		}
		else if (vecDanhHieu.size() == 0)
		{
			mFont.tahoma_7_black.drawString(g, textrong, xSub + wSub / 2, ySub + hSub / 2, 2);
		}
		else
		{
			if (idSelect >= 0 && (GameCanvas.isTouchNoOrPC() || timeShowFocus > 0))
			{
				paintSelect(g, num, num2 - 2, wSub - 40);
			}
			for (int i = 0; i < vecDanhHieu.size(); i++)
			{
				DanhHieu dh = (DanhHieu)vecDanhHieu.elementAt(i);
				if (i == 0)
				{
					yPaintFirst = num2;
				}
				paintInfo(g, dh, num, num2, i, wSub - 60);
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

	public void paint_show(mGraphics g)
	{
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
		g.fillRect(xbegin + miniItem / 2, ybegin + idSelect * hItem, wFocus - miniItem / 2 - 12, hItem);
		g.setColor(16066606);
		g.drawRect(xbegin + miniItem / 2, ybegin + idSelect * hItem, wFocus - miniItem / 2 - 12, hItem);
	}

	public void paintInfo(mGraphics g, DanhHieu dh, int xpaint, int ypaint, int i, int wsub)
	{
		g.setColor(10066278);
		g.fillRect(xpaint, ypaint, wsub - 10, 18);
		mFont.tahoma_7b_white.drawString(g, dh.name, xpaint + (wsub - 10) / 2, ypaint + 3, 2);
		if (dh.so_huu == 0)
		{
			mFont.tahoma_7b_red.drawString(g, "Chưa sở hữu", xpaint + (wsub - 10) / 2, ypaint + 22, 2);
		}
		else if (dh.so_huu == 1)
		{
			mFont.tahoma_7b_yellow.drawString(g, "Đã sở hữu", xpaint + (wsub - 10) / 2, ypaint + 22, 2);
		}
		else
		{
			mFont.tahoma_7b_green.drawString(g, "Đang dùng", xpaint + (wsub - 10) / 2, ypaint + 22, 2);
		}
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
			idSelect = AvMain.resetSelect(idSelect, vecDanhHieu.size() - 1, isreset: false);
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
				((iCommand)vecMenu.elementAt(i)).updatePointer();
			}
		}
		if (!GameCanvas.isPointerSelect || vecDanhHieu.size() <= 0 || !GameCanvas.isPoint(xSub, ySub + GameCanvas.hCommand, wSub, hContent))
		{
			return;
		}
		GameCanvas.isPointerSelect = false;
		int num = (GameCanvas.py - (ySub + GameCanvas.hCommand) + list.cmx) / hItem;
		if (show_danhhieu || num < 0 || num >= vecDanhHieu.size())
		{
			return;
		}
		if (isDelEvent)
		{
			if (GameCanvas.px < xSub + 30 + (wSub - 60) + 15 && GameCanvas.px > xSub + 30 + (wSub - 60) - 15 && num > 0)
			{
				DanhHieu mem = (DanhHieu)vecDanhHieu.elementAt(num);
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
			if (vecDanhHieu.size() == 0)
			{
				idSelect = -1;
			}
			if (idSelect >= vecDanhHieu.size())
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

	public virtual void delMem(DanhHieu mem)
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
