public class WantedScreen : MainScreen
{
	private sbyte page;

	private sbyte maxpage;

	public static WantedScreen instance;

	private iCommand cmdClose;

	private int x;

	private int y;

	private int w;

	private int h;

	private int idSelect;

	private mVector vecList = new mVector();

	public WantedScreen()
	{
	}

	public WantedScreen(mVector vec, sbyte page, sbyte maxpage)
	{
		this.page = page;
		this.maxpage = maxpage;
		vecList = vec;
		w = AvMain.wWanted;
		h = AvMain.hWanted;
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2;
		cmdClose = new iCommand(T.close, -1, this);
		cmdClose = AvMain.setPosCMD(cmdClose, 2);
		right = cmdClose;
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == -1 && lastScreen != null)
		{
			lastScreen.Show(lastScreen.lastScreen);
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		paintBg(g, x, y);
		if (idSelect >= 0 && idSelect < vecList.size())
		{
			InfoMemList mem = (InfoMemList)vecList.elementAt(idSelect);
			paintInfo(g, mem, x, y, w, h);
		}
		paintCmd(g);
	}

	public static void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int wpaint, int hpaint)
	{
		if (mem != null)
		{
			AvMain.FontBorderColor(g, mem.name, xpaint + wpaint / 2, ypaint + 102, 2, 0, 8);
			if (mem.charShow != null)
			{
				mem.charShow.paintCharShow(g, xpaint + wpaint / 2, ypaint + 80, 0, isNhip: false);
				NumberDam.paintSmallWanted(g, mem.charShow.wanted, xpaint + wpaint / 2 + 4, ypaint + 130);
			}
		}
	}

	public void paintBg(mGraphics g, int xpaint, int ypaint)
	{
		sbyte b = 2;
		if (vecList.size() == 1)
		{
			b = 3;
		}
		else if (idSelect == 0)
		{
			b = 1;
		}
		else if (idSelect == vecList.size() - 1)
		{
			b = 0;
		}
		AvMain.paintBG_Wanted(g, xpaint, ypaint, w, h, b);
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
		bool flag = false;
		if (GameCanvas.keyMove(0))
		{
			if (idSelect > 0)
			{
				idSelect--;
			}
			GameCanvas.ClearkeyMove(0);
			flag = true;
		}
		else if (GameCanvas.keyMove(2))
		{
			if (idSelect < vecList.size() - 1)
			{
				idSelect++;
			}
			GameCanvas.ClearkeyMove(2);
			flag = true;
		}
		if (flag)
		{
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		base.updatePointer();
		if (GameCanvas.isPointerSelect && GameCanvas.isPoint(0, 0, MotherCanvas.hw, MotherCanvas.h))
		{
			GameCanvas.isPointerSelect = false;
			if (idSelect > 0)
			{
				idSelect--;
			}
		}
		if (GameCanvas.isPointerSelect && GameCanvas.isPoint(MotherCanvas.hw, 0, MotherCanvas.hw, MotherCanvas.h))
		{
			GameCanvas.isPointerSelect = false;
			if (idSelect < vecList.size() - 1)
			{
				idSelect++;
			}
		}
	}
}
