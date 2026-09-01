public class EffectNum : MainEffect
{
	public const sbyte COLOR_NORMAL = 0;

	public const sbyte COLOR_XP = 1;

	public const sbyte COLOR_FIRE = 2;

	public const sbyte COLOR_PLUS_HP = 3;

	public const sbyte COLOR_PLUS_MP = 4;

	public const sbyte COLOR_PUT_ITEM = 5;

	public const sbyte COLOR_FIRE_PERSON = 6;

	public const sbyte COLOR_EFF_CRI = 7;

	public const sbyte COLOR_OPTION = 8;

	public const sbyte COLOR_EFF_MON_FIRE = 9;

	public const sbyte COLOR_EFF_BELI = 10;

	public const sbyte COLOR_FIRE_RED = 11;

	public const sbyte COLOR_BIG_NUMBER_SMALL_Y = 13;

	public const sbyte COLOR_BIG_NUMBER_SMALL_R = 14;

	public const sbyte COLOR_BIG_NUMBER_SMALL_W = 15;

	public const sbyte COLOR_BIG_CRI = 16;

	public const sbyte COLOR_BIG_MISS = 17;

	public const sbyte COLOR_WANTED = 18;

	public const sbyte COLOR_BIG_NUMBER_SMALL_V = 19;

	public const sbyte COLOR_BIG_NUMBER_SMALL_G = 20;

	public const sbyte COLOR_BIG_NUMBER_SMALL_EFF_R = 21;

	public const sbyte COLOR_BIG_NUMBER_SMALL_EFF_B = 22;

	public const sbyte COLOR_SUPER_BIG_CRI = 23;

	public const sbyte COLOR_XP_SKILL = 24;

	public const sbyte COLOR_HAP_THU = 25;

	private string strContent;

	private int typeNum;

	private int tick;

	private int valueNum;

	private int valueAP;

	private int timeStop;

	private int tickStop;

	private int xlechAP;

	private int valuePaintDam;

	private int tickSuperDam;

	private int stepSuperDam;

	private mFont fontPaint;

	private bool isGravity;

	private bool isTrieuDam;

	public static int numPaintDam;

	public EffectNum(string strContent, int x, int y, int typeColor)
	{
		createEffNum(strContent, x, y, typeColor);
	}

	public EffectNum(string strContent, int x, int y, int typeColor, FrameImage fra, int frame)
	{
		if (fra != null)
		{
			fraImgEff = fra;
			base.frame = frame;
		}
		createEffNum(strContent, x, y, typeColor);
	}

	public EffectNum(int value, int valueAP, int x, int y, int typeColor)
	{
		createEffNum(value, valueAP, x, y, typeColor);
	}

	public void createEffNum(int value, int valueAP, int x, int y, int typeColor)
	{
		numPaintDam++;
		valueEffect = 1;
		isGravity = false;
		tick = 0;
		tickStop = 0;
		base.x = x;
		base.y = y;
		typeNum = typeColor;
		valueNum = value;
		this.valueAP = valueAP;
		vy = -CRes.random(7, 9);
		fRemove = CRes.random(26, 30);
		int num = numPaintDam % 9;
		if (num % 3 != 0)
		{
			if (numPaintDam % 5 % 2 == 1)
			{
				vx = 3;
			}
			else
			{
				vx = -3;
			}
		}
		timeStop = 6 + num / 3;
		if (typeNum != 16 && typeNum != 25)
		{
			return;
		}
		valuePaintDam = value;
		timeStop = 8;
		if (value >= 1000000)
		{
			isTrieuDam = true;
			valuePaintDam = 500000;
			stepSuperDam = (value - valuePaintDam) / 10;
			if (stepSuperDam > 300000)
			{
				stepSuperDam = 300000;
			}
			levelPaint = 1;
			fRemove += 20;
		}
	}

	public void createEffNum(string strContent, int x, int y, int typeColor)
	{
		valueEffect = 1;
		isGravity = false;
		tick = 0;
		this.strContent = strContent;
		base.x = x;
		base.y = y;
		typeNum = typeColor;
		fontPaint = mFont.tahoma_7b_white;
		if (typeNum < 0)
		{
			fontPaint = AvMain.setTextColorName(-typeColor);
		}
		else
		{
			switch (typeColor)
			{
			case 1:
				vy = -2;
				fRemove = 16;
				fontPaint = mFont.tahoma_7b_yellow;
				return;
			case 24:
				vy = -2;
				fRemove = 16;
				fontPaint = mFont.tahoma_7b_violet;
				return;
			case 5:
				fontPaint = mFont.tahoma_7_white;
				break;
			case 8:
				fontPaint = AvMain.setTextColor(typeColor);
				break;
			case 10:
				vy = -1;
				fRemove = CRes.random(25, 35);
				return;
			}
		}
		if (isGravity)
		{
			vy = -CRes.random(8, 12);
			fRemove = CRes.random(24, 30);
		}
		else
		{
			vy = -2;
			fRemove = 20;
		}
	}

	public override void paint(mGraphics g)
	{
		int num = 0;
		if (fraImgEff != null)
		{
			num = fraImgEff.frameWidth / 2;
			int num2 = mFont.tahoma_7b_white.getWidth(strContent) / 2;
			fraImgEff.drawFrame(frame, x - num2, y + 5, 0, 3, g);
		}
		switch (typeNum)
		{
		case 2:
			AvMain.Font3dWhite(g, strContent, x + num, y, 2);
			break;
		case 6:
			mFont.tahoma_7b_red.drawString(g, strContent, x + num, y, 2);
			break;
		case 7:
			AvMain.Font3dColor(g, strContent, x + num, y, 2, 5);
			break;
		case 4:
			AvMain.FontBorderColor(g, strContent, x + num, y, 2, 4, 7);
			break;
		case 3:
			AvMain.FontBorderColor(g, strContent, x + num, y, 2, 1, 7);
			break;
		case 5:
			AvMain.Font3dColor(g, strContent, x + num, y, 2, 1);
			break;
		case 1:
			mFont.tahoma_7b_black.drawString(g, strContent, x + num + 1, y + 1, 2);
			fontPaint.drawString(g, strContent, x + num, y, 2);
			break;
		case 24:
			mFont.tahoma_7b_black.drawString(g, strContent, x + num + 1, y + 1, 2);
			fontPaint.drawString(g, strContent, x + num, y, 2);
			break;
		case 9:
			AvMain.Font3dColor(g, strContent, x + num, y, 2, 6);
			break;
		case 10:
			mFont.tahoma_7b_yellow.drawString(g, strContent, x + num, y, 2);
			break;
		case 13:
		case 14:
		case 15:
		case 20:
			NumberDam.paintSmall(g, valueNum, x + num, y, typeNum);
			if (valueAP > 0)
			{
				NumberDam.paintSmall(g, valueAP, x + num + 12, y + 12, 19);
			}
			break;
		case 21:
		case 22:
		case 25:
			NumberDam.paintSmall(g, valueNum, x + num, y, typeNum);
			break;
		case 16:
			if (isTrieuDam && valuePaintDam == valueNum)
			{
				if (tickSuperDam % 4 <= 3 || tickSuperDam >= 8)
				{
					NumberDam.paintSmall(g, valuePaintDam, x + num, y, 23);
				}
			}
			else
			{
				NumberDam.paintSmall(g, valuePaintDam, x + num, y, typeNum);
			}
			if (valueAP > 0)
			{
				NumberDam.paintSmall(g, valueAP, x + num + 12, y + 16, 19);
			}
			break;
		case 17:
			NumberDam.paintStringBig(g, valueNum, x + num, y, typeNum);
			if (valueAP > 0)
			{
				NumberDam.paintSmall(g, valueAP, x + num + 12, y + 14, 19);
			}
			break;
		default:
			fontPaint.drawString(g, strContent, x + num, y, 2);
			break;
		}
	}

	public override void update()
	{
		f++;
		x += vx;
		y += vy;
		if (f >= fRemove)
		{
			isStop = true;
		}
		if (isTrieuDam)
		{
			if (valuePaintDam < valueNum)
			{
				valuePaintDam += stepSuperDam + CRes.random(10) * 100;
			}
			else
			{
				tickSuperDam++;
			}
			if (valuePaintDam > valueNum)
			{
				valuePaintDam = valueNum;
			}
		}
		if (tick > 0)
		{
			tick--;
		}
		tickStop++;
		if (typeNum == 14 || typeNum == 13 || typeNum == 15 || typeNum == 20 || typeNum == 21 || typeNum == 22 || typeNum == 17 || typeNum == 16 || typeNum == 25)
		{
			if (typeNum != 16 && f > fRemove - 6)
			{
				vy = 3;
			}
			else if (tickStop >= timeStop && vy < 0)
			{
				int num = CRes.abs(vy) / 2;
				if (num < 2)
				{
					num = 2;
				}
				vy += num;
				if (vy >= 0)
				{
					vy = 0;
					vx = 0;
				}
			}
		}
		else
		{
			if (isGravity && tick <= 0)
			{
				vy++;
			}
			if (vy == 0 && tick == 0)
			{
				tick = 5;
			}
		}
		if (tickSuperDam >= 6)
		{
			vy = -1;
		}
	}
}
