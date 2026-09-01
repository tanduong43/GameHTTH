public class MsgDialogTrangTri : MsgDialog
{
	public static sbyte TT_BANH_TET;

	public static sbyte TT_LO_SUOI = 4;

	private sbyte typeTrangTri;

	private short[][] mpos;

	private sbyte[] mSoLuong;

	private iCommand cmdTrangTri;

	private iCommand cmdDoiQua;

	private bool isFullEvent;

	private mVector meff = new mVector();

	public MsgDialogTrangTri()
	{
	}

	public MsgDialogTrangTri(sbyte typeTrangTri)
	{
		this.typeTrangTri = typeTrangTri;
	}

	public void setInfoTrangTri(short[][] mpos)
	{
		wDia = 220;
		hDia = 200;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2;
		this.mpos = mpos;
		cmdList.removeAllElements();
		mSoLuong = new sbyte[this.mpos.Length];
		cmdTrangTri = new iCommand(T.trangtri, 0, this);
		cmdDoiQua = new iCommand(T.doiqua, 1, this);
		cmdClose = new iCommand(T.close, -1, this);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(xDia + wDia - 13, yDia + 11, MainTab.fraCloseTab, string.Empty);
			cmdList.addElement(cmdClose);
			if (cmdDoiQua != null)
			{
				cmdDoiQua.setPos(GameCanvas.wCommand - 3, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, cmdDoiQua.caption);
				cmdList.addElement(cmdDoiQua);
			}
			if (cmdTrangTri != null)
			{
				cmdTrangTri.setPos(MotherCanvas.w - GameCanvas.wCommand + 3, MotherCanvas.h - iCommand.hButtonCmdNor / 2 + 2, null, cmdTrangTri.caption);
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
		meff.removeAllElements();
		isFullEvent = false;
	}

	public override void commandPointer(int index, int subIndex)
	{
		switch (index)
		{
		case -1:
			GameCanvas.end_Dialog();
			break;
		case 0:
			GlobalService.gI().cmd_TrangTri(typeTrangTri, 0);
			break;
		case 1:
			GlobalService.gI().cmd_TrangTri(typeTrangTri, 1);
			break;
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		int num = yDia + hDia / 2;
		int num2 = xDia + wDia / 2;
		if (typeTrangTri == TT_LO_SUOI)
		{
			paintPaper_UpDown(g, xDia, yDia, wDia, hDia, wDia);
		}
		else
		{
			paintBgDialogTrangTri(g, xDia, yDia, wDia, hDia);
		}
		if (isFullEvent)
		{
			for (int i = 0; i < meff.size(); i++)
			{
				((Effect_End)meff.elementAt(i)).paint(g);
			}
		}
		for (int num3 = mpos.Length - 1; num3 >= 0; num3--)
		{
			MainImage imageAll = ObjectData.getImageAll(mpos[num3][0], ObjectData.hashImageItemOther, 9000);
			if (imageAll != null && imageAll.img != null)
			{
				if (isFullEvent && mpos[num3][5] < 0)
				{
					int num4 = imageAll.h / mpos[num3][1];
					g.drawRegion(imageAll.img, 0, GameCanvas.gameTickChia4 % mpos[num3][1] * num4, imageAll.w, num4, 0, num2 + mpos[num3][2], num + mpos[num3][3], mpos[num3][4]);
				}
				else if (!isFullEvent && mpos[num3][5] >= 0)
				{
					if (mpos[num3][1] > 2)
					{
						int num5 = imageAll.h / mpos[num3][1];
						g.drawRegion(imageAll.img, 0, GameCanvas.gameTickChia4 % mpos[num3][1] * num5, imageAll.w, num5, 0, num2 + mpos[num3][2], num + mpos[num3][3], mpos[num3][4]);
					}
					else
					{
						int num6 = imageAll.h / mpos[num3][1];
						int num7 = 0;
						if (mpos[num3][1] == 2 && mSoLuong[num3] == mpos[num3][5])
						{
							num7 = 1;
						}
						g.drawRegion(imageAll.img, 0, num7 * num6, imageAll.w, num6, 0, num2 + mpos[num3][2], num + mpos[num3][3], mpos[num3][4]);
						if (mpos[num3][5] > 1 && mSoLuong[num3] < mpos[num3][5])
						{
							mFont.tahoma_7b_black.drawString(g, mSoLuong[num3] + "/" + mpos[num3][5], num2 + mpos[num3][2] + mpos[num3][6], num + mpos[num3][3] + mpos[num3][7], 2);
						}
					}
				}
			}
		}
		if (isFullEvent)
		{
			MainImage imageAll2 = ObjectData.getImageAll(192, ObjectData.HashImageEffClient, 24000);
			if (imageAll2 != null && imageAll2.img != null)
			{
				int num8 = imageAll2.h / 2;
				g.drawRegion(imageAll2.img, 0, GameCanvas.gameTickChia4 / 2 % 2 * num8, imageAll2.w, num8, 0, num2 - 71, num - 58, 3);
				g.drawRegion(imageAll2.img, 0, GameCanvas.gameTickChia4 / 2 % 2 * num8, imageAll2.w, num8, 0, num2 + 70, num - 58, 3);
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
		if (isFullEvent && GameCanvas.gameTick % 6 == 0)
		{
			int num = CRes.random(5);
			for (int i = 0; i < num; i++)
			{
				int x = CRes.random(xDia + 50, xDia + wDia - 50);
				int y = CRes.random(yDia + 50, yDia + wDia - 50);
				meff.addElement(new Effect_End(169, 0, x, y, 0));
			}
		}
		for (int j = 0; j < meff.size(); j++)
		{
			Effect_End effect_End = (Effect_End)meff.elementAt(j);
			effect_End.update();
			if (effect_End.isStop)
			{
				meff.removeElement(effect_End);
				j--;
			}
		}
	}

	public void updateDataTrangTri(short[] mId, sbyte[] mvalue)
	{
		if (mId == null || mId.Length == 0)
		{
			isFullEvent = false;
			return;
		}
		for (int i = 0; i < mId.Length; i++)
		{
			for (int j = 0; j < mpos.Length; j++)
			{
				if (mpos[j][0] == mId[i])
				{
					if (mvalue[i] > mpos[j][5])
					{
						mSoLuong[j] = (sbyte)mpos[j][5];
					}
					else
					{
						mSoLuong[j] = mvalue[i];
					}
					mvalue[i] -= mSoLuong[j];
					if (mvalue[i] == 0)
					{
						break;
					}
				}
			}
		}
		bool flag = true;
		for (int k = 0; k < mpos.Length; k++)
		{
			if (mSoLuong[k] < mpos[k][5])
			{
				flag = false;
				break;
			}
		}
		isFullEvent = flag;
	}
}
