public class PartyScreen : PaintListScreen
{
	private iCommand cmdRemoveMem;

	private iCommand cmdLeave;

	private iCommand cmdCancelParty;

	private iCommand cmdChatParty;

	private iCommand cmdInfoBuffParty;

	public static PartyScreen instance;

	public PartyScreen(sbyte type, mVector vec)
		: base(type, vec, T.infoParty, 180, 180)
	{
		cmdRemoveMem = new iCommand(T.kickParty, 3, 0, this);
		cmdLeave = new iCommand(T.leave + " " + T.party, 7, 0, this);
		cmdCancelParty = new iCommand(T.cancel + " " + T.party, 8, 0, this);
		cmdChatParty = new iCommand(T.TroChuyen + " " + T.party, 9, 0, this);
		cmdInfoBuffParty = new iCommand(T.AttriParty, 10, 0, this);
		backCMD = cmdClose;
		okCMD = cmdMenuTouchPlayer;
		menuCMD = cmdMenu;
	}

	public static PartyScreen gI()
	{
		return (instance != null) ? instance : (instance = new PartyScreen(-1, Player.vecParty));
	}

	public override void beginShow()
	{
		if (GameCanvas.isTouchNoOrPC())
		{
			idCommand = 0;
		}
		setPosCmdNew(0, vecMenu);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 3:
			if (memCur != null)
			{
				GlobalService.gI().Party(2, (short)memCur.id);
			}
			break;
		case 7:
			GlobalService.gI().Party(2, GameScreen.player.ID);
			break;
		case 8:
			GlobalService.gI().Party(3, GameScreen.player.ID);
			break;
		case 9:
			GameCanvas.chatTabScr.addNewChat(T.party, string.Empty, string.Empty, 0, isFocus: true);
			GameCanvas.chatTabScr.Show(gI());
			break;
		case 10:
		{
			MsgDialog msgDialog = new MsgDialog();
			msgDialog.setinfoParty(GameScreen.player.vecAllInfoParty);
			GameCanvas.Start_Current_Dialog(msgDialog);
			break;
		}
		default:
			base.commandPointer(index, subIndex);
			break;
		}
	}

	public override void doMenuTouchPlayer()
	{
		if (vecPlayer.size() == 0)
		{
			return;
		}
		memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
		if (memCur != null && memCur.id != GameScreen.player.ID)
		{
			mVector mVector2 = new mVector();
			InfoMemList mem = InfoMemList.getMem(GameScreen.player.ID, Player.vecParty);
			sbyte b = 1;
			if (mem != null)
			{
				b = mem.isMain;
			}
			mVector2.addElement(cmdInfoPlayer);
			mVector2.addElement(cmdChat);
			mVector2.addElement(cmdAddFriend);
			if (b == 1)
			{
				mVector2.addElement(cmdRemoveMem);
			}
			GameCanvas.menu.startAt(mVector2, 2, memCur.name);
		}
	}

	public override void doMenu()
	{
		if (vecPlayer.size() == 0)
		{
			return;
		}
		memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
		if (memCur == null)
		{
			return;
		}
		mVector mVector2 = new mVector();
		mVector2.addElement(cmdLeave);
		mVector2.addElement(cmdChatParty);
		mVector2.addElement(cmdInfoBuffParty);
		InfoMemList mem = InfoMemList.getMem(GameScreen.player.ID, Player.vecParty);
		sbyte b = 1;
		if (mem != null)
		{
			b = mem.isMain;
		}
		if (b == 1)
		{
			mVector2.addElement(cmdCancelParty);
			if (!GameCanvas.isTouch)
			{
				mVector2.addElement(cmdRemoveMem);
			}
		}
		if (!GameCanvas.isTouch)
		{
			mVector2.addElement(cmdInfoPlayer);
			mVector2.addElement(cmdChat);
			mVector2.addElement(cmdAddFriend);
		}
		GameCanvas.menu.startAt(mVector2, 2, memCur.name);
	}

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		string text = mem.name;
		if (mem.Lv >= 0)
		{
			text = text + " - " + mem.Lv;
		}
		if (i == 0)
		{
			AvMain.Font3dColor(g, text, xpaint, ypaint, 0, 0, 7);
		}
		else
		{
			mFont.tahoma_7b_blue.drawString(g, text, xpaint, ypaint, 0);
		}
		Interface_Game.PaintHPMP(g, 1, mem.hp, mem.maxhp, xpaint, ypaint + 12, 0, 6, 66, -1, isflip: false, 0, isUpdateEff: false, 0);
		mFont.tahoma_7_black.drawString(g, LoadMap.getNameMap(mem.idmap) + " " + T.Area + " " + LoadMap.getShowArea((sbyte)mem.idArea), xpaint + miniItem, ypaint + 19, 0);
	}

	public override void update()
	{
		base.update();
		if (GameCanvas.gameTick % 100 != 0)
		{
			return;
		}
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			for (int j = 0; j < Player.vecParty.size(); j++)
			{
				InfoMemList infoMemList = (InfoMemList)Player.vecParty.elementAt(j);
				if (infoMemList.id == mainObject.ID && mainObject.typeObject == 0)
				{
					infoMemList.updateHP(mainObject.Hp, mainObject.maxHp, mainObject.Lv);
				}
			}
		}
	}
}
