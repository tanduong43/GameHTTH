public class MonsterHuman : MainMonster
{
	public MonsterHuman(short ID, int x, int y, CatalogyMonster cata)
		: base(ID, x, y)
	{
		timeLoadInfo = GameCanvas.timeNow;
		typeObject = 1;
		clazz = 6;
		idCatMonster = cata.idCat;
		base.ID = ID;
		xAnchor = x;
		yAnchor = y;
		base.x = x;
		base.y = y;
		toX = x;
		toY = y;
		toXNew = x;
		toYNew = y;
		downSpeedWater = 1;
		Dir = 0;
		hIconFocus = 0;
		if (LoadMap.specMap == 4)
		{
			setSpeed(4, 3);
			wOne = 100;
		}
		else if (LoadMap.specMap == 6)
		{
			setSpeed(3, 3);
			wOne = 26;
		}
		else
		{
			setSpeed(5, 5);
			wOne = 26;
		}
		limitMove = 60;
		timeAutoAction = CRes.random(100, 150);
		timeRanChangeAction = 80;
		timeMaxMoveAttack = 3000;
		limitAttack = 30;
		xsai = 0;
		ysai = 0;
		if (cata.isTemplate)
		{
			loadInfoAgain(cata);
		}
		setWName();
		setAddEffElite();
	}

	private void setBoss()
	{
		if (idCatMonster == 4)
		{
			vMax = 4;
			limitMove = 80;
		}
	}

	public override void paint(mGraphics g)
	{
		if (!isLoadTemplate)
		{
			g.drawImage(AvMain.imgHinhnhan, x, y, 33);
		}
		else if (Action == 4)
		{
			if (!isDie)
			{
				paintShadowMonster(g, xDie - 1, yDie, typeShadow);
				paintChar(g, xDie, yDie - dyDie);
			}
		}
		else if (!isDie)
		{
			paintEffBoss(g, 0);
			paintCharAllPart(g, typeShadow);
			paintEffElite(g);
		}
	}

	public override void update()
	{
		updateActionPerson();
		if (updateXuatHien())
		{
			return;
		}
		if (timeBeFire > 0)
		{
			timeBeFire--;
			vx = 0;
			vy = 0;
		}
		else if (Action != 4)
		{
			if (skillCurrent != null)
			{
				if (Action != 2)
				{
					move_to_XY_Normal();
				}
				if (skillCurrent.isRemove)
				{
					skillCurrent = null;
				}
				else if (CRes.abs(x - toX) < vMax && CRes.abs(y - toY) < vMax)
				{
					skillCurrent.setMonsterMoveFire();
				}
			}
			else if (Action != 2 && Action != 3 && plashNow == null)
			{
				if (!MainObject.isInScreen(this) && !setIsInScreen(toX, toY, wOne, hOne) && LoadMap.specMap != 6)
				{
					x = toX;
					y = toY;
					toX = toXNew;
					toY = toYNew;
					vx = 0;
					vy = 0;
					if (Action != 4)
					{
						Action = 0;
					}
				}
				else
				{
					Move_to_Focus_Person();
				}
			}
			else if (plashNow != null && Action != 2)
			{
				Action = 2;
			}
			if (Action != 2 && Action != 3)
			{
				int tile = GameCanvas.loadmap.getTile(x + vx, y + vy);
				setMove(downSpeedWater, tile);
			}
		}
		setEffDie();
		setRevice();
		base.update();
		updateDy();
		updatevXEffAva();
		if (LoadMap.specMap != 6)
		{
			updateDirFocusPlayer();
		}
		updateEffElite();
	}

	public override void setDataBeginSkill(MainSkill skill, mVector vec)
	{
		plashNow = new Plash(skill, this, vec);
		resetBeginFire();
		Action = 2;
	}

	public override void updateAva()
	{
		if (f > feAva.Length - 1)
		{
			Action = 0;
			f = 0;
			frame = feRun[f];
		}
		else
		{
			frame = feAva[f];
		}
	}

	public override void beginFire()
	{
		if (skillCurrent != null)
		{
			setDataBeginSkill(skillCurrent.skill, skillCurrent.vecObjBeFire);
			skillCurrent.isRemove = true;
		}
	}

	public bool updateXuatHien()
	{
		if (isXuatHien)
		{
			move_to_XY_Normal();
			x += vx;
			y += vy;
			if (vx == 0 && vy == 0)
			{
				setSpeed(5, 5);
				isXuatHien = false;
			}
		}
		else if (vMax > 5)
		{
			setSpeed(5, 5);
		}
		return isXuatHien;
	}

	public override void ReveiceMonster()
	{
		isXuatHien = true;
		toXNew = x;
		toYNew = y;
		vx = 0;
		vy = 0;
		setSpeed(10, 10);
		int num = 0;
		bool flag;
		do
		{
			toX = xAnchor + CRes.random_Am_0(48);
			toY = yAnchor + CRes.random_Am_0(48);
			int tile = GameCanvas.loadmap.getTile(toX, toY);
			flag = tile != 1 && tile != -1;
			num++;
			if (num > 15)
			{
				flag = true;
				toX = xAnchor;
				toY = yAnchor;
			}
		}
		while (!flag);
		if (CRes.random(2) == 0)
		{
			x = MainScreen.cameraMain.xCam - 30;
		}
		else
		{
			x = MainScreen.cameraMain.xCam + MotherCanvas.w + 30;
		}
		if (x < 0)
		{
			x = 0;
		}
		if (x > GameCanvas.loadmap.maxWMap)
		{
			x = GameCanvas.loadmap.maxWMap;
		}
		y = toY;
		if (!setIsInScreen(toX, toY, wOne, hOne))
		{
			isXuatHien = false;
			setSpeed(5, 5);
			x = toX;
			y = toY;
		}
	}

	public override void loadInfoAgain(CatalogyMonster cata)
	{
		typeMoveMonster = cata.typeMove;
		name = cata.name;
		if (LvThongThao > 0)
		{
			name = cata.name + T.bat + LvThongThao;
		}
		maxHp = cata.maxHp;
		isHumanMonster = cata.isHumanPart;
		typeBossMonster = cata.typeMonster;
		Hp = cata.maxHp;
		Lv = cata.lv;
		hOne = cata.hOne;
		sethead(cata.head);
		sethair(cata.hair);
		setWearing(cata.mWearing);
		if (body == 778 || body == 781 || body == 784 || body == 788 || body == 791 || body == 794)
		{
			typeShadow = 4;
		}
		else
		{
			typeShadow = 0;
		}
		isLoadTemplate = true;
	}
}
