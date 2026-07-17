using System;

public class MapBackGround
{
	private sbyte typeMap;

	private sbyte[] mList3;

	private int[] mHImg;

	private int[] mWImg;

	private int[] mSpeedImg;

	private int[] mHBegin;

	private int maxsize;

	private int sizeScreen;

	private int hLimit;

	private int valueMyRandom;

	private int sizeScreen2;

	private int nX;

	private int hEff;

	private int valueTick = 3;

	private int valueEndTurn = 150;

	private int yplusCloud;

	private int hSky;

	public int hBack;

	public bool isEff;

	public bool isEffKhangia;

	public bool isNight;

	public static bool isBeginVyCloud;

	private mVector mVecKhangia = new mVector("MapBackGround.mvecKhangia");

	private mVector mVecThunder = new mVector();

	private Point[][] PosCloud;

	private Point laboon;

	private short[][] PosObj12 = new short[3][]
	{
		new short[3] { 2, 100, 0 },
		new short[3] { 3, 160, 0 },
		new short[3] { 3, 280, 0 }
	};

	private short[][] PosObj13 = new short[4][]
	{
		new short[3] { 2, 40, 10 },
		new short[3] { 3, 80, 3 },
		new short[3] { 3, 180, 1 },
		new short[3] { 2, 300, 14 }
	};

	private short[][] PosObj14 = new short[7][]
	{
		new short[3] { 3, 80, 0 },
		new short[3] { 2, 140, 0 },
		new short[3] { 2, 260, 0 },
		new short[3] { 4, 70, 8 },
		new short[3] { 5, 20, 1 },
		new short[3] { 5, 230, 4 },
		new short[3] { 4, 290, 10 }
	};

	private short[][] PosObj15 = new short[5][]
	{
		new short[3] { 1, 0, 47 },
		new short[3] { 2, 26, 56 },
		new short[3] { 3, 62, 75 },
		new short[3] { 2, 110, 56 },
		new short[3] { 1, 144, 47 }
	};

	private short[][] PosObj16 = new short[13][]
	{
		new short[3] { 0, 10, 47 },
		new short[3] { 0, 45, 50 },
		new short[3] { 0, 105, 54 },
		new short[3] { 0, 120, 57 },
		new short[3] { 0, 180, 52 },
		new short[3] { 0, 210, 48 },
		new short[3] { 0, 230, 47 },
		new short[3] { 0, 280, 50 },
		new short[3] { 0, 310, 52 },
		new short[3] { 0, 30, 32 },
		new short[3] { 0, 140, 27 },
		new short[3] { 0, 205, 30 },
		new short[3] { 0, 270, 33 }
	};

	private short[][] PosObj17 = new short[22][]
	{
		new short[4] { 2, 0, -50, -180 },
		new short[4] { 2, 0, 280, -210 },
		new short[4] { 2, 2, 200, -200 },
		new short[4] { 2, 2, 105, -175 },
		new short[4] { 3, 2, 170, -125 },
		new short[4] { 3, 2, 20, -240 },
		new short[4] { 3, 2, 35, -75 },
		new short[4] { 3, 2, 260, -95 },
		new short[4] { 5, 0, 20, -235 },
		new short[4] { 3, 0, 20, -200 },
		new short[4] { 5, 0, -60, -150 },
		new short[4] { 0, 0, 110, -174 },
		new short[4] { 1, 2, 16, -51 },
		new short[4] { 1, 2, -190, -54 },
		new short[4] { 1, 0, 270, -56 },
		new short[4] { 3, 0, -339, -170 },
		new short[4] { 4, 0, -188, -157 },
		new short[4] { 3, 2, -65, -115 },
		new short[4] { 4, 2, 105, -130 },
		new short[4] { 4, 0, 260, -164 },
		new short[4] { 4, 0, 300, -260 },
		new short[4] { 4, 0, 200, -255 }
	};

	private short[][] PosObj18 = new short[17][]
	{
		new short[4] { 0, 0, -127, -165 },
		new short[4] { 2, 0, 20, -115 },
		new short[4] { 4, 0, 25, -80 },
		new short[4] { 2, 2, 260, -135 },
		new short[4] { 3, 2, 262, -105 },
		new short[4] { 4, 2, 222, -185 },
		new short[4] { 4, 0, 160, -65 },
		new short[4] { 4, 0, -110, -45 },
		new short[4] { 4, 0, -80, -165 },
		new short[4] { 5, 2, 56, -195 },
		new short[4] { 0, 2, 192, -129 },
		new short[4] { 1, 0, -120, 0 },
		new short[4] { 1, 2, 110, 0 },
		new short[4] { 3, 2, 192, -95 },
		new short[4] { 3, 0, 56, -160 },
		new short[4] { 3, 0, -127, -130 },
		new short[4] { 4, 2, -212, -75 }
	};

	private short[][] PosObj19 = new short[15][]
	{
		new short[4] { 5, 0, -30, -12 },
		new short[4] { 0, 0, 30, -10 },
		new short[4] { 5, 2, 52, -195 },
		new short[4] { 3, 2, 52, -160 },
		new short[4] { 0, 2, 72, -120 },
		new short[4] { 0, 0, -106, -133 },
		new short[4] { 2, 0, -139, -190 },
		new short[4] { 5, 0, -180, -193 },
		new short[4] { 3, 0, 72, -85 },
		new short[4] { 3, 0, -106, -100 },
		new short[4] { 3, 0, -180, -160 },
		new short[4] { 2, 2, 190, -160 },
		new short[4] { 4, 0, 200, -120 },
		new short[4] { 3, 0, 180, -205 },
		new short[4] { 3, 0, -100, -205 }
	};

	private short[][] PosObj21 = new short[5][]
	{
		new short[4] { 20, -42, 1, 0 },
		new short[4] { -24, -79, 0, 1 },
		new short[4] { -14, -167, 3, 1 },
		new short[4] { 26, -178, 2, 0 },
		new short[4] { 16, -115, 1, 1 }
	};

	private short[][] PosObj22 = new short[7][]
	{
		new short[3] { 120, 10, 3 },
		new short[3] { 250, -5, 4 },
		new short[3] { 380, 3, 3 },
		new short[3] { 500, -10, 3 },
		new short[3] { 630, 5, 4 },
		new short[3] { 750, 8, 3 },
		new short[3] { 900, 0, 4 }
	};

	private short[][] PosObj23 = new short[4][]
	{
		new short[3] { 60, 5, 5 },
		new short[3] { 190, 0, 6 },
		new short[3] { 320, -5, 5 },
		new short[3] { 450, 3, 6 }
	};

	private short[][] PosObj24 = new short[4][]
	{
		new short[3] { 30, 0, 8 },
		new short[3] { 110, -5, 7 },
		new short[3] { 200, 5, 7 },
		new short[3] { 300, 3, 8 }
	};

	private short[][] PosObj25 = new short[7][]
	{
		new short[4] { 0, 2, 52, -160 },
		new short[4] { 0, 0, 72, -85 },
		new short[4] { 1, 0, -106, -100 },
		new short[4] { 0, 0, -180, -160 },
		new short[4] { 1, 0, 200, -120 },
		new short[4] { 0, 0, 180, -205 },
		new short[4] { 1, 0, -100, -205 }
	};

	private short[][] PosObj;

	private short[][] PosObj2;

	private short[][][] posKhanGia = new short[4][][]
	{
		new short[6][]
		{
			new short[2] { 73, 16 },
			new short[2] { 55, 13 },
			new short[2] { 28, 9 },
			new short[2] { 49, 3 },
			new short[2] { 40, 1 },
			new short[2] { 64, -2 }
		},
		new short[5][]
		{
			new short[2] { 53, 17 },
			new short[2] { 80, 13 },
			new short[2] { 57, 4 },
			new short[2] { 64, 3 },
			new short[2] { 33, 0 }
		},
		new short[4][]
		{
			new short[2] { 23, 16 },
			new short[2] { 65, 15 },
			new short[2] { 48, 7 },
			new short[2] { 77, -3 }
		},
		new short[5][]
		{
			new short[2] { 77, 5 },
			new short[2] { 55, 4 },
			new short[2] { 28, 1 },
			new short[2] { 43, 0 },
			new short[2] { 62, -2 }
		}
	};

	private short[] PosObj20 = new short[8] { 16, 30, 40, 65, 76, 84, 98, 108 };

	private mImage[] mImg;

	private mImage[] mImgCloud;

	private mImage[] imgBgLogin;

	private mImage[] imgSeaLogin;

	private mImage[] mImgCloudNight;

	private mImage[] mBigNPC;

	private mImage imgSpec;

	private mImage imgSky;

	private mImage imgThunder;

	private FrameImage fraStar;

	private FrameImage fraImgNew;

	public int color;

	public int colorMini;

	public sbyte lastMap = -1;

	public static FrameImage fraChong;

	public static FrameImage fraChongNho;

	public static FrameImage fraWater7;

	public Point[] posXuongRong;

	public Point[] posXuongRong2;

	public int xCamOffline;

	private const sbyte IDBACK_0 = 0;

	private const sbyte IDBACK_1 = 1;

	private const sbyte IDBACK_2 = 2;

	private const sbyte IDBACK_3 = 3;

	private const sbyte IDBACK_4 = 4;

	private const sbyte IDBACK_5 = 5;

	private const sbyte IDBACK_6 = 6;

	private const sbyte IDBACK_7 = 7;

	private const sbyte IDBACK_8 = 8;

	private const sbyte IDBACK_9 = 9;

	private const sbyte IDBACK_10 = 10;

	private const sbyte IDBACK_11 = 11;

	private const sbyte IDBACK_12 = 12;

	private const sbyte IDBACK_13 = 13;

	private const sbyte IDBACK_14 = 14;

	private const sbyte IDBACK_15 = 15;

	private const sbyte IDBACK_16 = 16;

	private const sbyte IDBACK_17 = 17;

	private const sbyte IDBACK_18 = 18;

	private const sbyte IDBACK_19 = 19;

	private const sbyte IDBACK_20 = 20;

	private const sbyte IDBACK_21 = 21;

	private const sbyte IDBACK_22 = 22;

	private const sbyte IDBACK_23 = 23;

	private const sbyte IDBACK_24 = 24;

	private const sbyte IDBACK_25 = 25;

	private const sbyte IDBACK_26 = 26;

	private const sbyte IDBACK_27 = 27;

	private const sbyte IDBACK_28 = 28;

	private const sbyte IDBACK_29 = 29;

	private const sbyte IDBACK_30 = 30;

	private const sbyte IDBACK_31 = 31;

	private const sbyte IDBACK_32 = 32;

	private const sbyte IDBACK_33 = 33;

	private const sbyte IDBACK_34 = 34;

	private const sbyte IDBACK_35 = 35;

	private const sbyte IDBACK_36 = 36;

	private const sbyte IDBACK_37 = 37;

	private const sbyte IDBACK_38 = 38;

	private const sbyte IDBACK_39 = 39;

	private const sbyte IDBACK_40 = 40;

	private const sbyte IDBACK_41 = 41;

	private const sbyte IDBACK_42 = 42;

	private const sbyte IDBACK_43 = 43;

	private const sbyte IDBACK_44 = 44;

	private const sbyte IDBACK_45 = 45;

	private const sbyte IDBACK_46 = 46;

	private const sbyte IDBACK_47 = 47;

	private const sbyte IDBACK_51 = 51;

	private const sbyte IDBACK_53 = 53;

	private const sbyte IDBACK_55 = 55;

	private const sbyte IDBACK_60 = 60;

	private const sbyte IDBACK_61 = 61;

	private const sbyte IDBACK_62 = 62;

	private const sbyte IDBACK_63 = 63;

	private int yeff;

	public static int tickRanThunder = 10;

	public static int IndexCloud;

	private sbyte indexBg;

	private Point[] mPointObj;

	public void setBackGround(sbyte type, short h)
	{
		mVecThunder.removeAllElements();
		mVecKhangia.removeAllElements();
		isEffKhangia = false;
		laboon = null;
		typeMap = type;
		if (type == 35)
		{
			hBack = 700 - h;
		}
		else
		{
			hBack = GameCanvas.loadmap.maxHMap - h;
		}
		yeff = 0;
		isNight = false;
		mImgCloudNight = null;
		if ((GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg) && checkDonotLow())
		{
			if (h > 280)
			{
				h = 280;
				hBack = GameCanvas.loadmap.maxHMap - h;
			}
			color = 9356287;
			colorMini = 4367095;
			if (LoadMap.idTile == 11 || LoadMap.idTile == 14)
			{
				color = 15923962;
				colorMini = 15923962;
				return;
			}
			mHImg = new int[1];
			mHImg[0] = 96;
			mWImg = new int[mHImg.Length];
			mWImg[0] = 96;
			mSpeedImg = new int[mHImg.Length];
			mSpeedImg[0] = 8;
			mImg = new mImage[mHImg.Length];
			if (typeMap == 18)
			{
				mImg[0] = mImage.createImage("/bg/login3.png");
				color = 6192006;
				colorMini = 2453679;
				isNight = true;
			}
			else if (typeMap == 20 || typeMap == 21 || typeMap == 41)
			{
				mImg[0] = mImage.createImage("/bg/b121.png");
				color = 14018038;
				colorMini = 14018038;
				mWImg[0] = 124;
				if (typeMap == 21)
				{
					mHImg[0] = 45;
				}
				else
				{
					mHImg[0] = 55;
				}
				mSpeedImg[0] = 1;
			}
			else
			{
				mImg[0] = mImage.createImage("/bg/login1.png");
			}
			mHBegin = new int[mHImg.Length];
			int num = 0;
			num += mHImg[0];
			mHBegin[0] = hBack - num;
			return;
		}
		if (lastMap != typeMap)
		{
			yplusCloud = 0;
			switch (typeMap)
			{
			case 0:
			{
				hLimit = 100;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 75;
				color = 10343167;
				colorMini = 43488;
				mHImg = new int[3];
				mHImg[0] = 70;
				mHImg[1] = -14;
				mHImg[2] = 55;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 264;
				mWImg[1] = 264;
				mWImg[2] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 6;
				mSpeedImg[2] = 20;
				mImg = new mImage[mHImg.Length];
				for (int num5 = 0; num5 < mImg.Length; num5++)
				{
					if (num5 == 2)
					{
						mImg[num5] = mImage.createImage("/bg/login1.png");
						continue;
					}
					mImg[num5] = mImage.createImage("/bg/b" + typeMap + num5 + ".png");
				}
				if (fraChong == null || fraChongNho == null)
				{
					fraChong = new FrameImage(mImage.createImage("/bg/chong.png"), 50, 50);
					fraChongNho = new FrameImage(mImage.createImage("/bg/chongnho.png"), 24, 24);
				}
				imgSeaLogin = new mImage[1];
				imgSeaLogin[0] = mImage.createImage("/bg/sea0.png");
				break;
			}
			case 1:
			case 16:
			case 35:
				hLimit = 74;
				if (type == 35)
				{
					imgSky = mImage.createImage("/bg/sky4.png");
					color = 4616312;
				}
				else
				{
					imgSky = mImage.createImage("/bg/sky0.png");
					color = 10343167;
				}
				hSky = 105;
				colorMini = 43488;
				mHImg = new int[2];
				mHImg[0] = 24;
				mHImg[1] = 72;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mWImg[1] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 2;
				mSpeedImg[1] = 4;
				mImg = new mImage[mHImg.Length];
				mImg[0] = mImage.createImage("/bg/b03.png");
				mImg[1] = mImage.createImage("/bg/login1.png");
				yplusCloud = -15;
				break;
			case 2:
			case 45:
			{
				hLimit = 80;
				imgSky = mImage.createImage("/bg/sky1.png");
				hSky = 55;
				color = 8309991;
				colorMini = 4367095;
				mHImg = new int[2];
				mHImg[0] = 72;
				mHImg[1] = -5;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 219;
				mWImg[1] = 209;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 12;
				mImg = new mImage[4];
				for (int num3 = 0; num3 < 4; num3++)
				{
					mImg[num3] = mImage.createImage("/bg/b" + 2 + num3 + ".png");
				}
				break;
			}
			case 3:
				hLimit = 85;
				imgSky = mImage.createImage("/bg/sky1.png");
				hSky = 75;
				color = 8309991;
				colorMini = 7256539;
				mHImg = new int[3];
				mHImg[0] = 64;
				mHImg[1] = -23;
				mHImg[2] = 40;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 234;
				mWImg[1] = 32;
				mWImg[2] = 209;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 8;
				mSpeedImg[2] = 20;
				mImg = new mImage[3];
				mImg[0] = mImage.createImage("/bg/b10.png");
				mImg[1] = mImage.createImage("/bg/b11.png");
				mImg[2] = mImage.createImage("/bg/b21.png");
				break;
			case 4:
			{
				hLimit = 80;
				imgSky = mImage.createImage("/bg/sky1.png");
				hSky = 75;
				color = 8309991;
				colorMini = 4367095;
				mHImg = new int[2];
				mHImg[0] = 72;
				mHImg[1] = -5;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 219;
				mWImg[1] = 209;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 12;
				mImg = new mImage[3];
				for (int j = 0; j < 3; j++)
				{
					mImg[j] = mImage.createImage("/bg/b" + 2 + j + ".png");
				}
				break;
			}
			case 5:
				hLimit = 70;
				imgSky = mImage.createImage("/bg/sky1.png");
				hSky = 55;
				color = 8309991;
				colorMini = 7256539;
				mHImg = new int[2];
				mHImg[0] = 60;
				mHImg[1] = 10;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 237;
				mWImg[1] = 209;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 6;
				mSpeedImg[1] = 20;
				mImg = new mImage[mHImg.Length];
				mImg[0] = mImage.createImage("/bg/b50.png");
				mImg[1] = mImage.createImage("/bg/b21.png");
				break;
			case 6:
				hLimit = 96;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 35;
				color = 10343167;
				colorMini = 9356287;
				mHImg = new int[1];
				mHImg[0] = 96;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 20;
				mImg = new mImage[mHImg.Length];
				mImg[0] = mImage.createImage("/bg/login1.png");
				break;
			case 7:
			case 26:
			case 47:
				hLimit = 35;
				hSky = 80;
				color = 7920877;
				colorMini = 7984639;
				if (typeMap == 26)
				{
					color = 2639445;
					colorMini = 2639445;
				}
				else
				{
					imgSky = mImage.createImage("/bg/sky2.png");
				}
				mHImg = new int[1];
				mHImg[0] = 35;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 168;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 16;
				if (typeMap == 47)
				{
					mImg = new mImage[3];
					mImg[0] = mImage.createImage("/bg/b40.png");
					mImg[1] = mImage.createImage("/bg/b40_n.png");
					mImg[2] = mImage.createImage("/bg/b23.png");
				}
				else
				{
					mImg = new mImage[2];
					mImg[0] = mImage.createImage("/bg/b40.png");
					mImg[1] = mImage.createImage("/bg/b40_n.png");
				}
				yplusCloud = -40;
				break;
			case 8:
				hLimit = 96;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 35;
				color = 10343167;
				colorMini = 4367095;
				mHImg = new int[1];
				mHImg[0] = 96;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 20;
				mImg = new mImage[4];
				mImg[0] = mImage.createImage("/bg/login1.png");
				mImg[3] = mImage.createImage("/bg/b23.png");
				valueTick = CRes.random(5, 10);
				valueEndTurn = CRes.random(12, 21) * 10;
				break;
			case 40:
			{
				hLimit = 96;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 35;
				color = 10343167;
				colorMini = 4367095;
				mHImg = new int[1];
				mHImg[0] = 96;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 20;
				mImg = new mImage[3];
				mImg[0] = mImage.createImage("/bg/login1.png");
				mImg[1] = mImage.createImage("/bg/b23.png");
				mImg[2] = mImage.createImage("/bg/b72.png");
				valueTick = CRes.random(5, 10);
				valueEndTurn = CRes.random(12, 21) * 10;
				PosObj = new short[100][];
				int num16 = 0;
				for (int num17 = 0; num17 <= GameCanvas.loadmap.maxWMap / 120; num17++)
				{
					for (int num18 = 0; num18 < PosObj20.Length; num18++)
					{
						if (CRes.random(5) != 0)
						{
							PosObj[num16] = new short[6];
							PosObj[num16][0] = (short)(PosObj20[num18] + CRes.random_Am_0(3) + num17 * 120);
							PosObj[num16][1] = (short)CRes.random(3);
							PosObj[num16][2] = (short)CRes.random(6);
							PosObj[num16][3] = 0;
							PosObj[num16][4] = 0;
							PosObj[num16][5] = 0;
							if (PosObj[num16][0] < GameCanvas.loadmap.maxWMap / 2)
							{
								PosObj[num16][5] = 2;
							}
							num16++;
						}
					}
				}
				PosObj[num16] = null;
				break;
			}
			case 9:
				hLimit = 96;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 105;
				color = 10343167;
				colorMini = 429994;
				mHImg = new int[3];
				mHImg[0] = 24;
				mHImg[1] = 24;
				mHImg[2] = 48;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mWImg[1] = 24;
				mWImg[2] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 6;
				mSpeedImg[1] = 12;
				mSpeedImg[2] = 12;
				mImg = new mImage[3];
				mImg[2] = mImage.createImage("/bg/login1.png");
				mImg[1] = mImage.createImage("/bg/sea0.png");
				mImg[0] = mImage.createImage("/bg/b03.png");
				break;
			case 10:
				hLimit = 74;
				color = 9356287;
				colorMini = 43488;
				mHImg = new int[2];
				mHImg[0] = 24;
				mHImg[1] = 72;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mWImg[1] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 12;
				mImg = new mImage[mHImg.Length];
				mImg[0] = mImage.createImage("/bg/b03.png");
				mImg[1] = mImage.createImage("/bg/login1.png");
				break;
			case 11:
			case 27:
				hLimit = 107;
				hSky = 45;
				color = 9029626;
				colorMini = 43488;
				if (typeMap == 27)
				{
					color = 3557982;
				}
				else
				{
					imgSky = mImage.createImage("/bg/sky3.png");
				}
				mHImg = new int[1];
				mHImg[0] = 107;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 240;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mImg = new mImage[2];
				mImg[0] = mImage.createImage("/bg/b60.png");
				mImg[1] = mImage.createImage("/bg/b60_n.png");
				yplusCloud = -20;
				break;
			case 12:
			case 13:
			case 14:
			case 24:
			case 25:
				hLimit = 74;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 105;
				color = 10343167;
				colorMini = 43488;
				mHImg = new int[2];
				mHImg[0] = 24;
				mHImg[1] = 72;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mWImg[1] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 12;
				if (typeMap == 12)
				{
					mImg = new mImage[4];
					mImg[0] = mImage.createImage("/bg/b03.png");
					mImg[1] = mImage.createImage("/bg/login1.png");
					mImg[2] = mImage.createImage("/bg/nui1.png");
					mImg[3] = mImage.createImage("/bg/nui2.png");
					PosObj = PosObj12;
				}
				else if (typeMap == 13)
				{
					mImg = new mImage[4];
					mImg[0] = mImage.createImage("/bg/b03.png");
					mImg[1] = mImage.createImage("/bg/login1.png");
					mImg[2] = mImage.createImage("/bg/boatnear.png");
					mImg[3] = mImage.createImage("/bg/boatfar.png");
					PosObj = PosObj13;
					if (GameCanvas.loadmap.idMap == 191 && fraWater7 == null)
					{
						fraWater7 = new FrameImage(mImage.createImage("/bg/water7.png"), 3);
					}
				}
				else if (typeMap == 14)
				{
					mImg = new mImage[6];
					mImg[0] = mImage.createImage("/bg/b03.png");
					mImg[1] = mImage.createImage("/bg/login1.png");
					mImg[2] = mImage.createImage("/bg/nui1.png");
					mImg[3] = mImage.createImage("/bg/nui2.png");
					mImg[4] = mImage.createImage("/bg/boatnear.png");
					mImg[5] = mImage.createImage("/bg/boatfar.png");
					PosObj = PosObj14;
				}
				else if (typeMap == 24)
				{
					mImg = new mImage[6];
					mImg[0] = mImage.createImage("/bg/b03.png");
					mImg[1] = mImage.createImage("/bg/login1.png");
					mImg[2] = mImage.createImage("/bg/nui3.png");
					mImg[3] = mImage.createImage("/bg/nui4.png");
					mImg[4] = mImage.createImage("/bg/boatnear.png");
					mImg[5] = mImage.createImage("/bg/boatfar.png");
					PosObj = PosObj14;
				}
				else if (typeMap == 25)
				{
					mImg = new mImage[6];
					mImg[0] = mImage.createImage("/bg/b03.png");
					mImg[1] = mImage.createImage("/bg/login1.png");
					mImg[2] = mImage.createImage("/bg/nui5.png");
					mImg[3] = mImage.createImage("/bg/nui6.png");
					mImg[4] = mImage.createImage("/bg/boatnear.png");
					mImg[5] = mImage.createImage("/bg/boatfar.png");
					PosObj = PosObj14;
				}
				break;
			case 15:
			case 41:
			case 60:
			case 61:
			{
				hLimit = 80;
				imgSky = mImage.createImage("/bg/sky6.png");
				hSky = 80;
				color = 7920877;
				colorMini = 429994;
				mHImg = new int[2];
				mHImg[1] = 30;
				mHImg[0] = 57;
				mWImg = new int[mHImg.Length];
				mWImg[1] = 209;
				mWImg[0] = 125;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[1] = 12;
				mSpeedImg[0] = 1;
				mImg = new mImage[3];
				if (typeMap == 41 || typeMap == 61)
				{
					mImgCloud = null;
					color = 10668525;
					mImg[2] = mImage.createImage("/bg/b74.png");
					mImg[0] = mImage.createImage("/bg/b73.png");
				}
				else
				{
					mImg[2] = mImage.createImage("/bg/b71.png");
					mImg[1] = mImage.createImage("/bg/b21.png");
					mImg[0] = mImage.createImage("/bg/b70.png");
				}
				if (typeMap == 60 || typeMap == 61)
				{
					break;
				}
				int num9 = 0;
				if (MotherCanvas.w >= 480)
				{
					num9 = 34;
				}
				else if (MotherCanvas.w >= 320)
				{
					num9 = 24;
				}
				for (int num10 = 0; num10 <= GameCanvas.loadmap.maxWMap / mWImg[0] + 1; num10++)
				{
					int num11 = CRes.random(posKhanGia.Length);
					for (int num12 = 0; num12 < posKhanGia[num11].Length; num12++)
					{
						Point_Focus point_Focus = new Point_Focus(GameCanvas.loadmap.maxWMap / 125 * 125 - num10 * mWImg[0] + posKhanGia[num11][num12][0] + GameCanvas.loadmap.limitW % 125 + num9, hBack - posKhanGia[num11][num12][1]);
						point_Focus.dis = CRes.random(6) * 2;
						mVecKhangia.addElement(point_Focus);
					}
				}
				break;
			}
			case 53:
				hLimit = 85;
				imgSky = mImage.createImage("/bg/sky1.png");
				hSky = 65;
				color = 8309991;
				colorMini = 7256539;
				mHImg = new int[3];
				mHImg[0] = 64;
				mHImg[1] = -30;
				mHImg[2] = 40;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 234;
				mWImg[1] = 32;
				mWImg[2] = 209;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 8;
				mSpeedImg[2] = 20;
				mImg = new mImage[4];
				mImg[0] = mImage.createImage("/bg/b10.png");
				mImg[1] = mImage.createImage("/bg/b11.png");
				mImg[2] = mImage.createImage("/bg/b21.png");
				mImg[3] = mImage.createImage("/bg/b23.png");
				break;
			case 55:
				hLimit = 70;
				imgSky = mImage.createImage("/bg/sky1.png");
				hSky = 55;
				color = 8309991;
				colorMini = 7256539;
				mHImg = new int[2];
				mHImg[0] = 60;
				mHImg[1] = 10;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 237;
				mWImg[1] = 209;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 6;
				mSpeedImg[1] = 20;
				mImg = new mImage[3];
				mImg[0] = mImage.createImage("/bg/b50.png");
				mImg[1] = mImage.createImage("/bg/b21.png");
				mImg[2] = mImage.createImage("/bg/b23.png");
				break;
			case 51:
				hLimit = 74;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 105;
				color = 10343167;
				colorMini = 43488;
				mHImg = new int[2];
				mHImg[0] = 24;
				mHImg[1] = 72;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mWImg[1] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 12;
				mImg = new mImage[3];
				mImg[0] = mImage.createImage("/bg/b03.png");
				mImg[1] = mImage.createImage("/bg/login1.png");
				mImg[2] = mImage.createImage("/bg/b23.png");
				break;
			case 17:
			{
				hLimit = 110;
				imgSky = mImage.createImage("/bg/sky0.png");
				hSky = 80;
				color = 10343167;
				colorMini = 9356287;
				mHImg = new int[1];
				mHImg[0] = 62;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 48;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 1;
				fraImgNew = new FrameImage(144, 37, 55);
				mImg = new mImage[8];
				for (int num6 = 0; num6 < mImg.Length; num6++)
				{
					switch (num6)
					{
					case 6:
						mImg[num6] = mImage.createImage("/bg/boateff.png");
						break;
					default:
						mImg[num6] = mImage.createImage("/bg/b8" + num6 + ".png");
						break;
					case 7:
						break;
					}
				}
				laboon = new Point(140, 0);
				break;
			}
			case 18:
			{
				isNight = true;
				mImgCloud = null;
				hLimit = 150;
				imgSky = mImage.createImage("/bg/sky4.png");
				hSky = 70;
				color = 4616312;
				colorMini = 4943991;
				mHImg = new int[2];
				mHImg[0] = 63;
				mHImg[1] = -12;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 247;
				mWImg[1] = 91;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 1;
				mSpeedImg[1] = 2;
				mImg = new mImage[6];
				for (int num19 = 0; num19 < mImg.Length; num19++)
				{
					mImg[num19] = mImage.createImage("/bg/b9" + num19 + ".png");
				}
				int num20 = (GameCanvas.loadmap.maxWMap + 30) / 190 + 1;
				if (num20 < 2)
				{
					num20 = 2;
				}
				posXuongRong = new Point[num20];
				for (int num21 = 0; num21 < posXuongRong.Length; num21++)
				{
					Point point = new Point(-30 + num21 * 190, 0);
					point.frame = (num21 + GameCanvas.loadmap.idMap) % 2;
					if ((num21 + GameCanvas.loadmap.idMap) % 4 == 1)
					{
						point.isSmall = true;
					}
					posXuongRong[num21] = point;
				}
				num20 = (GameCanvas.loadmap.maxWMap - 90) / 190 + 1;
				if (num20 < 2)
				{
					num20 = 2;
				}
				posXuongRong2 = new Point[num20];
				for (int num22 = 0; num22 < posXuongRong2.Length; num22++)
				{
					Point point2 = new Point(90 + num22 * 190, 0);
					if ((num22 + GameCanvas.loadmap.idMap) % 4 == 3)
					{
						point2.isSmall = true;
					}
					posXuongRong2[num22] = point2;
				}
				break;
			}
			case 19:
			{
				hLimit = 95;
				imgSky = mImage.createImage("/bg/sky5.png");
				hSky = 55;
				color = 8707577;
				colorMini = 7984639;
				mHImg = new int[2];
				mHImg[0] = 52;
				mHImg[1] = -12;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 168;
				mWImg[1] = 168;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 8;
				mSpeedImg[1] = 24;
				mImg = new mImage[4];
				for (int num14 = 0; num14 < mImg.Length; num14++)
				{
					mImg[num14] = mImage.createImage("/bg/b10" + num14 + ".png");
				}
				break;
			}
			case 20:
			{
				hLimit = 120;
				imgSky = mImage.createImage("/bg/sky6.png");
				hSky = 50;
				color = 10668525;
				colorMini = 13819368;
				mHImg = new int[1];
				mHImg[0] = 135;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 143;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 12;
				mImg = new mImage[5];
				for (int num8 = 0; num8 < mImg.Length; num8++)
				{
					mImg[num8] = mImage.createImage("/bg/b11" + num8 + ".png");
				}
				PosObj = PosObj15;
				PosObj2 = PosObj16;
				imgSpec = mImage.createImage("/bg/b121.png");
				yplusCloud = -20;
				break;
			}
			case 21:
			{
				hLimit = 120;
				imgSky = mImage.createImage("/bg/sky6.png");
				hSky = 110;
				color = 10668525;
				colorMini = 14018038;
				mHImg = new int[2];
				mHImg[0] = 43;
				mHImg[1] = 40;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 124;
				mWImg[1] = 124;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 1;
				mSpeedImg[1] = 8;
				mImg = new mImage[2];
				for (int num7 = 0; num7 < mImg.Length; num7++)
				{
					mImg[num7] = mImage.createImage("/bg/b12" + num7 + ".png");
				}
				yplusCloud = -20;
				break;
			}
			case 22:
			{
				hLimit = 120;
				if (!GameCanvas.isLowGraOrWP_PvP() && !GameCanvas.isOffBg)
				{
					imgSky = mImage.createImage("/bg/sky6.png");
				}
				hSky = 65;
				color = 10668525;
				if (GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg)
				{
					color = 13819368;
				}
				colorMini = 13819368;
				mHImg = new int[1];
				mHImg[0] = 135;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 143;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 12;
				mImg = new mImage[6];
				for (int l = 0; l < mImg.Length; l++)
				{
					mImg[l] = mImage.createImage("/bg/b11" + l + ".png");
				}
				PosObj = PosObj15;
				PosObj2 = PosObj16;
				imgSpec = mImage.createImage("/bg/b121.png");
				yplusCloud = -20;
				break;
			}
			case 23:
			{
				hLimit = 95;
				if (!GameCanvas.lowGraphic && !GameCanvas.isOffBg)
				{
					imgSky = mImage.createImage("/bg/sky5.png");
				}
				hSky = 55;
				color = 8707577;
				colorMini = 7984639;
				mHImg = new int[2];
				mHImg[0] = 52;
				mHImg[1] = -12;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 168;
				mWImg[1] = 168;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 8;
				mSpeedImg[1] = 24;
				mImg = new mImage[5];
				for (int num26 = 0; num26 < mImg.Length; num26++)
				{
					if (num26 == 4)
					{
						mImg[num26] = mImage.createImage("/bg/b23.png");
					}
					else
					{
						mImg[num26] = mImage.createImage("/bg/b10" + num26 + ".png");
					}
				}
				break;
			}
			case 28:
				hLimit = 70;
				imgSky = mImage.createImage("/bg/sky7.png");
				hSky = 40;
				color = 37344;
				colorMini = 37344;
				mHImg = new int[1];
				mHImg[0] = 66;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 126;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 2;
				mImg = new mImage[2];
				mImg[0] = mImage.createImage("/bg/b131.png");
				mImg[1] = mImage.createImage("/bg/b23.png");
				mImgCloud = null;
				break;
			case 31:
				hLimit = 70;
				imgSky = mImage.createImage("/bg/sky7.png");
				hSky = 75;
				color = 37344;
				colorMini = 16776368;
				mHImg = new int[2];
				mHImg[0] = 20;
				mHImg[1] = 20;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 126;
				mWImg[1] = 190;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 2;
				mSpeedImg[1] = 8;
				mImg = new mImage[mHImg.Length];
				mImg[0] = mImage.createImage("/bg/b131.png");
				mImg[1] = mImage.createImage("/bg/b133.png");
				mImgCloud = null;
				break;
			case 42:
			{
				hLimit = 70;
				imgSky = mImage.createImage("/bg/sky7.png");
				hSky = 75;
				color = 37344;
				colorMini = 16776368;
				mHImg = new int[2];
				mHImg[0] = 20;
				mHImg[1] = 20;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 126;
				mWImg[1] = 190;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 2;
				mSpeedImg[1] = 8;
				mImg = new mImage[3];
				mImg[0] = mImage.createImage("/bg/b131.png");
				mImg[1] = mImage.createImage("/bg/b133.png");
				mImg[2] = mImage.createImage("/bg/b72.png");
				mImgCloud = null;
				PosObj = new short[100][];
				int num23 = 0;
				for (int num24 = 0; num24 <= GameCanvas.loadmap.maxWMap / 120; num24++)
				{
					for (int num25 = 0; num25 < PosObj20.Length; num25++)
					{
						if (CRes.random(5) != 0)
						{
							PosObj[num23] = new short[6];
							PosObj[num23][0] = (short)(PosObj20[num25] + CRes.random_Am_0(3) + num24 * 120);
							PosObj[num23][1] = (short)CRes.random(3);
							PosObj[num23][2] = (short)CRes.random(6);
							PosObj[num23][3] = 0;
							PosObj[num23][4] = 0;
							PosObj[num23][5] = 0;
							if (PosObj[num23][0] < GameCanvas.loadmap.maxWMap / 2)
							{
								PosObj[num23][5] = 2;
							}
							num23++;
						}
					}
				}
				PosObj[num23] = null;
				break;
			}
			case 43:
			case 62:
			{
				hLimit = 70;
				imgSky = null;
				hSky = 75;
				color = 5594686;
				colorMini = 5594686;
				mHImg = new int[3];
				mHImg[0] = 15;
				mHImg[1] = 15;
				mHImg[2] = 15;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 211;
				mWImg[1] = 211;
				mWImg[2] = 211;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 1;
				mSpeedImg[1] = 3;
				mSpeedImg[2] = 12;
				mImg = new mImage[9];
				for (int num15 = 0; num15 < mImg.Length; num15++)
				{
					mImg[num15] = mImage.createImage("/bg/b18" + num15 + ".png");
				}
				mImgCloud = null;
				break;
			}
			case 44:
			{
				hLimit = 100;
				imgSky = mImage.createImage("/bg/sky9.png");
				hSky = 130;
				color = 9953276;
				colorMini = 9953276;
				mHImg = new int[3];
				mHImg[0] = 24;
				mHImg[1] = 20;
				mHImg[2] = 40;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 168;
				mWImg[1] = 212;
				mWImg[2] = 250;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 2;
				mSpeedImg[1] = 5;
				mSpeedImg[2] = 8;
				mImg = new mImage[6];
				for (int num13 = 0; num13 < mImg.Length; num13++)
				{
					switch (num13)
					{
					case 3:
						mImg[num13] = mImage.createImage("/bg/b170.png");
						mImg[num13].setWH();
						break;
					case 4:
						mImg[num13] = mImage.createImage("/bg/b171.png");
						mImg[num13].setWH();
						break;
					case 5:
						mImg[num13] = mImage.createImage("/bg/b172.png");
						mImg[num13].setWH();
						break;
					default:
						mImg[num13] = mImage.createImage("/bg/b19" + num13 + ".png");
						break;
					}
				}
				mImgCloud = null;
				break;
			}
			case 29:
				hLimit = 80;
				imgSky = mImage.createImage("/bg/sky8.png");
				hSky = 70;
				color = 47092;
				colorMini = 16776621;
				mHImg = new int[2];
				mHImg[0] = 0;
				mHImg[1] = 20;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 240;
				mWImg[1] = 190;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 2;
				mSpeedImg[1] = 8;
				mImg = new mImage[2];
				mImg[0] = mImage.createImage("/bg/b132.png");
				mImg[1] = mImage.createImage("/bg/b133.png");
				mImgCloud = null;
				yplusCloud = -20;
				break;
			case 30:
			case 32:
				hLimit = 80;
				imgSky = mImage.createImage("/bg/sky8.png");
				hSky = 70;
				color = 47092;
				colorMini = 16776621;
				mHImg = new int[2];
				mHImg[0] = 0;
				mHImg[1] = 20;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 240;
				mWImg[1] = 190;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 12;
				mImg = new mImage[3];
				mImg[0] = mImage.createImage("/bg/b132.png");
				mImg[1] = mImage.createImage("/bg/b133.png");
				mImg[2] = mImage.createImage("/bg/b135.png");
				mImgCloud = null;
				yplusCloud = -20;
				break;
			case 33:
				hLimit = 80;
				color = 12368081;
				colorMini = 12368081;
				if (GameCanvas.loadmap.idMap >= 167 && GameCanvas.loadmap.idMap <= 176)
				{
					color = 791323;
					colorMini = 791323;
				}
				mHImg = new int[2];
				mHImg[0] = 20;
				mHImg[1] = 10;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 82;
				mWImg[1] = 45;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 1;
				mSpeedImg[1] = 1;
				mImg = new mImage[2];
				mImg[0] = mImage.createImage("/bg/b140.png");
				mImg[1] = mImage.createImage("/bg/b141.png");
				mImgCloud = null;
				yplusCloud = -20;
				break;
			case 63:
			{
				hLimit = 80;
				int num4 = 0;
				switch (GameCanvas.loadmap.idMap)
				{
				case 195:
					num4 = 12221098;
					break;
				case 196:
					num4 = 12693148;
					break;
				case 197:
					num4 = 15200189;
					break;
				case 198:
					num4 = 4277093;
					break;
				}
				color = num4;
				colorMini = num4;
				if (GameCanvas.loadmap.idMap == 197)
				{
					mHImg = new int[2];
					mHImg[0] = 95;
					mHImg[1] = 140;
					mWImg = new int[mHImg.Length];
					mWImg[0] = 99;
					mWImg[1] = 99;
					mSpeedImg = new int[mHImg.Length];
					mSpeedImg[0] = 1;
					mSpeedImg[1] = 1;
					mImg = new mImage[mHImg.Length];
					mImg[0] = mImage.createImage("/bg/b" + (GameCanvas.loadmap.idMap - 2) + ".png");
					mImg[1] = mImage.createImage("/bg/b" + (GameCanvas.loadmap.idMap - 2) + ".png");
				}
				else
				{
					mHImg = new int[1];
					mHImg[0] = 60;
					mWImg = new int[mHImg.Length];
					mWImg[0] = 25;
					mSpeedImg = new int[mHImg.Length];
					mSpeedImg[0] = 1;
					mImg = new mImage[1];
					mImg[0] = mImage.createImage("/bg/b" + (GameCanvas.loadmap.idMap - 2) + ".png");
				}
				if (GameCanvas.loadmap.idMap == 198)
				{
					mWImg[0] = 124;
				}
				mImgCloud = null;
				yplusCloud = -20;
				break;
			}
			case 36:
				hLimit = 80;
				imgSky = mImage.createImage("/bg/sky9.png");
				color = 9953276;
				hSky = 75;
				colorMini = 9953276;
				mHImg = new int[2];
				mHImg[0] = 70;
				mHImg[1] = -47;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 168;
				mWImg[1] = 72;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mSpeedImg[1] = 3;
				mImg = new mImage[mHImg.Length];
				mImg[0] = mImage.createImage("/bg/b151.png");
				mImg[1] = mImage.createImage("/bg/b150.png");
				yplusCloud = -5;
				break;
			case 37:
			{
				hLimit = 80;
				imgSky = null;
				color = 15923962;
				hSky = 75;
				colorMini = 9953276;
				mHImg = new int[1];
				mHImg[0] = 70;
				mWImg = new int[1];
				mWImg[0] = 168;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mImg = new mImage[7];
				for (int n = 0; n < mImg.Length; n++)
				{
					if (n == 6)
					{
						mImg[6] = mImage.createImage("/bg/b151.png");
						continue;
					}
					mImg[n] = mImage.createImage("/bg/b16" + n + ".png");
					mImg[n].setWH();
				}
				yplusCloud = -5;
				break;
			}
			case 38:
			{
				hLimit = 80;
				imgSky = null;
				color = 15923962;
				hSky = 75;
				colorMini = 9953276;
				mHImg = new int[1];
				mHImg[0] = 0;
				mWImg = new int[1];
				mWImg[0] = 168;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 2;
				mImg = new mImage[6];
				for (int m = 0; m < mImg.Length; m++)
				{
					mImg[m] = mImage.createImage("/bg/b16" + m + ".png");
					mImg[m].setWH();
				}
				yplusCloud = -5;
				break;
			}
			case 39:
			{
				hLimit = 80;
				imgSky = null;
				color = 15923962;
				hSky = 75;
				colorMini = 9953276;
				mHImg = new int[1];
				mHImg[0] = 0;
				mWImg = new int[1];
				mWImg[0] = 168;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mImg = new mImage[10];
				for (int k = 0; k < mImg.Length; k++)
				{
					switch (k)
					{
					case 6:
						mImg[k] = mImage.createImage("/bg/b170.png");
						mImg[k].setWH();
						break;
					case 7:
						mImg[k] = mImage.createImage("/bg/b171.png");
						mImg[k].setWH();
						break;
					case 8:
						mImg[k] = mImage.createImage("/bg/b151.png");
						mImg[k].setWH();
						break;
					case 9:
						mImg[k] = mImage.createImage("/bg/b172.png");
						mImg[k].setWH();
						break;
					default:
						mImg[k] = mImage.createImage("/bg/b16" + k + ".png");
						mImg[k].setWH();
						break;
					}
				}
				yplusCloud = -5;
				break;
			}
			case 46:
			{
				hLimit = 80;
				imgSky = null;
				color = 15923962;
				hSky = 75;
				colorMini = 9953276;
				mHImg = new int[1];
				mHImg[0] = 0;
				mWImg = new int[1];
				mWImg[0] = 168;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 4;
				mImg = new mImage[2];
				for (int i = 0; i < mImg.Length; i++)
				{
					mImg[i] = mImage.createImage("/bg/b16" + (3 + i) + ".png");
					mImg[i].setWH();
				}
				break;
			}
			default:
			{
				color = 9356287;
				colorMini = 4367095;
				mHImg = new int[1];
				mHImg[0] = 96;
				mWImg = new int[mHImg.Length];
				mWImg[0] = 96;
				mSpeedImg = new int[mHImg.Length];
				mSpeedImg[0] = 8;
				mImg = new mImage[mHImg.Length];
				mImg[0] = mImage.createImage("/bg/login1.png");
				mHBegin = new int[mHImg.Length];
				int num2 = 0;
				num2 += mHImg[0];
				mHBegin[0] = hBack - num2;
				return;
			}
			}
		}
		else if (lastMap == 28 || lastMap == 29 || lastMap == 30)
		{
			if (typeMap != 28 && typeMap != 29 && typeMap != 30)
			{
				mImgCloud = null;
			}
		}
		else if (typeMap == 28 || typeMap == 29 || typeMap == 30)
		{
			mImgCloud = null;
		}
		valueMyRandom = hBack - hLimit + yplusCloud;
		int num27 = 0;
		mHBegin = new int[mHImg.Length];
		for (int num28 = 0; num28 < mHImg.Length; num28++)
		{
			num27 += mHImg[num28];
			mHBegin[num28] = hBack - num27;
		}
		int num29 = GameCanvas.loadmap.maxWMap / 250;
		if ((!GameCanvas.lowGraphic || typeMap == 33) && typeMap != 35 && typeMap != 43 && typeMap != 62)
		{
			PosCloud = new Point[3][];
			for (int num30 = 0; num30 < PosCloud.Length; num30++)
			{
				PosCloud[num30] = new Point[num29 + 1];
				for (int num31 = 0; num31 < PosCloud[num30].Length; num31++)
				{
					PosCloud[num30][num31] = new Point();
					PosCloud[num30][num31].x = CRes.random(GameCanvas.loadmap.maxWMap) * 100;
					PosCloud[num30][num31].y = valueMyRandom - 60 + num30 * 30 + ((num30 == 1) ? 5 : 0) + CRes.random_Am_0(10);
					PosCloud[num30][num31].vx = -60 / (num30 * 4 + 1);
					PosCloud[num30][num31].frame = num30 % 3;
				}
			}
		}
		lastMap = typeMap;
	}

	public bool checkDonotLow()
	{
		if (typeMap == 8 || typeMap == 51 || typeMap == 17 || typeMap == 23 || typeMap == 22)
		{
			return false;
		}
		return true;
	}

	public void paint(mGraphics g)
	{
		if (MainScreen.cameraMain.yCam < 0)
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
		}
		if ((GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg) && checkDonotLow())
		{
			if (MainScreen.cameraMain.yCam + yeff <= hBack)
			{
				g.setColor(color);
				g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			}
			if (LoadMap.idTile != 11 && LoadMap.idTile != 14)
			{
				for (int i = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; i < MotherCanvas.w + mWImg[0]; i += mWImg[0])
				{
					g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - i, mHBegin[0] + yeff, 0);
				}
				g.setColor(colorMini);
				g.fillRect(MainScreen.cameraMain.xCam, mHBegin[0] + yeff + 96, MotherCanvas.w, 40);
			}
			return;
		}
		if (typeMap != 20)
		{
			if (typeMap != 8 && typeMap != 40)
			{
				if (MainScreen.cameraMain.yCam - yeff < mHBegin[mHBegin.Length - 1])
				{
					g.setColor(color);
					g.fillRect(MainScreen.cameraMain.xCam - GameScreen.dx, MainScreen.cameraMain.yCam - GameScreen.dy, MotherCanvas.w + GameScreen.dx, mHBegin[mHBegin.Length - 1] - MainScreen.cameraMain.yCam + yeff + GameScreen.dy + 10);
				}
			}
			else if (MainScreen.cameraMain.yCam - yeff < mHBegin[mHBegin.Length - 1])
			{
				g.setColor(color);
				g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam - hEff, MotherCanvas.w, mHBegin[mHBegin.Length - 1] - MainScreen.cameraMain.yCam + hEff + yeff);
			}
		}
		if (typeMap != 20 && typeMap != 21 && typeMap != 22)
		{
			paintSky(g);
		}
		switch (typeMap)
		{
		case 0:
		{
			for (int num66 = mWImg.Length - 1; num66 >= 0; num66--)
			{
				for (int num67 = MainScreen.cameraMain.xCam / mSpeedImg[num66] % mWImg[num66]; num67 < MotherCanvas.w + mWImg[num66]; num67 += mWImg[num66])
				{
					g.drawImage(mImg[num66], MainScreen.cameraMain.xCam + MotherCanvas.w - num67, mHBegin[num66] + yeff, 0);
					if (num66 == 0)
					{
						fraChong.drawFrame(GameCanvas.gameTick / 3 % fraChong.nFrame, MainScreen.cameraMain.xCam + MotherCanvas.w - num67 + 187, mHBegin[num66] + 7 + yeff, 0, 3, g);
					}
					if (num66 == 1)
					{
						fraChongNho.drawFrame(GameCanvas.gameTickChia4 % fraChong.nFrame, MainScreen.cameraMain.xCam + MotherCanvas.w - num67 + 243, mHBegin[num66] + 4 + yeff, 0, 3, g);
					}
				}
			}
			paintCloud(g);
			break;
		}
		case 1:
		case 3:
		case 5:
		case 6:
		case 10:
		case 35:
		{
			for (int num77 = mWImg.Length - 1; num77 >= 0; num77--)
			{
				for (int num78 = MainScreen.cameraMain.xCam / mSpeedImg[num77] % mWImg[num77]; num78 < MotherCanvas.w + mWImg[num77]; num78 += mWImg[num77])
				{
					g.drawImage(mImg[num77], MainScreen.cameraMain.xCam + MotherCanvas.w - num78, mHBegin[num77] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 11:
		case 27:
		{
			for (int num41 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num41 < MotherCanvas.w + mWImg[0]; num41 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num41, mHBegin[0] + yeff, 0);
				if (typeMap == 27)
				{
					g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num41, mHBegin[0] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 16:
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			paintCloud(g);
			break;
		case 2:
		case 4:
		case 45:
		{
			paintCloud(g);
			for (int num53 = MainScreen.cameraMain.xCam / mSpeedImg[1] % mWImg[1]; num53 < MotherCanvas.w + mWImg[1]; num53 += mWImg[1])
			{
				g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num53, mHBegin[1] + yeff, 0);
			}
			if (typeMap != 45)
			{
				int num = MainScreen.cameraMain.xCam + 120 - MainScreen.cameraMain.xCam / 16;
				g.drawImage(mImg[2], num, hBack - 112 + yeff, 0);
			}
			for (int num54 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num54 < MotherCanvas.w + mWImg[0]; num54 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num54, mHBegin[0] + yeff, 0);
			}
			break;
		}
		case 19:
		case 23:
		{
			if (!GameCanvas.isLowGraOrWP_PvP() && !GameCanvas.isOffBg)
			{
				paintCloud(g);
			}
			for (int num27 = MainScreen.cameraMain.xCam / mSpeedImg[1] % mWImg[1]; num27 < MotherCanvas.w + mWImg[1]; num27 += mWImg[1])
			{
				g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num27, mHBegin[1] + yeff, 0);
			}
			if (!GameCanvas.isLowGraOrWP_PvP() && !GameCanvas.isOffBg)
			{
				int num = MainScreen.cameraMain.xCam + 60 - MainScreen.cameraMain.xCam / 12;
				g.drawImage(mImg[3], num + 120, hBack - 70 + yeff, 0);
				g.drawImage(mImg[2], num, hBack - 90 + yeff, 0);
			}
			for (int num28 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num28 < MotherCanvas.w + mWImg[0]; num28 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num28, mHBegin[0] + yeff, 0);
			}
			break;
		}
		case 7:
		case 26:
		case 47:
		{
			paintCloud(g);
			for (int num57 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num57 < MotherCanvas.w + mWImg[0]; num57 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num57, mHBegin[0] - 18 + yeff, 0);
				if (typeMap == 26)
				{
					g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num57, mHBegin[0] - 18 + yeff, 0);
				}
			}
			break;
		}
		case 8:
		{
			for (int num74 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num74 < MotherCanvas.w + mWImg[0]; num74 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num74, mHBegin[0] - hEff + yeff, 0);
			}
			paintCloud(g);
			break;
		}
		case 40:
		{
			for (int num48 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num48 < MotherCanvas.w + mWImg[0]; num48 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num48, mHBegin[0] - hEff + yeff, 0);
			}
			paintCloud(g);
			for (int num49 = 0; num49 < PosObj.Length && PosObj[num49] != null; num49++)
			{
				if (12 + PosObj[num49][0] > MainScreen.cameraMain.xCam - 17 && 12 + PosObj[num49][0] < MainScreen.cameraMain.xCam + MotherCanvas.w + 17)
				{
					g.drawRegion(mImg[2], PosObj[num49][2] * 17, PosObj[num49][3] * 30, 17, 30, PosObj[num49][5], PosObj[num49][0], mHBegin[0] + 73 + PosObj[num49][1], 33);
				}
			}
			break;
		}
		case 9:
		{
			paintCloud(g);
			for (int num42 = mWImg.Length - 1; num42 >= 0; num42--)
			{
				for (int num43 = MainScreen.cameraMain.xCam / mSpeedImg[num42] % mWImg[num42]; num43 < MotherCanvas.w + mWImg[num42]; num43 += mWImg[num42])
				{
					if (num42 == 1)
					{
						g.drawRegion(mImg[num42], 0, 24 * (GameCanvas.gameTick / 8 % 2), 24, 24, 0, MainScreen.cameraMain.xCam + MotherCanvas.w - num43, mHBegin[num42] + yeff, 0);
					}
					else
					{
						g.drawImage(mImg[num42], MainScreen.cameraMain.xCam + MotherCanvas.w - num43, mHBegin[num42] + yeff, 0);
					}
				}
			}
			break;
		}
		case 12:
		case 13:
		case 14:
		case 24:
		case 25:
		{
			for (int num3 = mHImg.Length - 1; num3 >= 0; num3--)
			{
				for (int l = MainScreen.cameraMain.xCam / mSpeedImg[num3] % mWImg[num3]; l < MotherCanvas.w + mWImg[num3]; l += mWImg[num3])
				{
					g.drawImage(mImg[num3], MainScreen.cameraMain.xCam + MotherCanvas.w - l, mHBegin[num3] + yeff, 0);
				}
			}
			paintCloud(g);
			if (PosObj != null)
			{
				int num = MainScreen.cameraMain.xCam - MainScreen.cameraMain.xCam / mSpeedImg[mSpeedImg.Length - 1];
				for (int m = 0; m < PosObj.Length; m++)
				{
					g.drawImage(mImg[PosObj[m][0]], num + PosObj[m][1], hBack - 48 + yeff + PosObj[m][2], 33);
				}
			}
			if (typeMap == 13 && GameCanvas.loadmap.idMap == 191)
			{
				fraWater7.drawFrame(GameCanvas.gameTick / 3 % fraWater7.nFrame, MainScreen.cameraMain.xCam + MotherCanvas.w / 2 - MainScreen.cameraMain.xCam / 4, 45 + yeff, 0, 3, g);
			}
			break;
		}
		case 15:
		case 41:
		case 60:
		case 61:
		{
			if (typeMap == 15 || typeMap == 60)
			{
				for (int num68 = MainScreen.cameraMain.xCam / mSpeedImg[1] % mWImg[1]; num68 < MotherCanvas.w + mWImg[1]; num68 += mWImg[1])
				{
					g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num68, mHBegin[1] + yeff, 0);
				}
			}
			for (int num69 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num69 < MotherCanvas.w + mWImg[0] + GameCanvas.loadmap.maxWMap % 125; num69 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num69 + GameCanvas.loadmap.maxWMap % 125, mHBegin[0] + yeff, 0);
			}
			if (typeMap != 60 && typeMap != 61)
			{
				for (int num70 = 0; num70 < mVecKhangia.size(); num70++)
				{
					Point_Focus point_Focus = (Point_Focus)mVecKhangia.elementAt(num70);
					if (point_Focus.x > MainScreen.cameraMain.xCam - 25 && point_Focus.x < MainScreen.cameraMain.xCam + MotherCanvas.w + 25)
					{
						g.drawRegion(mImg[2], 0, (point_Focus.dis + point_Focus.frame) * 15, 25, 15, 0, point_Focus.x, point_Focus.y, 33);
					}
				}
			}
			paintCloud(g);
			break;
		}
		case 51:
		case 53:
		case 55:
		{
			for (int num21 = mWImg.Length - 1; num21 >= 0; num21--)
			{
				for (int num22 = MainScreen.cameraMain.xCam / mSpeedImg[num21] % mWImg[num21]; num22 < MotherCanvas.w + mWImg[num21]; num22 += mWImg[num21])
				{
					g.drawImage(mImg[num21], MainScreen.cameraMain.xCam + MotherCanvas.w - num22, mHBegin[num21] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 17:
		{
			if (MainScreen.cameraMain.xCam < 220)
			{
				g.drawImage(mImg[5], 0, mHBegin[0] - 85, 0);
			}
			if (laboon != null)
			{
				if (laboon.f >= 20 && laboon.f <= 27 && fraImgNew != null)
				{
					fraImgNew.drawFrame((laboon.f - 20) / 4 % 2, laboon.x + 4, mHBegin[0] + laboon.y + 12, 0, 3, g);
				}
				g.drawImage(mImg[4], laboon.x, mHBegin[0] + laboon.y, 0);
				if (laboon.f < 120)
				{
					g.drawRegion(mImg[6], 0, laboon.f / 4 % 2 * 12, 78, 12, 0, laboon.x, mHBegin[0] - 2, 0);
				}
			}
			if (MainScreen.cameraMain.xCam < 96)
			{
				g.drawImage(mImg[1], 0, mHBegin[0] - 30, 0);
			}
			for (int num46 = 0; num46 < GameCanvas.loadmap.maxWMap - 180; num46 += 96)
			{
				if (180 + num46 >= MainScreen.cameraMain.xCam - 96 && 180 + num46 <= MainScreen.cameraMain.xCam + MotherCanvas.w)
				{
					if (num46 == 0)
					{
						g.drawImage(mImg[3], 180 + num46, mHBegin[0] - 30, 0);
					}
					else
					{
						g.drawImage(mImg[2], 180 + num46, mHBegin[0] - 30, 0);
					}
				}
			}
			for (int num47 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num47 < MotherCanvas.w + mWImg[0]; num47 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num47, mHBegin[0], 0);
			}
			paintCloud(g);
			break;
		}
		case 18:
		{
			int num = MainScreen.cameraMain.xCam / 8;
			for (int num10 = 0; num10 < posXuongRong2.Length; num10++)
			{
				if (posXuongRong2[num10].x + num >= MainScreen.cameraMain.xCam - 145 && posXuongRong2[num10].x + num <= MainScreen.cameraMain.xCam + MotherCanvas.w && !posXuongRong2[num10].isSmall)
				{
					g.drawImage(mImg[4], posXuongRong2[num10].x + num, mHBegin[0] + posXuongRong2[num10].y - 100 + 45 + yeff, 0);
				}
			}
			num = MainScreen.cameraMain.xCam / 6;
			for (int num11 = 0; num11 < posXuongRong.Length; num11++)
			{
				if (posXuongRong[num11].x + num >= MainScreen.cameraMain.xCam - 145 && posXuongRong[num11].x + num <= MainScreen.cameraMain.xCam + MotherCanvas.w && !posXuongRong[num11].isSmall)
				{
					g.drawImage(mImg[2 + posXuongRong[num11].frame], posXuongRong[num11].x + num, mHBegin[0] + posXuongRong[num11].y - 100 + 35 + yeff, 0);
				}
			}
			for (int num12 = 1; num12 >= 0; num12--)
			{
				for (int num13 = MainScreen.cameraMain.xCam / mSpeedImg[num12] % mWImg[num12]; num13 < MotherCanvas.w + mWImg[num12]; num13 += mWImg[num12])
				{
					g.drawImage(mImg[num12], MainScreen.cameraMain.xCam + MotherCanvas.w - num13, mHBegin[num12] + yeff, 0);
				}
			}
			num = MainScreen.cameraMain.xCam + 100 - MainScreen.cameraMain.xCam / 16;
			g.drawImage(mImg[5], num + MotherCanvas.w / 3, mHBegin[0] - 120, 0);
			paintCloudNight(g);
			break;
		}
		case 20:
		case 22:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			paintSky(g);
			g.setColor(colorMini);
			g.fillRect(MainScreen.cameraMain.xCam, mHBegin[0] + 30, MotherCanvas.w, 100);
			if (!GameCanvas.isLowGraOrWP_PvP() && !GameCanvas.isOffBg)
			{
				int num = MainScreen.cameraMain.xCam + 80 - MainScreen.cameraMain.xCam / 24;
				for (int num23 = 0; num23 < PosObj.Length; num23++)
				{
					g.drawImage(mImg[PosObj[num23][0]], num + PosObj[num23][1], hBack - PosObj[num23][2] - 100 + yeff, 0);
				}
				for (int num24 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num24 < MotherCanvas.w + mWImg[0]; num24 += mWImg[0])
				{
					g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num24, mHBegin[0] + yeff, 0);
				}
				num = MainScreen.cameraMain.xCam - MainScreen.cameraMain.xCam / 12;
				for (int num25 = 0; num25 < PosObj2.Length; num25++)
				{
					g.drawImage(mImg[4], num + PosObj2[num25][1], hBack - PosObj2[num25][2] + yeff - 45, 0);
				}
			}
			for (int num26 = MainScreen.cameraMain.xCam % 124; num26 < MotherCanvas.w + 124; num26 += 124)
			{
				g.drawImage(imgSpec, MainScreen.cameraMain.xCam + MotherCanvas.w - num26, mHBegin[0] + 80, 0);
			}
			if (!GameCanvas.isLowGraOrWP_PvP() && !GameCanvas.isOffBg)
			{
				paintCloud(g);
			}
			break;
		}
		case 21:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			paintSky(g);
			g.setColor(colorMini);
			g.fillRect(MainScreen.cameraMain.xCam, mHBegin[1] + yeff + 4, MotherCanvas.w, 100);
			for (int num8 = MainScreen.cameraMain.xCam / mSpeedImg[1] % mWImg[1]; num8 < MotherCanvas.w + mWImg[1]; num8 += mWImg[1])
			{
				g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num8, mHBegin[1] + yeff, 0);
			}
			int num = MainScreen.cameraMain.xCam + 200 - MainScreen.cameraMain.xCam / 8;
			g.drawImage(mImg[0], num, hBack - 35 + yeff, 33);
			for (int num9 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num9 < MotherCanvas.w + mWImg[0]; num9 += mWImg[0])
			{
				g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num9, mHBegin[0] + yeff, 0);
			}
			paintCloud(g);
			break;
		}
		case 28:
		{
			for (int num73 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num73 < MotherCanvas.w + mWImg[0]; num73 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num73, mHBegin[0] + yeff, 0);
			}
			paintCloud(g);
			break;
		}
		case 31:
		{
			g.setColor(colorMini);
			g.fillRect(MainScreen.cameraMain.xCam, mHBegin[0] + 10, MotherCanvas.w, 100);
			for (int num58 = mWImg.Length - 1; num58 >= 0; num58--)
			{
				for (int num59 = MainScreen.cameraMain.xCam / mSpeedImg[num58] % mWImg[num58]; num59 < MotherCanvas.w + mWImg[num58]; num59 += mWImg[num58])
				{
					g.drawImage(mImg[num58], MainScreen.cameraMain.xCam + MotherCanvas.w - num59, mHBegin[num58] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 42:
		{
			g.setColor(colorMini);
			g.fillRect(MainScreen.cameraMain.xCam, mHBegin[0] + 10, MotherCanvas.w, 100);
			for (int num51 = mWImg.Length - 1; num51 >= 0; num51--)
			{
				for (int num52 = MainScreen.cameraMain.xCam / mSpeedImg[num51] % mWImg[num51]; num52 < MotherCanvas.w + mWImg[num51]; num52 += mWImg[num51])
				{
					g.drawImage(mImg[num51], MainScreen.cameraMain.xCam + MotherCanvas.w - num52, mHBegin[num51] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 29:
		{
			g.setColor(colorMini);
			g.fillRect(MainScreen.cameraMain.xCam, mHBegin[0] + 20, MotherCanvas.w, 100);
			for (int num44 = 1; num44 >= 0; num44--)
			{
				for (int num45 = MainScreen.cameraMain.xCam / mSpeedImg[num44] % mWImg[num44]; num45 < MotherCanvas.w + mWImg[num44]; num45 += mWImg[num44])
				{
					g.drawImage(mImg[num44], MainScreen.cameraMain.xCam + MotherCanvas.w - num45, mHBegin[num44] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 30:
		{
			g.setColor(colorMini);
			g.fillRect(MainScreen.cameraMain.xCam, mHBegin[0] + 10, MotherCanvas.w, 100);
			for (int num39 = 1; num39 >= 0; num39--)
			{
				for (int num40 = MainScreen.cameraMain.xCam / mSpeedImg[num39] % mWImg[num39]; num40 < MotherCanvas.w + mWImg[num39]; num40 += mWImg[num39])
				{
					g.drawImage(mImg[num39], MainScreen.cameraMain.xCam + MotherCanvas.w - num40, mHBegin[num39] + yeff, 0);
				}
			}
			int num = MainScreen.cameraMain.xCam + 160 - MainScreen.cameraMain.xCam / 4;
			g.drawImage(mImg[2], num, hBack - 75 + yeff, 0);
			g.drawRegion(mImg[2], 0, 0, 92, 125, 2, num + 92, hBack - 75 + yeff, 0);
			paintCloud(g);
			break;
		}
		case 32:
		{
			g.setColor(colorMini);
			g.fillRect(MainScreen.cameraMain.xCam, mHBegin[0] + 10, MotherCanvas.w, 100);
			for (int num14 = MainScreen.cameraMain.xCam / mSpeedImg[1] % mWImg[1]; num14 < MotherCanvas.w + mWImg[1]; num14 += mWImg[1])
			{
				g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num14, mHBegin[1] + yeff, 0);
			}
			int num = MainScreen.cameraMain.xCam + 160 - MainScreen.cameraMain.xCam / 8;
			g.drawImage(mImg[2], num, hBack - 85 + yeff, 0);
			g.drawRegion(mImg[2], 0, 0, 92, 125, 2, num + 92, hBack - 85 + yeff, 0);
			for (int num15 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num15 < MotherCanvas.w + mWImg[0]; num15 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num15, mHBegin[0] + yeff, 0);
			}
			paintCloud(g);
			break;
		}
		case 33:
		{
			if (GameCanvas.loadmap.idMap >= 167 && GameCanvas.loadmap.idMap <= 176)
			{
				break;
			}
			for (int num2 = 1; num2 >= 0; num2--)
			{
				for (int k = MainScreen.cameraMain.xCam / mSpeedImg[num2] % mWImg[num2]; k < MotherCanvas.w + mWImg[num2]; k += mWImg[num2])
				{
					g.drawImage(mImg[num2], MainScreen.cameraMain.xCam + MotherCanvas.w - k, mHBegin[num2] + yeff, 0);
				}
			}
			break;
		}
		case 63:
		{
			for (int num75 = mHImg.Length - 1; num75 >= 0; num75--)
			{
				for (int num76 = MainScreen.cameraMain.xCam / mSpeedImg[num75] % mWImg[num75]; num76 < MotherCanvas.w + mWImg[num75]; num76 += mWImg[num75])
				{
					g.drawImage(mImg[num75], MainScreen.cameraMain.xCam + MotherCanvas.w - num76, mHBegin[num75] + yeff, 0);
				}
			}
			break;
		}
		case 36:
		{
			for (int num71 = mWImg.Length - 1; num71 >= 0; num71--)
			{
				for (int num72 = MainScreen.cameraMain.xCam / mSpeedImg[num71] % mWImg[num71]; num72 < MotherCanvas.w + mWImg[num71]; num72 += mWImg[num71])
				{
					g.drawImage(mImg[num71], MainScreen.cameraMain.xCam + MotherCanvas.w - num72, mHBegin[num71] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 37:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			int num = MainScreen.cameraMain.xCam + 120 - MainScreen.cameraMain.xCam / 8;
			for (int num55 = 0; num55 < PosObj17.Length; num55++)
			{
				g.drawRegion(mImg[PosObj17[num55][0]], 0, 0, mImg[PosObj17[num55][0]].w, mImg[PosObj17[num55][0]].h, PosObj17[num55][1], num + PosObj17[num55][2], hBack + yeff + PosObj17[num55][3], 3);
			}
			for (int num56 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num56 < MotherCanvas.w + mWImg[0]; num56 += mWImg[0])
			{
				g.drawImage(mImg[6], MainScreen.cameraMain.xCam + MotherCanvas.w - num56, mHBegin[0] + yeff, 0);
			}
			paintCloud(g);
			break;
		}
		case 38:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			int num = MainScreen.cameraMain.xCam + 120 - MainScreen.cameraMain.xCam / 8;
			for (int num50 = 0; num50 < PosObj18.Length; num50++)
			{
				g.drawRegion(mImg[PosObj18[num50][0]], 0, 0, mImg[PosObj18[num50][0]].w, mImg[PosObj18[num50][0]].h, PosObj18[num50][1], num + PosObj18[num50][2], hBack + yeff + PosObj18[num50][3], 3);
			}
			paintCloud(g);
			break;
		}
		case 39:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			int num = MainScreen.cameraMain.xCam + 180 - MainScreen.cameraMain.xCam / 8;
			for (int num16 = 0; num16 < PosObj21.Length; num16++)
			{
				if (PosObj21[num16][3] == 0)
				{
					g.drawRegion(mImg[9], 0, PosObj21[num16][2] * 18, 18, 18, 0, num + PosObj21[num16][0], hBack + yeff + PosObj21[num16][1], 3);
				}
			}
			for (int num17 = 0; num17 <= 5; num17++)
			{
				if (num17 == 0)
				{
					g.drawRegion(mImg[6], 0, 0, mImg[6].w, mImg[6].h, 0, num, hBack + yeff - 23 - num17 * 47 + 30, 3);
				}
				else
				{
					g.drawRegion(mImg[7], 0, 0, mImg[7].w, mImg[7].h, 0, num, hBack + yeff - 23 - num17 * 47 + 30, 3);
				}
			}
			for (int num18 = 0; num18 < PosObj21.Length; num18++)
			{
				if (PosObj21[num18][3] == 1)
				{
					g.drawRegion(mImg[9], 0, PosObj21[num18][2] * 18, 18, 18, 0, num + PosObj21[num18][0], hBack + yeff + PosObj21[num18][1], 3);
				}
			}
			for (int num19 = 0; num19 < PosObj19.Length; num19++)
			{
				g.drawRegion(mImg[PosObj19[num19][0]], 0, 0, mImg[PosObj19[num19][0]].w, mImg[PosObj19[num19][0]].h, PosObj19[num19][1], num + PosObj19[num19][2], hBack + yeff + PosObj19[num19][3] + 30, 3);
			}
			for (int num20 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num20 < MotherCanvas.w + mWImg[0]; num20 += mWImg[0])
			{
				g.drawImage(mImg[8], MainScreen.cameraMain.xCam + MotherCanvas.w - num20, mHBegin[0] + yeff, 0);
			}
			paintCloud(g);
			break;
		}
		case 43:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			for (int num60 = MainScreen.cameraMain.xCam / mSpeedImg[2] % mWImg[2]; num60 < MotherCanvas.w + mWImg[2]; num60 += mWImg[2])
			{
				g.drawImage(mImg[2], MainScreen.cameraMain.xCam + MotherCanvas.w - num60, mHBegin[2] + yeff, 0);
			}
			int num = MainScreen.cameraMain.xCam - MainScreen.cameraMain.xCam / 12;
			for (int num61 = 0; num61 < PosObj24.Length; num61++)
			{
				if (num + PosObj24[num61][0] >= MainScreen.cameraMain.xCam - 120 && num + PosObj24[num61][0] <= MainScreen.cameraMain.xCam + MotherCanvas.w + 120)
				{
					g.drawImage(mImg[PosObj24[num61][2]], num + PosObj24[num61][0], mHBegin[2] + yeff + 40 + PosObj24[num61][1], 33);
				}
			}
			for (int num62 = MainScreen.cameraMain.xCam / mSpeedImg[1] % mWImg[1]; num62 < MotherCanvas.w + mWImg[1]; num62 += mWImg[1])
			{
				g.drawImage(mImg[1], MainScreen.cameraMain.xCam + MotherCanvas.w - num62, mHBegin[1] + yeff, 0);
			}
			num = MainScreen.cameraMain.xCam - MainScreen.cameraMain.xCam / 3;
			for (int num63 = 0; num63 < PosObj23.Length; num63++)
			{
				if (num + PosObj23[num63][0] >= MainScreen.cameraMain.xCam - 120 && num + PosObj23[num63][0] <= MainScreen.cameraMain.xCam + MotherCanvas.w + 120)
				{
					g.drawImage(mImg[PosObj23[num63][2]], num + PosObj23[num63][0], mHBegin[1] + yeff + 40 + PosObj23[num63][1], 33);
				}
			}
			for (int num64 = MainScreen.cameraMain.xCam / mSpeedImg[0] % mWImg[0]; num64 < MotherCanvas.w + mWImg[0]; num64 += mWImg[0])
			{
				g.drawImage(mImg[0], MainScreen.cameraMain.xCam + MotherCanvas.w - num64, mHBegin[0] + yeff, 0);
			}
			num = MainScreen.cameraMain.xCam - MainScreen.cameraMain.xCam;
			for (int num65 = 0; num65 < PosObj22.Length; num65++)
			{
				if (num + PosObj22[num65][0] >= MainScreen.cameraMain.xCam - 120 && num + PosObj22[num65][0] <= MainScreen.cameraMain.xCam + MotherCanvas.w + 120)
				{
					g.drawImage(mImg[PosObj22[num65][2]], num + PosObj22[num65][0], mHBegin[0] + yeff + 40 + PosObj22[num65][1], 33);
				}
			}
			paintCloud(g);
			break;
		}
		case 62:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			xCamOffline -= MapGotoGod.vxHardCode;
			int xCam = MainScreen.cameraMain.xCam;
			int num29 = xCam + xCamOffline;
			for (int num30 = num29 / mSpeedImg[2] % mWImg[2]; num30 < xCam + MotherCanvas.w + mWImg[2]; num30 += mWImg[2])
			{
				if (num30 >= -mWImg[2])
				{
					g.drawImage(mImg[2], num30, mHBegin[2] + yeff, 0);
				}
			}
			int num31 = num29 / 12;
			while (num31 < xCam + MotherCanvas.w)
			{
				for (int num32 = 0; num32 < PosObj24.Length; num32++)
				{
					num31 += PosObj24[num32][0];
					if (num31 >= -30)
					{
						g.drawImage(mImg[PosObj24[num32][2]], num31, mHBegin[2] + yeff + 40 + PosObj24[num32][1], 33);
					}
				}
			}
			for (int num33 = num29 / mSpeedImg[1] % mWImg[1]; num33 < xCam + MotherCanvas.w + mWImg[1]; num33 += mWImg[1])
			{
				if (num33 >= -mWImg[1])
				{
					g.drawImage(mImg[1], num33, mHBegin[1] + yeff, 0);
				}
			}
			int num34 = num29 / 3;
			while (num34 < xCam + MotherCanvas.w)
			{
				for (int num35 = 0; num35 < PosObj23.Length; num35++)
				{
					num34 += PosObj23[num35][0];
					if (num34 >= -30)
					{
						g.drawImage(mImg[PosObj23[num35][2]], num34, mHBegin[1] + yeff + 40 + PosObj23[num35][1], 33);
					}
				}
			}
			for (int num36 = num29 / mSpeedImg[0] % mWImg[0]; num36 < xCam + MotherCanvas.w + mWImg[0]; num36 += mWImg[0])
			{
				if (num36 >= -mWImg[0])
				{
					g.drawImage(mImg[0], num36, mHBegin[0] + yeff, 0);
				}
			}
			int num37 = num29;
			while (num37 < xCam + MotherCanvas.w)
			{
				for (int num38 = 0; num38 < PosObj22.Length; num38++)
				{
					num37 += PosObj22[num38][0];
					if (num37 >= -30)
					{
						g.drawImage(mImg[PosObj22[num38][2]], num37, mHBegin[0] + yeff + 40 + PosObj22[num38][1], 33);
					}
				}
			}
			paintCloud(g);
			break;
		}
		case 44:
		{
			int num = MainScreen.cameraMain.xCam + 180 - MainScreen.cameraMain.xCam / 12;
			for (int n = 0; n < PosObj21.Length; n++)
			{
				if (PosObj21[n][3] == 0)
				{
					g.drawRegion(mImg[5], 0, PosObj21[n][2] * 18, 18, 18, 0, num + PosObj21[n][0], hBack + yeff + PosObj21[n][1], 3);
				}
			}
			for (int num4 = 0; num4 <= 5; num4++)
			{
				if (num4 == 0)
				{
					g.drawRegion(mImg[3], 0, 0, mImg[3].w, mImg[3].h, 0, num, hBack + yeff - 23 - num4 * 47 + 30, 3);
				}
				else
				{
					g.drawRegion(mImg[4], 0, 0, mImg[4].w, mImg[4].h, 0, num, hBack + yeff - 23 - num4 * 47 + 30, 3);
				}
			}
			for (int num5 = 0; num5 < PosObj21.Length; num5++)
			{
				if (PosObj21[num5][3] == 1)
				{
					g.drawRegion(mImg[5], 0, PosObj21[num5][2] * 18, 18, 18, 0, num + PosObj21[num5][0], hBack + yeff + PosObj21[num5][1], 3);
				}
			}
			for (int num6 = mWImg.Length - 1; num6 >= 0; num6--)
			{
				for (int num7 = MainScreen.cameraMain.xCam / mSpeedImg[num6] % mWImg[num6]; num7 < MotherCanvas.w + mWImg[num6]; num7 += mWImg[num6])
				{
					g.drawImage(mImg[num6], MainScreen.cameraMain.xCam + MotherCanvas.w - num7, mHBegin[num6] + yeff, 0);
				}
			}
			paintCloud(g);
			break;
		}
		case 46:
		{
			g.setColor(color);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			int num = MainScreen.cameraMain.xCam + 180 - MainScreen.cameraMain.xCam / 4;
			for (int j = 0; j < PosObj25.Length; j++)
			{
				g.drawRegion(mImg[PosObj25[j][0]], 0, 0, mImg[PosObj25[j][0]].w, mImg[PosObj25[j][0]].h, PosObj25[j][1], num + PosObj25[j][2], hBack + yeff + PosObj25[j][3] + 30, 3);
			}
			paintCloud(g);
			break;
		}
		case 34:
		case 48:
		case 49:
		case 50:
		case 52:
		case 54:
		case 56:
		case 57:
		case 58:
		case 59:
			break;
		}
	}

	public void paintLast(mGraphics g)
	{
		if ((GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg) && checkDonotLow())
		{
			return;
		}
		switch (typeMap)
		{
		case 2:
		case 22:
		case 23:
		case 28:
		case 47:
		case 51:
		case 53:
		case 55:
			if (MainScreen.cameraMain.yCam > GameCanvas.loadmap.limitH - 25)
			{
				g.setColor(colorMini);
				g.fillRect(MainScreen.cameraMain.xCam, GameCanvas.loadmap.maxHMap - (6 + hEff), MotherCanvas.w, 6 + hEff);
				paintSeaLast(g, mImg[mImg.Length - 1], 64, 12, 18);
			}
			break;
		case 45:
			if (MainScreen.cameraMain.yCam > GameCanvas.loadmap.limitH - 25)
			{
				g.setColor(colorMini);
				g.fillRect(MainScreen.cameraMain.xCam, GameCanvas.loadmap.maxHMap - (10 + hEff), MotherCanvas.w, 10 + hEff);
				paintSeaLast(g, mImg[mImg.Length - 1], 64, 12, 22);
			}
			break;
		case 8:
			if (MainScreen.cameraMain.yCam > GameCanvas.loadmap.limitH - 40)
			{
				g.setColor(colorMini);
				g.fillRect(MainScreen.cameraMain.xCam, GameCanvas.loadmap.maxHMap - (20 + hEff), MotherCanvas.w, 20 + hEff);
				for (int l = MainScreen.cameraMain.xCam % 64; l < MotherCanvas.w + 64; l += 64)
				{
					g.drawRegion(mImg[3], 0, (l / 64 + GameCanvas.gameTickChia4) % 4 * 12, 64, 12, 0, MainScreen.cameraMain.xCam + MotherCanvas.w - l, GameCanvas.loadmap.maxHMap - 28 - hEff, 0);
				}
				for (int m = MainScreen.cameraMain.xCam % 64; m < MotherCanvas.w + 64 + 64; m += 64)
				{
					g.drawRegion(mImg[3], 0, (m / 64 + GameCanvas.gameTickChia4 + 2) % 4 * 12, 64, 12, 0, MainScreen.cameraMain.xCam + 12 + MotherCanvas.w - m, GameCanvas.loadmap.maxHMap - 12 - hEff, 0);
				}
			}
			break;
		case 40:
			if (MainScreen.cameraMain.yCam > GameCanvas.loadmap.limitH - 40)
			{
				g.setColor(colorMini);
				g.fillRect(MainScreen.cameraMain.xCam, GameCanvas.loadmap.maxHMap - (20 + hEff), MotherCanvas.w, 20 + hEff);
				for (int j = MainScreen.cameraMain.xCam % 64; j < MotherCanvas.w + 64; j += 64)
				{
					g.drawRegion(mImg[1], 0, (j / 64 + GameCanvas.gameTick / 4) % 4 * 12, 64, 12, 0, MainScreen.cameraMain.xCam + MotherCanvas.w - j, GameCanvas.loadmap.maxHMap - 28 - hEff, 0);
				}
				for (int k = MainScreen.cameraMain.xCam % 64; k < MotherCanvas.w + 64 + 64; k += 64)
				{
					g.drawRegion(mImg[1], 0, (k / 64 + GameCanvas.gameTick / 4 + 2) % 4 * 12, 64, 12, 0, MainScreen.cameraMain.xCam + 12 + MotherCanvas.w - k, GameCanvas.loadmap.maxHMap - 12 - hEff, 0);
				}
			}
			break;
		case 42:
		{
			for (int i = 0; i < PosObj.Length && PosObj[i] != null; i++)
			{
				if (12 + PosObj[i][0] > MainScreen.cameraMain.xCam - 17 && 12 + PosObj[i][0] < MainScreen.cameraMain.xCam + MotherCanvas.w + 17)
				{
					g.drawRegion(mImg[2], PosObj[i][2] * 17, PosObj[i][3] * 30, 17, 30, PosObj[i][5], 12 + PosObj[i][0], mHBegin[0] - 30 + 73 + 90 + PosObj[i][1], 33);
				}
			}
			break;
		}
		}
	}

	public void paintSeaLast(mGraphics g, mImage img, int w, int h, int hbegin)
	{
		for (int i = MainScreen.cameraMain.xCam % w; i < MotherCanvas.w + w; i += w)
		{
			g.drawRegion(img, 0, (i / w + GameCanvas.gameTickChia4) % 4 * h, w, h, 0, MainScreen.cameraMain.xCam + MotherCanvas.w - i, GameCanvas.loadmap.maxHMap - hbegin - hEff, 0);
		}
		for (int j = MainScreen.cameraMain.xCam % w; j < MotherCanvas.w + w + w; j += w)
		{
			g.drawRegion(img, 0, (j / w + GameCanvas.gameTickChia4 + 2) % 4 * h, w, h, 0, MainScreen.cameraMain.xCam + 12 + MotherCanvas.w - j, GameCanvas.loadmap.maxHMap - (hbegin - 10) - hEff, 0);
		}
	}

	public void paintSky(mGraphics g)
	{
		if (imgSky == null)
		{
			return;
		}
		for (int i = 0; i < MotherCanvas.w; i += 20)
		{
			g.drawImage(imgSky, MainScreen.cameraMain.xCam + i, mHBegin[0] - hSky, 0);
		}
		if (tickRanThunder <= 6 && tickRanThunder != 4)
		{
			g.setColor(16251387);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
		}
		for (int j = 0; j < mVecThunder.size(); j++)
		{
			Point point = (Point)mVecThunder.elementAt(j);
			if (point.f > point.fRe)
			{
				int num = 20 + (point.f - point.fRe) / 2 * 27;
				if (num > 101)
				{
					num = 101;
				}
				g.drawRegion(imgThunder, 0, 0, 41, num, point.dis, point.x, mHBegin[0] - hSky - 20 + point.y, 0);
			}
		}
	}

	public void update()
	{
		for (int i = 0; i < mVecThunder.size(); i++)
		{
			Point point = (Point)mVecThunder.elementAt(i);
			point.f++;
			if (point.f == point.fRe - 1)
			{
				point.x += MainScreen.cameraMain.xCam;
			}
			if (point.f > point.fRe + 10)
			{
				point.f = 0;
				point.x = CRes.random(0, MotherCanvas.w);
				point.y = -CRes.random(40);
				point.dis = CRes.random(2) * 2;
				point.fRe = CRes.random(50, 150);
				if (CRes.random(4) == 0)
				{
					mSound.playSound(53, mSound.volumeSound);
				}
			}
		}
		if (mVecThunder.size() > 0)
		{
			tickRanThunder++;
			if (tickRanThunder > 50 && CRes.random(60) == 0)
			{
				tickRanThunder = 0;
			}
		}
		switch (typeMap)
		{
		case 2:
		case 22:
		case 23:
		case 45:
		case 47:
		case 51:
		case 53:
		case 55:
			if (!isEff)
			{
				if (hEff < 6 && CRes.random(15) == 0)
				{
					hEff++;
				}
				if (CRes.random(200) == 0)
				{
					isEff = true;
				}
			}
			else
			{
				if (hEff > 0 && CRes.random(15) == 0)
				{
					hEff--;
				}
				if (CRes.random(200) == 0)
				{
					isEff = false;
				}
			}
			break;
		case 8:
		case 40:
		{
			if (typeMap == 40 && (GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg))
			{
				return;
			}
			if (!isEff)
			{
				if (hEff < 12 && GameCanvas.gameTick % valueTick == 0)
				{
					hEff++;
				}
				if (GameCanvas.gameTick % valueEndTurn == 0)
				{
					isEff = true;
					valueTick = CRes.random(5, 10);
					valueEndTurn = CRes.random(12, 21) * 10;
				}
			}
			else
			{
				if (hEff > 0 && GameCanvas.gameTick % valueTick == 0)
				{
					hEff--;
				}
				if (GameCanvas.gameTick % valueEndTurn == 0)
				{
					isEff = false;
					valueTick = CRes.random(5, 10);
					valueEndTurn = CRes.random(12, 21) * 10;
				}
			}
			if (typeMap != 40)
			{
				break;
			}
			for (int l = 0; l < PosObj.Length && PosObj[l] != null; l++)
			{
				if (PosObj[l][3] == 0)
				{
					if (CRes.random(32) == 0)
					{
						PosObj[l][3] = 1;
						PosObj[l][4] = 0;
					}
					continue;
				}
				PosObj[l][4] = (short)(PosObj[l][4] + 1);
				if (PosObj[l][4] == 4)
				{
					PosObj[l][3] = 2;
				}
				else if (PosObj[l][4] == 8)
				{
					PosObj[l][3] = 0;
					if (PosObj[l][0] < GameScreen.player.x)
					{
						PosObj[l][5] = 2;
					}
					else
					{
						PosObj[l][5] = 0;
					}
				}
			}
			break;
		}
		case 15:
		case 41:
		{
			for (int k = 0; k < mVecKhangia.size(); k++)
			{
				Point_Focus point_Focus = (Point_Focus)mVecKhangia.elementAt(k);
				if (point_Focus.x <= MainScreen.cameraMain.xCam - 25 || point_Focus.x >= MainScreen.cameraMain.xCam + MotherCanvas.w + 25)
				{
					continue;
				}
				point_Focus.f++;
				if (point_Focus.frame == 1)
				{
					if (point_Focus.f > 15)
					{
						point_Focus.frame = 0;
						point_Focus.f = 0;
					}
				}
				else
				{
					if (point_Focus.frame != 0)
					{
						continue;
					}
					if (isEffKhangia)
					{
						if (CRes.random(5) != 0)
						{
							point_Focus.f = 0;
							point_Focus.frame = 1;
						}
					}
					else if (CRes.random(30) == 0)
					{
						point_Focus.f = 0;
						point_Focus.frame = 1;
					}
				}
			}
			if (!isEffKhangia)
			{
				if (CRes.random(100) == 0)
				{
					isEffKhangia = true;
				}
			}
			else if (CRes.random(2) == 0)
			{
				isEffKhangia = false;
			}
			break;
		}
		case 17:
			if (laboon != null)
			{
				laboon.f++;
				laboon.x += laboon.vx;
				laboon.y += laboon.vy;
				if (laboon.f == 10)
				{
					laboon.vx = -6;
					laboon.vy = -4;
				}
				if (laboon.f == 20)
				{
					laboon.vx = 0;
					laboon.vy = 0;
					LoadMap.timeVibrateScreen = 8;
				}
				if (laboon.f == 100)
				{
					laboon.vx = 5;
					laboon.vy = 3;
				}
				if (laboon.f == 116)
				{
					laboon.vx = 0;
					laboon.vy = 0;
				}
				if (laboon.f > 160 && CRes.random(40) == 0)
				{
					laboon.x = 140;
					laboon.y = 0;
					laboon.f = 0;
				}
			}
			break;
		case 42:
		{
			if (GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg)
			{
				return;
			}
			for (int j = 0; j < PosObj.Length && PosObj[j] != null; j++)
			{
				if (PosObj[j][3] == 0)
				{
					if (CRes.random(32) == 0)
					{
						PosObj[j][3] = 1;
						PosObj[j][4] = 0;
					}
					continue;
				}
				PosObj[j][4] = (short)(PosObj[j][4] + 1);
				if (PosObj[j][4] == 4)
				{
					PosObj[j][3] = 2;
				}
				else if (PosObj[j][4] == 8)
				{
					PosObj[j][3] = 0;
					if (PosObj[j][0] < GameScreen.player.x)
					{
						PosObj[j][5] = 2;
					}
					else
					{
						PosObj[j][5] = 0;
					}
				}
			}
			break;
		}
		}
		if (typeMap == 15 || typeMap == 41 || typeMap == 60 || typeMap == 61 || ((typeMap == 20 || typeMap == 21 || typeMap == 22) && GameCanvas.isLowGraOrWP_PvP()) || typeMap == 28 || typeMap == 31 || typeMap == 42 || typeMap == 33 || typeMap == 43 || typeMap == 63)
		{
			yeff = 0;
		}
		else if (typeMap == 6)
		{
			yeff = MainScreen.cameraMain.yCam / 12;
		}
		else
		{
			yeff = MainScreen.cameraMain.yCam / 6;
		}
		if (yeff < 0)
		{
			yeff = 0;
		}
	}

	public void paintCloud(mGraphics g)
	{
		if (mImgCloud == null)
		{
			loadImgCloud(islogin: false);
		}
		else
		{
			if (PosCloud == null)
			{
				return;
			}
			int num = 0;
			if (typeMap == 8)
			{
				num = hEff;
			}
			int num2 = MainScreen.cameraMain.xCam / mSpeedImg[mSpeedImg.Length - 1];
			for (int i = 0; i < PosCloud.Length; i++)
			{
				for (int j = 0; j < PosCloud[i].Length; j++)
				{
					g.drawImage(mImgCloud[PosCloud[i][j].frame], MainScreen.cameraMain.xCam - num2 + PosCloud[i][j].x / 100, PosCloud[i][j].y + yeff - num, mGraphics.VCENTER | mGraphics.LEFT);
				}
			}
		}
	}

	public void paintCloudNight(mGraphics g)
	{
		if (mImgCloudNight == null)
		{
			loadImageCloudNight();
		}
		else
		{
			if (PosCloud == null)
			{
				return;
			}
			int num = 0;
			if (typeMap == 8 || typeMap == 40)
			{
				num = hEff;
			}
			int num2 = MainScreen.cameraMain.xCam / mSpeedImg[mSpeedImg.Length - 1];
			for (int i = 0; i < PosCloud.Length; i++)
			{
				for (int j = 0; j < PosCloud[i].Length; j++)
				{
					g.setColor(0);
					g.drawImage(mImgCloudNight[PosCloud[i][j].frame], MainScreen.cameraMain.xCam - num2 + PosCloud[i][j].x / 100, PosCloud[i][j].y + yeff - num, mGraphics.VCENTER | mGraphics.LEFT);
				}
			}
		}
	}

	private void loadImageCloudNight()
	{
		mImgCloudNight = new mImage[3];
		for (int i = 0; i < mImgCloudNight.Length; i++)
		{
			mImgCloudNight[i] = mImage.createImage("/bg/cloud1" + i + ".png");
		}
	}

	private void loadImgCloud(bool islogin)
	{
		if (GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg)
		{
			return;
		}
		mImgCloud = new mImage[3];
		for (int i = 0; i < mImgCloud.Length; i++)
		{
			if (GameScreen.effMap != null && GameScreen.effMap.type == 8)
			{
				mImgCloud[i] = mImage.createImage("/bg/cloud1" + i + ".png");
				IndexCloud = 1;
			}
			else if (!islogin && (typeMap == 28 || typeMap == 29 || typeMap == 30 || lastMap == 31 || lastMap == 42))
			{
				mImgCloud[i] = mImage.createImage("/bg/cloud2" + i + ".png");
				IndexCloud = 2;
			}
			else if (!islogin && (LoadMap.idTile == 11 || LoadMap.idTile == 14))
			{
				mImgCloud[i] = mImage.createImage("/bg/cloud3" + i + ".png");
				IndexCloud = 3;
			}
			else
			{
				mImgCloud[i] = mImage.createImage("/bg/cloud" + i + ".png");
				IndexCloud = 0;
			}
		}
	}

	public void updateCloud()
	{
		if (GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg || PosCloud == null)
		{
			return;
		}
		for (int i = 0; i < PosCloud.Length; i++)
		{
			for (int j = 0; j < PosCloud[i].Length; j++)
			{
				PosCloud[i][j].x += PosCloud[i][j].vx;
				if (PosCloud[i][j].x / 100 < -80)
				{
					PosCloud[i][j].x = (GameCanvas.loadmap.mapW * LoadMap.wTile + CRes.random_Am_0(125)) * 100;
					PosCloud[i][j].y = valueMyRandom - 60 + i * 30 + CRes.random_Am_0(10) + ((i == 1) ? 5 : 0);
					PosCloud[i][j].vx = -60 / (i * 4 + 1);
					PosCloud[i][j].frame = i % 3;
				}
			}
		}
	}

	public void setBGLogin()
	{
		lastMap = -1;
		addObj();
		indexBg = 0;
		color = 10343167;
		PosCloud = null;
		if (GameCanvas.Day_Night == GameCanvas.DAY)
		{
			if (MotherCanvas.h > 230)
			{
				PosCloud = new Point[3][];
				for (int i = 0; i < PosCloud.Length; i++)
				{
					int num = CRes.random(1, 3);
					PosCloud[i] = new Point[num];
					for (int j = 0; j < num; j++)
					{
						PosCloud[i][j] = new Point();
						PosCloud[i][j].x = CRes.random(MotherCanvas.w) * 100;
						PosCloud[i][j].y = MotherCanvas.h - 290 + i * 30 + CRes.random_Am_0(10) + ((i == 1) ? 5 : 0);
						PosCloud[i][j].vx = -CRes.random(100, 200) / (i * 4 + 1);
						PosCloud[i][j].frame = i % 3;
					}
				}
			}
		}
		else
		{
			indexBg = 2;
			color = 4616312;
			PosCloud = new Point[1][];
			for (int k = 0; k < 1; k++)
			{
				int num2 = MotherCanvas.w / 18;
				PosCloud[k] = new Point[num2];
				for (int l = 0; l < num2; l++)
				{
					PosCloud[k][l] = new Point();
					PosCloud[k][l].x = l % 5 * MotherCanvas.w / 5 + MotherCanvas.w / 10 + CRes.random_Am_0(MotherCanvas.w / 10);
					int num3 = MotherCanvas.h - 230;
					if (num3 <= 0)
					{
						num3 = 1;
					}
					PosCloud[k][l].y = MotherCanvas.h - (230 + CRes.random(num3));
					PosCloud[k][l].frame = CRes.random(4) * 3;
					PosCloud[k][l].f = 4;
					PosCloud[k][l].isSmall = CRes.random(100) == 0;
					if (PosCloud[k][l].isSmall)
					{
						PosCloud[k][l].f = CRes.random(4);
					}
				}
			}
		}
		loadBGLogin();
	}

	public void updateCloudLogin()
	{
		if (GameCanvas.isLowGraOrWP_PvP() || GameCanvas.isOffBg || PosCloud == null)
		{
			return;
		}
		if (GameCanvas.Day_Night == GameCanvas.DAY)
		{
			for (int i = 0; i < PosCloud.Length; i++)
			{
				for (int j = 0; j < PosCloud[i].Length; j++)
				{
					PosCloud[i][j].x += PosCloud[i][j].vx;
					if (PosCloud[i][j].x < -9000)
					{
						PosCloud[i][j].x = (MotherCanvas.w + 60 + CRes.random_Am_0(10)) * 100;
						PosCloud[i][j].vx = -CRes.random(100, 200) / (i * 4 + 1);
						PosCloud[i][j].frame = i % 3;
					}
				}
			}
			return;
		}
		for (int k = 0; k < PosCloud.Length; k++)
		{
			for (int l = 0; l < PosCloud[k].Length; l++)
			{
				if (PosCloud[k][l].isSmall)
				{
					PosCloud[k][l].f++;
					if (PosCloud[k][l].f / 2 > 2)
					{
						PosCloud[k][l].isSmall = false;
						PosCloud[k][l].f = 4;
					}
				}
				else if (CRes.random(100) == 0)
				{
					PosCloud[k][l].frame = CRes.random(4) * 3;
					PosCloud[k][l].f = CRes.random(4);
					PosCloud[k][l].isSmall = true;
				}
			}
		}
	}

	public void updateBoat()
	{
		if (mPointObj == null)
		{
			return;
		}
		mPointObj[0].x2 += mPointObj[0].vx;
		mPointObj[0].x = mPointObj[0].x2 / 100;
		if (mPointObj[0].x < -100)
		{
			mPointObj[0].x2 = (MotherCanvas.w + 60 + CRes.random_Am_0(10)) * 100;
		}
		if (CRes.random(40) == 0)
		{
			if (CRes.random(2) == 0)
			{
				mPointObj[0].vy = 4;
			}
			else
			{
				mPointObj[0].vy = -4;
			}
		}
		if (mPointObj[0].dis > 0 && mPointObj[0].vy > 0)
		{
			mPointObj[0].vy = -4;
		}
		else if (mPointObj[0].dis < -60 && mPointObj[0].vy < 0)
		{
			mPointObj[0].vy = 4;
		}
		mPointObj[0].dis += mPointObj[0].vy;
	}

	public void addObj()
	{
		mPointObj = new Point[6];
		if (MotherCanvas.w > 300)
		{
			mPointObj = new Point[8];
		}
		mPointObj[0] = new Point(MotherCanvas.w - 40 + CRes.random_Am_0(20), MotherCanvas.h - 140 + CRes.random_Am_0(10));
		mPointObj[0].frame = 12;
		if (GameCanvas.Day_Night == GameCanvas.DAY)
		{
			mPointObj[0].vx = -CRes.random(10, 30);
		}
		mPointObj[0].x2 = mPointObj[0].x * 100;
		mPointObj[1] = new Point(20 + CRes.random_Am_0(10), MotherCanvas.h - 25 + CRes.random_Am_0(10));
		mPointObj[1].frame = 6 + GameCanvas.Day_Night * 4;
		mPointObj[2] = new Point(180 + CRes.random_Am_0(10), MotherCanvas.h - 8 + CRes.random_Am_0(10));
		mPointObj[2].frame = 6 + GameCanvas.Day_Night * 4;
		mPointObj[3] = new Point(100 + CRes.random_Am_0(10), MotherCanvas.h - 30 + CRes.random_Am_0(10));
		mPointObj[3].frame = 5 + GameCanvas.Day_Night * 4;
		mPointObj[4] = new Point(MotherCanvas.w - 25 + CRes.random_Am_0(10), MotherCanvas.h - 65 + CRes.random_Am_0(10));
		mPointObj[4].frame = 4;
		mPointObj[5] = new Point(20 + CRes.random_Am_0(10), MotherCanvas.h - 70 + CRes.random_Am_0(5));
		mPointObj[5].frame = 7;
		if (MotherCanvas.w > 300)
		{
			mPointObj[6] = new Point(MotherCanvas.w / 2 + MotherCanvas.w / 3 + CRes.random_Am_0(10), MotherCanvas.h - 65 + CRes.random_Am_0(10));
			mPointObj[6].frame = 7;
			mPointObj[7] = new Point(MotherCanvas.w / 2 - MotherCanvas.w / 3 + CRes.random_Am_0(10), MotherCanvas.h - 70 + CRes.random_Am_0(5));
			mPointObj[7].frame = 4;
		}
	}

	public void setBGLoading()
	{
		lastMap = -1;
		indexBg = 0;
		color = 10343167;
		colorMini = 4367095;
		PosCloud = null;
		if (GameCanvas.Day_Night == GameCanvas.DAY)
		{
			imgSky = mImage.createImage("/bg/sky0.png");
			if (MotherCanvas.h > 230)
			{
				PosCloud = new Point[3][];
				for (int i = 0; i < PosCloud.Length; i++)
				{
					int num = CRes.random(1, 3);
					PosCloud[i] = new Point[num];
					for (int j = 0; j < num; j++)
					{
						PosCloud[i][j] = new Point();
						PosCloud[i][j].x = CRes.random(MotherCanvas.w) * 100;
						PosCloud[i][j].y = MotherCanvas.h - 290 + 20 + i * 45 + CRes.random_Am_0(10) + ((i == 1) ? 5 : 0);
						PosCloud[i][j].vx = -CRes.random(100, 200) / (i * 4 + 1);
						PosCloud[i][j].frame = i % 3;
					}
				}
			}
		}
		else
		{
			imgSky = mImage.createImage("/bg/sky4.png");
			indexBg = 2;
			color = 4616312;
			colorMini = 3372977;
			PosCloud = new Point[1][];
			for (int k = 0; k < 1; k++)
			{
				int num2 = MotherCanvas.w / 14;
				PosCloud[k] = new Point[num2];
				for (int l = 0; l < num2; l++)
				{
					PosCloud[k][l] = new Point();
					PosCloud[k][l].x = l % 5 * MotherCanvas.w / 5 + MotherCanvas.w / 10 + CRes.random_Am_0(MotherCanvas.w / 10);
					int num3 = MotherCanvas.h - 230 + 55;
					if (num3 <= 0)
					{
						num3 = 1;
					}
					PosCloud[k][l].y = MotherCanvas.h - (230 + CRes.random(num3)) + 55;
					PosCloud[k][l].frame = CRes.random(4) * 3;
					PosCloud[k][l].f = 4;
					PosCloud[k][l].isSmall = CRes.random(100) == 0;
					if (PosCloud[k][l].isSmall)
					{
						PosCloud[k][l].f = CRes.random(4);
					}
				}
			}
		}
		mPointObj = new Point[1];
		mPointObj[0] = new Point(MotherCanvas.w - 40 + CRes.random_Am_0(20), MotherCanvas.h - 90 + CRes.random_Am_0(10));
		mPointObj[0].frame = 12;
		if (GameCanvas.Day_Night == GameCanvas.DAY)
		{
			mPointObj[0].vx = -CRes.random(10, 30);
		}
		mPointObj[0].x2 = mPointObj[0].x * 100;
	}

	public void paintBgLogin(mGraphics g)
	{
		g.setColor(color);
		g.fillRect(0, 0, MotherCanvas.w, MotherCanvas.h);
		if (imgBgLogin == null)
		{
			loadBGLogin();
			return;
		}
		if (imgSky != null)
		{
			for (int i = 0; i < MotherCanvas.w; i += 20)
			{
				g.drawImage(imgSky, i, MotherCanvas.h - 160 - 115, 0);
			}
		}
		for (int j = 0; j < MotherCanvas.w; j += 96)
		{
			g.drawImage(imgBgLogin[1 + indexBg], j, MotherCanvas.h - 191 - 82 + 34, 0);
		}
		for (int k = 0; k < MotherCanvas.w; k += 48)
		{
			g.drawImage(imgBgLogin[indexBg], k, MotherCanvas.h - 191, 0);
		}
		for (int l = 0; l < MotherCanvas.w; l += 24)
		{
			g.drawRegion(imgSeaLogin[0], 0, GameCanvas.gameTick / 8 % 2 * 24, 24, 24, 0, l, MotherCanvas.h - 191, 0);
		}
		if (GameCanvas.Day_Night == GameCanvas.DAY)
		{
			paintCloudLogin(g);
		}
		else
		{
			paintStarLogin(g);
		}
	}

	public void paintBgLoading(mGraphics g)
	{
		g.setColor(color);
		g.fillRect(0, 0, MotherCanvas.w, MotherCanvas.h);
		if (imgSky != null)
		{
			for (int i = 0; i < MotherCanvas.w; i += 20)
			{
				g.drawImage(imgSky, i, MotherCanvas.h - 100 - 115, 0);
			}
		}
		if (imgBgLogin == null || imgSeaLogin == null)
		{
			loadBGLogin();
			return;
		}
		g.setColor(colorMini);
		g.fillRect(0, MotherCanvas.h - 140, MotherCanvas.w, 140);
		for (int j = 0; j < MotherCanvas.w; j += 48)
		{
			g.drawRegion(imgBgLogin[indexBg], 0, 10, 48, 85, 0, j, MotherCanvas.h - 191 + 90, 0);
		}
		for (int k = 0; k < MotherCanvas.w; k += 96)
		{
			g.drawRegion(imgBgLogin[1 + indexBg], 0, 0, 96, 48, 0, k, MotherCanvas.h - 191 - 82 + 34 + 55, 0);
		}
		for (int l = 0; l < MotherCanvas.w; l += 24)
		{
			g.drawRegion(imgSeaLogin[0], 0, GameCanvas.gameTick / 8 % 2 * 24, 24, 20, 0, l, MotherCanvas.h - 191 + 55, 0);
		}
		if (GameCanvas.Day_Night == GameCanvas.DAY)
		{
			paintCloudLogin(g);
		}
		else
		{
			paintStarLogin(g);
		}
	}

	public void paintObjLastLogin(mGraphics g)
	{
		if (imgBgLogin == null)
		{
			loadBGLogin();
			return;
		}
		for (int i = 4; i < mPointObj.Length; i++)
		{
			if (i != 0)
			{
				g.drawImage(imgBgLogin[mPointObj[i].frame], mPointObj[i].x, mPointObj[i].y, 33);
			}
		}
	}

	public void paintObjFristLogin(mGraphics g)
	{
		if (imgBgLogin == null)
		{
			loadBGLogin();
			return;
		}
		for (int i = 0; i < 4; i++)
		{
			if (i != 0)
			{
				g.drawImage(imgBgLogin[mPointObj[i].frame], mPointObj[i].x, mPointObj[i].y, 33);
			}
		}
	}

	public void paintObjLoading(mGraphics g)
	{
		if (imgBgLogin == null)
		{
			loadBGLogin();
			return;
		}
		for (int i = 0; i < mPointObj.Length; i++)
		{
			if (i == 0)
			{
				continue;
			}
			g.drawRegion(imgBgLogin[mPointObj[i].frame], 0, 0, 90, 74 + mPointObj[i].dis / 24, 0, mPointObj[i].x, mPointObj[i].y, 33);
			if (i == 0 && !GameCanvas.isLowGraOrWP_PvP())
			{
				if (GameCanvas.Day_Night == GameCanvas.DAY)
				{
					g.drawRegion(Boat.imgEffSea, 0, 12 * (GameCanvas.gameTick / 8 % 2), 78, 12, 0, mPointObj[i].x, mPointObj[i].y + 8, 33);
				}
				else
				{
					g.drawRegion(Boat.imgEffSea2, 0, 12 * (GameCanvas.gameTick / 8 % 2), 78, 12, 0, mPointObj[i].x, mPointObj[i].y + 8, 33);
				}
			}
		}
	}

	private void paintStarLogin(mGraphics g)
	{
		if (fraStar == null)
		{
			loadImgStar();
		}
		else
		{
			if (PosCloud == null)
			{
				return;
			}
			for (int i = 0; i < PosCloud.Length; i++)
			{
				for (int j = 0; j < PosCloud[i].Length; j++)
				{
					fraStar.drawFrame(PosCloud[i][j].frame + PosCloud[i][j].f / 2, PosCloud[i][j].x, PosCloud[i][j].y, 0, 3, g);
				}
			}
		}
	}

	private void loadImgStar()
	{
		fraStar = new FrameImage(mImage.createImage("/bg/star.png"), 5, 5);
	}

	public void paintCloudLogin(mGraphics g)
	{
		try
		{
			if (mImgCloud == null)
			{
				loadImgCloud(islogin: true);
			}
			else
			{
				if (PosCloud == null)
				{
					return;
				}
				for (int i = 0; i < PosCloud.Length; i++)
				{
					for (int j = 0; j < PosCloud[i].Length; j++)
					{
						g.drawImage(mImgCloud[PosCloud[i][j].frame], PosCloud[i][j].x / 100, PosCloud[i][j].y, 3);
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void loadBGLogin()
	{
		if (imgBgLogin == null || imgSeaLogin == null)
		{
			imgBgLogin = new mImage[11];
			for (int i = 0; i < imgBgLogin.Length; i++)
			{
				if (i != 8 && (GameCanvas.Day_Night != GameCanvas.NIGHT || (i != 0 && i != 1 && i != 5 && i != 6)) && (GameCanvas.Day_Night != GameCanvas.DAY || (i != 2 && i != 3 && i != 9 && i != 10)))
				{
					imgBgLogin[i] = mImage.createImage("/bg/login" + i + ".png");
				}
			}
			imgSeaLogin = new mImage[1];
			if (GameCanvas.Day_Night == GameCanvas.DAY)
			{
				imgSeaLogin[0] = mImage.createImage("/bg/sea0.png");
				imgSky = mImage.createImage("/bg/sky0.png");
			}
			else
			{
				imgSeaLogin[0] = mImage.createImage("/bg/sea3.png");
				imgSky = mImage.createImage("/bg/sky4.png");
			}
		}
		if (lastMap == 28 || lastMap == 29 || lastMap == 30 || lastMap == 31 || lastMap == 42)
		{
			mImgCloud = null;
		}
	}

	public void setSkySnow()
	{
		if (GameCanvas.loadmap.idMap != 69 && GameCanvas.loadmap.idMap != 113)
		{
			imgSky = mImage.createImage("/bg/sky6.png");
			color = 10668525;
		}
	}

	public void setSkyDragon()
	{
		if (typeMap != 1 && typeMap != 18 && typeMap != 16 && typeMap != 35 && typeMap != 33 && typeMap != 63 && GameCanvas.loadmap.idMap != 113)
		{
			color = 7839938;
			imgSky = mImage.createImage("/bg/sky10.png");
		}
		int num = MotherCanvas.w / 150 + 1;
		if (num > 3)
		{
			num = 3;
		}
		for (int i = 0; i < num; i++)
		{
			Point point = new Point(CRes.random(0, MotherCanvas.w), -CRes.random(40));
			point.dis = CRes.random(2) * 2;
			point.fRe = CRes.random(100, 200);
			point.f = CRes.random(100);
			mVecThunder.addElement(point);
		}
		if (imgThunder == null)
		{
			imgThunder = mImage.createImage("/bg/thunder.png");
		}
		loadImgCloud(islogin: false);
		tickRanThunder = 10;
	}

	public void setDark()
	{
		color = 2635073;
		imgSky = mImage.createImage("/bg/sky11.png");
		mImgCloud = new mImage[3];
		for (int i = 0; i < mImgCloud.Length; i++)
		{
			if (GameScreen.effMap != null)
			{
				mImgCloud[i] = mImage.createImage("/bg/cloud1" + i + ".png");
				IndexCloud = 1;
			}
		}
	}
}
