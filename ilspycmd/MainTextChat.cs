public class MainTextChat
{
	public string text;

	public string textRight = string.Empty;

	public string textTime = string.Empty;

	public string nameText;

	public sbyte color;

	public sbyte typeLeftRight = -1;

	public sbyte typeBg;

	public sbyte typeEvent;

	public sbyte lenghtText;

	public long timeBegin = -1L;

	public short IDMem = -1;

	public short IdText;

	public MainTextChat(string text, sbyte color)
	{
		this.text = text;
		this.color = color;
	}

	public MainTextChat(string text, string textRight, sbyte color, sbyte colorBg, sbyte typeBg)
	{
		this.text = text;
		this.color = color;
		this.color = colorBg;
		this.typeBg = typeBg;
		this.textRight = textRight;
	}

	public void setTimePaint()
	{
		long num = (GameCanvas.timeNow - timeBegin) / 1000;
		if (num < 60)
		{
			textTime = 1 + T.phuttruoc;
		}
		else if (num < 3600)
		{
			textTime = num / 60 + T.phuttruoc;
		}
		else if (num < 86400)
		{
			textTime = num / 3600 + T.gio + " " + num % 3600 / 60 + T.phuttruoc;
		}
		else if (num < 604800)
		{
			textTime = num / 86400 + " " + T.ngaytruoc;
		}
		else
		{
			textTime = T.hon1tuan;
		}
	}
}
