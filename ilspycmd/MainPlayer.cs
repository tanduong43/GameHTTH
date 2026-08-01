public class MainPlayer : MainObject
{
	public static short[][][] mPosTrainOther = new short[6][][]
	{
		new short[3][]
		{
			new short[2] { 4, 11 },
			new short[2] { 16, 10 },
			new short[2] { 24, 10 }
		},
		new short[3][]
		{
			new short[2] { 9, 9 },
			new short[2] { 16, 7 },
			new short[2] { 23, 8 }
		},
		new short[3][]
		{
			new short[2] { 11, 9 },
			new short[2] { 20, 8 },
			new short[2] { 16, 7 }
		},
		new short[3][]
		{
			new short[2] { 7, 9 },
			new short[2] { 15, 13 },
			new short[2] { 24, 10 }
		},
		new short[3][]
		{
			new short[2] { 11, 11 },
			new short[2] { 16, 9 },
			new short[2] { 21, 12 }
		},
		new short[3][]
		{
			new short[2] { 10, 11 },
			new short[2] { 17, 10 },
			new short[2] { 23, 12 }
		}
	};

	public int timeStand;

	public int timeFireMapTrain;

	public int indexSkillFireMapTrain;

	public int countAutoMove;

	public int xStopMove;

	public int yStopMove;

	public mVector vecAllInfo = new mVector("MainPlayer.vecAllInfo");

	public mVector vecAllInfoParty = new mVector("MainPlayer.vecAllInfoParty");

	public int maxTimeUpsea;

	public void setMove(bool isAutomove)
	{
		if (typeActionBoat != 0)
		{
			return;
		}
		int tile = GameCanvas.loadmap.getTile(x + vx, y + vy);
		if (tile == 1)
		{
			if (isAutomove)
			{
				setAutoMoveNear();
			}
			else
			{
				if (Action != 4)
				{
					Action = 0;
				}
				vx = 0;
				vy = 0;
			}
		}
		else if (LoadMap.specMap == 4)
		{
			setMoveSea(this);
		}
		if (vx != 0 || vy != 0)
		{
			return;
		}
		int tile2 = GameCanvas.loadmap.getTile(x, y);
		if (!LoadMap.Tile_Stand(tile2))
		{
			return;
		}
		int num = 24;
		int num2 = x * 1000;
		int num3 = y * 1000;
		int num4 = 0;
		bool flag = false;
		do
		{
			flag = false;
			int num5 = num2 + CRes.getcos(num4) * num;
			int num6 = num3 + CRes.getsin(num4) * num;
			if (num5 >= 0 && num6 >= 0)
			{
				int tile3 = GameCanvas.loadmap.getTile(num5 / 1000, num6 / 1000);
				if (tile3 == 0 || tile3 == 2)
				{
					x = num5 / 1000;
					y = num6 / 1000;
					resetAction();
					flag = true;
				}
			}
			num4 += 44;
			if (num4 >= 360)
			{
				num4 = 0;
				num += 24;
			}
		}
		while (!flag);
	}

	private void setMoveSea(MainObject objMain)
	{
		if (objMain.boatSea == null || objMain.ID != objMain.boatSea.ID)
		{
			return;
		}
		objMain.boatSea.boatSetVaCham(objMain.vx, objMain.vy);
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject == objMain || mainObject.boatSea == null || mainObject.ID != mainObject.boatSea.ID || !setIsInScreen(mainObject.x, mainObject.y, 50, 50))
			{
				continue;
			}
			mainObject.boatSea.boatSetVaCham(0, 0);
			if (CRes.setVaCham(objMain.boatSea, mainObject.boatSea) && ((objMain.vx > 0 && objMain.x < mainObject.x) || (objMain.vx < 0 && objMain.x > mainObject.x) || (objMain.vy > 0 && objMain.y <= mainObject.y) || (objMain.vy < 0 && objMain.y > mainObject.y)))
			{
				if (Action != 4)
				{
					Action = 0;
				}
				vx = 0;
				vy = 0;
				break;
			}
		}
	}

	public void setAutoMoveNear()
	{
		int index = GameCanvas.loadmap.getIndex(x + vx, y + vy);
		if (vy != 0)
		{
			if (index % GameCanvas.loadmap.mapW > 0 && (GameCanvas.loadmap.getTile(x + vx - LoadMap.wTile, y + vy) == 0 || GameCanvas.loadmap.getTile(x + vx - LoadMap.wTile, y + vy) == 2) && (GameCanvas.loadmap.getTile(x + vx - LoadMap.wTile, y) == 0 || GameCanvas.loadmap.getTile(x + vx - LoadMap.wTile, y) == 2))
			{
				vx = -vMax;
				Dir = 0;
				vy = 0;
			}
			else if (index % GameCanvas.loadmap.mapW < GameCanvas.loadmap.mapW - 1 && (GameCanvas.loadmap.getTile(x + vx + LoadMap.wTile, y + vy) == 0 || GameCanvas.loadmap.getTile(x + vx + LoadMap.wTile, y + vy) == 2) && (GameCanvas.loadmap.getTile(x + vx + LoadMap.wTile, y) == 0 || GameCanvas.loadmap.getTile(x + vx + LoadMap.wTile, y) == 2))
			{
				vx = vMax;
				Dir = 2;
				vy = 0;
			}
			else
			{
				vy = 0;
			}
		}
		else if (vx != 0)
		{
			if ((GameCanvas.loadmap.getTile(x + vx, y + vy - LoadMap.wTile) == 0 || GameCanvas.loadmap.getTile(x + vx, y + vy - LoadMap.wTile) == 2) && (GameCanvas.loadmap.getTile(x, y + vy - LoadMap.wTile) == 0 || GameCanvas.loadmap.getTile(x, y + vy - LoadMap.wTile) == 2))
			{
				vy = -vMax;
				Dir = 1;
				vx = 0;
			}
			else if ((GameCanvas.loadmap.getTile(x + vx, y + vy + LoadMap.wTile) == 0 || GameCanvas.loadmap.getTile(x + vx, y + vy + LoadMap.wTile) == 2) && (GameCanvas.loadmap.getTile(x, y + vy + LoadMap.wTile) == 0 || GameCanvas.loadmap.getTile(x, y + vy + LoadMap.wTile) == 2))
			{
				vy = vMax;
				Dir = 3;
				vx = 0;
			}
			else
			{
				vx = 0;
			}
		}
		if (vx == 0 && vy == 0 && Action != 4)
		{
			Action = 0;
		}
	}

	public override void resetAction()
	{
		vx = (vy = 0);
		toX = x;
		toY = y;
		if (Action != 2 && Action != 4)
		{
			Action = 0;
			weapon_frame = 0;
		}
	}

	public override void setDataBeginSkill(MainSkill skill, mVector vec)
	{
		plashNow = new Plash(skill, this, vec);
		resetBeginFire();
		Action = 2;
	}

	public override void addSkillFire(MainSkill skill, mVector vec)
	{
		if (skillCurrent != null)
		{
			skillCurrent.beginSkill();
		}
		vecSkillFires.addElement(new Start_Skill(this, vec, skill));
	}

	public virtual void updateMapTrain()
	{
		if ((GameCanvas.gameTick + tickMapTrain) % 75 == 0 && CRes.random(3) == 0 && Action != 2 && skillCurrent != null)
		{
			int num = CRes.random(MainObject.mPosMapTrain.Length);
			toX = MainObject.mPosMapTrain[num][0];
			toY = MainObject.mPosMapTrain[num][1];
			return;
		}
		timeFireMapTrain++;
		if (timeFireMapTrain <= 80 || mSkillMapTrain == null)
		{
			return;
		}
		timeFireMapTrain = 0;
		toX = x;
		toY = y;
		mVector mVector2 = new mVector();
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (MainObject.getDistance(x, y, mainObject.x, mainObject.y) < Player.wFocus && mainObject.typeObject == 1)
			{
				Object_Effect_Skill object_Effect_Skill = new Object_Effect_Skill(mainObject.ID, mainObject.typeObject);
				object_Effect_Skill.setHP(mainObject.maxHp / 10, mainObject.Hp - mainObject.maxHp / 10, 0);
				mVector2.addElement(object_Effect_Skill);
				MainSkill skill = new MainSkill(-1, mSkillMapTrain[indexSkillFireMapTrain]);
				addSkillFire(skill, mVector2);
				indexSkillFireMapTrain++;
				if (indexSkillFireMapTrain >= mSkillMapTrain.Length)
				{
					indexSkillFireMapTrain = 0;
				}
				break;
			}
		}
	}

	public override void updateActionUpBoat()
	{
		if (typeActionBoat == 1)
		{
			updateActionUp();
		}
		else if (typeActionBoat == 2)
		{
			updateActionDown();
		}
		else if (typeActionBoat == 3)
		{
			updateActionUp_2();
		}
		else if (typeActionBoat == 4)
		{
			updateActionDown_2();
		}
		maxTimeUpsea++;
	}

	public override void updateActionDown_2()
	{
		if (stepUpboat == 0)
		{
			if (Action == 5)
			{
				if (f < 2)
				{
					dy = (f + 1) * 5;
				}
				else if (f == 2 || f == 3)
				{
					dy = 12;
				}
				else if (dy > 3)
				{
					dy = 12 - (f - 3) * 5;
				}
				if (f == 5)
				{
					dy = 0;
					Action = 0;
					vx = 0;
					vy = 0;
					stepUpboat = 1;
					setSpeed(7, 7);
					setXtoYto(xAnchor, yAnchor);
				}
			}
			else if (posTransRoad == null && MainObject.getDistance(x, y, xUpBoat, yUpBoat) <= 48)
			{
				vx = 0;
				vy = -12;
				f = 0;
				Action = 5;
				type_left_right = 2;
				Dir = 2;
				if (boatSea != null)
				{
					GameScreen.addBoatVec_And_mItem(boatSea, isSort: true);
					boatSea = null;
				}
			}
		}
		else if (stepUpboat == 1 && posTransRoad == null && MainObject.getDistance(x, y, xAnchor, yAnchor) <= 48)
		{
			typeActionBoat = 0;
		}
	}

	public override void updateActionUp_2()
	{
		if (stepUpboat == 0)
		{
			if (Action == 5)
			{
				if (f < 2)
				{
					dy = (f + 1) * 5;
				}
				else if (f == 2 || f == 3)
				{
					dy = 12;
				}
				else if (dy > 3)
				{
					dy = 12 - (f - 3) * 5;
				}
				if (f == 6)
				{
					x = xUpBoat;
					y = yUpBoat;
					dy = 0;
					Action = 0;
					vx = 0;
					vy = 0;
					stepUpboat = 1;
					setSpeed(3, 3);
					for (int i = 0; i < GameScreen.vecBoat.size(); i++)
					{
						Boat boat = (Boat)GameScreen.vecBoat.elementAt(i);
						if (boat.ID == ID)
						{
							boat.isPaint = false;
							boatSea = boat;
							break;
						}
					}
					if (boatSea == null)
					{
						setNextSea();
					}
				}
				type_left_right = 2;
				Dir = 2;
			}
			else
			{
				tickTypeActionBoat++;
				if (posTransRoad == null && (MainObject.getDistance(x, y, xUpBoat, yUpBoat) <= 72 || tickTypeActionBoat > 40))
				{
					vx = (xUpBoat - x) / 6;
					vy = (yUpBoat - y) / 6;
					f = 0;
					Action = 5;
					type_left_right = 2;
					Dir = 2;
					tickTypeActionBoat = 0;
				}
			}
		}
		else if (stepUpboat == 1)
		{
			tickTypeActionBoat++;
			if (tickTypeActionBoat == 20)
			{
				setXtoYto(x + 120, y);
			}
			if (tickTypeActionBoat == 50)
			{
				setNextSea();
			}
		}
	}

	public override void updateActionDown()
	{
		if (stepUpboat == 0)
		{
			if (Action == 5)
			{
				if (f == 5)
				{
					dy = 0;
					Action = 0;
					vx = 0;
					vy = 0;
					stepUpboat = 1;
					setSpeed(7, 7);
					setXtoYto(xAnchor, yAnchor);
				}
				else if (f < 2)
				{
					dy = (f + 1) * 5;
				}
				else if (f == 2 || f == 3)
				{
					dy = 12;
				}
				else if (f > 3)
				{
					dy = 12 - (f - 3) * 5;
				}
			}
			else
			{
				if (posTransRoad != null)
				{
					return;
				}
				tickTypeActionBoat++;
				if (MainObject.getDistance(x, y, xUpBoat, yUpBoat) <= 72 || tickTypeActionBoat >= 10)
				{
					vx = 0;
					vy = 6;
					f = 0;
					Action = 5;
					type_left_right = 2;
					Dir = 2;
					if (boatSea != null)
					{
						GameScreen.addBoatVec_And_mItem(boatSea, isSort: true);
						boatSea = null;
					}
				}
			}
		}
		else if (stepUpboat == 1 && posTransRoad == null && MainObject.getDistance(x, y, xAnchor, yAnchor) <= 48)
		{
			typeActionBoat = 0;
		}
	}

	public override void updateActionUp()
	{
		if (stepUpboat == 0)
		{
			if (Action == 5)
			{
				if (f < 2)
				{
					dy = (f + 1) * 5;
				}
				else if (f == 2 || f == 3)
				{
					dy = 12;
				}
				else if (f > 3)
				{
					dy = 12 - (f - 3) * 5;
				}
				if (f == 6)
				{
					x = xUpBoat;
					y = yUpBoat;
					toX = x;
					toY = y;
					dy = 0;
					Action = 0;
					vx = 0;
					vy = 0;
					stepUpboat = 1;
					setSpeed(3, 3);
					for (int i = 0; i < GameScreen.vecBoat.size(); i++)
					{
						Boat boat = (Boat)GameScreen.vecBoat.elementAt(i);
						if (boat.ID == ID)
						{
							boat.isPaint = false;
							boatSea = boat;
							break;
						}
					}
					if (boatSea == null)
					{
						setNextSea();
					}
				}
				type_left_right = 2;
				Dir = 2;
			}
			else
			{
				tickTypeActionBoat++;
				if (posTransRoad == null && (MainObject.getDistance(x, y, xUpBoat, yUpBoat) <= 48 || tickTypeActionBoat > 40))
				{
					vx = (xUpBoat - x) / 6;
					vy = (yUpBoat - y) / 6;
					f = 0;
					Action = 5;
					type_left_right = 2;
					Dir = 2;
					tickTypeActionBoat = 0;
				}
			}
		}
		else if (stepUpboat == 1)
		{
			tickTypeActionBoat++;
			if (tickTypeActionBoat == 20)
			{
				setXtoYto(x + 120, y);
			}
			if (tickTypeActionBoat == 50)
			{
				setNextSea();
			}
		}
	}

	public override void setActionSea(sbyte type, int xSea, int ySea)
	{
		typeActionBoat = type;
		tickTypeActionBoat = 0;
		posTransRoad = null;
		stepUpboat = 0;
		xUpBoat = 0;
		yUpBoat = 0;
		maxTimeUpsea = 0;
		if (typeActionBoat == 1)
		{
			for (int i = 0; i < GameScreen.vecBoat.size(); i++)
			{
				Boat boat = (Boat)GameScreen.vecBoat.elementAt(i);
				if (boat.ID == ID)
				{
					xUpBoat = boat.x - ((boat.Dir != 2) ? (-boat.wlech) : boat.wlech);
					yUpBoat = boat.y;
					break;
				}
			}
			if (xUpBoat == 0 || yUpBoat == 0)
			{
				setNextSea();
				typeActionBoat = 0;
			}
			else
			{
				setPosUpBoat();
			}
		}
		else if (typeActionBoat == 2)
		{
			xUpBoat = xSea;
			yUpBoat = ySea;
			xAnchor = x;
			yAnchor = y;
			y = yUpBoat;
			x = xUpBoat - 80;
			boatSea = new Boat(ID, x, y, 0, (sbyte)type_left_right);
			boatSea.setPartImage(myBoat, typePirate);
			setPosDownBoat();
			for (int j = 0; j < GameScreen.vecBoat.size(); j++)
			{
				Boat boat2 = (Boat)GameScreen.vecBoat.elementAt(j);
				if (boat2.ID == ID)
				{
					GameScreen.vecBoat.removeElement(boat2);
					break;
				}
			}
			setSpeed(3, 3);
			vySea = 4;
		}
		else if (typeActionBoat == 3)
		{
			for (int k = 0; k < GameScreen.vecBoat.size(); k++)
			{
				Boat boat3 = (Boat)GameScreen.vecBoat.elementAt(k);
				if (boat3.ID == ID)
				{
					xUpBoat = boat3.x - ((boat3.Dir != 2) ? (-boat3.wlech) : boat3.wlech);
					yUpBoat = boat3.y;
					break;
				}
			}
			if (xUpBoat == 0 || yUpBoat == 0)
			{
				setNextSea();
				typeActionBoat = 0;
			}
			else
			{
				setPosUpBoat();
			}
		}
		else
		{
			if (typeActionBoat != 4)
			{
				return;
			}
			xUpBoat = xSea;
			yUpBoat = ySea;
			xAnchor = x;
			yAnchor = y;
			y = yUpBoat;
			x = xUpBoat - 80;
			setPosDownBoat();
			boatSea = new Boat(ID, x, y, 0, (sbyte)type_left_right);
			boatSea.setPartImage(myBoat, typePirate);
			for (int l = 0; l < GameScreen.vecBoat.size(); l++)
			{
				Boat boat4 = (Boat)GameScreen.vecBoat.elementAt(l);
				if (boat4.ID == ID)
				{
					GameScreen.vecBoat.removeElement(boat4);
					break;
				}
			}
			setSpeed(3, 3);
			vySea = 4;
		}
	}

	public void checkRemoveBoat()
	{
		if (boatSea != null && LoadMap.specMap != 4 && maxTimeUpsea > 120)
		{
			if (typeActionBoat == 1 || typeActionBoat == 3)
			{
				setNextSea();
			}
			else if (typeActionBoat == 2 || typeActionBoat == 4)
			{
				setSpeed(7, 7);
				boatSea = null;
				maxTimeUpsea = 0;
			}
			typeActionBoat = 0;
		}
	}

	public virtual void setPosDownBoat()
	{
		toX = xUpBoat;
		toY = yUpBoat;
	}

	public override void setNextSea()
	{
		isRemove = true;
	}

	public virtual void setPosUpBoat()
	{
		if (typeActionBoat == 1)
		{
			toX = xUpBoat - 24;
			toY = yUpBoat - 12;
		}
		else if (typeActionBoat == 3)
		{
			toX = xUpBoat - 24;
			toY = yUpBoat + 12;
		}
	}

	public virtual void setXtoYto(int xto, int yto)
	{
		toX = xto;
		toY = yto;
	}

	public override iCommand getCenterCmd()
	{
		if (GameCanvas.loadmap.mapLang())
		{
			return GameScreen.cmdGiaoTiep;
		}
		return null;
	}
}
