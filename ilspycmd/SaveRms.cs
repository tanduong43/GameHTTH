using System;

public class SaveRms
{
	public static string userLast = string.Empty;

	public const sbyte RMS_SER_SKILL = 0;

	public const sbyte RMS_SER_AUTO_MP_HP = 1;

	public const sbyte RMS_SER_AUTO_GET_ITEM = 2;

	public const sbyte RMS_SER_AUTO_MATERIAL = 3;

	public const sbyte RMS_SER_INDEX_HELP = 4;

	public const sbyte RMS_SER_AUTO_FIRE = 5;

	public const sbyte RMS_SER_UNIFORM = 6;

	public const sbyte RMS_SER_SKILL_BUFF = 7;

	public const sbyte RMS_SER_INDEX_LANG = 8;

	public const sbyte RMS_SER_SKILL_PLAYER = 9;

	public const sbyte RMS_SER_NHAN_VAT = 10;

	public static sbyte[] datahotKeySkill;

	public void saveUserPass(string user, string pass)
	{
		if (user.Length == 0 || pass.Length == 0)
		{
			return;
		}
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeUTF(user);
			dataOutputStream.writeUTF(pass);
			dataOutputStream.writeByte(ListChar_Screen.IndexCharSelected);
			dataOutputStream.writeByte((sbyte)GameCanvas.IndexServer);
			CRes.saveRMS("MAIN_user_pass", byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void saveDataeff(short id, sbyte[] data)
	{
		try
		{
			CRes.saveRMS("dataeff_SKILL" + id, data);
		}
		catch (Exception)
		{
		}
	}

	public bool loadUserPass()
	{
		sbyte[] array = CRes.loadRMS("MAIN_user_pass");
		if (array == null)
		{
			if (GameCanvas.IndexServer > GameCanvas.strListServer[GameCanvas.language].Length)
			{
				GameCanvas.IndexServer = 0;
			}
			return false;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(array);
			DataInputStream dataInputStream = new DataInputStream(bis);
			GameCanvas.loginScr.tfUser.setText(dataInputStream.readUTF());
			GameCanvas.loginScr.tfPass.setText(dataInputStream.readUTF());
			ListChar_Screen.IndexCharSelected = dataInputStream.readByte();
			if (dataInputStream.available() > 0)
			{
				GameCanvas.IndexServer = dataInputStream.readByte();
				if (GameCanvas.IndexServer > GameCanvas.strListServer[GameCanvas.language].Length)
				{
					GameCanvas.IndexServer = 0;
				}
			}
			else
			{
				GameCanvas.IndexServer = GameCanvas.strListServer[GameCanvas.language].Length - 1;
			}
			dataInputStream.close();
		}
		catch (Exception)
		{
		}
		return true;
	}

	public void saveUserLast(string user)
	{
		if (user.Length == 0)
		{
			return;
		}
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeUTF(user);
			CRes.saveRMS("MAIN_user_last", byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void loadUserLast()
	{
		sbyte[] array = CRes.loadRMS("MAIN_user_last");
		if (array == null)
		{
			return;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(array);
			DataInputStream dataInputStream = new DataInputStream(bis);
			userLast = dataInputStream.readUTF();
			dataInputStream.close();
			if (userLast.Length >= 10)
			{
				userLast.Substring(0, 9);
			}
		}
		catch (Exception)
		{
		}
	}

	public void saveIpLast(string ip)
	{
		if (ip.Length == 0)
		{
			return;
		}
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeUTF(ip);
			CRes.saveRMS("MAIN_ip_last", byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public static string loadIpLast()
	{
		mSystem.outz("1");
		sbyte[] array = CRes.loadRMS("MAIN_ip_last");
		mSystem.outz("2");
		if (array == null)
		{
			return string.Empty;
		}
		try
		{
			mSystem.outz("3");
			ByteArrayInputStream bis = new ByteArrayInputStream(array);
			DataInputStream dataInputStream = new DataInputStream(bis);
			mSystem.outz("4");
			string text = dataInputStream.readUTF();
			mSystem.outz("5");
			dataInputStream.close();
			mSystem.outz("ip = " + text);
			return text;
		}
		catch (Exception)
		{
			mSystem.outz("loi ip");
			return string.Empty;
		}
	}

	public void loadBeginGame()
	{
		loadUserPass();
		GlobalService.VerCharPar = loadVer("VerdataCharPart");
		GlobalService.VerMonster = loadVer("VerdataMon");
		GlobalService.VerNameAtribute = loadVer("VerdataAttri");
		GlobalService.VerPotion = loadVer("VerdataPotion");
		GlobalService.VerNameMap = loadVer("VerdataNameMap");
		GlobalService.VerItemMap = loadVer("VerdataItemMap");
		GlobalService.VerImageSave = loadVer("VerdataImageSave");
		GlobalService.VerNamePotionQuest = loadVer("VerdataNamePotionquest");
		GlobalService.VerDataUpgrade = loadVer("VerdataUpgradeSave");
	}

	public void saveHotKey()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			for (int i = 0; i < Player.hotkeyPlayer.Length; i++)
			{
				for (int j = 0; j < Player.hotkeyPlayer[i].Length; j++)
				{
					dataOutputStream.writeByte(Player.hotkeyPlayer[i][j].getTypeHotKey());
					if (Player.hotkeyPlayer[i][j].getTypeHotKey() == 0)
					{
						dataOutputStream.writeShort(Player.hotkeyPlayer[i][j].itemcur.ID);
					}
					else if (Player.hotkeyPlayer[i][j].getTypeHotKey() == 1)
					{
						dataOutputStream.writeShort(Player.hotkeyPlayer[i][j].skill.ID);
					}
				}
			}
			GlobalService.gI().Save_RMS_Server(1, 0, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void loadHotKey(sbyte[] bData)
	{
		if (bData == null)
		{
			for (int i = 0; i < Player.hotkeyPlayer.Length; i++)
			{
				if (Player.vecListSkill.size() > 0)
				{
					Skill_Info skill_Info = (Skill_Info)Player.vecListSkill.elementAt(0);
					if (skill_Info != null)
					{
						MainSkill mainSkill = new MainSkill(skill_Info.ID, -1);
						mainSkill.indexHotKey = skill_Info.indexHotKey;
						mainSkill.idIcon = skill_Info.idIcon;
						mainSkill.isBuff = skill_Info.typeSkill == 2;
						Player.hotkeyPlayer[i][2].setSkill(mainSkill, mainSkill.idIcon);
					}
				}
			}
			return;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(bData);
			DataInputStream dataInputStream = new DataInputStream(bis);
			for (int j = 0; j < Player.hotkeyPlayer.Length; j++)
			{
				for (int k = 0; k < Player.hotkeyPlayer[j].Length; k++)
				{
					int num = dataInputStream.readByte();
					if (num == -1)
					{
						continue;
					}
					short id = dataInputStream.readShort();
					switch (num)
					{
					case 0:
					{
						MainItem itemVec = MainItem.getItemVec(4, id, Player.vecInventory);
						if (itemVec != null)
						{
							Player.hotkeyPlayer[j][k].setPotion(itemVec);
						}
						break;
					}
					case 1:
					{
						Skill_Info skillFromID = Skill_Info.getSkillFromID(id);
						if (skillFromID != null)
						{
							MainSkill mainSkill2 = new MainSkill(skillFromID.ID, -1);
							mainSkill2.indexHotKey = skillFromID.indexHotKey;
							mainSkill2.idIcon = skillFromID.idIcon;
							mainSkill2.isBuff = skillFromID.typeSkill == 2;
							Player.hotkeyPlayer[j][k].setSkill(mainSkill2, mainSkill2.idIcon);
						}
						break;
					}
					}
				}
			}
			dataInputStream.close();
			Skill_Info skill_Info2 = (Skill_Info)Player.vecListSkill.elementAt(0);
			if (skill_Info2 == null)
			{
				return;
			}
			for (int l = 0; l < Player.hotkeyPlayer.Length; l++)
			{
				if (Player.vecListSkill.size() > 0 && (Player.hotkeyPlayer[l][2].skill == null || Player.hotkeyPlayer[l][2].skill.ID != skill_Info2.ID))
				{
					MainSkill mainSkill3 = new MainSkill(skill_Info2.ID, -1);
					mainSkill3.indexHotKey = skill_Info2.indexHotKey;
					mainSkill3.idIcon = skill_Info2.idIcon;
					mainSkill3.isBuff = skill_Info2.typeSkill == 2;
					Player.hotkeyPlayer[l][2].setSkill(mainSkill3, mainSkill3.idIcon);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void setLoadRMSServer(sbyte id, sbyte[] data)
	{
		if (id == 0)
		{
			datahotKeySkill = data;
			loadHotKey(data);
		}
		else if (id == 1)
		{
			loadAutoMp_Hp(data);
		}
		else if (id == 2)
		{
			loadAutoGetItem(data);
		}
		else if (id == 3)
		{
			loadAutoMaterial(data);
		}
		else if (id == 4)
		{
			if (data != null)
			{
				try
				{
					ByteArrayInputStream bis = new ByteArrayInputStream(data);
					DataInputStream dataInputStream = new DataInputStream(bis);
					GameScreen.indexHelp = dataInputStream.readShort();
					dataInputStream.close();
					return;
				}
				catch (Exception)
				{
					return;
				}
			}
			GameScreen.indexHelp = 0;
		}
		else if (id == 5)
		{
			loadAutoFire(data);
		}
		else if (id == 6)
		{
			loadUniform(data);
		}
		else if (id == 7)
		{
			if (data != null)
			{
				try
				{
					ByteArrayInputStream bis2 = new ByteArrayInputStream(data);
					DataInputStream dataInputStream2 = new DataInputStream(bis2);
					GameScreen.isShowSkillBuff = dataInputStream2.readByte() == 1;
					dataInputStream2.close();
				}
				catch (Exception)
				{
				}
			}
		}
		else if (id == 8)
		{
			if (data != null)
			{
				try
				{
					ByteArrayInputStream bis3 = new ByteArrayInputStream(data);
					DataInputStream dataInputStream3 = new DataInputStream(bis3);
					GameScreen.indexHDVaoLang = dataInputStream3.readByte();
					dataInputStream3.close();
				}
				catch (Exception)
				{
				}
			}
		}
		else if (id == 9)
		{
			if (data != null)
			{
				try
				{
					ByteArrayInputStream bis4 = new ByteArrayInputStream(data);
					DataInputStream dataInputStream4 = new DataInputStream(bis4);
					GameScreen.isShowSkillPlayer = dataInputStream4.readByte() == 1;
					dataInputStream4.close();
				}
				catch (Exception)
				{
				}
			}
		}
		else if (id == 10 && data != null)
		{
			try
			{
				ByteArrayInputStream bis5 = new ByteArrayInputStream(data);
				DataInputStream dataInputStream5 = new DataInputStream(bis5);
				GameScreen.isShowNhanVat = dataInputStream5.readByte() == 1;
				dataInputStream5.close();
			}
			catch (Exception)
			{
			}
		}
	}

	public void loadAutoMaterial(sbyte[] bData)
	{
		if (bData == null)
		{
			return;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(bData);
			DataInputStream dataInputStream = new DataInputStream(bis);
			Player.isAutoMaterial = dataInputStream.readByte();
			dataInputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void SaveAutoMaterial()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte(Player.isAutoMaterial);
			GlobalService.gI().Save_RMS_Server(1, 3, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public static void saveVer(short ver, string name)
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeShort(ver);
			CRes.saveRMS("MAIN_" + name, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public static short loadVer(string name)
	{
		sbyte[] array = CRes.loadRMS("MAIN_" + name);
		short result = -1;
		try
		{
			if (array != null)
			{
				ByteArrayInputStream bis = new ByteArrayInputStream(array);
				DataInputStream dataInputStream = new DataInputStream(bis);
				result = dataInputStream.readShort();
				dataInputStream.close();
			}
		}
		catch (Exception)
		{
		}
		return result;
	}

	public static void saveData(sbyte[] dataSave, string name)
	{
		try
		{
			CRes.saveRMS("MAIN_" + name, dataSave);
		}
		catch (Exception)
		{
		}
	}

	public static DataInputStream loadData(string name)
	{
		sbyte[] array = CRes.loadRMS("MAIN_" + name);
		DataInputStream result = null;
		if (array == null)
		{
			return null;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(array);
			result = new DataInputStream(bis);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public void SaveAutoMp_Hp()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte((sbyte)(Player.isMPHP ? 1 : 0));
			if (Player.isMPHP)
			{
				dataOutputStream.writeByte((sbyte)MsgAutoMPHP.mp);
				dataOutputStream.writeByte((sbyte)MsgAutoMPHP.hp);
				dataOutputStream.writeByte((sbyte)MsgAutoMPHP.typeUu);
			}
			GlobalService.gI().Save_RMS_Server(1, 1, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void loadAutoMp_Hp(sbyte[] bData)
	{
		if (bData == null)
		{
			return;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(bData);
			DataInputStream dataInputStream = new DataInputStream(bis);
			Player.isMPHP = dataInputStream.readByte() == 1;
			if (Player.isMPHP)
			{
				MsgAutoMPHP.mp = dataInputStream.readByte();
				MsgAutoMPHP.hp = dataInputStream.readByte();
				MsgAutoMPHP.typeUu = dataInputStream.readByte();
			}
			dataInputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void SaveUniform()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte((sbyte)Player.vecUniform.size());
			for (int i = 0; i < Player.vecUniform.size(); i++)
			{
				Uniform uniform = (Uniform)Player.vecUniform.elementAt(i);
				dataOutputStream.writeByte(uniform.index);
				dataOutputStream.writeByte(uniform.isSet);
				for (int j = 0; j < 8; j++)
				{
					if (j < uniform.mIdUniform.Length)
					{
						dataOutputStream.writeShort(uniform.mIdUniform[j]);
					}
					else
					{
						dataOutputStream.writeShort(-1);
					}
				}
			}
			GlobalService.gI().Save_RMS_Server(1, 6, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void loadUniform(sbyte[] bData)
	{
		if (bData == null)
		{
			return;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(bData);
			DataInputStream dataInputStream = new DataInputStream(bis);
			sbyte b = dataInputStream.readByte();
			for (int i = 0; i < b; i++)
			{
				Uniform uniform = new Uniform();
				uniform.index = dataInputStream.readByte();
				uniform.isSet = dataInputStream.readByte();
				uniform.mIdUniform = new short[8];
				for (int j = 0; j < 8; j++)
				{
					uniform.mIdUniform[j] = dataInputStream.readShort();
				}
				Player.vecUniform.addElement(uniform);
			}
			dataInputStream.close();
			Uniform.checkIndexItem(isRemove: false);
		}
		catch (Exception)
		{
		}
	}

	public void SaveAutoGetItem()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte((sbyte)(Player.isGetItem ? 1 : 0));
			if (Player.isGetItem)
			{
				for (int i = 0; i < MsgAutoGetItem.mValue.Length; i++)
				{
					dataOutputStream.writeByte((sbyte)MsgAutoGetItem.mValue[i]);
				}
			}
			GlobalService.gI().Save_RMS_Server(1, 2, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void SaveShowSkillBuff()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte((sbyte)(GameScreen.isShowSkillBuff ? 1 : 0));
			GlobalService.gI().Save_RMS_Server(1, 7, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void SaveShowSkillPlayer()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte((sbyte)(GameScreen.isShowSkillPlayer ? 1 : 0));
			GlobalService.gI().Save_RMS_Server(1, 9, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void SaveShowNhanVat()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte((sbyte)(GameScreen.isShowNhanVat ? 1 : 0));
			GlobalService.gI().Save_RMS_Server(1, 10, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void loadAutoGetItem(sbyte[] bData)
	{
		if (bData == null)
		{
			return;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(bData);
			DataInputStream dataInputStream = new DataInputStream(bis);
			Player.isGetItem = dataInputStream.readByte() == 1;
			if (Player.isGetItem)
			{
				for (int i = 0; i < MsgAutoGetItem.mValue.Length; i++)
				{
					MsgAutoGetItem.mValue[i] = dataInputStream.readByte();
				}
			}
			dataInputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void SaveIndexHelp()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeShort((short)GameScreen.indexHelp);
			GlobalService.gI().Save_RMS_Server(1, 4, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void SaveAutoFire()
	{
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
		try
		{
			dataOutputStream.writeByte(Player.typeAutoFireMain);
			if (MsgAutoFire.value == null)
			{
				dataOutputStream.writeByte(0);
			}
			else
			{
				dataOutputStream.writeByte((sbyte)MsgAutoFire.value.Length);
				for (int i = 0; i < MsgAutoFire.value.Length; i++)
				{
					dataOutputStream.writeShort(MsgAutoFire.value[i][0]);
					dataOutputStream.writeByte((sbyte)MsgAutoFire.value[i][1]);
				}
			}
			dataOutputStream.writeByte(Player.AutoRevice);
			GlobalService.gI().Save_RMS_Server(1, 5, byteArrayOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	public void loadAutoFire(sbyte[] bData)
	{
		if (bData == null)
		{
			return;
		}
		try
		{
			ByteArrayInputStream bis = new ByteArrayInputStream(bData);
			DataInputStream dataInputStream = new DataInputStream(bis);
			Player.typeAutoFireMain = dataInputStream.readByte();
			if (Player.typeAutoFireMain == -1)
			{
				Player.AutoFireCur = -1;
			}
			mSystem.outz("Player autofire type " + Player.typeAutoFireMain + " AutoFireCur " + Player.AutoFireCur);
			Player.typeAutoBuff = 0;
			sbyte b = dataInputStream.readByte();
			if (b > 0)
			{
				MsgAutoFire.value = new short[b][];
				for (int i = 0; i < b; i++)
				{
					MsgAutoFire.value[i] = new short[2];
					MsgAutoFire.value[i][0] = dataInputStream.readShort();
					MsgAutoFire.value[i][1] = dataInputStream.readByte();
					if (MsgAutoFire.value[i][1] == 1)
					{
						Player.typeAutoBuff = 1;
					}
				}
			}
			Player.AutoRevice = dataInputStream.readByte();
			dataInputStream.close();
		}
		catch (Exception)
		{
		}
		if (Player.AutoRevice == 1)
		{
			Interface_Game.addInfoPlayerNormal(T.dangbatauto, mFont.tahoma_7_yellow);
		}
	}
}
