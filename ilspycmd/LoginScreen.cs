using System;

public class LoginScreen : MainScreen
{
	public sbyte type;

	public static bool isCheckData = false;

	public const sbyte LOGIN = 0;

	public const sbyte REGISTER = 1;

	public TField tfUser;

	public TField tfPass;

	public TField tfIp;

	private iCommand cmdLogin;

	private iCommand cmdMenu;

	private iCommand cmdRegister;

	private iCommand cmdExitGame;

	private iCommand cmdLowGraphic;

	private iCommand cmdFristLogin;

	private iCommand cmdHelp;

	private iCommand cmdLastLogin;

	private iCommand cmdDelRMS;

	private iCommand cmdOffBg;

	private iCommand cmdTaitho;

	public static iCommand cmdSound;

	private mVector vecCmd = new mVector();

	public static int yBeginPaint = 0;

	private int hItem = 32;

	private int idSelect;

	private int sizeListServer;

	private int yLechNameServer;

	private InputDialog input;

	public static mVector vecEff = new mVector("LoginScreen.vecEff");

	public static mVector vecOBJ = new mVector("LoginScreen.vecOBJ");

	private static bool isNewShow = false;

	private static bool isLoadDataCharShowOk = false;

	public static int yPaintLogo;

	public static int dy = 0;

	private int wShowPaper;

	private int maxWShow = 180;

	private int vShow = 6;

	private int hShowPaper = 85;

	public static int hLogo = 50;

	public static int frameBanhlai = 0;

	public static int tickBanhLai;

	public static short idRan = 0;

	public LoginScreen()
	{
		if (GameCanvas.isTouch)
		{
			hShowPaper = 120 + hItem + 2;
			hItem = 40;
			yLechNameServer = 4;
		}
		else
		{
			hShowPaper = 85 + hItem + 2;
		}
		sizeListServer = GameCanvas.strListServer[GameCanvas.language].Length;
		yBeginPaint = MotherCanvas.hh - hShowPaper / 2 + 23;
		tfUser = new TField(MotherCanvas.hw - 70, yBeginPaint + 17, 140);
		tfUser.setStringNull(T.tfUserNull);
		tfIp = new TField(MotherCanvas.hw - 70, yBeginPaint + 17 - hItem, 140);
		if (CRes.loadRMS("MAIN_ip_last") != null)
		{
			GameCanvas.strIP = SaveRms.loadIpLast().Trim();
			tfIp.setText(GameCanvas.strIP);
		}
		else
		{
			tfIp.setText("192.168.1.71");
		}
		tfPass = new TField(MotherCanvas.hw - 70, yBeginPaint + 17 + hItem, 140);
		tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
		tfPass.setStringNull(T.tfPassNull);
		cmdFristLogin = new iCommand(T.newGame, 7, this);
		cmdLogin = new iCommand(T.login, 0, this);
		cmdMenu = new iCommand(T.menu, 1, this);
		cmdRegister = new iCommand(T.register, 3, this);
		cmdExitGame = new iCommand(T.exit, 4, this);
		cmdLowGraphic = new iCommand(T.on + T.lowGraphic, 6, this);
		cmdLastLogin = new iCommand(T.loadGame, 12, this);
		cmdSound = new iCommand(T.amthanh, 13, this);
		cmdHelp = new iCommand(T.hotro, 10, this);
		cmdDelRMS = new iCommand(T.delRMS, 15, this);
		cmdOffBg = new iCommand(T.offBg, 18, this);
		if (GameCanvas.lowGraphic)
		{
			cmdLowGraphic.caption = T.off + T.lowGraphic;
		}
		cmdTaitho = new iCommand(T.on + T.taitho, 19, this);
		if (GameCanvas.isTaiTho)
		{
			cmdLowGraphic.caption = T.off + T.taitho;
		}
		cmdLogin = AvMain.setPosCMD(cmdLogin, 0);
		cmdRegister = AvMain.setPosCMD(cmdRegister, 0);
		cmdMenu = AvMain.setPosCMD(cmdMenu, 1);
		if (GameCanvas.isTouch)
		{
			if (GameCanvas.isTaiTho)
			{
				cmdMenu.setPos(30, MotherCanvas.h - 15, AvMain.fraIconMenu, string.Empty);
				cmdHelp.setPos(MotherCanvas.w - 30, MotherCanvas.h - 15, AvMain.fraIconHome, string.Empty);
			}
			else
			{
				cmdMenu.setPos(15, MotherCanvas.h - 15, AvMain.fraIconMenu, string.Empty);
				cmdHelp.setPos(MotherCanvas.w - 15, MotherCanvas.h - 15, AvMain.fraIconHome, string.Empty);
			}
			right = cmdHelp;
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			tfUser.setFocus(isFocus: true);
		}
		if (GameCanvas.isTouch)
		{
			cmdLogin.setPos(MotherCanvas.hw, yBeginPaint + hShowPaper, AvMain.fraBtLogin, string.Empty);
			cmdLogin.isPlayframe = true;
			cmdRegister.setPosXY(MotherCanvas.hw, yBeginPaint + hShowPaper - iCommand.hButtonCmdNor + 8);
		}
		left = cmdMenu;
		menuCMD = cmdMenu;
		setCmdClear();
		sbyte[] array = GameMidlet.loadRMS("Main_IPNEW");
		if (array != null)
		{
			try
			{
				ByteArrayInputStream bis = new ByteArrayInputStream(array);
				DataInputStream dataInputStream = new DataInputStream(bis);
				GameCanvas.strIP = dataInputStream.readUTF();
				dataInputStream.close();
			}
			catch (Exception)
			{
			}
		}
		if (yPaintLogo == 0)
		{
			yPaintLogo = hLogo;
		}
	}

	public override void Show(MainScreen last)
	{
		Show();
	}

	public override void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public override void Show()
	{
		if (GameScreen.vecHelp != null)
		{
			GameScreen.vecHelp.removeAllElements();
		}
		if (GameCanvas.mapBack == null)
		{
			GameCanvas.mapBack = new MapBackGround();
		}
		GameCanvas.mapBack.setBGLogin();
		GameScreen.player = null;
		Session_ME.gI().close();
		type = 0;
		setCmd();
		wShowPaper = 5;
		if (GameCanvas.currentScreen != null && GameCanvas.currentScreen != GameCanvas.loginScr && GameCanvas.currentScreen != GameCanvas.fristLoginScr)
		{
			beginShowChar();
		}
		loadCharPart();
		base.Show();
		mSound.playMus(3, mSound.volumeMusic, loop: true);
		isNewShow = true;
	}

	public static void beginShowChar()
	{
		vecOBJ.removeAllElements();
		vecEff.removeAllElements();
	}

	public static void loadCharPart()
	{
	}

	public void setCmd()
	{
		if (type == 0)
		{
			center = cmdLogin;
		}
		else
		{
			center = cmdRegister;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (GameCanvas.istest)
			{
				GameCanvas.strIP = tfIp.getText().Trim();
				GameCanvas.saveRms.saveIpLast(GameCanvas.strIP);
				mSystem.outz("connect ip " + GameCanvas.strIP);
			}
			if (tfUser.getText().Trim().Length > 0 && tfPass.getText().Trim().Length > 0)
			{
				ListChar_Screen.IndexCharSelected = -1;
				mSystem.outz("6");
				doLogin(isGetData: true, 0, tfUser.getText(), tfPass.getText());
			}
			else
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.checkRegister1);
			}
			break;
		case 1:
		{
			mVector mVector2 = new mVector();
			SaveRms.userLast = string.Empty;
			GameCanvas.saveRms.loadUserLast();
			if (SaveRms.userLast.Length > 0 && CRes.loadRMS("MAIN_user_pass") == null)
			{
				cmdLastLogin.caption = T.loadGame + " " + SaveRms.userLast;
				mVector2.addElement(cmdLastLogin);
			}
			mVector2.addElement(cmdFristLogin);
			if (GameMidlet.DEVICE == 0 && !GameCanvas.isSuperLowGraphic)
			{
				mVector2.addElement(cmdLowGraphic);
			}
			if (!GameCanvas.isTouch)
			{
				mVector2.addElement(cmdHelp);
			}
			mVector2.addElement(cmdSound);
			cmdOffBg.caption = T.on + T.offBg;
			if (!GameCanvas.isOffBg)
			{
				cmdOffBg.caption = T.off + T.offBg;
			}
			mVector2.addElement(cmdOffBg);
			if (GameMidlet.DEVICE == 2 || GameMidlet.DEVICE == 4)
			{
				mVector2.addElement(cmdDelRMS);
			}
			else if (GameCanvas.isTouch)
			{
				cmdTaitho.caption = T.on + T.taitho;
				if (GameCanvas.isTaiTho)
				{
					cmdTaitho.caption = T.off + T.taitho;
				}
				mVector2.addElement(cmdTaitho);
			}
			if (GameMidlet.DEVICE == 0 || GameCanvas.isAndroid())
			{
				mVector2.addElement(cmdExitGame);
			}
			if (GameScreen.isIP_Local)
			{
				mVector2.addElement(new iCommand("NHAP IP", 8, this));
				mVector2.addElement(new iCommand("VŨ ĐỆ 146 port 23", 17, this));
			}
			if (GameCanvas.strIP.Length > 0)
			{
				mVector2.addElement(new iCommand("XOA IP", 14, this));
			}
			GameCanvas.menu.startAt(mVector2, 0, T.menu);
			break;
		}
		case 2:
			if (type == 0)
			{
				type = 1;
			}
			else
			{
				type = 0;
			}
			setCmd();
			break;
		case 3:
			if (tfUser.getText().Trim().Length > 0 && tfPass.getText().Trim().Length > 0)
			{
				GameCanvas.connect();
				GlobalService.gI().Register(tfUser.getText(), tfPass.getText());
				GameCanvas.clearAll();
			}
			else
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.checkRegister1);
			}
			break;
		case 4:
			GameCanvas.Start_Normal_DiaLog(T.hoiThoat, new iCommand(T.exit, 5, this), isCmdClose: true);
			break;
		case 5:
			GameMidlet.instance.destroy();
			break;
		case 6:
			GameCanvas.lowGraphic = !GameCanvas.lowGraphic;
			cmdLowGraphic.caption = T.on + T.lowGraphic;
			if (GameCanvas.lowGraphic)
			{
				cmdLowGraphic.caption = T.off + T.lowGraphic;
			}
			try
			{
				CRes.saveRMS("SUB_LOWGRAPHIC", new sbyte[1] { (sbyte)(GameCanvas.lowGraphic ? 1 : 0) });
			}
			catch (Exception)
			{
			}
			LoadImageStatic.LoadLowGraphic();
			break;
		case 7:
			ListChar_Screen.IndexCharSelected = -1;
			GameCanvas.loginScr.doLogin(isGetData: true, 1, string.Empty, string.Empty);
			break;
		case 8:
			input = new InputDialog();
			input.setinfo("NHAP IP", new iCommand("OK", 9, this), isNum: false, "IP-PORT");
			GameCanvas.currentDialog = input;
			break;
		case 9:
			GameCanvas.strIP = input.tfInput.getText().Trim();
			saveIP_New();
			break;
		case 10:
		{
			mVector mVector3 = new mVector();
			mVector3.addElement(new iCommand(T.home, 11, 0, this));
			mVector3.addElement(new iCommand(T.forum, 11, 1, this));
			mVector3.addElement(new iCommand(T.fanpage, 11, 4, this));
			mVector3.addElement(new iCommand(T.changePassword, 11, 2, this));
			mVector3.addElement(new iCommand(T.getPassword, 11, 3, this));
			if (GameMidlet.DEVICE == 6)
			{
				mVector3.addElement(new iCommand(T.rate, 11, 5, this));
			}
			GameCanvas.menu.startAt(mVector3, 2, T.hotro);
			break;
		}
		case 11:
		{
			string empty = string.Empty;
			if (GameCanvas.language == 1)
			{
				switch (subIndex)
				{
				case 0:
					empty = "http://world.teamobi.com";
					break;
				case 1:
					empty = "http://world.teamobi.com";
					break;
				case 2:
					empty = "http://world.teamobi.com/community/change-password.html";
					break;
				case 3:
					empty = "http://world.teamobi.com/community/forgot-password.html";
					break;
				case 4:
					empty = "https://www.facebook.com/haitactihon.teamobi";
					break;
				case 5:
					empty = "itms-apps://itunes.apple.com/app/1212748675";
					break;
				}
			}
			else
			{
				switch (subIndex)
				{
				case 0:
					empty = "http://haitactihon.com/index.php";
					break;
				case 1:
					empty = "http://haitactihon.com/forum/game/5/hai-tact-ti-hon-0.html";
					break;
				case 2:
					empty = "http://haitactihon.com/forum/change-password.html";
					break;
				case 3:
					empty = "http://haitactihon.com/forum/forgot-password.html";
					break;
				case 4:
					empty = "https://www.facebook.com/haitactihon.teamobi";
					break;
				case 5:
					empty = "itms-apps://itunes.apple.com/app/1068798626";
					break;
				}
			}
			break;
		}
		case 12:
			GameCanvas.fristLoginScr.cmdBegin.perform();
			break;
		case 13:
		{
			MsgSound msgSound = new MsgSound();
			msgSound.setinfoSound();
			GameCanvas.Start_Current_Dialog(msgSound);
			break;
		}
		case 14:
			GameCanvas.strIP = string.Empty;
			GameMidlet.delRMS("Main_IPNEW");
			break;
		case 15:
			GameCanvas.Start_Normal_DiaLog(T.closeDelRMS, new iCommand(T.del, 16, this), isCmdClose: true);
			break;
		case 16:
			isCheckData = false;
			GameMidlet.delAllRms();
			GameCanvas.end_Dialog();
			if (GameMidlet.DEVICE == 2)
			{
				GameCanvas.updateImageAndroidScr = new UpdateImageScreen();
				GameCanvas.updateImageAndroidScr.Show();
			}
			else
			{
				setData();
			}
			break;
		case 17:
			GameCanvas.strIP = "192.168.1.146-20123";
			saveIP_New();
			break;
		case 18:
			GameCanvas.isOffBg = !GameCanvas.isOffBg;
			try
			{
				CRes.saveRMS("SUB_OFFBG", new sbyte[1] { (sbyte)(GameCanvas.isOffBg ? 1 : 0) });
			}
			catch (Exception)
			{
			}
			break;
		case 19:
			GameCanvas.isTaiTho = !GameCanvas.isTaiTho;
			try
			{
				CRes.saveRMS("SUB_TAITHO", new sbyte[1] { (sbyte)(GameCanvas.isTaiTho ? 1 : 0) });
			}
			catch (Exception)
			{
			}
			GameScreen.interfaceGame.setPosMenu_TaiTho();
			break;
		}
		base.commandPointer(index, subIndex);
	}

	private void saveIP_New()
	{
		if (GameCanvas.strIP.Length == 0)
		{
			GameMidlet.delRMS("Main_IPNEW");
			return;
		}
		string[] array = mFont.split(GameCanvas.strIP, "-");
		GameCanvas.Start_Normal_Only_CmdClose_DiaLog("IP:" + array[0] + "\nPORT:" + array[1]);
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeUTF(GameCanvas.strIP);
			GameMidlet.saveRMS("Main_IPNEW", byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public override void paint(mGraphics g)
	{
		try
		{
			if (GameCanvas.mapBack != null)
			{
				GameCanvas.mapBack.paintBgLogin(g);
				GameCanvas.mapBack.paintObjFristLogin(g);
				GameCanvas.mapBack.paintObjLastLogin(g);
			}
		}
		catch (Exception)
		{
		}
		paintShowchar(g);
		paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yBeginPaint, wShowPaper, hShowPaper, maxWShow, AvMain.PAPER_NORMAL);
		GameCanvas.resetTrans(g);
		paintLogo(g, MotherCanvas.hw);
		mFont.tahoma_7_black.drawString(g, "Ver: 1.2.4", MotherCanvas.w - 2, 2 + GameScreen.h12plus, 1);
		mFont.tahoma_7_black.drawString(g, "No: 1", MotherCanvas.w - 2, 4 + GameScreen.h12plus + GameCanvas.hText / 2, 1);
		GameCanvas.resetTrans(g);
		g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		if (GameCanvas.istest)
		{
			tfIp.paint(g, MotherCanvas.hw - wShowPaper / 2 < tfUser.x);
			g.restoreCanvas();
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
			g.saveCanvas();
			g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		}
		tfUser.paint(g, MotherCanvas.hw - wShowPaper / 2 < tfUser.x);
		g.restoreCanvas();
		g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		tfPass.paint(g, MotherCanvas.hw - wShowPaper / 2 < tfPass.x);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		paintListServer(g);
		if (idSelect != 2 && idSelect != -2)
		{
			if (GameCanvas.isTouch && !GameCanvas.lowGraphic)
			{
				AvMain.fraBtBanhlai.drawFrame(frameBanhlai, cmdLogin.xCmd, cmdLogin.yCmd, 0, 3, g);
			}
			base.paint(g);
		}
	}

	public static void paintLogo(mGraphics g, int x)
	{
		if (AvMain.imgLg == null)
		{
			LoadImageStatic.loadImageLanguage();
		}
		g.drawImage(AvMain.imgLg, x, yPaintLogo, 3);
		if (GameCanvas.language == 0 && !GameCanvas.lowGraphic && !GameCanvas.isDeviceStore() && GameCanvas.IndexServer < AvMain.fraIconServer.nFrame)
		{
			if (MotherCanvas.h >= 240)
			{
				AvMain.fraIconServer.drawFrame(GameCanvas.IndexServer, x + 35, yPaintLogo + 20, 0, 3, g);
			}
			else
			{
				Interface_Game.fraBorderNoti.drawFrame(GameCanvas.IndexServer + 1, x + 34, yPaintLogo + 17, 0, 3, g);
			}
		}
	}

	public static void updateYPaintLogo(int y)
	{
		if (yPaintLogo > y)
		{
			yPaintLogo -= 2;
			if (yPaintLogo < y)
			{
				yPaintLogo = y;
			}
		}
		else if (yPaintLogo < y)
		{
			yPaintLogo += 2;
			if (yPaintLogo > y)
			{
				yPaintLogo = y;
			}
		}
	}

	public void paintListServer(mGraphics g)
	{
		if (sizeListServer > 2 && dy == 0)
		{
			dy = -15 * sizeListServer;
		}
		if (idSelect != 2)
		{
			AvMain.paintRect(g, tfPass.x, tfPass.y + hItem, tfPass.width, tfPass.height, 0, (idSelect != -2) ? 1 : 0);
			mFont.tahoma_7b_black.drawString(g, T.server + " " + GameCanvas.strListServer[GameCanvas.language][GameCanvas.IndexServer], tfPass.x + 4, tfPass.y + tfPass.height - 16 + hItem - yLechNameServer, 0);
			g.drawRegion(AvMain.imgArrowListServer, 0, ((idSelect != -2) ? 1 : 0) * 10, 15, 10, 0, tfPass.x + tfPass.width - 12, tfPass.y + hItem + tfPass.height / 2, 3);
			return;
		}
		AvMain.paintRect(g, tfPass.x, tfPass.y + hItem + dy, tfPass.width, tfPass.height * sizeListServer - 1, 0, 0);
		for (int i = 0; i < sizeListServer; i++)
		{
			if (i > 0)
			{
				g.setColor(12609544);
				g.drawRect(tfPass.x + 6, tfPass.y + hItem + i * tfPass.height - 1 + dy, tfPass.width - 12, 1);
			}
			if (GameCanvas.isTouchNoOrPC() && i == GameCanvas.IndexServer)
			{
				g.setColor(14194752);
				g.fillRect(tfPass.x + 3, tfPass.y + hItem + GameCanvas.IndexServer * tfPass.height + 3 + dy, tfPass.width - 4, tfPass.height - 6);
			}
			string text = T.server + " " + GameCanvas.strListServer[GameCanvas.language][i];
			if (i > 0 && i == GameCanvas.strListServer[GameCanvas.language].Length - 1)
			{
				int width = mFont.tahoma_7b_black.getWidth(text);
				AvMain.fraNew.drawFrame(GameCanvas.gameTick / 5 % AvMain.fraNew.nFrame, tfPass.x + 4 + width + 16, tfPass.y + tfPass.height - 16 + hItem + i * tfPass.height - yLechNameServer + 4 + dy, 0, 3, g);
			}
			mFont.tahoma_7b_black.drawString(g, text, tfPass.x + 4, tfPass.y + tfPass.height - 16 + hItem + i * tfPass.height - yLechNameServer + dy, 0);
		}
	}

	public static void paintShowchar(mGraphics g)
	{
		CRes.quickSort(vecOBJ);
		for (int i = 0; i < vecOBJ.size(); i++)
		{
			MainObject mainObject = (MainObject)vecOBJ.elementAt(i);
			mainObject.paint(g);
			mainObject.ySort = mainObject.y;
		}
		for (int j = 0; j < vecEff.size(); j++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(j);
			mainEffect.paint(g);
		}
	}

	public static void updateCharShow()
	{
		for (int i = 0; i < vecOBJ.size(); i++)
		{
			MainObject mainObject = (MainObject)vecOBJ.elementAt(i);
			mainObject.updateLoginShow();
			if (mainObject.isRemove)
			{
				vecOBJ.removeElement(mainObject);
				i--;
			}
		}
		for (int j = 0; j < vecEff.size(); j++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(j);
			mainEffect.update();
			if (mainEffect.isStop)
			{
				vecEff.removeElement(mainEffect);
				j--;
			}
		}
		if (CRes.random(20) == 0 && vecOBJ.size() < MotherCanvas.w / 80)
		{
			addObjShow(isNew: false);
		}
		if (isNewShow)
		{
			isNewShow = false;
			for (int k = 0; k < 3; k++)
			{
				addObjShow(isNew: true);
			}
		}
	}

	public override void update()
	{
		updateYPaintLogo(yBeginPaint / 2);
		updateBanhLai();
		if (wShowPaper < maxWShow)
		{
			wShowPaper += vShow;
			if (wShowPaper > maxWShow)
			{
				wShowPaper = maxWShow;
				vShow = 15;
			}
			if (vShow < 100)
			{
				vShow += 15;
				if (vShow > 100)
				{
					vShow = 100;
				}
			}
		}
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.updateCloudLogin();
		}
		tfIp.update();
		tfUser.update();
		tfPass.update();
		updateCharShow();
		if (MsgDialog.isAuroReconect && GameCanvas.gameTick % 100 == 0 && (GameCanvas.currentDialog == null || GameCanvas.currentDialog.type != 9))
		{
			string info = T.disconnect;
			if (GameCanvas.infoDisConnect != null && GameCanvas.infoDisConnect.Length > 10)
			{
				info = GameCanvas.infoDisConnect;
				GameCanvas.infoDisConnect = string.Empty;
			}
			mVector mVector2 = new mVector();
			mVector2.addElement(GameScreen.cmdReConnect);
			mVector2.addElement(GameCanvas.gameScr.cmdExit);
			mSystem.outz("start here");
			GameCanvas.Start_ReConect_DiaLog(info, mVector2, isCmdClose: false);
		}
	}

	public static void updateBanhLai()
	{
		int num = CRes.random(20);
		if (tickBanhLai < 40)
		{
			tickBanhLai++;
			if (tickBanhLai == 40)
			{
				if (frameBanhlai != 1)
				{
					frameBanhlai = 1;
				}
				tickBanhLai = 0;
			}
		}
		if (num == 0)
		{
			if (frameBanhlai == 0 || frameBanhlai == 2)
			{
				frameBanhlai = 1;
			}
			else if (CRes.random(2) == 0)
			{
				frameBanhlai = 0;
			}
			else
			{
				frameBanhlai = 2;
			}
			tickBanhLai = 0;
		}
	}

	public override void updatekey()
	{
		if (idSelect == -2)
		{
			if (GameCanvas.keyMyHold[5])
			{
				idSelect = 2;
				GameCanvas.clearKeyPressed(5);
				GameCanvas.clearKeyHold(5);
			}
		}
		else if (idSelect == 2 && GameCanvas.keyMyHold[5])
		{
			idSelect = -2;
			GameCanvas.clearKeyPressed(5);
			GameCanvas.clearKeyHold(5);
		}
		if (GameCanvas.keyMyHold[8])
		{
			if (idSelect == 2)
			{
				if (GameCanvas.IndexServer < sizeListServer - 1)
				{
					setIndexServer(GameCanvas.IndexServer + 1);
				}
			}
			else if (idSelect == -2)
			{
				if (GameCanvas.isTouchNoOrPC())
				{
					tfUser.setFocus(isFocus: true);
				}
				tfPass.setFocus(isFocus: false);
				idSelect = 0;
			}
			else if (tfUser.isFocused())
			{
				tfUser.setFocus(isFocus: false);
				if (GameCanvas.isTouchNoOrPC())
				{
					tfPass.setFocus(isFocus: true);
				}
			}
			else if (tfPass.isFocused())
			{
				tfUser.setFocus(isFocus: false);
				tfPass.setFocus(isFocus: false);
				idSelect = -2;
			}
			GameCanvas.clearKeyHold(8);
			setCmdClear();
		}
		else if (GameCanvas.keyMyHold[2])
		{
			if (idSelect == 2)
			{
				if (GameCanvas.IndexServer > 0)
				{
					setIndexServer(GameCanvas.IndexServer - 1);
				}
			}
			else if (idSelect == -2)
			{
				tfUser.setFocus(isFocus: false);
				if (GameCanvas.isTouchNoOrPC())
				{
					tfPass.setFocus(isFocus: true);
				}
				idSelect = 0;
			}
			else if (tfUser.isFocused())
			{
				tfUser.setFocus(isFocus: false);
				tfPass.setFocus(isFocus: false);
				idSelect = -2;
			}
			else if (tfPass.isFocused())
			{
				if (GameCanvas.isTouchNoOrPC())
				{
					tfUser.setFocus(isFocus: true);
				}
				tfPass.setFocus(isFocus: false);
			}
			GameCanvas.clearKeyHold(2);
			setCmdClear();
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		if (idSelect == 2)
		{
			for (int i = 0; i < sizeListServer; i++)
			{
				if (GameCanvas.isPointSelect(tfPass.x, tfPass.y + hItem + i * tfPass.height + dy, tfPass.width, tfPass.height))
				{
					setIndexServer(i);
					idSelect = -1;
					GameCanvas.isPointerSelect = false;
				}
			}
			if (GameCanvas.isPointSelect(0, 0, MotherCanvas.w, MotherCanvas.h))
			{
				idSelect = -1;
				GameCanvas.isPointerSelect = false;
			}
		}
		else if (GameCanvas.isPointSelect(tfPass.x, tfPass.y + hItem, tfPass.width, tfPass.height))
		{
			idSelect = 2;
			GameCanvas.isPointerSelect = false;
		}
		base.updatePointer();
		tfUser.updatePointer();
		tfIp.updatePointer();
		tfPass.updatePointer();
	}

	public void setIndexServer(int index)
	{
		if (index >= 0 && index < sizeListServer)
		{
			GameCanvas.IndexServer = index;
			if (Session_ME.gI().isConnected())
			{
				Session_ME.gI().close();
			}
		}
	}

	public void doLogin(bool isGetData, sbyte typeLogin, string user, string pass)
	{
		GameCanvas.connect();
		if (isGetData)
		{
			setData();
		}
		if (!isCheckData)
		{
			GlobalService.gI().CheckVersion();
			GameCanvas.Start_Waiting_Connect_DiaLog(T.checkVersion, isCmdClose: false);
		}
		else
		{
			GlobalService.gI().Login(user, pass, typeLogin);
			GameCanvas.Start_Waiting_Connect_DiaLog(T.doingLogin, isCmdClose: true);
			if (typeLogin >= 0)
			{
				Player.StepAutoRe = 3;
			}
		}
		if (GameMidlet.DEVICE == 4)
		{
			ObjectData.checkDelHash(ObjectData.HashImageCharPart, 120, isTrue: true);
		}
	}

	public override void keyPress(int keyCode)
	{
		if (tfUser.isFocused())
		{
			tfUser.keyPressed(keyCode);
		}
		else if (tfPass.isFocused())
		{
			tfPass.keyPressed(keyCode);
		}
	}

	public void setData()
	{
		GlobalService.gI().get_DATA(4);
		GlobalService.gI().get_DATA(10);
		GlobalService.gI().get_DATA(8);
		GlobalService.gI().get_DATA(21);
		GlobalService.gI().get_DATA(30);
		if (!GlobalService.isGetKichAn)
		{
			GlobalService.gI().get_DATA(26);
		}
		if (!GlobalService.isGetMaterial)
		{
			GlobalService.gI().get_DATA(11);
		}
		if (LoadMap.mSea == null)
		{
			GlobalService.gI().get_DATA(13);
		}
		if (ScreenUpgrade.mItemUpgrade == null)
		{
			GlobalService.gI().get_DATA(19);
		}
		if (GameCanvas.clockServer == 0)
		{
			GlobalService.gI().get_DATA(17);
		}
	}

	public void setCmdClear()
	{
		if (!GameCanvas.isTouch)
		{
			if (tfUser.isFocused())
			{
				right = tfUser.cmdClear;
			}
			else if (tfPass.isFocused())
			{
				right = tfPass.cmdClear;
			}
		}
	}

	public static void addObjShow(bool isNew)
	{
		if (isLoadDataCharShowOk && !GameCanvas.lowGraphic)
		{
			idRan++;
			Other_Player other_Player = new Other_Player(idRan, 0, string.Empty, 0, 0);
			if (CRes.random(2) == 0)
			{
				other_Player.x = -CRes.random(10, 40);
				other_Player.Dir = 2;
			}
			else
			{
				other_Player.x = MotherCanvas.w + CRes.random(10, 40);
				other_Player.Dir = 0;
			}
			if (isNew)
			{
				other_Player.x = CRes.random(10, MotherCanvas.w - 10);
			}
			other_Player.setSpeed(7, 7);
			other_Player.y = MotherCanvas.h - CRes.random(10, 50);
			other_Player.xAnchor = other_Player.x;
			other_Player.toX = other_Player.x;
			other_Player.toY = other_Player.y;
			other_Player.clazz = (sbyte)(1 + CRes.random(5));
			other_Player = CreateChar_Screen.setCharClass(other_Player, isRan: true);
			other_Player.isInfo = true;
			vecOBJ.addElement(other_Player);
		}
	}

	public static void addEffectEnd(short type, int subtype, int x, int y, sbyte dir, MainObject objEff)
	{
		Effect_End o = new Effect_End(type, (sbyte)subtype, x, y, dir, objEff);
		vecEff.addElement(o);
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdExitGame != null && GameMidlet.DEVICE != 4)
		{
			cmdExitGame.perform();
		}
		return false;
	}
}
