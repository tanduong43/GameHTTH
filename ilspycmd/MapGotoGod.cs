public class MapGotoGod
{
	public static mVector vecAxe = new mVector("MapGotoGod.Axe");

	public static int wTile = 24;

	public static int idTile = 14;

	public static mImage imgAxe;

	public static int mapW;

	public static int mapH;

	public static int maxX;

	public static int maxY;

	public static MainImage imgTile;

	public static MainImage imgTileWater;

	public static int[] mapPaint;

	public static sbyte[] mLockMap;

	public static int yHardCode = MotherCanvas.h / 6 * 4;

	public static int xHardCode = MotherCanvas.w / 2;

	public static int vxHardCode = 6;

	public static int range = MotherCanvas.w / 6;

	public static int yPaintAudition = MotherCanvas.h - 20;

	private static int fStand;

	private static int fWater;

	private static int xCamOffline;

	private static sbyte right;

	private static sbyte createAxe;

	private static int tickAxe;

	private static sbyte finalMove;

	private static sbyte finalAxe;

	public static void setPos()
	{
		vxHardCode = 6;
		xCamOffline = 0;
		right = 0;
		createAxe = 0;
		tickAxe = 15;
		finalMove = 0;
		finalAxe = 0;
		MapOff_RedLine.timeRedline = 0;
		MapOff_RedLine.timebegin = 0L;
		vecAxe.removeAllElements();
		GameCanvas.loadMapScr.mItemMap = null;
		MainScreen.cameraMain.xCam = 0;
		MainScreen.cameraMain.yCam = 0;
		GameScreen.player.x = xHardCode;
		GameScreen.player.y = yHardCode;
		GameScreen.player.Dir = 2;
		GameScreen.player.type_left_right = 2;
		GameScreen.player.boatSea = new Boat(GameScreen.player.ID, GameScreen.player.x, GameScreen.player.y, 0, (sbyte)GameScreen.player.type_left_right);
		GameScreen.player.boatSea.setPartImage(GameScreen.player.myBoat, GameScreen.player.typePirate);
		GameScreen.player.setSpeed(2, 2);
		GameScreen.player.isMoveNor = true;
		ObjectData.getImageOther((short)idTile, 20);
		if (LoadMapScreen.isMapSky != 1)
		{
			LoadMapScreen.isMapSky = 1;
			LoadImageStatic.loadImageEffBoat();
		}
		maxX = MotherCanvas.w / wTile + 1;
		maxY = MotherCanvas.h / wTile + 1;
		mapW = 45;
		mapH = 18;
		imgTile = null;
		imgTileWater = null;
		mLockMap = new sbyte[30]
		{
			8, 11, 8, 8, 5, 5, 20, 20, 13, 13,
			8, 8, 4, 4, 4, 7, 4, 4, 7, 7,
			25, 42, 9, 12, 5, 5, 8, 8, 9, 12
		};
		fWater = mLockMap[idTile * 2];
		fStand = mLockMap[idTile * 2 + 1];
		mapPaint = new int[810]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			25, 23, 24, 25, 23, 24, 25, 23, 24, 25,
			23, 24, 25, 23, 24, 25, 23, 24, 25, 23,
			24, 25, 23, 24, 25, 23, 24, 25, 23, 24,
			25, 23, 24, 25, 23, 24, 25, 23, 24, 25,
			23, 24, 25, 24, 25, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 11, 9,
			10, 11, 9, 10, 11, 9, 10, 11, 9, 10,
			11, 9, 10, 11, 9, 10, 11, 9, 10, 11,
			9, 10, 11, 9, 10, 11, 9, 10, 9, 10,
			22, 20, 21, 22, 20, 21, 22, 20, 21, 22,
			20, 21, 22, 20, 21, 22, 20, 21, 22, 20,
			21, 22, 20, 21, 22, 20, 21, 22, 20, 21,
			22, 20, 21, 22, 20, 21, 22, 20, 21, 22,
			20, 21, 22, 21, 22, 19, 19, 19, 19, 19,
			19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
			19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
			19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
			19, 19, 19, 19, 19, 19, 19, 19, 19, 19
		};
	}

	public static void setTypeMoveredLine(sbyte type)
	{
		GameScreen.player.typeMoveGotoSky = type;
	}

	public static void SetPosNextMove()
	{
	}

	public static void update()
	{
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.update();
		}
		xCamOffline -= vxHardCode;
		updateActionPlayer();
		addAxe();
		updateAxe();
		if (GameScreen.player.typeMoveGotoSky == 1)
		{
			updateKeyAudition();
		}
		GameScreen.player.updateMapGotoGod(finalMove);
		if (Interface_Game.timePointer > 0)
		{
			Interface_Game.timePointer--;
		}
	}

	public static void updateActionPlayer()
	{
		if (GameScreen.player.typeMoveGotoSky == 0)
		{
			GameScreen.player.typeMoveGotoSky = 1;
		}
		else if (GameScreen.player.typeMoveGotoSky == 1)
		{
			if (CRes.abs(GameScreen.player.x - GameScreen.player.toX) < 4 && CRes.abs(GameScreen.player.y - GameScreen.player.toY) < 4 && CRes.random(25) == 0 && createAxe == 0)
			{
				if (right == 0)
				{
					right = 1;
					GameScreen.player.toX = xHardCode + range;
					createAxe = 1;
				}
				else
				{
					right = 0;
					GameScreen.player.toX = xHardCode - range;
					createAxe = 1;
				}
			}
		}
		else if (GameScreen.player.typeMoveGotoSky == 2)
		{
			if (finalAxe == 0)
			{
				GameScreen.player.toX = xHardCode;
				createAxe = 1;
				tickAxe = 15;
				finalAxe = 1;
			}
		}
		else if (GameScreen.player.typeMoveGotoSky != 3)
		{
		}
	}

	private static void addAxe()
	{
		if (createAxe == 0)
		{
			return;
		}
		if (tickAxe > 0)
		{
			tickAxe--;
			return;
		}
		createAxe = 0;
		tickAxe = 15;
		Point point = new Point();
		point.dis = 0;
		if (GameScreen.player.typeMoveGotoSky == 2)
		{
			point.x2 = -MotherCanvas.w / 2;
			point.subType = 1;
		}
		else if (right == 0)
		{
			point.x2 = -MotherCanvas.w / 2 + range * 2;
		}
		else
		{
			point.x2 = -MotherCanvas.w / 2 - range * 2;
		}
		vecAxe.addElement(point);
	}

	private static void updateAxe()
	{
		for (int i = 0; i < vecAxe.size(); i++)
		{
			Point point = (Point)vecAxe.elementAt(i);
			if (point.subType == 1)
			{
				if (point.y + point.y2 + imgAxe.h < yHardCode)
				{
					point.dis += 6;
				}
				else if (finalMove == 0)
				{
					finalMove = 1;
					vxHardCode = 0;
					GameScreen.player.vx = -4;
					GameScreen.player.vy = -4;
				}
			}
			else
			{
				point.dis += 6;
			}
			Point pointOnCircle = getPointOnCircle(MotherCanvas.w, MotherCanvas.h, point.dis, MotherCanvas.w / 2);
			point.x = pointOnCircle.x;
			point.y = pointOnCircle.y;
			if (point.dis >= 225)
			{
				vecAxe.removeElement(point);
				i--;
			}
		}
	}

	private static Point getPointOnCircle(int width, int height, int degress, int radius)
	{
		int num = width / 2;
		int num2 = height / 2;
		int a = degress - 90;
		int x = num + CRes.getcos(a) * radius / 1000;
		int y = num2 + CRes.getsin(a) * radius / 1000;
		return new Point(x, y);
	}

	private static void updateKeyAudition()
	{
		if (GameCanvas.keyMove(1))
		{
			GlobalService.gI().Red_Line(3, 1);
			GameCanvas.ClearkeyMove(1);
			Player.indexAudition++;
		}
		else if (GameCanvas.keyMove(2))
		{
			GlobalService.gI().Red_Line(3, 2);
			GameCanvas.ClearkeyMove(2);
			Player.indexAudition++;
		}
		else if (GameCanvas.keyMove(3))
		{
			GlobalService.gI().Red_Line(3, 3);
			GameCanvas.ClearkeyMove(3);
			Player.indexAudition++;
		}
		else if (GameCanvas.keyMove(0))
		{
			GlobalService.gI().Red_Line(3, 0);
			GameCanvas.ClearkeyMove(0);
			Player.indexAudition++;
		}
	}

	public static void paint(mGraphics g)
	{
		if (imgAxe == null)
		{
			imgAxe = mImage.createImage("/bg/axe.png");
			imgAxe.setWH();
		}
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.paint(g);
		}
		int num = MainScreen.cameraMain.xCam / wTile - 1;
		int num2 = MainScreen.cameraMain.yCam / wTile - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		int num3 = num + maxX + 2;
		int num4 = num2 + maxY + 2;
		if (num3 > mapW)
		{
			num3 = mapW;
		}
		if (num4 > mapH)
		{
			num4 = mapH;
		}
		if ((imgTileWater == null || imgTileWater.img == null) && !GameCanvas.lowGraphic)
		{
			imgTileWater = ObjectData.getImageOther((short)idTile, 70);
		}
		if (imgTile == null || imgTile.img == null)
		{
			imgTile = ObjectData.getImageOther((short)idTile, 20);
			g.setColor(15445332);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			return;
		}
		for (int i = num; i < num3; i++)
		{
			for (int j = num2; j < num4; j++)
			{
				int num5 = j * mapW + i;
				if (num5 >= mapPaint.Length)
				{
					continue;
				}
				int num6 = mapPaint[num5] - 1;
				int k;
				for (k = i * wTile + xCamOffline; k <= -wTile; k += num3 * wTile)
				{
				}
				if (!GameCanvas.lowGraphic && num6 >= fWater - 1 && num6 < fStand - 1 && GameCanvas.gameTick % 14 < 7)
				{
					int num7 = 0;
					if (GameCanvas.gameTick / 14 % 2 == 0)
					{
						num7 = fStand - fWater;
					}
					if (imgTileWater != null && imgTileWater.img != null)
					{
						g.drawRegion(imgTileWater.img, (num7 + num6 - (fWater - 1)) / 10 * wTile, (num7 + num6 - (fWater - 1)) % 10 * wTile, wTile, wTile, 0, k, j * wTile, 0);
					}
				}
				else if (num6 > -1 && imgTile != null && imgTile.img != null)
				{
					g.drawRegion(imgTile.img, num6 / 10 * wTile, num6 % 10 * wTile, wTile, wTile, 0, k, j * wTile, 0);
				}
			}
		}
		for (int l = 0; l < vecAxe.size(); l++)
		{
			Point point = (Point)vecAxe.elementAt(l);
			if (point.y < GameScreen.player.y)
			{
				paintAxe(g, point);
			}
		}
		GameScreen.player.paintGotoSky(g);
		for (int m = 0; m < vecAxe.size(); m++)
		{
			Point point2 = (Point)vecAxe.elementAt(m);
			if (point2.y >= GameScreen.player.y)
			{
				paintAxe(g, point2);
			}
		}
		GameCanvas.resetTrans(g);
		if (ReadMessenge.mImgCapredLine != null)
		{
			g.setColor(0);
			g.drawRect(MotherCanvas.hw - 40 - 1, yPaintAudition - 20 - 1, 81, 8);
			int num8 = (int)(MapOff_RedLine.timeRedline - (GameCanvas.timeNow - MapOff_RedLine.timebegin));
			if (num8 <= 0 && GameScreen.player.typeMoveGotoSky == 1)
			{
				num8 = 0;
			}
			Interface_Game.PaintHPMP(g, 1, num8, MapOff_RedLine.timeRedline, MotherCanvas.hw - 40, yPaintAudition - 20, 0, 7, 80, 4, isflip: false, 0, isUpdateEff: false, 0);
			paintAudition(g);
		}
		if (!GameCanvas.isTouch)
		{
			return;
		}
		g.drawImage(Interface_Game.imgMove[0], Interface_Game.xPointMove, Interface_Game.yPointMove, 3);
		for (int n = 0; n < 4; n++)
		{
			if (Interface_Game.timePointer > 0 && GameScreen.interfaceGame.mKeyMove[n] == Interface_Game.keyPoint)
			{
				GameScreen.interfaceGame.paintMoveTouch(g, n);
			}
		}
	}

	public static void paintAxe(mGraphics g, Point p)
	{
		int num = p.x + p.x2;
		int num2 = p.y + p.y2 + imgAxe.h / 2;
		for (int i = -1; i < 2; i++)
		{
			g.drawLine(MotherCanvas.w / 4 + p.x2 + i, -MotherCanvas.h / 2 + i, num + i, num2 - imgAxe.h / 2 + i);
		}
		g.drawImage(imgAxe, num, num2, 33);
	}

	public static void paintAudition(mGraphics g)
	{
		int num = MotherCanvas.hw - ReadMessenge.mImgCapredLine.Length / 2 * Interface_Game.wComboSkill;
		int num2 = ReadMessenge.mImgCapredLine.Length;
		for (int i = 0; i < num2; i++)
		{
			int num3 = i;
			int x = Interface_Game.wComboSkill;
			if (num3 == 0)
			{
				x = 0;
			}
			else if (num3 == num2 - 1)
			{
				x = Interface_Game.wComboSkill * 2;
			}
			g.drawRegion(AvMain.imgCombo, x, 0, Interface_Game.wComboSkill, Interface_Game.wComboSkill, 0, num + i * Interface_Game.wComboSkill - Interface_Game.wComboSkill / 2, yPaintAudition - Interface_Game.wComboSkill / 2, 0);
			g.drawImage(ReadMessenge.mImgCapredLine[i], num + i * Interface_Game.wComboSkill, yPaintAudition, 3);
			if (num3 <= Player.indexAudition)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, Interface_Game.wComboSkill, Interface_Game.wComboSkill, 0, num + i * Interface_Game.wComboSkill - Interface_Game.wComboSkill / 2, yPaintAudition - Interface_Game.wComboSkill / 2, 0);
			}
		}
	}
}
