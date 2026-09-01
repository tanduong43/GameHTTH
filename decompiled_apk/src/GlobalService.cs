using System;
using UnityEngine;

public class GlobalService : Cmd_Message
{
	protected static GlobalService instance;

	public const sbyte DATA_MONSTER = 15;

	public const sbyte DATA_POTION = 25;

	public const sbyte DATA_POTION_FULL = 1;

	public const sbyte DATA_NAME_ATTRIBUTES = 2;

	public const sbyte DATA_SKILL = 3;

	public const sbyte DATA_LOCK_MAP = 4;

	public const sbyte DATA_CHAR_PART = 5;

	public const sbyte DATA_NAME_MAP = 6;

	public const sbyte DATA_NAME_POTION_QUEST = 7;

	public const sbyte DATA_SELL_SHOP = 8;

	public const sbyte DATA_ITEM_MAP = 9;

	public const sbyte DATA_MAP_LANG = 10;

	public const sbyte DATA_MATERIAL = 11;

	public const sbyte DATA_UPGRADE = 12;

	public const sbyte DATA_SEA = 13;

	public const sbyte DATA_TILE_CUONGHOA = 19;

	public const sbyte DATA_CLOCK_SERVER = 17;

	public const sbyte DATA_POTION_CLAN = 18;

	public const sbyte DATA_END_TT = 21;

	public const sbyte DATA_OFF_NAME = 22;

	public const sbyte DATA_PART_IMAGE_NEW = 23;

	public const sbyte DATA_POTION_NEW = 24;

	public const sbyte DATA_ATTRI_KICH_AN = 26;

	public const sbyte IS_CHIEM_DAO = 27;

	public const sbyte UPDATE_DATA_POTION = 28;

	public const sbyte UP_DATE_DATA_POTION_CLAN = 29;

	public const sbyte UP_DATE_DATA_SKILL = 30;

	public const sbyte DATA_EFF_HAIR = 31;

	public static bool isGetMaterial;

	public static bool isGetKichAn;

	public static short VerMonster;

	public static short VerPotion;

	public static short VerNameAtribute;

	public static short VerNameMap;

	public static short VerCharPar;

	public static short VerNamePotionQuest;

	public static short VerItemMap;

	public static short VerImageSave;

	public static short VerDataUpgrade;

	public static short verPotionClan;

	public const string STRING_MONSTER = "dataMon";

	public const string STRING_POTION = "dataPotion";

	public const string STRING_ATTRI = "dataAttri";

	public const string STRING_CHARPART = "dataCharPart";

	public const string STRING_NAMEMAP = "dataNameMap";

	public const string STRING_NAMEPOTIONQUEST = "dataNamePotionquest";

	public const string STRING_ITEMMAP = "dataItemMap";

	public const string STRING_IMAGE_SAVE = "dataImageSave";

	public const string STRING_DATA_UPGRADE = "dataUpgradeSave";

	public const string STRING_POTION_CLAN = "dataPotionClan";

	public const sbyte DATA_LIST_POTION_OPEN = 1;

	public const sbyte DATA_LIST_POTION_KHAM = -7;

	public const sbyte FRIEND_REQUEST = 0;

	public const sbyte FRIEND_DEL = 1;

	public const sbyte FRIEND_LIST = 2;

	public const sbyte FRIEND_ACCEPT = 3;

	public const sbyte SET_INVEN_TO_CHEST = 1;

	public const sbyte GET_CHEST_TO_INVEN = 2;

	public const sbyte REMOVE_FROM_CHEST = 4;

	public const sbyte UPGRADE_CHEST = 5;

	public const sbyte FIGHT_REQUEST = 0;

	public const sbyte FIGHT_ACC = 1;

	public const sbyte TYPE_FIGHT_NORMAL = 0;

	public const sbyte TYPE_FIGHT_BATTLE = 1;

	public const sbyte SHIP_CHANGE = 0;

	public const sbyte SHIP_START = 1;

	public const sbyte SHIP_ERROR = 2;

	public const sbyte CLAN_CHAT_ALL = 0;

	public const sbyte CLAN_KICK = 1;

	public const sbyte CLAN_DONATE = 2;

	public const sbyte CLAN_PHONG_CHUC = 3;

	public const sbyte CLAN_LEAVE = 4;

	public const sbyte CLAN_STATUS = 5;

	public const sbyte CLAN_ATTRI = 6;

	public const sbyte CLAN_ACCEPT_JOIN_CLAN = 7;

	public const sbyte CLAN_REQUEST_DATA_CLAN = 9;

	public const sbyte CLAN_MOI_VAO_CLAN = 10;

	public const sbyte CLAN_XIN_VAO_CLAN = 11;

	public const sbyte CLAN_NHAN_LOI_MOI = 12;

	public const sbyte CLAN_LEVEL_UP = 13;

	public const sbyte CLAN_USE_POTION = 14;

	public const sbyte CLAN_NAP_TIEN = 15;

	public const sbyte CLAN_NO_ACCEPT_JOIN_CLAN = 16;

	public const sbyte CLAN_UPDATE_LIST_MEM = 17;

	public const sbyte PAINT_HAT = 0;

	public const sbyte PAINT_WEAPON_FASHION = 1;

	public const sbyte MARKET_BUY = 0;

	public const sbyte MARKET_PAGE = 1;

	public const sbyte MARKET_OPEN = 2;

	public const sbyte MARKET_UPDATE = 3;

	public const sbyte MARKET_SELL = 4;

	public const sbyte MARKET_SEND_TO_INVEN = 5;

	public const sbyte MARKET_CANCLE_SELL = 6;

	public const sbyte MARKET_SELL_CHEST = 7;

	public const sbyte MARKET_REQUEST_PAGE = 8;

	public const sbyte MARKET_UPDATE_SHOP_ANY_ITEM = 9;

	public const sbyte MARKET_SELL_ANY_ITEM_FORM_INVEN = 10;

	public const sbyte MARKET_SELL_ANY_ITEM_FORM_CHEST = 11;

	public const sbyte EVENT_MERRY_CM = 0;

	public const sbyte ACTION_GET_MENU_TRANG_TRI = 0;

	public const sbyte ACTION_DOI_QUA = 1;

	public const sbyte ACTION_UPDATE_TRANG_TRI = 2;

	public const sbyte ACTION_OPEN = 3;

	public const sbyte ACTION_QUA_THA_TROI = 4;

	public const sbyte QUAY_1_LAN = 1;

	public const sbyte QUAY_3_LAN = 2;

	public const sbyte QUAY_REQUEST = 3;

	public const sbyte QUAY_BUY = 4;

	public const sbyte CLAN_FIGHT_REQUEST_LIST_CLAN = 0;

	public const sbyte CLAN_FIGHT_ACCEPT = 1;

	public const sbyte CLAN_FIGHT_NO_ACCEPT = 2;

	public const sbyte CLAN_FIGHT_REQUEST_AUTO = 3;

	public const sbyte CLAN_FIGHT_ERROR = 4;

	public const sbyte CLAN_FIGHT_REQUEST_FIGHT_CLAN = 5;

	public const sbyte CLAN_FIGHT_LIST_MEM_CLAN = 6;

	public const sbyte SUDO_CHAT_ALL = 15;

	public const sbyte SUDO_KICK = 16;

	public const sbyte SUDO_LEAVE = 17;

	public const sbyte SUDO_ACCEPT_JOIN_CLAN = 18;

	public const sbyte SUDO_XIN_VAO_CLAN = 19;

	public const sbyte SUDO_NO_ACCEPT_JOIN_CLAN = 20;

	public static GlobalService gI()
	{
		if (instance == null)
		{
			instance = new GlobalService();
		}
		return instance;
	}

	public void danhhieu(sbyte type, int id, sbyte action)
	{
		init(-102);
		try
		{
			m.writer().writeSByte(type);
			m.writer().writeInt(id);
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Login(string user, string pass, sbyte type)
	{
		init(-2);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeUTF(user);
			m.writer().writeUTF(pass);
			if (GameCanvas.isSuperLowGraphic)
			{
				m.writer().writeByte(0);
			}
			else if (GameCanvas.isIos())
			{
				m.writer().writeByte(GameMidlet.ZOOM_IOS);
			}
			else
			{
				m.writer().writeByte(mGraphics.zoomLevel);
			}
			m.writer().writeUTF("1.2.7");
			m.writer().writeByte(GameMidlet.DEVICE);
			m.writer().writeByte(ListChar_Screen.IndexCharSelected);
			m.writer().writeUTF(GameMidlet.loginPlus());
			m.writer().writeUTF("checkmodhaitac:android:isolatedSplits=true");
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Obj_Move(short x, short y)
	{
		init(1);
		try
		{
			m.writer().writeShort(x);
			m.writer().writeShort(y);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void load_image(short id, short cat)
	{
		if (cat == 10000 && id >= 10000)
		{
			cat = 26000;
			id -= 10000;
		}
		init(-51);
		try
		{
			_ = 400;
			m.writer().writeShort(id + cat);
			if (cat == 1000)
			{
				Debug.Log((object)$"[GLOBAL_SERVICE load_image] Sending -51 for monster icon id={id + cat} (rawId={id})");
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Load_Image_New(sbyte type, short id)
	{
		init(-92);
		try
		{
			m.writer().write(type);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void char_info(short id)
	{
		init(-5);
		try
		{
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void monster_info(short id)
	{
		init(4);
		try
		{
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void get_DATA(sbyte type)
	{
		init(-7);
		try
		{
			m.writer().writeByte(type);
		}
		catch (Exception)
		{
		}
		send();
		mSystem.outz("getdata=" + type);
	}

	public void Player_Fire(short idSkill, sbyte CatBeFire, mVector VecBeFire)
	{
		init(2);
		sbyte b = 0;
		try
		{
			m.writer().writeShort(idSkill);
			m.writer().writeByte(CatBeFire);
			b = (sbyte)VecBeFire.size();
			m.writer().writeByte(b);
			if (b == 0)
			{
				return;
			}
			for (int i = 0; i < b; i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)VecBeFire.elementAt(i);
				m.writer().writeShort(object_Effect_Skill.ID);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Player_Revice(sbyte type)
	{
		init(6);
		try
		{
			m.writer().writeByte(type);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Get_Xp_Map_Train(sbyte type)
	{
		init(9);
		try
		{
			m.writer().writeByte(type);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Select_Char(short id, sbyte type, short idSupport)
	{
		if (GameCanvas.tickSelectChar <= 0)
		{
			init(-9);
			try
			{
				m.writer().writeShort(id);
				m.writer().writeByte(type);
				m.writer().writeShort(idSupport);
			}
			catch (Exception)
			{
			}
			send();
			GameCanvas.tickSelectChar = 100;
		}
	}

	public void Del_Char(sbyte type, short id)
	{
		init(-37);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Create_Char(string name, sbyte clazz, short head, short hair)
	{
		init(-8);
		try
		{
			m.writer().writeUTF(name);
			m.writer().writeByte(clazz);
			m.writer().writeShort(head);
			m.writer().writeShort(hair);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Choice_Dialog_server(short id, sbyte value)
	{
		init(-11);
		try
		{
			m.writer().writeShort(id);
			m.writer().writeByte(value);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Get_Item_Map(short id, sbyte cat)
	{
		init(12);
		try
		{
			m.writer().writeShort(id);
			m.writer().writeByte(cat);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Use_Potion(short id)
	{
		init(-13);
		try
		{
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Set_PK(sbyte type, sbyte action)
	{
		init(14);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Add_Point_Attribute(sbyte index, short value)
	{
		init(-16);
		try
		{
			m.writer().writeByte(index);
			m.writer().writeShort(value);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Add_Point_Skill(short ID, short value)
	{
		init(-17);
		try
		{
			m.writer().writeShort(ID);
			m.writer().writeShort(value);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Buy_Item_Potion(sbyte TypeShop, short ID, short value, sbyte cat)
	{
		mSystem.outz("Buy_Item_Potion " + ID);
		init(-18);
		try
		{
			m.writer().writeByte(TypeShop);
			m.writer().writeShort(ID);
			m.writer().writeShort(value);
			if (TypeShop == 116 || TypeShop == 118)
			{
				m.writer().writeByte(cat);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Dynamic_Menu(short idNPC, sbyte idMenu, sbyte index)
	{
		init(-20);
		try
		{
			m.writer().writeShort(idNPC);
			m.writer().writeByte(idMenu);
			m.writer().writeByte(index);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void shop_NPC(short idNPC)
	{
		if (GameScreen.player != null)
		{
			Obj_Move((short)GameScreen.player.x, (short)GameScreen.player.y);
		}
		init(-19);
		try
		{
			m.writer().writeShort(idNPC);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Use_Item(short idItem, sbyte cat)
	{
		init(-22);
		try
		{
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Sell_Item(sbyte type, short idItem, sbyte cat, short num)
	{
		init(-21);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
			m.writer().writeShort(num);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void quest(sbyte action, short id)
	{
		init(-23);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Get_Info_NPC(short id)
	{
		init(-24);
		try
		{
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Party(sbyte type, short id)
	{
		init(-25);
		try
		{
			m.writer().writeByte(type);
			if (type == 0 || type == 2 || type == 4 || type == 6)
			{
				m.writer().writeShort(id);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void chatPopup(string text)
	{
		init(17);
		if (text.CompareTo("mod") == 0 || text.CompareTo("mob") == 0 || text.CompareTo("menumod") == 0 || text.CompareTo("hack") == 0)
		{
			ModMenu.ShowMenu();
			return;
		}
		if (text.CompareTo("dung") == 0 || text.CompareTo("item") == 0)
		{
			ModMenu.ShowUseItemSelectMenu();
			return;
		}
		if (text.CompareTo("ruong") == 0)
		{
			ModMenu.ShowChestSelectMenu();
			return;
		}
		if (text.CompareTo("gq") == 0 || text.CompareTo("gom") == 0 || text.CompareTo("gomquai") == 0)
		{
			ModMenu.isAutoGomQuai = !ModMenu.isAutoGomQuai;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Gom Quái: " + (ModMenu.isAutoGomQuai ? ("BẬT (" + ModMenu.gomQuaiDistance + "px)") : "TẮT");
			}
			return;
		}
		if (text.StartsWith("gq") && text.Length > 2)
		{
			try
			{
				int num = int.Parse(text.Substring(2).Trim());
				if (num >= 10 && num <= 300)
				{
					ModMenu.gomQuaiDistance = num;
					ModMenu.isAutoGomQuai = true;
					if (GameScreen.player != null)
					{
						GameScreen.player.strChatPopup = "Auto Gom Quái: BẬT (" + num + "px)";
					}
					return;
				}
			}
			catch (Exception)
			{
			}
		}
		if (text.CompareTo("ad") == 0 || text.CompareTo("danh") == 0 || text.CompareTo("autodanh") == 0)
		{
			ModMenu.isAutoDanh = !ModMenu.isAutoDanh;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Đánh: " + (ModMenu.isAutoDanh ? "BẬT" : "TẮT");
			}
			return;
		}
		if (text.CompareTo("all") == 0)
		{
			bool flag = (ModMenu.isAutoDanh = (ModMenu.isAutoGomQuai = !ModMenu.isAutoDanh));
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Gom + Đánh: " + (flag ? "BẬT" : "TẮT");
			}
			return;
		}
		if (text.CompareTo("qs") == 0 || text.CompareTo("quayso") == 0 || text.CompareTo("vongquay") == 0 || text.CompareTo("vqmm") == 0)
		{
			ModMenu.isAutoQuaySo = !ModMenu.isAutoQuaySo;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Vòng Quay May Mắn: " + (ModMenu.isAutoQuaySo ? "BẬT" : "TẮT");
			}
			return;
		}
		if (text.StartsWith("ruong") && text.Length > 5)
		{
			try
			{
				int num2 = int.Parse(text.Substring(5).Trim());
				if (num2 >= 0)
				{
					ModMenu.autoOpenChestLimit = num2;
					ModMenu.autoOpenChestCount = 0;
					ModMenu.isAutoOpenChest = true;
					if (GameScreen.player != null)
					{
						GameScreen.player.strChatPopup = ((num2 == 0) ? "Auto mở rương: Đến khi hết" : ("Auto mở rương: " + num2 + " cái"));
					}
					return;
				}
			}
			catch (Exception)
			{
			}
		}
		if (text.StartsWith("cao") && text.Length > 3)
		{
			try
			{
				int num3 = (ModMenu.offsetYName = int.Parse(text.Substring(3)));
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Độ cao tên: +" + num3 + "px";
				}
				return;
			}
			catch (Exception)
			{
			}
		}
		if (text.StartsWith("s") && text.Length > 1 && char.IsDigit(text[1]))
		{
			try
			{
				float num4 = float.Parse(text.Substring(1));
				if (num4 >= 0.1f && num4 <= 20f)
				{
					ModMenu.gameSpeed = num4;
					Time.timeScale = num4;
					if (GameScreen.player != null)
					{
						GameScreen.player.strChatPopup = "Tốc độ game: " + num4 + "x";
					}
					return;
				}
			}
			catch (Exception)
			{
			}
		}
		if (text.CompareTo("super") == 0)
		{
			checkChangeHair();
		}
		if (GameCanvas.istest)
		{
			if (text.CompareTo("opcchestadmin") == 0)
			{
				GameScreen.isOnChestAdmin = true;
				startMenuChest();
				return;
			}
			if (text.CompareTo("onmod123") == 0)
			{
				GameScreen.player.strChatPopup = "Bật Mod";
				GameScreen.typeMod = 1;
				return;
			}
			if (text.CompareTo("offmod123") == 0)
			{
				GameScreen.player.strChatPopup = "Tắt Mod";
				GameScreen.typeMod = 0;
				return;
			}
			if (text.CompareTo("muaruong") == 0)
			{
				HuyHieuClanScreen.instance = new HuyHieuClanScreen();
				HuyHieuClanScreen.instance.Show(GameCanvas.gameScr);
				return;
			}
			if (text.CompareTo("taixiu") == 0)
			{
				TaiXiuScreen.instance = new TaiXiuScreen(string.Empty, 0, 0, 0, 0, 60);
				TaiXiuScreen.instance.Show(GameCanvas.gameScr);
				return;
			}
			if (text.CompareTo("log") == 0)
			{
				GameScreen.isShowText = !GameScreen.isShowText;
				GameCanvas.vecTest.removeAllElements();
				mSystem.outz("clear xong");
			}
			if (text.CompareTo("clear") == 0)
			{
				GameCanvas.vecTest.removeAllElements();
				mSystem.outz("clear xong");
			}
			if (text.CompareTo("login") == 0)
			{
				GameCanvas.loginScr.doLogin(isGetData: false, 0, GameCanvas.loginScr.tfUser.getText(), GameCanvas.loginScr.tfPass.getText());
			}
			if (text.CompareTo("delrms") == 0)
			{
				GameMidlet.delAllRms();
				mSystem.outz("delete rms xong");
			}
			if (text.CompareTo("goal") == 0)
			{
				GameScreen.addEffectEnd(178, 0, GameScreen.player.x, GameScreen.player.y, 2, GameScreen.player);
			}
			try
			{
				int num5 = int.Parse(text);
				GameScreen.player.TestSkill((short)num5);
			}
			catch (Exception)
			{
			}
		}
		try
		{
			m.writer().writeUTF(text);
		}
		catch (Exception)
		{
		}
		send();
		GameCanvas.chatTabScr.addNewChat(T.tabServer, string.Empty, GameScreen.player.name + ": " + text, 1, isFocus: false);
	}

	private void checkChangeHair()
	{
		if (GameScreen.player.hair == 772 || GameScreen.player.hair == 773 || GameScreen.player.hair == 774)
		{
			init(68);
			send();
		}
	}

	public void startMenuChest()
	{
		mVector mVector2 = new mVector();
		ReadMessenge.cmdOffAll = new iCommand("Off All", 5, 3, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdOffAll);
		ReadMessenge.cmdOffInterface = new iCommand("Off Interface", 5, 0, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdOffInterface);
		ReadMessenge.cmdOffChat = new iCommand("Off Chat", 5, 1, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdOffChat);
		ReadMessenge.cmdOffName = new iCommand("Off Name", 5, 2, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdOffName);
		ReadMessenge.cmdShowPos = new iCommand("Show Pos", 5, 4, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdShowPos);
		ReadMessenge.cmdShowIpLocal = new iCommand("Show Ip Local", 5, 6, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdShowIpLocal);
		ReadMessenge.cmdShowSysout = new iCommand("Show System Out", 5, 5, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdShowSysout);
		ReadMessenge.cmdShowInTabAdmin = new iCommand("Show Info Tab Admin", 5, 7, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdShowInTabAdmin);
		iCommand o = new iCommand("Xét hiệu ứng Skill 5", 14, GameCanvas.readMess);
		mVector2.addElement(o);
		iCommand o2 = new iCommand("Xét hiệu ứng Skill 10", 15, GameCanvas.readMess);
		mVector2.addElement(o2);
		ReadMessenge.cmdSetSkillEff15 = new iCommand("Xét hiệu ứng Skill 15", 7, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdSetSkillEff15);
		ReadMessenge.cmdSetSkillEff20 = new iCommand("Xét hiệu ứng Skill 20", 9, GameCanvas.readMess);
		mVector2.addElement(ReadMessenge.cmdSetSkillEff20);
		GameCanvas.menu.startAt(mVector2, 2, "Chest Admin");
	}

	public void chatTab(string name, string str)
	{
		init(18);
		try
		{
			m.writer().writeUTF(name);
			m.writer().writeUTF(str);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Register(string name, string pass)
	{
		init(-26);
		try
		{
			m.writer().writeUTF(name);
			m.writer().writeUTF(pass);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Save_RMS_Server(sbyte type, sbyte id, sbyte[] mdata)
	{
		init(-33);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(id);
			if (mdata != null)
			{
				int num = mdata.Length;
				m.writer().writeShort(num);
				for (int i = 0; i < num; i++)
				{
					m.writer().writeByte(mdata[i]);
				}
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Friend(sbyte type, int Id)
	{
		init(-29);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeInt(Id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void ListFromServer(sbyte type, sbyte IdList, sbyte page)
	{
		init(-30);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(IdList);
			m.writer().writeByte(page);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Chest(sbyte action, short Id, sbyte cat, int num)
	{
		mSystem.outz("chestList " + action + " " + Id + " " + cat + " " + num);
		init(-32);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(Id);
			m.writer().writeByte(cat);
			m.writer().writeInt(num);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void CheckVersion()
	{
		init(-6);
		send();
	}

	public void Fight(sbyte type, short Id, sbyte typeFight)
	{
		init(-35);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(Id);
			m.writer().writeByte(typeFight);
		}
		catch (Exception)
		{
		}
		send();
		GameCanvas.tickAction = 4500;
	}

	public void Buff(short IdBuff, sbyte cat, mVector vec)
	{
		init(20);
		try
		{
			m.writer().writeShort(IdBuff);
			m.writer().writeByte(cat);
			m.writer().writeByte(vec.size());
			for (int i = 0; i < vec.size(); i++)
			{
				Object_Effect_Skill object_Effect_Skill = (Object_Effect_Skill)vec.elementAt(i);
				m.writer().writeShort(object_Effect_Skill.ID);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Learn_Skill(sbyte typeAction, short Index)
	{
		init(-28);
		try
		{
			m.writer().writeByte(typeAction);
			m.writer().writeShort(Index);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void List_Skill_Map_Train(short Id)
	{
		init(22);
		try
		{
			m.writer().writeShort(Id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Select_Area(sbyte type, sbyte select)
	{
		init(23);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(select);
		}
		catch (Exception)
		{
		}
		send();
		GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: true);
	}

	public void Move_To_Player(sbyte type, int ID)
	{
		init(-36);
		try
		{
			m.writer().writeInt(ID);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Request_Image_Android()
	{
		init(-38);
		try
		{
			if (GameCanvas.isIos())
			{
				m.writer().writeByte(GameMidlet.ZOOM_IOS);
			}
			else
			{
				m.writer().writeByte(mGraphics.zoomLevel);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Show_Player_Info(string name)
	{
		init(-42);
		try
		{
			m.writer().writeUTF(name);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Exit_View()
	{
		init(-43);
		send();
	}

	public void getDataEffAuto(short id)
	{
		init(-44);
		try
		{
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void getDataSkillEFf(sbyte type, short id)
	{
		init(74);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Update_Pk_Point()
	{
		init(-45);
		send();
	}

	public void World_Chanel(sbyte type, string text)
	{
		init(-46);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeUTF(text);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Upgrade_Item(sbyte type, short idItem, sbyte bery_gem)
	{
		init(-48);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(idItem);
			m.writer().writeByte(bery_gem);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Split_Item(sbyte type, sbyte action, short idItem, sbyte cat, short num)
	{
		init(-50);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
			m.writer().writeShort(num);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Hanh_Trinh(sbyte action, short idItem)
	{
		init(79);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(idItem);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Trade(sbyte action, short Id, sbyte cat, int num, string str)
	{
		init(-49);
		try
		{
			m.writer().writeByte(action);
			switch (action)
			{
			case 1:
			case 6:
				m.writer().writeShort(Id);
				m.writer().writeByte(cat);
				m.writer().writeInt(num);
				break;
			case 2:
				m.writer().writeUTF(str);
				break;
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Ship(sbyte action)
	{
		init(-53);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void ghost(sbyte id)
	{
		init(-55);
		try
		{
			m.writer().writeByte(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void changeMapOk()
	{
		init(0);
		try
		{
			m.writer().writeShort(GameCanvas.loadmap.idMap);
			m.writer().writeByte(ReadMessenge.actionChangeMap);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void OkChangeMapLink()
	{
		init(30);
		send();
	}

	public void Frist_Login(string str)
	{
		init(-57);
		try
		{
			m.writer().writeUTF(str);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void InputServer(short Id, string[] str)
	{
		init(-58);
		try
		{
			m.writer().writeShort(Id);
			m.writer().writeByte(str.Length);
			for (int i = 0; i < str.Length; i++)
			{
				m.writer().writeUTF(str[i]);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Use_Item_Shop(short id, sbyte typeShop)
	{
		init(-60);
		try
		{
			m.writer().writeShort(id);
			m.writer().writeByte(typeShop);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Update_Part_Boat()
	{
		init(-62);
		send();
	}

	public void PvP(sbyte action)
	{
		init(-63);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void BeginGame()
	{
		init(-65);
		send();
	}

	public void rebuild_Item(sbyte type, sbyte action, short idItem, sbyte cat, short num)
	{
		init(-67);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
			m.writer().writeShort(num);
			m.writer().writeShort(-1);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void rebuild_Item(sbyte type, sbyte action, short idItem, sbyte cat, short num, short idBua)
	{
		init(-67);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
			m.writer().writeShort(num);
			m.writer().writeShort(idBua);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void rebuild_Item_DIAL(sbyte type, sbyte action, short idItem, sbyte cat, short num, short hammer)
	{
		init(-67);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
			m.writer().writeShort(num);
			m.writer().writeShort(hammer);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Buy_Gem(string str1, string str2)
	{
		init(-68);
		try
		{
			m.writer().writeUTF(str1);
			m.writer().writeUTF(str2);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Update_Num_Player_Map()
	{
		init(-70);
		send();
	}

	public void Auto_revice(sbyte type)
	{
		init(-71);
		try
		{
			m.writer().writeByte(type);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Red_Line(sbyte type, sbyte key)
	{
		init(-72);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(key);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Check_List_Pho_Bang(sbyte type)
	{
		init(-74);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(ReadMessenge.IDDialogPhoBang);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Check_AFK()
	{
		init(-102);
		send();
	}

	public void Clan_CMD(sbyte type, string strChat, int id, sbyte chucvu)
	{
		init(-52);
		try
		{
			m.writer().writeByte(type);
			switch (type)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 5:
				m.writer().writeUTF(strChat);
				m.writer().writeByte(chucvu);
				break;
			case 10:
			case 11:
			case 12:
				m.writer().writeInt(id);
				break;
			case 6:
				m.writer().writeByte(chucvu);
				m.writer().writeByte(id);
				break;
			case 7:
			case 16:
				m.writer().writeUTF(strChat);
				m.writer().writeInt(id);
				break;
			case 14:
				m.writer().writeShort(id);
				m.writer().writeByte(chucvu);
				break;
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void ChuyenHoa(sbyte type, short idLeft, short idRight)
	{
		init(-77);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(idLeft);
			if (type == 2 || type == 3)
			{
				m.writer().writeShort(idRight);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void RegisterNew(string[] mstr)
	{
		init(-103);
		try
		{
			for (int i = 0; i < mstr.Length; i++)
			{
				m.writer().writeUTF(mstr[i]);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void NextMap(short idMap)
	{
		init(34);
		try
		{
			m.writer().writeShort(idMap);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Archi_Daily(sbyte index, sbyte type)
	{
		init(37);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(index);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void getDataPart(short index)
	{
		init(-82);
		try
		{
			m.writer().writeShort(index);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void getDataInfoPotion(short index)
	{
		init(-105);
		try
		{
			m.writer().writeShort(index);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void EventMoon(short id, sbyte type)
	{
		init(41);
		try
		{
			m.writer().writeShort(id);
			m.writer().writeByte(2);
		}
		catch (Exception)
		{
		}
		send();
	}

	public static short CheckEmotion(string str)
	{
		if (str.Length >= 3 && str.Substring(0, 2).CompareTo("et") == 0)
		{
			if (str.Length >= 4)
			{
				string text = str.Substring(0, 4);
				if (text.CompareTo("et10") == 0)
				{
					return 30;
				}
				if (text.CompareTo("et11") == 0)
				{
					return 31;
				}
				if (text.CompareTo("et12") == 0)
				{
					return 32;
				}
				if (text.CompareTo("et13") == 0)
				{
					return 33;
				}
				if (text.CompareTo("et14") == 0)
				{
					return 34;
				}
			}
			string text2 = str.Substring(0, 3);
			if (text2.CompareTo("et0") == 0)
			{
				return 20;
			}
			if (text2.CompareTo("et1") == 0)
			{
				return 21;
			}
			if (text2.CompareTo("et2") == 0)
			{
				return 22;
			}
			if (text2.CompareTo("et3") == 0)
			{
				return 23;
			}
			if (text2.CompareTo("et4") == 0)
			{
				return 24;
			}
			if (text2.CompareTo("et5") == 0)
			{
				return 25;
			}
			if (text2.CompareTo("et6") == 0)
			{
				return 26;
			}
			if (text2.CompareTo("et7") == 0)
			{
				return 27;
			}
			if (text2.CompareTo("et8") == 0)
			{
				return 28;
			}
			if (text2.CompareTo("et9") == 0)
			{
				return 29;
			}
		}
		return -1;
	}

	public void Paint_Client(sbyte type, sbyte value)
	{
		init(43);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(value);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Market(sbyte type, sbyte indexMarket, short ID, sbyte cat, short value)
	{
		init(44);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(indexMarket);
			m.writer().writeShort(ID);
			m.writer().writeByte(cat);
			m.writer().writeShort(value);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void checkPlayInMap(mVector vec)
	{
		init(46);
		try
		{
			for (int i = 0; i < vec.size(); i++)
			{
				MainObject mainObject = (MainObject)vec.elementAt(i);
				if (mainObject.typeObject == 0)
				{
					m.writer().writeShort(mainObject.ID);
				}
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Devil_Upgrade(sbyte action, short idItem, sbyte cat, short num)
	{
		init(45);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
			m.writer().writeShort(num);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void cmd_Event(sbyte action)
	{
		init(47);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
		mSystem.outz("Send event=" + action);
	}

	public void cmd_TrangTri(sbyte type, sbyte action)
	{
		init(75);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void GetTemplate(sbyte type, short value)
	{
		init(48);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(value);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void One_Point_Max_Level(sbyte action, short id)
	{
		init(49);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Room_Wanted(sbyte action)
	{
		init(-85);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Chest_Wanted(sbyte action, short ID)
	{
		init(-86);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(ID);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Use_Poke(short ID, short IDMon)
	{
		init(-87);
		try
		{
			m.writer().writeShort(ID);
			m.writer().writeShort(IDMon);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void reDisconectWhy()
	{
		init(-84);
		send();
	}

	public void Quick_Chat_LoL(sbyte action, sbyte index, sbyte isCap, string name)
	{
		init(51);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeByte(index);
			m.writer().writeByte(isCap);
			m.writer().writeUTF(name);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Quick_Chat_Game(sbyte index)
	{
		init(52);
		try
		{
			m.writer().writeByte(index);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Quayso(sbyte action)
	{
		init(54);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void QuayWC(sbyte action)
	{
		mSystem.outz("vao nao>>>>>>>>>>> goilen=" + action);
		init(82);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Quay_oc_sen(sbyte action)
	{
		init(77);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Huy_hieu(sbyte action, sbyte type, short id)
	{
		mSystem.outz("QUAY_HUY_HIEU>>>>>>>>>>> goilen=" + action + " type = " + type + " id = " + id);
		init(-95);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeByte(type);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Pet(sbyte action, sbyte type, short id)
	{
		mSystem.outz("Send_Pet>>>>>>>>>>> goilen=" + action + " type = " + type + " id = " + id);
		init(-80);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeByte(type);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Pet(sbyte action)
	{
		mSystem.outz("Send_Pet>>>>>>>>>>> goilen=" + action);
		init(-80);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void TaiXiu(sbyte type, sbyte action, int tienDatCuoc, sbyte cuaDatCuoc, sbyte isTatTay)
	{
		mSystem.outz("Tai Xiu >>>>>>>>>>> goilen=" + action + " tienDatCuoc = " + tienDatCuoc + " cua = " + cuaDatCuoc);
		init(80);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
			m.writer().writeInt(tienDatCuoc);
			m.writer().writeByte(cuaDatCuoc);
			m.writer().writeByte(isTatTay);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void TaiXiu(sbyte type, sbyte action)
	{
		mSystem.outz("Tai Xiu >>>>>>>>>>> goilen=" + action);
		init(80);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void VotBanhChung(sbyte type, sbyte action, sbyte status)
	{
		mSystem.outz("Vot banh >>>>>>>>>>> action=" + action + " status=" + status);
		init(80);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(action);
			m.writer().writeByte(status);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Giaotiep_FormServer(sbyte action, short idObj, sbyte cat)
	{
		init(59);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(idObj);
			m.writer().writeByte(cat);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Clan_Fight(sbyte action, short idClan, sbyte typeFight)
	{
		init(101);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeShort(idClan);
			m.writer().writeByte(typeFight);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Clan_Fight_List_Mem(sbyte action, mVector vec)
	{
		init(101);
		try
		{
			m.writer().writeByte(action);
			m.writer().writeByte(vec.size());
			for (int i = 0; i < vec.size(); i++)
			{
				InfoMemList infoMemList = (InfoMemList)vec.elementAt(i);
				m.writer().writeShort(infoMemList.id);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Daily_Login(sbyte action)
	{
		init(60);
		try
		{
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void send_GPS()
	{
		init(61);
		try
		{
			m.writer().writeUTF(GameMidlet.LONG);
			m.writer().writeUTF(GameMidlet.LAT);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void get_Info_Clan_Dao()
	{
		init(63);
		send();
	}

	public void Thanh_Toan(string token, string id)
	{
		init(99);
		try
		{
			m.writer().writeUTF(id);
			m.writer().writeUTF(token);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Upgrade_Super_Item(sbyte type, short idItem, sbyte bery_gem, sbyte num)
	{
		init(66);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(idItem);
			m.writer().writeByte(bery_gem);
			m.writer().writeByte(num);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Upgrade_Dial(sbyte type, short idItem, sbyte bery_gem, sbyte num)
	{
		mSystem.outz(" gui Upgrade dial " + type + " " + idItem + " " + bery_gem + " num = " + num);
		init(-94);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(idItem);
			m.writer().writeByte(bery_gem);
			m.writer().writeByte(num);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Upgrade_Skin(sbyte type, sbyte cat, short idItem, sbyte posItem, sbyte bovao)
	{
		mSystem.outz(" gui Upgrade skin " + type + " " + cat + " " + idItem + " posItem " + posItem + " " + bovao);
		init(81);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(cat);
			m.writer().writeShort(idItem);
			m.writer().writeByte(posItem);
			m.writer().writeByte(bovao);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Upgrade_Skin(sbyte type, short idItem)
	{
		mSystem.outz(" gui Upgrade skin  " + idItem);
		init(81);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(idItem);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Upgrade_Skin(sbyte type, mVector listItem)
	{
		mSystem.outz(" gui Upgrade skin size  " + listItem.size());
		init(81);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(listItem.size());
			for (sbyte b = 0; b < listItem.size(); b++)
			{
				MainItem mainItem = (MainItem)listItem.elementAt(b);
				m.writer().writeShort(mainItem.ID);
				m.writer().writeByte(mainItem.typeObject);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Upgrade_Skin(sbyte type, short idFashion, mVector listItem)
	{
		mSystem.outz(" gui Upgrade skin size  " + listItem.size() + " idFashion " + idFashion);
		init(81);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(idFashion);
			m.writer().writeByte(listItem.size());
			for (sbyte b = 0; b < listItem.size(); b++)
			{
				MainItem mainItem = (MainItem)listItem.elementAt(b);
				m.writer().writeShort(mainItem.ID);
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Use_Item_BOX_CHOICE(short idItem, sbyte cat, sbyte select)
	{
		init(69);
		try
		{
			m.writer().writeShort(idItem);
			m.writer().writeByte(cat);
			m.writer().writeByte(select);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Wanted_Poster(sbyte type, short id)
	{
		init(-89);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Wanted_Poster_Action(sbyte type, short id, sbyte action)
	{
		init(-89);
		mSystem.outz("Send wanted action type = " + type + " id " + id + " action " + action);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(id);
			m.writer().writeByte(action);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Nhan_NapTheScr(sbyte type, sbyte id)
	{
		init(-90);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Sudo(sbyte type)
	{
		init(-108);
		mSystem.outz("SUDO type = " + type);
		try
		{
			m.writer().writeByte(type);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Sudo_CMD(sbyte type, string strChat, int id, sbyte chucvu)
	{
		mSystem.outz("gời lên Sudo_CMD type=" + type + " id=" + id + "  name=" + strChat);
		init(-108);
		try
		{
			m.writer().writeByte(type);
			switch (type)
			{
			case 2:
			case 3:
			case 5:
			case 15:
			case 16:
				m.writer().writeUTF(strChat);
				m.writer().writeByte(chucvu);
				break;
			case 10:
			case 12:
			case 19:
				m.writer().writeInt(id);
				break;
			case 6:
				m.writer().writeByte(chucvu);
				m.writer().writeByte(id);
				break;
			case 18:
			case 20:
				m.writer().writeUTF(strChat);
				m.writer().writeInt(id);
				break;
			case 14:
				m.writer().writeShort(id);
				m.writer().writeByte(chucvu);
				break;
			}
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Nhan_TichLuyCongDon(sbyte type, short cost)
	{
		init(-97);
		mSystem.outz("TICH_LUY_CONG_DON type = " + type + " cost = " + cost);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeShort(cost);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Nhan_TichTieuScr(sbyte type, sbyte id)
	{
		init(-96);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Type_ID(sbyte cmd, sbyte type, sbyte id)
	{
		init(cmd);
		try
		{
			m.writer().writeByte(type);
			m.writer().writeByte(id);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Type(sbyte cmd, sbyte type)
	{
		mSystem.outz("send cmd = " + cmd + " type = " + type);
		init(cmd);
		try
		{
			m.writer().writeByte(type);
		}
		catch (Exception)
		{
		}
		send();
	}

	public void Send_Potion_Quick_Open(sbyte subcmd)
	{
		init(-107);
		try
		{
			m.writer().writeByte(subcmd);
		}
		catch (Exception)
		{
		}
		send();
	}
}
