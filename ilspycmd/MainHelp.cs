public class MainHelp : AvMain
{
	private int x;

	private int y;

	private int w;

	private int h;

	private int type;

	private int typeSub;

	private int xbegin;

	private int ybegin;

	private int archor;

	private string str;

	private string[] strShow;

	public bool isBreak;

	public bool isRemove;

	public bool isInMap;

	public const sbyte HELLO_1 = 0;

	public const sbyte MP_HP = 1;

	public const sbyte HOT_KEY = 2;

	public const sbyte MOVE = 3;

	public const sbyte NEXT_FOCUS = 4;

	public const sbyte CHANGE_HOT_KEY = 5;

	public const sbyte CHAT = 6;

	public const sbyte EVENT_M = 7;

	public const sbyte GET_ITEM = 8;

	public const sbyte INVENTORY = 9;

	public const sbyte NEXT_INVENTORY = 10;

	public const sbyte EQUIP = 11;

	public const sbyte INFO = 12;

	public const sbyte LV_UP = 13;

	public const sbyte QUEST = 14;

	public const sbyte SKILL = 15;

	public const sbyte SETTING = 16;

	public const sbyte END = 17;

	public const sbyte BEGINNEW = 18;

	public const sbyte BUY_SELL = 19;

	public const sbyte UPGRADE = 20;

	public const sbyte BOSS = 21;

	private iCommand cmd;

	private mFont fontPaint;

	private Point_Focus p;

	private RunWord runText;

	public MainHelp(int type, int typeSub)
	{
		runText = new RunWord();
		this.type = type;
		this.typeSub = typeSub;
		isBreak = true;
		w = 100;
		fontPaint = mFont.tahoma_7_white;
		switch (type)
		{
		case 0:
			cmd = new iCommand(T.close, 2, this);
			GameCanvas.Start_Normal_DiaLog(T.mHelp[0], cmd, isCmdClose: false);
			isRemove = true;
			break;
		case 18:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(new iCommand(T.textHelp, 1, 1, this));
			mVector2.addElement(new iCommand(T.boqua, 1, 0, this));
			GameCanvas.Start_Normal_DiaLog(T.mHelp[1], mVector2, isCmdClose: false);
			isRemove = true;
			break;
		}
		case 1:
			str = T.mHelp[2];
			x = 100;
			y = 5;
			archor = 0;
			createPoint(x, y + 20, 0);
			GameScreen.indexHelp = type;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 2:
			setHotKey();
			break;
		case 3:
			setMove();
			break;
		case 4:
			if (GameCanvas.isTouch)
			{
				if (Interface_Game.typeTouch == 1)
				{
					str = T.mHelp[8];
					x = MotherCanvas.hw;
					y = MotherCanvas.h - GameCanvas.hCommand * 2;
					archor = 33;
				}
				else
				{
					str = T.mHelp[10];
					x = Interface_Game.mPosOther[3][0] - 15;
					y = Interface_Game.mPosOther[3][1];
					archor = 1;
					createPoint(x, y + 20, 1);
				}
			}
			else
			{
				str = T.mHelp[9];
				x = MotherCanvas.hw;
				y = MotherCanvas.h - GameCanvas.hCommand * 2;
				archor = 33;
			}
			GameScreen.indexHelp = type;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 5:
			if (GameCanvas.isTouch)
			{
				str = T.mHelp[18];
				x = Interface_Game.mPosOther[2][0] - 15;
				y = Interface_Game.mPosOther[2][1] + 20;
				archor = -1;
				createPoint(x, y - 5, 1);
			}
			else
			{
				str = T.mHelp[17];
				x = MotherCanvas.hw;
				y = MotherCanvas.h - GameCanvas.hCommand * 2;
				archor = 33;
			}
			GameScreen.indexHelp = type;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 6:
			if (GameCanvas.isTouch)
			{
				str = T.mHelp[13];
				x = Interface_Game.mPosOther[1][0] + 35;
				y = Interface_Game.mPosOther[1][1];
				archor = 0;
				createPoint(x, y + 15, 0);
			}
			else
			{
				str = T.mHelp[12];
				x = MotherCanvas.hw;
				y = MotherCanvas.h - GameCanvas.hCommand * 2;
				archor = 33;
			}
			GameScreen.indexHelp = type;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 7:
			if (GameCanvas.isTouch)
			{
				str = T.mHelp[15];
				x = Interface_Game.xNumMess + 35;
				y = Interface_Game.yNumMess;
				archor = 0;
				createPoint(x, y + 15, 0);
			}
			else
			{
				str = T.mHelp[14];
				x = Interface_Game.xNumMess + 35;
				y = Interface_Game.yNumMess;
				archor = 0;
				createPoint(x, y + 15, 0);
			}
			GameScreen.indexHelp = type;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 8:
		{
			if (GameCanvas.isTouch)
			{
				str = T.mHelp[20];
			}
			else
			{
				str = T.mHelp[19];
			}
			for (int j = 0; j < GameScreen.vecPlayers.size(); j++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(j);
				if (mainObject.typeObject == 3 || mainObject.typeObject == 4 || mainObject.typeObject == 7)
				{
					x = mainObject.x;
					y = mainObject.y - 40;
					archor = 33;
					createPoint(x, y, 3);
					isInMap = true;
					break;
				}
			}
			GameScreen.indexHelp = 9;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		}
		case 9:
			if (GameCanvas.isTouch)
			{
				str = T.mHelp[22];
				x = 100;
				y = 5;
				archor = 0;
				createPoint(x, y + 20, 0);
			}
			else
			{
				str = T.mHelp[21];
				x = 20;
				y = MotherCanvas.h - 30;
				archor = 2;
				createPoint(x, y, 3);
			}
			GameScreen.indexHelp = 10;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 10:
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			switch (typeSub)
			{
			case 0:
				w = 140;
				str = T.mHelp[23];
				GameScreen.indexHelp = 10;
				GameCanvas.saveRms.SaveIndexHelp();
				createPoint(MainTab.xTab + 22 + 20, MainTab.yTab + 36, 0);
				break;
			case 1:
			{
				str = T.mHelp[24];
				int num3 = MainTab.xTab + MainTab.wTab / 2 - MainTab.wItem * MainTabShop.maxNumItemW / 2 + 10;
				int num4 = MainTab.yTab + 32;
				for (int i = 0; i < Player.vecInventory.size(); i++)
				{
					MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
					if (mainItem.typeObject == 4)
					{
						createPoint(num3 + i % MainTabShop.maxNumItemW * MainTab.wItem + MainTab.wItem / 2 + 14, num4 + i / MainTabShop.maxNumItemW * MainTab.wItem + MainTab.wItem / 2, 10);
						break;
					}
				}
				break;
			}
			}
			break;
		case 11:
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			w = 140;
			switch (typeSub)
			{
			case 0:
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
				{
					GameCanvas.tabAllScr.idSelect = 1;
					GameCanvas.tabAllScr.setTabSelect();
					GameCanvas.tabAllScr.tabCurrent.beginFocus();
				}
				str = T.mHelp[25];
				createPoint(MainTab.xTab + 22 + 20, MainTab.yTab + 36 + MainTab.hItemTab, 0);
				break;
			case 1:
			{
				str = T.mHelp[26];
				int num = TabEquip.mposEquip[1][0] + 20;
				int num2 = TabEquip.mposEquip[1][1];
				createPoint(num + 12, num2 + 12, 10);
				break;
			}
			case 2:
				str = T.mHelp[27];
				break;
			}
			break;
		case 14:
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			switch (typeSub)
			{
			case 0:
				w = 140;
				str = T.mHelp[28];
				GameScreen.indexHelp = 14;
				GameCanvas.saveRms.SaveIndexHelp();
				break;
			case 1:
				str = T.mHelp[29];
				x = 100;
				y = 5;
				archor = 0;
				createPoint(x, y + 20, 0);
				break;
			case 2:
				str = T.mHelp[30];
				createPoint(MainTab.xTab + 22 + 20, MainTab.yTab + 36 + MainTab.hItemTab * 4, 0);
				break;
			case 3:
				str = T.mHelp[31];
				break;
			}
			break;
		case 13:
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			w = 140;
			str = T.mHelp[32];
			break;
		case 15:
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			switch (typeSub)
			{
			case 0:
				w = 140;
				str = T.mHelp[33];
				createPoint(MainTab.xTab + 22 + 20, MainTab.yTab + 36 + MainTab.hItemTab * 3, 0);
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
				{
					GameCanvas.tabAllScr.idSelect = 3;
					GameCanvas.tabAllScr.setTabSelect();
					GameCanvas.tabAllScr.tabCurrent.beginFocus();
				}
				break;
			case 1:
				w = 140;
				str = T.mHelp[34];
				createPoint(MainTab.xTab + MainTab.wTab / 2 - (MainTab.wTab - 70) / 2 + 10 + 40 + 20, MainTab.yTab + 32 + 24, 0);
				break;
			}
			break;
		case 12:
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			switch (typeSub)
			{
			case 0:
				w = 140;
				str = T.mHelp[36];
				createPoint(MainTab.xTab + 22 + 20, MainTab.yTab + 36 + MainTab.hItemTab * 2, 0);
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
				{
					GameCanvas.tabAllScr.idSelect = 2;
					GameCanvas.tabAllScr.setTabSelect();
					GameCanvas.tabAllScr.tabCurrent.beginFocus();
				}
				break;
			case 1:
				w = 140;
				str = T.mHelp[37];
				createPoint(MainTab.xTab + MainTab.wTab / 2 - (MainTab.wTab - 70) / 2 + 10 + (MainTab.wTab - 22) / 4, MainTab.yTab + 20, 0);
				break;
			case 2:
				w = 140;
				str = T.mHelp[38];
				createPoint(MainTab.xTab + MainTab.wTab / 2 - (MainTab.wTab - 70) / 2 + 10 + (MainTab.wTab - 22) / 4 * 3, MainTab.yTab + 20, 0);
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
				{
					GameCanvas.tabAllScr.tabCurrent.updateChangeTabInfo();
				}
				break;
			case 3:
				w = 140;
				str = T.mHelp[39];
				break;
			}
			break;
		case 16:
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			GameScreen.indexHelp = 17;
			GameCanvas.saveRms.SaveIndexHelp();
			switch (typeSub)
			{
			case 0:
				if (!GameCanvas.isTouch)
				{
					w = 140;
					str = T.mHelp[40];
					createPoint(MainTab.xTab + 22 + 20, MainTab.yTab + 36 + MainTab.hItemTab * 5, 0);
					if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
					{
						GameCanvas.tabAllScr.idSelect = 5;
						GameCanvas.tabAllScr.setTabSelect();
						GameCanvas.tabAllScr.tabCurrent.beginFocus();
					}
				}
				else
				{
					isRemove = true;
				}
				break;
			case 1:
				if (GameCanvas.isTouch)
				{
					w = 140;
					str = T.mHelp[41];
					archor = 0;
					x = Interface_Game.mPosOther[4][0] + 35;
					y = Interface_Game.mPosOther[4][1] + 35;
					createPoint(x, y, 0);
				}
				else
				{
					isRemove = true;
				}
				break;
			}
			break;
		case 17:
			isRemove = true;
			break;
		case 19:
			w = 140;
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			str = T.mHelp[42];
			break;
		case 20:
			w = 140;
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			str = T.mHelp[43];
			break;
		case 21:
			w = 140;
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
			str = T.mHelp[44];
			break;
		}
		if (str != null)
		{
			strShow = fontPaint.splitFontArray(str, w);
			h = strShow.Length * GameCanvas.hText;
		}
		if (archor == 0)
		{
			xbegin = x;
			ybegin = y;
		}
		else if (archor == 1)
		{
			xbegin = x - w;
			ybegin = y;
		}
		else if (archor == 33)
		{
			xbegin = x - w / 2;
			ybegin = y - h;
		}
		else if (archor == 3)
		{
			xbegin = x - w / 2;
			ybegin = y - h / 2;
		}
		else if (archor == -1)
		{
			xbegin = x - w;
			ybegin = y - h;
		}
		else if (archor == 2)
		{
			xbegin = x;
			ybegin = y - h;
		}
		if (str != null)
		{
			runText.startDialogChain(str, 0, xbegin + 3, ybegin + 3, w, fontPaint);
		}
		GameCanvas.clearAll();
	}

	private void setMove()
	{
		if (GameCanvas.isTouch)
		{
			if (Interface_Game.typeTouch == 1)
			{
				str = T.mHelp[8];
				x = MotherCanvas.hw;
				y = MotherCanvas.h - GameCanvas.hCommand * 2;
				archor = 33;
			}
			else
			{
				str = T.mHelp[7];
				x = Interface_Game.xPointMove + 15;
				y = Interface_Game.yPointMove - 45;
				archor = 0;
				strShow = fontPaint.splitFontArray(str, w);
				h = strShow.Length * GameCanvas.hText;
				createPoint(x, y + h, 3);
			}
		}
		else
		{
			str = T.mHelp[6];
			x = MotherCanvas.hw;
			y = MotherCanvas.h - GameCanvas.hCommand * 2;
			archor = 33;
		}
		GameScreen.indexHelp = type;
		GameCanvas.saveRms.SaveIndexHelp();
	}

	private void setHotKey()
	{
		if (typeSub == 0)
		{
			str = T.mHelp[3];
		}
		else if (typeSub == 1)
		{
			if (GameCanvas.isTouch)
			{
				str = T.mHelp[5];
			}
			else
			{
				str = T.mHelp[4];
			}
		}
		strShow = fontPaint.splitFontArray(str, w);
		h = strShow.Length * GameCanvas.hText;
		if (GameCanvas.isTouch)
		{
			if (Interface_Game.typeTouch == 1)
			{
				x = Interface_Game.mPosKillCur[2][0];
				y = Interface_Game.mPosKillCur[2][1] - 30;
				archor = 33;
				createPoint(x, y, 5);
			}
			else
			{
				x = Interface_Game.mPosKillCur[1][0];
				y = Interface_Game.mPosKillCur[1][1] - 30;
				archor = 1;
				createPoint(x, y + h, 5);
			}
		}
		else
		{
			x = Interface_Game.mPosKillCur[2][0];
			y = Interface_Game.mPosKillCur[2][1] - 30;
			archor = 33;
			createPoint(x, y, 5);
		}
		GameScreen.indexHelp = type;
		GameCanvas.saveRms.SaveIndexHelp();
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(new iCommand(T.textHelp, 1, 1, this));
			mVector2.addElement(new iCommand(T.boqua, 1, 0, this));
			GameCanvas.Start_Normal_DiaLog(T.mHelp[1], mVector2, isCmdClose: false);
			break;
		}
		case 1:
			GameCanvas.end_Dialog();
			if (subIndex == 1)
			{
				GameScreen.indexHelp = 2;
				GameScreen.addHelp(1, 0);
			}
			else
			{
				GameScreen.indexHelp = -1;
			}
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 2:
			GameCanvas.end_Dialog();
			GameScreen.indexHelp = 18;
			GameCanvas.saveRms.SaveIndexHelp();
			GlobalService.gI().BeginGame();
			break;
		}
		base.commandPointer(index, subIndex);
	}

	public override void paint(mGraphics g)
	{
		if (strShow != null)
		{
			AvMain.paintRect(g, xbegin, ybegin, w + 3, h, 1, 4);
			if (runText != null)
			{
				runText.paintText(g, 0);
			}
		}
		if (p != null)
		{
			if (AvMain.imgHand == null)
			{
				AvMain.imgHand = mImage.createImage("/interface/hand.png");
			}
			else
			{
				g.drawRegion(AvMain.imgHand, 0, 0, 14, 16, p.dis, p.x, p.y, 3);
			}
		}
	}

	public override void update()
	{
		if (GameCanvas.currentDialog == null && GameCanvas.subDialog == null && !GameCanvas.menu.isShowMenu)
		{
			updatekey();
			updatePointer();
		}
		if (runText != null)
		{
			runText.updateDlg();
		}
		if (p == null)
		{
			return;
		}
		p.f++;
		if (p.f < 10)
		{
			p.x += p.vx;
			p.y += p.vy;
			if (p.vx > 0)
			{
				p.vx--;
			}
			if (p.vx < 0)
			{
				p.vx++;
			}
			if (p.vy > 0)
			{
				p.vy--;
			}
			if (p.vy < 0)
			{
				p.vy++;
			}
		}
		else if (p.f == 15)
		{
			p.x = p.toX;
			p.y = p.toY;
			p.vx = p.vxMax;
			p.vy = p.vyMax;
			p.f = 0;
		}
	}

	public override void updatekey()
	{
		switch (type)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 19:
		case 20:
		case 21:
			if (GameCanvas.AnyKey())
			{
				setActionHelp();
			}
			break;
		case 8:
		case 17:
		case 18:
			break;
		}
	}

	public override void updatePointer()
	{
		switch (type)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 19:
		case 20:
		case 21:
			if (GameCanvas.isPointerSelect)
			{
				setActionHelp();
			}
			break;
		case 8:
		case 17:
		case 18:
			break;
		}
	}

	public void setActionHelp()
	{
		if (runText != null && !runText.nextDlgStep())
		{
			GameCanvas.clearAll();
			return;
		}
		switch (type)
		{
		case 1:
			GameScreen.addHelp(3, 0);
			isRemove = true;
			break;
		case 2:
			if (typeSub == 0)
			{
				GameScreen.addHelp(2, 1);
				isRemove = true;
			}
			else if (typeSub == 1)
			{
				isRemove = true;
			}
			break;
		case 3:
			GameScreen.addHelp(2, 0);
			isRemove = true;
			break;
		case 4:
			GameScreen.addHelp(5, 0);
			isRemove = true;
			break;
		case 5:
			GameScreen.addHelp(6, 0);
			isRemove = true;
			break;
		case 6:
			GameScreen.addHelp(7, 0);
			GameCanvas.chatTabScr.addNewChat(T.haitac, string.Empty, T.chucban, 0, isFocus: false);
			isRemove = true;
			break;
		case 7:
			isRemove = true;
			GameScreen.indexHelp = 8;
			GameCanvas.saveRms.SaveIndexHelp();
			break;
		case 9:
			isRemove = true;
			break;
		case 10:
			if (typeSub == 0)
			{
				GameScreen.addHelp(10, 1);
				isRemove = true;
			}
			else if (typeSub == 1)
			{
				GameScreen.addHelp(11, 0);
				isRemove = true;
			}
			break;
		case 11:
			if (typeSub == 0)
			{
				GameScreen.addHelp(11, 1);
				isRemove = true;
			}
			else if (typeSub == 1)
			{
				GameScreen.addHelp(11, 2);
				isRemove = true;
			}
			else if (typeSub == 2)
			{
				GameScreen.indexHelp = 14;
				GameCanvas.saveRms.SaveIndexHelp();
				isRemove = true;
			}
			break;
		case 14:
			if (typeSub == 0)
			{
				isRemove = true;
			}
			else if (typeSub == 1)
			{
				isRemove = true;
			}
			else if (typeSub == 2)
			{
				GameScreen.addHelp(14, 3);
				isRemove = true;
			}
			else if (typeSub == 3)
			{
				GameScreen.indexHelp = 13;
				GameCanvas.saveRms.SaveIndexHelp();
				isRemove = true;
			}
			break;
		case 13:
			if (typeSub == 0)
			{
				GameScreen.indexHelp = 15;
				GameCanvas.saveRms.SaveIndexHelp();
				isRemove = true;
			}
			break;
		case 15:
			if (typeSub == 0)
			{
				GameScreen.addHelp(15, 1);
				isRemove = true;
			}
			else if (typeSub == 1)
			{
				GameScreen.addHelp(12, 0);
				isRemove = true;
			}
			break;
		case 12:
			if (typeSub == 0)
			{
				GameScreen.addHelp(12, 1);
				isRemove = true;
			}
			else if (typeSub == 1)
			{
				GameScreen.addHelp(12, 2);
				isRemove = true;
			}
			else if (typeSub == 2)
			{
				GameScreen.addHelp(12, 3);
				isRemove = true;
			}
			else if (typeSub == 3)
			{
				GameScreen.indexHelp = 16;
				GameCanvas.saveRms.SaveIndexHelp();
				isRemove = true;
			}
			break;
		case 16:
			if (typeSub == 0)
			{
				isRemove = true;
			}
			else if (typeSub == 1)
			{
				isRemove = true;
			}
			break;
		case 19:
		case 20:
		case 21:
			isRemove = true;
			break;
		case 8:
		case 17:
		case 18:
			break;
		}
	}

	public void createPoint(int x, int y, int ar)
	{
		p = new Point_Focus(x, y);
		p.toX = p.x;
		p.toY = p.y;
		switch (ar)
		{
		case 0:
			p.vx = -5;
			p.vy = 0;
			break;
		case 1:
			p.vx = 5;
			p.vy = 0;
			p.dis = 2;
			break;
		case 2:
			p.vx = 0;
			p.vy = -5;
			p.dis = 5;
			break;
		case 3:
		case 5:
			p.vx = 0;
			p.vy = 5;
			if (ar == 3)
			{
				p.dis = 6;
			}
			else
			{
				p.dis = 7;
			}
			break;
		case 10:
			p.vx = -3;
			p.vy = 0;
			break;
		}
		p.vxMax = p.vx;
		p.vyMax = p.vy;
	}

	public static bool checkIndexHelp(int typeCheck)
	{
		if (GameScreen.vecHelp == null)
		{
			return false;
		}
		for (int i = 0; i < GameScreen.vecHelp.size(); i++)
		{
			MainHelp mainHelp = (MainHelp)GameScreen.vecHelp.elementAt(i);
			if (mainHelp.type == typeCheck)
			{
				return true;
			}
		}
		return false;
	}

	public static void checkRemoveIndexHelp(int typeCheck)
	{
		if (GameScreen.vecHelp == null)
		{
			return;
		}
		for (int i = 0; i < GameScreen.vecHelp.size(); i++)
		{
			MainHelp mainHelp = (MainHelp)GameScreen.vecHelp.elementAt(i);
			if (mainHelp.type == typeCheck)
			{
				GameScreen.vecHelp.removeElement(mainHelp);
			}
		}
	}

	public static void setNextHelp(bool isHanhTrang)
	{
		if (GameScreen.indexHelp < 0)
		{
			return;
		}
		switch (GameScreen.indexHelp)
		{
		case 2:
			GameScreen.addHelp(4, 0);
			checkRemoveIndexHelp(2);
			break;
		case 8:
			GameScreen.addHelp(8, 0);
			break;
		case 9:
			GameScreen.addHelp(9, 0);
			break;
		case 10:
			if (isHanhTrang)
			{
				GameScreen.addHelp(10, 0);
			}
			break;
		case 14:
			if (isHanhTrang && Player.vecQuest.size() > 0)
			{
				GameScreen.addHelp(14, 2);
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
				{
					GameCanvas.tabAllScr.idSelect = 4;
					GameCanvas.tabAllScr.setTabSelect();
					GameCanvas.tabAllScr.tabCurrent.beginFocus();
				}
			}
			break;
		case 15:
			if (isHanhTrang)
			{
				GameScreen.addHelp(15, 0);
			}
			break;
		case 16:
			if (isHanhTrang && !GameCanvas.isTouch)
			{
				GameScreen.addHelp(16, 0);
			}
			break;
		default:
			GameScreen.addHelp(GameScreen.indexHelp, 0);
			break;
		}
	}
}
