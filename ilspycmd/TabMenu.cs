public class TabMenu : MainTab
{
	private Menu menu = new Menu();

	public TabMenu(string name, mVector vecmenu)
	{
		nameTab = name;
		wItemCur = GameCanvas.hCommand;
		if (GameCanvas.isTouch)
		{
			wItemCur = 32;
		}
		initCmd();
		menu.menuX = xCurBegin;
		menu.wUni = wCur;
		menu.menuH = hCur - miniItem * 3;
		menu.menuW = wItemCur;
		menu.menuY = yCurBegin;
		menu.menuTemY = menu.menuY;
		menu.startTabMenu(vecmenu);
		if (GameCanvas.isTouch)
		{
			menu.menuSelectedItem = -1;
		}
		else
		{
			menu.menuSelectedItem = 0;
		}
		indexIconTab = 6;
	}

	public override void beginFocus()
	{
		if (GameCanvas.isTouch)
		{
			menu.menuSelectedItem = -1;
		}
		else
		{
			menu.menuSelectedItem = 0;
		}
		menu.cmtoX = 0;
		menu.cmx = 0;
		menu.startTabMenu(GameCanvas.gameScr.getMenu());
	}

	private void initCmd()
	{
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		g.saveCanvas();
		g.ClipRec(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		g.translate(0, -menu.cmx);
		for (int i = 0; i < menu.menuItems.size() - 1; i++)
		{
			paintLine(g, i);
		}
		int num = menu.cmx / menu.menuW - 1;
		if (num < 0)
		{
			num = 0;
		}
		int num2 = num + menu.sizeMenu + 2;
		if (num2 > menu.menuItems.size())
		{
			num2 = menu.menuItems.size();
			num = num2 - menu.sizeMenu - 2;
			if (num < 0)
			{
				num = 0;
			}
		}
		if (GameCanvas.currentScreen.setCurTypetab(1) && menu.menuSelectedItem > -1 && (!GameCanvas.isTouch || menu.timeShowSelect > 0))
		{
			paintSelect(g, menu.menuX + 10, menu.menuY + 7 + menu.menuSelectedItem * menu.menuW, menu.wUni - 20, menu.menuW - 8);
		}
		for (int j = num; j < num2; j++)
		{
			iCommand iCommand2 = (iCommand)menu.menuItems.elementAt(j);
			iCommand2.paintCaptionImageMenu(g, menu.menuX + menu.wUni / 2, menu.menuY + 4 + menu.menuW / 4 + j * menu.menuW, 2, is3D: false);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		base.paint(g);
	}

	public void paintLine(mGraphics g, int i)
	{
		g.setColor(AvMain.colorMenu[4]);
		g.fillRect(menu.menuX + 8, menu.menuY + 3 + menu.menuW + i * menu.menuW - 1, menu.wUni - 16, 2);
		g.fillRect(menu.menuX + 8 + 1, menu.menuY + 3 + menu.menuW + i * menu.menuW - 2, menu.wUni - 16 - 2, 4);
	}

	public override void update()
	{
		menu.updateMenu();
		base.update();
	}

	public override void updatekey()
	{
		if (GameCanvas.keyMove(0) || GameCanvas.keyMove(2))
		{
			GameCanvas.currentScreen.setTypeTab(0);
			GameCanvas.ClearkeyMove(0);
			GameCanvas.ClearkeyMove(2);
		}
		menu.updateMenuKey();
		base.updatekey();
	}

	public override void updatePointer()
	{
		base.updatePointer();
	}
}
