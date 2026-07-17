public class SubScreen : MainScreen
{
	public sbyte type;

	public int xSub;

	public int ySub;

	public int wSub;

	public int hSub;

	public int hItem;

	public int xBegin;

	public int w2cmd;

	public SubScreen(sbyte type)
	{
		this.type = type;
	}

	public override void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public virtual void setPosCmdNew(int yplus, mVector vecMenu)
	{
		if (vecMenu == null || vecMenu.size() <= 0)
		{
			return;
		}
		int num = vecMenu.size();
		switch (num)
		{
		case 1:
			xBegin = xSub + wSub / 2;
			w2cmd = 0;
			break;
		case 2:
			w2cmd = 10;
			xBegin = xSub + wSub / 2 - w2cmd / 2 - iCommand.wButtonCmd / 2;
			break;
		default:
			w2cmd = 10;
			xBegin = xSub + wSub / 2 - w2cmd / 2 - iCommand.wButtonCmd / 2;
			break;
		}
		for (int i = 0; i < num; i++)
		{
			iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
			iCommand2.isSelect = false;
			if (num == 3 && i == 2)
			{
				iCommand2.setPos(xSub + wSub / 2, ySub + hSub - iCommand.hButtonCmdNor - (num - 1) / 2 * (iCommand.hButtonCmdNor + 5) + iCommand.hButtonCmdNor / 2 + 2 + i / 2 * (iCommand.hButtonCmdNor + 5) + yplus - 5, null, iCommand2.caption);
			}
			else
			{
				iCommand2.setPos(xBegin + i % 2 * (iCommand.wButtonCmd + w2cmd), ySub + hSub - iCommand.hButtonCmdNor / 2 - ((num - 1) / 2 * iCommand.hButtonCmdNor + 5) + 2 + i / 2 * (iCommand.hButtonCmdNor + 5) + yplus - 5, null, iCommand2.caption);
			}
			if (i == 0 && !GameCanvas.isTouch)
			{
				iCommand2.isSelect = true;
			}
		}
	}

	public void setPosVecMenu(mVector vecMenu)
	{
		if (vecMenu == null || vecMenu.size() <= 0)
		{
			return;
		}
		int num = vecMenu.size();
		switch (num)
		{
		case 1:
			xBegin = xSub + wSub / 2;
			w2cmd = 0;
			break;
		case 2:
			w2cmd = 10;
			xBegin = xSub + wSub / 2 - w2cmd / 2 - iCommand.wButtonCmd / 2;
			break;
		default:
			w2cmd = 10;
			xBegin = xSub + wSub / 2 - w2cmd / 2 - iCommand.wButtonCmd / 2;
			break;
		}
		for (int i = 0; i < num; i++)
		{
			iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
			iCommand2.isSelect = false;
			if (num == 3 && i == 2)
			{
				iCommand2.setPos(xSub + wSub / 2, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, iCommand2.caption);
			}
			else
			{
				iCommand2.setPos(xBegin + i % 2 * (iCommand.wButtonCmd + w2cmd), MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, iCommand2.caption);
			}
			if (i == 0 && !GameCanvas.isTouch)
			{
				iCommand2.isSelect = true;
			}
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
	}

	public override void paint(mGraphics g)
	{
	}
}
