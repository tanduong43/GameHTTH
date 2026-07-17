using System;

public class Cmd_Message
{
	public ISession session = Session_ME.gI();

	protected Message m;

	public const sbyte GET_SESSION_ID = -27;

	public const sbyte SUDO = -108;

	public const sbyte GET_DATA_INFO_POTION = -105;

	public const sbyte WEAPON_FASHION = -104;

	public const sbyte REGISTER_NEW = -103;

	public const sbyte CHECK_AFK = -102;

	public const sbyte LOAD_IMAGE_BIG = -101;

	public const sbyte TEST_CMD = -100;

	public const sbyte LOGIN_PLUS = -99;

	public const sbyte TICH_LUY_CONG_DON = -97;

	public const sbyte TICH_TIEU = -96;

	public const sbyte HUY_HIEU_CLAN_VIP = -95;

	public const sbyte UPGRADE_DIAL = -94;

	public const sbyte LOAD_IMAGE_BIG_NEW = -93;

	public const sbyte LOAD_IMAGE_NEW = -92;

	public const sbyte DAU_GIA = -91;

	public const sbyte TICH_LUY_NAP_THE = -90;

	public const sbyte LENH_TRUY_NA = -89;

	public const sbyte DONOT_AUTO_RE = -88;

	public const sbyte EVENT_POKE = -87;

	public const sbyte CHEST_WANTED = -86;

	public const sbyte ROOM_WANTED = -85;

	public const sbyte DISCONNECT_WHY = -84;

	public const sbyte UPDATE_MP_HP_NEW = -83;

	public const sbyte GET_DATA_PART = -82;

	public const sbyte INPUT_PASSWORD = -81;

	public const sbyte PET = -80;

	public const sbyte LITTLE_GARDEN = -79;

	public const sbyte ATTRIBUTE_PLAYER = -78;

	public const sbyte CHUYEN_HOA = -77;

	public const sbyte UPDATE_INFO_MAIN_CHAR_INFO = -75;

	public const sbyte CHECK_LIST_PHO_BANG = -74;

	public const sbyte TIME_SHOW = -73;

	public const sbyte RED_LINE = -72;

	public const sbyte AUTO_REVICE = -71;

	public const sbyte NUM_PLAYER_MAP = -70;

	public const sbyte DIALOG_TIME = -69;

	public const sbyte BUY_GEM = -68;

	public const sbyte REBUILD_ITEM = -67;

	public const sbyte UPDATE_PVP_POINT = -66;

	public const sbyte BEGIN_GAME = -65;

	public const sbyte BUY_POTION_OK = -64;

	public const sbyte PVP = -63;

	public const sbyte UPDATE_PART_BOAT = -62;

	public const sbyte COUNT_DOWN_TICKET = -61;

	public const sbyte BUY_ITEM_SHOP = -60;

	public const sbyte UPDATE_CREATE_USER_OK = -59;

	public const sbyte INPUT_SERVER = -58;

	public const sbyte FRIST_LOGIN = -57;

	public const sbyte BOAT_IN_MAP = -56;

	public const sbyte GHOST = -55;

	public const sbyte HELP_SERVER = -54;

	public const sbyte SHIP = -53;

	public const sbyte CLAN = -52;

	public const sbyte LOAD_IMAGE = -51;

	public const sbyte SPLIT_ITEM = -50;

	public const sbyte TRADE = -49;

	public const sbyte UPGRADE_ITEM = -48;

	public const sbyte SET_WEATHER = -47;

	public const sbyte WORLD_CHANEL = -46;

	public const sbyte UPDATE_PK_POINT = -45;

	public const sbyte EFF_AUTO = -44;

	public const sbyte EXIT_VIEW = -43;

	public const sbyte SHOW_PLAYER_INFO = -42;

	public const sbyte SET_SHOW_IMAGE_ANDROID = -41;

	public const sbyte OK_IMAGE_ANDROID = -40;

	public const sbyte SAVE_IMAGE_ANDROID = -39;

	public const sbyte REQUEST_IMAGE_ANDROID = -38;

	public const sbyte DEL_CHAR = -37;

	public const sbyte MOVE_TO_PLAYER = -36;

	public const sbyte FIGHT = -35;

	public const sbyte SHOW_GIFT = -34;

	public const sbyte SAVE_RMS = -33;

	public const sbyte CHEST = -32;

	public const sbyte NOTIFY_SERVER = -31;

	public const sbyte LIST_FROM_SERVER = -30;

	public const sbyte FRIEND = -29;

	public const sbyte LEARN_SKILL = -28;

	public const sbyte REGISTER = -26;

	public const sbyte PARTY = -25;

	public const sbyte GET_INFO_NPC = -24;

	public const sbyte QUEST = -23;

	public const sbyte USE_ITEM = -22;

	public const sbyte SELL_ITEM = -21;

	public const sbyte DYNAMIC_MENU = -20;

	public const sbyte SHOP_NPC = -19;

	public const sbyte BUY_ITEM_POTION = -18;

	public const sbyte ADD_POINT_SKILL = -17;

	public const sbyte ADD_POINT_ATTRIBUTE = -16;

	public const sbyte EFF_OBJ = -15;

	public const sbyte UPDATE_MP_HP = -14;

	public const sbyte USE_POTION = -13;

	public const sbyte UPDATE_INVENTORY = -12;

	public const sbyte DIALOG_SERVER = -11;

	public const sbyte MAIN_CHAR_INFO = -10;

	public const sbyte SELECT_CHAR = -9;

	public const sbyte CREATE_CHAR = -8;

	public const sbyte GET_DATA = -7;

	public const sbyte CHECK_DATA_VERSION = -6;

	public const sbyte CHAR_INFO = -5;

	public const sbyte LIST_CHAR = -4;

	public const sbyte LOGIN = -2;

	public const sbyte CHANGE_MAP = 0;

	public const sbyte OBJ_MOVE = 1;

	public const sbyte PLAYER_FIRE = 2;

	public const sbyte REMOVE_CHAR = 3;

	public const sbyte MONSTER_INFO = 4;

	public const sbyte MONSTER_NONE_FOCUS = 5;

	public const sbyte PLAYER_REVICE = 6;

	public const sbyte PLAYER_DIE = 7;

	public const sbyte GET_XP_MAP_TRAIN = 9;

	public const sbyte UPDATE_XP = 10;

	public const sbyte ITEM_DROP = 11;

	public const sbyte GET_ITEM_MAP = 12;

	public const sbyte REMOVE_OBJ = 13;

	public const sbyte PK = 14;

	public const sbyte LIST_PK = 15;

	public const sbyte LIST_NPC = 16;

	public const sbyte CHAT_POPUP = 17;

	public const sbyte CHAT_TAB = 18;

	public const sbyte CHAR_WEARING = 19;

	public const sbyte BUFF = 20;

	public const sbyte CHANGE_AREA = 21;

	public const sbyte SKILL_PLAYER_MAP_TRAIN = 22;

	public const sbyte STATUS_AREA = 23;

	public const sbyte DIE_MONSTER = 24;

	public const sbyte NUM_ITEM_QUEST = 25;

	public const sbyte TELEPORT_BOSS = 26;

	public const sbyte BUFF_SPEC = 27;

	public const sbyte ADD_HP_EFF = 28;

	public const sbyte COMBO_SKILL = 29;

	public const sbyte OK_CHANGE_MAP_LINK = 30;

	public const sbyte UPDATE_NAME_NPC = 31;

	public const sbyte PARTY_BUFF = 32;

	public const sbyte GET_ITEM_MAP_LITTLE = 33;

	public const sbyte NEXT_MAP = 34;

	public const sbyte CHANGE_MAP_NON_DATA = 35;

	public const sbyte PVP_TYPE = 36;

	public const sbyte ARCHI_DAILY = 37;

	public const sbyte TABLE_MATCH = 38;

	public const sbyte UPDATE_XP_ARENA = 39;

	public const sbyte NEW_DIALOG = 40;

	public const sbyte TYPE_NPC_EVENT = 41;

	public const sbyte TIME_ITEM_DROP = 42;

	public const sbyte PAINT_CLIENT = 43;

	public const sbyte MARKET = 44;

	public const sbyte DEVIL_UPGRADE = 45;

	public const sbyte CHECK_PLAYER_IN_MAP = 46;

	public const sbyte CMD_EVENT = 47;

	public const sbyte CMD_GET_TEMPLATE = 48;

	public const sbyte ONE_POINT_MAX_LEVEL = 49;

	public const sbyte OPEN_MESSENGER = 50;

	public const sbyte UPDATE_LOL = 51;

	public const sbyte QUICK_CHAT = 52;

	public const sbyte UPDATE_POINT_WW = 53;

	public const sbyte QUAY_SO = 54;

	public const sbyte UPDATE_MP_HP_EFF = 55;

	public const sbyte EFF_KICH_AN = 57;

	public const sbyte COUNT_KICK_AVA = 58;

	public const sbyte GIAOTIEP_FORMSERVER = 59;

	public const sbyte DAILY_LOGIN = 60;

	public const sbyte GPS_GIFTCODE = 61;

	public const sbyte CLAN_DAMAGE_LIST = 62;

	public const sbyte GET_INFO_CLAN_DAO = 63;

	public const sbyte LIST_ITEM_GIFT_AREA = 64;

	public const sbyte GET_TOP_PLAYER = 65;

	public const sbyte UPGRADE_SUPER_ITEM = 66;

	public const sbyte TITLE_ROOM_FIGHT = 67;

	public const sbyte CHANGE_HAIR = 68;

	public const sbyte USE_ITEM_SELECT_ONE_ON_MORE = 69;

	public const sbyte UPDATE_XP_SKILL = 70;

	public const sbyte TRONG_CAY = 71;

	public const sbyte INFO_BODY_FASHION = 72;

	public const sbyte VONG_SINH_TU = 73;

	public const sbyte GET_BYTE_DATA_EFF = 74;

	public const sbyte TRANG_TRI = 75;

	public const sbyte GET_BYTE_DATA_EFF_BIG = 76;

	public const sbyte QUAY_OC_SEN = 77;

	public const sbyte HANH_TRINH = 79;

	public const sbyte EVENT = 80;

	public const sbyte UPGRADE_SKIN = 81;

	public const sbyte QUAY_WC = 82;

	public const sbyte GOOGLE_CMD = 99;

	public const sbyte FIRE_NEW = 100;

	public const sbyte CLAN_FIGHT = 101;

	public const sbyte NAP_THE = 102;

	public const sbyte TANG_RUBY = 103;

	protected void writeInt(int mint)
	{
		try
		{
			m.writer().writeInt(mint);
		}
		catch (Exception)
		{
		}
	}

	protected void writeByte(int by)
	{
		try
		{
			m.writer().writeByte(by);
		}
		catch (Exception)
		{
		}
	}

	protected void writeShort(int by)
	{
		try
		{
			m.writer().writeShort(by);
		}
		catch (Exception)
		{
		}
	}

	public void writeUTF(string str)
	{
		try
		{
			m.writer().writeUTF(str);
		}
		catch (Exception)
		{
		}
	}

	protected void writeBoolean(bool boo)
	{
		try
		{
			m.writer().writeBoolean(boo);
		}
		catch (Exception)
		{
		}
	}

	public void setSession(ISession gi)
	{
		session = null;
		session = gi;
	}

	public void send()
	{
		session.sendMessage(m);
		m.cleanup();
	}

	public void init(sbyte cmd)
	{
		m = new Message(cmd);
	}
}
