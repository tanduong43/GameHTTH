using System;
using UnityEngine;

public class ModMenu
{
	public static bool isAutoOpenChest = false;
	public static bool isAutoQuaySo = false;
	public static float gameSpeed = 1f;
	public static string selectedChestName = "";
	public static int autoOpenInterval = 5; // Default to "Nhanh" (5 ticks)

	private static int tickOpenChest = 0;
	private static int tickQuaySo = 0;

	// Custom implementation of IAction to handle menu item clicks cleanly
	public class ModAction : IAction
	{
		private Action _action;

		public ModAction(Action action)
		{
			_action = action;
		}

		public void perform()
		{
			if (_action != null)
			{
				_action();
			}
		}
	}

	public static void ShowMenu()
	{
		mVector menuItems = new mVector();

		// 1. Game Speed Option
		menuItems.addElement(new iCommand("Toc Do Game (" + gameSpeed + "x)", new ModAction(ShowSpeedMenu)));

		// 2. Auto Open Chest Option (Now opens the selection menu directly)
		string chestText = "TAT";
		if (isAutoOpenChest)
		{
			chestText = "BAT (" + (string.IsNullOrEmpty(selectedChestName) ? "Tat ca" : selectedChestName) + ")";
		}
		menuItems.addElement(new iCommand("Auto Mo Ruong: " + chestText, new ModAction(ShowChestSelectMenu)));

		// 3. Chest Speed Option
		menuItems.addElement(new iCommand("Toc Do Mo: " + GetSpeedText(autoOpenInterval), new ModAction(ShowChestSpeedMenu)));

		// 4. Auto Quay So Option
		menuItems.addElement(new iCommand("Auto Quay So: " + (isAutoQuaySo ? "BAT" : "TAT"), new ModAction(() => {
			isAutoQuaySo = !isAutoQuaySo;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto Quay So: " + (isAutoQuaySo ? "BAT" : "TAT");
			}
			ShowMenu();
		})));

		// Show the popup menu
		GameCanvas.menu.startAt(menuItems, 2, "MENU MOD HACK");
	}

	private static void ShowSpeedMenu()
	{
		mVector speedItems = new mVector();
		float[] speeds = new float[] { 1f, 1.5f, 2f, 3f, 5f, 10f };
		foreach (float s in speeds)
		{
			float currentSpeed = s;
			speedItems.addElement(new iCommand(currentSpeed + "x", new ModAction(() => {
				gameSpeed = currentSpeed;
				Time.timeScale = currentSpeed;
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Toc do game: " + currentSpeed + "x";
				}
				ShowMenu();
			})));
		}
		GameCanvas.menu.startAt(speedItems, 2, "CHON TOC DO");
	}

	private static void ShowChestSelectMenu()
	{
		mVector chestItems = new mVector();
		
		// If auto open is currently active, show a turn-off option
		if (isAutoOpenChest)
		{
			chestItems.addElement(new iCommand("TAT AUTO", new ModAction(() => {
				isAutoOpenChest = false;
				selectedChestName = "";
				if (GameScreen.player != null)
				{
					GameScreen.player.strChatPopup = "Auto Mo Ruong: TAT";
				}
				ShowMenu();
			})));
		}

		// Add "Tất cả các rương" option
		chestItems.addElement(new iCommand("Tat ca cac ruong", new ModAction(() => {
			selectedChestName = "";
			isAutoOpenChest = true;
			if (GameScreen.player != null)
			{
				GameScreen.player.strChatPopup = "Auto mo: Tat ca cac ruong";
			}
			ShowMenu();
		})));

		if (Player.vecInventory != null)
		{
			// Collect unique chest names
			mVector uniqueNames = new mVector();
			for (int i = 0; i < Player.vecInventory.size(); i++)
			{
				MainItem item = (MainItem)Player.vecInventory.elementAt(i);
				if (item != null)
				{
					string itemName = item.name != null ? item.name : "";
					string itemPaintName = item.namepaint != null ? item.namepaint : "";
					bool isChest = itemName.ToLower().Contains("rương") || itemName.ToLower().Contains("ruong") ||
								   itemPaintName.ToLower().Contains("rương") || itemPaintName.ToLower().Contains("ruong");
					
					if (isChest && !string.IsNullOrEmpty(item.name))
					{
						// Check if name is already added
						bool exists = false;
						for (int j = 0; j < uniqueNames.size(); j++)
						{
							if (((string)uniqueNames.elementAt(j)) == item.name)
							{
								exists = true;
								break;
							}
						}
						if (!exists)
						{
							uniqueNames.addElement(item.name);
						}
					}
				}
			}

			// Add unique chest names to the menu
			for (int i = 0; i < uniqueNames.size(); i++)
			{
				string name = (string)uniqueNames.elementAt(i);
				chestItems.addElement(new iCommand(name, new ModAction(() => {
					selectedChestName = name;
					isAutoOpenChest = true; // Auto turn on
					if (GameScreen.player != null)
					{
						GameScreen.player.strChatPopup = "Auto mo: " + name;
					}
					ShowMenu();
				})));
			}
		}

		GameCanvas.menu.startAt(chestItems, 2, "CHON RUONG DE AUTO");
	}

	private static void ShowChestSpeedMenu()
	{
		mVector speedItems = new mVector();
		
		speedItems.addElement(new iCommand("Sieu nhanh (~0.06s)", new ModAction(() => {
			autoOpenInterval = 2;
			ShowMenu();
		})));
		speedItems.addElement(new iCommand("Nhanh (~0.16s)", new ModAction(() => {
			autoOpenInterval = 5;
			ShowMenu();
		})));
		speedItems.addElement(new iCommand("Vua (~0.5s)", new ModAction(() => {
			autoOpenInterval = 15;
			ShowMenu();
		})));
		speedItems.addElement(new iCommand("Cham (~1.0s)", new ModAction(() => {
			autoOpenInterval = 30;
			ShowMenu();
		})));

		GameCanvas.menu.startAt(speedItems, 2, "TOC DO MO RUONG");
	}

	private static string GetSpeedText(int interval)
	{
		if (interval <= 2) return "Sieu nhanh";
		if (interval <= 5) return "Nhanh";
		if (interval <= 15) return "Vua";
		return "Cham";
	}

	public static void Update()
	{
		// Maintain game speed
		if (Time.timeScale != gameSpeed)
		{
			Time.timeScale = gameSpeed;
		}

		// Auto Open Chest
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

		// Auto Quay So
		if (isAutoQuaySo)
		{
			tickQuaySo++;
			if (tickQuaySo >= 50) // approx. 1.6 seconds at normal speed
			{
				tickQuaySo = 0;
				PerformAutoQuaySo();
			}
		}
		else
		{
			tickQuaySo = 0;
		}
	}

	private static void PerformAutoOpenChest()
	{
		if (Player.vecInventory == null) return;

		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem item = (MainItem)Player.vecInventory.elementAt(i);
			if (item != null)
			{
				string itemName = item.name != null ? item.name.ToLower() : "";
				string itemPaintName = item.namepaint != null ? item.namepaint.ToLower() : "";

				bool isChest = itemName.Contains("rương") || itemName.Contains("ruong") ||
							   itemPaintName.Contains("rương") || itemPaintName.Contains("ruong");

				if (isChest)
				{
					// If a specific chest is selected, check name match
					if (!string.IsNullOrEmpty(selectedChestName))
					{
						if (item.name != selectedChestName)
						{
							continue; // Skip this item as it doesn't match
						}
					}

					// Use the correct method depending on the item type (Potion vs normal Item)
					if (item.typeObject == 4 || item.typeObject == 8)
					{
						item.Use_Item();
					}
					else
					{
						GlobalService.gI().Use_Item(item.ID, item.typeObject);
					}

					if (GameScreen.player != null)
					{
						GameScreen.player.strChatPopup = "Auto mo: " + item.name;
					}
					break; // Only open one chest per interval to avoid spam/ban
				}
			}
		}
	}

	private static void PerformAutoQuaySo()
	{
		// Trigger lucky spin (1 spin)
		GlobalService.gI().Quayso(1);
		if (GameScreen.player != null)
		{
			GameScreen.player.strChatPopup = "Auto quay so...";
		}
	}
}
