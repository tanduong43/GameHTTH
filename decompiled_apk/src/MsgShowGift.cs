public class MsgShowGift : MsgDialog
{
	private Item_Drop[] mItemgift;

	private string strphanthuong = string.Empty;

	public const sbyte GIFT_QUEST = 0;

	public const sbyte OPEN_BOX = 1;

	public const sbyte OPEN_BOX_WANTED = 2;

	public const sbyte GIFT_CHEST_WANTED = 3;

	public const sbyte OPEN_BOX_WANTED_GO = 10;

	public const sbyte OPEN_BOX_WANTED_VANG = 11;

	public const sbyte OPEN_BOX_WANTED_MATHUAT = 12;

	public const sbyte OPEN_BOX_WANTED_KHONGLO = 13;

	public const sbyte OPEN_BOX_WANTED_SIEUMATHUAT = 14;

	public const sbyte OPEN_BOX_WANTED_THANTHOAI = 15;

	public const sbyte OPEN_BOX_QUAY_SO = 20;

	public const sbyte OPEN_BOX_VIP = 21;

	public const sbyte OPEN_BOX_ON_HEAD = 22;

	public const sbyte OPEN_BOX_TYPE_ITEM_DROP = 23;

	public const sbyte OPEN_BOX_TYPE_ITEM_DROP_2 = 24;

	private MainObject objPaint;

	private int indexShowItemBox;

	private int timeShowEff;

	private int yShowEff;

	private int speed;

	private int countClose;

	private int timeClose;

	private short idChest;

	private FrameImage fraRuongVip;

	public static MainItem gift;

	private sbyte typeBanner = -1;

	private sbyte indexShow;

	public const sbyte SHOW_RUONG = 0;

	public const sbyte SHOW_OPEN_RUONG = 1;

	public const sbyte SHOW_GIFT = 2;

	public static int[] colorBorder = new int[18]
	{
		7351040, 9062926, 9062926, 6619136, 9175040, 11993088, 1377112, 2494085, 4202940, 6829826,
		10115089, 13403438, 1049732, 1179830, 3742207, 264001, 1119857, 1582026
	};

	public static int[] colorBorderVongQuay = new int[4] { 15384971, 13214061, 15913633, 13808516 };

	public int xCloseQuickOpen = 243;

	public int yCloseQuickOpen = 76;

	private int[][] mShowRuongVip = new int[38][]
	{
		new int[3] { -1, 0, 1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3] { 1, 0, -1 },
		new int[3] { -1, 0, 1 },
		new int[3],
		new int[3] { 1, 0, -1 }
	};

	public void setinfoShow_Gift(sbyte type, string name, string info, Item_Drop[] mitem, short IdChest)
	{
		fraRuongVip = null;
		base.type = type;
		mItemgift = mitem;
		nameDialog = name;
		idChest = IdChest;
		wItem = 22;
		vecEffUni.removeAllElements();
		fontDia = mFont.tahoma_7_black;
		timeShowEff = 0;
		beginDia();
		cmdList = new mVector();
		if (type == 0 || type == 3)
		{
			strphanthuong = T.phanthuong;
			indexShowItemBox = mItemgift.Length;
		}
		else if (type == 1 || type == 2 || type >= 10)
		{
			wItem = 26;
			strphanthuong = T.nhanduoc;
			indexShowItemBox = 0;
			if (type >= 10 && type < 20)
			{
				typeBanner = (sbyte)(type - 10);
			}
			switch (type)
			{
			case 1:
				if (checkRuongInInventory())
				{
					iCommand o = new iCommand(T.openNext, 11, this);
					cmdList.addElement(o);
				}
				break;
			case 21:
			{
				fraRuongVip = null;
				indexShowItemBox = mItemgift.Length;
				MainImage mainImage = null;
				mainImage = ObjectData.getImageAll(idChest, ObjectData.HashImageOtherNew, 23000);
				if (mainImage != null && mainImage.img != null)
				{
					fraRuongVip = new FrameImage(mainImage.img, 2);
				}
				vecEffUni.addElement(GameScreen.createEffectEndTime(144, 1, MotherCanvas.hw, MotherCanvas.hh, 2300, 0, null));
				break;
			}
			}
		}
		cmdClose = new iCommand(T.close, 10, this);
		cmdList.addElement(cmdClose);
		strinfo = null;
		wDia = 160;
		if (mitem != null)
		{
			for (int i = 0; i < mitem.Length; i++)
			{
				string s = mitem[i].num + " " + mitem[i].name;
				if (mitem[i].typeObject == 3)
				{
					s = mitem[i].name;
				}
				int width = mFont.tahoma_7b_black.getWidth(s);
				if (width + 60 > wDia)
				{
					wDia = width + 60;
				}
			}
		}
		if (wDia > MotherCanvas.w)
		{
			wDia = MotherCanvas.w;
		}
		if (type == 20)
		{
			wDia = 190;
			wItem = 44;
		}
		int num = 0;
		if (info != null && info.Length > 0)
		{
			strinfo = fontDia.splitFontArray(info, wDia - 20);
			num = strinfo.Length;
		}
		hDia = GameCanvas.hText * num + MsgDialog.hPlus + (iCommand.hButtonCmdNor + wItem);
		hDia += GameCanvas.hCommand;
		if (mItemgift != null)
		{
			switch (type)
			{
			case 3:
				hDia += mItemgift.Length * 50;
				if (mitem != null)
				{
					for (int j = 0; j < mitem.Length; j++)
					{
						typeBanner = (sbyte)(mitem[j].IdIcon - 500);
					}
				}
				break;
			case 20:
				hDia = (mItemgift.Length / 3 + 1) * wItem + iCommand.hButtonCmdNor;
				break;
			default:
				hDia += mItemgift.Length * wItem;
				break;
			}
		}
		if (!GameCanvas.lowGraphic && typeBanner >= AvMain.fraBorderWanted.nFrame)
		{
			typeBanner = (sbyte)(AvMain.fraBorderWanted.nFrame - 1);
		}
		maxWShow = wDia;
		wShowPaper = 5;
		xDia = MotherCanvas.hw - wDia / 2;
		yDia = MotherCanvas.hh - hDia / 2 - 5;
		setPosCmdNew(-2, isLast: false);
	}

	public void setCmdList(mVector vecCmd)
	{
		cmdList = vecCmd;
		setPosCmdNew(-2, isLast: false);
	}

	public void setinfoShow_Gift_OnHead(sbyte type, string name, string info, Item_Drop[] mitem, short IdChest, MainObject obj)
	{
		fraRuongVip = null;
		base.type = type;
		mItemgift = mitem;
		nameDialog = name;
		idChest = IdChest;
		objPaint = obj;
		speed = 3;
		if (mItemgift.Length >= 5)
		{
			speed = 4;
		}
		GameCanvas.chatTabScr.addNewChat(T.tabSieuBoss, string.Empty, "-- " + info + " --", 1, isFocus: false, 0);
		for (int i = 0; i < mItemgift.Length; i++)
		{
			Item_Drop item_Drop = mItemgift[i];
			item_Drop.y = -(24 * i);
			string content = item_Drop.num + " " + item_Drop.name;
			if (item_Drop.typeObject == 3)
			{
				content = item_Drop.name;
			}
			GameCanvas.chatTabScr.addNewChat(T.tabSieuBoss, string.Empty, content, 1, isFocus: false, 5);
		}
	}

	private bool checkRuongInInventory()
	{
		if (gift == null)
		{
			return false;
		}
		for (int i = 0; i < Player.vecInventory.size(); i++)
		{
			MainItem mainItem = (MainItem)Player.vecInventory.elementAt(i);
			if (mainItem.ID == gift.ID && mainItem.typeObject == gift.typeObject)
			{
				if (mainItem.numPotion > 1)
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}

	public override void paint(mGraphics g)
	{
		if (type == 22)
		{
			for (int i = 0; i < mItemgift.Length; i++)
			{
				Item_Drop item_Drop = mItemgift[i];
				if (item_Drop.y < 0 || item_Drop.y >= 120)
				{
					continue;
				}
				if (item_Drop.typeObject == 99)
				{
					g.drawImage(AvMain.imgXp, objPaint.x, objPaint.y - 52 - item_Drop.y, 3);
					AvMain.setTextColor(item_Drop.colorName).drawString(g, " " + item_Drop.num + item_Drop.name, objPaint.x + 14, objPaint.y - 56 - item_Drop.y, 0);
					continue;
				}
				item_Drop.paintXY(g, objPaint.x, objPaint.y - 52 - item_Drop.y);
				string str = item_Drop.num + " " + item_Drop.name;
				if (item_Drop.typeObject == 3)
				{
					str = item_Drop.name;
				}
				AvMain.Font3dColor(g, str, objPaint.x + 15, objPaint.y - 56 - item_Drop.y, 0, item_Drop.colorName);
			}
			return;
		}
		GameCanvas.resetTrans(g);
		if (type == 21 && (indexShow == 0 || indexShow == 1))
		{
			for (int j = 0; j < vecEffUni.size(); j++)
			{
				MainEffect mainEffect = (MainEffect)vecEffUni.elementAt(j);
				if (mainEffect.levelPaint == -1)
				{
					mainEffect.paint(g, 0, 0);
				}
			}
			paintShowVip(g);
			if (TabInventory.numPotionTemp > 0)
			{
				MainTab.fraCloseTab.drawFrame(0, xCloseQuickOpen, yCloseQuickOpen, 0, 3, g);
			}
			GameCanvas.resetTrans(g);
			return;
		}
		if (type == 21)
		{
			AvMain.paintRuongVip(g, MotherCanvas.hw - maxWShow / 2 - 4, yDia - 4, maxWShow + 8, hDia + 8);
			GameCanvas.resetTrans(g);
		}
		else if (type == 2 || type == 3 || (type >= 10 && type <= 19))
		{
			AvMain.paintThongThao(g, MotherCanvas.hw - maxWShow / 2, yDia, maxWShow, hDia);
			GameCanvas.resetTrans(g);
		}
		else
		{
			paintPaper(g, MotherCanvas.hw - wShowPaper / 2, yDia, wShowPaper, hDia, maxWShow, AvMain.PAPER_NORMAL);
			GameCanvas.resetTrans(g);
			g.setClip(MotherCanvas.hw - wShowPaper / 2, 0, wShowPaper, MotherCanvas.h);
		}
		paintBanner(g);
		AvMain.FontBorderColor(g, nameDialog, xDia + wDia / 2, yDia + 15, 2, 6, 5);
		int num = yDia + GameCanvas.hCommand + 10;
		if (strinfo != null)
		{
			for (int k = 0; k < strinfo.Length; k++)
			{
				if (k == 0 && type == 0)
				{
					mFont.tahoma_7b_black.drawString(g, strinfo[k], xDia + wDia / 2, num, 2);
				}
				else
				{
					fontDia.drawString(g, strinfo[k], xDia + 15, num, 0);
				}
				num += GameCanvas.hText;
			}
		}
		if (mItemgift != null)
		{
			if (type == 20)
			{
				paintItemQuaySo(g, num);
			}
			else
			{
				paintItemNormal(g, num);
			}
		}
		for (int l = 0; l < vecEffUni.size(); l++)
		{
			MainEffect mainEffect2 = (MainEffect)vecEffUni.elementAt(l);
			if (mainEffect2.levelPaint != -1)
			{
				mainEffect2.paint(g);
				mainEffect2.paint(g, 0, 0);
			}
		}
		if (cmdList != null)
		{
			for (int m = 0; m < cmdList.size(); m++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(m);
				iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
			}
		}
		if (TabInventory.numPotionTemp > 0)
		{
			MainTab.fraCloseTab.drawFrame(0, xCloseQuickOpen, yCloseQuickOpen, 0, 3, g);
		}
	}

	private void paintShowVip(mGraphics g)
	{
		if (indexShow == 0)
		{
			if (fraRuongVip == null)
			{
				return;
			}
			int num = timeShowEff;
			if (num >= mShowRuongVip.Length)
			{
				return;
			}
			int num2 = 0;
			int num3 = 0;
			if (timeShowEff >= 10 && timeShowEff < 24)
			{
				num2 = CRes.random_Am_0(2);
				num3 = CRes.random_Am_0(2);
			}
			else if (timeShowEff >= 24)
			{
				num2 = CRes.random_Am_0(3);
				num3 = CRes.random_Am_0(2);
			}
			for (int i = 0; i < 3; i++)
			{
				g.drawRegion(fraRuongVip.imgFrame, i % 3 * fraRuongVip.frameWidth / 3, 0, fraRuongVip.frameWidth / 3, fraRuongVip.frameHeight, 0, MotherCanvas.hw - fraRuongVip.frameWidth / 2 + i % 3 * (fraRuongVip.frameWidth / 3) + num2, MotherCanvas.hh - fraRuongVip.frameHeight / 2 + mShowRuongVip[num][i % 3] + num3, 0);
				if (i == 1)
				{
					AvMain.fraEffOpen.drawFrame(timeShowEff % AvMain.fraEffOpen.nFrame, MotherCanvas.hw + num2, MotherCanvas.hh + num3, 0, 3, g);
				}
			}
		}
		else if (indexShow == 1 && fraRuongVip != null)
		{
			fraRuongVip.drawFrame(1, MotherCanvas.hw, MotherCanvas.hh, 0, 3, g);
		}
	}

	private void paintItemNormal(mGraphics g, int ypaint)
	{
		fontDia.drawString(g, strphanthuong, xDia + 15, ypaint, 0);
		if (type != 3)
		{
			AvMain.paintRect(g, xDia + 10, ypaint + wItem / 2 + 5, wDia - 20, mItemgift.Length * wItem, 0, 4);
		}
		ypaint += wItem;
		yShowEff = ypaint;
		for (int i = 0; i < indexShowItemBox; i++)
		{
			if (i >= mItemgift.Length)
			{
				continue;
			}
			Item_Drop item_Drop = mItemgift[i];
			if (type == 3)
			{
				item_Drop.paintXY(g, xDia + wDia / 2, ypaint + 16);
				ypaint += 50;
				continue;
			}
			if (item_Drop.typeObject == 99)
			{
				g.drawImage(AvMain.imgXp, xDia + 28, ypaint + wItem / 4 + 1, 3);
				AvMain.setTextColor(item_Drop.colorName).drawString(g, " " + item_Drop.num + item_Drop.name, xDia + 42, ypaint, 0);
			}
			else
			{
				item_Drop.paintXY(g, xDia + 28, ypaint + wItem / 4);
				string str = item_Drop.num + " " + item_Drop.name;
				if (item_Drop.typeObject == 3)
				{
					str = item_Drop.name;
				}
				AvMain.Font3dColor(g, str, xDia + 43, ypaint, 0, item_Drop.colorName);
			}
			ypaint += wItem;
		}
	}

	public void paintBanner(mGraphics g)
	{
		if (type == 21)
		{
			g.setColor(15509578);
			g.fillRect(xDia + wDia / 2 - 45, yDia + 13, 90, 16);
			g.setColor(16770995);
			g.fillRect(xDia + wDia / 2 - 45, yDia + 14, 90, 14);
			g.setColor(16775390);
			g.fillRect(xDia + wDia / 2 - 45, yDia + 17, 90, 8);
			g.drawRegion(AvMain.imgBannerRuong, 0, 0, 16, 16, 0, xDia + wDia / 2 - 45 - 15, yDia + 11 + 2, 0);
			g.drawRegion(AvMain.imgBannerRuong, 0, 0, 16, 16, 2, xDia + wDia / 2 + 45 - 1, yDia + 11 + 2, 0);
		}
		else if (type == 20)
		{
			if (AvMain.mImgDialogVongQuay == null)
			{
				loadmImg();
			}
			g.setColor(10451279);
			g.fillRect(xDia + wDia / 2 - 60, yDia + 13, 120, 16);
			g.setColor(12358501);
			g.fillRect(xDia + wDia / 2 - 60, yDia + 14, 120, 14);
			g.drawRegion(AvMain.mImgDialogVongQuay[0], 0, 0, 16, 16, 0, xDia + wDia / 2 - 60 - 15, yDia + 11 + 2, 0);
			g.drawRegion(AvMain.mImgDialogVongQuay[0], 0, 0, 16, 16, 2, xDia + wDia / 2 + 60 - 1, yDia + 11 + 2, 0);
		}
		else if (typeBanner >= 0 && !GameCanvas.lowGraphic)
		{
			g.setColor(colorBorder[typeBanner * 3]);
			g.fillRect(xDia + 16, yDia + 11, wDia - 30, 20);
			g.setColor(colorBorder[typeBanner * 3 + 1]);
			g.fillRect(xDia + 17, yDia + 12, wDia - 32, 18);
			g.setColor(colorBorder[typeBanner * 3 + 2]);
			g.fillRect(xDia + 18, yDia + 13, wDia - 34, 16);
			AvMain.fraBorderWanted.drawFrame(typeBanner, xDia + 15, yDia + 11 + 10, 0, 3, g);
			AvMain.fraBorderWanted.drawFrame(typeBanner, xDia + 15 + (wDia - 28), yDia + 11 + 10, 2, 3, g);
		}
		else
		{
			g.setColor(15972174);
			g.fillRoundRect(xDia + 10, yDia + 12, wDia - 20, 16, 4, 4);
		}
	}

	private void paintItemQuaySo(mGraphics g, int ypaint)
	{
		if (AvMain.mImgDialogVongQuay == null)
		{
			loadmImg();
		}
		for (int i = 0; i < mItemgift.Length / 3; i++)
		{
			g.setColor(colorBorderVongQuay[i % 2]);
			g.fillRect(xDia + wDia / 2 - 50, ypaint + i * wItem, 100, 36);
			g.setColor(colorBorderVongQuay[2 + i % 2]);
			g.fillRect(xDia + wDia / 2 - 50, ypaint + i * wItem, 100, 36);
			g.drawRegion(AvMain.mImgDialogVongQuay[1 + i % 2], 0, 0, 36, 36, 0, xDia + wDia / 2 - 80, ypaint + i * wItem, 0);
			g.drawRegion(AvMain.mImgDialogVongQuay[1 + i % 2], 0, 36, 36, 36, 0, xDia + wDia / 2 + 80 - 36, ypaint + i * wItem, 0);
			g.drawRegion(AvMain.mImgDialogVongQuay[4], 0, i * 17, 13, 17, 0, xDia + wDia / 2 - 70, ypaint + i * wItem + wItem / 2 - 5, 3);
		}
		for (int j = 0; j < mItemgift.Length; j++)
		{
			int num = xDia + wDia / 2 - wItem + wItem * (j % 3);
			int num2 = ypaint + wItem / 2 + wItem * (j / 3) - 4;
			g.drawRegion(AvMain.mImgDialogVongQuay[3], 0, j / 3 % 2 * 30, 30, 30, 0, num, num2, 3);
			if (j >= indexShowItemBox)
			{
				continue;
			}
			Item_Drop item_Drop = mItemgift[j];
			if (item_Drop.IdIcon >= 0)
			{
				if (item_Drop.typeObject == 99)
				{
					g.drawImage(AvMain.imgXp, num, num2 - 3, 3);
				}
				else
				{
					item_Drop.paintXY(g, num, num2 - 3);
				}
				AvMain.setTextColor(item_Drop.colorName).drawString(g, item_Drop.num + string.Empty, num - 1, num2 + 3, 0);
			}
		}
	}

	private void loadmImg()
	{
		AvMain.mImgDialogVongQuay = new mImage[5];
		for (int i = 0; i < AvMain.mImgDialogVongQuay.Length; i++)
		{
			AvMain.mImgDialogVongQuay[i] = mImage.createImage("/interface/diavongquay" + i + ".png");
		}
	}

	public override void update()
	{
		for (int i = 0; i < vecEffUni.size(); i++)
		{
			MainEffect mainEffect = (MainEffect)vecEffUni.elementAt(i);
			mainEffect.update();
			if (mainEffect.isStop)
			{
				vecEffUni.removeElement(mainEffect);
				i--;
			}
		}
		if (type == 22)
		{
			timeClose++;
			if (mItemgift != null)
			{
				bool flag = true;
				for (int j = 0; j < mItemgift.Length; j++)
				{
					Item_Drop obj = mItemgift[j];
					obj.y += speed;
					if (obj.y < 0)
					{
						flag = false;
					}
				}
				if (flag)
				{
					countClose++;
					if (countClose >= 40)
					{
						GameCanvas.showDialog = null;
					}
				}
			}
			else
			{
				GameCanvas.showDialog = null;
			}
			if (timeClose >= 200)
			{
				GameCanvas.showDialog = null;
			}
			return;
		}
		if (isClose)
		{
			updateClose();
			return;
		}
		updateOpen();
		if (GameCanvas.isTouchNoOrPC())
		{
			updatekey();
		}
		updatePointer();
		if (type == 21)
		{
			timeShowEff++;
			if (indexShow == 0)
			{
				if (timeShowEff % 10 == 0 && timeShowEff <= 30)
				{
					mSound.playSound(27, mSound.volumeSound);
				}
				int num = (int)((double)mShowRuongVip.Length / (1.0 + (double)GameCanvas.percent / 100.0));
				if (timeShowEff >= num)
				{
					indexShow = 1;
					timeShowEff = 0;
					mSound.playSound(52, mSound.volumeSound);
				}
				if (timeShowEff % 5 == 0 && fraRuongVip == null)
				{
					MainImage mainImage = null;
					mainImage = ObjectData.getImageAll(idChest, ObjectData.HashImageOtherNew, 23000);
					if (mainImage != null && mainImage.img != null)
					{
						fraRuongVip = new FrameImage(mainImage.img, 2);
					}
				}
			}
			else if (indexShow == 1 && timeShowEff >= 3)
			{
				indexShow = 2;
			}
		}
		else if (type == 1 || type == 2 || (type >= 10 && type <= 19))
		{
			if (indexShowItemBox < mItemgift.Length)
			{
				timeShowEff++;
				int num2 = (int)(10.0 / (1.0 + (double)GameCanvas.percent / 100.0));
				if (timeShowEff % num2 == 2)
				{
					mSound.playSound(26, mSound.volumeSound);
					vecEffUni.addElement(GameScreen.CreateEffectEnd(53, 0, xDia + 30, yShowEff + indexShowItemBox * wItem, 0, null));
				}
				if (timeShowEff % num2 == 0)
				{
					indexShowItemBox++;
				}
			}
		}
		else if (type == 20 && indexShowItemBox < mItemgift.Length)
		{
			timeShowEff++;
			int num3 = (int)(10.0 / (1.0 + (double)GameCanvas.percent / 100.0));
			if (timeShowEff % num3 == 2)
			{
				int x = xDia + wDia / 2 - wItem + wItem * (indexShowItemBox % 3);
				int y = yDia + GameCanvas.hCommand + 10 + wItem / 2 + wItem * (indexShowItemBox / 3) - 4;
				mSound.playSound(47, mSound.volumeSound);
				vecEffUni.addElement(GameScreen.CreateEffectEnd(53, 0, x, y, 0, null));
			}
			if (timeShowEff % num3 == 0)
			{
				indexShowItemBox++;
			}
		}
	}

	public override void updatekey()
	{
		if (type == 22 || (type == 21 && indexShow != 2))
		{
			return;
		}
		int num = idCommand;
		int num2 = cmdList.size();
		if (GameCanvas.keyMove(0))
		{
			idCommand--;
			GameCanvas.ClearkeyMove(0);
		}
		else if (GameCanvas.keyMove(2))
		{
			idCommand++;
			GameCanvas.ClearkeyMove(2);
		}
		idCommand = AvMain.resetSelect(idCommand, num2 - 1, isreset: false);
		if (num != idCommand && GameCanvas.isTouchNoOrPC())
		{
			if (((iCommand)cmdList.elementAt(idCommand)).caption.Length <= 0)
			{
				idCommand = 0;
			}
			for (int i = 0; i < num2; i++)
			{
				iCommand iCommand2 = (iCommand)cmdList.elementAt(i);
				if (i == idCommand)
				{
					iCommand2.isSelect = true;
				}
				else
				{
					iCommand2.isSelect = false;
				}
			}
		}
		if (GameCanvas.keyMyHold[5])
		{
			GameCanvas.clearKeyHold(5);
			if (cmdList != null && idCommand < cmdList.size())
			{
				((iCommand)cmdList.elementAt(idCommand)).perform();
			}
		}
	}

	public override void updatePointer()
	{
		if (GameCanvas.isPointerSelect && TabInventory.numPotionTemp > 0)
		{
			updatePointerQuickOpenPotion();
		}
		if (type != 22 && (type != 21 || indexShow == 2))
		{
			base.updatePointer();
		}
	}

	public void updatePointerQuickOpenPotion()
	{
		if (GameCanvas.isPoint(xCloseQuickOpen - 10, yCloseQuickOpen - 10, 25, 25))
		{
			if (TabInventory.numPotionTemp > 0)
			{
				TabInventory.numPotionTemp = 0;
			}
			GameCanvas.isPointerSelect = false;
		}
	}
}
