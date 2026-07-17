using System;

public class ScreenJoinItem : MainScreen
{
	private MainItem[] mItem;

	private int hItem;

	private int[][] posItem;

	private InputDialog input;

	private mVector cmdList;

	private mVector vecEff = new mVector("ScreenJoinItem.vecEff");

	private iCommand cmdClose;

	private iCommand cmdOk;

	private iCommand cmdInputNum;

	private iCommand cmdAgain;

	private int wDia;

	private int wItem;

	private int hDia;

	private int xDia;

	private int yDia;

	private int idCommand;

	private string[] strinfo;

	public short numJoin = 1;

	public static ScreenJoinItem instance;

	private int Step;

	private int tickStep;

	private int tickStop;

	public void setInfoJoin(string info, MainItem[] mItem)
	{
		if (mItem != null && mItem.Length != 0)
		{
			this.mItem = mItem;
			cmdList = new mVector();
			cmdInputNum = new iCommand(T.soluong, 3, this);
			cmdList.addElement(cmdInputNum);
			cmdOk = new iCommand(T.join, 1, this);
			cmdList.addElement(cmdOk);
			cmdClose = new iCommand(T.close, 0, this);
			cmdList.addElement(cmdClose);
			cmdAgain = new iCommand(T.gheptiep, 4, this);
			wDia = MotherCanvas.w;
			if (wDia > 210)
			{
				wDia = 210;
			}
			if (wDia < 190)
			{
				wDia = 190;
			}
			hItem = 40;
			wItem = 28;
			if (wDia < 210)
			{
				wItem = 20;
			}
			strinfo = mFont.tahoma_7_black.splitFontArray(info, wDia - 20);
			hDia = (strinfo.Length + 1) * GameCanvas.hText + (mItem.Length - 1) * hItem + iCommand.hButtonCmdNor + 40;
			if (GameCanvas.isTouchNoOrPC())
			{
				hDia += iCommand.hButtonCmdNor;
			}
			xDia = MotherCanvas.hw - wDia / 2;
			yDia = MotherCanvas.hh - hDia / 2 - 5;
			int num = xDia + wDia / 2;
			int num2 = yDia + (strinfo.Length + 1) * GameCanvas.hText + 15;
			posItem = mSystem.new_M_Int(mItem.Length, 2);
			posItem[0][0] = num + wDia / 4 - wItem / 2;
			posItem[0][1] = num2 + (mItem.Length - 1) * hItem / 2;
			for (int i = 1; i < mItem.Length; i++)
			{
				posItem[i][0] = num - wDia / 4 - wItem / 2;
				posItem[i][1] = num2 + (i - 1) * hItem + hItem / 2;
			}
			idCommand = 0;
			if (!GameCanvas.isTouchNoOrPC())
			{
				cmdInputNum.setPos(xDia + wDia / 2 - iCommand.wButtonCmd / 2 - 10, yDia + hDia - iCommand.hButtonCmdNor / 2 - 5, null, cmdInputNum.caption);
				cmdOk.setPos(xDia + wDia / 2 + iCommand.wButtonCmd / 2 + 10, yDia + hDia - iCommand.hButtonCmdNor / 2 - 5, null, cmdOk.caption);
				cmdClose.setPos(xDia + wDia - 10, yDia + 20, MainTab.fraCloseTab, string.Empty);
			}
			else
			{
				cmdInputNum.setPos(xDia + wDia / 2 - iCommand.wButtonCmd / 2 - 10, yDia + hDia - iCommand.hButtonCmdNor / 2 - 10 - iCommand.hButtonCmdNor, null, cmdInputNum.caption);
				cmdInputNum.isSelect = true;
				cmdOk.setPos(xDia + wDia / 2 + iCommand.wButtonCmd / 2 + 10, yDia + hDia - iCommand.hButtonCmdNor / 2 - 10 - iCommand.hButtonCmdNor, null, cmdOk.caption);
				cmdClose.setPos(xDia + wDia / 2, yDia + hDia + iCommand.hButtonCmdNor / 2 - 10 - iCommand.hButtonCmdNor, null, cmdClose.caption);
				cmdInputNum.isSelect = true;
				backCMD = cmdClose;
			}
			Step = 0;
			numJoin = 1;
		}
	}

	public void setItemJoin(MainItem item, sbyte ishardcode)
	{
		if (mItem != null && mItem.Length > 0)
		{
			mItem[0] = item;
			mItem[0].isRemove = true;
			for (int i = 1; i < mItem.Length; i++)
			{
				if (ishardcode == 0)
				{
					mItem[i].numPotion = (short)(mItem[i].numPotion * mItem[0].numPotion);
				}
				else
				{
					mItem[i].numPotion = (short)(mItem[i].numPotion * numJoin);
				}
			}
		}
		Step = 1;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 1:
			if (numJoin <= 0)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.chuanhapsoluong);
				break;
			}
			GameCanvas.end_Dialog();
			GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: true);
			GlobalService.gI().Split_Item(1, 1, mItem[0].ID, mItem[0].typeObject, numJoin);
			break;
		case 2:
		{
			if (mItem[0] == null)
			{
				break;
			}
			short num = 1;
			try
			{
				num = (short)int.Parse(input.tfInput.getText());
				if (num <= 0)
				{
					num = 1;
				}
			}
			catch (Exception)
			{
				num = 1;
			}
			numJoin = num;
			GameCanvas.end_Dialog();
			break;
		}
		case 0:
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
			break;
		case 3:
			input = GameCanvas.Start_Input_Dialog(T.nhapsoluongmuonghep, new iCommand(T.strconfirm, 2, 0, this), isNum: true, T.joinItem);
			GameCanvas.currentDialog = input;
			break;
		case 4:
			GameCanvas.end_Dialog();
			GlobalService.gI().Split_Item(1, 4, 0, 0, 0);
			break;
		default:
			base.commandPointer(index, subIndex);
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
		int num = yDia;
		int num2 = xDia + 15;
		paintPaper(g, MotherCanvas.hw - wDia / 2, num, wDia, hDia, wDia, AvMain.PAPER_NORMAL);
		Interface_Game.paintNumMess(g, -Interface_Game.xNumMess + 8, -Interface_Game.yNumMess + 3);
		g.setClip(MotherCanvas.hw - wDia / 2, 0, wDia, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wDia / 2, 0, wDia, MotherCanvas.h);
		num += 12;
		g.setColor(14203529);
		g.fillRoundRect(xDia + 10, num, wDia - 20, 16, 4, 4);
		num += 3;
		mFont.tahoma_7b_red.drawString(g, T.joinItem, xDia + wDia / 2, num, 2);
		num += GameCanvas.hText;
		for (int i = 0; i < strinfo.Length; i++)
		{
			mFont.tahoma_7_black.drawString(g, strinfo[i], MotherCanvas.w / 2, num + 10, 2);
			num += GameCanvas.hText;
		}
		if (Step == 0)
		{
			if (AvMain.imgJoin == null)
			{
				AvMain.imgJoin = mImage.createImage("/interface/muiten.png");
			}
			else
			{
				g.drawImage(AvMain.imgJoin, xDia + wDia / 2, posItem[0][1] + wItem / 2, 3);
			}
		}
		for (int j = 0; j < posItem.Length; j++)
		{
			AvMain.paintRect(g, posItem[j][0], posItem[j][1], wItem, wItem, 1, 3);
			if (mItem[j] != null && !mItem[j].isRemove)
			{
				mItem[j].paintColor(g, posItem[j][0] + wItem / 2, posItem[j][1] + wItem / 2, wItem - 1);
				mItem[j].paintAllItem_Num1(g, posItem[j][0] + wItem / 2, posItem[j][1] + wItem / 2, wItem, 0, mItem[j].colorName, (short)(mItem[j].numPotion * numJoin));
			}
		}
		if (cmdList != null)
		{
			for (int k = 0; k < cmdList.size(); k++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(k);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		for (int l = 0; l < vecEff.size(); l++)
		{
			MainEffect mainEffect = (MainEffect)vecEff.elementAt(l);
			if (mainEffect.levelPaint > -1)
			{
				mainEffect.paint(g);
			}
		}
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		if (GameCanvas.currentDialog == null)
		{
			updatekey();
			updatePointer();
		}
		updateStep();
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
	}

	private void updateStep()
	{
		if (Step != 1)
		{
			return;
		}
		tickStep++;
		if (tickStop == 0)
		{
			cmdList.removeAllElements();
			numJoin = 1;
			if (tickStep % 10 == 0)
			{
				mSound.playSound(26, mSound.volumeSound);
				mSound.playSound(27, mSound.volumeSound);
				for (int i = 1; i < posItem.Length; i++)
				{
					vecEff.addElement(GameScreen.createEffEnd(78, 0, posItem[i][0] + wItem / 2, posItem[i][1] + wItem / 2, posItem[0][0] + wItem / 2, posItem[0][1] + wItem / 2));
					mItem[i].isRemove = true;
				}
				tickStop = tickStep + 11 + wDia / 2 / 8;
			}
		}
		else if (tickStep >= tickStop)
		{
			mSound.playSound(26, mSound.volumeSound);
			vecEff.addElement(GameScreen.createEffEnd(53, 0, posItem[0][0] + wItem / 2, posItem[0][1] + wItem / 2, posItem[0][0] + wItem / 2, posItem[0][1] + wItem / 2));
			Step = 2;
			tickStep = 0;
			tickStop = 0;
			numJoin = 1;
			if (mItem != null && mItem.Length > 0)
			{
				mItem[0].isRemove = false;
			}
			cmdList.removeAllElements();
			cmdList.addElement(cmdAgain);
			cmdList.addElement(cmdClose);
			idCommand = 0;
			if (!GameCanvas.isTouchNoOrPC())
			{
				cmdAgain.setPos(xDia + wDia / 2, yDia + hDia - iCommand.hButtonCmdNor / 2 - 5, null, cmdAgain.caption);
				return;
			}
			cmdAgain.setPos(xDia + wDia / 2 - iCommand.wButtonCmd / 2 - 10, yDia + hDia - 10 - iCommand.hButtonCmdNor, null, cmdAgain.caption);
			cmdAgain.isSelect = true;
			cmdClose.setPos(xDia + wDia / 2 + iCommand.wButtonCmd / 2 + 10, yDia + hDia - 10 - iCommand.hButtonCmdNor, null, cmdClose.caption);
		}
	}

	public override void updatekey()
	{
		if (Step == 1)
		{
			return;
		}
		if (cmdList != null)
		{
			int num = cmdList.size();
			if (GameCanvas.isTouchNoOrPC() && num > 0)
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
					GameCanvas.ClearkeyMove(2);
				}
				idCommand = AvMain.resetSelect(idCommand, num - 1, isreset: false);
				iCommand iCommand2 = (iCommand)cmdList.elementAt(idCommand);
				if (iCommand2.caption.Length == 0)
				{
					idCommand = 0;
				}
				if (num2 != idCommand && GameCanvas.isTouchNoOrPC())
				{
					for (int i = 0; i < num; i++)
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
			}
		}
		if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (cmdList != null && idCommand < cmdList.size())
			{
				((iCommand)cmdList.elementAt(idCommand)).perform();
			}
		}
		updatekeyPC();
	}

	public override void updatePointer()
	{
		if (Step != 1 && cmdList != null)
		{
			for (int i = 0; i < cmdList.size(); i++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
				iCommand2.updatePointer();
			}
		}
	}

	public override bool keyBack()
	{
		if (!base.keyBack() && cmdClose != null)
		{
			cmdClose.perform();
		}
		return false;
	}
}
