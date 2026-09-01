public class QuayWCScreen : LuckyScreen
{
	public new static QuayWCScreen instance;

	public short idIconVongQuay = -1;

	public short numLanDaQuay;

	public short numVe;

	public QuayWCScreen()
	{
		StepQuaySo = 0;
		w = 260;
		h = 215;
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2;
		xQuay = x + 100;
		yQuay = y + h / 2 + 12;
		wButton = (h - 25) / 5 + 5;
		xButton = x + w - 40;
		yButton = y + 15 + wButton;
		LuckyScreen.mImgVongquay = null;
		loadImgVongQuay();
		loadIconVongQuay();
		cmdClose = new iCommand(T.close, -1, this);
		cmdQuay = new iCommand(string.Empty, 2, 1, this);
		cmdQuay3Lan = new iCommand(string.Empty, 2, 2, this);
		cmdQuay.setPos(xButton, yButton + wButton * 2, fraImg1Lan, string.Empty);
		cmdQuay3Lan.setPos(xButton, yButton + wButton * 3, fraImg5Lan, string.Empty);
		vecCmd = new mVector();
		vecCmd.addElement(cmdQuay);
		vecCmd.addElement(cmdQuay3Lan);
		vecCmd.addElement(cmdClose);
		if (GameCanvas.isTouch)
		{
			int num = x + w - 13;
			if (num > MotherCanvas.w - 9)
			{
				num = MotherCanvas.w - 9;
			}
			cmdClose.setPos(num, y + 13, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			AvMain.setPosCMD(cmdClose, 2);
			right = cmdClose;
			idSelect = 1;
			cmdQuay.isPlayframe = true;
		}
		setPotion();
		mItemShow = null;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			break;
		case 1:
			GlobalService.gI().QuayWC(4);
			break;
		case 2:
			GlobalService.gI().QuayWC((sbyte)subIndex);
			break;
		case 0:
			break;
		}
	}

	public override void setPosPotion()
	{
		for (int i = 0; i < LuckyScreen.mListItemLucky.size(); i++)
		{
			MainItem mainItem = (MainItem)LuckyScreen.mListItemLucky.elementAt(i);
			if (i < 8)
			{
				mainItem.x = xQuay + CRes.getcos(CRes.fixangle(i * 360 / 8)) * 66 / 1000;
				mainItem.y = yQuay + CRes.getsin(CRes.fixangle(i * 360 / 8)) * 66 / 1000;
			}
			else if (i % 2 == 0)
			{
				mainItem.x = xQuay + 1 + CRes.getcos(CRes.fixangle(30 + i / 2 * 360 / 6)) * 32 / 1000;
				mainItem.y = yQuay + CRes.getsin(CRes.fixangle(30 + i / 2 * 360 / 6)) * 32 / 1000;
			}
			else
			{
				mainItem.x = xQuay + 1 + CRes.getcos(CRes.fixangle(210 + i / 2 * 360 / 6)) * 32 / 1000;
				mainItem.y = yQuay + CRes.getsin(CRes.fixangle(210 + i / 2 * 360 / 6)) * 32 / 1000;
			}
		}
	}

	public override void loadImgVongQuay()
	{
		if (LuckyScreen.mImgVongquay == null)
		{
			LuckyScreen.mImgVongquay = new mImage[6];
			for (int i = 0; i < LuckyScreen.mImgVongquay.Length; i++)
			{
				if (i != 5)
				{
					LuckyScreen.mImgVongquay[i] = mImage.createImage("/interface/lucky" + i + ".png");
				}
			}
		}
		if (fraImgBuy == null)
		{
			fraImgBuy = new FrameImage(mImage.createImage("/interface/lucky8.png"), 37, 27);
			fraImg1Lan = new FrameImage(mImage.createImage("/interface/lucky6.png"), 40, 42);
			fraImg5Lan = new FrameImage(mImage.createImage("/interface/lucky7.png"), 40, 42);
		}
	}

	public void loadIconVongQuay()
	{
		if (LuckyScreen.mImgVongquay[5] == null && idIconVongQuay != -1)
		{
			MainImage imageAll = ObjectData.getImageAll(idIconVongQuay, ObjectData.HashImageOtherNew, 23000);
			if (imageAll != null)
			{
				LuckyScreen.mImgVongquay[5] = imageAll.img;
			}
		}
	}

	public override void paint(mGraphics g)
	{
		loadIconVongQuay();
		base.paint(g);
		mFont.tahoma_7b_black.drawString(g, "QUAY", xButton, yButton + wButton + 5 - 12, 2);
		AvMain.FontBorderSmall(g, string.Empty + numLanDaQuay, xButton, yButton + wButton + 5, 2, 4);
	}

	public override int updateNumve()
	{
		return numVe;
	}
}
