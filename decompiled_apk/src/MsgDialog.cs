using System;

public class MsgDialog : MainDialog
{
	public const sbyte CMD_CLOSE = -1;

	public const sbyte CMD_DOWNLOAD = 0;

	public const sbyte CMD_CANCEL_QUEST = 1;

	public const sbyte CMD_CLOSE_QUEST = 2;

	public const sbyte CMD_CHANGE_EQUIP = 3;

	public const sbyte CMD_OK_AUTO_MP_HP = 4;

	public const sbyte CMD_LEARN_SKILL = 5;

	public const sbyte CMD_OK_AUTO_GET_ITEM = 6;

	public const sbyte CMD_JOIN_ITEM = 7;

	public const sbyte CMD_JOIN_ITEM_OK = 8;

	public const sbyte CMD_OK_AUTO_FIRE = 9;

	public const sbyte CMD_CLOSE_GIFT = 10;

	public const sbyte CMD_OPEN_NEXT = 11;

	public const sbyte CMD_CLOSE_SOUND = 12;

	public const sbyte CMD_REWARD = 13;

	public const sbyte CMD_DAILY_LOGIN = 14;

	public const sbyte CMD_CLOSE_MSG_SPAM = 15;

	public const sbyte CMD_CONFIRM_DOI_QUA = 16;

	public int idCommand;

	public const sbyte NORMAL = 0;

	public const sbyte WAITING = 1;

	public const sbyte QUEST = 2;

	public const sbyte SKILL_INFO = 4;

	public const sbyte NORMAL_2 = 5;

	public const sbyte TIME = 6;

	public const sbyte NORMAL_ITEM = 7;

	public const sbyte WAITING_CONNECT = 8;

	public const sbyte RE_CONNECT = 9;

	public const sbyte INFO_CLAN = 10;

	public mVector cmdList = new mVector();

	public static mVector vecEff = new mVector("MsgDialog.vecEff");

	public mVector vecEffUni = new mVector("MsgShowGift.vecEffUni");

	public iCommand cmdClose;

	public iCommand cmdChangeEquip;

	public mFont fontDia = mFont.tahoma_7_black;

	public static FrameImage fraImgWaiting;

	public static FrameImage fraAutoMpHp;

	public int fWait;

	public int wItem;

	public int idSelect;

	public static int hPlus;

	public MainQuest quest;

	public bool isClose;

	public bool isNewShop;

	public ListNew list = new ListNew();

	public string nameDialog = string.Empty;

	public static int xEFF;

	public static int yEFF;

	public string infoHelp = string.Empty;

	public int wInfoHelp;

	public int tickCloseInfoHelp;

	private CountDownTicket timeDialog;

	public mVector vecItem;

	public static bool isAuroReconect = false;

	private MainClan clanpaint;

	private string linkDownLoad = string.Empty;

	public int wShowPaper;

	public int maxWShow = 160;

	public int vShow = 6;

	private Skill_Info skill;

	private int[] posValueName;

	public int xBegin;

	public int w2cmd;

	public int miniItem = 5;

	private int lastTick;

	private int framepaint;

	public override void commandPointer(int index, int subIndex)
	{
		GameCanvas.clearAll();
		switch (index)
		{
		case -1:
			GameCanvas.end_Dialog();
			break;
		case 0:
			if (linkDownLoad.Length > 0)
			{
				GameMidlet.openUrl(linkDownLoad);
			}
			break;
		case 1:
			if (quest != null)
			{
				GlobalService.gI().quest(2, quest.ID);
				GameCanvas.end_Dialog();
			}
			break;
		case 2:
			isClose = true;
			if (type == 2 && GameScreen.indexHelp == 18)
			{
				MainHelp.setNextHelp(isHanhTrang: false);
			}
			break;
		case 5:
			if (skill != null)
			{
				GlobalService.gI().Learn_Skill(0, skill.indexSkillInServer);
				GameCanvas.end_Dialog();
			}
			break;
		case 10:
			GameCanvas.end_Cur_Dialog();
			if (Player.questMainNew != null && Player.idNPCQuestCur == Player.questMainNew.idNPC)
			{
				GameCanvas.menuCur.isShowMenu = false;
				GameCanvas.menuCur.runText = null;
				Player.questMainNew.beginQuest((short)Player.questMainNew.idNPC);
				Player.idNPCQuestCur = -1;
			}
			break;
		case 11:
			if (MsgShowGift.gift != null)
			{
				GlobalService.gI().Use_Potion(MsgShowGift.gift.ID);
			}
			GameCanvas.end_Dialog();
			break;
		case 14:
			GlobalService.gI().Daily_Login((sbyte)subIndex);
			break;
		case 15:
			GameCanvas.end_Dialog();
			MsgSpamSetup.saveClose();
			break;
		case 3:
		case 4:
		case 6:
		case 7:
		case 8:
		case 9:
		case 12:
		case 13:
			break;
		}
	}

	public void beginDia()
	{
		left = null;
		right = null;
		center = null;
		isNewShop = false;
		cmdList.removeAllElements();
		vecEff.removeAllElements();
		if (GameScreen.player != null)
		{
			GameScreen.player.resetAction();
		}
	}

	public void setinfoDownload(string info, string link)
	{
		linkDownLoad = link;
		mVector mVector2 = new mVector();
		mVector2.addElement(new iCommand(T.download, 0, this));
		setinfo(info, mVector2, isCmdClose: true, 0);
	}

	public void setinfo(string info, mVector vecCmd, bool isCmdClose, sbyte type)
	{
		beginDia();
		base.type = type;
		if (type == 9 && Player.StepAutoRe != 6)
		{
			isAuroReconect = true;
		}
		if (vecCmd != null)
		{
			cmdList = vecCmd;
		}
		else
		{
			cmdList = new mVector();
		}
		if (isCmdClose)
		{
			cmdClose = new iCommand(T.close, 2, this);
			cmdList.addElement(cmdClose);
			backCMD = cmdClose;
		}
		else
		{
			isBack = false;
		}
		if (cmdList.size() == 0)
		{
			GameCanvas.end_Dialog();
			return;
		}
		wDia = MotherCanvas.w - 30;
		if (wDia > 200)
		{
			wDia = 200;
		}
		maxWShow = wDia;
		if (GameCanvas.currentDialog == null)
		{
			wShowPaper = 5;
		}
		else
		{
			wShowPaper = maxWShow;
		}
		int num = cmdList.size();
		strinfo = fontDia.splitFontArray(info, wDia - 20);
		hDia = GameCanvas.hText * strinfo.Length + hPlus + ((num - 1) / 2 + 1) * (iCommand.hButtonCmdNor + 5);
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia - 5;
		setPosCmdNew(0, isLast: false);
	}

	public void setinfoItem(string info, mVector vecitem, mVector vecCmd, bool isCmdClose)
	{
		beginDia();
		type = 7;
		isNewShop = true;
		vecItem = vecitem;
		if (vecCmd != null)
		{
			cmdList = vecCmd;
		}
		else
		{
			cmdList = new mVector();
		}
		if (isCmdClose)
		{
			cmdClose = new iCommand(T.close, 2, this);
			cmdList.addElement(cmdClose);
			backCMD = cmdClose;
		}
		else
		{
			isBack = false;
		}
		if (cmdList.size() == 0)
		{
			GameCanvas.end_Dialog();
			return;
		}
		wItem = 22;
		wDia = MotherCanvas.w - 30;
		if (wDia > 180)
		{
			wDia = 180;
		}
		maxWShow = wDia;
		if (GameCanvas.currentDialog == null)
		{
			wShowPaper = 5;
		}
		else
		{
			wShowPaper = maxWShow;
		}
		int num = cmdList.size();
		strinfo = fontDia.splitFontArray(info, wDia - 20);
		hDia = GameCanvas.hText * strinfo.Length + (vecItem.size() - 1) * wItem + (((num - 1) / 2 + 1) * (iCommand.hButtonCmdNor + 5) + 5);
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia - 5;
		setPosCmdNew(0, isLast: false);
	}

	public void setinfoNew(string info, mVector vecCmd, bool isCmdClose, string nameDia)
	{
		if (!GameCanvas.isTouch)
		{
			setinfo(info, vecCmd, isCmdClose, 0);
		}
		else
		{
			beginDia();
			isNewShop = true;
			nameDialog = nameDia;
			type = 0;
			if (vecCmd != null)
			{
				cmdList = vecCmd;
			}
			else
			{
				cmdList = new mVector();
			}
			wDia = MotherCanvas.w - 30;
			if (wDia > 200)
			{
				wDia = 200;
			}
			maxWShow = wDia;
			if (GameCanvas.currentDialog == null)
			{
				wShowPaper = 5;
			}
			else
			{
				wShowPaper = maxWShow;
			}
			int num = cmdList.size();
			strinfo = fontDia.splitFontArray(info, wDia - 20);
			hDia = GameCanvas.hText * strinfo.Length + hPlus + (iCommand.hButtonCmdNor + 5);
			xDia = MotherCanvas.hw - wDia / 2;
			yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia - 5;
			int wButtonCmd = iCommand.wButtonCmd;
			int num2 = 0;
			if (num % 2 == 0)
			{
				num2 = wButtonCmd / 2;
			}
			for (int i = 0; i < num; i++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
				iCommand2.isSelect = false;
				iCommand2.setPos(MotherCanvas.hw - cmdList.size() / 2 * wButtonCmd + i * wButtonCmd + num2, yDia + hDia - GameCanvas.hCommand, null, iCommand2.caption);
				if (i == 0 && GameCanvas.isTouchNoOrPC())
				{
					iCommand2.isSelect = true;
				}
			}
			if (isCmdClose)
			{
				cmdClose = new iCommand(T.close, 2, this);
				cmdClose.setPos(MotherCanvas.hw + (wDia - 50) / 2, yDia - GameCanvas.hCommand / 2 + 7, MainTab.fraCloseTab, string.Empty);
				cmdList.addElement(cmdClose);
				backCMD = cmdClose;
			}
			else
			{
				isBack = false;
			}
		}
		xEFF = xDia + wDia / 2;
		yEFF = yDia + hDia - GameCanvas.hCommand * 3;
	}

	public void setWaitinginfo(string info, mVector vecCmd, bool isCmdClose, sbyte type, int time)
	{
		beginDia();
		base.type = type;
		if (vecCmd != null)
		{
			cmdList = vecCmd;
		}
		else
		{
			cmdList = new mVector();
		}
		if (isCmdClose)
		{
			cmdClose = new iCommand(T.close, 2, this);
			cmdList.addElement(cmdClose);
			backCMD = cmdClose;
		}
		else
		{
			isBack = false;
		}
		wDia = MotherCanvas.w - 30;
		if (wDia > 200)
		{
			wDia = 200;
		}
		maxWShow = wDia;
		wShowPaper = maxWShow;
		strinfo = fontDia.splitFontArray(info, wDia - 20);
		hDia = GameCanvas.hText * strinfo.Length + (isCmdClose ? iCommand.hButtonCmdNor : 0) + 28 + hPlus;
		if (time > 0)
		{
			hDia += 24;
			timeDialog = new CountDownTicket();
			timeDialog.setCountDown(time);
		}
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia - 5;
		int num = cmdList.size();
		int wButtonCmd = iCommand.wButtonCmd;
		int num2 = 0;
		if (num % 2 == 0)
		{
			num2 = wButtonCmd / 2;
		}
		for (int i = 0; i < num; i++)
		{
			iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
			iCommand2.isSelect = false;
			iCommand2.setPos(MotherCanvas.hw - cmdList.size() / 2 * wButtonCmd + i * wButtonCmd + num2, yDia + hDia - GameCanvas.hCommand, null, iCommand2.caption);
			if (i == 0 && GameCanvas.isTouchNoOrPC())
			{
				iCommand2.isSelect = true;
			}
		}
		setPosCmdNew(0, isLast: false);
	}

	public void setinfoQuest(MainQuest quest, bool isNew)
	{
		this.quest = quest;
		fontDia = mFont.tahoma_7_black;
		beginDia();
		type = 2;
		cmdList = new mVector();
		cmdClose = new iCommand(T.close, 2, this);
		cmdClose.levelSmall = 1;
		if (quest.typeMainSub != 0 && quest.statusQuest != 0 && !isNew)
		{
			iCommand iCommand2 = new iCommand(T.cancelQuest, 1, this);
			iCommand2.levelSmall = 1;
			cmdList.addElement(iCommand2);
		}
		cmdList.addElement(cmdClose);
		backCMD = cmdClose;
		wDia = MotherCanvas.w - 30;
		if (wDia > 160)
		{
			wDia = 160;
		}
		maxWShow = wDia;
		if (GameCanvas.currentDialog == null)
		{
			wShowPaper = 5;
		}
		else
		{
			wShowPaper = maxWShow;
		}
		int num = cmdList.size();
		nameDialog = T.questInfo;
		string empty = string.Empty;
		if (quest.statusQuest == 0)
		{
			nameDialog = T.newquest + ((quest.typeMainSub != 0) ? string.Empty : quest.getMainSub());
			empty = GameMidlet.format(T.helpQuestENG, quest.strNPC_Map);
		}
		else if (quest.statusQuest == 2)
		{
			empty = quest.name + "\n" + quest.strShowDialog;
			bool flag = false;
			for (int i = 0; i < quest.vecTypeQuest.size(); i++)
			{
				DataQuest dataQuest = (DataQuest)quest.vecTypeQuest.elementAt(i);
				if (dataQuest.typeQuest == 2 || dataQuest.typeQuest == 1)
				{
					empty = empty + "\n " + T.daHoanthanh;
					flag = true;
					break;
				}
			}
			if (flag)
			{
				empty = empty + "\n " + T.traNvtai + quest.strNPC_Map;
			}
		}
		else
		{
			empty = quest.name + "\n" + quest.strShowDialog;
			bool flag2 = false;
			for (int j = 0; j < quest.vecTypeQuest.size(); j++)
			{
				DataQuest dataQuest2 = (DataQuest)quest.vecTypeQuest.elementAt(j);
				if (dataQuest2.typeQuest != 2 && dataQuest2.typeQuest != 1)
				{
					continue;
				}
				if (!flag2)
				{
					if (quest.typeMainSub != 2 && quest.typeMainSub != 3)
					{
						empty = empty + "(" + LoadMap.getNameMap(quest.idMapHelp) + ")";
					}
					empty = empty + "\n " + T.mucdoHoanthanh;
					flag2 = true;
				}
				if (dataQuest2.typeQuest == 2)
				{
					string text = empty;
					empty = text + "\n  " + dataQuest2.nameItem + ": " + dataQuest2.numCur + "/" + dataQuest2.numMax;
				}
				if (dataQuest2.typeQuest == 1)
				{
					if (dataQuest2.nameItem != null && dataQuest2.nameItem.Length == 0)
					{
						dataQuest2.loadNameAgain();
					}
					string text2 = empty;
					empty = text2 + "\n  " + dataQuest2.nameItem + ": " + dataQuest2.numCur + "/" + dataQuest2.numMax;
				}
			}
		}
		strinfo = fontDia.splitFontArray(empty, wDia - 20);
		hDia = GameCanvas.hText * strinfo.Length + hPlus + ((num - 1) / 2 + 1) * (iCommand.hButtonCmdNor + 5);
		hDia += GameCanvas.hCommand;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h / 2 - hDia / 2;
		setPosCmdNew(0, isLast: true);
	}

	public void setNhanTraQuest(MainQuest quest, iCommand cmd)
	{
		this.quest = quest;
		fontDia = mFont.tahoma_7_black;
		beginDia();
		type = 2;
		cmdList = new mVector();
		cmdClose = new iCommand(T.close, 2, this);
		cmdClose.levelSmall = 1;
		cmd.levelSmall = 1;
		cmdList.addElement(cmd);
		backCMD = cmdClose;
		wDia = MotherCanvas.w - 30;
		if (wDia > 160)
		{
			wDia = 160;
		}
		maxWShow = wDia;
		if (GameCanvas.currentDialog == null)
		{
			wShowPaper = 5;
		}
		else
		{
			wShowPaper = maxWShow;
		}
		int num = cmdList.size();
		nameDialog = T.quest + " " + T.main;
		if (quest.typeMainSub == 1)
		{
			nameDialog = T.quest + " " + T.sub;
		}
		else if (quest.typeMainSub == 2)
		{
			nameDialog = T.quest + " " + T.replay;
		}
		string name = quest.name;
		strinfo = fontDia.splitFontArray(name, wDia - 20);
		hDia = GameCanvas.hText * strinfo.Length + hPlus + ((num - 1) / 2 + 1) * (iCommand.hButtonCmdNor + 5);
		hDia += GameCanvas.hCommand;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2;
		setPosCmdNew(0, isLast: false);
	}

	public void setinfoSkillInfo(Skill_Info skill)
	{
		this.skill = skill;
		fontDia = mFont.tahoma_7_black;
		beginDia();
		type = 4;
		cmdList = new mVector();
		iCommand iCommand2 = new iCommand(T.learn, 5, this);
		iCommand2.levelSmall = 1;
		cmdList.addElement(iCommand2);
		cmdClose = new iCommand(T.close, -1, this);
		cmdClose.levelSmall = 1;
		cmdList.addElement(cmdClose);
		backCMD = cmdClose;
		wDia = MotherCanvas.w - 30;
		if (wDia > 160)
		{
			wDia = 160;
		}
		maxWShow = wDia;
		int num = cmdList.size();
		skill.setVecInfo(wDia);
		hDia = GameCanvas.hText * skill.vecInfo.size() + hPlus + ((num - 1) / 2 + 1) * (iCommand.hButtonCmdNor + 5);
		hDia += GameCanvas.hCommand;
		int num2 = 0;
		if (hDia > MotherCanvas.h - GameCanvas.hCommand * 2)
		{
			num2 = hDia - (MotherCanvas.h - GameCanvas.hCommand * 2);
			hDia = MotherCanvas.h - GameCanvas.hCommand * 2;
		}
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2;
		if (num2 > 0)
		{
			list = new ListNew(xDia, yDia, wDia, hDia, 0, 0, num2, isLim0: true);
		}
		wShowPaper = maxWShow;
		setPosCmdNew(0, isLast: false);
		okCMD = cmdClose;
	}

	public void setinfoParty(mVector att)
	{
		if (att != null && att.size() != 0)
		{
			beginDia();
			fontDia = mFont.tahoma_7_white;
			nameDialog = T.AttriParty;
			type = 5;
			cmdClose = new iCommand(T.close, 2, this);
			cmdList.addElement(cmdClose);
			backCMD = cmdClose;
			wDia = MotherCanvas.w - 30;
			if (wDia > 160)
			{
				wDia = 160;
			}
			maxWShow = wDia;
			if (GameCanvas.currentDialog == null)
			{
				wShowPaper = 5;
			}
			else
			{
				wShowPaper = maxWShow;
			}
			int num = cmdList.size();
			strinfo = new string[att.size()];
			for (int i = 0; i < att.size(); i++)
			{
				MainInfoItem info = (MainInfoItem)att.elementAt(i);
				strinfo[i] = MainItem.getInfoEveryWhere(info);
			}
			hDia = GameCanvas.hText * strinfo.Length + hPlus + ((num - 1) / 2 + 1) * (iCommand.hButtonCmdNor + 15);
			if (GameCanvas.isTouch)
			{
				hDia = GameCanvas.hText * strinfo.Length + hPlus + 20;
			}
			xDia = MotherCanvas.hw - wDia / 2;
			yDia = MotherCanvas.hh - hDia / 2;
			if (GameCanvas.isTouch)
			{
				cmdClose.setPos(xDia + wDia - 25, yDia + 4 + 8, MainTab.fraCloseTab, string.Empty);
			}
			else
			{
				setPosCmdNew(0, isLast: false);
			}
		}
	}

	public void setinfoClan(MainClan Clan)
	{
		clanpaint = Clan;
		posValueName = new int[T.mNameClan.Length];
		wItem = GameCanvas.hText + 4;
		for (int i = 0; i < T.mNameClan.Length; i++)
		{
			posValueName[i] = mFont.tahoma_7_white.getWidth(T.mNameClan[i]);
		}
		beginDia();
		isNewShop = true;
		nameDialog = T.Clan + " " + clanpaint.name;
		type = 10;
		cmdList = new mVector();
		wDia = MotherCanvas.w - 30;
		if (wDia > 180)
		{
			wDia = 180;
		}
		maxWShow = wDia;
		if (GameCanvas.currentDialog == null)
		{
			wShowPaper = 5;
		}
		else
		{
			wShowPaper = maxWShow;
		}
		hDia = 120;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h / 2 - hDia / 2 - 5;
		cmdClose = new iCommand(T.close, 2, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(MotherCanvas.hw + (wDia - 50) / 2, yDia + 4 + 7, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			cmdClose = AvMain.setPosCMD(cmdClose, 2);
		}
		cmdList.addElement(cmdClose);
		backCMD = cmdClose;
		xEFF = xDia + wDia / 2;
		yEFF = yDia + hDia - GameCanvas.hCommand * 3;
	}

	public void setPosCmdNew(int yplus, bool isLast)
	{
		idCommand = 0;
		if (cmdList.size() <= 0)
		{
			return;
		}
		int num = cmdList.size();
		switch (num)
		{
		case 1:
			xBegin = xDia + wDia / 2;
			w2cmd = 0;
			break;
		case 2:
			w2cmd = 10;
			xBegin = xDia + wDia / 2 - w2cmd / 2 - iCommand.wButtonCmd / 2;
			break;
		default:
			w2cmd = 10;
			xBegin = xDia + wDia / 2 - w2cmd / 2 - iCommand.wButtonCmd / 2;
			break;
		}
		for (int i = 0; i < num; i++)
		{
			iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
			iCommand2.isSelect = false;
			if (num == 3 && i == 2)
			{
				iCommand2.setPos(xDia + wDia / 2, yDia + hDia - iCommand.hButtonCmdNor - (num - 1) / 2 * (iCommand.hButtonCmdNor + 5) + iCommand.hButtonCmdNor / 2 + 2 + i / 2 * (iCommand.hButtonCmdNor + 5) + yplus - 8, null, iCommand2.caption);
			}
			else
			{
				iCommand2.setPos(xBegin + i % 2 * (iCommand.wButtonCmd + w2cmd), yDia + hDia - iCommand.hButtonCmdNor / 2 - (num - 1) / 2 * (iCommand.hButtonCmdNor + 5) + 2 + i / 2 * (iCommand.hButtonCmdNor + 5) + yplus - 8, null, iCommand2.caption);
			}
			if (isLast)
			{
				if (i == num - 1 && GameCanvas.isTouchNoOrPC())
				{
					iCommand2.isSelect = true;
					idCommand = i;
				}
			}
			else if (i == 0 && GameCanvas.isTouchNoOrPC())
			{
				iCommand2.isSelect = true;
				idCommand = 0;
			}
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = 0;
		switch (type)
		{
		case 0:
		case 9:
		{
			if (isNewShop)
			{
				num = GameCanvas.hCommand;
			}
			paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia - miniItem - num, wShowPaper, hDia + miniItem * 2 + num, maxWShow, AvMain.PAPER_NORMAL);
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.saveCanvas();
			g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			if (isNewShop)
			{
				g.setColor(15972174);
				g.fillRoundRect(xDia + 25, yDia - num + 12, wDia - 50, 16, 4, 4);
				AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia + 15 - num, 2, 6, 5);
			}
			for (int num9 = 0; num9 < strinfo.Length; num9++)
			{
				fontDia.drawString(g, strinfo[num9], MotherCanvas.w / 2, yDia + 10 + num9 * GameCanvas.hText, 2);
			}
			for (int num10 = 0; num10 < vecEff.size(); num10++)
			{
				((MainEffect)vecEff.elementAt(num10)).paint(g);
			}
			if (cmdList != null)
			{
				for (int num11 = 0; num11 < cmdList.size(); num11++)
				{
					iCommand iCommand7 = (iCommand)cmdList.elementAt(num11);
					iCommand7.paint(g, iCommand7.xCmd, iCommand7.yCmd);
				}
			}
			g.restoreCanvas();
			break;
		}
		case 5:
		{
			AvMain.paintRect(g, MotherCanvas.hw - wShowPaper / 2, yDia - miniItem - num, wShowPaper, hDia + miniItem * 2 + num - 5, 1, 4);
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.saveCanvas();
			g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.setColor(15972174);
			g.fillRoundRect(xDia + 25, yDia - num + 4, wDia - 50, 16, 4, 4);
			AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia + 7 - num, 2, 6, 5);
			for (int m = 0; m < strinfo.Length; m++)
			{
				fontDia.drawString(g, strinfo[m], xDia + 4, yDia + 27 + m * GameCanvas.hText, 0);
			}
			if (cmdList != null)
			{
				for (int n = 0; n < cmdList.size(); n++)
				{
					iCommand iCommand4 = (iCommand)cmdList.elementAt(n);
					iCommand4.paint(g, iCommand4.xCmd, iCommand4.yCmd);
				}
			}
			g.restoreCanvas();
			break;
		}
		case 1:
		case 6:
		case 8:
		{
			paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia - miniItem, wShowPaper, hDia + miniItem * 2, maxWShow, AvMain.PAPER_NORMAL);
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.saveCanvas();
			g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			for (int num7 = 0; num7 < strinfo.Length; num7++)
			{
				fontDia.drawString(g, strinfo[num7], MotherCanvas.w / 2, yDia + 10 + num7 * GameCanvas.hText, 2);
			}
			if (type == 1 || type == 8)
			{
				fraImgWaiting.drawFrame(fWait / 2 % fraImgWaiting.nFrame, MotherCanvas.hw, yDia + 22 + strinfo.Length * GameCanvas.hText, 0, mGraphics.VCENTER | mGraphics.HCENTER, g);
			}
			else if (type == 6 && timeDialog != null)
			{
				timeDialog.paintCountDownTicket(g, mFont.tahoma_7b_black, MotherCanvas.hw, yDia + 12 + strinfo.Length * GameCanvas.hText, 2);
			}
			if (cmdList != null)
			{
				for (int num8 = 0; num8 < cmdList.size(); num8++)
				{
					iCommand iCommand6 = (iCommand)cmdList.elementAt(num8);
					iCommand6.paint(g, iCommand6.xCmd, iCommand6.yCmd);
				}
			}
			g.restoreCanvas();
			break;
		}
		case 2:
		{
			paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
			GameCanvas.resetTrans(g);
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.saveCanvas();
			g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.setColor(15972174);
			g.fillRoundRect(xDia + 10, yDia + 12, wDia - 20, 16, 4, 4);
			if (quest != null)
			{
				AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia + 15, 2, 6, 5);
			}
			for (int num5 = 0; num5 < strinfo.Length; num5++)
			{
				if (num5 == 0 && quest.statusQuest != 0)
				{
					mFont.tahoma_7b_black.drawString(g, strinfo[num5], MotherCanvas.w / 2, yDia + GameCanvas.hCommand + 10 + num5 * GameCanvas.hText, 2);
				}
				else
				{
					fontDia.drawString(g, strinfo[num5], xDia + 10, yDia + GameCanvas.hCommand + 10 + num5 * GameCanvas.hText, 0);
				}
			}
			if (cmdList != null)
			{
				for (int num6 = 0; num6 < cmdList.size(); num6++)
				{
					iCommand iCommand5 = (iCommand)cmdList.elementAt(num6);
					iCommand5.paint(g, iCommand5.xCmd, iCommand5.yCmd);
				}
			}
			g.restoreCanvas();
			break;
		}
		case 4:
			paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
			g.setColor(14203529);
			g.fillRoundRect(xDia + 10, yDia + 12, wDia - 20, 16, 4, 4);
			if (skill != null)
			{
				AvMain.PaintName_Image(g, skill.idIcon, skill.name, xDia + wDia / 2, yDia + 15, 2, 3, 5);
				g.setClip(xDia, yDia + GameCanvas.hCommand + 10, wDia, hDia - (GameCanvas.hCommand + iCommand.hButtonCmdNor + 20));
				g.saveCanvas();
				g.ClipRec(xDia, yDia + GameCanvas.hCommand + 10, wDia, hDia - (GameCanvas.hCommand + iCommand.hButtonCmdNor + 20));
				if (list != null)
				{
					g.translate(0, -list.cmx);
				}
				for (int k = 0; k < skill.vecInfo.size(); k++)
				{
					infoShow infoShow2 = (infoShow)skill.vecInfo.elementAt(k);
					fontDia.drawString(g, infoShow2.strShow, xDia + 12, yDia + GameCanvas.hCommand + 10 + k * GameCanvas.hText, 0);
				}
				mGraphics.resetTransAndroid(g);
				g.restoreCanvas();
			}
			GameCanvas.resetTrans(g);
			if (cmdList != null)
			{
				for (int l = 0; l < cmdList.size(); l++)
				{
					iCommand iCommand3 = (iCommand)cmdList.elementAt(l);
					iCommand3.paint(g, iCommand3.xCmd, iCommand3.yCmd);
				}
			}
			break;
		case 7:
			if (isNewShop)
			{
				num = GameCanvas.hCommand;
			}
			paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia - miniItem - num, wShowPaper, hDia + miniItem * 2 + num, maxWShow, AvMain.PAPER_NORMAL);
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.saveCanvas();
			g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			if (vecItem != null)
			{
				if (isNewShop)
				{
					g.setColor(15972174);
					g.fillRoundRect(xDia + 25, yDia - num + 10, wDia - 50, 20, 4, 4);
					MainItem mainItem = (MainItem)vecItem.elementAt(0);
					int width = mFont.tahoma_7b_black.getWidth(mainItem.name);
					mainItem.paintAllItem(g, xDia + wDia / 2 - width / 2, yDia + 9 + wItem / 2 - num, wItem, 0, 0);
					AvMain.FontBorderColor(g, mainItem.name, xDia + wDia / 2 + wItem / 2, yDia + 15 - num, 2, 6, 5);
				}
				int num12 = yDia + 10;
				for (int num13 = 0; num13 < strinfo.Length; num13++)
				{
					fontDia.drawString(g, strinfo[num13], MotherCanvas.w / 2, num12, 2);
					num12 += GameCanvas.hText;
				}
				for (int num14 = 1; num14 < vecItem.size(); num14++)
				{
					MainItem mainItem2 = (MainItem)vecItem.elementAt(num14);
					mainItem2.paintAllItem(g, xDia + 10 + wItem / 2, num12 + 5 + (num14 - 1) * wItem, wItem, 0, 0);
					mFont.tahoma_7b_black.drawString(g, mainItem2.name, xDia + 11 + wItem, num12 + (num14 - 1) * wItem, 0);
				}
			}
			if (cmdList != null)
			{
				for (int num15 = 0; num15 < cmdList.size(); num15++)
				{
					iCommand iCommand8 = (iCommand)cmdList.elementAt(num15);
					iCommand8.paint(g, iCommand8.xCmd, iCommand8.yCmd);
				}
			}
			g.restoreCanvas();
			break;
		case 10:
		{
			paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia - miniItem - num, wShowPaper, hDia + miniItem * 2 + num, maxWShow, AvMain.PAPER_NORMAL);
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.saveCanvas();
			g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.setColor(15972174);
			g.fillRoundRect(xDia + 25, yDia - num + 4, wDia - 50, 16, 4, 4);
			AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia + 7 - num, 2, 6, 5);
			int num2 = yDia + 10 - num + GameCanvas.hText;
			int num3 = xDia + 10;
			for (int i = 1; i < 6; i++)
			{
				mFont.tahoma_7_black.drawString(g, T.mNameClan[i], num3, num2, 0);
				switch (i)
				{
				case 1:
					mFont.tahoma_7b_brown.drawString(g, clanpaint.nameCaption, num3 + posValueName[i], num2, 0);
					break;
				case 2:
				{
					MainImage iconClanBig = Potion.getIconClanBig(clanpaint.idIcon);
					if (iconClanBig == null || iconClanBig.img == null)
					{
						break;
					}
					if (iconClanBig.frame == -1)
					{
						iconClanBig.set_Frame();
					}
					if (iconClanBig.frame <= 1)
					{
						g.drawImage(iconClanBig.img, num3 + posValueName[i] + 14, num2 + 4, 3);
						break;
					}
					int num4 = ((framepaint < iconClanBig.frame - 1) ? 3 : 15);
					if (CRes.abs(GameCanvas.gameTick - lastTick) > num4)
					{
						framepaint++;
						if (framepaint >= iconClanBig.frame)
						{
							framepaint = 0;
						}
						lastTick = GameCanvas.gameTick;
					}
					g.drawRegion(iconClanBig.img, 0, framepaint * iconClanBig.w, iconClanBig.w, iconClanBig.w, 0, num3 + posValueName[i] + 14, num2 + 4, 3);
					break;
				}
				case 3:
					mFont.tahoma_7b_brown.drawString(g, clanpaint.level + string.Empty, num3 + posValueName[3], num2, 0);
					break;
				case 4:
					mFont.tahoma_7b_brown.drawString(g, clanpaint.numMem + "/" + clanpaint.maxNumMem, num3 + posValueName[4], num2, 0);
					break;
				case 5:
					mFont.tahoma_7b_brown.drawString(g, clanpaint.trungsinh + string.Empty, num3 + posValueName[5], num2, 0);
					break;
				}
				num2 += wItem;
			}
			if (cmdList != null)
			{
				for (int j = 0; j < cmdList.size(); j++)
				{
					iCommand iCommand2 = (iCommand)cmdList.elementAt(j);
					iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
				}
			}
			g.restoreCanvas();
			break;
		}
		case 3:
			break;
		}
	}

	public void updateOpen()
	{
		if (wShowPaper >= maxWShow)
		{
			return;
		}
		wShowPaper += vShow;
		if (wShowPaper > maxWShow)
		{
			wShowPaper = maxWShow;
			vShow = 15;
		}
		if (vShow < 100)
		{
			vShow += 15;
			if (vShow > 100)
			{
				vShow = 100;
			}
		}
	}

	public void updateClose()
	{
		if (wShowPaper <= 0)
		{
			return;
		}
		if (wShowPaper <= 5)
		{
			closeDialog();
		}
		wShowPaper -= vShow;
		if (wShowPaper < 5)
		{
			wShowPaper = 5;
			vShow = 15;
		}
		if (vShow < 100)
		{
			vShow += 15;
			if (vShow > 100)
			{
				vShow = 100;
			}
		}
	}

	public override void update()
	{
		if (isClose)
		{
			updateClose();
			return;
		}
		if (type == 1 || type == 8)
		{
			fWait++;
			if (type == 8 && fWait == 200 && GameMidlet.DEVICE != 0)
			{
				try
				{
					GameCanvas.infoDisConnect = GameMidlet.connectHTTP(GameMidlet.getThongBao());
				}
				catch (Exception)
				{
				}
			}
			else if (fWait > 1200)
			{
				closeDialog();
				if (Session_ME.gI().isConnected() && GameCanvas.currentScreen != GameCanvas.loginScr && GameCanvas.currentScreen != GameCanvas.loadMapScr)
				{
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.getdatafail);
				}
				else
				{
					mSystem.outz("disconect MsgDialog");
					GameCanvas.dialogDisconect();
				}
			}
		}
		else if (type == 6)
		{
			if (timeDialog != null)
			{
				timeDialog.updateTimeCountDownTicket();
				if (timeDialog.timeCountDown <= 0)
				{
					closeDialog();
				}
			}
			else
			{
				closeDialog();
			}
		}
		else if (type == 9)
		{
			fWait++;
			if (fWait >= 500 && isAuroReconect)
			{
				GameScreen.cmdReConnect.perform();
				fWait = 0;
			}
		}
		if (type == 4)
		{
			if (list != null)
			{
				list.moveCamera();
			}
		}
		else
		{
			updateOpen();
		}
		updatekey();
		updatePointer();
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			mainEffect.update();
			if (mainEffect.isStop)
			{
				vecEff.removeElement(mainEffect);
				i--;
			}
		}
	}

	public override void updatekey()
	{
		if (isClose)
		{
			return;
		}
		if (type == 4 && list != null)
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
		if (cmdList != null)
		{
			int num = cmdList.size();
			if (GameCanvas.isTouchNoOrPC() && num > 0)
			{
				int num2 = idCommand;
				if (GameCanvas.keyMove(0))
				{
					idCommand--;
					GameCanvas.ClearkeyMove(0);
				}
				else if (GameCanvas.keyMove(2))
				{
					idCommand++;
					GameCanvas.ClearkeyMove(2);
				}
				if (list == null || list.cmxLim <= 0)
				{
					if (GameCanvas.keyMove(1))
					{
						GameCanvas.ClearkeyMove(1);
						idCommand -= 2;
					}
					else if (GameCanvas.keyMove(3))
					{
						GameCanvas.ClearkeyMove(3);
						idCommand += 2;
					}
				}
				idCommand = AvMain.resetSelect(idCommand, num - 1, isreset: false);
				if (num2 != idCommand && GameCanvas.isTouchNoOrPC())
				{
					if (((iCommand)cmdList.elementAt(idCommand)).caption.Length == 0)
					{
						idCommand = 0;
					}
					for (int i = 0; i < num; i++)
					{
						iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
						if (i == idCommand)
						{
							iCommand2.isSelect = true;
						}
						else
						{
							iCommand2.isSelect = false;
						}
					}
				}
			}
		}
		if (GameCanvas.keyMyHold[5] && cmdList != null && idCommand < cmdList.size())
		{
			((iCommand)cmdList.elementAt(idCommand)).perform();
			GameCanvas.clearKeyHold(5);
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		if (isClose)
		{
			return;
		}
		if (type == 4)
		{
			list.update_Pos_UP_DOWN();
		}
		if (cmdList != null)
		{
			for (int i = 0; i < cmdList.size(); i++)
			{
				((iCommand)cmdList.elementAt(i)).updatePointer();
			}
		}
	}

	public void closeDialog()
	{
		if (GameCanvas.currentDialog != null)
		{
			GameCanvas.currentDialog = null;
		}
		else
		{
			GameCanvas.subDialog = null;
		}
	}

	public static void addEff(MainEffect eff)
	{
		if (eff != null)
		{
			eff.x = xEFF;
			eff.y = yEFF;
			vecEff.addElement(eff);
		}
	}

	public void setInfoHelp(string str)
	{
		infoHelp = str;
		wInfoHelp = mFont.tahoma_7_white.getWidth(str) + 6;
		tickCloseInfoHelp = 100;
	}

	public void paintInfoHelp(mGraphics g)
	{
		if (infoHelp.Length > 0)
		{
			Interface_Game.paintBackInfoServer(g, xDia + wDia / 2, yDia + hDia + 5, wInfoHelp);
			mFont.tahoma_7_white.drawString(g, infoHelp, xDia + wDia / 2, yDia + hDia + 10, 2);
		}
	}

	public void updateInfoHelp()
	{
		if (infoHelp.Length > 0)
		{
			tickCloseInfoHelp--;
			if (tickCloseInfoHelp == 0)
			{
				infoHelp = string.Empty;
			}
		}
	}

	public virtual void updatekeyCMD()
	{
		base.updatekey();
	}
}
