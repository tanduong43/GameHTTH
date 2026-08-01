public class MsgAutoMPHP : MsgDialog
{
	public static int mp = 30;

	public static int hp = 30;

	public static int typeUu;

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 4)
		{
			Player.isMPHP = true;
			isClose = true;
			GameScreen.player.hpPoi = null;
			GameScreen.player.mpPoi = null;
			GameCanvas.saveRms.SaveAutoMp_Hp();
		}
	}

	public void setinfoAuto_MP_HP()
	{
		fontDia = mFont.tahoma_7_black;
		beginDia();
		cmdList = new mVector();
		iCommand o = new iCommand(T.xong, 4, this);
		cmdList.addElement(o);
		okCMD = o;
		wDia = 160;
		maxWShow = wDia;
		wShowPaper = 5;
		wItem = 28;
		hDia = 160;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 - 5;
		setPosCmdNew(-2, isLast: false);
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia;
		int num2 = xDia + 30;
		paintPaper(g, MotherCanvas.hw - wShowPaper / 2, num, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
		g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		num += 12;
		g.setColor(15972174);
		g.fillRoundRect(xDia + 10, num, wDia - 20, 16, 4, 4);
		num += 3;
		AvMain.FontBorderColor(g, T.setting, xDia + wDia / 2, num, 2, 6, 5);
		num += wItem;
		if (GameCanvas.isTouchNoOrPC())
		{
			paintSelect(g, num2, num - wItem / 4 - 1, 100 + wItem);
		}
		g.drawRegion(Interface_Game.imgIconMPHP, 0, 0, 10, 10, 0, num2 + 4, num + 6, 3);
		Interface_Game.PaintHPMP(g, 1, hp * 10, 100, num2 + 10, num, 0, 12, 90, 1, isflip: false, 0, isUpdateEff: false, 0);
		num += wItem;
		g.drawRegion(Interface_Game.imgIconMPHP, 0, 10, 10, 10, 0, num2 + 4, num + 6, 3);
		Interface_Game.PaintHPMP(g, 2, mp * 10, 100, num2 + 10, num, 0, 12, 90, 1, isflip: false, 0, isUpdateEff: false, 0);
		num += wItem;
		mFont.tahoma_7b_black.drawString(g, T.uutien, num2, num, 0);
		g.drawImage(AvMain.imgHotKey, num2 + 55 + 30 * typeUu, num + 6, 3);
		if (MsgDialog.fraAutoMpHp == null)
		{
			MsgDialog.fraAutoMpHp = new FrameImage(mImage.createImage("/interface/automphp.png"), 20, 20);
		}
		else
		{
			MsgDialog.fraAutoMpHp.drawFrame(0, num2 + 55, num + 6, 0, 3, g);
			MsgDialog.fraAutoMpHp.drawFrame(1, num2 + 85, num + 6, 0, 3, g);
		}
		paintInfoHelp(g);
		if (cmdList != null)
		{
			for (int i = 0; i < cmdList.size(); i++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		g.restoreCanvas();
	}

	public void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(16774758);
		xbegin -= wItem / 2;
		g.fillRect(xbegin + miniItem / 2, ybegin + idSelect * wItem, wFocus - miniItem / 2, wItem);
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
		updatekeyPC();
	}

	public void setSelect(int value)
	{
		if (idSelect == 0)
		{
			hp += value * 10;
			if (hp > 90)
			{
				hp = 90;
			}
			if (hp < 0)
			{
				hp = 0;
			}
			setInfoHelp(T.mHelpAutoMPHP[0] + hp + "%");
		}
		else if (idSelect == 1)
		{
			mp += value * 10;
			if (mp > 90)
			{
				mp = 90;
			}
			if (mp < 0)
			{
				mp = 0;
			}
			setInfoHelp(T.mHelpAutoMPHP[1] + mp + "%");
		}
		else if (idSelect == 2)
		{
			typeUu++;
			if (typeUu > 1)
			{
				typeUu = 0;
			}
			setInfoHelp(T.mHelpAutoMPHP[2 + typeUu]);
		}
	}

	public override void updatePointer()
	{
		int num = yDia;
		int num2 = xDia + 30;
		num += 15 + wItem - wItem / 4;
		if (GameCanvas.isPointLast(num2 + 10, num, 90, wItem))
		{
			if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
			{
				int select = (GameCanvas.px - (num2 + 10) + 5) / 9 * 10;
				hp = AvMain.resetSelect(select, 90, isreset: false);
				setInfoHelp(T.mHelpAutoMPHP[0] + hp + "%");
			}
		}
		else if (GameCanvas.isPointLast(num2 + 10, num + wItem, 90, wItem))
		{
			if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
			{
				int select2 = (GameCanvas.px - (num2 + 10) + 5) / 9 * 10;
				mp = AvMain.resetSelect(select2, 90, isreset: false);
				setInfoHelp(T.mHelpAutoMPHP[1] + mp + "%");
			}
		}
		else if (GameCanvas.isPoint(num2 + 55 - 15, num + wItem * 2, 60, wItem) && GameCanvas.isPointerSelect)
		{
			if (GameCanvas.px < num2 + 55 + 15)
			{
				typeUu = 0;
			}
			else
			{
				typeUu = 1;
			}
			GameCanvas.isPointerSelect = false;
			setInfoHelp(T.mHelpAutoMPHP[2 + typeUu]);
		}
		base.updatePointer();
	}
}
