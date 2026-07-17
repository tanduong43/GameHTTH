public class MsgArchiDaily : MsgDialog
{
	public static mVector vecArchi = new mVector("MsgArchiDaily.vecArchi");

	private iCommand cmdReward;

	public void setinfo(string name, mVector vec)
	{
		vecArchi.removeAllElements();
		vecArchi = vec;
		wDia = 260;
		hDia = 180;
		if (wDia > MotherCanvas.w)
		{
			wDia = MotherCanvas.w;
		}
		if (hDia > MotherCanvas.h - 26)
		{
			hDia = GameCanvas.hCommand - 26;
		}
		xDia = MotherCanvas.w / 2 - wDia / 2;
		yDia = MotherCanvas.h / 2 - hDia / 2 + 13;
		wItem = 46;
		nameDialog = name;
		list = new ListNew(xDia, yDia + 38, wDia, hDia - 40, 0, 0, vec.size() * wItem - (hDia - 60), isLim0: false);
		cmdReward = new iCommand(T.reward, 13, this);
		cmdReward = AvMain.setPosCMD(cmdReward, 0);
		cmdClose = new iCommand(T.close, -1, this);
		if (!GameCanvas.isTouch)
		{
			idSelect = 0;
			right = cmdClose;
			cmdClose = AvMain.setPosCMD(cmdClose, 2);
			getCmd();
		}
		else
		{
			if (GameCanvas.isTouchNoOrPC())
			{
				idSelect = 0;
			}
			else
			{
				idSelect = -1;
			}
			cmdClose.setPos(xDia + wDia / 2 + 72, yDia - 14, MainTab.fraCloseTab, string.Empty);
			if (AvMain.mimgBgA == null)
			{
				cmdClose.yCmd += 19;
			}
		}
		backCMD = cmdClose;
		cmdList.addElement(cmdClose);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 13:
			GlobalService.gI().Archi_Daily((sbyte)idSelect, 1);
			break;
		case -1:
			GameCanvas.end_Dialog();
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		AvMain.paintBG_AChi(g, xDia, yDia, wDia, hDia, 0);
		if (AvMain.mimgBgA == null)
		{
			AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia, 2, 0, 8);
		}
		else
		{
			AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia - 19, 2, 0, 8);
		}
		int num = xDia + 30;
		int num2 = yDia + 38;
		int num3 = wDia - 60;
		g.saveCanvas();
		g.ClipRec(xDia + 30 - 2, yDia + 38 - 2, wDia - 60 + 4, hDia - 60);
		setClip(g);
		for (int i = 0; i < vecArchi.size(); i++)
		{
			g.setColor(10653032);
			if (idSelect == i)
			{
				g.setColor(11828531);
				g.fillRect(num - 1, num2 + 5 - 1, num3 + 2, wItem - 10 + 2);
				g.setColor(14733208);
			}
			g.fillRect(num, num2 + 5, num3, wItem - 10);
			g.drawImage(AvMain.imgBorderIcon, num + 2 + 16, num2 + wItem / 2, 3);
			InfoArchi infoArchi = (InfoArchi)vecArchi.elementAt(i);
			short id = infoArchi.idIcon;
			if (infoArchi.typeReward == InfoArchi.REWARD_OFF)
			{
				id = 19;
			}
			MainImage imageAll = ObjectData.getImageAll(id, ObjectData.hashImageItemOther, 9000);
			if (imageAll.img != null)
			{
				g.drawImage(imageAll.img, num + 2 + 16, num2 + wItem / 2, 3);
			}
			g.setColor(13216120);
			if (idSelect == i)
			{
				g.setColor(16181424);
			}
			g.fillRect(num + 40, num2 + 5 + 4, num3 - 44, 12);
			if (infoArchi.typeReward == InfoArchi.REWARD_OFF)
			{
				mFont.tahoma_7b_black.drawString(g, T.lastArchi, num + 42, num2 + 9, 0);
			}
			else
			{
				mFont.tahoma_7b_black.drawString(g, infoArchi.name, num + 42, num2 + 9, 0);
				mFont.tahoma_7_black.drawString(g, infoArchi.valueCur + "/" + infoArchi.valueMax, num + num3 - 6, num2 + 9, 1);
			}
			if (-list.cmx + num2 + 12 + 12 > yDia + 38 - 2 && -list.cmx + num2 + 12 + 12 < yDia + 38 - 2 + hDia - 70)
			{
				if (infoArchi.maxLechInfo > 0 && infoArchi.typeReward == InfoArchi.REWARD_DOING)
				{
					g.setClip(num + 40, num2 + 5 + 12, num3 - 44, 20);
					mFont.tahoma_7_black.drawString(g, infoArchi.info, num + 55 - GameCanvas.gameTick % infoArchi.maxLechInfo, num2 + 12 + 12, 0);
					setClip(g);
				}
				else if (infoArchi.typeReward == InfoArchi.REWARD_READY)
				{
					g.drawRegion(AvMain.imgReward, 0, 0, 28, 15, 0, num + num3 - 20, num2 + 30, 3);
					mFont.tahoma_7_green.drawString(g, T.reward, num + num3 - 30, num2 + 25, 1);
				}
				else if (infoArchi.typeReward == InfoArchi.REWARD_GOT)
				{
					g.drawRegion(AvMain.imgReward, 0, 15, 28, 15, 0, num + num3 - 20, num2 + 30, 3);
					mFont.tahoma_7_white.drawString(g, T.danhan, num + num3 - 30, num2 + 25, 1);
				}
				else if (infoArchi.typeReward == InfoArchi.REWARD_OFF)
				{
					g.drawRegion(AvMain.imgReward, 0, 30, 28, 15, 0, num + num3 - 20, num2 + 30, 3);
					mFont.tahoma_7_white.drawString(g, T.strlock, num + num3 - 30, num2 + 25, 1);
				}
				else
				{
					mFont.tahoma_7_black.drawString(g, infoArchi.info, num + 42, num2 + 12 + 12, 0);
				}
			}
			num2 += wItem;
			if (i < vecArchi.size() - 1)
			{
				g.setColor(14203529);
				g.fillRect(num + 6, num2 - 1, num3 - 12, 2);
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (cmdList != null)
		{
			for (int j = 0; j < cmdList.size(); j++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	public void setClip(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(xDia + 30 - 2, yDia + 38 - 2, wDia - 60 + 4, hDia - 60);
		g.translate(0, -list.cmx);
	}

	public override void update()
	{
		list.moveCamera();
		updatePointer();
		updatekey();
		if (GameCanvas.isTouch && GameCanvas.isPointerMove && idSelect != -1)
		{
			idSelect = -1;
		}
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointerSelect && vecArchi.size() > 0)
		{
			int x = xDia + 30;
			int num = yDia + 38;
			int w = wDia - 60;
			int h = hDia - 60;
			if (GameCanvas.isPoint(x, num, w, h))
			{
				int num2 = (GameCanvas.py - num + list.cmx) / wItem;
				if (num2 >= 0 && num2 < vecArchi.size())
				{
					setFocus(num2);
					idSelect = num2;
				}
				GameCanvas.isPointerSelect = false;
			}
		}
		base.updatePointer();
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
		if (flag)
		{
			idSelect = AvMain.resetSelect(idSelect, vecArchi.size() - 1, isreset: false);
			getCmd();
			if (GameCanvas.isTouchNoOrPC())
			{
				list.setToX((idSelect + 1) * wItem - (hDia - 60) / 2);
			}
		}
		updatekeyPC();
		if (!GameCanvas.isTouch)
		{
			updatekeyCMD();
		}
	}

	private void getCmd()
	{
		cmdList.removeAllElements();
		if (idSelect >= 0 && idSelect < vecArchi.size())
		{
			InfoArchi infoArchi = (InfoArchi)vecArchi.elementAt(idSelect);
			if (infoArchi.typeReward == InfoArchi.REWARD_READY)
			{
				center = cmdReward;
				okCMD = cmdReward;
				cmdList.addElement(cmdReward);
			}
			else
			{
				center = null;
				okCMD = null;
			}
		}
		cmdList.addElement(cmdClose);
	}

	private void setFocus(int value)
	{
		InfoArchi infoArchi = (InfoArchi)vecArchi.elementAt(value);
		if (infoArchi.typeReward == InfoArchi.REWARD_READY)
		{
			GlobalService.gI().Archi_Daily((sbyte)value, 1);
		}
	}
}
