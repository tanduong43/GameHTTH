public class MsgAutoFire : MsgDialog
{
	private int hItem;

	private int valueFire;

	private int valueRevice;

	public static short[][] value;

	private int xbeginSkill;

	private int xbeginBuff;

	private int indexBuff;

	private iCommand cmdOK;

	public override void commandPointer(int index, int subIndex)
	{
		if (index != 9)
		{
			return;
		}
		isClose = true;
		if (valueFire == 0)
		{
			Player.typeAutoFireMain = 1;
			if (Player.AutoFireCur == 2)
			{
				Player.AutoFireCur = 1;
			}
		}
		else if (valueFire == 1)
		{
			Player.typeAutoFireMain = 2;
			if (Player.AutoFireCur == 1)
			{
				Player.AutoFireCur = 2;
			}
		}
		else
		{
			Player.typeAutoFireMain = -1;
			Player.AutoFireCur = -1;
		}
		if (value != null)
		{
			Player.typeAutoBuff = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i][1] == 1)
				{
					Player.typeAutoBuff = 1;
				}
			}
		}
		Player.AutoRevice = (sbyte)valueRevice;
		GameCanvas.saveRms.SaveAutoFire();
	}

	public void setinfoAuto_Fire()
	{
		fontDia = mFont.tahoma_7b_black;
		beginDia();
		cmdList = new mVector();
		cmdOK = new iCommand(T.xong, 9, this);
		cmdList.addElement(cmdOK);
		wDia = MotherCanvas.w;
		if (wDia > 180)
		{
			wDia = 180;
		}
		maxWShow = wDia;
		wShowPaper = 5;
		hItem = 28;
		wItem = 24;
		hDia = 160;
		int num = 0;
		short[] array = new short[10];
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(i);
			if (skill_Info.Lv_RQ >= 0 && skill_Info.typeSkill == 2)
			{
				array[num] = skill_Info.ID;
				num++;
			}
		}
		if (num > 0)
		{
			value = new short[num][];
			for (int j = 0; j < value.Length; j++)
			{
				value[j] = new short[2];
				value[j][0] = array[j];
				value[j][1] = 1;
			}
		}
		if (num > 3)
		{
			hDia += hItem;
		}
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 - 5;
		indexBuff = 0;
		valueRevice = Player.AutoRevice;
		xbeginSkill = fontDia.getWidth(T.fire);
		xbeginBuff = fontDia.getWidth(T.buff);
		setPosCmdNew(-2, isLast: false);
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia;
		int num2 = xDia + 15;
		paintPaper(g, MotherCanvas.hw - wShowPaper / 2, num, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
		g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		g.saveCanvas();
		g.ClipRec(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		num += 12;
		g.setColor(15972174);
		g.fillRoundRect(xDia + 10, num, wDia - 20, 16, 4, 4);
		num += 3;
		AvMain.FontBorderColor(g, T.setting, xDia + wDia / 2, num, 2, 6, 5);
		int num3 = 0;
		num3 = ((value != null) ? ((value.Length - 1) / 3) : 0);
		num += hItem;
		if (GameCanvas.isTouchNoOrPC() && idSelect < 3)
		{
			int h = hItem;
			int y = num - hItem / 4 - 1 + idSelect * hItem;
			if (idSelect == 1)
			{
				h = hItem * (num3 + 1);
			}
			if (idSelect == 2)
			{
				y = num - hItem / 4 - 1 + (idSelect + num3) * hItem;
			}
			paintSelect(g, xDia, y, wDia, h);
		}
		mFont.tahoma_7b_brown.drawString(g, T.fire, num2, num, 0);
		AvMain.Font3dColor(g, " " + T.mAutoFire[valueFire], num2 + xbeginSkill, num, 0, 0, 7);
		num += hItem;
		mFont.tahoma_7b_brown.drawString(g, T.buff, num2, num, 0);
		if (value == null)
		{
			mFont.tahoma_7_black.drawString(g, " " + T.buffnull, num2 + xbeginBuff, num, 0);
		}
		else
		{
			for (int i = 0; i < value.Length; i++)
			{
				Skill_Info skillFromID = Skill_Info.getSkillFromID(value[i][0]);
				int num4 = num2 + xbeginBuff + wItem / 2 + (wItem + 8) * (i % 3);
				int num5 = num + wItem / 4 + i / 3 * hItem;
				if (idSelect == 1 && indexBuff == i)
				{
					g.setColor(16777215);
					g.drawRect(num4 - hItem / 2 - 1, num5 - hItem / 2 - 1, hItem + 1, hItem);
					g.setColor(0);
					g.drawRect(num4 - hItem / 2, num5 - hItem / 2, hItem - 2 + 1, hItem - 2);
				}
				Skill_Info.paintIcon(g, num4, num5, skillFromID.idIcon, skillFromID.LvDevilSkill);
				if (value[i][1] == 0)
				{
					AvMain.fraDelay2.drawFrame(0, num4, num5, 0, 3, g);
				}
			}
		}
		num += hItem * (num3 + 1);
		g.drawImage(AvMain.imgBorderCombo, num2 + 5, num + 5, 3);
		if (valueRevice == 1)
		{
			AvMain.fraCheck.drawFrame(2, num2 + 5, num + 5, 0, 3, g);
		}
		mFont.tahoma_7b_brown.drawString(g, T.autoRevice, num2 + 15, num, 0);
		paintInfoHelp(g);
		if (cmdList != null)
		{
			for (int j = 0; j < cmdList.size(); j++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		g.restoreCanvas();
	}

	public override void paintSelect(mGraphics g, int xbegin, int ybegin, int wFocus, int hFocus)
	{
		g.setColor(16774758);
		g.fillRect(xbegin + miniItem, ybegin, wFocus - miniItem * 2, hFocus);
	}

	public override void update()
	{
		updateInfoHelp();
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
		if (GameCanvas.isTouchNoOrPC())
		{
			if (idSelect == 3)
			{
				cmdOK.isSelect = true;
			}
			else
			{
				cmdOK.isSelect = false;
			}
		}
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
			if (idSelect < 3)
			{
				idSelect++;
			}
			GameCanvas.ClearkeyMove(3);
		}
		else if (GameCanvas.keyMove(0))
		{
			setSelect(-1);
			GameCanvas.ClearkeyMove(0);
		}
		else if (GameCanvas.keyMove(2))
		{
			setSelect(1);
			GameCanvas.ClearkeyMove(2);
		}
		else if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (idSelect == 1)
			{
				if (value != null)
				{
					if (value[indexBuff][1] == 0)
					{
						value[indexBuff][1] = 1;
					}
					else
					{
						value[indexBuff][1] = 0;
					}
					Skill_Info skillFromID = Skill_Info.getSkillFromID(value[indexBuff][0]);
					if (skillFromID != null)
					{
						setInfoHelp(T.mHelpAutoFire[3 + value[indexBuff][1]] + skillFromID.name);
					}
				}
			}
			else if (idSelect == 3 && cmdList != null && idCommand < cmdList.size())
			{
				((iCommand)cmdList.elementAt(idCommand)).perform();
			}
		}
		updatekeyPC();
	}

	public void setSelect(int plus)
	{
		if (idSelect == 0)
		{
			valueFire += plus;
			if (valueFire < 0)
			{
				valueFire = 0;
			}
			if (valueFire >= T.mAutoFire.Length)
			{
				valueFire = T.mAutoFire.Length - 1;
			}
			string text = T.mHelpAutoFire[valueFire];
			if (valueFire == 1)
			{
				Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(0);
				if (skill_Info != null)
				{
					text += skill_Info.name;
				}
			}
			setInfoHelp(text);
		}
		else if (idSelect == 1)
		{
			if (value != null)
			{
				indexBuff += plus;
				if (indexBuff < 0)
				{
					indexBuff = 0;
				}
				if (indexBuff >= value.Length)
				{
					indexBuff = value.Length - 1;
				}
			}
		}
		else if (idSelect == 2)
		{
			if (valueRevice == 0)
			{
				valueRevice = 1;
			}
			else
			{
				valueRevice = 0;
			}
			setInfoHelp(T.helpAutoRevice);
		}
	}

	public override void updatePointer()
	{
		int num = yDia;
		int num2 = xDia + 15;
		num += 15 + wItem - wItem / 4;
		if (GameCanvas.isPointerSelect)
		{
			if (GameCanvas.isPoint(num2 + 10, num, wDia, hItem))
			{
				valueFire++;
				if (valueFire >= T.mAutoFire.Length)
				{
					valueFire = 0;
				}
				string text = T.mHelpAutoFire[valueFire];
				GameCanvas.isPointerSelect = false;
				if (valueFire == 1)
				{
					Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(0);
					if (skill_Info != null)
					{
						text += skill_Info.name;
					}
				}
				setInfoHelp(text);
			}
			int num3 = 0;
			num3 = ((value != null) ? ((value.Length - 1) / 3) : 0);
			int num4 = (num3 + 1) * hItem;
			if (GameCanvas.isPoint(num2 + 10, num + hItem, wDia, num4) && value != null)
			{
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						int num5 = num2 + xbeginBuff + (wItem + 8) * (i % 3);
						int y = num + hItem - wItem / 4 + i / 3 * hItem;
						if (GameCanvas.isPoint(num5 - 4, y, hItem + 8, hItem))
						{
							if (value[i][1] == 0)
							{
								value[i][1] = 1;
							}
							else
							{
								value[i][1] = 0;
							}
							Skill_Info skillFromID = Skill_Info.getSkillFromID(value[i][0]);
							if (skillFromID != null)
							{
								setInfoHelp(T.mHelpAutoFire[3 + value[i][1]] + skillFromID.name);
							}
						}
					}
				}
				GameCanvas.isPointerSelect = false;
			}
			if (GameCanvas.isPoint(num2, num + hItem + num4, wDia, hItem))
			{
				if (valueRevice == 0)
				{
					valueRevice = 1;
				}
				else
				{
					valueRevice = 0;
				}
				setInfoHelp(T.helpAutoRevice);
				GameCanvas.isPointerSelect = false;
			}
		}
		base.updatePointer();
	}
}
