using System;

public class MainObject : AvMain
{
	public DanhHieu danhieu;

	public string chatNPC = string.Empty;

	public string name = string.Empty;

	public string strChatPopup;

	public string namePlayer = string.Empty;

	public bool isDrop42;

	public string nameGiaotiep = string.Empty;

	public string npcChatMenu = string.Empty;

	public static string[] strHelp;

	public static int StepHelp = 0;

	public short ID;

	public short IdIcon;

	public short IDMainShiper;

	public short IdEffShow;

	public int[][] mActionMonSter;

	public int[] mActionStandMonSter;

	public short[] myBoat;

	public int[] mValuePvP;

	public Item_Drop[] mItemDrop;

	public sbyte typeMoveMonster;

	public short head = -1;

	public short body = -1;

	public short leg = -1;

	public short weapon = -1;

	public short hair = -1;

	public short hat = -1;

	public short weaponFashion = -1;

	public short headMain = -1;

	public short bodyMain = -1;

	public short legMain = -1;

	public short hairMain = -1;

	public int x;

	public int y;

	public int xLast;

	public int yLast;

	public int dx;

	public int dy;

	public int dyMain;

	public int dyMovePet;

	public int dySea;

	public int vySea;

	public int vx;

	public int vy;

	public int wOne;

	public int hOne;

	public int vMax;

	public int vMaxYSea;

	public int ySort;

	public int ysai;

	public int xsai;

	public int frame;

	public int frameOneStep = -1;

	public int toX;

	public int toY;

	public int toXNew;

	public int toYNew;

	public int weapon_frame;

	public int xStand;

	public int yStand;

	public int xAnchor;

	public int yAnchor;

	public int xSeaAnchor;

	public int ySeaAnchor;

	public int nFrame;

	public int wAvatar;

	public int hAvatar;

	public int hIconFocus;

	public int vXEffAva;

	public int xUpBoat;

	public int yUpBoat;

	public int wNameFocus = 60;

	public int wNameNPC;

	public int timeBeginUpdateMove;

	public int tickSoundMove;

	public int xpArena;

	public int timeItemDropEvent;

	public int checkWW;

	public int killWW;

	public int deadWW;

	public int dyShadow;

	public int tickEffHead;

	public int indexBodySkill;

	public int tickAfterSkill = 50;

	public int xDie;

	public int yDie;

	public int dyDie;

	public int vxDie;

	public int vyDie;

	public int Hp;

	public int HpEff;

	public int Mp;

	public int maxHp;

	public int maxMp;

	public int Lv;

	public int pointPk;

	public int percentLv;

	public int wanted;

	public int PointPvP;

	public int LvThongThao;

	public int percentThongThao;

	public int rankWanted = -1;

	public int Action;

	public int Dir;

	public int type_left_right;

	public int DirSpec;

	public int timeFreeMove;

	public int timeRevice;

	public int timeBeFire;

	public int tickMapTrain;

	public int timeFreeFire;

	public int timeEffRemoveChar;

	public int timeDragon;

	public int timeFullSet;

	public int timeOutScreen;

	public int f;

	public int dhCharPopup = 30;

	public int timeInviMove;

	public int timeSafe;

	public long timeBeginSafe;

	public long timeBeginItemDropEvent;

	public int timeRemove = 60;

	public int timeLeft = -1;

	public sbyte state;

	public MainSkill Skilldefault;

	public MainObject objMainFocus;

	public BigBossLittleGraden bossLittle;

	public static mImage imgShadow;

	public static mImage imgShadow2;

	public static mImage imgShadow3;

	public MyHashTable hashEquip = new MyHashTable();

	public PopupChat chat;

	public MainSkill skillMonDefault;

	public Start_Skill skillCurrent;

	public Plash plashNow;

	public sbyte downSpeedWater;

	public sbyte maxEffShip;

	public sbyte indexShip;

	public sbyte stepLeg;

	public sbyte colorXPArena;

	public sbyte levelPerfect;

	public sbyte typeShadow = -1;

	public sbyte countKick;

	public sbyte countAva;

	public sbyte lechYHead;

	public sbyte lvHeart;

	public sbyte clazz;

	public sbyte typeEffStand;

	public sbyte typeObject;

	public sbyte typeSpecMonSter;

	public sbyte colorName;

	public sbyte typePK = -1;

	public sbyte typePirate = -1;

	public sbyte DirNew;

	public sbyte typeQuest;

	public sbyte typeSpecCharList;

	public sbyte typePlayer;

	public sbyte indexTeam;

	public sbyte indexPaintView;

	public sbyte typeActionBoat;

	public sbyte isHumanMonster;

	public sbyte typeIconNPC;

	public sbyte isDonotShowHat;

	public sbyte typeBossMonster;

	public sbyte typePet;

	public sbyte typeColor7;

	public sbyte typeNpcEvent;

	public sbyte isDonotShowWeaponF;

	public sbyte indexEff_1;

	public sbyte isPokemon;

	public sbyte dxEye;

	public sbyte dyEye;

	public sbyte indexEye;

	public sbyte demDonotPaint;

	public sbyte indexFullSet;

	public sbyte thanhtichPvP = -1;

	public sbyte thanhtichLv = -1;

	public sbyte typeEfffashion = -1;

	public long timeLoadInfo;

	public long timeDie;

	public bool isDirMove;

	public bool isTanHinh;

	public bool isPaintWeapon = true;

	public bool isPaintSpec;

	public bool isPaintLeg = true;

	public bool isPlayStand;

	public bool isPet;

	public bool isFlyDie;

	public bool isDragon;

	public bool isDie;

	public bool isRemove;

	public bool isStop;

	public bool isInfo;

	public bool isLoadTemplate;

	public bool isParty;

	public bool isSend;

	public bool isMoveNor;

	public short[] posTransRoad;

	public short[] mSkillMapTrain;

	public mVector vecSkillFires = new mVector("Mainobject.vecSkillFires");

	public mVector vecBuffCur = new mVector("Mainobject.vecEffBuff");

	public mVector vecBuffCurNew = new mVector("Mainobject.vecEffBuffNew");

	public mVector vecEffspec = new mVector("Mainobject.vecEffspec");

	public mVector vecTrade = new mVector("Mainobject.vecTrade");

	public mVector vecEffFashion = new mVector("Mainobject.veceffFas");

	public static DataHairServer[] dataEffHair;

	public static DataHairServer[] dataInfoHair;

	public sbyte isTrade;

	public int gem;

	public int vnd;

	public long beli;

	public int bua;

	public int diemNap;

	public static int[][] mPosMapTrain = new int[3][]
	{
		new int[2] { 5, 10 },
		new int[2] { 18, 9 },
		new int[2] { 24, 12 }
	};

	public MainClan clan;

	public MainSudo sudo;

	public Boat boatSea;

	public const int DIR_UP = 1;

	public const int DIR_DOWN = 3;

	public const int DIR_LEFT = 0;

	public const int DIR_RIGHT = 2;

	public const int AC_STAND = 0;

	public const int AC_MOVE = 1;

	public const int AC_FIRE = 2;

	public const int AC_AVA = 3;

	public const int AC_DIE = 4;

	public const int AC_UP_BOAT = 5;

	public const sbyte CAT_HAMMER_DIAL = -8;

	public const sbyte CAT_PLAYER = 0;

	public const sbyte CAT_MONSTER = 1;

	public const sbyte CAT_NPC = 2;

	public const sbyte CAT_ITEM = 3;

	public const sbyte CAT_POTION = 4;

	public const sbyte CAT_POTION_QUEST = 5;

	public const sbyte CAT_MONEY = 6;

	public const sbyte CAT_POTION_MATERIAL = 7;

	public const sbyte CAT_POTION_CLAN = 8;

	public const sbyte CAT_EFF_ITEM = 9;

	public const sbyte CAT_PET = 10;

	public const sbyte CAT_POTION_EVENT = 11;

	public const sbyte CAT_DATA_ATTRI_KICH_AN = 96;

	public const sbyte CAT_DATA_PLASH = 97;

	public const sbyte CAT_DATA_ITEM_MAP = 98;

	public const sbyte CAT_XP = 99;

	public const sbyte CAT_IMAGE_OTHER = 100;

	public const sbyte CAT_ITEM_BOAT = 102;

	public const sbyte CAT_ITEM_HAIR = 103;

	public const sbyte CAT_ITEM_SKILL = 104;

	public const sbyte CAT_ITEM_FASHION = 105;

	public const sbyte CAT_ICON_CLAN = 107;

	public const sbyte CAT_ITEM_HEAD = 108;

	public const sbyte CAT_ITEM_WANTED = 109;

	public const sbyte CAT_ITEM_PET = 110;

	public const sbyte PLAYER_NORMAL = 0;

	public const sbyte PLAYER_SUPPORT = 1;

	public const sbyte PLAYER_SHIPER = 2;

	public const sbyte PLAYER_NPC_EVENT = 3;

	public const sbyte PK_DOSAT = 0;

	public const sbyte PK_SATNHAN = 1;

	public const sbyte PK_DAUTRUONG = 2;

	public const sbyte PK_CHIENDAU = 3;

	public const sbyte PK_COLOR = 4;

	public const sbyte CLASS_ALL = 0;

	public const sbyte CLASS_LUFFY = 1;

	public const sbyte CLASS_ZORO = 2;

	public const sbyte CLASS_SANJI = 3;

	public const sbyte CLASS_NAMI = 4;

	public const sbyte CLASS_USSOP = 5;

	public const sbyte CLASS_MONSTER = 6;

	public const sbyte WEAR_WEAPON = 0;

	public const sbyte WEAR_HAT = 1;

	public const sbyte WEAR_DAY = 2;

	public const sbyte WEAR_BODY = 3;

	public const sbyte WEAR_RING = 4;

	public const sbyte WEAR_LEG = 5;

	public const sbyte WEAR_HEAD = 6;

	public const sbyte WEAR_HAIR = 7;

	public const sbyte ONE_PART_HAIR = 5;

	public const sbyte ONE_PART_HEAD = 0;

	public const sbyte ACTIONBOAT_NORMAL = 0;

	public const sbyte ACTIONBOAT_UP = 1;

	public const sbyte ACTIONBOAT_DOWN = 2;

	public const sbyte ACTIONBOAT_UP_2 = 3;

	public const sbyte ACTIONBOAT_DOWN_2 = 4;

	public const sbyte PIRATE_NORMAL = -1;

	public const sbyte PIRATE_GOLD = 0;

	public const sbyte PIRATE_WHITE = 1;

	public const sbyte PIRATE_BLACK = 2;

	public const sbyte BOSS_SUPER_BOSS = 1;

	public mVector vecDataEff = new mVector();

	private int lastTick;

	private int framepaint;

	public int left_right_Sea;

	public int tickJoinSea;

	public long timeBegin;

	private int tickEffFashion;

	public const sbyte EFASHION_VALENTINE = 1;

	public const sbyte EFASHION_WB_WOMEN = 2;

	public const sbyte EFASHION_WB_MEN = 3;

	public const sbyte EFASHION_DRAGON = 4;

	public const sbyte EFASHION_RAU_DEN = 5;

	public static sbyte testEFF = 0;

	public int vDy;

	public static int[][][] CharInfo = new int[62][][]
	{
		new int[7][]
		{
			new int[3] { 0, -5, -42 },
			new int[3] { 0, -11, -18 },
			new int[3] { 0, -8, -30 },
			new int[3] { 0, -15, -39 },
			new int[3] { 0, -9, -44 },
			new int[3] { 0, -8, -44 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -5, -41 },
			new int[3] { 0, -11, -18 },
			new int[3] { 0, -8, -29 },
			new int[3] { 0, -15, -38 },
			new int[3] { 0, -9, -43 },
			new int[3] { 0, -8, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -11, -40 },
			new int[3] { 1, -18, -18 },
			new int[3] { 1, -11, -31 },
			new int[3] { 1, -18, -42 },
			new int[3] { 0, -15, -42 },
			new int[3] { 0, -14, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -11, -41 },
			new int[3] { 2, -6, -18 },
			new int[3] { 2, -7, -30 },
			new int[3] { 2, -14, -43 },
			new int[3] { 0, -15, -43 },
			new int[3] { 0, -14, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -11, -43 },
			new int[3] { 3, -6, -22 },
			new int[3] { 3, -17, -37 },
			new int[3] { 3, -17, -45 },
			new int[3] { 0, -15, -45 },
			new int[3] { 0, -14, -45 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -11, -41 },
			new int[3] { 4, -14, -20 },
			new int[3] { 3, -17, -35 },
			new int[3] { 4, -17, -43 },
			new int[3] { 0, -15, -43 },
			new int[3] { 0, -14, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -11, -42 },
			new int[3] { 5, -5, -19 },
			new int[3] { 2, -7, -31 },
			new int[3] { 2, -14, -44 },
			new int[3] { 0, -15, -44 },
			new int[3] { 0, -14, -44 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -11, -44 },
			new int[3] { 6, -9, -23 },
			new int[3] { 4, -13, -35 },
			new int[3] { 5, -17, -46 },
			new int[3] { 0, -15, -46 },
			new int[3] { 0, -14, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -7, -40 },
			new int[3] { 7, -12, -17 },
			new int[3] { 5, -15, -29 },
			new int[3] { 6, -22, -47 },
			new int[3] { 0, -11, -42 },
			new int[3] { 0, -10, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -7, -39 },
			new int[3] { 7, -12, -17 },
			new int[3] { 5, -15, -28 },
			new int[3] { 6, -22, -46 },
			new int[3] { 0, -11, -41 },
			new int[3] { 0, -10, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 3, -6, -39 },
			new int[3] { 7, -12, -17 },
			new int[3] { 5, -15, -28 },
			new int[3] { 6, -22, -46 },
			new int[3] { 0, -10, -41 },
			new int[3] { 0, -9, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -7, -42 },
			new int[3] { 6, -9, -18 },
			new int[3] { 5, -15, -31 },
			new int[3] { 7, -22, -43 },
			new int[3] { 0, -11, -44 },
			new int[3] { 0, -10, -44 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -7, -41 },
			new int[3] { 5, -4, -16 },
			new int[3] { 5, -15, -30 },
			new int[3] { 8, -22, -42 },
			new int[3] { 0, -11, -43 },
			new int[3] { 0, -10, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -10, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 2, -6, -30 },
			new int[3] { 9, -18, -32 },
			new int[3] { 0, -14, -43 },
			new int[3] { 0, -13, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -10, -42 },
			new int[3] { 6, -8, -18 },
			new int[3] { 2, -6, -31 },
			new int[3] { 9, -18, -33 },
			new int[3] { 0, -14, -44 },
			new int[3] { 0, -13, -44 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -13, -39 },
			new int[3] { 9, -21, -18 },
			new int[3] { 6, -19, -28 },
			new int[3] { 10, -1, -31 },
			new int[3] { 0, -17, -41 },
			new int[3] { 0, -16, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -13, -39 },
			new int[3] { 14, -9, -18 },
			new int[3] { 6, -19, -28 },
			new int[3] { 10, -1, -31 },
			new int[3] { 0, -17, -41 },
			new int[3] { 0, -16, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 4, -13, -40 },
			new int[3] { 9, -21, -18 },
			new int[3] { 7, -19, -29 },
			new int[3] { 11, 5, -17 },
			new int[3] { 1, -17, -42 },
			new int[3] { 1, -16, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 4, -13, -40 },
			new int[3] { 14, -9, -18 },
			new int[3] { 7, -19, -29 },
			new int[3] { 11, 5, -17 },
			new int[3] { 1, -17, -42 },
			new int[3] { 1, -16, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -10, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 8, -2, -46 },
			new int[3] { 12, -9, -55 },
			new int[3] { 0, -14, -43 },
			new int[3] { 0, -13, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -10, -41 },
			new int[3] { 6, -8, -18 },
			new int[3] { 8, -2, -46 },
			new int[3] { 12, -9, -55 },
			new int[3] { 0, -14, -43 },
			new int[3] { 0, -13, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -10, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 9, -2, -48 },
			new int[3] { 13, -4, -56 },
			new int[3] { 0, -14, -43 },
			new int[3] { 0, -13, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -10, -41 },
			new int[3] { 6, -8, -18 },
			new int[3] { 9, -2, -48 },
			new int[3] { 13, -4, -56 },
			new int[3] { 0, -14, -43 },
			new int[3] { 0, -13, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -12, -39 },
			new int[3] { 8, -17, -18 },
			new int[3] { 10, -4, -32 },
			new int[3] { 14, 4, -50 },
			new int[3] { 0, -16, -41 },
			new int[3] { 0, -15, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -12, -39 },
			new int[3] { 6, -8, -18 },
			new int[3] { 10, -4, -32 },
			new int[3] { 14, 4, -50 },
			new int[3] { 0, -16, -41 },
			new int[3] { 0, -15, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -10, -40 },
			new int[3] { 9, -21, -18 },
			new int[3] { 11, -18, -29 },
			new int[3] { 15, 5, -17 },
			new int[3] { 0, -14, -42 },
			new int[3] { 0, -13, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -10, -40 },
			new int[3] { 14, -9, -18 },
			new int[3] { 11, -18, -29 },
			new int[3] { 15, 5, -17 },
			new int[3] { 0, -14, -42 },
			new int[3] { 0, -13, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -5, -44 },
			new int[3] { 10, -21, -27 },
			new int[3] { 12, -9, -32 },
			new int[3] { 16, -16, -41 },
			new int[3] { 0, -9, -46 },
			new int[3] { 0, -8, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -7, -44 },
			new int[3] { 11, -23, -29 },
			new int[3] { 13, -12, -32 },
			new int[3] { 17, -17, -41 },
			new int[3] { 0, -11, -46 },
			new int[3] { 0, -10, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -7, -44 },
			new int[3] { 12, -28, -25 },
			new int[3] { 13, -12, -32 },
			new int[3] { 17, -17, -41 },
			new int[3] { 0, -11, -46 },
			new int[3] { 0, -10, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -7, -44 },
			new int[3] { 13, -17, -37 },
			new int[3] { 13, -12, -32 },
			new int[3] { 17, -17, -41 },
			new int[3] { 0, -11, -46 },
			new int[3] { 0, -10, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -7, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 14, -16, -29 },
			new int[3] { 18, -25, -42 },
			new int[3] { 0, -11, -43 },
			new int[3] { 0, -10, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -7, -41 },
			new int[3] { 14, -9, -17 },
			new int[3] { 14, -16, -29 },
			new int[3] { 18, -25, -42 },
			new int[3] { 0, -11, -43 },
			new int[3] { 0, -10, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -4, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 15, -11, -34 },
			new int[3] { 19, -16, -47 },
			new int[3] { 0, -8, -43 },
			new int[3] { 0, -7, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -4, -41 },
			new int[3] { 14, -9, -17 },
			new int[3] { 15, -11, -34 },
			new int[3] { 19, -16, -47 },
			new int[3] { 0, -8, -43 },
			new int[3] { 0, -7, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -7, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 16, -19, -30 },
			new int[3] { 20, -28, -43 },
			new int[3] { 0, -11, -43 },
			new int[3] { 0, -10, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -7, -41 },
			new int[3] { 14, -9, -17 },
			new int[3] { 16, -19, -30 },
			new int[3] { 20, -28, -43 },
			new int[3] { 0, -11, -43 },
			new int[3] { 0, -10, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -4, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 17, -15, -34 },
			new int[3] { 21, -20, -48 },
			new int[3] { 0, -8, -43 },
			new int[3] { 0, -7, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -4, -41 },
			new int[3] { 14, -9, -17 },
			new int[3] { 17, -15, -34 },
			new int[3] { 21, -20, -48 },
			new int[3] { 0, -8, -43 },
			new int[3] { 0, -7, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -7, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 18, -13, -29 },
			new int[3] { 22, -26, -31 },
			new int[3] { 0, -11, -43 },
			new int[3] { 0, -10, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -7, -41 },
			new int[3] { 14, -9, -17 },
			new int[3] { 18, -13, -29 },
			new int[3] { 22, -26, -31 },
			new int[3] { 0, -11, -43 },
			new int[3] { 0, -10, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -2, -41 },
			new int[3] { 9, -19, -18 },
			new int[3] { 19, -9, -29 },
			new int[3] { 23, -19, -39 },
			new int[3] { 0, -6, -43 },
			new int[3] { 0, -5, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -2, -41 },
			new int[3] { 14, -8, -17 },
			new int[3] { 19, -9, -29 },
			new int[3] { 23, -19, -39 },
			new int[3] { 0, -6, -43 },
			new int[3] { 0, -5, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -13, -39 },
			new int[3] { 1, -18, -17 },
			new int[3] { 10, -5, -32 },
			new int[3] { 14, 3, -50 },
			new int[3] { 0, -17, -41 },
			new int[3] { 0, -16, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -13, -40 },
			new int[3] { 2, -6, -18 },
			new int[3] { 10, -5, -33 },
			new int[3] { 14, 3, -51 },
			new int[3] { 0, -17, -42 },
			new int[3] { 0, -16, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -13, -42 },
			new int[3] { 3, -6, -22 },
			new int[3] { 10, -5, -35 },
			new int[3] { 14, 3, -53 },
			new int[3] { 0, -17, -44 },
			new int[3] { 0, -16, -44 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -13, -40 },
			new int[3] { 4, -14, -19 },
			new int[3] { 10, -5, -33 },
			new int[3] { 14, 3, -51 },
			new int[3] { 0, -17, -42 },
			new int[3] { 0, -16, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -13, -41 },
			new int[3] { 5, -5, -19 },
			new int[3] { 10, -5, -34 },
			new int[3] { 14, 3, -52 },
			new int[3] { 0, -17, -43 },
			new int[3] { 0, -16, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -13, -44 },
			new int[3] { 6, -9, -23 },
			new int[3] { 10, -5, -37 },
			new int[3] { 14, 3, -55 },
			new int[3] { 0, -17, -46 },
			new int[3] { 0, -16, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 3, -3, -41 },
			new int[3] { 8, -17, -18 },
			new int[3] { 17, -14, -34 },
			new int[3] { 21, -19, -48 },
			new int[3] { 0, -7, -43 },
			new int[3] { 0, -6, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 3, -4, -41 },
			new int[3] { 14, -9, -17 },
			new int[3] { 17, -15, -34 },
			new int[3] { 21, -20, -48 },
			new int[3] { 0, -8, -43 },
			new int[3] { 0, -7, -43 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 3, -4, -42 },
			new int[3] { 5, -5, -19 },
			new int[3] { 17, -15, -35 },
			new int[3] { 21, -20, -49 },
			new int[3] { 0, -8, -44 },
			new int[3] { 0, -7, -44 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -13, -39 },
			new int[3] { 1, -18, -17 },
			new int[3] { 6, -19, -28 },
			new int[3] { 10, -1, -31 },
			new int[3] { 0, -17, -41 },
			new int[3] { 0, -16, -41 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 4, -13, -40 },
			new int[3] { 1, -18, -17 },
			new int[3] { 7, -19, -29 },
			new int[3] { 11, 5, -17 },
			new int[3] { 1, -17, -42 },
			new int[3] { 1, -16, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -10, -40 },
			new int[3] { 1, -18, -17 },
			new int[3] { 11, -18, -29 },
			new int[3] { 15, 5, -17 },
			new int[3] { 0, -14, -42 },
			new int[3] { 0, -13, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 0, -17, -44 },
			new int[3] { 11, -23, -29 },
			new int[3] { 4, -19, -35 },
			new int[3] { 5, -23, -46 },
			new int[3] { 0, -21, -46 },
			new int[3] { 0, -20, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -15, -44 },
			new int[3] { 12, -28, -25 },
			new int[3] { 4, -18, -34 },
			new int[3] { 5, -22, -45 },
			new int[3] { 0, -19, -46 },
			new int[3] { 0, -18, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 2, -20, -43 },
			new int[3] { 12, -28, -25 },
			new int[3] { 6, -26, -32 },
			new int[3] { 10, -8, -35 },
			new int[3] { 0, -24, -45 },
			new int[3] { 0, -23, -45 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 4, -19, -44 },
			new int[3] { 12, -28, -25 },
			new int[3] { 7, -25, -33 },
			new int[3] { 11, -1, -21 },
			new int[3] { 1, -23, -46 },
			new int[3] { 1, -22, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 4, -19, -44 },
			new int[3] { 11, -23, -29 },
			new int[3] { 7, -25, -33 },
			new int[3] { 11, -1, -21 },
			new int[3] { 1, -23, -46 },
			new int[3] { 1, -22, -46 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -11, -40 },
			new int[3] { 14, -9, -18 },
			new int[3] { 1, -11, -31 },
			new int[3] { 1, -18, -42 },
			new int[3] { 0, -15, -42 },
			new int[3] { 0, -14, -42 },
			new int[3] { 0, -29, -57 }
		},
		new int[7][]
		{
			new int[3] { 1, -8, -42 },
			new int[3] { 0, -11, -18 },
			new int[3] { 5, -16, -31 },
			new int[3] { 6, -23, -49 },
			new int[3] { 0, -12, -44 },
			new int[3] { 0, -11, -44 },
			new int[3] { 0, -29, -57 }
		}
	};

	public int[] feStand = new int[13]
	{
		0, 0, 0, 0, 1, 1, 1, 1, 1, 0,
		0, 0, 0
	};

	public int[] feRun = new int[18]
	{
		2, 2, 2, 3, 3, 3, 4, 4, 4, 5,
		5, 5, 6, 6, 6, 7, 7, 7
	};

	public int[] feAva = new int[6] { 8, 8, 10, 10, 10, 10 };

	public int[][] fetest = new int[3][]
	{
		new int[73]
		{
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 6, 6, 6, 6, 6, 6, 6, 1,
			1, 1, 1, 0, 0, 0, 0, 7, 7, 7,
			7, 7, 7, 7, 3, 3, 3, 3, 3, 3,
			3, 4, 4, 4, 4, 4, 4, 4, 6, 6,
			6, 6, 6, 6, 6, 1, 1, 1, 1, 0,
			0, 0, 0
		},
		new int[38]
		{
			0, 0, 27, 27, 27, 27, 27, 27, 28, 28,
			28, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 29, 29, 29, 1, 1, 1, 1,
			0, 0, 0, 0, 1, 1, 1, 1
		},
		new int[26]
		{
			0, 0, 14, 14, 14, 13, 13, 13, 39, 39,
			39, 41, 41, 41, 39, 39, 39, 41, 41, 41,
			39, 39, 39, 41, 41, 41
		}
	};

	public static sbyte[][] fLechWeaponBigBody = new sbyte[4][]
	{
		new sbyte[124]
		{
			0, -3, 0, -3, 0, -5, 0, -5, 0, -5,
			0, -5, 0, -5, 0, -5, 0, -5, 0, -5,
			0, -5, 0, -5, 0, -5, 3, -6, 3, -6,
			0, 0, 0, 0, 0, 0, 0, 0, 0, -11,
			0, -11, -1, -9, -1, -9, -2, -8, -2, -8,
			0, 0, 0, 0, 0, -5, 0, -5, 0, -5,
			0, -5, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, -2, -8, -2, -8,
			-2, -8, -2, -8, -2, -8, -2, -8, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, -5, 0, -5, 0, 0, 0, 0, 0, 0,
			0, -5, 0, -5
		},
		new sbyte[124]
		{
			-1, -2, -1, -2, -3, -5, -2, -10, -2, -5,
			-2, -6, 0, -9, 0, -9, -6, -5, -6, -5,
			-6, -5, -5, -5, -5, -5, -2, -4, -2, -4,
			-2, -7, -2, -7, 0, 0, 0, 0, 0, -9,
			0, -9, 0, 0, 0, 0, -2, -5, -2, -5,
			-2, -7, -2, -7, -2, -5, -2, -5, -2, -5,
			-2, -5, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, -2, -5, -2, -5,
			-2, -5, -2, -5, -2, -5, -2, -5, 0, 0,
			0, 0, 0, 0, -2, -7, 0, 0, -2, -7,
			0, -9, 0, -9, 0, 0, 0, 0, 0, 0,
			-2, -4, -5, -5
		},
		new sbyte[124]
		{
			0, -9, 0, -9, 0, -7, 0, -7, 0, -7,
			0, -7, 0, -7, 0, -9, 0, -6, 0, -6,
			0, -6, 0, -8, 0, -7, -5, -11, -5, -11,
			0, 0, 0, 0, 0, 0, 0, 0, 0, -7,
			0, -7, 0, -7, 0, -7, 0, -6, 0, -6,
			0, 0, 0, 0, -2, -7, -2, -7, -2, -7,
			-2, -7, -3, -5, -3, -5, -1, -5, -1, -5,
			-3, -6, -3, -6, -3, -6, -3, -6, -2, -6,
			-2, -6, 0, -5, 0, -5, 0, -6, 0, -6,
			0, -6, 0, -6, 0, -6, 0, -6, -3, -6,
			-3, -6, -3, -6, 0, 0, 0, 0, 0, 0,
			0, -6, 0, -6, 0, 0, 0, 0, 0, 0,
			0, 9, 0, 6
		},
		new sbyte[124]
		{
			2, -6, 2, -6, -4, -4, -4, -4, -1, -5,
			-1, -5, -4, -4, -3, -6, -4, -6, -4, -6,
			-4, -6, -4, -6, -4, -6, -3, -6, -3, -6,
			-4, -6, -4, -6, 0, 0, 0, 0, 0, -9,
			0, -9, 0, 0, 0, 0, 4, -2, 4, -2,
			0, 0, 0, 0, -2, -6, -2, -6, -2, -6,
			-2, -6, -2, -6, -2, -6, -1, -5, -1, -5,
			-1, -6, -1, -6, -2, -6, -2, -6, 0, -5,
			0, -5, -1, -5, -1, -5, 4, -2, 4, -2,
			4, -2, 4, -2, 4, -2, 4, -2, -2, -6,
			-2, -6, -2, -6, -4, -6, 0, 0, 0, 0,
			-3, -6, -3, -6, -4, -6, 0, 0, 0, 0,
			-4, -4, -4, -6
		}
	};

	public static FrameImage[] mfraImgFullSet = new FrameImage[10];

	public int nFrameEffHair;

	public int ntickFrameOneStep;

	public int nFrameEffBody;

	public int indexPlayMBody;

	public int tickPlayframeBody;

	public int nFrameEffHead;

	public int tickPlayframeWeapon;

	public int indexPlayWeapon;

	public int framePlayHead;

	public int nFrameEffLeg;

	public int nFrameEffWeapon;

	private FrameImage fraEffBody;

	private MainImage imgEffHair;

	private MainImage imgEffHead;

	private MainImage imgEffPlayBody;

	private MainImage imgEffPlayLeg;

	private MainImage imgEffPlayWeapon;

	private int[][] mPlayframeBody = new int[5][]
	{
		new int[16]
		{
			0, 0, 1, 1, 2, 2, 0, 0, 1, 1,
			2, 2, 1, 1, 0, 0
		},
		new int[18]
		{
			0, 0, 1, 1, 2, 2, 1, 1, 0, 0,
			1, 1, 2, 2, 1, 1, 0, 0
		},
		new int[30]
		{
			0, 0, 1, 1, 2, 2, 2, 2, 1, 1,
			1, 1, 2, 2, 2, 2, 1, 1, 2, 2,
			1, 1, 2, 2, 1, 1, 0, 0, 0, 0
		},
		new int[18]
		{
			0, 0, 1, 1, 2, 2, 1, 1, 0, 0,
			1, 1, 2, 2, 1, 1, 0, 0
		},
		new int[12]
		{
			0, 0, 1, 1, 2, 2, 1, 1, 0, 0,
			0, 0
		}
	};

	public short idparP = -1;

	public short FramePP;

	public short fPP;

	public static sbyte[][] AR_FRAME_PP = new sbyte[3][]
	{
		new sbyte[0],
		new sbyte[0],
		new sbyte[0]
	};

	private int xEffBody;

	private int yEffBody;

	private int frameEffBody;

	private int typeActionEffBody;

	private int framePlayEB;

	public static sbyte EBODY_RANDOM_NORMAL = 0;

	public static short[] idEffBody = new short[4] { 798, 799, 801, 802 };

	public short idHairbay = -1;

	public short idBodybay = -1;

	public short idLegBay = -1;

	public short idWeaponBay = -1;

	public static short[] idPlayFrameHead = new short[1] { 815 };

	public int[] mSortPaint = new int[7] { 1, 2, 0, 5, 4, 3, 6 };

	public int[] mSortPaintRight = new int[7] { 1, 2, 3, 6, 0, 5, 4 };

	public static short[] idWeaponF = new short[4] { 803, 804, 805, 806 };

	public static int[] mSortPaintHead = new int[3] { 0, 5, 4 };

	public static short[] ListLechHEAD = new short[15]
	{
		719, 748, 751, 756, 798, 799, 801, 802, 849, 851,
		894, 896, 950, 963, 972
	};

	public static short[] ListKoLechHEAD = new short[2] { 893, 889 };

	private bool isKoLechHead;

	private bool isBuffDevil;

	private const sbyte MOVE_LEFT = 0;

	private const sbyte MOVE_RIGHT = 1;

	public bool isTestchoi;

	public int[] mActionShow;

	private int demFire;

	private int tickfire;

	public int endEye;

	public int eye = 2;

	public int timeEye;

	public sbyte stepUpboat;

	public sbyte tickTypeActionBoat;

	public int speedHpEff;

	public int tickHpEff;

	public static MyHashTable ALL_DATA_SKILL_EFF = new MyHashTable();

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
		{
			mVector listQuestNPC = getListQuestNPC();
			mVector mVector2 = new mVector();
			for (int i = 0; i < listQuestNPC.size(); i++)
			{
				MainQuest mainQuest = (MainQuest)listQuestNPC.elementAt(i);
				if (mainQuest.statusQuest == 2)
				{
					GameCanvas.menuCur.isShowMenu = false;
					GameCanvas.menuCur.runText = null;
					mainQuest.beginQuest(ID);
					return;
				}
				iCommand iCommand2 = new iCommand(mainQuest.name + ((mainQuest.typeMainSub != 0) ? string.Empty : mainQuest.getMainSub()), 1, mainQuest.ID, this);
				if (mainQuest.idNPC == ID)
				{
					iCommand2.setFraCaption(AvMain.fraQuest, 1, mainQuest.statusQuest + 1, 3);
				}
				else if (mainQuest.statusQuest == 2 && mainQuest.idNPC_Sub == ID)
				{
					iCommand2.setFraCaption(AvMain.fraQuest, 1, 2, 3);
				}
				mVector2.addElement(iCommand2);
			}
			GameCanvas.menu.startAt(mVector2, 2, T.quest);
			break;
		}
		case 1:
			MainQuest.getQuest((short)subIndex)?.beginQuest(ID);
			break;
		case 2:
			GlobalService.gI().shop_NPC(ID);
			GameCanvas.menuCur.isShowMenu = false;
			break;
		case 3:
			GlobalService.gI().shop_NPC(ID);
			GameCanvas.menuCur.isShowMenu = false;
			break;
		case 4:
			GlobalService.gI().EventMoon(ID, 2);
			break;
		case 5:
			GlobalService.gI().EventMoon(ID, 2);
			break;
		}
	}

	public override void paint(mGraphics g)
	{
	}

	public override void paintHideAvatar(mGraphics g)
	{
	}

	public virtual void paintOnlyShadown(mGraphics g)
	{
		g.drawImage(AvMain.imgHinhnhan, x, y, 33);
		if (GameScreen.objFocus == null || this != GameScreen.objFocus)
		{
			return;
		}
		sbyte color = colorName;
		if (Player.vecParty.size() > 0)
		{
			for (int i = 0; i < Player.vecParty.size(); i++)
			{
				if (((InfoMemList)Player.vecParty.elementAt(i)).name.CompareTo(name) == 0)
				{
					color = 4;
					break;
				}
			}
		}
		if (Player.mSatnhan.Length != 0)
		{
			for (int j = 0; j < Player.mSatnhan.Length; j++)
			{
				if (ID == Player.mSatnhan[j])
				{
					color = 6;
				}
			}
		}
		paintName(g, color, 0);
	}

	public void paintshadowFocus(mGraphics g, int ylech)
	{
		if (GameScreen.typeViewPlayer == 0 && this == GameScreen.objFocus)
		{
			int num = 2;
			if (type_left_right == 2)
			{
				num = -2;
			}
			AvMain.fraShadowFocus.drawFrame(GameCanvas.gameTickChia4 % AvMain.fraShadowFocus.nFrame, x + num, y + ylech, 0, 3, g);
		}
	}

	public virtual void paintShadow(mGraphics g, int xpaint)
	{
		paintShadow(g, xpaint, y);
	}

	public virtual void paintShadow(mGraphics g, int xpaint, int ypaint)
	{
		int num = 2;
		if (type_left_right == 2)
		{
			num = -2;
		}
		g.drawImage(imgShadow, xpaint + num, ypaint, 3);
	}

	public virtual void paintShadowMonster(mGraphics g, int xpaint, int dyS, int type)
	{
		switch (type)
		{
		case 0:
			g.drawImage(imgShadow, xpaint, y + dyS, 3);
			break;
		case 1:
			g.drawImage(AvMain.imgShadowSmall, xpaint, y + dyS, 3);
			break;
		case 2:
			if (imgShadow2 == null)
			{
				imgShadow2 = LoadImageStatic.LoadNewInterface("/shadow2.png");
			}
			g.drawImage(imgShadow2, xpaint, y + dyS, 3);
			break;
		case 3:
			try
			{
				if (imgShadow3 == null)
				{
					imgShadow3 = LoadImageStatic.LoadNewInterface("/shadow3.png");
				}
				g.drawImage(imgShadow3, xpaint, y + dyS, 3);
				break;
			}
			catch (Exception)
			{
				break;
			}
		case 4:
			g.drawRegion(imgShadow, 0, 0, 18, 10, 0, xpaint - 9, y + dyS, 3);
			g.drawRegion(imgShadow, 0, 0, 18, 10, 2, xpaint + 9, y + dyS, 3);
			break;
		}
	}

	public void paintWhenBoatOther(mGraphics g, int x, int y)
	{
		paintChar(g, x, y);
	}

	public void paintCharAllPart(mGraphics g, int typeSha)
	{
		paintshadowFocus(g, 0);
		int num = y - dy;
		if (checkBoatSea() && boatSea != null && boatSea.ID == ID)
		{
			boatSea.paintFrist(g);
			if (dy == 0)
			{
				num = y - dySea / 10;
			}
			boatSea.paintBuom(g);
		}
		else if (!isTanHinh)
		{
			if (typeObject == 1)
			{
				paintShadowMonster(g, x - dx, dyShadow, typeSha);
			}
			else
			{
				paintShadow(g, x - dx);
			}
		}
		if (!isTanHinh)
		{
			paintEffFashion(g, -1);
			paintBuffFirst(g, x - dx, num);
			paintEffSpecFirst(g);
			paintBuffLastNew(g, x - dx, num);
			paintChar(g, x - dx, num);
		}
		if (checkBoatSea() && boatSea != null && boatSea.ID == ID)
		{
			boatSea.paintLastInMap(g);
		}
		if (!isTanHinh)
		{
			paintEffFashion(g, 0);
			paintBuffLast(g, x - dx, num);
			paintEffSpecLast(g);
		}
	}

	public void paintCharNoPart(mGraphics g, int typeSha)
	{
		paintshadowFocus(g, 0);
		_ = y;
		_ = dy;
		if (checkBoatSea() && boatSea != null && boatSea.ID == ID)
		{
			boatSea.paintFrist(g);
			if (dy == 0)
			{
				_ = y;
				_ = dySea / 10;
			}
			boatSea.paintBuom(g);
		}
		else if (!isTanHinh)
		{
			if (typeObject == 1)
			{
				paintShadowMonster(g, x - dx, dyShadow, typeSha);
			}
			else
			{
				paintShadow(g, x - dx);
			}
		}
		if (checkBoatSea() && boatSea != null && boatSea.ID == ID)
		{
			boatSea.paintLastInMap(g);
		}
	}

	public void paintBuffFirst(mGraphics g, int x, int y)
	{
		for (int i = 0; i < vecBuffCur.size(); i++)
		{
			MainBuff mainBuff = (MainBuff)vecBuffCur.elementAt(i);
			if (mainBuff.levelPaint == -1)
			{
				mainBuff.paint(g, x, y);
			}
		}
	}

	public void paintBuffLast(mGraphics g, int x, int y)
	{
		for (int i = 0; i < vecBuffCur.size(); i++)
		{
			MainBuff mainBuff = (MainBuff)vecBuffCur.elementAt(i);
			if (mainBuff.levelPaint != -1)
			{
				mainBuff.paint(g, x, y);
			}
			mainBuff.paintLastSpec(g, x, y, this);
		}
	}

	public void paintBuffLastNew(mGraphics g, int x, int y)
	{
		for (int i = 0; i < vecBuffCurNew.size(); i++)
		{
			MainBuff mainBuff = (MainBuff)vecBuffCurNew.elementAt(i);
			if (mainBuff.levelPaint != -1)
			{
				mainBuff.paint(g, x, y);
			}
			mainBuff.paintLastSpec(g, x, y, this);
		}
	}

	public void paintEffSpecLast(mGraphics g)
	{
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			Effect_Spec effect_Spec = (Effect_Spec)vecEffspec.elementAt(i);
			if (effect_Spec.levelPaint != -1)
			{
				effect_Spec.paint(g);
			}
		}
	}

	public void paintEffSpecFirst(mGraphics g)
	{
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			Effect_Spec effect_Spec = (Effect_Spec)vecEffspec.elementAt(i);
			if (effect_Spec.levelPaint == -1)
			{
				effect_Spec.paint(g);
			}
			effect_Spec.paintFrist(g);
		}
	}

	public virtual void paintName(mGraphics g, sbyte color, int isFocus)
	{
		if (GameScreen.getIsOffAdmin(0))
		{
			return;
		}
		if (GameScreen.isShowNameSUPER_BOSS || (GameScreen.player != null && this == GameScreen.player) || (GameScreen.objFocus != null && this == GameScreen.objFocus) || isFocus < 0)
		{
			int num = 0;
			if (Action == 4)
			{
				num = 5;
			}
			int num2 = y - dy - hOne - 18 + num - ModMenu.offsetYName;
			string text = name;
			if (GameScreen.isShowIndex)
			{
				text = name + " " + ID + " f=" + frame;
			}
			if (GameScreen.isShowNameXpArena && typeObject == 0 && isFocus >= 0)
			{
				text = xpArena + string.Empty;
			}
			int num3 = 0;
			if (GameScreen.isShowNameSetting || isFocus < 0)
			{
				int num4 = 0;
				if (clan != null && (GameCanvas.currentScreen == GameCanvas.listCharScr || LoadMap.specMap != 4))
				{
					num4 = 1;
				}
				if (rankWanted < 100 && rankWanted >= 0 && isFocus >= 0)
				{
					num4 += 2;
				}
				if (num4 > 0)
				{
					int num5 = mFont.tahoma_7b_black.getWidth(name) / 2;
					if (!checkMapChiemDao() && isFocus >= 0)
					{
						num5 = 0;
					}
					switch (num4)
					{
					case 1:
						num3 = 10;
						paintIconClan(g, x - num5, num2 + 5);
						break;
					case 2:
						num3 = -10;
						paintIconWanted(g, x + num5, num2 + 6);
						break;
					case 3:
						paintIconWanted(g, x + num5 + 10, num2 + 6);
						paintIconClan(g, x - num5 - 10, num2 + 5);
						break;
					}
				}
				if (checkMapChiemDao() || isFocus < 0)
				{
					if (isFocus >= 0 && !GameScreen.isShowNameXpArena && this != GameScreen.player)
					{
						if (Player.vecParty.size() > 0)
						{
							for (int i = 0; i < Player.vecParty.size(); i++)
							{
								if (((InfoMemList)Player.vecParty.elementAt(i)).name.CompareTo(name) == 0)
								{
									color = 4;
									break;
								}
							}
						}
						if (Player.mSatnhan.Length != 0)
						{
							for (int j = 0; j < Player.mSatnhan.Length; j++)
							{
								if (ID == Player.mSatnhan[j])
								{
									color = 6;
									break;
								}
							}
						}
					}
					if (GameScreen.isShowNameXpArena && typeObject == 0 && isFocus >= 0)
					{
						color = colorXPArena;
					}
					if (GameScreen.isShowNameWW && typeObject == 0 && isFocus >= 0 && checkWW <= 0 && typePK >= 11 && typePK <= 13)
					{
						color = 7;
						mFont.tahoma_7b_black.drawString(g, text, x + num3, num2, 2);
					}
					else if (isFocus == 1)
					{
						AvMain.FontBorderColor(g, text, x + num3, num2, 2, color, 7);
					}
					else if (typeColor7 == 1 && isFocus == 0)
					{
						AvMain.FontSevenColor(g, text, x + num3, num2, 2, -1);
					}
					else if (GameCanvas.lowGraphic)
					{
						AvMain.setTextColorName(color).drawString(g, text, x + num3, num2, 2);
					}
					else
					{
						AvMain.Font3dColor(g, text, x + num3, num2, 2, color);
					}
					if (isFocus >= 0)
					{
						paintIconPerfect(g, x + num3, num2 + 14);
					}
					if (timeLeft > 0)
					{
						AvMain.Font3dColor(g, Interface_Game.ConvertTimeToStr(timeLeft), x + num3, num2 - 15, 2, 5);
					}
				}
			}
		}
		if (isFocus >= 0)
		{
			paintIconPk_ThanhTich(g);
		}
	}

	public void paintThanhTich(mGraphics g, int yBegin, int xBegin)
	{
		int num = 0;
		if (thanhtichPvP >= 0)
		{
			if (thanhtichPvP == 0)
			{
				g.drawRegion(AvMain.mImgThanhTich[0], 0, GameCanvas.gameTickChia4 % 3 * 15, 63, 15, 0, xBegin, yBegin, 3);
				num += 14;
			}
			else if (thanhtichPvP >= 1 && thanhtichPvP <= 3)
			{
				g.drawRegion(AvMain.mImgThanhTich[0], 0, (thanhtichPvP + 2) * 15, 63, 15, 0, xBegin, yBegin, 3);
				num += 12;
			}
		}
		if (thanhtichLv >= 0)
		{
			if (thanhtichLv == 0)
			{
				g.drawRegion(AvMain.mImgThanhTich[1], 0, GameCanvas.gameTickChia4 % 3 * 15, 52, 15, 0, xBegin, yBegin - num, 3);
				num += 14;
			}
			else if (thanhtichLv >= 1 && thanhtichLv <= 3)
			{
				g.drawRegion(AvMain.mImgThanhTich[1], 0, (thanhtichLv + 2) * 15, 52, 15, 0, xBegin, yBegin - num, 3);
				num += 12;
			}
		}
		if (danhieu != null)
		{
			danhieu.paint(g, xBegin, yBegin - num - 10);
			num += 12;
		}
	}

	private bool checkMapChiemDao()
	{
		if (LoadMap.specMap != 11)
		{
			return true;
		}
		if (GameScreen.player == null || GameScreen.player == this)
		{
			return true;
		}
		if (clan == null || GameScreen.player.clan == null)
		{
			return false;
		}
		if (GameScreen.player.clan.ID == clan.ID)
		{
			return true;
		}
		return false;
	}

	private void paintIconPerfect(mGraphics g, int xp, int yp)
	{
		if (levelPerfect > 0)
		{
			sbyte b = (sbyte)(GameCanvas.gameTick % 40 / 4);
			sbyte b2 = (sbyte)(GameCanvas.gameTick / 20 % (AvMain.fraEffItem.nFrame / 2));
			for (int i = 0; i < levelPerfect; i++)
			{
				int idx = b2 * 2 + ((b == i) ? 1 : 0);
				AvMain.fraEffItem.drawFrame(idx, xp - (levelPerfect - 1) * 3 + i * 6, yp, 0, 3, g);
			}
		}
	}

	public void paintLV(mGraphics g, sbyte color)
	{
		AvMain.Font3dColor(g, T.Lv + " " + Lv, x, y - dy - hOne - 28, 2, color);
	}

	public void paintIconQuest(mGraphics g, int x)
	{
		AvMain.fraQuest.drawFrame(typeQuest, x, y - hOne - 31, 0, 3, g);
	}

	public void paintIconNPC(mGraphics g, int x)
	{
		AvMain.fraIconNpc.drawFrame(typeIconNPC, x, y - hOne - 30, 0, 3, g);
	}

	public void paintIconPk_ThanhTich(mGraphics g)
	{
		int num = 8;
		int num2 = y - hOne - dy - 25 - ModMenu.offsetYName;
		int num3 = 0;
		if (LoadMap.specMap == 11)
		{
			num2 -= 3;
		}
		if (typePirate > -1 && typePirate <= 2)
		{
			num -= 8;
		}
		if (typePK >= 0)
		{
			num = ((typePK <= 10) ? (num - 8) : (num - 9));
		}
		if (timeSafe > 0)
		{
			num -= 8;
		}
		if (!GameScreen.isShowNameSetting && clan != null)
		{
			num -= 10;
			num2 += 10;
			paintIconClan(g, x + num, num2);
			num += 16;
		}
		if (typePirate > -1 && typePirate <= 2)
		{
			AvMain.fraPirate.drawFrame(typePirate, x + num, num2, 0, 3, g);
			num += 16;
			num3 = 14;
		}
		if (typePK >= 0)
		{
			if (typePK > 10)
			{
				if (AvMain.fraPk2 == null)
				{
					AvMain.fraPk2 = LoadImageStatic.loadFraImage("/interface/iconpk2.png", 16, 16);
				}
				else
				{
					AvMain.fraPk2.drawFrame((typePK - 11) * 3 + GameCanvas.gameTick / 6 % 3, x + num, num2 - 1, 0, 3, g);
				}
				num += 18;
			}
			else
			{
				AvMain.fraPk.drawFrame(typePK * 3 + GameCanvas.gameTick / 4 % 3, x + num, num2 + 1, 0, 3, g);
				num += 16;
			}
			num3 = 14;
		}
		if (timeSafe > 0)
		{
			if (AvMain.imgSafe == null)
			{
				AvMain.imgSafe = mImage.createImage("/interface/safe.png");
			}
			else
			{
				g.drawImage(AvMain.imgSafe, x + num, num2, 3);
			}
			num3 = 14;
		}
		paintThanhTich(g, num2 - num3 - 2, x);
	}

	private void paintIconClan(mGraphics g, int x2, int y2)
	{
		MainImage iconClan = Potion.getIconClan(clan.idIcon);
		if (iconClan == null || iconClan.img == null)
		{
			return;
		}
		if (clan.borderIconClan >= 1)
		{
			AvMain.fraBorderClan2.drawFrameNew((clan.borderIconClan - 1) * AvMain.fraBorderClan2.maxNumFrame + GameCanvas.gameTick / 3 % AvMain.fraBorderClan2.maxNumFrame, x2, y2, 0, 3, g);
		}
		int num = -1;
		if (clan.chucvu == 0)
		{
			num = 10;
		}
		else if (clan.chucvu == 1)
		{
			num = 2;
		}
		iconClan.set_W_H();
		if (num != -1)
		{
			MainItem.eff_UpLv_Clan.paintUpgradeEffect(x2 - 1, y2, num, (iconClan.w > 15) ? (iconClan.w + 2) : 15, g, 0, isBorder: true);
		}
		if (iconClan.frame == -1)
		{
			iconClan.set_Frame();
		}
		if (iconClan.frame <= 1)
		{
			g.drawImage(iconClan.img, x2, y2 + 1, 3);
			return;
		}
		int num2 = ((framepaint < iconClan.frame - 1) ? 3 : 15);
		if (CRes.abs(GameCanvas.gameTick - lastTick) > num2)
		{
			framepaint++;
			if (framepaint >= iconClan.frame)
			{
				framepaint = 0;
			}
			lastTick = GameCanvas.gameTick;
		}
		g.drawRegion(iconClan.img, 0, framepaint * iconClan.w, iconClan.w, iconClan.w, 0, x2, y2 + 1, 3);
	}

	private void paintIconWanted(mGraphics g, int x, int y)
	{
		if (rankWanted == 0)
		{
			int num = GameCanvas.gameTick / 6 % 10;
			if (num > 3)
			{
				num = 0;
			}
			AvMain.fraIconWanted.drawFrame(num, x, y, 0, 3, g);
		}
		else if (rankWanted == 1)
		{
			int num2 = GameCanvas.gameTick / 6 % 10;
			if (num2 > 2)
			{
				num2 = 0;
			}
			AvMain.fraIconWanted.drawFrame(4 + num2, x, y, 0, 3, g);
		}
		else if (rankWanted == 2)
		{
			AvMain.fraIconWanted.drawFrame(7, x, y, 0, 3, g);
		}
		else if (rankWanted < 10)
		{
			AvMain.fraIconWanted.drawFrame(8, x, y, 0, 3, g);
		}
		else
		{
			AvMain.fraIconWanted.drawFrame(9, x, y, 0, 3, g);
		}
	}

	public void paintCharShow(mGraphics g, int x, int y, int Dirr, bool isNhip)
	{
		paintDataEff(g, x, y, 1);
		paintEffFullSet(g, x + 1, y, 0);
		int fr = feStand[0];
		if (isNhip)
		{
			fr = feStand[GameCanvas.gameTick % feStand.Length];
		}
		updateEye();
		paintBody(g, x, y, fr, 0, isNhip);
		paintEffFullSet_Last(g, x + 1, y + 1, 0);
		paintDataEff(g, x, y, 0);
	}

	public void paintCharSelect(mGraphics g, int x, int y, int Dirr, int fr)
	{
		updateEye();
		paintBody(g, x, y, fr, Dirr, isEye: true);
	}

	public override void update()
	{
		if (typeActionBoat != 0)
		{
			tickJoinSea++;
			if (tickJoinSea >= 250)
			{
				setNextSea();
				tickJoinSea = 0;
			}
		}
		if (timeDragon > 0)
		{
			timeDragon--;
			if (timeDragon == 0)
			{
				frameOneStep = 7;
			}
		}
		if (timeBeginUpdateMove >= 0)
		{
			timeBeginUpdateMove--;
		}
		x += vx;
		y += vy;
		updateChatPopup(dhCharPopup);
		if (timeInviMove > 0)
		{
			timeInviMove--;
			if (timeInviMove == 1)
			{
				x = toX;
				y = toY;
			}
			if (timeInviMove == 0)
			{
				isTanHinh = false;
			}
		}
		updateBuff();
		updateEye();
		updateEffSpec();
		if (checkBoatSea() && boatSea != null && boatSea.ID == ID)
		{
			boatSea.updateBoat();
			updateDySea();
			if (Action != 2 && Action != 4)
			{
				left_right_Sea = type_left_right;
			}
			if (Action != 2)
			{
				boatSea.updateXY(x, y, dySea / 10, (sbyte)left_right_Sea);
			}
			if (typeActionBoat != 0 && GameCanvas.loadmap.getTile(x, y) == 0)
			{
				setSpeed(7, 7);
				typeActionBoat = 0;
			}
		}
		setHpEff();
		setEffDie();
		if (Action == 1)
		{
			tickSoundMove++;
			if (tickSoundMove >= 20)
			{
				tickSoundMove = 0;
				if (this == GameScreen.player || CRes.random(5) == 0)
				{
					if (LoadMap.specMap == 4)
					{
						mSound.playSound(31, mSound.volumeSound);
					}
					else
					{
						mSound.playSound(30, mSound.volumeSound);
					}
				}
			}
		}
		if (GameCanvas.gameTick % 10 == 0)
		{
			updateTimeSafe();
		}
		updateEffFashion();
		updateDataEff();
		if (timeLeft >= 0 && GameCanvas.timeNow - timeBegin >= 1000)
		{
			timeBegin += 1000L;
			timeLeft--;
		}
	}

	private void updateEffFashion()
	{
		for (int i = 0; i < vecEffFashion.size(); i++)
		{
			MainEffect obj = (MainEffect)vecEffFashion.elementAt(i);
			obj.update();
			if (obj.isStop)
			{
				vecEffFashion.removeElementAt(i);
				i--;
			}
		}
		if (typeEfffashion != 1)
		{
			return;
		}
		if (Action == 0)
		{
			tickEffFashion--;
			if (tickEffFashion < 0)
			{
				tickEffFashion = 10 + CRes.random(25);
				vecEffFashion.addElement(GameScreen.createEffectEnd_ObjTo(161, (clazz == 4) ? 1 : 0, x, y, ID, typeObject, (sbyte)Dir, this));
			}
		}
		else if (Action == 1 && GameCanvas.gameTick % 2 == 0)
		{
			vecEffFashion.addElement(GameScreen.createEffectEnd_ObjTo(160, (clazz == 4) ? 1 : 0, x, y, ID, typeObject, (sbyte)Dir, this));
		}
	}

	public void paintDataEff(mGraphics g, int x, int y, int type)
	{
		for (int i = 0; i < vecDataEff.size(); i++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(i);
			if (dataSkillEff != null)
			{
				if (type == 0)
				{
					dataSkillEff.paintTopEff(g, x, y, hOne);
				}
				if (type == 1)
				{
					dataSkillEff.paintBottomEff(g, x, y, hOne);
				}
			}
		}
	}

	public void paintEffFashion(mGraphics g, int levelpaint)
	{
		for (int i = 0; i < vecEffFashion.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEffFashion.elementAt(i);
			if (mainEffect.levelPaint == levelpaint)
			{
				mainEffect.paint(g);
			}
		}
	}

	public virtual void updateTimeSafe()
	{
		if (timeSafe > 0 && (GameCanvas.timeNow - timeBeginSafe) / 1000 > timeSafe)
		{
			timeSafe = 0;
		}
	}

	public void updateLoginShow()
	{
		x += vx;
		y += vy;
		move_to_XY_Normal();
		updateActionPerLogin();
		updateDirPaint();
		if (vx == 0 && vy == 0 && Action == 1)
		{
			Action = 0;
			f = 0;
		}
		if (boatSea != null)
		{
			if (Action == 0 && CRes.random(20) == 0)
			{
				int num = CRes.random(40, 120);
				toX = x + ((xAnchor >= 0) ? (-num) : num);
				toY = y + CRes.random_Am(24, 48);
				if (toY < MotherCanvas.h - 170)
				{
					toY = MotherCanvas.h - 170;
				}
				if (toY > MotherCanvas.h - 100)
				{
					toY = MotherCanvas.h - 100;
				}
				Action = 1;
			}
		}
		else if (Action == 0 && CRes.random(40) == 0)
		{
			if (CRes.random(2) == 0)
			{
				MainSkill skill = new MainSkill(0, IdEffShow);
				plashNow = new Plash(skill, this, null, isShow: true);
				resetBeginFire();
				Action = 2;
			}
			else
			{
				int num2 = CRes.random(40, 120);
				toX = x + ((xAnchor >= 0) ? (-num2) : num2);
				toY = y + CRes.random_Am(24, 48);
				if (toY < MotherCanvas.h - 70)
				{
					toY = MotherCanvas.h - 70;
				}
				if (toY > MotherCanvas.h)
				{
					toY = MotherCanvas.h;
				}
				Action = 1;
			}
		}
		if (xAnchor < 0 && x > MotherCanvas.w + 30)
		{
			isRemove = true;
		}
		else if ((xAnchor > 0) & (x < -30))
		{
			isRemove = true;
		}
	}

	public void setEffDie()
	{
		if (Action == 4 && !isDie)
		{
			if (vxDie > 0)
			{
				vxDie += 2;
			}
			else
			{
				vxDie -= 2;
			}
			xDie += vxDie;
			yDie += vyDie;
			dyDie += 10;
			timeDie++;
			if (timeDie >= 20)
			{
				isDie = true;
				timeDie = 0L;
			}
		}
	}

	public virtual void updateDySea()
	{
		if (CRes.random(40) == 0)
		{
			if (CRes.random(2) == 0)
			{
				vySea = 4;
			}
			else
			{
				vySea = -4;
			}
		}
		if (dySea > 0 && vySea > 0)
		{
			vySea = -4;
		}
		else if (dySea < -50 && vySea < 0)
		{
			vySea = 4;
			if (Action == 1)
			{
				boatSea.addEffSea(x, y, 0, (sbyte)((type_left_right == 0) ? 2 : 0), 0);
			}
			else
			{
				boatSea.addEffSea(x, y, 1, 0, 0);
			}
		}
		dySea += vySea;
		if (Action == 1)
		{
			if (GameCanvas.gameTick % 8 == 0)
			{
				boatSea.addEffSea(x, y, -1, (sbyte)type_left_right, 0);
			}
			if (CRes.random(8) == 0)
			{
				boatSea.addEffSea(x, y, 0, (sbyte)((type_left_right == 0) ? 2 : 0), 0);
			}
			if (GameCanvas.gameTick % 6 == 0)
			{
				boatSea.addEffSea(x, y, 2, (sbyte)type_left_right, 0);
			}
		}
		else if (CRes.random(20) == 0)
		{
			boatSea.addEffSea(x, y, 1, 0, 0);
		}
		if (vy != 0)
		{
			boatSea.setLevelPaint();
		}
	}

	public void updateEffSpec()
	{
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			Effect_Spec effect_Spec = (Effect_Spec)vecEffspec.elementAt(i);
			effect_Spec.update();
			if (effect_Spec.isRemove)
			{
				vecEffspec.removeElement(effect_Spec);
				if (effect_Spec.typeEffect == 15)
				{
					SetXlechEffectBongToi();
				}
			}
		}
	}

	public void updateBuff()
	{
		for (int i = 0; i < vecBuffCur.size(); i++)
		{
			MainBuff mainBuff = (MainBuff)vecBuffCur.elementAt(i);
			mainBuff.update();
			if (mainBuff.isRemove)
			{
				if (mainBuff.isDevil)
				{
					setResetWearing();
					addEffBuff(3, 166, 0);
				}
				vecBuffCur.removeElement(mainBuff);
				i--;
			}
		}
		for (int j = 0; j < vecBuffCurNew.size(); j++)
		{
			MainBuff mainBuff2 = (MainBuff)vecBuffCurNew.elementAt(j);
			mainBuff2.update();
			if (mainBuff2.isRemove)
			{
				if (mainBuff2.isDevil)
				{
					setResetWearing();
					addEffBuff(3, 166, 0);
				}
				vecBuffCurNew.removeElement(mainBuff2);
				j--;
			}
		}
	}

	public void updateChatPopup(int dh)
	{
		if (strChatPopup != null)
		{
			addChat(strChatPopup, isStop: true);
			strChatPopup = null;
		}
		if (chat != null)
		{
			chat.updatePos(x, y - hOne - dh - ModMenu.offsetYName);
			if (chat.setOff() || (typeObject == 1 && Action == 4))
			{
				chat = null;
			}
		}
	}

	public void updateDy()
	{
		if (dy > 0)
		{
			vDy -= 2;
			dy += vDy;
		}
		if (dy < 0)
		{
			dy = -dy;
			vDy = 0;
		}
		if (dy < 3)
		{
			dy = 0;
		}
	}

	public void updatevXEffAva()
	{
		if (!returnAction() && vXEffAva != 0 && x + vXEffAva > 0 && x + vXEffAva < GameCanvas.loadmap.maxWMap)
		{
			x += vXEffAva;
			vXEffAva /= 2;
			if (vXEffAva == 0)
			{
				toX = x;
				toY = y;
			}
		}
	}

	public void setMove(int MonWater, int t)
	{
		if (typeActionBoat != 0)
		{
			return;
		}
		if ((t == 1 || t == -1) && timeFreeMove < 12)
		{
			vx = 0;
			vy = 0;
			isDirMove = !isDirMove;
			timeFreeMove++;
			if (timeFreeMove >= 10)
			{
				timeFreeMove = 25;
			}
		}
		else if (timeFreeMove > 0)
		{
			timeFreeMove--;
		}
	}

	public bool setIsInScreen(int x, int y, int wOne, int hOne)
	{
		if (x < MainScreen.cameraMain.xCam - wOne || x > MainScreen.cameraMain.xCam + MotherCanvas.w + wOne || y < MainScreen.cameraMain.yCam - hOne / 2 || y > MainScreen.cameraMain.yCam + MotherCanvas.h + hOne * 3 / 2)
		{
			return false;
		}
		return true;
	}

	public static bool isInScreen(MainObject obj)
	{
		if (obj.x < MainScreen.cameraMain.xCam - obj.wOne || obj.x > MainScreen.cameraMain.xCam + MotherCanvas.w + obj.wOne || obj.y < MainScreen.cameraMain.yCam - obj.hOne || obj.y > MainScreen.cameraMain.yCam + MotherCanvas.h + obj.hOne * 3 / 2)
		{
			return false;
		}
		return true;
	}

	public static int getDistance(int x, int y, int x2, int y2)
	{
		return getDistance(x - x2, y - y2);
	}

	public static int getDistance(int x, int y)
	{
		return CRes.sqrt(x * x + y * y);
	}

	public static MainObject get_Object(int ID, sbyte tem)
	{
		for (int num = GameScreen.vecPlayers.size() - 1; num >= 0; num--)
		{
			if (num != GameScreen.vecPlayers.size())
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(num);
				if (mainObject != null && mainObject.typeObject == tem)
				{
					short num2 = mainObject.ID;
					if (tem == 10)
					{
						num2 = mainObject.IDMainShiper;
					}
					if (num2 == ID)
					{
						if (mainObject.isRemove || mainObject.isStop)
						{
							return null;
						}
						return mainObject;
					}
				}
			}
		}
		return null;
	}

	public void addChat(string str, bool isStop)
	{
		if (chat == null)
		{
			chat = new PopupChat();
		}
		short num = GlobalService.CheckEmotion(str);
		if (num == -1)
		{
			chat.setChat(str, isStop);
		}
		else
		{
			chat.setChat(num);
		}
		chat.updatePos(x, y - hOne - 30 - ModMenu.offsetYName);
	}

	public void addChatItem(short idIconChat, sbyte cat, short num)
	{
		try
		{
			if (chat == null)
			{
				chat = new PopupChat();
			}
			chat.setChatItem(idIconChat, cat, num);
			chat.updatePos(x, y - hOne - 30 - ModMenu.offsetYName);
		}
		catch (Exception)
		{
		}
	}

	public void move_to_XY()
	{
		if (CRes.abs(x - toX) > vMax)
		{
			vy = 0;
			Action = 1;
			if (CRes.abs(x - toX) > vMax)
			{
				if (x > toX)
				{
					vx = -vMax;
					Dir = 0;
				}
				else
				{
					vx = vMax;
					Dir = 2;
				}
			}
			else
			{
				vx = toX - x;
			}
		}
		else if (CRes.abs(y - toY) > vMax)
		{
			vx = 0;
			Action = 1;
			if (CRes.abs(y - toY) > vMax)
			{
				if (y > toY)
				{
					if (checkBoatSea())
					{
						vy = -vMaxYSea;
					}
					else
					{
						vy = -vMax;
					}
					Dir = 1;
				}
				else
				{
					if (LoadMap.specMap == 4 || typeActionBoat != 0 || GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						vy = vMaxYSea;
					}
					else
					{
						vy = vMax;
					}
					Dir = 3;
				}
			}
			else
			{
				vy = toY - y;
			}
		}
		else
		{
			vx = 0;
			vy = 0;
		}
	}

	public bool returnAction()
	{
		if (isRemove || isStop || (typeObject == 0 && !isInfo))
		{
			return true;
		}
		return false;
	}

	public void paintHPFocus(mGraphics g)
	{
		if (!isTanHinh)
		{
			int num = 0;
			int num2 = y - dy - hOne - 4;
			int num3 = 40;
			int hrect = 4;
			if (levelPerfect > 0)
			{
				num2 += 4;
			}
			num = x - num3 / 2;
			num3 = 30;
			num = x - num3 / 2;
			sbyte type = 102;
			if (Lv >= 100)
			{
				type = 106;
			}
			Interface_Game.PaintHPMP(g, type, Hp, maxHp, num, num2, 0, hrect, num3, -1, isflip: false, 0, isUpdateEff: false, lvHeart);
		}
	}

	public void resetXY()
	{
		toX = x;
		toY = y;
		toXNew = x;
		toYNew = y;
		vx = 0;
		vy = 0;
	}

	public virtual void beginDie(MainObject objFire)
	{
		resetAction();
		if (isTanHinh)
		{
			isTanHinh = false;
		}
		if (Action != 4)
		{
			Action = 4;
		}
		if (!isFlyDie)
		{
			isFlyDie = true;
			isDie = false;
			timeDie = 0L;
			xDie = x;
			yDie = y;
			vyDie = -12;
			dyDie = 0;
			if (objFire == null || objFire.x < x)
			{
				vxDie = 12;
				GameScreen.addEffectEnd(82, 0, x + 10, y, 2, this);
			}
			else
			{
				vxDie = -12;
				GameScreen.addEffectEnd(82, 0, x - 10, y, 0, this);
			}
		}
	}

	public virtual void resetAction()
	{
		f = 0;
		vx = (vy = 0);
		toX = x;
		toY = y;
		if (Action != 2 && Action != 4 && Action != 3)
		{
			Action = 0;
			weapon_frame = 0;
		}
	}

	public void resetBeginFire()
	{
		vx = (vy = 0);
		toX = x;
		toY = y;
	}

	public void move_to_XY_Normal()
	{
		if (isDirMove)
		{
			if (CRes.abs(x - toX) >= vMax)
			{
				moveX();
				return;
			}
			if (CRes.abs(y - toY) >= vMax)
			{
				moveY();
				return;
			}
			vx = 0;
			vy = 0;
		}
		else if (CRes.abs(y - toY) >= vMax)
		{
			moveY();
		}
		else if (CRes.abs(x - toX) >= vMax)
		{
			moveX();
		}
		else
		{
			vx = 0;
			vy = 0;
		}
	}

	public void moveX()
	{
		vy = 0;
		Action = 1;
		if (CRes.abs(x - toX) >= vMax)
		{
			if (x >= toX)
			{
				vx = -vMax;
				Dir = 0;
			}
			else
			{
				vx = vMax;
				Dir = 2;
			}
		}
		else
		{
			vx = toX - x;
		}
	}

	public void moveY()
	{
		vx = 0;
		Action = 1;
		if (CRes.abs(y - toY) >= vMax)
		{
			if (y > toY)
			{
				if (checkBoatSea())
				{
					vy = -vMaxYSea;
				}
				else
				{
					vy = -vMax;
				}
				Dir = 1;
			}
			else
			{
				if (checkBoatSea())
				{
					vy = vMaxYSea;
				}
				else
				{
					vy = vMax;
				}
				Dir = 3;
			}
		}
		else
		{
			vy = toY - y;
		}
	}

	public void getSpeedforRun(int max)
	{
		switch (Dir)
		{
		case 1:
			vy = -max;
			vx = 0;
			break;
		case 3:
			vy = max;
			vx = 0;
			break;
		case 0:
			vy = 0;
			vx = -max;
			break;
		case 2:
			vy = 0;
			vx = max;
			break;
		}
	}

	public virtual void addSkillFire(MainSkill skill, mVector vec)
	{
	}

	public virtual void setDataBeginSkill(MainSkill skill, mVector vec)
	{
	}

	public virtual void Reveive()
	{
		isFlyDie = false;
		vecSkillFires.removeAllElements();
		vecBuffCur.removeAllElements();
		vecBuffCurNew.removeAllElements();
		vecEffspec.removeAllElements();
		setResetWearing();
		Action = 0;
		timeBeginUpdateMove = -1;
		Mp = maxMp;
		Hp = maxHp;
		GameScreen.addEffectEnd_ObjTo(83, 0, 0, 0, ID, typeObject, (sbyte)Dir, this);
		if (this == GameScreen.player)
		{
			GlobalService.gI().Obj_Move((short)x, (short)y);
		}
	}

	public virtual void paintInfoFocus(mGraphics g, int xp, int yp)
	{
		int num = 0;
		int isNum = 3;
		int num2 = 0;
		if (GameCanvas.isTaiTho)
		{
			num2 = 5;
		}
		if (typePK >= 11 && typePK <= 13)
		{
			AvMain.paintnenFocus(g, MotherCanvas.w - wNameFocus - 4 - num2, yp - 1, wNameFocus, 45);
			mFont.tahoma_7_green.drawString(g, string.Empty + killWW, MotherCanvas.w - wNameFocus / 2 - 3 - 4 - num2, yp + 32, 1);
			mFont.tahoma_7_red.drawString(g, string.Empty + deadWW, MotherCanvas.w - wNameFocus / 2 - 3 + 4 - num2, yp + 32, 0);
			mFont.tahoma_7_white.drawString(g, "-", MotherCanvas.w - wNameFocus / 2 - 3 - num2, yp + 32, 2);
		}
		else
		{
			AvMain.paintnenFocus(g, MotherCanvas.w - wNameFocus - 4 - num2, yp - 1, wNameFocus, 36);
		}
		sbyte b = 102;
		if (GameCanvas.isSmallScreen && typeObject != 2)
		{
			AvMain.Font3dWhite(g, name + " (" + Lv + ")", xp + 48, yp, 1);
			yp += 13;
		}
		else
		{
			string str = name;
			if (isNPC_Area() == 99)
			{
				str = T.Area + " " + LoadMap.getAreaPaint();
			}
			if (typeObject != 2)
			{
				sbyte color = 0;
				if (typeObject == 1)
				{
					int num3 = Lv - GameScreen.player.Lv;
					if (num3 > 0)
					{
						color = (sbyte)((num3 <= 2) ? 5 : ((num3 > 4) ? 6 : 3));
					}
				}
				isNum = 2;
				AvMain.Font3dColor(g, str, MotherCanvas.w - wNameFocus / 2 - 3 - num2, yp + 1, 2, color);
				yp += GameCanvas.hText;
				if (Lv >= 100)
				{
					g.drawRegion(Interface_Game.imgIconMPHP2, 0, 22, 10, 11, 0, xp - 14, yp + 5, 3);
					AvMain.Font3dColor(g, string.Empty + LvThongThao, xp - 1, yp - 3, 2, 1);
					b = 106;
				}
				else
				{
					AvMain.Font3dWhite(g, T.Lv + Lv, xp - 6, yp - 3, 2);
				}
			}
			else
			{
				num = 14;
				AvMain.Font3dWhite(g, str, MotherCanvas.w - wNameFocus / 2 - 3 - num2, yp + 1, 2);
				yp += GameCanvas.hText;
			}
		}
		if (isNPC_Area() == 99)
		{
			g.setColor(13350814);
			g.fillRect(xp + 8 - num - 2, yp - 1, 46, 8);
			Interface_Game.PaintHPMP(g, 102, LoadMap.numPlayerMap, LoadMap.maxnumPlayerMap, xp + 8 - num - 1, yp, 10, 6, 44, isNum, isflip: false, HpEff, isUpdateEff: false, 0);
		}
		else if (isNPC_Area() == 0 || isNPC_Area() == 2)
		{
			if (LoadMap.mStrNameMapNPC != null)
			{
				int num4 = 0;
				int hText = GameCanvas.hText;
				if (LoadMap.mStrNameMapNPC.Length >= 2)
				{
					num4 = 3;
					hText -= 3;
				}
				for (int i = 0; i < LoadMap.mStrNameMapNPC.Length; i++)
				{
					AvMain.Font3dSmall(g, LoadMap.mStrNameMapNPC[i], xp + 17, yp - num4 + (GameCanvas.hText - num4) * i, 2, 0);
				}
			}
		}
		else
		{
			g.setColor(13350814);
			g.fillRect(xp + 9 - num - 2, yp - 2, 46, 8);
			Interface_Game.PaintHPMP(g, (sbyte)((!GameCanvas.lowGraphic) ? b : 104), Hp, maxHp, xp + 9 - num - 1, yp - 1, 10, 6, 44, isNum, isflip: false, HpEff, isUpdateEff: false, lvHeart);
		}
	}

	public void paintChar(mGraphics g, int x, int y)
	{
		bool flag = false;
		int num = 1;
		if (timeFullSet >= 40 || (timeFullSet > 34 && timeFullSet % 3 != 2) || indexFullSet >= 4)
		{
			if (type_left_right == 2)
			{
				num = -1;
			}
			flag = true;
		}
		if (flag)
		{
			paintEffFullSet(g, x + num, y, type_left_right);
		}
		paintDataEff(g, x, y, 1);
		if (!checPaintByDataEff())
		{
			paintBody(g, x, y, frame, type_left_right, isEye: true);
		}
		if (flag)
		{
			paintEffFullSet_Last(g, x + num, y + 1, type_left_right);
		}
		paintDataEff(g, x, y, 0);
	}

	public bool checPaintByDataEff()
	{
		if (vecDataEff.size() == 0)
		{
			return false;
		}
		for (int i = 0; i < vecDataEff.size(); i++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(i);
			if (dataSkillEff != null && dataSkillEff.typeMove == 2)
			{
				return true;
			}
		}
		return false;
	}

	public void paintEffFullSet(mGraphics g, int x2, int y2, int typeLeftRight)
	{
		if (indexFullSet <= 0)
		{
			return;
		}
		int num = GameCanvas.gameTick / 5 % 3;
		int num2 = 0;
		if (indexFullSet == 5)
		{
			for (int num3 = 2; num3 >= 0; num3--)
			{
				if (mfraImgFullSet[2 + num3] == null || mfraImgFullSet[2 + num3].imgFrame == null || mfraImgFullSet[2 + num3].imgFrame.image == null)
				{
					mfraImgFullSet[2 + num3] = new FrameImage(373 + num3, 3);
				}
				else if (CRes.random(10) != 2)
				{
					if (lechYHead != 0)
					{
						num2 = 6;
						g.drawRegion(mfraImgFullSet[2 + num3].imgFrame, 0, mfraImgFullSet[2 + num3].frameHeight - 8 + num * mfraImgFullSet[2 + num3].frameHeight, mfraImgFullSet[2 + num3].frameWidth, 8, type_left_right, x2, y2, 33);
					}
					mfraImgFullSet[2 + num3].drawFrame(num, x2, y2 - num2, type_left_right, 33, g);
				}
			}
		}
		else if (mfraImgFullSet[indexFullSet - 1] == null || mfraImgFullSet[indexFullSet - 1].imgFrame == null || mfraImgFullSet[indexFullSet - 1].imgFrame.image == null)
		{
			mfraImgFullSet[indexFullSet - 1] = new FrameImage(370 + indexFullSet, 3);
		}
		else
		{
			if (lechYHead != 0)
			{
				num2 = 6;
				g.drawRegion(mfraImgFullSet[indexFullSet - 1].imgFrame, 0, mfraImgFullSet[indexFullSet - 1].frameHeight - 8 + num * mfraImgFullSet[indexFullSet - 1].frameHeight, mfraImgFullSet[indexFullSet - 1].frameWidth, 8, type_left_right, x2, y2, 33);
			}
			mfraImgFullSet[indexFullSet - 1].drawFrame(num, x2, y2 - num2, type_left_right, 33, g);
		}
	}

	public void paintEffFullSet_Last(mGraphics g, int x2, int y2, int typeLeftRight)
	{
		if (indexFullSet <= 0)
		{
			return;
		}
		int num = GameCanvas.gameTickChia4 % 3;
		int num2 = 0;
		if (mfraImgFullSet[5 + indexFullSet - 1] == null || mfraImgFullSet[5 + indexFullSet - 1].imgFrame == null || mfraImgFullSet[5 + indexFullSet - 1].imgFrame.image == null)
		{
			mfraImgFullSet[5 + indexFullSet - 1] = new FrameImage(375 + indexFullSet, 3);
			return;
		}
		if (lechYHead != 0)
		{
			g.drawRegion(mfraImgFullSet[5 + indexFullSet - 1].imgFrame, 0, mfraImgFullSet[5 + indexFullSet - 1].frameHeight - 8 + num * mfraImgFullSet[5 + indexFullSet - 1].frameHeight, mfraImgFullSet[5 + indexFullSet - 1].frameWidth, 8, type_left_right, x2, y2, 33);
		}
		mfraImgFullSet[5 + indexFullSet - 1].drawFrame(num, x2, y2 - num2, type_left_right, 33, g);
	}

	public void paintPhiPhong(mGraphics g)
	{
		MainImage imageAll = ObjectData.getImageAll(idparP, ObjectData.HashImageOtherNew, 23000);
		if (imageAll != null && imageAll.img != null)
		{
			int num = 1;
			int num2 = 1;
			int num3 = mImage.getImageWidth(imageAll.img.image) / num;
			int num4 = mImage.getImageHeight(imageAll.img.image) / num2;
			if (FramePP >= 0 && FramePP < num * num2)
			{
				int num5 = 0;
				int num6 = 0;
				num5 = FramePP / num2 * num3;
				num6 = FramePP % num2 * num4;
				int arg = ((type_left_right == 2) ? 2 : 0);
				g.drawRegion(imageAll.img, num5, num6, wOne, hOne, arg, x, y, mGraphics.TOP | mGraphics.HCENTER);
			}
		}
	}

	public void updatePP()
	{
		if (Action == 0 || Action == 1 || Action == 2)
		{
			fPP++;
			if (fPP > AR_FRAME_PP[Action].Length - 1)
			{
				fPP = 0;
			}
			FramePP = AR_FRAME_PP[Action][fPP];
		}
	}

	public void paintBody(mGraphics g, int x, int y, int fr, int dirr, bool isEye)
	{
		int num = ((dirr == 2) ? 2 : 0);
		int num2 = fr;
		if (frameOneStep != -1)
		{
			num2 = frameOneStep;
			ntickFrameOneStep++;
			if (ntickFrameOneStep >= 2)
			{
				ntickFrameOneStep = 0;
				frameOneStep = -1;
			}
		}
		int num3 = 0;
		int arg = 0;
		int num4 = 0;
		int[] array;
		if (num == 0)
		{
			array = mSortPaint;
			num4 = 3 + dxEye;
		}
		else
		{
			array = mSortPaintRight;
			num3 = 24;
			arg = 2;
			num4 = -8 - dxEye;
		}
		int[] array2 = array;
		foreach (int num5 in array2)
		{
			mPart mPart2 = partLow(num5);
			int num6 = num5;
			if (num5 == 6)
			{
				num6 = 3;
			}
			if ((isDonotShowHat == 0 && isDragon && ((timeDragon <= 0 && num6 == 5) || (timeDragon > 0 && num6 == 4))) || mPart2 == null || mPart2.pi == null)
			{
				continue;
			}
			int num7 = 0;
			int num8 = y + CharInfo[num2][num6][2] + mPart2.pi[CharInfo[num2][num6][0]].dy;
			num7 = ((num != 0) ? (x - CharInfo[num2][num6][1] - mPart2.pi[CharInfo[num2][num6][0]].dx) : (x + CharInfo[num2][num6][1] + mPart2.pi[CharInfo[num2][num6][0]].dx));
			if (num5 == 3 && lechYHead != 0 && clazz != 1 && clazz != 0)
			{
				num7 = ((num != 0) ? (num7 - fLechWeaponBigBody[clazz - 2][num2 * 2]) : (num7 + fLechWeaponBigBody[clazz - 2][num2 * 2]));
				num8 += fLechWeaponBigBody[clazz - 2][num2 * 2 + 1];
			}
			if ((num6 == 0 || num6 == 5 || num6 == 4) && (num6 != 0 || !isKoLechHead))
			{
				num8 += lechYHead;
			}
			if (num6 == 5 && (body == 950 || body == 963 || body == 972 || body == 953 || body == 1002))
			{
				num8 -= 20;
				num7 = ((num != 0) ? (num7 + 5) : (num7 - 5));
			}
			if (typeObject == 0 && !isBuffDevil && (num6 == 0 || num6 == 2 || num6 == 4 || num6 == 5))
			{
				int num9 = y - num8 + 5;
				if (num9 > 62 && num9 > hOne)
				{
					hOne = num9;
				}
			}
			short num10 = checkEffHair();
			short num11 = checkPlayFrameBody();
			short num12 = checkPlayFrameLeg();
			short num13 = checkPlayFrameWeapon();
			short num14 = checkPlayFrameHead();
			bool flag = true;
			if (num5 == 5 && num10 >= 0 && checkCF(num2))
			{
				if (imgEffHair == null || imgEffHair.img == null)
				{
					imgEffHair = ObjectData.getImageAll(num10, ObjectData.HashImageOtherNew, 23000);
				}
				if (imgEffHair.img != null)
				{
					MainImage imageAll = ObjectData.getImageAll(mPart2.pi[CharInfo[num2][num6][0]].id, ObjectData.HashImageCharPart, 10000);
					if (imageAll.img != null)
					{
						if (nFrameEffHair == 0)
						{
							imgEffHair.img.w = mImage.getImageWidth(imageAll.img.image);
							imgEffHair.img.h = mImage.getImageHeight(imageAll.img.image);
							nFrameEffHair = mImage.getImageHeight(imgEffHair.img.image) / imgEffHair.img.h;
						}
						else if (GameCanvas.gameTickChia4 % (nFrameEffHair + 1) < nFrameEffHair)
						{
							g.drawRegion(imgEffHair.img, 0, imgEffHair.img.h * (GameCanvas.gameTickChia4 % (nFrameEffHair + 1)), imgEffHair.img.w, imgEffHair.img.h, num, num7, num8, num3);
							flag = false;
						}
					}
				}
			}
			else if (num5 == 2 && num11 >= 0 && checkCFBody(num2) && !isBuffDevil)
			{
				if (imgEffPlayBody == null || imgEffPlayBody.img == null)
				{
					imgEffPlayBody = ObjectData.getImageAll(num11, ObjectData.HashImageOtherNew, 23000);
				}
				if (imgEffPlayBody.img != null)
				{
					MainImage imageAll2 = ObjectData.getImageAll(mPart2.pi[CharInfo[num2][num6][0]].id, ObjectData.HashImageCharPart, 10000);
					if (imageAll2.img != null)
					{
						if (nFrameEffBody == 0)
						{
							imgEffPlayBody.img.w = mImage.getImageWidth(imgEffPlayBody.img.image);
							imgEffPlayBody.img.h = mImage.getImageHeight(imageAll2.img.image);
							nFrameEffBody = mImage.getImageHeight(imgEffPlayBody.img.image) / imgEffPlayBody.img.h;
						}
						else if (mPlayframeBody[indexPlayMBody][tickPlayframeBody] < nFrameEffBody)
						{
							g.drawRegion(imgEffPlayBody.img, 0, imgEffPlayBody.img.h * mPlayframeBody[indexPlayMBody][tickPlayframeBody], imgEffPlayBody.img.w, imgEffPlayBody.img.h, num, num7, num8, num3);
							flag = false;
						}
					}
				}
			}
			else if (num5 == 1 && num12 >= 0 && checkCFLeg(num2))
			{
				if (imgEffPlayLeg == null || imgEffPlayLeg.img == null)
				{
					imgEffPlayLeg = ObjectData.getImageAll(num12, ObjectData.HashImageOtherNew, 23000);
				}
				if (imgEffPlayLeg.img != null)
				{
					MainImage imageAll3 = ObjectData.getImageAll(mPart2.pi[CharInfo[num2][num6][0]].id, ObjectData.HashImageCharPart, 10000);
					if (imageAll3.img != null)
					{
						if (nFrameEffLeg == 0)
						{
							imgEffPlayLeg.img.w = mImage.getImageWidth(imgEffPlayLeg.img.image);
							imgEffPlayLeg.img.h = mImage.getImageHeight(imageAll3.img.image);
							nFrameEffLeg = mImage.getImageHeight(imgEffPlayLeg.img.image) / imgEffPlayLeg.img.h;
						}
						else if (mPlayframeBody[indexPlayMBody][tickPlayframeBody] < nFrameEffLeg)
						{
							g.drawRegion(imgEffPlayLeg.img, 0, imgEffPlayLeg.img.h * mPlayframeBody[indexPlayMBody][tickPlayframeBody], imgEffPlayLeg.img.w, imgEffPlayLeg.img.h, num, num7, num8, num3);
							flag = false;
						}
					}
				}
			}
			else if (num5 == 0 && num14 >= 0 && checkCFHead(num2))
			{
				if (imgEffHead == null || imgEffHead.img == null)
				{
					imgEffHead = ObjectData.getImageAll(num14, ObjectData.HashImageOtherNew, 23000);
				}
				if (imgEffHead.img != null)
				{
					MainImage imageAll4 = ObjectData.getImageAll(mPart2.pi[CharInfo[num2][num6][0]].id, ObjectData.HashImageCharPart, 10000);
					if (imageAll4.img != null)
					{
						if (nFrameEffHead == 0)
						{
							imgEffHead.img.w = mImage.getImageWidth(imageAll4.img.image);
							imgEffHead.img.h = mImage.getImageHeight(imageAll4.img.image);
							nFrameEffHead = mImage.getImageHeight(imgEffHead.img.image) / imgEffHead.img.h;
						}
						else if (framePlayHead > 0)
						{
							g.drawRegion(imgEffHead.img, 0, imgEffHead.img.h * (framePlayHead - 1), imgEffHead.img.w, imgEffHead.img.h, num, num7, num8, num3);
							flag = false;
						}
					}
				}
			}
			else if (((num5 == 3 && weaponFashion == -1) || (num5 == 6 && weaponFashion != -1)) && num13 >= 0 && checkCFWeapon(num2))
			{
				if (imgEffPlayWeapon == null || imgEffPlayWeapon.img == null)
				{
					imgEffPlayWeapon = ObjectData.getImageAll(num13, ObjectData.HashImageOtherNew, 23000);
				}
				if (imgEffPlayWeapon.img != null && ObjectData.getImageAll(mPart2.pi[CharInfo[num2][num6][0]].id, ObjectData.HashImageCharPart, 10000).img != null)
				{
					if (nFrameEffWeapon == 0)
					{
						imgEffPlayWeapon.img.w = mImage.getImageWidth(imgEffPlayWeapon.img.image);
						imgEffPlayWeapon.img.h = mImage.getImageHeight(imgEffPlayWeapon.img.image) / 3;
						nFrameEffWeapon = mImage.getImageHeight(imgEffPlayWeapon.img.image) / imgEffPlayWeapon.img.h;
					}
					else if (mPlayframeBody[indexPlayWeapon][tickPlayframeWeapon] < nFrameEffWeapon)
					{
						g.drawRegion(imgEffPlayWeapon.img, 0, imgEffPlayWeapon.img.h * mPlayframeBody[indexPlayWeapon][tickPlayframeWeapon], imgEffPlayWeapon.img.w, imgEffPlayWeapon.img.h, num, num7, num8, num3);
						flag = false;
					}
				}
			}
			if (flag)
			{
				SmallImage.drawSmallImage(g, mPart2.pi[CharInfo[num2][num6][0]].id, num7, num8, num, num3, num5);
			}
			if (!GameCanvas.lowGraphic && isEye && checkCF(num2) && num6 == 0 && indexEye >= 0 && eye < 2)
			{
				g.drawRegion(AvMain.imgEye, indexEye * 5, eye * 5, 5, 5, arg, num7 + num4, num8 + 6 + dyEye, 0);
				if (head == 770)
				{
					g.drawRegion(AvMain.imgEye, indexEye * 5 + 2, eye * 5, 2, 5, arg, num7 + num4 + ((type_left_right != 0) ? 6 : (-3)), num8 + 6 + dyEye, 0);
				}
				else if (head == 769 && mGraphics.zoomLevel > 1)
				{
					g.drawRegion(AvMain.imgEye, indexEye * 5 + 2, eye * 5, 2, 5, arg, num7 + num4 + ((type_left_right != 0) ? 5 : (-2)), num8 + 6 + dyEye, 0);
				}
			}
		}
		short num15 = checkEffBody();
		if (num15 < 0)
		{
			return;
		}
		if (fraEffBody == null)
		{
			fraEffBody = new FrameImage(num15, frameEffBody);
		}
		else if (typeActionEffBody == EBODY_RANDOM_NORMAL)
		{
			int num16 = x + xEffBody;
			if (type_left_right == 2)
			{
				num16 = x - xEffBody;
			}
			fraEffBody.drawFrame(framePlayEB, num16, y + yEffBody, type_left_right, 3, g);
		}
	}

	private bool checkCF(int fr)
	{
		if (fr == 17 || fr == 18 || fr == 53 || fr == 58 || fr == 59)
		{
			return false;
		}
		return true;
	}

	private bool checkCFHead(int fr)
	{
		if (fr <= 7)
		{
			return true;
		}
		return false;
	}

	private bool checkCFBody(int fr)
	{
		if (GameCanvas.gameTick % 4 == 0)
		{
			tickPlayframeBody++;
		}
		if (tickPlayframeBody >= mPlayframeBody[indexPlayMBody].Length)
		{
			tickPlayframeBody = 0;
			indexPlayMBody = CRes.random(mPlayframeBody.Length);
		}
		if (fr == 0 || fr == 1)
		{
			return true;
		}
		return false;
	}

	private bool checkCFWeapon(int fr)
	{
		if (GameCanvas.gameTick % 4 == 0)
		{
			tickPlayframeWeapon++;
		}
		if (tickPlayframeWeapon >= mPlayframeBody[indexPlayWeapon].Length)
		{
			tickPlayframeWeapon = 0;
			indexPlayWeapon = CRes.random(mPlayframeBody.Length);
		}
		if (fr == 0 || fr == 1)
		{
			return true;
		}
		return false;
	}

	private bool checkCFLeg(int fr)
	{
		if (fr == 0 || fr == 1)
		{
			return true;
		}
		return false;
	}

	private short checkEffBody()
	{
		for (int i = 0; i < idEffBody.Length; i++)
		{
			if (body != idEffBody[i])
			{
				continue;
			}
			xEffBody = 3;
			yEffBody = -47;
			frameEffBody = 4;
			typeActionEffBody = EBODY_RANDOM_NORMAL;
			if (GameCanvas.gameTick % 5 == 0 && CRes.random(6) != 0)
			{
				framePlayEB++;
				if (framePlayEB >= 4)
				{
					framePlayEB = 0;
				}
			}
			return 384;
		}
		return -1;
	}

	private short checkEffHair()
	{
		if (dataEffHair == null)
		{
			return idHairbay;
		}
		try
		{
			for (int i = 0; i < dataEffHair.Length; i++)
			{
				if (hair == dataEffHair[i].id)
				{
					return (short)dataEffHair[i].idIcon;
				}
			}
			return idHairbay;
		}
		catch (Exception)
		{
			return idHairbay;
		}
	}

	private short checkPlayFrameBody()
	{
		return body switch
		{
			756 => 302, 
			719 => 301, 
			748 => 300, 
			_ => idBodybay, 
		};
	}

	private short checkPlayFrameLeg()
	{
		return idLegBay;
	}

	private short checkPlayFrameWeapon()
	{
		return idWeaponBay;
	}

	private short checkPlayFrameHead()
	{
		for (int i = 0; i < idPlayFrameHead.Length; i++)
		{
			if (head != idPlayFrameHead[i])
			{
				continue;
			}
			tickEffHead++;
			if (framePlayHead == 0)
			{
				if (tickEffHead > 400 && CRes.random(60) == 0)
				{
					framePlayHead = 1;
					tickEffHead = 0;
				}
			}
			else if (framePlayHead == 1)
			{
				if (tickEffHead > 4)
				{
					framePlayHead = 2;
					tickEffHead = 0;
				}
			}
			else if (framePlayHead == 2)
			{
				if (tickEffHead > 120 && CRes.random(30) == 0)
				{
					framePlayHead = 0;
					tickEffHead = 0;
				}
			}
			else
			{
				framePlayHead = 0;
				tickEffHead = 0;
			}
			return 303;
		}
		return -1;
	}

	public void resetPart()
	{
		if (GameScreen.player != null)
		{
			head = GameScreen.player.head;
			leg = GameScreen.player.leg;
			body = GameScreen.player.body;
			weapon = GameScreen.player.weapon;
			hat = GameScreen.player.hat;
			hair = GameScreen.player.hair;
			weaponFashion = GameScreen.player.weaponFashion;
			clazz = GameScreen.player.clazz;
			nFrameEffHair = 0;
			nFrameEffHead = 0;
			setHeadBigBody();
		}
	}

	public mPart getpaint(int index)
	{
		mPart result = null;
		int num = -1;
		switch (index)
		{
		case 0:
			num = head;
			break;
		case 1:
			if (isPaintLeg)
			{
				num = leg;
			}
			break;
		case 2:
			num = body;
			break;
		case 3:
			if (isPaintWeapon && (isDonotShowWeaponF == 1 || ischeckWeaponF()))
			{
				num = weapon;
			}
			break;
		case 4:
			if (isDonotShowHat != 1 && (isDonotShowHat == 0 || isDragon))
			{
				num = hat;
			}
			break;
		case 5:
			num = hair;
			break;
		case 6:
			if (ischeckWeaponF())
			{
				if (isDonotShowWeaponF == 0)
				{
					num = weaponFashion;
				}
			}
			else if (isPaintWeapon && isDonotShowWeaponF == 0)
			{
				num = weaponFashion;
			}
			break;
		}
		if (num >= 0)
		{
			result = CharPartInfo.getPart(num);
		}
		return result;
	}

	private bool ischeckWeaponF()
	{
		for (int i = 0; i < idWeaponF.Length; i++)
		{
			if (weaponFashion == idWeaponF[i])
			{
				return false;
			}
		}
		return true;
	}

	public mPart partLow(int index)
	{
		if (this == GameScreen.player || !GameCanvas.isLowGraOrWP_PvP() || GameCanvas.gameScr == null || GameCanvas.currentScreen != GameCanvas.gameScr)
		{
			return getpaint(index);
		}
		mPart result = null;
		int num = -1;
		if (typeObject != 1)
		{
			int num2 = ((index != 0) ? (clazz switch
			{
				1 => index switch
				{
					1 => 4, 
					2 => 3, 
					5 => 1, 
					3 => weapon, 
					_ => -1, 
				}, 
				2 => index switch
				{
					1 => 27, 
					2 => 26, 
					5 => 24, 
					3 => weapon, 
					_ => -1, 
				}, 
				3 => index switch
				{
					1 => 31, 
					2 => 30, 
					5 => 28, 
					3 => weapon, 
					_ => -1, 
				}, 
				4 => index switch
				{
					1 => 35, 
					2 => 34, 
					5 => 32, 
					3 => weapon, 
					_ => -1, 
				}, 
				5 => index switch
				{
					1 => 39, 
					2 => 38, 
					5 => 36, 
					3 => weapon, 
					_ => -1, 
				}, 
				_ => index switch
				{
					1 => 4, 
					2 => 3, 
					5 => 1, 
					3 => weapon, 
					_ => -1, 
				}, 
			}) : 0);
			num = num2;
		}
		else
		{
			if (typeBossMonster != 0)
			{
				return getpaint(index);
			}
			num = index switch
			{
				0 => 8, 
				1 => 10, 
				2 => 9, 
				3 => weapon, 
				_ => -1, 
			};
		}
		if (num >= 0)
		{
			result = CharPartInfo.getPart(num);
		}
		return result;
	}

	public static mPart getOnePart(int value)
	{
		mPart result = null;
		if (value > -1)
		{
			result = CharPartInfo.getPart((short)value);
		}
		return result;
	}

	public static void paintOnePart(mGraphics g, int value, int index, int x, int y, int valueDy)
	{
		mPart onePart = getOnePart(value);
		if (onePart != null && onePart.pi != null)
		{
			int num = 0;
			int num2 = 0;
			switch (valueDy)
			{
			case 0:
				num2 = CharInfo[num][index][2];
				break;
			case 1:
				num2 = 0;
				break;
			case 2:
				num2 = CharInfo[num][index][1];
				break;
			}
			SmallImage.drawSmallImage(g, onePart.pi[CharInfo[num][index][0]].id, x + CharInfo[num][index][1] + onePart.pi[CharInfo[num][index][0]].dx, y + num2 + onePart.pi[CharInfo[num][index][0]].dy, 0, 0, value);
		}
	}

	public virtual void paintHead(mGraphics g, int x, int y, int trans)
	{
		short num = -1;
		if (isDonotShowHat == 0)
		{
			num = hat;
		}
		paintHeadEveryWhere(g, head, hair, num, x, y + 38, trans);
	}

	public static void paintHeadEveryWhere(mGraphics g, short head, short hair, short hat, int x, int y, int trans)
	{
		int num = 0;
		for (int i = 0; i < 3; i++)
		{
			int num2 = mSortPaintHead[i];
			mPart mPart2 = null;
			if (i == 0 && head >= 0)
			{
				mPart2 = CharPartInfo.getPart(head);
			}
			else if (i == 1 && hair >= 0)
			{
				mPart2 = CharPartInfo.getPart(hair);
			}
			else if (i == 2 && hat >= 0)
			{
				mPart2 = CharPartInfo.getPart(hat);
			}
			if (mPart2 != null && mPart2.pi != null)
			{
				if (trans == 0)
				{
					SmallImage.drawSmallImage(g, mPart2.pi[CharInfo[num][num2][0]].id, x + CharInfo[num][num2][1] + mPart2.pi[CharInfo[num][num2][0]].dx, y + CharInfo[num][num2][2] + mPart2.pi[CharInfo[num][num2][0]].dy, trans, 0, num2);
				}
				else
				{
					SmallImage.drawSmallImage(g, mPart2.pi[CharInfo[num][num2][0]].id, x - CharInfo[num][num2][1] - mPart2.pi[CharInfo[num][num2][0]].dx, y + CharInfo[num][num2][2] + mPart2.pi[CharInfo[num][num2][0]].dy, trans, 24, num2);
				}
			}
		}
	}

	public virtual void Giaotiep()
	{
	}

	public void GiaotiepNPC()
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(new iCommand(nameGiaotiep, 2, this));
		if (getListQuestNPC().size() > 0)
		{
			mVector2.addElement(new iCommand(T.quest, 0, this));
		}
		GameCanvas.menu.startAt_NPC(mVector2, chatNPC, ID, typeObject, isHelp: false, 0, isDynamic: false);
	}

	public void GiaotiepOtherPlayer()
	{
		GameScreen.objGiaotiep = GameScreen.objFocus;
		mVector mVector2 = new mVector();
		if (ReadMessenge.ID_GiaoTiep_Server >= 0)
		{
			GameCanvas.gameScr.cmdGiaotiepServer.caption = ReadMessenge.Str_GiaoTiep_Server;
			mVector2.addElement(GameCanvas.gameScr.cmdGiaotiepServer);
		}
		mVector2.addElement(GameCanvas.gameScr.cmdInfoPlayer);
		if (GameCanvas.language == 0)
		{
			mVector2.addElement(GameCanvas.gameScr.cmdTangRuby);
		}
		mVector2.addElement(GameCanvas.gameScr.cmdInviteParty);
		mVector2.addElement(GameCanvas.gameScr.cmdChatPlayer);
		mVector2.addElement(GameCanvas.gameScr.cmdAddFriend);
		if (GameScreen.typeMod == 1)
		{
			mVector2.addElement(GameCanvas.gameScr.cmdLockChat);
		}
		mVector2.addElement(GameCanvas.gameScr.cmdFight);
		mVector2.addElement(GameCanvas.gameScr.cmdInviteTrade);
		if (clan == null && GameScreen.player.clan != null)
		{
			mVector2.addElement(GameCanvas.gameScr.cmdMoiVaoClan);
		}
		if (clan != null && GameScreen.player.clan == null)
		{
			mVector2.addElement(GameCanvas.gameScr.cmdXinVaoClan);
		}
		mVector2.addElement(GameCanvas.gameScr.cmdBaisu);
		GameCanvas.menu.startAt(mVector2, 2, T.Giaotiep);
	}

	public mVector getListQuestNPC()
	{
		mVector mVector2 = new mVector();
		for (int i = 0; i < Player.vecQuest.size(); i++)
		{
			MainQuest mainQuest = (MainQuest)Player.vecQuest.elementAt(i);
			if (mainQuest.idNPC == ID || mainQuest.idNPC_Sub == ID)
			{
				mVector2.addElement(mainQuest);
			}
		}
		return mVector2;
	}

	public virtual iCommand getCenterCmd()
	{
		return null;
	}

	public void setResetWearing()
	{
		sethead(headMain);
		body = bodyMain;
		leg = legMain;
		sethair(hairMain);
		isBuffDevil = false;
		setHeadBigBody();
	}

	public virtual void setWearing(short[] mWear)
	{
		weapon = mWear[0];
		hat = mWear[1];
		if (hat == 0)
		{
			hat = -1;
		}
		body = mWear[3];
		leg = mWear[5];
		headMain = head;
		bodyMain = body;
		legMain = leg;
		hairMain = hair;
		mSystem.outz("setWearing name=" + name + " " + weapon + "  " + hat + "  " + body + "  " + leg + "  " + hair + "  " + head);
		checkBuffDevil();
		checkEffWeapon();
		setHeadBigBody();
	}

	public void sethead(short headset)
	{
		head = headset;
		nFrameEffHead = 0;
		short num = checkPlayFrameHead();
		imgEffHair = null;
		if (num >= 0)
		{
			imgEffHead = ObjectData.getImageAll(num, ObjectData.HashImageOtherNew, 23000);
		}
		setEye();
	}

	public void setBodySkill()
	{
		indexBodySkill = 0;
	}

	public void sethair(short hairset)
	{
		nFrameEffHair = 0;
		hair = hairset;
		short num = checkEffHair();
		imgEffHair = null;
		if (num >= 0)
		{
			imgEffHair = ObjectData.getImageAll(num, ObjectData.HashImageOtherNew, 23000);
		}
	}

	public void setHeadBigBody()
	{
		fraEffBody = null;
		typeEfffashion = -1;
		nFrameEffBody = 0;
		imgEffPlayBody = null;
		bool flag = false;
		for (int i = 0; i < ListLechHEAD.Length; i++)
		{
			if (body == ListLechHEAD[i])
			{
				flag = true;
			}
		}
		isKoLechHead = false;
		for (int j = 0; j < ListKoLechHEAD.Length; j++)
		{
			if (head == ListKoLechHEAD[j])
			{
				isKoLechHead = true;
			}
		}
		if (typeObject == 0)
		{
			isDragon = false;
			if (isBuffDevil)
			{
				lechYHead = 0;
				hOne = 52;
			}
			else if (body == 950 || body == 953 || body == 963 || body == 972 || body == 1002)
			{
				hOne = 80;
			}
			else if (flag)
			{
				lechYHead = -6;
				hOne = 62;
				if (body == 748)
				{
					isDragon = true;
				}
				if (body == 798)
				{
					short num = checkEffBody();
					if (num >= 0)
					{
						fraEffBody = new FrameImage(num, frameEffBody);
					}
				}
			}
			else
			{
				lechYHead = 0;
				hOne = 52;
			}
			if (body == 810 || body == 813)
			{
				typeEfffashion = 1;
			}
			short num2 = checkPlayFrameBody();
			if (num2 != -1)
			{
				imgEffPlayBody = ObjectData.getImageAll(num2, ObjectData.HashImageOtherNew, 23000);
			}
			updateHOneByParts();
		}
		mSystem.outz("body " + body + " lechYHead = " + lechYHead + " hone = " + hOne);
	}

	public void updateHOneByParts()
	{
		if (typeObject != 0 || isBuffDevil)
		{
			return;
		}
		try
		{
			int num = 0;
			if (CharInfo == null || num >= CharInfo.Length)
			{
				return;
			}
			int[] obj = new int[4] { 0, 2, 4, 5 };
			int num2 = hOne;
			int[] array = obj;
			foreach (int num3 in array)
			{
				mPart mPart2 = getpaint(num3);
				if (mPart2 == null || mPart2.pi == null || num3 >= CharInfo[num].Length)
				{
					continue;
				}
				int num4 = CharInfo[num][num3][0];
				if (num4 >= 0 && num4 < mPart2.pi.Length && mPart2.pi[num4] != null)
				{
					int num5 = CharInfo[num][num3][2] + mPart2.pi[num4].dy;
					if ((num3 == 0 || num3 == 5 || num3 == 4) && (num3 != 0 || !isKoLechHead))
					{
						num5 += lechYHead;
					}
					if (num3 == 5 && (body == 950 || body == 963 || body == 972 || body == 953 || body == 1002))
					{
						num5 -= 20;
					}
					int num6 = -num5 + 5;
					if (num6 > 62 && num6 > num2)
					{
						num2 = num6;
					}
				}
			}
			if (num2 > hOne)
			{
				hOne = num2;
			}
		}
		catch (Exception)
		{
		}
	}

	private void setEye()
	{
		if (hat == 460)
		{
			indexEye = -1;
		}
		else if (head == 0 || head == 487 || head == 488 || head == 489 || head == 573 || head == 574 || head == 575 || head == 614 || head == 769 || head == 770 || head == 768 || head == 731)
		{
			if (mGraphics.zoomLevel <= 1)
			{
				if (head == 573)
				{
					dxEye = 3;
					dyEye = 0;
					indexEye = 0;
				}
				else if (head == 574)
				{
					dxEye = 1;
					dyEye = -1;
					indexEye = 1;
				}
				else if (head == 575)
				{
					dxEye = 1;
					dyEye = 0;
					indexEye = 0;
				}
				else if (head == 731 || head == 614)
				{
					dxEye = 2;
					dyEye = -1;
					indexEye = 2;
				}
				else if (head == 768)
				{
					dxEye = 5;
					dyEye = -2;
					indexEye = 3;
				}
				else if (head == 769)
				{
					dxEye = 1;
					dyEye = -1;
					indexEye = 4;
				}
				else if (head == 770)
				{
					dxEye = 1;
					dyEye = 0;
					indexEye = 0;
				}
				else
				{
					dxEye = 0;
					dyEye = 0;
					indexEye = 0;
				}
			}
			else if (head == 573)
			{
				dxEye = 2;
				dyEye = 0;
				indexEye = 0;
			}
			else if (head == 574)
			{
				dxEye = 0;
				dyEye = 0;
				indexEye = 1;
			}
			else if (head == 575)
			{
				dxEye = 0;
				dyEye = -1;
				indexEye = 0;
			}
			else if (head == 731 || head == 614)
			{
				dxEye = 0;
				dyEye = -1;
				indexEye = 2;
			}
			else if (head == 768)
			{
				dxEye = 4;
				dyEye = -2;
				indexEye = 3;
			}
			else if (head == 769)
			{
				dxEye = 1;
				dyEye = -1;
				indexEye = 4;
			}
			else if (head == 770)
			{
				dxEye = 1;
				dyEye = -1;
				indexEye = 0;
			}
			else
			{
				dxEye = 0;
				dyEye = 0;
				indexEye = 0;
			}
		}
		else
		{
			indexEye = -1;
		}
	}

	private void checkEffWeapon()
	{
		if (clazz == 4)
		{
			switch (weapon)
			{
			case 182:
				indexEff_1 = 1;
				break;
			case 6:
			case 227:
			case 463:
				indexEff_1 = 0;
				break;
			case 177:
			case 246:
				indexEff_1 = 2;
				break;
			case 262:
				indexEff_1 = 3;
				break;
			case 467:
				indexEff_1 = 4;
				break;
			default:
				indexEff_1 = 0;
				break;
			}
		}
		else if (clazz == 2)
		{
			switch (weapon)
			{
			case 5:
			case 225:
			case 244:
			case 260:
			case 461:
				indexEff_1 = 0;
				break;
			case 183:
				indexEff_1 = 1;
				break;
			case 465:
				indexEff_1 = 2;
				break;
			}
		}
	}

	public virtual void setWearingMon(short[] mWear)
	{
		sethead(mWear[0]);
		body = mWear[1];
		leg = mWear[2];
		weapon = mWear[3];
	}

	public void checkBuffDevil()
	{
		for (int i = 0; i < vecBuffCur.size(); i++)
		{
			MainBuff mainBuff = (MainBuff)vecBuffCur.elementAt(i);
			if (mainBuff.isDevil && !mainBuff.isRemove)
			{
				sethead(mainBuff.head);
				body = mainBuff.body;
				leg = mainBuff.leg;
				sethair(-1);
				isBuffDevil = true;
				setHeadBigBody();
				break;
			}
		}
	}

	public void setWearingIsNull(short[] mWear)
	{
		if (mWear[0] >= 0)
		{
			weapon = mWear[0];
		}
		if (mWear[1] == -2)
		{
			hat = -1;
		}
		else if (mWear[1] >= 0)
		{
			hat = mWear[1];
			if (hat == 0)
			{
				hat = -1;
			}
		}
		if (mWear[3] >= 0)
		{
			body = mWear[3];
		}
		if (mWear[5] >= 0)
		{
			leg = mWear[5];
		}
		if (mWear[1] == -2)
		{
			sethair(-1);
		}
		else if (mWear[7] >= 0)
		{
			sethair(mWear[7]);
		}
		if (mWear[6] >= 0)
		{
			sethead(mWear[6]);
		}
	}

	public virtual void setTouchPoint()
	{
	}

	public virtual void setFireObject(int value)
	{
	}

	public virtual void setImgMonSterforOtherPlayer(sbyte typeMove)
	{
	}

	public void setSpeed(int vM, int vySeaM)
	{
		vMax = vM * (100 + GameCanvas.percent) / 100;
		vMaxYSea = vySeaM * (100 + GameCanvas.percent) / 100;
	}

	public void setDirNew(sbyte typeEff)
	{
		switch (typeEff)
		{
		case 2:
			if (x < 100)
			{
				toX = x;
				x = 0;
			}
			else if (x > GameCanvas.loadmap.maxWMap - 100)
			{
				toX = x;
				x = GameCanvas.loadmap.maxWMap;
			}
			break;
		case 1:
			isTanHinh = true;
			timeEffRemoveChar = 6;
			if (GameScreen.isShowNameSUPER_BOSS)
			{
				GameScreen.addEffectEnd_ObjTo(32, 0, x, y, ID, typeObject, (sbyte)type_left_right, null);
			}
			break;
		case 3:
		{
			int xSea = 0;
			int num = -1;
			for (int i = 0; i < LoadMap.mSea.Length; i++)
			{
				if (LoadMap.mSea[i][0] == GameCanvas.loadmap.idMap)
				{
					xSea = LoadMap.mSea[i][3];
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				break;
			}
			if (GameScreen.vecBoat.size() > 0)
			{
				int num2 = 40;
				if (GameScreen.vecBoat.size() > 6)
				{
					num2 = 20;
				}
				if (GameScreen.vecBoat.size() > 10)
				{
					num2 = 5;
				}
				if (GameScreen.vecBoat.size() > 0)
				{
					Boat boat = (Boat)GameScreen.vecBoat.elementAt(GameScreen.vecBoat.size() - 1);
					if (num != -1)
					{
						xSea = boat.x + LoadMap.mSea[num][5] * num2;
					}
				}
			}
			setActionSea((sbyte)LoadMap.mSea[num][2], xSea, LoadMap.mSea[num][4]);
			break;
		}
		}
	}

	public void updateActionPerson()
	{
		f++;
		if (Action != 0)
		{
			timeFullSet = 0;
		}
		else if (timeFullSet < 100)
		{
			timeFullSet++;
		}
		switch (Action)
		{
		case 0:
			actionStand();
			break;
		case 1:
		{
			int[] array = feRun;
			if (checkBoatSea() && boatSea != null)
			{
				array = feStand;
			}
			else if (GameCanvas.gameTick % 3 == 0)
			{
				if (GameCanvas.loadmap.getTile(x, y) == 0 && isInfo)
				{
					int num = x;
					int num2 = y;
					int dir = type_left_right;
					GameScreen.CreateEffOnMap(0, num, num2, dir, stepLeg++);
				}
				if (indexFullSet == 5)
				{
					int num3 = x;
					int num4 = y + 4;
					int dir2 = type_left_right;
					GameScreen.CreateEffOnMapFullset(157, num3, num4, dir2, stepLeg++);
				}
				if (stepLeg > 100)
				{
					stepLeg = 0;
				}
			}
			if (f > array.Length - 1)
			{
				f = 0;
			}
			if (vx == 0 && vy == 0 && posTransRoad == null && !isMoveNor)
			{
				Action = 0;
				f = 0;
			}
			frame = array[f];
			break;
		}
		case 2:
			updateActionFire();
			break;
		case 3:
			updateAva();
			break;
		case 4:
			frame = 38;
			updateDy();
			return;
		case 5:
			frame = 60;
			break;
		}
		updateDirPaint();
		if (Action == 2)
		{
			return;
		}
		updateDy();
		if (!isPaintWeapon)
		{
			isPaintWeapon = true;
		}
		if (!isPaintLeg)
		{
			isPaintLeg = true;
		}
		if (isTanHinh)
		{
			if (timeEffRemoveChar > 0)
			{
				timeEffRemoveChar--;
			}
			else
			{
				isTanHinh = false;
			}
		}
	}

	public void updateActionPerLogin()
	{
		f++;
		switch (Action)
		{
		case 0:
			actionStand();
			break;
		case 1:
		{
			int[] array = feRun;
			if (checkBoatSea() && boatSea != null)
			{
				array = feStand;
			}
			if (f > array.Length - 1)
			{
				f = 0;
			}
			if (vx == 0 && vy == 0 && posTransRoad == null)
			{
				Action = 0;
				f = 0;
			}
			frame = array[f];
			break;
		}
		case 2:
			updateActionFire();
			break;
		}
	}

	public virtual void actionStand()
	{
		if (f > feStand.Length - 1)
		{
			f = 0;
		}
		frame = feStand[f];
	}

	public void updateActionMonSter(bool isPet)
	{
		f++;
		if (mActionMonSter == null)
		{
			return;
		}
		int num = Action;
		if (checkBoatSea() && boatSea != null && Action == 1)
		{
			num = 0;
		}
		bool flag = false;
		if (isPet)
		{
			if (isPlayStand && Action == 0)
			{
				if (f > mActionStandMonSter.Length - 1)
				{
					flag = true;
				}
			}
			else if (f > mActionMonSter[Action].Length - 1)
			{
				flag = true;
			}
		}
		else if (f > mActionMonSter[num].Length - 1)
		{
			flag = true;
		}
		if (flag)
		{
			f = 0;
			if (Action == 3 || Action == 2)
			{
				Action = 0;
				vx = 0;
				vy = 0;
			}
		}
		if (Action == 1 && vx == 0 && vy == 0)
		{
			Action = 0;
			f = 0;
			if (isPet)
			{
				setSpeed(5, 5);
				typeShadow = 1;
				dyMovePet = 0;
			}
		}
		if (timeFreeFire > 0)
		{
			timeFreeFire--;
		}
	}

	public virtual void updateActionFire()
	{
		if (plashNow == null)
		{
			return;
		}
		tickfire++;
		int num = plashNow.update();
		if (num == -1 || tickfire >= 200)
		{
			if (GameScreen.player != null && this == GameScreen.player)
			{
				demFire++;
				if (demFire >= 3)
				{
					GlobalService.gI().Obj_Move((short)x, (short)y);
					demFire = 0;
				}
			}
			plashNow = null;
			Action = 0;
			resetAction();
			tickfire = 0;
		}
		else
		{
			frame = num;
		}
	}

	public void updateDirPaint()
	{
		if (Dir == 1 || Dir == 2)
		{
			if (toX > x)
			{
				type_left_right = 2;
			}
			else if (toX < x)
			{
				type_left_right = 0;
			}
		}
		if (Dir == 0 && type_left_right == 2)
		{
			type_left_right = 0;
		}
		else if (Dir == 2 && type_left_right == 0)
		{
			type_left_right = 2;
		}
	}

	public virtual void updateAva()
	{
		Action = 0;
	}

	public void setTypeQuest()
	{
		typeQuest = 0;
		for (int i = 0; i < Player.vecQuest.size(); i++)
		{
			MainQuest mainQuest = (MainQuest)Player.vecQuest.elementAt(i);
			if (mainQuest.idNPC == ID)
			{
				if (typeQuest == 0)
				{
					if (mainQuest.statusQuest == 0)
					{
						typeQuest = 1;
					}
					if (mainQuest.statusQuest == 1)
					{
						typeQuest = 2;
					}
					if (mainQuest.statusQuest == 2)
					{
						typeQuest = 3;
					}
				}
				else if (typeQuest == 1)
				{
					if (mainQuest.statusQuest == 2)
					{
						typeQuest = 3;
					}
				}
				else if (typeQuest == 2)
				{
					if (mainQuest.statusQuest == 0)
					{
						typeQuest = 1;
					}
					if (mainQuest.statusQuest == 2)
					{
						typeQuest = 3;
					}
				}
			}
			else if (mainQuest.idNPC_Sub == ID && typeQuest == 0)
			{
				typeQuest = 2;
			}
			if (typeQuest == 3)
			{
				break;
			}
		}
	}

	public void updateEye()
	{
		if (this == GameScreen.player && Player.isGhost)
		{
			eye = 1;
		}
		else if (eye < 2)
		{
			timeEye++;
			if (timeEye > 1 && timeEye < 6)
			{
				eye = 1;
			}
			else
			{
				eye = 0;
			}
			if (timeEye >= 8)
			{
				timeEye = 0;
				eye = 2;
				if (clazz != 4)
				{
					endEye = CRes.random(50, 100);
				}
				else
				{
					endEye = CRes.random(20, 70);
				}
			}
		}
		else
		{
			timeEye++;
			if (timeEye >= endEye)
			{
				timeEye = 0;
				eye = 0;
			}
		}
	}

	public virtual void AddBuff(MainBuff buff)
	{
		for (int i = 0; i < vecBuffCur.size(); i++)
		{
			if (((MainBuff)vecBuffCur.elementAt(i)).IdBuff == buff.IdBuff)
			{
				vecBuffCur.removeElementAt(i);
				i--;
			}
		}
		buff.setYlech(this);
		vecBuffCur.addElement(buff);
	}

	public virtual void AddBuffNew(MainBuff buff)
	{
		for (int i = 0; i < vecBuffCurNew.size(); i++)
		{
			if (((MainBuff)vecBuffCurNew.elementAt(i)).IdBuff == buff.IdBuff)
			{
				vecBuffCurNew.removeElementAt(i);
				i--;
			}
		}
		buff.setYlech(this);
		vecBuffCurNew.addElement(buff);
	}

	public virtual void AddBuffNew2(MainBuff buff)
	{
		for (int i = 0; i < vecBuffCurNew.size(); i++)
		{
			if (((MainBuff)vecBuffCurNew.elementAt(i)).IdBuff == buff.IdBuff)
			{
				vecBuffCurNew.removeElementAt(i);
				i--;
			}
		}
		buff.setYlech(this);
		vecBuffCurNew.addElement(buff);
	}

	public void setInfoMapTrain()
	{
		mSkillMapTrain = new short[3] { 21, 34, 38 };
		mPosMapTrain = new int[3][];
		tickMapTrain = CRes.random(70);
		for (int i = 0; i < MainPlayer.mPosTrainOther[0].Length; i++)
		{
			mPosMapTrain[i] = new int[2];
			for (int j = 0; j < MainPlayer.mPosTrainOther[0][i].Length; j++)
			{
				mPosMapTrain[i][j] = MainPlayer.mPosTrainOther[0][i][j] + CRes.random_Am_0(2);
			}
		}
	}

	public virtual int isNPC_Area()
	{
		return 1;
	}

	public void addEffSpec(short type, short time)
	{
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			Effect_Spec effect_Spec = (Effect_Spec)vecEffspec.elementAt(i);
			if (effect_Spec.typeEffect == type)
			{
				if (effect_Spec.getTimeEff() > time)
				{
					return;
				}
				vecEffspec.removeElement(effect_Spec);
				break;
			}
		}
		Effect_Spec effect_Spec2 = new Effect_Spec(this, type, time);
		vecEffspec.addElement(effect_Spec2);
		if (effect_Spec2.typeEffect == 15)
		{
			SetXlechEffectBongToi();
		}
	}

	public void Move_to_Focus_Person()
	{
		if (timeBeginUpdateMove == 0)
		{
			toX = toXNew;
			toY = toYNew;
		}
		if (x != toX || y != toY)
		{
			if (CRes.abs(x - toX) > vMax || CRes.abs(y - toY) > vMax)
			{
				move_to_XY_Normal();
			}
			else if (Action != 2 && Action != 3 && Action != 4)
			{
				toX = x;
				toY = y;
				vx = 0;
				vy = 0;
				Action = 0;
			}
		}
	}

	public virtual void updateActionUpBoat()
	{
	}

	public virtual void setActionSea(sbyte type, int xSea, int ySea)
	{
	}

	public virtual void updateActionDown_2()
	{
	}

	public virtual void updateActionUp_2()
	{
	}

	public virtual void updateActionDown()
	{
	}

	public virtual void updateActionUp()
	{
	}

	public bool isHuman()
	{
		if (typePlayer == 2 || typePlayer == 3)
		{
			return false;
		}
		return true;
	}

	public void setHpEff()
	{
		if (HpEff > Hp)
		{
			tickHpEff++;
			if (speedHpEff <= 0)
			{
				speedHpEff = 20;
			}
			if (speedHpEff < (HpEff - Hp) / 10)
			{
				speedHpEff = (HpEff - Hp) / 10;
			}
			if (tickHpEff > 10)
			{
				HpEff -= speedHpEff;
			}
		}
		else
		{
			HpEff = Hp;
			speedHpEff = 0;
			tickHpEff = 0;
		}
	}

	public virtual int getTypeMoveMonster()
	{
		return -1;
	}

	public void setWName()
	{
		wNameNPC = mFont.tahoma_7b_white.getWidth(name) + 8;
		wNameNPC += wNameNPC % 2;
		wNameFocus = wNameNPC + 6;
		if (wNameFocus < 74)
		{
			wNameFocus = 74;
		}
	}

	public bool checkBoatSea()
	{
		if (LoadMap.specMap == 4 || typeActionBoat != 0 || GameCanvas.currentScreen == GameCanvas.loginScr || (!LoadMap.isOnlineMap && (LoadMap.specMap == 5 || LoadMap.specMap == 8 || LoadMap.specMap == 12)))
		{
			return true;
		}
		return false;
	}

	public virtual void addEffBuff(sbyte typeBuff, short effBuff, short time)
	{
		mVector mVector2 = new mVector();
		Object_Effect_Skill object_Effect_Skill = new Object_Effect_Skill(ID, typeObject);
		object_Effect_Skill.setHP(0, Hp, 0);
		mVector2.addElement(object_Effect_Skill);
		MainSkill mainSkill = new MainSkill(-1, effBuff);
		mainSkill.setTypeBuff(typeBuff, effBuff, time);
		addSkillFire(mainSkill, mVector2);
	}

	public static MainObject getPet(short IDMain)
	{
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject.typeObject == 10 && mainObject.IDMainShiper == IDMain)
			{
				return mainObject;
			}
		}
		return null;
	}

	public void setXYPet(int x, int y)
	{
		this.x = x;
		this.y = y;
		toX = x;
		toY = y;
		dy = 0;
		Action = 0;
		f = 0;
	}

	public void getPosLast()
	{
		ObjMove objMove = GameScreen.getObjMove(ID, typeObject);
		if (objMove != null)
		{
			xLast = objMove.x;
			yLast = objMove.y;
		}
	}

	public void moveToLastPos()
	{
		if (xLast >= 0 && yLast >= 0)
		{
			if (getDistance(x, y, xLast, yLast) > 48)
			{
				GameScreen.addEffectEnd_ObjTo(56, 0, x, y, ID, typeObject, (sbyte)Dir, this);
				toX = xLast;
				toY = yLast;
				x = xLast;
				y = yLast;
			}
			xLast = -1;
			yLast = -1;
		}
	}

	public void addEffFashion()
	{
		typeEfffashion = 5;
		vecEffFashion.addElement(new Effect_End(165, this));
	}

	public static DataSkillEff getDataSkillEff(int id)
	{
		if (id == -1)
		{
			return null;
		}
		return (DataSkillEff)ALL_DATA_SKILL_EFF.get(id + string.Empty);
	}

	public void updateDataEff()
	{
		for (int i = 0; i < vecDataEff.size(); i++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(i);
			if (dataSkillEff != null)
			{
				dataSkillEff.update();
				if (dataSkillEff.wantDestroy)
				{
					vecDataEff.removeElementAt(i);
					i--;
				}
			}
		}
	}

	public void addDataEff(short id, int time)
	{
		for (int i = 0; i < vecDataEff.size(); i++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(i);
			if (dataSkillEff != null && dataSkillEff.idEff == id)
			{
				vecDataEff.removeElementAt(i);
			}
		}
		DataSkillEff o = new DataSkillEff(id, time);
		vecDataEff.addElement(o);
	}

	public void addDataEff(short id, int time, sbyte typemove, sbyte loop)
	{
		for (int i = 0; i < vecDataEff.size(); i++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(i);
			if (dataSkillEff != null && dataSkillEff.idEff == id)
			{
				vecDataEff.removeElementAt(i);
			}
		}
		DataSkillEff o = new DataSkillEff(id, time, typemove, loop);
		vecDataEff.addElement(o);
	}

	public void removeDataEff(short id)
	{
		for (int i = 0; i < vecDataEff.size(); i++)
		{
			DataSkillEff dataSkillEff = (DataSkillEff)vecDataEff.elementAt(i);
			if (dataSkillEff != null && dataSkillEff.idEff == id)
			{
				vecDataEff.removeElementAt(i);
			}
		}
	}

	public void setbody()
	{
		nFrameEffBody = 0;
		short num = checkPlayFrameBody();
		if (num != -1)
		{
			imgEffPlayBody = ObjectData.getImageAll(num, ObjectData.HashImageOtherNew, 23000);
		}
	}

	public void setLeg()
	{
		nFrameEffLeg = 0;
		short num = checkPlayFrameLeg();
		if (num != -1)
		{
			imgEffPlayLeg = ObjectData.getImageAll(num, ObjectData.HashImageOtherNew, 23000);
		}
	}

	public void setWeapon()
	{
		nFrameEffWeapon = 0;
		short num = checkPlayFrameWeapon();
		if (num != -1)
		{
			imgEffPlayWeapon = ObjectData.getImageAll(num, ObjectData.HashImageOtherNew, 23000);
		}
	}

	public virtual void setDataPet(short ID, short idImage, sbyte type)
	{
	}

	public virtual void setPetActionFire()
	{
	}

	public virtual void setNextSea()
	{
	}

	public virtual void setPosEvent()
	{
	}

	public virtual void loadInfoAgain(CatalogyMonster cata)
	{
	}

	public void setBodyBay(short id)
	{
		if (id == -1 || id >= 300)
		{
			idBodybay = id;
		}
		setbody();
	}

	public void setLegBay(short id)
	{
		if (id == -1 || id >= 300)
		{
			idLegBay = id;
		}
		setLeg();
	}

	public void setWeaponBay(short id)
	{
		if (id == -1 || id >= 300)
		{
			idWeaponBay = id;
		}
		setWeapon();
	}

	public void setHairBay(short id)
	{
		idHairbay = id;
		short num = checkEffHair();
		imgEffHair = null;
		if (num >= 0)
		{
			mSystem.outz("checkEffHair " + num);
			imgEffHair = ObjectData.getImageAll(num, ObjectData.HashImageOtherNew, 23000);
		}
	}

	public virtual bool isTru()
	{
		return false;
	}

	public void SetXlechEffectBongToi()
	{
		if (vecEffspec.size() == 0)
		{
			return;
		}
		mVector mVector2 = new mVector();
		for (int i = 0; i < vecEffspec.size(); i++)
		{
			Effect_Spec effect_Spec = (Effect_Spec)vecEffspec.elementAt(i);
			if (effect_Spec.typeEffect == 15)
			{
				mVector2.addElement(effect_Spec);
			}
		}
		if (mVector2.size() >= 2)
		{
			((Effect_Spec)vecEffspec.elementAt(0)).SetXlech(-7);
			((Effect_Spec)vecEffspec.elementAt(1)).SetXlech(7);
		}
		else if (mVector2.size() > 0)
		{
			((Effect_Spec)vecEffspec.elementAt(0)).SetXlech(0);
		}
	}
}
