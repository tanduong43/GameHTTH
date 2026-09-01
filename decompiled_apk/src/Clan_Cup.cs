public class Clan_Cup : ChatDetail
{
	private int minChat;

	private int maxChat;

	public Clan_Cup(string name, sbyte type)
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
		updateCameraNew(0, 0);
	}

	public override void beginFocus()
	{
		CamDetailChat.cmx = 0;
		CamDetailChat.cmtoX = 0;
		idSelect = 0;
		updateTime();
	}

	public override void paint(mGraphics g)
	{
		if (tfchat != null)
		{
			tfchat.paint(g);
		}
		g.setClip(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon - ((tfchat == null) ? (-miniItem) : tfchat.height) + 2);
		g.saveCanvas();
		g.ClipRec(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon - ((tfchat == null) ? (-miniItem) : tfchat.height) + 2);
		g.translate(0, -CamDetailChat.cmx);
		minChat = CamDetailChat.cmx / GameCanvas.hText - 8;
		if (minChat < 0)
		{
			minChat = 0;
		}
		maxChat = minChat + hChat + 10;
		for (int i = minChat; i <= maxChat; i++)
		{
			if (i < vecDetail.size() && i >= 0)
			{
				MainTextChat mainTextChat = (MainTextChat)vecDetail.elementAt(i);
				if (mainTextChat.lenghtText > 0)
				{
					paintBorder(g, mainTextChat.typeBg, -1, 0, mainTextChat.lenghtText * GameCanvas.hText - 3, yBe + i * GameCanvas.hText, isfocus: false);
					AvMain.FontBorderColor(g, mainTextChat.text, xBe + miniItem + 2, yBe + i * GameCanvas.hText + 1, 0, mainTextChat.color, 7);
				}
				else if (mainTextChat.timeBegin > -1)
				{
					mFont.tahoma_7_black.drawString(g, mainTextChat.textTime, xBe - miniItem + wCon, yBe + i * GameCanvas.hText - 2, 1);
				}
				else
				{
					mFont.tahoma_7b_black.drawString(g, mainTextChat.text, xBe + miniItem + 2, yBe + i * GameCanvas.hText, 0);
				}
				if (mainTextChat.textRight.Length > 0)
				{
					mFont.tahoma_7_white.drawString(g, mainTextChat.textRight, xBe + wCon - miniItem, yBe + i * GameCanvas.hText + 1, 1);
				}
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
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

	public override void updatePointer()
	{
		CamDetailChat.update_Pos_UP_DOWN();
		if (tfchat != null)
		{
			tfchat.updatePointer();
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
		addStringClan(IDChat, str, name, textRight, 9, -1, IDMem, time);
	}

	public override void addStringClan(short ID, string str, string nametext, string textRight, sbyte typeBg, sbyte IDEvent, short IdMem, long time)
	{
		if (str.Length <= 0)
		{
			return;
		}
		string empty = string.Empty;
		empty = nametext + "\n" + str;
		string[] array = mFont.tahoma_7b_white.splitFontArray(empty, GameCanvas.chatTabScr.wCon - 12);
		MainTextChat[] array2 = addChatNew(array, 0);
		if (array2 != null)
		{
			for (int i = 0; i < array2.Length; i++)
			{
				if (i == 0)
				{
					array2[0].lenghtText = (sbyte)(array2.Length + 1);
					array2[0].typeBg = typeBg;
				}
				vecDetail.addElement(array2[i]);
			}
			MainTextChat mainTextChat = new MainTextChat(string.Empty, 0);
			mainTextChat.timeBegin = time;
			mainTextChat.setTimePaint();
			vecDetail.addElement(mainTextChat);
		}
		setLim();
		if (limY > 0)
		{
			updateCameraNew(array.Length, 1);
		}
	}

	public override void updatekey()
	{
		int num = idSelect;
		if (GameCanvas.keyMyHold[2])
		{
			GameCanvas.clearKeyHold(2);
			if (idSelect > 0)
			{
				idSelect--;
			}
		}
		else if (GameCanvas.keyMyHold[8])
		{
			GameCanvas.clearKeyHold(8);
			if (idSelect < vecDetail.size() - 1)
			{
				idSelect++;
			}
		}
		if (num != idSelect)
		{
			setXCam();
		}
		base.updatekey();
	}

	private void setXCam()
	{
		int num = idSelect * GameCanvas.hText;
		if (num > CamDetailChat.cmxLim)
		{
			idSelect = CamDetailChat.cmxLim / GameCanvas.hText + 1;
		}
		CamDetailChat.setToX(num);
	}
}
