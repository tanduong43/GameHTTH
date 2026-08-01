public class MainMonster : MainObject
{
	public const sbyte MONSTER_MOVE_12 = 0;

	public const sbyte MONSTER_MOVE_234 = 1;

	public const sbyte MONSTER_MOVE_2345 = 2;

	public const sbyte MONSTER_MOVE_012 = 3;

	public const sbyte MONSTER_MOVE_1234 = 4;

	public const sbyte MONSTER_MOVE_23 = 5;

	public const sbyte MONSTER_MOVE_123 = 6;

	public const sbyte MONSTER_MOVE_BUNHIN = 8;

	public const sbyte MONSTER_MOVE_0MOVE = 9;

	public const sbyte MONSTER_MOVE_01 = 10;

	public const sbyte MONSTER_MOVE_2343 = 11;

	public const sbyte MONSTER_MOVE_FLY = 12;

	public const sbyte MONSTER_MOVE_CHOPPER = 13;

	public const sbyte MONSTER_MOVE_BINGO = 14;

	public const sbyte MONSTER_MOVE_KUNGFU = 15;

	public const sbyte MONSTER_MOVE_BANANA = 16;

	public const sbyte MONSTER_MOVE_ICE_SNOW = 17;

	public const sbyte MONSTER_MOVE_POKEMON = 18;

	public const sbyte MONSTER_MOVE_TRU = 19;

	public const sbyte MONSTER_MOVE_BANH_KEM = 20;

	public const sbyte MONSTER_PET_DOG = 21;

	public const sbyte MONSTER_SPEC_NORMAL = 0;

	public const sbyte MONSTER_SPEC_MINI_BOSS = 1;

	public const sbyte MONSTER_SPEC_BOSS = 2;

	public int idCatMonster;

	public int timeAutoAction;

	public new int timeRemove;

	public int frameDie;

	public int timeBienmat;

	public int timeRanChangeAction = 24;

	public int limitMove;

	public int limitAttack;

	public int vyStyleDie;

	public int vyStyleStand;

	public long timedie;

	public long timeMove;

	public long timeStand;

	public static mVector VecCatalogyMonSter = new mVector("MainMonster.vecCatalogyMonSter");

	public static MyHashTable hashMonsterTemplate = new MyHashTable();

	public bool isRunAttack;

	public bool isXuatHien;

	public int timeMaxMoveAttack;

	public long timeBeginMoveAttack;

	public mVector vecEffMapSea = new mVector("MainMonster.vecEffMapSea");

	public mVector vecEffElite = new mVector("MainMonster.vecEffElite");

	public int timezombie;

	public MainMonster(short ID, short idMain, short idImage, sbyte type)
	{
	}

	public MainMonster(short ID, int x, int y)
	{
		base.ID = ID;
		toX = x;
		toY = y;
		toXNew = x;
		toYNew = y;
		typeObject = 1;
	}

	public MainMonster(short ID, int IdCatMonster, sbyte typeMove, string name, int x, int y, int maxHP, int lv, CatalogyMonster cata)
	{
	}

	public static MainMonster createMonster(short ID, int x, int y, short IdCatMonster)
	{
		CatalogyMonster catalogyMonster = getCatalogyMonster(IdCatMonster);
		if (catalogyMonster == null || !catalogyMonster.isTemplate)
		{
			mSystem.outloi("mon cat null");
			return null;
		}
		if (catalogyMonster.isHumanPart == 1)
		{
			return new MonsterHuman(ID, x, y, catalogyMonster);
		}
		return new MonsterWalk(ID, x, y, catalogyMonster);
	}

	public void paintAvatarFocus(mGraphics g, int x, int y)
	{
		MainImage imageAll = ObjectData.getImageAll((short)idCatMonster, ObjectData.HashImageMonster, 1000);
		if (imageAll.img == null)
		{
			return;
		}
		if (wAvatar <= 0)
		{
			if (wOne < 0)
			{
				hOne = mImage.getImageHeight(imageAll.img.image) / nFrame;
				wOne = mImage.getImageWidth(imageAll.img.image);
			}
			wAvatar = wOne;
			hAvatar = hOne;
			if (wAvatar > 26)
			{
				wAvatar = 26;
			}
			if (hAvatar > 26)
			{
				hAvatar = 26;
			}
		}
		g.drawRegion(imageAll.img, wOne / 2 - wAvatar / 2, 0, wAvatar, hAvatar, 0, x, y, 3);
	}

	public void paintEffBoss(mGraphics g, int ylech)
	{
		if (isTanHinh || GameCanvas.lowGraphic)
		{
			return;
		}
		if (typeBossMonster == 1 || typeBossMonster == 2)
		{
			int num = 0;
			if (isHumanMonster == 1)
			{
				num = 2;
				if (type_left_right == 2)
				{
					num = -2;
				}
			}
			AvMain.fraEffBoss.drawFrame((typeBossMonster - 1) * 3 + GameCanvas.gameTick / 3 % 3, x + num, y + ylech, 0, 3, g);
		}
		if (typeMoveMonster == 19 && IdIcon != 58)
		{
		}
	}

	public override void update()
	{
		if (timeBeginUpdateMove >= 0)
		{
			timeBeginUpdateMove--;
		}
		if (isDie && Action != 4)
		{
			Action = 4;
			timedie = GameCanvas.timeNow;
		}
		if (skillCurrent == null)
		{
			setNextSkill();
		}
		if (Hp <= 0)
		{
			if (Action != 4)
			{
				timezombie++;
			}
			if (timezombie > 20)
			{
				Action = 4;
				timedie = GameCanvas.timeNow;
				timezombie = 0;
			}
		}
		x += vx;
		y += vy;
		if (x < 0 || x > GameCanvas.loadmap.maxWMap)
		{
			x = GameCanvas.loadmap.maxWMap / 2;
		}
		getInfo();
		updateBuff();
		updateEffSpec();
		updateChatPopup(dhCharPopup);
	}

	public void updateDirFocusPlayer()
	{
		if (Action != 1 && Action != 0)
		{
			return;
		}
		int distance = MainObject.getDistance(x, y, GameScreen.player.x, GameScreen.player.y);
		if (distance < LoadMap.wTile * 3)
		{
			if (x < GameScreen.player.x)
			{
				type_left_right = 2;
			}
			else
			{
				type_left_right = 0;
			}
		}
		if (distance < 24 && Action == 0)
		{
			if (x < GameScreen.player.x)
			{
				toX = x - 24;
			}
			else
			{
				toX = x + 24;
			}
		}
	}

	public virtual void updateEffMapSea()
	{
		for (int i = 0; i < vecEffMapSea.size(); i++)
		{
			Point point = (Point)vecEffMapSea.elementAt(i);
			point.f++;
			if (point.f / 2 > 2)
			{
				vecEffMapSea.removeElement(point);
				i--;
			}
		}
		if (LoadMap.specMap == 4 && !GameCanvas.lowGraphic && Action == 1 && f % 3 == 0)
		{
			Point point2 = new Point(x + vx, y + vy);
			point2.dis = ((Dir == 2) ? 2 : 0);
			vecEffMapSea.addElement(point2);
		}
	}

	public void setNextSkill()
	{
		if (vecSkillFires.size() > 0)
		{
			skillCurrent = (Start_Skill)vecSkillFires.elementAt(0);
			objMainFocus = skillCurrent.objBefire;
			resetBeginFire();
			vecSkillFires.removeElementAt(0);
			timeBeginMoveAttack = GameCanvas.timeNow;
			if (!isRunAttack)
			{
				skillCurrent.skill = Skilldefault;
				setDataBeginSkill(skillCurrent.skill, skillCurrent.vecObjBeFire);
			}
		}
	}

	public void paint()
	{
	}

	public override void paintOnlyShadown(mGraphics g)
	{
		g.drawImage(MainObject.imgShadow, x, y, 3);
	}

	public static CatalogyMonster getCatalogyMonster(int id)
	{
		CatalogyMonster catalogyMonster = (CatalogyMonster)hashMonsterTemplate.get(string.Empty + id);
		if (catalogyMonster == null)
		{
			catalogyMonster = new CatalogyMonster(id);
			catalogyMonster.timeRequest = GameCanvas.timeNow;
			hashMonsterTemplate.put(string.Empty + id, catalogyMonster);
			GlobalService.gI().GetTemplate(1, (short)id);
		}
		if ((GameCanvas.timeNow - catalogyMonster.timeRequest) / 1000 > 20)
		{
			GlobalService.gI().GetTemplate(1, (short)id);
		}
		return catalogyMonster;
	}

	public void auto_Move()
	{
		if (MainObject.getDistance(x, y, xAnchor, yAnchor) > limitMove + limitMove / 2)
		{
			if (!MainObject.isInScreen(this) && !setIsInScreen(xAnchor, yAnchor, wOne, hOne))
			{
				x = xAnchor;
				y = yAnchor;
				toX = xAnchor;
				toY = yAnchor;
			}
			else
			{
				toX = xAnchor;
				toY = yAnchor;
				move_to_XY();
			}
			return;
		}
		if (!MainObject.isInScreen(this) && !setIsInScreen(xAnchor, yAnchor, wOne, hOne))
		{
			x = xAnchor;
			y = yAnchor;
			toX = xAnchor;
			toY = yAnchor;
			return;
		}
		timeMove++;
		if (Action == 4)
		{
			return;
		}
		if (timeStand > 0)
		{
			timeMove = 0L;
			Action = 0;
			vx = 0;
			vy = 0;
			timeStand--;
		}
		else if (MainObject.getDistance(x + vx, y + vy, GameScreen.player.x, GameScreen.player.y) < 50)
		{
			if (Action == 1)
			{
				if ((timeMove > timeAutoAction / 3 && CRes.random(20) == 0) || MainObject.getDistance(x + vx, y + vy, xAnchor, yAnchor) >= limitMove - vMax)
				{
					timeMove = 0L;
					Action = 0;
					vx = 0;
					vy = 0;
					if (x > GameScreen.player.x)
					{
						Dir = 0;
					}
					else
					{
						Dir = 2;
					}
				}
			}
			else if (Action == 0 || CRes.random(30) == 0)
			{
				vx = 0;
				vy = 0;
				if (timeMove > timeAutoAction)
				{
					timeMove = 0L;
					Action = 1;
					Dir = CRes.random(4);
					setSpeed8Huong(vMax - 2);
				}
				if (x > GameScreen.player.x)
				{
					Dir = 0;
				}
				else
				{
					Dir = 2;
				}
			}
		}
		else if (Action == 1)
		{
			if ((timeMove > timeAutoAction / 2 && CRes.random(timeRanChangeAction) == 0) || MainObject.getDistance(x + vx, y + vy, xAnchor, yAnchor) >= limitMove - vMax)
			{
				timeMove = 0L;
				Action = 0;
				vx = 0;
				vy = 0;
			}
		}
		else if (Action == 0)
		{
			vx = 0;
			vy = 0;
			if (timeMove > timeAutoAction / 2 || CRes.random(timeRanChangeAction) == 0)
			{
				timeMove = 0L;
				Action = 1;
				Dir = CRes.random(4);
				setSpeed8Huong(vMax);
			}
		}
		if (MainObject.getDistance(x, y, xAnchor, yAnchor) <= limitMove)
		{
			return;
		}
		int num = CRes.abs(x - xAnchor);
		int num2 = CRes.abs(y - yAnchor);
		if (num > num2)
		{
			if (x > xAnchor)
			{
				Dir = 0;
			}
			else
			{
				Dir = 2;
			}
		}
		else if (y > yAnchor)
		{
			Dir = 1;
		}
		else
		{
			Dir = 3;
		}
		setSpeed8Huong(vMax);
	}

	public void setSpeed8Huong(int max)
	{
		int a = CRes.random_Am_0(3);
		if (CRes.abs(a) > 1)
		{
			max--;
		}
		switch (Dir)
		{
		case 1:
			vy = -max;
			vx = a;
			break;
		case 3:
			vy = max;
			vx = a;
			break;
		case 0:
			vy = a;
			vx = -max;
			break;
		case 2:
			vy = a;
			vx = max;
			break;
		}
		if (vx == 0 && CRes.random(3) == 0)
		{
			timeMove = 0L;
			Action = 0;
			vx = 0;
			vy = 0;
		}
		if (vx > 0)
		{
			Dir = 2;
		}
		else
		{
			Dir = 0;
		}
	}

	public void autoMoveFire()
	{
		timeMove++;
		if (Action == 4)
		{
			return;
		}
		if (Action == 1)
		{
			if (timeMove <= timeAutoAction && CRes.random(16) != 0)
			{
				return;
			}
			timeMove = 0L;
			Action = 0;
			vx = 0;
			vy = 0;
			if (objMainFocus != null)
			{
				if (x > objMainFocus.x)
				{
					Dir = 0;
				}
				else
				{
					Dir = 2;
				}
			}
		}
		else if (Action == 0)
		{
			vx = 0;
			vy = 0;
			if (timeMove > timeAutoAction / 2 || CRes.random(12) == 0)
			{
				timeMove = 0L;
				Action = 1;
				Dir = CRes.random(4);
				getSpeedforRun(vMax);
			}
		}
	}

	public void Move_to_Focus()
	{
		if (skillCurrent == null)
		{
			return;
		}
		if (GameCanvas.timeNow - timeBeginMoveAttack > timeMaxMoveAttack)
		{
			skillCurrent.skill = Skilldefault;
			setDataBeginSkill(skillCurrent.skill, skillCurrent.vecObjBeFire);
			return;
		}
		if (skillCurrent.objBefire == null)
		{
			skillCurrent = null;
			return;
		}
		toX = skillCurrent.objBefire.x;
		toY = skillCurrent.objBefire.y;
		if (MainObject.getDistance(x + vx, y + vy, skillCurrent.objBefire.x, skillCurrent.objBefire.y) <= limitAttack)
		{
			setDataBeginSkill(skillCurrent.skill, skillCurrent.vecObjBeFire);
		}
		else if (CRes.abs(x - toX) >= 4 || CRes.abs(y - toY) >= 4)
		{
			move_to_XY_Normal();
		}
	}

	public override void updateActionFire()
	{
		if (plashNow != null)
		{
			int num = plashNow.update();
			if (num == -1)
			{
				plashNow = null;
				if (skillCurrent == null)
				{
					setNextSkill();
				}
				if (skillCurrent == null)
				{
					Action = 0;
					resetAction();
				}
			}
			else
			{
				frame = num;
			}
		}
		else
		{
			Action = 0;
			resetAction();
		}
	}

	public void Move_To_Player_Skill()
	{
	}

	public override void Reveive()
	{
		if (typeMoveMonster != 19)
		{
			ReveiceMonster();
		}
		isRunAttack = false;
		objMainFocus = null;
		isFlyDie = false;
		vecSkillFires.removeAllElements();
		vecBuffCur.removeAllElements();
		vecEffspec.removeAllElements();
		setResetWearing();
		if (isHumanMonster == 0)
		{
			resetXY();
		}
		frameDie = 0;
		timeBeginUpdateMove = -1;
		timeFreeFire = 0;
		timeBienmat = 10;
		timezombie = 0;
		f = 0;
		Action = 0;
		timeMove = 0L;
		isDie = false;
		vxDie = 0;
		vyDie = 0;
		Hp = maxHp;
		vyStyleDie = (vyStyleStand = 0);
	}

	public virtual void ReveiceMonster()
	{
		int num = 0;
		bool flag;
		do
		{
			x = xAnchor + CRes.random_Am_0(48);
			y = yAnchor + CRes.random_Am_0(48);
			int tile = GameCanvas.loadmap.getTile(x, y);
			flag = tile != 1 && tile != -1;
			num++;
			if (num > 15)
			{
				flag = true;
				x = xAnchor;
				y = yAnchor;
			}
		}
		while (!flag);
	}

	public void updateDieMonsterNormal()
	{
		if (Action != 4)
		{
			return;
		}
		frameDie++;
		if (isDie)
		{
			return;
		}
		x += vxDie;
		y += vyDie;
		if (vyStyleDie > 0)
		{
			vyStyleDie -= 3;
			if (vyStyleDie <= 0 && vyStyleStand > 2)
			{
				vyStyleStand -= 2;
				vyStyleDie = vyStyleStand;
				if (CRes.abs(vxDie) > 1)
				{
					vxDie -= vxDie / CRes.abs(vxDie);
				}
				if (CRes.abs(vyDie) > 1)
				{
					vyDie -= vyDie / CRes.abs(vyDie);
				}
			}
		}
		else
		{
			vxDie >>= 1;
			vyDie >>= 1;
		}
		if (frameDie >= timeBienmat)
		{
			GameScreen.addEffectEnd(92, 0, x, y - hOne / 2, (sbyte)Dir, this);
			isDie = true;
		}
	}

	public void setRevice()
	{
		if (Action != 4)
		{
			return;
		}
		if (GameCanvas.gameTick % 10 == 0 && timeRevice != -2)
		{
			if (timeRevice > 0)
			{
				if (isDie && (GameCanvas.timeNow - timedie) / 1000 > timeRevice)
				{
					Reveive();
				}
			}
			else
			{
				isRemove = true;
			}
		}
		if (strChatPopup != null)
		{
			strChatPopup = null;
		}
	}

	public void resetV()
	{
		vx = 0;
		vy = 0;
		toX = x;
		toY = y;
	}

	public void startDie()
	{
	}

	public virtual void beginFire()
	{
		if (skillCurrent != null && skillCurrent.skill != null && skillCurrent.vecObjBeFire != null)
		{
			GameScreen.addEffectSkill(skillCurrent.skill, this, skillCurrent.vecObjBeFire);
			Action = 2;
			f = 0;
			skillCurrent = null;
		}
	}

	public override void beginDie(MainObject objFire)
	{
		timeBienmat = 10;
		Hp = 0;
		Action = 4;
		timedie = GameCanvas.timeNow;
		resetXY();
		if (skillCurrent != null && skillCurrent.vecObjBeFire != null && Skilldefault != null)
		{
			GameScreen.addEffectSkill(Skilldefault, this, skillCurrent.vecObjBeFire);
		}
		int num;
		for (num = 0; num < vecSkillFires.size(); num++)
		{
			skillCurrent = (Start_Skill)vecSkillFires.elementAt(num);
			if (skillCurrent != null && skillCurrent.vecObjBeFire != null && Skilldefault != null)
			{
				GameScreen.addEffectSkill(Skilldefault, this, skillCurrent.vecObjBeFire);
			}
			vecSkillFires.removeElement(skillCurrent);
			num--;
		}
		skillCurrent = null;
		if (typeMoveMonster == 19)
		{
			GameScreen.addEffectEnd(110, 0, x, y - 30, (sbyte)Dir, this);
			GameScreen.addEffectEnd(110, 0, x, y - 45, (sbyte)Dir, this);
			isDie = true;
			timeDie = 0L;
		}
		else if (objFire != null)
		{
			if (isHumanMonster != 0)
			{
				base.beginDie(objFire);
			}
			else
			{
				startMonDeadFly(this, objFire.ID, 0);
			}
		}
	}

	public static void startMonDeadFly(MainMonster m, int attacker, int typeDie)
	{
		MainObject mainObject = MainObject.get_Object(attacker, 0);
		if (mainObject == null)
		{
			return;
		}
		int time = 10;
		int vyStyle = 0;
		if (m == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		if (mainObject != null)
		{
			num = (m.x - mainObject.x) * 2;
			num2 = (m.y - mainObject.y) * 2;
			while (MainObject.getDistance(num, num2) > 20)
			{
				num = num * 2 / 3;
				num2 = num2 * 2 / 3;
			}
			if (typeDie == 1)
			{
				num *= 2;
				num2 *= 2;
			}
			else if (typeDie == 2)
			{
				while (MainObject.getDistance(num, num2) > 16)
				{
					num = num * 2 / 3;
					num2 = num2 * 2 / 3;
				}
				time = 20;
				vyStyle = 18;
			}
		}
		m.DeadFly(num, num2, time, vyStyle);
	}

	public void DeadFly(int dx, int dy, int time, int vyStyle)
	{
		timedie = GameCanvas.timeNow;
		Action = 4;
		vx = 0;
		vy = 0;
		vxDie = dx;
		vyDie = dy;
		vyStyleDie = vyStyle;
		vyStyleStand = vyStyle;
		timeBienmat = time;
		isDie = false;
	}

	public override void setDataBeginSkill(MainSkill skill, mVector vec)
	{
		beginFire();
	}

	public override void addSkillFire(MainSkill skill, mVector vec)
	{
		if (skillCurrent != null)
		{
			skillCurrent.beginSkill();
		}
		vecSkillFires.addElement(new Start_Skill(this, vec, skill));
		isRunAttack = true;
	}

	public override void setTouchPoint()
	{
		if (Action != 4 && !GameCanvas.loadmap.mapLang() && (!isTru() || GameScreen.player.typePK != typePK))
		{
			GameScreen.player.beginPlayerFirePoint();
		}
	}

	public override void setFireObject(int value)
	{
		if (Action != 4 && !GameCanvas.loadmap.mapLang() && (!isTru() || GameScreen.player.typePK != typePK))
		{
			GameScreen.player.beginPlayerFire(value);
		}
	}

	public void getInfo()
	{
		if (!isInfo && GameCanvas.gameTick % 20 == 0 && (GameCanvas.timeNow - timeLoadInfo) / 1000 > 5)
		{
			timeLoadInfo = GameCanvas.timeNow;
			GlobalService.gI().monster_info(ID);
		}
		if (!isLoadTemplate && GameCanvas.gameTick % 20 == 0)
		{
			CatalogyMonster catalogyMonster = getCatalogyMonster(idCatMonster);
			if (catalogyMonster.isTemplate)
			{
				loadInfoAgain(catalogyMonster);
			}
		}
	}

	public void setAddEffElite()
	{
	}

	public override int getTypeMoveMonster()
	{
		return typeMoveMonster;
	}

	public void updateEffElite()
	{
		if (typeSpecMonSter == 1 && GameCanvas.gameTick % 300 == 0)
		{
			GameScreen.addEffectEnd_Time(111, 0, x, y, ID, typeObject, (sbyte)Dir, this, 0);
		}
		for (int i = 0; i < vecEffElite.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEffElite.elementAt(i);
			mainEffect.update();
		}
	}

	public void paintEffElite(mGraphics g)
	{
		for (int i = 0; i < vecEffElite.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEffElite.elementAt(i);
			mainEffect.paint(g, x, y);
		}
	}

	public static void LoadInfoMonsterAgain()
	{
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject.typeObject == 1 && !mainObject.isLoadTemplate)
			{
				CatalogyMonster catalogyMonster = (CatalogyMonster)hashMonsterTemplate.get(string.Empty + mainObject.ID);
				if (catalogyMonster.isTemplate)
				{
					mainObject.loadInfoAgain(catalogyMonster);
				}
			}
		}
	}
}
