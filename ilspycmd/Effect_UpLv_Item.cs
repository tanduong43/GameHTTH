public class Effect_UpLv_Item
{
	private int wnew = 1;

	private int[][] colorBorder = new int[8][]
	{
		new int[7] { 16777215, 16777215, 15724527, 15724527, 12698049, 12698049, 10526880 },
		new int[7] { 2684726, 2684726, 2481970, 2481970, 2213420, 2213420, 2529579 },
		new int[7] { 11830015, 11830015, 10975725, 10975725, 10121178, 10121178, 8215216 },
		new int[7] { 16580155, 16580155, 15264311, 15264311, 13948466, 13948466, 11184937 },
		new int[7] { 16122935, 16122935, 14746675, 14746675, 13304878, 13304878, 11272999 },
		new int[7] { 1832452, 1832452, 16122935, 16122935, 16777215, 16777215, 15724527 },
		new int[7] { 1832452, 1832452, 16122935, 2684726, 16777215, 16777215, 15724527 },
		new int[7] { 1832452, 1832452, 16122935, 2684726, 11830015, 16777215, 15724527 }
	};

	private int[] size = new int[6] { 2, 1, 1, 1, 1, 1 };

	private int[] size15 = new int[8] { 2, 2, 1, 1, 1, 1, 1, 1 };

	private int valueTest;

	private int lv;

	public void paintUpgradeEffect(int x, int y, int upgrade, int w, mGraphics g, int lech, bool isBorder)
	{
		if (upgrade <= 0)
		{
			return;
		}
		lv = getValueUpgrade(upgrade);
		int num = 0;
		int num2 = lv;
		if (isBorder)
		{
			if (upgrade >= 11)
			{
				g.setColor(colorBorder[GameCanvas.gameTick / 6 % 5][6]);
				g.drawRect(x - w / 2 - lech, y - w / 2 - lech, w, w);
			}
			else
			{
				g.setColor(colorBorder[num2][6]);
				g.drawRect(x - w / 2 - lech, y - w / 2 - lech, w, w);
			}
		}
		int[] array = ((upgrade < 11) ? size : size15);
		for (int i = num; i < array.Length; i++)
		{
			int num3 = upgradeEffectX(GameCanvas.gameTick - i * wnew, w);
			if (num3 != -1)
			{
				int num4 = x - w / 2 + num3;
				int num5 = y - w / 2 + upgradeEffectY(GameCanvas.gameTick - i * wnew, w);
				int num6 = i;
				if (num6 > 5)
				{
					num6 = 5;
				}
				g.setColor(colorBorder[num2][num6]);
				int num7 = array[i];
				int num8 = 0;
				if (num3 <= w && num7 == 2)
				{
					num8 = 1;
				}
				g.fillRect(num4 - num7 / 2 - lech, num5 - num7 / 2 - lech + num8, num7, num7);
			}
		}
		for (int j = num; j < array.Length; j++)
		{
			int num9 = upgradeEffectX2(GameCanvas.gameTick + w - j * wnew, w);
			if (num9 != -1)
			{
				int num10 = x - w / 2 + num9;
				int num11 = y - w / 2 + upgradeEffectY(GameCanvas.gameTick + w - j * wnew, w);
				int num12 = j;
				if (num12 > 5)
				{
					num12 = 5;
				}
				g.setColor(colorBorder[num2][num12]);
				int num13 = array[j];
				int num14 = 0;
				if (num9 == 0 && num13 == 2)
				{
					num14 = 1;
				}
				g.fillRect(num10 - num13 / 2 - lech + num14, num11 - num13 / 2 - lech, num13, num13);
			}
		}
	}

	public int upgradeEffectY(int tick, int w)
	{
		int num = (tick + valueTest) % (4 * w);
		if (0 <= num && num < w)
		{
			return 0;
		}
		if (w <= num && num < w * 2)
		{
			return num % w;
		}
		if (w * 2 <= num && num < w * 3)
		{
			return w;
		}
		return w - num % w;
	}

	public int upgradeEffectX(int tick, int w)
	{
		int num = (tick + valueTest) % (4 * w);
		if (0 <= num && num < w)
		{
			return num % w;
		}
		if (w <= num && num < w * 2)
		{
			return w;
		}
		return -1;
	}

	public int upgradeEffectX2(int tick, int w)
	{
		int num = (tick + valueTest) % (4 * w);
		if (w <= num && num < w * 2)
		{
			return 0;
		}
		if (w * 2 <= num && num < w * 3)
		{
			return w - (w - num % w);
		}
		if (valueTest == 0)
		{
			valueTest = w * 2;
		}
		else
		{
			valueTest = 0;
		}
		return -1;
	}

	public int getValueUpgrade(int up)
	{
		if (up <= 2)
		{
			return 0;
		}
		if (up <= 5)
		{
			return 1;
		}
		if (up <= 8)
		{
			return 2;
		}
		if (up <= 9)
		{
			return 3;
		}
		if (up <= 10)
		{
			return 4;
		}
		if (up <= 11)
		{
			return 1;
		}
		if (up <= 12)
		{
			return 2;
		}
		if (up <= 13)
		{
			return 3;
		}
		if (up <= 14)
		{
			return 4;
		}
		return 5;
	}
}
