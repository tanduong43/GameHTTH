public class Clan_Mem : ChatDetail
{
	private iCommand cmdView;

	private iCommand cmdUpdate;

	private int minChat;

	private int maxChat;

	public static int tickupdate;

	public Clan_Mem(string name, sbyte type)
		: base(name, type)
	{
	}

	public override void setPos(int xBe, int yBe, int wCon, int hCon, int miniItem, int hchat)
	{
		base.xBe = xBe;
		base.yBe = yBe;
		base.wCon = wCon;
		base.hCon = hCon;
		base.miniItem = miniItem;
		hChat = hchat;
		wItem = 30;
		CamDetailChat = new ListNew(xBe, yBe, wCon, hCon, wItem, 0, vecDetail.size() * wItem - hCon, isLim0: true);
		cmdView = new iCommand(T.view, 0, this);
		cmdUpdate = new iCommand(T.update, 1, this);
		if (!GameCanvas.isTouch)
		{
			center = cmdView;
			left = cmdUpdate;
		}
		okCMD = cmdView;
	}

	public override void beginFocus()
	{
		tickupdate = 0;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			doMenuTouchPlayer();
			break;
		case 1:
			GlobalService.gI().Clan_CMD(17, string.Empty, 0, 0);
			tickupdate = 40;
			break;
		}
	}

	public void setDataCam()
	{
		if (CamDetailChat == null)
		{
			CamDetailChat = new ListNew(xBe, yBe, wCon, hCon, wItem, 0, vecDetail.size() * wItem - hCon, isLim0: true);
			return;
		}
		CamDetailChat.cmxLim = vecDetail.size() * wItem - hCon;
		if (CamDetailChat.cmxLim < 0)
		{
			CamDetailChat.cmxLim = 0;
		}
	}

	public override void paint(mGraphics g)
	{
		g.setClip(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon + 2);
		g.saveCanvas();
		g.ClipRec(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon + 2);
		g.translate(0, -CamDetailChat.cmx);
		minChat = 0;
		maxChat = 0;
		if (vecDetail != null)
		{
			maxChat = vecDetail.size();
		}
		if (tickupdate > 0)
		{
			MsgDialog.fraImgWaiting.drawFrame(GameCanvas.gameTick / 6 % MsgDialog.fraImgWaiting.nFrame, xBe + wCon / 2, yBe + hCon / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER, g);
			return;
		}
		for (int i = minChat; i <= maxChat; i++)
		{
			if (i < vecDetail.size() && i >= 0)
			{
				InfoMemList infoMemList = (InfoMemList)vecDetail.elementAt(i);
				int num = yBe + i * wItem;
				sbyte b = (sbyte)((infoMemList.name.CompareTo(GameScreen.player.name) != 0) ? 4 : 5);
				paintBorder(g, b, -1, 0, wItem - 2, num, i == idSelect);
				int num2 = xBe - 2;
				int num3 = wCon + 4;
				g.drawImage(AvMain.imgDonateClan, num2 + num3 - 47, num - 1 + 2, 0);
				g.setColor(getColorBorderNumber(b));
				g.fillRect(num2 + num3 - 51, num - 1 + 17, 22, 9);
				g.drawImage(AvMain.imgLvClan, num2 + num3 - 28, num - 1 + 1, 0);
				AvMain.FontBorderColor(g, infoMemList.name, xBe + miniItem * 2 + 16, num + miniItem / 2, 0, 0, 7);
				mFont.tahoma_7b_black.drawString(g, infoMemList.getCaptionClan(infoMemList.chucInClan), xBe + miniItem * 2 + 16 + 6, num + GameCanvas.hText - 1, 0);
				mFont.tahoma_7_white.drawString(g, infoMemList.numTangqua + string.Empty, xBe - 2 + wCon + 4 - 40, num + 15, 2);
				mFont.tahoma_7b_white.drawString(g, infoMemList.Lv + string.Empty, xBe - 2 + wCon + 4 - 15, num + 7, 2);
				MainObject.paintHeadEveryWhere(g, infoMemList.head, infoMemList.hair, infoMemList.hat, xBe + miniItem * 2 + 2, num + miniItem + 45, 2);
				AvMain.fraStatusOnline.drawFrame(infoMemList.typeOnline, xBe + miniItem * 2 + 16 + 1, num + miniItem + GameCanvas.hText, 0, 3, g);
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		base.paint(g);
	}

	public override void update()
	{
		CamDetailChat.moveCamera();
		if (tickupdate > 0)
		{
			tickupdate--;
		}
	}

	public override void updatekey()
	{
		int num = idSelect;
		if (GameCanvas.keyMyHold[2])
		{
			GameCanvas.clearKeyHold(2);
			if (idSelect > 0)
			{
				idSelect--;
			}
		}
		else if (GameCanvas.keyMyHold[8])
		{
			GameCanvas.clearKeyHold(8);
			if (idSelect < vecDetail.size() - 1)
			{
				idSelect++;
			}
		}
		if (num != idSelect)
		{
			setXCam();
		}
		base.updatekey();
		updatekeyPC();
	}

	private void setXCam()
	{
		int toX = idSelect * wItem - hCon / 4;
		CamDetailChat.setToX(toX);
	}

	public override void updatePointer()
	{
		CamDetailChat.update_Pos_UP_DOWN();
		if (GameCanvas.isPointerSelect && vecDetail.size() > 0 && GameCanvas.isPoint(xBe, yBe, wCon, hCon))
		{
			GameCanvas.isPointerSelect = false;
			int num = (GameCanvas.py - yBe + CamDetailChat.cmx) / wItem;
			if (num >= 0 && num < vecDetail.size())
			{
				idSelect = num;
				cmdView.perform();
			}
		}
		if (GameCanvas.isPointerRelease && CamDetailChat.cmx < -wItem && tickupdate <= 0)
		{
			cmdUpdate.perform();
			GameCanvas.isPointerRelease = false;
		}
	}

	private void doMenuTouchPlayer()
	{
		if (idSelect >= 0 && idSelect <= vecDetail.size())
		{
			InfoMemList mem = (InfoMemList)vecDetail.elementAt(idSelect);
			MsgInfoMemClan dgl = new MsgInfoMemClan(mem);
			GameCanvas.Start_Sub_Dialog(dgl);
		}
	}

	public void updateLimCam()
	{
		int num = vecDetail.size() * wItem - hCon;
		if (num > 0)
		{
			CamDetailChat.cmxLim = num;
		}
	}
}
