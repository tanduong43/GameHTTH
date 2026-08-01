public class MsgOtherCharInfo : MsgDialog
{
	private MainObject obj;

	private int sizeBanner = 120;

	private int timeShowInfo;

	private int[][] mposEquip;

	private MainItem itemCur;

	private int xInfo;

	private int yInfo;

	private int ylechthanhtich;

	private int yLechChar;

	private bool isShowInfo;

	private bool isCheckTop;

	public static InfoMemList infoFight;

	private iCommand cmdfight;

	private int lastTick;

	private int framepaint;

	public MsgOtherCharInfo(MainObject obj)
	{
		this.obj = obj;
		cmdfight = null;
		if (obj == null)
		{
			return;
		}
		cmdList.removeAllElements();
		cmdClose = new iCommand(T.close, 1, this);
		cmdClose = AvMain.setPosCMD(cmdClose, 2);
		if (infoFight != null && infoFight.name.CompareTo(this.obj.name) == 0)
		{
			cmdfight = new iCommand(T.chapnhan, 2, this);
			cmdfight = AvMain.setPosCMD(cmdfight, 0);
			cmdfight = AvMain.setPosCMD(cmdfight, 1);
			left = cmdfight;
		}
		right = cmdClose;
		wDia = 160;
		yLechChar = 0;
		if (obj.hOne > 52)
		{
			yLechChar += obj.hOne - 52;
		}
		if (obj.indexFullSet > 0)
		{
			yLechChar += 15;
		}
		hDia = 140;
		if (yLechChar > 15)
		{
			hDia += yLechChar - 15;
		}
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2;
		wItem = 27;
		mposEquip = new int[TabEquip.maxEquip][];
		int num = yDia + wItem - 7 + 15;
		int num2 = xDia + wDia / 6 - wItem / 2;
		for (int i = 0; i < mposEquip.Length; i++)
		{
			mposEquip[i] = new int[2];
			switch (i)
			{
			case 8:
				mposEquip[i][0] = num2 + i % 2 * (wDia / 3 * 2 + 2) + wItem * 5 / 4;
				mposEquip[i][1] = num - wItem / 2 + i / 2 * wItem - wItem + wItem / 4;
				break;
			case 9:
				mposEquip[i][0] = num2 + i % 2 * (wDia / 3 * 2 + 2) - wItem * 5 / 4;
				mposEquip[i][1] = num - wItem / 2 + i / 2 * wItem - wItem + wItem / 4;
				break;
			default:
				mposEquip[i][0] = num2 + i % 2 * (wDia / 3 * 2 + 2);
				mposEquip[i][1] = num - wItem / 2 + i / 2 * wItem;
				break;
			}
		}
		if (GameCanvas.isTouch)
		{
			idSelect = -1;
			cmdClose.setPos(xDia + maxWShow / 2 + sizeBanner / 2, yDia - 20 + 8, MainTab.fraCloseTab, string.Empty);
			cmdList.addElement(cmdClose);
			if (cmdfight != null)
			{
				cmdfight = AvMain.setPosCMD(cmdfight, 0);
				cmdList.addElement(cmdfight);
			}
		}
		else
		{
			hDia += 10;
			isShowInfo = false;
			itemCur = (MainItem)obj.hashEquip.get(string.Empty + idSelect);
		}
		backCMD = cmdClose;
		isCheckTop = false;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			doMenu();
			infoFight = null;
			break;
		case 1:
			GameCanvas.end_Dialog();
			infoFight = null;
			break;
		case 2:
		{
			if (infoFight == null)
			{
				return;
			}
			GlobalService.gI().Fight(1, (short)infoFight.id, 0);
			if (GameCanvas.eventScr.vecPlayer == null)
			{
				return;
			}
			for (int i = 0; i < GameCanvas.eventScr.vecPlayer.size(); i++)
			{
				InfoMemList infoMemList = (InfoMemList)GameCanvas.eventScr.vecPlayer.elementAt(i);
				if (infoMemList == infoFight)
				{
					GameCanvas.eventScr.vecPlayer.removeElement(infoMemList);
					break;
				}
			}
			return;
		}
		}
		base.commandPointer(index, subIndex);
	}

	private void doMenu()
	{
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia + 4;
		int num2 = xDia + wDia / 2;
		paintPaper_UpDown(g, xDia - 5, yDia - 32, maxWShow + 10, hDia + 44, maxWShow + 10);
		g.setColor(15972174);
		g.fillRoundRect(xDia + wDia / 2 - sizeBanner / 2, yDia - 20, sizeBanner, 16, 4, 4);
		AvMain.FontBorderColor(g, obj.name, xDia + maxWShow / 2, yDia - 18, 2, 6, 5);
		g.setClip(MotherCanvas.hw - maxWShow / 2, yDia, maxWShow, hDia);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - maxWShow / 2, yDia, maxWShow, hDia);
		num += 4;
		if (obj.clan != null)
		{
			MainImage iconClan = Potion.getIconClan(obj.clan.idIcon);
			if (iconClan != null && iconClan.img != null)
			{
				int num3 = -mFont.tahoma_7b_black.getWidth(obj.clan.name) / 2;
				if (iconClan.frame == -1)
				{
					iconClan.set_Frame();
				}
				if (iconClan.frame <= 1)
				{
					g.drawImage(iconClan.img, xDia + wDia / 2 + num3, num, 3);
				}
				else
				{
					int num4 = ((framepaint < iconClan.frame - 1) ? 3 : 15);
					if (CRes.abs(GameCanvas.gameTick - lastTick) > num4)
					{
						framepaint++;
						if (framepaint >= iconClan.frame)
						{
							framepaint = 0;
						}
						lastTick = GameCanvas.gameTick;
					}
					g.drawRegion(iconClan.img, 0, framepaint * iconClan.w, iconClan.w, iconClan.w, 0, xDia + wDia / 2 + num3, num, 3);
				}
				num3 = 9;
				mFont.tahoma_7b_black.drawString(g, obj.clan.name, xDia + wDia / 2 + num3, num - 6, 2);
			}
		}
		num += 15;
		int num5 = 47;
		mImage image = ((obj.Lv != 100) ? Interface_Game.imgIconMPHP : Interface_Game.imgIconMPHP2);
		g.drawImage(image, num2 - num5 + 7, num, 0);
		Interface_Game.PaintHPMP(g, 1, obj.Hp, obj.maxHp, num2 - num5 + 18, num, 0, 9, 66, 0, isflip: false, 0, isUpdateEff: false, obj.lvHeart);
		num += 11;
		Interface_Game.PaintHPMP(g, 2, obj.Mp, obj.maxMp, num2 - num5 + 18, num, 0, 9, 66, 0, isflip: false, 0, isUpdateEff: false, 0);
		num += 9;
		int num6 = 0;
		if (obj.Lv == 100)
		{
			mFont.tahoma_7_black.drawString(g, obj.LvThongThao + " + " + obj.percentThongThao / 10 + "," + obj.percentThongThao % 10 + "%", num2 - num5 + 20, num, 0);
			num += 10;
			num6 = obj.percentThongThao / 10 * 70 / 100;
		}
		else
		{
			mFont.tahoma_7_black.drawString(g, obj.Lv + " + " + obj.percentLv / 10 + "," + obj.percentLv % 10 + "%", num2 - num5 + 20, num, 0);
			num += 10;
			num6 = obj.percentLv / 10 * 70 / 100;
		}
		g.setColor(1258003);
		g.fillRect(num2 - num5 + 18, num, 65, 2);
		if (num6 > 0)
		{
			g.setColor(3514158);
			g.fillRect(num2 - num5 + 18, num, num6, 2);
		}
		for (int i = 1; i < 5; i++)
		{
			g.setColor(16777215);
			g.fillRect(num2 - num5 + 18 + i * 13, num, 1, 2);
		}
		for (int j = 0; j < TabEquip.maxEquip; j++)
		{
			int index = 3;
			if (j < 8)
			{
				AvMain.paintRect(g, mposEquip[j][0] - 1, mposEquip[j][1] - 1, wItem - 2, wItem - 2, 0, index);
			}
			MainItem mainItem = (MainItem)obj.hashEquip.get(string.Empty + j);
			if (mainItem != null)
			{
				mainItem.paintColor(g, mposEquip[j][0] + wItem / 2 - 1, mposEquip[j][1] + wItem / 2 - 1, wItem - 3);
				mainItem.paint(g, mposEquip[j][0] + wItem / 2 - 1, mposEquip[j][1] + wItem / 2 - 1, wItem, 1);
			}
			if (idSelect == j)
			{
				g.setColor(16777215);
				g.drawRect(mposEquip[j][0], mposEquip[j][1], wItem - 4, wItem - 4);
				g.drawRect(mposEquip[j][0] + 1, mposEquip[j][1] + 1, wItem - 6, wItem - 6);
			}
		}
		obj.paintThanhTich(g, num + 26 + yLechChar, xDia + wDia / 2);
		num += 50 + yLechChar;
		obj.paintCharShow(g, xDia + wDia / 2, num, 0, isNhip: true);
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (cmdList != null)
		{
			for (int k = 0; k < cmdList.size(); k++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(k);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		paintCmd(g);
		if (isShowInfo && itemCur != null)
		{
			MainTab.paintInfoEveryWhere(g, itemCur, null, 0, xInfo, yInfo, itemCur.wInfo, itemCur.hInfo, isLv: false, obj, 0);
		}
	}

	public override void update()
	{
		if (itemCur != null && !isShowInfo)
		{
			timeShowInfo++;
			if (timeShowInfo >= 10)
			{
				isShowInfo = true;
				setPosInfo(itemCur, mposEquip[idSelect][0] + wItem / 2, mposEquip[idSelect][1] + wItem);
			}
		}
		if (!isCheckTop)
		{
			isCheckTop = true;
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject != null && !mainObject.isRemove && mainObject.typeObject == 0 && mainObject.name.CompareTo(obj.name) == 0)
				{
					obj.thanhtichLv = mainObject.thanhtichLv;
					obj.thanhtichPvP = mainObject.thanhtichPvP;
					if (obj.thanhtichLv >= 0)
					{
						ylechthanhtich += 15;
					}
					if (obj.thanhtichPvP >= 0)
					{
						ylechthanhtich += 15;
					}
					if (ylechthanhtich > yLechChar)
					{
						yLechChar = ylechthanhtich;
					}
					return;
				}
			}
		}
		base.update();
	}

	public override void updatekey()
	{
		bool flag = false;
		int num = idSelect;
		if (idSelect == -1 && (GameCanvas.keyMove(0) || GameCanvas.keyMove(2) || GameCanvas.keyMove(1) || GameCanvas.keyMove(3)))
		{
			idSelect = 0;
			GameCanvas.clearKeyHold();
			flag = true;
		}
		if (GameCanvas.keyMove(0))
		{
			idSelect--;
			GameCanvas.ClearkeyMove(0);
			flag = true;
		}
		else if (GameCanvas.keyMove(2))
		{
			idSelect++;
			GameCanvas.ClearkeyMove(2);
			flag = true;
		}
		else if (GameCanvas.keyMove(1))
		{
			if (idSelect >= 2)
			{
				idSelect -= 2;
			}
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			if (idSelect < TabEquip.maxEquip - 2)
			{
				idSelect += 2;
			}
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (flag)
		{
			if (idSelect == 8)
			{
				MainItem mainItem = (MainItem)obj.hashEquip.get(string.Empty + idSelect);
				if (mainItem == null)
				{
					mainItem = (MainItem)obj.hashEquip.get(string.Empty + 9);
					if (mainItem == null)
					{
						idSelect = num;
						return;
					}
					idSelect = 9;
				}
			}
			else if (idSelect == 9)
			{
				MainItem mainItem2 = (MainItem)obj.hashEquip.get(string.Empty + idSelect);
				if (mainItem2 == null)
				{
					mainItem2 = (MainItem)obj.hashEquip.get(string.Empty + 8);
					if (mainItem2 == null)
					{
						idSelect = num;
						return;
					}
					idSelect = 8;
				}
			}
			if (idSelect < 0 || idSelect >= TabEquip.maxEquip)
			{
				idSelect = -1;
				isShowInfo = false;
				itemCur = null;
			}
			else if (num != idSelect)
			{
				isShowInfo = false;
				itemCur = (MainItem)obj.hashEquip.get(string.Empty + idSelect);
			}
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		if (cmdList != null)
		{
			for (int i = 0; i < cmdList.size(); i++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		if (GameCanvas.isPointerSelect)
		{
			bool flag = true;
			for (int j = 0; j < TabEquip.maxEquip; j++)
			{
				if (j == 8 || j == 9)
				{
					MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + j);
					if (mainItem == null)
					{
						GameCanvas.isPointerSelect = false;
						break;
					}
				}
				if (GameCanvas.isPointSelect(mposEquip[j][0] - 2, mposEquip[j][1] - 2, wItem, wItem))
				{
					flag = false;
					if (j != idSelect)
					{
						idSelect = j;
						itemCur = (MainItem)obj.hashEquip.get(string.Empty + idSelect);
						isShowInfo = false;
					}
					GameCanvas.isPointerSelect = false;
					break;
				}
			}
			if (flag)
			{
				itemCur = null;
				isShowInfo = false;
				idSelect = -1;
			}
		}
		base.updatePointer();
	}

	public void setPosInfo(MainItem item, int xbe, int ybe)
	{
		int num = 100;
		int num2 = 40;
		if (item != null)
		{
			num = item.wInfo;
			num2 = item.hInfo;
		}
		xInfo = xbe - num / 2;
		if (xInfo + num > MotherCanvas.w - 8)
		{
			xInfo = MotherCanvas.w - num - 8;
		}
		if (xInfo < 8)
		{
			xInfo = 8;
		}
		yInfo = ybe;
		if (yInfo + num2 > MotherCanvas.h - GameCanvas.hCommand - 8)
		{
			yInfo = MotherCanvas.h - GameCanvas.hCommand - num2 - 8;
		}
		if (yInfo < 8)
		{
			yInfo = 8;
		}
	}
}
