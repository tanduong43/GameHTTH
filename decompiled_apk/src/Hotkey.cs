public class Hotkey
{
	public MainItem itemcur;

	public MainSkill skill;

	public void setPotion(MainItem item)
	{
		itemcur = item;
		skill = null;
	}

	public void setSkill(MainSkill skill, short IdIcon)
	{
		this.skill = skill;
		this.skill.idIcon = IdIcon;
		itemcur = null;
		Skill_Info skillFromID = Skill_Info.getSkillFromID(skill.ID);
		if (skillFromID.typeSkill == 2)
		{
			this.skill.setTypeBuff(1, 46, 0);
		}
		this.skill.lvDevil = skillFromID.LvDevilSkill;
	}

	public int getIndexDelay()
	{
		if (skill != null)
		{
			return skill.indexHotKey;
		}
		if (itemcur != null)
		{
			return itemcur.indexHotKey;
		}
		return -1;
	}

	public void paint(mGraphics g, int x, int y, int w)
	{
		if (skill != null)
		{
			skill.paint(g, x, y, skill.lvDevil);
		}
		else if (itemcur != null)
		{
			itemcur.paintHotkey(g, x, y, w, 0);
		}
	}

	public sbyte getTypeHotKey()
	{
		if (itemcur != null)
		{
			return 0;
		}
		if (skill != null)
		{
			return 1;
		}
		return -1;
	}

	public static void checkUpdatePotion(MainItem itemcheck)
	{
		for (int i = 0; i < Player.hotkeyPlayer.Length; i++)
		{
			for (int j = 0; j < Player.hotkeyPlayer[i].Length; j++)
			{
				if (Player.hotkeyPlayer[i][j].itemcur != null && Player.hotkeyPlayer[i][j].itemcur.typeObject == itemcheck.typeObject && Player.hotkeyPlayer[i][j].itemcur.ID == itemcheck.ID)
				{
					Player.hotkeyPlayer[i][j].itemcur = itemcheck;
				}
			}
		}
	}
}
