public class Lazer : MainEffect
{
	private int angle0;

	private int lim;

	private new int x;

	private new int y;

	private int xlim;

	private int ylim;

	private int xkc;

	private int ykc;

	private int power;

	private int time;

	private int maxlim;

	private int idColor;

	private sbyte type;

	private int[] color = new int[4] { 0, 8388469, 7710463, 16711680 };

	public Lazer(sbyte type, int x, int y, int xTo, int yTo)
	{
		this.x = x;
		this.y = y;
		xlim = xTo;
		ylim = yTo;
		xkc = xlim - x;
		ykc = ylim - y;
		angle0 = CRes.angle(xkc, ykc);
		lim = 10;
		idColor = 0;
		type = 0;
		time = 10;
	}

	public Lazer()
	{
	}

	public override void paint(mGraphics g)
	{
		if ((angle0 > 60 && angle0 < 120) || (angle0 > 240 && angle0 < 300))
		{
			g.setColor(color[idColor]);
			for (int i = 0; i < lim; i++)
			{
				g.drawLine(x + i, y, x + i + xkc, y + ykc);
				g.drawLine(x - i, y, x - i + xkc, y + ykc);
			}
			g.setColor(16777215);
			for (int j = 0; j < lim / 2; j++)
			{
				g.drawLine(x + j, y, x + j + xkc, y + ykc);
				g.drawLine(x - j, y, x - j + xkc, y + ykc);
			}
		}
		else
		{
			g.setColor(color[idColor]);
			for (int k = 0; k < lim / 2; k++)
			{
				g.drawLine(x, y + k, x + xkc, y + ykc + k);
				g.drawLine(x, y - k, x + xkc, y + ykc - k);
			}
			g.setColor(16777215);
			for (int l = 0; l < lim / 2; l++)
			{
				g.drawLine(x, y + l, x + xkc, y + ykc + l);
				g.drawLine(x, y - l, x + xkc, y + ykc - l);
			}
		}
	}

	public void update2()
	{
		if (time >= 0)
		{
			time--;
		}
		if (time <= 0)
		{
			removeEff();
		}
		lim--;
		if (lim <= 0)
		{
			lim = maxlim;
		}
	}

	public override void update()
	{
		if (type == 0)
		{
			CFrame++;
			if (CFrame > 2)
			{
				CFrame = 0;
			}
			time--;
			if (time <= 0)
			{
				lim--;
			}
			if (lim <= 0)
			{
				removeEff();
			}
		}
		if (type == 1)
		{
			update2();
		}
	}

	public override void removeEff()
	{
		isStop = true;
		f = -1;
	}
}
