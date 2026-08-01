public class MsgListItem : MsgDialog
{
	private mVector listItem;

	private MainItem itemCur;

	private mVector vecInfoSS;

	private int xInfo;

	private int yInfo;

	private int wInfo = 100;

	private int hInfo = 40;

	private int dir;

	public override void commandPointer(int index, int subIndex)
	{
		if (index == 3)
		{
			MainItem mainItem = (MainItem)listItem.elementAt(idSelect);
			if (mainItem != null)
			{
				GlobalService.gI().Use_Item(mainItem.ID, mainItem.typeObject);
				GameCanvas.end_Dialog();
			}
		}
		else
		{
			base.commandPointer(index, subIndex);
		}
	}

	public void setinfoListItem(mVector vec, int x, int y, int wItem, int dir)
	{
		this.dir = dir;
		listItem = vec;
		if (listItem != null && listItem.size() != 0)
		{
			cmdClose = new iCommand(T.close, -1, this);
			cmdClose = AvMain.setPosCMD(cmdClose, 2);
			cmdChangeEquip = new iCommand(T.tabEquip, 3, this);
			cmdChangeEquip = AvMain.setPosCMD(cmdChangeEquip, 0);
			wDia = wItem;
			hDia = listItem.size() * wItem;
			if (hDia > wItem * 5)
			{
				hDia = wItem * 5;
			}
			xDia = x + wItem + 2;
			if (dir == 0)
			{
				xDia = x - wItem - 4;
			}
			yDia = y - hDia / 2;
			if (yDia < 2)
			{
				yDia = 2;
			}
			if (yDia + hDia + 2 > MotherCanvas.h - GameCanvas.hCommand)
			{
				yDia = MotherCanvas.h - hDia - 2 - GameCanvas.hCommand;
			}
			list = new ListNew(xDia, yDia, wDia, hDia, 0, 0, listItem.size() * wItem - hDia, isLim0: true);
			base.wItem = wItem;
			if (GameCanvas.isTouchNoOrPC())
			{
				idSelect = 0;
				updateInfo();
				right = cmdClose;
				center = cmdChangeEquip;
				backCMD = right;
				okCMD = cmdChangeEquip;
			}
			else
			{
				idSelect = -1;
			}
		}
	}

	public override void paint(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		AvMain.paintRect(g, xDia - 2, yDia - 2, wDia + 2, hDia + 2, 1, 2);
		g.setClip(xDia, yDia, wDia, hDia);
		g.saveCanvas();
		g.ClipRec(xDia, yDia, wDia, hDia);
		g.translate(0, -list.cmx);
		int num = list.cmx / wItem - 1;
		int num2 = num + hDia / wItem + 2;
		for (int i = num; i < num2; i++)
		{
			if (i >= 0 && i < listItem.size())
			{
				MainItem mainItem = (MainItem)listItem.elementAt(i);
				mainItem.paintColor(g, xDia + wItem / 2, yDia + wItem / 2 + i * wItem, wItem - 3);
				mainItem.paintEffSub(g, xDia + wItem / 2, yDia + wItem / 2 + i * wItem, wItem - 2, 1);
				mainItem.paintEff_LvUp(g, xDia + wItem / 2, yDia + wItem / 2 + i * wItem, wItem - 2, 1);
				AvMain.paintDrawRect(g, xDia + 1, yDia + 1 + wItem / 2 - wItem / 2 + i * wItem, wItem - 3, wItem - 3);
				if (i == idSelect)
				{
					g.setColor(16777215);
					g.drawRect(xDia + 1, yDia + wItem / 2 - wItem / 2 + 1 + i * wItem, wItem - 4, wItem - 4);
					g.setColor(14353458);
					g.drawRect(xDia, yDia + wItem / 2 - wItem / 2 + i * wItem, wItem - 2, wItem - 2);
				}
			}
		}
		mGraphics.resetTransAndroid(g);
		g.restoreCanvas();
		GameCanvas.resetTrans(g);
		if (itemCur != null)
		{
			MainTab.paintInfoEveryWhere(g, itemCur, vecInfoSS, 0, xInfo, yInfo, wInfo, hInfo, isLv: false, null, 0);
		}
		if (!GameCanvas.menuCur.isShowMenu)
		{
			paintCmd(g);
		}
		if (cmdList != null)
		{
			for (int j = 0; j < cmdList.size(); j++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(j);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
	}

	public override void update()
	{
		if (list != null)
		{
			list.moveCamera();
		}
		updatekey();
		updatePointer();
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
			idSelect = AvMain.resetSelect(idSelect, listItem.size() - 1, isreset: false);
			updateInfo();
			list.setToX((idSelect + 1) * wItem - wDia / 2);
		}
		base.updatekey();
		updatekeyPC();
	}

	public override void updatePointer()
	{
		list.update_Pos_UP_DOWN();
		if (GameCanvas.isPointSelect(xDia, yDia, wDia, hDia))
		{
			int num = (GameCanvas.py - yDia + list.cmx) / wItem;
			if (num == idSelect)
			{
				cmdChangeEquip.perform();
			}
			else if (num >= 0 && num < listItem.size())
			{
				idSelect = num;
				updateInfo();
			}
			GameCanvas.isPointerSelect = false;
		}
		if (GameCanvas.isPointerSelect && !GameCanvas.isPoint(xDia, yDia, wDia, hDia))
		{
			cmdClose.perform();
		}
	}

	public void setPosInfo()
	{
		int num = wItem * 2 + wInfo / 2;
		if (dir == 0)
		{
			num = -wItem - wInfo / 2 - 2;
		}
		setPosInfo(itemCur, xDia + num, yDia);
	}

	public void setPosInfo(MainItem item, int xbe, int ybe)
	{
		int num = wInfo;
		int num2 = hInfo;
		if (item != null)
		{
			num = item.wInfo;
			num2 = item.hInfo;
		}
		xInfo = xbe - num / 2;
		if (xInfo + num > MotherCanvas.w - 8)
		{
			xInfo = MotherCanvas.w - num - 8;
		}
		if (xInfo < 8)
		{
			xInfo = 8;
		}
		yInfo = ybe;
		if (yInfo + num2 > MotherCanvas.h - GameCanvas.hCommand - 8)
		{
			yInfo = MotherCanvas.h - GameCanvas.hCommand - num2 - 8;
		}
		if (yInfo < 8)
		{
			yInfo = 8;
		}
	}

	public void updateInfo()
	{
		if (idSelect < 0 || idSelect >= listItem.size())
		{
			itemCur = null;
			return;
		}
		MainItem mainItem = (MainItem)listItem.elementAt(idSelect);
		if (mainItem != null)
		{
			itemCur = mainItem;
			vecInfoSS = MainItem.getInfoSS(itemCur);
			setPosInfo();
		}
	}
}
