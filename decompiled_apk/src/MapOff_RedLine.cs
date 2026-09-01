public class MapOff_RedLine
{
	public static mImage[] mImgMapOffline;

	public static mVector vecSongTuong = new mVector("MapOff_RedLine.SongTuong");

	public static mVector vecSongBien = new mVector("MapOff_RedLine.SongBien");

	public static mVector vecDaBien = new mVector("MapOff_RedLine.DaBien");

	public static mVector vecEffDie = new mVector("MapOff_RedLine.EffDie");

	public static mVector vecEffSongTuong = new mVector("MapOff_RedLine.EffSongTuong");

	public static int xOffline;

	public static int yOffline;

	public static int xCamOffline;

	public static int yCamOffline;

	public static int timeRedline;

	public static int tickNoPaint = 0;

	public static int tickSendDie = 0;

	public static long timebegin;

	public static int yHarcodeMapRedLine = MotherCanvas.h / 5 * 4;

	public static int xHardCodeMapRedLine = MotherCanvas.w / 3;

	public static int vxHardcodeRedLine = 6;

	public static int[] mPlayFrameSong = new int[5] { 2, 0, 1, 2, 3 };

	public static bool isFinish = false;

	public static int yPaintAudition = MotherCanvas.h - 20;

	public static int tickAddDa = 0;

	public static void setTypeMoveredLine(sbyte type)
	{
		GameScreen.player.typeMoveMapRedLine = type;
		switch (type)
		{
		case 1:
		{
			Point point = new Point();
			point.dis = GameScreen.player.lineShowRedLineNext;
			point.x = MotherCanvas.w + 48;
			point.y = yHarcodeMapRedLine - 48 + point.dis * 24 - (MotherCanvas.w - xHardCodeMapRedLine) / 48 * 10;
			point.frame = CRes.random(2);
			vecDaBien.addElement(point);
			break;
		}
		case 2:
			Player.isShowDie = 2;
			GameScreen.player.tickFinish = 0;
			break;
		}
	}

	public static void paintRedLine(mGraphics g)
	{
		if (tickNoPaint > 0)
		{
			tickNoPaint--;
			return;
		}
		if (isFinish)
		{
			paintRedLine_Finish(g);
			return;
		}
		if (mImgMapOffline == null)
		{
			loadImgRedLine();
		}
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.paint(g);
		}
		int num = MotherCanvas.w / 48 + 2;
		int num2 = xOffline + xCamOffline % 48;
		int num3 = yOffline + yCamOffline % 10;
		for (int i = 0; i < num; i++)
		{
			g.drawImage(mImgMapOffline[2], num2 + i * 48, num3 - 128 - i * 10, 0);
		}
		int num4 = 0;
		for (int j = 0; j < num; j++)
		{
			g.drawRegion(mImgMapOffline[5], num4 % 3 * 24, 0, 48, 24, 0, num2 + j * 48, num3 - j * 10 - 6, 0);
			for (int k = 0; k <= (MotherCanvas.h - num3 + j * 10 + 6) / 24; k++)
			{
				num4++;
				g.drawRegion(mImgMapOffline[5], num4 % 3 * 24, 0, 48, 24, 0, num2 + j * 48, num3 - j * 10 - 6 + (k + 1) * 24, 0);
			}
			num4++;
		}
		for (int l = 0; l < vecSongTuong.size(); l++)
		{
			Point point = (Point)vecSongTuong.elementAt(l);
			g.drawRegion(mImgMapOffline[0], 0, point.frame * 24, 24, 24, 0, num2 + point.x, num3 + point.y - 24, 0);
		}
		for (int m = 0; m < vecSongBien.size(); m++)
		{
			Point point2 = (Point)vecSongBien.elementAt(m);
			if (point2.isSmall)
			{
				g.drawRegion(mImgMapOffline[1], 0, point2.frame * 24, 24, 24, 0, point2.x2 + point2.x + point2.x0, point2.y2 + point2.y + point2.y0, 0);
			}
		}
		for (int n = 0; n < vecDaBien.size(); n++)
		{
			Point point3 = (Point)vecDaBien.elementAt(n);
			if (point3.dis < GameScreen.player.lineShowRedLineCur)
			{
				paintDa(g, point3);
			}
		}
		for (int num5 = 0; num5 < vecEffSongTuong.size(); num5++)
		{
			Point point4 = (Point)vecEffSongTuong.elementAt(num5);
			g.drawRegion(mImgMapOffline[6], 0, mPlayFrameSong[point4.fSmall] * 48, 48, 48, 0, point4.x + point4.x2, point4.y + point4.y2 - 3, 33);
		}
		GameScreen.player.paint(g);
		for (int num6 = 0; num6 < vecDaBien.size(); num6++)
		{
			Point point5 = (Point)vecDaBien.elementAt(num6);
			if (point5.dis >= GameScreen.player.lineShowRedLineCur)
			{
				paintDa(g, point5);
			}
		}
		for (int num7 = 0; num7 < vecEffDie.size(); num7++)
		{
			Point point6 = (Point)vecEffDie.elementAt(num7);
			g.drawRegion(mImgMapOffline[6], 0, mPlayFrameSong[point6.fSmall] * 48, 48, 48, 0, point6.x, point6.y, 33);
		}
		if (ReadMessenge.mImgCapredLine != null)
		{
			g.setColor(0);
			g.drawRect(MotherCanvas.hw - 40 - 1, yPaintAudition - 20 - 1, 81, 8);
			int num8 = (int)(timeRedline - (GameCanvas.timeNow - timebegin));
			if (num8 <= 0 && GameScreen.player.typeMoveGotoSky == 1)
			{
				num8 = 0;
			}
			Interface_Game.PaintHPMP(g, 1, num8, timeRedline, MotherCanvas.hw - 40, yPaintAudition - 20, 0, 7, 80, 4, isflip: false, 0, isUpdateEff: false, 0);
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

	public static void paintDa(mGraphics g, Point p)
	{
		if (mImgMapOffline == null)
		{
			loadImgRedLine();
		}
		g.drawImage(mImgMapOffline[3 + p.frame], p.x + p.x2, p.y + p.y2 + 10, 33);
		g.drawRegion(mImgMapOffline[6], 0, (2 + GameCanvas.gameTick / 3 % 2) * 48, 48, 48, 0, p.x + p.x2 - 24 + 6, p.y + p.y2 + 14, 33);
		if (p.isSmall)
		{
			g.drawRegion(mImgMapOffline[6], 0, mPlayFrameSong[p.fSmall] * 48, 48, 48, 0, p.x + p.x2 - 24 + p.fSmall * 3, p.y + p.y2 + 14, 33);
		}
	}

	public static void loadImgRedLine()
	{
		mImgMapOffline = new mImage[8];
		for (int i = 0; i < mImgMapOffline.Length; i++)
		{
			if (i == 5)
			{
				mImgMapOffline[i] = mImage.createImage("/bg/b03.png");
			}
			else
			{
				mImgMapOffline[i] = mImage.createImage("/bg/redline" + i + ".png");
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
			g.drawRegion(AvMain.imgCombo, x, 0, Interface_Game.wComboSkill, Interface_Game.wComboSkill, 0, num + i * Interface_Game.wComboSkill - Interface_Game.wComboSkill / 2, yPaintAudition - Interface_Game.wComboSkill / 2, 0);
			g.drawImage(ReadMessenge.mImgCapredLine[i], num + i * Interface_Game.wComboSkill, yPaintAudition, 3);
			if (num3 <= Player.indexAudition)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, Interface_Game.wComboSkill, Interface_Game.wComboSkill, 0, num + i * Interface_Game.wComboSkill - Interface_Game.wComboSkill / 2, yPaintAudition - Interface_Game.wComboSkill / 2, 0);
			}
		}
	}

	public static void updateRedLine()
	{
		if (isFinish)
		{
			updateRedLine_Finish();
			return;
		}
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.updateCloud();
		}
		if (Interface_Game.timePointer > 0)
		{
			Interface_Game.timePointer--;
		}
		xCamOffline -= vxHardcodeRedLine;
		yCamOffline = -(xCamOffline * 5) / 24;
		updateSongTuong();
		updateSongBien();
		updateEffDie();
		updateEffSongTuong();
		addDaBien();
		updateDaBien();
		GameScreen.player.updateMapRedLine();
		updateKeyAudition();
		updateRedLineFail();
		addEffSongTuong();
		if (GameScreen.player.typeMoveMapRedLine == 0)
		{
			if (tickSendDie > 0)
			{
				tickSendDie--;
			}
			else if (timeRedline - (GameCanvas.timeNow - timebegin) <= 0)
			{
				GlobalService.gI().Red_Line(0, 10);
				tickSendDie = 40;
			}
		}
	}

	private static void updateEffDie()
	{
		for (int i = 0; i < vecEffDie.size(); i++)
		{
			Point point = (Point)vecEffDie.elementAt(i);
			point.update();
			if (GameCanvas.gameTick % 2 == 0)
			{
				point.fSmall++;
			}
			if (point.fSmall >= mPlayFrameSong.Length)
			{
				vecEffDie.removeElement(point);
				i--;
			}
		}
	}

	private static void updateEffSongTuong()
	{
		for (int i = 0; i < vecEffSongTuong.size(); i++)
		{
			Point point = (Point)vecEffSongTuong.elementAt(i);
			point.x2 -= vxHardcodeRedLine;
			point.y2 = -(point.x2 * 5) / 24;
			if (GameCanvas.gameTick % 2 == 0)
			{
				point.fSmall++;
			}
			if (point.fSmall >= mPlayFrameSong.Length)
			{
				vecEffSongTuong.removeElement(point);
				i--;
			}
		}
	}

	private static void updateRedLineFail()
	{
		if ((Player.isShowDie == 1 || Player.isShowDie == 11) && CRes.random(3) == 0)
		{
			Point point = new Point();
			point.x = GameScreen.player.x + 10 + CRes.random_Am_0(30);
			point.y = GameScreen.player.y + 18 + CRes.random_Am_0(4);
			point.vx = GameScreen.player.vx;
			point.vy = GameScreen.player.vy;
			vecEffDie.addElement(point);
		}
	}

	private static void addEffSongTuong()
	{
		if (CRes.random(10) == 0)
		{
			Point point = new Point();
			point.x = xOffline + CRes.random(MotherCanvas.w);
			point.y = yOffline - point.x / 24 * 5;
			vecEffSongTuong.addElement(point);
		}
	}

	private static void updateSongBien()
	{
		for (int i = 0; i < vecSongBien.size(); i++)
		{
			Point point = (Point)vecSongBien.elementAt(i);
			point.f++;
			if (point.x2 == 0)
			{
				point.x2 = xOffline;
				point.y2 = yOffline;
				point.x0 = 0;
				point.y0 = 0;
			}
			if (point.isSmall)
			{
				if (point.f % 3 == 0)
				{
					point.frame++;
				}
				point.x0 -= 3;
				point.y0 = -(point.x0 * 5) / 24;
			}
			else if (CRes.random(100) == 0)
			{
				point.isSmall = true;
				point.x2 = xOffline;
				point.y2 = yOffline;
				point.x0 = 0;
				point.y0 = 0;
			}
			if (point.frame >= 7)
			{
				point.frame = 0;
				if (CRes.random(100) == 0)
				{
					point.isSmall = true;
					point.x2 = xOffline;
					point.y2 = yOffline;
					point.x0 = 0;
					point.y0 = 0;
				}
				else
				{
					point.isSmall = false;
				}
			}
		}
	}

	private static void updateSongTuong()
	{
		for (int i = 0; i < vecSongTuong.size(); i++)
		{
			Point point = (Point)vecSongTuong.elementAt(i);
			point.f++;
			if (point.isSmall)
			{
				if (point.f % 3 == 0)
				{
					point.frame++;
				}
			}
			else if (CRes.random(5) == 0)
			{
				point.isSmall = true;
			}
			if (point.frame >= 7)
			{
				point.frame = 0;
				if (CRes.random(3) == 0)
				{
					point.isSmall = true;
				}
				else
				{
					point.isSmall = false;
				}
			}
		}
	}

	public static void moveTypeKeypad()
	{
		if (GameCanvas.isPointerSelect && GameCanvas.isPointLast(Interface_Game.xPointMove - 2 * Interface_Game.wArrowMove, Interface_Game.yPointMove - 2 * Interface_Game.wArrowMove, Interface_Game.wArrowMove * 4, Interface_Game.wArrowMove * 4))
		{
			int num = CRes.angle(GameCanvas.px - Interface_Game.xPointMove, GameCanvas.py - Interface_Game.yPointMove);
			int num2 = 0;
			num2 = ((num > 45 && num <= 135) ? 3 : ((num <= 135 || num > 225) ? ((num <= 225 || num > 315) ? 1 : 2) : 0));
			GameCanvas.clearKeyHold();
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerSelect = false;
			Interface_Game.keyPoint = GameScreen.interfaceGame.mKeyMove[num2];
			Interface_Game.timePointer = 3;
			GameCanvas.keyMyHold[Interface_Game.keyPoint] = true;
		}
	}

	private static void updateKeyAudition()
	{
		if (GameCanvas.keyMove(1))
		{
			GlobalService.gI().Red_Line(0, 1);
			GameCanvas.ClearkeyMove(1);
			Player.indexAudition++;
		}
		else if (GameCanvas.keyMove(2))
		{
			GlobalService.gI().Red_Line(0, 2);
			GameCanvas.ClearkeyMove(2);
			Player.indexAudition++;
		}
		else if (GameCanvas.keyMove(3))
		{
			GlobalService.gI().Red_Line(0, 3);
			GameCanvas.ClearkeyMove(3);
			Player.indexAudition++;
		}
		else if (GameCanvas.keyMove(0))
		{
			GlobalService.gI().Red_Line(0, 0);
			GameCanvas.ClearkeyMove(0);
			Player.indexAudition++;
		}
	}

	public static void updateDaBien()
	{
		for (int i = 0; i < vecDaBien.size(); i++)
		{
			Point point = (Point)vecDaBien.elementAt(i);
			point.x2 -= vxHardcodeRedLine;
			point.y2 = -(point.x2 * 5) / 24;
			if (point.isSmall)
			{
				if (GameCanvas.gameTick % 3 == 0)
				{
					point.fSmall++;
					if (point.fSmall >= mPlayFrameSong.Length)
					{
						point.isSmall = false;
					}
				}
			}
			else if (CRes.random(4) == 0)
			{
				point.fSmall = 0;
				point.isSmall = true;
			}
			if (point.x2 < -(MotherCanvas.w + 72))
			{
				vecDaBien.removeElement(point);
				i--;
			}
		}
	}

	public static void addDaBien()
	{
		if (tickAddDa > 0)
		{
			tickAddDa--;
		}
		else if (CRes.random(80) == 0 && Player.isShowDie == 0)
		{
			tickAddDa = 70;
			Point point = new Point();
			point.dis = GameScreen.player.lineShowRedLineNext;
			point.x = MotherCanvas.w + 48;
			point.y = yHarcodeMapRedLine - 48 + point.dis * 24 - (MotherCanvas.w - xHardCodeMapRedLine) / 48 * 10;
			point.frame = CRes.random(2);
			vecDaBien.addElement(point);
		}
	}

	public static void paintRedLine_Finish(mGraphics g)
	{
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.paint(g);
		}
		if (mImgMapOffline == null)
		{
			loadImgRedLine();
		}
		int num = MotherCanvas.w / 48 + 2;
		int num2 = xOffline + xCamOffline % 48;
		int num3 = yOffline + yCamOffline % 10;
		for (int i = 0; i < num; i++)
		{
			g.drawRegion(mImgMapOffline[2], 0, 0, 48, 128, 2, num2 + i * 48, num3 - 128 + i * 10, 0);
		}
		int num4 = 0;
		for (int j = 0; j < num; j++)
		{
			g.drawRegion(mImgMapOffline[5], num4 % 3 * 24, 0, 48, 24, 0, num2 + j * 48, num3 + j * 10 - 6, 0);
			for (int k = 0; k <= (MotherCanvas.h - num3 + j * 10 + 6) / 24; k++)
			{
				num4++;
				g.drawRegion(mImgMapOffline[5], num4 % 3 * 24, 0, 48, 24, 0, num2 + j * 48, num3 + j * 10 - 6 + (k + 1) * 24, 0);
			}
			num4++;
		}
		for (int l = 0; l < vecSongTuong.size(); l++)
		{
			Point point = (Point)vecSongTuong.elementAt(l);
			g.drawRegion(mImgMapOffline[7], 0, point.frame * 24, 24, 24, 0, num2 + point.x, num3 + point.y - 24, 0);
		}
		for (int m = 0; m < vecSongBien.size(); m++)
		{
			Point point2 = (Point)vecSongBien.elementAt(m);
			if (point2.isSmall)
			{
				g.drawRegion(mImgMapOffline[7], 0, point2.frame * 24, 24, 24, 0, point2.x2 + point2.x + point2.x0, point2.y2 + point2.y + point2.y0, 0);
			}
		}
		for (int n = 0; n < vecEffSongTuong.size(); n++)
		{
			Point point3 = (Point)vecEffSongTuong.elementAt(n);
			g.drawRegion(mImgMapOffline[6], 0, mPlayFrameSong[point3.fSmall] * 48, 48, 48, 0, point3.x + point3.x2, point3.y + point3.y2 - 3, 33);
		}
		GameScreen.player.paint(g);
	}

	public static void updateRedLine_Finish()
	{
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.updateCloud();
		}
		xCamOffline -= vxHardcodeRedLine;
		yCamOffline = xCamOffline * 5 / 24;
		updateSongTuong();
		updateSongBien();
		addEffSongTuong_Finish();
		updateEffSongTuong_Finish();
		GameScreen.player.updateRedLineFinish();
	}

	private static void addEffSongTuong_Finish()
	{
		if (CRes.random(10) == 0)
		{
			Point point = new Point();
			point.x = xOffline + CRes.random(MotherCanvas.w);
			point.y = yOffline + point.x / 24 * 5;
			vecEffSongTuong.addElement(point);
		}
	}

	private static void updateEffSongTuong_Finish()
	{
		for (int i = 0; i < vecEffSongTuong.size(); i++)
		{
			Point point = (Point)vecEffSongTuong.elementAt(i);
			point.x2 -= vxHardcodeRedLine;
			point.y2 = point.x2 * 5 / 24;
			if (GameCanvas.gameTick % 2 == 0)
			{
				point.fSmall++;
			}
			if (point.fSmall >= mPlayFrameSong.Length)
			{
				vecEffSongTuong.removeElement(point);
				i--;
			}
		}
	}

	public static void changeFinish()
	{
		GameCanvas.readMess.LoadRedLine(isFinish: true);
		tickNoPaint = 5;
	}
}
