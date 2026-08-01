public class Boat : MainItemMap
{
	public short idIconClan = -1;

	public bool isSetLevelPaint = true;

	public bool isPaint = true;

	public bool loadDataOk;

	public int wlech = 12;

	public int numNextFrame = 3;

	public static int hlech = 12;

	public sbyte levelPaintBoat;

	public sbyte pirate = 2;

	public static mImage imgShip;

	public static mImage imgEffSea;

	public static mImage imgEffSea2;

	public static mImage imgEffSea3;

	public static int[][] sizeImgBoat;

	public static FrameImage fraEffSea;

	public static FrameImage fraEffSea2;

	public static FrameImage fraEffSea3;

	public static FrameImage fraPirateUnity;

	public static FrameImage fraEffSea4;

	public mVector vecEff = new mVector("Boat.vecEff");

	public const sbyte EFF_SONG_DAU_THUYEN_MOVE = -1;

	public const sbyte EFF_SONG_MANG_THUYEN_MOVE = 0;

	public const sbyte EFF_SONG_MANG_THUYEN_STAND = 1;

	public const sbyte EFF_VET_NUOC_MANG_THUYEN_STAND = 2;

	public int xset;

	public int yset;

	public int wset;

	public int hset;

	public short[] mPartImage = new short[4] { 0, 1, 2, 3 };

	public int tickDonotMove = 40;

	private int lastTick;

	private int framepaint;

	public Boat(short ID, int x, int y, int dy, sbyte Dir)
	{
		wOne = 100;
		hOne = 70;
		ysai = -50;
		vySea = 3;
		base.ID = ID;
		base.y = y;
		ySort = y + ysai;
		base.dy = dy;
		base.Dir = (sbyte)((Dir == 2) ? 2 : 0);
		base.x = x + ((Dir != 2) ? (-wlech) : wlech);
		if (y < GameCanvas.loadmap.maxHMap - 100)
		{
			levelPaintBoat = -1;
		}
		TypeItem = 1;
		setIconClan();
	}

	public void setIconClan()
	{
		MainObject mainObject = MainObject.get_Object(ID, 0);
		if (mainObject != null && mainObject.clan != null && GameCanvas.currentScreen != GameCanvas.listCharScr)
		{
			idIconClan = mainObject.clan.idIcon;
		}
	}

	public void setPartImage(short[] mp, sbyte pirate)
	{
		mPartImage = mp;
		this.pirate = pirate;
	}

	public override void paint(mGraphics g)
	{
		if (isPaint)
		{
			paintFrist(g);
			paintBuom(g);
			paintLastInMap(g);
		}
	}

	public override void update()
	{
		updateBoat();
		updateDySea();
		if (tickDonotMove <= 0)
		{
			resetY();
		}
		ySort = y + ysai;
	}

	public void updateXY(int x, int y, int dy, sbyte Dir)
	{
		base.y = y;
		base.dy = dy;
		base.Dir = (sbyte)((Dir == 2) ? 2 : 0);
		base.x = x + ((Dir != 2) ? (-wlech) : wlech);
		boatSetVaCham(0, 0);
		tickDonotMove = 40;
	}

	public void paintFrist(mGraphics g)
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			Point point = (Point)vecEff.elementAt(i);
			if (point.levelPaint != -1)
			{
				continue;
			}
			if (point.frame == 2)
			{
				fraEffSea2.drawFrame(point.f / point.fSmall % fraEffSea2.nFrame, point.x, point.y, point.dis, 3, g);
			}
			else if (point.frame == -1)
			{
				FrameImage frameImage = fraEffSea;
				if (MapGotoSky.isBeginEffBoat)
				{
					frameImage = fraEffSea4;
				}
				frameImage.drawFrame(point.f / point.fSmall % frameImage.nFrame, x + ((Dir != 2) ? (-37) : 37), y + dy / 2 + hlech - 3, Dir, 3, g);
			}
			else
			{
				FrameImage frameImage2 = fraEffSea;
				if (MapGotoSky.isBeginEffBoat)
				{
					frameImage2 = fraEffSea4;
				}
				frameImage2.drawFrame(point.f / point.fSmall % frameImage2.nFrame, point.x, point.y, point.dis, 3, g);
			}
		}
		if (mPartImage != null)
		{
			ItemBoat.paintPartBoat(g, mPartImage[0], 0, x, y, dy, Dir);
		}
	}

	public void paintBuom(mGraphics g)
	{
		paintBuom(g, idIconClan);
	}

	public void paintBuom(mGraphics g, short iconClan)
	{
		if (mPartImage == null)
		{
			return;
		}
		ItemBoat.paintPartBoat(g, mPartImage[1], 1, x, y, dy, Dir);
		ItemBoat.paintPartBoat(g, mPartImage[2], 2, x, y, dy, Dir);
		if (iconClan >= 0)
		{
			MainImage iconClanBig = Potion.getIconClanBig(iconClan);
			if (iconClanBig != null && iconClanBig.img != null)
			{
				int num = GameCanvas.gameTick / 6 % 2;
				int num2 = -(16 - num);
				if (Dir == 2)
				{
					num2 = 16 - num;
				}
				if (iconClanBig.frame == -1)
				{
					iconClanBig.set_Frame();
				}
				if (iconClanBig.frame <= 1)
				{
					g.drawImage(iconClanBig.img, x + num2, y - 30 - dy, 3);
				}
				else
				{
					int num3 = ((framepaint < iconClanBig.frame - 1) ? 3 : 15);
					if (CRes.abs(GameCanvas.gameTick - lastTick) > num3)
					{
						framepaint++;
						if (framepaint >= iconClanBig.frame)
						{
							framepaint = 0;
						}
						lastTick = GameCanvas.gameTick;
					}
					g.drawRegion(iconClanBig.img, 0, framepaint * iconClanBig.w, iconClanBig.w, iconClanBig.w, 0, x + num2, y - 30 - dy, 3);
				}
			}
		}
		int num4 = pirate;
		if (pirate == -1)
		{
			num4 = 3;
		}
		ItemBoat.paintPartBoat(g, 0, 100 + num4, x, y, dy, Dir);
	}

	public void paintHang(mGraphics g)
	{
		int num = ((Dir != 2) ? 0 : 0);
		g.drawRegion(imgShip, 0, 0, 59, 39, Dir, x + num, y + hlech - 6 - dy, 33);
	}

	public void paintLastInMap(mGraphics g)
	{
		sbyte day_night = GameCanvas.DAY;
		if (GameCanvas.mapBack != null && GameCanvas.mapBack.isNight)
		{
			day_night = GameCanvas.NIGHT;
		}
		paintLast(g, day_night);
	}

	public void paintLast(mGraphics g, sbyte day_night)
	{
		if (mPartImage != null)
		{
			ItemBoat.paintPartBoat(g, mPartImage[3], 3, x, y, dy, Dir);
		}
		if (!GameCanvas.lowGraphic)
		{
			paintEff(g, day_night);
		}
		if (day_night == GameCanvas.NIGHT)
		{
			return;
		}
		for (int i = 0; i < vecEff.size(); i++)
		{
			Point point = (Point)vecEff.elementAt(i);
			if (point.levelPaint <= -1)
			{
				continue;
			}
			if (point.frame == 2)
			{
				fraEffSea2.drawFrame(point.f / point.fSmall % fraEffSea2.nFrame, point.x, point.y, point.dis, 3, g);
			}
			else if (point.frame == -1)
			{
				FrameImage frameImage = fraEffSea;
				if (MapGotoSky.isBeginEffBoat)
				{
					frameImage = fraEffSea4;
				}
				frameImage.drawFrame(point.f / point.fSmall % frameImage.nFrame, x + ((Dir != 2) ? (-37) : 37), y + dy / 2 + hlech - 3, Dir, 3, g);
			}
			else
			{
				FrameImage frameImage2 = fraEffSea;
				if (MapGotoSky.isBeginEffBoat)
				{
					frameImage2 = fraEffSea4;
				}
				frameImage2.drawFrame(point.f / point.fSmall % frameImage2.nFrame, point.x, point.y, point.dis, 3, g);
			}
		}
	}

	public void paintEff(mGraphics g, sbyte day_night)
	{
		if (day_night == GameCanvas.DAY)
		{
			if (MapGotoSky.isBeginEffBoat)
			{
				if (GameScreen.player.typeMoveGotoSky != 2)
				{
					g.drawRegion(imgEffSea3, 0, 12 * (GameCanvas.gameTick / 12 % 2), 78, 12, Dir, x, y + hlech + 2, 3);
				}
			}
			else
			{
				g.drawRegion(imgEffSea, 0, 12 * (GameCanvas.gameTick / 12 % 2), 78, 12, Dir, x, y + hlech + 2, 3);
			}
		}
		else
		{
			g.drawRegion(imgEffSea2, 0, 12 * (GameCanvas.gameTick / 12 % 2), 78, 12, Dir, x, y + hlech + 2, 3);
		}
	}

	public void updateBoat()
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			Point point = (Point)vecEff.elementAt(i);
			point.x += point.vx;
			point.f++;
			if (point.f >= point.fRe)
			{
				vecEff.removeElement(point);
			}
		}
		if (tickDonotMove > 0)
		{
			tickDonotMove--;
		}
	}

	public override void updateDySea()
	{
		if (CRes.random(40) == 0)
		{
			if (CRes.random(2) == 0)
			{
				vySea = 3;
			}
			else
			{
				vySea = -3;
			}
		}
		if (dySea > 0 && vySea > 0)
		{
			vySea = -3;
		}
		else if (dySea < -30 && vySea < 0)
		{
			vySea = 3;
		}
		dySea += vySea;
		dy = dySea / 10;
	}

	public void addEffSea(int x, int y, int type, sbyte dis, int vx)
	{
		isSetLevelPaint = true;
		if (!GameCanvas.lowGraphic)
		{
			Point point = new Point();
			point.frame = type;
			switch (type)
			{
			case -1:
				point.x = base.x;
				point.y = base.y;
				point.vx = vx;
				point.fSmall = 3;
				break;
			case 0:
			{
				int num = CRes.random(20, 48);
				point.x = x + ((Dir != 2) ? (-num) : num);
				point.y = y + hlech - 3 + dy / 2;
				point.vx = vx;
				point.fSmall = 2;
				break;
			}
			case 1:
			{
				int num2 = CRes.random_Am_0(30);
				point.x = x + ((Dir != 2) ? (-wlech) : wlech) + num2;
				point.y = y + hlech - 3 + dy / 2;
				point.vx = vx;
				point.fSmall = 3;
				break;
			}
			case 2:
				point.x = x + ((Dir != 2) ? (-wlech) : wlech);
				point.y = y + hlech - 4 + dy / 2;
				point.vx = vx;
				point.fSmall = 3;
				point.levelPaint = -1;
				break;
			}
			point.dis = dis;
			point.fRe = 7 * point.fSmall;
			vecEff.addElement(point);
		}
	}

	public void setLevelPaint()
	{
		if (isSetLevelPaint)
		{
			for (int i = 0; i < vecEff.size(); i++)
			{
				Point point = (Point)vecEff.elementAt(i);
				point.levelPaint = -1;
			}
		}
		isSetLevelPaint = false;
	}

	public void boatSetVaCham(int vx, int vy)
	{
		xset = x - ((Dir != 2) ? (-wlech + 10) : (wlech - 10)) - 30 + vx;
		wset = 60;
		yset = y + vy;
		hset = 8;
	}

	public void boatSetTouch()
	{
		xset = x - ((Dir != 2) ? (-wlech + 15) : (wlech - 15)) - 14;
		wset = 90;
		yset = y - 6;
		hset = 12;
	}

	public void resetY()
	{
		y = ReadMessenge.yBoatMap;
	}

	public override bool isInScreen()
	{
		if (x + dx + wOne / 2 < MainScreen.cameraMain.xCam || x + dx - wOne / 2 > MainScreen.cameraMain.xCam + MotherCanvas.w || y + dy + 10 < MainScreen.cameraMain.yCam || y + dy + 10 - hOne > MainScreen.cameraMain.yCam + MotherCanvas.h)
		{
			return false;
		}
		return true;
	}
}
