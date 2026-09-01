using System;
using System.Collections;
using UnityEngine;

public class ReadMessenge : AvMain
{
	public static iCommand cmdOffInterface;

	public static iCommand cmdOffChat;

	public static iCommand cmdOffName;

	public static iCommand cmdOffAll;

	public static iCommand cmdShowPos;

	public static iCommand cmdShowSysout;

	public static iCommand cmdShowIpLocal;

	public static iCommand cmdShowInTabAdmin;

	public static iCommand cmdSetSkillEff15;

	public static iCommand cmdSetSkillEff20;

	private short[][] hardcodeSkill5 = new short[5][]
	{
		new short[3] { 33, 35, 37 },
		new short[3] { 15, 29, 122 },
		new short[3] { 44, 48, 50 },
		new short[3] { 51, 53, 55 },
		new short[3] { 58, 66, 68 }
	};

	private short[][] hardcodeSkill10 = new short[5][]
	{
		new short[3] { 83, 84, 85 },
		new short[3] { 86, 87, 123 },
		new short[3] { 124, 125, 12 },
		new short[3] { 52, 63, 56 },
		new short[3] { 126, 127, 69 }
	};

	private short[][] hardcodeSkill15 = new short[5][]
	{
		new short[3] { 180, 181, 182 },
		new short[3] { 183, 184, 185 },
		new short[3] { 186, 187, 188 },
		new short[3] { 189, 190, 191 },
		new short[3] { 192, 193, 194 }
	};

	private short[][] hardcodeSkill20 = new short[5][]
	{
		new short[3] { 212, 213, 214 },
		new short[3] { 215, 216, 217 },
		new short[3] { 218, 219, 220 },
		new short[3] { 221, 222, 223 },
		new short[3] { 224, 225, 226 }
	};

	public Message msgLuu;

	public short idMapLuu;

	public static sbyte actionChangeMap;

	public static bool isNondata;

	public const sbyte REMOVE_TELEPORT = 1;

	public const sbyte REMOVE_LINK_MAP = 2;

	public const sbyte REMOVE_MAP_SEA = 3;

	public static short indexHotKeySkill;

	public static short IdDialog = -1;

	public const sbyte DIALOG_CLOSE = 0;

	public const sbyte DIALOG_DOWNLOAD = 1;

	public const sbyte DIALOG_NORMAL = 2;

	public const sbyte DIALOG_TIME = 3;

	public const sbyte DIALOG_RUBY = 4;

	public const sbyte UPDATE_ALL = 0;

	public const sbyte ADD_REPLACE = 1;

	public const sbyte DELETE = 2;

	public const sbyte UPDATE_MONEY = 3;

	public const sbyte SIZE_CHEST = 6;

	public const sbyte SHOP_MATERIAL = 6;

	public const sbyte INVEN = 100;

	public const sbyte CHEST = 99;

	public const sbyte SHIP = 101;

	public const sbyte BOAT = 102;

	public const sbyte HAIR = 103;

	public const sbyte FASHION = 105;

	public const sbyte INVEN_CLAN = 110;

	public const sbyte SHOP_DA_KHAM = 111;

	public const sbyte HEAD = 112;

	public const sbyte FASHION_EVENT = 113;

	public const sbyte FASHION_GIFT_EVENT = 114;

	public const sbyte SHOP_EVENT = 116;

	public const sbyte DIEM_TICH_LUY = 118;

	private int lechYNum;

	public const sbyte EFF_LV_UP = 0;

	public const sbyte EFF_HOA_THU = 1;

	public const sbyte EFF_GET_MONEY_VUON_CAM = 2;

	public const sbyte EFF_SET_NAMI = 3;

	public const sbyte EFF_BOOM_USSOP = 4;

	public const sbyte EFF_PHAN_DAM_ZORO = 5;

	public const sbyte EFF_CHOPPER_BUFF = 6;

	public const sbyte EFF_LITTLE_BUFF_HP = 7;

	public const sbyte EFF_LITTLE_BUFF_HP_BOSS = 8;

	public const sbyte EFF_LITTLE_DAM_BOSS = 9;

	public const sbyte EFF_WAPOL = 10;

	public const sbyte EFF_BI_BE = 11;

	public const sbyte EFF_NEM_POKE_OK = 12;

	public const sbyte EFF_NEM_POKE_FAIL = 13;

	public const sbyte EFF_LOL_TRU_TREN = 14;

	public const sbyte EFF_LOL_TRU_DUOI = 15;

	public const sbyte EFF_CHOANG = 16;

	public const sbyte EFF_KIET_SUC = 17;

	public const sbyte EFF_BAT_TU = 18;

	public const sbyte EFF_KHANG_TAT_CA = 19;

	public const sbyte EFF_THA_DEN = 20;

	public const sbyte EFF_TU_CHOI_TU_THAN = 21;

	public const sbyte EFF_LIGHTING_RED = 22;

	public const sbyte EFF_THA_PHAO_HOA = 23;

	public const sbyte EFF_LAW_HEART = 24;

	public const sbyte EFF_GOAL = 25;

	public const sbyte EFF_DECREASE_POWER = 26;

	public const sbyte ACTION_LIST = 0;

	private const sbyte ACTION_INVEN = 1;

	private const sbyte ACTION_USE = 2;

	private const sbyte ACTION_MO_HH = 3;

	public const sbyte TYPE_MO_HH_MENU = 0;

	private const sbyte TYPE_MO_HH_OPEN = 1;

	public const sbyte MENU_NORMAL = 0;

	public const sbyte MENU_IMAGE = 1;

	public const sbyte MENU_NPC = 2;

	public const sbyte MENU_ITEM = 3;

	public const sbyte MENU_SKILL = 4;

	public const sbyte MENU_IMAGE_NEW = 5;

	public const sbyte MENU_IMAGE_COLOR = 6;

	public const sbyte STATUS_QUEST_MAP = 1;

	public const sbyte QUEST_ALL = 0;

	public const sbyte QUEST_ADD = 1;

	public const sbyte QUEST_DEL = 2;

	public const sbyte QUEST_FINISH = 3;

	public const sbyte QUEST_ACTION_FINISH = 4;

	public const sbyte QUEST_ACTION_SHOW_DIALOG = 5;

	public const sbyte PARTY_INVITE = 0;

	public const sbyte PARTY_ADD = 1;

	public const sbyte PARTY_DEL = 2;

	public const sbyte PARTY_CANCEL = 3;

	public const sbyte PARTY_APPCEPT = 4;

	public const sbyte PARTY_CREATE = 5;

	public const sbyte PARTY_APPCEPT_ADD = 6;

	public const sbyte PARTY_XIN_VAO = 7;

	public const sbyte SKILL_LEARN = 0;

	public const sbyte SKILL_ADD = 1;

	public const sbyte SKILL_UPDATE_XP = 2;

	public const sbyte SKILL_DEL = 3;

	public const sbyte UPGRADE_STEP_LOI = 0;

	public const sbyte UPGRADE_STEP_HOI = 1;

	public const sbyte UPGRADE_STEP_THANH_CONG = 2;

	public const sbyte UPGRADE_STEP_THAT_BAI = 3;

	public const sbyte UPGRADE_STEP_ITEM = 4;

	public const sbyte UPGRADE_STEP_LUCKY = 5;

	public const sbyte UPGRADE_STEP_BAOHIEM = 6;

	public const sbyte UPGRADE_STEP_OPEN_UPGRADE = 7;

	public const sbyte UPGRADE_STEP_OPEN_SHOP_MATERIAL = 8;

	public const sbyte UPGRADE_STEP_OPEN_KHAM = 9;

	public const sbyte UPGRADE_STEP_OPEN_DUT = 10;

	public const sbyte UPGRADE_STEP_OPEN_SHOP_DA = 11;

	public const sbyte UPGRADE_STEP_OPEN_GHEP_DA = 12;

	public const sbyte UPGRADE_STEP_OPEN_TACH_DA = 13;

	public const sbyte UPGRADE_STEP_BAO_HIEM_2 = 14;

	public const sbyte UPGRADE_LAW_HEART = 15;

	public const sbyte UPGRADE_LAW_HEART_THANH_CONG = 16;

	public const sbyte UPGRADE_LAW_HEART_THAT_BAI = 17;

	public static short idItemUpgrade;

	public const sbyte ACTION_OPEN_MENU = 0;

	public const sbyte ACTION_SET_ITEM = 1;

	public const sbyte ACTION_CUONG_HOA = 2;

	public const sbyte ACTION_SUCC = 3;

	public const sbyte ACTION_HOI_NANG_CAP = 4;

	public const sbyte ACTION_UPDATE_LIST = 5;

	public const sbyte ACTION_HE_SO_AN = 6;

	public const sbyte SPLIT_TYPE_SPLIT = 0;

	public const sbyte SPLIT_TYPE_JOIN = 1;

	public const sbyte SPLIT_ACTION_BO_VAO = 0;

	public const sbyte SPLIT_ACTION_XONG = 1;

	public const sbyte SPLIT_ACTION_LOI = 2;

	public const sbyte SPLIT_ACTION_OPEN_SPLIT = 3;

	public const sbyte SPLIT_ACTION_OPEN_MENU_SPLIT = 4;

	public const sbyte TRADE_OPEN = 0;

	public const sbyte TRADE_SET_ITEM = 1;

	public const sbyte TRADE_CHAT = 2;

	public const sbyte TRADE_LOCK = 3;

	public const sbyte TRADE_OK = 4;

	public const sbyte TRADE_CANCEL = 5;

	public const sbyte TRADE_INVITE = 6;

	public const sbyte TRADE_ACTION_DEL = 0;

	public const sbyte TRADE_ACTION_SET = 1;

	public static int yBoatMap;

	public static sbyte[] mtest;

	public const sbyte TICKET_BANHMI = 0;

	public const sbyte TICKET_BOSS = 1;

	public const sbyte TICKET_PVP = 2;

	public const sbyte TICKET_X2XP = 3;

	public const sbyte RE_ITEM_OPEN = 0;

	public const sbyte RE_ITEM_SET = 1;

	public const sbyte RE_ITEM_REMOVE = 2;

	public const sbyte RE_ITEM_ERROR = 3;

	public const sbyte RE_ITEM_OK_KHAM = 4;

	public const sbyte RE_ITEM_OK_UPGRADE_DA = 5;

	public const sbyte RE_ITEM_OK_TACH_DA = 6;

	public const sbyte RE_ITEM_OK_DUC_LO = 7;

	public const sbyte RE_ITEM_OK_HOAN_MY = 20;

	public const sbyte RE_ITEM_FAIL_HOAN_MY = 21;

	public const sbyte RE_ITEM_OK_KICH_AN = 22;

	public const sbyte RE_ITEM_FAIL_KICH_AN = 23;

	public const sbyte RE_CHE_TAC_CONG_DAP = 24;

	public const sbyte RE_CHE_TAC_CONG_OK = 25;

	public const sbyte RE_DA_SIEU_CAP_DAP = 26;

	public const sbyte RE_DA_SIEU_CAP_OK = 27;

	public const sbyte RE_DA_SIEU_SET_DA_SC = 28;

	public const sbyte RE_DA_SIEU_SET_DA_NL = 29;

	public const sbyte RE_DA_SIEU_CAP_FAIL = 30;

	public const sbyte RE_GHEP_DO_OK = 31;

	public const sbyte RE_GHEP_DO_FAIL = 32;

	public const sbyte RE_ITEM_OK_DUC_BANG_BUA_THUONG = 33;

	public const sbyte RE_ITEM_OK_DUC_BANG_BUA_VIP = 34;

	public const sbyte RE_DEVIL_SKILL_OPEN = 8;

	public const sbyte RE_DEVIL_SKILL_SET = 9;

	public const sbyte RE_DEVIL_SKILL_REMOVE = 10;

	public const sbyte RE_DEVIL_SKILL_ERROR = 11;

	public const sbyte RE_DEVIL_SKILL_OK_KHAM = 12;

	public const sbyte RE_DEVIL_ITEM_OPEN = 13;

	public const sbyte RE_DEVIL_ITEM_SET = 14;

	public const sbyte RE_DEVIL_ITEM_REMOVE = 15;

	public const sbyte RE_DEVIL_ITEM_ERROR = 16;

	public const sbyte RE_DEVIL_ITEM_OK_KHAM = 17;

	public const sbyte RE_DEVIL_SKILL_TI_LE = 18;

	public const sbyte RE_DEVIL_ITEM_TI_LE = 19;

	public const sbyte RE_GHEP_NHIEU_NGLIEU_THANH_1_OPEN = 20;

	public const sbyte RE_GHEP_NHIEU_NGLIEU_THANH_1_OK = 21;

	public const sbyte AU_REVICE = 1;

	public const sbyte CHECK_LIST_PHO_BANG = 0;

	public const sbyte CHECK_MEM_READY = 1;

	public const sbyte CHECK_MEM_CANCLE = 2;

	public const sbyte CHECK_ONE_MEM_CANCLE = 3;

	public static short IDDialogPhoBang = -1;

	public const sbyte CAPCHAR_RED_LINE = 0;

	public const sbyte CAPCHAR_RED_LINE_FAIL = 1;

	public const sbyte CAPCHAR_RED_LINE_FINISH = 2;

	public const sbyte CAPCHAR_GOTO_SKY = 3;

	public const sbyte CAPCHAR_GOTO_SKY_FAIL = 4;

	public const sbyte CAPCHAR_GOTO_SKY_FINISH = 5;

	public static mImage[] mImgCapredLine;

	public const sbyte TIME_PVP_PHO_BANG = 0;

	public const sbyte TIME_HP_PHO_BANG = 1;

	public const sbyte TIME_PHO_BANG_LITTLE = 2;

	public const sbyte TIME_PHO_BANG_LITTLE_REVICE = 3;

	public const sbyte TIME_SCORE = 4;

	public const sbyte TIME_SCORE_NEW = 5;

	public const sbyte CLAN_CREATE_DATA = 0;

	public const sbyte CLAN_UPDATE_CHUC_VU = 1;

	public const sbyte CLAN_UPDATE_DATA = 2;

	public const sbyte CLAN_UPDATE_LIST_MEM = 3;

	public const sbyte CLAN_ATTRIBUTE = 4;

	public const sbyte CLAN_INFO_PLAYER = 5;

	public const sbyte CLAN_CHAT_IN_CLAN = 8;

	public const sbyte CLAN_MOI_VAO_CLAN = 7;

	public const sbyte CLAN_ALL_CHAT_IN_CLAN = 9;

	public const sbyte CLAN_REMOVE_CHAT = 11;

	public const sbyte CLAN_LEAVE_CLAN = 10;

	public const sbyte CLAN_ADD_REMOVE_LIST_MEM = 12;

	public const sbyte CLAN_UPDATE_TIME_DONATE = 13;

	public const sbyte CLAN_UPDATE_STATUS = 14;

	public const sbyte CLAN_UPDATE_ATTRIBUTE_PLUS_PLAYER = 15;

	public const sbyte CLAN_UPDATE_XP = 16;

	public const sbyte CLAN_UPDATE_MONEY = 17;

	public const sbyte CLAN_UPDATE_INFO_MEM_CLAN = 18;

	public const sbyte CLAN_UPDATE_INVEN_CLAN = 19;

	public const sbyte CLAN_THONGBAO_CUP = 20;

	public const sbyte CLAN_CLOSE_CREATE_CLAN = 21;

	public const sbyte UPDATECHAT_ALL = 0;

	public const sbyte UPDATECHAT_ADD = 1;

	public const sbyte CHUYENHOA_OPEN = 0;

	public const sbyte CHUYENHOA_IMPORT_ITEM = 1;

	public const sbyte CHUYENHOA_END = 3;

	public const sbyte CHUYENHOA_UPDATE_PRICE = 2;

	public static short idLvthap = -1;

	public static short idLvCao = -1;

	public static sbyte numCuonghoa;

	public const sbyte LITTLE_CREATE_BOSS = 0;

	public const sbyte LITTLE_UPDATE_MP_HP = 1;

	public const sbyte LITTLE_BOSS_FIGHT = 2;

	public const sbyte LITTLE_BOSS_DIE = 3;

	public const sbyte PET_CREATE = 0;

	public const sbyte PET_REMOVE = 1;

	public const sbyte PET_USE_SKILL = 2;

	public const sbyte PET_INVEN = 3;

	public const sbyte PET_USE = 4;

	public const sbyte PVP_READY = 0;

	public const sbyte PVP_3_SECOND = 1;

	public const sbyte PVP_START = 2;

	public const sbyte PVP_WIN = 3;

	public const sbyte PVP_LOSE = 4;

	public static int timeLoadItemMap = -1;

	public const sbyte MAX_UPDATE_ALL = 0;

	public const sbyte MAX_UPDATE_ONE = 1;

	public const sbyte MAX_OPEN = 2;

	public const sbyte WANTED_OPEN = 0;

	public const sbyte WANTED_FIND = 1;

	public const sbyte WANTED_READY = 2;

	public const sbyte WANTED_CANCLE = 3;

	public const sbyte WANTED_UPDATE_POINT = 4;

	public const sbyte WANTED_UPDATE_TIME_NEXT = 5;

	public const sbyte CHEST_W_UPDATE = 0;

	public const sbyte CHEST_W_DEL = 1;

	public const sbyte LOL_CHAT = 2;

	public const sbyte LOL_UPDATE_TYPE_OBJ = 0;

	public const sbyte LOL_TYPE_PK = 1;

	public const sbyte LOL_UPDATE_TRU_KD = 3;

	public const sbyte LOL_LIST_BUFF = 4;

	public const sbyte EFF_MP_HP_NOR = 0;

	public const sbyte EFF_MP_HP_PHAN_DAM = 1;

	public const sbyte EFF_KICK_BAT_TU = 0;

	public const sbyte EFF_KICK_LOI_CAM_ON = 1;

	public const sbyte EFF_KICK_LA_CHAN = 2;

	public const sbyte EFF_KICK_KHOA_NANG_LUONG = 3;

	public const sbyte EFF_KICK_BOC_PHA = 4;

	public const sbyte EFF_KICK_TAP_TRUNG_CAO_DO = 5;

	public const sbyte EFF_KICK_MA_CA_RONG = 6;

	public const sbyte EFF_KICK_RESET_COUNTDOWN_KICK = 8;

	public const sbyte EFF_KICK_RESET_COUNTDOWN_AVA = 9;

	public const sbyte EFF_KICK_GIAI_PHONG_NANG_LUONG = 10;

	public const sbyte EFF_KICK_NGUOI_BAT_TU = 11;

	public static sbyte ID_GiaoTiep_Server = -1;

	public static string Str_GiaoTiep_Server = string.Empty;

	public const sbyte EVENT_TYPE_TAI_XIU = 0;

	public const sbyte EVENT_ACTION_OPEN = 0;

	public const sbyte EVENT_ACTION_TAI_XIU_DAT_CUOC = 1;

	public const sbyte EVENT_ACTION_TAI_XIU_KET_QUA = 2;

	public const sbyte EVENT_ACTION_TAI_XIU_UPDATE = 3;

	public const sbyte EVENT_ACTION_TAI_XIU_LICH_SU_VAN = 4;

	public const sbyte EVENT_TYPE_NAU_BANH_CHUNG_AUDITION = 1;

	public const sbyte EVENT_ACTION_NAU_BANH_CHUNG_AUDITION_NAU = 0;

	public const sbyte EVENT_ACTION_NAU_BANH_CHUNG_AUDITION_VOT_BANH = 1;

	private const sbyte ACTION_OPEN = 0;

	private const sbyte ACTION_SEND_INFO_EQUI = 1;

	private const sbyte ACTION_KHAM_DA = 2;

	private const sbyte ACTION_TACH_DA = 3;

	private const sbyte ACTION_HANH_TRINH = 4;

	public const sbyte TYPE_OPEN = 0;

	public const sbyte TYPE_SEND_INFO = 1;

	public const sbyte TYPE_SEND_QUA = 2;

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (IdDialog == 33 && ScreenUpgrade.isClickUpgradeSkillDevilAuto && (subIndex == 0 || subIndex == 1))
			{
				ScreenUpgrade.choiceDevilBeriOrRuby = (sbyte)subIndex;
			}
			GlobalService.gI().Choice_Dialog_server(IdDialog, (sbyte)subIndex);
			GameCanvas.end_Dialog();
			break;
		case 1:
			if (subIndex != 0)
			{
				if (ScreenUpgrade.isClickUpgradeItemToSucess)
				{
					ScreenUpgrade.beriOrRuby = (sbyte)subIndex;
				}
				GlobalService.gI().Upgrade_Item(2, idItemUpgrade, (sbyte)subIndex);
			}
			GameCanvas.end_Dialog();
			break;
		case 3:
		{
			MainObject.StepHelp++;
			if (MainObject.StepHelp >= MainObject.strHelp.Length - 1)
			{
				GameCanvas.menu.startAt_NPC(null, MainObject.strHelp[MainObject.strHelp.Length - 1], subIndex, 2, isHelp: true, 2, isDynamic: false);
				break;
			}
			mVector mVector6 = new mVector();
			iCommand o5 = new iCommand(T.next, 3, subIndex, this);
			mVector6.addElement(o5);
			GameCanvas.menu.startAt_NPC(mVector6, MainObject.strHelp[MainObject.StepHelp], subIndex, 2, isHelp: true, 2, isDynamic: false);
			break;
		}
		case 4:
			GlobalService.gI().ChuyenHoa(3, idLvthap, idLvCao);
			GameCanvas.end_Dialog();
			break;
		case 5:
			switch (subIndex)
			{
			case 0:
				GameScreen.isPaintInterface = !GameScreen.isPaintInterface;
				break;
			case 1:
				GameScreen.isShowChat = !GameScreen.isShowChat;
				break;
			case 2:
				GameScreen.isShowName = !GameScreen.isShowName;
				break;
			case 3:
				GameScreen.isOffAll = !GameScreen.isOffAll;
				break;
			case 4:
				GameScreen.isShowIndex = !GameScreen.isShowIndex;
				break;
			case 5:
				GameScreen.isShowText = !GameScreen.isShowText;
				break;
			case 6:
				GameScreen.isIP_Local = !GameScreen.isIP_Local;
				break;
			case 7:
				GameScreen.isShowTextTab = !GameScreen.isShowTextTab;
				break;
			}
			break;
		case 6:
		{
			sbyte indexMarket = -1;
			if (GameCanvas.currentScreen == GameCanvas.tabMarketScr)
			{
				indexMarket = (sbyte)GameCanvas.tabMarketScr.idSelect;
			}
			GlobalService.gI().Market(1, indexMarket, (short)subIndex, 0, 1);
			break;
		}
		case 7:
		{
			mVector mVector5 = new mVector();
			for (int l = 0; l < 5; l++)
			{
				iCommand o4 = new iCommand(T.mClazz[l + 1], 8, l, this);
				mVector5.addElement(o4);
			}
			GameCanvas.menu.startAt(mVector5, 2, "Select Class");
			break;
		}
		case 9:
		{
			mVector mVector3 = new mVector();
			for (int j = 0; j < 5; j++)
			{
				iCommand o2 = new iCommand(T.mClazz[j + 1], 10, j, this);
				mVector3.addElement(o2);
			}
			GameCanvas.menu.startAt(mVector3, 2, "Select Class");
			break;
		}
		case 8:
			setEffSkill15(subIndex);
			break;
		case 10:
			setEffSkill20(subIndex);
			break;
		case 11:
			GlobalService.gI().Clan_Fight(5, (short)subIndex, 0);
			break;
		case 12:
			if (subIndex != 0)
			{
				if (ScreenUpgrade.isClickUpgradeSuperItemToSucess)
				{
					ScreenUpgrade.beriOrRuby = (sbyte)subIndex;
				}
				GlobalService.gI().Upgrade_Super_Item(2, idItemUpgrade, (sbyte)subIndex, 0);
			}
			GameCanvas.end_Dialog();
			break;
		case 13:
			GlobalService.gI().Upgrade_Super_Item(2, idItemUpgrade, 0, 0);
			GameCanvas.end_Dialog();
			break;
		case 14:
		{
			mVector mVector4 = new mVector();
			for (int k = 0; k < 5; k++)
			{
				iCommand o3 = new iCommand(T.mClazz[k + 1], 16, k, this);
				mVector4.addElement(o3);
			}
			GameCanvas.menu.startAt(mVector4, 2, "Select Class");
			break;
		}
		case 15:
		{
			mVector mVector2 = new mVector();
			for (int i = 0; i < 5; i++)
			{
				iCommand o = new iCommand(T.mClazz[i + 1], 17, i, this);
				mVector2.addElement(o);
			}
			GameCanvas.menu.startAt(mVector2, 2, "Select Class");
			break;
		}
		case 16:
			setEffSkill5(subIndex);
			break;
		case 17:
			setEffSkill10(subIndex);
			break;
		case 18:
			GlobalService.gI().Upgrade_Dial(2, idItemUpgrade, 0, 0);
			GameCanvas.end_Dialog();
			break;
		case 2:
			break;
		}
	}

	private void setEffSkill5(int subIndex)
	{
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			((Skill_Info)Player.vecListSkill.elementAt(i)).typeEffSkill = hardcodeSkill5[subIndex][i];
			if (i == 2)
			{
				break;
			}
		}
	}

	private void setEffSkill10(int subIndex)
	{
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			((Skill_Info)Player.vecListSkill.elementAt(i)).typeEffSkill = hardcodeSkill10[subIndex][i];
			if (i == 2)
			{
				break;
			}
		}
	}

	private void setEffSkill15(int subIndex)
	{
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			((Skill_Info)Player.vecListSkill.elementAt(i)).typeEffSkill = hardcodeSkill15[subIndex][i];
			if (i == 2)
			{
				break;
			}
		}
	}

	private void setEffSkill20(int subIndex)
	{
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			((Skill_Info)Player.vecListSkill.elementAt(i)).typeEffSkill = hardcodeSkill20[subIndex][i];
			if (i == 2)
			{
				break;
			}
		}
	}

	public void ListChar(Message msg)
	{
		try
		{
			GameCanvas.saveRms.saveUserPass(GameCanvas.loginScr.tfUser.getText(), GameCanvas.loginScr.tfPass.getText());
			ListChar_Screen.vecListChar.removeAllElements();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				short iD = msg.reader().readShort();
				string name = msg.reader().readUTF();
				Other_Player other_Player = new Other_Player(iD, 0, name, 0, 0);
				other_Player.clazz = msg.reader().readByte();
				other_Player.Lv = msg.reader().readShort();
				other_Player.sethead(msg.reader().readShort());
				other_Player.sethair(msg.reader().readShort());
				short num = msg.reader().readShort();
				if (num >= 0)
				{
					other_Player.clan = new MainClan();
					other_Player.clan.idIcon = num;
					other_Player.clan.chucvu = 10;
				}
				sbyte b2 = msg.reader().readByte();
				short[] array = new short[b2];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = -1;
				}
				for (int k = 0; k < b2; k++)
				{
					if (msg.reader().readByte() == 1)
					{
						array[k] = msg.reader().readShort();
					}
				}
				other_Player.setWearing(array);
				other_Player.typeSpecCharList = msg.reader().readByte();
				if (other_Player.typeSpecCharList != 0)
				{
					other_Player.timeDie = msg.reader().readInt();
				}
				ListChar_Screen.vecListChar.addElement(other_Player);
			}
			if (b == 0)
			{
				CreateChar_Screen.gI().Show(GameCanvas.loginScr);
			}
			else
			{
				ListChar_Screen.gI().Show();
			}
		}
		catch (Exception)
		{
		}
	}

	public void ChangeMap(Message msg)
	{
		try
		{
			actionChangeMap = 0;
			GameCanvas.loadmap.idLastMap = GameCanvas.loadmap.idMap;
			short num = msg.reader().readShort();
			if (setMapSea(num))
			{
				idMapLuu = num;
				msgLuu = msg;
				isNondata = false;
			}
			else
			{
				readChangeMapNew(msg, num);
			}
		}
		catch (Exception)
		{
		}
	}

	private bool setMapSea(int id)
	{
		if (LoadMap.mSea != null)
		{
			for (int i = 0; i < LoadMap.mSea.Length; i++)
			{
				if (LoadMap.mSea[i][1] == id && LoadMap.mSea[i][0] == GameCanvas.loadmap.idLastMap)
				{
					GameScreen.player.setActionSea((sbyte)(LoadMap.mSea[i][2] - 1), LoadMap.mSea[i][3], LoadMap.mSea[i][4]);
					return true;
				}
			}
		}
		return false;
	}

	public void danhhieu(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				MsgDanhHieu.instance = new MsgDanhHieu();
				MsgDanhHieu instance = MsgDanhHieu.instance;
				instance.vecDanhHieu = new mVector();
				sbyte b = msg.reader().readByte();
				for (int j = 0; j < b; j++)
				{
					int id2 = msg.reader().readInt();
					string name = msg.reader().readUTF();
					int id_img = msg.reader().readInt();
					sbyte frame = msg.reader().readByte();
					DanhHieu danhHieu2 = new DanhHieu(id2, name, id_img, frame);
					danhHieu2.so_huu = msg.reader().readByte();
					sbyte b2 = msg.reader().readByte();
					for (int k = 0; k < b2; k++)
					{
						danhHieu2.option.Add(new MainInfoItem(msg.reader().readByte(), msg.reader().readInt()));
					}
					instance.vecDanhHieu.addElement(danhHieu2);
				}
				instance.Show(GameCanvas.gameScr);
				break;
			}
			case 1:
			{
				if (msg.reader().readByte() == 0)
				{
					int num2 = msg.reader().readInt();
					if (GameScreen.player.ID == num2)
					{
						GameScreen.player.danhieu = new DanhHieu(msg.reader().readInt(), msg.reader().readInt());
						Debug.Log((object)("id danh hieu su dung" + GameScreen.player.danhieu.id));
						break;
					}
					for (int l = 0; l < GameScreen.vecPlayers.size(); l++)
					{
						MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(l);
						if (mainObject.ID == num2)
						{
							mainObject.danhieu = new DanhHieu(msg.reader().readInt(), msg.reader().readInt());
							break;
						}
					}
					break;
				}
				int num3 = msg.reader().readInt();
				if (GameScreen.player.ID == num3)
				{
					GameScreen.player.danhieu = null;
					break;
				}
				for (int m = 0; m < GameScreen.vecPlayers.size(); m++)
				{
					MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(m);
					if (mainObject2.ID == num3)
					{
						mainObject2.danhieu = null;
						break;
					}
				}
				break;
			}
			case 2:
			{
				int num = msg.reader().readInt();
				sbyte so_huu = msg.reader().readByte();
				for (int i = 0; i < MsgDanhHieu.instance.vecDanhHieu.size(); i++)
				{
					DanhHieu danhHieu = (DanhHieu)MsgDanhHieu.instance.vecDanhHieu.elementAt(i);
					if (danhHieu.id == num)
					{
						danhHieu.so_huu = so_huu;
						break;
					}
				}
				break;
			}
			case 3:
			{
				short id = msg.reader().readShort();
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().read(ref data);
				mImage image = mImage.createImage(data, 0, data.Length);
				DanhHieu.add_Image_Frame(id, image);
				break;
			}
			}
		}
		catch (Exception ex)
		{
			Debug.Log((object)ex.StackTrace.ToString());
		}
	}

	public void readChangeMapNew(Message msg, short idmap)
	{
		try
		{
			GameCanvas.loadMapScr.Show();
			GameScreen.RemoveLoadMap();
			DataMap dataMap = new DataMap();
			GameCanvas.loadmap.idMap = idmap;
			Interface_Game.nameMap = string.Empty;
			GameCanvas.loadMapScr.area = msg.reader().readByte();
			sbyte typeViewPlayer = msg.reader().readByte();
			GameScreen.player.posTransRoad = null;
			GameScreen.player.x = msg.reader().readShort();
			GameScreen.player.y = msg.reader().readShort();
			GameScreen.player.toX = GameScreen.player.x;
			GameScreen.player.toY = GameScreen.player.y;
			GameScreen.player.maxHp = msg.reader().readInt();
			GameScreen.player.Hp = msg.reader().readInt();
			GameScreen.player.maxMp = msg.reader().readInt();
			GameScreen.player.Mp = msg.reader().readInt();
			sbyte b = msg.reader().readByte();
			if (LoadMap.specMap == 3)
			{
				GameScreen.player.isBeginTrain = false;
			}
			else if (LoadMap.specMap == 4)
			{
				GameCanvas.saveRms.loadHotKey(SaveRms.datahotKeySkill);
			}
			LoadMap.specMap = msg.reader().readByte();
			if (LoadMap.specMap == 1 && check_IDMap_PVP())
			{
				GameScreen.tickPvP = 60;
			}
			if (b == 1)
			{
				LoadMap.isOnlineMap = true;
				int num = msg.reader().readInt();
				sbyte[] data = null;
				if (num > 0)
				{
					data = new sbyte[num];
					msg.reader().read(ref data);
				}
				dataMap.setDataMap(data);
				GameCanvas.loadmap.loadmap(data);
				int num2 = msg.reader().readInt();
				sbyte[] data2 = null;
				if (num2 > 0)
				{
					data2 = new sbyte[num2];
					msg.reader().read(ref data2);
				}
				dataMap.setDataItemMap(data2);
				GameCanvas.loadMapScr.mItemMap = data2;
				LoadMap.vecPointChange.removeAllElements();
				sbyte b2 = msg.reader().readByte();
				dataMap.vecPointMap.removeAllElements();
				for (int i = 0; i < b2; i++)
				{
					Point point = new Point();
					point.name = msg.reader().readUTF();
					point.x = msg.reader().readShort();
					point.y = msg.reader().readShort();
					dataMap.vecPointMap.addElement(point);
					int num3 = 115;
					if ((GameCanvas.loadmap.idMap == 158 || GameCanvas.loadmap.idMap == 160 || GameCanvas.loadmap.idMap == 162 || GameCanvas.loadmap.idMap == 164 || GameCanvas.loadmap.idMap == 166) && point.y < GameCanvas.loadmap.mapH * LoadMap.wTile - num3)
					{
						point.y -= 10;
						point.dis = 2;
						point.f = 2;
						point.x2 = point.x;
						point.y2 = point.y + 10;
						point.vy = -1;
					}
					else
					{
						if (GameCanvas.loadmap.idMap == 157 || GameCanvas.loadmap.idMap == 159 || GameCanvas.loadmap.idMap == 161 || GameCanvas.loadmap.idMap == 163 || GameCanvas.loadmap.idMap == 165 || (((GameCanvas.loadmap.idMap >= 167 && GameCanvas.loadmap.idMap <= 176) || GameCanvas.loadmap.idMap == 190) && point.y <= 72))
						{
							continue;
						}
						if (point.x < num3)
						{
							point.dis = 0;
							point.x2 = point.x - 8;
							point.y2 = point.y - 18;
							point.f = 0;
							point.vx = -1;
						}
						else if (point.x > GameCanvas.loadmap.mapW * LoadMap.wTile - num3)
						{
							point.dis = 1;
							point.f = 1;
							point.x2 = point.x + 8;
							point.y2 = point.y - 18;
							point.vx = 1;
						}
						else if (point.y > GameCanvas.loadmap.mapH * LoadMap.wTile - num3 || GameCanvas.loadmap.idMap == 191)
						{
							point.dis = 3;
							point.f = 2;
							point.x2 = point.x;
							point.y2 = point.y - 20;
							point.vy = 1;
						}
						else
						{
							point.y -= 10;
							point.dis = 2;
							point.f = 2;
							point.x2 = point.x;
							point.y2 = point.y + 10;
							point.vy = -1;
						}
					}
					LoadMap.vecPointChange.addElement(point);
				}
			}
			if (b == 0)
			{
				LoadMap.isOnlineMap = false;
				if (LoadMap.specMap == 5)
				{
					LoadRedLine(isFinish: false);
				}
				else if (LoadMap.specMap == 8)
				{
					MapGotoSky.setPos();
				}
				else if (LoadMap.specMap == 12)
				{
					MapGotoGod.setPos();
				}
			}
			LoadMapScreen.IDBack = msg.reader().readByte();
			LoadMapScreen.HBack = msg.reader().readShort();
			dataMap.IdBack = LoadMapScreen.IDBack;
			dataMap.hBack = LoadMapScreen.HBack;
			LoadMapScreen.isNextMap = true;
			if (b == 1)
			{
				GameCanvas.gameScr.setTypeViewPlayer(typeViewPlayer);
			}
			sbyte b3 = msg.reader().readByte();
			sbyte level = msg.reader().readByte();
			LoadMapScreen.typeChangeMap = msg.reader().readByte();
			if (b3 < 0 || GameCanvas.lowGraphic)
			{
				GameScreen.effMap = null;
			}
			else
			{
				GameScreen.effMap = new Effect_Map(b3, level);
			}
			if (LoadMap.specMap == 3)
			{
				GlobalService.gI().Get_Xp_Map_Train(0);
				Player.AutoFireCur = Player.typeAutoFireMain;
				sbyte b4 = msg.reader().readByte();
				MainObject.mPosMapTrain = mSystem.new_M_Int(b4, 2);
				for (int j = 0; j < b4; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						MainObject.mPosMapTrain[j][k] = msg.reader().readByte();
					}
				}
				Player.strTimeChange = msg.reader().readUTF();
			}
			Interface_Game.nameMap = msg.reader().readUTF();
			dataMap.nameMap = Interface_Game.nameMap;
			DataMap.hashDataMap.put(string.Empty + idmap, dataMap);
			if (LoadMap.specMap == 4)
			{
				GameScreen.player.boatSea = new Boat(GameScreen.player.ID, GameScreen.player.x, GameScreen.player.y, 0, (sbyte)GameScreen.player.type_left_right);
				GameScreen.player.setSpeed(4, 3);
				GameScreen.player.vySea = 4;
				if (!GameCanvas.lowGraphic)
				{
					GameScreen.effSea = new Effect_Map(4, 0);
				}
			}
			else if (LoadMap.isOnlineMap && LoadMap.specMap != 5 && LoadMap.specMap != 8)
			{
				GameScreen.player.boatSea = null;
				GameScreen.player.setSpeed(7, 7);
				GameScreen.effSea = null;
			}
		}
		catch (Exception)
		{
			GlobalService.gI().changeMapOk();
		}
	}

	public bool check_IDMap_PVP()
	{
		if (GameCanvas.loadmap.idMap == 58 || GameCanvas.loadmap.idMap == 59 || GameCanvas.loadmap.idMap == 109 || GameCanvas.loadmap.idMap == 119 || GameCanvas.loadmap.idMap == 120 || GameCanvas.loadmap.idMap == 121 || GameCanvas.loadmap.idMap == 123)
		{
			return true;
		}
		return false;
	}

	public void LoadRedLine(bool isFinish)
	{
		MapOff_RedLine.setTypeMoveredLine(0);
		GameCanvas.loadMapScr.mItemMap = null;
		MainScreen.cameraMain.xCam = 0;
		MainScreen.cameraMain.yCam = 0;
		MapOff_RedLine.xHardCodeMapRedLine = MotherCanvas.w / 3;
		if (isFinish)
		{
			MapOff_RedLine.isFinish = true;
			MapOff_RedLine.yHarcodeMapRedLine = MotherCanvas.h / 5 * 3;
			MapOff_RedLine.vxHardcodeRedLine = 8;
			GameScreen.player.x = 0;
			GameScreen.player.y = MapOff_RedLine.yHarcodeMapRedLine + 20;
		}
		else
		{
			MapOff_RedLine.isFinish = false;
			MapOff_RedLine.yHarcodeMapRedLine = MotherCanvas.h / 5 * 4;
			MapOff_RedLine.vxHardcodeRedLine = 6;
			GameScreen.player.x = MapOff_RedLine.xHardCodeMapRedLine;
			GameScreen.player.y = MapOff_RedLine.yHarcodeMapRedLine;
		}
		GameScreen.player.Dir = 2;
		GameScreen.player.type_left_right = 2;
		GameScreen.player.boatSea = new Boat(GameScreen.player.ID, GameScreen.player.x, GameScreen.player.y, 0, (sbyte)GameScreen.player.type_left_right);
		GameScreen.player.boatSea.setPartImage(GameScreen.player.myBoat, GameScreen.player.typePirate);
		Player.isChangeLine = false;
		Player.isShowDie = 0;
		GameScreen.player.lineShowRedLineCur = 2;
		GameScreen.player.lineShowRedLineNext = 2;
		MapOff_RedLine.xOffline = 0;
		MapOff_RedLine.yOffline = MapOff_RedLine.yHarcodeMapRedLine - 40;
		MapOff_RedLine.vecSongTuong.removeAllElements();
		MapOff_RedLine.vecSongBien.removeAllElements();
		MapOff_RedLine.vecDaBien.removeAllElements();
		MapOff_RedLine.vecEffDie.removeAllElements();
		MapOff_RedLine.vecEffSongTuong.removeAllElements();
		int num = MotherCanvas.w / 24 + 4;
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = i * 24;
			if (isFinish)
			{
				point.y = i * 5;
			}
			else
			{
				point.y = -(i * 5);
			}
			point.frame = CRes.random(7);
			MapOff_RedLine.vecSongTuong.addElement(point);
		}
		for (int j = 0; j < MotherCanvas.w / 24 + 2; j++)
		{
			for (int k = 0; k <= (MotherCanvas.h - MapOff_RedLine.yOffline + j * 10 + 6) / 24; k++)
			{
				Point point2 = new Point();
				point2.x = j * 24;
				if (isFinish)
				{
					point2.y = j * 5 - 6 + k * 24;
				}
				else
				{
					point2.y = -j * 5 - 6 + k * 24;
				}
				point2.frame = CRes.random(7);
				if (CRes.random(10) == 0)
				{
					point2.isSmall = true;
				}
				MapOff_RedLine.vecSongBien.addElement(point2);
			}
		}
	}

	public void ObjectMove(Message msg)
	{
		try
		{
			if (!LoadMapScreen.isNextMap)
			{
				return;
			}
			while (msg.reader().available() > 0)
			{
				sbyte cat = msg.reader().readByte();
				short id = msg.reader().readShort();
				short x = msg.reader().readShort();
				short y = msg.reader().readShort();
				if (GameScreen.vecObjMove.size() <= 50)
				{
					ObjMove o = new ObjMove(cat, id, x, y);
					GameScreen.vecObjMove.addElement(o);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void loadImage(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			if (ObjectData.setIdOK(num))
			{
				SaveImageRMS.vecSaveImage.addElement(new idSaveImage(num, data));
			}
			mImage mImage2 = mImage.createImage(data, 0, data.Length);
			if (num >= 1000 && num <= 2000)
			{
				Debug.Log((object)string.Format("[LOAD_IMAGE] imgId={0}, bytes={1}, img={2}", num, data.Length, (mImage2 == null) ? "null" : ((mImage2.image != null) ? $"OK({mImage2.image.w}x{mImage2.image.h})" : "img.image_null")));
			}
			SetImage(mImage2, num);
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"[LOAD_IMAGE ERROR] {arg}");
		}
	}

	public void LoadImageNew(Message msg)
	{
		try
		{
			sbyte type = msg.reader().readByte();
			short num = msg.reader().readShort();
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			if (ObjectData.setIdOK(num))
			{
				SaveImageRMS.vecSaveImage.addElement(new idSaveImage(num, data));
			}
			mImage img = mImage.createImage(data, 0, data.Length);
			SetImage(img, type, num);
		}
		catch (Exception)
		{
		}
	}

	private void updateHash(MyHashTable hash, string key, mImage img)
	{
		if (hash != null && img != null)
		{
			MainImage mainImage = (MainImage)hash.get(key);
			if (mainImage != null)
			{
				mainImage.img = img;
				mainImage.set_W_H();
				mainImage.timeImageNull = 0;
				Debug.Log((object)$"[UPDATE_HASH] Updated MainImage for key={key} in hash={hash.linkImage}, imgSize=({img.image?.w}x{img.image?.h})");
			}
			else
			{
				hash.put(key, new MainImage(img));
				Debug.Log((object)$"[UPDATE_HASH] Added new MainImage for key={key} in hash={hash.linkImage}, imgSize=({img.image?.w}x{img.image?.h})");
			}
		}
	}

	public void SetImage(mImage img, sbyte type, short id)
	{
		if (type == 7)
		{
			updateHash(ObjectData.HashImageCharPart, string.Empty + id, img);
		}
	}

	public void SetImage(mImage img, short id)
	{
		if (id >= 26000)
		{
			updateHash(ObjectData.HashImageCharPart, string.Empty + (id - 26000 + 10000), img);
		}
		else if (id >= 25000)
		{
			updateHash(ObjectData.HashImageEffClientLow, string.Empty + (id - 25000), img);
		}
		else if (id >= 24000)
		{
			updateHash(ObjectData.HashImageEffClient, string.Empty + (id - 24000), img);
		}
		else if (id >= 23000)
		{
			updateHash(ObjectData.HashImageOtherNew, string.Empty + (id - 23000), img);
		}
		else if (id >= 22000)
		{
			updateHash(ObjectData.hashImageIconClanBig, string.Empty + (id - 22000), img);
		}
		else if (id >= 21000)
		{
			updateHash(ObjectData.hashImageSkill, string.Empty + id, img);
		}
		else if (id >= 20000)
		{
			updateHash(ObjectData.HashImageFashion, string.Empty + (id - 20000), img);
		}
		else if (id >= 10000)
		{
			updateHash(ObjectData.HashImageCharPart, string.Empty + (id - 10000), img);
		}
		else if (id >= 9000)
		{
			updateHash(ObjectData.hashImageItemOther, string.Empty + (id - 9000), img);
		}
		else if (id >= 8000)
		{
			updateHash(ObjectData.hashImageBoat, string.Empty + (id - 8000), img);
		}
		else if (id >= 7000)
		{
			updateHash(ObjectData.hashImageIconClan, string.Empty + (id - 7000), img);
		}
		else if (id >= 6500)
		{
			updateHash(ObjectData.hashImageMaterialPotion, string.Empty + (id - 6500), img);
		}
		else if (id >= 6000)
		{
			updateHash(ObjectData.hashImageQuestPotion, string.Empty + (id - 6000), img);
		}
		else if (id >= 5000)
		{
			updateHash(ObjectData.hashImageNPC, string.Empty + (id - 5000), img);
		}
		else if (id >= 4500)
		{
			updateHash(ObjectData.hashImageSkillSmall, string.Empty + (id - 4500), img);
		}
		else if (id >= 4000)
		{
			updateHash(ObjectData.hashImageSkill, string.Empty + (id - 4000), img);
		}
		else if (id >= 3000)
		{
			updateHash(ObjectData.hashImageItem, string.Empty + (id - 3000), img);
		}
		else if (id >= 2000)
		{
			updateHash(ObjectData.hashImagePotion, string.Empty + (id - 2000), img);
		}
		else if (id >= 1000)
		{
			updateHash(ObjectData.HashImageMonster, string.Empty + (id - 1000), img);
		}
		else
		{
			updateHash(ObjectData.HashImageItemMap, string.Empty + id, img);
		}
	}

	public void char_info(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(num, b);
			if (mainObject != null)
			{
				sbyte typePlayer = msg.reader().readByte();
				mainObject.typePirate = msg.reader().readByte();
				mainObject.typePK = msg.reader().readByte();
				sbyte b2 = msg.reader().readByte();
				mainObject.indexTeam = msg.reader().readByte();
				mainObject.name = msg.reader().readUTF();
				mainObject.typePlayer = typePlayer;
				mainObject.Lv = msg.reader().readShort();
				mainObject.maxHp = msg.reader().readInt();
				mSystem.outz(GameScreen.player.name + "char info name=" + mainObject.name + " maxhp=" + mainObject.maxHp + " cat " + b + " typePlayer " + mainObject.typePlayer);
				mainObject.Hp = msg.reader().readInt();
				mainObject.LvThongThao = msg.reader().readShort();
				mainObject.rankWanted = msg.reader().readInt();
				mainObject.levelPerfect = msg.reader().readByte();
				if (mainObject.typePlayer == 1)
				{
					mainObject.colorName = 4;
				}
				if (mainObject.Hp <= 0)
				{
					mainObject.beginDie(null);
					mainObject.isDie = true;
				}
				else
				{
					mainObject.Action = 0;
				}
				if (mainObject.typePlayer == 2 || mainObject.typePlayer == 3)
				{
					if (b2 == 3)
					{
						b2 = -1;
					}
					mainObject.colorName = 4;
					if (mainObject.typePlayer == 3)
					{
						mainObject.colorName = 2;
					}
					mainObject.IdIcon = msg.reader().readShort();
					sbyte imgMonSterforOtherPlayer = msg.reader().readByte();
					short iDMainShiper = msg.reader().readShort();
					sbyte typePirate = msg.reader().readByte();
					mainObject.setSpeed(2, 2);
					mainObject.typePirate = typePirate;
					mainObject.IDMainShiper = iDMainShiper;
					mainObject.setImgMonSterforOtherPlayer(imgMonSterforOtherPlayer);
					if (LoadMap.specMap == 4)
					{
						mainObject.boatSea = new Boat(mainObject.ID, mainObject.x, mainObject.y, 0, (sbyte)mainObject.type_left_right);
						mainObject.vySea = 4;
					}
				}
				else
				{
					if (LoadMap.specMap == 4)
					{
						mainObject.boatSea = new Boat(mainObject.ID, mainObject.x, mainObject.y, 0, (sbyte)mainObject.type_left_right);
						mainObject.setSpeed(4, 3);
						mainObject.wOne = 100;
						mainObject.vySea = 4;
					}
					else
					{
						mainObject.boatSea = null;
						mainObject.setSpeed(7, 7);
						mainObject.wOne = 26;
					}
					mainObject.clazz = msg.reader().readByte();
					mainObject.DirNew = msg.reader().readByte();
					if (b == 2)
					{
						mainObject.nameGiaotiep = msg.reader().readUTF();
						mainObject.chatNPC = msg.reader().readUTF();
						mSystem.outz("NPC name " + mainObject.nameGiaotiep + " chat " + mainObject.chatNPC);
					}
					if (msg.reader().available() > 0)
					{
						mainObject.lvHeart = msg.reader().readByte();
					}
					if (LoadMap.specMap == 3)
					{
						GlobalService.gI().List_Skill_Map_Train(num);
						mainObject.setInfoMapTrain();
					}
					if (GameScreen.typeViewPlayer == 1)
					{
						updateIndexView();
						if (GameScreen.isSetObjdDefault && GameScreen.objView == null)
						{
							GameScreen.objView = mainObject;
						}
					}
				}
				mainObject.isInfo = true;
				mainObject.setDirNew(b2);
				mainObject.setWName();
				short num2 = -1;
				try
				{
					num2 = msg.reader().readShort();
				}
				catch (Exception)
				{
					num2 = -1;
				}
				mainObject.setBodyBay(num2);
				short num3 = -1;
				try
				{
					num3 = msg.reader().readShort();
				}
				catch (Exception)
				{
					num3 = -1;
				}
				mainObject.setLegBay(num3);
				short num4 = -1;
				try
				{
					num4 = msg.reader().readShort();
				}
				catch (Exception)
				{
					num4 = -1;
				}
				mainObject.setWeaponBay(num4);
				short num5 = -1;
				try
				{
					num5 = msg.reader().readShort();
				}
				catch (Exception)
				{
					num5 = -1;
				}
				mainObject.setHairBay(num5);
				mSystem.outz("char_info name = " + mainObject.name + " bay : " + num2 + " " + num3 + " " + num4 + " " + num5);
			}
			else
			{
				mSystem.outloi("name null chang");
			}
		}
		catch (Exception)
		{
		}
	}

	public void updateIndexView()
	{
		sbyte b = 0;
		sbyte b2 = 0;
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject.indexTeam == 1)
			{
				mainObject.indexPaintView = b;
				b++;
			}
			else if (mainObject.indexTeam == 2)
			{
				mainObject.indexPaintView = b2;
				b2++;
			}
		}
	}

	public void remove_Char(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			if (msg.reader().available() == 0)
			{
				return;
			}
			sbyte b = msg.reader().readByte();
			if (num == GameScreen.player.ID)
			{
				return;
			}
			MainObject mainObject = MainObject.get_Object(num, 0);
			if (mainObject == null || mainObject.isRemove)
			{
				return;
			}
			mainObject.isRemove = true;
			if (GameCanvas.lowGraphic)
			{
				GameScreen.checkObjRemoveInVecMove(num, 0);
				return;
			}
			switch (b)
			{
			case 1:
				mainObject.timeEffRemoveChar = 8;
				if (GameScreen.isShowNameSUPER_BOSS)
				{
					GameScreen.addEffectEnd(31, 0, mainObject.x, mainObject.y, (sbyte)mainObject.type_left_right, null);
				}
				break;
			case 2:
				if (mainObject.x < 100)
				{
					mainObject.timeEffRemoveChar = 10;
					mainObject.toX = 0;
				}
				else if (mainObject.x > GameCanvas.loadmap.maxWMap - 100)
				{
					mainObject.timeEffRemoveChar = 10;
					mainObject.toX = GameCanvas.loadmap.maxWMap;
				}
				else
				{
					GameScreen.addEffectEnd(80, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, (sbyte)mainObject.type_left_right, null);
				}
				break;
			case 3:
				if (mainObject.typePlayer != 2 && mainObject.typePlayer != 3)
				{
					for (int i = 0; i < GameScreen.vecBoat.size(); i++)
					{
						Boat boat = (Boat)GameScreen.vecBoat.elementAt(i);
						if (boat.ID != mainObject.ID)
						{
							continue;
						}
						for (int j = 0; j < LoadMap.mSea.Length; j++)
						{
							if (LoadMap.mSea[j][0] == GameCanvas.loadmap.idMap)
							{
								mainObject.timeEffRemoveChar = 80;
								mainObject.setActionSea((sbyte)(LoadMap.mSea[j][2] - 1), boat.x, LoadMap.mSea[j][4]);
								return;
							}
						}
						break;
					}
					GameScreen.addEffectEnd(80, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, (sbyte)mainObject.type_left_right, null);
				}
				else
				{
					mainObject.timeEffRemoveChar = 80;
				}
				break;
			default:
				GameScreen.addEffectEnd(80, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, (sbyte)mainObject.type_left_right, null);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void getData(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 15:
				Get_Monster_Tem(msg);
				break;
			case 25:
				Get_Potion_Tem(msg, 4);
				break;
			case 2:
				Get_Name_Attributes(msg);
				break;
			case 3:
				Get_Skill_Player(msg);
				break;
			case 4:
			{
				sbyte b8 = msg.reader().readByte();
				LoadMap.mLockMap = new sbyte[b8];
				for (int num7 = 0; num7 < b8; num7++)
				{
					LoadMap.mLockMap[num7] = msg.reader().readByte();
				}
				break;
			}
			case 5:
				Get_Data_Char_Part(msg);
				break;
			case 6:
				Get_Data_Name_Map(msg);
				break;
			case 7:
				Get_Data_Name_Potion_Quest(msg);
				break;
			case 8:
				TabInventory.priceItemSell = msg.reader().readShort();
				TabInventory.maxPriceItemSell = msg.reader().readShort();
				TabInventory.pricePotionSell = msg.reader().readShort();
				break;
			case 9:
				Get_Data_Item_Map(msg);
				break;
			case 10:
			{
				sbyte b4 = msg.reader().readByte();
				LoadMap.mMapLang = new short[b4];
				for (int num3 = 0; num3 < b4; num3++)
				{
					LoadMap.mMapLang[num3] = msg.reader().readShort();
				}
				break;
			}
			case 11:
			{
				sbyte b2 = msg.reader().readByte();
				for (int m = 0; m < b2; m++)
				{
					MainMaterial mainMaterial = new MainMaterial(msg.reader().readByte(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readByte());
					MainItem.hashMaterialTem.put(string.Empty + mainMaterial.ID, mainMaterial);
				}
				GlobalService.isGetMaterial = true;
				break;
			}
			case 12:
				Get_Data_Upgrade(msg);
				break;
			case 13:
			{
				sbyte b = msg.reader().readByte();
				LoadMap.mSea = new short[b][];
				for (int k = 0; k < b; k++)
				{
					LoadMap.mSea[k] = new short[6];
					for (int l = 0; l < LoadMap.mSea[k].Length; l++)
					{
						LoadMap.mSea[k][l] = msg.reader().readShort();
					}
				}
				break;
			}
			case 19:
			{
				sbyte b6 = msg.reader().readByte();
				ScreenUpgrade.mTileUpdate = new short[b6];
				for (int num5 = 0; num5 < b6; num5++)
				{
					ScreenUpgrade.mTileUpdate[num5] = msg.reader().readShort();
				}
				sbyte b7 = msg.reader().readByte();
				ScreenUpgrade.mTileGhepĐa = new short[b7];
				for (int num6 = 0; num6 < b7; num6++)
				{
					ScreenUpgrade.mTileGhepĐa[num6] = msg.reader().readShort();
				}
				break;
			}
			case 17:
				GameCanvas.clockServer = msg.reader().readLong() - GameCanvas.timeNow;
				break;
			case 18:
				Get_Potion_Tem(msg, 8);
				break;
			case 21:
				if (msg.reader().readByte() == 0)
				{
					GameScreen.h12plus = 0;
					Interface_Game.setYTouch();
				}
				else
				{
					GameScreen.h12plus = 12;
					Interface_Game.setYTouch();
				}
				GameScreen.interfaceGame.loadPosLoL();
				break;
			case 22:
				switch (msg.reader().readByte())
				{
				case 1:
					GameScreen.isShowNameXpArena = true;
					break;
				case 0:
					GameScreen.isShowNameSUPER_BOSS = false;
					break;
				case 2:
					GameScreen.isShowNameWW = true;
					break;
				}
				break;
			case 23:
				Get_Data_Char_Part(msg);
				break;
			case 26:
			{
				GlobalService.isGetKichAn = true;
				sbyte b5 = msg.reader().readByte();
				for (int num4 = 0; num4 < b5; num4++)
				{
					string v = msg.reader().readUTF();
					MainItem.hashAttriKichAn.put(string.Empty + num4, v);
				}
				break;
			}
			case 27:
				if (msg.reader().readByte() == 1)
				{
					GameScreen.isOpenDao = true;
				}
				else
				{
					GameScreen.isOpenDao = false;
				}
				break;
			case 28:
				Update_Potion_Tem(msg, 4);
				break;
			case 29:
				Update_Potion_Tem(msg, 4);
				break;
			case 30:
			{
				sbyte b3 = msg.reader().readByte();
				T.mEffSpec = new string[b3];
				for (int n = 0; n < b3; n++)
				{
					T.mEffSpec[n] = msg.reader().readUTF();
				}
				break;
			}
			case 31:
			{
				short num = msg.reader().readShort();
				MainObject.dataEffHair = new DataHairServer[num];
				for (int i = 0; i < num; i++)
				{
					MainObject.dataEffHair[i] = new DataHairServer();
					MainObject.dataEffHair[i].id = msg.reader().readInt();
					MainObject.dataEffHair[i].idIcon = msg.reader().readInt();
				}
				short num2 = msg.reader().readShort();
				if (num2 > 0)
				{
					MainObject.dataInfoHair = new DataHairServer[num2];
					for (int j = 0; j < num2; j++)
					{
						MainObject.dataInfoHair[j] = new DataHairServer();
						MainObject.dataInfoHair[j].id = msg.reader().readInt();
						MainObject.dataInfoHair[j].info = msg.reader().readUTF();
					}
				}
				break;
			}
			case 14:
			case 16:
			case 20:
			case 24:
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	private void Get_Data_Upgrade(Message msg)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			MainDataUpgrade.LoadDataUpgrade(new DataInputStream(new ByteArrayInputStream(data)), isSave: true);
			SaveRms.saveData(data, "dataUpgradeSave");
		}
		catch (Exception)
		{
		}
	}

	private void Get_Data_Name_Potion_Quest(Message msg)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			MainQuest.LoadNamePotionQuest(new DataInputStream(new ByteArrayInputStream(data)), isSave: true);
			SaveRms.saveData(data, "dataNamePotionquest");
		}
		catch (Exception)
		{
		}
	}

	private void Get_Data_Name_Map(Message msg)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			LoadMap.LoadNameMap(new DataInputStream(new ByteArrayInputStream(data)), isSave: true);
			SaveRms.saveData(data, "dataNameMap");
		}
		catch (Exception)
		{
		}
	}

	private void Get_Data_Char_Part(Message msg)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			CharPartInfo.LoadDataCharPart(new DataInputStream(new ByteArrayInputStream(data)), 1);
			SaveRms.saveData(data, "dataCharPart");
		}
		catch (Exception)
		{
		}
	}

	private void Get_Name_Attributes(Message msg)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			MainItem.LoadNameAttribute(new DataInputStream(new ByteArrayInputStream(data)), isSave: true);
			SaveRms.saveData(data, "dataAttri");
		}
		catch (Exception)
		{
		}
	}

	private void Get_Potion_Tem(Message msg, sbyte typePotion)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			Potion.UpdateDataPotion(new DataInputStream(new ByteArrayInputStream(data)), isSave: true, typePotion);
			if (typePotion == 4)
			{
				SaveRms.saveData(data, "dataPotion");
			}
			if (typePotion == 8)
			{
				SaveRms.saveData(data, "dataPotionClan");
			}
		}
		catch (Exception)
		{
		}
	}

	private void Update_Potion_Tem(Message msg, sbyte typePotion)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			Potion.UpdateDataPotion(new DataInputStream(new ByteArrayInputStream(data)), isSave: true, typePotion);
			if (typePotion == 4)
			{
				SaveRms.saveData(data, "dataPotion");
			}
			if (typePotion == 8)
			{
				SaveRms.saveData(data, "dataPotionClan");
			}
		}
		catch (Exception)
		{
		}
	}

	public void Get_Monster_Tem(Message msg)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			CatalogyMonster.LoadDataMon(new DataInputStream(new ByteArrayInputStream(data)), isSave: true);
			SaveRms.saveData(data, "dataMon");
		}
		catch (Exception)
		{
		}
	}

	private void Get_Skill_Player(Message msg)
	{
		try
		{
			indexHotKeySkill = 0;
			Player.vecListSkill.removeAllElements();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				Skill_Info skill_Info = readSkillInfo(msg);
				if (skill_Info != null)
				{
					skill_Info.indexHotKey = indexHotKeySkill;
					Player.vecListSkill.addElement(skill_Info);
					indexHotKeySkill++;
				}
			}
			Player.vecListSkill = MainItem.SortVecItem(Player.vecListSkill);
			GlobalService.gI().Save_RMS_Server(0, 0, null);
			Player.isSkillready = false;
			for (int j = 0; j < Player.vecListSkill.size(); j++)
			{
				Skill_Info skill_Info2 = (Skill_Info)Player.vecListSkill.elementAt(j);
				if (skill_Info2.Lv_RQ == -1 && ((skill_Info2.typeSkill != 3 && skill_Info2.typeSkill != 6) || TabSkill.numCurrentPassive < Player.numPassive))
				{
					Player.isSkillready = true;
					break;
				}
			}
			Player.setHotKeyBuff();
		}
		catch (Exception)
		{
		}
	}

	public Skill_Info readSkillInfo(Message msg)
	{
		Skill_Info skill_Info = null;
		try
		{
			skill_Info = new Skill_Info(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte(), msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), 0);
			skill_Info.getData(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readInt(), msg.reader().readByte(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readShort(), msg.reader().readByte());
			skill_Info.vecAtt.removeAllElements();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				MainInfoItem o = new MainInfoItem(msg.reader().readByte(), msg.reader().readShort());
				skill_Info.vecAtt.addElement(o);
			}
			skill_Info.idEffSpec = msg.reader().readByte();
			if (skill_Info.idEffSpec > 0)
			{
				skill_Info.perEffSpec = msg.reader().readShort();
				skill_Info.timeEffSpec = msg.reader().readShort();
			}
			skill_Info.LvDevilSkill = msg.reader().readByte();
			skill_Info.phanTramDevilSkill = msg.reader().readByte();
		}
		catch (Exception)
		{
		}
		return skill_Info;
	}

	private void Get_Data_Item_Map(Message msg)
	{
		mSystem.outz("000000000000000000 Get_Data_Item_Map");
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			LoadMap.load_Table_Map(new DataInputStream(new ByteArrayInputStream(data)), isSave: true);
			SaveRms.saveData(data, "dataItemMap");
		}
		catch (Exception)
		{
		}
	}

	public void monsterInfo(Message msg)
	{
		try
		{
			if (!LoadMapScreen.isNextMap)
			{
				return;
			}
			short iD = msg.reader().readShort();
			short idCatMonster = msg.reader().readShort();
			MainObject mainObject = MainObject.get_Object(iD, 1);
			if (mainObject != null)
			{
				mainObject.isRemove = true;
			}
			short x = msg.reader().readShort();
			short y = msg.reader().readShort();
			MainMonster mainMonster = MainMonster.createMonster(iD, x, y, idCatMonster);
			if (mainMonster == null)
			{
				return;
			}
			mainMonster.Lv = msg.reader().readShort();
			mainMonster.Hp = msg.reader().readInt();
			mainMonster.maxHp = msg.reader().readInt();
			short typeEff = msg.reader().readShort();
			MainSkill skilldefault = new MainSkill(-1, typeEff);
			mainMonster.Skilldefault = skilldefault;
			mainMonster.timeRevice = msg.reader().readShort();
			mainMonster.typeSpecMonSter = msg.reader().readByte();
			mainMonster.isInfo = true;
			if (msg.reader().available() > 0)
			{
				mainMonster.LvThongThao = msg.reader().readByte();
				if (mainMonster.LvThongThao > 0)
				{
					mainMonster.name = mainMonster.name + T.bat + mainMonster.LvThongThao;
					mainMonster.setWName();
				}
			}
			mainMonster.idCatMonster = idCatMonster;
			GameScreen.addPlayer(mainMonster);
			if (mainMonster.isPokemon > 0)
			{
				GameScreen.addEffectEnd_ObjTo(32, 0, mainMonster.x, mainMonster.y, mainMonster.ID, mainMonster.typeObject, (sbyte)mainMonster.Dir, null);
			}
			if (mainMonster.isTru() && mainMonster.Hp <= 0)
			{
				mainMonster.isDie = true;
				mainMonster.timeDie = 0L;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Fire_Object(Message msg)
	{
		try
		{
			if (LoadMapScreen.isNextMap)
			{
				if (GameCanvas.lowGraphic && GameScreen.vecObjFire.size() > 20)
				{
					GameScreen.vecObjFire.removeElementAt(1);
				}
				msg.isOld = 1;
				GameScreen.vecObjFire.addElement(msg);
			}
		}
		catch (Exception)
		{
		}
	}

	public void MonsterNoneFocus(Message msg)
	{
		try
		{
			MainMonster mainMonster = (MainMonster)MainObject.get_Object(msg.reader().readShort(), 1);
			if (mainMonster == null || mainMonster.returnAction())
			{
				return;
			}
			if (mainMonster.Action == 4)
			{
				if (mainMonster.timeRevice < 0)
				{
					return;
				}
				mainMonster.Reveive();
			}
			if (mainMonster.skillCurrent != null)
			{
				mainMonster.beginFire();
			}
			mainMonster.isRunAttack = false;
			mainMonster.objMainFocus = null;
			mainMonster.posTransRoad = null;
		}
		catch (Exception)
		{
		}
	}

	public void diePlayer(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			short iD2 = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			short pointPk = msg.reader().readShort();
			MainObject mainObject = MainObject.get_Object(iD2, tem);
			MainObject mainObject2 = MainObject.get_Object(iD, b);
			if (b == 0 && mainObject2 != null && !mainObject2.returnAction())
			{
				mainObject2.pointPk = pointPk;
			}
			if (mainObject == null || mainObject.returnAction() || mainObject.Hp <= 0)
			{
				return;
			}
			mainObject.Hp = 0;
			mainObject.beginDie(mainObject2);
			if (mainObject == GameScreen.player || (mainObject.typeObject == 0 && CRes.random(3) == 0))
			{
				if (mainObject.clazz == 4)
				{
					mSound.playSound(36, mSound.volumeSound);
				}
				else
				{
					mSound.playSound(35, mSound.volumeSound);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Revice_Player(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null && !mainObject.returnAction())
			{
				mainObject.maxHp = msg.reader().readInt();
				mainObject.maxMp = msg.reader().readInt();
				mainObject.Reveive();
			}
		}
		catch (Exception)
		{
		}
	}

	public void Set_XP(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 0);
			if (mainObject != null && !mainObject.returnAction())
			{
				if (mainObject.Lv == 100)
				{
					mainObject.percentThongThao = msg.reader().readShort();
				}
				else
				{
					mainObject.percentLv = msg.reader().readShort();
				}
				int num = msg.reader().readInt();
				if (!GameCanvas.lowGraphic || mainObject == GameScreen.player)
				{
					GameScreen.addEffectNum(num + "xp", mainObject.x, mainObject.y - mainObject.hOne, 1);
				}
				if (mainObject == GameScreen.player && GameScreen.isShowTextTab)
				{
					GameCanvas.chatTabScr.addNewChat(T.tabTestAdmin, "+Xp: ", string.Empty + num, 1, isFocus: false);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Set_XP_Skill(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 0);
			if (mainObject != null && !mainObject.returnAction())
			{
				int num = msg.reader().readInt();
				if (!GameCanvas.lowGraphic || mainObject == GameScreen.player)
				{
					GameScreen.addEffectNum(num + "xp", mainObject.x, mainObject.y - mainObject.hOne, 24);
				}
				if (mainObject == GameScreen.player && GameScreen.isShowTextTab)
				{
					GameCanvas.chatTabScr.addNewChat(T.tabTestAdmin, "+XpSkill: ", string.Empty + num, 1, isFocus: false);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Get_Xp_Map_Train(Message msg)
	{
		try
		{
			LoadMap.currentXp = msg.reader().readInt();
			LoadMap.maxXp = msg.reader().readInt();
		}
		catch (Exception)
		{
		}
	}

	public void Main_char_Info(Message msg)
	{
		try
		{
			if (GameScreen.player == null)
			{
				GameScreen.player = new Player();
				resetPlayer();
			}
			GameScreen.player.ID = msg.reader().readShort();
			GameScreen.player.name = msg.reader().readUTF();
			GameCanvas.saveRms.saveUserLast(GameScreen.player.name);
			GameScreen.player.maxHp = msg.reader().readInt();
			GameScreen.player.maxMp = msg.reader().readInt();
			GameScreen.player.Hp = msg.reader().readInt();
			GameScreen.player.Mp = msg.reader().readInt();
			GameScreen.player.Lv = msg.reader().readShort();
			GameScreen.player.percentLv = msg.reader().readShort();
			GameScreen.player.LvThongThao = msg.reader().readShort();
			GameScreen.player.percentThongThao = msg.reader().readShort();
			GameScreen.player.rankWanted = msg.reader().readInt();
			GameScreen.player.clazz = msg.reader().readByte();
			GameScreen.player.isInfo = true;
			GameScreen.player.pointPk = msg.reader().readInt();
			Player.pointAttribute = msg.reader().readShort();
			GameScreen.player.typePirate = msg.reader().readByte();
			Player.indexGhostServer = msg.reader().readByte();
			Player.numPassive = msg.reader().readByte();
			GameScreen.player.levelPerfect = msg.reader().readByte();
			Player.giamCountDownAtt = 0;
			readAttribute(msg);
			Player.pointSkill = msg.reader().readShort();
			sbyte num = msg.reader().readByte();
			Player.mLvSkill = new sbyte[num];
			Player.mLvSkillPlus = new sbyte[num];
			for (int i = 0; i < Player.mLvSkill.Length; i++)
			{
				Player.mLvSkill[i] = msg.reader().readByte();
			}
			for (int j = 0; j < Player.mLvSkillPlus.Length; j++)
			{
				Player.mLvSkillPlus[j] = msg.reader().readByte();
			}
			GameScreen.player.vecAllInfo.removeAllElements();
			sbyte b = msg.reader().readByte();
			for (int k = 0; k < b; k++)
			{
				MainInfoItem mainInfoItem = new MainInfoItem(msg.reader().readByte(), msg.reader().readInt());
				if (mainInfoItem.id <= 4 || mainInfoItem.id >= 10)
				{
					GameScreen.player.vecAllInfo.addElement(mainInfoItem);
				}
				for (int l = 0; l < Player.hardCodeShortInfo[0].Length; l++)
				{
					if (mainInfoItem.id == Player.hardCodeShortInfo[0][l])
					{
						Player.hardCodeShortInfo[1][l] = mainInfoItem.value;
					}
				}
				if (mainInfoItem.id == 25)
				{
					Player.giamCountDownAtt = (short)mainInfoItem.value;
				}
			}
			Player.SetGiamCountDown();
			getInfoEquip();
			short num2 = -1;
			try
			{
				num2 = msg.reader().readShort();
			}
			catch (Exception)
			{
				num2 = -1;
			}
			GameScreen.player.setBodyBay(num2);
			short num3 = -1;
			try
			{
				num3 = msg.reader().readShort();
			}
			catch (Exception)
			{
				num3 = -1;
			}
			GameScreen.player.setLegBay(num3);
			short num4 = -1;
			try
			{
				num4 = msg.reader().readShort();
			}
			catch (Exception)
			{
				num4 = -1;
			}
			GameScreen.player.setWeaponBay(num4);
			short num5 = -1;
			try
			{
				num5 = msg.reader().readShort();
			}
			catch (Exception)
			{
				num5 = -1;
			}
			GameScreen.player.setHairBay(num5);
			mSystem.outz("Main_char_Info name = " + GameScreen.player.name + " bay : " + num2 + " " + num3 + " " + num4 + " " + num5);
		}
		catch (Exception)
		{
		}
	}

	public void readAttribute(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			Main_Attribute[] array = new Main_Attribute[b];
			for (int i = 0; i < b; i++)
			{
				string name = msg.reader().readUTF();
				short value = msg.reader().readShort();
				short valueP = msg.reader().readShort();
				MainInfoItem[] array2 = new MainInfoItem[msg.reader().readByte()];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = new MainInfoItem(msg.reader().readByte(), msg.reader().readInt());
				}
				array[i] = new Main_Attribute((sbyte)i, value, valueP, name, array2);
			}
			TabInfo.updateTabAttri(array);
		}
		catch (Exception)
		{
		}
	}

	public void getInfoEquip()
	{
		Player.InfoShortEquip[0] = MainItem.strGetPercent(Player.hardCodeShortInfo[1][0], 0);
		Player.InfoShortEquip[1] = MainItem.strGetPercent(Player.hardCodeShortInfo[1][1], 1);
		Player.InfoShortEquip[2] = string.Empty + (Player.hardCodeShortInfo[1][2] + Player.hardCodeShortInfo[1][2] * Player.hardCodeShortInfo[1][3] / 1000);
		Player.InfoShortEquip[3] = MainItem.strGetPercent(Player.hardCodeShortInfo[1][4], 1);
		Player.InfoShortEquip[4] = MainItem.strGetPercent(Player.hardCodeShortInfo[1][5], 1);
		Player.InfoShortEquip[5] = MainItem.strGetPercent(Player.hardCodeShortInfo[1][6], 1);
	}

	public static void resetPlayer()
	{
		GameCanvas.saveRms.saveUserPass(GameCanvas.loginScr.tfUser.getText(), GameCanvas.loginScr.tfPass.getText());
		GameScreen.vecPlayers.addElement(GameScreen.player);
		GameCanvas.chatTabScr.vecTabChat.removeAllElements();
		Player.vecParty.removeAllElements();
		Player.mSatnhan = new short[0];
		GameCanvas.tabAllScr.idSelect = 0;
		GameCanvas.tabAllScr.setTabSelect();
		GameCanvas.tabAllScr.tabCurrent.beginFocus();
		Interface_Game.vecEventShow.removeAllElements();
		Interface_Game.vecInfoServer.removeAllElements();
		Interface_Game.infoFight = null;
		Interface_Game.infoNormal = null;
		Interface_Game.infoSpec = null;
		Interface_Game.mCountKichAn = null;
		RoomWantedScreen.instance = null;
		LoadMap.hashMapItem.clear();
		LoadMap.demSendTem = 0;
		Player.vecEvent.removeAllElements();
		Player.vecChest.removeAllElements();
		Player.vecInventory.removeAllElements();
		Player.vecInvenClan.removeAllElements();
		Player.mComboSkill = null;
		Player.isAutoMaterial = 0;
		Player.isGhost = false;
		Player.beliTest = 0L;
		MainTab.CDKeyBoss.setCountDown(0);
		MainTab.CDPvP.setCountDown(0);
		MainTab.CDTicket.setCountDown(0);
		MainTab.CDx2XP.setCountDown(0);
		LoadMap.specMap = 0;
		GameScreen.indexHelp = -1;
		GameScreen.vecHelp = null;
		GameCanvas.chatTabScr.addNewChat(T.tabServer, string.Empty, T.thongbao, 1, isFocus: false);
		GameCanvas.chatTabScr.getCurTab(0);
		GameCanvas.chatTabScr.updateCamTab();
		GameScreen.numMess = 0;
		FriendList.isGetListFriend = false;
		Player.isGetItem = true;
		Player.isMPHP = true;
		Player.typeAutoFireMain = 1;
		Player.AutoFireCur = 0;
		Player.isGetDataClan = -1;
		for (int i = 0; i < Player.mChestWanted.Length; i++)
		{
			Player.mChestWanted[i] = null;
		}
		GameScreen.player.clan = null;
		GameCanvas.ClanScr = null;
		MsgAutoFire.value = null;
		ScreenUpgradeSkillDevil.instance = null;
		Player.setStart_EndAutoFire(isAu: false);
		GlobalService.gI().Save_RMS_Server(0, 1, null);
		GlobalService.gI().Save_RMS_Server(0, 8, null);
		GlobalService.gI().Save_RMS_Server(0, 2, null);
		GlobalService.gI().Save_RMS_Server(0, 3, null);
		GlobalService.gI().Save_RMS_Server(0, 4, null);
		GlobalService.gI().Save_RMS_Server(0, 5, null);
		GlobalService.gI().Save_RMS_Server(0, 6, null);
		GlobalService.gI().Save_RMS_Server(0, 7, null);
		GlobalService.gI().Save_RMS_Server(0, 9, null);
		GlobalService.gI().Save_RMS_Server(0, 10, null);
		if (Player.AutoRevice == 1)
		{
			Interface_Game.addInfoPlayerNormal(T.dangbatauto, mFont.tahoma_7_white);
		}
	}

	public void Dialog_More_server(Message msg)
	{
		if (GameCanvas.currentScreen == GameCanvas.updateImageAndroidScr)
		{
			GameCanvas.currentDialog = null;
			GameCanvas.subDialog = null;
			return;
		}
		try
		{
			IdDialog = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			string text = msg.reader().readUTF();
			string info = msg.reader().readUTF();
			mVector mVector2 = new mVector();
			sbyte b2 = msg.reader().readByte();
			for (int i = 0; i < b2; i++)
			{
				string caption = msg.reader().readUTF();
				sbyte subType = msg.reader().readByte();
				iCommand iCommand2 = new iCommand(caption, 0, subType, this);
				sbyte b3 = msg.reader().readByte();
				if (b3 >= 0)
				{
					iCommand2 = setImgCmdNew(iCommand2, b3);
				}
				mVector2.addElement(iCommand2);
			}
			if (IdDialog == 33 && ScreenUpgrade.isClickUpgradeSkillDevilAuto && ScreenUpgrade.choiceDevilBeriOrRuby != -1)
			{
				GlobalService.gI().Choice_Dialog_server(IdDialog, ScreenUpgrade.choiceDevilBeriOrRuby);
				return;
			}
			if (IdDialog == 28 && MsgUpdateHeart.isClickUpgradeHeartAuto)
			{
				GlobalService.gI().Choice_Dialog_server(IdDialog, 0);
				return;
			}
			switch (b)
			{
			case 0:
				GameCanvas.Start_Normal_DiaLog(info, mVector2, isCmdClose: true);
				break;
			case 1:
			{
				MsgDialog msgDialog = new MsgDialog();
				msgDialog.setinfoDownload(info, text);
				GameCanvas.Start_Current_Dialog(msgDialog);
				break;
			}
			case 2:
				GameCanvas.Start_Normal_DiaLog(info, mVector2, isCmdClose: false);
				break;
			case 3:
			{
				int time = msg.reader().readInt();
				GameCanvas.Start_Time_DiaLog(info, isCmdClose: false, time, mVector2);
				break;
			}
			case 4:
			{
				sbyte b4 = msg.reader().readByte();
				Item_Drop[] array = new Item_Drop[b4];
				for (int j = 0; j < b4; j++)
				{
					sbyte type = msg.reader().readByte();
					string name = msg.reader().readUTF();
					short idIcon = msg.reader().readShort();
					int num = msg.reader().readInt();
					sbyte color = msg.reader().readByte();
					array[j] = new Item_Drop((short)j, type, name, 0, 0, idIcon, color);
					array[j].num = num;
				}
				MsgShowGift msgShowGift = new MsgShowGift();
				msgShowGift.setinfoShow_Gift(1, text, info, array, -1);
				msgShowGift.setCmdList(mVector2);
				GameCanvas.Start_Current_Dialog(msgShowGift);
				break;
			}
			}
			GameMidlet.instance.removeEditText();
		}
		catch (Exception)
		{
		}
	}

	private iCommand setImgCmdNew(iCommand cmd, sbyte IdImg)
	{
		MainItem item = new MainItem(100, IdImg, 0);
		cmd.setItem(item);
		return cmd;
	}

	public void Item_Drop(Message msg)
	{
		try
		{
			mVector mVector2 = new mVector();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				short iD = msg.reader().readShort();
				sbyte typeItem = msg.reader().readByte();
				short idIcon = msg.reader().readShort();
				sbyte colorName = msg.reader().readByte();
				string name = msg.reader().readUTF();
				MainItem mainItem = new MainItem(typeItem, iD, idIcon, name, 0);
				mainItem.colorName = colorName;
				mVector2.addElement(mainItem);
			}
			short iD2 = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			short iD3 = msg.reader().readShort();
			int num = 456;
			int num2 = 240;
			MainObject mainObject = MainObject.get_Object(iD2, tem);
			if (mainObject != null)
			{
				num = mainObject.x;
				num2 = mainObject.y;
			}
			else
			{
				MainObject mainObject2 = MainObject.get_Object(iD3, 0);
				if (mainObject2 != null)
				{
					num = mainObject2.x;
					num2 = mainObject2.y;
				}
			}
			if (mVector2.size() <= 3)
			{
				for (int j = 0; j < mVector2.size(); j++)
				{
					MainItem mainItem2 = (MainItem)mVector2.elementAt(j);
					GameScreen.addPlayer(new Item_Drop(mainItem2.ID, mainItem2.typeObject, mainItem2.name, num, num2, mainItem2.idIcon, mainItem2.colorName));
				}
				return;
			}
			int num3 = 40;
			int num4 = 45;
			int num5 = CRes.random(num4);
			for (int k = 0; k < mVector2.size(); k++)
			{
				switch (k)
				{
				case 8:
					num3 = 60;
					num4 = 30;
					num5 = CRes.random(num4);
					break;
				case 20:
					num3 = 80;
					num4 = 22;
					num5 = CRes.random(num4);
					break;
				case 36:
					num3 = 100;
					num4 = 15;
					num5 = CRes.random(num4);
					break;
				}
				int xto = num + CRes.getcos(CRes.fixangle(num5)) * num3 / 1000;
				int yto = num2 + CRes.getsin(CRes.fixangle(num5)) * num3 / 1000;
				MainItem mainItem3 = (MainItem)mVector2.elementAt(k);
				GameScreen.addPlayer(new Item_Drop(mainItem3.ID, mainItem3.typeObject, mainItem3.name, num, num2, mainItem3.idIcon, mainItem3.colorName, xto, yto));
				num5 += num4;
			}
		}
		catch (Exception)
		{
		}
	}

	public void GetItemMap(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			short iD2 = msg.reader().readShort();
			Item_Drop item_Drop = (Item_Drop)MainObject.get_Object(iD, b);
			if (item_Drop == null || item_Drop.returnAction())
			{
				return;
			}
			MainObject mainObject = MainObject.get_Object(iD2, 0);
			if (mainObject != null && !mainObject.returnAction())
			{
				if (mainObject == GameScreen.player)
				{
					if (GameScreen.indexHelp == 9)
					{
						MainHelp.setNextHelp(isHanhTrang: false);
					}
					MainHelp.checkRemoveIndexHelp(8);
				}
				if (!GameCanvas.lowGraphic || mainObject == GameScreen.player)
				{
					if (item_Drop.typeObject == 4 && item_Drop.colorName == 5)
					{
						GameScreen.addEffectNumImage(item_Drop.name, mainObject.x, mainObject.y - mainObject.hOne / 2, 10, AvMain.fraMoney, 0);
					}
					else if ((item_Drop.typeObject == 3 || item_Drop.typeObject == 4) && item_Drop.colorName > 0)
					{
						GameScreen.addEffectNum(item_Drop.name, mainObject.x, mainObject.y - mainObject.hOne / 2, (sbyte)(-item_Drop.colorName));
					}
					else
					{
						GameScreen.addEffectNum(item_Drop.name, mainObject.x, mainObject.y - mainObject.hOne / 2, 5);
					}
				}
				item_Drop.objMainFocus = mainObject;
				if (mainObject == GameScreen.player && b == 5)
				{
					MainQuest.updateDataQuestGetPotion(item_Drop.IdIcon);
				}
			}
			else
			{
				item_Drop.isRemove = true;
			}
		}
		catch (Exception)
		{
		}
	}

	public void GetItemMapLittle(Message msg)
	{
		try
		{
			if (LoadMap.specMap != 7)
			{
				return;
			}
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			sbyte b = msg.reader().readByte();
			Item_Drop item_Drop = (Item_Drop)MainObject.get_Object(iD, tem);
			if (item_Drop == null || item_Drop.returnAction())
			{
				return;
			}
			BigBossLittleGraden bigBossLittleGraden = null;
			for (int i = 0; i < GameScreen.vecBigBossLittleGraden.size(); i++)
			{
				BigBossLittleGraden bigBossLittleGraden2 = (BigBossLittleGraden)GameScreen.vecBigBossLittleGraden.elementAt(i);
				if (bigBossLittleGraden2.type == b)
				{
					bigBossLittleGraden = bigBossLittleGraden2;
					break;
				}
			}
			if (bigBossLittleGraden != null)
			{
				item_Drop.bossLittle = bigBossLittleGraden;
			}
			else
			{
				item_Drop.isRemove = true;
			}
		}
		catch (Exception)
		{
		}
	}

	public void remove_Obj(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != GameScreen.player && mainObject != null && !mainObject.isRemove)
			{
				mainObject.isRemove = true;
				mainObject.timeEffRemoveChar = 2;
			}
		}
		catch (Exception)
		{
		}
	}

	public void update_InVen_Or_Chest(Message msg, mVector vec, sbyte typeInvenOrChest)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			sbyte b2 = msg.reader().readByte();
			switch (b)
			{
			case 0:
			{
				MainItem.removeUpdateItemVec(b2, vec);
				sbyte b3 = msg.reader().readByte();
				switch (b2)
				{
				case 4:
				case 8:
				{
					for (int m = 0; m < b3; m++)
					{
						Potion potion = readUpdatePotion(msg, isShop: false, b2);
						if (potion != null)
						{
							vec.addElement(potion);
						}
					}
					break;
				}
				case 3:
				{
					for (int k = 0; k < b3; k++)
					{
						Item item = readUpdateItem(msg, isShop: false);
						if (item != null)
						{
							vec.addElement(item);
						}
					}
					break;
				}
				case 5:
				{
					for (int l = 0; l < b3; l++)
					{
						Quest_Potion quest_Potion = readUpdateQuestPotion(msg);
						if (quest_Potion != null)
						{
							vec.addElement(quest_Potion);
						}
					}
					break;
				}
				case 7:
				{
					for (int j = 0; j < b3; j++)
					{
						MainMaterial mainMaterial = readUpdateMaterial(msg, isShop: false);
						if (mainMaterial != null)
						{
							vec.addElement(mainMaterial);
						}
					}
					break;
				}
				}
				break;
			}
			case 1:
				switch (b2)
				{
				case 4:
				case 8:
				{
					Potion potion2 = readUpdatePotion(msg, isShop: false, b2);
					if (potion2 == null)
					{
						return;
					}
					MainItem itemVec3 = MainItem.getItemVec(b2, potion2.ID, vec);
					if (itemVec3 == null)
					{
						vec.addElement(potion2);
					}
					else
					{
						vec.setElementAt(potion2, vec.indexOf(itemVec3));
					}
					break;
				}
				case 3:
				{
					Item item2 = readUpdateItem(msg, isShop: false);
					if (item2 == null)
					{
						return;
					}
					MainItem itemVec4 = MainItem.getItemVec(b2, item2.ID, vec);
					if (itemVec4 == null)
					{
						vec.addElement(item2);
					}
					else
					{
						vec.setElementAt(item2, vec.indexOf(itemVec4));
					}
					break;
				}
				case 5:
				{
					Quest_Potion quest_Potion2 = readUpdateQuestPotion(msg);
					if (quest_Potion2 == null)
					{
						return;
					}
					MainItem itemVec2 = MainItem.getItemVec(b2, quest_Potion2.ID, vec);
					if (itemVec2 == null)
					{
						vec.addElement(quest_Potion2);
					}
					else
					{
						vec.setElementAt(quest_Potion2, vec.indexOf(itemVec2));
					}
					break;
				}
				case 7:
				{
					MainMaterial mainMaterial2 = readUpdateMaterial(msg, isShop: false);
					if (mainMaterial2 == null)
					{
						return;
					}
					MainItem itemVec = MainItem.getItemVec(b2, mainMaterial2.ID, vec);
					if (itemVec == null)
					{
						vec.addElement(mainMaterial2);
					}
					else
					{
						vec.setElementAt(mainMaterial2, vec.indexOf(itemVec));
					}
					break;
				}
				}
				MainTabShop.isSortInven = true;
				break;
			case 2:
			{
				short num8 = msg.reader().readShort();
				MainItem itemVec5 = MainItem.getItemVec(b2, num8, vec);
				if (itemVec5 != null)
				{
					itemVec5.numPotion = 0;
					itemVec5.isRemove = true;
					vec.removeElement(itemVec5);
				}
				if (typeInvenOrChest == 100 && b2 == 4)
				{
					for (int n = 0; n < Player.hotkeyPlayer.Length; n++)
					{
						for (int num9 = 0; num9 < Player.hotkeyPlayer[n].Length; num9++)
						{
							Hotkey hotkey = Player.hotkeyPlayer[n][num9];
							if (hotkey.itemcur != null && hotkey.itemcur.ID == num8)
							{
								hotkey.itemcur = null;
							}
						}
					}
				}
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
				{
					GameCanvas.tabInven.getItemCurNew();
				}
				break;
			}
			case 3:
			{
				long num = msg.reader().readLong();
				int num2 = msg.reader().readInt();
				switch (typeInvenOrChest)
				{
				case 100:
				{
					bool flag = false;
					short ticket = msg.reader().readShort();
					updateMoney(num, num2, ticket);
					GameScreen.player.beli = num;
					GameScreen.player.gem = num2;
					Player.ticket = ticket;
					Player.maxTicket = msg.reader().readShort();
					sbyte num3 = msg.reader().readByte();
					if (num3 != Player.PvPticket)
					{
						flag = true;
					}
					Player.maxPvPticket = msg.reader().readByte();
					Player.PvPticket = num3;
					sbyte num4 = msg.reader().readByte();
					if (num4 != Player.keyBoss)
					{
						flag = true;
					}
					Player.keyBoss = num4;
					Player.maxKeyboss = msg.reader().readByte();
					if (flag)
					{
						Interface_Game.tickeffShowMoney = 40;
						Interface_Game.typeShowMoney = 1;
					}
					break;
				}
				case 99:
					Player.Chestbeli = num;
					Player.Chestgem = num2;
					break;
				}
				int num5 = msg.reader().readInt();
				if (num5 != GameScreen.player.vnd && GameCanvas.tabMarketScr != null && GameCanvas.currentScreen == GameCanvas.tabMarketScr)
				{
					_ = string.Empty;
					TabScreen.addEffectNumImage((GameScreen.player.vnd <= num5) ? ("+" + (num5 - GameScreen.player.vnd)) : ("-" + (GameScreen.player.vnd - num5)), MainTab.xMoney - 20, MainTab.yMoney + 36, 10, AvMain.fraMoney, 7);
				}
				GameScreen.player.vnd = num5;
				int num6 = msg.reader().readInt();
				if (num6 != GameScreen.player.bua && GameCanvas.currentScreen == AuctionScreen.instance)
				{
					_ = string.Empty;
					AuctionScreen.addEffectNumImage((GameScreen.player.bua <= num6) ? ("+" + (num6 - GameScreen.player.bua)) : ("-" + (GameScreen.player.bua - num6)), MainTab.xMoney - 10, MainTab.yMoney + 36, 10, AvMain.fraMoney, 8);
				}
				GameScreen.player.bua = num6;
				int num7 = msg.reader().readInt();
				if (num7 != GameScreen.player.diemNap && GameCanvas.currentScreen == AuctionScreen.instance)
				{
					string empty = string.Empty;
					empty = ((GameScreen.player.diemNap <= num7) ? ("+" + (num7 - GameScreen.player.diemNap)) : ("-" + (GameScreen.player.diemNap - num7)));
					mSystem.outz("Add effect num diemNap " + empty);
					TabScreen.addEffectNumImage(empty, MainTab.xMoney - 20, MainTab.yMoney + 36, 10, AvMain.fraMoney, 9);
				}
				GameScreen.player.diemNap = num7;
				break;
			}
			case 6:
				switch (typeInvenOrChest)
				{
				case 100:
					Player.maxInventory = msg.reader().readShort();
					GameCanvas.tabAllScr.tabCurrent.resize(Player.maxInventory);
					break;
				case 99:
				{
					Player.maxChest = msg.reader().readShort();
					if (GameCanvas.currentScreen != GameCanvas.tabShopScr)
					{
						break;
					}
					for (int i = 0; i < GameCanvas.tabShopScr.vecTabs.size(); i++)
					{
						MainTab mainTab = (MainTab)GameCanvas.tabShopScr.vecTabs.elementAt(i);
						if (mainTab.indexIconTab == 7)
						{
							mainTab.maxSize = Player.maxChest;
						}
					}
					break;
				}
				}
				break;
			}
			TabScreen.isRefresh = true;
			if (typeInvenOrChest == 100 && Player.isFullInven && Player.vecInventory.size() < Player.maxInventory)
			{
				Player.isFullInven = false;
			}
			if (MainTabShop.isSortInven && (GameCanvas.currentScreen == GameCanvas.tabAllScr || GameCanvas.currentScreen == GameCanvas.tabShopScr))
			{
				vec = MainItem.SortVecItem(vec);
				MainTabShop.isSortInven = false;
			}
		}
		catch (Exception)
		{
		}
	}

	public void updateMoney(long xu, int luong, short ticket)
	{
		string empty = string.Empty;
		if (GameScreen.player.beli != xu)
		{
			empty = ((GameScreen.player.beli <= xu) ? ("+" + (xu - GameScreen.player.beli)) : ("-" + (GameScreen.player.beli - xu)));
			if (GameCanvas.currentScreen == GameCanvas.tabAllScr || GameCanvas.currentScreen == GameCanvas.tabShopScr)
			{
				TabScreen.addEffectNumImage(empty, MainTab.xMoney - 20, MainTab.yMoney + 10, 10, AvMain.fraMoney, 0);
			}
			else if (GameCanvas.currentScreen == GameCanvas.gameScr)
			{
				GameScreen.addEffectNumImage(empty, GameScreen.player.x, GameScreen.player.y - GameScreen.player.hOne, 10, AvMain.fraMoney, 0);
			}
		}
		if (GameScreen.player.gem != luong)
		{
			empty = ((GameScreen.player.gem <= luong) ? ("+" + (luong - GameScreen.player.gem)) : ("-" + (GameScreen.player.gem - luong)));
			if (GameCanvas.currentScreen == GameCanvas.tabAllScr || GameCanvas.currentScreen == GameCanvas.tabShopScr)
			{
				TabScreen.addEffectNumImage(empty, MainTab.xMoney - 20, MainTab.yMoney + 23, 10, AvMain.fraMoney, 1);
			}
			else if (GameCanvas.currentScreen == GameCanvas.gameScr)
			{
				GameScreen.addEffectNumImage(empty, GameScreen.player.x, GameScreen.player.y - GameScreen.player.hOne, 10, AvMain.fraMoney, 1);
				Interface_Game.tickeffShowMoney = 40;
				Interface_Game.typeShowMoney = 0;
			}
			else if (GameCanvas.currentScreen == QuayOcSenScreen.instance)
			{
				QuayOcSenScreen.instance.addEffectNumImage(empty, MainTab.xMoney - 10, MainTab.yMoney + 36, 10, AvMain.fraMoney, 1);
			}
		}
		if (Player.ticket > ticket && GameCanvas.currentScreen == GameCanvas.gameScr)
		{
			GameScreen.addEffectNumImage("-" + (Player.ticket - ticket), GameScreen.player.x, GameScreen.player.y - GameScreen.player.hOne, 10, AvMain.fraMoney, 2);
			Interface_Game.tickeffShowMoney = 40;
			Interface_Game.typeShowMoney = 1;
		}
	}

	public Potion readUpdatePotion(Message msg, bool isShop, sbyte cat)
	{
		Potion potion = null;
		try
		{
			short num = msg.reader().readShort();
			short num2 = msg.reader().readShort();
			Potion potion2 = null;
			if (cat == 4)
			{
				potion2 = Potion.getTemplatePotion(num);
			}
			if (cat == 8)
			{
				potion2 = (Potion)MainItem.hashPotionClan.get(string.Empty + num);
			}
			potion = null;
			if (potion2.isUpdateTem)
			{
				potion = new Potion(cat, num, potion2.idIcon, potion2.name, potion2.isTrade);
				potion.setdata(potion2.price, potion2.priceRuby, potion2.value, potion2.timeActive, potion2.timeDelayPotion, potion2.Hp_Mp_Other, potion2.nameUse);
				potion.numPotion = num2;
				potion.isShop = isShop;
				if (cat == 8)
				{
					potion.info = potion2.info;
				}
				if (!potion.getInfoPotion(potion2.indexInfoPotion))
				{
					Potion.vecPotionDonotInfo.addElement(potion);
				}
			}
			else
			{
				potion = new Potion(cat, num, num2, isShop);
				Potion.vecPotionDonotData.addElement(potion);
			}
			if (num == 232)
			{
				LuckyScreen.isUpdateVe = true;
			}
			if (num == 441)
			{
				LuckyScreen.isUpdateVe = true;
			}
			Hotkey.checkUpdatePotion(potion);
		}
		catch (Exception)
		{
		}
		return potion;
	}

	public ItemBoat readUpdateItemBoat(Message msg)
	{
		ItemBoat result = null;
		try
		{
			sbyte iD = msg.reader().readByte();
			string name = msg.reader().readUTF();
			sbyte type = msg.reader().readByte();
			short idImage = msg.reader().readShort();
			short iDIcon = msg.reader().readShort();
			result = new ItemBoat(iD, iDIcon, idImage, name, type);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public ItemHair readUpdateItemHair(Message msg, sbyte typeObj)
	{
		ItemHair result = null;
		try
		{
			sbyte iD = msg.reader().readByte();
			string name = msg.reader().readUTF();
			msg.reader().readByte();
			short iDIcon = msg.reader().readShort();
			msg.reader().readShort();
			result = new ItemHair(iD, iDIcon, name, typeObj);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public ItemFashion readUpdateItemFashion(Message msg)
	{
		ItemFashion itemFashion = null;
		try
		{
			sbyte iD = msg.reader().readByte();
			string name = msg.reader().readUTF();
			string info = msg.reader().readUTF();
			short iDIcon = msg.reader().readShort();
			short[] array = new short[msg.reader().readByte()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = msg.reader().readShort();
			}
			itemFashion = new ItemFashion(iD, iDIcon, name, info, array);
			itemFashion.isShop = true;
			itemFashion.setInfoPotion(itemFashion.info);
		}
		catch (Exception)
		{
		}
		return itemFashion;
	}

	public MainMaterial readUpdateMaterial(Message msg, bool isShop)
	{
		MainMaterial mainMaterial = null;
		try
		{
			sbyte id = msg.reader().readByte();
			short num = msg.reader().readShort();
			MainMaterial mainMaterial2 = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + id);
			mainMaterial = new MainMaterial(7, id, mainMaterial2.name, mainMaterial2.typeMaterial, (sbyte)mainMaterial2.idIcon, num, mainMaterial2.price, mainMaterial2.priceRuby, mainMaterial2.isTrade);
			mainMaterial.isShop = isShop;
			mainMaterial.setInfoPotion(mainMaterial2.info);
		}
		catch (Exception)
		{
		}
		return mainMaterial;
	}

	public Item readUpdateItem(Message msg, bool isShop)
	{
		Item item = null;
		try
		{
			short num = msg.reader().readShort();
			string name = msg.reader().readUTF();
			sbyte charClass = msg.reader().readByte();
			sbyte typeEquip = msg.reader().readByte();
			short idIcon = msg.reader().readShort();
			short lv = msg.reader().readShort();
			sbyte lvUp = msg.reader().readByte();
			sbyte colorName = msg.reader().readByte();
			sbyte isTrade = msg.reader().readByte();
			sbyte typelock = msg.reader().readByte();
			sbyte numHoleDaDuc = msg.reader().readByte();
			int timeUse = msg.reader().readInt();
			short valueChetac = msg.reader().readShort();
			sbyte isHoanMy = msg.reader().readByte();
			sbyte valueKichAn = msg.reader().readByte();
			sbyte b = msg.reader().readByte();
			MainInfoItem[] array = null;
			MainInfoItem[] array2 = new MainInfoItem[b];
			for (int i = 0; i < b; i++)
			{
				sbyte id = msg.reader().readByte();
				short value = msg.reader().readShort();
				array2[i] = new MainInfoItem(id, value);
				array2[i].colorMain = infoShow.HARDCODE_INFO_CO_BAN;
			}
			sbyte b2 = msg.reader().readByte();
			array = new MainInfoItem[b + b2];
			for (int j = 0; j < b; j++)
			{
				array[j] = new MainInfoItem(array2[j].id, array2[j].value, array2[j].colorMain);
			}
			for (int k = 0; k < b2; k++)
			{
				sbyte b3 = msg.reader().readByte();
				short value2 = msg.reader().readShort();
				array[k + b] = new MainInfoItem(b3, value2);
				if (MainItem.mNameAttributes[b3].color != 0)
				{
					array[k + b].colorMain = MainItem.mNameAttributes[b3].color;
				}
				else
				{
					array[k + b].colorMain = 4;
				}
			}
			sbyte numLoKham = msg.reader().readByte();
			sbyte b4 = msg.reader().readByte();
			short[] array3 = new short[b4];
			for (int l = 0; l < b4; l++)
			{
				array3[l] = msg.reader().readShort();
			}
			item = new Item(3, num, idIcon, name, isTrade);
			item.setDataItem(lv, charClass, colorName, timeUse, typeEquip, lvUp, numLoKham, array3, valueChetac, isHoanMy, valueKichAn);
			item.IDMarket = num;
			item.typelock = typelock;
			if (item.typelock == 1)
			{
				item.isTrade = 1;
			}
			item.numHoleDaDuc = numHoleDaDuc;
			item.setInfoItem(array, b);
			return item;
		}
		catch (Exception)
		{
			return item;
		}
	}

	public Quest_Potion readUpdateQuestPotion(Message msg)
	{
		Quest_Potion quest_Potion = null;
		try
		{
			short iD = msg.reader().readShort();
			string name = msg.reader().readUTF();
			short numPotion = msg.reader().readShort();
			quest_Potion = new Quest_Potion(5, iD, name);
			quest_Potion.numPotion = numPotion;
			quest_Potion.setInfoPotion(T.infoQuestPotion);
		}
		catch (Exception)
		{
		}
		return quest_Potion;
	}

	public void update_Inventory(Message msg)
	{
		update_InVen_Or_Chest(msg, Player.vecInventory, 100);
	}

	public void update_Chest(Message msg)
	{
		update_InVen_Or_Chest(msg, Player.vecChest, 99);
		mSystem.outz("update_Chest size vec = " + Player.vecChest.size());
	}

	public void update_Inven_Clan(Message msg)
	{
		update_InVen_Or_Chest(msg, Player.vecInvenClan, 110);
	}

	public void use_Potion(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			MainItem itemVec = MainItem.getItemVec(4, num, Player.vecInventory);
			if (itemVec == null)
			{
				return;
			}
			short num2 = (itemVec.numPotion = msg.reader().readShort());
			if (itemVec.Hp_Mp_Other == 1 || itemVec.Hp_Mp_Other == 2)
			{
				GameScreen.interfaceGame.addEffCurrent(itemVec);
			}
			if (num2 > 0)
			{
				return;
			}
			GameScreen.player.hpPoi = null;
			GameScreen.player.mpPoi = null;
			Player.vecInventory.removeElement(itemVec);
			if (Player.isFullInven && Player.vecInventory.size() < Player.maxInventory)
			{
				Player.isFullInven = false;
			}
			for (int i = 0; i < Player.hotkeyPlayer.Length; i++)
			{
				for (int j = 0; j < Player.hotkeyPlayer[i].Length; j++)
				{
					Hotkey hotkey = Player.hotkeyPlayer[i][j];
					if (hotkey.itemcur != null && hotkey.itemcur.ID == num)
					{
						hotkey.itemcur = null;
					}
				}
			}
			if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
			{
				GameCanvas.tabInven.getItemCurNew();
			}
		}
		catch (Exception)
		{
		}
	}

	public void update_MP_HP(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(num, b);
			if (mainObject == null || mainObject.returnAction())
			{
				return;
			}
			mainObject.maxHp = msg.reader().readInt();
			int hp = msg.reader().readInt();
			int num2 = msg.reader().readInt();
			if (num2 != 0)
			{
				string content = string.Empty + num2;
				if (num2 > 0)
				{
					content = "+" + num2;
				}
				if (mainObject == GameScreen.player)
				{
					GameScreen.addEffectNum(content, mainObject.x - 12, mainObject.y - mainObject.hOne / 4 * 3 - lechYNum, 3);
				}
			}
			mainObject.Hp = hp;
			if (mainObject.Action == 4 && mainObject.Hp > 0)
			{
				mainObject.Reveive();
			}
			if (b == 0)
			{
				for (int i = 0; i < Player.vecParty.size(); i++)
				{
					InfoMemList infoMemList = (InfoMemList)Player.vecParty.elementAt(i);
					if (infoMemList.id == num && b == 0)
					{
						infoMemList.updateHP(hp, mainObject.maxHp, mainObject.Lv);
					}
				}
			}
			if (b != 1)
			{
				mainObject.maxMp = msg.reader().readInt();
				int mp = msg.reader().readInt();
				int num3 = msg.reader().readInt();
				if (num3 != 0)
				{
					string content2 = string.Empty + num3;
					if (num3 > 0)
					{
						content2 = "+" + num3;
					}
					if (mainObject == GameScreen.player)
					{
						GameScreen.addEffectNum(content2, mainObject.x + 12, mainObject.y - mainObject.hOne / 4 * 3 - lechYNum, 4);
					}
				}
				mainObject.Mp = mp;
			}
			lechYNum += 10;
			if (lechYNum > 20)
			{
				lechYNum = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	public void update_PK(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			sbyte typePirate = msg.reader().readByte();
			sbyte isDonotShowHat = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, 0);
			if (mainObject == null || mainObject.returnAction())
			{
				return;
			}
			sbyte typePK = mainObject.typePK;
			mainObject.typePK = b;
			mainObject.typePirate = typePirate;
			mainObject.isDonotShowHat = isDonotShowHat;
			if (mainObject == GameScreen.player)
			{
				GameCanvas.gameScr.cmdSetDosat.caption = T.Dosat;
				if (b == 0)
				{
					GameCanvas.gameScr.cmdSetDosat.caption = T.tatDosat;
				}
				if (LoadMap.specMap == 1)
				{
					GlobalService.gI().Set_PK(b, 1);
				}
				if (typePK == 0)
				{
					Interface_Game.addInfoPlayerNormal(T.tatdosat, mFont.tahoma_7_yellow);
				}
				else if (b == 0)
				{
					Interface_Game.addInfoPlayerNormal(T.batdosat, mFont.tahoma_7_yellow);
				}
			}
			msg.reader().readShort();
			sbyte isDonotShowWeaponF = msg.reader().readByte();
			mainObject.isDonotShowWeaponF = isDonotShowWeaponF;
			sbyte typeColor = msg.reader().readByte();
			mainObject.typeColor7 = typeColor;
		}
		catch (Exception)
		{
		}
	}

	public void update_List_Pk(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			Player.msattam = new short[b];
			for (int i = 0; i < b; i++)
			{
				Player.msattam[i] = msg.reader().readShort();
			}
		}
		catch (Exception)
		{
		}
	}

	public void Effect_Obj(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			short num = msg.reader().readShort();
			sbyte b2 = msg.reader().readByte();
			short num2 = msg.reader().readShort();
			short num3 = 0;
			sbyte b3 = 0;
			short num4 = -1;
			MainObject mainObject = MainObject.get_Object(num, b2);
			if ((mainObject == null || mainObject.returnAction()) && b < 16 && b > 18)
			{
				return;
			}
			switch (b)
			{
			case 25:
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr || GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameScreen.addEffectEnd_ObjTo(178, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			case 0:
				if (mainObject == GameScreen.player)
				{
					setHelp();
					setUpLv();
				}
				GameScreen.addEffectEnd_ObjTo(28, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, (sbyte)mainObject.Dir, mainObject);
				break;
			case 1:
				if (mainObject == GameScreen.player && (GameCanvas.currentScreen == GameCanvas.tabAllScr || GameCanvas.currentScreen == GameCanvas.tabShopScr))
				{
					GameCanvas.gameScr.Show();
				}
				mainObject.addEffBuff(3, 165, 0);
				break;
			case 2:
			{
				short x = (short)(msg.reader().readShort() * 24);
				short y = (short)(msg.reader().readShort() * 24);
				mainObject.x = x;
				mainObject.y = y;
				mainObject.addEffBuff(3, 17, num2);
				break;
			}
			case 3:
				if (mainObject != null)
				{
					GameScreen.addEffectEnd_ObjTo(46, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, (sbyte)mainObject.Dir, mainObject);
				}
				break;
			case 4:
				num3 = msg.reader().readShort();
				b3 = msg.reader().readByte();
				GameScreen.addEffectEnd_Time(96, 0, mainObject.x, mainObject.y, num3, b3, (sbyte)mainObject.Dir, mainObject, num2);
				break;
			case 5:
				num3 = msg.reader().readShort();
				b3 = msg.reader().readByte();
				GameScreen.addEffectEnd_Time(97, 0, mainObject.x, mainObject.y, num3, b3, (sbyte)mainObject.Dir, mainObject, num2);
				break;
			case 6:
			{
				num3 = msg.reader().readShort();
				b3 = msg.reader().readByte();
				MainObject mainObject2 = MainObject.get_Object(num3, b3);
				if (mainObject2 != null)
				{
					GameScreen.addEffectEnd_ObjTo(101, 0, mainObject2.x, mainObject2.y, mainObject2.ID, mainObject2.typeObject, 0, mainObject);
				}
				mainObject.setPetActionFire();
				break;
			}
			case 7:
				GameScreen.addEffectEnd_ObjTo(101, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			case 8:
			{
				sbyte subType = msg.reader().readByte();
				GameScreen.addEffectEnd_ObjTo(104, subType, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			}
			case 9:
			{
				sbyte subType2 = msg.reader().readByte();
				GameScreen.addEffectEnd_ObjTo(105, subType2, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			}
			case 10:
				GameScreen.addEffectEnd_ObjTo(106, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			case 11:
				GameScreen.addEffectEnd(115, 0, mainObject.x, mainObject.y, 0, 0, mainObject);
				break;
			case 13:
			{
				num3 = msg.reader().readShort();
				b3 = msg.reader().readByte();
				short num6 = msg.reader().readShort();
				int subType4 = 10;
				switch (num6)
				{
				case 180:
					subType4 = 10;
					break;
				case 181:
					subType4 = 11;
					break;
				case 182:
					subType4 = 12;
					break;
				case 193:
					subType4 = 13;
					break;
				case 233:
					subType4 = 14;
					break;
				case 234:
					subType4 = 15;
					break;
				case 235:
					subType4 = 16;
					break;
				case 577:
					subType4 = 17;
					break;
				case 611:
					subType4 = 24;
					break;
				}
				mSystem.outz("con song fali >>>" + num6 + " frame " + subType4);
				GameScreen.addEffectEnd_ObjTo(127, subType4, mainObject.x, mainObject.y, num3, b3, 0, mainObject);
				break;
			}
			case 12:
			{
				num3 = msg.reader().readShort();
				b3 = msg.reader().readByte();
				short num5 = msg.reader().readShort();
				int subType3 = 0;
				switch (num5)
				{
				case 180:
					subType3 = 0;
					break;
				case 181:
					subType3 = 1;
					break;
				case 182:
					subType3 = 2;
					break;
				case 193:
					subType3 = 3;
					break;
				case 233:
					subType3 = 4;
					break;
				case 234:
					subType3 = 5;
					break;
				case 235:
					subType3 = 6;
					break;
				case 577:
					subType3 = 7;
					break;
				case 611:
					subType3 = 24;
					break;
				}
				mSystem.outz("con song >>>" + num5 + " frame " + subType3);
				GameScreen.addEffectEnd_ObjTo(127, subType3, mainObject.x, mainObject.y, num3, b3, 0, mainObject);
				break;
			}
			case 14:
				GameScreen.addEffectEnd_Time(130, 0, mainObject.x, mainObject.y, num, b2, 0, mainObject, num2);
				break;
			case 15:
				GameScreen.addEffectEnd_Time(131, 0, mainObject.x, mainObject.y, num, b2, 0, mainObject, num2);
				break;
			case 16:
			{
				mSystem.outz("readMessage Eff_choang");
				num4 = msg.reader().readShort();
				for (int k = 0; k < GameScreen.vecPlayers.size(); k++)
				{
					MainObject mainObject5 = (MainObject)GameScreen.vecPlayers.elementAt(k);
					if (mainObject5.typeObject == 0 && (mainObject5.clan == null || num4 != mainObject5.clan.ID))
					{
						mainObject5.addEffSpec(1, num2);
						GameScreen.addEffectEnd_Time(149, 0, mainObject5.x, mainObject5.y, mainObject5.ID, mainObject5.typeObject, 0, mainObject5, num2);
					}
				}
				break;
			}
			case 17:
			{
				num4 = msg.reader().readShort();
				for (int j = 0; j < GameScreen.vecPlayers.size(); j++)
				{
					MainObject mainObject4 = (MainObject)GameScreen.vecPlayers.elementAt(j);
					if (mainObject4.typeObject == 0 && (mainObject4.clan == null || mainObject4.clan.ID != num4))
					{
						GameScreen.addEffectEnd_Time(151, 0, mainObject4.x, mainObject4.y, mainObject4.ID, mainObject4.typeObject, 0, mainObject4, num2);
						GameScreen.addEffectEnd_Time(150, 0, mainObject4.x, mainObject4.y, mainObject4.ID, mainObject4.typeObject, 0, mainObject4, num2 / 10);
					}
				}
				break;
			}
			case 18:
			{
				num4 = msg.reader().readShort();
				for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
				{
					MainObject mainObject3 = (MainObject)GameScreen.vecPlayers.elementAt(i);
					if (mainObject3.typeObject == 0 && mainObject3.clan != null && mainObject3.clan.ID == num4)
					{
						GameScreen.addEffectEnd_Time(151, 1, mainObject3.x, mainObject3.y, mainObject3.ID, mainObject3.typeObject, 0, mainObject3, num2);
						mainObject3.addEffSpec(11, num2);
					}
				}
				break;
			}
			case 19:
				GameScreen.addEffectEnd(80, 0, mainObject.x, mainObject.y - mainObject.hOne / 2, 0, null);
				GameScreen.addEffectEnd_Time(152, 0, mainObject.x, mainObject.y, num, b2, 0, mainObject, num2 / 10);
				break;
			case 20:
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr || GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameScreen.addEffectEnd_ObjTo(153, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			case 21:
				GameScreen.addEffectEnd_Time(156, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject, num2);
				break;
			case 23:
				if (GameCanvas.currentScreen == GameCanvas.tabAllScr || GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameScreen.addEffectEnd_ObjTo(158, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			case 22:
				GameScreen.addEffectEnd_ObjTo(159, 0, mainObject.x, mainObject.y, mainObject.ID, mainObject.typeObject, (sbyte)mainObject.Dir, mainObject);
				break;
			case 24:
				GameScreen.addEffectEnd_ObjTo(162, 0, mainObject.x, mainObject.y - 2, mainObject.ID, mainObject.typeObject, 0, mainObject);
				break;
			case 26:
				if (mainObject != null)
				{
					short time = (short)(num2 * 100);
					GameScreen.addEffectBuffNew(mainObject, 2060, time);
				}
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	private void setUpLv()
	{
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem.typeObject != 3 || mainItem.Lv_RQ != GameScreen.player.Lv)
			{
				continue;
			}
			for (int j = 0; j < mainItem.vecInfo.size(); j++)
			{
				infoShow infoShow2 = (infoShow)mainItem.vecInfo.elementAt(j);
				if (infoShow2.colorMain == infoShow.HARDCODE_CHECK_LVRQ)
				{
					infoShow2.color = 4;
				}
			}
		}
	}

	private void setHelp()
	{
		if (GameScreen.indexHelp == 13)
		{
			GameScreen.addHelp(13, 0);
		}
		else if (GameScreen.player.Lv == 20)
		{
			GameScreen.addHelp(19, 0);
		}
	}

	public void List_NPC(Message msg)
	{
		GameScreen.RemoveAllNPC();
		try
		{
			sbyte b = msg.reader().readByte();
			bool flag = false;
			for (int i = 0; i < b; i++)
			{
				short iDItem = msg.reader().readShort();
				string name = msg.reader().readUTF();
				string namegt = msg.reader().readUTF();
				string chatNPC = msg.reader().readUTF();
				short x = msg.reader().readShort();
				short y = msg.reader().readShort();
				sbyte b2 = msg.reader().readByte();
				sbyte typeIconNPC = msg.reader().readByte();
				sbyte wBlock = msg.reader().readByte();
				sbyte hBlock = msg.reader().readByte();
				sbyte b3 = msg.reader().readByte();
				NPC nPC = new NPC(name, namegt, iDItem, x, y, wBlock, hBlock, b3);
				nPC.isPerson = b2;
				nPC.chatNPC = chatNPC;
				nPC.typeIconNPC = typeIconNPC;
				if (b3 == 0)
				{
					sbyte iDImage = msg.reader().readByte();
					sbyte nFrame = msg.reader().readByte();
					nPC.setDataFrame(iDImage, nFrame);
				}
				else
				{
					nPC.sethead(msg.reader().readShort());
					nPC.sethair(msg.reader().readShort());
					nPC.hOne = 52;
					nPC.wOne = 26;
					if (LoadMap.specMap == 4)
					{
						nPC.wOne = 100;
					}
					sbyte b4 = msg.reader().readByte();
					short[] array = new short[b4];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = -1;
					}
					for (int k = 0; k < b4; k++)
					{
						if (msg.reader().readByte() == 1)
						{
							array[k] = msg.reader().readShort();
						}
					}
					nPC.setWearing(array);
				}
				nPC.isRemove = false;
				nPC.isInfo = true;
				GameScreen.addPlayer(nPC);
				nPC.setTypeQuest();
				if (b2 == 98 && !flag)
				{
					GlobalService.gI().get_Info_Clan_Dao();
					flag = true;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Shop_IconClanVip(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz("Shop huy hieu action = " + b);
			switch (b)
			{
			case 0:
			{
				string name2 = msg.reader().readUTF();
				short num4 = msg.reader().readShort();
				mVector mVector4 = new mVector();
				for (int k = 0; k < num4; k++)
				{
					short id3 = msg.reader().readShort();
					string name3 = msg.reader().readUTF();
					string info2 = msg.reader().readUTF();
					short idImage = msg.reader().readShort();
					string text = msg.reader().readUTF();
					Potion potion2 = new Potion(id3, idImage, name3, info2, 0);
					sbyte b4 = msg.reader().readByte();
					for (int l = 0; l < b4; l++)
					{
						sbyte id4 = msg.reader().readByte();
						short value2 = msg.reader().readShort();
						potion2.addInfo(id4, value2, infoShow.HARDCODE_INFO_CO_BAN, 1);
					}
					if (potion2 != null)
					{
						if (text != string.Empty)
						{
							potion2.addInfo(text, 5);
						}
						if (potion2.price > 0)
						{
							potion2.addInfoFrist(T.price + " " + potion2.price + " " + T.bery, 5);
						}
						else if (potion2.priceRuby > 0)
						{
							potion2.addInfoFrist(T.price + " " + potion2.priceRuby + " " + T.ruby, 5);
						}
						mVector4.addElement(potion2);
					}
				}
				TabShop tabShop = null;
				GameCanvas.tabShopScr = new TabScreen(MainTab.xTab, 0);
				mVector mVector5 = new mVector();
				GameCanvas.tabShopScr.isShopClan = true;
				GameCanvas.tabInven.setTypeInven(1);
				tabShop = new TabShop(name2, mVector4, 107, MainTab.xTab);
				tabShop.isSelect = false;
				mVector5.addElement(tabShop);
				GameCanvas.tabShopScr.addVecTab(mVector5);
				GameCanvas.tabShopScr.idSelect = 0;
				GameCanvas.tabShopScr.Show(GameCanvas.gameScr);
				GameCanvas.tabShopScr.typeCurrent = 1;
				GameCanvas.tabShopScr.setTabSelect();
				break;
			}
			case 1:
			{
				short num3 = msg.reader().readShort();
				mVector mVector2 = new mVector();
				for (int i = 0; i < num3; i++)
				{
					short iD = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					short iDIcon = msg.reader().readShort();
					ItemHuyHieu itemHuyHieu = new ItemHuyHieu(iD, iDIcon, name, info);
					if (msg.reader().readByte() == 1)
					{
						itemHuyHieu.addInfoFrist(T.daTrangBi, 1);
						itemHuyHieu.colorName = 1;
					}
					sbyte b3 = msg.reader().readByte();
					for (int j = 0; j < b3; j++)
					{
						sbyte id2 = msg.reader().readByte();
						short value = msg.reader().readShort();
						itemHuyHieu.addInfo(id2, value, infoShow.HARDCODE_INFO_CO_BAN, 1);
					}
					mVector2.addElement(itemHuyHieu);
				}
				GameCanvas.tabShopScr = new TabScreen(MainTab.xTab, 0);
				mVector mVector3 = new mVector();
				GameCanvas.tabShopScr.isShopClan = true;
				GameCanvas.tabInvenClan = new TabInventory(T.khoHuyHieu, mVector2, 6, MainTab.xTab);
				GameCanvas.tabInvenClan.initCmd();
				mVector3.addElement(GameCanvas.tabInvenClan);
				GameCanvas.tabShopScr.addVecTab(mVector3);
				GameCanvas.tabShopScr.idSelect = 0;
				GameCanvas.tabShopScr.Show(GameCanvas.ClanScr);
				GameCanvas.tabShopScr.typeCurrent = 1;
				GameCanvas.tabShopScr.setTabSelect();
				break;
			}
			case 2:
			{
				short idIcon2 = msg.reader().readShort();
				mSystem.outz("action use idIcon " + idIcon2);
				GameCanvas.tabInvenClan.Use(idIcon2);
				break;
			}
			case 3:
			{
				sbyte b2 = msg.reader().readByte();
				mSystem.outz("Mo shop huy hieu type = " + b2);
				switch (b2)
				{
				case 0:
				{
					short id = msg.reader().readShort();
					short num2 = msg.reader().readShort();
					sbyte cat = msg.reader().readByte();
					short idIcon = msg.reader().readShort();
					mSystem.outz("id num cat icon = " + id + " , " + num2 + " , " + cat + " , " + idIcon);
					Potion potion = new Potion(cat, id, num2, isShop: false);
					potion.idIcon = idIcon;
					HuyHieuClanScreen.instance = new HuyHieuClanScreen();
					HuyHieuClanScreen.instance.potionQuay = potion;
					HuyHieuClanScreen.instance.Show(GameCanvas.gameScr);
					break;
				}
				case 1:
				{
					Interface_Game.isPaintInfoServer = false;
					HuyHieuClanScreen.instance.potionQuay.numPotion = msg.reader().readShort();
					short num = msg.reader().readShort();
					mSystem.outz("xu hanh trinh con lai = " + HuyHieuClanScreen.instance.potionQuay.numPotion);
					if (num == -1)
					{
						HuyHieuClanScreen.instance.isThanhCong = false;
						mSound.playSound(48, mSound.volumeSound);
						HuyHieuClanScreen.instance.StepQuaySo = 1;
						HuyHieuClanScreen.instance.tickAction = 0;
					}
					else
					{
						Potion potionNhan = new Potion(msg.reader().readByte(), -1, num, string.Empty, 0);
						mSystem.outz("potionNhan type " + b2 + " icon " + num);
						HuyHieuClanScreen.instance.potionNhan = potionNhan;
						HuyHieuClanScreen.instance.isThanhCong = true;
						mSound.playSound(48, mSound.volumeSound);
						HuyHieuClanScreen.instance.StepQuaySo = 1;
						HuyHieuClanScreen.instance.tickAction = 0;
					}
					break;
				}
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Shop_NPC(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			string text = msg.reader().readUTF();
			sbyte b2 = msg.reader().readByte();
			short num = msg.reader().readShort();
			mSystem.outz("nhan shop day typeShop=" + b + " nameshop=" + text + " typeitem=" + b2 + "  size=" + num);
			mVector mVector2 = new mVector();
			switch (b2)
			{
			case 4:
			case 8:
			{
				for (int m = 0; m < num; m++)
				{
					Potion potion2 = readUpdatePotion(msg, isShop: true, b2);
					if (potion2 != null)
					{
						if (potion2.price > 0)
						{
							potion2.addInfoFrist(T.price + " " + potion2.price + " " + T.bery, 5);
						}
						else if (potion2.priceRuby > 0)
						{
							potion2.addInfoFrist(T.price + " " + potion2.priceRuby + " " + T.ruby, 5);
						}
						mVector2.addElement(potion2);
					}
				}
				break;
			}
			case 3:
			{
				for (int num2 = 0; num2 < num; num2++)
				{
					Item item = readUpdateItem(msg, isShop: true);
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					if (item != null)
					{
						item.price = price;
						item.addInfoFrist(T.price + " " + item.price + " " + ((b3 != 0) ? T.ruby : T.bery), 5);
						mVector2.addElement(item);
					}
				}
				break;
			}
			case 7:
			{
				for (int num6 = 0; num6 < num; num6++)
				{
					MainMaterial mainMaterial = readUpdateMaterial(msg, isShop: true);
					if (mainMaterial != null)
					{
						if (mainMaterial.price > 0)
						{
							mainMaterial.addInfoFrist(T.price + " " + mainMaterial.price + " " + T.bery, 5);
						}
						else
						{
							mainMaterial.addInfoFrist(T.price + " " + mainMaterial.priceRuby + " " + T.ruby, 5);
						}
						mVector2.addElement(mainMaterial);
					}
				}
				break;
			}
			case 102:
			{
				for (int j = 0; j < num; j++)
				{
					ItemBoat itemBoat = readUpdateItemBoat(msg);
					itemBoat.price = msg.reader().readInt();
					itemBoat.priceRuby = msg.reader().readShort();
					if (itemBoat == null)
					{
						continue;
					}
					if (itemBoat.price > 0)
					{
						itemBoat.addInfoFrist(T.price + " " + itemBoat.price + " " + T.bery, 5);
					}
					else if (itemBoat.priceRuby > 0)
					{
						itemBoat.addInfoFrist(T.price + " " + itemBoat.priceRuby + " " + T.ruby, 5);
					}
					else
					{
						bool flag = false;
						if (GameScreen.player.myBoat != null)
						{
							for (int k = 0; k < GameScreen.player.myBoat.Length; k++)
							{
								if (k == itemBoat.typeBoat && itemBoat.idPart == GameScreen.player.myBoat[k])
								{
									itemBoat.addInfoFrist(T.daTrangBi, 4);
									itemBoat.colorName = 1;
									flag = true;
								}
							}
						}
						if (!flag)
						{
							itemBoat.addInfoFrist(T.dasuhuu, 1);
							itemBoat.colorName = 1;
						}
					}
					mVector2.addElement(itemBoat);
				}
				GlobalService.gI().Update_Part_Boat();
				break;
			}
			case 103:
			case 108:
			{
				for (int num3 = 0; num3 < num; num3++)
				{
					int num4 = GameScreen.player.hair;
					if (b2 == 108)
					{
						num4 = GameScreen.player.head;
					}
					ItemHair itemHair = readUpdateItemHair(msg, b2);
					if (MainObject.dataInfoHair != null)
					{
						for (int num5 = 0; num5 < MainObject.dataInfoHair.Length; num5++)
						{
							if (itemHair.ID == MainObject.dataInfoHair[num5].id)
							{
								itemHair.setInfoPotion(MainObject.dataInfoHair[num5].info);
								break;
							}
						}
					}
					itemHair.price = msg.reader().readInt();
					itemHair.priceRuby = msg.reader().readShort();
					if (itemHair != null)
					{
						if (itemHair.price > 0)
						{
							itemHair.addInfoFrist(T.price + " " + itemHair.price + " " + T.bery, 5);
						}
						else if (itemHair.priceRuby > 0)
						{
							itemHair.addInfoFrist(T.price + " " + itemHair.priceRuby + " " + T.ruby, 5);
						}
						else if (itemHair.idIcon == num4)
						{
							itemHair.addInfoFrist(T.daTrangBi, 4);
							itemHair.colorName = 4;
						}
						else
						{
							itemHair.addInfoFrist(T.dasuhuu, 1);
							itemHair.colorName = 1;
						}
						mVector2.addElement(itemHair);
					}
				}
				break;
			}
			case 105:
			{
				for (int n = 0; n < num; n++)
				{
					ItemFashion itemFashion = readUpdateItemFashion(msg);
					itemFashion.price = msg.reader().readInt();
					itemFashion.priceRuby = msg.reader().readShort();
					itemFashion.LvUpgrade = msg.reader().readByte();
					if (itemFashion != null)
					{
						if (itemFashion.price > 0)
						{
							itemFashion.addInfoFrist(T.price + " " + itemFashion.price + " " + T.bery, 5);
						}
						else if (itemFashion.priceRuby > 0)
						{
							itemFashion.addInfoFrist(T.price + " " + itemFashion.priceRuby + " " + T.ruby, 5);
						}
						else if (itemFashion.price == -2)
						{
							itemFashion.addInfoFrist(T.gift, 5);
						}
						else if (itemFashion.price == -1)
						{
							itemFashion.addInfoFrist(T.chuaban, 6);
							itemFashion.colorName = 6;
						}
						else if (itemFashion.ID == Player.idFashion && b != 114)
						{
							itemFashion.addInfoFrist(T.daTrangBi, 4);
							itemFashion.colorName = 4;
						}
						else
						{
							itemFashion.addInfoFrist(T.dasuhuu, 1);
							itemFashion.colorName = 1;
						}
						mVector2.addElement(itemFashion);
					}
				}
				break;
			}
			case 107:
			{
				for (int l = 0; l < num; l++)
				{
					short id = msg.reader().readShort();
					short idImage = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					short priceRuby = msg.reader().readShort();
					Potion potion = new Potion(id, idImage, name, info, priceRuby);
					if (potion != null)
					{
						if (potion.price > 0)
						{
							potion.addInfoFrist(T.price + " " + potion.price + " " + T.bery, 5);
						}
						else if (potion.priceRuby > 0)
						{
							potion.addInfoFrist(T.price + " " + potion.priceRuby + " " + T.ruby, 5);
						}
						mVector2.addElement(potion);
					}
				}
				break;
			}
			case 11:
			{
				for (int i = 0; i < num; i++)
				{
					MainItem mainItem = new MainItem();
					mainItem.ID = msg.reader().readShort();
					mainItem.typeObject = msg.reader().readByte();
					mainItem.namepaint = msg.reader().readUTF();
					mainItem.idIcon = msg.reader().readShort();
					string infoPotion = msg.reader().readUTF();
					mainItem.isShop = true;
					mainItem.setInfoPotion(infoPotion);
					mVector2.addElement(mainItem);
				}
				break;
			}
			}
			TabShop tabShop = null;
			GameCanvas.tabShopScr = new TabScreen(MainTab.xTab, 0);
			mVector mVector3 = new mVector();
			if (b == 110)
			{
				GameCanvas.tabShopScr.isShopClan = true;
				GameCanvas.tabInvenClan = new TabInventory(T.tabInven, Player.vecInvenClan, 4, MainTab.xTab);
				GameCanvas.tabInvenClan.initCmd();
				mVector3.addElement(GameCanvas.tabInvenClan);
			}
			else if (b2 != 107)
			{
				mVector3.addElement(GameCanvas.tabInven);
			}
			else
			{
				GameCanvas.tabShopScr.isShopClan = true;
			}
			if (b == 99)
			{
				GameCanvas.tabInven.setTypeInven(2);
				TabChest tabChest = new TabChest(text, Player.vecChest, MainTab.xTab);
				tabChest.initCmd();
				mVector3.addElement(tabChest);
			}
			else
			{
				GameCanvas.tabInven.setTypeInven(1);
				tabShop = new TabShop(text, mVector2, b, MainTab.xTab);
				mVector3.addElement(tabShop);
			}
			GameCanvas.tabShopScr.addVecTab(mVector3);
			if (b2 != 107)
			{
				GameCanvas.tabShopScr.idSelect = 1;
			}
			else
			{
				GameCanvas.tabShopScr.idSelect = 0;
			}
			GameCanvas.tabShopScr.Show(GameCanvas.gameScr);
			if (b == 99)
			{
				GameCanvas.tabShopScr.typeCurrent = 0;
			}
			else
			{
				GameCanvas.tabShopScr.typeCurrent = 1;
			}
			GameCanvas.tabShopScr.setTabSelect();
			if (b == 101)
			{
				tabShop?.beginFocus();
			}
		}
		catch (Exception)
		{
		}
	}

	public void Dynamic_Menu(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			short num = msg.reader().readShort();
			sbyte b2 = msg.reader().readByte();
			string name = msg.reader().readUTF();
			sbyte b3 = msg.reader().readByte();
			mVector mVector2 = new mVector();
			for (int i = 0; i < b3; i++)
			{
				iCommand iCommand2 = new iCommand(msg.reader().readUTF(), null);
				switch (b)
				{
				case 1:
				{
					sbyte idImage = msg.reader().readByte();
					sbyte textColorName2 = msg.reader().readByte();
					iCommand2 = setfraIComment(b, idImage, iCommand2);
					iCommand2.setFont(AvMain.setTextColorName(textColorName2));
					break;
				}
				case 3:
				{
					short idP = msg.reader().readShort();
					sbyte b4 = msg.reader().readByte();
					switch (b4)
					{
					case 7:
					{
						MainMaterial mainMaterial = null;
						mainMaterial = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + idP);
						if (mainMaterial != null)
						{
							MainItem item5 = new MainItem(b4, mainMaterial.idIcon, 0);
							iCommand2.setItem(item5);
						}
						break;
					}
					case 4:
					{
						Potion templatePotion = Potion.getTemplatePotion(idP);
						if (templatePotion != null && templatePotion.isUpdateTem)
						{
							MainItem item4 = new MainItem(b4, templatePotion.idIcon, 0);
							iCommand2.setItem(item4);
						}
						break;
					}
					}
					break;
				}
				case 4:
				{
					short idIcon3 = msg.reader().readShort();
					MainItem item3 = new MainItem(104, idIcon3, 0);
					iCommand2.setItem(item3);
					break;
				}
				case 5:
				{
					short idIcon2 = msg.reader().readShort();
					MainItem item2 = new MainItem(100, (short)i, idIcon2, 0, 0, 0);
					iCommand2.setItem(item2);
					break;
				}
				case 6:
				{
					short idIcon = msg.reader().readShort();
					sbyte textColorName = msg.reader().readByte();
					MainItem item = new MainItem(100, (short)i, idIcon, 0, 0, 0);
					iCommand2.setItem(item);
					iCommand2.setFont(AvMain.setTextColorName(textColorName));
					break;
				}
				}
				mVector2.addElement(iCommand2);
			}
			if (b == 2)
			{
				GameCanvas.menu.startAt_NPC(mVector2, string.Empty, num, 2, isHelp: false, 0, isDynamic: true);
				GameCanvas.menu.IdMenu = b2;
				return;
			}
			int pos = 2;
			if (b == 1)
			{
				pos = 4;
			}
			GameCanvas.menu.setinfoDynamic(mVector2, pos, b2, num, name);
		}
		catch (Exception)
		{
		}
	}

	public iCommand setfraIComment(sbyte type, short idImage, iCommand cmd)
	{
		if (type == 1 && idImage >= 0)
		{
			cmd.setFraCaption(AvMain.fraCheck, 1, idImage, 0);
		}
		return cmd;
	}

	public void List_Quest(Message msg)
	{
		try
		{
			mVector mVector2 = new mVector();
			switch (msg.reader().readByte())
			{
			case 0:
			{
				sbyte b2 = msg.reader().readByte();
				for (int i = 0; i < b2; i++)
				{
					MainQuest o = QuestAdd(msg);
					mVector2.addElement(o);
				}
				sbyte b3 = msg.reader().readByte();
				for (int j = 0; j < b3; j++)
				{
					MainQuest o2 = QuestCurrent(msg);
					mVector2.addElement(o2);
				}
				sbyte b4 = msg.reader().readByte();
				for (int k = 0; k < b4; k++)
				{
					MainQuest o3 = QuestFinish(msg);
					mVector2.addElement(o3);
				}
				Player.vecQuest = mVector2;
				break;
			}
			case 1:
			{
				sbyte b = msg.reader().readByte();
				MainQuest mainQuest = null;
				switch (b)
				{
				case 0:
					mainQuest = QuestAdd(msg);
					if (mainQuest.typeMainSub != 2)
					{
						Player.questMainNew = mainQuest;
					}
					break;
				case 1:
					mainQuest = QuestCurrent(msg);
					if (GameScreen.indexHelp == 14)
					{
						GameScreen.addHelp(14, 1);
					}
					break;
				case 2:
					mainQuest = QuestFinish(msg);
					if (GameScreen.indexHelp == 14)
					{
						GameScreen.addHelp(14, 1);
					}
					break;
				}
				MainQuest quest2 = MainQuest.getQuest(mainQuest.ID);
				if (quest2 != null)
				{
					Player.vecQuest.removeElement(quest2);
				}
				if (mainQuest != null)
				{
					Player.vecQuest.addElement(mainQuest);
				}
				break;
			}
			case 2:
			{
				MainQuest quest3 = MainQuest.getQuest(msg.reader().readShort());
				if (quest3 != null)
				{
					Player.vecQuest.removeElement(quest3);
				}
				break;
			}
			case 5:
			{
				MainQuest quest = MainQuest.getQuest(msg.reader().readShort());
				if (quest != null)
				{
					GameCanvas.end_Dialog();
					MsgDialog msgDialog = new MsgDialog();
					msgDialog.setinfoQuest(quest, isNew: true);
					GameCanvas.Start_Current_Dialog(msgDialog);
				}
				break;
			}
			}
			for (int l = 0; l < GameScreen.vecPlayers.size(); l++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(l);
				if (mainObject.typeObject == 2)
				{
					mainObject.setTypeQuest();
				}
			}
			TabQuest.isNewQuest = false;
			for (int m = 0; m < Player.vecQuest.size(); m++)
			{
				MainQuest mainQuest2 = (MainQuest)Player.vecQuest.elementAt(m);
				if (mainQuest2.statusQuest == 0 || mainQuest2.statusQuest == 2)
				{
					TabQuest.isNewQuest = true;
					break;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private MainQuest QuestFinish(Message msg)
	{
		MainQuest mainQuest = null;
		try
		{
			mainQuest = new MainQuest(msg.reader().readShort());
			mainQuest.statusQuest = 2;
			mainQuest.typeMainSub = msg.reader().readByte();
			mainQuest.typeActionQuest = msg.reader().readByte();
			mainQuest.name = msg.reader().readUTF();
			mainQuest.idNPC = msg.reader().readShort();
			mainQuest.idNPC_Sub = msg.reader().readShort();
			mainQuest.strTalk = msg.reader().readUTF();
			DataQuest o = new DataQuest(msg.reader().readByte());
			mainQuest.vecTypeQuest.addElement(o);
			mainQuest.strShowDialog = msg.reader().readUTF();
			mainQuest.strNPC_Map = msg.reader().readUTF();
			mainQuest.strNhacNho = msg.reader().readUTF();
		}
		catch (Exception)
		{
		}
		return mainQuest;
	}

	private MainQuest QuestCurrent(Message msg)
	{
		MainQuest mainQuest = null;
		try
		{
			mainQuest = new MainQuest(msg.reader().readShort());
			mainQuest.statusQuest = 1;
			mainQuest.typeMainSub = msg.reader().readByte();
			mainQuest.typeActionQuest = msg.reader().readByte();
			mainQuest.name = msg.reader().readUTF();
			mainQuest.idNPC = msg.reader().readShort();
			mainQuest.strNhacNho = msg.reader().readUTF();
			mainQuest.strShowDialog = msg.reader().readUTF();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				sbyte b2 = msg.reader().readByte();
				DataQuest dataQuest = new DataQuest(b2);
				if (b2 == 2 || b2 == 1)
				{
					dataQuest.SetDataQuest(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
				}
				mainQuest.vecTypeQuest.addElement(dataQuest);
			}
			mainQuest.strNPC_Map = msg.reader().readUTF();
			mainQuest.idMapHelp = msg.reader().readShort();
		}
		catch (Exception)
		{
		}
		return mainQuest;
	}

	private MainQuest QuestAdd(Message msg)
	{
		MainQuest mainQuest = null;
		try
		{
			mainQuest = new MainQuest(msg.reader().readShort());
			mainQuest.statusQuest = 0;
			mainQuest.typeMainSub = msg.reader().readByte();
			mainQuest.typeActionQuest = msg.reader().readByte();
			mainQuest.name = msg.reader().readUTF();
			mainQuest.idNPC = msg.reader().readShort();
			mainQuest.strTalk = msg.reader().readUTF();
			sbyte typeQ = msg.reader().readByte();
			mainQuest.strShowDialog = msg.reader().readUTF();
			DataQuest o = new DataQuest(typeQ);
			mainQuest.vecTypeQuest.addElement(o);
			mainQuest.strNPC_Map = msg.reader().readUTF();
			mainQuest.lvRequest = msg.reader().readShort();
		}
		catch (Exception)
		{
		}
		return mainQuest;
	}

	public void get_Info_NPC(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 2);
			mainObject.IdIcon = msg.reader().readShort();
			mainObject.nameGiaotiep = msg.reader().readUTF();
			sbyte w = msg.reader().readByte();
			sbyte h = msg.reader().readByte();
			GameCanvas.loadmap.setBlockNPC(mainObject.x, mainObject.y, w, h);
		}
		catch (Exception)
		{
		}
	}

	public void Party(Message msg)
	{
		try
		{
			int num = Player.vecParty.size();
			switch (msg.reader().readByte())
			{
			case 5:
			{
				Player.vecParty.removeAllElements();
				sbyte b = msg.reader().readByte();
				for (int i = 0; i < b; i++)
				{
					InfoMemList infoMemList = new InfoMemList(msg.reader().readShort());
					infoMemList.updateData(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
					Player.vecParty.addElement(infoMemList);
				}
				for (int j = 0; j < GameScreen.vecPlayers.size(); j++)
				{
					MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(j);
					for (int k = 0; k < Player.vecParty.size(); k++)
					{
						InfoMemList infoMemList2 = (InfoMemList)Player.vecParty.elementAt(k);
						if (infoMemList2.id == mainObject.ID && mainObject.typeObject == 0)
						{
							infoMemList2.updateHP(mainObject.Hp, mainObject.maxHp, mainObject.Lv);
						}
					}
				}
				break;
			}
			case 1:
			{
				short num2 = msg.reader().readShort();
				InfoMemList infoMemList3 = InfoMemList.getMem(num2, Player.vecParty);
				if (infoMemList3 == null)
				{
					infoMemList3 = new InfoMemList(num2);
					Player.vecParty.addElement(infoMemList3);
				}
				infoMemList3.updateData(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
				for (int l = 0; l < GameScreen.vecPlayers.size(); l++)
				{
					MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(l);
					if (num2 == mainObject2.ID && mainObject2.typeObject == 0)
					{
						infoMemList3.updateHP(mainObject2.Hp, mainObject2.maxHp, mainObject2.Lv);
					}
				}
				break;
			}
			case 2:
			{
				InfoMemList mem = InfoMemList.getMem(msg.reader().readShort(), Player.vecParty);
				if (mem != null)
				{
					Player.vecParty.removeElement(mem);
				}
				break;
			}
			case 3:
				Player.vecParty.removeAllElements();
				break;
			case 0:
			{
				short iD2 = msg.reader().readShort();
				string name2 = msg.reader().readUTF();
				InfoMemList.setTypeEvent(iD2, 1, name2, T.eventParty, 0, 0);
				break;
			}
			case 7:
			{
				short iD = msg.reader().readShort();
				string name = msg.reader().readUTF();
				InfoMemList.setTypeEvent(iD, 5, name, T.xinvaoParty, 0, 0);
				break;
			}
			}
			if (Player.vecParty.size() != num && GameCanvas.currentScreen == PartyScreen.gI())
			{
				PartyScreen.gI().updateInfo();
			}
		}
		catch (Exception)
		{
		}
	}

	public void ChatPopup(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null)
			{
				string chat = msg.reader().readUTF();
				chat = (mainObject.strChatPopup = GameMidlet.fixString(chat));
				if (mainObject.typeObject == 0)
				{
					GameCanvas.chatTabScr.addNewChat(T.tabServer, string.Empty, mainObject.name + ": " + chat, 1, isFocus: false);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void ChatTab(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			string chat = msg.reader().readUTF();
			chat = GameMidlet.fixString(chat);
			if (chat != null && chat.Trim().Length != 0)
			{
				if (text.CompareTo(T.tabServer) == 0)
				{
					GameCanvas.chatTabScr.addNewChat(text, T.thongbao + ": ", chat, 1, isFocus: false);
				}
				else if (text.CompareTo(T.cmdEvent) == 0)
				{
					GameCanvas.chatTabScr.addNewChat(text, string.Empty, chat, 1, isFocus: false);
				}
				else if (text.CompareTo(T.tabPhobang) == 0)
				{
					GameCanvas.chatTabScr.addNewChat(text, string.Empty, chat, 1, isFocus: false);
				}
				else if (text.CompareTo(T.tabThongBao) == 0)
				{
					GameCanvas.chatTabScr.addNewChat(text, string.Empty, chat, 1, isFocus: false);
				}
				else if (text.CompareTo(T.tabBangChu) == 0)
				{
					GameCanvas.chatTabScr.addNewChat(text, string.Empty, chat, 1, isFocus: false);
				}
				else if (text.CompareTo(T.tabBangHoi) == 0)
				{
					GameCanvas.chatTabScr.addNewChat(text, string.Empty, chat, 0, isFocus: false);
				}
				else if (text.CompareTo(T.party) == 0)
				{
					GameCanvas.chatTabScr.addNewChat(text, string.Empty, chat, 0, isFocus: false);
				}
				else
				{
					GameCanvas.chatTabScr.addNewChatCheckSpam(text, text + ": ", chat, 0, isFocus: false);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void charWearing(Message msg)
	{
		string empty = string.Empty;
		try
		{
			short iD = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, b);
			if (mainObject == null)
			{
				return;
			}
			empty = mainObject.name;
			mSystem.outloi("update charwearing name=" + empty);
			short headset = msg.reader().readShort();
			mainObject.sethead(headset);
			mainObject.sethair(msg.reader().readShort());
			sbyte b2 = msg.reader().readByte();
			short[] array = new short[b2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			for (int j = 0; j < b2; j++)
			{
				if (msg.reader().readByte() == 1 && mainObject == GameScreen.player)
				{
					MainItem mainItem = readUpdateItem(msg, isShop: false);
					GameScreen.player.hashEquip.put(string.Empty + j, mainItem);
					if (mainItem.typeSpec == 1)
					{
						GameScreen.player.lvHeart = mainItem.LvUpgrade;
					}
				}
				array[j] = msg.reader().readShort();
			}
			if (b == 1)
			{
				mainObject.setWearingMon(array);
			}
			else
			{
				mainObject.setWearing(array);
			}
			if (mainObject == GameScreen.player)
			{
				if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					for (int k = 0; k < GameCanvas.tabShopScr.vecTabs.size(); k++)
					{
						((MainTab)GameCanvas.tabShopScr.vecTabs.elementAt(k)).updateTrangBi();
					}
				}
				TabScreen.isRefresh = true;
				Uniform.checkIndexItem(isRemove: false);
			}
			short num = -1;
			try
			{
				num = msg.reader().readShort();
			}
			catch (Exception)
			{
				num = -1;
			}
			mainObject.setBodyBay(num);
			short num2 = -1;
			try
			{
				num2 = msg.reader().readShort();
			}
			catch (Exception)
			{
				num2 = -1;
			}
			mainObject.setLegBay(num2);
			short num3 = -1;
			try
			{
				num3 = msg.reader().readShort();
			}
			catch (Exception)
			{
				num3 = -1;
			}
			mainObject.setWeaponBay(num3);
			short num4 = -1;
			try
			{
				num4 = msg.reader().readShort();
			}
			catch (Exception)
			{
				num4 = -1;
			}
			mainObject.setHairBay(num4);
			mSystem.outz("charWearing name = " + mainObject.name + " bay : " + num + " " + num2 + " " + num3 + " " + num4);
		}
		catch (Exception)
		{
		}
	}

	public void Save_RMS_Server(Message msg)
	{
		try
		{
			sbyte id = msg.reader().readByte();
			short num = msg.reader().readShort();
			sbyte[] array = null;
			if (num > 0)
			{
				array = new sbyte[num];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = msg.reader().readByte();
				}
			}
			GameCanvas.saveRms.setLoadRMSServer(id, array);
		}
		catch (Exception)
		{
		}
	}

	public void Learn_Skill(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b == 0 || b == 1)
			{
				Skill_Info skill_Info = readSkillInfo(msg);
				if (skill_Info == null)
				{
					return;
				}
				switch (b)
				{
				case 0:
				{
					MsgDialog msgDialog = new MsgDialog();
					msgDialog.setinfoSkillInfo(skill_Info);
					GameCanvas.Start_Current_Dialog(msgDialog);
					break;
				}
				case 1:
				{
					Skill_Info skillFromID = Skill_Info.getSkillFromID(skill_Info.ID);
					if (skillFromID != null)
					{
						skill_Info.indexHotKey = skillFromID.indexHotKey;
						Player.vecListSkill.setElementAt(skill_Info, Player.vecListSkill.indexOf(skillFromID));
					}
					else
					{
						skill_Info.indexHotKey = indexHotKeySkill;
						indexHotKeySkill++;
						Player.vecListSkill.addElement(skill_Info);
					}
					Player.vecListSkill = MainItem.SortVecItem(Player.vecListSkill);
					break;
				}
				}
				if (b == 1)
				{
					if (skill_Info.typeSkill == 1)
					{
						if (skill_Info.Lv_RQ == 1)
						{
							bool flag = false;
							for (int i = 0; i < Player.hotkeyPlayer[0].Length; i++)
							{
								if (flag)
								{
									break;
								}
								if (Player.hotkeyPlayer[Player.currentTab][i].skill == null)
								{
									MainSkill mainSkill = new MainSkill(skill_Info.ID, -1);
									mainSkill.indexHotKey = skill_Info.indexHotKey;
									mainSkill.idIcon = skill_Info.idIcon;
									mainSkill.isBuff = skill_Info.typeSkill == 2;
									Player.setHotKey(i, mainSkill, null);
									flag = true;
								}
							}
						}
						else if (skill_Info.Lv_RQ > 1)
						{
							MainImage image = Skill_Info.getImage(skill_Info.idIcon);
							FrameImage fra = null;
							if (image != null && image.img != null)
							{
								fra = new FrameImage(image.img, mImage.getImageWidth(image.img.image), mImage.getImageHeight(image.img.image));
							}
							GameScreen.addEffectNumImage(T.lvUp, GameScreen.player.x, GameScreen.player.y - GameScreen.player.hOne, 2, fra, 0);
						}
					}
					Player.setHotKeyBuff();
				}
				if (!Player.isSkillready)
				{
					if (skill_Info.Lv_RQ == -1)
					{
						Player.isSkillready = true;
					}
				}
				else
				{
					Player.isSkillready = false;
					for (int j = 0; j < Player.vecListSkill.size(); j++)
					{
						Skill_Info skill_Info2 = (Skill_Info)Player.vecListSkill.elementAt(j);
						if (skill_Info2.Lv_RQ == -1 && (skill_Info2.typeSkill != 3 || TabSkill.numCurrentPassive < Player.numPassive))
						{
							Player.isSkillready = true;
							break;
						}
					}
				}
			}
			if (b == 2)
			{
				Skill_Info skillFromID2 = Skill_Info.getSkillFromID(msg.reader().readShort());
				if (skillFromID2 != null)
				{
					skillFromID2.percentLv = msg.reader().readShort();
				}
			}
			if (b != 3)
			{
				return;
			}
			short num = msg.reader().readShort();
			Skill_Info skillFromID3 = Skill_Info.getSkillFromID(num);
			for (int k = 0; k < Player.hotkeyPlayer.Length; k++)
			{
				for (int l = 0; l < Player.hotkeyPlayer[k].Length; l++)
				{
					Hotkey hotkey = Player.hotkeyPlayer[k][l];
					if (hotkey.skill != null && hotkey.skill.ID == num)
					{
						hotkey.skill = null;
					}
				}
			}
			Player.vecListSkill.removeElement(skillFromID3);
			Player.setHotKeyBuff();
		}
		catch (Exception)
		{
		}
	}

	public void Friend(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				short iD = msg.reader().readShort();
				string name = msg.reader().readUTF();
				InfoMemList.setTypeEvent(iD, 0, name, T.eventAddFriend, 0, 0);
				break;
			}
			case 1:
			{
				int num = msg.reader().readInt();
				for (int i = 0; i < Player.vecFriendList.size(); i++)
				{
					InfoMemList infoMemList2 = (InfoMemList)Player.vecFriendList.elementAt(i);
					if (infoMemList2.id == num)
					{
						Player.vecFriendList.removeElement(infoMemList2);
						break;
					}
				}
				break;
			}
			case 2:
			{
				mVector mVector2 = new mVector();
				sbyte b = msg.reader().readByte();
				for (int j = 0; j < b; j++)
				{
					InfoMemList infoMemList3 = ReadInfoMemList(msg);
					if (infoMemList3 != null)
					{
						mVector2.addElement(infoMemList3);
					}
				}
				Player.vecFriendList = mVector2;
				FriendList.isGetListFriend = true;
				FriendList.gI().vecPlayer = Player.vecFriendList;
				FriendList.gI().updateInfo();
				if (!MsgSpamSetup.isDontShowFriendList)
				{
					FriendList.gI().Show(GameCanvas.gameScr);
				}
				MsgSpamSetup.isDontShowFriendList = false;
				break;
			}
			case 3:
			{
				InfoMemList infoMemList = ReadInfoMemList(msg);
				if (infoMemList != null)
				{
					Player.vecFriendList.addElement(infoMemList);
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void ListPlayerServer(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			string name = msg.reader().readUTF();
			sbyte page = msg.reader().readByte();
			mVector mVector2 = new mVector();
			sbyte b2 = msg.reader().readByte();
			for (int i = 0; i < b2; i++)
			{
				InfoMemList infoMemList = null;
				infoMemList = (PlayerListServer.typeClan(b) ? ReadInfoClanList(msg) : ((b != 9) ? ReadInfoMemList(msg) : ReadInfoMemListWanted(msg)));
				if (infoMemList != null)
				{
					mVector2.addElement(infoMemList);
				}
			}
			switch (b)
			{
			case 2:
				BlackList.instance = new BlackList(b, mVector2, name, page);
				BlackList.instance.Show(GameCanvas.gameScr);
				BlackList.instance.isLoad = false;
				break;
			case 9:
				WantedScreen.instance = new WantedScreen(mVector2, 0, 0);
				WantedScreen.instance.Show(GameCanvas.gameScr);
				break;
			default:
				PlayerListServer.instance = new PlayerListServer(b, mVector2, name, page);
				PlayerListServer.instance.Show(GameCanvas.gameScr);
				PlayerListServer.instance.isLoad = false;
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public InfoMemList ReadInfoMemList(Message msg)
	{
		InfoMemList infoMemList = null;
		try
		{
			infoMemList = new InfoMemList(msg.reader().readInt());
			infoMemList.name = msg.reader().readUTF();
			if (infoMemList.name.CompareTo(GameScreen.player.name) == 0)
			{
				infoMemList.isMe = true;
			}
			infoMemList.Lv = msg.reader().readShort();
			infoMemList.head = msg.reader().readShort();
			infoMemList.hair = msg.reader().readShort();
			infoMemList.hat = msg.reader().readShort();
			infoMemList.typeOnline = msg.reader().readByte();
			infoMemList.info = msg.reader().readUTF();
			infoMemList.rank = msg.reader().readShort();
		}
		catch (Exception)
		{
		}
		return infoMemList;
	}

	public InfoMemList ReadInfoMemListWanted(Message msg)
	{
		InfoMemList infoMemList = null;
		try
		{
			infoMemList = new InfoMemList(msg.reader().readInt());
			infoMemList.name = msg.reader().readUTF();
			if (infoMemList.name.CompareTo(GameScreen.player.name) == 0)
			{
				infoMemList.isMe = true;
			}
			infoMemList.updateChar(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
			infoMemList.charShow.rankWanted = msg.reader().readInt();
			infoMemList.charShow.wanted = msg.reader().readInt();
		}
		catch (Exception)
		{
		}
		return infoMemList;
	}

	public InfoMemList ReadInfoClanList(Message msg)
	{
		InfoMemList infoMemList = null;
		try
		{
			infoMemList = new InfoMemList(msg.reader().readShort());
			string chat = msg.reader().readUTF();
			chat = GameMidlet.fixString(chat);
			infoMemList.name = chat;
			infoMemList.info = msg.reader().readUTF();
			infoMemList.idmap = msg.reader().readShort();
			infoMemList.rank = msg.reader().readShort();
		}
		catch (Exception)
		{
		}
		return infoMemList;
	}

	public void Notify_Server(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			string chat = msg.reader().readUTF();
			chat = GameMidlet.fixString(chat);
			sbyte b2 = msg.reader().readByte();
			InfoShowNotify infoShowNotify = new InfoShowNotify(chat, b);
			mFont font = null;
			if (b == 0)
			{
				font = AvMain.setTextColor(b2);
			}
			if (b == 1)
			{
				font = AvMain.setTextColorName(b2);
			}
			infoShowNotify.setFont(font);
			try
			{
				short iconClan = msg.reader().readShort();
				infoShowNotify.iconClan = iconClan;
			}
			catch (Exception)
			{
			}
			Interface_Game.addInfoServer(infoShowNotify);
			mSystem.outloi("vao day info server info=" + chat);
		}
		catch (Exception)
		{
		}
	}

	public void Check_Data_Ver(Message msg)
	{
		try
		{
			mSystem.outz("vao check data");
			short num = msg.reader().readShort();
			short num2 = msg.reader().readShort();
			if (!GameCanvas.lowGraphic)
			{
				if (num != GlobalService.VerMonster)
				{
					GlobalService.gI().get_DATA(15);
				}
				else
				{
					CatalogyMonster.LoadDataMon(SaveRms.loadData("dataMon"), isSave: false);
				}
				if (num2 != GlobalService.VerPotion)
				{
					GlobalService.gI().get_DATA(28);
				}
				else
				{
					Potion.UpdateDataPotion(SaveRms.loadData("dataPotion"), isSave: false, 4);
				}
			}
			else
			{
				LoadMapScreen.isLoadDataMon = true;
			}
			if (msg.reader().readShort() != GlobalService.VerNameAtribute)
			{
				GlobalService.gI().get_DATA(2);
			}
			else
			{
				MainItem.LoadNameAttribute(SaveRms.loadData("dataAttri"), isSave: false);
			}
			msg.reader().readShort();
			if (msg.reader().readShort() != GlobalService.VerNameMap)
			{
				GlobalService.gI().get_DATA(6);
			}
			else
			{
				LoadMap.LoadNameMap(SaveRms.loadData("dataNameMap"), isSave: false);
			}
			if (msg.reader().readShort() != GlobalService.VerNamePotionQuest)
			{
				GlobalService.gI().get_DATA(7);
			}
			else
			{
				MainQuest.LoadNamePotionQuest(SaveRms.loadData("dataNamePotionquest"), isSave: false);
			}
			msg.reader().readShort();
			short num3 = msg.reader().readShort();
			if (num3 != GlobalService.VerImageSave)
			{
				if (GlobalService.VerImageSave != -1)
				{
					GameMidlet.delRMS();
				}
				GlobalService.VerImageSave = num3;
				SaveRms.saveVer(GlobalService.VerImageSave, "VerdataImageSave");
			}
			if (msg.reader().readShort() != GlobalService.VerDataUpgrade)
			{
				GlobalService.gI().get_DATA(12);
			}
			else
			{
				MainDataUpgrade.LoadDataUpgrade(SaveRms.loadData("dataUpgradeSave"), isSave: false);
			}
			if (msg.reader().readShort() != GlobalService.verPotionClan)
			{
				GlobalService.gI().get_DATA(29);
			}
			else
			{
				Potion.UpdateDataPotion(SaveRms.loadData("dataPotionClan"), isSave: false, 8);
			}
			LoginScreen.isCheckData = true;
			GameCanvas.end_Dialog();
			if (GameCanvas.currentScreen == GameCanvas.loginScr)
			{
				GameCanvas.loginScr.doLogin(isGetData: false, 0, GameCanvas.loginScr.tfUser.getText(), GameCanvas.loginScr.tfPass.getText());
			}
			else if (GameCanvas.currentScreen == GameCanvas.fristLoginScr)
			{
				GameCanvas.fristLoginScr.setNewAcc(isCheckDataOK: true);
			}
		}
		catch (Exception)
		{
		}
	}

	public void Open_Box(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			short idChest = -1;
			if (b == 21)
			{
				idChest = msg.reader().readShort();
			}
			if (GameScreen.isOnChestAdmin)
			{
				idChest = -1;
			}
			string name = msg.reader().readUTF();
			string info = msg.reader().readUTF();
			int num = msg.reader().readByte();
			Item_Drop[] array = new Item_Drop[num];
			for (int i = 0; i < num; i++)
			{
				sbyte type = msg.reader().readByte();
				string name2 = msg.reader().readUTF();
				short idIcon = msg.reader().readShort();
				int num2 = msg.reader().readInt();
				sbyte color = msg.reader().readByte();
				array[i] = new Item_Drop((short)i, type, name2, 0, 0, idIcon, color);
				array[i].num = num2;
			}
			MsgShowGift msgShowGift = new MsgShowGift();
			switch (b)
			{
			case 23:
				GameScreen.addPlayer(new Item_Drop(-1, 4, T.ruongqua, GameScreen.player.x, GameScreen.player.y, 325, 4)
				{
					mItemDrop = array
				});
				break;
			case 24:
				GameScreen.addPlayer(new Item_Drop(-1, 4, T.ruongqua, GameScreen.player.x, GameScreen.player.y, 326, 4)
				{
					mItemDrop = array
				});
				break;
			case 22:
				msgShowGift.setinfoShow_Gift_OnHead(b, name, info, array, idChest, GameScreen.player);
				GameCanvas.showDialog = msgShowGift;
				break;
			default:
				msgShowGift.setinfoShow_Gift(b, name, info, array, idChest);
				GameCanvas.Start_Current_Dialog(msgShowGift);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Fight(Message msg)
	{
		try
		{
			if (msg.reader().readByte() == 0)
			{
				short iD = msg.reader().readShort();
				string name = msg.reader().readUTF();
				short priceFight = msg.reader().readShort();
				sbyte typeFight = 0;
				if (msg.reader().available() > 0)
				{
					typeFight = msg.reader().readByte();
				}
				InfoMemList.setTypeEvent(iD, 3, name, T.eventFight, priceFight, typeFight);
			}
		}
		catch (Exception)
		{
		}
	}

	public void Buff(Message msg)
	{
		try
		{
			sbyte num = msg.reader().readByte();
			short typeBuff = msg.reader().readShort();
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			short data = msg.reader().readShort();
			short effBuff = msg.reader().readShort();
			int timeBuff = msg.reader().readInt();
			sbyte tem2 = msg.reader().readByte();
			sbyte b = msg.reader().readByte();
			if (num == 1)
			{
				MainObject mainObject = MainObject.get_Object(iD, tem);
				if (mainObject != null && mainObject != GameScreen.player)
				{
					mainObject.addEffBuff(1, effBuff, 0);
				}
			}
			MainBuff mainBuff = new MainBuff(typeBuff);
			mainBuff.setTimeBuff(timeBuff);
			mainBuff.setData(data);
			mVector mVector2 = new mVector();
			for (int i = 0; i < b; i++)
			{
				MainObject mainObject2 = MainObject.get_Object(msg.reader().readShort(), tem2);
				if (mainObject2 == null)
				{
					continue;
				}
				mVector2.addElement(mainObject2);
				if (mainObject2 == GameScreen.player)
				{
					sbyte b2 = msg.reader().readByte();
					for (int j = 0; j < b2; j++)
					{
						MainInfoItem o = new MainInfoItem(msg.reader().readByte(), msg.reader().readShort());
						mainBuff.vecInfoAtt.addElement(o);
					}
				}
				mainBuff.setObjc(mainObject2);
				mainObject2.AddBuff(mainBuff);
			}
			if (msg.reader().readByte() == 3)
			{
				short head = msg.reader().readShort();
				short body = msg.reader().readShort();
				short leg = msg.reader().readShort();
				mainBuff.setDevilBuff(head, body, leg);
				for (int k = 0; k < mVector2.size(); k++)
				{
					((MainObject)mVector2.elementAt(k)).checkBuffDevil();
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void ChangeArea(Message msg)
	{
		try
		{
			LoadMapScreen.typeChangeMap = 0;
			GameCanvas.loadMapScr.Show();
			GameCanvas.loadmap.idLastMap = GameCanvas.loadmap.idMap;
			GameScreen.RemoveLoadMap();
			GameCanvas.loadMapScr.area = msg.reader().readByte();
			sbyte typeViewPlayer = msg.reader().readByte();
			GameScreen.player.posTransRoad = null;
			GameScreen.player.x = msg.reader().readShort();
			GameScreen.player.y = msg.reader().readShort();
			GameScreen.player.maxHp = msg.reader().readInt();
			GameScreen.player.Hp = msg.reader().readInt();
			GameScreen.player.maxMp = msg.reader().readInt();
			GameScreen.player.Mp = msg.reader().readInt();
			LoadMapScreen.IDBack = msg.reader().readByte();
			LoadMapScreen.HBack = msg.reader().readShort();
			LoadMapScreen.isNextMap = true;
			GameCanvas.gameScr.setTypeViewPlayer(typeViewPlayer);
		}
		catch (Exception)
		{
		}
	}

	public void Skill_Map_Train(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, 0);
			mainObject.mSkillMapTrain = new short[b];
			for (int i = 0; i < b; i++)
			{
				mainObject.mSkillMapTrain[i] = msg.reader().readShort();
			}
		}
		catch (Exception)
		{
		}
	}

	public void AreaStatus(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			sbyte[] array = new sbyte[b];
			for (int i = 0; i < b; i++)
			{
				array[i] = msg.reader().readByte();
			}
			MsgArea msgArea = new MsgArea();
			msgArea.setinfoChangeArea(array, 0);
			GameCanvas.Start_Current_Dialog(msgArea);
		}
		catch (Exception)
		{
		}
	}

	public void Register(Message msg)
	{
		try
		{
			if (msg.reader().readByte() == 0)
			{
				string info = msg.reader().readUTF();
				ScreenUpgrade.stopUpgradeItemAuto();
				ScreenUpgrade.stopUpgradeSuperItemAuto();
				ScreenUpgrade.stopUpgradeSkillDevilAuto();
				GameCanvas.loginScr.type = 0;
				GameCanvas.loginScr.setCmd();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
			}
		}
		catch (Exception)
		{
		}
	}

	public void Del_Char(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			for (int i = 0; i < ListChar_Screen.vecListChar.size(); i++)
			{
				MainObject mainObject = (MainObject)ListChar_Screen.vecListChar.elementAt(i);
				if (mainObject.ID == num)
				{
					mainObject.typeSpecCharList = msg.reader().readByte();
					if (mainObject.typeSpecCharList != 0)
					{
						mainObject.timeDie = msg.reader().readInt();
					}
					else
					{
						mainObject.timeDie = 0L;
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void MonsterDie(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 1);
			if (mainObject != null && (mainObject.x < MainScreen.cameraMain.xCam || mainObject.x > MainScreen.cameraMain.xCam + MotherCanvas.w))
			{
				mainObject.Hp = 0;
				mainObject.beginDie(null);
			}
		}
		catch (Exception)
		{
		}
	}

	public void NumItemQuest(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			short num2 = msg.reader().readShort();
			short num3 = msg.reader().readShort();
			mFont font = mFont.tahoma_7_white;
			if (num2 >= num3)
			{
				font = mFont.tahoma_7_yellow;
			}
			switch (b)
			{
			case 1:
			{
				CatalogyMonster catalogyMonster = MainMonster.getCatalogyMonster(num);
				Interface_Game.addInfoPlayerNormal(T.haguc + " " + catalogyMonster.name + ": " + num2 + "/" + num3, font);
				break;
			}
			case 5:
				if (num < DataQuest.nameItemQuest.Length)
				{
					Interface_Game.addInfoPlayerNormal(T.thuthap + " " + DataQuest.nameItemQuest[num] + ": " + num2 + "/" + num3, font);
				}
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Teleport_Boss(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			short x = msg.reader().readShort();
			short y = msg.reader().readShort();
			MainObject mainObject = MainObject.get_Object(num, b);
			if (mainObject != null && !mainObject.returnAction())
			{
				GameScreen.addEffectEnd_ObjTo(56, 0, x, y, num, b, (sbyte)mainObject.Dir, mainObject);
			}
		}
		catch (Exception)
		{
		}
	}

	public void SkillSpec(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			mSystem.outz("SkillSpec cat = " + b);
			short num2 = 0;
			mVector mVector2 = new mVector();
			mVector mVector3 = new mVector();
			MainObject mainObject = MainObject.get_Object(num, b);
			if (mainObject == null || mainObject.returnAction())
			{
				return;
			}
			num2 = msg.reader().readShort();
			Object_Effect_Skill o = new Object_Effect_Skill(mainObject.ID, mainObject.typeObject);
			short timebuff = msg.reader().readShort();
			sbyte b2 = msg.reader().readByte();
			switch (num2)
			{
			case 10014:
				GameScreen.addEffectEnd_ObjTo(69, 0, 0, 0, num, b, (sbyte)mainObject.Dir, mainObject);
				return;
			case 10016:
			{
				msg.reader().readShort();
				msg.reader().readShort();
				sbyte b3 = msg.reader().readByte();
				for (int i = 0; i < b3; i++)
				{
					Point_Focus point_Focus = new Point_Focus(msg.reader().readShort(), msg.reader().readShort());
					GameScreen.addEffectEnd_ObjTo(70, 0, point_Focus.x, point_Focus.y, num, b, (sbyte)mainObject.Dir, mainObject);
				}
				return;
			}
			}
			if (b2 != 0 && b2 != 2)
			{
				b2 = 0;
			}
			MainSkill mainSkill = new MainSkill(-1, num2);
			mainSkill.setTypeBuff(2, 0, timebuff);
			short x = msg.reader().readShort();
			short y = msg.reader().readShort();
			sbyte b4 = msg.reader().readByte();
			for (int j = 0; j < b4; j++)
			{
				Point_Focus o2 = new Point_Focus(msg.reader().readShort(), msg.reader().readShort());
				mVector3.addElement(o2);
			}
			mainSkill.setPos(x, y, mVector3, b2);
			mVector2.addElement(o);
			mainObject.addSkillFire(mainSkill, mVector2);
		}
		catch (Exception)
		{
		}
	}

	public void addHP_EffSkill(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			int num = msg.reader().readInt();
			int maxHp = msg.reader().readInt();
			short num2 = msg.reader().readShort();
			short time = msg.reader().readShort();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null && !mainObject.returnAction())
			{
				mainObject.maxHp = maxHp;
				mSystem.outz("add hp eff name=" + mainObject.name + " maxhp=" + mainObject.maxHp);
				string content = string.Empty + (num - mainObject.Hp);
				if (num > mainObject.Hp)
				{
					content = "+" + (num - mainObject.Hp);
				}
				if (num - mainObject.Hp != 0)
				{
					GameScreen.addEffectNum(content, mainObject.x, mainObject.y - mainObject.hOne / 4 * 3, 3);
				}
				mainObject.Hp = num;
				if (num2 != -1)
				{
					mainObject.addEffSpec(num2, time);
				}
				if (mainObject == GameScreen.player)
				{
					GlobalService.gI().Obj_Move((short)mainObject.x, (short)mainObject.y);
				}
				mSystem.outz("nhan update HP=" + num + "  " + maxHp + " ideff = " + num2);
			}
		}
		catch (Exception)
		{
		}
	}

	public void SaveImageAndroid(Message msg)
	{
		try
		{
			UpdateImageScreen.curNum++;
			string name = msg.reader().readUTF();
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			SaveImageRMS.vecSaveImageAndroid.addElement(new idSaveImage(name, data));
		}
		catch (Exception)
		{
		}
	}

	public void resetValueUpdateImage(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			for (int i = 0; i < T.mStringDownload.Length; i++)
			{
				if (text.CompareTo(T.mStringDownload[i]) == 0)
				{
					text = T.mStringDownload[i + 1];
					break;
				}
			}
			UpdateImageScreen.setmNamePaint(T.dangtai + text);
			UpdateImageScreen.setValueUpdate(0, msg.reader().readShort());
		}
		catch (Exception)
		{
		}
	}

	public void LoadImageAndroidOk(Message msg)
	{
		try
		{
			UpdateImageScreen.setmNamePaint(T.dangcai);
			UpdateImageScreen.statusUpdate = 3;
			UpdateImageScreen.maxNum = SaveImageRMS.vecSaveImageAndroid.size();
			SaveImageRMS.start();
		}
		catch (Exception)
		{
		}
	}

	public void comboSkill(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b <= 0)
			{
				Interface_Game.isCombo = false;
				Player.indexCombo = -1;
				Interface_Game.indexEffCombo = -1;
				return;
			}
			Interface_Game.isCombo = true;
			Player.indexCombo = -1;
			Player.mComboSkill = new short[b];
			for (int i = 0; i < b; i++)
			{
				Player.mComboSkill[i] = msg.reader().readShort();
			}
			Interface_Game.timeCombo = 30;
			Interface_Game.indexEffCombo = -1;
		}
		catch (Exception)
		{
		}
	}

	public void ShowInfoPlayer(Message msg)
	{
		try
		{
			MainObject mainObject = new MainObject();
			mainObject.name = msg.reader().readUTF();
			mainObject.maxHp = msg.reader().readInt();
			mainObject.maxMp = msg.reader().readInt();
			mainObject.Hp = msg.reader().readInt();
			mainObject.Mp = msg.reader().readInt();
			mainObject.Lv = msg.reader().readShort();
			mainObject.percentLv = msg.reader().readShort();
			short headset = msg.reader().readShort();
			mainObject.sethead(headset);
			mainObject.sethair(msg.reader().readShort());
			short num = msg.reader().readShort();
			if (num >= 0)
			{
				mainObject.clan = new MainClan();
				mainObject.clan.ID = num;
				mainObject.clan.idIcon = msg.reader().readShort();
				string chat = msg.reader().readUTF();
				chat = GameMidlet.fixString(chat);
				mainObject.clan.name = chat;
			}
			sbyte b = msg.reader().readByte();
			mSystem.outz("size " + b);
			short[] array = new short[b];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			for (int j = 0; j < b; j++)
			{
				sbyte b2 = msg.reader().readByte();
				if (b2 == 1)
				{
					MainItem v = readUpdateItem(msg, isShop: false);
					mainObject.hashEquip.put(string.Empty + j, v);
					array[j] = msg.reader().readShort();
				}
				mSystem.outz("isWear " + b2 + " mWear " + j + " = " + array[j]);
			}
			mainObject.setWearing(array);
			sbyte num2 = msg.reader().readByte();
			short weaponFashion = msg.reader().readShort();
			if (num2 == 0)
			{
				mainObject.weaponFashion = weaponFashion;
			}
			mainObject.indexFullSet = msg.reader().readByte();
			mSystem.outz("fullset = " + mainObject.indexFullSet);
			GameCanvas.Start_Current_Dialog(new MsgOtherCharInfo(mainObject));
		}
		catch (Exception)
		{
		}
	}

	public void loadDataEff(Message msg)
	{
		try
		{
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			EffectAuto.readData(data, isSave: true);
		}
		catch (Exception)
		{
		}
	}

	public void update_Pk_Point(Message msg)
	{
		try
		{
			GameScreen.player.pointPk = msg.reader().readInt();
		}
		catch (Exception)
		{
		}
	}

	public void setWeather(Message msg)
	{
		if (GameCanvas.lowGraphic || LoadMapScreen.isSuperBoss)
		{
			return;
		}
		try
		{
			sbyte b = msg.reader().readByte();
			sbyte level = msg.reader().readByte();
			if (b < 0)
			{
				GameScreen.effMap = null;
				return;
			}
			GameScreen.effMap = new Effect_Map(b, level);
			switch (b)
			{
			case 9:
				GameCanvas.mapBack.setDark();
				LoadMapScreen.isSuperBoss = true;
				break;
			case 8:
				if (GameCanvas.mapBack != null)
				{
					GameCanvas.mapBack.setSkyDragon();
				}
				break;
			case 1:
				if (GameCanvas.mapBack != null)
				{
					GameCanvas.mapBack.setSkySnow();
				}
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Upgrade_Item(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b != 7 && ScreenUpgrade.instance == null)
			{
				ScreenUpgrade.instance = new ScreenUpgrade(5, -1);
			}
			switch (b)
			{
			case 0:
			{
				string info2 = msg.reader().readUTF();
				if (ScreenUpgrade.isClickUpgradeItemToSucess)
				{
					ScreenUpgrade.stopUpgradeItemAuto();
				}
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info2);
				break;
			}
			case 1:
			{
				string info = msg.reader().readUTF();
				int num = msg.reader().readInt();
				short num2 = msg.reader().readShort();
				idItemUpgrade = msg.reader().readShort();
				mVector mVector2 = new mVector();
				if (num > 0)
				{
					iCommand iCommand2 = new iCommand(num + string.Empty, 1, 1, this);
					iCommand2.setFraCaption(AvMain.fraMoney, 1, 0, 0);
					mVector2.addElement(iCommand2);
				}
				if (num2 > 0)
				{
					iCommand iCommand3 = new iCommand(num2 + string.Empty, 1, 2, this);
					iCommand3.setFraCaption(AvMain.fraMoney, 1, 1, 0);
					mVector2.addElement(iCommand3);
				}
				iCommand o = new iCommand(T.close, 1, 0, this);
				mVector2.addElement(o);
				GameCanvas.Start_Normal_DiaLog(info, mVector2, isCmdClose: false);
				break;
			}
			case 2:
			{
				string showServer2 = msg.reader().readUTF();
				ScreenUpgrade.instance.showServer = showServer2;
				ScreenUpgrade.instance.Step = 1;
				if (ScreenUpgrade.isClickUpgradeItemToSucess)
				{
					ScreenUpgrade.isUpgradeItemSucess = true;
				}
				break;
			}
			case 3:
			{
				string showServer = msg.reader().readUTF();
				ScreenUpgrade.instance.showServer = showServer;
				ScreenUpgrade.instance.Step = 2;
				if (ScreenUpgrade.isClickUpgradeItemToSucess)
				{
					ScreenUpgrade.isUpgradeItemSucess = false;
				}
				break;
			}
			case 4:
			{
				short id3 = msg.reader().readShort();
				MainItem itemVec3 = MainItem.getItemVec(3, id3, Player.vecInventory);
				if (itemVec3 != null)
				{
					MainItem mainItem3 = new MainItem(3, itemVec3.ID, itemVec3.idIcon, 1, itemVec3.colorName, itemVec3.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[0] = mainItem3;
					ScreenUpgrade.instance.setDataUpgrade();
				}
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 5:
			{
				sbyte num3 = msg.reader().readByte();
				short id = msg.reader().readShort();
				if (num3 == 0)
				{
					ScreenUpgrade.mItemUpgrade[1] = null;
					ScreenUpgrade.valueLucky = 1;
				}
				else
				{
					MainItem itemVec = MainItem.getItemVec(7, id, Player.vecInventory);
					if (itemVec != null)
					{
						if (itemVec.typeMaterial == 4)
						{
							ScreenUpgrade.valueLucky = 10;
						}
						else if (itemVec.typeMaterial == 2)
						{
							ScreenUpgrade.valueLucky = 20;
						}
						MainItem mainItem = new MainItem(7, itemVec.ID, itemVec.idIcon, 1, itemVec.colorName, itemVec.LvUpgrade);
						ScreenUpgrade.mItemUpgrade[1] = mainItem;
					}
				}
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 6:
			{
				sbyte num4 = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				if (num4 == 0)
				{
					ScreenUpgrade.mItemUpgrade[2] = null;
				}
				else
				{
					MainItem itemVec2 = MainItem.getItemVec(7, id2, Player.vecInventory);
					if (itemVec2 != null)
					{
						MainItem mainItem2 = new MainItem(7, itemVec2.ID, itemVec2.idIcon, 1, itemVec2.colorName, itemVec2.LvUpgrade);
						ScreenUpgrade.mItemUpgrade[2] = mainItem2;
					}
				}
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 7:
				ScreenUpgrade.instance = new ScreenUpgrade(5, -1);
				ScreenUpgrade.instance.Show(GameCanvas.gameScr);
				break;
			case 15:
				if (MsgUpdateHeart.instance == null)
				{
					MsgUpdateHeart.instance = new MsgUpdateHeart();
				}
				MsgUpdateHeart.instance.Show(GameCanvas.gameScr);
				break;
			case 16:
			case 17:
			{
				string text = msg.reader().readUTF();
				if (MsgUpdateHeart.instance == null)
				{
					MsgUpdateHeart.instance = new MsgUpdateHeart();
				}
				MsgUpdateHeart.instance.updateStepUpgrade(b, text);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void UpgradeSuper_Item(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b != 7 && ScreenUpgrade.instance == null)
			{
				ScreenUpgrade.instance = new ScreenUpgrade(15, -1);
			}
			switch (b)
			{
			case 0:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				if (ScreenUpgrade.isClickUpgradeSuperItemToSucess)
				{
					ScreenUpgrade.stopUpgradeSuperItemAuto();
				}
				break;
			case 1:
			{
				string info = msg.reader().readUTF();
				int num2 = msg.reader().readInt();
				short num3 = msg.reader().readShort();
				idItemUpgrade = msg.reader().readShort();
				mVector mVector2 = new mVector();
				if (num2 > 0)
				{
					iCommand iCommand2 = new iCommand(num2 + string.Empty, 12, 1, this);
					iCommand2.setFraCaption(AvMain.fraMoney, 1, 0, 0);
					mVector2.addElement(iCommand2);
				}
				if (num3 > 0)
				{
					iCommand iCommand3 = new iCommand(num3 + string.Empty, 12, 2, this);
					iCommand3.setFraCaption(AvMain.fraMoney, 1, 1, 0);
					mVector2.addElement(iCommand3);
				}
				iCommand o = new iCommand(T.close, 1, 0, this);
				mVector2.addElement(o);
				GameCanvas.Start_Normal_DiaLog(info, mVector2, isCmdClose: false);
				break;
			}
			case 2:
			{
				string showServer2 = msg.reader().readUTF();
				ScreenUpgrade.instance.showServer = showServer2;
				ScreenUpgrade.instance.Step = 1;
				if (ScreenUpgrade.isClickUpgradeSuperItemToSucess)
				{
					ScreenUpgrade.isUpgradeSuperItemSucess = true;
				}
				break;
			}
			case 3:
			{
				string showServer = msg.reader().readUTF();
				ScreenUpgrade.instance.showServer = showServer;
				ScreenUpgrade.instance.Step = 2;
				if (ScreenUpgrade.isClickUpgradeSuperItemToSucess)
				{
					ScreenUpgrade.isUpgradeSuperItemSucess = false;
				}
				break;
			}
			case 4:
			{
				short id4 = msg.reader().readShort();
				MainItem itemVec4 = MainItem.getItemVec(3, id4, Player.vecInventory);
				if (itemVec4 != null)
				{
					MainItem mainItem4 = new MainItem(3, itemVec4.ID, itemVec4.idIcon, 1, itemVec4.colorName, itemVec4.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[0] = mainItem4;
					ScreenUpgrade.mItemUpgradeSaveTemp[0] = mainItem4;
				}
				short iD = msg.reader().readShort();
				short numPotion2 = msg.reader().readShort();
				MainMaterial mainMaterial = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + iD);
				MainItem mainItem5 = new MainItem(7, mainMaterial.idIcon, iD);
				mainItem5.numPotion = numPotion2;
				ScreenUpgrade.mItemUpgrade[4] = mainItem5;
				short iD2 = msg.reader().readShort();
				short numPotion3 = msg.reader().readShort();
				MainMaterial mainMaterial2 = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + iD2);
				MainItem mainItem6 = new MainItem(7, mainMaterial2.idIcon, iD2);
				mainItem6.numPotion = numPotion3;
				ScreenUpgrade.mItemUpgrade[5] = mainItem6;
				break;
			}
			case 5:
			{
				sbyte num4 = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				if (num4 == 0)
				{
					ScreenUpgrade.mItemUpgradeSaveTemp[1] = null;
					ScreenUpgrade.mItemUpgrade[1] = null;
					ScreenUpgrade.valueLucky = 1;
				}
				else
				{
					MainItem itemVec2 = MainItem.getItemVec(7, id2, Player.vecInventory);
					if (itemVec2 != null)
					{
						if (itemVec2.typeMaterial == 4)
						{
							ScreenUpgrade.valueLucky = 10;
						}
						else if (itemVec2.typeMaterial == 2)
						{
							ScreenUpgrade.valueLucky = 20;
						}
						MainItem mainItem2 = new MainItem(7, itemVec2.ID, itemVec2.idIcon, 1, itemVec2.colorName, itemVec2.LvUpgrade);
						ScreenUpgrade.mItemUpgrade[1] = mainItem2;
						ScreenUpgrade.mItemUpgradeSaveTemp[1] = mainItem2;
					}
				}
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 6:
			{
				sbyte num5 = msg.reader().readByte();
				short id3 = msg.reader().readShort();
				sbyte numPotion = msg.reader().readByte();
				if (num5 == 0)
				{
					ScreenUpgrade.mItemUpgradeSaveTemp[2] = null;
					ScreenUpgrade.mItemUpgrade[2] = null;
				}
				else
				{
					MainItem itemVec3 = MainItem.getItemVec(7, id3, Player.vecInventory);
					if (itemVec3 != null)
					{
						MainItem mainItem3 = new MainItem(7, itemVec3.ID, itemVec3.idIcon, 1, itemVec3.colorName, itemVec3.LvUpgrade);
						mainItem3.numPotion = numPotion;
						ScreenUpgrade.mItemUpgrade[2] = mainItem3;
						ScreenUpgrade.mItemUpgradeSaveTemp[2] = mainItem3;
					}
				}
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 14:
			{
				sbyte num = msg.reader().readByte();
				short id = msg.reader().readShort();
				if (num == 0)
				{
					ScreenUpgrade.mItemUpgrade[3] = null;
					ScreenUpgrade.mItemUpgradeSaveTemp[3] = null;
				}
				else
				{
					MainItem itemVec = MainItem.getItemVec(7, id, Player.vecInventory);
					if (itemVec != null)
					{
						MainItem mainItem = new MainItem(7, itemVec.ID, itemVec.idIcon, 1, itemVec.colorName, itemVec.LvUpgrade);
						ScreenUpgrade.mItemUpgrade[3] = mainItem;
						ScreenUpgrade.mItemUpgradeSaveTemp[3] = mainItem;
					}
				}
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 7:
				ScreenUpgrade.instance = new ScreenUpgrade(15, -1);
				ScreenUpgrade.instance.Show(GameCanvas.gameScr);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Upgrade_Dial(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz("nhan type upgrade dial =" + b);
			if (b != 7 && ScreenUpgrade.instance == null)
			{
				ScreenUpgrade.instance = new ScreenUpgrade(18, -1);
			}
			switch (b)
			{
			case 0:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				break;
			case 7:
				ScreenUpgrade.instance = new ScreenUpgrade(18, -1);
				ScreenUpgrade.instance.Show(GameCanvas.gameScr);
				break;
			case 4:
			{
				short id3 = msg.reader().readShort();
				MainItem itemVec3 = MainItem.getItemVec(3, id3, Player.vecInventory);
				if (itemVec3 != null)
				{
					MainItem mainItem3 = new MainItem(3, itemVec3.ID, itemVec3.idIcon, 1, itemVec3.colorName, itemVec3.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[0] = mainItem3;
				}
				sbyte b2 = msg.reader().readByte();
				for (int i = 1; i <= b2; i++)
				{
					short iD = msg.reader().readShort();
					short numPotion2 = msg.reader().readShort();
					MainMaterial mainMaterial = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + iD);
					MainItem mainItem4 = new MainItem(7, mainMaterial.idIcon, iD);
					mainItem4.numPotion = numPotion2;
					ScreenUpgrade.mItemUpgrade[ScreenUpgrade.mItemUpgrade.Length - i] = mainItem4;
				}
				int valueMonney_ = msg.reader().readInt();
				int valueMonney_2 = msg.reader().readInt();
				int valueMonney_3 = msg.reader().readInt();
				sbyte b3 = msg.reader().readByte();
				ScreenUpgrade.instance.valueTileRotCap = b3;
				ScreenUpgrade.instance.valueBaoHiem = 0;
				ScreenUpgrade.instance.valueMayMan = 0;
				ScreenUpgrade.instance.setInfo_money(b3, valueMonney_, valueMonney_2, valueMonney_3);
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 1:
			{
				string info = msg.reader().readUTF();
				idItemUpgrade = msg.reader().readShort();
				mVector mVector2 = new mVector();
				iCommand o = new iCommand(T.chapnhan, 18, 0, this);
				mVector2.addElement(o);
				iCommand o2 = new iCommand(T.close, 1, 0, this);
				mVector2.addElement(o2);
				GameCanvas.Start_Normal_DiaLog(info, mVector2, isCmdClose: false);
				break;
			}
			case 2:
			{
				string showServer2 = msg.reader().readUTF();
				ScreenUpgrade.instance.showServer = showServer2;
				ScreenUpgrade.instance.Step = 1;
				break;
			}
			case 3:
			{
				string showServer = msg.reader().readUTF();
				ScreenUpgrade.instance.showServer = showServer;
				ScreenUpgrade.instance.Step = 2;
				break;
			}
			case 5:
			{
				sbyte num2 = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				sbyte numPotion = msg.reader().readByte();
				if (num2 == 0)
				{
					ScreenUpgrade.mItemUpgrade[1] = null;
					ScreenUpgrade.valueLucky = 1;
				}
				else
				{
					MainItem itemVec2 = MainItem.getItemVec(7, id2, Player.vecInventory);
					if (itemVec2 != null)
					{
						MainItem mainItem2 = new MainItem(7, itemVec2.ID, itemVec2.idIcon, 1, itemVec2.colorName, itemVec2.LvUpgrade);
						mainItem2.numPotion = numPotion;
						ScreenUpgrade.mItemUpgrade[1] = mainItem2;
					}
				}
				ScreenUpgrade.instance.valueMayMan = msg.reader().readByte();
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			case 6:
			{
				sbyte num3 = msg.reader().readByte();
				short id4 = msg.reader().readShort();
				sbyte numPotion3 = msg.reader().readByte();
				if (num3 == 0)
				{
					ScreenUpgrade.mItemUpgrade[2] = null;
				}
				else
				{
					MainItem itemVec4 = MainItem.getItemVec(7, id4, Player.vecInventory);
					if (itemVec4 != null)
					{
						MainItem mainItem5 = new MainItem(7, itemVec4.ID, itemVec4.idIcon, 1, itemVec4.colorName, itemVec4.LvUpgrade);
						mainItem5.numPotion = numPotion3;
						ScreenUpgrade.mItemUpgrade[2] = mainItem5;
					}
				}
				ScreenUpgrade.instance.valueBaoHiem = msg.reader().readByte();
				ScreenUpgrade.instance.getMenuActionItem();
				mSystem.outz(" valueBaoHiem = " + ScreenUpgrade.instance.valueBaoHiem);
				break;
			}
			case 14:
			{
				sbyte num = msg.reader().readByte();
				short id = msg.reader().readShort();
				if (num == 0)
				{
					ScreenUpgrade.mItemUpgrade[3] = null;
				}
				else
				{
					MainItem itemVec = MainItem.getItemVec(7, id, Player.vecInventory);
					if (itemVec != null)
					{
						MainItem mainItem = new MainItem(7, itemVec.ID, itemVec.idIcon, 1, itemVec.colorName, itemVec.LvUpgrade);
						ScreenUpgrade.mItemUpgrade[3] = mainItem;
					}
				}
				ScreenUpgrade.instance.getMenuActionItem();
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Upgrade_Skin(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz("nhan type upgrade skin =" + b);
			switch (b)
			{
			case 0:
			{
				sbyte b9 = msg.reader().readByte();
				sbyte b10 = msg.reader().readByte();
				mSystem.outz("size skin " + b10);
				mVector mVector5 = new mVector();
				for (int k = 0; k < b10; k++)
				{
					short iD2 = msg.reader().readShort();
					string text4 = msg.reader().readUTF();
					string info3 = msg.reader().readUTF();
					short iDIcon2 = msg.reader().readShort();
					sbyte lvUpgrade2 = msg.reader().readByte();
					mSystem.outz(iD2 + " " + text4 + " " + iDIcon2 + " " + lvUpgrade2);
					ItemFashion itemFashion2 = new ItemFashion(iD2, iDIcon2, text4, info3, null);
					itemFashion2.isShop = false;
					itemFashion2.LvUpgrade = lvUpgrade2;
					itemFashion2.setInfoPotion(itemFashion2.info);
					if (itemFashion2.ID == Player.idFashion)
					{
						itemFashion2.addInfoFrist(T.daTrangBi, 4);
						itemFashion2.colorName = 4;
					}
					else
					{
						itemFashion2.addInfoFrist(T.dasuhuu, 1);
						itemFashion2.colorName = 1;
					}
					mVector5.addElement(itemFashion2);
				}
				b10 = msg.reader().readByte();
				mSystem.outz("size da kham " + b10);
				mVector mVector6 = new mVector();
				for (int l = 0; l < b10; l++)
				{
					b9 = msg.reader().readByte();
					short num4 = msg.reader().readShort();
					short num5 = msg.reader().readShort();
					string text5 = msg.reader().readUTF();
					short idIcon2 = msg.reader().readShort();
					mSystem.outz(b9 + " " + num4 + " " + num5 + " " + text5 + " " + idIcon2);
					switch (b9)
					{
					case 4:
					{
						Potion potion2 = new Potion(b9, num4, idIcon2, text5, 0);
						potion2.numPotion = num5;
						mVector6.addElement(potion2);
						break;
					}
					case 7:
					{
						MainMaterial mainMaterial2 = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + num4);
						MainMaterial o4 = new MainMaterial(7, (sbyte)num4, mainMaterial2.name, mainMaterial2.typeMaterial, (sbyte)mainMaterial2.idIcon, num5, mainMaterial2.price, mainMaterial2.priceRuby, mainMaterial2.isTrade);
						mVector6.addElement(o4);
						break;
					}
					}
				}
				SkinUpgradeScreen.instance = new SkinUpgradeScreen(22, -1, mVector5, mVector6);
				SkinUpgradeScreen.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 1:
			{
				sbyte b6 = msg.reader().readByte();
				short id = msg.reader().readShort();
				sbyte perSuc = msg.reader().readByte();
				sbyte b7 = msg.reader().readByte();
				sbyte b8 = msg.reader().readByte();
				mSystem.outz("nhan upgrade skin " + b6 + " " + id + " " + perSuc + " posItem " + b7 + " " + b8);
				if (b8 == 0)
				{
					ScreenUpgrade.mItemUpgrade[b7] = null;
				}
				else
				{
					MainItem mainItem = null;
					mainItem = ((b6 != 105) ? MainItem.getItemVec(b6, id, SkinUpgradeScreen.vecDa) : MainItem.getItemVec(b6, id, SkinUpgradeScreen.vecSkin));
					if (mainItem != null)
					{
						MainItem mainItem2 = new MainItem(b6, mainItem.ID, mainItem.idIcon, 1, mainItem.colorName, mainItem.LvUpgrade);
						mainItem2.perSuc = perSuc;
						ScreenUpgrade.mItemUpgrade[b7] = mainItem2;
					}
				}
				int valueMonney_ = msg.reader().readInt();
				short valueMonney_2 = msg.reader().readShort();
				int valueMonney_3 = msg.reader().readInt();
				mSystem.outz("set money " + valueMonney_ + " " + valueMonney_2 + " " + valueMonney_3);
				SkinUpgradeScreen.instance.setInfo_money(valueMonney_, valueMonney_2, valueMonney_3);
				SkinUpgradeScreen.instance.getMenuActionItem();
				SkinUpgradeScreen.instance.SetValueMayMan();
				break;
			}
			case 4:
			{
				string info2 = msg.reader().readUTF();
				idItemUpgrade = msg.reader().readShort();
				mVector mVector4 = new mVector();
				iCommand o2 = new iCommand(T.chapnhan, 15, 0, SkinUpgradeScreen.instance);
				mVector4.addElement(o2);
				iCommand o3 = new iCommand(T.close, 1, 0, this);
				mVector4.addElement(o3);
				GameCanvas.Start_Normal_DiaLog(info2, mVector4, isCmdClose: false);
				break;
			}
			case 3:
			{
				sbyte b4 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				sbyte b5 = msg.reader().readByte();
				string text3 = msg.reader().readUTF();
				mSystem.outz("ACTION_SUCC " + b4 + " " + num3 + " " + b5 + " " + text3);
				SkinUpgradeScreen.instance.showServer = text3;
				if (b4 == 0)
				{
					SkinUpgradeScreen.instance.Step = 1;
					if (ScreenUpgrade.isClickUpgradeSkinToSucess)
					{
						SkinUpgradeScreen.stopUpgradeSkin();
					}
				}
				else
				{
					SkinUpgradeScreen.instance.Step = 2;
					if (text3.IndexOf(T.upgradeFail) > -1 && ScreenUpgrade.isClickUpgradeSkinToSucess)
					{
						ScreenUpgrade.isContinueUpgradeSkin = true;
					}
				}
				break;
			}
			case 5:
			{
				mSystem.outz("nhan type upgrade skin = ACTION_UPDATE_LIST" + b);
				sbyte b2 = msg.reader().readByte();
				sbyte b3 = msg.reader().readByte();
				mSystem.outz("size skin " + b3);
				mVector mVector2 = new mVector();
				for (int i = 0; i < b3; i++)
				{
					short iD = msg.reader().readShort();
					string text = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					short iDIcon = msg.reader().readShort();
					sbyte lvUpgrade = msg.reader().readByte();
					mSystem.outz(iD + " " + text + " " + iDIcon + " " + lvUpgrade);
					ItemFashion itemFashion = new ItemFashion(iD, iDIcon, text, info, null);
					itemFashion.isShop = false;
					itemFashion.LvUpgrade = lvUpgrade;
					itemFashion.setInfoPotion(itemFashion.info);
					if (itemFashion.ID == Player.idFashion)
					{
						itemFashion.addInfoFrist(T.daTrangBi, 4);
						itemFashion.colorName = 4;
					}
					else
					{
						itemFashion.addInfoFrist(T.dasuhuu, 1);
						itemFashion.colorName = 1;
					}
					mVector2.addElement(itemFashion);
				}
				b3 = msg.reader().readByte();
				mSystem.outz("size da kham " + b3);
				mVector mVector3 = new mVector();
				for (int j = 0; j < b3; j++)
				{
					b2 = msg.reader().readByte();
					short num = msg.reader().readShort();
					short num2 = msg.reader().readShort();
					string text2 = msg.reader().readUTF();
					short idIcon = msg.reader().readShort();
					mSystem.outz(num + " " + num2 + " " + text2 + " " + idIcon);
					switch (b2)
					{
					case 4:
					{
						Potion potion = new Potion(b2, num, idIcon, text2, 0);
						potion.numPotion = num2;
						mVector3.addElement(potion);
						break;
					}
					case 7:
					{
						MainMaterial mainMaterial = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + num);
						MainMaterial o = new MainMaterial(7, (sbyte)num, mainMaterial.name, mainMaterial.typeMaterial, (sbyte)mainMaterial.idIcon, num2, mainMaterial.price, mainMaterial.priceRuby, mainMaterial.isTrade);
						mVector3.addElement(o);
						break;
					}
					}
				}
				SkinUpgradeScreen.vecSkin = mVector2;
				SkinUpgradeScreen.vecDa = mVector3;
				break;
			}
			case 6:
				SkinUpgradeScreen.instance.valueMayMan = msg.reader().readByte();
				mSystem.outz("valueMayMan " + SkinUpgradeScreen.instance.valueMayMan);
				break;
			}
			mSystem.outz("DONE nhan type upgrade skin =" + b);
		}
		catch (Exception)
		{
		}
	}

	public void Split_Join_Item(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
				Split_Item(msg);
				break;
			case 1:
				Join_Item(msg);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Split_Item(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b != 3 && SplitScreen.instance == null)
			{
				SplitScreen.instance = new SplitScreen(0, -1);
			}
			switch (b)
			{
			case 0:
			{
				short id2 = msg.reader().readShort();
				sbyte b3 = msg.reader().readByte();
				short num2 = msg.reader().readShort();
				MainItem itemVec2 = MainItem.getItemVec(b3, id2, Player.vecInventory);
				if (itemVec2 != null)
				{
					sbyte color2 = itemVec2.colorName;
					if (b3 != 3)
					{
						color2 = 5;
					}
					MainItem mainItem2 = new MainItem(b3, itemVec2.ID, itemVec2.idIcon, num2, color2, itemVec2.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[0] = mainItem2;
					ScreenUpgrade.mItemUpgrade[1] = null;
				}
				break;
			}
			case 1:
			{
				string showServer = msg.reader().readUTF();
				SplitScreen.instance.showServer = showServer;
				short id = msg.reader().readShort();
				sbyte b2 = msg.reader().readByte();
				short num = msg.reader().readShort();
				MainItem itemVec = MainItem.getItemVec(b2, id, Player.vecInventory);
				if (itemVec != null)
				{
					sbyte color = itemVec.colorName;
					if (b2 != 3)
					{
						color = 5;
					}
					MainItem mainItem = new MainItem(itemVec.typeObject, itemVec.ID, itemVec.idIcon, num, color, itemVec.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[1] = mainItem;
					ScreenUpgrade.mItemUpgrade[1].isRemove = true;
					SplitScreen.instance.Step = 1;
				}
				break;
			}
			case 3:
				SplitScreen.instance = new SplitScreen(0, -1);
				SplitScreen.instance.Show(GameCanvas.gameScr);
				break;
			case 2:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Join_Item(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				string info = msg.reader().readUTF();
				sbyte b2 = msg.reader().readByte();
				MainItem[] array = new MainItem[b2];
				for (int i = 0; i < b2; i++)
				{
					array[i] = new MainItem();
					short num3 = msg.reader().readShort();
					sbyte b3 = msg.reader().readByte();
					short num4 = msg.reader().readShort();
					MainMaterial mainMaterial2 = null;
					if (b3 == 7)
					{
						mainMaterial2 = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + num3);
					}
					if (mainMaterial2 != null)
					{
						sbyte color2 = mainMaterial2.colorName;
						if (b3 != 3)
						{
							color2 = 5;
						}
						MainItem mainItem2 = new MainItem(b3, mainMaterial2.ID, mainMaterial2.idIcon, num4, color2, mainMaterial2.LvUpgrade);
						array[i] = mainItem2;
					}
				}
				if (ScreenJoinItem.instance == null)
				{
					ScreenJoinItem.instance = new ScreenJoinItem();
				}
				ScreenJoinItem.instance.setInfoJoin(info, array);
				ScreenJoinItem.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 1:
			{
				GameCanvas.end_Dialog();
				sbyte ishardcode = 0;
				msg.reader().readUTF();
				short num = msg.reader().readShort();
				sbyte b = msg.reader().readByte();
				if (b == 7 && num == 8)
				{
					ishardcode = 1;
				}
				short num2 = msg.reader().readShort();
				MainMaterial mainMaterial = null;
				MainItem mainItem = null;
				if (b == 7)
				{
					mainMaterial = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + num);
				}
				if (mainMaterial != null)
				{
					sbyte color = mainMaterial.colorName;
					if (b != 3)
					{
						color = 5;
					}
					mainItem = new MainItem(b, mainMaterial.ID, mainMaterial.idIcon, num2, color, mainMaterial.LvUpgrade);
				}
				if (ScreenJoinItem.instance == null)
				{
					ScreenJoinItem.instance = new ScreenJoinItem();
				}
				if (mainItem != null)
				{
					ScreenJoinItem.instance.setItemJoin(mainItem, ishardcode);
				}
				break;
			}
			case 2:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Trade(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			sbyte b2 = msg.reader().readByte();
			if (b != 0 && TradeScreen.instance == null)
			{
				TradeScreen.instance = new TradeScreen(6, 1);
			}
			switch (b)
			{
			case 0:
			{
				string nameObjTrade = msg.reader().readUTF();
				TradeScreen.instance = new TradeScreen(6, 1);
				TradeScreen.instance.setNameObjTrade(nameObjTrade);
				TradeScreen.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 1:
			{
				sbyte b3 = msg.reader().readByte();
				if (b3 == 6)
				{
					int num = msg.reader().readInt();
					switch (b2)
					{
					case 0:
						TradeScreen.instance.objMain.beli = num;
						break;
					case 1:
						TradeScreen.instance.objTrade.beli = num;
						break;
					}
					break;
				}
				sbyte b4 = msg.reader().readByte();
				MainItem mainItem = null;
				switch (b4)
				{
				case 1:
					if (b3 == 3)
					{
						mainItem = readUpdateItem(msg, isShop: false);
					}
					if (b3 == 7)
					{
						mainItem = readUpdateMaterial(msg, isShop: false);
					}
					if (b3 == 4)
					{
						mainItem = readUpdatePotion(msg, isShop: false, b3);
					}
					break;
				case 0:
				{
					short iD2 = msg.reader().readShort();
					mainItem = new MainItem(b3, iD2, 0, 1, 0, 0);
					break;
				}
				}
				if (mainItem != null)
				{
					TradeScreen.instance.setItem(b2, b4, mainItem);
				}
				break;
			}
			case 3:
				switch (b2)
				{
				case 0:
					TradeScreen.instance.objMain.isTrade = 1;
					TradeScreen.instance.setLock();
					break;
				case 1:
					TradeScreen.instance.objTrade.isTrade = 1;
					break;
				}
				break;
			case 4:
				switch (b2)
				{
				case 0:
					TradeScreen.instance.objMain.isTrade = 2;
					TradeScreen.instance.setTrade();
					break;
				case 1:
					TradeScreen.instance.objTrade.isTrade = 2;
					break;
				}
				break;
			case 2:
			{
				string strChatPopup = msg.reader().readUTF();
				switch (b2)
				{
				case 0:
					TradeScreen.instance.objMain.strChatPopup = strChatPopup;
					break;
				case 1:
					TradeScreen.instance.objTrade.strChatPopup = strChatPopup;
					break;
				}
				break;
			}
			case 5:
			{
				string info = msg.reader().readUTF();
				GameCanvas.gameScr.Show();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				break;
			}
			case 6:
			{
				short iD = msg.reader().readShort();
				string name = msg.reader().readUTF();
				InfoMemList.setTypeEvent(iD, 4, name, T.eventTrade, 0, 0);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Ship(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			string info = msg.reader().readUTF();
			switch (b)
			{
			case 0:
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				short iD = msg.reader().readShort();
				MainTabShop.itemShipCur = new MainItem(4, -1, iD);
				if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					MainTabShop.isUpdateItemShip = true;
				}
				break;
			}
			case 1:
				if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				break;
			case 2:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Help_From_Server(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			string original = msg.reader().readUTF();
			MainObject mainObject = MainObject.get_Object(num, 2);
			if (mainObject != null && !mainObject.isRemove)
			{
				MainObject.strHelp = mFont.split(original, "\b");
				MainObject.StepHelp = 0;
				mVector mVector2 = new mVector();
				iCommand o = new iCommand(T.next, 3, num, this);
				mVector2.addElement(o);
				GameCanvas.menu.startAt_NPC(mVector2, MainObject.strHelp[0], num, 2, isHelp: true, 2, isDynamic: false);
			}
		}
		catch (Exception)
		{
		}
	}

	public void ghost(Message msg)
	{
		try
		{
			if (msg.reader().readByte() <= 0)
			{
				Player.isGhost = false;
				GameScreen.addEffectEnd(84, 0, GameScreen.player.x, GameScreen.player.y - 70, (sbyte)GameScreen.player.Dir, GameScreen.player);
				ObjectData.hashImageItemOther.remove(string.Empty + (short)999);
				return;
			}
			ObjectData.hashImageItemOther.remove(string.Empty + (short)999);
			GameScreen.player.resetAction();
			Player.isGhost = true;
			Player.vecGhostInput.removeAllElements();
			InfoShowNotify infoShowNotify = new InfoShowNotify(T.ghost, 0);
			mFont mFont2 = null;
			mFont2 = AvMain.setTextColor(0);
			infoShowNotify.setFont(mFont2);
			Interface_Game.addInfoServer(infoShowNotify);
			ObjectData.getImageAll(999, ObjectData.hashImageItemOther, 9000);
		}
		catch (Exception)
		{
		}
	}

	public void Boat_In_Map(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				Boat boat = new Boat(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), 0, 2);
				yBoatMap = boat.y;
				sbyte b2 = msg.reader().readByte();
				if (b2 > 0)
				{
					short[] array = new short[b2];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = msg.reader().readShort();
					}
					boat.mPartImage = array;
				}
				GameScreen.addBoatVec_And_mItem(boat, isSort: false);
			}
			if (GameScreen.player.boatSea != null)
			{
				for (int k = 0; k < GameScreen.vecBoat.size(); k++)
				{
					Boat boat2 = (Boat)GameScreen.vecBoat.elementAt(k);
					if (boat2.ID == GameScreen.player.boatSea.ID)
					{
						GameScreen.removeBoatVec_And_mItem(boat2);
						break;
					}
				}
			}
			CRes.quickSort(LoadMap.mItemMap[3]);
		}
		catch (Exception)
		{
		}
	}

	public void Ok_Change_Map_Link(Message msg)
	{
		try
		{
			if (Player.AutoFireCur <= 0 && GameScreen.player.Action == 0)
			{
				GlobalService.gI().OkChangeMapLink();
			}
		}
		catch (Exception)
		{
		}
	}

	public void CMD_test(Message msg)
	{
		try
		{
			mtest = new sbyte[msg.reader().available()];
			msg.reader().read(ref mtest);
		}
		catch (Exception)
		{
		}
	}

	public void Frist_Login(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			mSystem.outz("CMd -57 team str=" + text);
			ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
			DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
			dataOutputStream.writeUTF(text);
			dataOutputStream.writeByte((sbyte)GameCanvas.IndexServer);
			CRes.saveRMS("MAIN_frist_login", byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
			GameMidlet.delRMS("MAIN_user_pass");
			GameCanvas.loginScr.tfUser.setText(string.Empty);
			GameCanvas.loginScr.tfPass.setText(string.Empty);
			GameMidlet.delRMS("MAIN_user_last");
			SaveRms.userLast = string.Empty;
		}
		catch (Exception)
		{
		}
	}

	public void Input_server(Message msg)
	{
		try
		{
			short id = msg.reader().readShort();
			string name = msg.reader().readUTF();
			string[] array = new string[msg.reader().readByte()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = msg.reader().readUTF();
			}
			GameCanvas.subDialog = GameCanvas.Start_Input_Dialog_Server(array, name, id);
		}
		catch (Exception)
		{
		}
	}

	public void Update_User_Ok(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			string text2 = msg.reader().readUTF();
			GameCanvas.saveRms.saveUserPass(text, text2);
			GameMidlet.delRMS("MAIN_frist_login");
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.registerOk + "\n" + T.username + ": " + text + "\n" + T.password + ": " + text2);
		}
		catch (Exception)
		{
		}
	}

	public void Buy_Item_Shop(Message msg)
	{
		try
		{
			short id = msg.reader().readShort();
			sbyte type = msg.reader().readByte();
			if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
			{
				for (int i = 0; i < GameCanvas.tabShopScr.vecTabs.size(); i++)
				{
					((MainTab)GameCanvas.tabShopScr.vecTabs.elementAt(i)).updateBuyItem(id, type);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void CountDown_Ticket(Message msg)
	{
		try
		{
			sbyte num = msg.reader().readByte();
			int countDown = msg.reader().readInt();
			if (num == 0)
			{
				MainTab.CDTicket.setCountDown(countDown);
			}
			if (num == 1)
			{
				MainTab.CDKeyBoss.setCountDown(countDown);
			}
			if (num == 2)
			{
				MainTab.CDPvP.setCountDown(countDown);
			}
			if (num == 3)
			{
				MainTab.CDx2XP.setCountDown(countDown);
			}
		}
		catch (Exception)
		{
		}
	}

	public void update_Part_Boat(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject == null)
			{
				return;
			}
			short[] array = new short[msg.reader().readByte()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = msg.reader().readShort();
			}
			mainObject.myBoat = array;
			if (mainObject.boatSea != null && mainObject.boatSea.ID == mainObject.ID)
			{
				mainObject.boatSea.setPartImage(array, mainObject.typePirate);
			}
			if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
			{
				for (int j = 0; j < GameCanvas.tabShopScr.vecTabs.size(); j++)
				{
					((MainTab)GameCanvas.tabShopScr.vecTabs.elementAt(j)).updateTrangBi();
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void login_Ok(Message msg)
	{
		if (ListChar_Screen.IndexCharSelected >= 0)
		{
			GlobalService.gI().get_DATA(3);
		}
		GameMidlet.loginPlus();
		GameMidlet.loginOk();
		MsgDialog.isAuroReconect = false;
	}

	public void PvP(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (PvPScreen.instance == null)
			{
				PvPScreen.instance = new PvPScreen();
			}
			switch (b)
			{
			case 3:
			{
				string name = msg.reader().readUTF();
				sbyte clazz = msg.reader().readByte();
				Other_Player other_Player = new Other_Player(0, 0, name, 0, 0);
				other_Player.clazz = clazz;
				PvPScreen.instance.setObjPvp(other_Player);
				break;
			}
			case 4:
				if (msg.reader().readByte() == 0)
				{
					PvPScreen.instance.isMeReady = true;
				}
				else
				{
					PvPScreen.instance.isOtherReady = true;
				}
				break;
			case 6:
			{
				string mStr = msg.reader().readUTF();
				PvPScreen.instance.setMStr(mStr);
				break;
			}
			case 0:
				PvPScreen.instance.numPlayerWait = msg.reader().readShort();
				break;
			}
			PvPScreen.instance.setAction(b);
		}
		catch (Exception)
		{
		}
	}

	public void UpdateNameNPC(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 2);
			if (mainObject != null)
			{
				mainObject.nameGiaotiep = msg.reader().readUTF();
			}
		}
		catch (Exception)
		{
		}
	}

	public void Buy_Potion_Ok(Message msg)
	{
		try
		{
			MsgDialog.addEff(new EffectNum(msg.reader().readUTF(), 0, 0, 7));
		}
		catch (Exception)
		{
		}
	}

	public void Party_Buff(Message msg)
	{
		try
		{
			Player.giamCountDownParty = 0;
			GameScreen.player.vecAllInfoParty.removeAllElements();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				MainInfoItem mainInfoItem = new MainInfoItem(msg.reader().readByte(), msg.reader().readShort());
				GameScreen.player.vecAllInfoParty.addElement(mainInfoItem);
				if (mainInfoItem.id == 25)
				{
					Player.giamCountDownParty = 0;
				}
			}
			Player.SetGiamCountDown();
		}
		catch (Exception)
		{
		}
	}

	public void UpdatePvpPoint(Message msg)
	{
		try
		{
			GameScreen.player.PointPvP = msg.reader().readInt();
			GameScreen.player.mValuePvP[0] = msg.reader().readInt();
			GameScreen.player.mValuePvP[1] = msg.reader().readInt();
		}
		catch (Exception)
		{
		}
	}

	public void RebuildItem(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b != 0 && SplitScreen.instance == null)
			{
				return;
			}
			switch (b)
			{
			case 0:
				SplitScreen.instance = new SplitScreen(msg.reader().readByte(), -1);
				SplitScreen.instance.Show(GameCanvas.gameScr);
				return;
			case 1:
			{
				sbyte b6 = 0;
				short id3 = msg.reader().readShort();
				sbyte b7 = msg.reader().readByte();
				if ((SplitScreen.instance.typeRebuild == 1 || SplitScreen.instance.typeRebuild == 10 || SplitScreen.instance.typeRebuild == 11) && b7 != 3)
				{
					b6 = 1;
				}
				short num4 = msg.reader().readShort();
				MainItem itemVec3 = MainItem.getItemVec(b7, id3, Player.vecInventory);
				if (itemVec3 != null)
				{
					sbyte color2 = itemVec3.colorName;
					if (b7 != 3)
					{
						color2 = 5;
					}
					MainItem mainItem3 = new MainItem(b7, itemVec3.ID, itemVec3.idIcon, num4, color2, itemVec3.LvUpgrade);
					mainItem3.numLoKham = itemVec3.numLoKham;
					ScreenUpgrade.mItemUpgrade[b6] = mainItem3;
				}
				SplitScreen.instance.setSetItem();
				return;
			}
			case 2:
			{
				short num2 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				for (int i = 0; i < ScreenUpgrade.mItemUpgrade.Length; i++)
				{
					if (ScreenUpgrade.mItemUpgrade[i].ID == num2 && ScreenUpgrade.mItemUpgrade[i].typeObject == b4)
					{
						ScreenUpgrade.mItemUpgrade[i] = null;
					}
				}
				return;
			}
			case 3:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				return;
			case 4:
			{
				string showServer6 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer6;
				SplitScreen.instance.Step = 1;
				return;
			}
			case 5:
			{
				string showServer2 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				msg.reader().readShort();
				short id2 = msg.reader().readShort();
				short num3 = msg.reader().readShort();
				sbyte b5 = msg.reader().readByte();
				MainItem itemVec2 = MainItem.getItemVec(b5, id2, Player.vecInventory);
				if (itemVec2 != null && num3 > 0)
				{
					MainItem mainItem2 = new MainItem(b5, itemVec2.ID, itemVec2.idIcon, num3, itemVec2.colorName, itemVec2.LvUpgrade);
					mainItem2.isRemove = true;
					ScreenUpgrade.mItemUpgrade[1] = mainItem2;
				}
				SplitScreen.instance.showServer = showServer2;
				SplitScreen.instance.Step = 1;
				return;
			}
			case 6:
			{
				string showServer3 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer3;
				SplitScreen.instance.Step = 1;
				return;
			}
			case 7:
			{
				string showServer = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer;
				SplitScreen.instance.Step = 1;
				return;
			}
			case 20:
			case 21:
			case 22:
			case 23:
			{
				string showServer5 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer5;
				if (b == 20 || b == 22)
				{
					SplitScreen.instance.Step = 1;
				}
				else
				{
					SplitScreen.instance.Step = 3;
				}
				return;
			}
			case 25:
			{
				string showServer4 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer4;
				SplitScreen.instance.Step = 1;
				return;
			}
			case 28:
			case 29:
			{
				sbyte b2 = 0;
				if (b == 29)
				{
					b2 = 1;
				}
				short id = msg.reader().readShort();
				sbyte b3 = msg.reader().readByte();
				short num = msg.reader().readShort();
				sbyte tile = msg.reader().readByte();
				MainItem itemVec = MainItem.getItemVec(b3, id, Player.vecInventory);
				if (itemVec != null)
				{
					sbyte color = itemVec.colorName;
					if (b3 != 3)
					{
						color = 5;
					}
					MainItem mainItem = new MainItem(b3, itemVec.ID, itemVec.idIcon, num, color, itemVec.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[b2] = mainItem;
				}
				SplitScreen.instance.tile = tile;
				return;
			}
			}
			switch (b)
			{
			case 25:
			{
				string showServer10 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer10;
				SplitScreen.instance.Step = 1;
				break;
			}
			case 27:
			case 30:
			{
				string showServer9 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer9;
				if (b == 30)
				{
					SplitScreen.instance.Step = 3;
				}
				else
				{
					SplitScreen.instance.Step = 1;
				}
				break;
			}
			case 31:
			{
				string showServer8 = msg.reader().readUTF();
				SplitScreen.instance.idItem9x = msg.reader().readShort();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer8;
				SplitScreen.instance.Step = 1;
				break;
			}
			case 32:
			{
				string showServer7 = msg.reader().readUTF();
				SplitScreen.instance.updateNewUpgrade();
				SplitScreen.instance.showServer = showServer7;
				SplitScreen.instance.Step = 3;
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Diaolog_time(Message msg)
	{
		try
		{
			string info = msg.reader().readUTF();
			short time = msg.reader().readShort();
			GameCanvas.Start_Time_DiaLog(info, isCmdClose: true, time, null);
		}
		catch (Exception)
		{
		}
	}

	public void updateNumPlayerMap(Message msg)
	{
		try
		{
			LoadMap.numPlayerMap = msg.reader().readByte();
			LoadMap.maxnumPlayerMap = msg.reader().readByte();
		}
		catch (Exception)
		{
		}
	}

	public void Auto_Revice(Message msg)
	{
		try
		{
			if (msg.reader().readByte() != 1)
			{
				return;
			}
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			int timeSafe = msg.reader().readInt();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null)
			{
				if (mainObject == GameScreen.player && Player.AutoFireCur == 0)
				{
					Player.AutoFireCur = 1;
				}
				mainObject.timeSafe = timeSafe;
				mainObject.timeBeginSafe = GameCanvas.timeNow;
			}
		}
		catch (Exception)
		{
		}
	}

	public void List_Player_Check(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			IDDialogPhoBang = msg.reader().readShort();
			switch (b)
			{
			case 0:
			{
				ListDungeon.isKey = false;
				ListDungeon.vecDungeon.removeAllElements();
				string name = msg.reader().readUTF();
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < b2; j++)
				{
					InfoMemList infoMemList2 = new InfoMemList(msg.reader().readShort());
					infoMemList2.updateData(msg.reader().readUTF(), msg.reader().readShort(), 0, 0);
					if (j == 0)
					{
						infoMemList2.typeOnline = 1;
						if (infoMemList2.id == GameScreen.player.ID)
						{
							ListDungeon.isKey = true;
						}
					}
					ListDungeon.vecDungeon.addElement(infoMemList2);
				}
				ListDungeon.instance = new ListDungeon(-4, ListDungeon.vecDungeon, name);
				ListDungeon.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 1:
			{
				short num2 = msg.reader().readShort();
				if (ListDungeon.vecDungeon == null)
				{
					break;
				}
				for (int k = 0; k < ListDungeon.vecDungeon.size(); k++)
				{
					InfoMemList infoMemList3 = (InfoMemList)ListDungeon.vecDungeon.elementAt(k);
					if (infoMemList3.id == num2)
					{
						infoMemList3.typeOnline = 1;
						break;
					}
				}
				break;
			}
			case 2:
			{
				string info = msg.reader().readUTF();
				ListDungeon.vecDungeon.removeAllElements();
				if (GameCanvas.currentScreen != GameCanvas.gameScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				break;
			}
			case 3:
			{
				short num = msg.reader().readShort();
				if (ListDungeon.vecDungeon == null)
				{
					break;
				}
				for (int i = 0; i < ListDungeon.vecDungeon.size(); i++)
				{
					InfoMemList infoMemList = (InfoMemList)ListDungeon.vecDungeon.elementAt(i);
					if (infoMemList.id == num)
					{
						infoMemList.typeOnline = 2;
						break;
					}
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Red_Line(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				sbyte b = msg.reader().readByte();
				MapOff_RedLine.setTypeMoveredLine(0);
				mImgCapredLine = new mImage[b];
				for (int i = 0; i < b; i++)
				{
					sbyte[] data = new sbyte[msg.reader().readInt()];
					msg.reader().read(ref data);
					mImgCapredLine[i] = mImage.createImage(data, 0, data.Length);
				}
				MapOff_RedLine.timeRedline = msg.reader().readInt();
				MapOff_RedLine.timebegin = GameCanvas.timeNow;
				Player.indexAudition = -1;
				MapOff_RedLine.tickSendDie = 0;
				break;
			}
			case 1:
				mImgCapredLine = null;
				Interface_Game.addInfoPlayerNormal(T.RedLineFail, mFont.tahoma_7_white);
				Player.indexAudition = -1;
				MapOff_RedLine.setTypeMoveredLine(1);
				break;
			case 2:
				mImgCapredLine = null;
				Interface_Game.addInfoPlayerNormal(T.RedLineFinish, mFont.tahoma_7_white);
				Player.indexAudition = -1;
				MapOff_RedLine.setTypeMoveredLine(2);
				break;
			case 3:
			{
				sbyte b2 = msg.reader().readByte();
				GameScreen.player.typeMoveGotoSky = 1;
				mImgCapredLine = new mImage[b2];
				for (int j = 0; j < b2; j++)
				{
					sbyte[] data2 = new sbyte[msg.reader().readInt()];
					msg.reader().read(ref data2);
					mImgCapredLine[j] = mImage.createImage(data2, 0, data2.Length);
				}
				MapOff_RedLine.timeRedline = msg.reader().readInt();
				MapOff_RedLine.timebegin = GameCanvas.timeNow;
				Player.indexAudition = -1;
				MapOff_RedLine.tickSendDie = 0;
				break;
			}
			case 5:
				mImgCapredLine = null;
				Interface_Game.addInfoPlayerNormal(T.gotoSkyFinish, mFont.tahoma_7_white);
				Player.indexAudition = -1;
				if (LoadMap.specMap == 8)
				{
					MapGotoSky.setTypeMoveredLine(3);
				}
				else if (LoadMap.specMap == 12)
				{
					MapGotoGod.setTypeMoveredLine(3);
				}
				break;
			case 4:
				mImgCapredLine = null;
				Interface_Game.addInfoPlayerNormal(T.gotoSkyFail, mFont.tahoma_7_white);
				Player.indexAudition = -1;
				MapGotoSky.setTypeMoveredLine(2);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void TimeShow(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			switch (b)
			{
			case 0:
			case 2:
			{
				short countDown2 = msg.reader().readShort();
				string strInfo = msg.reader().readUTF();
				Interface_Game.watchMap.setCountDown(countDown2);
				Interface_Game.watchMap.strInfo = strInfo;
				Interface_Game.watchMap.typeTime = b;
				break;
			}
			case 1:
				Interface_Game.maxHPMap = msg.reader().readInt();
				Interface_Game.HPMap = msg.reader().readInt();
				break;
			case 3:
			{
				short countDown3 = msg.reader().readShort();
				string strInfo2 = msg.reader().readUTF();
				Interface_Game.watchRevice.setCountDown(countDown3);
				Interface_Game.watchRevice.strInfo = strInfo2;
				Interface_Game.watchRevice.typeTime = b;
				break;
			}
			case 4:
			{
				Interface_Game.watchMap.typeTime = b;
				short countDown4 = msg.reader().readShort();
				sbyte valueLeft2 = msg.reader().readByte();
				sbyte valueright2 = msg.reader().readByte();
				Interface_Game.watchMap.setCountDown(countDown4);
				Interface_Game.watchMap.valueLeft = valueLeft2;
				Interface_Game.watchMap.valueright = valueright2;
				break;
			}
			case 5:
			{
				if (MotherCanvas.w >= 300)
				{
					GameScreen.isPvPNew = true;
					Interface_Game.watchMap.typeTime = b;
				}
				else
				{
					Interface_Game.watchMap.typeTime = 4;
				}
				short countDown = msg.reader().readShort();
				sbyte valueLeft = msg.reader().readByte();
				sbyte valueright = msg.reader().readByte();
				Interface_Game.watchMap.setCountDown(countDown);
				Interface_Game.watchMap.valueLeft = valueLeft;
				Interface_Game.watchMap.valueright = valueright;
				Interface_Game.maxWin = msg.reader().readByte();
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void UpdateInfoMaincharInfo(Message msg)
	{
		try
		{
			Player.idFashion = msg.reader().readShort();
			Player.giamCountDownAtt = msg.reader().readShort();
			Player.SetGiamCountDown();
			Player.TangManaUseSkill = msg.reader().readShort();
		}
		catch (Exception)
		{
		}
	}

	public void Clan(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
				if (GameScreen.player.clan == null)
				{
					GameScreen.player.clan = new MainClan(msg.reader().readShort(), msg.reader().readUTF());
					break;
				}
				GameScreen.player.clan.ID = msg.reader().readShort();
				GameScreen.player.clan.name = msg.reader().readUTF();
				break;
			case 1:
			{
				if (GameScreen.player.clan == null)
				{
					GlobalService.gI().Clan_CMD(9, string.Empty, 0, 0);
					break;
				}
				string name2 = msg.reader().readUTF();
				sbyte b3 = msg.reader().readByte();
				InfoMemList memInCLan2 = Clan_Screen.getMemInCLan(name2);
				if (memInCLan2 != null)
				{
					memInCLan2.chucInClan = b3;
					if (memInCLan2.name.CompareTo(GameScreen.player.name) == 0)
					{
						MainClan.UpdateRankMe(b3);
					}
				}
				break;
			}
			case 2:
				if (GameScreen.player.clan == null)
				{
					GlobalService.gI().Clan_CMD(9, string.Empty, 0, 0);
					break;
				}
				GameScreen.player.clan.setData(msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readByte(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readUTF());
				if (Player.isGetDataClan == 0)
				{
					if (GameCanvas.ClanScr == null)
					{
						GameCanvas.ClanScr = new Clan_Screen(GameScreen.player.clan);
					}
					GameCanvas.ClanScr.Show(GameCanvas.gameScr);
					Player.isGetDataClan = 1;
				}
				GameScreen.player.clan.trungsinh = msg.reader().readByte();
				GameScreen.player.clan.countAction = msg.reader().readInt();
				break;
			case 3:
			{
				if (GameScreen.player.clan == null)
				{
					GlobalService.gI().Clan_CMD(9, string.Empty, 0, 0);
					break;
				}
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < b2; j++)
				{
					readMemInCLan(msg);
				}
				Clan_Mem.tickupdate = 10;
				break;
			}
			case 4:
			{
				if (GameScreen.player.clan == null)
				{
					GlobalService.gI().Clan_CMD(9, string.Empty, 0, 0);
					break;
				}
				short maxAttri = msg.reader().readShort();
				short pointAttri = msg.reader().readShort();
				short[] array2 = new short[5];
				for (int m = 0; m < array2.Length; m++)
				{
					array2[m] = msg.reader().readShort();
				}
				GameScreen.player.clan.pointAttri = pointAttri;
				GameScreen.player.clan.maxAttri = maxAttri;
				GameScreen.player.clan.setAttri(array2);
				break;
			}
			case 5:
			{
				MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 0);
				if (mainObject != null)
				{
					short iD = msg.reader().readShort();
					short idIcon = msg.reader().readShort();
					sbyte b = msg.reader().readByte();
					if (mainObject.clan == null)
					{
						mainObject.clan = new MainClan(iD, idIcon, b);
					}
					else
					{
						mainObject.clan.ID = iD;
						mainObject.clan.idIcon = idIcon;
						mainObject.clan.chucvu = b;
					}
					if (mainObject == GameScreen.player)
					{
						MainClan.UpdateRankMe(b);
					}
					sbyte borderIconClan = msg.reader().readByte();
					mainObject.clan.borderIconClan = borderIconClan;
				}
				if (mainObject.boatSea != null)
				{
					mainObject.boatSea.setIconClan();
				}
				break;
			}
			case 8:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					read_Data_Chat(msg, 0);
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.clanchat);
				}
				break;
			case 7:
			{
				int iD2 = msg.reader().readInt();
				string name = msg.reader().readUTF();
				InfoMemList.setTypeEvent(iD2, 6, name, T.eventMoiClan, 0, 0);
				break;
			}
			case 9:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					GameScreen.player.clan.vecChatCLan.removeAllElements();
					short num = msg.reader().readShort();
					for (int i = 0; i < num; i++)
					{
						read_Data_Chat(msg, 0);
					}
				}
				break;
			case 11:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					short id = msg.reader().readShort();
					GameCanvas.ClanScr.clanchat.RemoveChat(id);
				}
				break;
			case 10:
			{
				MainObject mainObject2 = MainObject.get_Object(msg.reader().readShort(), 0);
				if (mainObject2 != null && mainObject2.clan != null)
				{
					mainObject2.clan = null;
				}
				if (mainObject2 == GameScreen.player && GameCanvas.currentScreen == GameCanvas.ClanScr)
				{
					GameCanvas.gameScr.Show();
					GameCanvas.ClanScr = null;
					Player.isGetDataClan = -1;
				}
				break;
			}
			case 12:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					switch (msg.reader().readByte())
					{
					case 0:
						readMemInCLan(msg);
						break;
					case 1:
						Clan_Screen.getRemoveMemInCLan(msg.reader().readUTF());
						break;
					}
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.member);
				}
				break;
			case 13:
			{
				int countDown = msg.reader().readInt();
				MainTab.CDDonateClan.setCountDown(countDown);
				int numTangqua = msg.reader().readInt();
				InfoMemList memInCLan = Clan_Screen.getMemInCLan(GameScreen.player.ID);
				if (memInCLan != null)
				{
					memInCLan.numTangqua = numTangqua;
				}
				break;
			}
			case 14:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					string text = msg.reader().readUTF();
					GameScreen.player.clan.strVoice = text;
					GameCanvas.ClanScr.info.getmStrInfo(text, GameCanvas.ClanScr.info.wCon - 6);
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.info);
				}
				break;
			case 15:
			{
				short[] array = new short[5];
				for (int k = 0; k < 5; k++)
				{
					array[k] = msg.reader().readShort();
				}
				for (int l = 0; l < Player.mAttribute.Length; l++)
				{
					Main_Attribute main_Attribute = Player.mAttribute[l];
					if (l < array.Length)
					{
						main_Attribute.valuePlus = array[l];
					}
				}
				break;
			}
			case 16:
				if (GameScreen.player.clan != null)
				{
					GameScreen.player.clan.isLevelUp = msg.reader().readByte();
					GameScreen.player.clan.xp = msg.reader().readInt();
				}
				break;
			case 17:
				if (GameScreen.player.clan != null)
				{
					GameScreen.player.clan.rubiClan = msg.reader().readInt();
					GameScreen.player.clan.beryClan = msg.reader().readInt();
				}
				break;
			case 19:
				update_Inven_Clan(msg);
				break;
			case 20:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					GameCanvas.ClanScr.achi.vecDetail.removeAllElements();
					while (msg.reader().available() > 0)
					{
						read_Data_Chat(msg, 1);
					}
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.achi);
				}
				break;
			case 21:
			{
				string info = msg.reader().readUTF();
				if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	private void readMemInCLan(Message msg)
	{
		try
		{
			short id = msg.reader().readShort();
			string text = msg.reader().readUTF();
			InfoMemList infoMemList = Clan_Screen.getMemInCLan(text);
			bool flag = false;
			if (infoMemList == null)
			{
				infoMemList = new InfoMemList(id);
				flag = true;
			}
			infoMemList.updateMemClan(text, msg.reader().readShort(), msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readInt());
			infoMemList.updateFace(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
			infoMemList.typeOnline = msg.reader().readByte();
			if (flag)
			{
				GameScreen.player.clan.vecMem.addElement(infoMemList);
			}
			if (text.CompareTo(GameScreen.player.name) == 0)
			{
				Player.ChucInCLan = infoMemList.chucInClan;
			}
			if (GameCanvas.ClanScr != null)
			{
				GameCanvas.ClanScr.member.updateLimCam();
			}
		}
		catch (Exception)
		{
		}
	}

	public void read_Data_Chat(Message msg, int tab)
	{
		try
		{
			sbyte typeAction = msg.reader().readByte();
			short iDChat = msg.reader().readShort();
			short iDMem = msg.reader().readShort();
			string name = msg.reader().readUTF();
			string str = msg.reader().readUTF();
			long time = msg.reader().readLong() - GameCanvas.clockServer;
			switch (tab)
			{
			case 0:
				GameCanvas.ClanScr.clanchat.addOneChat(iDChat, iDMem, name, str, typeAction, time);
				break;
			case 1:
				GameCanvas.ClanScr.achi.addOneChat(iDChat, iDMem, name, str, typeAction, time);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void ChuyenHoa(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b != 0 && SplitScreen.instance == null)
			{
				SplitScreen.instance = new SplitScreen(7, -1);
			}
			switch (b)
			{
			case 0:
				SplitScreen.instance = new SplitScreen(7, -1);
				SplitScreen.instance.Show(GameCanvas.gameScr);
				break;
			case 1:
			{
				sbyte b2 = msg.reader().readByte();
				short id = msg.reader().readShort();
				MainItem itemVec = MainItem.getItemVec(3, id, Player.vecInventory);
				if (itemVec != null)
				{
					switch (b2)
					{
					case 0:
						idLvthap = id;
						break;
					case 1:
						idLvCao = id;
						break;
					}
					MainItem mainItem = new MainItem(3, itemVec.ID, itemVec.idIcon, 1, itemVec.colorName, itemVec.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[b2] = mainItem;
				}
				break;
			}
			case 3:
			{
				string showServer = msg.reader().readUTF();
				SplitScreen.instance.showServer = showServer;
				numCuonghoa = msg.reader().readByte();
				SplitScreen.instance.Step = 1;
				break;
			}
			case 2:
			{
				SplitScreen.instance.PriceChuyenHoa = msg.reader().readShort();
				mVector mVector2 = new mVector();
				iCommand iCommand2 = new iCommand(SplitScreen.instance.PriceChuyenHoa + string.Empty, 4, this);
				iCommand2.setFraCaption(AvMain.fraMoney, 1, 1, 0);
				mVector2.addElement(iCommand2);
				iCommand o = new iCommand(T.close, 1, 0, this);
				mVector2.addElement(o);
				GameCanvas.Start_Normal_DiaLog(T.textchuyenhoa + SplitScreen.instance.PriceChuyenHoa + " " + T.ruby + "?", mVector2, isCmdClose: false);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void little_Garden(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				sbyte type = msg.reader().readByte();
				int maxHP = msg.reader().readInt();
				int maxMP = msg.reader().readInt();
				BigBossLittleGraden bigBossLittleGraden4 = new BigBossLittleGraden(type);
				bigBossLittleGraden4.MaxHP = maxHP;
				bigBossLittleGraden4.MaxMP = maxMP;
				GameScreen.vecBigBossLittleGraden.addElement(bigBossLittleGraden4);
				break;
			}
			case 1:
			{
				sbyte b2 = msg.reader().readByte();
				int hp = msg.reader().readInt();
				int mp = msg.reader().readInt();
				for (int j = 0; j < GameScreen.vecBigBossLittleGraden.size(); j++)
				{
					BigBossLittleGraden bigBossLittleGraden2 = (BigBossLittleGraden)GameScreen.vecBigBossLittleGraden.elementAt(j);
					if (bigBossLittleGraden2.type == b2)
					{
						bigBossLittleGraden2.hp = hp;
						bigBossLittleGraden2.mp = mp;
						break;
					}
				}
				break;
			}
			case 2:
			{
				sbyte b3 = msg.reader().readByte();
				int dam = msg.reader().readInt();
				for (int k = 0; k < GameScreen.vecBigBossLittleGraden.size(); k++)
				{
					BigBossLittleGraden bigBossLittleGraden3 = (BigBossLittleGraden)GameScreen.vecBigBossLittleGraden.elementAt(k);
					if (bigBossLittleGraden3.type == b3)
					{
						bigBossLittleGraden3.setActionBigBoss(1);
						bigBossLittleGraden3.dam = dam;
						break;
					}
				}
				break;
			}
			case 3:
			{
				sbyte b = msg.reader().readByte();
				for (int i = 0; i < GameScreen.vecBigBossLittleGraden.size(); i++)
				{
					BigBossLittleGraden bigBossLittleGraden = (BigBossLittleGraden)GameScreen.vecBigBossLittleGraden.elementAt(i);
					if (bigBossLittleGraden.type == b)
					{
						bigBossLittleGraden.setActionBigBoss(3);
						break;
					}
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Pet(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			switch (b)
			{
			case 0:
			{
				short num = msg.reader().readShort();
				short num2 = msg.reader().readShort();
				short num3 = msg.reader().readShort();
				sbyte b2 = msg.reader().readByte();
				Debug.Log((object)$"[MSG -80 PET SPAWN] petID={num}, ownerID={num2}, idImage/frame={num3}, type={b2}");
				MainObject pet2 = MainObject.getPet(num2);
				if (pet2 == null)
				{
					pet2 = new Pet(num, num2, num3, b2);
					GameScreen.addPlayer(pet2);
				}
				else
				{
					pet2.setDataPet(num, num3, b2);
				}
				break;
			}
			case 1:
			{
				msg.reader().readShort();
				MainObject pet = MainObject.getPet(msg.reader().readShort());
				if (pet != null)
				{
					pet.isRemove = true;
				}
				break;
			}
			}
			switch (b)
			{
			case 3:
			{
				short num4 = msg.reader().readShort();
				mVector mVector2 = new mVector();
				for (int i = 0; i < num4; i++)
				{
					short iD = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					short idIcon2 = msg.reader().readShort();
					sbyte typeItem = msg.reader().readByte();
					mSystem.outz("PET_INVEN " + iD + " " + idIcon2 + " " + typeItem);
					MainItem mainItem = new MainItem(typeItem, iD, idIcon2, name, 0);
					mainItem.name = name;
					mainItem.info = info;
					if (msg.reader().readByte() == 1)
					{
						mainItem.addInfoFrist(T.daTrangBi, 1);
						mainItem.colorName = 1;
					}
					sbyte b3 = msg.reader().readByte();
					for (int j = 0; j < b3; j++)
					{
						sbyte id = msg.reader().readByte();
						short value = msg.reader().readShort();
						mainItem.addInfo(id, value, infoShow.HARDCODE_INFO_CO_BAN, 1);
					}
					mVector2.addElement(mainItem);
				}
				GameCanvas.tabShopScr = new TabScreen(MainTab.xTab, 0);
				mVector mVector3 = new mVector();
				GameCanvas.tabShopScr.isShopClan = false;
				GameCanvas.tabInvenClan = new TabInventory(T.khoPet, mVector2, 7, MainTab.xTab);
				GameCanvas.tabInvenClan.initCmd();
				mVector3.addElement(GameCanvas.tabInvenClan);
				GameCanvas.tabShopScr.addVecTab(mVector3);
				GameCanvas.tabShopScr.idSelect = 0;
				GameCanvas.tabShopScr.Show(GameCanvas.gameScr);
				GameCanvas.tabShopScr.typeCurrent = 1;
				GameCanvas.tabShopScr.setTabSelect();
				break;
			}
			case 4:
			{
				short idIcon = msg.reader().readShort();
				mSystem.outz("action use idIcon " + idIcon);
				GameCanvas.tabInvenClan.Use(idIcon);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Input_password(Message msg)
	{
		try
		{
			short id = msg.reader().readShort();
			string name = msg.reader().readUTF();
			string[] array = new string[msg.reader().readByte()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = msg.reader().readUTF();
			}
			GameCanvas.currentDialog = GameCanvas.Start_Input_Dialog_Server(array, name, id);
		}
		catch (Exception)
		{
		}
	}

	public void registerNew(Message msg)
	{
		try
		{
			sbyte num = msg.reader().readByte();
			FristLoginScreen.cmdRegister = new iCommand(T.register, 3, GameCanvas.fristLoginScr);
			FristLoginScreen.input = new InputDialog();
			FristLoginScreen.input.setinfo(T.registerNew, T.info, 0, FristLoginScreen.cmdRegister, T.registerCaution);
			if (num == 1)
			{
				FristLoginScreen.input.setNameDefaul(T.registerDefaul);
			}
			GameCanvas.Start_Sub_Dialog(FristLoginScreen.input);
		}
		catch (Exception)
		{
		}
	}

	public void ChangeMapNonData(Message msg)
	{
		try
		{
			actionChangeMap = 0;
			GameCanvas.loadmap.idLastMap = GameCanvas.loadmap.idMap;
			short num = msg.reader().readShort();
			if (setMapSea(num))
			{
				idMapLuu = num;
				msgLuu = msg;
				isNondata = true;
			}
			else
			{
				readChangeMapNonData(msg, num);
			}
		}
		catch (Exception)
		{
		}
	}

	public void readChangeMapNonData(Message msg, short idmap)
	{
		try
		{
			GameCanvas.loadMapScr.Show();
			GameScreen.RemoveLoadMap();
			DataMap dataMap = (DataMap)DataMap.hashDataMap.get(string.Empty + idmap);
			if (dataMap == null)
			{
				GlobalService.gI().NextMap(idmap);
				return;
			}
			GameCanvas.loadmap.idMap = idmap;
			Interface_Game.nameMap = string.Empty;
			GameCanvas.loadMapScr.area = msg.reader().readByte();
			sbyte typeViewPlayer = msg.reader().readByte();
			GameScreen.player.posTransRoad = null;
			GameScreen.player.x = msg.reader().readShort();
			GameScreen.player.y = msg.reader().readShort();
			GameScreen.player.toX = GameScreen.player.x;
			GameScreen.player.toY = GameScreen.player.y;
			GameScreen.player.maxHp = msg.reader().readInt();
			GameScreen.player.Hp = msg.reader().readInt();
			GameScreen.player.maxMp = msg.reader().readInt();
			GameScreen.player.Mp = msg.reader().readInt();
			sbyte b = msg.reader().readByte();
			if (LoadMap.specMap == 3)
			{
				GameScreen.player.isBeginTrain = false;
			}
			else if (LoadMap.specMap == 4)
			{
				GameCanvas.saveRms.loadHotKey(SaveRms.datahotKeySkill);
			}
			LoadMap.specMap = msg.reader().readByte();
			if (LoadMap.specMap == 1 && check_IDMap_PVP())
			{
				GameScreen.tickPvP = 60;
			}
			if (b == 1)
			{
				LoadMap.isOnlineMap = true;
				GameCanvas.loadmap.loadmap(dataMap.dataMap);
				GameCanvas.loadMapScr.mItemMap = dataMap.dataItemMap;
				LoadMap.vecPointChange.removeAllElements();
				if (GameCanvas.loadmap.idMap != 157 && GameCanvas.loadmap.idMap != 159 && GameCanvas.loadmap.idMap != 161)
				{
					for (int i = 0; i < dataMap.vecPointMap.size(); i++)
					{
						Point o = (Point)dataMap.vecPointMap.elementAt(i);
						LoadMap.vecPointChange.addElement(o);
					}
				}
			}
			if (b == 0)
			{
				LoadMap.isOnlineMap = false;
				if (LoadMap.specMap == 5)
				{
					LoadRedLine(isFinish: false);
				}
				else if (LoadMap.specMap == 8)
				{
					MapGotoSky.setPos();
				}
				else if (LoadMap.specMap == 12)
				{
					MapGotoGod.setPos();
				}
			}
			LoadMapScreen.IDBack = dataMap.IdBack;
			LoadMapScreen.HBack = dataMap.hBack;
			LoadMapScreen.isNextMap = true;
			if (b == 1)
			{
				GameCanvas.gameScr.setTypeViewPlayer(typeViewPlayer);
			}
			sbyte b2 = msg.reader().readByte();
			sbyte level = msg.reader().readByte();
			LoadMapScreen.typeChangeMap = msg.reader().readByte();
			if (b2 < 0 || GameCanvas.lowGraphic)
			{
				GameScreen.effMap = null;
			}
			else
			{
				GameScreen.effMap = new Effect_Map(b2, level);
			}
			if (LoadMap.specMap == 3)
			{
				GlobalService.gI().Get_Xp_Map_Train(0);
				Player.AutoFireCur = Player.typeAutoFireMain;
				sbyte b3 = msg.reader().readByte();
				MainObject.mPosMapTrain = mSystem.new_M_Int(b3, 2);
				for (int j = 0; j < b3; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						MainObject.mPosMapTrain[j][k] = msg.reader().readByte();
					}
				}
				Player.strTimeChange = msg.reader().readUTF();
			}
			Interface_Game.nameMap = dataMap.nameMap;
			if (LoadMap.specMap == 4)
			{
				GameScreen.player.boatSea = new Boat(GameScreen.player.ID, GameScreen.player.x, GameScreen.player.y, 0, (sbyte)GameScreen.player.type_left_right);
				GameScreen.player.setSpeed(4, 3);
				GameScreen.player.vySea = 4;
				if (!GameCanvas.lowGraphic)
				{
					GameScreen.effSea = new Effect_Map(4, 0);
				}
			}
			else if (LoadMap.isOnlineMap && LoadMap.specMap != 5)
			{
				GameScreen.player.boatSea = null;
				GameScreen.player.setSpeed(7, 7);
				GameScreen.effSea = null;
			}
		}
		catch (Exception)
		{
		}
	}

	public void PvP_Thong_Bao(Message msg)
	{
		try
		{
			Interface_Game.setTypePvP(msg.reader().readByte());
		}
		catch (Exception)
		{
		}
	}

	public void Archi_Daily(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				string name = msg.reader().readUTF();
				sbyte b2 = msg.reader().readByte();
				mVector mVector2 = new mVector();
				for (int i = 0; i < b2; i++)
				{
					InfoArchi o = new InfoArchi(msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readByte());
					mVector2.addElement(o);
				}
				GameCanvas.Start_Sub_Dialog(GameCanvas.Start_Archi_Dialog(name, mVector2));
				break;
			}
			case 1:
			{
				sbyte b = msg.reader().readByte();
				if (MsgArchiDaily.vecArchi != null && b >= 0 && b < MsgArchiDaily.vecArchi.size())
				{
					((InfoArchi)MsgArchiDaily.vecArchi.elementAt(b)).typeReward = msg.reader().readByte();
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Table_Match(Message msg)
	{
		try
		{
			mVector mVector2 = new mVector();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				InfoMatch infoMatch = new InfoMatch();
				infoMatch.type = msg.reader().readByte();
				if (infoMatch.type == 0 || infoMatch.type == 2)
				{
					infoMatch.mname = new string[2];
					infoMatch.mname[0] = msg.reader().readUTF();
					infoMatch.mname[1] = msg.reader().readUTF();
				}
				else if (infoMatch.type == 1)
				{
					infoMatch.mname = new string[4];
					infoMatch.mname[0] = msg.reader().readUTF();
					infoMatch.mname[1] = msg.reader().readUTF();
					infoMatch.mname[2] = msg.reader().readUTF();
					infoMatch.mname[3] = msg.reader().readUTF();
				}
				infoMatch.typeLeftRight = msg.reader().readByte();
				mVector2.addElement(infoMatch);
				GameCanvas.Start_Sub_Dialog(GameCanvas.Start_Match_Dialog(mVector2));
			}
		}
		catch (Exception)
		{
		}
	}

	public void ReadPartNew(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			mPart mPart2 = new mPart(msg.reader().readByte());
			for (int i = 0; i < mPart2.pi.Length; i++)
			{
				mPart2.pi[i] = new PartImage();
				mPart2.pi[i].id = msg.reader().readShort();
				mPart2.pi[i].dx = msg.reader().readByte();
				mPart2.pi[i].dy = msg.reader().readByte();
			}
			CharPartInfo.hashMyPart.put(string.Empty + num, mPart2);
		}
		catch (Exception)
		{
		}
	}

	public void updateXP_Arena(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 0);
			if (mainObject != null)
			{
				mainObject.colorXPArena = 0;
				mainObject.xpArena = msg.reader().readInt();
				sbyte b = msg.reader().readByte();
				if (b == 1)
				{
					mainObject.colorXPArena = 6;
				}
				else if (b == 2 || b == 3)
				{
					mainObject.colorXPArena = 1;
				}
				else if (b <= 10)
				{
					mainObject.colorXPArena = 4;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void NewDialog(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			string info = msg.reader().readUTF();
			switch (b)
			{
			case 0:
			{
				mVector mVector2 = new mVector();
				mVector2.addElement(TabInventory.cmdSellWhite);
				mVector2.addElement(TabInventory.cmdSell_W_G);
				GameCanvas.Start_Normal_DiaLog(info, mVector2, isCmdClose: true);
				break;
			}
			case 1:
			{
				mVector mVector3 = new mVector();
				sbyte b2 = msg.reader().readByte();
				for (int i = 0; i < b2; i++)
				{
					sbyte typeItem = msg.reader().readByte();
					string name = msg.reader().readUTF();
					short idIcon = msg.reader().readShort();
					MainItem mainItem = new MainItem(typeItem, (short)i, idIcon, 0, 0, 0);
					mainItem.name = name;
					mVector3.addElement(mainItem);
				}
				MsgDialog msgDialog = new MsgDialog();
				msgDialog.setinfoItem(info, mVector3, null, isCmdClose: true);
				GameCanvas.Start_Current_Dialog(msgDialog);
				break;
			}
			case 2:
				if (GameCanvas.currentScreen != GameCanvas.gameScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void TypeNpcEvent(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 2);
			sbyte typeNpcEvent = msg.reader().readByte();
			if (mainObject != null)
			{
				mainObject.typeNpcEvent = typeNpcEvent;
				mainObject.setPosEvent();
			}
		}
		catch (Exception)
		{
		}
	}

	public void TimeItemDrop(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			short num = msg.reader().readShort();
			string text = msg.reader().readUTF();
			if (mainObject != null)
			{
				mainObject.timeItemDropEvent = num;
				if (num != -1)
				{
					mainObject.timeRemove = num;
				}
				mainObject.timeBeginItemDropEvent = GameCanvas.timeNow;
				mainObject.namePlayer = text;
				mSystem.outz("item drop id cat " + iD + " - " + tem + " namePlayer =" + text + "!");
				mainObject.isDrop42 = true;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Weapon_fashion(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null)
			{
				sbyte b = msg.reader().readByte();
				short num = msg.reader().readShort();
				if (mainObject.name == GameScreen.player.name && b == 0)
				{
					mainObject.weaponFashion = num;
				}
				if (b == 6)
				{
					mainObject.sethead(num);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Market(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 2:
			{
				int num3 = MotherCanvas.hw - (MainTab.wTab + 130) / 2;
				if (num3 < 0)
				{
					num3 = 0;
				}
				GameCanvas.tabMarketScr = new TabScreen(num3, 0);
				mVector mVector4 = new mVector();
				ScreenMarket o = new ScreenMarket(T.tabHat_Wea, null, GameCanvas.tabMarketScr, 0);
				mVector4.addElement(o);
				GameCanvas.tabMarketScr.addVecTab(mVector4);
				ScreenMarket o2 = new ScreenMarket(T.tabAo_Quan, null, GameCanvas.tabMarketScr, 1);
				mVector4.addElement(o2);
				ScreenMarket o3 = new ScreenMarket(T.tabTranhsuc, null, GameCanvas.tabMarketScr, 2);
				mVector4.addElement(o3);
				ScreenMarket o4 = new ScreenMarket(T.tabNguyenLieu, null, GameCanvas.tabMarketScr, 5);
				mVector4.addElement(o4);
				ScreenMarket o5 = new ScreenMarket(T.tabVatPham, null, GameCanvas.tabMarketScr, 6);
				mVector4.addElement(o5);
				ScreenMarket o6 = new ScreenMarket(T.tabChest, null, GameCanvas.tabMarketScr, 3);
				mVector4.addElement(o6);
				GameCanvas.tabInvenMarket = new TabInventory(T.tabInven, Player.vecInventory, 5, num3);
				GameCanvas.tabInvenMarket.initCmd();
				mVector4.addElement(GameCanvas.tabInvenMarket);
				GameCanvas.tabMarketScr.idSelect = 0;
				GameCanvas.tabMarketScr.Show(GameCanvas.gameScr);
				GameCanvas.tabMarketScr.setTabSelect();
				GameCanvas.tabMarketScr.setCurTypetab(1);
				break;
			}
			case 3:
				if (GameCanvas.tabMarketScr != null)
				{
					sbyte index2 = msg.reader().readByte();
					short num2 = msg.reader().readShort();
					mVector mVector3 = new mVector();
					for (int k = 0; k < num2; k++)
					{
						MainItem mainItem2 = readUpdateItem(msg, isShop: false);
						mainItem2.priceVND = msg.reader().readInt();
						mainItem2.setTimeMarket(msg.reader().readInt());
						mainItem2.typeMarket = msg.reader().readByte();
						mVector3.addElement(mainItem2);
					}
					GameCanvas.tabMarketScr.updateVecDataMarket(index2, mVector3);
				}
				break;
			case 8:
			{
				sbyte b2 = msg.reader().readByte();
				sbyte[] array = new sbyte[b2];
				for (int j = 0; j < b2; j++)
				{
					array[j] = 0;
				}
				MsgArea msgArea = new MsgArea();
				msgArea.setinfoChangeArea(array, 1);
				GameCanvas.Start_Current_Dialog(msgArea);
				break;
			}
			case 9:
				if (GameCanvas.tabMarketScr != null)
				{
					sbyte index = msg.reader().readByte();
					short num = msg.reader().readShort();
					mVector mVector2 = new mVector();
					for (int i = 0; i < num; i++)
					{
						sbyte b = msg.reader().readByte();
						MainItem mainItem = null;
						mainItem = ((b != 3) ? readUpdatePotionMarket(msg, b) : readUpdateItem(msg, isShop: false));
						mainItem.priceVND = msg.reader().readInt();
						int timeMarket = msg.reader().readInt();
						mainItem.setTimeMarket(timeMarket);
						mainItem.typeMarket = msg.reader().readByte();
						mVector2.addElement(mainItem);
					}
					GameCanvas.tabMarketScr.updateVecDataMarket(index, mVector2);
				}
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public MainItem readUpdatePotionMarket(Message msg, sbyte cat)
	{
		try
		{
			short iDMarket = msg.reader().readShort();
			short num = msg.reader().readShort();
			short numPotion = msg.reader().readShort();
			MainItem mainItem = null;
			switch (cat)
			{
			case 4:
			{
				Potion templatePotion = Potion.getTemplatePotion(num);
				mainItem = new MainItem(cat, templatePotion.idIcon, num);
				mainItem.IDMarket = iDMarket;
				mainItem.numPotion = numPotion;
				mainItem.name = templatePotion.name;
				mainItem.namepaint = templatePotion.name;
				if (templatePotion.isUpdateTem)
				{
					if (!mainItem.getInfoPotion(templatePotion.indexInfoPotion))
					{
						Potion.vecPotionDonotInfo.addElement(mainItem);
					}
				}
				else
				{
					mainItem.setInfoPotion(templatePotion.info);
				}
				break;
			}
			case 7:
			{
				MainMaterial mainMaterial = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + num);
				mainItem = new MainItem(cat, mainMaterial.idIcon, num);
				mainItem.IDMarket = iDMarket;
				mainItem.numPotion = numPotion;
				mainItem.name = mainMaterial.name;
				mainItem.namepaint = mainMaterial.name;
				mainItem.setInfoPotion(mainMaterial.info);
				break;
			}
			}
			return mainItem;
		}
		catch (Exception)
		{
		}
		return null;
	}

	public void upgradeDevil(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 20:
				openGhepnhieu(msg);
				break;
			case 21:
			{
				sbyte num5 = msg.reader().readByte();
				string showServer3 = msg.reader().readUTF();
				ScreenUpgrade.instance.showServer = showServer3;
				if (num5 == 1)
				{
					ScreenUpgrade.instance.Step = 1;
				}
				else
				{
					ScreenUpgrade.instance.Step = 2;
				}
				break;
			}
			case 13:
				SplitScreen.instance = new SplitScreen(8, -1);
				SplitScreen.instance.Show(GameCanvas.gameScr);
				break;
			case 14:
			{
				sbyte b3 = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				short num4 = msg.reader().readShort();
				MainItem itemVec2 = MainItem.getItemVec(b4, id2, Player.vecInventory);
				if (itemVec2 != null)
				{
					sbyte color2 = itemVec2.colorName;
					if (b4 != 3)
					{
						color2 = 5;
					}
					MainItem mainItem3 = new MainItem(b4, itemVec2.ID, itemVec2.idIcon, num4, color2, itemVec2.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[b3] = mainItem3;
				}
				break;
			}
			case 15:
			{
				for (int j = 0; j < ScreenUpgrade.mItemUpgrade.Length; j++)
				{
					ScreenUpgrade.mItemUpgrade[j] = null;
				}
				break;
			}
			case 16:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				break;
			case 17:
			{
				sbyte num2 = msg.reader().readByte();
				string showServer2 = msg.reader().readUTF();
				SplitScreen.instance.showServer = showServer2;
				if (num2 == 1)
				{
					SplitScreen.instance.Step = 1;
				}
				else
				{
					SplitScreen.instance.Step = 3;
				}
				break;
			}
			case 19:
			{
				sbyte tile = msg.reader().readByte();
				SplitScreen.instance.tile = tile;
				break;
			}
			case 8:
				ScreenUpgradeSkillDevil.instance = new ScreenUpgradeSkillDevil();
				ScreenUpgradeSkillDevil.instance.begin();
				ScreenUpgradeSkillDevil.instance.Show(GameCanvas.gameScr);
				break;
			case 9:
			{
				sbyte b = msg.reader().readByte();
				short id = msg.reader().readShort();
				sbyte b2 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				if (b2 == 104)
				{
					Skill_Info skillFromID = Skill_Info.getSkillFromID(id);
					MainItem mainItem = new MainItem(b2, skillFromID.ID, skillFromID.idIcon, num3, 0, skillFromID.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[b] = mainItem;
					break;
				}
				MainItem itemVec = MainItem.getItemVec(b2, id, Player.vecInventory);
				if (itemVec != null)
				{
					sbyte color = itemVec.colorName;
					if (b2 != 3)
					{
						color = 5;
					}
					MainItem mainItem2 = new MainItem(b2, itemVec.ID, itemVec.idIcon, num3, color, itemVec.LvUpgrade);
					ScreenUpgrade.mItemUpgrade[b] = mainItem2;
				}
				break;
			}
			case 10:
			{
				for (int i = 0; i < ScreenUpgrade.mItemUpgrade.Length; i++)
				{
					ScreenUpgrade.mItemUpgrade[i] = null;
				}
				break;
			}
			case 11:
				ScreenUpgrade.stopUpgradeSkillDevilAuto();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				break;
			case 12:
			{
				sbyte num = msg.reader().readByte();
				string showServer = msg.reader().readUTF();
				ScreenUpgradeSkillDevil.instance.showServer = showServer;
				if (num == 1)
				{
					ScreenUpgradeSkillDevil.instance.Step = 1;
				}
				else
				{
					ScreenUpgradeSkillDevil.instance.Step = 3;
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void cmdEvent(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz(" cmdEvent action = " + b);
			switch (b)
			{
			case 2:
			{
				if (GameCanvas.EventDialog == null)
				{
					GameCanvas.EventDialog = new MsgDialogEvent();
					GameCanvas.EventDialog.setInfoMerryCM();
				}
				sbyte b2 = msg.reader().readByte();
				sbyte[] array = new sbyte[b2];
				for (int i = 0; i < b2; i++)
				{
					array[i] = msg.reader().readByte();
				}
				GameCanvas.EventDialog.updateDataCM(array);
				break;
			}
			case 3:
				if (GameCanvas.EventDialog == null)
				{
					GameCanvas.EventDialog = new MsgDialogEvent();
				}
				GameCanvas.EventDialog.setInfoMerryCM();
				GameCanvas.Start_Sub_Dialog(GameCanvas.EventDialog);
				break;
			case 1:
				GameCanvas.end_Dialog();
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void ReadEventTrangTri(Message msg)
	{
		try
		{
			sbyte typeTrangTri = msg.reader().readByte();
			switch (msg.reader().readByte())
			{
			case 2:
			{
				if (GameCanvas.TrangTriDialog == null)
				{
					GameCanvas.TrangTriDialog = new MsgDialogTrangTri(typeTrangTri);
					GameCanvas.TrangTriDialog.setInfoTrangTri(null);
				}
				sbyte b = msg.reader().readByte();
				short[] array2 = new short[b];
				sbyte[] array3 = new sbyte[b];
				for (int k = 0; k < b; k++)
				{
					array2[k] = msg.reader().readShort();
					array3[k] = msg.reader().readByte();
				}
				GameCanvas.TrangTriDialog.updateDataTrangTri(array2, array3);
				break;
			}
			case 3:
			{
				if (GameCanvas.TrangTriDialog == null)
				{
					GameCanvas.TrangTriDialog = new MsgDialogTrangTri(typeTrangTri);
				}
				short num = msg.reader().readShort();
				short[][] array = new short[num][];
				for (int i = 0; i < num; i++)
				{
					short num2 = msg.reader().readShort();
					array[i] = new short[num2];
					for (int j = 0; j < num2; j++)
					{
						array[i][j] = msg.reader().readShort();
					}
				}
				GameCanvas.TrangTriDialog.setInfoTrangTri(array);
				GameCanvas.Start_Sub_Dialog(GameCanvas.TrangTriDialog);
				break;
			}
			case 1:
				GameCanvas.end_Dialog();
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void ReadTrongCay(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			mainObject.timeLeft = msg.reader().readShort();
			mainObject.state = msg.reader().readByte();
			mainObject.timeBegin = mSystem.currentTimeMillis();
			mSystem.outz("id " + iD + " cat " + tem + " time " + mainObject.timeLeft + " state" + mainObject.state);
		}
		catch (Exception)
		{
		}
	}

	public void getTemplate(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			sbyte[] data = new sbyte[msg.reader().available()];
			msg.reader().read(ref data);
			DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(data));
			switch (b)
			{
			case 4:
				Potion.LoadDataPotionTemplate(dataInputStream, 4);
				Potion.CheckAddDataTemplate();
				break;
			case 1:
				CatalogyMonster.LoadDataMonTemplate(dataInputStream);
				break;
			case 98:
				GameCanvas.loadmap.load_Table_Map(dataInputStream);
				timeLoadItemMap = 30;
				LoadMap.demSendTem--;
				break;
			case 97:
				Plash.readDataPlash(dataInputStream);
				break;
			case 96:
			{
				sbyte b2 = dataInputStream.readByte();
				string v = dataInputStream.readUTF();
				MainItem.hashAttriKichAn.put(string.Empty + b2, v);
				Item.CheckAddDataKichAn();
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void disconnect_Why(Message msg)
	{
		try
		{
			GameCanvas.infoDisConnect = msg.reader().readUTF();
			GlobalService.gI().reDisconectWhy();
		}
		catch (Exception)
		{
		}
	}

	public void Max_Level(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			Player.pointMaxLevelAttri = msg.reader().readShort();
			switch (b)
			{
			case 0:
			{
				Player.vecMaxLevelAttri.removeAllElements();
				mVector mVector2 = new mVector();
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < b2; j++)
				{
					MaxLevelAttribute o = new MaxLevelAttribute(msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort());
					mVector2.addElement(o);
				}
				Player.vecMaxLevelAttri = mVector2;
				break;
			}
			case 1:
			{
				short num = msg.reader().readShort();
				if (Player.vecMaxLevelAttri == null)
				{
					break;
				}
				for (int i = 0; i < Player.vecMaxLevelAttri.size(); i++)
				{
					MaxLevelAttribute maxLevelAttribute = (MaxLevelAttribute)Player.vecMaxLevelAttri.elementAt(i);
					if (maxLevelAttribute.Id == num)
					{
						maxLevelAttribute.value = msg.reader().readShort();
					}
				}
				break;
			}
			case 2:
				ScreenMaxLevel.gI().setData();
				ScreenMaxLevel.gI().Show(GameCanvas.gameScr);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Wanted(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b == 4)
			{
				GameScreen.player.wanted = msg.reader().readInt();
				return;
			}
			if (RoomWantedScreen.instance == null)
			{
				RoomWantedScreen.instance = new RoomWantedScreen();
			}
			if (b == 5)
			{
				int timeCmdFind = msg.reader().readInt();
				RoomWantedScreen.instance.setTimeCmdFind(timeCmdFind);
			}
			else
			{
				RoomWantedScreen.instance.setAction(b);
			}
		}
		catch (Exception)
		{
		}
	}

	public void ChestWanted(Message msg)
	{
		try
		{
			sbyte num = msg.reader().readByte();
			if (num == 0)
			{
				sbyte b = msg.reader().readByte();
				short iD = msg.reader().readShort();
				short idIcon = msg.reader().readShort();
				sbyte typeItem = msg.reader().readByte();
				string name = msg.reader().readUTF();
				short maxTimeUse = msg.reader().readShort();
				short timeUse = msg.reader().readShort();
				short ruby = msg.reader().readShort();
				Item item = new Item(typeItem, iD, idIcon, name, timeUse, maxTimeUse, ruby);
				Player.mChestWanted[b] = item;
			}
			if (num == 1)
			{
				sbyte b2 = msg.reader().readByte();
				Player.mChestWanted[b2] = null;
			}
		}
		catch (Exception)
		{
		}
	}

	public void OpenMessenger(Message msg)
	{
		GameCanvas.chatTabScr.getCurTab(1);
		GameCanvas.chatTabScr.Show(GameCanvas.currentScreen);
	}

	public void UpdateLoL(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				sbyte b3 = msg.reader().readByte();
				for (int j = 0; j < b3; j++)
				{
					if (j < Interface_Game.mValueLoL.Length)
					{
						Interface_Game.mValueLoL[j][1] = msg.reader().readByte();
					}
				}
				break;
			}
			case 1:
			{
				sbyte b2 = msg.reader().readByte();
				for (int i = 0; i < b2; i++)
				{
					short iD = msg.reader().readShort();
					sbyte typePK = msg.reader().readByte();
					MainObject mainObject = MainObject.get_Object(iD, 1);
					if (mainObject != null)
					{
						mainObject.typePK = typePK;
					}
				}
				break;
			}
			case 2:
			{
				sbyte b = msg.reader().readByte();
				sbyte type = msg.reader().readByte();
				InfoShowNotify o = new InfoShowNotify(msg.reader().readUTF() + ": " + T.mChatInLoL[b], type);
				Interface_Game.vecQuickChatLoL.insertElementAt(o, 0);
				break;
			}
			case 3:
				Interface_Game.killLeft = msg.reader().readShort();
				Interface_Game.truLeft = msg.reader().readShort();
				Interface_Game.killRight = msg.reader().readShort();
				Interface_Game.truRight = msg.reader().readShort();
				break;
			case 4:
				Interface_Game.valueHoiSinhLOL = msg.reader().readByte();
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Fire_Object_NEW(Message msg)
	{
		try
		{
			if (LoadMapScreen.isNextMap)
			{
				if (GameCanvas.lowGraphic && GameScreen.vecObjFire.size() > 20)
				{
					GameScreen.vecObjFire.removeElementAt(1);
				}
				GameScreen.vecObjFire.addElement(msg);
			}
		}
		catch (Exception)
		{
		}
	}

	public void DonotAutoReconnect(Message msg)
	{
		Player.StepAutoRe = 6;
	}

	public void QuickChat(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			sbyte b = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null && b >= 0 && b < T.mChatInGame.Length)
			{
				string chat = T.mChatInGame[b];
				chat = (mainObject.strChatPopup = GameMidlet.fixString(chat));
				if (mainObject.typeObject == 0)
				{
					GameCanvas.chatTabScr.addNewChat(T.tabServer, string.Empty, mainObject.name + ": " + chat, 1, isFocus: false);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void getDataInfoPotion(Message msg)
	{
		try
		{
			short index = msg.reader().readShort();
			string text = msg.reader().readUTF();
			Potion.hashInfoPotion.put(string.Empty + index, text);
			Potion.checkVecNonInfo(index, text);
		}
		catch (Exception)
		{
		}
	}

	public void updatePoint_WorldWar(Message msg)
	{
		try
		{
			MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 0);
			if (mainObject != null)
			{
				mainObject.checkWW = msg.reader().readByte();
				mainObject.killWW = msg.reader().readShort();
				mainObject.deadWW = msg.reader().readShort();
			}
		}
		catch (Exception)
		{
		}
	}

	public void update_MP_HP_Eff(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(num, b);
			if (mainObject == null || mainObject.returnAction())
			{
				return;
			}
			sbyte b2 = msg.reader().readByte();
			mainObject.maxHp = msg.reader().readInt();
			int hp = msg.reader().readInt();
			int num2 = msg.reader().readInt();
			if (num2 != 0)
			{
				string content = string.Empty + num2;
				if (num2 > 0)
				{
					content = "+" + num2;
				}
				mainObject.Hp = hp;
				if (b2 == 1)
				{
					GameScreen.addEffectNumBig_NEW_AP(num2, 0, mainObject.x, mainObject.y - mainObject.hOne, 20);
				}
				else
				{
					GameScreen.addEffectNum(content, mainObject.x - 12, mainObject.y - mainObject.hOne / 4 * 3 - lechYNum, 3);
				}
			}
			if (mainObject.Action == 4 && mainObject.Hp > 0)
			{
				mainObject.Reveive();
			}
			for (int i = 0; i < Player.vecParty.size(); i++)
			{
				InfoMemList infoMemList = (InfoMemList)Player.vecParty.elementAt(i);
				if (infoMemList.id == num && b == 0)
				{
					infoMemList.updateHP(hp, mainObject.maxHp, mainObject.Lv);
					break;
				}
			}
			if (b != 1)
			{
				mainObject.maxMp = msg.reader().readInt();
				int mp = msg.reader().readInt();
				int num3 = msg.reader().readInt();
				if (num3 != 0)
				{
					string content2 = string.Empty + num3;
					if (num3 > 0)
					{
						content2 = "+" + num3;
					}
					mainObject.Mp = mp;
					GameScreen.addEffectNum(content2, mainObject.x + 12, mainObject.y - mainObject.hOne / 4 * 3 - lechYNum, 4);
				}
			}
			lechYNum += 10;
			if (lechYNum > 20)
			{
				lechYNum = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Eff_Kich_An(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			short num = msg.reader().readShort();
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			msg.reader().readByte();
			int num2 = msg.reader().readInt();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null && b != 11)
			{
				GameScreen.addEffectEnd(143, b, mainObject.x, mainObject.y - 50, 0, mainObject);
				if (mainObject == GameScreen.player)
				{
					Item_Skill_Eff item_Skill_Eff = new Item_Skill_Eff((short)(400 + b), b, 1500);
					GameScreen.interfaceGame.addEffCurrent(item_Skill_Eff);
					Player.setDelaySkill(item_Skill_Eff.indexHotKey, num * 1000, isNotCooldown: true, 0);
				}
			}
			short iD2 = msg.reader().readShort();
			sbyte tem2 = msg.reader().readByte();
			sbyte type = msg.reader().readByte();
			int num3 = msg.reader().readInt();
			MainObject mainObject2 = MainObject.get_Object(iD2, tem2);
			switch (b)
			{
			case 0:
				if (mainObject != null)
				{
					Effect_Spec o = new Effect_Spec(mainObject, 11, (short)num2);
					mainObject.vecEffspec.addElement(o);
				}
				break;
			case 8:
			case 9:
			{
				if (mainObject == null || mainObject != GameScreen.player)
				{
					break;
				}
				IDictionaryEnumerator enumerator = Player.delaySkill.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string k = (string)enumerator.Value;
					DelaySkill delaySkill = (DelaySkill)Player.delaySkill.get(k);
					if (delaySkill.typeSkill == 1)
					{
						delaySkill.value = 0;
					}
				}
				break;
			}
			case 2:
			case 10:
				mainObject2?.addEffSpec(type, (short)num3);
				break;
			case 1:
				if (mainObject != null)
				{
					GameScreen.addEffectNumBig_NEW_AP(num2, 0, mainObject.x, mainObject.y - mainObject.hOne, 21);
				}
				break;
			case 3:
				if (mainObject2 != null)
				{
					GameScreen.addEffectNumBig_NEW_AP(-num3, 0, mainObject2.x, mainObject2.y - mainObject2.hOne, 22);
				}
				break;
			case 4:
				if (mainObject2 != null)
				{
					GameScreen.addEffectNumBig_NEW_AP(-num3, 0, mainObject2.x, mainObject2.y - mainObject2.hOne, 21);
				}
				break;
			case 5:
				if (mainObject != null)
				{
					Effect_Spec o2 = new Effect_Spec(mainObject, 12, (short)num2);
					mainObject.vecEffspec.addElement(o2);
				}
				break;
			case 6:
				if (mainObject != null)
				{
					GameScreen.addEffectNumBig_NEW_AP(num2, 0, mainObject.x, mainObject.y - mainObject.hOne, 21);
				}
				break;
			case 11:
				if (mainObject != null)
				{
					GameScreen.addEffectNumBig_NEW_AP(num2, 0, mainObject.x, mainObject.y - mainObject.hOne, 21);
				}
				break;
			case 7:
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Count_Kick_Ava(Message msg)
	{
		try
		{
			if (Interface_Game.mCountKichAn == null)
			{
				Interface_Game.mCountKichAn = new Item_Skill_Eff[20];
			}
			sbyte b = msg.reader().readByte();
			sbyte numPotion = msg.reader().readByte();
			for (int i = 0; i < Interface_Game.mCountKichAn.Length; i++)
			{
				if (Interface_Game.mCountKichAn[i] == null)
				{
					Interface_Game.mCountKichAn[i] = new Item_Skill_Eff((short)(400 + b), b, 1500);
					Interface_Game.mCountKichAn[i].numPotion = numPotion;
					Interface_Game.mCountKichAn[i].setColorKickAn();
					break;
				}
				if (Interface_Game.mCountKichAn[i].ID == b)
				{
					Interface_Game.mCountKichAn[i].numPotion = numPotion;
					break;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void GiaoTiep_FormServer(Message msg)
	{
		try
		{
			ID_GiaoTiep_Server = msg.reader().readByte();
			Str_GiaoTiep_Server = msg.reader().readUTF();
		}
		catch (Exception)
		{
		}
	}

	public void Event(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			sbyte b2 = msg.reader().readByte();
			mSystem.outz("type " + b + " action " + b2);
			switch (b)
			{
			case 0:
				switch (b2)
				{
				case 0:
				{
					string name = msg.reader().readUTF();
					short time = msg.reader().readShort();
					int tongXiu2 = msg.reader().readInt();
					int tongTai2 = msg.reader().readInt();
					int daCuoc2 = msg.reader().readInt();
					sbyte cua2 = msg.reader().readByte();
					sbyte kq = msg.reader().readByte();
					sbyte x = msg.reader().readByte();
					sbyte x2 = msg.reader().readByte();
					sbyte x3 = msg.reader().readByte();
					TaiXiuScreen.instance = new TaiXiuScreen(name, tongTai2, tongXiu2, daCuoc2, cua2, time);
					TaiXiuScreen.instance.SetXucXac(kq, x, x2, x3, isEff: false);
					TaiXiuScreen.instance.Show(GameCanvas.gameScr);
					break;
				}
				case 1:
				{
					int tongXiu = msg.reader().readInt();
					int tongTai = msg.reader().readInt();
					int daCuoc = msg.reader().readInt();
					sbyte cua = msg.reader().readByte();
					TaiXiuScreen.instance.SetDatCuoc(tongTai, tongXiu, daCuoc, cua);
					break;
				}
				case 2:
				{
					sbyte kq2 = msg.reader().readByte();
					sbyte x4 = msg.reader().readByte();
					sbyte x5 = msg.reader().readByte();
					sbyte x6 = msg.reader().readByte();
					TaiXiuScreen.instance.SetXucXac(kq2, x4, x5, x6, isEff: true);
					break;
				}
				case 3:
				{
					int tongXiu3 = msg.reader().readInt();
					int tongTai3 = msg.reader().readInt();
					TaiXiuScreen.instance.SetUpdateTaiXiu(tongTai3, tongXiu3);
					break;
				}
				case 4:
				{
					sbyte b3 = msg.reader().readByte();
					mVector mVector2 = new mVector();
					for (int i = 0; i < b3; i++)
					{
						string caption = msg.reader().readUTF();
						mVector2.addElement(new iCommand(caption, 100, this));
					}
					GameCanvas.menu.startAt(mVector2, 2, T.lichSu);
					break;
				}
				}
				break;
			case 1:
				if (b2 == 0)
				{
					mSystem.outz("nau banh");
					GameCanvas.gameScr.setNauBanh(isNauBanh: true);
				}
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Hanh_Trinh(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz("Hanh_Trinh>>>>>>>>>>> action=" + b);
			switch (b)
			{
			case 0:
			{
				short idMinimap = msg.reader().readShort();
				string text = msg.reader().readUTF();
				sbyte b6 = msg.reader().readByte();
				mSystem.outz("minimap=" + idMinimap + " size=" + b6 + " ten map=" + text);
				mVector mVector3 = new mVector();
				for (sbyte b7 = 0; b7 < b6; b7++)
				{
					string namepaint2 = msg.reader().readUTF();
					sbyte typeItem2 = msg.reader().readByte();
					short iD2 = msg.reader().readShort();
					msg.reader().readByte();
					short idIcon2 = msg.reader().readShort();
					Potion potion2 = new Potion(typeItem2, iD2, idIcon2, string.Empty, 0);
					potion2.namepaint = namepaint2;
					potion2.info = msg.reader().readUTF();
					potion2.setInfoPotion(potion2.info);
					mVector3.addElement(potion2);
					mSystem.outz("item " + iD2 + " icon " + idIcon2 + " info " + potion2.info);
				}
				SplitScreen.instance = new SplitScreen(21, -1);
				SplitScreen.instance.vecInventory = mVector3;
				SplitScreen.instance.getItemCurNew();
				SplitScreen.instance.idMinimap = idMinimap;
				SplitScreen.instance.nameMinimap = text;
				SplitScreen.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 1:
			{
				sbyte b4 = msg.reader().readByte();
				mSystem.outz("size=" + b4);
				mVector mVector2 = new mVector();
				for (sbyte b5 = 0; b5 < b4; b5++)
				{
					string namepaint = msg.reader().readUTF();
					sbyte typeItem = msg.reader().readByte();
					short iD = msg.reader().readShort();
					short idIcon = msg.reader().readShort();
					Potion potion = new Potion(typeItem, iD, idIcon, string.Empty, 0);
					potion.namepaint = namepaint;
					potion.info = msg.reader().readUTF();
					potion.setInfoPotion(potion.info);
					mVector2.addElement(potion);
				}
				SplitScreen.instance.vecDaKhamHanhTrinh = mVector2;
				if (b4 == 0)
				{
					ScreenUpgrade.mItemUpgrade[0] = null;
				}
				else
				{
					ScreenUpgrade.mItemUpgrade[0] = (MainItem)mVector2.elementAt(0);
				}
				SplitScreen.instance.ChangeCmd();
				break;
			}
			case 2:
			{
				short id = msg.reader().readShort();
				mSystem.outz("xoa da id = " + id);
				SplitScreen.instance.XoaDa(id);
				break;
			}
			case 3:
			{
				string namepaint3 = msg.reader().readUTF();
				sbyte typeItem3 = msg.reader().readByte();
				short iD3 = msg.reader().readShort();
				msg.reader().readByte();
				short idIcon3 = msg.reader().readShort();
				Potion potion3 = new Potion(typeItem3, iD3, idIcon3, string.Empty, 0);
				potion3.namepaint = namepaint3;
				potion3.info = msg.reader().readUTF();
				potion3.setInfoPotion(potion3.info);
				SplitScreen.instance.vecInventory.addElement(potion3);
				mSystem.outz("item " + iD3 + " icon " + idIcon3 + " info " + potion3.info);
				ScreenUpgrade.mItemUpgrade[0] = null;
				SplitScreen.instance.ChangeCmd();
				break;
			}
			case 4:
			{
				HanhTrinhScreen.instance = new HanhTrinhScreen();
				HanhTrinhScreen.instance.nameList = msg.reader().readUTF();
				sbyte b2 = msg.reader().readByte();
				mSystem.outz("size=" + b2);
				HanhTrinhScreen.instance.mTenLang = new string[b2];
				HanhTrinhScreen.instance.mIcon = new short[b2];
				for (sbyte b3 = 0; b3 < b2; b3++)
				{
					HanhTrinhScreen.instance.mTenLang[b3] = msg.reader().readUTF();
					HanhTrinhScreen.instance.mIcon[b3] = msg.reader().readShort();
				}
				HanhTrinhScreen.instance.Show(GameCanvas.gameScr);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Quay_So(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
				LuckyScreen.instance = new LuckyScreen();
				LuckyScreen.instance.Show(GameCanvas.gameScr);
				LuckyScreen.instance.mItemShow = null;
				GlobalService.gI().Quayso(3);
				LuckyScreen.mListItemLucky = new mVector();
				break;
			case 1:
			case 2:
			{
				sbyte num2 = msg.reader().readByte();
				LuckyScreen.instance.typeQuay = 0;
				if (num2 == 9)
				{
					LuckyScreen.instance.typeQuay = 1;
					mSound.playSound(50, mSound.volumeSound);
				}
				else
				{
					mSound.playSound(48, mSound.volumeSound);
				}
				LuckyScreen.instance.StepQuaySo = 1;
				LuckyScreen.instance.tickAction = 0;
				Item_Drop[] array = new Item_Drop[num2];
				for (int num3 = num2 - 1; num3 >= 0; num3--)
				{
					sbyte type2 = msg.reader().readByte();
					string name = msg.reader().readUTF();
					short idIcon2 = msg.reader().readShort();
					int num4 = msg.reader().readInt();
					sbyte color = msg.reader().readByte();
					array[num3] = new Item_Drop((short)num3, type2, name, 0, 0, idIcon2, color);
					array[num3].num = num4;
				}
				LuckyScreen.instance.dataDialog(array);
				break;
			}
			case 3:
			{
				LuckyScreen.mListItemLucky.removeAllElements();
				sbyte b = msg.reader().readByte();
				for (short num = 0; num < b; num++)
				{
					sbyte type = msg.reader().readByte();
					short idIcon = msg.reader().readShort();
					MainItem o = new MainItem(type, idIcon, num);
					LuckyScreen.mListItemLucky.addElement(o);
				}
				LuckyScreen.instance.setPosPotion();
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Quay_WC(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz("vao nao>>>>>>>>>>> action=" + b);
			switch (b)
			{
			case 0:
				QuayWCScreen.instance = new QuayWCScreen();
				QuayWCScreen.instance.Show(GameCanvas.gameScr);
				QuayWCScreen.instance.mItemShow = null;
				GlobalService.gI().QuayWC(3);
				LuckyScreen.mListItemLucky = new mVector();
				break;
			case 1:
			case 2:
			{
				sbyte num2 = msg.reader().readByte();
				QuayWCScreen.instance.typeQuay = 0;
				if (num2 == 9)
				{
					QuayWCScreen.instance.typeQuay = 1;
					mSound.playSound(50, mSound.volumeSound);
				}
				else
				{
					mSound.playSound(48, mSound.volumeSound);
				}
				QuayWCScreen.instance.StepQuaySo = 1;
				QuayWCScreen.instance.tickAction = 0;
				Item_Drop[] array = new Item_Drop[num2];
				for (int num3 = num2 - 1; num3 >= 0; num3--)
				{
					sbyte type2 = msg.reader().readByte();
					string name = msg.reader().readUTF();
					short idIcon2 = msg.reader().readShort();
					int num4 = msg.reader().readInt();
					sbyte color = msg.reader().readByte();
					array[num3] = new Item_Drop((short)num3, type2, name, 0, 0, idIcon2, color);
					array[num3].num = num4;
				}
				QuayWCScreen.instance.dataDialog(array);
				break;
			}
			case 3:
			{
				LuckyScreen.mListItemLucky.removeAllElements();
				sbyte b2 = msg.reader().readByte();
				mSystem.outz("vao nao>>>>>>>>>>> size=" + b2);
				for (short num = 0; num < b2; num++)
				{
					sbyte type = msg.reader().readByte();
					short idIcon = msg.reader().readShort();
					mSystem.outz("vao nao>>>>>>>>>>> cat=" + type);
					mSystem.outz("vao nao>>>>>>>>>>> id=" + idIcon);
					MainItem o = new MainItem(type, idIcon, num);
					LuckyScreen.mListItemLucky.addElement(o);
				}
				QuayWCScreen.instance.setPosPotion();
				break;
			}
			case 4:
				QuayWCScreen.instance.idIconVongQuay = msg.reader().readShort();
				QuayWCScreen.instance.numVe = msg.reader().readShort();
				QuayWCScreen.instance.numLanDaQuay = msg.reader().readShort();
				mSystem.outz("nhan " + QuayWCScreen.instance.idIconVongQuay + " " + QuayWCScreen.instance.numVe + " " + QuayWCScreen.instance.numLanDaQuay);
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Quay_oc_sen(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
				QuayOcSenScreen.instance = new QuayOcSenScreen();
				QuayOcSenScreen.instance.title = msg.reader().readUTF();
				QuayOcSenScreen.instance.Show(GameCanvas.gameScr);
				QuayOcSenScreen.instance.mItemShow = null;
				GlobalService.gI().Quay_oc_sen(1);
				LuckyScreen.mListItemLucky = new mVector();
				break;
			case 1:
			{
				LuckyScreen.mListItemLucky.removeAllElements();
				sbyte b = msg.reader().readByte();
				sbyte[] array = new sbyte[b];
				for (short num = 0; num < b; num++)
				{
					sbyte id = msg.reader().readByte();
					sbyte type = msg.reader().readByte();
					short idIcon = msg.reader().readShort();
					int num2 = msg.reader().readInt();
					MainItem o = new MainItem(type, idIcon, id, num2);
					LuckyScreen.mListItemLucky.addElement(o);
					sbyte b2 = msg.reader().readByte();
					array[num] = b2;
				}
				QuayOcSenScreen.instance.SetmNhan(array);
				QuayOcSenScreen.instance.setPosPotion();
				break;
			}
			case 2:
			{
				sbyte indexQuaySo = msg.reader().readByte();
				QuayOcSenScreen.instance.SetIndexQuaySo(indexQuaySo);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Clan_Fight(Message msg)
	{
		try
		{
			mVector mVector2 = new mVector();
			switch (msg.reader().readByte())
			{
			case 0:
			{
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < b2; j++)
				{
					short subType = msg.reader().readShort();
					string text = msg.reader().readUTF();
					if (text.Length >= 15)
					{
						text = text.Substring(0, 12) + "...";
					}
					short idIconClan = msg.reader().readShort();
					short lvClan = msg.reader().readShort();
					iCommand iCommand2 = new iCommand(text + " " + T.Lv + " " + lvClan, 11, subType, this);
					iCommand2.setIdIconClan(idIconClan);
					iCommand2.lvClan = lvClan;
					mVector2.addElement(iCommand2);
				}
				GameCanvas.menu.startAt(mVector2, 2, T.thachDauClan);
				break;
			}
			case 4:
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(msg.reader().readUTF());
				break;
			case 5:
			{
				short iD = msg.reader().readShort();
				string text2 = msg.reader().readUTF();
				if (text2.Length >= 15)
				{
					text2 = text2.Substring(0, 12) + "...";
				}
				short num2 = msg.reader().readShort();
				InfoMemList.setTypeEvent(typeFight: msg.reader().readByte(), ID: iD, typeEvent: 7, name: text2 + " " + T.Lv + " " + num2, info: T.eventClanFight, priceFight: 5);
				break;
			}
			case 6:
			{
				sbyte b = msg.reader().readByte();
				for (int i = 0; i < b; i++)
				{
					short id = msg.reader().readShort();
					string name = msg.reader().readUTF();
					short num = msg.reader().readShort();
					InfoMemList infoMemList = new InfoMemList(id);
					infoMemList.name = name;
					infoMemList.info = T.Lv + num;
					infoMemList.rank = i;
					mVector2.addElement(infoMemList);
				}
				PlayerListServer.instance = new PlayerListServer(15, mVector2, T.listMemClanFight, 0);
				PlayerListServer.instance.Show(GameCanvas.gameScr);
				PlayerListServer.instance.isLoad = false;
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void daily_Bonus(Message msg)
	{
		try
		{
			mSystem.outz("vao cmd 60");
			MsgGiftLogin msgGiftLogin = new MsgGiftLogin();
			string name = msg.reader().readUTF();
			string nameCmd = msg.reader().readUTF();
			string empty = string.Empty;
			int num = msg.reader().readByte();
			Item_Drop[] array = new Item_Drop[num];
			for (int i = 0; i < num; i++)
			{
				sbyte type = msg.reader().readByte();
				string name2 = msg.reader().readUTF();
				short idIcon = msg.reader().readShort();
				sbyte color = msg.reader().readByte();
				array[i] = new Item_Drop((short)i, type, name2, 0, 0, idIcon, color);
				array[i].num = msg.reader().readShort();
				array[i].typeGiftDaily = msg.reader().readByte();
			}
			msgGiftLogin.setinfoShow_GiftLogin(0, name, empty, array, 0, nameCmd, null);
			GameCanvas.Start_Current_Dialog(msgGiftLogin);
		}
		catch (Exception)
		{
		}
	}

	public void Potion_Choice(Message msg)
	{
		try
		{
			MsgGiftLogin msgGiftLogin = new MsgGiftLogin();
			string name = msg.reader().readUTF();
			string nameCmd = msg.reader().readUTF();
			string empty = string.Empty;
			int num = msg.reader().readByte();
			Item_Drop[] array = new Item_Drop[num];
			for (int i = 0; i < num; i++)
			{
				sbyte type = msg.reader().readByte();
				string name2 = msg.reader().readUTF();
				short idIcon = msg.reader().readShort();
				sbyte color = msg.reader().readByte();
				array[i] = new Item_Drop((short)i, type, name2, 0, 0, idIcon, color);
				array[i].num = msg.reader().readShort();
				array[i].typeGiftDaily = msg.reader().readByte();
			}
			short iD = msg.reader().readShort();
			MainItem mainItem = new MainItem(msg.reader().readByte(), -1, iD);
			mainItem.name = name;
			msgGiftLogin.setinfoShow_GiftLogin(1, name, empty, array, 0, nameCmd, mainItem);
			GameCanvas.Start_Sub_Dialog(msgGiftLogin);
		}
		catch (Exception)
		{
		}
	}

	public void gps(Message msg)
	{
		GameMidlet.instance.CheckPerGPS();
	}

	public void getDamList(Message msg)
	{
		try
		{
			mVector mVector2 = new mVector();
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				MainClan mainClan = new MainClan();
				mainClan.name = msg.reader().readUTF();
				mainClan.idIcon = msg.reader().readShort();
				mainClan.xp = msg.reader().readInt();
				mVector2.addElement(mainClan);
			}
			Interface_Game.vecClanDam.removeAllElements();
			Interface_Game.vecClanDam = mVector2;
		}
		catch (Exception)
		{
		}
	}

	public void getInfoClanDao(Message msg)
	{
		try
		{
			GameScreen.ClanDao = new MainClan();
			GameScreen.ClanDao.ID = msg.reader().readShort();
			GameScreen.ClanDao.idIcon = msg.reader().readShort();
			GameScreen.ClanDao.name = msg.reader().readUTF();
			GameScreen.ClanDao.nameCaption = msg.reader().readUTF();
			GameScreen.ClanDao.level = msg.reader().readShort();
			GameScreen.ClanDao.numMem = msg.reader().readByte();
			GameScreen.ClanDao.maxNumMem = msg.reader().readByte();
			GameScreen.ClanDao.rank = msg.reader().readInt();
		}
		catch (Exception)
		{
		}
	}

	public void getThanhTich(Message msg)
	{
		try
		{
			short iD = msg.reader().readShort();
			sbyte tem = msg.reader().readByte();
			MainObject mainObject = MainObject.get_Object(iD, tem);
			if (mainObject != null && !mainObject.returnAction())
			{
				mainObject.thanhtichPvP = msg.reader().readByte();
				mainObject.thanhtichLv = msg.reader().readByte();
				mainObject.indexFullSet = msg.reader().readByte();
			}
		}
		catch (Exception)
		{
		}
	}

	public void check_AFK(Message msg)
	{
		try
		{
			if (GameCanvas.tickAction <= 0)
			{
				GlobalService.gI().Check_AFK();
			}
		}
		catch (Exception)
		{
		}
	}

	public void listGiftArea(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			for (int i = 0; i < b; i++)
			{
				MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 0);
				short idIconChat = msg.reader().readShort();
				sbyte cat = msg.reader().readByte();
				short num = msg.reader().readShort();
				mainObject?.addChatItem(idIconChat, cat, num);
			}
		}
		catch (Exception)
		{
		}
	}

	public void Nap_The(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
				GameCanvas.gameScr.Open_Dialog_Nap();
				break;
			case 1:
				GameCanvas.gameScr.Open_Nap_Store();
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void Thanh_Toan(Message msg)
	{
		try
		{
			GameMidlet.CheckThanhToan(msg.reader().readUTF());
		}
		catch (Exception)
		{
		}
	}

	public void title_Map_Fight(Message msg)
	{
		try
		{
			Interface_Game.typeTitleRoomFight = msg.reader().readByte();
		}
		catch (Exception)
		{
		}
	}

	public void ListDauGia(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				sbyte b = msg.reader().readByte();
				mVector mVector2 = new mVector();
				for (int i = 0; i < b; i++)
				{
					InfoMemList infoMemList = null;
					infoMemList = new InfoMemList(msg.reader().readByte());
					int money = msg.reader().readInt();
					int time = msg.reader().readInt();
					int giaChot = msg.reader().readInt();
					sbyte cat = msg.reader().readByte();
					string name = msg.reader().readUTF();
					short id = msg.reader().readShort();
					short num = msg.reader().readShort();
					sbyte color = msg.reader().readByte();
					sbyte isOwn = msg.reader().readByte();
					infoMemList.item = new ItemQuaNT(name, cat, id, num, color);
					infoMemList.item.setDauGia(money, time, giaChot);
					infoMemList.isOwn = isOwn;
					if (infoMemList != null)
					{
						mVector2.addElement(infoMemList);
					}
				}
				AuctionScreen.instance = new AuctionScreen(mVector2);
				AuctionScreen.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 1:
				AuctionScreen.instance.SetNewValueDauGia(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readInt(), msg.reader().readInt());
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void ListTichNapThe(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				int ruby = msg.reader().readInt();
				sbyte b = msg.reader().readByte();
				mVector mVector2 = new mVector();
				for (int i = 0; i < b; i++)
				{
					InfoMemList infoMemList = null;
					infoMemList = new InfoMemList(msg.reader().readByte());
					int cost = msg.reader().readInt();
					sbyte status = msg.reader().readByte();
					short num = msg.reader().readShort();
					infoMemList.quaNapThe = new QuaNapThe(cost, status, num);
					mVector mVector3 = new mVector();
					for (int j = 0; j < num; j++)
					{
						ItemQuaNT itemQuaNT = new ItemQuaNT(msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte());
						if (itemQuaNT != null)
						{
							mVector3.addElement(itemQuaNT);
						}
					}
					infoMemList.quaNapThe.vecQua = mVector3;
					if (infoMemList != null)
					{
						mVector2.addElement(infoMemList);
					}
				}
				NapTheScreen.instance = new NapTheScreen(mVector2, ruby);
				NapTheScreen.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 2:
			{
				sbyte cmdDaNhanIndex = msg.reader().readByte();
				NapTheScreen.instance.setCmdDaNhanIndex(cmdDaNhanIndex);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void Read_Sudo(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz("Read SUDO type = " + b);
			switch (b)
			{
			case 3:
			{
				mVector mVector2 = new mVector();
				sbyte b3 = msg.reader().readByte();
				mSystem.outz("Read SUDO chucDanh = " + b3);
				Player.ChucInSudo = b3;
				if (Player.ChucInSudo != 0)
				{
					string empty = string.Empty;
					mVector2.addElement(new MainInfoItem(b3 switch
					{
						1 => T.chucDanh + T.suPhu, 
						2 => T.chucDanh + T.deTu, 
						_ => string.Empty, 
					}, 0));
					sbyte value = msg.reader().readByte();
					mSystem.outz("Read SUDO level = " + value);
					mVector2.addElement(new MainInfoItem(T.capDoSudo, value));
					short num2 = msg.reader().readShort();
					short exp = msg.reader().readShort();
					mSystem.outz("Read SUDO lvExp = " + num2 + " exp = " + exp);
					mVector2.addElement(new MainInfoItem(T.diemSudoCanhan, 0));
					mVector2.addElement(new MainInfoItem(T.Lv + ": " + num2, 0));
					Sudo_Info.instance.exp = exp;
					sbyte b4 = msg.reader().readByte();
					mSystem.outz("Read SUDO sizeAtt = " + b4);
					for (int j = 0; j < b4; j++)
					{
						string text = msg.reader().readUTF();
						mSystem.outz("Read SUDO nameAtt = " + text);
						mVector2.addElement(new MainInfoItem(text, 0));
					}
					Sudo_Info.vecInfoCanhan = mVector2;
					Sudo_Info.instance.setHplus();
					GameCanvas.SudoScr.Show(GameCanvas.gameScr);
				}
				return;
			}
			case 2:
			{
				mVector mVector3 = new mVector();
				sbyte b6 = msg.reader().readByte();
				mSystem.outz("Read SUDO sizeList = " + b6);
				for (int l = 0; l < b6; l++)
				{
					InfoMemList infoMemList = new InfoMemList(-1);
					string text2 = msg.reader().readUTF();
					string text3 = msg.reader().readUTF();
					short head = msg.reader().readShort();
					short hair = msg.reader().readShort();
					short hat = msg.reader().readShort();
					infoMemList.title = text2;
					infoMemList.name = text3;
					infoMemList.updateFace(head, hair, hat);
					mSystem.outz("Read SUDO " + text2 + " " + text3 + " " + head + " " + hair + " " + hat);
					infoMemList.chucInSudo = msg.reader().readByte();
					infoMemList.Lv = msg.reader().readShort();
					infoMemList.expSudo = msg.reader().readUTF();
					infoMemList.typeOnline = msg.reader().readByte();
					mVector3.addElement(infoMemList);
					if (text3.CompareTo(GameScreen.player.name) == 0)
					{
						Player.ChucInSudo = infoMemList.chucInSudo;
					}
				}
				Sudo_Mem.vecSudo = mVector3;
				return;
			}
			case 0:
				if (GameScreen.player.clan == null)
				{
					GameScreen.player.clan = new MainClan(msg.reader().readShort(), msg.reader().readUTF());
					return;
				}
				GameScreen.player.clan.ID = msg.reader().readShort();
				GameScreen.player.clan.name = msg.reader().readUTF();
				return;
			case 1:
			{
				if (GameScreen.player.clan == null)
				{
					GlobalService.gI().Clan_CMD(9, string.Empty, 0, 0);
					return;
				}
				string name2 = msg.reader().readUTF();
				sbyte b5 = msg.reader().readByte();
				InfoMemList memInCLan = Clan_Screen.getMemInCLan(name2);
				if (memInCLan != null)
				{
					memInCLan.chucInClan = b5;
					if (memInCLan.name.CompareTo(GameScreen.player.name) == 0)
					{
						MainClan.UpdateRankMe(b5);
					}
				}
				return;
			}
			case 4:
			{
				if (GameScreen.player.clan == null)
				{
					GlobalService.gI().Clan_CMD(9, string.Empty, 0, 0);
					return;
				}
				short maxAttri = msg.reader().readShort();
				short pointAttri = msg.reader().readShort();
				short[] array = new short[5];
				for (int k = 0; k < array.Length; k++)
				{
					array[k] = msg.reader().readShort();
				}
				GameScreen.player.clan.pointAttri = pointAttri;
				GameScreen.player.clan.maxAttri = maxAttri;
				GameScreen.player.clan.setAttri(array);
				return;
			}
			case 13:
			{
				MainObject mainObject = MainObject.get_Object(msg.reader().readShort(), 0);
				if (mainObject != null)
				{
					short iD = msg.reader().readShort();
					sbyte b2 = msg.reader().readByte();
					if (mainObject.sudo == null)
					{
						mainObject.sudo = new MainSudo(iD, b2);
					}
					else
					{
						mainObject.sudo.ID = iD;
						mainObject.sudo.chucvu = b2;
					}
					if (mainObject == GameScreen.player)
					{
						MainSudo.UpdateRankMe(b2);
					}
					short level = msg.reader().readShort();
					string diemSudo = msg.reader().readUTF();
					mainObject.sudo.level = level;
					mainObject.sudo.diemSudo = diemSudo;
				}
				return;
			}
			case 8:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					read_Data_Chat(msg, 0);
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.clanchat);
				}
				return;
			case 7:
			{
				int iD2 = msg.reader().readInt();
				string name = msg.reader().readUTF();
				InfoMemList.setTypeEvent(iD2, 8, name, T.baisu, 0, 0);
				return;
			}
			case 9:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					GameScreen.player.clan.vecChatCLan.removeAllElements();
					short num = msg.reader().readShort();
					for (int i = 0; i < num; i++)
					{
						read_Data_Chat(msg, 0);
					}
				}
				return;
			case 11:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					short id = msg.reader().readShort();
					GameCanvas.ClanScr.clanchat.RemoveChat(id);
				}
				return;
			case 10:
			{
				MainObject mainObject2 = MainObject.get_Object(msg.reader().readShort(), 0);
				if (mainObject2 != null && mainObject2.sudo != null)
				{
					mainObject2.sudo = null;
				}
				if (mainObject2 == GameScreen.player && GameCanvas.currentScreen == GameCanvas.SudoScr)
				{
					GameCanvas.gameScr.Show();
					GameCanvas.SudoScr = null;
					Player.isGetDataClan = -1;
				}
				return;
			}
			case 12:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					switch (msg.reader().readByte())
					{
					case 0:
						readMemInCLan(msg);
						break;
					case 1:
						Clan_Screen.getRemoveMemInCLan(msg.reader().readUTF());
						break;
					}
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.member);
				}
				return;
			}
			switch (b)
			{
			case 13:
			{
				int countDown = msg.reader().readInt();
				MainTab.CDDonateClan.setCountDown(countDown);
				int numTangqua = msg.reader().readInt();
				InfoMemList memInCLan2 = Clan_Screen.getMemInCLan(GameScreen.player.ID);
				if (memInCLan2 != null)
				{
					memInCLan2.numTangqua = numTangqua;
				}
				break;
			}
			case 14:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					string text4 = msg.reader().readUTF();
					GameScreen.player.clan.strVoice = text4;
					GameCanvas.ClanScr.info.getmStrInfo(text4, GameCanvas.ClanScr.info.wCon - 6);
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.info);
				}
				break;
			case 15:
			{
				short[] array2 = new short[5];
				for (int m = 0; m < 5; m++)
				{
					array2[m] = msg.reader().readShort();
				}
				for (int n = 0; n < Player.mAttribute.Length; n++)
				{
					Main_Attribute main_Attribute = Player.mAttribute[n];
					if (n < array2.Length)
					{
						main_Attribute.valuePlus = array2[n];
					}
				}
				break;
			}
			case 16:
				if (GameScreen.player.clan != null)
				{
					GameScreen.player.clan.isLevelUp = msg.reader().readByte();
					GameScreen.player.clan.xp = msg.reader().readInt();
				}
				break;
			case 17:
				if (GameScreen.player.clan != null)
				{
					GameScreen.player.clan.rubiClan = msg.reader().readInt();
					GameScreen.player.clan.beryClan = msg.reader().readInt();
				}
				break;
			case 19:
				update_Inven_Clan(msg);
				break;
			case 20:
				if (GameScreen.player.clan != null && GameCanvas.ClanScr != null)
				{
					GameCanvas.ClanScr.achi.vecDetail.removeAllElements();
					while (msg.reader().available() > 0)
					{
						read_Data_Chat(msg, 1);
					}
					GameCanvas.ClanScr.setIsNewTab(GameCanvas.ClanScr.achi);
				}
				break;
			case 21:
			{
				string info = msg.reader().readUTF();
				if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
				{
					GameCanvas.gameScr.Show();
				}
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void ListTichCongDon(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.outz("type = " + b);
			switch (b)
			{
			case 0:
			{
				sbyte b2 = msg.reader().readByte();
				mSystem.outz("size = " + b2);
				mVector mVector2 = new mVector();
				for (int i = 0; i < b2; i++)
				{
					InfoMemList infoMemList = null;
					short cost2 = msg.reader().readShort();
					int rubyDaNap = msg.reader().readInt();
					mSystem.outz("ruby = " + rubyDaNap);
					infoMemList = new InfoMemList(i);
					sbyte status = 1;
					sbyte b3 = msg.reader().readByte();
					infoMemList.quaNapThe = new QuaNapThe(cost2, status, b3);
					infoMemList.quaNapThe.rubyDaNap = rubyDaNap;
					mVector mVector3 = new mVector();
					for (int j = 0; j < b3; j++)
					{
						ItemQuaNT itemQuaNT = new ItemQuaNT(msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte());
						if (itemQuaNT != null)
						{
							mVector3.addElement(itemQuaNT);
						}
					}
					infoMemList.quaNapThe.vecQua = mVector3;
					if (infoMemList != null)
					{
						mVector2.addElement(infoMemList);
					}
				}
				TichLuyCongDonScr.instance = new TichLuyCongDonScr(mVector2);
				TichLuyCongDonScr.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 2:
			{
				short cost = msg.reader().readShort();
				int ruby = msg.reader().readInt();
				TichLuyCongDonScr.instance.setUpdateCostRuby(cost, ruby);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void ListTichTieu(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				int ruby = msg.reader().readInt();
				sbyte b = msg.reader().readByte();
				mVector mVector2 = new mVector();
				for (int i = 0; i < b; i++)
				{
					InfoMemList infoMemList = null;
					infoMemList = new InfoMemList(msg.reader().readByte());
					int cost = msg.reader().readInt();
					sbyte status = msg.reader().readByte();
					short num = msg.reader().readShort();
					infoMemList.quaNapThe = new QuaNapThe(cost, status, num);
					mVector mVector3 = new mVector();
					for (int j = 0; j < num; j++)
					{
						ItemQuaNT itemQuaNT = new ItemQuaNT(msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte());
						if (itemQuaNT != null)
						{
							mVector3.addElement(itemQuaNT);
						}
					}
					infoMemList.quaNapThe.vecQua = mVector3;
					if (infoMemList != null)
					{
						mVector2.addElement(infoMemList);
					}
				}
				TichTieuScreen.instance = new TichTieuScreen(mVector2, ruby);
				TichTieuScreen.instance.Show(GameCanvas.gameScr);
				break;
			}
			case 2:
			{
				sbyte cmdDaNhanIndex = msg.reader().readByte();
				TichTieuScreen.instance.setCmdDaNhanIndex(cmdDaNhanIndex);
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void ListWantedServer(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mVector mVector2 = new mVector();
			switch (b)
			{
			case 0:
			{
				short num2 = msg.reader().readShort();
				for (int j = 0; j < num2; j++)
				{
					InfoMemList infoMemList2 = null;
					infoMemList2 = ReadInfoMemWantedWarrant(msg);
					infoMemList2.typeOnline = msg.reader().readByte();
					if (infoMemList2 != null)
					{
						mVector2.addElement(infoMemList2);
					}
				}
				WantedList.instance = new WantedList(b, mVector2);
				WantedList.instance.Show(GameCanvas.gameScr);
				WantedList.instance.isLoad = false;
				break;
			}
			case 3:
			{
				short num = msg.reader().readShort();
				for (int i = 0; i < num; i++)
				{
					InfoMemList infoMemList = null;
					infoMemList = ReadInfoMemWantedWarrant(msg);
					infoMemList.namePlayerNhan = msg.reader().readUTF();
					infoMemList.isWantedSuccess = msg.reader().readByte();
					infoMemList.isReceiveGift = msg.reader().readByte();
					if (infoMemList != null)
					{
						mVector2.addElement(infoMemList);
					}
				}
				WantedList.instance = new WantedList(b, mVector2);
				WantedList.instance.Show(GameCanvas.gameScr);
				WantedList.instance.isLoad = false;
				break;
			}
			case 1:
			case 4:
			case 5:
				ListWantedServerTypeViewInfo(msg, b);
				break;
			case 2:
				break;
			}
		}
		catch (Exception)
		{
		}
	}

	public void ListWantedServerTypeViewInfo(Message msg, sbyte type)
	{
		InfoMemList infoMemList = null;
		try
		{
			infoMemList = new InfoMemList(msg.reader().readShort());
			infoMemList.charShow = new MainObject();
			infoMemList.charShow.name = msg.reader().readUTF();
			infoMemList.charShow.wanted = msg.reader().readInt();
			infoMemList.charShow.Lv = msg.reader().readShort();
			short head = msg.reader().readShort();
			short hair = msg.reader().readShort();
			sbyte b = msg.reader().readByte();
			short[] array = new short[b];
			for (sbyte b2 = 0; b2 < b; b2++)
			{
				array[b2] = msg.reader().readShort();
			}
			infoMemList.updateCharFullPart(head, hair, array[1], array[3], array[5], array[0]);
			if (type == 4 || type == 5)
			{
				infoMemList.isWantedSuccess = msg.reader().readByte();
			}
			if (type == 5)
			{
				infoMemList.isReceiveGift = msg.reader().readByte();
			}
			WantedPoster.instance = new WantedPoster(type, infoMemList);
			WantedPoster.instance.Show(GameCanvas.gameScr);
		}
		catch (Exception)
		{
		}
	}

	public InfoMemList ReadInfoMemWantedWarrant(Message msg)
	{
		InfoMemList infoMemList = null;
		try
		{
			infoMemList = new InfoMemList(msg.reader().readShort());
			infoMemList.name = msg.reader().readUTF();
			if (infoMemList.name.CompareTo(GameScreen.player.name) == 0)
			{
				infoMemList.isMe = true;
			}
			if (infoMemList.charShow == null)
			{
				infoMemList.charShow = new MainObject();
			}
			infoMemList.charShow.wanted = msg.reader().readInt();
			infoMemList.charShow.name = msg.reader().readUTF();
			infoMemList.charShow.Lv = msg.reader().readShort();
			infoMemList.updateCharFace(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
		}
		catch (Exception)
		{
		}
		return infoMemList;
	}

	public void infoFashion(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				short num2 = msg.reader().readShort();
				if (num2 > 0)
				{
					MainObject.ListLechHEAD = new short[num2];
					for (int j = 0; j < num2; j++)
					{
						MainObject.ListLechHEAD[j] = msg.reader().readShort();
					}
				}
				break;
			}
			case 1:
			{
				short num3 = msg.reader().readShort();
				MainItem.ID_POTION_CAN_SELL = new short[num3];
				for (int k = 0; k < num3; k++)
				{
					MainItem.ID_POTION_CAN_SELL[k] = msg.reader().readShort();
				}
				short num4 = msg.reader().readShort();
				MainItem.ID_MATERIAL_CAN_SELL = new short[num4];
				for (int l = 0; l < num4; l++)
				{
					MainItem.ID_MATERIAL_CAN_SELL[l] = msg.reader().readShort();
				}
				break;
			}
			case 2:
			{
				short num5 = msg.reader().readShort();
				MainObject.idEffBody = new short[num5];
				for (int m = 0; m < num5; m++)
				{
					MainObject.idEffBody[m] = msg.reader().readShort();
				}
				short num6 = msg.reader().readShort();
				MainObject.idPlayFrameHead = new short[num6];
				for (int n = 0; n < num6; n++)
				{
					MainObject.idPlayFrameHead[n] = msg.reader().readShort();
				}
				short num7 = msg.reader().readShort();
				MainObject.idWeaponF = new short[num7];
				for (int num8 = 0; num8 < num7; num8++)
				{
					MainObject.idWeaponF[num8] = msg.reader().readShort();
				}
				break;
			}
			case 3:
			{
				short num = msg.reader().readShort();
				if (num > 0)
				{
					MainObject.ListKoLechHEAD = new short[num];
					for (int i = 0; i < num; i++)
					{
						MainObject.ListKoLechHEAD[i] = msg.reader().readShort();
					}
				}
				break;
			}
			}
		}
		catch (Exception)
		{
		}
	}

	public void vongsinhtu(Message msg)
	{
		try
		{
			if (msg.reader().readByte() == 0)
			{
				GameCanvas.gameScr.isFullScreen = false;
				GameCanvas.gameScr.xRec = msg.reader().readShort();
				GameCanvas.gameScr.yRec = msg.reader().readShort();
				GameCanvas.gameScr.wRec = msg.reader().readShort();
				GameCanvas.gameScr.hRec = msg.reader().readShort();
				GameCanvas.gameScr.colorRec = msg.reader().readInt();
				if (GameCanvas.gameScr.wRec == 0 && GameCanvas.gameScr.hRec == 0)
				{
					GameCanvas.gameScr.isFullScreen = true;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void getDataEff(Message msg)
	{
		try
		{
			switch (msg.reader().readByte())
			{
			case 0:
			{
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().read(ref data);
				DataSkillEff.readData(data, isSave: true);
				break;
			}
			case 1:
			{
				short num2 = msg.reader().readShort();
				short id2 = msg.reader().readShort();
				int time = msg.reader().readInt();
				sbyte typemove = msg.reader().readByte();
				sbyte loop = msg.reader().readByte();
				MainObject mainObject2 = GameScreen.findObjByID(num2);
				if (mainObject2 == null && GameScreen.player != null && (GameScreen.player.ID == num2 || num2 == 0))
				{
					mainObject2 = GameScreen.player;
				}
				if (mainObject2 != null)
				{
					mainObject2.addDataEff(id2, time, typemove, loop);
					mSystem.outz("[EFF Client] Added effect id=" + id2 + " to obj=" + num2 + " (" + mainObject2.name + "), time=" + time + ", move=" + typemove + ", loop=" + loop);
				}
				else
				{
					mSystem.outz("[EFF Client Warning] Target obj=" + num2 + " not found for effect id=" + id2);
				}
				break;
			}
			case 2:
			{
				short num = msg.reader().readShort();
				short id = msg.reader().readShort();
				MainObject mainObject = GameScreen.findObjByID(num);
				if (mainObject == null && GameScreen.player != null && (GameScreen.player.ID == num || num == 0))
				{
					mainObject = GameScreen.player;
				}
				if (mainObject != null)
				{
					mainObject.removeDataEff(id);
					mSystem.outz("[EFF Client] Removed effect id=" + id + " from obj=" + num);
				}
				break;
			}
			}
		}
		catch (Exception ex)
		{
			mSystem.outz("[EFF Client Error] getDataEff exception: " + ex.Message);
		}
	}

	public void openGhepnhieu(Message msg)
	{
		try
		{
			string nameScreen = msg.reader().readUTF();
			sbyte b = msg.reader().readByte();
			MainItem[] array = new MainItem[b + 1];
			for (int i = 0; i < b; i++)
			{
				short num = msg.reader().readShort();
				short numPotionNeed = msg.reader().readShort();
				sbyte type = msg.reader().readByte();
				short idIcon = msg.reader().readShort();
				MainItem mainItem = (array[i] = new MainItem(type, idIcon, num)
				{
					numPotionNeed = numPotionNeed
				});
				MainItem itemVec = MainItem.getItemVec(type, num, Player.vecInventory);
				if (itemVec != null)
				{
					mainItem.numPotion = itemVec.numPotion;
				}
			}
			int valueMonney_ = msg.reader().readInt();
			short valueMonney_2 = msg.reader().readShort();
			int valueMonney_3 = msg.reader().readInt();
			short iD = msg.reader().readShort();
			short numPotion = msg.reader().readShort();
			sbyte type2 = msg.reader().readByte();
			short idIcon2 = msg.reader().readShort();
			sbyte valueTile = msg.reader().readByte();
			MainItem mainItem2 = new MainItem(type2, idIcon2, iD);
			mainItem2.numPotion = numPotion;
			array[^1] = mainItem2;
			ScreenUpgrade.instance = new ScreenUpgrade(20, b);
			ScreenUpgrade.instance.setInfo_money(valueTile, valueMonney_, valueMonney_2, valueMonney_3);
			ScreenUpgrade.instance.setInfo(array);
			ScreenUpgrade.instance.nameScreen = nameScreen;
			ScreenUpgrade.instance.Show(GameCanvas.gameScr);
		}
		catch (Exception)
		{
		}
	}

	public void quickPotionOpen(Message msg)
	{
		try
		{
			if (msg.reader().readByte() == 1)
			{
				short num = msg.reader().readShort();
				for (int i = 0; i < num; i++)
				{
					ItemQuickOpen o = new ItemQuickOpen(msg.reader().readShort());
					GameScreen.vecQuickOpenPotion.addElement(o);
				}
			}
		}
		catch (Exception)
		{
		}
	}
}
