using System;
using System.Collections;

public class Player : MainPlayer
{
	public static bool isSendMove = true;

	public static bool isGhost = false;

	public static bool isBlock = false;

	public static sbyte AutoFireCur = 0;

	public static sbyte currentTab = 0;

	public static sbyte typeAutoBuff = 0;

	public static sbyte typeAutoFireMain = 1;

	public static sbyte AutoRevice = 0;

	public static sbyte isGetDataClan = -1;

	public int demUnFire;

	public static int wFocus = 140;

	public static int LvMinPk;

	public static int pointSkill;

	public static int pointAttribute = 10;

	public static int pointMaxLevelAttri = 0;

	public long timeLastSkill;

	public static mVector vecInventory = new mVector("Player.vecInventory");

	public static mVector vecChest = new mVector("Player.vecChest");

	public static mVector vecInvenClan = new mVector("Player.vecInvenClan");

	public static mVector vecMaxLevelAttri = new mVector("Player.vecMaxLevelAttri");

	public static short[] mSatnhan = new short[5];

	public static short[] msattam;

	public static sbyte[] mLvSkill = new sbyte[4];

	public static sbyte[] mLvSkillPlus = new sbyte[4];

	public static Hotkey[][] hotkeyPlayer;

	public static Hotkey[] hotkeyBuffPlayer;

	public static MyHashTable delaySkill = new MyHashTable();

	public static Main_Attribute[] mAttribute = new Main_Attribute[5];

	public static short[] mComboSkill;

	public static mVector vecListSkill = new mVector("Player.vecListSkill");

	public static mVector vecQuest = new mVector("Player.vecQuest");

	public static mVector vecGhostInput = new mVector("Player.vecGhostInput");

	public static mVector vecUniform = new mVector("Player.vecUniform");

	public const int HARDCODE_INDEXPOTION = 500;

	public const int HARDCODE_INDEXPASSIVE = 1000;

	public const int HARDCODE_INDEX_EFF_SPEC = 1500;

	public static sbyte indexGhostServer = 0;

	public static sbyte numPassive = 0;

	public static sbyte ChucInCLan = 10;

	public static sbyte ChucInSudo = 0;

	public static sbyte isAutoMaterial;

	public static string strTimeChange = string.Empty;

	public static string strHethong = string.Empty;

	public static bool isSkillready = false;

	public static bool isAFK = false;

	public static short idNPCQuestCur = 0;

	public static short idFashion = -1;

	public static MainQuest questMainNew = null;

	public static Item[] mChestWanted = new Item[2];

	public static int indexPosMapTrain = 0;

	public static int maxInventory = 40;

	public static int maxChest = 10;

	public static int indexCombo = -1;

	public static int indexAudition = -1;

	public static int Chestgem = 0;

	public static long Chestbeli = 0L;

	public static short ticket = 0;

	public static short keyBoss = 0;

	public static short PvPticket = 0;

	public static short maxTicket = 0;

	public static short maxKeyboss = 0;

	public static short maxPvPticket = 0;

	public static MainQuest curQuest = null;

	public static mVector vecParty = new mVector("Player.vecParty");

	public static mVector vecFriendList = new mVector("Player.vecFriendList");

	public static mVector vecEvent = new mVector("Player.vecEvent");

	public static bool isFocusNPC;

	public static int timeFocusNPC;

	public static int[][] hardCodeShortInfo = new int[2][]
	{
		new int[7] { 0, 2, 3, 4, 10, 11, 14 },
		new int[7]
	};

	public static string[] InfoShortEquip = new string[6]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	public static bool isMPHP = true;

	public static bool isGetItem = true;

	public static int xBeginAuto;

	public static int yBeginAuto;

	public static bool isBack = false;

	public static int maxRangeAuto = 200;

	public static short giamCountDownAtt = 0;

	public static short giamCountDownParty = 0;

	public static short GiamCountDownCur = 0;

	public static short tickAutoRevice = 100;

	public static short TangManaUseSkill = 0;

	public int tickNauBanh;

	public bool isNauBanh;

	public static Boat boatKeyParty = null;

	public static int[] mframeghost = new int[10] { 0, 0, 1, 1, 2, 2, 2, 2, 1, 1 };

	public int lineShowRedLineNext = 2;

	public int lineShowRedLineCur = 2;

	public int tickmove;

	public int tickChangeLine;

	public int tickFinish;

	public int typeMoveMapRedLine;

	public const sbyte RED_LINE_FREE = 0;

	public const sbyte RED_LINE_DIE = 1;

	public const sbyte RED_LINE_FINISH = 2;

	public static bool isChangeLine = false;

	public static int isShowDie = 0;

	public int typeMoveGotoSky;

	public const sbyte GOTO_SKY_BEGIN = 0;

	public const sbyte GOTO_CAPCHAR = 1;

	public const sbyte GOTO_DIE = 2;

	public const sbyte GOTO_FINISH = 3;

	public MainItem hpPoi;

	public MainItem mpPoi;

	private int tickdemHP;

	private int tickdemMp;

	public static bool isFullInven = false;

	public bool isBeginTrain;

	private int indexkey;

	private int[] mindexkey = new int[4] { -1, -1, -1, -1 };

	private int indexFocus;

	private long timeFristSkill;

	public static int IndexFire = 0;

	private MainObject objAutoFrist;

	public int tickMaxBack;

	public static int distest = 0;

	public static long beliTest;

	public static long valueTestBeli;

	public static long timeTestBeli;

	public static bool isUpdateMonney = false;

	public static sbyte StepAutoRe = 0;

	public static int tickAutoRe = 0;

	public const sbyte AUTO_RE_CHECK_MAP_LANG = 0;

	public const sbyte AUTO_RE_SET_MOVE_CHECK_POINT = 1;

	public const sbyte AUTO_RE_MOVE_TO_CHECK_POINT = 2;

	public const sbyte AUTO_RE_DISCONECT = 3;

	public const sbyte AUTO_RE_WAIT = 4;

	public const sbyte AUTO_RE_CHECK_AUTO_OK = 5;

	public const sbyte AUTO_RE_DONOT_AUTO = 6;

	public Player()
	{
		hOne = 52;
		wOne = 26;
		setSpeed(7, 7);
		x = MotherCanvas.w / 2;
		y = MotherCanvas.h / 2;
		Hp = 100;
		Mp = 100;
		maxHp = 100;
		maxMp = 100;
		percentLv = 0;
		hIconFocus = 0;
		Action = 0;
		hotkeyPlayer = new Hotkey[2][];
		hotkeyBuffPlayer = new Hotkey[6];
		for (int i = 0; i < hotkeyPlayer.Length; i++)
		{
			hotkeyPlayer[i] = new Hotkey[6];
			for (int j = 0; j < hotkeyPlayer[i].Length; j++)
			{
				hotkeyPlayer[i][j] = new Hotkey();
			}
		}
		myBoat = new short[4] { 4, 5, 6, 7 };
		mValuePvP = new int[2];
		PointPvP = 0;
	}

	public override void paint(mGraphics g)
	{
		if (GameScreen.typeViewPlayer == 1)
		{
			return;
		}
		if (Action == 4)
		{
			if (!isDie)
			{
				g.drawImage(MainObject.imgShadow, xDie + 1, yDie, 3);
				paintChar(g, xDie, yDie - dyDie);
			}
			else
			{
				if (LoadMap.specMap != 4)
				{
					paintShadow(g, x);
				}
				g.drawRegion(AvMain.fraDiePlayer.imgFrame, 0, f / 5 % AvMain.fraDiePlayer.nFrame * AvMain.fraDiePlayer.frameHeight, AvMain.fraDiePlayer.frameWidth, AvMain.fraDiePlayer.frameHeight - 2 + dySea / 10, 0, x - 4, y - dy, mGraphics.BOTTOM | mGraphics.LEFT);
			}
		}
		else
		{
			paintCharAllPart(g, 0);
		}
		if (!isGhost && GameScreen.typePaintGameScreen != 1)
		{
			paintName(g, 0, 0);
		}
		if (GameScreen.isShowIndex)
		{
			mFont.tahoma_7b_black.drawString(g, x + " - " + y, x, y + 5, 2);
		}
	}

	public void paintGotoSky(mGraphics g)
	{
		paintshadowFocus(g, 0);
		int num = y - dy;
		if (boatKeyParty != null)
		{
			boatKeyParty.paintFrist(g);
			if (dy == 0)
			{
				num = y - dySea / 10;
			}
			boatKeyParty.paintBuom(g);
		}
		else if (!isTanHinh)
		{
			paintShadow(g, x - dx);
		}
		if (!isTanHinh)
		{
			paintBuffFirst(g, x - dx, num);
			paintEffSpecFirst(g);
			int num2 = 7;
			int num3 = GameScreen.vecPlayers.size();
			for (int num4 = GameScreen.vecPlayers.size() - 1; num4 >= 0; num4--)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(num4);
				if (mainObject != this)
				{
					mainObject.type_left_right = type_left_right;
					mainObject.paintWhenBoatOther(g, x - dx + num2 * num3, num);
					num3--;
				}
			}
			paintChar(g, x - dx, num);
		}
		if (boatKeyParty != null)
		{
			boatKeyParty.paintLastInMap(g);
		}
		if (!isTanHinh)
		{
			paintBuffLast(g, x - dx, num);
			paintEffSpecLast(g);
		}
	}

	public static void paintGhost(mGraphics g, int x, int y)
	{
	}

	public override void update()
	{
		if (typeActionBoat != 0)
		{
			updateActionUpBoat();
		}
		checkRemoveBoat();
		if (check_Move_EffSpec())
		{
			vx = 0;
			vy = 0;
			if (Action == 1)
			{
				Action = 0;
			}
			if (skillCurrent != null)
			{
				if (skillCurrent.isRemove)
				{
					skillCurrent = null;
				}
				else
				{
					skillCurrent.beginSkill();
					if (timeDragon <= 0)
					{
						frameOneStep = 20;
					}
					timeDragon = 150;
				}
			}
			if (posTransRoad != null)
			{
				posTransRoad = null;
			}
			if (checkEffSpec(1))
			{
				Dir = DirSpec;
			}
		}
		updateActionPerson();
		if (Action != 2 && !isMoveNor)
		{
			setMove(isAutomove: true);
		}
		updatePlayer();
		setFocus(allFocus: false);
		setCmdGame();
		setAutoPlayer();
		if (isMoveNor)
		{
			move_to_XY_Normal();
			if (posTransRoad != null)
			{
				posTransRoad = null;
			}
			if (CRes.abs(x - toX) < vMax && CRes.abs(y - toY) < vMax)
			{
				isMoveNor = false;
			}
		}
		else if (posTransRoad == null && skillCurrent != null)
		{
			if (skillCurrent.isRemove)
			{
				skillCurrent = null;
			}
			else
			{
				skillCurrent.setMoveFire();
			}
		}
		updateDelay();
		if (!isFocusNPC)
		{
			timeFocusNPC++;
			if (timeFocusNPC > 60)
			{
				isFocusNPC = true;
				timeFocusNPC = 0;
			}
		}
		if (GameScreen.isMoveCamEff && Action != 2)
		{
			GameScreen.isMoveCamEff = false;
		}
		updateMoney();
		if (StepAutoRe != 4 && StepAutoRe != 3 && StepAutoRe != 6)
		{
			StepAutoReconnect();
		}
		if (isNauBanh)
		{
			tickNauBanh++;
			if (tickNauBanh > 87)
			{
				GameCanvas.gameScr.setNauBanh(isNauBanh: false);
				GlobalService.gI().VotBanhChung(1, 1, 3);
			}
		}
		base.update();
	}

	public void updateMapRedLine()
	{
		if (boatSea != null)
		{
			y += vy;
			x += vx;
			if (tickChangeLine > 0)
			{
				tickChangeLine--;
			}
			if (lineShowRedLineNext != lineShowRedLineCur)
			{
				tickmove = 6;
				if (lineShowRedLineNext > lineShowRedLineCur)
				{
					vy = 4;
				}
				else
				{
					vy = -4;
				}
				lineShowRedLineCur = lineShowRedLineNext;
				tickChangeLine = 50;
			}
			if (isChangeLine)
			{
				if (tickmove == 0)
				{
					y = MapOff_RedLine.yHarcodeMapRedLine - 48 + lineShowRedLineCur * 24;
				}
				if (lineShowRedLineNext == 0)
				{
					lineShowRedLineNext = 1;
				}
				else if (lineShowRedLineNext == 3)
				{
					lineShowRedLineNext = 2;
				}
				else if (CRes.random(2) == 0)
				{
					lineShowRedLineNext++;
				}
				else
				{
					lineShowRedLineNext--;
				}
				isChangeLine = false;
			}
			if (boatSea != null)
			{
				boatSea.updateXY(x, y, 0, (sbyte)type_left_right);
			}
			if (vy != 0)
			{
				boatSea.setLevelPaint();
			}
			if (tickmove > 0)
			{
				tickmove--;
				if (tickmove == 0)
				{
					vy = 0;
					y = MapOff_RedLine.yHarcodeMapRedLine - 48 + lineShowRedLineCur * 24;
				}
			}
			if (GameCanvas.gameTick % 8 == 0)
			{
				boatSea.addEffSea(x, y, -1, (sbyte)type_left_right, -3);
			}
			if (CRes.random(4) == 0)
			{
				boatSea.addEffSea(x, y, 0, (sbyte)((type_left_right == 0) ? 2 : 0), -3);
			}
			if (GameCanvas.gameTick % 6 == 0)
			{
				boatSea.addEffSea(x, y, 2, (sbyte)type_left_right, -3);
			}
			if (boatSea != null)
			{
				boatSea.updateBoat();
			}
			if (tickChangeLine <= 0 && CRes.random(120) == 0 && lineShowRedLineCur == lineShowRedLineNext && typeMoveMapRedLine == 0)
			{
				if (lineShowRedLineNext == 0)
				{
					lineShowRedLineNext = 1;
				}
				else if (lineShowRedLineNext == 3)
				{
					lineShowRedLineNext = 2;
				}
				else if (CRes.random(2) == 0)
				{
					lineShowRedLineNext++;
				}
				else
				{
					lineShowRedLineNext--;
				}
			}
			if (typeMoveMapRedLine == 0 || typeMoveMapRedLine == 2)
			{
				for (int i = 0; i < MapOff_RedLine.vecDaBien.size(); i++)
				{
					Point point = (Point)MapOff_RedLine.vecDaBien.elementAt(i);
					if (point.dis == lineShowRedLineNext && point.x + point.x2 > MapOff_RedLine.xHardCodeMapRedLine + 80 && point.x + point.x2 < MapOff_RedLine.xHardCodeMapRedLine + 120)
					{
						isChangeLine = true;
						return;
					}
				}
				if (isShowDie == 2)
				{
					tickFinish++;
					if (MapOff_RedLine.vecDaBien.size() == 0 || tickFinish > 100)
					{
						isShowDie = 12;
						MapOff_RedLine.vxHardcodeRedLine = 0;
						GameScreen.player.vx = 5;
					}
				}
				if (isShowDie == 12 && x > MotherCanvas.w - 50)
				{
					MapOff_RedLine.changeFinish();
				}
			}
			else if (typeMoveMapRedLine == 1)
			{
				if (isShowDie == 0)
				{
					for (int j = 0; j < MapOff_RedLine.vecDaBien.size(); j++)
					{
						Point point2 = (Point)MapOff_RedLine.vecDaBien.elementAt(j);
						if (point2.dis == lineShowRedLineNext && point2.x + point2.x2 > MapOff_RedLine.xHardCodeMapRedLine + 50 && point2.x + point2.x2 < MapOff_RedLine.xHardCodeMapRedLine + 65)
						{
							isShowDie = 1;
							MapOff_RedLine.vxHardcodeRedLine = 0;
							GameScreen.player.vx = -3;
							if (lineShowRedLineCur < 2)
							{
								GameScreen.player.vy = 1;
							}
							else
							{
								GameScreen.player.vy = -1;
							}
							return;
						}
					}
				}
				else if (isShowDie == 1 && x < 0)
				{
					GlobalService.gI().Red_Line(1, 0);
					isShowDie = 11;
				}
			}
		}
		updateEye();
		updateChatPopup(dhCharPopup);
	}

	public void updateRedLineFinish()
	{
		vx = 3;
		if (GameCanvas.gameTick % 10 == 0 && CRes.random(4) == 0)
		{
			if (vy == 0)
			{
				vy = 1;
			}
			else
			{
				vy = 0;
			}
		}
		y += vy;
		x += vx;
		if (boatSea != null)
		{
			boatSea.updateXY(x, y, 0, (sbyte)type_left_right);
		}
		if (vy != 0)
		{
			boatSea.setLevelPaint();
		}
		if (GameCanvas.gameTick % 8 == 0)
		{
			boatSea.addEffSea(x, y, -1, (sbyte)type_left_right, -3);
		}
		if (CRes.random(4) == 0)
		{
			boatSea.addEffSea(x, y, 0, (sbyte)((type_left_right == 0) ? 2 : 0), -3);
		}
		if (GameCanvas.gameTick % 6 == 0)
		{
			boatSea.addEffSea(x, y, 2, (sbyte)type_left_right, -3);
		}
		if (boatSea != null)
		{
			boatSea.updateBoat();
		}
		updateEye();
		updateChatPopup(dhCharPopup);
		if (isShowDie == 12 && x > MotherCanvas.w - 50)
		{
			GlobalService.gI().Red_Line(2, 0);
			isShowDie = 22;
		}
	}

	public void updateMapGotoSky()
	{
		if (boatKeyParty == null || !boatKeyParty.loadDataOk)
		{
			for (int num = GameScreen.vecPlayers.size() - 1; num >= 0; num--)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(num);
				if (vecParty.size() > 0 && ((InfoMemList)vecParty.elementAt(0)).id == mainObject.ID)
				{
					bool loadDataOk = true;
					if (boatKeyParty == null)
					{
						boatKeyParty = new Boat(mainObject.ID, mainObject.x, mainObject.y, 0, (sbyte)mainObject.type_left_right);
					}
					else
					{
						loadDataOk = false;
					}
					if (mainObject.myBoat != null)
					{
						boatKeyParty.setPartImage(mainObject.myBoat, mainObject.typePirate);
					}
					else
					{
						loadDataOk = false;
					}
					if (mainObject.clan != null)
					{
						boatKeyParty.idIconClan = mainObject.clan.idIcon;
					}
					else
					{
						loadDataOk = false;
					}
					boatKeyParty.loadDataOk = loadDataOk;
				}
			}
		}
		if (boatKeyParty != null)
		{
			if (boatKeyParty != null)
			{
				boatKeyParty.updateXY(x, y, 0, (sbyte)type_left_right);
			}
			if (vy != 0)
			{
				boatKeyParty.setLevelPaint();
			}
			if (typeMoveGotoSky != 2)
			{
				if (GameCanvas.gameTick % 8 == 0)
				{
					boatKeyParty.addEffSea(x, y, -1, (sbyte)type_left_right, -3);
				}
				if (CRes.random(4) == 0)
				{
					boatKeyParty.addEffSea(x, y, 0, (sbyte)((type_left_right == 0) ? 2 : 0), -3);
				}
				if (GameCanvas.gameTick % 6 == 0)
				{
					boatKeyParty.addEffSea(x, y, 2, (sbyte)type_left_right, -3);
				}
			}
			boatKeyParty.updateBoat();
		}
		if (typeMoveGotoSky == 0)
		{
			move_to_XY_Normal();
			y += vy;
			x += vx;
		}
		else if (typeMoveGotoSky == 1)
		{
			move_to_XY_Normal();
			y += vy;
			x += vx;
		}
		else if (typeMoveGotoSky == 2)
		{
			y += vy;
			x += vx;
			if (vy < 10)
			{
				vy++;
			}
		}
		else if (typeMoveGotoSky == 3)
		{
			move_to_XY_Normal();
			y += vy;
			x += vx;
		}
		if (!MapGotoSky.isStopUpdateCamera)
		{
			MainScreen.cameraMain.moveCamera(GameScreen.player.x - MotherCanvas.w / 2, GameScreen.player.y - MotherCanvas.h / 3 * 2);
		}
		updateEye();
		updateEffSpec();
		updateBuff();
	}

	public void updateMapGotoGod(sbyte finalMove)
	{
		if (boatKeyParty == null || !boatKeyParty.loadDataOk)
		{
			for (int num = GameScreen.vecPlayers.size() - 1; num >= 0; num--)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(num);
				if (vecParty.size() > 0 && ((InfoMemList)vecParty.elementAt(0)).id == mainObject.ID)
				{
					bool loadDataOk = true;
					if (boatKeyParty == null)
					{
						boatKeyParty = new Boat(mainObject.ID, mainObject.x, mainObject.y, 0, (sbyte)mainObject.type_left_right);
					}
					else
					{
						loadDataOk = false;
					}
					if (mainObject.myBoat != null)
					{
						boatKeyParty.setPartImage(mainObject.myBoat, mainObject.typePirate);
					}
					else
					{
						loadDataOk = false;
					}
					if (mainObject.clan != null)
					{
						boatKeyParty.idIconClan = mainObject.clan.idIcon;
					}
					else
					{
						loadDataOk = false;
					}
					boatKeyParty.loadDataOk = loadDataOk;
				}
			}
		}
		if (boatKeyParty != null)
		{
			if (boatKeyParty != null)
			{
				boatKeyParty.updateXY(x, y, 0, (sbyte)type_left_right);
			}
			if (vy != 0)
			{
				boatKeyParty.setLevelPaint();
			}
			if (typeMoveGotoSky != 2)
			{
				if (GameCanvas.gameTick % 8 == 0)
				{
					boatKeyParty.addEffSea(x, y, -1, (sbyte)type_left_right, -3);
				}
				if (CRes.random(4) == 0)
				{
					boatKeyParty.addEffSea(x, y, 0, (sbyte)((type_left_right == 0) ? 2 : 0), -3);
				}
				if (GameCanvas.gameTick % 6 == 0)
				{
					boatKeyParty.addEffSea(x, y, 2, (sbyte)type_left_right, -3);
				}
			}
			boatKeyParty.updateBoat();
		}
		if (typeMoveGotoSky == 0)
		{
			move_to_XY_Normal();
			y += vy;
			x += vx;
		}
		else if (typeMoveGotoSky == 1)
		{
			move_to_XY_Normal();
			y += vy;
			x += vx;
		}
		else if (typeMoveGotoSky == 2)
		{
			if (finalMove == 0)
			{
				move_to_XY_Normal();
				y += vy;
				x += vx;
			}
			else
			{
				y += vy;
				x += vx;
				if (vy < 10)
				{
					vy++;
				}
			}
		}
		else if (typeMoveGotoSky == 3)
		{
			y += vy;
			x += vx;
			if (vx < 4)
			{
				vx++;
			}
		}
		updateEye();
		updateEffSpec();
		updateBuff();
	}

	public void setAutoPlayer()
	{
		tickdemHP++;
		tickdemMp++;
		if (Action == 4 || Hp <= 0 || isGhost)
		{
			setStart_EndAutoFire(isAu: false);
			return;
		}
		if (isMPHP)
		{
			if (isUseMPHP(1))
			{
				if (hpPoi == null || hpPoi.numPotion <= 0 || tickdemHP >= 500)
				{
					hpPoi = getPotionAuto(1);
					tickdemHP = 0;
				}
				if (hpPoi != null && hpPoi.numPotion > 0)
				{
					hpPoi.Use_Item();
				}
			}
			if (isUseMPHP(2))
			{
				if (mpPoi == null || mpPoi.numPotion <= 0 || tickdemMp >= 500)
				{
					mpPoi = getPotionAuto(2);
					tickdemMp = 0;
				}
				if (mpPoi != null && mpPoi.numPotion > 0)
				{
					mpPoi.Use_Item();
				}
			}
		}
		if (AutoFireCur >= 1 && Action != 2 && Interface_Game.isAutoFireInterface)
		{
			setAutoFire(AutoFireCur == 2);
			if (typeAutoBuff == 1 && MsgAutoFire.value != null && GameCanvas.gameTick % 5 == 1)
			{
				updateAutoBuff();
			}
		}
		if (isGetItem)
		{
			autoGetItem();
		}
	}

	public void updateAutoBuff()
	{
		for (int i = 0; i < MsgAutoFire.value.Length; i++)
		{
			if (MsgAutoFire.value[i][1] == 1)
			{
				Skill_Info skillFromID = Skill_Info.getSkillFromID(MsgAutoFire.value[i][0]);
				if (skillFromID != null && getManaNeedUse(skillFromID.manaLost) <= Mp && DelaySkill.getDelay(skillFromID.indexHotKey).isCoolDown() && beginPlayerFire(skillFromID))
				{
					break;
				}
			}
		}
	}

	private bool isUseMPHP(sbyte type)
	{
		switch (type)
		{
		case 1:
			if (GameCanvas.loadmap.idMap == 986 || GameCanvas.loadmap.idMap == 985)
			{
				return false;
			}
			if (Hp * 100 / maxHp <= MsgAutoMPHP.hp)
			{
				return true;
			}
			break;
		case 2:
			if (Mp * 100 / maxMp <= MsgAutoMPHP.mp)
			{
				return true;
			}
			break;
		}
		return false;
	}

	public MainItem getPotionAuto(sbyte mphp)
	{
		MainItem mainItem = null;
		for (int i = 0; i < vecInventory.size(); i++)
		{
			MainItem mainItem2 = (MainItem)vecInventory.elementAt(i);
			if (mainItem2.typeObject == 4 && mphp == mainItem2.Hp_Mp_Other && mainItem2.numPotion > 0 && (mainItem == null || (MsgAutoMPHP.typeUu == 0 && mainItem2.value < mainItem.value) || (MsgAutoMPHP.typeUu == 1 && mainItem2.value > mainItem.value)))
			{
				mainItem = mainItem2;
			}
		}
		return mainItem;
	}

	private void autoGetItem()
	{
		if (GameCanvas.gameTick % 200 == 0)
		{
			if (isFullInven)
			{
				Interface_Game.addInfoPlayerNormal(T.fullInven, mFont.tahoma_7_white);
				return;
			}
			if (vecInventory.size() >= maxInventory)
			{
				isFullInven = true;
			}
		}
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject != null && !mainObject.isSend && mainObject.mItemDrop == null)
			{
				bool flag = false;
				if (mainObject.namePlayer != string.Empty)
				{
					flag = false;
				}
				else if (mainObject.typeObject == 5 || mainObject.typeObject == 7)
				{
					flag = true;
				}
				else if ((mainObject.typeObject == 4 || mainObject.typeObject == 3) && setGetItem(mainObject))
				{
					flag = true;
				}
				if (flag && MainObject.getDistance(x, y, mainObject.x, mainObject.y) < wFocus)
				{
					GlobalService.gI().Get_Item_Map(mainObject.ID, mainObject.typeObject);
					mainObject.isSend = true;
				}
			}
		}
	}

	private bool setGetItem(MainObject obj)
	{
		if (obj.typeObject == 4)
		{
			if (obj.colorName == 5)
			{
				if (MsgAutoGetItem.mValue[2] == 0)
				{
					return true;
				}
			}
			else
			{
				if (MsgAutoGetItem.mValue[1] == 3)
				{
					return false;
				}
				if (MsgAutoGetItem.mValue[1] == 0)
				{
					return true;
				}
				if (obj.colorName == 4 && MsgAutoGetItem.mValue[1] == 2)
				{
					return true;
				}
				if (obj.colorName == 6 && MsgAutoGetItem.mValue[1] == 1)
				{
					return true;
				}
			}
		}
		else if (obj.typeObject == 3 && !isFullInven)
		{
			if (MsgAutoGetItem.mValue[0] == 3)
			{
				return false;
			}
			if (MsgAutoGetItem.mValue[0] == 0)
			{
				return true;
			}
			if (obj.colorName > MsgAutoGetItem.mValue[0])
			{
				return true;
			}
		}
		return false;
	}

	private void updateDelay()
	{
		IDictionaryEnumerator enumerator = Player.delaySkill.GetEnumerator();
		while (enumerator.MoveNext())
		{
			DelaySkill delaySkill = (DelaySkill)enumerator.Value;
			if (delaySkill.value > -150)
			{
				delaySkill.value -= (int)(GameCanvas.timeNow - delaySkill.timebegin);
				delaySkill.timebegin = GameCanvas.timeNow;
			}
		}
	}

	public void updatePlayer()
	{
		if (LoadMap.specMap == 3)
		{
			updateMapTrain();
		}
		if (Hp <= 0)
		{
			Hp = 0;
			Mp = 0;
			if (Action != 4)
			{
				if (plashNow != null)
				{
					plashNow = null;
				}
				if (skillCurrent != null)
				{
					skillCurrent.isRemove = true;
				}
				if (posTransRoad != null)
				{
					posTransRoad = null;
				}
				setStart_EndAutoFire(isAu: false);
				Interface_Game.vecEffCurrent.removeAllElements();
				setCmdGame();
				timeDie++;
				if (timeDie >= 40)
				{
					beginDie(null);
				}
			}
			checkAutoRevice();
		}
		if (posTransRoad != null)
		{
			updatePosTrans();
			move_to_XY();
		}
		else if (!isSendMove)
		{
			isSendMove = true;
			xStand = x;
			yStand = y;
		}
		if (MainObject.getDistance(xStand, yStand, x, y) >= 50 || timeStand > 20 || tickAfterSkill == 0)
		{
			xStand = x;
			yStand = y;
			if (timeStand > 20)
			{
				timeStand = -1;
			}
			if (tickAfterSkill == 0)
			{
				tickAfterSkill = -1;
			}
			if (isSendMove && !check_Move_EffSpec() && LoadMap.specMap != 3 && typeActionBoat == 0)
			{
				GlobalService.gI().Obj_Move((short)x, (short)y);
			}
		}
		if (msattam != null)
		{
			mSatnhan = new short[msattam.Length];
			for (int i = 0; i < msattam.Length; i++)
			{
				mSatnhan[i] = msattam[i];
			}
			msattam = null;
		}
		if (LoadMap.specMap == 4 && boatSea != null && boatSea.ID == ID && GameCanvas.gameTick % 20 == 0)
		{
			setMoveBoatOther();
		}
	}

	private void checkAutoRevice()
	{
		if (AutoRevice != 0)
		{
			if (tickAutoRevice <= 0)
			{
				GlobalService.gI().Auto_revice(1);
				tickAutoRevice = 100;
			}
			else
			{
				tickAutoRevice--;
			}
		}
	}

	private void setMoveBoatOther()
	{
		boatSea.boatSetVaCham(0, 0);
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject == this || mainObject.boatSea == null || mainObject.boatSea.ID != mainObject.ID || mainObject.vx != 0 || mainObject.vy != 0)
			{
				continue;
			}
			mainObject.boatSea.boatSetVaCham(0, 0);
			if (!CRes.setVaCham(boatSea, mainObject.boatSea))
			{
				continue;
			}
			if (CRes.abs(mainObject.xSeaAnchor - mainObject.x) < 48 && CRes.abs(mainObject.ySeaAnchor - mainObject.y) < 48)
			{
				mainObject.toX = mainObject.xSeaAnchor;
				mainObject.toY = mainObject.ySeaAnchor;
				mainObject.xSeaAnchor = mainObject.x;
				mainObject.ySeaAnchor = mainObject.y;
				continue;
			}
			int xset = mainObject.x;
			int yset = mainObject.y;
			for (int j = 0; j < 4; j++)
			{
				xset = mainObject.x + CRes.random_Am(24, 48);
				yset = mainObject.y + CRes.random_Am(24, 48);
				if (!LoadMap.Tile_Stand(GameCanvas.loadmap.getTile(xset, yset)))
				{
					break;
				}
			}
			mainObject.toX = xset;
			mainObject.toY = yset;
			mainObject.xSeaAnchor = mainObject.x;
			mainObject.ySeaAnchor = mainObject.y;
		}
	}

	public override void updateMapTrain()
	{
		if (GameCanvas.gameScr.left != GameCanvas.gameScr.cmdGetXpMapTrain || GameCanvas.gameScr.right != GameCanvas.gameScr.cmdThoatFormMapTrain)
		{
			GameCanvas.gameScr.left = GameCanvas.gameScr.cmdGetXpMapTrain;
			GameCanvas.gameScr.right = GameCanvas.gameScr.cmdThoatFormMapTrain;
			center = null;
		}
		if (GameCanvas.gameTick % 125 == 0)
		{
			GlobalService.gI().Get_Xp_Map_Train(0);
		}
		if ((GameCanvas.gameTick % 75 == 0 || !isBeginTrain) && CRes.random(3) == 0 && Action != 2)
		{
			GameScreen.player.toX = GameScreen.player.x;
			GameScreen.player.toY = GameScreen.player.y;
			if (GameScreen.player.posTransRoad != null)
			{
				GameScreen.player.countAutoMove = 1;
			}
			for (int i = 0; i < 10; i++)
			{
				int num = CRes.random(MainObject.mPosMapTrain.Length);
				if (num != indexPosMapTrain)
				{
					indexPosMapTrain = num;
					break;
				}
			}
			posTransRoad = GameCanvas.loadmap.updateFindRoad(MainObject.mPosMapTrain[indexPosMapTrain][0], MainObject.mPosMapTrain[indexPosMapTrain][1], GameScreen.player.x / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, 200, this);
			AutoFireCur = 0;
			isBeginTrain = true;
		}
		if (posTransRoad == null)
		{
			setStart_EndAutoFire(isAu: true);
		}
	}

	private void updatePosTrans()
	{
		if (CRes.abs(x - toX) > vMax || CRes.abs(y - toY) > vMax)
		{
			return;
		}
		if (countAutoMove > posTransRoad.Length - 1)
		{
			countAutoMove = 0;
			posTransRoad = null;
			xStopMove = 0;
			yStopMove = 0;
			return;
		}
		if (countAutoMove == posTransRoad.Length - 1 && xStopMove > 0 && yStopMove > 0)
		{
			toX = xStopMove;
			toY = yStopMove;
		}
		else
		{
			sbyte b = (sbyte)(posTransRoad[countAutoMove] >> 8);
			sbyte b2 = (sbyte)(posTransRoad[countAutoMove] & 0xFF);
			toX = b * LoadMap.wTile + LoadMap.wTile / 2;
			toY = b2 * LoadMap.wTile + LoadMap.wTile / 2;
		}
		countAutoMove++;
	}

	public void updateKey()
	{
		if (LoadMap.specMap == 3 || typeActionBoat != 0 || isBlock)
		{
			return;
		}
		if (!isGhost)
		{
			bool flag = false;
			if (Action != 4 && Action != 2 && Action != 3 && isSendMove && posTransRoad == null && Hp > 0)
			{
				vx = 0;
				vy = 0;
				setKeyMoveNew();
				if (vx == 0 && vy == 0)
				{
					if (timeStand != -1)
					{
						timeStand++;
					}
				}
				else
				{
					flag = true;
					timeStand = 0;
					setStart_EndAutoFire(isAu: false);
				}
			}
			if (flag)
			{
				skillCurrent = null;
			}
			if (GameCanvas.keyActionUni(6) && GameScreen.objFocus != null)
			{
				GameCanvas.ClearActionUni(6);
				if (!GameCanvas.loadmap.mapLang())
				{
					GameScreen.interfaceGame.selectPointer(2);
				}
			}
			if (GameCanvas.keyActionUni(8))
			{
				GameCanvas.ClearActionUni(8);
				GameCanvas.gameScr.cmdEvent.perform();
			}
		}
		if (GameCanvas.keyActionUni(1))
		{
			setActionHotKey(0);
		}
		else if (GameCanvas.keyActionUni(3))
		{
			setActionHotKey(1);
		}
		else if (GameCanvas.keyActionUni(5))
		{
			setActionHotKey(2);
		}
		else if (GameCanvas.keyActionUni(7))
		{
			setActionHotKey(3);
		}
		else if (GameCanvas.keyActionUni(9))
		{
			setActionHotKey(999);
		}
		if (GameCanvas.keyActionUni(10))
		{
			GameCanvas.ClearActionUni(10);
			GameCanvas.gameScr.cmdChatPopup.perform();
		}
		if (GameCanvas.keyMyHold[40] || (GameCanvas.isTouch && GameCanvas.keyMyHold[12]))
		{
			GameCanvas.clearKeyHold(40);
			GameCanvas.clearKeyHold(12);
			GameCanvas.gameScr.cmdInfoMe.perform();
		}
		if (GameCanvas.keyMyHold[41] || (GameCanvas.isTouch && GameCanvas.keyMyHold[13]))
		{
			GameCanvas.clearKeyHold(41);
			GameCanvas.clearKeyHold(13);
			GameCanvas.gameScr.cmdNextFocus.perform();
		}
		if (GameCanvas.keyActionUni(4))
		{
			GameCanvas.ClearActionUni(4);
			if (GameCanvas.isTouch)
			{
				Interface_Game.startQuickMenu();
			}
			else
			{
				actionPC(4);
			}
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			if (GameCanvas.keyActionUni(11))
			{
				GameCanvas.ClearActionUni(11);
				actionPC(11);
			}
			else if (GameCanvas.keyActionUni(12))
			{
				GameCanvas.ClearActionUni(12);
				actionPC(12);
			}
			else if (GameCanvas.keyActionUni(13))
			{
				GameCanvas.ClearActionUni(13);
				actionPC(13);
			}
			else if (GameCanvas.keyActionUni(14))
			{
				GameCanvas.ClearActionUni(14);
				actionPC(14);
			}
			else if (GameCanvas.keyActionUni(16))
			{
				GameCanvas.ClearActionUni(16);
				actionPC(16);
			}
			else if (GameCanvas.keyActionUni(15))
			{
				GameCanvas.ClearActionUni(15);
				GameCanvas.gameScr.cmdFriendList.perform();
			}
		}
	}

	public void actionPC(sbyte type)
	{
		GlobalService.gI().Update_Pk_Point();
		GameCanvas.tabInven.setTypeInven(0);
		GameCanvas.tabAllScr.Show(GameCanvas.gameScr);
		resetAction();
		GameCanvas.clearAll();
		if (GameCanvas.currentScreen != GameCanvas.tabAllScr)
		{
			return;
		}
		switch (type)
		{
		case 11:
			GameCanvas.tabAllScr.idSelect = 0;
			break;
		case 12:
			GameCanvas.tabAllScr.idSelect = 4;
			break;
		case 13:
			GameCanvas.tabAllScr.idSelect = 2;
			break;
		case 14:
			GameCanvas.tabAllScr.idSelect = 3;
			break;
		case 16:
			GameCanvas.tabAllScr.idSelect = 1;
			break;
		case 4:
			if (GameCanvas.tabAllScr.vecTabs.size() == 6)
			{
				GameCanvas.tabAllScr.idSelect = 5;
			}
			break;
		}
		GameCanvas.tabAllScr.setTabSelect();
		GameCanvas.tabAllScr.tabCurrent.beginFocus();
	}

	private void setKeyMoveNew()
	{
		for (int i = 0; i < 4; i++)
		{
			if (GameCanvas.keyMove(i))
			{
				if (mindexkey[i] == -1)
				{
					mindexkey[i] = indexkey;
					indexkey++;
				}
			}
			else
			{
				mindexkey[i] = -1;
			}
		}
		int num = -1;
		int num2 = -1;
		for (int j = 0; j < mindexkey.Length; j++)
		{
			if (mindexkey[j] > num)
			{
				num = mindexkey[j];
				num2 = j;
			}
		}
		if (num2 > -1)
		{
			setKeyMoveNew(num2);
		}
		else
		{
			indexkey = 0;
		}
	}

	private void setKeyMoveNew(int valuemax)
	{
		switch (valuemax)
		{
		case 0:
			Action = 1;
			Dir = 0;
			vx = -vMax;
			vy = 0;
			break;
		case 2:
			Action = 1;
			Dir = 2;
			vx = vMax;
			vy = 0;
			break;
		case 1:
			Action = 1;
			if (LoadMap.specMap == 4)
			{
				vy = -vMaxYSea;
			}
			else
			{
				vy = -vMax;
			}
			vx = 0;
			break;
		case 3:
			Action = 1;
			if (LoadMap.specMap == 4)
			{
				vy = vMaxYSea;
			}
			else
			{
				vy = vMax;
			}
			vx = 0;
			break;
		}
	}

	public void setFocus(bool allFocus)
	{
		if (GameScreen.isOnAutoPB || GameScreen.isOnRepeatQuest || GameScreen.isOnSuperBoss)
		{
			if (GameScreen.objFocus != null && GameScreen.objFocus.typeObject == 1 && (GameScreen.objFocus.Action == 4 || GameScreen.objFocus.isDie))
			{
				GameScreen.objFocus = null;
				GameCanvas.gameScr.center = null;
				if (Interface_Game.isPaintInfoFocus)
				{
					Interface_Game.isPaintInfoFocus = false;
				}
			}
		}
		else
		{
			if (GameScreen.isOnRepeatQuest || GameScreen.isOnSuperBoss)
			{
				return;
			}
			if (GameScreen.objFocus != null && ((GameScreen.objFocus.typeObject == 1 && (GameScreen.objFocus.Action == 4 || GameScreen.objFocus.isDie)) || MainObject.getDistance(GameScreen.objFocus.x, GameScreen.objFocus.y, x, y) > wFocus + 60))
			{
				GameScreen.objFocus = null;
				GameCanvas.gameScr.center = null;
				if (Interface_Game.isPaintInfoFocus)
				{
					Interface_Game.isPaintInfoFocus = false;
				}
			}
			if (Action == 2 || Action == 4 || GameScreen.objFocus != null)
			{
				return;
			}
			int num = GameScreen.vecPlayers.size();
			if (indexFocus > num - 1)
			{
				indexFocus = num - 1;
			}
			int num2 = 1000;
			int num3 = -1;
			MainObject mainObject = null;
			for (int i = 0; i < num; i++)
			{
				MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt((i + indexFocus) % num);
				int distance = MainObject.getDistance(mainObject2.x, mainObject2.y, x, y);
				if (distance <= wFocus)
				{
					int num4 = setTypeFocus(mainObject2.typeObject);
					if (num4 >= num3 && !CheckSkipFocus(mainObject2) && (mainObject2.typeObject != 2 || !allFocus) && (distance < num2 || num4 > num3))
					{
						mainObject = mainObject2;
						num2 = distance;
						indexFocus = i;
						num3 = num4;
					}
				}
			}
			if (mainObject != null)
			{
				GameScreen.objFocus = mainObject;
				if (!GameCanvas.isTouch)
				{
					GameCanvas.gameScr.center = GameScreen.objFocus.getCenterCmd();
				}
			}
		}
	}

	private int setTypeFocus(int typeObj)
	{
		int result = -1;
		switch (typeObj)
		{
		case 0:
			result = 0;
			break;
		case 1:
			result = 1;
			break;
		case 3:
		case 4:
			result = 2;
			break;
		case 2:
			result = 3;
			break;
		}
		return result;
	}

	private bool CheckSkipFocus(MainObject obj)
	{
		if (obj == GameScreen.player || ((obj.Action == 4 || obj.isDie) && obj.typeObject != 0) || obj.isRemove || (GameScreen.objFocus != null && obj == GameScreen.objFocus) || obj.typeObject == 10)
		{
			return true;
		}
		return false;
	}

	public void nextFocus()
	{
		setStart_EndAutoFire(isAu: false);
		if (Action == 2)
		{
			return;
		}
		int num = -1;
		if (GameScreen.objFocus != null)
		{
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				if ((MainObject)GameScreen.vecPlayers.elementAt(i) == GameScreen.objFocus)
				{
					num = i;
					break;
				}
			}
		}
		if (typePK == 0)
		{
			if (num >= 0)
			{
				for (int j = num; j < GameScreen.vecPlayers.size(); j++)
				{
					MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(j);
					if (mainObject.typeObject == 0 && !CheckSkipFocus(mainObject) && MainObject.getDistance(mainObject.x, mainObject.y, x, y) < wFocus)
					{
						GameScreen.objFocus = mainObject;
						if (!GameCanvas.isTouch)
						{
							GameCanvas.gameScr.center = GameScreen.objFocus.getCenterCmd();
						}
						return;
					}
				}
			}
			else
			{
				num = GameScreen.vecPlayers.size();
			}
		}
		if (num >= 0)
		{
			for (int k = num; k < GameScreen.vecPlayers.size(); k++)
			{
				MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(k);
				if (!CheckSkipFocus(mainObject2) && (typePK != 0 || mainObject2.typeObject != 0) && MainObject.getDistance(mainObject2.x, mainObject2.y, x, y) < wFocus)
				{
					GameScreen.objFocus = mainObject2;
					if (!GameCanvas.isTouch)
					{
						GameCanvas.gameScr.center = GameScreen.objFocus.getCenterCmd();
					}
					return;
				}
			}
		}
		else
		{
			num = GameScreen.vecPlayers.size();
		}
		for (int l = 0; l < num; l++)
		{
			MainObject mainObject3 = (MainObject)GameScreen.vecPlayers.elementAt(l);
			if (!CheckSkipFocus(mainObject3) && (typePK != 0 || mainObject3.typeObject != 0) && MainObject.getDistance(mainObject3.x, mainObject3.y, x, y) < wFocus)
			{
				GameScreen.objFocus = mainObject3;
				if (!GameCanvas.isTouch)
				{
					GameCanvas.gameScr.center = GameScreen.objFocus.getCenterCmd();
				}
				break;
			}
		}
	}

	public void nextMonster()
	{
		if (GameScreen.isOnRepeatQuest || GameScreen.isOnSuperBoss)
		{
			return;
		}
		int num = wFocus * 3 / 2;
		MainObject mainObject = null;
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject2 != null && mainObject2.Action != 4 && !mainObject2.isSend && mainObject2.typeObject != 10 && mainObject2.typeObject == 1)
			{
				int distance = MainObject.getDistance(x, y, mainObject2.x, mainObject2.y);
				if (distance < num)
				{
					num = distance;
					mainObject = mainObject2;
				}
			}
		}
		if (mainObject != null)
		{
			GameScreen.objFocus = mainObject;
			demUnFire = 0;
		}
		else
		{
			demUnFire++;
		}
	}

	public void setActionHotKey(int index)
	{
		if (index == 999)
		{
			GameCanvas.clearAll();
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				_ = (MainObject)GameScreen.vecPlayers.elementAt(i);
			}
			return;
		}
		GameCanvas.clearAll();
		if (isGhost)
		{
			Point_Focus point_Focus = new Point_Focus();
			point_Focus.dis = index;
			vecGhostInput.addElement(point_Focus);
			if (vecGhostInput.size() > 4)
			{
				vecGhostInput.removeElementAt(0);
			}
			GlobalService.gI().ghost((sbyte)index);
		}
		else if (Action != 2 && Action != 4 && Hp > 0)
		{
			Hotkey hotkey = hotkeyPlayer[currentTab][index];
			if (hotkey.itemcur != null)
			{
				hotkey.itemcur.Use_Item();
			}
			else if (hotkey.skill != null && hotkey.skill.isBuff && !GameCanvas.loadmap.mapLang() && setSkillBuff(index, hotkey))
			{
				beginPlayerFire(index, hotkey);
			}
			else if (GameScreen.objFocus != null)
			{
				GameScreen.objFocus.setFireObject(index);
			}
		}
	}

	public void setActionHotKeyBuff(int index)
	{
		GameCanvas.clearAll();
		if (Action != 2 && Action != 4 && Hp > 0)
		{
			Hotkey hotkey = hotkeyBuffPlayer[index];
			if (hotkey != null && hotkey.skill != null && !GameCanvas.loadmap.mapLang() && setSkillBuff(index, hotkey))
			{
				beginPlayerFire(index, hotkey);
			}
		}
	}

	private bool setSkillBuff(int index, Hotkey hot)
	{
		Skill_Info skillFromID = Skill_Info.getSkillFromID(hot.skill.ID);
		if (skillFromID.typeSkill == 2)
		{
			if (skillFromID.typeBuff == 1 || skillFromID.typeBuff == 2)
			{
				return true;
			}
			if (skillFromID.typeBuff == 3)
			{
				return setFightPk(GameScreen.objFocus);
			}
		}
		return false;
	}

	public void beginPlayerFirePoint()
	{
		if (Action == 4 || Hp <= 0)
		{
			return;
		}
		for (int i = 0; i < hotkeyPlayer[0].Length; i++)
		{
			int num = i;
			if (i == 0)
			{
				num = 2;
			}
			else if (i <= 2)
			{
				num = i - 1;
			}
			Hotkey hotkey = hotkeyPlayer[currentTab][num];
			if (hotkey.skill != null)
			{
				Skill_Info skillFromID = Skill_Info.getSkillFromID(hotkey.skill.ID);
				if (DelaySkill.getDelay(skillFromID.indexHotKey).isCoolDown() && Mp >= getManaNeedUse(skillFromID.manaLost))
				{
					beginPlayerFire(num, hotkey);
					break;
				}
			}
		}
	}

	public bool beginPlayerFire(Skill_Info skill_info)
	{
		if (skill_info == null)
		{
			return false;
		}
		setStart_EndAutoFire(isAu: true);
		if (!DelaySkill.getDelay(skill_info.indexHotKey).isCoolDown())
		{
			return false;
		}
		if ((LoadMap.specMap == 4 && skill_info.typeSkill == 1) || (LoadMap.specMap != 4 && skill_info.typeSkill == 4))
		{
			Interface_Game.addInfoPlayerNormal(T.khongdungduocmapnay, mFont.tahoma_7_white);
		}
		if (Mp < getManaNeedUse(skill_info.manaLost))
		{
			Interface_Game.addInfoPlayerNormal(T.manaLost, mFont.tahoma_7_white);
			return false;
		}
		if (check_Fire_EffSpec())
		{
			return false;
		}
		mVector mVector2 = new mVector();
		mVector2 = setSkillLan(skill_info);
		if (LoadMap.specMap == 3)
		{
			for (int i = 0; i < mVector2.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)mVector2.elementAt(i);
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					object_Effect_Skill.setHP(mainObject.maxHp / 10, mainObject.Hp - mainObject.maxHp / 10, 0);
				}
			}
		}
		MainSkill mainSkill = createSkill(skill_info);
		if (mainSkill == null || mVector2.size() == 0)
		{
			return false;
		}
		if (skill_info.typeSkill == 1 || skill_info.typeSkill == 4)
		{
			objAutoFrist = GameScreen.objFocus;
		}
		skillCurrent = new Start_Skill(this, mVector2, mainSkill);
		skillCurrent.setMoveFire();
		if (checkEffSpec(8))
		{
			skillCurrent.isRemove = true;
		}
		return true;
	}

	public int getManaNeedUse(int mana)
	{
		return mana + mana * TangManaUseSkill / 1000;
	}

	public void beginPlayerFire(int index)
	{
		Hotkey hot = hotkeyPlayer[currentTab][index];
		beginPlayerFire(index, hot);
	}

	public void beginPlayerFire(int index, Hotkey hot)
	{
		if (hot.skill != null)
		{
			Skill_Info skillFromID = Skill_Info.getSkillFromID(hot.skill.ID);
			beginPlayerFire(skillFromID);
		}
	}

	public void setCmdGame()
	{
		if (GameScreen.typeViewPlayer == 0)
		{
			if (GameCanvas.currentScreen != GameCanvas.gameScr)
			{
				return;
			}
			if (LoadMap.specMap == 3)
			{
				if (GameCanvas.gameScr.left != GameCanvas.gameScr.cmdGetXpMapTrain || GameCanvas.gameScr.right != GameCanvas.gameScr.cmdThoatFormMapTrain)
				{
					GameCanvas.gameScr.left = GameCanvas.gameScr.cmdGetXpMapTrain;
					GameCanvas.gameScr.right = GameCanvas.gameScr.cmdThoatFormMapTrain;
					center = null;
				}
			}
			else if (Action == 4)
			{
				if (!checkShowHoiSinh())
				{
					return;
				}
				if (!GameCanvas.isTouch)
				{
					if (GameCanvas.gameScr.left != GameCanvas.gameScr.cmdHoiSinh)
					{
						GameCanvas.gameScr.left = GameCanvas.gameScr.cmdHoiSinh;
					}
				}
				else if (GameCanvas.gameScr.center != GameCanvas.gameScr.cmdHoiSinh)
				{
					GameCanvas.gameScr.center = GameCanvas.gameScr.cmdHoiSinh;
					GameCanvas.gameScr.cmdHoiSinh = AvMain.setPosCMD(GameCanvas.gameScr.cmdHoiSinh, 0);
				}
			}
			else if (!GameCanvas.isTouch)
			{
				if (GameCanvas.gameScr.left != GameCanvas.gameScr.cmdInfoMe)
				{
					GameCanvas.gameScr.left = GameCanvas.gameScr.cmdInfoMe;
				}
				if (GameCanvas.gameScr.right != GameCanvas.gameScr.cmdNextFocus)
				{
					GameCanvas.gameScr.right = GameCanvas.gameScr.cmdNextFocus;
				}
			}
			else if (GameCanvas.gameScr.center == GameCanvas.gameScr.cmdHoiSinh || GameCanvas.gameScr.center == GameCanvas.gameScr.cmdVeLang)
			{
				GameCanvas.gameScr.center = null;
			}
		}
		else if (GameScreen.typeViewPlayer == 1)
		{
			if (GameCanvas.gameScr.left != GameCanvas.gameScr.cmdSettingView)
			{
				GameCanvas.gameScr.left = GameCanvas.gameScr.cmdSettingView;
			}
			if (GameCanvas.gameScr.right != GameCanvas.gameScr.cmdExitView)
			{
				GameCanvas.gameScr.right = GameCanvas.gameScr.cmdExitView;
			}
			if (GameCanvas.gameScr.center != null)
			{
				GameCanvas.gameScr.center = null;
			}
		}
	}

	public bool checkShowHoiSinh()
	{
		if (LoadMap.specMap == 1 || LoadMap.specMap == 2 || LoadMap.specMap == 10 || LoadMap.specMap == 11)
		{
			return false;
		}
		return true;
	}

	public bool setFightPk(MainObject objset)
	{
		if (objset == null || objset.returnAction() || objset.Action == 4)
		{
			return false;
		}
		if (GameCanvas.loadmap.mapLang())
		{
			return false;
		}
		if (objset.typeObject == 1)
		{
			if (!objset.isTru() || typePK != objset.typePK)
			{
				return true;
			}
			return false;
		}
		if (objset.typeObject == 2)
		{
			return false;
		}
		if (typePirate != -1)
		{
			if (objset.typePirate == typePirate && objset.IDMainShiper == ID)
			{
				return false;
			}
			if ((typePirate == 0 || typePirate == 1) && objset.typePirate == 2)
			{
				return true;
			}
			if (typePirate == 2 && objset.typePirate != -1)
			{
				return true;
			}
		}
		if (objset.Lv < LvMinPk)
		{
			return false;
		}
		if (typePK == 0)
		{
			return true;
		}
		if (objset.typePK == 0)
		{
			return true;
		}
		if (objset.typePK == 1)
		{
			return true;
		}
		if (typePK == 2 && objset.typePK >= 0)
		{
			return true;
		}
		if (typePK == 3 && !checkCungClan(objset) && objset.typePK >= 0)
		{
			return true;
		}
		if (typePK >= 4 && objset.typePK >= 0 && objset.typePK != typePK && objset.typePlayer != 2 && objset.typePlayer != 3)
		{
			return true;
		}
		if (typePK == 1)
		{
			for (int i = 0; i < mSatnhan.Length; i++)
			{
				if (objset.ID == mSatnhan[i])
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	private bool checkCungClan(MainObject objset)
	{
		if (clan == null)
		{
			return false;
		}
		if (objset.clan == null)
		{
			return false;
		}
		if (clan.ID != objset.clan.ID)
		{
			return false;
		}
		return true;
	}

	public override void addSkillFire(MainSkill skill, mVector vec)
	{
		tickAfterSkill = 70;
		if (GameScreen.indexHelp == 2)
		{
			MainHelp.setNextHelp(isHanhTrang: false);
		}
		if (vec != null)
		{
			for (int i = 0; i < vec.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vec.elementAt(i);
				if (object_Effect_Skill.tem != 1)
				{
					break;
				}
				if (object_Effect_Skill.hpLast <= 0)
				{
					try
					{
						MainQuest.updateDataQuestKillMon(object_Effect_Skill.ID);
					}
					catch (Exception)
					{
					}
				}
			}
		}
		bool isAdd = true;
		for (int j = 0; j < GameScreen.VecEffect.size(); j++)
		{
			MainEffect mainEffect = (MainEffect)GameScreen.VecEffect.elementAt(j);
			if (mainEffect.valueEffect == 0 && !mainEffect.isAddHP && !mainEffect.isStop && mainEffect.typeEffect == skill.typeEffSkill && mainEffect.timeAddNum == -1 && mainEffect.objFireMain != null && mainEffect.objFireMain == GameScreen.player)
			{
				mainEffect.replaceHP(vec);
				mainEffect.isEff = false;
				mainEffect.isAddHP = true;
				isAdd = false;
				break;
			}
		}
		Effect_Skill.setHP_New(vec, this, isAdd);
	}

	public static void setHotKey(int index, MainSkill skill, MainItem item)
	{
		if (skill != null)
		{
			hotkeyPlayer[currentTab][index].setSkill(skill, skill.idIcon);
		}
		else if (item != null)
		{
			hotkeyPlayer[currentTab][index].setPotion(item);
		}
		if (LoadMap.specMap != 4)
		{
			GameCanvas.saveRms.saveHotKey();
		}
	}

	public static void setHotKeyBuff()
	{
		if (!GameCanvas.isTouch || vecListSkill == null)
		{
			return;
		}
		if (hotkeyBuffPlayer == null)
		{
			hotkeyBuffPlayer = new Hotkey[6];
		}
		for (int i = 0; i < hotkeyBuffPlayer.Length; i++)
		{
			hotkeyBuffPlayer[i] = null;
		}
		int num = 0;
		for (int j = 0; j < vecListSkill.size(); j++)
		{
			Skill_Info skill_Info = (Skill_Info)vecListSkill.elementAt(j);
			if (skill_Info.Lv_RQ >= 0 && skill_Info.typeSkill == 2)
			{
				MainSkill mainSkill = new MainSkill(skill_Info.ID, skill_Info.typeEffSkill);
				hotkeyBuffPlayer[num] = new Hotkey();
				mainSkill.indexHotKey = skill_Info.indexHotKey;
				mainSkill.timeDelay = skill_Info.timeDelay;
				hotkeyBuffPlayer[num].setSkill(mainSkill, skill_Info.idIcon);
				num++;
			}
			if (num == 6)
			{
				break;
			}
		}
	}

	public static void setDelaySkill(int index, int time, bool isNotCooldown, sbyte isSkill)
	{
		DelaySkill delay = DelaySkill.getDelay(index);
		if (!isNotCooldown)
		{
			if (GameScreen.player.checkEffSpec(8))
			{
				time += time / 2;
			}
			_ = (float)time * (float)GiamCountDownCur / 1000f;
			time -= time * GiamCountDownCur / 1000;
		}
		if (time < 1000)
		{
			time = 1000;
		}
		delay.timebegin = GameCanvas.timeNow;
		delay.value = time + 200;
		delay.limit = time + 200;
		delay.typeSkill = isSkill;
	}

	public void setUseMana(int manause)
	{
		Mp -= getManaNeedUse(manause);
	}

	public MainSkill createSkill(Skill_Info info)
	{
		if (info == null)
		{
			return null;
		}
		MainSkill mainSkill = new MainSkill(info.ID, info.typeEffSkill);
		if (info.typeSkill == 2)
		{
			mainSkill.setTypeBuff(1, 46, 0);
		}
		mainSkill.range = info.range;
		mainSkill.timeDelay = info.timeDelay;
		mainSkill.indexHotKey = info.indexHotKey;
		mainSkill.mana = info.manaLost;
		mainSkill.typeDevil = info.typeDevil;
		mainSkill.lvDevil = info.LvDevilSkill;
		return mainSkill;
	}

	public static bool isClazz(sbyte type)
	{
		if (GameScreen.player.clazz == type || type == 0)
		{
			return true;
		}
		return false;
	}

	public mVector setSkillLan(Skill_Info skill)
	{
		mVector mVector2 = new mVector();
		if (skill.typeSkill == 2)
		{
			if (skill.typeBuff == 1 || skill.typeBuff == 2 || skill.typeBuff == 2)
			{
				Object_Effect_Skill o = new Object_Effect_Skill(ID, typeObject);
				mVector2.addElement(o);
			}
			if (GameScreen.objFocus != null && ((skill.typeBuff == 2 && GameScreen.objFocus.typeObject == 0) || skill.typeBuff == 3))
			{
				Object_Effect_Skill o2 = new Object_Effect_Skill(GameScreen.objFocus.ID, GameScreen.objFocus.typeObject);
				mVector2.addElement(o2);
			}
		}
		else
		{
			Object_Effect_Skill o3 = new Object_Effect_Skill(GameScreen.objFocus.ID, GameScreen.objFocus.typeObject);
			mVector2.addElement(o3);
			sbyte b = GameScreen.objFocus.typeObject;
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				if (mVector2.size() >= skill.nTarget)
				{
					break;
				}
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject.typeObject == b && mainObject != GameScreen.objFocus && setFightPk(mainObject) && MainObject.getDistance(GameScreen.objFocus.x, GameScreen.objFocus.y, mainObject.x, mainObject.y) <= 120)
				{
					Object_Effect_Skill o4 = new Object_Effect_Skill(mainObject.ID, b);
					mVector2.addElement(o4);
				}
			}
		}
		return mVector2;
	}

	public void setAutoFire(bool isAutoNew)
	{
		if (GameCanvas.gameTick % 5 != 0 || GameCanvas.timeNow - timeFristSkill <= 1000 || skillCurrent != null)
		{
			return;
		}
		setDisAuto();
		if (isBack)
		{
			tickMaxBack++;
			if (tickMaxBack > 200)
			{
				isBack = false;
			}
			return;
		}
		tickMaxBack = 0;
		if ((GameScreen.isOnRepeatQuest && GameScreen.objFocus == null) || (GameScreen.isOnSuperBoss && GameScreen.objFocus == null))
		{
			AutoFireCur = 0;
			return;
		}
		MainObject objFocus = GameScreen.objFocus;
		if (!GameScreen.isOnAutoPB)
		{
			if (objFocus != null && objFocus.typeObject == 0 && (objAutoFrist == null || objAutoFrist.typeObject == 0))
			{
				if (!setFightPk(objFocus))
				{
					AutoFireCur = 0;
					return;
				}
			}
			else
			{
				if (objFocus == null || objFocus.returnAction() || objFocus.typeObject != 1 || objFocus.isDie || objFocus.Hp <= 0)
				{
					nextMonster();
					if (GameScreen.objFocus != null)
					{
						if (GameScreen.objFocus.typeObject != 1)
						{
							return;
						}
						GameScreen.addEffectEnd_ObjTo(24, 0, GameScreen.objFocus.x, GameScreen.objFocus.y, GameScreen.objFocus.ID, GameScreen.objFocus.typeObject, 0, null);
						Interface_Game.isPaintInfoFocus = true;
					}
				}
				objFocus = GameScreen.objFocus;
				if (objFocus == null || objFocus.typeObject != 1)
				{
					return;
				}
			}
		}
		else if (objFocus == null || objFocus.isRemove || objFocus.isStop || objFocus.isDie || objFocus.Hp <= 0)
		{
			AutoBattlefield.isChangeFocus = true;
			if (GameScreen.objFocus == null)
			{
				return;
			}
		}
		int num = hotkeyPlayer[0].Length;
		for (int i = 0; i < num; i++)
		{
			int num2 = (i + IndexFire) % num;
			if (isAutoNew)
			{
				num2 = IndexFire % num;
				IndexFire = 2;
			}
			Hotkey hotkey = hotkeyPlayer[currentTab][num2];
			if (hotkey.skill != null && DelaySkill.getDelay(hotkey.skill.indexHotKey).isCoolDown())
			{
				Skill_Info skillFromID = Skill_Info.getSkillFromID(hotkey.skill.ID);
				if (skillFromID.typeSkill != 2 && skillFromID != null && getManaNeedUse(skillFromID.manaLost) <= Mp)
				{
					GameScreen.objFocus.setFireObject(num2);
					timeFristSkill = GameCanvas.timeNow;
					if (!isAutoNew)
					{
						IndexFire = num2 + 1;
					}
					break;
				}
			}
			if (isAutoNew)
			{
				break;
			}
		}
	}

	private void setDisAuto()
	{
		if (isBack)
		{
			if (MainObject.getDistance(x, y, xBeginAuto, yBeginAuto) <= LoadMap.wTile * 2)
			{
				isBack = false;
			}
			if (posTransRoad == null && Action == 0)
			{
				GameScreen.player.toX = GameScreen.player.x;
				GameScreen.player.toY = GameScreen.player.y;
				posTransRoad = GameCanvas.loadmap.updateFindRoad(xBeginAuto / LoadMap.wTile, yBeginAuto / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 80, this);
				if (posTransRoad != null && posTransRoad.Length > 80)
				{
					posTransRoad = null;
				}
			}
		}
		else if (objAutoFrist == null || GameScreen.objFocus == null || objAutoFrist != GameScreen.objFocus)
		{
			distest = MainObject.getDistance(x, y, xBeginAuto, yBeginAuto);
			if (distest > maxRangeAuto + 200)
			{
				isBack = true;
			}
			else if (distest > maxRangeAuto + 100 && (GameScreen.objFocus == null || GameScreen.objFocus.Hp > GameScreen.objFocus.maxHp / 10 || GameScreen.objFocus.isHumanMonster == 0))
			{
				isBack = true;
			}
			else if (distest > maxRangeAuto && (GameScreen.objFocus == null || GameScreen.objFocus.Hp > GameScreen.objFocus.maxHp - GameScreen.objFocus.maxHp / 20 || GameScreen.objFocus.isHumanMonster == 0))
			{
				isBack = true;
			}
		}
	}

	public static void setStart_EndAutoFire(bool isAu)
	{
		if (isAu)
		{
			if (AutoFireCur != typeAutoFireMain)
			{
				AutoFireCur = typeAutoFireMain;
				xBeginAuto = GameScreen.player.x;
				yBeginAuto = GameScreen.player.y;
			}
		}
		else if (AutoFireCur >= 1)
		{
			AutoFireCur = 0;
		}
	}

	public bool checkEffSpec(short typeEff)
	{
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			if (((Effect_Spec)vecEffspec.elementAt(i)).typeEffect == typeEff)
			{
				return true;
			}
		}
		return false;
	}

	public bool check_Fire_EffSpec()
	{
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			Effect_Spec effect_Spec = (Effect_Spec)vecEffspec.elementAt(i);
			if (effect_Spec.typeEffect == 1 || effect_Spec.typeEffect == 5)
			{
				return true;
			}
		}
		for (int j = 0; j < vecDataEff.size(); j++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(j);
			if (dataSkillEff != null && dataSkillEff.typeMove == 1)
			{
				return true;
			}
		}
		return false;
	}

	public bool check_Move_EffSpec()
	{
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			Effect_Spec effect_Spec = (Effect_Spec)vecEffspec.elementAt(i);
			if (effect_Spec.typeEffect == 1 || effect_Spec.typeEffect == 8)
			{
				return true;
			}
		}
		for (int j = 0; j < vecDataEff.size(); j++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(j);
			if (dataSkillEff != null && dataSkillEff.typeMove == 1)
			{
				return true;
			}
		}
		return false;
	}

	public void updateMoney()
	{
		if (beliTest > beli)
		{
			Interface_Game.tickeffShowMoney = 60;
			Interface_Game.typeShowMoney = 0;
			if (valueTestBeli == 0L)
			{
				timeTestBeli = 0L;
				valueTestBeli = (beliTest - beli) / 10;
				if (valueTestBeli < 100)
				{
					valueTestBeli = 100L;
				}
				return;
			}
			beliTest -= valueTestBeli;
			if (beliTest <= beli)
			{
				beliTest = beli;
				valueTestBeli = 0L;
				timeTestBeli = 0L;
			}
		}
		else
		{
			beliTest = beli;
			valueTestBeli = 0L;
			timeTestBeli = 0L;
		}
	}

	public override void setPosUpBoat()
	{
		toX = x;
		toY = y;
		if (typeActionBoat == 1)
		{
			int num = xUpBoat - 24;
			int num2 = yUpBoat + 24;
			posTransRoad = GameCanvas.loadmap.updateFindRoad(num / LoadMap.wTile, num2 / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 20, this);
			if (posTransRoad == null || posTransRoad.Length > 20)
			{
				num = xUpBoat + 24;
				num2 = yUpBoat + 24;
				posTransRoad = GameCanvas.loadmap.updateFindRoad(num / LoadMap.wTile, num2 / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 20, this);
			}
			if (posTransRoad == null || posTransRoad.Length > 20)
			{
				posTransRoad = null;
				x = xUpBoat - 24;
				y = yUpBoat + 24;
			}
		}
		else if (typeActionBoat == 3)
		{
			int num3 = xUpBoat - 24;
			int num4 = yUpBoat - 48;
			posTransRoad = GameCanvas.loadmap.updateFindRoad(num3 / LoadMap.wTile, num4 / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 20, this);
			if (posTransRoad == null || posTransRoad.Length > 20)
			{
				num3 = xUpBoat + 24;
				num4 = yUpBoat - 48;
				posTransRoad = GameCanvas.loadmap.updateFindRoad(num3 / LoadMap.wTile, num4 / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 20, this);
			}
			if (posTransRoad == null || posTransRoad.Length > 20)
			{
				posTransRoad = null;
				x = xUpBoat - 24;
				y = yUpBoat - 48;
			}
		}
	}

	public override void setPosDownBoat()
	{
		toX = x;
		toY = y;
		int num = xUpBoat;
		int num2 = yUpBoat;
		posTransRoad = GameCanvas.loadmap.updateFindRoad(num / LoadMap.wTile, num2 / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 20, this);
		GameScreen.player.countAutoMove = 1;
		if (posTransRoad == null || posTransRoad.Length > 20)
		{
			posTransRoad = null;
			x = xUpBoat;
			y = yUpBoat;
		}
	}

	public override void setNextSea()
	{
		if (ReadMessenge.isNondata)
		{
			GameCanvas.readMess.readChangeMapNonData(GameCanvas.readMess.msgLuu, GameCanvas.readMess.idMapLuu);
		}
		else
		{
			GameCanvas.readMess.readChangeMapNew(GameCanvas.readMess.msgLuu, GameCanvas.readMess.idMapLuu);
		}
		tickJoinSea = 0;
	}

	public override void setXtoYto(int xto, int yto)
	{
		toX = x;
		toY = y;
		posTransRoad = GameCanvas.loadmap.updateFindRoad(xto / LoadMap.wTile, yto / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 30, this);
		GameScreen.player.countAutoMove = 1;
	}

	public static void SetMaterialToChest(sbyte type)
	{
		for (int i = 0; i < vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)vecInventory.elementAt(i);
			if (mainItem.typeObject == type)
			{
				mSystem.outz("vo 5");
				GlobalService.gI().Chest(1, mainItem.ID, mainItem.typeObject, mainItem.numPotion);
			}
		}
	}

	public static void SetMaterialToInven(sbyte type)
	{
		for (int i = 0; i < vecChest.size(); i++)
		{
			MainItem mainItem = (MainItem)vecChest.elementAt(i);
			if (mainItem.typeObject == type)
			{
				mSystem.outz("vo 6");
				GlobalService.gI().Chest(2, mainItem.ID, mainItem.typeObject, mainItem.numPotion);
			}
		}
	}

	public static void SetDiamondToChest()
	{
		for (int i = 0; i < vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)vecInventory.elementAt(i);
			if (mainItem.typeObject == 4 && ((mainItem.ID >= 44 && mainItem.ID <= 79) || (mainItem.ID >= 362 && mainItem.ID <= 367)))
			{
				mSystem.outz("vo 3");
				GlobalService.gI().Chest(1, mainItem.ID, mainItem.typeObject, mainItem.numPotion);
			}
		}
	}

	public static void SetDiamondToInven()
	{
		for (int i = 0; i < vecChest.size(); i++)
		{
			MainItem mainItem = (MainItem)vecChest.elementAt(i);
			if (mainItem.typeObject == 4 && ((mainItem.ID >= 44 && mainItem.ID <= 79) || (mainItem.ID >= 362 && mainItem.ID <= 367)))
			{
				mSystem.outz("vo 4");
				GlobalService.gI().Chest(2, mainItem.ID, mainItem.typeObject, mainItem.numPotion);
			}
		}
	}

	public override void resetAction()
	{
		mindexkey = new int[4] { -1, -1, -1, -1 };
		base.resetAction();
	}

	public override void actionStand()
	{
		if (tickAfterSkill > 0)
		{
			tickAfterSkill--;
		}
		if (f > feStand.Length - 1)
		{
			f = 0;
		}
		if (isGhost)
		{
			f = 0;
		}
		frame = feStand[f];
	}

	public static void SetGiamCountDown()
	{
		if (vecParty.size() == 0)
		{
			giamCountDownParty = 0;
		}
		GiamCountDownCur = (short)(giamCountDownParty + giamCountDownAtt);
	}

	public override void updateTimeSafe()
	{
		if (timeSafe > 0 && (int)(GameCanvas.timeNow - timeBeginSafe) > 1000)
		{
			timeSafe--;
			timeBeginSafe += 1000L;
		}
	}

	public override void addEffBuff(sbyte typeBuff, short effBuff, short time)
	{
		if (skillCurrent != null)
		{
			skillCurrent.beginSkill();
		}
		mVector mVector2 = new mVector();
		Object_Effect_Skill object_Effect_Skill = new Object_Effect_Skill(ID, typeObject);
		object_Effect_Skill.setHP(0, Hp, 0);
		mVector2.addElement(object_Effect_Skill);
		MainSkill mainSkill = new MainSkill(-1, effBuff);
		mainSkill.setTypeBuff(typeBuff, effBuff, time);
		setDataBeginSkill(mainSkill, mVector2);
	}

	public void StepAutoReconnect()
	{
		if (StepAutoRe == 0)
		{
			tickAutoRe = 0;
			if (GameCanvas.loadmap.mapLang())
			{
				StepAutoRe = 4;
			}
			else if (xBeginAuto != 0 && yBeginAuto != 0)
			{
				StepAutoRe = 1;
			}
			else
			{
				StepAutoRe = 4;
			}
		}
		else if (StepAutoRe == 1)
		{
			GameScreen.player.toX = GameScreen.player.x;
			GameScreen.player.toY = GameScreen.player.y;
			posTransRoad = GameCanvas.loadmap.updateFindRoad(xBeginAuto / LoadMap.wTile, yBeginAuto / LoadMap.wTile, x / LoadMap.wTile, y / LoadMap.wTile, 200, this);
			StepAutoRe = 2;
		}
		else if (StepAutoRe == 2)
		{
			if (posTransRoad == null)
			{
				nextMonster();
				setStart_EndAutoFire(isAu: true);
				StepAutoRe = 5;
				strHethong = strHethong + T.chathethong + GameCanvas.getTextTime() + "\n";
				GameCanvas.chatTabScr.addNewChat(T.hethong, string.Empty, strHethong, 1, isFocus: false);
			}
		}
		else
		{
			if (StepAutoRe != 5)
			{
				return;
			}
			tickAutoRe++;
			if (tickAutoRe % 200 != 0)
			{
				return;
			}
			if (AutoFireCur != typeAutoFireMain)
			{
				int num = 0;
				do
				{
					num++;
					nextMonster();
				}
				while (num != 10 && (GameScreen.objFocus == null || GameScreen.objFocus.typeObject != 1));
				setStart_EndAutoFire(isAu: true);
				tickAutoRe = 0;
			}
			else
			{
				StepAutoRe = 4;
			}
		}
	}

	public override void AddBuff(MainBuff buff)
	{
		for (int i = 0; i < vecBuffCur.size(); i++)
		{
			if (((MainBuff)vecBuffCur.elementAt(i)).IdBuff == buff.IdBuff)
			{
				vecBuffCur.removeElementAt(i);
				CheckBuffinEffCur(buff.IdBuff);
				i--;
			}
		}
		buff.setYlech(this);
		vecBuffCur.addElement(buff);
		Item_Skill_Eff item_Skill_Eff = new Item_Skill_Eff(buff.idIcon, buff.IdBuff, 1000);
		GameScreen.interfaceGame.addEffCurrent(item_Skill_Eff);
		setDelaySkill(item_Skill_Eff.indexHotKey, buff.timeBuff, isNotCooldown: true, 0);
	}

	public override void AddBuffNew(MainBuff buff)
	{
		for (int i = 0; i < vecBuffCurNew.size(); i++)
		{
			if (((MainBuff)vecBuffCurNew.elementAt(i)).IdBuff == buff.IdBuff)
			{
				vecBuffCurNew.removeElementAt(i);
				CheckBuffinEffCur(buff.IdBuff);
				i--;
			}
		}
		buff.setYlech(this);
		vecBuffCurNew.addElement(buff);
		Item_Skill_Eff item_Skill_Eff = new Item_Skill_Eff(buff.idIcon, buff.IdBuff, 1000);
		GameScreen.interfaceGame.addEffCurrent(item_Skill_Eff);
		setDelaySkill(item_Skill_Eff.indexHotKey, buff.timeBuff, isNotCooldown: true, 0);
	}

	public override void AddBuffNew2(MainBuff buff)
	{
		for (int i = 0; i < vecBuffCurNew.size(); i++)
		{
			if (((MainBuff)vecBuffCurNew.elementAt(i)).IdBuff == buff.IdBuff)
			{
				vecBuffCurNew.removeElementAt(i);
				CheckBuffinEffCur(buff.IdBuff);
				i--;
			}
		}
		buff.setYlech(this);
		vecBuffCurNew.addElement(buff);
	}

	public void CheckBuffinEffCur(short idbuff)
	{
		for (int i = 0; i < Interface_Game.vecEffCurrent.size(); i++)
		{
			MainItem mainItem = (MainItem)Interface_Game.vecEffCurrent.elementAt(i);
			if (mainItem.typeObject == 9 && mainItem.ID == idbuff)
			{
				mainItem.isRemove = true;
			}
		}
	}

	public void TestSkill(short typeEffSkill)
	{
		Skill_Info skillFromID = Skill_Info.getSkillFromID(hotkeyPlayer[0][2].skill.ID);
		skillFromID.typeEffSkill = typeEffSkill;
		beginPlayerFire(skillFromID);
	}
}
