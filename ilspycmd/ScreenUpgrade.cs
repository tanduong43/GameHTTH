using System;

public class ScreenUpgrade : MainScreen
{
	public int w;

	public int h;

	public int wItem;

	public int x;

	public int y;

	public int lech;

	public int IdSelect;

	public int xInven;

	public int yInven;

	public int wInven;

	public int hInven;

	public int maxNumItemW;

	public int xInfo;

	public int yInfo;

	public int xMid;

	public int yMid;

	public int[][] posUp;

	public bool isShowInfo;

	public ListNew list;

	public mVector vecCmd = new mVector();

	public MainItem itemCur;

	public iCommand cmdBovao;

	public iCommand cmdLucky;

	public iCommand cmdClose;

	public iCommand cmdUpgrade;

	public iCommand cmdBoVaoAll;

	public iCommand cmdShop;

	public iCommand cmdMenu;

	public iCommand cmdBovaoDasieucap;

	public static MainItem[] mItemUpgrade;

	public static MainDataUpgrade[] mTemMaterialUpgrade;

	public mVector vecEff = new mVector("ScreenUpgrade.vecEff");

	public string nameScreen = string.Empty;

	public string showServer = string.Empty;

	public int Step;

	public int tickStep;

	public int indexAreaSellect;

	public int xTiLe;

	public sbyte typeRebuild;

	public static short[] mTileUpdate;

	public static short[] mTileGhepĐa;

	public static short[] mTileChuyenHoa;

	public static sbyte curTypeShop = -1;

	public FrameImage fraEffUpgrade;

	public static ScreenUpgrade instance;

	public sbyte typeShowItem;

	public const sbyte SHOWITEM_ONLY_DA_KHAM = 1;

	public const sbyte SHOWITEM_ONLY_DA_KHAM_ITEM = 2;

	public const sbyte SHOWITEM_ONLY_ITEM = 3;

	public const sbyte SHOWITEM_ONLY_ITEM_AND_BUA = 4;

	public const sbyte SHOWITEM_ONLY_ITEM_AND_HAMMER_DIAL = 5;

	private InputDialog input;

	public mVector vecInventory = new mVector();

	public static int valueLucky = 1;

	public static int valueRotCap;

	public int valueBaoHiem;

	public int valueMayMan;

	public mImage imghoavan;

	public mImage imgtron;

	public int frameEff;

	public int frameEndEff;

	private bool isRunEff;

	public sbyte timeShowInfo;

	public int indexStep = 1;

	public int tickStop;

	public static int valueTile;

	public int valueTileRotCap;

	public static int valueMonney_1;

	public static int valueMonney_2;

	public static int valueMonney_3;

	public ScreenUpgrade()
	{
	}

	public ScreenUpgrade(sbyte typeRebuild, int size)
	{
		mSystem.outz("ScreenUpgrade type rebuild " + typeRebuild);
		this.typeRebuild = typeRebuild;
		vecInventory = Player.vecInventory;
		curTypeShop = this.typeRebuild;
		Step = 0;
		lech = 10;
		w = 290;
		h = 185;
		wItem = 24;
		if (GameCanvas.isTouch)
		{
			wItem = 28;
		}
		if (w > MotherCanvas.w)
		{
			w = MotherCanvas.w;
		}
		if (h > MotherCanvas.h - GameCanvas.hCommand * 2)
		{
			h = MotherCanvas.h - GameCanvas.hCommand * 2;
		}
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2 + 5;
		wInven = w / 2;
		wInven -= wInven % wItem;
		hInven = h - lech * 3 - 5;
		maxNumItemW = wInven / wItem;
		xInven = x + w / 4 - wInven / 2 + 15;
		yInven = y + h / 2 - hInven / 2;
		int limX = ((Player.maxInventory - 1) / maxNumItemW + 1) * wItem - hInven;
		list = new ListNew(xInven, yInven, wInven, hInven, 0, 0, limX, isLim0: true);
		cmdClose = new iCommand(T.close, -1, this);
		cmdShop = new iCommand(T.shopDa, 2, this);
		cmdMenu = new iCommand(T.menu, 3, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x + w / 2 + 60, y - 15 + 10 + 8, MainTab.fraCloseTab, string.Empty);
			vecCmd.addElement(cmdClose);
			if (typeRebuild != 18 && typeRebuild != 22)
			{
				AvMain.setPosCMD(cmdShop, 2);
				if (typeRebuild != 6 && typeRebuild != 21)
				{
					vecCmd.addElement(cmdShop);
				}
				if (GameCanvas.isTouchNoOrPC())
				{
					backCMD = cmdClose;
					menuCMD = cmdUpgrade;
				}
			}
			else if (GameCanvas.isTouchNoOrPC())
			{
				backCMD = cmdClose;
				menuCMD = cmdUpgrade;
			}
		}
		else
		{
			cmdClose.caption = T.close + " " + T.Upgrade;
			right = cmdMenu;
			left = cmdUpgrade;
		}
		xTiLe = xInven + wInven + 4;
		setStart(typeRebuild, size);
		if (typeRebuild == 20)
		{
			cmdUpgrade.caption = T.join;
		}
	}

	public void getMenushop()
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(cmdShop);
		mVector2.addElement(cmdClose);
		GameCanvas.menuCur.startAt(mVector2, 2, T.Upgrade);
	}

	public override void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public virtual void setStart(sbyte typeRebuild, int size)
	{
		int num = x + wInven + (w - lech * 3 - wInven) / 2 + 10;
		int num2 = y + h / 2 - wItem / 2 - 10;
		if (size < 0)
		{
			int num3 = 270;
			int num4 = (w - lech * 3 - wInven) / 2 - 20;
			if (num4 < wItem)
			{
				num4 = wItem;
			}
			nameScreen = T.cuonghoa;
			posUp = mSystem.new_M_Int(5, 2);
			if (typeRebuild == 15)
			{
				posUp = mSystem.new_M_Int(6, 2);
				num2 += wItem / 4;
				nameScreen = T.sieuCuonghoa;
			}
			if (typeRebuild == 18)
			{
				posUp = mSystem.new_M_Int(7, 2);
				num2 += wItem / 4;
				nameScreen = T.cuonghoaDIAL;
			}
			if (typeRebuild == 22)
			{
				posUp = mSystem.new_M_Int(6, 2);
				num2 += wItem / 4;
				nameScreen = T.cuongHoaSkin;
				typeShowItem = 1;
				IdSelect = -1;
			}
			posUp[0][0] = num;
			posUp[0][1] = num2;
			if (typeRebuild == 22)
			{
				num3 = 180;
			}
			for (int i = 1; i < posUp.Length; i++)
			{
				if (typeRebuild == 18 || typeRebuild == 22)
				{
					posUp[i][0] = num + CRes.getsin(CRes.fixangle(num3)) * num4 / 1000;
					posUp[i][1] = num2 + CRes.getcos(CRes.fixangle(num3)) * num4 / 1000;
				}
				else
				{
					posUp[i][0] = num + CRes.getcos(CRes.fixangle(num3)) * num4 / 1000;
					posUp[i][1] = num2 + CRes.getsin(CRes.fixangle(num3)) * num4 / 1000;
				}
				num3 += 360 / (posUp.Length - 1);
			}
			cmdBovao = new iCommand(T.bovao, 0, this);
			cmdUpgrade = new iCommand(T.Upgrade, 1, this);
			if (typeRebuild == 15)
			{
				cmdBovao = new iCommand(T.bovao, 4, this);
				cmdUpgrade = new iCommand(T.Upgrade, 5, this);
			}
			if (typeRebuild == 18)
			{
				cmdBovao = new iCommand(T.bovao, 11, this);
				cmdUpgrade = new iCommand(T.Upgrade, 12, this);
			}
			if (typeRebuild == 22)
			{
				cmdBovao = new iCommand(T.bovao, 13, this);
				cmdUpgrade = new iCommand(T.Upgrade, 14, this);
			}
			if (typeRebuild != 18 && typeRebuild != 22 && GameCanvas.isTouch)
			{
				cmdUpgrade.setPos(posUp[0][0] + wItem / 2, y + h - iCommand.hButtonCmdNor / 2 - 10, null, cmdUpgrade.caption);
				vecCmd.addElement(cmdUpgrade);
			}
			if (!GameCanvas.isTouch)
			{
				right = cmdMenu;
				left = cmdUpgrade;
			}
		}
		else
		{
			num = x + wInven + (w - lech * 3 - wInven) / 6 + 5;
			num2 = y + h / 2 - wItem / 2 - 5;
			int num5 = 3;
			posUp = mSystem.new_M_Int(size + 1, 2);
			int num6 = 0;
			int num7 = size % 2;
			num6 = ((size > num5) ? (num2 - (wItem + wItem / 4)) : ((num7 != 0) ? (num2 - size / 2 * (wItem + wItem / 4)) : (y + h / 2 - 5 - size / 2 * (wItem + wItem / 4))));
			for (int j = 0; j < posUp.Length; j++)
			{
				posUp[j][0] = num + j / num5 * (wItem + wItem / 4);
				posUp[j][1] = num6 + j % num5 * (wItem + wItem / 4);
				if (j == posUp.Length - 1)
				{
					posUp[posUp.Length - 1][0] = num + (w - lech * 3 - wInven) / 6 * 4;
					posUp[posUp.Length - 1][1] = num2;
				}
			}
			cmdUpgrade = new iCommand(T.Upgrade, 10, this);
			left = cmdUpgrade;
		}
		mItemUpgrade = new MainItem[posUp.Length];
		if (IdSelect >= 0)
		{
			getItemCurNew();
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (itemCur != null)
			{
				if (itemCur.typeObject == 3)
				{
					GlobalService.gI().Upgrade_Item(4, itemCur.ID, 0);
				}
				else if (itemCur.typeObject == 7 && itemCur.getStar())
				{
					GlobalService.gI().Upgrade_Item(5, itemCur.ID, (sbyte)subIndex);
				}
				else if (itemCur.typeObject == 7 && itemCur.typeMaterial == 3)
				{
					GlobalService.gI().Upgrade_Item(6, itemCur.ID, (sbyte)subIndex);
				}
			}
			break;
		case 1:
			if (mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItemUpgrade);
			}
			else
			{
				GlobalService.gI().Upgrade_Item(1, mItemUpgrade[0].ID, 0);
			}
			break;
		case 2:
			GlobalService.gI().Upgrade_Item(8, 0, 0);
			break;
		case 3:
			getMenushop();
			break;
		case 4:
			if (itemCur == null)
			{
				break;
			}
			if (itemCur.typeObject == 3)
			{
				GlobalService.gI().Upgrade_Super_Item(4, itemCur.ID, 0, 0);
			}
			else if (itemCur.typeObject == 7 && itemCur.getStar())
			{
				GlobalService.gI().Upgrade_Super_Item(5, itemCur.ID, (sbyte)subIndex, 1);
			}
			else if (itemCur.typeObject == 7 && itemCur.typeMaterial == 3)
			{
				if (subIndex == 0)
				{
					GlobalService.gI().Upgrade_Super_Item(6, itemCur.ID, 0, 0);
					break;
				}
				input = new InputDialog();
				iCommand cmd = new iCommand(T.bovao, 6, this);
				input.setinfo(T.soluong, cmd, isNum: false, itemCur.namepaint);
				GameCanvas.Start_Current_Dialog(input);
			}
			else if (itemCur.typeObject == 7 && itemCur.typeMaterial == 7)
			{
				GlobalService.gI().Upgrade_Super_Item(14, itemCur.ID, (sbyte)subIndex, 1);
			}
			break;
		case 5:
			if (mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItemUpgrade);
			}
			else
			{
				GlobalService.gI().Upgrade_Super_Item(1, mItemUpgrade[0].ID, 0, 0);
			}
			break;
		case 6:
			if (itemCur != null)
			{
				int num3 = 1;
				try
				{
					num3 = int.Parse(input.tfInput.getText());
					if (num3 < 0)
					{
						num3 = 1;
					}
				}
				catch (Exception)
				{
					num3 = 1;
				}
				GlobalService.gI().Upgrade_Super_Item(6, itemCur.ID, 1, (sbyte)num3);
			}
			GameCanvas.end_Cur_Dialog();
			break;
		case 7:
			if (itemCur != null)
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
				GlobalService.gI().Upgrade_Dial(5, itemCur.ID, 1, (sbyte)num);
			}
			GameCanvas.end_Cur_Dialog();
			break;
		case 8:
			if (itemCur != null)
			{
				int num2 = 1;
				try
				{
					num2 = int.Parse(input.tfInput.getText());
					if (num2 < 0)
					{
						num2 = 1;
					}
				}
				catch (Exception)
				{
					num2 = 1;
				}
				GlobalService.gI().Upgrade_Dial(6, itemCur.ID, 1, (sbyte)num2);
			}
			GameCanvas.end_Cur_Dialog();
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
		case 10:
			GlobalService.gI().Devil_Upgrade(20, mItemUpgrade[mItemUpgrade.Length - 1].ID, mItemUpgrade[mItemUpgrade.Length - 1].typeObject, 0);
			break;
		case 11:
			if (itemCur == null)
			{
				break;
			}
			if (itemCur.typeObject == 3)
			{
				GlobalService.gI().Upgrade_Dial(4, itemCur.ID, 0, 0);
			}
			else if (itemCur.typeObject == 7 && itemCur.getStar())
			{
				if (subIndex == 0)
				{
					GlobalService.gI().Upgrade_Dial(5, itemCur.ID, 0, 0);
					break;
				}
				input = new InputDialog();
				iCommand cmd2 = new iCommand(T.bovao, 7, this);
				input.setinfo(T.soluong, cmd2, isNum: false, itemCur.namepaint);
				GameCanvas.Start_Current_Dialog(input);
			}
			else if (itemCur.typeObject == 7 && itemCur.typeMaterial == 3)
			{
				if (subIndex == 0)
				{
					GlobalService.gI().Upgrade_Dial(6, itemCur.ID, 0, 0);
					break;
				}
				input = new InputDialog();
				iCommand cmd3 = new iCommand(T.bovao, 8, this);
				input.setinfo(T.soluong, cmd3, isNum: false, itemCur.namepaint);
				GameCanvas.Start_Current_Dialog(input);
			}
			else if (itemCur.typeObject == 7 && itemCur.typeMaterial == 7)
			{
				GlobalService.gI().Upgrade_Dial(14, itemCur.ID, (sbyte)subIndex, 1);
			}
			break;
		case 12:
			if (mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItemUpgrade);
			}
			else
			{
				GlobalService.gI().Upgrade_Dial(1, mItemUpgrade[0].ID, 0, 0);
			}
			break;
		case 9:
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		paintBackGr(g);
		if (Step == 0)
		{
			paintInven(g);
		}
		else if (typeRebuild != 22)
		{
			if (fraEffUpgrade == null)
			{
				fraEffUpgrade = new FrameImage(mImage.createImage("/interface/effupgrade.png"), 58, 50);
			}
			fraEffUpgrade.drawFrame(frameEff % fraEffUpgrade.nFrame, xInven + wInven / 2 + 10, yInven + hInven / 2, 0, 3, g);
		}
		GameCanvas.resetTrans(g);
		Interface_Game.paintNumMess(g, -Interface_Game.xNumMess + 8, -Interface_Game.yNumMess + 3);
		paintEff(g, -1);
		paintTrade(g);
		paintPosItem(g);
		paintTiLe(g);
		if (vecCmd != null && GameCanvas.getShowCmd())
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		if (isShowInfo && Step == 0 && itemCur != null)
		{
			MainTab.paintInfoEveryWhere(g, itemCur, null, 0, xInfo, yInfo, itemCur.wInfo, itemCur.hInfo, isLv: false, null, 0);
		}
		base.paint(g);
		Interface_Game.paintEffShowMoney(g, MotherCanvas.hw - Interface_Game.wMoneyEff / 2, GameScreen.h12plus, isShow: true, 0);
		paintChat(g);
		paintEff(g, 0);
	}

	public virtual void paintTiLe(mGraphics g)
	{
		if (typeRebuild == 20)
		{
			mFont.tahoma_7b_black.drawString(g, T.tile + valueTile + "%", xTiLe, yInven, 0);
			int num = xInven + wInven + 20;
			int num2 = yInven + hInven - 20;
			AvMain.paintRect(g, num, num2, Interface_Game.wMoneyEff - 50, 30, 1, 4);
			num2 += 9;
			AvMain.fraMoney.drawFrame(0, num + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 - 3, num2, 0, 3, g);
			AvMain.fraMoney.drawFrame(1, num + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 + 45, num2, 0, 3, g);
			AvMain.fraMoney.drawFrame(7, num + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 - 3, num2 + 15, 0, 3, g);
			mFont.tahoma_7_yellow.drawString(g, " " + AvMain.getMoneyShortText(valueMonney_1), num + Interface_Game.mini + AvMain.fraMoney.frameWidth - 4, num2 - 5, 0);
			mFont.tahoma_7_red.drawString(g, " " + AvMain.getDotNumber(valueMonney_2), num + Interface_Game.mini + AvMain.fraMoney.frameWidth + 45, num2 - 5, 0);
			mFont.tahoma_7_green.drawString(g, " " + AvMain.getMoneyShortText(valueMonney_3), num + Interface_Game.mini + AvMain.fraMoney.frameWidth - 4, num2 - 5 + 15, 0);
		}
		else if (typeRebuild == 18)
		{
			mFont.tahoma_7b_black.drawString(g, T.tilerot + valueRotCap + "%", xTiLe, yInven, 0);
			int num3 = xInven + wInven + 20;
			int num4 = yInven + hInven - 20;
			AvMain.paintRect(g, num3, num4, Interface_Game.wMoneyEff - 50, 30, 1, 4);
			num4 += 9;
			AvMain.fraMoney.drawFrame(0, num3 + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 - 3, num4, 0, 3, g);
			AvMain.fraMoney.drawFrame(1, num3 + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 + 45, num4, 0, 3, g);
			AvMain.fraMoney.drawFrame(7, num3 + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 - 3, num4 + 15, 0, 3, g);
			mFont.tahoma_7_yellow.drawString(g, " " + AvMain.getMoneyShortText(valueMonney_1), num3 + Interface_Game.mini + AvMain.fraMoney.frameWidth - 4, num4 - 5, 0);
			mFont.tahoma_7_red.drawString(g, " " + AvMain.getDotNumber(valueMonney_2), num3 + Interface_Game.mini + AvMain.fraMoney.frameWidth + 45, num4 - 5, 0);
			mFont.tahoma_7_green.drawString(g, " " + AvMain.getMoneyShortText(valueMonney_3), num3 + Interface_Game.mini + AvMain.fraMoney.frameWidth - 4, num4 - 5 + 15, 0);
		}
		else if (typeRebuild == 15 && mItemUpgrade[0] != null)
		{
			mFont.tahoma_7b_black.drawString(g, T.tilerot + valueRotCap + "%", xTiLe, yInven, 0);
		}
		else if (mItemUpgrade[0] != null && mItemUpgrade[0].LvUpgrade < mTileUpdate.Length)
		{
			if (mItemUpgrade[1] != null)
			{
				mFont.tahoma_7b_black.drawString(g, T.tile + mTileUpdate[mItemUpgrade[0].LvUpgrade] / 10 + "% + " + mTileUpdate[mItemUpgrade[0].LvUpgrade] / valueLucky + "%", xTiLe, yInven, 0);
			}
			else
			{
				mFont.tahoma_7b_black.drawString(g, T.tile + mTileUpdate[mItemUpgrade[0].LvUpgrade] / 10 + "%", xTiLe, yInven, 0);
			}
		}
	}

	public virtual void paintTrade(mGraphics g)
	{
	}

	public virtual void paintChat(mGraphics g)
	{
	}

	public virtual void paintBackGr(mGraphics g)
	{
		paintPaper(g, x, y - 17, w, h + 17, w, AvMain.PAPER_NORMAL);
		AvMain.paintRect(g, xInven, yInven, wInven, hInven, 0, 3);
		g.setColor(14203529);
		g.fillRoundRect(x + w / 2 - 60, y - 15 + 10, 120, 16, 4, 4);
		AvMain.FontBorderColor(g, nameScreen, x + w / 2, y - 15 + 12, 2, 6, 5);
		if (typeRebuild == 15 || typeRebuild == 18 || typeRebuild == 22)
		{
			if (imghoavan == null)
			{
				loadImg();
			}
			g.drawRegion(imghoavan, 0, 0, 35, 35, 0, x + w - 35 - 10, y + h - 35 - 10, 0);
			g.drawRegion(imghoavan, 0, 0, 35, 35, 1, x + w - 35 - 10, y - 5, 0);
			g.drawRegion(imghoavan, 0, 0, 35, 35, 2, x + 10, y + h - 35 - 10, 0);
			g.drawRegion(imghoavan, 0, 0, 35, 35, 3, x + 10, y - 5, 0);
			g.drawImage(imgtron, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, 3);
		}
		if (typeRebuild != 21)
		{
			return;
		}
		MainImage imageOther = ObjectData.getImageOther(SplitScreen.instance.idMinimap, 0);
		if (imageOther != null && imageOther.img != null)
		{
			g.drawImage(imageOther.img, posUp[0][0] + wItem / 2, posUp[0][1] - wItem / 6, 33);
		}
		if (SplitScreen.instance.vecDaKhamHanhTrinh.size() > 0)
		{
			mFont.tahoma_7b_red.drawString(g, SplitScreen.instance.nameMinimap, posUp[0][0] + wItem / 2, yInven, 2);
		}
		else
		{
			mFont.tahoma_7b_black.drawString(g, SplitScreen.instance.nameMinimap, posUp[0][0] + wItem / 2, yInven, 2);
		}
		if (mItemUpgrade[0] != null && mItemUpgrade[0].info != null)
		{
			string[] array = mFont.tahoma_7_black.splitFontArray(mItemUpgrade[0].info, 120);
			if (array.Length == 1)
			{
				mFont.tahoma_7_black.drawString(g, mItemUpgrade[0].info, posUp[0][0] + wItem / 2, posUp[0][1] + wItem * 5 / 4, 2);
			}
			else if (array.Length > 1)
			{
				mFont.tahoma_7_black.drawString(g, array[0], posUp[0][0] + wItem / 2, posUp[0][1] + wItem * 5 / 4 - 5, 2);
				mFont.tahoma_7_black.drawString(g, array[1], posUp[0][0] + wItem / 2, posUp[0][1] + wItem * 5 / 4 + 5, 2);
			}
		}
	}

	private void loadImg()
	{
		imghoavan = mImage.createImage("/interface/u_hoavan.png");
		imgtron = mImage.createImage("/interface/u_tron.png");
	}

	public virtual void paintPosItem(mGraphics g)
	{
		for (int i = 0; i < posUp.Length; i++)
		{
			AvMain.paintRect(g, posUp[i][0], posUp[i][1], wItem, wItem, 1, 3);
			if (mItemUpgrade[i] == null || mItemUpgrade[i].isRemove)
			{
				continue;
			}
			mItemUpgrade[i].paintColor(g, posUp[i][0] + wItem / 2, posUp[i][1] + wItem / 2, wItem - 1);
			if (typeRebuild == 20)
			{
				if (mItemUpgrade[i].isPaint)
				{
					mItemUpgrade[i].paintAllItem_Num1(g, posUp[i][0] + wItem / 2, posUp[i][1] + wItem / 2, wItem, 0, mItemUpgrade[i].colorName, mItemUpgrade[i].numPotion);
				}
			}
			else
			{
				mItemUpgrade[i].paintAllItem(g, posUp[i][0] + wItem / 2, posUp[i][1] + wItem / 2, wItem, 0, mItemUpgrade[i].colorName);
			}
		}
		paintRectPlus(g);
	}

	public virtual void paintRectPlus(mGraphics g)
	{
	}

	public void paintEff(mGraphics g, int level)
	{
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			if (level == 0 && mainEffect.levelPaint > -1)
			{
				mainEffect.paint(g);
			}
			else if (mainEffect.levelPaint == -1)
			{
				mainEffect.paint(g);
			}
		}
	}

	public virtual void paintIconUpgrade(mGraphics g, int x, int y, MainItem item)
	{
		for (int i = 0; i < mItemUpgrade.Length - 2; i++)
		{
			if (mItemUpgrade[i] != null && mItemUpgrade[i].ID == item.ID && mItemUpgrade[i].typeObject == item.typeObject)
			{
				g.drawImage(AvMain.imgcheck, x, y, mGraphics.BOTTOM | mGraphics.LEFT);
			}
		}
	}

	public virtual void paintInven(mGraphics g)
	{
		g.setClip(xInven - 1, yInven + 1, wInven + 2, hInven - 1);
		g.saveCanvas();
		g.ClipRec(xInven - 1, yInven + 1, wInven + 2, hInven - 1);
		g.translate(xInven, yInven);
		g.translate(0, -list.cmx);
		for (int i = 0; i < vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)vecInventory.elementAt(i);
			if (mainItem.typeObject == 3)
			{
				mainItem.paintColor(g, i % maxNumItemW * wItem + wItem / 2, i / maxNumItemW * wItem + wItem / 2, wItem);
			}
			mainItem.paint(g, i % maxNumItemW * wItem + wItem / 2, i / maxNumItemW * wItem + wItem / 2, wItem);
			paintIconUpgrade(g, i % maxNumItemW * wItem + 3, i / maxNumItemW * wItem + wItem - 2, mainItem);
			paintlockTrade(g, mainItem, i % maxNumItemW * wItem + wItem / 2, i / maxNumItemW * wItem + wItem / 2);
			paintlockShow(g, mainItem, i % maxNumItemW * wItem + wItem / 2, i / maxNumItemW * wItem + wItem / 2);
			if (IdSelect == i && indexAreaSellect == 0)
			{
				g.setColor(16777215);
				g.drawRect(i % maxNumItemW * wItem + 1, i / maxNumItemW * wItem + 1, wItem - 2, wItem - 2);
				if (!GameCanvas.isSmallScreen)
				{
					g.drawRect(i % maxNumItemW * wItem + 2, i / maxNumItemW * wItem + 2, wItem - 4, wItem - 4);
				}
			}
		}
		if (Player.maxInventory % maxNumItemW != 0)
		{
			for (int j = Player.maxInventory; j < Player.maxInventory + (maxNumItemW - Player.maxInventory % maxNumItemW); j++)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, wItem - 1, wItem - 1, 0, j % maxNumItemW * wItem + 1, j / maxNumItemW * wItem + 1, 0);
			}
		}
		g.setColor(14075832);
		for (int k = 0; k < maxNumItemW - 1; k++)
		{
			g.fillRect(wItem + k * wItem, 1, 1, wItem * ((Player.maxInventory - 1) / maxNumItemW + 1));
		}
		for (int l = 0; l <= (Player.maxInventory - 1) / maxNumItemW + 1; l++)
		{
			g.fillRect(1, l * wItem, wInven - 1, 1);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
	}

	public virtual void paintlockShow(mGraphics g, MainItem item, int x, int y)
	{
		bool flag = false;
		if (typeShowItem == 1)
		{
			if (item.typeObject == -8)
			{
				flag = false;
			}
			else if (item.typeObject != 4 || item.ID < 44 || (item.ID > 79 && item.ID < 221) || (item.ID > 226 && item.ID < 241) || (item.ID > 270 && item.ID < 362) || (item.ID > 373 && item.ID < 493) || item.ID > 517)
			{
				flag = true;
			}
		}
		else if (typeShowItem == 2)
		{
			if (item.typeObject == -8)
			{
				flag = false;
			}
			else if (item.typeObject == 4)
			{
				if (item.ID < 44 || (item.ID > 79 && item.ID < 221) || (item.ID > 226 && item.ID < 241) || (item.ID > 270 && item.ID < 362 && item.ID != 324 && item.ID != 325 && item.ID != 326) || item.ID > 373)
				{
					flag = true;
				}
			}
			else if (item.typeObject != 3)
			{
				flag = true;
			}
		}
		else if (typeShowItem == 3)
		{
			if (item.typeObject == -8)
			{
				flag = false;
			}
			else if (item.typeObject != 3)
			{
				flag = true;
			}
		}
		else if (typeShowItem == 4)
		{
			if (item.typeObject != 3 && (item.typeObject != 4 || (item.ID != 323 && item.ID != 339)))
			{
				flag = true;
			}
		}
		else if (typeShowItem == 5 && item.typeObject != 3 && (item.typeObject != 4 || item.ID != 457))
		{
			flag = true;
		}
		if (flag)
		{
			g.drawRegion(AvMain.imgDelay, 0, 0, wItem, wItem, 0, x, y, 3);
		}
	}

	public virtual void paintlockTrade(mGraphics g, MainItem item, int x, int y)
	{
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		int cmx = list.cmx;
		list.moveCamera();
		if (list.cmx != cmx || list.pointerIsDowning)
		{
			isShowInfo = false;
		}
		else if (itemCur != null)
		{
			updateShowInfo();
		}
		for (int i = 0; i < vecEff.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(i);
			mainEffect.update();
			if (mainEffect.isStop)
			{
				vecEff.removeElement(mainEffect);
				i--;
			}
		}
		if (Step != 0)
		{
			if (frameEff == 0 && CRes.random(7) < 3)
			{
				isRunEff = true;
				frameEndEff = (1 + CRes.random(2)) * 3;
			}
			if (isRunEff)
			{
				frameEff++;
			}
			if (frameEff >= frameEndEff)
			{
				isRunEff = false;
				frameEff = 0;
			}
		}
		base.update();
		setStep();
	}

	public void updateShowInfo()
	{
		if (!isShowInfo)
		{
			timeShowInfo++;
			if (timeShowInfo >= MainTab.limitTimeShow)
			{
				isShowInfo = true;
				setPosInfo();
			}
		}
		else
		{
			timeShowInfo = 0;
		}
	}

	public virtual void setPosInfo()
	{
		setPosInfo(itemCur, xInven, yInven + (IdSelect / maxNumItemW + 1) * wItem - list.cmx + 4);
	}

	public void setPosInfo(MainItem item, int xbe, int ybe)
	{
		int num = 100;
		int num2 = 50;
		if (item != null)
		{
			num = item.wInfo;
			num2 = item.hInfo;
		}
		xInfo = xbe - num / 2;
		if (xInfo + num > MotherCanvas.w - 8)
		{
			xInfo = MotherCanvas.w - num - 8;
		}
		if (xInfo < 8)
		{
			xInfo = 8;
		}
		yInfo = ybe;
		if (yInfo + num2 > MotherCanvas.h - GameCanvas.hCommand - 8)
		{
			yInfo = MotherCanvas.h - GameCanvas.hCommand - num2 - 8;
		}
		if (yInfo < 8)
		{
			yInfo = 8;
		}
	}

	public void paintSuper(mGraphics g)
	{
		base.paint(g);
	}

	public override void updatekey()
	{
		if (Step != 0)
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyMove(0))
		{
			IdSelect--;
			GameCanvas.ClearkeyMove(0);
			flag = true;
		}
		else if (GameCanvas.keyMove(2))
		{
			IdSelect++;
			GameCanvas.ClearkeyMove(2);
			flag = true;
		}
		else if (GameCanvas.keyMove(1))
		{
			if (IdSelect >= maxNumItemW)
			{
				IdSelect -= maxNumItemW;
			}
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			if (IdSelect < vecInventory.size() - maxNumItemW)
			{
				IdSelect += maxNumItemW;
			}
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (flag)
		{
			getItemCurNew();
		}
		base.updatekey();
		updatekeyPC();
	}

	public void updatekeySuper()
	{
		base.updatekey();
	}

	public override void updatePointer()
	{
		if (Step != 0)
		{
			return;
		}
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointSelect(xInven, yInven, wInven, hInven))
		{
			int num = (GameCanvas.px - xInven) / wItem + (GameCanvas.py - yInven + list.cmx) / wItem * maxNumItemW;
			int num2 = vecInventory.size();
			if (num >= 0 && num < num2)
			{
				GameCanvas.isPointerSelect = false;
				if (num == IdSelect)
				{
					if (itemCur != null && (itemCur.typeObject == 3 || (itemCur.typeObject == 7 && (itemCur.getStar() || itemCur.typeMaterial == 3 || itemCur.typeMaterial == 7)) || (itemCur.typeObject == 4 && (itemCur.ID == 457 || itemCur.ID == 323 || itemCur.ID == 339))) && cmdBovao != null)
					{
						cmdBovao.perform();
					}
				}
				else
				{
					isShowInfo = false;
					IdSelect = num;
					setPosCmd(getMenuActionItem());
				}
			}
			else
			{
				isShowInfo = false;
				IdSelect = -1;
				setPosCmd(null);
			}
		}
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		base.updatePointer();
	}

	public virtual void getItemCurNew()
	{
		isShowInfo = false;
		IdSelect = AvMain.resetSelect(IdSelect, vecInventory.size() - 1, isreset: false);
		if (GameCanvas.isTouchNoOrPC())
		{
			list.setToX((IdSelect / maxNumItemW + 1) * wItem - h / 2);
		}
		if (IdSelect >= 0)
		{
			setPosCmd(getMenuActionItem());
		}
	}

	public virtual mVector getMenuActionItem()
	{
		mVector result = null;
		MainItem mainItem = (MainItem)vecInventory.elementAt(IdSelect);
		if (mainItem != null)
		{
			if (typeRebuild == 20)
			{
				itemCur = mainItem;
				return null;
			}
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
			result = itemCur.getActionUpgrade();
		}
		if (typeRebuild == 15 || typeRebuild == 18)
		{
			setRotCap();
		}
		return result;
	}

	private void setRotCap()
	{
		if (mItemUpgrade[0] != null && (mItemUpgrade[0].LvUpgrade == 10 || (typeRebuild == 18 && mItemUpgrade[0].LvUpgrade == 0)))
		{
			valueRotCap = 0;
			return;
		}
		if (typeRebuild == 18)
		{
			valueRotCap = valueTileRotCap - valueBaoHiem;
			if (mItemUpgrade[3] != null)
			{
				valueRotCap -= 20;
			}
			return;
		}
		int num = 0;
		if (mItemUpgrade[2] != null)
		{
			num = mItemUpgrade[2].numPotion * 15;
		}
		if (mItemUpgrade[3] != null)
		{
			num += 20;
		}
		if (num > 80)
		{
			num = 80;
		}
		valueRotCap = 80 - num;
	}

	public virtual void setPosCmd(mVector vec)
	{
		left = null;
		center = null;
		vecCmd.removeAllElements();
		if (vec != null)
		{
			if (GameCanvas.isTouch)
			{
				vecCmd = vec;
				for (int i = 0; i < vecCmd.size(); i++)
				{
					iCommand cmd = (iCommand)vecCmd.elementAt(i);
					if (i == 0)
					{
						cmd = (menuCMD = AvMain.setPosCMD(cmd, 1));
					}
					if (i == 1)
					{
						cmd = AvMain.setPosCMD(cmd, 2);
						backCMD = cmd;
					}
				}
			}
			else
			{
				for (int j = 0; j < vec.size(); j++)
				{
					iCommand iCommand2 = (iCommand)vec.elementAt(j);
					if (j == 0)
					{
						center = iCommand2;
					}
					if (j == 1)
					{
						left = iCommand2;
					}
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			if (typeRebuild == 20)
			{
				vecCmd.addElement(cmdClose);
				left = cmdUpgrade;
				if (GameCanvas.isTouchNoOrPC())
				{
					backCMD = cmdClose;
					okCMD = cmdUpgrade;
				}
				return;
			}
			if (typeRebuild == 18 || typeRebuild == 22)
			{
				vecCmd.addElement(cmdClose);
				right = cmdUpgrade;
				if (GameCanvas.isTouchNoOrPC())
				{
					backCMD = cmdClose;
					okCMD = cmdUpgrade;
				}
				return;
			}
			vecCmd.addElement(cmdClose);
			vecCmd.addElement(cmdUpgrade);
			iCommand cmdShopMore = getCmdShopMore();
			if (cmdShopMore != null)
			{
				vecCmd.addElement(cmdShopMore);
			}
			if (GameCanvas.isTouchNoOrPC())
			{
				backCMD = cmdClose;
				okCMD = cmdUpgrade;
			}
		}
		else if (typeRebuild == 20)
		{
			left = cmdUpgrade;
		}
		else
		{
			right = cmdMenu;
			left = cmdUpgrade;
		}
	}

	public virtual iCommand getCmdShopMore()
	{
		return cmdShop;
	}

	public virtual bool isGetItemUpgrade(short Id, sbyte cat)
	{
		for (int i = 1; i < mItemUpgrade.Length; i++)
		{
			if (mItemUpgrade[i] != null && mItemUpgrade[i].ID == Id && mItemUpgrade[i].typeObject == cat)
			{
				return true;
			}
		}
		return false;
	}

	public virtual void updateNewUpgrade()
	{
		isShowInfo = false;
		getItemCurNew();
		if (mItemUpgrade[1] != null)
		{
			MainItem itemVec = MainItem.getItemVec(7, mItemUpgrade[1].ID, vecInventory);
			if (itemVec != null && itemVec.numPotion > 0)
			{
				GlobalService.gI().Upgrade_Item(5, itemVec.ID, 1);
			}
		}
		if (mItemUpgrade[2] != null)
		{
			MainItem itemVec2 = MainItem.getItemVec(7, mItemUpgrade[2].ID, vecInventory);
			if (itemVec2 != null && itemVec2.numPotion > 0)
			{
				GlobalService.gI().Upgrade_Item(6, itemVec2.ID, 1);
			}
		}
	}

	public virtual void setDataUpgrade()
	{
		if (mItemUpgrade[0] == null)
		{
			return;
		}
		MainDataUpgrade mainDataUpgrade = mTemMaterialUpgrade[mItemUpgrade[0].LvUpgrade];
		for (int i = 3; i < mItemUpgrade.Length; i++)
		{
			mItemUpgrade[i] = null;
		}
		int num = 3;
		for (int j = 0; j < mainDataUpgrade.mMaterial.Length; j++)
		{
			MainMaterial mainMaterial = (MainMaterial)MainItem.hashMaterialTem.get(string.Empty + mainDataUpgrade.mMaterial[j][0]);
			if (mItemUpgrade[0].colorName == mainDataUpgrade.mMaterial[j][2] || mainDataUpgrade.mMaterial[j][2] == -1)
			{
				sbyte color = 5;
				MainItem itemVec = MainItem.getItemVec(7, mainDataUpgrade.mMaterial[j][0], vecInventory);
				if (itemVec == null || itemVec.numPotion < mainDataUpgrade.mMaterial[j][1])
				{
					color = 6;
				}
				MainItem mainItem = new MainItem(7, mainMaterial.ID, mainMaterial.idIcon, mainDataUpgrade.mMaterial[j][1], color, 0);
				mItemUpgrade[num] = mainItem;
				num++;
			}
		}
	}

	public virtual void setStep()
	{
		if (Step == 1 || Step == 2)
		{
			if (tickStep % 15 == 0)
			{
				mSound.playSound(27, mSound.volumeSound);
			}
			tickStep++;
			updateEff();
			if (tickStep % 5 != 0)
			{
				return;
			}
			if (indexStep < mItemUpgrade.Length)
			{
				if (tickStep > 50)
				{
					indexStep = mItemUpgrade.Length;
				}
				if (typeRebuild == 20)
				{
					for (int i = indexStep; i < mItemUpgrade.Length - 1; i++)
					{
						if (mItemUpgrade[i] != null)
						{
							createEff(75, 0, posUp[i][0] + wItem / 2, posUp[i][1] + wItem / 2, posUp[mItemUpgrade.Length - 1][0] + wItem / 2, posUp[mItemUpgrade.Length - 1][1] + wItem / 2);
							mItemUpgrade[i].isPaint = false;
							indexStep = i + 1;
							tickStop = tickStep + 11 + ((w - lech * 3 - wInven) / 2 - 15) / 5;
							break;
						}
					}
					return;
				}
				for (int j = indexStep; j < mItemUpgrade.Length; j++)
				{
					if (mItemUpgrade[j] != null)
					{
						createEff(75, 0, posUp[j][0] + wItem / 2, posUp[j][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
						mItemUpgrade[j].isRemove = true;
						indexStep = j + 1;
						tickStop = tickStep + 11 + ((w - lech * 3 - wInven) / 2 - 15) / 5;
						break;
					}
				}
			}
			else
			{
				if (tickStep < tickStop)
				{
					return;
				}
				if (Step == 1)
				{
					mSound.playSound(28, mSound.volumeSound);
					int subtype = 0;
					if (GameCanvas.language == 1)
					{
						subtype = 2;
					}
					if (typeRebuild == 20)
					{
						createEff(79, subtype, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2);
						createEff(76, 0, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2);
					}
					else
					{
						createEff(79, subtype, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
						createEff(76, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
					}
					createEff(53, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
				}
				else if (Step == 2)
				{
					mSound.playSound(29, mSound.volumeSound);
					int subtype2 = 1;
					if (GameCanvas.language == 1)
					{
						subtype2 = 3;
					}
					if (typeRebuild == 20)
					{
						createEff(79, subtype2, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2);
						createEff(77, 0, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2, posUp[posUp.Length - 1][0] + wItem / 2, posUp[posUp.Length - 1][1] + wItem / 2);
					}
					else
					{
						createEff(79, subtype2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
						createEff(77, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
					}
				}
				Step = 3;
				tickStep = 0;
				tickStop = 20;
			}
		}
		else
		{
			if (Step != 3)
			{
				return;
			}
			tickStep++;
			if (tickStep < tickStop)
			{
				return;
			}
			Step = 0;
			tickStop = 0;
			tickStep = 0;
			indexStep = 1;
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(showServer);
			if (typeRebuild == 20)
			{
				indexStep = 0;
				for (int k = 0; k < mItemUpgrade.Length - 1; k++)
				{
					MainItem itemVec = MainItem.getItemVec(mItemUpgrade[k].typeObject, mItemUpgrade[k].ID, vecInventory);
					if (itemVec != null)
					{
						mItemUpgrade[k].numPotion = itemVec.numPotion;
					}
					else
					{
						mItemUpgrade[k].numPotion = 0;
					}
					mItemUpgrade[k].isPaint = true;
				}
			}
			else if (typeRebuild == 18)
			{
				GlobalService.gI().Upgrade_Dial(4, mItemUpgrade[0].ID, 0, 0);
				for (int l = 0; l < mItemUpgrade.Length; l++)
				{
					mItemUpgrade[l] = null;
				}
			}
			else if (typeRebuild == 15)
			{
				GlobalService.gI().Upgrade_Super_Item(4, mItemUpgrade[0].ID, 0, 0);
				for (int m = 0; m < mItemUpgrade.Length; m++)
				{
					mItemUpgrade[m] = null;
				}
			}
			else if (typeRebuild != 12)
			{
				GlobalService.gI().Upgrade_Item(4, mItemUpgrade[0].ID, 0);
				updateNewUpgrade();
			}
		}
	}

	public void updateEff()
	{
		if (typeRebuild == 20)
		{
			return;
		}
		if (typeRebuild == 15 || typeRebuild == 18 || typeRebuild == 22)
		{
			if (GameCanvas.gameTick % 15 == 0 || GameCanvas.gameTick % 15 == 5)
			{
				createEff(154, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
			}
			return;
		}
		if (GameCanvas.gameTick % 15 == 0)
		{
			for (int i = 1; i < posUp.Length; i++)
			{
				if (i == posUp.Length - 1)
				{
					createEff(73, i - 1, posUp[i][0] + wItem / 2, posUp[i][1] + wItem / 2, posUp[1][0] + wItem / 2, posUp[1][1] + wItem / 2);
				}
				else
				{
					createEff(73, i - 1, posUp[i][0] + wItem / 2, posUp[i][1] + wItem / 2, posUp[i + 1][0] + wItem / 2, posUp[i + 1][1] + wItem / 2);
				}
			}
		}
		if (GameCanvas.gameTick % 15 != 5)
		{
			return;
		}
		for (int j = 1; j < posUp.Length; j++)
		{
			if (j == posUp.Length - 1)
			{
				createEff(74, j - 1, posUp[j][0] + wItem / 2, posUp[j][1] + wItem / 2, posUp[1][0] + wItem / 2, posUp[1][1] + wItem / 2);
			}
			else
			{
				createEff(74, j - 1, posUp[j][0] + wItem / 2, posUp[j][1] + wItem / 2, posUp[j + 1][0] + wItem / 2, posUp[j + 1][1] + wItem / 2);
			}
		}
	}

	public void createEff(short type, int subtype, int x, int y, int xto, int yto)
	{
		Effect_End o = new Effect_End(type, (sbyte)subtype, x, y, xto, yto, 0, null);
		vecEff.addElement(o);
	}

	public void createEff(short type, int subtype, int x, int y, int time)
	{
		Effect_End o = new Effect_End(type, (sbyte)subtype, x, y, time, 0, null);
		vecEff.addElement(o);
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdClose != null)
		{
			cmdClose.perform();
		}
		return false;
	}

	public void updateKHAM()
	{
		if (Step == 1 || Step == 3)
		{
			tickStep++;
			if (tickStop == 0)
			{
				mSound.playSound(26, mSound.volumeSound);
				mSound.playSound(27, mSound.volumeSound);
				createEff(30, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, 1000);
				createEff(75, 0, posUp[1][0] + wItem / 2, posUp[1][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
				mItemUpgrade[1] = null;
				tickStop = tickStep + 11 + (w - lech * 3 - wInven) / 6 * 4 / 5;
			}
			else
			{
				if (tickStep < tickStop)
				{
					return;
				}
				if (typeRebuild == 8 || typeRebuild == 9 || typeRebuild == 13)
				{
					if (Step == 1)
					{
						mSound.playSound(28, mSound.volumeSound);
						int subtype = 0;
						if (GameCanvas.language == 1)
						{
							subtype = 2;
						}
						createEff(79, subtype, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
						createEff(76, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
						createEff(53, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
					}
					else if (Step == 3)
					{
						mSound.playSound(29, mSound.volumeSound);
						int subtype2 = 1;
						if (GameCanvas.language == 1)
						{
							subtype2 = 3;
						}
						createEff(79, subtype2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
						createEff(77, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
					}
				}
				else
				{
					mSound.playSound(26, mSound.volumeSound);
					createEff(53, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
				}
				Step = 2;
				tickStep = 0;
				tickStop = 0;
			}
		}
		else
		{
			if (Step != 2 && Step != 4)
			{
				return;
			}
			tickStep++;
			if (tickStep < 20)
			{
				return;
			}
			Step = 0;
			tickStep = 0;
			tickStop = 0;
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(showServer);
			if (typeRebuild == 8 || typeRebuild == 13)
			{
				mItemUpgrade[0] = null;
				mItemUpgrade[1] = null;
			}
			else if (typeRebuild == 9)
			{
				updateNewUpgrade();
				if (mItemUpgrade[0] != null)
				{
					GlobalService.gI().Devil_Upgrade(9, mItemUpgrade[0].ID, mItemUpgrade[0].typeObject, 1);
				}
				mItemUpgrade[0] = null;
				mItemUpgrade[1] = null;
			}
			else
			{
				if (mItemUpgrade[0] == null)
				{
					return;
				}
				MainItem itemVec = MainItem.getItemVec(mItemUpgrade[0].typeObject, mItemUpgrade[0].ID, vecInventory);
				if (typeRebuild == 10 || typeRebuild == 11)
				{
					if (itemVec != null && Step == 4)
					{
						GlobalService.gI().rebuild_Item(typeRebuild, 1, mItemUpgrade[0].ID, mItemUpgrade[0].typeObject, 1);
					}
					else
					{
						mItemUpgrade[0] = null;
					}
				}
				else if (itemVec != null && itemVec.mDaKham != null && itemVec.numLoKham > itemVec.mDaKham.Length)
				{
					GlobalService.gI().rebuild_Item(typeRebuild, 1, mItemUpgrade[0].ID, mItemUpgrade[0].typeObject, 1);
				}
				else
				{
					mItemUpgrade[0] = null;
				}
			}
		}
	}

	public void updatePointerSuper()
	{
		base.updatePointer();
	}

	public void setInfo_money(int valueTile, int valueMonney_1, int valueMonney_2, int valueMonney_3)
	{
		ScreenUpgrade.valueTile = valueTile;
		ScreenUpgrade.valueMonney_1 = valueMonney_1;
		ScreenUpgrade.valueMonney_2 = valueMonney_2;
		ScreenUpgrade.valueMonney_3 = valueMonney_3;
	}

	public void setInfo(MainItem[] listItem)
	{
		mItemUpgrade = listItem;
		indexStep = 0;
	}
}
