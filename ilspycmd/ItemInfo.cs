public class ItemInfo : SubScreen
{
	public static ItemInfo instance;

	private InfoMemList mem;

	private iCommand cmdClose;

	private mVector vecMenu = new mVector();

	private int idCommand;

	public ItemInfo(sbyte type, InfoMemList mem)
		: base(type)
	{
		this.mem = mem;
		wSub = 160;
		hSub = 120;
		xSub = MotherCanvas.hw - wSub / 2;
		ySub = MotherCanvas.hh - hSub / 2;
		idCommand = 0;
		cmdClose = new iCommand(T.close, -1, this);
		vecMenu.addElement(cmdClose);
		setPosCmdNew(0, vecMenu);
		mem.item.strShow = mFont.tahoma_7_white.splitFontArray(mem.item.numPotion + " " + mem.item.name, wSub - 20 - 32 - 15);
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == -1)
		{
			AuctionScreen.instance.Show(GameCanvas.gameScr);
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		paintInfo(g);
		GameCanvas.resetTrans(g);
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	private void paintInfo(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		paintPaper(g, xSub, ySub, wSub, hSub, wSub, AvMain.PAPER_NORMAL);
		g.setColor(15972174);
		g.fillRoundRect(xSub + 10, ySub + 16, wSub - 20, 16, 4, 4);
		AvMain.FontBorderColor(g, T.info, xSub + wSub / 2, ySub + 18, 2, 6, 5);
		AvMain.paintRect(g, xSub + 10, ySub + 40, wSub - 20, 32, 0, 4);
		if (mem != null)
		{
			GameCanvas.resetTrans(g);
			mem.item.paintInfo(g, xSub + 10, ySub + 40);
		}
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

	public override void update()
	{
		base.update();
		if (lastScreen != null)
		{
			lastScreen.update();
		}
	}
}
