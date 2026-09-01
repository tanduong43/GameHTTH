using System;

public class Quest_Potion : MainItem
{
	public Quest_Potion(sbyte typeItem, short ID, string name)
		: base(typeItem, ID, ID, name, 0)
	{
		indexSort = 2;
		isTrade = 1;
		namepaint = name;
	}

	public override void addInfo(string str, sbyte color, sbyte colorMain)
	{
		vecInfo.addElement(new infoShow(-1, 0, str, color, colorMain));
	}

	public override void paint(mGraphics g, int x, int y, int w)
	{
		base.paint(g, x, y, w);
		paintPotion(g, x, y, w);
	}

	public override MainImage getImage()
	{
		MainImage result = null;
		try
		{
			result = ObjectData.getImageAll(idIcon, ObjectData.hashImageQuestPotion, 6000);
		}
		catch (Exception)
		{
		}
		return result;
	}
}
