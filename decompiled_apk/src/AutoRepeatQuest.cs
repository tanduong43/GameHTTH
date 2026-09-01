using System;
using System.Collections;
using UnityEngine;

public class AutoRepeatQuest : MainObject
{
	public int timeStep = 1000;

	public const int timeStep1 = 1000;

	public const int timeStep2 = 2000;

	public const int timeStep3 = 3000;

	public const int timeStep4 = 5000;

	public const int timeStep5 = 10000;

	public static int step;

	public const int xMove = 500;

	public const int yMove = 60;

	public const int dFocus = 140;

	public static int toXDefault = 500;

	public static int toYDefault = 237;

	public static bool isCheckFinish;

	public static bool isStart;

	public static bool isComeMonster;

	public static bool isWaitingForMons;

	public static bool isGoBack;

	public static bool isFar;

	public bool isCome;

	public static bool isFindMons = true;

	private string nameMonQuest = string.Empty;

	public static MainObject objectFocus;

	private int timeSpeedUp1 = (int)(1000.0 / (1.0 + (double)GameCanvas.percent / 100.0));

	private int timeSpeedUp2 = (int)(2000.0 / (1.0 + (double)GameCanvas.percent / 100.0));

	private int timeSpeedUp3 = (int)(3000.0 / (1.0 + (double)GameCanvas.percent / 100.0));

	private int timeSpeedUp4 = (int)(5000.0 / (1.0 + (double)GameCanvas.percent / 100.0));

	private int timeSpeedUp5 = (int)(10000.0 / (1.0 + (double)GameCanvas.percent / 100.0));

	public void moveToQuestBoard()
	{
		MainObject theQuestBoard = getTheQuestBoard();
		if (theQuestBoard != null)
		{
			int distance = MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, theQuestBoard.x, theQuestBoard.y);
			GameScreen.player.toX = theQuestBoard.x;
			GameScreen.player.toY = theQuestBoard.y;
			if (MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, GameScreen.player.toX, GameScreen.player.toY) < GameScreen.player.vMax * 2)
			{
				GameScreen.player.x = theQuestBoard.x;
				GameScreen.player.y = theQuestBoard.y;
			}
			else
			{
				GameScreen.player.isMoveNor = true;
			}
			isCome = distance < 10;
			GameScreen.objFocus = theQuestBoard;
		}
	}

	public static bool movePlayer(int toX, int toY)
	{
		if (isComeMonster)
		{
			return true;
		}
		GameScreen.player.toX = toX;
		GameScreen.player.toY = toY;
		int distance = MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, toX, toY);
		if (MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, GameScreen.player.toX, GameScreen.player.toY) < GameScreen.player.vMax * 2)
		{
			GameScreen.player.x = toX;
			GameScreen.player.y = toY;
		}
		else
		{
			GameScreen.player.isMoveNor = true;
		}
		return distance == 0;
	}

	public void nextStep(MainQuest repeatQuest)
	{
		int statusQuest = repeatQuest.statusQuest;
		mVector mVector2 = new mVector();
		switch (statusQuest)
		{
		case 1:
			if (movePlayer(toXDefault, toYDefault))
			{
				isCheckFinish = true;
			}
			break;
		case 0:
		{
			iCommand o = new iCommand(repeatQuest.name + ((repeatQuest.typeMainSub != 0) ? string.Empty : repeatQuest.getMainSub()), 1, repeatQuest.ID, this);
			mVector2.addElement(o);
			GameCanvas.menu.startAt(mVector2, 2, T.quest);
			if (!isStart)
			{
				runNextStep();
			}
			break;
		}
		}
	}

	public static int checkGetQuest()
	{
		int result = 0;
		MainDialog currentDialog = GameCanvas.currentDialog;
		if (currentDialog != null)
		{
			if (currentDialog.strinfo[0].IndexOf(T.textNotFound) >= 0)
			{
				result = 1;
			}
			else if (currentDialog.strinfo[0].IndexOf(T.textTheOnly) >= 0)
			{
				result = 2;
			}
		}
		return result;
	}

	public static MainQuest getRepeatQuest()
	{
		MainObject theQuestBoard = getTheQuestBoard();
		if (theQuestBoard == null)
		{
			return null;
		}
		try
		{
			MainQuest result = null;
			for (int i = 0; i < Player.vecQuest.size(); i++)
			{
				MainQuest mainQuest = (MainQuest)Player.vecQuest.elementAt(i);
				if (mainQuest.idNPC == theQuestBoard.ID || mainQuest.idNPC_Sub == theQuestBoard.ID)
				{
					result = mainQuest;
					break;
				}
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public MainQuest getRepeatQuestFinsh()
	{
		MainObject theQuestBoard = getTheQuestBoard();
		if (theQuestBoard == null)
		{
			return null;
		}
		try
		{
			MainQuest result = null;
			for (int i = 0; i < Player.vecQuest.size(); i++)
			{
				MainQuest mainQuest = (MainQuest)Player.vecQuest.elementAt(i);
				if ((mainQuest.idNPC == theQuestBoard.ID || mainQuest.idNPC_Sub == theQuestBoard.ID) && mainQuest.statusQuest == 2)
				{
					result = mainQuest;
					break;
				}
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static MainQuest getRepeatCurrentQuest()
	{
		MainObject theQuestBoard = getTheQuestBoard();
		if (theQuestBoard == null)
		{
			return null;
		}
		try
		{
			MainQuest result = null;
			for (int i = 0; i < Player.vecQuest.size(); i++)
			{
				MainQuest mainQuest = (MainQuest)Player.vecQuest.elementAt(i);
				if ((mainQuest.idNPC == theQuestBoard.ID || mainQuest.idNPC_Sub == theQuestBoard.ID) && mainQuest.statusQuest == 1)
				{
					result = mainQuest;
					break;
				}
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public MainObject findMonster()
	{
		MainObject result = null;
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject.typeObject == 1 && MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, mainObject.x, mainObject.y) < 140)
			{
				result = mainObject;
				break;
			}
		}
		return result;
	}

	public MainObject findMonsterFocus(MainQuest quest)
	{
		try
		{
			MainObject result = null;
			if (isFindMons)
			{
				isFindMons = false;
				nameMonQuest = getNameMonsterRepeatQuest(quest);
				if (nameMonQuest != null && nameMonQuest != string.Empty)
				{
					for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
					{
						MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
						if (mainObject.typeObject == 1)
						{
							int distance = MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, mainObject.x, mainObject.y);
							if (nameMonQuest != string.Empty && mainObject.name.ToLower().Equals(nameMonQuest.ToLower()) && !mainObject.isDie && mainObject.Hp > 0 && distance < 140)
							{
								result = mainObject;
								isFar = false;
								break;
							}
						}
					}
				}
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public MainObject findMonsterFocusFar(MainQuest quest)
	{
		try
		{
			MainObject result = null;
			if (isFindMons)
			{
				isFindMons = false;
				nameMonQuest = getNameMonsterRepeatQuest(quest);
				if (nameMonQuest != null && nameMonQuest != string.Empty)
				{
					for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
					{
						MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
						if (mainObject.typeObject == 1 && nameMonQuest != string.Empty && mainObject.name.ToLower().Equals(nameMonQuest.ToLower()) && !mainObject.isDie && mainObject.Hp > 0)
						{
							result = mainObject;
							isFar = true;
							isComeMonster = false;
							break;
						}
					}
				}
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public string getNameMonsterRepeatQuest(MainQuest quest)
	{
		try
		{
			string result = string.Empty;
			int num = quest.vecTypeQuest.size();
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					DataQuest dataQuest = (DataQuest)quest.vecTypeQuest.elementAt(i);
					if (dataQuest.numMax > 0 && dataQuest.numCur < dataQuest.numMax && quest.typeMainSub != 0)
					{
						result = dataQuest.nameItem;
						break;
					}
				}
			}
			return result;
		}
		catch (Exception)
		{
			return string.Empty;
		}
	}

	public int getStatusRepeatQuest()
	{
		int result = -1;
		MainQuest repeatQuest = getRepeatQuest();
		if (repeatQuest != null)
		{
			result = repeatQuest.statusQuest;
		}
		return result;
	}

	public MainObject findTheQuestBoard()
	{
		MainObject result = null;
		int num = -1;
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject.name != null && mainObject.name.ToLower().Equals(T.nhiemvu.ToLower()) && mainObject.typeObject == 2)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			result = (MainObject)GameScreen.vecPlayers.elementAt(num);
		}
		return result;
	}

	public void startAuto()
	{
		isFindMons = true;
		objectFocus = null;
		if (GameScreen.player.Hp <= 0 || GameScreen.player.Action == 4)
		{
			Player.AutoFireCur = 0;
			return;
		}
		Player.AutoFireCur = 0;
		MainQuest repeatQuest = getRepeatQuest();
		switch (repeatQuest.statusQuest)
		{
		case 1:
			nextStep(repeatQuest);
			break;
		case 2:
			isCheckFinish = true;
			break;
		case 0:
			if (!checkBreadForQuest())
			{
				stopAuto();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.notEnoughBread);
				break;
			}
			moveToQuestBoard();
			if (isCome)
			{
				isStart = false;
				isCome = false;
				GameCanvas.clearAll();
				nextStep(repeatQuest);
			}
			break;
		}
	}

	public void checkFinishProcess()
	{
		if (GameScreen.player.Hp <= 0 || GameScreen.player.Action == 4)
		{
			Player.AutoFireCur = 0;
			return;
		}
		MainQuest repeatCurrentQuest = getRepeatCurrentQuest();
		if (repeatCurrentQuest != null)
		{
			if (isFindMons)
			{
				objectFocus = findMonsterFocus(repeatCurrentQuest);
			}
			if (objectFocus == null)
			{
				isFindMons = true;
				objectFocus = findMonsterFocusFar(repeatCurrentQuest);
			}
			if (objectFocus == null)
			{
				Player.AutoFireCur = 0;
				isWaitingForMons = true;
				if (!isGoBack)
				{
					if (movePlayer(GameCanvas.loadmap.limitW - 140, toYDefault))
					{
						isGoBack = true;
					}
				}
				else if (movePlayer(GameCanvas.loadmap.limitW / 2, toYDefault))
				{
					isGoBack = false;
				}
			}
			else
			{
				isWaitingForMons = false;
				if (objectFocus.isDie || objectFocus.Hp <= 0)
				{
					objectFocus = null;
					GameScreen.objFocus = null;
					isFindMons = true;
					return;
				}
				if (GameScreen.objFocus != null && !GameScreen.objFocus.isDie && GameScreen.objFocus.Hp > 0)
				{
					_ = GameScreen.objFocus.typeObject;
					_ = 1;
				}
				GameScreen.objFocus = objectFocus;
				if (GameScreen.objFocus != null)
				{
					Player.AutoFireCur = 0;
					if (!isFar)
					{
						GameScreen.player.beginPlayerFire(2);
					}
					if (isFar)
					{
						if (movePlayer(GameScreen.objFocus.x - 100, GameScreen.objFocus.y))
						{
							isComeMonster = true;
						}
						if (isComeMonster)
						{
							GameScreen.player.beginPlayerFire(2);
						}
					}
				}
			}
		}
		MainQuest repeatQuestFinsh = getRepeatQuestFinsh();
		if (repeatQuestFinsh != null)
		{
			Player.AutoFireCur = 0;
			moveToQuestBoard();
			if (isCome)
			{
				if (checkFullItem())
				{
					return;
				}
				GlobalService.gI().quest(4, repeatQuestFinsh.ID);
				Player.idNPCQuestCur = (short)repeatQuestFinsh.idNPC;
			}
		}
		if (checkShowGift())
		{
			isCheckFinish = false;
			isStart = true;
		}
	}

	public bool checkShowGift()
	{
		bool result = false;
		try
		{
			MainDialog currentDialog = GameCanvas.currentDialog;
			if (currentDialog != null)
			{
				MsgShowGift msgShowGift = (MsgShowGift)currentDialog;
				if (msgShowGift != null && msgShowGift.type == 0 && msgShowGift.nameDialog.IndexOf(T.textDoneDialog) >= 0)
				{
					result = true;
				}
			}
			return result;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool checkFullItem()
	{
		bool result = false;
		try
		{
			int num = Player.vecInventory.size();
			if (Player.maxInventory - num < 2)
			{
				result = true;
				stopAuto();
				GameCanvas.end_Dialog();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.fullItem);
			}
			return result;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static void stopAuto()
	{
		Player.AutoFireCur = 0;
		isCheckFinish = false;
		isStart = false;
		GameScreen.isOnRepeatQuest = false;
		GameCanvas.clearAll();
	}

	public bool checkBreadForQuest()
	{
		try
		{
			return Player.ticket >= 3;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public void beforeGetQuest()
	{
		if (!checkBreadForQuest())
		{
			stopAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.notEnoughBread);
		}
	}

	public static int getFocusTheQuestBoard()
	{
		int result = -1;
		try
		{
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject.name != null && mainObject.name.ToLower().Equals(T.nhiemvu.ToLower()) && mainObject.typeObject == 2)
				{
					result = i;
					break;
				}
			}
			return result;
		}
		catch (Exception)
		{
			return -1;
		}
	}

	public static MainObject getTheQuestBoard()
	{
		try
		{
			int focusTheQuestBoard = getFocusTheQuestBoard();
			MainObject result = null;
			if (focusTheQuestBoard >= 0)
			{
				result = (MainObject)GameScreen.vecPlayers.elementAt(focusTheQuestBoard);
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static void step1()
	{
		MainQuest repeatQuest = getRepeatQuest();
		if (repeatQuest != null)
		{
			MainQuest.getQuest(repeatQuest.ID)?.beginQuest((short)repeatQuest.idNPC);
			step = 1;
		}
	}

	public static void step2()
	{
		MainQuest repeatQuest = getRepeatQuest();
		if (repeatQuest != null && step == 1)
		{
			GameCanvas.menuCur.doCloseMenu();
			GlobalService.gI().quest(1, repeatQuest.ID);
			step = 2;
		}
	}

	public static void step3()
	{
		if (step == 2)
		{
			switch (checkGetQuest())
			{
			case 0:
				((iCommand)((MsgDialog)GameCanvas.currentDialog).cmdList.elementAt(0)).perform();
				step = 3;
				break;
			case 1:
				stopAuto();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.offAutoRepeatCondition);
				step = -1;
				break;
			case 2:
				stopAuto();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.offAutoRepeatLevelRequest);
				step = -1;
				break;
			}
		}
	}

	public static void step4()
	{
		if (step == 3 && getRepeatCurrentQuest() != null)
		{
			movePlayer(toXDefault, toYDefault);
			step = 4;
		}
	}

	public static void step5()
	{
		if (step == 4)
		{
			GameCanvas.end_Dialog();
			isCheckFinish = true;
			step = 5;
		}
	}

	public void StartAutoRepeatQuest()
	{
		if (GameScreen.isOnRepeatQuest && isStart && !isCheckFinish)
		{
			GameScreen.isOnRepeatQuest = true;
			if (!Interface_Game.isAutoFireInterface)
			{
				GameScreen.interfaceGame.selectPointer(6);
			}
			startAuto();
		}
		if (GameScreen.isOnRepeatQuest && isCheckFinish)
		{
			GameScreen.isOnRepeatQuest = true;
			if (!Interface_Game.isAutoFireInterface)
			{
				GameScreen.interfaceGame.selectPointer(6);
			}
			checkFinishProcess();
		}
	}

	public static IEnumerator StepDelay()
	{
		int timeSpeedUp1 = (int)(1000f / (1f + (float)GameCanvas.percent / 100f));
		int timeSpeedUp2 = (int)(2000f / (1f + (float)GameCanvas.percent / 100f));
		int timeSpeedUp3 = (int)(3000f / (1f + (float)GameCanvas.percent / 100f));
		int timeSpeedUp4 = (int)(5000f / (1f + (float)GameCanvas.percent / 100f));
		int timeSpeedUp5 = (int)(10000f / (1f + (float)GameCanvas.percent / 100f));
		yield return (object)new WaitForSeconds((float)timeSpeedUp1 / 1000f);
		step1();
		yield return (object)new WaitForSeconds((float)(timeSpeedUp2 - timeSpeedUp1) / 1000f);
		step2();
		yield return (object)new WaitForSeconds((float)(timeSpeedUp3 - timeSpeedUp2) / 1000f);
		step3();
		yield return (object)new WaitForSeconds((float)(timeSpeedUp4 - timeSpeedUp3) / 1000f);
		step4();
		yield return (object)new WaitForSeconds((float)(timeSpeedUp5 - timeSpeedUp4) / 1000f);
		step5();
	}

	public void runNextStep()
	{
		Main.main.AddJob(StepDelay());
	}

	public static bool fBeginAuto()
	{
		if (getRepeatQuest() == null)
		{
			stopAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.noRepeatTask);
			return false;
		}
		return true;
	}
}
