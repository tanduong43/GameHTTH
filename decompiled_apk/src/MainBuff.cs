public class MainBuff
{
	public MainObject obj;

	public short IdBuff;

	public short idIcon;

	public sbyte levelPaint;

	public int timeBuff;

	public int xlech;

	public int ylech;

	public int color;

	public int f;

	public int maxsize;

	private long timeBegin;

	private sbyte numNextframe = 1;

	public const short BUFF_0 = 1003;

	public const short BUFF_1 = 1002;

	public const short BUFF_2 = 1004;

	public const short BUFF_CAUSU = 2008;

	public const short BUFF_TUAN_LOC = 2010;

	public const short BUFF_NEW_LUFFY = 1010;

	public const short BUFF_NEW_ZORO = 1011;

	public const short BUFF_NEW_SANJI = 1012;

	public const short BUFF_NEW_NAMI = 1013;

	public const short BUFF_NEW_USSOP = 1014;

	public const short BUFF_DALTON = 2017;

	public const short BUFF_GLODEN_WEEK_DAM = 2018;

	public const short BUFF_GLODEN_WEEK_COMBO = 2020;

	public const short BUFF_GLODEN_WEEK_DEF = 2019;

	public const short BUFF_ICE = 2032;

	public const short BUFF_LAVA = 2028;

	public const short BUFF_LAVA_2 = 2042;

	public const short BUFF_COMBO_SKILL_GEAR_1 = 1019;

	public const short BUFF_COMBO_SKILL_GEAR_2 = 1020;

	public const short BUFF_COMBO_SKILL_GEAR_3 = 1021;

	public const short BUFF_COMBO_SKILL_GEAR_4 = 1022;

	public const short BUFF_COMBO_SKILL_GEAR_5 = 1023;

	public const short BUFF_PELL = 2037;

	public const short BUFF_LOL_KILL_BOSS = 2035;

	public const short BUFF_LUCCI = 2040;

	public const short BUFF_KILO = 2053;

	public const short BUFF_SOI = 2061;

	public const short BUFF_HUOU = 2064;

	public const short BUFF_XAPHONG = 2067;

	public const short BUFF_DOOR = 2070;

	public const short BUFF_FASHION_RAU_DEN = 2054;

	public const short BUFF_FASHION_RAU_DEN_2 = 2057;

	public const short BUFF_THUNDER_FALL = 2004;

	public const short BUFF_FASHION_INCREASE_DAMAGE = 2059;

	public const short BUFF_DECREASE_DAMAGE = 2060;

	private FrameImage fraBuff;

	private FrameImage fraBuff2;

	private int numFrame;

	private int numFrame2;

	private FrameImage[] mfraBuff;

	public bool isRemove;

	public bool isDevil;

	public mVector vecInfoAtt = new mVector("MainBuff.vecInfoAtt");

	public mVector vecEffBuff = new mVector("MainBuff.vecEffBuff");

	public short head;

	public short body;

	public short leg;

	public short hair = -1;

	public int[] mPlayframe;

	private int Cx;

	private int Cy;

	private int vy;

	public MainBuff(short typeBuff)
	{
		IdBuff = typeBuff;
		levelPaint = 0;
		numNextframe = 6;
		int num = 12;
		switch (IdBuff)
		{
		case 2004:
			fraBuff = new FrameImage(mImage.createImage("/test_eff/skill3/eff.png"), 7);
			break;
		case 2054:
			createCharTeleport();
			break;
		case 1003:
			fraBuff = new FrameImage(90, 26, 19);
			break;
		case 1002:
		{
			fraBuff = new FrameImage(110, 5, 5);
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				num = 6;
			}
			for (int m = 0; m < num; m++)
			{
				Point point4 = new Point(CRes.random_Am_0(22), 8 + -CRes.random(18));
				point4.frame = CRes.random(fraBuff.nFrame);
				point4.x2 = point4.x;
				point4.y2 = point4.y;
				point4.vx = 0;
				point4.vy = -CRes.random(2, 4);
				point4.fRe = CRes.random(15, 20);
				vecEffBuff.addElement(point4);
			}
			break;
		}
		case 2008:
		{
			fraBuff = new FrameImage(105, 5, 5);
			num = 16;
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				num = 8;
			}
			for (int l = 0; l < num; l++)
			{
				Point point3 = new Point();
				int num3 = l;
				point3.frame = CRes.random(4);
				point3.x = -16 + (num3 % 8 + 1) / 2 * 8;
				point3.y = 0;
				if (num3 % 2 == 1)
				{
					point3.y = -50;
				}
				point3.x2 = point3.x;
				point3.y2 = point3.y;
				point3.vx = 0;
				if (num3 % 2 == 1)
				{
					point3.vy = 3;
				}
				else
				{
					point3.vy = -3;
				}
				point3.fRe = 20;
				if (l >= 8)
				{
					point3.y = -25;
					point3.f = point3.fRe / 2;
				}
				vecEffBuff.addElement(point3);
			}
			break;
		}
		case 2010:
		{
			fraBuff = new FrameImage(105, 5, 5);
			fraBuff2 = new FrameImage(110, 5, 5);
			num = 16;
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				num = 8;
			}
			for (int j = 0; j < num; j++)
			{
				Point point = new Point();
				int num2 = j;
				point.x = -16 + (num2 / 2 + 1) / 2 * 8;
				point.y = 0;
				if (num2 % 2 == 1)
				{
					point.dis = 1;
					point.y = -50;
					point.frame = CRes.random(fraBuff2.nFrame);
				}
				else
				{
					point.dis = 0;
					point.frame = CRes.random(fraBuff.nFrame);
				}
				point.x2 = point.x;
				point.y2 = point.y;
				point.vx = 0;
				if (num2 % 2 == 1)
				{
					point.vy = 5;
				}
				else
				{
					point.vy = -5;
				}
				point.fRe = 10;
				vecEffBuff.addElement(point);
			}
			break;
		}
		case 1010:
		case 1011:
		case 1012:
		case 1013:
		case 1014:
		{
			fraBuff = new FrameImage(176, 3, 25, 1);
			num = 16;
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				num = 8;
			}
			maxsize = num;
			for (int k = 0; k < num; k++)
			{
				Point point2 = new Point();
				createPointNewBuff(point2);
				point2.vy = -5;
				vecEffBuff.addElement(point2);
			}
			break;
		}
		case 2017:
			numNextframe = 2;
			ylech = 5;
			fraBuff = new FrameImage(180, 32, 63);
			break;
		case 2018:
		case 2019:
		case 2020:
			levelPaint = -1;
			numNextframe = 3;
			fraBuff = new FrameImage(203, 33, 24);
			break;
		case 1019:
		case 1020:
		case 1021:
		case 1022:
		case 1023:
		{
			switch (typeBuff)
			{
			case 1019:
				color = 8890951;
				break;
			case 1020:
				color = 11960483;
				break;
			case 1021:
				color = 14784319;
				break;
			case 1022:
				color = 7310769;
				break;
			case 1023:
				color = 11807013;
				break;
			}
			num = 16;
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				num = 8;
			}
			maxsize = num;
			for (int i = 0; i < num; i++)
			{
				Point o = new Point
				{
					x = CRes.random_Am_0(15),
					y = 10 - CRes.random(40),
					dis = 1 + CRes.random(3),
					vy = -4
				};
				vecEffBuff.addElement(o);
			}
			break;
		}
		case 2032:
			fraBuff = new FrameImage(248, 40, 15);
			levelPaint = -1;
			break;
		case 2028:
			fraBuff = new FrameImage(249, 42, 19);
			levelPaint = -1;
			break;
		case 2042:
			fraBuff = new FrameImage(250, 57, 57);
			numNextframe = 4;
			mPlayframe = new int[4] { 0, 1, 2, 1 };
			ylech = 10;
			break;
		case 2035:
			fraBuff = new FrameImage(308, 43, 30);
			numNextframe = 2;
			levelPaint = -1;
			break;
		case 2070:
			fraBuff = new FrameImage(424, 3);
			numNextframe = 3;
			levelPaint = -1;
			break;
		case 2067:
			fraBuff = new FrameImage(425, 5);
			numNextframe = 3;
			levelPaint = -1;
			break;
		case 2053:
			mfraBuff = new FrameImage[6]
			{
				new FrameImage(360, 32, 40),
				new FrameImage(361, 32, 40),
				new FrameImage(362, 32, 40),
				new FrameImage(363, 32, 40),
				new FrameImage(364, 32, 40),
				new FrameImage(365, 32, 40)
			};
			numNextframe = 2;
			levelPaint = -1;
			mPlayframe = new int[6] { 0, 1, 2, 3, 4, 5 };
			break;
		case 2059:
			fraBuff = new FrameImage(401, 46, 21);
			levelPaint = -1;
			break;
		case 2060:
			fraBuff = new FrameImage(432, 46, 21);
			fraBuff2 = new FrameImage(400, 3);
			numFrame2 = 0;
			break;
		}
	}

	private void createCharTeleport()
	{
		fraBuff = new FrameImage(mImage.createImage("/eff/n2.png"), 66, 15, 55, 1);
		fraBuff2 = new FrameImage(mImage.createImage("/eff/n3.png"), 67, 3, 25, 1);
		createPoint();
	}

	public void createPoint()
	{
		for (int i = 0; i < 10; i++)
		{
			Point point = new Point(Cx + CRes.random_Am_0(20), Cy - CRes.random(30) + 10);
			point.vy = -2 - CRes.random(4);
			point.dis = 0;
			point.frame = CRes.random(fraBuff2.nFrame);
			point.fRe = CRes.random(12, 20);
			vecEffBuff.addElement(point);
		}
	}

	public void setDevilBuff(short head, short body, short leg)
	{
		isDevil = true;
		this.head = head;
		this.body = body;
		this.leg = leg;
		mSystem.outz("head=" + head + "  body=" + body + "  leg=" + leg);
	}

	public void setObjc(MainObject obj)
	{
		if (IdBuff == 2057)
		{
			obj.addDataEff(3, timeBuff);
		}
	}

	public void setTimeBuff(int timebuff)
	{
		timeBegin = GameCanvas.timeNow;
		timeBuff = timebuff;
	}

	public void setData(short idIcon)
	{
		this.idIcon = idIcon;
	}

	public void setYlech(MainObject obj)
	{
		if (obj != null)
		{
			switch (IdBuff)
			{
			case 1003:
				ylech = -obj.hOne / 2 + 5;
				break;
			case 1002:
				ylech = 0;
				break;
			case 2053:
				ylech = -obj.hOne / 2 + 5;
				break;
			}
		}
	}

	public void paint(mGraphics g, int x, int y)
	{
		switch (IdBuff)
		{
		case 2004:
			fraBuff.drawFrame(numFrame, x, y, 0, 3, g);
			break;
		case 2054:
		{
			for (int j = 0; j < vecEffBuff.size(); j++)
			{
				Point point2 = (Point)vecEffBuff.elementAt(j);
				if (point2.dis == 1)
				{
					fraBuff.drawFrameNew(point2.f / 2 % fraBuff.nFrame, point2.x, point2.y, 0, 33, g);
				}
				else if (point2.dis == 0)
				{
					fraBuff2.drawFrameNew((point2.frame + point2.f / 2) % fraBuff2.nFrame, point2.x, point2.y, 0, 3, g);
				}
			}
			Cx = x;
			Cy = y;
			break;
		}
		case 1003:
			fraBuff.drawFrame(GameCanvas.gameTick / numNextframe % fraBuff.nFrame, x + xlech, y + ylech, 0, 3, g);
			break;
		case 1002:
		case 2008:
		{
			for (int m = 0; m < vecEffBuff.size(); m++)
			{
				Point point5 = (Point)vecEffBuff.elementAt(m);
				fraBuff.drawFrame((point5.frame + point5.f) % fraBuff.nFrame, x + point5.x, y + point5.y, 0, 3, g);
			}
			break;
		}
		case 2010:
		{
			for (int k = 0; k < vecEffBuff.size(); k++)
			{
				Point point3 = (Point)vecEffBuff.elementAt(k);
				if (point3.dis == 0)
				{
					fraBuff.drawFrame((point3.frame + point3.f) % fraBuff.nFrame, x + point3.x, y + point3.y, 0, 3, g);
				}
				else
				{
					fraBuff2.drawFrame((point3.frame + point3.f) % fraBuff2.nFrame, x + point3.x, y + point3.y, 0, 3, g);
				}
			}
			break;
		}
		case 1010:
		case 1011:
		case 1012:
		case 1013:
		case 1014:
		{
			for (int num6 = 0; num6 < vecEffBuff.size(); num6++)
			{
				Point point9 = (Point)vecEffBuff.elementAt(num6);
				fraBuff.drawFrameNew(point9.frame + point9.dis * 5, x + point9.x, y + point9.y, 0, 3, g);
			}
			break;
		}
		case 2017:
		{
			int num3 = GameCanvas.gameTick / numNextframe % (fraBuff.nFrame + 1);
			if (num3 < fraBuff.nFrame)
			{
				fraBuff.drawFrame(num3, x + xlech, y + ylech, 0, 33, g);
			}
			break;
		}
		case 2018:
			if (GameCanvas.gameTick % 24 < 8)
			{
				fraBuff.drawFrame(GameCanvas.gameTick / numNextframe % 2, x + xlech, y + ylech, 0, 3, g);
			}
			break;
		case 2020:
			if (GameCanvas.gameTick % 24 >= 8 && GameCanvas.gameTick % 24 < 16)
			{
				fraBuff.drawFrame(2 + GameCanvas.gameTick / numNextframe % 2, x + xlech, y + ylech, 0, 3, g);
			}
			break;
		case 2019:
			if (GameCanvas.gameTick % 24 >= 16)
			{
				fraBuff.drawFrame(4 + GameCanvas.gameTick / numNextframe % 2, x + xlech, y + ylech, 0, 3, g);
			}
			break;
		case 1019:
		case 1020:
		case 1021:
		case 1022:
		case 1023:
		{
			g.setColor(color);
			for (int num5 = 0; num5 < vecEffBuff.size(); num5++)
			{
				Point point8 = (Point)vecEffBuff.elementAt(num5);
				g.fillRect(x + point8.x, y + point8.y, point8.dis, point8.dis);
			}
			break;
		}
		case 2032:
		{
			for (int num4 = 0; num4 < vecEffBuff.size(); num4++)
			{
				Point point7 = (Point)vecEffBuff.elementAt(num4);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point7.frame * fraBuff.frameHeight, fraBuff.frameWidth, 8, 0, x + point7.x, y + point7.y - 7 + 3, 33);
				}
			}
			break;
		}
		case 2028:
		{
			for (int n = 0; n < vecEffBuff.size(); n++)
			{
				Point point6 = (Point)vecEffBuff.elementAt(n);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point6.frame * fraBuff.frameHeight, fraBuff.frameWidth, 10, 0, x + point6.x, y + point6.y - 9 + 3, 33);
				}
			}
			break;
		}
		case 2042:
		{
			int num2 = GameCanvas.gameTick / numNextframe % mPlayframe.Length;
			fraBuff.drawFrame(mPlayframe[num2], x + xlech, y + ylech, 0, 33, g);
			break;
		}
		case 2035:
		case 2067:
		case 2070:
			fraBuff.drawFrame(GameCanvas.gameTick / numNextframe % fraBuff.nFrame, x + xlech, y + ylech, 0, 3, g);
			break;
		case 2053:
		{
			int num = GameCanvas.gameTick / numNextframe % mPlayframe.Length;
			mfraBuff[mPlayframe[num]].drawFrame(0, x + xlech, y + ylech, 0, 3, g);
			break;
		}
		case 2059:
		{
			for (int l = 0; l < vecEffBuff.size(); l++)
			{
				Point point4 = (Point)vecEffBuff.elementAt(l);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point4.frame * fraBuff.frameHeight, fraBuff.frameWidth, 10, 0, x + point4.x, y + point4.y - 5, 33);
				}
			}
			break;
		}
		case 2060:
		{
			if (fraBuff2 != null)
			{
				fraBuff2.drawFrame(numFrame2, obj.x, obj.y - obj.hOne, 0, 3, g);
			}
			for (int i = 0; i < vecEffBuff.size(); i++)
			{
				Point point = (Point)vecEffBuff.elementAt(i);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point.frame * fraBuff.frameHeight, fraBuff.frameWidth, 10, 0, x + point.x, y + point.y - 5, 33);
				}
			}
			break;
		}
		}
	}

	public void paintLastSpec(mGraphics g, int x, int y, MainObject obj)
	{
		switch (IdBuff)
		{
		case 2032:
		{
			for (int j = 0; j < vecEffBuff.size(); j++)
			{
				Point point2 = (Point)vecEffBuff.elementAt(j);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point2.frame * fraBuff.frameHeight + 8, fraBuff.frameWidth, 7, 0, x + point2.x, y + point2.y + 3, 33);
				}
			}
			break;
		}
		case 2028:
		{
			for (int k = 0; k < vecEffBuff.size(); k++)
			{
				Point point3 = (Point)vecEffBuff.elementAt(k);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point3.frame * fraBuff.frameHeight + 10, fraBuff.frameWidth, 9, 0, x + point3.x, y + point3.y + 3, 33);
				}
			}
			break;
		}
		case 2053:
		{
			int num = GameCanvas.gameTick / numNextframe % mPlayframe.Length;
			mfraBuff[mPlayframe[num]].drawFrame(1, x + xlech, y + ylech, 0, 3, g);
			break;
		}
		case 2059:
		{
			for (int l = 0; l < vecEffBuff.size(); l++)
			{
				Point point4 = (Point)vecEffBuff.elementAt(l);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point4.frame * fraBuff.frameHeight, fraBuff.frameWidth, 21, 0, x + point4.x, y + point4.y + 5, 33);
				}
			}
			break;
		}
		case 2060:
		{
			if (fraBuff2 != null)
			{
				fraBuff2.drawFrame(numFrame2, obj.x, obj.y - obj.hOne, 0, 3, g);
			}
			for (int i = 0; i < vecEffBuff.size(); i++)
			{
				Point point = (Point)vecEffBuff.elementAt(i);
				if (fraBuff.getImageFrame() != null)
				{
					g.drawRegion(fraBuff.getImageFrame(), 0, point.frame * fraBuff.frameHeight, fraBuff.frameWidth, 21, 0, x + point.x, y + point.y + 5, 33);
				}
			}
			break;
		}
		}
	}

	public void update()
	{
		switch (IdBuff)
		{
		case 2004:
			if (GameCanvas.gameTick % 3 == 0)
			{
				numFrame++;
				if (numFrame > fraBuff.maxNumFrame)
				{
					numFrame = 0;
				}
			}
			break;
		case 2054:
		{
			f++;
			if (GameCanvas.gameTick % 5 == 0)
			{
				createPoint();
			}
			Cx += vy;
			if (f == 6)
			{
				Point point17 = new Point(Cx, Cy);
				point17.vy = 0;
				point17.dis = 1;
				point17.frame = 0;
				point17.fRe = 10;
				vecEffBuff.addElement(point17);
			}
			if (f == 8)
			{
				vy = -25;
			}
			for (int m = 0; m < vecEffBuff.size(); m++)
			{
				Point point18 = (Point)vecEffBuff.elementAt(m);
				point18.update();
				if (point18.dis == 1)
				{
					point18.vy = vy;
				}
				if (point18.f >= point18.fRe)
				{
					vecEffBuff.removeElement(point18);
					m--;
				}
			}
			if (f < 10 && f % 3 == 2)
			{
				for (int n = 0; n < 4; n++)
				{
					Point point19 = new Point(Cx + CRes.random_Am_0(20), Cy - CRes.random(30) + 10);
					point19.vy = -2 - CRes.random(4);
					point19.dis = 0;
					point19.frame = CRes.random(fraBuff2.nFrame);
					point19.fRe = CRes.random(12, 20);
					vecEffBuff.addElement(point19);
				}
			}
			break;
		}
		case 1002:
		case 2008:
		case 2010:
		{
			for (int num = 0; num < vecEffBuff.size(); num++)
			{
				Point point20 = (Point)vecEffBuff.elementAt(num);
				point20.update();
				if (point20.f >= point20.fRe)
				{
					point20.f = 0;
					point20.x = point20.x2;
					point20.y = point20.y2;
				}
			}
			break;
		}
		case 1010:
		case 1011:
		case 1012:
		case 1013:
		case 1014:
		{
			for (int l = 0; l < vecEffBuff.size(); l++)
			{
				Point point16 = (Point)vecEffBuff.elementAt(l);
				point16.x += point16.vx;
				point16.y += point16.vy;
				if (point16.y <= -90)
				{
					if (l >= maxsize)
					{
						vecEffBuff.removeElementAt(l);
						l--;
					}
					else
					{
						createPointNewBuff(point16);
					}
				}
			}
			break;
		}
		case 2017:
			if (GameCanvas.gameTick / numNextframe % (fraBuff.nFrame + 1) == fraBuff.nFrame)
			{
				if (levelPaint != -1)
				{
					levelPaint = -1;
				}
				else
				{
					levelPaint = 0;
				}
			}
			break;
		case 1019:
		case 1020:
		case 1021:
		case 1022:
		case 1023:
		{
			for (int j = 0; j < vecEffBuff.size(); j++)
			{
				Point point8 = (Point)vecEffBuff.elementAt(j);
				point8.x += point8.vx;
				point8.y += point8.vy;
				if (point8.y <= -70)
				{
					if (j >= maxsize)
					{
						vecEffBuff.removeElementAt(j);
						j--;
					}
					else
					{
						point8.x = CRes.random_Am_0(15);
						point8.y = 10 - CRes.random(40);
						point8.dis = 1 + CRes.random(3);
					}
				}
			}
			break;
		}
		case 2028:
		case 2032:
		{
			f++;
			for (int num2 = 0; num2 < vecEffBuff.size(); num2++)
			{
				Point point21 = (Point)vecEffBuff.elementAt(num2);
				if (point21.dis == 1)
				{
					point21.f++;
					if (point21.f >= point21.fRe)
					{
						vecEffBuff.removeElement(point21);
						num2--;
					}
				}
			}
			if (f % 12 == 0)
			{
				Point point22 = new Point();
				point22.y = 4;
				point22.frame = 0;
				point22.dis = 1;
				point22.fRe = 8;
				vecEffBuff.addElement(point22);
			}
			else if (f % 12 == 2)
			{
				Point point23 = new Point();
				point23.y = -2;
				point23.frame = 0;
				point23.dis = 1;
				point23.fRe = 7;
				vecEffBuff.addElement(point23);
			}
			else if (f % 12 == 4)
			{
				Point point24 = new Point();
				point24.y = -8;
				point24.frame = 1;
				point24.dis = 1;
				point24.fRe = 6;
				vecEffBuff.addElement(point24);
			}
			else if (f % 12 == 6)
			{
				Point point25 = new Point();
				point25.y = -14;
				point25.frame = 2;
				point25.dis = 1;
				point25.fRe = 5;
				vecEffBuff.addElement(point25);
			}
			else if (f % 12 == 8)
			{
				Point point26 = new Point();
				point26.y = -20;
				point26.frame = 3;
				point26.dis = 1;
				point26.fRe = 4;
				vecEffBuff.addElement(point26);
			}
			else if (f % 12 == 10)
			{
				Point point27 = new Point();
				point27.y = -26;
				point27.frame = 3;
				point27.dis = 1;
				point27.fRe = 3;
				vecEffBuff.addElement(point27);
			}
			break;
		}
		case 2059:
		{
			f++;
			for (int k = 0; k < vecEffBuff.size(); k++)
			{
				Point point9 = (Point)vecEffBuff.elementAt(k);
				if (point9.dis == 1)
				{
					point9.f++;
					if (point9.f >= point9.fRe)
					{
						vecEffBuff.removeElement(point9);
						k--;
					}
				}
			}
			if (f % 12 == 0)
			{
				Point point10 = new Point();
				point10.y = 4;
				point10.frame = 0;
				point10.dis = 1;
				point10.fRe = 8;
				vecEffBuff.addElement(point10);
			}
			else if (f % 12 == 2)
			{
				Point point11 = new Point();
				point11.y = 4;
				point11.frame = 0;
				point11.dis = 1;
				point11.fRe = 7;
				vecEffBuff.addElement(point11);
			}
			else if (f % 12 == 4)
			{
				Point point12 = new Point();
				point12.y = 4;
				point12.frame = 1;
				point12.dis = 1;
				point12.fRe = 6;
				vecEffBuff.addElement(point12);
			}
			else if (f % 12 == 6)
			{
				Point point13 = new Point();
				point13.y = 4;
				point13.frame = 2;
				point13.dis = 1;
				point13.fRe = 5;
				vecEffBuff.addElement(point13);
			}
			else if (f % 12 == 8)
			{
				Point point14 = new Point();
				point14.y = 4;
				point14.frame = 3;
				point14.dis = 1;
				point14.fRe = 4;
				vecEffBuff.addElement(point14);
			}
			else if (f % 12 == 10)
			{
				Point point15 = new Point();
				point15.y = 4;
				point15.frame = 3;
				point15.dis = 1;
				point15.fRe = 3;
				vecEffBuff.addElement(point15);
			}
			break;
		}
		case 2060:
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				numFrame2++;
				if (numFrame2 > 2)
				{
					numFrame2 = 0;
				}
			}
			f++;
			for (int i = 0; i < vecEffBuff.size(); i++)
			{
				Point point = (Point)vecEffBuff.elementAt(i);
				if (point.dis == 1)
				{
					point.f++;
					if (point.f >= point.fRe)
					{
						vecEffBuff.removeElement(point);
						i--;
					}
				}
			}
			if (f % 12 == 0)
			{
				Point point2 = new Point();
				point2.y = 4;
				point2.frame = 0;
				point2.dis = 1;
				point2.fRe = 8;
				vecEffBuff.addElement(point2);
			}
			else if (f % 12 == 2)
			{
				Point point3 = new Point();
				point3.y = 4;
				point3.frame = 0;
				point3.dis = 1;
				point3.fRe = 7;
				vecEffBuff.addElement(point3);
			}
			else if (f % 12 == 4)
			{
				Point point4 = new Point();
				point4.y = 4;
				point4.frame = 1;
				point4.dis = 1;
				point4.fRe = 6;
				vecEffBuff.addElement(point4);
			}
			else if (f % 12 == 6)
			{
				Point point5 = new Point();
				point5.y = 4;
				point5.frame = 2;
				point5.dis = 1;
				point5.fRe = 5;
				vecEffBuff.addElement(point5);
			}
			else if (f % 12 == 8)
			{
				Point point6 = new Point();
				point6.y = 4;
				point6.frame = 3;
				point6.dis = 1;
				point6.fRe = 4;
				vecEffBuff.addElement(point6);
			}
			else if (f % 12 == 10)
			{
				Point point7 = new Point();
				point7.y = 4;
				point7.frame = 3;
				point7.dis = 1;
				point7.fRe = 3;
				vecEffBuff.addElement(point7);
			}
			break;
		}
		}
		if (GameCanvas.timeNow - timeBegin > timeBuff)
		{
			isRemove = true;
		}
	}

	public Point createPointNewBuff(Point p)
	{
		p.frame = CRes.random(5);
		p.x = CRes.random_Am_0(15);
		p.y = 10 - CRes.random(40);
		if (IdBuff == 1010)
		{
			p.dis = 0;
		}
		else if (IdBuff == 1011)
		{
			p.dis = 1;
		}
		else if (IdBuff == 1012)
		{
			p.dis = 3;
		}
		else if (IdBuff == 1013)
		{
			p.dis = 2;
		}
		else if (IdBuff == 1014)
		{
			p.dis = 4;
		}
		return p;
	}

	public void paintHotKey(mGraphics g)
	{
	}
}
