public class HuyHieuClanScreen : LuckyScreen
{
	public new static HuyHieuClanScreen instance;

	private FrameImage fraRuong;

	private static mImage[] mImgHuyHieu;

	public MainItem potionQuay;

	public Potion potionNhan;

	public bool isThanhCong;

	private MainImage imgNhan;

	private FrameImage fraNhan;

	private int tickRuong;

	public HuyHieuClanScreen()
	{
		StepQuaySo = 0;
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
		loadImgVongQuay();
		cmdClose = new iCommand(T.close, -1, this);
		cmdQuay = new iCommand(string.Empty, 1, this);
		cmdQuay.setPos(xButton + 1, yButton + wButton * 5 / 2, fraImg1Lan, string.Empty);
		vecCmd = new mVector();
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
			GlobalService.gI().Huy_hieu(3, 1, potionQuay.ID);
			break;
		}
	}

	public override void loadImgVongQuay()
	{
		if (mImgHuyHieu == null)
		{
			mImgHuyHieu = new mImage[3];
			for (int i = 0; i < mImgHuyHieu.Length; i++)
			{
				mImgHuyHieu[i] = mImage.createImage("/interface/huyhieu" + i + ".png");
			}
		}
		fraImg1Lan = new FrameImage(mImage.createImage("/interface/lucky6.png"), 40, 42);
		fraRuong = new FrameImage(mImage.createImage("/interface/huyhieu3.png"), 12);
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		AvMain.paintBG_AChi(g, x, y, w, h, 0);
		AvMain.FontBorderColor(g, T.huyHieuHanhTrinh, MotherCanvas.hw, y - 20, 2, 6, 5);
		paintVongQuay(g);
		int num = 5;
		int num2 = yButton + wButton * 3 / 2;
		AvMain.paintRect(g, xButton - 16, num2 - 16, 32, 32, 1, 4);
		if (potionQuay != null)
		{
			potionQuay.paintQuay(g, xButton, num2, 32);
		}
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
		if (mImgHuyHieu == null)
		{
			loadImgVongQuay();
			return;
		}
		g.drawImage(mImgHuyHieu[2], xButton, yButton, 3);
		mSystem.outz("numpotion quay " + potionQuay.numPotion);
		AvMain.FontBorderSmall(g, string.Empty + potionQuay.numPotion, xButton + 1, yButton + 13, 2, 5);
		mFont.tahoma_7b_black.drawString(g, string.Empty + 0, xButton + 1, yButton + 23, 2);
		int num = 0;
		int num2 = 0;
		if (StepQuaySo == 3)
		{
			num = mPlayVongQuayTo[tickVongQuay];
			num2 = mPlayVongQuayNho[tickVongQuay];
		}
		g.drawRegion(mImgHuyHieu[0], num * 90, 0, 90, 180, 0, xQuay - 89, yQuay - 89, 0);
		g.drawRegion(mImgHuyHieu[0], num * 90, 0, 90, 180, 2, xQuay, yQuay - 89, 0);
		g.drawRegion(mImgHuyHieu[1], 0, num2 * 105, 105, 105, 0, xQuay, yQuay, 3);
		fraRuong.drawFrame(tickRuong, xQuay + 3, yQuay - 8, 0, 3, g);
	}

	public override void UpdateStepQuaySo()
	{
		if (potionNhan != null)
		{
			imgNhan = Potion.getIconClan(potionNhan.idIcon);
		}
		if (imgNhan != null && imgNhan.img != null)
		{
			int imageWidth = mImage.getImageWidth(imgNhan.img.image);
			int imageHeight = mImage.getImageHeight(imgNhan.img.image);
			if (imageHeight / 2 >= imageWidth)
			{
				fraNhan = new FrameImage(imgNhan.img, imageWidth, imageWidth);
			}
		}
		tickAction++;
		if (StepQuaySo == 0)
		{
			tickRuong = tickAction / 4 % 6;
		}
		else if (StepQuaySo == 1)
		{
			tickRuong = 0;
			if (tickAction == 5)
			{
				if (typeQuay == 0)
				{
					vecEff.addElement(GameScreen.createEffEnd(78, 1, cmdQuay.xCmd, cmdQuay.yCmd, xQuay - 5, yQuay - 5));
				}
				else
				{
					vecEff.addElement(GameScreen.createEffEnd(78, 2, cmdQuay3Lan.xCmd, cmdQuay3Lan.yCmd, xQuay - 5, yQuay - 5));
				}
			}
			if (tickAction == 33)
			{
				StepQuaySo = 2;
				tickAction = -10;
				tickVongQuay = 0;
				vecEff.addElement(GameScreen.createEffEnd(53, 0, xQuay, yQuay, xQuay, yQuay));
				indexOffPaint = 0;
			}
			if (tickAction >= 100)
			{
				StepQuaySo = 2;
				tickAction = -10;
				tickVongQuay = 0;
				vecEff.addElement(GameScreen.createEffEnd(53, 0, xQuay, yQuay, xQuay, yQuay));
				indexOffPaint = 0;
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
			if (tickAction >= 10)
			{
				StepQuaySo = 3;
				tickAction = -5;
				tickVongQuay = 0;
			}
			if (tickAction < 0)
			{
				return;
			}
			int num = indexOffPaint;
			if (tickAction < 6)
			{
				if (tickAction % 5 == 0)
				{
					indexOffPaint++;
				}
			}
			else if (tickAction < 16)
			{
				if (tickAction % 3 == 0)
				{
					indexOffPaint++;
				}
			}
			else if (tickAction % 2 == 0)
			{
				indexOffPaint++;
			}
			if (indexOffPaint != num)
			{
				mSound.playSound(51, mSound.volumeSound);
			}
		}
		else if (StepQuaySo == 3)
		{
			if (tickVongQuay % 15 == 0)
			{
				mSound.playSound(49, mSound.volumeSound);
			}
			if (GameCanvas.gameTick % 2 == 0)
			{
				indexShowPotion++;
			}
			if (LuckyScreen.mListItemLucky != null && indexShowPotion >= LuckyScreen.mListItemLucky.size())
			{
				indexShowPotion = 0;
			}
			if (tickAction >= 0)
			{
				tickVongQuay++;
			}
			if (tickVongQuay >= mPlayVongQuayTo.Length)
			{
				tickVongQuay = mPlayVongQuayTo.Length - 1;
			}
			if (tickAction >= 100)
			{
				StepQuaySo = 4;
				tickAction = 0;
			}
		}
		else
		{
			if (StepQuaySo != 4)
			{
				return;
			}
			indexOffPaint = 0;
			if (tickAction < 36)
			{
				tickRuong = tickAction / 3;
			}
			if (tickAction == 36)
			{
				Interface_Game.isPaintInfoServer = true;
				if (!isThanhCong)
				{
					mSound.playSound(29, mSound.volumeSound);
					int subtype = 1;
					if (GameCanvas.language == 1)
					{
						subtype = 3;
					}
					int num2 = 10;
					createEff(79, subtype, xQuay + 3, yQuay - 15 + num2, xQuay + 3, yQuay - 15 + num2);
					createEff(77, 0, xQuay + 3, yQuay - 15 + num2, xQuay + 3, yQuay - 15 + num2);
				}
				else
				{
					mSystem.outz("potionNhan cat " + potionNhan.typeObject + " icon " + potionNhan.idIcon);
					createEff(53, 0, xQuay + 3, yQuay - 15, xQuay + 3, yQuay - 15);
					addEffectNumImage(string.Empty, xQuay + 3, yQuay - 15, 3, fraNhan, 0);
					isThanhCong = false;
				}
			}
			if (tickAction == 100)
			{
				StepQuaySo = 0;
				tickAction = 0;
			}
		}
	}

	public void createEff(short type, int subtype, int x, int y, int xto, int yto)
	{
		Effect_End o = new Effect_End(type, (sbyte)subtype, x, y, xto, yto, 0, null);
		vecEff.addElement(o);
	}
}
