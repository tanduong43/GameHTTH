public class WantedPoster : SubScreen
{
	private InfoMemList mem;

	public static WantedPoster instance;

	private iCommand cmdClose;

	private iCommand cmdNhan;

	private iCommand cmdHuy;

	private iCommand cmdNhanThuong;

	private iCommand cmdXoa;

	private mVector vecMenu = new mVector();

	private int idCommand;

	private int hCharShow;

	public WantedPoster(sbyte type, InfoMemList mem)
		: base(type)
	{
		this.mem = mem;
		wSub = AvMain.wWanted;
		hSub = AvMain.hWanted;
		xSub = MotherCanvas.hw - wSub / 2;
		ySub = MotherCanvas.hh - hSub / 2;
		idCommand = 0;
		cmdClose = new iCommand(T.close, -1, this);
		cmdNhan = new iCommand(T.nhanQuest, 0, this);
		cmdHuy = new iCommand(T.cancel, 1, this);
		cmdNhanThuong = new iCommand(T.nhanThuong, 2, this);
		cmdXoa = new iCommand(T.del, 3, this);
		vecMenu.removeAllElements();
		if (type == 1)
		{
			vecMenu.addElement(cmdNhan);
		}
		else if ((type == 5 || type == 4) && mem.isWantedSuccess == 0)
		{
			vecMenu.addElement(cmdHuy);
		}
		else if (type == 4 && mem.isWantedSuccess == 1)
		{
			vecMenu.addElement(cmdNhanThuong);
		}
		else if (type == 5 && mem.isReceiveGift == 1)
		{
			vecMenu.addElement(cmdXoa);
		}
		vecMenu.addElement(cmdClose);
		setPosVecMenu(vecMenu);
		setCharHpaint();
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			WantedList.instance.Show(GameCanvas.gameScr);
			break;
		case 0:
			GlobalService.gI().Send_Wanted_Poster(2, (short)mem.id);
			GameCanvas.gameScr.Show();
			break;
		case 1:
			GlobalService.gI().Send_Wanted_Poster_Action(type, (short)mem.id, 1);
			GameCanvas.gameScr.Show();
			break;
		case 2:
			GlobalService.gI().Send_Wanted_Poster_Action(type, (short)mem.id, 2);
			GameCanvas.gameScr.Show();
			break;
		case 3:
			GlobalService.gI().Send_Wanted_Poster_Action(5, (short)mem.id, 3);
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		paintBg(g, xSub, ySub);
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		paintInfo(g, xSub, ySub, wSub, hSub);
	}

	private void paintInfo(mGraphics g, int xpaint, int ypaint, int wpaint, int hpaint)
	{
		if (mem != null && mem.charShow != null)
		{
			g.translate(xpaint, ypaint);
			mem.charShow.paintCharShow(g, wpaint / 2, hCharShow, 1, isNhip: false);
			AvMain.FontBorderColor(g, mem.charShow.name, wpaint / 2, 103, 2, 0, 8);
			NumberDam.paintSmallWantedPoster(g, mem.charShow.wanted, wpaint / 2 + 4, 128);
			g.translate(-xpaint, -ypaint);
		}
	}

	private void paintBg(mGraphics g, int xpaint, int ypaint)
	{
		if (GameCanvas.isTouch)
		{
			g.drawImage(AvMain.imgDemoWanted, xpaint, ypaint, 0);
			return;
		}
		paintWantedPaper(g, xpaint, ypaint, wSub, hSub);
		g.translate(xpaint, ypaint);
		g.drawImage(AvMain.imgTrangTri, wSub / 2, hSub * 4 / 31, mGraphics.HCENTER | mGraphics.VCENTER);
		g.setColor(13019772);
		g.fillRect(15, hSub * 7 / 31, wSub - 30, 52);
		g.fillRect(15, 100, wSub - 32, 16);
		g.setColor(10781286);
		g.fillRect(16, hSub * 7 / 31 + 1, wSub - 30, 50);
		g.fillRect(16, 101, wSub - 32, 14);
		g.drawImage(AvMain.mimgWanted[13], wSub / 2, 94, 3);
		g.drawImage(AvMain.mimgWanted[14], 23, 130, 3);
		g.drawImage(AvMain.mimgWanted[16], 7, 93, 0);
		g.drawRegion(AvMain.mimgWanted[16], 0, 0, 6, 46, 2, wSub - 13, 93f, 0);
		g.drawImage(AvMain.mimgWanted[15], 102, 145, 3);
		g.drawImage(AvMain.mimgWanted[17], 48, 144, 3);
		g.translate(-xpaint, -ypaint);
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		base.update();
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
		base.updatePointer();
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.updatePointer();
			}
		}
	}

	private void setCharHpaint()
	{
		if (mem.charShow.hOne == 0)
		{
			mem.charShow.setHeadBigBody();
			hCharShow = hSub * 7 / 31 + (hSub * 3 / 5 - hSub * 7 / 31) / 2 + mem.charShow.hOne / 2 - 7;
			if (mem.charShow.hOne > 52)
			{
				hCharShow -= 3;
			}
		}
	}
}
