public class MsgAutoGetItem : MsgDialog
{
	private int[][] mAuto;

	public static int[] mValue = new int[3];

	private int hItem;

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 6)
		{
			Player.isGetItem = true;
			isClose = true;
			GameCanvas.saveRms.SaveAutoGetItem();
		}
	}

	public void setinfoAuto_Get_Item()
	{
		mAuto = new int[3][]
		{
			new int[4] { 5, 7, 8, 6 },
			new int[4] { 5, 1, 2, 6 },
			new int[2] { 5, 6 }
		};
		fontDia = mFont.tahoma_7_black;
		beginDia();
		cmdList = new mVector();
		iCommand o = new iCommand(T.xong, 6, this);
		cmdList.addElement(o);
		okCMD = o;
		wDia = MotherCanvas.w;
		if (wDia > 210)
		{
			wDia = 210;
		}
		if (wDia < 190)
		{
			wDia = 190;
		}
		maxWShow = wDia;
		wShowPaper = 5;
		hItem = 28;
		wItem = 26;
		if (wDia < 210)
		{
			wItem = 20;
		}
		hDia = 160;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 - 5;
		if (GameCanvas.isTouch)
		{
			miniItem = 28;
		}
		setPosCmdNew(-2, isLast: false);
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia;
		int num2 = xDia + 15;
		paintPaper(g, MotherCanvas.hw - wShowPaper / 2, num, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
		g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		num += 12;
		g.setColor(15972174);
		g.fillRoundRect(xDia + 10, num, wDia - 20, 16, 4, 4);
		num += 3;
		AvMain.FontBorderColor(g, T.setting, xDia + wDia / 2, num, 2, 6, 5);
		num += hItem;
		if (GameCanvas.isTouchNoOrPC())
		{
			paintSelect(g, num2 + 3, num - hItem / 4 - 1, wDia - 36);
		}
		for (int i = 0; i < mAuto.Length; i++)
		{
			int num3 = num2 + 70;
			mFont.tahoma_7b_black.drawString(g, T.mAutoGetItem[i], num3 - 14, num, 1);
			for (int j = 0; j < mAuto[i].Length; j++)
			{
				if (j == mValue[i])
				{
					g.drawImage(AvMain.imgHotKey, num3, num + 6, 3);
				}
				if (MsgDialog.fraAutoMpHp == null)
				{
					MsgDialog.fraAutoMpHp = new FrameImage(mImage.createImage("/interface/automphp.png"), 20, 20);
				}
				else
				{
					MsgDialog.fraAutoMpHp.drawFrame(mAuto[i][j], num3, num + 6, 0, 3, g);
				}
				num3 += wItem;
			}
			num += hItem;
		}
		paintInfoHelp(g);
		if (cmdList != null)
		{
			for (int k = 0; k < cmdList.size(); k++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(k);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		g.restoreCanvas();
	}

	public void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(16774758);
		xbegin -= miniItem;
		g.fillRect(xbegin, ybegin + idSelect * hItem, wFocus + miniItem * 2, hItem);
	}

	public override void update()
	{
		updateInfoHelp();
		if (isClose)
		{
			updateClose();
			return;
		}
		updateOpen();
		if (GameCanvas.isTouchNoOrPC())
		{
			updatekey();
		}
		updatePointer();
	}

	public override void updatekey()
	{
		if (GameCanvas.keyMove(1))
		{
			if (idSelect > 0)
			{
				idSelect--;
			}
			GameCanvas.ClearkeyMove(1);
		}
		else if (GameCanvas.keyMove(3))
		{
			if (idSelect < 2)
			{
				idSelect++;
			}
			GameCanvas.ClearkeyMove(3);
		}
		else if (GameCanvas.keyMove(0))
		{
			setSelect(-1);
			GameCanvas.ClearkeyMove(0);
		}
		else if (GameCanvas.keyMove(2))
		{
			setSelect(1);
			GameCanvas.ClearkeyMove(2);
		}
		else if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (cmdList != null && idCommand < cmdList.size())
			{
				((iCommand)cmdList.elementAt(idCommand)).perform();
			}
		}
	}

	public void setSelect(int value)
	{
		mValue[idSelect] += value;
		if (mValue[idSelect] > mAuto[idSelect].Length - 1)
		{
			mValue[idSelect] = mAuto[idSelect].Length - 1;
		}
		if (mValue[idSelect] < 0)
		{
			mValue[idSelect] = 0;
		}
		setInfoHelp(T.mHelpGetItem[idSelect][mValue[idSelect]]);
	}

	public override void updatePointer()
	{
		int num = yDia;
		int num2 = xDia + 15;
		num += 15;
		num += hItem;
		for (int i = 0; i < mAuto.Length; i++)
		{
			int num3 = num2 + 70;
			for (int j = 0; j < mAuto[i].Length; j++)
			{
				if (GameCanvas.isPointSelect(num3 - wItem / 2, num + 6 - hItem / 2, wItem, hItem))
				{
					mValue[i] = j;
					GameCanvas.isPointerSelect = false;
					setInfoHelp(T.mHelpGetItem[i][j]);
					return;
				}
				num3 += wItem;
			}
			num += hItem;
		}
		base.updatePointer();
	}
}
