using System;

public class FristLoginScreen : MainScreen
{
	public iCommand cmdBegin;

	public iCommand cmdChangeAcc;

	public iCommand cmdServer;

	public iCommand cmdNewGame;

	private mVector vecCmd = new mVector();

	public static string userNew = string.Empty;

	public static InputDialog input;

	public static iCommand cmdRegister;

	private int valueBegin;

	private int idCommand;

	public FristLoginScreen()
	{
		cmdBegin = new iCommand(T.loadGame, 0, 0, this);
		SaveRms.userLast = string.Empty;
		if (CRes.loadRMS("MAIN_user_last") != null)
		{
			GameCanvas.saveRms.loadUserLast();
		}
		else
		{
			GameCanvas.IndexServer = GameCanvas.strListServer[GameCanvas.language].Length - 1;
		}
		if (GameCanvas.IndexServer >= GameCanvas.strListServer[GameCanvas.language].Length)
		{
			GameCanvas.IndexServer = GameCanvas.strListServer[GameCanvas.language].Length - 1;
		}
		cmdRegister = new iCommand(T.register, 3, this);
		cmdServer = new iCommand(T.server + "\n" + GameCanvas.strListServer[GameCanvas.language][GameCanvas.IndexServer], 4, this);
		valueBegin = (GameCanvas.strListServer[GameCanvas.language].Length - 1) * 38;
		getVecBegin();
		if (LoginScreen.yPaintLogo == 0)
		{
			LoginScreen.yPaintLogo = LoginScreen.hLogo;
		}
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
		idCommand = 0;
		if (!GameCanvas.isTouch || GameCanvas.isTouchAndKey())
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				if (i == idCommand)
				{
					iCommand2.isSelect = true;
				}
				else
				{
					iCommand2.isSelect = false;
				}
			}
		}
		if (GameCanvas.currentScreen != null && GameCanvas.currentScreen != GameCanvas.loginScr && GameCanvas.currentScreen != GameCanvas.fristLoginScr)
		{
			LoginScreen.beginShowChar();
		}
		LoginScreen.loadCharPart();
		getVecBegin();
		base.Show();
		mSound.playMus(3, mSound.volumeMusic, loop: true);
	}

	public void resetPos()
	{
		getVecBegin();
	}

	public override void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public void setBeginGame()
	{
		if (CRes.loadRMS("MAIN_user_pass") != null)
		{
			GameCanvas.loginScr.Show();
			GameCanvas.loginScr.doLogin(isGetData: true, 0, GameCanvas.loginScr.tfUser.getText(), GameCanvas.loginScr.tfPass.getText());
		}
		else
		{
			setNewAcc(isCheckDataOK: false);
			getVecBegin();
		}
	}

	public void setNewAcc(bool isCheckDataOK)
	{
		sbyte[] array = CRes.loadRMS("MAIN_frist_login");
		if (array == null && !isCheckDataOK)
		{
			return;
		}
		ListChar_Screen.IndexCharSelected = -1;
		userNew = string.Empty;
		try
		{
			DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(array));
			userNew = dataInputStream.readUTF();
			if (dataInputStream.available() > 0)
			{
				GameCanvas.IndexServer = dataInputStream.readByte();
			}
		}
		catch (Exception)
		{
			userNew = string.Empty;
		}
		mSystem.outz("4");
		GameCanvas.loginScr.doLogin(isGetData: true, 1, userNew, string.Empty);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
		{
			ListChar_Screen.IndexCharSelected = -1;
			userNew = string.Empty;
			sbyte[] array = CRes.loadRMS("MAIN_frist_login");
			if (array != null)
			{
				try
				{
					DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(array));
					userNew = dataInputStream.readUTF();
					if (dataInputStream.available() > 0)
					{
						GameCanvas.IndexServer = dataInputStream.readByte();
					}
				}
				catch (Exception)
				{
					userNew = string.Empty;
				}
			}
			GameCanvas.loginScr.doLogin(isGetData: true, 1, userNew, string.Empty);
			break;
		}
		case 1:
			GameCanvas.loginScr.Show();
			break;
		case 2:
			GameCanvas.loginScr.doLogin(isGetData: true, 1, string.Empty, string.Empty);
			break;
		case 3:
		{
			string[] array2 = new string[input.mtfInput.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				if (input.mtfInput[j].getText().Length > 0)
				{
					array2[j] = input.mtfInput[j].getText();
				}
				else
				{
					array2[j] = string.Empty;
				}
			}
			GlobalService.gI().RegisterNew(array2);
			break;
		}
		case 4:
		{
			if (GameCanvas.strListServer[GameCanvas.language].Length == 1)
			{
				return;
			}
			vecCmd.removeAllElements();
			for (int i = 0; i < GameCanvas.strListServer[GameCanvas.language].Length; i++)
			{
				iCommand iCommand2 = new iCommand(T.server + "\n" + GameCanvas.strListServer[GameCanvas.language][i], 5, i, this);
				iCommand2.setPos(MotherCanvas.hw - valueBegin + i * 76, MotherCanvas.h - 60, null, iCommand2.caption);
				iCommand2.setTypeSpec();
				vecCmd.addElement(iCommand2);
			}
			break;
		}
		case 5:
			GameCanvas.IndexServer = subIndex;
			getVecBegin();
			break;
		}
		base.commandPointer(index, subIndex);
	}

	private void getVecBegin()
	{
		vecCmd.removeAllElements();
		if (GameCanvas.language > GameCanvas.strListServer.Length - 1)
		{
			GameCanvas.language = 0;
		}
		if (GameCanvas.IndexServer > GameCanvas.strListServer[GameCanvas.language].Length - 1)
		{
			GameCanvas.IndexServer = GameCanvas.strListServer[GameCanvas.language].Length - 1;
		}
		if (SaveRms.userLast.Length > 0)
		{
			cmdBegin.caption = T.loadGame + "\n " + SaveRms.userLast;
			cmdBegin.setPos(MotherCanvas.hw - 38, MotherCanvas.h - 98, null, cmdBegin.caption);
			cmdBegin.setTypeSpec();
			vecCmd.addElement(cmdBegin);
			cmdNewGame = new iCommand(T.newGame, 2, 0, this);
			cmdNewGame.setPos(MotherCanvas.hw + 38, MotherCanvas.h - 98, null, cmdNewGame.caption);
			cmdNewGame.setTypeSpec();
			vecCmd.addElement(cmdNewGame);
			cmdChangeAcc = new iCommand(T.changeAcc, 1, 0, this);
			cmdChangeAcc.setPos(MotherCanvas.hw - 38, MotherCanvas.h - 46, null, cmdChangeAcc.caption);
			cmdChangeAcc.setTypeSpec();
			vecCmd.addElement(cmdChangeAcc);
			cmdServer.caption = T.server + "\n" + GameCanvas.strListServer[GameCanvas.language][GameCanvas.IndexServer];
			cmdServer.setPos(MotherCanvas.hw + 38, MotherCanvas.h - 46, null, cmdServer.caption);
			cmdServer.setTypeSpec();
			vecCmd.addElement(cmdServer);
		}
		else
		{
			cmdNewGame = new iCommand(T.newGame, 2, 0, this);
			cmdNewGame.setPos(MotherCanvas.hw - 76, MotherCanvas.h - 60, null, cmdNewGame.caption);
			cmdNewGame.setTypeSpec();
			vecCmd.addElement(cmdNewGame);
			cmdChangeAcc = new iCommand(T.changeAcc, 1, 0, this);
			cmdChangeAcc.setPos(MotherCanvas.hw, MotherCanvas.h - 60, null, cmdChangeAcc.caption);
			cmdChangeAcc.setTypeSpec();
			vecCmd.addElement(cmdChangeAcc);
			cmdServer.caption = T.server + "\n" + GameCanvas.strListServer[GameCanvas.language][GameCanvas.IndexServer];
			cmdServer.setPos(MotherCanvas.hw + 76, MotherCanvas.h - 60, null, cmdServer.caption);
			cmdServer.setTypeSpec();
			vecCmd.addElement(cmdServer);
		}
		idCommand = 0;
		if (GameCanvas.isTouch && !GameCanvas.isTouchAndKey())
		{
			return;
		}
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			if (i == idCommand)
			{
				iCommand2.isSelect = true;
			}
			else
			{
				iCommand2.isSelect = false;
			}
		}
	}

	public override void paint(mGraphics g)
	{
		if (GameCanvas.mapBack != null)
		{
			GameCanvas.mapBack.paintBgLogin(g);
			GameCanvas.mapBack.paintObjFristLogin(g);
			GameCanvas.mapBack.paintObjLastLogin(g);
		}
		LoginScreen.paintShowchar(g);
		LoginScreen.paintLogo(g, MotherCanvas.hw);
		GameCanvas.resetTrans(g);
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
		base.paint(g);
	}

	public override void update()
	{
		LoginScreen.updateYPaintLogo(LoginScreen.hLogo);
		LoginScreen.updateCharShow();
	}

	public override void updatekey()
	{
		int num = vecCmd.size();
		if ((!GameCanvas.isTouch || GameCanvas.isTouchAndKey()) && num > 0)
		{
			int num2 = idCommand;
			if (GameCanvas.keyMove(0))
			{
				idCommand--;
				GameCanvas.ClearkeyMove(0);
			}
			else if (GameCanvas.keyMove(2))
			{
				idCommand++;
				GameCanvas.clearKeyHold(6);
				GameCanvas.ClearkeyMove(2);
			}
			idCommand = AvMain.resetSelect(idCommand, num - 1, isreset: false);
			if (num2 != idCommand && (!GameCanvas.isTouch || GameCanvas.isTouchAndKey()))
			{
				for (int i = 0; i < num; i++)
				{
					iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
					if (i == idCommand)
					{
						iCommand2.isSelect = true;
					}
					else
					{
						iCommand2.isSelect = false;
					}
				}
			}
		}
		if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (vecCmd != null && idCommand < vecCmd.size())
			{
				((iCommand)vecCmd.elementAt(idCommand)).perform();
			}
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		for (int i = 0; i < vecCmd.size(); i++)
		{
			((iCommand)vecCmd.elementAt(i)).updatePointer();
		}
		base.updatePointer();
	}
}
