public class FriendList : PaintListScreen
{
	public static bool isGetListFriend;

	private iCommand cmdRemoveFriend;

	private iCommand cmdRefresh;

	public static FriendList instance;

	public FriendList(sbyte type, mVector vec)
		: base(type, vec, T.friendList, 200, 180)
	{
		cmdRemoveFriend = new iCommand(T.delFriend, 10, this);
		cmdRefresh = new iCommand(T.update, 11, this);
		vecMenu.removeAllElements();
		if (!GameCanvas.isTouch)
		{
			vecMenu.addElement(cmdMenu);
		}
		else
		{
			vecMenu.addElement(cmdRefresh);
		}
		if (GameCanvas.isTouch)
		{
			right = cmdClose;
		}
		else
		{
			vecMenu.addElement(cmdClose);
		}
		backCMD = cmdClose;
		menuCMD = cmdMenu;
		okCMD = cmdMenu;
		idCommand = 0;
		setPosCmdNew(0, vecMenu);
	}

	public static FriendList gI()
	{
		return (instance != null) ? instance : (instance = new FriendList(-2, Player.vecFriendList));
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 10:
			if (memCur != null)
			{
				GlobalService.gI().Friend(1, memCur.id);
			}
			break;
		case 11:
			GlobalService.gI().Friend(2, 0);
			break;
		default:
			base.commandPointer(index, subIndex);
			break;
		}
	}

	public override void doMenuTouchPlayer()
	{
		if (vecPlayer.size() != 0 && idSelect >= 0 && idSelect < vecPlayer.size())
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			if (memCur != null && memCur.id != GameScreen.player.ID)
			{
				mVector mVector2 = new mVector();
				mVector2.addElement(cmdInfoPlayer);
				mVector2.addElement(cmdChat);
				mVector2.addElement(cmdRemoveFriend);
				mVector2.addElement(cmdMove);
				GameCanvas.menu.startAt(mVector2, 2, memCur.name);
			}
		}
	}

	public override void doMenu()
	{
		mVector mVector2 = new mVector();
		string name = T.menu;
		if (idSelect >= 0 && idSelect < vecPlayer.size())
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			if (memCur != null)
			{
				mVector2.addElement(cmdInfoPlayer);
				mVector2.addElement(cmdChat);
				mVector2.addElement(cmdRemoveFriend);
				mVector2.addElement(cmdMove);
				name = memCur.name;
			}
		}
		mVector2.addElement(cmdRefresh);
		GameCanvas.menu.startAt(mVector2, 2, name);
	}

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		if (mem != null)
		{
			string text = mem.name;
			if (mem.Lv >= 0)
			{
				string text2 = text;
				text = text2 + " - " + T.Lv + " " + mem.Lv;
			}
			AvMain.fraStatusOnline.drawFrame(mem.typeOnline, xpaint + 25, ypaint + 5, 0, 3, g);
			mFont.tahoma_7b_black.drawString(g, text, xpaint + 30, ypaint, 0);
			mFont.tahoma_7_black.drawString(g, mem.info, xpaint + 25, ypaint + GameCanvas.hText, 0);
			MainObject.paintHeadEveryWhere(g, mem.head, mem.hair, mem.hat, xpaint + 10, ypaint + hItem / 2 + 32, 0);
		}
	}
}
