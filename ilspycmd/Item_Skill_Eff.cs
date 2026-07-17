public class Item_Skill_Eff : MainItem
{
	public Item_Skill_Eff(short idIcon, short id, int hardcode_Index)
	{
		indexHotKey = (short)(id + hardcode_Index);
		base.idIcon = idIcon;
		ID = id;
		typeObject = 9;
		if (hardcode_Index == 1500)
		{
			colorName = 6;
		}
		else
		{
			colorName = 0;
		}
	}

	public void setColorKickAn()
	{
		if (ID == 9 || ID == 10)
		{
			colorName = 1;
		}
		else
		{
			colorName = 6;
		}
	}

	public override void paintHotkey(mGraphics g, int x, int y, int w, int ylech)
	{
		MainImage mainImage = null;
		mainImage = getImage();
		if (mainImage != null && mainImage.img != null)
		{
			g.drawImage(mainImage.img, x, y, 3);
		}
		else
		{
			AvMain.imgLoadImage.drawFrame(GameCanvas.gameTick % AvMain.imgLoadImage.nFrame, x, y, 0, 3, g);
		}
	}

	public override MainImage getImage()
	{
		MainImage mainImage = null;
		return ObjectData.getImageAll(idIcon, ObjectData.hashImageSkillSmall, 4500);
	}
}
