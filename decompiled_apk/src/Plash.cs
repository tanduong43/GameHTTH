using System;

public class Plash
{
	public static MyHashTable hashPlash = new MyHashTable();

	private MainSkill skill;

	private Plashdata plashdata;

	public short typePlash;

	public short timeEndPlash;

	public int f;

	public int removef;

	public int fAddEff;

	private mVector vecObj;

	private MainObject objFire;

	private bool isShow;

	private sbyte isNextf;

	private long timebeginSkill;

	public const short LU_SUNGLIENTHANH = 0;

	public const short LU_SUNGLUC = 1;

	public const short SAN_DATHUONG = 2;

	public const short ZORO_BIENHINH = 3;

	public const short USS_BAN2VIEN = 4;

	public const short USS_BANTUONGOT = 5;

	public const short NAMI_BUNGMERANG = 6;

	public const short NAMI_SET = 7;

	public const short NAMI_LOCXOAY = 8;

	public const short SAN_DACHIEULUA = 9;

	public const short SAN_DAGIO = 10;

	public const short LU_TRAI_PHAI_CHAN = 11;

	public const short SAN_CHAN_CHAY = 12;

	public const short ZORO_CHEM_CHAY_L2 = 13;

	public const short NAMI_DANH_CHAY = 14;

	public const short USS_BAN_CHAY = 15;

	public const short LU_DA_CHE = 16;

	public const short ZORO_CHEM_GIO = 17;

	public const short USS_BUA_TA = 18;

	public const short LU_DAM_2_CAI = 19;

	public const short LU_DAM_2_TAY = 20;

	public const short LU_DAM_2_CAI_DAM_2_TAY = 21;

	public const short LU_BAY_DAM_2_CAI = 22;

	public const short LU_BAY_DAM_2_CAI_DAM_2_TAY = 23;

	public const short ZORO_CHEM_2_CAI = 24;

	public const short ZORO_BAY_CHEM_2_CAI = 25;

	public const short ZORO_CHEM_ALPHA = 26;

	public const short ZORO_CHEM_LUOT_QUA = 27;

	public const short NAMI_BONGBONG_SET = 28;

	public const short LU_DAM_2_TAY_LV2 = 29;

	public const short ZORO_CHEM_CHAY_L1 = 30;

	public const short BUFF_0 = 31;

	public const short NAMI_BUNGMERANG_2 = 32;

	public const short USS_BANTUONGOT_2 = 33;

	public const short USS_BANPHAOHOA = 34;

	public const short SPE_CAUSU_1 = 35;

	public const short MON_DAM_1_CAI = 36;

	public const short MON_PHONG_1_DAO = 37;

	public const short MON_BAN_1_CAI = 38;

	public const short MON_DANH_SUNG_1_CAI = 39;

	public const short ALVIDA_DANH_DA_MUC_TIEU = 40;

	public const short MON_TAY_1_TRAI_DA = 41;

	public const short MON_TAY_1_PHAI_TRAI = 42;

	public const short MON_CHAN_2_CAI_TAY_TRAI = 43;

	public const short LU_SKILL_1_FINAL = 44;

	public const short LU_SKILL_2_FINAL = 45;

	public const short LU_SKILL_3_FINAL = 46;

	public const short ZORO_SKILL_1_FINAL = 47;

	public const short ZORO_SKILL_2_FINAL = 48;

	public const short MORGAN_CHEM_CAI = 49;

	public const short MORGAN_CHEM_NGANG = 50;

	public const short HELMEPO_1 = 51;

	public const short HELMEPO_2 = 52;

	public const short MON_BAN_SUNG_TRUONG = 53;

	public const short MOHJI_1 = 54;

	public const short MOHJI_2 = 55;

	public const short BUGGY_1 = 56;

	public const short BUGGY_2 = 57;

	public const short CABAJI_1 = 58;

	public const short CABAJI_2 = 59;

	public const short NYABAN_1 = 60;

	public const short NYABAN_2 = 61;

	public const short NYABAN_3 = 62;

	public const short KURO_1 = 63;

	public const short KURO_2 = 64;

	public const short PEARL_1 = 65;

	public const short PEARL_2 = 66;

	public const short GHIN_1 = 67;

	public const short GHIN_2 = 68;

	public const short DON_KRIEG_1 = 69;

	public const short DON_KRIEG_3 = 70;

	public const short HACHI_2 = 71;

	public const short CHU_1 = 72;

	public const short CHU_2 = 73;

	public const short KUROBI_1 = 74;

	public const short KUROBI_2 = 75;

	public const short ARLONG_1 = 76;

	public const short ARLONG_2 = 77;

	public const short ARLONG_3 = 78;

	public const short ZORO_XOAY_KIEM = 79;

	public const short ZORO_XOAY_KIEM_2 = 80;

	public const short ZORO_XOAY_KIEM_3 = 81;

	public const short PAN_1 = 82;

	public const short PAN_2 = 83;

	public const short GALIO_1 = 84;

	public const short GALIO_2 = 85;

	public const short NO_NANG_LUONG = 86;

	public const short MON_CHAY_THANG = 87;

	public const short SAN_SKILL_1_FINAL = 88;

	public const short SAN_SKILL_2_FINAL = 89;

	public const short USS_SKILL_1_FINAL = 90;

	public const short USS_SKILL_2_FINAL = 91;

	public const short MON_NEM_BOOM = 92;

	public const short MON_DANH_TAY_TRAI_PHAI_MANH = 93;

	public const short MON_DANH_TAY_PHAI_DA_MANH = 94;

	public const short MON_DA_TAY_MANH = 95;

	public const short MON_CHEM_2_CAI = 96;

	public const short MON_CHEM_3_CAI = 97;

	public const short LU_SEA_LV1 = 98;

	public const short LU_SEA_LV2 = 99;

	public const short LU_SEA_LV3 = 100;

	public const short ZORO_SEA_LV1 = 101;

	public const short ZORO_SEA_LV2 = 102;

	public const short ZORO_SEA_LV3 = 103;

	public const short SAN_SEA_LV1 = 104;

	public const short SAN_SEA_LV2 = 105;

	public const short SAN_SEA_LV3 = 106;

	public const short NAMI_SEA_LV2 = 107;

	public const short NAMI_SEA_LV3 = 108;

	public const short USS_SEA_LV2 = 109;

	public const short USS_SEA_LV3 = 110;

	public const short ACE_1 = 111;

	public const short ACE_2 = 112;

	public const short AOKIJI_1 = 113;

	public const short AOKIJI_2 = 114;

	public const short SMOKER_1 = 115;

	public const short SMOKER_2 = 116;

	public const short MON_SMOKER_1 = 117;

	public const short MON_SMOKER_2 = 118;

	public const short MON_CHEM_2_CAI_NEW = 119;

	public const short MON_DANH_SUNG_DA = 120;

	public const short ZORO_CHEM_2_CAI_XA = 121;

	public const short LU_S1_L3_SHORT = 122;

	public const short ZORO_S1_L3_SHORT = 123;

	public const short SAN_S1_L3_SHORT = 124;

	public const short NAMI_S1_L3_SHORT = 125;

	public const short USS_S1_L3_SHORT = 126;

	public const short LU_SKILL_2_FINAL_SHORT = 127;

	public const short ZORO_SKILL_2_FINAL_SHORT = 128;

	public const short SAN_SKILL_2_FINAL_SHORT = 129;

	public const short NAMI_BUNGMERANG_SHORT = 130;

	public const short BUFF_2 = 131;

	public const short BUFF_2_END = 132;

	public const short EFF_GET_MONEY = 133;

	public const short SAN_DACHIEULUA_LV3 = 134;

	public const short NAMI_BONGBONG_SET_LV3 = 135;

	public const short MON_VALENTINE = 136;

	public const short MON_MR5_1 = 137;

	public const short CROCODILE_1 = 138;

	public const short CROCODILE_2 = 139;

	public const short CHESS = 140;

	public const short KUROMARIMO = 141;

	public const short WAPOL_1 = 142;

	public const short WAPOL_2 = 143;

	public const short WAPOL_3 = 144;

	public const short WAPOL_4 = 145;

	public const short MR3_1 = 146;

	public const short MR3_2 = 147;

	public const short MISS_GOLDEN_WEEKEND = 148;

	public const short MISS_MS_1 = 149;

	public const short MON_FIRE = 150;

	public const short SET_1 = 151;

	public const short SET_2 = 152;

	public const short NHAM_THACH_1 = 153;

	public const short MR1_1 = 154;

	public const short MR1_2 = 155;

	public const short DF_1 = 156;

	public const short DF_2 = 157;

	public const short MR2_2 = 158;

	public const short MR2_1 = 159;

	public const short MR0_1 = 160;

	public const short PELL_1 = 161;

	public const short LU_S1_LV4 = 162;

	public const short ENEL_1 = 163;

	public const short ENEL_2 = 164;

	public const short ENEL_3 = 165;

	public const short SATORI_1 = 166;

	public const short SATORI_2 = 167;

	public const short OHM_1 = 168;

	public const short OHM_2 = 169;

	public const short GEDATSU_1 = 170;

	public const short GEDATSU_2 = 171;

	public const short SHURA_1 = 172;

	public const short SHURA_2 = 173;

	public const short LINHTROI_1 = 174;

	public const short TRU_1 = 175;

	public const short LUCCI_1 = 176;

	public const short DONG_DAT_1 = 177;

	public const short DONG_DAT_2 = 178;

	public const short LU_S3_L5 = 179;

	public const short SAN_S2_L5 = 180;

	public const short USS_S1_L5 = 181;

	public const short ACE_1_L2 = 182;

	public const short DEVIL_MR3_L2 = 183;

	public const short DEVIL_KILO_L1 = 184;

	public const short DEVIL_DAO_L1 = 185;

	public const short THA_DEN = 186;

	public const short THA_PHAO_HOA = 187;

	public const short LAW_HEART = 188;

	public const short LUFFY_S1_L6 = 189;

	public const short ZORO_S2_L6 = 190;

	public const short GOAL = 191;

	public const short FIRE_EXPLORE = 194;

	public static short[][] mframePlash = new short[194][]
	{
		new short[23]
		{
			8, 8, 8, 9, 9, 9, 10, 10, 10, 13,
			13, 15, 15, 13, 13, 17, 17, 13, 13, 15,
			15, 13, 13
		},
		new short[16]
		{
			37, 37, 37, 37, 37, 37, 17, 17, 17, 17,
			17, 17, 17, 17, 17, 17
		},
		new short[18]
		{
			8, 8, 9, 10, 27, 27, 27, 27, 27, 27,
			27, 27, 30, 30, 29, 29, 27, 27
		},
		new short[6] { 8, 9, 10, 21, 25, 25 },
		new short[15]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 35,
			37, 31, 31, 33, 35
		},
		new short[8] { 8, 8, 9, 9, 10, 35, 35, 37 },
		new short[10] { 8, 8, 9, 9, 39, 39, 23, 23, 25, 25 },
		new short[12]
		{
			31, 39, 31, 39, 31, 39, 31, 39, 31, 39,
			31, 39
		},
		new short[15]
		{
			31, 31, 31, 39, 39, 31, 31, 31, 39, 39,
			31, 31, 31, 39, 39
		},
		new short[22]
		{
			8, 8, 9, 9, 27, 27, 27, 27, 27, 27,
			30, 30, 28, 28, 28, 28, 29, 29, 28, 28,
			30, 30
		},
		new short[12]
		{
			28, 29, 29, 28, 28, 29, 29, 28, 28, 29,
			29, 28
		},
		new short[15]
		{
			8, 8, 17, 17, 17, 14, 14, 15, 15, 15,
			14, 14, 56, 56, 56
		},
		new short[17]
		{
			8, 8, 9, 10, 27, 27, 29, 29, 29, 27,
			30, 30, 30, 13, 15, 15, 15
		},
		new short[19]
		{
			13, 13, 13, 13, 13, 13, 15, 15, 15, 13,
			13, 15, 15, 15, 13, 13, 17, 17, 17
		},
		new short[23]
		{
			8, 8, 9, 9, 31, 31, 31, 31, 31, 31,
			39, 39, 31, 31, 31, 31, 39, 39, 31, 31,
			31, 31, 31
		},
		new short[21]
		{
			8, 8, 9, 9, 31, 31, 31, 31, 33, 33,
			33, 35, 35, 37, 37, 37, 31, 33, 35, 37,
			37
		},
		new short[18]
		{
			28, 28, 28, 30, 30, 30, 30, 30, 30, 30,
			30, 55, 55, 55, 56, 56, 56, 27
		},
		new short[21]
		{
			10, 10, 8, 8, 8, 8, 9, 9, 8, 8,
			8, 10, 10, 22, 22, 22, 22, 22, 22, 25,
			25
		},
		new short[14]
		{
			23, 23, 23, 24, 22, 22, 22, 22, 26, 26,
			25, 25, 25, 25
		},
		new short[12]
		{
			8, 8, 9, 10, 17, 17, 17, 14, 14, 15,
			15, 15
		},
		new short[19]
		{
			8, 8, 9, 10, 45, 45, 43, 44, 44, 45,
			46, 46, 47, 26, 26, 25, 25, 25, 25
		},
		new short[30]
		{
			8, 8, 9, 9, 17, 17, 17, 14, 14, 15,
			15, 15, 45, 45, 45, 45, 43, 43, 44, 45,
			45, 46, 47, 26, 26, 25, 25, 25, 25, 25
		},
		new short[17]
		{
			8, 8, 9, 9, 11, 11, 11, 18, 18, 18,
			14, 14, 16, 16, 16, 12, 12
		},
		new short[29]
		{
			8, 8, 9, 9, 11, 11, 11, 18, 18, 18,
			14, 14, 16, 16, 16, 45, 45, 45, 43, 43,
			44, 45, 46, 47, 26, 26, 25, 25, 25
		},
		new short[13]
		{
			13, 13, 13, 19, 19, 15, 15, 15, 13, 13,
			17, 17, 17
		},
		new short[13]
		{
			14, 14, 14, 20, 20, 16, 16, 16, 14, 14,
			18, 18, 18
		},
		new short[5] { 25, 25, 25, 25, 25 },
		new short[15]
		{
			8, 8, 9, 9, 10, 10, 23, 23, 23, 23,
			25, 25, 25, 25, 25
		},
		new short[24]
		{
			8, 8, 9, 9, 31, 31, 31, 31, 31, 39,
			39, 31, 31, 31, 31, 31, 31, 39, 39, 31,
			31, 31, 31, 31
		},
		new short[17]
		{
			8, 8, 9, 10, 45, 45, 43, 44, 45, 46,
			47, 26, 26, 25, 25, 25, 25
		},
		new short[15]
		{
			13, 13, 13, 13, 13, 13, 13, 15, 15, 15,
			13, 13, 17, 17, 17
		},
		new short[10] { 8, 9, 10, 10, 10, 10, 10, 10, 10, 10 },
		new short[20]
		{
			8, 8, 9, 9, 39, 39, 23, 23, 25, 25,
			8, 8, 9, 9, 39, 39, 23, 23, 25, 25
		},
		new short[11]
		{
			8, 8, 9, 9, 10, 35, 35, 37, 31, 31,
			33
		},
		new short[30]
		{
			8, 8, 8, 9, 9, 9, 9, 35, 35, 35,
			35, 35, 35, 35, 35, 35, 35, 35, 35, 35,
			35, 35, 35, 35, 35, 37, 9, 9, 8, 8
		},
		new short[30]
		{
			8, 8, 9, 9, 10, 13, 15, 13, 17, 13,
			15, 13, 15, 13, 17, 13, 15, 13, 15, 13,
			17, 13, 15, 13, 17, 13, 15, 13, 10, 10
		},
		new short[10] { 8, 8, 9, 9, 15, 15, 15, 19, 19, 19 },
		new short[7] { 8, 9, 13, 13, 15, 15, 15 },
		new short[7] { 8, 9, 35, 35, 35, 37, 37 },
		new short[7] { 8, 9, 21, 21, 21, 25, 25 },
		new short[13]
		{
			8, 9, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 25, 25
		},
		new short[13]
		{
			8, 8, 9, 9, 15, 15, 15, 27, 27, 28,
			28, 28, 27
		},
		new short[11]
		{
			8, 8, 9, 9, 17, 17, 17, 13, 13, 16,
			16
		},
		new short[18]
		{
			8, 8, 9, 9, 27, 28, 28, 28, 27, 27,
			29, 29, 13, 13, 15, 15, 15, 15
		},
		new short[45]
		{
			8, 8, 9, 10, 17, 17, 17, 13, 13, 15,
			15, 15, 13, 13, 17, 17, 17, 13, 13, 15,
			15, 15, 8, 8, 8, 9, 9, 9, 10, 10,
			10, 8, 8, 8, 9, 9, 9, 10, 10, 13,
			13, 17, 18, 17, 17
		},
		new short[39]
		{
			8, 9, 10, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 25, 25, 25, 25, 25, 8, 8,
			8, 8, 8, 8, 8, 8, 45, 43, 44, 45,
			46, 47, 26, 26, 25, 25, 25, 25, 8
		},
		new short[35]
		{
			8, 8, 8, 8, 9, 9, 9, 9, 8, 8,
			8, 8, 9, 9, 9, 9, 10, 10, 10, 10,
			10, 13, 13, 15, 15, 13, 13, 17, 17, 13,
			13, 15, 15, 13, 13
		},
		new short[55]
		{
			13, 13, 13, 13, 13, 13, 13, 15, 15, 15,
			13, 13, 17, 17, 17, 13, 13, 13, 13, 13,
			13, 13, 15, 15, 21, 21, 13, 13, 14, 14,
			14, 14, 14, 14, 16, 16, 16, 14, 14, 18,
			18, 18, 14, 14, 22, 22, 26, 26, 26, 14,
			14, 14, 14, 13, 13
		},
		new short[47]
		{
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 14, 14, 14,
			16, 16, 14, 14, 14, 14, 14, 18, 18, 18,
			22, 22, 22, 22, 26, 26, 26, 23, 23, 23,
			23, 23, 23, 25, 25, 25, 25
		},
		new short[11]
		{
			21, 21, 21, 21, 21, 21, 21, 21, 35, 35,
			35
		},
		new short[13]
		{
			8, 8, 9, 9, 10, 10, 13, 13, 23, 23,
			31, 31, 35
		},
		new short[7] { 13, 13, 13, 13, 15, 15, 15 },
		new short[7] { 49, 49, 49, 49, 17, 17, 17 },
		new short[7] { 8, 9, 39, 39, 39, 41, 41 },
		new short[28]
		{
			8, 8, 9, 9, 10, 10, 13, 13, 13, 13,
			13, 13, 15, 15, 15, 15, 13, 13, 13, 13,
			17, 17, 17, 17, 13, 13, 13, 13
		},
		new short[15]
		{
			8, 8, 9, 9, 10, 17, 17, 17, 13, 13,
			15, 15, 15, 13, 13
		},
		new short[11]
		{
			8, 8, 9, 9, 10, 35, 35, 15, 15, 15,
			35
		},
		new short[48]
		{
			10, 10, 9, 9, 8, 8, 8, 8, 8, 8,
			8, 8, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 35, 35, 35, 35, 0, 1, 0, 1,
			0, 1, 0, 1, 0, 1, 10, 10, 10, 10,
			9, 9, 9, 8, 8, 8, 8, 8
		},
		new short[11]
		{
			8, 8, 9, 9, 10, 10, 25, 25, 26, 26,
			8
		},
		new short[21]
		{
			8, 8, 9, 9, 10, 10, 49, 49, 50, 50,
			50, 50, 50, 50, 50, 50, 39, 39, 40, 40,
			8
		},
		new short[16]
		{
			8, 8, 9, 9, 10, 10, 15, 15, 15, 15,
			10, 10, 17, 17, 17, 17
		},
		new short[17]
		{
			8, 8, 9, 9, 10, 10, 22, 22, 22, 22,
			22, 61, 61, 61, 61, 61, 61
		},
		new short[31]
		{
			8, 8, 9, 9, 10, 10, 11, 11, 11, 11,
			11, 11, 15, 15, 15, 15, 10, 10, 17, 17,
			17, 17, 11, 11, 11, 11, 11, 11, 11, 11,
			11
		},
		new short[25]
		{
			8, 8, 9, 9, 10, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 11, 11, 16, 16, 16,
			16, 16, 11, 11, 11
		},
		new short[45]
		{
			8, 8, 9, 9, 10, 0, 0, 0, 0, 0,
			0, 0, 0, 16, 16, 18, 18, 28, 28, 16,
			16, 18, 18, 28, 28, 16, 16, 18, 18, 28,
			28, 16, 16, 16, 16, 18, 18, 28, 28, 16,
			16, 18, 18, 0, 0
		},
		new short[18]
		{
			8, 8, 9, 9, 10, 10, 23, 23, 23, 23,
			23, 23, 23, 11, 11, 25, 25, 25
		},
		new short[34]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			13, 13, 15, 15, 13, 13, 15, 15, 13, 13,
			15, 15, 13, 13, 15, 15, 13, 13, 15, 15,
			13, 13, 10, 10
		},
		new short[10] { 8, 8, 9, 9, 11, 11, 17, 17, 17, 17 },
		new short[32]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			10, 10, 43, 43, 44, 44, 45, 45, 46, 46,
			47, 47, 48, 48, 43, 43, 44, 44, 45, 45,
			8, 8
		},
		new short[18]
		{
			8, 8, 9, 9, 13, 13, 15, 15, 15, 15,
			15, 15, 15, 13, 13, 15, 15, 15
		},
		new short[19]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			21, 21, 15, 15, 15, 15, 15, 15, 15
		},
		new short[24]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			12, 12, 12, 12
		},
		new short[22]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 11, 11,
			13, 13, 11, 11, 13, 13, 11, 11, 13, 13,
			10, 10
		},
		new short[42]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 11, 11,
			13, 13, 11, 11, 13, 13, 11, 11, 13, 13,
			11, 11, 13, 13, 11, 11, 13, 13, 11, 11,
			13, 13, 11, 11, 13, 13, 11, 11, 13, 13,
			10, 10
		},
		new short[37]
		{
			8, 8, 9, 9, 10, 10, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 14, 15, 15, 15,
			15, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			11, 17, 17, 17, 17, 10, 10
		},
		new short[37]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 11, 11, 11, 11, 26, 26,
			26, 26, 26, 26, 51, 51, 51, 51, 51, 51,
			51, 56, 56, 56, 60, 60, 10
		},
		new short[16]
		{
			8, 8, 9, 9, 10, 10, 16, 16, 16, 16,
			16, 16, 16, 16, 10, 10
		},
		new short[8] { 8, 8, 9, 9, 11, 11, 11, 11 },
		new short[16]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 19, 19,
			13, 13, 15, 15, 10, 10
		},
		new short[33]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25
		},
		new short[39]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25
		},
		new short[30]
		{
			8, 9, 25, 25, 25, 25, 26, 54, 25, 26,
			54, 25, 26, 54, 25, 26, 54, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25
		},
		new short[6] { 8, 8, 9, 9, 10, 10 },
		new short[22]
		{
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22
		},
		new short[42]
		{
			8, 8, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			10, 10
		},
		new short[8] { 22, 22, 22, 25, 25, 25, 25, 25 },
		new short[2] { 10, 10 },
		new short[8] { 2, 3, 3, 4, 5, 6, 6, 7 },
		new short[49]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			27, 27, 29, 29, 27, 27, 27, 27, 30, 30,
			14, 14, 14, 14, 14, 14, 14, 14, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 18, 18, 18,
			18, 14, 14, 14, 14, 14, 14, 14, 14
		},
		new short[44]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			27, 27, 29, 29, 13, 13, 13, 13, 15, 15,
			11, 11, 17, 17, 7, 7, 7, 7, 30, 30,
			27, 27, 29, 29, 7, 7, 7, 7, 15, 15,
			15, 15, 10, 10
		},
		new short[34]
		{
			8, 8, 9, 9, 10, 35, 35, 37, 31, 33,
			1, 1, 35, 35, 35, 37, 31, 33, 1, 1,
			35, 35, 35, 37, 31, 33, 1, 1, 35, 35,
			35, 37, 31, 33
		},
		new short[24]
		{
			8, 8, 9, 9, 10, 35, 35, 35, 35, 35,
			35, 35, 35, 35, 35, 35, 35, 35, 35, 35,
			35, 35, 37, 37
		},
		new short[2] { 15, 15 },
		new short[17]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 17, 17,
			13, 13, 13, 13, 13, 16, 16
		},
		new short[24]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 15, 15,
			27, 27, 27, 27, 27, 27, 29, 29, 27, 27,
			27, 27, 28, 28
		},
		new short[25]
		{
			8, 8, 9, 9, 27, 27, 27, 27, 28, 28,
			13, 13, 13, 13, 13, 15, 15, 10, 10, 10,
			10, 10, 10, 17, 17
		},
		new short[16]
		{
			8, 8, 9, 9, 19, 19, 19, 25, 25, 25,
			13, 13, 13, 25, 25, 25
		},
		new short[20]
		{
			8, 8, 9, 9, 19, 19, 19, 25, 25, 25,
			13, 13, 13, 25, 25, 13, 13, 13, 25, 25
		},
		new short[21]
		{
			8, 9, 10, 10, 13, 13, 15, 15, 15, 13,
			13, 13, 13, 13, 13, 15, 15, 15, 13, 13,
			13
		},
		new short[20]
		{
			13, 13, 15, 15, 13, 13, 13, 17, 17, 13,
			13, 14, 14, 14, 16, 16, 14, 14, 14, 13
		},
		new short[45]
		{
			8, 9, 10, 10, 13, 13, 13, 13, 13, 13,
			13, 13, 15, 15, 15, 13, 13, 13, 13, 13,
			13, 13, 17, 17, 17, 13, 13, 13, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 16, 16,
			16, 14, 14, 14, 13
		},
		new short[15]
		{
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			22, 22, 22, 25, 25
		},
		new short[20]
		{
			21, 21, 21, 22, 22, 25, 25, 21, 21, 21,
			21, 21, 21, 21, 21, 22, 22, 22, 25, 25
		},
		new short[20]
		{
			14, 14, 14, 14, 10, 22, 22, 26, 26, 22,
			22, 22, 26, 26, 10, 10, 10, 10, 9, 8
		},
		new short[15]
		{
			11, 11, 11, 11, 27, 29, 29, 27, 30, 30,
			11, 11, 11, 11, 10
		},
		new short[20]
		{
			11, 11, 11, 11, 27, 29, 29, 27, 30, 30,
			11, 11, 11, 11, 27, 29, 29, 27, 30, 30
		},
		new short[40]
		{
			8, 9, 10, 11, 11, 11, 11, 11, 27, 27,
			29, 29, 29, 29, 27, 30, 30, 30, 11, 11,
			11, 11, 11, 27, 27, 29, 29, 29, 29, 27,
			27, 30, 30, 30, 11, 11, 11, 11, 11, 10
		},
		new short[20]
		{
			31, 31, 31, 39, 39, 31, 31, 31, 39, 39,
			31, 31, 31, 39, 39, 31, 31, 31, 39, 39
		},
		new short[20]
		{
			31, 31, 31, 39, 39, 31, 31, 31, 39, 39,
			31, 31, 31, 39, 39, 31, 31, 31, 39, 39
		},
		new short[20]
		{
			10, 10, 10, 35, 35, 37, 37, 31, 31, 33,
			33, 33, 35, 35, 37, 37, 37, 31, 31, 37
		},
		new short[20]
		{
			35, 35, 37, 37, 31, 31, 33, 33, 35, 35,
			37, 37, 31, 31, 33, 33, 35, 35, 37, 37
		},
		new short[30]
		{
			9, 10, 10, 10, 10, 10, 38, 38, 38, 38,
			38, 38, 38, 38, 38, 38, 38, 38, 38, 38,
			38, 38, 38, 38, 38, 38, 38, 38, 38, 38
		},
		new short[30]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10
		},
		new short[34]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10
		},
		new short[34]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 23, 23, 23, 23, 23, 22,
			21, 21, 26, 25, 25, 25, 25, 25, 25, 25,
			10, 10, 10, 10
		},
		new short[30]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 23, 23, 23,
			23, 23, 23, 23, 25, 25, 25, 25, 9, 8
		},
		new short[30]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 11, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 9, 8
		},
		new short[24]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 10, 9, 8
		},
		new short[24]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 13,
			13, 13, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 10, 9, 8
		},
		new short[11]
		{
			8, 9, 21, 21, 21, 25, 25, 13, 15, 15,
			15
		},
		new short[12]
		{
			8, 9, 21, 21, 21, 25, 25, 27, 27, 27,
			28, 28
		},
		new short[14]
		{
			8, 9, 10, 10, 19, 19, 18, 18, 21, 21,
			15, 15, 10, 10
		},
		new short[16]
		{
			8, 8, 9, 10, 17, 17, 8, 8, 8, 9,
			9, 10, 10, 18, 17, 17
		},
		new short[16]
		{
			13, 13, 13, 13, 13, 13, 13, 15, 22, 22,
			26, 26, 20, 20, 16, 16
		},
		new short[16]
		{
			8, 8, 9, 9, 10, 27, 28, 28, 27, 30,
			30, 27, 29, 29, 27, 10
		},
		new short[16]
		{
			31, 31, 31, 39, 39, 31, 31, 31, 31, 39,
			39, 31, 31, 31, 31, 31
		},
		new short[16]
		{
			35, 35, 37, 31, 33, 1, 1, 35, 35, 35,
			37, 31, 33, 1, 1, 35
		},
		new short[25]
		{
			10, 10, 25, 25, 25, 8, 8, 8, 8, 8,
			8, 8, 8, 45, 43, 44, 45, 46, 47, 26,
			26, 25, 25, 25, 8
		},
		new short[24]
		{
			13, 13, 13, 13, 13, 13, 13, 14, 14, 14,
			16, 16, 14, 14, 14, 14, 14, 18, 18, 18,
			22, 22, 22, 22
		},
		new short[24]
		{
			27, 27, 29, 27, 27, 29, 13, 13, 15, 15,
			11, 11, 17, 17, 7, 7, 7, 7, 30, 30,
			27, 27, 29, 10
		},
		new short[24]
		{
			8, 8, 8, 9, 9, 9, 10, 10, 10, 10,
			39, 39, 39, 39, 8, 9, 9, 39, 39, 39,
			39, 9, 9, 8
		},
		new short[20]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 9, 8
		},
		new short[20]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 9, 8
		},
		new short[25]
		{
			8, 8, 8, 8, 8, 8, 8, 8, 8, 13,
			13, 15, 15, 8, 8, 8, 8, 8, 8, 8,
			8, 8, 8, 8, 8
		},
		new short[30]
		{
			8, 8, 9, 9, 27, 27, 27, 27, 27, 27,
			30, 30, 28, 28, 28, 28, 29, 29, 28, 28,
			30, 30, 28, 28, 29, 29, 28, 28, 30, 30
		},
		new short[30]
		{
			8, 8, 9, 9, 31, 31, 31, 31, 31, 39,
			39, 31, 31, 31, 39, 39, 31, 31, 31, 31,
			31, 31, 39, 39, 31, 31, 31, 31, 31, 31
		},
		new short[20]
		{
			8, 8, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 10, 10, 10, 9
		},
		new short[8] { 13, 13, 13, 13, 13, 15, 15, 15 },
		new short[20]
		{
			8, 9, 9, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 13, 13, 13, 15, 15
		},
		new short[20]
		{
			8, 9, 9, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10
		},
		new short[10] { 8, 9, 10, 35, 35, 35, 35, 35, 37, 37 },
		new short[10] { 8, 9, 9, 10, 10, 21, 21, 21, 15, 15 },
		new short[7] { 8, 9, 10, 23, 22, 22, 22 },
		new short[8] { 8, 9, 10, 25, 25, 19, 19, 19 },
		new short[25]
		{
			8, 9, 10, 10, 31, 31, 31, 31, 31, 33,
			31, 31, 33, 31, 31, 33, 31, 31, 33, 31,
			31, 33, 31, 31, 33
		},
		new short[23]
		{
			8, 9, 10, 35, 35, 35, 35, 35, 35, 35,
			35, 35, 35, 35, 35, 35, 35, 35, 35, 35,
			35, 35, 35
		},
		new short[14]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			23, 23, 25, 25
		},
		new short[20]
		{
			8, 9, 10, 10, 23, 23, 25, 25, 0, 0,
			1, 1, 0, 0, 1, 1, 23, 23, 25, 25
		},
		new short[13]
		{
			8, 9, 10, 10, 17, 17, 19, 19, 15, 15,
			21, 21, 10
		},
		new short[39]
		{
			37, 37, 37, 37, 37, 37, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22
		},
		new short[6] { 15, 15, 15, 17, 17, 17 },
		new short[23]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 25, 25, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21
		},
		new short[23]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 21,
			21, 21, 21, 21, 21, 21, 25, 25, 25, 25,
			10, 10, 9
		},
		new short[24]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 21, 21, 21, 21, 21, 21, 10,
			10, 10, 10, 10
		},
		new short[16]
		{
			8, 9, 30, 30, 28, 28, 29, 29, 29, 29,
			28, 28, 30, 30, 10, 8
		},
		new short[14]
		{
			8, 9, 10, 23, 23, 23, 25, 25, 25, 25,
			25, 25, 10, 10
		},
		new short[18]
		{
			8, 9, 9, 10, 10, 16, 16, 16, 16, 16,
			16, 16, 16, 16, 16, 16, 16, 10
		},
		new short[14]
		{
			8, 9, 10, 21, 21, 21, 21, 21, 26, 26,
			26, 9, 10, 10
		},
		new short[49]
		{
			8, 8, 9, 9, 10, 10, 10, 10, 10, 10,
			27, 27, 29, 29, 27, 27, 27, 27, 30, 30,
			14, 14, 14, 14, 14, 14, 14, 14, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 28, 28, 28,
			28, 14, 14, 14, 14, 14, 14, 14, 14
		},
		new short[14]
		{
			8, 8, 9, 10, 17, 17, 17, 14, 15, 15,
			27, 27, 28, 28
		},
		new short[19]
		{
			8, 8, 9, 10, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 25, 25, 25, 10, 10
		},
		new short[27]
		{
			8, 10, 36, 36, 36, 36, 36, 36, 36, 36,
			36, 36, 36, 36, 36, 36, 36, 36, 36, 36,
			36, 36, 36, 36, 36, 36, 10
		},
		new short[17]
		{
			8, 8, 9, 10, 17, 17, 8, 8, 8, 9,
			9, 10, 15, 15, 10, 17, 17
		},
		new short[27]
		{
			8, 8, 9, 10, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 22, 22, 31, 22, 22, 31,
			22, 22, 31, 22, 22, 31, 10
		},
		new short[23]
		{
			8, 8, 9, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10
		},
		new short[17]
		{
			8, 8, 9, 10, 23, 23, 22, 22, 31, 31,
			31, 31, 31, 31, 31, 31, 8
		},
		new short[19]
		{
			8, 8, 9, 10, 39, 39, 39, 39, 39, 39,
			39, 39, 23, 23, 25, 25, 25, 10, 8
		},
		new short[19]
		{
			8, 8, 9, 10, 0, 0, 0, 0, 35, 35,
			35, 49, 49, 0, 0, 0, 0, 9, 8
		},
		new short[26]
		{
			8, 9, 10, 35, 35, 35, 37, 37, 35, 35,
			37, 37, 35, 35, 37, 37, 35, 35, 37, 37,
			35, 35, 37, 37, 10, 8
		},
		new short[18]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 9
		},
		new short[19]
		{
			8, 9, 10, 10, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 26, 25, 25, 10, 8
		},
		new short[21]
		{
			8, 9, 10, 10, 14, 14, 14, 14, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 15, 15, 15,
			10
		},
		new short[19]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 21, 21, 21, 35, 35, 35, 10, 9
		},
		new short[17]
		{
			8, 9, 10, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 10, 9
		},
		new short[11]
		{
			8, 9, 10, 39, 39, 39, 39, 40, 41, 41,
			39
		},
		new short[4],
		new short[16]
		{
			8, 9, 10, 10, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25
		},
		new short[27]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 23, 23, 25, 25
		},
		new short[26]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10
		},
		new short[35]
		{
			11, 11, 11, 11, 9, 9, 9, 9, 8, 8,
			8, 8, 9, 9, 9, 9, 10, 10, 10, 10,
			10, 13, 13, 15, 15, 13, 13, 17, 17, 13,
			13, 15, 15, 13, 13
		},
		new short[26]
		{
			27, 27, 27, 27, 27, 27, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 27, 27, 27, 27
		},
		new short[18]
		{
			35, 35, 37, 31, 33, 35, 35, 37, 31, 33,
			35, 35, 37, 31, 33, 34, 34, 34
		},
		new short[30]
		{
			9, 10, 10, 10, 10, 10, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4
		},
		new short[24]
		{
			8, 9, 10, 10, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 17, 17, 21, 21, 21, 21,
			21, 17, 17, 10
		},
		new short[24]
		{
			8, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 10, 10, 10
		},
		new short[24]
		{
			8, 10, 10, 10, 10, 10, 14, 14, 21, 25,
			25, 30, 29, 28, 25, 19, 19, 17, 19, 18,
			10, 10, 10, 10
		},
		new short[48]
		{
			35, 35, 35, 35, 35, 35, 35, 35, 35, 37,
			37, 35, 35, 35, 35, 35, 35, 35, 35, 35,
			35, 35, 35, 35, 35, 35, 35, 35, 35, 37,
			37, 35, 35, 35, 35, 35, 35, 35, 35, 37,
			37, 35, 35, 35, 35, 35, 35, 35
		},
		new short[29]
		{
			35, 35, 35, 35, 35, 35, 35, 35, 35, 37,
			37, 35, 35, 35, 35, 35, 35, 35, 35, 35,
			35, 35, 35, 35, 35, 35, 35, 35, 35
		},
		new short[175]
		{
			1, 1, 1, 1, 1, 1, 8, 8, 8, 8,
			8, 8, 8, 8, 9, 9, 9, 9, 9, 9,
			9, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 61, 61, 61, 61, 61, 61, 61,
			61, 61, 61, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1
		},
		new short[15]
		{
			8, 8, 8, 9, 9, 9, 10, 10, 10, 13,
			13, 17, 18, 17, 17
		},
		new short[37]
		{
			13, 13, 13, 13, 13, 13, 13, 14, 14, 14,
			16, 16, 14, 14, 14, 14, 14, 18, 18, 18,
			22, 22, 22, 22, 26, 26, 26, 23, 23, 23,
			23, 23, 23, 25, 25, 25, 25
		},
		new short[15]
		{
			8, 8, 8, 9, 9, 9, 10, 10, 10, 13,
			13, 17, 18, 17, 17
		},
		new short[37]
		{
			13, 13, 13, 13, 13, 13, 13, 14, 14, 14,
			16, 16, 14, 14, 14, 14, 14, 18, 18, 18,
			22, 22, 22, 22, 26, 26, 26, 23, 23, 23,
			23, 23, 23, 25, 25, 25, 25
		},
		new short[12]
		{
			8, 8, 9, 10, 27, 27, 30, 30, 29, 29,
			27, 27
		}
	};

	public const sbyte NEXT_NORMAL = 0;

	public const sbyte NEXT_STOP = 1;

	public const sbyte NEXT_AGAIN = 2;

	public Plash()
	{
	}

	public Plash(MainSkill skill, MainObject obj, mVector vec)
	{
		this.skill = skill;
		vecObj = vec;
		objFire = obj;
		timeEndPlash = skill.timebuff;
		isNextf = 0;
		if (skill.timeBegin == 0L)
		{
			timebeginSkill = GameCanvas.timeNow;
		}
		else
		{
			timebeginSkill = skill.timeBegin;
		}
		if (vec != null && vec.size() > 0)
		{
			Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vecObj.elementAt(0);
			MainObject mainObject = MainObject.get_Object(object_Effect_Skill.ID, object_Effect_Skill.tem);
			if (mainObject == null)
			{
				return;
			}
			if (objFire != mainObject)
			{
				if (objFire.x < mainObject.x)
				{
					objFire.Dir = 2;
					objFire.type_left_right = 2;
				}
				else
				{
					objFire.Dir = 0;
					objFire.type_left_right = 0;
				}
			}
		}
		getTypePlash(skill.typeEffSkill);
		plashdata = getPlashData(typePlash);
		f = -1;
		removef = plashdata.mDataPlash.Length;
	}

	public Plash(MainSkill skill, MainObject obj, mVector vec, bool isShow)
	{
		this.skill = skill;
		vecObj = vec;
		objFire = obj;
		timeEndPlash = skill.timebuff;
		this.isShow = isShow;
		isNextf = 0;
		if (skill.timeBegin == 0L)
		{
			timebeginSkill = GameCanvas.timeNow;
		}
		else
		{
			timebeginSkill = skill.timeBegin;
		}
		getTypePlash(skill.typeEffSkill);
		plashdata = getPlashData(typePlash);
		f = -1;
		removef = plashdata.mDataPlash.Length;
	}

	public void endArray()
	{
	}

	public Plashdata getPlashData(short index)
	{
		Plashdata plashdata = (Plashdata)hashPlash.get(string.Empty + index);
		if (plashdata == null)
		{
			GlobalService.gI().GetTemplate(97, index);
			plashdata = new Plashdata();
		}
		return plashdata;
	}

	public short getTypePlash(short type)
	{
		switch (type)
		{
		case 2000:
			typePlash = 194;
			fAddEff = 0;
			break;
		case 1:
		case 37:
		case 112:
		case 270:
			typePlash = 0;
			fAddEff = 9;
			break;
		case 47:
		case 48:
			typePlash = 2;
			fAddEff = 6;
			break;
		case 155:
			typePlash = 3;
			fAddEff = 3;
			break;
		case 7:
			typePlash = 4;
			fAddEff = 0;
			break;
		case 64:
		case 66:
			typePlash = 5;
			fAddEff = 6;
			break;
		case 57:
		case 58:
			typePlash = 33;
			fAddEff = 6;
			break;
		case 206:
		case 207:
			fAddEff = 6;
			typePlash = 174;
			break;
		case 9:
		case 53:
		case 163:
			typePlash = 6;
			fAddEff = 6;
			break;
		case 63:
		case 190:
		case 222:
		case 312:
			typePlash = 130;
			fAddEff = 0;
			break;
		case 11:
			typePlash = 8;
			fAddEff = 0;
			break;
		case 49:
		case 50:
		case 266:
		case 276:
		case 277:
		case 278:
		case 279:
			typePlash = 9;
			fAddEff = 6;
			break;
		case 12:
		case 188:
		case 220:
		case 293:
			typePlash = 134;
			fAddEff = 6;
			break;
		case 14:
		case 44:
			typePlash = 12;
			fAddEff = 6;
			break;
		case 15:
			typePlash = 13;
			fAddEff = 2;
			break;
		case 16:
		case 51:
			typePlash = 14;
			fAddEff = 6;
			break;
		case 52:
		case 189:
		case 221:
		case 311:
			typePlash = 125;
			fAddEff = 0;
			break;
		case 19:
			typePlash = 103;
			fAddEff = 0;
			break;
		case 42:
			typePlash = 101;
			fAddEff = 0;
			break;
		case 43:
			typePlash = 102;
			fAddEff = 0;
			break;
		case 21:
		case 33:
			typePlash = 19;
			fAddEff = 4;
			break;
		case 176:
			typePlash = 159;
			fAddEff = 4;
			break;
		case 34:
			typePlash = 20;
			fAddEff = 6;
			break;
		case 35:
			typePlash = 29;
			fAddEff = 6;
			break;
		case 41:
			typePlash = 121;
			fAddEff = 4;
			break;
		case 29:
			typePlash = 27;
			fAddEff = 10;
			break;
		case 31:
		case 55:
			typePlash = 28;
			fAddEff = 4;
			break;
		case 56:
		case 191:
		case 223:
		case 313:
			typePlash = 135;
			fAddEff = 4;
			break;
		case 38:
			typePlash = 30;
			fAddEff = 3;
			break;
		case 46:
			typePlash = 31;
			fAddEff = 0;
			break;
		case 67:
		case 68:
		case 69:
		case 194:
		case 226:
		case 303:
			typePlash = 34;
			fAddEff = 10;
			break;
		case 164:
		case 227:
			typePlash = 35;
			fAddEff = 5;
			break;
		case 30:
			typePlash = 36;
			fAddEff = 4;
			break;
		case 71:
			typePlash = 37;
			fAddEff = 4;
			break;
		case 72:
			typePlash = 38;
			fAddEff = 6;
			break;
		case 92:
			typePlash = 53;
			fAddEff = 6;
			break;
		case 75:
		case 76:
			typePlash = 39;
			fAddEff = 6;
			break;
		case 73:
			typePlash = 120;
			fAddEff = 6;
			break;
		case 74:
			typePlash = 119;
			fAddEff = 6;
			break;
		case 77:
			typePlash = 40;
			fAddEff = 2;
			break;
		case 78:
			typePlash = 42;
			fAddEff = 4;
			break;
		case 79:
			typePlash = 41;
			fAddEff = 4;
			break;
		case 24:
		case 80:
			typePlash = 43;
			fAddEff = 4;
			break;
		case 156:
			typePlash = 44;
			fAddEff = 4;
			break;
		case 160:
			typePlash = 45;
			fAddEff = 4;
			break;
		case 84:
		case 181:
		case 213:
		case 272:
			typePlash = 127;
			fAddEff = 0;
			break;
		case 85:
		case 182:
			typePlash = 46;
			fAddEff = 0;
			break;
		case 157:
			typePlash = 47;
			fAddEff = 0;
			break;
		case 87:
		case 184:
		case 216:
			typePlash = 48;
			fAddEff = 0;
			break;
		case 282:
			typePlash = 190;
			fAddEff = 0;
			break;
		case 161:
			typePlash = 128;
			fAddEff = 0;
			break;
		case 88:
			typePlash = 49;
			fAddEff = 0;
			break;
		case 89:
			typePlash = 50;
			fAddEff = 0;
			break;
		case 90:
			typePlash = 51;
			fAddEff = 4;
			break;
		case 91:
			typePlash = 52;
			fAddEff = 4;
			break;
		case 93:
			typePlash = 54;
			fAddEff = 0;
			break;
		case 94:
			typePlash = 55;
			fAddEff = 6;
			break;
		case 95:
			typePlash = 56;
			fAddEff = 6;
			break;
		case 96:
			typePlash = 57;
			fAddEff = 0;
			break;
		case 97:
			typePlash = 58;
			fAddEff = 5;
			break;
		case 98:
		case 102:
			typePlash = 59;
			fAddEff = 6;
			break;
		case 99:
			typePlash = 60;
			fAddEff = 6;
			break;
		case 100:
			typePlash = 61;
			fAddEff = 6;
			break;
		case 101:
			typePlash = 62;
			fAddEff = 6;
			break;
		case 103:
			typePlash = 63;
			fAddEff = 6;
			break;
		case 104:
			typePlash = 64;
			fAddEff = 6;
			break;
		case 105:
			typePlash = 65;
			fAddEff = 6;
			break;
		case 106:
			typePlash = 66;
			fAddEff = 0;
			break;
		case 65:
		case 70:
		case 107:
			typePlash = 67;
			fAddEff = 6;
			break;
		case 108:
			typePlash = 68;
			fAddEff = 0;
			break;
		case 109:
		case 110:
			typePlash = 69;
			fAddEff = 4;
			break;
		case 111:
			typePlash = 70;
			fAddEff = 0;
			break;
		case 113:
			typePlash = 71;
			fAddEff = 0;
			break;
		case 114:
			typePlash = 72;
			fAddEff = 0;
			break;
		case 115:
			typePlash = 73;
			fAddEff = 0;
			break;
		case 116:
			typePlash = 74;
			fAddEff = 6;
			break;
		case 117:
			typePlash = 75;
			fAddEff = 4;
			break;
		case 118:
			typePlash = 76;
			fAddEff = 6;
			break;
		case 119:
			typePlash = 77;
			fAddEff = 4;
			break;
		case 120:
			typePlash = 78;
			fAddEff = 0;
			break;
		case 121:
			typePlash = 79;
			fAddEff = 4;
			break;
		case 122:
			typePlash = 80;
			fAddEff = 4;
			break;
		case 123:
		case 185:
		case 217:
		case 283:
			typePlash = 81;
			fAddEff = 0;
			break;
		case 10001:
		case 10005:
		case 10008:
		case 10010:
		case 10013:
		case 10017:
		case 10018:
		case 10020:
		case 10021:
		case 10022:
		case 10024:
		case 10026:
		case 10027:
			typePlash = 82;
			fAddEff = 0;
			break;
		case 10002:
			typePlash = 83;
			fAddEff = 0;
			break;
		case 10003:
			typePlash = 84;
			fAddEff = 0;
			break;
		case 10004:
		case 10007:
		case 10009:
			typePlash = 85;
			fAddEff = 0;
			break;
		case 10006:
		case 10011:
		case 10012:
		case 10015:
		case 10023:
			typePlash = 86;
			fAddEff = 0;
			break;
		case 10019:
			typePlash = 87;
			fAddEff = 0;
			break;
		case 158:
			typePlash = 88;
			fAddEff = 0;
			break;
		case 177:
			typePlash = 158;
			fAddEff = 0;
			break;
		case 162:
			typePlash = 89;
			fAddEff = 0;
			break;
		case 125:
		case 187:
			typePlash = 129;
			fAddEff = 0;
			break;
		case 159:
			typePlash = 90;
			fAddEff = 6;
			break;
		case 126:
		case 192:
			typePlash = 126;
			fAddEff = 0;
			break;
		case 127:
		case 193:
		case 225:
		case 302:
			typePlash = 91;
			fAddEff = 6;
			break;
		case 10025:
			typePlash = 92;
			fAddEff = 0;
			break;
		case 128:
			typePlash = 93;
			fAddEff = 7;
			break;
		case 129:
			typePlash = 94;
			fAddEff = 7;
			break;
		case 130:
			typePlash = 95;
			fAddEff = 7;
			break;
		case 131:
			typePlash = 96;
			fAddEff = 6;
			break;
		case 132:
			typePlash = 97;
			fAddEff = 6;
			break;
		case 133:
			typePlash = 98;
			fAddEff = 0;
			break;
		case 134:
		case 135:
			typePlash = 99;
			fAddEff = 0;
			break;
		case 136:
			typePlash = 104;
			fAddEff = 0;
			break;
		case 137:
		case 138:
			typePlash = 105;
			fAddEff = 0;
			break;
		case 139:
			typePlash = 107;
			fAddEff = 0;
			break;
		case 140:
			typePlash = 108;
			fAddEff = 0;
			break;
		case 141:
			typePlash = 109;
			fAddEff = 0;
			break;
		case 142:
			typePlash = 110;
			fAddEff = 0;
			break;
		case 2:
			typePlash = 111;
			fAddEff = 0;
			break;
		case 228:
		case 259:
		case 260:
		case 261:
			typePlash = 182;
			fAddEff = 0;
			break;
		case 3:
		case 229:
		case 262:
		case 263:
		case 264:
			typePlash = 112;
			fAddEff = 0;
			break;
		case 4:
		case 230:
		case 246:
		case 253:
			typePlash = 113;
			fAddEff = 4;
			break;
		case 5:
		case 231:
			typePlash = 114;
			fAddEff = 4;
			break;
		case 6:
		case 232:
			typePlash = 115;
			fAddEff = 0;
			break;
		case 10:
		case 234:
			typePlash = 116;
			fAddEff = 0;
			break;
		case 18:
			typePlash = 118;
			fAddEff = 4;
			break;
		case 83:
			typePlash = 122;
			fAddEff = 0;
			break;
		case 86:
		case 183:
		case 215:
		case 281:
			typePlash = 123;
			fAddEff = 0;
			break;
		case 124:
		case 186:
		case 218:
		case 291:
			typePlash = 124;
			fAddEff = 0;
			break;
		case 280:
			typePlash = 191;
			break;
		case 165:
			typePlash = 131;
			fAddEff = 0;
			break;
		case 166:
			typePlash = 132;
			fAddEff = 0;
			break;
		case 17:
			typePlash = 133;
			fAddEff = 1000;
			isNextf = 2;
			break;
		case 20:
			typePlash = 136;
			fAddEff = 0;
			break;
		case 23:
			typePlash = 137;
			fAddEff = 5;
			break;
		case 25:
		case 235:
			typePlash = 138;
			fAddEff = 5;
			break;
		case 26:
		case 236:
			typePlash = 139;
			fAddEff = 0;
			break;
		case 27:
			typePlash = 140;
			fAddEff = 8;
			break;
		case 28:
			typePlash = 141;
			fAddEff = 8;
			break;
		case 32:
			typePlash = 142;
			fAddEff = 4;
			break;
		case 36:
			typePlash = 143;
			fAddEff = 3;
			break;
		case 39:
			typePlash = 144;
			fAddEff = 0;
			break;
		case 40:
			typePlash = 145;
			fAddEff = 0;
			break;
		case 45:
			typePlash = 146;
			fAddEff = 0;
			break;
		case 54:
			typePlash = 147;
			fAddEff = 5;
			break;
		case 59:
		case 60:
			typePlash = 148;
			fAddEff = 4;
			break;
		case 167:
			typePlash = 149;
			fAddEff = 6;
			break;
		case 143:
			typePlash = 150;
			fAddEff = 0;
			break;
		case 169:
		case 237:
			typePlash = 151;
			fAddEff = 2;
			break;
		case 400:
		case 401:
		case 402:
		case 403:
			typePlash = 151;
			fAddEff = 0;
			break;
		case 170:
		case 238:
			typePlash = 152;
			fAddEff = 4;
			break;
		case 171:
		case 172:
		case 239:
		case 240:
			typePlash = 153;
			fAddEff = 6;
			break;
		case 168:
		case 258:
			typePlash = 154;
			fAddEff = 2;
			break;
		case 173:
			typePlash = 155;
			fAddEff = 6;
			break;
		case 174:
			typePlash = 156;
			fAddEff = 2;
			break;
		case 175:
			typePlash = 157;
			fAddEff = 6;
			break;
		case 178:
			typePlash = 160;
			fAddEff = 4;
			break;
		case 179:
		case 241:
			typePlash = 161;
			fAddEff = 1;
			break;
		case 180:
		case 212:
			typePlash = 162;
			fAddEff = 0;
			break;
		case 271:
			typePlash = 189;
			fAddEff = 0;
			break;
		case 195:
			typePlash = 163;
			fAddEff = 4;
			break;
		case 196:
			typePlash = 164;
			fAddEff = 0;
			break;
		case 197:
		case 267:
		case 274:
		case 275:
			typePlash = 165;
			fAddEff = 8;
			break;
		case 198:
			typePlash = 166;
			fAddEff = 4;
			break;
		case 199:
			typePlash = 167;
			fAddEff = 4;
			break;
		case 200:
			typePlash = 168;
			fAddEff = 4;
			break;
		case 201:
			typePlash = 169;
			fAddEff = 4;
			break;
		case 202:
			typePlash = 170;
			fAddEff = 4;
			break;
		case 203:
			typePlash = 171;
			fAddEff = 4;
			break;
		case 204:
			typePlash = 172;
			fAddEff = 4;
			break;
		case 205:
			typePlash = 173;
			fAddEff = 3;
			break;
		case 208:
			typePlash = 175;
			fAddEff = 0;
			break;
		case 209:
		case 242:
			typePlash = 176;
			fAddEff = 6;
			break;
		case 210:
		case 243:
			typePlash = 177;
			fAddEff = 4;
			break;
		case 211:
		case 244:
			typePlash = 177;
			fAddEff = 4;
			break;
		case 214:
		case 273:
			typePlash = 179;
			fAddEff = 0;
			break;
		case 219:
		case 292:
			typePlash = 180;
			fAddEff = 0;
			break;
		case 224:
		case 301:
			typePlash = 181;
			fAddEff = 0;
			break;
		case 247:
		case 254:
			typePlash = 183;
			fAddEff = 4;
			break;
		case 248:
		case 255:
			typePlash = 184;
			fAddEff = 4;
			break;
		case 245:
		case 249:
		case 251:
		case 252:
			typePlash = 185;
			fAddEff = 0;
			break;
		case 256:
			typePlash = 186;
			fAddEff = 0;
			break;
		case 257:
			typePlash = 187;
			fAddEff = 0;
			break;
		case 265:
			typePlash = 188;
			fAddEff = 0;
			break;
		}
		return typePlash;
	}

	public int update()
	{
		try
		{
			if (GameCanvas.gameTick % 25 == 0 && (GameCanvas.timeNow - timebeginSkill) / 1000 > 30)
			{
				return -1;
			}
			if (isNextf == 0)
			{
				f++;
			}
			else
			{
				if (isNextf == 1)
				{
					return plashdata.mDataPlash[f];
				}
				if (isNextf == 2)
				{
					f++;
					if (f >= plashdata.mDataPlash.Length)
					{
						f = 0;
					}
					if ((GameCanvas.timeNow - timebeginSkill) / 1000 > timeEndPlash)
					{
						return -1;
					}
				}
			}
			if (f >= removef)
			{
				if (skill.typeBuff == 2 && GameCanvas.timeNow - timebeginSkill < timeEndPlash)
				{
					return plashdata.mDataPlash[removef - 1];
				}
				return -1;
			}
			if (isShow)
			{
				if (f == fAddEff)
				{
					LoginScreen.addEffectEnd(skill.typeEffSkill, 0, objFire.x, objFire.y, (sbyte)objFire.Dir, objFire);
					fAddEff = 10000;
				}
			}
			else if (f == fAddEff)
			{
				if (skill.typeBuff == 2)
				{
					GameScreen.addEffectSkillSpec(skill, objFire);
				}
				else if (skill.typeBuff == 1)
				{
					GameScreen.addEffectSkill(skill, objFire, vecObj);
				}
				else
				{
					GameScreen.addEffectSkill(skill, objFire, vecObj);
				}
				fAddEff = 10000;
			}
			return plashdata.mDataPlash[f];
		}
		catch (Exception)
		{
		}
		return 0;
	}

	public void setIsNextf(sbyte isnext)
	{
		isNextf = isnext;
	}

	public void setf(int f)
	{
		this.f = f;
	}

	public static void readDataPlash(DataInputStream iss)
	{
		try
		{
			short num = iss.readShort();
			sbyte b = iss.readByte();
			Plashdata plashdata = new Plashdata();
			plashdata.mDataPlash = new short[b];
			for (int i = 0; i < b; i++)
			{
				plashdata.mDataPlash[i] = iss.readByte();
			}
			hashPlash.put(string.Empty + num, plashdata);
		}
		catch (Exception)
		{
		}
	}
}
