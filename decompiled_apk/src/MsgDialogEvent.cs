public class MsgDialogEvent : MsgDialog
{
	private int[][] mpos;

	private int[] mvalueCM;

	private iCommand cmdTrangTri;

	private iCommand cmdDoiQua;

	private bool isFullEventCM;

	public void setInfoMerryCM()
	{
		wDia = 160;
		hDia = 150;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 - iCommand.hButtonCmdNor / 2;
		mpos = new int[14][]
		{
			new int[6] { 50, 1, 0, -8, 33, -1 },
			new int[6] { 51, 2, 0, -103, 3, 0 },
			new int[6] { 52, 2, -12, -74, 3, 0 },
			new int[6] { 53, 2, 4, -58, 3, 0 },
			new int[6] { 54, 2, -30, -28, 3, 0 },
			new int[6] { 55, 2, 30, -28, 3, 0 },
			new int[6] { 56, 2, 10, -83, 3, 1 },
			new int[6] { 57, 2, 22, -53, 3, 1 },
			new int[6] { 58, 2, -19, -47, 3, 1 },
			new int[6] { 59, 2, 0, -25, 3, 1 },
			new int[6] { 60, 2, -18, -8, 3, 2 },
			new int[6] { 61, 2, 21, -12, 3, 2 },
			new int[6] { 62, 2, 0, -120, 3, 3 },
			new int[6] { 63, 3, 0, -8, 33, -1 }
		};
		cmdList.removeAllElements();
		mvalueCM = new int[mpos.Length];
		cmdTrangTri = new iCommand(T.trangtri, 0, this);
		cmdDoiQua = new iCommand(T.doiqua, 1, this);
		cmdClose = new iCommand(T.close, -1, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xDia + wDia - 13, yDia + 13, MainTab.fraCloseTab, string.Empty);
			cmdList.addElement(cmdClose);
			if (cmdDoiQua != null)
			{
				cmdDoiQua.setPos(xDia + wDia / 2 - iCommand.wButtonCmd / 2 - 2, yDia + hDia + iCommand.hButtonCmdNor, null, cmdDoiQua.caption);
				cmdList.addElement(cmdDoiQua);
			}
			if (cmdTrangTri != null)
			{
				cmdTrangTri.setPos(xDia + wDia / 2 + iCommand.wButtonCmd / 2 + 2, yDia + hDia + iCommand.hButtonCmdNor, null, cmdTrangTri.caption);
				cmdList.addElement(cmdTrangTri);
			}
			backCMD = cmdClose;
			okCMD = cmdDoiQua;
			menuCMD = cmdTrangTri;
		}
		else
		{
			left = cmdTrangTri;
			center = cmdDoiQua;
			right = cmdClose;
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			GameCanvas.end_Dialog();
			break;
		case 0:
			GlobalService.gI().cmd_Event(0);
			break;
		case 1:
			GlobalService.gI().cmd_Event(1);
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia + 140;
		int num2 = xDia + wDia / 2;
		paintPaper_UpDown(g, xDia - 5, yDia, maxWShow + 10, hDia, maxWShow + 10);
		for (int i = 0; i < mpos.Length; i++)
		{
			MainImage imageAll = ObjectData.getImageAll((short)mpos[i][0], ObjectData.hashImageItemOther, 9000);
			if (i == mpos.Length - 1)
			{
				if (imageAll != null && imageAll.img != null && isFullEventCM)
				{
					int num3 = imageAll.h / mpos[i][1];
					g.drawRegion(imageAll.img, 0, GameCanvas.gameTickChia4 % mpos[i][1] * num3, imageAll.w, num3, 0, num2 + mpos[i][2], num + mpos[i][3], mpos[i][4]);
				}
			}
			else if (imageAll != null && imageAll.img != null)
			{
				int num4 = imageAll.h / mpos[i][1];
				g.drawRegion(imageAll.img, 0, mvalueCM[i] * num4, imageAll.w, num4, 0, num2 + mpos[i][2], num + mpos[i][3], mpos[i][4]);
			}
		}
		GameCanvas.resetTrans(g);
		if (cmdList != null)
		{
			for (int j = 0; j < cmdList.size(); j++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		paintCmd(g);
	}

	public override void update()
	{
		base.update();
	}

	public void updateDataCM(sbyte[] mvalue)
	{
		bool flag = true;
		if (mvalue == null || mvalue.Length == 0)
		{
			flag = false;
			return;
		}
		for (int i = 0; i < mvalue.Length; i++)
		{
			if (i + 1 < mvalueCM.Length)
			{
				mvalueCM[i + 1] = mvalue[i];
			}
			if (mvalue[i] == 0)
			{
				flag = false;
			}
		}
		isFullEventCM = flag;
	}
}
