public class GlobalMessageHandler : Cmd_Message, IMessageHandler
{
	public GlobalLogicHandler globalLogicHandler = new GlobalLogicHandler();

	public static GlobalMessageHandler me;

	public IMiniGameMsgHandler miniGameMessageHandler;

	public static GlobalMessageHandler gI()
	{
		if (me == null)
		{
			me = new GlobalMessageHandler();
		}
		return me;
	}

	public void setGameLogicHandler(GlobalLogicHandler gI)
	{
		globalLogicHandler = gI;
	}

	public void onConnectOK()
	{
		globalLogicHandler.onConnectOK();
	}

	public void onConnectionFail()
	{
		globalLogicHandler.onConnectFail();
	}

	public void onDisconnected()
	{
		GlobalLogicHandler.onDisconnect();
	}

	public void onMessage(Message msg)
	{
		switch (msg.command)
		{
		case -4:
			GameCanvas.readMess.ListChar(msg);
			break;
		case 0:
			GameCanvas.readMess.ChangeMap(msg);
			break;
		case 1:
			GameCanvas.readMess.ObjectMove(msg);
			break;
		case -101:
		case -51:
			GameCanvas.readMess.loadImage(msg);
			break;
		case -93:
		case -92:
			GameCanvas.readMess.LoadImageNew(msg);
			break;
		case -5:
			GameCanvas.readMess.char_info(msg);
			break;
		case 3:
			GameCanvas.readMess.remove_Char(msg);
			break;
		case -7:
			GameCanvas.readMess.getData(msg);
			break;
		case 2:
			GameCanvas.readMess.Fire_Object(msg);
			break;
		case 4:
			GameCanvas.readMess.monsterInfo(msg);
			break;
		case 5:
			GameCanvas.readMess.MonsterNoneFocus(msg);
			break;
		case 6:
			GameCanvas.readMess.Revice_Player(msg);
			break;
		case 9:
			GameCanvas.readMess.Get_Xp_Map_Train(msg);
			break;
		case 10:
			GameCanvas.readMess.Set_XP(msg);
			break;
		case -10:
			GameCanvas.readMess.Main_char_Info(msg);
			break;
		case -11:
			GameCanvas.readMess.Dialog_More_server(msg);
			break;
		case 11:
			GameCanvas.readMess.Item_Drop(msg);
			break;
		case 12:
			GameCanvas.readMess.GetItemMap(msg);
			break;
		case 13:
			GameCanvas.readMess.remove_Obj(msg);
			break;
		case -12:
			GameCanvas.readMess.update_Inventory(msg);
			break;
		case -13:
			GameCanvas.readMess.use_Potion(msg);
			break;
		case -83:
			GameCanvas.readMess.update_MP_HP(msg);
			break;
		case 14:
			GameCanvas.readMess.update_PK(msg);
			break;
		case 15:
			GameCanvas.readMess.update_List_Pk(msg);
			break;
		case -15:
			GameCanvas.readMess.Effect_Obj(msg);
			break;
		case 16:
			GameCanvas.readMess.List_NPC(msg);
			break;
		case -19:
			GameCanvas.readMess.Shop_NPC(msg);
			break;
		case -95:
			GameCanvas.readMess.Shop_IconClanVip(msg);
			break;
		case -20:
			GameCanvas.readMess.Dynamic_Menu(msg);
			break;
		case -23:
			GameCanvas.readMess.List_Quest(msg);
			break;
		case -24:
			GameCanvas.readMess.get_Info_NPC(msg);
			break;
		case -25:
			GameCanvas.readMess.Party(msg);
			break;
		case 17:
			GameCanvas.readMess.ChatPopup(msg);
			break;
		case 18:
			GameCanvas.readMess.ChatTab(msg);
			break;
		case 19:
			GameCanvas.readMess.charWearing(msg);
			break;
		case -33:
			GameCanvas.readMess.Save_RMS_Server(msg);
			break;
		case -28:
			GameCanvas.readMess.Learn_Skill(msg);
			break;
		case -29:
			GameCanvas.readMess.Friend(msg);
			break;
		case -30:
			GameCanvas.readMess.ListPlayerServer(msg);
			break;
		case -31:
			GameCanvas.readMess.Notify_Server(msg);
			break;
		case -32:
			GameCanvas.readMess.update_Chest(msg);
			break;
		case -6:
			GameCanvas.readMess.Check_Data_Ver(msg);
			break;
		case -34:
			GameCanvas.readMess.Open_Box(msg);
			break;
		case -35:
			GameCanvas.readMess.Fight(msg);
			break;
		case 20:
			GameCanvas.readMess.Buff(msg);
			break;
		case 21:
			GameCanvas.readMess.ChangeArea(msg);
			break;
		case 7:
			GameCanvas.readMess.diePlayer(msg);
			break;
		case 22:
			GameCanvas.readMess.Skill_Map_Train(msg);
			break;
		case 23:
			GameCanvas.readMess.AreaStatus(msg);
			break;
		case -26:
			GameCanvas.readMess.Register(msg);
			break;
		case -37:
			GameCanvas.readMess.Del_Char(msg);
			break;
		case 24:
			GameCanvas.readMess.MonsterDie(msg);
			break;
		case 25:
			GameCanvas.readMess.NumItemQuest(msg);
			break;
		case 26:
			GameCanvas.readMess.Teleport_Boss(msg);
			break;
		case 27:
			GameCanvas.readMess.SkillSpec(msg);
			break;
		case 28:
			GameCanvas.readMess.addHP_EffSkill(msg);
			break;
		case -39:
			GameCanvas.readMess.SaveImageAndroid(msg);
			break;
		case -40:
			GameCanvas.readMess.LoadImageAndroidOk(msg);
			break;
		case -41:
			GameCanvas.readMess.resetValueUpdateImage(msg);
			break;
		case 29:
			GameCanvas.readMess.comboSkill(msg);
			break;
		case -42:
			GameCanvas.readMess.ShowInfoPlayer(msg);
			break;
		case -44:
			GameCanvas.readMess.loadDataEff(msg);
			break;
		case -45:
			GameCanvas.readMess.update_Pk_Point(msg);
			break;
		case -47:
			GameCanvas.readMess.setWeather(msg);
			break;
		case -48:
			GameCanvas.readMess.Upgrade_Item(msg);
			break;
		case -50:
			GameCanvas.readMess.Split_Join_Item(msg);
			break;
		case -49:
			GameCanvas.readMess.Trade(msg);
			break;
		case -53:
			GameCanvas.readMess.Ship(msg);
			break;
		case -54:
			GameCanvas.readMess.Help_From_Server(msg);
			break;
		case -55:
			GameCanvas.readMess.ghost(msg);
			break;
		case -56:
			GameCanvas.readMess.Boat_In_Map(msg);
			break;
		case 30:
			GameCanvas.readMess.Ok_Change_Map_Link(msg);
			break;
		case -100:
			GameCanvas.readMess.CMD_test(msg);
			break;
		case -57:
			GameCanvas.readMess.Frist_Login(msg);
			break;
		case -58:
			GameCanvas.readMess.Input_server(msg);
			break;
		case -59:
			GameCanvas.readMess.Update_User_Ok(msg);
			break;
		case -60:
			GameCanvas.readMess.Buy_Item_Shop(msg);
			break;
		case -61:
			GameCanvas.readMess.CountDown_Ticket(msg);
			break;
		case -62:
			GameCanvas.readMess.update_Part_Boat(msg);
			break;
		case -2:
			GameCanvas.readMess.login_Ok(msg);
			break;
		case -63:
			GameCanvas.readMess.PvP(msg);
			break;
		case 31:
			GameCanvas.readMess.UpdateNameNPC(msg);
			break;
		case -64:
			GameCanvas.readMess.Buy_Potion_Ok(msg);
			break;
		case 32:
			GameCanvas.readMess.Party_Buff(msg);
			break;
		case -66:
			GameCanvas.readMess.UpdatePvpPoint(msg);
			break;
		case -67:
			GameCanvas.readMess.RebuildItem(msg);
			break;
		case -69:
			GameCanvas.readMess.Diaolog_time(msg);
			break;
		case -70:
			GameCanvas.readMess.updateNumPlayerMap(msg);
			break;
		case -71:
			GameCanvas.readMess.Auto_Revice(msg);
			break;
		case -72:
			GameCanvas.readMess.Red_Line(msg);
			break;
		case -73:
			GameCanvas.readMess.TimeShow(msg);
			break;
		case -74:
			GameCanvas.readMess.List_Player_Check(msg);
			break;
		case -75:
			GameCanvas.readMess.UpdateInfoMaincharInfo(msg);
			break;
		case -52:
			GameCanvas.readMess.Clan(msg);
			break;
		case -77:
			GameCanvas.readMess.ChuyenHoa(msg);
			break;
		case -78:
			GameCanvas.readMess.readAttribute(msg);
			break;
		case -79:
			GameCanvas.readMess.little_Garden(msg);
			break;
		case -80:
			GameCanvas.readMess.Pet(msg);
			break;
		case 33:
			GameCanvas.readMess.GetItemMapLittle(msg);
			break;
		case -81:
			GameCanvas.readMess.Input_password(msg);
			break;
		case -103:
			GameCanvas.readMess.registerNew(msg);
			break;
		case 35:
			GameCanvas.readMess.ChangeMapNonData(msg);
			break;
		case 36:
			GameCanvas.readMess.PvP_Thong_Bao(msg);
			break;
		case 37:
			GameCanvas.readMess.Archi_Daily(msg);
			break;
		case 38:
			GameCanvas.readMess.Table_Match(msg);
			break;
		case -82:
			GameCanvas.readMess.ReadPartNew(msg);
			break;
		case 39:
			GameCanvas.readMess.updateXP_Arena(msg);
			break;
		case 40:
			GameCanvas.readMess.NewDialog(msg);
			break;
		case 41:
			GameCanvas.readMess.TypeNpcEvent(msg);
			break;
		case 42:
			GameCanvas.readMess.TimeItemDrop(msg);
			break;
		case -104:
			GameCanvas.readMess.Weapon_fashion(msg);
			break;
		case 44:
			GameCanvas.readMess.Market(msg);
			break;
		case 45:
			GameCanvas.readMess.upgradeDevil(msg);
			break;
		case 47:
			GameCanvas.readMess.cmdEvent(msg);
			break;
		case 48:
			GameCanvas.readMess.getTemplate(msg);
			break;
		case -84:
			GameCanvas.readMess.disconnect_Why(msg);
			break;
		case 49:
			GameCanvas.readMess.Max_Level(msg);
			break;
		case -85:
			GameCanvas.readMess.Wanted(msg);
			break;
		case -86:
			GameCanvas.readMess.ChestWanted(msg);
			break;
		case 50:
			GameCanvas.readMess.OpenMessenger(msg);
			break;
		case 51:
			GameCanvas.readMess.UpdateLoL(msg);
			break;
		case -88:
			GameCanvas.readMess.DonotAutoReconnect(msg);
			break;
		case 52:
			GameCanvas.readMess.QuickChat(msg);
			break;
		case -105:
			GameCanvas.readMess.getDataInfoPotion(msg);
			break;
		case 53:
			GameCanvas.readMess.updatePoint_WorldWar(msg);
			break;
		case 100:
			GameCanvas.readMess.Fire_Object_NEW(msg);
			break;
		case 55:
			GameCanvas.readMess.update_MP_HP_Eff(msg);
			break;
		case 57:
			GameCanvas.readMess.Eff_Kich_An(msg);
			break;
		case 58:
			GameCanvas.readMess.Count_Kick_Ava(msg);
			break;
		case 59:
			GameCanvas.readMess.GiaoTiep_FormServer(msg);
			break;
		case 54:
			GameCanvas.readMess.Quay_So(msg);
			break;
		case 82:
			GameCanvas.readMess.Quay_WC(msg);
			break;
		case 79:
			GameCanvas.readMess.Hanh_Trinh(msg);
			break;
		case 80:
			GameCanvas.readMess.Event(msg);
			break;
		case 77:
			GameCanvas.readMess.Quay_oc_sen(msg);
			break;
		case 101:
			GameCanvas.readMess.Clan_Fight(msg);
			break;
		case 60:
			GameCanvas.readMess.daily_Bonus(msg);
			break;
		case 61:
			GameCanvas.readMess.gps(msg);
			break;
		case 62:
			GameCanvas.readMess.getDamList(msg);
			break;
		case 63:
			GameCanvas.readMess.getInfoClanDao(msg);
			break;
		case 64:
			GameCanvas.readMess.listGiftArea(msg);
			break;
		case 65:
			GameCanvas.readMess.getThanhTich(msg);
			break;
		case 99:
			GameCanvas.readMess.Thanh_Toan(msg);
			break;
		case 66:
			GameCanvas.readMess.UpgradeSuper_Item(msg);
			break;
		case -94:
			GameCanvas.readMess.Upgrade_Dial(msg);
			break;
		case 81:
			GameCanvas.readMess.Upgrade_Skin(msg);
			break;
		case 67:
			GameCanvas.readMess.title_Map_Fight(msg);
			break;
		case 69:
			GameCanvas.readMess.Potion_Choice(msg);
			break;
		case 70:
			GameCanvas.readMess.Set_XP_Skill(msg);
			break;
		case 75:
			GameCanvas.readMess.ReadEventTrangTri(msg);
			break;
		case 71:
			GameCanvas.readMess.ReadTrongCay(msg);
			break;
		case -89:
			GameCanvas.readMess.ListWantedServer(msg);
			break;
		case -90:
			GameCanvas.readMess.ListTichNapThe(msg);
			break;
		case -96:
			GameCanvas.readMess.ListTichTieu(msg);
			break;
		case -97:
			GameCanvas.readMess.ListTichCongDon(msg);
			break;
		case -108:
			GameCanvas.readMess.Read_Sudo(msg);
			break;
		case -91:
			GameCanvas.readMess.ListDauGia(msg);
			break;
		case 72:
			GameCanvas.readMess.infoFashion(msg);
			break;
		case 73:
			GameCanvas.readMess.vongsinhtu(msg);
			break;
		case 74:
		case 76:
			GameCanvas.readMess.getDataEff(msg);
			break;
		default:
			mSystem.outz(">>>>>>>>client k co cmd=" + msg.command);
			break;
		}
	}
}
