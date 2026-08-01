public class NPC : MainObject
{
	private sbyte typeNPC;

	private sbyte fMyRandom;

	private int numnextframe = 7;

	public const sbyte NPC_THING = 0;

	public const sbyte NPC_PERSON = 1;

	public const sbyte NPC_PERSON_THING = 2;

	public const sbyte NPC_CLAN_DAO = 98;

	public const sbyte NPC_AREA = 99;

	private int hardcodehead;

	private int hardcodexlechhead;

	private int indexEventMoon;

	public sbyte isPerson;

	public static int[][] hardCodeEventMoon = new int[31][]
	{
		new int[5] { 1, 8, -19, 0, 1 },
		new int[5] { 2, 2, -24, 1, 1 },
		new int[5] { 3, 8, -21, 1, 1 },
		new int[5] { 36, 10, -22, 3, 1 },
		new int[5] { 35, -10, -23, 4, 1 },
		new int[5] { 34, 5, -20, 0, 1 },
		new int[5] { 33, -1, -22, 1, 1 },
		new int[5] { 32, -7, -25, 1, 1 },
		new int[5] { 29, 6, -19, 3, 1 },
		new int[5] { 28, 8, -24, 4, 1 },
		new int[5] { 27, -8, -17, 2, 0 },
		new int[5] { 22, 4, -25, 0, 1 },
		new int[5] { 21, 10, -20, 1, 1 },
		new int[5] { 20, 8, -22, 1, 1 },
		new int[5] { 19, -8, -12, 2, 0 },
		new int[5] { 18, 8, -20, 0, 1 },
		new int[5] { 17, 0, -10, 2, 0 },
		new int[5] { 16, 8, -24, 3, 1 },
		new int[5] { 15, -8, -17, 2, 0 },
		new int[5] { 14, 10, -21, 4, 1 },
		new int[5] { 13, -12, -17, 2, 0 },
		new int[5] { 12, 10, -25, 1, 1 },
		new int[5] { 11, -2, -23, 1, 1 },
		new int[5] { 10, -8, -17, 2, 0 },
		new int[5] { 9, -2, -19, 1, 1 },
		new int[5] { 37, 7, -27, 3, 1 },
		new int[5] { 38, 4, -25, 4, 1 },
		new int[5] { 39, 6, -27, 1, 1 },
		new int[5] { 60, 16, -10, 2, 0 },
		new int[5] { 59, 6, -22, 0, 1 },
		new int[5] { 58, 6, -22, 3, 1 }
	};

	private int lastTick;

	private int framepaint;

	public NPC(string name, string namegt, short IDItem, short x, short y, sbyte wBlock, sbyte hBlock, sbyte typeNPC)
	{
		base.name = name;
		nameGiaotiep = namegt;
		ID = IDItem;
		base.x = x;
		base.y = y;
		typeObject = 2;
		this.typeNPC = typeNPC;
		Hp = 100;
		maxHp = 100;
		ysai = 0;
		Action = 0;
		GameCanvas.loadmap.setBlockNPC(x, y, wBlock, hBlock);
		fMyRandom = (sbyte)CRes.random(7);
		colorName = 5;
		vySea = 4;
		setWName();
	}

	public void setDataFrame(sbyte IDImage, sbyte nFrame)
	{
		IdIcon = IDImage;
		base.nFrame = nFrame;
		numnextframe = 7;
		if (IdIcon == 36)
		{
			hardcodehead = 12;
		}
		else if (IdIcon == 18)
		{
			hardcodehead = 18;
		}
		else if (IdIcon == 30)
		{
			hardcodexlechhead = -8;
			hardcodehead = 3;
		}
		else if (IdIcon == 31)
		{
			hardcodexlechhead = -10;
			hardcodehead = 13;
		}
		else if (IdIcon == 66)
		{
			dyShadow = 3;
			numnextframe = 12;
		}
		else if (IdIcon == 68)
		{
			hardcodehead = 6;
		}
	}

	public override void paint(mGraphics g)
	{
		if (isPerson == 1 || isPerson == 98)
		{
			g.drawImage(MainObject.imgShadow, x + 1, y - 3 - dySea / 10 + dyShadow, 3);
		}
		if (typeNPC == 1)
		{
			paintCharAllPart(g, 0);
		}
		else if (typeNPC == 0)
		{
			MainImage imageAll = ObjectData.getImageAll(IdIcon, ObjectData.hashImageNPC, 5000);
			if (imageAll.img != null)
			{
				if (wOne == 0)
				{
					hOne = mImage.getImageHeight(imageAll.img.image) / nFrame;
					wOne = mImage.getImageWidth(imageAll.img.image);
				}
				g.drawRegion(imageAll.img, 0, (fMyRandom + GameCanvas.gameTick / numnextframe) % nFrame * hOne, wOne, hOne + dySea / 10, 0, x, y, 33);
			}
			if (isPerson == 99)
			{
				int num = 0;
				if (LoadMap.specMap == 4)
				{
					num = 3;
				}
				mFont.tahoma_7b_black.drawString(g, string.Empty + LoadMap.getShowArea(LoadMap.Area), x, y - 22 - dySea / 10 - num, 2);
			}
			else if (isPerson == 98 && GameScreen.ClanDao != null)
			{
				MainImage iconClanBig = Potion.getIconClanBig(GameScreen.ClanDao.idIcon);
				if (iconClanBig != null && iconClanBig.img != null)
				{
					int num2 = 0;
					if ((fMyRandom + GameCanvas.gameTick / numnextframe) % 2 == 1)
					{
						num2 = -2;
					}
					if (iconClanBig.frame == -1)
					{
						iconClanBig.set_Frame();
					}
					if (iconClanBig.frame <= 1)
					{
						g.drawImage(iconClanBig.img, x + num2 + 2, y - 52, 3);
					}
					else
					{
						int num3 = ((framepaint < imageAll.frame - 1) ? 3 : 15);
						if (CRes.abs(GameCanvas.gameTick - lastTick) > num3)
						{
							framepaint++;
							if (framepaint >= imageAll.frame)
							{
								framepaint = 0;
							}
							lastTick = GameCanvas.gameTick;
						}
						g.drawRegion(iconClanBig.img, 0, framepaint * iconClanBig.w, iconClanBig.w, iconClanBig.w, 0, x + num2 + 2, y - 52, 3);
					}
				}
			}
			if (typeNpcEvent > 0)
			{
				paintNpcEvent(g);
			}
			if (ID == -993 && GameScreen.player.isNauBanh)
			{
				AvMain.fraNauBanh.drawFrame(2, x, y - hOne - 30, 0, 3, g);
				g.setColor(65394);
				g.fillRect(x + 20, y - hOne - 30 - 3, 3, 7);
				g.setColor(16580352);
				if (GameScreen.player.tickNauBanh < 87)
				{
					g.fillRect(x - 43 + GameScreen.player.tickNauBanh, y - hOne - 30 - 3, 1, 7);
				}
			}
		}
		if (isPerson != 99 && isPerson != 98)
		{
			paintName(g, colorName, 0);
		}
	}

	private void paintNpcEvent(mGraphics g)
	{
		if (typeNpcEvent == 1)
		{
			AvMain.fraEventMoon.drawFrameNew(hardCodeEventMoon[indexEventMoon][3] * 3, x + hardCodeEventMoon[indexEventMoon][1], y + hardCodeEventMoon[indexEventMoon][2] - 3 + (fMyRandom + GameCanvas.gameTick / 7) % 2 * hardCodeEventMoon[indexEventMoon][4], 0, mGraphics.TOP | mGraphics.RIGHT, g);
		}
		if (typeNpcEvent == 2)
		{
			AvMain.fraEventMoon.drawFrameNew(1 + GameCanvas.gameTick / 4 % 2 + hardCodeEventMoon[indexEventMoon][3] * 3, x + hardCodeEventMoon[indexEventMoon][1], y + hardCodeEventMoon[indexEventMoon][2] - 3 + (fMyRandom + GameCanvas.gameTick / 7) % 2 * hardCodeEventMoon[indexEventMoon][4], 0, mGraphics.TOP | mGraphics.RIGHT, g);
		}
	}

	public override void paintName(mGraphics g, sbyte color, int isFocus)
	{
		if (GameScreen.getIsOffAdmin(0) || (GameScreen.objFocus != null && GameScreen.objFocus == this && isFocus == 0) || isPerson == 99 || isPerson == 98)
		{
			return;
		}
		int num = 0;
		if (Action == 4)
		{
			num = 5;
		}
		int num2 = y - dy - hOne - 18 + num;
		if (wNameNPC > 51)
		{
			g.drawRegion(AvMain.imgCombo, 0, 2, 4, 13, 0, x - wNameNPC / 2 + 2, num2 + 6, 3);
			g.drawRegion(AvMain.imgCombo, 47, 2, 4, 13, 0, x + wNameNPC / 2 - 2, num2 + 6, 3);
			int num3 = (wNameNPC - 8) / 40;
			for (int i = 0; i <= (wNameNPC - 8) / 40; i++)
			{
				if (i == num3)
				{
					g.drawRegion(AvMain.imgCombo, 4, 2, (wNameNPC - 8) % 40, 13, 0, x - wNameNPC / 2 + 4 + 20 + i * 40 - 20 + (wNameNPC - 8) % 40 / 2, num2 + 6, 3);
				}
				else
				{
					g.drawRegion(AvMain.imgCombo, 4, 2, 40, 13, 0, x - wNameNPC / 2 + 4 + 20 + i * 40, num2 + 6, 3);
				}
			}
		}
		else
		{
			g.drawRegion(AvMain.imgCombo, 0, 2, 51, 13, 0, x, num2 + 6, 3);
		}
		int num4 = x;
		if (typeQuest > 0)
		{
			num4 -= 10;
		}
		if (typeIconNPC > -1)
		{
			paintIconNPC(g, num4);
			num4 += 20;
		}
		else
		{
			num4 = x;
		}
		if (typeQuest > 0 && GameCanvas.gameTick % 14 < 12)
		{
			paintIconQuest(g, num4);
		}
		if (isFocus == 1)
		{
			AvMain.FontBorderColor(g, name, x, num2, 2, color, 7);
		}
		else
		{
			AvMain.Font3dColor(g, name, x, num2, 2, color);
		}
		paintIconPk_ThanhTich(g);
	}

	public override void paintHead(mGraphics g, int x, int y, int trans)
	{
		if (isPerson != 1)
		{
			return;
		}
		if (typeNPC == 1)
		{
			MainObject.paintHeadEveryWhere(g, head, hair, hat, x, y + 38, trans);
			return;
		}
		MainImage imageAll = ObjectData.getImageAll(IdIcon, ObjectData.hashImageNPC, 5000);
		if (imageAll.img != null)
		{
			if (wOne == 0)
			{
				hOne = mImage.getImageHeight(imageAll.img.image) / nFrame;
				wOne = mImage.getImageWidth(imageAll.img.image);
			}
			int num = 18;
			int num2 = 18;
			if (num > hOne)
			{
				num = hOne;
			}
			if (num2 > wOne)
			{
				num2 = wOne;
			}
			g.drawRegion(imageAll.img, wOne / 2 - num2 / 2 + hardcodexlechhead, hardcodehead, num2, num, 0, x, y, 3);
		}
	}

	public override void updateDySea()
	{
		if (ID == -119)
		{
			return;
		}
		if (CRes.random(40) == 0)
		{
			if (CRes.random(2) == 0)
			{
				vySea = 4;
			}
			else
			{
				vySea = -4;
			}
		}
		if (dySea > 0 && vySea > 0)
		{
			vySea = -4;
		}
		else if (dySea < -50 && vySea < 0)
		{
			vySea = 4;
		}
		dySea += vySea;
	}

	public override void update()
	{
		updateActionPerson();
		updateChatPopup(dhCharPopup);
		if (LoadMap.specMap == 4)
		{
			updateDySea();
		}
		if (GameScreen.objFocus != null && GameScreen.objFocus == this && isNPC_Area() == 99 && GameCanvas.gameTick % 100 == 0)
		{
			GlobalService.gI().Update_Num_Player_Map();
		}
	}

	public override void Giaotiep()
	{
		mSystem.outz("vao day NPC giao tiep");
		if (isPerson == 98)
		{
			mSystem.outz("vao day 1111111");
			if (GameScreen.ClanDao != null)
			{
				mSystem.outz("vao day 222222222 clan=" + GameScreen.player.clan.ID + "  " + GameScreen.ClanDao.ID);
				if (GameScreen.player.clan == null || GameScreen.player.clan.ID != GameScreen.ClanDao.ID)
				{
					mSystem.outz("vao day 333333");
					MsgDialog msgDialog = new MsgDialog();
					msgDialog.setinfoClan(GameScreen.ClanDao);
					GameCanvas.Start_Current_Dialog(msgDialog);
				}
				else
				{
					GlobalService.gI().shop_NPC(ID);
				}
			}
		}
		else if (isPerson != 2 && isPerson != 1)
		{
			GlobalService.gI().shop_NPC(ID);
		}
		else
		{
			GiaotiepNPC();
		}
	}

	public override iCommand getCenterCmd()
	{
		return GameScreen.cmdGiaoTiep;
	}

	public override void setTouchPoint()
	{
		if (Interface_Game.vecfocus.size() <= 1)
		{
			setFireObject(2);
			Interface_Game.timeShowNear = 0;
		}
	}

	public override void setFireObject(int value)
	{
		if (value == 2)
		{
			Giaotiep();
		}
	}

	public override int isNPC_Area()
	{
		return isPerson;
	}

	public override void setPosEvent()
	{
		if (typeNpcEvent != 1 && typeNpcEvent != 2)
		{
			return;
		}
		for (int i = 0; i < hardCodeEventMoon.Length; i++)
		{
			if (IdIcon == hardCodeEventMoon[i][0])
			{
				indexEventMoon = i;
				break;
			}
		}
	}
}
