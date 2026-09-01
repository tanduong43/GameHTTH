using System;

public class AutoBattlefield : MainObject
{
	public static bool isFar;

	public static int dFocus = 140;

	public static bool isChangeFocus;

	public static bool isGoBack;

	public static bool isComeTarget;

	public static MainObject findEnemyFocus()
	{
		try
		{
			MainObject result = null;
			int num = GameScreen.player.typePK;
			int num2 = ((GameScreen.player.clan != null) ? GameScreen.player.clan.ID : (-1));
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject != null && mainObject.ID != GameScreen.player.ID)
				{
					int num3 = mainObject.typePK;
					int distance = MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, mainObject.x, mainObject.y);
					int num4 = ((mainObject.clan == null) ? (-1) : mainObject.clan.ID);
					if (num > 0 && (num3 != num || (num3 == 3 && num2 != num4)) && num3 != -1 && mainObject.typeObject == 0 && (!mainObject.isDie || mainObject.Hp > 0) && distance < dFocus)
					{
						result = mainObject;
						isFar = false;
						break;
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

	public static MainObject findEnemyFocusFar()
	{
		try
		{
			MainObject result = null;
			int num = GameScreen.player.typePK;
			int num2 = ((GameScreen.player.clan != null) ? GameScreen.player.clan.ID : (-1));
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject != null && mainObject.ID != GameScreen.player.ID)
				{
					int num3 = mainObject.typePK;
					int distance = MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, mainObject.x, mainObject.y);
					int num4 = ((mainObject.clan == null) ? (-1) : mainObject.clan.ID);
					if (num > 0 && (num3 != num || (num3 == 3 && num2 != num4)) && num3 != -1 && mainObject.typeObject == 0 && (!mainObject.isDie || mainObject.Hp > 0) && distance >= dFocus)
					{
						result = mainObject;
						isFar = false;
						break;
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

	public static bool checkMapPb()
	{
		int idMap = GameCanvas.loadmap.idMap;
		if (idMap == 157 || idMap == 159 || idMap == 161 || idMap == 163 || idMap == 165)
		{
			return true;
		}
		return false;
	}

	public static void CheckMapAutoBattlefield()
	{
		if (checkMapPb())
		{
			GameScreen.isOnAutoPB = true;
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.notifyOnAutoPB);
		}
		else
		{
			StopAutoBattlefield();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.noAutoPB);
		}
	}

	public static void StartAutoBattlefield()
	{
		if (!GameScreen.isOnAutoPB || !isChangeFocus)
		{
			return;
		}
		MainObject mainObject = findEnemyFocus();
		isFar = false;
		isComeTarget = false;
		if (mainObject == null)
		{
			mainObject = findEnemyFocusFar();
			isFar = true;
		}
		if (mainObject != null)
		{
			GameScreen.objFocus = mainObject;
			if (GameScreen.player.Hp <= 0 || GameScreen.player.Action == 4)
			{
				Player.AutoFireCur = 0;
				return;
			}
			GameScreen.player.beginPlayerFire(2);
			isChangeFocus = false;
		}
	}

	public static void StopAutoBattlefield()
	{
		GameScreen.isOnAutoPB = false;
		isChangeFocus = false;
	}

	public static bool movePlayer(int toX, int toY)
	{
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
}
