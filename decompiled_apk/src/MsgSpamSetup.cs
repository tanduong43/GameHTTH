public class MsgSpamSetup : MsgDialog
{
	public static int[] valueSetup;

	private int hItem;

	public const sbyte CHECK_CHAT = 1;

	public const sbyte CHECK_ADD_FRIEND = 2;

	public const sbyte CHECK_PARTY = 3;

	public const sbyte CHECK_TRADE = 4;

	public const sbyte CHECK_FIGHT = 5;

	public static bool isDontShowFriendList;

	public void setinfo()
	{
		valueSetup = new int[7];
		for (int i = 0; i < valueSetup.Length; i++)
		{
			if (i != 0)
			{
				valueSetup[i] = 1;
			}
		}
		wDia = 180;
		maxWShow = wDia;
		hItem = 24;
		wItem = 26;
		if (wDia < 210)
		{
			wItem = 20;
		}
		hDia = 190;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 + 10;
		if (GameCanvas.isTouch)
		{
			miniItem = 28;
		}
		cmdList = new mVector();
		iCommand iCommand2 = new iCommand(T.xong, 15, this);
		if (!GameCanvas.isTouch)
		{
			iCommand2 = (right = AvMain.setPosCMD(iCommand2, 2));
		}
		else
		{
			iCommand2.setPos(xDia + wDia + 3, yDia - 5, MainTab.fraCloseTab, string.Empty);
			backCMD = iCommand2;
		}
		cmdList.addElement(iCommand2);
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia + hItem / 2;
		int num2 = xDia + 15;
		paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia - 7, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
		g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		if (GameCanvas.isTouchNoOrPC())
		{
			paintSelect(g, num2 + 3, num - hItem / 4 - 1, wDia - 36);
		}
		for (int i = 0; i < T.mSpam.Length; i++)
		{
			int num3 = num2 + 5;
			mFont.tahoma_7b_black.drawString(g, T.mSpam[i], num3 + 14, num, 0);
			g.drawRegion(AvMain.imgDaKham, 0, 0, 20, 20, 0, num3, num + 6, 3);
			if (valueSetup[i] == 1)
			{
				AvMain.fraCheck.drawFrame(2, num2 + 5, num + 5, 0, 3, g);
			}
			else
			{
				AvMain.fraCheck.drawFrame(1, num2 + 5, num + 5, 0, 3, g);
			}
			num += hItem;
		}
		paintInfoHelp(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (cmdList != null)
		{
			for (int j = 0; j < cmdList.size(); j++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	public void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus)
	{
		g.setColor(16774758);
		xbegin -= miniItem;
		g.fillRect(xbegin, ybegin + idSelect * hItem, wFocus + miniItem * 2, hItem);
	}

	public override void updatekey()
	{
		if (GameCanvas.keyMove(1))
		{
			if (idSelect > 0)
			{
				idSelect--;
			}
			GameCanvas.ClearkeyMove(1);
		}
		else if (GameCanvas.keyMove(3))
		{
			if (idSelect < valueSetup.Length)
			{
				idSelect++;
			}
			GameCanvas.ClearkeyMove(3);
		}
		else if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (valueSetup[idSelect] == 1)
			{
				valueSetup[idSelect] = 0;
			}
			else
			{
				valueSetup[idSelect] = 1;
			}
		}
	}

	public override void updatePointer()
	{
		int num = yDia + 5;
		int num2 = xDia;
		for (int i = 0; i < valueSetup.Length; i++)
		{
			if (GameCanvas.isPointSelect(num2 + 5, num, wDia - 25, hItem))
			{
				if (valueSetup[i] == 1)
				{
					valueSetup[i] = 0;
				}
				else
				{
					valueSetup[i] = 1;
				}
				GameCanvas.isPointerSelect = false;
				setInfoHelp(T.mHelpSpam[i]);
				idSelect = i;
				return;
			}
			num += hItem;
		}
		base.updatePointer();
	}

	public static bool isCheckSpam(int index, string str)
	{
		if (valueSetup == null)
		{
			return false;
		}
		if (index < 0 || index >= valueSetup.Length)
		{
			return false;
		}
		if (valueSetup[0] == 1)
		{
			return true;
		}
		if (valueSetup[index] == 1)
		{
			return false;
		}
		if (index == 1)
		{
			if (FriendList.isGetListFriend)
			{
				for (int i = 0; i < Player.vecFriendList.size(); i++)
				{
					if (((InfoMemList)Player.vecFriendList.elementAt(i)).name.CompareTo(str) == 0)
					{
						return false;
					}
				}
			}
			else if (!isDontShowFriendList)
			{
				GlobalService.gI().Friend(2, 0);
				isDontShowFriendList = true;
			}
			return true;
		}
		if (valueSetup[index] == 0)
		{
			return true;
		}
		return false;
	}

	public static void saveClose()
	{
		if (valueSetup != null)
		{
			CRes.saveRMS("SUB_SPAM", new sbyte[1] { (sbyte)valueSetup[valueSetup.Length - 1] });
		}
	}
}
