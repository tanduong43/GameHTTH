public class Other_Player : MainPlayer
{
	private mVector vecEffShip = new mVector("Other_Player.vecEffShip");

	public Other_Player(short ID, sbyte type, string name, int x, int y)
	{
		hOne = 52;
		if (LoadMap.specMap == 4)
		{
			setSpeed(4, 3);
			wOne = 100;
		}
		else
		{
			setSpeed(7, 7);
			wOne = 26;
		}
		typeObject = type;
		base.name = name;
		base.ID = ID;
		base.x = x;
		base.y = y;
		toX = x;
		toY = y;
		toXNew = x;
		toYNew = y;
		hIconFocus = 0;
		f = CRes.random(feStand.Length);
		Action = 0;
		colorName = 5;
		setWName();
	}

	public override void setImgMonSterforOtherPlayer(sbyte typeMove)
	{
		wOne = 0;
		maxEffShip = 4;
		indexShip = 2;
		typeMoveMonster = typeMove;
		switch (typeMove)
		{
		case 0:
			nFrame = 5;
			mActionMonSter = MonsterWalk.mMon12;
			break;
		case 1:
			nFrame = 7;
			mActionMonSter = MonsterWalk.mMon234;
			break;
		case 2:
			nFrame = 8;
			mActionMonSter = MonsterWalk.mMon2345Long;
			break;
		case 4:
			nFrame = 7;
			mActionMonSter = MonsterWalk.mMon1234;
			break;
		case 13:
			nFrame = 9;
			mActionMonSter = Pet.mChoper;
			break;
		case 19:
			nFrame = 2;
			mActionMonSter = MonsterWalk.mMonTru;
			typeShadow = 0;
			break;
		case 17:
			nFrame = 4;
			mActionMonSter = MonsterWalk.mMonLoSuoi;
			typeShadow = 0;
			break;
		default:
			nFrame = 5;
			mActionMonSter = MonsterWalk.mMon012;
			break;
		}
		mSystem.outz("other_player " + name + " nFrame " + nFrame + " type " + typeMoveMonster);
	}

	public override void paint(mGraphics g)
	{
		if (!isInfo)
		{
			_ = timeInviMove;
			_ = 0;
			return;
		}
		sbyte color = colorName;
		if (Player.vecParty.size() > 0)
		{
			for (int i = 0; i < Player.vecParty.size(); i++)
			{
				if (((InfoMemList)Player.vecParty.elementAt(i)).name.CompareTo(name) == 0)
				{
					color = 4;
					break;
				}
			}
		}
		if (Player.mSatnhan.Length != 0)
		{
			for (int j = 0; j < Player.mSatnhan.Length; j++)
			{
				if (ID == Player.mSatnhan[j])
				{
					color = 6;
				}
			}
		}
		if (isHuman())
		{
			if (Action == 4)
			{
				if (!isDie)
				{
					g.drawImage(MainObject.imgShadow, xDie + 1, yDie, 3);
					paintChar(g, xDie, yDie - dyDie);
				}
				else
				{
					if (LoadMap.specMap != 4)
					{
						paintShadow(g, x);
					}
					g.drawRegion(AvMain.fraDiePlayer.imgFrame, 0, f / 5 % AvMain.fraDiePlayer.nFrame * AvMain.fraDiePlayer.frameHeight, AvMain.fraDiePlayer.frameWidth, AvMain.fraDiePlayer.frameHeight - 2 + dySea / 10, 0, x - 4, y - dy, mGraphics.BOTTOM | mGraphics.LEFT);
				}
			}
			else
			{
				paintCharAllPart(g, 0);
			}
		}
		else
		{
			if (typeMoveMonster == 19)
			{
				if (Action == 4)
				{
					if (typeShadow >= 0)
					{
						paintShadowMonster(g, x, 0, typeShadow);
					}
					MainImage imageAll = ObjectData.getImageAll(IdIcon, ObjectData.HashImageMonster, 1000);
					if (imageAll != null && imageAll.img != null)
					{
						g.drawRegion(imageAll.img, 0, mActionMonSter[Action][f] * hOne, wOne, hOne, (Dir == 2) ? 2 : 0, x, y - dy, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
				dy = 0;
			}
			if (Action != 4)
			{
				paintImgMonSter(g);
			}
		}
		paintName(g, color, 0);
	}

	public override void paintHideAvatar(mGraphics g)
	{
		if (!isInfo)
		{
			_ = timeInviMove;
			_ = 0;
			return;
		}
		sbyte color = colorName;
		if (Player.vecParty.size() > 0)
		{
			for (int i = 0; i < Player.vecParty.size(); i++)
			{
				if (((InfoMemList)Player.vecParty.elementAt(i)).name.CompareTo(name) == 0)
				{
					color = 4;
					break;
				}
			}
		}
		if (Player.mSatnhan.Length != 0)
		{
			for (int j = 0; j < Player.mSatnhan.Length; j++)
			{
				if (ID == Player.mSatnhan[j])
				{
					color = 6;
				}
			}
		}
		if (isHuman())
		{
			if (Action == 4)
			{
				if (!isDie)
				{
					g.drawImage(MainObject.imgShadow, xDie + 1, yDie, 3);
				}
				else
				{
					if (LoadMap.specMap != 4)
					{
						paintShadow(g, x);
					}
					g.drawRegion(AvMain.fraDiePlayer.imgFrame, 0, f / 5 % AvMain.fraDiePlayer.nFrame * AvMain.fraDiePlayer.frameHeight, AvMain.fraDiePlayer.frameWidth, AvMain.fraDiePlayer.frameHeight - 2 + dySea / 10, 0, x - 4, y - dy, mGraphics.BOTTOM | mGraphics.LEFT);
				}
			}
			else
			{
				paintCharNoPart(g, 0);
			}
		}
		else
		{
			if (typeMoveMonster == 19)
			{
				if (Action == 4)
				{
					if (typeShadow >= 0)
					{
						paintShadowMonster(g, x, 0, typeShadow);
					}
					MainImage imageAll = ObjectData.getImageAll(IdIcon, ObjectData.HashImageMonster, 1000);
					if (imageAll != null && imageAll.img != null)
					{
						g.drawRegion(imageAll.img, 0, mActionMonSter[Action][f] * hOne, wOne, hOne, (Dir == 2) ? 2 : 0, x, y - dy, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
				dy = 0;
			}
			if (Action != 4)
			{
				paintImgMonSter(g);
			}
		}
		paintName(g, color, 0);
	}

	public override void paintOnlyShadown(mGraphics g)
	{
		g.drawImage(MainObject.imgShadow, x, y, 3);
		g.drawImage(AvMain.imgHinhnhan, x, y, 33);
	}

	public void paintImgMonSter(mGraphics g)
	{
		if (mActionMonSter == null)
		{
			return;
		}
		MainImage imageAll = ObjectData.getImageAll(IdIcon, ObjectData.HashImageMonster, 1000);
		int num = Action;
		if ((LoadMap.specMap == 4 || typeActionBoat != 0) && boatSea != null && Action == 1)
		{
			num = 0;
		}
		if (num > mActionMonSter.Length - 1)
		{
			num = 0;
		}
		if (f > mActionMonSter[num].Length - 1)
		{
			f = 0;
		}
		paintShadow(g);
		if (imageAll.img == null)
		{
			return;
		}
		if (wOne <= 0 || hOne <= 0)
		{
			hOne = mImage.getImageHeight(imageAll.img.image) / nFrame;
			wOne = mImage.getImageWidth(imageAll.img.image);
		}
		int num2 = y - dy;
		if ((LoadMap.specMap == 4 || typeActionBoat != 0) && boatSea != null && boatSea.ID == ID)
		{
			boatSea.paintFrist(g);
			if (dy == 0)
			{
				num2 = y - dySea / 10 + 5;
			}
			boatSea.paintHang(g);
			boatSea.paintBuom(g);
			boatSea.paintLastInMap(g);
			return;
		}
		g.drawRegion(imageAll.img, 0, mActionMonSter[num][f] * hOne, wOne, hOne, type_left_right, x, num2, mGraphics.BOTTOM | mGraphics.HCENTER);
		if (state > 0)
		{
			if (state == 1 || state == 3)
			{
				AvMain.fraTrongCay.drawFrame(0, x - 20, num2 - hOne / 2 - 5, 0, 3, g);
			}
			if (state == 2 || state == 3)
			{
				AvMain.fraTrongCay.drawFrame(1, x + 20, num2 - hOne / 2 - 5, 0, 3, g);
			}
			if (state == 4)
			{
				AvMain.fraTrongCay.drawFrame(2, x - 20, num2 - hOne / 2 - 5, 0, 3, g);
				AvMain.fraTrongCay.drawFrame(2, x + 20, num2 - hOne / 2 - 5, 0, 3, g);
				AvMain.fraTrongCay.drawFrame(2, x, num2 - hOne / 2 - 20, 0, 3, g);
			}
		}
	}

	private void paintShadow(mGraphics g)
	{
		if (IdIcon != 999)
		{
			g.drawImage(AvMain.imgShadowSmall, x, y - 3, 3);
		}
	}

	public override void update()
	{
		if (Action == 4)
		{
			if (isHuman())
			{
				updateActionPerson();
			}
		}
		else
		{
			if (typeActionBoat != 0)
			{
				updateActionUpBoat();
				if (Action != 5)
				{
					move_to_XY_Normal();
				}
			}
			checkRemoveBoat();
			if (LoadMap.specMap == 3)
			{
				updateMapTrain();
			}
			getInfo();
			if (isHuman())
			{
				updateActionPerson();
			}
			else
			{
				updateActionMonSter(isPet: false);
				updateDirPaint();
				if (Action != 2)
				{
					updateDy();
				}
			}
			if (skillCurrent == null && vecSkillFires.size() > 0)
			{
				skillCurrent = (Start_Skill)vecSkillFires.elementAt(0);
				resetBeginFire();
				getPosLast();
				vecSkillFires.removeElementAt(0);
			}
			if (skillCurrent != null)
			{
				if (skillCurrent.isRemove)
				{
					skillCurrent = null;
				}
				else if (CRes.abs(x - toX) < vMax && CRes.abs(y - toY) < vMax)
				{
					moveToLastPos();
					skillCurrent.setMonsterMoveFire();
				}
			}
			if (!MainObject.isInScreen(this) && !setIsInScreen(toX, toY, wOne, hOne) && !setIsInScreen(toXNew, toYNew, wOne, hOne))
			{
				toX = toXNew;
				toY = toYNew;
				x = toX;
				y = toY;
				vx = 0;
				vy = 0;
				if (Action != 4)
				{
					Action = 0;
				}
				return;
			}
			if (typeActionBoat == 0 && Action != 2)
			{
				Move_to_Focus_Person();
				int tile = GameCanvas.loadmap.getTile(x + vx, y + vy);
				setMove(1, tile);
			}
		}
		base.update();
	}

	public override void Giaotiep()
	{
		if (typeObject == 2)
		{
			GiaotiepNPC();
		}
		else if (typePlayer == 0)
		{
			GiaotiepOtherPlayer();
		}
		else if (typePlayer == 1)
		{
			addChat(T.playerSupport, isStop: true);
		}
		else if (typePlayer == 2)
		{
			addChat(T.playerShiper, isStop: true);
		}
	}

	public override void setTouchPoint()
	{
		if (GameScreen.player.setFightPk(this))
		{
			GameCanvas.isPointerSelect = false;
			GameScreen.player.beginPlayerFirePoint();
		}
		else
		{
			GameCanvas.isPointerSelect = false;
		}
	}

	public override void setFireObject(int value)
	{
		if (GameScreen.player.setFightPk(this) && !GameCanvas.loadmap.mapLang())
		{
			GameScreen.player.beginPlayerFire(value);
		}
		else if (value == 2)
		{
			Giaotiep();
		}
	}

	public void getInfo()
	{
		if (!isInfo && GameCanvas.gameTick % 20 == 0 && (GameCanvas.timeNow - timeLoadInfo) / 1000 > 10)
		{
			timeLoadInfo = GameCanvas.timeNow;
			GlobalService.gI().char_info(ID);
		}
	}
}
