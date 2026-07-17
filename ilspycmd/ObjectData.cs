using System;
using System.Collections;

public class ObjectData
{
	public const sbyte T_CHAR_PART = 7;

	public const short ITEMMAP = 0;

	public const short MONSTER = 1000;

	public const short POTION = 2000;

	public const short ITEM = 3000;

	public const short SKILL = 4000;

	public const short SKILL_SMALL = 4500;

	public const short NPC = 5000;

	public const short QUEST_POTION = 6000;

	public const short MATERIAL_POTION = 6500;

	public const short ICON_CLAN = 7000;

	public const short BOAT = 8000;

	public const short ITEM_OTHER = 9000;

	public const short CHAR_PART = 10000;

	public const short FASHION = 20000;

	public const short SKILL_COMBO = 21000;

	public const short ICON_CLAN_BIG = 22000;

	public const short IMAGE_OTHER_NEW = 23000;

	public const short IMAGE_EFF_CLIENT = 24000;

	public const short IMAGE_EFF_CLIENT_LOW = 25000;

	public const short CHAR_PART_VER2 = 26000;

	public const short GHOST = 999;

	public const short OTHER_BIG_BOSS = 0;

	public const short OTHER_TILE = 20;

	public const short OTHER_TILE_WATER = 70;

	public static mVector vecSaveImage = new mVector("ObjectData.vecSaveImage");

	public static MyHashTable HashImageItemMap = new MyHashTable("/server/item_map/");

	public static MyHashTable HashImageMonster = new MyHashTable("/server/monster/");

	public static MyHashTable hashImageItem = new MyHashTable("/server/items/");

	public static MyHashTable hashImagePotion = new MyHashTable("/server/potion/");

	public static MyHashTable hashImageSkill = new MyHashTable("/server/skill/");

	public static MyHashTable hashImageSkillSmall = new MyHashTable("/server/skill_small/");

	public static MyHashTable hashImageNPC = new MyHashTable("/server/npc/");

	public static MyHashTable hashImageQuestPotion = new MyHashTable("/server/questitem/");

	public static MyHashTable hashImageMaterialPotion = new MyHashTable("/server/material/");

	public static MyHashTable hashImageIconClan = new MyHashTable("/server/Clan/");

	public static MyHashTable hashImageIconClanBig = new MyHashTable("/server/ClanBig/");

	public static MyHashTable hashImageBoat = new MyHashTable();

	public static MyHashTable hashImageItemOther = new MyHashTable("/server/dialog/");

	public static MyHashTable HashImageCharPart = new MyHashTable("/server/char_part/Small");

	public static MyHashTable HashImageFashion = new MyHashTable("/server/itemFashion/");

	public static MyHashTable HashImageOtherNew = new MyHashTable("/server/hinhtonghop/");

	public static MyHashTable HashImageEffClient = new MyHashTable("/eff/");

	public static MyHashTable HashImageEffClientLow = new MyHashTable("/efflow/");

	public static MainImage getImageOther(short id, short type)
	{
		return getImageAll((short)(id + type), HashImageOtherNew, 23000);
	}

	public static MainImage getImageAll(short id, MyHashTable hash, short typeImage)
	{
		return getImageAll(id, string.Empty + id, hash, typeImage);
	}

	public static MainImage getImageAll(short id, string strId, MyHashTable hash, short typeImage)
	{
		if (id == -1)
		{
			return null;
		}
		MainImage mainImage = (MainImage)hash.get(strId);
		if (mainImage == null)
		{
			mainImage = new MainImage();
			hash.put(string.Empty + id, mainImage);
			mainImage.img = getFromRms(id, typeImage, hash);
			mainImage.set_W_H();
		}
		mainImage.count = GameCanvas.timeNow / 1000;
		if (mainImage.img == null)
		{
			mainImage.timeImageNull++;
			if (mainImage.timeImageNull >= 200)
			{
				GlobalService.gI().load_image(id, typeImage);
				mainImage.timeImageNull = 0;
			}
		}
		return mainImage;
	}

	public static mImage getFromRms(short id, int typeImage, MyHashTable myhash)
	{
		mImage mImage2 = null;
		sbyte[] array = null;
		if (GameMidlet.DEVICE == 4 || (GameMidlet.DEVICE != 0 && typeImage == 24000))
		{
			mImage2 = getImageServerIOS(mImage2, id, typeImage, myhash);
			if (mImage2 != null && mImage2.image != null)
			{
				return mImage2;
			}
		}
		if (typeImage == 10000 && id > 10000)
		{
			typeImage = 26000;
			id -= 10000;
		}
		if (setIdOK(id) && id >= 0)
		{
			array = CRes.loadRMS("SUB_image" + (id + typeImage));
		}
		if (array == null)
		{
			if (id >= 0)
			{
				GlobalService.gI().load_image(id, (short)typeImage);
			}
			return mImage2;
		}
		try
		{
			return mImage.createImage(array, 0, array.Length);
		}
		catch (Exception)
		{
			if (id >= 0)
			{
				GlobalService.gI().load_image(id, (short)typeImage);
			}
			return null;
		}
	}

	public static mImage getImageServerIOS(mImage img, short id, int typeImage, MyHashTable myhash)
	{
		string empty = string.Empty;
		empty = typeImage switch
		{
			24000 => "/eff/g" + id + ".png", 
			4000 => (id >= 500) ? ("/server/skill_small/" + (id - 500) + ".png") : ("/server/skill/" + id + ".png"), 
			8000 => (id >= 500) ? ("/server/boat/" + (id - 500) + ".png") : ("/server/boat_part/" + id + ".png"), 
			_ => myhash.linkImage + id + ".png", 
		};
		try
		{
			img = mImage.createImage(empty);
			return img;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static bool setIdOK(short id)
	{
		if (GameMidlet.DEVICE != 0 && GameMidlet.DEVICE != 4)
		{
			return true;
		}
		return false;
	}

	public static void setToRms(sbyte[] mimg, short id)
	{
		try
		{
			CRes.saveRMS("SUB_image" + id, mimg);
		}
		catch (Exception)
		{
		}
	}

	public static void saveImageToRmsAndroid(sbyte[] mimg, string name)
	{
		try
		{
			CRes.saveRMS("Main_Image_" + name, mimg);
		}
		catch (Exception)
		{
		}
	}

	public static void checkDelHash(MyHashTable hash, int time, bool isTrue)
	{
		if (GameMidlet.DEVICE == 4 && !isTrue)
		{
			return;
		}
		mVector mVector2 = new mVector();
		if (isTrue)
		{
			hash.clear();
			return;
		}
		IDictionaryEnumerator enumerator = hash.GetEnumerator();
		while (enumerator.MoveNext())
		{
			MainImage mainImage = (MainImage)enumerator.Value;
			if (mainImage.count != -1 && GameCanvas.timeNow / 1000 - mainImage.count > time)
			{
				string o = (string)enumerator.Key;
				mVector2.addElement(o);
			}
		}
		for (int i = 0; i < mVector2.size(); i++)
		{
			hash.remove((string)mVector2.elementAt(i));
		}
	}

	public static void checkDelHash_Data(MyHashTable hash, int time, bool isTrue)
	{
		if (GameMidlet.DEVICE == 4 && !isTrue)
		{
			return;
		}
		mVector mVector2 = new mVector();
		if (isTrue)
		{
			hash.clear();
			return;
		}
		IDictionaryEnumerator enumerator = hash.GetEnumerator();
		while (enumerator.MoveNext())
		{
			EffectData effectData = (EffectData)enumerator.Value;
			if (effectData.count != -1 && GameCanvas.timeNow / 1000 - effectData.count > time)
			{
				string o = (string)enumerator.Key;
				mVector2.addElement(o);
			}
		}
		for (int i = 0; i < mVector2.size(); i++)
		{
			hash.remove((string)mVector2.elementAt(i));
		}
	}
}
