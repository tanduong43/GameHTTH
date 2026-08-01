public class AuctionScreen : PaintListScreen
{
	public const sbyte TYPE_LIST = 0;

	public const sbyte TYPE_DAU_GIA = 1;

	public const sbyte TYPE_NHAN = 2;

	public const sbyte TYPE_GIA_CHOT = 3;

	public const sbyte TYPE_CLOSE = 4;

	public static AuctionScreen instance;

	private mVector vecCmdDauGia = new mVector();

	private mVector vecCmdGiaChot = new mVector();

	private mVector vecCmdNhan = new mVector();

	public static mVector vecEff = new mVector();

	private int[] col = new int[4];

	private int yLechCmd;

	private int sizeItem = 29;

	private long timeBegin;

	private string[][] strShow;

	private int gap = 2;

	public AuctionScreen(mVector vec)
		: base(0, vec, string.Empty, 320, 200)
	{
		wSub = MotherCanvas.w - 148 - 40;
		int num = 0;
		if (GameCanvas.isTouch && wSub < 250)
		{
			num = (250 - wSub) / 2;
			wSub = 250;
		}
		if (!GameCanvas.isTouch)
		{
			wSub = MotherCanvas.w - 20;
			yLechCmd = 10;
			ySub += 10;
		}
		col[0] = 0;
		col[1] = wSub / 9 * 3;
		col[2] = col[1] + wSub / 9 * 2;
		col[3] = col[2] + wSub / 9 * 2;
		xSub = MotherCanvas.hw - wSub / 2 - num;
		hBegin = 35;
		hItem = 38;
		hContent = hSub - hBegin - 10;
		textrong = T.danhSachTrong;
		timeBegin = mSystem.currentTimeMillis();
		cmdInfoPlayer.indexMenu = 10;
		vecMenu.removeAllElements();
		if (!GameCanvas.isTouch)
		{
			vecMenu.addElement(cmdMenu);
		}
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xSub + wSub, ySub + 11 + 8, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			vecMenu.addElement(cmdClose);
		}
		setPosVecMenu(vecMenu);
		strShow = new string[vecPlayer.size()][];
		for (int i = 0; i < vecPlayer.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(i);
			iCommand iCommand2 = new iCommand(T.dauGia, 7, i, this);
			iCommand2.setPos(xSub + col[1] + 26 + 10, ySub + hBegin + (hItem + gap) * i + 26, AvMain.fraCmdNhanNapThe, iCommand2.caption);
			if (infoMemList.isOwn == 1)
			{
				iCommand2.setTypeRed();
			}
			vecCmdDauGia.addElement(iCommand2);
			iCommand iCommand3 = new iCommand(T.cmdBuy, 8, i, this);
			iCommand3.setPos(xSub + col[2] + 26 + 10, ySub + hBegin + (hItem + gap) * i + 26, AvMain.fraCmdNhanNapThe, iCommand3.caption);
			vecCmdGiaChot.addElement(iCommand3);
			iCommand iCommand4 = new iCommand(T.nhanQuest, 9, i, this);
			iCommand4.setPos(xSub + col[1] + 26 + 10, ySub + hBegin + (hItem + gap) * i + 26, AvMain.fraCmdNhanNapThe, iCommand4.caption);
			iCommand4.setTypeGreen();
			vecCmdNhan.addElement(iCommand4);
			strShow[i] = mFont.tahoma_7b_brown.splitFontArray(infoMemList.item.name, col[1] - 5 - sizeItem);
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(subIndex);
		switch (index)
		{
		case 7:
			GlobalService.gI().Send_Type_ID(-91, 1, (sbyte)infoMemList.id);
			break;
		case 8:
			GlobalService.gI().Send_Type_ID(-91, 3, (sbyte)infoMemList.id);
			break;
		case 9:
			GlobalService.gI().Send_Type_ID(-91, 2, (sbyte)infoMemList.id);
			break;
		case 10:
			infoMemList = (InfoMemList)vecPlayer.elementAt(idSelect);
			ItemInfo.instance = new ItemInfo(type, infoMemList);
			ItemInfo.instance.Show(this);
			break;
		case 2:
			GlobalService.gI().Send_Type(-91, 4);
			break;
		}
		base.commandPointer(index, subIndex);
	}

	public override void doMenuTouchPlayer()
	{
		if (vecPlayer.size() != 0)
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			if (memCur != null)
			{
				mVector mVector2 = new mVector();
				mVector2.addElement(cmdInfoPlayer);
				GameCanvas.menu.startAt(mVector2, 2, T.info);
			}
		}
	}

	public override void doMenu()
	{
		mVector mVector2 = new mVector();
		string menu = T.menu;
		if (vecPlayer.size() > 0)
		{
			memCur = (InfoMemList)vecPlayer.elementAt(idSelect);
			if (memCur != null && !GameCanvas.isTouch)
			{
				mVector2.addElement(cmdInfoPlayer);
				if (memCur.isOwn == 1)
				{
					iCommand o = (iCommand)vecCmdNhan.elementAt(idSelect);
					mVector2.addElement(o);
				}
				iCommand o2 = (iCommand)vecCmdDauGia.elementAt(idSelect);
				iCommand o3 = (iCommand)vecCmdGiaChot.elementAt(idSelect);
				mVector2.addElement(o2);
				mVector2.addElement(o3);
			}
		}
		GameCanvas.menu.startAt(mVector2, 2, menu);
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		paintBg(g);
		cmdClose.paint(g, cmdClose.xCmd, cmdClose.yCmd);
		int num = xSub + 10;
		int num2 = ySub + hBegin;
		setClip(g);
		if (vecPlayer.size() == 0)
		{
			mFont.tahoma_7_black.drawString(g, textrong, xSub + wSub / 2, ySub + (hSub - 20) / 2, 2);
		}
		else
		{
			if (idSelect >= 0)
			{
				paintSelect(g, num, num2 - 1, wSub - 20);
			}
			for (int i = 0; i < vecPlayer.size(); i++)
			{
				if (num2 - list.cmx + hItem >= hBegin + ySub && num2 - list.cmx - hItem <= hBegin + hContent + ySub)
				{
					InfoMemList mem = (InfoMemList)vecPlayer.elementAt(i);
					paintInfo(g, mem, num, num2, i, wSub - 20);
				}
				num2 += hItem + gap;
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (vecMenu != null)
		{
			for (int j = 0; j < vecMenu.size(); j++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		if (right != null)
		{
			right.paint(g, right.xCmd, right.yCmd);
		}
		MainTab.paintMoney(g, MotherCanvas.w - 78, 4 + GameScreen.h12plus);
		for (int k = 0; k < vecEff.size(); k++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(k);
			if (mainEffect != null && !mainEffect.isRemove && !mainEffect.isStop)
			{
				mainEffect.paint(g);
			}
		}
	}

	public override void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wpaint)
	{
		if (mem == null)
		{
			return;
		}
		g.setColor(14203529);
		paintBgMemListPhatLenh(g, xpaint, ypaint, wpaint, hItem);
		mem.item.paint(g, xpaint + 4 + sizeItem / 2, ypaint + 4 + sizeItem / 2, sizeItem);
		if (yLechCmd == 0 && strShow[i] != null)
		{
			int num = ypaint + 5;
			for (int j = 0; j < strShow[i].Length; j++)
			{
				mFont.tahoma_7b_brown.drawString(g, strShow[i][j], xpaint + 2 + 33, num, 0);
				num += 12;
			}
		}
		int num2 = xpaint;
		if (!GameCanvas.isTouch)
		{
			num2 = xSub;
		}
		AvMain.fraMoney.drawFrame(8, num2 + col[1] + 2, ypaint + 2 + yLechCmd, 0, 0, g);
		AvMain.Font3dColor(g, mem.item.priceDauGia + string.Empty, num2 + col[1] + 15, ypaint + 3 + yLechCmd, 0, 5);
		mFont mFont2 = mFont.tahoma_7b_black;
		if (GameCanvas.isSmallScreen)
		{
			mFont2 = mFont.tahoma_7_black;
		}
		if (mem.item.timeRemain == -1)
		{
			mFont.tahoma_7b_red.drawString(g, T.chuaMo, num2 + col[3] + (wpaint - col[3]) / 2, ypaint + 13, 2);
		}
		else
		{
			mFont2.drawString(g, Interface_Game.ConvertTimeToStr(mem.item.timeRemain), num2 + col[3] + (wpaint - col[3]) / 2, ypaint + 13, 2);
		}
		if (mem.item.giaChot > 0)
		{
			AvMain.fraMoney.drawFrame(8, num2 + col[2] + 2, ypaint + 2 + yLechCmd, 0, 0, g);
			AvMain.Font3dColor(g, mem.item.giaChot + string.Empty, num2 + col[2] + 15, ypaint + 3 + yLechCmd, 0, 5);
		}
		else
		{
			mFont.tahoma_7_black.drawString(g, "---", num2 + col[2] + 26, ypaint + 13, 2);
		}
		if (GameCanvas.isTouch)
		{
			if (mem.item.timeRemain > 0 || mem.isOwn == 0)
			{
				iCommand iCommand2 = (iCommand)vecCmdDauGia.elementAt(i);
				iCommand2.paintCmd1Frame(g, iCommand2.xCmd, iCommand2.yCmd);
			}
			else if (mem.isOwn == 1)
			{
				iCommand iCommand3 = (iCommand)vecCmdNhan.elementAt(i);
				iCommand3.paintCmd1Frame(g, iCommand3.xCmd, iCommand3.yCmd);
			}
			if (mem.item.giaChot > 0)
			{
				iCommand iCommand2 = (iCommand)vecCmdGiaChot.elementAt(i);
				iCommand2.paintCmd1Frame(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		if (mem.item.timeRemain <= 0 && mem.item.timeRemain != -1)
		{
			g.drawImage(AvMain.imgComplete, xpaint + sizeItem + 8, ypaint + 1, 0);
		}
	}

	public override void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(11936290);
		g.drawRect(xbegin - 1, ybegin + idSelect * (hItem + gap), wFocus + 1, hItem + 1);
	}

	public override void paintBg(mGraphics g)
	{
		paintPaper(g, xSub, ySub, wSub, hSub, wSub, AvMain.PAPER_NORMAL);
		g.setColor(15972174);
		g.fillRoundRect(xSub + 5, ySub + 12, wSub - 10, 16, 4, 4);
		AvMain.FontBorderColor(g, (!GameCanvas.isTouch) ? T.tenVP : T.tenVatPham, xSub + col[0] + (col[1] - col[0]) / 2, ySub + 14, 2, 6, 5);
		AvMain.FontBorderColor(g, (!GameCanvas.isTouch) ? T.dGia : T.dauGia, xSub + col[1] + (col[2] - col[1]) / 2, ySub + 14, 2, 6, 5);
		AvMain.FontBorderColor(g, (!GameCanvas.isTouch) ? T.gChot : T.giaChot, xSub + col[2] + (col[3] - col[2]) / 2, ySub + 14, 2, 6, 5);
		AvMain.FontBorderColor(g, (!GameCanvas.isTouch) ? T.tGian : T.thoiGianCon, xSub + col[3] + (wSub - col[3]) / 2 - 5, ySub + 14, 2, 6, 5);
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isTouch)
		{
			if (left != null)
			{
				if (left.isPosCmd())
				{
					left.updatePointer();
				}
				else if (GameCanvas.isPointSelect(0, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
				{
					left.perform();
				}
			}
			if (right != null)
			{
				if (right.isPosCmd())
				{
					right.updatePointer();
				}
				else if (GameCanvas.isPointSelect(MotherCanvas.w - GameCanvas.wCommand * 2, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
				{
					right.perform();
				}
			}
			if (center != null)
			{
				if (center.isPosCmd())
				{
					center.updatePointer();
				}
				else if (GameCanvas.isPointSelect(MotherCanvas.hw - GameCanvas.wCommand, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
				{
					center.perform();
				}
			}
		}
		if (vecMenu != null)
		{
			for (int i = 0; i < vecMenu.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecMenu.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		for (int j = 0; j < vecPlayer.size(); j++)
		{
			InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(j);
			if (infoMemList.item.timeRemain > 0)
			{
				iCommand iCommand3 = (iCommand)vecCmdDauGia.elementAt(j);
				iCommand3.updatePointerInList(list.cmx);
				if (infoMemList.item.giaChot > 0)
				{
					iCommand3 = (iCommand)vecCmdGiaChot.elementAt(j);
					iCommand3.updatePointerInList(list.cmx);
				}
			}
			else if (infoMemList.isOwn == 1)
			{
				iCommand iCommand3 = (iCommand)vecCmdNhan.elementAt(j);
				iCommand3.updatePointerInList(list.cmx);
			}
		}
		if (!GameCanvas.isPointerSelect || vecPlayer.size() <= 0 || !GameCanvas.isPoint(xSub, ySub + hBegin, wSub, hContent))
		{
			return;
		}
		GameCanvas.isPointerSelect = false;
		int num = (GameCanvas.py - (ySub + hBegin) + list.cmx) / (hItem + gap);
		if (num >= 0 && num < vecPlayer.size())
		{
			if (idSelect != num)
			{
				idSelect = num;
			}
			else
			{
				doMenuTouchPlayer();
			}
		}
	}

	public override void setCamera()
	{
		int limX = vecPlayer.size() * (hItem + gap) - hContent + miniItem;
		list = new ListNew(xSub + 10, ySub + hBegin, wSub - 20, hContent, 0, 0, limX, isLim0: true);
		list.setToX((idSelect + 1) * (hItem + gap) - hContent / 2);
	}

	public override void update()
	{
		base.update();
		if (GameCanvas.timeNow - timeBegin >= 1000)
		{
			timeBegin += 1000L;
			for (int i = 0; i < vecPlayer.size(); i++)
			{
				InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(i);
				if (infoMemList.item.timeRemain > 0)
				{
					infoMemList.item.timeRemain--;
				}
			}
		}
		for (int j = 0; j < vecEff.size(); j++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(j);
			if (mainEffect != null && !mainEffect.isRemove && !mainEffect.isStop)
			{
				mainEffect.update();
			}
		}
	}

	public void SetNewValueDauGia(sbyte id, short idPlayer, int curPrice, int time)
	{
		mSystem.outz("id = " + id + " Set new value dau gia = " + curPrice + " time = " + time);
		for (int i = 0; i < vecPlayer.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)vecPlayer.elementAt(i);
			if (infoMemList.id == id)
			{
				if (idPlayer == GameScreen.player.ID)
				{
					infoMemList.isOwn = 1;
					iCommand iCommand2 = (iCommand)vecCmdDauGia.elementAt(i);
					iCommand2.setTypeRed();
				}
				else
				{
					infoMemList.isOwn = 0;
					iCommand iCommand3 = (iCommand)vecCmdDauGia.elementAt(i);
					iCommand3.setTypeButton(iCommand.BTT_NOR);
				}
				infoMemList.item.priceDauGia = curPrice;
				infoMemList.item.timeRemain = time;
				break;
			}
		}
	}

	public static void addEffectNumImage(string content, int x, int y, sbyte typeColor, FrameImage fra, int frame)
	{
		EffectNum effectNum = new EffectNum(content, x, y, typeColor, fra, frame);
		int num = GameScreen.find_Index_Stop(vecEff);
		if (num == vecEff.size())
		{
			vecEff.addElement(effectNum);
		}
		else
		{
			vecEff.setElementAt(effectNum, num);
		}
	}
}
