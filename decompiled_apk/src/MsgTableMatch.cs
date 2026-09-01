public class MsgTableMatch : MsgDialog
{
	public mVector vecMatch = new mVector("MsgTableMatch.vecMatch");

	public void setinfo(mVector vec)
	{
		if (vec != null && vec.size() != 0)
		{
			vecMatch.removeAllElements();
			wDia = 190;
			hDia = 200;
			wItem = 80;
			if (wDia > MotherCanvas.w)
			{
				wDia = MotherCanvas.w;
			}
			if (hDia > MotherCanvas.h - 26)
			{
				hDia = GameCanvas.hCommand - 26;
			}
			if (vec.size() * wItem + 10 < hDia)
			{
				hDia = vec.size() * wItem + 10;
				list = new ListNew(xDia, yDia, wDia, hDia - 16, 0, 0, 0, isLim0: false);
			}
			else
			{
				list = new ListNew(xDia, yDia, wDia, hDia - 16, 0, 0, vec.size() * wItem - (hDia - 15), isLim0: false);
			}
			xDia = MotherCanvas.w / 2 - wDia / 2;
			yDia = MotherCanvas.h / 2 - hDia / 2 + 13;
			vecMatch = vec;
			cmdClose = new iCommand(T.close, -1, this);
			cmdClose = AvMain.setPosCMD(cmdClose, 2);
			if (GameCanvas.isTouchNoOrPC())
			{
				right = cmdClose;
				backCMD = cmdClose;
			}
			if (AvMain.imgFightMatch == null || AvMain.imgStarMatch == null || AvMain.fraMatch == null)
			{
				LoadImageStatic.LoadImgMatch();
			}
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == -1)
		{
			GameCanvas.end_Dialog();
		}
	}

	public override void paint(mGraphics g)
	{
		paintPaper_UpDown(g, xDia, yDia, wDia, hDia, hDia);
		g.setClip(xDia + 10, yDia + 8, wDia - 20, hDia - 16);
		g.saveCanvas();
		g.ClipRec(xDia + 10, yDia + 8, wDia - 20, hDia - 16);
		g.translate(0, -list.cmx);
		int num = yDia;
		int num2 = xDia + 5;
		for (int i = 0; i < vecMatch.size(); i++)
		{
			InfoMatch infoMatch = (InfoMatch)vecMatch.elementAt(i);
			AvMain.fraMatch.drawFrame(infoMatch.type, num2 + wDia / 2, num + 20, 0, 3, g);
			int num3 = infoMatch.mname.Length;
			int num4 = num + 20 + 30;
			if (infoMatch.type == 1)
			{
				num4 = num + 35;
			}
			g.drawImage(AvMain.imgFightMatch, num2 + wDia / 2, num + wItem / 2 + 18, 3);
			for (int j = 0; j < num3; j++)
			{
				if (infoMatch.type == 2)
				{
					if (j < num3 / 2)
					{
						mFont.tahoma_7b_red.drawString(g, T.Clan, num2 + wDia / 4, num4 - GameCanvas.hText / 2, 2);
						mFont.tahoma_7b_red.drawString(g, infoMatch.mname[j], num2 + wDia / 4, num4 + GameCanvas.hText / 2, 2);
					}
					else
					{
						mFont.tahoma_7b_blue.drawString(g, T.Clan, num2 + wDia / 4 * 3, num4 - GameCanvas.hText / 2, 2);
						mFont.tahoma_7b_blue.drawString(g, infoMatch.mname[j], num2 + wDia / 4 * 3, num4 + GameCanvas.hText / 2, 2);
					}
				}
				else if (j < num3 / 2)
				{
					if (infoMatch.typeLeftRight == 0)
					{
						AvMain.FontBorderColor(g, infoMatch.mname[j], num2 + wDia / 4, num4 + 30 * (j % (num3 / 2)), 2, 6, 7);
					}
					else
					{
						mFont.tahoma_7b_red.drawString(g, infoMatch.mname[j], num2 + wDia / 4, num4 + 30 * (j % (num3 / 2)), 2);
					}
				}
				else if (infoMatch.typeLeftRight == 1)
				{
					AvMain.FontBorderColor(g, infoMatch.mname[j], num2 + wDia / 4 * 3, num4 + 30 * (j % (num3 / 2)), 2, 4, 7);
				}
				else
				{
					mFont.tahoma_7b_blue.drawString(g, infoMatch.mname[j], num2 + wDia / 4 * 3, num4 + 30 * (j % (num3 / 2)), 2);
				}
			}
			if (i < vecMatch.size() - 1)
			{
				for (int k = 0; k < 3; k++)
				{
					if (k == 2)
					{
						g.drawRegion(AvMain.imgStarMatch, 0, 0, 38, 5, 0, num2 + wDia / 2 - 60 + k * 40, num + wItem + 4, 0);
					}
					else
					{
						g.drawImage(AvMain.imgStarMatch, num2 + wDia / 2 - 60 + k * 40, num + wItem + 4, 0);
					}
				}
			}
			num += wItem;
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (!GameCanvas.isTouch && right != null)
		{
			right.paint(g, right.xCmd, right.yCmd);
		}
	}

	public override void update()
	{
		list.moveCamera();
		updatePointer();
		updatekey();
	}

	public override void updatekey()
	{
		bool flag = false;
		if (GameCanvas.keyMove(1))
		{
			idSelect--;
			GameCanvas.ClearkeyMove(1);
			flag = true;
		}
		else if (GameCanvas.keyMove(3))
		{
			idSelect++;
			GameCanvas.ClearkeyMove(3);
			flag = true;
		}
		if (flag)
		{
			idSelect = AvMain.resetSelect(idSelect, vecMatch.size() - 1, isreset: false);
			if (GameCanvas.isTouchNoOrPC())
			{
				list.setToX((idSelect + 1) * wItem - (hDia - 16) / 2);
			}
		}
		base.updatekeyCMD();
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointerSelect && !GameCanvas.isPoint(xDia, yDia, wDia, hDia))
		{
			GameCanvas.end_Dialog();
			GameCanvas.isPointerSelect = false;
		}
	}
}
