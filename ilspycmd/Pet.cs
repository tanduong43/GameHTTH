public class Pet : MainMonster
{
	public static int[][] mChoper = new int[5][]
	{
		new int[14]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
			1, 1, 1, 1
		},
		new int[12]
		{
			3, 3, 3, 4, 4, 4, 5, 5, 5, 6,
			6, 6
		},
		new int[8] { 7, 7, 7, 7, 7, 8, 8, 8 },
		new int[10] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
		new int[10] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
	};

	public static int[][] mLasso = new int[5][]
	{
		new int[14]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
			1, 1, 1, 1
		},
		new int[12]
		{
			1, 1, 1, 2, 2, 2, 1, 1, 1, 3,
			3, 3
		},
		new int[8] { 1, 1, 4, 4, 4, 4, 4, 4 },
		new int[8] { 1, 1, 1, 1, 1, 1, 1, 1 },
		new int[8] { 1, 1, 1, 1, 1, 1, 1, 1 }
	};

	public static int[][] mGhost = new int[5][]
	{
		new int[24]
		{
			0, 0, 0, 0, 0, 0, 1, 1, 1, 1,
			1, 1, 2, 2, 2, 2, 2, 2, 1, 1,
			1, 1, 1, 1
		},
		new int[18]
		{
			3, 3, 3, 3, 3, 3, 4, 4, 4, 4,
			4, 4, 5, 5, 5, 5, 5, 5
		},
		new int[12]
		{
			0, 0, 0, 1, 1, 1, 2, 2, 2, 1,
			1, 1
		},
		new int[12]
		{
			0, 0, 0, 1, 1, 1, 2, 2, 2, 1,
			1, 1
		},
		new int[12]
		{
			0, 0, 0, 1, 1, 1, 2, 2, 2, 1,
			1, 1
		}
	};

	public static int[][] mBat = new int[5][]
	{
		new int[18]
		{
			0, 0, 0, 0, 0, 0, 1, 1, 1, 1,
			1, 1, 2, 2, 2, 2, 2, 2
		},
		new int[9] { 0, 0, 0, 1, 1, 1, 2, 2, 2 },
		new int[9] { 0, 0, 0, 1, 1, 1, 2, 2, 2 },
		new int[9] { 0, 0, 0, 1, 1, 1, 2, 2, 2 },
		new int[9] { 0, 0, 0, 1, 1, 1, 2, 2, 2 }
	};

	public static int[][] mDog = new int[5][]
	{
		new int[10] { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
		new int[6] { 2, 2, 3, 3, 4, 4 },
		new int[6] { 2, 2, 3, 3, 4, 4 },
		new int[6] { 2, 2, 3, 3, 4, 4 },
		new int[6] { 2, 2, 3, 3, 4, 4 }
	};

	public static int[] mPlayStandChopper = new int[14]
	{
		0, 0, 0, 0, 0, 0, 0, 0, 0, 2,
		2, 2, 2, 2
	};

	public static int[] mPlayStandDogVip = new int[36]
	{
		5, 5, 5, 5, 5, 5, 6, 6, 6, 6,
		6, 6, 5, 5, 5, 5, 5, 5, 6, 6,
		6, 6, 6, 6, 5, 5, 5, 5, 5, 5,
		6, 6, 6, 6, 6, 6
	};

	public const sbyte PET_CHOPPER = 0;

	public const sbyte PET_TOTO = 1;

	public const sbyte PET_LASSO = 2;

	public const sbyte PET_GHOST = 3;

	public const sbyte PET_BAT = 4;

	public const sbyte PET_GHOST_BROOK = 5;

	private MainImage img;

	public Pet(short ID, short idMain, short idImage, sbyte type)
		: base(ID, idMain, idImage, type)
	{
		IDMainShiper = idMain;
		setDataPet(ID, idImage, type);
	}

	public override void setDataPet(short ID, short idImage, sbyte type)
	{
		base.ID = ID;
		typePet = type;
		IdIcon = idImage;
		wOne = (hOne = -1);
		colorName = 5;
		objMainFocus = MainObject.get_Object(IDMainShiper, 0);
		if (objMainFocus != null)
		{
			setInfoObjMain();
		}
		f = 0;
		typeObject = 10;
		Action = 0;
		setSpeed(5, 5);
		typeShadow = 1;
		dyMovePet = 0;
		switch (typePet)
		{
		case 0:
			mActionMonSter = mChoper;
			mActionStandMonSter = mPlayStandChopper;
			nFrame = 9;
			break;
		case 1:
			mActionMonSter = MonsterWalk.mMonKungfu;
			mActionStandMonSter = MonsterWalk.mMonKungfu[0];
			nFrame = 9;
			break;
		case 2:
			mActionMonSter = mLasso;
			mActionStandMonSter = mLasso[0];
			nFrame = 5;
			break;
		case 3:
		case 5:
			mActionMonSter = mGhost;
			mActionStandMonSter = mGhost[0];
			nFrame = 6;
			setSpeed(3, 3);
			dyMain = 10;
			typeShadow = -1;
			dyMovePet = 10;
			break;
		case 4:
			mActionMonSter = mBat;
			mActionStandMonSter = mBat[0];
			nFrame = 3;
			dyMain = 20;
			dyMovePet = 15;
			break;
		case 21:
			setSpeed(6, 6);
			mActionMonSter = mDog;
			mActionStandMonSter = mDog[0];
			if (IdIcon == 55 || IdIcon == 56)
			{
				nFrame = 7;
				mActionStandMonSter = mPlayStandDogVip;
				if (IdIcon == 56)
				{
					typeShadow = 0;
				}
			}
			else
			{
				nFrame = 5;
			}
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		MainImage mainImage = null;
		mainImage = ObjectData.getImageAll(IdIcon, ObjectData.HashImageMonster, 1000);
		if (LoadMap.specMap != 4 && typeShadow >= 0)
		{
			paintShadowMonster(g, x, -3, typeShadow);
		}
		if (objMainFocus == null || (LoadMap.specMap == 4 && objMainFocus.Action == 4))
		{
			return;
		}
		int num = Action;
		if (num > mActionMonSter.Length - 1)
		{
			num = 0;
		}
		if (isPlayStand && Action == 0)
		{
			if (f > mActionStandMonSter.Length - 1)
			{
				f = 0;
			}
		}
		else if (f > mActionMonSter[num].Length - 1)
		{
			f = 0;
		}
		if (mainImage.img != null)
		{
			if (wOne < 0)
			{
				hOne = mImage.getImageHeight(mainImage.img.image) / nFrame;
				wOne = mImage.getImageWidth(mainImage.img.image);
			}
			int num2 = 0;
			num2 = ((!isPlayStand || Action != 0) ? mActionMonSter[num][f] : mActionStandMonSter[f]);
			if (Action != 4)
			{
				g.drawRegion(mainImage.img, 0, num2 * hOne, wOne, hOne, (Dir == 2) ? 2 : 0, x, y - dyMain - objMainFocus.dySea / 10, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		if (LoadMap.specMap != 4)
		{
			AvMain.FontBorderSmall(g, objMainFocus.name, x, y - dyMain - 1 - hOne - 10, 2, colorName);
		}
	}

	public override void update()
	{
		if (objMainFocus == null)
		{
			if (GameCanvas.gameTick % 100 == 0)
			{
				objMainFocus = MainObject.get_Object(IDMainShiper, 0);
				if (objMainFocus != null)
				{
					setInfoObjMain();
				}
			}
			return;
		}
		if (f == 0 && Action == 0)
		{
			if (CRes.random(6) == 0)
			{
				isPlayStand = true;
			}
			else
			{
				isPlayStand = false;
			}
		}
		x += vx;
		y += vy;
		updateActionMonSter(isPet: true);
		move_to_XY_Normal();
		if (skillCurrent == null)
		{
			setNextSkill();
		}
		if (Action == 0)
		{
			setBeginMove();
		}
		else if (Action == 1)
		{
			int distance = MainObject.getDistance(x, y, toX, toY);
			if (distance <= 24)
			{
				setBeginMove();
			}
		}
		if (Action != 0 && isPlayStand)
		{
			isPlayStand = false;
		}
	}

	public void setBeginMove()
	{
		if (objMainFocus.typeActionBoat != 0)
		{
			return;
		}
		if (LoadMap.specMap == 4)
		{
			if (objMainFocus.boatSea != null)
			{
				if (objMainFocus.boatSea.Dir == 0)
				{
					x = objMainFocus.boatSea.x + 30;
				}
				else
				{
					x = objMainFocus.boatSea.x - 30;
				}
				y = objMainFocus.boatSea.y + 1;
				dy = 5;
				toX = x;
				toY = y;
				Dir = objMainFocus.boatSea.Dir;
			}
			if (Action == 1)
			{
				Action = 0;
			}
			return;
		}
		bool flag = false;
		int distance = MainObject.getDistance(x, y, objMainFocus.x, objMainFocus.y);
		int num = CRes.random(100);
		if (distance > 250)
		{
			setSpeed(14, 14);
			flag = true;
		}
		if (!flag && distance > 150)
		{
			setSpeed(8, 8);
			flag = true;
		}
		if (!flag && distance > 100)
		{
			setSpeed(vMax, vMax);
			flag = true;
		}
		if (!flag && num < 2)
		{
			setSpeed(vMax, vMax);
			flag = true;
		}
		if (!flag && distance > 48 && num < 25)
		{
			setSpeed(vMax, vMax);
			flag = true;
		}
		int num2 = 0;
		if (!flag)
		{
			return;
		}
		int xset;
		int yset;
		int tile;
		do
		{
			num2++;
			xset = objMainFocus.x + CRes.random_Am(12, 48);
			yset = objMainFocus.y + CRes.random_Am(12, 24);
			if (Action == 1)
			{
				yset = ((num2 >= 5) ? (objMainFocus.y + 12) : (objMainFocus.y - 12));
			}
			tile = GameCanvas.loadmap.getTile(xset, yset);
		}
		while (tile != 0 && tile != 2 && num2 < 10);
		toX = xset;
		toY = yset;
	}

	public void setInfoObjMain()
	{
		x = objMainFocus.x;
		y = objMainFocus.y - dyMovePet;
		toX = objMainFocus.x;
		toY = objMainFocus.y - dyMovePet;
		objMainFocus.isPet = true;
		if (objMainFocus == GameScreen.player)
		{
			colorName = 0;
		}
	}

	public override void setPetActionFire()
	{
		if (Action != 2 || Action != 4)
		{
			Action = 2;
			f = 0;
		}
	}
}
