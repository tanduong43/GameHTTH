public class DelaySkill
{
	public sbyte typeSkill;

	public long timebegin;

	public int value;

	public int limit;

	public void paint(mGraphics g, int x, int y, int w)
	{
		FrameImage frameImage = AvMain.fraDelay;
		if (w > 32)
		{
			w = 32;
		}
		if (w <= 24)
		{
			frameImage = AvMain.fraDelay2;
		}
		if (limit <= 0)
		{
			return;
		}
		if (value > 0)
		{
			int num = 4 - value / (limit / 5);
			if (num >= 0 && num <= 5)
			{
				g.drawRegion(frameImage.imgFrame, frameImage.frameWidth / 2 - w / 2, frameImage.frameWidth / 2 - w / 2 + num * frameImage.frameHeight, w, w, 0, x, y, 0);
			}
			int num2 = value / 1000;
			string empty = string.Empty;
			empty = ((num2 != 0) ? (string.Empty + num2) : ("0." + value % 1000 / 100));
			mFont.tahoma_7_white.drawString(g, empty, x + w / 2, y + w / 2 - 5, 2);
		}
		else if (value > -150)
		{
			g.setColor(15658700);
			g.fillRoundRect(x + 1, y + 1, w - 2, w - 2, 4, 4);
		}
	}

	public void paintOnlytime(mGraphics g, int x, int y, int w, sbyte color)
	{
		int num = value / 1000;
		string empty = string.Empty;
		empty = ((num != 0) ? (string.Empty + num) : ("0." + value % 1000 / 100));
		AvMain.setTextColor(color).drawString(g, empty, x + w / 2, y + w / 2 - 5, 2);
	}

	public bool isCoolDown()
	{
		if (value > 0)
		{
			return false;
		}
		return true;
	}

	public static DelaySkill getDelay(int index)
	{
		DelaySkill delaySkill = (DelaySkill)Player.delaySkill.get(string.Empty + index);
		if (delaySkill == null)
		{
			delaySkill = new DelaySkill();
			delaySkill.value = -150;
			delaySkill.limit = 0;
			delaySkill.timebegin = GameCanvas.timeNow;
			Player.delaySkill.put(string.Empty + index, delaySkill);
		}
		return delaySkill;
	}
}
