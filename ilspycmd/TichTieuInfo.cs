public class TichTieuInfo : SubScreen
{
	public static TichTieuInfo instance;

	private ListNew list;

	private InfoMemList mem;

	private iCommand cmdClose;

	private iCommand cmdNhan;

	private mVector vecMenu = new mVector();

	private int idCommand;

	private int idSelect;

	private int hContent;

	private int miniItem;

	private Scroll scroll = new Scroll();

	public TichTieuInfo(sbyte type, InfoMemList mem)
		: base(type)
	{
		this.mem = mem;
		if (TichTieuScreen.instance.wSub > 180)
		{
			wSub = TichTieuScreen.instance.wSub - 20;
		}
		else
		{
			wSub = TichTieuScreen.instance.wSub;
		}
		hSub = TichTieuScreen.instance.hSub;
		xSub = MotherCanvas.hw - wSub / 2;
		ySub = MotherCanvas.hh - hSub / 2;
		idCommand = 0;
		idSelect = 0;
		hItem = 29;
		miniItem = 5;
		hContent = hSub - 80 - iCommand.hButtonCmdNor;
		cmdClose = new iCommand(T.close, -1, this);
		vecMenu.removeAllElements();
		vecMenu.addElement(cmdClose);
		setPosCmdNew(0, vecMenu);
		int limX = mem.quaNapThe.numQua * hItem - hContent + miniItem * 2;
		list = new ListNew(xSub + 10, ySub + 68, wSub - 20 - miniItem, hContent, 0, 0, limX, isLim0: true);
		scroll.setInfo(xSub + wSub - 10, ySub + 69, hContent, 8809550);
		for (int i = 0; i < mem.quaNapThe.vecQua.size(); i++)
		{
			ItemQuaNT itemQuaNT = (ItemQuaNT)mem.quaNapThe.vecQua.elementAt(i);
			itemQuaNT.strShow = mFont.tahoma_7_white.splitFontArray(itemQuaNT.numPotion + " " + itemQuaNT.name, wSub - 70);
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			TichTieuScreen.instance.Show(GameCanvas.gameScr);
			break;
		case 0:
			GlobalService.gI().Send_Nhan_TichTieuScr(1, (sbyte)mem.id);
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
		paintBg(g, xSub + 10, ySub + 38);
		setClip(g);
		paintInfo(g, xSub + 10, ySub + 73);
		GameCanvas.resetTrans(g);
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (list.cmxLim > 0)
		{
			scroll.paint(g);
		}
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	private void paintInfo(mGraphics g, int xpaint, int ypaint)
	{
		if (mem != null)
		{
			for (int i = 0; i < mem.quaNapThe.numQua; i++)
			{
				ItemQuaNT itemQuaNT = (ItemQuaNT)mem.quaNapThe.vecQua.elementAt(i);
				itemQuaNT.paintInfo(g, xpaint + miniItem, ypaint);
				ypaint += hItem;
			}
		}
	}

	private void paintBg(mGraphics g, int xpaint, int ypaint)
	{
		paintPaper(g, xSub, ySub, wSub, hSub, wSub, AvMain.PAPER_NORMAL);
		g.setColor(15972174);
		g.fillRoundRect(xSub + 10, ySub + 16, wSub - 20, 16, 4, 4);
		AvMain.FontBorderColor(g, T.phanthuong, xSub + wSub / 2, ySub + 18, 2, 6, 5);
		mFont.tahoma_7_black.drawString(g, T.tichTieuRuby + " " + Interface_Game.ConvertNumToStr(mem.quaNapThe.cost) + " ", xpaint, ypaint, 0);
		mFont.tahoma_7_black.drawString(g, T.nhanduoc, xpaint, ypaint + 14, 0);
		AvMain.paintRect(g, xpaint, ypaint + 30, wSub - 20 - miniItem, hContent, 0, 4);
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		base.update();
		list.moveCamera();
		scroll.setYScrool(list.cmx, list.cmxLim);
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
			idSelect = AvMain.resetSelect(idSelect, mem.quaNapThe.numQua - 1, isreset: false);
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
		base.updatePointer();
		list.update_Pos_UP_DOWN();
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.updatePointer();
			}
		}
	}

	private void setClip(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(xSub + 10, ySub + 68, wSub - 20 - miniItem, hContent);
		g.saveCanvas();
		g.ClipRec(xSub + 10, ySub + 68, wSub - 20 - miniItem, hContent);
		g.translate(0, -list.cmx);
	}
}
