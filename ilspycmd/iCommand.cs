public class iCommand
{
	public string caption;

	public string clickCaption = string.Empty;

	public string[] subCaption;

	public IAction action;

	public AvMain Pointer;

	public int indexMenu;

	public int subIndex = -1;

	public bool isSelect;

	public int levelSmall;

	public int nframe;

	public int beginframe;

	public int timeSelect;

	public FrameImage fraImgCaption;

	public FrameImage fraImageCmd;

	public int wimgCaption;

	public int himgCaption;

	public int wstr;

	public int wimgCmd;

	public int himgCmd;

	public int xCmd = -1;

	public int yCmd = -1;

	public int frameCmd;

	public int xdich;

	public int ydich;

	public static int wButtonCmd = 70;

	public static int hButtonCmdNor = 30;

	public static int hButtonCmdSpec = 46;

	private sbyte typeButton;

	public static sbyte BTT_NOR;

	public static sbyte BTT_SPEC = 1;

	public static sbyte BTT_NONE = 2;

	public static sbyte BTT_RED = 3;

	public static sbyte BTT_GREEN = 4;

	public bool isEffText;

	public bool hardcodeEvent;

	public bool isDonotCloseMenu;

	public bool isPlayframe;

	public int IdGiaotiep;

	private mFont fontCmd;

	public MainItem item;

	public short idIconClan = -1;

	public short lvClan = -1;

	public short IdImage = -1;

	private CountDownTicket timeCount = new CountDownTicket();

	public bool isDisplay;

	private int xnone;

	private int ynone;

	private int wnone;

	private int hnone;

	private int lechHfra;

	public iCommand(string caption, IAction action)
	{
		this.caption = caption;
		this.action = action;
	}

	public iCommand(string caption, int type)
	{
		this.caption = caption;
		indexMenu = (sbyte)type;
	}

	public iCommand(string caption, int type, AvMain pointer)
	{
		this.caption = caption;
		indexMenu = (sbyte)type;
		Pointer = pointer;
	}

	public iCommand(string caption, int type, int subType, AvMain pointer)
	{
		this.caption = caption;
		indexMenu = type;
		subIndex = subType;
		Pointer = pointer;
	}

	public iCommand(string caption, int type, int subIndex)
	{
		this.caption = caption;
		indexMenu = (sbyte)type;
		this.subIndex = (sbyte)subIndex;
	}

	public void setTypeSpec()
	{
		subCaption = mFont.tahoma_7b_white.splitFontArray(caption, wButtonCmd - 4);
		typeButton = BTT_SPEC;
	}

	public void setTypeGreen()
	{
		typeButton = BTT_GREEN;
	}

	public void setTypeRed()
	{
		typeButton = BTT_RED;
	}

	public void setTypeButton(sbyte type)
	{
		typeButton = type;
	}

	public void setIsEffTest(bool isEff)
	{
		isEffText = isEff;
	}

	public void setFont(mFont f)
	{
		fontCmd = f;
	}

	public void setTime(int time)
	{
		timeCount.setCountDown(time);
	}

	public void setIdIconClan(short IdIconClan)
	{
		idIconClan = IdIconClan;
		wimgCaption = 20;
		himgCaption = 20;
		lechHfra = 4;
		if (GameCanvas.isSmallScreen)
		{
			wstr = mFont.tahoma_7_white.getWidth(caption);
		}
		else
		{
			wstr = mFont.tahoma_7b_white.getWidth(caption);
		}
	}

	public void setItem(MainItem item)
	{
		this.item = item;
		wimgCaption = 20;
		himgCaption = 20;
		lechHfra = 4;
		if (GameCanvas.isSmallScreen)
		{
			wstr = mFont.tahoma_7_white.getWidth(caption);
		}
		else
		{
			wstr = mFont.tahoma_7b_white.getWidth(caption);
		}
	}

	public void setTypeNone(int x, int y, int w, int h)
	{
		xnone = x;
		ynone = y;
		wnone = w;
		hnone = h;
		typeButton = BTT_NONE;
	}

	public void setFraCaption(mImage img)
	{
		FrameImage fraCaption = new FrameImage(img, mImage.getImageWidth(img.image), mImage.getImageHeight(img.image));
		setFraCaption(fraCaption);
	}

	public void setFraCaption(FrameImage fra)
	{
		fraImgCaption = fra;
		wimgCaption = fraImgCaption.frameWidth;
		himgCaption = fraImgCaption.frameHeight;
		nframe = fraImgCaption.nFrame;
		beginframe = 0;
		if (GameCanvas.isSmallScreen)
		{
			wstr = mFont.tahoma_7_white.getWidth(caption);
		}
		else
		{
			wstr = mFont.tahoma_7b_white.getWidth(caption);
		}
	}

	public void setFraCaption(FrameImage fra, int nframe, int beginframe, int lech)
	{
		fraImgCaption = fra;
		wimgCaption = fraImgCaption.frameWidth;
		himgCaption = fraImgCaption.frameHeight;
		lechHfra = lech;
		this.nframe = nframe;
		this.beginframe = beginframe;
		if (GameCanvas.isSmallScreen)
		{
			wstr = mFont.tahoma_7_white.getWidth(caption);
		}
		else
		{
			wstr = mFont.tahoma_7b_white.getWidth(caption);
		}
	}

	public void setPos(int x, int y, mImage img, string caption, int nframe)
	{
		FrameImage fra = new FrameImage(img, mImage.getImageWidth(img.image), mImage.getImageHeight(img.image) / nframe);
		setPos(x, y, fra, caption);
	}

	public void setPos(int x, int y, FrameImage fra, string caption)
	{
		this.caption = caption;
		xCmd = x;
		yCmd = y;
		fraImageCmd = fra;
		if (fraImageCmd != null)
		{
			wimgCmd = fraImageCmd.frameWidth;
			himgCmd = fraImageCmd.frameHeight;
			if (wimgCmd < 28)
			{
				wimgCmd = 28;
			}
			if (himgCmd < 28)
			{
				himgCmd = 28;
			}
		}
		else
		{
			wimgCmd = wButtonCmd;
			himgCmd = hButtonCmdNor;
			if (typeButton == BTT_SPEC)
			{
				himgCmd = hButtonCmdSpec;
			}
		}
	}

	public void setPos_ShowName(int x, int y, FrameImage fra, string clickcaption, int xdich, int ydich)
	{
		caption = string.Empty;
		clickCaption = clickcaption;
		xCmd = x;
		yCmd = y;
		this.xdich = xdich;
		this.ydich = ydich;
		fraImageCmd = fra;
		if (fraImageCmd != null)
		{
			wimgCmd = fraImageCmd.frameWidth;
			himgCmd = fraImageCmd.frameHeight;
			if (wimgCmd < 28)
			{
				wimgCmd = 28;
			}
			if (himgCmd < 28)
			{
				himgCmd = 28;
			}
		}
		else
		{
			wimgCmd = wButtonCmd;
			himgCmd = hButtonCmdNor;
			if (typeButton == BTT_SPEC)
			{
				himgCmd = hButtonCmdSpec;
			}
		}
	}

	public void setPosXY(int x, int y)
	{
		xCmd = x;
		yCmd = y;
	}

	public void perform()
	{
		timeSelect = 5;
		if (action != null)
		{
			action.perform();
			GameCanvas.isPointerSelect = false;
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			mSound.playSound(0, mSound.volumeSound);
			return;
		}
		mSound.playSound(0, mSound.volumeSound);
		if (Pointer != null)
		{
			Pointer.commandPointer(indexMenu, subIndex);
			GameCanvas.isPointerSelect = false;
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
		else if (GameCanvas.currentDialog != null)
		{
			GameCanvas.currentDialog.commandTab(indexMenu, subIndex);
			GameCanvas.isPointerSelect = false;
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
		else if (ChatTextField.isShow)
		{
			ChatTextField.gI().commandTab(indexMenu, subIndex);
		}
		else if (GameCanvas.subDialog != null)
		{
			GameCanvas.subDialog.commandTab(indexMenu, subIndex);
		}
		else
		{
			GameCanvas.currentScreen.commandTab(indexMenu, subIndex);
		}
	}

	public void update()
	{
	}

	public void paint(mGraphics g, int x, int y)
	{
		if (timeSelect > 0)
		{
			timeSelect--;
		}
		int idx = frameCmd;
		if (isPlayframe && frameCmd == 0)
		{
			idx = GameCanvas.gameTick / 8 % 2;
		}
		if (isPosCmd())
		{
			if (fraImageCmd != null)
			{
				fraImageCmd.drawFrame(idx, xCmd, yCmd, 0, 3, g);
			}
			else
			{
				paintbutton(g, xCmd, yCmd);
			}
			paintCaptionImage(g, xCmd, yCmd - 6, 2);
		}
		else
		{
			if (fraImageCmd != null)
			{
				fraImageCmd.drawFrame(idx, x, y, 0, 3, g);
			}
			else
			{
				paintbutton(g, x, y);
			}
			paintCaptionImage(g, x, y - 6, 2);
		}
	}

	public void paintCmd1Frame(mGraphics g, int x, int y)
	{
		if (timeSelect > 0)
		{
			timeSelect--;
		}
		int num = frameCmd;
		if (isPlayframe && frameCmd == 0)
		{
			num = GameCanvas.gameTick / 8 % 2;
		}
		if (num == 0)
		{
			fraImageCmd.drawFrame(num, xCmd, yCmd, 0, 3, g);
		}
		else
		{
			paintbuttonCmd1Frame(g, xCmd, yCmd);
		}
		paintCaptionImage(g, xCmd, yCmd - 6, 2);
	}

	public void paintXY(mGraphics g, int x, int y)
	{
		if (timeSelect > 0)
		{
			timeSelect--;
		}
		int idx = frameCmd;
		if (isPlayframe && frameCmd == 0)
		{
			idx = GameCanvas.gameTick / 8 % 2;
		}
		if (fraImageCmd != null)
		{
			fraImageCmd.drawFrame(idx, x, y, 0, 3, g);
		}
		else
		{
			paintbutton(g, x, y);
		}
		paintCaptionImage(g, x, y - 6, 2);
	}

	public void paintOnlyCaption(mGraphics g, int x, int y)
	{
		paintCaptionImage(g, x, y, 2);
	}

	public void paintCaptionImage(mGraphics g, int x, int y, int pos)
	{
		if (isEffText && GameCanvas.gameTick % 20 > 15)
		{
			return;
		}
		if (typeButton == BTT_SPEC)
		{
			int num = subCaption.Length;
			for (int i = 0; i < num; i++)
			{
				if (num == 3)
				{
					AvMain.Font3dColor(g, subCaption[i], x, y - hButtonCmdSpec / 7 * (num - 1) + i * (hButtonCmdSpec * 2 / 7), pos, 0);
				}
				else
				{
					AvMain.Font3dColor(g, subCaption[i], x, y - hButtonCmdSpec / 5 * (num - 1) + i * (hButtonCmdSpec * 2 / 5), pos, 0);
				}
			}
			return;
		}
		int num2 = 0;
		if (fraImgCaption != null)
		{
			fraImgCaption.drawFrame(beginframe + GameCanvas.gameTickChia4 % nframe, x - wstr / 2 - 1, y + himgCaption / 2 - 1 - lechHfra, 0, 3, g);
			num2 = wimgCaption / 2 + 1;
		}
		else if (item != null)
		{
			item.paintAllItem(g, x - wstr / 2 - 1, y + himgCaption / 2 - 1 - lechHfra, 0, 0, 7);
			if (wimgCaption == 20 && item.getImageAll() != null && item.getImageAll().img != null)
			{
				wimgCaption = mImage.getImageWidth(item.getImageAll().img.image);
				if (wimgCaption == 20)
				{
					wimgCaption = 21;
				}
			}
			num2 = wimgCaption / 2 + 1;
		}
		string text = caption;
		if (timeCount.timeCountDown > 0)
		{
			text = CountDownTicket.timeShow(timeCount.timeCountDown);
			timeCount.updateTimeCountDownTicket();
		}
		if (typeButton == BTT_GREEN)
		{
			AvMain.Font3dColor(g, text, x + num2, y, pos, 1);
		}
		else if (typeButton == BTT_RED)
		{
			AvMain.Font3dColor(g, text, x + num2, y, pos, 6);
		}
		else if (typeButton == BTT_NONE)
		{
			AvMain.Font3dColor(g, text, x + num2, y, pos, 5);
		}
		else if (GameCanvas.isSmallScreen)
		{
			mFont.tahoma_7_white.drawString(g, text, x + num2, y, pos);
		}
		else if (isDisplay)
		{
			AvMain.Font3dColor(g, text, x + num2, y, pos, 5);
		}
		else
		{
			AvMain.Font3dColor(g, text, x + num2, y, pos, 0);
		}
	}

	public void paintClickCaption(mGraphics g, int x, int y, int pos)
	{
	}

	public void paintImageCaption(mGraphics g)
	{
	}

	public void paintCaptionImageMenu(mGraphics g, int x, int y, int pos, bool is3D)
	{
		if (caption == null)
		{
			return;
		}
		int num = 0;
		if (fraImgCaption != null)
		{
			switch (pos)
			{
			case 2:
				fraImgCaption.drawFrame(beginframe + GameCanvas.gameTickChia4 % nframe, x - wstr / 2 - 1, y + himgCaption / 2 - 1 - lechHfra, 0, 3, g);
				num = wimgCaption / 2 + 1;
				break;
			case 0:
				fraImgCaption.drawFrame(beginframe + GameCanvas.gameTickChia4 % nframe, x + wimgCaption / 2, y + himgCaption / 2 - 1 - lechHfra, 0, 3, g);
				num = wimgCaption + 6;
				break;
			}
		}
		else if (item != null)
		{
			switch (pos)
			{
			case 2:
				item.paintAllItem(g, x - wstr / 2 - 1, y + himgCaption / 2 - 1 - lechHfra, 0, 0, 7);
				num = wimgCaption / 2 + 1;
				break;
			case 0:
				item.paintAllItem(g, x + wimgCaption / 2, y + himgCaption / 2 - 1 - lechHfra, 0, 0, 7);
				num = wimgCaption + 6;
				break;
			}
		}
		else if (idIconClan > 0)
		{
			MainImage iconClan = Potion.getIconClan(idIconClan);
			if (iconClan != null && iconClan.img != null)
			{
				switch (pos)
				{
				case 2:
					g.drawImage(iconClan.img, x - wstr / 2 - 1, y + himgCaption / 2 - 1 - lechHfra, 3);
					num = wimgCaption / 2 + 1;
					break;
				case 0:
					g.drawImage(iconClan.img, x + wimgCaption / 2, y + himgCaption / 2 - 1 - lechHfra, 3);
					num = wimgCaption + 6;
					break;
				}
			}
		}
		if (GameCanvas.isSmallScreen)
		{
			mFont.tahoma_7_black.drawString(g, caption, x + num, y - 1, pos);
			return;
		}
		if (is3D)
		{
			mFont.tahoma_7b_red.drawString(g, caption, x + num, y - 1, pos);
			return;
		}
		if (fontCmd != null)
		{
			fontCmd.drawString(g, caption, x + num, y - 1, pos);
		}
		else
		{
			mFont.tahoma_7b_black.drawString(g, caption, x + num, y - 1, pos);
		}
		if (hardcodeEvent && GameScreen.numMess > 0 && GameCanvas.gameTick % 10 < 8)
		{
			int width = mFont.tahoma_7b_black.getWidth(caption);
			g.drawImage(MainEvent.imgNew, x + width / 2 + 4, y, 3);
		}
	}

	public void paintOnlyImage(mGraphics g, int x, int y, sbyte focus)
	{
		fraImgCaption.drawFrame(beginframe + focus % nframe, x, y, 0, 3, g);
	}

	public void updatePointer()
	{
		if (!GameCanvas.isTouch)
		{
			return;
		}
		if (typeButton == BTT_NONE)
		{
			if (GameCanvas.isPointSelect(xnone, ynone, wnone, hnone))
			{
				perform();
				GameCanvas.isPointerSelect = false;
				frameCmd = 0;
			}
			return;
		}
		int num = wimgCmd;
		if (levelSmall > 0)
		{
			num -= levelSmall * 6;
		}
		if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
		{
			if (GameCanvas.isPoint(xCmd - num / 2 - 5, yCmd - himgCmd / 2 - 5, num + 10, himgCmd + 10))
			{
				frameCmd = 1;
				isSelect = true;
			}
			else
			{
				frameCmd = 0;
				isSelect = false;
			}
		}
		else if (!GameCanvas.isTouchAndKey())
		{
			isSelect = false;
			frameCmd = 0;
		}
		if (GameCanvas.isPointSelect(xCmd - num / 2 - 5, yCmd - himgCmd / 2 - 5, num + 10, himgCmd + 10))
		{
			perform();
			GameCanvas.isPointerSelect = false;
			frameCmd = 0;
		}
	}

	public void updatePointerInList(int yList)
	{
		if (!GameCanvas.isTouch)
		{
			return;
		}
		if (typeButton == BTT_NONE)
		{
			if (GameCanvas.isPointSelect(xnone, ynone, wnone, hnone))
			{
				perform();
				GameCanvas.isPointerSelect = false;
				frameCmd = 0;
			}
			return;
		}
		int num = wimgCmd;
		if (levelSmall > 0)
		{
			num -= levelSmall * 6;
		}
		if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
		{
			if (GameCanvas.isPoint(xCmd - num / 2 - 5, yCmd - yList - himgCmd / 2 - 5, num + 10, himgCmd + 10))
			{
				frameCmd = 1;
				isSelect = true;
			}
			else
			{
				frameCmd = 0;
				isSelect = false;
			}
		}
		else if (!GameCanvas.isTouchAndKey())
		{
			isSelect = false;
			frameCmd = 0;
		}
		if (GameCanvas.isPointSelect(xCmd - num / 2 - 5, yCmd - yList - himgCmd / 2 - 5, num + 10, himgCmd + 10))
		{
			perform();
			GameCanvas.isPointerSelect = false;
			frameCmd = 0;
		}
	}

	public void updatePointerNapThe(int xBtn, int yBtn)
	{
		int num = wimgCmd;
		if (levelSmall > 0)
		{
			num -= levelSmall * 6;
		}
		if (GameCanvas.isPointerDown && !GameCanvas.isPointerMove)
		{
			if (GameCanvas.isPoint(xBtn - num / 2 - 5, yBtn - 10 - 5, num + 10, 30))
			{
				frameCmd = 1;
				isSelect = true;
			}
			else
			{
				frameCmd = 0;
				isSelect = false;
			}
		}
		else if (!GameCanvas.isTouchAndKey() || GameCanvas.isPointerMove)
		{
			isSelect = false;
			frameCmd = 0;
		}
		if (GameCanvas.isPointSelect(xBtn - num / 2 - 5, yBtn - 10 - 5, num + 10, 30))
		{
			perform();
			GameCanvas.isPointerSelect = false;
			frameCmd = 0;
		}
	}

	public void updatePointerCameraScreen(Camera cam)
	{
		if (!isPosCmd() || !GameCanvas.isTouch)
		{
			return;
		}
		if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
		{
			if (GameCanvas.isPoint(xCmd - cam.xCam - wimgCmd / 2 - 5, yCmd - cam.yCam - himgCmd / 2 - 5, wimgCmd + 10, himgCmd + 10))
			{
				frameCmd = 1;
				isSelect = true;
			}
			else
			{
				frameCmd = 0;
				isSelect = false;
			}
		}
		else
		{
			isSelect = false;
			frameCmd = 0;
		}
		if (GameCanvas.isPointSelect(xCmd - cam.xCam - wimgCmd / 2 - 5, yCmd - cam.yCam - himgCmd / 2 - 5, wimgCmd + 10, himgCmd + 10))
		{
			perform();
			GameCanvas.isPointerSelect = false;
			frameCmd = 0;
		}
	}

	public void updatePointer(int cmx, int cmy)
	{
	}

	public void updatePointerShow(int cmx, int cmy)
	{
		if (!isPosCmd())
		{
			return;
		}
		if (GameCanvas.isPointerDown || GameCanvas.isPointerMove)
		{
			if (GameCanvas.isPoint(xCmd - wimgCmd / 2 - 3 - cmx, yCmd - himgCmd / 2 - 3 - cmy, wimgCmd + 6, himgCmd + 6))
			{
				frameCmd = 1;
			}
			else
			{
				frameCmd = 0;
			}
		}
		else
		{
			frameCmd = 0;
		}
	}

	public void paintbutton(mGraphics g, int x, int y)
	{
		if (typeButton != BTT_NONE)
		{
			int num = 24;
			int num2 = wButtonCmd - 2;
			if (levelSmall > 0)
			{
				num2 -= 6 * levelSmall;
			}
			if (typeButton == BTT_SPEC)
			{
				num = hButtonCmdSpec;
			}
			else if (num > hButtonCmdNor)
			{
				num = hButtonCmdNor;
			}
			sbyte b = 0;
			if (isSelect || timeSelect > 0)
			{
				b = (sbyte)((GameCanvas.gameTick % 20 >= 4) ? ((GameCanvas.gameTick % 20 < 8) ? 1 : 2) : 0);
			}
			AvMain.paintRectButton(g, x - num2 / 2, y - num / 2, num2, num, b);
		}
	}

	public void paintbuttonCmd1Frame(mGraphics g, int x, int y)
	{
		int num = 52;
		int num2 = 20;
		sbyte b = 0;
		if (isSelect || timeSelect > 0)
		{
			b = (sbyte)((GameCanvas.gameTick % 20 >= 4) ? ((GameCanvas.gameTick % 20 < 8) ? 1 : 2) : 0);
		}
		AvMain.paintRectButton(g, x - num / 2, y - num2 / 2, num, num2, b);
	}

	public bool isPosCmd()
	{
		if (xCmd >= 0 && yCmd >= 0)
		{
			return true;
		}
		return false;
	}
}
