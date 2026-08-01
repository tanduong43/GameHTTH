public class QuaNapThe
{
	public int rubyDaNap;

	public int cost;

	public sbyte statusNhan;

	public short numQua;

	public mVector vecQua = new mVector();

	public int xplus;

	public bool isRun;

	private bool isLeftRight;

	private int time;

	private int speed = 1;

	private int limit;

	private int sizeItem = 29;

	private int wPaintQua = 93;

	private int timeDelay = 20;

	private bool isDelay;

	private int t;

	public QuaNapThe(int cost, sbyte status, short sizeQ)
	{
		this.cost = cost;
		statusNhan = status;
		numQua = sizeQ;
		initAnim();
	}

	public void paint(mGraphics g, int xpaint, int ypaint)
	{
		if (!isRun || numQua <= 3)
		{
			for (int i = 0; i < 3; i++)
			{
				((ItemQuaNT)vecQua.elementAt(i))?.paint(g, xpaint + (sizeItem + 3) * i + sizeItem / 2, ypaint + sizeItem / 2, sizeItem);
			}
			return;
		}
		for (int j = 0; j < numQua; j++)
		{
			ItemQuaNT itemQuaNT = (ItemQuaNT)vecQua.elementAt(j);
			itemQuaNT.paint(g, xpaint - xplus + (sizeItem + 3) * j + sizeItem / 2, ypaint + sizeItem / 2, sizeItem);
		}
	}

	public void update()
	{
		if (isRun)
		{
			time++;
			if (isDelay)
			{
				if (time <= t)
				{
					return;
				}
				isDelay = false;
			}
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
			if (xplus > limit || xplus < 0)
			{
				isLeftRight = !isLeftRight;
				isDelay = true;
				t = time + timeDelay;
			}
		}
		else
		{
			xplus = 0;
			time = 0;
			isDelay = false;
		}
	}

	private void initAnim()
	{
		int num = (sizeItem + 3) * numQua - 3;
		xplus = 0;
		time = 0;
		isLeftRight = false;
		if (num > wPaintQua)
		{
			limit = num - wPaintQua;
			isRun = true;
		}
		else
		{
			isRun = false;
		}
	}
}
