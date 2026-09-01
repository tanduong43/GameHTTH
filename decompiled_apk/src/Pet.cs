using UnityEngine;

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
		Debug.Log((object)$"[PET CONSTRUCTOR] ID={ID}, idMain={idMain}, idImage={idImage}, type={type}, playerID={((GameScreen.player != null) ? GameScreen.player.ID : (-999))}");
		setDataPet(ID, idImage, type);
	}

	public override void setDataPet(short ID, short idImage, sbyte type)
	{
		base.ID = ID;
		typePet = type;
		IdIcon = idImage;
		wOne = (hOne = -1);
		colorName = 5;
		if (IDMainShiper == GameScreen.player.ID)
		{
			objMainFocus = GameScreen.player;
		}
		else
		{
			objMainFocus = MainObject.get_Object(IDMainShiper, 0);
		}
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
		if (IdIcon == 129 || IdIcon == 130)
		{
			adjustActionFrames(10);
			setSpeed(3, 3);
			dyMain = 10;
			typeShadow = -1;
			dyMovePet = 10;
		}
		else if (IdIcon == 131)
		{
			adjustActionFrames(9);
			setSpeed(3, 3);
			dyMain = 10;
			typeShadow = -1;
			dyMovePet = 10;
		}
		else if (IdIcon == 132 || IdIcon == 133 || IdIcon == 137 || IdIcon == 138)
		{
			adjustActionFrames(6);
			setSpeed(3, 3);
			dyMain = 10;
			typeShadow = -1;
			dyMovePet = 10;
		}
		else if (IdIcon == 134)
		{
			mActionMonSter = mBat;
			mActionStandMonSter = mBat[0];
			nFrame = 3;
			dyMain = 20;
			dyMovePet = 15;
		}
		else if ((IdIcon >= 90 && IdIcon <= 128) || IdIcon == 135 || IdIcon == 136 || IdIcon == 139)
		{
			adjustActionFrames(8);
			setSpeed(3, 3);
			dyMain = 10;
			typeShadow = -1;
			dyMovePet = 10;
		}
		else
		{
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
			case 29:
				setSpeed(6, 6);
				mActionMonSter = mDog;
				mActionStandMonSter = mDog[0];
				nFrame = 8;
				break;
			default:
				mActionMonSter = mChoper;
				mActionStandMonSter = mPlayStandChopper;
				nFrame = 9;
				break;
			}
		}
		Debug.Log((object)string.Format("[PET setDataPet OK] ID={0}, IdIcon={1}, typePet={2}, objMainFocus={3}, nFrame={4}, mActionMonSter={5}", ID, IdIcon, typePet, (objMainFocus != null) ? objMainFocus.name : "null", nFrame, (mActionMonSter != null) ? mActionMonSter.Length : 0));
	}

	public void adjustActionFrames(int frameCount)
	{
		if (frameCount > 0)
		{
			nFrame = frameCount;
			int[][] array = new int[5][];
			int[] array2 = new int[frameCount * 2];
			for (int i = 0; i < frameCount; i++)
			{
				array2[i * 2] = i;
				array2[i * 2 + 1] = i;
			}
			int[] array3 = new int[frameCount * 2];
			for (int j = 0; j < frameCount; j++)
			{
				array3[j * 2] = j;
				array3[j * 2 + 1] = j;
			}
			int[] array4 = new int[frameCount];
			for (int k = 0; k < frameCount; k++)
			{
				array4[k] = k;
			}
			array[0] = array2;
			array[1] = array3;
			array[2] = array4;
			array[3] = array4;
			array[4] = array4;
			mActionMonSter = array;
			mActionStandMonSter = array2;
		}
	}

	public override void paint(mGraphics g)
	{
		if (objMainFocus == null || objMainFocus.isRemove || (LoadMap.specMap == 4 && objMainFocus.Action == 4))
		{
			if (GameCanvas.gameTick % 120 == 0)
			{
				Debug.LogWarning((object)$"[PET PAINT SKIP] objMainFocus is null or removed for ID={ID}, IDMainShiper={IDMainShiper}, playerID={((GameScreen.player != null) ? GameScreen.player.ID : (-999))}");
			}
			return;
		}
		if (mActionMonSter == null || mActionMonSter.Length == 0 || nFrame <= 0)
		{
			if (GameCanvas.gameTick % 120 == 0)
			{
				Debug.LogWarning((object)$"[PET PAINT SKIP] mActionMonSter is null or nFrame <= 0 for IdIcon={IdIcon}, nFrame={nFrame}");
			}
			return;
		}
		MainImage mainImage = null;
		mainImage = ObjectData.getImageAll(IdIcon, ObjectData.HashImageMonster, 1000);
		if (LoadMap.specMap != 4 && typeShadow >= 0)
		{
			paintShadowMonster(g, x, -3, typeShadow);
		}
		int num = Action;
		if (num > mActionMonSter.Length - 1)
		{
			num = 0;
		}
		if (isPlayStand && Action == 0 && mActionStandMonSter != null)
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
		if (mainImage != null && mainImage.img != null && mainImage.img.image != null)
		{
			if (wOne < 0)
			{
				hOne = mImage.getImageHeight(mainImage.img.image) / nFrame;
				wOne = mImage.getImageWidth(mainImage.img.image);
			}
			int num2 = 0;
			num2 = ((!isPlayStand || Action != 0 || mActionStandMonSter == null) ? mActionMonSter[num][f] : mActionStandMonSter[f]);
			if (nFrame > 0 && num2 >= nFrame)
			{
				num2 %= nFrame;
			}
			if (Action != 4 && wOne > 0 && hOne > 0)
			{
				g.drawRegion(mainImage.img, 0, num2 * hOne, wOne, hOne, (Dir == 2) ? 2 : 0, x, y - dyMain - objMainFocus.dySea / 10, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		else if (GameCanvas.gameTick % 120 == 0)
		{
			Debug.LogWarning((object)string.Format("[PET PAINT WAITING IMG] IdIcon={0}, mainImage={1}, timeNull={2}", IdIcon, (mainImage == null) ? "mainImage_null" : ((mainImage.img != null) ? "img.image_null" : "mainImage.img_null"), mainImage?.timeImageNull ?? (-1)));
		}
		if (GameCanvas.gameTick % 120 == 0 && objMainFocus == GameScreen.player)
		{
			Debug.Log((object)string.Format("[PET DEBUG TICK] IdIcon={0}, typePet={1}, nFrame={2}, mainImage={3}, wOne={4}, hOne={5}, Action={6}, f={7}, pos=({8},{9})", IdIcon, typePet, nFrame, (mainImage == null) ? "null" : ((mainImage.img != null) ? $"OK({mainImage.w}x{mainImage.h})" : "img_null(loading)"), wOne, hOne, Action, f, x, y));
		}
		if (LoadMap.specMap != 4 && objMainFocus.name != null)
		{
			AvMain.FontBorderSmall(g, objMainFocus.name, x, y - dyMain - 1 - ((hOne > 0) ? hOne : 20) - 10, 2, colorName);
		}
	}

	public override void update()
	{
		if (objMainFocus == null || objMainFocus.isRemove)
		{
			if (IDMainShiper == GameScreen.player.ID)
			{
				objMainFocus = GameScreen.player;
			}
			else
			{
				objMainFocus = MainObject.get_Object(IDMainShiper, 0);
			}
			if (objMainFocus == null || objMainFocus.isRemove)
			{
				return;
			}
			setInfoObjMain();
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
		else if (Action == 1 && MainObject.getDistance(x, y, toX, toY) <= 24)
		{
			setBeginMove();
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
