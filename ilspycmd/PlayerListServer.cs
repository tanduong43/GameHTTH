public class PlayerListServer : PaintListScreen
{
	private iCommand cmdNext;

	private iCommand cmdPre;

	private iCommand cmdUpdateBlackList;

	private iCommand cmdPosFightClan;

	private iCommand cmdSendListMem;

	private sbyte page;

	public static PlayerListServer instance;

	private int lastTick;

	private int framepaint;

	public PlayerListServer(sbyte type, mVector vec, string name, sbyte page)
		: base(type, vec, name, 200, 180)
	{
		this.page = page;
		cmdNext = new iCommand(T.nextpage, 12, this);
		cmdPre = new iCommand(T.prepage, 13, this);
		cmdUpdateBlackList = new iCommand(T.update, 14, this);
		cmdPosFightClan = new iCommand(T.sapxep, 15, this);
		cmdSendListMem = new iCommand(T.xong, 17, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xSub + 20 + wSub - 34, ySub - 10, MainTab.fraCloseTab, string.Empty);
			right = cmdClose;
		}
		if (type == 15)
		{
			cmdPosFightClan = AvMain.setPosCMD(cmdPosFightClan, 0);
			cmdSendListMem = AvMain.setPosCMD(cmdSendListMem, 1);
			center = cmdPosFightClan;
			left = cmdSendListMem;
			vecMenu.removeAllElements();
			vecMenu.addElement(cmdClose);
			if (!GameCanvas.isTouch)
			{
				vecMenu.addElement(cmdPosFightClan);
			}
			vecMenu.addElement(cmdSendListMem);
		}
		else
		{
			setPosCmdNew(GameCanvas.hCommand + 12, vecMenu);
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 12:
			GlobalService.gI().ListFromServer(3, type, (sbyte)(page + 1));
			return;
		case 13:
			GlobalService.gI().ListFromServer(3, type, (sbyte)(page - 1));
			return;
		case 14:
			GameCanvas.gameScr.cmdBlackList.perform();
			isLoad = true;
			return;
		case 15:
			getMenuSetPos();
			break;
		case 16:
		{
			if (memCur == null)
			{
				break;
			}
			int rank = memCur.rank;
			for (int i = 0; i < vecPlayer.size(); i++)
			{
				InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(i);
				if (infoMemList.rank == subIndex)
				{
					infoMemList.rank = rank;
				}
			}
			memCur.rank = subIndex;
			break;
		}
		case 17:
			CRes.quickSortMemList(vecPlayer);
			GlobalService.gI().Clan_Fight_List_Mem(6, vecPlayer);
			cmdClose.perform();
			break;
		}
		base.commandPointer(index, subIndex);
	}

	private void getMenuSetPos()
	{
		if (memCur != null)
		{
			mVector mVector2 = new mVector();
			int num = vecPlayer.size();
			for (int i = 0; i < num; i++)
			{
				iCommand o = new iCommand(T.viTri + (i + 1), 16, i, this);
				mVector2.addElement(o);
			}
			GameCanvas.menuCur.startAt(mVector2, 2, T.sapxep);
		}
	}

	public override void doMenuTouchPlayer()
	{
		if (vecPlayer.size() == 0)
		{
			return;
		}
		memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
		if (type == 15)
		{
			getMenuSetPos();
		}
		else if (memCur != null && memCur.id != GameScreen.player.ID)
		{
			mVector mVector2 = new mVector();
			if (memCur.typeOnline == 1)
			{
				mVector2.addElement(cmdInfoPlayer);
			}
			if (type == 2)
			{
				mVector2.addElement(cmdMove);
			}
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
		if (type == 7 || type == 6 || type == 4 || type == 11 || type == 12 || type == 13 || type == 14 || type == 17 || type == 16)
		{
			mVector2.addElement(cmdNext);
			mVector2.addElement(cmdPre);
		}
		else if (type == 2)
		{
			mVector2.addElement(cmdUpdateBlackList);
			if (!GameCanvas.isTouch)
			{
				mVector2.addElement(cmdMove);
			}
		}
		else if (type == 15)
		{
			mVector2.addElement(cmdPosFightClan);
		}
		GameCanvas.menu.startAt(mVector2, 2, name);
	}

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		if (mem == null)
		{
			return;
		}
		g.setColor(11834730);
		g.fillRect(xpaint, ypaint - 1, 28, 29);
		g.fillRect(xpaint + 32, ypaint - 1, wSub - 32 - 60, 29);
		g.setColor(9403484);
		g.fillRect(xpaint + 1, ypaint, 26, 27);
		g.fillRect(xpaint + 32 + 1, ypaint, wSub - 34 - 60, 27);
		paintIconTop(g, mem, xpaint, ypaint);
		int num = 32;
		xpaint += num;
		if (typeClan(type))
		{
			MainImage iconClan = Potion.getIconClan(mem.idmap);
			if (iconClan != null && iconClan.img != null)
			{
				if (iconClan.frame == -1)
				{
					iconClan.set_Frame();
				}
				if (iconClan.frame <= 1)
				{
					g.drawImage(iconClan.img, xpaint + 10, ypaint + 8, 3);
				}
				else
				{
					int num2 = ((framepaint < iconClan.frame - 1) ? 3 : 15);
					if (CRes.abs(GameCanvas.gameTick - lastTick) > num2)
					{
						framepaint++;
						if (framepaint >= iconClan.frame)
						{
							framepaint = 0;
						}
						lastTick = GameCanvas.gameTick;
					}
					g.drawRegion(iconClan.img, 0, framepaint * iconClan.w, iconClan.w, iconClan.w, 0, xpaint + 10, ypaint + 8, 3);
				}
			}
			mFont.tahoma_7b_black.drawString(g, mem.name, xpaint + 20, ypaint + 2, 0);
			mFont.tahoma_7_black.drawString(g, mem.info, xpaint + 2, ypaint + 1 + GameCanvas.hText, 0);
		}
		else if (type == 15)
		{
			string name = mem.name;
			if (mem.isMe)
			{
				AvMain.FontBorderColor(g, name, xpaint + 31 + 20, ypaint, 2, 0, 7);
			}
			else
			{
				mFont.tahoma_7b_black.drawString(g, name, xpaint + 30 + 20, ypaint, 2);
			}
			mFont.tahoma_7_black.drawString(g, mem.info, xpaint + 25 + 25, ypaint + GameCanvas.hText, 2);
		}
		else
		{
			string name2 = mem.name;
			AvMain.fraStatusOnline.drawFrame(mem.typeOnline, xpaint + 25, ypaint + 5, 0, 3, g);
			if (mem.isMe)
			{
				AvMain.FontBorderColor(g, name2, xpaint + 31, ypaint, 0, 0, 7);
			}
			else
			{
				mFont.tahoma_7b_black.drawString(g, name2, xpaint + 30, ypaint, 0);
			}
			mFont.tahoma_7_black.drawString(g, mem.info, xpaint + 25, ypaint + GameCanvas.hText, 0);
			MainObject.paintHeadEveryWhere(g, mem.head, mem.hair, mem.hat, xpaint + 10, ypaint + hItem / 2 + 32, 0);
		}
	}

	private void paintIconTop(mGraphics g, InfoMemList mem, int x, int y)
	{
		if (type == 15)
		{
			mFont.tahoma_7b_white.drawString(g, mem.rank + 1 + string.Empty, x + 14, y + 8, 2);
		}
		else if (mem.rank < 10)
		{
			if (AvMain.fraIconTop == null)
			{
				AvMain.fraIconTop = new FrameImage(mImage.createImage("/interface/icontop.png"), 24, 24);
			}
			else if (mem.rank < 3)
			{
				AvMain.fraIconTop.drawFrame(mem.rank, x + 14, y + 14, 0, 3, g);
			}
			else
			{
				AvMain.fraIconTop.drawFrame(3, x + 14, y + 14, 0, 3, g);
			}
		}
		else
		{
			mFont.tahoma_7b_white.drawString(g, mem.rank + 1 + string.Empty, x + 14, y + 8, 2);
		}
	}

	public override void paintBg(mGraphics g)
	{
		AvMain.paintBG_AChi(g, xSub, ySub - 20, wSub, hSub + 20, 1);
		mFont.tahoma_7b_brown.drawString(g, nameList, xSub + wSub / 2, ySub + GameCanvas.hCommand / 2 + 3, 2);
		if (type != 15)
		{
			mFont.tahoma_7b_black.drawString(g, T.page + (page + 1), xSub + wSub / 2, ySub + hSub - GameCanvas.hCommand / 2 - 20, 2);
		}
	}

	public override void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(11936290);
		g.fillRect(xbegin, ybegin + idSelect * hItem, 29, 30);
		g.fillRect(xbegin + 31, ybegin + idSelect * hItem, wSub - 32 - 60 + 1, 30);
	}

	public static bool typeClan(sbyte type)
	{
		if (type == 6 || type == 11 || type == 12 || type == 14 || type == 13)
		{
			return true;
		}
		return false;
	}
}
