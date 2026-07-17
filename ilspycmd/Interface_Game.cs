public class Interface_Game
{
	public const sbyte KEYPAD = 0;

	public const sbyte TOUCH = 1;

	public static mImage[] imgMove;

	public static mImage[] imgFire;

	public static mImage[] imgOther;

	public static mImage[] mImgPvPType;

	public static mImage[] mImgPvPNew;

	public static mImage imgInfo;

	public static mImage imgHoavan;

	public static mImage imgIconMPHP;

	public static mImage imgInfoServer;

	public static mImage imgInfoNew;

	public static mImage imgIconMPHP2;

	public static mImage imgbgLoL;

	public static mImage imgRankSkill;

	public static mImage imgHoiSinhLoL;

	public static mImage imgBorderWW;

	public static mImage imgBorderNoti;

	public static mImage imgBorderNoti2;

	public static FrameImage fraBorderNoti;

	public static FrameImage fraBorderNoti4;

	public static int[][] mPosKillCur = mSystem.new_M_Int(6, 2);

	public static int[][] mPosKillBuff = mSystem.new_M_Int(6, 2);

	public static int[][] mPosKillSub = mSystem.new_M_Int(6, 2);

	public static int[][] mPosMove = mSystem.new_M_Int(4, 2);

	public static int[][] mPosOther = mSystem.new_M_Int(6, 2);

	public static int[][] mSizeImgOther = mSystem.new_M_Int(6, 2);

	public static int[][] mVChangTab = mSystem.new_M_Int(6, 2);

	public static int[][] mPosEffCurrent = mSystem.new_M_Int(12, 2);

	public int[] mKeyMove = new int[4] { 4, 6, 2, 8 };

	private int[] mRotateMove = new int[4] { 2, 0, 3, 1 };

	public static int[] mValueHotKey = new int[6] { 1, 3, 5, 7, 9, 0 };

	public static short[][] mValueLoL = new short[9][]
	{
		new short[2] { 1, 1 },
		new short[2] { 3, 1 },
		new short[2] { 1, 1 },
		new short[2] { 5, 1 },
		new short[2] { 0, 1 },
		new short[2] { 5, 1 },
		new short[2] { 2, 1 },
		new short[2] { 4, 1 },
		new short[2] { 2, 1 }
	};

	public static int killLeft;

	public static int truLeft;

	public static int killRight;

	public static int truRight;

	public static int xPointMove;

	public static int yPointMove;

	public static int keyPoint;

	public static int wArrowMove = 30;

	public static int timePointer = 0;

	private static int gocBegin = 285;

	private static int lSkill = 50;

	public static int xFocus;

	public static int xBeginKill;

	public static int yBeginKill;

	public static int wSkill = 28;

	public static int wMainSkill = 50;

	public static int yInfoServer = 50;

	public static int xInfoServer;

	public static int hInfoServer;

	public static int wInfoServer;

	public static int xBeginCombo;

	public static int yBeginCombo;

	public static int xShowEffPvP;

	public static int yShowEffPvP;

	public static int xLoL;

	public static int yLoL;

	public static int yQuickChat;

	private int timeMove;

	private int timeChangeTab;

	private int maxTimeChange;

	public short[] posTam;

	private int dirTam;

	private bool isFocusTime;

	private int timepointer;

	public static bool isPaintInfoFocus;

	public static sbyte typeTouch = 1;

	public static sbyte typeShowPvP = -1;

	public static sbyte tickShowPvP;

	public static sbyte valueHoiSinhLOL;

	public static sbyte typeTitleRoomFight = 0;

	private static bool isMove = true;

	public static mVector vecEffCurrent = new mVector("Interface.vecEffCurrent");

	public static mVector vecEventShow = new mVector("Interface.vecEventShow");

	public static mVector vecInfoServer = new mVector("Interface.vecInfoServer");

	public static mVector vecQuickChatLoL = new mVector("Interface.vecQuickChatLoL");

	public static int xMoveTo;

	public static int yMoveto;

	public static int timeMoveTo;

	private int valueSmallScreen;

	public static CountDownTicket watchMap = new CountDownTicket();

	public static CountDownTicket watchRevice = new CountDownTicket();

	public static bool isSmallInfoServer = false;

	public static bool isPaintInfoServer = true;

	public static FrameImage[] fraLol;

	public static Item_Skill_Eff[] mCountKichAn;

	public static short[][] mHardcodeSkill = new short[10][]
	{
		new short[1] { 227 },
		new short[2] { 228, 229 },
		new short[2] { 230, 231 },
		new short[2] { 232, 234 },
		new short[2] { 235, 236 },
		new short[2] { 237, 238 },
		new short[2] { 239, 240 },
		new short[1] { 241 },
		new short[1] { 242 },
		new short[2] { 243, 244 }
	};

	public static string[] mCallSkill = new string[10] { "Cau su", "Lửa", "Băng", "Khói", "Cát", "Sét", "Nham thạch", "Chim ưng", "Báo đóm", "Chấn thiên" };

	public static int timePaintIconSkill = 0;

	public static int wComboSkill = 17;

	public static int timeCombo;

	public static int indexEffCombo = -1;

	public static int frameCombo;

	public static int timeEndCombo;

	public static bool isCombo = true;

	private static bool isEffHP = false;

	private static bool isEffMP = false;

	private static int[] mcolorEffHp = new int[4] { 15411779, 15615579, 16673145, 15411779 };

	private static int[] mcolorEffMp = new int[4] { 2794196, 3127269, 7064575, 2794196 };

	public static sbyte maxWin = -1;

	public static int wMoneyEff = 145;

	public static int mini = 5;

	public static int tickeffShowMoney = 0;

	public static int yEffShowMoney;

	public static int typeShowMoney;

	private static int frameIconfocus = 0;

	private static bool isNextFrame = true;

	public static int[][] colorHPHeart = new int[10][]
	{
		new int[3] { 13631520, 5253670, 13669790 },
		new int[3] { 7186180, 2439169, 12508041 },
		new int[3] { 11797622, 5308981, 13604283 },
		new int[3] { 14646020, 5582594, 14469295 },
		new int[3] { 7310769, 140881, 11584990 },
		new int[3] { 217519, 149793, 10602161 },
		new int[3] { 14725634, 5589273, 14472112 },
		new int[3] { 307402, 150873, 11390932 },
		new int[3] { 12518415, 5046790, 13610929 },
		new int[3] { 3671496, 1311304, 11181773 }
	};

	public static int vEffHP = 0;

	public static int vEffMP = 0;

	private int tickCheckPoint;

	private int xmovetam;

	private int ymovetam;

	private bool isFire;

	public static int indexHardcodeSkill = 0;

	private int indexsetClass;

	private int indexsetSkill;

	private short[] msetweapon = new short[5] { -1, 687, 688, 689, 690 };

	private string[] msetSkillDevil = new string[22]
	{
		"causu 1", "lửa 1", "lửa 2", "băng 1", "băng 2", "khói 1", "khói 2", "cát 1", "cát 2", "sét 1",
		"sét 2", "nham 1", "nham 2", "chim 1", "báo 1", "chấn thiên 1", "chấn thiên 2", "dao 1", "dao 2", "sáp 2",
		"sáp 1", "kilo 1"
	};

	public static mVector vecfocus = new mVector("Interface.vecfocus");

	private int minDisFocus = 40;

	public static int hShowEvent = 0;

	public static int timeShowEvent = 0;

	public static int speedShowEvent = 6;

	public static int maxTimePaint = 100;

	public static InfoMemList eventcur = null;

	public static InfoShowNotify infoFight = null;

	public static InfoShowNotify infoNormal = null;

	public static InfoShowNotify infoSpec = null;

	public static InfoShowNotify infoPlayer = new InfoShowNotify(string.Empty, 10);

	private static int wShowInfoPlayer = 0;

	private static int lastTick_2 = 0;

	private static int framepaint_2 = 0;

	private static int yEffInfoPlayer = 0;

	public static int xNumMess;

	public static int yNumMess;

	public static int xChat;

	public static int yChat;

	public static int xAutoFire;

	public static int yAutoFire;

	public static int xClan;

	public static int yClan;

	public static bool isAutoFireInterface = true;

	public static NumberMess numMess = new NumberMess();

	public static NumberMess numClan = new NumberMess();

	public static int yShowNameMap = 0;

	public static int speedShowNameMap = 0;

	public static int timeShowNameMap;

	public static int wtable;

	public static int HPMap = -1;

	public static int maxHPMap = -1;

	public static int wtable2 = 60;

	public static int hTextWatch = 20;

	public static string nameMap = string.Empty;

	public static int indexPaintTable = 0;

	public static int xShowNear;

	public static int speedShowNear;

	public static int timeShowNear = 0;

	public static mVector vecClanDam = new mVector("Interface.vecClanDam");

	public sbyte xShow = 75;

	public sbyte speedShow;

	public bool beginShow;

	private int lastTick;

	private int framepaint;

	public static int demShowEffPvP = 0;

	public bool isshowtime;

	public void load_Image_Pointer()
	{
		if (!GameCanvas.isTouch)
		{
			typeTouch = 0;
		}
		maxTimeChange = 5;
		xFocus = MotherCanvas.w - 56;
		hInfoServer = 20;
		xNumMess = 38;
		xAutoFire = MotherCanvas.w - 29;
		yAutoFire = MotherCanvas.h - 170;
		if (!GameCanvas.isTouch)
		{
			xNumMess = 35;
		}
		yNumMess = 50;
		wInfoServer = 120;
		if (MotherCanvas.w > 330)
		{
			wInfoServer = 140;
		}
		if (wInfoServer > MotherCanvas.w - (xNumMess * 2 + 40))
		{
			wInfoServer = MotherCanvas.w - (xNumMess * 2 + 40);
		}
		if (wInfoServer < 100)
		{
			wInfoServer = 100;
		}
		if (!GameCanvas.isTouch)
		{
			xChat = 5;
			yChat = 50;
		}
		if (MotherCanvas.w >= 320)
		{
			yInfoServer = 3;
			xInfoServer = MotherCanvas.hw - wInfoServer / 2;
		}
		else
		{
			xInfoServer = MotherCanvas.hw - wInfoServer / 2;
			yInfoServer = 3;
			if (xInfoServer < 92)
			{
				yInfoServer = 48;
				xInfoServer = MotherCanvas.w - wInfoServer - 3;
				isSmallInfoServer = true;
			}
		}
		if (GameCanvas.isSmallScreen)
		{
			valueSmallScreen = 10;
		}
		yNumMess -= valueSmallScreen;
		yInfoServer -= valueSmallScreen;
		yQuickChat = MotherCanvas.h - 50;
		if (GameCanvas.isTouch)
		{
			wSkill = 32;
			xPointMove = 55;
			yPointMove = MotherCanvas.h - 55;
			imgMove = new mImage[2];
			for (int i = 0; i < imgMove.Length; i++)
			{
				imgMove[i] = mImage.createImage("/point/move_" + i + ".png");
			}
			for (int j = 0; j < mPosMove.Length; j++)
			{
				mPosMove[j][0] = xPointMove + ((j < 2) ? (-wArrowMove + wArrowMove * 2 * (j % 2)) : 0);
				mPosMove[j][1] = yPointMove + ((j > 1) ? (-wArrowMove + wArrowMove * 2 * (j % 2)) : 0);
			}
			imgFire = new mImage[3];
			for (int k = 0; k < imgFire.Length; k++)
			{
				imgFire[k] = mImage.createImage("/point/fire_" + k + ".png");
			}
			mPosOther[0][0] = 3;
			mPosOther[0][1] = 3;
			mPosOther[1][0] = 4;
			mPosOther[1][1] = 46;
			mPosOther[2][0] = MotherCanvas.w - 30;
			mPosOther[2][1] = MotherCanvas.h - 30;
			mPosOther[3][0] = MotherCanvas.w - 30;
			mPosOther[3][1] = MotherCanvas.h - 145;
			mPosOther[4][0] = -2;
			mPosOther[4][1] = MotherCanvas.h / 2 - 20;
			mPosOther[5][0] = 66;
			mPosOther[5][1] = 45;
			setPosTouch();
			setPosMenu_TaiTho();
			imgOther = new mImage[6];
			for (int l = 0; l < imgOther.Length; l++)
			{
				imgOther[l] = mImage.createImage("/point/other_" + l + ".png");
				if (l == 0)
				{
					mSizeImgOther[l][0] = 92;
					mSizeImgOther[l][1] = 45;
				}
				else
				{
					mSizeImgOther[l][0] = mImage.getImageWidth(imgOther[l].image);
					mSizeImgOther[l][1] = mImage.getImageHeight(imgOther[l].image) / 2;
				}
			}
			QuickMenu.fraQuickMenu = new FrameImage[16];
			for (int m = 0; m < QuickMenu.fraQuickMenu.Length; m++)
			{
				QuickMenu.fraQuickMenu[m] = new FrameImage(mImage.createImage("/point/quick_" + m + ".png"), 30, 30);
			}
			QuickMenu.imgNenMenu = mImage.createImage("/point/nenmenu.png");
			QuickMenu.imgTamGiac = mImage.createImage("/point/tamgiac.png");
		}
		loadPosLoL();
		setPosSkill();
		setYTouch();
	}

	public void setPosMenu_TaiTho()
	{
		mPosOther[4][0] = -2;
		mPosOther[0][0] = 3;
		mPosOther[5][0] = 66;
		xNumMess = 38;
		if (GameCanvas.isTaiTho)
		{
			mPosOther[4][0] = 30;
			mPosOther[0][0] = 8;
			mPosOther[5][0] = 71;
			xNumMess = 43;
		}
	}

	public void loadPosLoL()
	{
		xLoL = MotherCanvas.hw;
		yLoL = 34 + GameScreen.h12plus;
		if (MotherCanvas.w < 280)
		{
			xLoL = MotherCanvas.w - 52;
			yLoL = 72 + GameScreen.h12plus;
		}
	}

	public void loadImageLOL()
	{
		fraLol = new FrameImage[6];
		for (int i = 0; i < fraLol.Length; i++)
		{
			fraLol[i] = new FrameImage(mImage.createImage("/interface/lol" + i + ".png"), 2);
		}
		if (!GameCanvas.lowGraphic)
		{
			imgbgLoL = mImage.createImage("/interface/lol10.png");
		}
		imgHoiSinhLoL = mImage.createImage("/interface/lol12.png");
	}

	public static void setYTouch()
	{
		mPosOther[0][1] = 3 + GameScreen.h12plus;
		mPosOther[1][1] = 46 + GameScreen.h12plus;
		mPosOther[5][1] = 45 + GameScreen.h12plus;
		yNumMess = 50 + GameScreen.h12plus;
		if (GameCanvas.isSmallScreen)
		{
			yNumMess -= 10;
		}
	}

	public void paintInGame(mGraphics g)
	{
		if (GameCanvas.isTouch && xShow < 75 && vecClanDam != null && vecClanDam.size() > 0)
		{
			paintInfoClanDam(g);
			return;
		}
		if (GameScreen.player != null && GameScreen.player.Action != 4)
		{
			if (GameCanvas.isTouch && typeTouch == 0)
			{
				g.drawImage(imgMove[0], xPointMove, yPointMove, 3);
				for (int i = 0; i < 4; i++)
				{
					if (timePointer > 0 && mKeyMove[i] == keyPoint)
					{
						paintMoveTouch(g, i);
					}
				}
			}
			timePaintIconSkill = 90;
			if (GameCanvas.loadmap.mapLang() && timePaintIconSkill <= 0 && !Player.isGhost)
			{
				if (typeTouch == 0 && GameCanvas.isTouch && GameScreen.objFocus != null)
				{
					if (AvMain.imgTrade == null)
					{
						AvMain.imgTrade = mImage.createImage("/interface/icontrade.png");
					}
					else
					{
						g.drawImage(AvMain.imgTrade, mPosKillCur[2][0], mPosKillCur[2][1], 3);
					}
				}
			}
			else
			{
				if (timePaintIconSkill > 0)
				{
					timePaintIconSkill--;
				}
				if (GameScreen.tickPvP <= 0)
				{
					for (int j = 0; j < mPosKillCur.Length; j++)
					{
						int num = mPosKillCur[j][0] + mVChangTab[j][0] * timeChangeTab / 100;
						int num2 = mPosKillCur[j][1] + mVChangTab[j][1] * timeChangeTab / 100;
						if (timeChangeTab == 0 && !GameCanvas.isTouch)
						{
							mFont.tahoma_7b_black.drawString(g, string.Empty + mValueHotKey[j], mPosKillCur[j][0] - wSkill / 2 + 9, mPosKillCur[j][1], 0);
						}
						Hotkey hotkey = Player.hotkeyPlayer[Player.currentTab][j];
						bool flag = false;
						if (j == 2 && isPaintIconGiaotiep())
						{
							flag = true;
						}
						if (Player.isGhost)
						{
							g.drawImage(AvMain.imgHotKey, num, num2, 3);
						}
						else if (GameCanvas.isTouch)
						{
							int num3 = 0;
							if (timePointer > 0 && keyPoint == mValueHotKey[j])
							{
								num3 = 1;
							}
							paintHotKey(g, hotkey, num, num2, 20, flag);
							if (hotkey.skill == null || hotkey.skill.lvDevil == 0 || flag)
							{
								if (j == 2 && typeTouch == 0)
								{
									g.drawRegion(imgFire[1], 0, num3 * 50, 50, 50, 0, num, num2, 3);
								}
								else
								{
									g.drawRegion(imgFire[0], 0, num3 * 50, 50, 50, 0, num, num2, 3);
								}
							}
						}
						else
						{
							paintHotKey(g, hotkey, num, num2, 20, flag);
							g.drawImage(AvMain.imgHotKey, num, num2, 3);
						}
					}
					if (!GameCanvas.loadmap.mapLang() && GameCanvas.isTouch && GameScreen.isShowSkillBuff)
					{
						for (int k = 0; k < mPosKillBuff.Length; k++)
						{
							int num4 = mPosKillBuff[k][0];
							int num5 = mPosKillBuff[k][1];
							if (GameCanvas.isTouch)
							{
								int num6 = 0;
								if (timePointer > 0 && keyPoint == 200 + k)
								{
									num6 = 1;
								}
								Hotkey hotkey2 = Player.hotkeyBuffPlayer[k];
								if (hotkey2 != null)
								{
									paintHotKey(g, hotkey2, num4, num5, 20, isPaintGiaotiep: false);
									g.drawRegion(imgFire[2], 0, num6 * 50, 50, 50, 0, num4, num5, 3);
								}
							}
						}
					}
				}
				if (Player.mComboSkill != null && Player.mComboSkill.Length > 0 && !Player.isGhost && !GameCanvas.loadmap.mapLang())
				{
					paintComboSkill(g);
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			for (int l = 0; l < mPosOther.Length; l++)
			{
				if (l == 0 || (l == 2 && GameCanvas.loadmap.mapLang() && timePaintIconSkill <= 0) || (l == 5 && GameScreen.player.clan == null))
				{
					continue;
				}
				int num7 = 0;
				if (timePointer > 0 && keyPoint == 100 + l)
				{
					num7 = 1;
				}
				if (GameScreen.player.Action == 4 && l == 2)
				{
					continue;
				}
				int num8 = mPosOther[l][0];
				int num9 = mPosOther[l][1];
				if (l == 5 && Clan_Screen.isNew)
				{
					g.drawRegion(imgOther[l], 0, num7 * mSizeImgOther[l][1], mSizeImgOther[l][0], mSizeImgOther[l][1], 0, num8, num9 + numClan.yNum, 0);
					if (GameCanvas.gameTick % 10 < 5)
					{
						g.drawImage(MainEvent.imgNew, num8, num9 + 3 + numClan.yNum, 0);
					}
				}
				else
				{
					g.drawRegion(imgOther[l], 0, num7 * mSizeImgOther[l][1], mSizeImgOther[l][0], mSizeImgOther[l][1], 0, num8, num9, 0);
				}
			}
		}
		if (GameCanvas.currentScreen == GameCanvas.gameScr)
		{
			paintVecBuffCurrent(g);
			if (!GameCanvas.isTouch)
			{
				paintInfoClanDam(g);
			}
		}
		if (timeChangeTab > 0)
		{
			paintChangTab(g);
		}
	}

	public void paintVecEffKickAn(mGraphics g)
	{
		if (mCountKichAn == null)
		{
			return;
		}
		int num = 12;
		int num2 = 3 + num / 2 + GameScreen.h12plus;
		if (GameScreen.isPvPNew)
		{
			num2 = yNumMess + 22 + GameScreen.h12plus;
			num = 28;
			for (int i = 0; i < mCountKichAn.Length && mCountKichAn[i] != null; i++)
			{
				g.drawImage(AvMain.imgBgnum, 22 + i * num, num2, 3);
				mCountKichAn[i].paintHotkey(g, 12 + i * num, num2, 11, 0);
				AvMain.FontBorderSmall(g, string.Empty + mCountKichAn[i].numPotion, 25 + i * num, num2 - 5, 2, mCountKichAn[i].colorName);
			}
		}
		else
		{
			for (int j = 0; j < mCountKichAn.Length && mCountKichAn[j] != null; j++)
			{
				g.drawImage(AvMain.imgBgnum, 112, num2 + num * j, 3);
				mCountKichAn[j].paintHotkey(g, 102, num2 + num * j, 11, 0);
				AvMain.FontBorderSmall(g, string.Empty + mCountKichAn[j].numPotion, 115, num2 + num * j - 5, 2, mCountKichAn[j].colorName);
			}
		}
	}

	public bool isPaintIconGiaotiep()
	{
		if (GameCanvas.loadmap.mapLang())
		{
			return true;
		}
		if (!GameScreen.player.setFightPk(GameScreen.objFocus))
		{
			return true;
		}
		return false;
	}

	public void paintMoveTouch(mGraphics g, int i)
	{
		switch (i)
		{
		case 0:
			g.drawRegion(imgMove[1], 0, 34, 38, 34, 0, mPosMove[i][0], mPosMove[i][1], 3);
			break;
		case 1:
			g.drawRegion(imgMove[1], 63, 34, 38, 34, 0, mPosMove[i][0], mPosMove[i][1], 3);
			break;
		case 2:
			g.drawRegion(imgMove[1], 34, 0, 34, 38, 0, mPosMove[i][0], mPosMove[i][1], 3);
			break;
		case 3:
			g.drawRegion(imgMove[1], 34, 63, 34, 38, 0, mPosMove[i][0], mPosMove[i][1], 3);
			break;
		}
	}

	public void paintViewGame(mGraphics g)
	{
		int num = 30;
		int num2 = 20;
		int num3 = MotherCanvas.w - 20;
		int num4 = 15;
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject.indexTeam == 1)
			{
				PaintHPMP(g, 1, mainObject.Hp, mainObject.maxHp, num2 + 7, num4 + mainObject.indexPaintView * num - 8, 0, 9, 66, 0, isflip: false, mainObject.HpEff, isUpdateEff: false, 0);
				PaintHPMP(g, 2, mainObject.Mp, mainObject.maxMp, num2 + 7, num4 + mainObject.indexPaintView * num + 11 - 8, 0, 9, 66, 0, isflip: false, 0, isUpdateEff: false, 0);
				if (AvMain.fraBorder == null)
				{
					AvMain.fraBorder = new FrameImage(mImage.createImage("/interface/border.png"), 26, 26);
				}
				else
				{
					AvMain.fraBorder.drawFrame(0, num2 - 2, num4 + mainObject.indexPaintView * num + 2, 0, 3, g);
				}
				mainObject.paintHead(g, num2 - 2, num4 + mainObject.indexPaintView * num - 2, 2);
				mFont.tahoma_7b_white.drawString(g, string.Empty + mainObject.Lv, num2 + 6, num4 + mainObject.indexPaintView * num + 2, 2);
			}
			else if (mainObject.indexTeam == 2)
			{
				PaintHPMP(g, 1, mainObject.Hp, mainObject.maxHp, num3 - 7 - 66, num4 + mainObject.indexPaintView * num - 8, 0, 9, 66, 0, isflip: true, mainObject.HpEff, isUpdateEff: false, 0);
				PaintHPMP(g, 2, mainObject.Mp, mainObject.maxMp, num3 - 7 - 66, num4 + mainObject.indexPaintView * num + 11 - 8, 0, 9, 66, 0, isflip: true, 0, isUpdateEff: false, 0);
				if (AvMain.fraBorder == null)
				{
					AvMain.fraBorder = new FrameImage(mImage.createImage("/interface/border.png"), 26, 26);
				}
				else
				{
					AvMain.fraBorder.drawFrame(0, num3 + 2, num4 + mainObject.indexPaintView * num + 2, 0, 3, g);
				}
				mainObject.paintHead(g, num3 + 2, num4 + mainObject.indexPaintView * num - 2, 0);
				mFont.tahoma_7b_white.drawString(g, string.Empty + mainObject.Lv, num3 - 6, num4 + mainObject.indexPaintView * num + 2, 2);
			}
		}
	}

	private void paintComboSkill(mGraphics g)
	{
		short[] mComboSkill = Player.mComboSkill;
		int num = mComboSkill.Length;
		int num2 = wComboSkill;
		int num3 = yBeginCombo;
		int num4 = xBeginCombo - num * wComboSkill / 2;
		if (checkPaintWatch())
		{
			num3 += wComboSkill;
		}
		if (!isCombo)
		{
			for (int i = 0; i < num; i++)
			{
				int num5 = i;
				num4 = xBeginCombo + num5 * num2 - num / 2 * num2 - num % 2 * num2 / 2 + num2 / 2;
				int x = wComboSkill;
				if (num5 == 0)
				{
					x = 0;
				}
				else if (num5 == num - 1)
				{
					x = wComboSkill * 2;
				}
				g.drawRegion(AvMain.imgCombo, x, 0, wComboSkill, wComboSkill, 0, num4 - wComboSkill / 2, num3 - wComboSkill / 2, 0);
				if (i <= indexEffCombo && timeEndCombo % 4 < 2)
				{
					g.setColor(14885955);
					g.fillRect(num4 - wComboSkill / 2 + 2, num3 - wComboSkill / 2 + 2, wComboSkill - 4, wComboSkill - 4);
				}
			}
			timeEndCombo++;
			if (indexEffCombo < num)
			{
				indexEffCombo++;
			}
			if (timeEndCombo >= num + 6)
			{
				timeEndCombo = 0;
				indexEffCombo = -1;
				isCombo = true;
				Player.mComboSkill = null;
				Player.indexCombo = -1;
			}
			return;
		}
		if (timeCombo > 0)
		{
			timeCombo--;
			for (int j = 0; j < num; j++)
			{
				int num6 = j;
				num4 = xBeginCombo + num6 * num2 - num / 2 * num2 - num % 2 * num2 / 2 + num2 / 2;
				int x2 = wComboSkill;
				if (num6 == 0)
				{
					x2 = 0;
				}
				else if (num6 == num - 1)
				{
					x2 = wComboSkill * 2;
				}
				g.drawRegion(AvMain.imgCombo, x2, 0, wComboSkill, wComboSkill, 0, num4 - wComboSkill / 2, num3 - wComboSkill / 2, 0);
				if (j == indexEffCombo)
				{
					g.setColor(16777215);
					g.fillRect(num4 - wComboSkill / 2, num3 - wComboSkill / 2, wComboSkill, wComboSkill);
				}
				if (j < indexEffCombo)
				{
					Skill_Info.paintIcon(g, num4, num3, mComboSkill[num6], 0);
				}
			}
			indexEffCombo++;
			if (indexEffCombo > num)
			{
				timeCombo = 0;
				indexEffCombo = -1;
			}
			return;
		}
		for (int k = 0; k < num; k++)
		{
			int num7 = k;
			num4 = xBeginCombo + num7 * num2 - num / 2 * num2 - num % 2 * num2 / 2 + num2 / 2;
			int x3 = wComboSkill;
			if (num7 == 0)
			{
				x3 = 0;
			}
			else if (num7 == num - 1)
			{
				x3 = wComboSkill * 2;
			}
			g.drawRegion(AvMain.imgCombo, x3, 0, wComboSkill, wComboSkill, 0, num4 - wComboSkill / 2, num3 - wComboSkill / 2, 0);
			if (k == indexEffCombo)
			{
				AvMain.fraComboSkill.drawFrame(frameCombo / 2, num4, num3, 0, 3, g);
				frameCombo++;
				if (frameCombo / 2 >= 4)
				{
					indexEffCombo = -1;
					frameCombo = 0;
				}
				continue;
			}
			Skill_Info.paintIcon(g, num4, num3, mComboSkill[num7], 0);
			if (num7 <= Player.indexCombo)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, wComboSkill, wComboSkill, 0, num4 - wComboSkill / 2, num3 - wComboSkill / 2, 0);
			}
			if (k == Player.indexCombo + 1)
			{
				g.drawImage(AvMain.imgBorderCombo, num4, num3, 3);
			}
		}
	}

	private void paintChangTab(mGraphics g)
	{
		for (int i = 0; i < mPosKillCur.Length; i++)
		{
			int num = mPosKillSub[i][0] - mVChangTab[i][0] * timeChangeTab / 100;
			int num2 = mPosKillSub[i][1] - mVChangTab[i][1] * timeChangeTab / 100;
			Hotkey hotkey = Player.hotkeyPlayer[Player.currentTab][i];
			if (GameCanvas.isTouch)
			{
				int num3 = 0;
				if (timePointer > 0 && keyPoint == mValueHotKey[i])
				{
					num3 = 1;
				}
				paintHotKey(g, hotkey, num, num2, 20, isPaintGiaotiep: false);
				if (hotkey.skill == null || hotkey.skill.lvDevil == 0)
				{
					if (i == 2 && typeTouch == 0)
					{
						g.drawRegion(imgFire[1], 0, num3 * 50, 50, 50, 0, num, num2, 3);
					}
					else
					{
						g.drawRegion(imgFire[0], 0, num3 * 50, 50, 50, 0, num, num2, 3);
					}
				}
			}
			else
			{
				paintHotKey(g, hotkey, num, num2, 20, isPaintGiaotiep: false);
				g.drawImage(AvMain.imgHotKey, num, num2, 3);
			}
		}
		timeChangeTab++;
		if (timeChangeTab >= maxTimeChange)
		{
			timeChangeTab = 0;
			Player.currentTab = ((Player.currentTab == 0) ? ((sbyte)1) : ((sbyte)0));
		}
	}

	private void paintHotKey(mGraphics g, Hotkey hot, int x, int y, int w, bool isPaintGiaotiep)
	{
		if (isPaintGiaotiep)
		{
			AvMain.fraQuest.drawFrame(2, x, y - 2 + GameCanvas.gameTick / 5 % 3, 0, 3, g);
			return;
		}
		hot.paint(g, x, y, w);
		DelaySkill.getDelay(hot.getIndexDelay()).paint(g, x - 10, y - 10, w);
	}

	public static void paintInfoPlayer(mGraphics g, int x, int y, bool isborder, mFont fontLv)
	{
		if (GameCanvas.isTaiTho)
		{
			x += 5;
		}
		mImage image = ((GameScreen.player.Lv != 100) ? imgIconMPHP : imgIconMPHP2);
		if (isborder)
		{
			if (GameCanvas.currentScreen == GameCanvas.gameScr && LoadMap.specMap == 7)
			{
				if (imgInfoNew == null)
				{
					imgInfoNew = LoadImageStatic.LoadNewInterface("/infonew.png");
				}
				g.drawImage(imgInfoNew, x, y, 0);
			}
			else if (!GameCanvas.lowGraphic)
			{
				g.drawImage(imgInfo, x, y, 0);
				g.drawImage(imgHoavan, x + 3, y + 20, 0);
				g.drawRegion(imgHoavan, 0, 0, 23, 23, 3, x + 66, y + 2, 0);
			}
			else
			{
				g.setColor(16246726);
				g.fillRect(x + 2, y, 88, 45);
			}
			g.drawImage(image, x + 7, y + 7, 0);
			y += 7;
		}
		else
		{
			x -= 19;
		}
		PaintHPMP(g, 1, GameScreen.player.Hp, GameScreen.player.maxHp, x + 18, y, 0, 9, 66, 0, isflip: false, GameScreen.player.HpEff, isEffHP, GameScreen.player.lvHeart);
		y += 11;
		PaintHPMP(g, 2, GameScreen.player.Mp, GameScreen.player.maxMp, x + 18, y, 0, 9, 66, 0, isflip: false, 0, isEffMP, 0);
		y += 8;
		int num = 0;
		if (GameScreen.player.Lv == 100)
		{
			fontLv.drawString(g, GameScreen.player.LvThongThao + " + " + GameScreen.player.percentThongThao / 10 + "," + GameScreen.player.percentThongThao % 10 + "%", x + 20, y, 0);
			y += 10;
			num = GameScreen.player.percentThongThao / 10 * 65 / 100;
		}
		else
		{
			fontLv.drawString(g, GameScreen.player.Lv + " + " + GameScreen.player.percentLv / 10 + "," + GameScreen.player.percentLv % 10 + "%", x + 20, y, 0);
			y += 10;
			num = GameScreen.player.percentLv / 10 * 65 / 100;
		}
		if (num > 65)
		{
			num = 65;
		}
		g.setColor(1258003);
		g.fillRect(x + 18, y, 65, 2);
		if (num > 0)
		{
			g.setColor(3514158);
			g.fillRect(x + 18, y, num, 2);
		}
		for (int i = 1; i < 5; i++)
		{
			g.setColor(16777215);
			g.fillRect(x + 18 + i * 13, y, 1, 2);
		}
	}

	public static void paintInfoPlayer_Short(mGraphics g, int x, int y, bool isborder, mFont fontLv)
	{
		mImage arg = ((GameScreen.player.Lv != 100) ? imgIconMPHP : imgIconMPHP2);
		AvMain.paintRect(g, x, y, 320, 15, 1, 4);
		g.drawRegion(arg, 0, 0, 10, 11, 0, x + 8, y + 9, 3);
		PaintHPMP(g, 1, GameScreen.player.Hp, GameScreen.player.maxHp, x + 15, y + 3, 0, 9, 66, 0, isflip: false, GameScreen.player.HpEff, isEffHP, GameScreen.player.lvHeart);
		g.drawRegion(arg, 0, 11, 10, 11, 0, x + 17 + 70, y + 9, 3);
		PaintHPMP(g, 2, GameScreen.player.Mp, GameScreen.player.maxMp, x + 15 + 10 + 70, y + 3, 0, 9, 66, 0, isflip: false, 0, isEffMP, 0);
		x += 165;
		int num = y + 7;
		AvMain.fraMoney.drawFrame(0, x + mini + AvMain.fraMoney.frameWidth / 2 - 3, num, 0, 3, g);
		AvMain.fraMoney.drawFrame(1, x + mini + AvMain.fraMoney.frameWidth / 2 + 45, num, 0, 3, g);
		AvMain.fraMoney.drawFrame(7, x + mini + AvMain.fraMoney.frameWidth / 2 + 90, num, 0, 3, g);
		if (GameScreen.player != null)
		{
			mFont.tahoma_7_yellow.drawString(g, " " + AvMain.getMoneyShortText(Player.beliTest), x + mini + AvMain.fraMoney.frameWidth - 4, num - 5, 0);
			mFont.tahoma_7_red.drawString(g, " " + AvMain.getDotNumber(GameScreen.player.gem), x + mini + AvMain.fraMoney.frameWidth + 45, num - 5, 0);
			mFont.tahoma_7_green.drawString(g, " " + AvMain.getMoneyShortText(GameScreen.player.vnd), x + mini + AvMain.fraMoney.frameWidth + 91, num - 5, 0);
		}
	}

	public static void paintPvPNew(mGraphics g, MainObject objLeft, MainObject objRight)
	{
		int hw = MotherCanvas.hw;
		int num = 84;
		int num2 = 30;
		if (mImgPvPNew == null)
		{
			loadImgPvPNew();
			return;
		}
		g.drawImage(mImgPvPNew[0], hw - num, num2, 3);
		g.drawRegion(mImgPvPNew[0], 0, 0, 129, 40, 2, hw + num, num2, 3);
		g.drawImage(mImgPvPNew[4], hw, num2 - 12, 3);
		watchMap.paintCountDownTicket(g, mFont.tahoma_7_white, hw + 1, num2 - 17, 2);
		if (objLeft != null)
		{
			PaintHPMP(g, (sbyte)((objLeft.Lv != 100) ? 98 : 96), objLeft.Hp, objLeft.maxHp, hw - 110, num2 - 16, 0, 9, 87, 0, isflip: false, objLeft.HpEff, isUpdateEff: true, 0);
			PaintHPMP(g, 97, objLeft.Mp, objLeft.maxMp, hw - 112 + 5, num2 - 2, 0, 9, 50, 4, isflip: false, 0, isUpdateEff: true, 0);
			objLeft.paintHead(g, hw - 130, num2 - 4, 2);
			if (objLeft.Lv < 100)
			{
				AvMain.Font3dSmall(g, objLeft.Lv + string.Empty, hw - 116, num2 + 6, 2, 0);
			}
			else
			{
				AvMain.Font3dSmall(g, objLeft.LvThongThao + string.Empty, hw - 115, num2 + 6, 2, 5);
			}
		}
		if (objRight != null)
		{
			PaintHPMP(g, (sbyte)((objRight.Lv != 100) ? 98 : 96), objRight.Hp, objRight.maxHp, hw + 24, num2 - 16, 0, 9, 87, 0, isflip: true, objRight.HpEff, isUpdateEff: true, 0);
			PaintHPMP(g, 97, objRight.Mp, objRight.maxMp, hw + 57, num2 - 2, 0, 9, 50, 4, isflip: true, 0, isUpdateEff: true, 0);
			objRight.paintHead(g, hw + 130, num2 - 4, 0);
			if (objRight.Lv < 100)
			{
				AvMain.Font3dSmall(g, objRight.Lv + string.Empty, hw + 116, num2 + 6, 2, 0);
			}
			else
			{
				AvMain.Font3dSmall(g, objRight.LvThongThao + string.Empty, hw + 117, num2 + 6, 2, 5);
			}
		}
		if (maxWin <= 0 || objLeft == null || objRight == null)
		{
			return;
		}
		for (int i = 1; i <= maxWin; i++)
		{
			if (objLeft.typePK == 4)
			{
				g.drawRegion(mImgPvPNew[3], 0, (watchMap.valueLeft >= i) ? 14 : 0, 7, 7, 0, hw - 9 - i * 8, num2, 3);
				g.drawRegion(mImgPvPNew[3], 0, (watchMap.valueright >= i) ? 7 : 0, 7, 7, 0, hw + 9 + i * 8, num2, 3);
			}
			else
			{
				g.drawRegion(mImgPvPNew[3], 0, (watchMap.valueLeft >= i) ? 14 : 0, 7, 7, 0, hw + 9 + i * 8, num2, 3);
				g.drawRegion(mImgPvPNew[3], 0, (watchMap.valueright >= i) ? 7 : 0, 7, 7, 0, hw - 9 - i * 8, num2, 3);
			}
		}
	}

	public static void loadImgPvPNew()
	{
		mImgPvPNew = new mImage[5];
		for (int i = 0; i < mImgPvPNew.Length; i++)
		{
			mImgPvPNew[i] = mImage.createImage("/interface/pvpnew" + i + ".png");
		}
	}

	public static void paintEffShowMoney(mGraphics g, int x, int y, bool isShow, int type)
	{
		switch (type)
		{
		case 0:
			if (isShow || y + yEffShowMoney + 18 > 0)
			{
				int num2 = y + yEffShowMoney;
				if (isShow)
				{
					num2 = y;
				}
				AvMain.paintRect(g, x, num2, wMoneyEff, 18, 1, 4);
				num2 += 9;
				AvMain.fraMoney.drawFrame(0, x + mini + AvMain.fraMoney.frameWidth / 2 - 3, num2, 0, 3, g);
				AvMain.fraMoney.drawFrame(1, x + mini + AvMain.fraMoney.frameWidth / 2 + 45, num2, 0, 3, g);
				AvMain.fraMoney.drawFrame(7, x + mini + AvMain.fraMoney.frameWidth / 2 + 90, num2, 0, 3, g);
				if (GameScreen.player != null)
				{
					mFont.tahoma_7_yellow.drawString(g, " " + AvMain.getMoneyShortText(Player.beliTest), x + mini + AvMain.fraMoney.frameWidth - 4, num2 - 5, 0);
					mFont.tahoma_7_red.drawString(g, " " + AvMain.getDotNumber(GameScreen.player.gem), x + mini + AvMain.fraMoney.frameWidth + 45, num2 - 5, 0);
					mFont.tahoma_7_green.drawString(g, " " + AvMain.getMoneyShortText(GameScreen.player.vnd), x + mini + AvMain.fraMoney.frameWidth + 91, num2 - 5, 0);
				}
			}
			break;
		case 1:
			if (isShow || y + yEffShowMoney + 18 > 0)
			{
				int num = y + yEffShowMoney;
				if (isShow)
				{
					num = y;
				}
				AvMain.paintRect(g, x, num, wMoneyEff, 18, 1, 3);
				num += 9;
				AvMain.fraMoney.drawFrame(2, x + mini + AvMain.fraMoney.frameWidth / 2, num, 0, 3, g);
				AvMain.fraMoney.drawFrame(3, x + mini + AvMain.fraMoney.frameWidth / 2 + 45, num, 0, 3, g);
				AvMain.fraMoney.drawFrame(4, x + mini + AvMain.fraMoney.frameWidth / 2 + 93, num, 0, 3, g);
				if (GameScreen.player != null)
				{
					mFont.tahoma_7_yellow.drawString(g, " " + Player.ticket, x + mini + AvMain.fraMoney.frameWidth, num - 5, 0);
					mFont.tahoma_7_yellow.drawString(g, " " + Player.keyBoss, x + mini + AvMain.fraMoney.frameWidth + 45, num - 5, 0);
					mFont.tahoma_7_yellow.drawString(g, " " + Player.PvPticket, x + mini + AvMain.fraMoney.frameWidth + 93, num - 5, 0);
				}
			}
			break;
		}
		updateEffShowMoney();
	}

	public static void updateEffShowMoney()
	{
		if (tickeffShowMoney > 0)
		{
			tickeffShowMoney--;
			if (yEffShowMoney < 0)
			{
				yEffShowMoney += 5;
				if (yEffShowMoney > 0)
				{
					yEffShowMoney = 0;
				}
			}
		}
		else if (yEffShowMoney > -25)
		{
			yEffShowMoney -= 3;
			if (yEffShowMoney < -25)
			{
				yEffShowMoney = -25;
			}
		}
	}

	public void paintVecBuffCurrent(mGraphics g)
	{
		isEffHP = false;
		isEffMP = false;
		int num = 0;
		for (int i = 0; i < vecEffCurrent.size(); i++)
		{
			if (i >= 10)
			{
				continue;
			}
			MainItem mainItem = (MainItem)vecEffCurrent.elementAt(i);
			if (mainItem == null || DelaySkill.getDelay(mainItem.indexHotKey).value <= 0)
			{
				continue;
			}
			if (mainItem.typeObject == 4)
			{
				if (mainItem.Hp_Mp_Other == 1)
				{
					isEffHP = true;
				}
				if (mainItem.Hp_Mp_Other == 2)
				{
					isEffMP = true;
				}
				mainItem.paintHotkey(g, mPosEffCurrent[num][0], mPosEffCurrent[num][1] - 4, 20, -4);
				DelaySkill.getDelay(mainItem.indexHotKey).paint(g, mPosEffCurrent[num][0] - 10, mPosEffCurrent[num][1] - 10 - 4, 20);
			}
			else if (mainItem.typeObject == 9)
			{
				mainItem.paintHotkey(g, mPosEffCurrent[num][0], mPosEffCurrent[num][1] - 4, 20, 0);
				g.drawImage(AvMain.imgBgnum, mPosEffCurrent[num][0], mPosEffCurrent[num][1] + 10, 3);
				DelaySkill.getDelay(mainItem.indexHotKey).paintOnlytime(g, mPosEffCurrent[num][0] - 11, mPosEffCurrent[num][1] - 1, 22, mainItem.colorName);
			}
			num++;
		}
	}

	public static void paintIconFocus(mGraphics g)
	{
		if (GameScreen.objFocus == null || GameScreen.objFocus.typeObject == 0 || GameScreen.objFocus.typeObject == 1 || GameScreen.objFocus.returnAction() || GameScreen.objFocus.isTanHinh)
		{
			return;
		}
		MainObject objFocus = GameScreen.objFocus;
		if (AvMain.fraIconfocus.nFrame == 1)
		{
			g.drawImage(AvMain.fraIconfocus.imgFrame, objFocus.x, objFocus.y - objFocus.hOne - objFocus.dy - objFocus.hIconFocus - GameCanvas.gameTick % 5, 3);
			return;
		}
		if (isNextFrame)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				frameIconfocus++;
			}
			if (frameIconfocus >= AvMain.fraIconfocus.nFrame - 1)
			{
				isNextFrame = false;
			}
		}
		else
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				frameIconfocus--;
			}
			if (frameIconfocus <= 0)
			{
				isNextFrame = true;
			}
		}
		AvMain.fraIconfocus.drawFrame(frameIconfocus, objFocus.x, objFocus.y - objFocus.hOne - objFocus.dy - objFocus.hIconFocus - 3, 0, 3, g);
	}

	public void paintInfoFocus(mGraphics g)
	{
		if (GameScreen.objFocus != null && !GameScreen.objFocus.returnAction() && (GameScreen.objFocus.name.Length > 0 || GameScreen.objFocus.isNPC_Area() == 99))
		{
			int num = xFocus - 2;
			if (GameCanvas.isTaiTho)
			{
				num -= 5;
			}
			int yp = 4 + GameScreen.h12plus;
			GameScreen.objFocus.paintInfoFocus(g, num, yp);
		}
	}

	public static void PaintHPMP(mGraphics g, sbyte type, int cur, int max, int x, int y, int plusHFont, int hrect, int wRect, int isNum, bool isflip, int hpEff, bool isUpdateEff, int LvHeart)
	{
		int num = cur;
		if (isNum == 1)
		{
			num = cur / 10;
		}
		int color = 13631520;
		int color2 = 5451296;
		if (type < 0)
		{
			if (CRes.abs(type) == 2)
			{
				color = 2264788;
				color2 = 4806486;
			}
		}
		else if (type == 2)
		{
			color = 2264788;
			color2 = 4806486;
		}
		else if (type == 3)
		{
			color = 3787037;
			color2 = 930311;
		}
		else if (type == 99)
		{
			color2 = 13631520;
			color = 2132744;
		}
		else if (type == 100)
		{
			color2 = 2132744;
			color = 2953937;
		}
		else if (type == 101)
		{
			color2 = 2953937;
			color = 1557973;
		}
		else if (type == 102 || type == 104 || type == 106)
		{
			int num2 = LvHeart / 10;
			if (num2 < 0 && num2 >= colorHPHeart.Length)
			{
				num2 = 0;
			}
			color = ((cur >= max / 10 || GameCanvas.gameTick % 5 >= 2) ? colorHPHeart[num2][0] : colorHPHeart[num2][2]);
			color2 = colorHPHeart[num2][1];
		}
		else if (type == 103)
		{
			color = 6215679;
			color2 = 2928878;
			g.setColor(21978);
			g.fillRect(x - 1, y - 1, wRect + 2, hrect + 2);
		}
		else if (type == 98 || type == 97 || type == 96)
		{
			color = 6215679;
			color2 = 2928878;
		}
		if (type != 98 && type != 97 && type != 96)
		{
			g.setColor(color2);
			g.fillRect(x, y, wRect, hrect);
		}
		int num3 = 0;
		if (max > 0 && num > 0)
		{
			num3 = ((max <= 1000000) ? (num * wRect / max) : (num / 1000 * wRect / (max / 1000)));
			if (num3 <= 0)
			{
				num3 = 1;
			}
			else if (num3 > wRect)
			{
				num3 = wRect;
			}
			if (hpEff > 0)
			{
				int num4 = 0;
				num4 = ((max <= 1000000) ? (hpEff * wRect / max) : (hpEff / 1000 * wRect / (max / 1000)));
				if (num4 <= 0)
				{
					num4 = 1;
				}
				else if (num4 > wRect)
				{
					num4 = wRect;
				}
				if (type == 98 || type == 97 || type == 96)
				{
					if ((type == 98 || type == 96) && num4 > 0)
					{
						if (!isflip)
						{
							g.drawRegion(mImgPvPNew[1], 0, hrect + hrect, num4, hrect, 0, x, y, 0);
						}
						else
						{
							g.drawRegion(mImgPvPNew[1], 0, hrect + hrect, num4, hrect, 2, x + (wRect - num4), y, 0);
						}
					}
				}
				else
				{
					g.setColor(0);
					if (!isflip)
					{
						g.fillRect(x, y, num4, hrect);
					}
					else
					{
						g.fillRect(x + wRect - num4, y, num4, hrect);
					}
				}
			}
			g.setColor(color);
			if (type == 98 || type == 97 || type == 96)
			{
				int y2 = 0;
				int num5 = 1;
				if (type == 96)
				{
					y2 = hrect;
				}
				if (type == 97)
				{
					num5 = 2;
				}
				if (num3 > 0)
				{
					if (!isflip)
					{
						g.drawRegion(mImgPvPNew[num5], 0, y2, num3, hrect, 0, x, y, 0);
					}
					else
					{
						g.drawRegion(mImgPvPNew[num5], 0, y2, num3, hrect, 2, x + (wRect - num3), y, 0);
					}
				}
			}
			else
			{
				if (type < 0)
				{
					g.fillRect(x, y, num3, hrect);
				}
				else
				{
					if (!isflip)
					{
						g.fillRect(x, y, num3, hrect);
					}
					else
					{
						g.fillRect(x + wRect - num3, y, num3, hrect);
					}
					if (type == 106)
					{
						g.setColor(16120660);
						if (!isflip)
						{
							g.fillRect(x, y, num3, 1);
						}
						else
						{
							g.fillRect(x + wRect - num3, y, num3, 1);
						}
					}
				}
				if (isUpdateEff && num > mcolorEffHp.Length * 2)
				{
					if (type == 1)
					{
						if (vEffHP + mcolorEffHp.Length * 2 - 1 <= num3)
						{
							for (int i = 0; i < mcolorEffHp.Length; i++)
							{
								g.setColor(mcolorEffHp[i]);
								int w = 2;
								if (i == mcolorEffHp.Length - 1)
								{
									w = 1;
								}
								g.fillRect(x + i * 2 + vEffHP, y, w, hrect);
							}
						}
						vEffHP += 2;
						if (vEffHP + mcolorEffHp.Length * 2 > wRect)
						{
							vEffHP = 0;
						}
					}
					else
					{
						if (vEffMP + mcolorEffMp.Length * 2 - 1 <= num3)
						{
							for (int j = 0; j < mcolorEffMp.Length; j++)
							{
								g.setColor(mcolorEffMp[j]);
								int w2 = 2;
								if (j == mcolorEffMp.Length - 1)
								{
									w2 = 1;
								}
								g.fillRect(x + j * 2 + vEffMP, y, w2, hrect);
							}
						}
						vEffMP += 2;
						if (vEffMP + mcolorEffMp.Length * 2 > wRect)
						{
							vEffMP = 0;
						}
					}
				}
			}
		}
		string text = string.Empty + max;
		string text2 = string.Empty + cur;
		if (max >= 1000000000)
		{
			text = max / 1000000000 + "," + max % 1000000000 / 100000000 + "B";
		}
		else if (max > 1000000)
		{
			text = max / 1000000 + "," + max % 1000000 / 100000 + "M";
		}
		else if (max >= 100000)
		{
			text = max / 1000 + "k";
		}
		if (cur >= 100000)
		{
			text2 = cur / 1000 + "k";
		}
		switch (isNum)
		{
		case 0:
			mFont.tahoma_7_white.drawString(g, text2 + "/" + text, x + wRect / 2, y - 1 + plusHFont + (hrect - 9) / 2, 2);
			break;
		case 1:
			mFont.tahoma_7_white.drawString(g, MainItem.strGetPercent(cur, 1), x + wRect / 2, y - 1 + plusHFont + (hrect - 9) / 2, 2);
			break;
		case 2:
			if (type == 104)
			{
				int num6 = mFont.tahoma_7_black.getWidth(text2 + "/" + text) + 4;
				g.setColor(0);
				g.fillRect(x + wRect / 2 - 10 - num6 / 2, y - 1 + plusHFont + (hrect - 9) / 2, num6, 11);
			}
			AvMain.Font3dSmall(g, text2 + "/" + text, x + wRect / 2 - 10, y - 1 + plusHFont + (hrect - 9) / 2, 2, 0);
			break;
		case 3:
			AvMain.Font3dSmall(g, text2 + "/" + text, x + wRect / 2, y - 1 + plusHFont + (hrect - 9) / 2, 2, 0);
			break;
		case 4:
			mFont.tahoma_7_white.drawString(g, text2 + string.Empty, x + wRect / 2, y - 1 + plusHFont + (hrect - 9) / 2, 2);
			break;
		}
	}

	public static string ConvertNumToStr(int num)
	{
		string result = string.Empty + num;
		if (num >= 1000000000)
		{
			result = num / 1000000000 + "," + num % 1000000000 / 100000000 + "B";
		}
		else if (num >= 1000000)
		{
			result = num / 1000000 + "," + num % 1000000 / 100000 + num % 100000 / 10000 + "M";
		}
		else if (num >= 1000)
		{
			result = num / 1000 + "k";
		}
		return result;
	}

	public static string ConvertTimeToStr(int time)
	{
		if (time < 0)
		{
			return "00:00:00";
		}
		string empty = string.Empty;
		empty = ((time / 60 / 60 >= 10) ? (empty + time / 60 / 60) : (empty + "0" + time / 60 / 60));
		empty += ":";
		empty = ((time / 60 % 60 >= 10) ? (empty + time / 60 % 60) : (empty + "0" + time / 60 % 60));
		empty += ":";
		if (time % 60 < 10)
		{
			return empty + "0" + time % 60;
		}
		return empty + time % 60;
	}

	public static void paintHP_Thong_Thao(mGraphics g, int x, int y, int maxpaint, int hpEff, int max, bool isFocus)
	{
		int color = 11572345;
		int num = 0;
		if (isFocus)
		{
			color = 16775897;
			num = 2;
		}
		if (AvMain.fraNenThongThao == null)
		{
			AvMain.fraNenThongThao = LoadImageStatic.loadFraImage("/interface/thong_thao_2.png", 10, 12);
		}
		g.setColor(color);
		g.fillRect(x - 1, y, maxpaint + 2, 12);
		for (int i = 0; i <= (maxpaint - 1) / 10; i++)
		{
			if (i < (maxpaint - 1) / 10)
			{
				AvMain.fraNenThongThao.drawFrame(num, x + i * 10, y, 0, 0, g);
			}
			else
			{
				g.drawRegion(AvMain.fraNenThongThao.imgFrame, 0, num * 12, (maxpaint - 1) % 10 + 1, 12, 0, x + i * 10, y, 0);
			}
		}
		int num2 = 0;
		num2 = hpEff * maxpaint / max;
		if (num2 < 0)
		{
			num2 = 0;
		}
		else if (num2 > maxpaint)
		{
			num2 = maxpaint;
		}
		if (num2 > 0)
		{
			for (int j = 0; j <= (num2 - 1) / 10; j++)
			{
				if (j < (num2 - 1) / 10)
				{
					AvMain.fraNenThongThao.drawFrame(1 + num, x + j * 10, y, 0, 0, g);
				}
				else
				{
					g.drawRegion(AvMain.fraNenThongThao.imgFrame, 0, 12 + num * 12, (num2 - 1) % 10 + 1, 12, 0, x + j * 10, y, 0);
				}
			}
		}
		mFont.tahoma_7_white.drawString(g, hpEff + "/" + max, x + maxpaint / 2, y, 2);
	}

	public static void PaintLoadData(mGraphics g, int curupdate, int maxupdate, int xp, int yp, int wUpdate, int hUpdate, int hNum)
	{
		g.setColor(5002069);
		g.fillRect(xp - 5, yp + 1 + 15, wUpdate + 4, hUpdate - 2);
		g.fillRect(xp - 2, yp - 2 + 15, wUpdate - 2, hUpdate + 4);
		g.setColor(2698542);
		g.fillRect(xp - 4, yp + 15, wUpdate + 2, hUpdate);
		g.fillRect(xp - 4 + 1, yp + 14, wUpdate, 1);
		g.fillRect(xp - 4 + 1, yp + 15 + hUpdate, wUpdate, 1);
		g.setColor(3027507);
		g.fillRect(xp - 4 + 1, yp + 15, wUpdate, hUpdate);
		int num = 0;
		if (maxupdate > 0 && curupdate > 0)
		{
			num = curupdate * wUpdate / maxupdate;
			if (num <= 0)
			{
				num = 1;
			}
			else if (num > wUpdate)
			{
				num = wUpdate;
			}
			g.setColor(10339648);
			g.fillRect(xp - 4 + 1, yp + 15, num, hUpdate);
		}
		g.setColor(2698542);
		g.fillRect(xp - 3, yp + hUpdate + 3, 1, 1);
		g.fillRect(xp - 3, yp + 14 + hUpdate, 1, 1);
		g.fillRect(xp + wUpdate - 4, yp + hUpdate + 3, 1, 1);
		g.fillRect(xp + wUpdate - 4, yp + 14 + hUpdate, 1, 1);
		mFont.tahoma_7b_white.drawString(g, curupdate + "/" + maxupdate, xp + wUpdate / 2 - 3, yp + hNum, 2);
	}

	public void updateInGame()
	{
		if (timePointer > 0)
		{
			timePointer--;
		}
		updateTouchVecNear();
		if (LoadMap.specMap != 3)
		{
			TouchMoveIngame();
		}
	}

	public void updateViewKhanGia()
	{
		updateMoveCamera();
	}

	public void updateTouchVecNear()
	{
		if (timeShowNear <= 0 || !GameCanvas.isPointerSelect)
		{
			return;
		}
		int num = 60;
		int num2 = GameCanvas.hText + 8;
		for (int i = 0; i < vecfocus.size(); i++)
		{
			if (GameCanvas.isPoint(MotherCanvas.w - 70, num + num2 * i, 70, num2))
			{
				GameCanvas.isPointerSelect = false;
				MainObject mainObject = (MainObject)vecfocus.elementAt(i);
				if (mainObject != null && !mainObject.returnAction())
				{
					GameScreen.objFocus = mainObject;
					GameScreen.objFocus.Giaotiep();
					isPaintInfoFocus = true;
					timeShowNear = 0;
				}
				break;
			}
		}
	}

	public void TouchMoveIngame()
	{
		isMove = true;
		updateShowTime();
		for (int i = 0; i < mPosOther.Length; i++)
		{
			if ((i != 2 || !GameCanvas.loadmap.mapLang() || timePaintIconSkill > 0) && (i != 5 || GameScreen.player.clan != null) && GameCanvas.isPoint(mPosOther[i][0] - 2, mPosOther[i][1] - 2, mSizeImgOther[i][0] + 4, mSizeImgOther[i][1] + 4))
			{
				if (GameCanvas.isPointerSelect)
				{
					GameCanvas.isPointerSelect = false;
					selectPointer(i);
					isMove = false;
					break;
				}
				if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
				{
					keyPoint = 100 + i;
					timePointer = 3;
					break;
				}
			}
		}
		if (GameCanvas.isPoint(xNumMess, yNumMess - 3, 22, 22))
		{
			if (GameCanvas.isPointerSelect)
			{
				GameCanvas.isPointerSelect = false;
				selectPointer(7);
				isMove = false;
			}
			else if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
			{
				keyPoint = 106;
				timePointer = 3;
			}
		}
		if (GameCanvas.isPoint(xAutoFire, yAutoFire - 3, 22, 22) && GameCanvas.isPointerSelect)
		{
			GameCanvas.isPointerSelect = false;
			selectPointer(6);
			isMove = false;
		}
		if (GameCanvas.isPointSelect(MotherCanvas.w - 74, 0, 74, 40) && GameScreen.objFocus != null && (GameScreen.objFocus.typeObject == 0 || GameScreen.objFocus.typeObject == 2))
		{
			GameCanvas.isPointerSelect = false;
			GameScreen.objFocus.Giaotiep();
		}
		if (!isMove)
		{
			return;
		}
		if (timeChangeTab == 0)
		{
			for (int j = 0; j < mPosKillCur.Length; j++)
			{
				if (!GameCanvas.isPoint(mPosKillCur[j][0] - wSkill / 2, mPosKillCur[j][1] - wSkill / 2, wSkill, wSkill))
				{
					continue;
				}
				if (GameCanvas.isPointerSelect)
				{
					if (GameScreen.objFocus == null)
					{
						Hotkey hotkey = Player.hotkeyPlayer[Player.currentTab][j];
						if (hotkey.skill != null && !hotkey.skill.isBuff)
						{
							GameScreen.player.setFocus(allFocus: true);
						}
					}
					Player.IndexFire = j;
					GameScreen.player.setActionHotKey(j);
					break;
				}
				if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
				{
					keyPoint = mValueHotKey[j];
					timePointer = 3;
				}
			}
			if (!GameCanvas.loadmap.mapLang() && GameCanvas.isTouch && GameScreen.isShowSkillBuff)
			{
				for (int k = 0; k < mPosKillBuff.Length; k++)
				{
					if (GameCanvas.isPoint(mPosKillBuff[k][0] - wSkill / 2, mPosKillBuff[k][1] - wSkill / 2, wSkill, wSkill) && GameCanvas.isPointerSelect)
					{
						GameScreen.player.setActionHotKeyBuff(k);
						keyPoint = 200 + k;
						timePointer = 3;
						break;
					}
				}
			}
		}
		checkChangeMapPoint();
		if (typeTouch == 1)
		{
			moveTypeTouch();
		}
		else if (typeTouch == 0)
		{
			moveTypeKeypad();
		}
	}

	private void checkChangeMapPoint()
	{
		if (tickCheckPoint > 0)
		{
			tickCheckPoint--;
			return;
		}
		int x = GameCanvas.px + MainScreen.cameraMain.xCam;
		int y = GameCanvas.py + MainScreen.cameraMain.yCam;
		for (int i = 0; i < LoadMap.vecPointChange.size(); i++)
		{
			Point point = (Point)LoadMap.vecPointChange.elementAt(i);
			if (MainObject.getDistance(point.x, point.y, x, y) < 28 && CRes.abs(GameScreen.player.x - point.x) < 48 && CRes.abs(GameScreen.player.y - point.y) < 72 && GameScreen.player.Hp > 0 && GameScreen.player.typeActionBoat == 0 && GameScreen.player.Action == 0)
			{
				tickCheckPoint = 20;
				GlobalService.gI().Obj_Move((short)point.x, (short)point.y);
			}
		}
	}

	public void moveTypeTouch()
	{
		if (Player.isGhost)
		{
			return;
		}
		int num = GameCanvas.px + MainScreen.cameraMain.xCam;
		int num2 = GameCanvas.py + MainScreen.cameraMain.yCam;
		MainObject mainObject = null;
		if (GameCanvas.isPointerSelect && timeMove == 0)
		{
			isFire = false;
			if (MainObject.getDistance(num, num2, GameScreen.player.x, GameScreen.player.y) <= Player.wFocus - 15 || GameScreen.player.Action == 4)
			{
				mainObject = setObjectNear(num, num2);
			}
			if (mainObject != null && mainObject.typeObject != 1)
			{
				Player.setStart_EndAutoFire(isAu: false);
			}
			if (mainObject != null)
			{
				GameScreen.objFocus = mainObject;
				if (MainObject.getDistance(mainObject.x, mainObject.y, GameScreen.player.x, GameScreen.player.y) <= Player.wFocus && GameScreen.player.Action != 4)
				{
					GameCanvas.isPointerSelect = false;
					GameScreen.objFocus.setTouchPoint();
					isPaintInfoFocus = true;
					int num3 = 1;
					if (GameScreen.player.setFightPk(GameScreen.objFocus))
					{
						num3 = 0;
						GameScreen.addEffectEnd_ObjTo(24, (sbyte)num3, GameScreen.objFocus.x, GameScreen.objFocus.y, GameScreen.objFocus.ID, GameScreen.objFocus.typeObject, 0, null);
						isFire = true;
					}
					posTam = null;
				}
			}
			int tile = GameCanvas.loadmap.getTile(num, num2);
			if (tile == -1 || tile == 1)
			{
				int num4 = num - 24;
				int num5 = num2 + 24;
				int num6 = -1;
				if (num2 + MainScreen.cameraMain.yCam > GameCanvas.loadmap.maxHMap - 140)
				{
					num5 = num2 - 24;
				}
				for (int i = 0; i < 3; i++)
				{
					num6 = GameCanvas.loadmap.getTile(num4 + i * 24, num5);
					if (num6 != -1 && num6 != 1)
					{
						num = num4 + i * 24;
						num2 = num5;
						tile = GameCanvas.loadmap.getTile(num, num2);
						break;
					}
				}
			}
			if (tile != -1 && tile != 1 && GameScreen.player.Action != 4)
			{
				if (num > GameScreen.player.x)
				{
					dirTam = 2;
				}
				else
				{
					dirTam = 0;
				}
				posTam = GameCanvas.loadmap.updateFindRoad(num / LoadMap.wTile, num2 / LoadMap.wTile, GameScreen.player.x / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, 100, GameScreen.player);
				xmovetam = num;
				ymovetam = num2;
				if (posTam != null && posTam.Length > 100)
				{
					posTam = null;
				}
				timeMove = 2;
			}
			else
			{
				posTam = null;
				if (vecfocus.size() > 0)
				{
					GameCanvas.isPointerSelect = false;
				}
			}
		}
		if (timeMove > 0)
		{
			if (timeMove == 1 && posTam != null && GameScreen.player.Action != 4 && GameScreen.player.Action != 2)
			{
				GameScreen.player.xStopMove = 0;
				GameScreen.player.yStopMove = 0;
				GameScreen.player.toX = GameScreen.player.x;
				GameScreen.player.toY = GameScreen.player.y;
				if (GameScreen.player.posTransRoad != null)
				{
					GameScreen.player.countAutoMove = 2;
				}
				GameScreen.player.posTransRoad = posTam;
				GameScreen.player.Dir = dirTam;
				setMoveTo(xmovetam, ymovetam);
				posTam = null;
				if (!isFire)
				{
					Player.setStart_EndAutoFire(isAu: false);
					if (isPaintInfoFocus)
					{
						isPaintInfoFocus = false;
					}
					if (GameScreen.player.skillCurrent != null)
					{
						GameScreen.player.skillCurrent = null;
					}
				}
				GameCanvas.isPointerSelect = false;
			}
			timeMove--;
		}
		updateMoveCamera();
	}

	public void updateMoveCamera()
	{
		if (GameCanvas.currentScreen != GameCanvas.gameScr)
		{
			return;
		}
		if (GameCanvas.isPointerMove)
		{
			if (!GameScreen.isMoveCamera && (CRes.abs(GameCanvas.px - GameCanvas.pxLast) > 48 || CRes.abs(GameCanvas.py - GameCanvas.pyLast) > 48))
			{
				GameScreen.isMoveCamera = true;
			}
			GameScreen.xMoveCam = GameCanvas.px - GameCanvas.pxLast;
			GameScreen.yMoveCam = GameCanvas.py - GameCanvas.pyLast;
			GameScreen.timeResetCam = 40;
		}
		else if (GameCanvas.isPointerDown)
		{
			GameScreen.xCur = MainScreen.cameraMain.xCam;
			GameScreen.yCur = MainScreen.cameraMain.yCam;
			GameScreen.xMoveCam = 0;
			GameScreen.yMoveCam = 0;
		}
	}

	public void moveTypeKeypad()
	{
		if ((GameCanvas.isPointerDown || GameCanvas.isPointerMove) && GameCanvas.isPointLast(xPointMove - 2 * wArrowMove, yPointMove - 2 * wArrowMove, wArrowMove * 4, wArrowMove * 4))
		{
			int num = CRes.angle(GameCanvas.px - xPointMove, GameCanvas.py - yPointMove);
			int num2 = 0;
			num2 = ((num > 45 && num <= 135) ? 3 : ((num <= 135 || num > 225) ? ((num <= 225 || num > 315) ? 1 : 2) : 0));
			GameCanvas.clearKeyHold();
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerSelect = false;
			keyPoint = mKeyMove[num2];
			GameCanvas.keyMyHold[keyPoint] = true;
			timePointer = 3;
			Player.setStart_EndAutoFire(isAu: false);
			if (GameScreen.player.skillCurrent != null)
			{
				GameScreen.player.skillCurrent = null;
			}
		}
		if (GameCanvas.isPointerSelect && !GameCanvas.isPointer(xPointMove - 50, yPointMove - 50, 100, 100))
		{
			int x = GameCanvas.px + MainScreen.cameraMain.xCam;
			int y = GameCanvas.py + MainScreen.cameraMain.yCam;
			MainObject mainObject = setObjectNear(x, y);
			if (mainObject != null)
			{
				GameScreen.objFocus = mainObject;
				GameScreen.objFocus.setTouchPoint();
			}
		}
	}

	public void selectPointer(int select)
	{
		switch (select)
		{
		case 0:
			GameCanvas.gameScr.cmdInfoMe.perform();
			break;
		case 1:
			GameCanvas.gameScr.cmdChatPopup.perform();
			break;
		case 2:
			if (timeChangeTab > 0 || Player.isGhost || GameScreen.player.Action == 4)
			{
				break;
			}
			timeChangeTab = 1;
			if (!GameCanvas.isTouch || typeTouch == 1)
			{
				for (int i = 0; i < mVChangTab.Length; i++)
				{
					mVChangTab[i][0] = 0;
					mVChangTab[i][1] = (MotherCanvas.h - mPosKillCur[i][1]) * 200 / maxTimeChange;
				}
			}
			else
			{
				for (int j = 0; j < mVChangTab.Length; j++)
				{
					mVChangTab[j][0] = (MotherCanvas.w - mPosKillCur[j][0]) * 200 / maxTimeChange;
					mVChangTab[j][1] = 0;
				}
			}
			GameCanvas.clearAll();
			break;
		case 3:
			GameCanvas.gameScr.cmdNextFocus.perform();
			break;
		case 4:
			startQuickMenu();
			break;
		case 5:
			GameCanvas.gameScr.cmdClan.perform();
			break;
		case 7:
			GameCanvas.eventScr.Show(GameCanvas.gameScr);
			break;
		case 6:
			if (!isAutoFireInterface && Player.typeAutoFireMain == -1)
			{
				addInfoPlayerNormal(T.caidatauto, mFont.tahoma_7_yellow);
			}
			else
			{
				isAutoFireInterface = !isAutoFireInterface;
			}
			break;
		}
	}

	private void setskillClass()
	{
		GameScreen.player.weapon = msetweapon[indexsetClass % 5];
		GameScreen.player.addChat(T.mClazz[indexsetClass % 5 + 1], isStop: true);
		GameCanvas.readMess.commandPointer(10, indexsetClass % 5);
		indexsetClass++;
	}

	private void setSkillDevil()
	{
		GameScreen.player.addChat(msetSkillDevil[indexsetSkill % msetSkillDevil.Length], isStop: true);
		if (indexsetSkill % msetSkillDevil.Length <= 5)
		{
			for (int i = 0; i < Player.vecListSkill.size(); i++)
			{
				Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(i);
				skill_Info.typeEffSkill = (short)(227 + indexsetSkill % msetSkillDevil.Length);
				if (i == 2)
				{
					break;
				}
			}
		}
		else if (indexsetSkill % msetSkillDevil.Length <= 16)
		{
			for (int j = 0; j < Player.vecListSkill.size(); j++)
			{
				Skill_Info skill_Info2 = (Skill_Info)Player.vecListSkill.elementAt(j);
				skill_Info2.typeEffSkill = (short)(234 + (indexsetSkill % msetSkillDevil.Length - 6));
				if (j == 2)
				{
					break;
				}
			}
		}
		else if (indexsetSkill % msetSkillDevil.Length >= 17)
		{
			for (int k = 0; k < Player.vecListSkill.size(); k++)
			{
				Skill_Info skill_Info3 = (Skill_Info)Player.vecListSkill.elementAt(k);
				skill_Info3.typeEffSkill = (short)(251 + (indexsetSkill % msetSkillDevil.Length - 17));
				if (k == 2)
				{
					break;
				}
			}
		}
		indexsetSkill++;
	}

	public void addInfo()
	{
		MsgGiftLogin msgGiftLogin = new MsgGiftLogin();
		string name = "Đổi quà";
		string empty = string.Empty;
		int num = 21;
		Item_Drop[] array = new Item_Drop[num];
		int num2 = 5;
		for (int i = 0; i < num; i++)
		{
			sbyte type = 4;
			string name2 = "ngày " + (i + 1);
			short idIcon = (short)i;
			int num3 = i + 1;
			sbyte color = 0;
			array[i] = new Item_Drop((short)i, type, name2, 0, 0, idIcon, color);
			array[i].num = num3;
			array[i].typeGiftDaily = 0;
			if (i == 0)
			{
			}
		}
		MainItem mainItem = new MainItem(4, 0, 0);
		mainItem.name = "test";
		msgGiftLogin.setinfoShow_GiftLogin(0, name, empty, array, 0, "Chọn", null);
		GameCanvas.Start_Sub_Dialog(msgGiftLogin);
	}

	public static void startQuickMenu()
	{
		QuickMenu.gI().startAt();
	}

	public void setPosSkill()
	{
		if (GameCanvas.isTouch)
		{
			if (typeTouch == 0)
			{
				xBeginKill = MotherCanvas.w - 35;
				yBeginKill = MotherCanvas.h - 50;
				int num = gocBegin;
				int num2 = gocBegin - 30;
				for (int i = 0; i < mPosKillCur.Length; i++)
				{
					if (i == 2)
					{
						mPosKillCur[i][0] = xBeginKill;
						mPosKillCur[i][1] = yBeginKill;
					}
					else
					{
						mPosKillCur[i][0] = xBeginKill + CRes.getcos(CRes.fixangle(num)) * lSkill / 1000;
						mPosKillCur[i][1] = yBeginKill + CRes.getsin(CRes.fixangle(num)) * lSkill / 1000;
						num -= 35;
					}
					mPosKillBuff[i][0] = xBeginKill + CRes.getcos(CRes.fixangle(num2)) * (lSkill + 40) / 1000;
					mPosKillBuff[i][1] = yBeginKill + CRes.getsin(CRes.fixangle(num2)) * (lSkill + 40) / 1000;
					num2 -= 20;
					mPosKillSub[i][0] = MotherCanvas.w * 2 - mPosKillCur[i][0];
					mPosKillSub[i][1] = mPosKillCur[i][1];
				}
				xBeginKill = wSkill / 2;
				yBeginKill = 80;
				for (int j = 0; j < mPosKillCur.Length; j++)
				{
					mPosEffCurrent[j][0] = xBeginKill;
					mPosEffCurrent[j][1] = yBeginKill + j * wSkill;
					mPosEffCurrent[j + 6][0] = xBeginKill + wSkill / 2;
					mPosEffCurrent[j + 6][1] = yBeginKill + j * wSkill;
				}
			}
			else if (typeTouch == 1)
			{
				xBeginKill = MotherCanvas.w - wSkill * 6 - wSkill / 2;
				yBeginKill = MotherCanvas.h - 24;
				for (int k = 0; k < mPosKillCur.Length; k++)
				{
					mPosKillCur[k][0] = xBeginKill + k * wSkill;
					mPosKillCur[k][1] = yBeginKill;
					mPosKillSub[k][0] = mPosKillCur[k][0];
					mPosKillSub[k][1] = MotherCanvas.h * 2 - mPosKillCur[k][1];
					mPosEffCurrent[k][0] = xBeginKill + k * wSkill;
					mPosEffCurrent[k][1] = yBeginKill - wSkill;
					mPosEffCurrent[k + 6][0] = xBeginKill + k * wSkill;
					mPosEffCurrent[k + 6][1] = yBeginKill - wSkill * 2 + 4;
				}
				xBeginKill = wSkill / 2;
				int num3 = wSkill;
				if (GameCanvas.isTaiTho)
				{
					xBeginKill += 10;
				}
				for (int l = 0; l < mPosKillBuff.Length; l++)
				{
					mPosKillBuff[l][0] = xBeginKill + l % 3 * num3;
					mPosKillBuff[l][1] = yBeginKill - num3 * (l / 3);
				}
			}
		}
		else
		{
			xBeginKill = MotherCanvas.hw - 70;
			yBeginKill = MotherCanvas.h - GameCanvas.hCommand - 5;
			for (int m = 0; m < mPosKillCur.Length; m++)
			{
				mPosKillCur[m][0] = xBeginKill + m * wSkill;
				mPosKillCur[m][1] = yBeginKill;
				mPosKillSub[m][0] = mPosKillCur[m][0];
				mPosKillSub[m][1] = MotherCanvas.h * 2 - mPosKillCur[m][1];
				mPosEffCurrent[m][0] = xBeginKill + m * wSkill;
				mPosEffCurrent[m][1] = yBeginKill - wSkill;
			}
		}
		yBeginCombo = yInfoServer + 40;
		xBeginCombo = MotherCanvas.w / 2;
	}

	public static void setPosTouch()
	{
		if (typeTouch == 1)
		{
			mPosOther[2][1] = MotherCanvas.h - 37;
			mPosOther[3][1] = MotherCanvas.h - 67;
			yAutoFire = MotherCanvas.h - 92;
			yQuickChat = MotherCanvas.h - 100;
		}
		else if (typeTouch == 0)
		{
			mPosOther[2][1] = MotherCanvas.h - 30;
			mPosOther[3][1] = MotherCanvas.h - 145;
			yAutoFire = MotherCanvas.h - 170;
			yQuickChat = MotherCanvas.h - 94 - 74;
		}
	}

	public MainObject setObjectNear(int x, int y)
	{
		MainObject mainObject = null;
		vecfocus.removeAllElements();
		int num = minDisFocus;
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject2 == null || mainObject2 == GameScreen.player || mainObject2.returnAction() || mainObject2.typeObject == 10 || (mainObject2.Action == 4 && mainObject2.typeObject == 1))
			{
				continue;
			}
			int distance = MainObject.getDistance(x, y, mainObject2.x, mainObject2.y - mainObject2.hOne / 2);
			if (distance > minDisFocus)
			{
				continue;
			}
			if (mainObject == null)
			{
				mainObject = mainObject2;
				num = distance;
			}
			else if (mainObject.typeSpecMonSter != 1 && distance < num && mainObject.typeObject != 2)
			{
				mainObject = mainObject2;
				num = distance;
			}
			if (mainObject2.typeObject != 1 && !GameScreen.player.setFightPk(mainObject2) && (mainObject2.typeObject == 0 || mainObject.typeObject == 2))
			{
				if (mainObject2.typeObject == 2)
				{
					vecfocus.insertElementAt(mainObject2, 0);
				}
				else if (vecfocus.size() < 5)
				{
					vecfocus.addElement(mainObject2);
				}
			}
			if (!GameScreen.isShowNameSUPER_BOSS && mainObject2.typeSpecMonSter == 1)
			{
				mainObject = mainObject2;
			}
		}
		if (vecfocus.size() > 0)
		{
			createShowNear();
			if (vecfocus.size() > 1 && GameScreen.player.typePK == -1 && mainObject != null && mainObject.typeSpecMonSter != 1)
			{
				mainObject = null;
			}
		}
		return mainObject;
	}

	public void addEffCurrent(MainItem itemAdd)
	{
		if (itemAdd == null)
		{
			return;
		}
		for (int i = 0; i < vecEffCurrent.size(); i++)
		{
			MainItem mainItem = (MainItem)vecEffCurrent.elementAt(i);
			if (mainItem.typeObject == itemAdd.typeObject && mainItem.indexHotKey == itemAdd.indexHotKey)
			{
				vecEffCurrent.removeElement(mainItem);
				i--;
			}
		}
		if (itemAdd.typeObject == 4)
		{
			vecEffCurrent.insertElementAt(itemAdd, 0);
		}
		else
		{
			vecEffCurrent.addElement(itemAdd);
		}
	}

	public static void addShowEvent(InfoMemList mem)
	{
		vecEventShow.addElement(mem);
	}

	public static void paintShowEvent(mGraphics g)
	{
		if (eventcur != null && GameScreen.isShowChat)
		{
			MainEvent.paintShort(g, MotherCanvas.hw, hShowEvent - MainEvent.hShort + GameScreen.h12plus, eventcur);
		}
	}

	public static void paintShowHelp(mGraphics g, bool isInmap)
	{
		if (GameScreen.vecHelp == null)
		{
			return;
		}
		for (int i = 0; i < GameScreen.vecHelp.size(); i++)
		{
			MainHelp mainHelp = (MainHelp)GameScreen.vecHelp.elementAt(i);
			if (mainHelp.isInMap == isInmap)
			{
				mainHelp.paint(g);
				if (mainHelp.isBreak)
				{
					break;
				}
			}
		}
	}

	public static void UpdateShowHelp(mGraphics g)
	{
		if (GameScreen.vecHelp == null)
		{
			return;
		}
		for (int i = 0; i < GameScreen.vecHelp.size(); i++)
		{
			MainHelp mainHelp = (MainHelp)GameScreen.vecHelp.elementAt(i);
			mainHelp.update();
			if (mainHelp.isRemove)
			{
				GameScreen.vecHelp.removeElement(mainHelp);
			}
			else if (mainHelp.isBreak)
			{
				break;
			}
		}
	}

	public static void updateShowEvent()
	{
		if (eventcur != null)
		{
			timeShowEvent++;
			if (timeShowEvent < maxTimePaint)
			{
				if (hShowEvent < MainEvent.hShort + 3)
				{
					hShowEvent += speedShowEvent;
					if (hShowEvent > MainEvent.hShort + 3)
					{
						hShowEvent = MainEvent.hShort + 3;
					}
				}
			}
			else if (hShowEvent > 0)
			{
				hShowEvent -= speedShowEvent;
			}
			else
			{
				eventcur = null;
			}
			if (GameCanvas.isTouch && GameCanvas.isPointSelect(MotherCanvas.hw - MainEvent.wShort / 2, hShowEvent - MainEvent.hShort, MainEvent.wShort, MainEvent.hShort))
			{
				GameCanvas.isPointerSelect = false;
				InfoMemList @event = InfoMemList.getEvent(eventcur.name, eventcur.typeEvent);
				if (@event != null)
				{
					GameCanvas.eventScr.memCur = @event;
				}
				@event.setAction();
				eventcur = null;
			}
		}
		if (eventcur == null && vecEventShow.size() > 0)
		{
			eventcur = (InfoMemList)vecEventShow.elementAt(0);
			hShowEvent = 0;
			timeShowEvent = 0;
			vecEventShow.removeElementAt(0);
		}
	}

	public static void paintInfoServer(mGraphics g)
	{
		int num = yInfoServer;
		int num2 = xInfoServer;
		num = ((LoadMap.specMap == 10) ? (num + (66 + GameScreen.h12plus)) : ((checkPaintWatch() && watchMap.typeTime == 4) ? (num + (50 + GameScreen.h12plus)) : ((eventcur == null || !GameScreen.isShowChat) ? (num + GameScreen.h12plus) : (num + (hShowEvent + GameScreen.h12plus)))));
		if (eventcur == null)
		{
			num += yEffShowMoney + 25;
		}
		if (infoSpec != null)
		{
			GameCanvas.resetTrans(g);
			paintBackInfoServerNew(g, num2, num + infoSpec.y, 3);
			g.setClip(num2, num + infoSpec.y, wInfoServer, hInfoServer);
			g.saveCanvas();
			g.ClipRec(num2, num + infoSpec.y, wInfoServer, hInfoServer);
			int num3 = 0;
			if (infoSpec.iconClan >= 0)
			{
				num2 -= 10;
				MainImage iconClan = Potion.getIconClan(infoSpec.iconClan);
				if (iconClan != null && iconClan.img != null)
				{
					if (iconClan.frame == -1)
					{
						iconClan.set_Frame();
					}
					if (iconClan.frame <= 1)
					{
						g.drawImage(iconClan.img, num2 + wInfoServer - infoSpec.x + 9, num + infoSpec.y + hInfoServer / 2 - 1, 3);
					}
					else
					{
						int num4 = ((framepaint_2 < iconClan.frame - 1) ? 3 : 15);
						if (CRes.abs(GameCanvas.gameTick - lastTick_2) > num4)
						{
							framepaint_2++;
							if (framepaint_2 >= iconClan.frame)
							{
								framepaint_2 = 0;
							}
							lastTick_2 = GameCanvas.gameTick;
						}
						g.drawRegion(iconClan.img, 0, framepaint_2 * iconClan.w, iconClan.w, iconClan.w, 0, num2 + wInfoServer - infoSpec.x + 9, num + infoSpec.y + hInfoServer / 2 - 1, 3);
					}
				}
				num3 = 18;
			}
			if (GameCanvas.lowGraphic)
			{
				mFont.tahoma_7b_black.drawString(g, infoSpec.strShow, num2 + wInfoServer - infoSpec.x + 1 + num3, num + infoSpec.y + GameCanvas.hText / 4 + 1, 0);
			}
			infoSpec.fontpaint.drawString(g, infoSpec.strShow, num2 + wInfoServer - infoSpec.x + num3, num + infoSpec.y + GameCanvas.hText / 4, 0);
			num += hInfoServer + infoSpec.y + 2;
			g.restoreCanvas();
		}
		if (infoFight != null)
		{
			GameCanvas.resetTrans(g);
			paintBackInfoServerNew(g, num2, num + infoFight.y, 5);
			g.setClip(num2, num + infoFight.y, wInfoServer, hInfoServer);
			g.saveCanvas();
			g.ClipRec(num2, num + infoFight.y, wInfoServer, hInfoServer);
			if (GameCanvas.lowGraphic)
			{
				mFont.tahoma_7_black.drawString(g, infoFight.strShow, num2 + wInfoServer - infoFight.x + 1, num + infoFight.y + GameCanvas.hText / 4 + 1, 0);
			}
			infoFight.fontpaint.drawString(g, infoFight.strShow, num2 + wInfoServer - infoFight.x, num + infoFight.y + GameCanvas.hText / 4, 0);
			num += hInfoServer + infoFight.y + 2;
			g.restoreCanvas();
		}
		if (isPaintInfoServer && infoNormal != null)
		{
			GameCanvas.resetTrans(g);
			paintBackInfoServerNew(g, num2, num + infoNormal.y, 0);
			g.setClip(num2, num + infoNormal.y, wInfoServer, hInfoServer);
			g.saveCanvas();
			g.ClipRec(num2, num + infoNormal.y, wInfoServer, hInfoServer);
			if (GameCanvas.lowGraphic)
			{
				mFont.tahoma_7_black.drawString(g, infoNormal.strShow, num2 + wInfoServer - infoNormal.x + 1, num + infoNormal.y + GameCanvas.hText / 4 + 1, 0);
			}
			infoNormal.fontpaint.drawString(g, infoNormal.strShow, num2 + wInfoServer - infoNormal.x, num + infoNormal.y + GameCanvas.hText / 4, 0);
			num += hInfoServer + infoNormal.y + 2;
			g.restoreCanvas();
		}
		if (infoPlayer != null && !infoPlayer.isRemove)
		{
			int num5 = MotherCanvas.hw;
			if (isSmallInfoServer)
			{
				num5 = MotherCanvas.w - wShowInfoPlayer / 2 - 4;
			}
			GameCanvas.resetTrans(g);
			paintBackInfoServer(g, num5, num + infoPlayer.y, wShowInfoPlayer);
			g.setClip(num5 - wShowInfoPlayer / 2, num + infoPlayer.y, wShowInfoPlayer, hInfoServer);
			g.saveCanvas();
			g.ClipRec(num5 - wShowInfoPlayer / 2, num + infoPlayer.y, wShowInfoPlayer, hInfoServer);
			if (GameCanvas.lowGraphic)
			{
				AvMain.FontBorderSmall(g, infoPlayer.strShow, num5 + 1, num + infoPlayer.y + GameCanvas.hText / 4 + yEffInfoPlayer, 2, 0);
			}
			else
			{
				infoPlayer.fontpaint.drawString(g, infoPlayer.strShow, num5, num + infoPlayer.y + GameCanvas.hText / 4 + yEffInfoPlayer, 2);
			}
			g.restoreCanvas();
		}
		GameCanvas.resetTrans(g);
	}

	public static void paintBackInfoServerNew(mGraphics g, int x, int y, int type)
	{
		if (wInfoServer > 140)
		{
			g.drawImage(imgInfoServer, x, y, 0);
			g.drawRegion(imgInfoServer, 0, 0, wInfoServer - 140, 20, 0, x + 140, y, 0);
		}
		else
		{
			g.drawRegion(imgInfoServer, 0, 0, wInfoServer, 20, 0, x, y, 0);
		}
		switch (type)
		{
		case 1:
			g.drawImage(imgBorderNoti, x - 4, y + hInfoServer / 2, 3);
			break;
		case 2:
			g.drawImage(imgBorderNoti2, x - 4, y + hInfoServer / 2, 3);
			break;
		case 3:
			if (GameCanvas.language == 0 && !GameCanvas.isDeviceStore())
			{
				if (GameCanvas.IndexServer == 3)
				{
					fraBorderNoti.drawFrame(6, x - 4, y + hInfoServer / 2, 0, 3, g);
				}
				else
				{
					fraBorderNoti.drawFrame(GameCanvas.IndexServer + 1, x - 4, y + hInfoServer / 2, 0, 3, g);
				}
			}
			else
			{
				fraBorderNoti.drawFrame(0, x - 4, y + hInfoServer / 2, 0, 3, g);
			}
			break;
		case 4:
		case 5:
			fraBorderNoti.drawFrame(type, x - 4, y + hInfoServer / 2, 0, 3, g);
			break;
		}
	}

	public static void paintBackInfoServer(mGraphics g, int x, int y, int w)
	{
		if (w > 140 && w <= 280)
		{
			g.drawImage(imgInfoServer, x - w / 2, y, 0);
			g.drawRegion(imgInfoServer, 0, 0, w - 140, 20, 0, x - w / 2 + 140, y, 0);
		}
		else
		{
			g.drawRegion(imgInfoServer, 0, 0, w, 20, 0, x - w / 2, y, 0);
		}
	}

	public static void updateInfoServer()
	{
		if (infoSpec != null)
		{
			infoSpec.update();
			if (infoSpec.isRemove)
			{
				infoSpec = null;
			}
		}
		else
		{
			for (int i = 0; i < vecInfoServer.size(); i++)
			{
				InfoShowNotify infoShowNotify = (InfoShowNotify)vecInfoServer.elementAt(i);
				if (infoShowNotify.type == 1 || infoShowNotify.type == 2)
				{
					infoSpec = new InfoShowNotify(infoShowNotify.strShow, infoShowNotify.type);
					infoSpec.setValue(infoShowNotify.fontpaint);
					infoSpec.iconClan = infoShowNotify.iconClan;
					string text = string.Empty;
					if (infoSpec.iconClan >= 0)
					{
						text = T.Clan + " ";
					}
					GameCanvas.chatTabScr.addNewChat(T.tabServer, string.Empty, text + infoShowNotify.strShow, 1, isFocus: false);
					vecInfoServer.removeElement(infoShowNotify);
					break;
				}
			}
		}
		infoNormal = setUpdateInfoServer(infoNormal, 0);
		infoFight = setUpdateInfoServer(infoFight, 5);
		if (infoPlayer != null && !infoPlayer.isRemove)
		{
			infoPlayer.update();
			if (yEffInfoPlayer > 0)
			{
				yEffInfoPlayer -= 2;
			}
		}
		for (int j = 0; j < vecQuickChatLoL.size(); j++)
		{
			InfoShowNotify infoShowNotify2 = (InfoShowNotify)vecQuickChatLoL.elementAt(j);
			infoShowNotify2.time++;
			if (infoShowNotify2.time >= 200)
			{
				vecQuickChatLoL.removeElementAt(j);
				j--;
			}
		}
	}

	public static InfoShowNotify setUpdateInfoServer(InfoShowNotify infoInput, sbyte type)
	{
		if (infoInput != null)
		{
			infoInput.isPaint = isPaintInfoServer;
			infoInput.update();
			if (infoInput.isRemove)
			{
				infoInput = null;
			}
		}
		else
		{
			for (int i = 0; i < vecInfoServer.size(); i++)
			{
				InfoShowNotify infoShowNotify = (InfoShowNotify)vecInfoServer.elementAt(i);
				if (infoShowNotify.type == type)
				{
					infoInput = new InfoShowNotify(infoShowNotify.strShow, infoShowNotify.type);
					infoInput.setValue(infoShowNotify.fontpaint);
					GameCanvas.chatTabScr.addNewChat(T.tabServer, string.Empty, infoShowNotify.strShow, 1, isFocus: false);
					vecInfoServer.removeElement(infoShowNotify);
					break;
				}
			}
		}
		return infoInput;
	}

	public static void addInfoServer(InfoShowNotify info)
	{
		if (info.type == 2)
		{
			vecInfoServer.insertElementAt(info, 0);
		}
		else
		{
			vecInfoServer.addElement(info);
		}
	}

	public static void addInfoPlayerNormal(string str, mFont font)
	{
		infoPlayer.strShow = str;
		infoPlayer.setValue(font);
		yEffInfoPlayer = 20;
		wShowInfoPlayer = infoPlayer.fontpaint.getWidth(infoPlayer.strShow) + 4;
	}

	public static void paintNumMess(mGraphics g, int xlech, int ylech)
	{
		int num = 0;
		if (timePointer > 0 && keyPoint == 106)
		{
			num = 1;
		}
		int num2 = xNumMess + xlech;
		int num3 = yNumMess + ylech;
		if (GameCanvas.isTouch)
		{
			g.drawRegion(AvMain.imgMess, 0, num * 16, 22, 16, 0, num2, num3 + numMess.yNum, 0);
		}
		else
		{
			g.drawImage(AvMain.imgMess, num2, num3 + numMess.yNum, 0);
		}
		if (!GameCanvas.isTouch && GameCanvas.currentScreen == GameCanvas.gameScr)
		{
			g.drawImage(AvMain.imgChat, xChat, yChat + GameScreen.h12plus, 0);
		}
		if (GameScreen.numMess > 0)
		{
			int num4 = -3;
			if (GameCanvas.isTouch)
			{
				num4 = 0;
			}
			string st = string.Empty + GameScreen.numMess;
			if (GameScreen.numMess > 9)
			{
				st = "9+";
			}
			mFont.tahoma_7_black.drawString(g, st, num2 + num4, num3 + numMess.yNum + 2, 2);
		}
		if (GameCanvas.isTouch && GameCanvas.currentScreen == GameCanvas.gameScr)
		{
			int idx = 2;
			if (!isAutoFireInterface)
			{
				idx = 0;
			}
			else if (GameCanvas.gameTick % 12 < 7)
			{
				idx = 1;
			}
			AvMain.fraAutoFire.drawFrame(idx, xAutoFire, yAutoFire, 0, 0, g);
		}
	}

	public static void updateNumMess()
	{
		if (GameScreen.numMess > 0)
		{
			numMess.update();
		}
		else
		{
			numMess.yNum = 0;
		}
		if (Clan_Screen.isNew)
		{
			numClan.update();
		}
	}

	public static void setMoveTo(int x, int y)
	{
		xMoveTo = x;
		yMoveto = y;
		timeMoveTo = 0;
	}

	public static void updateMoveTo()
	{
		if (timeMoveTo < 10)
		{
			timeMoveTo++;
		}
	}

	public static void paintMoveTo(mGraphics g)
	{
		if (timeMoveTo < 10 && AvMain.fraMoveTo != null)
		{
			AvMain.fraMoveTo.drawFrame(timeMoveTo / 2, xMoveTo, yMoveto, 0, 3, g);
		}
	}

	public void paintShowNameMap(mGraphics g)
	{
		if (maxHPMap > -1)
		{
			int num = 42;
			g.setColor(13350814);
			g.fillRect(MotherCanvas.w - 60 - 2, num + 36 - 1, 58, 12);
			PaintHPMP(g, 1, HPMap, maxHPMap, MotherCanvas.w - 60 - 1, num + 36, 0, 10, 56, 0, isflip: false, 0, isUpdateEff: false, 0);
		}
		if (LoadMap.specMap != 10 && checkPaintWatch())
		{
			paintWatchMap(g);
		}
		paintQuickChatLoL(g);
		if (watchRevice.timeCountDown > 0)
		{
			paintWatchRevice(g);
		}
		if (yShowNameMap > 0 && GameScreen.player != null && GameScreen.player.Lv > 1)
		{
			paintNameMap(g);
		}
		if (typeShowPvP >= 0)
		{
			paintShowtypePvP(g, MotherCanvas.hw, 80);
		}
		if (LoadMap.specMap == 10)
		{
			paintInfoLoL(g);
		}
		if (GameScreen.isShowNameWW)
		{
			if (imgBorderWW == null)
			{
				imgBorderWW = mImage.createImage("/interface/wwborder.png");
				return;
			}
			g.drawImage(imgBorderWW, MotherCanvas.w / 2, 15 + GameScreen.h12plus, 3);
			mFont.tahoma_7_white.drawString(g, string.Empty + GameScreen.player.checkWW, MotherCanvas.w / 2, 3 + GameScreen.h12plus, 2);
			mFont.tahoma_7b_green.drawString(g, string.Empty + GameScreen.player.killWW, MotherCanvas.w / 2 - 12, 15 + GameScreen.h12plus, 2);
			mFont.tahoma_7b_red.drawString(g, string.Empty + GameScreen.player.deadWW, MotherCanvas.w / 2 + 12, 15 + GameScreen.h12plus, 2);
		}
	}

	private void paintQuickChatLoL(mGraphics g)
	{
		for (int i = 0; i < vecQuickChatLoL.size(); i++)
		{
			InfoShowNotify infoShowNotify = (InfoShowNotify)vecQuickChatLoL.elementAt(i);
			infoShowNotify.paintQuickChat(g, MotherCanvas.w - 4, yQuickChat - GameCanvas.hText - i * GameCanvas.hText);
		}
	}

	public static bool checkPaintWatch()
	{
		if (watchMap == null || GameCanvas.loadmap == null)
		{
			return false;
		}
		if (watchMap.timeCountDown > 0 || GameCanvas.loadmap.idMap == 995 || GameCanvas.loadmap.idMap == 997 || GameCanvas.loadmap.idMap == 997 || (GameCanvas.loadmap.idMap >= 988 && GameCanvas.loadmap.idMap >= 995))
		{
			return true;
		}
		return false;
	}

	private void paintWatchMap(mGraphics g)
	{
		if (GameCanvas.menuCur.isShowMenu || (GameCanvas.isTouch && xShow < 75 && vecClanDam != null && vecClanDam.size() > 0) || GameScreen.isPvPNew)
		{
			return;
		}
		if (watchMap.typeTime == 4 || GameCanvas.loadmap.idMap == 995)
		{
			if (indexPaintTable == 1)
			{
				paintTableScoreLienTiep(g);
			}
			else
			{
				paintTableScoreNormal(g);
			}
			return;
		}
		int num = wtable2;
		int num2 = 42;
		int num3 = MotherCanvas.w - num / 2 - 3;
		if (!GameCanvas.isTouch && vecClanDam != null && vecClanDam.size() > 0)
		{
			num2 = 80;
			num3 = 35;
		}
		if (watchMap.typeTime == 2)
		{
			num = 54;
			num3 = MotherCanvas.w - num / 2;
			num2 = 94 + GameScreen.h12plus;
			if (GameCanvas.loadmap.idMap >= 167 && GameCanvas.loadmap.idMap <= 175)
			{
				num2 = 42 + GameScreen.h12plus;
			}
			AvMain.paintnenFocusSmall(g, num3 - num / 2 - 2, num2 + 3, num, 27);
		}
		else
		{
			AvMain.paintRect(g, num3 - num / 2 - 1, num2, num, 32, 1, 4);
		}
		mFont.tahoma_7b_white.drawString(g, watchMap.strInfo, num3, num2 + hTextWatch / 5, 2);
		watchMap.paintCountDownTicket(g, mFont.tahoma_7b_white, num3, num2 + hTextWatch / 2 + hTextWatch / 5 + 4, 2);
	}

	private void paintTableScoreNormal(mGraphics g)
	{
		mImage mImage2 = null;
		int num = 136;
		int num2 = MotherCanvas.w / 2 + 8;
		if (GameScreen.typeViewPlayer == 1)
		{
			num2 = MotherCanvas.w / 2;
		}
		int num3 = 2 + GameScreen.h12plus;
		if (MotherCanvas.w < 280)
		{
			if (AvMain.imgTimePvpSmall == null)
			{
				AvMain.imgTimePvpSmall = mImage.createImage("/interface/timepvpsmall.png");
				return;
			}
			mImage2 = AvMain.imgTimePvpSmall;
			num3 = 4;
			num = 84;
			g.drawImage(mImage2, num2, 25 + num3, 3);
			AvMain.FontBorderColor(g, watchMap.valueLeft + string.Empty, num2 - num / 4, num3 + hTextWatch / 5, 2, 6, 7);
			AvMain.FontBorderColor(g, watchMap.valueright + string.Empty, num2 + num / 4, num3 + hTextWatch / 5, 2, 1, 7);
			watchMap.paintCountDownTicket(g, mFont.tahoma_7_white, num2, num3 + hTextWatch / 2 + hTextWatch / 5 + 4 + 8, 2);
		}
		else if (AvMain.imgTimePvp == null)
		{
			AvMain.imgTimePvp = mImage.createImage("/interface/timepvp.png");
		}
		else
		{
			mImage2 = AvMain.imgTimePvp;
			g.drawImage(mImage2, num2, 25 + num3, 3);
			AvMain.FontBorderColor(g, watchMap.valueLeft + string.Empty, num2 - num / 4, num3 + 5 + hTextWatch / 5, 2, 6, 7);
			AvMain.FontBorderColor(g, watchMap.valueright + string.Empty, num2 + num / 4, num3 + hTextWatch / 5 + 5, 2, 1, 7);
			watchMap.paintCountDownTicket(g, mFont.tahoma_7_white, num2, num3 + hTextWatch / 2 + hTextWatch / 5 + 4 + 4, 2);
		}
	}

	private void paintTableScoreLienTiep(mGraphics g)
	{
		mImage mImage2 = null;
		int num = 136;
		int num2 = MotherCanvas.w / 2 + 8;
		if (GameScreen.typeViewPlayer == 1)
		{
			num2 = MotherCanvas.w / 2;
		}
		int num3 = 2 + GameScreen.h12plus;
		if (MotherCanvas.w < 280)
		{
			if (AvMain.imgTimePvpSmall == null)
			{
				AvMain.imgTimePvpSmall = mImage.createImage("/interface/timepvpsmall2.png");
				return;
			}
			mImage2 = AvMain.imgTimePvpSmall;
			num3 = 4;
			num = 84;
			g.drawImage(mImage2, num2, 25 + num3, 3);
			AvMain.FontBorderColor(g, watchMap.valueLeft + string.Empty, num2 - num / 4, num3 + hTextWatch / 5, 2, 0, 7);
			AvMain.FontBorderColor(g, watchMap.valueright + string.Empty, num2 + num / 4, num3 + hTextWatch / 5, 2, 0, 7);
			watchMap.paintCountDownTicket(g, mFont.tahoma_7_white, num2, num3 + hTextWatch / 2 + hTextWatch / 5 + 4 + 8, 2);
		}
		else if (AvMain.imgTimePvp == null)
		{
			AvMain.imgTimePvp = mImage.createImage("/interface/timepvp2.png");
		}
		else
		{
			mImage2 = AvMain.imgTimePvp;
			g.drawImage(mImage2, num2, 25 + num3, 3);
			AvMain.FontBorderColor(g, watchMap.valueLeft + string.Empty, num2 - num / 4, num3 + 5 + hTextWatch / 5, 2, 0, 7);
			AvMain.FontBorderColor(g, watchMap.valueright + string.Empty, num2 + num / 4, num3 + hTextWatch / 5 + 5, 2, 0, 7);
			watchMap.paintCountDownTicket(g, mFont.tahoma_7_white, num2, num3 + hTextWatch / 2 + hTextWatch / 5 + 4 + 4, 2);
		}
	}

	private void paintWatchRevice(mGraphics g)
	{
		int num = 54;
		int num2 = 124 + GameScreen.h12plus;
		int num3 = MotherCanvas.w - num / 2 - 2;
		if (LoadMap.specMap == 10)
		{
			num2 = yLoL + 28;
			num3 = xLoL;
		}
		AvMain.paintnenFocusSmall(g, num3 - num / 2, num2 + 3, num, 27);
		mFont.tahoma_7b_white.drawString(g, watchRevice.strInfo, num3, num2 + hTextWatch / 5, 2);
		watchRevice.paintCountDownTicket(g, mFont.tahoma_7b_white, num3, num2 + hTextWatch / 2 + hTextWatch / 5 + 4, 2);
	}

	private void paintNameMap(mGraphics g)
	{
		AvMain.paintnenFocus(g, MotherCanvas.hw - wtable / 2, yShowNameMap + GameScreen.h12plus - 40, wtable, 36);
		AvMain.Font3dColor(g, nameMap, MotherCanvas.hw, yShowNameMap + GameScreen.h12plus - 40 + GameCanvas.hText / 2, 2, 0);
		mFont.tahoma_7_white.drawString(g, "- " + T.Area + " " + LoadMap.getShowArea(LoadMap.Area) + " -", MotherCanvas.hw, yShowNameMap + GameScreen.h12plus - 40 + GameCanvas.hText * 3 / 2, 2);
	}

	public void paintInfoLoL(mGraphics g)
	{
		if (fraLol == null)
		{
			loadImageLOL();
		}
		if (!GameCanvas.lowGraphic)
		{
			g.drawImage(imgbgLoL, xLoL, yLoL, 3);
		}
		watchMap.paintCountDownTicket(g, mFont.tahoma_7_white, xLoL, yLoL - 30, 2);
		AvMain.FontBorderSmall(g, string.Empty + killLeft, xLoL - 37, yLoL - 29, 2, 6);
		AvMain.FontBorderSmall(g, string.Empty + truLeft, xLoL - 23, yLoL - 28, 2, 6);
		AvMain.FontBorderSmall(g, string.Empty + killRight, xLoL + 38, yLoL - 29, 2, 1);
		AvMain.FontBorderSmall(g, string.Empty + truRight, xLoL + 24, yLoL - 28, 2, 1);
		if (valueHoiSinhLOL == 1)
		{
			g.drawImage(imgHoiSinhLoL, xLoL - 60, yLoL - 8, 3);
		}
		for (int i = 0; i < mValueLoL.Length; i++)
		{
			fraLol[mValueLoL[i][0]].drawFrame(mValueLoL[i][1], xLoL - 35 + i / 3 * 35, yLoL + 3 - 12 + 16 * (i % 3), 0, 3, g);
		}
	}

	private void paintShowtypePvP(mGraphics g, int x, int y)
	{
		switch (typeShowPvP)
		{
		case 0:
		case 2:
		case 3:
		case 4:
			if (mImgPvPType == null)
			{
				loadImagePvP();
			}
			else
			{
				g.drawImage(mImgPvPType[typeShowPvP], x + xShowEffPvP, y + yShowEffPvP, 3);
			}
			break;
		case 1:
			if (mImgPvPType == null)
			{
				loadImagePvP();
			}
			else
			{
				int num = 5;
				g.drawRegion(y0: ((tickShowPvP >= 75) ? 5 : ((tickShowPvP >= 60) ? 4 : ((tickShowPvP >= 50) ? 3 : ((tickShowPvP >= 35) ? 2 : ((tickShowPvP >= 25) ? 1 : 0))))) * 20, arg0: mImgPvPType[1], x0: 0, w0: 20, h0: 20, arg5: 0, x: x + xShowEffPvP, y: y + yShowEffPvP, arg8: 3);
			}
			break;
		}
	}

	public void updateShowNameMap()
	{
		if (watchMap.timeCountDown > 0)
		{
			watchMap.updateTimeCountDownTicket();
		}
		if (watchRevice.timeCountDown > 0)
		{
			watchRevice.updateTimeCountDownTicket();
		}
		if (timeShowNameMap >= 0)
		{
			timeShowNameMap++;
			if (timeShowNameMap <= 80)
			{
				yShowNameMap = MotherCanvas.h / 5 + 14;
				if (GameCanvas.isSmallScreen)
				{
					yShowNameMap = 50;
				}
			}
			else
			{
				speedShowNameMap -= 2;
				yShowNameMap += speedShowNameMap;
				if (yShowNameMap <= 0)
				{
					timeShowNameMap = -1;
				}
			}
			if (timeShowNameMap == 80)
			{
				speedShowNameMap = 8;
			}
		}
		if (typeShowPvP >= 0)
		{
			updateTypePvP();
		}
	}

	public static void paintShowNear(mGraphics g)
	{
		if (timeShowNear <= -5)
		{
			return;
		}
		int num = 60;
		int num2 = GameCanvas.hText + 8;
		for (int i = 0; i < vecfocus.size(); i++)
		{
			MainObject mainObject = (MainObject)vecfocus.elementAt(i);
			string text = mainObject.name;
			if (mainObject.isNPC_Area() == 99)
			{
				text = T.Area;
			}
			if (text.Length > 11)
			{
				text = mainObject.name.Substring(0, 10);
			}
			sbyte isborder = 1;
			if (mainObject.typeObject == 2)
			{
				isborder = 3;
			}
			int num3 = xShowNear;
			AvMain.paintRect(g, num3, num + num2 * i, 68, num2 - 4, isborder, 4);
			if (mainObject.isNPC_Area() == 1 && mainObject.typePlayer != 2 && mainObject.typePlayer != 3)
			{
				mFont.tahoma_7_white.drawString(g, text, num3 + 21, num + GameCanvas.hText / 4 + num2 * i, 0);
				mainObject.paintHead(g, num3 + 10, num + GameCanvas.hText / 2 + num2 * i, 2);
			}
			else
			{
				mFont.tahoma_7_white.drawString(g, text, num3 + 35, num + GameCanvas.hText / 4 + num2 * i, 2);
			}
		}
	}

	public void paintShowtime(mGraphics g)
	{
		if (vecClanDam != null && vecClanDam.size() != 0 && !GameCanvas.menuCur.isShowMenu)
		{
			int num = 0;
			if (timepointer > 0 && isFocusTime)
			{
				num = 1;
			}
			bool flag = true;
			if (xShow < 75)
			{
				flag = false;
			}
			if (flag)
			{
				g.drawRegion(imgOther[4], 0, num * 40, 16, 40, 2, MotherCanvas.w - 14, MotherCanvas.h / 2 - 35, 0);
			}
		}
	}

	public void paintInfoClanDam(mGraphics g)
	{
		if (vecClanDam == null || vecClanDam.size() == 0)
		{
			return;
		}
		int num = 60;
		int hText = GameCanvas.hText;
		for (int i = 0; i < vecClanDam.size(); i++)
		{
			MainClan mainClan = (MainClan)vecClanDam.elementAt(i);
			string text = mainClan.name;
			if (text.Length > 11)
			{
				text = mainClan.name.Substring(0, 10);
			}
			sbyte isborder = 1;
			int num2 = MotherCanvas.w - 70 + xShow;
			AvMain.paintRect(g, num2, num + hText * 2 * i, 68, hText * 2 - 4, isborder, 4);
			mFont.tahoma_7_white.drawString(g, text, num2 + 21, num + GameCanvas.hText / 4 + hText * 2 * i - 2, 0);
			MainImage iconClan = Potion.getIconClan(mainClan.idIcon);
			if (iconClan != null && iconClan.img != null)
			{
				if (iconClan.frame == -1)
				{
					iconClan.set_Frame();
				}
				if (iconClan.frame <= 1)
				{
					g.drawImage(iconClan.img, num2 + 10, num + GameCanvas.hText / 2 + hText * 2 * i, 3);
				}
				else
				{
					int num3 = ((framepaint < iconClan.frame - 1) ? 3 : 15);
					if (CRes.abs(GameCanvas.gameTick - lastTick) > num3)
					{
						framepaint++;
						if (framepaint >= iconClan.frame)
						{
							framepaint = 0;
						}
						lastTick = GameCanvas.gameTick;
					}
					g.drawRegion(iconClan.img, 0, framepaint * iconClan.w, iconClan.w, iconClan.w, 0, num2 + 10, num + GameCanvas.hText / 2 + hText * 2 * i, 3);
				}
			}
			AvMain.FontBorderColor(g, string.Empty + mainClan.xp, num2 + 34, num + hText * 2 * i + hText, 2, 6, 7);
		}
	}

	public static void updateShowNear()
	{
		timeShowNear--;
		if (timeShowNear > 0)
		{
			if (xShowNear > MotherCanvas.w - 70)
			{
				speedShowNear += 10;
				xShowNear -= speedShowNear;
				if (xShowNear < MotherCanvas.w - 70)
				{
					xShowNear = MotherCanvas.w - 70;
					speedShowNear = 0;
				}
			}
		}
		else if (xShowNear < MotherCanvas.w)
		{
			speedShowNear += 10;
			xShowNear += speedShowNear;
		}
	}

	private void createShowNear()
	{
		xShowNear = MotherCanvas.w;
		speedShowNear = 0;
		timeShowNear = 80;
	}

	public static void setTypePvP(sbyte type)
	{
		typeShowPvP = type;
		yShowEffPvP = 0;
		xShowEffPvP = 0;
		demShowEffPvP = 0;
		switch (typeShowPvP)
		{
		case 0:
			tickShowPvP = 25;
			xShowEffPvP = -MotherCanvas.w / 2;
			break;
		case 2:
			tickShowPvP = 25;
			xShowEffPvP = MotherCanvas.w / 2;
			break;
		case 3:
			tickShowPvP = 60;
			yShowEffPvP = MotherCanvas.h - 80;
			break;
		case 4:
			yShowEffPvP = -80;
			tickShowPvP = 60;
			break;
		case 1:
			tickShowPvP = 75;
			break;
		}
	}

	public static void updateTypePvP()
	{
		demShowEffPvP++;
		if (typeShowPvP == 0)
		{
			if (demShowEffPvP < 10)
			{
				if (xShowEffPvP < 0)
				{
					xShowEffPvP += 50;
					if (xShowEffPvP > 0)
					{
						xShowEffPvP = 0;
					}
				}
			}
			else if (demShowEffPvP > 20 && xShowEffPvP < MotherCanvas.w)
			{
				xShowEffPvP += 50;
			}
		}
		else if (typeShowPvP == 2)
		{
			if (demShowEffPvP < 10)
			{
				if (xShowEffPvP > 0)
				{
					xShowEffPvP -= 80;
					if (xShowEffPvP < 0)
					{
						xShowEffPvP = 0;
					}
				}
			}
			else if (demShowEffPvP > 20 && xShowEffPvP > -MotherCanvas.w)
			{
				xShowEffPvP -= 50;
			}
		}
		else if (typeShowPvP == 3)
		{
			if (demShowEffPvP < 10 && yShowEffPvP > 0)
			{
				yShowEffPvP -= 80;
				if (yShowEffPvP < 0)
				{
					yShowEffPvP = 0;
				}
			}
		}
		else if (typeShowPvP == 4 && demShowEffPvP < 10 && yShowEffPvP < 0)
		{
			yShowEffPvP += 10;
			if (yShowEffPvP > 0)
			{
				yShowEffPvP = 0;
			}
		}
		tickShowPvP--;
		if (tickShowPvP <= 0)
		{
			typeShowPvP = -1;
		}
	}

	public void loadImagePvP()
	{
		mImgPvPType = new mImage[5];
		for (int i = 0; i < mImgPvPType.Length; i++)
		{
			if (i != 1 && GameCanvas.language == 1)
			{
				mImgPvPType[i] = mImage.createImage("/interface/pvp" + i + "_e.png");
			}
			else
			{
				mImgPvPType[i] = mImage.createImage("/interface/pvp" + i + ".png");
			}
		}
	}

	public void updateShowTime()
	{
		if (vecClanDam == null || vecClanDam.size() == 0)
		{
			return;
		}
		if (GameCanvas.isPoint(MotherCanvas.w - 14, MotherCanvas.h / 2 - 35, 16, 40))
		{
			if (GameCanvas.isPointerSelect)
			{
				GameCanvas.isPointerSelect = false;
				isFocusTime = false;
				beginShow = true;
				isshowtime = true;
			}
			else if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
			{
				isFocusTime = true;
				timepointer = 3;
			}
		}
		if ((GameCanvas.isPointerClick || GameCanvas.isPointerDown) && xShow == 0)
		{
			beginShow = true;
			isshowtime = false;
		}
		if (timepointer > 0)
		{
			timepointer--;
		}
		if (beginShow)
		{
			if (xShow == 0)
			{
				speedShow = 5;
			}
			if (xShow == 75)
			{
				speedShow = -5;
			}
			xShow += speedShow;
			if (xShow == 0 || xShow == 75)
			{
				beginShow = false;
			}
			if (speedShow > 0 && xShow >= 75)
			{
				xShow = 75;
			}
			else if (speedShow < 0 && xShow <= 0)
			{
				xShow = 0;
			}
		}
	}
}
