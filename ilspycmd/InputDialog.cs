using System;

public class InputDialog : MainDialog
{
	public TField tfInput;

	private iCommand cmdClose;

	public TField[] mtfInput;

	private bool isMore;

	private bool isNew;

	private int price;

	private string name;

	private string info;

	private string xuluong;

	private string[] mStrNote;

	private static int hTouch;

	private static int istouchMore;

	public short IDInputserver;

	private mFont fontInput = mFont.tahoma_7_white;

	public static iCommand cmdInputServer;

	private ListNew listnew;

	private string strtam;

	public InputDialog()
	{
		cmdClose = new iCommand(T.close, -1);
		cmdInputServer = new iCommand(T.xong, 0);
		istouchMore = 15;
		if (GameCanvas.isTouch)
		{
			hTouch = iCommand.hButtonCmdNor + 5;
		}
	}

	public void closeDialog()
	{
		if (GameCanvas.currentDialog != null)
		{
			GameCanvas.currentDialog = null;
		}
		else
		{
			GameCanvas.subDialog = null;
		}
	}

	public override void commandTab(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			closeDialog();
			break;
		case 0:
		{
			string[] array = null;
			if (mtfInput != null)
			{
				array = new string[mtfInput.Length];
				for (int i = 0; i < mtfInput.Length; i++)
				{
					array[i] = mtfInput[i].getText();
				}
			}
			else
			{
				if (tfInput == null)
				{
					return;
				}
				array = new string[1] { tfInput.getText() };
			}
			GlobalService.gI().InputServer(IDInputserver, array);
			GameCanvas.end_Dialog();
			GameCanvas.Start_Waiting_DiaLog(T.pleaseWaiting, isCmdClose: true);
			break;
		}
		}
		base.commandTab(index, subIndex);
	}

	public void setinfo(string info, iCommand cmd, bool isNum, string nameInput)
	{
		listnew = null;
		isMore = false;
		isNew = false;
		left = null;
		right = null;
		center = null;
		if (cmd == null)
		{
			GameCanvas.currentDialog = null;
		}
		wDia = MotherCanvas.w - 30;
		if (wDia > 200)
		{
			wDia = 200;
		}
		strinfo = fontInput.splitFontArray(info, wDia - 20);
		name = nameInput;
		hDia = 15 * strinfo.Length + 10 + TField.getHeight() + GameCanvas.hCommand;
		hDia += hTouch + istouchMore;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia + 15;
		tfInput = new TField(xDia + 10, yDia + hDia - hTouch - (TField.getHeight() + 8), wDia - 20);
		tfInput.setMaxTextLenght(100);
		if (isNum)
		{
			tfInput.setIputType(TField.INPUT_TYPE_NUMERIC);
		}
		tfInput.setText(string.Empty);
		if (GameCanvas.isTouch)
		{
			cmd.setPos(xDia + wDia / 2, yDia + hDia - iCommand.hButtonCmdNor / 2 - 5, null, cmd.caption);
			cmdClose.setPos(xDia + wDia - 13, yDia + 13, MainTab.fraCloseTab, string.Empty);
			right = cmdClose;
			left = cmd;
			tfInput.title = info;
			if (GameCanvas.isTouchNoOrPC())
			{
				tfInput.setFocus(isFocus: true);
			}
			backCMD = right;
			okCMD = left;
		}
		else
		{
			center = cmd;
			left = cmdClose;
			tfInput.setFocus(isFocus: true);
			right = tfInput.cmdClear;
		}
	}

	public void setinfo(string[] info, string name, short Id, iCommand cmd, string strNote)
	{
		if (cmd == null)
		{
			cmd = cmdInputServer;
		}
		listnew = null;
		mStrNote = null;
		IDInputserver = Id;
		isMore = true;
		isNew = false;
		left = null;
		right = null;
		center = null;
		wDia = MotherCanvas.w - 30;
		if (wDia > 160)
		{
			wDia = 160;
		}
		mtfInput = new TField[info.Length];
		strinfo = info;
		this.name = name;
		hDia = (TField.getHeight() + 18) * strinfo.Length + 6 + GameCanvas.hCommand;
		hDia += hTouch + istouchMore;
		if (strNote != null && strNote.Length > 0)
		{
			mStrNote = mFont.tahoma_7b_red.splitFontArray(strNote, wDia - 10);
			hDia += GameCanvas.hText * mStrNote.Length;
		}
		int num = 0;
		if (GameCanvas.isTouch)
		{
			if (hDia > MotherCanvas.h - GameCanvas.hCommand)
			{
				num = hDia;
				hDia = MotherCanvas.h - GameCanvas.hCommand;
			}
		}
		else if (hDia > MotherCanvas.h - GameCanvas.hCommand * 2)
		{
			num = hDia;
			hDia = MotherCanvas.h - GameCanvas.hCommand * 2;
		}
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia + 15;
		if (num > 0)
		{
			if (GameCanvas.isTouch)
			{
				yDia = MotherCanvas.h - GameCanvas.hCommand - hDia + 15;
			}
			else
			{
				yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia + 15;
			}
			listnew = new ListNew(xDia, yDia, wDia, hDia, 0, 0, num - hDia, isLim0: true);
		}
		for (int i = 0; i < mtfInput.Length; i++)
		{
			mtfInput[i] = new TField(xDia + 10, yDia + 8 + (TField.getHeight() + 18) * i + istouchMore + GameCanvas.hCommand, wDia - 20);
			mtfInput[i].setText(string.Empty);
		}
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xDia + wDia - 13, yDia + 13, MainTab.fraCloseTab, string.Empty);
			right = cmdClose;
			cmd.setPos(xDia + wDia / 2, yDia + hDia - iCommand.hButtonCmdNor / 2 - 3, null, cmd.caption);
			left = cmd;
			if (GameCanvas.isTouchNoOrPC())
			{
				mtfInput[0].setFocus(isFocus: true);
				backCMD = right;
				okCMD = left;
			}
		}
		else
		{
			center = cmd;
			left = cmdClose;
		}
		if (!GameCanvas.isTouch)
		{
			mtfInput[0].setFocus(isFocus: true);
			right = mtfInput[0].cmdClear;
		}
	}

	public void setNameDefaul(string[] str)
	{
		if (mtfInput == null)
		{
			return;
		}
		for (int i = 0; i < mtfInput.Length; i++)
		{
			if (i < str.Length)
			{
				mtfInput[i].setText(str[i]);
			}
		}
	}

	public void settfNull(string[] strnull)
	{
		if (mtfInput == null)
		{
			return;
		}
		for (int i = 0; i < mtfInput.Length; i++)
		{
			if (i < strnull.Length)
			{
				mtfInput[i].setStringNull(strnull[i]);
			}
		}
	}

	public void setinfoSmallNew(string info, iCommand cmd, bool isNum, int defValue, int price, string name, string xuluong)
	{
		isMore = false;
		isNew = true;
		left = null;
		right = null;
		center = null;
		if (cmd == null)
		{
			GameCanvas.currentDialog = null;
		}
		this.price = price;
		this.info = info;
		this.name = name;
		this.xuluong = xuluong;
		wDia = MotherCanvas.w / 3 * 2;
		if (wDia > 160)
		{
			wDia = 160;
		}
		string text = "\n" + T.price + " " + price + " " + xuluong;
		strinfo = fontInput.splitFontArray(info + " " + text, wDia - 20);
		hDia = 15 * strinfo.Length + 10 + TField.getHeight() + GameCanvas.hCommand;
		hDia += hTouch + istouchMore;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia + 15;
		int num = wDia - 100;
		if (num < 40)
		{
			num = 40;
		}
		tfInput = new TField(xDia + wDia / 2 - num / 2, yDia + hDia - hTouch - (TField.getHeight() + 8), num);
		if (isNum)
		{
			tfInput.setIputType(TField.INPUT_TYPE_NUMERIC);
		}
		if (defValue >= 0)
		{
			tfInput.setText(string.Empty + defValue);
		}
		strtam = tfInput.getText();
		if (GameCanvas.isTouch)
		{
			cmd.setPos(xDia + wDia / 2, yDia + hDia - iCommand.hButtonCmdNor / 2 - 5, null, cmd.caption);
			cmdClose.setPos(xDia + wDia - 17, yDia + 13, MainTab.fraCloseTab, string.Empty);
			right = cmdClose;
			left = cmd;
		}
		else
		{
			center = cmd;
			left = cmdClose;
			tfInput.setFocus(isFocus: true);
			right = tfInput.cmdClear;
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xDia, yDia, wDia, hDia, 2, 4);
		g.setColor(15972174);
		g.fillRoundRect(xDia + 15, yDia + 5, wDia - 25, 16, 4, 4);
		mFont.tahoma_7b_black.drawString(g, name, xDia + wDia / 2, yDia + 7, 2);
		int num = yDia + istouchMore + GameCanvas.hCommand;
		if (listnew != null)
		{
			if (GameCanvas.isTouch)
			{
				g.setClip(xDia, num - GameCanvas.hCommand / 2, wDia, hDia - GameCanvas.hCommand * 2 - GameCanvas.hCommand / 2);
			}
			else
			{
				g.setClip(xDia, num - GameCanvas.hCommand / 2, wDia, hDia - GameCanvas.hCommand - GameCanvas.hCommand / 2);
			}
			g.saveCanvas();
			g.ClipRec(xDia, num - GameCanvas.hCommand / 2, wDia, hDia - GameCanvas.hCommand * 2 - GameCanvas.hCommand / 2);
			g.translate(0, -listnew.cmx);
		}
		if (isMore)
		{
			for (int i = 0; i < mtfInput.Length; i++)
			{
				fontInput.drawString(g, strinfo[i], MotherCanvas.w / 2, num - 5 + i * (TField.getHeight() + 18), 2);
				mtfInput[i].paintNew(g);
			}
			if (mStrNote != null)
			{
				int num2 = num - 5 + mtfInput.Length * (TField.getHeight() + 18);
				for (int j = 0; j < mStrNote.Length; j++)
				{
					mFont.tahoma_7b_white.drawString(g, mStrNote[j], xDia + 5, num2 + j * GameCanvas.hText, 0);
				}
			}
		}
		else
		{
			for (int k = 0; k < strinfo.Length; k++)
			{
				fontInput.drawString(g, strinfo[k], MotherCanvas.w / 2, num + k * 15 - 5, 2);
			}
			tfInput.paint(g);
		}
		if (listnew != null)
		{
			mGraphics.resetTransAndroid(g);
			g.restoreCanvas();
			GameCanvas.resetTrans(g);
		}
		paintCmd(g);
	}

	public override void keypress(int keyCode)
	{
		if (isMore)
		{
			for (int i = 0; i < mtfInput.Length; i++)
			{
				if (mtfInput[i].isFocused())
				{
					mtfInput[i].keyPressed(keyCode);
					break;
				}
			}
		}
		else
		{
			tfInput.keyPressed(keyCode);
		}
	}

	public override void update()
	{
		updatekey();
		updatePointer();
		if (isMore)
		{
			if (listnew != null)
			{
				listnew.moveCamera();
			}
			for (int i = 0; i < mtfInput.Length; i++)
			{
				mtfInput[i].update();
			}
		}
		else if (tfInput != null)
		{
			tfInput.update();
			if (!GameCanvas.isTouch && right != tfInput.cmdClear)
			{
				right = tfInput.cmdClear;
			}
			if (isNew && tfInput.getText().CompareTo(strtam) != 0)
			{
				strtam = tfInput.getText();
				int num = 0;
				try
				{
					num = int.Parse(strtam);
				}
				catch (Exception)
				{
					num = 0;
				}
				string text = "\n" + T.price + " " + price * num + " " + xuluong;
				strinfo = fontInput.splitFontArray(info + " " + text, wDia - 20);
				hDia = 15 * strinfo.Length + 10 + TField.getHeight() + hTouch + istouchMore + GameCanvas.hCommand;
				int num2 = wDia - 100;
				if (num2 < 40)
				{
					num2 = 40;
				}
				xDia = MotherCanvas.hw - wDia / 2;
				yDia = MotherCanvas.h - GameCanvas.hCommand * 2 - hDia + 15;
				numw = (wDia - 6) / 32;
				numh = (hDia - 6) / 32;
				tfInput.x = xDia + wDia / 2 - num2 / 2;
				tfInput.y = yDia + hDia - (TField.getHeight() + 8) - hTouch;
			}
		}
		base.update();
	}

	public override void updatekey()
	{
		if (isMore)
		{
			if (GameCanvas.keyMyHold[8])
			{
				for (int i = 0; i < mtfInput.Length; i++)
				{
					if (!mtfInput[i].isFocused())
					{
						continue;
					}
					mtfInput[i].setFocus(isFocus: false);
					if (i < mtfInput.Length - 1)
					{
						mtfInput[i + 1].setFocus(isFocus: true);
						if (!GameCanvas.isTouch)
						{
							right = mtfInput[i + 1].cmdClear;
						}
						if (listnew != null)
						{
							listnew.setToX((i + 1) * (TField.getHeight() + 18) - hDia / 4);
						}
					}
					else
					{
						mtfInput[0].setFocus(isFocus: true);
						if (!GameCanvas.isTouch)
						{
							right = mtfInput[0].cmdClear;
						}
						if (listnew != null)
						{
							listnew.setToX(0);
						}
					}
					break;
				}
				GameCanvas.clearKeyHold(8);
			}
			else if (GameCanvas.keyMyHold[2])
			{
				for (int j = 0; j < mtfInput.Length; j++)
				{
					if (!mtfInput[j].isFocused())
					{
						continue;
					}
					mtfInput[j].setFocus(isFocus: false);
					if (j > 0)
					{
						mtfInput[j - 1].setFocus(isFocus: true);
						if (listnew != null)
						{
							listnew.setToX((j - 1) * (TField.getHeight() + 18) - hDia / 4);
						}
					}
					else
					{
						mtfInput[mtfInput.Length - 1].setFocus(isFocus: true);
						if (listnew != null)
						{
							listnew.setToX(listnew.cmxLim);
						}
					}
					break;
				}
				GameCanvas.clearKeyHold(2);
			}
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		if (isMore)
		{
			if (listnew != null)
			{
				listnew.update_Pos_UP_DOWN();
				int y = yDia + istouchMore + GameCanvas.hCommand / 2;
				if (GameCanvas.isPoint(xDia, y, wDia, hDia - GameCanvas.hCommand * 2))
				{
					for (int i = 0; i < mtfInput.Length; i++)
					{
						mtfInput[i].updatePointer(listnew.cmx);
					}
				}
			}
			else
			{
				for (int j = 0; j < mtfInput.Length; j++)
				{
					mtfInput[j].updatePointer();
				}
			}
		}
		else if (tfInput != null)
		{
			tfInput.updatePointer();
		}
		base.updatePointer();
	}
}
