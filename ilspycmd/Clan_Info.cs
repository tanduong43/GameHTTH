public class Clan_Info : ChatDetail
{
	private int[] posValueName;

	private static mFont fontpaint;

	private static mFont fontInfo;

	public MainClan clan;

	public string[] strInfo;

	public iCommand cmdStatus;

	public iCommand cmdAttri;

	public iCommand cmdLevelUp;

	private int indexFocusAtt;

	private InputDialog input;

	private int hplus = 2;

	private int lastTick;

	private int framepaint;

	private int[] hCam = new int[6] { 0, 4, 8, 16, 20, 20 };

	public Clan_Info(string name, sbyte type, MainClan clan)
		: base(name, type)
	{
		this.clan = clan;
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
		if (clan != null)
		{
			getmStrInfo(clan.strVoice, wCon - 6);
		}
		int num = T.mNameClan.Length + 3 + T.mAttribute.Length;
		hplus = 2;
		if (strInfo != null)
		{
			hplus = strInfo.Length;
		}
		if (hplus < 2)
		{
			hplus = 2;
		}
		num += hplus;
		CamDetailChat = new ListNew(xBe, yBe, wCon, hCon, wItem, 0, num * wItem + 5 - hCon, isLim0: true);
		idSelect = 0;
		cmdAttri = new iCommand(T.congDiem, 1, this);
		cmdStatus = new iCommand(T.thongbao, 2, this);
		cmdLevelUp = new iCommand(T.levelUp, 3, this);
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
		paintBorder(g, 3, -1, 0, wItem * 4, num, idSelect == 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 0, 51, 20, 0, xBe + wCon / 2 - 51, num + 1, 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 0, 51, 20, 2, xBe + wCon / 2, num + 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.tabInfo, num2 + wCon / 2, num + 3, 2);
		num += wItem + 2;
		for (int i = 0; i < 3; i++)
		{
			fontpaint.drawString(g, T.mNameClan[i], num2, num, 0);
			switch (i)
			{
			case 0:
				fontInfo.drawString(g, clan.name, num2 + posValueName[i], num, 0);
				break;
			case 1:
				fontInfo.drawString(g, clan.nameCaption, num2 + posValueName[i], num, 0);
				break;
			case 2:
			{
				MainImage iconClanBig = Potion.getIconClanBig(clan.idIcon);
				if (iconClanBig == null || iconClanBig.img == null)
				{
					break;
				}
				if (iconClanBig.frame == -1)
				{
					iconClanBig.set_Frame();
				}
				if (iconClanBig.frame <= 1)
				{
					g.drawImage(iconClanBig.img, num2 + posValueName[i] + 11, num + 4, 3);
					break;
				}
				int num3 = ((framepaint < iconClanBig.frame - 1) ? 3 : 15);
				if (CRes.abs(GameCanvas.gameTick - lastTick) > num3)
				{
					framepaint++;
					if (framepaint >= iconClanBig.frame)
					{
						framepaint = 0;
					}
					lastTick = GameCanvas.gameTick;
				}
				g.drawRegion(iconClanBig.img, 0, framepaint * iconClanBig.w, iconClanBig.w, iconClanBig.w, 0, num2 + posValueName[i] + 11, num + 4, 3);
				break;
			}
			}
			num += wItem;
		}
		paintBorder(g, 0, -1, 0, wItem * 5, num, idSelect == 1);
		g.drawRegion(AvMain.imgBannerClan, 0, 20, 51, 20, 0, xBe + wCon / 2 - 51, num + 1, 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 20, 51, 20, 2, xBe + wCon / 2, num + 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.chiso, num2 + wCon / 2, num + 3, 2);
		num += wItem + 2;
		if (clan.isLevelUp > 0 && Player.ChucInCLan == 0)
		{
			if (AvMain.fraClanLevelUp == null)
			{
				AvMain.fraClanLevelUp = LoadImageStatic.loadFraImage("/interface/clanlevelup.png", 12, 12);
			}
			AvMain.fraClanLevelUp.drawFrame(GameCanvas.gameTick / 3 % AvMain.fraClanLevelUp.nFrame, num2 + wCon / 5 * 2 - 8, num + 5, 0, 3, g);
		}
		for (int j = 3; j < 7; j++)
		{
			fontpaint.drawString(g, T.mNameClan[j], num2, num, 0);
			switch (j)
			{
			case 3:
				if (clan.isLevelUp == 2 && Player.ChucInCLan == 0)
				{
					fontInfo.drawString(g, T.trungsinh, num2 + wCon / 5 * 2, num, 0);
				}
				else
				{
					fontpaint.drawString(g, T.kinhnghiem, num2 + wCon / 5 * 2, num, 0);
					int width4 = fontpaint.getWidth(T.kinhnghiem);
					fontInfo.drawString(g, clan.xp + "/" + clan.maxXp, num2 + wCon / 5 * 2 + width4, num, 0);
				}
				fontInfo.drawString(g, string.Empty + clan.level, num2 + posValueName[j], num, 0);
				break;
			case 4:
			{
				fontInfo.drawString(g, clan.numMem + "/" + clan.maxNumMem, num2 + posValueName[j], num, 0);
				fontpaint.drawString(g, T.hoatdong, num2 + wCon / 5 * 2, num, 0);
				int width3 = fontpaint.getWidth(T.hoatdong);
				fontInfo.drawString(g, clan.countAction + string.Empty, num2 + wCon / 5 * 2 + width3, num, 0);
				break;
			}
			case 5:
			{
				fontpaint.drawString(g, T.captrungsinh, num2 + wCon / 5 * 2, num, 0);
				int width2 = fontpaint.getWidth(T.captrungsinh);
				fontInfo.drawString(g, clan.trungsinh + string.Empty, num2 + wCon / 5 * 2 + width2, num, 0);
				fontInfo.drawString(g, string.Empty + clan.rank, num2 + posValueName[j], num, 0);
				break;
			}
			case 6:
			{
				fontInfo.drawString(g, clan.rubiClan + string.Empty, num2 + posValueName[j], num, 0);
				fontpaint.drawString(g, T.bery + ": ", num2 + wCon / 5 * 2, num, 0);
				int width = fontpaint.getWidth(T.bery + ": ");
				fontInfo.drawString(g, clan.beryClan + string.Empty, num2 + wCon / 5 * 2 + width, num, 0);
				break;
			}
			}
			num += wItem;
		}
		paintBorder(g, 0, -1, 0, wItem * (T.mAttribute.Length + 1), num, idSelect == 2);
		g.drawRegion(AvMain.imgBannerClan, 0, 20, 51, 20, 0, xBe + wCon / 2 - 51, num + 1, 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 20, 51, 20, 2, xBe + wCon / 2, num + 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.tabAttribute, num2 + wCon / 2, num + 3, 2);
		int num4 = 0;
		if (clan.pointAttri > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, "+" + clan.pointAttri, num2 + wCon - 2, num + 3, 1);
			num4 = 1;
		}
		num += wItem + 2;
		for (int k = 0; k < T.mAttribute.Length; k++)
		{
			fontpaint.drawString(g, T.mAttribute[k], num2 + 55, num, 1);
			Interface_Game.PaintHPMP(g, 103, clan.numAttribute[k], clan.maxAttri, num2 + 59, num + 1, 0, 10, wCon - 80, 0, isflip: false, 0, isUpdateEff: false, 0);
			if (Player.ChucInCLan == 0)
			{
				g.drawRegion(AvMain.imgPlusClan, 0, num4 * 12, 12, 12, 0, num2 + wCon - 10, num + 6, 3);
				if (idSelect == 2 && GameCanvas.gameTick % 10 > 6 && k == indexFocusAtt)
				{
					g.drawRegion(AvMain.imgPlusClan, 0, 0, 12, 12, 0, num2 + wCon - 10, num + 6, 3);
				}
			}
			num += wItem;
		}
		paintBorder(g, 1, -1, 0, wItem * (hplus + 1), num, idSelect == 3);
		g.drawRegion(AvMain.imgBannerClan, 0, 40, 51, 20, 0, xBe + wCon / 2 - 51, num + 1, 0);
		g.drawRegion(AvMain.imgBannerClan, 0, 40, 51, 20, 2, xBe + wCon / 2, num + 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.mNameClan[7], num2 + wCon / 2, num + 3, 2);
		num += wItem + 3;
		if (strInfo != null)
		{
			for (int l = 0; l < strInfo.Length; l++)
			{
				fontInfo.drawString(g, strInfo[l], num2 + 2, num, 0);
				num += wItem;
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		base.paint(g);
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
