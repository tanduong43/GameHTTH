using System;

public class TabChest : MainTabShop
{
	public static iCommand cmdRemove;

	public static iCommand cmdGetItem;

	public static iCommand cmdGetPotion;

	public static iCommand cmdGetAllMaterial;

	public static iCommand cmdUpgrade;

	public static iCommand cmdChucnang;

	public static iCommand cmdGetAllDiamond;

	public TabChest(string name, mVector vec, int xTab)
		: base(name, vec, Player.maxChest, xTab)
	{
		indexIconTab = 7;
		cmdUpgrade = new iCommand(T.morong, 8, this);
	}

	public void initCmd()
	{
		cmdRemove = new iCommand(T.del, 2, this);
		cmdGetItem = new iCommand(T.cmdget, 4, this);
		cmdGetPotion = new iCommand(T.cmdget, 11, this);
		cmdGetAllMaterial = new iCommand(T.laytatca, 7, this);
		cmdChucnang = new iCommand(T.cmdChucNang, 9, this);
		cmdMenu = new iCommand(T.select, 10, this);
		cmdGetAllDiamond = new iCommand(T.laytatcaDa, 13, this);
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 2:
			if (itemCur != null)
			{
				string info = GameMidlet.format(T.HoiRemoveENG, new string[2]
				{
					string.Empty + itemCur.numPotion,
					itemCur.name
				});
				GameCanvas.Start_Normal_DiaLog(info, new iCommand(T.remove, 3, 1, this), isCmdClose: true);
			}
			break;
		case 3:
			if (itemCur != null)
			{
				GlobalService.gI().Chest(4, itemCur.ID, itemCur.typeObject, 1);
			}
			break;
		case 4:
			if (itemCur != null)
			{
				GlobalService.gI().Chest(2, itemCur.ID, itemCur.typeObject, 1);
			}
			break;
		case 5:
			GameCanvas.end_Dialog();
			if (itemCur != null)
			{
				if (itemCur.numPotion == 1)
				{
					GlobalService.gI().Chest(2, itemCur.ID, itemCur.typeObject, 1);
				}
				else
				{
					input = GameCanvas.Start_Input_Dialog(T.nhapsoluong, new iCommand(T.cmdget, 6, 0, this), isNum: true, itemCur.namepaint);
				}
				GameCanvas.subDialog = input;
			}
			break;
		case 6:
		{
			if (itemCur == null)
			{
				break;
			}
			int num = 1;
			try
			{
				num = int.Parse(input.tfInput.getText());
				if (num < 0)
				{
					num = 1;
				}
			}
			catch (Exception)
			{
				num = 1;
			}
			MainItem itemVec = MainItem.getItemVec(itemCur.typeObject, itemCur.ID, Player.vecChest);
			if (itemVec != null)
			{
				if (num > itemVec.numPotion)
				{
					num = itemVec.numPotion;
				}
				GameCanvas.end_Dialog();
				GlobalService.gI().Chest(2, itemCur.ID, itemCur.typeObject, num);
			}
			break;
		}
		case 7:
			Player.SetMaterialToInven(7);
			break;
		case 8:
			GlobalService.gI().Chest(5, 0, 0, 0);
			break;
		case 9:
		{
			mVector mVector3 = new mVector();
			mVector3.addElement(cmdUpgrade);
			mVector3.addElement(cmdGetAllMaterial);
			mVector3.addElement(cmdGetAllDiamond);
			GameCanvas.menu.startAt(mVector3, 2, T.menu);
			break;
		}
		case 10:
		{
			mVector menuActionItem = getMenuActionItem();
			if (menuActionItem != null)
			{
				GameCanvas.menu.startAt(menuActionItem, 2, T.menu);
			}
			break;
		}
		case 11:
			if (itemCur != null)
			{
				if (itemCur.numPotion == 1)
				{
					GlobalService.gI().Chest(2, itemCur.ID, itemCur.typeObject, 1);
					break;
				}
				mVector mVector2 = new mVector();
				iCommand o = new iCommand(T.allMaterial, 12, 0, this);
				mVector2.addElement(o);
				iCommand o2 = new iCommand(T.soluong, 5, 0, this);
				mVector2.addElement(o2);
				GameCanvas.Start_Normal_DiaLog_New(T.muonlayrabaonhieu + itemCur.namepaint, mVector2, isCmdClose: true, itemCur.name);
			}
			break;
		case 12:
			if (itemCur != null)
			{
				if (subIndex == 0)
				{
					GlobalService.gI().Chest(2, itemCur.ID, itemCur.typeObject, itemCur.numPotion);
				}
				GameCanvas.end_Dialog();
			}
			break;
		case 13:
			Player.SetDiamondToInven();
			break;
		}
	}

	public override mVector getMenuActionItem()
	{
		mVector result = null;
		MainItem mainItem = (MainItem)Player.vecChest.elementAt(IdSelect);
		if (mainItem != null)
		{
			itemCur = mainItem;
			result = itemCur.getActionChest();
		}
		return result;
	}

	public override iCommand setCmdEndInfo()
	{
		return cmdUpgrade;
	}
}
