public class QuickMenu : Menu
{
	public static QuickMenu instance;

	public static FrameImage[] fraQuickMenu;

	public static mImage imgNenMenu;

	public static mImage imgTamGiac;

	private ListNew list;

	private bool isClose;

	private int speed = 10;

	private int xEff;

	public static int[][] mValueCmd = new int[13][]
	{
		new int[2],
		new int[2] { 12, 12 },
		new int[2] { 7, 7 },
		new int[2] { 2, 2 },
		new int[2] { 14, 14 },
		new int[2] { 15, 15 },
		new int[2] { 13, 13 },
		new int[2] { 3, 3 },
		new int[2] { 5, 5 },
		new int[2] { 6, 6 },
		new int[2] { 9, 8 },
		new int[2] { 11, 11 },
		new int[2] { 8, 9 }
	};

	private int[] color = new int[4] { 9389312, 16777215, 16777215, 16771856 };

	public static QuickMenu gI()
	{
		return (instance != null) ? instance : (instance = new QuickMenu());
	}

	public void startAt()
	{
		beginMenu();
		isClose = false;
		cmdClose = new iCommand(string.Empty, -1, this);
		if (GameCanvas.isTouchNoOrPC())
		{
			menuSelectedItem = 0;
		}
		else
		{
			menuSelectedItem = -1;
		}
		menuItems = new mVector();
		for (int i = 0; i < mValueCmd.Length; i++)
		{
			if (mValueCmd[i][0] <= 5 || (mValueCmd[i][0] == 6 && Player.vecParty.size() > 0) || mValueCmd[i][0] == 7 || mValueCmd[i][0] == 8 || mValueCmd[i][0] == 9 || mValueCmd[i][0] == 12 || mValueCmd[i][0] == 11 || (mValueCmd[i][0] == 10 && GameScreen.player.clan != null) || mValueCmd[i][0] == 13 || mValueCmd[i][0] == 14 || mValueCmd[i][0] == 15)
			{
				iCommand iCommand2 = new iCommand(getTextCmd(mValueCmd[i][0]), mValueCmd[i][0], this);
				iCommand2.setFraCaption(fraQuickMenu[mValueCmd[i][1]]);
				menuItems.addElement(iCommand2);
			}
		}
		wUni = 60;
		xEff = -40;
		menuW = 50;
		menuX = 0;
		if (GameCanvas.isTaiTho)
		{
			menuX = 30;
		}
		menuY = 0;
		list = new ListNew(menuX, menuY, menuW, MotherCanvas.h, 0, 0, menuItems.size() * menuW - MotherCanvas.h, isLim0: true);
		resetBegin();
		isShowMenu = true;
		GameCanvas.ShowMenu(gI());
		backCMD = cmdClose;
	}

	public string getTextCmd(int index)
	{
		if (index >= 0 && index < T.mQuickMenu.Length)
		{
			return T.mQuickMenu[index];
		}
		return T.select;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			isClose = true;
			break;
		case 0:
		{
			mVector mVector3 = new mVector();
			mVector3.addElement(GameCanvas.gameScr.cmdFriendList);
			mVector3.addElement(GameCanvas.gameScr.cmdBlackList);
			GameCanvas.menu.startAt(mVector3, 2, T.danhsach);
			break;
		}
		case 1:
			GameCanvas.gameScr.cmdBlackList.perform();
			break;
		case 2:
			GameCanvas.gameScr.cmdAuto.perform();
			break;
		case 3:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(GameCanvas.gameScr.cmdSetPk);
			mVector2.addElement(GameCanvas.gameScr.cmdSetDosat);
			GameCanvas.menu.startAt(mVector2, 2, T.chonco);
			break;
		}
		case 4:
			GameCanvas.gameScr.cmdSetDosat.perform();
			break;
		case 5:
			GameCanvas.gameScr.cmdChangeTouch.perform();
			break;
		case 6:
			GameCanvas.gameScr.cmdParty.perform();
			break;
		case 7:
			GameCanvas.gameScr.cmdQuickChat.perform();
			break;
		case 8:
			GameCanvas.gameScr.cmdLogOut.perform();
			break;
		case 9:
			GameCanvas.gameScr.cmdShowWC.perform();
			break;
		case 10:
			GameCanvas.gameScr.cmdClan.perform();
			break;
		case 11:
			if (!GameCanvas.isIos())
			{
				GameCanvas.gameScr.cmdBuyGem.perform();
			}
			break;
		case 12:
			GameCanvas.gameScr.cmdUniform.perform();
			break;
		case 13:
			GameCanvas.gameScr.cmdDauGia.perform();
			break;
		case 14:
			GameCanvas.gameScr.cmdSudo.perform();
			break;
		case 15:
			GameCanvas.gameScr.cmdPet.perform();
			break;
		}
	}

	public override void paintMenu(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = menuX + xEff;
		for (int i = 0; i <= MotherCanvas.h / wUni; i++)
		{
			g.drawImage(imgNenMenu, num, i * wUni, 0);
		}
		if (GameCanvas.isTaiTho)
		{
			for (int j = 0; j <= MotherCanvas.h / wUni; j++)
			{
				g.drawImage(imgNenMenu, num - 60, j * wUni, 0);
			}
		}
		int num2 = 0;
		if (xEff != 0)
		{
			num2 = 1;
		}
		g.drawRegion(imgTamGiac, 0, num2 * 24, 13, 24, 0, num + wUni, MotherCanvas.h / 2 - 12, 0);
		g.setColor(color[num2 * 2]);
		g.fillRect(num + wUni + 1, 0, 1, MotherCanvas.h / 2 - 12);
		g.fillRect(num + wUni + 1, MotherCanvas.h / 2 + 12, 1, MotherCanvas.h / 2 - 12);
		g.setColor(color[num2 * 2 + 1]);
		g.fillRect(num + wUni, 0, 1, MotherCanvas.h / 2 - 11);
		g.fillRect(num + wUni, MotherCanvas.h / 2 + 11, 1, MotherCanvas.h / 2 - 11);
		g.translate(0, -list.cmx);
		for (int k = 0; k < menuItems.size(); k++)
		{
			iCommand iCommand2 = (iCommand)menuItems.elementAt(k);
			sbyte focus = 0;
			if (menuSelectedItem == k)
			{
				focus = 1;
			}
			iCommand2.paintOnlyImage(g, num + wUni / 2, menuY + menuW / 2 + k * menuW - 5, focus);
			mFont.tahoma_7_white.drawString(g, iCommand2.caption, num + wUni / 2, menuY + menuW / 2 + 7 + k * menuW, 2);
		}
	}

	public override void updateMenu()
	{
		if (!isClose)
		{
			if (xEff < 0)
			{
				xEff += speed;
			}
			list.moveCamera();
			updatePointer();
		}
		else
		{
			xEff -= speed;
			if (xEff < -wUni)
			{
				isShowMenu = false;
			}
		}
	}

	public override void updatePointer()
	{
		if (GameCanvas.isPointerSelect)
		{
			if (!GameCanvas.isPoint(menuX, menuY, menuW, MotherCanvas.h))
			{
				isClose = true;
			}
			else
			{
				int num = (list.cmx + GameCanvas.py) / menuW;
				if (num >= 0 && num < menuItems.size())
				{
					iCommand iCommand2 = (iCommand)menuItems.elementAt(num);
					iCommand2.perform();
					isShowMenu = false;
				}
			}
		}
		list.update_Pos_UP_DOWN();
		base.updatePointer();
	}

	public override void updateMenuKey()
	{
		int num = menuSelectedItem;
		if (GameCanvas.keyMove(0) || GameCanvas.keyMove(1))
		{
			menuSelectedItem--;
			GameCanvas.ClearkeyMove(0);
			GameCanvas.ClearkeyMove(1);
		}
		else if (GameCanvas.keyMove(2) || GameCanvas.keyMove(3))
		{
			menuSelectedItem++;
			GameCanvas.ClearkeyMove(2);
			GameCanvas.ClearkeyMove(3);
		}
		menuSelectedItem = AvMain.resetSelect(menuSelectedItem, menuItems.size() - 1, isreset: false);
		if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (menuSelectedItem < menuItems.size() && menuSelectedItem >= 0)
			{
				((iCommand)menuItems.elementAt(menuSelectedItem)).perform();
				isShowMenu = false;
			}
		}
		if (num != menuSelectedItem && GameCanvas.isTouchNoOrPC())
		{
			list.setToX((menuSelectedItem + 1) * menuW - MotherCanvas.h / 2);
		}
		updatekeyPC();
	}
}
