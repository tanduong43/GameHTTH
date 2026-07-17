public class Sudo_Chat : ChatDetail
{
	private iCommand cmdView;

	private iCommand cmdChoVaoClan;

	private iCommand cmdTuChoiVaoClan;

	private iCommand cmdChat;

	private InfoMemList memCur;

	private MainTextChat chatCur;

	private int minChat;

	private int maxChat;

	public const sbyte EVENT_CHAT = -1;

	public const sbyte EVENT_XIN_VAO_CLAN = 1;

	public const sbyte EVENT_MAU_DO = -2;

	public const sbyte EVENT_THONG_BAO = -3;

	public const sbyte EVENT_DONATE = -4;

	public Sudo_Chat(string name, sbyte type)
		: base(name, type)
	{
	}

	public override void setPos(int xBe, int yBe, int wCon, int hCon, int miniItem, int hchat)
	{
		base.xBe = xBe;
		base.yBe = yBe;
		base.wCon = wCon;
		base.hCon = hCon;
		base.miniItem = miniItem;
		hChat = hchat;
		tfchat = new TField(xBe + 60, yBe + hCon - TField.getHeight(), wCon - 60);
		tfchat.setStringNull(T.TroChuyen);
		tfchat.setFocus(isFocus: true);
		updateCameraNew(0, 0);
		cmdView = new iCommand(T.view, 0, this);
		cmdChoVaoClan = new iCommand(T.chapnhan, 1, this);
		cmdTuChoiVaoClan = new iCommand(T.tuchoi, 2, this);
		cmdChat = new iCommand(T.chat, 3, this);
		if (!GameCanvas.isTouch)
		{
			center = cmdChat;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (memCur != null)
			{
				mVector mVector2 = new mVector();
				mVector2.addElement(cmdChoVaoClan);
				mVector2.addElement(cmdTuChoiVaoClan);
				GameCanvas.Start_Normal_DiaLog(memCur.name + T.muonvaoClan, mVector2, isCmdClose: true);
			}
			break;
		case 1:
			GameCanvas.end_Dialog();
			if (memCur != null)
			{
				GlobalService.gI().Sudo_CMD(18, memCur.name, memCur.idEvent, 0);
			}
			break;
		case 2:
			GameCanvas.end_Dialog();
			if (memCur != null)
			{
				GlobalService.gI().Sudo_CMD(20, memCur.name, memCur.idEvent, 0);
			}
			break;
		case 3:
			sendChat();
			break;
		}
	}

	public override void beginFocus()
	{
		idSelect = vecDetail.size() - 1;
		for (int num = idSelect; num >= 0; num--)
		{
			MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(num);
			if (mainTextChat.lenghtText > 0)
			{
				idSelect = num;
				break;
			}
		}
		checkRemoveChat();
		setXCam();
		updateTime();
	}

	public override void paint(mGraphics g)
	{
		if (tfchat != null)
		{
			tfchat.paint(g);
		}
		AvMain.paintRect(g, xBe, yBe + hCon - TField.getHeight(), 60 - miniItem, TField.getHeight() + 1, 0, 1);
		if (GameCanvas.isTouch)
		{
			g.drawImage(AvMain.imgDonateClan, xBe + 30 - miniItem / 2, yBe + hCon - TField.getHeight() + 2 + 7, 3);
			if (MainTab.CDDonateClan.timeCountDown <= 0)
			{
				mFont.tahoma_7_white.drawString(g, T.tangqua, xBe + 30 - miniItem / 2, yBe + hCon - TField.getHeight() + 17, 2);
			}
			else
			{
				MainTab.CDDonateClan.paintCountDownTicketHour(g, mFont.tahoma_7_white, xBe + 18 - miniItem / 2, yBe + hCon - TField.getHeight() + 17, 0);
			}
		}
		else if (MainTab.CDDonateClan.timeCountDown <= 0)
		{
			g.drawImage(AvMain.imgDonateClan, xBe + 10 - miniItem / 2, yBe + hCon - TField.getHeight() + 2 + 10, 3);
			mFont.tahoma_7_white.drawString(g, T.tangqua, xBe + 37 - miniItem / 2, yBe + hCon - TField.getHeight() + 17 - 12, 2);
		}
		else
		{
			g.drawImage(AvMain.imgDonateClan, xBe + 16 - miniItem / 2, yBe + hCon - TField.getHeight() + 2 + 10, 3);
			MainTab.CDDonateClan.paintCountDownTicketHour(g, mFont.tahoma_7_white, xBe + 39 - miniItem / 2, yBe + hCon - TField.getHeight() + 17 - 11, 0);
		}
		g.setClip(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon - ((tfchat == null) ? (-miniItem) : tfchat.height) + 2);
		g.saveCanvas();
		g.ClipRec(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon - ((tfchat == null) ? (-miniItem) : tfchat.height) + 2);
		g.translate(0, -CamDetailChat.cmx);
		minChat = CamDetailChat.cmx / GameCanvas.hText - 4;
		if (minChat < 0)
		{
			minChat = 0;
		}
		maxChat = minChat + hChat;
		for (int i = minChat; i <= maxChat; i++)
		{
			if (i >= vecDetail.size() || i < 0)
			{
				continue;
			}
			MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(i);
			if (mainTextChat.lenghtText > 0)
			{
				paintBorder(g, mainTextChat.typeBg, mainTextChat.typeLeftRight, miniItem, GameCanvas.hText * mainTextChat.lenghtText - 2, yBe + i * GameCanvas.hText, i == idSelect);
				if (mainTextChat.typeBg == 2)
				{
					AvMain.setTextColorName(mainTextChat.color).drawString(g, mainTextChat.text, xBe + miniItem + 2, yBe + i * GameCanvas.hText, 0);
				}
				else
				{
					AvMain.FontBorderColor(g, mainTextChat.text, xBe + miniItem + 2, yBe + i * GameCanvas.hText + 1, 0, mainTextChat.color, 7);
				}
				if (mainTextChat.typeEvent > -1 && MainClan.isPhongChuc())
				{
					AvMain.paintRect(g, xBe + miniItem + 2, yBe + i * GameCanvas.hText + (mainTextChat.lenghtText - 1) * GameCanvas.hText - 2, 30, GameCanvas.hText - 5, 3, 1);
					mFont.tahoma_7_white.drawString(g, T.view, xBe + miniItem + 2 + 15, yBe + i * GameCanvas.hText + (mainTextChat.lenghtText - 1) * GameCanvas.hText - 2, 2);
				}
			}
			else if (mainTextChat.timeBegin > -1)
			{
				mFont.tahoma_7_black.drawString(g, mainTextChat.textTime, xBe - miniItem + wCon, yBe + i * GameCanvas.hText - 2, 1);
			}
			else
			{
				AvMain.setTextColorName(mainTextChat.color).drawString(g, mainTextChat.text, xBe + miniItem + 2, yBe + i * GameCanvas.hText, 0);
			}
			if (mainTextChat.textRight.Length > 0)
			{
				mFont.tahoma_7_white.drawString(g, mainTextChat.textRight, xBe + wCon - miniItem, yBe + i * GameCanvas.hText + 1, 1);
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		base.paint(g);
	}

	public override void update()
	{
		CamDetailChat.moveCamera();
		if (tfchat != null)
		{
			tfchat.update();
		}
		if (GameCanvas.gameTick % 500 == 0)
		{
			updateTime();
			if (CamDetailChat.cmx == CamDetailChat.cmxLim)
			{
				checkRemoveChat();
			}
		}
	}

	public void updateTime()
	{
		for (int i = 0; i < vecDetail.size(); i++)
		{
			MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(i);
			if (mainTextChat.timeBegin > 0)
			{
				mainTextChat.setTimePaint();
			}
		}
	}

	public override void updatekey()
	{
		int num = idSelect;
		if (GameCanvas.keyMyHold[2])
		{
			GameCanvas.clearKeyHold(2);
			if (num > 0)
			{
				num--;
			}
		}
		else if (GameCanvas.keyMyHold[8])
		{
			GameCanvas.clearKeyHold(8);
			if (num < vecDetail.size() - 1)
			{
				num++;
			}
		}
		if (num != idSelect)
		{
			MainTextChat mainTextChat = null;
			if (num < idSelect)
			{
				for (int num2 = num; num2 >= 0; num2--)
				{
					mainTextChat = (MainTextChat)vecDetail.elementAt(num2);
					if (mainTextChat.lenghtText > 0)
					{
						idSelect = num2;
						break;
					}
				}
			}
			else
			{
				for (int i = num; i < vecDetail.size(); i++)
				{
					mainTextChat = (MainTextChat)vecDetail.elementAt(i);
					if (mainTextChat.lenghtText > 0)
					{
						idSelect = i;
						break;
					}
				}
			}
			chatCur = mainTextChat;
			left = null;
			if (MainClan.isPhongChuc())
			{
				getCmdEvent();
			}
			setXCam();
		}
		base.updatekey();
	}

	private void setXCam()
	{
		int num = idSelect;
		if (chatCur != null)
		{
			num += chatCur.lenghtText;
		}
		int toX = num * GameCanvas.hText - hCon / 4;
		CamDetailChat.setToX(toX);
	}

	private void getCmdEvent()
	{
		if (idSelect < 0 || idSelect >= vecDetail.size())
		{
			return;
		}
		for (int num = idSelect; num >= 0; num--)
		{
			MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(num);
			if (mainTextChat.lenghtText > 0)
			{
				if (mainTextChat.typeEvent < 0)
				{
					break;
				}
				if (mainTextChat.typeEvent == 1)
				{
					memCur = new InfoMemList(mainTextChat.IDMem);
					memCur.name = mainTextChat.nameText;
					memCur.idEvent = mainTextChat.IdText;
					left = cmdView;
				}
			}
		}
	}

	public override void updatePointer()
	{
		CamDetailChat.update_Pos_UP_DOWN();
		if (tfchat != null)
		{
			tfchat.updatePointer();
		}
		if (GameCanvas.isPointerSelect && vecDetail.size() > 0 && GameCanvas.isPoint(xBe, yBe, wCon, hCon))
		{
			GameCanvas.isPointerSelect = false;
			int num = (GameCanvas.py - yBe + CamDetailChat.cmx) / GameCanvas.hText;
			if (num >= 0 && num < vecDetail.size())
			{
				idSelect = num;
				setEvent();
			}
		}
	}

	private void setEvent()
	{
		if (idSelect < 0 || idSelect >= vecDetail.size())
		{
			return;
		}
		for (int num = idSelect; num >= 0; num--)
		{
			MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(num);
			if (mainTextChat.lenghtText > 0)
			{
				idSelect = num;
				if (mainTextChat.typeEvent >= 0 && mainTextChat.typeEvent == 1 && MainClan.isPhongChuc() && (Player.ChucInCLan == 0 || Player.ChucInCLan == 1 || Player.ChucInCLan == 2))
				{
					memCur = new InfoMemList(mainTextChat.IDMem);
					memCur.name = mainTextChat.nameText;
					memCur.idEvent = mainTextChat.IdText;
					cmdView.perform();
				}
				break;
			}
		}
	}

	public override void addStringClan(short ID, string str, string nametext, string textRight, sbyte typeBg, sbyte IDEvent, short IdMem, long time)
	{
		if (str.Length <= 0)
		{
			return;
		}
		string empty = string.Empty;
		empty = ((typeBg == 2) ? (nametext + " " + str) : (nametext + "\n" + str));
		string[] array = mFont.tahoma_7b_white.splitFontArray(empty, GameCanvas.chatTabScr.wCon - 12);
		MainTextChat[] array2 = addChatNew(array, 0);
		if (array2 != null)
		{
			for (int i = 0; i < array2.Length; i++)
			{
				if (i == 0)
				{
					if (typeBg != 2)
					{
						array2[0].textRight = textRight;
						if (typeBg == 1)
						{
							array2[0].typeLeftRight = 1;
						}
						else if (typeBg == 0)
						{
							array2[0].typeLeftRight = 2;
						}
					}
					array2[0].lenghtText = (sbyte)(array2.Length + 1);
					array2[0].typeBg = typeBg;
					array2[0].typeEvent = IDEvent;
					array2[0].nameText = nametext;
					array2[0].IDMem = IdMem;
					if (array2[0].text.CompareTo(GameScreen.player.name) == 0)
					{
						array2[0].text = T.ban;
						array2[0].color = 4;
					}
				}
				array2[i].IdText = ID;
				vecDetail.addElement(array2[i]);
			}
			MainTextChat mainTextChat = new MainTextChat(string.Empty, 0);
			mainTextChat.timeBegin = time;
			mainTextChat.setTimePaint();
			mainTextChat.IdText = ID;
			vecDetail.addElement(mainTextChat);
		}
		setLim();
		if (limY > 0)
		{
			updateCameraNew(array.Length, 1);
		}
	}

	public void addOneChat(short IDChat, short IDMem, string name, string str, sbyte typeAction, long time)
	{
		InfoMemList memInCLan = Clan_Screen.getMemInCLan(name);
		string textRight = string.Empty;
		if (memInCLan != null)
		{
			textRight = memInCLan.getCaptionClan(memInCLan.chucInClan);
		}
		if (typeAction == -1)
		{
			if (IDMem == GameScreen.player.ID)
			{
				addStringClan(IDChat, str, name, textRight, 1, -1, IDMem, time);
			}
			else
			{
				addStringClan(IDChat, str, name, textRight, 0, -1, IDMem, time);
			}
		}
		else if (typeAction == 1)
		{
			addStringClan(IDChat, str, name, string.Empty, 3, 1, IDMem, time);
		}
		else if (typeAction == -2)
		{
			addStringClan(IDChat, str, name, string.Empty, 2, -1, IDMem, time);
		}
		else if (typeAction == -3)
		{
			addStringClan(IDChat, str, T.thongbaobang, name, 3, -1, IDMem, time);
		}
		else if (typeAction == -4)
		{
			addStringClan(IDChat, str, name, textRight, 5, -1, IDMem, time);
		}
	}

	public void RemoveChat(short id)
	{
		bool flag = false;
		for (int i = 0; i < vecDetail.size(); i++)
		{
			MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(i);
			if (mainTextChat.IdText == id)
			{
				vecDetail.removeElementAt(i);
				i--;
				flag = true;
			}
			else if (flag)
			{
				break;
			}
		}
		int num = hCon;
		if (tfchat != null)
		{
			num -= tfchat.height;
		}
		limY = vecDetail.size() * GameCanvas.hText - num;
		if (limY > 0 && limY != CamDetailChat.cmxLim)
		{
			CamDetailChat.cmxLim = limY;
		}
	}

	public void checkRemoveChat()
	{
		int num = 0;
		for (int num2 = vecDetail.size(); num2 >= 0; num2--)
		{
			MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(num2);
			if (mainTextChat != null)
			{
				if (num > 30)
				{
					vecDetail.removeElementAt(num2);
				}
				else if (mainTextChat.lenghtText > 0)
				{
					num++;
				}
			}
			else
			{
				vecDetail.removeElementAt(num2);
			}
		}
		if (num > 30)
		{
			setLim();
			if (limY > 0)
			{
				updateCameraNew(0, 0);
			}
			setXCam();
		}
	}

	public new void sendChat()
	{
		if (tfchat != null && tfchat.getText().Length > 0 && typeChat == 3)
		{
			GlobalService.gI().Sudo_CMD(15, tfchat.getText(), 0, 0);
			tfchat.setText(string.Empty);
		}
	}
}
