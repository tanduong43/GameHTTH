public class ChatTabScreen : MainScreen
{
	public int x;

	public int y;

	public int w = 225;

	public int h = 194;

	public int miniItem = 5;

	public int xBe;

	public int yBe;

	public int wCon;

	public int hCon;

	public int wItem;

	public int hChat;

	public int wPaintTab;

	public mVector vecTabChat = new mVector("ChatTabScreen.vecTabChat");

	public ChatDetail tabCur;

	public int idSelect;

	private ListNew CamDetailChat;

	private ListNew CamTab;

	private iCommand cmdMenu;

	private iCommand cmdCloseChat;

	private iCommand cmdCloseTab;

	private iCommand cmdChat;

	private int minChat;

	private int maxChat;

	public const sbyte UPDATE = 1;

	public const sbyte NEW = 0;

	public ChatTabScreen()
	{
		if (w > MotherCanvas.w)
		{
			w = MotherCanvas.w;
		}
		if (h > MotherCanvas.h - GameCanvas.hCommand - 10)
		{
			h = MotherCanvas.h - GameCanvas.hCommand - 10;
		}
		x = MotherCanvas.hw - w / 2;
		if (GameCanvas.isTouch)
		{
			y = MotherCanvas.hh - h / 2;
		}
		else
		{
			y = MotherCanvas.hh - h / 2 - GameCanvas.hCommand / 2;
		}
		wItem = 24;
		xBe = x + wItem + miniItem;
		yBe = y + wItem + miniItem;
		hCon = h - wItem - miniItem - miniItem * 2;
		wCon = w - wItem * 2 - miniItem * 2;
		hChat = hCon / GameCanvas.hText + 2;
		CamDetailChat = new ListNew();
		CamTab = new ListNew();
		wPaintTab = 60;
		cmdMenu = new iCommand(T.close, 0, this);
		if (GameCanvas.isTouch)
		{
			cmdMenu.setPos(x + w - wItem / 2, y + wItem / 10 + miniItem / 2 + wItem / 5 * 2, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			cmdMenu = AvMain.setPosCMD(cmdMenu, 1);
		}
		left = cmdMenu;
		menuCMD = left;
		cmdCloseChat = new iCommand(T.close + " " + T.TroChuyen, 1, this);
		cmdCloseTab = new iCommand(T.close + " " + T.thistab, 2, this);
		cmdChat = new iCommand(T.chat, 3, this);
	}

	public override void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public override void Show(MainScreen screen)
	{
		checkRemove();
		getCurTab(idSelect);
		base.Show(screen);
	}

	private void checkRemove()
	{
		for (int i = 0; i < vecTabChat.size(); i++)
		{
			ChatDetail chatDetail = (ChatDetail)vecTabChat.elementAt(i);
			if (chatDetail.typeChat != 3 && chatDetail.typeChat != 5 && chatDetail.vecDetail.size() > 40)
			{
				int num = chatDetail.vecDetail.size() - 30;
				for (int j = 0; j < num; j++)
				{
					chatDetail.vecDetail.removeElementAt(0);
				}
			}
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			doMenu();
			break;
		case 1:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			break;
		case 2:
			if (idSelect == 0)
			{
				cmdCloseChat.perform();
				break;
			}
			if (idSelect >= 0 && idSelect < vecTabChat.size())
			{
				vecTabChat.removeElementAt(idSelect);
			}
			idSelect = AvMain.resetSelect(idSelect, vecTabChat.size() - 1, isreset: false);
			getCurTab(idSelect);
			if (vecTabChat.size() == 1 && !GameCanvas.isTouch)
			{
				cmdMenu.caption = T.close;
			}
			break;
		case 3:
			if (tabCur != null && tabCur.tfchat != null)
			{
				tabCur.addStartChat(GameScreen.player.name);
			}
			break;
		}
	}

	public void checkRemoveTab(string name)
	{
		for (int i = 0; i < vecTabChat.size(); i++)
		{
			ChatDetail chatDetail = (ChatDetail)vecTabChat.elementAt(i);
			if (chatDetail.name.CompareTo(name) == 0)
			{
				vecTabChat.removeElementAt(i);
				break;
			}
		}
		idSelect = AvMain.resetSelect(idSelect, vecTabChat.size() - 1, isreset: false);
	}

	private void doMenu()
	{
		cmdCloseChat.perform();
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		MainTab.paintPaperTab(g, x, y, w, h);
		AvMain.paintRect(g, xBe - miniItem, yBe - miniItem, wCon + miniItem * 2, hCon + miniItem * 2, 0, 4);
		g.setClip(x + wItem / 2, y, w - wItem, wItem + miniItem * 2);
		g.saveCanvas();
		g.ClipRec(x + wItem / 2, y, w - wItem, wItem + miniItem * 2);
		g.translate(-CamTab.cmx, 0);
		int num = xBe;
		for (int i = 0; i < idSelect && i < vecTabChat.size(); i++)
		{
			ChatDetail chatDetail = (ChatDetail)vecTabChat.elementAt(i);
			int index = 2;
			if (chatDetail.isNew && (chatDetail.timeNew + GameCanvas.gameTick) % 8 < 4)
			{
				index = 1;
			}
			AvMain.paintRect(g, num, y + wItem / 10 + miniItem / 2, wPaintTab / 2, wItem / 5 * 4, 1, index);
			mFont.tahoma_7_white.drawString(g, chatDetail.shortName, num + 2, y + wItem / 10 + miniItem / 2 + 2, 0);
			num += wPaintTab / 2;
		}
		int num2 = vecTabChat.size() - 1;
		while (num2 > idSelect && num2 < vecTabChat.size() && num2 >= 0)
		{
			ChatDetail chatDetail2 = (ChatDetail)vecTabChat.elementAt(num2);
			int index2 = 2;
			if (chatDetail2.isNew && (chatDetail2.timeNew + GameCanvas.gameTick) % 8 < 4)
			{
				index2 = 1;
			}
			AvMain.paintRect(g, xBe + wPaintTab / 2 + num2 * (wPaintTab / 2), y + wItem / 10 + miniItem / 2, wPaintTab / 2 + 1, wItem / 5 * 4, 1, index2);
			mFont.tahoma_7_white.drawString(g, chatDetail2.shortName, xBe + wPaintTab / 2 + num2 * (wPaintTab / 2) + 4, y + wItem / 10 + miniItem / 2 + 2, 0);
			num2--;
		}
		if (tabCur != null)
		{
			AvMain.paintRect(g, xBe + wPaintTab / 2 * idSelect, y + wItem / 10 + miniItem / 2, wPaintTab, wItem / 5 * 4, 1, 4);
			int num3 = 0;
			if (idSelect == 0 && !GameCanvas.isDeviceStore())
			{
				num3 = 9;
				int num4 = GameCanvas.IndexServer;
				if (GameCanvas.IndexServer >= 3)
				{
					num4 = GameCanvas.IndexServer + 1;
				}
				Interface_Game.fraBorderNoti4.drawFrame((GameCanvas.language != 0) ? 3 : num4, xBe + wPaintTab / 2 * idSelect + 9, y + wItem / 10 + miniItem / 2 + 2 + 5, 0, 3, g);
			}
			mFont.tahoma_7b_white.drawString(g, tabCur.shortNameFocus, xBe + wPaintTab / 2 * idSelect + wPaintTab / 2 + num3, y + wItem / 10 + miniItem / 2 + 2, 2);
			mGraphics.resetTransAndroid(g);
			g.restoreCanvas();
			GameCanvas.resetTrans(g);
			if (tabCur.tfchat != null)
			{
				tabCur.tfchat.paint(g);
			}
			g.setClip(xBe - miniItem, yBe - miniItem, wCon + miniItem * 2, hCon - ((tabCur.tfchat == null) ? (-miniItem) : tabCur.tfchat.height) + 2);
			g.saveCanvas();
			g.ClipRec(xBe - miniItem, yBe - miniItem, wCon + miniItem * 2, hCon - ((tabCur.tfchat == null) ? (-miniItem) : tabCur.tfchat.height) + 2);
			g.translate(0, -CamDetailChat.cmx);
			minChat = CamDetailChat.cmx / GameCanvas.hText - 2;
			if (minChat < 0)
			{
				minChat = 0;
			}
			maxChat = minChat + hChat;
			for (int j = minChat; j <= maxChat; j++)
			{
				if (j < tabCur.vecDetail.size() && j >= 0)
				{
					MainTextChat mainTextChat = (MainTextChat)tabCur.vecDetail.elementAt(j);
					AvMain.setTextColor(mainTextChat.color).drawString(g, mainTextChat.text, xBe, yBe + j * GameCanvas.hText, 0);
				}
			}
			mGraphics.resetTransAndroid(g);
			g.restoreCanvas();
		}
		base.paint(g);
		if (tabCur != null && CamDetailChat.cmxLim > 0)
		{
			tabCur.scrChat.paint(g);
		}
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		CamTab.moveCamera();
		CamDetailChat.moveCamera();
		if (tabCur != null)
		{
			tabCur.scrChat.setYScrool(CamDetailChat.cmx, CamDetailChat.cmxLim);
		}
		if (tabCur.tfchat != null)
		{
			tabCur.tfchat.update();
		}
		base.update();
	}

	public override void updatekey()
	{
		int num = idSelect;
		if (GameCanvas.keyMyHold[4])
		{
			GameCanvas.clearKeyHold(4);
			if (idSelect > 0)
			{
				idSelect--;
			}
		}
		else if (GameCanvas.keyMyHold[6])
		{
			GameCanvas.clearKeyHold(6);
			if (idSelect < vecTabChat.size() - 1)
			{
				idSelect++;
			}
		}
		else if (GameCanvas.keyMyHold[2])
		{
			GameCanvas.clearKeyHold(2);
			CamDetailChat.cmtoX -= GameCanvas.hText;
			if (CamDetailChat.cmtoX < 0)
			{
				CamDetailChat.cmtoX = 0;
			}
		}
		else if (GameCanvas.keyMyHold[8])
		{
			GameCanvas.clearKeyHold(8);
			CamDetailChat.cmtoX += GameCanvas.hText;
			if (CamDetailChat.cmtoX > CamDetailChat.cmxLim)
			{
				CamDetailChat.cmtoX = CamDetailChat.cmxLim;
			}
		}
		if (num != idSelect)
		{
			getCurTab(idSelect);
			CamTab.setToX(xBe + idSelect * wPaintTab / 2 + wPaintTab / 2 - w / 2);
		}
		updateCmd();
		updatekeyPCForTField();
	}

	public void updateCmd()
	{
		base.updatekey();
	}

	public override void updatePointer()
	{
		CamTab.updatePos_LEFT_RIGHT();
		CamDetailChat.update_Pos_UP_DOWN();
		if (GameCanvas.isPointSelect(x + wItem / 2, y, w - wItem, wItem + miniItem * 2))
		{
			int num = (CamTab.cmx + GameCanvas.px - xBe) / (wPaintTab / 2);
			if (num > idSelect)
			{
				num--;
			}
			if (num >= 0 && num < vecTabChat.size() && num != idSelect)
			{
				getCurTab(num);
			}
		}
		if (tabCur.tfchat != null)
		{
			tabCur.tfchat.updatePointer();
		}
		base.updatePointer();
	}

	public override void keyPress(int keyCode)
	{
		if (tabCur.tfchat != null)
		{
			tabCur.tfchat.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	public void updateCameraNew(int size, sbyte type)
	{
		int num = hCon;
		if (tabCur.tfchat != null)
		{
			num -= tabCur.tfchat.height;
		}
		if (tabCur == null)
		{
			return;
		}
		if (type == 1)
		{
			int cmtoX = CamDetailChat.cmtoX;
			int num2 = 0;
			num2 = ((cmtoX != 0 && cmtoX != CamDetailChat.cmxLim) ? ((cmtoX < CamDetailChat.cmxLim - hCon) ? 1 : 2) : 0);
			if (CamDetailChat == null)
			{
				CamDetailChat = new ListNew(xBe, yBe, wCon, num, 0, 0, tabCur.vecDetail.size() * GameCanvas.hText - num, isLim0: true);
			}
			else
			{
				CamDetailChat.cmxLim = tabCur.vecDetail.size() * GameCanvas.hText - num;
				if (CamDetailChat.cmxLim < 0)
				{
					CamDetailChat.cmxLim = 0;
				}
			}
			switch (num2)
			{
			case 0:
				CamDetailChat.setToX(CamDetailChat.cmxLim);
				break;
			case 1:
				CamDetailChat.setToX(cmtoX);
				CamDetailChat.cmx = cmtoX;
				break;
			default:
				CamDetailChat.setToX(cmtoX + size * GameCanvas.hText);
				break;
			}
		}
		else if (type == 0)
		{
			CamDetailChat = new ListNew(xBe, yBe, wCon, num, 0, 0, tabCur.vecDetail.size() * GameCanvas.hText - num, isLim0: true);
			CamDetailChat.setToX(CamDetailChat.cmxLim);
			CamDetailChat.cmx = CamDetailChat.cmxLim;
		}
	}

	public void addNewChatCheckSpam(string name, string FristContent, string content, sbyte type, bool isFocus)
	{
		if (MsgSpamSetup.isCheckSpam(2, name))
		{
			for (int i = 0; i < vecTabChat.size(); i++)
			{
				ChatDetail chatDetail = (ChatDetail)vecTabChat.elementAt(i);
				if (chatDetail.name.CompareTo(name) == 0)
				{
					addNewChat(name, FristContent, content, type, isFocus);
					break;
				}
			}
		}
		else
		{
			addNewChat(name, FristContent, content, type, isFocus);
		}
	}

	public void addNewChat(string name, string FristContent, string content, sbyte type, bool isFocus)
	{
		addNewChat(name, FristContent, content, type, isFocus, -1);
	}

	public void addNewChat(string name, string FristContent, string content, sbyte type, bool isFocus, int color)
	{
		if (content == null)
		{
			return;
		}
		if (content.Length > 0)
		{
			string[] array = mFont.tahoma_7_white.splitFontArray(content, MainEvent.wShort - 50);
			string text = array[0];
			if (array.Length > 1)
			{
				text += "...";
			}
			InfoMemList.setTypeEvent(-1, 2, name, text, 0, 0);
		}
		for (int i = 0; i < vecTabChat.size(); i++)
		{
			ChatDetail chatDetail = (ChatDetail)vecTabChat.elementAt(i);
			if (chatDetail.name.CompareTo(name) == 0)
			{
				if (isFocus)
				{
					idSelect = i;
				}
				if (content.Length > 0)
				{
					chatDetail.addString(FristContent + content, name, color);
				}
				return;
			}
		}
		ChatDetail chatDetail2 = new ChatDetail(name, type);
		if (content.Length > 0)
		{
			chatDetail2.addString(FristContent + content, name, color);
		}
		vecTabChat.addElement(chatDetail2);
		if (isFocus)
		{
			idSelect = vecTabChat.size() - 1;
		}
		if (!GameCanvas.isTouch)
		{
			cmdMenu.caption = T.close;
		}
		int num = (vecTabChat.size() + 1) * wPaintTab / 2 - wCon;
		if (num > 0)
		{
			CamTab.cmxLim = num;
		}
	}

	public void set_text_min_max()
	{
	}

	public virtual void getCurTab(int id)
	{
		idSelect = id;
		if (idSelect < 0 || idSelect >= vecTabChat.size())
		{
			return;
		}
		tabCur = (ChatDetail)vecTabChat.elementAt(idSelect);
		if (GameCanvas.eventScr != null && GameCanvas.eventScr.vecPlayer != null)
		{
			for (int i = 0; i < GameCanvas.eventScr.vecPlayer.size(); i++)
			{
				InfoMemList infoMemList = (InfoMemList)GameCanvas.eventScr.vecPlayer.elementAt(i);
				if (infoMemList.name.CompareTo(tabCur.name) == 0 && infoMemList.typeEvent == 2)
				{
					infoMemList.isNew = false;
					break;
				}
			}
		}
		tabCur.isNew = false;
		updateCameraNew(0, 0);
		getRightCmd();
		if (tabCur.name.CompareTo(T.hethong) == 0)
		{
			Player.strHethong = string.Empty;
		}
	}

	public void updateCamTab()
	{
		CamTab = new ListNew(x, y, wCon, wItem + miniItem * 2, 0, 0, (vecTabChat.size() + 1) * wPaintTab / 2 - wCon, isLim0: true);
	}

	public void getRightCmd()
	{
		if (!GameCanvas.isTouchNoOrPC() || tabCur == null)
		{
			return;
		}
		if (tabCur.tfchat != null)
		{
			if (!GameCanvas.isTouch)
			{
				right = tabCur.tfchat.cmdClear;
			}
			tabCur.tfchat.setFocus(isFocus: true);
			center = cmdChat;
		}
		else
		{
			right = null;
			center = null;
		}
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdCloseChat != null)
		{
			cmdCloseChat.perform();
		}
		return false;
	}
}
