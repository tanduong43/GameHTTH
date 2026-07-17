public class MsgInfoMemClan : MsgDialog
{
	private InfoMemList mem;

	private int sizeBanner = 120;

	private iCommand cmdGiaoTiep;

	private iCommand cmdKick;

	private iCommand cmdPhongChuc;

	private iCommand cmdDonate;

	private iCommand cmdInfo;

	private iCommand cmdChat;

	private iCommand cmdAddFriend;

	private iCommand cmdLeave;

	private iCommand cmdGopRuby;

	private iCommand cmdInven;

	private iCommand cmdWorld;

	private iCommand cmdIconClan;

	private InputDialog input;

	public MsgInfoMemClan(InfoMemList mem)
	{
		this.mem = mem;
		if (mem == null)
		{
			return;
		}
		cmdList.removeAllElements();
		cmdClose = new iCommand(T.close, 1, this);
		if (mem.name.CompareTo(GameScreen.player.name) != 0)
		{
			cmdGiaoTiep = new iCommand(T.Giaotiep, 2, this);
			cmdKick = new iCommand(T.kickClan, 10, this);
			cmdDonate = new iCommand(T.strDonate, 4, this);
			cmdInfo = new iCommand(T.strclanMyInfo, 5, this);
			cmdChat = new iCommand(T.TroChuyen, 6, this);
			cmdAddFriend = new iCommand(T.addFriend, 7, this);
			cmdPhongChuc = new iCommand(T.phongchuc, 8, this);
			cmdList.addElement(cmdDonate);
			cmdList.addElement(cmdGiaoTiep);
		}
		else
		{
			if (Player.ChucInCLan == 0)
			{
				cmdInven = new iCommand(T.Khobang, 14, this);
				cmdList.addElement(cmdInven);
				cmdWorld = new iCommand(T.WorldChanel, 15, this);
				cmdList.addElement(cmdWorld);
				cmdIconClan = new iCommand(T.khoHuyHieu, 17, this);
				cmdList.addElement(cmdIconClan);
			}
			else if (Player.ChucInCLan == 1)
			{
				cmdInven = new iCommand(T.Khobang, 14, this);
				cmdList.addElement(cmdInven);
				cmdLeave = new iCommand(T.leaveClan, 11, this);
				cmdList.addElement(cmdLeave);
				cmdWorld = new iCommand(T.WorldChanel, 15, this);
				cmdList.addElement(cmdWorld);
			}
			else
			{
				cmdLeave = new iCommand(T.leaveClan, 11, this);
				cmdList.addElement(cmdLeave);
			}
			cmdGopRuby = new iCommand(T.donggop, 12, this);
			cmdList.addElement(cmdGopRuby);
		}
		if (!GameCanvas.isTouch)
		{
			cmdList.addElement(cmdClose);
		}
		wDia = 160;
		hDia = 140;
		if (Player.ChucInCLan == 1 || Player.ChucInCLan == 0)
		{
			hDia += GameCanvas.hCommand + 10;
		}
		else if (!GameCanvas.isTouch)
		{
			hDia += GameCanvas.hCommand + 10;
		}
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2;
		wItem = 27;
		setPosCmdNew(15, isLast: false);
		if (GameCanvas.isTouch)
		{
			cmdList.addElement(cmdClose);
			cmdClose.setPos(MotherCanvas.hw + (wDia - 50) / 2, yDia - GameCanvas.hCommand / 2, MainTab.fraCloseTab, string.Empty);
		}
		backCMD = cmdClose;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 1:
			GameCanvas.end_Dialog();
			break;
		case 2:
		{
			mVector mVector4 = new mVector();
			mVector4.addElement(cmdInfo);
			mVector4.addElement(cmdChat);
			mVector4.addElement(cmdAddFriend);
			if (Player.ChucInCLan == 0 || Player.ChucInCLan == 1)
			{
				mVector4.addElement(cmdPhongChuc);
				mVector4.addElement(cmdKick);
			}
			GameCanvas.menu.startAt(mVector4, 2, mem.name);
			break;
		}
		case 3:
			GlobalService.gI().Clan_CMD(1, mem.name, (short)mem.id, 0);
			GameCanvas.end_Dialog();
			break;
		case 4:
			GlobalService.gI().Clan_CMD(2, mem.name, (short)mem.id, 0);
			break;
		case 5:
			GameCanvas.gameScr.ShowInfoOtherPlayer(mem.name);
			break;
		case 6:
			GameCanvas.gameScr.ShowChatTab(mem.name);
			break;
		case 7:
			GlobalService.gI().Friend(0, mem.id);
			break;
		case 8:
		{
			mVector mVector3 = new mVector();
			if (Player.ChucInCLan == 0)
			{
				iCommand o = new iCommand(T.thuyenpho, 13, 1, this);
				iCommand o2 = new iCommand(T.hoatieu, 13, 2, this);
				iCommand o3 = new iCommand(T.thanhvien, 13, 10, this);
				mVector3.addElement(o);
				mVector3.addElement(o2);
				mVector3.addElement(o3);
			}
			if (Player.ChucInCLan == 1)
			{
				iCommand o4 = new iCommand(T.hoatieu, 13, 2, this);
				iCommand o5 = new iCommand(T.thanhvien, 13, 10, this);
				mVector3.addElement(o4);
				mVector3.addElement(o5);
			}
			GameCanvas.menu.startAt(mVector3, 2, mem.name);
			break;
		}
		case 9:
			GlobalService.gI().Clan_CMD(4, string.Empty, (short)mem.id, 0);
			GameCanvas.end_Dialog();
			break;
		case 10:
			GameCanvas.Start_Normal_DiaLog(T.banmuon + mem.name + " " + T.khoibang, new iCommand(T.kickClan, 3, this), isCmdClose: true);
			break;
		case 11:
			GameCanvas.Start_Normal_DiaLog(T.banmuon + T.khoibang, new iCommand(T.leaveClan, 9, this), isCmdClose: true);
			break;
		case 12:
			GlobalService.gI().Clan_CMD(15, string.Empty, 0, 0);
			GameCanvas.end_Dialog();
			break;
		case 13:
			GlobalService.gI().Clan_CMD(3, mem.name, (short)mem.id, (sbyte)subIndex);
			break;
		case 14:
		{
			GameCanvas.end_Dialog();
			GameCanvas.tabShopScr = new TabScreen(MainTab.xTab, 0);
			mVector mVector2 = new mVector();
			GameCanvas.tabShopScr.isShopClan = true;
			GameCanvas.tabInvenClan = new TabInventory(T.Khobang, Player.vecInvenClan, 4, MainTab.xTab);
			GameCanvas.tabInvenClan.initCmd();
			mVector2.addElement(GameCanvas.tabInvenClan);
			GameCanvas.tabShopScr.addVecTab(mVector2);
			GameCanvas.tabShopScr.idSelect = 0;
			GameCanvas.tabShopScr.Show(GameCanvas.ClanScr);
			GameCanvas.tabShopScr.typeCurrent = 1;
			GameCanvas.tabShopScr.setTabSelect();
			break;
		}
		case 15:
			if (input == null)
			{
				input = new InputDialog();
				iCommand cmd = new iCommand(T.chat, 16, this);
				input.setinfo(T.nhapNoiDung, cmd, isNum: false, T.WorldChanel);
			}
			else
			{
				input.tfInput.setText(string.Empty);
			}
			GameCanvas.Start_Sub_Dialog(input);
			break;
		case 16:
			GlobalService.gI().World_Chanel(1, input.tfInput.getText());
			GameCanvas.end_Dialog();
			break;
		case 17:
			GlobalService.gI().Send_Type(-95, 1);
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia + 4;
		int num2 = xDia + miniItem * 3;
		paintPaper_UpDown(g, xDia - 5, yDia - 32, maxWShow + 10, hDia + 44, maxWShow + 10);
		g.setColor(15972174);
		g.fillRoundRect(xDia + wDia / 2 - sizeBanner / 2, yDia - 20, sizeBanner, 16, 4, 4);
		AvMain.FontBorderColor(g, T.info, xDia + maxWShow / 2, yDia - 18, 2, 6, 5);
		MainObject.paintHeadEveryWhere(g, mem.head, mem.hair, mem.hat, num2 + miniItem * 2, num + miniItem + 45, 2);
		AvMain.FontBorderColor(g, mem.name, num2 + miniItem * 2 + 16, num + miniItem / 2, 0, 0, 7);
		mFont.tahoma_7b_black.drawString(g, mem.getCaptionClan(mem.chucInClan), num2 + miniItem * 2 + 16, num + GameCanvas.hText - 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.capdo + ": " + mem.Lv, num2, num + GameCanvas.hText * 2 - 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.tangqua + ": " + mem.numTangqua, num2, num + GameCanvas.hText * 3 - 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.nhiemvu + ": " + mem.numQuest, num2, num + GameCanvas.hText * 4 - 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.donggop + ": " + mem.gopRuby + " " + T.ruby, num2, num + GameCanvas.hText * 5 - 1, 0);
		mFont.tahoma_7b_black.drawString(g, T.congHien + ": " + mem.congHien + " " + T.diem, num2, num + GameCanvas.hText * 6 - 1, 0);
		if (cmdList != null)
		{
			for (int i = 0; i < cmdList.size(); i++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	public override void update()
	{
		if (isClose)
		{
			updateClose();
			return;
		}
		updateOpen();
		if (GameCanvas.isTouchNoOrPC())
		{
			updatekey();
		}
		updatePointer();
	}

	public override void updatekey()
	{
		if (GameCanvas.keyMove(0))
		{
			if (idCommand > 0)
			{
				idCommand--;
			}
			else
			{
				idCommand = cmdList.size() - 1;
			}
			GameCanvas.ClearkeyMove(0);
		}
		else if (GameCanvas.keyMove(2))
		{
			if (idCommand < cmdList.size() - 1)
			{
				idCommand++;
			}
			else
			{
				idCommand = 0;
			}
			GameCanvas.ClearkeyMove(2);
		}
		else if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (cmdList != null && idCommand < cmdList.size())
			{
				((iCommand)cmdList.elementAt(idCommand)).perform();
			}
		}
		if (GameCanvas.isTouchNoOrPC())
		{
			if (idCommand >= 0 && idCommand < cmdList.size())
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(idCommand);
				if (iCommand2.caption.Length == 0)
				{
					idCommand = 0;
				}
			}
			for (int i = 0; i < cmdList.size(); i++)
			{
				iCommand iCommand3 = (iCommand)cmdList.elementAt(i);
				if (i == idCommand)
				{
					iCommand3.isSelect = true;
				}
				else
				{
					iCommand3.isSelect = false;
				}
			}
		}
		updatekeyPC();
	}

	public override void updatePointer()
	{
		base.updatePointer();
		if (cmdList != null)
		{
			for (int i = 0; i < cmdList.size(); i++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
				iCommand2.updatePointer();
			}
		}
	}
}
