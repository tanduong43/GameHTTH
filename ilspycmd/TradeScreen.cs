using System;

public class TradeScreen : ScreenUpgrade
{
	public MainObject objMain;

	public MainObject objTrade;

	private int[][] posName_Money;

	private iCommand cmdTradeMoney;

	private iCommand cmdLock;

	private iCommand cmdTrade;

	private iCommand cmdChat;

	private iCommand cmdCancel;

	private new iCommand cmdMenu;

	private InputDialog input;

	private int xtest;

	private int ytest;

	public new static TradeScreen instance;

	public TradeScreen(sbyte typeRebuild, int size)
		: base(typeRebuild, size)
	{
	}

	public override void setStart(sbyte typeRe, int size)
	{
		if (wItem * 8 > w - 30)
		{
			wItem = (w - 30) / 8;
		}
		wInven -= wInven % wItem;
		xtest = x + wInven + (w - wInven) / 2 - (wItem + 2) * 2 + 5;
		ytest = yInven;
		posName_Money = mSystem.new_M_Int(4, 2);
		posName_Money[0][0] = xtest;
		posName_Money[0][1] = ytest;
		posName_Money[1][0] = xtest;
		posName_Money[1][1] = ytest + 18 + wItem + 1;
		posName_Money[2][0] = xtest;
		posName_Money[2][1] = ytest + 18 + wItem + 13;
		posName_Money[3][0] = xtest;
		posName_Money[3][1] = ytest + 18 + wItem + 15 + wItem + 18;
		posUp = mSystem.new_M_Int(8, 2);
		for (int i = 0; i < posUp.Length; i++)
		{
			posUp[i][0] = xtest + i % 4 * (wItem + 2);
			posUp[i][1] = ytest + 15 + i / 4 * (30 + wItem);
		}
		maxNumItemW = wInven / wItem;
		int limX = ((Player.maxInventory - 1) / maxNumItemW + 1) * wItem - hInven;
		list = new ListNew(xInven, yInven, wInven, hInven, 0, 0, limX, isLim0: true);
		objMain = new MainObject();
		objMain.name = GameScreen.player.name;
		if (objMain.name.Length > 12)
		{
			objMain.name = GameScreen.player.name.Substring(0, 8) + "...";
		}
		objTrade = new MainObject();
		input = new InputDialog();
		cmdBovao = new iCommand(T.bovao, 0, this);
		cmdLock = new iCommand(T.strlock, 1, this);
		cmdTradeMoney = new iCommand(T.tradeMoney, 2, this);
		cmdTrade = new iCommand(T.trade, 4, this);
		cmdCancel = new iCommand(T.cancel, 5, this);
		cmdChat = new iCommand(T.TroChuyen, 6, this);
		cmdMenu = new iCommand(T.menu, 8, this);
		cmdClose = new iCommand(T.close, -1, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x + w - 15, y - 15 + 10 + 8, MainTab.fraCloseTab, string.Empty);
			cmdLock.setPos(x + wInven + (w - wInven) / 2 - 4 + iCommand.wButtonCmd / 2 + 7, y + h - iCommand.hButtonCmdNor / 2 - 5, null, cmdLock.caption);
			cmdTradeMoney.setPos(x + wInven + (w - wInven) / 2 - iCommand.wButtonCmd / 2 + 4 + 7, y + h - iCommand.hButtonCmdNor / 2 - 5, null, cmdTradeMoney.caption);
			vecCmd.addElement(cmdLock);
			vecCmd.addElement(cmdTradeMoney);
			cmdCancel.setPos(x + wInven + (w - wInven) / 2 + iCommand.wButtonCmd / 2 - 4, y + h - iCommand.hButtonCmdNor / 2 - 5, null, cmdCancel.caption);
			cmdTrade.setPos(x + wInven + (w - wInven) / 2 - iCommand.wButtonCmd / 2 + 4 + 5, y + h - iCommand.hButtonCmdNor / 2 - 5, null, cmdTrade.caption);
			int width = mFont.tahoma_7b_black.getWidth(objMain.name);
			cmdChat.setPos(xtest + width + 13, ytest + 4, Interface_Game.imgOther[1], string.Empty, 2);
			vecCmd.addElement(cmdChat);
			setSmallCmd();
		}
		ScreenUpgrade.mItemUpgrade = new MainItem[posUp.Length];
		nameScreen = T.trade;
		if (!GameCanvas.isTouch)
		{
			right = cmdClose;
			left = cmdBovao;
		}
		menuCMD = cmdBovao;
		okCMD = cmdMenu;
		backCMD = cmdClose;
	}

	private void setSmallCmd()
	{
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			iCommand2.levelSmall = 1;
		}
	}

	public void setNameObjTrade(string name)
	{
		objTrade.name = name;
		indexAreaSellect = 0;
		if (GameCanvas.isTouchNoOrPC())
		{
			IdSelect = 0;
		}
		else
		{
			IdSelect = -1;
		}
		if (IdSelect >= 0)
		{
			setPosCmd(getMenuActionItem());
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			GlobalService.gI().Trade(5, 0, 0, 1, string.Empty);
			break;
		case 0:
			if (itemCur != null)
			{
				if (itemCur.typeObject == 3 || setCurrentInTrade(itemCur))
				{
					GlobalService.gI().Trade(1, itemCur.ID, itemCur.typeObject, 1, string.Empty);
					break;
				}
				input.setinfo(T.nhapsoluongmuongiaodich, new iCommand(T.strconfirm, 3, 1, this), isNum: true, T.vatpham);
				GameCanvas.currentDialog = input;
			}
			break;
		case 1:
			GlobalService.gI().Trade(3, 0, 0, 1, string.Empty);
			break;
		case 2:
			input.setinfo(T.nhapsotien, new iCommand(T.strconfirm, 3, 0, this), isNum: true, T.tradeMoney);
			GameCanvas.currentDialog = input;
			break;
		case 3:
		{
			int num = 0;
			try
			{
				num = int.Parse(input.tfInput.getText());
				if (num < 0)
				{
					num = 0;
				}
			}
			catch (Exception)
			{
				num = 0;
			}
			switch (subIndex)
			{
			case 0:
				GlobalService.gI().Trade(1, 0, 6, num, string.Empty);
				break;
			case 1:
				GlobalService.gI().Trade(1, itemCur.ID, itemCur.typeObject, num, string.Empty);
				break;
			}
			GameCanvas.end_Dialog();
			input = new InputDialog();
			break;
		}
		case 4:
			GlobalService.gI().Trade(4, 0, 0, 1, string.Empty);
			break;
		case 5:
			GlobalService.gI().Trade(5, 0, 0, 1, string.Empty);
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			break;
		case 6:
			input.setinfo(T.nhapNoiDung, new iCommand(T.chat, 7, this), isNum: false, T.TroChuyen);
			GameCanvas.currentDialog = input;
			break;
		case 7:
		{
			string text = input.tfInput.getText();
			if (text != null && text.Length > 0)
			{
				GlobalService.gI().Trade(2, 0, 0, 1, text);
				objMain.strChatPopup = text;
			}
			GameCanvas.end_Dialog();
			input = new InputDialog();
			break;
		}
		case 8:
			getMenu();
			break;
		}
	}

	public override void paintTrade(mGraphics g)
	{
		mFont.tahoma_7b_black.drawString(g, objMain.name, posName_Money[0][0], posName_Money[0][1], 0);
		AvMain.fraMoney.drawFrame(0, posName_Money[1][0] + AvMain.fraMoney.frameWidth / 2, posName_Money[1][1] + 5, 0, 3, g);
		mFont.tahoma_7_black.drawString(g, AvMain.getDotNumber(objMain.beli), posName_Money[1][0] + AvMain.fraMoney.frameWidth + 1, posName_Money[1][1], 0);
		mFont.tahoma_7b_black.drawString(g, objTrade.name, posName_Money[2][0], posName_Money[2][1], 0);
		AvMain.fraMoney.drawFrame(0, posName_Money[3][0] + AvMain.fraMoney.frameWidth / 2, posName_Money[3][1] + 5, 0, 3, g);
		mFont.tahoma_7_black.drawString(g, AvMain.getDotNumber(objTrade.beli), posName_Money[3][0] + AvMain.fraMoney.frameWidth + 1, posName_Money[3][1], 0);
		if (objMain.isTrade > 0)
		{
			g.setColor(16711680);
			g.drawRect(xtest - 2, ytest - 2 + 15, (wItem + 2) * 4 + 4, wItem + 4);
		}
		if (objMain.isTrade == 2)
		{
			g.setColor(255);
			g.drawRect(xtest - 3, ytest - 3 + 15, (wItem + 2) * 4 + 6, wItem + 6);
		}
		if (objTrade.isTrade > 0)
		{
			g.setColor(16711680);
			g.drawRect(xtest - 2, ytest - 2 + 45 + wItem, (wItem + 2) * 4 + 4, wItem + 4);
		}
		if (objTrade.isTrade == 2)
		{
			g.setColor(255);
			g.drawRect(xtest - 3, ytest - 3 + 45 + wItem, (wItem + 2) * 4 + 6, wItem + 6);
		}
	}

	public override void paintPosItem(mGraphics g)
	{
		for (int i = 0; i < posUp.Length; i++)
		{
			AvMain.paintRect(g, posUp[i][0], posUp[i][1], wItem, wItem, 1, 3);
		}
		for (int j = 0; j < objMain.vecTrade.size(); j++)
		{
			MainItem mainItem = (MainItem)objMain.vecTrade.elementAt(j);
			if (mainItem.typeObject == 3)
			{
				mainItem.paintColor(g, posUp[j][0] + wItem / 2, posUp[j][1] + wItem / 2, wItem);
			}
			mainItem.paint(g, posUp[j][0] + wItem / 2, posUp[j][1] + wItem / 2, wItem);
		}
		for (int k = 0; k < objTrade.vecTrade.size(); k++)
		{
			MainItem mainItem2 = (MainItem)objTrade.vecTrade.elementAt(k);
			if (mainItem2.typeObject == 3)
			{
				mainItem2.paintColor(g, posUp[4 + k][0] + wItem / 2, posUp[4 + k][1] + wItem / 2, wItem);
			}
			mainItem2.paint(g, posUp[4 + k][0] + wItem / 2, posUp[4 + k][1] + wItem / 2, wItem);
		}
		if (indexAreaSellect != 1)
		{
			return;
		}
		for (int l = 0; l < posUp.Length; l++)
		{
			if (IdSelect == l)
			{
				g.setColor(16777215);
				g.drawRect(posUp[l][0] + 1, posUp[l][1] + 1, wItem - 2, wItem - 2);
				if (!GameCanvas.isSmallScreen)
				{
					g.drawRect(posUp[l][0] + 2, posUp[l][1] + 2, wItem - 4, wItem - 4);
				}
			}
		}
	}

	public override void paintChat(mGraphics g)
	{
		if (objMain.chat != null)
		{
			objMain.chat.paint(g);
		}
		if (objTrade.chat != null)
		{
			objTrade.chat.paint(g);
		}
	}

	public override void paintIconUpgrade(mGraphics g, int x, int y, MainItem item)
	{
		if (setCurrentInTrade(item))
		{
			if (AvMain.imgTrade2 == null)
			{
				AvMain.imgTrade2 = mImage.createImage("/interface/icontrade2.png");
			}
			else
			{
				g.drawImage(AvMain.imgTrade2, x, y, mGraphics.BOTTOM | mGraphics.LEFT);
			}
		}
	}

	public override void update()
	{
		base.update();
		updateChatPopup(objMain);
		updateChatPopup(objTrade);
		if (GameCanvas.isTouch && !GameCanvas.menu.isShowMenu && cmdChat.caption.Length > 0)
		{
			cmdChat.caption = string.Empty;
		}
	}

	public override void updatekey()
	{
		bool flag = false;
		if (indexAreaSellect == 0)
		{
			if (GameCanvas.keyMove(0))
			{
				IdSelect--;
				GameCanvas.ClearkeyMove(0);
				flag = true;
			}
			else if (GameCanvas.keyMove(2))
			{
				if (IdSelect % maxNumItemW == maxNumItemW - 1 || IdSelect >= Player.vecInventory.size() - 1)
				{
					indexAreaSellect = 1;
					IdSelect = 0;
				}
				else
				{
					IdSelect++;
				}
				GameCanvas.ClearkeyMove(2);
				flag = true;
			}
			else if (GameCanvas.keyMove(1))
			{
				if (IdSelect >= maxNumItemW)
				{
					IdSelect -= maxNumItemW;
				}
				GameCanvas.ClearkeyMove(1);
				flag = true;
			}
			else if (GameCanvas.keyMove(3))
			{
				if (IdSelect < Player.vecInventory.size() - maxNumItemW)
				{
					IdSelect += maxNumItemW;
				}
				GameCanvas.ClearkeyMove(3);
				flag = true;
			}
		}
		else if (indexAreaSellect == 1)
		{
			if (GameCanvas.keyMove(0))
			{
				if (IdSelect % 4 == 0)
				{
					indexAreaSellect = 0;
					IdSelect = 0;
				}
				else
				{
					IdSelect--;
				}
				GameCanvas.ClearkeyMove(0);
				flag = true;
			}
			else if (GameCanvas.keyMove(2))
			{
				if (IdSelect % 4 < 3)
				{
					IdSelect++;
					flag = true;
				}
				GameCanvas.ClearkeyMove(2);
			}
			else if (GameCanvas.keyMove(1))
			{
				if (IdSelect >= 4)
				{
					IdSelect -= 4;
				}
				GameCanvas.ClearkeyMove(1);
				flag = true;
			}
			else if (GameCanvas.keyMove(3))
			{
				if (IdSelect < 4)
				{
					IdSelect += 4;
				}
				GameCanvas.ClearkeyMove(3);
				flag = true;
			}
		}
		if (flag)
		{
			getItemCurNew();
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		if (GameCanvas.isPointSelect(xInven, yInven, wInven, hInven))
		{
			indexAreaSellect = 0;
		}
		else
		{
			for (int i = 0; i < posUp.Length; i++)
			{
				if (GameCanvas.isPointSelect(posUp[i][0], posUp[i][1], wItem, wItem))
				{
					indexAreaSellect = 1;
					IdSelect = i;
					getItemCurNew();
					break;
				}
			}
		}
		base.updatePointer();
	}

	public override void getItemCurNew()
	{
		isShowInfo = false;
		if (indexAreaSellect == 0)
		{
			IdSelect = AvMain.resetSelect(IdSelect, Player.vecInventory.size() - 1, isreset: false);
			if (!GameCanvas.isTouch)
			{
				list.setToX((IdSelect / maxNumItemW + 1) * wItem - h / 2);
			}
		}
		if (IdSelect >= 0)
		{
			setPosCmd(getMenuActionItem());
		}
	}

	public override mVector getMenuActionItem()
	{
		mVector result = null;
		if (indexAreaSellect == 0)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(IdSelect);
			if (mainItem != null)
			{
				itemCur = mainItem;
			}
			if (itemCur != null)
			{
				cmdBovao.caption = T.bovao;
				if (setCurrentInTrade(itemCur))
				{
					cmdBovao.caption = T.layra;
				}
				result = itemCur.getActionTrade();
			}
		}
		else if (indexAreaSellect == 1)
		{
			itemCur = null;
			if (IdSelect < 4)
			{
				if (IdSelect < objMain.vecTrade.size())
				{
					MainItem mainItem2 = (MainItem)objMain.vecTrade.elementAt(IdSelect);
					if (mainItem2 != null)
					{
						itemCur = mainItem2;
					}
				}
			}
			else if (IdSelect < 8 && IdSelect - 4 < objTrade.vecTrade.size())
			{
				MainItem mainItem3 = (MainItem)objTrade.vecTrade.elementAt(IdSelect - 4);
				if (mainItem3 != null)
				{
					itemCur = mainItem3;
				}
			}
		}
		return result;
	}

	public override void setPosCmd(mVector vec)
	{
		left = null;
		center = null;
		vecCmd.removeAllElements();
		if (vec != null)
		{
			if (GameCanvas.isTouch)
			{
				vecCmd = vec;
				for (int i = 0; i < vecCmd.size(); i++)
				{
					iCommand cmd = (iCommand)vecCmd.elementAt(i);
					if (i == 0)
					{
						cmd = AvMain.setPosCMD(cmd, 1);
					}
					if (i == 1)
					{
						cmd = AvMain.setPosCMD(cmd, 2);
					}
				}
			}
			else
			{
				for (int j = 0; j < vec.size(); j++)
				{
					iCommand iCommand2 = (iCommand)vec.elementAt(j);
					if (j == 0)
					{
						center = iCommand2;
					}
					if (j == 1)
					{
						left = iCommand2;
					}
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			vecCmd.addElement(cmdClose);
			vecCmd.addElement(cmdChat);
			if (objMain.isTrade == 0)
			{
				vecCmd.addElement(cmdLock);
				vecCmd.addElement(cmdTradeMoney);
			}
			else if (objMain.isTrade == 1)
			{
				vecCmd.addElement(cmdTrade);
				vecCmd.addElement(cmdCancel);
			}
			else if (objMain.isTrade == 2)
			{
				vecCmd.addElement(cmdCancel);
			}
		}
		else
		{
			right = cmdClose;
			left = cmdMenu;
		}
		setSmallCmd();
	}

	public override bool isGetItemUpgrade(short Id, sbyte cat)
	{
		return false;
	}

	public override void updateNewUpgrade()
	{
		isShowInfo = false;
		getItemCurNew();
	}

	public override void setStep()
	{
	}

	public bool setCurrentInTrade(MainItem item)
	{
		for (int i = 0; i < objMain.vecTrade.size(); i++)
		{
			MainItem mainItem = (MainItem)objMain.vecTrade.elementAt(i);
			if (mainItem.ID == item.ID && mainItem.typeObject == item.typeObject)
			{
				return true;
			}
		}
		return false;
	}

	public void setItem(sbyte ai, sbyte action, MainItem item)
	{
		MainObject mainObject = ((ai != 0) ? objTrade : objMain);
		if (action == 0)
		{
			for (int i = 0; i < mainObject.vecTrade.size(); i++)
			{
				MainItem mainItem = (MainItem)mainObject.vecTrade.elementAt(i);
				if (mainItem.ID == item.ID && mainItem.typeObject == item.typeObject)
				{
					mainObject.vecTrade.removeElement(mainItem);
					i--;
				}
			}
		}
		else if (action == 1)
		{
			mainObject.vecTrade.addElement(item);
		}
		if (ai == 0)
		{
			updateNewUpgrade();
		}
	}

	public void setLock()
	{
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			if (iCommand2 != cmdClose)
			{
				vecCmd.removeElement(iCommand2);
				i--;
			}
		}
		vecCmd.addElement(cmdTrade);
		vecCmd.addElement(cmdCancel);
		if (GameCanvas.isTouch)
		{
			vecCmd.addElement(cmdChat);
		}
		setSmallCmd();
	}

	public void setTrade()
	{
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			if (iCommand2 != cmdClose)
			{
				vecCmd.removeElement(iCommand2);
				i--;
			}
		}
		cmdCancel.setPos(x + wInven + (w - wInven) / 2, y + h - iCommand.hButtonCmdNor / 2 - 5, null, cmdCancel.caption);
		vecCmd.addElement(cmdCancel);
		if (GameCanvas.isTouch)
		{
			vecCmd.addElement(cmdChat);
		}
		setSmallCmd();
	}

	public void getMenu()
	{
		mVector mVector2 = new mVector();
		cmdChat.caption = T.TroChuyen;
		mVector2.addElement(cmdChat);
		if (objMain.isTrade == 0)
		{
			mVector2.addElement(cmdTradeMoney);
			mVector2.addElement(cmdLock);
		}
		else if (objMain.isTrade == 1)
		{
			mVector2.addElement(cmdTrade);
			mVector2.addElement(cmdCancel);
		}
		else if (objMain.isTrade == 2)
		{
			mVector2.addElement(cmdCancel);
		}
		GameCanvas.menu.startAt(mVector2, 2, T.cmdChucNang);
	}

	public void addChat(string str, MainObject obj)
	{
		if (obj.chat == null)
		{
			obj.chat = new PopupChat();
		}
		obj.chat.setChat(str, isStop: true);
		if (obj == objMain)
		{
			obj.chat.updatePos(xtest + obj.chat.w / 2, posName_Money[0][1] - 5);
		}
		else
		{
			obj.chat.updatePos(xtest + obj.chat.w / 2, posName_Money[2][1] - 5);
		}
	}

	public void updateChatPopup(MainObject obj)
	{
		if (obj.strChatPopup != null)
		{
			addChat(obj.strChatPopup, obj);
			obj.strChatPopup = null;
		}
		if (obj.chat != null && obj.chat.setOff())
		{
			obj.chat = null;
		}
	}

	public override void setPosInfo()
	{
		if (indexAreaSellect == 0)
		{
			setPosInfo(itemCur, xInven, yInven + (IdSelect / maxNumItemW + 1) * wItem - list.cmx + 4);
		}
		else if (indexAreaSellect == 1)
		{
			setPosInfo(itemCur, xInven + wInven - itemCur.wInfo, yInven + 15 + IdSelect / 4 * (30 + wItem) - list.cmx + 4);
		}
	}

	public override void paintlockTrade(mGraphics g, MainItem item, int x, int y)
	{
		if (item.isTrade != 0)
		{
			g.drawRegion(AvMain.imgDelay, 0, 0, wItem, wItem, 0, x, y, 3);
		}
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdClose != null)
		{
			cmdClose.perform();
		}
		return false;
	}
}
