public class MsgSound : MsgDialog
{
	private bool isMusicCur;

	public void setinfoSound()
	{
		fontDia = mFont.tahoma_7_black;
		beginDia();
		cmdList = new mVector();
		iCommand o = new iCommand(T.xong, 12, this);
		cmdList.addElement(o);
		okCMD = o;
		wDia = 100;
		maxWShow = wDia;
		wShowPaper = 5;
		wItem = 28;
		hDia = 130;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 - 5;
		isMusicCur = mSound.isMusic;
		setPosCmdNew(-2, isLast: false);
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 12)
		{
			CRes.saveRMS("MAIN_SOUND", new sbyte[2]
			{
				(sbyte)(mSound.isMusic ? 1 : 0),
				(sbyte)(mSound.isSound ? 1 : 0)
			});
			stopMusic();
			closeDialog();
		}
	}

	public void stopMusic()
	{
		if (!mSound.isMusic)
		{
			mSound.stopAll();
		}
		else if (!isMusicCur)
		{
			mSound.idCurent = -1;
			LoadMapScreen.PlayMusicLang();
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia;
		int num2 = xDia + 20;
		paintPaper(g, MotherCanvas.hw - wShowPaper / 2, num, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
		g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		num += 12;
		g.setColor(15972174);
		g.fillRoundRect(xDia + 10, num, wDia - 20, 16, 4, 4);
		num += 3;
		AvMain.FontBorderColor(g, T.amthanh, xDia + wDia / 2, num, 2, 6, 5);
		num += wItem;
		if (GameCanvas.isTouchNoOrPC())
		{
			paintSelect(g, num2, num - wItem / 4 - 1, 58 + wItem);
		}
		g.drawImage(AvMain.imgBorderCombo, num2, num + 5, 3);
		mFont.tahoma_7b_black.drawString(g, T.nhacnen, num2 + 12, num, 0);
		if (mSound.isMusic)
		{
			AvMain.fraCheck.drawFrame(2, num2, num + 5, 0, 3, g);
		}
		num += wItem;
		g.drawImage(AvMain.imgBorderCombo, num2, num + 5, 3);
		if (mSound.isSound)
		{
			AvMain.fraCheck.drawFrame(2, num2, num + 5, 0, 3, g);
		}
		mFont.tahoma_7b_black.drawString(g, T.hieuung, num2 + 12, num, 0);
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
			if (idSelect < 1)
			{
				idSelect++;
			}
			GameCanvas.ClearkeyMove(3);
		}
		else if (GameCanvas.keyMove(0) || GameCanvas.keyMove(2))
		{
			setSelect();
			GameCanvas.ClearkeyMove(0);
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

	public void setSelect()
	{
		if (idSelect == 0)
		{
			mSound.isMusic = !mSound.isMusic;
		}
		else if (idSelect == 1)
		{
			mSound.isSound = !mSound.isSound;
		}
	}

	public override void updatePointer()
	{
		int num = yDia;
		int num2 = xDia;
		num += 15 + wItem - wItem / 4;
		if (GameCanvas.isPointSelect(num2 + 10, num, 80, wItem))
		{
			mSound.isMusic = !mSound.isMusic;
		}
		else if (GameCanvas.isPointSelect(num2 + 10, num + wItem, 80, wItem))
		{
			mSound.isSound = !mSound.isSound;
		}
		base.updatePointer();
	}
}
