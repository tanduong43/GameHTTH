public class MsgUpdateHeart : ScreenUpgrade
{
	public new static MsgUpdateHeart instance;

	public static bool isClickUpgradeHeartAuto;

	public static bool isContinueUpgradeHeart;

	public static bool isUpgradeHeartSuccess;

	public long timeStartUpgradeHeartAuto;

	public int xCheckBoxHeart;

	public int yCheckBoxHeart;

	public int wCheckBoxHeart;

	public int hCheckBoxHeart;

	private bool hasCreatedEndEff;

	private string text = string.Empty;

	private MainItem itemHeart;

	private int ylech;

	private new iCommand cmdUpgrade;

	private int[] mplayFrameNormal = new int[21]
	{
		0, 0, 0, 0, 0, 0, 0, 1, 1, 1,
		1, 1, 1, 1, 2, 2, 2, 2, 2, 2,
		2
	};

	private int[] mplayFrameUpgrade = new int[178]
	{
		0, 0, 0, 0, 0, 0, 0, 1, 1, 1,
		1, 1, 1, 1, 2, 2, 2, 2, 2, 2,
		2, 0, 0, 0, 0, 0, 0, 0, 1, 1,
		1, 1, 1, 1, 1, 2, 2, 2, 2, 2,
		2, 2, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 1, 1, 1, 1, 1, 2, 2, 2, 2,
		2, 2, 2, 1, 1, 1, 1, 1, 2, 2,
		2, 2, 0, 0, 0, 0, 1, 1, 1, 2,
		2, 2, 0, 0, 1, 1, 2, 2, 0, 1,
		2, 0, 1, 2, 0, 1, 2, 0, 1, 2,
		0, 1, 2, 0, 1, 2, 0, 1, 2, 0,
		1, 2, 0, 1, 2, 0, 1, 2, 0, 1,
		2, 0, 1, 2, 0, 1, 2, 0, 1, 2,
		0, 1, 2, 0, 1, 2, 0, 1, 2, 0,
		1, 2, 0, 1, 2, 0, 1, 2, 0, 1,
		2, 0, 1, 2, 0, 1, 2, 0, 0, 0,
		0, 0, 0, 0, 1, 1, 1, 1, 1, 1,
		1, 2, 2, 2, 2, 2, 2, 2
	};

	public const sbyte STEP_NORMAL = 0;

	public const sbyte STEP_UPGRADE = 1;

	public const sbyte STEP_WAIT_UPGRADE = 2;

	private int stepUpgrade;

	private int playHeart;

	private int play1_0;

	private int play1_1;

	private int play1_2;

	private int play1_3;

	private int play2;

	private int play3_0;

	private int play3_1;

	private int play4_0;

	private int play4_1;

	private int play4_2;

	private int play4_3;

	public MsgUpdateHeart()
	{
		w = 150;
		h = 190 + GameCanvas.hCommand;
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2 - 5;
		ylech = 10;
		if (!GameCanvas.isTouch)
		{
			h -= 30;
			ylech += 12;
		}
		vecCmd = new mVector();
		cmdClose = new iCommand(T.close, 0, this);
		cmdUpgrade = new iCommand(T.mo, 1, this);
		vecCmd.addElement(cmdClose);
		vecCmd.addElement(cmdUpgrade);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x + w / 2 + 60, y + 20, MainTab.fraCloseTab3, string.Empty);
			cmdUpgrade.setPos(x + w / 2, y + h - GameCanvas.hCommand + 5, MainTab.fraCmdMo, string.Empty);
			backCMD = cmdClose;
			okCMD = cmdUpgrade;
		}
		else
		{
			cmdClose = AvMain.setPosCMD(cmdClose, 2);
			cmdUpgrade = AvMain.setPosCMD(cmdUpgrade, 0);
			right = cmdClose;
			center = cmdUpgrade;
		}
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + 7);
		if (mainItem != null && mainItem.typeSpec == 1)
		{
			itemHeart = mainItem;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 1:
			if (canUpgradeHeart())
			{
				GlobalService.gI().Upgrade_Item(15, -1, 0);
				stepUpgrade = 2;
				tickStep = 0;
			}
			else
			{
				stopUpgradeHeartAuto();
			}
			break;
		case 0:
			stopUpgradeHeartAuto();
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		Interface_Game.paintInfoPlayer_Short(g, MotherCanvas.hw - 160, 3 + GameScreen.h12plus, isborder: true, mFont.tahoma_7_black);
		paintPaper(g, MotherCanvas.hw - w / 2, y, w, h, w, AvMain.PAPER_LAW);
		paintInfoHeart(g);
		if (AvMain.imgEff_Law == null)
		{
			load_Img_Law();
		}
		g.drawImage(AvMain.imgEff_Law[7], x + w / 2, y + 20, 3);
		g.drawImage(AvMain.imgEff_Law[0], x + w / 2, y + h / 2 + ylech, 3);
		if (stepUpgrade == 0 || stepUpgrade == 2)
		{
			g.drawRegion(AvMain.imgEff_Law[6], 0, 50 * mplayFrameNormal[playHeart], 48, 50, 0, x + w / 2, y + h / 2 + ylech, 3);
		}
		else if (stepUpgrade == 1)
		{
			if (playHeart < 115 || GameCanvas.gameTick % 3 != 0)
			{
				if (play1_0 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[1], 0, 55 - play1_0, play1_0, play1_0, 0, x + w / 2 - 55, y + h / 2 + ylech - play1_0, 0);
				}
				if (play1_1 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[1], 55, 0, play1_1, play1_1, 0, x + w / 2, y + h / 2 + ylech - 55, 0);
				}
				if (play1_2 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[1], 110 - play1_2, 55, play1_2, play1_2, 0, x + w / 2 + 55 - play1_2, y + h / 2 + ylech, 0);
				}
				if (play1_3 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[1], 55 - play1_3, 110 - play1_3, play1_3, play1_3, 0, x + w / 2 - play1_3, y + h / 2 + ylech + 55 - play1_3, 0);
				}
				if (play2 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[2], 0, 0, 90, play2, 0, x + w / 2 - 45, y + h / 2 + ylech - 45, 0);
				}
				if (play3_0 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[3], 0, 0, play3_0, play3_0, 0, x + w / 2 - 32, y + h / 2 + ylech - 32, 0);
				}
				if (play3_1 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[3], 62, 0, 2, play3_1, 0, x + w / 2 + 32 - 2, y + h / 2 + ylech - 32, 0);
					g.drawRegion(AvMain.imgEff_Law[3], 0, 62, play3_1, 2, 0, x + w / 2 - 32, y + h / 2 + ylech + 32 - 2, 0);
				}
				if (play4_0 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[4], 32 - play4_0, 0, play4_0, play4_0, 0, x + w / 2 - play4_0, y + h / 2 + ylech - 32, 0);
				}
				if (play4_1 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[4], 0, 32, play4_1, play4_1, 0, x + w / 2 - 32, y + h / 2 + ylech, 0);
				}
				if (play4_2 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[4], 32, 64 - play4_2, play4_2, play4_2, 0, x + w / 2, y + h / 2 + ylech + 32 - play4_2, 0);
				}
				if (play4_3 > 0)
				{
					g.drawRegion(AvMain.imgEff_Law[4], 64 - play4_3, 32 - play4_3, play4_3, play4_3, 0, x + w / 2 + 32 - play4_3, y + h / 2 + ylech - play4_3, 0);
				}
			}
			g.drawRegion(AvMain.imgEff_Law[6], 0, 50 * mplayFrameUpgrade[playHeart], 48, 50, 0, x + w / 2, y + h / 2 + ylech, 3);
		}
		g.drawImage(AvMain.imgEff_Law[5], x + w / 2, y + h / 2 + ylech, 3);
		GameCanvas.resetTrans(g);
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			if (mainEffect.levelPaint != -1)
			{
				mainEffect.paint(g);
				mainEffect.paint(g, 0, 0);
			}
		}
		if (stepUpgrade == 0 && vecCmd != null)
		{
			xCheckBoxHeart = x + w / 2 - 22;
			yCheckBoxHeart = y + h - GameCanvas.hCommand - 20;
			g.drawImage(AvMain.imgBorderCombo, xCheckBoxHeart, yCheckBoxHeart, 3);
			mFont tahoma_7b_black = mFont.tahoma_7b_black;
			string text = "Auto";
			int height = tahoma_7b_black.getHeight();
			int width = tahoma_7b_black.getWidth(text);
			tahoma_7b_black.drawString(g, text, xCheckBoxHeart + 10, yCheckBoxHeart - 5, 0);
			wCheckBoxHeart = width + 20;
			hCheckBoxHeart = height;
			if (isClickUpgradeHeartAuto)
			{
				AvMain.fraCheck.drawFrame(2, xCheckBoxHeart, yCheckBoxHeart, 0, 3, g);
			}
			for (int j = 0; j < vecCmd.size(); j++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	private void paintInfoHeart(mGraphics g)
	{
		if (itemHeart != null)
		{
			mFont.tahoma_7b_white.drawString(g, T.cuonghoa + ": +" + itemHeart.LvUpgrade, x + 10, y + 33, 0);
			mFont.tahoma_7b_white.drawString(g, T.chetac + itemHeart.valueCheTac, x + 10, y + 33 + GameCanvas.hText, 0);
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
		if (stepUpgrade == 2)
		{
			tickStep++;
			if (tickStep >= 80)
			{
				stepUpgrade = 0;
			}
		}
		playHeart++;
		if (stepUpgrade == 0 || stepUpgrade == 2)
		{
			if (playHeart >= mplayFrameNormal.Length)
			{
				playHeart = 0;
			}
		}
		else
		{
			if (stepUpgrade != 1)
			{
				return;
			}
			if (isClickUpgradeHeartAuto)
			{
				playHeart += 2;
			}
			play1_0 = playHeart * 5;
			if (play1_0 > 55)
			{
				play1_0 = 55;
			}
			play1_1 = (playHeart - 11) * 5;
			if (play1_1 > 55)
			{
				play1_1 = 55;
			}
			play1_2 = (playHeart - 22) * 5;
			if (play1_2 > 55)
			{
				play1_2 = 55;
			}
			play1_3 = (playHeart - 33) * 5;
			if (play1_3 > 55)
			{
				play1_3 = 55;
			}
			play2 = (playHeart - 44) * 5;
			if (play2 > 90)
			{
				play2 = 90;
			}
			play3_0 = (playHeart - 59) * 5;
			if (play3_0 > 63)
			{
				play3_0 = 63;
			}
			play3_1 = (playHeart - 72) * 5;
			if (play3_1 > 64)
			{
				play3_1 = 64;
			}
			play4_0 = (playHeart - 85) * 5;
			if (play4_0 > 32)
			{
				play4_0 = 32;
			}
			play4_1 = (playHeart - 91) * 5;
			if (play4_1 > 32)
			{
				play4_1 = 32;
			}
			play4_2 = (playHeart - 97) * 5;
			if (play4_2 > 32)
			{
				play4_2 = 32;
			}
			play4_3 = (playHeart - 103) * 5;
			if (play4_3 > 32)
			{
				play4_3 = 32;
			}
			if (playHeart >= mplayFrameUpgrade.Length - 50 && playHeart <= mplayFrameUpgrade.Length - 20 && playHeart % 5 == 0)
			{
				mSound.playSound(7, mSound.volumeSound);
				createEff(10, 0, x + w / 2, y + h / 2 + 5, x + w / 2, y + h / 2 + 5);
			}
			if (!hasCreatedEndEff && playHeart >= mplayFrameUpgrade.Length - 20)
			{
				hasCreatedEndEff = true;
				if (Step == 16)
				{
					mSound.playSound(28, mSound.volumeSound);
					int subtype = 0;
					if (GameCanvas.language == 1)
					{
						subtype = 2;
					}
					createEff(79, subtype, x + w / 2, y + h / 2 + ylech, x + w / 2, y + h / 2 + ylech);
					createEff(76, 0, x + w / 2, y + h / 2 + ylech, x + w / 2, y + h / 2 + ylech);
					createEff(53, 0, x + w / 2, y + h / 2 + ylech, x + w / 2, y + h / 2 + ylech);
				}
				else if (Step == 17)
				{
					mSound.playSound(29, mSound.volumeSound);
					int subtype2 = 1;
					if (GameCanvas.language == 1)
					{
						subtype2 = 3;
					}
					createEff(79, subtype2, x + w / 2, y + h / 2 + ylech, x + w / 2, y + h / 2 + ylech);
					createEff(77, 0, x + w / 2, y + h / 2 + ylech, x + w / 2, y + h / 2 + ylech);
				}
			}
			if (playHeart >= mplayFrameUpgrade.Length)
			{
				playHeart = 0;
				play1_0 = 0;
				play1_1 = 0;
				play2 = 0;
				stepUpgrade = 0;
				hasCreatedEndEff = false;
				MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + 7);
				if (mainItem != null && mainItem.typeSpec == 1)
				{
					itemHeart = mainItem;
				}
				if (isClickUpgradeHeartAuto)
				{
					checkContinueOrStopHeartAuto();
				}
				else
				{
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(text);
				}
			}
		}
		if (isContinueUpgradeHeart && isClickUpgradeHeartAuto)
		{
			startUpgradeHeartAuto();
		}
	}

	public void updatePointerCheckBoxUpgradeHeart()
	{
		if (GameCanvas.isPoint(xCheckBoxHeart - 10, yCheckBoxHeart - 12, wCheckBoxHeart + 15, hCheckBoxHeart + 15))
		{
			isClickUpgradeHeartAuto = !isClickUpgradeHeartAuto;
			if (!isClickUpgradeHeartAuto)
			{
				stopUpgradeHeartAuto();
			}
			GameCanvas.isPointerSelect = false;
		}
	}

	public override void updatePointer()
	{
		if (stepUpgrade == 0 && GameCanvas.isPointerSelect)
		{
			updatePointerCheckBoxUpgradeHeart();
		}
		if (stepUpgrade == 0 && vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				((iCommand)vecCmd.elementAt(i)).updatePointer();
			}
		}
	}

	public override void updatekey()
	{
		if (stepUpgrade == 0)
		{
			updatekeySuper();
		}
	}

	public void updateStepUpgrade(sbyte type, string text)
	{
		if (type == 16 || type == 17)
		{
			playHeart = 0;
			play1_0 = 0;
			play1_1 = 0;
			play2 = 0;
			hasCreatedEndEff = false;
			stepUpgrade = 1;
			Step = type;
			isUpgradeHeartSuccess = type == 16;
		}
		this.text = text;
		if (cmdUpgrade != null)
		{
			cmdUpgrade.isSelect = false;
			cmdUpgrade.frameCmd = 0;
		}
	}

	public static long getBeriReq(int level)
	{
		if (level < 10)
		{
			return 1200000 + (long)level * 300000L;
		}
		if (level < 20)
		{
			return 4400000 + (long)(level - 10) * 500000L;
		}
		if (level < 50)
		{
			return 9900000 + (long)(level - 20) * 1000000L;
		}
		return 40400000 + (long)(level - 50) * 1500000L;
	}

	public bool canUpgradeHeart()
	{
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + 7);
		if (mainItem != null && mainItem.typeSpec == 1)
		{
			itemHeart = mainItem;
		}
		if (itemHeart == null)
		{
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog("Chưa trang bị tim!");
			return false;
		}
		if (itemHeart.LvUpgrade >= 99)
		{
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog("Tim đã đạt cấp tối đa (+99)!");
			return false;
		}
		long beriReq = getBeriReq(itemHeart.LvUpgrade);
		int num = ((itemHeart.LvUpgrade % 5 == 4) ? ((itemHeart.LvUpgrade / 5 + 1) * 10) : 0);
		if (GameScreen.player.beli < beriReq)
		{
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog("Không đủ " + AvMain.getMoneyShortText(beriReq) + " Beri để phẫu thuật tim!");
			return false;
		}
		if (GameScreen.player.gem < num)
		{
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog("Không đủ " + num + " Ruby để phẫu thuật tim!");
			return false;
		}
		return true;
	}

	public void startUpgradeHeartAuto()
	{
		long num = mSystem.currentTimeMillis();
		int num2 = (int)(800.0 / (1.0 + (double)GameCanvas.percent / 100.0));
		if (num - timeStartUpgradeHeartAuto >= num2)
		{
			if (canUpgradeHeart())
			{
				GlobalService.gI().Upgrade_Item(15, -1, 0);
				stepUpgrade = 2;
				tickStep = 0;
				isContinueUpgradeHeart = false;
			}
			else
			{
				stopUpgradeHeartAuto();
			}
		}
	}

	public static void stopUpgradeHeartAuto()
	{
		isClickUpgradeHeartAuto = false;
		isContinueUpgradeHeart = false;
	}

	public void checkContinueOrStopHeartAuto()
	{
		MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + 7);
		if (mainItem != null && mainItem.typeSpec == 1)
		{
			itemHeart = mainItem;
		}
		if (itemHeart == null)
		{
			stopUpgradeHeartAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(text + "\nKhông tìm thấy tim!");
			return;
		}
		if (itemHeart.LvUpgrade >= 99)
		{
			stopUpgradeHeartAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(text + "\nTim đã đạt cấp tối đa (+99)!");
			return;
		}
		long beriReq = getBeriReq(itemHeart.LvUpgrade);
		int num = ((itemHeart.LvUpgrade % 5 == 4) ? ((itemHeart.LvUpgrade / 5 + 1) * 10) : 0);
		if (GameScreen.player.beli < beriReq)
		{
			stopUpgradeHeartAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(text + "\nKhông đủ " + AvMain.getMoneyShortText(beriReq) + " Beri!");
		}
		else if (GameScreen.player.gem < num)
		{
			stopUpgradeHeartAuto();
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(text + "\nKhông đủ " + num + " Ruby!");
		}
		else
		{
			isContinueUpgradeHeart = true;
			timeStartUpgradeHeartAuto = mSystem.currentTimeMillis();
		}
	}
}
