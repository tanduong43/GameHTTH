using System;

public class LoadMapScreen : MainScreen
{
	public static bool isNextMap;

	public static bool isLoadDataMon;

	public static bool isPaintBack = true;

	public static bool isSuperBoss;

	private long time;

	public sbyte[] mItemMap;

	public static sbyte IDBack;

	public static sbyte typeChangeMap;

	public static short HBack;

	private int tick;

	private int tickPaintMap;

	public static sbyte isMapSky = -1;

	public static sbyte countLoadMap;

	public sbyte area = -1;

	public override void Show()
	{
		countLoadMap++;
		isNextMap = false;
		GameCanvas.menuCur.isShowMenu = false;
		isPaintBack = true;
		GameScreen.isShowNameSUPER_BOSS = true;
		GameScreen.isShowNameXpArena = false;
		GameScreen.isShowNameWW = false;
		GameScreen.checkRemoveImage(1, countLoadMap % 2 == 1);
		base.Show();
		if (GameCanvas.lowGraphic)
		{
			AvMain.imgLg = null;
		}
		time = GameCanvas.timeNow;
		if (Interface_Game.isPaintInfoFocus)
		{
			Interface_Game.isPaintInfoFocus = false;
		}
		SaveImageRMS.start();
		GameScreen.VecEffect.removeAllElements();
		GameScreen.VecNum.removeAllElements();
		GameScreen.vecObjMove.removeAllElements();
		GameScreen.isPvPNew = false;
		GameScreen.objPvPNew = null;
		Interface_Game.typeTitleRoomFight = 0;
		Interface_Game.vecClanDam.removeAllElements();
		tick = 0;
		tickPaintMap = 0;
		GameCanvas.mapBack = null;
		GameScreen.ClanDao = null;
		if (!GameCanvas.isLowGraOrWP_PvP() && GameMidlet.DEVICE != 0 && GameMidlet.DEVICE != 4)
		{
			if (GameCanvas.mapLogin == null)
			{
				GameCanvas.mapLogin = new MapBackGround();
			}
			GameCanvas.mapLogin.setBGLoading();
		}
		GameScreen.vecBigBossLittleGraden.removeAllElements();
		if (GameCanvas.loadmap.mapLang())
		{
			Interface_Game.mImgPvPType = null;
			MapOff_RedLine.mImgMapOffline = null;
			AvMain.imgTimePvpSmall = null;
			AvMain.imgTimePvp = null;
		}
		if (GameCanvas.loadmap.idMap >= 988 && GameCanvas.loadmap.idMap <= 995)
		{
			Interface_Game.indexPaintTable = 1;
		}
		else if (GameCanvas.loadmap.idMap == 999 || GameCanvas.loadmap.idMap == 997 || GameCanvas.loadmap.idMap == 1000 || GameCanvas.loadmap.idMap == 1001)
		{
			Interface_Game.indexPaintTable = 0;
		}
		GameScreen.checkRemoveImage(2, countLoadMap % 2 == 0);
		checkDelInstance();
		MapGotoSky.isBeginEffBoat = false;
		isSuperBoss = false;
	}

	private void checkDelInstance()
	{
		ListDungeon.instance = null;
		CreateChar_Screen.instance = null;
		PartyScreen.instance = null;
		PlayerListServer.instance = null;
		PvPScreen.instance = null;
		ScreenUpgrade.instance = null;
		SplitScreen.instance = null;
		TradeScreen.instance = null;
		ScreenJoinItem.instance = null;
	}

	public override void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public override void paint(mGraphics g)
	{
		try
		{
			GameCanvas.resetTrans(g);
			g.setColor(0);
			g.fillRect(0, 0, MotherCanvas.w, MotherCanvas.h);
			if (GameCanvas.mapLogin != null && isPaintBack && !GameCanvas.isLowGraOrWP_PvP() && GameMidlet.DEVICE != 0 && GameMidlet.DEVICE != 4)
			{
				GameCanvas.mapLogin.paintBgLoading(g);
				GameCanvas.mapLogin.paintObjLoading(g);
			}
			if (!GameCanvas.lowGraphic)
			{
				LoginScreen.paintLogo(g, MotherCanvas.hw);
			}
			if (!GameCanvas.lowGraphic)
			{
				AvMain.fraBtBanhlai.drawFrame(LoginScreen.frameBanhlai, MotherCanvas.w - 30, MotherCanvas.h - 30, 0, 3, g);
			}
			else
			{
				MsgDialog.fraImgWaiting.drawFrame(GameCanvas.gameTick % MsgDialog.fraImgWaiting.nFrame, MotherCanvas.w - MsgDialog.fraImgWaiting.frameWidth, MotherCanvas.h - MsgDialog.fraImgWaiting.frameHeight, 0, 3, g);
			}
		}
		catch (Exception)
		{
		}
	}

	public override void update()
	{
		try
		{
			LoginScreen.updateYPaintLogo(LoginScreen.hLogo);
			LoginScreen.updateBanhLai();
			tick++;
			if (GameCanvas.mapLogin != null && isPaintBack && !GameCanvas.isLowGraOrWP_PvP() && GameMidlet.DEVICE != 0 && GameMidlet.DEVICE != 4)
			{
				GameCanvas.mapLogin.updateCloudLogin();
				GameCanvas.mapLogin.updateBoat();
			}
			if (!isNextMap || !isLoadDataMon || (SaveImageRMS.vecSaveImage.size() > 20 && (GameCanvas.timeNow - time) / 1000 <= 15))
			{
				return;
			}
			GameScreen.checkRemoveImage(3, countLoadMap % 2 == 1);
			if (isPaintBack)
			{
				tickPaintMap++;
				if (tickPaintMap > 10)
				{
					isPaintBack = false;
					tickPaintMap = 0;
				}
				return;
			}
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject == null || mainObject.isRemove)
				{
					GameScreen.vecPlayers.removeElement(mainObject);
					i--;
				}
			}
			if (mItemMap != null)
			{
				GameCanvas.loadmap.load_ItemMap(mItemMap);
			}
			GameScreen.player.posTransRoad = null;
			GameScreen.player.skillCurrent = null;
			GameScreen.player.plashNow = null;
			GameScreen.player.Action = 0;
			GameScreen.player.resetAction();
			GameScreen.player.typeActionBoat = 0;
			GameScreen.player.toX = GameScreen.player.x;
			GameScreen.player.toY = GameScreen.player.y;
			GameScreen.checkRemoveImage(4, countLoadMap % 2 == 0);
			if (GameScreen.player.isPet)
			{
				MainObject.getPet(GameScreen.player.ID)?.setXYPet(GameScreen.player.x, GameScreen.player.y);
			}
			Interface_Game.HPMap = -1;
			Interface_Game.maxHPMap = -1;
			Interface_Game.watchMap.timeCountDown = 0;
			Interface_Game.watchMap.valueLeft = 0;
			Interface_Game.watchMap.valueright = 0;
			Interface_Game.watchRevice.timeCountDown = 0;
			Player.setStart_EndAutoFire(isAu: false);
			if (LoadMap.specMap == 3)
			{
				Player.indexPosMapTrain = CRes.random(MainObject.mPosMapTrain.Length);
				GameScreen.player.posTransRoad = GameCanvas.loadmap.updateFindRoad(MainObject.mPosMapTrain[Player.indexPosMapTrain][0], MainObject.mPosMapTrain[Player.indexPosMapTrain][1], GameScreen.player.x / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, 200, GameScreen.player);
			}
			if (LoadMap.specMap == 4)
			{
				goToMapSea();
			}
			else if (GameScreen.typeViewPlayer == 0 && LoadMap.isOnlineMap && checkMapDontshow())
			{
				if (typeChangeMap == 2)
				{
					if (GameScreen.player.x < 100 || GameScreen.player.x > GameCanvas.loadmap.maxWMap - 100)
					{
						int num = GameScreen.player.x / 24 * 24;
						if (GameScreen.player.x < 100)
						{
							GameScreen.player.x = 0;
						}
						else
						{
							GameScreen.player.x = GameCanvas.loadmap.maxWMap - LoadMap.wTile;
						}
						GameScreen.player.toX = GameScreen.player.x;
						GameScreen.player.toY = GameScreen.player.y;
						GameScreen.player.posTransRoad = GameCanvas.loadmap.updateFindRoad(num / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, GameScreen.player.x / LoadMap.wTile, GameScreen.player.y / LoadMap.wTile, 20, GameScreen.player);
						GameScreen.player.countAutoMove = 1;
						Player.isSendMove = false;
						if (GameScreen.player.posTransRoad == null || GameScreen.player.posTransRoad.Length > 20)
						{
							GameScreen.player.x = num;
							Player.isSendMove = true;
						}
					}
				}
				else if (typeChangeMap == 1)
				{
					GameScreen.addEffectEnd_ObjTo(32, 0, GameScreen.player.x, GameScreen.player.y, GameScreen.player.ID, GameScreen.player.typeObject, (sbyte)GameScreen.player.type_left_right, GameScreen.player);
				}
			}
			Player.isSendMove = false;
			if (GameCanvas.mapBack == null)
			{
				GameCanvas.mapBack = new MapBackGround();
			}
			GameCanvas.mapBack.setBackGround(IDBack, HBack);
			if (GameScreen.effMap != null && GameScreen.effMap.type == 1 && GameCanvas.mapBack != null)
			{
				GameCanvas.mapBack.setSkySnow();
			}
			if (LoadMap.specMap == 8)
			{
				MapGotoSky.setTypeMoveredLine(0);
				MapGotoSky.SetPosNextMove();
				Player.boatKeyParty = null;
			}
			if (LoadMap.specMap == 12)
			{
				MapGotoGod.setTypeMoveredLine(0);
				MapGotoGod.SetPosNextMove();
				Player.boatKeyParty = null;
			}
			LoadMap.Area = area;
			GameCanvas.gameScr.Show();
			Interface_Game.timeShowNameMap = 0;
			GameCanvas.hLoad = MotherCanvas.h / 4 * 3;
			Interface_Game.wtable = mFont.tahoma_7b_white.getWidth(LoadMap.getNameMap(GameCanvas.loadmap.idMap)) + 20;
			if (Interface_Game.wtable < 100)
			{
				Interface_Game.wtable = 100;
			}
			setMapSea();
			GlobalService.gI().changeMapOk();
			if (GameScreen.vecHelp == null)
			{
				GameScreen.vecHelp = new mVector();
				if (GameScreen.indexHelp >= 0)
				{
					MainHelp.setNextHelp(isHanhTrang: false);
				}
			}
			if (GameCanvas.loadmap.idMap == 1 && GameScreen.indexHelp == 14)
			{
				GameScreen.addHelp(14, 0);
			}
			PlayMusicLang();
			LoadMap.mStrNameMapNPC = mFont.tahoma_7_white.splitFontArray(LoadMap.getNameMap(GameCanvas.loadmap.idMap), 70);
			GlobalService.gI().Update_Num_Player_Map();
			if (GameCanvas.loadmap.idMap == 130)
			{
				GameScreen.player.Dir = 0;
				GameScreen.player.type_left_right = 0;
			}
			else if (GameCanvas.loadmap.idMap == 129)
			{
				GameScreen.player.Dir = 2;
				GameScreen.player.type_left_right = 2;
			}
			if (Player.StepAutoRe == 3)
			{
				Player.StepAutoRe = 0;
			}
			if (GameCanvas.loadmap.mapLang())
			{
				Player.xBeginAuto = 0;
				Player.yBeginAuto = 0;
			}
			CheckHuongDanVaoLang();
			if (GameScreen.isOnRepeatQuest)
			{
				AutoRepeatQuest.stopAuto();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.offAutoRepeatQuestLoadMap);
			}
			if (GameScreen.isOnSuperBoss)
			{
				AutoSuperBoss.stopAuto();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.offAutoSuperBossLoadMap);
			}
			if (GameScreen.isOnAutoPB && !AutoBattlefield.checkMapPb())
			{
				AutoBattlefield.StopAutoBattlefield();
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.noAutoPB);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CheckHuongDanVaoLang()
	{
		if (GameScreen.player.Lv < 50 && GameScreen.indexHDVaoLang < T.mHuongdanVaoLang.Length)
		{
			bool flag = false;
			if (GameCanvas.loadmap.idMap == 9 && GameScreen.player.Lv < 20 && GameScreen.indexHDVaoLang <= 0)
			{
				flag = true;
				GameScreen.indexHDVaoLang = 1;
			}
			else if (GameCanvas.loadmap.idMap == 17 && GameScreen.indexHDVaoLang <= 1 && GameScreen.player.Lv < 30)
			{
				flag = true;
				GameScreen.indexHDVaoLang = 2;
			}
			else if (GameCanvas.loadmap.idMap == 25 && GameScreen.indexHDVaoLang <= 2 && GameScreen.player.Lv < 40)
			{
				flag = true;
				GameScreen.indexHDVaoLang = 3;
			}
			else if (GameCanvas.loadmap.idMap == 33 && GameScreen.indexHDVaoLang <= 3)
			{
				flag = true;
				GameScreen.indexHDVaoLang = 4;
			}
			else if (GameCanvas.loadmap.idMap == 41 && GameScreen.indexHDVaoLang <= 4)
			{
				flag = true;
				GameScreen.indexHDVaoLang = 5;
			}
			else if (GameCanvas.loadmap.idMap == 49 && GameScreen.indexHDVaoLang <= 5)
			{
				flag = true;
				GameScreen.indexHDVaoLang = 6;
			}
			if (flag && GameScreen.indexHDVaoLang < T.mHuongdanVaoLang.Length)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.mHuongdanVaoLang[GameScreen.indexHDVaoLang - 1]);
				GlobalService.gI().Save_RMS_Server(1, 8, new sbyte[1] { (sbyte)GameScreen.indexHDVaoLang });
			}
		}
	}

	private bool checkMapDontshow()
	{
		if (GameCanvas.loadmap.idMap == 157 || GameCanvas.loadmap.idMap == 159 || GameCanvas.loadmap.idMap == 161)
		{
			return false;
		}
		return true;
	}

	private void goToMapSea()
	{
		Skill_Info skill_Info = null;
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			Skill_Info skill_Info2 = (Skill_Info)Player.vecListSkill.elementAt(i);
			if (skill_Info2.typeSkill == 4)
			{
				skill_Info = skill_Info2;
				break;
			}
		}
		if (skill_Info != null)
		{
			MainSkill mainSkill = new MainSkill(skill_Info.ID, skill_Info.typeEffSkill);
			mainSkill.indexHotKey = skill_Info.indexHotKey;
			mainSkill.idIcon = skill_Info.idIcon;
			mainSkill.isBuff = false;
			bool flag = true;
			for (int j = 0; j < Player.hotkeyPlayer.Length; j++)
			{
				flag = true;
				for (int k = 0; k < Player.hotkeyPlayer[j].Length; k++)
				{
					int num = k;
					switch (num)
					{
					case 0:
						num = 2;
						break;
					case 2:
						num = 0;
						break;
					}
					Hotkey hotkey = Player.hotkeyPlayer[j][num];
					if (hotkey.skill != null && hotkey.skill.typeBuff == 0)
					{
						if (flag)
						{
							Player.hotkeyPlayer[j][num].setSkill(mainSkill, mainSkill.idIcon);
							flag = false;
						}
						else
						{
							Player.hotkeyPlayer[j][num].skill = null;
						}
					}
				}
			}
			return;
		}
		for (int l = 0; l < Player.hotkeyPlayer.Length; l++)
		{
			for (int m = 0; m < Player.hotkeyPlayer[l].Length; m++)
			{
				if (Player.hotkeyPlayer[l][m].skill != null)
				{
					Player.hotkeyPlayer[l][m].skill = null;
				}
			}
		}
	}

	private void setMapSea()
	{
		if (LoadMap.mSea == null || GameScreen.typeViewPlayer != 0)
		{
			return;
		}
		for (int i = 0; i < LoadMap.mSea.Length; i++)
		{
			if (LoadMap.mSea[i][0] == GameCanvas.loadmap.idMap && LoadMap.mSea[i][1] == GameCanvas.loadmap.idLastMap)
			{
				GameScreen.player.setActionSea((sbyte)LoadMap.mSea[i][2], LoadMap.mSea[i][3], LoadMap.mSea[i][4]);
				break;
			}
		}
	}

	public static void PlayMusicLang()
	{
		try
		{
			int num = -1;
			switch (GameCanvas.loadmap.idMap)
			{
			case 1:
			case 9:
			case 17:
			case 25:
			case 79:
			case 107:
				num = 2;
				break;
			case 33:
			case 41:
			case 49:
			case 66:
			case 83:
			case 113:
				num = 0;
				break;
			case 69:
				num = 1;
				break;
			case 2:
			case 10:
			case 11:
			case 12:
			case 13:
			case 18:
			case 19:
			case 20:
			case 21:
			case 26:
			case 27:
			case 28:
			case 29:
			case 42:
			case 43:
			case 44:
			case 45:
			case 50:
			case 51:
			case 52:
			case 53:
				num = 4;
				break;
			case 0:
			case 3:
			case 4:
			case 5:
			case 6:
			case 8:
			case 14:
			case 16:
			case 22:
			case 24:
			case 30:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 40:
			case 46:
			case 48:
			case 54:
			case 70:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 80:
			case 81:
				num = 5;
				break;
			case 7:
			case 15:
			case 23:
			case 31:
			case 39:
			case 47:
			case 63:
			case 64:
			case 68:
			case 78:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
				num = 6;
				break;
			default:
				num = 4;
				break;
			}
			mSound.stopSoundAll();
			mSound.playMus(num, mSound.volumeMusic, loop: true);
		}
		catch (Exception)
		{
			mSystem.outloi("load bat am thanh");
		}
	}
}
