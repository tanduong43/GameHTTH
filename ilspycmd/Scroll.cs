public class Scroll
{
	private int x;

	private int y;

	private int h;

	private int color;

	private int yScroll;

	private int hScroll;

	public static int hRectScroll = 24;

	public static int maxRectScroll = 24;

	private int hEffScroll;

	public void setInfo(int x, int y, int h, int color)
	{
		this.x = x;
		this.y = y;
		this.h = h;
		this.color = color;
		hRectScroll = maxRectScroll;
		hScroll = h - maxRectScroll;
	}

	public void paint(mGraphics g)
	{
		g.setColor(color);
		g.fillRect(x - 2, y - 1, 3, 1);
		g.fillRect(x - 3, y, 1, h - 1);
		g.fillRect(x + 1, y, 1, h - 1);
		g.fillRect(x - 2, y + h - 1, 3, 1);
		if (hEffScroll <= 0)
		{
			g.fillRect(x - 2, y + yScroll, 3, hRectScroll - CRes.abs(hEffScroll));
		}
		else
		{
			g.fillRect(x - 2, y + yScroll + CRes.abs(hEffScroll), 3, hRectScroll - CRes.abs(hEffScroll));
		}
	}

	public void setYScrool(int yS, int yMax)
	{
		if (yMax <= 0)
		{
			return;
		}
		yScroll = yS * hScroll / yMax;
		if (yScroll >= 0 && yScroll <= hScroll)
		{
			hEffScroll = 0;
			return;
		}
		if (yScroll > hScroll)
		{
			hEffScroll = (yScroll - hScroll) / 18;
			yScroll = hScroll;
		}
		if (yScroll < 0)
		{
			hEffScroll = yScroll / 18;
			yScroll = 0;
		}
		sethEff();
	}

	public void sethEff()
	{
		if (hEffScroll > 12)
		{
			hEffScroll = 12;
		}
		else if (hEffScroll < -12)
		{
			hEffScroll = -12;
		}
	}
}
