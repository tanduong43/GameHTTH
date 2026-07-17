using System;

public class LoadMap
{
	public const int T_MAP_NULL = -1;

	public const int T_MAP_NORMAL = 0;

	public const int T_MAP_STAND = 1;

	public const int T_MAP_SLOW = 2;

	public const int MAP_NORMAL = 0;

	public const int MAP_THACH_DAU = 1;

	public const int MAP_PHO_BANG = 2;

	public const int MAP_TRAIN = 3;

	public const int MAP_SEA = 4;

	public const int MAP_RED_LINE = 5;

	public const int MAP_PHO_BANG_VUON_CAM = 6;

	public const int MAP_PHO_BANG_LITTLE_GARDEN = 7;

	public const int MAP_GOTO_SKY = 8;

	public const int MAP_SAN_BOSS = 9;

	public const int MAP_LOL = 10;

	public const int MAP_CHIEM_DAO = 11;

	public const int MAP_GOTO_GOD = 12;

	public const int IDMAP_PVP = 995;

	public const int IDMAP_ROOM_PVP_CLAN = 997;

	public const int IDMAP_PVP_CLAN = 996;

	public const int IDMAP_FIGHT_CLAN_ROOM_TRONG = 986;

	public const int IDMAP_FIGHT_CLAN_ROOM_NGOAI = 987;

	public const int IDMAP_FIGHT_CLAN_MAP_FIGHT = 985;

	public const int IDMAP_CHO_CHIEM_DAO_1 = 158;

	public const int IDMAP_CHO_CHIEM_DAO_2 = 160;

	public const int IDMAP_CHO_CHIEM_DAO_3 = 162;

	public const int IDMAP_DAU_CHIEM_DAO_1 = 157;

	public const int IDMAP_DAU_CHIEM_DAO_2 = 159;

	public const int IDMAP_DAU_CHIEM_DAO_3 = 161;

	public static mVector vecPointChange = new mVector("Loadmap.vecPointChange");

	public static MyHashTable hashMapItem = new MyHashTable();

	public static mVector[] mItemMap = new mVector[6];

	public static mVector mItemMapNonData = new mVector();

	public static sbyte[] mLockMap;

	public static short[] mMapLang;

	public static short[][] mSea;

	public static int[] mTranPointChangeMap = new int[4] { 5, 4, 1, 0 };

	public int idMap;

	public int idMapMini;

	public int mapW;

	public int mapH;

	public int limitW;

	public int limitH;

	public int limitMap;

	public int maxX;

	public int maxY;

	public int maxWMap;

	public int maxHMap;

	public int idLastMap;

	public static int idTile = -1;

	public string nameMap = string.Empty;

	public static int wTile = 24;

	public int[] mapPaint;

	public int[] mapType;

	public static int[] mStatusArea;

	public sbyte[][] mapFind;

	public static MainImage imgTile;

	public static MainImage imgTileWater;

	public static mImage imgTileSmall;

	public static int timeVibrateScreen = 0;

	public static int currentXp = 0;

	public static int maxXp = 0;

	public static sbyte Area = 0;

	public static sbyte MaxArea = 10;

	public static sbyte specMap = 0;

	public static sbyte numPlayerMap;

	public static sbyte maxnumPlayerMap;

	public static string[] mNameMap = new string[6] { "aa", "bb", "cc", "dd", "ee", "ff" };

	public static string[] mStrNameMapNPC;

	public static bool isOnlineMap = true;

	private int fStand;

	private int fWater;

	public static int demSendTem = 0;

	private int xStart;

	private int yStart;

	private int cmxMini;

	private int cmyMini;

	public LoadMap()
	{
		maxX = MotherCanvas.w / wTile + 1;
		maxY = MotherCanvas.h / wTile + 1;
		for (int i = 0; i < mItemMap.Length; i++)
		{
			mItemMap[i] = new mVector();
		}
	}

	public void loadmap(sbyte[] msbyte)
	{
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(msbyte);
			DataInputStream dataInputStream = new DataInputStream(bis);
			mapW = dataInputStream.readByte();
			mapH = dataInputStream.readByte();
			int num = dataInputStream.readByte();
			if (idTile != num)
			{
				idTile = num;
				imgTile = null;
				imgTileWater = null;
				fWater = mLockMap[idTile * 2];
				fStand = mLockMap[idTile * 2 + 1];
				ObjectData.getImageOther((short)idTile, 20);
				if (idTile == 11 || idTile == 14)
				{
					if (LoadMapScreen.isMapSky != 1)
					{
						LoadMapScreen.isMapSky = 1;
						LoadImageStatic.loadImageEffBoat();
					}
					LoadMapScreen.isMapSky = 1;
				}
				else
				{
					if (LoadMapScreen.isMapSky != 0)
					{
						LoadMapScreen.isMapSky = 0;
						LoadImageStatic.loadImageEffBoat();
					}
					LoadMapScreen.isMapSky = 0;
				}
			}
			maxWMap = mapW * wTile;
			maxHMap = mapH * wTile;
			limitW = maxWMap - MotherCanvas.w;
			limitH = maxHMap - MotherCanvas.h;
			MainScreen.cameraMain.setAll(limitW, limitH, GameScreen.player.x - MotherCanvas.hw, GameScreen.player.y - MotherCanvas.hh);
			mapPaint = new int[mapW * mapH];
			mapType = new int[mapW * mapH];
			mapFind = new sbyte[mapW][];
			for (int i = 0; i < mapW; i++)
			{
				mapFind[i] = new sbyte[mapH];
			}
			limitMap = mapW * mapH;
			for (int j = 0; j < mapW * mapH; j++)
			{
				sbyte b = dataInputStream.readByte();
				mapPaint[j] = b;
				if (b >= fStand || b == 0)
				{
					mapType[j] = 1;
				}
				else if (b >= fWater)
				{
					mapType[j] = 2;
				}
				else
				{
					mapType[j] = 0;
				}
			}
		}
		catch (Exception)
		{
			mSystem.outloi("loi load map 2");
		}
	}

	public void load_ItemMap(sbyte[] msbyte)
	{
		for (int i = 0; i < mItemMap.Length; i++)
		{
			mItemMap[i].removeAllElements();
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(msbyte);
			DataInputStream dataInputStream = new DataInputStream(bis);
			short num = dataInputStream.readShort();
			for (int j = 0; j < num; j++)
			{
				short id = dataInputStream.readShort();
				MainItemMap mainItemMap = get_Item(id);
				if (mainItemMap == null)
				{
					dataInputStream.readShort();
					dataInputStream.readShort();
					continue;
				}
				short num2 = dataInputStream.readShort();
				short num3 = dataInputStream.readShort();
				if (GameCanvas.isLowGraOrWP_PvP() && mainItemMap.layer != 2 && mainItemMap.Block.Length <= 0 && mainItemMap.isLoadDataOk)
				{
					continue;
				}
				ItemMap itemMap = new ItemMap(mainItemMap.IDItem, mainItemMap.IDImage, mainItemMap.dx, mainItemMap.dy, mainItemMap.Block, mainItemMap.layer);
				if (mainItemMap.IDImage == 312)
				{
					itemMap.typeEff = 0;
					if (j % 4 == 0)
					{
						itemMap.typeEff = 1;
					}
				}
				Block_TileMap_Item(num2, num3, mainItemMap.Block);
				itemMap.setInfoItem(num2 * wTile, num3 * wTile);
				if (mainItemMap.layer == -1)
				{
					mItemMapNonData.addElement(itemMap);
					continue;
				}
				itemMap.getImage();
				if (mainItemMap.layer != 6)
				{
					mItemMap[mainItemMap.layer].addElement(itemMap);
				}
			}
			if (!GameCanvas.isLowGraOrWP_PvP())
			{
				short num4 = dataInputStream.readShort();
				if (num4 > 0)
				{
					for (int k = 0; k < num4; k++)
					{
						int num5 = dataInputStream.readByte();
						string text = string.Empty;
						for (int l = 0; l < num5; l++)
						{
							text += (char)dataInputStream.readByte();
						}
						num5 = dataInputStream.readByte();
						string text2 = string.Empty;
						for (int m = 0; m < num5; m++)
						{
							text2 += (char)dataInputStream.readByte();
						}
						mItemMap[3].addElement(GameScreen.addEffectAuto(text, text2));
					}
				}
			}
			for (int n = 0; n < mItemMap.Length; n++)
			{
				CRes.quickSort(mItemMap[n]);
			}
		}
		catch (Exception)
		{
			mSystem.outloi("loi load map 3");
		}
	}

	public void paint(mGraphics g)
	{
		int num = MainScreen.cameraMain.xCam / wTile - 1;
		int num2 = MainScreen.cameraMain.yCam / wTile - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		int num3 = num + maxX + 2;
		int num4 = num2 + maxY + 2;
		if (num3 > mapW)
		{
			num3 = mapW;
		}
		if (num4 > mapH)
		{
			num4 = mapH;
		}
		if ((imgTileWater == null || imgTileWater.img == null) && !GameCanvas.lowGraphic)
		{
			imgTileWater = ObjectData.getImageOther((short)idTile, 70);
		}
		if (imgTile == null || imgTile.img == null)
		{
			imgTile = ObjectData.getImageOther((short)idTile, 20);
			g.setColor(15445332);
			g.fillRect(MainScreen.cameraMain.xCam, MainScreen.cameraMain.yCam, MotherCanvas.w, MotherCanvas.h);
			return;
		}
		for (int i = num; i < num3; i++)
		{
			for (int j = num2; j < num4; j++)
			{
				int num5 = j * mapW + i;
				if (num5 >= mapPaint.Length)
				{
					continue;
				}
				int num6 = mapPaint[num5] - 1;
				if (!GameCanvas.lowGraphic && idTile == 3 && num6 >= 35 && num6 <= 37 && GameCanvas.gameTick % 14 < 7)
				{
					int num7 = 0;
					if (GameCanvas.gameTick / 14 % 2 == 0)
					{
						num7 = 3;
					}
					if (imgTileWater != null && imgTileWater.img != null)
					{
						g.drawRegion(imgTileWater.img, (num7 + num6 - 35) / 10 * wTile, (num7 + num6 - 35) % 10 * wTile, wTile, wTile, 0, i * wTile, j * wTile, 0);
					}
				}
				else if (!GameCanvas.lowGraphic && num6 >= fWater - 1 && num6 < fStand - 1 && GameCanvas.gameTick % 14 < 7 && idTile != 3)
				{
					int num8 = 0;
					if (GameCanvas.gameTick / 14 % 2 == 0)
					{
						num8 = fStand - fWater;
					}
					if (imgTileWater != null && imgTileWater.img != null)
					{
						g.drawRegion(imgTileWater.img, (num8 + num6 - (fWater - 1)) / 10 * wTile, (num8 + num6 - (fWater - 1)) % 10 * wTile, wTile, wTile, 0, i * wTile, j * wTile, 0);
					}
				}
				else if (num6 > -1 && imgTile != null && imgTile.img != null)
				{
					g.drawRegion(imgTile.img, num6 / 10 * wTile, num6 % 10 * wTile, wTile, wTile, 0, i * wTile, j * wTile, 0);
				}
			}
		}
	}

	public void update()
	{
	}

	public int getTile(int xset, int yset)
	{
		int num = yset / wTile * mapW + xset / wTile;
		if (num > limitMap || xset < 0 || xset >= limitW + MotherCanvas.w || yset < 0 || yset >= limitH + MotherCanvas.h)
		{
			return 1;
		}
		return mapType[num];
	}

	public int getIndex(int xset, int yset)
	{
		return yset / wTile * mapW + xset / wTile;
	}

	public MainItemMap get_Item(short id)
	{
		MainItemMap mainItemMap = (MainItemMap)hashMapItem.get(string.Empty + id);
		if (mainItemMap == null)
		{
			mainItemMap = new MainItemMap(id);
			mainItemMap.isLoadDataOk = false;
			hashMapItem.put(string.Empty + id, mainItemMap);
			GlobalService.gI().GetTemplate(98, id);
			if (demSendTem == 0)
			{
				demSendTem = 1000;
			}
			demSendTem++;
		}
		return mainItemMap;
	}

	public void Block_TileMap_Item(int indexW, int indexH, int[][] mb)
	{
		try
		{
			for (int i = 0; i < mb.Length; i++)
			{
				if (indexW + mb[i][0] >= 0 && indexW + mb[i][0] < mapW && indexH + mb[i][1] >= 0 && indexH + mb[i][1] < mapH)
				{
					mapType[(indexH + mb[i][1]) * mapW + (indexW + mb[i][0])] = 1;
				}
			}
		}
		catch (Exception)
		{
			mSystem.outloi("loi load map 4");
		}
	}

	public static sbyte getAreaPaint()
	{
		return (sbyte)(Area + 1);
	}

	public static bool Tile_Stand(int type)
	{
		if (type == -1 || type == 1)
		{
			return true;
		}
		return false;
	}

	public short[] updateFindRoad(int xF, int yF, int xTo, int yTo, int maxSize, MainObject objMain)
	{
		short[] array = updateFindRoad(xF, yF, xTo, yTo, maxSize, objMain, 0);
		if (specMap == 4 && (array == null || array.Length > maxSize))
		{
			array = updateFindRoad(xF, yF, xTo, yTo, maxSize, objMain, 1);
		}
		return array;
	}

	public short[] updateFindRoad(int xF, int yF, int xTo, int yTo, int maxSize, MainObject objMain, int numindex)
	{
		if (MainObject.getDistance(xF * wTile, yF * wTile, xTo * wTile, yTo * wTile) <= wTile)
		{
			return null;
		}
		if (xF < 0 || yF < 0 || xF >= wTile * mapW || yF >= wTile * mapH)
		{
			return null;
		}
		xStart = (sbyte)cmxMini;
		yStart = (sbyte)cmyMini;
		xF -= xStart;
		yF -= yStart;
		xTo -= xStart;
		yTo -= yStart;
		for (int i = 0; i < GameCanvas.loadmap.mapFind.Length; i++)
		{
			for (int j = 0; j < GameCanvas.loadmap.mapFind[i].Length; j++)
			{
				int num = (yStart + j) * GameCanvas.loadmap.mapW + (xStart + i);
				if (num < GameCanvas.loadmap.mapType.Length - 1)
				{
					if (objMain.typeActionBoat != 0)
					{
						GameCanvas.loadmap.mapFind[i][j] = 0;
					}
					else if (GameCanvas.loadmap.mapType[num] == 1 || GameCanvas.loadmap.mapType[num] == -1)
					{
						GameCanvas.loadmap.mapFind[i][j] = -1;
					}
					else
					{
						GameCanvas.loadmap.mapFind[i][j] = 0;
					}
				}
			}
		}
		if (specMap == 4 && numindex == 0)
		{
			for (int k = 0; k < GameScreen.vecPlayers.size(); k++)
			{
				MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(k);
				if (mainObject != objMain && mainObject.boatSea != null && mainObject.boatSea.ID == mainObject.ID)
				{
					mainObject.boatSea.boatSetTouch();
					setBlockPlayer(mainObject.boatSea.xset, mainObject.boatSea.yset, mainObject.boatSea.wset, mainObject.boatSea.hset);
				}
			}
		}
		short num2 = 0;
		int num3 = xF;
		int num4 = yF;
		short num5 = (short)num3;
		short num6 = (short)num4;
		GameCanvas.loadmap.mapFind[num3][num4] = 1;
		num2 = 2;
		int num7 = GameCanvas.loadmap.mapFind.Length;
		int num8 = GameCanvas.loadmap.mapFind[0].Length;
		int num9 = 0;
		while (true)
		{
			num9++;
			if (num9 > 1000)
			{
				return new short[maxSize + 1];
			}
			int num10 = -1;
			int num11 = -1;
			if (num3 + 1 < num7 && GameCanvas.loadmap.mapFind[num3 + 1][num4] == 0)
			{
				GameCanvas.loadmap.mapFind[num3 + 1][num4] = (sbyte)num2;
				num10 = num3 + 1;
				num11 = num4;
			}
			if (num3 - 1 >= 0 && GameCanvas.loadmap.mapFind[num3 - 1][num4] == 0)
			{
				GameCanvas.loadmap.mapFind[num3 - 1][num4] = (sbyte)num2;
				if (num10 != -1)
				{
					if (CRes.setDis(num10, num11, xTo, yTo) > CRes.setDis(num3 - 1, num4, xTo, yTo))
					{
						num10 = num3 - 1;
						num11 = num4;
					}
				}
				else
				{
					num10 = num3 - 1;
					num11 = num4;
				}
			}
			if (num4 + 1 < num8 && GameCanvas.loadmap.mapFind[num3][num4 + 1] == 0)
			{
				GameCanvas.loadmap.mapFind[num3][num4 + 1] = (sbyte)num2;
				if (num10 != -1)
				{
					if (CRes.setDis(num10, num11, xTo, yTo) > CRes.setDis(num3, num4 + 1, xTo, yTo))
					{
						num10 = num3;
						num11 = num4 + 1;
					}
				}
				else
				{
					num10 = num3;
					num11 = num4 + 1;
				}
			}
			if (num4 - 1 >= 0 && GameCanvas.loadmap.mapFind[num3][num4 - 1] == 0)
			{
				GameCanvas.loadmap.mapFind[num3][num4 - 1] = (sbyte)num2;
				if (num10 != -1)
				{
					if (CRes.setDis(num10, num11, xTo, yTo) > CRes.setDis(num3, num4 - 1, xTo, yTo))
					{
						num10 = num3;
						num11 = num4 - 1;
					}
				}
				else
				{
					num10 = num3;
					num11 = num4 - 1;
				}
			}
			int num12 = -1;
			if (num10 != -1)
			{
				num12 = 0;
				num3 = num10;
				num4 = num11;
			}
			else
			{
				num3 = (num4 = 1000);
			}
			for (short num13 = 0; num13 < num7; num13++)
			{
				for (short num14 = 0; num14 < num8; num14++)
				{
					if (GameCanvas.loadmap.mapFind[num13][num14] > 1 && setContinue(num13, num14, GameCanvas.loadmap.mapFind) && GameCanvas.loadmap.mapFind[num13][num14] + CRes.setDis(num13, num14, xTo, yTo) < num2 + CRes.setDis(num3, num4, xTo, yTo))
					{
						num3 = num13;
						num4 = num14;
						num2 = GameCanvas.loadmap.mapFind[num13][num14];
						num12 = 0;
					}
				}
			}
			if (num3 == xTo && num4 == yTo)
			{
				break;
			}
			if (num12 == 0)
			{
				num2++;
				if (num2 > maxSize)
				{
					return new short[num2];
				}
				continue;
			}
			return new short[maxSize + 1];
		}
		if (num2 >= 127)
		{
			return new short[maxSize + 1];
		}
		int num15 = 0;
		short[] array = new short[num2];
		try
		{
			while (true)
			{
				array[num15] = (short)((num3 << 8) + num4);
				if (num3 + 1 < num7 && GameCanvas.loadmap.mapFind[num3 + 1][num4] == GameCanvas.loadmap.mapFind[num3][num4] - 1)
				{
					num3++;
				}
				else if (num3 - 1 >= 0 && GameCanvas.loadmap.mapFind[num3 - 1][num4] == GameCanvas.loadmap.mapFind[num3][num4] - 1)
				{
					num3--;
				}
				else if (num4 + 1 < num8 && GameCanvas.loadmap.mapFind[num3][num4 + 1] == GameCanvas.loadmap.mapFind[num3][num4] - 1)
				{
					num4++;
				}
				else if (num4 - 1 >= 0 && GameCanvas.loadmap.mapFind[num3][num4 - 1] == GameCanvas.loadmap.mapFind[num3][num4] - 1)
				{
					num4--;
				}
				if (num3 == num5 && num4 == num6)
				{
					break;
				}
				num15++;
			}
			array[num2 - 1] = (short)((xF << 8) + yF);
			return array;
		}
		catch (Exception)
		{
			return new short[maxSize + 1];
		}
	}

	private bool setContinue(int i, int j, sbyte[][] mapFind)
	{
		if (i + 1 < mapFind.Length && mapFind[i + 1][j] == 0)
		{
			return true;
		}
		if (i - 1 >= 0 && mapFind[i - 1][j] == 0)
		{
			return true;
		}
		if (j + 1 < mapFind[i].Length && mapFind[i][j + 1] == 0)
		{
			return true;
		}
		if (j - 1 >= 0 && mapFind[i][j - 1] == 0)
		{
			return true;
		}
		return false;
	}

	public void setBlockNPC(int x, int y, int w, int h)
	{
		for (int i = 0; i < w / 24; i++)
		{
			for (int j = 0; j < h / 24; j++)
			{
				int num = x - w / 2 + i * 24 + 13;
				int num2 = y - h / 2 + j * 24 + 13;
				if (num >= 0 && num < maxWMap && num2 >= 0 && num2 < maxHMap)
				{
					mapType[num2 / 24 * mapW + num / 24] = 1;
				}
			}
		}
	}

	public void setBlockPlayer(int x, int y, int w, int h)
	{
		for (int num = 0; num <= w; num = num)
		{
			for (int num2 = 0; num2 <= h; num2 = num2)
			{
				int num3 = x - w / 2 + num;
				int num4 = y - h / 2 + num2;
				if (num2 == h || num2 + 24 <= h)
				{
					num2 += 24;
				}
				else if (num2 + 24 > h)
				{
					num2 = h;
				}
				if (num3 >= 0 && num3 < maxWMap && num4 >= 0 && num4 < maxHMap)
				{
					mapFind[num3 / 24][num4 / 24] = -1;
				}
			}
			if (num == w || num + 24 <= w)
			{
				num += 24;
			}
			else if (num + 24 > w)
			{
				num = w;
			}
		}
	}

	public bool mapLang()
	{
		for (int i = 0; i < mMapLang.Length; i++)
		{
			if (idMap == mMapLang[i])
			{
				return true;
			}
		}
		return false;
	}

	public static void LoadNameMap(DataInputStream iss, bool isSave)
	{
		if (iss == null)
		{
			GlobalService.gI().get_DATA(6);
			return;
		}
		try
		{
			short num = iss.readShort();
			mNameMap = new string[num];
			for (int i = 0; i < num; i++)
			{
				mNameMap[i] = iss.readUTF();
			}
			if (isSave)
			{
				GlobalService.VerNameMap = iss.readShort();
				SaveRms.saveVer(GlobalService.VerNameMap, "VerdataNameMap");
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}

	public static string getNameMap(int index)
	{
		if (index >= mNameMap.Length)
		{
			return string.Empty;
		}
		return mNameMap[index];
	}

	public static void load_Table_Map(DataInputStream iss, bool isSave)
	{
		try
		{
			short num = iss.readShort();
			for (short num2 = 0; num2 < num; num2++)
			{
				short iDImage = iss.readShort();
				sbyte layer = iss.readByte();
				short dx = iss.readShort();
				short dy = iss.readShort();
				sbyte b = iss.readByte();
				int[][] array = new int[b][];
				for (int i = 0; i < b; i++)
				{
					array[i] = new int[2];
					array[i][0] = iss.readByte();
					array[i][1] = iss.readByte();
				}
				hashMapItem.put(string.Empty + num2, new MainItemMap(num2, iDImage, dx, dy, array, layer));
			}
			if (isSave)
			{
				GlobalService.VerItemMap = iss.readShort();
				SaveRms.saveVer(GlobalService.VerItemMap, "VerdataItemMap");
			}
			iss.close();
		}
		catch (Exception)
		{
			mSystem.outloi("loi load map 1");
		}
	}

	public void load_Table_Map(DataInputStream iss)
	{
		try
		{
			short num = iss.readShort();
			short iDImage = iss.readShort();
			sbyte layer = iss.readByte();
			short dx = iss.readShort();
			short dy = iss.readShort();
			sbyte b = iss.readByte();
			int[][] array = new int[b][];
			for (int i = 0; i < b; i++)
			{
				array[i] = new int[2];
				array[i][0] = iss.readByte();
				array[i][1] = iss.readByte();
			}
			hashMapItem.put(string.Empty + num, new MainItemMap(num, iDImage, dx, dy, array, layer));
			iss.close();
		}
		catch (Exception)
		{
		}
	}

	public void checkSetItemMap()
	{
		for (int i = 0; i < mItemMapNonData.size(); i++)
		{
			ItemMap itemMap = (ItemMap)mItemMapNonData.elementAt(i);
			MainItemMap mainItemMap = get_Item(itemMap.IDItem);
			if (!mainItemMap.isLoadDataOk)
			{
				continue;
			}
			if (!GameCanvas.isLowGraOrWP_PvP() || mainItemMap.layer == 2 || mainItemMap.Block.Length > 0)
			{
				ItemMap itemMap2 = new ItemMap(itemMap.IDItem, mainItemMap.IDImage, mainItemMap.dx, mainItemMap.dy, mainItemMap.Block, mainItemMap.layer);
				Block_TileMap_Item(itemMap.x / 24, itemMap.y / 24, mainItemMap.Block);
				itemMap2.setInfoItem(itemMap.x, itemMap.y);
				itemMap2.getImage();
				if (mainItemMap.IDImage == 312)
				{
					itemMap2.typeEff = 0;
					if (i % 4 == 0)
					{
						itemMap2.typeEff = 1;
					}
				}
				if (itemMap2.layer != 6)
				{
					mItemMap[itemMap2.layer].addElement(itemMap2);
				}
			}
			mItemMapNonData.removeElementAt(i);
			i--;
		}
	}

	public static sbyte getShowArea(sbyte area)
	{
		return (sbyte)(area + 1);
	}
}
