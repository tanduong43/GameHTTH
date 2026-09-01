public class MsgGiftLogin : MsgDialog
{
	private Item_Drop[] mItemgift;

	private int wItemInterface = 22;

	private MainItem itemMain;

	public static sbyte TYPE_DAILY_GIFT;

	public static sbyte TYPE_BOX_CHOICE = 1;

	public void setinfoShow_GiftLogin(sbyte type, string name, string info, Item_Drop[] mitem, sbyte action, string nameCmd, MainItem itemMain)
	{
		base.type = type;
		mItemgift = mitem;
		nameDialog = name;
		this.itemMain = itemMain;
		wItem = 46;
		wItemInterface = 22;
		wDia = 212;
		if (wDia > MotherCanvas.w)
		{
			wDia = MotherCanvas.w;
		}
		if (mitem.Length < 4)
		{
			wDia = mitem.Length * 46 + 28;
		}
		hDia = ((mitem.Length - 1) / 4 + 2) * 46 + 5;
		if (hDia > 200)
		{
			hDia = 200;
		}
		maxWShow = wDia;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 - 5;
		if (mitem.Length / 4 >= 3)
		{
			int num = hDia - wItemInterface - wItemInterface / 2 - 10;
			list = new ListNew(xDia, wItemInterface + 10, wDia, num, 0, 0, ((mitem.Length - 1) / 4 + 1) * wItem - (num - 5), isLim0: false);
		}
		iCommand iCommand2 = new iCommand(nameCmd, 14, action, this);
		cmdList.addElement(iCommand2);
		cmdClose = new iCommand(T.close, 10, this);
		if (type == TYPE_BOX_CHOICE)
		{
			cmdClose = new iCommand(T.close, -1, this);
		}
		cmdList.addElement(cmdClose);
		if (GameCanvas.isTouchNoOrPC())
		{
			center = iCommand2;
			right = cmdClose;
			cmdClose = AvMain.setPosCMD(cmdClose, 2);
		}
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xDia + wDia - 20, yDia + 19, MainTab.fraCloseTab, string.Empty);
		}
		iCommand2 = AvMain.setPosCMD(iCommand2, 0);
		wShowPaper = wDia;
		idSelect = -1;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 14:
			if (type == TYPE_DAILY_GIFT)
			{
				GlobalService.gI().Daily_Login((sbyte)subIndex);
			}
			else if (type == TYPE_BOX_CHOICE)
			{
				if (idSelect >= 0 && idSelect <= mItemgift.Length - 1)
				{
					string[] args = new string[2]
					{
						mItemgift[idSelect].name,
						itemMain.name
					};
					mVector mVector2 = new mVector();
					iCommand o = new iCommand(T.strconfirm, 16, this);
					mVector2.addElement(o);
					GameCanvas.Start_Normal_DiaLog(GameMidlet.format(T.hoidoiqua, args), mVector2, isCmdClose: true);
				}
				else
				{
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.chuachonqua);
				}
			}
			break;
		case 16:
			if (itemMain != null)
			{
				GlobalService.gI().Use_Item_BOX_CHOICE(itemMain.ID, itemMain.typeObject, (sbyte)idSelect);
			}
			GameCanvas.end_Dialog();
			break;
		default:
			base.commandPointer(index, subIndex);
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
		GameCanvas.resetTrans(g);
		int num = yDia + wItemInterface + 10;
		paintBanner(g);
		AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia + 15, 2, 6, 5);
		AvMain.paintRect(g, xDia + 10, num, wDia - 20, hDia - wItemInterface - wItemInterface / 2 - 10, 0, 4);
		g.setClip(MotherCanvas.hw - wShowPaper / 2, num + 2, wShowPaper, hDia - wItemInterface - wItemInterface / 2 - 14);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, num + 2, wShowPaper, hDia - wItemInterface - wItemInterface / 2 - 14);
		if (list != null)
		{
			g.translate(0, -list.cmx);
		}
		for (int i = 0; i < mItemgift.Length; i++)
		{
			if (list != null && (i / 4 < list.cmx / wItem || i / 4 >= (list.cmx + wItem / 2) / wItem + 4))
			{
				continue;
			}
			int num2 = xDia + 14 + wItem / 2 + i % 4 * wItem + 2;
			int num3 = num + wItem / 2 + i / 4 * wItem + 5;
			Item_Drop item_Drop = mItemgift[i];
			sbyte index = 1;
			int color = 8148796;
			if (item_Drop.typeGiftDaily == 1 || (type == TYPE_BOX_CHOICE && idSelect == i))
			{
				index = 2;
				color = 2087750;
			}
			AvMain.paintImgButton(g, num2 - wItem / 2, num3 - wItem / 2, wItem - 4, wItem - 4, index);
			if (item_Drop.IdIcon >= 0)
			{
				item_Drop.paintXY(g, num2 + 3, num3 - 8);
				if (item_Drop.typeGiftDaily == 2)
				{
					MsgDialog.fraAutoMpHp.drawFrame(4, num2 - 9, num3 - 8, 0, 3, g);
				}
				else
				{
					mFont.tahoma_7b_white.drawString(g, item_Drop.num + string.Empty, num2 - 13, num3 - 12, 2);
				}
				g.setColor(color);
				g.fillRoundRect(num2 - wItem / 2 + 2, num3 + wItem / 2 - 15, wItem - 8, 10, 4, 4);
				mFont.tahoma_7_white.drawString(g, item_Drop.name, num2 - 2, num3 + wItem / 2 - 16, 2);
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		for (int j = 0; j < cmdList.size(); j++)
		{
			iCommand iCommand2 = (iCommand)cmdList.elementAt(j);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
	}

	private void paintBanner(mGraphics g)
	{
		g.setColor(15972174);
		g.fillRoundRect(xDia + 20, yDia + 12, wDia - 40, 16, 4, 4);
	}

	public override void update()
	{
		if (list != null)
		{
			list.moveCamera();
		}
		if (isClose)
		{
			updateClose();
			return;
		}
		updateOpen();
		if (GameCanvas.isTouchNoOrPC())
		{
			updatekey();
		}
		updatePointer();
	}

	public override void updatekey()
	{
		if (list != null)
		{
			if (GameCanvas.keyMove(1))
			{
				GameCanvas.ClearkeyMove(1);
				list.setToX(list.cmtoX - GameCanvas.hText);
			}
			else if (GameCanvas.keyMove(3))
			{
				GameCanvas.ClearkeyMove(3);
				list.setToX(list.cmtoX + GameCanvas.hText);
			}
		}
		if (type == TYPE_BOX_CHOICE)
		{
			if (GameCanvas.keyMove(0))
			{
				GameCanvas.ClearkeyMove(0);
				if (idSelect > 0)
				{
					idSelect--;
				}
				if (idSelect == -1)
				{
					idSelect = 0;
				}
			}
			else if (GameCanvas.keyMove(2))
			{
				GameCanvas.ClearkeyMove(2);
				if (idSelect < mItemgift.Length - 1)
				{
					idSelect++;
				}
			}
		}
		updatekeyCMD();
	}

	public override void updatePointer()
	{
		if (list != null)
		{
			list.update_Pos_UP_DOWN();
		}
		if (type == TYPE_BOX_CHOICE)
		{
			int num = yDia + wItemInterface + 10;
			for (int i = 0; i < mItemgift.Length; i++)
			{
				int x = xDia + 14 + i % 4 * wItem + 2;
				int y = num + i / 4 * wItem + 5;
				if (GameCanvas.isPointSelect(x, y, wItem, wItem))
				{
					idSelect = i;
				}
			}
		}
		base.updatePointer();
	}
}
