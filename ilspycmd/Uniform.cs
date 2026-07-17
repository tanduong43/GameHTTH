public class Uniform
{
	public sbyte index = -1;

	public sbyte isSet;

	public short[] mIdUniform;

	public void setUniform(sbyte index)
	{
		this.index = index;
		mIdUniform = new short[8];
		for (int i = 0; i < mIdUniform.Length; i++)
		{
			MainItem mainItem = (MainItem)GameScreen.player.hashEquip.get(string.Empty + i);
			if (mainItem != null)
			{
				mIdUniform[i] = mainItem.ID;
			}
			else
			{
				mIdUniform[i] = -1;
			}
		}
		for (int j = 0; j < Player.vecUniform.size(); j++)
		{
			Uniform uniform = (Uniform)Player.vecUniform.elementAt(j);
			uniform.isSet = 0;
		}
		isSet = 1;
		Interface_Game.addInfoPlayerNormal(T.mColorUniform[index] + T.settingEquipOk, mFont.tahoma_7_yellow);
	}

	public void getUniform()
	{
		for (int i = 0; i < mIdUniform.Length; i++)
		{
			if (mIdUniform[i] < 0)
			{
				continue;
			}
			bool flag = false;
			for (int j = 0; j < Player.vecInventory.size(); j++)
			{
				if (flag)
				{
					break;
				}
				MainItem mainItem = (MainItem)Player.vecInventory.elementAt(j);
				if (mainItem.typeObject == 3 && mainItem.ID == mIdUniform[i])
				{
					GlobalService.gI().Use_Item(mIdUniform[i], 3);
					flag = true;
				}
			}
		}
		isSet = 1;
		Interface_Game.addInfoPlayerNormal(T.mColorUniform[index] + T.changeEquipOk, mFont.tahoma_7_yellow);
	}

	public static void checkIndexItem(bool isRemove)
	{
		if (Player.vecUniform.size() == 0 && !isRemove)
		{
			return;
		}
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			mainItem.indexUniform = -1;
			if (isRemove || mainItem.typeObject != 3)
			{
				continue;
			}
			for (int j = 0; j < Player.vecUniform.size(); j++)
			{
				Uniform uniform = (Uniform)Player.vecUniform.elementAt(j);
				for (int k = 0; k < uniform.mIdUniform.Length; k++)
				{
					if (mainItem.ID == uniform.mIdUniform[k])
					{
						if (mainItem.indexUniform == -1)
						{
							mainItem.indexUniform = uniform.index;
						}
						else
						{
							mainItem.indexUniform = 2;
						}
					}
				}
			}
		}
		for (int l = 0; l < 8; l++)
		{
			MainItem mainItem2 = (MainItem)GameScreen.player.hashEquip.get(string.Empty + l);
			if (mainItem2 == null)
			{
				continue;
			}
			mainItem2.indexUniform = -1;
			if (isRemove || mainItem2.typeObject != 3)
			{
				continue;
			}
			for (int m = 0; m < Player.vecUniform.size(); m++)
			{
				Uniform uniform2 = (Uniform)Player.vecUniform.elementAt(m);
				for (int n = 0; n < uniform2.mIdUniform.Length; n++)
				{
					if (mainItem2.ID == uniform2.mIdUniform[n])
					{
						if (mainItem2.indexUniform == -1)
						{
							mainItem2.indexUniform = uniform2.index;
						}
						else
						{
							mainItem2.indexUniform = 2;
						}
					}
				}
			}
		}
	}
}
