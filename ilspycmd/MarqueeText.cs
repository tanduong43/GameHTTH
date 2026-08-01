public class MarqueeText
{
	public int limit;

	public int maxW;

	public int speed = 2;

	public int xplus;

	public int time;

	public string text;

	public bool isRun;

	public mFont fontPaint;

	public bool isLeftRight;

	public MarqueeText(int maxW)
	{
		this.maxW = maxW;
	}

	public void setdata(string str, mFont font)
	{
		fontPaint = font;
		text = str;
		int width = font.getWidth(str);
		xplus = 0;
		time = 0;
		isLeftRight = false;
		if (width > maxW)
		{
			limit = width - maxW + 20;
			isRun = true;
		}
		else
		{
			isRun = false;
		}
	}

	public void paint(mGraphics g, int x, int y, int align)
	{
		fontPaint.drawString(g, text, x - xplus, y, align);
	}

	public void update()
	{
		if (isRun)
		{
			time++;
			if (time > 10)
			{
				xplus += speed;
			}
			if (xplus >= limit)
			{
				xplus = 0;
				time = 0;
			}
		}
		else
		{
			xplus = 0;
			time = 0;
		}
	}

	public void update2()
	{
		if (isRun)
		{
			time++;
			if (time > 10)
			{
				if (!isLeftRight)
				{
					xplus += speed;
				}
				else
				{
					xplus -= speed;
				}
			}
			if (xplus > limit || xplus < -10)
			{
				isLeftRight = !isLeftRight;
			}
		}
		else
		{
			xplus = 0;
			time = 0;
		}
	}

	public int getxPlus()
	{
		return xplus;
	}
}
