public class Sudo_Mem : ChatDetail
{
	private iCommand cmdView;

	private iCommand cmdUpdate;

	private int minChat;

	private int maxChat;

	public static mVector vecSudo = new mVector();

	private int wPaintQua = 93;

	private int hItem = 48;

	public static int tickupdate = 0;

	public Sudo_Mem(string name, sbyte type)
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
		wItem = 48;
		CamDetailChat = new ListNew(xBe, yBe, wCon, hCon, wItem, 0, vecSudo.size() * wItem - hCon, isLim0: true);
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
			GlobalService.gI().Send_Sudo(2);
			tickupdate = 40;
			break;
		}
	}

	public void setDataCam()
	{
		if (CamDetailChat == null)
		{
			CamDetailChat = new ListNew(xBe, yBe, wCon, hCon, wItem, 0, vecSudo.size() * wItem - hCon, isLim0: true);
			return;
		}
		CamDetailChat.cmxLim = vecSudo.size() * wItem - hCon;
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
		if (vecSudo != null)
		{
			maxChat = vecSudo.size();
		}
		if (tickupdate > 0)
		{
			MsgDialog.fraImgWaiting.drawFrame(GameCanvas.gameTick / 6 % MsgDialog.fraImgWaiting.nFrame, xBe + wCon / 2, yBe + hCon / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER, g);
			return;
		}
		for (int i = 0; i < vecSudo.size(); i++)
		{
			int ypaint = yBe + i * wItem;
			int xpaint = xBe - 2;
			int wsub = wCon + 4;
			InfoMemList mem = (InfoMemList)vecSudo.elementAt(i);
			paintInfo(g, mem, xpaint, ypaint, i, wsub);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		base.paint(g);
	}

	public void paintInfo(mGraphics g, InfoMemList mem, int xpaint, int ypaint, int i, int wsub)
	{
		if (mem != null)
		{
			AvMain.paintRect(g, xpaint, ypaint, wsub - 1, 40, 1, 3);
			AvMain.paintRect(g, xpaint + 45, ypaint + 2, wsub - 42 - 10, 14, 1, 1);
			mFont.tahoma_7b_black.drawString(g, mem.title, xpaint + 45 + (wsub - 52) / 2, ypaint + 3, 2);
			mFont.tahoma_7b_white.drawString(g, mem.name, xpaint + 45 + (wsub - 52) / 2, ypaint + 22, 2);
			g.drawImage(AvMain.imgBorderIcon, xpaint + 4 + 16, ypaint + 4 + 16, 3);
			MainObject.paintHeadEveryWhere(g, mem.head, mem.hair, mem.hat, xpaint + 2 + 16, ypaint + 54, 0);
			AvMain.fraStatusOnline.drawFrame(mem.typeOnline, xpaint + 45 + (wsub - 52) / 10, ypaint + 22 + 6, 0, 3, g);
			g.drawImage(AvMain.imgLvClan, xpaint + 45 + (wsub - 52) / 5 * 4 + 5, ypaint + 22 - 5, 0);
			mFont.tahoma_7b_white.drawString(g, mem.Lv + string.Empty, xpaint + 45 + (wsub - 52) / 5 * 4 + 18, ypaint + 22 + 2, 2);
		}
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
			if (idSelect < vecSudo.size() - 1)
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
		if (GameCanvas.isPointerSelect && vecSudo.size() > 0 && GameCanvas.isPoint(xBe, yBe, wCon, hCon))
		{
			GameCanvas.isPointerSelect = false;
			int num = (GameCanvas.py - yBe + CamDetailChat.cmx) / wItem;
			if (num >= 0 && num < vecSudo.size())
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
		if (idSelect >= 0 && idSelect <= vecSudo.size())
		{
			InfoMemList mem = (InfoMemList)vecSudo.elementAt(idSelect);
			MsgInfoMemSudo dgl = new MsgInfoMemSudo(mem);
			GameCanvas.Start_Sub_Dialog(dgl);
		}
	}

	public void updateLimCam()
	{
		int num = vecSudo.size() * wItem - hCon;
		if (num > 0)
		{
			CamDetailChat.cmxLim = num;
		}
	}
}
