public class MainEffect
{
	public int typeEffect;

	public int valueEffect;

	public int fRemove;

	public int x;

	public int y;

	public int toX;

	public int toY;

	public int f;

	public int frame;

	public int ysai;

	public int vMax;

	public int vx;

	public int vy;

	public int hOne;

	public int timeAddNum;

	public int x1000;

	public int y1000;

	public int step;

	public int am_duong;

	public int CFrame;

	public int fImg;

	public int fSub;

	public int fSub2;

	public int fSub3;

	public int indexImg;

	public int indexSub;

	public int indexSub2;

	public int indexSub3;

	public sbyte frameSuper;

	public int xplus;

	public int yplus;

	public short timeEnd;

	public sbyte Dir;

	public int levelPaint;

	public int indexEff_1;

	public long timeBegin;

	public int[] mframe;

	public bool isRemove;

	public bool isStop;

	public bool isAddHP;

	public FrameImage fraImgEff;

	public FrameImage fraImgSubEff;

	public FrameImage fraImgSub2Eff;

	public FrameImage fraImgSub3Eff;

	public FrameImage fraImgSub4Eff;

	public bool isPauseImgEff;

	public bool isPauseSubEff;

	public bool isPauseSub2Eff;

	public bool isPauseSub3Eff;

	public long timeStartPauseImgEff;

	public long timeStartPauseSubEff;

	public long timeStartPauseSub2Eff;

	public long timeStartPauseSub3Eff;

	public const sbyte EFFECT_SKILL = 0;

	public const sbyte EFFECT_NUM = 1;

	public const sbyte EFFECT_OUT = 2;

	public MainObject objFireMain;

	public MainObject objMainEff;

	public bool isEff;

	public sbyte numNextFrame = 1;

	public Point PointSpeed;

	public mVector vecObjsBeFire;

	public int life;

	public int goc_Arc;

	public int vx1000;

	public int vy1000;

	public int va;

	private int[] mImageBullet = new int[24]
	{
		0, 0, 2, 1, 1, 2, 0, 0, 2, 1,
		1, 2, 0, 0, 2, 1, 1, 2, 0, 0,
		2, 1, 1, 2
	};

	private int[] mXoayBullet = new int[24]
	{
		2, 2, 3, 3, 3, 4, 5, 5, 5, 5,
		5, 1, 0, 0, 0, 0, 0, 7, 6, 6,
		6, 6, 6, 2
	};

	private int[] mpaintone_Bullet = new int[24]
	{
		12, 11, 10, 9, 8, 7, 6, 5, 4, 3,
		2, 1, 0, 23, 22, 21, 20, 19, 18, 17,
		16, 15, 14, 13
	};

	public virtual void paint(mGraphics g)
	{
	}

	public virtual void paint(mGraphics g, int x, int y)
	{
	}

	public virtual bool CreateEffectSkill()
	{
		return true;
	}

	public virtual void update()
	{
		f++;
		x += vx;
		y += vy;
	}

	public void setPosition(int x, int y, int xto, int yto)
	{
		this.x = x;
		this.y = y;
		toX = xto;
		toY = yto;
	}

	public static bool isInScreen(int x, int y, int w, int h)
	{
		if (x < MainScreen.cameraMain.xCam - w || x > MainScreen.cameraMain.xCam + MotherCanvas.w + w || y < MainScreen.cameraMain.yCam - h || y > MainScreen.cameraMain.yCam + MotherCanvas.h + h)
		{
			return false;
		}
		return true;
	}

	public virtual void replaceHP(mVector vec)
	{
	}

	public Point_Focus create_Speed(int xdich, int ydich, Point_Focus p)
	{
		if (ydich == 0)
		{
			ydich = 1;
		}
		if (xdich == 0)
		{
			xdich = 1;
		}
		int num = 0;
		int num2 = 0;
		int num3 = MainObject.getDistance(xdich, ydich) / vMax;
		if (num3 == 0)
		{
			num3 = 1;
		}
		num = xdich / num3;
		num2 = ydich / num3;
		if (CRes.abs(num) > CRes.abs(xdich))
		{
			num = xdich;
		}
		if (CRes.abs(num2) > CRes.abs(ydich))
		{
			num2 = ydich;
		}
		if (p != null)
		{
			p.x = x;
			p.y = y;
			p.vx = num;
			p.vy = num2;
			p.toX = toX;
			p.toY = toY;
			p.fRe = num3;
		}
		else
		{
			fRemove = num3;
			vx = num;
			vy = num2;
		}
		return p;
	}

	public Point_Focus create_Speed(int xdich, int ydich, Point_Focus p, int x, int y, int toX, int toY)
	{
		if (ydich == 0)
		{
			ydich = 1;
		}
		if (xdich == 0)
		{
			xdich = 1;
		}
		int num = 0;
		int num2 = 0;
		int num3 = MainObject.getDistance(xdich, ydich) / vMax;
		if (num3 == 0)
		{
			num3 = 1;
		}
		num = xdich / num3;
		num2 = ydich / num3;
		if (CRes.abs(num) > CRes.abs(xdich))
		{
			num = xdich;
		}
		if (CRes.abs(num2) > CRes.abs(ydich))
		{
			num2 = ydich;
		}
		if (p != null)
		{
			p.x = x;
			p.y = y;
			p.vx = num;
			p.vy = num2;
			p.toX = toX;
			p.toY = toY;
			p.fRe = num3;
		}
		else
		{
			fRemove = num3;
			vx = num;
			vy = num2;
		}
		return p;
	}

	public void setInfoNormal(MainObject objset)
	{
		if (objset == null)
		{
			goc_Arc = 0;
		}
		else
		{
			switch (objset.Dir)
			{
			case 3:
				goc_Arc = 90;
				break;
			case 1:
				goc_Arc = 270;
				break;
			case 0:
				goc_Arc = 180;
				break;
			case 2:
				goc_Arc = 0;
				break;
			}
		}
		va = 4096;
		vx = 0;
		vy = 0;
		life = 0;
		vx1000 = va * CRes.getcos(goc_Arc) >> 10;
		vy1000 = va * CRes.getsin(goc_Arc) >> 10;
	}

	public void setInfoNormalPhaoHoa(MainObject objset)
	{
		goc_Arc = 270;
		va = 4096;
		vx = 0;
		vy = 0;
		life = 0;
		vx1000 = va * CRes.getcos(goc_Arc) >> 10;
		vy1000 = va * CRes.getsin(goc_Arc) >> 10;
	}

	public bool updateAngleNormal(MainObject objset, int hPlus)
	{
		if (objset == null)
		{
			stopUpdateNormal();
			return true;
		}
		int num = objset.x - x;
		int num2 = objset.y - (objset.hOne >> 1) - hPlus - y;
		life++;
		if ((CRes.abs(num) < vMax / 1000 && CRes.abs(num2) < vMax / 1000) || life > fRemove)
		{
			stopUpdateNormal();
			return true;
		}
		int num3 = CRes.angle(num, num2);
		if (CRes.abs(num3 - goc_Arc) < 90 || num * num + num2 * num2 > 4096)
		{
			if (CRes.abs(num3 - goc_Arc) < 15)
			{
				goc_Arc = num3;
			}
			else if ((num3 - goc_Arc >= 0 && num3 - goc_Arc < 180) || num3 - goc_Arc < -180)
			{
				goc_Arc = CRes.fixangle(goc_Arc + 15);
			}
			else
			{
				goc_Arc = CRes.fixangle(goc_Arc - 15);
			}
		}
		if (va < vMax)
		{
			va += 2048;
		}
		vx1000 = va * CRes.getcos(goc_Arc) >> 10;
		vy1000 = va * CRes.getsin(goc_Arc) >> 10;
		num += vx1000;
		int num4 = num >> 10;
		x += num4;
		num &= 0x3FF;
		num2 += vy1000;
		int num5 = num2 >> 10;
		y += num5;
		num2 &= 0x3FF;
		return false;
	}

	public virtual void removeEff()
	{
	}

	public virtual void stopUpdateNormal()
	{
	}

	public void setAva(int level, MainObject obj)
	{
		if (obj == null || obj.returnAction())
		{
			return;
		}
		if (level == -1)
		{
			obj.vXEffAva = 0;
		}
		switch (level)
		{
		case 0:
			if (obj.dy == 0)
			{
				obj.dy = 12;
			}
			break;
		case 1:
			if (Dir == 0)
			{
				obj.vXEffAva = -4;
			}
			else
			{
				obj.vXEffAva = 4;
			}
			if (obj.dy == 0)
			{
				obj.dy = 16;
			}
			break;
		case 2:
			if (Dir == 0)
			{
				obj.vXEffAva = -6;
			}
			else
			{
				obj.vXEffAva = 6;
			}
			if (obj.dy == 0)
			{
				obj.dy = 20;
			}
			break;
		}
		if (LoadMap.specMap == 4)
		{
			obj.vXEffAva = 0;
		}
		if (obj.typeObject == 1 && (obj.getTypeMoveMonster() == 9 || obj.getTypeMoveMonster() == 8 || obj.getTypeMoveMonster() == 19))
		{
			obj.vXEffAva = 0;
		}
		if (obj.Action != 4 && obj.Action != 2 && obj.Hp > 0)
		{
			obj.Action = 3;
			obj.f = 0;
			obj.resetAction();
		}
		else
		{
			obj.vXEffAva = 0;
			obj.dy = 0;
		}
	}

	public void setDy(int value, MainObject obj)
	{
		if (obj != null && !obj.returnAction())
		{
			obj.dy = value;
			if (obj.Action != 4 && obj.Action != 2)
			{
				obj.Action = 3;
				obj.f = 0;
			}
		}
	}

	public void paint_Bullet(mGraphics g, FrameImage frm, int index, int x, int y, bool isMore, int fbegin)
	{
		frm.drawFrame(mImageBullet[index], x, y, mXoayBullet[index], 3, g);
	}

	public int setFrameAngle(int goc)
	{
		if (goc <= 15 || goc > 345)
		{
			return 12;
		}
		int num = (goc - 15) / 15 + 1;
		if (num > 24)
		{
			num = 24;
		}
		return mpaintone_Bullet[num];
	}

	public void setAngle()
	{
		Dir = 0;
		if (x < toX)
		{
			Dir = 2;
		}
	}
}
