public class InfoMemList
{
	public const sbyte ADD_FRIEND = 0;

	public const sbyte ADD_PARTY = 1;

	public const sbyte ADD_CHAT = 2;

	public const sbyte ADD_FIGHT = 3;

	public const sbyte ADD_TRADE = 4;

	public const sbyte ADD_XIN_VAO_PARTY = 5;

	public const sbyte ADD_MOI_VAO_CLAN = 6;

	public const sbyte ADD_CLAN_FIGHT = 7;

	public const sbyte ADD_BAISU = 8;

	public const sbyte CAPTION_TRUONG = 0;

	public const sbyte CAPTION_PHO = 1;

	public const sbyte CAPTION_HOATIEU = 2;

	public const sbyte CAPTION_MEM = 10;

	public const sbyte CAPTION_SUPHU = 1;

	public const sbyte CAPTION_DETU = 2;

	private int x;

	private int y;

	public short idmap;

	public short idArea;

	public string name;

	public string info = string.Empty;

	public string infoFull;

	public string title;

	public string expSudo;

	public int id;

	public int idEvent;

	public int hp;

	public int maxhp;

	public int Lv;

	public int priceFight;

	public int numTangqua;

	public int numQuest;

	public int rank;

	public sbyte isMain;

	public sbyte typeOnline = -1;

	public sbyte typeEvent;

	public sbyte indexIconEvent;

	public sbyte chucInClan;

	public sbyte typeFight;

	public sbyte chucInSudo = -1;

	public short lvSudo;

	public short head;

	public short hair;

	public short hat;

	public short body;

	public short leg;

	public short weapon;

	public bool isNew;

	public bool isMe;

	public int gopRuby;

	public int congHien;

	public MainObject charShow;

	public string namePlayerNhan;

	public sbyte isWantedSuccess;

	public sbyte isReceiveGift;

	public sbyte isOwn;

	public QuaNapThe quaNapThe;

	public ItemQuaNT item;

	public InfoMemList(int id)
	{
		this.id = id;
		Lv = -1;
	}

	public void updateXY(int x, int y)
	{
	}

	public void updateFace(short head, short hair, short hat)
	{
		this.head = head;
		this.hair = hair;
		this.hat = hat;
	}

	public void updateChar(short head, short hair, short hat, short body, short leg, short weapon)
	{
		charShow = new MainObject();
		charShow.sethead(head);
		charShow.sethair(hair);
		charShow.hat = hat;
		charShow.body = body;
		charShow.leg = leg;
		charShow.weapon = weapon;
	}

	public void updateData(string name, short idMap, sbyte isMain, sbyte idArea)
	{
		this.name = name;
		idmap = idMap;
		this.isMain = isMain;
		this.idArea = idArea;
	}

	public void updateHP(int hp, int maxhp, int Lv)
	{
		this.hp = hp;
		this.maxhp = maxhp;
		this.Lv = Lv;
	}

	public void updateMemClan(string name, short Lv, sbyte levelInClan, short numDonate, short numGopRuby, short numQuest, int numCongHien)
	{
		this.name = name;
		this.Lv = Lv;
		chucInClan = levelInClan;
		numTangqua = numDonate;
		gopRuby = numGopRuby;
		this.numQuest = numQuest;
		congHien = numCongHien;
		mSystem.outz("name=" + name + " lv=" + Lv + "chucvu=" + chucInClan + "numdo=" + numDonate + "numq=" + numQuest + "numruby=" + numGopRuby + "congHien=" + numCongHien);
	}

	public static void setTypeEvent(int ID, sbyte typeEvent, string name, string info, int priceFight, int typeFight)
	{
		if (checkReturnEvent(typeEvent, name))
		{
			return;
		}
		InfoMemList infoMemList = getEvent(name, typeEvent);
		bool flag = false;
		if (infoMemList == null)
		{
			infoMemList = new InfoMemList(ID);
			flag = true;
		}
		infoMemList.typeEvent = typeEvent;
		infoMemList.name = name;
		infoMemList.isNew = true;
		infoMemList.info = info;
		infoMemList.priceFight = priceFight;
		if (infoMemList.typeEvent == 0)
		{
			infoMemList.indexIconEvent = 1;
			if (flag)
			{
				Interface_Game.addShowEvent(infoMemList);
			}
		}
		else if (infoMemList.typeEvent == 1)
		{
			infoMemList.indexIconEvent = 2;
			if (flag)
			{
				Interface_Game.addShowEvent(infoMemList);
			}
		}
		else if (infoMemList.typeEvent == 2)
		{
			infoMemList.indexIconEvent = 0;
			infoMemList.isNew = false;
			if (name.CompareTo(T.tabServer) != 0 && info.Length > 0)
			{
				if (flag)
				{
					Interface_Game.addShowEvent(infoMemList);
				}
				infoMemList.isNew = true;
			}
		}
		else if (infoMemList.typeEvent == 3)
		{
			infoMemList.indexIconEvent = 3;
			infoMemList.typeFight = (sbyte)typeFight;
			if (flag)
			{
				Interface_Game.addShowEvent(infoMemList);
			}
		}
		else if (infoMemList.typeEvent == 4)
		{
			infoMemList.indexIconEvent = 4;
			if (flag)
			{
				Interface_Game.addShowEvent(infoMemList);
			}
		}
		else if (infoMemList.typeEvent == 5)
		{
			infoMemList.indexIconEvent = 2;
			infoMemList.infoFull = name + info;
			infoMemList.info = name;
			if (infoMemList.info.Length > 16)
			{
				infoMemList.info = infoMemList.info.Substring(0, 15);
			}
			infoMemList.name = T.xinvao;
			if (flag)
			{
				Interface_Game.addShowEvent(infoMemList);
			}
		}
		else if (infoMemList.typeEvent == 6)
		{
			infoMemList.indexIconEvent = 5;
			if (flag)
			{
				Interface_Game.addShowEvent(infoMemList);
			}
		}
		else if (infoMemList.typeEvent == 7)
		{
			infoMemList.indexIconEvent = 6;
			infoMemList.typeFight = (sbyte)typeFight;
			if (flag)
			{
				Interface_Game.addShowEvent(infoMemList);
			}
		}
		if (flag)
		{
			Player.vecEvent.addElement(infoMemList);
		}
		GameScreen.setNumMess();
	}

	private static bool checkReturnEvent(sbyte type, string name)
	{
		if (type == 0)
		{
			if (MsgSpamSetup.isCheckSpam(2, string.Empty))
			{
				GameCanvas.chatTabScr.addNewChat(T.hethong, string.Empty, name + T.mInfoSpamChat[2] + GameCanvas.getTextTime() + "\n", 1, isFocus: false);
				return true;
			}
		}
		else if (type == 1)
		{
			if (MsgSpamSetup.isCheckSpam(3, string.Empty))
			{
				GameCanvas.chatTabScr.addNewChat(T.hethong, string.Empty, name + T.mInfoSpamChat[3] + GameCanvas.getTextTime() + "\n", 1, isFocus: false);
				return true;
			}
		}
		else if (type == 4)
		{
			if (MsgSpamSetup.isCheckSpam(4, string.Empty))
			{
				GameCanvas.chatTabScr.addNewChat(T.hethong, string.Empty, name + T.mInfoSpamChat[4] + GameCanvas.getTextTime() + "\n", 1, isFocus: false);
				return true;
			}
		}
		else if (type == 3 && MsgSpamSetup.isCheckSpam(5, string.Empty))
		{
			GameCanvas.chatTabScr.addNewChat(T.hethong, string.Empty, name + T.mInfoSpamChat[5] + GameCanvas.getTextTime() + "\n", 1, isFocus: false);
			return true;
		}
		return false;
	}

	public static InfoMemList getMem(int id, mVector vec)
	{
		for (int i = 0; i < vec.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)vec.elementAt(i);
			if (infoMemList.id == id)
			{
				return infoMemList;
			}
		}
		return null;
	}

	public void setAction()
	{
		if (isNew)
		{
			isNew = false;
		}
		GameScreen.setNumMess();
		mVector mVector2 = new mVector();
		switch (typeEvent)
		{
		case 0:
			mVector2.addElement(GameCanvas.eventScr.cmdAddfriend);
			mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
			GameCanvas.Start_Normal_DiaLog(name + " " + T.yeucauketban, mVector2, isCmdClose: true);
			break;
		case 1:
			mVector2.addElement(GameCanvas.eventScr.cmdAddParty);
			mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
			GameCanvas.Start_Normal_DiaLog(name + T.inviteParty, mVector2, isCmdClose: true);
			break;
		case 2:
			if (GameCanvas.currentScreen != GameCanvas.chatTabScr)
			{
				if (!GameCanvas.isTouch && name.CompareTo(T.tabServer) != 0)
				{
					mVector2.addElement(GameCanvas.eventScr.cmdTroChuyen);
					mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
					GameCanvas.Start_Normal_DiaLog(T.TroChuyen + " " + name, mVector2, isCmdClose: true);
				}
				else
				{
					GameCanvas.chatTabScr.addNewChat(name, string.Empty, string.Empty, 0, isFocus: true);
					GameCanvas.chatTabScr.Show(GameCanvas.currentScreen);
				}
			}
			break;
		case 3:
		{
			mVector2.addElement(MainEvent.cmdFight);
			mVector2.addElement(GameCanvas.eventScr.cmdInfoEnemy);
			mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
			string empty = string.Empty;
			empty = ((typeFight != 1) ? GameMidlet.format(T.yeucauthachdauENG, new string[2]
			{
				name,
				string.Empty + priceFight
			}) : GameMidlet.format(T.yeucauthachdauENG2, new string[2]
			{
				name,
				string.Empty + priceFight
			}));
			GameCanvas.Start_Normal_DiaLog(empty, mVector2, isCmdClose: true);
			break;
		}
		case 4:
			mVector2.addElement(GameCanvas.eventScr.cmdTrade);
			mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
			GameCanvas.Start_Normal_DiaLog(name + " " + T.yeucaugiaodich, mVector2, isCmdClose: true);
			break;
		case 5:
			mVector2.addElement(GameCanvas.eventScr.cmdAcceptAddParty);
			mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
			GameCanvas.Start_Normal_DiaLog(infoFull, mVector2, isCmdClose: true);
			break;
		case 6:
			mVector2.addElement(GameCanvas.eventScr.cmdAcceptClan);
			mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
			GameCanvas.Start_Normal_DiaLog(name + T.muonmoivaoClan, mVector2, isCmdClose: true);
			break;
		case 7:
			mVector2.addElement(GameCanvas.eventScr.cmdAcceptFightClan);
			mVector2.addElement(GameCanvas.eventScr.cmdCancelFightClan);
			GameCanvas.Start_Normal_DiaLog(name + T.thachDauClanText, mVector2, isCmdClose: true);
			break;
		case 8:
			mVector2.addElement(GameCanvas.eventScr.cmdAcceptSudo);
			mVector2.addElement(GameCanvas.eventScr.cmdDelEvent);
			GameCanvas.Start_Normal_DiaLog(name + T.muonBaisu, mVector2, isCmdClose: false);
			break;
		}
	}

	public static InfoMemList getEvent(string name, sbyte type)
	{
		for (int i = 0; i < Player.vecEvent.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)Player.vecEvent.elementAt(i);
			if (infoMemList.name.CompareTo(name) == 0 && infoMemList.typeEvent == type)
			{
				return infoMemList;
			}
		}
		return null;
	}

	public string getCaptionClan(sbyte type)
	{
		if (type == 0)
		{
			return T.thuyentruong;
		}
		if (type == 1)
		{
			return T.thuyenpho;
		}
		if (type == 2)
		{
			return T.hoatieu;
		}
		return T.thanhvien;
	}

	public string getCaptionSudo(sbyte type)
	{
		if (type == 1)
		{
			return T.suPhu;
		}
		return T.deTu;
	}

	public void updateCharFace(short head, short hair, short hat)
	{
		if (charShow == null)
		{
			charShow = new MainObject();
		}
		charShow.head = head;
		charShow.hair = hair;
		charShow.hat = hat;
	}

	public void updateCharFullPart(short head, short hair, short hat, short body, short leg, short weapon)
	{
		if (charShow == null)
		{
			charShow = new MainObject();
		}
		charShow.sethead(head);
		charShow.sethair(hair);
		charShow.hat = hat;
		charShow.body = body;
		charShow.leg = leg;
		charShow.weapon = weapon;
	}
}
