using System;

public class TaiXiuScreen : LuckyScreen
{
	public string nameList = string.Empty;

	public new static TaiXiuScreen instance;

	private new int x;

	private new int y;

	private new int w;

	private new int h;

	private new iCommand cmdClose;

	private iCommand cmdDatCuoc;

	private iCommand cmdTatTay;

	private iCommand cmdCuocTai;

	private iCommand cmdCuocXiu;

	private iCommand cmdDatCuocTai;

	private iCommand cmdDatCuocXiu;

	private iCommand cmdTTTai;

	private iCommand cmdTTXiu;

	private iCommand cmdTTT1m;

	private iCommand cmdTTT2m;

	private iCommand cmdTTT3m;

	private iCommand cmdTTT4m;

	private iCommand cmdTTT5m;

	private iCommand cmdTTX1m;

	private iCommand cmdTTX2m;

	private iCommand cmdTTX3m;

	private iCommand cmdTTX4m;

	private iCommand cmdTTX5m;

	private FrameImage fraTaiXiu;

	private FrameImage fraXucXac;

	private int tongTienTai = 1600000;

	private int tongTienXiu = 160000;

	private int daCuoc = 600000;

	private short countDown = 60;

	private sbyte cua = -1;

	private sbyte kq = 1;

	private sbyte x1 = 1;

	private sbyte x2 = 2;

	private sbyte x3 = 3;

	private InputDialog input;

	private long timeBegin;

	private long timeUpdate;

	public bool isEffKq;

	private bool isSendSv;

	private int indexShowKq = -1;

	private int index1;

	private int index2 = 1;

	private int index3 = 2;

	public TaiXiuScreen(string name, int tongTai, int tongXiu, int daCuoc, sbyte cua, short time)
	{
		nameList = name;
		tongTienTai = tongTai;
		tongTienXiu = tongXiu;
		this.daCuoc = daCuoc;
		this.cua = cua;
		countDown = time;
		w = 260;
		h = 215;
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2;
		cmdClose = new iCommand(T.close, -1, this);
		cmdDatCuoc = new iCommand(T.datCuoc, 1, this);
		cmdTatTay = new iCommand(T.tatTay, 2, this);
		cmdDatCuoc.setPos(x + w / 4, y + 190, null, T.datCuoc);
		cmdTatTay.setPos(x + w / 4 * 3, y + 190, null, T.tatTay);
		vecCmd = new mVector();
		vecCmd.addElement(cmdDatCuoc);
		vecCmd.addElement(cmdTatTay);
		vecCmd.addElement(cmdClose);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x + w / 2 + 60, y + 10 + 8, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			right = cmdClose;
		}
		fraTaiXiu = new FrameImage(mImage.createImage("/interface/taixiu.png"), 4);
		fraXucXac = new FrameImage(mImage.createImage("/interface/xucxac.png"), 6);
		cmdCuocTai = new iCommand(T.datCuoc + " " + T.tai, 11, this);
		cmdCuocXiu = new iCommand(T.datCuoc + " " + T.xiu, 12, this);
		cmdDatCuocTai = new iCommand(T.datCuoc, 111, this);
		cmdDatCuocXiu = new iCommand(T.datCuoc, 121, this);
		cmdTTTai = new iCommand(T.tatTay + " " + T.tai, 21, this);
		cmdTTT1m = new iCommand(T.tatTay + " " + T.tai + " 1M", 31, this);
		cmdTTT2m = new iCommand(T.tatTay + " " + T.tai + " 2M", 32, this);
		cmdTTT3m = new iCommand(T.tatTay + " " + T.tai + " 3M", 33, this);
		cmdTTT4m = new iCommand(T.tatTay + " " + T.tai + " 4M", 34, this);
		cmdTTT5m = new iCommand(T.tatTay + " " + T.tai + " 5M", 35, this);
		cmdTTXiu = new iCommand(T.tatTay + " " + T.xiu, 22, this);
		cmdTTX1m = new iCommand(T.tatTay + " " + T.xiu + " 1M", 41, this);
		cmdTTX2m = new iCommand(T.tatTay + " " + T.xiu + " 2M", 42, this);
		cmdTTX3m = new iCommand(T.tatTay + " " + T.xiu + " 3M", 43, this);
		cmdTTX4m = new iCommand(T.tatTay + " " + T.xiu + " 4M", 44, this);
		cmdTTX5m = new iCommand(T.tatTay + " " + T.xiu + " 5M", 45, this);
		timeBegin = mSystem.currentTimeMillis();
		timeUpdate = mSystem.currentTimeMillis();
		index1 = CRes.random(6);
		index2 = CRes.random(6);
		index3 = CRes.random(6);
		indexShowKq = -1;
		StepQuaySo = 0;
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		MainTab.paintMoney(g, MotherCanvas.w - 78, 4 + GameScreen.h12plus, isClan: false);
		paintPaper(g, x, y, w, h, w, AvMain.PAPER_NORMAL);
		g.setColor(14203529);
		g.fillRoundRect(x + w / 2 - 60, y + 10, 120, 16, 4, 4);
		AvMain.FontBorderColor(g, nameList, x + w / 2, y + 12, 2, 6, 5);
		AvMain.FontBorderColor(g, countDown + string.Empty, MotherCanvas.hw, y + 35, 2, 1, 7);
		int num = x + w / 4 - 15;
		int num2 = y + 60;
		fraTaiXiu.drawFrame(0, num, num2, 0, 3, g);
		AvMain.paintRect(g, num - 30, num2 + 40, 60, 15, 1, 4);
		string st = ((tongTienTai >= 1000000) ? AvMain.getMoneyShortText(tongTienTai) : AvMain.getDotNumber(tongTienTai));
		mFont.tahoma_7_yellow.drawString(g, st, num, num2 + 40 + 2, 2);
		int num3 = x + w / 4 * 3 + 15;
		fraTaiXiu.drawFrame(2, num3, num2, 0, 3, g);
		AvMain.paintRect(g, num3 - 30, num2 + 40, 60, 15, 1, 4);
		st = ((tongTienXiu >= 1000000) ? AvMain.getMoneyShortText(tongTienXiu) : AvMain.getDotNumber(tongTienXiu));
		mFont.tahoma_7_yellow.drawString(g, st, num3, num2 + 40 + 2, 2);
		int num4 = MotherCanvas.hw + 2;
		num2 = y + 150;
		mFont.tahoma_7_black.drawString(g, T.daCuoc, num4, num2, 1);
		mFont.tahoma_7_red.drawString(g, AvMain.getDotNumber(daCuoc), num4 + 2, num2, 0);
		mFont.tahoma_7_black.drawString(g, T.cua, num4, num2 + 15, 1);
		if (cua == -1)
		{
			mFont.tahoma_7_red.drawString(g, "...", num4 + 2, num2 + 15, 0);
		}
		else if (cua == 0)
		{
			mFont.tahoma_7_red.drawString(g, T.xiu, num4 + 2, num2 + 15, 0);
		}
		else
		{
			mFont.tahoma_7_red.drawString(g, T.tai, num4 + 2, num2 + 15, 0);
		}
		num4 = MotherCanvas.hw;
		num2 = y + 60;
		if (StepQuaySo == 4)
		{
			num2 = y + 100;
			fraXucXac.drawFrame(x1 - 1, num4 - 18, num2 - 5, 0, 3, g);
			fraXucXac.drawFrame(x2 - 1, num4 + 20, num2 + 5, 0, 3, g);
			fraXucXac.drawFrame(x3 - 1, num4 - 5, num2 + 20, 0, 3, g);
			if (kq != -1)
			{
				num2 = y + 60;
				if (kq == 1)
				{
					fraTaiXiu.drawFrame(1, num, num2, 0, 3, g);
				}
				else if (kq == 0)
				{
					fraTaiXiu.drawFrame(3, num3, num2, 0, 3, g);
				}
			}
		}
		else if (StepQuaySo != 4)
		{
			num2 = y + 100;
			fraXucXac.drawFrame(index1, num4 - 18, num2 - 5, 0, 3, g);
			fraXucXac.drawFrame(index2, num4 + 20, num2 + 5, 0, 3, g);
			fraXucXac.drawFrame(index3, num4 - 5, num2 + 20, 0, 3, g);
			if (indexShowKq != -1)
			{
				num2 = y + 60;
				if (indexShowKq == 1)
				{
					fraTaiXiu.drawFrame(1, num, num2, 0, 3, g);
				}
				else if (indexShowKq == 0)
				{
					fraTaiXiu.drawFrame(3, num3, num2, 0, 3, g);
				}
			}
		}
		for (int i = 0; i < vecCmd.size(); i++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
		paintEff(g, 0);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
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
		case 1:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(cmdCuocTai);
			mVector2.addElement(cmdCuocXiu);
			GameCanvas.menu.startAt(mVector2, 2, T.datCuoc);
			break;
		}
		case 11:
			input = GameCanvas.Start_Input_Dialog(T.nhapSoTien, cmdDatCuocTai, isNum: true, T.datCuoc + " " + T.tai);
			GameCanvas.subDialog = input;
			break;
		case 12:
			input = GameCanvas.Start_Input_Dialog(T.nhapSoTien, cmdDatCuocXiu, isNum: true, T.datCuoc + " " + T.xiu);
			GameCanvas.subDialog = input;
			break;
		case 111:
		{
			int num = 0;
			try
			{
				num = int.Parse(input.tfInput.getText());
				if (num < 0)
				{
					num = 0;
				}
			}
			catch (Exception)
			{
				num = 0;
			}
			GameCanvas.end_Dialog();
			if (num / 1000 > 0 && num % 1000 == 0 && num <= 500000)
			{
				GlobalService.gI().TaiXiu(0, 1, num, 1, 0);
			}
			else
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.vuiLongNhapSoChan);
			}
			break;
		}
		case 121:
		{
			int num = 0;
			try
			{
				num = int.Parse(input.tfInput.getText());
				if (num < 0)
				{
					num = 0;
				}
			}
			catch (Exception)
			{
				num = 0;
			}
			GameCanvas.end_Dialog();
			if (num / 1000 > 0 && num % 1000 == 0 && num <= 500000)
			{
				GlobalService.gI().TaiXiu(0, 1, num, 0, 0);
			}
			else
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.vuiLongNhapSoChan);
			}
			break;
		}
		case 2:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(cmdTTTai);
			mVector2.addElement(cmdTTXiu);
			GameCanvas.menu.startAt(mVector2, 2, T.tatTay);
			break;
		}
		case 21:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(cmdTTT1m);
			mVector2.addElement(cmdTTT2m);
			mVector2.addElement(cmdTTT3m);
			mVector2.addElement(cmdTTT4m);
			mVector2.addElement(cmdTTT5m);
			GameCanvas.menu.startAt(mVector2, 2, T.tatTay + " " + T.tai);
			break;
		}
		case 22:
		{
			mVector mVector2 = new mVector();
			mVector2.addElement(cmdTTX1m);
			mVector2.addElement(cmdTTX2m);
			mVector2.addElement(cmdTTX3m);
			mVector2.addElement(cmdTTX4m);
			mVector2.addElement(cmdTTX5m);
			GameCanvas.menu.startAt(mVector2, 2, T.tatTay + " " + T.xiu);
			break;
		}
		case 31:
			GlobalService.gI().TaiXiu(0, 1, 1000000, 1, 1);
			break;
		case 32:
			GlobalService.gI().TaiXiu(0, 1, 2000000, 1, 1);
			break;
		case 33:
			GlobalService.gI().TaiXiu(0, 1, 3000000, 1, 1);
			break;
		case 34:
			GlobalService.gI().TaiXiu(0, 1, 4000000, 1, 1);
			break;
		case 35:
			GlobalService.gI().TaiXiu(0, 1, 5000000, 1, 1);
			break;
		case 41:
			GlobalService.gI().TaiXiu(0, 1, 1000000, 0, 1);
			break;
		case 42:
			GlobalService.gI().TaiXiu(0, 1, 2000000, 0, 1);
			break;
		case 43:
			GlobalService.gI().TaiXiu(0, 1, 3000000, 0, 1);
			break;
		case 44:
			GlobalService.gI().TaiXiu(0, 1, 4000000, 0, 1);
			break;
		case 45:
			GlobalService.gI().TaiXiu(0, 1, 5000000, 0, 1);
			break;
		}
	}

	public override void update()
	{
		base.update();
		if (GameCanvas.timeNow - timeBegin >= 1000 && countDown > 0)
		{
			timeBegin = GameCanvas.timeNow;
			countDown--;
			if (countDown == 0)
			{
				isSendSv = true;
			}
		}
		if (isSendSv && GameCanvas.timeNow - timeBegin >= 3000)
		{
			GlobalService.gI().TaiXiu(0, 2);
			isSendSv = false;
		}
		if (GameCanvas.timeNow - timeUpdate >= 5000)
		{
			timeUpdate = GameCanvas.timeNow;
			GlobalService.gI().TaiXiu(0, 3);
		}
	}

	public override void updatePointer()
	{
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

	private void IncreaseIndexShowKq()
	{
		indexShowKq++;
		if (indexShowKq > 1)
		{
			indexShowKq = 0;
		}
		mSound.playSound(51, mSound.volumeSound);
		index1 = CRes.random(6);
		index2 = CRes.random(6);
		index3 = CRes.random(6);
	}

	public override void UpdateStepQuaySo()
	{
		tickAction++;
		if (StepQuaySo == 1)
		{
			if (tickAction >= 0)
			{
				if (tickAction < 12)
				{
					if (tickAction % 6 == 0)
					{
						IncreaseIndexShowKq();
					}
				}
				else if (tickAction < 20)
				{
					if (tickAction % 4 == 0)
					{
						IncreaseIndexShowKq();
					}
				}
				else if (tickAction % 3 == 0)
				{
					IncreaseIndexShowKq();
				}
			}
			if (tickAction >= 100)
			{
				StepQuaySo = 3;
				tickAction = 0;
			}
		}
		else if (StepQuaySo == 3)
		{
			if (tickAction >= 0)
			{
				if (tickAction < 20)
				{
					if (tickAction % 4 == 0)
					{
						IncreaseIndexShowKq();
					}
				}
				else if (tickAction % 6 == 0)
				{
					IncreaseIndexShowKq();
				}
			}
			if (tickAction > 50 && indexShowKq == kq)
			{
				StepQuaySo = 4;
				tickAction = 0;
			}
		}
		else
		{
			if (StepQuaySo != 4)
			{
				return;
			}
			if (tickAction == 200)
			{
				StepQuaySo = 0;
				tickAction = 0;
				indexShowKq = -1;
				GlobalService.gI().TaiXiu(0, 0);
			}
			if (tickAction != 1)
			{
				return;
			}
			int num = 10;
			if (cua != -1)
			{
				if (kq == cua)
				{
					mSound.playSound(28, mSound.volumeSound);
					int subtype = 0;
					createEff(177, subtype, x + w / 2, y + h / 2 + num, x + w / 2, y + h / 2 + num);
					createEff(76, 0, x + w / 2, y + h / 2 + num, x + w / 2, y + h / 2 + num);
				}
				else if (kq != cua)
				{
					mSound.playSound(29, mSound.volumeSound);
					int subtype2 = 1;
					createEff(177, subtype2, x + w / 2, y + h / 2 + num, x + w / 2, y + h / 2 + num);
					createEff(77, 0, x + w / 2, y + h / 2 + num, x + w / 2, y + h / 2 + num);
				}
			}
		}
	}

	public void SetXucXac(sbyte kq, sbyte x1, sbyte x2, sbyte x3, bool isEff)
	{
		this.kq = kq;
		this.x1 = x1;
		this.x2 = x2;
		this.x3 = x3;
		isEffKq = isEff;
		if (isEff)
		{
			StepQuaySo = 1;
			tickAction = 0;
		}
		index1 = x1 - 1;
		index2 = x2 - 1;
		index3 = x3 - 1;
	}

	public void SetDatCuoc(int tongTai, int tongXiu, int daCuoc, sbyte cua)
	{
		tongTienTai = tongTai;
		tongTienXiu = tongXiu;
		this.daCuoc = daCuoc;
		this.cua = cua;
	}

	public void SetUpdateTaiXiu(int tongTai, int tongXiu)
	{
		tongTienTai = tongTai;
		tongTienXiu = tongXiu;
	}

	public void createEff(short type, int subtype, int x, int y, int xto, int yto)
	{
		Effect_End o = new Effect_End(type, (sbyte)subtype, x, y, xto, yto, 0, null);
		vecEff.addElement(o);
	}
}
