public class Effect_Map
{
	private mVector vecEff = new mVector();

	private mVector vecEffSub = new mVector();

	public sbyte type;

	public sbyte level;

	public sbyte hRain;

	private int swingXSpeed;

	private int YSpped;

	private int timeSpeed;

	private int time;

	private int numNextFrame;

	public const sbyte GREEN_LEAF = 0;

	public const sbyte SNOW = 1;

	public const sbyte FLOWER = 2;

	public const sbyte RAIN = 3;

	public const sbyte SEA = 4;

	public const sbyte MAI = 5;

	public const sbyte PHAO = 6;

	public const sbyte DAO = 7;

	public const sbyte RAIN_THUNDER = 8;

	public const sbyte DARK = 9;

	public static sbyte[][] frameLeaf = new sbyte[2][]
	{
		new sbyte[20]
		{
			0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
			2, 2, 2, 2, 2, 1, 1, 1, 1, 1
		},
		new sbyte[16]
		{
			0, 0, 1, 1, 2, 2, 3, 3, 4, 4,
			5, 5, 6, 6, 7, 7
		}
	};

	private FrameImage fraImg;

	private FrameImage fraImgSub;

	private int Plus;

	public Effect_Map(sbyte type, sbyte level)
	{
		if (type == 4)
		{
			if (LoadMapScreen.isMapSky == 1)
			{
				fraImg = new FrameImage(mImage.createImage("/bg/sea1fly.png"), 24, 24);
				fraImgSub = new FrameImage(mImage.createImage("/bg/sea2fly.png"), 24, 24);
			}
			else
			{
				fraImg = new FrameImage(mImage.createImage("/bg/sea1.png"), 24, 24);
				fraImgSub = new FrameImage(mImage.createImage("/bg/sea2.png"), 24, 24);
			}
			numNextFrame = 3;
			return;
		}
		this.type = type;
		vecEff.removeAllElements();
		int num = 8000;
		hRain = 5;
		if (level >= 3)
		{
			hRain = 6;
		}
		switch (type)
		{
		case 1:
		case 6:
			num = 3000;
			break;
		case 3:
			num = 2000;
			break;
		case 8:
			num = 1500;
			break;
		}
		int num2 = MotherCanvas.w * MotherCanvas.h / num;
		if (level == 0)
		{
			num2 /= 4;
		}
		if (level == 1)
		{
			num2 /= 2;
		}
		if (level == 3)
		{
			num2 = num2 * 3 / 2;
		}
		if (level == 4)
		{
			num2 *= 2;
		}
		if (num2 <= 0)
		{
			num2 = 1;
		}
		randomSpeed();
		switch (type)
		{
		case 0:
		{
			Plus = 30;
			fraImg = new FrameImage(mImage.createImage("/bg/leaf.png"), 11, 11);
			for (int j = 0; j < num2; j++)
			{
				Point point2 = new Point
				{
					x = MainScreen.cameraMain.xCam + Plus + CRes.random(MotherCanvas.w),
					y = MainScreen.cameraMain.yCam - Plus + CRes.random(MotherCanvas.h),
					vx = swingXSpeed,
					vy = YSpped
				};
				setCreate(point2);
				vecEff.addElement(point2);
			}
			break;
		}
		case 1:
		{
			Plus = 30;
			fraImg = new FrameImage(mImage.createImage("/bg/snow.png"), 7, 7);
			for (int l = 0; l < num2; l++)
			{
				Point point4 = new Point
				{
					x = MainScreen.cameraMain.xCam + Plus + CRes.random(MotherCanvas.w),
					y = MainScreen.cameraMain.yCam - Plus + CRes.random(MotherCanvas.h),
					vx = swingXSpeed,
					maxframe = fraImg.nFrame
				};
				setCreate(point4);
				vecEff.addElement(point4);
			}
			break;
		}
		case 2:
		case 5:
		case 6:
		case 7:
		{
			Plus = 30;
			switch (type)
			{
			case 2:
				fraImg = new FrameImage(mImage.createImage("/bg/flower.png"), 9, 10);
				break;
			case 5:
				fraImg = new FrameImage(mImage.createImage("/bg/flowermai.png"), 9, 10);
				break;
			case 7:
				fraImg = new FrameImage(mImage.createImage("/bg/flowerdao.png"), 9, 10);
				break;
			default:
				fraImg = new FrameImage(mImage.createImage("/bg/flowerphao.png"), 7, 7);
				break;
			}
			for (int k = 0; k < num2; k++)
			{
				Point point3 = new Point
				{
					x = MainScreen.cameraMain.xCam + Plus + CRes.random(MotherCanvas.w),
					y = MainScreen.cameraMain.yCam - Plus + CRes.random(MotherCanvas.h),
					vx = swingXSpeed,
					vy = YSpped,
					maxframe = fraImg.nFrame
				};
				setCreate(point3);
				vecEff.addElement(point3);
			}
			break;
		}
		case 3:
		case 8:
		{
			Plus = 10;
			fraImg = new FrameImage(mImage.createImage("/bg/rain.png"), 12, 7);
			for (int i = 0; i < num2; i++)
			{
				Point point = new Point
				{
					x = MainScreen.cameraMain.xCam + Plus + CRes.random(MotherCanvas.w),
					y = MainScreen.cameraMain.yCam - Plus + CRes.random(MotherCanvas.h),
					vx = swingXSpeed
				};
				setCreate(point);
				vecEff.addElement(point);
			}
			break;
		}
		case 4:
			break;
		}
	}

	public void paintLast(mGraphics g)
	{
		switch (type)
		{
		case 0:
		{
			for (int k = 0; k < vecEff.size(); k++)
			{
				Point point3 = (Point)vecEff.elementAt(k);
				fraImg.drawFrame(frameLeaf[point3.dis][(point3.f + point3.frame) % point3.maxframe], point3.x, point3.y, 0, 3, g);
			}
			break;
		}
		case 1:
		case 2:
		case 5:
		case 6:
		case 7:
		{
			for (int l = 0; l < vecEff.size(); l++)
			{
				Point point4 = (Point)vecEff.elementAt(l);
				fraImg.drawFrame(point4.frame, point4.x, point4.y, 0, 3, g);
			}
			break;
		}
		case 3:
		case 8:
		{
			g.setColor(16777215);
			for (int i = 0; i < vecEff.size(); i++)
			{
				Point point = (Point)vecEff.elementAt(i);
				g.drawLine(point.x, point.y, point.x - 1, point.y + point.h);
			}
			for (int j = 0; j < vecEffSub.size(); j++)
			{
				Point point2 = (Point)vecEffSub.elementAt(j);
				if (!point2.isRemove)
				{
					fraImg.drawFrame(point2.dis + point2.f / 3 % fraImg.nFrame, point2.x, point2.y, 0, 3, g);
				}
			}
			break;
		}
		case 4:
			break;
		}
	}

	public void createSea(int x, int y)
	{
		Point point = new Point(x, y);
		point.frame = CRes.random(2);
		int num = CRes.random(20);
		point.dis = ((num >= 19) ? 2 : 0);
		point.fRe = 7 * numNextFrame;
		vecEff.addElement(point);
	}

	public void paintSea(mGraphics g)
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			Point point = (Point)vecEff.elementAt(i);
			if (point.frame == 0)
			{
				fraImg.drawFrame(point.f / numNextFrame % fraImg.nFrame, point.x, point.y, point.dis, 0, g);
			}
			else
			{
				fraImgSub.drawFrame(point.f / numNextFrame % fraImgSub.nFrame, point.x, point.y, point.dis, 0, g);
			}
		}
	}

	public void updateSea()
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			Point point = (Point)vecEff.elementAt(i);
			point.f++;
			if (point.f >= point.fRe)
			{
				vecEff.removeElement(point);
				i--;
			}
		}
		if (GameCanvas.gameTick % 5 != 0)
		{
			return;
		}
		int num = MainScreen.cameraMain.xCam / LoadMap.wTile - 1;
		int num2 = MainScreen.cameraMain.yCam / LoadMap.wTile - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		int num3 = num + GameCanvas.loadmap.maxX + 2;
		int num4 = num2 + GameCanvas.loadmap.maxY + 2;
		if (num3 > GameCanvas.loadmap.mapW)
		{
			num3 = GameCanvas.loadmap.mapW;
		}
		if (num4 > GameCanvas.loadmap.mapH)
		{
			num4 = GameCanvas.loadmap.mapH;
		}
		for (int j = num; j < num3; j++)
		{
			for (int k = num2; k < num4; k++)
			{
				if (GameCanvas.loadmap.mapType[k * GameCanvas.loadmap.mapW + j] == 2 && CRes.random(20) == 0)
				{
					createSea(j * LoadMap.wTile, k * LoadMap.wTile);
				}
			}
		}
	}

	public void randomSpeed()
	{
		time = 0;
		timeSpeed = CRes.random(30, 100);
		switch (type)
		{
		case 0:
		case 2:
		case 5:
		case 6:
		case 7:
			YSpped = 1;
			swingXSpeed = -CRes.random(2, 5);
			break;
		case 1:
			swingXSpeed = -CRes.random(3, 6);
			break;
		case 3:
			swingXSpeed = -CRes.random(2, 4);
			break;
		case 8:
			swingXSpeed = -CRes.random(3, 5);
			break;
		case 4:
			break;
		}
	}

	public void update()
	{
		time++;
		bool flag = false;
		if (time > timeSpeed)
		{
			randomSpeed();
			flag = true;
		}
		setPos();
		switch (type)
		{
		case 0:
		{
			for (int l = 0; l < vecEff.size(); l++)
			{
				Point point5 = (Point)vecEff.elementAt(l);
				if (flag)
				{
					point5.vx = swingXSpeed;
					point5.vy = YSpped;
				}
				point5.update();
				if (point5.dis == 0 && CRes.random(80) == 0)
				{
					point5.dis = 1;
					point5.maxframe = frameLeaf[point5.dis].Length;
				}
				else if (point5.dis == 1 && point5.f == 7 && CRes.random(3) == 0)
				{
					point5.dis = 0;
					point5.maxframe = frameLeaf[point5.dis].Length;
				}
			}
			break;
		}
		case 1:
		{
			for (int m = 0; m < vecEff.size(); m++)
			{
				Point point6 = (Point)vecEff.elementAt(m);
				if (flag)
				{
					point6.vx = swingXSpeed;
					if (point6.vy == 1 && CRes.random(30) == 0)
					{
						point6.vy = 0;
					}
					else if (CRes.random(10) == 0 && point6.vy != 1)
					{
						point6.vy = 1;
					}
				}
				point6.update();
			}
			break;
		}
		case 2:
		case 5:
		case 6:
		case 7:
		{
			for (int k = 0; k < vecEff.size(); k++)
			{
				Point point4 = (Point)vecEff.elementAt(k);
				if (flag)
				{
					point4.vx = swingXSpeed;
					point4.vy = YSpped;
				}
				if (CRes.random(5) == 0)
				{
					point4.frame = CRes.random(point4.maxframe);
				}
				point4.update();
			}
			break;
		}
		case 3:
		case 8:
		{
			for (int i = 0; i < vecEff.size(); i++)
			{
				Point point = (Point)vecEff.elementAt(i);
				if (flag)
				{
					point.vx = swingXSpeed;
				}
				point.update();
				if (CRes.random(40) != 0)
				{
					continue;
				}
				int num = 200;
				int num2 = 1;
				if (GameCanvas.mapBack != null)
				{
					num = GameCanvas.mapBack.hBack;
					if (GameCanvas.loadmap.maxHMap > num)
					{
						num2 = CRes.random(GameCanvas.loadmap.maxHMap - num);
					}
				}
				Point point2 = new Point();
				point2.x = MainScreen.cameraMain.xCam + CRes.random(MotherCanvas.w);
				point2.y = num + num2;
				point2.frame = 1;
				point2.dis = ((CRes.random(4) != 0) ? 1 : 0) * 2;
				vecEffSub.addElement(point2);
				point = resetPos(point);
			}
			for (int j = 0; j < vecEffSub.size(); j++)
			{
				Point point3 = (Point)vecEffSub.elementAt(j);
				point3.f++;
				if (point3.f >= 6)
				{
					if (point3.isRemove)
					{
						vecEffSub.removeElement(point3);
						j--;
					}
					else
					{
						point3.isRemove = true;
					}
				}
			}
			break;
		}
		case 4:
			break;
		}
	}

	public void setPos()
	{
		switch (type)
		{
		case 0:
		case 1:
		case 2:
		case 5:
		case 6:
		case 7:
		{
			for (int j = 0; j < vecEff.size(); j++)
			{
				Point point2 = (Point)vecEff.elementAt(j);
				if (point2.x < MainScreen.cameraMain.xCam - Plus || point2.y > MainScreen.cameraMain.yCam + MotherCanvas.h + Plus)
				{
					point2 = resetPos(point2);
				}
				if (point2.x > MainScreen.cameraMain.xCam + MotherCanvas.w + Plus * 2)
				{
					point2.x -= MotherCanvas.w + Plus;
				}
			}
			break;
		}
		case 3:
		case 8:
		{
			for (int i = 0; i < vecEff.size(); i++)
			{
				Point point = (Point)vecEff.elementAt(i);
				if (point.frame == 0)
				{
					if (point.x < MainScreen.cameraMain.xCam - Plus || point.y > MainScreen.cameraMain.yCam + MotherCanvas.h + Plus)
					{
						point = resetPos(point);
					}
					if (point.x > MainScreen.cameraMain.xCam + MotherCanvas.w + Plus * 2)
					{
						point.x -= MotherCanvas.w + Plus;
					}
				}
			}
			break;
		}
		case 4:
			break;
		}
	}

	public Point resetPos(Point p)
	{
		switch (type)
		{
		case 0:
		case 1:
		case 2:
		case 5:
		case 6:
		case 7:
			if (CRes.random(3) != 0)
			{
				p.x = MainScreen.cameraMain.xCam + MotherCanvas.w + CRes.random(Plus);
				p.y = MainScreen.cameraMain.yCam - Plus + CRes.random(MotherCanvas.h);
			}
			else
			{
				p.x = MainScreen.cameraMain.xCam + Plus + CRes.random(MotherCanvas.w);
				p.y = MainScreen.cameraMain.yCam - CRes.random(Plus);
			}
			setCreate(p);
			break;
		case 3:
		case 8:
			if (CRes.random(3) == 0)
			{
				p.x = MainScreen.cameraMain.xCam + MotherCanvas.w + CRes.random(Plus);
				p.y = MainScreen.cameraMain.yCam - Plus + CRes.random(MotherCanvas.h);
			}
			else
			{
				p.x = MainScreen.cameraMain.xCam + Plus + CRes.random(MotherCanvas.w);
				p.y = MainScreen.cameraMain.yCam - CRes.random(Plus);
			}
			setCreate(p);
			break;
		}
		return p;
	}

	public Point setCreate(Point p)
	{
		switch (type)
		{
		case 0:
			p.levelPaint = CRes.random(10);
			p.dis = ((CRes.random(15) == 1) ? 1 : 0);
			p.maxframe = frameLeaf[p.dis].Length;
			p.frame = CRes.random(p.maxframe);
			break;
		case 1:
			p.levelPaint = CRes.random(10);
			p.frame = CRes.random(p.maxframe);
			p.vy = ((CRes.random(10) > 0) ? 1 : 0);
			break;
		case 2:
		case 5:
		case 6:
		case 7:
			p.levelPaint = CRes.random(10);
			p.frame = CRes.random(p.maxframe);
			break;
		case 3:
		case 8:
			p.levelPaint = CRes.random(10);
			p.h = CRes.random(1, hRain);
			p.vy = hRain;
			p.frame = 0;
			break;
		}
		return p;
	}
}
