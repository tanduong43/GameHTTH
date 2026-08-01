public class Item_Drop : MainObject
{
	public static Item_Drop currentSelectItemDrop;

	private long timeAp;

	private int timefly;

	private int timeDrop;

	private int timeStopDrop;

	private int timeGet;

	public sbyte typeDrop;

	public sbyte typeGiftDaily;

	public int num;

	private bool isUpdateDrop;

	private bool isLittlegarden;

	public static MainImage imgDefault;

	public static MainImage imgMainIconXp;

	public FrameImage fraImgVip;

	public bool isloadfra;

	public const sbyte GIFT_DAILY_NORMAL = 0;

	public const sbyte GIFT_DAILY_CURRENT = 1;

	public const sbyte GIFT_DAILY_GOT = 2;

	public const sbyte GIFT_DAILY_MISS = 3;

	public Item_Drop(short ID, sbyte type, string name, int x, int y, short IdIcon, sbyte color)
	{
		base.ID = ID;
		typeObject = type;
		base.name = name;
		if (x < 48)
		{
			x = 48;
		}
		if (x > GameCanvas.loadmap.maxWMap - 48)
		{
			x = GameCanvas.loadmap.maxWMap - 48;
		}
		if (y < 48)
		{
			y = 48;
		}
		if (y > GameCanvas.loadmap.maxHMap - 48)
		{
			y = GameCanvas.loadmap.maxHMap - 48;
		}
		base.x = x;
		base.y = y;
		base.IdIcon = IdIcon;
		colorName = color;
		wOne = (hOne = -1);
		vx = CRes.random_Am(1, 5);
		vy = -CRes.random(3, 10);
		timeDrop = CRes.random(5, 12);
		vMax = 16;
		vySea = 4;
		dyShadow = 0;
		if (typeObject == 4 && (IdIcon == 10 || IdIcon == 286))
		{
			dyShadow = 20;
		}
		timeAp = GameCanvas.timeNow;
		timeRemove = 60;
		isSend = false;
		typeDrop = 0;
	}

	public Item_Drop(short ID, sbyte type, string name, int x, int y, short IdIcon, sbyte color, int xto, int yto)
	{
		base.ID = ID;
		typeObject = type;
		base.name = name;
		if (x < 48)
		{
			x = 48;
		}
		if (x > GameCanvas.loadmap.maxWMap - 48)
		{
			x = GameCanvas.loadmap.maxWMap - 48;
		}
		if (y < 48)
		{
			y = 48;
		}
		if (y > GameCanvas.loadmap.maxHMap - 48)
		{
			y = GameCanvas.loadmap.maxHMap - 48;
		}
		base.x = x;
		base.y = y;
		base.IdIcon = IdIcon;
		colorName = color;
		wOne = (hOne = -1);
		vMax = 8;
		timeAp = GameCanvas.timeNow;
		timeRemove = 60;
		isSend = false;
		typeDrop = 1;
		create_Speed(xto - x, yto - y, null);
	}

	public Point_Focus create_Speed(int xdich, int ydich, Point_Focus p)
	{
		if (ydich == 0)
		{
			ydich = 1;
		}
		if (xdich == 0)
		{
			xdich = 1;
		}
		int num = 0;
		int num2 = 0;
		int num3 = MainObject.getDistance(xdich, ydich) / vMax;
		if (num3 == 0)
		{
			num3 = 1;
		}
		num = xdich / num3;
		num2 = ydich / num3;
		if (CRes.abs(num) > CRes.abs(xdich))
		{
			num = xdich;
		}
		if (CRes.abs(num2) > CRes.abs(ydich))
		{
			num2 = ydich;
		}
		if (p != null)
		{
			p.x = x;
			p.y = y;
			p.vx = num;
			p.vy = num2;
			p.toX = toX;
			p.toY = toY;
			p.fRe = num3;
		}
		else
		{
			timeDrop = num3;
			vx = num;
			vy = num2;
		}
		return p;
	}

	public override void paint(mGraphics g)
	{
		if (isLittlegarden)
		{
			paintXY(g, MainScreen.cameraMain.xCam + x, y);
			return;
		}
		MainImage mainImage = null;
		mainImage = getImg();
		if (!isloadfra)
		{
			setFraImageVip(mainImage);
		}
		if (mainImage.img != null)
		{
			if (wOne < 0)
			{
				wOne = mImage.getImageWidth(mainImage.img.image);
			}
			if (hOne < 0)
			{
				hOne = mImage.getImageHeight(mainImage.img.image);
			}
			int num = y;
			if (timeDrop <= 0)
			{
				g.drawImage(AvMain.imgShadowSmall, x, num - dySea / 10 - 4 + dyShadow, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if (fraImgVip != null)
			{
				int num2 = GameCanvas.gameTick / 3 % fraImgVip.nFrame;
				fraImgVip.drawFrame((num2 <= fraImgVip.nFrame - 1) ? num2 : 0, x, y, 0, 3, g);
			}
			else if (dySea / 10 != 0)
			{
				g.drawRegion(mainImage.img, 0, 0, wOne, hOne + dySea / 10, 0, x, num, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			else
			{
				g.drawImage(mainImage.img, x, num, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		if (Interface_Game.typeTouch == 1)
		{
			if (!checkXp())
			{
				sbyte color = 0;
				if (typeObject == 3)
				{
					color = colorName;
				}
				paintName(g, color, 0);
			}
		}
		else if (timeItemDropEvent > 0)
		{
			AvMain.FontBorderColor(g, "( " + timeItemDropEvent + " )", x, y - dy - hOne - 18, 2, 6, 7);
		}
	}

	private bool checkXp()
	{
		if (typeObject == 4 && (IdIcon == 10 || IdIcon == 286))
		{
			return true;
		}
		return false;
	}

	public void paintXY(mGraphics g, int x, int y)
	{
		MainImage mainImage = null;
		mainImage = getImg();
		if (mainImage == null || mainImage.img == null)
		{
			return;
		}
		if (!isloadfra)
		{
			setFraImageVip(mainImage);
		}
		if (fraImgVip != null)
		{
			int num = GameCanvas.gameTick / 3 % fraImgVip.nFrame;
			fraImgVip.drawFrame((num <= fraImgVip.nFrame - 1) ? num : 0, x, y, 0, 3, g);
			return;
		}
		if (hOne == 0)
		{
			hOne = mImage.getImageHeight(mainImage.img.image);
		}
		g.drawImage(mainImage.img, x, y, 3);
	}

	private void setFraImageVip(MainImage img)
	{
		if (img != null && img.img != null)
		{
			int imageWidth = mImage.getImageWidth(img.img.image);
			int imageHeight = mImage.getImageHeight(img.img.image);
			if (imageHeight / 2 >= imageWidth)
			{
				fraImgVip = new FrameImage(img.img, imageWidth, imageWidth);
			}
			isloadfra = true;
			hOne = imageWidth;
		}
	}

	public void paintAvatarFocus(mGraphics g, int xp, int yp)
	{
		MainImage mainImage = null;
		mainImage = getImg();
		if (!isloadfra)
		{
			setFraImageVip(mainImage);
		}
		if (mainImage.img != null)
		{
			if (hOne == 0)
			{
				hOne = mImage.getImageHeight(mainImage.img.image);
			}
			g.drawImage(mainImage.img, xp - 1, yp, 3);
		}
	}

	public override void paintName(mGraphics g, sbyte color, int isFocus)
	{
		if (!GameScreen.getIsOffAdmin(0))
		{
			if (isFocus == 0)
			{
				AvMain.setTextColor(colorName).drawString(g, name, x, y - dy - hOne - 18, 2);
			}
			if (timeItemDropEvent > 0)
			{
				AvMain.FontBorderColor(g, "( " + timeItemDropEvent + " )", x, y - dy - hOne - 28, 2, 6, 7);
				AvMain.FontBorderColor(g, namePlayer, x, y - dy - hOne - 38, 2, 5, 7);
			}
		}
	}

	public MainImage getImg()
	{
		if (IdIcon < 0)
		{
			return null;
		}
		if (typeObject == 99)
		{
			return imgMainIconXp;
		}
		if (typeObject == 3)
		{
			return ObjectData.getImageAll(IdIcon, ObjectData.hashImageItem, 3000);
		}
		if (typeObject == 4)
		{
			return ObjectData.getImageAll(IdIcon, ObjectData.hashImagePotion, 2000);
		}
		if (typeObject == 5)
		{
			return ObjectData.getImageAll(IdIcon, ObjectData.hashImageQuestPotion, 6000);
		}
		if (typeObject == 7)
		{
			return ObjectData.getImageAll(IdIcon, ObjectData.hashImageMaterialPotion, 6500);
		}
		if (typeObject == 109)
		{
			return ObjectData.getImageAll(IdIcon, ObjectData.HashImageOtherNew, 23000);
		}
		if (typeObject == 105)
		{
			return ObjectData.getImageAll(IdIcon, ObjectData.HashImageFashion, 20000);
		}
		return null;
	}

	public override void paintInfoFocus(mGraphics g, int xp, int yp)
	{
		AvMain.Font3dColor(g, name, xp + 48, yp, 1, colorName);
	}

	public override void update()
	{
		if (isRemove)
		{
			return;
		}
		if (timeDrop > 0)
		{
			x += vx;
			y += vy;
			if (typeDrop == 0)
			{
				vy += 2;
			}
			timeDrop--;
		}
		if (timeDrop == 0 && GameScreen.indexHelp == 8)
		{
			MainHelp.setNextHelp(isHanhTrang: false);
		}
		if (isUpdateDrop)
		{
			timefly++;
			x += vx;
			y += vy;
			if (timefly >= timeStopDrop)
			{
				isRemove = true;
			}
		}
		else if (timeDrop <= 0)
		{
			if (timeDrop == 0)
			{
				vMax = 8;
			}
			if (objMainFocus != null)
			{
				setVx_Vy_Item();
			}
			if (bossLittle != null)
			{
				setVx_Vy_ItemLittle();
			}
		}
		if (isSend)
		{
			timeGet++;
			if (timeGet > 40)
			{
				isSend = false;
				timeGet = 0;
			}
		}
		if ((GameCanvas.timeNow - timeAp) / 1000 >= timeRemove)
		{
			isRemove = true;
		}
		if (LoadMap.specMap == 4)
		{
			updateDySea();
		}
		if (timeItemDropEvent > 0 && GameCanvas.timeNow - timeBeginItemDropEvent > 1000)
		{
			timeItemDropEvent--;
			timeBeginItemDropEvent += 1000L;
		}
	}

	public override void updateDySea()
	{
		if (CRes.random(40) == 0)
		{
			if (CRes.random(2) == 0)
			{
				vySea = 4;
			}
			else
			{
				vySea = -4;
			}
		}
		if (dySea > 0 && vySea > 0)
		{
			vySea = -4;
		}
		else if (dySea < -50 && vySea < 0)
		{
			vySea = 4;
		}
		dySea += vySea;
	}

	public void setVx_Vy_Item()
	{
		toX = objMainFocus.x;
		toY = objMainFocus.y - objMainFocus.hOne / 2;
		int num = toX - x;
		int num2 = toY - y;
		if (num2 == 0)
		{
			num2 = 1;
		}
		if (num == 0)
		{
			num = 1;
		}
		int num3 = MainObject.getDistance(num, num2) / vMax;
		if (num3 == 0)
		{
			num3 = 1;
		}
		vx = num / num3;
		vy = num2 / num3;
		if (CRes.abs(vx) > CRes.abs(num))
		{
			vx = num;
		}
		if (CRes.abs(vy) > CRes.abs(num2))
		{
			vy = num2;
		}
		timefly = 0;
		timeStopDrop = num3 + 1;
		timeDrop = 0;
		isUpdateDrop = true;
	}

	public void setVx_Vy_ItemLittle()
	{
		if (bossLittle.type == 0)
		{
			toX = MotherCanvas.hw - 40;
		}
		else
		{
			toX = MotherCanvas.hw + 100;
		}
		toY = 60;
		x -= MainScreen.cameraMain.xCam;
		int num = toX - x;
		int num2 = toY - y;
		if (num2 == 0)
		{
			num2 = 1;
		}
		if (num == 0)
		{
			num = 1;
		}
		int num3 = MainObject.getDistance(num, num2) / vMax;
		if (num3 == 0)
		{
			num3 = 1;
		}
		vx = num / num3;
		vy = num2 / num3;
		if (CRes.abs(vx) > CRes.abs(num))
		{
			vx = num;
		}
		if (CRes.abs(vy) > CRes.abs(num2))
		{
			vy = num2;
		}
		timefly = 0;
		timeStopDrop = num3 + 1;
		timeDrop = 0;
		isUpdateDrop = true;
		isLittlegarden = true;
	}

	public override void setTouchPoint()
	{
		setFireObject(2);
	}

	public void setOpenBox()
	{
		MsgShowGift msgShowGift = new MsgShowGift();
		msgShowGift.setinfoShow_Gift(1, name, T.phanthuong, mItemDrop, -1);
		GameCanvas.Start_Current_Dialog(msgShowGift);
		isRemove = true;
	}

	public override void setFireObject(int value)
	{
		if (!isRemove)
		{
			if (namePlayer != string.Empty)
			{
				currentSelectItemDrop = this;
				GlobalService.gI().Get_Item_Map(ID, typeObject);
				isSend = true;
			}
			else if (mItemDrop != null)
			{
				setOpenBox();
			}
			else
			{
				GlobalService.gI().Get_Item_Map(ID, typeObject);
				isSend = true;
			}
		}
	}
}
