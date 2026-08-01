public class TabQuest : MainTab
{
	private ListNew list;

	private MainQuest questCur;

	public static iCommand cmdShow;

	private MarqueeText marName = new MarqueeText(0);

	private MarqueeText marInfo = new MarqueeText(0);

	private Scroll scrQuest = new Scroll();

	private int maxPaint;

	public static bool isNewQuest;

	public TabQuest(string name)
	{
		nameTab = name;
		list = new ListNew();
		indexIconTab = 4;
		wItemCur = 32;
		if (isBigScreen)
		{
			wItemCur = 36;
		}
		initCmd();
		marName = new MarqueeText(wCur - (miniItem + 16 + 14));
		marInfo = new MarqueeText(wCur - (miniItem + 16 + 20));
		maxPaint = hCur / wItemCur + 1;
	}

	public override void beginFocus()
	{
		int limX = Player.vecQuest.size() * wItemCur - hCur + miniItem * 3;
		list = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrQuest.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
		if (GameCanvas.isTouchNoOrPC())
		{
			IdSelect = 0;
			if (Player.vecQuest.size() > 0)
			{
				getQuestCur(IdSelect);
				if (questCur != null)
				{
					center = questCur.getCmeTabQuest();
				}
			}
			else
			{
				center = null;
			}
			okCMD = center;
		}
		else
		{
			IdSelect = -1;
		}
	}

	private void initCmd()
	{
		cmdShow = new iCommand(T.show, 0, this);
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 0 && questCur != null)
		{
			showDialogQuest();
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xCurBegin, yCurBegin, wCur, hCur - miniItem, 0, 4);
		g.saveCanvas();
		g.ClipRec(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		setClip(g);
		int num = miniItem * 2;
		int num2 = miniItem + 16;
		int num3 = list.cmx / wItemCur;
		int num4 = num3 + maxPaint;
		num += wItemCur * num3;
		for (int i = num3; i < num4; i++)
		{
			if (i >= 0 && i < Player.vecQuest.size())
			{
				MainQuest mainQuest = (MainQuest)Player.vecQuest.elementAt(i);
				AvMain.fraQuest.drawFrame(mainQuest.statusQuest + 1, num2 + 6, num + wItemCur / 4, 0, 3, g);
				if ((marName.isRun || marInfo.isRun) && IdSelect == i && GameCanvas.currentScreen.setCurTypetab(1) && list.cmx == list.cmtoX)
				{
					g.setClip(num2 + 14, num - 2, marName.maxW, wItemCur);
					if (mainQuest.statusQuest == 0)
					{
						mFont.tahoma_7_blue.drawString(g, T.newquest + ((mainQuest.typeMainSub != 0) ? string.Empty : mainQuest.getMainSub()), num2 + 14 - marName.xplus, num - 2, 0);
					}
					else
					{
						mFont.tahoma_7b_white.drawString(g, mainQuest.name + mainQuest.getMainSub(), num2 + 14 - marName.xplus, num - 2, 0);
					}
					mFont.tahoma_7_white.drawString(g, mainQuest.strNPC_Map, num2 + 20 - marInfo.xplus, num + GameCanvas.hText - 2 - 2, 0);
					setClip(g);
				}
				else
				{
					if (mainQuest.statusQuest == 0)
					{
						mFont.tahoma_7_blue.drawString(g, T.newquest + ((mainQuest.typeMainSub != 0) ? string.Empty : mainQuest.getMainSub()), num2 + 14, num - 2, 0);
					}
					else
					{
						mFont.tahoma_7b_white.drawString(g, mainQuest.name + mainQuest.getMainSub(), num2 + 14, num - 2, 0);
					}
					mFont.tahoma_7_white.drawString(g, mainQuest.strNPC_Map, num2 + 20, num + GameCanvas.hText - 2 - 2, 0);
				}
				if (IdSelect == i && GameCanvas.currentScreen.setCurTypetab(1))
				{
					g.drawRegion(AvMain.imgSelect, 0, 0, 12, 16, 4, num2 - GameCanvas.gameTick / 2 % 3, num + 4, mGraphics.VCENTER | mGraphics.RIGHT);
				}
			}
			num += wItemCur;
		}
		if (GameCanvas.currentScreen.setCurTypetab(1))
		{
			base.paint(g);
			if (list.cmxLim > 0)
			{
				scrQuest.paint(g);
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
	}

	public void setClip(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(xCurBegin - 1, yCurBegin + 1, wCur + 2, hCur - 1 - miniItem - 1);
		g.translate(xCurBegin, yCurBegin);
		g.translate(0, -list.cmx);
	}

	public override void update()
	{
		list.moveCamera();
		scrQuest.setYScrool(list.cmx, list.cmxLim);
		marName.update2();
		marInfo.update();
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(1))
		{
			IdSelect--;
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			IdSelect++;
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (GameCanvas.keyMove(0) || GameCanvas.keyMove(2))
		{
			GameCanvas.currentScreen.setTypeTab(0);
			GameCanvas.ClearkeyMove(0);
			GameCanvas.ClearkeyMove(2);
		}
		if (flag)
		{
			IdSelect = AvMain.resetSelect(IdSelect, Player.vecQuest.size() - 1, isreset: false);
			getQuestCur(IdSelect);
			if (questCur != null)
			{
				center = questCur.getCmeTabQuest();
				okCMD = center;
			}
			if (GameCanvas.isTouchNoOrPC())
			{
				list.setToX((IdSelect + 1) * wItemCur - hCur / 2);
			}
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointerSelect && Player.vecQuest.size() > 0 && GameCanvas.isPoint(xCurBegin, yCurBegin, wCur, hCur))
		{
			int num = (GameCanvas.py - yCurBegin + list.cmx) / wItemCur;
			if (num >= 0 && num < Player.vecQuest.size())
			{
				if (num == IdSelect)
				{
					showDialogQuest();
				}
				else
				{
					IdSelect = num;
				}
				getQuestCur(IdSelect);
				center = questCur.getCmeTabQuest();
			}
			GameCanvas.isPointerSelect = false;
		}
		base.updatePointer();
	}

	public void getQuestCur(int id)
	{
		questCur = (MainQuest)Player.vecQuest.elementAt(id);
		if (questCur != null)
		{
			string str = questCur.name + questCur.getMainSub();
			if (questCur.statusQuest == 0)
			{
				str = T.newquest;
			}
			marName.setdata(str, mFont.tahoma_7b_black);
			marInfo.setdata(questCur.strNPC_Map, mFont.tahoma_7_black);
		}
	}

	public void showDialogQuest()
	{
		MsgDialog msgDialog = new MsgDialog();
		msgDialog.setinfoQuest(questCur, isNew: false);
		GameCanvas.Start_Current_Dialog(msgDialog);
	}
}
