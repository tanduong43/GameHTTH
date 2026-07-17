using System;

public class SplitScreen : ScreenUpgrade
{
	public new static SplitScreen instance;

	public short idItem9x = -1;

	public MainItem potionBua;

	private bool isPaintrectPlus;

	private InputDialog input;

	public const sbyte RE_SPLIT = 0;

	public const sbyte RE_KHAM = 1;

	public const sbyte RE_DUT = 2;

	public const sbyte RE_LAY = 3;

	public const sbyte RE_NANGCAPDA = 4;

	public const sbyte RE_NANGCAPITEM = 5;

	public const sbyte RE_TRADE = 6;

	public const sbyte RE_CHUYEN_HOA = 7;

	public const sbyte RE_DEVIL_CHEST = 8;

	public const sbyte RE_DEVIL_SKILL = 9;

	public const sbyte RE_HOAN_MY = 10;

	public const sbyte RE_KICH_AN = 11;

	public const sbyte RE_CONG_CHE_TAC = 12;

	public const sbyte RE_DA_SIEU_CAP = 13;

	public const sbyte RE_DO_9X = 14;

	public const sbyte RE_SIEU_NANGCAPITEM = 15;

	public const sbyte RE_CUONG_HOA_DIAL = 18;

	public const sbyte RE_DUC_LO_DIAL = 19;

	public const sbyte RE_GHEP_NHIEU_NGLIEU_THANH_1 = 20;

	public const sbyte RE_HANH_TRINH = 21;

	public const sbyte RE_CUONG_HOA_SKIN = 22;

	public int PriceChuyenHoa;

	public sbyte tile;

	public mVector vecDaKhamHanhTrinh = new mVector();

	public short idMinimap = -1;

	public string nameMinimap = string.Empty;

	public SplitScreen(sbyte typeRebuild, int size)
		: base(typeRebuild, size)
	{
	}

	public override void setStart(sbyte typeRebuild, int size)
	{
		base.typeRebuild = typeRebuild;
		int num = x + wInven + (w - lech * 3 - wInven) / 6 + 4;
		int num2 = y + h / 2 - wItem / 2;
		potionBua = null;
		isPaintrectPlus = false;
		if (typeRebuild == 21)
		{
			posUp = mSystem.new_M_Int(1, 2);
			posUp[0][0] = num + wItem * 3 / 2;
			posUp[0][1] = num2 + wItem * 2 / 3;
		}
		else
		{
			posUp = mSystem.new_M_Int(2, 2);
			posUp[0][0] = num;
			posUp[0][1] = num2;
			posUp[1][0] = num + (w - lech * 3 - wInven) / 6 * 4;
			posUp[1][1] = num2;
			if (typeRebuild == 19)
			{
				posUp[0][1] = num2 + wItem - 5;
			}
		}
		maxNumItemW = wInven / wItem;
		int limX = ((Player.maxInventory - 1) / maxNumItemW + 1) * wItem - hInven;
		list = new ListNew(xInven, yInven, wInven, hInven, 0, 0, limX, isLim0: true);
		cmdBovao = new iCommand(T.bovao, 0, this);
		cmdUpgrade = new iCommand(T.split, 1, this);
		cmdMenu = new iCommand(T.menu, 10, this);
		nameScreen = T.SplitItem;
		if (typeRebuild == 1)
		{
			typeShowItem = 2;
			cmdUpgrade = new iCommand(T.kham, 2, this);
			nameScreen = T.tabkhamda;
		}
		else if (typeRebuild == 21)
		{
			typeShowItem = 1;
			cmdUpgrade = new iCommand(T.kham, 22, this);
			nameScreen = T.tabkhamda;
		}
		else if (typeRebuild == 2)
		{
			typeShowItem = 4;
			cmdUpgrade = new iCommand(T.duclo, 3, this);
			nameScreen = T.tabdutlo;
		}
		else if (typeRebuild == 3)
		{
			typeShowItem = 2;
			cmdUpgrade = new iCommand(T.laydara, 4, this);
			nameScreen = T.tablayda;
		}
		else if (typeRebuild == 4)
		{
			typeShowItem = 1;
			cmdUpgrade = new iCommand(T.cmdghepda, 5, this);
			cmdBoVaoAll = new iCommand(T.allMaterial, 8, this);
			nameScreen = T.tabupgradeDa;
		}
		else if (typeRebuild == 7)
		{
			typeShowItem = 3;
			cmdUpgrade = new iCommand(T.cmdChuyenHoa, 7, this);
			nameScreen = T.tabChuyenHoa;
		}
		else if (typeRebuild == 8)
		{
			cmdUpgrade = new iCommand(T.Upgrade, 12, this);
			nameScreen = T.tabDevilUpgrade;
		}
		else if (typeRebuild == 10)
		{
			typeShowItem = 2;
			cmdUpgrade = new iCommand(T.hoanmy, 13, this);
			nameScreen = T.tabHoanMy;
		}
		else if (typeRebuild == 11)
		{
			typeShowItem = 2;
			cmdUpgrade = new iCommand(T.kichAn, 14, this);
			nameScreen = T.taKichAn;
		}
		else if (typeRebuild == 12)
		{
			typeShowItem = 3;
			cmdUpgrade = new iCommand(T.dapCheTac, 15, this);
			nameScreen = T.tabCongCheTac;
			posUp = mSystem.new_M_Int(1, 2);
			posUp[0][0] = x + wInven + (w - lech * 3 - wInven) / 2;
			posUp[0][1] = num2;
		}
		else if (typeRebuild == 13)
		{
			typeShowItem = 1;
			cmdUpgrade = new iCommand(T.dapCheTac, 18, this);
			nameScreen = T.taDaSieuCap;
		}
		else if (typeRebuild == 14)
		{
			cmdUpgrade = new iCommand(T.ghepDo, 19, this);
			nameScreen = T.tabGhepDo;
		}
		else if (typeRebuild == 15)
		{
			typeShowItem = 5;
			cmdUpgrade = new iCommand(T.duclo, 20, this);
			nameScreen = T.tabdutlo;
		}
		else if (typeRebuild == 19)
		{
			typeShowItem = 5;
			cmdUpgrade = new iCommand(T.duclo, 3, this);
			nameScreen = T.tabdutlo;
			isPaintrectPlus = true;
		}
		AvMain.setPosCMD(cmdMenu, 2);
		if (typeRebuild != 19 && typeRebuild != 21)
		{
			vecCmd.addElement(cmdMenu);
		}
		if (GameCanvas.isTouch)
		{
			cmdUpgrade.setPos(x + wInven + (w - lech * 3 - wInven) / 2 + wItem * 3 / 4, y + h - iCommand.hButtonCmdNor / 2 - 10, null, cmdUpgrade.caption);
			vecCmd.addElement(cmdUpgrade);
		}
		ScreenUpgrade.mItemUpgrade = new MainItem[posUp.Length];
		if (!GameCanvas.isTouch)
		{
			AvMain.setPosCMD(cmdUpgrade, 1);
			cmdClose.caption = T.close + " " + T.hientai;
			if (typeRebuild != 19 && typeRebuild != 21)
			{
				right = cmdMenu;
			}
			left = cmdUpgrade;
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			backCMD = cmdClose;
			okCMD = cmdUpgrade;
		}
		if (IdSelect >= 0 && typeRebuild != 21)
		{
			getItemCurNew();
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (itemCur == null)
			{
				break;
			}
			if (typeRebuild == 21)
			{
				ScreenUpgrade.mItemUpgrade[0] = itemCur;
			}
			else if (typeRebuild == 0)
			{
				GlobalService.gI().Split_Item(0, 0, itemCur.ID, itemCur.typeObject, 1);
			}
			else if (typeRebuild == 4)
			{
				if (cmdBoVaoAll == null)
				{
					cmdBoVaoAll = new iCommand(T.allMaterial, 8, this);
				}
				mVector mVector2 = new mVector();
				mVector2.addElement(cmdBoVaoAll);
				mVector2.addElement(new iCommand(T.soluong, 9, this));
				GameCanvas.Start_Normal_DiaLog(T.nhapsoluong, mVector2, isCmdClose: true);
			}
			else if (typeRebuild == 7)
			{
				GlobalService.gI().ChuyenHoa(1, itemCur.ID, 0);
			}
			else if (typeRebuild == 8)
			{
				GlobalService.gI().Devil_Upgrade(14, itemCur.ID, itemCur.typeObject, 1);
			}
			else if (typeRebuild == 13)
			{
				mVector mVector3 = new mVector();
				mVector3.addElement(new iCommand(T.daSieuCap, 17, 28, this));
				mVector3.addElement(new iCommand(T.daNguyenLieu, 17, 29, this));
				GameCanvas.menuCur.startAt(mVector3, 2, T.taDaSieuCap);
			}
			else if (typeRebuild == 2)
			{
				if (itemCur.typeObject == 3)
				{
					GlobalService.gI().rebuild_Item(typeRebuild, 1, itemCur.ID, itemCur.typeObject, (short)subIndex);
				}
				else if (itemCur.typeObject == 4)
				{
					if (potionBua != null && itemCur.ID == potionBua.ID)
					{
						potionBua = null;
					}
					else if (itemCur.ID == 323 || itemCur.ID == 339)
					{
						potionBua = itemCur;
					}
				}
			}
			else if (typeRebuild == 19)
			{
				if (itemCur.typeObject == 3)
				{
					GlobalService.gI().rebuild_Item(typeRebuild, 1, itemCur.ID, itemCur.typeObject, (short)subIndex);
				}
				else if (itemCur.typeObject == 4)
				{
					if (potionBua != null && itemCur.ID == potionBua.ID)
					{
						potionBua = null;
					}
					else if (itemCur.ID == 457)
					{
						potionBua = itemCur;
					}
				}
			}
			else if (typeRebuild == 15)
			{
				if (itemCur.typeObject == 3)
				{
					GlobalService.gI().rebuild_Item(typeRebuild, 1, itemCur.ID, itemCur.typeObject, (short)subIndex);
				}
				else if (itemCur.typeObject == -8)
				{
					if (potionBua != null && itemCur.ID == potionBua.ID)
					{
						potionBua = null;
					}
					else
					{
						potionBua = itemCur;
					}
				}
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 1, itemCur.ID, itemCur.typeObject, (short)subIndex);
				if ((typeRebuild == 2 || typeRebuild == 3 || typeRebuild == 4 || typeRebuild == 15) && ScreenUpgrade.mItemUpgrade[1] != null)
				{
					ScreenUpgrade.mItemUpgrade[1] = null;
				}
			}
			break;
		case 1:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItemUpgrade);
			}
			else
			{
				GlobalService.gI().Split_Item(0, 1, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1);
			}
			break;
		case 2:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
			}
			else if (ScreenUpgrade.mItemUpgrade[1] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nulldakham);
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 4, 0, 0, 0);
			}
			break;
		case 3:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
				break;
			}
			if (ScreenUpgrade.mItemUpgrade[1] != null)
			{
				ScreenUpgrade.mItemUpgrade[1] = null;
			}
			if (potionBua != null)
			{
				if (potionBua.ID == 339)
				{
					GlobalService.gI().rebuild_Item(typeRebuild, 33, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1);
				}
				else if (potionBua.ID == 323)
				{
					GlobalService.gI().rebuild_Item(typeRebuild, 34, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1);
				}
				else if (typeRebuild == 19)
				{
					mSystem.outz(" potionBua id = " + potionBua.ID);
					GlobalService.gI().rebuild_Item(typeRebuild, 7, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1, potionBua.ID);
				}
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 7, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1);
			}
			break;
		case 4:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
				break;
			}
			if (ScreenUpgrade.mItemUpgrade[1] != null)
			{
				ScreenUpgrade.mItemUpgrade[1] = null;
			}
			GlobalService.gI().rebuild_Item(typeRebuild, 6, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1);
			break;
		case 5:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
				break;
			}
			if (ScreenUpgrade.mItemUpgrade[1] != null)
			{
				ScreenUpgrade.mItemUpgrade[1] = null;
			}
			GlobalService.gI().rebuild_Item(typeRebuild, 5, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, ScreenUpgrade.mItemUpgrade[0].numPotion);
			break;
		case 6:
		{
			int num = 1;
			try
			{
				num = int.Parse(input.tfInput.getText());
				if (num < 0)
				{
					num = 1;
				}
			}
			catch (Exception)
			{
				num = 1;
			}
			sendUpgradeDa(num);
			GameCanvas.end_Dialog();
			break;
		}
		case 7:
			if (ScreenUpgrade.mItemUpgrade[0] != null && ScreenUpgrade.mItemUpgrade[1] != null)
			{
				GlobalService.gI().ChuyenHoa(2, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[1].ID);
			}
			break;
		case 8:
			if (itemCur != null)
			{
				sendUpgradeDa(itemCur.numPotion);
			}
			GameCanvas.end_Dialog();
			break;
		case 9:
			GameCanvas.end_Dialog();
			input = GameCanvas.Start_Input_Dialog(T.nhapsoluong, new iCommand(T.strconfirm, 6, 0, this), isNum: true, T.tabupgradeDa);
			GameCanvas.subDialog = input;
			break;
		case 10:
			getMenu();
			break;
		case 11:
			GlobalService.gI().Upgrade_Item((sbyte)subIndex, 0, 0);
			break;
		case 12:
			if (ScreenUpgrade.mItemUpgrade[0] != null && ScreenUpgrade.mItemUpgrade[1] != null)
			{
				GlobalService.gI().Devil_Upgrade(17, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, ScreenUpgrade.mItemUpgrade[0].numPotion);
			}
			break;
		case 13:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
			}
			else if (ScreenUpgrade.mItemUpgrade[1] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullHaiThach);
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 20, 0, 0, 0);
			}
			break;
		case 14:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
			}
			else if (ScreenUpgrade.mItemUpgrade[1] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullHaiThach);
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 22, 0, 0, 0);
			}
			break;
		case 15:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 24, 0, 0, 0);
			}
			break;
		case 16:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 26, 0, 0, 0);
			}
			break;
		case 17:
			if (itemCur != null)
			{
				GlobalService.gI().rebuild_Item(typeRebuild, (sbyte)subIndex, itemCur.ID, itemCur.typeObject, 0);
			}
			break;
		case 18:
			if (ScreenUpgrade.mItemUpgrade[0] == null || ScreenUpgrade.mItemUpgrade[1] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullDaSieuCap);
			}
			else
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 26, 0, 0, 0);
			}
			break;
		case 19:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullDaSieuCap);
				break;
			}
			if (ScreenUpgrade.mItemUpgrade[1] != null)
			{
				ScreenUpgrade.mItemUpgrade[1].isRemove = true;
			}
			GlobalService.gI().rebuild_Item(typeRebuild, 31, 0, 0, 0);
			break;
		case 20:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
				break;
			}
			if (ScreenUpgrade.mItemUpgrade[1] != null)
			{
				ScreenUpgrade.mItemUpgrade[1] = null;
			}
			if (potionBua != null)
			{
				GlobalService.gI().rebuild_Item_DIAL(typeRebuild, 7, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1, potionBua.ID);
			}
			else
			{
				GlobalService.gI().rebuild_Item_DIAL(typeRebuild, 7, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 1, -1);
			}
			break;
		case 22:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItem);
			}
			else if (typeRebuild == 21)
			{
				GlobalService.gI().Hanh_Trinh(2, ScreenUpgrade.mItemUpgrade[0].ID);
				mSound.playSound(26, mSound.volumeSound);
				createEff(53, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
			}
			break;
		case 23:
			if (ScreenUpgrade.mItemUpgrade[0] != null && typeRebuild == 21)
			{
				GlobalService.gI().Hanh_Trinh(3, ScreenUpgrade.mItemUpgrade[0].ID);
			}
			break;
		case -1:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			break;
		case 21:
			break;
		}
	}

	private void getMenu()
	{
		mVector mVector2 = new mVector();
		if (!GameCanvas.isTouch)
		{
			mVector2.addElement(cmdClose);
		}
		if (typeRebuild != 1)
		{
			iCommand o = new iCommand(T.tabkhamda, 11, 9, this);
			mVector2.addElement(o);
		}
		if (typeRebuild != 4)
		{
			iCommand o2 = new iCommand(T.tabupgradeDa, 11, 12, this);
			mVector2.addElement(o2);
		}
		if (typeRebuild != 2)
		{
			iCommand o3 = new iCommand(T.tabdutlo, 11, 10, this);
			mVector2.addElement(o3);
		}
		if (typeRebuild != 19)
		{
			iCommand o4 = new iCommand(T.tabdutlo, 11, 10, this);
			mVector2.addElement(o4);
		}
		if (typeRebuild != 3)
		{
			iCommand o5 = new iCommand(T.tablayda, 11, 13, this);
			mVector2.addElement(o5);
		}
		if (typeRebuild != 6)
		{
			iCommand o6 = new iCommand(T.shopDa, 11, 11, this);
			mVector2.addElement(o6);
		}
		if (typeRebuild != 15)
		{
			iCommand o7 = new iCommand(T.tabdutlo, 11, 10, this);
			mVector2.addElement(o7);
		}
		GameCanvas.menuCur.startAt(mVector2, 2, T.menu);
	}

	private void sendUpgradeDa(int t)
	{
		MainItem itemVec = MainItem.getItemVec(itemCur.typeObject, itemCur.ID, vecInventory);
		if (itemVec != null)
		{
			if (t > itemVec.numPotion)
			{
				t = itemVec.numPotion;
			}
			if (itemCur != null)
			{
				GlobalService.gI().rebuild_Item(typeRebuild, 1, itemCur.ID, itemCur.typeObject, (short)t);
			}
		}
	}

	public override void paintTiLe(mGraphics g)
	{
		if (typeRebuild == 4)
		{
			if (ScreenUpgrade.mItemUpgrade[0] != null)
			{
				int num = (ScreenUpgrade.mItemUpgrade[0].ID - 44) % 6;
				if (ScreenUpgrade.mItemUpgrade[0].ID >= 221)
				{
					num = (ScreenUpgrade.mItemUpgrade[0].ID - 221) % 6;
				}
				if (num + 1 < ScreenUpgrade.mTileGhepĐa.Length)
				{
					mFont.tahoma_7b_black.drawString(g, T.tile + ScreenUpgrade.mTileGhepĐa[num + 1] + "%", xTiLe, yInven, 0);
				}
			}
		}
		else if (typeRebuild == 7)
		{
			if (Step == 0)
			{
				if (AvMain.imgJoin == null)
				{
					AvMain.imgJoin = mImage.createImage("/interface/muiten.png");
				}
				else
				{
					g.drawRegion(AvMain.imgJoin, 20, 0, 33, 14, 0, x + wInven + (w - wInven) / 2 - 2, posUp[0][1] + wItem, 33);
				}
			}
		}
		else if (typeRebuild == 8)
		{
			if (ScreenUpgrade.mItemUpgrade[0] != null)
			{
				mFont.tahoma_7b_black.drawString(g, T.tile + tile + "%", xTiLe, yInven, 0);
			}
		}
		else if (typeRebuild == 8)
		{
			if (ScreenUpgrade.mItemUpgrade[0] != null)
			{
				mFont.tahoma_7b_black.drawString(g, T.tile + tile + "%", xTiLe, yInven, 0);
			}
		}
		else if (typeRebuild == 11 || typeRebuild == 10)
		{
			if (ScreenUpgrade.mItemUpgrade[1] != null)
			{
				string text = "< 5";
				if (ScreenUpgrade.mItemUpgrade[1].ID == 226)
				{
					text = "25";
				}
				mFont.tahoma_7b_black.drawString(g, T.tile + text + "%", xTiLe, yInven, 0);
				if (ScreenUpgrade.mItemUpgrade[1].ID >= 221 && ScreenUpgrade.mItemUpgrade[1].ID <= 226)
				{
					mFont.tahoma_7b_black.drawString(g, T.truCheTac + T.mTiLeKichAn[ScreenUpgrade.mItemUpgrade[1].ID - 221], xTiLe, yInven + GameCanvas.hText, 0);
				}
			}
		}
		else if (typeRebuild == 13 && ScreenUpgrade.mItemUpgrade[0] != null && ScreenUpgrade.mItemUpgrade[1] != null)
		{
			mFont.tahoma_7b_black.drawString(g, T.tile + tile + "%", xTiLe, yInven, 0);
		}
	}

	public override void paintRectPlus(mGraphics g)
	{
		if (typeRebuild == 19)
		{
			AvMain.paintRect(g, posUp[0][0], posUp[0][1] - 2 * wItem + 5, wItem, wItem, 1, 3);
			if (potionBua != null)
			{
				potionBua.paintAllItem_Num1(g, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2 - 2 * wItem + 5, wItem, 0, potionBua.colorName, 1);
			}
		}
		else if (isPaintrectPlus)
		{
			AvMain.paintRect(g, posUp[0][0], posUp[0][1] - wItem - 3, wItem, wItem, 1, 3);
			if (potionBua != null)
			{
				potionBua.paintAllItem_Num1(g, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2 - wItem - 3, wItem, 0, potionBua.colorName, 1);
			}
		}
	}

	public override void paintIconUpgrade(mGraphics g, int x, int y, MainItem item)
	{
		if (ScreenUpgrade.mItemUpgrade[0] != null && ScreenUpgrade.mItemUpgrade[0].ID == item.ID && ScreenUpgrade.mItemUpgrade[0].typeObject == item.typeObject)
		{
			g.drawImage(AvMain.imgcheck, x, y - wItem / 2 + 2, mGraphics.BOTTOM | mGraphics.LEFT);
		}
	}

	public override bool isGetItemUpgrade(short Id, sbyte cat)
	{
		return false;
	}

	public override void updateNewUpgrade()
	{
		isShowInfo = false;
		getItemCurNew();
	}

	public override mVector getMenuActionItem()
	{
		mVector result = null;
		MainItem mainItem = (MainItem)vecInventory.elementAt(IdSelect);
		if (mainItem != null)
		{
			itemCur = mainItem;
			cmdBovao.caption = T.bovao;
			cmdBovao.subIndex = 1;
			if (isGetItemUpgrade(itemCur.ID, itemCur.typeObject))
			{
				cmdBovao.caption = T.layra;
				cmdBovao.subIndex = 0;
			}
		}
		if (itemCur != null)
		{
			result = itemCur.getActionSplit();
		}
		return result;
	}

	public override void setStep()
	{
		if (typeRebuild == 0)
		{
			updateSplit();
		}
		else if (typeRebuild == 1 || typeRebuild == 10 || typeRebuild == 11 || typeRebuild == 13 || typeRebuild == 8 || typeRebuild == 21)
		{
			updateKHAM();
		}
		else if (typeRebuild == 2 || typeRebuild == 19 || typeRebuild == 3 || typeRebuild == 4 || typeRebuild == 7 || typeRebuild == 14 || typeRebuild == 15)
		{
			updateLAY();
		}
		else if (typeRebuild == 12)
		{
			updateCONG_CHE_TAC();
		}
	}

	private void updateCONG_CHE_TAC()
	{
		if (Step == 1)
		{
			tickStep++;
			if (tickStep == 2)
			{
				mSound.playSound(28, mSound.volumeSound);
				createEff(76, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
				createEff(53, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
			}
			if (tickStep >= 30)
			{
				Step = 0;
				tickStep = 0;
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(showServer);
			}
		}
	}

	private void updateLAY()
	{
		if (Step == 1)
		{
			tickStep++;
			if (tickStop == 0)
			{
				mSound.playSound(26, mSound.volumeSound);
				mSound.playSound(27, mSound.volumeSound);
				int num = 0;
				if (typeRebuild == 7)
				{
					ScreenUpgrade.mItemUpgrade[0].LvUpgrade = 0;
					num = wItem / 4;
				}
				else
				{
					ScreenUpgrade.mItemUpgrade[0].isRemove = true;
				}
				createEff(75, 0, posUp[0][0] + wItem / 2 + num, posUp[0][1] + wItem / 2 + num, posUp[1][0] + wItem / 2 + num, posUp[1][1] + wItem / 2 + num);
				if (potionBua != null)
				{
					createEff(75, 0, posUp[0][0] + wItem / 2 + num, posUp[0][1] + wItem / 2 + num - wItem - 2, posUp[1][0] + wItem / 2 + num, posUp[1][1] + wItem / 2 + num);
					potionBua = null;
				}
				tickStop = tickStep + 11 + (w - num * 3 - wInven) / 6 * 4 / 5;
			}
			else
			{
				if (tickStep < tickStop)
				{
					return;
				}
				mSound.playSound(26, mSound.volumeSound);
				int num2 = 0;
				if (typeRebuild == 7)
				{
					num2 = wItem / 4;
				}
				createEff(53, 0, posUp[1][0] + wItem / 2 + num2, posUp[1][1] + wItem / 2 + num2, posUp[1][0] + wItem / 2 + num2, posUp[1][1] + wItem / 2 + num2);
				Step = 2;
				tickStep = 0;
				tickStop = 0;
				if (ScreenUpgrade.mItemUpgrade[0] != null)
				{
					if (typeRebuild == 7)
					{
						ScreenUpgrade.mItemUpgrade[1].LvUpgrade = ReadMessenge.numCuonghoa;
					}
					else
					{
						if (typeRebuild != 4)
						{
							ScreenUpgrade.mItemUpgrade[1] = ScreenUpgrade.mItemUpgrade[0];
						}
						if (ScreenUpgrade.mItemUpgrade[1] != null)
						{
							ScreenUpgrade.mItemUpgrade[1].isRemove = false;
						}
						ScreenUpgrade.mItemUpgrade[0] = null;
						potionBua = null;
					}
				}
				if (typeRebuild != 14)
				{
					return;
				}
				for (int i = 0; i < vecInventory.size(); i++)
				{
					MainItem mainItem = (MainItem)vecInventory.elementAt(i);
					if (mainItem.typeObject == 3 && mainItem.ID == idItem9x)
					{
						MainItem mainItem2 = new MainItem(3, mainItem.ID, mainItem.idIcon, 0, mainItem.colorName, mainItem.LvUpgrade);
						ScreenUpgrade.mItemUpgrade[1] = mainItem2;
					}
				}
			}
		}
		else if (Step == 2)
		{
			tickStep++;
			if (tickStep >= 20)
			{
				Step = 0;
				tickStep = 0;
				tickStop = 0;
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(showServer);
			}
		}
	}

	private void updateSplit()
	{
		if (Step != 1)
		{
			return;
		}
		tickStep++;
		if (tickStep % 5 != 0)
		{
			return;
		}
		if (tickStop == 0)
		{
			mSound.playSound(26, mSound.volumeSound);
			mSound.playSound(27, mSound.volumeSound);
			createEff(75, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[1][0] + wItem / 2, posUp[1][1] + wItem / 2);
			ScreenUpgrade.mItemUpgrade[0].isRemove = true;
			tickStop = tickStep + 11 + (w - lech * 3 - wInven) / 6 * 4 / 5;
		}
		else if (tickStep >= tickStop)
		{
			mSound.playSound(26, mSound.volumeSound);
			createEff(53, 0, posUp[1][0] + wItem / 2, posUp[1][1] + wItem / 2, posUp[1][0] + wItem / 2, posUp[1][1] + wItem / 2);
			Step = 0;
			tickStep = 0;
			tickStop = 0;
			ScreenUpgrade.mItemUpgrade[0] = null;
			if (ScreenUpgrade.mItemUpgrade[1] != null)
			{
				ScreenUpgrade.mItemUpgrade[1].isRemove = false;
			}
		}
	}

	public void setSetItem()
	{
		if (typeRebuild == 2)
		{
			isPaintrectPlus = false;
			if (ScreenUpgrade.mItemUpgrade[0] != null)
			{
				if (ScreenUpgrade.mItemUpgrade[0].numLoKham >= 4)
				{
					isPaintrectPlus = true;
				}
				else
				{
					potionBua = null;
				}
			}
		}
		else if (typeRebuild == 19)
		{
			isPaintrectPlus = false;
			if (ScreenUpgrade.mItemUpgrade[0] != null)
			{
				isPaintrectPlus = true;
			}
		}
		else if (typeRebuild == 15)
		{
			isPaintrectPlus = false;
			if (ScreenUpgrade.mItemUpgrade[0] != null)
			{
				isPaintrectPlus = true;
			}
		}
	}

	public override iCommand getCmdShopMore()
	{
		if (typeRebuild == 19 || typeRebuild == 21)
		{
			return null;
		}
		return cmdMenu;
	}

	public void XoaDa(short id)
	{
		for (int i = 0; i < vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)vecInventory.elementAt(i);
			if (mainItem.ID == id)
			{
				vecInventory.removeElementAt(i);
				break;
			}
		}
	}

	public void ChangeCmd()
	{
		if (ScreenUpgrade.mItemUpgrade[0] == null)
		{
			cmdUpgrade.caption = T.kham;
			cmdUpgrade.indexMenu = 22;
		}
		else
		{
			cmdUpgrade.caption = T.laydara;
			cmdUpgrade.indexMenu = 23;
		}
	}
}
