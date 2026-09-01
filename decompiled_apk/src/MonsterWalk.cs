public class MonsterWalk : MainMonster
{
	public static int[][] mMon012 = new int[5][]
	{
		new int[6] { 0, 0, 0, 0, 1, 1 },
		new int[6] { 1, 1, 0, 0, 2, 2 },
		new int[8] { 3, 3, 3, 3, 2, 2, 2, 2 },
		new int[10] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
		new int[6] { 4, 4, 4, 4, 4, 4 }
	};

	public static int[][] mMon234 = new int[5][]
	{
		new int[8] { 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[6] { 2, 2, 3, 3, 4, 4 },
		new int[8] { 5, 5, 5, 5, 4, 4, 4, 4 },
		new int[10] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 },
		new int[6] { 6, 6, 6, 6, 6, 6 }
	};

	public static int[][] mMon12 = new int[5][]
	{
		new int[10] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[6] { 1, 1, 1, 2, 2, 2 },
		new int[8] { 3, 3, 3, 3, 2, 2, 2, 2 },
		new int[10] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
		new int[6] { 4, 4, 4, 4, 4, 4 }
	};

	public static int[][] mMon2345 = new int[5][]
	{
		new int[8] { 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[8] { 2, 2, 3, 3, 4, 4, 5, 5 },
		new int[8] { 6, 6, 6, 6, 5, 5, 5, 5 },
		new int[10] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
		new int[10] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 }
	};

	public static int[][] mMon2345Long = new int[5][]
	{
		new int[12]
		{
			0, 0, 0, 0, 0, 0, 0, 1, 1, 1,
			1, 1
		},
		new int[14]
		{
			2, 2, 2, 2, 3, 3, 3, 4, 4, 4,
			4, 5, 5, 5
		},
		new int[8] { 6, 6, 6, 6, 5, 5, 5, 5 },
		new int[10] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 },
		new int[10] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 }
	};

	public static int[][] mMon1234 = new int[5][]
	{
		new int[10] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[12]
		{
			2, 2, 2, 3, 3, 3, 4, 4, 4, 1,
			1, 1
		},
		new int[8] { 5, 5, 5, 5, 4, 4, 4, 4 },
		new int[10] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 },
		new int[10] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 }
	};

	public static int[][] mMon23 = new int[5][]
	{
		new int[10] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[8] { 3, 3, 3, 3, 2, 2, 2, 2 },
		new int[8] { 3, 3, 3, 3, 4, 4, 4, 4 },
		new int[10] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
		new int[6] { 5, 5, 5, 5, 5, 5 }
	};

	public static int[][] mMon123 = new int[5][]
	{
		new int[6] { 0, 0, 0, 0, 1, 1 },
		new int[6] { 3, 3, 2, 2, 1, 1 },
		new int[8] { 3, 3, 3, 3, 4, 4, 4, 4 },
		new int[10] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
		new int[6] { 5, 5, 5, 5, 5, 5 }
	};

	public static int[][] mMon0Move = new int[5][]
	{
		new int[12]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1
		},
		new int[6] { 0, 0, 0, 0, 1, 1 },
		new int[7] { 2, 2, 2, 3, 3, 3, 3 },
		new int[10] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
		new int[6] { 4, 4, 4, 4, 4, 4 }
	};

	public static int[][] mMonBuNhin = new int[5][]
	{
		new int[6],
		new int[6],
		new int[6],
		new int[6] { 1, 1, 1, 1, 1, 1 },
		new int[6] { 1, 1, 1, 1, 1, 1 }
	};

	public static int[][] mMonMove01 = new int[5][]
	{
		new int[6],
		new int[6] { 0, 0, 0, 0, 1, 1 },
		new int[6] { 1, 1, 1, 2, 2, 2 },
		new int[10] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
		new int[10] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }
	};

	public static int[][] mMon2343 = new int[5][]
	{
		new int[8] { 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[8] { 2, 2, 3, 3, 4, 4, 3, 3 },
		new int[8] { 5, 5, 5, 5, 4, 4, 4, 4 },
		new int[10] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 },
		new int[6] { 6, 6, 6, 6, 6, 6 }
	};

	public static int[][] mMonBingo = new int[5][]
	{
		new int[16]
		{
			0, 0, 0, 0, 1, 1, 1, 1, 2, 2,
			3, 3, 2, 2, 1, 1
		},
		new int[16]
		{
			0, 0, 0, 0, 1, 1, 1, 1, 2, 2,
			3, 3, 2, 2, 1, 1
		},
		new int[16]
		{
			0, 0, 0, 0, 1, 1, 1, 1, 2, 2,
			3, 3, 2, 2, 1, 1
		},
		new int[16]
		{
			0, 0, 0, 0, 1, 1, 1, 1, 2, 2,
			3, 3, 2, 2, 1, 1
		},
		new int[16]
		{
			0, 0, 0, 0, 1, 1, 1, 1, 2, 2,
			3, 3, 2, 2, 1, 1
		}
	};

	public static int[][] mMonKungfu = new int[5][]
	{
		new int[8] { 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[12]
		{
			1, 1, 1, 2, 2, 2, 3, 3, 3, 2,
			2, 2
		},
		new int[10] { 4, 4, 5, 5, 5, 6, 6, 7, 7, 7 },
		new int[9] { 8, 8, 8, 8, 8, 8, 8, 8, 8 },
		new int[9] { 8, 8, 8, 8, 8, 8, 8, 8, 8 }
	};

	public static int[][] mMon1232 = new int[5][]
	{
		new int[8],
		new int[12]
		{
			1, 1, 1, 2, 2, 2, 3, 3, 3, 2,
			2, 2
		},
		new int[8] { 3, 3, 3, 4, 4, 4, 4, 4 },
		new int[8] { 5, 5, 5, 5, 5, 5, 5, 5 },
		new int[8] { 5, 5, 5, 5, 5, 5, 5, 5 }
	};

	public static int[][] mMonIceSnow = new int[5][]
	{
		new int[8] { 0, 0, 0, 0, 2, 2, 2, 2 },
		new int[10] { 1, 1, 1, 2, 2, 3, 3, 3, 2, 2 },
		new int[10] { 1, 1, 1, 2, 2, 3, 3, 3, 2, 2 },
		new int[10] { 1, 1, 1, 2, 2, 3, 3, 3, 2, 2 },
		new int[10] { 1, 1, 1, 2, 2, 3, 3, 3, 2, 2 }
	};

	public static int[][] mMonPokemon = new int[5][]
	{
		new int[8] { 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[8] { 1, 1, 2, 2, 1, 1, 3, 3 },
		new int[8] { 1, 1, 2, 2, 1, 1, 3, 3 },
		new int[8] { 1, 1, 2, 2, 1, 1, 3, 3 },
		new int[8] { 1, 1, 2, 2, 1, 1, 3, 3 }
	};

	public static int[][] mMonTru = new int[5][]
	{
		new int[4],
		new int[4],
		new int[4],
		new int[4],
		new int[4] { 1, 1, 1, 1 }
	};

	public static int[][] mMonBanhKem = new int[5][]
	{
		new int[4],
		new int[4],
		new int[4],
		new int[4],
		new int[4]
	};

	public static int[][] mMonLoSuoi = new int[5][]
	{
		new int[12]
		{
			1, 1, 1, 1, 2, 2, 3, 3, 2, 2,
			1, 1
		},
		new int[12]
		{
			1, 1, 1, 1, 2, 2, 3, 3, 2, 2,
			1, 1
		},
		new int[12]
		{
			1, 1, 1, 1, 2, 2, 3, 3, 2, 2,
			1, 1
		},
		new int[12]
		{
			1, 1, 1, 1, 2, 2, 3, 3, 2, 2,
			1, 1
		},
		new int[12]
		{
			1, 1, 1, 1, 2, 2, 3, 3, 2, 2,
			1, 1
		}
	};

	private int tickQuaydau;

	private bool isFly;

	private sbyte isAddEff;

	private int lastTick;

	private int framepaint;

	public MonsterWalk(short ID, int x, int y, CatalogyMonster cata)
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
		downSpeedWater = 1;
		Dir = 0;
		wOne = (hOne = -1);
		hIconFocus = 5;
		xsai = 0;
		ysai = 0;
		if (LoadMap.specMap == 4)
		{
			setSpeed(3, 3);
			ysai = -12;
		}
		else
		{
			setSpeed(4, 4);
		}
		limitMove = 60;
		timeAutoAction = CRes.random(100, 150);
		timeRanChangeAction = 80;
		timeMaxMoveAttack = 2000;
		limitAttack = 30;
		if (cata.isTemplate)
		{
			loadInfoAgain(cata);
		}
		setWName();
	}

	public override void paint(mGraphics g)
	{
		if (!isLoadTemplate)
		{
			g.drawImage(AvMain.imgHinhnhan, x, y, 33);
			return;
		}
		int num = Action;
		if (num > mActionMonSter.Length - 1 && Action != 4)
		{
			num = 0;
		}
		if (f > mActionMonSter[num].Length - 1)
		{
			f = 0;
		}
		if (typeMoveMonster == 19)
		{
			if (Action == 4)
			{
				if (typeShadow >= 0)
				{
					paintShadowMonster(g, x, dyShadow, typeShadow);
				}
				MainImage imageAll = ObjectData.getImageAll(IdIcon, ObjectData.HashImageMonster, 1000);
				if (imageAll != null && imageAll.img != null)
				{
					paintFrame(g, imageAll, num);
				}
			}
			dy = 0;
		}
		if (isDie)
		{
			return;
		}
		paintEffBoss(g, -4);
		if (LoadMap.specMap == 4 && !GameCanvas.lowGraphic)
		{
			for (int i = 0; i < vecEffMapSea.size(); i++)
			{
				Point point = (Point)vecEffMapSea.elementAt(i);
				Boat.fraEffSea3.drawFrame(point.f / 2, point.x, point.y, point.dis, 3, g);
			}
		}
		MainImage imageAll2 = ObjectData.getImageAll(IdIcon, ObjectData.HashImageMonster, 1000);
		if (isFly)
		{
			dy = 30;
		}
		if (typeShadow >= 0)
		{
			paintShadowMonster(g, x, dyShadow, typeShadow);
		}
		if (imageAll2.img != null)
		{
			paintFrame(g, imageAll2, num);
			if (typeMoveMonster == 19 && IdIcon == 58 && GameScreen.ClanDao != null)
			{
				MainImage iconClan = Potion.getIconClan(GameScreen.ClanDao.idIcon);
				if (iconClan != null && iconClan.img != null)
				{
					if (iconClan.frame == -1)
					{
						iconClan.set_Frame();
					}
					if (iconClan.frame <= 1)
					{
						g.drawImage(iconClan.img, x, y - 30, 3);
					}
					else
					{
						int num2 = ((framepaint < imageAll2.frame - 1) ? 3 : 15);
						if (CRes.abs(GameCanvas.gameTick - lastTick) > num2)
						{
							framepaint++;
							if (framepaint >= imageAll2.frame)
							{
								framepaint = 0;
							}
							lastTick = GameCanvas.gameTick;
						}
						g.drawRegion(iconClan.img, 0, framepaint * iconClan.w, iconClan.w, iconClan.w, 0, x, y - 30, 3);
					}
				}
			}
		}
		paintIconPk_ThanhTich(g);
	}

	public void paintFrame(mGraphics g, MainImage img, int actest)
	{
		if (wOne < 0)
		{
			hOne = mImage.getImageHeight(img.img.image) / nFrame;
			wOne = mImage.getImageWidth(img.img.image);
		}
		int num = mActionMonSter[actest][f];
		int arg = ((Dir == 2) ? 2 : 0);
		if (isTru())
		{
			arg = 0;
		}
		if (Action != 4 || frameDie < timeBienmat - 6 || frameDie % 2 == 0 || isTru())
		{
			g.drawRegion(img.img, 0, num * hOne, wOne, hOne, arg, x, y - dy - vyStyleDie, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
	}

	public override void update()
	{
		base.update();
		updateDy();
		updatevXEffAva();
		setRevice();
		updateDieMonsterNormal();
		updateActionMonSter(isPet: false);
		if (Action != 4)
		{
			if (isAddEff > 0)
			{
				addEff();
			}
			if (Action != 3 && Action != 2)
			{
				if (timeBeFire > 0)
				{
					timeBeFire--;
					vx = 0;
					vy = 0;
				}
				else if (typeMoveMonster == 9 || typeMoveMonster == 8 || typeMoveMonster == 14 || typeMoveMonster == 20)
				{
					if (skillCurrent != null)
					{
						setDataBeginSkill(skillCurrent.skill, skillCurrent.vecObjBeFire);
					}
					if (!MainObject.isInScreen(this) && !setIsInScreen(xAnchor, yAnchor, wOne, hOne))
					{
						x = xAnchor;
						y = yAnchor;
					}
				}
				else if (isRunAttack)
				{
					if (timeFreeFire > 0)
					{
						autoMoveFire();
					}
					else
					{
						Move_to_Focus();
					}
				}
				else
				{
					auto_Move();
				}
			}
		}
		if (typeMoveMonster == 9 || typeMoveMonster == 8 || typeMoveMonster == 14 || typeMoveMonster == 20)
		{
			if (MainObject.getDistance(x, y, xAnchor, yAnchor) > 50)
			{
				x = xAnchor;
				y = yAnchor;
			}
		}
		else if (typeMoveMonster == 19 && MainObject.getDistance(x, y, xAnchor, yAnchor) != 0)
		{
			x = xAnchor;
			y = yAnchor;
		}
		int tile = GameCanvas.loadmap.getTile(x + vx, y + vy);
		setMove(downSpeedWater, tile);
		updateEffMapSea();
		updateDirFocusPlayer();
	}

	private void addEff()
	{
		if (isAddEff == 1)
		{
			if ((GameCanvas.gameTick + timeAutoAction) % 30 == 0)
			{
				GameScreen.addEffectEnd(116, 0, x, y - hOne / 2, (sbyte)Dir, this);
			}
			if ((GameCanvas.gameTick + timeAutoAction) % 30 == 15)
			{
				GameScreen.addEffectEnd(116, 1, x, y - hOne / 2, (sbyte)Dir, this);
			}
		}
		else if (isAddEff == 2 && (Action == 1 || CRes.random(4) == 0) && CRes.random(2) == 0)
		{
			GameScreen.addEffectEnd(117, 0, x + CRes.random_Am_0(10), y, (sbyte)Dir, this);
		}
	}

	public override void loadInfoAgain(CatalogyMonster cata)
	{
		typeMoveMonster = cata.typeMove;
		IdIcon = cata.idIcon;
		name = cata.name;
		maxHp = cata.maxHp;
		isHumanMonster = cata.isHumanPart;
		Lv = cata.lv;
		typeBossMonster = cata.typeMonster;
		if (cata.idCat == 75)
		{
			typeShadow = 2;
			dyShadow = -12;
			ysai = -4;
		}
		else if ((idCatMonster >= 115 && idCatMonster <= 119) || (idCatMonster >= 128 && idCatMonster <= 131) || (idCatMonster >= 168 && idCatMonster <= 170) || idCatMonster == 172)
		{
			isPokemon = 1;
		}
		else if (idCatMonster == 121)
		{
			isPokemon = 2;
		}
		else if (idCatMonster == 114)
		{
			dyShadow = -2;
			typeShadow = 0;
		}
		else if (idCatMonster == 174)
		{
			dyShadow = -6;
			typeShadow = 2;
		}
		switch (cata.typeMove)
		{
		case 0:
			nFrame = 5;
			mActionMonSter = mMon12;
			break;
		case 1:
			nFrame = 7;
			mActionMonSter = mMon234;
			break;
		case 2:
			nFrame = 8;
			mActionMonSter = mMon2345;
			break;
		case 3:
			nFrame = 5;
			mActionMonSter = mMon012;
			break;
		case 4:
			nFrame = 7;
			mActionMonSter = mMon1234;
			if (IdIcon == 35)
			{
				typeShadow = 2;
				dyShadow = -5;
				isAddEff = 1;
			}
			break;
		case 5:
			nFrame = 6;
			mActionMonSter = mMon23;
			break;
		case 6:
			nFrame = 6;
			mActionMonSter = mMon123;
			break;
		case 9:
			nFrame = 5;
			mActionMonSter = mMon0Move;
			break;
		case 8:
			nFrame = 2;
			mActionMonSter = mMonBuNhin;
			break;
		case 10:
			nFrame = 4;
			mActionMonSter = mMonMove01;
			break;
		case 11:
			nFrame = 7;
			mActionMonSter = mMon2343;
			break;
		case 12:
			nFrame = 6;
			mActionMonSter = mMon23;
			isFly = true;
			typeShadow = 0;
			break;
		case 14:
			nFrame = 4;
			mActionMonSter = mMonBingo;
			typeShadow = 2;
			dyShadow = -12;
			ysai = -4;
			if (IdIcon == 34)
			{
				ysai = 0;
				typeShadow = 2;
				dyShadow = -5;
				isAddEff = 2;
			}
			else if (IdIcon == 59)
			{
				typeShadow = 1;
				dyShadow = -1;
			}
			break;
		case 15:
			nFrame = 9;
			mActionMonSter = mMonKungfu;
			dyShadow = -1;
			typeShadow = 1;
			break;
		case 16:
			nFrame = 6;
			mActionMonSter = mMon1232;
			typeShadow = 3;
			ysai = -5;
			break;
		case 17:
			nFrame = 4;
			mActionMonSter = mMonIceSnow;
			typeShadow = 0;
			dyShadow = -1;
			if (idCatMonster == 98)
			{
				isAddEff = 2;
			}
			break;
		case 18:
			nFrame = 4;
			mActionMonSter = mMonPokemon;
			typeShadow = 0;
			dyShadow = -1;
			if (idCatMonster == 121)
			{
				dyShadow = -6;
				typeShadow = 2;
			}
			break;
		case 19:
			nFrame = 2;
			mActionMonSter = mMonTru;
			typeShadow = 0;
			dyShadow = -1;
			if (IdIcon == 58)
			{
				typeShadow = 2;
				dyShadow = -3;
			}
			break;
		case 20:
			nFrame = 1;
			mActionMonSter = mMonBanhKem;
			typeShadow = 2;
			dyShadow = -12;
			ysai = -4;
			break;
		case 21:
			setSpeed(6, 6);
			mActionMonSter = Pet.mDog;
			mActionStandMonSter = Pet.mDog[0];
			typeShadow = 1;
			if (IdIcon == 55 || IdIcon == 56)
			{
				nFrame = 7;
				mActionStandMonSter = Pet.mPlayStandDogVip;
				if (IdIcon == 56)
				{
					typeShadow = 0;
				}
			}
			else
			{
				nFrame = 5;
				dyShadow = -2;
			}
			break;
		}
		isLoadTemplate = true;
		mSystem.outz("monster " + name + " nFrame " + nFrame + " type " + typeMoveMonster);
	}

	public override bool isTru()
	{
		if (typeMoveMonster == 19)
		{
			return true;
		}
		return false;
	}
}
