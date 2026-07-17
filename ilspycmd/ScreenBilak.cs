using System;

public class ScreenBilak : MainScreen
{
	private mVector vecCmd = new mVector();

	private iCommand cmdSetTop;

	private iCommand cmdOff;

	private iCommand cmdMyRandom;

	private iCommand cmdTableAfter;

	private iCommand cmdSetCongWin;

	private iCommand cmdSetTruWin;

	private iCommand cmdTop6;

	private iCommand cmdOffTable;

	private int[][] valuePlay;

	private string strSoNguoi = "Số người chơi: ";

	private string strTop = "Số Top: ";

	private string strLe = string.Empty;

	private string strRMS = string.Empty;

	private string[] strSauCheck;

	public static string[] strListPlayer = new string[18]
	{
		"Phong", "Trung", "Hanh", "Hieu", "Vu de", "Lam", "Jacky", "Thang", "Khoi", "Quan",
		"Thai", "Giang", "Xuan", "Phi", "Trong", "Carot", "Vu ca", "Tri"
	};

	public static int[] valueTeam;

	public static int[] valueWin;

	public static int ValueAction = 0;

	private string[] stListPlayerFight;

	public static int demNG;

	public static int demTop;

	public ScreenBilak()
	{
		valuePlay = mSystem.new_M_Int(strListPlayer.Length, 2);
		valueWin = new int[strListPlayer.Length];
		for (int i = 0; i < valueWin.Length; i++)
		{
			valueWin[i] = 0;
		}
		for (int j = 0; j < valuePlay.Length; j++)
		{
			valuePlay[j] = new int[2];
			valuePlay[j][0] = 1;
			valuePlay[j][1] = 0;
		}
		cmdSetTop = new iCommand("Set Top", 0, this);
		cmdSetTop.setPos(MotherCanvas.hw, 15, null, cmdSetTop.caption);
		cmdSetTop.isSelect = true;
		cmdOff = new iCommand("Off", 1, this);
		cmdOff.setPos(MotherCanvas.hw, 45, null, cmdOff.caption);
		cmdMyRandom = new iCommand("Fight", 2, this);
		cmdMyRandom.setPos(MotherCanvas.hw, 75, null, cmdMyRandom.caption);
		cmdTableAfter = new iCommand("Lần Trước", 3, this);
		cmdTableAfter.setPos(MotherCanvas.hw + iCommand.wButtonCmd + 5, 15, null, cmdTableAfter.caption);
		cmdSetCongWin = new iCommand("+ Win", 4, this);
		cmdSetCongWin.setPos(MotherCanvas.hw + iCommand.wButtonCmd + 5, 45, null, cmdSetCongWin.caption);
		cmdSetTruWin = new iCommand("- Win", 5, this);
		cmdSetTruWin.setPos(MotherCanvas.hw + iCommand.wButtonCmd + 5, 75, null, cmdSetTruWin.caption);
		cmdTop6 = new iCommand("Auto Top 6", 6, this);
		cmdTop6.setPos(MotherCanvas.hw + iCommand.wButtonCmd * 2 + 10, 15, null, cmdTop6.caption);
		cmdOffTable = new iCommand("Off Table", 7, this);
		cmdOffTable.setPos(MotherCanvas.hw + iCommand.wButtonCmd * 2 + 10, 45, null, cmdOffTable.caption);
		vecCmd.addElement(cmdSetTop);
		vecCmd.addElement(cmdOff);
		vecCmd.addElement(cmdMyRandom);
		vecCmd.addElement(cmdTableAfter);
		vecCmd.addElement(cmdSetCongWin);
		vecCmd.addElement(cmdSetTruWin);
		vecCmd.addElement(cmdTop6);
		vecCmd.addElement(cmdOffTable);
		demNG = strListPlayer.Length;
		strSoNguoi = "Số người chơi: " + demNG;
		sbyte[] array = CRes.loadRMS("SUB_BILAK");
		try
		{
			if (array != null)
			{
				ByteArrayInputStream bis = new ByteArrayInputStream(array);
				DataInputStream dataInputStream = new DataInputStream(bis);
				strRMS = dataInputStream.readUTF();
				dataInputStream.close();
			}
		}
		catch (Exception)
		{
		}
		sbyte[] data = CRes.loadRMS("SUB_BILAK_TOP");
		try
		{
			if (array != null)
			{
				ByteArrayInputStream bis2 = new ByteArrayInputStream(data);
				DataInputStream dataInputStream2 = new DataInputStream(bis2);
				for (int k = 0; k < valueWin.Length; k++)
				{
					valueWin[k] = dataInputStream2.readByte();
				}
				dataInputStream2.close();
			}
		}
		catch (Exception)
		{
		}
		autoSetTop6();
		strTop = "Số Top: " + demTop;
	}

	private void autoSetTop6()
	{
		int num = 0;
		for (int i = 0; i < strListPlayer.Length; i++)
		{
			int num2 = 0;
			for (int j = 0; j < strListPlayer.Length; j++)
			{
				if (i != j && valueWin[i] < valueWin[j])
				{
					num2++;
				}
			}
			if (num2 < 6)
			{
				valuePlay[i][1] = 1;
				num++;
			}
			else
			{
				valuePlay[i][1] = 0;
			}
			if (num >= strListPlayer.Length / 2)
			{
				break;
			}
		}
		demTop = num;
	}

	public new void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case 0:
			ValueAction = 0;
			break;
		case 1:
			ValueAction = 1;
			break;
		case 2:
		{
			strSauCheck = new string[demNG];
			if (strSauCheck.Length % 2 == 1)
			{
				mSystem.outz("Danh sach nguoi choi le: " + strSauCheck.Length);
				strLe = "Danh sach nguoi choi le: " + strSauCheck.Length;
				break;
			}
			strLe = string.Empty;
			getStrSauCheck();
			if (demTop >= strSauCheck.Length / 2)
			{
				mSystem.outz("Top đang quá nữa danh sách" + demTop + "/" + strSauCheck.Length);
				strLe = "Top đang quá nữa danh sách" + demTop + "/" + strSauCheck.Length;
				break;
			}
			valueTeam = new int[strSauCheck.Length];
			for (int i = 0; i < valueTeam.Length; i++)
			{
				if (i == demTop)
				{
					for (int j = 0; j < demTop; j++)
					{
						if (CRes.random(2) == 0)
						{
							valueTeam[j]++;
						}
					}
				}
				if (i < demTop)
				{
					valueTeam[i] = CRes.random(strSauCheck.Length / 2) * 2;
					if (i <= 0)
					{
						continue;
					}
					bool flag = false;
					bool flag2 = false;
					while (!flag2)
					{
						flag = false;
						for (int k = 0; k < i; k++)
						{
							if (valueTeam[i] == valueTeam[k])
							{
								flag = true;
							}
						}
						if (!flag)
						{
							flag2 = true;
						}
						else
						{
							valueTeam[i] = CRes.random(strSauCheck.Length / 2) * 2;
						}
					}
					continue;
				}
				bool flag3 = false;
				bool flag4 = false;
				valueTeam[i] = CRes.random(strSauCheck.Length);
				while (!flag4)
				{
					flag3 = false;
					for (int l = 0; l < i; l++)
					{
						if (valueTeam[i] == valueTeam[l])
						{
							flag3 = true;
						}
					}
					if (!flag3)
					{
						flag4 = true;
					}
					else
					{
						valueTeam[i] = CRes.random(strSauCheck.Length);
					}
				}
			}
			stListPlayerFight = new string[strSauCheck.Length];
			for (int m = 0; m < stListPlayerFight.Length; m++)
			{
				for (int n = 0; n < valueTeam.Length; n++)
				{
					if (valueTeam[n] == m)
					{
						stListPlayerFight[m] = strSauCheck[n];
					}
				}
			}
			strRMS = string.Empty;
			mSystem.outz("===--- Chia Doi ---===");
			for (int num = 0; num < valueTeam.Length / 4; num++)
			{
				string text = stListPlayerFight[num * 4] + "-" + stListPlayerFight[num * 4 + 1] + " VS " + stListPlayerFight[num * 4 + 2] + "-" + stListPlayerFight[num * 4 + 3];
				mSystem.outz(text);
				strRMS = strRMS + text + "\n";
			}
			if (valueTeam.Length / 2 % 2 == 1)
			{
				string text2 = "Doi le " + stListPlayerFight[stListPlayerFight.Length - 1] + "-" + stListPlayerFight[stListPlayerFight.Length - 2];
				mSystem.outz(text2);
				strRMS += text2;
			}
			ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
			DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
			try
			{
				dataOutputStream.writeUTF(strRMS);
				CRes.saveRMS("SUB_BILAK", byteArrayOutputStream.toByteArray());
				dataOutputStream.close();
				break;
			}
			catch (Exception)
			{
				break;
			}
		}
		case 3:
		{
			string info = "Không có dữ liệu";
			if (strRMS.Length > 0)
			{
				info = strRMS;
			}
			GameCanvas.Start_Normal_Only_CmdClose_DiaLog(info);
			break;
		}
		case 4:
			ValueAction = 2;
			break;
		case 5:
			ValueAction = 3;
			break;
		case 6:
			autoSetTop6();
			break;
		case 7:
			stListPlayerFight = null;
			break;
		}
	}

	private void getStrSauCheck()
	{
		int num = 0;
		for (int i = 0; i < strListPlayer.Length; i++)
		{
			if (valuePlay[i][0] == 1 && valuePlay[i][1] == 1)
			{
				strSauCheck[num] = strListPlayer[i];
				num++;
			}
		}
		for (int j = 0; j < strListPlayer.Length; j++)
		{
			if (valuePlay[j][1] == 0 && valuePlay[j][0] == 1)
			{
				strSauCheck[num] = strListPlayer[j];
				num++;
			}
		}
	}

	public new void paint(mGraphics g)
	{
		g.setColor(0);
		g.fillRect(0, 0, MotherCanvas.w, MotherCanvas.h);
		for (int i = 0; i < strListPlayer.Length; i++)
		{
			g.setColor(728640);
			if (valuePlay[i][0] == 1)
			{
				if (valuePlay[i][1] == 1)
				{
					g.setColor(12200257);
				}
				else
				{
					g.setColor(5159465);
				}
			}
			g.fillRect(2 + i / 10 * 60, 2 + i % 10 * 24, 50, 20);
			mFont.tahoma_7b_yellow.drawString(g, string.Empty + valueWin[i], 9 + i / 10 * 60, 6 + i % 10 * 24, 2);
			mFont.tahoma_7b_white.drawString(g, strListPlayer[i], 16 + i / 10 * 60, 6 + i % 10 * 24, 0);
		}
		mFont.tahoma_7b_green.drawString(g, strSoNguoi, MotherCanvas.hw, 105, 2);
		mFont.tahoma_7b_red.drawString(g, strTop, MotherCanvas.hw, 125, 2);
		int num = 155;
		if (stListPlayerFight != null)
		{
			int num2 = num + valueTeam.Length / 4 * 24 + 24;
			if (valueTeam.Length / 2 % 2 == 1)
			{
				num2 += 24;
			}
			if (num2 > MotherCanvas.h)
			{
				num -= num2 - MotherCanvas.h;
			}
			g.setColor(8882193);
			g.fillRect(MotherCanvas.hw - 60, num - 5, 120, 20);
			mFont.tahoma_7b_white.drawString(g, "Chia Cặp", MotherCanvas.hw, num, 2);
			num += 24;
			for (int j = 0; j < valueTeam.Length / 4; j++)
			{
				g.setColor(8882221);
				g.fillRect(MotherCanvas.hw - 100, num - 5, 200, 20);
				mFont.tahoma_7b_white.drawString(g, stListPlayerFight[j * 4] + "-" + stListPlayerFight[j * 4 + 1] + " VS", MotherCanvas.hw, num, 1);
				mFont.tahoma_7b_white.drawString(g, " " + stListPlayerFight[j * 4 + 2] + "-" + stListPlayerFight[j * 4 + 3], MotherCanvas.hw, num, 0);
				num += 24;
			}
			if (valueTeam.Length / 2 % 2 == 1)
			{
				g.setColor(8882221);
				g.fillRect(MotherCanvas.hw - 80, num - 5, 160, 20);
				mFont.tahoma_7b_white.drawString(g, "Đội lẻ " + stListPlayerFight[stListPlayerFight.Length - 1] + "-" + stListPlayerFight[stListPlayerFight.Length - 2], MotherCanvas.hw, num, 2);
			}
		}
		else if (strLe.Length > 0)
		{
			mFont.tahoma_7b_white.drawString(g, strLe, MotherCanvas.hw, num, 2);
		}
		for (int k = 0; k < vecCmd.size(); k++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(k);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
		base.paint(g);
	}

	public new void updatePointer()
	{
		bool flag = false;
		for (int i = 0; i < strListPlayer.Length; i++)
		{
			if (!GameCanvas.isPointerSelect || !GameCanvas.isPoint(2 + i / 10 * 60, 2 + i % 10 * 24, 50, 20))
			{
				continue;
			}
			if (ValueAction == 1)
			{
				if (valuePlay[i][1] == 1)
				{
					valuePlay[i][1] = 0;
				}
				if (valuePlay[i][0] == 1)
				{
					valuePlay[i][0] = 0;
				}
				else
				{
					valuePlay[i][0] = 1;
				}
			}
			else if (ValueAction == 0)
			{
				if (valuePlay[i][0] == 0)
				{
					valuePlay[i][0] = 1;
				}
				if (valuePlay[i][1] == 1)
				{
					valuePlay[i][1] = 0;
				}
				else
				{
					valuePlay[i][1] = 1;
				}
			}
			else
			{
				if (ValueAction == 2)
				{
					valueWin[i]++;
				}
				else if (ValueAction == 3)
				{
					valueWin[i]--;
				}
				ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
				DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
				try
				{
					for (int j = 0; j < valueWin.Length; j++)
					{
						dataOutputStream.writeByte((sbyte)valueWin[j]);
					}
					CRes.saveRMS("SUB_BILAK_TOP", byteArrayOutputStream.toByteArray());
					dataOutputStream.close();
				}
				catch (Exception)
				{
				}
			}
			GameCanvas.isPointerSelect = false;
			flag = true;
		}
		if (flag)
		{
			demNG = 0;
			demTop = 0;
			for (int k = 0; k < strListPlayer.Length; k++)
			{
				if (valuePlay[k][0] == 1)
				{
					demNG++;
				}
				if (valuePlay[k][1] == 1)
				{
					demTop++;
				}
			}
			strSoNguoi = "Số người chơi: " + demNG;
			strTop = "Số Top: " + demTop;
		}
		for (int l = 0; l < vecCmd.size(); l++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(l);
			iCommand2.updatePointer();
		}
		base.updatePointer();
	}
}
