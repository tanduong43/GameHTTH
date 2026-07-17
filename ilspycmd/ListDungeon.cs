public class ListDungeon : PaintListScreen
{
	private iCommand cmdReady;

	private iCommand cmdCancle;

	public static mVector vecDungeon = new mVector("ListDungeon.vecDungeon");

	public static bool isKey = false;

	public static ListDungeon instance;

	public ListDungeon(sbyte type, mVector vec, string name)
		: base(type, vec, name, 180, 180)
	{
		cmdReady = new iCommand(T.ready, 0, this);
		cmdCancle = new iCommand(T.cancel, 1, this);
		vecMenu.removeAllElements();
		if (isKey)
		{
			cmdReady.caption = T.start;
		}
		right = null;
		vecMenu.addElement(cmdReady);
		vecMenu.addElement(cmdCancle);
		setPosCmdNew(0, vecMenu);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			GlobalService.gI().Check_List_Pho_Bang(1);
			break;
		case 1:
			GlobalService.gI().Check_List_Pho_Bang(2);
			GameCanvas.gameScr.Show();
			break;
		default:
			base.commandPointer(index, subIndex);
			break;
		}
	}

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		string text = mem.name;
		if (mem.Lv >= 0)
		{
			text = text + " - " + mem.Lv;
		}
		g.drawImage(AvMain.imgBorderCombo, xpaint + 8, ypaint + 6, 3);
		if (mem.typeOnline == 1)
		{
			AvMain.fraCheck.drawFrame(2, xpaint + 8, ypaint + 6, 0, 3, g);
		}
		if (mem.typeOnline == 2)
		{
			AvMain.fraCheck.drawFrame(1, xpaint + 8, ypaint + 6, 0, 3, g);
		}
		AvMain.Font3dColor(g, text, xpaint + 20, ypaint, 0, 0, 7);
		mFont.tahoma_7_black.drawString(g, LoadMap.getNameMap(mem.idmap), xpaint + miniItem, ypaint + 17, 0);
	}
}
