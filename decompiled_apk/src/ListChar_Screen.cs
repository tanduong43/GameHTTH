using System;

public class ListChar_Screen : MainScreen
{
	private int selectChar;

	public static mVector vecListChar = new mVector("ListChar_Screen.vecListChar");

	public int[][] mPosShow;

	private iCommand cmdSelect;

	private iCommand cmdExit;

	private iCommand cmdDelChar;

	private iCommand cmdStart;

	public static sbyte IndexCharSelected = -1;

	public static short IDChar = 0;

	public static bool isDelChar = false;

	public const sbyte CHAR_DEL = 1;

	public const sbyte CHAR_LOCK = 2;

	private int timeSpec;

	private mVector vecDelButton = new mVector("ListChar_Screen.vecDelButton");

	private sbyte typeSelect;

	public const sbyte SELECT_CHAR = 0;

	public const sbyte SELECT_SUPPORT = 1;

	public const sbyte SELECT_OK = 2;

	private int[] mActionFire = new int[15]
	{
		8, 8, 8, 9, 9, 9, 10, 10, 10, 10,
		10, 10, 10, 10, 10
	};

	private Boat boat = new Boat(0, MotherCanvas.w / 7 * 4, MotherCanvas.h - 105, 0, 2);

	private static int frameIconfocus = 0;

	private static bool isNextFrame = true;

	private int wpl = 40;

	private int hpl = 80;

	private int vySea = 4;

	private int dySea;

	public static ListChar_Screen gI()
	{
		if (GameCanvas.listCharScr == null)
		{
			GameCanvas.listCharScr = new ListChar_Screen();
		}
		return GameCanvas.listCharScr;
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
		{
			if (isDelChar)
			{
				isDelChar = false;
				setCmd();
				break;
			}
			if (selectChar > vecListChar.size() - 1)
			{
				CreateChar_Screen.gI().Show(this);
				break;
			}
			Other_Player other_Player2 = (Other_Player)vecListChar.elementAt(selectChar);
			if (other_Player2.typeSpecCharList == 1)
			{
				GlobalService.gI().Del_Char(1, other_Player2.ID);
			}
			else if (typeSelect == 0)
			{
				IndexCharSelected = (sbyte)selectChar;
				IDChar = other_Player2.ID;
				other_Player2.Action = 2;
				other_Player2.f = 0;
				typeSelect = 2;
				setCmd();
			}
			else if (typeSelect == 1)
			{
				if (other_Player2.ID != IDChar)
				{
					other_Player2.Action = 2;
					other_Player2.f = 0;
					typeSelect = 2;
					other_Player2.strChatPopup = T.toisegiup;
				}
			}
			else if (typeSelect == 2)
			{
				GlobalService.gI().Select_Char(IDChar, 1, other_Player2.ID);
				GlobalService.gI().get_DATA(3);
				GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: false);
				GameCanvas.saveRms.saveUserPass(GameCanvas.loginScr.tfUser.getText(), GameCanvas.loginScr.tfPass.getText());
			}
			break;
		}
		case 1:
			GameCanvas.loginScr.Show();
			break;
		case 2:
			if (isDelChar)
			{
				if (!GameCanvas.isTouch)
				{
					commandPointer(4, selectChar);
				}
			}
			else
			{
				GameCanvas.Start_Normal_DiaLog(T.hoiXoaChar, new iCommand("Ok", 3, this), isCmdClose: true);
			}
			break;
		case 3:
			isDelChar = true;
			setCmd();
			GameCanvas.end_Dialog();
			break;
		case 4:
		{
			selectChar = subIndex;
			if (selectChar < 0 || selectChar >= vecListChar.size())
			{
				return;
			}
			Other_Player other_Player = (Other_Player)vecListChar.elementAt(selectChar);
			GlobalService.gI().Del_Char(0, other_Player.ID);
			GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: true);
			break;
		}
		case 5:
			GlobalService.gI().Select_Char(IDChar, 0, 0);
			GlobalService.gI().get_DATA(3);
			GameCanvas.saveRms.saveUserPass(GameCanvas.loginScr.tfUser.getText(), GameCanvas.loginScr.tfPass.getText());
			break;
		}
		base.commandPointer(index, subIndex);
	}

	public override void Show()
	{
		center = null;
		left = null;
		right = null;
		okCMD = null;
		typeSelect = 0;
		isDelChar = false;
		timeSpec = 0;
		mPosShow = mSystem.new_M_Int(3, 2);
		mPosShow[0][0] = MotherCanvas.hw;
		mPosShow[0][1] = MotherCanvas.h - 50;
		mPosShow[1][0] = MotherCanvas.hw - 60;
		mPosShow[1][1] = MotherCanvas.h - 40;
		mPosShow[2][0] = MotherCanvas.hw + 60;
		mPosShow[2][1] = MotherCanvas.h - 40;
		cmdSelect = new iCommand(T.select, 0, this);
		cmdExit = new iCommand(T.exit, 1, this);
		cmdExit = AvMain.setPosCMD(cmdExit, 1);
		cmdStart = new iCommand(T.startgame, 5, this);
		cmdStart = AvMain.setPosCMD(cmdStart, 0);
		if (!GameCanvas.isTouch)
		{
			cmdStart = AvMain.setPosCMD(cmdStart, 2);
		}
		cmdDelChar = new iCommand(T.delChar, 2, this);
		cmdDelChar = AvMain.setPosCMD(cmdDelChar, 2);
		setCmd();
		for (int i = 0; i < vecListChar.size(); i++)
		{
			Other_Player obj = (Other_Player)vecListChar.elementAt(i);
			obj.x = mPosShow[i][0];
			obj.y = mPosShow[i][1];
		}
		GameScreen.player = null;
		if (GameCanvas.mapBack == null)
		{
			GameCanvas.mapBack = new MapBackGround();
		}
		if (GameCanvas.currentScreen != GameCanvas.loginScr && GameCanvas.currentScreen != CreateChar_Screen.gI())
		{
			GameCanvas.mapBack.setBGLogin();
		}
		if (GameCanvas.currentScreen == GameCanvas.loginScr)
		{
			selectChar = 0;
		}
		boat.isPaint = true;
		if (LoadMapScreen.isMapSky != 0)
		{
			LoadMapScreen.isMapSky = 0;
			LoadImageStatic.loadImageEffBoat();
		}
		base.Show();
		GameCanvas.clearAll();
	}

	public override void paint(mGraphics g)
	{
		try
		{
			if (GameCanvas.mapBack != null)
			{
				GameCanvas.mapBack.paintBgLogin(g);
				GameCanvas.mapBack.paintObjFristLogin(g);
			}
		}
		catch (Exception)
		{
		}
		LoginScreen.paintLogo(g, MotherCanvas.hw);
		for (int i = 0; i < vecDelButton.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecDelButton.elementAt(i);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
		if (boat.isPaint)
		{
			boat.paintFrist(g);
			boat.paintBuom(g);
			boat.paintLast(g, GameCanvas.Day_Night);
		}
		for (int j = 0; j < 3; j++)
		{
			if (j < vecListChar.size())
			{
				Other_Player other_Player = (Other_Player)vecListChar.elementAt(j);
				other_Player.paintShadow(g, other_Player.x);
				int y = other_Player.y - other_Player.dy;
				if (other_Player.boatSea != null)
				{
					other_Player.boatSea.paintFrist(g);
					other_Player.boatSea.paintBuom(g);
					y = other_Player.y - dySea / 10;
				}
				other_Player.paintCharSelect(g, other_Player.x, y, other_Player.type_left_right, other_Player.frame);
				if (other_Player.boatSea != null)
				{
					other_Player.boatSea.paintLast(g, GameCanvas.Day_Night);
				}
			}
			else if (!isDelChar)
			{
				g.drawImage(MainObject.imgShadow, mPosShow[j][0] + 1, mPosShow[j][1], 3);
				if (j < 2 || vecListChar.size() > 1)
				{
					AvMain.paintRect(g, mPosShow[j][0] - 18, mPosShow[j][1] - 32, 36, 16, 1, 4);
					mFont.tahoma_7b_white.drawString(g, T.create, mPosShow[j][0], mPosShow[j][1] - 30, 2);
				}
			}
			if (j == selectChar && GameCanvas.isTouchNoOrPC())
			{
				if (isDelChar)
				{
					g.drawImage(AvMain.imgIconDel, mPosShow[j][0], mPosShow[j][1] - 52, 3);
				}
				else
				{
					paintFocus(g, mPosShow[j][0], mPosShow[j][1] - 52);
				}
			}
		}
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.paintObjLastLogin(g);
		}
		for (int k = 0; k < vecListChar.size(); k++)
		{
			if (k < vecListChar.size())
			{
				Other_Player other_Player2 = (Other_Player)vecListChar.elementAt(k);
				sbyte color = 0;
				if (other_Player2.typeSpecCharList == 1)
				{
					color = 6;
				}
				else if (other_Player2.typeSpecCharList == 2)
				{
					color = 3;
				}
				other_Player2.paintName(g, color, -1);
				other_Player2.paintLV(g, color);
				if (other_Player2.chat != null)
				{
					other_Player2.chat.paint(g);
				}
			}
		}
		if (right != null)
		{
			right.paint(g, right.xCmd, right.yCmd);
		}
	}

	public void paintFocus(mGraphics g, int x, int y)
	{
		if (AvMain.fraIconfocus.nFrame == 1)
		{
			g.drawImage(AvMain.fraIconfocus.imgFrame, x, y - GameCanvas.gameTick % 5, 3);
			return;
		}
		if (isNextFrame)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				frameIconfocus++;
			}
			if (frameIconfocus >= AvMain.fraIconfocus.nFrame - 1)
			{
				isNextFrame = false;
			}
		}
		else
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				frameIconfocus--;
			}
			if (frameIconfocus <= 0)
			{
				isNextFrame = true;
			}
		}
		AvMain.fraIconfocus.drawFrame(frameIconfocus, x, y - 3, 0, 3, g);
	}

	public override void update()
	{
		LoginScreen.updateYPaintLogo(LoginScreen.hLogo);
		if (vecListChar.size() > 1)
		{
			CRes.quickSort(vecListChar);
		}
		boat.updateXY(MotherCanvas.w / 7 * 4, MotherCanvas.h - 105, dySea / 10, (sbyte)boat.Dir);
		updateDySea();
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.updateCloudLogin();
		}
		if (timeSpec % 100 == 0)
		{
			int num = timeSpec / 100 % 3;
			if (num < vecListChar.size())
			{
				Other_Player other_Player = (Other_Player)vecListChar.elementAt(num);
				if (other_Player.typeSpecCharList == 1)
				{
					other_Player.strChatPopup = T.chatXoaChar + " " + CRes.getDay((int)other_Player.timeDie);
				}
				else if (other_Player.typeSpecCharList == 2)
				{
					other_Player.strChatPopup = T.chatKhoaChar + " " + CRes.getDay((int)other_Player.timeDie);
				}
				else
				{
					timeSpec += 90;
				}
			}
		}
		for (int i = 0; i < vecListChar.size(); i++)
		{
			Other_Player other_Player2 = (Other_Player)vecListChar.elementAt(i);
			other_Player2.ySort = other_Player2.y;
			other_Player2.updateChatPopup(14);
			other_Player2.updateDirPaint();
			other_Player2.f++;
			if (other_Player2.Action == 0)
			{
				if (other_Player2.f > other_Player2.feStand.Length - 1)
				{
					other_Player2.f = 0;
				}
				other_Player2.frame = other_Player2.feStand[other_Player2.f];
			}
			else if (other_Player2.Action == 2)
			{
				if (other_Player2.f > mActionFire.Length - 1)
				{
					other_Player2.f = 0;
					other_Player2.Action = 1;
					other_Player2.toX = boat.x - 10;
					other_Player2.toY = boat.y + 24;
					if (typeSelect == 2)
					{
						cmdSelect.perform();
					}
				}
				else
				{
					other_Player2.frame = mActionFire[other_Player2.f];
				}
			}
			else if (other_Player2.Action == 1)
			{
				if (other_Player2.f > other_Player2.feRun.Length - 1)
				{
					other_Player2.f = 0;
				}
				other_Player2.frame = other_Player2.feRun[other_Player2.f];
				other_Player2.x += other_Player2.vx;
				other_Player2.y += other_Player2.vy;
				if (MainObject.getDistance(other_Player2.x, other_Player2.y, other_Player2.toX, other_Player2.toY) < 12)
				{
					other_Player2.vx = 0;
					other_Player2.vy = 0;
					other_Player2.Action = 5;
					if (typeSelect == 1)
					{
						other_Player2.vx = (boat.x - 10 - other_Player2.x) / 6;
						other_Player2.vy = (boat.y - other_Player2.y) / 6;
					}
					else
					{
						other_Player2.vx = (boat.x - 10 - other_Player2.x) / 6;
						other_Player2.vy = (boat.y - 2 - other_Player2.y) / 6;
					}
					other_Player2.f = 0;
					other_Player2.type_left_right = 2;
					other_Player2.Dir = 2;
				}
				else
				{
					other_Player2.move_to_XY_Normal();
				}
			}
			else
			{
				if (other_Player2.Action != 5)
				{
					continue;
				}
				other_Player2.x += other_Player2.vx;
				other_Player2.y += other_Player2.vy;
				other_Player2.frame = 60;
				if (other_Player2.f < 2)
				{
					other_Player2.dy = (other_Player2.f + 1) * 5;
				}
				else if (other_Player2.f == 2 || other_Player2.f == 3)
				{
					other_Player2.dy = 12;
				}
				else if (other_Player2.dy > 3)
				{
					other_Player2.dy = 12 - (other_Player2.f - 3) * 5;
				}
				if (other_Player2.f != 5)
				{
					continue;
				}
				other_Player2.dy = 0;
				other_Player2.Action = 0;
				other_Player2.vx = 0;
				other_Player2.vy = 0;
				if (typeSelect == 1)
				{
					other_Player2.x = boat.x - 10;
					other_Player2.y = boat.y;
					boat.isPaint = false;
					other_Player2.boatSea = boat;
					if (vecListChar.size() > 1)
					{
						other_Player2.strChatPopup = T.chonhotro;
					}
				}
				else
				{
					other_Player2.x = boat.x - 30;
					other_Player2.y = boat.y - 2;
				}
				if (vecListChar.size() == 1)
				{
					GlobalService.gI().Select_Char(IDChar, 0, other_Player2.ID);
					GlobalService.gI().get_DATA(3);
					GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: false);
				}
				break;
			}
		}
		timeSpec++;
		if (typeSelect != 1 || vecListChar.size() <= 1)
		{
			return;
		}
		if (selectChar >= 0 && selectChar < vecListChar.size())
		{
			if (selectChar == IndexCharSelected)
			{
				selectChar++;
			}
		}
		else
		{
			selectChar++;
		}
	}

	public override void updatekey()
	{
		if (typeSelect == 2)
		{
			return;
		}
		if (GameCanvas.keyMove(0))
		{
			if (typeSelect == 1 && IndexCharSelected == 1)
			{
				if (selectChar == 2)
				{
					selectChar = 0;
				}
			}
			else if (selectChar == 0 || vecListChar.size() > 1)
			{
				if (selectChar == 0)
				{
					selectChar = 1;
				}
				else if (selectChar == 1)
				{
					selectChar = 2;
				}
				else if (selectChar == 2)
				{
					selectChar = 0;
				}
			}
			GameCanvas.ClearkeyMove(0);
		}
		else if (GameCanvas.keyMove(2))
		{
			if (typeSelect == 1 && IndexCharSelected == 0)
			{
				if (selectChar == 1)
				{
					selectChar = 2;
				}
			}
			else if (selectChar == 1 || vecListChar.size() > 1)
			{
				if (selectChar == 0)
				{
					selectChar = 2;
				}
				else if (selectChar == 1)
				{
					selectChar = 0;
				}
				else if (selectChar == 2)
				{
					selectChar = 1;
				}
			}
			GameCanvas.ClearkeyMove(2);
		}
		if (isDelChar)
		{
			selectChar = AvMain.resetSelect(selectChar, vecListChar.size() - 1, isreset: true);
		}
		else
		{
			selectChar = AvMain.resetSelect(selectChar, 2, isreset: true);
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		if (typeSelect == 2)
		{
			return;
		}
		if (GameCanvas.isPointerSelect)
		{
			for (int i = 0; i < mPosShow.Length; i++)
			{
				if (!GameCanvas.isPoint(mPosShow[i][0] - wpl / 2, mPosShow[i][1] - hpl + 10, wpl, hpl))
				{
					continue;
				}
				if (isDelChar)
				{
					if (i < vecListChar.size())
					{
						selectChar = i;
						cmdDelChar.perform();
					}
				}
				else if (i <= vecListChar.size())
				{
					selectChar = i;
					cmdSelect.perform();
				}
				GameCanvas.isPointerSelect = false;
				break;
			}
		}
		for (int j = 0; j < vecDelButton.size(); j++)
		{
			((iCommand)vecDelButton.elementAt(j)).updatePointer();
		}
		base.updatePointer();
	}

	public void setCmd()
	{
		vecDelButton.removeAllElements();
		if (isDelChar)
		{
			if (GameCanvas.isTouchNoOrPC())
			{
				cmdDelChar = new iCommand(T.delChar, 2, this);
				center = cmdDelChar;
			}
			cmdSelect = new iCommand(T.back, 0, this);
			cmdSelect = AvMain.setPosCMD(cmdSelect, 2);
			right = cmdSelect;
			left = cmdExit;
			if (GameCanvas.isTouch)
			{
				for (int i = 0; i < vecListChar.size(); i++)
				{
					Other_Player other_Player = (Other_Player)vecListChar.elementAt(i);
					iCommand iCommand2 = new iCommand(T.del, 4, i, this);
					iCommand2.setPos(other_Player.x, other_Player.y - 92, null, iCommand2.caption);
					iCommand2.levelSmall = 3;
					vecDelButton.addElement(iCommand2);
				}
			}
		}
		else if (typeSelect == 0)
		{
			cmdSelect = new iCommand(T.select, 0, this);
			if (GameCanvas.isTouchNoOrPC())
			{
				center = cmdSelect;
			}
		}
		else if (typeSelect == 1)
		{
			if (vecListChar.size() > 1)
			{
				cmdSelect = new iCommand(T.support, 0, this);
				if (GameCanvas.isTouchNoOrPC())
				{
					center = cmdSelect;
				}
				left = null;
				right = cmdStart;
			}
			else
			{
				center = null;
				left = null;
				right = null;
			}
		}
		else if (typeSelect == 2)
		{
			center = null;
			left = null;
			right = null;
		}
	}

	private void updateDySea()
	{
		if (CRes.random(40) == 0)
		{
			if (CRes.random(2) == 0)
			{
				vySea = 4;
			}
			else
			{
				vySea = -4;
			}
		}
		if (dySea > 0 && vySea > 0)
		{
			vySea = -4;
		}
		else if (dySea < -50 && vySea < 0)
		{
			vySea = 4;
		}
		dySea += vySea;
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdExit != null)
		{
			cmdExit.perform();
		}
		return false;
	}
}
