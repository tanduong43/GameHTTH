public class CreateChar_Screen : MainScreen
{
	public static CreateChar_Screen instance;

	private TField tfNameChar;

	private iCommand cmdCreate;

	private iCommand cmdexit;

	private int xBegin;

	private int yBegin;

	private int h;

	private Other_Player otherPl;

	private mVector vecHeadShow = new mVector("CreateChar_Screen.vecHeadShow");

	private int wItem = 30;

	public static short[][] hardcodePart = new short[20][]
	{
		new short[2] { 1, 41 },
		new short[3] { -1, 2, 42 },
		new short[2] { 3, 43 },
		new short[2] { 44, 4 },
		new short[2] { 45, 24 },
		new short[3] { -1, 46, 25 },
		new short[2] { 47, 26 },
		new short[2] { 48, 27 },
		new short[2] { 49, 28 },
		new short[3] { -1, 29, 50 },
		new short[2] { 30, 51 },
		new short[2] { 31, 52 },
		new short[2] { 32, 53 },
		new short[3] { -1, 33, 58 },
		new short[2] { 34, 55 },
		new short[2] { 35, 56 },
		new short[2] { 36, 57 },
		new short[2] { -1, 37 },
		new short[2] { 38, 59 },
		new short[2] { 39, 60 }
	};

	public const int TOC = 0;

	public const int NON = 1;

	public const int AO = 2;

	public const int QUAN = 3;

	public CreateChar_Screen()
	{
		yBegin = MotherCanvas.h - 55;
		xBegin = MotherCanvas.hw - 40;
		tfNameChar = new TField(MotherCanvas.hw - 40, yBegin, 80);
		tfNameChar.setStringNull(T.tfNameCharNull);
		cmdCreate = new iCommand(T.create, 0, this);
		cmdexit = new iCommand(T.back, 1, this);
		if (!GameCanvas.isTouch)
		{
			cmdexit = AvMain.setPosCMD(cmdexit, 1);
			cmdCreate = AvMain.setPosCMD(cmdCreate, 2);
			center = cmdCreate;
			cmdCreate = AvMain.setPosCMD(cmdCreate, 0);
			right = tfNameChar.cmdClear;
			left = cmdexit;
		}
		else
		{
			cmdexit = AvMain.setPosCMD(cmdexit, 2);
			cmdCreate = AvMain.setPosCMD(cmdCreate, 1);
			left = cmdCreate;
			right = cmdexit;
			backCMD = cmdexit;
			okCMD = cmdCreate;
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			tfNameChar.setFocus(isFocus: true);
		}
		if (vecHeadShow.size() == 0)
		{
			for (int i = 1; i < 6; i++)
			{
				otherPl = new Other_Player(0, 0, string.Empty, 0, 0);
				otherPl.clazz = (sbyte)i;
				otherPl = setCharClass(otherPl, isRan: false);
				vecHeadShow.addElement(otherPl);
			}
		}
	}

	public static CreateChar_Screen gI()
	{
		return (instance != null) ? instance : (instance = new CreateChar_Screen());
	}

	public override void Show(MainScreen screen)
	{
		base.Show(screen);
		otherPl = new Other_Player(0, 0, string.Empty, MotherCanvas.hw, yBegin - 10);
		otherPl.clazz = (sbyte)CRes.random(1, 6);
		otherPl = setCharClass(otherPl, isRan: false);
		tfNameChar.setText(string.Empty);
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
			if (tfNameChar.getText().Length > 0)
			{
				GlobalService.gI().Create_Char(tfNameChar.getText(), otherPl.clazz, otherPl.head, otherPl.hair);
			}
			else
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.checkCreate);
			}
			break;
		case 1:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.loginScr.Show();
			}
			break;
		case 2:
		{
			Other_Player other_Player = otherPl;
			other_Player.clazz++;
			if (otherPl.clazz > 5)
			{
				otherPl.clazz = 1;
			}
			otherPl = setCharClass(otherPl, isRan: false);
			break;
		}
		}
		base.commandPointer(index, subIndex);
	}

	public override void paint(mGraphics g)
	{
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.paintBgLogin(g);
			GameCanvas.mapBack.paintObjFristLogin(g);
			GameCanvas.mapBack.paintObjLastLogin(g);
		}
		for (int i = 0; i < vecHeadShow.size(); i++)
		{
			Other_Player other_Player = (Other_Player)vecHeadShow.elementAt(i);
			int idx = 0;
			if (other_Player.clazz == otherPl.clazz)
			{
				idx = 1;
			}
			if (AvMain.fraBorder == null)
			{
				AvMain.fraBorder = new FrameImage(mImage.createImage("/interface/border.png"), 26, 26);
			}
			else
			{
				AvMain.fraBorder.drawFrame(idx, wItem / 2, wItem / 2 + i * wItem + GameScreen.h12plus, 0, 3, g);
			}
			MainObject.paintHeadEveryWhere(g, other_Player.head, other_Player.hair, other_Player.hat, wItem / 2 + 2, wItem / 2 + i * wItem + 37 + GameScreen.h12plus, 2);
		}
		GameCanvas.resetTrans(g);
		tfNameChar.paint(g);
		otherPl.paintShadow(g, otherPl.x);
		otherPl.paintCharShow(g, otherPl.x, otherPl.y, 0, isNhip: true);
		if (otherPl.chat != null)
		{
			otherPl.chat.paint(g);
		}
		base.paint(g);
	}

	public override void update()
	{
		tfNameChar.update();
		otherPl.updateChatPopup(10);
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.updateCloudLogin();
		}
	}

	public override void updatekey()
	{
		int num = otherPl.clazz;
		base.updatekey();
		if (GameCanvas.keyMyHold[8])
		{
			GameCanvas.clearKeyHold(8);
			num++;
		}
		else if (GameCanvas.keyMyHold[2])
		{
			GameCanvas.clearKeyHold(2);
			num--;
		}
		if (num != otherPl.clazz)
		{
			if (num > 5)
			{
				otherPl.clazz = 1;
			}
			else if (num < 1)
			{
				otherPl.clazz = 5;
			}
			else
			{
				otherPl.clazz = (sbyte)num;
			}
			otherPl = setCharClass(otherPl, isRan: false);
		}
	}

	public override void updatePointer()
	{
		tfNameChar.updatePointer();
		if (GameCanvas.isPointSelect(0, GameScreen.h12plus, wItem, wItem * 5))
		{
			int num = (GameCanvas.py - GameScreen.h12plus) / wItem;
			if (num > 4)
			{
				num = 4;
			}
			otherPl.clazz = (sbyte)(num + 1);
			otherPl = setCharClass(otherPl, isRan: false);
			GameCanvas.isPointerSelect = false;
		}
		base.updatePointer();
	}

	public override void keyPress(int keyCode)
	{
		tfNameChar.keyPressed(keyCode);
	}

	public static Other_Player setCharClass(Other_Player other, bool isRan)
	{
		switch (other.clazz)
		{
		case 1:
			other.head = 0;
			if (isRan)
			{
				other.hair = RanPart(0, 0);
			}
			else
			{
				other.hair = 1;
			}
			if (isRan)
			{
				other.hat = RanPart(0, 1);
			}
			else
			{
				other.hat = -1;
			}
			if (isRan)
			{
				other.body = RanPart(0, 2);
			}
			else
			{
				other.body = 3;
			}
			if (isRan)
			{
				other.leg = RanPart(0, 3);
			}
			else
			{
				other.leg = 4;
			}
			other.weapon = -1;
			other.IdEffShow = 33;
			break;
		case 2:
			other.head = 0;
			if (isRan)
			{
				other.hair = RanPart(1, 0);
			}
			else
			{
				other.hair = 24;
			}
			if (isRan)
			{
				other.hat = RanPart(1, 1);
			}
			else
			{
				other.hat = -1;
			}
			if (isRan)
			{
				other.body = RanPart(1, 2);
			}
			else
			{
				other.body = 26;
			}
			if (isRan)
			{
				other.leg = RanPart(1, 3);
			}
			else
			{
				other.leg = 27;
			}
			other.weapon = 5;
			other.IdEffShow = 15;
			break;
		case 3:
			other.head = 0;
			if (isRan)
			{
				other.hair = RanPart(2, 0);
			}
			else
			{
				other.hair = 28;
			}
			if (isRan)
			{
				other.hat = RanPart(2, 1);
			}
			else
			{
				other.hat = -1;
			}
			if (isRan)
			{
				other.body = RanPart(2, 2);
			}
			else
			{
				other.body = 30;
			}
			if (isRan)
			{
				other.leg = RanPart(2, 3);
			}
			else
			{
				other.leg = 31;
			}
			other.weapon = 180;
			other.IdEffShow = 44;
			break;
		case 4:
			other.head = 0;
			if (isRan)
			{
				other.hair = RanPart(3, 0);
			}
			else
			{
				other.hair = 32;
			}
			if (isRan)
			{
				other.hat = RanPart(3, 1);
			}
			else
			{
				other.hat = -1;
			}
			if (isRan)
			{
				other.body = RanPart(3, 2);
			}
			else
			{
				other.body = 34;
			}
			if (isRan)
			{
				other.leg = RanPart(3, 3);
			}
			else
			{
				other.leg = 35;
			}
			other.weapon = 6;
			other.IdEffShow = 51;
			break;
		case 5:
			other.head = 0;
			other.hair = 36;
			other.hat = -1;
			other.body = 38;
			other.leg = 39;
			other.weapon = 7;
			other.IdEffShow = 7;
			break;
		case 6:
			other.head = 8;
			other.hair = -1;
			other.hat = 11;
			other.body = 9;
			other.leg = 10;
			other.weapon = -1;
			other.IdEffShow = 7;
			break;
		}
		if (other.clazz < T.mTalkChar.Length)
		{
			other.strChatPopup = T.mTalkChar[other.clazz];
		}
		return other;
	}

	public static short RanPart(int clazz, int index)
	{
		return hardcodePart[clazz * 4 + index][CRes.random(hardcodePart[clazz * 4].Length)];
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdexit != null)
		{
			cmdexit.perform();
		}
		return false;
	}
}
