public class LuckyScreen : MainScreen
{
	public const sbyte STEP_WAIT_BEGIN = 0;

	public const sbyte STEP_BEGIN = 1;

	public const sbyte STEP_WAIT_QUAY = 2;

	public const sbyte STEP_QUAY = 3;

	public const sbyte STEP_KETQUA = 4;

	public const sbyte TYPE_1 = 0;

	public const sbyte TYPE_3 = 1;

	public const short ID_TICKET = 0;

	public static LuckyScreen instance;

	public mVector vecCmd = new mVector();

	public mVector vecEff = new mVector("LuckyScreen.vecEff");

	protected int x;

	protected int y;

	protected int w;

	protected int h;

	protected int idSelect = -1;

	protected int tickVongQuay;

	protected int xQuay;

	protected int yQuay;

	protected int xButton;

	protected int yButton;

	protected int wButton;

	protected int indexShowPotion;

	public int tickAction;

	public int typeQuay;

	public int StepQuaySo;

	protected iCommand cmdClose;

	protected iCommand cmdQuay;

	protected iCommand cmdQuay3Lan;

	protected iCommand cmdBuyTicket;

	protected FrameImage fraImgBuy;

	protected FrameImage fraImg1Lan;

	protected FrameImage fraImg5Lan;

	private MainItem poi;

	public static mImage[] mImgVongquay;

	protected int[] mPlayVongTrungTam = new int[38]
	{
		0, 0, 0, 0, 0, 1, 1, 0, 0, 0,
		0, 0, 1, 1, 0, 0, 0, 1, 1, 1,
		0, 0, 1, 1, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0
	};

	protected int[] mPlayVongQuayTo = new int[139]
	{
		0, 0, 0, 1, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0
	};

	protected int[] mPlayVongQuayNho = new int[186]
	{
		0, 0, 0, 1, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1
	};

	public static mVector mListItemLucky;

	public int indexOffPaint;

	private MainItem vequay;

	public static bool isUpdateVe;

	public Item_Drop[] mItemShow;

	public LuckyScreen()
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
		mImgVongquay = null;
		loadImgVongQuay();
		cmdClose = new iCommand(T.close, -1, this);
		cmdBuyTicket = new iCommand(string.Empty, 1, this);
		cmdQuay = new iCommand(string.Empty, 2, 1, this);
		cmdQuay3Lan = new iCommand(string.Empty, 2, 2, this);
		cmdBuyTicket.setPos(xButton, yButton + wButton + 5, fraImgBuy, string.Empty);
		cmdQuay.setPos(xButton, yButton + wButton * 2, fraImg1Lan, string.Empty);
		cmdQuay3Lan.setPos(xButton, yButton + wButton * 3, fraImg5Lan, string.Empty);
		vecCmd = new mVector();
		vecCmd.addElement(cmdBuyTicket);
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
			GlobalService.gI().Quayso(4);
			break;
		case 2:
			GlobalService.gI().Quayso((sbyte)subIndex);
			break;
		case 0:
			break;
		}
	}

	public void setPotion()
	{
		poi = null;
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem.typeObject == 4 && mainItem.ID == 0)
			{
				poi = mainItem;
				break;
			}
		}
	}

	public virtual void setPosPotion()
	{
		for (int i = 0; i < mListItemLucky.size(); i++)
		{
			MainItem mainItem = (MainItem)mListItemLucky.elementAt(i);
			if (i < 8)
			{
				mainItem.x = xQuay + CRes.getcos(CRes.fixangle(i * 360 / 8)) * 66 / 1000;
				mainItem.y = yQuay + CRes.getsin(CRes.fixangle(i * 360 / 8)) * 66 / 1000;
			}
			else
			{
				mainItem.x = xQuay + 1 + CRes.getcos(CRes.fixangle(30 + i * 360 / 6)) * 32 / 1000;
				mainItem.y = yQuay + CRes.getsin(CRes.fixangle(30 + i * 360 / 6)) * 32 / 1000;
			}
		}
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
			AvMain.paintBG_Wanted_Room(g, x, y, w, h);
		}
		paintVongQuay(g);
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
		paintEff(g, 0);
	}

	public virtual void paintVongQuay(mGraphics g)
	{
		if (mImgVongquay == null)
		{
			loadImgVongQuay();
			return;
		}
		g.drawImage(mImgVongquay[5], xButton, yButton, 3);
		AvMain.FontBorderSmall(g, string.Empty + updateNumve(), xButton, yButton + 12, 2, 5);
		if (StepQuaySo == 3 && mListItemLucky != null)
		{
			for (int i = -1; i < 2; i++)
			{
				int num = indexShowPotion + i;
				int num2 = mListItemLucky.size();
				if (num < 0)
				{
					num = num2 + num;
				}
				else if (num >= num2)
				{
					num -= num2;
				}
				((MainItem)mListItemLucky.elementAt(num)).paintAllItem(g, xQuay, yQuay - i * 14, 24, 0, 0);
			}
		}
		int num3 = 0;
		int num4 = 0;
		if (StepQuaySo == 3)
		{
			num3 = mPlayVongQuayTo[tickVongQuay];
			num4 = mPlayVongQuayNho[tickVongQuay];
		}
		g.drawRegion(mImgVongquay[0], num3 * 95, 0, 95, 190, 0, xQuay - 94, yQuay - 94, 0);
		g.drawRegion(mImgVongquay[0], num3 * 95, 0, 95, 190, 2, xQuay, yQuay - 94, 0);
		g.drawRegion(mImgVongquay[1], 0, num4 * 99, 99, 99, 0, xQuay, yQuay, 3);
		if (StepQuaySo == 1 || StepQuaySo == 0)
		{
			g.drawRegion(mImgVongquay[3], 0, 0, 33, 33, 0, xQuay, yQuay, 3);
		}
		else if (StepQuaySo == 2)
		{
			int num5 = yQuay;
			if (tickAction > 23)
			{
				g.drawRegion(mImgVongquay[4], 0, 33, 33, 33, 0, xQuay, yQuay + 1, 3);
				g.drawRegion(mImgVongquay[4], 0, 0, 33, 33, 0, xQuay, yQuay, 3);
			}
			else
			{
				g.drawRegion(mImgVongquay[2], 0, mPlayVongTrungTam[tickVongQuay] * 33, 33, 33, 0, xQuay, num5, 3);
			}
		}
		else
		{
			g.drawRegion(mImgVongquay[4], 0, 0, 33, 33, 0, xQuay, yQuay, 3);
		}
		if (StepQuaySo != 2 && StepQuaySo != 0 && StepQuaySo != 1)
		{
			return;
		}
		for (int j = 0; j < mListItemLucky.size(); j++)
		{
			MainItem mainItem = (MainItem)mListItemLucky.elementAt(j);
			if (j >= indexOffPaint)
			{
				mainItem.paintAllItem(g, mainItem.x, mainItem.y, 24, 0, 0);
			}
		}
	}

	public void paintEff(mGraphics g, int level)
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			if (level == 0 && mainEffect.levelPaint > -1)
			{
				mainEffect.paint(g);
			}
			else if (mainEffect.levelPaint == -1)
			{
				mainEffect.paint(g);
			}
		}
	}

	public virtual void loadImgVongQuay()
	{
		if (mImgVongquay == null)
		{
			mImgVongquay = new mImage[6];
			for (int i = 0; i < mImgVongquay.Length; i++)
			{
				mImgVongquay[i] = mImage.createImage("/interface/lucky" + i + ".png");
			}
		}
		if (fraImgBuy == null)
		{
			fraImgBuy = new FrameImage(mImage.createImage("/interface/lucky8.png"), 37, 27);
			fraImg1Lan = new FrameImage(mImage.createImage("/interface/lucky6.png"), 40, 42);
			fraImg5Lan = new FrameImage(mImage.createImage("/interface/lucky7.png"), 40, 42);
		}
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			mainEffect.update();
			if (mainEffect.isStop)
			{
				vecEff.removeElement(mainEffect);
				i--;
			}
		}
		UpdateStepQuaySo();
	}

	public virtual void UpdateStepQuaySo()
	{
		if (ModMenu.isAutoQuaySo)
		{
			StepQuaySo = 0;
			tickAction = 0;
			if (GameCanvas.currentDialog is MsgShowGift)
			{
				GameCanvas.end_Dialog();
			}
			if (GameCanvas.subDialog is MsgShowGift)
			{
				GameCanvas.subDialog = null;
			}
			return;
		}
		tickAction++;
		if (StepQuaySo == 1)
		{
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
			if (tickAction >= 30)
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
			if (mListItemLucky != null && indexShowPotion >= mListItemLucky.size())
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
		else if (StepQuaySo == 4)
		{
			indexOffPaint = 0;
			if (tickAction >= 5)
			{
				StepQuaySo = 0;
				tickAction = 0;
			}
			MsgShowGift msgShowGift = new MsgShowGift();
			msgShowGift.setinfoShow_Gift(20, T.quaySo, string.Empty, mItemShow, -1);
			GameCanvas.Start_Current_Dialog(msgShowGift);
		}
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(1))
		{
			if (idSelect > 0)
			{
				idSelect--;
			}
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			if (idSelect < 2)
			{
				idSelect++;
			}
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (GameCanvas.keyMyHold[5] && idSelect != -1)
		{
			GameCanvas.clearKeyHold(5);
			((iCommand)vecCmd.elementAt(idSelect)).perform();
		}
		if (flag)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				if (i == idSelect)
				{
					iCommand2.isPlayframe = true;
				}
				else
				{
					iCommand2.isPlayframe = false;
				}
			}
		}
		base.updatekey();
	}

	public override void updatePointer()
	{
		if (GameCanvas.isPointerSelect && HuyHieuClanScreen.isRunningOpenXu)
		{
			HuyHieuClanScreen.instance.updatePointerOpenXu();
		}
		if (StepQuaySo != 0)
		{
			return;
		}
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				((iCommand)vecCmd.elementAt(i)).updatePointer();
			}
		}
		base.updatePointer();
	}

	public virtual int updateNumve()
	{
		if (vequay == null || isUpdateVe)
		{
			for (int i = 0; i < Player.vecInventory.size(); i++)
			{
				MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
				if (mainItem.typeObject == 4 && mainItem.ID == 232)
				{
					vequay = mainItem;
					isUpdateVe = false;
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

	public void dataDialog(Item_Drop[] itemLucky)
	{
		mSystem.outz("itemLucky " + itemLucky.Length);
		mItemShow = null;
		mItemShow = itemLucky;
	}

	public void addEffectNumImage(string content, int x, int y, sbyte typeColor, FrameImage fra, int frame)
	{
		EffectNum effectNum = new EffectNum(content, x, y, typeColor, fra, frame);
		int num = GameScreen.find_Index_Stop(vecEff);
		if (num == vecEff.size())
		{
			vecEff.addElement(effectNum);
		}
		else
		{
			vecEff.setElementAt(effectNum, num);
		}
	}
}
