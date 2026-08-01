public class Start_Skill
{
	public MainObject objFire;

	public MainObject objBefire;

	public mVector vecObjBeFire;

	public MainSkill skill;

	public bool isRemove;

	private bool isVeryFar;

	private int indexMove;

	public Start_Skill(MainObject objMain, mVector vec, MainSkill skill)
	{
		objFire = objMain;
		vecObjBeFire = vec;
		this.skill = skill;
		Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjBeFire.elementAt(0);
		objBefire = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
		if (skill.typeBuff == 0)
		{
			if (objFire == null || objFire.returnAction() || ((vecObjBeFire == null || vecObjBeFire.size() == 0) && skill.typeBuff != 3) || objBefire == null || objBefire.returnAction())
			{
				isRemove = true;
			}
			else if (objFire != GameScreen.player)
			{
				this.skill.getData();
				objMain.timeBeginUpdateMove = -1;
			}
		}
	}

	public void setMoveFire()
	{
		if (skill.typeBuff != 0)
		{
			beginSkill();
			return;
		}
		if (objBefire == null || isVeryFar)
		{
			isRemove = true;
			return;
		}
		int distance = MainObject.getDistance(objFire.x, objFire.y, objBefire.x, objBefire.y);
		int goc = CRes.angle(objFire.x - objBefire.x, objFire.y - objBefire.y);
		if ((CRes.abs(distance) < skill.range && setGoc(goc) && objFire.posTransRoad == null) || indexMove >= 2)
		{
			beginSkill();
			return;
		}
		indexMove++;
		int num = 0;
		int num2 = 0;
		int num3 = 64;
		if (num3 > skill.range - 8)
		{
			num3 = skill.range - 8;
		}
		int num4 = 60;
		int num5 = 5;
		int num6 = 0;
		num4 = 90;
		num5 = num4 / 12;
		num6 = 0;
		int num7 = 360 + num6;
		if (objFire.x < objBefire.x)
		{
			num7 = 180 - num6;
		}
		int num8;
		int num9;
		int tile;
		do
		{
			num8 = objBefire.x + CRes.getcos(CRes.fixangle(num7 + num2)) * num3 / 1000;
			num9 = objBefire.y + CRes.getsin(CRes.fixangle(num7 + num2)) * num3 / 1000;
			tile = GameCanvas.loadmap.getTile(num8, num9);
			num2 = ((num % 2 != 0) ? (-(num % 6 + 1) * num5) : ((num % 6 + 1) * num5));
			num++;
			if (num == 6)
			{
				num3 = 40;
				if (num3 > skill.range - 8)
				{
					num3 = skill.range - 8;
				}
				num7 = ((num7 != 180) ? (180 - num6) : (360 + num6));
				num2 = 0;
			}
			if (num > 12)
			{
				tile = 0;
				num8 = objBefire.x + CRes.getcos(CRes.fixangle(0)) * num3 / 1000;
				num9 = objBefire.y + CRes.getsin(CRes.fixangle(0)) * num3 / 1000;
				objFire.posTransRoad = null;
				break;
			}
			if (num % 2 == 0 && num3 < skill.range - 8 - LoadMap.wTile / 2)
			{
				num3 += LoadMap.wTile / 2;
			}
		}
		while (tile == -1 || tile == 1);
		if (objFire == GameScreen.player)
		{
			objBefire.timeBeFire = 10;
			movePlayer(distance, num8, num9);
		}
		else
		{
			objFire.toX = num8;
			objFire.toY = num9;
		}
	}

	public bool setGoc(int gocCur)
	{
		int num = 30;
		int num2 = 30;
		num = 45;
		num2 = 45;
		if ((CRes.fixangle(gocCur) <= 180 + num && CRes.fixangle(gocCur) >= 180 - num2) || CRes.fixangle(gocCur) >= 360 - num || CRes.fixangle(gocCur) <= num2)
		{
			return true;
		}
		return false;
	}

	public void beginSkill()
	{
		if (objFire == null)
		{
			isRemove = true;
			return;
		}
		if (skill.typeBuff == 3)
		{
			objFire.setDataBeginSkill(skill, vecObjBeFire);
			isRemove = true;
			return;
		}
		if (skill.typeBuff == 0 && LoadMap.specMap != 4 && objBefire != null)
		{
			int distance = MainObject.getDistance(objFire.x, objFire.y, objBefire.x, objBefire.y);
			if (distance <= 32 || (distance <= 48 && objFire.clazz == 5))
			{
				if (CRes.random(8) == 0)
				{
					if (objFire.clazz == 5)
					{
						if (objFire.x < objBefire.x)
						{
							objFire.x = objBefire.x + 48;
						}
						else
						{
							objFire.x = objBefire.x - 48;
						}
					}
					else if (objFire.x < objBefire.x)
					{
						objFire.x = objBefire.x + 32;
					}
					else
					{
						objFire.x = objBefire.x - 32;
					}
					objFire.type_left_right = ((objFire.type_left_right == 0) ? 2 : 0);
					GameScreen.addEffectEnd_ObjTo(56, 0, objFire.x, objFire.y, objFire.ID, objFire.typeObject, (sbyte)objFire.type_left_right, objFire);
				}
				else if (objFire.clazz == 5)
				{
					if (objFire.x < objBefire.x)
					{
						objFire.x = objBefire.x - 48;
					}
					else
					{
						objFire.x = objBefire.x + 48;
					}
				}
				else if (objFire.x < objBefire.x)
				{
					objBefire.x = objFire.x + 32;
				}
				else
				{
					objBefire.x = objFire.x - 32;
				}
			}
		}
		if (objFire == GameScreen.player && LoadMap.specMap != 3)
		{
			GameScreen.player.setUseMana(skill.mana);
			Player.setDelaySkill(skill.indexHotKey, skill.timeDelay, skill.isBuffSpecNew, 1);
			if (skill.typeBuff != 0)
			{
				if (objBefire != null)
				{
					GlobalService.gI().Buff(skill.ID, objBefire.typeObject, vecObjBeFire);
				}
			}
			else
			{
				if (objBefire != null)
				{
					GlobalService.gI().Obj_Move((short)GameScreen.player.x, (short)GameScreen.player.y);
					GlobalService.gI().Player_Fire(skill.ID, objBefire.typeObject, vecObjBeFire);
				}
				if (skill.typeDevil == 0)
				{
					Player.indexCombo++;
					Interface_Game.indexEffCombo = Player.indexCombo;
					Interface_Game.frameCombo = 0;
				}
			}
		}
		objFire.setDataBeginSkill(skill, vecObjBeFire);
		isRemove = true;
	}

	public void movePlayer(int distant, int toX, int toY)
	{
		GameScreen.player.xStopMove = 0;
		GameScreen.player.yStopMove = 0;
		GameScreen.player.toX = GameScreen.player.x;
		GameScreen.player.toY = GameScreen.player.y;
		GameScreen.player.countAutoMove = 0;
		if (GameScreen.player.posTransRoad != null)
		{
			GameScreen.player.countAutoMove = 1;
		}
		GameScreen.player.posTransRoad = GameCanvas.loadmap.updateFindRoad(toX / LoadMap.wTile, toY / LoadMap.wTile, GameScreen.player.x / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, 16, GameScreen.player);
		if (GameScreen.player.posTransRoad != null && GameScreen.player.posTransRoad.Length > 16)
		{
			isVeryFar = true;
			GameScreen.player.posTransRoad = null;
			return;
		}
		if (GameScreen.player.posTransRoad == null)
		{
			GameScreen.player.toX = toX;
			GameScreen.player.toY = toY;
			if (MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, GameScreen.player.toX, GameScreen.player.toY) < GameScreen.player.vMax * 2)
			{
				GameScreen.player.x = toX;
				GameScreen.player.y = toY;
				beginSkill();
			}
			else
			{
				GameScreen.player.isMoveNor = true;
			}
		}
		if (toX > GameScreen.player.x)
		{
			GameScreen.player.Dir = 2;
		}
		else
		{
			GameScreen.player.Dir = 0;
		}
	}

	public void InviMove()
	{
		objFire.isTanHinh = true;
		objFire.timeInviMove = 2;
	}

	public void setMonsterMoveFire()
	{
		if (skill.typeBuff != 0)
		{
			if (skill.typeBuff == 2)
			{
				if (indexMove == 0)
				{
					indexMove = 1;
					objFire.toX = skill.x;
					objFire.toY = skill.y;
					return;
				}
				if (MainObject.getDistance(objFire.x, objFire.y, skill.x, skill.y) >= 24)
				{
					GameScreen.addEffectEnd_ObjTo(56, 0, skill.x, skill.y, objFire.ID, objFire.typeObject, (sbyte)objFire.Dir, objFire);
				}
				beginSkill();
			}
			else
			{
				beginSkill();
			}
			return;
		}
		if (objBefire == null)
		{
			isRemove = true;
			return;
		}
		if (indexMove == 1 || objFire.vecSkillFires.size() >= 1)
		{
			beginSkill();
			objFire.toX = objFire.x;
			objFire.toY = objFire.y;
			return;
		}
		int distance = MainObject.getDistance(objFire.x, objFire.y, objBefire.x, objBefire.y);
		int goc = CRes.angle(objFire.x - objBefire.x, objFire.y - objBefire.y);
		if (CRes.abs(distance) < skill.range && setGoc(goc))
		{
			beginSkill();
			return;
		}
		indexMove = 1;
		int angle = 180;
		int num = skill.range - 5;
		if (CRes.random(2) == 0)
		{
			angle = 0;
		}
		int toX = objBefire.x + CRes.getcos(CRes.fixangle(angle)) * num / 1000;
		int toY = objBefire.y + CRes.getsin(CRes.fixangle(angle)) * num / 1000;
		objFire.toX = toX;
		objFire.toY = toY;
	}
}
