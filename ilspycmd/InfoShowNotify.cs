public class InfoShowNotify
{
	public const sbyte INFO_NOR = 0;

	public const sbyte INFO_IMP = 1;

	public const sbyte INFO_SPE = 2;

	public const sbyte INFO_LOL_MEM = 3;

	public const sbyte INFO_LOL_CAPTION = 4;

	public const sbyte INFO_FIGHT = 5;

	public const sbyte INFO_PLAYER = 10;

	public string strShow;

	public bool isRemove;

	public bool isPaint = true;

	public short iconClan = -1;

	public mFont fontpaint = mFont.tahoma_7_white;

	public int time;

	public int x;

	public int y;

	public int w;

	public int speed;

	public int maxtime;

	public sbyte type;

	public InfoShowNotify(string str, sbyte type)
	{
		strShow = str;
		this.type = type;
	}

	public void setValue(mFont f)
	{
		if (strShow == null || strShow.Length == 0)
		{
			isRemove = true;
			return;
		}
		time = 0;
		isRemove = false;
		setFont(f);
		if (type == 10)
		{
			speed = 0;
			maxtime = 70;
			x = Interface_Game.wInfoServer / 2;
			return;
		}
		w = fontpaint.getWidth(strShow);
		if (iconClan >= 0)
		{
			w += 30;
		}
		x = 0;
		maxtime = 500;
		y = 25;
		if (w < Interface_Game.wInfoServer)
		{
			speed = 0;
			maxtime = 150;
			x = Interface_Game.wInfoServer / 2 + w / 2;
		}
		else
		{
			speed = 2;
		}
	}

	public void setFont(mFont f)
	{
		if (f != null)
		{
			fontpaint = f;
			return;
		}
		switch (type)
		{
		case 0:
			fontpaint = mFont.tahoma_7_white;
			break;
		case 1:
			fontpaint = mFont.tahoma_7b_white;
			break;
		case 2:
			fontpaint = mFont.tahoma_7b_yellow;
			break;
		case 10:
			fontpaint = mFont.tahoma_7_white;
			break;
		case 5:
			fontpaint = mFont.tahoma_7b_white;
			break;
		}
	}

	public void update()
	{
		if (isPaint)
		{
			time++;
			x += speed;
		}
		if (time > maxtime || x > w + Interface_Game.wInfoServer)
		{
			isRemove = true;
		}
		else if (y > 0)
		{
			y -= 10;
			if (y < 0)
			{
				y = 0;
			}
		}
	}

	public void paintQuickChat(mGraphics g, int x, int y)
	{
		if (type == 3)
		{
			AvMain.FontBorderSmall(g, strShow, x, y, 1, 0);
		}
		else if (type == 4)
		{
			AvMain.FontBorderSmall(g, strShow, x, y, 1, 5);
		}
	}
}
