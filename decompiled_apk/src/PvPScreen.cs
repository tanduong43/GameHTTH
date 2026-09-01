public class PvPScreen : MainScreen
{
	public int w;

	public int h;

	public int x;

	public int y;

	public int yPaintChar;

	public int idCommand;

	public int xMoveEff;

	public int vxMoveEff;

	public MainObject objPvP;

	public string[] mstrShow;

	public iCommand cmdFind;

	public iCommand cmdCancle;

	public iCommand cmdReady;

	public iCommand cmdFindCancle;

	public const sbyte PVP_OPEN = 0;

	public const sbyte PVP_FIND = 1;

	public const sbyte PVP_FIND_CANCLE = 2;

	public const sbyte PVP_FIND_OK = 3;

	public const sbyte PVP_READY = 4;

	public const sbyte PVP_CANCLE = 5;

	public const sbyte PVP_ERROR = 6;

	public mVector vecCmd = new mVector();

	public bool isMeReady;

	public bool isOtherReady;

	public bool isfind;

	private long timeBeginReady;

	public short timeReady;

	public short numPlayerWait;

	public static PvPScreen instance;

	public PvPScreen()
	{
		w = 180;
		h = 190;
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2 + GameCanvas.hCommand / 2;
		yPaintChar = y + 78;
		cmdFind = new iCommand(T.find, 0, this);
		cmdCancle = new iCommand(T.cancel, 1, this);
		cmdReady = new iCommand(T.ready, 2, this);
		cmdFindCancle = new iCommand(T.findCancle, 3, this);
		if (AvMain.imgPvpObjdef == null || AvMain.imgPvpVs == null || AvMain.imgPvpOk == null)
		{
			LoadImageStatic.LoadImgPvP();
		}
	}

	public override void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			GlobalService.gI().PvP(1);
			break;
		case 1:
			GlobalService.gI().PvP(5);
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			break;
		case 2:
			GlobalService.gI().PvP(4);
			break;
		case 3:
			GlobalService.gI().PvP(2);
			break;
		}
	}

	public void setAction(sbyte action)
	{
		isfind = false;
		vecCmd.removeAllElements();
		switch (action)
		{
		case 0:
			xMoveEff = 0;
			vxMoveEff = 0;
			timeReady = 0;
			isMeReady = false;
			isOtherReady = false;
			objPvP = null;
			vecCmd.addElement(cmdFind);
			vecCmd.addElement(cmdCancle);
			setMStr(T.PvPOpen + " " + numPlayerWait + T.soluongnguoicho);
			if (instance != null)
			{
				instance.Show(GameCanvas.gameScr);
			}
			break;
		case 2:
			isMeReady = false;
			isOtherReady = false;
			vxMoveEff = -5;
			setMStr(T.PvPfindCancle);
			vecCmd.addElement(cmdFind);
			vecCmd.addElement(cmdCancle);
			timeReady = 0;
			break;
		case 6:
			isMeReady = false;
			isOtherReady = false;
			objPvP = null;
			vxMoveEff = -5;
			vecCmd.addElement(cmdFind);
			vecCmd.addElement(cmdCancle);
			timeReady = 0;
			break;
		case 1:
			isfind = true;
			isMeReady = false;
			isOtherReady = false;
			vxMoveEff = 5;
			setMStr(T.PvPfind);
			vecCmd.addElement(cmdFindCancle);
			break;
		case 3:
			timeBeginReady = GameCanvas.timeNow;
			timeReady = 30;
			setMStr(T.PvPfindOk);
			vecCmd.addElement(cmdReady);
			vecCmd.addElement(cmdCancle);
			break;
		case 4:
			if (isMeReady && isOtherReady)
			{
				timeReady = 0;
				setMStr(T.PvPready3);
			}
			else if (isMeReady)
			{
				timeReady = 0;
				setMStr(T.PvPready);
			}
			else if (isOtherReady)
			{
				vecCmd.addElement(cmdReady);
				vecCmd.addElement(cmdCancle);
				setMStr(T.PvPready2);
			}
			break;
		}
		setPosCmd();
	}

	public void setMStr(string str)
	{
		mstrShow = mFont.tahoma_7_black.splitFontArray(str, w - 10);
	}

	public void setObjPvp(MainObject obj)
	{
		objPvP = obj;
		setCharClass(objPvP.clazz);
	}

	public void setPosCmd()
	{
		idCommand = 0;
		if (vecCmd.size() == 1)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(0);
			iCommand2.setPos(MotherCanvas.hw, y + h - iCommand.hButtonCmdNor, null, iCommand2.caption);
			if (!GameCanvas.isTouch)
			{
				iCommand2.isSelect = true;
			}
			okCMD = iCommand2;
		}
		if (vecCmd.size() == 2)
		{
			iCommand iCommand3 = (iCommand)vecCmd.elementAt(0);
			iCommand3.setPos(MotherCanvas.hw - iCommand.wButtonCmd / 2, y + h - iCommand.hButtonCmdNor, null, iCommand3.caption);
			if (!GameCanvas.isTouch)
			{
				iCommand3.isSelect = true;
			}
			okCMD = iCommand3;
			iCommand iCommand4 = (iCommand)vecCmd.elementAt(1);
			iCommand4.setPos(MotherCanvas.hw + iCommand.wButtonCmd / 2, y + h - iCommand.hButtonCmdNor, null, iCommand4.caption);
			if (!GameCanvas.isTouch)
			{
				iCommand4.isSelect = false;
			}
			backCMD = iCommand4;
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		AvMain.paintBG_AChi(g, x - 12, y - 20, w + 24, h + 25, 2);
		GameScreen.player.paintCharShow(g, x + w / 2 - xMoveEff, yPaintChar, 2, isNhip: true);
		mFont.tahoma_7_black.drawString(g, GameScreen.player.name, x + w / 2 - xMoveEff, yPaintChar - 60, 2);
		if (objPvP != null)
		{
			objPvP.paintCharShow(g, x + w / 2 + xMoveEff, yPaintChar, 0, isNhip: true);
			mFont.tahoma_7_black.drawString(g, objPvP.name, x + w / 2 + xMoveEff, yPaintChar - 60, 2);
		}
		else if (xMoveEff == w / 4 || vxMoveEff != 0)
		{
			g.drawImage(AvMain.imgPvpObjdef, x + w / 2 + xMoveEff, yPaintChar + 2, 33);
		}
		if (xMoveEff > 12)
		{
			g.drawImage(AvMain.imgPvpVs, x + w / 2, yPaintChar - 24, 3);
		}
		if (isMeReady)
		{
			g.drawImage(AvMain.imgPvpOk, x + w / 2 - xMoveEff - 20, yPaintChar, 33);
		}
		if (isOtherReady)
		{
			if (AvMain.imgPvpOk == null)
			{
				LoadImageStatic.LoadImgPvP();
			}
			else
			{
				g.drawImage(AvMain.imgPvpOk, x + w / 2 + xMoveEff + 20, yPaintChar, 33);
			}
		}
		int num = yPaintChar + GameCanvas.hText / 2;
		mFont.tahoma_7_black.drawString(g, T.pointPvP + GameScreen.player.PointPvP, x + 10, num, 0);
		num += GameCanvas.hText;
		mFont.tahoma_7_black.drawString(g, T.thangthua + GameScreen.player.mValuePvP[0] + "/" + GameScreen.player.mValuePvP[1], x + 10, num, 0);
		num += GameCanvas.hText;
		if (mstrShow != null)
		{
			for (int i = 0; i < mstrShow.Length; i++)
			{
				string text = mstrShow[i];
				if (isfind)
				{
					if (GameCanvas.gameTick % 20 < 5)
					{
						text += ".";
					}
					else if (GameCanvas.gameTick % 20 < 10)
					{
						text += "..";
					}
					else if (GameCanvas.gameTick % 20 < 15)
					{
						text += "...";
					}
				}
				if (timeReady > 0 && i == mstrShow.Length - 1)
				{
					text = text + " " + timeReady + "s.";
				}
				mFont.tahoma_7_black.drawString(g, text, x + w / 2, num, 2);
				num += GameCanvas.hText;
			}
		}
		Interface_Game.paintEffShowMoney(g, MotherCanvas.hw - Interface_Game.wMoneyEff / 2, GameScreen.h12plus, isShow: false, 1);
		for (int j = 0; j < vecCmd.size(); j++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(j);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		if (vxMoveEff > 0)
		{
			if (xMoveEff < w / 4)
			{
				xMoveEff += vxMoveEff;
			}
			else
			{
				vxMoveEff = 0;
				xMoveEff = w / 4;
			}
		}
		else if (vxMoveEff < 0)
		{
			if (xMoveEff > 0)
			{
				xMoveEff += vxMoveEff;
			}
			else
			{
				xMoveEff = 0;
				vxMoveEff = 0;
			}
		}
		if (timeReady > 0 && GameCanvas.timeNow - timeBeginReady > 1000)
		{
			timeReady--;
			timeBeginReady += 1000L;
		}
	}

	public override void updatekey()
	{
		int num = vecCmd.size();
		if (!GameCanvas.isTouch && num > 0)
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
					iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
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
		if (GameCanvas.keyMyHold[5] && idCommand < vecCmd.size())
		{
			((iCommand)vecCmd.elementAt(idCommand)).perform();
			GameCanvas.clearKeyHold(5);
		}
		updatekeyPC();
	}

	public override void updatePointer()
	{
		for (int i = 0; i < vecCmd.size(); i++)
		{
			((iCommand)vecCmd.elementAt(i)).updatePointer();
		}
		base.updatePointer();
	}

	public void setCharClass(sbyte clazz)
	{
		switch (clazz)
		{
		case 1:
			objPvP.head = 0;
			objPvP.hair = 1;
			objPvP.hat = -1;
			objPvP.body = 3;
			objPvP.leg = 4;
			objPvP.weapon = -1;
			break;
		case 2:
			objPvP.head = 0;
			objPvP.hair = 24;
			objPvP.hat = -1;
			objPvP.body = 26;
			objPvP.leg = 27;
			objPvP.weapon = 5;
			break;
		case 3:
			objPvP.head = 0;
			objPvP.hair = 28;
			objPvP.hat = -1;
			objPvP.body = 30;
			objPvP.leg = 31;
			objPvP.weapon = 180;
			break;
		case 4:
			objPvP.head = 0;
			objPvP.hair = 32;
			objPvP.hat = -1;
			objPvP.body = 34;
			objPvP.leg = 35;
			objPvP.weapon = 6;
			break;
		case 5:
			objPvP.head = 0;
			objPvP.hair = 36;
			objPvP.hat = -1;
			objPvP.body = 38;
			objPvP.leg = 39;
			objPvP.weapon = 7;
			break;
		}
	}
}
