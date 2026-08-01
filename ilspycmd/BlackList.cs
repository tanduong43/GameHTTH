public class BlackList : PaintListScreen
{
	private iCommand cmdUpdateBlackList;

	private sbyte page;

	public static BlackList instance;

	public BlackList(sbyte type, mVector vec, string name, sbyte page)
		: base(type, vec, name, 200, 180)
	{
		this.page = page;
		cmdUpdateBlackList = new iCommand(T.update, 14, this);
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 14)
		{
			GameCanvas.gameScr.cmdBlackList.perform();
			isLoad = true;
		}
		else
		{
			base.commandPointer(index, subIndex);
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
			mVector2.addElement(cmdInfoPlayer);
			mVector2.addElement(cmdMove);
			if (mVector2.size() > 0)
			{
				GameCanvas.menu.startAt(mVector2, 2, memCur.name);
			}
		}
	}

	public override void doMenu()
	{
		mVector mVector2 = new mVector();
		string name = T.menu;
		if (vecPlayer.size() > 0)
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			if (memCur != null && !GameCanvas.isTouch)
			{
				mVector2.addElement(cmdInfoPlayer);
				name = memCur.name;
			}
		}
		mVector2.addElement(cmdUpdateBlackList);
		if (!GameCanvas.isTouch)
		{
			mVector2.addElement(cmdMove);
		}
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
