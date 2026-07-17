public class MapGotoSky
{
	public static int x = 140;

	public static int y = MotherCanvas.h - 100;

	public static int w;

	public static int h;

	public static int stepMove = 0;

	public static int hMax;

	public static int wMax;

	public static mVector vecEff_Song_Ria = new mVector("MapGotoSky.Song_Ria");

	public static mVector vecEff_Song_Giua = new mVector("MapGotoSky.Song_Giua");

	public static mVector vecEff_Song_Nho = new mVector("MapGotoSky.Song_Nho");

	public static mVector vecEff_Song_Keo = new mVector("MapGotoSky.Song_Keo");

	public static mVector vecEff_Song_Bien = new mVector("MapGotoSky.Song_Bien");

	public static mVector vecEff_Song_Bien_Nho = new mVector("MapGotoSky.Song_Bien_Nho");

	public static mVector vecEff_May = new mVector("MapGotoSky.May");

	public static mVector vecEff_Die = new mVector("MapGotoSky.Die");

	public static mVector vecEff_May_Goto = new mVector("MapGotoSky.May_Goto");

	public static FrameImage fraEffSky0;

	public static FrameImage fraEffSky1;

	public static FrameImage fraEffSky2;

	public static FrameImage fraEffSky3;

	public static FrameImage fraEffSky4;

	public static FrameImage fraEffSky5;

	public static FrameImage fraEffSky6;

	public static FrameImage fraEffSky7;

	public static mImage[] mcloud = new mImage[3];

	public static mImage[] mcloudGoto = new mImage[3];

	public static int[] mPlayFrameSong = new int[7] { 0, 1, 1, 1, 1, 1, 2 };

	public static int[] mPlayFrameBottom = new int[7] { 0, 2, 2, 2, 2, 2, 4 };

	public static int[][] mPosGotoSky = new int[5][]
	{
		new int[2] { 180, 90 },
		new int[2] { 220, 120 },
		new int[2] { 200, 190 },
		new int[2] { 244, 270 },
		new int[2] { 184, 320 }
	};

	public static int[][] mGo;

	public static int[][] mPosWinSky = new int[2][]
	{
		new int[2] { 244, 420 },
		new int[2] { 184, 600 }
	};

	public static bool isBeginEffBoat = false;

	public static bool isStopUpdateCamera = false;

	public static int tickGo = 0;

	public static int valueGo;

	public static void setPos()
	{
		hMax = 700;
		wMax = 480;
		x = 140;
		y = hMax - 120;
		w = 170;
		h = 504;
		LoadImage();
		vecEff_Song_Ria.removeAllElements();
		for (int i = 0; i <= h / 24; i++)
		{
			Point point = new Point(x + 1, y - 24 - i * 24);
			point.dis = 2;
			point.f = CRes.random(fraEffSky0.nFrame * 2);
			point.vy = -8;
			vecEff_Song_Ria.addElement(point);
			Point point2 = new Point(x - 1 + w, y - 24 - i * 24);
			point2.dis = 0;
			point2.f = CRes.random(fraEffSky0.nFrame * 2);
			point2.vy = -8;
			vecEff_Song_Ria.addElement(point2);
		}
		vecEff_Song_Giua.removeAllElements();
		for (int j = 0; j < MotherCanvas.h / 60; j++)
		{
			Point point3 = new Point(x + 12, y - 15);
			if (j > 0)
			{
				point3.y -= 80 * j + CRes.random(40);
			}
			point3.frame = 0;
			point3.vy = -8;
			vecEff_Song_Giua.addElement(point3);
		}
		vecEff_Song_Nho.removeAllElements();
		for (int k = 0; k < MotherCanvas.h / 60; k++)
		{
			Point point4 = new Point(x - 24 + CRes.random(w + 48), y - 15 - CRes.random(h));
			point4.frame = 0;
			point4.vy = -CRes.random(7, 12);
			vecEff_Song_Nho.addElement(point4);
		}
		vecEff_Song_Keo.removeAllElements();
		for (int l = 0; l < 12; l++)
		{
			Point point5 = new Point(x - 24 + CRes.random(w + 48), y + 40);
			if (point5.x - x < (w + 36) / 3)
			{
				point5.frame = 1;
				point5.dis = 0;
				point5.vx = 2;
			}
			else if (point5.x - x > (w + 36) / 3 * 2)
			{
				point5.frame = 1;
				point5.dis = 2;
				point5.vx = CRes.random_Am_0(2);
			}
			else
			{
				point5.frame = 0;
				point5.dis = 0;
				point5.vx = -2;
			}
			point5.vy = -7;
			vecEff_Song_Keo.addElement(point5);
		}
		vecEff_Song_Bien.removeAllElements();
		for (int m = 0; m < w / LoadMap.wTile + 5; m++)
		{
			Point point6 = new Point((x / 24 - 2) * 24 + m * 24, y / 24 * 24);
			point6.frame = CRes.random((fraEffSky5.nFrame + 1) * 2);
			point6.dis = ((CRes.random(5) != 0) ? 2 : 0);
			vecEff_Song_Bien.addElement(point6);
			Point point7 = new Point((x / 24 - 2) * 24 + m * 24, (y / 24 + 1) * 24);
			point7.frame = CRes.random((fraEffSky5.nFrame + 1) * 2);
			point7.dis = ((CRes.random(5) != 0) ? 2 : 0);
			vecEff_Song_Bien.addElement(point7);
			if (m != 0 && m != w / LoadMap.wTile + 4)
			{
				Point point8 = new Point((x / 24 - 2) * 24 + m * 24, (y / 24 + 2) * 24);
				point8.frame = CRes.random((fraEffSky5.nFrame + 1) * 2);
				point8.dis = ((CRes.random(5) != 0) ? 2 : 0);
				vecEff_Song_Bien.addElement(point8);
				if (m <= 2 || m >= w / LoadMap.wTile)
				{
					Point point9 = new Point((x / 24 - 2) * 24 + m * 24, (y / 24 - 1) * 24);
					point9.frame = CRes.random((fraEffSky5.nFrame + 1) * 2);
					point9.dis = ((CRes.random(5) != 0) ? 2 : 0);
					vecEff_Song_Bien.addElement(point9);
				}
			}
		}
		vecEff_Song_Bien_Nho.removeAllElements();
		for (int n = 0; n <= wMax / LoadMap.wTile; n++)
		{
			for (int num = 0; num <= 160 / LoadMap.wTile; num++)
			{
				Point point10 = new Point(n * LoadMap.wTile, hMax - 160 + num * LoadMap.wTile);
				point10.frame = CRes.random(14);
				point10.fSmall = CRes.random(6);
				point10.dis = CRes.random(5);
				point10.vx = 0;
				point10.vy = 0;
				vecEff_Song_Bien_Nho.addElement(point10);
			}
		}
		vecEff_May.removeAllElements();
		vecEff_Die.removeAllElements();
		mGo = mSystem.new_M_Int(mPosGotoSky.Length, 2);
		for (int num2 = 0; num2 < mPosGotoSky.Length; num2++)
		{
			mGo[num2] = new int[2];
			mGo[num2][0] = mPosGotoSky[num2][0];
			mGo[num2][1] = hMax - 30 - mPosGotoSky[num2][1];
		}
		for (int num3 = 0; num3 <= 10; num3++)
		{
			Point point11 = new Point(num3 * 45, hMax - 30 - 550 - 20);
			point11.frame = 0;
			vecEff_May_Goto.addElement(point11);
		}
		for (int num4 = 0; num4 <= 5; num4++)
		{
			Point point12 = new Point(num4 * 86, hMax - 30 - 550 - 6 - 25);
			point12.frame = 1;
			vecEff_May_Goto.addElement(point12);
		}
		for (int num5 = 0; num5 <= 6; num5++)
		{
			Point point13 = new Point(num5 * 70, hMax - 30 - 550 - 20 - 20);
			point13.frame = 2;
			vecEff_May_Goto.addElement(point13);
		}
		GameCanvas.loadMapScr.mItemMap = null;
		stepMove = 0;
		GameScreen.player.setSpeed(2, 2);
		GameScreen.player.x = 50;
		GameScreen.player.y = hMax - 30;
		GameScreen.player.Dir = 2;
		GameScreen.player.type_left_right = 2;
		GameScreen.player.boatSea = new Boat(GameScreen.player.ID, GameScreen.player.x, GameScreen.player.y, 0, (sbyte)GameScreen.player.type_left_right);
		GameScreen.player.boatSea.setPartImage(GameScreen.player.myBoat, GameScreen.player.typePirate);
		GameScreen.player.toX = mGo[stepMove][0];
		GameScreen.player.toY = mGo[stepMove][1];
		GameScreen.player.isMoveNor = true;
		MainScreen.cameraMain.xCam = 0;
		MainScreen.cameraMain.yCam = 0;
		MainScreen.cameraMain.setAll(wMax - MotherCanvas.w, hMax - MotherCanvas.h, GameScreen.player.x - MotherCanvas.hw, GameScreen.player.y - MotherCanvas.hh);
		isBeginEffBoat = false;
		isStopUpdateCamera = false;
		tickGo = 0;
		valueGo = 0;
		MapOff_RedLine.timeRedline = 0;
		MapOff_RedLine.timebegin = 0L;
	}

	public static void LoadImage()
	{
		fraEffSky0 = new FrameImage(mImage.createImage("/bg/eff_sky0.png"), 24, 24);
		fraEffSky1 = new FrameImage(mImage.createImage("/bg/eff_sky1.png"), 24, 24);
		fraEffSky2 = new FrameImage(mImage.createImage("/bg/eff_sky2.png"), 36, 32);
		fraEffSky3 = new FrameImage(mImage.createImage("/bg/eff_sky3.png"), 24, 24);
		fraEffSky4 = new FrameImage(mImage.createImage("/bg/eff_sky4.png"), 24, 24);
		fraEffSky5 = new FrameImage(mImage.createImage("/bg/sea1.png"), 24, 24);
		fraEffSky6 = new FrameImage(mImage.createImage("/bg/sea2.png"), 24, 24);
		fraEffSky7 = new FrameImage(mImage.createImage("/bg/redline6.png"), 48, 48);
		for (int i = 0; i < mcloud.Length; i++)
		{
			mcloud[i] = mImage.createImage("/bg/cloud" + i + ".png");
		}
		for (int j = 0; j < mcloudGoto.Length; j++)
		{
			mcloudGoto[j] = mImage.createImage("/bg/goto" + j + ".png");
		}
	}

	public static void paint(mGraphics g)
	{
		paintEff(g);
	}

	public static void paintEff(mGraphics g)
	{
		g.setColor(4367095);
		g.fillRect(0, hMax - 200, wMax, 200);
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.paint(g);
		}
		for (int i = 0; i < vecEff_Song_Bien_Nho.size(); i++)
		{
			Point point = (Point)vecEff_Song_Bien_Nho.elementAt(i);
			if (point.frame / 2 < 7)
			{
				if (point.fSmall == 0)
				{
					fraEffSky5.drawFrame(point.frame / 2, point.x, point.y, (point.dis == 0) ? 2 : 0, 0, g);
				}
				else
				{
					fraEffSky6.drawFrame((3 + point.frame / 2) % fraEffSky6.nFrame, point.x, point.y, (point.dis == 0) ? 2 : 0, 0, g);
				}
			}
		}
		for (int j = 0; j < vecEff_Song_Bien.size(); j++)
		{
			Point point2 = (Point)vecEff_Song_Bien.elementAt(j);
			if (point2.frame / 2 < fraEffSky5.nFrame)
			{
				fraEffSky5.drawFrame(point2.frame / 2, point2.x, point2.y, point2.dis, 0, g);
			}
		}
		for (int k = 0; k < vecEff_May.size(); k++)
		{
			Point point3 = (Point)vecEff_May.elementAt(k);
			if (point3.dis != 0)
			{
				g.drawImage(mcloud[point3.frame], point3.x, point3.y, 3);
			}
		}
		if (GameScreen.player.typeMoveGotoSky == 3)
		{
			g.setColor(14284799);
			g.fillRect(MainScreen.cameraMain.xCam, hMax - 30 - 550 - 60, MotherCanvas.w, 40);
			for (int l = 0; l < vecEff_May_Goto.size(); l++)
			{
				Point point4 = (Point)vecEff_May_Goto.elementAt(l);
				if (point4.frame == 0 && (point4.x + 25 > MainScreen.cameraMain.xCam || point4.x - 25 < MainScreen.cameraMain.xCam + MotherCanvas.w))
				{
					g.drawImage(mcloudGoto[0], point4.x, point4.y, 0);
				}
			}
		}
		g.setColor(5555965);
		g.fillRect(x + 3, y - h, w - 6, h - 24);
		for (int m = 0; m < 7; m++)
		{
			fraEffSky3.drawFrame(mPlayFrameBottom[m] + GameCanvas.gameTick / 2 % 2, x + 1 + m * 24, y - 24, 0, 0, g);
		}
		for (int n = 0; n < vecEff_Song_Ria.size(); n++)
		{
			Point point5 = (Point)vecEff_Song_Ria.elementAt(n);
			fraEffSky0.drawFrame(point5.f / 2 % fraEffSky0.nFrame, point5.x, point5.y, point5.dis, 3, g);
		}
		for (int num = 0; num < vecEff_Song_Giua.size(); num++)
		{
			Point point6 = (Point)vecEff_Song_Giua.elementAt(num);
			for (int num2 = 0; num2 < 7; num2++)
			{
				fraEffSky1.drawFrame(mPlayFrameSong[num2], point6.x + num2 * 24, point6.y, 0, 3, g);
			}
		}
		for (int num3 = 0; num3 < vecEff_Song_Nho.size(); num3++)
		{
			Point point7 = (Point)vecEff_Song_Nho.elementAt(num3);
			fraEffSky2.drawFrame(point7.f / 2 % fraEffSky2.nFrame, point7.x, point7.y, 0, 3, g);
		}
		for (int num4 = 0; num4 < vecEff_Song_Keo.size(); num4++)
		{
			Point point8 = (Point)vecEff_Song_Keo.elementAt(num4);
			fraEffSky4.drawFrame(point8.frame * 4 + point8.fSmall, point8.x, point8.y, point8.dis, 3, g);
		}
		GameScreen.player.paintGotoSky(g);
		for (int num5 = 0; num5 < vecEff_Die.size(); num5++)
		{
			Point point9 = (Point)vecEff_Die.elementAt(num5);
			fraEffSky4.drawFrame(point9.frame * 4 + point9.fSmall, point9.x, point9.y, point9.dis, 3, g);
		}
		for (int num6 = 0; num6 < vecEff_May.size(); num6++)
		{
			Point point10 = (Point)vecEff_May.elementAt(num6);
			if (point10.dis == 0)
			{
				g.drawImage(mcloud[point10.frame], point10.x, point10.y, 3);
			}
		}
		if (GameScreen.player.typeMoveGotoSky == 3)
		{
			for (int num7 = 0; num7 < vecEff_May_Goto.size(); num7++)
			{
				Point point11 = (Point)vecEff_May_Goto.elementAt(num7);
				if (point11.frame != 0 && (point11.x + 45 > MainScreen.cameraMain.xCam || point11.x - 45 < MainScreen.cameraMain.xCam + MotherCanvas.w))
				{
					g.drawImage(mcloudGoto[point11.frame], point11.x, point11.y, 0);
				}
			}
		}
		GameCanvas.resetTrans(g);
		if (ReadMessenge.mImgCapredLine != null)
		{
			g.setColor(0);
			g.drawRect(MotherCanvas.hw - 40 - 1, MapOff_RedLine.yPaintAudition - 20 - 1, 81, 8);
			int num8 = (int)(MapOff_RedLine.timeRedline - (GameCanvas.timeNow - MapOff_RedLine.timebegin));
			if (num8 <= 0 && GameScreen.player.typeMoveGotoSky == 1)
			{
				num8 = 0;
			}
			Interface_Game.PaintHPMP(g, 1, num8, MapOff_RedLine.timeRedline, MotherCanvas.hw - 40, MapOff_RedLine.yPaintAudition - 20, 0, 7, 80, 4, isflip: false, 0, isUpdateEff: false, 0);
			paintAudition(g);
		}
		if (!GameCanvas.isTouch)
		{
			return;
		}
		g.drawImage(Interface_Game.imgMove[0], Interface_Game.xPointMove, Interface_Game.yPointMove, 3);
		for (int num9 = 0; num9 < 4; num9++)
		{
			if (Interface_Game.timePointer > 0 && GameScreen.interfaceGame.mKeyMove[num9] == Interface_Game.keyPoint)
			{
				GameScreen.interfaceGame.paintMoveTouch(g, num9);
			}
		}
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
			g.drawRegion(AvMain.imgCombo, x, 0, Interface_Game.wComboSkill, Interface_Game.wComboSkill, 0, num + i * Interface_Game.wComboSkill - Interface_Game.wComboSkill / 2, MapOff_RedLine.yPaintAudition - Interface_Game.wComboSkill / 2, 0);
			g.drawImage(ReadMessenge.mImgCapredLine[i], num + i * Interface_Game.wComboSkill, MapOff_RedLine.yPaintAudition, 3);
			if (num3 <= Player.indexAudition)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, Interface_Game.wComboSkill, Interface_Game.wComboSkill, 0, num + i * Interface_Game.wComboSkill - Interface_Game.wComboSkill / 2, MapOff_RedLine.yPaintAudition - Interface_Game.wComboSkill / 2, 0);
			}
		}
	}

	public static void update()
	{
		tickGo++;
		if (tickGo == 2000)
		{
			valueGo = 10;
		}
		else if (tickGo == 2500)
		{
			valueGo = 15;
		}
		else if (tickGo == 3500)
		{
			valueGo = 20;
		}
		updateEff();
		updateActionPlayer();
		if (GameScreen.player.typeMoveGotoSky == 1)
		{
			updateKeyAudition();
		}
		GameScreen.player.updateMapGotoSky();
		MainScreen.cameraMain.UpdateCameraGameScreen();
		if (Interface_Game.timePointer > 0)
		{
			Interface_Game.timePointer--;
		}
	}

	public static void updateActionPlayer()
	{
		if (GameScreen.player.typeMoveGotoSky == 0)
		{
			if (stepMove >= mGo.Length || CRes.abs(GameScreen.player.x - mGo[stepMove][0]) >= 10 || CRes.abs(GameScreen.player.y - mGo[stepMove][1]) >= 10)
			{
				return;
			}
			stepMove++;
			if (stepMove >= mGo.Length)
			{
				GameScreen.player.vx = 0;
				GameScreen.player.vy = 0;
				GameScreen.player.typeMoveGotoSky = 1;
				return;
			}
			SetPosNextMove();
			if (stepMove == 1)
			{
				isBeginEffBoat = true;
				GameScreen.player.setSpeed(4, 4);
			}
		}
		else if (GameScreen.player.typeMoveGotoSky == 1)
		{
			if (CRes.abs(GameScreen.player.x - GameScreen.player.toX) < 4 && CRes.abs(GameScreen.player.y - GameScreen.player.toY) < 4 && CRes.random(25) == 0)
			{
				GameScreen.player.toX = x + 20 + CRes.random(w - 76);
			}
		}
		else
		{
			if (GameScreen.player.typeMoveGotoSky != 3)
			{
				return;
			}
			if (stepMove < mGo.Length && CRes.abs(GameScreen.player.x - mGo[stepMove][0]) < 10 && CRes.abs(GameScreen.player.y - mGo[stepMove][1]) < 10)
			{
				stepMove++;
				if (stepMove < mGo.Length)
				{
					SetPosNextMove();
				}
			}
			if (MainScreen.cameraMain.yTo <= hMax - 30 - 540 - 45)
			{
				MainScreen.cameraMain.yTo = hMax - 30 - 540 - 45;
				isStopUpdateCamera = true;
			}
		}
	}

	public static void SetPosNextMove()
	{
		GameScreen.player.toX = mGo[stepMove][0];
		GameScreen.player.toY = mGo[stepMove][1];
		GameScreen.player.isMoveNor = true;
	}

	public static void updateEff()
	{
		for (int i = 0; i < vecEff_Song_Ria.size(); i++)
		{
			Point point = (Point)vecEff_Song_Ria.elementAt(i);
			point.y += point.vy;
			point.f++;
			if (point.y < y - h)
			{
				point.y = y - 24;
			}
		}
		for (int j = 0; j < vecEff_Song_Giua.size(); j++)
		{
			Point point2 = (Point)vecEff_Song_Giua.elementAt(j);
			point2.y += point2.vy;
			point2.f++;
			if (point2.y < y - h)
			{
				if (point2.frame == 0)
				{
					point2.y = y - 15;
					continue;
				}
				vecEff_Song_Giua.removeElementAt(j);
				j--;
			}
		}
		for (int k = 0; k < vecEff_Song_Nho.size(); k++)
		{
			Point point3 = (Point)vecEff_Song_Nho.elementAt(k);
			point3.y += point3.vy;
			point3.f++;
			if (point3.y < y - h)
			{
				vecEff_Song_Nho.removeElementAt(k);
				k--;
			}
		}
		for (int l = 0; l < vecEff_Song_Nho.size(); l++)
		{
			Point point4 = (Point)vecEff_Song_Nho.elementAt(l);
			point4.y += point4.vy;
			point4.f++;
			if (point4.y < y - h)
			{
				vecEff_Song_Nho.removeElementAt(l);
				l--;
			}
		}
		for (int m = 0; m < vecEff_Song_Keo.size(); m++)
		{
			Point point5 = (Point)vecEff_Song_Keo.elementAt(m);
			point5.y += point5.vy;
			point5.x += point5.vx;
			point5.f++;
			if (point5.f < 4)
			{
				point5.fSmall = point5.f;
			}
			else if (point5.f <= 6)
			{
				point5.fSmall = 3;
			}
			else if (point5.f > 6 && point5.f < 10)
			{
				point5.fSmall = 9 - point5.f;
			}
			else
			{
				point5.fSmall = 0;
			}
			if (point5.y < y - 24)
			{
				vecEff_Song_Keo.removeElementAt(m);
				m--;
			}
		}
		for (int n = 0; n < vecEff_Song_Bien.size(); n++)
		{
			Point point6 = (Point)vecEff_Song_Bien.elementAt(n);
			point6.frame++;
			if (point6.frame / 2 >= fraEffSky5.nFrame && CRes.random(4) == 0)
			{
				point6.frame = 0;
				point6.dis = ((CRes.random(5) != 0) ? 2 : 0);
			}
		}
		for (int num = 0; num < vecEff_Song_Bien_Nho.size(); num++)
		{
			Point point7 = (Point)vecEff_Song_Bien_Nho.elementAt(num);
			point7.frame++;
			if (point7.frame / 2 >= fraEffSky5.nFrame && CRes.random(10) == 0)
			{
				point7.frame = 0;
				point7.fSmall = CRes.random(6);
				point7.dis = CRes.random(5);
			}
		}
		for (int num2 = 0; num2 < vecEff_May.size(); num2++)
		{
			Point point8 = (Point)vecEff_May.elementAt(num2);
			point8.update();
			if (point8.x < MainScreen.cameraMain.xCam - 50 || point8.y > MainScreen.cameraMain.yCam + MotherCanvas.h + 15)
			{
				vecEff_May.removeElementAt(num2);
				num2--;
			}
		}
		if (CRes.random(20) == 0)
		{
			Point point9 = new Point(x + 18, y - 15);
			point9.frame = 1;
			point9.vy = -CRes.random(6, 10);
			vecEff_Song_Giua.addElement(point9);
		}
		if (CRes.random(5) == 0)
		{
			Point point10 = new Point(x + 18 + CRes.random(w - 36), y - 15 - CRes.random(10));
			point10.frame = 0;
			point10.vy = -CRes.random(7, 12);
			vecEff_Song_Nho.addElement(point10);
		}
		if (CRes.random(3) == 0)
		{
			int num3 = CRes.random(2, 4);
			for (int num4 = 0; num4 < num3; num4++)
			{
				Point point11 = new Point(x - 24 + CRes.random(w + 48), y + 40);
				if (point11.x - x < (w + 36) / 3)
				{
					point11.frame = 1;
					point11.dis = 0;
					point11.vx = 1;
				}
				else if (point11.x - x > (w + 36) / 3 * 2)
				{
					point11.frame = 1;
					point11.dis = 2;
					point11.vx = CRes.random_Am_0(2);
				}
				else
				{
					point11.frame = 0;
					point11.dis = 0;
					point11.vx = -1;
				}
				point11.vy = -7;
				vecEff_Song_Keo.addElement(point11);
			}
		}
		if (tickGo > 1000 && CRes.random(30 - valueGo) == 0)
		{
			Point point12 = new Point();
			point12.x = MainScreen.cameraMain.xCam + 10 + CRes.random(MotherCanvas.w);
			point12.y = MainScreen.cameraMain.yCam - 10;
			point12.vx = -CRes.random(2);
			point12.vy = 2;
			point12.frame = CRes.random(3);
			point12.dis = CRes.random(4);
			vecEff_May.addElement(point12);
		}
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

	public static void setTypeMoveredLine(sbyte type)
	{
		GameScreen.player.typeMoveGotoSky = type;
		if (type == 2)
		{
			GameScreen.player.vy = -12;
			GameScreen.player.vx = -8;
			GameScreen.player.boatSea.vecEff.removeAllElements();
			isStopUpdateCamera = true;
			for (int i = 0; i < 3; i++)
			{
				Point point = new Point();
				point.x = GameScreen.player.x + CRes.random_Am_0(15);
				point.y = GameScreen.player.y + CRes.random_Am_0(5);
			}
		}
		else if (type == 3)
		{
			stepMove = 0;
			mGo = mSystem.new_M_Int(mPosGotoSky.Length, 2);
			for (int j = 0; j < mPosWinSky.Length; j++)
			{
				mGo[j] = new int[2];
				mGo[j][0] = mPosWinSky[j][0];
				mGo[j][1] = hMax - 30 - mPosWinSky[j][1];
			}
			GameScreen.player.toX = mGo[stepMove][0];
			GameScreen.player.toY = mGo[stepMove][1];
		}
	}
}
