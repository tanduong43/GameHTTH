using System;

public class MainQuest : AvMain
{
	public const int MAIN = 0;

	public const int SUB = 1;

	public const int REPLAY = 2;

	public const int CLAN = 3;

	public const int TYPE_ITEM = 0;

	public const int TYPE_MONSTER = 1;

	public const int NHAN = 0;

	public const int TRA = 1;

	public const sbyte STATUSQUEST_ADD = 0;

	public const sbyte STATUSQUEST_CURRENT = 1;

	public const sbyte STATUSQUEST_FINISH = 2;

	public const sbyte ACTION_QUEST_TALK = 0;

	public const sbyte ACTION_QUEST_KILL_MON = 1;

	public const sbyte ACTION_QUEST_GET_ITEM = 2;

	public sbyte typeMainSub;

	public sbyte statusQuest;

	public sbyte typeActionQuest;

	public int idNPC;

	public int idNPC_Sub = -32000;

	public short ID;

	public short idMapHelp;

	public short lvRequest;

	public string name;

	public string strTalk;

	public string strNhacNho;

	public string[] mstrTalk;

	public string strShowDialog;

	public string strNPC_Map = string.Empty;

	public mVector vecTypeQuest = new mVector("MainQuest.vecTypeQuest");

	private int step;

	public MainQuest(short ID)
	{
		this.ID = ID;
	}

	public static MainQuest getQuest(short ID)
	{
		for (int i = 0; i < Player.vecQuest.size(); i++)
		{
			MainQuest mainQuest = (MainQuest)Player.vecQuest.elementAt(i);
			if (mainQuest.ID == ID)
			{
				return mainQuest;
			}
		}
		return null;
	}

	public string getMainSub()
	{
		if (typeMainSub == 0)
		{
			return T.main;
		}
		if (typeMainSub == 2)
		{
			return T.replay;
		}
		if (typeMainSub == 3)
		{
			return T.Qclan;
		}
		return T.sub;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (GameCanvas.menuCur.runText == null || GameCanvas.menuCur.runText.nextDlgStep())
			{
				nextStep();
			}
			break;
		case 1:
			if (GameCanvas.menuCur.runText == null || GameCanvas.menuCur.runText.nextDlgStep())
			{
				MainObject mainObject = MainObject.get_Object(idNPC, 2);
				if (mainObject != null)
				{
					mainObject.chat = null;
				}
				string text = strShowDialog;
				if (text == null)
				{
					text = "sai roi";
				}
				GameScreen.player.chat = null;
				iCommand iCommand2 = setCmdDialog();
				Player.curQuest = null;
				GameCanvas.clearKeyHold();
				GameCanvas.isPointerSelect = false;
				GameCanvas.menuCur.isShowMenu = false;
				iCommand2.perform();
			}
			break;
		case 2:
			if (statusQuest == 0 && GameScreen.player.Lv < lvRequest && typeMainSub == 0)
			{
				GameCanvas.menuCur.doCloseMenu();
				GameCanvas.end_Dialog();
				break;
			}
			GameCanvas.menuCur.doCloseMenu();
			GlobalService.gI().quest(1, ID);
			GameCanvas.end_Dialog();
			GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: true);
			break;
		case 3:
			GameCanvas.end_Dialog();
			GameCanvas.menuCur.doCloseMenu();
			step = 0;
			Player.curQuest = this;
			nextStep();
			break;
		case 4:
			GameCanvas.menuCur.doCloseMenu();
			GlobalService.gI().quest(4, ID);
			GameCanvas.end_Dialog();
			GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: true);
			Player.idNPCQuestCur = (short)idNPC;
			break;
		}
	}

	public iCommand setCmdDialog()
	{
		iCommand result = null;
		if (statusQuest == 0)
		{
			result = new iCommand(T.nhanQuest, 3, this);
		}
		else if (statusQuest == 2)
		{
			result = new iCommand(T.traQuest, 3, this);
		}
		return result;
	}

	public void beginQuest(short IDObj)
	{
		step = 0;
		if (idNPC == IDObj)
		{
			if (statusQuest == 2 || statusQuest == 0)
			{
				commandPointer(0, 0);
			}
			else if (statusQuest == 1)
			{
				MainObject mainObject = MainObject.get_Object(idNPC, 2);
				if (mainObject != null)
				{
					mainObject.strChatPopup = strNhacNho;
				}
			}
		}
		else if (statusQuest == 2 && idNPC_Sub == IDObj)
		{
			MainObject mainObject2 = MainObject.get_Object(idNPC_Sub, 2);
			if (mainObject2 != null)
			{
				mainObject2.strChatPopup = strNhacNho;
			}
		}
	}

	public void nextStep()
	{
		if (MainObject.get_Object(idNPC, 2) == null)
		{
			Player.curQuest = null;
			return;
		}
		if (mstrTalk == null)
		{
			if (statusQuest == 0 && GameScreen.player.Lv < lvRequest && typeMainSub == 0)
			{
				mstrTalk = mFont.split(T.strQuestDe + lvRequest + ".", ">");
			}
			else
			{
				mstrTalk = mFont.split(strTalk, ">");
			}
		}
		if (mstrTalk == null)
		{
			Player.curQuest = null;
			return;
		}
		if (mstrTalk[step].Trim().StartsWith("0"))
		{
			MainObject.get_Object(idNPC, 2).chat = null;
			mVector mVector2 = new mVector();
			iCommand o = setCmd();
			mVector2.addElement(o);
			GameCanvas.menu.startAt_NPC_Quest(mVector2, GameMidlet.SubStr(mstrTalk[step], 1, mstrTalk[step].Length), GameScreen.player.ID, 0, isQuest: true, 0);
		}
		else
		{
			GameScreen.player.chat = null;
			mVector mVector3 = new mVector();
			iCommand o2 = setCmd();
			mVector3.addElement(o2);
			GameCanvas.menu.startAt_NPC_Quest(mVector3, GameMidlet.SubStr(mstrTalk[step], 1, mstrTalk[step].Length), idNPC, 2, isQuest: true, 0);
		}
		step++;
	}

	public iCommand setCmd()
	{
		iCommand iCommand2 = null;
		if (step < mstrTalk.Length - 1)
		{
			return new iCommand(T.next + " >>", 0, this);
		}
		if (statusQuest == 2)
		{
			return new iCommand(T.next + " >>", 4, this);
		}
		return new iCommand(T.next + " >>", 2, this);
	}

	public iCommand getCmeTabQuest()
	{
		if (statusQuest == 0)
		{
			return TabQuest.cmdShow;
		}
		return TabQuest.cmdShow;
	}

	public static void updateDataQuestKillMon(short idMonster)
	{
		MainMonster mainMonster = (MainMonster)MainObject.get_Object(idMonster, 1);
		if (mainMonster == null)
		{
			return;
		}
		string empty = string.Empty;
		MainQuest mainQuest = null;
		bool flag = false;
		for (int i = 0; i < Player.vecQuest.size(); i++)
		{
			if (flag)
			{
				break;
			}
			MainQuest mainQuest2 = (MainQuest)Player.vecQuest.elementAt(i);
			if (mainQuest2.statusQuest != 1)
			{
				continue;
			}
			for (int j = 0; j < mainQuest2.vecTypeQuest.size(); j++)
			{
				DataQuest dataQuest = (DataQuest)mainQuest2.vecTypeQuest.elementAt(j);
				if (dataQuest.typeQuest == 1 && dataQuest.IDItem == mainMonster.idCatMonster && dataQuest.numCur < dataQuest.numMax)
				{
					mainQuest = mainQuest2;
					if (mainQuest2.typeMainSub == 1)
					{
						flag = true;
					}
					break;
				}
			}
		}
		if (mainQuest == null)
		{
			return;
		}
		for (int k = 0; k < mainQuest.vecTypeQuest.size(); k++)
		{
			DataQuest dataQuest2 = (DataQuest)mainQuest.vecTypeQuest.elementAt(k);
			if (dataQuest2.typeQuest == 1 && dataQuest2.IDItem == mainMonster.idCatMonster && dataQuest2.numCur < dataQuest2.numMax)
			{
				dataQuest2.numCur++;
				break;
			}
		}
	}

	public static void updateDataQuestGetPotion(short id)
	{
		MainQuest mainQuest = null;
		bool flag = false;
		for (int i = 0; i < Player.vecQuest.size(); i++)
		{
			if (flag)
			{
				break;
			}
			MainQuest mainQuest2 = (MainQuest)Player.vecQuest.elementAt(i);
			if (mainQuest2.statusQuest != 1 || (mainQuest != null && mainQuest.typeMainSub < mainQuest2.typeMainSub))
			{
				continue;
			}
			for (int j = 0; j < mainQuest2.vecTypeQuest.size(); j++)
			{
				DataQuest dataQuest = (DataQuest)mainQuest2.vecTypeQuest.elementAt(j);
				if (dataQuest.typeQuest == 2 && dataQuest.IDItem == id && dataQuest.numCur < dataQuest.numMax)
				{
					mainQuest = mainQuest2;
					if (mainQuest2.typeMainSub == 1)
					{
						flag = true;
					}
					break;
				}
			}
		}
		if (mainQuest == null)
		{
			return;
		}
		for (int k = 0; k < mainQuest.vecTypeQuest.size(); k++)
		{
			DataQuest dataQuest2 = (DataQuest)mainQuest.vecTypeQuest.elementAt(k);
			if (dataQuest2.typeQuest == 2 && dataQuest2.IDItem == id && dataQuest2.numCur < dataQuest2.numMax)
			{
				dataQuest2.numCur++;
				break;
			}
		}
	}

	public static void LoadNamePotionQuest(DataInputStream iss, bool isSave)
	{
		if (iss == null)
		{
			GlobalService.gI().get_DATA(7);
			return;
		}
		try
		{
			short num = iss.readShort();
			DataQuest.nameItemQuest = new string[num];
			for (int i = 0; i < num; i++)
			{
				DataQuest.nameItemQuest[i] = iss.readUTF();
			}
			if (isSave)
			{
				GlobalService.VerNamePotionQuest = iss.readShort();
				SaveRms.saveVer(GlobalService.VerNamePotionQuest, "VerdataNamePotionquest");
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}
}
