using System;

public class TabSkill : MainTab
{
	private Skill_Info skillCur;

	private mVector vecCmd = new mVector();

	public static iCommand cmdSetPoint;

	public static iCommand cmdSetHotKey;

	private InputDialog input;

	private ListNew list;

	private ListNew listLong;

	private MarqueeText marName = new MarqueeText(0);

	private Scroll scrSkill = new Scroll();

	public static int indexPassive;

	public static int indexJob;

	public static int numCurrentPassive;

	public static int numbuffSkill;

	public TabSkill(string name)
	{
		nameTab = name;
		initCmd();
		list = new ListNew();
		indexIconTab = 3;
		wItemCur = 32;
		if (isBigScreen)
		{
			wItemCur = 36;
		}
		marName = new MarqueeText(wCur - (wItemCur + miniItem * 2 + 3));
	}

	public override void beginFocus()
	{
		skillCur = null;
		int limX = (Player.vecListSkill.size() + 1) * wItemCur - hCur + miniItem * 3 + wItemCur * 2;
		list = new ListNew(xCurBegin, yCurBegin, wCur, hCur, 0, 0, limX, isLim0: true);
		scrSkill.setInfo(xCurBegin + wCur + miniItem, yCurBegin + miniItem / 2, hCur - miniItem * 2, 8809550);
		indexPassive = 0;
		indexJob = 0;
		numCurrentPassive = 0;
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(i);
			if (skill_Info.typeSkill == 1 || skill_Info.typeSkill == 4 || skill_Info.typeSkill == 2)
			{
				indexPassive++;
				indexJob++;
			}
			else if (skill_Info.typeSkill == 3)
			{
				if (skill_Info.Lv_RQ >= 0 && skill_Info.typeDevil == 0)
				{
					numCurrentPassive++;
				}
				indexJob++;
			}
		}
		isShowInfo = false;
		if (GameCanvas.isTouchNoOrPC())
		{
			IdSelect = 0;
			getSkillCur();
		}
		else
		{
			IdSelect = -1;
			setPosCmd(null);
		}
	}

	private void initCmd()
	{
		cmdSetPoint = new iCommand(T.congDiem, 0, this);
		cmdSetHotKey = new iCommand(T.cmdHotKey, 3, this);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			if (skillCur != null)
			{
				if (Player.mLvSkill[skillCur.indexHotKey] >= Skill_Info.maxLv)
				{
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.maxLvSkill);
					vecCmd = skillCur.getActionInven();
				}
				else if (Player.pointSkill > 1)
				{
					input = GameCanvas.Start_Input_Dialog(T.nhappoint, new iCommand(T.cmdSetPoint, 1, 0, this), isNum: true, T.congDiem);
					GameCanvas.subDialog = input;
				}
				else if (Player.pointSkill == 1)
				{
					commandPointer(2, 0);
				}
			}
			break;
		case 1:
			if (skillCur == null)
			{
				break;
			}
			if (subIndex == 0)
			{
				int num = 1;
				try
				{
					num = int.Parse(input.tfInput.getText());
					if (num < 0)
					{
						num = 1;
					}
				}
				catch (Exception)
				{
					num = 1;
				}
				GlobalService.gI().Add_Point_Skill(skillCur.indexHotKey, (short)num);
				GameCanvas.end_Dialog();
			}
			else
			{
				GlobalService.gI().Add_Point_Skill(skillCur.indexHotKey, 1);
				GameCanvas.end_Dialog();
			}
			break;
		case 2:
			GameCanvas.Start_Normal_DiaLog(T.add1Point, new iCommand(T.congDiem, 1, 1, this), isCmdClose: true);
			break;
		case 3:
		{
			if (skillCur == null || skillCur.Lv_RQ <= 0)
			{
				break;
			}
			if (skillCur != null && ((LoadMap.specMap == 4 && skillCur.typeSkill == 1) || (LoadMap.specMap != 4 && skillCur.typeSkill == 4)))
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.khongdungduocmapnay);
				break;
			}
			mVector mVector2 = new mVector();
			for (int i = 0; i < 6; i++)
			{
				if (i != 2 && (GameCanvas.isTouch || i != 5))
				{
					iCommand iCommand2 = null;
					iCommand2 = (GameCanvas.isTouch ? new iCommand(T.keys + " " + (i + 1), 4, i, this) : ((!TField.isQwerty) ? new iCommand(T.keys + " " + (i * 2 + 1), 4, i, this) : new iCommand(T.keys + " " + T.mKeyQty[i], 4, i, this)));
					mVector2.addElement(iCommand2);
				}
			}
			GameCanvas.menu.startAt(mVector2, 2, T.cmdHotKey);
			break;
		}
		case 4:
			if (skillCur != null)
			{
				MainSkill mainSkill = new MainSkill(skillCur.ID, -1);
				mainSkill.indexHotKey = skillCur.indexHotKey;
				mainSkill.idIcon = skillCur.idIcon;
				mainSkill.isBuff = skillCur.typeSkill == 2;
				Player.setHotKey(subIndex, mainSkill, null);
				Interface_Game.timePaintIconSkill = 100;
			}
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xCurBegin, yCurBegin, wCur, hCur - miniItem, 0, 4);
		g.saveCanvas();
		g.ClipRec(xCurBegin + 2, yCurBegin + 1, wCur - 4, hCur - 1 - miniItem - 1);
		setClip(g);
		int num = miniItem;
		int num2 = 0;
		AvMain.Font3dWhite(g, T.skillActive, num, num2 + wItemCur / 2 - 2, 0);
		num2 += wItemCur;
		if (IdSelect >= 0 && IdSelect < Player.vecListSkill.size() && GameCanvas.currentScreen.setCurTypetab(1))
		{
			paintSelect(g);
		}
		int num3 = 0;
		int num4 = 0;
		num3 = list.cmx / wItemCur - 2;
		num4 = hCur / wItemCur + 1 + num3;
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			if (i >= num3 && i <= num4)
			{
				Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(i);
				string st = skill_Info.name;
				if (skill_Info.Lv_RQ == -1)
				{
					AvMain.fraQuest.drawFrame(3, num + wItemCur / 2, num2 + wItemCur / 2, 0, 3, g);
					st = T.skillnew;
				}
				else
				{
					skill_Info.paint(g, num + wItemCur / 2, num2 + wItemCur / 2);
				}
				if (skill_Info.Lv_RQ == Skill_Info.maxLv)
				{
					st = skill_Info.name + T.max;
				}
				if (skill_Info.Lv_RQ < 0)
				{
					mFont.tahoma_7b_blue.drawString(g, st, num + wItemCur + miniItem, num2 + miniItem / 2 + 1, 0);
				}
				else if (IdSelect == i && GameCanvas.currentScreen.setCurTypetab(1) && marName.isRun)
				{
					g.setClip(num + wItemCur + miniItem, num2, marName.maxW - 1, wItemCur);
					mFont.tahoma_7b_white.drawString(g, st, num + wItemCur + miniItem - marName.xplus, num2 + miniItem / 2 + 1, 0);
					setClip(g);
				}
				else
				{
					mFont.tahoma_7b_white.drawString(g, st, num + wItemCur + miniItem, num2 + miniItem / 2 + 1, 0);
				}
				if (skill_Info.Lv_RQ == -1)
				{
					mFont.tahoma_7_blue.drawString(g, T.gapGrap, num + wItemCur + miniItem, num2 + miniItem / 2 + GameCanvas.hText + 1, 0);
				}
				else
				{
					if (skill_Info.typeDevil == 1)
					{
						string empty = string.Empty;
						empty = ((skill_Info.typeSkill == 1) ? T.devilfruitA : ((skill_Info.typeSkill != 3) ? T.devilfruitB : T.devilfruitP));
						mFont.tahoma_7_green.drawString(g, empty, num + wItemCur + miniItem, num2 + miniItem / 2 + GameCanvas.hText + 1, 0);
					}
					else if (skill_Info.typeSkill == 3 || skill_Info.typeSkill == 2)
					{
						mFont.tahoma_7_green.drawString(g, T.Lv + ": " + skill_Info.Lv_RQ + " " + skill_Info.getStrType(), num + wItemCur + miniItem, num2 + miniItem / 2 + GameCanvas.hText + 1, 0);
					}
					else if (skill_Info.Lv_RQ >= Skill_Info.maxLv)
					{
						mFont.tahoma_7_green.drawString(g, T.maxLv, num + wItemCur + miniItem, num2 + miniItem / 2 + GameCanvas.hText + 1, 0);
					}
					else if (GameCanvas.isSmallScreen)
					{
						mFont.tahoma_7_green.drawString(g, T.Lv + ": " + skill_Info.Lv_RQ + "+" + MainItem.strGetPercent(skill_Info.percentLv, 1), num + wItemCur + miniItem, num2 + miniItem / 2 + GameCanvas.hText + 1, 0);
					}
					else
					{
						mFont.tahoma_7_green.drawString(g, T.Lv + ": " + skill_Info.Lv_RQ, num + wItemCur + miniItem, num2 + miniItem / 2 + GameCanvas.hText + 1, 0);
						Interface_Game.PaintHPMP(g, 3, skill_Info.percentLv, 100, num + wItemCur * 2, num2 + miniItem / 2 + GameCanvas.hText + 1, 0, miniItem * 2, wCur / 2, 1, isflip: false, 0, isUpdateEff: false, 0);
					}
					if (skill_Info.phanTramDevilSkill > 0)
					{
						int num5 = skill_Info.phanTramDevilSkill / 5;
						num5 = ((num5 < 20) ? (num5 + 1) : ((num5 != 20) ? 22 : (num5 + 2)));
						g.drawRegion(AvMain.imgLvDevilSkill, 0, 22 - num5, 22, num5, 0, num + wItemCur / 2, num2 + wItemCur / 2 + 11, 33);
					}
				}
			}
			num2 += wItemCur;
			if (i == indexPassive - 1)
			{
				AvMain.Font3dWhite(g, T.skillPassive + ": " + numCurrentPassive + "/" + Player.numPassive, num, num2 + wItemCur / 2 - 6, 0);
				num2 += wItemCur;
			}
			if (i == indexJob - 1)
			{
				AvMain.Font3dWhite(g, T.skillJob, num, num2 + wItemCur / 2 - 6, 0);
				num2 += wItemCur;
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (!GameCanvas.currentScreen.setCurTypetab(1))
		{
			return;
		}
		if (list.cmxLim > 0)
		{
			scrSkill.paint(g);
		}
		if (isShowInfo && skillCur != null)
		{
			bool isLv = false;
			if (skillCur.typeSkill == 1 || skillCur.typeSkill == 4)
			{
				isLv = true;
			}
			ShowInfo(g, skillCur, null, 0, xInfo, yInfo, isLv, null, 0);
		}
		if (vecCmd != null && GameCanvas.getShowCmd())
		{
			for (int j = 0; j < vecCmd.size(); j++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		base.paint(g);
	}

	public void setClip(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(xCurBegin + 2, yCurBegin + 1, wCur - 4, hCur - 1 - miniItem - 1);
		g.translate(xCurBegin, yCurBegin);
		g.translate(0, -list.cmx);
	}

	public void paintSelect(mGraphics g)
	{
		int num = wItemCur;
		if (IdSelect >= indexPassive)
		{
			num += wItemCur;
		}
		if (IdSelect >= indexJob)
		{
			num += wItemCur;
		}
		g.setColor(16446420);
		g.fillRect(miniItem / 2, num + IdSelect * wItemCur, 1, wItemCur);
		g.fillRect(wCur - miniItem / 2 - 1, num + IdSelect * wItemCur, 1, wItemCur);
		g.fillRect(miniItem / 2 + 1, num + IdSelect * wItemCur - 1, wCur - miniItem - 1, 1);
		g.fillRect(miniItem / 2 + 1, num + IdSelect * wItemCur + wItemCur, wCur - miniItem - 1, 1);
	}

	public override void update()
	{
		int cmx = list.cmx;
		list.moveCamera();
		scrSkill.setYScrool(list.cmx, list.cmxLim);
		if (list.cmx != cmx || list.pointerIsDowning)
		{
			isShowInfo = false;
		}
		else if (skillCur != null)
		{
			updateShowInfo();
		}
		marName.update2();
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(1))
		{
			IdSelect--;
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			IdSelect++;
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (GameCanvas.keyMove(0) || GameCanvas.keyMove(2))
		{
			GameCanvas.currentScreen.setTypeTab(0);
			GameCanvas.ClearkeyMove(0);
			GameCanvas.ClearkeyMove(2);
		}
		if (flag)
		{
			IdSelect = AvMain.resetSelect(IdSelect, Player.vecListSkill.size() - 1, isreset: false);
			if (IdSelect >= 0)
			{
				if (GameCanvas.isTouchNoOrPC())
				{
					int num = (IdSelect + 2) * wItemCur - hCur / 2;
					if (IdSelect >= indexPassive)
					{
						num += wItemCur;
					}
					if (IdSelect >= indexJob)
					{
						num += wItemCur;
					}
					list.setToX(num);
				}
				getSkillCur();
				isShowInfo = false;
			}
			else
			{
				skillCur = null;
			}
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointerSelect && Player.vecListSkill.size() > 0 && GameCanvas.isPoint(xCurBegin, yCurBegin, wCur, hCur))
		{
			int num = (GameCanvas.py - yCurBegin + list.cmx) / wItemCur;
			if (num != 0 && num != indexPassive + 1 && num != indexJob + 1)
			{
				num = ((num > indexJob) ? (num - 3) : ((num <= indexPassive) ? (num - 1) : (num - 2)));
				if (num != IdSelect && num >= 0 && num < Player.vecListSkill.size())
				{
					IdSelect = num;
					getSkillCur();
				}
			}
			GameCanvas.isPointerSelect = false;
		}
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

	public void getSkillCur()
	{
		if (IdSelect < 0 || IdSelect >= Player.vecListSkill.size())
		{
			return;
		}
		Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(IdSelect);
		if (skill_Info.Lv_RQ == -1)
		{
			skillCur = null;
		}
		else if (skillCur == null || skillCur != skill_Info)
		{
			skillCur = skill_Info;
			if (skillCur != null)
			{
				setPosCmd(skillCur.getActionInven());
			}
			marName.setdata(skill_Info.name, mFont.tahoma_7b_black);
		}
	}

	public override void setPosInfo()
	{
		if (skillCur.Lv_RQ != -1)
		{
			if (skillCur != null)
			{
				skillCur.setVecInfo(skillCur.wInfo);
			}
			setPosInfo(skillCur, xCurBegin + MainTab.wTab, yCurBegin + (IdSelect + 1) * wItemCur - list.cmx + 1 + miniItem * 2);
		}
	}

	public void setPosCmd(mVector vec)
	{
		left = null;
		center = null;
		vecCmd.removeAllElements();
		if (vec == null || MainTab.bigInfo > 0)
		{
			return;
		}
		if (GameCanvas.isTouch)
		{
			vecCmd = vec;
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand cmd = (iCommand)vecCmd.elementAt(i);
				cmd = AvMain.setPosCMD(cmd, i);
				if (i == 0)
				{
					okCMD = cmd;
				}
				if (i == 1)
				{
					menuCMD = cmd;
				}
			}
			return;
		}
		for (int j = 0; j < vec.size(); j++)
		{
			iCommand iCommand2 = (iCommand)vec.elementAt(j);
			if (j == 0)
			{
				center = iCommand2;
			}
			if (j == 1)
			{
				left = iCommand2;
			}
		}
	}
}
