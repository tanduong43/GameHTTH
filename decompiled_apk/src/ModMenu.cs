using System;
using UnityEngine;

public class ModMenu
{
	public class ModAction : IAction
	{
		private Action m_action;

		public ModAction(Action action)
		{
			m_action = action;
		}

		public void perform()
		{
			if (m_action != null)
			{
				m_action();
			}
		}
	}

	public static bool isAutoOpenChest = false;

	public static bool isAutoUseItem = false;

	public static bool isAutoQuaySo = false;

	public static bool isAutoGomQuai = false;

	public static bool isAutoDanh = false;

	public static float gameSpeed = 1f;

	public static string selectedChestName = "";

	public static string selectedUseItemName = "";

	public static int autoOpenInterval = 3;

	public static int autoUseItemInterval = 3;

	public static int autoOpenChestLimit = 0;

	public static int autoOpenChestCount = 0;

	public static int autoUseItemLimit = 0;

	public static int autoUseItemCount = 0;

	public static int initialUseItemTotal = 0;

	public static int initialOpenChestTotal = 0;

	public static int offsetYName = 0;

	private static int tickOpenChest = 0;

	private static int tickUseItem = 0;

	private static int tickQuaySo = 0;

	private static int tickAutoDanh = 0;

	private static InputDialog currentInputDialog;

	public static int gomQuaiDistance = 45;

	public static void ShowMenu()
	{
		mVector mVector2 = new mVector();
		mVector2.addElement(new iCommand("Tốc Độ Game (" + gameSpeed + "x)", new ModAction(ShowSpeedMenu)));
		mVector2.addElement(new iCommand("Auto Gom Quái: " + (isAutoGomQuai ? "BẬT" : "TẮT"), new ModAction(delegate
		{
			isAutoGomQuai = !isAutoGomQuai;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Gom Quái: " + (isAutoGomQuai ? "BẬT" : "TẮT");
			}
			ShowMenu();
		})));
		mVector2.addElement(new iCommand("Auto Đánh: " + (isAutoDanh ? "BẬT" : "TẮT"), new ModAction(delegate
		{
			isAutoDanh = !isAutoDanh;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Đánh: " + (isAutoDanh ? "BẬT" : "TẮT");
			}
			ShowMenu();
		})));
		string text = "TẮT";
		if (isAutoUseItem)
		{
			int totalItemCount = GetTotalItemCount(selectedUseItemName);
			int num = Math.Max(0, initialUseItemTotal - totalItemCount);
			string text2 = ((autoUseItemLimit > 0) ? (" " + num + "/" + autoUseItemLimit) : (" (Còn " + totalItemCount + ")"));
			text = "BẬT (" + selectedUseItemName + text2 + ")";
		}
		mVector2.addElement(new iCommand("Auto Dùng Item: " + text, new ModAction(ShowUseItemSelectMenu)));
		string text3 = "TẮT";
		if (isAutoOpenChest)
		{
			int totalChestCount = GetTotalChestCount(selectedChestName);
			int num2 = Math.Max(0, initialOpenChestTotal - totalChestCount);
			string text4 = (string.IsNullOrEmpty(selectedChestName) ? "Tất cả" : selectedChestName);
			string text5 = ((autoOpenChestLimit > 0) ? (" " + num2 + "/" + autoOpenChestLimit) : (" (Còn " + totalChestCount + ")"));
			text3 = "BẬT (" + text4 + text5 + ")";
		}
		mVector2.addElement(new iCommand("Auto Mở Rương: " + text3, new ModAction(ShowChestSelectMenu)));
		mVector2.addElement(new iCommand("Auto Vòng Quay May Mắn: " + (isAutoQuaySo ? "BẬT" : "TẮT"), new ModAction(delegate
		{
			isAutoQuaySo = !isAutoQuaySo;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Vòng Quay May Mắn: " + (isAutoQuaySo ? "BẬT" : "TẮT");
			}
			ShowMenu();
		})));
		mVector2.addElement(new iCommand("Auto NV Lặp: " + (GameScreen.isOnRepeatQuest ? "BẬT" : "TẮT"), new ModAction(delegate
		{
			if (GameScreen.isOnRepeatQuest)
			{
				AutoRepeatQuest.stopAuto();
				GameScreen.isOnRepeatQuest = false;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Auto NV Lặp: TẮT";
				}
			}
			else
			{
				GameScreen.isOnRepeatQuest = true;
				if (AutoRepeatQuest.fBeginAuto())
				{
					AutoRepeatQuest.isStart = true;
				}
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Auto NV Lặp: BẬT";
				}
			}
			ShowMenu();
		})));
		mVector2.addElement(new iCommand("Auto Super Boss: " + (GameScreen.isOnSuperBoss ? "BẬT" : "TẮT"), new ModAction(delegate
		{
			if (GameScreen.isOnSuperBoss)
			{
				AutoSuperBoss.stopAuto();
				GameScreen.isOnSuperBoss = false;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Auto Super Boss: TẮT";
				}
			}
			else
			{
				GameScreen.isOnSuperBoss = true;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Auto Super Boss: BẬT";
				}
			}
			ShowMenu();
		})));
		mVector2.addElement(new iCommand("Độ Cao Tên NV: +" + offsetYName + "px", new ModAction(ShowNameHeightMenu)));
		GameCanvas.menu.startAt(mVector2, 2, "MENU MOD HACK");
	}

	private static void ShowNameHeightMenu()
	{
		mVector mVector2 = new mVector();
		int[] array = new int[8] { 0, 10, 15, 20, 25, 30, 40, 50 };
		foreach (int num in array)
		{
			int currentH = num;
			string caption = ((currentH == 0) ? "Mặc Định (0px)" : ("Nâng Cao +" + currentH + "px"));
			mVector2.addElement(new iCommand(caption, new ModAction(delegate
			{
				offsetYName = currentH;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Độ cao tên: +" + currentH + "px";
				}
				ShowMenu();
			})));
		}
		GameCanvas.menu.startAt(mVector2, 2, "CHỌN ĐỘ CAO TÊN NV");
	}

	private static void ShowSpeedMenu()
	{
		mVector mVector2 = new mVector();
		float[] array = new float[9] { 1f, 1.25f, 1.5f, 2f, 2.5f, 3f, 4f, 5f, 10f };
		foreach (float num in array)
		{
			float currentSpeed = num;
			mVector2.addElement(new iCommand(currentSpeed + "x", new ModAction(delegate
			{
				gameSpeed = currentSpeed;
				Time.timeScale = currentSpeed;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Tốc độ game: " + currentSpeed + "x";
				}
				ShowMenu();
			})));
		}
		GameCanvas.menu.startAt(mVector2, 2, "CHỌN TỐC ĐỘ");
	}

	public static bool IsChestItem(MainItem item)
	{
		if (item == null || string.IsNullOrEmpty(item.name))
		{
			return false;
		}
		string text = item.name.ToLower();
		string text2 = ((item.namepaint != null) ? item.namepaint.ToLower() : "");
		if (!text.Contains("rương") && !text.Contains("ruong") && !text.Contains("hòm") && !text.Contains("hom") && !text2.Contains("rương") && !text2.Contains("ruong") && !text2.Contains("hòm"))
		{
			return text2.Contains("hom");
		}
		return true;
	}

	public static int GetTotalChestCount(string chestName)
	{
		if (Player.vecInventory == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem != null && IsChestItem(mainItem) && (string.IsNullOrEmpty(chestName) || mainItem.name == chestName))
			{
				num += ((mainItem.numPotion <= 0) ? 1 : mainItem.numPotion);
			}
		}
		return num;
	}

	public static void ShowChestSelectMenu()
	{
		mVector mVector2 = new mVector();
		if (isAutoOpenChest)
		{
			mVector2.addElement(new iCommand("TẮT AUTO MỞ RƯƠNG", new ModAction(delegate
			{
				isAutoOpenChest = false;
				selectedChestName = "";
				autoOpenChestLimit = 0;
				autoOpenChestCount = 0;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Auto Mở Rương: TẮT";
				}
				ShowMenu();
			})));
		}
		mVector2.addElement(new iCommand("Mở tất cả các rương", new ModAction(delegate
		{
			ShowInputChestQuantity("");
		})));
		if (Player.vecInventory != null)
		{
			mVector mVector3 = new mVector();
			for (int num = 0; num < Player.vecInventory.size(); num++)
			{
				MainItem mainItem = (MainItem)Player.vecInventory.elementAt(num);
				if (mainItem == null || !IsChestItem(mainItem) || string.IsNullOrEmpty(mainItem.name))
				{
					continue;
				}
				bool flag = false;
				for (int num2 = 0; num2 < mVector3.size(); num2++)
				{
					if ((string)mVector3.elementAt(num2) == mainItem.name)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					mVector3.addElement(mainItem.name);
				}
			}
			for (int num3 = 0; num3 < mVector3.size(); num3++)
			{
				string name = (string)mVector3.elementAt(num3);
				mVector2.addElement(new iCommand(name, new ModAction(delegate
				{
					ShowInputChestQuantity(name);
				})));
			}
		}
		if (mVector2.size() == 0 || (mVector2.size() == 1 && isAutoOpenChest))
		{
			mVector2.addElement(new iCommand("Không có rương trong túi đồ", new ModAction(ShowMenu)));
		}
		else
		{
			mVector2.addElement(new iCommand("Quay lại", new ModAction(ShowMenu)));
		}
		GameCanvas.menu.startAt(mVector2, 2, "CHỌN RƯƠNG ĐỂ AUTO MỞ");
	}

	public static void ShowInputChestQuantity()
	{
		ShowInputChestQuantity(selectedChestName);
	}

	public static void ShowInputChestQuantity(string chestName)
	{
		iCommand cmd = new iCommand("Xác nhận", new ModAction(delegate
		{
			int num = 0;
			if (currentInputDialog != null && currentInputDialog.tfInput != null)
			{
				try
				{
					string text = currentInputDialog.tfInput.getText();
					num = ((!string.IsNullOrEmpty(text)) ? int.Parse(text) : 0);
					if (num < 0)
					{
						num = 0;
					}
				}
				catch (Exception)
				{
					num = 0;
				}
			}
			GameCanvas.end_Dialog();
			selectedChestName = chestName;
			initialOpenChestTotal = GetTotalChestCount(chestName);
			autoOpenChestLimit = num;
			autoOpenChestCount = 0;
			isAutoOpenChest = true;
			autoOpenInterval = 3;
			if (GameScreen.player != null)
			{
				string text2 = ((num == 0) ? "đến khi hết" : (num + " rương"));
				string text3 = (string.IsNullOrEmpty(selectedChestName) ? "tất cả rương" : selectedChestName);
				GameScreen.player.strChatPopup = "Auto mở (" + text2 + "): " + text3;
			}
		}));
		string name = (string.IsNullOrEmpty(chestName) ? "Tất cả các rương" : chestName);
		currentInputDialog = GameCanvas.Start_Input_Dialog("Nhập số lượng rương muốn mở (0 = hết):", cmd, isNum: true, name);
		GameCanvas.currentDialog = currentInputDialog;
	}

	public static bool IsUsableItem(MainItem item)
	{
		if (item == null || string.IsNullOrEmpty(item.name))
		{
			return false;
		}
		if (item.typeObject != 4 && item.typeObject != 8)
		{
			return false;
		}
		if (item is MainMaterial || item is Item || item is ItemFashion || item is ItemHead || item is ItemHair || item is ItemHuyHieu || item is ItemBoat || item is Skill_Info)
		{
			return false;
		}
		string text = item.name.ToLower();
		string text2 = ((item.namepaint != null) ? item.namepaint.ToLower() : "");
		string text3 = ((!string.IsNullOrEmpty(item.nameUse)) ? item.nameUse.Trim().ToLower() : "");
		if (text.Contains("mảnh") || text.Contains("manh") || text.Contains("mãnh") || text2.Contains("mảnh") || text2.Contains("manh") || text2.Contains("mãnh") || text.Contains("ghép") || text.Contains("ghep") || text2.Contains("ghép") || text2.Contains("ghep") || text.Contains("đá") || text.Contains("da ") || text.StartsWith("da") || text2.Contains("đá") || text2.Contains("da ") || text.Contains("nguyên liệu") || text.Contains("nguyen lieu") || text.Contains("công thức") || text.Contains("cong thuc") || text.Contains("bản vẽ") || text.Contains("ban ve") || text.Contains("tinh thể") || text.Contains("tinh the") || text.Contains("khoáng") || text.Contains("khoang") || text.Contains("phôi") || text.Contains("phoi"))
		{
			return false;
		}
		if (text3.Contains("ghép") || text3.Contains("ghep") || text3.Contains("khảm") || text3.Contains("kham") || text3.Contains("hợp") || text3.Contains("hop") || text3.Contains("cường hóa") || text3.Contains("cuong hoa") || text3.Contains("đập") || text3.Contains("dap"))
		{
			return false;
		}
		if (!string.IsNullOrEmpty(text3) && text3 != "null")
		{
			return true;
		}
		if (text.Contains("exp") || text.Contains("chiêu thức") || text.Contains("chieu thuc") || text.Contains("pháo") || text.Contains("phao") || text.Contains("khóa") || text.Contains("khoa") || text.Contains("thức ăn") || text.Contains("thuc an") || text.Contains("bình") || text.Contains("binh") || text.Contains("thuốc") || text.Contains("thuoc") || text.Contains("thẻ") || text.Contains("the") || text.Contains("bùa") || text.Contains("bua") || text2.Contains("exp") || text2.Contains("chiêu thức") || text2.Contains("chieu thuc") || text2.Contains("pháo") || text2.Contains("phao"))
		{
			return true;
		}
		return false;
	}

	public static void ShowUseItemSelectMenu()
	{
		mVector mVector2 = new mVector();
		if (isAutoUseItem)
		{
			mVector2.addElement(new iCommand("TẮT AUTO DÙNG ITEM", new ModAction(delegate
			{
				isAutoUseItem = false;
				selectedUseItemName = "";
				autoUseItemLimit = 0;
				autoUseItemCount = 0;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Auto Dùng Item: TẮT";
				}
				ShowMenu();
			})));
		}
		if (Player.vecInventory != null)
		{
			mVector mVector3 = new mVector();
			for (int num = 0; num < Player.vecInventory.size(); num++)
			{
				MainItem mainItem = (MainItem)Player.vecInventory.elementAt(num);
				if (!IsUsableItem(mainItem))
				{
					continue;
				}
				bool flag = false;
				for (int num2 = 0; num2 < mVector3.size(); num2++)
				{
					if ((string)mVector3.elementAt(num2) == mainItem.name)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					mVector3.addElement(mainItem.name);
				}
			}
			for (int num3 = 0; num3 < mVector3.size(); num3++)
			{
				string itemName = (string)mVector3.elementAt(num3);
				mVector2.addElement(new iCommand(itemName, new ModAction(delegate
				{
					ShowInputUseItemQuantity(itemName);
				})));
			}
		}
		if (mVector2.size() == 0 || (mVector2.size() == 1 && isAutoUseItem))
		{
			mVector2.addElement(new iCommand("Không có item khả dụng", new ModAction(ShowMenu)));
		}
		else
		{
			mVector2.addElement(new iCommand("Quay lại", new ModAction(ShowMenu)));
		}
		GameCanvas.menu.startAt(mVector2, 2, "CHỌN ITEM ĐỂ AUTO DÙNG");
	}

	public static int GetTotalItemCount(string itemName)
	{
		if (Player.vecInventory == null || string.IsNullOrEmpty(itemName))
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem != null && mainItem.name == itemName && IsUsableItem(mainItem))
			{
				num += ((mainItem.numPotion <= 0) ? 1 : mainItem.numPotion);
			}
		}
		return num;
	}

	public static void ShowInputUseItemQuantity(string itemName)
	{
		iCommand cmd = new iCommand("Xác nhận", new ModAction(delegate
		{
			int num = 0;
			if (currentInputDialog != null && currentInputDialog.tfInput != null)
			{
				try
				{
					string text = currentInputDialog.tfInput.getText();
					num = ((!string.IsNullOrEmpty(text)) ? int.Parse(text) : 0);
					if (num < 0)
					{
						num = 0;
					}
				}
				catch (Exception)
				{
					num = 0;
				}
			}
			GameCanvas.end_Dialog();
			selectedUseItemName = itemName;
			initialUseItemTotal = GetTotalItemCount(itemName);
			autoUseItemLimit = num;
			autoUseItemCount = 0;
			isAutoUseItem = true;
			autoUseItemInterval = 3;
			if (GameScreen.player != null)
			{
				string text2 = ((num == 0) ? "đến khi hết" : (num + " cái"));
				GameScreen.player.strChatPopup = "Auto dùng (" + text2 + "): " + itemName;
			}
		}));
		currentInputDialog = GameCanvas.Start_Input_Dialog("Nhập số lượng cần dùng (0 = hết):", cmd, isNum: true, itemName);
		GameCanvas.currentDialog = currentInputDialog;
	}

	private static string GetSpeedText(int interval)
	{
		if (interval <= 2)
		{
			return "Siêu nhanh";
		}
		if (interval <= 5)
		{
			return "Nhanh";
		}
		if (interval <= 15)
		{
			return "Vừa";
		}
		return "Chậm";
	}

	public static void Update()
	{
		if (Time.timeScale != gameSpeed)
		{
			Time.timeScale = gameSpeed;
		}
		if (isAutoOpenChest)
		{
			tickOpenChest++;
			if (tickOpenChest >= autoOpenInterval)
			{
				tickOpenChest = 0;
				PerformAutoOpenChest();
			}
		}
		else
		{
			tickOpenChest = 0;
		}
		if (isAutoUseItem)
		{
			tickUseItem++;
			if (tickUseItem >= autoUseItemInterval)
			{
				tickUseItem = 0;
				PerformAutoUseItem();
			}
		}
		else
		{
			tickUseItem = 0;
		}
		if (isAutoQuaySo)
		{
			tickQuaySo++;
			if (tickQuaySo >= 3)
			{
				tickQuaySo = 0;
				PerformAutoQuaySo();
			}
		}
		else
		{
			tickQuaySo = 0;
		}
		if (isAutoGomQuai)
		{
			PerformAutoGomQuai();
		}
		if (isAutoDanh)
		{
			tickAutoDanh++;
			if (tickAutoDanh >= 3)
			{
				tickAutoDanh = 0;
				PerformAutoDanh();
			}
		}
		else
		{
			tickAutoDanh = 0;
		}
	}

	private static void PerformAutoGomQuai()
	{
		if (GameScreen.player == null || GameScreen.vecPlayers == null)
		{
			return;
		}
		int x = GameScreen.player.x;
		int y = GameScreen.player.y;
		int dir = GameScreen.player.Dir;
		int num = x;
		int num2 = y;
		switch (dir)
		{
		case 0:
			num = x - gomQuaiDistance;
			break;
		case 2:
			num = x + gomQuaiDistance;
			break;
		case 1:
			num2 = y - gomQuaiDistance;
			break;
		case 3:
			num2 = y + gomQuaiDistance;
			break;
		default:
			num = x + gomQuaiDistance;
			break;
		}
		int dir2 = 0;
		switch (dir)
		{
		case 0:
			dir2 = 2;
			break;
		case 2:
			dir2 = 0;
			break;
		case 1:
			dir2 = 3;
			break;
		case 3:
			dir2 = 1;
			break;
		}
		for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
		{
			MainObject mainObject = (MainObject)GameScreen.vecPlayers.elementAt(i);
			if (mainObject != null && mainObject.typeObject == 1 && !mainObject.isDie && mainObject.Hp > 0 && mainObject.Action != 4)
			{
				mainObject.x = num;
				mainObject.y = num2;
				mainObject.toX = num;
				mainObject.toY = num2;
				mainObject.vx = 0;
				mainObject.vy = 0;
				mainObject.Dir = dir2;
			}
		}
	}

	private static void PerformAutoDanh()
	{
		if (GameScreen.player == null || GameScreen.player.Hp <= 0 || GameScreen.player.Action == 4 || GameScreen.vecPlayers == null)
		{
			return;
		}
		if (GameScreen.objFocus == null || GameScreen.objFocus.isDie || GameScreen.objFocus.Hp <= 0 || GameScreen.objFocus.Action == 4 || GameScreen.objFocus.typeObject != 1)
		{
			MainObject mainObject = null;
			int num = 999999;
			for (int i = 0; i < GameScreen.vecPlayers.size(); i++)
			{
				MainObject mainObject2 = (MainObject)GameScreen.vecPlayers.elementAt(i);
				if (mainObject2 != null && mainObject2.typeObject == 1 && !mainObject2.isDie && mainObject2.Hp > 0 && mainObject2.Action != 4 && !mainObject2.isSend)
				{
					int distance = MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, mainObject2.x, mainObject2.y);
					if (distance < num)
					{
						num = distance;
						mainObject = mainObject2;
					}
				}
			}
			if (mainObject != null)
			{
				GameScreen.objFocus = mainObject;
			}
		}
		if (GameScreen.objFocus == null || GameScreen.objFocus.typeObject != 1 || GameScreen.objFocus.isDie || GameScreen.objFocus.Hp <= 0 || GameScreen.objFocus.Action == 4)
		{
			return;
		}
		if (MainObject.getDistance(GameScreen.player.x, GameScreen.player.y, GameScreen.objFocus.x, GameScreen.objFocus.y) > Player.wFocus && !isAutoGomQuai)
		{
			GameScreen.player.toX = GameScreen.objFocus.x;
			GameScreen.player.toY = GameScreen.objFocus.y;
			GameScreen.player.isMoveNor = true;
			return;
		}
		GameScreen.player.beginPlayerFirePoint();
		if (GameScreen.player.Action != 2)
		{
			GameScreen.player.beginPlayerFire(2);
		}
	}

	private static void PerformAutoOpenChest()
	{
		if (Player.vecInventory == null)
		{
			isAutoOpenChest = false;
			return;
		}
		int totalChestCount = GetTotalChestCount(selectedChestName);
		if (totalChestCount <= 0)
		{
			isAutoOpenChest = false;
			if (GameScreen.player != null)
			{
				int num = Math.Max(0, initialOpenChestTotal - totalChestCount);
				string text = (string.IsNullOrEmpty(selectedChestName) ? "rương" : selectedChestName);
				GameScreen.player.strChatPopup = "Hết " + text + ((autoOpenChestLimit > 0) ? (" (Đã mở " + num + "/" + autoOpenChestLimit + ")") : " (Xong)");
			}
			return;
		}
		if (autoOpenChestLimit > 0 && initialOpenChestTotal - totalChestCount >= autoOpenChestLimit)
		{
			isAutoOpenChest = false;
			if (GameScreen.player != null)
			{
				string text2 = (string.IsNullOrEmpty(selectedChestName) ? "rương" : selectedChestName);
				GameScreen.player.strChatPopup = "Đã mở đủ " + autoOpenChestLimit + " " + text2 + " (Xong)!";
			}
			return;
		}
		MainItem mainItem = null;
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem2 = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem2 != null && IsChestItem(mainItem2) && (string.IsNullOrEmpty(selectedChestName) || mainItem2.name == selectedChestName))
			{
				mainItem = mainItem2;
				break;
			}
		}
		if (mainItem == null)
		{
			return;
		}
		if (mainItem.typeObject == 4 || mainItem.typeObject == 8)
		{
			mainItem.Use_Item();
		}
		else
		{
			GlobalService.gI().Use_Item(mainItem.ID, mainItem.typeObject);
		}
		if (GameScreen.player != null)
		{
			int num2 = Math.Max(0, initialOpenChestTotal - totalChestCount);
			if (autoOpenChestLimit > 0)
			{
				GameScreen.player.strChatPopup = "Auto mở: " + mainItem.name + " (" + num2 + "/" + autoOpenChestLimit + ")";
			}
			else
			{
				GameScreen.player.strChatPopup = "Auto mở: " + mainItem.name + " (Còn " + totalChestCount + ")";
			}
		}
	}

	private static void PerformAutoUseItem()
	{
		if (GameCanvas.currentDialog is MsgShowGift)
		{
			GameCanvas.end_Dialog();
		}
		if (GameCanvas.subDialog is MsgShowGift)
		{
			GameCanvas.subDialog = null;
		}
		if (Player.vecInventory == null || string.IsNullOrEmpty(selectedUseItemName))
		{
			isAutoUseItem = false;
			return;
		}
		int totalItemCount = GetTotalItemCount(selectedUseItemName);
		if (totalItemCount <= 0)
		{
			isAutoUseItem = false;
			if (GameScreen.player != null)
			{
				int num = Math.Max(0, initialUseItemTotal - totalItemCount);
				GameScreen.player.strChatPopup = "Hết vật phẩm: " + selectedUseItemName + ((autoUseItemLimit > 0) ? (" (Đã dùng " + num + "/" + autoUseItemLimit + ")") : " (Xong)");
			}
			return;
		}
		if (autoUseItemLimit > 0 && initialUseItemTotal - totalItemCount >= autoUseItemLimit)
		{
			isAutoUseItem = false;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Đã dùng đủ " + autoUseItemLimit + " " + selectedUseItemName + " (Xong)!";
			}
			return;
		}
		MainItem mainItem = null;
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem2 = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem2 != null && mainItem2.name == selectedUseItemName && IsUsableItem(mainItem2))
			{
				mainItem = mainItem2;
				break;
			}
		}
		if (mainItem == null)
		{
			return;
		}
		if (mainItem is Potion { indexHotKey: >=0 } potion)
		{
			DelaySkill delay = DelaySkill.getDelay(potion.indexHotKey);
			if (delay != null)
			{
				delay.value = 0;
			}
		}
		if (mainItem.typeObject == 4 || mainItem.typeObject == 8)
		{
			mainItem.Use_Item();
		}
		else
		{
			GlobalService.gI().Use_Item(mainItem.ID, mainItem.typeObject);
		}
		if (GameScreen.player != null)
		{
			int num2 = Math.Max(0, initialUseItemTotal - totalItemCount);
			if (autoUseItemLimit > 0)
			{
				GameScreen.player.strChatPopup = "Auto dùng: " + mainItem.name + " (" + num2 + "/" + autoUseItemLimit + ")";
			}
			else
			{
				GameScreen.player.strChatPopup = "Auto dùng: " + mainItem.name + " (Còn " + totalItemCount + ")";
			}
		}
	}

	private static void PerformAutoQuaySo()
	{
		if (GameCanvas.currentDialog is MsgShowGift)
		{
			GameCanvas.end_Dialog();
		}
		if (GameCanvas.subDialog is MsgShowGift)
		{
			GameCanvas.subDialog = null;
		}
		int num = 0;
		if (Player.vecInventory != null)
		{
			for (int i = 0; i < Player.vecInventory.size(); i++)
			{
				MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
				if (mainItem != null && mainItem.typeObject == 4 && (mainItem.ID == 232 || (mainItem.name != null && (mainItem.name.ToLower().Contains("vé quay") || mainItem.name.ToLower().Contains("ve quay")))))
				{
					num += mainItem.numPotion;
				}
			}
		}
		if (num <= 0)
		{
			isAutoQuaySo = false;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Hết vé quay may mắn (Tự động TẮT Auto)!";
			}
		}
		else if (num >= 3)
		{
			GlobalService.gI().Quayso(2);
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Vòng Quay (x3)... Còn " + num + " vé";
			}
		}
		else
		{
			GlobalService.gI().Quayso(1);
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Vòng Quay (x1)... Còn " + num + " vé";
			}
		}
	}
}
