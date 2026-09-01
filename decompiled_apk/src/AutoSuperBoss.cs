public class AutoSuperBoss : MainObject
{
	public static bool isStart;

	public static bool isKillBoss;

	public static string lastName = string.Empty;

	public const int wFocus = 160;

	public bool isCome;

	public static bool isMove;

	public void startAuto()
	{
		MainObject mainObject = findSuperBoss();
		if (mainObject != null)
		{
			if (GameScreen.objFocus == null || GameScreen.objFocus.isDie || GameScreen.objFocus.Hp <= 0 || GameScreen.objFocus.typeObject != 1 || GameScreen.objFocus.typeSpecMonSter != 1 || GameScreen.objFocus.typeBossMonster != 2)
			{
				GameScreen.objFocus = mainObject;
			}
			if (GameScreen.objFocus != null)
			{
				lastName = GameScreen.objFocus.name;
				if (MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, GameScreen.objFocus.x, GameScreen.objFocus.y) > 180)
				{
					isStart = false;
					isMove = true;
				}
				else
				{
					GameScreen.player.beginPlayerFire(2);
				}
			}
		}
		else if (lastName.Equals(string.Empty))
		{
			stopAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.notifyNoSuperBoss);
		}
	}

	public void checkKillBoss()
	{
		MainObject objFocus = GameScreen.objFocus;
		if (objFocus != null && lastName.IndexOf("10") >= 0 && (objFocus.isDie || objFocus.Hp <= 0))
		{
			isKillBoss = true;
			stopAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.notifyKillSuperBoss);
		}
	}

	public void moveToSuperBoss()
	{
		MainObject mainObject = findSuperBoss();
		if (mainObject == null)
		{
			return;
		}
		int distance = MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, mainObject.x, mainObject.y);
		if (mainObject != null)
		{
			GameScreen.player.toX = mainObject.x;
			GameScreen.player.toY = mainObject.y;
			if (MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, GameScreen.player.toX, GameScreen.player.toY) < GameScreen.player.vMax * 2)
			{
				GameScreen.player.x = mainObject.x;
				GameScreen.player.y = mainObject.y;
			}
			else
			{
				GameScreen.player.isMoveNor = true;
			}
			isCome = distance < 160;
		}
	}

	public static void stopAuto()
	{
		Player.AutoFireCur = 0;
		lastName = string.Empty;
		isStart = false;
		isKillBoss = false;
		isMove = false;
		GameScreen.isOnSuperBoss = false;
		GameCanvas.clearAll();
	}

	public MainObject findSuperBoss()
	{
		MainObject result = null;
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject.typeObject == 1 && mainObject.typeSpecMonSter == 1 && mainObject.typeBossMonster == 2 && mainObject.Hp > 0)
			{
				result = mainObject;
				break;
			}
		}
		return result;
	}

	public void StartAutoSuperBoss()
	{
		if (GameScreen.isOnSuperBoss && isStart && !isKillBoss)
		{
			GameScreen.isOnSuperBoss = true;
			if (!Interface_Game.isAutoFireInterface)
			{
				GameScreen.interfaceGame.selectPointer(6);
			}
			startAuto();
			checkKillBoss();
		}
		if (GameScreen.isOnSuperBoss && isMove)
		{
			if (!Interface_Game.isAutoFireInterface)
			{
				GameScreen.interfaceGame.selectPointer(6);
			}
			moveToSuperBoss();
			if (isCome)
			{
				isMove = false;
				isStart = true;
			}
		}
	}
}
