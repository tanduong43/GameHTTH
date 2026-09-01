using System;

public class Skill_Info : MainItem
{
	public sbyte typeSkill;

	public sbyte typeBuff;

	public sbyte typeDevil;

	public short typeEffSkill;

	public short perEffSpec;

	public short timeEffSpec;

	public short indexSkillInServer;

	public sbyte idEffSpec;

	public sbyte nTarget;

	public sbyte nKick;

	public short range;

	public short manaLost;

	public short rangeLan;

	public short percentLv;

	public static sbyte maxLv = 25;

	public int timeDelay;

	public int damage;

	public const sbyte SKILL_ACTIVE = 1;

	public const sbyte SKILL_PASSIVE = 3;

	public const sbyte SKILL_BUFF = 2;

	public const sbyte SKILL_ACTIVE_SEA = 4;

	public const sbyte SKILL_JOB = 6;

	public const sbyte BUFF_ME = 1;

	public const sbyte BUFF_ME_FRIEND = 2;

	public const sbyte BUFF_ENEMY = 3;

	public const sbyte BUFF_ME_DEVIL = 4;

	public const sbyte DEVIL_NONE = 0;

	public const sbyte DEVIL_FRUIT = 1;

	public mVector vecAtt = new mVector("Skill_Info.vecAtt");

	public static Plash plashTest;

	public Skill_Info(short Index, short Id, short IdImage, sbyte type, sbyte typeBuff, string name, short typeEff, short range, short Lv)
	{
		indexSkillInServer = Index;
		ID = Id;
		idIcon = IdImage;
		typeSkill = type;
		this.typeBuff = typeBuff;
		base.name = name;
		this.range = range;
		typeEffSkill = typeEff;
		Lv_RQ = Lv;
		indexSort = typeSkill;
		if (indexSort == 4)
		{
			indexSort = 1;
		}
		int width = mFont.tahoma_7b_black.getWidth(name);
		if (width > 120)
		{
			wInfo = width + 8;
		}
		if (Lv_RQ > 0)
		{
			if (Lv_RQ == 20)
			{
				namepaint = name + " + " + T.max;
			}
			else
			{
				namepaint = name + " +" + LvUpgrade;
			}
		}
		else
		{
			namepaint = name;
		}
		plashTest = new Plash();
		plashTest.getPlashData(plashTest.getTypePlash(typeEffSkill));
		plashTest = null;
	}

	public void getData(sbyte nTarget, short rangeLan, int Damage, short Manacost, int CoolDown, sbyte nkick, string Description, sbyte LvCur, short percentLv, sbyte typeDevil)
	{
		this.nTarget = nTarget;
		this.rangeLan = rangeLan;
		damage = Damage;
		manaLost = Manacost;
		timeDelay = CoolDown;
		nKick = nkick;
		info = Description;
		Lv_RQ = LvCur;
		this.percentLv = percentLv;
		this.typeDevil = typeDevil;
	}

	public void setVecInfo(int w)
	{
		vecInfo.removeAllElements();
		string[] array = mFont.tahoma_7b_black.splitFontArray(info, w);
		for (int i = 0; i < array.Length; i++)
		{
			addInfo(array[i], 0);
		}
		if (LvDevilSkill > 0 || phanTramDevilSkill > 0)
		{
			addInfo(T.lvDevil + LvDevilSkill + " + " + phanTramDevilSkill + "%", 5);
		}
		if (typeSkill == 2)
		{
			addInfo(T.typeSkill + ": " + T.mTypeSkill[typeSkill], 0);
			addInfo(T.tacdung + ": " + T.mTacdung[typeBuff], 0);
		}
		if (typeSkill != 3 && typeSkill != 6)
		{
			if (typeSkill != 2)
			{
				addInfo(T.damage + ": " + damage, 0);
			}
			addInfo(T.manalost + ": " + manaLost, 0);
			addInfo(T.target + ": " + nTarget, 0);
			if (nTarget > 1)
			{
				addInfo(T.rangeLan + ": " + rangeLan, 0);
			}
			addInfo(T.delay + ": " + MainItem.getTimeDelay(timeDelay), 0);
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		for (int j = 0; j < vecAtt.size(); j++)
		{
			MainInfoItem mainInfoItem = (MainInfoItem)vecAtt.elementAt(j);
			if (mainInfoItem.id >= 28 && mainInfoItem.id < 32)
			{
				if (mainInfoItem.id == 28)
				{
					num3 = mainInfoItem.value;
				}
				else if (mainInfoItem.id == 29)
				{
					num2 = mainInfoItem.value;
				}
				else if (mainInfoItem.id == 30)
				{
					num = mainInfoItem.value;
				}
			}
			else if (mainInfoItem.id >= 64 && mainInfoItem.id <= 65)
			{
				if (mainInfoItem.id == 64)
				{
					num5 = mainInfoItem.value;
				}
				else if (mainInfoItem.id == 65)
				{
					num4 = mainInfoItem.value;
				}
			}
			else
			{
				addInfo(mainInfoItem.id, mainInfoItem.value, mainInfoItem.colorMain);
			}
		}
		if (num > 0)
		{
			string text = "null";
			if (num3 >= 0 && num3 <= T.mEffSpec.Length)
			{
				text = T.mEffSpec[num3];
			}
			string src = MainItem.strGetPercent(num2, 1) + " " + T.gay + " " + text + " " + T.trong + " " + MainItem.strGetPercent(num, 10);
			string[] array2 = mFont.tahoma_7b_black.splitFontArray(src, w);
			for (int k = 0; k < array2.Length; k++)
			{
				addInfo(array2[k], 0);
			}
		}
		if (num5 > 0)
		{
			string src2 = MainItem.strGetPercent(num4, 1) + " " + T.bocPhaAtt + " " + MainItem.strGetPercent(num5, 1);
			string[] array3 = mFont.tahoma_7b_black.splitFontArray(src2, w);
			for (int l = 0; l < array3.Length; l++)
			{
				addInfo(array3[l], 0);
			}
		}
	}

	public static MainImage getImage(short icon)
	{
		MainImage result = null;
		try
		{
			result = ObjectData.getImageAll(icon, ObjectData.hashImageSkill, 4000);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public mVector getActionInven()
	{
		mVector mVector2 = new mVector();
		if (Lv_RQ >= 0 && (typeSkill == 1 || typeSkill == 2 || typeSkill == 4))
		{
			mVector2.addElement(TabSkill.cmdSetHotKey);
		}
		return mVector2;
	}

	public void paint(mGraphics g, int x, int y)
	{
		MainImage mainImage = null;
		mainImage = getImage(idIcon);
		if (mainImage != null && mainImage.img != null && mainImage.img.image != null)
		{
			int imageWidth = mImage.getImageWidth(mainImage.img.image);
			int imageHeight = mImage.getImageHeight(mainImage.img.image);
			int num = 24;
			if (imageWidth > num || imageHeight > num)
			{
				int num2 = Math.Min(imageWidth, num);
				int num3 = Math.Min(imageHeight, num);
				int x2 = (imageWidth - num2) / 2;
				int y2 = (imageHeight - num3) / 2;
				g.drawRegion(mainImage.img, x2, y2, num2, num3, 0, x, y, 3);
			}
			else
			{
				g.drawImage(mainImage.img, x, y, 3);
			}
			if (Lv_RQ > GameScreen.player.Lv)
			{
				g.drawRegion(AvMain.imgDelay, 0, 0, 20, 20, 0, x, y, 3);
			}
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		if (LvDevilSkill > 0 && LvDevilSkill - 1 < AvMain.fraBorderSkill.nFrame)
		{
			AvMain.fraBorderSkill.drawFrame(LvDevilSkill - 1, x, y, 0, 3, g);
		}
	}

	public static void paintIcon(mGraphics g, int x, int y, short icon, sbyte lvDevil)
	{
		MainImage mainImage = null;
		mainImage = getImage(icon);
		if (mainImage != null && mainImage.img != null && mainImage.img.image != null)
		{
			int imageWidth = mImage.getImageWidth(mainImage.img.image);
			int imageHeight = mImage.getImageHeight(mainImage.img.image);
			int num = 24;
			if (imageWidth > num || imageHeight > num)
			{
				int num2 = Math.Min(imageWidth, num);
				int num3 = Math.Min(imageHeight, num);
				int x2 = (imageWidth - num2) / 2;
				int y2 = (imageHeight - num3) / 2;
				g.drawRegion(mainImage.img, x2, y2, num2, num3, 0, x, y, 3);
			}
			else
			{
				g.drawImage(mainImage.img, x, y, 3);
			}
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
		if (lvDevil > 0 && lvDevil - 1 < AvMain.fraBorderSkill.nFrame)
		{
			AvMain.fraBorderSkill.drawFrame(lvDevil - 1, x, y, 0, 3, g);
		}
	}

	public static Skill_Info getSkillFromID(short Id)
	{
		try
		{
			for (int i = 0; i < Player.vecListSkill.size(); i++)
			{
				Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(i);
				if (skill_Info.ID == Id)
				{
					return skill_Info;
				}
			}
		}
		catch (Exception)
		{
		}
		return null;
	}

	public static void quickSortSkill(mVector actors)
	{
		recQuickSort(actors, 0, actors.size() - 1);
	}

	private static void recQuickSort(mVector actors, int left, int right)
	{
		try
		{
			if (right - left > 0)
			{
				int pivot = ((Skill_Info)actors.elementAt(right)).typeSkill;
				int num = partitionIt(actors, left, right, pivot);
				recQuickSort(actors, left, num - 1);
				recQuickSort(actors, num + 1, right);
			}
		}
		catch (Exception)
		{
			mSystem.outloi("loi Cres 1");
		}
	}

	private static int partitionIt(mVector actors, int left, int right, int pivot)
	{
		int num = left - 1;
		int num2 = right;
		try
		{
			while (true)
			{
				if (((Skill_Info)actors.elementAt(++num)).typeSkill >= pivot)
				{
					while (num2 > 0 && ((Skill_Info)actors.elementAt(--num2)).typeSkill > pivot)
					{
					}
					if (num >= num2)
					{
						break;
					}
					swap(actors, num, num2);
				}
			}
			swap(actors, num, right);
		}
		catch (Exception)
		{
		}
		return num;
	}

	private static void swap(mVector actors, int dex1, int dex2)
	{
		object obj = actors.elementAt(dex2);
		if (((Skill_Info)actors.elementAt(dex2)).typeSkill != ((Skill_Info)actors.elementAt(dex1)).typeSkill)
		{
			actors.setElementAt(actors.elementAt(dex1), dex2);
			actors.setElementAt(obj, dex1);
		}
	}

	public string getStrType()
	{
		return typeSkill switch
		{
			1 => T.skillActive, 
			3 => T.skillPassive, 
			4 => T.skillActiveSea, 
			6 => T.skillJob, 
			2 => T.skillBuff, 
			_ => string.Empty, 
		};
	}
}
