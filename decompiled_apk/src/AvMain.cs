public class AvMain
{
	public iCommand left;

	public iCommand right;

	public iCommand center;

	public iCommand backCMD;

	public iCommand menuCMD;

	public iCommand okCMD;

	public static int wimg;

	public static int wWanted = 130;

	public static int hWanted = 154;

	public static mImage imgSelect;

	public static mImage imgHotKey;

	public static mImage imgIconDel;

	public static mImage imgDieChar;

	public static mImage imgDelay;

	public static mImage imgLg;

	public static mImage imgEffCur;

	public static mImage imgBorderNum;

	public static mImage imgMess;

	public static mImage imgEye;

	public static mImage imgXp;

	public static mImage imgUpgrade;

	public static mImage imgJoin;

	public static mImage imgTrade;

	public static mImage imgcheck;

	public static mImage imgTrade2;

	public static mImage imgShadowSmall;

	public static mImage imgChat;

	public static mImage imgCombo;

	public static mImage imgBorderCombo;

	public static mImage imgHand;

	public static mImage imgPvpVs;

	public static mImage imgPvpOk;

	public static mImage imgPvpObjdef;

	public static mImage imgPlus12;

	public static mImage imgPlus12_2;

	public static mImage imgBgnum;

	public static mImage imgNenfocus;

	public static mImage imgBgnum2;

	public static mImage imgDaKham;

	public static mImage imgSafe;

	public static mImage imgTabClan;

	public static mImage imgChatClan;

	public static mImage imgDonateClan;

	public static mImage imgLvClan;

	public static mImage imgBannerClan;

	public static mImage imgPlusClan;

	public static mImage imgTimePvp;

	public static mImage imgTimePvpSmall;

	public static mImage imgBorderIcon;

	public static mImage imgReward;

	public static mImage imgStarMatch;

	public static mImage imgFightMatch;

	public static mImage imgWanted;

	public static mImage imgLock;

	public static mImage imgHinhnhan;

	public static mImage imgLvDevilSkill;

	public static mImage imgBeri;

	public static mImage imgArrowListServer;

	public static mImage imgInfoLock;

	public static mImage imgInfoClass;

	public static mImage imgInfoStar;

	public static mImage imgBannerRuong;

	public static mImage demo;

	public static mImage[] imgPaper;

	public static mImage[] imgButton;

	public static mImage[] mimgBgA;

	public static mImage[] imgPaperDoc;

	public static mImage[] mimgWanted;

	public static mImage[] mimgBgB;

	public static mImage[] mimgBgC;

	public static mImage[] mImgRoomW;

	public static mImage[] mImgDialogVongQuay;

	public static mImage[] mImgThanhTich;

	public static mImage[] imgPaper_Law;

	public static mImage[] imgEff_Law;

	public static mImage[] mimgWanted2;

	public static FrameImage fraPk;

	public static FrameImage fraPk2;

	public static FrameImage fraPirate;

	public static FrameImage fraQuest;

	public static FrameImage fratf;

	public static FrameImage fratf1;

	public static FrameImage fraMoveTo;

	public static FrameImage fraBorder;

	public static FrameImage fraCheck;

	public static FrameImage fraStatusArea;

	public static FrameImage imgLoadImage;

	public static FrameImage fraIconfocus;

	public static FrameImage fraStatusOnline;

	public static FrameImage fraButtonTiemNang;

	public static FrameImage fraTwoTab;

	public static FrameImage fraMoney;

	public static FrameImage fraLevelUp;

	public static FrameImage fraImgEffOnMap0;

	public static FrameImage fraDelay;

	public static FrameImage fraDelay2;

	public static FrameImage fraDiePlayer;

	public static FrameImage fraComboSkill;

	public static FrameImage fraEquip;

	public static FrameImage fraIconNpc;

	public static FrameImage fraShadowFocus;

	public static FrameImage fraEffBoss;

	public static FrameImage fraIconClan;

	public static FrameImage fraBorderClan;

	public static FrameImage fraBorderClan2;

	public static FrameImage fraAutoFire;

	public static FrameImage fraClanLevelUp;

	public static FrameImage fraEffItem;

	public static FrameImage fraEffItem2;

	public static FrameImage fraMatch;

	public static FrameImage fraEventMoon;

	public static FrameImage fraIconMoon;

	public static FrameImage fraIconTop;

	public static FrameImage fraBorderSkill;

	public static FrameImage fraThongThao;

	public static FrameImage fraNenThongThao;

	public static FrameImage fraBanhLai;

	public static FrameImage fraBorderWanted;

	public static FrameImage fraIconWanted;

	public static FrameImage fraIconMenu;

	public static FrameImage fraIconServer;

	public static FrameImage fraUniform;

	public static FrameImage fraBtLogin;

	public static FrameImage fraBtBanhlai;

	public static FrameImage fraNew;

	public static FrameImage fraIconHome;

	public static FrameImage fraEffOpen;

	public static FrameImage fraEffDasieucap;

	public static FrameImage fraPlus;

	public static FrameImage fraCmdNhanNapThe;

	public static FrameImage fraNauBanh;

	public static FrameImage fraTrongCay;

	public static mImage[] imgDuoi;

	public static mImage[] imgGoc;

	public static mImage[] imgGiua;

	public static mImage[] imgPhai;

	public static mImage[] imgTrai;

	public static mImage[] imgTren;

	public static mImage[] imgKhung;

	public static mImage[] imgDialogTrangTri;

	public static mImage imgTrangTri;

	public static mImage imgDemoWanted;

	public static mImage imgComplete;

	public static mImage imgKhungItem;

	public static mImage imgKhungMem;

	public const sbyte COLOR_WHITE = 0;

	public const sbyte COLOR_BLUE = 4;

	public const sbyte COLOR_YELLOW = 5;

	public const sbyte COLOR_VIOLET = 2;

	public const sbyte COLOR_ORANGE = 3;

	public const sbyte COLOR_GREEN = 1;

	public const sbyte COLOR_RED = 6;

	public const sbyte COLOR_BLACK = 7;

	public const sbyte COLOR_BROWN = 8;

	public const sbyte COLOR_RAINBOW = 9;

	public const int LOR_NEN_TIEU_DE = 15972174;

	public const int LOR_FOCUS = 16774758;

	public const int LOR_NEN_TIEU_DE_LAW = 5408452;

	public static int[][] colorRect = new int[7][]
	{
		new int[2] { 16298080, 14190632 },
		new int[2] { 14194752, 12609544 },
		new int[2] { 10719096, 8086346 },
		new int[2] { 11506025, 6966058 },
		new int[2] { 8809550, 6966058 },
		new int[2] { 15972174, 14843656 },
		new int[2] { 5982773, 4731677 }
	};

	public const sbyte BOR_WHITE = 1;

	public const sbyte BOR_NO = 0;

	public const sbyte BOR_MENU = 2;

	public const sbyte BOR_YELLOW = 3;

	public static int[][] colorButton = new int[3][]
	{
		new int[5] { 6178867, 4470312, 8876379, 7430736, 16446420 },
		new int[5] { 9464649, 6573367, 12889227, 11310713, 16446420 },
		new int[5] { 10318923, 7032887, 13744793, 12231812, 16446420 }
	};

	public const sbyte TYPE_ARCHI = 0;

	public const sbyte TYPE_TOP = 1;

	public const sbyte TYPE_PVP = 2;

	public const sbyte TYPE_LEFT_WANTED = 0;

	public const sbyte TYPE_RIGHT_WANTED = 1;

	public const sbyte TYPE_CENTER_WANTED = 2;

	public const sbyte TYPE_CENTER_ONLY = 3;

	private static int[] colorLow = new int[6] { 14075822, 10259575, 7365460, 8944231, 4932409, 13151620 };

	public static int[] colorMenu = new int[5] { 4724752, 16300104, 9998456, 14733496, 14203529 };

	public static sbyte PAPER_NORMAL = 0;

	public static sbyte PAPER_LAW = 1;

	public static int[] mValue7Color = new int[7] { 6, 3, 5, 1, 4, 0, 2 };

	public const sbyte POS_LEFT = 1;

	public const sbyte POS_RIGHT = 2;

	public const sbyte POS_CENTER = 0;

	public virtual void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		if (GameCanvas.currentDialog == null && GameCanvas.subDialog == null && !GameCanvas.menuCur.isShowMenu)
		{
			paintCmd(g);
		}
	}

	public virtual void paintHideAvatar(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		if (GameCanvas.currentDialog == null && GameCanvas.subDialog == null && !GameCanvas.menuCur.isShowMenu)
		{
			paintCmd(g);
		}
	}

	public void paintCmd(mGraphics g)
	{
		if (left != null)
		{
			if (left.xCmd > 0 && left.yCmd > 0)
			{
				left.paint(g, left.xCmd, left.yCmd);
			}
			else
			{
				left.paint(g, GameCanvas.wCommand - 3, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2);
			}
		}
		if (right != null)
		{
			right.paint(g, MotherCanvas.w - GameCanvas.wCommand + 3, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2);
		}
		if (center != null)
		{
			center.paint(g, MotherCanvas.hw, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2);
		}
	}

	public void paintOnlyCaption(mGraphics g)
	{
		if (left != null)
		{
			left.paintOnlyCaption(g, GameCanvas.wCommand / 3 * 2, MotherCanvas.h - iCommand.hButtonCmdNor / 2 - 1);
		}
		if (right != null)
		{
			right.paintOnlyCaption(g, MotherCanvas.w - GameCanvas.wCommand / 3 * 2, MotherCanvas.h - iCommand.hButtonCmdNor / 2 - 1);
		}
		if (center != null)
		{
			center.paintOnlyCaption(g, MotherCanvas.hw, MotherCanvas.h - iCommand.hButtonCmdNor / 2 - 1);
		}
	}

	public virtual void update()
	{
	}

	public virtual void commandTab(int index, int subIndex)
	{
	}

	public virtual void commandMenu(int index, int subIndex)
	{
	}

	public virtual void commandPointer(int index, int subIndex)
	{
	}

	public virtual void updatekey()
	{
		if (GameCanvas.keyMyHold[5])
		{
			if (center != null)
			{
				GameCanvas.clearKeyPressed(5);
				GameCanvas.clearKeyHold(5);
				center.perform();
			}
		}
		else if (GameCanvas.keyMyHold[12])
		{
			if (left != null)
			{
				GameCanvas.clearKeyPressed(12);
				GameCanvas.clearKeyHold(12);
				left.perform();
			}
		}
		else if (GameCanvas.keyMyHold[13] && right != null)
		{
			GameCanvas.clearKeyPressed(13);
			GameCanvas.clearKeyHold(13);
			right.perform();
		}
	}

	public virtual void updatekeyPC()
	{
		if (GameCanvas.UseKey(5))
		{
			if (okCMD != null)
			{
				GameCanvas.clearKeyPressed(5);
				GameCanvas.clearKeyHold(5);
				okCMD.perform();
			}
		}
		else if (GameCanvas.UseKey(40) || (GameCanvas.isTouch && GameCanvas.UseKey(12)))
		{
			if (menuCMD != null)
			{
				GameCanvas.clearKeyPressed(40);
				GameCanvas.clearKeyHold(40);
				menuCMD.perform();
			}
		}
		else if ((GameCanvas.UseKey(41) || (GameCanvas.isTouch && GameCanvas.UseKey(13))) && backCMD != null)
		{
			GameCanvas.clearKeyPressed(41);
			GameCanvas.clearKeyHold(41);
			backCMD.perform();
		}
	}

	public virtual void updatekeyPCForTField()
	{
		if (GameCanvas.UseKey(5))
		{
			if (okCMD != null)
			{
				GameCanvas.clearKeyPressed(5);
				GameCanvas.clearKeyHold(5);
				okCMD.perform();
			}
		}
		else if (GameCanvas.isTouch && GameCanvas.UseKey(12))
		{
			if (menuCMD != null)
			{
				GameCanvas.clearKeyPressed(12);
				GameCanvas.clearKeyHold(12);
				menuCMD.perform();
			}
		}
		else if (GameCanvas.isTouch && GameCanvas.UseKey(13) && backCMD != null)
		{
			GameCanvas.clearKeyPressed(13);
			GameCanvas.clearKeyHold(13);
			backCMD.perform();
		}
	}

	public virtual void updatePointer()
	{
		if (!GameCanvas.isTouch)
		{
			return;
		}
		if (left != null)
		{
			if (left.isPosCmd())
			{
				left.updatePointer();
			}
			else if (GameCanvas.isPointSelect(0, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
			{
				left.perform();
			}
		}
		if (right != null)
		{
			if (right.isPosCmd())
			{
				right.updatePointer();
			}
			else if (GameCanvas.isPointSelect(MotherCanvas.w - GameCanvas.wCommand * 2, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
			{
				right.perform();
			}
		}
		if (center != null)
		{
			if (center.isPosCmd())
			{
				center.updatePointer();
			}
			else if (GameCanvas.isPointSelect(MotherCanvas.hw - GameCanvas.wCommand, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
			{
				center.perform();
			}
		}
	}

	public static int resetSelect(int select, int max, bool isreset)
	{
		if (select < 0)
		{
			select = (isreset ? max : 0);
		}
		else if (select > max)
		{
			select = ((!isreset) ? max : 0);
		}
		return select;
	}

	public static void paintnenFocus(mGraphics g, int x, int y, int w, int h)
	{
		if (w < 76)
		{
			w = 76;
		}
		if (h > 36)
		{
			if (Interface_Game.imgInfoNew == null)
			{
				Interface_Game.imgInfoNew = LoadImageStatic.LoadNewInterface("/infonew.png");
			}
			if (w == 76)
			{
				g.drawRegion(Interface_Game.imgInfoNew, 0, 0, 38, 45, 0, x, y, 0);
				g.drawRegion(Interface_Game.imgInfoNew, 54, 0, 38, 45, 0, x + 38, y, 0);
				return;
			}
			g.drawRegion(Interface_Game.imgInfoNew, 18, 0, 74, 36, 0, x + w - 74, y, 0);
			int num = w - 74;
			if (num > 74)
			{
				num = 74;
			}
			g.drawRegion(Interface_Game.imgInfoNew, 0, 0, num, 36, 0, x, y, 0);
		}
		else if (w == 76)
		{
			g.drawImage(imgNenfocus, x, y, 0);
		}
		else
		{
			g.drawRegion(imgNenfocus, 2, 0, 74, 36, 0, x + w - 74, y, 0);
			int num2 = w - 74;
			if (num2 > 74)
			{
				num2 = 74;
			}
			g.drawRegion(imgNenfocus, 0, 0, num2, 36, 0, x, y, 0);
		}
	}

	public static void paintnenFocusSmall(mGraphics g, int x, int y, int w, int h)
	{
		if (w > 72)
		{
			w = 72;
		}
		if (h > 32)
		{
			h = 32;
		}
		g.drawRegion(imgNenfocus, 2, 2, w, h, 0, x, y, 0);
	}

	public static void paintRect(mGraphics g, int xText, int yText, int wText, int hText, sbyte isborder, int index)
	{
		if (index >= colorRect.Length)
		{
			index = (sbyte)(colorRect.Length - 1);
		}
		int num = index * 4;
		int num2 = 2;
		switch (isborder)
		{
		case 1:
			g.setColor(16446420);
			g.fillRect(xText + 1, yText - 1, wText - 1, hText + 3);
			g.fillRect(xText, yText, wText + 1, hText + 1);
			g.fillRect(xText - 1, yText + 1, wText + 3, hText - 1);
			fratf1.drawFrame(0, xText - num2, yText - num2, 0, 0, g);
			fratf1.drawFrame(1, xText - num2, yText + hText - 3, 0, 0, g);
			fratf1.drawFrame(2, xText + wText - 3, yText - num2, 0, 0, g);
			fratf1.drawFrame(3, xText + wText - 3, yText + hText - 3, 0, 0, g);
			break;
		case 3:
			g.setColor(16049947);
			g.fillRect(xText + 1, yText - 1, wText - 1, hText + 3);
			g.fillRect(xText, yText, wText + 1, hText + 1);
			g.fillRect(xText - 1, yText + 1, wText + 3, hText - 1);
			fratf1.drawFrame(4, xText - num2, yText - num2, 0, 0, g);
			fratf1.drawFrame(5, xText - num2, yText + hText - 3, 0, 0, g);
			fratf1.drawFrame(6, xText + wText - 3, yText - num2, 0, 0, g);
			fratf1.drawFrame(7, xText + wText - 3, yText + hText - 3, 0, 0, g);
			break;
		}
		g.setColor(colorRect[index][0]);
		g.fillRect(xText + 4, yText, wText - 7, hText + 1);
		g.fillRect(xText, yText + 4, wText + 1, hText - 7);
		g.setColor(colorRect[index][1]);
		g.fillRect(xText + 4, yText, wText - 7, 1);
		g.fillRect(xText, yText + 4, 1, hText - 7);
		if (isborder == 2)
		{
			fratf.drawFrame(num + 2, xText + wText - 3, yText - num2, 0, 0, g);
			fratf.drawFrame(num + 3, xText + wText - 3, yText + hText - 3, 0, 0, g);
			fratf.drawFrame(num, xText - num2, yText - num2, 0, 0, g);
			fratf.drawFrame(num + 1, xText - num2, yText + hText - 3, 0, 0, g);
			g.setColor(colorRect[index][1]);
			g.fillRect(xText + 1, yText + hText, wText - 1, 1);
			g.fillRect(xText + wText, yText + 1, 1, hText - 1);
		}
		else
		{
			fratf.drawFrame(num + 2, xText + wText - 3, yText - num2, 0, 0, g);
			fratf.drawFrame(num + 3, xText + wText - 3, yText + hText - 3, 0, 0, g);
			fratf.drawFrame(num, xText - num2, yText - num2, 0, 0, g);
			fratf.drawFrame(num + 1, xText - num2, yText + hText - 3, 0, 0, g);
		}
	}

	public static void paintDrawRect(mGraphics g, int xText, int yText, int wText, int hText)
	{
		g.setColor(16446420);
		g.fillRect(xText + 1, yText - 1, wText - 2, 1);
		g.fillRect(xText + 1, yText + hText, wText - 2, 1);
		g.fillRect(xText - 1, yText + 1, 1, hText - 2);
		g.fillRect(xText + wText, yText + 1, 1, hText - 2);
		g.fillRect(xText, yText, 1, 1);
		g.fillRect(xText + wText - 1, yText, 1, 1);
		g.fillRect(xText, yText + hText - 1, 1, 1);
		g.fillRect(xText + wText - 1, yText + hText - 1, 1, 1);
	}

	public static void paintSeletedSaveButton(mGraphics g, int xText, int yText, int wText, int hText)
	{
		g.setColor(colorButton[2][1]);
		g.fillRect(xText, yText, wText, hText);
		g.fillRect(xText - 1, yText + 1, 1, hText - 2);
		g.fillRect(xText + 1, yText - 1, wText - 2, 1);
		g.fillRect(xText + wText - 1 + 1, yText + 1, 1, hText - 2);
		g.fillRect(xText + 1, yText + hText, wText - 2, 1);
	}

	public static void paintRectButton(mGraphics g, int xText, int yText, int wText, int hText, int index)
	{
		if (GameMidlet.DEVICE == 0)
		{
			if (index >= colorRect.Length)
			{
				index = (sbyte)(colorRect.Length - 1);
			}
			g.setColor(colorButton[index][4]);
			g.fillRect(xText, yText, wText, hText);
			g.fillRect(xText - 1, yText + 1, 1, hText - 2);
			g.fillRect(xText + 1, yText - 1, wText - 2, 1);
			g.fillRect(xText + wText - 1 + 1, yText + 1, 1, hText - 2);
			g.fillRect(xText + 1, yText + hText, wText - 2, 1);
			g.setColor(colorButton[index][2]);
			g.fillRect(xText + 1, yText + 1, wText - 2, hText / 2 - 1);
			g.setColor(colorButton[index][3]);
			g.fillRect(xText + 1, yText + 1 + hText / 2 - 1, wText - 2, hText - hText / 2 - 1);
			g.setColor(colorButton[index][0]);
			g.fillRect(xText, yText + 1, 1, hText - 2);
			g.fillRect(xText + 1, yText, wText - 2, 1);
			g.setColor(colorButton[index][1]);
			g.fillRect(xText + wText - 1, yText + 1, 1, hText - 2);
			g.fillRect(xText + 1, yText + hText - 1, wText - 2, 1);
		}
		else
		{
			paintImgButton(g, xText, yText, wText, hText, index);
		}
	}

	public static void paintImgButton(mGraphics g, int xText, int yText, int wText, int hText, int index)
	{
		wText += wText % 2;
		hText += hText % 2;
		if (index >= colorRect.Length)
		{
			index = (sbyte)(colorRect.Length - 1);
		}
		g.setColor(colorButton[index][2]);
		g.fillRect(xText + 2, yText + 2, wText - 4, hText - 4);
		g.drawRegion(imgButton[0], 0, index * 20, 5, 5, 0, xText, yText, 0);
		g.drawRegion(imgButton[0], 0, index * 20 + 5, 5, 5, 0, xText + wText, yText, mGraphics.TOP | mGraphics.RIGHT);
		g.drawRegion(imgButton[0], 0, index * 20 + 10, 5, 5, 0, xText, yText + hText, mGraphics.BOTTOM | mGraphics.LEFT);
		g.drawRegion(imgButton[0], 0, index * 20 + 15, 5, 5, 0, xText + wText, yText + hText, mGraphics.BOTTOM | mGraphics.RIGHT);
		g.setColor(colorButton[index][3]);
		g.fillRect(xText + 3, yText + hText / 2, wText - 6, hText / 2 - 4);
		g.fillRect(xText + 4, yText + hText - 4, wText - 8, 1);
		g.drawRegion(imgButton[1], 0, index * 12, 15, 6, 0, xText + 3, yText + 3, 0);
		g.drawRegion(imgButton[1], 0, index * 12 + 6, 15, 6, 0, xText + wText - 3, yText + hText - 3, mGraphics.BOTTOM | mGraphics.RIGHT);
		g.setColor(colorButton[index][4]);
		g.fillRect(xText + 3, yText, wText - 6, 1);
		g.fillRect(xText + 3, yText + hText - 1, wText - 6, 1);
		g.fillRect(xText, yText + 3, 1, hText - 6);
		g.fillRect(xText + wText - 1, yText + 3, 1, hText - 6);
		g.setColor(colorButton[index][0]);
		g.fillRect(xText + 3, yText + 1, wText - 6, 1);
		g.fillRect(xText + 1, yText + 3, 1, hText - 6);
		g.setColor(colorButton[index][1]);
		g.fillRect(xText + 3, yText + hText - 2, wText - 6, 1);
		g.fillRect(xText + wText - 2, yText + 3, 1, hText - 6);
	}

	public static void paintBG_AChi(mGraphics g, int x, int y, int w, int h, int type)
	{
		if (mimgBgA == null)
		{
			mSystem.outz("paint bg low");
			paintBG_AChi_Low(g, x + 16, y + 16, w - 32, h - 32, type);
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 36;
		int num4 = 72;
		switch (type)
		{
		case 1:
			num2 = 22;
			num3 = 32;
			num4 = 68;
			break;
		case 2:
			num2 = 19;
			num3 = 32;
			num4 = 68;
			break;
		}
		int num5 = x;
		int num6 = y;
		int num7 = h;
		int num8 = w;
		g.drawImage(mimgBgA[num + num2], num5, num6, 0);
		g.drawImage(mimgBgA[2 + num + num2], num5 + num8 - 36, num6, 0);
		int num9 = (num8 - 73) / num3 + 1;
		for (int i = 0; i <= num9; i++)
		{
			if (i == num9)
			{
				g.drawImage(mimgBgA[1 + num + num2], num5 + num8 - num4, num6, 0);
			}
			else
			{
				g.drawImage(mimgBgA[1 + num + num2], num5 + num3 + i * num3, num6, 0);
			}
		}
		num9 = (num8 - 73) / 36 + 1;
		for (int j = 0; j < num9; j++)
		{
			if (j == num9 - 1)
			{
				g.drawImage(mimgBgA[7 + num], num5 + num8 - 72, num6 + num7 - 38, 0);
			}
			else
			{
				g.drawImage(mimgBgA[7 + num], num5 + 36 + j * 36, num6 + num7 - 38, 0);
			}
		}
		int num10 = (num7 - 36 - 39) / 38 + 1;
		for (int k = 0; k < num10; k++)
		{
			if (k == num10 - 1)
			{
				g.drawImage(mimgBgA[3 + num], num5 + 4, num6 + num7 - 76, 0);
				g.drawImage(mimgBgA[5 + num], num5 + num8 - 4 - 36, num6 + num7 - 76, 0);
			}
			else
			{
				g.drawImage(mimgBgA[3 + num], num5 + 4, num6 + 36 + k * 38, 0);
				g.drawImage(mimgBgA[5 + num], num5 + num8 - 4 - 36, num6 + 36 + k * 38, 0);
			}
		}
		g.drawImage(mimgBgA[4 + num], num5 + 4, num6 + num7 - 38, 0);
		g.drawImage(mimgBgA[6 + num], num5 + num8 - 36 - 4, num6 + num7 - 38, 0);
		num = 8;
		num5 = x + 12;
		num6 = y + 22;
		if (type == 2)
		{
			num6 = y + 26;
		}
		num7 = h - 30;
		num8 = w - 24;
		g.drawImage(mimgBgA[num], num5, num6, 0);
		g.drawImage(mimgBgA[2 + num], num5 + num8 - 36, num6, 0);
		num9 = (num8 - 73) / 36 + 1;
		for (int l = 0; l < num9; l++)
		{
			if (l == num9 - 1)
			{
				g.drawImage(mimgBgA[1 + num], num5 + num8 - 72, num6, 0);
				g.drawImage(mimgBgA[7 + num], num5 + num8 - 72, num6 + num7 - 36, 0);
			}
			else
			{
				g.drawImage(mimgBgA[1 + num], num5 + 36 + l * 36, num6, 0);
				g.drawImage(mimgBgA[7 + num], num5 + 36 + l * 36, num6 + num7 - 36, 0);
			}
		}
		num10 = (num7 - 36 - 37) / 36 + 1;
		for (int m = 0; m < num10; m++)
		{
			if (m == num10 - 1)
			{
				g.drawImage(mimgBgA[3 + num], num5, num6 + num7 - 72, 0);
				g.drawImage(mimgBgA[5 + num], num5 + num8 - 36, num6 + num7 - 72, 0);
			}
			else
			{
				g.drawImage(mimgBgA[3 + num], num5, num6 + 36 + m * 36, 0);
				g.drawImage(mimgBgA[5 + num], num5 + num8 - 36, num6 + 36 + m * 36, 0);
			}
		}
		g.drawImage(mimgBgA[4 + num], num5, num6 + num7 - 36, 0);
		g.drawImage(mimgBgA[6 + num], num5 + num8 - 36, num6 + num7 - 36, 0);
		g.setColor(16246726);
		g.fillRect(num5 + 35, num6 + 35, num8 - 70, num7 - 70);
		if (type == 0)
		{
			g.drawImage(mimgBgA[16], x + w / 2 - 84, y - 24, 0);
			g.drawImage(mimgBgA[18], x + w / 2 + 84 - 24, y - 24, 0);
			for (int n = 0; n < 5; n++)
			{
				g.drawImage(mimgBgA[17], x + w / 2 - 84 + 24 + n * 24, y - 24, 0);
			}
		}
	}

	public static void paintThongThao(mGraphics g, int x, int y, int w, int h)
	{
		if (GameCanvas.lowGraphic)
		{
			paintRectLowGraphic(g, x, y, w, h, 1);
			return;
		}
		if (mimgBgB == null)
		{
			LoadImageStatic.loadImageBgB();
		}
		int num = 0;
		int num2 = x;
		int num3 = y;
		int num4 = h;
		int num5 = w;
		num2 = x;
		num3 = y;
		num4 = h;
		num5 = w;
		g.drawImage(mimgBgB[num], num2, num3, 0);
		g.drawImage(mimgBgB[2 + num], num2 + num5 - 36, num3, 0);
		int num6 = (num5 - 73) / 36 + 1;
		for (int i = 0; i < num6; i++)
		{
			if (i == num6 - 1)
			{
				g.drawImage(mimgBgB[1 + num], num2 + num5 - 72, num3, 0);
				g.drawImage(mimgBgB[7 + num], num2 + num5 - 72, num3 + num4 - 36, 0);
			}
			else
			{
				g.drawImage(mimgBgB[1 + num], num2 + 36 + i * 36, num3, 0);
				g.drawImage(mimgBgB[7 + num], num2 + 36 + i * 36, num3 + num4 - 36, 0);
			}
		}
		int num7 = (num4 - 36 - 37) / 36 + 1;
		for (int j = 0; j < num7; j++)
		{
			if (j == num7 - 1)
			{
				g.drawImage(mimgBgB[3 + num], num2, num3 + num4 - 72, 0);
				g.drawImage(mimgBgB[5 + num], num2 + num5 - 36, num3 + num4 - 72, 0);
			}
			else
			{
				g.drawImage(mimgBgB[3 + num], num2, num3 + 36 + j * 36, 0);
				g.drawImage(mimgBgB[5 + num], num2 + num5 - 36, num3 + 36 + j * 36, 0);
			}
		}
		g.drawImage(mimgBgB[4 + num], num2, num3 + num4 - 36, 0);
		g.drawImage(mimgBgB[6 + num], num2 + num5 - 36, num3 + num4 - 36, 0);
		g.setColor(15392973);
		g.fillRect(num2 + 35, num3 + 35, num5 - 70, num4 - 70);
	}

	public static void paintRuongVip(mGraphics g, int x, int y, int w, int h)
	{
		if (GameCanvas.lowGraphic)
		{
			paintRectLowGraphic(g, x, y, w, h, 1);
			return;
		}
		if (mimgBgC == null)
		{
			LoadImageStatic.loadImageBgC();
		}
		int num = 0;
		int num2 = x;
		int num3 = y;
		int num4 = h;
		int num5 = w;
		num2 = x;
		num3 = y;
		num4 = h;
		num5 = w;
		g.drawImage(mimgBgC[num], num2, num3, 0);
		g.drawImage(mimgBgC[2 + num], num2 + num5 - 46, num3, 0);
		int num6 = (num5 - 93) / 46 + 1;
		for (int i = 0; i < num6; i++)
		{
			if (i == num6 - 1)
			{
				g.drawImage(mimgBgC[1 + num], num2 + num5 - 92, num3, 0);
				g.drawImage(mimgBgC[7 + num], num2 + num5 - 92, num3 + num4 - 46, 0);
			}
			else
			{
				g.drawImage(mimgBgC[1 + num], num2 + 46 + i * 46, num3, 0);
				g.drawImage(mimgBgC[7 + num], num2 + 46 + i * 46, num3 + num4 - 46, 0);
			}
		}
		int num7 = (num4 - 46 - 47) / 46 + 1;
		for (int j = 0; j < num7; j++)
		{
			if (j == num7 - 1)
			{
				g.drawImage(mimgBgC[3 + num], num2, num3 + num4 - 92, 0);
				g.drawImage(mimgBgC[5 + num], num2 + num5 - 46, num3 + num4 - 72, 0);
			}
			else
			{
				g.drawImage(mimgBgC[3 + num], num2, num3 + 46 + j * 46, 0);
				g.drawImage(mimgBgC[5 + num], num2 + num5 - 46, num3 + 46 + j * 46, 0);
			}
		}
		g.drawImage(mimgBgC[4 + num], num2, num3 + num4 - 46, 0);
		g.drawImage(mimgBgC[6 + num], num2 + num5 - 46, num3 + num4 - 46, 0);
		g.setColor(16709599);
		g.fillRect(num2 + 45, num3 + 45, num5 - 90, num4 - 90);
	}

	public static void paintBG_Wanted_Room(mGraphics g, int x, int y, int w, int h)
	{
		if (mimgBgA == null)
		{
			paintBG_AChi_Low(g, x + 16, y + 16, w - 32, h - 32, 2);
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 36;
		int num4 = 72;
		num2 = 19;
		num3 = 32;
		num4 = 68;
		g.setColor(14850638);
		g.fillRect(x + 36, y + 36, w - 72, h - 72);
		g.drawImage(mimgBgA[num + num2], x, y, 0);
		g.drawImage(mimgBgA[2 + num + num2], x + w - 36, y, 0);
		int num5 = (w - 73) / num3 + 1;
		for (int i = 0; i <= num5; i++)
		{
			if (i == num5)
			{
				g.drawImage(mimgBgA[1 + num + num2], x + w - num4, y, 0);
			}
			else
			{
				g.drawImage(mimgBgA[1 + num + num2], x + num3 + i * num3, y, 0);
			}
		}
		num5 = (w - 73) / 36 + 1;
		for (int j = 0; j < num5; j++)
		{
			if (j == num5 - 1)
			{
				g.drawImage(mimgBgA[7 + num], x + w - 72, y + h - 38, 0);
			}
			else
			{
				g.drawImage(mimgBgA[7 + num], x + 36 + j * 36, y + h - 38, 0);
			}
		}
		int num6 = (h - 36 - 39) / 38 + 1;
		for (int k = 0; k < num6; k++)
		{
			if (k == num6 - 1)
			{
				g.drawImage(mimgBgA[3 + num], x + 4, y + h - 76, 0);
				g.drawImage(mimgBgA[5 + num], x + w - 4 - 36, y + h - 76, 0);
				g.drawImage(mimgBgA[25 + num], x + w - 72 - 4 - 36, y + h - 76, 0);
				g.drawImage(mimgBgA[25 + num], x + w - 36 - 4 - 36, y + h - 76, 0);
			}
			else
			{
				g.drawImage(mimgBgA[3 + num], x + 4, y + 36 + k * 38, 0);
				g.drawImage(mimgBgA[5 + num], x + w - 4 - 36, y + 36 + k * 38, 0);
				g.drawImage(mimgBgA[25 + num], x + w - 72 - 4 - 36, y + 36 + k * 38, 0);
				g.drawImage(mimgBgA[25 + num], x + w - 36 - 4 - 36, y + 36 + k * 38, 0);
			}
		}
		g.drawImage(mimgBgA[4 + num], x + 4, y + h - 38, 0);
		g.drawImage(mimgBgA[6 + num], x + w - 36 - 4, y + h - 38, 0);
	}

	public static void paintBG_AChi_Low(mGraphics g, int x, int y, int w, int h, int type)
	{
		if (type == 0)
		{
			paintRectLowGraphic(g, x + w / 2 - 84, y - 5 - 16, 168, 22, 1);
			x += 10;
			w -= 20;
			y += 14;
			h -= 18;
		}
		paintRectLowGraphic(g, x, y, w, h, type);
	}

	public static void paintBG_WantedFull(mGraphics g, int x, int y)
	{
		if (imgWanted != null)
		{
			g.drawImage(imgWanted, x, y, 0);
		}
	}

	public static void paintBG_Wanted2(mGraphics g, int x, int y, int w, int h, int type)
	{
		if (demo != null)
		{
			if (GameMidlet.DEVICE != 0)
			{
				g.drawImage(demo, x, y, 0);
			}
			else
			{
				paintBGWanted(g, x, y, w, h);
			}
		}
	}

	public static void paintBGWanted(mGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(mimgWanted2[8], x + 30, y + 30, 0);
		g.drawImage(mimgWanted2[9], x + 30 + 30, y + 30, 0);
		g.drawImage(mimgWanted2[10], x + 30 + 30 + 30, y + 30, 0);
		g.drawImage(mimgWanted2[11], x + 30, y + 30 + 30, 0);
		g.drawImage(mimgWanted2[8], x + 30 + 30, y + 30 + 30, 0);
		g.drawImage(mimgWanted2[9], x + 30 + 30 + 30, y + 30 + 30, 0);
		g.drawImage(mimgWanted2[10], x + 30, y + 30 + 30 + 30, 0);
		g.drawImage(mimgWanted2[11], x + 30 + 30, y + 30 + 30 + 30, 0);
		g.drawImage(mimgWanted2[8], x + 30 + 30 + 30, y + 30 + 30 + 30, 0);
		g.drawImage(mimgWanted2[11], x + 30, y + 30 + 30 + 30 + 30, 0);
		g.drawImage(mimgWanted2[8], x + 30 + 30, y + 30 + 30 + 30 + 30, 0);
		g.drawImage(mimgWanted2[9], x + 30 + 30 + 30, y + 30 + 30 + 30 + 30, 0);
		g.drawImage(mimgWanted2[7], x, y, 0);
		g.drawImage(mimgWanted2[5], x, y + h - 30, 0);
		g.drawImage(mimgWanted2[6], x + w - 30, y, 0);
		g.drawImage(mimgWanted2[4], x + w - 30, y + h - 30, 0);
		g.drawImage(mimgWanted2[22], x + 30, y, 0);
		g.drawImage(mimgWanted2[23], x + 30 + 30, y, 0);
		g.drawImage(mimgWanted2[24], x + 30 + 30 + 15, y, 0);
		g.drawImage(mimgWanted2[25], x + 30 + 30 + 15 + 15, y, 0);
		g.drawImage(mimgWanted2[17], x, y + 30, 0);
		g.drawImage(mimgWanted2[18], x, y + 30 + 30, 0);
		g.drawImage(mimgWanted2[19], x, y + 30 + 30 + 15, 0);
		g.drawImage(mimgWanted2[20], x, y + 30 + 30 + 15 + 15, 0);
		g.drawImage(mimgWanted2[21], x, y + 30 + 30 + 15 + 15 + 15, 0);
		g.drawImage(mimgWanted2[21], x, y + 30 + 30 + 15 + 15 + 15 + 15, 0);
		g.drawImage(mimgWanted2[0], x + 30, y + h - 30, 0);
		g.drawImage(mimgWanted2[1], x + 30 + 30, y + h - 30, 0);
		g.drawImage(mimgWanted2[2], x + 30 + 30 + 15, y + h - 30, 0);
		g.drawImage(mimgWanted2[3], x + 30 + 30 + 15 + 15, y + h - 30, 0);
		g.drawImage(mimgWanted2[12], x + w - 30, y + 30, 0);
		g.drawImage(mimgWanted2[13], x + w - 30, y + 30 + 30, 0);
		g.drawImage(mimgWanted2[14], x + w - 30, y + 30 + 30 + 15, 0);
		g.drawImage(mimgWanted2[15], x + w - 30, y + 30 + 30 + 15 + 15, 0);
		g.drawImage(mimgWanted2[15], x + w - 30, y + 30 + 30 + 15 + 15 + 15, 0);
		g.drawImage(mimgWanted2[15], x + w - 30, y + 30 + 30 + 15 + 15 + 15 + 15, 0);
		g.drawImage(mimgWanted2[26], x + w / 2, y + 20, 3);
		g.drawImage(mimgWanted2[27], x + w / 2, y + 15 + 20, 3);
		g.drawImage(mimgWanted2[27], x + w / 2, y + h - 15, 3);
		g.drawImage(mimgWanted2[28], x + w / 2, y + 100, 3);
	}

	public static void paintBG_Wanted(mGraphics g, int x, int y, int w, int h, int type)
	{
		int num = w;
		if (mimgWanted == null)
		{
			num = 0;
		}
		switch (type)
		{
		case 0:
			paintBG_Left_Right_Wanted(g, x - 14, y - 6, w, h, 0);
			paintBG_Left_Right_Wanted(g, x - 8, y - 3, w, h, 0);
			break;
		case 1:
			paintBG_Left_Right_Wanted(g, x + num + 14, y + 6, w, h, 1);
			paintBG_Left_Right_Wanted(g, x + num + 8, y + 3, w, h, 1);
			break;
		case 2:
			paintBG_Left_Right_Wanted(g, x - 8, y - 3, w, h, 0);
			paintBG_Left_Right_Wanted(g, x + num + 8, y + 3, w, h, 1);
			break;
		}
		if (mimgWanted == null)
		{
			if (GameMidlet.DEVICE != 0)
			{
				paintBG_WantedFull(g, x, y);
				return;
			}
			paintBG_AChi_Low(g, x, y, w, h, 5);
			FontBorderColor(g, T.wanted, x + w / 2, y + GameCanvas.hCommand / 4, 2, 0, 7);
			g.setColor(9530962);
			g.fillRect(x + 16, y + 24, w - 32, 62);
			g.fillRect(x + 16, y + 98, w - 32, 20);
			g.drawImage(imgBeri, x + 20, y + 130, 3);
			return;
		}
		int num2 = 0;
		int num3 = 0;
		int num4 = 38;
		int num5 = 53;
		g.setColor(14402719);
		g.fillRect(x + 10, y + 10, w - 20, h - 20);
		g.drawImage(mimgWanted[num2 + num3], x, y, 0);
		g.drawImage(mimgWanted[2 + num2 + num3], x + w - 15, y, 0);
		int num6 = (w - 30) / num4;
		for (int i = 0; i <= num6; i++)
		{
			if (i == num6)
			{
				g.drawImage(mimgWanted[1 + num2 + num3], x + w - num5, y, 0);
				g.drawImage(mimgWanted[7 + num2], x + w - num5, y + h - 15, 0);
			}
			else
			{
				g.drawImage(mimgWanted[1 + num2 + num3], x + 15 + i * num4, y, 0);
				g.drawImage(mimgWanted[7 + num2], x + 15 + i * num4, y + h - 15, 0);
			}
		}
		int num7 = (h - 30) / 40;
		for (int j = 0; j <= num7; j++)
		{
			if (j == num7)
			{
				g.drawImage(mimgWanted[3 + num2], x, y + h - 55, 0);
				g.drawImage(mimgWanted[5 + num2], x + w - 15, y + h - 55, 0);
			}
			else
			{
				g.drawImage(mimgWanted[3 + num2], x, y + 15 + j * 40, 0);
				g.drawImage(mimgWanted[5 + num2], x + w - 15, y + 15 + j * 40, 0);
			}
		}
		g.drawImage(mimgWanted[4 + num2], x, y + h - 15, 0);
		g.drawImage(mimgWanted[6 + num2], x + w - 15, y + h - 15, 0);
		g.drawImage(mimgWanted[12], x + w / 2, y + 12, 3);
		g.setColor(13019772);
		g.fillRect(x + 15, y + 22, w - 30, 66);
		g.fillRect(x + 15, y + 100, w - 32, 16);
		g.setColor(10781286);
		g.fillRect(x + 16, y + 23, w - 30, 64);
		g.fillRect(x + 16, y + 101, w - 32, 14);
		g.drawImage(mimgWanted[13], x + w / 2, y + 94, 3);
		g.drawImage(mimgWanted[14], x + 23, y + 130, 3);
		g.drawImage(mimgWanted[16], x + 7, y + 93, 0);
		g.drawRegion(mimgWanted[16], 0, 0, 6, 46, 2, x + w - 13, y + 93, 0);
		g.drawImage(mimgWanted[15], x + 102, y + 145, 3);
		g.drawImage(mimgWanted[17], x + 48, y + 144, 3);
	}

	public static void paintBG_Left_Right_Wanted(mGraphics g, int x, int y, int w, int h, int type)
	{
		h = 154;
		if (GameCanvas.lowGraphic)
		{
			paintBG_AChi_Low(g, x, y, w, h, 5);
			return;
		}
		if (mimgWanted == null)
		{
			if (GameMidlet.DEVICE != 0)
			{
				paintBG_WantedFull(g, x, y);
			}
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = x;
		int num4 = h;
		int num5 = 38;
		int num6 = 53;
		if (type == 0)
		{
			g.drawImage(mimgWanted[num + num2], num3, y, 0);
			g.drawImage(mimgWanted[4 + num], num3, y + num4 - 15, 0);
			g.drawImage(mimgWanted[2 + num], num3 + w - 15, y, 0);
		}
		else
		{
			num3 = x - 15;
			g.drawImage(mimgWanted[2 + num + num2], num3, y, 0);
			g.drawImage(mimgWanted[6 + num], num3, y + num4 - 15, 0);
			g.drawImage(mimgWanted[4 + num], num3 - w + 15, y + num4 - 15, 0);
		}
		int num7 = (w - 30) / num5;
		for (int i = 0; i <= num7; i++)
		{
			if (type == 0)
			{
				if (i == num7)
				{
					g.drawImage(mimgWanted[1 + num], num3 + w - num6, y, 0);
				}
				else
				{
					g.drawImage(mimgWanted[1 + num], num3 + 15 + i * num5, y, 0);
				}
			}
			else if (i == num7)
			{
				g.drawImage(mimgWanted[7 + num], num3 - w + 30, y + num4 - 15, 0);
			}
			else
			{
				g.drawImage(mimgWanted[7 + num], num3 - 38 - i * num5, y + num4 - 15, 0);
			}
		}
		int num8 = (num4 - 30) / 40;
		for (int j = 0; j <= num8; j++)
		{
			if (j == num8)
			{
				if (type == 0)
				{
					g.drawImage(mimgWanted[3 + num], num3, y + num4 - 55, 0);
				}
				else
				{
					g.drawImage(mimgWanted[5 + num], num3, y + num4 - 55, 0);
				}
			}
			else if (type == 0)
			{
				g.drawImage(mimgWanted[3 + num], num3, y + 15 + j * 40, 0);
			}
			else
			{
				g.drawImage(mimgWanted[5 + num], num3, y + 15 + j * 40, 0);
			}
		}
	}

	public void paintRectNice(mGraphics g, int x, int y, int w, int h, sbyte indexType)
	{
		g.setColor(4521779);
		g.fillRect(x, y, w, h);
	}

	public static void paintRectLowGraphic(mGraphics g, int x, int y, int w, int h, int indexColor)
	{
		g.setColor(16446420);
		g.fillRect(x, y, w, h);
		g.setColor(colorLow[indexColor]);
		g.fillRect(x + 1, y + 1, w - 2, h - 2);
	}

	public static void Font3dWhite(mGraphics g, string str, int x, int y, int ar)
	{
		mFont.tahoma_7b_black.drawString(g, str, x + 1, y + 1, ar);
		mFont.tahoma_7b_white.drawString(g, str, x, y, ar);
	}

	public static void Font3dColor(mGraphics g, string str, int x, int y, int ar, sbyte color)
	{
		mFont.tahoma_7b_black.drawString(g, str, x + 1, y + 1, ar);
		setTextColorName(color).drawString(g, str, x, y, ar);
	}

	public static void Font3dSmall(mGraphics g, string str, int x, int y, int ar, sbyte color)
	{
		mFont.tahoma_7_black.drawString(g, str, x + 1, y + 1, ar);
		setTextColor(color).drawString(g, str, x, y, ar);
	}

	public static void Font3dColor(mGraphics g, string str, int x, int y, int ar, sbyte colorShadow, sbyte colorFont)
	{
		setTextColorName(colorShadow).drawString(g, str, x + 1, y + 1, ar);
		setTextColorName(colorFont).drawString(g, str, x, y, ar);
	}

	public virtual void paintSelect(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16774758);
		g.fillRect(x, y, w, h);
	}

	public static mFont setTextColor(int id)
	{
		return id switch
		{
			0 => mFont.tahoma_7_white, 
			4 => mFont.tahoma_7_blue, 
			5 => mFont.tahoma_7_yellow, 
			2 => mFont.tahoma_7_violet, 
			3 => mFont.tahoma_7_orange, 
			1 => mFont.tahoma_7_green, 
			6 => mFont.tahoma_7_red, 
			7 => mFont.tahoma_7_black, 
			_ => mFont.tahoma_7_white, 
		};
	}

	public static mFont setTextColorName(int id)
	{
		return id switch
		{
			0 => mFont.tahoma_7b_white, 
			4 => mFont.tahoma_7b_blue, 
			5 => mFont.tahoma_7b_yellow, 
			2 => mFont.tahoma_7b_violet, 
			3 => mFont.tahoma_7b_orange, 
			1 => mFont.tahoma_7b_green, 
			6 => mFont.tahoma_7b_red, 
			7 => mFont.tahoma_7b_black, 
			8 => mFont.tahoma_7b_brown, 
			_ => mFont.tahoma_7b_white, 
		};
	}

	public void paintPaper(mGraphics g, int x, int y, int w, int h, int maxw, int type)
	{
		if (GameCanvas.lowGraphic)
		{
			paintRect(g, x + 4, y + 4, w - 8, h - 8, 1, 3);
			return;
		}
		int color = 16312512;
		mImage[] array;
		if (type == PAPER_LAW)
		{
			if (imgPaper_Law == null)
			{
				load_Img_Law();
				return;
			}
			array = imgPaper_Law;
			color = 10009570;
		}
		else
		{
			array = imgPaper;
		}
		if (h % 2 == 0)
		{
			h++;
		}
		g.setColor(color);
		g.fillRect(x, y + 5, w, h - 10);
		int num = 15;
		int num2 = w / 2 - 15;
		if (num2 < 0)
		{
			num2 = 0;
		}
		for (int i = 0; i <= num2; i += 30)
		{
			g.drawImage(array[5], x + w / 2 + i - 15, y + 3, 0);
			g.drawImage(array[6], x + w / 2 + i - 15, y + h - 1 - 8, 0);
			if (i != 0)
			{
				g.drawImage(array[5], x + w / 2 - i - 15, y + 3, 0);
				g.drawImage(array[6], x + w / 2 - i - 15, y + h - 1 - 8, 0);
			}
			num = i + 15;
		}
		int num3 = (w / 2 - 15) % 30;
		if (num3 != 0 && num3 > 0)
		{
			g.drawRegion(array[5], 0, 0, num3, 8, 0, x + w / 2 + num, y + 3, 0);
			g.drawRegion(array[6], 0, 0, num3, 8, 0, x + w / 2 + num, y + h - 1 - 8, 0);
			g.drawRegion(array[5], 0, 0, num3, 8, 0, x + w / 2 - num - num3, y + 3, 0);
			g.drawRegion(array[6], 0, 0, num3, 8, 0, x + w / 2 - num - num3, y + h - 1 - 8, 0);
		}
		if (w >= 20)
		{
			for (int j = 0; j < h - 38; j += 10)
			{
				g.drawRegion(array[7], 0, 0, 8, 10, 2, x + w, y + 19 + j, mGraphics.RIGHT | mGraphics.TOP);
			}
			g.drawRegion(array[3], 0, 0, 8, 16, 2, x + w, y + 3, mGraphics.RIGHT | mGraphics.TOP);
			g.drawRegion(array[4], 0, 0, 8, 17, 2, x + w, y + h - 2 - 16, mGraphics.RIGHT | mGraphics.TOP);
		}
		if (w >= 20)
		{
			for (int k = 0; k < h - 37; k += 10)
			{
				g.drawImage(array[7], x, y + 19 + k, 0);
			}
			g.drawImage(array[3], x, y + 3, 0);
			g.drawImage(array[4], x, y + h - 2 - 16, 0);
		}
		for (int l = 0; l < h - 37; l += 10)
		{
			g.drawRegion(array[1], 0, 0, 14, 10, 2, x + w + 14, y + 19 + l, mGraphics.RIGHT | mGraphics.TOP);
		}
		g.drawRegion(array[0], 0, 0, 14, 19, 2, x + w + 14, y, mGraphics.RIGHT | mGraphics.TOP);
		g.drawRegion(array[2], 0, 0, 14, 19, 2, x + w + 14, y + h - 19, mGraphics.RIGHT | mGraphics.TOP);
		for (int m = 0; m < h - 38; m += 10)
		{
			g.drawImage(array[1], x - 14, y + 19 + m, 0);
		}
		g.drawImage(array[0], x - 14, y, 0);
		g.drawImage(array[2], x - 14, y + h - 19, 0);
	}

	public void load_Img_Law()
	{
		imgPaper_Law = new mImage[8];
		for (int i = 0; i < imgPaper_Law.Length; i++)
		{
			imgPaper_Law[i] = mImage.createImage("/interface/papern" + i + ".png");
		}
		imgEff_Law = new mImage[8];
		for (int j = 0; j < imgEff_Law.Length; j++)
		{
			imgEff_Law[j] = mImage.createImage("/interface/law" + j + ".png");
		}
	}

	public void paintPaper_UpDown(mGraphics g, int x, int y, int w, int h, int maxw)
	{
		if (imgPaperDoc == null)
		{
			paintPaper(g, x, y - 4, w, h + 4, maxw, PAPER_NORMAL);
			return;
		}
		if (w % 2 == 0)
		{
			w++;
		}
		for (int i = 0; i < w - 38; i += 10)
		{
			g.drawRegion(imgPaperDoc[1], 0, 0, 10, 14, 0, x - 2 + 19 + i, y + 9, mGraphics.LEFT | mGraphics.BOTTOM);
		}
		g.drawRegion(imgPaperDoc[2], 0, 0, 19, 14, 0, x - 1, y + 9, mGraphics.LEFT | mGraphics.BOTTOM);
		g.drawRegion(imgPaperDoc[2], 0, 0, 19, 14, 2, x - 2 + w - 19, y + 9, mGraphics.LEFT | mGraphics.BOTTOM);
		g.setColor(16312512);
		g.fillRect(x + 5, y, w - 10, h);
		int num = 15;
		int num2 = h / 2 - 15;
		if (num2 < 0)
		{
			num2 = 0;
		}
		for (int j = 0; j <= num2; j += 30)
		{
			g.drawRegion(imgPaperDoc[6], 0, 0, 8, 30, 0, x + 3, y + h / 2 + j - 15, 0);
			g.drawRegion(imgPaperDoc[6], 0, 0, 8, 30, 2, x + w - 1 - 8, y + h / 2 + j - 15, 0);
			if (j != 0)
			{
				g.drawRegion(imgPaperDoc[6], 0, 0, 8, 30, 0, x + 3, y + h / 2 - j - 15, 0);
				g.drawRegion(imgPaperDoc[6], 0, 0, 8, 30, 2, x + w - 1 - 8, y + h / 2 - j - 15, 0);
			}
			num = j + 15;
		}
		int num3 = (h / 2 - 15) % 30;
		if (num3 != 0 && num3 > 0)
		{
			g.drawRegion(imgPaperDoc[6], 0, 0, 8, num3, 0, x + 3, y + h / 2 + num, 0);
			g.drawRegion(imgPaperDoc[6], 0, 0, 8, num3, 2, x + w - 1 - 8, y + h / 2 + num, 0);
			g.drawRegion(imgPaperDoc[6], 0, 0, 8, num3, 0, x + 3, y + h / 2 - num - num3, 0);
			g.drawRegion(imgPaperDoc[6], 0, 0, 8, num3, 2, x + w - 1 - 8, y + h / 2 - num - num3, 0);
		}
		if (h >= 20)
		{
			for (int k = 0; k < w - 38; k += 10)
			{
				g.drawRegion(imgPaperDoc[7], 0, 0, 10, 8, 0, x + 19 + k, y, 0);
			}
			g.drawRegion(imgPaperDoc[4], 0, 0, 17, 8, 0, x + 3, y, 0);
			g.drawRegion(imgPaperDoc[4], 0, 0, 17, 8, 2, x + w - 2 - 16, y, 0);
		}
		if (h >= 20)
		{
			for (int l = 0; l < w - 38; l += 10)
			{
				g.drawRegion(imgPaperDoc[7], 0, 0, 10, 8, 1, x + 19 + l, y + h, mGraphics.LEFT | mGraphics.BOTTOM);
			}
			g.drawRegion(imgPaperDoc[4], 0, 0, 17, 8, 1, x + 3, y + h, mGraphics.LEFT | mGraphics.BOTTOM);
			g.drawRegion(imgPaperDoc[4], 0, 0, 17, 8, 3, x + w - 2 - 16, y + h, mGraphics.LEFT | mGraphics.BOTTOM);
		}
		for (int m = 0; m < w - 38; m += 10)
		{
			g.drawRegion(imgPaperDoc[1], 0, 0, 10, 14, 1, x + 2 + 19 + m, y + h, 0);
		}
		g.drawRegion(imgPaperDoc[2], 0, 0, 19, 14, 1, x + 2, y + h, 0);
		g.drawRegion(imgPaperDoc[2], 0, 0, 19, 14, 3, x + 2 + w - 19, y + h, 0);
	}

	public static string getDotNumber(long m)
	{
		string text = string.Empty;
		bool flag = ((m < 0) ? true : false);
		if (flag)
		{
			m = -m;
		}
		long num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			if (m >= 1000)
			{
				long num2 = m % 1000;
				text = ((num2 != 0L) ? ((num2 >= 10) ? ((num2 >= 100) ? ("." + num2 + text) : (".0" + num2 + text)) : (".00" + num2 + text)) : (".000" + text));
				m /= 1000;
				continue;
			}
			text = m + text;
			break;
		}
		if (flag)
		{
			text = "-" + text;
		}
		return text;
	}

	public static string getMoneyShortText(long m)
	{
		_ = string.Empty;
		if (m >= 1000000000)
		{
			return m / 1000000000 + "," + m % 1000000000 / 100000000 + "B";
		}
		if (m > 1000000)
		{
			return m / 1000000 + "," + m % 1000000 / 100000 + "M";
		}
		return getDotNumber(m);
	}

	public static void FontBorderColor(mGraphics g, string str, int x, int y, int ar, int color, int colorBorder)
	{
		setTextColorName(colorBorder).drawString(g, str, x - 1, y - 1, ar);
		setTextColorName(colorBorder).drawString(g, str, x - 1, y + 1, ar);
		setTextColorName(colorBorder).drawString(g, str, x + 1, y - 1, ar);
		setTextColorName(colorBorder).drawString(g, str, x + 1, y + 1, ar);
		setTextColorName(colorBorder).drawString(g, str, x - 1, y, ar);
		setTextColorName(colorBorder).drawString(g, str, x + 1, y, ar);
		setTextColorName(colorBorder).drawString(g, str, x, y - 1, ar);
		setTextColorName(colorBorder).drawString(g, str, x, y + 1, ar);
		setTextColorName(color).drawString(g, str, x, y, ar);
	}

	public static void FontSevenColor(mGraphics g, string str, int x, int y, int ar, int colorBorder)
	{
		if (colorBorder == -1)
		{
			mFont.tahoma_7b_black.drawString(g, str, x + 1, y + 1, ar);
		}
		else if (colorBorder >= 0)
		{
			setTextColorName(colorBorder).drawString(g, str, x - 1, y - 1, ar);
			setTextColorName(colorBorder).drawString(g, str, x - 1, y + 1, ar);
			setTextColorName(colorBorder).drawString(g, str, x + 1, y - 1, ar);
			setTextColorName(colorBorder).drawString(g, str, x + 1, y + 1, ar);
			setTextColorName(colorBorder).drawString(g, str, x - 1, y, ar);
			setTextColorName(colorBorder).drawString(g, str, x + 1, y, ar);
			setTextColorName(colorBorder).drawString(g, str, x, y - 1, ar);
			setTextColorName(colorBorder).drawString(g, str, x, y + 1, ar);
		}
		setTextColorName(mValue7Color[GameCanvas.gameTick / 7 % mValue7Color.Length]).drawString(g, str, x, y, ar);
	}

	public static void FontBorderSmall(mGraphics g, string str, int x, int y, int ar, int color)
	{
		mFont.tahoma_7_black.drawString(g, str, x - 1, y, ar);
		mFont.tahoma_7_black.drawString(g, str, x + 1, y, ar);
		mFont.tahoma_7_black.drawString(g, str, x, y - 1, ar);
		mFont.tahoma_7_black.drawString(g, str, x, y + 1, ar);
		setTextColor(color).drawString(g, str, x, y, ar);
	}

	public static void PaintName_Image(mGraphics g, short idIcon, string str, int x, int y, int ar, int color, int colorBorder)
	{
		int num = 0;
		MainImage image = Skill_Info.getImage(idIcon);
		if (image != null && image.img != null)
		{
			int imageWidth = mImage.getImageWidth(image.img.image);
			switch (ar)
			{
			case 2:
			{
				int width = mFont.tahoma_7b_black.getWidth(str);
				num = imageWidth / 2 + 1;
				g.drawImage(image.img, x - width / 2 - 2, y + 5, 3);
				break;
			}
			case 1:
				num = -2;
				g.drawImage(image.img, x, y + 5, mGraphics.VCENTER | mGraphics.LEFT);
				break;
			default:
				num = imageWidth + 2;
				g.drawImage(image.img, x, y + 5, mGraphics.VCENTER | mGraphics.LEFT);
				break;
			}
		}
		FontBorderColor(g, str, x + num, y, ar, color, colorBorder);
	}

	public static iCommand setPosCMD(iCommand cmd, int pos)
	{
		switch (pos)
		{
		case 1:
			if (GameCanvas.isTaiTho)
			{
				cmd.setPos(GameCanvas.wCommand - 3 + 5, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, cmd.caption);
			}
			else
			{
				cmd.setPos(GameCanvas.wCommand - 3, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, cmd.caption);
			}
			break;
		case 2:
			if (GameCanvas.isTaiTho)
			{
				cmd.setPos(MotherCanvas.w - GameCanvas.wCommand + 3 - 5, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, cmd.caption);
			}
			else
			{
				cmd.setPos(MotherCanvas.w - GameCanvas.wCommand + 3, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, cmd.caption);
			}
			break;
		case 0:
			cmd.setPos(MotherCanvas.hw, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, cmd.caption);
			break;
		}
		return cmd;
	}

	public void paintWantedPaper(mGraphics g, int x, int y, int width, int height)
	{
		g.translate(x, y);
		int num = 0;
		int num2 = 0;
		int num3 = width - 30;
		int num4 = height - 30;
		int num5 = 0;
		for (int i = num2 + 30; i < num4; i += 30)
		{
			for (int j = num + 30; j < num3; j += 30)
			{
				g.drawImage(imgGiua[num5], j, i, 0);
				num5++;
				if (num5 >= imgGiua.Length)
				{
					num5 = 0;
				}
			}
		}
		num5 = 0;
		int num6 = num + 30;
		while (num6 < num3)
		{
			g.drawImage(imgDuoi[num5], num6, num4, 0);
			g.drawImage(imgTren[num5], num6, num2, 0);
			num6 += mImage.getImageWidth(imgTren[num5].image);
			num5++;
			if (num5 >= imgTren.Length)
			{
				num5 = 0;
			}
		}
		num5 = 0;
		int num7 = num2 + 30;
		while (num7 < num4)
		{
			g.drawImage(imgTrai[num5], num, num7, 0);
			g.drawImage(imgPhai[num5], num3, num7, 0);
			num7 += mImage.getImageHeight(imgTrai[num5].image);
			num5++;
			if (num5 >= imgTrai.Length)
			{
				num5 = 0;
			}
		}
		g.drawImage(imgGoc[3], num3, num4, 0);
		g.drawImage(imgGoc[2], num, num4, 0);
		g.drawImage(imgGoc[1], num3, num2, 0);
		g.drawImage(imgGoc[0], num, num2, 0);
		g.translate(-x, -y);
	}

	public void paintBgDialogTrangTri(mGraphics g, int x, int y, int width, int height)
	{
		g.translate(x, y);
		int num = 0;
		int num2 = 0;
		int num3 = width - 25;
		int num4 = height - 25;
		for (int i = num + 25; i < num3; i += 25)
		{
			g.drawImage(imgDialogTrangTri[6], i, num4, 0);
			g.drawImage(imgDialogTrangTri[1], i, num2, 0);
		}
		for (int j = num2 + 25; j < num4; j += 25)
		{
			g.drawImage(imgDialogTrangTri[3], num, j, 0);
			g.drawImage(imgDialogTrangTri[4], num3, j, 0);
		}
		g.drawImage(imgDialogTrangTri[7], num3, num4, 0);
		g.drawImage(imgDialogTrangTri[5], num, num4, 0);
		g.drawImage(imgDialogTrangTri[2], num3, num2, 0);
		g.drawImage(imgDialogTrangTri[0], num, num2, 0);
		num = 20;
		num2 = 20;
		num3 = width - 20;
		num4 = height - 20;
		g.setClip(num, num2, num3 - num, num4 - num2);
		g.saveCanvas();
		g.ClipRec(num, num2, num3 - num, num4 - num2);
		for (int k = num2; k < num4; k += 36)
		{
			for (int l = num; l < num3; l += 36)
			{
				g.drawImage(imgDialogTrangTri[8], l, k, 0);
			}
		}
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		g.translate(x, y);
		num = 25;
		num2 = 25;
		num3 = width - 25 - 40;
		num4 = height - 25 - 40;
		for (int m = num + 40; m < num3; m += 40)
		{
			g.drawImage(imgDialogTrangTri[15], m, num4, 0);
			g.drawImage(imgDialogTrangTri[10], m, num2, 0);
		}
		for (int n = num2 + 40; n < num4; n += 40)
		{
			g.drawImage(imgDialogTrangTri[12], num, n, 0);
			g.drawImage(imgDialogTrangTri[13], num3, n, 0);
		}
		g.drawImage(imgDialogTrangTri[16], num3, num4, 0);
		g.drawImage(imgDialogTrangTri[14], num, num4, 0);
		g.drawImage(imgDialogTrangTri[11], num3, num2, 0);
		g.drawImage(imgDialogTrangTri[9], num, num2, 0);
		g.translate(-x, -y);
	}

	public void paintBgMemListPhatLenh(mGraphics g, int x, int y, int w, int h)
	{
		g.drawRegion(imgKhungMem, 0, 0, 10, 10, 0, x, y, 0);
		g.drawRegion(imgKhungMem, 0, 0, 10, 10, 2, x + w - 10, y, 0);
		g.drawRegion(imgKhungMem, 0, 0, 10, 10, 1, x, y + h - 10, 0);
		g.drawRegion(imgKhungMem, 0, 0, 10, 10, 7, x + w - 10, y + h - 10, 0);
		g.setColor(14203529);
		g.fillRect(x + 10, y, w - 20, h);
		g.fillRect(x, y + 10, w, h - 20);
	}

	public static void paintBg_WantedList(mGraphics g, int x, int y, int w, int h, int type, int wCmd)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 36;
		int num4 = 72;
		switch (type)
		{
		case 1:
			num2 = 22;
			num3 = 32;
			num4 = 68;
			break;
		case 2:
			num2 = 19;
			num3 = 32;
			num4 = 68;
			break;
		}
		int num5 = x - wCmd / 2;
		int num6 = y;
		int num7 = h;
		int num8 = w + wCmd;
		g.drawImage(mimgBgA[num + num2], num5, num6, 0);
		g.drawImage(mimgBgA[2 + num + num2], num5 + num8 - 36, num6, 0);
		int num9 = (num8 - 73) / num3 + 1;
		for (int i = 0; i <= num9; i++)
		{
			if (i == num9)
			{
				g.drawImage(mimgBgA[1 + num + num2], num5 + num8 - num4, num6, 0);
			}
			else
			{
				g.drawImage(mimgBgA[1 + num + num2], num5 + num3 + i * num3, num6, 0);
			}
		}
		num9 = (num8 - 73) / 36 + 1;
		for (int j = 0; j < num9; j++)
		{
			if (j == num9 - 1)
			{
				g.drawImage(mimgBgA[7 + num], num5 + num8 - 72, num6 + num7 - 38, 0);
			}
			else
			{
				g.drawImage(mimgBgA[7 + num], num5 + 36 + j * 36, num6 + num7 - 38, 0);
			}
		}
		int num10 = (num7 - 36 - 39) / 38 + 1;
		for (int k = 0; k < num10; k++)
		{
			g.drawImage(mimgBgA[3 + num], num5 + 4, num6 + 36 + k * 38, 0);
			g.drawImage(mimgBgA[5 + num], num5 + num8 - 4 - 36, num6 + 36 + k * 38, 0);
			for (int l = num5 + 4 + 36; l <= x + 12 + wCmd / 2; l += 36)
			{
				g.drawImage(mimgBgA[25], l, num6 + 36 + k * 38, 0);
			}
		}
		g.drawImage(mimgBgA[4 + num], num5 + 4, num6 + num7 - 38, 0);
		g.drawImage(mimgBgA[6 + num], num5 + num8 - 36 - 4, num6 + num7 - 38, 0);
		num5 = num5 + 4 + 10;
		num6 = y + 22 + 2;
		num7 = h - 30 - 2;
		num8 = x + 12 + wCmd / 2 - num5 - 10;
		g.drawRegion(imgKhung[1], 0, 0, 20, 20, 0, num5, num6, 0);
		g.drawRegion(imgKhung[1], 0, 0, 20, 20, 2, num5 + num8 - 20, num6, 0);
		g.drawRegion(imgKhung[1], 0, 0, 20, 20, 1, num5, num6 + num7 - 20, 0);
		g.drawRegion(imgKhung[1], 0, 0, 20, 20, 7, num5 + num8 - 20, num6 + num7 - 20, 0);
		g.setColor(9393716);
		g.fillRect(num5 + 20, num6, num8 - 40, num7);
		g.fillRect(num5, num6 + 20, num8, num7 - 40);
		num5 += 5;
		num6 += 13;
		for (int m = num6; m < num6 + num7 - 20; m += 30)
		{
			g.drawRegion(imgKhung[0], 0, 0, 79, 22, 0, num5, m, 0);
		}
		num = 8;
		num5 = x + 12 + wCmd / 2;
		num6 = y + 22;
		if (type == 2)
		{
			num6 = y + 26;
		}
		num7 = h - 30;
		num8 = w - 24;
		g.drawImage(mimgBgA[num], num5, num6, 0);
		g.drawImage(mimgBgA[2 + num], num5 + num8 - 36, num6, 0);
		num9 = (num8 - 73) / 36 + 1;
		for (int n = 0; n < num9; n++)
		{
			if (n == num9 - 1)
			{
				g.drawImage(mimgBgA[1 + num], num5 + num8 - 72, num6, 0);
				g.drawImage(mimgBgA[7 + num], num5 + num8 - 72, num6 + num7 - 36, 0);
			}
			else
			{
				g.drawImage(mimgBgA[1 + num], num5 + 36 + n * 36, num6, 0);
				g.drawImage(mimgBgA[7 + num], num5 + 36 + n * 36, num6 + num7 - 36, 0);
			}
		}
		num10 = (num7 - 36 - 37) / 36 + 1;
		for (int num11 = 0; num11 < num10; num11++)
		{
			if (num11 == num10 - 1)
			{
				g.drawImage(mimgBgA[3 + num], num5, num6 + num7 - 72, 0);
				g.drawImage(mimgBgA[5 + num], num5 + num8 - 36, num6 + num7 - 72, 0);
			}
			else
			{
				g.drawImage(mimgBgA[3 + num], num5, num6 + 36 + num11 * 36, 0);
				g.drawImage(mimgBgA[5 + num], num5 + num8 - 36, num6 + 36 + num11 * 36, 0);
			}
		}
		g.drawImage(mimgBgA[4 + num], num5, num6 + num7 - 36, 0);
		g.drawImage(mimgBgA[6 + num], num5 + num8 - 36, num6 + num7 - 36, 0);
		g.setColor(16246726);
		g.fillRect(num5 + 35, num6 + 35, num8 - 70, num7 - 70);
		if (type == 0)
		{
			g.drawImage(mimgBgA[16], x + w / 2 - 84, y - 24, 0);
			g.drawImage(mimgBgA[18], x + w / 2 + 84 - 24, y - 24, 0);
			for (int num12 = 0; num12 < 5; num12++)
			{
				g.drawImage(mimgBgA[17], x + w / 2 - 84 + 24 + num12 * 24, y - 24, 0);
			}
		}
	}
}
