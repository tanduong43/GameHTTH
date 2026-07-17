public class GlobalLogicHandler
{
	public static GlobalLogicHandler instance;

	public static GlobalLogicHandler gI()
	{
		if (instance == null)
		{
			instance = new GlobalLogicHandler();
		}
		return instance;
	}

	public void onConnectFail()
	{
		if (GameCanvas.currentScreen == GameCanvas.updateImageAndroidScr)
		{
			return;
		}
		if (GameMidlet.DEVICE == 0)
		{
			string info = T.connectfail;
			if (GameCanvas.infoDisConnect != null && GameCanvas.infoDisConnect.Length > 10)
			{
				info = GameCanvas.infoDisConnect;
				GameCanvas.infoDisConnect = string.Empty;
			}
			GameScreen.cmdCheckServer = new iCommand(T.kiemtra, 46, GameCanvas.gameScr);
			GameCanvas.Start_Normal_DiaLog(info, GameScreen.cmdCheckServer, isCmdClose: true);
			return;
		}
		string info2 = T.connectfail;
		if (GameCanvas.getHourTime() == 5 && GameCanvas.language == 0)
		{
			info2 = T.baotridinhky;
		}
		if (GameCanvas.infoDisConnect != null && GameCanvas.infoDisConnect.Length > 10)
		{
			info2 = GameCanvas.infoDisConnect;
			GameCanvas.infoDisConnect = string.Empty;
		}
		GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info2);
	}

	public void onConnectOK()
	{
	}

	public static void onDisconnect()
	{
		mSystem.outz("disconect global");
		GameCanvas.dialogDisconect();
	}
}
