using System;

public class Effect_Skill : MainEffect
{
	public const sbyte EFF_NORMAL = 0;

	public const short EFF_ZORO_1 = 154;

	public const short EFF_TASHIGI_2 = 155;

	public const sbyte EFF_LUFFY_S1_L1 = 21;

	public const sbyte EFF_LUFFY_S1_L2 = 33;

	public const short EFF_LUFFY_S1_L3 = 83;

	public const short EFF_LUFFY_S1_L4 = 180;

	public const short EFF_LUFFY_S1_L5 = 212;

	public const short EFF_LUFFY_S1_L6 = 271;

	public const sbyte EFF_LUFFY_S2_L1 = 34;

	public const sbyte EFF_LUFFY_S2_L2 = 35;

	public const short EFF_LUFFY_S2_L3 = 84;

	public const short EFF_LUFFY_S2_L4 = 181;

	public const short EFF_LUFFY_S2_L5 = 213;

	public const short EFF_LUFFY_S2_L6 = 272;

	public const sbyte EFF_LUFFY_S3_L1 = 1;

	public const sbyte EFF_LUFFY_S3_L2 = 37;

	public const short EFF_LUFFY_S3_L3 = 85;

	public const short EFF_LUFFY_S3_L4 = 182;

	public const short EFF_LUFFY_S3_L5 = 214;

	public const short EFF_LUFFY_S3_L6 = 273;

	public const short EFF_LUFFY_SEA_L1 = 133;

	public const short EFF_LUFFY_SEA_L2 = 134;

	public const short EFF_LUFFY_SEA_L3 = 135;

	public const sbyte EFF_ZORO_S1_L1 = 38;

	public const sbyte EFF_ZORO_S1_L2 = 15;

	public const short EFF_ZORO_S1_L3 = 86;

	public const short EFF_ZORO_S1_L4 = 183;

	public const short EFF_ZORO_S1_L5 = 215;

	public const short EFF_ZORO_S1_L6 = 281;

	public const sbyte EFF_ZORO_S2_L1 = 41;

	public const sbyte EFF_ZORO_S2_L2 = 29;

	public const short EFF_ZORO_S2_L3 = 87;

	public const short EFF_ZORO_S2_L4 = 184;

	public const short EFF_ZORO_S2_L5 = 216;

	public const short EFF_ZORO_S2_L6 = 282;

	public const sbyte EFF_ZORO_S3_L1 = 121;

	public const sbyte EFF_ZORO_S3_L2 = 122;

	public const short EFF_ZORO_S3_L3 = 123;

	public const short EFF_ZORO_S3_L4 = 185;

	public const short EFF_ZORO_S3_L5 = 217;

	public const short EFF_ZORO_S3_L6 = 283;

	public const sbyte EFF_ZORO_SEA_L1 = 42;

	public const sbyte EFF_ZORO_SEA_L2 = 43;

	public const sbyte EFF_ZORO_SEA_L3 = 19;

	public const sbyte EFF_SANJI_S1_L1 = 14;

	public const sbyte EFF_SANJI_S1_L2 = 44;

	public const short EFF_SANJI_S1_L3 = 124;

	public const short EFF_SANJI_S1_L4 = 186;

	public const short EFF_SANJI_S1_L5 = 218;

	public const short EFF_SANJI_S1_L6 = 291;

	public const sbyte EFF_SANJI_S2_L1 = 47;

	public const sbyte EFF_SANJI_S2_L2 = 48;

	public const short EFF_SANJI_S2_L3 = 125;

	public const short EFF_SANJI_S2_L4 = 187;

	public const short EFF_SANJI_S2_L5 = 219;

	public const short EFF_SANJI_S2_L6 = 292;

	public const sbyte EFF_SANJI_S3_L1 = 49;

	public const sbyte EFF_SANJI_S3_L2 = 50;

	public const short EFF_SANJI_S3_L3 = 12;

	public const short EFF_SANJI_S3_L4 = 188;

	public const short EFF_SANJI_S3_L5 = 220;

	public const short EFF_SANJI_S3_L6 = 293;

	public const short EFF_SANJI_SEA_L1 = 136;

	public const short EFF_SANJI_SEA_L2 = 137;

	public const short EFF_SANJI_SEA_L3 = 138;

	public const sbyte EFF_NAMI_S1_L1 = 16;

	public const sbyte EFF_NAMI_S1_L2 = 51;

	public const short EFF_NAMI_S1_L3 = 52;

	public const short EFF_NAMI_S1_L4 = 189;

	public const short EFF_NAMI_S1_L5 = 221;

	public const short EFF_NAMI_S1_L6 = 311;

	public const sbyte EFF_NAMI_S2_L1 = 9;

	public const sbyte EFF_NAMI_S2_L2 = 53;

	public const short EFF_NAMI_S2_L3 = 63;

	public const short EFF_NAMI_S2_L4 = 190;

	public const short EFF_NAMI_S2_L5 = 222;

	public const short EFF_NAMI_S2_L6 = 312;

	public const sbyte EFF_NAMI_S3_L1 = 31;

	public const sbyte EFF_NAMI_S3_L2 = 55;

	public const short EFF_NAMI_S3_L3 = 56;

	public const short EFF_NAMI_S3_L4 = 191;

	public const short EFF_NAMI_S3_L5 = 223;

	public const short EFF_NAMI_S3_L6 = 313;

	public const sbyte EFF_NAMI_SEA_L1 = 11;

	public const short EFF_NAMI_SEA_L2 = 139;

	public const short EFF_NAMI_SEA_L3 = 140;

	public const sbyte EFF_USSOP_S1_L1 = 57;

	public const sbyte EFF_USSOP_S1_L2 = 58;

	public const short EFF_USSOP_S1_L3 = 126;

	public const short EFF_USSOP_S1_L4 = 192;

	public const short EFF_USSOP_S1_L5 = 224;

	public const short EFF_USSOP_S1_L6 = 301;

	public const sbyte EFF_USSOP_S2_L1 = 64;

	public const sbyte EFF_USSOP_S2_L2 = 66;

	public const short EFF_USSOP_S2_L3 = 127;

	public const short EFF_USSOP_S2_L4 = 193;

	public const short EFF_USSOP_S2_L5 = 225;

	public const short EFF_USSOP_S2_L6 = 302;

	public const sbyte EFF_USSOP_S3_L1 = 67;

	public const sbyte EFF_USSOP_S3_L2 = 68;

	public const short EFF_USSOP_S3_L3 = 69;

	public const short EFF_USSOP_S3_L4 = 194;

	public const short EFF_USSOP_S3_L5 = 226;

	public const short EFF_USSOP_S3_L6 = 303;

	public const sbyte EFF_USSOP_SEA_L1 = 7;

	public const short EFF_USSOP_SEA_L2 = 141;

	public const short EFF_USSOP_SEA_L3 = 142;

	public const short EFF_LUFFY_S1_L3_OLD = 156;

	public const short EFF_LUFFY_S2_L3_OLD = 160;

	public const short EFF_ZORO_S1_L3_OLD = 157;

	public const short EFF_ZORO_S2_L3_SHORT_OLD = 161;

	public const short EFF_SANJI_S1_L3_OLD = 158;

	public const short EFF_SANJI_S2_L3_OLD = 162;

	public const short EFF_NAMI_S2_L3_OLD = 163;

	public const short EFF_USSOP_S1_L3_OLD = 159;

	public const sbyte EFF_BUFF = 46;

	public const short EFF_BUFF_2 = 165;

	public const short EFF_END_BUFF_2 = 166;

	public const short EFF_GET_MONEY = 17;

	public const short EFF_CAUSU_1 = 164;

	public const short EFF_CAUSU_1_L2 = 227;

	public const sbyte EFF_ACE_1 = 2;

	public const short EFF_ACE_1_L2 = 228;

	public const short EFF_ACE_1_L2_SUPER_1 = 259;

	public const short EFF_ACE_1_L2_SUPER_2 = 260;

	public const short EFF_ACE_1_L2_SUPER_3 = 261;

	public const sbyte EFF_ACE_2 = 3;

	public const short EFF_ACE_2_L2 = 229;

	public const short EFF_ACE_2_L2_SUPER_1 = 262;

	public const short EFF_ACE_2_L2_SUPER_2 = 263;

	public const short EFF_ACE_2_L2_SUPER_3 = 264;

	public const sbyte EFF_AOKIJI_1 = 4;

	public const short EFF_AOKIJI_1_L2 = 230;

	public const sbyte EFF_AOKIJI_2 = 5;

	public const short EFF_AOKIJI_2_L2 = 231;

	public const sbyte EFF_SMOKER_1 = 6;

	public const short EFF_SMOKER_1_L2 = 232;

	public const sbyte EFF_SMOKER_2 = 10;

	public const short EFF_SMOKER_2_L2 = 234;

	public const sbyte EFF_CROCODILE_1 = 25;

	public const short EFF_CROCODILE_1_L2 = 235;

	public const sbyte EFF_CROCODILE_2 = 26;

	public const short EFF_CROCODILE_2_L2 = 236;

	public const short EFF_SET_1 = 169;

	public const short EFF_SET_1_L2 = 237;

	public const short EFF_SET_2 = 170;

	public const short EFF_SET_2_L2 = 238;

	public const short EFF_NHAM_THACH_1 = 171;

	public const short EFF_NHAM_THACH_1_L2 = 239;

	public const short EFF_NHAM_THACH_2 = 172;

	public const short EFF_NHAM_THACH_2_L2 = 240;

	public const short EFF_PELL_1 = 179;

	public const short EFF_PELL_1_L2 = 241;

	public const short EFF_LUCCI_1 = 209;

	public const short EFF_LUCCI_1_L2 = 242;

	public const short EFF_DONG_DAT_1 = 210;

	public const short EFF_DONG_DAT_1_L2 = 243;

	public const short EFF_DONG_DAT_2 = 211;

	public const short EFF_DONG_DAT_2_L2 = 244;

	public const short EFF_MR5_1 = 233;

	public const short EFF_DAO_1 = 245;

	public const short EFF_DAO_1_L2 = 251;

	public const short EFF_SAP_1 = 246;

	public const short EFF_SAP_1_L2 = 253;

	public const short EFF_SAP_2 = 247;

	public const short EFF_SAP_2_L2 = 254;

	public const short EFF_KILO_1 = 248;

	public const short EFF_KILO_1_L2 = 255;

	public const short EFF_DAO_2 = 249;

	public const short EFF_DAO_2_L2 = 252;

	public const short EFF_RANKYAKU = 266;

	public const short EFF_SHIGAN = 267;

	public const short EFF_DOOR = 268;

	public const short EFF_DOOR_L2 = 269;

	public const short EFF_KUMADORI = 270;

	public const short EFF_XA_PHONG = 274;

	public const short EFF_XA_PHONG_L2 = 275;

	public const short EFF_SOI = 276;

	public const short EFF_SOI_L2 = 277;

	public const short EFF_HUOU = 278;

	public const short EFF_HUOU_L2 = 279;

	public const short EFF_GOAL = 280;

	public const short EFF_THUNDER_FALLS_1 = 1998;

	public const short EFF_THUNDER_FALLS_2 = 1999;

	public const short EFF_FIRE_EXPLORE = 2000;

	public const sbyte EFF_MON_1 = 30;

	public const sbyte EFF_MON_2 = 71;

	public const sbyte EFF_MON_3 = 72;

	public const sbyte EFF_MON_4 = 73;

	public const sbyte EFF_MON_5 = 74;

	public const sbyte EFF_MON_6 = 75;

	public const sbyte EFF_ALVIDA_1 = 76;

	public const sbyte EFF_ALVIDA_2 = 77;

	public const sbyte EFF_MON_7 = 78;

	public const sbyte EFF_MON_8 = 79;

	public const sbyte EFF_MON_9 = 80;

	public const sbyte EFF_MON_10 = 81;

	public const sbyte EFF_MON_11 = 82;

	public const sbyte EFF_MORGAN_1 = 88;

	public const sbyte EFF_MORGAN_2 = 89;

	public const sbyte EFF_HELMEPO_1 = 90;

	public const sbyte EFF_HELMEPO_2 = 91;

	public const sbyte EFF_MON_12 = 92;

	public const sbyte EFF_MOHJI_1 = 93;

	public const sbyte EFF_MOHJI_2 = 94;

	public const sbyte EFF_BUGGY_1 = 95;

	public const sbyte EFF_BUGGY_2 = 96;

	public const sbyte EFF_CABAJI_1 = 97;

	public const sbyte EFF_CABAJI_2 = 98;

	public const sbyte EFF_NYABAN_1 = 99;

	public const sbyte EFF_NYABAN_2 = 100;

	public const sbyte EFF_NYABAN_3 = 101;

	public const sbyte EFF_JANGO_1 = 102;

	public const sbyte EFF_KURO_1 = 103;

	public const sbyte EFF_KURO_2 = 104;

	public const sbyte EFF_PEARL_1 = 105;

	public const sbyte EFF_PEARL_2 = 106;

	public const sbyte EFF_GHIN_1 = 107;

	public const sbyte EFF_GHIN_2 = 108;

	public const sbyte EFF_DON_KRIEG_1 = 109;

	public const sbyte EFF_DON_KRIEG_2 = 110;

	public const sbyte EFF_DON_KRIEG_3 = 111;

	public const sbyte EFF_HACHI_1 = 112;

	public const sbyte EFF_HACHI_2 = 113;

	public const sbyte EFF_CHU_1 = 114;

	public const sbyte EFF_CHU_2 = 115;

	public const sbyte EFF_KUROBI_1 = 116;

	public const sbyte EFF_KUROBI_2 = 117;

	public const sbyte EFF_ARLONG_1 = 118;

	public const sbyte EFF_ARLONG_2 = 119;

	public const sbyte EFF_ARLONG_3 = 120;

	public const short EFF_MON_13 = 128;

	public const short EFF_MON_14 = 129;

	public const short EFF_MON_15 = 130;

	public const short EFF_MON_16 = 131;

	public const short EFF_MON_17 = 132;

	public const short EFF_MON_18 = 143;

	public const short EFF_MON_19 = 144;

	public const short EFF_MON_20 = 145;

	public const short EFF_MON_21 = 146;

	public const short EFF_MON_22 = 147;

	public const short EFF_MON_23 = 148;

	public const short EFF_MON_24 = 149;

	public const short EFF_MON_25 = 150;

	public const short EFF_MON_26 = 151;

	public const short EFF_MON_27 = 152;

	public const short EFF_MON_28 = 153;

	public const short EFF_MON_SMOKER_1 = 13;

	public const short EFF_MON_SMOKER_2 = 18;

	public const short EFF_MON_VALENTINE = 20;

	public const short EFF_MON_VALENTINE_2 = 22;

	public const short EFF_MON_MR5 = 23;

	public const short EFF_MON_MR5_2 = 24;

	public const short EFF_MON_CHESS = 27;

	public const short EFF_MON_KUROMARIMO = 28;

	public const short EFF_WAPOL_1 = 32;

	public const short EFF_WAPOL_2 = 36;

	public const short EFF_WAPOL_3 = 39;

	public const short EFF_WAPOL_4 = 40;

	public const short EFF_MR3_1 = 45;

	public const short EFF_MR3_2 = 54;

	public const short EFF_MISS_GOLDEN_WEEKEND_1 = 59;

	public const short EFF_MISS_GOLDEN_WEEKEND_2 = 60;

	public const short EFF_LAPIN = 61;

	public const short EFF_MON_29 = 62;

	public const sbyte EFF_MR4_1 = 65;

	public const sbyte EFF_MR4_2 = 70;

	public const short EFF_MISS_MS_1 = 167;

	public const short EFF_MR1_1 = 168;

	public const short EFF_MR1_2 = 173;

	public const short EFF_DF_1 = 174;

	public const short EFF_DF_2 = 175;

	public const short EFF_MR2_1 = 176;

	public const short EFF_MR2_2 = 177;

	public const short EFF_MR0_1 = 178;

	public const short EFF_ENEL_1 = 195;

	public const short EFF_ENEL_2 = 196;

	public const short EFF_ENEL_3 = 197;

	public const short EFF_SATORI_1 = 198;

	public const short EFF_SATORI_2 = 199;

	public const short EFF_OHM_1 = 200;

	public const short EFF_OHM_2 = 201;

	public const short EFF_GEDATSU_1 = 202;

	public const short EFF_GEDATSU_2 = 203;

	public const short EFF_SHURA_1 = 204;

	public const short EFF_SHURA_2 = 205;

	public const short EFF_LINH_TROI_1 = 206;

	public const short EFF_LINH_TROI_2 = 207;

	public const short EFF_TRU_1 = 208;

	public const short EFF_TRU_2_LAN = 250;

	public const short EFF_THA_DEN = 256;

	public const short EFF_THA_PHAO_HOA = 257;

	public const short EFF_SNOW_DOWN = 258;

	public const short EFF_LAW_HEART = 265;

	public const short EFF_PANTHEONG_1 = 10001;

	public const short EFF_PANTHEONG_2 = 10002;

	public const short EFF_GALIO_1 = 10003;

	public const short EFF_GALIO_2 = 10004;

	public const short EFF_NO_NANG_LUONG_1 = 10005;

	public const short EFF_NO_NANG_LUONG_2 = 10006;

	public const short EFF_NO_NANG_LUONG_3 = 10007;

	public const short EFF_NO_THEO_HUONG_1 = 10008;

	public const short EFF_NO_THEO_HUONG_2 = 10009;

	public const short EFF_XERATH_1 = 10010;

	public const short EFF_XERATH_2 = 10011;

	public const short EFF_XERATH_3 = 10012;

	public const short EFF_URGOT_1 = 10013;

	public const short EFF_URGOT_2 = 10014;

	public const short EFF_URGOT_3 = 10015;

	public const short EFF_URGOT_4 = 10016;

	public const short EFF_MONSTER_HUT_MAU = 10017;

	public const short EFF_MONSTER_CHAY_THANG_1 = 10018;

	public const short EFF_MONSTER_CHAY_THANG_2 = 10019;

	public const short EFF_MONSTER_GIAP_GAI = 10020;

	public const short EFF_MONSTER_HUT_MANA = 10021;

	public const short EFF_MONSTER_DANH_TRON_1 = 10022;

	public const short EFF_MONSTER_DANH_TRON_2 = 10023;

	public const short EFF_MONSTER_NEM_BOOM_1 = 10024;

	public const short EFF_MONSTER_NEM_BOOM_2 = 10025;

	public const short EFF_MONSTER_KHONG_DANH_1 = 10026;

	public const short EFF_MONSTER_KHONG_DANH_2 = 10027;

	public const short EFF_TRAI_AC_QUY_HO_DEN_VU_TRU = 10028;

	public const short EFF_BLACK_HOLE_1 = 400;

	public const short EFF_TRAI_AC_QUY_HO_DEN_VU_TRU_3 = 10030;

	public const short EFF_BLACK_HOLE_2 = 401;

	public const short EFF_BLACK_HOLE_1_LV5 = 402;

	public const short EFF_BLACK_HOLE_2_LV5 = 403;

	public const sbyte EFF_DAN_FOCUS = -1;

	public int[][][] skillZoro3 = new int[4][][]
	{
		new int[2][]
		{
			new int[3] { 4, -26, -23 },
			new int[3] { 1, -10, 20 }
		},
		new int[4][]
		{
			new int[3] { 3, -15, -43 },
			new int[3] { 1, 5, -26 },
			new int[3] { 3, -28, -4 },
			new int[3] { 4, -28, 10 }
		},
		new int[5][]
		{
			new int[3] { 0, -27, -90 },
			new int[3] { 3, -27, -76 },
			new int[3] { 4, -22, -58 },
			new int[3] { 2, -10, -30 },
			new int[3] { 1, 0, -14 }
		},
		new int[4][]
		{
			new int[3] { 3, -44, -70 },
			new int[3] { 0, -44, -45 },
			new int[3] { 4, -36, -21 },
			new int[3] { 3, -24, 0 }
		}
	};

	public int[][] Mr1 = new int[6][]
	{
		new int[3] { 3, -15, -35 },
		new int[3] { 4, -25, -32 },
		new int[3] { 5, -25, -27 },
		new int[3] { 0, -30, -20 },
		new int[3] { 1, -30, -20 },
		new int[3] { 2, -30, -20 }
	};

	public int[][] speedEff;

	public int subType;

	public int waitTick;

	public int indexObjBefire;

	public int xArchor;

	public int yArchor;

	public int[][] plusxy;

	public int[][] mframeSuper;

	public int fPlayFrameSuper;

	public int fPre;

	public FrameImage[] mImgframe;

	public bool isAddSound;

	public bool isaddEff;

	public MainObject objBeFireMain;

	public mVector VecEff = new mVector();

	public mVector VecSubEff = new mVector();

	public mVector vecPos = new mVector();

	public mVector vectargetPos = new mVector();

	public int[] xLech;

	public int[] yLech;

	public short[] posSmock = new short[7] { 0, 50, 75, 100, 20, 110, 30 };

	private int vXTam;

	private int vYTam;

	private new int x1000;

	private new int y1000;

	private int vX1000;

	private int vY1000;

	private int xEff;

	private int yEff;

	private int angle;

	private int R;

	private int size1;

	private int[][] mTamgiac;

	private int lT_Arc;

	private int gocT_Arc;

	private int r;

	public int[] radian = new int[12]
	{
		0, 30, 60, 90, 120, 150, 180, 210, 240, 270,
		300, 330
	};

	public int CR = 15;

	public int Ctick;

	public int t2;

	private int disHard;

	private int tickadd;

	private Point_Focus rocket1;

	private Point_Focus rocket2;

	private int frame1;

	private int frame2;

	private int xLight1;

	private int xLight2;

	private int giatocFly;

	private int fSpeedTest;

	public const short EFF_SPEC_CRI = 1010;

	public const short EFF_SPEC_GIAP = 1013;

	public const short EFF_SPEC_PHAN = 1014;

	public const short EFF_SPEC_HUT_HP = 1021;

	public const short EFF_SPEC_HUT_MP = 1022;

	public const short EFF_SPEC_HAP_THU = 1058;

	public Effect_Skill(MainSkill skill, MainObject objEffFire, int x, int y, mVector vec)
	{
		indexObjBefire = 0;
		valueEffect = 0;
		if (LoadMapScreen.isNextMap)
		{
			base.x = x;
			base.y = y;
			vecPos = vec;
			Dir = skill.Dirbuff;
			if (vecPos != null && vecPos.size() > 0)
			{
				Point_Focus point_Focus = (Point_Focus)vecPos.elementAt(0);
				toX = point_Focus.x;
				toY = point_Focus.y;
			}
			else
			{
				toX = x;
				toY = y;
			}
			timeAddNum = -1;
			objBeFireMain = objEffFire;
			isStop = false;
			isRemove = false;
			f = -1;
			ysai = 0;
			typeEffect = skill.typeEffSkill;
			subType = skill.typeEffBuff;
			timeBegin = skill.timeBegin;
			timeEnd = skill.timebuff;
			objFireMain = objEffFire;
			isEff = true;
			numNextFrame = 1;
		}
	}

	public Effect_Skill(MainSkill skill, MainObject objEffFire)
	{
		indexObjBefire = 0;
		valueEffect = 0;
		if (LoadMapScreen.isNextMap)
		{
			timeAddNum = -1;
			objBeFireMain = objEffFire;
			isStop = false;
			isRemove = false;
			f = -1;
			ysai = 0;
			typeEffect = skill.typeEffSkill;
			subType = skill.typeEffBuff;
			timeBegin = skill.timeBegin;
			objFireMain = objEffFire;
			isEff = true;
			numNextFrame = 1;
		}
	}

	public Effect_Skill()
	{
	}

	public Effect_Skill(int typeKill, int subtype, MainObject objEffFire, mVector vec)
	{
		indexObjBefire = 0;
		valueEffect = 0;
		if (!LoadMapScreen.isNextMap)
		{
			return;
		}
		timeAddNum = -1;
		objBeFireMain = null;
		subType = subtype;
		isStop = false;
		isRemove = false;
		if (vec == null || vec.size() == 0)
		{
			return;
		}
		vecObjsBeFire = vec;
		f = -1;
		ysai = 0;
		typeEffect = typeKill;
		timeBegin = GameCanvas.timeNow;
		objFireMain = objEffFire;
		Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(0);
		if (object_Effect_Skill != null)
		{
			objBeFireMain = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
		}
		if (objBeFireMain != null && objFireMain != null)
		{
			isEff = false;
			if (objFireMain == GameScreen.player && LoadMap.specMap != 3)
			{
				isEff = true;
			}
			numNextFrame = 1;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			toX = objBeFireMain.x;
			toY = objBeFireMain.y - objBeFireMain.hOne / 2;
			if (objFireMain != objBeFireMain)
			{
				setAngle();
				objFireMain.type_left_right = Dir;
				objFireMain.Dir = Dir;
			}
		}
	}

	public Effect_Skill(int typeKill, int subtype, MainObject objEffFire, mVector vec, int x, int y)
	{
		indexObjBefire = 0;
		valueEffect = 0;
		if (!LoadMapScreen.isNextMap)
		{
			return;
		}
		timeAddNum = -1;
		objBeFireMain = null;
		subType = subtype;
		isStop = false;
		isRemove = false;
		if (vec == null || vec.size() == 0)
		{
			return;
		}
		vecObjsBeFire = vec;
		f = -1;
		ysai = 0;
		typeEffect = typeKill;
		timeBegin = GameCanvas.timeNow;
		objFireMain = objEffFire;
		Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(0);
		if (object_Effect_Skill != null)
		{
			objBeFireMain = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
		}
		if (objBeFireMain != null && objFireMain != null)
		{
			isEff = false;
			if (objFireMain == GameScreen.player && LoadMap.specMap != 3)
			{
				isEff = true;
			}
			numNextFrame = 1;
			base.x = x;
			base.y = y;
			toX = objBeFireMain.x;
			toY = objBeFireMain.y - objBeFireMain.hOne / 2;
			if (objFireMain != objBeFireMain)
			{
				setAngle();
				objFireMain.type_left_right = Dir;
				objFireMain.Dir = Dir;
			}
		}
	}

	private void createEffFireExplore()
	{
		if (isAddSound)
		{
			mSound.playSound(43, mSound.volumeSound);
		}
		indexImg = 0;
		fraImgEff = new FrameImage(mImage.createImage("/test_eff/skill2/eff.png"), 18);
		x = GameScreen.player.x;
		y = GameScreen.player.y;
		Player.isBlock = true;
	}

	private void createEffThunderFalls()
	{
		indexImg = 0;
		fImg = -1;
		fSub = (fSub2 = (fSub3 = -1));
		fRemove = 24;
		fraImgEff = new FrameImage(mImage.createImage("/test_eff/skill1/eff1.png"), 4);
		fraImgSubEff = new FrameImage(mImage.createImage("/test_eff/skill1/eff2.png"), 4);
		fraImgSub2Eff = new FrameImage(mImage.createImage("/test_eff/skill1/eff3.png"), 4);
		x = objBeFireMain.x;
		y = objBeFireMain.y;
		xLech = new int[4] { -50, 70, 55, -30 };
		yLech = new int[4] { -30, -20, 30, 20 };
		if (isAddSound)
		{
			mSound.playSound(17, mSound.volumeSound);
		}
	}

	private void updateEffFireExplore()
	{
		if (f % 3 == 0 && !isPauseImgEff)
		{
			fImg++;
		}
		if (fImg > 18)
		{
			fImg = 0;
			indexImg++;
		}
		if (indexImg > 0)
		{
			Player.isBlock = false;
			removeEff();
		}
	}

	private void updateEffThunderFall()
	{
		x = objBeFireMain.x;
		y = objBeFireMain.y;
		for (int i = 0; i < xLech.Length; i++)
		{
			if (GameCanvas.loadmap.getTile(x + xLech[i], y + yLech[i]) == 1)
			{
				yLech[i] = -yLech[i] + CRes.random(10, 20);
			}
		}
		if (indexImg < 5)
		{
			if (f % 3 == 0 && !isPauseImgEff)
			{
				fImg++;
			}
			if (fImg > 3)
			{
				timeStartPauseImgEff = mSystem.currentTimeMillis();
				isPauseImgEff = true;
				fImg = 0;
				indexImg++;
			}
			if (isPauseImgEff)
			{
				GameScreen.addEffectEnd(63, 0, x, y, Dir, objMainEff);
				GameScreen.addEffectEnd(59, 0, x, y, Dir, objMainEff);
				if (mSystem.currentTimeMillis() - timeStartPauseImgEff >= 300)
				{
					isPauseImgEff = false;
				}
			}
		}
		if (indexSub < 4)
		{
			if (f % 3 == 0 && !isPauseSubEff && indexImg > 0)
			{
				fSub++;
			}
			if (fSub > 3)
			{
				timeStartPauseSubEff = mSystem.currentTimeMillis();
				isPauseSubEff = true;
				fSub = 0;
				indexSub++;
			}
			if (isPauseSubEff && mSystem.currentTimeMillis() - timeStartPauseSubEff >= 300)
			{
				isPauseSubEff = false;
			}
		}
		if (indexSub2 < 4)
		{
			if (f % 3 == 0 && !isPauseSub2Eff && indexImg > 0)
			{
				fSub2++;
			}
			if (fSub2 > 3)
			{
				timeStartPauseSub2Eff = mSystem.currentTimeMillis();
				isPauseSub2Eff = true;
				fSub2 = 0;
				indexSub2++;
			}
			if (isPauseSub2Eff && mSystem.currentTimeMillis() - timeStartPauseSub2Eff >= 300)
			{
				isPauseSub2Eff = false;
			}
		}
		if (indexImg == 5 && indexSub == 4 && indexSub2 == 4)
		{
			removeEff();
		}
	}

	public override bool CreateEffectSkill()
	{
		if (!GameMidlet.isTest && (objFireMain == null || objBeFireMain == null || objFireMain.returnAction() || objBeFireMain.returnAction()))
		{
			return false;
		}
		if (objFireMain == GameScreen.player || CRes.random(3) == 0)
		{
			isAddSound = true;
		}
		objMainEff = objFireMain;
		am_duong = -1;
		if (GameCanvas.lowGraphic && objFireMain != GameScreen.player)
		{
			if (MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, objFireMain.x, objFireMain.y) > 120)
			{
				removeEff();
				return true;
			}
			if (GameMidlet.DEVICE == 0 && GameScreen.vecObjFire.size() > 30)
			{
				removeEff();
				return true;
			}
		}
		if (Dir == 2)
		{
			am_duong = 1;
		}
		switch (typeEffect)
		{
		case 2000:
			createEffFireExplore();
			break;
		case 1998:
		case 1999:
			createEffThunderFalls();
			break;
		case 0:
			createNormal();
			break;
		case 1:
		case 37:
			createLuffy1();
			break;
		case 47:
		case 48:
			createSanji1();
			break;
		case 154:
			createZoro1();
			break;
		case 155:
			createZoro2();
			break;
		case 7:
			createUssopSea1();
			break;
		case 141:
			createUssopSea2();
			break;
		case 142:
			createUssopSea3();
			break;
		case 57:
		case 64:
		case 66:
		case 206:
		case 207:
			createUssop2();
			break;
		case 127:
		case 193:
		case 225:
		case 302:
			createUssopS2_Le_New();
			break;
		case 58:
			createUssopSkill1_Lv3();
			break;
		case 159:
			createUssopSkill1_Lv3_New();
			break;
		case 126:
		case 192:
			createUssopSkill1_Lv3_SHORT();
			break;
		case 224:
		case 301:
			createUssopS1_L5();
			break;
		case 9:
		case 53:
		case 163:
			createNami1();
			break;
		case 63:
		case 190:
		case 222:
		case 312:
			createNami1_SHORT();
			break;
		case 11:
			fRemove = 15;
			createNamiSea1_2();
			break;
		case 139:
			fRemove = 20;
			createNamiSea1_2();
			break;
		case 140:
			fRemove = 40;
			createNamiSea3();
			break;
		case 12:
		case 49:
		case 50:
		case 188:
		case 220:
		case 293:
			createSanji2();
			break;
		case 266:
			createRankyaku();
			break;
		case 276:
		case 277:
			createSoi();
			break;
		case 278:
		case 279:
			createHuou();
			break;
		case 267:
			createShigan();
			break;
		case 268:
		case 269:
			createDoor();
			break;
		case 14:
		case 44:
			fRemove = 9;
			break;
		case 15:
		case 38:
			createZoro3();
			break;
		case 16:
		case 51:
			createNamiSkill1();
			break;
		case 52:
		case 189:
		case 221:
		case 311:
			createNamiSkill1_L3();
			break;
		case 19:
			createZoro4();
			break;
		case 42:
			fRemove = 15;
			createZoroSkill3_Lv1();
			break;
		case 43:
			fRemove = 20;
			createZoroSkill3_Lv1();
			break;
		case 21:
		case 33:
		case 176:
			fRemove = 8;
			break;
		case 34:
		case 35:
			createLuffy6();
			break;
		case 41:
			createZoro_S2_L1_New();
			break;
		case 29:
			createZoro8();
			break;
		case 31:
		case 55:
		case 56:
		case 191:
		case 223:
		case 313:
			createNamiSkill3();
			break;
		case 46:
			addSoundBuff();
			objFireMain.toX = objFireMain.x;
			objFireMain.toY = objFireMain.y;
			if (objFireMain.posTransRoad != null)
			{
				objFireMain.posTransRoad = null;
			}
			GameScreen.addEffectEnd(85, 0, x, y, 500, Dir, objMainEff);
			isEff = true;
			fRemove = 1;
			return true;
		case 67:
		case 68:
		case 69:
		case 194:
		case 226:
			createPhaohoa();
			break;
		case 303:
			createPhaohoa_L6();
			break;
		case 164:
		case 227:
			createCausu_1();
			break;
		case 30:
			createMon_1();
			break;
		case 71:
		case 145:
		case 146:
		case 147:
		case 148:
			createMon2();
			break;
		case 72:
		case 92:
			createMon3();
			break;
		case 73:
		case 74:
			createMon_4_5();
			break;
		case 75:
			createMon6();
			break;
		case 76:
			createAlvida1();
			break;
		case 77:
			createAlvida2();
			break;
		case 81:
		case 143:
		case 149:
			createMon_10();
			break;
		case 82:
		case 144:
			createMon_11();
			break;
		case 156:
			fRemove = 33;
			break;
		case 83:
			fRemove = 16;
			if (objFireMain.type_left_right == 0)
			{
				Dir = 0;
			}
			else
			{
				Dir = 2;
			}
			break;
		case 180:
		case 212:
			fRemove = 20;
			if (objFireMain.type_left_right == 0)
			{
				Dir = 0;
			}
			else
			{
				Dir = 2;
			}
			if (typeEffect == 212)
			{
				fraImgEff = new FrameImage(61, 24, 30);
			}
			break;
		case 271:
			fRemove = 15;
			if (objFireMain.type_left_right == 0)
			{
				Dir = 0;
			}
			else
			{
				Dir = 2;
			}
			fraImgEff = new FrameImage(61, 24, 30);
			break;
		case 274:
		case 275:
			fRemove = 15;
			fraImgEff = new FrameImage(431, 2);
			break;
		case 160:
			createLuffy_New2();
			break;
		case 84:
		case 181:
		case 213:
		case 272:
			createLuffy_New2_SHORT();
			break;
		case 85:
		case 182:
		case 214:
		case 273:
			createLuffy_New3();
			break;
		case 157:
			createZoro_New1();
			break;
		case 86:
		case 183:
		case 215:
			createZoro_S1_L3_SHORT();
			break;
		case 281:
			createZoro_S1_L6();
			break;
		case 87:
		case 184:
		case 216:
			createZoro_New2();
			break;
		case 282:
			fRemove = 34;
			vMax = 12;
			fraImgEff = new FrameImage(413, 91, 73);
			if (isAddSound)
			{
				mSound.playSound(8, mSound.volumeSound);
			}
			GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
			fraImgSubEff = new FrameImage(415, 3);
			break;
		case 161:
			createZoro_New2_SHORT();
			break;
		case 88:
			GameScreen.addEffectEnd(30, 0, x, y - 30, 300, Dir, objMainEff);
			fRemove = 8;
			addSound(3);
			break;
		case 89:
			createMorgan_2();
			break;
		case 90:
		case 91:
			fRemove = 1;
			break;
		case 93:
			toY = objBeFireMain.y;
			fRemove = 32;
			fraImgEff = new FrameImage(8, 40, 47, 40, 47);
			break;
		case 94:
			createMohji_2();
			break;
		case 95:
			createBuggy_1();
			break;
		case 96:
			createBuggy_2();
			break;
		case 97:
			createCabaji_1();
			break;
		case 22:
		case 98:
			createCabaji_2();
			break;
		case 248:
		case 255:
			createKilo_1();
			break;
		case 99:
			createNyaban_1();
			break;
		case 100:
			createNyaban_2();
			break;
		case 101:
			createNyaban_3();
			break;
		case 102:
			createJango_1();
			break;
		case 103:
			createKuro_1();
			break;
		case 104:
			createKuro_2();
			break;
		case 105:
			createPearl_1();
			break;
		case 106:
			createPearl_2();
			break;
		case 65:
		case 70:
		case 107:
			createGhin_1();
			break;
		case 108:
			createGhin_2();
			break;
		case 109:
			createDonKrieg_1();
			break;
		case 110:
			createDonKrieg_2();
			break;
		case 111:
			createDonKrieg_3();
			break;
		case 112:
			numNextFrame = 2;
			fraImgEff = new FrameImage(140, 70, 70);
			if (Dir == 0)
			{
				x -= 20;
			}
			else
			{
				x += 20;
			}
			fRemove = 15;
			break;
		case 270:
			numNextFrame = 2;
			fraImgEff = new FrameImage(427, 4);
			if (Dir == 0)
			{
				x -= 20;
			}
			else
			{
				x += 20;
			}
			fRemove = 15;
			break;
		case 113:
		case 150:
		case 151:
		case 152:
		case 153:
			createHachi_2();
			break;
		case 114:
			createChu_1();
			break;
		case 115:
			createChu_2();
			break;
		case 116:
			createKurobi_1();
			break;
		case 117:
			createKurobi_2();
			break;
		case 118:
			createArlong_1();
			break;
		case 119:
			createArlong_2();
			break;
		case 120:
			createArlong_3();
			break;
		case 121:
			createZoroS3_L1_New();
			break;
		case 122:
			createZoroS3_L2_New();
			break;
		case 123:
		case 185:
		case 217:
		case 283:
			createZoroLv3();
			break;
		case 158:
		case 177:
			createSanji_s1_l3_New();
			break;
		case 124:
		case 186:
		case 218:
			createSanji_s1_l3_SHORT();
			break;
		case 162:
			createSanji_s2_l3_New();
			break;
		case 125:
		case 187:
			createSanji_s2_l3_New_SHORT();
			break;
		case 131:
			fRemove = 6;
			break;
		case 132:
			fRemove = 10;
			break;
		case 133:
			fraImgEff = new FrameImage(193, 25, 15);
			fraImgSubEff = new FrameImage(68, 28, 44);
			fRemove = 15;
			vMax = 18;
			break;
		case 134:
		case 135:
			fraImgEff = new FrameImage(193, 25, 15);
			fraImgSubEff = new FrameImage(68, 28, 44);
			fraImgSub2Eff = new FrameImage(194, 48, 34, 1);
			if (typeEffect == 135)
			{
				fraImgSub3Eff = new FrameImage(30, 38, 38);
			}
			fRemove = 20;
			vMax = 18;
			break;
		case 136:
			fraImgEff = new FrameImage(183, 20, 54);
			fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
			fRemove = 15;
			y = objFireMain.y;
			break;
		case 137:
		case 138:
			fraImgEff = new FrameImage(183, 20, 54);
			fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
			fRemove = 20;
			y = objFireMain.y;
			break;
		case 2:
		case 228:
		case 259:
		case 260:
		case 261:
			create_Devil_FIRE1();
			break;
		case 10030:
			create_ho_den_vu_tru();
			break;
		case 3:
		case 229:
		case 262:
		case 263:
		case 264:
			create_Devil_FIRE2();
			break;
		case 4:
		case 230:
			create_Devil_ICE1();
			break;
		case 5:
		case 231:
			create_Devil_ICE2();
			break;
		case 6:
		case 232:
			create_Devil_Smoker1();
			break;
		case 10:
		case 234:
			create_Devil_Smoker2();
			break;
		case 13:
		case 258:
			createSmoker1();
			break;
		case 280:
		{
			fraImgEff = new FrameImage(mImage.createImage("/eff/khungthanh.png"), 1);
			fraImgSubEff = new FrameImage(mImage.createImage("/eff/ball.png"), 4);
			x = objFireMain.x;
			y = objFireMain.y;
			toX = x + 200;
			toY = y - 30;
			vx = 5;
			vy = -1;
			vMax = 10;
			int xdich2 = toX - x;
			int ydich2 = toY - y - CRes.random(20);
			create_Speed(xdich2, ydich2, null);
			fRemove = 40;
			objFireMain.Dir = 2;
			break;
		}
		case 18:
			createSmoker2();
			break;
		case 78:
			fRemove = 6;
			break;
		case 79:
			fRemove = 8;
			break;
		case 24:
		case 80:
			fRemove = 14;
			break;
		case 128:
			fRemove = 10;
			break;
		case 129:
		case 130:
			fRemove = 16;
			break;
		case 10001:
			fraImgEff = new FrameImage(173, 70, 42, 50, 30);
			fraImgSubEff = new FrameImage(172, 60, 43);
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			levelPaint = -1;
			break;
		case 10002:
			fraImgEff = new FrameImage(76, 32, 70);
			fraImgSubEff = new FrameImage(129, 40, 80);
			x = objFireMain.x;
			y = objFireMain.y;
			fRemove = 22;
			break;
		case 10003:
			fraImgEff = new FrameImage(77, 64, 75, 43, 50);
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 4;
			break;
		case 10004:
			fraImgEff = new FrameImage(174, 40, 40);
			fraImgSubEff = new FrameImage(26, 40, 40);
			x = objFireMain.x;
			y = objFireMain.y;
			fRemove = 30;
			break;
		case 10005:
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			break;
		case 10006:
		case 10011:
			fraImgSubEff = new FrameImage(172, 60, 43);
			levelPaint = -1;
			break;
		case 10007:
			fraImgEff = new FrameImage(118, 62, 64, 47, 48);
			fraImgSubEff = new FrameImage(173, 70, 42, 50, 30);
			break;
		case 10009:
			objFireMain.Dir = Dir;
			fRemove = 30;
			break;
		case 10008:
			levelPaint = -1;
			fraImgEff = new FrameImage(175, 13, 11);
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			break;
		case 10010:
		case 10013:
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			fraImgEff = new FrameImage(178, 70, 65);
			numNextFrame = 2;
			break;
		case 10012:
			createXerath3();
			break;
		case 10015:
			createUrgot3();
			break;
		case 10017:
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 4;
			fraImgEff = new FrameImage(180, 32, 63);
			numNextFrame = 3;
			break;
		case 10018:
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y;
			fraImgEff = new FrameImage(8, 40, 47, 40, 47);
			break;
		case 10019:
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y;
			fRemove = 8;
			break;
		case 10020:
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			fraImgEff = new FrameImage(189, 37, 62);
			numNextFrame = 3;
			levelPaint = -1;
			break;
		case 10021:
		case 10022:
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			fraImgEff = new FrameImage(181, 47, 63, 38, 51);
			numNextFrame = 3;
			levelPaint = -1;
			break;
		case 10023:
			fRemove = 4;
			break;
		case 10024:
			setAngle();
			objFireMain.Dir = Dir;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			fraImgEff = new FrameImage(181, 47, 63, 38, 51);
			fraImgSubEff = new FrameImage(172, 60, 43);
			numNextFrame = 3;
			levelPaint = -1;
			break;
		case 10025:
			setAngle();
			objFireMain.Dir = Dir;
			createMonster_NEM_BOOM_2();
			break;
		case 10026:
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			fraImgEff = new FrameImage(182, 56, 80, 40, 57);
			numNextFrame = 2;
			levelPaint = -1;
			break;
		case 10027:
		{
			for (int m = 0; m < vecPos.size(); m++)
			{
				Point_Focus point_Focus = (Point_Focus)vecPos.elementAt(m);
				GameScreen.addEffectEnd_ObjTo(22, 0, point_Focus.x, point_Focus.y - 30, objFireMain.ID, objFireMain.typeObject, (sbyte)objFireMain.Dir, objMainEff);
			}
			fRemove = 10;
			break;
		}
		case 165:
			addSoundBuff();
			isEff = true;
			Dir = (sbyte)objFireMain.type_left_right;
			addSoundBuff();
			GameScreen.addEffectEnd(85, 0, x, y, 900, Dir, objMainEff);
			GameScreen.addEffectEnd(85, 0, x, y, 900, Dir, objMainEff);
			y = objFireMain.y;
			objFireMain.toX = objFireMain.x;
			objFireMain.toY = objFireMain.y;
			fraImgEff = new FrameImage(101, 40, 47);
			fRemove = 40;
			return true;
		case 166:
			isEff = true;
			Dir = (sbyte)objFireMain.type_left_right;
			addSoundBuff();
			y = objFireMain.y;
			objFireMain.toX = objFireMain.x;
			objFireMain.toY = objFireMain.y;
			fraImgEff = new FrameImage(101, 40, 47);
			fRemove = 20;
			return true;
		case 17:
			isEff = true;
			Dir = (sbyte)objFireMain.type_left_right;
			addSoundBuff();
			y = objFireMain.y;
			objFireMain.toX = objFireMain.x;
			objFireMain.toY = objFireMain.y;
			fraImgEff = new FrameImage(101, 40, 47);
			fRemove = 20;
			return true;
		case 20:
			fRemove = 24;
			levelPaint = -1;
			fraImgEff = new FrameImage(171, 153, 84, 100, 54);
			GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2, 600, Dir, objMainEff);
			y = objFireMain.y + 20;
			break;
		case 23:
		{
			fRemove = 3;
			fraImgEff = new FrameImage(20, 10, 10);
			vMax = 18;
			y -= 5;
			if (Dir == 0)
			{
				x -= 10;
			}
			else
			{
				x += 10;
			}
			int xdich = toX - x;
			int ydich = toY - y;
			create_Speed(xdich, ydich, null);
			GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
			fPlayFrameSuper = fRemove;
			if (fRemove < 5)
			{
				fRemove = 5;
			}
			break;
		}
		case 25:
		case 235:
			create_Crocodile_1();
			break;
		case 26:
		case 236:
			addVir(10, 5, 10, isPlayer: true);
			fRemove = 20;
			fraImgEff = new FrameImage(99, 32, 32);
			y = objFireMain.y;
			if (isAddSound)
			{
				addSoundBuff();
			}
			break;
		case 27:
			createChess();
			break;
		case 28:
			createKuromarimo();
			break;
		case 32:
			createWapol();
			break;
		case 36:
			createWapol2();
			break;
		case 39:
			createWapol3();
			break;
		case 40:
			create_Wapol4();
			break;
		case 45:
			createMr3_1();
			break;
		case 54:
			createMr3_2();
			break;
		case 59:
		case 60:
			createMissGold_1();
			break;
		case 61:
			createLapin();
			break;
		case 62:
			createMon29();
			break;
		case 167:
			fraImgEff = new FrameImage(152, 25, 21);
			fraImgSubEff = new FrameImage(201, 64, 50, 45, 35);
			fraImgSub2Eff = new FrameImage(217, 39, 18);
			fraImgSub3Eff = new FrameImage(92, 64, 126, 45, 89, 1);
			fraImgSub4Eff = new FrameImage(218, 64, 64);
			x = objFireMain.x;
			y = objFireMain.y + 10;
			fRemove = 30;
			x1000 = x;
			y1000 = y;
			levelPaint = -1;
			break;
		case -1:
			fRemove = 60;
			fraImgEff = new FrameImage(mImage.createImage("/eff/n1.png"), 14, 15);
			fraImgSubEff = new FrameImage(mImage.createImage("/eff/n1.png"), 14, 15);
			vMax = 16000;
			createDanFocus();
			frame = setFrameAngle(gocT_Arc);
			break;
		case 401:
			levelPaint = 1;
			x = objBeFireMain.x;
			y = objBeFireMain.y;
			GameScreen.addHightDataeff(4, x, y);
			GameScreen.addEffectEnd(63, 0, x, y, Dir, objMainEff);
			GameScreen.addEffectEnd(110, 0, x, y, Dir, objMainEff);
			LoadMap.timeVibrateScreen = CRes.random(1, 5);
			removeEff();
			break;
		case 403:
			levelPaint = 1;
			x = objBeFireMain.x;
			y = objBeFireMain.y;
			GameScreen.addHightDataeff(8, x, y);
			GameScreen.addEffectEnd(63, 0, x, y, Dir, objMainEff);
			GameScreen.addEffectEnd(110, 0, x, y, Dir, objMainEff);
			LoadMap.timeVibrateScreen = CRes.random(1, 5);
			removeEff();
			break;
		case 400:
		{
			for (int l = 0; l < vecObjsBeFire.size(); l++)
			{
				Object_Effect_Skill object_Effect_Skill3 = (Object_Effect_Skill)vecObjsBeFire.elementAt(l);
				if (object_Effect_Skill3 != null)
				{
					MainObject mainObject2 = MainObject.get_Object(object_Effect_Skill3.ID, object_Effect_Skill3.tem);
					if (mainObject2 != null)
					{
						GameScreen.addHightDataeff(2, mainObject2.x, mainObject2.y);
						LoadMap.timeVibrateScreen = CRes.random(6, 20);
						GameScreen.addEffectEnd(112, 0, mainObject2.x, mainObject2.y, Dir, objMainEff);
						VecSubEff.addElement(new Point(mainObject2.x, mainObject2.y));
					}
				}
			}
			break;
		}
		case 402:
		{
			VecSubEff = new mVector();
			for (int k = 0; k < vecObjsBeFire.size(); k++)
			{
				Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
				if (object_Effect_Skill2 != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill2.ID, object_Effect_Skill2.tem);
					if (mainObject != null)
					{
						GameScreen.addHightDataeff(2, mainObject.x, mainObject.y);
						LoadMap.timeVibrateScreen = CRes.random(6, 20);
						GameScreen.addEffectEnd(112, 0, mainObject.x, mainObject.y, Dir, objMainEff);
						VecSubEff.addElement(new Point(mainObject.x, mainObject.y));
					}
				}
			}
			frameSuper = 4;
			fraImgEff = new FrameImage(32, 45, 45, (sbyte)5, frameSuper);
			fRemove = 30;
			vMax = 12;
			y = objFireMain.y;
			break;
		}
		case 10028:
		{
			fraImgSub3Eff = new FrameImage(242, 49, 28, 2);
			fRemove = 33;
			x = objBeFireMain.x;
			y = objBeFireMain.y;
			y1000 = 240;
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					GameScreen.addEffectSkill2(-1, objFireMain, object_Effect_Skill, x + posSmock[CRes.random(posSmock.Length - 1)], y - 200 + CRes.random_Am(-10, 10));
					GameScreen.addEffectSkill2(-1, objFireMain, object_Effect_Skill, x + posSmock[CRes.random(posSmock.Length - 1)], y - 200 + CRes.random_Am(-10, 10));
					GameScreen.addEffectSkill2(-1, objFireMain, object_Effect_Skill, x + posSmock[CRes.random(posSmock.Length - 1)], y - 200 + CRes.random_Am(-10, 10));
				}
			}
			GameScreen.addEffectEnd(112, 0, x, y + 10, Dir, objMainEff);
			break;
		}
		case 169:
		case 237:
			fraImgEff = new FrameImage(240, 30, 73, 1);
			fraImgSubEff = new FrameImage(241, 40, 27, 2);
			fraImgSub2Eff = new FrameImage(104, 30, 30);
			fraImgSub3Eff = new FrameImage(242, 49, 28, 2);
			fraImgSub4Eff = new FrameImage(243, 36, 39);
			fRemove = 33;
			x = objBeFireMain.x;
			y = objBeFireMain.y;
			y1000 = 240;
			break;
		case 170:
		case 238:
			fraImgEff = new FrameImage(244, 20, 37, 3);
			fraImgSubEff = new FrameImage(152, 25, 21);
			fraImgSub4Eff = new FrameImage(243, 36, 39);
			fraImgSub2Eff = new FrameImage(240, 30, 73, 1);
			fraImgSub3Eff = new FrameImage(241, 40, 27, 2);
			fRemove = 43;
			vMax = 30;
			if (isAddSound)
			{
				addSoundBuffShort();
			}
			break;
		case 171:
		case 239:
			y = objBeFireMain.y;
			x = objBeFireMain.x;
			fraImgEff = new FrameImage(118, 62, 64, 47, 48);
			fraImgSubEff = new FrameImage(174, 40, 40);
			fraImgSub2Eff = new FrameImage(247, 49, 28);
			fraImgSub3Eff = new FrameImage(254, 30, 40);
			vMax = 16;
			fRemove = 24;
			break;
		case 172:
		case 240:
			fRemove = 24;
			fraImgEff = new FrameImage(254, 30, 40);
			if (isAddSound)
			{
				addSoundBuffShort();
			}
			break;
		case 168:
			fRemove = 10;
			fraImgEff = new FrameImage(255, 42, 50, 3);
			y = objFireMain.y;
			break;
		case 173:
			fRemove = 8;
			fraImgEff = new FrameImage(257, 15, 51);
			fraImgSubEff = new FrameImage(3, 30, 50);
			vMax = 12;
			x += am_duong * 30;
			break;
		case 174:
			fRemove = 12;
			fraImgEff = new FrameImage(219, 47, 7);
			vMax = 7;
			break;
		case 175:
			levelPaint = -1;
			fRemove = 8;
			fraImgEff = new FrameImage(258, 35, 28);
			break;
		case 178:
			fraImgEff = new FrameImage(266, 80, 100, 64, 80, 2);
			fraImgSubEff = new FrameImage(201, 64, 50, 45, 35);
			fRemove = 35;
			GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne, 200, Dir, objMainEff);
			x -= am_duong * 15;
			y = objFireMain.y;
			vMax = 10;
			frame = -1;
			break;
		case 179:
		case 241:
			fRemove = 26;
			if (objFireMain.vecBuffCur != null)
			{
				for (int i = 0; i < objFireMain.vecBuffCur.size(); i++)
				{
					if (((MainBuff)objFireMain.vecBuffCur.elementAt(i)).IdBuff == 2037)
					{
						fraImgEff = new FrameImage(267, 46, 53);
						fraImgSubEff = new FrameImage(270, 80, 47);
						fraImgSub2Eff = new FrameImage(271, 130, 80, 3);
						fraImgSub3Eff = new FrameImage(272, 50, 24);
						if (typeEffect == 241)
						{
							fraImgSub4Eff = new FrameImage(224, 22, 28);
						}
						frame = 1;
						break;
					}
				}
			}
			if (fraImgEff == null)
			{
				fraImgEff = new FrameImage(10, 40, 47);
				fraImgSubEff = new FrameImage(260, 54, 54, 1);
				frame = 0;
			}
			if (typeEffect == 241)
			{
				step = 1;
			}
			break;
		case 195:
			fraImgEff = new FrameImage(238, 30, 73);
			fraImgSubEff = new FrameImage(195, 40, 27);
			fRemove = 20;
			GameScreen.addEffectEnd(30, 0, x - am_duong * 20, objFireMain.y - objFireMain.hOne / 2 - 5, 300, Dir, objMainEff);
			break;
		case 196:
			fraImgEff = new FrameImage(225, 24, 32);
			fraImgSubEff = new FrameImage(286, 50, 100);
			fraImgSub2Eff = new FrameImage(98, 78, 70);
			GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2, 800, Dir, objMainEff);
			fRemove = 25;
			break;
		case 197:
			fraImgEff = new FrameImage(287, 76, 27);
			fRemove = 8;
			x += am_duong * 20;
			y -= 10;
			break;
		case 198:
			fraImgEff = new FrameImage(288, 30, 30);
			fRemove = 20;
			vMax = 12;
			x += am_duong * 25;
			break;
		case 199:
			fraImgEff = new FrameImage(291, 47, 48);
			fRemove = 16;
			y = objMainEff.y;
			break;
		case 200:
			Dir = (sbyte)objFireMain.type_left_right;
			am_duong = -1;
			if (Dir == 2)
			{
				am_duong = 1;
			}
			fraImgEff = new FrameImage(292, 78, 24);
			fraImgSubEff = new FrameImage(293, 50, 14);
			fRemove = 16;
			vMax = 10;
			objFireMain.isPaintWeapon = false;
			x += am_duong * 30;
			y -= 5;
			GameScreen.addEffectEnd(30, 0, x, y, 800, Dir, objMainEff);
			break;
		case 201:
			fRemove = 16;
			vMax = 10;
			GameScreen.addEffectEnd(30, 0, x, y, 400, Dir, objMainEff);
			y = objFireMain.y;
			break;
		case 202:
			fraImgEff = new FrameImage(295, 34, 24);
			vMax = 12;
			GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne - 10, 400, Dir, objMainEff);
			fRemove = 16;
			break;
		case 203:
			fraImgEff = new FrameImage(296, 36, 63);
			fRemove = 16;
			y = objMainEff.y;
			break;
		case 204:
			fRemove = 26;
			fraImgSubEff = new FrameImage(297, 83, 47);
			fraImgSub2Eff = new FrameImage(272, 50, 24);
			break;
		case 205:
			fraImgEff = new FrameImage(224, 22, 28);
			fraImgSubEff = new FrameImage(32, 45, 45);
			fRemove = 60;
			break;
		case 208:
			create_Eff_Tru();
			break;
		case 250:
			create_Eff_Tru_2();
			break;
		case 209:
		case 242:
			create_Eff_Lucci_1();
			break;
		case 210:
		case 243:
			create_Eff_Dong_Dat_1();
			break;
		case 211:
		case 244:
			create_Eff_Dong_Dat_2();
			break;
		case 219:
			fraImgEff = new FrameImage(323, 92, 64);
			fraImgSubEff = new FrameImage(183, 20, 54);
			fRemove = 26;
			GameScreen.addEffectEnd(30, 0, x + am_duong * 15, y, 200, Dir, objMainEff);
			mframe = new int[22]
			{
				-1, -1, -1, -1, -1, 0, 0, 1, 1, -1,
				2, 2, 2, 4, 4, 5, 5, -1, 6, 6,
				7, -1
			};
			x1000 = objFireMain.x;
			y1000 = objFireMain.y;
			break;
		case 292:
			fraImgEff = new FrameImage(323, 92, 64);
			fraImgSubEff = new FrameImage(183, 20, 54);
			fRemove = 26;
			GameScreen.addEffectEnd(30, 0, x + am_duong * 15, y, 200, Dir, objMainEff);
			mframe = new int[26]
			{
				4, 4, 5, 5, 4, 4, 5, 5, 4, 4,
				5, 5, 6, 6, 0, 0, 0, 0, 0, 0,
				1, 1, 1, 1, 1, 1
			};
			x1000 = objFireMain.x;
			y1000 = objFireMain.y;
			break;
		case 291:
			fraImgEff = new FrameImage(323, 92, 64);
			fraImgSubEff = new FrameImage(183, 20, 54);
			fRemove = 18;
			GameScreen.addEffectEnd(30, 0, x + am_duong * 15, y, 200, Dir, objMainEff);
			mframe = new int[20]
			{
				4, 4, 5, 5, 4, 4, 5, 5, 4, 4,
				5, 5, 4, 4, 5, 5, 6, 6, 6, -1
			};
			x1000 = objFireMain.x;
			y1000 = objFireMain.y;
			break;
		case 233:
			fraImgEff = new FrameImage(107, 50, 54);
			fRemove = 20;
			break;
		case 245:
		case 251:
			fraImgEff = new FrameImage(357, 100, 100, 2);
			fraImgSubEff = new FrameImage(358, 51, 22);
			fRemove = 22;
			x += am_duong * 30;
			mframeSuper = new int[12][]
			{
				new int[3],
				new int[3] { 0, 10, 0 },
				new int[3] { 0, 25, 0 },
				new int[3] { 1, 0, -15 },
				new int[3] { 1, 10, -5 },
				new int[3] { 1, 20, 5 },
				new int[3] { 2, 10, 0 },
				new int[3] { 2, 15, 5 },
				new int[3] { 2, 30, 15 },
				new int[3] { 3, 0, 0 },
				new int[3] { 3, 10, 0 },
				new int[3] { 3, 30, 0 }
			};
			break;
		case 249:
		case 252:
			fraImgEff = new FrameImage(357, 100, 100, 2);
			fraImgSubEff = new FrameImage(359, 64, 64);
			fraImgSub2Eff = new FrameImage(183, 20, 54);
			fRemove = 22;
			x += am_duong * 30;
			addSoundBuffShort();
			mframeSuper = new int[12][]
			{
				new int[3],
				new int[3] { 0, 10, 0 },
				new int[3] { 0, 35, 0 },
				new int[3] { 1, 0, -15 },
				new int[3] { 1, 10, -5 },
				new int[3] { 1, 30, 5 },
				new int[3] { 2, 10, 0 },
				new int[3] { 2, 15, 5 },
				new int[3] { 2, 40, 15 },
				new int[3] { 3, 0, 0 },
				new int[3] { 3, 10, 0 },
				new int[3] { 3, 40, 0 }
			};
			break;
		case 246:
		case 253:
			fraImgEff = new FrameImage(351, 35, 62);
			fraImgSubEff = new FrameImage(354, 40, 47);
			fRemove = 26;
			if (isAddSound)
			{
				addSoundBuffShort();
			}
			GameScreen.addEffectEnd(30, 0, x, y, 400, Dir, objMainEff);
			break;
		case 247:
		case 254:
			y = objFireMain.y - objFireMain.hOne + 8;
			fraImgEff = new FrameImage(352, 52, 15);
			fraImgSubEff = new FrameImage(353, 9, 7);
			fraImgSub2Eff = new FrameImage(355, 9, 10);
			fraImgSub3Eff = new FrameImage(224, 22, 28);
			fRemove = 24;
			GameScreen.addEffectEnd(30, 0, x, y, 400, Dir, objMainEff);
			vMax = 140;
			if (CRes.abs(objFireMain.x - objBeFireMain.x) < 32)
			{
				objFireMain.x -= am_duong * 32;
			}
			break;
		}
		if (objFireMain == GameScreen.player)
		{
			for (int n = 0; n < vecObjsBeFire.size(); n++)
			{
				Object_Effect_Skill object_Effect_Skill4 = (Object_Effect_Skill)vecObjsBeFire.elementAt(n);
				if (object_Effect_Skill4 == null)
				{
					continue;
				}
				if (GameScreen.typePaintGameScreen == 1)
				{
					MainObject mainObject3 = MainObject.get_Object(object_Effect_Skill4.ID, object_Effect_Skill4.tem);
					if (mainObject3 != null)
					{
						mainObject3.isPaintSpec = true;
					}
					continue;
				}
				MainObject mainObject4 = MainObject.get_Object(object_Effect_Skill4.ID, object_Effect_Skill4.tem);
				if (mainObject4 == null || mainObject4.typeObject != 1)
				{
					continue;
				}
				int num = CRes.abs(objFireMain.x - mainObject4.x);
				if (num < 32)
				{
					mainObject4.x += am_duong * (num - 32 + 10);
					mainObject4.vXEffAva = (54 - num) / 2 * am_duong;
					if (mainObject4.Action != 4 && mainObject4.Action != 2 && mainObject4.Hp > 0)
					{
						mainObject4.Action = 3;
						mainObject4.f = 0;
						mainObject4.resetAction();
					}
					else
					{
						mainObject4.vXEffAva = 0;
						mainObject4.dy = 0;
					}
				}
			}
		}
		if (!isEff)
		{
			setHP_New(vecObjsBeFire, objFireMain, isAdd: false);
			if (vecObjsBeFire.size() == 0)
			{
				isStop = true;
				return false;
			}
		}
		return true;
	}

	public void createDanFocus()
	{
		switch (CRes.random(4))
		{
		case 0:
			gocT_Arc = 90;
			break;
		case 1:
			gocT_Arc = 270;
			break;
		case 2:
			gocT_Arc = 180;
			break;
		case 3:
			gocT_Arc = 0;
			break;
		}
		va = 4096;
		vx = 0;
		vy = 0;
		life = 0;
		vX1000 = va * CRes.getcos(gocT_Arc) >> 10;
		vY1000 = va * CRes.getsin(gocT_Arc) >> 10;
	}

	private void create_Eff_Tru_2()
	{
		fraImgEff = new FrameImage(100, 15, 20);
		y = objFireMain.y - 55;
		if (objFireMain.IdIcon == 58)
		{
			fraImgEff = new FrameImage(366, 15, 20);
			y = objFireMain.y - 80;
		}
		vMax = 20;
		for (int i = 0; i < vecObjsBeFire.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
			if (object_Effect_Skill == null)
			{
				continue;
			}
			MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
			if (mainObject != null)
			{
				Point_Focus point_Focus = new Point_Focus(x, y);
				int xdich = mainObject.x - x;
				int ydich = mainObject.y - mainObject.hOne / 2 - y;
				create_Speed(xdich, ydich, point_Focus, x, y, mainObject.x, mainObject.y - mainObject.hOne / 2);
				point_Focus.dis = 0;
				if (mainObject.x > x)
				{
					point_Focus.dis = 2;
				}
				VecEff.addElement(point_Focus);
			}
		}
	}

	private void createKilo_1()
	{
		fraImgEff = new FrameImage(356, 40, 80);
		fraImgSubEff = new FrameImage(183, 20, 54);
		toY = objBeFireMain.y;
		fRemove = 22;
	}

	private void create_Crocodile_1()
	{
		fRemove = 20;
		y = objFireMain.y;
		fraImgEff = new FrameImage(200, 54, 70, 40, 52);
		objFireMain.isTanHinh = true;
		if (isAddSound)
		{
			mSound.playSound(42, mSound.volumeSound);
		}
		if (typeEffect == 235)
		{
			vMax = 120;
			fraImgSubEff = new FrameImage(118, 62, 64);
		}
	}

	private void createUssopS1_L5()
	{
		fraImgEff = new FrameImage(183, 20, 54);
		fraImgSubEff = new FrameImage(330, 46, 49);
		mframeSuper = new int[6][]
		{
			new int[2] { -5, -15 },
			new int[2] { 5, 15 },
			new int[2] { 15, -5 },
			new int[2] { -15, 5 },
			new int[2] { -10, -10 },
			new int[2] { 10, 10 }
		};
		y = objFireMain.y;
		fRemove = 18;
		vMax = 24;
		if (typeEffect == 301)
		{
			x1000 = x + 30 * am_duong;
			xLight1 = x1000;
			xLight2 = x1000;
			fraImgSub2Eff = new FrameImage(416, 4);
			int xdich = x1000 - x;
			int ydich = 0;
			VecSubEff.addElement(create_Speed(xdich, ydich, new Point_Focus(), x, y - objFireMain.hOne / 4 * 3, toX, toY));
			VecSubEff.addElement(create_Speed(xdich, ydich, new Point_Focus(), x, y - objFireMain.hOne / 2, toX, toY));
		}
	}

	private void create_Eff_Dong_Dat_2()
	{
		fraImgEff = new FrameImage(118, 62, 64);
		if (typeEffect == 244)
		{
			fraImgSubEff = new FrameImage(138, 62, 64);
		}
		fRemove = 30;
		GameScreen.addEffectEnd(30, 0, x + 10, objFireMain.y - objFireMain.hOne / 2, 600, Dir, objMainEff);
		GameScreen.addEffectEnd(30, 0, x - 10, objFireMain.y - objFireMain.hOne / 2, 600, Dir, objMainEff);
		y = objFireMain.y;
		if (isAddSound)
		{
			addSoundBuffShort();
		}
	}

	private void create_Eff_Dong_Dat_1()
	{
		fraImgEff = new FrameImage(310, 73, 59);
		fraImgSubEff = new FrameImage(311, 149, 179);
		GameScreen.addEffectEnd(30, 0, x + 10, objFireMain.y - objFireMain.hOne / 2, 400, Dir, objMainEff);
		GameScreen.addEffectEnd(30, 0, x - 10, objFireMain.y - objFireMain.hOne / 2, 400, Dir, objMainEff);
		fRemove = 50;
		if (objFireMain == GameScreen.player)
		{
			fRemove = 70;
		}
		if (isAddSound)
		{
			addSoundBuffShort();
		}
	}

	private void create_Eff_Lucci_1()
	{
		fraImgEff = new FrameImage(274, 23, 74, 3);
		frame = 0;
		vx = am_duong * 12;
		x1000 = x;
		GameScreen.addEffectEnd(30, 0, x + am_duong * 20, objFireMain.y - objFireMain.hOne / 2, 400, Dir, objMainEff);
		x = x1000 - am_duong * 24;
		fRemove = 20;
		if (isAddSound)
		{
			mSound.playSound(42, mSound.volumeSound);
			addSoundBuffShort();
		}
		if (objFireMain.vecBuffCur != null)
		{
			for (int i = 0; i < objFireMain.vecBuffCur.size(); i++)
			{
				MainBuff mainBuff = (MainBuff)objFireMain.vecBuffCur.elementAt(i);
				if (mainBuff.IdBuff == 2040 || mainBuff.IdBuff == 2064)
				{
					fraImgSubEff = new FrameImage(273, 24, 24, 4);
					frame = 1;
					break;
				}
				if (mainBuff.IdBuff == 2061)
				{
					fraImgSubEff = new FrameImage(273, 24, 24, 4);
					frame = 3;
					break;
				}
			}
		}
		if (frame == 0)
		{
			mframe = new int[23]
			{
				1, 0, 1, 0, 1, 0, 0, 1, 1, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2
			};
		}
		else
		{
			mframe = new int[20]
			{
				1, 0, 1, 0, 1, 0, 0, 1, 2, 2,
				3, 3, 4, 4, 4, 4, 4, 4, 4, 4
			};
		}
		if (typeEffect == 242)
		{
			GameScreen.addEffectEnd(147, (sbyte)frame, x + am_duong * 120, objFireMain.y - objFireMain.hOne / 2, 400, Dir, objMainEff);
		}
	}

	private void create_Eff_Tru()
	{
		fraImgEff = new FrameImage(100, 15, 20);
		vMax = 20;
		y = objFireMain.y - 55;
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
	}

	private void createMon29()
	{
		fraImgEff = new FrameImage(118, 62, 64, 47, 48);
		toY = objBeFireMain.y + 5;
		numNextFrame = 2;
		fRemove = 8;
	}

	private void createLapin()
	{
		vMax = 16;
		fraImgEff = new FrameImage(213, 15, 15);
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
	}

	public void paintEffThunderFall(mGraphics g)
	{
		if (!isPauseImgEff)
		{
			if (typeEffect == 1999)
			{
				fraImgEff.drawFrame(fImg, x + xLech[0], y + yLech[0], 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
			fraImgSub2Eff.drawFrame(fImg, x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			fraImgSubEff.drawFrame(fImg, x + xLech[1], y + yLech[1], 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
		}
		if (!isPauseSubEff && typeEffect == 1999)
		{
			fraImgEff.drawFrame(fSub, x + xLech[2], y + yLech[2], 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
		}
		if (!isPauseSub2Eff)
		{
			fraImgSubEff.drawFrame(fSub2, x + xLech[3], y + yLech[3], 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
		}
	}

	public override void paint(mGraphics g)
	{
		try
		{
			switch (typeEffect)
			{
			case 2000:
				fraImgEff.drawFrame(fImg, x, y - 22, 0, 3, g);
				break;
			case 1998:
			case 1999:
				paintEffThunderFall(g);
				break;
			case 280:
				fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, x, y, 0, 3, g);
				fraImgEff.drawFrame(0, toX, toY, 0, 3, g);
				break;
			case 0:
			case 36:
			case 61:
			case 71:
			case 76:
			case 81:
			case 143:
			case 145:
			case 146:
			case 148:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				break;
			case 82:
			case 144:
				if (f <= fRemove)
				{
					fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				}
				break;
			case 1:
			case 37:
			{
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				for (int num92 = 0; num92 < VecEff.size(); num92++)
				{
					Point point49 = (Point)VecEff.elementAt(num92);
					fraImgSubEff.drawFrame(point49.f % fraImgSubEff.nFrame, point49.x, point49.y, Dir, 3, g);
				}
				break;
			}
			case 154:
				fraImgEff.drawFrame(f % fraImgEff.nFrame, x, y, Dir, 3, g);
				break;
			case 155:
				if (f < 2 || f > 5)
				{
					fraImgEff.drawFrame(f % fraImgEff.nFrame, x + xplus, y, Dir, 3, g);
				}
				if (f < 6 && f > 1)
				{
					fraImgSubEff.drawFrame((f + ((f > 3) ? 1 : 0)) % 2, x + xplus, y, 0, 3, g);
				}
				break;
			case 7:
			case 141:
			{
				for (int num11 = 0; num11 < VecEff.size(); num11++)
				{
					Point_Focus point_Focus4 = (Point_Focus)VecEff.elementAt(num11);
					paint_Bullet(g, fraImgEff, point_Focus4.frame, point_Focus4.x, point_Focus4.y, isMore: false, 0);
				}
				break;
			}
			case 142:
			{
				for (int num22 = 0; num22 < VecEff.size(); num22++)
				{
					Point_Focus point_Focus9 = (Point_Focus)VecEff.elementAt(num22);
					fraImgEff.drawFrame((point_Focus9.frame + f) % fraImgEff.nFrame, point_Focus9.x / 10, point_Focus9.y / 10, point_Focus9.dis, 3, g);
				}
				break;
			}
			case 57:
			case 64:
			case 66:
				if (f < fPlayFrameSuper)
				{
					fraImgEff.drawFrame(0, x, y, 0, 3, g);
				}
				break;
			case 58:
				fraImgEff.drawFrame(2, x, y, 0, 3, g);
				break;
			case 126:
			case 159:
			case 192:
			{
				for (int num98 = 0; num98 < VecEff.size(); num98++)
				{
					fraImgEff.drawFrame(2, x, y, 0, 3, g);
				}
				if (objFireMain.isTanHinh)
				{
					fraImgSubEff.drawFrame(0, objFireMain.x, objFireMain.y - objFireMain.dy, Dir, 33, g);
				}
				break;
			}
			case 224:
				if (f == 1 || f == 15)
				{
					fraImgEff.drawFrame(0, x, y, 0, 33, g);
				}
				if (objFireMain.isTanHinh)
				{
					fraImgSubEff.drawFrame(f / 2 % fraImgSubEff.nFrame, x + mframeSuper[(f - 2) / 2][0], y + mframeSuper[(f - 2) / 2][1], Dir, 33, g);
				}
				break;
			case 301:
			{
				if (f == 1 || f == 15)
				{
					fraImgEff.drawFrame(0, x, y, 0, 33, g);
				}
				if (objFireMain.isTanHinh)
				{
					fraImgSubEff.drawFrame(f / 2 % fraImgSubEff.nFrame, x + mframeSuper[(f - 2) / 2][0], y + mframeSuper[(f - 2) / 2][1], Dir, 33, g);
				}
				for (int num103 = 0; num103 < VecSubEff.size(); num103++)
				{
					if (f > num103 * 4)
					{
						Point_Focus point_Focus32 = (Point_Focus)VecSubEff.elementAt(num103);
						int trans10 = 0;
						if (Dir == 0)
						{
							trans10 = 2;
						}
						fraImgSub2Eff.drawFrame(f % fraImgSub2Eff.nFrame, point_Focus32.x, point_Focus32.y, trans10, 3, g);
					}
				}
				break;
			}
			case 9:
			case 53:
			case 163:
			{
				if (f < 3)
				{
					fraImgEff.drawFrame(f, x, y, Dir, 3, g);
					break;
				}
				for (int num54 = 0; num54 < VecEff.size(); num54++)
				{
					Point_Focus point_Focus19 = (Point_Focus)VecEff.elementAt(num54);
					fraImgSubEff.drawFrameNew(indexEff_1 * fraImgSubEff.maxNumFrame + point_Focus19.f % fraImgSubEff.maxNumFrame, point_Focus19.x, point_Focus19.y, 0, 3, g);
					if (typeEffect != 9)
					{
						fraImgSub2Eff.drawFrame(CRes.random(fraImgSub2Eff.nFrame), point_Focus19.x, point_Focus19.y, 0, 3, g);
					}
				}
				break;
			}
			case 63:
			case 190:
			case 222:
			case 312:
			{
				if ((f >= 20 && f < 23) || (f >= 10 && f < 13))
				{
					fraImgEff.drawFrame(f % 10, x, y, Dir, 3, g);
				}
				if (typeEffect == 222 || typeEffect == 312)
				{
					for (int num106 = 0; num106 < VecSubEff.size(); num106++)
					{
						Point point54 = (Point)VecSubEff.elementAt(num106);
						fraImgSub3Eff.drawFrame(point54.f / 2, point54.x, point54.y, 0, 3, g);
					}
				}
				for (int num107 = 0; num107 < VecEff.size(); num107++)
				{
					Point_Focus point_Focus35 = (Point_Focus)VecEff.elementAt(num107);
					fraImgSubEff.drawFrameNew(indexEff_1 * fraImgSubEff.maxNumFrame + point_Focus35.f % fraImgSubEff.maxNumFrame, point_Focus35.x, point_Focus35.y, 0, 3, g);
					fraImgSub2Eff.drawFrame(CRes.random(fraImgSub2Eff.nFrame), point_Focus35.x, point_Focus35.y, 0, 3, g);
				}
				break;
			}
			case 11:
			{
				if (f > 3 && f < 12)
				{
					fraImgSub2Eff.drawFrameNew(indexEff_1 * fraImgSub2Eff.maxNumFrame + f % fraImgSub2Eff.maxNumFrame, xplus, yplus, Dir, 3, g);
					fraImgSub3Eff.drawFrame(CRes.random(fraImgSub3Eff.nFrame), xplus, yplus, Dir, 3, g);
					fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, x, y, 0, 33, g);
				}
				for (int num12 = 0; num12 < VecEff.size(); num12++)
				{
					Point_Focus point_Focus5 = (Point_Focus)VecEff.elementAt(num12);
					fraImgEff.drawFrame(point_Focus5.f % fraImgEff.nFrame, point_Focus5.x, point_Focus5.y, 0, 33, g);
				}
				break;
			}
			case 139:
			{
				if (f > 2 && f < 16)
				{
					fraImgSub2Eff.drawFrameNew(indexEff_1 * fraImgSub2Eff.maxNumFrame + f % fraImgSub2Eff.maxNumFrame, xplus, yplus, Dir, 3, g);
					fraImgSub3Eff.drawFrame(CRes.random(fraImgSub3Eff.nFrame), xplus, yplus, Dir, 3, g);
					fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, x, y, 0, 33, g);
				}
				for (int num75 = 0; num75 < VecEff.size(); num75++)
				{
					Point_Focus point_Focus24 = (Point_Focus)VecEff.elementAt(num75);
					fraImgEff.drawFrame(point_Focus24.f % fraImgEff.nFrame, point_Focus24.x, point_Focus24.y, 0, 33, g);
					fraImgSub3Eff.drawFrame(CRes.random(fraImgSub3Eff.nFrame), point_Focus24.x + CRes.random_Am_0(10), point_Focus24.y - CRes.random(10), 0, 33, g);
				}
				break;
			}
			case 140:
			{
				if (f > 2 && f < 18)
				{
					fraImgSub2Eff.drawFrameNew(indexEff_1 * fraImgSub2Eff.maxNumFrame + f % fraImgSub2Eff.maxNumFrame, xplus, yplus, Dir, 3, g);
					fraImgSub3Eff.drawFrame(CRes.random(fraImgSub3Eff.nFrame), xplus, yplus, Dir, 3, g);
				}
				for (int num34 = 0; num34 < VecEff.size(); num34++)
				{
					Point_Focus point_Focus12 = (Point_Focus)VecEff.elementAt(num34);
					mImgframe[2].drawFrame(point_Focus12.f % mImgframe[2].nFrame, point_Focus12.x, point_Focus12.y, 0, 3, g);
				}
				if (f >= 32 && f <= 36 && !checkNullObject(2) && CRes.random(4) != 0)
				{
					int num35 = CRes.random(1, 5);
					for (int num36 = 0; num36 < num35; num36++)
					{
						int num37 = CRes.random_Am(0, 25) + objBeFireMain.x;
						mImgframe[1].drawFrame(CRes.random(mImgframe[1].nFrame), num37, objBeFireMain.y - 70, 0, 0, g);
					}
				}
				if (f >= 20 && f <= 38)
				{
					if (f < 24 || f >= 36)
					{
						mImgframe[0].drawFrame(0, objBeFireMain.x, objBeFireMain.y - 60, 0, 33, g);
					}
					else if (f < 28)
					{
						mImgframe[0].drawFrame(1, objBeFireMain.x, objBeFireMain.y - 60, 0, 33, g);
					}
					else if (f < 36)
					{
						mImgframe[0].drawFrame(2, objBeFireMain.x, objBeFireMain.y - 60, 0, 33, g);
					}
				}
				break;
			}
			case 12:
			case 49:
			case 50:
			case 188:
			case 220:
			case 293:
				paintSanji_3(g);
				break;
			case 16:
			case 51:
				if (f < fRemove)
				{
					fraImgSubEff.drawFrameNew(indexEff_1 * fraImgSubEff.maxNumFrame + f % fraImgSubEff.maxNumFrame, x, y, Dir, 3, g);
					if (fraImgEff != null)
					{
						fraImgEff.drawFrameNew(indexEff_1 * fraImgEff.maxNumFrame + CRes.random(fraImgEff.maxNumFrame), x, y, Dir, 3, g);
					}
				}
				break;
			case 52:
			case 189:
			case 221:
			case 311:
			{
				if (f < fRemove)
				{
					fraImgSubEff.drawFrameNew(indexEff_1 * fraImgSubEff.maxNumFrame + f % fraImgSubEff.maxNumFrame, x, y, Dir, 3, g);
					int num113 = 12 + CRes.random(fraImgEff.maxNumFrame);
					if ((typeEffect == 221 || typeEffect == 311) && CRes.random(2) == 0)
					{
						num113 -= 4;
					}
					fraImgEff.drawFrameNew(num113, x, y, Dir, 3, g);
				}
				for (int num114 = 0; num114 < VecEff.size(); num114++)
				{
					Point point57 = (Point)VecEff.elementAt(num114);
					fraImgEff.drawFrameNew(12 - point57.frame * 4 + point57.f, point57.x, point57.y, Dir, 3, g);
				}
				break;
			}
			case 19:
				if (!checkNullObject(1) && f >= 1 && f <= 12)
				{
					fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, objFireMain.x, objFireMain.y, 0, 33, g);
				}
				break;
			case 34:
				if (f <= 1 && objFireMain != null)
				{
					if (f == 0)
					{
						fraImgSubEff.drawFrame(f, x, y + objFireMain.hOne / 2, Dir, 33, g);
					}
					else
					{
						fraImgSubEff.drawFrame(f, objFireMain.x, objFireMain.y, Dir, 33, g);
					}
				}
				if (f >= 7 && objFireMain != null)
				{
					int num48 = 16;
					if (Dir == 0)
					{
						num48 = -16;
					}
					fraImgEff.drawFrame(2, objFireMain.x + num48, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, 3, g);
				}
				break;
			case 35:
			{
				if (f == 0 && objFireMain != null)
				{
					fraImgSubEff.drawFrame(0, x, y + objFireMain.hOne / 2, Dir, 33, g);
				}
				if (f >= 5 && objFireMain != null)
				{
					int num2 = 16;
					if (Dir == 0)
					{
						num2 = -16;
					}
					fraImgEff.drawFrame(2, objFireMain.x + num2, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, 3, g);
				}
				for (int j = 0; j < VecEff.size(); j++)
				{
					Point point2 = (Point)VecEff.elementAt(j);
					fraImgSubEff.drawFrame(point2.f / 2, point2.x, point2.y, Dir, 33, g);
				}
				break;
			}
			case 29:
				fraImgEff.drawFrame(f % fraImgEff.nFrame, objFireMain.x, objFireMain.y + 5, Dir, 33, g);
				break;
			case 31:
			case 55:
			case 56:
			case 191:
			case 223:
			case 313:
			{
				if (f < fRemove)
				{
					fraImgSubEff.drawFrameNew(indexEff_1 * fraImgSubEff.maxNumFrame + f % fraImgSubEff.maxNumFrame, x, y, Dir, 3, g);
					if (fraImgSub2Eff != null)
					{
						fraImgSub2Eff.drawFrame(f % fraImgSubEff.nFrame, x, y, Dir, 3, g);
					}
				}
				for (int num104 = 0; num104 < VecEff.size(); num104++)
				{
					Point_Focus point_Focus33 = (Point_Focus)VecEff.elementAt(num104);
					fraImgEff.drawFrame(point_Focus33.f % fraImgEff.nFrame, point_Focus33.x, point_Focus33.y, Dir, 3, g);
					if (fraImgSub2Eff != null)
					{
						fraImgSub2Eff.drawFrame(point_Focus33.f % fraImgSub2Eff.nFrame, point_Focus33.x, point_Focus33.y, Dir, 3, g);
					}
				}
				break;
			}
			case 67:
			case 68:
			case 69:
			case 194:
			case 226:
				if (f >= 10 && f <= fRemove)
				{
					paint_Bullet(g, fraImgEff, frame, x, y, isMore: false, f % 2 * 3);
				}
				break;
			case 303:
				if (f >= 10 && f <= fRemove)
				{
					paint_Bullet(g, fraImgEff, frame, x, y, isMore: false, 0);
					paint_Bullet(g, fraImgEff, frame1, rocket1.x, rocket1.y, isMore: false, 0);
					paint_Bullet(g, fraImgEff, frame2, rocket2.x, rocket2.y, isMore: false, 0);
				}
				break;
			case 164:
			case 227:
			{
				for (int num97 = 0; num97 < VecEff.size(); num97++)
				{
					Point point52 = (Point)VecEff.elementAt(num97);
					if (point52.dis == 0)
					{
						fraImgEff.drawFrame(point52.frame, point52.x, point52.y, Dir, 3, g);
					}
					else
					{
						fraImgSubEff.drawFrame(point52.frame, point52.x, point52.y, Dir, 3, g);
					}
				}
				break;
			}
			case 72:
			case 92:
				fraImgEff.drawFrame(3, x, y, Dir, 3, g);
				break;
			case 75:
				if (f < 2)
				{
					fraImgSubEff.drawFrame(0, x1000, y1000, Dir, 3, g);
				}
				fraImgEff.drawFrame(frame, x, y, Dir, 3, g);
				break;
			case 77:
			{
				int num61 = x;
				int num62 = y;
				if (f > 7)
				{
					fraImgEff.drawFrame(0, num61, num62, Dir, 3, g);
					num62 += 15;
					fraImgSubEff.drawFrame(0, num61, num62, Dir, 3, g);
				}
				break;
			}
			case 160:
				paintLuffy_New2(g);
				break;
			case 84:
			case 181:
			case 213:
			case 272:
				paintLuffy_New2_SHORT(g);
				break;
			case 85:
			case 182:
			case 214:
			case 273:
				paintLuffy_New3(g);
				break;
			case 86:
			case 157:
			case 183:
			case 215:
			{
				for (int num57 = 0; num57 < VecEff.size(); num57++)
				{
					Point_Focus point_Focus20 = (Point_Focus)VecEff.elementAt(num57);
					int num58 = point_Focus20.f * fraImgEff.frameHeight / 3 + fraImgEff.frameHeight / 3;
					if (num58 > fraImgEff.frameHeight)
					{
						num58 = fraImgEff.frameHeight;
					}
					if (fraImgEff.getImageFrame() != null)
					{
						g.drawRegion(fraImgEff.getImageFrame(), 0, fraImgEff.frameHeight - num58 + point_Focus20.f % fraImgEff.nFrame * fraImgEff.frameHeight, fraImgEff.frameWidth, num58, 0, point_Focus20.x, point_Focus20.y, 33);
					}
				}
				break;
			}
			case 281:
			{
				for (int num30 = 0; num30 < VecEff.size(); num30++)
				{
					Point_Focus point_Focus10 = (Point_Focus)VecEff.elementAt(num30);
					int num31 = point_Focus10.f * fraImgEff.frameHeight / 3 + fraImgEff.frameHeight / 3;
					if (num31 > fraImgEff.frameHeight)
					{
						num31 = fraImgEff.frameHeight;
					}
					if (fraImgEff.getImageFrame() != null)
					{
						g.drawRegion(fraImgEff.getImageFrame(), 0, fraImgEff.frameHeight - num31 + point_Focus10.f % fraImgEff.nFrame * fraImgEff.frameHeight, fraImgEff.frameWidth, num31, 0, point_Focus10.x, point_Focus10.y, 33);
					}
				}
				for (int num32 = 0; num32 < VecSubEff.size(); num32++)
				{
					if (f > 8 + num32 * 4)
					{
						Point_Focus point_Focus11 = (Point_Focus)VecSubEff.elementAt(num32);
						int trans3 = 0;
						if (Dir == 2)
						{
							trans3 = 2;
						}
						fraImgSub2Eff.drawFrame(f % fraImgSub2Eff.nFrame, point_Focus11.x, point_Focus11.y, trans3, 3, g);
					}
				}
				break;
			}
			case 266:
			{
				for (int num49 = 0; num49 < VecEff.size(); num49++)
				{
					if (f > 3 + num49 * 4)
					{
						Point_Focus point_Focus16 = (Point_Focus)VecEff.elementAt(num49);
						int trans5 = 0;
						if (Dir == 2)
						{
							trans5 = 2;
						}
						fraImgEff.drawFrame(0, point_Focus16.x, point_Focus16.y, trans5, 3, g);
					}
				}
				break;
			}
			case 276:
			case 277:
			{
				for (int num16 = 0; num16 < VecEff.size(); num16++)
				{
					Point_Focus point_Focus8 = (Point_Focus)VecEff.elementAt(num16);
					fraImgEff.drawFrame((point_Focus8.frame + point_Focus8.f) % fraImgEff.nFrame, point_Focus8.x, point_Focus8.y, Dir, 3, g);
				}
				break;
			}
			case 278:
			case 279:
			{
				for (int num20 = 0; num20 < VecEff.size(); num20++)
				{
					Point point15 = (Point)VecEff.elementAt(num20);
					fraImgEff.drawFrameNew(point15.frame + point15.dis * 5, objBeFireMain.x + point15.x, y + point15.y, 0, 3, g);
				}
				for (int num21 = 0; num21 < VecSubEff.size(); num21++)
				{
					Point point16 = (Point)VecSubEff.elementAt(num21);
					fraImgSubEff.drawFrameNew(point16.frame + point16.dis * 4, objBeFireMain.x + point16.x, y + point16.y, 0, 3, g);
				}
				break;
			}
			case 267:
			{
				if (f <= 2)
				{
					break;
				}
				for (int num15 = 0; num15 < VecEff.size(); num15++)
				{
					Point_Focus point_Focus7 = (Point_Focus)VecEff.elementAt(num15);
					int trans = 0;
					if (Dir == 2)
					{
						trans = 2;
					}
					fraImgEff.drawFrame(0, point_Focus7.x, point_Focus7.y - 5, trans, 3, g);
				}
				break;
			}
			case 268:
				if ((f >= 2 && f <= 11) || (f >= 16 && f <= 25))
				{
					fraImgEff.drawFrame(GameCanvas.gameTickChia4 % 2, x1000, y + 3, 0, 3, g);
				}
				break;
			case 269:
			{
				for (int num135 = 0; num135 < VecEff.size(); num135++)
				{
					Point point64 = (Point)VecEff.elementAt(num135);
					fraImgEff.drawFrame(GameCanvas.gameTickChia4 % 2, point64.x, point64.y, Dir, 3, g);
				}
				break;
			}
			case 87:
			case 184:
			case 216:
				if (f > 12 && f < 15)
				{
					fraImgEff.drawFrame(f - 13, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				else if (f > 22 && f < 25)
				{
					fraImgEff.drawFrame(f - 23, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				else if (f > 28 && f < 31)
				{
					fraImgEff.drawFrame(f - 29, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				else if (f > 34 && f < 37)
				{
					fraImgEff.drawFrame(f - 35, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				break;
			case 282:
			{
				if (f > 12 && f < 15)
				{
					fraImgEff.drawFrame(f - 13, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				else if (f > 22 && f < 25)
				{
					fraImgEff.drawFrame(f - 23, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				else if (f > 28 && f < 31)
				{
					fraImgEff.drawFrame(f - 29, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				else if (f > 34 && f < 37)
				{
					fraImgEff.drawFrame(f - 35, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				for (int num55 = 0; num55 < VecEff.size(); num55++)
				{
					Point point30 = (Point)VecEff.elementAt(num55);
					int trans6 = point30.dis;
					int idx2 = point30.frame;
					if (point30.frame == 2)
					{
						trans6 = 5;
					}
					else if (point30.frame == 3)
					{
						idx2 = 2;
					}
					fraImgSubEff.drawFrame(idx2, point30.x, point30.y, trans6, 3, g);
				}
				break;
			}
			case 161:
				paintZoroS2_L3_SHORT(g);
				break;
			case 93:
				if (f > 2 && f < 6)
				{
					fraImgEff.drawFrame(f - 3, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				if (f > 8 && f < 12)
				{
					fraImgEff.drawFrame(11 - f, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				if (f > 26 && f < 29)
				{
					fraImgEff.drawFrame(f - 27, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
				}
				break;
			case 94:
				if (f <= 3)
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 33, g);
				}
				if (f > 3 && f <= 7)
				{
					fraImgEff.drawFrame((f - 4) / 2, x, y, (Dir == 0) ? 2 : 0, 33, g);
				}
				break;
			case 95:
			{
				if (f < 2)
				{
					fraImgEff.drawFrame(f, x, y + 3, Dir, 3, g);
				}
				for (int num125 = 0; num125 < VecEff.size(); num125++)
				{
					Point_Focus point_Focus41 = (Point_Focus)VecEff.elementAt(num125);
					fraImgEff.drawFrame(point_Focus41.frame, point_Focus41.x, point_Focus41.y, Dir, 3, g);
				}
				break;
			}
			case 96:
				paintBuggy_2(g);
				break;
			case 97:
			{
				if (f < 4)
				{
					fraImgSub2Eff.drawFrame(f, x, y, Dir, 3, g);
				}
				for (int num118 = 0; num118 < VecEff.size(); num118++)
				{
					Point_Focus point_Focus37 = (Point_Focus)VecEff.elementAt(num118);
					if (point_Focus37.frame == 0)
					{
						fraImgEff.drawFrame(f % fraImgEff.nFrame, point_Focus37.x, point_Focus37.y, Dir, 3, g);
					}
					else
					{
						fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, point_Focus37.x, point_Focus37.y, Dir, 3, g);
					}
				}
				break;
			}
			case 22:
			case 98:
				if (!checkNullObject(1))
				{
					if (f < 5)
					{
						fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy, 0, 33, g);
					}
					if (f >= 10 && f <= 14)
					{
						fraImgEff.drawFrame(f % fraImgEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy, 0, 33, g);
					}
				}
				break;
			case 248:
			case 255:
				if (!checkNullObject(1))
				{
					if (f == 5 || f == 6 || f == 10 || f == 11 || f == 14)
					{
						fraImgSubEff.drawFrame(0, objFireMain.x, objFireMain.y - objFireMain.dy, 0, 33, g);
					}
					if (f >= 15 && f <= 19)
					{
						fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy, 0, 33, g);
					}
				}
				break;
			case 99:
			{
				int num82 = f % 4;
				if (num82 < 4 && f < 8)
				{
					fraImgEff.drawFrame(num82, x, y - num82 * 2 + 5, Dir, 3, g);
					int trans8 = 1;
					if (Dir == 2)
					{
						trans8 = 3;
					}
					fraImgEff.drawFrame(num82, x, y - num82 * 2 - 15, trans8, 3, g);
				}
				break;
			}
			case 100:
				if (f >= 5 && f <= 11)
				{
					fraImgEff.drawFrame(0, objFireMain.x, objFireMain.y, Dir, 33, g);
				}
				break;
			case 101:
				if (f >= 6 && f <= 15)
				{
					int num71 = (f - 2) % 4;
					fraImgEff.drawFrame(num71, objFireMain.x + x1000, objFireMain.y - objFireMain.hOne / 2 - num71 * 2 + 5, Dir, 3, g);
					int trans7 = 1;
					if (Dir == 2)
					{
						trans7 = 3;
					}
					fraImgEff.drawFrame(num71, objFireMain.x + x1000, objFireMain.y - objFireMain.hOne / 2 - num71 * 2 - 15, trans7, 3, g);
				}
				break;
			case 102:
			{
				if (f < 4)
				{
					fraImgSub2Eff.drawFrame(f, x, y, Dir, 3, g);
				}
				for (int num40 = 0; num40 < VecEff.size(); num40++)
				{
					Point_Focus point_Focus14 = (Point_Focus)VecEff.elementAt(num40);
					fraImgEff.drawFrame(point_Focus14.f % fraImgEff.nFrame, point_Focus14.x, point_Focus14.y, Dir, 3, g);
				}
				for (int num41 = 0; num41 < VecSubEff.size(); num41++)
				{
					Point point23 = (Point)VecSubEff.elementAt(num41);
					fraImgSubEff.drawFrame((point23.f + point23.frame) % fraImgSubEff.nFrame, point23.x, point23.y, Dir, 3, g);
				}
				break;
			}
			case 103:
			{
				if (f < 4)
				{
					fraImgSubEff.drawFrame(0, x, y, Dir, 33, g);
				}
				for (int num33 = 0; num33 < VecEff.size(); num33++)
				{
					Point point21 = (Point)VecEff.elementAt(num33);
					int trans4 = Dir;
					if (point21.frame == 2)
					{
						trans4 = 5;
					}
					fraImgEff.drawFrame(point21.frame, point21.x, point21.y, trans4, 3, g);
				}
				break;
			}
			case 104:
			{
				if (f < 8 && f % 2 == 1)
				{
					fraImgSubEff.drawFrame(0, x, y, Dir, 33, g);
				}
				for (int num17 = 0; num17 < VecEff.size(); num17++)
				{
					Point point12 = (Point)VecEff.elementAt(num17);
					if (point12.frame == 4)
					{
						fraImgSubEff.drawFrame(0, point12.x, point12.y, Dir, 33, g);
						continue;
					}
					int trans2 = point12.dis;
					int idx = point12.frame;
					if (point12.frame == 2)
					{
						trans2 = 5;
					}
					else if (point12.frame == 3)
					{
						idx = 2;
					}
					fraImgEff.drawFrame(idx, point12.x, point12.y, trans2, 3, g);
				}
				break;
			}
			case 106:
			{
				if (f < 10 || f % 4 > 1)
				{
					for (int num13 = 0; num13 < VecEff.size(); num13++)
					{
						Point point11 = (Point)VecEff.elementAt(num13);
						fraImgEff.drawFrame((f / 2 + point11.frame) % 3, point11.x, point11.y, Dir, 3, g);
					}
				}
				for (int num14 = 0; num14 < VecSubEff.size(); num14++)
				{
					Point_Focus point_Focus6 = (Point_Focus)VecSubEff.elementAt(num14);
					fraImgEff.drawFrame((f + point_Focus6.frame) % 3, point_Focus6.x, point_Focus6.y - 4, Dir, 3, g);
					if (point_Focus6.f % 2 == 0)
					{
						fraImgSubEff.drawFrame(0, point_Focus6.x, point_Focus6.y + 4, Dir, 3, g);
					}
				}
				break;
			}
			case 65:
			case 107:
				if (f < 4)
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 33, g);
				}
				break;
			case 70:
				if (f < 4)
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 33, g);
				}
				else
				{
					fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, x, y, Dir, 33, g);
				}
				break;
			case 108:
			{
				for (int k = 0; k < VecEff.size(); k++)
				{
					Point point3 = (Point)VecEff.elementAt(k);
					fraImgEff.drawFrame((point3.frame + f / point3.dis) % fraImgEff.nFrame, point3.x, point3.y, Dir, 3, g);
				}
				break;
			}
			case 109:
				paintDonKrieg_1(g);
				break;
			case 110:
				paintDonKrieg_2(g);
				break;
			case 111:
				paintDonKrieg_3(g);
				break;
			case 112:
			case 270:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				break;
			case 113:
			case 150:
			{
				for (int num127 = 0; num127 < VecEff.size(); num127++)
				{
					Point_Focus point_Focus43 = (Point_Focus)VecEff.elementAt(num127);
					fraImgEff.drawFrame(f % fraImgEff.nFrame, point_Focus43.x, point_Focus43.y, Dir, 3, g);
				}
				break;
			}
			case 151:
			case 152:
			case 153:
			{
				for (int num126 = 0; num126 < VecEff.size(); num126++)
				{
					Point_Focus point_Focus42 = (Point_Focus)VecEff.elementAt(num126);
					fraImgEff.drawFrame(frame * 3 + f / 2 % 2, point_Focus42.x, point_Focus42.y, Dir, 3, g);
				}
				break;
			}
			case 114:
			case 115:
			{
				for (int num119 = 0; num119 < VecEff.size(); num119++)
				{
					Point_Focus point_Focus38 = (Point_Focus)VecEff.elementAt(num119);
					fraImgEff.drawFrame(4, point_Focus38.x, point_Focus38.y, Dir, 3, g);
				}
				break;
			}
			case 116:
				if (f >= 11 && f <= 16)
				{
					fraImgEff.drawFrame((f - 11) / 3, x + x1000, y, Dir, 3, g);
				}
				else if (f >= 26 && f <= 31)
				{
					fraImgEff.drawFrame((f - 26) / 3, x + x1000, y, Dir, 3, g);
				}
				break;
			case 117:
				paintKurobi_2(g);
				break;
			case 118:
				if (vecObjsBeFire.size() > 1)
				{
					for (int num115 = 0; num115 < VecEff.size(); num115++)
					{
						Point point58 = (Point)VecEff.elementAt(num115);
						fraImgEff.drawFrame(point58.f / 2, point58.x, point58.y, point58.dis, 3, g);
					}
				}
				else
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 3, g);
				}
				break;
			case 119:
			{
				for (int num105 = 0; num105 < VecEff.size(); num105++)
				{
					Point_Focus point_Focus34 = (Point_Focus)VecEff.elementAt(num105);
					fraImgEff.drawFrame(0, point_Focus34.x, point_Focus34.y, point_Focus34.dis, 3, g);
					fraImgSubEff.drawFrame(0, point_Focus34.x, point_Focus34.y + 30, point_Focus34.dis, 3, g);
					if (f % 2 == 0)
					{
						if (point_Focus34.dis == 0)
						{
							fraImgSub2Eff.drawFrame(CRes.random(2), point_Focus34.x - 25, point_Focus34.y, 0, 3, g);
						}
						else if (point_Focus34.dis == 2)
						{
							fraImgSub2Eff.drawFrame(CRes.random(2), point_Focus34.x + 25, point_Focus34.y, 2, 3, g);
						}
					}
				}
				break;
			}
			case 120:
			{
				if (f <= 9)
				{
					fraImgSubEff.drawFrame(0, x + plusxy[2][0], y + plusxy[2][1], Dir, 3, g);
				}
				else if (f >= 10 && f <= 11)
				{
					fraImgEff.drawFrame(0, x + plusxy[0][0], y + plusxy[0][1], Dir, 3, g);
					fraImgSubEff.drawFrame(1, x + plusxy[3][0], y + plusxy[3][1], Dir, 3, g);
				}
				else if (f >= 12 && f <= 13)
				{
					fraImgEff.drawFrame(1, x + plusxy[1][0], y + plusxy[1][1], Dir, 3, g);
					fraImgSubEff.drawFrame(2, x + plusxy[4][0], y + plusxy[4][1], Dir, 3, g);
				}
				for (int num91 = 0; num91 < VecEff.size(); num91++)
				{
					Point point48 = (Point)VecEff.elementAt(num91);
					fraImgSub2Eff.drawFrame(point48.frame, point48.x, point48.y, Dir, 33, g);
				}
				break;
			}
			case 121:
				if (f >= 13)
				{
					fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x, y, Dir, 3, g);
				}
				break;
			case 122:
			{
				if (f >= 16)
				{
					fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x, y, Dir, 3, g);
					fraImgSubEff.drawFrame(f / 2 % 2, x1000, y1000, Dir, 0, g);
				}
				for (int num87 = 0; num87 < VecEff.size(); num87++)
				{
					Point_Focus point_Focus28 = (Point_Focus)VecEff.elementAt(num87);
					fraImgSub2Eff.drawFrame(point_Focus28.f % fraImgSub2Eff.nFrame, point_Focus28.x, point_Focus28.y, Dir, 33, g);
				}
				break;
			}
			case 123:
			case 185:
			case 217:
			case 283:
			{
				if ((f >= 9 && f <= 11) || (f >= 24 && f <= 26))
				{
					fraImgSub4Eff.drawFrame(0, objFireMain.x, objFireMain.y, Dir, 33, g);
				}
				if (f <= 11 || f >= 26)
				{
					if (typeEffect == 185 || typeEffect == 217 || typeEffect == 283)
					{
						int num43 = f / 2 % fraImgEff.nFrame;
						if (typeEffect == 217 || typeEffect == 283)
						{
							num43 += 2;
						}
						fraImgEff.drawFrameNew(num43, x + am_duong * 5, y, Dir, 3, g);
					}
					else
					{
						fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x, y, Dir, 3, g);
					}
					fraImgSubEff.drawFrame(f / 2 % 2, x1000, y1000, Dir, 0, g);
				}
				if (typeEffect == 283)
				{
					for (int num44 = 0; num44 < VecEff.size(); num44++)
					{
						Point_Focus point_Focus15 = (Point_Focus)VecEff.elementAt(num44);
						fraImgSub2Eff.drawFrame(point_Focus15.f % fraImgSub2Eff.nFrame, point_Focus15.x, point_Focus15.y, Dir, 33, g);
					}
					break;
				}
				for (int num45 = 0; num45 < VecEff.size(); num45++)
				{
					Point point25 = (Point)VecEff.elementAt(num45);
					if (point25.f >= 3 && (point25.f - 3) / 2 < 3)
					{
						fraImgSub2Eff.drawFrame((point25.f - 3) / 2, point25.x, point25.y, Dir, 3, g);
					}
					if (point25.f / 2 < 3)
					{
						fraImgSub3Eff.drawFrame(point25.f / 2, point25.x, point25.y, Dir, 3, g);
					}
				}
				break;
			}
			case 158:
			case 177:
				if (f >= 20 && f <= 25)
				{
					fraImgEff.drawFrame(0, objFireMain.x, objFireMain.y - objFireMain.dy, Dir, 33, g);
				}
				break;
			case 124:
			case 186:
			case 218:
				if (f >= 0 && f <= 5)
				{
					fraImgEff.drawFrame(0, objFireMain.x, objFireMain.y - objFireMain.dy, Dir, 33, g);
				}
				break;
			case 125:
			case 162:
			case 187:
				if (objFireMain.isTanHinh)
				{
					fraImgEff.drawFrame(0, objFireMain.x, objFireMain.y - objFireMain.dy, Dir, 33, g);
				}
				break;
			case 127:
			case 193:
			case 225:
			case 302:
				if (typeEffect == 302 && f > 2 && f < 15)
				{
					Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(0);
					fraImgSub3Eff.drawFrame((f / 3 >= fraImgSub3Eff.nFrame) ? (fraImgSub3Eff.nFrame - 1) : (f / 3), point_Focus2.x, point_Focus2.y, Dir ^ 2, 3, g);
				}
				if (f >= 7 && f <= 15)
				{
					fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, objFireMain.x + am_duong * 40, objFireMain.y - objFireMain.hOne / 2 - 10, Dir, 3, g);
				}
				if (f >= 15)
				{
					fraImgSub2Eff.drawFrame(0, x, y + 50, Dir, 3, g);
					fraImgSubEff.drawFrame(mframe[f / 2 % mframe.Length], x, y, Dir, 3, g);
				}
				break;
			case 133:
			{
				for (int num124 = 0; num124 < VecEff.size(); num124++)
				{
					Point_Focus point_Focus40 = (Point_Focus)VecEff.elementAt(num124);
					fraImgEff.drawFrame((point_Focus40.frame + point_Focus40.f / 2) % fraImgEff.nFrame, point_Focus40.x + CRes.random_Am_0(3), point_Focus40.y + CRes.random_Am_0(3), point_Focus40.dis, 3, g);
				}
				if (f >= 2 && f <= 4)
				{
					fraImgSubEff.drawFrame(f - 7, x + am_duong * 17, y, (Dir != 2) ? 2 : 0, 3, g);
				}
				if (f >= 10 && f <= 12)
				{
					fraImgSubEff.drawFrame(f - 15, x + am_duong * 17, y, (Dir != 2) ? 2 : 0, 3, g);
				}
				break;
			}
			case 134:
			case 135:
			{
				if (fraImgSub3Eff != null)
				{
					for (int num93 = 0; num93 < VecSubEff.size(); num93++)
					{
						Point point50 = (Point)VecSubEff.elementAt(num93);
						fraImgSub3Eff.drawFrame(1 + point50.f / 2, point50.x, point50.y, 0, 3, g);
					}
				}
				for (int num94 = 0; num94 < VecEff.size(); num94++)
				{
					Point_Focus point_Focus29 = (Point_Focus)VecEff.elementAt(num94);
					if (point_Focus29.maxdis == 1)
					{
						fraImgSub2Eff.drawFrameNew(point_Focus29.frame % fraImgSub2Eff.nFrame, point_Focus29.x + CRes.random_Am_0(5), point_Focus29.y + CRes.random_Am_0(5), point_Focus29.dis, 3, g);
					}
					else
					{
						fraImgEff.drawFrame((point_Focus29.frame + point_Focus29.f / 2) % fraImgEff.nFrame, point_Focus29.x + CRes.random_Am_0(5), point_Focus29.y + CRes.random_Am_0(5), point_Focus29.dis, 3, g);
					}
				}
				if (f >= 2 && f <= 4)
				{
					fraImgSubEff.drawFrame(f - 7, x + am_duong * 17, y, (Dir != 2) ? 2 : 0, 3, g);
				}
				if (f >= 5 && f <= 7)
				{
					fraImgSubEff.drawFrame(f - 15, x + am_duong * 17, y, (Dir != 2) ? 2 : 0, 3, g);
				}
				if (!checkNullObject(1) && f >= 10 && f <= 13)
				{
					fraImgSubEff.drawFrame(f - 15, x + am_duong * 17, y - objFireMain.dy, (Dir != 2) ? 2 : 0, 3, g);
				}
				break;
			}
			case 136:
				if (f == 4 || f == 10 || f == 14)
				{
					fraImgSubEff.drawFrame(0, objFireMain.x, objFireMain.y, Dir, 33, g);
				}
				if (f == 1 || f == 3 || f == 11 || f == 13)
				{
					fraImgEff.drawFrame(0, objFireMain.x, objFireMain.y, 0, 33, g);
				}
				break;
			case 137:
			case 138:
				if (f == 2 || f == 13 || f == 18)
				{
					fraImgSubEff.drawFrame(0, objFireMain.x, objFireMain.y, Dir, 33, g);
				}
				if (f == 3 || f == 12 || f == 19)
				{
					fraImgEff.drawFrame(0, objFireMain.x, objFireMain.y, 0, 33, g);
				}
				break;
			case 2:
			{
				for (int num65 = 0; num65 < VecEff.size(); num65++)
				{
					Point point35 = (Point)VecEff.elementAt(num65);
					fraImgEff.drawFrameNew(point35.frame, point35.x, point35.y, 0, 33, g);
				}
				break;
			}
			case 228:
			case 259:
			case 260:
			case 261:
			{
				for (int num51 = 0; num51 < VecSubEff.size(); num51++)
				{
					Point point28 = (Point)VecSubEff.elementAt(num51);
					fraImgSub2Eff.drawFrameNew_BeginSuper(point28.f, point28.x, point28.y, 0, 3, g);
				}
				for (int num52 = 0; num52 < VecEff.size(); num52++)
				{
					Point point29 = (Point)VecEff.elementAt(num52);
					fraImgEff.drawFrameNew_BeginSuper(point29.frame, point29.x, point29.y, 0, 33, g);
				}
				if (!checkNullObject(1) && f >= 4 && f <= 12)
				{
					fraImgSubEff.drawFrameNew_BeginSuper(f % fraImgSubEff.maxNumFrame, objFireMain.x - am_duong * 20, objFireMain.y - objFireMain.dy - 15, objFireMain.type_left_right, 3, g);
				}
				break;
			}
			case 3:
			case 229:
			case 262:
			case 263:
			case 264:
			{
				for (int num26 = 0; num26 < VecEff.size(); num26++)
				{
					Point point19 = (Point)VecEff.elementAt(num26);
					if (point19.frame == 0)
					{
						int num27 = 0;
						num27 = ((point19.f >= point19.fRe - 3) ? (fraImgSubEff.maxNumFrame - (point19.fRe - point19.f)) : (point19.f % 2));
						fraImgSubEff.drawFrameNew_BeginSuper(num27, point19.x / 1000, point19.y / 1000, 0, 3, g);
					}
					else
					{
						int num28 = 0;
						num28 = ((point19.f >= point19.fRe - 3) ? (fraImgEff.maxNumFrame - (point19.fRe - point19.f)) : ((point19.f + point19.fSmall) % 3));
						fraImgEff.drawFrameNew_BeginSuper(num28, point19.x / 1000, point19.y / 1000, 0, 3, g);
					}
				}
				for (int num29 = 0; num29 < VecSubEff.size(); num29++)
				{
					Point point20 = (Point)VecSubEff.elementAt(num29);
					if (point20.frame == 0)
					{
						fraImgSubEff.drawFrameNew_BeginSuper(point20.f % fraImgSubEff.maxNumFrame, point20.x, point20.y, 0, 3, g);
					}
					else if (point20.frame == 1)
					{
						fraImgSub2Eff.drawFrameNew_BeginSuper(point20.f / 2 % 3, point20.x, point20.y, 0, 33, g);
					}
				}
				if (f >= 13 && f <= 23)
				{
					fraImgSub3Eff.drawFrameNew_BeginSuper(f / 2 % 3, x, y + 8, 0, 33, g);
				}
				else if (f >= 8 && f <= 28)
				{
					fraImgEff.drawFrameNew_BeginSuper(f % 5, x, y + 3, 0, 33, g);
				}
				break;
			}
			case 402:
			{
				for (int num24 = 0; num24 < VecEff.size(); num24++)
				{
					Point point18 = (Point)VecEff.elementAt(num24);
					if (point18.frame != 0)
					{
						int num25 = 0;
						num25 = ((point18.f >= point18.fRe - 3) ? (fraImgEff.maxNumFrame - (point18.fRe - point18.f)) : ((point18.f + point18.fSmall) % 3));
						fraImgEff.drawFrameNew_BeginSuper(num25, point18.x / 1000, point18.y / 1000, 0, 3, g);
					}
				}
				break;
			}
			case 4:
			case 230:
			{
				if (f >= 0 && f < mframe.Length)
				{
					fraImgSub2Eff.drawFrame(mframe[f], x, y + 4, 0, 33, g);
				}
				for (int num18 = 0; num18 < VecSubEff.size(); num18++)
				{
					Point point13 = (Point)VecSubEff.elementAt(num18);
					fraImgSub3Eff.drawFrame(point13.f / 2 % fraImgSub3Eff.nFrame, point13.x, point13.y, 0, 3, g);
				}
				for (int num19 = 0; num19 < VecEff.size(); num19++)
				{
					Point point14 = (Point)VecEff.elementAt(num19);
					if (point14.f >= point14.fSmall)
					{
						if (point14.frame == 0)
						{
							fraImgEff.drawFrame(0, point14.x, point14.y, 0, 3, g);
						}
						else if (point14.frame == 1 && fraImgEff.getImageFrame() != null)
						{
							g.drawRegion(fraImgEff.getImageFrame(), 0, 0, fraImgEff.frameWidth, fraImgEff.frameHeight - point14.dis, 0, point14.x, point14.y, 33);
						}
					}
				}
				break;
			}
			case 5:
			case 231:
			{
				for (int num5 = 0; num5 < VecSubEff.size(); num5++)
				{
					Point point6 = (Point)VecSubEff.elementAt(num5);
					fraImgSub3Eff.drawFrame(point6.f / 2 % fraImgSub3Eff.nFrame, point6.x, point6.y, 0, 3, g);
				}
				if (f >= 10 && f <= 15)
				{
					fraImgEff.drawFrame(0, x + plusxy[0][0] * am_duong, y + plusxy[0][1], Dir, 3, g);
				}
				if (f > 15 && f <= 17)
				{
					fraImgEff.drawFrame(1, x + plusxy[1][0] * am_duong, y + plusxy[1][1], Dir, 3, g);
				}
				if (f > 17 && f <= 26)
				{
					fraImgSubEff.drawFrame((f - 18) / 3, x + plusxy[2][0] * am_duong, y + plusxy[2][1], Dir, 3, g);
				}
				break;
			}
			case 6:
			case 232:
			{
				if (f >= 20 && f <= 24)
				{
					fraImgEff.drawFrame((f - 30) / 2, x, y, Dir, 3, g);
				}
				for (int l = 0; l < VecSubEff.size(); l++)
				{
					Point point4 = (Point)VecSubEff.elementAt(l);
					if (point4.frame == 1)
					{
						fraImgSub3Eff.drawFrame(3 + point4.f % 3, point4.x, point4.y, 0, 3, g);
					}
					else
					{
						fraImgSub2Eff.drawFrame(point4.f % fraImgSub2Eff.nFrame, point4.x, point4.y, 0, 3, g);
					}
				}
				for (int m = 0; m < VecEff.size(); m++)
				{
					Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(m);
					fraImgSubEff.drawFrame(point_Focus.frame / 2, point_Focus.x, point_Focus.y, Dir, 3, g);
				}
				break;
			}
			case 10:
			case 234:
			{
				if (!checkNullObject(1))
				{
					if (f >= 7)
					{
						fraImgEff.drawFrame((f - 7) / 2, objFireMain.x, objFireMain.y, Dir, 33, g);
					}
					if (f >= 7 && f <= 16)
					{
						fraImgSubEff.drawFrame((f - 11) / 2 % fraImgSubEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy + 5, Dir, 33, g);
					}
					if (f >= 24 && f <= 29)
					{
						fraImgSubEff.drawFrame((2 - (f - 34)) / 2 % fraImgSubEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy + 5, Dir, 33, g);
					}
				}
				for (int num116 = 0; num116 < VecSubEff.size(); num116++)
				{
					Point point59 = (Point)VecSubEff.elementAt(num116);
					if (point59.frame == 1)
					{
						fraImgSub4Eff.drawFrame(3 + point59.f % 3, point59.x, point59.y, 0, 3, g);
					}
					else
					{
						fraImgSub3Eff.drawFrame(point59.f % fraImgSub3Eff.nFrame, point59.x, point59.y, 0, 3, g);
					}
				}
				for (int num117 = 0; num117 < VecEff.size(); num117++)
				{
					Point_Focus point_Focus36 = (Point_Focus)VecEff.elementAt(num117);
					fraImgSub2Eff.drawFrame(point_Focus36.f / 2 % fraImgSub2Eff.nFrame, point_Focus36.x, point_Focus36.y, Dir, 3, g);
				}
				break;
			}
			case 147:
				fraImgEff.drawFrame(5, x, y, Dir, 3, g);
				break;
			case 149:
			{
				int num112 = f;
				if (num112 > 2)
				{
					num112 = 2;
				}
				fraImgEff.drawFrame(num112, x, y, Dir, 3, g);
				break;
			}
			case 13:
			case 258:
			{
				if (!checkNullObject(1))
				{
					if (f >= 7 && f <= 12)
					{
						fraImgEff.drawFrame((f - 7) / 2, objFireMain.x, objFireMain.y, Dir, 33, g);
					}
					if (f >= 9 && f <= 11)
					{
						fraImgSubEff.drawFrame((f - 9) % fraImgSubEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy + 5, Dir, 33, g);
					}
					if (f >= 18 && f <= 20)
					{
						fraImgSubEff.drawFrame((2 - (f - 18)) % fraImgSubEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy + 5, Dir, 33, g);
					}
				}
				for (int num99 = 0; num99 < VecEff.size(); num99++)
				{
					Point_Focus point_Focus31 = (Point_Focus)VecEff.elementAt(num99);
					if (typeEffect == 13)
					{
						fraImgSub2Eff.drawFrame(0, point_Focus31.x, point_Focus31.y, Dir, 3, g);
					}
					else if (fraImgSub2Eff.getImageFrame() != null)
					{
						g.drawRegion(fraImgSub2Eff.getImageFrame(), 0, 0, fraImgSub2Eff.frameWidth, 62, 0, point_Focus31.x, point_Focus31.y, 33);
					}
				}
				for (int num100 = 0; num100 < VecSubEff.size(); num100++)
				{
					Point point53 = (Point)VecSubEff.elementAt(num100);
					fraImgSub3Eff.drawFrame(point53.f % fraImgSub3Eff.nFrame, point53.x, point53.y, 0, 3, g);
				}
				break;
			}
			case 18:
			{
				for (int num95 = 0; num95 < VecSubEff.size(); num95++)
				{
					Point point51 = (Point)VecSubEff.elementAt(num95);
					fraImgSubEff.drawFrame(point51.f / 2 % fraImgSubEff.nFrame, point51.x, point51.y, Dir, 3, g);
				}
				for (int num96 = 0; num96 < VecEff.size(); num96++)
				{
					Point_Focus point_Focus30 = (Point_Focus)VecEff.elementAt(num96);
					fraImgEff.drawFrame(0, point_Focus30.x, point_Focus30.y, frame, 3, g);
				}
				break;
			}
			case 30:
				if (f < 3)
				{
					fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				}
				else
				{
					fraImgSubEff.drawFrame(0, x1000, y1000, Dir, 3, g);
				}
				break;
			case 73:
			case 74:
				if (f < 2)
				{
					fraImgEff.drawFrame(0, x, y, Dir, 3, g);
				}
				break;
			case 10025:
				fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x, y, Dir, 3, g);
				break;
			case 10001:
				paintPan_1(g);
				break;
			case 10002:
				if (f < 6)
				{
					fraImgEff.drawFrame(f % fraImgEff.nFrame, x, y - objFireMain.dy, Dir, 33, g);
				}
				if (f >= 13 && f <= 18)
				{
					fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.dy, Dir, 33, g);
				}
				break;
			case 10003:
				paintGalio_1(g);
				break;
			case 10004:
			{
				if (f < 4)
				{
					fraImgSubEff.drawFrame(f / 2 % fraImgSubEff.nFrame, x, y, Dir, 3, g);
				}
				for (int num81 = 0; num81 < VecEff.size(); num81++)
				{
					Point point41 = (Point)VecEff.elementAt(num81);
					fraImgEff.drawFrame(point41.f / 2 % fraImgEff.nFrame, point41.x, point41.y, Dir, 33, g);
				}
				break;
			}
			case 10006:
			case 10011:
			{
				for (int num78 = 0; num78 < vecPos.size(); num78++)
				{
					Point_Focus point_Focus27 = (Point_Focus)vecPos.elementAt(num78);
					fraImgSubEff.drawFrame(point_Focus27.frame + point_Focus27.f / 3 % 2, point_Focus27.x, point_Focus27.y, 0, mGraphics.BOTTOM | mGraphics.RIGHT, g);
					fraImgSubEff.drawFrame(point_Focus27.frame + point_Focus27.f / 3 % 2, point_Focus27.x, point_Focus27.y, 2, mGraphics.BOTTOM | mGraphics.LEFT, g);
					fraImgSubEff.drawFrame(point_Focus27.frame + point_Focus27.f / 3 % 2, point_Focus27.x, point_Focus27.y, 1, mGraphics.TOP | mGraphics.RIGHT, g);
					fraImgSubEff.drawFrame(point_Focus27.frame + point_Focus27.f / 3 % 2, point_Focus27.x, point_Focus27.y, 3, 0, g);
					g.setColor(0);
					g.fillRect(point_Focus27.x - 1, point_Focus27.y - 1, 3, 3);
				}
				break;
			}
			case 10007:
			{
				for (int num77 = 0; num77 < vecPos.size(); num77++)
				{
					Point_Focus point_Focus26 = (Point_Focus)vecPos.elementAt(num77);
					fraImgEff.drawFrame(point_Focus26.f / 2 % fraImgEff.nFrame, point_Focus26.x, point_Focus26.y, Dir, 33, g);
					fraImgSubEff.drawFrame(point_Focus26.f / 2 % fraImgSubEff.nFrame, point_Focus26.x, point_Focus26.y, Dir, 3, g);
				}
				break;
			}
			case 10008:
			{
				for (int num74 = 0; num74 < VecSubEff.size(); num74++)
				{
					Point point40 = (Point)VecSubEff.elementAt(num74);
					fraImgEff.drawFrame(point40.frame, point40.x, point40.y, Dir, 3, g);
				}
				break;
			}
			case 10012:
			{
				if (f <= fRemove)
				{
					fraImgEff.drawFrame(1, x1000 / 1000, y1000, Dir, 3, g);
				}
				for (int num72 = 0; num72 < VecEff.size(); num72++)
				{
					Point point39 = (Point)VecEff.elementAt(num72);
					point39.fraImgEff.drawFrame(point39.f / 2, point39.x, point39.y, Dir, 3, g);
				}
				break;
			}
			case 10010:
			case 10013:
				fraImgEff.drawFrame(GameCanvas.gameTick / numNextFrame % fraImgEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, Dir, 3, g);
				break;
			case 10015:
			{
				for (int num70 = 0; num70 < VecEff.size(); num70++)
				{
					Point point38 = (Point)VecEff.elementAt(num70);
					fraImgEff.drawFrame(point38.frame, objFireMain.x, objFireMain.y + point38.y, Dir, 33, g);
				}
				break;
			}
			case 10017:
				fraImgEff.drawFrame(GameCanvas.gameTick / numNextFrame % fraImgEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.hOne / 4, Dir, 3, g);
				break;
			case 10020:
			case 10021:
			case 10022:
			case 10026:
				fraImgEff.drawFrame(GameCanvas.gameTick / numNextFrame % fraImgEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, Dir, 3, g);
				break;
			case 10024:
			{
				fraImgEff.drawFrame(GameCanvas.gameTick / numNextFrame % fraImgEff.nFrame, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, Dir, 3, g);
				for (int num68 = 0; num68 < vecPos.size(); num68++)
				{
					Point_Focus point_Focus21 = (Point_Focus)vecPos.elementAt(num68);
					fraImgSubEff.drawFrame(point_Focus21.frame + point_Focus21.f / 3 % 2, point_Focus21.x, point_Focus21.y, 0, mGraphics.BOTTOM | mGraphics.RIGHT, g);
					fraImgSubEff.drawFrame(point_Focus21.frame + point_Focus21.f / 3 % 2, point_Focus21.x, point_Focus21.y, 2, mGraphics.BOTTOM | mGraphics.LEFT, g);
					fraImgSubEff.drawFrame(point_Focus21.frame + point_Focus21.f / 3 % 2, point_Focus21.x, point_Focus21.y, 1, mGraphics.TOP | mGraphics.RIGHT, g);
					fraImgSubEff.drawFrame(point_Focus21.frame + point_Focus21.f / 3 % 2, point_Focus21.x, point_Focus21.y, 3, 0, g);
				}
				break;
			}
			case 10018:
			{
				for (int num66 = 0; num66 < VecEff.size(); num66++)
				{
					Point point36 = (Point)VecEff.elementAt(num66);
					fraImgEff.drawFrame(point36.frame, point36.x, point36.y, Dir, 33, g);
				}
				break;
			}
			case 165:
			case 166:
			{
				int num64 = f / 2 % 6;
				if (num64 < 2)
				{
					fraImgEff.drawFrame(num64, x, y, Dir, 33, g);
				}
				break;
			}
			case 20:
				if (f >= 17 && f <= 24)
				{
					fraImgEff.drawFrame((f - 17) / 2, x, y, Dir, 33, g);
				}
				break;
			case 23:
				if (f < fPlayFrameSuper)
				{
					fraImgEff.drawFrame(3, x, y, 0, 3, g);
				}
				break;
			case 25:
			case 235:
				paintCrocodile1(g);
				break;
			case 26:
			case 236:
				paintCrocodile2(g);
				break;
			case 27:
				if (f % 4 < 2)
				{
					paint_Bullet(g, fraImgEff, frame, x, y, isMore: false, 0);
				}
				else
				{
					paint_Bullet(g, fraImgSubEff, frame, x, y, isMore: false, 0);
				}
				break;
			case 28:
				fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x, y, 0, 3, g);
				break;
			case 32:
			{
				for (int num56 = 0; num56 < VecEff.size(); num56++)
				{
					Point point31 = (Point)VecEff.elementAt(num56);
					if (point31.frame == 0)
					{
						fraImgEff.drawFrame(0, point31.x, point31.y, Dir, 33, g);
					}
					else
					{
						fraImgSubEff.drawFrame(point31.f / 2 % fraImgSubEff.nFrame, point31.x, point31.y, Dir, 33, g);
					}
				}
				break;
			}
			case 39:
			{
				for (int num50 = 0; num50 < VecEff.size(); num50++)
				{
					Point_Focus point_Focus17 = (Point_Focus)VecEff.elementAt(num50);
					fraImgEff.drawFrame(2, point_Focus17.x, point_Focus17.y, Dir, 33, g);
				}
				break;
			}
			case 40:
				if (Dir == 0)
				{
					g.setColor(15956504);
					g.fillRect(x, y - 3, x1000 - x, 6);
					g.setColor(15985419);
					g.fillRect(x, y - 2, x1000 - x, 4);
					g.setColor(16777215);
					g.fillRect(x, y - 1, x1000 - x, 2);
				}
				else
				{
					g.setColor(15956504);
					g.fillRect(x1000, y - 3, x, 6);
					g.setColor(15985419);
					g.fillRect(x1000, y - 2, x, 4);
					g.setColor(16777215);
					g.fillRect(x1000, y - 1, x, 2);
				}
				break;
			case 45:
			{
				for (int num53 = 0; num53 < VecEff.size(); num53++)
				{
					Point_Focus point_Focus18 = (Point_Focus)VecEff.elementAt(num53);
					fraImgEff.drawFrame(1, point_Focus18.x, point_Focus18.y, point_Focus18.dis, 3, g);
				}
				break;
			}
			case 54:
			{
				for (int num38 = 0; num38 < VecEff.size(); num38++)
				{
					Point_Focus point_Focus13 = (Point_Focus)VecEff.elementAt(num38);
					if (point_Focus13.frame == 0)
					{
						fraImgEff.drawFrame(0, point_Focus13.x, point_Focus13.y, point_Focus13.dis, 3, g);
					}
					else
					{
						fraImgSub2Eff.drawFrame(point_Focus13.f / 2 % 3, point_Focus13.x, point_Focus13.y, point_Focus13.dis, 3, g);
					}
				}
				for (int num39 = 0; num39 < VecSubEff.size(); num39++)
				{
					Point point22 = (Point)VecSubEff.elementAt(num39);
					if (point22.obj != null && !point22.obj.returnAction())
					{
						if (point22.frame == 0)
						{
							fraImgEff.drawFrame(2, point22.obj.x, point22.obj.y - point22.obj.hOne / 2, point22.dis, 3, g);
						}
						else if (point22.frame == 1)
						{
							fraImgSubEff.drawFrame(point22.f / 2 % fraImgSubEff.nFrame, point22.obj.x, point22.obj.y - point22.obj.hOne / 2 + 5, point22.dis, 33, g);
						}
					}
				}
				break;
			}
			case 59:
				if (!checkNullObject(2))
				{
					if (f < 6)
					{
						fraImgEff.drawFrame(f / 2, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, 0, 3, g);
					}
					else if (f % 4 < 2)
					{
						fraImgEff.drawFrame(3, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, 0, 3, g);
					}
				}
				break;
			case 60:
				if (!checkNullObject(2))
				{
					if (f < 9)
					{
						fraImgEff.drawFrame(4 + f / 3, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, 0, 3, g);
					}
					else if (f % 4 < 3)
					{
						fraImgEff.drawFrame(7, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, 0, 3, g);
					}
				}
				break;
			case 62:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, toX, toY, 0, 33, g);
				break;
			case 167:
			{
				for (int num9 = 0; num9 < VecSubEff.size(); num9++)
				{
					Point point9 = (Point)VecSubEff.elementAt(num9);
					fraImgSub2Eff.drawFrame(point9.frame, point9.x, point9.y, 0, 33, g);
				}
				for (int num10 = 0; num10 < VecEff.size(); num10++)
				{
					Point point10 = (Point)VecEff.elementAt(num10);
					fraImgSub3Eff.drawFrameNew(point10.f / 3, point10.x, point10.y, 0, 33, g);
				}
				if (f >= 4 && f <= 5)
				{
					g.drawRegion(fraImgSub4Eff.getImageFrame(), 0, 0, fraImgSub4Eff.frameWidth, fraImgSub4Eff.frameHeight / (f - 3), 0, x1000, y1000, 33);
				}
				if (f > 4)
				{
					if (f > fRemove - 4)
					{
						fraImgSub2Eff.drawFrame(fRemove - f, x1000, y1000, 0, 33, g);
					}
					else
					{
						fraImgSub2Eff.drawFrame(3, x1000, y1000, 0, 33, g);
					}
				}
				if (f < 8)
				{
					fraImgSubEff.drawFrame(f / 2, x1000, y1000, 0, 33, g);
				}
				break;
			}
			case -1:
			{
				for (int num6 = 0; num6 < VecEff.size(); num6++)
				{
					Point point7 = (Point)VecEff.elementAt(num6);
					fraImgSubEff.drawFrame(point7.f / 2 % fraImgSubEff.nFrame, point7.x, point7.y, 0, 3, g);
				}
				break;
			}
			case 10028:
				if (f > 16)
				{
					int num3 = -4;
					int num4 = GameCanvas.gameTick % 2 * 2;
					if (y1000 == 0)
					{
						fraImgSub3Eff.drawFrameNew(GameCanvas.gameTick / 3 % fraImgSub3Eff.nFrame, x, y + 7, 0, 3, g);
					}
					g.setColor(1513500);
					g.fillRect(x - 20 - num3, y - 350 - y1000, 40 + num3 * 2, 360);
					g.setColor(1908256);
					g.fillRect(x - 18 - num3, y - 350 - y1000, 36 + num3 * 2, 360);
					g.setColor(7040880);
					g.fillRect(x - 16 - num3, y - 350 - y1000, 32 + num3 * 2, 360);
					g.setColor(2500651);
					g.fillRect(x - 14 - num3 + num4, y - 350 - y1000, 28 + num3 * 2 - num4 * 2, 360);
					g.setColor(9540757);
					g.fillRect(x - 12 - num3 + num4, y - 350 - y1000, 24 + num3 * 2 - num4 * 2, 360);
					g.setColor(16514814);
					g.fillRect(x - 10 - num3 + num4, y - 350 - y1000, 20 + num3 * 2 - num4 * 2, 360);
				}
				break;
			case 169:
			case 237:
			{
				if (f <= 20 && !checkNullObject(1))
				{
					fraImgSub4Eff.drawFrame(CRes.random(fraImgSub4Eff.nFrame), objFireMain.x, objFireMain.y - objFireMain.hOne / 2 + 3, 0, 3, g);
				}
				for (int num128 = 0; num128 < VecSubEff.size(); num128++)
				{
					if (num128 % 2 == 1)
					{
						Point point61 = (Point)VecSubEff.elementAt(num128);
						fraImgSubEff.drawFrameNew(point61.frame * fraImgSubEff.maxNumFrame + GameCanvas.gameTick / 2 % fraImgSubEff.maxNumFrame, point61.x / 10, point61.y / 10, 0, 3, g);
						for (int num129 = 0; num129 < 4; num129++)
						{
							fraImgEff.drawFrameNew(point61.frame * fraImgEff.maxNumFrame, point61.x / 10, point61.y / 10 - num129 * 73, (CRes.random(2) != 0) ? 2 : 0, 33, g);
						}
					}
				}
				if (f > 16)
				{
					int num130 = -4;
					int num131 = GameCanvas.gameTick % 2 * 2;
					if (y1000 == 0)
					{
						fraImgSub3Eff.drawFrameNew(GameCanvas.gameTick / 3 % fraImgSub3Eff.nFrame, x, y + 7, 0, 3, g);
					}
					g.setColor(407521);
					g.fillRect(x - 20 - num130, y - 350 - y1000, 40 + num130 * 2, 360);
					g.setColor(31983);
					g.fillRect(x - 18 - num130, y - 350 - y1000, 36 + num130 * 2, 360);
					g.setColor(11661052);
					g.fillRect(x - 16 - num130, y - 350 - y1000, 32 + num130 * 2, 360);
					g.setColor(31983);
					g.fillRect(x - 14 - num130 + num131, y - 350 - y1000, 28 + num130 * 2 - num131 * 2, 360);
					g.setColor(11661052);
					g.fillRect(x - 12 - num130 + num131, y - 350 - y1000, 24 + num130 * 2 - num131 * 2, 360);
					g.setColor(16514814);
					g.fillRect(x - 10 - num130 + num131, y - 350 - y1000, 20 + num130 * 2 - num131 * 2, 360);
				}
				for (int num132 = 0; num132 < VecEff.size(); num132++)
				{
					Point point62 = (Point)VecEff.elementAt(num132);
					fraImgSub2Eff.drawFrame(point62.f / 2, point62.x, point62.y, 0, 3, g);
				}
				for (int num133 = 0; num133 < VecSubEff.size(); num133++)
				{
					if (num133 % 2 == 0)
					{
						Point point63 = (Point)VecSubEff.elementAt(num133);
						fraImgSubEff.drawFrameNew(point63.frame * fraImgSubEff.maxNumFrame + GameCanvas.gameTick / 2 % fraImgSubEff.maxNumFrame, point63.x / 10, point63.y / 10, 0, 3, g);
						for (int num134 = 0; num134 < 4; num134++)
						{
							fraImgEff.drawFrameNew(point63.frame * fraImgEff.maxNumFrame, point63.x / 10, point63.y / 10 - num134 * 73, (CRes.random(2) != 0) ? 2 : 0, 33, g);
						}
					}
				}
				break;
			}
			case 170:
			case 238:
			{
				if (f <= 20 && !checkNullObject(1))
				{
					fraImgSub4Eff.drawFrame(CRes.random(fraImgSub4Eff.nFrame), objFireMain.x, objFireMain.y - objFireMain.hOne / 2 + 3, 0, 3, g);
				}
				for (int num120 = 0; num120 < VecEff.size(); num120++)
				{
					Point_Focus point_Focus39 = (Point_Focus)VecEff.elementAt(num120);
					int num121 = 0;
					if (Dir == 2)
					{
						num121 = 2;
					}
					if (point_Focus39.f >= point_Focus39.fRe)
					{
						if (fraImgEff.getImageFrame() != null && point_Focus39.f % 5 != 2 && point_Focus39.f < point_Focus39.fRe + 8)
						{
							g.drawRegion(fraImgEff.getImageFrame(), point_Focus39.typeSpec * fraImgEff.frameWidth, point_Focus39.frame * fraImgEff.frameHeight, fraImgEff.frameWidth, point_Focus39.maxdis, num121, point_Focus39.x, point_Focus39.y, 33);
						}
					}
					else if (point_Focus39.f % 5 != 2)
					{
						fraImgEff.drawFrameNew(point_Focus39.typeSpec * fraImgEff.maxNumFrame + point_Focus39.frame, point_Focus39.x, point_Focus39.y, num121, 3, g);
					}
				}
				for (int num122 = 0; num122 < VecSubEff.size(); num122++)
				{
					Point point60 = (Point)VecSubEff.elementAt(num122);
					fraImgSub3Eff.drawFrameNew(point60.frame * fraImgSub3Eff.maxNumFrame + GameCanvas.gameTick / 2 % fraImgSub3Eff.maxNumFrame, point60.x / 10, point60.y / 10, 0, 3, g);
					for (int num123 = 0; num123 < 4; num123++)
					{
						fraImgSub2Eff.drawFrameNew(point60.frame * fraImgSub2Eff.maxNumFrame, point60.x / 10, point60.y / 10 - num123 * 73, (CRes.random(2) != 0) ? 2 : 0, 33, g);
					}
				}
				break;
			}
			case 171:
			case 239:
			{
				if (f < 20 && !checkNullObject(1) && (f <= 8 || f >= 13))
				{
					fraImgSub3Eff.drawFrame(f / 2 % fraImgSub3Eff.nFrame, objFireMain.x, objFireMain.y + objFireMain.dy, Dir, 33, g);
				}
				for (int num108 = VecEff.size() - 1; num108 >= 0; num108--)
				{
					Point point55 = (Point)VecEff.elementAt(num108);
					if (point55.frame == 0 && point55.fSmall >= 2)
					{
						fraImgEff.drawFrame(point55.f / 2 % fraImgEff.nFrame, point55.x / 1000, point55.y / 1000, 0, 33, g);
					}
					else if (point55.frame == 1 && point55.fSmall == 3)
					{
						fraImgSubEff.drawFrame(point55.f / 2 % fraImgSubEff.nFrame, point55.x / 1000, point55.y / 1000, 0, 33, g);
					}
				}
				if (f > 6 && f < fRemove)
				{
					int num109 = -4;
					int num110 = GameCanvas.gameTick % 2 * 2;
					fraImgSub2Eff.drawFrame(GameCanvas.gameTick / 3 % fraImgSub2Eff.nFrame, x, y - 3, 0, 3, g);
					g.setColor(16722432);
					g.fillRect(x - 20 - num109, y - y1000, 40 + num109 * 2, y1000);
					g.setColor(16745472);
					g.fillRect(x - 18 - num109, y - y1000, 36 + num109 * 2, y1000);
					g.setColor(16765184);
					g.fillRect(x - 16 - num109, y - y1000, 32 + num109 * 2, y1000);
					g.setColor(16745472);
					g.fillRect(x - 14 - num109 + num110, y - y1000, 28 + num109 * 2 - num110 * 2, y1000);
					g.setColor(16765184);
					g.fillRect(x - 12 - num109 + num110, y - y1000, 24 + num109 * 2 - num110 * 2, y1000);
					g.setColor(16777085);
					g.fillRect(x - 10 - num109 + num110, y - y1000, 20 + num109 * 2 - num110 * 2, y1000);
				}
				for (int num111 = VecEff.size() - 1; num111 >= 0; num111--)
				{
					Point point56 = (Point)VecEff.elementAt(num111);
					if (point56.frame == 0 && point56.fSmall < 2)
					{
						fraImgEff.drawFrame(point56.f / 2 % fraImgEff.nFrame, point56.x / 1000, point56.y / 1000, 0, 33, g);
					}
					else if (point56.frame == 1 && point56.fSmall != 3)
					{
						fraImgSubEff.drawFrame(point56.f / 2 % fraImgSubEff.nFrame, point56.x / 1000, point56.y / 1000, 0, 33, g);
					}
				}
				break;
			}
			case 172:
			case 240:
				if (f < 20 && !checkNullObject(1) && (f <= 8 || f >= 13))
				{
					fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, objFireMain.x, objFireMain.y + objFireMain.dy, Dir, 33, g);
				}
				break;
			case 168:
				if (f < 12)
				{
					int num101 = Mr1[f / 2][1];
					int num102 = Mr1[f / 2][2];
					int trans9 = 0;
					if (!checkNullObject(1) && objFireMain.Dir == 2)
					{
						trans9 = 2;
						num101 = -Mr1[f / 2][1];
					}
					fraImgEff.drawFrameNew(f / 2 % fraImgEff.nFrame, x + num101, y + num102, trans9, 3, g);
				}
				break;
			case 173:
				paintEff_Mr1_2(g);
				break;
			case 174:
				if (f >= 4 && f <= fRemove && !checkNullObject(1))
				{
					int idx3 = 1;
					if (f < 6)
					{
						idx3 = 0;
					}
					fraImgEff.drawFrame(idx3, objFireMain.x + am_duong * 36, objFireMain.y - 25, objFireMain.type_left_right, 3, g);
				}
				break;
			case 175:
				paintEff_Df_2(g);
				break;
			case 178:
				paintEff_Mr0_1(g);
				break;
			case 179:
			case 241:
			{
				if ((f >= 1 && f <= 2) || (f >= 24 && f <= 25))
				{
					fraImgEff.drawFrame(0, x + am_duong * 5, y, Dir, 3, g);
				}
				for (int num88 = 0; num88 < VecSubEff.size(); num88++)
				{
					Point point45 = (Point)VecSubEff.elementAt(num88);
					if (point45.frame == 0)
					{
						if (frame == 1)
						{
							fraImgSub4Eff.drawFrameNew(point45.f / 2 % fraImgSub4Eff.nFrame, point45.x, point45.y, point45.dis, 3, g);
						}
						else
						{
							fraImgSubEff.drawFrameNew((point45.f + point45.frame) % fraImgSubEff.nFrame, point45.x, point45.y, point45.dis, 3, g);
						}
					}
				}
				for (int num89 = 0; num89 < VecEff.size(); num89++)
				{
					Point point46 = (Point)VecEff.elementAt(num89);
					if (frame == 1)
					{
						fraImgSubEff.drawFrame(point46.f % fraImgSubEff.nFrame, point46.x, point46.y, point46.dis, 3, g);
						fraImgSub3Eff.drawFrame(0, point46.x, point46.y + 60, point46.dis, 3, g);
						if (point46.f % 2 == 0)
						{
							fraImgSub2Eff.drawFrameNew(step * fraImgSub2Eff.maxNumFrame + point46.f / 3 % fraImgSub2Eff.maxNumFrame, point46.x + am_duong * 10, point46.y + 5, point46.dis, 3, g);
						}
					}
					else
					{
						fraImgSubEff.drawFrameNew(point46.f % fraImgSubEff.nFrame, point46.x, point46.y, point46.dis, 3, g);
					}
				}
				for (int num90 = 0; num90 < VecSubEff.size(); num90++)
				{
					Point point47 = (Point)VecSubEff.elementAt(num90);
					if (point47.frame == 1 && frame == 1)
					{
						fraImgSub4Eff.drawFrameNew(point47.f / 2 % fraImgSub4Eff.nFrame, point47.x, point47.y, point47.dis, 3, g);
					}
				}
				break;
			}
			case 195:
			{
				for (int num85 = 0; num85 < VecEff.size(); num85++)
				{
					Point point44 = (Point)VecEff.elementAt(num85);
					for (int num86 = 0; num86 < 4; num86++)
					{
						fraImgEff.drawFrame(0, point44.x / 10, point44.y / 10 - 73 - num86 * 73, CRes.random(2) * 2, 0, g);
					}
					fraImgSubEff.drawFrame(point44.f / 2 % fraImgSubEff.nFrame, point44.x / 10 + 15, point44.y / 10 + 4, CRes.random(2) * 2, 33, g);
				}
				break;
			}
			case 196:
			{
				for (int num83 = 0; num83 < VecEff.size(); num83++)
				{
					Point point42 = (Point)VecEff.elementAt(num83);
					fraImgEff.drawFrame(point42.f % fraImgEff.nFrame, point42.x, point42.y, Dir, 3, g);
				}
				for (int num84 = 0; num84 < VecSubEff.size(); num84++)
				{
					Point point43 = (Point)VecSubEff.elementAt(num84);
					if (point43.frame == 0)
					{
						fraImgSub2Eff.drawFrame(point43.f % fraImgSub2Eff.nFrame, point43.x, point43.y, Dir, 3, g);
						continue;
					}
					fraImgSubEff.drawFrame(point43.f / 2 % fraImgSubEff.nFrame, point43.x - 50, point43.y - 50, 0, 0, g);
					fraImgSubEff.drawFrame(point43.f / 2 % fraImgSubEff.nFrame, point43.x, point43.y - 50, 2, 0, g);
				}
				break;
			}
			case 197:
			{
				int num79 = 30 + f / 2 * 15;
				int num80 = 0;
				if (num79 > 76)
				{
					num79 = 76;
				}
				if (Dir == 0)
				{
					num80 = num79;
				}
				if (fraImgEff.getImageFrame() != null)
				{
					g.drawRegion(fraImgEff.getImageFrame(), 0, 0, num79, 27, Dir, x - num80, y - 13, 0);
				}
				break;
			}
			case 198:
			{
				if (f < 8)
				{
					fraImgEff.drawFrame(f / 4, x, y, Dir, 3, g);
				}
				for (int num76 = 0; num76 < VecEff.size(); num76++)
				{
					Point_Focus point_Focus25 = (Point_Focus)VecEff.elementAt(num76);
					fraImgEff.drawFrame(point_Focus25.frame, point_Focus25.x, point_Focus25.y, 0, 3, g);
				}
				break;
			}
			case 199:
				if (!checkNullObject(1) && objFireMain.isTanHinh)
				{
					fraImgEff.drawFrame(0, x, y, Dir, 33, g);
				}
				break;
			case 200:
			{
				if (f < 20)
				{
					fraImgSubEff.drawFrame(f / 2 % fraImgSubEff.nFrame, x + 4 * am_duong, y - f % 4 / 2 * 3 + 2, Dir, 3, g);
				}
				for (int num73 = 0; num73 < VecEff.size(); num73++)
				{
					Point_Focus point_Focus23 = (Point_Focus)VecEff.elementAt(num73);
					fraImgEff.drawFrame((point_Focus23.frame + point_Focus23.f) % fraImgEff.nFrame, point_Focus23.x, point_Focus23.y, Dir, 3, g);
				}
				break;
			}
			case 202:
			{
				for (int num69 = 0; num69 < VecEff.size(); num69++)
				{
					Point_Focus point_Focus22 = (Point_Focus)VecEff.elementAt(num69);
					fraImgEff.drawFrame(point_Focus22.f / 2 % 2, point_Focus22.x, point_Focus22.y, 0, 3, g);
				}
				if (!checkNullObject(1))
				{
					if (f < 10)
					{
						fraImgEff.drawFrame(f / 2, x, objFireMain.y - objFireMain.hOne - 15, 0, 3, g);
					}
					else if (f < 12)
					{
						fraImgEff.drawFrame(2, x + am_duong * 20, objFireMain.y - objFireMain.hOne / 2 - 20, 0, 3, g);
					}
				}
				break;
			}
			case 203:
				if (!checkNullObject(1) && objFireMain.isTanHinh)
				{
					fraImgEff.drawFrame(0, x, y, Dir, 33, g);
				}
				break;
			case 204:
			{
				for (int num67 = 0; num67 < VecEff.size(); num67++)
				{
					Point point37 = (Point)VecEff.elementAt(num67);
					fraImgSubEff.drawFrame(point37.f % fraImgSubEff.nFrame, point37.x, point37.y, point37.dis, 3, g);
					fraImgSub2Eff.drawFrame(0, point37.x, point37.y + 60, point37.dis, 3, g);
				}
				break;
			}
			case 205:
			{
				if (f > 10 && f <= fRemove)
				{
					fraImgEff.drawFrame(0, x + x1000 / 1000, y + y1000 / 1000, 0, 3, g);
				}
				for (int num63 = 0; num63 < VecEff.size(); num63++)
				{
					Point point34 = (Point)VecEff.elementAt(num63);
					fraImgEff.drawFrame(1 + point34.f / 3, point34.x, point34.y, 0, 3, g);
				}
				break;
			}
			case 207:
			{
				if (f < fPlayFrameSuper)
				{
					fraImgEff.drawFrame(3, x, y, 0, 3, g);
				}
				for (int num60 = 0; num60 < VecEff.size(); num60++)
				{
					Point point33 = (Point)VecEff.elementAt(num60);
					fraImgSubEff.drawFrame(point33.f / 2 % fraImgSubEff.nFrame, point33.x, point33.y, 0, 3, g);
				}
				break;
			}
			case 206:
			{
				if (f < fPlayFrameSuper)
				{
					paint_Bullet(g, fraImgEff, frame, x, y, isMore: false, 0);
				}
				for (int num59 = 0; num59 < VecEff.size(); num59++)
				{
					Point point32 = (Point)VecEff.elementAt(num59);
					fraImgSubEff.drawFrame(point32.f / 2 % fraImgSubEff.nFrame, point32.x, point32.y, 0, 3, g);
				}
				break;
			}
			case 208:
				paintEffTru(g);
				break;
			case 250:
				paintEffTru2(g);
				break;
			case 209:
			case 242:
				if (typeEffect == 242 && objFireMain != null && f < 11)
				{
					objFireMain.paintBody(g, objFireMain.x + am_duong * 120, objFireMain.y, objFireMain.frame, (objFireMain.type_left_right == 0) ? 2 : 0, isEye: true);
				}
				if (frame == 1)
				{
					for (int num46 = 0; num46 < VecEff.size(); num46++)
					{
						Point point26 = (Point)VecEff.elementAt(num46);
						if (fraImgSubEff != null && fraImgSubEff.imgFrame != null)
						{
							fraImgSubEff.drawFrameNew(CRes.random(fraImgSubEff.maxNumFrame), point26.x, point26.y, 0, 3, g);
						}
						fraImgEff.drawFrameNew(6 + point26.frame, point26.x, point26.y, 0, 3, g);
					}
				}
				else
				{
					for (int num47 = 0; num47 < VecEff.size(); num47++)
					{
						Point point27 = (Point)VecEff.elementAt(num47);
						fraImgEff.drawFrameNew(6 + point27.frame, point27.x, point27.y, 0, 3, g);
					}
				}
				if (f < fRemove)
				{
					fraImgEff.drawFrameNew(6 + mframe[f], x, y, Dir, 3, g);
				}
				break;
			case 210:
			case 243:
				paint_Dong_Dat_1(g);
				break;
			case 211:
			case 244:
				paint_Dong_Dat_2(g);
				break;
			case 212:
			case 271:
			case 274:
			case 275:
			{
				for (int num42 = 0; num42 < VecSubEff.size(); num42++)
				{
					Point point24 = (Point)VecSubEff.elementAt(num42);
					fraImgEff.drawFrame(point24.f % fraImgEff.nFrame, point24.x, point24.y, 0, 3, g);
				}
				break;
			}
			case 219:
			case 292:
				if (f == 4)
				{
					fraImgSubEff.drawFrame(0, x, y, Dir, 3, g);
				}
				if (f == 24)
				{
					fraImgSubEff.drawFrame(0, x1000, y1000, Dir, 33, g);
				}
				if (mframe[f] >= 0)
				{
					fraImgEff.drawFrame(mframe[f], x, y + 5, Dir, 33, g);
				}
				break;
			case 291:
				if (f == 4)
				{
					fraImgSubEff.drawFrame(0, x1000, y1000, Dir, 33, g);
				}
				if (mframe[f] >= 0)
				{
					fraImgEff.drawFrame(mframe[f], objBeFireMain.x - am_duong * 30, objBeFireMain.y + 5, Dir, 33, g);
				}
				break;
			case 246:
			case 253:
			{
				if (f >= 10 && f <= fRemove - 4 && f % 3 != 2)
				{
					fraImgSubEff.drawFrame(f / 2 % fraImgSubEff.nFrame, x, y + 3, Dir, 3, g);
				}
				for (int num23 = 0; num23 < VecEff.size(); num23++)
				{
					Point point17 = (Point)VecEff.elementAt(num23);
					if (point17.frame == 0)
					{
						fraImgEff.drawFrame(0, point17.x, point17.y, 0, 3, g);
					}
					else if (point17.frame == 1)
					{
						if (fraImgEff.getImageFrame() != null)
						{
							g.drawRegion(fraImgEff.getImageFrame(), 0, 0, fraImgEff.frameWidth, fraImgEff.frameHeight - point17.dis, 0, point17.x, point17.y, 33);
						}
					}
					else if ((point17.frame == 2 || point17.frame == 3) && fraImgEff.getImageFrame() != null)
					{
						g.drawRegion(fraImgEff.getImageFrame(), 0, (point17.frame - 1) * fraImgEff.frameHeight, fraImgEff.frameWidth, fraImgEff.frameHeight - point17.dis, 0, point17.x, point17.y, 33);
					}
				}
				break;
			}
			case 247:
			case 254:
			{
				if (f >= 5 && f <= 7)
				{
					if (fraImgEff.getImageFrame() != null)
					{
						g.drawRegion(fraImgEff.getImageFrame(), 0, 0, fraImgEff.frameWidth / 4 * (f - 4), fraImgEff.frameHeight, Dir, x, y, 3);
					}
					if (!checkNullObject(1))
					{
						fraImgSub2Eff.drawFrame(0, x - am_duong * 10, objFireMain.y - objFireMain.hOne + 10, Dir, 3, g);
					}
				}
				if (f == 7 || f == 8 || f == 14 || f == 15)
				{
					fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x - am_duong * 14, y, Dir, 3, g);
					if (!checkNullObject(1))
					{
						fraImgSub2Eff.drawFrame(0, x - am_duong * 10, objFireMain.y - objFireMain.hOne + 10, Dir, 3, g);
					}
				}
				for (int num7 = 0; num7 < VecEff.size(); num7++)
				{
					Point_Focus point_Focus3 = (Point_Focus)VecEff.elementAt(num7);
					if (typeEffect == 254 && CRes.random(2) == 0)
					{
						fraImgSub3Eff.drawFrame(CRes.random(5), point_Focus3.x / 10 + CRes.random_Am_0(5) + am_duong * 5, point_Focus3.y / 10 - 8, point_Focus3.Dir, 3, g);
					}
					fraImgEff.drawFrame(point_Focus3.f / 2 % fraImgEff.nFrame, point_Focus3.x / 10, point_Focus3.y / 10, point_Focus3.Dir, 3, g);
					if (typeEffect == 254 && CRes.random(2) == 0)
					{
						fraImgSub3Eff.drawFrame(CRes.random(5), point_Focus3.x / 10 + CRes.random_Am_0(5) + am_duong * 20, point_Focus3.y / 10 - 8, point_Focus3.Dir, 3, g);
					}
				}
				for (int num8 = 0; num8 < VecSubEff.size(); num8++)
				{
					Point point8 = (Point)VecSubEff.elementAt(num8);
					if (point8.fRe == 5)
					{
						fraImgSub3Eff.drawFrame(point8.f % fraImgSub3Eff.nFrame, point8.x, point8.y, 0, 3, g);
					}
					else
					{
						fraImgSubEff.drawFrame(point8.f % fraImgSubEff.nFrame, point8.x, point8.y, 0, 3, g);
					}
				}
				break;
			}
			case 245:
			case 251:
			{
				if (!checkNullObject(1) && f >= 8 && f <= 19 && f - 8 < mframeSuper.Length)
				{
					fraImgEff.drawFrameNew(mframeSuper[f - 8][0], objFireMain.x + am_duong * (mframeSuper[f - 8][1] + 20), objFireMain.y - objFireMain.hOne / 2 - mframeSuper[f - 8][2] - objFireMain.dy, objFireMain.type_left_right, 3, g);
				}
				for (int n = 0; n < VecEff.size(); n++)
				{
					Point point5 = (Point)VecEff.elementAt(n);
					fraImgSubEff.drawFrame(point5.f / 2 % fraImgSubEff.nFrame, point5.x, point5.y, point5.dis, 3, g);
				}
				break;
			}
			case 249:
			case 252:
			{
				if (!checkNullObject(1) && (f == 6 || f == 8 || f == 19 || f == 21))
				{
					fraImgSub2Eff.drawFrame(0, objFireMain.x, objFireMain.y, 0, 33, g);
				}
				for (int i = 0; i < VecEff.size(); i++)
				{
					Point point = (Point)VecEff.elementAt(i);
					int num = (point.f + point.frame) % mframeSuper.Length;
					fraImgSubEff.drawFrameNew(point.f % fraImgSubEff.nFrame, point.x, point.y - point.limitY, point.dis, 3, g);
					fraImgEff.drawFrameNew(mframeSuper[num][0], point.x + point.fSmall * (mframeSuper[num][1] + 20), point.y - mframeSuper[num][2] - point.limitY, point.dis, 3, g);
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	private void paintEffTru2(mGraphics g)
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			fraImgEff.drawFrame(0, point_Focus.x, point_Focus.y, 0, 3, g);
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point point = (Point)VecSubEff.elementAt(j);
			fraImgEff.drawFrame(point.f, point.x, point.y, 0, 3, g);
		}
	}

	private void paint_Dong_Dat_2(mGraphics g)
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			if (point.frame == 0 && typeEffect == 244)
			{
				fraImgSubEff.drawFrameNew(point.f / 3 % fraImgSubEff.nFrame, point.x, point.y, Dir, 33, g);
			}
			else
			{
				fraImgEff.drawFrameNew(point.f / 3 % fraImgEff.nFrame, point.x, point.y, Dir, 33, g);
			}
		}
	}

	private void paint_Dong_Dat_1(mGraphics g)
	{
		if (f > 20 && fraImgEff.getImageFrame() != null)
		{
			int num = 1;
			int num2 = 0;
			int num3 = 0;
			if (f < 24)
			{
				num = 3;
				num3 = fraImgEff.frameWidth / num;
				num2 = fraImgEff.frameWidth / 2 - num3 / 2;
			}
			else if (f < 27)
			{
				num = 2;
				num3 = fraImgEff.frameWidth / num;
				num2 = fraImgEff.frameWidth / 2 - num3 / 2;
			}
			else
			{
				num3 = fraImgEff.frameWidth;
				num2 = 0;
			}
			g.drawRegion(fraImgEff.getImageFrame(), num2, 0, num3, fraImgEff.frameHeight, 0, MainScreen.cameraMain.xCam + x, MainScreen.cameraMain.yCam + y, 3);
			if (f < 24)
			{
				num3 = fraImgSubEff.frameWidth / num;
				num2 = fraImgSubEff.frameWidth / 2 - num3 / 2;
			}
			else if (f < 27)
			{
				num3 = fraImgSubEff.frameWidth / num;
				num2 = fraImgSubEff.frameWidth / 2 - num3 / 2;
			}
			else
			{
				num3 = fraImgSubEff.frameWidth;
				num2 = 0;
			}
			g.drawRegion(fraImgSubEff.getImageFrame(), num2, 0, num3, fraImgSubEff.frameHeight, 0, MainScreen.cameraMain.xCam + x1000, MainScreen.cameraMain.yCam + y1000, 3);
		}
	}

	private void paintEffTru(mGraphics g)
	{
		if (f <= fRemove)
		{
			fraImgEff.drawFrame(0, x, y, 0, 3, g);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			fraImgEff.drawFrame(point.f, point.x, point.y, 0, 3, g);
		}
	}

	private void paintEff_Mr0_1(mGraphics g)
	{
		int num = 14;
		if (frame == 0)
		{
			fraImgEff.drawFrameNew(4, x - am_duong * 4, y + num - 4 - 8, Dir, 33, g);
			fraImgEff.drawFrameNew(4, x + am_duong * 4, y + num - 2 - 8, Dir, 33, g);
			fraImgEff.drawFrameNew(5, x + am_duong * 8, y + num - 8, Dir, 33, g);
			fraImgEff.drawFrameNew(5, x + am_duong * 12, y + num + 2 - 8, Dir, 33, g);
		}
		else if (frame == 1)
		{
			fraImgEff.drawFrameNew(2, x - am_duong * 5, y - 6 - 24, Dir, 33, g);
			fraImgEff.drawFrameNew(2, x + am_duong * 5, y - 3 - 16, Dir, 33, g);
			fraImgEff.drawFrameNew(3, x + am_duong * 15, y + 2 - 10, Dir, 33, g);
			fraImgEff.drawFrameNew(3, x + am_duong * 25, y + 13 - 10, Dir, 33, g);
		}
		else if (frame == 2)
		{
			fraImgEff.drawFrameNew(0, x - am_duong * 5, y - 6 - 24, Dir, 33, g);
			fraImgEff.drawFrameNew(0, x + am_duong * 5, y - 3 - 6 - 10, Dir, 33, g);
			fraImgEff.drawFrameNew(0, x + am_duong * 15, y + 2 - 10, Dir, 33, g);
			fraImgEff.drawFrameNew(0, x + am_duong * 25, y + 13 - 10, Dir, 33, g);
		}
		if (f >= 10 && f < 14 && !checkNullObject(1))
		{
			fraImgSubEff.drawFrameNew(2 + (f - 10) / 2, objFireMain.x + am_duong * 20, objFireMain.y - 50, Dir, 3, g);
		}
	}

	private void paintEff_Df_2(mGraphics g)
	{
		for (int num = VecEff.size() - 1; num >= 0; num--)
		{
			Point point = (Point)VecEff.elementAt(num);
			fraImgEff.drawFrame(point.frame, point.x, point.y, Dir, 33, g);
		}
	}

	private void paintEff_Mr1_2(mGraphics g)
	{
		int trans = 0;
		if (!checkNullObject(1) && objFireMain.Dir == 2)
		{
			trans = 2;
		}
		if (f < 6)
		{
			fraImgSubEff.drawFrameNew(f / 2 % fraImgSubEff.nFrame, x - 10 * am_duong, y, trans, 3, g);
		}
		for (int num = VecEff.size() - 1; num >= 0; num--)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(num);
			fraImgEff.drawFrame(point_Focus.f / 2 % fraImgEff.nFrame, point_Focus.x, point_Focus.y, Dir, 3, g);
		}
	}

	private void paintCrocodile2(mGraphics g)
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			fraImgEff.drawFrame(2 + point.f % 3, point.x, point.y, Dir, 33, g);
		}
	}

	private void paintCrocodile1(mGraphics g)
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			fraImgSubEff.drawFrame(point.f % fraImgSubEff.nFrame, point.x / 10, point.y / 10, 0, 33, g);
		}
		if (f <= 2)
		{
			fraImgEff.drawFrame(f, x, y, 0, 33, g);
		}
		if (f >= 10 && f <= 12)
		{
			fraImgEff.drawFrame(12 - f, x, y, 0, 33, g);
		}
	}

	private void paintZoroS2_L3_SHORT(mGraphics g)
	{
		if (f > 2 && f < 5)
		{
			fraImgEff.drawFrame(f - 13, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
		}
		else if (f > 12 && f < 15)
		{
			fraImgEff.drawFrame(f - 23, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
		}
		else if (f > 18 && f < 21)
		{
			fraImgEff.drawFrame(f - 29, objFireMain.x, objFireMain.y - 10, Dir, 33, g);
		}
	}

	public override void update()
	{
		if (objFireMain != null && (objFireMain.returnAction() || objFireMain.Action == 4))
		{
			removeEff();
			return;
		}
		base.update();
		switch (typeEffect)
		{
		case 2000:
			updateEffFireExplore();
			break;
		case 1998:
		case 1999:
			updateEffThunderFall();
			break;
		case 280:
			if (x > toX)
			{
				x = toX;
				if (y < toY + 20)
				{
					y += 5;
				}
			}
			if (f == 15)
			{
				GameScreen.addEffectEnd(178, 0, toX, toY - 55, Dir, objMainEff);
			}
			if (f == 10)
			{
				GameScreen.addEffectEnd(119, 4, objFireMain.x + 20, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 0:
			updateAngleNormal(objBeFireMain, 0);
			break;
		case 1:
		case 37:
			updateLuffy1();
			break;
		case 47:
		case 48:
			updateSanji1();
			break;
		case 154:
			updateZoro1();
			break;
		case 155:
			updateZoro2();
			break;
		case 7:
			updateUssopSea1();
			break;
		case 141:
			updateUssopSea2();
			break;
		case 142:
			updateUssopSea3();
			break;
		case 57:
			updateUssop2();
			break;
		case 64:
		case 66:
			updateUssop_Skill2();
			break;
		case 58:
			updateUssopSkill1_Lv3();
			break;
		case 159:
			updateUssopSkill1_Lv3_New();
			break;
		case 126:
		case 192:
			updateUssopSkill1_Lv3_SHORT();
			break;
		case 224:
		case 301:
			updateUssopS1_L5();
			break;
		case 9:
		case 53:
		case 163:
			updateNami1();
			break;
		case 63:
		case 190:
		case 222:
		case 312:
			updateNami1_SHORT();
			break;
		case 11:
			updateNamiSea1();
			break;
		case 139:
			updateNamiSea2();
			break;
		case 140:
			updateNamiSea3();
			break;
		case 12:
		case 188:
		case 220:
		case 293:
			updateSanji2();
			break;
		case 49:
		case 50:
			updateSanjiSkill3_Lv1();
			break;
		case 266:
			updateRankyaku();
			break;
		case 276:
			updateSoi();
			break;
		case 277:
			updateSoi2();
			break;
		case 278:
		case 279:
			updateHuou();
			break;
		case 267:
			updateShigan();
			break;
		case 268:
			updateDoor();
			break;
		case 269:
			updateDoor2();
			break;
		case 14:
		case 44:
			updateSanji4();
			break;
		case 15:
		case 38:
			updateZoro3();
			break;
		case 16:
		case 51:
			updateNami4();
			break;
		case 52:
		case 189:
		case 221:
		case 311:
			updateNamiSkill1_Lv3();
			break;
		case 19:
			updateZoroSea3();
			break;
		case 42:
			updateZoroSea1();
			break;
		case 43:
			updateZoroSea2();
			break;
		case 21:
		case 33:
		case 176:
			updateLuffyS1();
			break;
		case 34:
			updateLuffy6();
			break;
		case 35:
			updateLuffy_S2_L2();
			break;
		case 41:
			updateZoroS2_L1_NEW();
			break;
		case 29:
			updateZoro8();
			break;
		case 31:
		case 55:
		case 56:
		case 191:
		case 223:
			updateNami5();
			break;
		case 313:
			updateNami6();
			break;
		case 46:
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 17:
		case 165:
		case 166:
			if (f >= fRemove)
			{
				if (!checkNullObject(1))
				{
					objFireMain.isTanHinh = false;
				}
				removeEff();
			}
			break;
		case 67:
		case 68:
		case 69:
		case 194:
			updateUssopSkill3();
			break;
		case 226:
			updateUssopS3_L5();
			break;
		case 303:
			updateUssopS3_L6();
			break;
		case 164:
		case 227:
		{
			for (int num11 = 0; num11 < VecEff.size(); num11++)
			{
				Point point13 = (Point)VecEff.elementAt(num11);
				point13.update();
				point13.frame++;
				if (point13.dis == 0)
				{
					if (point13.frame >= fraImgEff.nFrame)
					{
						point13.frame = 0;
					}
				}
				else if (point13.frame >= fraImgSubEff.nFrame)
				{
					point13.frame = 0;
				}
				if (point13.f >= point13.fRe)
				{
					if (CRes.random(2) == 1)
					{
						setAva(0, objBeFireMain);
					}
					VecEff.removeElement(point13);
					num11--;
				}
			}
			if (f <= fRemove - 5 && f % 3 == 0)
			{
				Point point14 = new Point();
				point14.x = x + am_duong * 15;
				point14.y = y;
				point14.vx = am_duong * (5 + CRes.random(2));
				if (typeEffect == 227)
				{
					point14.vx = am_duong * (5 + CRes.random(2));
				}
				point14.vy = CRes.random_Am_0(2);
				point14.fRe = 6 + CRes.random(3);
				point14.dis = ((CRes.random(3) != 0) ? 1 : 0);
				VecEff.addElement(point14);
				if (CRes.random(2) == 0)
				{
					addSound(4);
					if (!checkNullObject(2))
					{
						GameScreen.addEffectEnd(108, 1, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
					}
				}
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 30:
			if (f >= fRemove)
			{
				GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
				removeEff();
			}
			break;
		case 71:
		case 72:
		case 75:
		case 92:
		case 145:
		case 146:
		case 147:
		case 148:
			if (f >= fRemove)
			{
				if (!checkNullObject(1))
				{
					objFireMain.isPaintWeapon = true;
				}
				GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
				removeEff();
			}
			break;
		case 73:
		case 78:
			if (f == 5 || f == 0)
			{
				if (!checkNullObject(1))
				{
					objFireMain.isPaintWeapon = true;
				}
				GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 79:
			if (f == 6 || f == 0)
			{
				if (!checkNullObject(1))
				{
					objFireMain.isPaintWeapon = true;
				}
				GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 24:
		case 80:
			if (f == 7 || f == 2 || f == 12)
			{
				if (!checkNullObject(1))
				{
					objFireMain.isPaintWeapon = true;
				}
				GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
				if (typeEffect == 24 && !checkNullObject(2))
				{
					GameScreen.addEffectEnd(4, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3) - 10, Dir, objMainEff);
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 74:
			update_Mon_5();
			break;
		case 76:
			if (f >= fRemove)
			{
				GameScreen.addEffectEnd(8, 0, toX, toY, Dir, objMainEff);
				removeEff();
			}
			break;
		case 77:
			updateAlvida2();
			break;
		case 81:
		case 143:
		case 149:
			updateMon10();
			break;
		case 82:
		case 144:
			updateMon11();
			break;
		case 156:
			updateLuffyS1_NEW();
			break;
		case 83:
		case 180:
		case 212:
			updateLuffyS1_L3_SHORT();
			break;
		case 271:
			update_Luffy_S1_L6();
			break;
		case 274:
		case 275:
			updateXaPhong();
			break;
		case 160:
			updateLuffyS2_NEW();
			break;
		case 84:
		case 181:
		case 213:
		case 272:
			updateLuffyS2_NEW_SHORT();
			break;
		case 85:
		case 182:
			updateLuffyS3_New();
			break;
		case 214:
		case 273:
			updateLuffyS3_L5();
			break;
		case 157:
			updateZoroS1_New();
			break;
		case 86:
		case 183:
		case 215:
			updateZoro_S1_L3_SHORT();
			break;
		case 281:
			updateZoro_S1_L6();
			break;
		case 87:
		case 184:
		case 216:
			updateZoroS2_New();
			break;
		case 282:
			updateZoroS2_L6();
			break;
		case 161:
			updateZoroS2_New_SHORT();
			break;
		case 88:
			updateMorgan_1();
			break;
		case 89:
			updateMorgan_2();
			break;
		case 90:
		case 91:
			if (f > fRemove)
			{
				addSound(2);
				GameScreen.addEffectEnd(3, 0, toX, toY, Dir, objMainEff);
				removeEff();
			}
			break;
		case 93:
			updateMohji_1();
			break;
		case 94:
			updateMohji_2();
			break;
		case 95:
			updateBuggy_1();
			break;
		case 96:
			updateBuggy_2();
			break;
		case 97:
			updateCabaji_1();
			break;
		case 22:
		case 98:
			updateCabaji_2();
			break;
		case 248:
		case 255:
			if (!checkNullObject(1))
			{
				if (f == 6 || f == 11)
				{
					objFireMain.isTanHinh = true;
				}
				if (f == 7)
				{
					objFireMain.isTanHinh = false;
				}
				if (f == 12)
				{
					if (isAddSound)
					{
						addSound(51);
					}
					if (MainObject.getDistance(objFireMain.x, objFireMain.y, objBeFireMain.x, objBeFireMain.y) < 260)
					{
						objFireMain.x = objBeFireMain.x;
						objFireMain.y = objBeFireMain.y + 5;
						objFireMain.dy = 400;
					}
				}
				if (f == 14)
				{
					objFireMain.isTanHinh = false;
					objFireMain.dy = 400;
				}
				if (f >= 15 && f <= 19)
				{
					if (typeEffect == 255)
					{
						GameScreen.addEffectEnd(108, 5, objFireMain.x, objFireMain.y - objFireMain.dy, Dir, objMainEff);
						GameScreen.addEffectEnd(108, 5, objFireMain.x, objFireMain.y - objFireMain.dy + 40, Dir, objMainEff);
					}
					if (objFireMain.dy >= 0)
					{
						objFireMain.dy -= 80;
					}
				}
				if (f == 19)
				{
					if (isAddSound)
					{
						addSound(5);
					}
					objFireMain.dy = 0;
					setAva(1, objBeFireMain);
					GameScreen.addEffectEnd(148, 0, objFireMain.x, objFireMain.y, Dir, objMainEff);
					if (typeEffect == 255)
					{
						GameScreen.addEffectEnd(54, 12, objFireMain.x, objFireMain.y, Dir, objMainEff);
					}
					GameScreen.addEffectEnd(45, 0, objFireMain.x, objFireMain.y + 25, Dir, objMainEff);
				}
			}
			if (f > fRemove)
			{
				removeEff();
			}
			break;
		case 99:
			if (f == 2)
			{
				addSound(10);
			}
			if (f == 2 || f == 8)
			{
				GameScreen.addEffectEnd(3, 0, toX, toY, Dir, objMainEff);
			}
			if (f > fRemove)
			{
				setAva(0, objBeFireMain);
				removeEff();
			}
			break;
		case 100:
			updateNyaban_2();
			break;
		case 101:
			updateNyaban_3();
			break;
		case 102:
			updateJango_1();
			break;
		case 103:
			updateKuro_1();
			break;
		case 104:
			updateKuro_2();
			break;
		case 105:
		case 107:
			if (f >= fRemove)
			{
				addSound(14);
				addVir(3, 5, 10, isPlayer: false);
				GameScreen.addEffectEnd(35, 0, x, y, Dir, objMainEff);
				setAva(1, objBeFireMain);
				removeEff();
			}
			break;
		case 65:
			if (f >= fRemove)
			{
				addSound(5);
				addVir(3, 5, 10, isPlayer: false);
				GameScreen.addEffectEnd(35, 0, toX, toY, Dir, objMainEff);
				GameScreen.addEffectEnd(21, 0, toX, toY, Dir, objMainEff);
				GameScreen.addEffectEnd(107, 0, toX, toY, Dir, objMainEff);
				setAva(2, objBeFireMain);
				removeEff();
			}
			break;
		case 70:
			if (f == 4)
			{
				x += am_duong * 20;
				y -= 10;
				int xdich = toX - x;
				int ydich = toY - y;
				create_Speed(xdich, ydich, null);
				fRemove += 4;
			}
			if (f >= fRemove)
			{
				GameScreen.addEffectEnd(4, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
				removeEff();
			}
			break;
		case 106:
			updatePearl_2();
			break;
		case 108:
			updateGhin_2();
			break;
		case 109:
			updateDonKrieg_1();
			break;
		case 110:
			updateDonKrieg_2();
			break;
		case 111:
			updateDonKrieg_3();
			break;
		case 112:
		case 270:
			updateHachi_1();
			break;
		case 113:
		case 150:
		case 151:
		case 152:
		case 153:
			updateHachi_2();
			break;
		case 114:
			updateChu_1();
			break;
		case 115:
			updateChu_2();
			break;
		case 116:
			updateKurobi_1();
			break;
		case 117:
			updateKurobi_2();
			break;
		case 118:
			updateArlong_1();
			break;
		case 119:
			updateArlong_2();
			break;
		case 120:
			updateArlong_3();
			break;
		case 121:
			update_Zoro_s3_l1_New();
			break;
		case 122:
			update_Zoro_s3_l2_New();
			break;
		case 123:
		case 185:
		case 217:
			update_Zoro_s3_l3_New();
			break;
		case 283:
			update_Zoro_s3_l6();
			break;
		case 158:
		case 177:
			updateSanji_S1_L3_New();
			break;
		case 124:
		case 186:
		case 218:
			updateSanji_S1_L3_SHORT();
			break;
		case 162:
			updateSanji_S2_L3_New();
			break;
		case 125:
		case 187:
			updateSanji_S2_L3_New_SHORT();
			break;
		case 127:
		case 193:
		case 225:
			updateUssop_S2_L3_New();
			break;
		case 302:
			updateUssop_S2_L6();
			break;
		case 128:
			if (f == 0 || f == 8)
			{
				GameScreen.addEffectEnd(8, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 129:
		case 130:
			if (f == 0 || f == 8 || f == 14)
			{
				GameScreen.addEffectEnd(8, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 131:
		case 132:
			updateLuffyMon16_17();
			break;
		case 133:
			updateLuffySea1();
			break;
		case 134:
		case 135:
			updateLuffySea2();
			break;
		case 136:
			updateSanjiSea1();
			break;
		case 137:
		case 138:
			updateSanjiSea2();
			break;
		case 2:
			update_Ace_1();
			break;
		case 228:
		case 259:
		case 260:
		case 261:
			update_Ace_1_L2();
			break;
		case 10030:
			update_ho_den_vu_tru();
			break;
		case 3:
		case 229:
		case 262:
		case 263:
		case 264:
			update_Ace_2();
			break;
		case 402:
			update_Blackhole();
			break;
		case 4:
		case 230:
			update_Aokiji_1();
			break;
		case 5:
		case 231:
			update_Aokiji_2();
			break;
		case 6:
		case 232:
			update_Smoker_1();
			break;
		case 10:
		case 234:
			update_Smoker_2();
			break;
		case 13:
		case 258:
			update_Mon_Smoker_1();
			break;
		case 18:
			update_Mon_Smoker_2();
			break;
		case 10001:
			update_Pan1();
			break;
		case 10002:
			updatePan2();
			break;
		case 10003:
		case 10017:
		case 10020:
		case 10021:
		case 10022:
		case 10026:
			if (GameCanvas.timeNow - timeBegin >= timeEnd)
			{
				removeEff();
			}
			break;
		case 10004:
			updateGalio2();
			break;
		case 10005:
			updateNoNangLuong1();
			break;
		case 10006:
			updateNoNangLuong2();
			break;
		case 10007:
			updateNoNangLuong3();
			break;
		case 10008:
			updateNoTheoHuong_1();
			break;
		case 10009:
			updateNoTheoHuong_2();
			break;
		case 10010:
		case 10013:
			updateXerath1();
			break;
		case 10011:
		case 10024:
			updateXerath2();
			break;
		case 10012:
			updatexerath3();
			break;
		case 10015:
			updateUrgot3();
			break;
		case 10018:
			updateMonster_Chay_Thang();
			break;
		case 10019:
			if (!checkNullObject(1))
			{
				objFireMain.vx = am_duong * 15;
			}
			if (f >= fRemove)
			{
				if (!checkNullObject(1))
				{
					objFireMain.vx = 0;
				}
				removeEff();
			}
			break;
		case 10023:
			updateMonster_DanhTron();
			break;
		case 10025:
			if (f >= fRemove)
			{
				GameScreen.addEffectEnd(57, 0, toX, toY, Dir, objMainEff);
				removeEff();
			}
			break;
		case 10027:
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 20:
			update_Mon_Valentine();
			break;
		case 23:
			update_Mon_Mr5();
			break;
		case 25:
		case 235:
			update_Crocodile_1();
			break;
		case 26:
		case 236:
			update_Crocodile_2();
			break;
		case 27:
			if (f >= fRemove)
			{
				if (!checkNullObject(2))
				{
					setAva(0, objBeFireMain);
					GameScreen.addEffectEnd(36, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				}
				removeEff();
			}
			break;
		case 28:
			if (f >= fRemove)
			{
				if (!checkNullObject(2))
				{
					setAva(0, objBeFireMain);
					GameScreen.addEffectEnd_ObjTo(102, subType, toX, toY, objBeFireMain.ID, objBeFireMain.typeObject, Dir, null);
				}
				removeEff();
			}
			break;
		case 32:
			updateWapol_1();
			break;
		case 36:
			if (f >= fRemove)
			{
				setAva(1, objBeFireMain);
				removeEff();
			}
			break;
		case 39:
			updateWapol_3();
			break;
		case 40:
			update_Wapol_4();
			break;
		case 45:
			updateMr3_1();
			break;
		case 54:
			updateMr3_2();
			break;
		case 59:
		case 60:
		case 62:
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 61:
			if (f >= fRemove)
			{
				GameScreen.addEffectEnd(60, 2, toX, toY, Dir, objMainEff);
				removeEff();
			}
			break;
		case 167:
			updateMissMS_1();
			break;
		case 169:
		case 237:
			updateSet_1();
			break;
		case 170:
		case 238:
			updateSet_2();
			break;
		case 171:
		case 239:
			updateNamThach_1();
			break;
		case 172:
		case 240:
			update_Nham_thach_2();
			break;
		case 168:
			update_Mr1_1();
			break;
		case 173:
			update_Mr1_2();
			break;
		case 174:
			update_DF_1();
			break;
		case 175:
			update_DF_2();
			break;
		case 178:
			update_Mr0_1();
			break;
		case 179:
		case 241:
			update_Pell_1();
			break;
		case 195:
			update_Enel_1();
			break;
		case 196:
			update_Enel_2();
			break;
		case 197:
			update_Enel_3();
			break;
		case 198:
			update_Satori_1();
			break;
		case 199:
			update_Satori_2();
			break;
		case 200:
			update_Ohm_1();
			break;
		case 201:
			update_Ohm_2();
			break;
		case 202:
			update_Gedatsu_1();
			break;
		case 203:
			update_Gedatsu_2();
			break;
		case 204:
			update_Shura_1();
			break;
		case 205:
			update_Shura_2();
			break;
		case 206:
		case 207:
			update_Linh_Troi();
			break;
		case 208:
			update_Tru_1();
			break;
		case 250:
			update_Tru_2();
			break;
		case 209:
		case 242:
			update_Lucci_1();
			break;
		case 210:
		case 243:
			update_Dong_Dat_1();
			break;
		case 211:
		case 244:
			update_Dong_Dat_2();
			break;
		case 219:
			if (f >= fRemove || checkNullObject(3))
			{
				objFireMain.isTanHinh = false;
				removeEff();
			}
			if (f == 5 || f == 14)
			{
				if (MainObject.getDistance(objFireMain.x, objFireMain.y, objBeFireMain.x, objBeFireMain.y) < 160)
				{
					objFireMain.x = objBeFireMain.x;
					objFireMain.y = objBeFireMain.y;
				}
				objFireMain.isTanHinh = true;
				changeDir();
				am_duong = -1;
				if (Dir == 2)
				{
					am_duong = 1;
				}
				objFireMain.Dir = Dir;
				x = objBeFireMain.x + am_duong * 30;
				y = objBeFireMain.y;
			}
			if (f == 9 || f == 19)
			{
				objFireMain.isTanHinh = true;
				changeDir();
				am_duong = -1;
				if (Dir == 2)
				{
					am_duong = 1;
				}
				objFireMain.Dir = Dir;
				x = objBeFireMain.x + am_duong * 30;
				y = objBeFireMain.y;
			}
			if (f == 6 || f == 11 || f == 15 || f == 20)
			{
				sbyte subtype3 = 0;
				if (f == 6 || f == 15)
				{
					if (isAddSound)
					{
						mSound.playSound(14, mSound.volumeSound);
					}
					subtype3 = 1;
				}
				GameScreen.addEffectEnd(108, 7, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
				addVir(5, 5, 10, isPlayer: true);
				GameScreen.addEffectEnd(36, subtype3, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
				GameScreen.addEffectEnd(25, 4, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
				setAva(1, objBeFireMain);
			}
			if (f == 24)
			{
				objFireMain.isTanHinh = false;
				objFireMain.x = x1000;
				objFireMain.y = y1000;
			}
			break;
		case 292:
		{
			if (f >= fRemove || checkNullObject(3))
			{
				objFireMain.isTanHinh = false;
				removeEff();
			}
			if (f == 0 || f == 8 || f == 15 || f == 25)
			{
				if (MainObject.getDistance(objFireMain.x, objFireMain.y, objBeFireMain.x, objBeFireMain.y) < 160)
				{
					objFireMain.x = objBeFireMain.x;
					objFireMain.y = objBeFireMain.y;
				}
				objFireMain.isTanHinh = true;
				changeDir();
				am_duong = -1;
				if (Dir == 2)
				{
					am_duong = 1;
				}
				objFireMain.Dir = Dir;
				x = objBeFireMain.x + am_duong * 30;
				y = objBeFireMain.y;
			}
			if (f >= 15 && f <= 19)
			{
				y -= 12 * (f - 14);
				objFireMain.isTanHinh = true;
			}
			if (f >= 20 && f <= 24)
			{
				y += 12 * (f - 19);
				objFireMain.isTanHinh = true;
			}
			if (f == 25)
			{
				GameScreen.addEffectEnd(172, 0, objBeFireMain.x, y + 5, Dir, objMainEff);
			}
			if (f != 6 && f != 11 && f != 15 && f != 20)
			{
				break;
			}
			sbyte subtype2 = 0;
			if (f == 6 || f == 15)
			{
				if (isAddSound)
				{
					mSound.playSound(14, mSound.volumeSound);
				}
				subtype2 = 1;
			}
			GameScreen.addEffectEnd(108, 7, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			addVir(5, 5, 10, isPlayer: true);
			GameScreen.addEffectEnd(36, subtype2, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			GameScreen.addEffectEnd(25, 4, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			setAva(1, objBeFireMain);
			break;
		}
		case 291:
		{
			if (f >= fRemove || checkNullObject(3))
			{
				objFireMain.isTanHinh = false;
				removeEff();
			}
			if (f == 0 || f == 8 || f == 15 || f == 25)
			{
				objFireMain.isTanHinh = true;
				am_duong = -1;
				if (Dir == 2)
				{
					am_duong = 1;
				}
				objFireMain.Dir = Dir;
				x = objBeFireMain.x + am_duong * 30;
				y = objBeFireMain.y;
				if (f == 8 || f == 15)
				{
					GameScreen.addEffectEnd(119, 4, objBeFireMain.x + am_duong * 10, objBeFireMain.y - objBeFireMain.hOne / 2 - 5, Dir, objMainEff);
				}
			}
			if (f != 6 && f != 11 && f != 15 && f != 20)
			{
				break;
			}
			sbyte subtype = 0;
			if (f == 6 || f == 15)
			{
				if (isAddSound)
				{
					mSound.playSound(14, mSound.volumeSound);
				}
				subtype = 1;
			}
			GameScreen.addEffectEnd(108, 7, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			addVir(5, 5, 10, isPlayer: true);
			GameScreen.addEffectEnd(36, subtype, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			GameScreen.addEffectEnd(25, 4, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			setAva(1, objBeFireMain);
			break;
		}
		case 246:
		case 253:
		{
			for (int num6 = 0; num6 < VecEff.size(); num6++)
			{
				Point point9 = (Point)VecEff.elementAt(num6);
				point9.update();
				if (point9.frame == 0 && point9.f == point9.fRe)
				{
					point9.frame = 1;
					point9.vx = 0;
					point9.vy = 0;
					point9.fSmall = CRes.random(16, 24);
					GameScreen.addEffectEnd(133, 1, point9.x, point9.y, Dir, objMainEff);
					GameScreen.addEffectEnd(59, 0, point9.x, point9.y + 5, Dir, objMainEff);
					if (point9.obj != null)
					{
						setAva(1, point9.obj);
					}
				}
				if (point9.frame == 1 && point9.f == point9.fRe + point9.fSmall)
				{
					point9.frame = 2;
					point9.f = 0;
				}
				if (point9.frame == 2 && point9.f == 4)
				{
					point9.frame = 3;
					point9.f = 0;
				}
				if (point9.frame == 3 && point9.f == 2)
				{
					VecEff.removeElement(point9);
					num6--;
				}
			}
			if (f == 12)
			{
				if (isAddSound)
				{
					addSound(14);
				}
				for (int num7 = 0; num7 < vecObjsBeFire.size(); num7++)
				{
					Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vecObjsBeFire.elementAt(num7);
					if (object_Effect_Skill2 == null)
					{
						continue;
					}
					MainObject mainObject2 = MainObject.get_Object(object_Effect_Skill2.ID, object_Effect_Skill2.tem);
					if (mainObject2 == null)
					{
						continue;
					}
					Point point10 = new Point();
					point10.vy = CRes.random(30, 40);
					point10.dis = CRes.random(14, 26);
					point10.fRe = 4 + num7;
					point10.x = mainObject2.x;
					point10.y = mainObject2.y + CRes.random(5);
					if (typeEffect == 253)
					{
						for (int num8 = 0; num8 < 2; num8++)
						{
							Point point11 = new Point();
							point11.vy = CRes.random(30, 40);
							point11.dis = point10.dis + 10;
							point11.fRe = 5 + num7 + num8;
							point11.x = mainObject2.x - 25 + num8 * 50;
							point11.y = point10.y - point11.vy * point11.fRe;
							point11.frame = 0;
							VecEff.addElement(point11);
						}
					}
					point10.y += -point10.vy * point10.fRe;
					point10.frame = 0;
					point10.obj = mainObject2;
					VecEff.addElement(point10);
				}
				if (!checkNullObject(1))
				{
					int num9 = CRes.random(1, 3);
					for (int num10 = 0; num10 < num9; num10++)
					{
						Point point12 = new Point();
						point12.vy = CRes.random(30, 40);
						point12.dis = CRes.random(14, 26);
						point12.fRe = 4 + vecObjsBeFire.size() + num10;
						point12.x = objBeFireMain.x + CRes.random_Am_0(100);
						point12.y = objBeFireMain.y + CRes.random_Am_0(80);
						point12.frame = 0;
						if (GameCanvas.loadmap.getTile(point12.x, point12.y) == 0)
						{
							point12.y += -(point12.vy * point12.fRe) + CRes.random(5);
							VecEff.addElement(point12);
						}
					}
				}
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 247:
		case 254:
		{
			for (int m = 0; m < VecSubEff.size(); m++)
			{
				Point point7 = (Point)VecSubEff.elementAt(m);
				point7.f++;
				if (point7.f >= point7.fRe)
				{
					VecSubEff.removeElement(point7);
				}
			}
			for (int n = 0; n < VecEff.size(); n++)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(n);
				point_Focus.update_Vx_Vy();
				if (point_Focus.f >= point_Focus.fRe + 10)
				{
					VecEff.removeElement(point_Focus);
				}
				else
				{
					int num2 = CRes.random(1, 4);
					for (int num3 = 0; num3 < num2; num3++)
					{
						Point point8 = new Point(point_Focus.x / 10 + CRes.random_Am_0(4) - point_Focus.vx / 10, point_Focus.y / 10 + CRes.random_Am_0(4) - point_Focus.vy / 10);
						point8.fRe = 4;
						if (typeEffect == 254 && num3 == 0 && CRes.random(3) == 0)
						{
							point8.fRe = 5;
						}
						VecSubEff.addElement(point8);
					}
				}
				if (point_Focus.f == point_Focus.fRe && !checkNullObject(2))
				{
					setAva(1, objBeFireMain);
					GameScreen.addEffectEnd(8, 0, point_Focus.x / 10, point_Focus.y / 10, Dir, objMainEff);
					if (typeEffect == 254)
					{
						GameScreen.addEffectEnd(108, 1, point_Focus.x / 10, point_Focus.y / 10 + CRes.random_Am_0(10), Dir, objMainEff);
					}
				}
			}
			if (f == 10 || f == 15)
			{
				if (isAddSound)
				{
					addSound(18);
				}
				Point_Focus p = new Point_Focus(x * 10, y * 10);
				int num4 = 0;
				int num5 = 0;
				if (!checkNullObject(2))
				{
					num4 = objBeFireMain.x - x;
					num5 = objBeFireMain.y - objBeFireMain.hOne / 2 - y;
					p = create_Speed(num4 * 10, num5 * 10, p, x * 10, y * 10, objBeFireMain.x * 10, (objBeFireMain.y - objBeFireMain.hOne / 2) * 10);
					p.Dir = 0;
					if (num4 > 0)
					{
						p.Dir = 2;
					}
					VecEff.addElement(p);
				}
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 245:
		case 251:
		{
			for (int l = 0; l < VecEff.size(); l++)
			{
				Point point4 = (Point)VecEff.elementAt(l);
				point4.f++;
				if (point4.f >= point4.fRe)
				{
					VecEff.removeElementAt(l);
					l--;
				}
			}
			if (f == 6 && !checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
				Point point5 = new Point(objFireMain.x + am_duong * 30, objFireMain.y - objFireMain.hOne / 2);
				point5.fRe = 6;
				point5.dis = objFireMain.type_left_right;
				VecEff.addElement(point5);
			}
			if (f == 7 && MainObject.getDistance(objMainEff.x, objMainEff.y, objBeFireMain.x, objBeFireMain.y) < 260 && !checkNullObject(3))
			{
				objFireMain.x = objBeFireMain.x - am_duong * 30;
				objFireMain.y = objBeFireMain.y;
				Point point6 = new Point(objFireMain.x - am_duong * 30, objFireMain.y - objFireMain.hOne / 2);
				point6.fRe = 6;
				point6.dis = objFireMain.type_left_right;
				VecEff.addElement(point6);
			}
			if (f == 8 && !checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			if (typeEffect == 251 && f >= 8 && f <= 20 && !checkNullObject(3))
			{
				objFireMain.dy = (f - 8) / 3 * 12;
				objBeFireMain.dy = objFireMain.dy;
			}
			if (!checkNullObject(2) && (f == 9 || f == 12 || f == 15 || f == 18))
			{
				if (isAddSound)
				{
					mSound.playSound(7, mSound.volumeSound);
				}
				GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
				if (f == 9 || f == 15)
				{
					setAva(1, objBeFireMain);
				}
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 249:
		case 252:
		{
			for (int j = 0; j < VecEff.size(); j++)
			{
				Point point2 = (Point)VecEff.elementAt(j);
				point2.f++;
				if (typeEffect == 252)
				{
					point2.limitY = (point2.f - 1) / 3 * 12;
					point2.obj.dy = point2.limitY;
				}
				if (point2.f == 1 || point2.f == 4 || point2.f == 7 || point2.f == 10)
				{
					GameScreen.addEffectEnd(10, 0, point2.obj.x, point2.obj.y - point2.obj.dy - point2.obj.hOne / 2, Dir, objMainEff);
					if (point2.f == 1 || point2.f == 7)
					{
						setAva(1, point2.obj);
					}
				}
				if (point2.f >= point2.fRe)
				{
					VecEff.removeElementAt(j);
					j--;
				}
			}
			if (f == 7 && !checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
			}
			if (f == 8)
			{
				for (int k = 0; k < vecObjsBeFire.size(); k++)
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
					if (object_Effect_Skill == null)
					{
						continue;
					}
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point point3 = new Point();
						point3.dis = CRes.random(2) * 2;
						point3.fSmall = -1;
						if (point3.dis == 2)
						{
							point3.fSmall = 1;
						}
						int num = 30 * point3.fSmall;
						point3.x = mainObject.x - num;
						point3.fRe = mframeSuper.Length;
						point3.y = mainObject.y - mainObject.hOne / 2;
						point3.frame = CRes.random(4) * 3;
						point3.obj = mainObject;
						VecEff.addElement(point3);
					}
				}
			}
			if (!checkNullObject(2) && (f == 9 || f == 12 || f == 15 || f == 18))
			{
				if (isAddSound)
				{
					mSound.playSound(7, mSound.volumeSound);
				}
				if (CRes.random(2) == 0)
				{
					mSound.playSound(9, mSound.volumeSound);
				}
			}
			if (f == 20 && !checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case -1:
		{
			if (f < fRemove)
			{
				updateAngleXP();
				frame = setFrameAngle(gocT_Arc);
			}
			if (VecEff.size() == 0 && f > fRemove)
			{
				removeEff();
			}
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point point = (Point)VecEff.elementAt(i);
				point.f++;
				if (point.f / 2 > 3)
				{
					VecEff.removeElement(point);
					i--;
				}
			}
			if (f == fRemove)
			{
				GameScreen.addEffectEnd(108, 0, x, y + 10, Dir, objMainEff);
			}
			break;
		}
		case 10028:
			updateHoDen();
			break;
		}
	}

	public void updateAngleXP()
	{
		if (typeEffect == -1)
		{
			Point point = new Point();
			point.x = x;
			point.y = y;
			VecEff.addElement(point);
		}
		if (objBeFireMain == null || objBeFireMain.isRemove || f >= fRemove || objBeFireMain.isStop)
		{
			f = fRemove;
			return;
		}
		int num = objBeFireMain.x - x;
		int num2 = objBeFireMain.y - (objBeFireMain.hOne >> 1) - y;
		life++;
		if ((CRes.abs(num) < 16 && CRes.abs(num2) < 16) || life > fRemove)
		{
			f = fRemove;
			return;
		}
		int num3 = CRes.angle(num, num2);
		if (CRes.abs(num3 - gocT_Arc) < 90 || num * num + num2 * num2 > 4096)
		{
			if (CRes.abs(num3 - gocT_Arc) < 15)
			{
				gocT_Arc = num3;
			}
			else if ((num3 - gocT_Arc >= 0 && num3 - gocT_Arc < 180) || num3 - gocT_Arc < -180)
			{
				gocT_Arc = CRes.fixangle(gocT_Arc + 15);
			}
			else
			{
				gocT_Arc = CRes.fixangle(gocT_Arc - 15);
			}
		}
		if (va < 8192)
		{
			va += 3096;
		}
		vX1000 = va * CRes.getcos(gocT_Arc) >> 10;
		vY1000 = va * CRes.getsin(gocT_Arc) >> 10;
		num += vX1000;
		int num4 = num >> 10;
		x += num4;
		num &= 0x3FF;
		num2 += vY1000;
		int num5 = num2 >> 10;
		y += num5;
		num2 &= 0x3FF;
		if (typeEffect != -1)
		{
			Point point2 = new Point();
			point2.x = x;
			point2.y = y;
			VecEff.addElement(point2);
		}
	}

	private void update_Tru_2()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.f++;
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElementAt(i);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(25, 4, point_Focus.x, point_Focus.y, (sbyte)point_Focus.dis, objMainEff);
				VecEff.removeElementAt(j);
				j--;
			}
			else
			{
				Point point2 = new Point(point_Focus.x, point_Focus.y);
				point2.fRe = 5;
				VecSubEff.addElement(point2);
			}
		}
		if (f >= fRemove && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Dong_Dat_2()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.f++;
			if (point.frame == 1 && point.f >= 6)
			{
				point.frame = 0;
				point.f = 0;
			}
			if (point.f / 3 >= 4)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		if (f >= 12 && f % 2 == 0 && f <= 30)
		{
			int num = (f - 12) / 4;
			int num2 = 4 + num;
			int num3 = 30 + 35 * num;
			int num4 = 100 / num2;
			if (f % 4 == 0)
			{
				int num5 = 180 + num2 * num4 / 2;
				for (int j = 0; j < num2; j++)
				{
					Point point2 = new Point();
					point2.x = x + CRes.getcos(CRes.fixangle(num5)) * num3 / 1000;
					point2.y = y + CRes.getsin(CRes.fixangle(num5)) * num3 / 1000;
					if (typeEffect == 244)
					{
						point2.frame = 1;
					}
					int tile = GameCanvas.loadmap.getTile(point2.x, point2.y);
					if (tile == 0 || tile == 2)
					{
						VecSubEff.addElement(point2);
						if (j % 2 == 0)
						{
							GameScreen.addEffectEnd(110, 0, point2.x, point2.y, Dir, objMainEff);
						}
						GameScreen.addEffectEnd(63, 0, point2.x, point2.y + 5, Dir, objMainEff);
					}
					num5 -= num4;
				}
			}
			if (f % 4 == 2)
			{
				num3 += 15;
				int num6 = 360 - num2 * num4 / 2;
				for (int k = 0; k < num2; k++)
				{
					Point point3 = new Point();
					point3.x = x + CRes.getcos(CRes.fixangle(num6)) * num3 / 1000;
					point3.y = y + CRes.getsin(CRes.fixangle(num6)) * num3 / 1000;
					if (typeEffect == 244)
					{
						point3.frame = 1;
					}
					int tile2 = GameCanvas.loadmap.getTile(point3.x, point3.y);
					if (tile2 == 0 || tile2 == 2)
					{
						VecSubEff.addElement(point3);
						if (k % 2 == 0)
						{
							GameScreen.addEffectEnd(110, 0, point3.x, point3.y, Dir, objMainEff);
						}
						GameScreen.addEffectEnd(63, 0, point3.x, point3.y + 5, Dir, objMainEff);
					}
					num6 += num4;
				}
			}
			if (isAddSound && f % 4 == 0)
			{
				mSound.playSound(41, mSound.volumeSound);
			}
		}
		if (f >= fRemove && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Ace_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				if (CRes.random(2) == 0)
				{
					addSound(5);
				}
				VecEff.removeElement(point);
				GameScreen.addEffectEnd(2, 0, point.x, point.y, Dir, objMainEff);
				if (CRes.random(4) == 0)
				{
					GameScreen.addEffectEnd(110, 1, point.x, point.y, Dir, objMainEff);
					GameScreen.addEffectEnd(63, 0, point.x, point.y + 5, Dir, objMainEff);
				}
				i--;
			}
		}
		if (f >= 7 && f <= 12 && !checkNullObject(1))
		{
			objFireMain.dy = (f - 6) * 30;
		}
		if (f >= 14 && f <= 26)
		{
			if (f == 14 && !checkNullObject(3))
			{
				toY = objBeFireMain.y;
				toX = objBeFireMain.x;
				x1000 = objFireMain.x;
				y1000 = objFireMain.y;
			}
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
			}
			if (f > 15 && f < 26 && f % 5 == 0)
			{
				addSound(15);
			}
			if (f > 15 && f % 2 == 1 && f <= 25)
			{
				int num = CRes.random(2, 4);
				for (int j = 0; j < num; j++)
				{
					Point point2 = new Point();
					point2.vy = CRes.random(25, 35);
					point2.x = toX + CRes.random_Am(0, 50);
					point2.y = toY - point2.vy * 3 + CRes.random_Am(0, 20) + 5;
					point2.frame = CRes.random(fraImgEff.maxNumFrame);
					point2.fRe = 3;
					VecEff.addElement(point2);
				}
			}
			if (f % 6 == 1)
			{
				setAva(0, objBeFireMain);
				addVir(1, 6, 12, isPlayer: false);
			}
		}
		if (f == 27 && !checkNullObject(1))
		{
			objFireMain.dy = 80;
			objFireMain.isTanHinh = false;
		}
		if (f > 27 && f <= 30 && !checkNullObject(1))
		{
			objFireMain.dy = 80 - (f - 27) * 20;
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
	}

	private void update_Ace_1_L2()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.f++;
			point.y += point.vy;
			if (point.f >= fraImgSub2Eff.maxNumFrame)
			{
				VecSubEff.removeElementAt(i);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			Point point3 = new Point(point2.x, point2.y - 30);
			point3.vy = -4;
			VecSubEff.addElement(point3);
			point2.update();
			if (point2.f < point2.fRe)
			{
				continue;
			}
			if (CRes.random(2) == 0)
			{
				addSound(5);
			}
			VecEff.removeElement(point2);
			if (CRes.random(2) == 0)
			{
				sbyte subtype = (sbyte)(frameSuper + 1);
				if (typeEffect == 228)
				{
					subtype = 0;
				}
				GameScreen.addEffectEnd(141, subtype, point2.x, point2.y, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(2, 0, point2.x, point2.y, Dir, objMainEff);
			}
			if (CRes.random(2) == 0)
			{
				GameScreen.addEffectEnd(110, 1, point2.x, point2.y, Dir, objMainEff);
				GameScreen.addEffectEnd(63, 0, point2.x, point2.y + 5, Dir, objMainEff);
			}
			j--;
		}
		if (f >= 4 && f <= 26)
		{
			objFireMain.isPaintLeg = false;
		}
		if (f >= 7 && f <= 12 && !checkNullObject(1))
		{
			objFireMain.dy = (f - 6) * 40;
		}
		if (f >= 14 && f <= 26)
		{
			if (f == 14 && !checkNullObject(3))
			{
				toY = objBeFireMain.y;
				toX = objBeFireMain.x;
				x1000 = objFireMain.x;
				y1000 = objFireMain.y;
			}
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
			}
			if (f > 15 && f < 26 && f % 5 == 0)
			{
				addSound(15);
			}
			if (f > 15 && f % 2 == 1 && f <= 25)
			{
				int num = CRes.random(2, 4);
				for (int k = 0; k < num; k++)
				{
					Point point4 = new Point();
					point4.vy = CRes.random(25, 35);
					point4.x = toX + CRes.random_Am(0, 50);
					point4.y = toY - point4.vy * 3 + CRes.random_Am(0, 20) + 5;
					point4.frame = CRes.random(fraImgEff.maxNumFrame);
					point4.fRe = 3;
					VecEff.addElement(point4);
				}
			}
			if (f % 6 == 1)
			{
				setAva(0, objBeFireMain);
				addVir(1, 6, 12, isPlayer: false);
			}
		}
		if (f == 27 && !checkNullObject(1))
		{
			objFireMain.dy = 80;
			objFireMain.isTanHinh = false;
			objFireMain.isPaintLeg = true;
		}
		if (f > 27 && f <= 30 && !checkNullObject(1))
		{
			objFireMain.dy = 80 - (f - 27) * 20;
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
	}

	private void update_Ace_2()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.frame == 0)
			{
				if (point.f / 2 >= 5)
				{
					VecSubEff.removeElement(point);
					i--;
				}
			}
			else if (point.f >= point.fRe)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.update();
			if (point2.frame == 1 && f % 4 == 0)
			{
				Point point3 = new Point();
				point3.x = point2.x / 1000;
				point3.y = point2.y / 1000;
				point3.frame = 1;
				point3.fRe = CRes.random(8, 10);
				point3.f = CRes.random(3);
				point3.fRe += point3.f;
				VecSubEff.addElement(point3);
			}
			if (point2.f >= point2.fRe)
			{
				addSound(15);
				VecEff.removeElement(point2);
				j--;
			}
		}
		if (f <= 10 && CRes.random(2) == 0)
		{
			Point point4 = new Point();
			point4.x = x + CRes.random_Am_0(10);
			point4.y = y - 10 - CRes.random(20);
			point4.vx = CRes.random_Am_0(3);
			point4.vy = -CRes.random(3, 7);
			VecSubEff.addElement(point4);
		}
		if (f == 15)
		{
			addSound(15);
			int num = 12;
			if (typeEffect != 3)
			{
				num = 16;
			}
			int num2 = 0;
			for (int k = 0; k < num; k++)
			{
				num2 %= 360;
				Point point5 = new Point(x * 1000, y * 1000);
				point5.vx = CRes.getcos(num2) * vMax;
				point5.vy = CRes.getsin(num2) * (vMax / 2);
				point5.fRe = 7;
				point5.frame = 0;
				VecEff.addElement(point5);
				num2 += 360 / num;
			}
		}
		if (f == 20)
		{
			addSound(15);
			addVir(1, 6, 12, isPlayer: true);
			int num3 = 15;
			int num4 = 16;
			if (typeEffect != 3)
			{
				num4 = 20;
			}
			for (int l = 0; l < num4; l++)
			{
				num3 %= 360;
				Point point6 = new Point(x * 1000, y * 1000);
				point6.vx = CRes.getcos(num3) * vMax;
				point6.vy = CRes.getsin(num3) * (vMax / 2);
				point6.fRe = 12;
				point6.fSmall = CRes.random(fraImgEff.maxNumFrame);
				point6.frame = 1;
				VecEff.addElement(point6);
				num3 += 360 / num4;
			}
		}
		if (typeEffect != 3 && f == 23)
		{
			addSound(15);
			addVir(1, 6, 12, isPlayer: true);
			int num5 = 30;
			int num6 = 24;
			for (int m = 0; m < num6; m++)
			{
				num5 %= 360;
				Point point7 = new Point(x * 1000, y * 1000);
				point7.vx = CRes.getcos(num5) * vMax;
				point7.vy = CRes.getsin(num5) * (vMax / 2);
				point7.fRe = 16;
				point7.fSmall = CRes.random(fraImgEff.maxNumFrame);
				point7.frame = 1;
				VecEff.addElement(point7);
				num5 += 360 / num6;
			}
		}
		if (f == 26)
		{
			for (int n = 0; n < vecObjsBeFire.size(); n++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(n);
				if (object_Effect_Skill != null)
				{
					MainObject obj = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					setAva(1, obj);
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0 && VecSubEff.size() == 0)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
	}

	private void update_Blackhole()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				addSound(15);
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f == 7)
		{
			addSound(15);
			int num = 12;
			if (typeEffect != 3)
			{
				num = 16;
			}
			int num2 = 0;
			for (int j = 0; j < VecSubEff.size(); j++)
			{
				Point point2 = (Point)VecSubEff.elementAt(j);
				for (int k = 0; k < num; k++)
				{
					num2 %= 360;
					Point point3 = new Point(point2.x * 1000, point2.y * 1000);
					point3.vx = CRes.getcos(num2) * vMax;
					point3.vy = CRes.getsin(num2) * (vMax / 2);
					point3.fRe = 7;
					point3.frame = 0;
					VecEff.addElement(point3);
					num2 += 360 / num;
				}
			}
		}
		if (f == 12)
		{
			addSound(15);
			addVir(1, 6, 12, isPlayer: true);
			int num3 = 15;
			int num4 = 16;
			if (typeEffect != 3)
			{
				num4 = 20;
			}
			for (int l = 0; l < VecSubEff.size(); l++)
			{
				Point point4 = (Point)VecSubEff.elementAt(l);
				for (int m = 0; m < num4; m++)
				{
					num3 %= 360;
					Point point5 = new Point(point4.x * 1000, point4.y * 1000);
					point5.vx = CRes.getcos(num3) * vMax;
					point5.vy = CRes.getsin(num3) * (vMax / 2);
					point5.fRe = 12;
					point5.fSmall = CRes.random(fraImgEff.maxNumFrame);
					point5.frame = 1;
					VecEff.addElement(point5);
					num3 += 360 / num4;
				}
			}
		}
		if (f == 26)
		{
			for (int n = 0; n < vecObjsBeFire.size(); n++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(n);
				if (object_Effect_Skill != null)
				{
					MainObject obj = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					setAva(1, obj);
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
	}

	private void create_Ice_Arc()
	{
		fRemove = 25;
		vMax = 5;
		for (int i = 0; i < 16; i++)
		{
			Point point = new Point();
			point.x = x * 1000;
			point.y = y * 1000;
			point.vx = 2 * CRes.getcos(225 * i / 10) * vMax;
			point.vy = CRes.getsin(225 * i / 10) * vMax;
			point.f = 0;
			VecEff.addElement(point);
		}
	}

	private void update_ho_den_vu_tru()
	{
		CR += 5;
		t2++;
		if (t2 % 2 == 0)
		{
			for (int i = 0; i < radian.Length; i++)
			{
				GameScreen.addEffectEnd(166, 0, 2 * (CRes.getcos(radian[i]) * CR) / 1024 + x, CRes.getsin(radian[i]) * CR / 1024 + y, Dir, objMainEff);
				radian[i] += 15;
				if (radian[i] > 360)
				{
					radian[i] = (radian[i] = 360);
				}
			}
		}
		Ctick++;
		if (Ctick % 5 == 0)
		{
			removeEff();
		}
	}

	private void update_Aokiji_1()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			if (point2.f >= point2.fSmall)
			{
				if (typeEffect == 230)
				{
					Point point3 = new Point(point2.x, point2.y - 25);
					point3.vy = -4;
					point3.f = CRes.random(2);
					point3.fRe = 6;
					VecSubEff.addElement(point3);
				}
				point2.update();
			}
			else
			{
				point2.f++;
			}
			if (point2.frame == 0)
			{
				if (point2.f >= point2.fRe)
				{
					point2.vy = 0;
					point2.frame = 1;
					point2.fRe = CRes.random(10, 12);
					point2.f = 0;
					GameScreen.addEffectEnd(17, CRes.random(20, 30), point2.x, point2.y, Dir, objMainEff);
					if (CRes.random(2) == 0)
					{
						GameScreen.addEffectEnd(110, 2, point2.x, point2.y, Dir, objMainEff);
					}
				}
			}
			else if (point2.frame == 1 && point2.f == point2.fRe)
			{
				GameScreen.addEffectEnd(14, 0, point2.x, point2.y, Dir, objMainEff);
				VecEff.removeElement(point2);
				j--;
			}
		}
		if (f <= 25 && CRes.random(4) == 0)
		{
			Point point4 = new Point();
			point4.x = x + CRes.random_Am_0(15);
			point4.y = y - CRes.random(20);
			point4.vx = CRes.random_Am_0(3);
			point4.vy = -CRes.random(3, 7);
			point4.fRe = 10;
			VecSubEff.addElement(point4);
		}
		if (typeEffect == 230 && f >= 16 && f < 20)
		{
			Point point5 = new Point();
			point5.vy = CRes.random(30, 40);
			point5.dis = CRes.random(25, 35);
			point5.fSmall = 10;
			point5.x = objFireMain.x + CRes.random_Am_0(MotherCanvas.w / 2);
			point5.y = objFireMain.y - point5.vy * 4 - 60;
			point5.frame = 0;
			point5.fRe = 5 + point5.fSmall + CRes.random(3);
			VecEff.addElement(point5);
		}
		if (f == 20)
		{
			addSound(15);
			for (int k = 0; k < vecObjsBeFire.size(); k++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point point6 = new Point();
						point6.vy = CRes.random(30, 40);
						point6.dis = CRes.random(25, 35);
						point6.fRe = 4;
						point6.x = mainObject.x;
						point6.y = mainObject.y - point6.vy * point6.fRe + CRes.random(5);
						point6.frame = 0;
						VecEff.addElement(point6);
					}
				}
			}
			if (!checkNullObject(1))
			{
				for (int l = 0; l < 5; l++)
				{
					Point point7 = new Point();
					point7.vy = CRes.random(30, 40);
					point7.dis = CRes.random(25, 35);
					point7.fSmall = l * 3;
					point7.x = objFireMain.x + CRes.random_Am_0(MotherCanvas.w / 2);
					point7.y = objFireMain.y - point7.vy * 4 - 60;
					point7.frame = 0;
					point7.fRe = 5 + point7.fSmall + CRes.random(3);
					VecEff.addElement(point7);
				}
			}
		}
		if (f == 24)
		{
			addVir(1, 6, 12, isPlayer: true);
			for (int m = 0; m < vecObjsBeFire.size(); m++)
			{
				Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vecObjsBeFire.elementAt(m);
				if (object_Effect_Skill2 != null)
				{
					MainObject obj = MainObject.get_Object(object_Effect_Skill2.ID, object_Effect_Skill2.tem);
					setAva(1, obj);
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Aokiji_2()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f / 2 >= 5)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			Point point2 = new Point(point_Focus.x, point_Focus.y);
			point2.vx = CRes.random_Am_0(3);
			point2.vy = -CRes.random(3, 7);
			VecSubEff.addElement(point2);
			if (point_Focus.f != point_Focus.fRe)
			{
				continue;
			}
			if (!checkNullObject(2))
			{
				setAva(1, objBeFireMain);
			}
			if (typeEffect == 231)
			{
				if (!checkNullObject(3) && MainObject.getDistance(objFireMain.x, objFireMain.y, objBeFireMain.x, objBeFireMain.y) < 60)
				{
					GameScreen.addEffectEnd(142, 1, point_Focus.x - 30, point_Focus.y + 8, Dir, objMainEff);
					GameScreen.addEffectEnd(142, 1, point_Focus.x + 30, point_Focus.y + 8, Dir, objMainEff);
				}
				else
				{
					int num = 1;
					if (point_Focus.vy < 0)
					{
						num = -1;
					}
					if (point_Focus.vy == 0)
					{
						num = 0;
					}
					GameScreen.addEffectEnd(142, 0, point_Focus.x - 40 * am_duong, point_Focus.y + 8 - num * 15, Dir, objMainEff);
					GameScreen.addEffectEnd(142, 1, point_Focus.x - 20 * am_duong, point_Focus.y + 8 - num * 7, Dir, objMainEff);
				}
			}
			GameScreen.addEffectEnd(88, 0, point_Focus.x, point_Focus.y + 8, Dir, objMainEff);
			addVir(2, 6, 12, isPlayer: true);
			VecEff.removeElement(point_Focus);
			j--;
		}
		if (f <= 15 && CRes.random(4) == 0)
		{
			Point point3 = new Point();
			point3.x = x + CRes.random_Am_0(15);
			point3.y = y - CRes.random(20);
			point3.vx = CRes.random_Am_0(3);
			point3.vy = -CRes.random(3, 7);
			VecSubEff.addElement(point3);
		}
		if (f == 18)
		{
			if (!checkNullObject(2))
			{
				toX = objBeFireMain.x;
				toY = objBeFireMain.y;
			}
			Point_Focus p = new Point_Focus();
			int xdich = toX - (x + am_duong * 75);
			int ydich = toY - y;
			p = create_Speed(xdich, ydich, p, x + am_duong * 75, y, toX, toY);
			VecEff.addElement(p);
			addVir(2, 6, 12, isPlayer: true);
			GameScreen.addEffectEnd(110, 2, p.x, p.y, Dir, objMainEff);
			GameScreen.addEffectEnd(110, 2, p.x, p.y, Dir, objMainEff);
		}
		if (f >= fRemove && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Smoker_1()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			if (typeEffect == 232 && f % 2 == 0 && !GameCanvas.lowGraphic)
			{
				Point point2 = new Point();
				point2.x = point_Focus.x;
				point2.y = point_Focus.y;
				point2.fRe = 7;
				point2.frame = 1;
				VecSubEff.addElement(point2);
			}
			Point point3 = new Point();
			point3.x = point_Focus.x + CRes.random_Am_0(5);
			point3.y = point_Focus.y;
			point3.vx = 0;
			point3.vy = -CRes.random(1, 4);
			point3.fRe = CRes.random(4, 7);
			VecSubEff.addElement(point3);
			point_Focus.frame++;
			if (point_Focus.f >= point_Focus.fRe)
			{
				point_Focus.vx = 0;
				point_Focus.vy = 0;
				point_Focus.x = point_Focus.toX;
				point_Focus.y = point_Focus.toY;
			}
			else if (point_Focus.frame / 2 > fraImgSubEff.nFrame - 1)
			{
				point_Focus.frame = (fraImgSubEff.nFrame - 1) * 2;
			}
			if (point_Focus.f >= point_Focus.fRe && point_Focus.frame >= fraImgSubEff.nFrame)
			{
				if (!checkNullObject(2))
				{
					setAva(2, objBeFireMain);
				}
				addSound(15);
				GameScreen.addEffectEnd(18, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 4, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(54, 3, toX, toY, Dir, objMainEff);
				if (typeEffect == 232)
				{
					GameScreen.addEffectEnd(146, 0, toX, toY, Dir, objMainEff);
				}
				VecEff.removeElement(point_Focus);
				j--;
			}
		}
		if (f <= 10 || (f > 20 && f < 26))
		{
			if (CRes.random(4) == 0)
			{
				Point point4 = new Point();
				point4.x = x + CRes.random_Am_0(15);
				point4.y = y + CRes.random(20);
				point4.vx = CRes.random_Am_0(3);
				point4.vy = -CRes.random(3, 7);
				point4.fRe = CRes.random(6, 10);
				VecSubEff.addElement(point4);
			}
		}
		else if (f <= 20 && CRes.random(2) == 0)
		{
			Point point5 = new Point();
			point5.x = x + CRes.random_Am_0(15) - am_duong * 10;
			point5.y = y + CRes.random(20);
			point5.vx = CRes.random_Am_0(4);
			point5.vy = -CRes.random(4, 8);
			point5.fRe = CRes.random(8, 14);
			VecSubEff.addElement(point5);
		}
		if (f == 24)
		{
			addSound(32);
			if (!checkNullObject(2))
			{
				toX = objBeFireMain.x;
				toY = objBeFireMain.y - objBeFireMain.hOne / 2;
			}
			Point_Focus p = new Point_Focus();
			int xdich = toX - x;
			int ydich = toY - y;
			p = create_Speed(xdich, ydich, p, x, y, toX, toY);
			VecEff.addElement(p);
			addVir(5, 6, 12, isPlayer: true);
		}
		if (f >= fRemove && VecEff.size() == 0 && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Smoker_2()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			if (typeEffect == 234 && !GameCanvas.lowGraphic)
			{
				Point point2 = new Point();
				point2.x = point_Focus.x;
				point2.y = point_Focus.y;
				point2.fRe = 3;
				point2.frame = 1;
				VecSubEff.addElement(point2);
			}
			Point point3 = new Point();
			point3.x = point_Focus.x;
			point3.y = point_Focus.y;
			point3.vx = 0;
			point3.vy = -CRes.random(1, 3);
			point3.fRe = CRes.random(3, 6);
			VecSubEff.addElement(point3);
			if (point_Focus.f >= point_Focus.fRe)
			{
				addVir(5, 6, 12, isPlayer: true);
				LoadMap.timeVibrateScreen = CRes.random(6, 12);
				GameScreen.addEffectEnd(18, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(63, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(110, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				j--;
			}
		}
		if (f <= 5 && CRes.random(3) == 0)
		{
			Point point4 = new Point();
			point4.x = x + CRes.random_Am_0(15);
			point4.y = y + CRes.random(20);
			point4.vx = CRes.random_Am_0(3);
			point4.vy = -CRes.random(3, 7);
			point4.fRe = CRes.random(6, 10);
			VecSubEff.addElement(point4);
		}
		if (f == 8 && !checkNullObject(1))
		{
			objFireMain.isPaintLeg = false;
		}
		if (f == 10 && !checkNullObject(1))
		{
			objFireMain.isTanHinh = true;
		}
		if (f == 15 || f == 20 || ((f == 12 || f == 17) && typeEffect == 234 && !GameCanvas.lowGraphic))
		{
			addSound(15);
			int num = x - am_duong * 40 + CRes.random_Am_0(30);
			int num2 = y - 160 + CRes.random_Am_0(20);
			for (int k = 0; k < vecObjsBeFire.size(); k++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus p = new Point_Focus();
						int num3 = CRes.random_Am_0(15);
						int num4 = CRes.random_Am_0(10);
						int xdich = mainObject.x + num3 - num;
						int ydich = mainObject.y + num4 - num2;
						p = create_Speed(xdich, ydich, p, num, num2, mainObject.x + num3, mainObject.y + num4);
						VecEff.addElement(p);
					}
				}
			}
		}
		if (f == 26 && !checkNullObject(1))
		{
			objFireMain.dy = 0;
			objFireMain.isTanHinh = false;
			objFireMain.isPaintLeg = true;
		}
		if (f >= fRemove && VecEff.size() == 0 && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Mon_Smoker_1()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			Point point2 = new Point();
			point2.x = point_Focus.x;
			point2.y = point_Focus.y;
			point2.vx = 0;
			point2.vy = -CRes.random(1, 3);
			point2.fRe = CRes.random(3, 6);
			VecSubEff.addElement(point2);
			if (point_Focus.f >= point_Focus.fRe)
			{
				addSound(5);
				addVir(5, 5, 10, isPlayer: false);
				GameScreen.addEffectEnd(18, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				j--;
			}
		}
		if (f <= 7 && CRes.random(4) == 0)
		{
			Point point3 = new Point();
			point3.x = x + CRes.random_Am_0(15);
			point3.y = y + CRes.random(20);
			point3.vx = CRes.random_Am_0(3);
			point3.vy = -CRes.random(3, 7);
			point3.fRe = CRes.random(6, 10);
			VecSubEff.addElement(point3);
		}
		if (f == 9 && !checkNullObject(1))
		{
			objFireMain.isPaintLeg = false;
		}
		if (f == 11)
		{
			addSound(3);
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
			}
		}
		if (f == 15)
		{
			int num = y - 160 + CRes.random_Am_0(20);
			for (int k = 0; k < vecObjsBeFire.size(); k++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus p = new Point_Focus();
						int num2 = CRes.random_Am_0(15);
						int num3 = CRes.random_Am_0(10);
						int xdich = mainObject.x + num2 - mainObject.x;
						int ydich = mainObject.y + num3 - num;
						p = create_Speed(xdich, ydich, p, mainObject.x, num, mainObject.x + num2, mainObject.y + num3);
						VecEff.addElement(p);
					}
				}
			}
		}
		if (f == 18 && !checkNullObject(1))
		{
			objFireMain.dy = 0;
			objFireMain.isTanHinh = false;
			objFireMain.isPaintLeg = true;
		}
		if (f >= fRemove && VecEff.size() == 0 && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Mon_Smoker_2()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.f++;
			if (point.f / 2 >= fraImgSubEff.nFrame)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			if (f % 2 == 0)
			{
				Point point2 = new Point();
				point2.x = point_Focus.x;
				point2.y = point_Focus.y;
				VecSubEff.addElement(point2);
			}
			if (point_Focus.f == point_Focus.fRe)
			{
				addSound(14);
				GameScreen.addEffectEnd(18, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				j--;
			}
		}
		if (f == 8)
		{
			addSound(19);
			for (int k = 0; k < vecObjsBeFire.size(); k++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus p = new Point_Focus();
						int xdich = mainObject.x - x;
						int ydich = mainObject.y - mainObject.hOne / 2 - y;
						p = create_Speed(xdich, ydich, p, x, y, mainObject.x, mainObject.y - mainObject.hOne / 2);
						VecEff.addElement(p);
					}
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0 && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Mon_5()
	{
		if (f == 3 && !checkNullObject(1))
		{
			int num = 20;
			if (Dir == 0)
			{
				num = -20;
			}
			GameScreen.addEffectEnd(72, 2, x + num, objFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isPaintWeapon = true;
			}
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			removeEff();
		}
	}

	private void update_Mon_Valentine()
	{
		if (f == 16)
		{
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd(63, 0, mainObject.x, mainObject.y, Dir, objMainEff);
						GameScreen.addEffectEnd(59, 0, mainObject.x, mainObject.y, Dir, objMainEff);
						setAva(1, mainObject);
					}
				}
			}
			LoadMap.timeVibrateScreen = 10;
			if (!checkNullObject(1))
			{
				objFireMain.y += 4;
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void update_Mon_Mr5()
	{
		if (f < fRemove)
		{
			return;
		}
		removeEff();
		for (int i = 0; i < vecObjsBeFire.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
			if (object_Effect_Skill != null)
			{
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					GameScreen.addEffectEnd(4, 0, mainObject.x + CRes.random_Am_0(15), mainObject.y - CRes.random(0, mainObject.hOne / 4 * 3) - 10, Dir, objMainEff);
					setAva(1, mainObject);
				}
			}
		}
	}

	private void update_Crocodile_1()
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElementAt(i);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f % 2 == 0)
			{
				Point point2 = new Point(point_Focus.x, point_Focus.y);
				point2.fRe = 4;
				VecSubEff.addElement(point2);
			}
			if (point_Focus.f >= point_Focus.fRe)
			{
				VecEff.removeElementAt(j);
				j--;
			}
		}
		if (f == 9 && !checkNullObject(2))
		{
			if (typeEffect == 235 && !GameCanvas.lowGraphic)
			{
				int num = objBeFireMain.x - am_duong * 48 - objFireMain.x;
				int num2 = objBeFireMain.y - objFireMain.y;
				Point_Focus point_Focus2 = new Point_Focus(objFireMain.x * 10, objFireMain.y * 10);
				create_Speed(num * 10, num2 * 10, point_Focus2, objFireMain.x * 10, objFireMain.y * 10, (objBeFireMain.x - am_duong * 48) * 10, objBeFireMain.y);
				VecEff.addElement(point_Focus2);
			}
			objFireMain.x = objBeFireMain.x - am_duong * 48;
			objFireMain.y = objBeFireMain.y;
		}
		if (f == 12 && !checkNullObject(1))
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 15)
		{
			if (!checkNullObject(2))
			{
				if (isAddSound)
				{
					mSound.playSound(5, mSound.volumeSound);
				}
				addVir(10, 5, 10, isPlayer: true);
				if (vecObjsBeFire.size() > 1)
				{
					for (int k = 0; k < vecObjsBeFire.size(); k++)
					{
						Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
						if (object_Effect_Skill != null)
						{
							MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
							if (mainObject != null)
							{
								GameScreen.addEffectEnd(63, 0, mainObject.x + 10, mainObject.y, Dir, mainObject);
								GameScreen.addEffectEnd(98, 0, mainObject.x, mainObject.y + 5, Dir, mainObject);
								GameScreen.addEffectEnd(110, 0, mainObject.x, mainObject.y + 5, Dir, mainObject);
								GameScreen.addEffectEnd(108, 5, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, mainObject);
								setAva(1, mainObject);
							}
						}
					}
				}
				else
				{
					GameScreen.addEffectEnd(63, 0, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
					GameScreen.addEffectEnd(63, 0, objBeFireMain.x - 10, objBeFireMain.y, Dir, objMainEff);
					GameScreen.addEffectEnd(63, 0, objBeFireMain.x + 10, objBeFireMain.y, Dir, objMainEff);
					GameScreen.addEffectEnd(98, 0, objBeFireMain.x, objBeFireMain.y + 5, Dir, objMainEff);
					GameScreen.addEffectEnd(59, 0, objBeFireMain.x, objBeFireMain.y + 5, Dir, objMainEff);
					GameScreen.addEffectEnd(110, 0, objBeFireMain.x, objBeFireMain.y + 5, Dir, objMainEff);
					GameScreen.addEffectEnd(108, 5, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				}
				if (typeEffect == 235 && !GameCanvas.lowGraphic)
				{
					GameScreen.addEffectEnd(54, 10, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
				}
			}
			setAva(2, objBeFireMain);
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
	}

	private void update_Crocodile_2()
	{
		if (f % 2 == 0 && f <= fRemove - 3)
		{
			Point point = new Point(x + CRes.random_Am_0(10), y + 10 + CRes.random_Am_0(10));
			point.vx = CRes.random_Am_0(3);
			point.vy = -CRes.random(3, 5);
			point.fRe = CRes.random(10, 14);
			VecEff.addElement(point);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point2 = (Point)VecEff.elementAt(i);
			point2.update();
			if (point2.f >= point2.fRe)
			{
				VecEff.removeElement(point2);
				i--;
			}
		}
		if (f == 13 && !checkNullObject(1))
		{
			sbyte subtype = 1;
			if (typeEffect == 236 && !GameCanvas.lowGraphic)
			{
				subtype = 11;
			}
			GameScreen.addEffectEnd(54, subtype, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
		}
		if (f == 15)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd(63, 0, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
						GameScreen.addEffectEnd(99, 0, mainObject.x, mainObject.y, Dir, objMainEff);
						GameScreen.addEffectEnd(59, 0, mainObject.x, mainObject.y, Dir, objMainEff);
						setAva(1, mainObject);
					}
				}
			}
		}
		if (isAddSound && (f == 14 || f == 17 || f == 20))
		{
			mSound.playSound(5, mSound.volumeSound);
		}
	}

	private void update_Wapol_4()
	{
		if (f == 5 && !checkNullObject(2))
		{
			GameScreen.addEffectEnd(57, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			setAva(1, objBeFireMain);
		}
		if (f == 8)
		{
			vx = 0;
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void update_Nham_thach_2()
	{
		if (f > 10 && f % 3 == 0)
		{
			sbyte subtype = 0;
			if (typeEffect == 240)
			{
				subtype = 1;
			}
			if (indexObjBefire < vecObjsBeFire.size())
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
				indexObjBefire++;
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd(113, subtype, mainObject.x, mainObject.y, Dir, objMainEff);
						setAva(2, mainObject);
					}
				}
			}
			else if (CRes.random(2) == 0)
			{
				GameScreen.addEffectEnd(113, subtype, objBeFireMain.x + CRes.random_Am_0(160), objBeFireMain.y + CRes.random_Am_0(80), Dir, objMainEff);
			}
			if (f % 6 == 0)
			{
				addSound(15);
			}
			addVir(3, 5, 10, isPlayer: false);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void update_Mr1_1()
	{
		if (f >= fRemove)
		{
			removeEff();
		}
		else
		{
			if (f % 4 != 0)
			{
				return;
			}
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill == null)
				{
					continue;
				}
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					setAva(1, mainObject);
					if (!checkNullObject(2))
					{
						GameScreen.addEffectEnd(1, 0, mainObject.x + CRes.random_Am_0(10), mainObject.y - mainObject.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
					}
				}
			}
		}
	}

	private void update_Mr1_2()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f == point_Focus.fRe)
			{
				GameScreen.addEffectEnd(1, 0, point_Focus.x + CRes.random_Am_0(10), point_Focus.y + CRes.random_Am_0(10), Dir, objMainEff);
				setAva(1, point_Focus.objMain);
			}
			if (point_Focus.f >= point_Focus.fRe + 6)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 2)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill == null)
				{
					continue;
				}
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					Point_Focus point_Focus2 = new Point_Focus();
					point_Focus2.x = x;
					point_Focus2.y = y;
					int xdich = mainObject.x - point_Focus2.x;
					int ydich = mainObject.y - mainObject.hOne / 2 - point_Focus2.y;
					point_Focus2 = create_Speed(xdich, ydich, point_Focus2);
					point_Focus2.objMain = mainObject;
					point_Focus2.dis = 0;
					if (x < mainObject.x)
					{
						point_Focus2.dis = 2;
					}
					VecEff.addElement(point_Focus2);
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_DF_1()
	{
		if (!checkNullObject(1))
		{
			if (f >= 4 && f <= 10)
			{
				objFireMain.vx = vMax * am_duong;
			}
			else
			{
				objFireMain.vx = 0;
			}
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			removeEff();
		}
	}

	private void update_DF_2()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f < 4)
			{
				point.frame = point.f / 2;
			}
			else if (point.f >= 4 && point.f <= point.fRe - 2)
			{
				point.frame = 2;
			}
			else
			{
				point.frame = point.fRe - point.f;
			}
			if (point.f >= point.fRe)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f == 2)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point point2 = new Point();
						point2.x = mainObject.x;
						point2.y = mainObject.y + 4;
						point2.fRe = 30 + CRes.random(12);
						GameScreen.addEffectEnd(10, 0, mainObject.x, mainObject.y - mainObject.dy - mainObject.hOne / 2, Dir, objMainEff);
						VecEff.addElement(point2);
					}
				}
			}
			if (objBeFireMain != null)
			{
				for (int k = 0; k < 4; k++)
				{
					Point point3 = new Point();
					point3.x = objBeFireMain.x + CRes.random_Am_0(160);
					point3.y = objBeFireMain.y + 4 + CRes.random_Am_0(80);
					point3.fRe = 30 + CRes.random(12);
					VecEff.addElement(point3);
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Mr0_1()
	{
		if (f == 10 && !checkNullObject(2))
		{
			vx = am_duong * vMax;
		}
		if (f == 15)
		{
			GameScreen.addEffectEnd_ToX_ToY(62, 0, x, y - 30, x + vx * 20, y - 30, Dir, objMainEff);
			GameScreen.addEffectEnd_ToX_ToY(62, 0, x + 10 * am_duong, y - 20, x + vx * 20, y - 20, Dir, objMainEff);
			GameScreen.addEffectEnd_ToX_ToY(62, 0, x + 20 * am_duong, y - 10, x + vx * 20, y - 10, Dir, objMainEff);
			GameScreen.addEffectEnd_ToX_ToY(62, 0, x + 30 * am_duong, y, x + vx * 20, y, Dir, objMainEff);
		}
		if (f < 10)
		{
			frame = -1;
		}
		else if (f < 14)
		{
			frame = 0;
		}
		else if (f < 30)
		{
			frame = 1;
		}
		else if (f < 35)
		{
			frame = 2;
		}
		if (f < fRemove)
		{
			return;
		}
		removeEff();
		for (int i = 0; i < vecObjsBeFire.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
			if (object_Effect_Skill != null)
			{
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					setAva(2, mainObject);
				}
			}
		}
	}

	private void update_Pell_1()
	{
		if (f > 1 && f < 26)
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (frame == 1)
			{
				point.frame = CRes.random(2);
			}
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.update();
			if (typeEffect == 241)
			{
				Point point3 = new Point(point2.x, point2.y);
				if (frame == 1)
				{
					point3.x = point2.x + point2.vx + CRes.random_Am_0(5);
					point3.y = point2.y + point2.vy + 5 + CRes.random_Am_0(15);
					point3.fRe = 10;
					point3.vx = CRes.random_Am_0(2);
					point3.vy = -CRes.random_Am(2, 5);
					Point point4 = new Point(point2.x + point2.vx + CRes.random_Am_0(5), point2.y + point2.vy + 5 + CRes.random_Am_0(15));
					point4.fRe = 10;
					point4.vx = CRes.random_Am_0(2);
					point4.vy = -CRes.random_Am(2, 5);
					VecSubEff.addElement(point4);
				}
				else
				{
					point3.fRe = 3;
					point3.frame = CRes.random(3);
				}
				VecSubEff.addElement(point3);
			}
			if (point2.f > 10)
			{
				point2.vy -= 2;
			}
			else
			{
				point2.vy--;
			}
			if (point2.f == 10 && !checkNullObject(2))
			{
				if (frame == 1)
				{
					GameScreen.addEffectEnd(118, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
					GameScreen.addEffectEnd(54, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd(1, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				}
				setAva(2, objBeFireMain);
			}
			if (point2.f >= point2.fRe)
			{
				VecEff.removeElement(point2);
				j--;
			}
		}
		if (f == 4)
		{
			if (isAddSound)
			{
				if (frame == 0)
				{
					mSound.playSound(13, mSound.volumeSound);
				}
				else
				{
					mSound.playSound(23, mSound.volumeSound);
				}
			}
			Point point5 = new Point();
			int num = toX;
			int num2 = toY;
			if (!checkNullObject(2))
			{
				num = objBeFireMain.x;
				num2 = objBeFireMain.y - objBeFireMain.hOne / 2;
			}
			point5.x = num - am_duong * 240;
			point5.vx = 24 * am_duong;
			point5.y = num2 - 55;
			point5.vy = 9;
			point5.dis = Dir;
			point5.fRe = 20;
			VecEff.addElement(point5);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Enel_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f > 10 && f < fRemove && f % 2 == 0)
		{
			if (indexObjBefire < vecObjsBeFire.size())
			{
				for (int j = 0; j < vecObjsBeFire.size(); j++)
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							Point point2 = new Point();
							point2.x = mainObject.x * 10;
							point2.y = (mainObject.y + 4) * 10;
							point2.vx = CRes.random_Am_0(30);
							point2.vy = CRes.random_Am_0(30);
							point2.fRe = 15 + CRes.random(6);
							GameScreen.addEffectEnd(10, 0, mainObject.x, mainObject.y - mainObject.dy - mainObject.hOne / 2, Dir, objMainEff);
							setAva(2, mainObject);
							VecEff.addElement(point2);
						}
					}
					indexObjBefire++;
				}
			}
			else
			{
				Point point3 = new Point();
				point3.x = (objBeFireMain.x + CRes.random_Am_0(100)) * 10;
				point3.y = (objBeFireMain.y + CRes.random_Am_0(80)) * 10;
				point3.vx = CRes.random_Am_0(30);
				point3.vy = CRes.random_Am_0(30);
				point3.fRe = 10 + CRes.random(6);
				VecEff.addElement(point3);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Enel_2()
	{
		if (f >= fRemove)
		{
			removeEff();
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= 3)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point point2 = (Point)VecSubEff.elementAt(j);
			point2.f++;
			if (point2.f >= point2.fRe)
			{
				VecSubEff.removeElement(point2);
				j--;
			}
		}
		if (f == 0 || f == 9)
		{
			int num = 0;
			for (int k = 0; k < 8; k++)
			{
				Point o = new Point(x + CRes.getcos(num) * 30 / 1000, y + CRes.getsin(num) * 25 / 1000);
				VecEff.addElement(o);
				num += 45;
			}
		}
		if (f == 3 || f == 12)
		{
			int num2 = 0;
			for (int l = 0; l < 12; l++)
			{
				Point o2 = new Point(x + CRes.getcos(num2) * 40 / 1000, y + CRes.getsin(num2) * 30 / 1000);
				VecEff.addElement(o2);
				num2 += 30;
			}
		}
		int num3 = 0;
		if (f == 15)
		{
			int num4 = 0;
			for (int m = 0; m < 16; m++)
			{
				num3++;
				Point o3 = new Point(x + CRes.getcos(num4) * 55 / 1000, y + CRes.getsin(num4) * 35 / 1000);
				VecEff.addElement(o3);
				num4 += 22;
			}
			Point point3 = new Point(x, y);
			point3.frame = 0;
			point3.fRe = 4;
			VecSubEff.addElement(point3);
		}
		if (f == 18)
		{
			Point point4 = new Point(x, y);
			point4.frame = 1;
			point4.fRe = 4;
			VecSubEff.addElement(point4);
		}
		if (f != 22)
		{
			return;
		}
		for (int n = 0; n < vecObjsBeFire.size(); n++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(n);
			if (object_Effect_Skill != null)
			{
				MainObject obj = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				setAva(2, obj);
			}
		}
		GameScreen.addEffectEnd(121, 0, x, y, Dir, objMainEff);
	}

	private void update_Enel_3()
	{
		if (f == 4)
		{
			vx = am_duong * 8;
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						setAva(2, mainObject);
						GameScreen.addEffectEnd(42, 0, mainObject.x, mainObject.y, Dir, mainObject);
					}
				}
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void update_Satori_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			point_Focus.frame = 2 + point_Focus.f % 2;
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(122, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f == 8)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus point_Focus2 = new Point_Focus();
						point_Focus2.x = x;
						point_Focus2.y = y;
						int xdich = mainObject.x - point_Focus2.x;
						int ydich = mainObject.y - mainObject.hOne / 2 - point_Focus2.y;
						point_Focus2 = create_Speed(xdich, ydich, point_Focus2);
						point_Focus2.objMain = mainObject;
						point_Focus2.frame = 1;
						if (point_Focus2.fRe < 3)
						{
							point_Focus2.fRe = 3;
						}
						VecEff.addElement(point_Focus2);
					}
				}
				indexObjBefire++;
			}
			for (int k = indexObjBefire; k < 5; k++)
			{
				Point_Focus point_Focus3 = new Point_Focus();
				point_Focus3.x = x;
				point_Focus3.y = y;
				int xdich2 = 110 * am_duong + CRes.random_Am_0(50);
				int ydich2 = CRes.random_Am_0(40);
				point_Focus3 = create_Speed(xdich2, ydich2, point_Focus3);
				point_Focus3.objMain = null;
				point_Focus3.frame = 1;
				if (point_Focus3.fRe < 3)
				{
					point_Focus3.fRe = 3;
				}
				VecEff.addElement(point_Focus3);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Satori_2()
	{
		if (f < 4 || (f >= 9 && f < 13))
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 2 && !checkNullObject(3))
		{
			x1000 = objFireMain.x;
			y1000 = objFireMain.y;
			objFireMain.x = objBeFireMain.x - am_duong * 30;
			objFireMain.y = objBeFireMain.y;
			x = objFireMain.x;
			y = objFireMain.y;
		}
		if (f == 11 && !checkNullObject(3))
		{
			objFireMain.x = x1000;
			objFireMain.y = y1000;
			x = objFireMain.x;
			y = objFireMain.y;
		}
		if (f == 7)
		{
			GameScreen.addEffectEnd(123, 1, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - objBeFireMain.hOne / 2 - 10 + CRes.random_Am_0(15), Dir, objMainEff);
			setAva(2, objBeFireMain);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void update_Ohm_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f == point_Focus.fRe && point_Focus.objMain != null)
			{
				GameScreen.addEffectEnd(123, 3, point_Focus.x, point_Focus.y, Dir, objMainEff);
				setAva(2, point_Focus.objMain);
			}
			if (point_Focus.f >= point_Focus.fRe + 3)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f >= 10 && f % 2 == 0 && f < 20)
		{
			if (indexObjBefire < vecObjsBeFire.size())
			{
				for (int j = 0; j < vecObjsBeFire.size(); j++)
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							Point_Focus point_Focus2 = new Point_Focus();
							point_Focus2.x = x;
							point_Focus2.y = y;
							int xdich = mainObject.x - point_Focus2.x;
							int ydich = mainObject.y - mainObject.hOne / 2 - point_Focus2.y;
							point_Focus2 = create_Speed(xdich, ydich, point_Focus2);
							point_Focus2.objMain = mainObject;
							point_Focus2.frame = CRes.random(4);
							VecEff.addElement(point_Focus2);
						}
						indexObjBefire++;
					}
				}
			}
			else
			{
				Point_Focus point_Focus3 = new Point_Focus();
				point_Focus3.x = x;
				point_Focus3.y = y;
				int xdich2 = 150 * am_duong + CRes.random_Am_0(50);
				int ydich2 = CRes.random_Am_0(40);
				point_Focus3 = create_Speed(xdich2, ydich2, point_Focus3);
				point_Focus3.objMain = null;
				point_Focus3.frame = CRes.random(4);
				point_Focus3.fRe += 5;
				VecEff.addElement(point_Focus3);
			}
		}
		if (f == 20)
		{
			objFireMain.isPaintWeapon = true;
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Ohm_2()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f % 2 == 1)
			{
				GameScreen.addEffectEnd(66, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
			}
			if (point_Focus.f == point_Focus.fRe)
			{
				int tile = GameCanvas.loadmap.getTile(point_Focus.x, point_Focus.y);
				if (tile == 0 || tile == 2)
				{
					GameScreen.addEffectEnd(124, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
					GameScreen.addEffectEnd(125, 0, point_Focus.x, point_Focus.y + 8, Dir, objMainEff);
				}
				if (point_Focus.objMain != null)
				{
					setAva(2, point_Focus.objMain);
				}
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f == 10)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus point_Focus2 = new Point_Focus();
						point_Focus2.x = x;
						point_Focus2.y = y;
						int xdich = mainObject.x - point_Focus2.x;
						int ydich = mainObject.y - mainObject.hOne / 2 - point_Focus2.y;
						point_Focus2 = create_Speed(xdich, ydich, point_Focus2);
						point_Focus2.objMain = mainObject;
						point_Focus2.frame = 1;
						if (point_Focus2.fRe < 3)
						{
							point_Focus2.fRe = 3;
						}
						VecEff.addElement(point_Focus2);
					}
				}
				indexObjBefire++;
			}
			for (int k = indexObjBefire; k < 5; k++)
			{
				Point_Focus point_Focus3 = new Point_Focus();
				point_Focus3.x = x;
				point_Focus3.y = y;
				int xdich2 = CRes.random_Am(60, 140);
				int ydich2 = CRes.random_Am_0(60);
				point_Focus3 = create_Speed(xdich2, ydich2, point_Focus3);
				point_Focus3.objMain = null;
				point_Focus3.frame = 1;
				if (point_Focus3.fRe < 3)
				{
					point_Focus3.fRe = 3;
				}
				VecEff.addElement(point_Focus3);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Gedatsu_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f > point_Focus.fRe && point_Focus.objMain != null)
			{
				point_Focus.x = point_Focus.objMain.x;
				point_Focus.y = point_Focus.objMain.y - point_Focus.objMain.hOne / 2;
			}
			if (point_Focus.f > point_Focus.fRe + 5)
			{
				GameScreen.addEffectEnd(123, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				setAva(2, point_Focus.objMain);
				VecEff.removeElementAt(i);
				i--;
			}
			if (point_Focus.f == point_Focus.fRe)
			{
				if (point_Focus.objMain == null)
				{
					GameScreen.addEffectEnd(123, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
					VecEff.removeElementAt(i);
					i--;
				}
				else
				{
					point_Focus.vx = 0;
					point_Focus.vy = 0;
					point_Focus.x = point_Focus.objMain.x;
					point_Focus.y = point_Focus.objMain.y - point_Focus.objMain.hOne / 2;
				}
			}
		}
		if (f == 11 && !checkNullObject(1))
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus point_Focus2 = new Point_Focus();
						point_Focus2.x = objFireMain.x + am_duong * 30;
						point_Focus2.y = objFireMain.y - objFireMain.hOne / 2 - 10;
						int xdich = mainObject.x - point_Focus2.x;
						int ydich = mainObject.y - mainObject.hOne / 2 - point_Focus2.y;
						point_Focus2 = create_Speed(xdich, ydich, point_Focus2);
						point_Focus2.objMain = mainObject;
						point_Focus2.frame = 1;
						VecEff.addElement(point_Focus2);
					}
				}
				indexObjBefire++;
			}
			for (int k = indexObjBefire; k < 5; k++)
			{
				Point_Focus point_Focus3 = new Point_Focus();
				point_Focus3.x = objFireMain.x + am_duong * 30;
				point_Focus3.y = objFireMain.y - objFireMain.hOne / 2 - 10;
				int xdich2 = CRes.random_Am(60, 140);
				int ydich2 = CRes.random_Am_0(60);
				point_Focus3 = create_Speed(xdich2, ydich2, point_Focus3);
				point_Focus3.objMain = null;
				point_Focus3.frame = 0;
				if (point_Focus3.fRe < 3)
				{
					point_Focus3.fRe = 3;
				}
				VecEff.addElement(point_Focus3);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Gedatsu_2()
	{
		if (f < 4)
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 2 && !checkNullObject(3))
		{
			x1000 = objFireMain.x;
			y1000 = objFireMain.y;
			if (!checkNullObject(2))
			{
				objFireMain.x = objBeFireMain.x - am_duong * 30;
				objFireMain.y = objBeFireMain.y;
				x = objFireMain.x;
				y = objFireMain.y;
			}
			GameScreen.addEffectEnd(30, 0, x, y - objFireMain.hOne / 2 - 10, 140, Dir, objMainEff);
		}
		if (f == 14)
		{
			GameScreen.addEffectEnd(123, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - objBeFireMain.hOne / 2 - 15 + CRes.random_Am_0(15), Dir, objMainEff);
			setAva(2, objBeFireMain);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void update_Shura_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f > 10)
			{
				point.vy -= 2;
			}
			else
			{
				point.vy--;
			}
			if (point.f == 10 && !checkNullObject(2))
			{
				GameScreen.addEffectEnd(123, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				setAva(2, objBeFireMain);
			}
			if (point.f >= point.fRe)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f == 10)
		{
			Point point2 = new Point();
			int num = toX;
			int num2 = toY;
			if (!checkNullObject(2))
			{
				num = objBeFireMain.x;
				num2 = objBeFireMain.y - objBeFireMain.hOne / 2;
			}
			point2.x = num - am_duong * 240;
			point2.vx = 24 * am_duong;
			point2.y = num2 - 55;
			point2.vy = 9;
			point2.dis = Dir;
			point2.fRe = 20;
			VecEff.addElement(point2);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Shura_2()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= point.fRe)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f == 10)
		{
			x += am_duong * 20;
			if (Dir == 0)
			{
				x -= 30;
			}
			vMax = 5;
			vx = am_duong * vMax;
		}
		if (f > 10)
		{
			int num = 360 - f % 12 * 30;
			int num2 = 26 + f / 4 * 3;
			x1000 = CRes.getcos(CRes.fixangle(num)) * (num2 * 2 / 3);
			y1000 = CRes.getsin(CRes.fixangle(num)) * num2;
			if (f < fRemove)
			{
				Point point2 = new Point(x + x1000 / 1000, y + y1000 / 1000);
				point2.fRe = 12;
				VecEff.addElement(point2);
			}
		}
		if (f == 20)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						setAva(1, mainObject);
					}
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Linh_Troi()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f == fPlayFrameSuper)
		{
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(12), toY + CRes.random_Am_0(12), Dir, objMainEff);
		}
		else if (f < fPlayFrameSuper)
		{
			Point point2 = new Point();
			point2.x = x;
			point2.y = y;
			point2.fRe = 6;
			VecEff.addElement(point2);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Tru_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= point.fRe)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f < fRemove)
		{
			Point point2 = new Point(x, y);
			point2.fRe = 5;
			VecEff.addElement(point2);
		}
		if (f == fRemove)
		{
			GameScreen.addEffectEnd(25, 4, toX, toY, Dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Lucci_1()
	{
		if (f == 2 || f == 4 || f == 6)
		{
			x = x1000 - am_duong * 24;
		}
		if (f >= 7 && vx <= 20)
		{
			vx += am_duong * 2;
		}
		if (f == 6 && isAddSound)
		{
			mSound.playSound(39, mSound.volumeSound);
			if (frame == 1)
			{
				mSound.playSound(17, mSound.volumeSound);
			}
		}
		if (f == 8)
		{
			setAva(2, objBeFireMain);
			GameScreen.addEffectEnd(132, (sbyte)frame, x + am_duong * 10, objFireMain.y - objFireMain.hOne / 2 - 5, 0, Dir, objMainEff);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (frame == 0 && point.f == 2)
			{
				point.frame = 0;
			}
			if (point.f >= point.fRe)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f < fRemove)
		{
			if (frame == 1)
			{
				for (int j = 0; j <= mframe[f]; j++)
				{
					Point point2 = new Point();
					point2.x = x;
					point2.y = y - mframe[f] * 10 + j * 20;
					if (mframe[f] < 2)
					{
						point2.fRe = 2;
					}
					else if (mframe[f] == 2)
					{
						if (j == 1)
						{
							point2.fRe = 4;
						}
						else
						{
							point2.fRe = 2;
						}
					}
					else if (mframe[f] == 3)
					{
						if (j == 1 || j == 2)
						{
							point2.fRe = 4;
						}
						else
						{
							point2.fRe = 2;
						}
					}
					else
					{
						switch (j)
						{
						case 2:
							point2.fRe = 6;
							break;
						case 1:
						case 3:
							point2.fRe = 4;
							break;
						default:
							point2.fRe = 2;
							break;
						}
					}
					VecEff.addElement(point2);
				}
			}
			else if (mframe[f] == 2)
			{
				Point point3 = new Point();
				point3.x = x;
				point3.y = y;
				point3.frame = 1;
				point3.fRe = 4;
				VecEff.addElement(point3);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void update_Dong_Dat_1()
	{
		if (f >= 2 && f <= 22)
		{
			addVir(2, 5, 12, isPlayer: true);
		}
		if (f == 15)
		{
			if (isAddSound)
			{
				mSound.playSound(40, mSound.volumeSound);
				mSound.playSound(41, mSound.volumeSound);
			}
			int num = objFireMain.x - GameScreen.player.x;
			int num2 = (objFireMain.y - GameScreen.player.y) / 2;
			x = MotherCanvas.hw + 30 + CRes.random_Am_0(10) + num;
			y = MotherCanvas.hh + CRes.random_Am_0(10) + num2;
			x1000 = x - 90 + CRes.random_Am_0(10);
			y1000 = y + CRes.random_Am_0(10);
		}
		if (f == 22)
		{
			GameScreen.addEffectEnd(133, 0, objFireMain.x, objFireMain.y, Dir, objMainEff);
			if (typeEffect == 243)
			{
				GameScreen.addEffectEnd(113, 2, objFireMain.x, objFireMain.y, Dir, objMainEff);
			}
			objFireMain.y += 3;
		}
		int subtype = 0;
		if (f >= 22 && f % 2 == 0 && f <= 32)
		{
			if (indexObjBefire < vecObjsBeFire.size())
			{
				for (int i = 0; i < vecObjsBeFire.size(); i++)
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
					if (typeEffect == 243 && CRes.random(2) == 0)
					{
						subtype = 1;
					}
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							setAva(2, mainObject);
							GameScreen.addEffectEnd(134, subtype, mainObject.x, mainObject.y, Dir, objMainEff);
						}
					}
					indexObjBefire++;
				}
			}
			else
			{
				if (typeEffect == 243 && CRes.random(2) == 0)
				{
					subtype = 1;
				}
				int num3 = objFireMain.x + CRes.random_Am(110, 140);
				int num4 = objFireMain.y + CRes.random_Am(10, 40);
				GameScreen.addEffectEnd(134, subtype, num3, num4, Dir, objMainEff);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void endUpdate()
	{
	}

	private void updateNamThach_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f == 1)
			{
				if (point.dis == 0)
				{
					int tile = GameCanvas.loadmap.getTile(point.x / 1000, point.y / 1000);
					if (tile == 0 || tile == 2)
					{
						if (point.frame == 0)
						{
							GameScreen.addEffectEnd(63, 0, point.x / 1000, point.y / 1000, Dir, objMainEff);
						}
						if (point.frame == 1)
						{
							GameScreen.addEffectEnd(63, 3, point.x / 1000, point.y / 1000, Dir, objMainEff);
						}
					}
					else
					{
						point.isRemove = true;
					}
				}
				if (CRes.random(6) == 0)
				{
					GameScreen.addEffectEnd(110, point.frame, point.x / 1000, point.y / 1000, Dir, objMainEff);
				}
			}
			if (point.f / 2 >= fraImgEff.nFrame || point.isRemove)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f < 7)
		{
			int num = 0;
			if (typeEffect == 239)
			{
				num = f * 10;
			}
			Point point2 = new Point();
			point2.x = x * 1000 + CRes.getcos(35 + num) * (6 - f) * vMax;
			point2.y = y * 1000 + CRes.getsin(35 + num) * (6 - f) * (vMax - 4);
			point2.dis = f % 2;
			point2.fSmall = 0;
			VecEff.addElement(point2);
			Point point3 = new Point();
			point3.x = x * 1000 + CRes.getcos(145 + num) * (6 - f) * vMax;
			point3.y = y * 1000 + CRes.getsin(145 + num) * (6 - f) * (vMax - 4);
			point3.dis = f % 2;
			point3.fSmall = 1;
			VecEff.addElement(point3);
			Point point4 = new Point();
			point4.x = x * 1000 + CRes.getcos(215 + num) * (6 - f) * vMax;
			point4.y = y * 1000 + CRes.getsin(215 + num) * (6 - f) * (vMax - 4);
			point4.dis = f % 2;
			point4.fSmall = 2;
			VecEff.addElement(point4);
			Point point5 = new Point();
			point5.x = x * 1000 + CRes.getcos(CRes.fixangle(325 + num)) * (6 - f) * vMax;
			point5.y = y * 1000 + CRes.getsin(CRes.fixangle(325 + num)) * (6 - f) * (vMax - 4);
			point5.dis = f % 2;
			point5.fSmall = 3;
			VecEff.addElement(point5);
			if (point2.f % 2 == 1)
			{
				int tile2 = GameCanvas.loadmap.getTile(point2.x / 10, point2.y / 10);
				if (tile2 == 0 || tile2 == 2)
				{
					GameScreen.addEffectEnd(63, 0, point2.x / 10, point2.y / 10, Dir, objMainEff);
				}
			}
			if (f % 4 == 2 && isAddSound)
			{
				mSound.playSound(4, mSound.volumeSound);
			}
		}
		else if (f < 20)
		{
			if (f == 7 && !checkNullObject(2))
			{
				setAva(2, objBeFireMain);
				if (isAddSound)
				{
					mSound.playSound(43, mSound.volumeSound);
				}
			}
			GameScreen.addEffectEnd(108, 7, x, y - CRes.random(240), Dir, objMainEff);
			if (CRes.random(3) == 0)
			{
				GameScreen.addEffectEnd(110, 1, x, y, Dir, objMainEff);
			}
			y1000 += 60;
			if (y1000 > 480)
			{
				y1000 = 480;
			}
			if (f % 2 == 1)
			{
				int num2 = 0;
				if (typeEffect == 239)
				{
					num2 = (f - 7) / 2 * 5;
				}
				disHard++;
				Point point6 = new Point();
				point6.x = x * 1000 + CRes.getcos(CRes.fixangle(num2)) * ((f - 5) / 2) * vMax;
				point6.y = y * 1000 + CRes.getsin(CRes.fixangle(num2)) * ((f - 5) / 2) * (vMax - 4);
				point6.frame = 1;
				point6.dis = disHard % 2;
				point6.fSmall = 0;
				VecEff.addElement(point6);
				Point point7 = new Point();
				point7.x = x * 1000 + CRes.getcos(90 + num2) * ((f - 5) / 2) * vMax;
				point7.y = y * 1000 + CRes.getsin(90 + num2) * ((f - 5) / 2) * (vMax - 4);
				point7.frame = 1;
				point7.fSmall = 1;
				point7.dis = disHard % 2;
				VecEff.addElement(point7);
				Point point8 = new Point();
				point8.x = x * 1000 + CRes.getcos(180 + num2) * ((f - 5) / 2) * vMax;
				point8.y = y * 1000 + CRes.getsin(180 + num2) * ((f - 5) / 2) * (vMax - 4);
				point8.frame = 1;
				point8.dis = disHard % 2;
				point8.fSmall = 2;
				VecEff.addElement(point8);
				Point point9 = new Point();
				point9.x = x * 1000 + CRes.getcos(CRes.fixangle(270 + num2)) * ((f - 5) / 2) * vMax;
				point9.y = y * 1000 + CRes.getsin(CRes.fixangle(270 + num2)) * ((f - 5) / 2) * (vMax - 4);
				point9.frame = 1;
				point9.fSmall = 3;
				point9.dis = disHard % 2;
				VecEff.addElement(point9);
			}
		}
		if (f == fRemove - 5)
		{
			setAva(2, objBeFireMain);
			GameScreen.addEffectEnd(112, 1, x, y, Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
		else if (f % 4 == 0)
		{
			LoadMap.timeVibrateScreen = 105;
			GameScreen.addEffectEnd(59, 0, x + CRes.random_Am_0(15), y + 5 + CRes.random_Am_0(5), Dir, objMainEff);
		}
	}

	private void updateWapol_1()
	{
		if (f < fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.x = x;
				objFireMain.y = y;
				objFireMain.dy = 4;
			}
			if (f % 2 == 1)
			{
				Point point = new Point(x, y);
				point.frame = 0;
				VecEff.addElement(point);
			}
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point2 = (Point)VecEff.elementAt(i);
			point2.f++;
			if (point2.f >= 4)
			{
				VecEff.removeElement(point2);
				i--;
			}
		}
		if (f == fRemove)
		{
			objFireMain.plashNow.setIsNextf(0);
			vx = 0;
			vy = 0;
			if (!checkNullObject(1))
			{
				objFireMain.dy = 0;
			}
			Point point3 = new Point(toX, toY - 24);
			point3.frame = 1;
			VecEff.addElement(point3);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void updateWapol_3()
	{
		if (f == 4)
		{
			GameScreen.addEffectEnd(30, 0, x, y, 200, Dir, objMainEff);
		}
		if (f >= 9 && f <= fRemove && f % 3 == 0)
		{
			if (indexObjBefire < vecObjsBeFire.size())
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
				indexObjBefire++;
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus p = new Point_Focus();
						int xdich = mainObject.x - x;
						int ydich = mainObject.y - mainObject.hOne / 2 - y;
						p = create_Speed(xdich, ydich, p);
						VecEff.addElement(p);
					}
				}
			}
			else
			{
				Point_Focus p2 = new Point_Focus();
				int num = 120 + CRes.random_Am_0(30);
				int ydich2 = CRes.random_Am_0(50);
				if (Dir == 0)
				{
					num = -num;
				}
				p2 = create_Speed(num, ydich2, p2);
				VecEff.addElement(p2);
			}
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(57, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void updateMr3_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(103, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				if (point_Focus.objMain != null)
				{
					setAva(2, point_Focus.objMain);
					GameScreen.addEffectEnd(8, 0, point_Focus.objMain.x, point_Focus.objMain.y - point_Focus.objMain.hOne / 2, Dir, point_Focus.objMain);
				}
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 9 && !checkNullObject(2))
		{
			if (vecObjsBeFire.size() > 1)
			{
				fRemove = 25;
				for (int j = 0; j < vecObjsBeFire.size(); j++)
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							Point_Focus p = new Point_Focus();
							int xdich = mainObject.x - x;
							int ydich = mainObject.y - y;
							p = create_Speed(xdich, ydich, p, x, y, mainObject.x, mainObject.y);
							p.dis = Dir;
							p.objMain = mainObject;
							VecEff.addElement(p);
						}
					}
				}
			}
			else
			{
				if (Dir == 0)
				{
					toX = objBeFireMain.x + 10;
				}
				else
				{
					toX = objBeFireMain.x - 10;
				}
				toY = objBeFireMain.y + 5;
				int xdich2 = toX - x;
				int ydich2 = toY - y;
				Point_Focus p2 = new Point_Focus();
				p2 = create_Speed(xdich2, ydich2, p2);
				p2.dis = Dir;
				p2.objMain = objBeFireMain;
				VecEff.addElement(p2);
				fRemove = 15 + p2.fRe;
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void updateMr3_2()
	{
		if (f == 1 || f == 10)
		{
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill == null)
				{
					continue;
				}
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					Point_Focus point_Focus = new Point_Focus();
					point_Focus.objMain = mainObject;
					int xdich = mainObject.x - x;
					int ydich = mainObject.y - mainObject.hOne / 2 - y;
					point_Focus = create_Speed(xdich, ydich, point_Focus);
					if (f == 1)
					{
						point_Focus.frame = 0;
					}
					if (f == 10)
					{
						point_Focus.frame = 1;
					}
					VecEff.addElement(point_Focus);
				}
			}
		}
		if (f == 6 && !checkNullObject(1))
		{
			objFireMain.addChat(T.haha, isStop: true);
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(j);
			point_Focus2.update_Vx_Vy();
			if (point_Focus2.f >= point_Focus2.fRe)
			{
				if (point_Focus2.frame == 0)
				{
					Point point = new Point();
					point.frame = point_Focus2.frame;
					point.fRe = point_Focus2.fRe * 2 + 10;
					point.obj = point_Focus2.objMain;
					VecSubEff.addElement(point);
				}
				else
				{
					Point point2 = new Point();
					point2.frame = point_Focus2.frame;
					point2.fRe = CRes.random(12, 20);
					point2.obj = point_Focus2.objMain;
					VecSubEff.addElement(point2);
				}
				VecEff.removeElement(point_Focus2);
				j--;
			}
		}
		for (int k = 0; k < VecSubEff.size(); k++)
		{
			Point point3 = (Point)VecSubEff.elementAt(k);
			point3.update();
			if (point3.f >= point3.fRe)
			{
				VecSubEff.removeElement(point3);
				k--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0 && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void updateMissMS_1()
	{
		if (f >= 30)
		{
			if (!checkNullObject(1) && f == 30)
			{
				objFireMain.isTanHinh = false;
				objFireMain.isPaintLeg = false;
				objFireMain.dy = -20;
			}
			if (!checkNullObject(1) && f == 31)
			{
				objFireMain.isTanHinh = false;
				objFireMain.isPaintLeg = false;
				objFireMain.dy = -10;
			}
			if (!checkNullObject(1) && f == 32)
			{
				objFireMain.isTanHinh = false;
				objFireMain.isPaintLeg = true;
				objFireMain.dy = 10;
			}
			if (!checkNullObject(1) && f == 33)
			{
				objFireMain.isTanHinh = false;
				objFireMain.isPaintLeg = true;
				objFireMain.dy = 20;
			}
		}
		else if (f >= 2 && f < 30 && !checkNullObject(1))
		{
			objFireMain.isTanHinh = true;
		}
		if (f >= 8 && f % 5 == 0 && indexObjBefire < vecObjsBeFire.size())
		{
			addSound(15);
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
			indexObjBefire++;
			if (object_Effect_Skill != null)
			{
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					Point point = new Point();
					point.x = mainObject.x;
					point.y = mainObject.y;
					point.fRe = 12;
					VecEff.addElement(point);
					Point point2 = new Point();
					point2.x = mainObject.x;
					point2.y = mainObject.y + 10;
					point2.fRe = 20;
					VecSubEff.addElement(point2);
					setAva(2, mainObject);
				}
			}
			addVir(3, 5, 10, isPlayer: false);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point3 = (Point)VecEff.elementAt(i);
			point3.update();
			if (point3.f >= point3.fRe)
			{
				VecEff.removeElement(point3);
				i--;
			}
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point point4 = (Point)VecSubEff.elementAt(j);
			point4.update();
			if (point4.f < 3)
			{
				point4.frame = point4.f;
			}
			if (point4.f > point4.fRe - 3)
			{
				point4.frame = point4.fRe - point4.f;
			}
			if (point4.f >= point4.fRe)
			{
				VecSubEff.removeElement(point4);
				j--;
			}
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
	}

	private void updateHoDen()
	{
		if (GameCanvas.gameTick % 20 == 0)
		{
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill != null)
				{
					GameScreen.addEffectSkill2(-1, objFireMain, object_Effect_Skill, x + posSmock[CRes.random(posSmock.Length - 1)], y - 200 + CRes.random_Am(-10, 10));
					GameScreen.addEffectSkill2(-1, objFireMain, object_Effect_Skill, x + posSmock[CRes.random(posSmock.Length - 1)], y - 200 + CRes.random_Am(-10, 10));
				}
			}
		}
		if (f == 16 && tickadd <= 1)
		{
			tickadd++;
			GameScreen.addEffectEnd(164, 0, x, y, Dir, objMainEff);
		}
		if (f == 10 || f == 16)
		{
			for (int j = 0; j < 4; j++)
			{
				Point point = new Point();
				switch (j)
				{
				case 0:
					point.x = (x - 80) * 10;
					point.y = y * 10;
					point.vx = CRes.random(30, 50);
					point.vy = CRes.random(30, 50);
					break;
				case 1:
					point.x = x * 10;
					point.y = (y - 40) * 10;
					point.vx = -CRes.random(40, 60);
					point.vy = CRes.random(20, 40);
					break;
				case 2:
					point.x = x * 10;
					point.y = (y + 40) * 10;
					point.vx = CRes.random(40, 60);
					point.vy = -CRes.random(25, 45);
					break;
				case 3:
					point.x = (x + 80) * 10;
					point.y = y * 10;
					point.vx = -CRes.random(30, 50);
					point.vy = -CRes.random(30, 50);
					break;
				}
				if ((j % 2 == 1 && f == 10) || (j % 2 == 0 && f == 16))
				{
					point.frame = 1;
				}
				point.fRe = 22;
				VecSubEff.addElement(point);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
		for (int k = 0; k < VecEff.size(); k++)
		{
			Point point2 = (Point)VecEff.elementAt(k);
			point2.f++;
			if (point2.f >= 6)
			{
				VecEff.removeElement(point2);
				k--;
			}
		}
		if (f <= 16)
		{
			return;
		}
		if (y1000 > 0)
		{
			y1000 -= 120;
			if (y1000 < 0)
			{
				y1000 = 0;
			}
		}
		if (y1000 == 0 && f < fRemove)
		{
			if (CRes.random(2) == 0)
			{
				Point o = new Point(x + CRes.random_Am_0(20), y + 5 + CRes.random_Am_0(10));
				VecEff.addElement(o);
			}
			if (f % 4 == 0)
			{
				LoadMap.timeVibrateScreen = 105;
			}
		}
	}

	private void updateSet_1()
	{
		if (f == 10 || (f == 16 && typeEffect == 237))
		{
			for (int i = 0; i < 4; i++)
			{
				Point point = new Point();
				switch (i)
				{
				case 0:
					point.x = (x - 80) * 10;
					point.y = y * 10;
					point.vx = CRes.random(30, 50);
					point.vy = CRes.random(30, 50);
					break;
				case 1:
					point.x = x * 10;
					point.y = (y - 40) * 10;
					point.vx = -CRes.random(40, 60);
					point.vy = CRes.random(20, 40);
					break;
				case 2:
					point.x = x * 10;
					point.y = (y + 40) * 10;
					point.vx = CRes.random(40, 60);
					point.vy = -CRes.random(25, 45);
					break;
				case 3:
					point.x = (x + 80) * 10;
					point.y = y * 10;
					point.vx = -CRes.random(30, 50);
					point.vy = -CRes.random(30, 50);
					break;
				}
				if (typeEffect == 237 && ((i % 2 == 1 && f == 10) || (i % 2 == 0 && f == 16)))
				{
					point.frame = 1;
				}
				point.fRe = 22;
				VecSubEff.addElement(point);
			}
			if (isAddSound)
			{
				mSound.playSound(38, mSound.volumeSound);
			}
		}
		if (f == fRemove - 5)
		{
			setAva(2, objBeFireMain);
			GameScreen.addEffectEnd(112, 0, x, y + 10, Dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.f++;
			if (point2.f >= fraImgSub2Eff.nFrame * 2)
			{
				VecEff.removeElement(point2);
				j--;
			}
		}
		for (int k = 0; k < VecSubEff.size(); k++)
		{
			Point point3 = (Point)VecSubEff.elementAt(k);
			point3.update();
			if (point3.f % 5 == 0)
			{
				int tile = GameCanvas.loadmap.getTile(point3.x / 10, point3.y / 10);
				if (tile == 0 || tile == 2)
				{
					GameScreen.addEffectEnd(63, 0, point3.x / 10, point3.y / 10, Dir, objMainEff);
				}
			}
			if (point3.f >= point3.fRe)
			{
				VecSubEff.removeElement(point3);
				k--;
			}
		}
		if (f == 16 && isAddSound)
		{
			mSound.playSound(37, mSound.volumeSound);
		}
		if (f <= 16)
		{
			return;
		}
		if (y1000 > 0)
		{
			y1000 -= 120;
			if (y1000 < 0)
			{
				y1000 = 0;
			}
		}
		if (y1000 == 0 && f < fRemove)
		{
			GameScreen.addEffectEnd(108, 6, x, y - CRes.random(240), Dir, objMainEff);
			if (CRes.random(2) == 0)
			{
				Point o = new Point(x + CRes.random_Am_0(20), y + 5 + CRes.random_Am_0(10));
				VecEff.addElement(o);
			}
			if (f % 4 == 0)
			{
				LoadMap.timeVibrateScreen = 105;
				GameScreen.addEffectEnd(110, 0, x + CRes.random_Am_0(15), y + 5 + CRes.random_Am_0(5), Dir, objMainEff);
			}
		}
	}

	private void updateSet_2()
	{
		if (f >= 10 && f <= 20)
		{
			if (isAddSound && f % 3 == 0)
			{
				mSound.playSound(17, mSound.volumeSound);
			}
			if (indexObjBefire < vecObjsBeFire.size())
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
				indexObjBefire++;
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						int num = 1 + CRes.random(2);
						for (int i = 0; i < num; i++)
						{
							Point_Focus point_Focus = new Point_Focus();
							point_Focus.x = mainObject.x + 300;
							if (Dir == 2)
							{
								point_Focus.x = mainObject.x - 300;
							}
							point_Focus.y = mainObject.y - 400;
							point_Focus.toX = mainObject.x + CRes.random_Am_0(20);
							point_Focus.toY = mainObject.y + CRes.random_Am_0(10);
							point_Focus = create_Speed(point_Focus.toX - point_Focus.x, point_Focus.toY - point_Focus.y, point_Focus, point_Focus.x, point_Focus.y, point_Focus.toX, point_Focus.toY);
							point_Focus.dis = CRes.random(16, 30);
							point_Focus.maxdis = CRes.random(10, 25);
							point_Focus.frame = CRes.random(fraImgEff.nFrame);
							if (i == 0)
							{
								point_Focus.goc = 1;
							}
							if (typeEffect == 238 && CRes.random(2) == 0)
							{
								point_Focus.typeSpec = 1;
							}
							VecEff.addElement(point_Focus);
						}
					}
				}
			}
			else if (!checkNullObject(2))
			{
				int num2 = 1 + CRes.random(4) / 3;
				for (int j = 0; j < num2; j++)
				{
					Point_Focus point_Focus2 = new Point_Focus();
					point_Focus2.x = objBeFireMain.x + 300;
					if (Dir == 2)
					{
						point_Focus2.x = objBeFireMain.x - 300;
					}
					point_Focus2.y = objBeFireMain.y - 400;
					point_Focus2.toX = objBeFireMain.x + CRes.random_Am_0(160);
					point_Focus2.toY = objBeFireMain.y + CRes.random_Am_0(80);
					point_Focus2 = create_Speed(point_Focus2.toX - point_Focus2.x, point_Focus2.toY - point_Focus2.y, point_Focus2, point_Focus2.x, point_Focus2.y, point_Focus2.toX, point_Focus2.toY);
					point_Focus2.dis = CRes.random(16, 30);
					point_Focus2.maxdis = CRes.random(10, 25);
					point_Focus2.frame = CRes.random(fraImgEff.nFrame);
					if (typeEffect == 238 && CRes.random(2) == 0)
					{
						point_Focus2.typeSpec = 1;
					}
					VecEff.addElement(point_Focus2);
				}
			}
		}
		for (int k = 0; k < VecEff.size(); k++)
		{
			Point_Focus point_Focus3 = (Point_Focus)VecEff.elementAt(k);
			point_Focus3.update_Vx_Vy();
			if (point_Focus3.f == point_Focus3.fRe)
			{
				point_Focus3.vx = 0;
				point_Focus3.vy = 0;
				point_Focus3.x = point_Focus3.toX;
				point_Focus3.y = point_Focus3.toY;
				if (CRes.random(3) == 0 || point_Focus3.goc == 1)
				{
					Point point = new Point();
					point.x = point_Focus3.x * 10;
					point.y = point_Focus3.y * 10;
					point.vx = CRes.random_Am_0(30);
					point.vy = CRes.random_Am_0(30);
					point.fRe = 14 + CRes.random(6);
					point.frame = point_Focus3.typeSpec;
					VecSubEff.addElement(point);
					GameScreen.addEffectEnd(59, 0, point_Focus3.x, point_Focus3.y, Dir, objMainEff);
				}
				if (GameCanvas.loadmap.getTile(point_Focus3.x, point_Focus3.y) == -1)
				{
					point_Focus3.isSpeedUp = true;
				}
				else
				{
					GameScreen.addEffectEnd(63, 0, point_Focus3.x, point_Focus3.y, Dir, objMainEff);
				}
			}
			if (point_Focus3.f % 2 == 0)
			{
				point_Focus3.frame++;
				if (point_Focus3.frame >= fraImgEff.maxNumFrame)
				{
					point_Focus3.frame = 0;
				}
			}
			if (point_Focus3.f >= point_Focus3.fRe + point_Focus3.dis || point_Focus3.isSpeedUp)
			{
				VecEff.removeElement(point_Focus3);
				k--;
			}
		}
		for (int l = 0; l < VecSubEff.size(); l++)
		{
			Point point2 = (Point)VecSubEff.elementAt(l);
			point2.update();
			if (point2.f % 8 == 0)
			{
				int tile = GameCanvas.loadmap.getTile(point2.x / 10, point2.y / 10);
				if (tile == 0 || tile == 2)
				{
					GameScreen.addEffectEnd(63, 0, point2.x / 10, point2.y / 10, Dir, objMainEff);
				}
			}
			if (point2.f >= point2.fRe)
			{
				VecSubEff.removeElement(point2);
				l--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0 && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	private void updateZoroS2_L1_NEW()
	{
		if (f == 1)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			GameScreen.addEffectEnd(16, 0, objFireMain.x, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2, Dir, objMainEff);
		}
		if (f == 2)
		{
			GameScreen.addEffectEnd(26, 1, objBeFireMain.x, objBeFireMain.y, 0, objMainEff);
		}
		if (f == 4)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			int num = 10;
			if (Dir == 0)
			{
				num = -10;
			}
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2, Dir, objMainEff);
		}
		if (f == 5)
		{
			GameScreen.addEffectEnd(26, 1, objBeFireMain.x, objBeFireMain.y, 2, objMainEff);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public override void stopUpdateNormal()
	{
		removeEff();
	}

	public override void removeEff()
	{
		if (objFireMain == GameScreen.player && GameScreen.typePaintGameScreen == 1)
		{
			GameScreen.isPaintNormal();
		}
		if (!isEff)
		{
			AddNumAndEffPlus(vecObjsBeFire);
		}
		VecEff.removeAllElements();
		VecSubEff.removeAllElements();
		isStop = true;
		f = -1;
	}

	private void AddNumAndEffPlus(mVector vec)
	{
		if (vec == null || vec.size() == 0)
		{
			return;
		}
		for (int i = 0; i < vec.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vec.elementAt(i);
			MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
			if (mainObject == null || mainObject.returnAction())
			{
				continue;
			}
			bool flag = setAddEffPlus(object_Effect_Skill, mainObject, objFireMain, objMainEff);
			if (mainObject.Hp <= 0 && mainObject.Action != 4)
			{
				mainObject.beginDie(objFireMain);
			}
			sbyte typeColor = 15;
			if (!checkNullObject(1) && objFireMain == GameScreen.player)
			{
				typeColor = 13;
			}
			int num = object_Effect_Skill.hpShow;
			if (objFireMain.typeObject == 1)
			{
				typeColor = 14;
				num = -num;
			}
			if (objFireMain != GameScreen.player && mainObject != GameScreen.player && GameCanvas.lowGraphic)
			{
				continue;
			}
			if (object_Effect_Skill.hpShow == 0)
			{
				GameScreen.addEffectNumBig_NEW_AP(num, object_Effect_Skill.hpMagic, mainObject.x, mainObject.y - mainObject.hOne, 17);
			}
			else
			{
				if (flag)
				{
					typeColor = 16;
				}
				GameScreen.addEffectNumBig_NEW_AP(num, object_Effect_Skill.hpMagic, mainObject.x, mainObject.y - mainObject.hOne, typeColor);
			}
			int num2 = HasHapThuEffPlus(object_Effect_Skill, mainObject, objFireMain, objMainEff);
			if (num2 >= 0 && object_Effect_Skill.mEff_HP_Plus[num2] > 0)
			{
				GameScreen.addEffectNumBig_NEW_AP(object_Effect_Skill.mEff_HP_Plus[num2], object_Effect_Skill.hpMagic, mainObject.x, mainObject.y - mainObject.hOne, 25);
			}
		}
		if (objFireMain != GameScreen.player && objFireMain.Hp <= 0 && objFireMain.Action != 4)
		{
			objFireMain.beginDie(objFireMain);
		}
	}

	private void beginCreate()
	{
	}

	public void createNormal()
	{
		fRemove = 60;
		if (subType == 0)
		{
			fraImgEff = new FrameImage(0, 14, 14);
		}
		vMax = 8000;
		numNextFrame = 2;
		setInfoNormal(objFireMain);
	}

	public void createPhaohoa()
	{
		y -= 6;
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		fRemove = 20;
		vMax = 10;
		numNextFrame = 2;
		fraImgEff = new FrameImage(111, 40, 30, 40, 30);
		GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2 - 3, 300, Dir, objMainEff);
		if (isAddSound)
		{
			mSound.playSound(24, mSound.volumeSound);
		}
	}

	public void createPhaohoa_L6()
	{
		y -= 6;
		x += 30 * am_duong;
		fRemove = 20;
		vMax = 10;
		numNextFrame = 2;
		fraImgEff = new FrameImage(418, 6);
		GameScreen.addEffectEnd(53, 0, x, objFireMain.y - objFireMain.hOne / 2 - 3, 300, Dir, objMainEff);
		GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2 - 3, 300, Dir, objMainEff);
		if (isAddSound)
		{
			mSound.playSound(24, mSound.volumeSound);
		}
	}

	public void createLuffy1()
	{
		fRemove = vecObjsBeFire.size() * 3 + 6;
		if (fRemove < 12)
		{
			fRemove = 12;
		}
		fraImgEff = new FrameImage(1, 80, 40);
		if (typeEffect == 37)
		{
			fraImgSubEff = new FrameImage(27, 24, 32);
		}
		if (Dir == 0)
		{
			x -= 20;
		}
		else
		{
			x += 20;
		}
		if (isAddSound)
		{
			mSound.playSound(4, mSound.volumeSound);
		}
	}

	public void createSanji1()
	{
		y = objFireMain.y;
		fRemove = 12;
	}

	public void createZoro1()
	{
		fraImgEff = new FrameImage(10, 40, 47);
		int a = objFireMain.x - objBeFireMain.x;
		if (CRes.abs(a) > 50)
		{
			fRemove = 5;
			vx = (CRes.abs(a) - 24) / 5;
		}
		else if (CRes.abs(a) > 24)
		{
			vx = 5;
			fRemove = (CRes.abs(a) - 24) / 5;
		}
		else
		{
			fRemove = 1;
			vx = 0;
		}
		if (Dir == 0)
		{
			xplus = 20;
			vx = -vx;
		}
		else
		{
			xplus = -20;
		}
	}

	public void createZoro2()
	{
		fraImgEff = new FrameImage(10, 40, 47);
		fraImgSubEff = new FrameImage(11, 40, 50);
		fRemove = 7;
		yplus = objBeFireMain.hOne / 2;
		if (objFireMain != null)
		{
			objFireMain.isTanHinh = true;
			if (objFireMain.plashNow != null)
			{
				objFireMain.plashNow.setIsNextf(1);
			}
		}
		if (Dir == 0)
		{
			toX += 30;
		}
		else
		{
			toX -= 30;
		}
	}

	public void createUssopSea1()
	{
		fraImgEff = new FrameImage(12, 15, 15);
		vMax = 24;
		fRemove = 15;
		y -= 6;
		if (Dir == 0)
		{
			x -= 20;
		}
		else
		{
			x += 20;
		}
		GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2, 300, Dir, objMainEff);
	}

	public void createUssopSea2()
	{
		fraImgEff = new FrameImage(196, 15, 15);
		vMax = 24;
		fRemove = 20;
		y -= 6;
		if (Dir == 0)
		{
			x -= 20;
		}
		else
		{
			x += 20;
		}
	}

	public void createUssopSea3()
	{
		fraImgEff = new FrameImage(197, 15, 10);
		Dir = (sbyte)objFireMain.type_left_right;
		vMax = 12;
		fRemove = 20;
		y -= 6;
		if (Dir == 0)
		{
			x -= 20;
		}
		else
		{
			x += 20;
		}
		GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2, 600, Dir, objMainEff);
	}

	public void createUssop1()
	{
		if (typeEffect == 64)
		{
			fraImgEff = new FrameImage(20, 10, 10);
		}
		else if (typeEffect == 66)
		{
			fraImgEff = new FrameImage(20, 10, 10);
		}
		vMax = 24;
		fRemove = 5;
		GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
	}

	public void createUssop2()
	{
		fraImgEff = new FrameImage(20, 10, 10);
		vMax = 24;
		if (typeEffect == 206)
		{
			setAngle();
			fraImgEff = new FrameImage(305, 16, 12);
			fraImgSubEff = new FrameImage(304, 10, 7);
			vMax = 16;
		}
		else if (typeEffect == 207)
		{
			setAngle();
			fraImgEff = new FrameImage(20, 10, 10);
			fraImgSubEff = new FrameImage(304, 10, 7);
			vMax = 16;
		}
		fRemove = 5;
		y -= 6;
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		int num = toX - x;
		int num2 = toY - y;
		create_Speed(num, num2, null);
		if (typeEffect == 206)
		{
			int frameAngle = CRes.angle(num, num2);
			frame = setFrameAngle(frameAngle);
		}
		GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
		fPlayFrameSuper = fRemove;
		if (fRemove < 5)
		{
			fRemove = 5;
		}
	}

	public void createUssopSkill1_Lv3()
	{
		fraImgEff = new FrameImage(53, 9, 9);
		fraImgSubEff = new FrameImage(20, 10, 10);
		vMax = 24;
		fRemove = 5;
		y -= 6;
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		int xdich = toX - x;
		int ydich = toY - y;
		Point_Focus p = new Point_Focus();
		p = create_Speed(xdich, ydich, p);
		p.frame = 1;
		GameScreen.addEffectEnd(1, 0, x, y, Dir, objMainEff);
		VecEff.addElement(p);
	}

	public void createNami1()
	{
		fraImgEff = new FrameImage(22, 70, 50);
		fraImgSubEff = new FrameImage(298, 24, 24, 6);
		fRemove = 10;
		if (typeEffect == 53 || typeEffect == 163)
		{
			fraImgSub2Eff = new FrameImage(27, 24, 24);
		}
		indexEff_1 = objFireMain.indexEff_1;
		vMax = 12;
		y += 5;
		int num = 0;
		int xdich = toX - x;
		int ydich = toY - num - y;
		Point_Focus p = new Point_Focus();
		p = create_Speed(xdich, ydich, p);
		p.frame = 0;
		VecEff.addElement(p);
		if (isAddSound)
		{
			mSound.playSound(18, mSound.volumeSound);
		}
	}

	public void createNami1_SHORT()
	{
		fraImgEff = new FrameImage(22, 70, 50);
		fraImgSubEff = new FrameImage(298, 24, 24, 6);
		if (typeEffect == 190 || typeEffect == 222 || typeEffect == 312)
		{
			fraImgSubEff = new FrameImage(299, 26, 26, 2);
			if (typeEffect == 222 || typeEffect == 312)
			{
				fraImgEff = new FrameImage(324, 70, 50);
				fraImgSub3Eff = new FrameImage(326, 26, 26, 3);
			}
		}
		indexEff_1 = objFireMain.indexEff_1;
		fRemove = 24;
		fraImgSub2Eff = new FrameImage(27, 24, 24);
		vMax = 12;
		if (isAddSound)
		{
			mSound.playSound(22, mSound.volumeSound);
		}
		int num = 0;
		num = ((Dir != 0) ? (-15) : 15);
		GameScreen.addEffectEnd(30, 0, x + num, objFireMain.y - objFireMain.hOne / 2, 500, Dir, objMainEff);
	}

	public void createNamiSea1_2()
	{
		yplus = y;
		y += objFireMain.hOne / 2;
		vMax = 12;
		fraImgEff = new FrameImage(28, 46, 50, 46, 50);
		fraImgSubEff = new FrameImage(29, 28, 30, 28, 30);
		fraImgSub2Eff = new FrameImage(298, 24, 24, 6);
		indexEff_1 = objFireMain.indexEff_1;
		if (Dir == 0)
		{
			xplus = x - 20;
		}
		else
		{
			xplus = x + 20;
		}
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		if (typeEffect == 139)
		{
			fraImgSub3Eff = new FrameImage(27, 24, 24);
			GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2, 500, Dir, objMainEff);
		}
		else
		{
			fraImgSub3Eff = new FrameImage(13, 24, 24);
			GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2, 300, Dir, objMainEff);
		}
		if (isAddSound)
		{
			mSound.playSound(18, mSound.volumeSound);
		}
	}

	public void createNamiSea3()
	{
		yplus = y;
		y += objFireMain.hOne / 2;
		vMax = 12;
		fraImgSub2Eff = new FrameImage(298, 24, 24, 6);
		indexEff_1 = objFireMain.indexEff_1;
		if (Dir == 0)
		{
			xplus = x - 20;
		}
		else
		{
			xplus = x + 20;
		}
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		if (isAddSound)
		{
			mSound.playSound(18, mSound.volumeSound);
		}
		fraImgSub3Eff = new FrameImage(27, 24, 24);
		GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 2, 500, Dir, objMainEff);
		mImgframe = new FrameImage[3];
		mImgframe[0] = new FrameImage(25, 80, 40, 60, 30);
		mImgframe[1] = new FrameImage(24, 15, 60);
		mImgframe[2] = new FrameImage(81, 24, 24);
	}

	public void createSanji2()
	{
		numNextFrame = 2;
		vMax = 16;
		fRemove = 16;
		fraImgEff = new FrameImage(31, 70, 70);
		Dir = (sbyte)objFireMain.type_left_right;
		int time = 300;
		if (typeEffect == 12)
		{
			fraImgSubEff = new FrameImage(77, 64, 75, 43, 50);
			fraImgSub2Eff = new FrameImage(224, 22, 28);
			fraImgSub3Eff = new FrameImage(78, 22, 28);
			fRemove = 24;
			time = 600;
		}
		else if (typeEffect == 188 || typeEffect == 220 || typeEffect == 293)
		{
			fraImgSubEff = new FrameImage(282, 64, 75);
			if (typeEffect == 293)
			{
				fraImgSub2Eff = new FrameImage(406, 42, 34);
				fraImgSub4Eff = new FrameImage(283, 22, 28);
				fraImgSubEff = new FrameImage(412, 64, 75);
			}
			else if (typeEffect == 220)
			{
				fraImgSub2Eff = new FrameImage(325, 32, 31);
				fraImgSub4Eff = new FrameImage(224, 22, 28);
			}
			else
			{
				fraImgSub2Eff = new FrameImage(224, 22, 28);
			}
			fraImgSub3Eff = new FrameImage(283, 22, 28);
			fraImgEff = new FrameImage(284, 70, 70);
			fRemove = 24;
			time = 600;
		}
		else if (typeEffect == 49)
		{
			fraImgSubEff = new FrameImage(78, 22, 28);
			fraImgSub2Eff = new FrameImage(102, 35, 19);
		}
		else if (typeEffect == 50)
		{
			fraImgSub2Eff = new FrameImage(103, 35, 19, 35, 19);
			fraImgSubEff = new FrameImage(78, 22, 28);
		}
		x1000 = x;
		y1000 = objFireMain.y;
		if (Dir == 0)
		{
			x -= 16;
		}
		else
		{
			x += 16;
		}
		if (isAddSound)
		{
			mSound.playSound(16, mSound.volumeSound);
		}
		GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 3 * 2, time, Dir, objMainEff);
	}

	public void createRankyaku()
	{
		vMax = 16;
		fRemove = 22;
		Dir = (sbyte)objFireMain.type_left_right;
		fraImgEff = new FrameImage(428, 1);
		x1000 = x + 30 * am_duong;
		int xdich = x1000 - x;
		VecEff.addElement(create_Speed(xdich, 0, new Point_Focus(), x, y, toX, toY));
		VecEff.addElement(create_Speed(xdich, -7, new Point_Focus(), x, y, toX, toY));
		VecEff.addElement(create_Speed(xdich, 7, new Point_Focus(), x, y, toX, toY));
	}

	public void createSoi()
	{
		vMax = 12;
		fRemove = 20;
		Dir = (sbyte)objFireMain.type_left_right;
		fraImgEff = new FrameImage(429, 4);
		if (typeEffect == 277)
		{
			fraImgEff = new FrameImage(430, 4);
		}
	}

	public void createShigan()
	{
		vMax = 17;
		fRemove = 14;
		Dir = (sbyte)objFireMain.type_left_right;
		fraImgEff = new FrameImage(75, 1);
		x1000 = x + 30 * am_duong;
		int xdich = x1000 - x;
		VecEff.addElement(create_Speed(xdich, 0, new Point_Focus(), x, y, toX, toY));
		GameScreen.addEffectEnd(30, 0, x + 15 * am_duong, objFireMain.y - objFireMain.hOne / 3 * 2, 200, Dir, objMainEff);
	}

	public void createDoor()
	{
		fRemove = 26;
		fraImgEff = new FrameImage(426, 2);
		levelPaint = -1;
	}

	public void createHuou()
	{
		fRemove = 20;
		fraImgEff = new FrameImage(176, 3, 25, 1);
		fraImgSubEff = new FrameImage(220, 9, 9, 4);
		size1 = 30;
		if (typeEffect == 279)
		{
			size1 = 60;
		}
		if (GameCanvas.isLowGraOrWP_PvP())
		{
			size1 = 10;
		}
		for (int i = 0; i < size1; i++)
		{
			Point point = new Point();
			createPointHuou(point);
			point.vy = 20;
			VecEff.addElement(point);
		}
	}

	private Point createPointHuou(Point p)
	{
		p.frame = CRes.random(5);
		if (typeEffect == 279)
		{
			p.x = CRes.random_Am_0(60);
			p.y = -10 - CRes.random(60);
			p.dis = CRes.random(6);
		}
		else
		{
			p.x = CRes.random_Am_0(40);
			p.y = -10 - CRes.random(60);
			p.dis = 2;
		}
		return p;
	}

	public void createZoro3()
	{
		fRemove = 12;
		if (typeEffect == 15)
		{
			fRemove = 15;
		}
	}

	public void createZoro4()
	{
		fraImgSub2Eff = new FrameImage(71, 64, 25);
		fraImgEff = new FrameImage(88, 32, 70);
		fRemove = 20;
		vMax = 12;
	}

	public void createZoroSkill3_Lv1()
	{
		vMax = 12;
		y = objFireMain.y + 5;
	}

	public void createZoro8()
	{
		fraImgEff = new FrameImage(8, 40, 47, 40, 47);
		objFireMain.isTanHinh = true;
		if (objFireMain.plashNow != null)
		{
			objFireMain.plashNow.setIsNextf(1);
		}
		x = objFireMain.x;
		y = objFireMain.y;
		toX = objBeFireMain.x;
		toY = objBeFireMain.y;
		vMax = 20;
		int num = 0;
		int num2 = 0;
		num = toX - x;
		num2 = toY - y;
		int num3 = 90;
		int a = CRes.angle(num, num2);
		toX = x + num3 * CRes.getcos(a) / 1000;
		toY = y + num3 * CRes.getsin(a) / 1000;
		num = toX - x;
		num2 = toY - y;
		if (num2 == 0)
		{
			num2 = 1;
		}
		if (num == 0)
		{
			num = 1;
		}
		int num4 = 0;
		int num5 = 0;
		int num6 = MainObject.getDistance(num, num2) / vMax;
		if (num6 == 0)
		{
			num6 = 1;
		}
		num4 = num / num6;
		num5 = num2 / num6;
		if (CRes.abs(num4) > CRes.abs(num))
		{
			num4 = num;
		}
		if (CRes.abs(num5) > CRes.abs(num2))
		{
			num5 = num2;
		}
		vx = num4;
		vy = num5;
		fRemove = num6;
		if (fRemove > 0)
		{
			timeAddNum = (sbyte)(fRemove / 2);
		}
	}

	public void createLuffy6()
	{
		if (objFireMain == GameScreen.player)
		{
			GameScreen.setIsMoveEff(ismove: true);
		}
		fRemove = 11;
		if (!checkNullObject(3))
		{
			objFireMain.x = objBeFireMain.x + objFireMain.vMax * 3 * 7;
			if (Dir == 2)
			{
				objFireMain.x = objBeFireMain.x - objFireMain.vMax * 3 * 7;
			}
			objFireMain.y = objBeFireMain.y;
		}
		fraImgEff = new FrameImage(4, 20, 20);
		fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
	}

	public void createNamiSkill1_L3()
	{
		Dir = (sbyte)objFireMain.type_left_right;
		for (int i = 0; i < 2; i++)
		{
			int a = 25;
			if (objFireMain.hOne > 1)
			{
				a = objFireMain.hOne / 2;
			}
			Point o = new Point(x + CRes.random_Am_0(20), y + CRes.random_Am_0(a));
			VecEff.addElement(o);
		}
		fRemove = 16;
		if (Dir == 0)
		{
			x -= 20;
		}
		else
		{
			x += 20;
		}
		fraImgSubEff = new FrameImage(299, 26, 26, 2);
		fraImgEff = new FrameImage(273, 24, 24, 4);
		indexEff_1 = objFireMain.indexEff_1;
		if (isAddSound)
		{
			mSound.playSound(10, mSound.volumeSound);
		}
	}

	public void createNamiSkill3()
	{
		fRemove = 20;
		vMax = 10;
		x1000 = x;
		if (objFireMain.Dir == 0)
		{
			x -= 20;
		}
		else
		{
			x += 20;
		}
		indexEff_1 = objFireMain.indexEff_1;
		if (typeEffect == 31)
		{
			fraImgEff = new FrameImage(83, 14, 14);
			fraImgSubEff = new FrameImage(298, 24, 24, 6);
		}
		else if (typeEffect == 55 || typeEffect == 56 || typeEffect == 191 || typeEffect == 223 || typeEffect == 313)
		{
			fraImgEff = new FrameImage(81, 24, 24);
			fraImgSubEff = new FrameImage(299, 26, 26, 2);
			fraImgSub2Eff = new FrameImage(27, 24, 24);
			if (typeEffect == 56 || typeEffect == 191 || typeEffect == 223 || typeEffect == 313)
			{
				GameScreen.addEffectEnd(30, 0, x, objFireMain.y - objFireMain.hOne / 3 * 2, 1000, Dir, objMainEff);
				fRemove = 26;
			}
		}
		if (isAddSound)
		{
			mSound.playSound(10, mSound.volumeSound);
		}
	}

	public void createNamiSkill1()
	{
		fRemove = 16;
		if (Dir == 0)
		{
			x -= 20;
		}
		else
		{
			x += 20;
		}
		indexEff_1 = objFireMain.indexEff_1;
		fraImgSubEff = new FrameImage(298, 24, 24, 6);
		if (typeEffect == 51)
		{
			fraImgEff = new FrameImage(299, 26, 26, 2);
		}
		if (isAddSound)
		{
			mSound.playSound(10, mSound.volumeSound);
		}
	}

	public void createAlvida2()
	{
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		y -= 15;
		fraImgEff = new FrameImage(116, 38, 53);
		fraImgSubEff = new FrameImage(117, 38, 22);
		fRemove = 10;
		int num = x;
		num = ((Dir != 0) ? (num - 45) : (num + 45));
		GameScreen.addEffectEnd(30, 0, num, y - 30, 300, Dir, objMainEff);
	}

	public void createAlvida1()
	{
		if (Dir == 0)
		{
			x -= 26;
		}
		else
		{
			x += 26;
		}
		y -= 15;
		fraImgEff = new FrameImage(116, 38, 53);
		fRemove = 2;
		addSound(2);
	}

	public void createMon_4_5()
	{
		if (Dir == 0)
		{
			x -= 14;
		}
		else
		{
			x += 14;
		}
		y -= 10;
		fRemove = 6;
		if (typeEffect == 73)
		{
			fraImgEff = new FrameImage(115, 34, 27);
		}
		else
		{
			fraImgEff = new FrameImage(35, 34, 27);
		}
	}

	public void createMon6()
	{
		if (Dir == 0)
		{
			x -= 14;
		}
		else
		{
			x += 14;
		}
		x1000 = x;
		y1000 = y - 10;
		vMax = 14;
		fraImgEff = new FrameImage(47, 41, 14);
		fraImgSubEff = new FrameImage(35, 34, 27);
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
		frame = CRes.random(fraImgSubEff.nFrame);
	}

	public void createMon3()
	{
		if (Dir == 0)
		{
			x -= 25;
		}
		else
		{
			x += 25;
		}
		vMax = 14;
		fraImgEff = new FrameImage(20, 10, 10);
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
		GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
	}

	public void createMon2()
	{
		vMax = 12;
		if (typeEffect == 145)
		{
			fraImgEff = new FrameImage(60, 15, 15);
		}
		else if (typeEffect == 146)
		{
			fraImgEff = new FrameImage(59, 23, 23);
		}
		else if (typeEffect == 147)
		{
			fraImgEff = new FrameImage(20, 10, 10);
		}
		else if (typeEffect == 148)
		{
			fraImgEff = new FrameImage(73, 20, 20);
			numNextFrame = 2;
			vMax = 14;
		}
		else
		{
			fraImgEff = new FrameImage(114, 21, 14);
		}
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
		objFireMain.isPaintWeapon = false;
	}

	public void createZoro_New2()
	{
		fRemove = 44;
		vMax = 12;
		fraImgEff = new FrameImage(8, 40, 47, 40, 47);
		if (isAddSound)
		{
			mSound.playSound(8, mSound.volumeSound);
		}
		GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
	}

	public void createZoro_New1()
	{
		fRemove = 50;
		vMax = 12;
		fraImgEff = new FrameImage(88, 32, 70);
		y = objFireMain.y;
	}

	public void createZoro_S1_L3_SHORT()
	{
		fRemove = 16;
		if (typeEffect == 183 || typeEffect == 215)
		{
			fRemove = 18;
		}
		vMax = 12;
		fraImgEff = new FrameImage(88, 32, 70);
		if (typeEffect == 215)
		{
			fraImgEff = new FrameImage(319, 32, 70);
		}
		y = objFireMain.y;
	}

	public void createZoro_S1_L6()
	{
		fRemove = 20;
		vMax = 18;
		fraImgEff = new FrameImage(422, 32, 70);
		y = objFireMain.y;
		x1000 = x + 30 * am_duong;
		xLight1 = x1000;
		xLight2 = x1000;
		fraImgSub2Eff = new FrameImage(417, 3);
		int xdich = x1000 - x;
		int ydich = 0;
		VecSubEff.addElement(create_Speed(xdich, ydich, new Point_Focus(), x, y - objFireMain.hOne - 20, toX, toY));
		VecSubEff.addElement(create_Speed(xdich, ydich, new Point_Focus(), x, y - objFireMain.hOne - 15, toX, toY));
	}

	public void createLuffy_New3()
	{
		levelPaint = -1;
		fRemove = 30;
		if (typeEffect == 182)
		{
			fraImgEff = new FrameImage(276, 90, 50);
		}
		else if (typeEffect == 214 || typeEffect == 273)
		{
			fraImgEff = new FrameImage(317, 90, 50);
			levelPaint = 0;
		}
		else
		{
			fraImgEff = new FrameImage(1, 80, 40);
		}
		fraImgSubEff = new FrameImage(27, 24, 32);
		fraImgSub2Eff = new FrameImage(8, 40, 47, 40, 47);
		Dir = (sbyte)objFireMain.type_left_right;
	}

	public void createLuffy_New2()
	{
		if (objFireMain == GameScreen.player)
		{
			GameScreen.setIsMoveEff(ismove: true);
		}
		if (!checkNullObject(3))
		{
			objFireMain.x = objBeFireMain.x + 30;
			if (Dir == 2)
			{
				objFireMain.x = objBeFireMain.x - 30;
			}
			objFireMain.y = objBeFireMain.y;
		}
		int num = -15;
		if (Dir == 0)
		{
			num = 15;
		}
		GameScreen.addEffectEnd(30, 0, x + num, y, 300, Dir, objMainEff);
		fraImgEff = new FrameImage(4, 20, 20);
		fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
		fraImgSub2Eff = new FrameImage(11, 40, 50);
		fRemove = 34;
	}

	public void createLuffy_New2_SHORT()
	{
		if (objFireMain == GameScreen.player)
		{
			GameScreen.setIsMoveEff(ismove: true);
		}
		if (!checkNullObject(3))
		{
			objFireMain.x = objBeFireMain.x + 30;
			if (Dir == 2)
			{
				objFireMain.x = objBeFireMain.x - 30;
			}
			objFireMain.y = objBeFireMain.y;
		}
		fraImgEff = new FrameImage(4, 20, 20);
		fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
		fraImgSub2Eff = new FrameImage(11, 40, 50);
		if (typeEffect == 213 || typeEffect == 272)
		{
			fraImgSub3Eff = new FrameImage(316, 44, 47);
		}
		fRemove = 24;
	}

	public void createMon_1()
	{
		if (Dir == 0)
		{
			x1000 = x - 10;
			x -= 20;
		}
		else
		{
			x1000 = x + 10;
			x += 20;
		}
		y1000 = y - 12;
		fraImgEff = new FrameImage(114, 16, 13);
		fraImgSubEff = new FrameImage(35, 34, 27);
		fRemove = 6;
		vx = 3 * am_duong;
	}

	public void createMon_10()
	{
		fRemove = 5;
		fraImgEff = new FrameImage(120, 50, 25);
		if (typeEffect == 143)
		{
			fraImgEff = new FrameImage(2, 53, 29);
		}
		if (typeEffect == 149)
		{
			fraImgEff = new FrameImage(68, 28, 44);
		}
		numNextFrame = 1;
		if (Dir == 0)
		{
			x -= 10;
		}
		else
		{
			x += 10;
		}
		if (Dir == 0)
		{
			vx = -8;
		}
		else
		{
			vx = 8;
		}
	}

	public void createMon_11()
	{
		numNextFrame = 1;
		if (Dir == 0)
		{
			x -= 10;
		}
		else
		{
			x += 10;
		}
		if (Dir == 0)
		{
			vx1000 = -12;
		}
		else
		{
			vx1000 = 12;
		}
		vMax = 12;
		fraImgEff = new FrameImage(120, 50, 25);
		if (typeEffect == 144)
		{
			fraImgEff = new FrameImage(2, 53, 29);
		}
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
	}

	public void createCausu_1()
	{
		fRemove = 26;
		if (typeEffect == 227)
		{
			fraImgEff = new FrameImage(317, 90, 50);
			fraImgSubEff = new FrameImage(334, 75, 42);
		}
		else
		{
			fraImgEff = new FrameImage(1, 80, 40);
			fraImgSubEff = new FrameImage(62, 48, 34);
		}
		if (!checkNullObject(3))
		{
			objFireMain.x = objBeFireMain.x - am_duong * 48;
			objFireMain.y = objBeFireMain.y;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
		}
		for (int i = 0; i < 2; i++)
		{
			Point point = new Point();
			point.x = x + am_duong * 15;
			point.y = y;
			point.vx = am_duong * (5 + CRes.random(2));
			point.vy = CRes.random_Am_0(2);
			point.fRe = 6 + CRes.random(3);
			point.dis = ((CRes.random(3) != 0) ? 1 : 0);
			VecEff.addElement(point);
		}
	}

	public void createMorgan_2()
	{
		int num = 20;
		if (Dir == 2)
		{
			num = -20;
		}
		GameScreen.addEffectEnd(30, 0, x + num, y, 300, Dir, objMainEff);
		fRemove = 8;
		addSound(7);
	}

	public void createCabaji_1()
	{
		fRemove = 5;
		fraImgEff = new FrameImage(186, 19, 22);
		fraImgSubEff = new FrameImage(187, 20, 20);
		fraImgSub2Eff = new FrameImage(120, 50, 25);
		vMax = 14;
		int num = -14;
		if (Dir == 2)
		{
			num = 14;
		}
		x += num;
	}

	public void createBuggy_2()
	{
		fraImgEff = new FrameImage(125, 60, 44, 60, 44);
		fraImgSubEff = new FrameImage(126, 45, 45);
		fraImgSub2Eff = new FrameImage(3, 30, 50);
		fraImgSub3Eff = new FrameImage(128, 16, 16);
		vMax = 24;
		int num = -14;
		if (Dir == 2)
		{
			num = 14;
		}
		x1000 = x + num;
		y1000 = y + 14;
		fRemove = 49;
	}

	public void createBuggy_1()
	{
		fRemove = 5;
		int num = -25;
		if (Dir == 2)
		{
			num = 25;
		}
		x += num;
		fraImgEff = new FrameImage(124, 27, 22);
		vMax = 10;
	}

	public void createMohji_2()
	{
		fRemove = 8;
		fraImgEff = new FrameImage(120, 50, 25);
		int num = -25;
		if (Dir == 2)
		{
			num = 25;
		}
		x += num;
		y += 10;
	}

	public void createKuro_1()
	{
		fraImgEff = new FrameImage(45, 80, 25);
		fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
		fRemove = 18;
		objFireMain.isTanHinh = true;
		toY = objBeFireMain.y;
		y = objFireMain.y;
	}

	public void createJango_1()
	{
		fRemove = 5;
		fraImgEff = new FrameImage(131, 20, 10);
		fraImgSubEff = new FrameImage(27, 12, 12);
		fraImgSub2Eff = new FrameImage(120, 50, 25);
		vMax = 14;
		int num = -14;
		if (Dir == 2)
		{
			num = 14;
		}
		x += num;
	}

	public void createNyaban_3()
	{
		fraImgEff = new FrameImage(120, 50, 25);
		fRemove = 27;
		x1000 = -15;
		if (Dir == 2)
		{
			x1000 = 15;
		}
		vx = (toX - (x + x1000)) / 5;
	}

	public void createNyaban_2()
	{
		fraImgEff = new FrameImage(130, 48, 39);
		fRemove = 12;
		vx = (toX - x) / 5;
	}

	public void createNyaban_1()
	{
		fRemove = 10;
		fraImgEff = new FrameImage(120, 50, 25);
		int num = -14;
		if (Dir == 2)
		{
			num = 14;
		}
		x += num;
	}

	public void createCabaji_2()
	{
		fraImgEff = new FrameImage(129, 40, 80);
		fraImgSubEff = new FrameImage(76, 32, 70);
		toY = objBeFireMain.y;
		fRemove = 15;
	}

	public void createKurobi_1()
	{
		fRemove = 32;
		fraImgEff = new FrameImage(144, 37, 55);
		x1000 = -30;
		if (Dir == 2)
		{
			x1000 = 30;
		}
		y -= 5;
		GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
	}

	public void createChu_2()
	{
		vMax = 14;
		fraImgEff = new FrameImage(20, 10, 10);
		fRemove = 40;
		y -= 5;
		int num = 10;
		if (Dir == 2)
		{
			num = -10;
		}
		x += num;
		GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
	}

	public void createChu_1()
	{
		vMax = 14;
		fraImgEff = new FrameImage(20, 10, 10);
		fRemove = 20;
		y -= 5;
		int num = 10;
		if (Dir == 2)
		{
			num = -10;
		}
		x += num;
	}

	public void createHachi_2()
	{
		vMax = 14;
		fraImgEff = new FrameImage(81, 24, 24);
		if (typeEffect == 150)
		{
			fraImgEff = new FrameImage(83, 14, 14);
		}
		else if (typeEffect == 151)
		{
			fraImgEff = new FrameImage(80, 30, 15);
			frame = 0;
		}
		else if (typeEffect == 152)
		{
			fraImgEff = new FrameImage(80, 30, 15);
			frame = 1;
		}
		else if (typeEffect == 153)
		{
			fraImgEff = new FrameImage(80, 30, 15);
			frame = 2;
		}
		fRemove = 24;
		y -= 10;
		int num = 10;
		if (Dir == 2)
		{
			num = -10;
		}
		if (typeEffect == 113)
		{
			GameScreen.addEffectEnd(30, 0, x, y, 600, Dir, objMainEff);
		}
		else
		{
			fRemove = 8;
			vMax = 16;
		}
		x += num;
		addSound(32);
	}

	public void createDonKrieg_3()
	{
		fRemove = 30;
		fraImgEff = new FrameImage(137, 75, 65);
		plusxy = new int[2][];
		plusxy[0] = new int[2];
		plusxy[1] = new int[2];
		plusxy[0][0] = 0;
		plusxy[0][1] = -37;
		plusxy[1][0] = -28;
		plusxy[1][1] = -28;
		int num = 25;
		if (Dir == 2)
		{
			plusxy[1][0] = 28;
			num = -25;
		}
		GameScreen.addEffectEnd(30, 0, x + num, y, 300, Dir, objMainEff);
	}

	public void createDonKrieg_1()
	{
		fraImgEff = new FrameImage(134, 30, 42);
		fraImgSubEff = new FrameImage(135, 20, 20);
		vMax = 12;
		int num = 10;
		x1000 = 15;
		xplus = -10;
		if (Dir == 0)
		{
			num = -10;
			x1000 = -15;
			xplus = 10;
		}
		x += num;
		y -= 5;
		fRemove = 22;
	}

	public void createDonKrieg_2()
	{
		fraImgEff = new FrameImage(134, 30, 42);
		fraImgSubEff = new FrameImage(136, 16, 12);
		fraImgSub2Eff = new FrameImage(131, 20, 10);
		vMax = 8;
		int num = 10;
		x1000 = 15;
		xplus = -10;
		if (Dir == 0)
		{
			num = -10;
			x1000 = -15;
			xplus = 10;
		}
		x += num;
		y -= 5;
		fRemove = 22;
		xArchor = x;
		yArchor = y;
	}

	public void createGhin_2()
	{
		objFireMain.isPaintWeapon = false;
		fRemove = 30;
		fraImgEff = new FrameImage(133, 36, 44);
		int num = 3;
		vx = -8;
		if (Dir == 2)
		{
			num = -3;
			vx = 8;
		}
		Point point = new Point(x - 15, y + num);
		point.frame = 0;
		point.dis = 4;
		VecEff.addElement(point);
		Point point2 = new Point(x + 15, y - num);
		point2.frame = 1;
		point2.dis = 4;
		VecEff.addElement(point2);
	}

	public void createGhin_1()
	{
		fraImgEff = new FrameImage(132, 60, 35);
		int num = 25;
		int num2 = 10;
		if (Dir == 0)
		{
			num = -25;
		}
		if (typeEffect == 65 || typeEffect == 70)
		{
			fraImgEff = new FrameImage(215, 60, 35);
			if (typeEffect == 70)
			{
				vMax = 16;
				fraImgSubEff = new FrameImage(216, 18, 18);
			}
			num = 28;
			if (Dir == 0)
			{
				num = -28;
			}
			num2 = 13;
			levelPaint = -1;
		}
		fRemove = 6;
		x += num;
		y += num2;
	}

	public void createPearl_2()
	{
		fRemove = 34;
		vMax = 12;
		fraImgEff = new FrameImage(78, 22, 28);
		fraImgSubEff = new FrameImage(20, 10, 10);
		int num = 10;
		Point point = new Point(x - 18, y - num);
		point.frame = CRes.random(3);
		VecEff.addElement(point);
		Point point2 = new Point(x + 18, y - num);
		point2.frame = CRes.random(3);
		VecEff.addElement(point2);
	}

	public void createPearl_1()
	{
		fRemove = 10;
		int num = 15;
		if (Dir == 0)
		{
			num = -15;
		}
		GameScreen.addEffectEnd(30, 0, x - num, y, 300, Dir, objMainEff);
		x += num;
	}

	public void createKuro_2()
	{
		fraImgEff = new FrameImage(45, 80, 25);
		fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
		fRemove = 38;
		y = objFireMain.y;
		x1000 = x;
		y1000 = y;
	}

	public void createArlong_3()
	{
		fraImgEff = new FrameImage(148, 104, 85);
		fraImgSubEff = new FrameImage(149, 73, 73);
		fraImgSub2Eff = new FrameImage(150, 66, 70, 42, 45);
		objFireMain.isTanHinh = false;
		plusxy = new int[5][];
		plusxy[0] = new int[2];
		plusxy[1] = new int[2];
		plusxy[2] = new int[2];
		plusxy[3] = new int[2];
		plusxy[4] = new int[2];
		plusxy[0][0] = -15;
		plusxy[0][1] = -30;
		plusxy[1][0] = -30;
		plusxy[1][1] = 10;
		plusxy[2][0] = 38;
		plusxy[2][1] = -30;
		plusxy[3][0] = -30;
		plusxy[3][1] = -20;
		plusxy[4][0] = -20;
		plusxy[4][1] = 20;
		if (Dir == 2)
		{
			for (int i = 0; i < plusxy.Length; i++)
			{
				plusxy[i][0] = -plusxy[i][0];
			}
		}
		GameScreen.addEffectEnd(30, 0, x + plusxy[2][0], y + plusxy[2][1], 350, Dir, objMainEff);
		fRemove = 20;
	}

	public void createArlong_2()
	{
		fraImgEff = new FrameImage(146, 96, 24);
		fraImgSubEff = new FrameImage(147, 48, 12);
		fraImgSub2Eff = new FrameImage(256, 80, 40);
		objFireMain.isTanHinh = false;
		fRemove = 40;
		vMax = 30;
	}

	public void createArlong_1()
	{
		fraImgEff = new FrameImage(145, 80, 80, 60, 60);
		fRemove = 12;
		objFireMain.isTanHinh = false;
		if (vecObjsBeFire.size() > 1)
		{
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill == null)
				{
					continue;
				}
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					Point point = new Point(mainObject.x, mainObject.y - mainObject.hOne / 2);
					if (x < point.x)
					{
						point.dis = 2;
					}
					else
					{
						point.dis = 0;
					}
					VecEff.addElement(point);
				}
			}
		}
		else
		{
			int num = -15;
			if (Dir == 2)
			{
				num = 15;
			}
			x += num;
			y -= 10;
		}
	}

	public void createKurobi_2()
	{
		fraImgEff = new FrameImage(144, 37, 55);
		fRemove = 30;
		x1000 = -25;
		y1000 = -25;
		if (Dir == 2)
		{
			x1000 = 25;
		}
		vx = (toX - (x + x1000)) / 5;
		GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
	}

	public void createUrgot3()
	{
		fRemove = 40;
		fraImgEff = new FrameImage(179, 54, 25);
		for (int i = 0; i < 5; i++)
		{
			Point point = new Point();
			point.y = -CRes.random(30);
			point.vy = CRes.random_Am(3, 8);
			point.frame = CRes.random(3);
			VecEff.addElement(point);
		}
	}

	public void createXerath3()
	{
		xplus = 4;
		yplus = 6;
		int num = 0;
		for (int i = 1; i <= yplus; i++)
		{
			num -= i * xplus;
		}
		fraImgEff = new FrameImage(83, 14, 14);
		fraImgSubEff = new FrameImage(51, 9, 9);
		fraImgSub2Eff = new FrameImage(52, 5, 5);
		x = objFireMain.x;
		y = objFireMain.y - objFireMain.hOne / 2;
		x1000 = x * 1000;
		y1000 = y;
		int num2 = num - (toY - y);
		int num3 = yplus - 1;
		if (num2 < 0)
		{
			for (int j = 1; j < 60; j++)
			{
				num2 += j * xplus;
				if (num2 >= 0)
				{
					num3 += j;
					break;
				}
			}
		}
		vy1000 = -(xplus * yplus);
		vx1000 = (toX - x) * 1000 / num3;
		fRemove = num3;
	}

	public void createZoroS3_L2_New()
	{
		fraImgEff = new FrameImage(165, 27, 50);
		fraImgSubEff = new FrameImage(167, 78, 22);
		fraImgSub2Eff = new FrameImage(166, 50, 60);
		fRemove = 36;
		vMax = 12;
		if (isAddSound)
		{
			mSound.playSound(8, mSound.volumeSound);
		}
		GameScreen.addEffectEnd(30, 0, x, y, 500, Dir, objMainEff);
		int num = -15;
		x1000 = x + 15;
		y1000 = objFireMain.y - 22;
		if (Dir == 2)
		{
			num = 15;
			x1000 = x - 63;
		}
		x += num;
		y -= 5;
	}

	public void createZoroS3_L1_New()
	{
		fraImgEff = new FrameImage(165, 27, 50);
		fRemove = 30;
		if (isAddSound)
		{
			mSound.playSound(8, mSound.volumeSound);
		}
		GameScreen.addEffectEnd(30, 0, x, y, 400, Dir, objMainEff);
		int num = -15;
		if (Dir == 2)
		{
			num = 15;
		}
		x += num;
		y -= 5;
	}

	public void createMonster_NEM_BOOM_2()
	{
		fraImgEff = new FrameImage(188, 9, 16);
		vMax = 12;
		y = objFireMain.y - objBeFireMain.hOne / 2;
		if (Dir == 0)
		{
			x -= 15;
		}
		else
		{
			x += 15;
		}
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
	}

	public void createUssopS2_Le_New()
	{
		vMax = 12;
		fRemove = 34;
		GameScreen.addEffectEnd(30, 0, x + am_duong * 25, y - 5, 400, Dir, objMainEff);
		fraImgEff = new FrameImage(185, 55, 25);
		if (typeEffect == 193)
		{
			fraImgSubEff = new FrameImage(285, 111, 90);
			mframe = new int[4] { 0, 1, 2, 1 };
		}
		else if (typeEffect == 225)
		{
			fRemove = 40;
			fraImgEff = new FrameImage(333, 55, 25);
			fraImgSubEff = new FrameImage(332, 111, 90);
			mframe = new int[4] { 0, 1, 2, 1 };
		}
		else if (typeEffect == 302)
		{
			fraImgEff = new FrameImage(419, 2);
			fraImgSubEff = new FrameImage(404, 3);
			mframe = new int[4] { 0, 1, 2, 1 };
			fraImgSub3Eff = new FrameImage(405, 3);
			int num = x - 50 * am_duong;
			int xdich = num - x;
			int ydich = 0;
			VecEff.addElement(create_Speed(xdich, ydich, new Point_Focus(), x, y, num, y));
		}
		else
		{
			fraImgSubEff = new FrameImage(184, 111, 70, 79, 50);
			mframe = new int[2] { 0, 1 };
		}
		fraImgSub2Eff = new FrameImage(251, 52, 21);
	}

	public void createUssopSkill1_Lv3_New()
	{
		fraImgEff = new FrameImage(53, 9, 9);
		fraImgSubEff = new FrameImage(183, 20, 54);
		vMax = 24;
		fRemove = 25;
		y -= 6;
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		int xdich = toX - x;
		int ydich = toY - y;
		Point_Focus p = new Point_Focus();
		p = create_Speed(xdich, ydich, p);
		p.frame = 1;
		GameScreen.addEffectEnd(1, 0, x, y, Dir, objMainEff);
		VecEff.addElement(p);
		xArchor = objFireMain.x;
		yArchor = objFireMain.y;
	}

	public void createUssopSkill1_Lv3_SHORT()
	{
		fraImgEff = new FrameImage(53, 9, 9);
		fraImgSubEff = new FrameImage(183, 20, 54);
		vMax = 24;
		fRemove = 16;
		y -= 6;
		if (Dir == 0)
		{
			x -= 30;
		}
		else
		{
			x += 30;
		}
		int xdich = toX - x;
		int ydich = toY - y;
		Point_Focus p = new Point_Focus();
		p = create_Speed(xdich, ydich, p);
		p.frame = 1;
		GameScreen.addEffectEnd(1, 0, x, y, Dir, objMainEff);
		VecEff.addElement(p);
		xArchor = objFireMain.x;
		yArchor = objFireMain.y;
	}

	public void createSanji_s2_l3_New()
	{
		y = objFireMain.y;
		fraImgEff = new FrameImage(183, 20, 54);
		fRemove = 44;
		GameScreen.addEffectEnd(30, 0, x, y - objFireMain.hOne / 2, 300, Dir, objMainEff);
	}

	public void createSanji_s2_l3_New_SHORT()
	{
		y = objFireMain.y;
		fraImgEff = new FrameImage(183, 20, 54);
		fRemove = 24;
	}

	public void createSanji_s1_l3_New()
	{
		fraImgEff = new FrameImage(183, 20, 54);
		if (typeEffect == 177)
		{
			fraImgEff = new FrameImage(265, 20, 54);
		}
		fRemove = 50;
		GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
	}

	public void createSanji_s1_l3_SHORT()
	{
		fraImgEff = new FrameImage(183, 20, 54);
		fRemove = 16;
	}

	public bool checkNullObject(int type)
	{
		if (type == 1 && (objFireMain == null || objFireMain.returnAction()))
		{
			return true;
		}
		if (type == 2 && (objBeFireMain == null || objBeFireMain.returnAction()))
		{
			return true;
		}
		if (type == 3 && (objFireMain == null || objFireMain.returnAction() || objBeFireMain == null || objBeFireMain.returnAction()))
		{
			return true;
		}
		return false;
	}

	public Point createPointCausu1(Point p)
	{
		p.x = x + am_duong * (5 + CRes.random_Am_0(20));
		p.y = y - 10 + CRes.random_Am_0(25);
		p.vx = am_duong * CRes.random(7, 18);
		p.fRe = CRes.random(2, 5);
		p.frame = CRes.random(fraImgEff.nFrame);
		p.dis = Dir;
		return p;
	}

	public void create_Sanji_Sea_Lv3()
	{
		fraImgEff = new FrameImage(183, 20, 54);
		fraImgSubEff = new FrameImage(8, 40, 47, 40, 47);
		fRemove = 40;
		y = objFireMain.y;
	}

	public void create_Devil_FIRE1()
	{
		addSoundBuff();
		if (typeEffect == 259 || typeEffect == 260 || typeEffect == 261)
		{
			if (typeEffect == 259)
			{
				frameSuper = 1;
			}
			else if (typeEffect == 260)
			{
				frameSuper = 2;
			}
			else if (typeEffect == 261)
			{
				frameSuper = 3;
			}
			fraImgSubEff = new FrameImage(336, 74, 30, (sbyte)3, frameSuper);
			fraImgSub2Eff = new FrameImage(78, 22, 28, (sbyte)5, frameSuper);
			fraImgEff = new FrameImage(7, 34, 64, (sbyte)2, frameSuper);
		}
		else
		{
			fraImgEff = new FrameImage(7, 34, 64, 2);
			if (typeEffect == 228)
			{
				fraImgSubEff = new FrameImage(336, 74, 30, 3);
				fraImgSub2Eff = new FrameImage(78, 22, 28, 5);
			}
		}
		fRemove = 30;
		GameScreen.addEffectEnd(30, 0, x, y, 250, Dir, objMainEff);
		toY = objBeFireMain.y;
	}

	public void create_Devil_FIRE2()
	{
		addSoundBuff();
		frameSuper = 0;
		if (typeEffect == 262)
		{
			frameSuper = 1;
		}
		else if (typeEffect == 263)
		{
			frameSuper = 2;
		}
		else if (typeEffect == 264)
		{
			frameSuper = 3;
		}
		fraImgEff = new FrameImage(32, 45, 45, (sbyte)5, frameSuper);
		fraImgSubEff = new FrameImage(78, 22, 28, (sbyte)5, frameSuper);
		fraImgSub2Eff = new FrameImage(224, 22, 28, (sbyte)5, frameSuper);
		fraImgSub3Eff = new FrameImage(38, 50, 80, (sbyte)3, frameSuper);
		fRemove = 30;
		vMax = 12;
		y = objFireMain.y;
		GameScreen.addEffectEnd(30, 0, x, y - objFireMain.hOne / 2, 1200, Dir, objMainEff);
	}

	public void create_ho_den_vu_tru()
	{
		Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(0);
		if (object_Effect_Skill != null)
		{
			MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
			if (mainObject != null)
			{
				x = mainObject.x;
				y = mainObject.y;
			}
		}
		for (int i = 0; i < radian.Length; i++)
		{
			GameScreen.addEffectEnd(166, 0, 2 * (CRes.getcos(radian[i]) * CR) / 1024 + x, CRes.getsin(radian[i]) * CR / 1024 + y, Dir, objMainEff);
		}
	}

	public void create_Devil_ICE1()
	{
		addSoundBuff();
		fraImgEff = new FrameImage(37, 31, 74);
		fraImgSub2Eff = new FrameImage(40, 63, 20);
		fraImgSub3Eff = new FrameImage(41, 40, 40);
		y = objFireMain.y;
		GameScreen.addEffectEnd(30, 0, x, y - objFireMain.hOne / 2, 1200, Dir, objMainEff);
		mframe = new int[41]
		{
			-1, -1, -1, -1, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 2, 2, 2, 2, 1, 1, 1, 0, 0,
			0
		};
		fRemove = 30;
	}

	public void create_Devil_ICE2()
	{
		addSoundBuff();
		fraImgEff = new FrameImage(5, 80, 50);
		fraImgSub3Eff = new FrameImage(41, 40, 40);
		fraImgSubEff = new FrameImage(43, 84, 110);
		plusxy = new int[3][];
		plusxy[0] = new int[2];
		plusxy[1] = new int[2];
		plusxy[2] = new int[2];
		plusxy[0][0] = -40;
		plusxy[0][1] = -35 + objFireMain.lechYHead;
		plusxy[1][0] = 20;
		plusxy[1][1] = -67;
		plusxy[2][0] = 47;
		plusxy[2][1] = -50 + objFireMain.lechYHead;
		fRemove = 30;
		vMax = 10;
		y = objFireMain.y;
		GameScreen.addEffectEnd(30, 0, x, y - objFireMain.hOne / 2, 1200, Dir, objMainEff);
	}

	public void create_Devil_Smoker1()
	{
		addSoundBuff();
		fraImgEff = new FrameImage(58, 40, 27);
		fraImgSubEff = new FrameImage(57, 42, 50, 32, 38);
		fraImgSub2Eff = new FrameImage(61, 24, 30);
		if (typeEffect == 232)
		{
			fraImgSub3Eff = new FrameImage(85, 34, 34, 28, 28);
		}
		fRemove = 30;
		vMax = 12;
	}

	public void create_Devil_Smoker2()
	{
		addSoundBuff();
		fraImgEff = new FrameImage(64, 50, 45);
		fraImgSubEff = new FrameImage(63, 71, 60, 50, 40);
		fraImgSub2Eff = new FrameImage(65, 59, 65);
		fraImgSub3Eff = new FrameImage(61, 24, 30);
		if (typeEffect == 234)
		{
			fraImgSub4Eff = new FrameImage(85, 34, 34, 28, 28);
		}
		fRemove = 30;
		vMax = 26;
	}

	public void createSmoker1()
	{
		fraImgEff = new FrameImage(64, 50, 45);
		fraImgSubEff = new FrameImage(63, 71, 60, 51, 43);
		fraImgSub2Eff = new FrameImage(86, 32, 79);
		fraImgSub3Eff = new FrameImage(61, 24, 30);
		GameScreen.addEffectEnd(30, 0, x, y, 100, Dir, objMainEff);
		fRemove = 20;
		vMax = 26;
	}

	public void createSmoker2()
	{
		fraImgEff = new FrameImage(86, 32, 79);
		fraImgSubEff = new FrameImage(87, 35, 35, 28, 28);
		GameScreen.addEffectEnd(30, 0, x, y, 100, Dir, objMainEff);
		frame = 5;
		if (Dir == 2)
		{
			frame = 6;
		}
		fRemove = 20;
		vMax = 14;
	}

	public void createZoro_S2_L1_New()
	{
		fRemove = 6;
	}

	private void createMissGold_1()
	{
		fraImgEff = new FrameImage(212, 33, 24);
		fRemove = 24;
	}

	private void createMr3_2()
	{
		fraImgEff = new FrameImage(211, 35, 22);
		fraImgSubEff = new FrameImage(32, 45, 45, 34, 34);
		fraImgSub2Eff = new FrameImage(160, 9, 14);
		fRemove = 20;
		vMax = 16;
	}

	private void createMr3_1()
	{
		fraImgEff = new FrameImage(211, 35, 22);
		GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
		fRemove = 20;
		vMax = 16;
	}

	private void create_Wapol4()
	{
		fraImgEff = new FrameImage(20, 10, 10);
		if (Dir == 0)
		{
			x -= 25;
			vx = -48;
		}
		else
		{
			x += 25;
			vx = 48;
		}
		y += 7;
		x1000 = x;
		fRemove = 20;
	}

	private void createWapol3()
	{
		fraImgEff = new FrameImage(20, 10, 10);
		if (Dir == 0)
		{
			x -= 5;
		}
		else
		{
			x += 5;
		}
		y += 7;
		fRemove = 25;
		vMax = 14;
	}

	private void createWapol2()
	{
		fraImgEff = new FrameImage(209, 32, 46);
		if (Dir == 0)
		{
			x -= 10;
		}
		else
		{
			x += 10;
		}
		y -= 5;
		numNextFrame = 2;
		vy = -3;
		fRemove = 4;
	}

	private void createWapol()
	{
		levelPaint = -1;
		fraImgEff = new FrameImage(208, 50, 57);
		fraImgSubEff = new FrameImage(144, 37, 55);
		vMax = 14;
		y = objFireMain.y;
		toY = objBeFireMain.y;
		if (objFireMain.plashNow != null)
		{
			objFireMain.plashNow.setIsNextf(1);
		}
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
	}

	private void createKuromarimo()
	{
		fraImgEff = new FrameImage(207, 14, 14);
		vMax = 14;
		if (Dir == 0)
		{
			x -= 5;
		}
		else
		{
			x += 5;
		}
		y -= 20;
		if (CRes.random(2) == 0)
		{
			subType = 0;
			toX += 6;
		}
		else
		{
			subType = 1;
			toX -= 6;
		}
		if (!checkNullObject(2))
		{
			toY = objBeFireMain.y - objBeFireMain.hOne / 2;
		}
		toY += 14;
		int xdich = toX - x;
		int ydich = toY - y;
		create_Speed(xdich, ydich, null);
	}

	private void createChess()
	{
		fraImgEff = new FrameImage(205, 20, 20);
		fraImgSubEff = new FrameImage(206, 20, 20);
		vMax = 18;
		if (Dir == 0)
		{
			x -= 10;
		}
		else
		{
			x += 10;
		}
		y -= 10;
		int num = toX - x;
		int num2 = toY - y;
		int frameAngle = CRes.angle(num, num2);
		create_Speed(num, num2, null);
		frame = setFrameAngle(frameAngle);
	}

	private void createZoroLv3()
	{
		Dir = (sbyte)objFireMain.type_left_right;
		if (typeEffect == 185 || typeEffect == 217)
		{
			fraImgEff = new FrameImage(280, 50, 74, 2);
		}
		else
		{
			fraImgEff = new FrameImage(165, 27, 50);
		}
		fraImgSubEff = new FrameImage(167, 78, 22);
		fraImgSub2Eff = new FrameImage(16, 55, 55);
		fraImgSub3Eff = new FrameImage(17, 55, 55);
		fraImgSub4Eff = new FrameImage(8, 40, 47, 40, 47);
		fRemove = 30;
		vMax = 12;
		if (typeEffect == 283)
		{
			fraImgEff = new FrameImage(421, 50, 74, 2);
			fraImgSub2Eff = new FrameImage(409, 4);
		}
		int num = -15;
		xArchor = objFireMain.x;
		yArchor = objFireMain.y;
		x1000 = x - 5;
		y1000 = objFireMain.y - 22;
		objFireMain.dy = 0;
		if (Dir == 2)
		{
			num = 15;
			x1000 = x - 73;
		}
		x += num;
		y -= 5;
	}

	private void createZoro_New2_SHORT()
	{
		fRemove = 24;
		vMax = 12;
		fraImgEff = new FrameImage(8, 40, 47, 40, 47);
	}

	private void beginUpdate()
	{
	}

	public void updateLuffy1()
	{
		if (objBeFireMain != null && f % 3 == 0 && indexObjBefire < vecObjsBeFire.size())
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
			indexObjBefire++;
			if (object_Effect_Skill != null)
			{
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					sbyte dir = 0;
					if (objFireMain.x < mainObject.x)
					{
						dir = 2;
					}
					int num = 12;
					if (Dir == 0)
					{
						num = -12;
					}
					sbyte b = 0;
					if (typeEffect == 37)
					{
						b = 2;
					}
					GameScreen.addEffectEnd_ObjTo(13, b, objFireMain.x + num, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, mainObject.ID, mainObject.typeObject, dir, objMainEff);
				}
			}
		}
		if (f >= fRemove)
		{
			if (VecEff.size() == 0)
			{
				removeEff();
			}
		}
		else if (typeEffect == 37 && f % 2 == 0)
		{
			Point o = new Point(x + CRes.random_Am_0(15), y + CRes.random_Am_0(20));
			VecEff.addElement(o);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= 3)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
	}

	public void updateSanji1()
	{
		if (f == 1)
		{
			if (isAddSound)
			{
				mSound.playSound(16, mSound.volumeSound);
			}
			int num = 15;
			if (Dir == 0)
			{
				num = -15;
			}
			GameScreen.addEffectEnd(30, 0, x + num, y - objFireMain.hOne / 2, 300, Dir, objMainEff);
		}
		if (f == 8 && objFireMain != null)
		{
			int num2 = 27;
			if (Dir == 0)
			{
				num2 = -27;
			}
			if (typeEffect == 47 || typeEffect == 48)
			{
				sbyte b = 0;
				if (typeEffect == 48)
				{
					b = 1;
				}
				if (!checkNullObject(2))
				{
					GameScreen.addEffectEnd_ObjTo(37, b, x + num2, y - objFireMain.hOne / 2, objBeFireMain.ID, objBeFireMain.typeObject, Dir, objMainEff);
				}
			}
		}
		if (f >= fRemove)
		{
			objFireMain.dx = 0;
			removeEff();
		}
	}

	public void updateZoro1()
	{
		if (!checkNullObject(1))
		{
			objFireMain.isTanHinh = true;
			objFireMain.Action = 2;
			objFireMain.vx = vx;
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
				objFireMain.Action = 0;
			}
			GameScreen.addEffectEnd(86, 0, x + ((Dir != 0) ? (-20) : 20), y, Dir, objMainEff);
			GameScreen.addEffectEnd(9, 0, toX, toY + 25, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateZoro2()
	{
		if (f < 2)
		{
			xplus = -3 + f * 3;
		}
		if (f == 4)
		{
			if (!checkNullObject(1) && objFireMain == GameScreen.player)
			{
				sendMove(x, toX, y, toY + fraImgEff.frameHeight / 2);
			}
			xplus = 0;
			x = toX;
			y = toY;
		}
		if (f > 5)
		{
			xplus = 3 - (f - 5) * 3;
		}
		if (f < fRemove)
		{
			return;
		}
		if (!checkNullObject(1))
		{
			objFireMain.isTanHinh = false;
			if (objFireMain.plashNow != null)
			{
				objFireMain.plashNow.setIsNextf(0);
			}
		}
		if (!checkNullObject(2))
		{
			GameScreen.addEffectEnd(9, 0, objBeFireMain.x, objBeFireMain.y + 25, Dir, objMainEff);
		}
		GameScreen.addEffectEnd(86, 0, x + ((Dir != 0) ? 10 : (-10)), y - 25, Dir, objMainEff);
		removeEff();
	}

	public void updateUssopSea1()
	{
		if ((f == 8 || f == 12) && isAddSound)
		{
			mSound.playSound(25, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				VecEff.removeElement(point_Focus);
				GameScreen.addEffectEnd(1, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				i--;
			}
		}
		if (f == 10 || f == 13 || f == 15)
		{
			if (!checkNullObject(2))
			{
				toX = objBeFireMain.x;
				toY = objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(8);
			}
			setAngle();
			if (!checkNullObject(1))
			{
				objFireMain.Dir = Dir;
			}
			int num = toX - x;
			int num2 = toY - y;
			int frameAngle = CRes.angle(num, num2);
			Point_Focus p = new Point_Focus();
			p = create_Speed(num, num2, p);
			p.frame = setFrameAngle(frameAngle);
			VecEff.addElement(p);
			GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
			GameScreen.addEffectEnd(93, 2, x, y, Dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateUssopSea2()
	{
		if ((f == 8 || f == 12) && isAddSound)
		{
			mSound.playSound(25, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(81, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				GameScreen.addEffectEnd(1, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 4 || f == 9 || f == 14 || f == 19)
		{
			if (!checkNullObject(3))
			{
				toX = objBeFireMain.x;
				toY = objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(8);
				y = objFireMain.y - objFireMain.hOne / 2 - 6;
				if (Dir == 0)
				{
					x = objFireMain.x - 22;
				}
				else
				{
					x = objFireMain.x + 22;
				}
			}
			GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
			setAngle();
			if (!checkNullObject(1))
			{
				objFireMain.Dir = Dir;
			}
			int num = 1;
			if (f == 9 || f == 19)
			{
				num = 2;
			}
			for (int j = 0; j < num; j++)
			{
				if (j == 1)
				{
					y -= 10;
				}
				int num2 = toX - x;
				int num3 = toY - y;
				int frameAngle = CRes.angle(num2, num3);
				Point_Focus p = new Point_Focus();
				p = create_Speed(num2, num3, p);
				p.frame = setFrameAngle(frameAngle);
				VecEff.addElement(p);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateUssopSea3()
	{
		if ((f == 4 || f == 8 || f == 12 || f == 16) && isAddSound)
		{
			mSound.playSound(25, mSound.volumeSound);
			if (f == 8 || f == 16)
			{
				mSound.playSound(15, mSound.volumeSound);
			}
		}
		if (f == 10 && !checkNullObject(2))
		{
			GameScreen.addEffectEnd(108, 1, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f > 4 && f % 3 == 0 && f <= 19)
		{
			GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
			int num = CRes.random(6, 9);
			for (int j = 0; j < num; j++)
			{
				Point_Focus point_Focus2 = new Point_Focus();
				point_Focus2.x = x * 10;
				point_Focus2.y = y * 10;
				point_Focus2.vx = vMax * 10 * am_duong + CRes.random_Am_0(7);
				point_Focus2.vy = -(num * 13) / 2 + 13 * j;
				point_Focus2.frame = 0;
				point_Focus2.fRe = 16;
				point_Focus2.dis = Dir;
				VecEff.addElement(point_Focus2);
			}
			addVir(5, 5, 10, isPlayer: true);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateUssop2()
	{
		if (f == 3)
		{
			GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
		}
		int a = 12;
		if ((f == 0 || f == 3) && isAddSound)
		{
			mSound.playSound(21, mSound.volumeSound);
		}
		if (f == fRemove - 2)
		{
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
			GameScreen.addEffectEnd(93, 1, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
			GameScreen.addEffectEnd(93, 1, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
			removeEff();
		}
	}

	public void updateUssop_Skill2()
	{
		if (f == 3 && isAddSound)
		{
			mSound.playSound(20, mSound.volumeSound);
		}
		if (f == 1)
		{
			GameScreen.addEffectEnd(5, 0, x, y, Dir, objMainEff);
		}
		if (f < fRemove)
		{
			return;
		}
		int a = 12;
		GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
		if (typeEffect == 64)
		{
			if (isAddSound)
			{
				mSound.playSound(19, mSound.volumeSound);
			}
			GameScreen.addEffectEnd(12, 1, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
		}
		else if (typeEffect == 66)
		{
			if (isAddSound)
			{
				mSound.playSound(14, mSound.volumeSound);
			}
			setAva(1, objBeFireMain);
			GameScreen.addEffectEnd(4, 2, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
		}
		GameScreen.addEffectEnd(93, 2, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
		removeEff();
	}

	public void updateNami1()
	{
		if (f <= 1)
		{
			return;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f < point_Focus.fRe)
			{
				continue;
			}
			if (isAddSound)
			{
				mSound.playSound(19, mSound.volumeSound);
			}
			setAva(1, objBeFireMain);
			sbyte subtype = 0;
			if (typeEffect == 9)
			{
				GameScreen.addEffectEnd(3, 0, toX, toY, Dir, objMainEff);
			}
			else if (typeEffect == 53)
			{
				GameScreen.addEffectEnd(38, 1, toX, toY, Dir, objMainEff);
				subtype = 1;
			}
			else if (typeEffect == 163)
			{
				if (isAddSound)
				{
					mSound.playSound(17, mSound.volumeSound);
				}
				addVir(5, 5, 10, isPlayer: true);
				GameScreen.addEffectEnd(42, 0, toX, toY, Dir, objMainEff);
				subtype = 1;
			}
			GameScreen.addEffectEnd(6, subtype, toX, toY, Dir, objMainEff);
			GameScreen.addEffectEnd(93, 1, toX, toY, Dir, objMainEff);
			VecEff.removeElement(point_Focus);
			i--;
		}
		if (VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateNami1_SHORT()
	{
		if (f == 12 || f == 22)
		{
			y += 5;
			int num = 0;
			int xdich = toX - x;
			int ydich = toY - num - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			if (f == 22)
			{
				p.frame = 1;
			}
			VecEff.addElement(p);
			if (isAddSound)
			{
				mSound.playSound(18, mSound.volumeSound);
			}
		}
		if (typeEffect == 222 || typeEffect == 312)
		{
			for (int i = 0; i < VecSubEff.size(); i++)
			{
				Point obj = (Point)VecSubEff.elementAt(i);
				obj.f++;
				if (obj.f / 2 >= fraImgSub3Eff.nFrame)
				{
					VecSubEff.removeElementAt(i);
					i--;
				}
			}
		}
		if (f > 1)
		{
			for (int j = 0; j < VecEff.size(); j++)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
				if ((typeEffect == 222 || typeEffect == 312) && !GameCanvas.lowGraphic)
				{
					Point o = new Point(point_Focus.x, point_Focus.y);
					VecSubEff.addElement(o);
				}
				point_Focus.update_Vx_Vy();
				if (point_Focus.f <= point_Focus.fRe)
				{
					continue;
				}
				if (isAddSound)
				{
					mSound.playSound(19, mSound.volumeSound);
				}
				setAva(1, objBeFireMain);
				sbyte subtype = 1;
				if (typeEffect == 190)
				{
					subtype = 2;
				}
				else if (typeEffect == 222 || typeEffect == 312)
				{
					subtype = 3;
				}
				if (isAddSound)
				{
					mSound.playSound(17, mSound.volumeSound);
				}
				addVir(5, 5, 10, isPlayer: true);
				if (point_Focus.frame == 1)
				{
					int subtype2 = 2;
					if (typeEffect == 190 || typeEffect == 222 || typeEffect == 312)
					{
						subtype2 = 8;
						GameScreen.addEffectEnd(108, 3, toX, toY, Dir, objMainEff);
					}
					GameScreen.addEffectEnd(54, subtype2, toX, toY, Dir, objMainEff);
				}
				else if (!GameCanvas.lowGraphic)
				{
					if (typeEffect == 222)
					{
						GameScreen.addEffectEnd(139, 0, toX, toY, Dir, objMainEff);
					}
					if (typeEffect == 312)
					{
						GameScreen.addEffectEnd(139, 1, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
					}
				}
				GameScreen.addEffectEnd(42, 0, toX, toY, Dir, objMainEff);
				GameScreen.addEffectEnd(6, subtype, toX, toY, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 8, toX, toY, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				j--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateNamiSea1()
	{
		if (f == 4 && isAddSound)
		{
			mSound.playSound(18, mSound.volumeSound);
		}
		if (f == 10 && !checkNullObject(2))
		{
			int xdich = objBeFireMain.x - x;
			int ydich = objBeFireMain.y - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			VecEff.addElement(p);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f < fRemove || VecEff.size() != 0)
		{
			return;
		}
		if (!checkNullObject(2))
		{
			if (isAddSound)
			{
				mSound.playSound(19, mSound.volumeSound);
			}
			GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(93, 1, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
		}
		removeEff();
	}

	public void updateNamiSea2()
	{
		if (f == 8 && isAddSound)
		{
			mSound.playSound(18, mSound.volumeSound);
		}
		if (f >= 2 && f <= 16)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isPaintWeapon = false;
			}
		}
		else
		{
			objFireMain.isPaintWeapon = true;
		}
		if (f == 14 && !checkNullObject(2))
		{
			int xdich = objBeFireMain.x - x;
			int ydich = objBeFireMain.y - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			VecEff.addElement(p);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f < fRemove || VecEff.size() != 0)
		{
			return;
		}
		if (!checkNullObject(2))
		{
			if (isAddSound)
			{
				mSound.playSound(19, mSound.volumeSound);
			}
			GameScreen.addEffectEnd(41, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
		}
		removeEff();
	}

	public void updateNamiSea3()
	{
		if (f == 2 && isAddSound)
		{
			mSound.playSound(18, mSound.volumeSound);
		}
		if (f >= 2 && f <= 16)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isPaintWeapon = false;
			}
		}
		else
		{
			objFireMain.isPaintWeapon = true;
		}
		if (f >= 24 && f <= 34 && !checkNullObject(2) && CRes.random(4) != 0)
		{
			int num = CRes.random(1, 3);
			for (int i = 0; i < num; i++)
			{
				int num2 = CRes.random_Am(0, 25) + objBeFireMain.x;
				GameScreen.addEffectEnd(90, 1, num2, objBeFireMain.y - 10, Dir, objMainEff);
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				VecEff.removeElement(point_Focus);
				j--;
			}
		}
		if ((f == 10 || f == 16) && !checkNullObject(3))
		{
			int xdich = objBeFireMain.x - x;
			int ydich = objBeFireMain.y - 60 - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p, x, objFireMain.y - objFireMain.hOne / 2, objBeFireMain.x, objBeFireMain.y - 70);
			p.frame = 1;
			VecEff.addElement(p);
		}
		if (f < fRemove || VecEff.size() != 0)
		{
			return;
		}
		if (!checkNullObject(2))
		{
			if (isAddSound)
			{
				mSound.playSound(17, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			GameScreen.addEffectEnd(41, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(108, 8, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
		}
		removeEff();
	}

	public void updateSanji2()
	{
		if (f == 4)
		{
			addVir(5, 5, 10, isPlayer: true);
		}
		if (f >= 6 && f <= fRemove)
		{
			if (!checkNullObject(1) && CRes.random(2) == 0)
			{
				objFireMain.dx = CRes.random_Am_0(2);
				xplus = objFireMain.dx;
			}
			if (f % 3 == 0)
			{
				if (isAddSound)
				{
					mSound.playSound(15, mSound.volumeSound);
				}
				if (indexObjBefire < vecObjsBeFire.size())
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
					indexObjBefire++;
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							int xdich = mainObject.x - x;
							int ydich = mainObject.y - objFireMain.hOne / 2 - y;
							Point_Focus p = new Point_Focus();
							int num = y;
							y += CRes.random_Am_0(15);
							p = create_Speed(xdich, ydich, p);
							y = num;
							p.dis = 1;
							p.maxdis = 0;
							if (typeEffect == 220 || typeEffect == 293)
							{
								p.maxdis = 5;
							}
							p.frame = indexObjBefire % 2;
							VecEff.addElement(p);
						}
					}
				}
				else if (!GameCanvas.lowGraphic)
				{
					indexObjBefire++;
					int xdich2 = am_duong * 140 + CRes.random_Am_0(20);
					int ydich2 = CRes.random_Am_0(80);
					Point_Focus p2 = new Point_Focus();
					int num2 = y;
					y += CRes.random_Am_0(15);
					p2 = create_Speed(xdich2, ydich2, p2);
					y = num2;
					p2.dis = 0;
					if (typeEffect == 220 || typeEffect == 293)
					{
						p2.maxdis = 5;
					}
					p2.frame = indexObjBefire % 2;
					VecEff.addElement(p2);
				}
			}
		}
		if (f >= fRemove && VecSubEff.size() == 0)
		{
			if (!checkNullObject(1))
			{
				objFireMain.dx = 0;
			}
			removeEff();
		}
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.f++;
			if (point.f >= fraImgSub3Eff.nFrame)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			Point point2 = new Point(point_Focus.x, point_Focus.y);
			point2.frame = point_Focus.frame;
			VecSubEff.addElement(point2);
			if (point_Focus.f == point_Focus.fRe && point_Focus.dis == 1)
			{
				GameScreen.addEffectEnd(35, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				int subtype = 7;
				if (typeEffect == 293)
				{
					subtype = 0;
				}
				GameScreen.addEffectEnd(108, subtype, point_Focus.x, point_Focus.y, Dir, objMainEff);
			}
			if (point_Focus.f >= point_Focus.fRe + point_Focus.maxdis)
			{
				VecEff.removeElement(point_Focus);
				j--;
			}
		}
	}

	public void updateRankyaku()
	{
		if (f >= 3 && isAddSound)
		{
			mSound.playSound(13, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			if (f > 3 + i * 4)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
				point_Focus.update_Vx_Vy();
				if ((i == 0) & (point_Focus.f == point_Focus.fRe))
				{
					GameScreen.addEffectEnd(19, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
					GameScreen.addEffectEnd(108, 8, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				}
				if (point_Focus.f >= point_Focus.fRe + 15)
				{
					VecEff.removeElementAt(i);
					i--;
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateSoi()
	{
		if (f >= 2 && isAddSound)
		{
			mSound.playSound(13, mSound.volumeSound);
		}
		x1000 = x + 30 * am_duong;
		int xdich = x1000 - x;
		if (f == 2)
		{
			VecEff.addElement(create_Speed(xdich, -8, new Point_Focus(), x1000, y, toX, toY));
			VecEff.addElement(create_Speed(xdich, 8, new Point_Focus(), x1000, y, toX, toY));
		}
		if (f == 4)
		{
			VecEff.addElement(create_Speed(xdich, 0, new Point_Focus(), x1000, y, toX, toY));
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if ((i == 0) & (point_Focus.f == point_Focus.fRe))
			{
				GameScreen.addEffectEnd(123, 3, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 3, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
			}
			if (point_Focus.f >= point_Focus.fRe + 25)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateSoi2()
	{
		if (f >= 2 && isAddSound)
		{
			mSound.playSound(13, mSound.volumeSound);
		}
		x1000 = x + 30 * am_duong;
		int xdich = x1000 - x;
		if (f == 2)
		{
			VecEff.addElement(create_Speed(xdich, -14, new Point_Focus(), x1000, y, toX, toY));
			VecEff.addElement(create_Speed(xdich, 14, new Point_Focus(), x1000, y, toX, toY));
		}
		if (f == 4)
		{
			VecEff.addElement(create_Speed(xdich, -8, new Point_Focus(), x1000, y, toX, toY));
			VecEff.addElement(create_Speed(xdich, 8, new Point_Focus(), x1000, y, toX, toY));
		}
		if (f == 6)
		{
			VecEff.addElement(create_Speed(xdich, 0, new Point_Focus(), x1000, y, toX, toY));
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if ((i == 0) & (point_Focus.f == point_Focus.fRe))
			{
				GameScreen.addEffectEnd(19, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
			}
			if (point_Focus.f >= point_Focus.fRe + 25)
			{
				VecEff.removeElementAt(i);
				i--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateHuou()
	{
		if (f >= 4 && isAddSound)
		{
			mSound.playSound(13, mSound.volumeSound);
		}
		if (f == 5)
		{
			GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.x += point.vx;
			point.y += point.vy;
			if (point.y < 40)
			{
				continue;
			}
			if (i >= size1)
			{
				VecEff.removeElementAt(i);
				i--;
				continue;
			}
			createPointHuou(point);
			Point point2 = new Point();
			point2.x = CRes.random_Am_0(40);
			point2.y = CRes.random_Am_0(30);
			point2.dis = 5;
			if (typeEffect == 279)
			{
				point2.dis = CRes.random(10);
			}
			point2.frame = 0;
			point2.maxframe = 3;
			VecSubEff.addElement(point2);
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point point3 = (Point)VecSubEff.elementAt(j);
			point3.frame++;
			if (point3.frame >= point3.maxframe)
			{
				VecSubEff.removeElementAt(j);
				j--;
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateShigan()
	{
		if (f > 2)
		{
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(0);
				point_Focus.update_Vx_Vy();
				if (f == 4)
				{
					GameScreen.addEffectEnd(35, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
					GameScreen.addEffectEnd(108, 5, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				}
			}
		}
		if (f >= fRemove)
		{
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateDoor()
	{
		if (f == 1)
		{
			x1000 = x;
		}
		if (f == 15)
		{
			x1000 = objBeFireMain.x + 40 * am_duong;
		}
		if (f == 6)
		{
			objFireMain.isTanHinh = true;
			GameScreen.addEffectEnd(80, 0, objFireMain.x, y, Dir, objMainEff);
		}
		if (f == 20)
		{
			objFireMain.x = x1000;
			changeDir();
			objFireMain.Dir = Dir;
			GameScreen.addEffectEnd(80, 0, objFireMain.x, y, Dir, objMainEff);
			objFireMain.isTanHinh = false;
		}
		if (f == 23)
		{
			GameScreen.addEffectEnd(123, 2, objBeFireMain.x, y, Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void addPoint(int x, int y)
	{
		Point point = new Point();
		point.x = x;
		point.y = y;
		VecEff.addElement(point);
	}

	public void updateDoor2()
	{
		if (f >= 2 && f <= 20)
		{
			if (f == 2)
			{
				addPoint(x, y);
			}
			if (f == 6)
			{
				addPoint(objBeFireMain.x + 90 * am_duong, y - 60);
			}
			if (f == 8)
			{
				addPoint(objBeFireMain.x - 90 * am_duong, y - 60);
			}
			if (f == 12)
			{
				addPoint(objBeFireMain.x + 90 * am_duong, y + 60);
			}
			if (f == 16)
			{
				addPoint(objBeFireMain.x - 90 * am_duong, y + 60);
			}
			if (f == 20)
			{
				addPoint(objBeFireMain.x + 40 * am_duong, y);
			}
		}
		if (f >= 4 && f <= 25 && (f - 4) % 4 == 0)
		{
			Point point = (Point)VecEff.elementAt((f - 4) / 4);
			objFireMain.isTanHinh = true;
			GameScreen.addEffectEnd(80, 0, point.x, point.y, Dir, objMainEff);
		}
		if (f == 25)
		{
			objFireMain.x = objBeFireMain.x + 40 * am_duong;
			changeDir();
			objFireMain.Dir = Dir;
			objFireMain.isTanHinh = false;
			GameScreen.addEffectEnd(80, 0, objFireMain.x, y, Dir, objMainEff);
		}
		if (f == 23)
		{
			GameScreen.addEffectEnd(123, 2, objBeFireMain.x, y, Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateSanji4()
	{
		if (objBeFireMain != null && objBeFireMain.hOne > 0)
		{
			if (f == 1 && isAddSound)
			{
				mSound.playSound(13, mSound.volumeSound);
			}
			if (f % 4 == 0)
			{
				if (typeEffect == 14)
				{
					setAva(0, objBeFireMain);
				}
				if (!checkNullObject(2))
				{
					GameScreen.addEffectEnd(1, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
					GameScreen.addEffectEnd(93, 2, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
				}
				if (typeEffect == 44)
				{
					setAva(1, objBeFireMain);
					if (!checkNullObject(2))
					{
						GameScreen.addEffectEnd(1, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
					}
					int num = 25;
					if (Dir == 0)
					{
						num = -25;
					}
					if (!checkNullObject(1))
					{
						GameScreen.addEffectEnd(35, 0, objFireMain.x + num, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
					}
				}
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateZoroSea3()
	{
		if ((f == 4 || f == 10) && !checkNullObject(1))
		{
			GameScreen.addEffectEnd(30, 0, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, 200, Dir, objMainEff);
		}
		if (f >= 1 && f <= 4 && !checkNullObject(1))
		{
			objFireMain.dy = f * 14;
		}
		if (f >= 5 && f <= 13 && !checkNullObject(1))
		{
			objFireMain.dy = 56;
		}
		if (f >= 14 && f <= 17 && !checkNullObject(1))
		{
			objFireMain.dy = (17 - f) * 14;
		}
		if (f == 5 || f == 11)
		{
			if (isAddSound)
			{
				mSound.playSound(12, mSound.volumeSound);
			}
			int num = 20;
			if (Dir == 0)
			{
				num = -20;
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(16, 0, x, objFireMain.y - objFireMain.hOne / 2 - 10 - objFireMain.dy, Dir, objMainEff);
				GameScreen.addEffectEnd(16, 1, x + num, objFireMain.y - objFireMain.hOne / 2 - 10 - objFireMain.dy, Dir, objMainEff);
			}
		}
		if (!checkNullObject(3) && (f == 6 || f == 12))
		{
			addVir(5, 5, 10, isPlayer: true);
			sbyte dir = 0;
			if (objFireMain.x < objBeFireMain.x)
			{
				dir = 2;
			}
			int num2 = 18;
			if (Dir == 0)
			{
				num2 = -18;
			}
			sbyte b = 2;
			GameScreen.addEffectEnd_ObjTo(27, b, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2, objBeFireMain.ID, objBeFireMain.typeObject, dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			if (!checkNullObject(1))
			{
				objFireMain.dy = 0;
			}
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateZoroSea1()
	{
		if (f == 1 && !checkNullObject(1))
		{
			GameScreen.addEffectEnd(30, 0, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, 300, Dir, objMainEff);
		}
		if (f == 11)
		{
			if (isAddSound)
			{
				mSound.playSound(12, mSound.volumeSound);
			}
			int num = 20;
			if (Dir == 0)
			{
				num = -20;
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(16, 0, x, objFireMain.y - objFireMain.hOne / 2 - 10, Dir, objMainEff);
				GameScreen.addEffectEnd(16, 1, x + num, objFireMain.y - objFireMain.hOne / 2 - 10, Dir, objMainEff);
			}
		}
		if (!checkNullObject(3) && f == 12)
		{
			sbyte dir = 0;
			if (objFireMain.x < objBeFireMain.x)
			{
				dir = 2;
			}
			int num2 = 18;
			if (Dir == 0)
			{
				num2 = -18;
			}
			sbyte b = 0;
			GameScreen.addEffectEnd_ObjTo(27, b, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2, objBeFireMain.ID, objBeFireMain.typeObject, dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateZoroSea2()
	{
		if (f == 7 && !checkNullObject(1))
		{
			GameScreen.addEffectEnd(30, 0, objFireMain.x, objFireMain.y - objFireMain.hOne, 250, Dir, objMainEff);
		}
		if (f == 4 || f == 16)
		{
			if (isAddSound)
			{
				mSound.playSound(12, mSound.volumeSound);
			}
			if (!checkNullObject(1))
			{
				int num = 20;
				if (Dir == 0)
				{
					num = -20;
				}
				if (!checkNullObject(1))
				{
					GameScreen.addEffectEnd(16, 0, x, objFireMain.y - objFireMain.hOne / 2 - 10, Dir, objMainEff);
					GameScreen.addEffectEnd(16, 1, x + num, objFireMain.y - objFireMain.hOne / 2 - 10, Dir, objMainEff);
				}
			}
		}
		if (!checkNullObject(3) && (f == 5 || f == 17))
		{
			sbyte dir = 0;
			if (objFireMain.x < objBeFireMain.x)
			{
				dir = 2;
			}
			int num2 = 18;
			if (Dir == 0)
			{
				num2 = -18;
			}
			sbyte b = 1;
			GameScreen.addEffectEnd_ObjTo(27, b, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2, objBeFireMain.ID, objBeFireMain.typeObject, dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
			removeEff();
		}
	}

	public void updateZoro3()
	{
		int num = 5;
		if (f == 5)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			int num2 = 20;
			if (Dir == 0)
			{
				num2 = -20;
			}
			setAva(0, objBeFireMain);
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				GameScreen.addEffectEnd(93, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(16, 1, x + num2, objFireMain.y - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
			}
		}
		if (f == 10)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			int num3 = 30;
			if (Dir == 0)
			{
				num3 = -30;
			}
			setAva(0, objBeFireMain);
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				GameScreen.addEffectEnd(93, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(16, 2, x + num3, objFireMain.y - objFireMain.hOne / 2 + num, Dir, objMainEff);
			}
		}
		if (f == 15)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			int num4 = 20;
			if (Dir == 0)
			{
				num4 = -20;
			}
			setAva(1, objBeFireMain);
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				GameScreen.addEffectEnd(93, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(16, 1, x + num4, objFireMain.y - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateLuffy6()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			removeEff();
			if (objFireMain == GameScreen.player)
			{
				GameScreen.setIsMoveEff(ismove: false);
			}
		}
		if (f < 7)
		{
			if (Dir == 0)
			{
				objFireMain.vx = -objFireMain.vMax * 3;
			}
			else
			{
				objFireMain.vx = objFireMain.vMax * 3;
			}
		}
		else
		{
			objFireMain.vx = 0;
		}
		if (f == 7)
		{
			setAva(1, objBeFireMain);
			int num = 20;
			if (Dir == 0)
			{
				num = -20;
			}
			GameScreen.addEffectEnd(1, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
			if (isAddSound)
			{
				mSound.playSound(3, mSound.volumeSound);
			}
			GameScreen.addEffectEnd(93, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
			GameScreen.addEffectEnd(0, 0, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
		}
	}

	public void updateLuffy_S2_L2()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			removeEff();
			if (objFireMain == GameScreen.player)
			{
				GameScreen.setIsMoveEff(ismove: false);
			}
			return;
		}
		if (f < 6)
		{
			if (Dir == 0)
			{
				objFireMain.vx = -objFireMain.vMax * 3;
			}
			else
			{
				objFireMain.vx = objFireMain.vMax * 3;
			}
			if (f % 2 == 1)
			{
				Point o = new Point(objFireMain.x - objFireMain.vx / 2, objFireMain.y);
				VecEff.addElement(o);
			}
		}
		else
		{
			objFireMain.vx = 0;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f / 2 >= 3)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f == 6)
		{
			if (isAddSound)
			{
				mSound.playSound(3, mSound.volumeSound);
			}
			setAva(2, objBeFireMain);
			int num = 20;
			if (Dir == 0)
			{
				num = -20;
			}
			GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(0, 0, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
		}
	}

	public void updateNami5()
	{
		if (!checkNullObject(1))
		{
			if (objFireMain.Dir == 0)
			{
				x = x1000 - 20;
			}
			else
			{
				x = x1000 + 20;
			}
		}
		if (f > 5 && (typeEffect == 55 || typeEffect == 31 || f >= 10) && f % 3 == 0 && f <= fRemove)
		{
			if (indexObjBefire < vecObjsBeFire.size())
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
				indexObjBefire++;
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						int num = mainObject.hOne / 2;
						if (typeEffect == 56 || typeEffect == 191 || typeEffect == 223)
						{
							num = mainObject.hOne + 20;
						}
						int xdich = mainObject.x - x;
						int ydich = mainObject.y - num - y;
						Point_Focus p = new Point_Focus();
						p = create_Speed(xdich, ydich, p);
						p.objMain = mainObject;
						VecEff.addElement(p);
					}
				}
			}
			else if (typeEffect == 223 && !GameCanvas.lowGraphic)
			{
				int xdich2 = CRes.random_Am_0(100);
				int ydich2 = -50 + CRes.random_Am_0(60);
				Point_Focus p2 = new Point_Focus();
				p2 = create_Speed(xdich2, ydich2, p2);
				VecEff.addElement(p2);
			}
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f < point_Focus.fRe)
			{
				continue;
			}
			if (isAddSound)
			{
				mSound.playSound(19, mSound.volumeSound);
			}
			if (typeEffect == 31)
			{
				GameScreen.addEffectEnd(38, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
			}
			else if (typeEffect == 55)
			{
				GameScreen.addEffectEnd(41, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
			}
			else if (typeEffect == 56 || typeEffect == 191 || typeEffect == 191 || typeEffect == 223)
			{
				if (isAddSound)
				{
					mSound.playSound(17, mSound.volumeSound);
				}
				addVir(5, 5, 10, isPlayer: true);
				sbyte subtype = 0;
				if (typeEffect == 191)
				{
					subtype = 1;
				}
				else if (typeEffect == 223)
				{
					subtype = 2;
				}
				if (point_Focus.objMain == null)
				{
					GameScreen.addEffectEnd(39, subtype, point_Focus.x, point_Focus.y, Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd_ObjTo(39, subtype, point_Focus.objMain.x, point_Focus.objMain.y - point_Focus.objMain.hOne - 20, point_Focus.objMain.ID, point_Focus.objMain.typeObject, 0, objMainEff);
				}
			}
			GameScreen.addEffectEnd(93, 1, point_Focus.x, point_Focus.y, Dir, objMainEff);
			VecEff.removeElement(point_Focus);
			i--;
		}
		if (f > fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateNami6()
	{
		if (!checkNullObject(1))
		{
			if (objFireMain.Dir == 0)
			{
				x = x1000 - 20;
			}
			else
			{
				x = x1000 + 20;
			}
		}
		if (f == 10)
		{
			int num = objFireMain.hOne + 50;
			int xdich = 0;
			int ydich = -num;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			VecEff.addElement(p);
		}
		if (f >= 10 && f <= 19)
		{
			int num2 = objFireMain.hOne + 50;
			int num3 = 100 * CRes.getcos((f - 10) * 360 / 10) / 1000;
			int num4 = -num2 + 30 * CRes.getsin((f - 10) * 360 / 10) / 1000;
			Point_Focus p2 = new Point_Focus();
			p2 = create_Speed(num3, num4, p2, x, y, num3 - x, num4 - y);
			p2.objMain = objFireMain;
			VecEff.addElement(p2);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				if (isAddSound)
				{
					mSound.playSound(19, mSound.volumeSound);
				}
				if (isAddSound)
				{
					mSound.playSound(17, mSound.volumeSound);
				}
				addVir(5, 5, 10, isPlayer: true);
				sbyte subtype = 2;
				if (point_Focus.objMain == null)
				{
					subtype = 3;
				}
				GameScreen.addEffectEnd(39, subtype, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(93, 1, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f <= fRemove || VecEff.size() != 0)
		{
			return;
		}
		for (int j = 0; j < vecObjsBeFire.size(); j++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
			if (object_Effect_Skill != null)
			{
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					GameScreen.addEffectEnd(42, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, mainObject);
					GameScreen.addEffectEnd(41, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, mainObject);
					GameScreen.addEffectEnd(8, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, mainObject);
					GameScreen.addEffectEnd(108, 8, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, mainObject);
				}
			}
		}
		removeEff();
	}

	public void updateNami4()
	{
		if (f == 8 && isAddSound)
		{
			mSound.playSound(10, mSound.volumeSound);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
		else if (f > 4 && f % 5 == 0 && !checkNullObject(2))
		{
			if (typeEffect == 16)
			{
				setAva(0, objBeFireMain);
				GameScreen.addEffectEnd(3, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
			}
			else if (typeEffect == 51)
			{
				setAva(1, objBeFireMain);
				GameScreen.addEffectEnd(1, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
			}
			GameScreen.addEffectEnd(93, 1, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
		}
	}

	public void updateZoro8()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			if (!checkNullObject(1))
			{
				objFireMain.vx = 0;
				objFireMain.vy = 0;
				objFireMain.isTanHinh = false;
				if (objFireMain.plashNow != null)
				{
					objFireMain.plashNow.setIsNextf(0);
				}
			}
			int num = 30;
			if (Dir == 0)
			{
				num = -30;
			}
			if (!checkNullObject(1))
			{
				if (isAddSound)
				{
					mSound.playSound(9, mSound.volumeSound);
				}
				if (typeEffect == 29)
				{
					setAva(2, objBeFireMain);
					GameScreen.addEffectEnd(26, 0, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd(19, 0, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
					setAva(1, objBeFireMain);
				}
				GameScreen.addEffectEnd(93, 0, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
			}
			removeEff();
		}
		else
		{
			objFireMain.vx = vx;
			objFireMain.vy = vy;
			if (LoadMap.Tile_Stand(GameCanvas.loadmap.getTile(objFireMain.x + objFireMain.vx, objFireMain.y + objFireMain.vy)))
			{
				objFireMain.vx = 0;
				objFireMain.vy = 0;
			}
		}
	}

	public void updateUssopSkill1_Lv3()
	{
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
		if ((f == 0 || f == 3) && isAddSound)
		{
			mSound.playSound(21, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				if (isAddSound)
				{
					mSound.playSound(19, mSound.volumeSound);
				}
				if (point_Focus.frame == 0)
				{
					GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(20), toY + CRes.random_Am_0(20), Dir, objMainEff);
					GameScreen.addEffectEnd(35, 0, toX, toY, Dir, objMainEff);
				}
				else if (point_Focus.frame == 1)
				{
					GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(20), toY + CRes.random_Am_0(20), Dir, objMainEff);
					GameScreen.addEffectEnd(35, 0, toX, toY, Dir, objMainEff);
				}
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 3)
		{
			int xdich = toX - x;
			int ydich = toY - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			p.frame = 1;
			GameScreen.addEffectEnd(1, 0, x, y, Dir, objMainEff);
			VecEff.addElement(p);
		}
	}

	public void updateUssopSkill1_Lv3_New()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(1))
		{
			removeEff();
		}
		if ((f == 0 || f == 3 || f == 10 || f == 13 || f == 20 || f == 23) && isAddSound)
		{
			mSound.playSound(21, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				addVir(5, 5, 10, isPlayer: true);
				if (point_Focus.frame == 0)
				{
					GameScreen.addEffectEnd(1, 0, point_Focus.toX + CRes.random_Am_0(20), point_Focus.toY + CRes.random_Am_0(20), Dir, objMainEff);
					GameScreen.addEffectEnd(35, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				}
				else if (point_Focus.frame == 1)
				{
					GameScreen.addEffectEnd(1, 0, point_Focus.toX + CRes.random_Am_0(20), point_Focus.toY + CRes.random_Am_0(20), Dir, objMainEff);
					GameScreen.addEffectEnd(35, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				}
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 5 || f == 15)
		{
			objFireMain.isTanHinh = true;
		}
		if (f == 7)
		{
			objFireMain.x -= am_duong * 10;
			objFireMain.y += CRes.random_Am(1, 2) * 20;
			y = objFireMain.y - objFireMain.hOne / 2;
			x = objFireMain.x;
			y -= 6;
			if (Dir == 0)
			{
				x -= 30;
			}
			else
			{
				x += 30;
			}
		}
		if (f == 9 || f == 19)
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 17)
		{
			objFireMain.x = xArchor;
			objFireMain.y = yArchor;
			y = objFireMain.y - objFireMain.hOne / 2;
			x = objFireMain.x;
			y -= 6;
			if (Dir == 0)
			{
				x -= 30;
			}
			else
			{
				x += 30;
			}
		}
		if ((f == 3 || f == 10 || f == 13 || f == 20 || f == 13) && !checkNullObject(3))
		{
			int num = 30;
			if (Dir == 0)
			{
				num = -30;
			}
			int xdich = objBeFireMain.x - (objFireMain.x + num);
			int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - (objFireMain.y - objFireMain.hOne / 2);
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2);
			p.frame = 1;
			GameScreen.addEffectEnd(1, 0, x, y, Dir, objMainEff);
			VecEff.addElement(p);
		}
	}

	public void updateUssopSkill1_Lv3_SHORT()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(1))
		{
			removeEff();
		}
		if ((f == 0 || f == 3 || f == 10 || f == 13) && isAddSound)
		{
			mSound.playSound(21, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f < point_Focus.fRe)
			{
				continue;
			}
			addVir(5, 5, 10, isPlayer: true);
			if (typeEffect == 192)
			{
				GameScreen.addEffectEnd(25, 4, point_Focus.toX + CRes.random_Am_0(20), point_Focus.toY + CRes.random_Am_0(20), Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(1, 0, point_Focus.toX + CRes.random_Am_0(20), point_Focus.toY + CRes.random_Am_0(20), Dir, objMainEff);
				if (point_Focus.frame == 2)
				{
					GameScreen.addEffectEnd(108, 5, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				}
			}
			GameScreen.addEffectEnd(35, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
			VecEff.removeElement(point_Focus);
			i--;
		}
		if (f == 5 || f == 14)
		{
			objFireMain.isTanHinh = true;
		}
		if (f == 7)
		{
			objFireMain.x -= am_duong * 10;
			objFireMain.y += CRes.random_Am(1, 2) * 20;
			y = objFireMain.y - objFireMain.hOne / 2;
			x = objFireMain.x;
			y -= 6;
			if (Dir == 0)
			{
				x -= 30;
			}
			else
			{
				x += 30;
			}
		}
		if (f == 15)
		{
			objFireMain.x = xArchor;
			objFireMain.y = yArchor;
			y = objFireMain.y - objFireMain.hOne / 2;
			x = objFireMain.x;
			y -= 6;
			if (Dir == 0)
			{
				x -= 30;
			}
			else
			{
				x += 30;
			}
		}
		if (f == 9 || f == 15)
		{
			objFireMain.isTanHinh = false;
		}
		if ((f == 3 || f == 10 || f == 13) && !checkNullObject(3))
		{
			int num = 30;
			if (Dir == 0)
			{
				num = -30;
			}
			int xdich = objBeFireMain.x - (objFireMain.x + num);
			int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - (objFireMain.y - objFireMain.hOne / 2);
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2);
			p.frame = 1;
			if (f == 10)
			{
				p.frame = 2;
			}
			GameScreen.addEffectEnd(1, 0, x, y, Dir, objMainEff);
			VecEff.addElement(p);
		}
	}

	public void updateUssopS1_L5()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(1))
		{
			if (objFireMain != null)
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
		if ((f == 2 || f == 6 || f == 10 || f == 14) && isAddSound)
		{
			mSound.playSound(21, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				addVir(5, 5, 10, isPlayer: true);
				sbyte subtype = 4;
				if (point_Focus.frame == 2)
				{
					subtype = 3;
				}
				GameScreen.addEffectEnd(25, subtype, point_Focus.toX + CRes.random_Am_0(20), point_Focus.toY + CRes.random_Am_0(20), Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				GameScreen.addEffectEnd(35, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 2)
		{
			objFireMain.isTanHinh = true;
		}
		else if (f == 15)
		{
			objFireMain.isTanHinh = false;
		}
		if (f > 3 && f % 2 == 0 && !checkNullObject(3))
		{
			int num = 25;
			int num2 = (f - 2) / 2;
			if (num2 >= mframeSuper.Length)
			{
				return;
			}
			if (Dir == 0)
			{
				num = -25;
			}
			int xdich = objFireMain.x + mframeSuper[num2][0] - objBeFireMain.x + num;
			int ydich = objFireMain.y - objFireMain.hOne / 2 + mframeSuper[num2][0] - (objBeFireMain.y - objBeFireMain.hOne / 2);
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p, objFireMain.x + mframeSuper[num2][0] + num, objFireMain.y - objFireMain.hOne / 2 + mframeSuper[num2][0], objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2);
			p.frame = 1;
			if (f == 8 || f == 14)
			{
				p.frame = 2;
			}
			GameScreen.addEffectEnd(1, 0, objFireMain.x + mframeSuper[num2][0] + num, objFireMain.y - objFireMain.hOne / 2 + mframeSuper[num2][0] - 10, Dir, objMainEff);
			VecEff.addElement(p);
		}
		if (typeEffect != 301)
		{
			return;
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			if (f > j * 4)
			{
				((Point_Focus)VecSubEff.elementAt(j)).update_Vx_Vy();
			}
		}
	}

	public void updateUssopS1_L6()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(1))
		{
			if (objFireMain != null)
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
		if ((f == 2 || f == 6 || f == 10 || f == 14) && isAddSound)
		{
			mSound.playSound(21, mSound.volumeSound);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				addVir(5, 5, 10, isPlayer: true);
				sbyte subtype = 4;
				if (point_Focus.frame == 2)
				{
					subtype = 3;
				}
				GameScreen.addEffectEnd(25, subtype, point_Focus.toX + CRes.random_Am_0(20), point_Focus.toY + CRes.random_Am_0(20), Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				GameScreen.addEffectEnd(35, 0, point_Focus.toX, point_Focus.toY, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 2)
		{
			objFireMain.isTanHinh = true;
		}
		else if (f == 15)
		{
			objFireMain.isTanHinh = false;
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			if (f > j * 4)
			{
				((Point_Focus)VecSubEff.elementAt(j)).update_Vx_Vy();
			}
		}
		if (f <= 3 || f % 2 != 0 || checkNullObject(3))
		{
			return;
		}
		int num = 25;
		int num2 = (f - 2) / 2;
		if (num2 < mframeSuper.Length)
		{
			if (Dir == 0)
			{
				num = -25;
			}
			int xdich = objFireMain.x + mframeSuper[num2][0] - objBeFireMain.x + num;
			int ydich = objFireMain.y - objFireMain.hOne / 2 + mframeSuper[num2][0] - (objBeFireMain.y - objBeFireMain.hOne / 2);
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p, objFireMain.x + mframeSuper[num2][0] + num, objFireMain.y - objFireMain.hOne / 2 + mframeSuper[num2][0], objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2);
			p.frame = 1;
			if (f == 8 || f == 14)
			{
				p.frame = 2;
			}
			GameScreen.addEffectEnd(1, 0, objFireMain.x + mframeSuper[num2][0] + num, objFireMain.y - objFireMain.hOne / 2 + mframeSuper[num2][0] - 10, Dir, objMainEff);
			VecEff.addElement(p);
		}
	}

	public void updateNamiSkill1_Lv3()
	{
		if (f >= fRemove)
		{
			removeEff();
		}
		else
		{
			if (isAddSound && f == 8)
			{
				mSound.playSound(10, mSound.volumeSound);
			}
			if ((f == 5 || f == 15) && isAddSound)
			{
				mSound.playSound(17, mSound.volumeSound);
			}
			if (f == 5 && typeEffect == 311)
			{
				GameScreen.addEffectEnd(174, 0, objBeFireMain.x, objBeFireMain.y, Dir, objBeFireMain);
			}
			if (f > 4 && f % 5 == 0 && objBeFireMain != null)
			{
				addVir(5, 5, 10, isPlayer: true);
				setAva(1, objBeFireMain);
				sbyte subtype = 1;
				if ((typeEffect == 221 || typeEffect == 311) && CRes.random(2) == 0)
				{
					subtype = 3;
				}
				GameScreen.addEffectEnd(38, subtype, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
				if (f == 10 || typeEffect == 221 || typeEffect == 311)
				{
					subtype = 3;
					if ((typeEffect == 221 || typeEffect == 311) && CRes.random(2) == 0)
					{
						subtype = 8;
					}
					GameScreen.addEffectEnd(108, subtype, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
				}
			}
			if ((typeEffect == 189 || typeEffect == 221 || typeEffect == 311) && f > 4 && f % 3 == 0 && objBeFireMain != null)
			{
				short type = 38;
				if ((typeEffect == 221 || typeEffect == 311) && CRes.random(2) == 0)
				{
					type = 138;
				}
				GameScreen.addEffectEnd(type, 2, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
			}
			if (objFireMain != null && !GameCanvas.lowGraphic)
			{
				int num = x - 20;
				if (Dir == 0)
				{
					num = x + 20;
				}
				int a = 25;
				if (objFireMain.hOne > 1)
				{
					a = objFireMain.hOne / 2;
				}
				Point point = new Point(num + CRes.random_Am_0(20), y + CRes.random_Am_0(a));
				if ((typeEffect == 221 || typeEffect == 311) && CRes.random(2) == 0)
				{
					point.frame = 1;
				}
				VecEff.addElement(point);
			}
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point2 = (Point)VecEff.elementAt(i);
			point2.f++;
			if (point2.f >= 4)
			{
				VecEff.removeElement(point2);
				i--;
			}
		}
	}

	public void updateSanjiSkill3_Lv1()
	{
		if (f >= 4)
		{
			if (isAddSound)
			{
				mSound.playSound(13, mSound.volumeSound);
			}
			if (objFireMain != null && CRes.random(2) == 0)
			{
				objFireMain.dx = CRes.random_Am_0(2);
				xplus = objFireMain.dx;
			}
			if (f % 2 == 0 && indexObjBefire < vecObjsBeFire.size())
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
				indexObjBefire++;
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						int xdich = mainObject.x - x;
						int ydich = mainObject.y - objFireMain.hOne / 2 - y;
						Point_Focus p = new Point_Focus();
						p = create_Speed(xdich, ydich, p);
						p.frame = CRes.random(6);
						VecEff.addElement(p);
					}
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			if (objFireMain != null)
			{
				objFireMain.dx = 0;
			}
			removeEff();
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				if (typeEffect == 49)
				{
					GameScreen.addEffectEnd(1, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				}
				else if (typeEffect == 50)
				{
					GameScreen.addEffectEnd(35, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				}
				GameScreen.addEffectEnd(93, 2, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
	}

	public void updateLuffyS1()
	{
		if (objBeFireMain != null && objBeFireMain.hOne > 0 && f % 5 == 0)
		{
			sbyte b = 0;
			if (typeEffect == 33)
			{
				b = 2;
				if (!checkNullObject(2))
				{
					GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				}
			}
			setAva(b, objBeFireMain);
			if (!checkNullObject(1))
			{
				int num = 28;
				if (objFireMain.Dir == 0)
				{
					num = -28;
				}
				if (isAddSound)
				{
					mSound.playSound(2, mSound.volumeSound);
				}
				if (typeEffect == 176)
				{
					GameScreen.addEffectEnd(114, 0, objFireMain.x + num - am_duong * 8, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 5, (sbyte)objFireMain.Dir, objMainEff);
					GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd(25, b, objFireMain.x + num, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
					GameScreen.addEffectEnd(93, 0, objFireMain.x + num, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				}
			}
		}
		if (f >= fRemove)
		{
			if (typeEffect == 176 && !checkNullObject(1))
			{
				GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			removeEff();
		}
	}

	public void updateLuffyS1_NEW()
	{
		if (f < 20 && f % 5 == 0)
		{
			if (isAddSound)
			{
				mSound.playSound(2, mSound.volumeSound);
			}
			int num = 28;
			if (Dir == 0)
			{
				num = -28;
			}
			if (!checkNullObject(2))
			{
				setDy(-6, objBeFireMain);
				if (objBeFireMain.typeObject == 1 && objBeFireMain.Action != 4)
				{
					objBeFireMain.Action = 3;
				}
				GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(25, 2, objFireMain.x + num, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
		}
		if (f == 20)
		{
			if (isAddSound)
			{
				mSound.playSound(6, mSound.volumeSound);
			}
			int num2 = -15;
			if (Dir == 0)
			{
				num2 = 15;
			}
			GameScreen.addEffectEnd(171, 0, x + num2, y, 450, Dir, objMainEff);
		}
		if (f == 32)
		{
			if (isAddSound)
			{
				mSound.playSound(5, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			setAva(2, objBeFireMain);
			int num3 = 28;
			if (Dir == 0)
			{
				num3 = -28;
			}
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(54, 2, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateLuffyS1_L3_SHORT()
	{
		if (f == 4)
		{
			if (isAddSound)
			{
				mSound.playSound(2, mSound.volumeSound);
			}
			int num = 28;
			if (Dir == 0)
			{
				num = -28;
			}
			if (!checkNullObject(2))
			{
				setDy(-6, objBeFireMain);
				if (objBeFireMain.typeObject == 1 && objBeFireMain.Action != 4)
				{
					objBeFireMain.Action = 3;
				}
				GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(25, 2, objFireMain.x + num, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
		}
		if (f == 5)
		{
			if (isAddSound)
			{
				mSound.playSound(6, mSound.volumeSound);
			}
			int num2 = -15;
			if (Dir == 0)
			{
				num2 = 15;
			}
			GameScreen.addEffectEnd(30, 0, x + num2, y, 150, Dir, objMainEff);
		}
		if (typeEffect == 83)
		{
			if (f == 15)
			{
				if (isAddSound)
				{
					mSound.playSound(5, mSound.volumeSound);
				}
				addVir(5, 5, 10, isPlayer: true);
				setAva(2, objBeFireMain);
				int num3 = 28;
				if (Dir == 0)
				{
					num3 = -28;
				}
				if (!checkNullObject(2))
				{
					GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
					GameScreen.addEffectEnd(108, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
				}
				if (!checkNullObject(1))
				{
					GameScreen.addEffectEnd(25, 2, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
					GameScreen.addEffectEnd(54, 0, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				}
			}
		}
		else if (typeEffect == 180)
		{
			if (f == 13 || f == 17)
			{
				if (isAddSound)
				{
					mSound.playSound(5, mSound.volumeSound);
				}
				addVir(5, 5, 10, isPlayer: true);
				setAva(2, objBeFireMain);
				int num4 = 28;
				if (Dir == 0)
				{
					num4 = -28;
				}
				if (!checkNullObject(2))
				{
					GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
					GameScreen.addEffectEnd(108, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
				}
				if (!checkNullObject(1))
				{
					GameScreen.addEffectEnd(25, 2, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
					GameScreen.addEffectEnd(54, (f != 13) ? 6 : 7, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				}
			}
		}
		else if (typeEffect == 212)
		{
			for (int i = 0; i < VecSubEff.size(); i++)
			{
				Point point = (Point)VecSubEff.elementAt(i);
				point.update();
				if (point.f >= point.fRe)
				{
					VecSubEff.removeElement(point);
					i--;
				}
			}
			if (f < fRemove && f % 3 == 0 && !GameCanvas.lowGraphic)
			{
				Point point2 = new Point();
				point2.x = x + CRes.random_Am_0(15);
				point2.y = y + 15 + CRes.random_Am_0(5);
				point2.vx = CRes.random_Am_0(2);
				point2.vy = -CRes.random(1, 4);
				point2.fRe = CRes.random(10, 14);
				VecSubEff.addElement(point2);
			}
			if (f == 13 || f == 17)
			{
				if (isAddSound)
				{
					mSound.playSound(5, mSound.volumeSound);
				}
				addVir(5, 5, 10, isPlayer: true);
				setAva(2, objBeFireMain);
				int num5 = 28;
				if (Dir == 0)
				{
					num5 = -28;
				}
				if (!checkNullObject(2))
				{
					GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
					GameScreen.addEffectEnd(108, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
				}
				if (!checkNullObject(1))
				{
					GameScreen.addEffectEnd(25, 2, objFireMain.x + num5, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
					GameScreen.addEffectEnd(54, (f != 13) ? 9 : 7, objFireMain.x + num5, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				}
			}
		}
		if (f >= fRemove && (typeEffect != 212 || VecSubEff.size() == 0))
		{
			removeEff();
		}
	}

	public void update_Luffy_S1_L6()
	{
		if (f >= fRemove && VecSubEff.size() == 0)
		{
			removeEff();
		}
		if (f == 1)
		{
			if (isAddSound)
			{
				mSound.playSound(6, mSound.volumeSound);
			}
			int num = -15;
			if (Dir == 0)
			{
				num = 15;
			}
			GameScreen.addEffectEnd(171, 0, x + num, y, 450, Dir, objMainEff);
		}
		if (f == 10)
		{
			if (isAddSound)
			{
				mSound.playSound(5, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			setAva(2, objBeFireMain);
			int num2 = 28;
			if (Dir == 0)
			{
				num2 = -28;
			}
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 5, objBeFireMain.x, objBeFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(54, 5, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
			GameScreen.addEffectEnd(119, 3, objFireMain.x + am_duong * 20, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
		}
		if (f == 14)
		{
			int num3 = 28;
			if (Dir == 0)
			{
				num3 = -28;
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(54, 6, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
		}
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecSubEff.removeElement(point);
				i--;
			}
		}
		if (f < fRemove && f % 3 == 0 && !GameCanvas.lowGraphic)
		{
			Point point2 = new Point();
			point2.x = x + CRes.random_Am_0(15);
			point2.y = y + 15 + CRes.random_Am_0(5);
			point2.vx = CRes.random_Am_0(2);
			point2.vy = -CRes.random(1, 4);
			point2.fRe = CRes.random(10, 14);
			VecSubEff.addElement(point2);
		}
	}

	public void updateXaPhong()
	{
		if (f >= fRemove && VecSubEff.size() == 0)
		{
			removeEff();
		}
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			point.update();
			if (point.f < point.fRe)
			{
				continue;
			}
			int num = CRes.random(3) + 1;
			if (typeEffect == 274)
			{
				num = 2;
			}
			if (num == 2)
			{
				GameScreen.addEffectEnd(71, 0, point.x, point.y, Dir, objMainEff);
				if (CRes.random(4) == 0)
				{
					GameScreen.addEffectEnd(108, 4, point.x, point.y, Dir, objMainEff);
				}
			}
			else
			{
				GameScreen.addEffectEnd(38, num, point.x, point.y, Dir, objMainEff);
				if (CRes.random(4) == 0)
				{
					if (num == 1)
					{
						GameScreen.addEffectEnd(108, 3, point.x, point.y, Dir, objMainEff);
					}
					else
					{
						GameScreen.addEffectEnd(108, 8, point.x, point.y, Dir, objMainEff);
					}
				}
			}
			VecSubEff.removeElement(point);
			i--;
		}
		if (f < fRemove && !GameCanvas.lowGraphic)
		{
			for (int j = 0; j < 2; j++)
			{
				Point point2 = new Point();
				point2.x = objBeFireMain.x + CRes.random_Am_0(15);
				point2.y = objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10);
				point2.vx = CRes.random_Am_0(4);
				point2.vy = CRes.random_Am_0(5);
				point2.fRe = CRes.random(10, 14);
				VecSubEff.addElement(point2);
			}
		}
	}

	public void updateMorgan_1()
	{
		if (f < fRemove)
		{
			return;
		}
		if (!checkNullObject(1))
		{
			int num = -10;
			int num2 = 20;
			if (Dir == 0)
			{
				num2 = -20;
			}
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
			num2 = 13;
			if (Dir == 0)
			{
				num2 = -13;
			}
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
			num2 = 5;
			if (Dir == 0)
			{
				num2 = -5;
			}
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
		}
		if (!checkNullObject(2))
		{
			addVir(3, 5, 10, isPlayer: false);
			setAva(1, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
		}
		removeEff();
	}

	public void updateMorgan_2()
	{
		if (f < fRemove)
		{
			return;
		}
		if (!checkNullObject(1))
		{
			int num = -10;
			int num2 = 20;
			if (Dir == 0)
			{
				num2 = -20;
			}
			GameScreen.addEffectEnd(16, 0, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
			num2 = 15;
			num += 3;
			if (Dir == 0)
			{
				num2 = -13;
			}
			GameScreen.addEffectEnd(16, 0, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
			num2 = 10;
			num += 3;
			if (Dir == 0)
			{
				num2 = -5;
			}
			GameScreen.addEffectEnd(16, 0, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
		}
		if (!checkNullObject(2))
		{
			setAva(1, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
		}
		removeEff();
	}

	public void updateZoroS2_New()
	{
		int num = 5;
		if (f >= fRemove || checkNullObject(3))
		{
			if (isAddSound)
			{
				mSound.playSound(9, mSound.volumeSound);
			}
			if (!checkNullObject(2))
			{
				setAva(2, objBeFireMain);
				GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				_ = Dir;
				int subtype = 0;
				if (typeEffect == 184)
				{
					subtype = 2;
				}
				else if (typeEffect == 216)
				{
					subtype = 3;
				}
				GameScreen.addEffectEnd(26, subtype, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
				objFireMain.vx = 0;
				objFireMain.vy = 0;
				objFireMain.toX = objFireMain.x;
				objFireMain.toY = objFireMain.y;
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		if ((f > 12 && f < 20) || (f > 22 && f < 26) || (f > 28 && f < 32) || (f > 34 && f < 38))
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 17)
		{
			objFireMain.y = objBeFireMain.y;
			giatocFly = 8;
		}
		if (f < 20 && f >= 17)
		{
			objBeFireMain.dy += giatocFly;
			giatocFly /= 2;
		}
		if (f >= 20 && f < 26)
		{
			giatocFly = 0;
			objBeFireMain.dy = 20;
			objFireMain.dy = 15;
		}
		if (f == 20)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			int num2 = 2;
			if (Dir == 0)
			{
				num2 = -2;
			}
			objFireMain.x = toX - num2;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			int subtype2 = 1;
			if (typeEffect == 184 || typeEffect == 216)
			{
				subtype2 = -1;
				num2 = 10;
				if (Dir == 0)
				{
					num2 = -10;
				}
			}
			GameScreen.addEffectEnd(16, subtype2, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
			if (typeEffect == 216)
			{
				GameScreen.addEffectEnd(136, 0, objFireMain.x + num2, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
			}
		}
		if (f >= 26 && f < 32)
		{
			objBeFireMain.dy = 30;
			objFireMain.dy = 25;
		}
		if (f == 26)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			changeDir();
			objFireMain.Dir = Dir;
			int num3 = 30;
			if (Dir == 0)
			{
				num3 = -30;
			}
			objFireMain.x = toX - num3;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			int subtype3 = 2;
			if (typeEffect == 184 || typeEffect == 216)
			{
				subtype3 = -2;
				num3 = 15;
				if (Dir == 0)
				{
					num3 = -15;
				}
			}
			GameScreen.addEffectEnd(16, subtype3, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
		}
		if (f == 32)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			changeDir();
			objFireMain.Dir = Dir;
			int num4 = 20;
			if (Dir == 0)
			{
				num4 = -20;
			}
			objFireMain.x = toX - num4;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			objBeFireMain.dy = 40;
			objFireMain.dy = 35;
			GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			int subtype4 = 1;
			if (typeEffect == 184 || typeEffect == 216)
			{
				subtype4 = -1;
				num4 = 10;
				if (Dir == 0)
				{
					num4 = -10;
				}
			}
			GameScreen.addEffectEnd(16, subtype4, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
		}
		if (f == 38)
		{
			changeDir();
			objFireMain.Dir = Dir;
			Point_Focus point_Focus = new Point_Focus();
			int num5 = 20;
			if (Dir == 0)
			{
				num5 = -20;
			}
			int num6 = toX;
			int num7 = toY;
			toX = x;
			toY = y;
			x = num6;
			y = num7;
			int xdich = toX - (x - num5);
			int ydich = toY - y;
			objFireMain.x = x - num5;
			objFireMain.y = y;
			objFireMain.dy = 0;
			objBeFireMain.dy = 0;
			create_Speed(xdich, ydich, point_Focus);
			objFireMain.vx = point_Focus.vx;
			objFireMain.vy = -point_Focus.vy;
			objFireMain.toX = point_Focus.toX;
			objFireMain.toY = point_Focus.toY;
		}
		if (f > 38 && MainObject.getDistance(objFireMain.x, objFireMain.y, objFireMain.toX, objFireMain.toY) < vMax)
		{
			objFireMain.vx = 0;
			objFireMain.vy = 0;
			objFireMain.toX = objFireMain.x;
			objFireMain.toY = objFireMain.y;
		}
	}

	public void updateZoroS2_L6()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (isAddSound)
			{
				mSound.playSound(9, mSound.volumeSound);
			}
			if (!checkNullObject(2))
			{
				setAva(2, objBeFireMain);
				GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				_ = Dir;
				int num = 0;
				num = 4;
				GameScreen.addEffectEnd(26, num, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
				objFireMain.vx = 0;
				objFireMain.vy = 0;
				objFireMain.toX = objFireMain.x;
				objFireMain.toY = objFireMain.y;
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		if ((f > 12 && f < 20) || (f > 22 && f < 26) || (f > 28 && f < 32) || (f > 34 && f < 38))
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 17)
		{
			objFireMain.y = objBeFireMain.y;
			giatocFly = 8;
		}
		if (f < 20 && f >= 17)
		{
			objBeFireMain.dy += giatocFly;
			giatocFly /= 2;
		}
		if (f >= 20 && f < 26)
		{
			giatocFly = 0;
			objBeFireMain.dy = 20;
			objFireMain.dy = 15;
		}
		if (f == 10)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			int num2 = 20;
			if (Dir == 0)
			{
				num2 = -20;
			}
			objFireMain.x = toX - num2;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			createSkillZoro2(1, toX + CRes.random_Am_0(5), toY - 20, 2);
		}
		if (f >= 16 && f < 22)
		{
			objBeFireMain.dy = 30;
			objFireMain.dy = 25;
		}
		if (f == 16)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			changeDir();
			objFireMain.Dir = Dir;
			int num3 = 30;
			if (Dir == 0)
			{
				num3 = -30;
			}
			objFireMain.x = toX - num3;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			createSkillZoro2(0, toX + CRes.random_Am_0(5), toY - 20, 2);
		}
		if (f == 22)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			changeDir();
			objFireMain.Dir = Dir;
			int num4 = 20;
			if (Dir == 0)
			{
				num4 = -20;
			}
			objFireMain.x = toX - num4;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			objBeFireMain.dy = 40;
			objFireMain.dy = 35;
			GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			createSkillZoro2(1, toX + CRes.random_Am_0(5), toY - 20, 2);
		}
		if (f == 28)
		{
			changeDir();
			objFireMain.Dir = Dir;
			Point_Focus point_Focus = new Point_Focus();
			int num5 = 20;
			if (Dir == 0)
			{
				num5 = -20;
			}
			int num6 = toX;
			int num7 = toY;
			toX = x;
			toY = y;
			x = num6;
			y = num7;
			int xdich = toX - (x - num5);
			int ydich = toY - y;
			objFireMain.x = x - num5;
			objFireMain.y = y;
			objFireMain.dy = 0;
			objBeFireMain.dy = 0;
			create_Speed(xdich, ydich, point_Focus);
			objFireMain.vx = point_Focus.vx;
			objFireMain.vy = -point_Focus.vy;
			objFireMain.toX = point_Focus.toX;
			objFireMain.toY = point_Focus.toY;
		}
		if (f > 28 && MainObject.getDistance(objFireMain.x, objFireMain.y, objFireMain.toX, objFireMain.toY) < vMax)
		{
			objFireMain.vx = 0;
			objFireMain.vy = 0;
			objFireMain.toX = objFireMain.x;
			objFireMain.toY = objFireMain.y;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f >= point.fRe)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
	}

	private void addShadow_Zoro_S1_L6()
	{
		Point point = new Point(objFireMain.x, objFireMain.y + 2);
		point.frame = (f - 12) / 2;
		if (point.frame >= fraImgSubEff.nFrame)
		{
			point.frame = fraImgSubEff.nFrame - 1;
		}
		VecSubEff.addElement(point);
		if (Dir == 0)
		{
			objFireMain.vx = -objFireMain.vMax * 4;
		}
		else
		{
			objFireMain.vx = objFireMain.vMax * 4;
		}
	}

	public void updateZoroS2_New_SHORT()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(1))
			{
				int num = 30;
				if (Dir == 0)
				{
					num = -30;
				}
				int subtype = 0;
				if (typeEffect == 184)
				{
					subtype = 2;
				}
				GameScreen.addEffectEnd(26, subtype, objFireMain.x + num, objFireMain.y - 5, Dir, objMainEff);
				GameScreen.addEffectEnd(26, subtype, objBeFireMain.x, objBeFireMain.y, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 3, objFireMain.x + num, objFireMain.y - 35, Dir, objMainEff);
				objFireMain.vx = 0;
				objFireMain.vy = 0;
				objFireMain.toX = objFireMain.x;
				objFireMain.toY = objFireMain.y;
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		if ((f > 2 && f < 10) || (f > 12 && f < 16) || (f > 18 && f < 22))
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		int num2 = 5;
		if (f == 7)
		{
			objFireMain.y = objBeFireMain.y;
			giatocFly = 8;
		}
		if (f < 10 && f >= 7)
		{
			objBeFireMain.dy += giatocFly;
			giatocFly /= 2;
		}
		if (f >= 10 && f < 16)
		{
			giatocFly = 0;
			objBeFireMain.dy = 20;
			objFireMain.dy = 15;
		}
		if (f == 10)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			int num3 = 20;
			if (Dir == 0)
			{
				num3 = -20;
			}
			objFireMain.x = toX - num3;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num2, Dir, objMainEff);
		}
		if (f >= 16 && f < 22)
		{
			objBeFireMain.dy = 30;
			objFireMain.dy = 25;
		}
		if (f == 16)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			changeDir();
			objFireMain.Dir = Dir;
			int num4 = 30;
			if (Dir == 0)
			{
				num4 = -30;
			}
			objFireMain.x = toX - num4;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(16, 2, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num2, Dir, objMainEff);
		}
		if (f == 22)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			changeDir();
			objFireMain.Dir = Dir;
			int num5 = 20;
			if (Dir == 0)
			{
				num5 = -20;
			}
			objFireMain.x = toX - num5;
			objFireMain.y = toY + objBeFireMain.hOne / 2;
			setAva(0, objBeFireMain);
			objBeFireMain.dy = 40;
			objFireMain.dy = 35;
			GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num5, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num2, Dir, objMainEff);
			if (isAddSound)
			{
				mSound.playSound(9, mSound.volumeSound);
			}
			if (!checkNullObject(2))
			{
				setAva(2, objBeFireMain);
				GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
		}
		if (f > 23 && MainObject.getDistance(objFireMain.x, objFireMain.y, objFireMain.toX, objFireMain.toY) < vMax)
		{
			objFireMain.vx = 0;
			objFireMain.vy = 0;
			objFireMain.toX = objFireMain.x;
			objFireMain.toY = objFireMain.y;
		}
	}

	public void updateZoroS1_New()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(1))
			{
				objFireMain.vx = 0;
				objFireMain.dy = 0;
			}
			removeEff();
			return;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f == point_Focus.fRe + 1)
			{
				point_Focus.vx = 0;
				point_Focus.vy = 0;
				point_Focus.x = objBeFireMain.x;
				point_Focus.y = objBeFireMain.y;
			}
			if (point_Focus.f > point_Focus.fRe + 10)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		int num = 5;
		if (f <= fRemove)
		{
			if (f == 21)
			{
				giatocFly = 12;
			}
			if (f >= 21 && f <= 26)
			{
				objBeFireMain.dy += giatocFly;
				giatocFly -= 2;
			}
			if (f > 26)
			{
				giatocFly = 0;
				setAva(-1, objBeFireMain);
				objFireMain.y = objBeFireMain.y;
				objFireMain.vx = 0;
				objFireMain.dy = 40;
				objBeFireMain.dy = 45;
			}
			else if (f == 24)
			{
				setAva(-1, objBeFireMain);
				int num2 = objBeFireMain.x - 10;
				if (Dir == 0)
				{
					num2 = objBeFireMain.x + 10;
				}
				int num3 = num2 - objFireMain.x;
				objFireMain.vx = num3 / 4;
			}
			else if (f >= 22)
			{
				setAva(-1, objBeFireMain);
			}
		}
		if (f == 5 || f == 37)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			if (f == 5)
			{
				setAva(0, objBeFireMain);
			}
			int num4 = 20;
			if (Dir == 0)
			{
				num4 = -20;
			}
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
		}
		if (f == 10 || f == 42)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			if (f == 10)
			{
				setAva(0, objBeFireMain);
			}
			int num5 = 30;
			if (Dir == 0)
			{
				num5 = -30;
			}
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(16, 2, objFireMain.x + num5, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 + num, Dir, objMainEff);
		}
		if (f == 47)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			int num6 = 20;
			if (Dir == 0)
			{
				num6 = -20;
			}
			GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(16, 1, objFireMain.x + num6, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
		}
		if (f == 12)
		{
			GameScreen.addEffectEnd(30, 0, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, 300, Dir, objMainEff);
		}
		if (f == 22)
		{
			if (isAddSound)
			{
				mSound.playSound(8, mSound.volumeSound);
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			int num7 = 20;
			if (Dir == 0)
			{
				num7 = -20;
			}
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(19, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(16, 1, x + num7, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
			int xdich = objBeFireMain.x - x;
			int ydich = objBeFireMain.y - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			VecEff.addElement(p);
		}
	}

	public void updateZoro_S1_L3_SHORT()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(1))
			{
				objFireMain.vx = 0;
				objFireMain.dy = 0;
			}
			removeEff();
			return;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f == point_Focus.fRe + 1)
			{
				point_Focus.vx = 0;
				point_Focus.vy = 0;
				if (typeEffect == 183 || typeEffect == 215)
				{
					point_Focus.vy = -4;
				}
				point_Focus.x = objBeFireMain.x;
				point_Focus.y = objBeFireMain.y;
			}
			if (typeEffect == 183 || typeEffect == 215)
			{
				if (point_Focus.f > point_Focus.fRe + 7)
				{
					VecEff.removeElement(point_Focus);
					i--;
				}
			}
			else if (point_Focus.f > point_Focus.fRe + 5)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		int num = 5;
		if (f <= 14)
		{
			if (f == 1)
			{
				giatocFly = 12;
			}
			if (f >= 1 && f <= 6)
			{
				objBeFireMain.dy += giatocFly;
				giatocFly -= 2;
			}
			if (f > 6)
			{
				giatocFly = 0;
				setAva(-1, objBeFireMain);
				objFireMain.y = objBeFireMain.y;
				objFireMain.vx = 0;
				objFireMain.dy = 40;
				objBeFireMain.dy = 45;
			}
			else if (f == 4)
			{
				setAva(-1, objBeFireMain);
				int num2 = objBeFireMain.x - 10;
				if (Dir == 0)
				{
					num2 = objBeFireMain.x + 10;
				}
				int num3 = num2 - objFireMain.x;
				objFireMain.vx = num3 / 4;
			}
			else if (f >= 2)
			{
				setAva(-1, objBeFireMain);
			}
		}
		if (f == 8 || f == 12)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			setAva(1, objBeFireMain);
			int num4 = 20;
			if (Dir == 0)
			{
				num4 = -20;
			}
			int subtype = 1;
			if (typeEffect == 183)
			{
				subtype = -1;
			}
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(108, 2, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
			if (typeEffect == 215)
			{
				GameScreen.addEffectEnd(135, 0, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 5 + num, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(16, subtype, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
			}
		}
		if (f == 1)
		{
			int xdich = objBeFireMain.x - x;
			int ydich = objBeFireMain.y - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			VecEff.addElement(p);
		}
	}

	public void updateZoro_S1_L6()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(1))
			{
				objFireMain.vx = 0;
				objFireMain.dy = 0;
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f == point_Focus.fRe + 1)
			{
				point_Focus.vx = 0;
				point_Focus.vy = -4;
			}
			if (point_Focus.f > point_Focus.fRe + 7)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		int num = 5;
		if (f <= 14)
		{
			if (f == 1)
			{
				giatocFly = 12;
			}
			if (f >= 1 && f <= 6)
			{
				objBeFireMain.dy += giatocFly;
				giatocFly -= 2;
			}
			if (f > 6)
			{
				giatocFly = 0;
				setAva(-1, objBeFireMain);
				objFireMain.y = objBeFireMain.y;
				objFireMain.vx = 0;
				objFireMain.dy = 40;
				objBeFireMain.dy = 45;
			}
			else if (f == 4)
			{
				setAva(-1, objBeFireMain);
				int num2 = objBeFireMain.x - 10;
				if (Dir == 0)
				{
					num2 = objBeFireMain.x + 10;
				}
				_ = objFireMain.x;
			}
			else if (f >= 2)
			{
				setAva(-1, objBeFireMain);
			}
		}
		if (f == 8 || f == 12)
		{
			if (isAddSound)
			{
				mSound.playSound(7, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			setAva(1, objBeFireMain);
			int num3 = 20;
			if (Dir == 0)
			{
				num3 = -20;
			}
			GameScreen.addEffectEnd(10, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			GameScreen.addEffectEnd(108, 2, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 2 - 10 + num, Dir, objMainEff);
		}
		if (f == 1)
		{
			int xdich = 0;
			int ydich = objBeFireMain.y - y;
			Point_Focus p = new Point_Focus();
			p = create_Speed(xdich, ydich, p);
			p.x = objBeFireMain.x;
			p.y = objBeFireMain.y;
			VecEff.addElement(p);
			int num4 = ((Dir != 0) ? (-5) : 5);
			GameScreen.addEffectEnd(170, 0, objFireMain.x + num4, objFireMain.y + 22, Dir, objMainEff);
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			if (f > 8 + j * 4)
			{
				((Point_Focus)VecSubEff.elementAt(j)).update_Vx_Vy();
			}
		}
	}

	public void updateLuffyS3_New()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(1))
		{
			removeEff();
			return;
		}
		if (f == 5)
		{
			int num = 30;
			if (Dir == 2)
			{
				num = -30;
			}
			Point o = new Point(objFireMain.x + num, objFireMain.y);
			VecSubEff.addElement(o);
		}
		if (f == 10)
		{
			int num2 = -10;
			if (Dir == 2)
			{
				num2 = 10;
			}
			Point o2 = new Point(objFireMain.x + num2, objFireMain.y - 35);
			VecSubEff.addElement(o2);
		}
		if (f == 15)
		{
			int num3 = -10;
			if (Dir == 2)
			{
				num3 = 10;
			}
			Point o3 = new Point(objFireMain.x + num3, objFireMain.y + 35);
			VecSubEff.addElement(o3);
		}
		if ((f == 22 || f == 25) && isAddSound)
		{
			mSound.playSound(4, mSound.volumeSound);
		}
		if (f >= 18)
		{
			if (f % 3 == 0)
			{
				if (indexObjBefire < vecObjsBeFire.size())
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
					indexObjBefire++;
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							sbyte dir = 0;
							if (objFireMain.x < mainObject.x)
							{
								dir = 2;
							}
							int num4 = 12;
							if (Dir == 0)
							{
								num4 = -12;
							}
							int num5 = 2;
							if (typeEffect == 182)
							{
								num5 = 3;
							}
							GameScreen.addEffectEnd_ObjTo(13, num5, objFireMain.x + num4, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, mainObject.ID, mainObject.typeObject, dir, objMainEff);
							for (int i = 0; i < VecSubEff.size(); i++)
							{
								Point point = (Point)VecSubEff.elementAt(i);
								int num6 = -20;
								if (Dir == 2)
								{
									num6 = 20;
								}
								GameScreen.addEffectEnd_ObjTo(13, num5, point.x + num4 + num6, point.y - objFireMain.hOne / 2, mainObject.ID, mainObject.typeObject, dir, objMainEff);
							}
						}
					}
				}
				else
				{
					int num7 = 12;
					if (Dir == 0)
					{
						num7 = -12;
					}
					int num8 = 0;
					if (typeEffect == 182)
					{
						num8 = 3;
					}
					if (CRes.random(3) == 0)
					{
						int xTo = objFireMain.x + num7 + am_duong * 120 + CRes.random_Am_0(20);
						int yTo = objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10 - objFireMain.hOne / 2 + CRes.random_Am_0(80);
						GameScreen.addEffectEnd_ToX_ToY(13, (sbyte)num8, objFireMain.x + num7, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, xTo, yTo, Dir, objMainEff);
					}
					for (int j = 0; j < VecSubEff.size(); j++)
					{
						Point point2 = (Point)VecSubEff.elementAt(j);
						if (CRes.random(3) == 0)
						{
							int num9 = -20;
							if (Dir == 2)
							{
								num9 = 20;
							}
							int xTo2 = point2.x + num7 + num9 + am_duong * 120 + CRes.random_Am_0(20);
							int yTo2 = point2.y - objFireMain.hOne / 2 + CRes.random_Am_0(80);
							GameScreen.addEffectEnd_ToX_ToY(13, (sbyte)num8, point2.x + num7 + num9, point2.y - objFireMain.hOne / 2, xTo2, yTo2, Dir, objMainEff);
						}
					}
				}
			}
			addVir(15, 5, 10, isPlayer: true);
		}
		for (int k = 0; k < VecSubEff.size(); k++)
		{
			((Point)VecSubEff.elementAt(k)).f++;
		}
		for (int l = 0; l < VecEff.size(); l++)
		{
			Point point3 = (Point)VecEff.elementAt(l);
			point3.f++;
			if (point3.f >= 3)
			{
				VecEff.removeElement(point3);
				l--;
			}
		}
	}

	public void updateLuffyS3_L5()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(1))
		{
			removeEff();
			return;
		}
		if (f < 4)
		{
			objFireMain.vx = -(am_duong * 7);
			objFireMain.dy += 20 - f * 3;
		}
		else if (f < fRemove - 3)
		{
			objFireMain.dy = 60;
			objFireMain.vx = 0;
		}
		else
		{
			if (objFireMain.dy <= 10)
			{
				objFireMain.dy = 0;
			}
			if (objFireMain.dy != 0)
			{
				objFireMain.dy /= 3;
			}
		}
		if (f == 5)
		{
			int num = 40;
			if (Dir == 2)
			{
				num = -40;
			}
			Point o = new Point(objFireMain.x + num, objFireMain.y - objFireMain.dy);
			VecSubEff.addElement(o);
		}
		if (typeEffect == 273)
		{
			if (f == 10)
			{
				int num2 = 15;
				if (Dir == 2)
				{
					num2 = -15;
				}
				Point o2 = new Point(objFireMain.x + num2, objFireMain.y - objFireMain.dy - 45);
				VecSubEff.addElement(o2);
				Point o3 = new Point(objFireMain.x + num2, objFireMain.y - objFireMain.dy + 45);
				VecSubEff.addElement(o3);
			}
			if (f == 15)
			{
				int num3 = 40;
				if (Dir == 2)
				{
					num3 = -40;
				}
				Point o4 = new Point(objFireMain.x + num3, objFireMain.y - objFireMain.dy - 90);
				VecSubEff.addElement(o4);
				Point o5 = new Point(objFireMain.x + num3, objFireMain.y - objFireMain.dy + 90);
				VecSubEff.addElement(o5);
			}
		}
		else
		{
			if (f == 10)
			{
				int num4 = 15;
				if (Dir == 2)
				{
					num4 = -15;
				}
				Point o6 = new Point(objFireMain.x + num4, objFireMain.y - objFireMain.dy - 45);
				VecSubEff.addElement(o6);
			}
			if (f == 15)
			{
				int num5 = 15;
				if (Dir == 2)
				{
					num5 = -15;
				}
				Point o7 = new Point(objFireMain.x + num5, objFireMain.y - objFireMain.dy + 45);
				VecSubEff.addElement(o7);
			}
		}
		if ((f == 22 || f == 25) && isAddSound)
		{
			mSound.playSound(4, mSound.volumeSound);
		}
		if (f >= 18)
		{
			if (f % 3 == 0)
			{
				if (indexObjBefire < vecObjsBeFire.size())
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
					indexObjBefire++;
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							sbyte dir = 0;
							if (objFireMain.x < mainObject.x)
							{
								dir = 2;
							}
							int num6 = 12;
							if (Dir == 0)
							{
								num6 = -12;
							}
							int num7 = 4;
							if (typeEffect == 273)
							{
								num7 = 5;
							}
							GameScreen.addEffectEnd_ObjTo(13, num7, objFireMain.x + num6, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, mainObject.ID, mainObject.typeObject, dir, objMainEff);
							for (int i = 0; i < VecSubEff.size(); i++)
							{
								Point point = (Point)VecSubEff.elementAt(i);
								int num8 = -20;
								if (Dir == 2)
								{
									num8 = 20;
								}
								GameScreen.addEffectEnd_ObjTo(13, num7, point.x + num6 + num8, point.y - objFireMain.hOne / 2, mainObject.ID, mainObject.typeObject, dir, objMainEff);
							}
						}
					}
				}
				else if (!GameCanvas.lowGraphic)
				{
					int num9 = 12;
					if (Dir == 0)
					{
						num9 = -12;
					}
					int num10 = 4;
					if (typeEffect == 273)
					{
						num10 = 5;
					}
					if (CRes.random(3) == 0)
					{
						int xTo = objFireMain.x + num9 + am_duong * 120 + CRes.random_Am_0(20);
						int yTo = objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10 - objFireMain.hOne / 2 + CRes.random_Am_0(80);
						GameScreen.addEffectEnd_ToX_ToY(13, (sbyte)num10, objFireMain.x + num9, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, xTo, yTo, Dir, objMainEff);
					}
					for (int j = 0; j < VecSubEff.size(); j++)
					{
						Point point2 = (Point)VecSubEff.elementAt(j);
						if (CRes.random(3) == 0)
						{
							int num11 = -20;
							if (Dir == 2)
							{
								num11 = 20;
							}
							int xTo2 = point2.x + num9 + num11 + am_duong * 120 + CRes.random_Am_0(20);
							int yTo2 = point2.y - objFireMain.hOne / 2 + CRes.random_Am_0(80);
							GameScreen.addEffectEnd_ToX_ToY(13, (sbyte)num10, point2.x + num9 + num11, point2.y - objFireMain.hOne / 2, xTo2, yTo2, Dir, objMainEff);
						}
					}
				}
			}
			addVir(15, 5, 10, isPlayer: true);
		}
		for (int k = 0; k < VecSubEff.size(); k++)
		{
			((Point)VecSubEff.elementAt(k)).f++;
		}
		for (int l = 0; l < VecEff.size(); l++)
		{
			Point point3 = (Point)VecEff.elementAt(l);
			point3.f++;
			if (point3.f >= 3)
			{
				VecEff.removeElement(point3);
				l--;
			}
		}
	}

	public void updateLuffyS2_NEW()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			removeEff();
			if (objFireMain == GameScreen.player)
			{
				GameScreen.setIsMoveEff(ismove: true);
			}
			return;
		}
		if (f >= 12 && f <= 20)
		{
			objFireMain.isTanHinh = true;
			if (objFireMain == GameScreen.player)
			{
				Player.isSendMove = false;
			}
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 16)
		{
			int num = 220;
			if (Dir == 0)
			{
				num = -220;
			}
			objFireMain.x += num;
			x = objFireMain.x;
			Dir = (sbyte)((Dir == 0) ? 2 : 0);
			objFireMain.Dir = Dir;
		}
		if (f == 12)
		{
			setDy(-10, objBeFireMain);
			int num2 = 20;
			if (Dir == 0)
			{
				num2 = -20;
			}
			GameScreen.addEffectEnd(0, 0, objFireMain.x + num2, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
			if (isAddSound)
			{
				mSound.playSound(3, mSound.volumeSound);
			}
		}
		if (f == 29)
		{
			addVir(5, 5, 10, isPlayer: true);
			setAva(2, objBeFireMain);
			int num3 = 20;
			if (Dir == 0)
			{
				num3 = -20;
			}
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			GameScreen.addEffectEnd(0, 0, objFireMain.x + num3, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
			if (isAddSound)
			{
				mSound.playSound(3, mSound.volumeSound);
			}
		}
		if (f <= 20)
		{
			return;
		}
		if (f < 27)
		{
			if (Dir == 0)
			{
				objFireMain.vx = -objFireMain.vMax * 4;
			}
			else
			{
				objFireMain.vx = objFireMain.vMax * 4;
			}
			if (f % 2 == 0 || typeEffect == 35)
			{
				Point o = new Point(objFireMain.x - objFireMain.vx / 2, objFireMain.y);
				VecEff.addElement(o);
			}
		}
		else
		{
			if (objFireMain == GameScreen.player)
			{
				Player.isSendMove = true;
			}
			objFireMain.vx = 0;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f / 2 >= 3)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
	}

	public void updateLuffyS2_NEW_SHORT()
	{
		if ((f >= fRemove && (typeEffect != 213 || typeEffect == 272 || VecSubEff.size() == 0)) || checkNullObject(1))
		{
			removeEff();
			if (objFireMain == GameScreen.player)
			{
				GameScreen.setIsMoveEff(ismove: true);
			}
			return;
		}
		if (f >= 4 && f <= 11)
		{
			objFireMain.isTanHinh = true;
			if (objFireMain == GameScreen.player)
			{
				Player.isSendMove = false;
			}
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 7)
		{
			int num = 320;
			if (Dir == 0)
			{
				num = -320;
			}
			objFireMain.x += num;
			x = objFireMain.x;
			Dir = (sbyte)((Dir == 0) ? 2 : 0);
			objFireMain.Dir = Dir;
		}
		if (f == 3)
		{
			setDy(-10, objBeFireMain);
			int num2 = 20;
			if (Dir == 0)
			{
				num2 = -20;
			}
			GameScreen.addEffectEnd(0, 0, objFireMain.x + num2, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
			if (isAddSound)
			{
				mSound.playSound(3, mSound.volumeSound);
			}
		}
		if (f == 20)
		{
			addVir(5, 5, 10, isPlayer: true);
			setAva(2, objBeFireMain);
			int num3 = 20;
			if (Dir == 0)
			{
				num3 = -20;
			}
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(8, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
				int subtype = 5;
				if (typeEffect == 272)
				{
					subtype = 3;
				}
				GameScreen.addEffectEnd(108, subtype, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(0, 0, objFireMain.x + num3, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
				if (typeEffect == 181 || typeEffect == 213 || typeEffect == 272)
				{
					num3 = 10;
					if (Dir == 0)
					{
						num3 = -10;
					}
					int subtype2 = 0;
					if (typeEffect == 213)
					{
						subtype2 = 3;
					}
					else if (typeEffect == 272)
					{
						subtype2 = 4;
					}
					GameScreen.addEffectEnd(119, subtype2, objFireMain.x + num3, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
				}
			}
			if (isAddSound)
			{
				mSound.playSound(3, mSound.volumeSound);
			}
		}
		if (f == 22 && typeEffect == 272)
		{
			int num4 = 10;
			if (Dir == 0)
			{
				num4 = -10;
			}
			GameScreen.addEffectEnd(173, 0, objFireMain.x + num4, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
		}
		if (f <= 11)
		{
			return;
		}
		if (f < 18)
		{
			if (Dir == 0)
			{
				objFireMain.vx = -objFireMain.vMax * 4;
			}
			else
			{
				objFireMain.vx = objFireMain.vMax * 4;
			}
			if (f % 2 == 0 || typeEffect == 35)
			{
				Point o = new Point(objFireMain.x - objFireMain.vx / 2, objFireMain.y);
				VecEff.addElement(o);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(109, 0, objFireMain.x, objFireMain.y + 5, Dir, objMainEff);
			}
			if (typeEffect == 213 || typeEffect == 272)
			{
				Point point = new Point(objFireMain.x, objFireMain.y + 2);
				point.frame = (f - 12) / 2;
				if (point.frame >= fraImgSub3Eff.nFrame)
				{
					point.frame = fraImgSub3Eff.nFrame - 1;
				}
				VecSubEff.addElement(point);
			}
		}
		else
		{
			if (objFireMain == GameScreen.player)
			{
				Player.isSendMove = true;
			}
			objFireMain.vx = 0;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point2 = (Point)VecEff.elementAt(i);
			point2.f++;
			if (point2.f / 2 >= 3)
			{
				VecEff.removeElement(point2);
				i--;
			}
		}
		if (typeEffect != 213 && typeEffect != 272)
		{
			return;
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point obj = (Point)VecSubEff.elementAt(j);
			obj.f++;
			if (obj.f >= 5)
			{
				VecSubEff.removeElementAt(j);
				j--;
			}
		}
	}

	public void updateMon11()
	{
		if (!checkNullObject(1))
		{
			if (f < 2)
			{
				objFireMain.vx = vx1000;
			}
			else if (f < 5)
			{
				objFireMain.vx = -vx1000;
			}
			else
			{
				objFireMain.vx = 0;
			}
		}
		if (f == fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.vx = 0;
			}
			if (typeEffect == 144)
			{
				GameScreen.addEffectEnd(11, 0, toX + CRes.random_Am_0(5), toY + CRes.random_Am_0(10), Dir, objMainEff);
				setAva(0, objBeFireMain);
			}
			else
			{
				GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			}
			if (fRemove >= 4)
			{
				if (!checkNullObject(1))
				{
					objFireMain.vx = 0;
				}
				removeEff();
			}
		}
		if (fRemove < 4 && f == 4)
		{
			if (!checkNullObject(1))
			{
				objFireMain.vx = 0;
			}
			removeEff();
		}
	}

	public void updateMon10()
	{
		if (!checkNullObject(1))
		{
			if (f < 2)
			{
				objFireMain.vx = vx;
			}
			else
			{
				objFireMain.vx = -vx;
			}
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.vx = 0;
			}
			if (typeEffect == 149)
			{
				GameScreen.addEffectEnd(8, 0, toX + CRes.random_Am_0(5), toY + CRes.random_Am_0(10), Dir, objMainEff);
				setAva(0, objBeFireMain);
			}
			else if (typeEffect == 143)
			{
				GameScreen.addEffectEnd(11, 0, toX + CRes.random_Am_0(5), toY + CRes.random_Am_0(10), Dir, objMainEff);
				setAva(0, objBeFireMain);
			}
			else
			{
				GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			}
			removeEff();
		}
	}

	public void updateAlvida2()
	{
		if (f == 7)
		{
			addSound(14);
			int num = x;
			num = ((Dir != 0) ? (num + 15) : (num - 15));
			GameScreen.addEffectEnd(89, 0, num, y + 20, Dir, objMainEff);
		}
		if (f >= 7)
		{
			vy = 6;
		}
		if (f < fRemove)
		{
			return;
		}
		addVir(3, 5, 10, isPlayer: false);
		for (int i = 0; i < vecObjsBeFire.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
			if (object_Effect_Skill != null)
			{
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject != null)
				{
					GameScreen.addEffectEnd(52, 0, mainObject.x, mainObject.y + 10, Dir, objMainEff);
					GameScreen.addEffectEnd(8, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, objMainEff);
				}
			}
		}
		removeEff();
	}

	public void updateUssopS3_L5()
	{
		if (f == 1)
		{
			mframeSuper = new int[4][]
			{
				new int[3] { 34, -30, 1 },
				new int[3] { 67, -44, 1 },
				new int[3] { 100, -42, 2 },
				new int[3] { 126, -17, 1 }
			};
		}
		if (f == 10 && !checkNullObject(3))
		{
			int num = toX - x;
			int num2 = toY - objBeFireMain.hOne - y - 50;
			create_Speed(num, num2, null);
			int frameAngle = CRes.angle(num, num2);
			frame = setFrameAngle(frameAngle);
			fRemove += 10;
		}
		if (f == fRemove)
		{
			addVir(5, 5, 10, isPlayer: true);
			GameScreen.addEffectEnd(120, 0, x, y, Dir, objMainEff);
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd(93, 2, mainObject.x + CRes.random_Am_0(10), mainObject.y - mainObject.hOne + CRes.random_Am_0(10), Dir, objMainEff);
					}
				}
			}
			indexObjBefire = 0;
			vx = 0;
			vy = 0;
		}
		if (f <= fRemove || f % 2 != 1)
		{
			return;
		}
		if (!GameCanvas.lowGraphic)
		{
			for (int j = 0; j < 2; j++)
			{
				GameScreen.addEffectEnd(120, mframeSuper[indexObjBefire][2], x + mframeSuper[indexObjBefire][0] * ((j == 0) ? 1 : (-1)), y + mframeSuper[indexObjBefire][1], Dir, objMainEff);
			}
		}
		indexObjBefire++;
		if (indexObjBefire >= mframeSuper.Length)
		{
			removeEff();
		}
	}

	public void updateUssopS3_L6()
	{
		if (f == 1)
		{
			mframeSuper = new int[4][]
			{
				new int[3]
				{
					40,
					-60,
					CRes.random(2, 4)
				},
				new int[3]
				{
					80,
					-25,
					CRes.random(1, 3)
				},
				new int[3]
				{
					120,
					-60,
					CRes.random(2, 4)
				},
				new int[3]
				{
					160,
					-25,
					CRes.random(2, 4)
				}
			};
		}
		if (f == 10 && !checkNullObject(3))
		{
			int num = toX - x;
			int num2 = toY - objBeFireMain.hOne - y - 50;
			create_Speed(num, num2, null);
			int frameAngle = CRes.angle(num, num2);
			frame = setFrameAngle(frameAngle);
			fRemove += 10;
			vMax = 14;
			rocket1 = create_Speed(toX + 80 - x, num2, new Point_Focus());
			rocket2 = create_Speed(toX - 80 - x, num2, new Point_Focus());
			frameAngle = CRes.angle(toX + 80 - x, num2);
			frame1 = setFrameAngle(frameAngle);
			frameAngle = CRes.angle(toX - 80 - x, num2);
			frame2 = setFrameAngle(frameAngle);
		}
		if (f > 10 && f < fRemove)
		{
			rocket1.update_Vx_Vy();
			rocket2.update_Vx_Vy();
		}
		if (f == fRemove)
		{
			addVir(5, 5, 10, isPlayer: true);
			GameScreen.addEffectEnd(168, 1, x, y, Dir, objMainEff);
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd(93, 2, mainObject.x + CRes.random_Am_0(10), mainObject.y - mainObject.hOne + CRes.random_Am_0(10), Dir, objMainEff);
					}
				}
			}
			indexObjBefire = 0;
			vx = 0;
			vy = 0;
		}
		if (f <= fRemove || f % 2 != 1)
		{
			return;
		}
		if (!GameCanvas.lowGraphic)
		{
			for (int j = 0; j < 2; j++)
			{
				GameScreen.addEffectEnd(168, mframeSuper[indexObjBefire][2], x + mframeSuper[indexObjBefire][0] * ((j == 0) ? 1 : (-1)), y + mframeSuper[indexObjBefire][1], Dir, objMainEff);
			}
		}
		indexObjBefire++;
		if (indexObjBefire >= mframeSuper.Length)
		{
			removeEff();
		}
	}

	public void updateUssopSkill3()
	{
		if (f == 10 && !checkNullObject(3))
		{
			int num = toX - x;
			int num2 = toY - objBeFireMain.hOne - y - 30;
			create_Speed(num, num2, null);
			int frameAngle = CRes.angle(num, num2);
			frame = setFrameAngle(frameAngle);
			if (typeEffect != 69 && typeEffect != 194)
			{
				GameScreen.addEffectEnd(5, 0, x, y, Dir, objMainEff);
			}
			fRemove += 10;
		}
		if (f < fRemove)
		{
			return;
		}
		sbyte subtype = 0;
		if (typeEffect == 68)
		{
			subtype = 1;
		}
		else if (typeEffect == 69)
		{
			addVir(5, 5, 10, isPlayer: true);
			subtype = 2;
			GameScreen.addEffectEnd(48, 0, x - 30 + CRes.random_Am_0(10), y - 30 + CRes.random_Am_0(10), Dir, objMainEff);
			GameScreen.addEffectEnd(48, 0, x + 30 + CRes.random_Am_0(10), y - 30 + CRes.random_Am_0(10), Dir, objMainEff);
		}
		else if (typeEffect == 194)
		{
			addVir(5, 5, 10, isPlayer: true);
			subtype = 2;
			GameScreen.addEffectEnd(120, 0, x - 30 + CRes.random_Am_0(10), y - 30 + CRes.random_Am_0(10), Dir, objMainEff);
			GameScreen.addEffectEnd(120, 0, x + 30 + CRes.random_Am_0(10), y - 30 + CRes.random_Am_0(10), Dir, objMainEff);
			GameScreen.addEffectEnd(120, 0, x - 60 + CRes.random_Am_0(10), y + CRes.random_Am_0(10), Dir, objMainEff);
			GameScreen.addEffectEnd(120, 0, x + 60 + CRes.random_Am_0(10), y + CRes.random_Am_0(10), Dir, objMainEff);
		}
		GameScreen.addEffectEnd(48, subtype, x, y, Dir, objMainEff);
		for (int i = 0; i < vecObjsBeFire.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
			if (object_Effect_Skill == null)
			{
				continue;
			}
			MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
			if (mainObject == null)
			{
				continue;
			}
			if (typeEffect == 67)
			{
				if (isAddSound)
				{
					mSound.playSound(14, mSound.volumeSound);
				}
				setAva(0, mainObject);
				GameScreen.addEffectEnd(1, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, objMainEff);
			}
			else if (typeEffect == 68)
			{
				if (isAddSound)
				{
					mSound.playSound(14, mSound.volumeSound);
				}
				setAva(1, mainObject);
				GameScreen.addEffectEnd(1, 0, mainObject.x + CRes.random_Am_0(10), mainObject.y - mainObject.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
				GameScreen.addEffectEnd(1, 0, mainObject.x + CRes.random_Am_0(10), mainObject.y - mainObject.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			}
			else if (typeEffect == 69)
			{
				if (isAddSound)
				{
					mSound.playSound(15, mSound.volumeSound);
				}
				if (i == 0)
				{
					setAva(2, mainObject);
				}
				else
				{
					GameScreen.addEffectEnd_ObjTo(49, 0, x, y, mainObject.ID, mainObject.typeObject, Dir, objMainEff);
				}
			}
			GameScreen.addEffectEnd(93, 2, mainObject.x + CRes.random_Am_0(10), mainObject.y - mainObject.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
		}
		removeEff();
	}

	public void updateMohji_1()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		if ((f >= 3 && f <= 11) || (f >= 26 && f <= 30))
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 8)
		{
			int num = 20;
			if (Dir == 2)
			{
				num = -20;
			}
			objFireMain.x = toX + num;
			objFireMain.y = toY;
		}
		if (f == 12 || f == 16)
		{
			addSound(7);
		}
		if (f == 12)
		{
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(10), toY - 5 + CRes.random_Am_0(10), Dir, objMainEff);
		}
		if (f == 20)
		{
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(10), toY - 5 + CRes.random_Am_0(10), Dir, objMainEff);
			setAva(0, objBeFireMain);
		}
		if (f == 30)
		{
			objFireMain.x = x;
			objFireMain.y = y;
		}
	}

	public void updateMohji_2()
	{
		if (f == 2)
		{
			addSound(7);
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(10), toY + CRes.random_Am_0(10), Dir, objMainEff);
			setAva(0, objBeFireMain);
		}
		if (f == 6)
		{
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(10), toY + CRes.random_Am_0(10), Dir, objMainEff);
			setAva(0, objBeFireMain);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateBuggy_1()
	{
		if (f == 2)
		{
			addSound(19);
			if (!checkNullObject(1) && objFireMain.plashNow != null)
			{
				objFireMain.plashNow.setIsNextf(1);
			}
			Point_Focus point_Focus = new Point_Focus();
			int xdich = toX - x;
			int ydich = toY - y;
			point_Focus.Dir = Dir;
			point_Focus.frame = 1;
			create_Speed(xdich, ydich, point_Focus);
			VecEff.addElement(point_Focus);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(i);
			point_Focus2.update_Vx_Vy();
			if (point_Focus2.f >= point_Focus2.fRe)
			{
				if (point_Focus2.frame == 1)
				{
					addSound(7);
					Point_Focus point_Focus3 = new Point_Focus();
					GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
					setAva(1, objBeFireMain);
					int num = x;
					int num2 = y;
					x = toX;
					y = toY;
					toX = num;
					toY = num2;
					int xdich2 = toX - x;
					int ydich2 = toY - y;
					point_Focus3.Dir = Dir;
					point_Focus3.frame = 2;
					create_Speed(xdich2, ydich2, point_Focus3);
					VecEff.addElement(point_Focus3);
				}
				VecEff.removeElement(point_Focus2);
				i--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			if (!checkNullObject(1) && objFireMain.plashNow != null)
			{
				objFireMain.plashNow.setIsNextf(0);
			}
			removeEff();
		}
	}

	public void updateBuggy_2()
	{
		if (f == 18)
		{
			GameScreen.addEffectEnd(30, 0, x1000, y1000, 300, Dir, objMainEff);
		}
		if (f == 28)
		{
			addSound(15);
			addVir(2, 6, 10, isPlayer: false);
			Point_Focus p = new Point_Focus();
			int xdich = -260;
			if (Dir == 2)
			{
				xdich = 260;
			}
			p = create_Speed(xdich, 0, p);
			p.y = y1000;
			VecEff.addElement(p);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f <= 28)
		{
			return;
		}
		addSound(19);
		if (f % 2 != 0 || indexObjBefire >= vecObjsBeFire.size())
		{
			return;
		}
		Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
		indexObjBefire++;
		if (object_Effect_Skill != null)
		{
			MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
			if (mainObject != null)
			{
				setAva(1, mainObject);
				GameScreen.addEffectEnd(48, 1, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, objMainEff);
			}
		}
	}

	public void updateCabaji_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(1, 0, point_Focus.objMain.x + CRes.random_Am_0(5), point_Focus.objMain.y - point_Focus.objMain.hOne / 2 + CRes.random_Am_0(5), Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 2)
		{
			addSound(18);
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus p = new Point_Focus();
						int xdich = mainObject.x - x;
						int ydich = mainObject.y - mainObject.hOne / 2 - y;
						p = create_Speed(xdich, ydich, p);
						p.objMain = mainObject;
						p.frame = CRes.random(2);
						VecEff.addElement(p);
					}
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateDonKrieg_3()
	{
		if (f == 18)
		{
			addSound(15);
			int num = -45;
			if (Dir == 2)
			{
				num = 45;
			}
			GameScreen.addEffectEnd(57, 0, x + num, y + 12, Dir, objMainEff);
		}
		if (f > 18 && f < 28)
		{
			if (f == 20 || f == 26)
			{
				addSound(14);
			}
			if (f % 2 == 1)
			{
				int num2 = -40 - ((f - 18) / 2 + 1) * 30;
				if (Dir == 2)
				{
					num2 = 40 + ((f - 18) / 2 + 1) * 30;
				}
				GameScreen.addEffectEnd(58, 0, x + num2, y + 30, Dir, objMainEff);
				GameScreen.addEffectEnd(59, 0, x + num2, y + 30, Dir, objMainEff);
				addVir(2, 5, 10, isPlayer: false);
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateDonKrieg_2()
	{
		if (f == 2)
		{
			xArchor += x1000;
		}
		if (f == 10)
		{
			addSound(32);
			if (!checkNullObject(2))
			{
				addVir(3, 5, 10, isPlayer: false);
				Point_Focus p = new Point_Focus();
				int xdich = objBeFireMain.x - x;
				int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - y;
				p = create_Speed(xdich, ydich, p);
				GameScreen.addEffectEnd(12, 1, x, y, Dir, objMainEff);
				VecEff.addElement(p);
			}
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f < point_Focus.fRe)
			{
				continue;
			}
			x = point_Focus.x;
			y = point_Focus.y;
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						vMax = 8 + CRes.random(5);
						Point_Focus p2 = new Point_Focus();
						int xdich2 = mainObject.x - x;
						int ydich2 = mainObject.y - mainObject.hOne / 2 - y;
						p2 = create_Speed(xdich2, ydich2, p2);
						p2.objMain = mainObject;
						VecSubEff.addElement(p2);
					}
				}
			}
			GameScreen.addEffectEnd(57, 0, x, y, Dir, objMainEff);
			if (VecSubEff.size() < 8)
			{
				for (int k = 0; k < 8 - VecEff.size(); k++)
				{
					vMax = 8 + CRes.random(5);
					Point_Focus p3 = new Point_Focus();
					int xdich3 = CRes.random_Am_0(120);
					int ydich3 = CRes.random_Am_0(50);
					p3 = create_Speed(xdich3, ydich3, p3);
					VecSubEff.addElement(p3);
				}
			}
			VecEff.removeElement(point_Focus);
			i--;
		}
		for (int l = 0; l < VecSubEff.size(); l++)
		{
			Point_Focus point_Focus2 = (Point_Focus)VecSubEff.elementAt(l);
			point_Focus2.update_Vx_Vy();
			if (point_Focus2.f == point_Focus2.fRe && point_Focus2.objMain != null)
			{
				GameScreen.addEffectEnd(1, 0, point_Focus2.x + CRes.random_Am_0(5), point_Focus2.y + CRes.random_Am_0(5), Dir, objMainEff);
				setAva(0, point_Focus2.objMain);
			}
			if (point_Focus2.f > point_Focus2.fRe + 8)
			{
				VecSubEff.removeElement(point_Focus2);
				l--;
			}
		}
		if (f >= fRemove && VecSubEff.size() == 0 && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateDonKrieg_1()
	{
		if (f == 2)
		{
			x += x1000;
			x1000 = x;
			y1000 = y;
		}
		if (f == 10)
		{
			addSound(32);
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill == null)
				{
					continue;
				}
				MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
				if (mainObject == null)
				{
					continue;
				}
				for (int j = 0; j < 2; j++)
				{
					Point_Focus p = new Point_Focus();
					if (Dir == 0)
					{
						x += CRes.random(10);
					}
					else
					{
						x -= CRes.random(10);
					}
					y += CRes.random_Am_0(25);
					int num = mainObject.x - x;
					int num2 = mainObject.y - mainObject.hOne / 2 - y;
					p = create_Speed(num, num2, p);
					int frameAngle = CRes.angle(num, num2);
					p.frame = setFrameAngle(frameAngle);
					GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
					if (j == 0)
					{
						p.objMain = mainObject;
					}
					VecEff.addElement(p);
					x = x1000;
					y = y1000;
				}
			}
			if (VecEff.size() < 8)
			{
				for (int k = 0; k < 8 - VecEff.size(); k++)
				{
					Point_Focus p2 = new Point_Focus();
					if (Dir == 0)
					{
						x += CRes.random(10);
					}
					else
					{
						x -= CRes.random(10);
					}
					y += CRes.random_Am_0(25);
					int num3 = 120 + CRes.random_Am_0(30);
					int num4 = CRes.random_Am_0(50);
					if (Dir == 0)
					{
						num3 = -num3;
					}
					p2 = create_Speed(num3, num4, p2);
					int frameAngle2 = CRes.angle(num3, num4);
					p2.frame = setFrameAngle(frameAngle2);
					GameScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
					VecEff.addElement(p2);
					x = x1000;
					y = y1000;
				}
			}
		}
		for (int l = 0; l < VecEff.size(); l++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(l);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f == point_Focus.fRe && point_Focus.objMain != null)
			{
				GameScreen.addEffectEnd(1, 0, point_Focus.x + CRes.random_Am_0(5), point_Focus.y + CRes.random_Am_0(5), Dir, objMainEff);
				setAva(0, point_Focus.objMain);
			}
			if (point_Focus.f > point_Focus.fRe + 10)
			{
				VecEff.removeElement(point_Focus);
				l--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateKuro_2()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(1))
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
		if (f == 10 || f == 24)
		{
			addSound(10);
		}
		if (f == 32 || f == 18)
		{
			addSound(7);
		}
		if (f <= 8)
		{
			if (f == 0 || f == 4)
			{
				objFireMain.x = x + 3;
			}
			if (f == 2 || f == 8)
			{
				objFireMain.x = x - 3;
			}
		}
		else if (f < fRemove)
		{
			if (f == 10)
			{
				createSkillKuro(0, toX + CRes.random_Am_0(30), toY - 10 + CRes.random_Am_0(30), CRes.random(2, 5));
				setAva(0, objBeFireMain);
			}
			else if (f % 4 == 0)
			{
				createSkillKuro(CRes.random(4), toX + CRes.random_Am_0(30), toY - 10 + CRes.random_Am_0(30), CRes.random(2, 5));
				setAva(0, objBeFireMain);
			}
			if (objFireMain.isTanHinh)
			{
				if (CRes.random(5) == 0)
				{
					objFireMain.isTanHinh = false;
				}
			}
			else if (CRes.random(3) == 0)
			{
				objFireMain.isTanHinh = true;
				objFireMain.x = toX + CRes.random_Am_0(30);
				objFireMain.y = toY + CRes.random_Am_0(30);
			}
			if (CRes.random(5) == 0)
			{
				Point point = new Point();
				point.x = toX + CRes.random_Am_0(30);
				point.y = toY + CRes.random_Am_0(30);
				point.frame = 4;
				point.fRe = 3;
				point.dis = ((CRes.random(2) != 0) ? 2 : 0);
				VecEff.addElement(point);
				addVir(3, 5, 10, isPlayer: false);
			}
		}
		if (f == fRemove - 2)
		{
			Point point2 = new Point();
			point2.x = x1000;
			point2.y = y1000;
			point2.frame = 4;
			point2.fRe = 2;
			point2.dis = Dir;
			VecEff.addElement(point2);
		}
		if (f == fRemove)
		{
			objFireMain.isTanHinh = false;
			objFireMain.x = x1000;
			objFireMain.y = y1000;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point3 = (Point)VecEff.elementAt(i);
			point3.update();
			if (point3.f >= point3.fRe)
			{
				VecEff.removeElement(point3);
				i--;
			}
		}
	}

	public void createSkillKuro(int type, int x, int y, int size)
	{
		switch (type)
		{
		case 0:
		{
			for (int l = 0; l < size; l++)
			{
				Point point4 = new Point();
				point4.y = y;
				if (Dir == 2)
				{
					point4.x = x + 7 * l;
				}
				else
				{
					point4.x = x - 7 * l;
				}
				point4.vy = -7;
				point4.frame = 2;
				point4.fRe = 5;
				point4.dis = ((CRes.random(2) != 0) ? 2 : 0);
				VecEff.addElement(point4);
			}
			break;
		}
		case 1:
		{
			for (int j = 0; j < size; j++)
			{
				Point point2 = new Point();
				point2.y = y + j * 7;
				point2.x = x;
				point2.vx = -5;
				point2.frame = 3;
				point2.fRe = 5;
				point2.dis = ((CRes.random(2) != 0) ? 2 : 0);
				VecEff.addElement(point2);
			}
			break;
		}
		case 2:
		{
			for (int k = 0; k < size; k++)
			{
				Point point3 = new Point();
				point3.y = y + k * 7;
				point3.x = x;
				point3.vx = -3;
				if (Dir == 0)
				{
					point3.vx = 3;
				}
				point3.frame = 0;
				point3.fRe = 4;
				point3.dis = ((CRes.random(2) != 0) ? 2 : 0);
				VecEff.addElement(point3);
			}
			break;
		}
		case 3:
		{
			for (int i = 0; i < size; i++)
			{
				Point point = new Point();
				point.y = y + i * 7;
				point.x = x;
				point.vx = -3;
				if (Dir == 0)
				{
					point.vx = 3;
				}
				point.frame = 1;
				point.fRe = 4;
				point.dis = ((CRes.random(2) != 0) ? 2 : 0);
				VecEff.addElement(point);
			}
			break;
		}
		}
	}

	public void createSkillZoro2(int type, int x, int y, int size)
	{
		switch (type)
		{
		case 0:
		{
			for (int j = 0; j < size; j++)
			{
				Point point2 = new Point();
				point2.y = y;
				if (Dir == 2)
				{
					point2.x = x + 15 * j;
				}
				else
				{
					point2.x = x - 15 * j;
				}
				point2.vy = -7;
				point2.frame = 2;
				point2.fRe = 4;
				point2.dis = ((CRes.random(2) != 0) ? 2 : 0);
				VecEff.addElement(point2);
			}
			break;
		}
		case 1:
		{
			for (int i = 0; i < size; i++)
			{
				Point point = new Point();
				point.y = y + i * 15;
				point.x = x;
				point.vx = -5;
				point.frame = 3;
				point.fRe = 4;
				point.dis = ((CRes.random(2) != 0) ? 2 : 0);
				VecEff.addElement(point);
			}
			break;
		}
		}
	}

	public void updateKuro_1()
	{
		if (f == 2)
		{
			int num = 14;
			if (Dir == 2)
			{
				num = -14;
			}
			x = toX + num;
			y = toY - objFireMain.hOne / 2;
			objFireMain.x = x;
			objFireMain.y = toY;
		}
		if (f == 4)
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 5)
		{
			int num2 = -14;
			if (Dir == 2)
			{
				num2 = 14;
			}
			x += num2;
			for (int i = 0; i < 3; i++)
			{
				Point point = new Point();
				point.y = y;
				if (Dir == 2)
				{
					point.x = x + 7 * i;
				}
				else
				{
					point.x = x - 7 * i;
				}
				point.vy = -10;
				point.frame = 2;
				point.fRe = 5;
				VecEff.addElement(point);
			}
		}
		if (f > 5)
		{
			if (f < 11)
			{
				objFireMain.dy = 10 * (f - 6);
				objBeFireMain.dy = 12 * (f - 6);
			}
			else if (f < 15)
			{
				objFireMain.dy = 50;
				objBeFireMain.dy = 60;
				objFireMain.vx = -5;
				if (Dir == 0)
				{
					objFireMain.vx = 5;
				}
			}
		}
		if (f == 8)
		{
			addSound(7);
			setAva(0, objBeFireMain);
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.update();
			if (point2.f >= point2.fRe)
			{
				VecEff.removeElement(point2);
				j--;
			}
		}
		if (f == 13)
		{
			addSound(7);
			setAva(1, objBeFireMain);
			for (int k = 0; k < 3; k++)
			{
				Point point3 = new Point();
				point3.y = y - objFireMain.dy + k * 7;
				point3.x = x;
				point3.vx = -3;
				if (Dir == 0)
				{
					point3.vx = 3;
				}
				point3.frame = 0;
				point3.fRe = 4;
				VecEff.addElement(point3);
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			addVir(3, 5, 10, isPlayer: false);
			objFireMain.dy = 0;
			removeEff();
		}
	}

	public void updateNyaban_2()
	{
		if (f > fRemove || checkNullObject(1))
		{
			if (!checkNullObject(1))
			{
				objFireMain.toX = x;
				objFireMain.isTanHinh = false;
				objFireMain.dy = 0;
			}
			setAva(0, objBeFireMain);
			removeEff();
			return;
		}
		if (f == 1)
		{
			addSound(3);
		}
		if (f < 5)
		{
			objFireMain.dy = 40 * f;
			objFireMain.vx = vx;
		}
		else if (f < 8)
		{
			objFireMain.isTanHinh = true;
		}
		if (f == 5)
		{
			objFireMain.x = toX;
			objFireMain.vx = 0;
			objFireMain.dy = 100;
		}
		if (f == 6)
		{
			addSound(14);
			addVir(2, 5, 10, isPlayer: false);
			objFireMain.dy = 0;
			setAva(1, objBeFireMain);
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(5), objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(5), Dir, objMainEff);
			GameScreen.addEffectEnd(90, 0, toX, objBeFireMain.y + 10, Dir, objMainEff);
		}
	}

	public void updateNyaban_3()
	{
		if (f > fRemove || checkNullObject(1))
		{
			if (!checkNullObject(1))
			{
				objFireMain.toX = x;
				objFireMain.dy = 0;
				objFireMain.vx = 0;
			}
			removeEff();
		}
		if (f < 3)
		{
			objFireMain.dy = 10 * f;
			objFireMain.vx = vx;
		}
		else if (f < 6)
		{
			objFireMain.dy = 10 * (6 - f);
			objFireMain.vx = vx;
		}
		if (f == 6)
		{
			objFireMain.dy = 0;
			objFireMain.vx = 0;
		}
		if (f == 17)
		{
			objFireMain.Dir = ((objFireMain.Dir == 0) ? 2 : 0);
			vx = 20;
			if (objFireMain.Dir == 0)
			{
				vx = -20;
			}
			setAva(0, objBeFireMain);
		}
		if (f == 8 || f == 13)
		{
			addSound(7);
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(5), objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(5), Dir, objMainEff);
		}
		if (f > 17)
		{
			if (f < 22)
			{
				objFireMain.dy = 5 * (f - 17);
				objFireMain.vx = vx;
			}
			else if (f < 26)
			{
				objFireMain.dy = 5 * (25 - f);
				objFireMain.vx = vx;
			}
		}
	}

	public void updateJango_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			Point point = new Point(point_Focus.x, point_Focus.y);
			point.frame = CRes.random(fraImgSubEff.nFrame);
			VecSubEff.addElement(point);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(1, 0, point_Focus.objMain.x + CRes.random_Am_0(5), point_Focus.objMain.y - point_Focus.objMain.hOne / 2 + CRes.random_Am_0(5), Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point point2 = (Point)VecSubEff.elementAt(j);
			point2.f++;
			if (point2.f >= 2)
			{
				VecSubEff.removeElement(point2);
				j--;
			}
		}
		if (f == 2)
		{
			addSound(18);
			for (int k = 0; k < vecObjsBeFire.size(); k++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(k);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus p = new Point_Focus();
						int xdich = mainObject.x - x;
						int ydich = mainObject.y - mainObject.hOne / 2 - y;
						p = create_Speed(xdich, ydich, p);
						p.objMain = mainObject;
						VecEff.addElement(p);
					}
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateCabaji_2()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			if (!checkNullObject(1))
			{
				objFireMain.toX = x;
			}
			removeEff();
			return;
		}
		if (f == 1)
		{
			addSound(3);
		}
		if (f < 5)
		{
			objFireMain.dy = 70 * f;
		}
		else if (f >= 5 && f <= 10)
		{
			objFireMain.dy = 330;
		}
		else if (f <= 13)
		{
			objFireMain.dy = (13 - f) * 110;
		}
		if (f == 10)
		{
			objFireMain.x = toX;
			objFireMain.y = toY;
		}
		if (f == 13)
		{
			addSound(15);
			addVir(3, 5, 10, isPlayer: false);
			objFireMain.dy = 0;
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(5), objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(5), Dir, objMainEff);
			}
			GameScreen.addEffectEnd(9, 0, toX, toY, Dir, objMainEff);
			setAva(2, objBeFireMain);
			if (typeEffect == 22)
			{
				GameScreen.addEffectEnd(45, 0, toX, toY + 20, Dir, objMainEff);
			}
		}
	}

	public void updateArlong_3()
	{
		if (f == 12)
		{
			addSound(15);
			if (vecObjsBeFire.size() > 1)
			{
				for (int i = 0; i < vecObjsBeFire.size(); i++)
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							setAva(1, mainObject);
						}
					}
				}
			}
			else
			{
				setAva(1, objBeFireMain);
			}
			GameScreen.addEffectEnd(8, 0, toX, toY, Dir, objMainEff);
			Point point = new Point(x + plusxy[4][0], y + 30);
			point.vx = -10;
			if (Dir == 2)
			{
				point.vx = 10;
			}
			point.fRe = 12;
			VecEff.addElement(point);
		}
		if (f == 18 || f == 22)
		{
			addSound(14);
		}
		if (f == 13)
		{
			addVir(1, 6, 12, isPlayer: false);
			int num = -10;
			if (Dir == 2)
			{
				num = 10;
			}
			GameScreen.addEffectEnd_ToX_ToY(62, 0, x + plusxy[4][0], y + 30, x + plusxy[4][0] + num * 12, y + 30, Dir, objMainEff);
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.update();
			if (point2.f < point2.fRe - 2)
			{
				point2.frame = CRes.random(2);
			}
			else
			{
				point2.frame = 2;
			}
			if (f % 3 == 0)
			{
				GameScreen.addEffectEnd(59, 0, point2.x, point2.y, Dir, objMainEff);
			}
			if (point2.f >= point2.fRe)
			{
				VecEff.removeElement(point2);
				j--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateArlong_2()
	{
		if ((f >= fRemove && VecEff.size() == 0) || checkNullObject(3))
		{
			if (!checkNullObject(1) && objFireMain.plashNow != null)
			{
				objFireMain.plashNow.setIsNextf(0);
			}
			removeEff();
		}
		if (f == 2 || f == 12 || f == 22)
		{
			addSound(19);
		}
		if (f == 2)
		{
			addVir(3, 5, 10, isPlayer: false);
			objFireMain.isTanHinh = true;
			if (objFireMain.plashNow != null)
			{
				objFireMain.plashNow.setIsNextf(1);
			}
			Point_Focus point_Focus = new Point_Focus();
			int xdich = objBeFireMain.x - x;
			int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - y;
			point_Focus.objMain = objBeFireMain;
			point_Focus = create_Speed(xdich, ydich, point_Focus);
			point_Focus.frame = 0;
			point_Focus.dis = Dir;
			VecEff.addElement(point_Focus);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(i);
			point_Focus2.update_Vx_Vy();
			if (point_Focus2.f >= point_Focus2.fRe)
			{
				if (point_Focus2.frame == 2)
				{
					objFireMain.isTanHinh = false;
					if (objFireMain.plashNow != null)
					{
						objFireMain.plashNow.setIsNextf(0);
					}
					VecEff.removeElement(point_Focus2);
					i--;
				}
				else if (point_Focus2.f == point_Focus2.fRe)
				{
					GameScreen.addEffectEnd(8, 0, toX, toY, Dir, objMainEff);
					setAva(1, objBeFireMain);
				}
			}
			if (point_Focus2.frame == 0 && point_Focus2.f >= 8)
			{
				Point_Focus point_Focus3 = new Point_Focus();
				int xdich2 = objBeFireMain.x - point_Focus2.x;
				int ydich2 = objBeFireMain.y - objBeFireMain.hOne / 2 - point_Focus2.y;
				point_Focus3.objMain = objBeFireMain;
				point_Focus3 = create_Speed(xdich2, ydich2, point_Focus3, point_Focus2.x, point_Focus2.y, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2);
				point_Focus3.frame = 1;
				point_Focus3.dis = ((Dir == 0) ? 2 : 0);
				VecEff.addElement(point_Focus3);
				VecEff.removeElement(point_Focus2);
				i--;
			}
			else if (point_Focus2.f >= 22 && point_Focus2.frame == 1)
			{
				vMax = 20;
				Point_Focus p = new Point_Focus();
				int xdich3 = x - point_Focus2.x;
				int ydich3 = y - point_Focus2.y;
				p = create_Speed(xdich3, ydich3, p, point_Focus2.x, point_Focus2.y, x, y);
				p.frame = 2;
				p.dis = Dir;
				VecEff.addElement(p);
				VecEff.removeElement(point_Focus2);
				i--;
			}
		}
	}

	public void updateArlong_1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			((Point)VecEff.elementAt(i)).f++;
		}
		if (f == 6)
		{
			addSound(33);
			addVir(3, 5, 10, isPlayer: false);
			if (vecObjsBeFire.size() > 1)
			{
				for (int j = 0; j < vecObjsBeFire.size(); j++)
				{
					Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
					if (object_Effect_Skill != null)
					{
						MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
						if (mainObject != null)
						{
							GameScreen.addEffectEnd(8, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, objMainEff);
							setAva(1, mainObject);
						}
					}
				}
			}
			else
			{
				GameScreen.addEffectEnd(8, 0, toX, toY, Dir, objMainEff);
				setAva(1, objBeFireMain);
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateKurobi_2()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			removeEff();
			return;
		}
		if (f >= 10 && f <= 16)
		{
			if (f < 13)
			{
				objFireMain.dy = 10 * (f - 10);
				objFireMain.vx = vx;
			}
			else if (f < 16)
			{
				objFireMain.dy = 10 * (16 - f);
				objFireMain.vx = vx;
			}
			if (f == 16)
			{
				if (!checkNullObject(2))
				{
					GameScreen.addEffectEnd(25, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				}
				objFireMain.dy = 0;
				objFireMain.vx = 0;
				setAva(1, objBeFireMain);
			}
		}
		if (f == 18)
		{
			GameScreen.addEffectEnd(30, 0, x, y + 10, 200, Dir, objMainEff);
		}
		if (f == 26)
		{
			addSound(5);
			if (!checkNullObject(2))
			{
				addVir(2, 5, 10, isPlayer: false);
				GameScreen.addEffectEnd(25, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 3 * 2 + 5, Dir, objMainEff);
			}
			objFireMain.dy = 0;
			objFireMain.vx = 0;
			setAva(1, objBeFireMain);
		}
	}

	public void updateKurobi_1()
	{
		if (f == 12 || f == 27)
		{
			addSound(13);
			if (!checkNullObject(2))
			{
				addVir(2, 5, 10, isPlayer: false);
				GameScreen.addEffectEnd(25, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				setAva(0, objBeFireMain);
			}
		}
		if (f == 15)
		{
			GameScreen.addEffectEnd(30, 0, x, y, 300, Dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateChu_2()
	{
		if (f >= 10 && f < fRemove && f % 4 == 0)
		{
			if (f % 8 == 0 && indexObjBefire < vecObjsBeFire.size())
			{
				addSound(21);
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
				indexObjBefire++;
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						Point_Focus point_Focus = new Point_Focus();
						int xdich = mainObject.x - x;
						int ydich = mainObject.y - mainObject.hOne / 2 - y;
						point_Focus.objMain = mainObject;
						point_Focus = create_Speed(xdich, ydich, point_Focus);
						VecEff.addElement(point_Focus);
					}
				}
				addVir(3, 5, 10, isPlayer: false);
			}
			else
			{
				for (int i = 0; i < 2; i++)
				{
					Point_Focus p = new Point_Focus();
					int num = 120 + CRes.random_Am_0(30);
					int ydich2 = CRes.random_Am_0(50);
					if (Dir == 0)
					{
						num = -num;
					}
					p = create_Speed(num, ydich2, p);
					VecEff.addElement(p);
				}
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(j);
			point_Focus2.update_Vx_Vy();
			if (point_Focus2.f >= point_Focus2.fRe)
			{
				GameScreen.addEffectEnd(61, 0, point_Focus2.x, point_Focus2.y, Dir, objMainEff);
				if (point_Focus2.objMain != null)
				{
					setAva(0, point_Focus2.objMain);
				}
				VecEff.removeElement(point_Focus2);
				j--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateChu_1()
	{
		if (f == 10 || f == 14 || f == 18)
		{
			addSound(21);
			if (!checkNullObject(2))
			{
				Point_Focus p = new Point_Focus();
				int xdich = objBeFireMain.x - x;
				int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - y;
				p = create_Speed(xdich, ydich, p);
				VecEff.addElement(p);
			}
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(61, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				if (CRes.random(3) == 0)
				{
					setAva(0, objBeFireMain);
				}
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateHachi_2()
	{
		if (f == fRemove - 4 && !checkNullObject(2))
		{
			Point_Focus p = new Point_Focus();
			int xdich = objBeFireMain.x - x;
			int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - y;
			p = create_Speed(xdich, ydich, p);
			VecEff.addElement(p);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			sbyte b = 0;
			if (typeEffect == 150)
			{
				b = 1;
			}
			else if (typeEffect != 113)
			{
				b = 2;
			}
			if (point_Focus.f >= point_Focus.fRe)
			{
				if (b < 2)
				{
					GameScreen.addEffectEnd(60, b, point_Focus.x, point_Focus.y, Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd(34, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
					GameScreen.addEffectEnd(87, frame, point_Focus.x, point_Focus.y, Dir, objMainEff);
				}
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateHachi_1()
	{
		if (f == 1)
		{
			addSound(4);
		}
		if (f < 10 && f % 3 == 0)
		{
			setAva(0, objBeFireMain);
			GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(15), toY + CRes.random_Am_0(15), Dir, objMainEff);
			addVir(3, 5, 10, isPlayer: false);
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateGhin_2()
	{
		if (f == 1 || f == 8)
		{
			addSound(10);
		}
		if (f == 4)
		{
			for (int i = 0; i < VecEff.size(); i++)
			{
				((Point)VecEff.elementAt(i)).dis = 2;
			}
		}
		if (f == 12)
		{
			for (int j = 0; j < VecEff.size(); j++)
			{
				Point point = (Point)VecEff.elementAt(j);
				if (!checkNullObject(1))
				{
					if (Dir == 0)
					{
						point.x = objFireMain.x + 20;
					}
					else
					{
						point.x = objFireMain.x - 20;
					}
				}
				else
				{
					point.x = x;
				}
				point.y = objFireMain.y - 28 + 4 * j;
				point.vx = vx;
			}
		}
		for (int k = 0; k < VecEff.size(); k++)
		{
			Point point2 = (Point)VecEff.elementAt(k);
			point2.update();
			if (!checkNullObject(2) && k == 0 && f % 4 == 0 && CRes.abs(point2.x - objBeFireMain.x) < 30)
			{
				GameScreen.addEffectEnd(1, 0, objBeFireMain.x + CRes.random_Am_0(5), objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(5), Dir, objMainEff);
				setAva(0, objBeFireMain);
			}
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isPaintWeapon = true;
				objFireMain.vx = 0;
			}
			removeEff();
		}
		else if (f >= 12 && !checkNullObject(1))
		{
			objFireMain.vx = vx;
		}
	}

	public void updatePearl_2()
	{
		if (f > 10 && f < fRemove && f % 4 == 0)
		{
			addSound(19);
			if (indexObjBefire < vecObjsBeFire.size())
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
				indexObjBefire++;
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					int xdich = mainObject.x - x;
					int ydich = mainObject.y - mainObject.hOne / 2 - y;
					Point_Focus p = new Point_Focus();
					p = create_Speed(xdich, ydich, p);
					p.frame = CRes.random(3);
					p.objMain = mainObject;
					VecSubEff.addElement(p);
				}
			}
			else if (!checkNullObject(2))
			{
				int xdich2 = objBeFireMain.x + CRes.random_Am_0(30) - x;
				int ydich2 = objBeFireMain.y + CRes.random_Am_0(30) - objBeFireMain.hOne / 2 - y;
				Point_Focus p2 = new Point_Focus();
				p2 = create_Speed(xdich2, ydich2, p2);
				p2.frame = CRes.random(3);
				VecSubEff.addElement(p2);
			}
		}
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecSubEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				if (point_Focus.objMain != null)
				{
					GameScreen.addEffectEnd_ObjTo(55, 0, point_Focus.objMain.x, point_Focus.objMain.y - point_Focus.objMain.hOne / 2, point_Focus.objMain.ID, point_Focus.objMain.typeObject, Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd(55, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				}
				VecSubEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f >= fRemove && VecSubEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateUrgot3()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			if ((point.vy > 0 && point.y >= 0) || (point.vy < 0 && point.y <= -30))
			{
				point.vy = -point.vy;
			}
			point.y += point.vy;
		}
		if (f == 30 && !checkNullObject(1))
		{
			objFireMain.x = toX;
			objFireMain.y = toY;
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updatexerath3()
	{
		vy1000 += xplus;
		x1000 += vx1000;
		y1000 += vy1000;
		if (f == fRemove)
		{
			x1000 = toX * 1000;
			y1000 = toY;
			GameScreen.addEffectEnd(68, 0, toX, toY + 10, Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			if (VecEff.size() == 0)
			{
				removeEff();
			}
		}
		else
		{
			for (int i = 0; i < 1; i++)
			{
				Point point = new Point();
				point.x = x1000 / 1000;
				point.y = y1000;
				if (CRes.random(3) == 0)
				{
					point.fraImgEff = fraImgSubEff;
				}
				else
				{
					point.fraImgEff = fraImgSub2Eff;
				}
				VecEff.addElement(point);
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.f++;
			if (point2.f >= 8)
			{
				VecEff.removeElement(point2);
				j--;
			}
		}
	}

	public void updateXerath2()
	{
		if (f > 10)
		{
			for (int i = 0; i < vecPos.size(); i++)
			{
				((Point_Focus)vecPos.elementAt(i)).f++;
			}
		}
		if (GameCanvas.timeNow - timeBegin >= timeEnd)
		{
			removeEff();
		}
	}

	public void updateXerath1()
	{
		if (f == 5 && !checkNullObject(1))
		{
			GameScreen.addEffectEnd(30, 2, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, (short)(timeEnd - (GameCanvas.timeNow - timeBegin) - 200), Dir, objMainEff);
		}
		if (GameCanvas.timeNow - timeBegin >= timeEnd)
		{
			removeEff();
		}
	}

	public void updateNoTheoHuong_1()
	{
		if (f == 5 && !checkNullObject(1))
		{
			GameScreen.addEffectEnd(30, 2, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, (short)(timeEnd - (GameCanvas.timeNow - timeBegin) - 200), Dir, objMainEff);
			x = objFireMain.x;
			y = objFireMain.y;
		}
		if (f == 20 || f == 40)
		{
			for (int i = 0; i < 4; i++)
			{
				Point point = new Point();
				point.x = x + am_duong * 20;
				point.y = y - 30 + i * 20;
				point.vx = am_duong * 40;
				VecEff.addElement(point);
			}
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point point2 = (Point)VecSubEff.elementAt(j);
			point2.f++;
			if (point2.f > 6 && point2.f % 2 == 0)
			{
				point2.frame++;
			}
			if (point2.frame > 2)
			{
				VecSubEff.removeElement(point2);
				j--;
			}
		}
		for (int k = 0; k < VecEff.size(); k++)
		{
			Point point3 = (Point)VecEff.elementAt(k);
			point3.f++;
			if (point3.f % 3 == 1)
			{
				Point point4 = new Point();
				point4.x = point3.x;
				point4.y = point3.y;
				VecSubEff.addElement(point4);
				point3.x += point3.vx;
			}
			if (point3.f > 13)
			{
				VecEff.removeElement(point3);
			}
		}
		if (GameCanvas.timeNow - timeBegin >= timeEnd)
		{
			removeEff();
		}
	}

	public void updateNoTheoHuong_2()
	{
		if (f < 30 && f % 6 == 3)
		{
			addVir(3, 5, 10, isPlayer: false);
			for (int i = 0; i < 4; i++)
			{
				GameScreen.addEffectEnd(52, 0, x + am_duong * 20 + am_duong * (f / 6) * 40, y - 30 + i * 20, Dir, objMainEff);
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updateNoNangLuong3()
	{
		for (int i = 0; i < vecPos.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)vecPos.elementAt(i);
			if (point_Focus.f == 0)
			{
				addVir(2, 6, 10, isPlayer: false);
				GameScreen.addEffectEnd(63, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(59, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
			}
			point_Focus.f++;
			if (point_Focus.f >= 8)
			{
				vecPos.removeElement(point_Focus);
				i--;
			}
		}
		if (f >= fRemove && vecPos.size() == 0)
		{
			removeEff();
		}
	}

	public void updateNoNangLuong2()
	{
		if (f > 25)
		{
			for (int i = 0; i < vecPos.size(); i++)
			{
				((Point_Focus)vecPos.elementAt(i)).f++;
			}
		}
		if (GameCanvas.timeNow - timeBegin >= timeEnd)
		{
			removeEff();
		}
	}

	public void updateNoNangLuong1()
	{
		if (f == 5)
		{
			GameScreen.addEffectEnd(30, 2, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, (short)(timeEnd - (GameCanvas.timeNow - timeBegin) - 200), Dir, objMainEff);
		}
		if (CRes.random(6) == 0)
		{
			for (int i = indexObjBefire; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (indexObjBefire == GameScreen.vecPlayers.size() - 1)
				{
					indexObjBefire = 0;
				}
				if (mainObject != objFireMain && MainObject.getDistance(objFireMain.x, objFireMain.y, mainObject.x, mainObject.y) <= 220)
				{
					indexObjBefire = i + 1;
					if (indexObjBefire >= GameScreen.vecPlayers.size())
					{
						indexObjBefire = 0;
					}
					GameScreen.addEffectEnd_ObjTo(22, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, objFireMain.ID, objFireMain.typeObject, (sbyte)objFireMain.Dir, objMainEff);
					break;
				}
			}
		}
		if (GameCanvas.timeNow - timeBegin >= timeEnd || checkNullObject(1))
		{
			removeEff();
		}
	}

	public void updateGalio2()
	{
		if (f == 2)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				num %= 360;
				Point point = new Point(x + CRes.getcos(num) * 43 / 1000, y + CRes.getsin(num) * 23 / 1000);
				VecEff.addElement(point);
				GameScreen.addEffectEnd(66, 0, point.x, point.y, Dir, objMainEff);
				num += 45;
			}
		}
		if (f == 8)
		{
			int num2 = 22;
			for (int j = 0; j < 12; j++)
			{
				num2 %= 360;
				Point point2 = new Point(x + CRes.getcos(num2) * 65 / 1000, y + CRes.getsin(num2) * 40 / 1000);
				VecEff.addElement(point2);
				GameScreen.addEffectEnd(66, 0, point2.x, point2.y, Dir, objMainEff);
				num2 += 30;
			}
		}
		if (f == 14)
		{
			int num3 = 45;
			addVir(2, 6, 12, isPlayer: false);
			for (int k = 0; k < 16; k++)
			{
				num3 %= 360;
				Point point3 = new Point(x + CRes.getcos(num3) * 100 / 1000, y + CRes.getsin(num3) * 65 / 1000);
				VecEff.addElement(point3);
				GameScreen.addEffectEnd(66, 0, point3.x, point3.y, Dir, objMainEff);
				num3 += 22;
			}
		}
		for (int l = 0; l < VecEff.size(); l++)
		{
			Point point4 = (Point)VecEff.elementAt(l);
			point4.f++;
			if (point4.f >= 8)
			{
				VecEff.removeElement(point4);
				l--;
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	public void updatePan2()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			removeEff();
			return;
		}
		if (f < 6)
		{
			objFireMain.dy = f * 40;
		}
		if (f >= 6 && f <= 12)
		{
			objFireMain.dy = 480;
			objFireMain.isTanHinh = true;
		}
		if (f == 13)
		{
			objFireMain.isTanHinh = false;
			objFireMain.x = toX;
			objFireMain.y = toY;
		}
		if (f > 13 && f < 18)
		{
			objFireMain.dy = (17 - f) * 120;
		}
		if (f >= 18)
		{
			objFireMain.dy = 0;
		}
		if (f == 18)
		{
			addVir(2, 6, 10, isPlayer: false);
			GameScreen.addEffectEnd(65, 0, objFireMain.x, objFireMain.y + 22, Dir, objMainEff);
			GameScreen.addEffectEnd(59, 0, objFireMain.x + CRes.random_Am_0(10), objFireMain.y, Dir, objMainEff);
			GameScreen.addEffectEnd(59, 0, objFireMain.x + CRes.random_Am_0(10), objFireMain.y, Dir, objMainEff);
		}
	}

	public void update_Pan1()
	{
		if (f == 15)
		{
			Point o = new Point(toX, toY);
			VecEff.addElement(o);
		}
		if (GameCanvas.timeNow - timeBegin >= timeEnd)
		{
			removeEff();
		}
		if (f == 17)
		{
			Point point = new Point(toX, toY);
			point.frame = 2;
			VecEff.addElement(point);
		}
		if (f != 19 && f != 21 && f <= 25)
		{
			return;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point2 = (Point)VecEff.elementAt(i);
			if (f >= 25)
			{
				point2.f++;
			}
			else if (point2.frame == 2 || point2.frame == 4)
			{
				point2.frame += 2;
			}
		}
	}

	public void update_Zoro_s3_l3_New()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			if (isAddSound)
			{
				mSound.playSound(9, mSound.volumeSound);
			}
			addVir(10, 5, 10, isPlayer: true);
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point point = (Point)VecEff.elementAt(i);
				int subtype = 0;
				if (typeEffect == 185)
				{
					subtype = 1;
				}
				GameScreen.addEffectEnd(64, subtype, point.x, point.y, Dir, objMainEff);
				setAva(1, point.obj);
			}
			removeEff();
			return;
		}
		x1000 += vx;
		if (f == 6)
		{
			vx = 8;
			if (Dir == 0)
			{
				vx = -8;
			}
			objFireMain.vx = vx;
		}
		if (f == 10 && typeEffect == 217 && !GameCanvas.lowGraphic)
		{
			GameScreen.addEffectEnd(137, 0, objFireMain.x, objFireMain.y + 10, Dir, objMainEff);
		}
		if (f == 12)
		{
			objFireMain.isTanHinh = true;
		}
		if (f == 14)
		{
			vx = 0;
			objFireMain.vx = vx;
			objFireMain.isTanHinh = true;
		}
		if (f == 20)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd(108, 1, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, objMainEff);
						GameScreen.addEffectEnd_ObjTo(24, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, null);
					}
				}
			}
		}
		if (f >= 16 && f % 3 == 0 && indexObjBefire < vecObjsBeFire.size())
		{
			Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
			indexObjBefire++;
			if (object_Effect_Skill2 != null)
			{
				MainObject mainObject2 = MainObject.get_Object(object_Effect_Skill2.ID, object_Effect_Skill2.tem);
				if (mainObject2 != null)
				{
					Point point2 = new Point(mainObject2.x, mainObject2.y - mainObject2.hOne / 2);
					point2.obj = mainObject2;
					VecEff.addElement(point2);
				}
			}
		}
		for (int k = 0; k < VecEff.size(); k++)
		{
			Point point3 = (Point)VecEff.elementAt(k);
			point3.f++;
			point3.x = point3.obj.x;
			point3.y = point3.obj.y - point3.obj.hOne / 2;
		}
		if (f == 24)
		{
			changeDir();
			objFireMain.Dir = Dir;
			objFireMain.x = xArchor;
			objFireMain.y = yArchor;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			x1000 = x - 15;
			y1000 = objFireMain.y - 22;
			int num = -15;
			if (Dir == 2)
			{
				num = 15;
				x1000 = x - 63;
			}
			x += num;
			y -= 5;
		}
		if (f == 26)
		{
			objFireMain.isTanHinh = false;
		}
	}

	public void update_Zoro_s3_l6()
	{
		if (f >= fRemove || checkNullObject(1))
		{
			if (isAddSound)
			{
				mSound.playSound(9, mSound.volumeSound);
			}
			addVir(10, 5, 10, isPlayer: true);
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
				int subtype = 2;
				GameScreen.addEffectEnd(64, subtype, point_Focus.x, point_Focus.y - point_Focus.objMain.hOne / 2, Dir, objMainEff);
				setAva(1, point_Focus.objMain);
			}
			removeEff();
			return;
		}
		x1000 += vx;
		if (f == 6)
		{
			vx = 8;
			if (Dir == 0)
			{
				vx = -8;
			}
			objFireMain.vx = vx;
		}
		if (f == 10 && !GameCanvas.lowGraphic)
		{
			GameScreen.addEffectEnd(137, 1, objFireMain.x, objFireMain.y + 10, Dir, objMainEff);
		}
		if (f == 12)
		{
			objFireMain.isTanHinh = true;
		}
		if (f == 14)
		{
			vx = 0;
			objFireMain.vx = vx;
			objFireMain.isTanHinh = true;
		}
		if (f == 20)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd(108, 1, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, objMainEff);
					}
				}
			}
		}
		if (f >= 10 && f % 3 == 0 && indexObjBefire < vecObjsBeFire.size())
		{
			Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
			indexObjBefire++;
			if (object_Effect_Skill2 != null)
			{
				MainObject mainObject2 = MainObject.get_Object(object_Effect_Skill2.ID, object_Effect_Skill2.tem);
				if (mainObject2 != null)
				{
					Point_Focus p = new Point_Focus();
					int num = y;
					if (!checkNullObject(1))
					{
						num = objFireMain.y;
					}
					int xdich = mainObject2.x - x;
					int ydich = mainObject2.y - num;
					p = create_Speed(xdich, ydich, p, x, num, mainObject2.x, mainObject2.y);
					p.objMain = mainObject2;
					VecEff.addElement(p);
				}
			}
		}
		for (int k = 0; k < VecEff.size(); k++)
		{
			Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(k);
			point_Focus2.update_Vx_Vy();
			if (point_Focus2.f == point_Focus2.fRe)
			{
				setAva(1, point_Focus2.objMain);
				point_Focus2.vx = 0;
				point_Focus2.vy = 0;
				point_Focus2.x = point_Focus2.objMain.x;
				point_Focus2.y = point_Focus2.objMain.y;
			}
			if (point_Focus2.f > point_Focus2.fRe)
			{
				point_Focus2.objMain.dy = CRes.random(20, 30);
				if (point_Focus2.f < point_Focus2.fRe + 4)
				{
					setAva(-1, point_Focus2.objMain);
				}
			}
		}
		if (f == 24)
		{
			changeDir();
			objFireMain.Dir = Dir;
			objFireMain.x = xArchor;
			objFireMain.y = yArchor;
			x = objFireMain.x;
			y = objFireMain.y - objFireMain.hOne / 2;
			x1000 = x - 15;
			y1000 = objFireMain.y - 22;
			int num2 = -15;
			if (Dir == 2)
			{
				num2 = 15;
				x1000 = x - 63;
			}
			x += num2;
			y -= 5;
		}
		if (f == 26)
		{
			objFireMain.isTanHinh = false;
		}
	}

	public void changeDir()
	{
		Dir = (sbyte)((Dir != 2) ? 2 : 0);
	}

	public void addVir(int ran, int min, int max, bool isPlayer)
	{
		if ((!isPlayer || (!checkNullObject(1) && objFireMain == GameScreen.player)) && CRes.random(ran) == 0)
		{
			LoadMap.timeVibrateScreen = CRes.random(min, max);
		}
	}

	public void sendMove(int x, int xto, int y, int yto)
	{
		if (objFireMain != GameScreen.player)
		{
			objFireMain.x = xto;
			objFireMain.y = yto;
			return;
		}
		if (MainObject.getDistance(x, y, x, y) <= 30)
		{
			objFireMain.x = xto;
			objFireMain.y = yto;
		}
		int num = CRes.abs(x - xto);
		if (num < CRes.abs(y - yto))
		{
			num = CRes.abs(y - yto);
		}
		int num2 = num / 20;
		if (num2 == 0)
		{
			num2 = 1;
		}
		int num3 = (xto - x) / num2;
		int num4 = (xto - x) / num2;
		for (int i = 0; i < num2; i++)
		{
			objFireMain.x += num3;
			objFireMain.y += num4;
			GlobalService.gI().Obj_Move((short)objFireMain.x, (short)objFireMain.y);
		}
		objFireMain.x = xto;
		objFireMain.y = yto;
		GlobalService.gI().Obj_Move((short)objFireMain.x, (short)objFireMain.y);
	}

	private void update_Zoro_s3_l2_New()
	{
		if ((f == 13 || f == 20) && isAddSound)
		{
			mSound.playSound(10, mSound.volumeSound);
		}
		if (f == 15)
		{
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd_ObjTo(24, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, null);
					}
				}
			}
		}
		if (f > 20 && f % 3 == 0 && indexObjBefire < vecObjsBeFire.size())
		{
			Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vecObjsBeFire.elementAt(indexObjBefire);
			indexObjBefire++;
			if (object_Effect_Skill2 != null)
			{
				MainObject mainObject2 = MainObject.get_Object(object_Effect_Skill2.ID, object_Effect_Skill2.tem);
				if (mainObject2 != null)
				{
					Point_Focus p = new Point_Focus();
					int num = y;
					if (!checkNullObject(1))
					{
						num = objFireMain.y;
					}
					int xdich = mainObject2.x - x;
					int ydich = mainObject2.y - num;
					p = create_Speed(xdich, ydich, p, x, num, mainObject2.x, mainObject2.y);
					p.objMain = mainObject2;
					VecEff.addElement(p);
				}
			}
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(j);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f == point_Focus.fRe)
			{
				setAva(1, point_Focus.objMain);
				point_Focus.vx = 0;
				point_Focus.vy = 0;
				point_Focus.x = point_Focus.objMain.x;
				point_Focus.y = point_Focus.objMain.y;
			}
			if (point_Focus.f > point_Focus.fRe)
			{
				point_Focus.objMain.dy = CRes.random(20, 30);
				if (point_Focus.f < point_Focus.fRe + 4)
				{
					setAva(-1, point_Focus.objMain);
				}
				if (point_Focus.f >= point_Focus.fRe + 8)
				{
					VecEff.removeElement(point_Focus);
					j--;
				}
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void update_Zoro_s3_l1_New()
	{
		if ((f == 13 || f == 20) && isAddSound)
		{
			mSound.playSound(10, mSound.volumeSound);
		}
		if (f == 15)
		{
			for (int i = 0; i < vecObjsBeFire.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
				if (object_Effect_Skill != null)
				{
					MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
					if (mainObject != null)
					{
						GameScreen.addEffectEnd_ObjTo(24, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, null);
					}
				}
			}
		}
		if (f == 23)
		{
			for (int j = 0; j < vecObjsBeFire.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vecObjsBeFire.elementAt(j);
				if (object_Effect_Skill2 != null)
				{
					MainObject mainObject2 = MainObject.get_Object(object_Effect_Skill2.ID, object_Effect_Skill2.tem);
					if (mainObject2 != null)
					{
						GameScreen.addEffectEnd(11, 0, mainObject2.x, mainObject2.y - mainObject2.hOne / 2, Dir, objMainEff);
						setAva(0, mainObject2);
					}
				}
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void updateLuffyMon16_17()
	{
		if (f == 1 || f == 5 || f == 10)
		{
			int num = 20;
			if (Dir == 0)
			{
				num = -20;
			}
			setAva(0, objBeFireMain);
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(35, 0, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			if (!checkNullObject(1))
			{
				GameScreen.addEffectEnd(72, (f != 5) ? 1 : 2, x + num, objFireMain.y - objFireMain.hOne / 2, Dir, objMainEff);
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void updateLuffySea1()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(93, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(8, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f == 3 || f == 11)
		{
			if (isAddSound)
			{
				mSound.playSound(11, mSound.volumeSound);
			}
			if (!checkNullObject(2))
			{
				for (int j = 0; j < 2; j++)
				{
					Point_Focus p = new Point_Focus(x, y);
					int xdich = objBeFireMain.x - x;
					int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - y;
					p = create_Speed(xdich, ydich, p);
					p.frame = CRes.random(3);
					p.dis = Dir;
					VecEff.addElement(p);
				}
			}
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void updateLuffySea2()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				GameScreen.addEffectEnd(8, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 4, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
			else if (typeEffect == 135)
			{
				Point o = new Point(point_Focus.x, point_Focus.y);
				VecSubEff.addElement(o);
			}
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point point = (Point)VecSubEff.elementAt(j);
			point.f++;
			if (point.f >= 4)
			{
				VecSubEff.removeElement(point);
				j--;
			}
		}
		if (f >= 10 && f <= 13 && !checkNullObject(1))
		{
			objFireMain.dy = (f - 9) * 8;
		}
		if (f >= 14 && f <= 16 && !checkNullObject(1))
		{
			objFireMain.dy = 32;
		}
		if (f >= 17 && f <= 20 && !checkNullObject(1))
		{
			objFireMain.dy = (20 - f) * 8;
		}
		if (f == 3 || f == 6)
		{
			if (isAddSound)
			{
				mSound.playSound(11, mSound.volumeSound);
			}
			if (!checkNullObject(2))
			{
				for (int k = 0; k < 2; k++)
				{
					Point_Focus p = new Point_Focus(x, y);
					int xdich = objBeFireMain.x - x;
					int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - y;
					p = create_Speed(xdich, ydich, p);
					p.frame = CRes.random(3);
					p.dis = Dir;
					if (typeEffect == 135)
					{
						p.maxdis = 1;
					}
					VecEff.addElement(p);
				}
			}
		}
		if (f == 12 && isAddSound)
		{
			mSound.playSound(6, mSound.volumeSound);
		}
		if (f == 15 && !checkNullObject(3))
		{
			Point_Focus p2 = new Point_Focus(x, y);
			int xdich2 = objBeFireMain.x - x;
			int ydich2 = objBeFireMain.y - objBeFireMain.hOne / 2 - (y - objFireMain.dy);
			p2 = create_Speed(xdich2, ydich2, p2, x, y - objFireMain.dy, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2);
			p2.maxdis = 1;
			p2.frame = CRes.random(4);
			p2.dis = Dir;
			VecEff.addElement(p2);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			if (!checkNullObject(1))
			{
				objFireMain.dy = 0;
			}
			removeEff();
		}
	}

	private void updateLuffySea3()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			point_Focus.update_Vx_Vy();
			if (point_Focus.f >= point_Focus.fRe)
			{
				addVir(5, 5, 10, isPlayer: true);
				GameScreen.addEffectEnd(8, 0, point_Focus.x, point_Focus.y, Dir, objMainEff);
				VecEff.removeElement(point_Focus);
				i--;
			}
		}
		if (f >= 22 && f <= 25 && !checkNullObject(1))
		{
			objFireMain.dy = (f - 21) * 10;
		}
		if (f >= 26 && f <= 35 && !checkNullObject(1))
		{
			objFireMain.dy = 40;
		}
		if (f >= 36 && f <= 39 && !checkNullObject(1))
		{
			objFireMain.dy = (39 - f) * 10;
		}
		if (f == 10 || f == 20 || f == 34)
		{
			if (isAddSound)
			{
				mSound.playSound(6, mSound.volumeSound);
			}
			if (!checkNullObject(3))
			{
				Point_Focus p = new Point_Focus(x, y);
				int xdich = objBeFireMain.x - x;
				int ydich = objBeFireMain.y - objBeFireMain.hOne / 2 - (y - objFireMain.dy);
				p = create_Speed(xdich, ydich, p, x, y - objFireMain.dy, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2);
				p.frame = CRes.random(4);
				p.dis = Dir;
				VecEff.addElement(p);
			}
		}
		if (f == 1 || f == 11 || f == 26)
		{
			GameScreen.addEffectEnd(30, 0, x, y - objFireMain.dy, 250, Dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			if (!checkNullObject(1))
			{
				objFireMain.dy = 0;
			}
			removeEff();
		}
	}

	private void updateSanjiSea1()
	{
		if (f <= 4 || (f >= 11 && f <= 15))
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
			}
		}
		else if (!checkNullObject(1))
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 2 && !checkNullObject(3))
		{
			int num = objBeFireMain.x - 20;
			if (Dir == 0)
			{
				num = objBeFireMain.x + 20;
			}
			objFireMain.x = num;
			objFireMain.y = objBeFireMain.y;
		}
		if (f == 12 && !checkNullObject(1))
		{
			objFireMain.x = x;
			objFireMain.y = y;
		}
		if (objBeFireMain != null && objBeFireMain.hOne > 0 && (f == 6 || f == 9))
		{
			if (isAddSound && f == 6)
			{
				mSound.playSound(2, mSound.volumeSound);
			}
			if (!checkNullObject(2))
			{
				GameScreen.addEffectEnd(1, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
				GameScreen.addEffectEnd(93, 2, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
			}
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.x = x;
				objFireMain.y = y;
			}
			removeEff();
		}
	}

	private void updateSanjiSea2()
	{
		if (f <= 4 || (f >= 8 && f <= 13) || f == 19)
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
			}
		}
		else if (!checkNullObject(1))
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 2 && !checkNullObject(3))
		{
			int num = objBeFireMain.x - 20;
			if (Dir == 0)
			{
				num = objBeFireMain.x + 20;
			}
			objFireMain.x = num;
			objFireMain.y = objBeFireMain.y;
			objFireMain.Dir = Dir;
		}
		if (f == 12 && !checkNullObject(3))
		{
			int num2 = objBeFireMain.x + 20;
			if (Dir == 0)
			{
				num2 = objBeFireMain.x - 20;
			}
			objFireMain.x = num2;
			objFireMain.y = objBeFireMain.y;
			objFireMain.Dir = ((Dir == 0) ? 2 : 0);
		}
		if (f == 19 && !checkNullObject(1))
		{
			objFireMain.x = x;
			objFireMain.y = y;
			objFireMain.Dir = Dir;
		}
		if (objBeFireMain != null && objBeFireMain.hOne > 0 && (f == 4 || f == 6 || f == 14 || f == 16))
		{
			if (isAddSound && (f == 4 || f == 14))
			{
				mSound.playSound(2, mSound.volumeSound);
			}
			if (!checkNullObject(3))
			{
				if (objFireMain.hOne > 0)
				{
					int num3 = 25;
					if (objFireMain.Dir == 0)
					{
						num3 = -25;
					}
					GameScreen.addEffectEnd(36, 0, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				}
				if (typeEffect == 137)
				{
					GameScreen.addEffectEnd(1, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3), Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd(4, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3) - 10, Dir, objMainEff);
					addVir(5, 5, 10, isPlayer: true);
				}
			}
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.x = x;
				objFireMain.y = y;
				objFireMain.Dir = Dir;
				objFireMain.isTanHinh = false;
			}
			removeEff();
		}
	}

	private void updateSanjiSea3()
	{
		if ((f >= 4 && f <= 8) || (f >= 19 && f <= 23) || (f >= 35 && f <= 39))
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = true;
			}
		}
		else if (!checkNullObject(1))
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 6 && !checkNullObject(3))
		{
			int num = objBeFireMain.x - 25;
			if (Dir == 0)
			{
				num = objBeFireMain.x + 25;
			}
			objFireMain.x = num;
			objFireMain.y = objBeFireMain.y;
			objFireMain.Dir = Dir;
		}
		if (f == 21 && !checkNullObject(3))
		{
			int num2 = objBeFireMain.x + 25;
			if (Dir == 0)
			{
				num2 = objBeFireMain.x - 25;
			}
			objFireMain.x = num2;
			objFireMain.y = objBeFireMain.y;
			objFireMain.Dir = ((Dir == 0) ? 2 : 0);
		}
		if (f == 37 && !checkNullObject(1))
		{
			objFireMain.x = x;
			objFireMain.y = y;
			objFireMain.Dir = Dir;
		}
		if (objBeFireMain != null && objBeFireMain.hOne > 0 && (f == 11 || f == 15 || f == 25 || f == 29))
		{
			if (isAddSound && (f == 11 || f == 25))
			{
				mSound.playSound(14, mSound.volumeSound);
			}
			if (!checkNullObject(3))
			{
				if (objFireMain.hOne > 0)
				{
					int num3 = 30;
					if (objFireMain.Dir == 0)
					{
						num3 = -30;
					}
					GameScreen.addEffectEnd(36, 0, objFireMain.x + num3, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				}
				GameScreen.addEffectEnd(4, 0, objBeFireMain.x + CRes.random_Am_0(15), objBeFireMain.y - CRes.random(0, objBeFireMain.hOne / 4 * 3) - 10, Dir, objMainEff);
				addVir(5, 5, 10, isPlayer: true);
			}
		}
		if (f >= fRemove)
		{
			if (!checkNullObject(1))
			{
				objFireMain.x = x;
				objFireMain.y = y;
				objFireMain.Dir = Dir;
			}
			removeEff();
		}
	}

	private void updateMonster_DanhTron()
	{
		if (f == 1 && !checkNullObject(1))
		{
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject.typeObject == 0 && MainObject.getDistance(mainObject.x, mainObject.y, objFireMain.x, objFireMain.y) <= 60)
				{
					setAva(-1, mainObject);
					GameScreen.addEffectEnd(3, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, Dir, objMainEff);
				}
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void updateUssop_S2_L3_New()
	{
		if (f >= fRemove)
		{
			removeEff();
			return;
		}
		if (f == 15)
		{
			if (isAddSound)
			{
				mSound.playSound(23, mSound.volumeSound);
			}
			toX = objBeFireMain.x;
			toY = objBeFireMain.y - objBeFireMain.hOne / 2;
			y -= 6;
			if (Dir == 0)
			{
				x -= 30;
			}
			else
			{
				x += 30;
			}
			if (toX > x)
			{
				vx = 12;
			}
			else
			{
				vx = -12;
			}
			if (toY > y)
			{
				vy = 2;
			}
			else
			{
				vy = -2;
			}
			setAngle();
			GameScreen.addEffectEnd(57, 0, x, y, Dir, objMainEff);
			addVir(5, 5, 10, isPlayer: true);
		}
		if (f > 15 && f < fRemove && typeEffect == 225 && !GameCanvas.lowGraphic)
		{
			GameScreen.addEffectEnd(140, 0, x, y + 40, Dir, objMainEff);
		}
		if ((typeEffect != 225 && f == fRemove - 10) || (typeEffect == 225 && f == fRemove - 16))
		{
			setAva(2, objBeFireMain);
			if (!checkNullObject(2))
			{
				int a = 12;
				GameScreen.addEffectEnd(4, 2, objBeFireMain.x + CRes.random_Am_0(a), objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(a), Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
		}
	}

	private void updateUssop_S2_L6()
	{
		if (f >= fRemove)
		{
			objFireMain.isTanHinh = false;
			removeEff();
			return;
		}
		if (f == 15)
		{
			if (isAddSound)
			{
				mSound.playSound(23, mSound.volumeSound);
			}
			toX = objBeFireMain.x;
			toY = objBeFireMain.y - objBeFireMain.hOne / 2;
			y -= 6;
			if (Dir == 0)
			{
				x -= 30;
			}
			else
			{
				x += 30;
			}
			if (toX > x)
			{
				vx = 12;
			}
			else
			{
				vx = -12;
			}
			if (toY > y)
			{
				vy = 2;
			}
			else
			{
				vy = -2;
			}
			setAngle();
			GameScreen.addEffectEnd(168, 2, x, y, Dir, objMainEff);
			addVir(5, 5, 10, isPlayer: true);
		}
		if (f > 15 && f < fRemove && !GameCanvas.lowGraphic)
		{
			GameScreen.addEffectEnd(167, 0, x, y + 40, Dir, objMainEff);
		}
		if (f == fRemove - 10 || f == fRemove - 16)
		{
			setAva(2, objBeFireMain);
			if (!checkNullObject(2))
			{
				int a = 12;
				GameScreen.addEffectEnd(4, 2, objBeFireMain.x + CRes.random_Am_0(a), objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(a), Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
		}
		if (f > 2 && f < 6)
		{
			((Point_Focus)VecEff.elementAt(0)).update();
		}
		if (f > 2 && f < 15)
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
	}

	private void updateUssop_S2_L6_old()
	{
		if (f >= fRemove)
		{
			if (objFireMain != null)
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		if (f == 1 || f == 10)
		{
			GameScreen.addEffectEnd(5, 0, x, y, Dir, objMainEff);
		}
		if (f == 2)
		{
			objFireMain.isTanHinh = true;
		}
		else if (f == fPre)
		{
			objFireMain.isTanHinh = false;
		}
		if (VecEff.size() > 0 && f < fPre)
		{
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
				int num = f - fPre * i / 3;
				if (num > 0 && num < point_Focus.fRe)
				{
					point_Focus.update();
				}
				int a = 12;
				if (num == point_Focus.fRe)
				{
					GameScreen.addEffectEnd(1, 0, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
					GameScreen.addEffectEnd(4, 2, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
					GameScreen.addEffectEnd(93, 2, toX + CRes.random_Am_0(a), toY + CRes.random_Am_0(a), Dir, objMainEff);
				}
			}
		}
		if (f == 8 + fPre)
		{
			if (isAddSound)
			{
				mSound.playSound(23, mSound.volumeSound);
			}
			toX = objBeFireMain.x;
			toY = objBeFireMain.y - objBeFireMain.hOne / 2;
			y = objFireMain.y - objFireMain.hOne / 2;
			if (Dir == 0)
			{
				x = objFireMain.x - 30;
			}
			else
			{
				x = objFireMain.x + 30;
			}
			if (toX > x)
			{
				vx = 12;
			}
			else
			{
				vx = -12;
			}
			if (toY > y)
			{
				vy = 2;
			}
			else
			{
				vy = -2;
			}
			setAngle();
			GameScreen.addEffectEnd(57, 0, x, y, Dir, objMainEff);
			addVir(5, 5, 10, isPlayer: true);
		}
		if (f > 8 + fPre && f < fRemove && !GameCanvas.lowGraphic)
		{
			GameScreen.addEffectEnd(167, 0, x, y + 40, Dir, objMainEff);
		}
		if (f == fRemove - 16)
		{
			setAva(2, objBeFireMain);
			if (!checkNullObject(2))
			{
				int a2 = 12;
				GameScreen.addEffectEnd(4, 2, objBeFireMain.x + CRes.random_Am_0(a2), objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(a2), Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, objBeFireMain.x, objBeFireMain.y - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
		}
	}

	private void updateMonster_Chay_Thang()
	{
		if (f > 12 && f % 12 == 0)
		{
			Point point = new Point();
			point.vx = am_duong * 15;
			if (!checkNullObject(1))
			{
				point.y = objFireMain.y;
				point.x = objFireMain.x + point.vx;
			}
			else
			{
				point.y = y;
				point.x = x + point.vx;
			}
			VecEff.addElement(point);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point2 = (Point)VecEff.elementAt(i);
			point2.update();
			if (point2.f > 6)
			{
				point2.frame++;
			}
			if (point2.frame >= 3)
			{
				VecEff.removeElement(point2);
				i--;
			}
		}
		if (GameCanvas.timeNow - timeBegin >= timeEnd)
		{
			removeEff();
		}
	}

	private void updateSanji_S2_L3_New()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		if ((f % 10 > 9 || f % 10 <= 1) && f > 5 && f < 35)
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 10 || f == 20 || f == 30)
		{
			if (f > 10)
			{
				changeDir();
				am_duong = -1;
				if (Dir == 2)
				{
					am_duong = 1;
				}
				objFireMain.Dir = Dir;
			}
			objFireMain.x = objBeFireMain.x - am_duong * 30;
			objFireMain.y = objBeFireMain.y;
		}
		if (f < 40 && f >= 10 && (f % 10 == 2 || f % 10 == 7))
		{
			if (isAddSound && f % 10 == 2)
			{
				mSound.playSound(14, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			GameScreen.addEffectEnd(36, 0, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			GameScreen.addEffectEnd(25, 0, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			setAva(0, objBeFireMain);
		}
		if (f == 42)
		{
			objFireMain.isTanHinh = true;
			changeDir();
			am_duong = -1;
			if (Dir == 2)
			{
				am_duong = 1;
			}
			objFireMain.Dir = Dir;
			objFireMain.x = x;
			objFireMain.y = y;
		}
	}

	private void updateSanji_S2_L3_New_SHORT()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(1))
			{
				objFireMain.isTanHinh = false;
			}
			removeEff();
			return;
		}
		if ((f % 10 > 9 || f % 10 <= 1) && f > 5 && f < 25)
		{
			objFireMain.isTanHinh = true;
		}
		else
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 1 || f == 10 || f == 20)
		{
			changeDir();
			am_duong = -1;
			if (Dir == 2)
			{
				am_duong = 1;
			}
			objFireMain.Dir = Dir;
			objFireMain.x = objBeFireMain.x - am_duong * 30;
			objFireMain.y = objBeFireMain.y;
		}
		if (f < 24 && (f % 10 == 2 || f % 10 == 7))
		{
			if (isAddSound && f % 10 == 2)
			{
				mSound.playSound(14, mSound.volumeSound);
			}
			if (f % 10 == 2 || typeEffect == 187)
			{
				GameScreen.addEffectEnd(108, 7, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			}
			addVir(5, 5, 10, isPlayer: true);
			GameScreen.addEffectEnd(36, 0, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			int subtype = 0;
			if (typeEffect == 187)
			{
				subtype = 4;
			}
			GameScreen.addEffectEnd(25, subtype, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
			if (typeEffect == 187)
			{
				GameScreen.addEffectEnd(119, 2, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.hOne / 2 + 2, (sbyte)objFireMain.Dir, objMainEff);
			}
			setAva(0, objBeFireMain);
		}
		if (f == 22)
		{
			objFireMain.isTanHinh = true;
			changeDir();
			am_duong = -1;
			if (Dir == 2)
			{
				am_duong = 1;
			}
			objFireMain.Dir = Dir;
			objFireMain.x = x;
			objFireMain.y = y;
		}
	}

	private void updateSanji_S1_L3_New()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(3))
			{
				objFireMain.isTanHinh = false;
				objBeFireMain.vx = 0;
			}
			removeEff();
			return;
		}
		if (f == 10)
		{
			objFireMain.x = objBeFireMain.x - am_duong * 30;
			objFireMain.y = objBeFireMain.y;
		}
		if (f == 12 || f == 17)
		{
			if (isAddSound)
			{
				mSound.playSound(2, mSound.volumeSound);
			}
			if (typeEffect == 177)
			{
				GameScreen.addEffectEnd(19, 0, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 1, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(36, 0, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
			GameScreen.addEffectEnd(25, 0, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
		}
		if (f == 20)
		{
			vy1000 = 35;
			objFireMain.isTanHinh = true;
		}
		if (f >= 20 && f <= 27)
		{
			objBeFireMain.dy = yplus;
			yplus += vy1000;
			if (vy1000 > 0)
			{
				vy1000 -= 5;
			}
			setAva(-1, objBeFireMain);
		}
		if (f == 25)
		{
			objFireMain.isTanHinh = false;
		}
		if (f == 23)
		{
			objFireMain.dy = 105;
			changeDir();
			am_duong = -1;
			if (Dir == 2)
			{
				am_duong = 1;
			}
			objFireMain.Dir = Dir;
			objFireMain.x = objBeFireMain.x - am_duong * 30;
			GameScreen.addEffectEnd(30, 0, objFireMain.x - am_duong * 5, objFireMain.y - objFireMain.hOne / 2 - objFireMain.dy, 400, Dir, objMainEff);
		}
		if (f >= 23 && f <= 40)
		{
			objFireMain.dy = 105;
			if (f >= 27)
			{
				objBeFireMain.dy = 105;
			}
			setAva(-1, objBeFireMain);
		}
		if (f == 40)
		{
			if (isAddSound)
			{
				mSound.playSound(15, mSound.volumeSound);
			}
			addVir(5, 5, 10, isPlayer: true);
			if (typeEffect == 177)
			{
				GameScreen.addEffectEnd(19, 0, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 1, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(36, 0, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
				GameScreen.addEffectEnd(35, 0, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.dy - objBeFireMain.hOne / 2, Dir, objMainEff);
			}
			vy1000 = 10;
			yplus = 120;
		}
		if (f >= 41 && f <= 46)
		{
			yplus -= vy1000;
			objBeFireMain.dy = yplus;
			vy1000 += 5;
			if (objBeFireMain.dy < 0)
			{
				objBeFireMain.dy = 0;
			}
			objBeFireMain.vx = am_duong * 15;
			objFireMain.updateDy();
		}
		if (f > 47)
		{
			objBeFireMain.vx = 0;
		}
	}

	private void updateSanji_S1_L3_SHORT()
	{
		if (f >= fRemove || checkNullObject(3))
		{
			if (!checkNullObject(3))
			{
				objFireMain.isTanHinh = false;
				objBeFireMain.vx = 0;
			}
			removeEff();
			return;
		}
		if (f == 1)
		{
			objFireMain.x = objBeFireMain.x - am_duong * 30;
			objFireMain.y = objBeFireMain.y;
		}
		if (f != 7 && f != 10 && f != 13)
		{
			return;
		}
		if (isAddSound)
		{
			mSound.playSound(2, mSound.volumeSound);
		}
		setAva(0, objBeFireMain);
		sbyte subtype = 1;
		if (typeEffect != 218 || f == 10)
		{
			subtype = 0;
		}
		GameScreen.addEffectEnd(36, subtype, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
		subtype = 0;
		if (typeEffect == 186)
		{
			subtype = 4;
		}
		GameScreen.addEffectEnd(25, subtype, objBeFireMain.x - am_duong * 5, objBeFireMain.y - objBeFireMain.hOne / 2 + CRes.random_Am_0(10), Dir, objMainEff);
		if (f == 10)
		{
			GameScreen.addEffectEnd(108, 7, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			if (typeEffect == 186)
			{
				GameScreen.addEffectEnd(119, 1, objFireMain.x + am_duong * 25, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
			}
			if (typeEffect == 218)
			{
				GameScreen.addEffectEnd(119, 4, objFireMain.x + am_duong * 20, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, objMainEff);
			}
		}
	}

	private void beginPaint()
	{
	}

	public void addSound(sbyte idS)
	{
		if (isAddSound)
		{
			mSound.playSound(idS, mSound.volumeSound);
		}
	}

	public void addSoundBuff()
	{
		if (objFireMain.clazz == 1)
		{
			addSound(6);
		}
		else if (objFireMain.clazz == 2)
		{
			addSound(8);
		}
		else if (objFireMain.clazz == 3)
		{
			addSound(16);
		}
		else if (objFireMain.clazz == 4)
		{
			addSound(22);
		}
		else if (objFireMain.clazz == 5)
		{
			addSound(34);
		}
	}

	public void addSoundBuffShort()
	{
		if (objFireMain.clazz == 1)
		{
			addSound(44);
		}
		else if (objFireMain.clazz == 2)
		{
			addSound(45);
		}
		else if (objFireMain.clazz == 3)
		{
			addSound(46);
		}
		else if (objFireMain.clazz == 4)
		{
			addSound(22);
		}
		else if (objFireMain.clazz == 5)
		{
			addSound(34);
		}
	}

	public void paintKurobi_2(mGraphics g)
	{
		if (f >= 15 && f <= 20)
		{
			fraImgEff.drawFrame((f - 11) / 3, objFireMain.x + x1000, objFireMain.y + y1000, Dir, 3, g);
		}
		else if (f >= 25 && f <= 30)
		{
			fraImgEff.drawFrame((f - 25) / 3, objFireMain.x + x1000, objFireMain.y + y1000 + 10, Dir, 3, g);
		}
	}

	public void paintDonKrieg_3(mGraphics g)
	{
		if (f > 10 && f < 18)
		{
			int num = x + plusxy[1][0];
			int num2 = y + plusxy[1][1];
			int idx = 1;
			if (f < 13)
			{
				num = x + plusxy[0][0];
				num2 = y + plusxy[0][1];
				idx = 0;
			}
			fraImgEff.drawFrame(idx, num, num2, Dir, 3, g);
		}
	}

	public void paintDonKrieg_2(mGraphics g)
	{
		if (f < fRemove)
		{
			int num = xArchor;
			int num2 = f / 2;
			if (f > 16)
			{
				num2 = 22 - f;
				if (f > 20)
				{
					num = xArchor + xplus * 2;
				}
				else if (f > 18)
				{
					num = xArchor + xplus;
				}
			}
			else if (f >= 4)
			{
				num2 = 2;
			}
			if (num2 == 2)
			{
				num2 = 3;
			}
			if (f >= 10 && f <= 12)
			{
				num = xArchor + xplus;
			}
			fraImgEff.drawFrame(num2, num, y, Dir, 3, g);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			fraImgSubEff.drawFrame(0, point_Focus.x, point_Focus.y, Dir, 3, g);
		}
		for (int j = 0; j < VecSubEff.size(); j++)
		{
			Point_Focus point_Focus2 = (Point_Focus)VecSubEff.elementAt(j);
			fraImgSub2Eff.drawFrame(point_Focus2.f % fraImgSub2Eff.nFrame, point_Focus2.x, point_Focus2.y, Dir, 3, g);
		}
	}

	public void paintDonKrieg_1(mGraphics g)
	{
		if (f < fRemove)
		{
			int num = x;
			int idx = f / 2;
			if (f > 16)
			{
				idx = 22 - f;
				if (f > 20)
				{
					num = x + xplus * 2;
				}
				else if (f > 18)
				{
					num = x + xplus;
				}
			}
			else if (f >= 4)
			{
				idx = 2;
			}
			if (f >= 10 && f <= 12)
			{
				num = x + xplus;
			}
			fraImgEff.drawFrame(idx, num, y, Dir, 3, g);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			paint_Bullet(g, fraImgSubEff, point_Focus.frame, point_Focus.x, point_Focus.y, isMore: false, 0);
		}
	}

	public void paintBuggy_2(mGraphics g)
	{
		int num = 0;
		int num2 = 5;
		if (f == 28)
		{
			num = -6;
		}
		else if (f > 28)
		{
			num = 0;
			num2 *= 2;
		}
		else
		{
			num2 = 0;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
			if (Dir == 2)
			{
				g.setColor(15956504);
				g.fillRect(x1000 - num2, y1000 - 7, CRes.abs(point_Focus.x - x1000) + num2, 14);
				g.setColor(15985419);
				g.fillRect(x1000 - num2, y1000 - 6, CRes.abs(point_Focus.x - x1000) + num2, 12);
				g.setColor(16645629);
				g.fillRect(x1000 - num2, y1000 - 4, CRes.abs(point_Focus.x - x1000) + num2, 8);
			}
			else
			{
				g.setColor(15956504);
				g.fillRect(x1000 - CRes.abs(point_Focus.x - x1000), y1000 - 7, CRes.abs(point_Focus.x - x1000) + num2, 14);
				g.setColor(15985419);
				g.fillRect(x1000 - CRes.abs(point_Focus.x - x1000), y1000 - 6, CRes.abs(point_Focus.x - x1000) + num2, 12);
				g.setColor(16645629);
				g.fillRect(x1000 - CRes.abs(point_Focus.x - x1000), y1000 - 4, CRes.abs(point_Focus.x - x1000) + num2, 8);
			}
			fraImgSub3Eff.drawFrame(0, point_Focus.x, point_Focus.y, Dir, 3, g);
		}
		if (f > 8 && f < 42)
		{
			if (Dir == 2)
			{
				num2 = -num2;
			}
			int idx = 0;
			if (f < 16)
			{
				idx = 2;
			}
			else if (f == 16 || f == 17)
			{
				idx = 1;
			}
			fraImgSubEff.drawFrame(idx, x + num2, y + 38 + num, Dir, 33, g);
		}
		if (f >= 18 && f <= 20)
		{
			fraImgSub2Eff.drawFrame(f % fraImgSub2Eff.nFrame, x1000, y1000, Dir, 3, g);
		}
		if (f < 12)
		{
			int idx2 = 1 + f / 2 % 2;
			if (f < 2 || f > 9)
			{
				idx2 = 0;
			}
			fraImgEff.drawFrame(idx2, x, y, Dir, mGraphics.TOP | mGraphics.HCENTER, g);
		}
		if (f > 40)
		{
			int idx3 = 1 + f / 2 % 2;
			if (f > 46)
			{
				idx3 = 0;
			}
			fraImgEff.drawFrame(idx3, x, y, Dir, mGraphics.TOP | mGraphics.HCENTER, g);
		}
	}

	public void paintLuffy_New3(mGraphics g)
	{
		for (int i = 0; i < VecSubEff.size(); i++)
		{
			Point point = (Point)VecSubEff.elementAt(i);
			if (point.f < 3)
			{
				fraImgSub2Eff.drawFrame(2 - point.f, point.x, point.y, Dir, 33, g);
			}
			else
			{
				objFireMain.paintBody(g, point.x, point.y, objFireMain.frame, objFireMain.Dir, isEye: true);
			}
			int num = -20;
			if (Dir == 2)
			{
				num = 20;
			}
			if (f > 20)
			{
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, point.x + num, point.y - objFireMain.hOne / 2, Dir, 3, g);
			}
		}
		if (f > 20)
		{
			fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
		}
	}

	public void paintLuffy_New2(mGraphics g)
	{
		if (objFireMain != null)
		{
			if (f == 1)
			{
				fraImgSubEff.drawFrame(0, x, y + objFireMain.hOne / 2, Dir, 33, g);
			}
			if ((f >= 9 && f <= 11) || (f > 25 && f < 33))
			{
				int num = 16;
				if (Dir == 0)
				{
					num = -16;
				}
				fraImgEff.drawFrame(2, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2 + 2, Dir, 3, g);
			}
		}
		if (f >= 12 && f <= 15)
		{
			fraImgSubEff.drawFrame((f - 12) / 2, x, y, Dir, 3, g);
		}
		if (f >= 17 && f <= 20)
		{
			fraImgSub2Eff.drawFrame((f - 17) / 2, x, y, Dir, 3, g);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			fraImgSubEff.drawFrame(point.f / 2, point.x, point.y, Dir, 33, g);
		}
	}

	public void paintLuffy_New2_SHORT(mGraphics g)
	{
		if (objFireMain != null && ((f >= 2 && f <= 3) || (f > 16 && f < 22)))
		{
			int num = 16;
			if (Dir == 0)
			{
				num = -16;
			}
			fraImgEff.drawFrame(2, objFireMain.x + num, objFireMain.y - objFireMain.hOne / 2 + 2 - objFireMain.dy, Dir, 3, g);
		}
		if (f >= 3 && f <= 6)
		{
			fraImgSubEff.drawFrame((f - 12) / 2, x, y, Dir, 3, g);
		}
		if (f >= 8 && f <= 11)
		{
			fraImgSub2Eff.drawFrame((f - 17) / 2, x, y, Dir, 3, g);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			fraImgSubEff.drawFrame(point.f / 2, point.x, point.y, Dir, 33, g);
		}
		if (typeEffect == 213 || typeEffect == 272)
		{
			for (int j = 0; j < VecSubEff.size(); j++)
			{
				Point point2 = (Point)VecSubEff.elementAt(j);
				fraImgSub3Eff.drawFrame(point2.frame, point2.x, point2.y, Dir, 33, g);
			}
		}
	}

	public void paintSanji_3(mGraphics g)
	{
		if (f >= 4 && f < fRemove)
		{
			fraImgEff.drawFrame((f - 4) / numNextFrame % fraImgEff.nFrame, x - xplus, y, Dir, 3, g);
		}
		if (typeEffect == 49 || typeEffect == 50)
		{
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(i);
				if (fraImgSubEff != null)
				{
					fraImgSubEff.drawFrame(point_Focus.f % fraImgSubEff.nFrame, point_Focus.x, point_Focus.y, Dir, 3, g);
				}
				fraImgSub2Eff.drawFrame((point_Focus.f + point_Focus.frame) % fraImgSub2Eff.nFrame, point_Focus.x, point_Focus.y, Dir, 3, g);
			}
			return;
		}
		if (typeEffect == 220 || typeEffect == 293)
		{
			if (f > 1 && f < fRemove - 1 && fraImgSubEff != null)
			{
				fraImgSubEff.drawFrame(f / 2 % fraImgSubEff.nFrame, x1000, y1000 + 5, Dir, 33, g);
			}
			for (int j = 0; j < VecSubEff.size(); j++)
			{
				Point point = (Point)VecSubEff.elementAt(j);
				if (point.f == 0)
				{
					fraImgSub2Eff.drawFrame(point.frame, point.x + am_duong * 5, point.y + 4, Dir, 3, g);
				}
				else if (point.frame == 0)
				{
					fraImgSub3Eff.drawFrame(point.f, point.x, point.y, Dir, 3, g);
				}
				else
				{
					fraImgSub4Eff.drawFrame(point.f, point.x, point.y, Dir, 3, g);
				}
			}
			return;
		}
		if (f > 1 && f < fRemove - 1 && fraImgSubEff != null)
		{
			fraImgSubEff.drawFrame(f / 2 % fraImgSubEff.nFrame, x1000, y1000 + 5, Dir, 33, g);
		}
		for (int k = 0; k < VecSubEff.size(); k++)
		{
			Point point2 = (Point)VecSubEff.elementAt(k);
			if (point2.f == 0)
			{
				fraImgSub2Eff.drawFrame(point2.f, point2.x, point2.y, Dir, 3, g);
			}
			else
			{
				fraImgSub3Eff.drawFrame(point2.f, point2.x, point2.y, Dir, 3, g);
			}
		}
	}

	public void paintGalio_1(mGraphics g)
	{
		int num = 2;
		numNextFrame = 4;
		if (f > 40)
		{
			num = fraImgEff.nFrame;
			numNextFrame = 2;
		}
		fraImgEff.drawFrame(f / numNextFrame % num, objFireMain.x, objFireMain.y - objFireMain.hOne / 2, Dir, 3, g);
	}

	public void paintPan_1(mGraphics g)
	{
		int num = 3;
		if (f > 20)
		{
			num = fraImgEff.nFrame;
		}
		fraImgEff.drawFrame(f / 2 % num, objFireMain.x, objFireMain.y, Dir, 3, g);
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			fraImgSubEff.drawFrame(point.frame + point.f / 3 % 2, point.x, point.y, 0, mGraphics.BOTTOM | mGraphics.RIGHT, g);
			fraImgSubEff.drawFrame(point.frame + point.f / 3 % 2, point.x, point.y, 2, mGraphics.BOTTOM | mGraphics.LEFT, g);
			fraImgSubEff.drawFrame(point.frame + point.f / 3 % 2, point.x, point.y, 1, mGraphics.TOP | mGraphics.RIGHT, g);
			fraImgSubEff.drawFrame(point.frame + point.f / 3 % 2, point.x, point.y, 3, 0, g);
		}
	}

	public override void replaceHP(mVector vec)
	{
		for (int i = 0; i < vecObjsBeFire.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObjsBeFire.elementAt(i);
			if (object_Effect_Skill == null)
			{
				continue;
			}
			for (int j = 0; j < vec.size(); j++)
			{
				Object_Effect_Skill object_Effect_Skill2 = (Object_Effect_Skill)vec.elementAt(i);
				if (object_Effect_Skill2 != null && object_Effect_Skill.ID == object_Effect_Skill2.ID)
				{
					if (GameScreen.isShowTextTab)
					{
						GameCanvas.chatTabScr.addNewChat(T.tabTestAdmin, "+DAM: ", string.Empty + object_Effect_Skill.hpShow, 1, isFocus: false);
					}
					object_Effect_Skill.hpShow = object_Effect_Skill2.hpShow;
					object_Effect_Skill.hpMagic = object_Effect_Skill2.hpMagic;
					object_Effect_Skill.mEffTypePlus = new int[object_Effect_Skill2.mEffTypePlus.Length];
					object_Effect_Skill.mEff_HP_Plus = new int[object_Effect_Skill2.mEffTypePlus.Length];
					object_Effect_Skill.mEff_Time_Plus = new int[object_Effect_Skill2.mEffTypePlus.Length];
					for (int k = 0; k < object_Effect_Skill.mEffTypePlus.Length; k++)
					{
						object_Effect_Skill.mEffTypePlus[k] = object_Effect_Skill2.mEffTypePlus[k];
						object_Effect_Skill.mEff_HP_Plus[k] = object_Effect_Skill2.mEff_HP_Plus[k];
						object_Effect_Skill.mEff_Time_Plus[k] = object_Effect_Skill2.mEff_Time_Plus[k];
					}
					break;
				}
			}
		}
	}

	public static void setHP_New(mVector vec, MainObject objFire, bool isAdd)
	{
		for (int i = 0; i < vec.size(); i++)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vec.elementAt(i);
			MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
			if (mainObject == null)
			{
				vec.removeElement(object_Effect_Skill);
				i--;
			}
			else
			{
				if (mainObject.Action == 4)
				{
					continue;
				}
				if (object_Effect_Skill.hpLast < mainObject.Hp)
				{
					mainObject.Hp = object_Effect_Skill.hpLast;
				}
				if (!isAdd)
				{
					continue;
				}
				bool flag = setAddEffPlus(object_Effect_Skill, mainObject, objFire, objFire);
				sbyte typeColor = 15;
				int num = object_Effect_Skill.hpShow;
				if (objFire == GameScreen.player)
				{
					typeColor = 13;
				}
				if (objFire.typeObject == 1)
				{
					typeColor = 14;
					num = -num;
				}
				if (objFire == GameScreen.player && GameScreen.isShowTextTab)
				{
					GameCanvas.chatTabScr.addNewChat(T.tabTestAdmin, "+DAM: ", string.Empty + object_Effect_Skill.hpShow, 1, isFocus: false);
				}
				if (objFire == GameScreen.player || mainObject == GameScreen.player || !GameCanvas.lowGraphic)
				{
					if (object_Effect_Skill.hpShow == 0)
					{
						GameScreen.addEffectNumBig_NEW_AP(num, object_Effect_Skill.hpMagic, mainObject.x, mainObject.y - mainObject.hOne, 17);
					}
					else
					{
						if (flag)
						{
							typeColor = 16;
						}
						GameScreen.addEffectNumBig_NEW_AP(num, object_Effect_Skill.hpMagic, mainObject.x, mainObject.y - mainObject.hOne, typeColor);
					}
				}
				if (mainObject.Hp <= 0)
				{
					mainObject.beginDie(objFire);
				}
			}
		}
	}

	public void setFlyFire(int begin, int end, int maxfly, int speedfly)
	{
		if (objFireMain != null && !objFireMain.returnAction() && f >= begin && f < end)
		{
			objFireMain.dy += speedfly;
			if (objFireMain.dy > maxfly)
			{
				objFireMain.dy = maxfly;
			}
		}
	}

	public void setDownFire(int begin, int end, int speedfly)
	{
		if (objFireMain != null && !objFireMain.returnAction() && f >= begin && f < end)
		{
			objFireMain.dy -= speedfly;
			if (objFireMain.dy < 0)
			{
				objFireMain.dy = 0;
			}
		}
	}

	public static bool setAddEffPlus(Object_Effect_Skill objEff, MainObject obj, MainObject objFire, MainObject OBJMainEff)
	{
		if (objEff == null || obj == null || objFire == null)
		{
			return false;
		}
		bool result = false;
		for (int i = 0; i < objEff.mEffTypePlus.Length; i++)
		{
			switch (objEff.mEffTypePlus[i])
			{
			case 1010:
				if (objEff.hpShow <= 1)
				{
					return false;
				}
				GameScreen.addEffectEnd(20, 0, obj.x, obj.y - obj.hOne / 2, (sbyte)obj.Dir, OBJMainEff);
				result = true;
				break;
			case 1013:
				GameScreen.addEffectEnd(21, 0, obj.x, obj.y - obj.hOne / 2, (sbyte)obj.Dir, OBJMainEff);
				break;
			case 1014:
				GameScreen.addEffectEnd_ToX_ToY(23, 0, obj.x, obj.y - obj.hOne / 2, objFire.x, objFire.y - objFire.hOne / 2, (sbyte)obj.Dir, OBJMainEff);
				break;
			case 1021:
			case 1022:
				GameScreen.addEffectEnd_ObjTo(22, (objEff.mEffTypePlus[i] == 1021) ? 1 : 0, obj.x, obj.y - obj.hOne / 2, objFire.ID, objFire.typeObject, (sbyte)objFire.Dir, OBJMainEff);
				break;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 15:
			case 16:
			case 17:
				obj.addEffSpec((short)objEff.mEffTypePlus[i], (short)objEff.mEff_Time_Plus[i]);
				break;
			case 12:
				GameScreen.addEffectNum(objEff.hpShow + T.chuan, obj.x, obj.y - obj.hOne, 11);
				break;
			}
		}
		return result;
	}

	public void CreateSpeedEff(int xS, int yS, int xToS, int yToS, int time, int vyBegin)
	{
		int num = (xToS - xS) * 1000 / time;
		int num2 = CRes.abs(vyBegin / (time / 2));
		speedEff = new int[time][];
		int num3 = yS;
		for (int i = 0; i < speedEff.Length - 1; i++)
		{
			speedEff[i] = new int[2];
			speedEff[i][0] = xS + num * i / 1000;
			speedEff[i][1] = num3 + vyBegin / 1000;
			vyBegin += num2;
			num3 = speedEff[i][1];
		}
		speedEff[time - 1][0] = xToS;
		speedEff[time - 1][1] = yToS;
	}

	public void CreateSuperFrameCausu1()
	{
		mframeSuper = new int[6][]
		{
			new int[2] { 1, 0 },
			new int[3] { 1, 1, 0 },
			new int[3] { 1, 1, 0 },
			new int[3] { 1, 1, 0 },
			new int[2] { 1, 0 },
			new int[2] { 1, 2 }
		};
	}

	public static int HasHapThuEffPlus(Object_Effect_Skill objEff, MainObject obj, MainObject objFire, MainObject OBJMainEff)
	{
		if (objEff == null || obj == null || objFire == null)
		{
			return -1;
		}
		for (int i = 0; i < objEff.mEffTypePlus.Length; i++)
		{
			if (objEff.mEffTypePlus[i] == 1058)
			{
				GameScreen.addEffectEnd(20, 0, obj.x, obj.y - obj.hOne / 2, (sbyte)obj.Dir, OBJMainEff);
				return i;
			}
		}
		return -1;
	}
}
