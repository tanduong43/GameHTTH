public class ScreenMaxLevel : MainScreen
{
	private int w;

	private int h;

	private int x;

	private int y;

	private int wItemCur;

	private int idSelect;

	public static ScreenMaxLevel instance;

	public ListNew list;

	private iCommand cmdClose;

	public ScreenMaxLevel()
	{
		w = MotherCanvas.w - 10;
		h = MotherCanvas.h - 10;
		if (w > 180)
		{
			w = 180;
		}
		if (h > 240)
		{
			h = 240;
		}
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2;
		wItemCur = 24;
		if (!GameCanvas.isTouch)
		{
			wItemCur = 20;
		}
		cmdClose = new iCommand(T.close, 0, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x - 14 + w, y + 14, MainTab.fraCloseTab2, string.Empty);
		}
		right = cmdClose;
		backCMD = cmdClose;
	}

	public static ScreenMaxLevel gI()
	{
		if (instance == null)
		{
			instance = new ScreenMaxLevel();
		}
		return instance;
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 0)
		{
			GameCanvas.gameScr.Show();
		}
	}

	public void setData()
	{
		if (h > (Player.vecMaxLevelAttri.size() + 2) * wItemCur + wItemCur / 4)
		{
			h = (Player.vecMaxLevelAttri.size() + 2) * wItemCur + wItemCur / 4;
		}
		list = new ListNew(x, y + GameCanvas.hCommand * 2, w, h - GameCanvas.hCommand * 2, wItemCur, Player.vecMaxLevelAttri.size(), Player.vecMaxLevelAttri.size() * wItemCur - (h - GameCanvas.hCommand * 2), isLim0: true);
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		AvMain.paintThongThao(g, x, y, w, h);
		int num = y + wItemCur;
		int num2 = 25;
		if (!GameCanvas.lowGraphic)
		{
			num2 = AvMain.fraThongThao.frameWidth / 2;
			AvMain.fraBanhLai.drawFrame(0, x + w / 2 - num2 - 12, num, 0, 3, g);
			AvMain.fraBanhLai.drawFrame(1, x + w / 2 + num2 + 12, num, 0, 3, g);
			AvMain.fraThongThao.drawFrame((GameCanvas.language != 0) ? 1 : 0, x + w / 2, num, 0, 3, g);
		}
		else
		{
			mFont.tahoma_7b_black.drawString(g, T.thongthao, x + w / 2, num, 2);
		}
		mFont.tahoma_7_yellow.drawString(g, GameScreen.player.LvThongThao + string.Empty, x + w / 2 - num2 - 12 + 1, num - 5, 2);
		mFont.tahoma_7_white.drawString(g, Player.pointMaxLevelAttri + string.Empty, x + w / 2 + num2 + 12 + 1, num - 5, 2);
		num += wItemCur + wItemCur / 4;
		if (Player.vecMaxLevelAttri != null)
		{
			g.setClip(x, num - wItemCur / 4, w, h - GameCanvas.hCommand * 2 - wItemCur / 4);
			g.saveCanvas();
			g.ClipRec(x, num - wItemCur / 4, w, h - GameCanvas.hCommand * 2 - wItemCur / 4);
			if (list.cmxLim != 0)
			{
				g.translate(0, -list.cmx);
			}
			int num3 = 0;
			int num4 = 0;
			num3 = list.cmx / wItemCur - 2;
			if (num3 < 0)
			{
				num3 = 0;
			}
			num4 = h / wItemCur + 1 + num3;
			if (num4 >= Player.vecMaxLevelAttri.size())
			{
				num4 = Player.vecMaxLevelAttri.size();
			}
			for (int i = num3; i < num4; i++)
			{
				MaxLevelAttribute maxLevelAttribute = (MaxLevelAttribute)Player.vecMaxLevelAttri.elementAt(i);
				mFont.tahoma_7b_black.drawString(g, MainItem.mNameAttributes[maxLevelAttribute.Id].name + ": ", x + 80, num - 4, 1);
				bool isFocus = false;
				if (i == idSelect && GameCanvas.isTouchNoOrPC())
				{
					isFocus = true;
				}
				Interface_Game.paintHP_Thong_Thao(g, x + 80, num - 4, w - 90, maxLevelAttribute.value, maxLevelAttribute.maxValue, isFocus);
				num += wItemCur;
			}
			mGraphics.resetTransAndroid(g);
			g.restoreCanvas();
		}
		GameCanvas.resetTrans(g);
		if (!GameCanvas.lowGraphic && AvMain.mimgBgB != null)
		{
			g.drawImage(AvMain.mimgBgB[8], x + w - 36, y + h - 36, 0);
		}
		if (GameCanvas.isTouch)
		{
			cmdClose.paint(g, cmdClose.xCmd, cmdClose.yCmd);
		}
		else
		{
			paintCmd(g);
		}
	}

	public override void update()
	{
		if (list.cmxLim > 0)
		{
			list.moveCamera();
		}
		if (lastScreen != null)
		{
			lastScreen.update();
		}
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(1))
		{
			idSelect--;
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			idSelect++;
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		else if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			setAction(idSelect);
		}
		if (flag)
		{
			idSelect = AvMain.resetSelect(idSelect, Player.vecMaxLevelAttri.size() - 1, isreset: false);
			if (list.cmxLim > 0)
			{
				list.setToX(idSelect * wItemCur - list.maxH / 2);
			}
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		if (list.cmxLim > 0)
		{
			list.update_Pos_UP_DOWN();
		}
		base.updatePointer();
		if (!GameCanvas.isPointerSelect)
		{
			return;
		}
		int num = y + wItemCur * 2 - wItemCur / 2;
		if (!GameCanvas.isPoint(x, y, w, h))
		{
			return;
		}
		int num2 = (GameCanvas.py - (num - list.cmx)) / wItemCur;
		if (num2 >= 0 && num2 < Player.vecMaxLevelAttri.size())
		{
			idSelect = num2;
			if (Player.pointMaxLevelAttri > 0)
			{
				setAction(idSelect);
			}
		}
		GameCanvas.isPointerSelect = false;
	}

	private void setAction(int ac)
	{
		if (ac >= 0 && ac < Player.vecMaxLevelAttri.size())
		{
			MaxLevelAttribute maxLevelAttribute = (MaxLevelAttribute)Player.vecMaxLevelAttri.elementAt(ac);
			GlobalService.gI().One_Point_Max_Level(0, (short)maxLevelAttribute.Id);
		}
	}
}
