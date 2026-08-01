public class Menu : AvMain
{
	public bool isShowMenu;

	public mVector menuItems = new mVector("Menu.menuItems");

	public int menuSelectedItem;

	public int SelectFocus;

	public int menuX;

	public int menuY;

	public int menuW;

	public int menuH;

	public int menuTemY;

	public int hPlus;

	public int cmtoX;

	public int cmx;

	public int cmdy;

	public int cmvy;

	public int cmxLim;

	public int xc;

	public int pos;

	public int sizeMenu;

	private string nameMenu = string.Empty;

	private string[] mStrTalk;

	public RunWord runText;

	public static bool isPaint = true;

	public static bool isLoadData = true;

	public static sbyte isNPCMenu;

	public sbyte timeShowSelect;

	public iCommand cmdClose;

	public const sbyte NORMAL = 0;

	public const sbyte NPC_MENU = 1;

	public const sbyte QUICK_MENU = 2;

	public static MainObject objSelect;

	public int IdMenu;

	public int IdNpc;

	public static int IdNPCLast;

	private sbyte typeO;

	private sbyte timeShow;

	private int hShow;

	private int maxShow;

	private int archorRunText;

	private int xBegin;

	private int w2cmd;

	public int wUni;

	private int pointerDownTime;

	private int pointerDownFirstX;

	private int[] pointerDownLastX = new int[3];

	private bool pointerIsDowning;

	private bool isDownWhenRunning;

	private int waitToPerform;

	private int cmRun;

	private int valueTime = 2;

	private int cmvx;

	private int cmdx;

	private int miniItem = 10;

	public void beginMenu()
	{
		cmdClose = new iCommand(T.close, 1, this);
		isLoadData = false;
		waitToPerform = 0;
		runText = null;
		right = null;
		SelectFocus = 0;
		menuSelectedItem = 0;
		menuItems.removeAllElements();
		backCMD = cmdClose;
	}

	public void startAt(mVector menuItems, int pos, string name)
	{
		beginMenu();
		isNPCMenu = 0;
		nameMenu = name;
		this.pos = pos;
		if (menuItems == null || menuItems.size() == 0)
		{
			return;
		}
		this.menuItems = menuItems;
		isShowMenu = true;
		if (pos == -1)
		{
			this.menuItems.addElement(cmdClose);
			hPlus = 0;
			menuW = 60;
			menuH = 60;
			for (int i = 0; i < menuItems.size(); i++)
			{
				iCommand iCommand2 = (iCommand)menuItems.elementAt(i);
				int width = mFont.tahoma_7_yellow.getWidth(iCommand2.caption);
				if (width > menuW - 8)
				{
					iCommand2.subCaption = mFont.tahoma_7b_yellow.splitFontArray(iCommand2.caption, menuW - 8);
				}
			}
			wUni = menuItems.size() * menuW - 1;
			if (wUni > MotherCanvas.w - 2)
			{
				wUni = MotherCanvas.w - 2;
			}
			menuX = MotherCanvas.hw - wUni / 2;
			if (menuX < 1)
			{
				menuX = 1;
			}
			menuY = MotherCanvas.h - menuH - (GameCanvas.hCommand + 1);
			if (GameCanvas.isTouch)
			{
				menuY -= 3;
			}
			menuY += 27;
			menuTemY = menuY;
			cmxLim = this.menuItems.size() * menuW - MotherCanvas.w;
			if (cmxLim < 0)
			{
				cmxLim = 0;
			}
			cmtoX = 0;
			cmx = 0;
			xc = 50;
		}
		else
		{
			menuW = GameCanvas.hCommand;
			if (GameCanvas.isTouch)
			{
				menuW = 32;
			}
			sizeMenu = MotherCanvas.h / 4 * 3 / menuW - 1;
			wUni = MotherCanvas.w / 3;
			if (wUni < mFont.tahoma_7b_white.getWidth(name) + 30)
			{
				wUni = mFont.tahoma_7b_white.getWidth(name) + 30;
			}
			hPlus = GameCanvas.hCommand;
			int num = 120;
			int num2 = 30;
			for (int j = 0; j < menuItems.size(); j++)
			{
				iCommand iCommand3 = (iCommand)menuItems.elementAt(j);
				int num3 = mFont.tahoma_7b_white.getWidth(iCommand3.caption) + num2;
				if (num3 > num)
				{
					num = num3;
				}
				if (iCommand3.wimgCaption > 0 && num3 + iCommand3.wimgCaption > num)
				{
					num = num3 + iCommand3.wimgCaption;
				}
			}
			if (wUni < num)
			{
				wUni = num;
			}
			if (wUni > MotherCanvas.w)
			{
				wUni = MotherCanvas.w;
			}
			cmtoX = 0;
			cmx = 0;
			iCommand iCommand4 = null;
			if (GameCanvas.isTouch)
			{
				iCommand4 = cmdClose;
			}
			else
			{
				this.menuItems.addElement(cmdClose);
			}
			if (menuItems.size() > sizeMenu)
			{
				menuH = sizeMenu * menuW + 8;
				cmxLim = (menuItems.size() - sizeMenu) * menuW;
			}
			else
			{
				menuH = menuItems.size() * menuW + 8;
				cmxLim = 0;
			}
			setPos();
			menuTemY = menuY;
			if (iCommand4 != null)
			{
				iCommand4.setPos(menuX + wUni - 11, menuY - hPlus + GameCanvas.hCommand / 2 + 1 + 3, MainTab.fraCloseTab, string.Empty);
				right = iCommand4;
			}
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			menuSelectedItem = 0;
		}
		else
		{
			menuSelectedItem = -1;
		}
		isLoadData = true;
		resetBegin();
		GameCanvas.ShowMenu(GameCanvas.menu);
	}

	public void updateMenuGame(mVector vec)
	{
		menuItems = vec;
	}

	public void setinfoDynamic(mVector menulist, int pos, int idmenu, int idnpc, string name)
	{
		beginMenu();
		if (menulist == null)
		{
			return;
		}
		nameMenu = name;
		isNPCMenu = 0;
		IdMenu = idmenu;
		IdNpc = idnpc;
		this.pos = pos;
		isShowMenu = true;
		menuItems = new mVector();
		menuW = GameCanvas.hCommand;
		if (GameCanvas.isTouch)
		{
			menuW = 32;
		}
		sizeMenu = MotherCanvas.h / 4 * 3 / menuW - 1;
		wUni = MotherCanvas.w / 3;
		hPlus = GameCanvas.hCommand;
		int num = 120;
		if (num < mFont.tahoma_7b_white.getWidth(name) + 45)
		{
			num = mFont.tahoma_7b_white.getWidth(name) + 45;
		}
		for (int i = 0; i < menulist.size(); i++)
		{
			iCommand iCommand2 = (iCommand)menulist.elementAt(i);
			iCommand2.indexMenu = 2;
			iCommand2.Pointer = this;
			int num2 = mFont.tahoma_7b_white.getWidth(iCommand2.caption) + 30;
			if (iCommand2.fraImgCaption != null)
			{
				num2 += iCommand2.fraImgCaption.frameWidth;
			}
			if (iCommand2.item != null)
			{
				num2 += 20;
			}
			if (num2 > num)
			{
				num = num2;
			}
		}
		menuItems = menulist;
		iCommand iCommand3 = null;
		if (GameCanvas.isTouch)
		{
			iCommand3 = cmdClose;
		}
		else
		{
			menuItems.addElement(cmdClose);
		}
		wUni = num;
		if (wUni > MotherCanvas.w)
		{
			wUni = MotherCanvas.w;
		}
		if (menuItems.size() > sizeMenu)
		{
			menuH = sizeMenu * menuW + 8;
			cmxLim = (menuItems.size() - sizeMenu) * menuW;
		}
		else
		{
			menuH = menuItems.size() * menuW + 8;
			cmxLim = 0;
		}
		cmtoX = 0;
		cmx = 0;
		setPos();
		menuTemY = menuY;
		if (iCommand3 != null)
		{
			iCommand3.setPos(menuX + wUni - 8, menuY - hPlus + GameCanvas.hCommand / 2 + 1 + 3, MainTab.fraCloseTab, string.Empty);
			right = iCommand3;
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			menuSelectedItem = 0;
		}
		else
		{
			menuSelectedItem = -1;
		}
		isLoadData = true;
		resetBegin();
		GameCanvas.ShowMenu(GameCanvas.menu);
	}

	public void startAt_NPC(mVector menuItems, string name, int idNPC, sbyte typeO, bool isHelp, int archor, bool isDynamic)
	{
		beginMenu();
		isNPCMenu = 1;
		nameMenu = name;
		IdNpc = idNPC;
		IdNPCLast = idNPC;
		this.typeO = typeO;
		archorRunText = archor;
		if (menuItems == null || menuItems.size() == 0)
		{
			this.menuItems = new mVector();
		}
		else
		{
			this.menuItems = menuItems;
		}
		for (int i = 0; i < this.menuItems.size(); i++)
		{
			iCommand iCommand2 = (iCommand)this.menuItems.elementAt(i);
			if (!isHelp)
			{
				iCommand2.setTypeSpec();
			}
			if (isDynamic)
			{
				iCommand2.indexMenu = 3;
				iCommand2.subIndex = i;
				iCommand2.Pointer = this;
			}
			if (i == 0 && !GameCanvas.isTouch)
			{
				iCommand2.isSelect = true;
			}
		}
		isShowMenu = true;
		menuW = GameCanvas.hCommand;
		if (GameCanvas.isTouch)
		{
			menuW = 32;
		}
		sizeMenu = 0;
		wUni = MotherCanvas.w - 10;
		if (wUni > 300)
		{
			wUni = 300;
		}
		mStrTalk = mFont.tahoma_7_black.splitFontArray(name, wUni - 20);
		hPlus = GameCanvas.hCommand;
		cmtoX = 0;
		cmx = 0;
		int num = mStrTalk.Length;
		iCommand iCommand3 = cmdClose;
		if (!isHelp)
		{
			iCommand3.setTypeSpec();
		}
		this.menuItems.addElement(iCommand3);
		menuH = (num + 2) * GameCanvas.hText + iCommand.hButtonCmdSpec + 5;
		cmxLim = 0;
		menuX = MotherCanvas.hw - wUni / 2;
		menuY = MotherCanvas.h - menuH - 10;
		menuTemY = menuY;
		runText = new RunWord();
		runText.startDialogChain(name, 0, menuX + 10, menuY + 10 + GameCanvas.hText, wUni - 20, mFont.tahoma_7_white);
		setPosNPC(iCommand.hButtonCmdSpec);
		if (GameCanvas.isTouchNoOrPC())
		{
			menuSelectedItem = 0;
		}
		else
		{
			menuSelectedItem = -1;
		}
		isLoadData = true;
		resetBegin();
		GameCanvas.ShowMenu(GameCanvas.menu);
	}

	public void startAt_NPC_Quest(mVector menuItems, string name, int idNPC, sbyte typeO, bool isQuest, int archor)
	{
		beginMenu();
		isNPCMenu = 1;
		nameMenu = name;
		IdNpc = idNPC;
		IdNPCLast = idNPC;
		this.typeO = typeO;
		archorRunText = archor;
		if (menuItems == null || menuItems.size() == 0)
		{
			this.menuItems = new mVector();
		}
		else
		{
			this.menuItems = menuItems;
		}
		isShowMenu = true;
		menuW = GameCanvas.hCommand;
		if (GameCanvas.isTouch)
		{
			menuW = 32;
		}
		sizeMenu = 0;
		wUni = MotherCanvas.w - 10;
		if (wUni > 300)
		{
			wUni = 300;
		}
		mStrTalk = mFont.tahoma_7_black.splitFontArray(name, wUni - 20);
		hPlus = GameCanvas.hCommand;
		cmtoX = 0;
		cmx = 0;
		int num = mStrTalk.Length;
		if (!isQuest)
		{
			iCommand iCommand2 = cmdClose;
			iCommand2.setTypeSpec();
			this.menuItems.addElement(iCommand2);
		}
		else if (num < 3)
		{
			num = 3;
		}
		menuH = (num + 2) * GameCanvas.hText + iCommand.hButtonCmdNor / 2;
		cmxLim = 0;
		menuX = MotherCanvas.hw - wUni / 2;
		menuY = MotherCanvas.h - menuH - 10;
		menuTemY = menuY;
		runText = new RunWord();
		runText.startDialogChain(name, 0, menuX + 10, menuY + 10 + GameCanvas.hText, wUni - 20, mFont.tahoma_7_white);
		for (int i = 0; i < menuItems.size(); i++)
		{
			iCommand iCommand3 = (iCommand)menuItems.elementAt(i);
			iCommand3.setPos(menuX + wUni - iCommand.wButtonCmd / 2, menuY + menuH - iCommand.hButtonCmdNor / 2, null, iCommand3.caption);
			iCommand3.setTypeNone(menuX, menuY, wUni, menuH);
			iCommand3.setIsEffTest(isEff: true);
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			menuSelectedItem = 0;
		}
		else
		{
			menuSelectedItem = -1;
		}
		isLoadData = true;
		resetBegin();
		GameCanvas.ShowMenu(GameCanvas.menu);
	}

	public void startTabMenu(mVector menuItems)
	{
		beginMenu();
		isNPCMenu = 0;
		menuSelectedItem = 0;
		this.menuItems = menuItems;
		isShowMenu = true;
		hPlus = GameCanvas.hCommand;
		sizeMenu = menuH / menuW;
		cmtoX = 0;
		cmx = 0;
		if (sizeMenu - 1 < menuItems.size())
		{
			cmxLim = menuItems.size() * menuW - menuH;
		}
		else
		{
			cmxLim = 0;
		}
		if (cmxLim < 0)
		{
			cmxLim = 0;
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			menuSelectedItem = 0;
		}
		else
		{
			menuSelectedItem = -1;
		}
		isLoadData = true;
		resetBegin();
	}

	public void resetBegin()
	{
		for (int i = 0; i < pointerDownLastX.Length; i++)
		{
			pointerDownLastX[i] = 0;
		}
		pointerDownTime = 0;
		pointerDownFirstX = 0;
		pointerIsDowning = false;
		isDownWhenRunning = false;
		waitToPerform = 0;
		cmRun = 0;
		timeShow = 0;
		if (GameScreen.player != null)
		{
			GameScreen.player.resetAction();
		}
	}

	public void setPosNPC(int hBut)
	{
		int num = menuItems.size();
		xBegin = num * ((iCommand.wButtonCmd + 6) / 2) - 3;
		for (int i = 0; i < num; i++)
		{
			iCommand iCommand2 = (iCommand)menuItems.elementAt(i);
			iCommand2.setPos(menuX + iCommand.wButtonCmd / 2 + wUni / 2 - xBegin + i * (iCommand.wButtonCmd + 6), menuY + menuH - hBut / 2 - 5, null, iCommand2.caption);
			if (GameCanvas.isTouchNoOrPC() && i == 0)
			{
				iCommand2.isSelect = true;
			}
		}
	}

	public void setPos()
	{
		switch (pos)
		{
		case 0:
			menuX = 2;
			menuY = MotherCanvas.h - GameCanvas.hCommand - menuH - 2;
			if (GameCanvas.isTouch)
			{
				menuY += GameCanvas.hCommand;
			}
			break;
		case 1:
			menuX = MotherCanvas.w - wUni - 2;
			menuY = MotherCanvas.h - GameCanvas.hCommand - menuH - 2;
			if (GameCanvas.isTouch)
			{
				menuY += GameCanvas.hCommand;
			}
			break;
		case 2:
		case 4:
			menuX = MotherCanvas.hw - wUni / 2;
			menuY = MotherCanvas.h / 2 - menuH / 2 - 2 + menuW / 2 + 6 - GameCanvas.hCommand / 2;
			break;
		case 3:
			menuX = 2;
			menuY = 2;
			break;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
		{
			iCommand iCommand2 = (iCommand)menuItems.elementAt(menuSelectedItem);
			if (!iCommand2.isDonotCloseMenu)
			{
				isShowMenu = false;
			}
			perform(iCommand2);
			break;
		}
		case 1:
			doCloseMenu();
			break;
		case 2:
			GlobalService.gI().Dynamic_Menu((short)IdNpc, (sbyte)IdMenu, (sbyte)menuSelectedItem);
			isShowMenu = false;
			GameCanvas.isPointerSelect = false;
			break;
		case 3:
			GlobalService.gI().Dynamic_Menu((short)IdNpc, (sbyte)IdMenu, (sbyte)subIndex);
			isShowMenu = false;
			GameCanvas.isPointerSelect = false;
			break;
		}
		base.commandPointer(index, subIndex);
	}

	public virtual void updateMenuKey()
	{
		if (!isPaint || !isShowMenu)
		{
			return;
		}
		if (timeShowSelect > 0)
		{
			timeShowSelect--;
		}
		bool flag = false;
		if (isNPCMenu == 1)
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
			if (num != menuSelectedItem && GameCanvas.isTouchNoOrPC())
			{
				for (int i = 0; i < menuItems.size(); i++)
				{
					iCommand iCommand2 = (iCommand)menuItems.elementAt(i);
					if (i == menuSelectedItem)
					{
						iCommand2.isSelect = true;
					}
					else
					{
						iCommand2.isSelect = false;
					}
				}
			}
			if (GameCanvas.keyMyHold[5])
			{
				GameCanvas.clearKeyHold(5);
				if (menuSelectedItem < menuItems.size() && menuSelectedItem >= 0)
				{
					((iCommand)menuItems.elementAt(menuSelectedItem)).perform();
				}
			}
		}
		else if (isNPCMenu == 0)
		{
			if (pos == -1)
			{
				if (GameCanvas.keyMove(0))
				{
					flag = true;
					menuSelectedItem--;
					if (menuSelectedItem < 0)
					{
						menuSelectedItem = menuItems.size() - 1;
					}
					GameCanvas.ClearkeyMove(0);
				}
				else if (GameCanvas.keyMove(2))
				{
					flag = true;
					menuSelectedItem++;
					if (menuSelectedItem > menuItems.size() - 1)
					{
						menuSelectedItem = 0;
					}
					GameCanvas.ClearkeyMove(2);
				}
			}
			else if (GameCanvas.keyMove(1))
			{
				flag = true;
				menuSelectedItem--;
				if (menuSelectedItem < 0)
				{
					menuSelectedItem = menuItems.size() - 1;
				}
				GameCanvas.ClearkeyMove(1);
			}
			else if (GameCanvas.keyMove(3))
			{
				flag = true;
				menuSelectedItem++;
				if (menuSelectedItem > menuItems.size() - 1)
				{
					menuSelectedItem = 0;
				}
				GameCanvas.ClearkeyMove(3);
			}
		}
		if (flag)
		{
			if (pos == -1)
			{
				cmtoX = menuSelectedItem * menuW + menuW - MotherCanvas.w / 2;
			}
			else
			{
				cmtoX = (menuSelectedItem + 1) * menuW - menuH / 2;
			}
			if (cmtoX > cmxLim)
			{
				cmtoX = cmxLim;
			}
			if (cmtoX < 0)
			{
				cmtoX = 0;
			}
			if (menuSelectedItem == menuItems.size() - 1 || menuSelectedItem == 0)
			{
				cmx = cmtoX;
			}
		}
		if (isNPCMenu == 0)
		{
			if (pos == -1)
			{
				updatePos_LEFT_RIGHT();
			}
			else
			{
				update_Pos_UP_DOWN();
			}
			if (GameCanvas.isPointerSelect && GameCanvas.menuCur == this && !GameCanvas.isPoint(menuX - 5, menuTemY - 5 - hPlus, wUni + 10, menuH + 10 + hPlus))
			{
				doCloseMenu();
			}
		}
		else if (isNPCMenu == 2)
		{
			updatePos_LEFT_RIGHT();
			if (GameCanvas.isPointerSelect && !GameCanvas.isPoint(menuX - 5, menuY - 5, wUni + 10, menuH + 10))
			{
				timeShow = -1;
			}
		}
		base.updatekey();
		updatekeyPC();
	}

	public void updatePos_LEFT_RIGHT()
	{
		if (GameCanvas.isPointerDown)
		{
			if (!pointerIsDowning && GameCanvas.isPointer(menuX, menuY, wUni, menuH))
			{
				for (int i = 0; i < pointerDownLastX.Length; i++)
				{
					pointerDownLastX[0] = GameCanvas.px;
				}
				pointerDownFirstX = GameCanvas.px;
				pointerIsDowning = true;
				isDownWhenRunning = cmRun != 0;
				cmRun = 0;
			}
			else if (pointerIsDowning)
			{
				pointerDownTime++;
				if (pointerDownTime > 5 && pointerDownFirstX == GameCanvas.px && !isDownWhenRunning)
				{
					pointerDownFirstX = -1000;
					menuSelectedItem = (cmtoX + GameCanvas.px - menuX) / menuW;
				}
				int num = GameCanvas.px - pointerDownLastX[0];
				if (num != 0 && menuSelectedItem != -1)
				{
					menuSelectedItem = -1;
				}
				for (int num2 = pointerDownLastX.Length - 1; num2 > 0; num2--)
				{
					pointerDownLastX[num2] = pointerDownLastX[num2 - 1];
				}
				pointerDownLastX[0] = GameCanvas.px;
				cmtoX -= num;
				if (cmtoX < 0)
				{
					cmtoX = 0;
				}
				if (cmtoX > cmxLim)
				{
					cmtoX = cmxLim;
				}
				if (cmx < 0 || cmx > cmxLim)
				{
					num /= 2;
				}
				cmx -= num;
			}
		}
		if (GameCanvas.isPointerClick && pointerIsDowning)
		{
			int a = GameCanvas.px - pointerDownLastX[0];
			GameCanvas.isPointerClick = false;
			if (CRes.abs(a) < 20 && CRes.abs(GameCanvas.px - pointerDownFirstX) < 20 && !isDownWhenRunning)
			{
				cmRun = 0;
				cmtoX = cmx;
				pointerDownFirstX = -1000;
				menuSelectedItem = (cmtoX + GameCanvas.px - menuX) / menuW;
				pointerDownTime = 0;
				waitToPerform = 1;
			}
			else if (menuSelectedItem != -1 && pointerDownTime > 5)
			{
				pointerDownTime = 0;
				waitToPerform = 1;
			}
			else if (menuSelectedItem == -1 && !isDownWhenRunning)
			{
				if (cmx < 0)
				{
					cmtoX = 0;
				}
				else if (cmx > cmxLim)
				{
					cmtoX = cmxLim;
				}
				else
				{
					int num3 = GameCanvas.px - pointerDownLastX[0] + (pointerDownLastX[0] - pointerDownLastX[1]) + (pointerDownLastX[1] - pointerDownLastX[2]);
					num3 = ((num3 > 10) ? 10 : ((num3 < -10) ? (-10) : 0));
					cmRun = -num3 * 100;
				}
			}
			pointerIsDowning = false;
			pointerDownTime = 0;
			GameCanvas.isPointerClick = false;
		}
		if (GameCanvas.isPointerRelease && pointerIsDowning)
		{
			pointerIsDowning = false;
		}
	}

	public void update_Pos_UP_DOWN()
	{
		if (GameCanvas.keyMyPressed[5])
		{
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			iCommand iCommand2 = (iCommand)menuItems.elementAt(menuSelectedItem);
			if (!iCommand2.isDonotCloseMenu)
			{
				doCloseMenu();
			}
			perform(iCommand2);
		}
		else if (GameCanvas.keyMyPressed[12])
		{
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			iCommand iCommand3 = (iCommand)menuItems.elementAt(menuSelectedItem);
			if (!iCommand3.isDonotCloseMenu)
			{
				doCloseMenu();
			}
			perform(iCommand3);
		}
		if (GameCanvas.isPointerDown)
		{
			if (!pointerIsDowning && GameCanvas.isPointer(menuX, menuY, wUni, menuH))
			{
				for (int i = 0; i < pointerDownLastX.Length; i++)
				{
					pointerDownLastX[0] = GameCanvas.py;
				}
				pointerDownFirstX = GameCanvas.py;
				pointerIsDowning = true;
				isDownWhenRunning = cmRun != 0;
				cmRun = 0;
			}
			else if (pointerIsDowning)
			{
				pointerDownTime++;
				if (pointerDownTime > valueTime && pointerDownFirstX == GameCanvas.py && !isDownWhenRunning)
				{
					pointerDownFirstX = -1000;
					menuSelectedItem = (cmtoX + GameCanvas.py - menuY) / menuW;
				}
				int num = GameCanvas.py - pointerDownLastX[0];
				if (num != 0 && menuSelectedItem != -1)
				{
					menuSelectedItem = -1;
				}
				for (int num2 = pointerDownLastX.Length - 1; num2 > 0; num2--)
				{
					pointerDownLastX[num2] = pointerDownLastX[num2 - 1];
				}
				pointerDownLastX[0] = GameCanvas.py;
				cmtoX -= num;
				if (cmtoX < 0)
				{
					cmtoX = 0;
				}
				if (cmtoX > cmxLim)
				{
					cmtoX = cmxLim;
				}
				if (cmx < 0 || cmx > cmxLim)
				{
					num /= 2;
				}
				cmx -= num;
			}
		}
		if (GameCanvas.isPointerClick && pointerIsDowning)
		{
			int a = GameCanvas.py - pointerDownLastX[0];
			GameCanvas.isPointerClick = false;
			if (CRes.abs(a) < 20 && CRes.abs(GameCanvas.py - pointerDownFirstX) < 20 && !isDownWhenRunning && GameCanvas.isPointerSelect)
			{
				cmRun = 0;
				cmtoX = cmx;
				pointerDownFirstX = -1000;
				menuSelectedItem = (cmtoX + GameCanvas.py - menuY) / menuW;
				pointerDownTime = 0;
				waitToPerform = 1;
			}
			else if (menuSelectedItem != -1 && pointerDownTime > valueTime)
			{
				pointerDownTime = 0;
				waitToPerform = 1;
			}
			else if (menuSelectedItem == -1 && !isDownWhenRunning)
			{
				timeShowSelect = 0;
				if (cmx < 0)
				{
					cmtoX = 0;
				}
				else if (cmx > cmxLim)
				{
					cmtoX = cmxLim;
				}
				else
				{
					int num3 = GameCanvas.py - pointerDownLastX[0] + (pointerDownLastX[0] - pointerDownLastX[1]) + (pointerDownLastX[1] - pointerDownLastX[2]);
					num3 = ((num3 > 10) ? 10 : ((num3 < -10) ? (-10) : 0));
					cmRun = -num3 * 100;
				}
			}
			pointerIsDowning = false;
			pointerDownTime = 0;
		}
		if (menuSelectedItem != -1 && GameCanvas.isPointerDown && !GameCanvas.isPointerMove)
		{
			timeShowSelect = 5;
		}
		if (GameCanvas.isPointerRelease && pointerIsDowning)
		{
			pointerIsDowning = false;
		}
	}

	public void moveCamera()
	{
		if (cmRun != 0 && !pointerIsDowning)
		{
			cmtoX += cmRun / 100;
			if (cmtoX < 0)
			{
				cmtoX = 0;
			}
			else if (cmtoX > cmxLim)
			{
				cmtoX = cmxLim;
			}
			else
			{
				cmx = cmtoX;
			}
			cmRun = cmRun * 9 / 10;
			if (cmRun < 100 && cmRun > -100)
			{
				cmRun = 0;
			}
		}
		if (cmx != cmtoX && !pointerIsDowning)
		{
			cmvx = cmtoX - cmx << 2;
			cmdx += cmvx;
			cmx += cmdx >> 4;
			cmdx &= 15;
		}
	}

	public virtual void paintMenu(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		if (!isLoadData)
		{
			return;
		}
		if (isNPCMenu == 1)
		{
			paint_NPC_MENU(g);
		}
		else
		{
			if (isNPCMenu != 0)
			{
				return;
			}
			paintPaper_UpDown(g, menuX - miniItem, menuTemY - GameCanvas.hCommand, wUni + miniItem * 2, menuH + GameCanvas.hCommand + miniItem, menuH + GameCanvas.hCommand + miniItem);
			g.setColor(15972174);
			g.fillRoundRect(menuX + miniItem, menuTemY - GameCanvas.hCommand + miniItem / 2 + 3, wUni - miniItem * 2, 16, 4, 4);
			AvMain.FontBorderColor(g, nameMenu, menuX + wUni / 2, menuTemY - hPlus + GameCanvas.hCommand / 4 + 1 + 3, 2, 6, 5);
			if (!isPaint || pos == -1)
			{
				return;
			}
			g.setClip(menuX + 3, menuY + 3, wUni - 6, menuH - 6);
			g.saveCanvas();
			g.ClipRec(menuX + 3, menuY + 3, wUni - 6, menuH - 6);
			g.translate(0, -cmx);
			g.setColor(AvMain.colorMenu[4]);
			if (pos == 2 || pos == 4 || pos == 0)
			{
				for (int i = 0; i < menuItems.size() - 1; i++)
				{
					paintLine(g, i);
				}
			}
			int num = cmx / menuW - 1;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = num + sizeMenu + 2;
			if (num2 > menuItems.size())
			{
				num2 = menuItems.size();
				num = num2 - sizeMenu - 2;
				if (num < 0)
				{
					num = 0;
				}
			}
			if (menuSelectedItem > -1)
			{
				paintSelect(g, menuX + 10, menuY + 7 + menuSelectedItem * menuW, wUni - 20, menuW - 8);
			}
			for (int j = num; j < num2; j++)
			{
				iCommand iCommand2 = (iCommand)menuItems.elementAt(j);
				bool is3D = false;
				if (menuSelectedItem == j)
				{
					is3D = true;
				}
				if (pos == 2)
				{
					iCommand2.paintCaptionImageMenu(g, menuX + wUni / 2, menuY + 6 + menuW / 4 + j * menuW, 2, is3D);
				}
				else if (pos == 0 || pos == 3)
				{
					iCommand2.paintCaptionImageMenu(g, menuX + 12, menuY + 6 + menuW / 4 + j * menuW, 0, is3D);
				}
				else if (pos == 1)
				{
					iCommand2.paintCaptionImageMenu(g, menuX + wUni - 6, menuY + 6 + menuW / 4 + j * menuW, 1, is3D);
				}
				else if (pos == 4)
				{
					iCommand2.paintCaptionImageMenu(g, menuX + 12, menuY + 6 + menuW / 4 + j * menuW, 0, is3D);
				}
			}
			mGraphics.resetTransAndroid(g);
			g.restoreCanvas();
			GameCanvas.resetTrans(g);
			paintCmd(g);
		}
	}

	public void paintLine(mGraphics g, int i)
	{
		g.setColor(AvMain.colorMenu[4]);
		g.fillRect(menuX + 8, menuY + 3 + menuW + i * menuW - 1, wUni - 16, 2);
		g.fillRect(menuX + 8 + 1, menuY + 3 + menuW + i * menuW - 2, wUni - 16 - 2, 4);
	}

	private void paint_NPC_MENU(mGraphics g)
	{
		int num = menuX + 6;
		int y = menuY + 8;
		AvMain.paintRect(g, menuX, menuTemY, wUni, menuH, 2, 4);
		MainObject mainObject = MainObject.get_Object(IdNpc, typeO);
		if (mainObject != null)
		{
			AvMain.Font3dWhite(g, mainObject.name, num + 10, y, 0);
			if (runText != null)
			{
				runText.paintText(g, archorRunText);
			}
			GameCanvas.resetTrans(g);
			for (int i = 0; i < menuItems.size(); i++)
			{
				iCommand iCommand2 = (iCommand)menuItems.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	public void doCloseMenu()
	{
		if (this == GameCanvas.menuCur)
		{
			isShowMenu = false;
		}
		GameCanvas.isPointerSelect = false;
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerEnd = true;
	}

	public virtual void updateMenu()
	{
		if (timeShow > 0)
		{
			timeShow++;
			if (hShow < wUni)
			{
				hShow += menuH;
				if (hShow >= wUni)
				{
					hShow = wUni;
					timeShow = 0;
				}
			}
		}
		else if (timeShow < 0)
		{
			timeShow--;
			if (hShow > 0)
			{
				hShow -= menuH;
				if (hShow <= 0)
				{
					hShow = 0;
					timeShow = 0;
					doCloseMenu();
				}
			}
		}
		if (!isLoadData)
		{
			return;
		}
		moveCamera();
		if (isNPCMenu == 1)
		{
			if (runText != null)
			{
				runText.updateDlg();
			}
			for (int i = 0; i < menuItems.size(); i++)
			{
				iCommand iCommand2 = (iCommand)menuItems.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		else if (isNPCMenu == 2 && !GameCanvas.isPointerMove && timeShow == 0)
		{
			for (int j = 0; j < menuItems.size(); j++)
			{
				iCommand iCommand3 = (iCommand)menuItems.elementAt(j);
				iCommand3.updatePointerShow(cmx, 0);
			}
		}
		if (menuTemY > menuY)
		{
			int num = menuTemY - menuY >> 1;
			if (num < 1)
			{
				num = 1;
			}
			menuTemY -= num;
		}
		if (xc != 0)
		{
			xc >>= 1;
			if (xc < 0)
			{
				xc = 0;
			}
		}
		if (waitToPerform > 0)
		{
			waitToPerform--;
			if (waitToPerform == 0)
			{
				if (menuSelectedItem >= 0 && menuSelectedItem < menuItems.size())
				{
					iCommand iCommand4 = (iCommand)menuItems.elementAt(menuSelectedItem);
					if (!iCommand4.isDonotCloseMenu)
					{
						if (this == GameCanvas.menuCur)
						{
							isShowMenu = false;
						}
						else
						{
							doCloseMenu();
						}
					}
					perform(iCommand4);
					timeShowSelect = 5;
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					GameCanvas.isPointerEnd = true;
					GameCanvas.isPointerSelect = false;
				}
				else if (this == GameCanvas.menuCur)
				{
					isShowMenu = false;
				}
				else
				{
					doCloseMenu();
				}
			}
		}
		base.updatePointer();
	}

	public void perform(iCommand cmd)
	{
		if (cmd != null)
		{
			if (cmd.action != null)
			{
				cmd.action.perform();
			}
			else if (cmd.Pointer != null)
			{
				cmd.Pointer.commandPointer(cmd.indexMenu, cmd.subIndex);
			}
			else
			{
				GameCanvas.currentScreen.commandMenu(cmd.indexMenu, cmd.subIndex);
			}
			GameCanvas.isPointerSelect = false;
			mSound.playSound(0, mSound.volumeSound);
		}
	}
}
