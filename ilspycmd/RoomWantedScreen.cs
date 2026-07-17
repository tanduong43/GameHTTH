public class RoomWantedScreen : MainScreen
{
	public static RoomWantedScreen instance;

	public string strShow = string.Empty;

	public mVector vecCmd = new mVector();

	private InfoMemList infoPlayer;

	private int x;

	private int y;

	private int w;

	private int h;

	private int wpos = 56;

	private int idSelect = -1;

	private int type;

	private iCommand cmdClose;

	private iCommand cmdFind;

	private iCommand cmdCancle;

	private iCommand cmdActionChest;

	private int[][] posChest = new int[2][]
	{
		new int[2] { 190, 60 },
		new int[2] { 190, 125 }
	};

	public RoomWantedScreen()
	{
		w = 240;
		h = 215;
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2;
		if (!GameCanvas.isTouch)
		{
			y -= 18;
		}
		infoPlayer = new InfoMemList(GameScreen.player.ID);
		infoPlayer.name = GameScreen.player.name;
		infoPlayer.charShow = GameScreen.player;
		cmdClose = new iCommand(T.close, -1, this);
		cmdFind = new iCommand(T.find, 1, this);
		cmdCancle = new iCommand(T.findCancle, 2, this);
		cmdActionChest = new iCommand(T.open, 3, this);
		if (GameCanvas.isTouch)
		{
			cmdFind.setPos(x + 190, y + 169, null, cmdFind.caption);
			cmdCancle.setPos(x + 190, y + 169, null, cmdCancle.caption);
			cmdClose.setPos(x + w - 13, y + 13, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			AvMain.setPosCMD(cmdFind, 0);
			AvMain.setPosCMD(cmdCancle, 0);
			AvMain.setPosCMD(cmdClose, 2);
			AvMain.setPosCMD(cmdActionChest, 1);
			idSelect = 0;
		}
	}

	public void setTimeCmdFind(int time)
	{
		if (time > 0 && cmdFind != null)
		{
			cmdFind.setTime(time);
		}
	}

	public void setAction(sbyte ac)
	{
		vecCmd.removeAllElements();
		type = ac;
		if (ac == 0)
		{
			instance.Show(GameCanvas.gameScr);
			instance.strShow = T.wantedClick;
			vecCmd.addElement(cmdFind);
			vecCmd.addElement(cmdClose);
			okCMD = cmdFind;
			backCMD = cmdClose;
			center = cmdFind;
			right = cmdClose;
			cmdFind.timeSelect = 0;
			cmdFind.isSelect = false;
		}
		else if (ac == 1)
		{
			instance.strShow = T.wantedWait;
			vecCmd.addElement(cmdCancle);
			vecCmd.addElement(cmdClose);
			okCMD = cmdCancle;
			backCMD = cmdClose;
			center = cmdCancle;
			right = cmdClose;
		}
		else if (ac == 2)
		{
			instance.strShow = T.wantedReady;
			okCMD = null;
			backCMD = null;
			center = null;
			right = null;
		}
		else if (ac == 3)
		{
			instance.strShow = T.wantedClick;
			vecCmd.addElement(cmdFind);
			vecCmd.addElement(cmdClose);
			okCMD = cmdFind;
			backCMD = cmdClose;
			center = cmdFind;
			right = cmdClose;
			cmdFind.timeSelect = 0;
			cmdFind.isSelect = false;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			GlobalService.gI().Room_Wanted(-1);
			GameCanvas.Start_Waiting_Connect_DiaLog(T.pleaseWaiting, isCmdClose: true);
			break;
		case 1:
			GlobalService.gI().Room_Wanted(1);
			break;
		case 2:
			GlobalService.gI().Room_Wanted(3);
			break;
		case 3:
			if (Player.mChestWanted[idSelect] != null)
			{
				GlobalService.gI().Chest_Wanted(0, Player.mChestWanted[idSelect].ID);
			}
			break;
		}
		base.commandPointer(index, subIndex);
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		if (!GameCanvas.isSmallScreen)
		{
			AvMain.paintBG_Wanted_Room(g, x, y, w, h);
		}
		if (GameCanvas.lowGraphic)
		{
			AvMain.paintBG_Wanted(g, x + 22, y + 30, AvMain.wWanted + 6, AvMain.hWanted, 3);
		}
		else
		{
			AvMain.paintBG_Wanted(g, x + 22, y + 30, AvMain.wWanted, AvMain.hWanted, 3);
		}
		WantedScreen.paintInfo(g, infoPlayer, x + 22, y + 30, AvMain.wWanted, AvMain.hWanted);
		AvMain.paintRect(g, x + 22, y + h - 22 - 5, w - 44, 20, 1, 1);
		string text = strShow;
		if (type == 1)
		{
			for (int i = 0; i < GameCanvas.gameTick / 6 % 3; i++)
			{
				text += ".";
			}
		}
		mFont.tahoma_7b_white.drawString(g, text, x + w / 2, y + h - 12 - 6 - 5, 2);
		for (int j = 0; j < Player.mChestWanted.Length; j++)
		{
			if (GameCanvas.lowGraphic)
			{
				g.setColor(8336436);
				g.drawRect(x + posChest[j][0] + 3 - 30, y + posChest[j][1] + 3 - 30, 54, 54);
				if (j == idSelect)
				{
					g.setColor(16770418);
					g.drawRect(x + posChest[j][0] + 2 - 30, y + posChest[j][1] + 2 - 30, 56, 56);
				}
				if (Player.mChestWanted[j] != null)
				{
					Player.mChestWanted[j].paintRuong(g, x + posChest[j][0] + 1, y + posChest[j][1], wpos);
				}
			}
			else
			{
				g.drawImage(AvMain.mImgRoomW[0], x + posChest[j][0], y + posChest[j][1], 3);
				if (Player.mChestWanted[j] != null)
				{
					Player.mChestWanted[j].paintRuong(g, x + posChest[j][0] + 1, y + posChest[j][1], wpos);
				}
				else
				{
					g.drawImage(AvMain.mImgRoomW[1], x + posChest[j][0], y + posChest[j][1], 3);
				}
				if (!GameCanvas.isTouch && j == idSelect)
				{
					g.drawRegion(AvMain.mImgRoomW[4], 0, 70 * (GameCanvas.gameTick / 4 % 2), 72, 70, 0, x + posChest[j][0], y + posChest[j][1], 3);
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			for (int k = 0; k < vecCmd.size(); k++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(k);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		else
		{
			base.paint(g);
		}
	}

	public override void update()
	{
		if (cmdFind != null && cmdFind.timeSelect == 0)
		{
			cmdFind.isSelect = false;
		}
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		for (int i = 0; i < Player.mChestWanted.Length; i++)
		{
			if (Player.mChestWanted[i] != null && Player.mChestWanted[i].timeUse > 0 && Player.mChestWanted[i].marketTime.CheckUpdate() == 0)
			{
				Player.mChestWanted[i].timeUse = 0;
			}
		}
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(1))
		{
			if (idSelect > 0)
			{
				idSelect--;
			}
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			if (idSelect < 1)
			{
				idSelect++;
			}
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (flag)
		{
			if (Player.mChestWanted[idSelect] != null)
			{
				left = cmdActionChest;
				menuCMD = cmdActionChest;
			}
			else
			{
				left = null;
				menuCMD = null;
			}
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		for (int i = 0; i < posChest.Length; i++)
		{
			if (GameCanvas.isPointerSelect && GameCanvas.isPoint(x + posChest[i][0] - wpos / 2, y + posChest[i][1] - wpos / 2, wpos, wpos))
			{
				GameCanvas.isPointerSelect = false;
				idSelect = i;
				if (Player.mChestWanted[i] != null)
				{
					cmdActionChest.perform();
				}
			}
		}
		for (int j = 0; j < vecCmd.size(); j++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(j);
			iCommand2.updatePointer();
		}
		base.updatePointer();
	}
}
