public class ChatDetail : AvMain
{
	public int x;

	public int y;

	public int miniItem = 5;

	public int xBe;

	public int yBe;

	public int wCon;

	public int hCon;

	public int wItem;

	public int hChat;

	public int idSelect;

	public ListNew CamDetailChat;

	public mVector vecDetail = new mVector("ChatDetail.vecDetail");

	public string name;

	public string friend;

	public string shortName;

	public string shortNameFocus;

	public sbyte timeNew = -1;

	public bool isNew;

	public TField tfchat;

	public sbyte typeChat;

	public const sbyte TYPE_CHAT = 0;

	public const sbyte TYPE_SERVER = 1;

	public const sbyte TYPE_ADDFRIEND = 2;

	public const sbyte TYPE_CLAN_CHAT = 3;

	public const sbyte TYPE_CLAN_MEM = 4;

	public const sbyte TYPE_CLAN_INFO = 5;

	public int limY;

	private int indexColor;

	private sbyte typeColorChat;

	public const sbyte TYPE_TROCHUYEN = 0;

	public const sbyte TYPE_BANGHOI_NHOM = 1;

	public Scroll scrChat = new Scroll();

	public const sbyte BG_BLUE_DARK = 0;

	public const sbyte BG_BLUE = 1;

	public const sbyte BG_RED = 2;

	public const sbyte BG_ORANGE = 3;

	public const sbyte BG_BROWN = 4;

	public const sbyte BG_GREEN = 5;

	public const sbyte BG_GOLD = 6;

	public const sbyte BG_SILVER = 7;

	public const sbyte BG_COPPER = 8;

	public const sbyte BG_WHITE = 9;

	public const sbyte BG_FOCUS = 10;

	public ChatDetail(string name, sbyte type)
	{
		this.name = name;
		typeChat = type;
		shortName = name;
		if (name.Length >= 5)
		{
			shortName = name.Substring(0, 5);
		}
		shortNameFocus = name;
		if (name.Length >= 10)
		{
			shortNameFocus = name.Substring(0, 9) + "...";
		}
		if (typeChat == 0)
		{
			tfchat = new TField(GameCanvas.chatTabScr.xBe, GameCanvas.chatTabScr.yBe + GameCanvas.chatTabScr.hCon - TField.getHeight() - GameCanvas.chatTabScr.miniItem / 2, GameCanvas.chatTabScr.wCon);
			tfchat.isCloseKey = false;
		}
		else if (typeChat == 2)
		{
			friend = name;
			this.name = T.addFriend;
		}
		if (name.CompareTo(T.tabBangHoi) == 0 || name.CompareTo(T.tabServer) == 0 || name.CompareTo(T.tabBangChu) == 0)
		{
			typeColorChat = 1;
		}
		else
		{
			typeColorChat = 0;
		}
		scrChat.setInfo(GameCanvas.chatTabScr.xBe + GameCanvas.chatTabScr.wCon + GameCanvas.chatTabScr.miniItem * 2, GameCanvas.chatTabScr.yBe, GameCanvas.chatTabScr.hCon, 8809550);
	}

	public void addString(string str, string nametext, int color)
	{
		if (str.Length <= 0)
		{
			return;
		}
		string[] array = mFont.tahoma_7_white.splitFontArray(str, GameCanvas.chatTabScr.wCon);
		sbyte b = 0;
		b = ((color < 0) ? setColorText(nametext) : ((sbyte)color));
		MainTextChat[] array2 = addChatNew(array, b);
		if (array2 != null)
		{
			for (int i = 0; i < array2.Length; i++)
			{
				vecDetail.addElement(array2[i]);
			}
		}
		setLim();
		if (limY > 0 && GameCanvas.currentScreen == GameCanvas.chatTabScr && GameCanvas.chatTabScr.tabCur != null && GameCanvas.chatTabScr.tabCur == this)
		{
			GameCanvas.chatTabScr.updateCameraNew(array.Length, 1);
		}
		if (((GameCanvas.chatTabScr.tabCur != null && GameCanvas.chatTabScr.tabCur != this) || GameCanvas.currentScreen != GameCanvas.chatTabScr) && name.CompareTo(T.tabServer) != 0)
		{
			isNew = true;
			timeNew = (sbyte)CRes.random(1, 11);
		}
	}

	public void addStartChat(string nametext)
	{
		string text = string.Empty;
		if (tfchat != null)
		{
			text = tfchat.getText();
		}
		if (text.Length > 0)
		{
			string[] array = mFont.tahoma_7_white.splitFontArray(GameScreen.player.name + ": " + text, GameCanvas.chatTabScr.wCon);
			MainTextChat[] array2 = addChatNew(array, setColorText(nametext));
			if (array2 != null)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					vecDetail.addElement(array2[i]);
				}
			}
			setLim();
			if (GameCanvas.currentScreen == GameCanvas.chatTabScr && GameCanvas.chatTabScr.tabCur != null && GameCanvas.chatTabScr.tabCur == this)
			{
				GameCanvas.chatTabScr.updateCameraNew(array.Length, 1);
			}
			GlobalService.gI().chatTab(name, text);
		}
		if (tfchat != null)
		{
			tfchat.setText(string.Empty);
		}
	}

	public void setLim()
	{
		limY = vecDetail.size() * GameCanvas.hText - (GameCanvas.chatTabScr.hCon - ((typeChat == 0) ? (TField.getHeight() + 2) : 0));
		if (limY < 0)
		{
			limY = 0;
		}
	}

	public MainTextChat[] addChatNew(string[] mstr, sbyte color)
	{
		if (mstr == null || mstr.Length == 0)
		{
			return null;
		}
		MainTextChat[] array = new MainTextChat[mstr.Length];
		for (int i = 0; i < mstr.Length; i++)
		{
			array[i] = new MainTextChat(mstr[i], color);
		}
		return array;
	}

	private sbyte setColorText(string name)
	{
		sbyte b = 0;
		if (typeColorChat == 1)
		{
			b = (sbyte)((indexColor % 2 != 0) ? colorServer() : 0);
			indexColor++;
		}
		else if (name.CompareTo(GameScreen.player.name) == 0)
		{
			b = 5;
		}
		else
		{
			if (GameCanvas.IndexServer == 1)
			{
				return 1;
			}
			b = 0;
		}
		return b;
	}

	private sbyte colorServer()
	{
		if (GameCanvas.IndexServer == 1)
		{
			return 1;
		}
		return 5;
	}

	public override void paint(mGraphics g)
	{
		base.paint(g);
	}

	public virtual void setPos(int xBe, int yBe, int wCon, int hCon, int miniItem, int hchat)
	{
	}

	public override void update()
	{
	}

	public override void updatePointer()
	{
	}

	public override void updatekey()
	{
		base.updatekey();
	}

	public virtual void addStringClan(short ID, string str, string nametext, string textRight, sbyte typeBg, sbyte IDEvent, short IdMem, long time)
	{
	}

	public virtual void updateCameraNew(int size, sbyte type)
	{
		int num = hCon;
		if (tfchat != null)
		{
			num -= tfchat.height;
		}
		switch (type)
		{
		case 1:
		{
			int cmtoX = CamDetailChat.cmtoX;
			int num2 = 0;
			num2 = ((cmtoX != 0 && cmtoX != CamDetailChat.cmxLim) ? ((cmtoX < CamDetailChat.cmxLim - hCon) ? 1 : 2) : 0);
			if (CamDetailChat == null)
			{
				CamDetailChat = new ListNew(xBe, yBe, wCon, num, 0, 0, vecDetail.size() * GameCanvas.hText - num, isLim0: true);
			}
			else
			{
				CamDetailChat.cmxLim = vecDetail.size() * GameCanvas.hText - num;
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
			break;
		}
		case 0:
			CamDetailChat = new ListNew(xBe, yBe, wCon, num, 0, 0, vecDetail.size() * GameCanvas.hText - num, isLim0: true);
			CamDetailChat.setToX(CamDetailChat.cmxLim);
			CamDetailChat.cmx = CamDetailChat.cmxLim;
			idSelect = vecDetail.size() - 1;
			break;
		}
	}

	public void sendChat()
	{
		if (tfchat != null && tfchat.getText().Length > 0 && typeChat == 3)
		{
			GlobalService.gI().Clan_CMD(0, tfchat.getText(), 0, 0);
			tfchat.setText(string.Empty);
		}
	}

	public virtual void beginFocus()
	{
	}

	public void paintBorder(mGraphics g, sbyte typeColorBg, sbyte typeChatLeftRight, int wLech, int hpaint, int ypaint, bool isfocus)
	{
		if (!GameCanvas.isTouchNoOrPC())
		{
			isfocus = false;
		}
		if (typeColorBg < 0 || typeColorBg > 9)
		{
			typeColorBg = 4;
		}
		g.setColor(getColorBg(typeColorBg));
		int num = xBe - 2 + wLech;
		int num2 = ypaint - 1;
		int num3 = wCon + 4 - wLech * 2;
		g.fillRect(num, num2, num3, hpaint);
		if (isfocus && GameCanvas.gameTick % 12 < 6)
		{
			typeColorBg = 10;
		}
		g.setColor(getColorBorderBg(typeColorBg));
		AvMain.fraBorderClan.drawFrame(typeColorBg * 4, num, num2, 0, 0, g);
		AvMain.fraBorderClan.drawFrame(typeColorBg * 4 + 1, num - miniItem + num3 + 1, num2, 0, 0, g);
		AvMain.fraBorderClan.drawFrame(typeColorBg * 4 + 2, num, num2 + hpaint - 4, 0, 0, g);
		AvMain.fraBorderClan.drawFrame(typeColorBg * 4 + 3, num - miniItem + num3 + 1, num2 + hpaint - 4, 0, 0, g);
		g.fillRect(num, ypaint + 3, 1, hpaint - 8);
		g.fillRect(num - 1 + num3, ypaint + 3, 1, hpaint - 8);
		g.fillRect(num + 4, ypaint - 1, num3 - 8, 1);
		g.fillRect(num + 4, ypaint + hpaint - 2, num3 - 8, 1);
		switch (typeChatLeftRight)
		{
		case 1:
			g.drawRegion(AvMain.imgChatClan, 0, 7, 7, 7, 0, num - 6, num2 + 12, 0);
			break;
		case 2:
			g.drawRegion(AvMain.imgChatClan, 0, 0, 7, 7, 0, num + num3 - 1, num2 + 12, 0);
			break;
		}
	}

	public int getColorBg(sbyte typecolor)
	{
		return typecolor switch
		{
			0 => 6526926, 
			1 => 8629951, 
			2 => 14834512, 
			3 => 14847037, 
			4 => 12093538, 
			5 => 8768101, 
			6 => 16441185, 
			7 => 14013137, 
			8 => 13072991, 
			9 => 15790320, 
			_ => 12093538, 
		};
	}

	public int getColorBorderBg(sbyte typecolor)
	{
		return typecolor switch
		{
			0 => 2645913, 
			1 => 4027015, 
			2 => 10432038, 
			3 => 10574103, 
			4 => 9068345, 
			5 => 5807167, 
			6 => 14594092, 
			7 => 8749954, 
			8 => 10178612, 
			9 => 11645361, 
			10 => 0, 
			_ => 9068345, 
		};
	}

	public int getColorBorderNumber(sbyte typecolor)
	{
		return typecolor switch
		{
			5 => 4239412, 
			4 => 9986635, 
			_ => 9986635, 
		};
	}

	public virtual void setIsNew(bool isnew)
	{
		isNew = isnew;
	}
}
