public class BigBossLittleGraden
{
	public int type;

	public int x;

	public int y;

	public int yEff;

	public int indexAction;

	public int vyEff = 1;

	public int vxFire;

	public int vxAva;

	public int f;

	public int wNameFocus = 54;

	public int yInfo;

	public int vyDie;

	public int yDie;

	public int action;

	public int hp = 10;

	public int MaxHP = 20;

	public int mp = 10;

	public int MaxMP = 10;

	public int dam;

	public string name = string.Empty;

	public const sbyte ACTION_BIG_BOSS_STAND = 0;

	public const sbyte ACTION_BIG_BOSS_FIRE = 1;

	public const sbyte ACTION_BIG_BOSS_AVA = 2;

	public const sbyte ACTION_BIG_BOSS_DIE = 3;

	private mVector vecEff = new mVector("BigBossLittleGraden.vecEff");

	private int[][][] mPlayFrame = new int[8][][]
	{
		new int[4][]
		{
			new int[3] { 0, 0, 62 },
			new int[3] { 1, 130, 58 },
			new int[3] { 2, 17, 74 },
			new int[3] { 3, 149, 92 }
		},
		new int[4][]
		{
			new int[3] { 0, 0, 62 },
			new int[3] { 3, 140, 32 },
			new int[3] { 4, 80, 64 },
			new int[3] { 5, 65, 17 }
		},
		new int[4][]
		{
			new int[3] { 0, 0, 62 },
			new int[3] { 2, 17, 74 },
			new int[3] { 3, 144, 50 },
			new int[3] { 4, 80, 64 }
		},
		new int[4][]
		{
			new int[3] { 0, 0, 62 },
			new int[3] { 1, 130, 58 },
			new int[3] { 2, 17, 74 },
			new int[3] { 3, 149, 92 }
		},
		new int[4][]
		{
			new int[3] { 6, 52, 73 },
			new int[3] { 7, 82, 70 },
			new int[3] { 8, 28, 18 },
			new int[3] { 9, 149, 95 }
		},
		new int[4][]
		{
			new int[3] { 6, 52, 73 },
			new int[3] { 9, 149, 95 },
			new int[3] { 10, -77, 65 },
			new int[3] { 11, 79, 65 }
		},
		new int[4][]
		{
			new int[3] { 6, 52, 73 },
			new int[3] { 9, 149, 95 },
			new int[3] { 12, 47, 42 },
			new int[3] { 13, 83, 73 }
		},
		new int[4][]
		{
			new int[3] { 6, 52, 73 },
			new int[3] { 9, 149, 95 },
			new int[3] { 12, 47, 42 },
			new int[3] { 13, 83, 73 }
		}
	};

	public BigBossLittleGraden(sbyte type)
	{
		this.type = type;
		if (type == 1)
		{
			indexAction = 4;
			x = MotherCanvas.hw;
			y = -40;
			vxFire = -16;
			vxAva = 2;
			yInfo = 70;
			name = "Brogy";
			vyDie = 10;
			yDie = 35;
		}
		else
		{
			x = MotherCanvas.hw - 200;
			y = -40;
			vxFire = 16;
			vxAva = -2;
			yInfo = 42;
			name = "Dorry";
			yDie = 40;
			vyDie = 10;
		}
	}

	public void paint(mGraphics g, int xCam)
	{
		for (int i = 0; i < mPlayFrame[indexAction + action].Length; i++)
		{
			MainImage imageOther = ObjectData.getImageOther((short)mPlayFrame[indexAction + action][i][0], 0);
			if (imageOther != null && imageOther.img != null)
			{
				g.drawImage(imageOther.img, x + xCam + mPlayFrame[indexAction + action][i][1], y + yEff + mPlayFrame[indexAction + action][i][2], 0);
			}
		}
		for (int j = 0; j < vecEff.size(); j++)
		{
			((MainEffect)vecEff.elementAt(j)).paint(g, xCam, 0);
		}
	}

	public void paintINFO(mGraphics g)
	{
		int num = yInfo + GameScreen.h12plus;
		int num2 = MotherCanvas.w - wNameFocus - 2;
		int width = mFont.tahoma_7b_black.getWidth(name);
		AvMain.paintnenFocusSmall(g, num2, num - 1, wNameFocus, 26);
		if (type == 0)
		{
			AvMain.fraPk.drawFrame(12 + GameCanvas.gameTick / 3 % 3, num2 + wNameFocus / 2 - width / 2 - 4, num + 6, 0, 3, g);
		}
		else
		{
			AvMain.fraPk.drawFrame(15 + GameCanvas.gameTick / 3 % 3, num2 + wNameFocus / 2 - width / 2 - 4, num + 6, 0, 3, g);
		}
		AvMain.Font3dWhite(g, name, num2 + wNameFocus / 2 + 6, num, 2);
		num += 13;
		Interface_Game.PaintHPMP(g, 1, hp, MaxHP, num2 + (wNameFocus - 44) / 2, num, 10, 4, 44, -1, isflip: false, 0, isUpdateEff: false, 0);
		num += 5;
		Interface_Game.PaintHPMP(g, 2, mp, MaxMP, num2 + (wNameFocus - 44) / 2, num, 10, 4, 44, -1, isflip: false, 0, isUpdateEff: false, 0);
	}

	public void update()
	{
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
		if ((GameCanvas.gameTick % 5 == 0 && action != 3) || (GameCanvas.gameTick % 10 == 0 && action == 3))
		{
			yEff += vyEff;
			if (vyEff > 0)
			{
				if (yEff >= 4)
				{
					vyEff = 0;
				}
			}
			else if (vyEff < 0)
			{
				if (yEff <= 0)
				{
					vyEff = 0;
				}
			}
			else if (CRes.random(2) == 0)
			{
				if (yEff > 0)
				{
					vyEff = -4;
				}
				else
				{
					vyEff = 4;
				}
			}
		}
		if (action == 1)
		{
			if (f < 5)
			{
				x += vxFire;
				if (f == 2)
				{
					for (int j = 0; j < GameScreen.vecBigBossLittleGraden.size(); j++)
					{
						BigBossLittleGraden bigBossLittleGraden = (BigBossLittleGraden)GameScreen.vecBigBossLittleGraden.elementAt(j);
						if (bigBossLittleGraden.type != type)
						{
							bigBossLittleGraden.setActionBigBoss(2);
							addEffFire(bigBossLittleGraden);
						}
					}
				}
			}
			else if (f >= 8 && f < 12)
			{
				x -= vxFire;
			}
			else if (f >= 12)
			{
				action = 0;
				if (type == 1)
				{
					x = MotherCanvas.hw;
				}
				else
				{
					x = MotherCanvas.hw - 200;
				}
			}
		}
		else if (action == 2)
		{
			if (f < 3)
			{
				x += vxAva;
			}
			else if (f < 6)
			{
				x -= vxAva;
			}
			else if (f >= 6)
			{
				action = 0;
				if (type == 1)
				{
					x = MotherCanvas.hw;
				}
				else
				{
					x = MotherCanvas.hw - 200;
				}
			}
		}
		else if (action == 0)
		{
			if (GameCanvas.gameTick % 100 == 0)
			{
				if (type == 1)
				{
					x = MotherCanvas.hw;
				}
				else
				{
					x = MotherCanvas.hw - 200;
				}
			}
		}
		else if (action == 3 && y < yDie)
		{
			vyDie += 2;
			y += vyDie;
			if (y > yDie)
			{
				y = yDie;
			}
		}
		f++;
	}

	public void setActionBigBoss(sbyte action)
	{
		if (this.action == 3)
		{
			return;
		}
		if (action == 3)
		{
			this.action = action;
			f = 0;
			vyDie = 10;
		}
		else if (this.action == 0)
		{
			this.action = action;
			f = 0;
		}
		else if (this.action == 2)
		{
			if (type == 1)
			{
				x = MotherCanvas.hw;
			}
			else
			{
				x = MotherCanvas.hw - 200;
			}
			this.action = action;
			f = 0;
		}
	}

	public void addEffFire(BigBossLittleGraden obj)
	{
		if (type == 0)
		{
			obj.vecEff.addElement(GameScreen.CreateEffectEnd(100, 0, MotherCanvas.hw + 100, 60, 2, null));
			obj.vecEff.addElement(GameScreen.CreateEffectEnd(100, 0, MotherCanvas.hw + 115, 66, 2, null));
			EffectNum o = new EffectNum("-" + dam, MotherCanvas.hw + 100, 60, 2);
			obj.vecEff.addElement(o);
		}
		else
		{
			obj.vecEff.addElement(GameScreen.CreateEffectEnd(100, 0, MotherCanvas.hw - 50, 40, 0, null));
			obj.vecEff.addElement(GameScreen.CreateEffectEnd(100, 0, MotherCanvas.hw - 65, 45, 0, null));
			EffectNum o2 = new EffectNum("-" + dam, MotherCanvas.hw + 100, 60, 2);
			obj.vecEff.addElement(o2);
		}
	}
}
