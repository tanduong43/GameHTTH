public class Sudo_Info : ChatDetail
{
	public static Sudo_Info instance;

	private int[] posValueName;

	private static mFont fontpaint;

	private static mFont fontInfo;

	public MainSudo clan;

	public string[] strInfo;

	public iCommand cmdStatus;

	public iCommand cmdAttri;

	public iCommand cmdLevelUp;

	private int indexFocusAtt;

	private InputDialog input;

	public short exp;

	private int hplus = 2;

	private int lastTick;

	private int framepaint;

	public static mVector vecInfoCanhan = new mVector();

	private int[] hCam = new int[6] { 0, 4, 8, 16, 20, 20 };

	public Sudo_Info(string name, sbyte type, MainSudo clan)
		: base(name, type)
	{
		this.clan = clan;
		instance = this;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 1:
			GlobalService.gI().Clan_CMD(6, string.Empty, 1, (sbyte)indexFocusAtt);
			break;
		case 2:
			input = new InputDialog();
			input.setinfo(T.nhapcauthongbao, new iCommand(T.thongbao, 4, this), isNum: false, T.thongbaobang);
			GameCanvas.Start_Current_Dialog(input);
			break;
		case 3:
			GlobalService.gI().Clan_CMD(13, string.Empty, 0, 0);
			break;
		case 4:
		{
			string text = input.tfInput.getText();
			if (text.Length > 0)
			{
				GlobalService.gI().Clan_CMD(5, text, 0, 0);
			}
			GameCanvas.end_Dialog();
			break;
		}
		}
	}

	public override void setPos(int xBe, int yBe, int wCon, int hCon, int miniItem, int hchat)
	{
		base.xBe = xBe;
		base.yBe = yBe;
		base.wCon = wCon;
		base.hCon = hCon;
		base.miniItem = miniItem;
		hChat = hchat;
		wItem = GameCanvas.hText + 4;
		fontpaint = mFont.tahoma_7_white;
		fontInfo = mFont.tahoma_7b_white;
		posValueName = new int[T.mNameClan.Length];
		for (int i = 0; i < T.mNameClan.Length; i++)
		{
			posValueName[i] = fontpaint.getWidth(T.mNameClan[i]);
		}
		strInfo = null;
		setHplus();
		idSelect = 0;
		cmdAttri = new iCommand(T.congDiem, 1, this);
		cmdStatus = new iCommand(T.thongbao, 2, this);
		cmdLevelUp = new iCommand(T.levelUp, 3, this);
	}

	public void setHplus()
	{
		int num = T.mNameClan.Length + 3 + T.mAttribute.Length;
		hplus = 2;
		if (strInfo != null)
		{
			hplus = vecInfoCanhan.size() - 3;
		}
		if (hplus < 2)
		{
			hplus = 2;
		}
		num += hplus - 1;
		CamDetailChat = new ListNew(xBe, yBe, wCon, hCon, wItem, 0, num * wItem + 5 - hCon, isLim0: true);
	}

	public void getmStrInfo(string str, int w)
	{
		strInfo = fontInfo.splitFontArray(str, w);
	}

	public override void paint(mGraphics g)
	{
		g.setClip(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon + 2);
		g.saveCanvas();
		g.ClipRec(xBe - miniItem, yBe - 2, wCon + miniItem * 2, hCon + 2);
		g.translate(0, -CamDetailChat.cmx);
		int num = yBe;
		int num2 = xBe + 2;
		paintBorder(g, 3, -1, 0, wItem * 6, num, idSelect == 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 0, 51, 20, 0, xBe + wCon / 2 - 51, num + 1, 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 0, 51, 20, 2, xBe + wCon / 2, num + 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.player, num2 + wCon / 2, num + 3, 2);
		num += wItem + 2;
		AvMain.paintRect(g, num2 + wCon / 2 - 30, num, 60, 66, 0, 4);
		GameScreen.player.paintCharShow(g, num2 + wCon / 2, num + 40 + GameScreen.player.hOne / 4 + 5, 0, isNhip: true);
		mFont.tahoma_7b_black.drawString(g, GameScreen.player.name, num2 + wCon / 2, num + 72, 2);
		num += wItem * 5;
		paintBorder(g, 0, -1, 0, wItem * 5, num, idSelect == 1);
		g.drawRegion(AvMain.imgBannerClan, 0, 20, 51, 20, 0, xBe + wCon / 2 - 51, num + 1, 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 20, 51, 20, 2, xBe + wCon / 2, num + 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.tabInfo, num2 + wCon / 2, num + 3, 2);
		num += wItem + 2;
		if (vecInfoCanhan.size() > 0)
		{
			for (int i = 0; i < 4; i++)
			{
				MainInfoItem mainInfoItem = (MainInfoItem)vecInfoCanhan.elementAt(i);
				switch (i)
				{
				case 0:
					mFont.tahoma_7b_yellow.drawString(g, mainInfoItem.name, num2 + 5, num, 0);
					break;
				case 1:
					mFont.tahoma_7_white.drawString(g, mainInfoItem.name, num2 + 5, num, 0);
					mFont.tahoma_7b_white.drawString(g, mainInfoItem.value + string.Empty, num2 + 80, num, 0);
					break;
				case 2:
					mFont.tahoma_7_white.drawString(g, mainInfoItem.name, num2 + 5, num, 0);
					break;
				case 3:
					mFont.tahoma_7b_white.drawString(g, mainInfoItem.name, num2 + 5, num, 0);
					Interface_Game.PaintHPMP(g, 2, exp, 100, xBe + wCon / 3, num, 0, miniItem * 2 + 2, wCon / 5 * 3, 1, isflip: false, 0, isUpdateEff: false, 0);
					break;
				}
				num += wItem;
			}
		}
		paintBorder(g, 1, -1, 0, wItem * (hplus * 2 + 1), num, idSelect == 3);
		g.drawRegion(AvMain.imgBannerClan, 0, 40, 51, 20, 0, xBe + wCon / 2 - 51, num + 1, 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 40, 51, 20, 2, xBe + wCon / 2, num + 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.chiso, num2 + wCon / 2, num + 3, 2);
		num += wItem + 2;
		if (vecInfoCanhan.size() >= 4)
		{
			for (int j = 4; j < vecInfoCanhan.size(); j++)
			{
				MainInfoItem mainInfoItem2 = (MainInfoItem)vecInfoCanhan.elementAt(j);
				mFont.tahoma_7b_yellow.drawString(g, "- " + mainInfoItem2.name, num2 + 5, num, 0);
				num += wItem;
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		base.paint(g);
	}

	public void paintCaNhan(mGraphics g)
	{
		for (int i = 0; i < vecInfoCanhan.size(); i++)
		{
			MainInfoItem mainInfoItem = (MainInfoItem)vecInfoCanhan.elementAt(i);
			string text = mainInfoItem.name;
			if (mainInfoItem.value != 0)
			{
				text = text + " " + mainInfoItem.value;
			}
			mFont.tahoma_7_white.drawString(g, text, miniItem, miniItem + i * GameCanvas.hText, 0);
		}
	}

	public override void update()
	{
		CamDetailChat.moveCamera();
	}

	public override void updatePointer()
	{
		CamDetailChat.update_Pos_UP_DOWN();
		if (!GameCanvas.isPointerSelect || !GameCanvas.isPoint(xBe, yBe, wCon, hCon))
		{
			return;
		}
		int num = (T.mNameClan.Length + 1 + T.mAttribute.Length + 1) * wItem;
		if (GameCanvas.isPointer(xBe, num - CamDetailChat.cmx + yBe, wCon, (hplus + 1) * wItem) && (Player.ChucInCLan == 0 || Player.ChucInCLan == 1))
		{
			cmdStatus.perform();
			GameCanvas.isPointerSelect = false;
		}
		num = 4 * wItem;
		if (GameCanvas.isPointer(xBe, num - CamDetailChat.cmx + yBe, wCon, 5 * wItem) && Player.ChucInCLan == 0 && GameScreen.player.clan.isLevelUp > 0)
		{
			cmdLevelUp.perform();
			GameCanvas.isPointerSelect = false;
		}
		num = (T.mNameClan.Length + 2) * wItem;
		if (GameCanvas.isPointer(xBe, num - CamDetailChat.cmx + yBe, wCon, T.mAttribute.Length * wItem) && GameScreen.player.clan.pointAttri > 0 && Player.ChucInCLan == 0)
		{
			int num2 = (GameCanvas.py - (num - CamDetailChat.cmx + yBe)) / wItem;
			if (num2 >= 0 && num2 < T.mAttribute.Length)
			{
				GlobalService.gI().Clan_CMD(6, string.Empty, 1, (sbyte)num2);
			}
			GameCanvas.isPointerSelect = false;
		}
	}

	public override void updatekey()
	{
		int num = idSelect;
		if (idSelect == 2 && Player.ChucInCLan == 0 && clan.pointAttri > 0)
		{
			if (GameCanvas.keyMyHold[2])
			{
				GameCanvas.clearKeyHold(2);
				if (indexFocusAtt > 0)
				{
					indexFocusAtt--;
				}
				else
				{
					idSelect--;
					setXCam();
				}
			}
			else if (GameCanvas.keyMyHold[8])
			{
				GameCanvas.clearKeyHold(8);
				if (indexFocusAtt < T.mAttribute.Length - 1)
				{
					indexFocusAtt++;
				}
				else
				{
					idSelect++;
					setXCam();
				}
			}
		}
		else
		{
			if (GameCanvas.keyMyHold[2])
			{
				GameCanvas.clearKeyHold(2);
				if (idSelect > 0)
				{
					idSelect--;
				}
			}
			else if (GameCanvas.keyMyHold[8])
			{
				GameCanvas.clearKeyHold(8);
				if (idSelect < 3)
				{
					idSelect++;
				}
			}
			if (num != idSelect)
			{
				setXCam();
			}
		}
		if (num != idSelect)
		{
			center = null;
			setCmd();
		}
		base.updatekey();
	}

	private void setCmd()
	{
		if (idSelect == 2 && GameScreen.player.clan.pointAttri > 0 && Player.ChucInCLan == 0)
		{
			center = cmdAttri;
		}
		else if (idSelect == 1 && GameScreen.player.clan.isLevelUp > 0 && Player.ChucInCLan == 0)
		{
			center = cmdLevelUp;
		}
		else if (idSelect == 3 && (Player.ChucInCLan == 0 || Player.ChucInCLan == 1))
		{
			center = cmdStatus;
		}
	}

	private void setXCam()
	{
		int toX = 0;
		if (idSelect >= 0 && idSelect < hCam.Length)
		{
			toX = hCam[idSelect] * wItem - hCon / 4;
		}
		if (idSelect == 0)
		{
			toX = 0;
		}
		if (idSelect == 3)
		{
			toX = CamDetailChat.cmxLim;
		}
		CamDetailChat.setToX(toX);
	}
}
