public class Sudo_Screen : ChatTabScreen
{
	public const sbyte SUBCMD_SUDO_NHAN = 0;

	public const sbyte SUBCMD_SUDO_HUY = 1;

	public const sbyte SUBCMD_SUDO_LIST = 2;

	public const sbyte SUBCMD_SUDO_INFO = 3;

	public const sbyte SUBCMD_SUDO_INVITE = 4;

	public const sbyte SUBCMD_SUDO_IS_ACTIVE = 5;

	public const sbyte TYPE_INFO_PLAYER_SEND_INVITE = 7;

	public const sbyte TYPE_INFO_PLAYER_CHAT = 8;

	public const sbyte TYPE_INFO_SUDO_CHAT = 9;

	public const sbyte TYPE_INFO_LEAVE_CLAN = 10;

	public const sbyte TYPE_INFO_REMOVE_CHAT = 11;

	public const sbyte TYPE_INFO_ADD_REMOVE_MEMBER = 12;

	public const sbyte SUDO_INFO_PLAYER = 13;

	public const sbyte CLIENT_TYPE_CHAT_SUDO = 15;

	public const sbyte CLIENT_TYPE_KICK = 16;

	public const sbyte CLIENT_TYPE_LEAVE = 17;

	public const sbyte CLIENT_TYPE_APPCEPT_VISIT_SUDO = 18;

	public const sbyte CLIENT_TYPE_VISIT_SUDO = 19;

	public const sbyte CLIENT_TYPE_CANCEL_VITSI_SUDO = 20;

	private int xbegintab;

	public static iCommand cmdClose;

	public MainSudo clan;

	public static InfoMemList memCur;

	public Sudo_Chat clanchat;

	public Sudo_Mem member;

	public Sudo_Info info;

	public Clan_Cup achi;

	public static bool isNew;

	private mVector vecCmd = new mVector();

	public Sudo_Screen(MainSudo clan)
	{
		this.clan = clan;
		w = 240;
		h = 220;
		idSelect = 0;
		if (w > MotherCanvas.w)
		{
			w = MotherCanvas.w;
		}
		if (h > MotherCanvas.h - GameCanvas.hCommand - 10)
		{
			h = MotherCanvas.h - GameCanvas.hCommand - 10;
		}
		x = MotherCanvas.hw - w / 2;
		if (GameCanvas.isTouch)
		{
			y = MotherCanvas.hh - h / 2;
		}
		else
		{
			y = MotherCanvas.hh - h / 2 - GameCanvas.hCommand / 2;
		}
		wItem = 24;
		xBe = x + wItem + miniItem;
		yBe = y + wItem + miniItem;
		hCon = h - wItem - miniItem - miniItem * 2;
		wCon = w - wItem * 2 - miniItem * 2;
		wPaintTab = (wCon + miniItem * 2) / 4;
		wCon = wPaintTab * 4 - miniItem * 2;
		hChat = hCon / GameCanvas.hText + 2;
		clanchat = new Sudo_Chat(T.chat, 3);
		clanchat.setPos(xBe, yBe, wCon, hCon, miniItem, hChat);
		member = new Sudo_Mem(T.anhem, 4);
		member.setPos(xBe, yBe, wCon, hCon, miniItem, hChat);
		vecTabChat.removeAllElements();
		vecTabChat.addElement(member);
		info = new Sudo_Info(T.info, 5, GameScreen.player.sudo);
		info.setPos(xBe, yBe, wCon, hCon, miniItem, hChat);
		vecTabChat.addElement(info);
		achi = new Clan_Cup(T.info, 5);
		achi.setPos(xBe, yBe, wCon, hCon, miniItem, hChat);
		xbegintab = x + w / 2 - vecTabChat.size() * wPaintTab / 2 - vecTabChat.size() % 2 * wPaintTab / 2;
		idSelect = 0;
		cmdClose = new iCommand(T.close, 5, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x + w - 13, y + 13, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			cmdClose = AvMain.setPosCMD(cmdClose, 2);
			right = cmdClose;
		}
		setAddCmd(null, -1);
		setData();
		backCMD = cmdClose;
		GlobalService.gI().Send_Sudo(2);
		GlobalService.gI().Send_Sudo(3);
	}

	public void setData()
	{
		if (clan != null)
		{
			member.vecDetail = clan.vecMem;
			info.clan = clan;
			clanchat.vecDetail = clan.vecChatCLan;
			achi.vecDetail = clan.vecAchi;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 5)
		{
			GameCanvas.gameScr.Show();
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		MainTab.paintPaperTab(g, x, y, w, h);
		AvMain.paintRect(g, xBe - miniItem, yBe - miniItem, wCon + miniItem * 2 - 1, hCon + miniItem * 2, 0, 4);
		g.setClip(x + wItem / 2, y, w - wItem, wItem + miniItem * 2);
		g.saveCanvas();
		g.ClipRec(x + wItem / 2, y, w - wItem, wItem + miniItem * 2);
		for (int i = 0; i < vecTabChat.size(); i++)
		{
			ChatDetail obj = (ChatDetail)vecTabChat.elementAt(i);
			int num = 0;
			if (i == idSelect)
			{
				num = 1;
			}
			g.drawRegion(AvMain.imgTabClan, 0, num * 19, 25, 19, 0, xBe - miniItem + i * wPaintTab, y + wItem - 18, 0);
			g.drawRegion(AvMain.imgTabClan, 0, num * 19, 25, 19, 2, xBe - miniItem + i * wPaintTab + wPaintTab - 25, y + wItem - 18, 0);
			if (i == 1)
			{
				AvMain.fraIconClan.drawFrame(8 + num, xBe - miniItem + i * wPaintTab + wPaintTab / 2, y + wItem / 2 + 3, 0, 3, g);
			}
			else
			{
				AvMain.fraIconClan.drawFrame((i + 1) * 2 + num, xBe - miniItem + i * wPaintTab + wPaintTab / 2, y + wItem / 2 + 3, 0, 3, g);
			}
			if (obj.isNew)
			{
				g.drawImage(MainEvent.imgNew, xBe - miniItem + i * wPaintTab + 5, y + 7, 0);
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (tabCur != null)
		{
			GameCanvas.resetTrans(g);
			tabCur.paint(g);
		}
		if (!GameCanvas.isDialogOrMenuShow())
		{
			for (int j = 0; j < vecCmd.size(); j++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		if (tabCur != null)
		{
			tabCur.update();
			if (tabCur.isNew)
			{
				tabCur.isNew = false;
			}
		}
		MainTab.CDDonateClan.updateTimeCountDownTicket();
	}

	public override void updatekey()
	{
		int num = idSelect;
		if (GameCanvas.keyMyHold[4])
		{
			GameCanvas.clearKeyHold(4);
			if (idSelect > 0)
			{
				idSelect--;
			}
		}
		else if (GameCanvas.keyMyHold[6])
		{
			GameCanvas.clearKeyHold(6);
			if (idSelect < vecTabChat.size() - 1)
			{
				idSelect++;
			}
		}
		if (num != idSelect)
		{
			getCurTab(idSelect);
		}
		if (tabCur != null)
		{
			tabCur.updatekey();
		}
		updateCmd();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				((iCommand)vecCmd.elementAt(i)).updatePointer();
			}
		}
		if (tabCur != null)
		{
			tabCur.updatePointer();
		}
		if (GameCanvas.isPointerSelect && GameCanvas.isPointer(xBe - miniItem, y + wItem - 24, wCon + miniItem * 2, 28))
		{
			int num = (GameCanvas.px - (xBe - miniItem)) / wPaintTab;
			if (num >= 0 && num < vecTabChat.size())
			{
				mSystem.outz("click tab " + num + " vecTabChat.size() " + vecTabChat.size());
				idSelect = num;
				getCurTab(idSelect);
			}
			GameCanvas.isPointerSelect = false;
		}
	}

	public override void getCurTab(int idSe)
	{
		idSelect = idSe;
		if (idSe >= 0 && idSe <= vecTabChat.size())
		{
			tabCur = (ChatDetail)vecTabChat.elementAt(idSe);
			setAddCmd(null, -1);
			tabCur.beginFocus();
		}
	}

	public void setAddCmd(iCommand cmd, sbyte pos)
	{
		vecCmd.removeAllElements();
		vecCmd.addElement(cmdClose);
		if (pos == 1)
		{
			left = cmd;
		}
		if (pos == 0)
		{
			center = cmd;
		}
	}

	public void setCMDTabCur(iCommand cmd, sbyte pos)
	{
	}

	public static InfoMemList getMemInCLan(short Id)
	{
		if (GameScreen.player.clan == null)
		{
			return null;
		}
		for (int i = 0; i < GameScreen.player.clan.vecMem.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)GameScreen.player.clan.vecMem.elementAt(i);
			if (infoMemList.id != -1 && infoMemList.id == Id)
			{
				return infoMemList;
			}
		}
		return null;
	}

	public static InfoMemList getMemInCLan(string name)
	{
		if (GameScreen.player.clan == null)
		{
			return null;
		}
		for (int i = 0; i < GameScreen.player.clan.vecMem.size(); i++)
		{
			InfoMemList infoMemList = (InfoMemList)GameScreen.player.clan.vecMem.elementAt(i);
			if (name.CompareTo(infoMemList.name) == 0)
			{
				return infoMemList;
			}
		}
		return null;
	}

	public static void getRemoveMemInCLan(string name)
	{
		if (GameScreen.player.clan == null)
		{
			return;
		}
		for (int i = 0; i < GameScreen.player.clan.vecMem.size(); i++)
		{
			if (((InfoMemList)GameScreen.player.clan.vecMem.elementAt(i)).name.CompareTo(name) == 0)
			{
				GameScreen.player.clan.vecMem.removeElementAt(i);
				break;
			}
		}
	}

	public void setIsNewTab(ChatDetail tab)
	{
		if (tabCur != null && tabCur != tab)
		{
			tab.setIsNew(isnew: true);
		}
		isNew = true;
	}
}
