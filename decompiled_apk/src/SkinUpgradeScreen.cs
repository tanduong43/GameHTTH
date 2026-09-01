public class SkinUpgradeScreen : ScreenUpgrade
{
	public new static SkinUpgradeScreen instance;

	public static mVector vecSkin = new mVector();

	public static mVector vecDa = new mVector();

	public ListNew listDa;

	private int yInven2;

	private int idSelect2;

	public static sbyte[] mTileCuongHoa = null;

	public SkinUpgradeScreen(sbyte typeRebuild, int size, mVector vecSkin1, mVector vecDa1)
		: base(typeRebuild, size)
	{
		mSystem.outz("SkinUpgradeScreen type " + typeRebuild + " vecSkin " + vecSkin.size() + " vecDa " + vecDa.size());
		vecSkin = vecSkin1;
		vecDa = vecDa1;
		IdSelect = 0;
		idSelect2 = -1;
		wInven = w / 2;
		wInven -= wInven % wItem;
		hInven = h - lech * 3 - 5;
		maxNumItemW = wInven / wItem;
		xInven = x + w / 4 - wInven / 2 + 15;
		yInven = y + h / 2 - hInven / 2;
		yInven2 = yInven + hInven / 2 + 4;
		hInven = hInven / 2 - 4;
		int limX = ((vecSkin.size() - 1) / maxNumItemW + 1) * wItem - hInven;
		list = new ListNew(xInven, yInven, wInven, hInven, 0, 0, limX, isLim0: true);
		limX = ((vecDa.size() - 1) / maxNumItemW + 1) * wItem - hInven;
		listDa = new ListNew(xInven, yInven2, wInven, hInven, 0, 0, limX, isLim0: true);
		isShowInfo = false;
		ScreenUpgrade.valueTile = 0;
	}

	public override void paintInven(mGraphics g)
	{
		g.setClip(xInven - 1, yInven + 1, wInven + 2, hInven - 1);
		g.saveCanvas();
		g.ClipRec(xInven - 1, yInven + 1, wInven + 2, hInven - 1);
		g.translate(xInven, yInven);
		g.translate(0, -list.cmx);
		for (int i = 0; i < vecSkin.size(); i++)
		{
			MainItem mainItem = (MainItem)vecSkin.elementAt(i);
			if (mainItem.typeObject == 105)
			{
				mainItem.paintColor(g, i % maxNumItemW * wItem + wItem / 2, i / maxNumItemW * wItem + wItem / 2, wItem);
			}
			mainItem.paint(g, i % maxNumItemW * wItem + wItem / 2, i / maxNumItemW * wItem + wItem / 2, wItem);
			paintIconUpgrade(g, i % maxNumItemW * wItem + 3, i / maxNumItemW * wItem + wItem - 2, mainItem);
			paintlockTrade(g, mainItem, i % maxNumItemW * wItem + wItem / 2, i / maxNumItemW * wItem + wItem / 2);
			if (IdSelect == i && indexAreaSellect == 0)
			{
				g.setColor(16777215);
				g.drawRect(i % maxNumItemW * wItem + 1, i / maxNumItemW * wItem + 1, wItem - 2, wItem - 2);
				if (!GameCanvas.isSmallScreen)
				{
					g.drawRect(i % maxNumItemW * wItem + 2, i / maxNumItemW * wItem + 2, wItem - 4, wItem - 4);
				}
			}
		}
		if (Player.maxInventory % maxNumItemW != 0)
		{
			for (int j = Player.maxInventory; j < Player.maxInventory + (maxNumItemW - Player.maxInventory % maxNumItemW); j++)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, wItem - 1, wItem - 1, 0, j % maxNumItemW * wItem + 1, j / maxNumItemW * wItem + 1, 0);
			}
		}
		g.setColor(14075832);
		for (int k = 0; k < maxNumItemW - 1; k++)
		{
			g.fillRect(wItem + k * wItem, 1, 1, wItem * ((Player.maxInventory - 1) / maxNumItemW + 1));
		}
		for (int l = 0; l <= (Player.maxInventory - 1) / maxNumItemW + 1; l++)
		{
			g.fillRect(1, l * wItem, wInven - 1, 1);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xInven, yInven2, wInven, hInven, 0, 3);
		g.setClip(xInven - 1, yInven2 + 1, wInven + 2, hInven - 1);
		g.saveCanvas();
		g.ClipRec(xInven - 1, yInven2 + 1, wInven + 2, hInven - 1);
		g.translate(xInven, yInven2);
		g.translate(0, -listDa.cmx);
		for (int m = 0; m < vecDa.size(); m++)
		{
			MainItem mainItem2 = (MainItem)vecDa.elementAt(m);
			mainItem2.paint(g, m % maxNumItemW * wItem + wItem / 2, m / maxNumItemW * wItem + wItem / 2, wItem);
			paintIconUpgrade(g, m % maxNumItemW * wItem + 3, m / maxNumItemW * wItem + wItem - 2, mainItem2);
			paintlockTrade(g, mainItem2, m % maxNumItemW * wItem + wItem / 2, m / maxNumItemW * wItem + wItem / 2);
			if (idSelect2 == m && indexAreaSellect == 0)
			{
				g.setColor(16777215);
				g.drawRect(m % maxNumItemW * wItem + 1, m / maxNumItemW * wItem + 1, wItem - 2, wItem - 2);
				if (!GameCanvas.isSmallScreen)
				{
					g.drawRect(m % maxNumItemW * wItem + 2, m / maxNumItemW * wItem + 2, wItem - 4, wItem - 4);
				}
			}
		}
		if (Player.maxInventory % maxNumItemW != 0)
		{
			for (int n = Player.maxInventory; n < Player.maxInventory + (maxNumItemW - Player.maxInventory % maxNumItemW); n++)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, wItem - 1, wItem - 1, 0, n % maxNumItemW * wItem + 1, n / maxNumItemW * wItem + 1, 0);
			}
		}
		g.setColor(14075832);
		for (int num = 0; num < maxNumItemW - 1; num++)
		{
			g.fillRect(wItem + num * wItem, 1, 1, wItem * ((Player.maxInventory - 1) / maxNumItemW + 1));
		}
		for (int num2 = 0; num2 <= (Player.maxInventory - 1) / maxNumItemW + 1; num2++)
		{
			g.fillRect(1, num2 * wItem, wInven - 1, 1);
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
	}

	public override void paintTiLe(mGraphics g)
	{
		if (valueMayMan > 0 && ScreenUpgrade.mItemUpgrade[5] != null)
		{
			mFont.tahoma_7b_black.drawString(g, T.tile + ScreenUpgrade.valueTile + "% + " + valueMayMan + "%", xTiLe, yInven, 0);
		}
		else
		{
			mFont.tahoma_7b_black.drawString(g, T.tile + ScreenUpgrade.valueTile + "%", xTiLe, yInven, 0);
		}
		int num = xInven + wInven + 20;
		int num2 = yInven + (hInven + 4) * 2 - 20;
		AvMain.paintRect(g, num, num2, Interface_Game.wMoneyEff - 50, 30, 1, 4);
		num2 += 9;
		AvMain.fraMoney.drawFrame(0, num + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 - 3, num2, 0, 3, g);
		AvMain.fraMoney.drawFrame(1, num + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 + 45, num2, 0, 3, g);
		AvMain.fraMoney.drawFrame(7, num + Interface_Game.mini + AvMain.fraMoney.frameWidth / 2 - 3, num2 + 15, 0, 3, g);
		mFont.tahoma_7_yellow.drawString(g, " " + AvMain.getMoneyShortText(ScreenUpgrade.valueMonney_1), num + Interface_Game.mini + AvMain.fraMoney.frameWidth - 4, num2 - 5, 0);
		mFont.tahoma_7_red.drawString(g, " " + AvMain.getDotNumber(ScreenUpgrade.valueMonney_2), num + Interface_Game.mini + AvMain.fraMoney.frameWidth + 45, num2 - 5, 0);
		mFont.tahoma_7_green.drawString(g, " " + AvMain.getMoneyShortText(ScreenUpgrade.valueMonney_3), num + Interface_Game.mini + AvMain.fraMoney.frameWidth - 4, num2 - 5 + 15, 0);
	}

	public override void paintBackGr(mGraphics g)
	{
		paintPaper(g, x, y - 17, w, h + 17, w, AvMain.PAPER_NORMAL);
		g.setColor(14203529);
		g.fillRoundRect(x + w / 2 - 60, y - 15 + 10, 120, 16, 4, 4);
		AvMain.FontBorderColor(g, nameScreen, x + w / 2, y - 15 + 12, 2, 6, 5);
		if (typeRebuild == 15 || typeRebuild == 18 || typeRebuild == 22)
		{
			if (imghoavan == null)
			{
				loadImg();
			}
			g.drawRegion(imghoavan, 0, 0, 35, 35, 0, x + w - 35 - 10, y + h - 35 - 10, 0);
			g.drawRegion(imghoavan, 0, 0, 35, 35, 1, x + w - 35 - 10, y - 5, 0);
			g.drawRegion(imghoavan, 0, 0, 35, 35, 2, x + 10, y + h - 35 - 10, 0);
			g.drawRegion(imghoavan, 0, 0, 35, 35, 3, x + 10, y - 5, 0);
			g.drawImage(imgtron, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, 3);
		}
		if (Step == 0)
		{
			AvMain.paintRect(g, xInven, yInven, wInven, hInven, 0, 3);
		}
		else if (Step != 0)
		{
			AvMain.paintRect(g, xInven, yInven, wInven, (hInven + 4) * 2, 0, 3);
			if (fraEffUpgrade == null)
			{
				fraEffUpgrade = new FrameImage(mImage.createImage("/interface/effupgrade.png"), 58, 50);
			}
			fraEffUpgrade.drawFrame(frameEff % fraEffUpgrade.nFrame, xInven + wInven / 2 + 10, yInven + hInven, 0, 3, g);
		}
	}

	private void loadImg()
	{
		imghoavan = mImage.createImage("/interface/u_hoavan.png");
		imgtron = mImage.createImage("/interface/u_tron.png");
	}

	public override void setPosInfo()
	{
		if (IdSelect >= 0)
		{
			setPosInfo(itemCur, xInven, yInven + (IdSelect / maxNumItemW + 1) * wItem - list.cmx + 4);
		}
		if (idSelect2 >= 0)
		{
			setPosInfo(itemCur, xInven, yInven2 + (idSelect2 / maxNumItemW + 1) * wItem - listDa.cmx + 4);
		}
	}

	public override void update()
	{
		base.update();
		int cmx = listDa.cmx;
		listDa.moveCamera();
		if (listDa.cmx != cmx || listDa.pointerIsDowning)
		{
			isShowInfo = false;
		}
		else if (itemCur != null)
		{
			isShowInfo = false;
		}
		if (ScreenUpgrade.isNextStepUpgradeSkin)
		{
			continueUpgradeSkin();
		}
	}

	public override void updatePointer()
	{
		if (GameCanvas.isPointerSelect)
		{
			updatePointerCheckBoxUpgradeSkin();
		}
		if ((Step == 0 && (ScreenUpgrade.isContinueUpgradeSkin || ScreenUpgrade.isNextStepUpgradeSkin)) || Step != 0)
		{
			return;
		}
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointSelect(xInven, yInven, wInven, hInven))
		{
			int num = (GameCanvas.px - xInven) / wItem + (GameCanvas.py - yInven + list.cmx) / wItem * maxNumItemW;
			int num2 = vecSkin.size();
			if (num >= 0 && num < num2)
			{
				GameCanvas.isPointerSelect = false;
				if (num == IdSelect)
				{
					if (itemCur != null && cmdBovao != null)
					{
						cmdBovao.perform();
					}
				}
				else
				{
					isShowInfo = false;
					IdSelect = num;
					idSelect2 = -1;
					setPosCmd(getMenuActionItem());
					mSystem.outz("select " + IdSelect + " select2 " + idSelect2);
				}
			}
			else
			{
				isShowInfo = false;
				IdSelect = -1;
				idSelect2 = -1;
				setPosCmd(null);
			}
		}
		listDa.update_Pos_UP_DOWN();
		if (GameCanvas.isPointSelect(xInven, yInven2, wInven, hInven))
		{
			int num3 = (GameCanvas.px - xInven) / wItem + (GameCanvas.py - yInven2 + listDa.cmx) / wItem * maxNumItemW;
			int num4 = vecDa.size();
			if (num3 >= 0 && num3 < num4)
			{
				GameCanvas.isPointerSelect = false;
				if (num3 == idSelect2)
				{
					if (itemCur != null && cmdBovao != null)
					{
						cmdBovao.perform();
					}
				}
				else
				{
					isShowInfo = false;
					idSelect2 = num3;
					IdSelect = -1;
					setPosCmd(getMenuActionItem());
					mSystem.outz("select " + IdSelect + " select2 " + idSelect2);
				}
			}
			else
			{
				isShowInfo = false;
				idSelect2 = -1;
				IdSelect = -1;
				setPosCmd(null);
			}
		}
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				((iCommand)vecCmd.elementAt(i)).updatePointer();
			}
		}
		if (!GameCanvas.isTouch)
		{
			return;
		}
		if (left != null)
		{
			if (left.isPosCmd())
			{
				left.updatePointer();
			}
			else if (GameCanvas.isPointSelect(0, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
			{
				left.perform();
			}
		}
		if (right != null)
		{
			if (right.isPosCmd())
			{
				right.updatePointer();
			}
			else if (GameCanvas.isPointSelect(MotherCanvas.w - GameCanvas.wCommand * 2, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
			{
				right.perform();
			}
		}
		if (center != null)
		{
			if (center.isPosCmd())
			{
				center.updatePointer();
			}
			else if (GameCanvas.isPointSelect(MotherCanvas.hw - GameCanvas.wCommand, MotherCanvas.h - GameCanvas.hCommand - 5, GameCanvas.wCommand * 2, GameCanvas.hCommand + 10))
			{
				center.perform();
			}
		}
	}

	public override mVector getMenuActionItem()
	{
		mVector mVector2 = new mVector();
		MainItem mainItem = null;
		if (IdSelect >= 0)
		{
			mainItem = (MainItem)vecSkin.elementAt(IdSelect);
		}
		else if (idSelect2 >= 0)
		{
			mainItem = (MainItem)vecDa.elementAt(idSelect2);
		}
		if (mainItem != null)
		{
			itemCur = mainItem;
			cmdBovao.caption = T.bovao;
			cmdBovao.subIndex = 1;
			if (isGetItemUpgrade(itemCur.ID, itemCur.typeObject))
			{
				cmdBovao.caption = T.layra;
				cmdBovao.subIndex = 0;
			}
			mVector2.addElement(cmdBovao);
		}
		return mVector2;
	}

	public override void getItemCurNew()
	{
		isShowInfo = false;
		if (IdSelect >= 0)
		{
			IdSelect = AvMain.resetSelect(IdSelect, vecSkin.size() - 1, isreset: false);
			if (GameCanvas.isTouchNoOrPC())
			{
				list.setToX((IdSelect / maxNumItemW + 1) * wItem - hInven);
			}
			setPosCmd(getMenuActionItem());
		}
		if (idSelect2 >= 0)
		{
			idSelect2 = AvMain.resetSelect(idSelect2, vecDa.size() - 1, isreset: false);
			if (GameCanvas.isTouchNoOrPC())
			{
				listDa.setToX((idSelect2 / maxNumItemW + 1) * wItem - hInven);
			}
			setPosCmd(getMenuActionItem());
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		base.commandPointer(index, subIndex);
		switch (index)
		{
		case 13:
		{
			if (itemCur == null)
			{
				break;
			}
			sbyte b2 = 0;
			if (subIndex == 0)
			{
				b2 = GetItemUpgrade(itemCur.ID, itemCur.typeObject);
			}
			else if (itemCur.typeObject == 7)
			{
				b2 = 1;
			}
			else if (itemCur.typeObject == 105)
			{
				b2 = 0;
			}
			else if (itemCur.typeObject == 4)
			{
				b2 = -1;
				for (sbyte b3 = 2; b3 < ScreenUpgrade.mItemUpgrade.Length; b3++)
				{
					if (ScreenUpgrade.mItemUpgrade[b3] == null)
					{
						b2 = b3;
						break;
					}
				}
			}
			if (b2 > 0 && ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItemUpgrade);
			}
			else if (b2 >= 0)
			{
				GlobalService.gI().Upgrade_Skin(1, itemCur.typeObject, itemCur.ID, b2, (sbyte)subIndex);
			}
			break;
		}
		case 14:
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItemUpgrade);
			}
			else
			{
				GlobalService.gI().Upgrade_Skin(4, ScreenUpgrade.mItemUpgrade[0].ID);
			}
			break;
		case 15:
		{
			mVector mVector3 = new mVector();
			for (sbyte b4 = 0; b4 < ScreenUpgrade.mItemUpgrade.Length; b4++)
			{
				if (ScreenUpgrade.mItemUpgrade[b4] != null)
				{
					mVector3.addElement(ScreenUpgrade.mItemUpgrade[b4]);
				}
			}
			GlobalService.gI().Upgrade_Skin(2, mVector3);
			GameCanvas.end_Dialog();
			break;
		}
		case 16:
		{
			if (ScreenUpgrade.mItemUpgrade[0] == null)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.nullItemUpgrade);
				break;
			}
			mVector mVector4 = new mVector();
			iCommand o = new iCommand(T.chapnhan, 17, 0, instance);
			mVector4.addElement(o);
			iCommand o2 = new iCommand(T.close, 18, 0, this);
			mVector4.addElement(o2);
			GameCanvas.Start_Normal_DiaLog("Bạn có muốn bật cường hoá tự động không?", mVector4, isCmdClose: false);
			break;
		}
		case 17:
		{
			mVector mVector2 = new mVector();
			for (sbyte b = 0; b < ScreenUpgrade.mItemUpgrade.Length; b++)
			{
				if (ScreenUpgrade.mItemUpgrade[b] != null)
				{
					mVector2.addElement(ScreenUpgrade.mItemUpgrade[b]);
				}
			}
			GlobalService.gI().Upgrade_Skin(2, mVector2);
			GameCanvas.end_Dialog();
			break;
		}
		case 18:
			GameCanvas.end_Dialog();
			break;
		case 19:
			stopUpgradeSkin();
			break;
		}
	}

	public void setInfo_money(int valueMonney_1, short valueMonney_2, int valueMonney_3)
	{
		int num = 0;
		bool flag = false;
		for (sbyte b = 1; b < ScreenUpgrade.mItemUpgrade.Length; b++)
		{
			if (ScreenUpgrade.mItemUpgrade[b] != null)
			{
				if (ScreenUpgrade.mItemUpgrade[b].typeObject == 7 && ScreenUpgrade.mItemUpgrade[b].ID == 17)
				{
					flag = true;
				}
				else
				{
					num += ScreenUpgrade.mItemUpgrade[b].perSuc;
				}
				mSystem.outz("perSucc " + ScreenUpgrade.mItemUpgrade[b].perSuc + " tile = " + num);
			}
		}
		ScreenUpgrade.valueTile = num - ScreenUpgrade.mItemUpgrade[0].LvUpgrade * 4;
		if (flag)
		{
			ScreenUpgrade.valueTile *= 2;
		}
		if (ScreenUpgrade.valueTile <= 0)
		{
			ScreenUpgrade.valueTile = 0;
		}
		mSystem.outz("valueTile = " + ScreenUpgrade.valueTile);
		if (valueMonney_1 != 0 || valueMonney_2 != 0 || valueMonney_3 != 0)
		{
			ScreenUpgrade.valueMonney_1 = valueMonney_1;
			ScreenUpgrade.valueMonney_2 = valueMonney_2;
			ScreenUpgrade.valueMonney_3 = valueMonney_3;
		}
	}

	public sbyte GetItemUpgrade(short Id, sbyte cat)
	{
		for (sbyte b = 1; b < ScreenUpgrade.mItemUpgrade.Length; b++)
		{
			if (ScreenUpgrade.mItemUpgrade[b] != null && ScreenUpgrade.mItemUpgrade[b].ID == Id && ScreenUpgrade.mItemUpgrade[b].typeObject == cat)
			{
				return b;
			}
		}
		return -1;
	}

	public override void paintIconUpgrade(mGraphics g, int x, int y, MainItem item)
	{
		for (int i = 0; i < ScreenUpgrade.mItemUpgrade.Length; i++)
		{
			if (ScreenUpgrade.mItemUpgrade[i] != null && ScreenUpgrade.mItemUpgrade[i].ID == item.ID && ScreenUpgrade.mItemUpgrade[i].typeObject == item.typeObject)
			{
				g.drawImage(AvMain.imgcheck, x, y, mGraphics.BOTTOM | mGraphics.LEFT);
			}
		}
	}

	public void setLvUpgradeItem(short idItem, sbyte level)
	{
		ScreenUpgrade.mItemUpgrade[0].LvUpgrade = level;
		for (sbyte b = 0; b < vecSkin.size(); b++)
		{
			MainItem mainItem = (MainItem)vecSkin.elementAt(b);
			if (mainItem.ID == idItem)
			{
				mainItem.LvUpgrade = level;
				break;
			}
		}
	}

	public override void setStep()
	{
		if (Step == 1 || Step == 2)
		{
			if (tickStep % 15 == 0)
			{
				mSound.playSound(27, mSound.volumeSound);
			}
			tickStep++;
			updateEff();
			if (tickStep % 5 != 0)
			{
				return;
			}
			if (indexStep < ScreenUpgrade.mItemUpgrade.Length)
			{
				if (tickStep > 50)
				{
					indexStep = ScreenUpgrade.mItemUpgrade.Length;
				}
				for (int i = indexStep; i < ScreenUpgrade.mItemUpgrade.Length; i++)
				{
					if (ScreenUpgrade.mItemUpgrade[i] != null)
					{
						createEff(75, 0, posUp[i][0] + wItem / 2, posUp[i][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
						ScreenUpgrade.mItemUpgrade[i].isRemove = true;
						indexStep = i + 1;
						tickStop = tickStep + 11 + ((w - lech * 3 - wInven) / 2 - 15) / 5;
						break;
					}
				}
			}
			else
			{
				if (tickStep < tickStop)
				{
					return;
				}
				if (Step == 1)
				{
					mSound.playSound(28, mSound.volumeSound);
					int subtype = 0;
					if (GameCanvas.language == 1)
					{
						subtype = 2;
					}
					createEff(79, subtype, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
					createEff(76, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
					createEff(53, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
				}
				else if (Step == 2)
				{
					mSound.playSound(29, mSound.volumeSound);
					int subtype2 = 1;
					if (GameCanvas.language == 1)
					{
						subtype2 = 3;
					}
					createEff(79, subtype2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
					createEff(77, 0, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2, posUp[0][0] + wItem / 2, posUp[0][1] + wItem / 2);
				}
				Step = 3;
				tickStep = 0;
				tickStop = 20;
			}
		}
		else
		{
			if (Step != 3)
			{
				return;
			}
			tickStep++;
			if (tickStep < tickStop)
			{
				return;
			}
			Step = 0;
			tickStop = 0;
			tickStep = 0;
			indexStep = 1;
			if (!ScreenUpgrade.isContinueUpgradeSkin)
			{
				GameCanvas.Start_Normal_Only_CmdClose_DiaLog(showServer);
			}
			GlobalService.gI().Upgrade_Skin(1, ScreenUpgrade.mItemUpgrade[0].typeObject, ScreenUpgrade.mItemUpgrade[0].ID, 0, 1);
			if (ScreenUpgrade.isContinueUpgradeSkin)
			{
				ScreenUpgrade.mItemUpgradeSaveTemp = new MainItem[ScreenUpgrade.mItemUpgrade.Length];
				for (int j = 0; j < ScreenUpgrade.mItemUpgrade.Length; j++)
				{
					ScreenUpgrade.mItemUpgradeSaveTemp[j] = ScreenUpgrade.mItemUpgrade[j];
					if (ScreenUpgrade.mItemUpgradeSaveTemp[j] != null)
					{
						getNameByIdItem(ScreenUpgrade.mItemUpgradeSaveTemp[j].ID, ScreenUpgrade.mItemUpgradeSaveTemp[j].typeObject);
					}
				}
				if (checkNextStepFunc())
				{
					for (int k = 1; k < ScreenUpgrade.mItemUpgradeSaveTemp.Length; k++)
					{
						if (ScreenUpgrade.mItemUpgradeSaveTemp[k] != null)
						{
							GlobalService.gI().Upgrade_Skin(1, ScreenUpgrade.mItemUpgradeSaveTemp[k].typeObject, ScreenUpgrade.mItemUpgradeSaveTemp[k].ID, (sbyte)k, 1);
							ScreenUpgrade.isNextStepUpgradeSkin = true;
							timeStartAddItemUpgradeSkin = mSystem.currentTimeMillis();
						}
					}
					return;
				}
				stopUpgradeSkin();
				for (int l = 0; l < ScreenUpgrade.mItemUpgrade.Length; l++)
				{
					ScreenUpgrade.mItemUpgrade[l] = null;
				}
				if (nameItemUpgradeSkinNotEnough.Equals(string.Empty))
				{
					stopUpgradeSkin();
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(T.materialSkin + T.notEnoughItemUpgradeSkin);
				}
				else
				{
					stopUpgradeSkin();
					GameCanvas.Start_Normal_Only_CmdClose_DiaLog(nameItemUpgradeSkinNotEnough + T.notEnoughItemUpgradeSkin);
				}
			}
			else
			{
				for (int m = 0; m < ScreenUpgrade.mItemUpgrade.Length; m++)
				{
					ScreenUpgrade.mItemUpgrade[m] = null;
				}
			}
		}
	}

	public override void updatekey()
	{
		if (Step != 0)
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyMove(0))
		{
			if (IdSelect > 0)
			{
				IdSelect--;
			}
			if (idSelect2 > 0)
			{
				idSelect2--;
			}
			GameCanvas.ClearkeyMove(0);
			flag = true;
		}
		else if (GameCanvas.keyMove(2))
		{
			if (IdSelect != -1 && IdSelect < vecSkin.size() - 1)
			{
				IdSelect++;
			}
			if (idSelect2 != -1 && idSelect2 < vecDa.size() - 1)
			{
				idSelect2++;
			}
			GameCanvas.ClearkeyMove(2);
			flag = true;
		}
		else if (GameCanvas.keyMove(1))
		{
			if (IdSelect >= maxNumItemW)
			{
				IdSelect -= maxNumItemW;
			}
			else if (idSelect2 >= maxNumItemW)
			{
				idSelect2 -= maxNumItemW;
			}
			else if (idSelect2 != -1 && idSelect2 < maxNumItemW)
			{
				idSelect2 = -1;
				IdSelect = vecSkin.size() - 1;
			}
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			if (IdSelect != -1 && IdSelect < vecSkin.size() - maxNumItemW)
			{
				IdSelect += maxNumItemW;
			}
			else if (idSelect2 != -1 && idSelect2 < vecDa.size() - maxNumItemW)
			{
				idSelect2 += maxNumItemW;
			}
			else if (IdSelect >= vecSkin.size() - maxNumItemW)
			{
				idSelect2 = 0;
				IdSelect = -1;
			}
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (IdSelect >= 0 && IdSelect < vecSkin.size())
		{
			idSelect2 = -1;
		}
		if (idSelect2 >= 0 && idSelect2 < vecDa.size())
		{
			IdSelect = -1;
		}
		if (flag)
		{
			getItemCurNew();
		}
		updatekeyPC();
	}

	public void SetValueMayMan()
	{
		mVector mVector2 = new mVector();
		for (sbyte b = 2; b < ScreenUpgrade.mItemUpgrade.Length; b++)
		{
			if (ScreenUpgrade.mItemUpgrade[b] != null)
			{
				mVector2.addElement(ScreenUpgrade.mItemUpgrade[b]);
				mSystem.outz("add item " + b);
			}
			else
			{
				mSystem.outz("add item " + b + " null");
			}
		}
		if (mVector2.size() == 4)
		{
			GlobalService.gI().Upgrade_Skin(6, ScreenUpgrade.mItemUpgrade[0].ID, mVector2);
		}
		else
		{
			valueMayMan = 0;
		}
	}

	private void continueUpgradeSkin()
	{
		long num = mSystem.currentTimeMillis();
		int num2 = (int)(800.0 / (1.0 + (double)GameCanvas.percent / 100.0));
		if (num - timeStartAddItemUpgradeSkin < num2)
		{
			return;
		}
		mVector mVector2 = new mVector();
		for (int i = 0; i < ScreenUpgrade.mItemUpgradeSaveTemp.Length; i++)
		{
			if (ScreenUpgrade.mItemUpgradeSaveTemp[i] != null)
			{
				mVector2.addElement(ScreenUpgrade.mItemUpgradeSaveTemp[i]);
			}
		}
		GlobalService.gI().Upgrade_Skin(2, mVector2);
		ScreenUpgrade.isNextStepUpgradeSkin = false;
	}

	private int getNumByIdItem(int id)
	{
		int result = -1;
		for (int i = 0; i < vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)vecInventory.elementAt(i);
			if (mainItem != null && mainItem.ID == id)
			{
				result = mainItem.numPotion;
				break;
			}
		}
		return result;
	}

	private bool checkNextStepFunc()
	{
		bool result = true;
		for (int i = 1; i < ScreenUpgrade.mItemUpgradeSaveTemp.Length; i++)
		{
			if (ScreenUpgrade.mItemUpgradeSaveTemp[i] != null && getNumByIdItem(ScreenUpgrade.mItemUpgradeSaveTemp[i].ID) <= 0)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private void getNameByIdItem(short id, sbyte type)
	{
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem != null && mainItem.ID == id && mainItem.typeObject == type)
			{
				if (nameItemUpgradeSkinNotEnough.Equals(string.Empty))
				{
					nameItemUpgradeSkinNotEnough = mainItem.name;
				}
				else
				{
					nameItemUpgradeSkinNotEnough = mainItem.name;
				}
			}
		}
	}

	public static void stopUpgradeSkin()
	{
		ScreenUpgrade.mItemUpgradeSaveTemp = new MainItem[6];
		ScreenUpgrade.isClickUpgradeSkinToSucess = false;
		ScreenUpgrade.isContinueUpgradeSkin = false;
		ScreenUpgrade.isNextStepUpgradeSkin = false;
	}

	public void updatePointerCheckBoxUpgradeSkin()
	{
		if (GameCanvas.isPoint(xCheckBoxSkin - 10, yCheckBoxSkin - 15, wCheckBoxSkin, hCheckBoxSkin + 10))
		{
			ScreenUpgrade.isClickUpgradeSkinToSucess = !ScreenUpgrade.isClickUpgradeSkinToSucess;
			if (!ScreenUpgrade.isClickUpgradeSkinToSucess)
			{
				stopUpgradeSkin();
			}
			GameCanvas.isPointerSelect = false;
		}
	}
}
