using System;

public class MainDataUpgrade
{
	public short[][] mMaterial;

	public int beli;

	public int beli_white_green;

	public short ruby;

	public short per;

	public short perAtt;

	public sbyte lv;

	public sbyte preLv;

	public static void LoadDataUpgrade(DataInputStream iss, bool isSave)
	{
		if (iss == null)
		{
			GlobalService.gI().get_DATA(12);
			return;
		}
		try
		{
			sbyte b = iss.readByte();
			ScreenUpgrade.mTemMaterialUpgrade = new MainDataUpgrade[b];
			for (int i = 0; i < b; i++)
			{
				ScreenUpgrade.mTemMaterialUpgrade[i] = new MainDataUpgrade();
				ScreenUpgrade.mTemMaterialUpgrade[i].lv = iss.readByte();
				ScreenUpgrade.mTemMaterialUpgrade[i].per = iss.readShort();
				ScreenUpgrade.mTemMaterialUpgrade[i].preLv = iss.readByte();
				ScreenUpgrade.mTemMaterialUpgrade[i].beli = iss.readInt();
				ScreenUpgrade.mTemMaterialUpgrade[i].beli_white_green = iss.readInt();
				ScreenUpgrade.mTemMaterialUpgrade[i].ruby = iss.readShort();
				ScreenUpgrade.mTemMaterialUpgrade[i].perAtt = iss.readShort();
				sbyte b2 = iss.readByte();
				ScreenUpgrade.mTemMaterialUpgrade[i].mMaterial = new short[b2][];
				for (int j = 0; j < b2; j++)
				{
					ScreenUpgrade.mTemMaterialUpgrade[i].mMaterial[j] = new short[3];
					ScreenUpgrade.mTemMaterialUpgrade[i].mMaterial[j][2] = iss.readByte();
					ScreenUpgrade.mTemMaterialUpgrade[i].mMaterial[j][0] = iss.readByte();
					ScreenUpgrade.mTemMaterialUpgrade[i].mMaterial[j][1] = iss.readShort();
				}
			}
			if (isSave)
			{
				GlobalService.VerDataUpgrade = iss.readShort();
				SaveRms.saveVer(GlobalService.VerDataUpgrade, "VerdataUpgradeSave");
			}
			iss.close();
		}
		catch (Exception)
		{
		}
	}
}
