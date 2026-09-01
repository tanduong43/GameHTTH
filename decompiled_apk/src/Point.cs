public class Point
{
	public int x;

	public int y;

	public int g;

	public int v;

	public int w;

	public int h;

	public int color;

	public int limitY;

	public int vx;

	public int vy;

	public int x2;

	public int y2;

	public int x0;

	public int y0;

	public int dis;

	public int f;

	public int fRe;

	public int frame;

	public int maxframe;

	public int fSmall;

	public int levelPaint;

	public int subType;

	public mVector vecEffPoint;

	public string name;

	public bool isRemove;

	public bool isSmall;

	public static FrameImage[] FraEffInMap;

	public FrameImage fraImgEff;

	public MainObject obj;

	public Point()
	{
	}

	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public void update()
	{
		f++;
		x += vx;
		y += vy;
	}

	public void paint(mGraphics g)
	{
		if (!isRemove)
		{
			int num = 0;
			if (isSmall && f >= fSmall)
			{
				num = 1;
			}
			FraEffInMap[color].drawFrame(frame / 2 + num, x, y, dis, 3, g);
		}
	}

	public void updateInMap()
	{
		f++;
		if (maxframe > 1)
		{
			frame++;
			if (frame / 2 >= maxframe)
			{
				frame = 0;
			}
		}
		if (f >= fRe)
		{
			isRemove = true;
		}
	}

	public void updateObj()
	{
		if (obj != null && obj.Action != 4 && !obj.returnAction())
		{
			x = obj.x;
			y = obj.y;
		}
	}
}
