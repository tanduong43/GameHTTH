public class ScreenDoiQua : MainScreen
{
	private int x;

	private int y;

	private int w;

	private int h;

	private int wItem;

	private int wsai;

	public mVector vecItemLose = new mVector();

	public mVector vecItemGif = new mVector();

	private mVector vecEff = new mVector();

	private int[] PosX;

	private int[] PosY;

	public sbyte Focus;

	public sbyte Type_Focus;

	public mVector vecCmd = new mVector();

	public iCommand cmdClose;

	public iCommand cmdNhan;

	public static ScreenDoiQua _instance;

	public ScreenDoiQua()
	{
		w = 240;
		h = 185;
		wItem = 24;
		if (GameCanvas.isTouch)
		{
			wItem = 28;
		}
		if (w > MotherCanvas.w)
		{
			w = MotherCanvas.w;
		}
		if (h > MotherCanvas.h - GameCanvas.hCommand * 2)
		{
			h = MotherCanvas.h - GameCanvas.hCommand * 2;
		}
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2 + 5;
		cmdClose = new iCommand(T.close, -1, this);
		cmdNhan = new iCommand(T.doiqua, 0, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x + w / 2 + 60, y - 15 + 10 + 8, MainTab.fraCloseTab, string.Empty);
			cmdNhan.setPos(MotherCanvas.hw, h + 8, null, T.doiqua);
			vecCmd.addElement(cmdClose);
			vecCmd.addElement(cmdNhan);
		}
		else
		{
			cmdClose.caption = T.close;
			cmdClose.setPos(MotherCanvas.hw - GameCanvas.wCommand / 2 - 18, y + h + 5 + GameCanvas.hCommand, null, T.close);
			cmdNhan.setPos(MotherCanvas.hw + GameCanvas.wCommand / 2 + 18, y + h + 5 + GameCanvas.hCommand, null, T.doiqua);
			right = cmdNhan;
			left = cmdClose;
		}
		PosX = new int[6];
		PosY = new int[6];
		for (int i = 0; i < PosX.Length; i++)
		{
			if (i < PosX.Length / 2)
			{
				PosX[i] = x + w / 5 - 10;
				PosY[i] = y + h / 6 + (wItem + 3) * i;
			}
			else
			{
				PosX[i] = x + w / 5 + 3 + wItem - 10;
				PosY[i] = y + h / 6 + (wItem + 3) * (i - 3);
			}
		}
		Focus = -1;
		wsai = w / 2 - 13;
	}

	public static ScreenDoiQua instance()
	{
		if (_instance == null)
		{
			_instance = new ScreenDoiQua();
		}
		return _instance;
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		paintBackGr(g);
		if (vecCmd != null && GameCanvas.getShowCmd())
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		paintEff(g, 0);
		base.paint(g);
	}

	public void paintBackGr(mGraphics g)
	{
		paintPaper(g, x, y - 17, w, h + 17, w, AvMain.PAPER_NORMAL);
		g.setColor(14203529);
		g.fillRoundRect(x + w / 2 - 60, y - 15 + 10, 120, 16, 4, 4);
		AvMain.FontBorderColor(g, T.doiqua, x + w / 2, y - 15 + 12, 2, 6, 5);
		for (int i = 0; i < PosX.Length; i++)
		{
			AvMain.paintRect(g, PosX[i], PosY[i], wItem, wItem, 1, 0);
			AvMain.paintRect(g, PosX[i] + wsai, PosY[i], wItem, wItem, 1, 0);
			if (Focus >= 0)
			{
				g.setColor(16711680);
				if (Type_Focus == 0)
				{
					g.drawRect(PosX[Focus] + 1, PosY[Focus] + 1, wItem - 2, wItem - 2);
				}
				else
				{
					g.drawRect(PosX[Focus] + 1 + wsai, PosY[Focus] + 1, wItem - 2, wItem - 2);
				}
			}
		}
		AvMain.FontBorderColor(g, T.doi, PosX[0] + wItem + 1, PosY[2] + wItem + 5, 2, 6, 7);
		AvMain.FontBorderColor(g, T.nhan, PosX[0] + wItem + 1 + wsai, PosY[2] + wItem + 5, 2, 1, 7);
	}

	public void paintEff(mGraphics g, int level)
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			mainEffect.paint(g);
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			vecEff.removeAllElements();
			break;
		case 0:
			createEff(75, 0, PosX[0] + wItem / 2, PosY[0] + wItem / 2, PosX[0] + wItem / 2, PosY[0] + wItem / 2);
			break;
		}
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdClose != null)
		{
			cmdClose.perform();
		}
		return false;
	}

	public void updateEff()
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			mainEffect.update();
			if (mainEffect.isStop)
			{
				if (mainEffect.typeEffect == 75)
				{
					createEff(53, 0, PosX[0] + wsai + wItem / 2, PosY[0] + wItem / 2, PosX[0] + wsai + wItem / 2, PosY[0] + wItem / 2);
				}
				vecEff.removeElement(mainEffect);
				i--;
			}
		}
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		updateEff();
	}

	public void createEff(short type, int subtype, int x, int y, int xto, int yto)
	{
		Effect_End o = new Effect_End(type, (sbyte)subtype, x, y, xto, yto, 0, null);
		vecEff.addElement(o);
	}

	public override void updatePointer()
	{
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		for (int j = 0; j < PosX.Length; j++)
		{
			if (GameCanvas.isPointSelect(PosX[j], PosY[j], wItem, wItem))
			{
				Focus = (sbyte)j;
				Type_Focus = 0;
				GameCanvas.isPointerSelect = false;
			}
			if (GameCanvas.isPointSelect(PosX[j] + wsai, PosY[j], wItem, wItem))
			{
				Focus = (sbyte)j;
				Type_Focus = 1;
				GameCanvas.isPointerSelect = false;
			}
		}
		base.updatePointer();
	}
}
