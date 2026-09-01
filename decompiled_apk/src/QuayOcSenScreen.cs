public class QuayOcSenScreen : LuckyScreen
{
	public const sbyte STEP_SHOW_KETQUA = 5;

	public const sbyte TYPE_XOAY_RUBY = 3;

	public const sbyte TYPE_XOAY_OC_SEN = 4;

	public new static QuayOcSenScreen instance;

	private iCommand cmdQuayOcSen;

	private sbyte indexQuaySo = -1;

	private bool isClockwise;

	private static mImage imgPhatSang;

	private static mImage imgOcSen;

	private sbyte[] mNhan;

	public string title = string.Empty;

	private int countRemainGift;

	private MainItem vequay;

	public QuayOcSenScreen()
	{
		w = 280;
		h = 230;
		if (h > MotherCanvas.h)
		{
			h = MotherCanvas.h;
		}
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2;
		xQuay = x + 110;
		yQuay = y + h / 2 + 6;
		wButton = (h - 25) / 5 + 5;
		xButton = x + w - 50;
		yButton = y + 22 + wButton;
		LuckyScreen.mImgVongquay = null;
		loadImgVongQuay();
		cmdClose = new iCommand(T.close, -1, this);
		cmdQuayOcSen = new iCommand(string.Empty, 1, 4, this);
		cmdQuay = new iCommand(string.Empty, 1, 3, this);
		cmdQuayOcSen.setPos(xButton, yButton + wButton + 30, fraImgBuy, string.Empty);
		cmdQuay.setPos(xButton, yButton + wButton * 2 + 25, fraImg1Lan, string.Empty);
		vecCmd = new mVector();
		vecCmd.addElement(cmdQuayOcSen);
		vecCmd.addElement(cmdQuay);
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
		isClockwise = false;
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
			GlobalService.gI().Quay_oc_sen((sbyte)subIndex);
			isClockwise = !isClockwise;
			if (subIndex == 4)
			{
				typeQuay = 4;
			}
			else
			{
				typeQuay = 3;
			}
			break;
		}
	}

	public override void setPosPotion()
	{
		for (int i = 0; i < LuckyScreen.mListItemLucky.size(); i++)
		{
			MainItem mainItem = (MainItem)LuckyScreen.mListItemLucky.elementAt(i);
			if (i < 16)
			{
				mainItem.x = xQuay + CRes.getcos(CRes.fixangle(i * 360 / 16)) * 66 / 1000;
				mainItem.y = yQuay + CRes.getsin(CRes.fixangle(i * 360 / 16)) * 66 / 1000;
			}
			else
			{
				mainItem.x = xQuay + 1 + CRes.getcos(CRes.fixangle(30 + i * 360 / 6)) * 32 / 1000;
				mainItem.y = yQuay + CRes.getsin(CRes.fixangle(30 + i * 360 / 6)) * 32 / 1000;
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
				LuckyScreen.mImgVongquay[i] = mImage.createImage("/interface/lucky" + i + ".png");
			}
		}
		if (fraImgBuy == null)
		{
			fraImgBuy = new FrameImage(mImage.createImage("/interface/oc_sen.png"), 28, 27);
			fraImg1Lan = new FrameImage(mImage.createImage("/interface/lucky6.png"), 40, 42);
			fraImg5Lan = new FrameImage(mImage.createImage("/interface/lucky7.png"), 40, 42);
		}
		_ = imgPhatSang;
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		if (!GameCanvas.isSmallScreen)
		{
			AvMain.paintBG_AChi(g, x, y, w, h, 0);
		}
		AvMain.FontBorderColor(g, title, MotherCanvas.hw, y - 20, 2, 6, 5);
		paintVongQuay(g);
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
		MainTab.paintMoney(g, MotherCanvas.w - 78, 4 + GameScreen.h12plus, isClan: false);
		paintEff(g, 0);
	}

	public override void paintVongQuay(mGraphics g)
	{
		if (LuckyScreen.mImgVongquay == null)
		{
			loadImgVongQuay();
			return;
		}
		if (imgOcSen == null)
		{
			MainImage imageAll = ObjectData.getImageAll(800, ObjectData.HashImageOtherNew, 23000);
			if (imageAll != null)
			{
				imgOcSen = imageAll.img;
			}
		}
		if (imgOcSen != null)
		{
			g.drawImage(imgOcSen, xButton, yButton, 3);
		}
		AvMain.FontBorderSmall(g, string.Empty + updateNumve(), xButton + 1, yButton + 13, 2, 5);
		if ((StepQuaySo == 3 || StepQuaySo == 4) && LuckyScreen.mListItemLucky != null)
		{
			for (int i = -1; i < 2; i++)
			{
				int num = indexShowPotion + i;
				int num2 = LuckyScreen.mListItemLucky.size();
				if (num < 0)
				{
					num = num2 + num;
				}
				else if (num >= num2)
				{
					num -= num2;
				}
				((MainItem)LuckyScreen.mListItemLucky.elementAt(num)).paintAllItem(g, xQuay, yQuay - i * 14, 24, 0, 0);
			}
		}
		int num3 = 0;
		int num4 = 0;
		if (StepQuaySo == 5)
		{
			num3 = mPlayVongQuayTo[tickVongQuay];
			num4 = mPlayVongQuayNho[tickVongQuay];
		}
		g.drawRegion(LuckyScreen.mImgVongquay[0], num3 * 95, 0, 95, 190, 0, xQuay - 94, yQuay - 94, 0);
		g.drawRegion(LuckyScreen.mImgVongquay[0], num3 * 95, 0, 95, 190, 2, xQuay, yQuay - 94, 0);
		g.drawRegion(LuckyScreen.mImgVongquay[1], 0, num4 * 99, 99, 99, 0, xQuay, yQuay, 3);
		if (StepQuaySo == 1 || StepQuaySo == 0)
		{
			g.drawRegion(LuckyScreen.mImgVongquay[4], 0, 33, 33, 33, 0, xQuay, yQuay, 3);
		}
		else if (StepQuaySo == 2)
		{
			int num5 = yQuay;
			if (tickAction > 23)
			{
				g.drawRegion(LuckyScreen.mImgVongquay[4], 0, 33, 33, 33, 0, xQuay, yQuay + 1, 3);
				g.drawRegion(LuckyScreen.mImgVongquay[4], 0, 0, 33, 33, 0, xQuay, yQuay, 3);
			}
			else
			{
				g.drawRegion(LuckyScreen.mImgVongquay[2], 0, mPlayVongTrungTam[tickVongQuay] * 33, 33, 33, 0, xQuay, num5, 3);
			}
		}
		else
		{
			g.drawRegion(LuckyScreen.mImgVongquay[4], 0, 0, 33, 33, 0, xQuay, yQuay, 3);
		}
		if (StepQuaySo != 5)
		{
			for (int j = 0; j < LuckyScreen.mListItemLucky.size(); j++)
			{
				MainItem mainItem = (MainItem)LuckyScreen.mListItemLucky.elementAt(j);
				if (j < 16 && j % 2 != 0)
				{
					AvMain.fraBorderSkill.drawFrame(0, mainItem.x, mainItem.y, 0, 3, g);
				}
				if (j == indexShowPotion)
				{
					if (imgPhatSang == null)
					{
						MainImage imageAll2 = ObjectData.getImageAll(801, ObjectData.HashImageOtherNew, 23000);
						if (imageAll2 != null)
						{
							imgPhatSang = imageAll2.img;
						}
					}
					if (imgPhatSang != null)
					{
						g.drawRegion(imgPhatSang, 0, 0, 27, 27, 0, mainItem.x, mainItem.y, 3);
					}
				}
				if (mNhan != null && mNhan[j] == 1)
				{
					g.drawRegion(LuckyScreen.mImgVongquay[3], 0, 0, 33, 33, 0, mainItem.x, mainItem.y, 3);
				}
				else
				{
					mainItem.paintAllItem(g, mainItem.x, mainItem.y, 24, 0, 0);
				}
			}
		}
		if (StepQuaySo != 5)
		{
			return;
		}
		g.drawRegion(LuckyScreen.mImgVongquay[2], 0, mPlayVongTrungTam[tickVongQuay] * 33, 33, 33, 0, xQuay, yQuay, 3);
		if (tickVongQuay % 2 == 0)
		{
			if (imgPhatSang == null)
			{
				MainImage imageAll3 = ObjectData.getImageAll(801, ObjectData.HashImageOtherNew, 23000);
				if (imageAll3 != null)
				{
					imgPhatSang = imageAll3.img;
				}
			}
			if (imgPhatSang != null)
			{
				g.drawImage(imgPhatSang, xQuay, yQuay, 3);
			}
		}
		((MainItem)LuckyScreen.mListItemLucky.elementAt(indexShowPotion)).paintAllItem(g, xQuay, yQuay, 24, 0, 0);
	}

	public override void UpdateStepQuaySo()
	{
		tickAction++;
		if (StepQuaySo == 1)
		{
			if (countRemainGift < 1)
			{
				StepQuaySo = 4;
				tickAction = 20;
				return;
			}
			if (tickAction == 5)
			{
				if (typeQuay == 4)
				{
					vecEff.addElement(GameScreen.createEffEnd(78, 1, cmdQuayOcSen.xCmd, cmdQuayOcSen.yCmd, xQuay - 5, yQuay - 5));
				}
				else
				{
					vecEff.addElement(GameScreen.createEffEnd(78, 2, cmdQuay.xCmd, cmdQuay.yCmd, xQuay - 5, yQuay - 5));
				}
			}
			if (tickAction == 33)
			{
				StepQuaySo = 2;
				tickAction = -10;
				tickVongQuay = 0;
				vecEff.addElement(GameScreen.createEffEnd(53, 0, xQuay, yQuay, xQuay, yQuay));
			}
			if (tickAction >= 100)
			{
				StepQuaySo = 2;
				tickAction = -10;
				tickVongQuay = 0;
				vecEff.addElement(GameScreen.createEffEnd(53, 0, xQuay, yQuay, xQuay, yQuay));
			}
		}
		else if (StepQuaySo == 2)
		{
			if (tickAction >= 0)
			{
				tickVongQuay++;
			}
			if (tickVongQuay >= mPlayVongTrungTam.Length)
			{
				tickVongQuay = mPlayVongTrungTam.Length - 1;
			}
			if (tickAction >= 30)
			{
				StepQuaySo = 3;
				tickAction = -5;
				tickVongQuay = 0;
			}
		}
		else if (StepQuaySo == 3)
		{
			if (tickAction >= 0)
			{
				int num = indexShowPotion;
				if (tickAction < 12)
				{
					if (tickAction % 6 == 0)
					{
						IncreaseIndexShowPotion();
					}
				}
				else if (tickAction < 20)
				{
					if (tickAction % 4 == 0)
					{
						IncreaseIndexShowPotion();
					}
				}
				else if (tickAction % 3 == 0)
				{
					IncreaseIndexShowPotion();
				}
				if (indexShowPotion != num)
				{
					mSound.playSound(51, mSound.volumeSound);
				}
			}
			if (tickVongQuay % 15 == 0)
			{
				mSound.playSound(49, mSound.volumeSound);
			}
			if (tickAction >= 100)
			{
				StepQuaySo = 4;
				tickAction = 0;
			}
		}
		else if (StepQuaySo == 4)
		{
			if (tickAction >= 0)
			{
				int num2 = indexShowPotion;
				if (tickAction < 10)
				{
					if (tickAction % 4 == 0)
					{
						IncreaseIndexShowPotion();
					}
				}
				else if (tickAction % 6 == 0)
				{
					IncreaseIndexShowPotion();
				}
				if (indexShowPotion != num2)
				{
					mSound.playSound(51, mSound.volumeSound);
				}
			}
			if (tickAction > 15 && indexShowPotion == indexQuaySo)
			{
				MainItem mainItem = (MainItem)LuckyScreen.mListItemLucky.elementAt(indexQuaySo);
				vecEff.addElement(GameScreen.createEffEnd(53, 0, mainItem.x, mainItem.y, mainItem.x, mainItem.y));
				StepQuaySo = 5;
				vecEff.addElement(GameScreen.createEffEnd(78, 1, mainItem.x, mainItem.y, xQuay, yQuay));
				tickAction = 0;
			}
			if (tickVongQuay % 15 == 0)
			{
				mSound.playSound(49, mSound.volumeSound);
			}
		}
		else if (StepQuaySo == 5)
		{
			if (tickAction >= 0 && tickAction % 4 == 0)
			{
				tickVongQuay++;
			}
			if (tickVongQuay >= mPlayVongQuayTo.Length)
			{
				tickVongQuay = mPlayVongQuayTo.Length - 1;
			}
			if (tickAction == 24)
			{
				vecEff.addElement(GameScreen.createEffEnd(53, 0, xQuay, yQuay, xQuay, yQuay));
			}
			if (tickAction == 34)
			{
				MainItem mainItem2 = (MainItem)LuckyScreen.mListItemLucky.elementAt(indexQuaySo);
				MainImage image = mainItem2.getImage();
				if (image != null && image.img != null)
				{
					if (image.frame == -1)
					{
						image.set_Frame();
					}
					addEffectNumImage(" x " + Interface_Game.ConvertNumToStr(mainItem2.numInt), xQuay, yQuay, 3, new FrameImage(image.img, image.frame), 0);
				}
			}
			if (tickAction == 70)
			{
				StepQuaySo = 0;
				tickAction = 0;
				if (indexQuaySo >= 0 && indexQuaySo < mNhan.Length)
				{
					mNhan[indexQuaySo] = 1;
				}
				if (countRemainGift > 0)
				{
					IncreaseIndexShowPotion();
				}
			}
		}
		if (LuckyScreen.mListItemLucky != null)
		{
			if (indexShowPotion >= LuckyScreen.mListItemLucky.size())
			{
				indexShowPotion = 0;
			}
			if (indexShowPotion < 0)
			{
				indexShowPotion = LuckyScreen.mListItemLucky.size() - 1;
			}
		}
	}

	public void SetmNhan(sbyte[] m)
	{
		mNhan = m;
		for (int i = 0; i < mNhan.Length; i++)
		{
			if (mNhan[i] == 0)
			{
				countRemainGift++;
			}
		}
	}

	public void SetIndexQuaySo(sbyte index)
	{
		if (indexQuaySo != index)
		{
			indexQuaySo = index;
			countRemainGift--;
			mSound.playSound(48, mSound.volumeSound);
			StepQuaySo = 1;
			tickAction = 0;
		}
	}

	private void IncreaseIndexShowPotion()
	{
		do
		{
			indexShowPotion += (isClockwise ? 1 : (-1));
			if (indexShowPotion >= LuckyScreen.mListItemLucky.size())
			{
				indexShowPotion = 0;
			}
			if (indexShowPotion < 0)
			{
				indexShowPotion = LuckyScreen.mListItemLucky.size() - 1;
			}
		}
		while (mNhan[indexShowPotion] == 1);
	}

	public override int updateNumve()
	{
		if (vequay == null || LuckyScreen.isUpdateVe)
		{
			for (int i = 0; i < Player.vecInventory.size(); i++)
			{
				MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
				if (mainItem.typeObject == 4 && mainItem.ID == 441)
				{
					vequay = mainItem;
					LuckyScreen.isUpdateVe = false;
					break;
				}
			}
		}
		if (vequay != null)
		{
			return vequay.numPotion;
		}
		return 0;
	}
}
