using System;
using UnityEngine;

public class GameCanvas : MotherCanvas, IActionListener
{
	public const string VERSION = "1.2.7";

	public static int lv = 1;

	public static sbyte Day_Night = 0;

	public static sbyte language = 0;

	public const string No = "1";

	public static bool lowGraphic = false;

	public static bool isSuperLowGraphic = false;

	public new static GameCanvas instance;

	public static bool[] keyMyPressed = new bool[55];

	public static bool[] keyMyReleased = new bool[55];

	public static bool[] keyMyHold = new bool[55];

	public static bool isMoto;

	public static bool isBB = false;

	public static bool isPointerDown = false;

	public static bool isPointerRelease = false;

	public static bool isPointerSelect = false;

	public static bool isPointerMove = false;

	public static bool isPointerClick = false;

	public static bool isPointerEnd = false;

	public static bool isSmallScreen = false;

	public static bool isOffBg = false;

	public static bool isShortH = false;

	public static bool isTaiTho = true;

	public static bool isTouch;

	public static bool isDisConnect = false;

	public static int px;

	public static int py;

	public static int pxLast;

	public static int pyLast;

	public static long timeNow = 0L;

	public static long clockServer = 0L;

	public static int hCommand = 25;

	public static int hText = 15;

	public static int wCommand;

	public static int hLoad = 0;

	public static int gameTick;

	public static int gameTickChia4;

	public static int xPlus12;

	public static int yPlus12;

	public static int tickSelectChar = 0;

	public static int keyAsciiPress;

	public static mVector listPoint;

	public static sbyte DAY = 0;

	public static sbyte NIGHT = 1;

	public mGraphics g = new mGraphics();

	public static MainScreen currentScreen;

	public static MainDialog currentDialog;

	public static MainDialog subDialog;

	public static MainDialog DevilDialog;

	public static MainDialog showDialog;

	public static MsgDialogEvent EventDialog;

	public static MsgDialogTrangTri TrangTriDialog;

	public static GameScreen gameScr;

	public static LoadMap loadmap;

	public static ReadMessenge readMess = new ReadMessenge();

	public static LoginScreen loginScr;

	public static FristLoginScreen fristLoginScr;

	public static MapBackGround mapBack;

	public static MapBackGround mapLogin;

	public static SaveImageRMS saveImage = new SaveImageRMS();

	public static LoadMapScreen loadMapScr;

	public static Menu menuCur = new Menu();

	public static Menu menu = new Menu();

	public static ListChar_Screen listCharScr;

	public static TabScreen tabAllScr;

	public static TabScreen tabShopScr;

	public static TabScreen tabMarketScr;

	public static ChatTabScreen chatTabScr;

	public static MainEvent eventScr;

	public static SaveRms saveRms;

	public static TabInventory tabInven;

	public static TabInventory tabInvenClan;

	public static TabInventory tabInvenMarket;

	public static UpdateImageScreen updateImageAndroidScr;

	public static Clan_Screen ClanScr;

	public static Sudo_Screen SudoScr;

	public static mVector vecTest = new mVector("GameCanvas.vecTest");

	private bool isLoadFont;

	public static string infoDisConnect = string.Empty;

	public static string[][] strListServer = new string[2][]
	{
		new string[4] { "Hải Tặc", "Hải Tặc", "Hải Tặc", "Hải Tặc" },
		new string[1] { "Hải Tặc" }
	};

	public static int IndexServer = 0;

	public static int tickAction = 0;

	public static int percent = 0;

	public static int percentTemp = 0;

	public static int hardcodeSpeedUp = 300;

	public const int hardcodeSpeedUpFinal = 300;

	public static bool isDevTest = false;

	public static int lvSavedPC = 0;

	private ScreenBilak bilak;

	public static bool istest = false;

	public static bool isLoadImage = false;

	public static string strIP = string.Empty;

	public static int indexdownload = 0;

	public static bool isSendChatpopup = false;

	public static bool isQuickMenu = false;

	public const sbyte ACTION_UNI_TAB_SCREEN = 0;

	public const sbyte ACTION_UNI_1 = 1;

	public const sbyte ACTION_UNI_NEXT_FOCUS = 2;

	public const sbyte ACTION_UNI_3 = 3;

	public const sbyte ACTION_UNI_QUICK_MENU = 4;

	public const sbyte ACTION_UNI_5 = 5;

	public const sbyte ACTION_UNI_CHANGE_SKILL = 6;

	public const sbyte ACTION_UNI_7 = 7;

	public const sbyte ACTION_UNI_EVENT = 8;

	public const sbyte ACTION_UNI_9 = 9;

	public const sbyte ACTION_UNI_CHAT = 10;

	public const sbyte ACTION_UNI_INVENTORY = 11;

	public const sbyte ACTION_UNI_QUEST = 12;

	public const sbyte ACTION_UNI_INFO = 13;

	public const sbyte ACTION_UNI_SKILL = 14;

	public const sbyte ACTION_UNI_FRIEND_LIST = 15;

	public const sbyte ACTION_UNI_EQUIP = 16;

	public const sbyte OLD_LEFT = 12;

	public const sbyte OLD_RIGHT = 13;

	public const sbyte NEW_LEFT = 40;

	public const sbyte NEW_RIGHT = 41;

	public const sbyte NEW_CENTER = 5;

	public static sbyte isSSItem = 0;

	public static sbyte isTypeSpam = 0;

	public GameCanvas(Context context)
		: base(context)
	{
		initGame();
	}

	public GameCanvas()
	{
		initGame();
	}

	public void initGame()
	{
		sbyte[] array = CRes.loadRMS("Main_IPWORLD");
		if (array != null)
		{
			DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(array));
			try
			{
				language = dataInputStream.readByte();
				dataInputStream.close();
			}
			catch (Exception)
			{
				language = 0;
			}
		}
		if (language == 1)
		{
			T_E.loadT_E();
		}
		else
		{
			T.loadT();
		}
		instance = this;
		SaveImageRMS.start();
		TField.setVendorTypeMode(TField.NOKIA);
		isTouch = hasPointerEvents();
		Day_Night = get_Day_Night();
		if (MotherCanvas.w < 200 || MotherCanvas.h < 200)
		{
			isSmallScreen = true;
			iCommand.wButtonCmd = 60;
			iCommand.hButtonCmdNor = 20;
			iCommand.hButtonCmdSpec = 46;
			Scroll.hRectScroll = 16;
		}
		wCommand = 36;
		if (isTouch)
		{
			wCommand = 40;
			listPoint = new mVector();
			iCommand.hButtonCmdNor = 30;
			iCommand.hButtonCmdSpec = 46;
			iCommand.wButtonCmd = 70;
		}
		else if (isSmallScreen)
		{
			wCommand = 30;
		}
		if (GameMidlet.DEVICE != 0 && (mGraphics.zoomLevel > 1 || GameMidlet.DEVICE == 2))
		{
			mFont.loadmFont();
			isLoadFont = true;
		}
		sbyte[] array2 = CRes.loadRMS("Main_Load_Image_Android_OK");
		string text = string.Empty;
		if (array2 != null)
		{
			DataInputStream dataInputStream2 = new DataInputStream(new ByteArrayInputStream(array2));
			try
			{
				text = dataInputStream2.readUTF();
				dataInputStream2.close();
			}
			catch (Exception)
			{
				text = string.Empty;
			}
		}
		if (text.CompareTo("1.2.7") == 0 || GameMidlet.DEVICE == 0 || GameMidlet.DEVICE == 4)
		{
			beginGame();
			return;
		}
		updateImageAndroidScr = new UpdateImageScreen();
		updateImageAndroidScr.Show();
	}

	public void beginGame()
	{
		if (!isLoadFont)
		{
			mFont.loadmFont();
		}
		CRes.loadSinCos();
		mSound.init(7, 54);
		Player.hotkeyPlayer = new Hotkey[2][];
		for (int i = 0; i < Player.hotkeyPlayer.Length; i++)
		{
			Player.hotkeyPlayer[i] = new Hotkey[5];
			for (int j = 0; j < Player.hotkeyPlayer[i].Length; j++)
			{
				Player.hotkeyPlayer[i][j] = new Hotkey();
			}
		}
		if (language == 1)
		{
			for (int k = 0; k < GameMidlet.google_productIds.Length; k++)
			{
				GameMidlet.google_productIds[k] = GameMidlet.google_productIds_Eng[k];
			}
		}
		loadRMSGAME();
		LoadImageStatic.LoadAllImage();
		MsgDialog.hPlus = 15;
		MainScreen.cameraMain = new Camera();
		MainScreen.cameraSub = new Camera();
		chatTabScr = new ChatTabScreen();
		gameScr = new GameScreen();
		loginScr = new LoginScreen();
		loadmap = new LoadMap();
		loadMapScr = new LoadMapScreen();
		if (MotherCanvas.h < 220 && isTouch)
		{
			isShortH = true;
		}
		addTabStart();
		saveRms = new SaveRms();
		saveRms.loadBeginGame();
		fristLoginScr = new FristLoginScreen();
		eventScr = new MainEvent(-3, Player.vecEvent);
		fristLoginScr.Show();
		fristLoginScr.setBeginGame();
	}

	private void randomBilak()
	{
		bilak = new ScreenBilak();
		currentScreen = bilak;
	}

	public static void addTabStart()
	{
		new MainTab().createValue();
		tabAllScr = new TabScreen(MainTab.xTab, 0);
		mVector mVector2 = new mVector();
		tabInven = new TabInventory(T.tabInven, Player.vecInventory, 0, MainTab.xTab);
		tabInven.initCmd();
		mVector2.addElement(tabInven);
		TabEquip o = new TabEquip(T.tabEquip);
		mVector2.addElement(o);
		TabInfo o2 = new TabInfo(T.tabInfo);
		mVector2.addElement(o2);
		TabSkill o3 = new TabSkill(T.tabSkill);
		mVector2.addElement(o3);
		TabQuest o4 = new TabQuest(T.tabQuest);
		mVector2.addElement(o4);
		if (!isTouch)
		{
			TabMenu o5 = new TabMenu(T.setting, gameScr.getMenu());
			mVector2.addElement(o5);
		}
		tabAllScr.addVecTab(mVector2);
	}

	public override void paint(mGraphics gx)
	{
		try
		{
			if (isTaiTho)
			{
				g.setColor(0);
				g.fillRect(0, 0, MotherCanvas.w, MotherCanvas.h);
			}
			currentScreen.paint(g);
			resetTrans(g);
			if (GameScreen.h12plus > 0)
			{
				g.setColor(0);
				g.fillRect(0, 0, MotherCanvas.w, GameScreen.h12plus);
				if (AvMain.imgPlus12_2 == null)
				{
					AvMain.imgPlus12_2 = mImage.createImage("/interface/plus12_2.png");
				}
				else
				{
					g.drawImage(AvMain.imgPlus12_2, 0, 0, 0);
				}
				if (mFont.tahoma_7_white != null)
				{
					mFont.tahoma_7_white.drawString(g, T.plus12, xPlus12 + 13, 1, 0);
				}
			}
			if (subDialog != null)
			{
				subDialog.paint(g);
			}
			if (currentDialog != null)
			{
				currentDialog.paint(g);
			}
			else
			{
				if (menuCur.isShowMenu)
				{
					menuCur.paintMenu(g);
				}
				else if (ChatTextField.isShow)
				{
					ChatTextField.gI().paint(g);
				}
				Interface_Game.paintShowHelp(g, isInmap: false);
			}
			if (!GameScreen.getIsOffAdmin(2))
			{
				resetTrans(g);
				Interface_Game.paintShowEvent(g);
				if (GameScreen.player != null)
				{
					Interface_Game.paintInfoServer(g);
				}
			}
			resetTrans(g);
			if (hLoad > 0)
			{
				g.setColor(0);
				g.fillRect(0, 0, MotherCanvas.w, MotherCanvas.h);
			}
			if (GameScreen.isShowText)
			{
				for (int i = 0; i < vecTest.size(); i++)
				{
					string st = (string)vecTest.elementAt(i);
					if (i < 20)
					{
						mFont.tahoma_7b_black.drawString(g, st, 0, MotherCanvas.h - hText - i * hText, 0);
						mFont.tahoma_7b_white.drawString(g, st, -1, MotherCanvas.h - hText - i * hText - 1, 0);
					}
					else
					{
						mFont.tahoma_7b_black.drawString(g, st, MotherCanvas.w, MotherCanvas.h - hText - (i - 20) * hText, 1);
						mFont.tahoma_7b_white.drawString(g, st, MotherCanvas.w - 1, MotherCanvas.h - hText - (i - 20) * hText - 1, 1);
					}
				}
			}
			if (istest)
			{
				mFont.tahoma_7_black.drawString(g, "server test", 0, MotherCanvas.h - 20, 0);
			}
		}
		catch (Exception)
		{
		}
	}

	public static void addTest(string str)
	{
		vecTest.addElement(str);
		if (vecTest.size() > 40)
		{
			vecTest.removeElementAt(0);
		}
	}

	public override void update()
	{
		try
		{
			gameTick++;
			ModMenu.Update();
			if (gameTick > 12000)
			{
				gameTick = 0;
			}
			gameTickChia4 = gameTick / 4;
			if (tickAction > 0)
			{
				tickAction--;
			}
			if (gameTick % 5 == 0)
			{
				timeNow = mSystem.currentTimeMillis();
			}
			if (hLoad > 0)
			{
				hLoad -= MotherCanvas.h / 10;
			}
			Interface_Game.updateShowEvent();
			Interface_Game.updateInfoServer();
			if (GameScreen.isPvPNew)
			{
				Interface_Game.updateShowEvent();
				Interface_Game.updateInfoServer();
			}
			if (currentDialog != null)
			{
				currentDialog.update();
			}
			else
			{
				Interface_Game.UpdateShowHelp(g);
				if (menuCur.isShowMenu)
				{
					menuCur.updateMenu();
					menuCur.updateMenuKey();
				}
				else if (subDialog != null)
				{
					subDialog.update();
				}
				else if (ChatTextField.isShow)
				{
					ChatTextField.gI().updatekey();
					ChatTextField.gI().updatePointer();
				}
				else
				{
					currentScreen.updatekey();
					currentScreen.updatePointer();
				}
			}
			currentScreen.update();
			isPointerClick = false;
			if (GameScreen.timeResetCam > 0)
			{
				GameScreen.timeResetCam--;
				if (GameScreen.timeResetCam == 0)
				{
					GameScreen.isMoveCamera = false;
				}
			}
			isPointerSelect = false;
			Net.update();
			if (tickSelectChar > 0)
			{
				tickSelectChar--;
			}
			updateSpeed();
		}
		catch (Exception)
		{
		}
	}

	public static GameCanvas gI()
	{
		return instance;
	}

	public static void connect()
	{
		isLoadImage = false;
		Session_ME.gI().setHandler(GlobalMessageHandler.gI());
		int num = 2239;
		string iP = GameMidlet.IP;
		if (language == 1)
		{
			iP = GameMidlet.IP;
			num = 2239;
		}
		else if (IndexServer == 0)
		{
			iP = GameMidlet.IP;
			num = 2239;
		}
		else if (IndexServer == 1)
		{
			iP = GameMidlet.IP;
			num = 2239;
		}
		else if (IndexServer == 2)
		{
			iP = GameMidlet.IP;
			num = 2239;
		}
		else if (IndexServer == 3)
		{
			iP = GameMidlet.IP;
			num = 2239;
		}
		else
		{
			iP = GameMidlet.IP;
			num = 2220;
		}
		if (GameMidlet.DEVICE != 0 && GameMidlet.DEVICE != 4)
		{
			if (language == 1)
			{
				iP = GameMidlet.IP;
			}
			else if (IndexServer == 0)
			{
				iP = GameMidlet.IP;
				num = 2239;
			}
			else if (IndexServer == 1)
			{
				iP = GameMidlet.IP;
				num = 2239;
			}
			else if (IndexServer == 2)
			{
				iP = GameMidlet.IP;
				num = 2239;
			}
			else if (IndexServer == 3)
			{
				iP = GameMidlet.IP;
				num = 2239;
			}
			else
			{
				iP = GameMidlet.IP;
				num = 2220;
			}
		}
		string[] array = null;
		try
		{
			if (strIP.Length > 0)
			{
				array = mFont.split(strIP, "-");
				mSystem.outz("nhap ip kieu moi ip=" + array[0] + " port=" + int.Parse(array[1]));
			}
		}
		catch (Exception)
		{
			array = null;
		}
		if (array != null)
		{
			iP = array[0];
			num = int.Parse(array[1]);
		}
		if (istest)
		{
			iP = ((!(strIP != string.Empty)) ? "localhost" : strIP);
			num = 2239;
		}
		Session_ME.gI().connect(iP, num);
		infoDisConnect = string.Empty;
	}

	public static void connectDownload()
	{
		isLoadImage = true;
		Session_ME.gI().setHandler(GlobalMessageHandler.gI());
		int port = 2239;
		string iP = GameMidlet.IP;
		Session_ME.gI().connect(iP, port);
	}

	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, MotherCanvas.w, MotherCanvas.h);
	}

	public override void keyPressed(int keyCode)
	{
		tickAction = 4500;
		Player.isAFK = false;
		MsgDialog.isAuroReconect = false;
		if (Player.StepAutoRe == 5)
		{
			Player.StepAutoRe = 4;
		}
		if (TField.isQwerty && ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32))
		{
			keyAsciiPress = keyCode;
		}
		mapKeyPress(keyCode);
	}

	public void mapKeyPress(int keyCode)
	{
		if (currentDialog != null)
		{
			currentDialog.keypress(keyCode);
		}
		else if (subDialog != null)
		{
			subDialog.keypress(keyCode);
		}
		else if (ChatTextField.isShow)
		{
			ChatTextField.gI().keyPressed(keyCode);
		}
		else
		{
			currentScreen.keyPress(keyCode);
		}
		if (!GameScreen.isShowFocusTK && GameMidlet.DEVICE == 0 && isTouch)
		{
			GameScreen.isShowFocusTK = true;
		}
		if (isSendChatpopup)
		{
			isSendChatpopup = false;
		}
		else
		{
			if (keyPressPc(keyCode))
			{
				return;
			}
			if (TField.isQwerty && !ChatTextField.isShow)
			{
				if (keyAsciiPress == 114 || keyAsciiPress == 82)
				{
					keyMyHold[21] = true;
					keyMyPressed[21] = true;
				}
				else if (keyAsciiPress == 116 || keyAsciiPress == 84)
				{
					keyMyHold[23] = true;
					keyMyPressed[23] = true;
				}
				else if (keyAsciiPress == 121 || keyAsciiPress == 89)
				{
					keyMyHold[25] = true;
					keyMyPressed[25] = true;
				}
				else if (keyAsciiPress == 117 || keyAsciiPress == 85)
				{
					keyMyHold[27] = true;
					keyMyPressed[27] = true;
				}
				else if (keyAsciiPress == 105 || keyAsciiPress == 73)
				{
					keyMyHold[29] = true;
					keyMyPressed[29] = true;
				}
			}
			switch (keyCode)
			{
			case -1:
				keyMyHold[2] = true;
				keyMyPressed[2] = true;
				break;
			case -2:
				keyMyHold[8] = true;
				keyMyPressed[8] = true;
				break;
			case -3:
				keyMyHold[4] = true;
				keyMyPressed[4] = true;
				break;
			case -4:
				keyMyHold[6] = true;
				keyMyPressed[6] = true;
				break;
			case -5:
			case 10:
				keyMyHold[5] = true;
				keyMyPressed[5] = true;
				break;
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
				keyMyHold[keyCode - 28] = true;
				keyMyPressed[keyCode - 28] = true;
				break;
			case 42:
				keyMyHold[10] = true;
				keyMyPressed[10] = true;
				break;
			case 35:
				keyMyHold[11] = true;
				keyMyPressed[11] = true;
				break;
			case -6:
				keyMyHold[12] = true;
				keyMyPressed[12] = true;
				break;
			case -7:
				keyMyHold[13] = true;
				keyMyPressed[13] = true;
				break;
			}
		}
	}

	public override void keyReleased(int keyCode)
	{
		if (TField.isQwerty)
		{
			keyAsciiPress = 0;
		}
		mapKeyRelease(keyCode);
	}

	public void mapKeyRelease(int keyCode)
	{
		keyReleasedPc(keyCode);
		switch (keyCode)
		{
		case -1:
			keyMyHold[2] = false;
			keyMyPressed[2] = false;
			break;
		case -2:
			keyMyHold[8] = false;
			keyMyPressed[8] = false;
			break;
		case -3:
			keyMyHold[4] = false;
			keyMyPressed[4] = false;
			break;
		case -4:
			keyMyHold[6] = false;
			keyMyPressed[6] = false;
			break;
		case -5:
		case 10:
			keyMyHold[5] = false;
			keyMyPressed[5] = false;
			break;
		case 48:
		case 49:
		case 50:
		case 51:
		case 52:
		case 53:
		case 54:
		case 55:
		case 56:
		case 57:
			keyMyHold[keyCode - 28] = false;
			break;
		case 42:
			keyMyHold[10] = false;
			break;
		case 35:
			keyMyHold[11] = false;
			break;
		case -6:
			keyMyHold[12] = false;
			break;
		case -7:
			keyMyHold[13] = false;
			break;
		}
	}

	public static bool isKeyPressed(int index)
	{
		if (keyMyPressed[index])
		{
			return true;
		}
		return false;
	}

	public void onPointerDragged(int x, int y)
	{
		isPointerSelect = false;
		px = x;
		py = y;
		if (isPointerMove)
		{
			listPoint.addElement(new Position(x, y));
		}
		else if (CRes.abs(px - pxLast) >= 15 || CRes.abs(py - pyLast) >= 15)
		{
			isPointerMove = true;
		}
	}

	public void onPointerPressed(int x, int y)
	{
		Player.isAFK = false;
		MsgDialog.isAuroReconect = false;
		if (Player.StepAutoRe == 5)
		{
			Player.StepAutoRe = 4;
		}
		tickAction = 4500;
		isPointerDown = true;
		isPointerMove = false;
		isPointerSelect = false;
		isPointerRelease = false;
		isPointerEnd = false;
		pxLast = x;
		pyLast = y;
		px = x;
		py = y;
	}

	public void onPointerReleased(int x, int y)
	{
		if (!isPointerMove && !isPointerEnd)
		{
			isPointerSelect = true;
		}
		clearKeyHold();
		clearKeyPressed();
		isPointerDown = false;
		isPointerRelease = true;
		isPointerMove = false;
		isPointerClick = true;
		isPointerEnd = false;
		px = x;
		py = y;
	}

	public static void clearKeyPressed()
	{
		isPointerRelease = false;
		isPointerDown = false;
		for (int i = 0; i < keyMyPressed.Length; i++)
		{
			keyMyPressed[i] = false;
		}
	}

	public static void clearKeyPressed(int keycode)
	{
		isPointerRelease = false;
		isPointerDown = false;
		keyMyPressed[keycode] = false;
	}

	public static void clearKeyHold()
	{
		isPointerRelease = false;
		isPointerDown = false;
		for (int i = 0; i < keyMyHold.Length; i++)
		{
			keyMyHold[i] = false;
		}
	}

	public static void clearKeyHold(int keycode)
	{
		isPointerRelease = false;
		isPointerDown = false;
		keyMyHold[keycode] = false;
	}

	public static void clearKeyReleased()
	{
		isPointerDown = false;
		isPointerRelease = false;
		for (int i = 0; i < keyMyReleased.Length; i++)
		{
			keyMyReleased[i] = false;
		}
	}

	public static void clearAll()
	{
		clearKeyHold();
		clearKeyPressed();
		clearKeyReleased();
		isPointerSelect = false;
		isPointerMove = false;
		isPointerClick = false;
		isPointerDown = false;
	}

	public static bool isPointer(int x, int y, int w, int h)
	{
		if (!isPointerDown && !isPointerRelease)
		{
			return false;
		}
		return isPoint(x, y, w, h);
	}

	public static bool isPointSelect(int x, int y, int w, int h)
	{
		if (!isPointerSelect)
		{
			return false;
		}
		return isPoint(x, y, w, h);
	}

	public static bool isPoint(int x, int y, int w, int h)
	{
		if (px >= x && px <= x + w && py >= y && py <= y + h)
		{
			return true;
		}
		return false;
	}

	public static bool isPointLast(int x, int y, int w, int h)
	{
		if (pxLast >= x && pxLast <= x + w && pyLast >= y && pyLast <= y + h)
		{
			return true;
		}
		return false;
	}

	public static void Start_Normal_DiaLog(string info, mVector vecCmd, bool isCmdClose)
	{
		MsgDialog msgDialog = new MsgDialog();
		msgDialog.setinfo(info, vecCmd, isCmdClose, 0);
		currentDialog = msgDialog;
	}

	public static void Start_ReConect_DiaLog(string info, mVector vecCmd, bool isCmdClose)
	{
		MsgDialog msgDialog = new MsgDialog();
		msgDialog.setinfo(info, vecCmd, isCmdClose, 9);
		currentDialog = msgDialog;
	}

	public static void Start_Normal_DiaLog_New(string info, mVector vecCmd, bool isCmdClose, string name)
	{
		MsgDialog msgDialog = new MsgDialog();
		msgDialog.setinfoNew(info, vecCmd, isCmdClose, name);
		currentDialog = msgDialog;
	}

	public static void Start_Normal_DiaLog(string info, iCommand cmd, bool isCmdClose)
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(cmd);
		Start_Normal_DiaLog(info, mVector2, isCmdClose);
	}

	public static void Start_Normal_Only_CmdClose_DiaLog(string info)
	{
		mVector vecCmd = new mVector();
		Start_Normal_DiaLog(info, vecCmd, isCmdClose: true);
	}

	public static void Start_Waiting_DiaLog(string info, bool isCmdClose)
	{
		MsgDialog msgDialog = new MsgDialog();
		msgDialog.setWaitinginfo(info, null, isCmdClose, 1, 0);
		currentDialog = msgDialog;
	}

	public static void Start_Waiting_Connect_DiaLog(string info, bool isCmdClose)
	{
		MsgDialog msgDialog = new MsgDialog();
		msgDialog.setWaitinginfo(info, null, isCmdClose, 8, 0);
		currentDialog = msgDialog;
	}

	public static void Start_Time_DiaLog(string info, bool isCmdClose, int time, mVector vec)
	{
		MsgDialog msgDialog = new MsgDialog();
		msgDialog.setWaitinginfo(info, vec, isCmdClose, 6, time);
		currentDialog = msgDialog;
	}

	public static InputDialog Start_Input_Dialog(string info, iCommand cmd, bool isNum, string name)
	{
		InputDialog inputDialog = new InputDialog();
		inputDialog.setinfo(info, cmd, isNum, name);
		return inputDialog;
	}

	public static InputDialog Start_Input_Dialog_Server(string[] info, string name, short Id)
	{
		InputDialog inputDialog = new InputDialog();
		inputDialog.setinfo(info, name, Id, null, string.Empty);
		return inputDialog;
	}

	public static InputDialog Start_Input_Dialog_Server(string[] info, string name, iCommand cmd)
	{
		InputDialog inputDialog = new InputDialog();
		inputDialog.setinfo(info, name, 0, cmd, string.Empty);
		return inputDialog;
	}

	public static InputDialog Start_ShopInput_Dialog(string info, iCommand cmd, bool isNum, int defValue, int price, string name, string xuluong)
	{
		InputDialog inputDialog = new InputDialog();
		inputDialog.setinfoSmallNew(info, cmd, isNum, defValue, price, name, xuluong);
		return inputDialog;
	}

	public static MsgArchiDaily Start_Archi_Dialog(string name, mVector vec)
	{
		MsgArchiDaily msgArchiDaily = new MsgArchiDaily();
		msgArchiDaily.setinfo(name, vec);
		return msgArchiDaily;
	}

	public static MsgTableMatch Start_Match_Dialog(mVector vec)
	{
		MsgTableMatch msgTableMatch = new MsgTableMatch();
		msgTableMatch.setinfo(vec);
		return msgTableMatch;
	}

	public static void Start_Current_Dialog(MainDialog dgl)
	{
		currentDialog = dgl;
	}

	public static void Start_Sub_Dialog(MainDialog dgl)
	{
		subDialog = dgl;
	}

	public static void ShowMenu(Menu menu)
	{
		menuCur = menu;
		mSound.playSound(1, mSound.volumeSound);
	}

	public static void end_Dialog()
	{
		currentDialog = null;
		subDialog = null;
		clearKeyHold();
		clearKeyPressed();
		if (currentScreen != null)
		{
			currentScreen.CheckClickCmd();
		}
	}

	public static void end_Cur_Dialog()
	{
		currentDialog = null;
		clearKeyHold();
		clearKeyPressed();
		if (currentScreen != null)
		{
			currentScreen.CheckClickCmd();
		}
	}

	public static bool getShowCmd()
	{
		if (!menuCur.isShowMenu && currentDialog == null && subDialog == null)
		{
			return true;
		}
		return false;
	}

	public static void clearAllPointerEvent()
	{
	}

	public void perform(int idAction, object p)
	{
	}

	public static bool keyMove(int Dir)
	{
		switch (Dir)
		{
		case 1:
			if (keyMyHold[2] || keyMyHold[22] || keyMyHold[32])
			{
				return true;
			}
			break;
		case 3:
			if (keyMyHold[8] || keyMyHold[28] || keyMyHold[38])
			{
				return true;
			}
			break;
		case 0:
			if (keyMyHold[4] || keyMyHold[24] || keyMyHold[34])
			{
				return true;
			}
			break;
		case 2:
			if (keyMyHold[6] || keyMyHold[26] || keyMyHold[36])
			{
				return true;
			}
			break;
		}
		return false;
	}

	public static void ClearkeyMove(int Dir)
	{
		switch (Dir)
		{
		case 1:
			keyMyHold[2] = false;
			keyMyHold[22] = false;
			keyMyHold[32] = false;
			break;
		case 3:
			keyMyHold[8] = false;
			keyMyHold[28] = false;
			keyMyHold[38] = false;
			break;
		case 0:
			keyMyHold[4] = false;
			keyMyHold[24] = false;
			keyMyHold[34] = false;
			break;
		case 2:
			keyMyHold[6] = false;
			keyMyHold[26] = false;
			keyMyHold[36] = false;
			break;
		}
	}

	public static bool keyActionUni(int action)
	{
		switch (action)
		{
		case 1:
			if (UseKey(21) || UseKey(31))
			{
				return true;
			}
			break;
		case 3:
			if (UseKey(23) || UseKey(33))
			{
				return true;
			}
			break;
		case 5:
			if (UseKey(5) || UseKey(25) || UseKey(35))
			{
				return true;
			}
			break;
		case 7:
			if (UseKey(27) || UseKey(37))
			{
				return true;
			}
			break;
		case 9:
			if (UseKey(29) || UseKey(39))
			{
				return true;
			}
			break;
		case 0:
			if (UseKey(12) || UseKey(40))
			{
				return true;
			}
			break;
		case 2:
			if (UseKey(13) || UseKey(41))
			{
				return true;
			}
			break;
		case 4:
			if (UseKey(42))
			{
				return true;
			}
			break;
		case 6:
			if (UseKey(20) || UseKey(43))
			{
				return true;
			}
			break;
		case 8:
			if (UseKey(11) || UseKey(44))
			{
				return true;
			}
			break;
		case 10:
			if (UseKey(10) || UseKey(45))
			{
				return true;
			}
			break;
		case 11:
			if (UseKey(46))
			{
				return true;
			}
			break;
		case 12:
			if (UseKey(47))
			{
				return true;
			}
			break;
		case 13:
			if (UseKey(48))
			{
				return true;
			}
			break;
		case 14:
			if (UseKey(49))
			{
				return true;
			}
			break;
		case 15:
			if (UseKey(50))
			{
				return true;
			}
			break;
		case 16:
			if (UseKey(51))
			{
				return true;
			}
			break;
		}
		return false;
	}

	public static void ClearActionUni(int action)
	{
		switch (action)
		{
		case 1:
			clearKeyHold(21);
			clearKeyHold(31);
			break;
		case 3:
			clearKeyHold(23);
			clearKeyHold(33);
			break;
		case 5:
			clearKeyHold(5);
			clearKeyHold(25);
			clearKeyHold(35);
			break;
		case 7:
			clearKeyHold(27);
			clearKeyHold(37);
			break;
		case 9:
			clearKeyHold(29);
			clearKeyHold(39);
			break;
		case 0:
			clearKeyHold(12);
			clearKeyHold(40);
			break;
		case 2:
			clearKeyHold(13);
			clearKeyHold(41);
			break;
		case 4:
			clearKeyHold(42);
			break;
		case 6:
			clearKeyHold(20);
			clearKeyHold(43);
			break;
		case 8:
			clearKeyHold(11);
			clearKeyHold(44);
			break;
		case 10:
			clearKeyHold(10);
			clearKeyHold(45);
			clearKeyPressed(10);
			clearKeyPressed(45);
			break;
		case 11:
			clearKeyHold(46);
			break;
		case 12:
			clearKeyHold(47);
			break;
		case 13:
			clearKeyHold(48);
			break;
		case 14:
			clearKeyHold(49);
			break;
		case 15:
			clearKeyHold(50);
			break;
		case 16:
			clearKeyHold(51);
			break;
		}
	}

	public static bool UseKey(int value)
	{
		for (int i = 0; i < keyMyHold.Length; i++)
		{
			if (i == value && (keyMyHold[i] || keyMyPressed[i]))
			{
				return true;
			}
		}
		return false;
	}

	public static bool AnyKey()
	{
		for (int i = 0; i < keyMyHold.Length; i++)
		{
			if (keyMyHold[i] || keyMyPressed[i])
			{
				return true;
			}
		}
		return false;
	}

	public static sbyte get_Day_Night()
	{
		sbyte result = DAY;
		int hour = DateTime.Now.Hour;
		if (hour >= 18 || hour < 6)
		{
			result = NIGHT;
		}
		return result;
	}

	public static long getTime()
	{
		return timeNow;
	}

	public static bool isTouchAndKey()
	{
		if (TField.isQwerty || GameMidlet.isPC || GameScreen.isShowFocusTK)
		{
			return true;
		}
		return false;
	}

	public static bool isTouchNoOrPC()
	{
		if (!isTouch || isTouchAndKey())
		{
			return true;
		}
		return false;
	}

	public static bool isIos()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Invalid comparison between Unknown and I4
		if (GameMidlet.DEVICE == 3 || GameMidlet.DEVICE == 5 || GameMidlet.DEVICE == 6 || (int)Application.platform == 8)
		{
			return true;
		}
		return false;
	}

	public static bool isAndroid()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Invalid comparison between Unknown and I4
		if (GameMidlet.DEVICE == 1 || (int)Application.platform == 11)
		{
			return true;
		}
		return false;
	}

	public static bool isDeviceStore()
	{
		if (GameMidlet.DEVICE == 4 || GameMidlet.DEVICE == 5 || GameMidlet.DEVICE == 6)
		{
			return true;
		}
		return false;
	}

	public static bool isDialogOrMenuShow()
	{
		if (currentDialog != null || subDialog != null || menuCur.isShowMenu)
		{
			return true;
		}
		return false;
	}

	public static void dialogDisconect()
	{
		if (isLoadImage)
		{
			return;
		}
		if (GameMidlet.DEVICE == 2)
		{
			isDisConnect = true;
			return;
		}
		string disconnect = T.disconnect;
		if (infoDisConnect != null && infoDisConnect.Length > 10)
		{
			disconnect = infoDisConnect;
			infoDisConnect = string.Empty;
		}
		bool flag = false;
		mVector mVector2 = new mVector();
		if (currentScreen != loginScr && currentScreen != loadMapScr)
		{
			mVector2.addElement(GameScreen.cmdReConnect);
			flag = true;
		}
		mVector2.addElement(gameScr.cmdExit);
		if (flag)
		{
			Start_ReConect_DiaLog(disconnect, mVector2, isCmdClose: false);
		}
		else
		{
			Start_Normal_DiaLog(disconnect, mVector2, isCmdClose: false);
		}
	}

	public void loadRMSGAME()
	{
		sbyte[] array = CRes.loadRMS("SUB_TYPETOUCH");
		if (array != null)
		{
			DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(array));
			try
			{
				Interface_Game.typeTouch = dataInputStream.readByte();
				dataInputStream.close();
			}
			catch (Exception)
			{
			}
		}
		if (isSuperLowGraphic)
		{
			lowGraphic = true;
		}
		else if (GameMidlet.DEVICE == 0)
		{
			sbyte[] array2 = CRes.loadRMS("SUB_LOWGRAPHIC");
			if (array2 != null)
			{
				DataInputStream dataInputStream2 = new DataInputStream(new ByteArrayInputStream(array2));
				try
				{
					lowGraphic = ((dataInputStream2.readByte() != 0) ? true : false);
					dataInputStream2.close();
				}
				catch (Exception)
				{
				}
			}
		}
		sbyte[] array3 = CRes.loadRMS("SUB_OFFBG");
		if (array3 != null)
		{
			DataInputStream dataInputStream3 = new DataInputStream(new ByteArrayInputStream(array3));
			try
			{
				isOffBg = ((dataInputStream3.readByte() != 0) ? true : false);
				dataInputStream3.close();
			}
			catch (Exception)
			{
			}
		}
		sbyte[] array4 = CRes.loadRMS("Main_isQty");
		if (array4 != null)
		{
			DataInputStream dataInputStream4 = new DataInputStream(new ByteArrayInputStream(array4));
			try
			{
				TField.isQwerty = dataInputStream4.readByte() == 1;
				dataInputStream4.close();
			}
			catch (Exception)
			{
			}
		}
		sbyte[] array5 = CRes.loadRMS("MAIN_SOUND");
		if (array5 != null)
		{
			mSound.isMusic = array5[0] == 1;
			mSound.isSound = array5[1] == 1;
		}
		sbyte[] array6 = CRes.loadRMS("SUB_TAITHO");
		if (array6 != null)
		{
			DataInputStream dataInputStream5 = new DataInputStream(new ByteArrayInputStream(array6));
			try
			{
				isTaiTho = dataInputStream5.readByte() == 1;
				dataInputStream5.close();
			}
			catch (Exception)
			{
			}
		}
		sbyte[] array7 = CRes.loadRMS("SUB_SSITEM");
		if (array7 != null)
		{
			DataInputStream dataInputStream6 = new DataInputStream(new ByteArrayInputStream(array7));
			try
			{
				isSSItem = dataInputStream6.readByte();
				dataInputStream6.close();
			}
			catch (Exception)
			{
			}
		}
		sbyte[] array8 = CRes.loadRMS("SUB_SPAM");
		if (array8 != null)
		{
			DataInputStream dataInputStream7 = new DataInputStream(new ByteArrayInputStream(array8));
			try
			{
				isTypeSpam = dataInputStream7.readByte();
				dataInputStream7.close();
			}
			catch (Exception)
			{
				isTypeSpam = 0;
			}
		}
	}

	public static string getTextTime()
	{
		long num = mSystem.currentTimeMillis();
		return (num / 1000 / 60 / 60 + 7) % 24 + "h " + num / 1000 / 60 % 60 + "'";
	}

	public static int getHourTime()
	{
		return (int)((mSystem.currentTimeMillis() / 1000 / 60 / 60 + 7) % 24);
	}

	public static bool isLowGraOrWP_PvP()
	{
		if (lowGraphic || (GameMidlet.DEVICE == 4 && (LoadMap.specMap == 2 || LoadMap.specMap == 6)))
		{
			return true;
		}
		return false;
	}

	public void updateSpeed()
	{
		if (percent != percentTemp)
		{
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject obj = (MainObject)GameScreen.vecPlayers.elementAt(i);
				int vM = obj.vMax * 100 / (100 + percentTemp);
				int vySeaM = obj.vMaxYSea * 100 / (100 + percentTemp);
				obj.setSpeed(vM, vySeaM);
			}
			if (percent == 0)
			{
				hardcodeSpeedUp = 0;
			}
			else
			{
				hardcodeSpeedUp = 300;
			}
			percentTemp = percent;
		}
	}

	public static void readLvGraphicPcRMS()
	{
		sbyte[] array = GameMidlet.loadRMS(Environment.MachineName + T.lowGraphic);
		if (array != null)
		{
			try
			{
				DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(array));
				lv = dataInputStream.readInt();
				dataInputStream.close();
			}
			catch (Exception)
			{
			}
		}
	}

	public static void readGraphicsPC()
	{
		int num = Rms.loadRMSInt2("RMS_Graphics_PC");
		lv = ((num == -1) ? 1 : num);
	}
}
