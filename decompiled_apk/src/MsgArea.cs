public class MsgArea : MsgDialog
{
	private sbyte[] mStatus;

	private int maxItemW;

	private int hItem;

	private int sizeButton;

	private int sizeBanner;

	private iCommand cmdSelect;

	private sbyte typePaintArea;

	public const sbyte TYPE_AREA = 0;

	public const sbyte TYPE_PAGE_MARKET = 1;

	public int timePaintfocus;

	public void setinfoChangeArea(sbyte[] mstatus, sbyte type)
	{
		mStatus = mstatus;
		typePaintArea = type;
		nameDialog = T.Area;
		if (type == 1)
		{
			nameDialog = T.page;
		}
		fontDia = mFont.tahoma_7_black;
		beginDia();
		wShowPaper = 5;
		wItem = 30;
		hItem = 30;
		sizeButton = 24;
		sizeBanner = 120;
		maxItemW = 5;
		if (maxItemW > mstatus.Length)
		{
			maxItemW = mstatus.Length;
		}
		list = new ListNew(xDia, yDia, wDia, hDia, 0, 0, 0, isLim0: true);
		wDia = maxItemW * wItem;
		if (wDia < 120)
		{
			wDia = 120;
		}
		if (sizeBanner > wDia - 20)
		{
			sizeBanner = wDia - 20;
		}
		maxWShow = wDia;
		xDia = MotherCanvas.hw - wDia / 2;
		hDia = ((mstatus.Length - 1) / maxItemW + 1) * hItem;
		cmdSelect = new iCommand(T.select, 0, this);
		cmdClose = new iCommand(T.close, 1, this);
		if (hDia > 140)
		{
			int num = hDia;
			hDia = 140;
			yDia = MotherCanvas.hh - hDia / 2 - 5;
			list = new ListNew(xDia, yDia, wDia, 140, 0, 0, num - 140, isLim0: true);
		}
		else
		{
			yDia = MotherCanvas.hh - hDia / 2 - 5;
		}
		if (!GameCanvas.isTouch)
		{
			right = cmdClose;
			left = cmdSelect;
		}
		else
		{
			if (GameCanvas.isTouchNoOrPC())
			{
				idSelect = 0;
			}
			else
			{
				idSelect = -1;
			}
			cmdClose.setPos(xDia + maxWShow / 2 + sizeBanner / 2, yDia - 20 + 8, MainTab.fraCloseTab, string.Empty);
		}
		backCMD = cmdClose;
		okCMD = cmdSelect;
		GameCanvas.isPointerSelect = false;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (idSelect >= 0 && idSelect < mStatus.Length)
			{
				if (typePaintArea == 0)
				{
					GlobalService.gI().Select_Area(0, (sbyte)idSelect);
				}
				else if (typePaintArea == 1)
				{
					sbyte indexMarket = -1;
					if (GameCanvas.currentScreen == GameCanvas.tabMarketScr)
					{
						indexMarket = ((MainTab)GameCanvas.tabMarketScr.vecTabs.elementAt(GameCanvas.tabMarketScr.idSelect)).indexMarket;
					}
					GlobalService.gI().Market(1, indexMarket, (short)idSelect, 0, 1);
				}
			}
			GameCanvas.end_Dialog();
			break;
		case 1:
			GameCanvas.end_Dialog();
			break;
		}
		base.commandPointer(index, subIndex);
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia + 4;
		int num2 = xDia + 4;
		paintPaper(g, xDia - 5, yDia - 32, maxWShow + 10, hDia + 44, maxWShow + 10, AvMain.PAPER_NORMAL);
		g.setColor(15972174);
		g.fillRoundRect(xDia + maxWShow / 2 - sizeBanner / 2, yDia - 20, sizeBanner, 16, 4, 4);
		AvMain.FontBorderColor(g, nameDialog, xDia + maxWShow / 2, yDia - 18, 2, 6, 5);
		g.setClip(MotherCanvas.hw - maxWShow / 2, yDia, maxWShow, hDia);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - maxWShow / 2, yDia, maxWShow, hDia);
		g.translate(0, -list.cmx);
		for (int i = 0; i < mStatus.Length; i++)
		{
			AvMain.paintRectButton(g, num2 + i % maxItemW * wItem, num + i / maxItemW * hItem, sizeButton, sizeButton, (idSelect == i) ? 1 : 0);
			sbyte color = 1;
			if (mStatus[i] == 1)
			{
				color = 3;
			}
			else if (mStatus[i] == 2)
			{
				color = 6;
			}
			else if (mStatus[i] == 3)
			{
				color = 2;
			}
			AvMain.FontBorderColor(g, string.Empty + (i + 1), num2 + i % maxItemW * wItem + sizeButton / 2, num + i / maxItemW * hItem + GameCanvas.hText / 2, 2, color, 7);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		paintCmd(g);
		if (GameCanvas.isTouch)
		{
			cmdClose.paint(g, cmdClose.xCmd, cmdClose.yCmd);
		}
	}

	public override void update()
	{
		list.moveCamera();
		updatekey();
		updatePointer();
		if (timePaintfocus > 0)
		{
			timePaintfocus--;
			if (GameCanvas.isTouch && timePaintfocus == 0)
			{
				idSelect = -1;
			}
		}
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(2))
		{
			idSelect++;
			GameCanvas.ClearkeyMove(2);
			flag = true;
		}
		else if (GameCanvas.keyMove(0))
		{
			idSelect--;
			GameCanvas.ClearkeyMove(0);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			idSelect += maxItemW;
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		else if (GameCanvas.keyMove(1))
		{
			idSelect -= maxItemW;
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMyHold[5])
		{
			cmdSelect.perform();
			GameCanvas.clearKeyHold(5);
		}
		if (flag)
		{
			idSelect = AvMain.resetSelect(idSelect, mStatus.Length - 1, isreset: true);
			list.setToX(idSelect / 3 * hItem - hDia / 2);
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		cmdClose.updatePointer();
		if (GameCanvas.isPointerDown && GameCanvas.isPoint(xDia, yDia, wDia, hDia))
		{
			int num = (GameCanvas.px - xDia) / wItem + (GameCanvas.py - yDia + list.cmx) / hItem * maxItemW;
			if (num >= 0 && num < mStatus.Length)
			{
				idSelect = num;
				timePaintfocus = 10;
			}
		}
		if (GameCanvas.isPointerMove)
		{
			timePaintfocus = 0;
			idSelect = -1;
		}
		if (GameCanvas.isPointSelect(xDia, yDia, wDia, hDia))
		{
			int num2 = (GameCanvas.px - xDia) / wItem + (GameCanvas.py - yDia + list.cmx) / hItem * maxItemW;
			if (num2 >= 0 && num2 < mStatus.Length)
			{
				cmdSelect.perform();
			}
			GameCanvas.isPointerSelect = false;
		}
		list.update_Pos_UP_DOWN();
		base.updatePointer();
	}
}
