public class ScreenUpgradeSkillDevil : ScreenUpgrade
{
	private mVector vecSkill = new mVector("ScreenUpgradeSkillDevil.vecSkill");

	private int wItemCur = 34;

	private int miniItem = 5;

	private MarqueeText marName = new MarqueeText(0);

	public new static ScreenUpgradeSkillDevil instance;

	public ScreenUpgradeSkillDevil()
	{
		typeRebuild = 9;
		nameScreen = T.upgradeDevilSkill;
		Step = 0;
		lech = 10;
		w = 290;
		h = 180;
		wInven = w / 2;
		hInven = h - lech * 3;
		wItem = 24;
		if (GameCanvas.isTouch)
		{
			wItem = 28;
		}
		if (w > MotherCanvas.w)
		{
			w = MotherCanvas.w;
		}
		if (h > MotherCanvas.h - GameCanvas.hCommand * 2)
		{
			h = MotherCanvas.h - GameCanvas.hCommand * 2;
		}
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2 + 5;
		xInven = x + 5;
		yInven = y + 20;
		xTiLe = xInven + wInven + 4;
		vecSkill.removeAllElements();
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(i);
			if (skill_Info.Lv_RQ >= 0 && skill_Info.typeSkill == 1)
			{
				vecSkill.addElement(skill_Info);
			}
		}
		int limX = vecSkill.size() * wItemCur - hInven + 10;
		list = new ListNew(x, y + 30, w, hInven, 0, 0, limX, isLim0: true);
		cmdClose = new iCommand(T.close, -1, this);
		cmdBovao = new iCommand(T.bovao, 0, this);
		cmdUpgrade = new iCommand(T.Upgrade, 1, this);
		if (GameCanvas.isTouch)
		{
			cmdUpgrade.setPos(x + wInven + (w - lech * 3 - wInven) / 2 + wItem / 2, y + h - iCommand.hButtonCmdNor / 2 - 5, null, cmdUpgrade.caption);
			cmdClose.setPos(x + w - 15, y - 15 + 10 + 8, MainTab.fraCloseTab, string.Empty);
			cmdBovao = AvMain.setPosCMD(cmdBovao, 1);
			vecCmd.addElement(cmdClose);
			vecCmd.addElement(cmdUpgrade);
			vecCmd.addElement(cmdBovao);
			if (GameCanvas.isTouchNoOrPC())
			{
				backCMD = cmdClose;
				menuCMD = cmdBovao;
				okCMD = cmdUpgrade;
			}
		}
		else
		{
			right = cmdClose;
			left = cmdBovao;
			center = cmdUpgrade;
		}
		int num = x + wInven + (w - lech * 3 - wInven) / 6;
		int num2 = y + h / 2 - wItem / 2;
		posUp = mSystem.new_M_Int(2, 2);
		posUp[0][0] = num;
		posUp[0][1] = num2;
		posUp[1][0] = num + (w - lech * 3 - wInven) / 6 * 4;
		posUp[1][1] = num2;
		marName = new MarqueeText(wInven - wItemCur - 10);
		begin();
	}

	public void begin()
	{
		ScreenUpgrade.mItemUpgrade = new MainItem[posUp.Length];
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
		case 0:
			if (IdSelect >= 0 && IdSelect < vecSkill.size())
			{
				Skill_Info skill_Info = (Skill_Info)vecSkill.elementAt(IdSelect);
				GlobalService.gI().Devil_Upgrade(9, skill_Info.ID, 104, 0);
			}
			break;
		case 1:
			if (ScreenUpgrade.mItemUpgrade[0] != null)
			{
				GlobalService.gI().Devil_Upgrade(12, ScreenUpgrade.mItemUpgrade[0].ID, ScreenUpgrade.mItemUpgrade[0].typeObject, 0);
			}
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
		paintBackGr(g);
		setClip(g);
		paintSkill(g);
		GameCanvas.resetTrans(g);
		Interface_Game.paintNumMess(g, -Interface_Game.xNumMess + 8, -Interface_Game.yNumMess + 3);
		paintEff(g, -1);
		paintPosItem(g);
		paintTiLe(g);
		if (vecCmd != null && GameCanvas.getShowCmd())
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		paintSuper(g);
		Interface_Game.paintEffShowMoney(g, MotherCanvas.hw - Interface_Game.wMoneyEff / 2, GameScreen.h12plus, isShow: true, 0);
		paintEff(g, 0);
	}

	public override void paintTiLe(mGraphics g)
	{
		mFont.tahoma_7b_black.drawString(g, T.tile + "50%", xTiLe, yInven, 0);
	}

	private void paintSkill(mGraphics g)
	{
		int num = miniItem;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		num3 = list.cmx / wItemCur - 1;
		num4 = hInven / wItemCur + 2 + num3;
		if (IdSelect >= 0 && IdSelect < vecSkill.size())
		{
			paintSelect(g);
		}
		for (int i = 0; i < vecSkill.size(); i++)
		{
			if (i >= num3 && i <= num4)
			{
				Skill_Info skill_Info = (Skill_Info)vecSkill.elementAt(i);
				string name = skill_Info.name;
				skill_Info.paint(g, num + wItemCur / 2, num2 + wItemCur / 2);
				if (i == 0)
				{
					g.setColor(14533533);
					g.fillRect(num + 1, num2 - 1, wInven - 1, 2);
					g.fillRect(num + 1 + wInven - 1, num2, 1, 2);
				}
				g.setColor(14533533);
				g.fillRect(num + 1, num2 + wItemCur - 1, wInven - 1, 2);
				g.fillRect(num + 1 + wInven - 1, num2 + wItemCur, 1, 2);
				if (IdSelect == i && marName.isRun)
				{
					g.setClip(num + wItemCur + miniItem, num2, marName.maxW, wItemCur);
					mFont.tahoma_7b_white.drawString(g, name, num + wItemCur + miniItem - marName.xplus, num2 + miniItem / 2 + 1, 0);
					setClip(g);
				}
				else
				{
					mFont.tahoma_7b_white.drawString(g, name, num + wItemCur + miniItem, num2 + miniItem / 2 + 1, 0);
				}
				mFont.tahoma_7_black.drawString(g, T.lvDevil + skill_Info.LvDevilSkill + " + " + skill_Info.phanTramDevilSkill + "%", num + wItemCur + miniItem, num2 + miniItem / 2 + GameCanvas.hText + 1, 0);
			}
			num2 += wItemCur;
		}
	}

	public void setClip(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setClip(x + 2, y + 20 + 2, wInven + 4, hInven - 1 - miniItem - 1 + 5);
		g.translate(x, y + 25);
		g.translate(0, -list.cmx);
	}

	public void paintSelect(mGraphics g)
	{
		g.setColor(14729352);
		g.fillRect(miniItem + 2, IdSelect * wItemCur + 2, wInven - 3, wItemCur - 4);
		g.fillRect(miniItem + wInven - 1, IdSelect * wItemCur + 3, 1, wItemCur - 5);
	}

	public override void update()
	{
		if (lastScreen != null)
		{
			lastScreen.update();
		}
		list.moveCamera();
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
		setStep();
		marName.update2();
	}

	public override void setStep()
	{
		updateKHAM();
	}

	public override void updatekey()
	{
		if (Step != 0)
		{
			return;
		}
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
		if (flag)
		{
			int num = IdSelect * wItemCur - hInven / 2;
			if (IdSelect > 0)
			{
				num += wItemCur / 2;
			}
			list.setToX(num);
			getItemCurNew();
		}
		updatekeySuper();
		updatekeyPC();
	}

	public override void getItemCurNew()
	{
		IdSelect = AvMain.resetSelect(IdSelect, vecSkill.size() - 1, isreset: false);
		if (IdSelect >= 0 && IdSelect < vecSkill.size())
		{
			Skill_Info skill_Info = (Skill_Info)vecSkill.elementAt(IdSelect);
			marName.setdata(skill_Info.name, mFont.tahoma_7b_black);
		}
	}

	public override void updatePointer()
	{
		if (Step != 0)
		{
			return;
		}
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointSelect(x, y, wInven, hInven))
		{
			int num = (GameCanvas.py - yInven + list.cmx) / wItemCur;
			int num2 = vecSkill.size();
			if (num >= 0 && num < num2)
			{
				GameCanvas.isPointerSelect = false;
				if (num == IdSelect)
				{
					cmdBovao.perform();
				}
				else
				{
					isShowInfo = false;
					IdSelect = num;
					getItemCurNew();
				}
			}
			else
			{
				isShowInfo = false;
				IdSelect = -1;
			}
		}
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		updatePointerSuper();
	}

	public override void updateNewUpgrade()
	{
		if (ScreenUpgrade.mItemUpgrade[0] == null)
		{
			return;
		}
		Skill_Info skill_Info = null;
		for (int i = 0; i < Player.vecListSkill.size(); i++)
		{
			Skill_Info skill_Info2 = (Skill_Info)Player.vecListSkill.elementAt(i);
			if (skill_Info2.ID == ScreenUpgrade.mItemUpgrade[0].ID)
			{
				skill_Info = skill_Info2;
				break;
			}
		}
		if (skill_Info == null)
		{
			return;
		}
		for (int j = 0; j < vecSkill.size(); j++)
		{
			Skill_Info skill_Info3 = (Skill_Info)vecSkill.elementAt(j);
			if (skill_Info3.ID == ScreenUpgrade.mItemUpgrade[0].ID)
			{
				skill_Info3.LvDevilSkill = skill_Info.LvDevilSkill;
				skill_Info3.phanTramDevilSkill = skill_Info.phanTramDevilSkill;
				break;
			}
		}
	}
}
