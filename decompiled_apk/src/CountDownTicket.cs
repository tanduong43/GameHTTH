public class CountDownTicket
{
	public int timeCountDown;

	public long tickBeginCount;

	public string strInfo = string.Empty;

	public sbyte typeTime;

	public int valueLeft;

	public int valueright;

	public void setCountDown(int time)
	{
		timeCountDown = time;
		tickBeginCount = GameCanvas.timeNow;
	}

	public int CheckUpdate()
	{
		if (timeCountDown > 0)
		{
			updateTimeCountDownTicket();
			if (timeCountDown > 0)
			{
				return 1;
			}
			return 0;
		}
		return -1;
	}

	public void updateTimeCountDownTicket()
	{
		if (timeCountDown > 0)
		{
			if (GameCanvas.timeNow - tickBeginCount > 2000)
			{
				short num = (short)((GameCanvas.timeNow - tickBeginCount) / 1000);
				timeCountDown -= num;
				tickBeginCount += num * 1000;
			}
			if (GameCanvas.timeNow - tickBeginCount > 1000)
			{
				timeCountDown--;
				tickBeginCount += 1000L;
			}
		}
	}

	public void paintCountDownTicket(mGraphics g, mFont f, int x, int y, int anchor)
	{
		int time = 0;
		if (timeCountDown > 0)
		{
			time = timeCountDown;
		}
		f.drawString(g, timeShow(time), x, y, anchor);
	}

	public void paintCountDownTicketHour(mGraphics g, mFont f, int x, int y, int anchor)
	{
		if (timeCountDown >= 3600)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			int num = timeCountDown / 60;
			int num2 = num / 60;
			int num3 = num % 60;
			empty = ((num2 >= 10) ? (string.Empty + num2) : ("0" + num2));
			f.drawString(g, empty, x, y, anchor);
			int width = f.getWidth(empty);
			if (GameCanvas.gameTick % 25 < 20)
			{
				f.drawString(g, ":", x + width + 1, y, anchor);
			}
			empty2 = ((num3 >= 10) ? (empty2 + string.Empty + num3) : (empty2 + "0" + num3));
			f.drawString(g, empty2, x + width + 4, y, anchor);
		}
		else
		{
			f.drawString(g, timeShow(timeCountDown), x, y, anchor);
		}
	}

	public static string timeShow(int time)
	{
		string empty = string.Empty;
		if (time >= 3600)
		{
			int num = time / 60;
			empty += num / 60;
			empty = ((GameCanvas.gameTick % 25 >= 20) ? (empty + " ") : (empty + ":"));
			if (num % 60 < 10)
			{
				return empty + "0" + num % 60;
			}
			return empty + string.Empty + num % 60;
		}
		empty = ((time < 60) ? (empty + "00") : ((time >= 600) ? (empty + time / 60) : (empty + "0" + time / 60)));
		if (time % 60 < 10)
		{
			return empty + ":0" + time % 60;
		}
		return empty + ":" + time % 60;
	}
}
