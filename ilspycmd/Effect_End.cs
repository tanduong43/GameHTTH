using System;

public class Effect_End : MainEffect
{
	public const sbyte END_LUFFY_2 = 0;

	public const sbyte END_SANJI_1 = 1;

	public const sbyte END_ZORO_1 = 86;

	public const sbyte END_USSOP_1 = 3;

	public const sbyte END_USSOP_2 = 4;

	public const sbyte END_USSOP_3 = 5;

	public const sbyte END_NAMI_1 = 6;

	public const sbyte END_NAMI_2 = 90;

	public const sbyte END_NAMI_3 = 8;

	public const sbyte END_ZORO_2 = 9;

	public const sbyte END_ZORO_3 = 10;

	public const sbyte END_ZORO_4 = 11;

	public const sbyte END_USSOP_4 = 12;

	public const sbyte END_LUFFY_1 = 13;

	public const sbyte END_ZORO_5 = 16;

	public const sbyte END_ZORO_7 = 19;

	public const sbyte END_CRI = 20;

	public const sbyte END_XUYEN_GIAP = 21;

	public const sbyte END_HUT_MP_HP = 22;

	public const sbyte END_PHAN_DAMAGE = 23;

	public const sbyte END_FOCUS_TOUCH = 24;

	public const sbyte END_LUFFY_6 = 25;

	public const sbyte END_ZORO_8 = 26;

	public const sbyte END_ZORO_9 = 27;

	public const sbyte END_SANJI_2 = 29;

	public const sbyte END_LINE_IN = 30;

	public const sbyte END_SANJI_3 = 35;

	public const sbyte END_SANJI_4 = 36;

	public const sbyte END_SANJI_5 = 37;

	public const sbyte END_NAMI_4 = 38;

	public const sbyte END_NAMI_5 = 39;

	public const sbyte END_NAMI_6 = 40;

	public const sbyte END_NAMI_7 = 41;

	public const sbyte END_NAMI_8 = 42;

	public const sbyte END_SANJI_6 = 47;

	public const sbyte END_USSOP_9 = 48;

	public const sbyte END_USSOP_10 = 49;

	public const sbyte END_USSOP_11 = 50;

	public const sbyte END_ALVIDA_1 = 89;

	public const sbyte END_ALVIDA_2 = 52;

	public const sbyte END_OPEN_BOX = 53;

	public const sbyte END_LUFFY_S1_FINAL = 54;

	public const sbyte END_PEARL_2 = 55;

	public const sbyte END_TELEPORT_BOSS = 56;

	public const sbyte END_DON_KRIEG_2 = 57;

	public const sbyte END_DON_KRIEG_3 = 58;

	public const sbyte END_ROCK = 59;

	public const sbyte END_HACHI_2 = 60;

	public const sbyte END_CHU_1 = 61;

	public const sbyte END_NUC_DAT = 62;

	public const sbyte END_NO_DAT = 63;

	public const sbyte END_ZORO_14 = 64;

	public const sbyte END_PAN_1 = 65;

	public const sbyte END_NO_DAT_NHO = 66;

	public const sbyte END_XERATH_1 = 68;

	public const sbyte END_URGOT_2 = 69;

	public const sbyte END_URGOT_4 = 70;

	public const sbyte END_MON_DANH_MANH = 71;

	public const sbyte END_MON_CHEM = 72;

	public const sbyte END_UPGRADE_1 = 73;

	public const sbyte END_UPGRADE_2 = 74;

	public const sbyte END_UPGRADE_3 = 75;

	public const sbyte END_UPGRADE_THANH_CONG = 76;

	public const sbyte END_UPGRADE_THAT_BAI = 77;

	public const sbyte END_UPGRADE_4 = 78;

	public const sbyte END_UPGRADE_5 = 79;

	public const sbyte END_TELEPORT = 80;

	public const sbyte END_USSOP_SEA_2 = 81;

	public const sbyte END_DIE_FLY = 82;

	public const sbyte END_REVICE = 83;

	public const sbyte END_GET_UP = 84;

	public const sbyte END_BUFF = 85;

	public const sbyte END_ACE_1 = 2;

	public const sbyte END_AOKIJI_1 = 14;

	public const sbyte END_AOKIJI_2 = 88;

	public const sbyte END_ICE = 17;

	public const sbyte END_SMOKE_1 = 18;

	public const sbyte END_UP_LV = 28;

	public const sbyte END_CHAR_TELEPORT_REMOVE = 31;

	public const sbyte END_CHAR_TELEPORT_NEW = 32;

	public const sbyte END_THUY_QUAI_1 = 87;

	public const sbyte END_THUY_QUAI_2 = 34;

	public const sbyte END_HP_MP = 91;

	public const sbyte END_MON_DIE = 92;

	public const sbyte END_EFFECT_SKILL_1 = 93;

	public const sbyte END_EFFECT_SKILL_UP_LV = 94;

	public const sbyte END_EFFECT_ELITE = 95;

	public const sbyte END_MON_VALENTINE = 45;

	public const sbyte END_SKILL_CLASS_NAMI = 46;

	public const sbyte END_SKILL_CLASS_USSOP = 96;

	public const sbyte END_SKILL_CLASS_ZORO = 97;

	public const sbyte END_CROCODILE_1 = 98;

	public const sbyte END_CROCODILE_2 = 99;

	public const sbyte END_BIG_BOSS = 100;

	public const sbyte END_CHOPPER_BUFF = 101;

	public const sbyte END_KUROMARIMO = 102;

	public const sbyte END_MR3_1 = 103;

	public const sbyte END_LITTLE_HP_BOSS = 104;

	public const sbyte END_LITTLE_DAM_BOSS = 105;

	public const sbyte END_WAPOL = 106;

	public const sbyte END_MR4_1 = 107;

	public const sbyte END_PARTICAL_VL = 108;

	public const sbyte END_PARTICAL_DUST = 109;

	public const sbyte END_ROCK_NEW = 110;

	public const sbyte END_SUPER_BOSS = 111;

	public const sbyte END_BIG_NO_DAT = 112;

	public const sbyte END_LAVA_1 = 113;

	public const sbyte END_MR2_1 = 114;

	public const sbyte END_EFF_BI_BE = 115;

	public const sbyte END_EFF_ICE_GOLEM = 116;

	public const sbyte END_EFF_ICE_SNOW = 117;

	public const sbyte END_PELL_1 = 118;

	public const sbyte END_LUFFY_S1_S4 = 119;

	public const sbyte END_USSOP_S3_LV4 = 120;

	public const sbyte END_ENEL_1 = 121;

	public const sbyte END_SATORI_1 = 122;

	public const sbyte END_SUPER_ATTACK = 123;

	public const sbyte END_OHM_1_1 = 124;

	public const sbyte END_OHM_1_2 = 125;

	public const sbyte END_POKE_OK = 126;

	public const sbyte END_POKE_BEGIN = sbyte.MaxValue;

	public const short END_POKE_FAIL = 128;

	public const short END_TRU_BE = 129;

	public const short END_LOL_TRU_TREN = 130;

	public const short END_LOL_TRU_DUOI = 131;

	public const short END_LUCCI_1 = 132;

	public const short END_DONG_DAT_1 = 133;

	public const short END_DONG_DAT_2 = 134;

	public const short END_ZORO_S1_L5 = 135;

	public const short END_ZORO_S2_L5 = 136;

	public const short END_ZORO_S3_L5 = 137;

	public const short END_NAMI_S1_L5 = 138;

	public const short END_NAMI_S2_L5 = 139;

	public const short END_USOPP_S2_L5 = 140;

	public const short END_ACE_1_L2 = 141;

	public const short END_AOKIJI_2_L2 = 142;

	public const short END_KICH_AN = 143;

	public const short END_LINE_IN_BIG = 144;

	public const short END_LINE_IN_OPEN_CHEST = 145;

	public const short END_SMOKER_L2 = 146;

	public const short END_LUCCI_L2 = 147;

	public const short END_KILO_L1 = 148;

	public const short END_CHOANG_SERVER = 149;

	public const short END_KIET_SUC_SERVER = 150;

	public const short END_BEGIN_EFF_CHIEM_DAO = 151;

	public const short END_KHANG_TAT_CA_SERVER = 152;

	public const short END_THA_DEN = 153;

	public const short END_UPGRADE_6 = 154;

	public const short END_UPGRADE_7 = 155;

	public const short END_TU_CHOI_TU_THAN = 156;

	public const short END_FIRE_FULL_SET = 157;

	public const short END_THA_PHAO_HOA = 158;

	public const short END_LIGHTING_RED = 159;

	public const short END_EF_VALENTINE_RUN = 160;

	public const short END_EF_VALENTINE_STAND = 161;

	public const short END_EF_LAW_HEART = 162;

	public const short END_EF_LAW_HEART_FRIST = 163;

	public const short END_EF_TRAI_AC_QUY_VU_TRU = 164;

	public const short END_EF_FASHION_RAU_DEN = 165;

	public const short END_EF_FASHION_RAU_DEN_2 = 166;

	public const short END_USOPP_S2_L6 = 167;

	public const short END_USSOP_S3_LV6 = 168;

	public const sbyte END_LUFFY_SHOW = 33;

	public const sbyte END_ZORO_SHOW = 15;

	public const sbyte END_SANJI_SHOW = 44;

	public const sbyte END_NAMI_SHOW = 51;

	public const sbyte END_USS_SHOW = 7;

	public const short END_FIREWORK = 169;

	public const short END_ZORO_XOAY_NUOC = 170;

	public const short END_GONG_DAM = 171;

	public const short END_DAP_DAT = 172;

	public const short END_RONG_TAP = 173;

	public const short END_LOI_NGUC = 174;

	public const short END_NAMI_S3_L6 = 175;

	public const short END_DOOR = 176;

	public const short END_TAIXIU = 177;

	public const short END_GOAL = 178;

	private sbyte typeSub;

	private mVector VecEff = new mVector();

	private mVector vecSubEff = new mVector();

	private int randomf;

	private int randomf2;

	private int lengthM;

	private MainObject objTo;

	private bool isUpdateNormal = true;

	private int[] mPlayFrame;

	private int[][] mPlayFrameVip;

	private long time;

	private int timeRemove;

	private int[] mposy;

	private int maxsize;

	private int[][] mPaintEFF;

	private int[] arrFrame = new int[15]
	{
		0, 0, 0, 1, 1, 1, 2, 2, 2, 3,
		3, 3, 4, 4, 4
	};

	private int tframe;

	public static int[][] colorStar = new int[3][]
	{
		new int[3] { 16310304, 16298056, 16777215 },
		new int[3] { 7045120, 12643960, 16777215 },
		new int[3] { 2407423, 11987199, 16777215 }
	};

	private int[] colorpaint;

	private int indexColorStar;

	private int xline;

	private int yline;

	public Effect_End(short type, sbyte subtype, int x, int y, int timeRe, sbyte Dir, MainObject objEff)
	{
		f = 0;
		typeEffect = type;
		typeSub = subtype;
		base.x = x;
		base.y = y;
		base.Dir = Dir;
		timeRemove = timeRe;
		time = GameCanvas.timeNow;
		objMainEff = objEff;
		numNextFrame = 1;
		create_Effect();
	}

	public Effect_End(short type, sbyte subtype, int x, int y, sbyte Dir, MainObject objEff)
	{
		f = 0;
		typeEffect = type;
		typeSub = subtype;
		base.x = x;
		base.y = y;
		base.Dir = Dir;
		objMainEff = objEff;
		numNextFrame = 1;
		create_Effect();
	}

	public Effect_End(short type, sbyte subtype, int x, int y, sbyte Dir)
	{
		f = 0;
		typeEffect = type;
		typeSub = subtype;
		base.x = x;
		base.y = y;
		base.Dir = Dir;
		numNextFrame = 1;
		create_Effect();
	}

	public Effect_End(short type, sbyte subtype, int x, int y, int xTo, int yTo, sbyte Dir, MainObject objEff)
	{
		f = 0;
		typeEffect = type;
		typeSub = subtype;
		base.x = x;
		base.y = y;
		toX = xTo;
		toY = yTo;
		base.Dir = Dir;
		objMainEff = objEff;
		numNextFrame = 1;
		create_Effect();
	}

	public Effect_End(short type, MainObject objEff)
	{
		f = 0;
		typeEffect = type;
		objMainEff = objEff;
	}

	public Effect_End(short type, sbyte subtype, int x, int y, short Id, sbyte tem, sbyte Dir, MainObject objEff, int timeRe)
	{
		f = 0;
		typeEffect = type;
		typeSub = subtype;
		base.x = x;
		base.y = y;
		base.Dir = Dir;
		objMainEff = objEff;
		objTo = MainObject.get_Object(Id, tem);
		if (objTo == null)
		{
			removeEff();
			return;
		}
		timeRemove = timeRe;
		time = GameCanvas.timeNow;
		toX = objTo.x;
		toY = objTo.y;
		setAngle();
		numNextFrame = 1;
		create_Effect();
	}

	public void create_Effect()
	{
		int xdich = toX - x;
		int ydich = toY - y;
		switch (typeEffect)
		{
		case 166:
			break;
		case 164:
			create_Ice_Arc();
			levelPaint = -1;
			break;
		case 0:
			numNextFrame = 2;
			fRemove = 6;
			fraImgEff = new FrameImage(3, 30, 50);
			break;
		case 1:
			numNextFrame = 1;
			fRemove = 4;
			fraImgEff = new FrameImage(6, 38, 38);
			break;
		case 86:
			numNextFrame = 2;
			fRemove = 6;
			fraImgEff = new FrameImage(84, 64, 40);
			break;
		case 3:
			fraImgEff = new FrameImage(18, 20, 21);
			numNextFrame = 1;
			fRemove = 4;
			break;
		case 4:
			fraImgEff = new FrameImage(19, 50, 54, 35, 38);
			if (typeSub > 0)
			{
				fraImgEff = new FrameImage(107, 50, 54, 38, 41);
				GameScreen.addEffectEnd(47, (sbyte)(typeSub - 1), x, y, Dir, objMainEff);
			}
			numNextFrame = 1;
			fRemove = 5;
			break;
		case 5:
			fraImgEff = new FrameImage(21, 20, 21);
			numNextFrame = 1;
			fRemove = 4;
			break;
		case 6:
			vMax = 100;
			fraImgEff = new FrameImage(298, 24, 24, 6);
			if (typeSub == 1 || typeSub == 2 || typeSub == 3)
			{
				if (typeSub == 2 || typeSub == 3)
				{
					fraImgEff = new FrameImage(299, 26, 26, 2);
				}
				fraImgSubEff = new FrameImage(27, 24, 24);
				if (typeSub == 3)
				{
					fraImgSub2Eff = new FrameImage(326, 26, 26, 3);
				}
			}
			if (objMainEff != null)
			{
				indexEff_1 = objMainEff.indexEff_1;
			}
			numNextFrame = 1;
			fRemove = CRes.random(12, 20);
			x *= 100;
			y *= 100;
			createEndBungmerang();
			break;
		case 90:
			if (typeSub == 1)
			{
				fraImgEff = new FrameImage(195, 40, 27, 40, 27);
			}
			else
			{
				fraImgEff = new FrameImage(26, 40, 20);
			}
			numNextFrame = 2;
			fRemove = 4;
			break;
		case 8:
			fraImgEff = new FrameImage(30, 38, 38);
			numNextFrame = 2;
			fRemove = 6;
			break;
		case 9:
			fraImgEff = new FrameImage(34, 62, 64, 45, 46);
			numNextFrame = 2;
			fRemove = 6;
			break;
		case 10:
			fraImgEff = new FrameImage(15 + CRes.random(3), 55, 55, 40, 40);
			numNextFrame = 2;
			fRemove = 6;
			break;
		case 11:
		{
			typeSub = (sbyte)(14 + CRes.random(2) * 2);
			Point point3 = new Point();
			point3.x = x;
			point3.y = y;
			point3.fRe = 6;
			if (typeSub == 14)
			{
				point3.fraImgEff = new FrameImage(typeSub, 55, 55);
			}
			else
			{
				point3.fraImgEff = new FrameImage(typeSub, 55, 55, 40, 40);
			}
			VecEff.addElement(point3);
			numNextFrame = 2;
			fRemove = 6;
			GameScreen.addEffectEnd(93, 0, x, y, Dir, objMainEff);
			break;
		}
		case 12:
			fraImgEff = new FrameImage(36, 28, 28);
			if (typeSub == 1)
			{
				fraImgEff = new FrameImage(106, 28, 28);
				GameScreen.addEffectEnd(36, 0, x, y, Dir, objMainEff);
			}
			numNextFrame = 2;
			fRemove = 6;
			break;
		case 13:
			if (objTo != null)
			{
				toX = objTo.x;
				toY = objTo.y - objTo.hOne / 2;
			}
			xdich = toX - x;
			ydich = toY - y;
			if (typeSub == 3 || typeSub == 4 || typeSub == 5)
			{
				fraImgSubEff = new FrameImage(69, 14, 48);
				fraImgSub2Eff = new FrameImage(27, 24, 32);
				fraImgEff = new FrameImage(275, 35, 25);
				if (typeSub == 4 || typeSub == 5)
				{
					fraImgEff = new FrameImage(318, 35, 25);
				}
				if (typeSub == 5)
				{
					fraImgSubEff = new FrameImage(410, 3);
				}
			}
			else
			{
				if (typeSub == 1 || typeSub == 2)
				{
					fraImgSubEff = new FrameImage(69, 14, 48);
					if (typeSub == 2)
					{
						fraImgSub2Eff = new FrameImage(27, 24, 32);
					}
				}
				fraImgEff = new FrameImage(44, 25, 15);
			}
			numNextFrame = 1;
			vMax = 12;
			create_Speed(xdich, ydich, null);
			randomf = CRes.random(fraImgEff.nFrame);
			break;
		case 16:
			fRemove = 3;
			if (typeSub < 0)
			{
				fraImgEff = new FrameImage(301, 80, 32, 3);
			}
			else
			{
				fraImgEff = new FrameImage(300, 80, 25, 3);
			}
			if (objMainEff != null)
			{
				indexEff_1 = objMainEff.indexEff_1;
			}
			numNextFrame = 2;
			vx = -4;
			if (Dir == 2)
			{
				vx = 4;
			}
			break;
		case 19:
			if (typeSub == 0)
			{
				fraImgEff = new FrameImage(14, 55, 55);
				fraImgSubEff = new FrameImage(15, 55, 55, 40, 40);
			}
			else if (typeSub == 1)
			{
				fraImgEff = new FrameImage(16, 55, 55, 40, 40);
				fraImgSubEff = new FrameImage(17, 55, 55, 40, 40);
			}
			fRemove = 8;
			numNextFrame = 2;
			break;
		case 20:
			fraImgEff = new FrameImage(49, 70, 70, 50, 50);
			numNextFrame = 2;
			fRemove = 6;
			randomf = CRes.random(8);
			break;
		case 21:
			createXuyenGiap();
			break;
		case 22:
			createHut_MP_HP();
			break;
		case 23:
			vMax = 14;
			createPhanDamage();
			create_Speed(xdich, ydich, null);
			break;
		case 24:
			createFocustouch();
			break;
		case 25:
			if (typeSub == 4)
			{
				fraImgEff = new FrameImage(281, 29, 50);
			}
			else
			{
				fraImgEff = new FrameImage(68, 28, 44);
				if (typeSub == 1 || typeSub == 2 || typeSub == 3)
				{
					fraImgSubEff = new FrameImage(69, 14, 48);
					if (typeSub == 2 || typeSub == 3)
					{
						fraImgSub2Eff = new FrameImage(27, 24, 32);
					}
				}
			}
			x1000 = x - 5;
			if (Dir == 0)
			{
				x1000 = x + 5;
			}
			numNextFrame = 2;
			fRemove = 6;
			if (typeSub == 3)
			{
				fRemove = 4;
			}
			break;
		case 26:
			if (typeSub == 2)
			{
				fraImgEff = new FrameImage(278, 70, 70);
			}
			else if (typeSub == 3)
			{
				fraImgEff = new FrameImage(279, 70, 70);
			}
			else if (typeSub == 4)
			{
				fraImgEff = new FrameImage(420, 70, 70);
			}
			else
			{
				fraImgEff = new FrameImage(70, 52, 60, 36, 41);
			}
			numNextFrame = 1;
			fRemove = 6;
			mPlayFrame = new int[5] { 0, 1, 2, 0, 3 };
			if (typeSub == 1)
			{
				fRemove = 4;
				mPlayFrame = new int[3] { 0, 2, 0 };
			}
			break;
		case 27:
			if (objTo != null)
			{
				toX = objTo.x;
				toY = objTo.y;
			}
			xdich = toX - x;
			ydich = toY - y;
			fraImgEff = new FrameImage(72, 32, 60);
			fraImgSubEff = new FrameImage(27, 24, 32);
			numNextFrame = 2;
			vMax = 12;
			create_Speed(xdich, ydich, null);
			break;
		case 29:
			x1000 = x;
			y1000 = y;
			fRemove = 20;
			fraImgEff = new FrameImage(74, 60, 74);
			fraImgSubEff = new FrameImage(75, 82, 25);
			fraImgSub2Eff = new FrameImage(18, 20, 21);
			break;
		case 30:
			indexColorStar = typeSub;
			x1000 = x * 1000;
			y1000 = y * 1000;
			fRemove = CRes.random(4, 6);
			vMax = 5;
			xline = 10;
			yline = 20;
			maxsize = 4;
			create_Star_Line_In(vMax, xline, yline, 0, maxsize);
			break;
		case 35:
			fraImgEff = new FrameImage(89, 28, 44);
			numNextFrame = 2;
			fRemove = 6;
			break;
		case 36:
			if (typeSub == 1)
			{
				fraImgEff = new FrameImage(283, 22, 28);
			}
			else
			{
				fraImgEff = new FrameImage(78, 22, 28);
			}
			vx = CRes.random_Am_0(2);
			vy = -3;
			fRemove = CRes.random(7, 12);
			break;
		case 47:
			create_Sanji6();
			break;
		case 37:
			x1000 = x;
			y1000 = y;
			fRemove = 20;
			fraImgEff = new FrameImage(69, 14, 48);
			if (typeSub == 1)
			{
				fraImgSubEff = new FrameImage(103, 35, 19, 35, 19);
				fraImgSub2Eff = new FrameImage(18, 20, 21);
			}
			else
			{
				fraImgSubEff = new FrameImage(102, 35, 19);
			}
			break;
		case 38:
			fRemove = 5;
			if (typeSub == 1)
			{
				fraImgEff = new FrameImage(104, 30, 30);
			}
			else if (typeSub == 2)
			{
				fraImgEff = new FrameImage(243, 36, 36);
			}
			else if (typeSub == 3)
			{
				fraImgEff = new FrameImage(82, 30, 30);
			}
			else
			{
				fraImgEff = new FrameImage(82, 30, 30);
			}
			break;
		case 138:
			fRemove = 5;
			fraImgEff = new FrameImage(243, 36, 36, 4);
			break;
		case 39:
			fRemove = 8;
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne - 20;
			}
			if (typeSub == 3)
			{
				fraImgEff = new FrameImage(25, 80, 40);
			}
			else if (typeSub == 2)
			{
				fraImgEff = new FrameImage(328, 38, 27);
			}
			else
			{
				fraImgEff = new FrameImage(95, 32, 27);
			}
			break;
		case 40:
			fRemove = 6;
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne - 20;
			}
			if (typeSub == 2)
			{
				fraImgEff = new FrameImage(328, 38, 27);
				fraImgSubEff = new FrameImage(329, 56, 31);
			}
			else
			{
				fraImgEff = new FrameImage(95, 32, 27);
				fraImgSubEff = new FrameImage(97, 56, 31);
			}
			fraImgSub2Eff = new FrameImage(96, 28, 79, 1);
			break;
		case 175:
			fRemove = 15;
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne - 20;
			}
			fraImgEff = new FrameImage(25, 80, 40);
			fraImgSubEff = new FrameImage(329, 56, 31);
			fraImgSub2Eff = new FrameImage(238, 30, 73);
			fraImgSub3Eff = new FrameImage(195, 2);
			break;
		case 41:
			fRemove = 4;
			fraImgEff = new FrameImage(93, 64, 48, 41, 31);
			break;
		case 42:
			fRemove = 4;
			fraImgEff = new FrameImage(98, 78, 70, 56, 50);
			break;
		case 48:
			createUssop9();
			break;
		case 120:
			createUssopS3_Lv4();
			break;
		case 168:
			createUssopS3_Lv6();
			break;
		case 169:
			createEff_Firework();
			break;
		case 49:
			createUssop10();
			break;
		case 50:
			createUssop11();
			break;
		case 89:
			fraImgEff = new FrameImage(34, 62, 64, 45, 46);
			fRemove = 6;
			numNextFrame = 2;
			break;
		case 52:
			fraImgEff = new FrameImage(118, 62, 64, 47, 48);
			fRemove = 8;
			numNextFrame = 2;
			GameScreen.addEffectEnd(63, 0, x, y, Dir, objMainEff);
			break;
		case 53:
			fraImgEff = new FrameImage(121, 32, 32);
			fRemove = 12;
			numNextFrame = 2;
			break;
		case 54:
			createEND_Lu_S1_Final();
			break;
		case 55:
			fraImgEff = new FrameImage(100, 15, 20);
			fRemove = 10;
			numNextFrame = 2;
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y;
			}
			break;
		case 56:
			fraImgEff = new FrameImage(8, 40, 47, 40, 47);
			fRemove = 4;
			if (objTo != null && objTo.Action != 4)
			{
				objTo.isTanHinh = true;
			}
			break;
		case 57:
			fraImgEff = new FrameImage(107, 50, 54, 38, 41);
			fRemove = 10;
			numNextFrame = 2;
			break;
		case 58:
			fraImgEff = new FrameImage(138, 62, 64, 47, 48);
			fRemove = 8;
			numNextFrame = 2;
			GameScreen.addEffectEnd(63, 0, x, y, Dir, objMainEff);
			break;
		case 59:
			createRock();
			break;
		case 60:
			createHachi2();
			break;
		case 61:
			numNextFrame = 1;
			fRemove = 4;
			fraImgEff = new FrameImage(143, 38, 38);
			break;
		case 62:
			levelPaint = -1;
			if (LoadMap.idTile == 2)
			{
				fraImgEff = new FrameImage(154, 20, 15);
			}
			else
			{
				fraImgEff = new FrameImage(151, 20, 15);
			}
			fRemove = CRes.abs(toX - x) / 10 + 20;
			if (fRemove % 2 == 0)
			{
				fRemove++;
			}
			vx = 10;
			if (toX < x)
			{
				vx = -10;
			}
			break;
		case 63:
			levelPaint = -1;
			if (typeSub <= 2)
			{
				frame = CRes.random(3);
			}
			else
			{
				frame = typeSub;
			}
			fraImgEff = new FrameImage(245, 25, 21, 3);
			fRemove = CRes.random(30, 50);
			break;
		case 64:
			if (typeSub == 1)
			{
				fraImgEff = new FrameImage(279, 70, 70);
			}
			else if (typeSub == 2)
			{
				fraImgEff = new FrameImage(420, 70, 70);
			}
			else
			{
				fraImgEff = new FrameImage(169, 52, 60, 40, 46);
			}
			numNextFrame = 1;
			fRemove = 4;
			break;
		case 65:
			fraImgEff = new FrameImage(171, 153, 84, 100, 54);
			numNextFrame = 2;
			fRemove = 8;
			levelPaint = -1;
			break;
		case 170:
			fraImgEff = new FrameImage(414, 8);
			fRemove = 16;
			levelPaint = -1;
			break;
		case 171:
			fraImgEff = new FrameImage(408, 4);
			fRemove = 8;
			numNextFrame = 2;
			break;
		case 66:
			levelPaint = -1;
			fraImgEff = new FrameImage(226, 13, 11, 3);
			fRemove = CRes.random(15, 20);
			break;
		case 68:
			fraImgEff = new FrameImage(177, 48, 56, 40, 47);
			fRemove = 8;
			numNextFrame = 2;
			GameScreen.addEffectEnd(59, 0, x, y - 10, Dir, objMainEff);
			break;
		case 69:
			timeBegin = GameCanvas.timeNow;
			createUrgot_4(2);
			timeEnd = 3000;
			break;
		case 70:
			fRemove = 40;
			createUrgot_4(5);
			break;
		case 71:
			fraImgEff = new FrameImage(190, 30, 30);
			fRemove = 6;
			numNextFrame = 2;
			break;
		case 72:
			fRemove = 3;
			fraImgEff = new FrameImage(191, 60, 17);
			numNextFrame = 2;
			vx = -3;
			if (Dir == 2)
			{
				vx = 3;
			}
			break;
		case 73:
			vMax = 2;
			fRemove = CRes.abs(toX - x) / vMax + 1;
			break;
		case 74:
			vMax = 2;
			fRemove = CRes.abs(toX - x) / vMax + 1;
			levelPaint = -1;
			break;
		case 154:
		case 155:
			create_upgrade6();
			break;
		case 75:
			vMax = 5;
			fraImgEff = new FrameImage(192, 25, 25);
			fraImgSubEff = new FrameImage(51, 9, 9);
			fraImgSub2Eff = new FrameImage(52, 5, 5);
			xdich = toX - x;
			ydich = toY - y;
			create_Speed(xdich, ydich, null);
			break;
		case 76:
			create_Upgrade_Thanh_Cong();
			break;
		case 77:
			numNextFrame = 2;
			fraImgEff = new FrameImage(85, 34, 34, 28, 28);
			fRemove = 12;
			break;
		case 78:
			vMax = 8;
			fraImgEff = new FrameImage(192, 25, 25);
			fraImgSubEff = new FrameImage(51, 9, 9);
			fraImgSub2Eff = new FrameImage(52, 5, 5);
			xdich = toX - x;
			ydich = toY - y;
			create_Speed(xdich, ydich, null);
			break;
		case 79:
			if (AvMain.fraLevelUp == null)
			{
				AvMain.fraLevelUp = new FrameImage(mImage.createImage("/interface/levelup.png"), 115, 35);
			}
			fraImgEff = AvMain.fraLevelUp;
			vy = -3;
			if (typeSub % 2 == 1)
			{
				vy = -2;
			}
			fRemove = 18;
			frame = typeSub;
			break;
		case 177:
			if (typeSub == 0)
			{
				fraImgEff = new FrameImage(mImage.createImage("/interface/pvp3_e.png"), 1);
			}
			else if (typeSub == 1)
			{
				fraImgEff = new FrameImage(mImage.createImage("/interface/pvp4_e.png"), 1);
			}
			vy = -3;
			if (typeSub % 2 == 1)
			{
				vy = -2;
			}
			fRemove = 18;
			frame = 0;
			break;
		case 80:
			fraImgEff = new FrameImage(11, 40, 50);
			fRemove = 4;
			numNextFrame = 2;
			break;
		case 81:
			fraImgEff = new FrameImage(106, 28, 28);
			fRemove = 6;
			numNextFrame = 2;
			break;
		case 82:
			fraImgEff = new FrameImage(198, 40, 40);
			fraImgSubEff = new FrameImage(85, 34, 34, 28, 28);
			fRemove = 6;
			numNextFrame = 2;
			break;
		case 83:
			createRevice();
			break;
		case 84:
			createGet_Up();
			break;
		case 85:
			indexColorStar = typeSub;
			x1000 = x * 1000;
			y1000 = y * 1000;
			fRemove = CRes.random(4, 6);
			vMax = 5;
			xline = 10;
			yline = 20;
			maxsize = 4;
			create_Star_Line_In(vMax, xline, yline, 10, maxsize);
			fraImgEff = new FrameImage(51, 9, 9);
			fraImgSubEff = new FrameImage(52, 5, 5);
			break;
		case 2:
			fraImgEff = new FrameImage(195, 40, 27, 40, 27);
			fraImgSubEff = new FrameImage(32, 45, 45, 34, 34);
			fRemove = 5;
			break;
		case 14:
			fraImgEff = new FrameImage(42, 53, 48);
			vy = 4;
			fRemove = 4;
			numNextFrame = 2;
			break;
		case 88:
			fraImgEff = new FrameImage(46, 70, 100, 49, 70);
			mPlayFrame = new int[21]
			{
				0, 0, 0, 1, 1, 1, 2, 2, 2, 2,
				2, 2, 2, 3, 3, 3, 4, 4, 4, 4,
				4
			};
			fRemove = mPlayFrame.Length;
			GameScreen.addEffectEnd(17, CRes.random(20, 30), x, y, Dir, objMainEff);
			break;
		case 142:
			fraImgEff = new FrameImage(46, 70, 100, 49, 70);
			if (typeSub == 1)
			{
				mPlayFrame = new int[15]
				{
					0, 0, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 4, 4
				};
			}
			else
			{
				mPlayFrame = new int[11]
				{
					0, 0, 0, 0, 0, 0, 0, 0, 0, 4,
					4
				};
			}
			fRemove = mPlayFrame.Length;
			GameScreen.addEffectEnd(17, CRes.random(20, 30), x, y, Dir, objMainEff);
			break;
		case 17:
			fraImgEff = new FrameImage(39, 53, 28);
			fRemove = typeSub;
			levelPaint = -1;
			break;
		case 18:
			fraImgEff = new FrameImage(33, 62, 42, 40, 27);
			fRemove = 10;
			numNextFrame = 2;
			vy = -2;
			break;
		case 28:
			numNextFrame = 1;
			fraImgEff = new FrameImage(9, 63, 21);
			fRemove = 24;
			vy = -3;
			if (objFireMain != null)
			{
				x = objFireMain.x;
				y = objFireMain.y - objFireMain.hOne / 2;
			}
			GameScreen.addEffectEnd(50, 0, x, y, Dir, objMainEff);
			break;
		case 31:
			createCharTeleport();
			break;
		case 32:
			createCharTeleportNew();
			break;
		case 87:
			vMax = 100;
			fraImgEff = new FrameImage(80, 30, 15);
			numNextFrame = 2;
			fRemove = CRes.random(6, 12);
			x *= 100;
			y *= 100;
			createEndBungmerang();
			break;
		case 34:
			fraImgEff = new FrameImage(104, 30, 30);
			numNextFrame = 2;
			fRemove = 6;
			break;
		case 33:
			fRemove = 8;
			objFireMain = objMainEff;
			break;
		case 15:
			objFireMain = objMainEff;
			fRemove = 15;
			break;
		case 44:
			fRemove = 9;
			objFireMain = objMainEff;
			break;
		case 51:
			objFireMain = objMainEff;
			fRemove = 16;
			if (Dir == 0)
			{
				x -= 20;
			}
			else
			{
				x += 20;
			}
			y = objFireMain.y - objFireMain.hOne / 2;
			fraImgSubEff = new FrameImage(23, 24, 24);
			fraImgEff = new FrameImage(27, 24, 24);
			break;
		case 7:
			objFireMain = objMainEff;
			fraImgEff = new FrameImage(12, 15, 15);
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
			y = objFireMain.y - objFireMain.hOne / 2;
			break;
		case 91:
			fRemove = 4;
			mPlayFrame = new int[4] { 4, 3, 2, 0 };
			fraImgEff = new FrameImage(121, 32, 32);
			break;
		case 92:
			fRemove = 4;
			mPlayFrame = new int[4] { 4, 4, 5, 5 };
			fraImgEff = new FrameImage(85, 34, 34, 28, 28);
			break;
		case 93:
			createEffectSkill1();
			break;
		case 95:
		{
			int num2 = CRes.random(5, 8);
			for (int l = 0; l < num2; l++)
			{
				Point point5 = new Point(CRes.random_Am_0(20), CRes.random(10));
				point5.y2 = point5.y;
				point5.vy = -CRes.random(2, 4);
				VecEff.addElement(point5);
			}
			break;
		}
		case 45:
			levelPaint = -1;
			fraImgEff = new FrameImage(168, 112, 69, 89, 55);
			fRemove = 8;
			numNextFrame = 2;
			break;
		case 46:
		case 159:
			createClassNami();
			break;
		case 96:
			createClassUssop();
			break;
		case 97:
			createClassZoro();
			break;
		case 98:
			fraImgEff = new FrameImage(92, 64, 126, 45, 89, 1);
			fraImgSubEff = new FrameImage(99, 32, 32);
			fRemove = 16;
			break;
		case 99:
			fraImgEff = new FrameImage(201, 64, 50, 45, 35);
			fraImgSubEff = new FrameImage(202, 40, 92, 30, 69, 1);
			fraImgSub2Eff = new FrameImage(99, 32, 32);
			fRemove = 12;
			break;
		case 172:
			fraImgEff = new FrameImage(402, 4);
			fRemove = 12;
			break;
		case 173:
			fraImgEff = new FrameImage(403, 2);
			fRemove = 4;
			break;
		case 176:
			fraImgEff = new FrameImage(71, 5);
			fRemove = 5;
			break;
		case 174:
			fraImgEff = new FrameImage(411, 3);
			fRemove = 12;
			break;
		case 100:
			fraImgEff = new FrameImage(144, 37, 55);
			numNextFrame = 3;
			fRemove = 6;
			break;
		case 101:
			createChopper();
			break;
		case 102:
			createKuromarimo();
			break;
		case 103:
			createMr3_1();
			break;
		case 104:
			createLittleHpBoss();
			break;
		case 105:
			createLittleDamBoss();
			break;
		case 106:
			fRemove = 10;
			fraImgEff = new FrameImage(101, 40, 47);
			break;
		case 107:
			createRock();
			break;
		case 108:
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				isStop = true;
			}
			createPartical();
			break;
		case 109:
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				isStop = true;
			}
			fraImgEff = new FrameImage(223, 19, 15);
			numNextFrame = 2;
			fRemove = fraImgEff.nFrame * numNextFrame;
			y -= fraImgEff.frameHeight;
			if (Dir == 2)
			{
				x -= fraImgEff.frameWidth;
			}
			break;
		case 110:
		case 115:
			if (GameCanvas.isLowGraOrWP_PvP())
			{
				isStop = true;
			}
			createRock();
			break;
		case 111:
		{
			fRemove = 60;
			fraImgEff = new FrameImage(238, 30, 73);
			fraImgSubEff = new FrameImage(67, 3, 25, 1);
			fraImgSub2Eff = new FrameImage(239, 38, 22, 38, 22);
			for (int k = 0; k < 3; k++)
			{
				Point point4 = new Point(CRes.random_Am_0(20), -5 + CRes.random(10));
				point4.vy = -CRes.random(12, 20);
				point4.frame = CRes.random(fraImgSubEff.nFrame);
				VecEff.addElement(point4);
			}
			break;
		}
		case 112:
			levelPaint = -1;
			fraImgEff = new FrameImage(246, 49, 21, 35, 15, 4);
			fRemove = 40;
			break;
		case 113:
			fraImgEff = new FrameImage(252, 62, 64, 40, 41);
			fraImgSubEff = new FrameImage(174, 40, 40, 4);
			vMax = 12;
			if (typeSub == 2)
			{
				vMax = 14;
			}
			fRemove = 10;
			goc_Arc = CRes.random(90);
			break;
		case 114:
			vx = 3;
			if (Dir == 0)
			{
				vx = -3;
			}
			fraImgEff = new FrameImage(259, 30, 14);
			fRemove = 4;
			numNextFrame = 2;
			break;
		case 116:
			if (typeSub == 1)
			{
				levelPaint = -1;
			}
			fraImgEff = new FrameImage(268, 24, 32);
			fRemove = 20;
			break;
		case 117:
			levelPaint = -1;
			fraImgEff = new FrameImage(269, 24, 10);
			if (CRes.random(3) == 0)
			{
				frame = 1;
			}
			fRemove = CRes.random(16, 30);
			break;
		case 118:
			fraImgEff = new FrameImage(144, 37, 55);
			numNextFrame = 3;
			fRemove = 6;
			break;
		case 119:
			createEndLuS1L4();
			break;
		case 121:
			mPlayFrame = new int[5] { 15328484, 16118000, 16578808, 16777215, 16777215 };
			fRemove = 16;
			break;
		case 122:
			fRemove = 10;
			numNextFrame = 2;
			fraImgEff = new FrameImage(289, 60, 60, 3);
			break;
		case 123:
			fRemove = 8;
			numNextFrame = 2;
			fraImgEff = new FrameImage(290, 60, 60, 4);
			if (typeSub > 3)
			{
				typeSub = 3;
			}
			break;
		case 124:
			fraImgEff = new FrameImage(246, 49, 21, 4);
			mPlayFrame = new int[28]
			{
				3, 1, 0, 5, 5, 5, 5, 5, 5, 5,
				5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
				5, 5, 5, 5, 0, 1, 2, 3
			};
			fRemove = mPlayFrame.Length;
			levelPaint = -1;
			break;
		case 125:
			mPlayFrame = new int[18]
			{
				0, 1, 2, 3, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 3, 2, 1, 0
			};
			fRemove = mPlayFrame.Length;
			fraImgEff = new FrameImage(294, 57, 57, 3);
			break;
		case 127:
			create_Poke_Begin();
			break;
		case 128:
			create_Poke_Fail();
			break;
		case 126:
			create_Poke_Ok();
			break;
		case 129:
			create_Tru_Be();
			break;
		case 130:
			createLOL_Tru_Tren();
			break;
		case 131:
			fraImgEff = new FrameImage(306, 38, 22, 3);
			break;
		case 132:
			fraImgEff = new FrameImage(309, 78, 70, 4);
			if (typeSub == 0)
			{
				fRemove = 4;
				numNextFrame = 1;
				vx = -6;
				if (Dir == 2)
				{
					vx = 6;
				}
			}
			else
			{
				fRemove = 16;
				numNextFrame = 1;
				vx = -16;
				if (Dir == 2)
				{
					vx = 16;
				}
			}
			break;
		case 133:
			if (typeSub == 1)
			{
				fraImgEff = new FrameImage(314, 96, 39);
			}
			else
			{
				fraImgEff = new FrameImage(312, 121, 77);
			}
			fRemove = 30;
			levelPaint = -1;
			break;
		case 134:
			fraImgEff = new FrameImage(314, 96, 39);
			fraImgSubEff = new FrameImage(315, 77, 54, 3);
			fRemove = 30;
			levelPaint = -1;
			break;
		case 135:
			fraImgEff = new FrameImage(320, 62, 44);
			fRemove = 4;
			numNextFrame = 2;
			vx = -20;
			if (Dir == 2)
			{
				vx = 20;
			}
			break;
		case 136:
			fraImgEff = new FrameImage(321, 52, 70, 3);
			numNextFrame = 1;
			mPlayFrameVip = new int[20][]
			{
				new int[1],
				new int[1] { 3 },
				new int[0],
				new int[0],
				new int[0],
				new int[1] { 1 },
				new int[1] { 4 },
				new int[0],
				new int[0],
				new int[0],
				new int[1] { 2 },
				new int[1] { 5 },
				new int[0],
				new int[3] { 3, 2, 1 },
				new int[3] { 0, 2, 4 },
				new int[3] { 3, 5, 4 },
				new int[0],
				new int[3] { 3, 2, 1 },
				new int[3] { 0, 2, 4 },
				new int[3] { 3, 5, 4 }
			};
			fRemove = mPlayFrameVip.Length;
			break;
		case 137:
			createZoro_S3();
			break;
		case 139:
			if (typeSub == 1)
			{
				fraImgEff = new FrameImage(407, 52, 199, 1);
			}
			else
			{
				fraImgEff = new FrameImage(327, 38, 198, 1);
			}
			if (typeSub == 1)
			{
				mPlayFrame = new int[10] { 0, 0, -1, 1, -1, 0, 0, -1, 1, 1 };
			}
			else
			{
				mPlayFrame = new int[9] { 0, 0, -1, 1, -1, 2, 2, -1, 3 };
			}
			fRemove = mPlayFrame.Length;
			break;
		case 140:
		case 167:
			create_US_S2_L5();
			break;
		case 141:
			frameSuper = typeSub;
			fraImgEff = new FrameImage(335, 80, 80, (sbyte)2, frameSuper);
			fRemove = 8;
			numNextFrame = 2;
			break;
		case 143:
			if (typeSub >= 0 && typeSub <= 11)
			{
				fraImgEff = new FrameImage(338 + typeSub, 1);
				fRemove = 30;
				vy = -9 + CRes.random_Am_0(3);
			}
			else
			{
				isStop = true;
			}
			break;
		case 144:
			indexColorStar = typeSub;
			levelPaint = -1;
			x1000 = x * 1000;
			y1000 = y * 1000;
			fRemove = CRes.random(4, 6);
			vMax = 7;
			xline = 15;
			yline = 20;
			maxsize = 8;
			create_Star_Line_In(vMax, xline, yline, 0, maxsize);
			break;
		case 145:
			indexColorStar = typeSub;
			levelPaint = -1;
			x1000 = x * 1000;
			y1000 = y * 1000;
			fRemove = CRes.random(4, 6);
			vMax = 5;
			xline = 10;
			yline = 15;
			maxsize = 4;
			create_Star_Line_In(vMax, xline, yline, 0, maxsize);
			break;
		case 146:
		{
			fraImgEff = new FrameImage(85, 34, 34, 28, 28);
			int num = 30;
			for (int i = 0; i < 4; i++)
			{
				Point point = new Point(x * 1000, y * 1000);
				point.vx = CRes.getcos(num) * 7;
				point.vy = CRes.getsin(num) * 7;
				VecEff.addElement(point);
				num = ((i != 0 && i != 2) ? (num + 60) : (num + 120));
			}
			num = 0;
			for (int j = 0; j < 4; j++)
			{
				Point point2 = new Point(x * 1000, y * 1000);
				point2.vx = CRes.getcos(num) * 12;
				point2.vy = CRes.getsin(num) * 12;
				VecEff.addElement(point2);
				num += 90;
			}
			fRemove = 10;
			break;
		}
		case 147:
			create_Lucci_L2();
			break;
		case 148:
			fraImgEff = new FrameImage(312, 121, 77);
			fRemove = 20;
			levelPaint = -1;
			break;
		case 149:
			fraImgEff = new FrameImage(327, 38, 198, 1);
			fRemove = 4;
			numNextFrame = 1;
			y = objTo.y;
			break;
		case 150:
			fraImgEff = new FrameImage(367, 35, 14);
			fRemove = 1000;
			numNextFrame = 2;
			y = objTo.y - objTo.hOne;
			break;
		case 178:
		{
			fraImgEff = new FrameImage(mImage.createImage("/eff/goal.png"), 4);
			fRemove = 30;
			fraImgSub2Eff = new FrameImage(mImage.createImage("/eff/khungthanh.png"), 1);
			fraImgSubEff = new FrameImage(mImage.createImage("/eff/ball.png"), 4);
			x = objTo.x;
			y = objTo.y;
			toX = x + 200;
			toY = y - 30;
			vx = 5;
			vy = -1;
			vMax = 10;
			xdich = toX - x;
			ydich = toY - y - CRes.random(20);
			create_Speed(xdich, ydich, null);
			fRemove = 40;
			objTo.Dir = 2;
			objTo.Action = 2;
			MainSkill skill3 = new MainSkill(9999, 280);
			objTo.plashNow = new Plash(skill3, objTo, null);
			break;
		}
		case 151:
			fraImgEff = new FrameImage(87, 35, 35, 5);
			fRemove = 5;
			numNextFrame = 1;
			y = objTo.y - objTo.hOne / 2;
			break;
		case 152:
			fraImgEff = new FrameImage(368, 56, 92);
			fRemove = 1000;
			mPlayFrame = new int[14]
			{
				-1, -1, 0, 0, 0, 0, 1, 1, 1, 1,
				1, 1, 1, 1
			};
			y = objTo.y + 4;
			break;
		case 153:
		{
			fraImgEff = new FrameImage(369, 37, 59);
			fraImgSubEff = new FrameImage(370, 14, 16);
			fraImgSub2Eff = new FrameImage(221, 5, 5, 4);
			fRemove = 250;
			objTo.Action = 2;
			MainSkill skill2 = new MainSkill(9999, 256);
			objTo.plashNow = new Plash(skill2, objTo, null);
			x = objTo.x + 3;
			if (objTo.type_left_right == 0)
			{
				x = objTo.x - 3;
			}
			break;
		}
		case 156:
			fraImgEff = new FrameImage(381, 64, 35);
			vy = -6;
			y = objTo.y - objTo.hOne;
			y1000 = 0;
			fRemove = 1000;
			break;
		case 157:
			fraImgEff = new FrameImage(382, 58, 80, 3);
			vx = CRes.random(3);
			if (Dir == 0)
			{
				vx *= -1;
			}
			y -= GameCanvas.gameTick % 10 * 2;
			fRemove = 15;
			f = CRes.random(4);
			levelPaint = -1;
			break;
		case 158:
		{
			Dir = (sbyte)objTo.type_left_right;
			fraImgEff = new FrameImage(385, 15, 27);
			fraImgSubEff = new FrameImage(386, 12, 22);
			fraImgSub2Eff = new FrameImage(221, 5, 5, 4);
			fraImgSub3Eff = new FrameImage(18, 20, 21);
			fRemove = 130;
			objTo.Action = 2;
			MainSkill skill = new MainSkill(9999, 257);
			y -= 15;
			objTo.plashNow = new Plash(skill, objTo, null);
			x = objTo.x + 25;
			x1000 = x - 14;
			if (Dir == 0)
			{
				x = objTo.x - 25;
				x1000 = x + 14;
			}
			y1000 = y;
			am_duong = 1;
			if (Dir == 2)
			{
				am_duong = -1;
			}
			break;
		}
		case 160:
			create_EE_Valentine();
			break;
		case 161:
			create_EE_Valentine_stand();
			break;
		case 162:
			create_EE_LAW_HEART();
			break;
		case 163:
			fraImgEff = new FrameImage(392, 101, 44);
			levelPaint = -1;
			fRemove = 190;
			x1000 = x;
			y1000 = y + 24;
			timeBegin = GameCanvas.timeNow;
			break;
		case 43:
		case 67:
		case 94:
		case 165:
			break;
		}
	}

	private void create_EE_LAW_HEART()
	{
		GameScreen.beginPaintSpec();
		MainSkill skill = new MainSkill(9999, 265);
		objTo.Action = 2;
		objTo.plashNow = new Plash(skill, objTo, null);
		if (objTo == GameScreen.player)
		{
			Player.isSendMove = false;
		}
		fraImgEff = new FrameImage(396, 20, 20);
		fraImgSubEff = new FrameImage(393, 110, 110);
		fraImgSub2Eff = new FrameImage(394, 126, 41);
		fraImgSub3Eff = new FrameImage(395, 69, 32);
		timeBegin = GameCanvas.timeNow;
		fRemove = 190;
		x1000 = x;
		y1000 = y + 24;
		x -= 30;
		y -= 60;
		vy = -2;
		vx = 0;
		GameScreen.addEffectEnd_ObjTo(163, 0, objTo.x, objTo.y, objTo.ID, objTo.typeObject, 0, objTo);
	}

	private void create_EE_Valentine_stand()
	{
		fraImgEff = new FrameImage(388, 8, 7, 4);
		fraImgSubEff = new FrameImage(389, 11, 11, 2);
		frame = CRes.random(2);
		mPlayFrameVip = new int[4][]
		{
			new int[16]
			{
				1, 1, 1, 1, 0, 1, 0, 1, 1, 1,
				1, 1, 0, 1, 0, 1
			},
			new int[8] { 1, 1, 1, 1, 0, 1, 0, 1 },
			new int[13]
			{
				1, 1, 1, 0, 1, 0, 1, 1, 1, 0,
				1, 0, 1
			},
			new int[25]
			{
				1, 1, 1, 0, 1, 0, 1, 1, 1, 0,
				1, 0, 1, 1, 1, 0, 1, 0, 1, 1,
				1, 0, 1, 0, 1
			}
		};
		x = objTo.x + CRes.random_Am_0(10);
		y = objTo.y - objTo.hOne / 2 - CRes.random(10);
		vy = -2;
		vx = CRes.random_Am_0(3);
		randomf = -3 + CRes.random(6);
		randomf2 = CRes.random(mPlayFrameVip.Length);
		lengthM = mPlayFrameVip[randomf2].Length;
		fRemove = 25 + mPlayFrameVip[randomf2].Length;
		if (CRes.random(5) == 0)
		{
			levelPaint = -1;
		}
	}

	private void create_EE_Valentine()
	{
		fraImgEff = new FrameImage(388, 8, 7, 4);
		for (int i = 0; i < 4; i++)
		{
			Point point = new Point();
			point.x = objTo.x + CRes.random_Am_0(5);
			point.y = objTo.y - objTo.hOne + 3 + i * 10 + CRes.random_Am_0(3) + GameCanvas.gameTick % 5 * 3;
			point.vx = -(2 + CRes.random(2));
			point.dis = 0;
			if (objTo.type_left_right == 0)
			{
				point.vx = 2 + CRes.random(2);
				point.dis = 2;
			}
			point.x += point.vx * 4;
			if (CRes.random(4) == 0)
			{
				point.frame = CRes.random(4);
			}
			else
			{
				point.frame = 2 + CRes.random(2);
			}
			point.vy = -1;
			VecEff.addElement(point);
		}
		fRemove = 17;
		if (CRes.random(5) != 0)
		{
			levelPaint = -1;
		}
	}

	private void create_upgrade6()
	{
		colorpaint = new int[7] { 15340368, 14454331, 16314661, 3863335, 2600435, 1339473, 13181375 };
		vMax = 40;
		fRemove = 20;
		levelPaint = -1;
		int num = 270;
		for (int i = 0; i < 5; i++)
		{
			Point point = new Point();
			point.dis = num;
			point.x = CRes.getcos(CRes.fixangle(point.dis)) * vMax / 1000 + x;
			point.y = CRes.getsin(CRes.fixangle(point.dis)) * vMax / 1000 + y;
			VecEff.addElement(point);
			num += 72;
		}
		if (typeEffect == 155)
		{
			fraImgEff = new FrameImage(220, 9, 9, 4);
		}
	}

	private void create_Lucci_L2()
	{
		am_duong = 1;
		if (Dir == 2)
		{
			am_duong = -1;
		}
		fraImgEff = new FrameImage(274, 23, 74, 3);
		frame = 0;
		vx = am_duong * 12;
		x1000 = x;
		x = x1000 - am_duong * 24;
		fRemove = 20;
		if (typeSub == 1)
		{
			fraImgSubEff = new FrameImage(273, 24, 24, 4);
			frame = 1;
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
	}

	public override void paint(mGraphics g)
	{
		try
		{
			switch (typeEffect)
			{
			case 166:
				break;
			case 0:
			case 1:
			case 3:
			case 4:
			case 5:
			case 8:
			case 10:
			case 12:
			case 18:
			case 24:
			case 28:
			case 34:
			case 36:
			case 41:
			case 42:
			case 53:
			case 55:
			case 57:
			case 61:
			case 71:
			case 77:
			case 80:
			case 81:
			case 86:
			case 89:
			case 90:
			case 114:
			case 118:
			case 135:
			case 143:
			case 150:
			case 171:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				break;
			case 178:
				if (f > 15)
				{
					fraImgEff.drawFrame(f % fraImgEff.nFrame, toX, toY - 50, 0, 3, g);
				}
				fraImgSub2Eff.drawFrame(0, toX, toY, 0, 3, g);
				fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, x, y, 0, 3, g);
				break;
			case 151:
				fraImgEff.drawFrameNew(typeSub * fraImgEff.maxNumFrame + f / numNextFrame % fraImgEff.maxNumFrame, x, y, Dir, 3, g);
				break;
			case 9:
			case 45:
			case 52:
			case 58:
			case 65:
			case 68:
			case 170:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 33, g);
				break;
			case 149:
				fraImgEff.drawFrameNew(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 33, g);
				break;
			case 141:
				if (f / numNextFrame % 4 > 1)
				{
					fraImgEff.drawFrameNew(f / numNextFrame % 4, x, y, Dir, 33, g);
				}
				else
				{
					fraImgEff.drawFrameNew_BeginSuper(f / numNextFrame % 4, x, y, Dir, 33, g);
				}
				break;
			case 6:
				if (typeSub == 3)
				{
					for (int num26 = 0; num26 < VecEff.size(); num26++)
					{
						Point point24 = (Point)VecEff.elementAt(num26);
						fraImgSub2Eff.drawFrameNew(indexEff_1 * fraImgEff.maxNumFrame + point24.f % fraImgEff.maxNumFrame, point24.x / 100, point24.y / 100, 0, 3, g);
					}
				}
				fraImgEff.drawFrameNew(indexEff_1 * fraImgEff.maxNumFrame + f / numNextFrame % fraImgEff.maxNumFrame, x / 100, y / 100, Dir, 3, g);
				if (fraImgSubEff != null)
				{
					fraImgSubEff.drawFrame(CRes.random(fraImgSubEff.nFrame), x / 100, y / 100, Dir, 3, g);
				}
				break;
			case 11:
			{
				for (int num46 = 0; num46 < VecEff.size(); num46++)
				{
					Point point37 = (Point)VecEff.elementAt(num46);
					point37.fraImgEff.drawFrame(point37.f / numNextFrame % point37.fraImgEff.nFrame, point37.x, point37.y, 0, 3, g);
				}
				break;
			}
			case 47:
			{
				for (int num52 = 0; num52 < VecEff.size(); num52++)
				{
					Point point42 = (Point)VecEff.elementAt(num52);
					fraImgEff.drawFrame(point42.f / numNextFrame % fraImgEff.nFrame, point42.x, point42.y, 0, 3, g);
				}
				break;
			}
			case 13:
				if (fraImgSubEff != null)
				{
					for (int num18 = 0; num18 < VecEff.size(); num18++)
					{
						Point point18 = (Point)VecEff.elementAt(num18);
						fraImgSubEff.drawFrame(point18.f % fraImgSubEff.nFrame, point18.x, point18.y, Dir, 3, g);
					}
				}
				if (f <= fRemove)
				{
					fraImgEff.drawFrame((f / numNextFrame + randomf) % fraImgEff.nFrame, x, y, Dir, 3, g);
					if (fraImgSub2Eff != null)
					{
						fraImgSub2Eff.drawFrame(f % fraImgSub2Eff.nFrame, x, y, Dir, 3, g);
					}
				}
				break;
			case 16:
				fraImgEff.drawFrameNew(indexEff_1 * fraImgEff.maxNumFrame + CRes.abs(typeSub), x, y, Dir, 3, g);
				break;
			case 72:
				fraImgEff.drawFrame(CRes.abs(typeSub), x, y, Dir, 3, g);
				break;
			case 19:
				if (f < 6)
				{
					fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				}
				if (f > 1)
				{
					fraImgSubEff.drawFrame((f - 2 / numNextFrame) % fraImgSubEff.nFrame, x, y, Dir, 3, g);
				}
				break;
			case 21:
			{
				for (int num34 = 0; num34 < VecEff.size(); num34++)
				{
					Point point29 = (Point)VecEff.elementAt(num34);
					fraImgEff.drawFrame(point29.frame, point29.x, point29.y, 0, 3, g);
				}
				if (f < 6)
				{
					fraImgSubEff.drawFrame(f / numNextFrame % fraImgSubEff.nFrame, x, y, Dir, 3, g);
				}
				break;
			}
			case 20:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, randomf, 3, g);
				break;
			case 22:
			{
				for (int num7 = 0; num7 < VecEff.size(); num7++)
				{
					Point point13 = (Point)VecEff.elementAt(num7);
					fraImgSubEff.drawFrame(point13.f / numNextFrame % fraImgSubEff.nFrame, point13.x, point13.y, Dir, 3, g);
				}
				if (isUpdateNormal)
				{
					fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				}
				break;
			}
			case 23:
			{
				if (f < 6)
				{
					fraImgSub2Eff.drawFrame((f / numNextFrame + randomf) % fraImgSub2Eff.nFrame, x1000, y1000, Dir, 3, g);
				}
				if (f <= fRemove)
				{
					fraImgEff.drawFrame((f / numNextFrame + randomf) % fraImgEff.nFrame, x, y, Dir, 3, g);
				}
				for (int num63 = 0; num63 < VecEff.size(); num63++)
				{
					Point point51 = (Point)VecEff.elementAt(num63);
					fraImgSubEff.drawFrame(point51.f / numNextFrame % fraImgSubEff.nFrame, point51.x, point51.y, Dir, 3, g);
				}
				break;
			}
			case 26:
				if (f < mPlayFrame.Length)
				{
					fraImgEff.drawFrame(mPlayFrame[f], x, y, Dir, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
				if (f > 0 && f < mPlayFrame.Length + 1)
				{
					fraImgEff.drawFrame(mPlayFrame[f - 1], x, y, (Dir == 0) ? 2 : 0, 33, g);
				}
				break;
			case 27:
				if (f <= fRemove)
				{
					int num = f / 2;
					if (num > typeSub)
					{
						num = typeSub;
					}
					fraImgEff.drawFrame(num, x, y, Dir, 3, g);
				}
				if (typeSub == 2)
				{
					for (int m = 0; m < VecEff.size(); m++)
					{
						Point point6 = (Point)VecEff.elementAt(m);
						fraImgSubEff.drawFrame(point6.f % fraImgSubEff.nFrame, point6.x, point6.y, Dir, 3, g);
					}
				}
				break;
			case 29:
			{
				if (f >= 0 && f < 4)
				{
					fraImgEff.drawFrame(f / 2, x1000, y1000, Dir, 3, g);
				}
				if (f < fRemove && f > 1)
				{
					fraImgSubEff.drawFrame(0, x, y, Dir, 3, g);
				}
				for (int num41 = 0; num41 < VecEff.size(); num41++)
				{
					Point point33 = (Point)VecEff.elementAt(num41);
					fraImgSub2Eff.drawFrame(point33.f % fraImgSub2Eff.nFrame, point33.x, point33.y, Dir, 3, g);
				}
				break;
			}
			case 30:
			{
				for (int num37 = 0; num37 < VecEff.size(); num37++)
				{
					Line line = (Line)VecEff.elementAt(num37);
					if (line != null)
					{
						int color = 0;
						if (num37 / 2 < colorpaint.Length)
						{
							color = colorpaint[num37 / 2];
						}
						g.setColor(color);
						g.drawLine(line.x0 / 1000, line.y0 / 1000, line.x1 / 1000, line.y1 / 1000);
						if (line.is2Line)
						{
							g.drawLine(line.x0 / 1000 + 1, line.y0 / 1000, line.x1 / 1000 + 1, line.y1 / 1000);
						}
					}
				}
				break;
			}
			case 25:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				if (typeSub != 1 && typeSub != 2 && typeSub != 3)
				{
					break;
				}
				if (f < 4)
				{
					fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, x1000, y, Dir, 3, g);
				}
				if (typeSub == 2)
				{
					for (int num19 = 0; num19 < VecEff.size(); num19++)
					{
						Point point19 = (Point)VecEff.elementAt(num19);
						fraImgSub2Eff.drawFrame(point19.f % fraImgSub2Eff.nFrame, point19.x, point19.y, Dir, 3, g);
					}
				}
				break;
			case 35:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 3, g);
				break;
			case 37:
			{
				for (int num58 = 0; num58 < VecEff.size(); num58++)
				{
					Point point47 = (Point)VecEff.elementAt(num58);
					fraImgSub2Eff.drawFrame(point47.f % fraImgSub2Eff.nFrame, point47.x, point47.y, Dir, 3, g);
				}
				if (f >= 0 && f < 6)
				{
					fraImgEff.drawFrame(f / 2, x1000, y1000, Dir, 3, g);
				}
				if (f < fRemove && f > 1)
				{
					int num59 = f / 2;
					if (num59 > 2)
					{
						num59 = 2;
					}
					fraImgSubEff.drawFrame(num59, x, y, Dir, 3, g);
				}
				break;
			}
			case 38:
			{
				for (int num50 = 0; num50 < VecEff.size(); num50++)
				{
					Point point40 = (Point)VecEff.elementAt(num50);
					fraImgEff.drawFrame(point40.f % fraImgEff.nFrame, point40.x, point40.y, Dir, 3, g);
				}
				break;
			}
			case 138:
			{
				for (int num45 = 0; num45 < VecEff.size(); num45++)
				{
					Point point36 = (Point)VecEff.elementAt(num45);
					fraImgEff.drawFrameNew(point36.frame * 4 + point36.f % fraImgEff.nFrame, point36.x, point36.y, Dir, 3, g);
				}
				break;
			}
			case 39:
			{
				int num42 = f;
				if (num42 > 2)
				{
					num42 = 2 + CRes.random(2);
				}
				fraImgEff.drawFrame(num42, x, y, Dir, 3, g);
				break;
			}
			case 40:
				if (f % 2 == 0)
				{
					fraImgSubEff.drawFrame(0, x, y + 12, Dir, 3, g);
				}
				fraImgEff.drawFrame(3, x, y, Dir, 3, g);
				if (f % 2 != 0)
				{
					break;
				}
				if (typeSub == 1 || typeSub == 2)
				{
					fraImgSub2Eff.drawFrameNew(0, x - 12, y + 12 + f * 14, Dir, 33, g);
					if (typeSub == 2)
					{
						fraImgSub2Eff.drawFrameNew(1, x + 12, y + 12 + f * 14, Dir, 33, g);
					}
					else
					{
						fraImgSub2Eff.drawFrameNew(0, x + 12, y + 12 + f * 14, Dir, 33, g);
					}
				}
				else
				{
					fraImgSub2Eff.drawFrameNew(0, x, y + 12 + f * 14, Dir, 33, g);
				}
				break;
			case 175:
				fraImgSub3Eff.drawFrame(f / 2 % fraImgSub3Eff.nFrame, x, y + 73 + 10, CRes.random(2) * 2, 33, g);
				fraImgSub2Eff.drawFrame(0, x, y, CRes.random(2) * 2, 17, g);
				if (f % 2 == 0)
				{
					fraImgSubEff.drawFrame(0, x, y + 12, Dir, 3, g);
				}
				fraImgEff.drawFrame(3, x, y, Dir, 3, g);
				break;
			case 120:
			{
				if (f < 10)
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 3, g);
				}
				for (int num8 = 0; num8 < VecEff.size(); num8++)
				{
					Point point14 = (Point)VecEff.elementAt(num8);
					if (point14.frame < 2)
					{
						fraImgSubEff.drawFrameNew(point14.fSmall * fraImgSubEff.maxNumFrame + point14.f / 2 % fraImgSubEff.maxNumFrame, point14.x, point14.y, 0, 3, g);
					}
					else
					{
						fraImgSub2Eff.drawFrameNew(point14.fSmall * fraImgSub2Eff.maxNumFrame + point14.f / 2 % fraImgSub2Eff.maxNumFrame, point14.x, point14.y, 0, 3, g);
					}
				}
				break;
			}
			case 168:
			{
				if (f < 10)
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 3, g);
				}
				for (int j = 0; j < VecEff.size(); j++)
				{
					Point point3 = (Point)VecEff.elementAt(j);
					if (point3.frame < 2)
					{
						fraImgSubEff.drawFrameNew(point3.fSmall * fraImgSubEff.maxNumFrame + point3.f / 2 % fraImgSubEff.maxNumFrame, point3.x, point3.y, 0, 3, g);
					}
					else
					{
						fraImgSub2Eff.drawFrameNew(point3.fSmall * fraImgSub2Eff.maxNumFrame + point3.f / 2 % fraImgSub2Eff.maxNumFrame, point3.x, point3.y, 0, 3, g);
					}
				}
				break;
			}
			case 169:
				if (f < 10)
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 3, g);
				}
				break;
			case 48:
			case 50:
			{
				if (f < 10)
				{
					fraImgEff.drawFrame(f / 2, x, y, Dir, 3, g);
				}
				for (int num54 = 0; num54 < VecEff.size(); num54++)
				{
					Point point44 = (Point)VecEff.elementAt(num54);
					if (point44.frame < 2)
					{
						fraImgSubEff.drawFrame(point44.f / 2 % fraImgSubEff.nFrame, point44.x, point44.y, 0, 3, g);
					}
					else
					{
						fraImgSub2Eff.drawFrame(point44.f / 2 % fraImgSubEff.nFrame, point44.x, point44.y, 0, 3, g);
					}
				}
				break;
			}
			case 49:
				paint_Bullet(g, fraImgEff, frame, x, y, isMore: false, f % 2 * 3);
				break;
			case 54:
			{
				for (int num43 = 0; num43 < VecEff.size(); num43++)
				{
					Point point34 = (Point)VecEff.elementAt(num43);
					if (typeSub == 1 || typeSub == 3 || typeSub == 10 || typeSub == 11 || typeSub == 12)
					{
						fraImgEff.drawFrame(3, point34.x, point34.y, Dir, 3, g);
					}
					else if (typeSub >= 4 && typeSub <= 7)
					{
						fraImgEff.drawFrameNew(frame * fraImgEff.maxNumFrame + point34.f % fraImgEff.maxNumFrame, point34.x, point34.y, Dir, 3, g);
					}
					else if (typeSub == 8)
					{
						fraImgEff.drawFrameNew((2 + num43 % 2) * fraImgEff.maxNumFrame + point34.f % fraImgEff.maxNumFrame, point34.x, point34.y, Dir, 3, g);
					}
					else if (typeSub == 9)
					{
						fraImgEff.drawFrameNew((frame + num43 % 2) * fraImgEff.maxNumFrame + point34.f % fraImgEff.maxNumFrame, point34.x, point34.y, Dir, 3, g);
					}
					else
					{
						fraImgEff.drawFrame(point34.f % fraImgEff.nFrame, point34.x, point34.y, Dir, 3, g);
					}
				}
				if (fraImgSubEff != null)
				{
					for (int num44 = 0; num44 < vecSubEff.size(); num44++)
					{
						Point point35 = (Point)vecSubEff.elementAt(num44);
						fraImgSubEff.drawFrame(point35.f / 2 % fraImgSubEff.nFrame, point35.x, point35.y, Dir, 33, g);
					}
				}
				break;
			}
			case 56:
				if (f < 3)
				{
					fraImgEff.drawFrame(f, toX, toY, Dir, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
				else
				{
					fraImgEff.drawFrame(5 - f, x, y, Dir, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
				break;
			case 59:
			case 107:
			{
				for (int num38 = 0; num38 < VecEff.size(); num38++)
				{
					Point point31 = (Point)VecEff.elementAt(num38);
					fraImgEff.drawFrame(point31.frame, point31.x, point31.y, 0, 3, g);
				}
				break;
			}
			case 110:
			{
				for (int num39 = 0; num39 < VecEff.size(); num39++)
				{
					Point point32 = (Point)VecEff.elementAt(num39);
					if (point32.subType == 1)
					{
						fraImgSubEff.drawFrameNew(typeSub * 3 + point32.frame, point32.x, point32.y, 0, 3, g);
					}
					else
					{
						fraImgEff.drawFrameNew(typeSub * 4 + point32.frame, point32.x, point32.y, 0, 3, g);
					}
				}
				break;
			}
			case 115:
			{
				fraImgSubEff.drawFrame(0, x, y, 0, 33, g);
				for (int num33 = 0; num33 < VecEff.size(); num33++)
				{
					Point point28 = (Point)VecEff.elementAt(num33);
					fraImgEff.drawFrame(point28.frame, point28.x, point28.y, 0, 3, g);
				}
				break;
			}
			case 108:
			{
				for (int num36 = 0; num36 < VecEff.size(); num36++)
				{
					Point point30 = (Point)VecEff.elementAt(num36);
					if (point30.dis != 0)
					{
						fraImgSubEff.drawFrameNew(point30.frame + typeSub * 4, point30.x, point30.y, 0, 3, g);
					}
					else
					{
						fraImgEff.drawFrameNew(point30.frame + typeSub * 4, point30.x, point30.y, 0, 3, g);
					}
				}
				break;
			}
			case 60:
			{
				for (int num24 = 0; num24 < VecEff.size(); num24++)
				{
					Point point22 = (Point)VecEff.elementAt(num24);
					fraImgEff.drawFrame(point22.frame, point22.x, point22.y, 0, 3, g);
				}
				if (typeSub == 2)
				{
					if (f < fraImgSubEff.nFrame)
					{
						fraImgSubEff.drawFrame(f / numNextFrame % fraImgSubEff.nFrame, x, y, Dir, 3, g);
					}
				}
				else if (f < 6)
				{
					fraImgSubEff.drawFrame(f / numNextFrame % fraImgSubEff.nFrame, x, y, Dir, 3, g);
				}
				break;
			}
			case 62:
			{
				for (int num22 = 0; num22 < VecEff.size(); num22++)
				{
					Point point21 = (Point)VecEff.elementAt(num22);
					int num23 = point21.frame;
					if (f > fRemove - 4)
					{
						num23 += 3;
					}
					fraImgEff.drawFrame(num23, point21.x, point21.y, Dir, 3, g);
				}
				break;
			}
			case 66:
			{
				int num9 = 0;
				if (f < 2)
				{
					num9 = 1;
				}
				else if (f > fRemove - 4)
				{
					num9 = 2 - (fRemove - f) / 2;
				}
				fraImgEff.drawFrameNew(typeSub * 3 + num9, x, y, Dir, 3, g);
				break;
			}
			case 63:
				paintNo_Dat_New(g);
				break;
			case 64:
				fraImgEff.drawFrame(f / numNextFrame, x, y, 0, 3, g);
				fraImgEff.drawFrame(f / numNextFrame, x, y, 2, 3, g);
				break;
			case 69:
			case 70:
			{
				for (int num5 = 0; num5 < VecEff.size(); num5++)
				{
					Point point11 = (Point)VecEff.elementAt(num5);
					fraImgEff.drawFrame(point11.frame, objTo.x, objTo.y + point11.y, Dir, 33, g);
				}
				break;
			}
			case 83:
			{
				if (f < 6)
				{
					fraImgSubEff.drawFrame(f, objTo.x, objTo.y - objTo.hOne / 2, Dir, 3, g);
				}
				for (int n = 0; n < VecEff.size(); n++)
				{
					Point point7 = (Point)VecEff.elementAt(n);
					fraImgEff.drawFrame(point7.frame, objTo.x, objTo.y + point7.y, Dir, 33, g);
				}
				break;
			}
			case 73:
				g.setColor(0);
				g.drawLine(x, y, toX, toY);
				break;
			case 74:
				g.setColor(16777215);
				g.drawLine(x, y, toX, toY);
				break;
			case 154:
			{
				g.setColor(colorpaint[f % colorpaint.Length]);
				for (int i = 0; i < VecEff.size(); i++)
				{
					Point point = (Point)VecEff.elementAt(i);
					Point point2 = null;
					point2 = ((i >= VecEff.size() - 1) ? ((Point)VecEff.elementAt(0)) : ((Point)VecEff.elementAt(i + 1)));
					g.drawLine(point.x, point.y, point2.x, point2.y);
				}
				break;
			}
			case 155:
			{
				g.setColor(colorpaint[f % colorpaint.Length]);
				for (int num64 = 0; num64 < VecEff.size(); num64++)
				{
					Point point52 = (Point)VecEff.elementAt(num64);
					fraImgEff.drawFrameNew(point52.f % 9 + f % fraImgEff.nFrame, point52.x, point52.y, 0, 3, g);
				}
				break;
			}
			case 75:
			case 78:
			{
				for (int num60 = 0; num60 < VecEff.size(); num60++)
				{
					Point point48 = (Point)VecEff.elementAt(num60);
					if (point48.frame == 0)
					{
						fraImgSubEff.drawFrame(point48.f % fraImgSubEff.nFrame, point48.x, point48.y, 0, 3, g);
					}
					else
					{
						fraImgSub2Eff.drawFrame(point48.f % fraImgSub2Eff.nFrame, point48.x, point48.y, 0, 3, g);
					}
				}
				if (f < fRemove + 10)
				{
					fraImgEff.drawFrame(f / 2 % 2, x, y, 0, 3, g);
				}
				break;
			}
			case 76:
			{
				for (int num57 = 0; num57 < VecEff.size(); num57++)
				{
					Point point46 = (Point)VecEff.elementAt(num57);
					if (point46.frame == 0)
					{
						fraImgEff.drawFrame(point46.f % fraImgEff.nFrame, point46.x, point46.y, 0, 3, g);
					}
					else
					{
						fraImgSubEff.drawFrame(point46.f % fraImgSubEff.nFrame, point46.x, point46.y, 0, 3, g);
					}
				}
				break;
			}
			case 79:
				if (f > 6 || f % 2 == 0)
				{
					fraImgEff.drawFrame(frame, x, y, 0, 3, g);
				}
				break;
			case 177:
				if (f > 6 || f % 2 == 0)
				{
					fraImgEff.drawFrame(frame, x, y, 0, 3, g);
				}
				break;
			case 84:
			{
				for (int num49 = 0; num49 < VecEff.size(); num49++)
				{
					Point point39 = (Point)VecEff.elementAt(num49);
					fraImgEff.drawFrame(point39.frame, point39.x, point39.y, point39.dis, 3, g);
				}
				break;
			}
			case 85:
			{
				for (int num47 = 0; num47 < VecEff.size(); num47++)
				{
					Line line2 = (Line)VecEff.elementAt(num47);
					if (line2 == null)
					{
						continue;
					}
					if (line2.type < 4)
					{
						fraImgEff.drawFrame(line2.f % fraImgEff.nFrame, line2.x0 / 1000, line2.y0 / 1000, 0, 3, g);
						continue;
					}
					if (line2.type < 8)
					{
						fraImgSubEff.drawFrame(line2.f % fraImgSubEff.nFrame, line2.x0 / 1000, line2.y0 / 1000, 0, 3, g);
						continue;
					}
					int color2 = 0;
					if (num47 / 2 < colorpaint.Length)
					{
						color2 = colorpaint[num47 / 2];
					}
					g.setColor(color2);
					g.drawLine(line2.x0 / 1000, line2.y0 / 1000, line2.x1 / 1000, line2.y1 / 1000);
					if (line2.is2Line)
					{
						g.drawLine(line2.x0 / 1000 + 1, line2.y0 / 1000, line2.x1 / 1000 + 1, line2.y1 / 1000);
					}
				}
				break;
			}
			case 2:
				if (f < 4)
				{
					fraImgEff.drawFrame(f / 2, x, y - 5, 0, 3, g);
				}
				fraImgSubEff.drawFrame(f, x, y, 0, 33, g);
				break;
			case 14:
				if (f < 4)
				{
					fraImgEff.drawFrame(f / numNextFrame, x, y, 0, 33, g);
				}
				break;
			case 88:
				if (f < mPlayFrame.Length && fraImgEff.getImageFrame() != null)
				{
					g.drawRegion(fraImgEff.getImageFrame(), mPlayFrame[f] * fraImgEff.frameWidth, 0, fraImgEff.frameWidth, fraImgEff.frameHeight, 0, x, y, 33);
				}
				break;
			case 142:
				if (f < mPlayFrame.Length && fraImgEff.getImageFrame() != null)
				{
					if (mPlayFrame[f] == 4)
					{
						g.drawRegion(fraImgEff.getImageFrame(), mPlayFrame[f] * fraImgEff.frameWidth, fraImgEff.frameHeight - fraImgEff.frameHeight / 2, fraImgEff.frameWidth, fraImgEff.frameHeight / 2, 0, x, y, 33);
					}
					else
					{
						g.drawRegion(fraImgEff.getImageFrame(), mPlayFrame[f] * fraImgEff.frameWidth, 0, fraImgEff.frameWidth, fraImgEff.frameHeight, 0, x, y, 33);
					}
				}
				break;
			case 17:
			{
				int num31 = f;
				if (f >= fRemove)
				{
					num31 = 2 - (f - fRemove) / 3;
				}
				else if (num31 > 2)
				{
					num31 = 2;
				}
				fraImgEff.drawFrame(num31, x, y, 0, 3, g);
				break;
			}
			case 31:
			{
				for (int num32 = 0; num32 < VecEff.size(); num32++)
				{
					Point point27 = (Point)VecEff.elementAt(num32);
					if (point27.dis == 1)
					{
						fraImgEff.drawFrameNew(point27.f / 2 % fraImgEff.nFrame, point27.x, point27.y, 0, 33, g);
					}
					else if (point27.dis == 0)
					{
						fraImgSubEff.drawFrameNew((point27.frame + point27.f / 2) % fraImgSubEff.nFrame, point27.x, point27.y, 0, 3, g);
					}
				}
				break;
			}
			case 32:
			{
				for (int num29 = 0; num29 < VecEff.size(); num29++)
				{
					Point point26 = (Point)VecEff.elementAt(num29);
					if (fraImgEff.nFrame != 0)
					{
						if (point26.dis == 1)
						{
							fraImgEff.drawFrameNew(fraImgEff.nFrame - point26.f / 2 % fraImgEff.nFrame, point26.x, point26.y, 0, 33, g);
						}
						else if (point26.dis == 0)
						{
							fraImgSubEff.drawFrameNew((point26.frame + point26.f / 2) % fraImgSubEff.nFrame, point26.x, point26.y, 0, 3, g);
						}
					}
				}
				break;
			}
			case 87:
				fraImgEff.drawFrame(typeSub * 3 + 2, x / 100, y / 100, Dir, 3, g);
				break;
			case 51:
				if (f < fRemove)
				{
					fraImgSubEff.drawFrame(f % fraImgSubEff.nFrame, x, y, Dir, 3, g);
					if (fraImgEff != null)
					{
						fraImgEff.drawFrame(CRes.random(fraImgEff.nFrame), x, y, Dir, 3, g);
					}
				}
				break;
			case 7:
			{
				for (int num27 = 0; num27 < VecEff.size(); num27++)
				{
					Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(num27);
					paint_Bullet(g, fraImgEff, point_Focus2.frame, point_Focus2.x, point_Focus2.y, isMore: false, 0);
				}
				break;
			}
			case 91:
			case 92:
				if (f < mPlayFrame.Length)
				{
					fraImgEff.drawFrame(mPlayFrame[f], x, y, Dir, 3, g);
				}
				break;
			case 93:
			{
				for (int num25 = 0; num25 < VecEff.size(); num25++)
				{
					Point point23 = (Point)VecEff.elementAt(num25);
					fraImgEff.drawFrame(point23.frame, point23.x, point23.y, 0, 3, g);
				}
				break;
			}
			case 46:
			case 159:
				if (f % 3 <= 1 || f >= 6)
				{
					fraImgEff.drawFrame(0, x, y, Dir, 33, g);
				}
				if (f >= 2 && f <= 7)
				{
					fraImgSubEff.drawFrame((f - 2) / 3, x, y + 5, Dir, 33, g);
				}
				break;
			case 96:
			{
				for (int num11 = 0; num11 < VecEff.size(); num11++)
				{
					Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(num11);
					fraImgEff.drawFrame(f / 3 % 3, point_Focus.x, point_Focus.y, 0, 3, g);
				}
				break;
			}
			case 97:
				if (f % 2 == 1)
				{
					fraImgSubEff.drawFrame(0, x, y, Dir, 33, g);
				}
				else
				{
					fraImgEff.drawFrame(0, x, y, Dir, 33, g);
				}
				break;
			case 82:
				fraImgSubEff.drawFrame((f / numNextFrame + 3) % fraImgSubEff.nFrame, x, y, Dir, 33, g);
				if (f < 4)
				{
					fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 33, g);
				}
				break;
			case 98:
			{
				if (f < 12)
				{
					fraImgEff.drawFrameNew(f / 3, x, y, 0, 33, g);
				}
				for (int num3 = 0; num3 < VecEff.size(); num3++)
				{
					Point point9 = (Point)VecEff.elementAt(num3);
					fraImgSubEff.drawFrame(point9.f % 5, point9.x, point9.y, 0, 3, g);
				}
				break;
			}
			case 99:
			{
				if (f < 8)
				{
					fraImgSubEff.drawFrameNew(f / 2, x, y, 0, 33, g);
					fraImgEff.drawFrame(f / 2, x, y, 0, 33, g);
				}
				for (int num2 = 0; num2 < VecEff.size(); num2++)
				{
					Point point8 = (Point)VecEff.elementAt(num2);
					fraImgSub2Eff.drawFrame(point8.f % 5, point8.x, point8.y, 0, 3, g);
				}
				break;
			}
			case 172:
				fraImgEff.drawFrameNew(f / 3, x, y, 0, 33, g);
				break;
			case 173:
				fraImgEff.drawFrame(f / 2, x, y, Dir, 3, g);
				break;
			case 176:
				fraImgEff.drawFrame(f, x, y, Dir, 3, g);
				break;
			case 174:
				fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, objMainEff.x, objMainEff.y, 0, 33, g);
				break;
			case 101:
			{
				for (int k = 0; k < VecEff.size(); k++)
				{
					Point point4 = (Point)VecEff.elementAt(k);
					if (objTo != null)
					{
						fraImgEff.drawFrame(point4.f, objTo.x + point4.x, objTo.y + point4.y, 0, 3, g);
					}
				}
				break;
			}
			case 102:
				if (f < 10)
				{
					fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x, y, 0, 3, g);
				}
				else if (f < 26)
				{
					fraImgSubEff.drawFrame(f / 2 % 3, x, y, 0, 3, g);
				}
				else
				{
					fraImgSubEff.drawFrame(3 + f / 2 % 2, x, y, 0, 3, g);
				}
				break;
			case 103:
				if (f < mPlayFrame.Length && f >= 0)
				{
					fraImgEff.drawFrame(mPlayFrame[f], x, y, Dir, 33, g);
				}
				break;
			case 104:
			{
				for (int num62 = 0; num62 < VecEff.size(); num62++)
				{
					Point point50 = (Point)VecEff.elementAt(num62);
					fraImgEff.drawFrame(point50.f, MainScreen.cameraMain.xCam + point50.x, point50.y, 0, 3, g);
				}
				break;
			}
			case 105:
			{
				for (int num61 = 0; num61 < VecEff.size(); num61++)
				{
					Point point49 = (Point)VecEff.elementAt(num61);
					fraImgEff.drawFrame(point49.frame, MainScreen.cameraMain.xCam + point49.x, point49.y, 0, 3, g);
				}
				break;
			}
			case 106:
				fraImgEff.drawFrame(f / 2 % fraImgEff.nFrame, x, y, 0, 33, g);
				break;
			case 109:
				fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x, y, Dir, 0, g);
				break;
			case 111:
			{
				for (int num55 = 0; num55 < 5; num55++)
				{
					fraImgEff.drawFrame(0, x, y - num55 * fraImgEff.frameHeight + 5, CRes.random(2) * 2, 33, g);
				}
				fraImgSub2Eff.drawFrame(f / 2 % fraImgSub2Eff.nFrame, x, y + 9, 0, 33, g);
				for (int num56 = 0; num56 < VecEff.size(); num56++)
				{
					Point point45 = (Point)VecEff.elementAt(num56);
					fraImgSubEff.drawFrameNew(point45.frame, x + point45.x, y + point45.y, Dir, 33, g);
				}
				break;
			}
			case 112:
				fraImgEff.drawFrameNew(typeSub * 4 + frame, x, y, Dir, 3, g);
				break;
			case 113:
			{
				for (int num53 = VecEff.size() - 1; num53 >= 0; num53--)
				{
					Point point43 = (Point)VecEff.elementAt(num53);
					fraImgSubEff.drawFrameNew(point43.frame * fraImgSubEff.maxNumFrame + point43.f / 3 % fraImgSubEff.maxNumFrame, point43.x / 1000, point43.y / 1000, 0, 33, g);
				}
				if (f / 3 < fraImgEff.nFrame)
				{
					fraImgEff.drawFrame(f / 3, x, y, Dir, 33, g);
				}
				break;
			}
			case 116:
			{
				for (int num51 = 0; num51 < VecEff.size(); num51++)
				{
					Point point41 = (Point)VecEff.elementAt(num51);
					fraImgEff.drawFrame(point41.f % fraImgEff.nFrame, point41.x, point41.y, 0, 3, g);
				}
				break;
			}
			case 117:
				if (f > fRemove - 2 && f <= fRemove)
				{
					fraImgEff.drawFrame(fRemove - f, x, y, 0, 3, g);
				}
				else
				{
					fraImgEff.drawFrame(0, x, y, 0, 3, g);
				}
				break;
			case 119:
			{
				for (int num48 = 0; num48 < VecEff.size(); num48++)
				{
					Point point38 = (Point)VecEff.elementAt(num48);
					if (point38.f < mPlayFrame.Length)
					{
						fraImgEff.drawFrameNew(mPlayFrame[point38.f], point38.x, point38.y, 0, 3, g);
					}
				}
				break;
			}
			case 121:
				frame = f / 2;
				if (frame >= mPlayFrame.Length)
				{
					frame = mPlayFrame.Length - 1;
				}
				if (f < 10 || f % 2 == 0)
				{
					g.setColor(mPlayFrame[frame]);
					g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
				}
				break;
			case 122:
				fraImgEff.drawFrameNew(f / numNextFrame, x, y, Dir, 3, g);
				break;
			case 123:
				fraImgEff.drawFrameNew(typeSub * fraImgEff.maxNumFrame + f / numNextFrame, x, y, Dir, 3, g);
				break;
			case 124:
				if (mPlayFrame[f] == 5)
				{
					if (fraImgEff.getImageFrame() != null)
					{
						g.drawRegion(fraImgEff.getImageFrame(), 0, 0, 30, 21, 0, x - 15, y, 3);
						g.drawRegion(fraImgEff.getImageFrame(), 19, 0, 30, 21, 0, x + 15, y, 3);
					}
				}
				else
				{
					fraImgEff.drawFrameNew(mPlayFrame[f], x, y, 0, 3, g);
				}
				break;
			case 125:
			{
				int num40 = x;
				if (f < 3 || (f > 10 && f < 14))
				{
					num40 += CRes.random_Am_0(3);
				}
				fraImgEff.drawFrameNew(mPlayFrame[f], num40, y, 0, 33, g);
				break;
			}
			case 127:
			{
				int num35 = y;
				if (f < mposy.Length)
				{
					num35 -= mposy[f];
				}
				if (typeSub % 10 > 3 && typeSub % 10 != 7)
				{
					fraImgEff.drawFrameNew(frame * 2 + f / 3 % 2, x, num35, 0, 3, g);
				}
				else
				{
					fraImgEff.drawFrameNew(frame * 2, x, num35, 0, 3, g);
				}
				break;
			}
			case 128:
				fraImgEff.drawFrame(frame * 2, x / 100, y / 100, Dir, 3, g);
				break;
			case 126:
			{
				if (step == 0 || step == 3)
				{
					fraImgEff.drawFrame(frame * 2, x / 100, y / 100, Dir, 3, g);
				}
				else
				{
					fraImgEff.drawFrame(frame * 2 + 1, x / 100, y / 100, Dir, 3, g);
				}
				if (frame == 1)
				{
					fraImgSubEff.drawFrame(frame / 2 % fraImgSubEff.nFrame, x1000, y1000, Dir, 3, g);
				}
				for (int num30 = 0; num30 < VecEff.size(); num30++)
				{
					Point_Focus point_Focus3 = (Point_Focus)VecEff.elementAt(num30);
					if (point_Focus3.frame == 0)
					{
						fraImgSubEff.drawFrame(point_Focus3.f / 2 % fraImgSubEff.nFrame, point_Focus3.x, point_Focus3.y, Dir, 3, g);
					}
					if (point_Focus3.frame == 1)
					{
						fraImgSub2Eff.drawFrame(0, point_Focus3.x, point_Focus3.y, Dir, 3, g);
					}
				}
				break;
			}
			case 130:
				if (objTo != null)
				{
					for (int num28 = 0; num28 < VecEff.size(); num28++)
					{
						Point point25 = (Point)VecEff.elementAt(num28);
						fraImgEff.drawFrameNew(point25.frame, objTo.x + point25.x, objTo.y + point25.y, 0, 33, g);
					}
				}
				break;
			case 131:
				if (objTo != null)
				{
					fraImgEff.drawFrameNew(3 + f / 2 % fraImgEff.maxNumFrame, objTo.x, objTo.y + 4, 0, 33, g);
				}
				break;
			case 132:
				if (typeSub == 1)
				{
					fraImgEff.drawFrameNew(typeSub * fraImgEff.maxNumFrame + CRes.random(3), x, y, 0, 3, g);
				}
				else
				{
					fraImgEff.drawFrameNew(typeSub * fraImgEff.maxNumFrame + (3 - f), x, y, 0, 3, g);
				}
				break;
			case 133:
			{
				int idx2 = 1;
				if (f < 2 || f > fRemove - 3)
				{
					idx2 = 0;
				}
				fraImgEff.drawFrame(idx2, x, y, 0, 3, g);
				break;
			}
			case 134:
			{
				int idx = 1;
				if (f < 2 || f > fRemove - 3)
				{
					idx = 0;
					fraImgEff.drawFrame(idx, x, y, 0, 3, g);
				}
				else
				{
					fraImgEff.drawFrame(idx, x, y, 0, 3, g);
					fraImgSubEff.drawFrameNew(typeSub * fraImgSubEff.maxNumFrame + CRes.random(3), x - 38, y - 40, 0, 0, g);
				}
				break;
			}
			case 136:
				if (f < mPlayFrameVip.Length)
				{
					for (int num21 = 0; num21 < mPlayFrameVip[f].Length; num21++)
					{
						fraImgEff.drawFrameNew(mPlayFrameVip[f][num21], x, y, 0, 3, g);
					}
				}
				break;
			case 137:
			{
				for (int num20 = 0; num20 < VecEff.size(); num20++)
				{
					Point point20 = (Point)VecEff.elementAt(num20);
					fraImgEff.drawFrame(point20.f / 4 % fraImgEff.nFrame, point20.x, point20.y, Dir, 33, g);
				}
				break;
			}
			case 139:
				if (mPlayFrame[f] >= 0)
				{
					fraImgEff.drawFrameNew(mPlayFrame[f], x, y, 0, 33, g);
				}
				break;
			case 140:
			case 167:
			{
				int num13 = CRes.random(2);
				for (int num14 = 0; num14 < VecEff.size(); num14++)
				{
					int num15 = num14 + num13;
					if (num15 >= VecEff.size())
					{
						num15 = 0;
					}
					Point point17 = (Point)VecEff.elementAt(num15);
					if (point17.frame == 1)
					{
						if (f > fRemove - 2)
						{
							frame = 3;
						}
						else
						{
							frame = CRes.random(3);
						}
						if (typeEffect == 140)
						{
							fraImgEff.drawFrameNew(frame, point17.x, point17.y, 0, 33, g);
						}
						continue;
					}
					if (f > fRemove - 2)
					{
						frame = 3;
					}
					else
					{
						frame = CRes.random(3);
					}
					int num16 = 0;
					int num17 = 0;
					if (typeEffect == 167)
					{
						if (Dir == 0)
						{
							num16 = 10;
						}
						else if (Dir == 2)
						{
							num16 = -10;
						}
						num17 = 10;
					}
					fraImgSubEff.drawFrameNew(frame, point17.x + num16, point17.y + num17, 0, 33, g);
				}
				break;
			}
			case 146:
			{
				for (int num12 = 0; num12 < VecEff.size(); num12++)
				{
					Point point16 = (Point)VecEff.elementAt(num12);
					fraImgEff.drawFrame(3 + point16.f % 3, point16.x / 1000, point16.y / 1000, Dir, 3, g);
				}
				break;
			}
			case 147:
				paint_Lucci_L2(g);
				break;
			case 148:
				fraImgEff.drawFrameNew(0, x, y, 0, 3, g);
				break;
			case 152:
				if (mPlayFrame[f % mPlayFrame.Length] >= 0)
				{
					fraImgEff.drawFrame(mPlayFrame[f % mPlayFrame.Length], x, y, Dir, 33, g);
				}
				break;
			case 153:
			{
				fraImgEff.drawFrame(frame, x, y, 0, 33, g);
				fraImgSubEff.drawFrame(CRes.random(4), x, y - 4, 0, 33, g);
				for (int num10 = 0; num10 < VecEff.size(); num10++)
				{
					Point point15 = (Point)VecEff.elementAt(num10);
					if (CRes.random(2) == 0)
					{
						fraImgSub2Eff.drawFrameNew(CRes.random(4) + point15.frame * 4, point15.x, point15.y, Dir, 3, g);
					}
				}
				break;
			}
			case 156:
				fraImgEff.drawFrame(GameCanvas.language, x, y + y1000, Dir, 33, g);
				break;
			case 157:
				if (f >= 10)
				{
					fraImgEff.drawFrameNew(3 + (f - 10) / 2, x, y, Dir, 33, g);
				}
				else
				{
					fraImgEff.drawFrameNew(f / 2 % 3, x, y, Dir, 33, g);
				}
				break;
			case 158:
			{
				if (f < 35)
				{
					fraImgSubEff.drawFrame(0, x1000 - am_duong * 3, y1000 + 12, Dir, 33, g);
				}
				fraImgEff.drawFrame(f / 3 % fraImgEff.nFrame, x, y + 5, Dir, 33, g);
				if (f < 30)
				{
					fraImgSub3Eff.drawFrame(CRes.random(4), x1000 - am_duong * 13, y1000 + 6 - f / 8, Dir, 3, g);
				}
				for (int num6 = 0; num6 < VecEff.size(); num6++)
				{
					Point point12 = (Point)VecEff.elementAt(num6);
					if (CRes.random(2) == 0)
					{
						fraImgSub2Eff.drawFrameNew(CRes.random(4) + point12.frame * 4, point12.x, point12.y, Dir, 3, g);
					}
				}
				break;
			}
			case 160:
			{
				for (int num4 = 0; num4 < VecEff.size(); num4++)
				{
					Point point10 = (Point)VecEff.elementAt(num4);
					if (!point10.isRemove)
					{
						fraImgEff.drawFrameNew(typeSub * fraImgEff.maxNumFrame + point10.frame, point10.x, point10.y, point10.dis, 3, g);
					}
				}
				break;
			}
			case 161:
			{
				if (f < 20 - randomf)
				{
					fraImgEff.drawFrameNew(typeSub * fraImgEff.maxNumFrame + frame, x, y, 0, 3, g);
				}
				else if (f < 20 + lengthM - randomf)
				{
					fraImgSubEff.drawFrameNew(typeSub * fraImgSubEff.maxNumFrame + mPlayFrameVip[randomf2][f - (20 - randomf)], x, y, 0, 3, g);
				}
				for (int l = 0; l < VecEff.size(); l++)
				{
					Point point5 = (Point)VecEff.elementAt(l);
					fraImgEff.drawFrameNew(typeSub * fraImgEff.maxNumFrame + point5.frame, point5.x / 100, point5.y / 100, 0, 3, g);
				}
				break;
			}
			case 162:
				if (f > 115 && f < 165)
				{
					fraImgEff.drawFrame(f / 3 % fraImgEff.maxNumFrame, x / 100, y / 100, Dir, 3, g);
				}
				if (f >= 20 && f < 25)
				{
					if (fraImgSubEff.getImageFrame() != null)
					{
						g.drawRegion(fraImgSubEff.getImageFrame(), 0, 0, fraImgSubEff.frameWidth, 30 + (f - 20) * 15, 0, x1000, y1000, 33);
					}
				}
				else if (f >= 25)
				{
					g.drawRegion(fraImgSubEff.getImageFrame(), 0, 0, fraImgSubEff.frameWidth, 90, 0, x1000, y1000, 33);
				}
				fraImgSub2Eff.drawFrame(f / 3 % fraImgSub2Eff.maxNumFrame, x1000, y1000 + 5, Dir, 33, g);
				if (f < 60)
				{
					fraImgSub3Eff.drawFrame(0, x + CRes.random_Am_0(2), y, Dir, 3, g);
				}
				break;
			case 163:
				fraImgEff.drawFrame(f / 3 % fraImgEff.maxNumFrame, x1000, y1000 - 6, Dir, 33, g);
				break;
			case 164:
				break;
			case 165:
				fraImgEff.drawFrame(CFrame, objMainEff.x, objMainEff.y - objMainEff.hOne / 2, 0, 3, g);
				break;
			case 15:
			case 33:
			case 43:
			case 44:
			case 67:
			case 94:
			case 95:
			case 100:
			case 129:
			case 144:
			case 145:
				break;
			}
		}
		catch (Exception)
		{
			mSystem.outz("loi End typeeff=" + typeEffect);
		}
	}

	private void paint_Lucci_L2(mGraphics g)
	{
		if (frame == 1)
		{
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point point = (Point)VecEff.elementAt(i);
				if (fraImgSubEff != null && fraImgSubEff.imgFrame != null)
				{
					fraImgSubEff.drawFrameNew(CRes.random(fraImgSubEff.maxNumFrame), point.x, point.y, 0, 3, g);
				}
				fraImgEff.drawFrameNew(6 + point.frame, point.x, point.y, 0, 3, g);
			}
		}
		else
		{
			for (int j = 0; j < VecEff.size(); j++)
			{
				Point point2 = (Point)VecEff.elementAt(j);
				fraImgEff.drawFrameNew(6 + point2.frame, point2.x, point2.y, 0, 3, g);
			}
		}
		if (f < fRemove)
		{
			fraImgEff.drawFrameNew(6 + mframe[f], x, y, Dir, 3, g);
		}
	}

	private void paintNo_Dat_New(mGraphics g)
	{
		int num = 0;
		if (f < 2)
		{
			num = 1;
		}
		else if (f > fRemove - 4)
		{
			num = 2 - (fRemove - f) / 2;
		}
		fraImgEff.drawFrameNew(frame * 3 + num, x, y, Dir, 3, g);
	}

	public override void paint(mGraphics g, int xOBJ, int yOBJ)
	{
		switch (typeEffect)
		{
		case 95:
		{
			for (int j = 0; j < VecEff.size(); j++)
			{
				Point point = (Point)VecEff.elementAt(j);
				g.setColor(15009800);
				g.fillRect(point.x + xOBJ, point.y + yOBJ, 1, point.dis);
			}
			break;
		}
		case 100:
			fraImgEff.drawFrame(f / numNextFrame % fraImgEff.nFrame, x + xOBJ, y + yOBJ, Dir, 3, g);
			break;
		case 144:
		case 145:
		{
			for (int i = 0; i < VecEff.size(); i++)
			{
				Line line = (Line)VecEff.elementAt(i);
				if (line != null)
				{
					int color = 0;
					if (i / 2 < colorpaint.Length)
					{
						color = colorpaint[i / 2];
					}
					g.setColor(color);
					g.drawLine(xOBJ + line.x0 / 1000, yOBJ + line.y0 / 1000, xOBJ + line.x1 / 1000, yOBJ + line.y1 / 1000);
					if (line.is2Line)
					{
						g.drawLine(xOBJ + line.x0 / 1000 + 1, yOBJ + line.y0 / 1000, xOBJ + line.x1 / 1000 + 1, yOBJ + line.y1 / 1000);
					}
				}
			}
			break;
		}
		}
	}

	public override void update()
	{
		f++;
		switch (typeEffect)
		{
		case 166:
			tframe++;
			if (tframe > arrFrame.Length)
			{
				tframe = 0;
				removeEff();
			}
			if (tframe < arrFrame.Length - 1)
			{
				CFrame = arrFrame[tframe];
			}
			break;
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 8:
		case 9:
		case 10:
		case 12:
		case 19:
		case 20:
		case 26:
		case 34:
		case 42:
		case 45:
		case 46:
		case 52:
		case 53:
		case 57:
		case 58:
		case 61:
		case 63:
		case 64:
		case 65:
		case 66:
		case 68:
		case 71:
		case 77:
		case 80:
		case 81:
		case 82:
		case 86:
		case 89:
		case 90:
		case 91:
		case 92:
		case 100:
		case 109:
		case 118:
		case 121:
		case 122:
		case 123:
		case 124:
		case 125:
		case 133:
		case 134:
		case 136:
		case 139:
		case 141:
		case 148:
		case 149:
		case 151:
		case 159:
		case 170:
		case 171:
		case 178:
			x += vx;
			y += vy;
			if (x > toX)
			{
				x = toX;
				if (y < toY + 20)
				{
					y += 5;
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 131:
		case 152:
			if ((GameCanvas.timeNow - time) / 1000 >= timeRemove || objTo == null)
			{
				removeEff();
			}
			break;
		case 87:
		case 128:
			x += vx;
			y += vy;
			vy += vMax;
			if (f < fRemove)
			{
				break;
			}
			mSystem.outz("vo day 1 typesub = " + typeSub);
			if (typeEffect == 128)
			{
				mSystem.outz("vo day 2 typesub = " + typeSub);
				if (typeSub > 20)
				{
					GameScreen.addEffectEnd(4, 0, x / 100, y / 100, Dir, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd(92, 0, x / 100, y / 100, Dir, objMainEff);
				}
			}
			removeEff();
			break;
		case 6:
			if (typeSub == 3)
			{
				for (int num14 = 0; num14 < VecEff.size(); num14++)
				{
					Point point20 = (Point)VecEff.elementAt(num14);
					point20.f++;
					if (point20.f >= fraImgSub2Eff.maxNumFrame)
					{
						VecEff.removeElementAt(num14);
					}
				}
				if (f < fRemove && f % 3 == 0 && !GameCanvas.lowGraphic)
				{
					Point o2 = new Point(x, y);
					VecEff.addElement(o2);
				}
			}
			x += vx;
			y += vy;
			vy += vMax;
			if (f >= fRemove && (typeSub != 3 || VecEff.size() == 0))
			{
				if (typeEffect == 128)
				{
					GameScreen.addEffectEnd(92, 0, x / 100, y / 100, Dir, objMainEff);
				}
				removeEff();
			}
			break;
		case 11:
			updateZoro4();
			break;
		case 13:
			updateENDLuffy1();
			break;
		case 21:
		case 105:
			updateXuyenGiap();
			break;
		case 59:
			updateRock();
			break;
		case 110:
		case 115:
			updateRockNew();
			break;
		case 22:
			updateHutMP_HP();
			break;
		case 23:
			updatePhanDamage();
			break;
		case 24:
			updateFocusTouch();
			break;
		case 27:
			updateZoro9();
			break;
		case 29:
			updateENDSanji2();
			break;
		case 30:
		case 144:
		case 145:
			updateLineIn();
			break;
		case 25:
			updateLuffy_6();
			break;
		case 36:
			if (f <= fRemove)
			{
				x += vx;
				y += vy;
			}
			if (f > fRemove)
			{
				removeEff();
			}
			break;
		case 35:
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		case 37:
			updateSanji_5();
			break;
		case 38:
		case 138:
			updateNami_4();
			break;
		case 39:
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne - 20;
			}
			if (f <= fRemove)
			{
				break;
			}
			removeEff();
			if (objTo != null)
			{
				if (typeSub != 3)
				{
					GameScreen.addEffectEnd_ObjTo(40, typeSub, objTo.x, objTo.y - objTo.hOne - 20, objTo.ID, objTo.typeObject, typeSub, objMainEff);
				}
				else
				{
					GameScreen.addEffectEnd_ObjTo(175, 0, objTo.x, objTo.y - objTo.hOne - 20, objTo.ID, objTo.typeObject, typeSub, objMainEff);
				}
				GameScreen.addEffectEnd(108, 8, objTo.x, objTo.y - objTo.hOne / 2, 0, objMainEff);
			}
			else if (typeSub != 3)
			{
				GameScreen.addEffectEnd(40, typeSub, x, y, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(175, 0, x, y, Dir, objMainEff);
			}
			break;
		case 40:
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne - 20;
				if (f == 4)
				{
					GameScreen.addEffectEnd(38, 0, objTo.x, objTo.y - objTo.hOne / 2, Dir, objMainEff);
				}
			}
			if (f == 4 && !GameCanvas.lowGraphic)
			{
				int tile = GameCanvas.loadmap.getTile(x, y + 85);
				if (tile == 0 || tile == 2)
				{
					GameScreen.addEffectEnd(63, 0, x - 10, y + 75, Dir, objMainEff);
					GameScreen.addEffectEnd(110, 0, x, y + 75, Dir, objMainEff);
					GameScreen.addEffectEnd(63, 0, x + 10, y + 75, Dir, objMainEff);
				}
			}
			if (f > fRemove)
			{
				removeEff();
			}
			break;
		case 175:
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne - 20;
				if (f == 4)
				{
					GameScreen.addEffectEnd(38, 0, objTo.x, objTo.y - objTo.hOne / 2, Dir, objMainEff);
				}
			}
			if (f == 4 && !GameCanvas.lowGraphic)
			{
				int tile3 = GameCanvas.loadmap.getTile(x, y + 85);
				if (tile3 == 0 || tile3 == 2)
				{
					GameScreen.addEffectEnd(63, 0, x, y + 75, Dir, objMainEff);
					GameScreen.addEffectEnd(110, 0, x, y + 75, Dir, objMainEff);
				}
			}
			if (f > fRemove)
			{
				removeEff();
			}
			break;
		case 41:
			if (typeSub == 1 && f == 1 && objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne - 20;
				GameScreen.addEffectEnd_ObjTo(39, 0, objTo.x, objTo.y - objTo.hOne - 20, objTo.ID, objTo.typeObject, 0, objMainEff);
			}
			if (f > fRemove)
			{
				removeEff();
			}
			break;
		case 47:
		{
			for (int num38 = 0; num38 < VecEff.size(); num38++)
			{
				Point point35 = (Point)VecEff.elementAt(num38);
				point35.update();
				if (point35.f >= point35.fRe)
				{
					VecEff.removeElement(point35);
					num38--;
				}
			}
			if (VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 48:
		case 50:
		case 120:
		{
			for (int num48 = 0; num48 < VecEff.size(); num48++)
			{
				Point point42 = (Point)VecEff.elementAt(num48);
				point42.update();
				point42.vy++;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 168:
		{
			for (int num12 = 0; num12 < VecEff.size(); num12++)
			{
				Point point18 = (Point)VecEff.elementAt(num12);
				point18.update();
				point18.vy++;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 49:
			x += vx;
			y += vy;
			if (f >= fRemove)
			{
				GameScreen.addEffectEnd(50, 0, objTo.x, objTo.y - objTo.hOne / 2, Dir, objMainEff);
				setAva(2, objTo);
				removeEff();
			}
			break;
		case 54:
			updateLuffy_S1_Final();
			break;
		case 55:
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 56:
			if (f == 3 && objTo != null && objTo.Action != 4)
			{
				objTo.x = x;
				objTo.y = y;
				objTo.toX = x;
				objTo.toY = y;
			}
			if (f >= fRemove)
			{
				objTo.isTanHinh = false;
				removeEff();
			}
			break;
		case 60:
		case 107:
		{
			for (int num49 = 0; num49 < VecEff.size(); num49++)
			{
				Point point43 = (Point)VecEff.elementAt(num49);
				point43.update();
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 108:
		{
			for (int num46 = 0; num46 < VecEff.size(); num46++)
			{
				Point point41 = (Point)VecEff.elementAt(num46);
				if (point41.f % 2 == 0)
				{
					point41.vy++;
				}
				point41.update();
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 62:
			if (f <= fRemove - 20 && f % 2 == 1)
			{
				Point point25 = new Point(x, y);
				if (f == 1)
				{
					point25.frame = 0;
				}
				else if (f == fRemove - 20)
				{
					point25.frame = 2;
				}
				else
				{
					point25.frame = 1;
				}
				VecEff.addElement(point25);
			}
			x += vx;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 69:
		case 83:
		{
			if (GameCanvas.timeNow - timeBegin >= timeEnd)
			{
				removeEff();
			}
			for (int num13 = 0; num13 < VecEff.size(); num13++)
			{
				Point point19 = (Point)VecEff.elementAt(num13);
				if ((point19.vy > 0 && point19.y >= 0) || (point19.vy < 0 && point19.y <= -30))
				{
					point19.vy = -point19.vy;
				}
				point19.y += point19.vy;
			}
			break;
		}
		case 70:
		{
			for (int num4 = 0; num4 < VecEff.size(); num4++)
			{
				Point point9 = (Point)VecEff.elementAt(num4);
				if ((point9.vy > 0 && point9.y >= 0) || (point9.vy < 0 && point9.y <= -30))
				{
					point9.vy = -point9.vy;
				}
				point9.y += point9.vy;
			}
			if (f == 30)
			{
				objTo.x = toX;
				objTo.y = toY;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 16:
		case 72:
			x += vx;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 73:
		case 74:
			if (typeSub == 3)
			{
				x += vMax;
				toY += vMax;
			}
			else if (typeSub == 0)
			{
				toX -= vMax;
				y += vMax;
			}
			else if (typeSub == 1)
			{
				x -= vMax;
				toY -= vMax;
			}
			else if (typeSub == 2)
			{
				toX += vMax;
				y -= vMax;
			}
			else if (typeSub == 4)
			{
				toX += vMax;
				y += vMax;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 154:
		case 155:
		{
			for (int num52 = 0; num52 < VecEff.size(); num52++)
			{
				Point point46 = (Point)VecEff.elementAt(num52);
				point46.x = CRes.getcos(CRes.fixangle(point46.dis)) * vMax / 1000 + x;
				point46.y = CRes.getsin(CRes.fixangle(point46.dis)) * vMax / 1000 + y;
				point46.dis += 30;
			}
			vMax -= 2;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 75:
		case 78:
		{
			if (f > 10)
			{
				x += vx;
				y += vy;
			}
			if (typeSub != 1 && f < fRemove + 10)
			{
				int num26 = CRes.random(1, 4);
				if (typeSub == 0)
				{
					num26 = 1;
				}
				for (int num27 = 0; num27 < num26; num27++)
				{
					Point point26 = new Point(x + CRes.random_Am_0(4), y + CRes.random_Am_0(4));
					point26.frame = CRes.random(3);
					point26.vy = -1 + CRes.random(3);
					point26.vx = CRes.random_Am_0(2);
					point26.fRe = 8 + CRes.random(4);
					VecEff.addElement(point26);
				}
			}
			for (int num28 = 0; num28 < VecEff.size(); num28++)
			{
				Point point27 = (Point)VecEff.elementAt(num28);
				point27.update();
				point27.vy++;
				if (point27.f >= point27.fRe)
				{
					VecEff.removeElement(point27);
					num28--;
				}
			}
			if (f >= fRemove + 10 && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 76:
		{
			for (int num30 = 0; num30 < VecEff.size(); num30++)
			{
				Point point29 = (Point)VecEff.elementAt(num30);
				point29.update();
				if (point29.f >= point29.fRe)
				{
					VecEff.removeElement(point29);
					num30--;
				}
			}
			if (VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 79:
			y += vy;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 177:
			y += vy;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 84:
		{
			for (int num5 = 0; num5 < VecEff.size(); num5++)
			{
				Point point10 = (Point)VecEff.elementAt(num5);
				point10.update();
				if (point10.f > point10.fRe)
				{
					if (point10.f - point10.fRe == 2)
					{
						point10.frame = 2;
					}
					if (point10.f - point10.fRe == 4)
					{
						point10.frame = 3;
					}
					if (point10.f - point10.fRe == 6)
					{
						VecEff.removeElement(point10);
						num5--;
					}
				}
			}
			if (VecEff.size() == 0 && f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 85:
			updateLineBuff();
			break;
		case 14:
		case 18:
			y += vy;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 17:
			if (f >= fRemove + 6)
			{
				removeEff();
			}
			break;
		case 88:
			y += vy;
			if (vy == 0 && mPlayFrame[f] == 4)
			{
				vy = 3;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 28:
			y += vy;
			if (f >= fRemove)
			{
				removeEff();
			}
			if (f % 6 == 3 && f < 16)
			{
				GameScreen.addEffectEnd(50, 0, x + CRes.random_Am_0(20), y, Dir, objMainEff);
			}
			if (objTo != null && objTo.Action != 4)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne / 2;
			}
			break;
		case 31:
		{
			y += vy;
			if (f == 6)
			{
				Point point36 = new Point(x, y);
				point36.vy = 0;
				point36.dis = 1;
				point36.frame = 0;
				point36.fRe = 10;
				VecEff.addElement(point36);
			}
			if (f == 8)
			{
				vy = -25;
			}
			for (int num39 = 0; num39 < VecEff.size(); num39++)
			{
				Point point37 = (Point)VecEff.elementAt(num39);
				point37.update();
				if (point37.dis == 1)
				{
					point37.vy = vy;
				}
				if (point37.f >= point37.fRe)
				{
					VecEff.removeElement(point37);
					num39--;
				}
			}
			if (f < 10 && f % 3 == 2)
			{
				for (int num40 = 0; num40 < 4; num40++)
				{
					Point point38 = new Point(x + CRes.random_Am_0(20), y - CRes.random(30) + 10);
					point38.vy = -2 - CRes.random(4);
					point38.dis = 0;
					point38.frame = CRes.random(fraImgSubEff.nFrame);
					point38.fRe = CRes.random(12, 20);
					VecEff.addElement(point38);
				}
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 32:
		{
			if (objTo != null && f > 5)
			{
				x = objTo.x;
				y = objTo.y;
			}
			if (f < fRemove && (f % 3 == 2 || f < 3))
			{
				for (int num7 = 0; num7 < 4; num7++)
				{
					Point point14 = new Point(x + CRes.random_Am_0(20), y + CRes.random(30) - 72);
					point14.vy = 2 + CRes.random(4);
					point14.dis = 0;
					point14.frame = CRes.random(fraImgSubEff.nFrame);
					point14.fRe = CRes.random(12, 20);
					VecEff.addElement(point14);
				}
			}
			if (f == 5)
			{
				vy = 0;
			}
			y += vy;
			for (int num8 = 0; num8 < VecEff.size(); num8++)
			{
				Point point15 = (Point)VecEff.elementAt(num8);
				point15.update();
				if (point15.dis == 1)
				{
					point15.vy = vy;
				}
				if (point15.f >= point15.fRe)
				{
					VecEff.removeElement(point15);
					num8--;
				}
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 33:
			if (f % 5 == 0)
			{
				int num2 = 28;
				if (Dir == 0)
				{
					num2 = -28;
				}
				sbyte b = 0;
				b = 2;
				LoginScreen.addEffectEnd(25, b, objMainEff.x + num2, objMainEff.y - objMainEff.dy - objMainEff.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 15:
		{
			int num54 = 5;
			if (f == 5)
			{
				int num55 = 20;
				if (Dir == 0)
				{
					num55 = -20;
				}
				LoginScreen.addEffectEnd(16, 1, x + num55, objMainEff.y - objFireMain.hOne / 2 - 10 + num54, Dir, objMainEff);
			}
			if (f == 10)
			{
				int num56 = 30;
				if (Dir == 0)
				{
					num56 = -30;
				}
				LoginScreen.addEffectEnd(16, 2, x + num56, objFireMain.y - objFireMain.hOne / 2 + num54, Dir, objMainEff);
			}
			if (f == 15)
			{
				int num57 = 20;
				if (Dir == 0)
				{
					num57 = -20;
				}
				LoginScreen.addEffectEnd(16, 1, x + num57, objFireMain.y - objFireMain.hOne / 2 - 10 + num54, Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 44:
			if (f % 4 == 0)
			{
				int num53 = 25;
				if (Dir == 0)
				{
					num53 = -25;
				}
				LoginScreen.addEffectEnd(35, 0, objFireMain.x + num53, objFireMain.y - objFireMain.dy - objFireMain.hOne / 3 * 2 + 10, Dir, objMainEff);
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 51:
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 7:
		{
			for (int num43 = 0; num43 < VecEff.size(); num43++)
			{
				Point_Focus point_Focus3 = (Point_Focus)VecEff.elementAt(num43);
				point_Focus3.update_Vx_Vy();
				if (point_Focus3.f >= point_Focus3.fRe)
				{
					VecEff.removeElement(point_Focus3);
					LoginScreen.addEffectEnd(1, 0, point_Focus3.toX, point_Focus3.toY, Dir, objMainEff);
					num43--;
				}
			}
			if (f == 10 || f == 13 || f == 16 || f == 19)
			{
				toX = x + ((objFireMain.Dir != 0) ? 140 : (-140));
				toY = y;
				setAngle();
				int num44 = toX - x;
				int num45 = toY - y;
				int frameAngle = CRes.angle(num44, num45);
				Point_Focus p = new Point_Focus();
				p = create_Speed(num44, num45, p);
				p.frame = setFrameAngle(frameAngle);
				VecEff.addElement(p);
				LoginScreen.addEffectEnd(3, 0, x, y, Dir, objMainEff);
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 93:
		{
			for (int num33 = 0; num33 < VecEff.size(); num33++)
			{
				Point point32 = (Point)VecEff.elementAt(num33);
				point32.update();
				point32.vy++;
				if (point32.f >= point32.fRe)
				{
					VecEff.removeElement(point32);
					num33--;
				}
			}
			if (f > 0 && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 95:
		{
			for (int num32 = 0; num32 < VecEff.size(); num32++)
			{
				Point point31 = (Point)VecEff.elementAt(num32);
				point31.update();
				if (point31.y < point31.y2 - 60)
				{
					point31.y = point31.y2;
				}
			}
			break;
		}
		case 96:
		{
			for (int num15 = 0; num15 < VecEff.size(); num15++)
			{
				Point_Focus point_Focus = (Point_Focus)VecEff.elementAt(num15);
				point_Focus.f++;
				if (point_Focus.f < point_Focus.fRe)
				{
					point_Focus.x += point_Focus.vx;
					point_Focus.y += point_Focus.vy;
					continue;
				}
				if (objTo != null)
				{
					point_Focus.vx = 0;
					point_Focus.vy = 0;
					point_Focus.x = objTo.x;
					point_Focus.y = objTo.y - objTo.hOne / 2;
				}
				if (objTo == null || objTo.Action == 4 || objTo.returnAction())
				{
					VecEff.removeAllElements();
					removeEff();
				}
			}
			if (GameCanvas.timeNow - time < timeRemove)
			{
				break;
			}
			for (int num16 = 0; num16 < VecEff.size(); num16++)
			{
				Point_Focus point_Focus2 = (Point_Focus)VecEff.elementAt(num16);
				for (int num17 = 0; num17 < 3; num17++)
				{
					GameScreen.addEffectEnd(4, 0, point_Focus2.x - 20 + 20 * num17, point_Focus2.y - 20 + num17 % 2 * 20, Dir, objMainEff);
				}
			}
			VecEff.removeAllElements();
			removeEff();
			break;
		}
		case 97:
			x += vx;
			y += vy;
			if (f == fRemove && objTo != null)
			{
				GameScreen.addEffectEnd(35, 0, objTo.x, objTo.y - objTo.hOne / 2, Dir, objMainEff);
			}
			if (f >= fRemove + 5)
			{
				removeEff();
			}
			break;
		case 98:
		case 99:
		{
			if (f % 3 == 0 && f <= fRemove - 5)
			{
				Point o = new Point(x + CRes.random_Am_0(30), y + CRes.random_Am_0(10));
				VecEff.addElement(o);
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			for (int num3 = 0; num3 < VecEff.size(); num3++)
			{
				Point point8 = (Point)VecEff.elementAt(num3);
				point8.f++;
				if (point8.f >= 5)
				{
					VecEff.removeElement(point8);
					num3--;
				}
			}
			break;
		}
		case 101:
		case 104:
		{
			if (f == 3 || f == 6)
			{
				int num = CRes.random(4, 7);
				for (int k = 0; k < num; k++)
				{
					Point point3 = new Point(x + CRes.random_Am_0(20), y + CRes.random_Am_0(10));
					point3.fRe = CRes.random(8, 11);
					point3.vy = -CRes.random(5, 7);
					VecEff.addElement(point3);
				}
			}
			for (int l = 0; l < VecEff.size(); l++)
			{
				Point point4 = (Point)VecEff.elementAt(l);
				point4.update();
				if (point4.f > point4.fRe)
				{
					VecEff.removeElement(point4);
					l--;
				}
			}
			if (VecEff.size() == 0 && f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 102:
			if (objTo != null)
			{
				if (typeSub == 0)
				{
					x = objTo.x + 6;
				}
				else
				{
					x = objTo.x - 6;
				}
				y = objTo.y - objTo.hOne / 2 + 14;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 103:
			if (f == 15)
			{
				levelPaint = -1;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 106:
			if (objTo != null && objTo.Action == 4)
			{
				objTo.Action = 2;
				objTo.frame = 10;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 111:
		{
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y;
			}
			for (int num50 = 0; num50 < VecEff.size(); num50++)
			{
				Point point44 = (Point)VecEff.elementAt(num50);
				point44.update();
				if (point44.y < -210)
				{
					VecEff.removeElementAt(num50);
					num50--;
				}
			}
			if (f % 2 == 1)
			{
				for (int num51 = 0; num51 < 3; num51++)
				{
					Point point45 = new Point(CRes.random_Am_0(20), -5 + CRes.random(10));
					point45.vy = -CRes.random(12, 20);
					point45.frame = CRes.random(fraImgSubEff.nFrame);
					VecEff.addElement(point45);
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 112:
			if (f >= fRemove - 6)
			{
				frame = 3 - (fRemove - f) / 2;
			}
			else
			{
				frame = 0;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 113:
		{
			if (f == 1)
			{
				GameScreen.addEffectEnd(63, 3, x, y, Dir, objMainEff);
			}
			if (f < 8)
			{
				int num20 = 90;
				int num21 = 4;
				int num22 = -1;
				if (typeSub == 1)
				{
					num21 = 5;
					num22 = 0;
				}
				else if (typeSub == 2)
				{
					num21 = 8;
				}
				num20 = 360 / num21;
				int num23 = CRes.random(num20);
				for (int num24 = 0; num24 < num21; num24++)
				{
					Point point23 = new Point();
					point23.x = x * 1000 + CRes.getcos(CRes.fixangle(num23 + num20 * num24) + f * 5) * f * vMax;
					point23.y = y * 1000 + CRes.getsin(CRes.fixangle(num23 + num20 * num24) + f * 5) * f * (vMax - 4);
					point23.dis = f % 1;
					point23.fSmall = 1;
					if (typeSub == 2)
					{
						if (num24 % 2 == 0)
						{
							point23.frame = 1;
						}
					}
					else if (num22 == num24)
					{
						point23.frame = 1;
					}
					VecEff.addElement(point23);
				}
			}
			for (int num25 = 0; num25 < VecEff.size(); num25++)
			{
				Point point24 = (Point)VecEff.elementAt(num25);
				point24.f++;
				if (point24.f == 1)
				{
					if (point24.dis == 0)
					{
						int tile2 = GameCanvas.loadmap.getTile(point24.x / 1000, point24.y / 1000);
						if (tile2 == 0 || tile2 == 2)
						{
							GameScreen.addEffectEnd(66, (point24.frame != 1) ? 1 : 0, point24.x / 1000, point24.y / 1000, Dir, objMainEff);
						}
						else
						{
							point24.isRemove = true;
						}
					}
					if (CRes.random(6) == 0)
					{
						GameScreen.addEffectEnd(110, (point24.frame != 1) ? 1 : 0, point24.x / 1000, point24.y / 1000, Dir, objMainEff);
					}
				}
				if (point24.f / 3 >= fraImgSubEff.maxNumFrame || point24.isRemove)
				{
					VecEff.removeElement(point24);
					num25--;
				}
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 114:
			x += vx;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 116:
		{
			for (int num6 = 0; num6 < VecEff.size(); num6++)
			{
				Point point11 = (Point)VecEff.elementAt(num6);
				point11.update();
				if (point11.f >= point11.fRe)
				{
					VecEff.removeElement(point11);
					num6--;
				}
			}
			if (f % 5 == 1 && f < fRemove && CRes.random(2) == 0)
			{
				Point point12 = new Point();
				point12.x = x + CRes.random_Am_0(10);
				point12.y = y + CRes.random(20);
				point12.vx = CRes.random_Am_0(3);
				point12.vy = -CRes.random(3, 7);
				point12.fRe = CRes.random(12, 18);
				VecEff.addElement(point12);
			}
			if (f % 4 == 1 && f < fRemove)
			{
				Point point13 = new Point();
				point13.x = x + CRes.random_Am_0(15);
				point13.y = y + CRes.random(20);
				point13.vx = CRes.random_Am_0(3);
				point13.vy = -CRes.random(3, 7);
				point13.fRe = CRes.random(4, 7);
				VecEff.addElement(point13);
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 117:
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 119:
		{
			for (int n = 0; n < VecEff.size(); n++)
			{
				Point point6 = (Point)VecEff.elementAt(n);
				point6.update();
				if (point6.f >= point6.fRe)
				{
					VecEff.removeElementAt(n);
					n--;
				}
			}
			if (f % 3 == 0 && f < fRemove)
			{
				Point point7 = new Point();
				point7.x = x;
				point7.y = y;
				if (Dir == 0)
				{
					point7.vx = -6;
				}
				else
				{
					point7.vx = 6;
				}
				point7.fRe = mPlayFrame.Length;
				VecEff.addElement(point7);
			}
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 127:
			if (f < 4)
			{
				objMainEff.frame = 33;
				if (objTo.x < objMainEff.x)
				{
					objMainEff.type_left_right = Dir;
				}
				else
				{
					objMainEff.type_left_right = Dir;
				}
			}
			x += vx;
			y += vy;
			if (f < fRemove)
			{
				break;
			}
			if (typeSub >= 20)
			{
				mSystem.outz("vo day 02 typesub = " + typeSub);
				GameScreen.addEffectEnd(128, typeSub, x, y, 0, objTo);
			}
			else if (typeSub % 10 > 3 && typeSub % 10 != 7)
			{
				if (objTo != null)
				{
					if (typeSub >= 10)
					{
						objTo.strChatPopup = T.gru;
					}
					else
					{
						objTo.strChatPopup = T.gaugau;
					}
				}
			}
			else
			{
				mSystem.outz("vo day 00 typesub = " + typeSub);
				if (typeSub >= 10)
				{
					mSystem.outz("vo day 01 typesub = " + typeSub);
					GameScreen.addEffectEnd(128, typeSub, x, y, 0, objTo);
				}
				else
				{
					mSystem.outz("vo day 02 typesub = " + typeSub);
					GameScreen.addEffectEnd(126, typeSub, x, y, 0, objTo);
				}
			}
			removeEff();
			break;
		case 126:
		{
			for (int num47 = 0; num47 < VecEff.size(); num47++)
			{
				Point_Focus point_Focus4 = (Point_Focus)VecEff.elementAt(num47);
				point_Focus4.update_Vx_Vy();
				if (point_Focus4.f >= point_Focus4.fRe)
				{
					VecEff.removeElementAt(num47);
					num47--;
				}
			}
			if (step == 2 && VecEff.size() == 0)
			{
				step = 3;
				Point_Focus point_Focus5 = new Point_Focus();
				point_Focus5.frame = 1;
				point_Focus5.fRe = 8;
				point_Focus5.x = x / 100;
				point_Focus5.y = y / 100;
				point_Focus5.vy = -4;
				VecEff.addElement(point_Focus5);
				GameScreen.addEffectEnd(108, 7, x / 100 - 4, y / 100, Dir, objMainEff);
				GameScreen.addEffectEnd(108, 7, x / 100 + 4, y / 100, Dir, objMainEff);
			}
			if (step == 1)
			{
				frame++;
				if (frame == 8)
				{
					step = 2;
					x1000 = objMainEff.x;
					y1000 = objMainEff.y - 15;
					Point_Focus point_Focus6 = new Point_Focus();
					int xdich = x / 100 - x1000;
					int ydich = y / 100 - y1000;
					point_Focus6.frame = 0;
					create_Speed(xdich, ydich, point_Focus6, x1000, y1000, x / 100, y / 100);
					VecEff.addElement(point_Focus6);
					objMainEff.isDie = true;
				}
			}
			if (step == 0)
			{
				x += vx;
				y += vy;
				vy += vMax;
				if (vy >= 0)
				{
					vx = 0;
					vy = 0;
					vMax = 6;
					step = 1;
					GameScreen.addEffectEnd(85, 0, x / 100, y / 100, 500, Dir, objMainEff);
					frame = 0;
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 130:
		{
			for (int num41 = 0; num41 < VecEff.size(); num41++)
			{
				Point point39 = (Point)VecEff.elementAt(num41);
				point39.x += point39.vx;
				point39.y += point39.vy;
				if (point39.y <= -60)
				{
					VecEff.removeElementAt(num41);
					num41--;
				}
			}
			if (f % 10 == 0)
			{
				for (int num42 = 0; num42 < 5; num42++)
				{
					Point point40 = new Point();
					point40.x = CRes.random_Am_0(15);
					point40.y = 10 - CRes.random(40);
					point40.dis = 1 + CRes.random(3);
					point40.vy = -4;
					point40.frame = CRes.random(12);
					VecEff.addElement(point40);
				}
			}
			if ((GameCanvas.timeNow - time) / 1000 >= timeRemove || objTo == null)
			{
				removeEff();
			}
			break;
		}
		case 132:
			x += vx;
			if (typeSub == 1 && vx <= 20)
			{
				vx -= 2;
				if (Dir == 2)
				{
					vx += 2;
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 135:
			x += vx;
			vx /= 2;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 137:
		{
			for (int num29 = 0; num29 < VecEff.size(); num29++)
			{
				Point point28 = (Point)VecEff.elementAt(num29);
				point28.x += vx;
				point28.f++;
				if (point28.f >= point28.fRe)
				{
					VecEff.removeElementAt(num29);
					num29--;
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 140:
		case 167:
		{
			for (int num18 = 0; num18 < VecEff.size(); num18++)
			{
				Point point21 = (Point)VecEff.elementAt(num18);
				if (CRes.random(3) == 0)
				{
					point21.x = x + CRes.random_Am_0(3);
					point21.y = y + CRes.random_Am_0(3);
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 143:
			y += vy;
			if (vy < 0)
			{
				vy++;
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 146:
		{
			for (int num19 = 0; num19 < VecEff.size(); num19++)
			{
				Point point22 = (Point)VecEff.elementAt(num19);
				point22.update();
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 147:
			update_Lucci_L2();
			break;
		case 150:
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne;
			}
			if ((GameCanvas.timeNow - time) / 1000 >= timeRemove || objTo == null)
			{
				removeEff();
			}
			break;
		case 153:
		{
			if (f == 20)
			{
				frame = 1;
			}
			else if (f == 40)
			{
				frame = 2;
			}
			else if (f == 50)
			{
				vy = -1;
			}
			else if (f == 60)
			{
				vy = -2;
			}
			if (f >= 50)
			{
				if (CRes.random(6) == 0)
				{
					vx = CRes.random_Am(1, 2);
				}
				else
				{
					vx = 0;
				}
			}
			else if (CRes.random(10) == 0)
			{
				vx = CRes.random_Am(1, 2);
			}
			else
			{
				vx = 0;
			}
			for (int i = 0; i < VecEff.size(); i++)
			{
				Point point = (Point)VecEff.elementAt(i);
				point.update();
				if (point.x < MainScreen.cameraMain.xCam)
				{
					point.x += MotherCanvas.w;
				}
				else if (point.x > MainScreen.cameraMain.xCam + MotherCanvas.w)
				{
					point.x -= MotherCanvas.w;
				}
				if (point.f >= point.fRe)
				{
					VecEff.removeElementAt(i);
					i--;
				}
			}
			if (f >= 140 && f <= 180)
			{
				for (int j = 0; j < MotherCanvas.w / 10 - 1; j++)
				{
					if (CRes.random(4) == 0)
					{
						Point point2 = new Point(MainScreen.cameraMain.xCam + 5 + j * 10 + CRes.random(3), y);
						point2.vy = 5;
						point2.vx = CRes.random_Am_0(2);
						point2.fRe = MotherCanvas.h / 5 + CRes.random_Am_0(10);
						point2.frame = CRes.random(9);
						VecEff.addElement(point2);
					}
				}
			}
			x += vx;
			y += vy;
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 156:
			if ((GameCanvas.timeNow - time) / 100 >= timeRemove || objTo == null)
			{
				removeEff();
			}
			if (objTo != null)
			{
				x = objTo.x;
				y = objTo.y - objTo.hOne;
			}
			y1000 += vy;
			if (vy < 0)
			{
				vy++;
			}
			break;
		case 157:
			x += vx;
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 158:
		{
			if (f % 3 == 0 || f >= 30)
			{
				GameScreen.addEffectEnd(108, (sbyte)CRes.random(7, 9), x - am_duong * 5, y + 5, 0, objMainEff);
			}
			if (f >= 30)
			{
				GameScreen.addEffectEnd(108, (sbyte)CRes.random(7, 9), x + am_duong * 5, y - 5, 0, objMainEff);
			}
			if (f >= 30 && f <= 36)
			{
				vy -= 3;
				vx -= am_duong;
			}
			for (int num34 = 0; num34 < VecEff.size(); num34++)
			{
				Point point33 = (Point)VecEff.elementAt(num34);
				point33.update();
				if (point33.x < MainScreen.cameraMain.xCam)
				{
					point33.x += MotherCanvas.w;
				}
				else if (point33.x > MainScreen.cameraMain.xCam + MotherCanvas.w)
				{
					point33.x -= MotherCanvas.w;
				}
				if (point33.f >= point33.fRe)
				{
					VecEff.removeElementAt(num34);
					num34--;
				}
			}
			if (f == 50)
			{
				levelPaint = 0;
			}
			if (f >= 40 && f <= 48 && f % 2 == 0 && !GameCanvas.lowGraphic)
			{
				int num35 = MainScreen.cameraMain.xCam + 20 + (f - 40) / 2 * (MotherCanvas.w / 5);
				if (Dir == 2)
				{
					num35 = MainScreen.cameraMain.xCam + MotherCanvas.w - 20 - (f - 40) / 2 * (MotherCanvas.w / 5);
				}
				GameScreen.addEffectEnd(120, 0, num35, MainScreen.cameraMain.yCam + 50, Dir, objMainEff);
			}
			if (f >= 50 && f <= 54 && f % 2 == 0)
			{
				int num36 = MainScreen.cameraMain.xCam + 40 + (f - 50) / 2 * (MotherCanvas.w / 3);
				if (Dir == 2)
				{
					num36 = MainScreen.cameraMain.xCam + MotherCanvas.w - 20 - (f - 50) / 2 * (MotherCanvas.w / 3);
				}
				GameScreen.addEffectEnd(120, 0, num36, MainScreen.cameraMain.yCam + 30, Dir, objMainEff);
			}
			if (f >= 40 && f <= 90)
			{
				for (int num37 = 0; num37 < MotherCanvas.w / 10 - 1; num37++)
				{
					if (CRes.random(4) == 0)
					{
						Point point34 = new Point(MainScreen.cameraMain.xCam + 5 + num37 * 10 + CRes.random(3), MainScreen.cameraMain.yCam - 10);
						point34.vy = 5;
						point34.vx = CRes.random_Am_0(2);
						point34.fRe = MotherCanvas.h / 5 + CRes.random_Am_0(10);
						point34.frame = CRes.random(9);
						VecEff.addElement(point34);
					}
				}
			}
			x += vx;
			y += vy;
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 160:
		{
			for (int num31 = 0; num31 < VecEff.size(); num31++)
			{
				Point point30 = (Point)VecEff.elementAt(num31);
				point30.x += point30.vx;
				point30.y += point30.vy;
				if (f < 6 || f > 12)
				{
					point30.vy = -1;
				}
				else
				{
					point30.vy = 1;
				}
				if (f > 10)
				{
					if (num31 == 0 || num31 == VecEff.size() - 1)
					{
						point30.isRemove = true;
					}
					else
					{
						point30.frame = 2 + CRes.random(2);
					}
				}
				else if (f > 14 && num31 == 1)
				{
					point30.isRemove = true;
				}
			}
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
		case 161:
		{
			for (int num9 = 0; num9 < VecEff.size(); num9++)
			{
				Point point16 = (Point)VecEff.elementAt(num9);
				point16.update();
				if (point16.f >= point16.fRe)
				{
					VecEff.removeElementAt(num9);
					num9--;
				}
			}
			if (f < 20 - randomf)
			{
				if (CRes.random(4) == 0)
				{
					vx = -vx;
				}
			}
			else
			{
				if (vx > 0)
				{
					vx--;
				}
				if (vx < 0)
				{
					vx++;
				}
				if (vy < 0)
				{
					vy++;
				}
			}
			if (f == 20 + lengthM - randomf)
			{
				int num10 = 7 + CRes.random(3);
				for (int num11 = 0; num11 < num10; num11++)
				{
					Point point17 = new Point();
					point17.x = x * 100 + CRes.random_Am_0(200);
					point17.y = y * 100 + CRes.random_Am_0(200);
					point17.vx = CRes.random_Am_0(200);
					point17.vy = CRes.random_Am_0(200);
					point17.frame = 2 + CRes.random(2);
					point17.fRe = 6 + CRes.random(3);
					VecEff.addElement(point17);
				}
			}
			x += vx;
			y += vy;
			if (f >= fRemove && VecEff.size() == 0)
			{
				removeEff();
			}
			break;
		}
		case 162:
			if (f == 20)
			{
				vy = 0;
			}
			if (f == 100 || f == 105 || f == 110)
			{
				mSound.playSound(7, mSound.volumeSound);
				GameScreen.addEffectEnd(10, 0, objTo.x, objTo.y - objTo.dy - objTo.hOne / 2, Dir, objTo);
			}
			if (f > 30 && f < 120)
			{
				objTo.dy = (f - 20) / 10;
			}
			y += vy;
			x += vx;
			if (f == 114)
			{
				x = objTo.x * 100;
				y = (objTo.y - objTo.dy - objTo.hOne / 2) * 100;
				vx = 50;
				if (objTo.type_left_right == 0)
				{
					vx = -50;
				}
				vy = -15;
			}
			if (f >= fRemove)
			{
				if (objTo == GameScreen.player)
				{
					Player.isSendMove = true;
					GameCanvas.tabAllScr.idSelect = 1;
					GameCanvas.tabAllScr.Show(GameCanvas.gameScr);
					GameCanvas.tabAllScr.typeCurrent = 0;
					GameScreen.player.resetAction();
					GameCanvas.clearAll();
				}
				GameScreen.isPaintNormal();
				removeEff();
			}
			break;
		case 163:
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		case 164:
		{
			CFrame++;
			if (CFrame > 2)
			{
				CFrame = 0;
			}
			bool flag = true;
			for (int m = 0; m < VecEff.size(); m++)
			{
				Point point5 = (Point)VecEff.elementAt(m);
				if (point5.f == 0)
				{
					flag = false;
					if (f >= fRemove)
					{
						point5.f = 1;
						continue;
					}
					point5.x += point5.vx;
					point5.y += point5.vy;
				}
				else if (point5.f > 0)
				{
					point5.f++;
					if ((point5.f - 1) / 2 >= 5)
					{
						point5.f = -1;
						VecEff.removeElement(point5);
					}
					flag = false;
				}
			}
			if (flag)
			{
				removeEff();
			}
			break;
		}
		case 165:
			if (GameCanvas.gameTick % 2 == 0)
			{
				CFrame++;
			}
			if (CFrame > 3)
			{
				CFrame = 0;
			}
			if (objMainEff.typeEfffashion != 5)
			{
				removeEff();
			}
			break;
		default:
			if (f >= fRemove)
			{
				removeEff();
			}
			break;
		}
	}

	private void update_Lucci_L2()
	{
		x += vx;
		y += vy;
		if (f == 2 || f == 4 || f == 6)
		{
			x = x1000 - am_duong * 24;
		}
		if (f >= 7 && vx <= 20)
		{
			vx += am_duong * 2;
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

	public void endUpdate()
	{
	}

	private void updateLuffy_6()
	{
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
		if (f >= fRemove)
		{
			if (VecEff.size() == 0)
			{
				removeEff();
			}
		}
		else if ((typeSub == 2 || typeSub == 3) && f % 2 == 0)
		{
			Point o = new Point(x + CRes.random_Am_0(15), y + CRes.random_Am_0(20));
			VecEff.addElement(o);
		}
	}

	private void updateSanji_5()
	{
		if (f == fRemove)
		{
			setAva(1, objTo);
			if (typeSub == 1)
			{
				GameScreen.addEffectEnd(25, 0, toX, toY, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(8, 0, toX, toY, Dir, objMainEff);
			}
			GameScreen.addEffectEnd(93, 2, toX, toY, Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			if (VecEff.size() == 0)
			{
				removeEff();
			}
		}
		else if (f >= 1)
		{
			if (typeSub == 1)
			{
				Point o = new Point(x, y);
				VecEff.addElement(o);
			}
			x += vx;
			y += vy;
		}
		if (f == 1)
		{
			if (objTo != null)
			{
				toY = objTo.y - objTo.hOne / 2;
			}
			int xdich = toX - x;
			int ydich = toY - y;
			vMax = 14;
			create_Speed(xdich, ydich, null);
			fRemove += 2;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= 4)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
	}

	private void updateNami_4()
	{
		if (f < fRemove && f % 2 == 0)
		{
			Point point = new Point();
			point.x = x + CRes.random_Am_0(20);
			point.y = y + CRes.random_Am_0(20);
			if (typeEffect == 138)
			{
				point.frame = CRes.random(2);
			}
			VecEff.addElement(point);
		}
		if (f > fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point2 = (Point)VecEff.elementAt(i);
			point2.f++;
			if (point2.f >= 6)
			{
				VecEff.removeElement(point2);
				i--;
			}
		}
	}

	private void updateUssop_8()
	{
		if (f >= 0 && objTo != null && CRes.random(2) == 0 && f < fRemove)
		{
			x = objTo.x;
			y = objTo.y - objTo.hOne / 2;
			Point o = new Point(x + CRes.random_Am_0(20), y + CRes.random_Am_0(20));
			VecEff.addElement(o);
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= 5)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f > fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	private void updateLuffy_S1_Final()
	{
		if (f >= fRemove)
		{
			removeEff();
		}
		if (f == 0)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				Point o = new Point(x + CRes.getcos(num) * 45 / 1000, y + CRes.getsin(num) * 35 / 1000);
				VecEff.addElement(o);
				num += 45;
			}
		}
		int num2 = 0;
		if (f == 3)
		{
			int num3 = 0;
			for (int j = 0; j < 12; j++)
			{
				Point point = new Point(x + CRes.getcos(num3) * 75 / 1000, y + CRes.getsin(num3) * 45 / 1000);
				VecEff.addElement(point);
				if (typeSub == 11 || typeSub == 12)
				{
					int tile = GameCanvas.loadmap.getTile(point.x, point.y);
					if (tile == 0 || tile == 2)
					{
						vecSubEff.addElement(point);
						GameScreen.addEffectEnd(110, 0, point.x, point.y, Dir, objMainEff);
					}
				}
				num3 += 30;
			}
		}
		if (f == 6 && typeSub != 12)
		{
			int num4 = 0;
			for (int k = 0; k < 24; k++)
			{
				num2++;
				Point point2 = new Point(x + CRes.getcos(num4) * 85 / 1000, y + CRes.getsin(num4) * 55 / 1000);
				VecEff.addElement(point2);
				if (typeSub == 1)
				{
					int tile2 = GameCanvas.loadmap.getTile(point2.x, point2.y);
					if (tile2 == 0 || tile2 == 2)
					{
						vecSubEff.addElement(point2);
						if (num2 % 2 == 0)
						{
							GameScreen.addEffectEnd(110, 0, point2.x, point2.y, Dir, objMainEff);
						}
					}
				}
				num4 += 15;
			}
		}
		if (f == 10 && typeSub == 11)
		{
			int num5 = 0;
			for (int l = 0; l < 32; l++)
			{
				num2++;
				Point point3 = new Point(x + CRes.getcos(num5) * 115 / 1000, y + CRes.getsin(num5) * 80 / 1000);
				VecEff.addElement(point3);
				if (typeSub == 11)
				{
					int tile3 = GameCanvas.loadmap.getTile(point3.x, point3.y);
					if (tile3 == 0 || tile3 == 2)
					{
						vecSubEff.addElement(point3);
						if (num2 % 2 == 0)
						{
							GameScreen.addEffectEnd(110, 0, point3.x, point3.y, Dir, objMainEff);
						}
					}
				}
				num5 += 11;
			}
		}
		for (int m = 0; m < VecEff.size(); m++)
		{
			Point point4 = (Point)VecEff.elementAt(m);
			point4.f++;
			if (point4.f >= 3)
			{
				VecEff.removeElement(point4);
				m--;
			}
		}
		num2 = 0;
		for (int n = 0; n < vecSubEff.size(); n++)
		{
			Point point5 = (Point)vecSubEff.elementAt(n);
			point5.f++;
			if (point5.f >= 8)
			{
				if (num2 % 2 == 0)
				{
					GameScreen.addEffectEnd(63, 0, point5.x, point5.y, Dir, objMainEff);
				}
				num2++;
				vecSubEff.removeElement(point5);
				n--;
			}
		}
	}

	private void updateLineIn()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Line line = (Line)VecEff.elementAt(i);
			line.update();
			if (f >= fRemove)
			{
				VecEff.removeElement(line);
				i--;
			}
		}
		if (f >= fRemove)
		{
			if (GameCanvas.timeNow - time >= timeRemove)
			{
				VecEff.removeAllElements();
				removeEff();
			}
			else
			{
				fRemove = CRes.random(4, 6);
				f = 0;
				create_Star_Line_In(vMax, xline, yline, 0, maxsize);
			}
		}
	}

	private void updateLineBuff()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Line line = (Line)VecEff.elementAt(i);
			line.update();
			if (f >= fRemove)
			{
				VecEff.removeElement(line);
				i--;
			}
		}
		if (f >= fRemove)
		{
			if (GameCanvas.timeNow - time >= timeRemove)
			{
				VecEff.removeAllElements();
				removeEff();
			}
			else
			{
				fRemove = CRes.random(4, 6);
				f = 0;
				create_Star_Line_In(vMax, xline, yline, 10, maxsize);
			}
		}
	}

	private void updateENDSanji2()
	{
		if (f == fRemove)
		{
			setAva(1, objTo);
			GameScreen.addEffectEnd(25, 0, toX, toY, Dir, objMainEff);
		}
		if (f >= fRemove)
		{
			if (VecEff.size() == 0)
			{
				removeEff();
			}
		}
		else if (f >= 1)
		{
			Point o = new Point(x, y);
			VecEff.addElement(o);
			x += vx;
			y += vy;
		}
		if (f == 1)
		{
			if (objTo != null)
			{
				toY = objTo.y - objTo.hOne / 2;
			}
			int xdich = toX - x;
			int ydich = toY - y;
			vMax = 14;
			create_Speed(xdich, ydich, null);
			fRemove += 2;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= 4)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
	}

	private void updateENDLuffy1()
	{
		if (f == fRemove && objTo != null)
		{
			setAva(1, objTo);
			if (typeSub == 5)
			{
				GameScreen.addEffectEnd(25, 4, toX, toY, Dir, objMainEff);
			}
			else
			{
				GameScreen.addEffectEnd(25, 0, toX, toY, Dir, objMainEff);
			}
		}
		if (f >= fRemove)
		{
			if (f == fRemove && objTo != null)
			{
				int subtype = 0;
				if (typeSub == 5)
				{
					subtype = 3;
				}
				GameScreen.addEffectEnd(93, subtype, x, y, Dir, objMainEff);
			}
			if (VecEff.size() == 0)
			{
				removeEff();
			}
		}
		else
		{
			if (typeSub >= 1)
			{
				Point o = new Point(x, y);
				VecEff.addElement(o);
			}
			x += vx;
			y += vy;
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

	private void updateZoro9()
	{
		if (f == fRemove)
		{
			setAva(1, objTo);
			if (objTo != null)
			{
				GameScreen.addEffectEnd(19, (f / 3 != 0) ? 1 : 0, objTo.x, objTo.y - objTo.hOne / 2, Dir, objMainEff);
				GameScreen.addEffectEnd(93, 0, objTo.x, objTo.y - objTo.hOne / 2, Dir, objMainEff);
				if (typeSub == 2)
				{
					GameScreen.addEffectEnd(108, 6, objTo.x, objTo.y - objTo.hOne / 2, Dir, objMainEff);
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
		else
		{
			if (typeSub == 2)
			{
				Point o = new Point(x, y - 16);
				VecEff.addElement(o);
				Point o2 = new Point(x, y + 16);
				VecEff.addElement(o2);
			}
			x += vx;
			y += vy;
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

	private void updateFocusTouch()
	{
		if (objTo != null)
		{
			x = objTo.x;
			if (typeSub == 0)
			{
				y = objTo.y - objTo.hOne / 2;
			}
			else if (typeSub == 1)
			{
				y = objTo.y - objTo.hOne - 8;
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void updateXuyenGiap()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			point.vy++;
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void updateRock()
	{
		if (f == 10)
		{
			levelPaint = -1;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.f < point.fSmall)
			{
				point.vy++;
			}
			else if (point.f == point.fSmall)
			{
				point.vx = 0;
				point.vy = 0;
				int tile = GameCanvas.loadmap.getTile(point.x, point.y);
				if (tile != 0 && tile != 2)
				{
					point.f = 100;
				}
			}
			if (point.f >= 100 || point.f >= point.fRe)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f >= fRemove)
		{
			removeEff();
		}
	}

	private void updateRockNew()
	{
		if (f == 10)
		{
			levelPaint = -1;
		}
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.update();
			if (point.subType == 1)
			{
				if (point.f > point.fRe - 2)
				{
					point.frame = 2 - (point.fRe - point.f);
				}
				if (point.frame > 2 || point.frame < 0)
				{
					point.frame = 2;
				}
			}
			if (point.f < point.fSmall)
			{
				point.vy++;
			}
			else if (point.f == point.fSmall)
			{
				point.vx = 0;
				point.vy = 0;
				int tile = GameCanvas.loadmap.getTile(point.x, point.y);
				if (tile == 0 || tile == 2)
				{
					if (typeEffect != 115 && CRes.random(2) == 0 && (point.frame == 2 || point.frame == 3))
					{
						point.subType = 1;
						point.frame = 0;
					}
				}
				else
				{
					point.f = 100;
				}
			}
			if (point.f >= 100 || point.f >= point.fRe)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if ((f >= fRemove && VecEff.size() == 0) || f >= 80)
		{
			removeEff();
		}
	}

	private void updatePhanDamage()
	{
		if (f <= fRemove)
		{
			for (int i = 0; i < 3; i++)
			{
				Point point = new Point(x + CRes.random_Am_0(4), y + CRes.random_Am_0(4));
				point.f = CRes.random(3);
				VecEff.addElement(point);
			}
			x += vx;
			y += vy;
		}
		if (f == fRemove)
		{
			GameScreen.addEffectEnd(1, 0, toX, toY, Dir, objMainEff);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.y += 2;
			point2.f++;
			if (point2.f >= 4)
			{
				VecEff.removeElement(point2);
				j--;
			}
		}
	}

	private void updateZoro4()
	{
		for (int i = 0; i < VecEff.size(); i++)
		{
			Point point = (Point)VecEff.elementAt(i);
			point.f++;
			if (point.f >= point.fRe)
			{
				VecEff.removeElement(point);
				i--;
			}
		}
		if (f == 2)
		{
			Point point2 = new Point();
			point2.x = x;
			point2.y = y;
			point2.fRe = 6;
			point2.fraImgEff = new FrameImage(typeSub + 1, 55, 55);
			VecEff.addElement(point2);
		}
		if (f >= fRemove && VecEff.size() == 0)
		{
			removeEff();
		}
	}

	public void updateHutMP_HP()
	{
		if (isUpdateNormal)
		{
			if (f % 2 == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					Point point = new Point(x + CRes.random_Am_0(4), y + CRes.random_Am_0(4));
					point.f = CRes.random(3);
					VecEff.addElement(point);
				}
			}
			if (f <= 5)
			{
				x += vx;
				y += vy;
			}
			if (f == 5)
			{
				vx = 0;
				vy = 0;
			}
			if (f == 10)
			{
				fRemove = 60;
				vMax = 10000;
				numNextFrame = 2;
				setInfoHutMPHP();
			}
			if (f >= 10)
			{
				updateAngleNormal(objTo, 0);
			}
		}
		else if (VecEff.size() == 0)
		{
			removeEff();
			GameScreen.addEffectEnd(91, 0, x, y, Dir, objMainEff);
		}
		for (int j = 0; j < VecEff.size(); j++)
		{
			Point point2 = (Point)VecEff.elementAt(j);
			point2.y += 2;
			point2.f++;
			if (point2.f >= 6)
			{
				VecEff.removeElement(point2);
				j--;
			}
		}
	}

	public override void stopUpdateNormal()
	{
		if (typeEffect == 22)
		{
			isUpdateNormal = false;
		}
		else
		{
			removeEff();
		}
	}

	public override void removeEff()
	{
		isStop = true;
		f = -1;
	}

	private void createFocustouch()
	{
		x = objTo.x;
		if (typeSub == 0)
		{
			fraImgEff = new FrameImage(55, 32, 30);
			fRemove = 6;
			numNextFrame = 1;
			y = objTo.y - objTo.hOne / 2;
		}
		else if (typeSub == 1)
		{
			fraImgEff = new FrameImage(56, 28, 15);
			fRemove = 8;
			numNextFrame = 2;
			y = objTo.y - objTo.hOne - 8;
		}
	}

	private void createPhanDamage()
	{
		fraImgSub2Eff = new FrameImage(50, 48, 48, 32, 32);
		numNextFrame = 2;
		fraImgEff = new FrameImage(0, 14, 14);
		fraImgSubEff = new FrameImage(52, 5, 5);
		x1000 = x;
		y1000 = y;
	}

	private void createHut_MP_HP()
	{
		switch (typeSub)
		{
		case 0:
			fraImgEff = new FrameImage(51, 9, 9);
			fraImgSubEff = new FrameImage(52, 5, 5);
			break;
		case 1:
			fraImgEff = new FrameImage(53, 9, 9);
			fraImgSubEff = new FrameImage(54, 5, 5);
			break;
		}
		vx = CRes.random(2, 7);
		if (Dir == 0)
		{
			vx = -vx;
		}
		vy = -CRes.random(6, 10);
		fRemove = 30;
	}

	private void createXuyenGiap()
	{
		if (GameCanvas.isLowGraOrWP_PvP())
		{
			removeEff();
			return;
		}
		numNextFrame = 2;
		fraImgEff = new FrameImage(48, 12, 11);
		fraImgSubEff = new FrameImage(30, 38, 38);
		fRemove = CRes.random(12, 16);
		int num = CRes.random(3, 7);
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = x + CRes.random_Am_0(5);
			point.y = y + CRes.random_Am_0(7);
			point.frame = CRes.random(fraImgEff.nFrame);
			point.dis = CRes.random(2);
			point.vy = -CRes.random(6, 9);
			point.vx = CRes.random(1, 4);
			if (i % 2 == 0)
			{
				point.vx = -point.vx;
			}
			VecEff.addElement(point);
		}
	}

	public void createEndBungmerang()
	{
		int num = 0;
		if (typeSub == 1)
		{
			num = 50;
		}
		vx = CRes.random_Am(150 + num, 400 + num * 2);
		vy = -CRes.random(700 + num, 1000 + num * 2);
	}

	public void setInfoHutMPHP()
	{
		goc_Arc = 0;
		switch (Dir)
		{
		case 0:
			goc_Arc = 180;
			break;
		case 2:
			goc_Arc = 0;
			break;
		}
		va = 4096;
		vx = 0;
		vy = 0;
		life = 0;
		vx1000 = va * CRes.getcos(goc_Arc) >> 10;
		vy1000 = va * CRes.getsin(goc_Arc) >> 10;
	}

	private void create_Star_Line_In(int vline, int minline, int maxline, int numpoint, int numLine)
	{
		if (f == -1)
		{
			VecEff.removeAllElements();
		}
		colorpaint = new int[numLine];
		if (maxline <= minline)
		{
			maxline = minline + 1;
		}
		for (int i = 0; i < numLine; i++)
		{
			if (CRes.random(2) == 0)
			{
				colorpaint[i] = colorStar[indexColorStar][CRes.random(3)];
			}
			else
			{
				colorpaint[i] = colorStar[indexColorStar][2];
			}
		}
		for (int j = 0; j < numLine; j++)
		{
			Line line = new Line();
			int num = 5 + 180 / numLine * j;
			int num2 = 180 / numLine + 180 / numLine * j - 5;
			if (num2 <= num)
			{
				num2 = num + 1;
			}
			int num3 = CRes.random(minline, maxline);
			int num4 = CRes.random(vline, vline + 3);
			int num5 = CRes.random(num, num2);
			int num6 = CRes.random(13, 23);
			if (numLine == 8)
			{
				num6 = CRes.random(50, 70);
			}
			bool is2Line = CRes.random(4) == 0;
			num5 = CRes.fixangle(num5 % 360);
			line.setLine(x1000 - CRes.getsin(num5) * (num3 + num6), y1000 - CRes.getcos(num5) * (num3 + num6), x1000 - CRes.getsin(num5) * num6, y1000 - CRes.getcos(num5) * num6, CRes.getsin(num5) * num4, CRes.getcos(num5) * num4, is2Line);
			if (numpoint > 1)
			{
				line.type = CRes.random(numpoint);
			}
			VecEff.addElement(line);
			line = new Line();
			num5 += 180 + CRes.random_Am(2, 5);
			num5 = CRes.fixangle(num5 % 360);
			line.setLine(x1000 - CRes.getsin(num5) * (num3 + num6), y1000 - CRes.getcos(num5) * (num3 + num6), x1000 - CRes.getsin(num5) * num6, y1000 - CRes.getcos(num5) * num6, CRes.getsin(num5) * num4, CRes.getcos(num5) * num4, is2Line);
			if (numpoint > 1)
			{
				line.type = CRes.random(numpoint);
			}
			VecEff.addElement(line);
		}
	}

	private void createUrgot_4(int value)
	{
		fraImgEff = new FrameImage(179, 54, 25);
		for (int i = 0; i < value; i++)
		{
			Point point = new Point();
			point.y = -CRes.random(30);
			point.vy = CRes.random_Am(3, 8);
			point.frame = 3 + CRes.random(3);
			VecEff.addElement(point);
		}
		toX = x;
		toY = y;
		x = objTo.x;
		y = objTo.y;
	}

	private void createRock()
	{
		fRemove = CRes.random(24, 40);
		fraImgEff = new FrameImage(139, 10, 10);
		int num = CRes.random(4, 8);
		fRemove = CRes.random(34, 60);
		if (typeEffect == 107)
		{
			fraImgEff = new FrameImage(162, 10, 20);
			num = CRes.random(3, 5);
			fRemove = CRes.random(10, 16);
		}
		else if (typeEffect == 110)
		{
			fraImgEff = new FrameImage(227, 10, 10, 4);
			fraImgSubEff = new FrameImage(226, 13, 11, 3);
			fRemove = CRes.random(34, 60);
		}
		else if (typeEffect == 115)
		{
			num = CRes.random(10, 14);
			fraImgEff = new FrameImage(262, 20, 15);
			fraImgSubEff = new FrameImage(261, 61, 33);
			fRemove = CRes.random(104, 130);
		}
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = x + CRes.random_Am_0(5);
			point.y = y + CRes.random_Am_0(7);
			point.frame = CRes.random(fraImgEff.nFrame);
			point.fSmall = CRes.random(12, 16);
			point.dis = CRes.random(2);
			point.vy = CRes.random_Am(2, 5) - 6;
			point.vx = CRes.random(5);
			point.fRe = fRemove;
			if (typeEffect == 110)
			{
				point.frame = CRes.random(4);
				point.fRe = fRemove;
			}
			else if (typeEffect == 115)
			{
				point.y -= 15;
				if (i == 0)
				{
					point.frame = 3;
				}
				else
				{
					point.frame = CRes.random(3);
				}
				point.fSmall = CRes.random(8, 12);
				point.fRe = point.fSmall + CRes.random(70, 100);
				point.vx = 1 + CRes.random(5);
				levelPaint = -1;
			}
			else if (typeEffect == 107)
			{
				point.dis = CRes.random(2);
				point.vy = CRes.random_Am(2, 5);
				point.vx = CRes.random(2, 5);
			}
			if (i % 2 == 0)
			{
				point.vx = -point.vx;
			}
			VecEff.addElement(point);
		}
	}

	private void createHachi2()
	{
		fRemove = CRes.random(10, 14);
		int num = CRes.random(5, 9);
		if (typeSub == 1)
		{
			num = CRes.random(3, 7);
			fRemove = CRes.random(6, 10);
			fraImgSubEff = new FrameImage(79, 25, 25);
			fraImgEff = new FrameImage(142, 10, 10);
		}
		else if (typeSub == 2)
		{
			fraImgSubEff = new FrameImage(87, 35, 35, 28, 28);
			fraImgEff = new FrameImage(214, 8, 8);
		}
		else
		{
			fraImgSubEff = new FrameImage(141, 50, 54, 36, 39);
			fraImgEff = new FrameImage(142, 10, 10);
		}
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = x + CRes.random_Am_0(20);
			point.y = y + CRes.random_Am_0(20);
			point.frame = CRes.random(fraImgEff.nFrame);
			point.dis = CRes.random(2);
			point.vy = CRes.random_Am(1, 4);
			point.vx = CRes.random(1, 4);
			if (i % 2 == 0)
			{
				point.vx = -point.vx;
			}
			VecEff.addElement(point);
		}
	}

	private void createEND_Lu_S1_Final()
	{
		fraImgEff = new FrameImage(27, 24, 32);
		fRemove = 10;
		if (GameCanvas.isLowGraOrWP_PvP() && typeSub >= 4)
		{
			typeSub = 0;
		}
		if (typeSub == 1 || typeSub == 11 || typeSub == 12)
		{
			fraImgEff = new FrameImage(99, 32, 32);
			fraImgSubEff = new FrameImage(138, 62, 64, 47, 48);
			fRemove = 14;
		}
		else if (typeSub == 2)
		{
			fraImgEff = new FrameImage(225, 24, 32);
		}
		else if (typeSub == 3)
		{
			fraImgEff = new FrameImage(85, 34, 34, 28, 28);
		}
		else if (typeSub >= 4 && typeSub <= 7)
		{
			frame = typeSub - 4;
			fraImgEff = new FrameImage(273, 24, 24, 4);
		}
		else if (typeSub == 8)
		{
			fraImgEff = new FrameImage(273, 24, 24, 4);
		}
		else if (typeSub == 9)
		{
			frame = 2;
			fraImgEff = new FrameImage(273, 24, 24, 4);
		}
		else if (typeSub == 10)
		{
			fraImgEff = new FrameImage(99, 32, 32);
		}
		else if (typeSub == 13)
		{
			fraImgEff = new FrameImage(104, 3);
		}
		for (int i = 0; i < 3; i++)
		{
			Point o = new Point(x + CRes.random_Am_0(15), y + CRes.random_Am_0(15));
			VecEff.addElement(o);
		}
	}

	private void createUssop11()
	{
		fraImgEff = new FrameImage(113, 25, 25);
		fraImgSubEff = new FrameImage(52, 5, 5);
		fRemove = 16;
		numNextFrame = 2;
		for (int i = 0; i < 10; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(3), y + CRes.random_Am_0(3));
			point.vx = CRes.random_Am_0(5);
			point.vy = -5 + CRes.random_Am_0(5);
			point.f = CRes.random(4);
			VecEff.addElement(point);
		}
	}

	private void createUssop10()
	{
		fraImgEff = new FrameImage(112, 20, 14);
		vMax = 12;
		int num = toX - x;
		int num2 = toY - y;
		create_Speed(num, num2, null);
		int frameAngle = CRes.angle(num, num2);
		frame = setFrameAngle(frameAngle);
	}

	private void createUssop9()
	{
		if (typeSub == 1)
		{
			fraImgEff = new FrameImage(107, 50, 54, 38, 41);
		}
		else
		{
			fraImgEff = new FrameImage(108, 38, 38, 32, 32);
		}
		fraImgSubEff = new FrameImage(110, 5, 5);
		fraImgSub2Eff = new FrameImage(109, 9, 9);
		fRemove = 30;
		numNextFrame = 2;
		for (int i = 0; i < 30; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(3), y + CRes.random_Am_0(3));
			point.vx = CRes.random_Am_0(5);
			point.vy = -5 + CRes.random_Am_0(5);
			point.f = CRes.random(4);
			if (frame == 1)
			{
				point.frame = CRes.random(fraImgSubEff.nFrame);
			}
			else
			{
				point.frame = CRes.random(2 + typeSub);
			}
			VecEff.addElement(point);
		}
	}

	private void createUssopS3_Lv4()
	{
		if (typeSub == 1)
		{
			frame = 1;
		}
		else if (typeSub == 2)
		{
			frame = 0;
		}
		else
		{
			frame = CRes.random(2);
		}
		if (frame == 0)
		{
			fraImgEff = new FrameImage(108, 38, 38, 32, 32);
			fraImgSubEff = new FrameImage(110, 5, 5);
			fraImgSub2Eff = new FrameImage(109, 9, 9);
		}
		else
		{
			fraImgEff = new FrameImage(113, 25, 25);
			fraImgSubEff = new FrameImage(221, 5, 5, 4);
			fraImgSub2Eff = new FrameImage(220, 9, 9, 4);
		}
		fRemove = 30;
		numNextFrame = 2;
		for (int i = 0; i < 30; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(3), y + CRes.random_Am_0(3));
			point.vx = CRes.random_Am_0(6);
			point.vy = -7 + CRes.random_Am_0(5);
			point.f = CRes.random(4);
			if (frame == 1)
			{
				point.fSmall = CRes.random(9);
			}
			point.frame = CRes.random(3);
			VecEff.addElement(point);
		}
	}

	private void createUssopS3_Lv6()
	{
		if (typeSub != 0)
		{
			frame = typeSub;
		}
		else
		{
			frame = CRes.random(3);
		}
		if (frame == 1)
		{
			fraImgEff = new FrameImage(19, 5);
			fraImgSubEff = new FrameImage(110, 5, 5);
			fraImgSub2Eff = new FrameImage(109, 9, 9);
		}
		else if (frame == 2)
		{
			fraImgEff = new FrameImage(50, 3);
			fraImgSubEff = new FrameImage(109, 9, 9);
			fraImgSub2Eff = new FrameImage(220, 9, 9, 4);
		}
		else
		{
			fraImgEff = new FrameImage(113, 25, 25);
			fraImgSubEff = new FrameImage(221, 5, 5, 4);
			fraImgSub2Eff = new FrameImage(51, 9, 9);
		}
		fRemove = 30;
		numNextFrame = 2;
		for (int i = 0; i < 30; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(3), y + CRes.random_Am_0(3));
			point.vx = CRes.random_Am_0(6);
			point.vy = -7 + CRes.random_Am_0(5);
			point.f = CRes.random(4);
			if (frame == 1)
			{
				point.fSmall = CRes.random(9);
			}
			point.frame = CRes.random(3);
			VecEff.addElement(point);
		}
	}

	private void createEff_Firework()
	{
		frame = CRes.random(3);
		if (frame == 0)
		{
			fraImgEff = new FrameImage(108, 5);
		}
		else if (frame == 1)
		{
			fraImgEff = new FrameImage(50, 3);
		}
		else
		{
			fraImgEff = new FrameImage(113, 5);
		}
		fRemove = 10;
	}

	private void create_Sanji6()
	{
		fraImgEff = new FrameImage(78, 22, 28);
		int num = CRes.random_Am(1, 4);
		Point point = new Point(x, y);
		point.vx = num;
		point.vy = -3;
		point.fRe = CRes.random(7, 12);
		VecEff.addElement(point);
		point = new Point(x, y);
		point.vx = -num;
		point.vy = 3;
		point.fRe = CRes.random(7, 12);
		VecEff.addElement(point);
		if (typeSub == 1)
		{
			point = new Point(x, y);
			point.vx = -num;
			point.vy = -3;
			point.fRe = CRes.random(10, 14);
			VecEff.addElement(point);
			point = new Point(x, y);
			point.vx = num;
			point.vy = 3;
			point.fRe = CRes.random(10, 14);
			VecEff.addElement(point);
		}
	}

	private void createEffectSkill1()
	{
		if (GameCanvas.isLowGraOrWP_PvP())
		{
			removeEff();
			return;
		}
		if (typeSub == 3)
		{
			fraImgEff = new FrameImage(109, 4);
		}
		else if (typeSub == 2)
		{
			fraImgEff = new FrameImage(110, 5, 5);
		}
		else if (typeSub == 1)
		{
			fraImgEff = new FrameImage(52, 5, 5);
		}
		else
		{
			fraImgEff = new FrameImage(54, 5, 5);
		}
		int num = CRes.random(4, 7);
		for (int i = 0; i < num; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(5), y + CRes.random_Am_0(5));
			point.vx = CRes.random_Am(1, 5);
			point.vy = -7 + CRes.random(1, 4);
			point.fRe = CRes.random(10, 14);
			point.frame = CRes.random(fraImgEff.nFrame);
			VecEff.addElement(point);
		}
	}

	private void createCharTeleportNew()
	{
		fRemove = 15;
		fraImgEff = new FrameImage(66, 15, 55, 1);
		fraImgSubEff = new FrameImage(67, 3, 25, 1);
		y -= 150;
		vy = 30;
		Point point = new Point(x, y);
		point.vy = vy;
		point.dis = 1;
		point.frame = 0;
		point.fRe = 10;
		VecEff.addElement(point);
	}

	private void createCharTeleport()
	{
		fRemove = 15;
		fraImgEff = new FrameImage(66, 15, 55, 1);
		fraImgSubEff = new FrameImage(67, 3, 25, 1);
		for (int i = 0; i < 10; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(20), y - CRes.random(30) + 10);
			point.vy = -2 - CRes.random(4);
			point.dis = 0;
			point.frame = CRes.random(fraImgSubEff.nFrame);
			point.fRe = CRes.random(12, 20);
			VecEff.addElement(point);
		}
	}

	private void createGet_Up()
	{
		fraImgEff = new FrameImage(199, 30, 16);
		fRemove = 10;
		for (int i = 0; i < 4; i++)
		{
			Point point = new Point();
			point.x = x + CRes.random_Am_0(10);
			point.y = y + CRes.random_Am_0(5);
			point.vx = -2 + i % 2 * 4 + CRes.random_Am_0(2);
			point.vy = -1 + CRes.random_Am_0(3);
			point.frame = CRes.random(2);
			point.fRe = CRes.random(6, 10);
			VecEff.addElement(point);
		}
	}

	private void createRevice()
	{
		timeBegin = GameCanvas.timeNow;
		fraImgEff = new FrameImage(179, 54, 25);
		fraImgSubEff = new FrameImage(121, 32, 32);
		timeEnd = 1000;
		for (int i = 0; i < 3; i++)
		{
			Point point = new Point();
			point.y = -CRes.random(30);
			point.vy = CRes.random_Am(3, 8);
			point.frame = 3 + CRes.random(3);
			VecEff.addElement(point);
		}
	}

	private void create_Upgrade_Thanh_Cong()
	{
		fraImgEff = new FrameImage(51, 9, 9);
		fraImgSubEff = new FrameImage(52, 5, 5);
		int num = CRes.random(25, 40);
		for (int i = 0; i < num; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(6), y + CRes.random_Am_0(6));
			point.fRe = CRes.random(20, 30);
			point.vx = CRes.random_Am(1, 6);
			point.vy = CRes.random_Am(1, 6);
			point.frame = CRes.random(3);
			VecEff.addElement(point);
		}
	}

	private void createPartical()
	{
		fRemove = CRes.random(12, 18);
		fraImgSubEff = new FrameImage(220, 9, 9, 4);
		fraImgEff = new FrameImage(221, 5, 5, 4);
		int num = CRes.random(8, 12);
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = x + CRes.random_Am_0(3);
			point.y = y + CRes.random_Am_0(5);
			point.frame = CRes.random(4);
			point.dis = CRes.random(3);
			point.vy = CRes.random_Am(1, 4) - 5;
			point.vx = CRes.random(6);
			if (i % 2 == 0)
			{
				point.vx = -point.vx;
			}
			VecEff.addElement(point);
		}
	}

	private void createLittleDamBoss()
	{
		if (typeSub == 0)
		{
			x = MotherCanvas.w / 2 - 60;
		}
		else
		{
			x = MotherCanvas.w / 2 + 100;
		}
		y = 40;
		fRemove = CRes.random(16, 22);
		fraImgEff = new FrameImage(139, 10, 10);
		int num = CRes.random(5, 9);
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = x + CRes.random_Am_0(5);
			point.y = y + CRes.random_Am_0(7);
			point.frame = CRes.random(fraImgEff.nFrame);
			point.dis = CRes.random(2);
			point.vy = -CRes.random(6, 9);
			point.vx = CRes.random(1, 4);
			if (i % 2 == 0)
			{
				point.vx = -point.vx;
			}
			VecEff.addElement(point);
		}
	}

	private void createLittleHpBoss()
	{
		if (typeSub == 0)
		{
			x = MotherCanvas.w / 2 - 60;
		}
		else
		{
			x = MotherCanvas.w / 2 + 100;
		}
		y = 40;
		fraImgEff = new FrameImage(204, 7, 7);
		fRemove = 12;
		for (int i = 0; i < 6; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(30), y + CRes.random_Am_0(10));
			point.fRe = CRes.random(8, 11);
			point.vy = -CRes.random(5, 7);
			VecEff.addElement(point);
		}
	}

	private void createMr3_1()
	{
		fraImgEff = new FrameImage(210, 63, 75, 44, 52);
		mPlayFrame = new int[20]
		{
			0, 0, 1, 1, 1, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 1, 1, 0, 0, 0
		};
		fRemove = mPlayFrame.Length;
	}

	private void createKuromarimo()
	{
		fraImgEff = new FrameImage(207, 14, 14);
		fraImgSubEff = new FrameImage(78, 22, 28);
		fRemove = 30;
		if (objTo != null)
		{
			if (typeSub == 0)
			{
				x = objTo.x + 6;
			}
			else
			{
				x = objTo.x - 6;
			}
			y = objTo.y - objTo.hOne / 2 + 14;
		}
	}

	public void createChopper()
	{
		fraImgEff = new FrameImage(204, 7, 7);
		fRemove = 12;
		for (int i = 0; i < 4; i++)
		{
			Point point = new Point(x + CRes.random_Am_0(20), y + CRes.random_Am_0(10));
			point.fRe = CRes.random(8, 11);
			point.vy = -CRes.random(5, 7);
			VecEff.addElement(point);
		}
	}

	private void createClassZoro()
	{
		vMax = 18;
		int xdich = objTo.x - x;
		int ydich = objTo.y - y;
		create_Speed(xdich, ydich, null);
		fraImgEff = new FrameImage(10, 40, 47);
		fraImgSubEff = new FrameImage(183, 20, 54);
	}

	private void createClassUssop()
	{
		if (objTo != null)
		{
			y -= 25;
			vMax = 16;
			fraImgEff = new FrameImage(188, 9, 16);
			Point_Focus p = new Point_Focus();
			int xdich = objTo.x - x;
			int ydich = objTo.y - objTo.hOne / 2 - (y - objTo.dy);
			p = create_Speed(xdich, ydich, p, x, y, objTo.x, objTo.y - objTo.hOne / 2);
			VecEff.addElement(p);
		}
		else
		{
			isRemove = true;
		}
	}

	private void createClassNami()
	{
		fRemove = 8;
		if (typeEffect == 159)
		{
			fraImgEff = new FrameImage(387, 24, 240);
			fraImgSubEff = new FrameImage(26, 40, 20);
		}
		else
		{
			fraImgEff = new FrameImage(24, 15, 240);
			fraImgSubEff = new FrameImage(26, 40, 20);
		}
		GameScreen.addEffectEnd(59, 0, x, y, Dir, objMainEff);
	}

	private void create_US_S2_L5()
	{
		fraImgEff = new FrameImage(283, 22, 28);
		if (typeEffect == 140)
		{
			fraImgSubEff = new FrameImage(78, 22, 28);
		}
		else
		{
			fraImgSubEff = new FrameImage(78, 22, 28, 5);
		}
		x += CRes.random_Am_0(5);
		y += CRes.random_Am_0(5);
		Point point = new Point(x + CRes.random_Am_0(3), y + CRes.random_Am_0(3));
		point.frame = 0;
		VecEff.addElement(point);
		Point point2 = new Point(x + CRes.random_Am_0(3), y + CRes.random_Am_0(3));
		point2.frame = 1;
		VecEff.addElement(point2);
		fRemove = CRes.random(8, 20);
	}

	private void createZoro_S3()
	{
		vx = -10;
		if (Dir == 2)
		{
			vx = 10;
		}
		if (typeSub == 1)
		{
			fraImgEff = new FrameImage(423, 32, 60);
		}
		else
		{
			fraImgEff = new FrameImage(322, 32, 60);
		}
		numNextFrame = 1;
		fRemove = 12;
		if (typeSub == 0)
		{
			for (int i = 0; i < 3; i++)
			{
				Point point = new Point(x, y - 15 + i * 15);
				if (Dir == 2)
				{
					point.x -= 6 * i;
				}
				else
				{
					point.x += 6 * i;
				}
				point.fRe = 12;
				VecEff.addElement(point);
				int num = point.x;
				num = ((Dir != 2) ? (num - 10) : (num + 10));
				GameScreen.addEffectEnd_ToX_ToY(62, 0, num, point.y - 17, num + 12 * vx, point.y - 17, Dir, objMainEff);
			}
		}
		else
		{
			if (typeSub != 1)
			{
				return;
			}
			for (int j = 0; j < 4; j++)
			{
				Point point2 = new Point(x, y - 21 + j * 14);
				if (Dir == 2)
				{
					point2.x -= 6 * j;
				}
				else
				{
					point2.x += 6 * j;
				}
				point2.fRe = 12;
				VecEff.addElement(point2);
				int num2 = point2.x;
				num2 = ((Dir != 2) ? (num2 - 10) : (num2 + 10));
				GameScreen.addEffectEnd_ToX_ToY(62, 0, num2, point2.y - 17, num2 + 12 * vx, point2.y - 17, Dir, objMainEff);
			}
		}
	}

	private void createLOL_Tru_Tren()
	{
		fraImgEff = new FrameImage(307, 4, 5, 6);
		int num = 20;
		if (GameCanvas.isLowGraOrWP_PvP())
		{
			num = 10;
		}
		maxsize = num;
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = CRes.random_Am_0(15);
			point.y = 10 - CRes.random(40);
			point.dis = 1 + CRes.random(3);
			point.vy = -4;
			point.frame = CRes.random(12);
			VecEff.addElement(point);
		}
	}

	private void create_Tru_Be()
	{
	}

	private void create_Poke_Ok()
	{
		fraImgEff = new FrameImage(302, 16);
		fraImgSubEff = new FrameImage(192, 25, 25);
		fraImgSub2Eff = new FrameImage(303, 49, 25);
		fRemove = 40;
		x *= 100;
		y *= 100;
		vx = CRes.random_Am(100, 300);
		vy = -CRes.random(600, 700);
		vMax = 50;
		frame = typeSub % 10;
	}

	private void create_Poke_Fail()
	{
		fraImgEff = new FrameImage(302, 16);
		fRemove = CRes.random(10, 16);
		x *= 100;
		y *= 100;
		vx = CRes.random_Am(200, 500);
		vy = -CRes.random(400, 600);
		vMax = 50;
		frame = typeSub % 10;
	}

	private void create_Poke_Begin()
	{
		fraImgEff = new FrameImage(302, 16);
		fRemove = 40;
		vMax = 8;
		y = objMainEff.y - objMainEff.hOne / 2 + 6;
		frame = typeSub % 10;
		objMainEff.frame = 33;
		if (objTo.x < objMainEff.x)
		{
			Dir = 0;
			objMainEff.Dir = 0;
			x -= 10;
		}
		else
		{
			Dir = 2;
			objMainEff.Dir = 2;
			x += 10;
		}
		int xdich = objTo.x - x;
		int ydich = objTo.y - 10 - y;
		create_Speed(xdich, ydich, null);
		if (fRemove <= 0)
		{
			fRemove = 1;
		}
		mposy = new int[fRemove];
		mposy[0] = 3;
		int num = fRemove / 2;
		for (int i = 1; i < mposy.Length; i++)
		{
			if (i <= num)
			{
				mposy[i] = mposy[i - 1] + 3;
				continue;
			}
			int num2 = mposy[i - 1] - 3;
			if (num2 < 0)
			{
				num2 = 0;
			}
			mposy[i] = num2;
		}
	}

	private void createEndLuS1L4()
	{
		fraImgEff = new FrameImage(274, 22, 74, 3);
		numNextFrame = 1;
		fRemove = 7;
		Point point = new Point();
		point.x = x;
		point.y = y;
		if (Dir == 0)
		{
			point.vx = -6;
		}
		else
		{
			point.vx = 6;
		}
		if (typeSub == 1)
		{
			mPlayFrame = new int[5] { 3, 4, 4, 4, 3 };
			fRemove = 5;
		}
		else if (typeSub == 2)
		{
			mPlayFrame = new int[4] { 3, 4, 4, 3 };
			fRemove = 4;
		}
		else if (typeSub == 3)
		{
			mPlayFrame = new int[7] { 0, 4, 2, 6, 2, 4, 0 };
			if (Dir == 0)
			{
				point.vx = -8;
			}
			else
			{
				point.vx = 8;
			}
		}
		else if (typeSub == 4)
		{
			mPlayFrame = new int[7] { 3, 10, 5, 11, 5, 10, 3 };
			if (Dir == 0)
			{
				point.vx = -4;
			}
			else
			{
				point.vx = 4;
			}
		}
		else
		{
			mPlayFrame = new int[7] { 0, 1, 2, 2, 2, 1, 0 };
		}
		point.fRe = mPlayFrame.Length;
		VecEff.addElement(point);
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
}
