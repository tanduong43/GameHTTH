using System;

public class PopupChat : AvMain
{
	private int x = MotherCanvas.hw;

	private int y = MotherCanvas.hh;

	private int dy;

	public int h;

	public int w;

	public int timeOff;

	public int indexpaint;

	public short idEmotion;

	public string[] strChat;

	public static mImage[] mPopup = new mImage[2];

	private bool isStop = true;

	private sbyte isEmotion;

	public const sbyte E_NONE = 0;

	public const sbyte E_EMOTION = 1;

	public const sbyte E_ITEM = 2;

	private MainItem item;

	private int[] color = new int[2] { 3349556, 16760428 };

	public void setChat(string strContent, bool isStop)
	{
		this.isStop = isStop;
		isEmotion = 0;
		strContent = strContent.Trim();
		w = mFont.tahoma_7_black.getWidth(strContent);
		if (w > 100)
		{
			w = 100;
		}
		else if (w < 20)
		{
			w = 20;
		}
		dy = 8;
		strChat = mFont.tahoma_7_black.splitFontArray(strContent, w);
		h = strChat.Length * GameCanvas.hText;
		if (strChat.Length <= 2)
		{
			timeOff = 100;
		}
		else
		{
			timeOff = 160;
		}
	}

	public void setChat(short id)
	{
		isStop = true;
		isEmotion = 1;
		w = 22;
		h = 22;
		dy = 8;
		idEmotion = id;
		timeOff = 200;
	}

	public void setChatItem(short id, sbyte cat, short num)
	{
		isStop = true;
		isEmotion = 2;
		w = 22;
		h = 22;
		dy = 8;
		item = new MainItem(cat, id, id);
		item.numPotion = num;
		timeOff = 100;
	}

	public override void paint(mGraphics g)
	{
		if (dy > 0)
		{
			dy -= 2;
		}
		paintPopup(g);
	}

	public void updatePos(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public bool setOff()
	{
		timeOff--;
		if (timeOff <= 0 && isStop)
		{
			return true;
		}
		return false;
	}

	public void paintPopup(mGraphics g)
	{
		try
		{
			int num = y - h + dy;
			int num2 = x - w / 2;
			g.setColor(color[0]);
			g.fillRect(num2 - 3, num, w + 6, h);
			g.fillRect(num2, num - 3, w, h + 6);
			g.setColor(color[1]);
			g.fillRect(num2, num - 2, w, h + 4);
			g.fillRect(num2 - 2, num, w + 4, h);
			g.drawRegion(mPopup[0], 0, 0, 3, 3, 0, num2 - 3, num - 3, 0);
			g.drawRegion(mPopup[0], 0, 3, 3, 3, 0, num2 + w, num - 3, 0);
			g.drawRegion(mPopup[0], 0, 9, 3, 3, 0, num2 - 3, num + h, 0);
			g.drawRegion(mPopup[0], 0, 6, 3, 3, 0, num2 + w, num + h, 0);
			if (indexpaint == 1)
			{
				g.drawRegion(mPopup[1], 0, 0, 7, 7, 3, num2 + w / 2 - 3, num - 9, 0);
			}
			else
			{
				g.drawImage(mPopup[1], num2 + w / 2 - 3, num + h + 2, 0);
			}
			if (isEmotion != 0)
			{
				if (isEmotion == 1)
				{
					MainImage imageAll = ObjectData.getImageAll(idEmotion, ObjectData.hashImageItemOther, 9000);
					if (imageAll.img != null && imageAll.frame == -1)
					{
						imageAll.set_Frame(24);
					}
					int num3 = imageAll.frame + 1;
					if (num3 == 0)
					{
						num3 = 1;
					}
					int num4 = GameCanvas.gameTick / 8 % num3;
					if (num4 >= imageAll.frame)
					{
						num4 = imageAll.frame - 1;
					}
					g.drawRegion(imageAll.img, 0, num4 * 24, 24, 24, 0, num2 + w / 2, num + h / 2, 3);
				}
				else if (isEmotion == 2 && item != null)
				{
					item.paintAllItem(g, num2 + w / 2, num + h / 2, 22, 0, 0);
				}
			}
			else if (strChat != null)
			{
				for (int i = 0; i < strChat.Length; i++)
				{
					mFont.tahoma_7_black.drawString(g, strChat[i], num2 + w / 2, num + 1 + i * GameCanvas.hText, 2);
				}
			}
		}
		catch (Exception)
		{
		}
	}
}
