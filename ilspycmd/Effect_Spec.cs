public class Effect_Spec : MainEffect
{
	public const sbyte CHOANG = 1;

	public const sbyte CHAY_MAU = 2;

	public const sbyte GIAM_TAN_CONG = 3;

	public const sbyte GIAM_PHONG_THU = 4;

	public const sbyte HOA_MAT = 5;

	public const sbyte DIEN_GIAT = 6;

	public const sbyte LUA_CHAY = 7;

	public const sbyte TROI_CHAN = 8;

	public const sbyte HUT_MANA = 9;

	public const sbyte TRUNG_DOC = 10;

	public const sbyte EFF_BAT_TU = 11;

	public const sbyte EFF_CRITICAL_LT = 12;

	public const sbyte LUA_PHAN_DAU = -1;

	public const sbyte LUA_TOAN_THAN = -2;

	public const sbyte DEC_DAMAGE = 15;

	public const sbyte DEC_POWER = 18;

	private int dy;

	private int dx;

	private short timeEff;

	private mVector vecEff = new mVector();

	private string[] mtest = new string[11]
	{
		"0 co gi", "choang", "Chay Mau", "Giam tan cong", "giam phong thu", "Hoa Mat", "Điện giật", "Lửa cháy", "Trói chân", "hut mana",
		"Trúng độc"
	};

	private int[][] mPlayFrameVip;

	private int[] mPlayframe;

	private Point pMain;

	private int tick;

	public Effect_Spec(MainObject objMain, short type, short time)
	{
		objFireMain = objMain;
		typeEffect = type;
		timeBegin = GameCanvas.timeNow;
		timeEff = time;
		mSystem.println("add eff " + typeEffect);
		switch (typeEffect)
		{
		case -1:
			fraImgEff = new FrameImage(155, 21, 28);
			dy = -objMain.hOne + 12;
			levelPaint = -1;
			break;
		case -2:
			fraImgEff = new FrameImage(156, 26, 50);
			dy = -objMain.hOne + 20;
			levelPaint = -1;
			break;
		case 5:
		{
			fraImgEff = new FrameImage(157, 10, 10);
			dy = -objMain.hOne + 3;
			for (int k = 0; k < 2; k++)
			{
				Point point = new Point
				{
					x = -5
				};
				if (k == 1)
				{
					point.x = 5;
				}
				point.y = dy;
				point.frame = k * 2;
				vecEff.addElement(point);
			}
			break;
		}
		case 1:
			objFireMain.DirSpec = objFireMain.Dir;
			fraImgEff = new FrameImage(158, 25, 12);
			dy = -objMain.hOne + 3;
			break;
		case 15:
			fraImgEff = new FrameImage(398, 19, 19);
			dy = -objMain.hOne + 1;
			break;
		case 18:
			fraImgEff = new FrameImage(400, 3);
			dy = -objMain.hOne;
			break;
		case 2:
			fraImgEff = new FrameImage(162, 10, 20);
			dy = -objMain.hOne + 35;
			dx = CRes.random_Am_0(8);
			vy = 2;
			break;
		case 3:
			createGiamTanCong();
			break;
		case 4:
			createGiamPhongThu();
			break;
		case 7:
		{
			fraImgEff = new FrameImage(160, 9, 14);
			dy = -objMain.hOne / 2;
			int num = CRes.random(2, 5);
			for (int j = 0; j < num; j++)
			{
				Point o2 = new Point(CRes.random_Am_0(10), dy + CRes.random_Am_0(15))
				{
					fRe = CRes.random(6, 10),
					frame = CRes.random(3) * 3,
					dis = CRes.random(3)
				};
				vecEff.addElement(o2);
			}
			break;
		}
		case 9:
			fraImgEff = new FrameImage(163, 28, 31);
			break;
		case 10:
			fraImgEff = new FrameImage(164, 19, 19);
			dy = -objMain.hOne;
			break;
		case 8:
			fraImgEff = new FrameImage(170, 32, 24);
			levelPaint = -1;
			break;
		case 6:
		{
			fraImgEff = new FrameImage(122, 16, 12);
			for (int i = 0; i < 3; i++)
			{
				Point o = new Point(objFireMain.x + CRes.random_Am_0(20), objFireMain.y - CRes.random(objFireMain.hOne + 10))
				{
					frame = CRes.random(3)
				};
				vecEff.addElement(o);
			}
			break;
		}
		case 11:
			fraImgEff = new FrameImage(350, 25, 44);
			mPlayFrameVip = new int[2][]
			{
				new int[12]
				{
					0, 0, 2, 2, 6, 6, 12, 12, -1, -1,
					-1, -1
				},
				new int[12]
				{
					0, 0, 1, 1, 1, 1, 2, 2, 0, 0,
					0, 0
				}
			};
			break;
		case 12:
			pMain = new Point(0, 0);
			pMain.vx = -4;
			pMain.vy = -15;
			fraImgEff = new FrameImage(220, 9, 9, 4);
			mPlayframe = new int[4] { 4, 7, 5, 7 };
			break;
		case 0:
		case 13:
		case 14:
		case 16:
		case 17:
			break;
		}
	}

	private void createGiamTanCong()
	{
		fraImgEff = new FrameImage(159, 45, 12);
		dy = -objFireMain.hOne + 5;
		Point point = new Point(0, dy);
		point.vy = 2;
		vecEff.addElement(point);
	}

	private void createGiamPhongThu()
	{
		fraImgEff = new FrameImage(161, 35, 13);
		dy = -objFireMain.hOne + 10;
		Point point = new Point(0, dy);
		point.vy = 2;
		vecEff.addElement(point);
	}

	public void paintFrist(mGraphics g)
	{
		switch (typeEffect)
		{
		case 3:
		case 4:
		{
			for (int k = 0; k < vecEff.size(); k++)
			{
				Point point3 = (Point)vecEff.elementAt(k);
				fraImgEff.drawFrame(GameCanvas.gameTick / 2 % fraImgEff.nFrame / 2 * 2, objFireMain.x + point3.x, objFireMain.y + point3.y - fraImgEff.frameHeight / 2, 0, 3, g);
			}
			break;
		}
		case 7:
		{
			for (int j = 0; j < vecEff.size(); j++)
			{
				Point point2 = (Point)vecEff.elementAt(j);
				if (point2.dis == 0)
				{
					fraImgEff.drawFrame(point2.frame + point2.f / 2 % 3, objFireMain.x + point2.x, objFireMain.y + point2.y, 0, 3, g);
				}
			}
			break;
		}
		case 8:
			fraImgEff.drawFrame(GameCanvas.gameTickChia4 % fraImgEff.nFrame, objFireMain.x, objFireMain.y + dy, 0, 3, g);
			break;
		case 12:
			if (pMain != null && pMain.vx > 0)
			{
				for (int i = 0; i < vecEff.size(); i++)
				{
					Point point = (Point)vecEff.elementAt(i);
					fraImgEff.drawFrameNew(mPlayframe[point.f / 2], objFireMain.x + point.x, objFireMain.y + point.y / 10, 0, 3, g);
				}
			}
			break;
		case 5:
		case 6:
		case 9:
		case 10:
		case 11:
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		switch (typeEffect)
		{
		case -1:
			fraImgEff.drawFrame(GameCanvas.gameTick / 3 % fraImgEff.nFrame, objFireMain.x, objFireMain.y + dy - GameCanvas.gameTick / 3 % 3, objFireMain.Dir, 3, g);
			break;
		case -2:
			fraImgEff.drawFrame(GameCanvas.gameTick / 3 % fraImgEff.nFrame, objFireMain.x, objFireMain.y + dy - GameCanvas.gameTick / 3 % 3 * 2, objFireMain.Dir, 3, g);
			break;
		case 5:
		{
			for (int l = 0; l < vecEff.size(); l++)
			{
				Point point4 = (Point)vecEff.elementAt(l);
				fraImgEff.drawFrame((GameCanvas.gameTick / 6 + point4.frame) % fraImgEff.nFrame, objFireMain.x + point4.x, objFireMain.y + point4.y, 0, 3, g);
			}
			break;
		}
		case 1:
		case 10:
			fraImgEff.drawFrame(GameCanvas.gameTickChia4 % fraImgEff.nFrame, objFireMain.x, objFireMain.y + dy, 0, 3, g);
			break;
		case 2:
			if (tick <= fRemove)
			{
				int num3 = tick / 2;
				if (num3 > 2)
				{
					num3 = 2;
				}
				fraImgEff.drawFrame(num3, objFireMain.x + dx, objFireMain.y + dy, 0, 3, g);
			}
			break;
		case 7:
		{
			for (int k = 0; k < vecEff.size(); k++)
			{
				Point point3 = (Point)vecEff.elementAt(k);
				if (point3.dis != 0)
				{
					fraImgEff.drawFrame(point3.frame + point3.f / 2 % 3, objFireMain.x + point3.x, objFireMain.y + point3.y, 0, 3, g);
				}
			}
			break;
		}
		case 9:
			fraImgEff.drawFrame(GameCanvas.gameTickChia4 % fraImgEff.nFrame, objFireMain.x, objFireMain.y, 0, 33, g);
			break;
		case 3:
		case 4:
		{
			for (int j = 0; j < vecEff.size(); j++)
			{
				Point point2 = (Point)vecEff.elementAt(j);
				fraImgEff.drawFrame(GameCanvas.gameTick / 2 % fraImgEff.nFrame / 2 * 2 + 1, objFireMain.x + point2.x, objFireMain.y + point2.y + fraImgEff.frameHeight / 2, 0, 3, g);
			}
			break;
		}
		case 6:
			paintDien_Giat(g);
			break;
		case 11:
		{
			int num = f % mPlayFrameVip[0].Length;
			if (mPlayFrameVip[0][num] >= 0)
			{
				int num2 = mPlayFrameVip[0][num];
				if (objFireMain.type_left_right == 0)
				{
					num2 = -num2;
				}
				fraImgEff.drawFrame(mPlayFrameVip[1][num], objFireMain.x - num2, objFireMain.y, (objFireMain.type_left_right == 0) ? 2 : 0, 33, g);
				fraImgEff.drawFrame(mPlayFrameVip[1][num], objFireMain.x + num2, objFireMain.y, objFireMain.type_left_right, 33, g);
			}
			break;
		}
		case 12:
			if (pMain != null && pMain.vx < 0)
			{
				for (int i = 0; i < vecEff.size(); i++)
				{
					Point point = (Point)vecEff.elementAt(i);
					fraImgEff.drawFrameNew(mPlayframe[point.f / 2], objFireMain.x + point.x, objFireMain.y + point.y / 10, 0, 3, g);
				}
			}
			break;
		case 15:
			fraImgEff.drawFrame(GameCanvas.gameTickChia4 % fraImgEff.nFrame, objFireMain.x + dx, objFireMain.y + dy, 0, 3, g);
			break;
		case 18:
			fraImgEff.drawFrame(GameCanvas.gameTick / 2 % fraImgEff.nFrame, objFireMain.x + dx, objFireMain.y + dy, 0, 3, g);
			break;
		case 0:
		case 8:
		case 13:
		case 14:
		case 16:
		case 17:
			break;
		}
	}

	private void paintDien_Giat(mGraphics g)
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			Point point = (Point)vecEff.elementAt(i);
			fraImgEff.drawFrame(point.frame * 2 + point.f / 2, objFireMain.x + point.x, objFireMain.y - point.y, point.dis, 3, g);
		}
	}

	public override void update()
	{
		if ((GameCanvas.timeNow - timeBegin) / 100 >= timeEff)
		{
			isRemove = true;
			return;
		}
		switch (typeEffect)
		{
		case -2:
		case -1:
		case 1:
		case 5:
		case 9:
		case 10:
		case 15:
		case 18:
			break;
		case 2:
			tick++;
			if (tick >= fRemove && (CRes.random(20) == 0 || tick > fRemove + 15))
			{
				dx = CRes.random_Am_0(8);
				dy = -objFireMain.hOne + 35;
				fRemove = CRes.random(8, 14);
				tick = 0;
			}
			if (tick >= 6)
			{
				dy += vy;
			}
			break;
		case 7:
		{
			if (CRes.random(3) == 0 && vecEff.size() < 4)
			{
				int num = CRes.random(1, 4);
				for (int m = 0; m < num; m++)
				{
					Point point7 = new Point(CRes.random_Am_0(10), dy + CRes.random_Am_0(15));
					point7.fRe = CRes.random(6, 10);
					point7.frame = CRes.random(3) * 3;
					point7.vy = -CRes.random(2);
					point7.dis = CRes.random(3);
					vecEff.addElement(point7);
				}
			}
			for (int n = 0; n < vecEff.size(); n++)
			{
				Point point8 = (Point)vecEff.elementAt(n);
				point8.update();
				if (point8.f > point8.fRe)
				{
					vecEff.removeElement(point8);
					n--;
				}
			}
			break;
		}
		case 3:
		case 4:
		{
			for (int i = 0; i < vecEff.size(); i++)
			{
				Point point = (Point)vecEff.elementAt(i);
				point.update();
				if (point.y < 0)
				{
					continue;
				}
				if (point.frame < 5)
				{
					point.y = 0;
					point.frame++;
					if (point.frame == 2)
					{
						Point point2 = new Point(0, dy);
						point2.vy = 2;
						vecEff.addElement(point2);
					}
				}
				else
				{
					vecEff.removeElement(point);
					i--;
				}
			}
			break;
		}
		case 6:
		{
			if (CRes.random(3) != 0)
			{
				for (int k = 0; k < 2; k++)
				{
					Point point5 = new Point(CRes.random_Am_0(15), CRes.random(objFireMain.hOne + 1));
					point5.frame = CRes.random(3);
					point5.dis = CRes.random(8);
					vecEff.addElement(point5);
				}
			}
			for (int l = 0; l < vecEff.size(); l++)
			{
				Point point6 = (Point)vecEff.elementAt(l);
				point6.f++;
				if (point6.f >= 4)
				{
					vecEff.removeElement(point6);
					l--;
				}
			}
			break;
		}
		case 11:
			f++;
			break;
		case 12:
		{
			for (int j = 0; j < vecEff.size(); j++)
			{
				Point point3 = (Point)vecEff.elementAt(j);
				point3.update();
				if (point3.f >= 8)
				{
					vecEff.removeElement(point3);
					j--;
				}
			}
			pMain.update();
			if (pMain.x < -16)
			{
				pMain.vx = 4;
			}
			if (pMain.x > 16)
			{
				pMain.vx = -4;
			}
			if (pMain.y < -500)
			{
				pMain.y = 0;
			}
			Point point4 = new Point(pMain.x, pMain.y);
			point4.vy = CRes.random(30);
			point4.f = 1;
			vecEff.addElement(point4);
			break;
		}
		default:
			if (GameCanvas.gameTick % 20 == 0)
			{
				GameScreen.addEffectNum(mtest[typeEffect], objFireMain.x, objFireMain.y - objFireMain.hOne, 5);
			}
			break;
		}
	}

	public short getTimeEff()
	{
		return (short)(timeEff - (GameCanvas.timeNow - timeBegin));
	}

	public void SetXlech(int xlech)
	{
		dx = xlech;
	}
}
