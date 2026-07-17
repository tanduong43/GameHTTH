public class MotherCanvas
{
	public static MotherCanvas instance;

	public GameCanvas tCanvas;

	public int zoomLevel = 1;

	public Image imgCache;

	private int[] imgRGBCache;

	private int newWidth;

	private int newHeight;

	private int[] output;

	private int OUTPUTSIZE = 20;

	public static int w;

	public static int h;

	public static int hw;

	public static int hh;

	public static int w4;

	public static int h4;

	public MotherCanvas()
	{
	}

	public MotherCanvas(Context context)
	{
	}

	public void checkZoomLevel()
	{
		int width = getWidth();
		int height = getHeight();
		Out.printLine("zoomLevel: " + mGraphics.zoomLevel + "    " + width + "    " + height + " " + GameMidlet.DEVICE);
		if (GameMidlet.DEVICE == 0)
		{
			mGraphics.zoomLevel = 1;
		}
		else if (width >= 1280 && height >= 960)
		{
			mGraphics.zoomLevel = 4;
		}
		else if (height >= 1280 && width >= 960)
		{
			mGraphics.zoomLevel = 4;
		}
		else if (width >= 720 && height >= 960)
		{
			mGraphics.zoomLevel = 3;
		}
		else if (width >= 960 && height >= 720)
		{
			mGraphics.zoomLevel = 3;
		}
		else if (width > 400 && height > 600)
		{
			mGraphics.zoomLevel = 2;
		}
		else if (width > 600 && height > 400)
		{
			mGraphics.zoomLevel = 2;
		}
		else
		{
			mGraphics.zoomLevel = 1;
		}
		mSystem.outz("chay vao khong" + mGraphics.zoomLevel);
		w = (width + mGraphics.zoomLevel - 1) / mGraphics.zoomLevel;
		h = (height + mGraphics.zoomLevel - 1) / mGraphics.zoomLevel;
		hw = w / 2;
		hh = h / 2;
		w4 = w / 4;
		h4 = h / 4;
	}

	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	public void setChildCanvas(GameCanvas tCanvas)
	{
		this.tCanvas = tCanvas;
	}

	public virtual void paint(mGraphics g)
	{
		tCanvas.paint(g);
	}

	public virtual void update()
	{
		tCanvas.update();
	}

	public virtual void keyPressed(int keyCode)
	{
		tCanvas.keyPressed(keyCode);
	}

	public virtual void keyReleased(int keyCode)
	{
		tCanvas.keyReleased(keyCode);
	}

	public void pointerDragged(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
	}

	public void pointerPressed(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
	}

	public void pointerReleased(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
	}

	public int getWidthz()
	{
		int width = getWidth();
		return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
	}

	public int getHeightz()
	{
		int height = getHeight();
		return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
	}

	public bool hasPointerEvents()
	{
		return true;
	}

	public bool keyPressPc(int keycode)
	{
		switch (keycode)
		{
		case 97:
			GameCanvas.keyMyHold[34] = true;
			GameCanvas.keyMyPressed[34] = true;
			return true;
		case 119:
			GameCanvas.keyMyHold[32] = true;
			GameCanvas.keyMyPressed[32] = true;
			return true;
		case 100:
			GameCanvas.keyMyHold[36] = true;
			GameCanvas.keyMyPressed[36] = true;
			return true;
		case 115:
			GameCanvas.keyMyHold[38] = true;
			GameCanvas.keyMyPressed[38] = true;
			return true;
		case 49:
		case 103:
			GameCanvas.keyMyHold[31] = true;
			GameCanvas.keyMyPressed[31] = true;
			return true;
		case 50:
		case 104:
			GameCanvas.keyMyHold[33] = true;
			GameCanvas.keyMyPressed[33] = true;
			return true;
		case 51:
		case 106:
			GameCanvas.keyMyHold[35] = true;
			GameCanvas.keyMyPressed[35] = true;
			return true;
		case 52:
		case 107:
			GameCanvas.keyMyHold[37] = true;
			GameCanvas.keyMyPressed[37] = true;
			return true;
		case 53:
		case 108:
			GameCanvas.keyMyHold[39] = true;
			GameCanvas.keyMyPressed[39] = true;
			break;
		case -21:
			GameCanvas.keyMyHold[40] = true;
			GameCanvas.keyMyPressed[40] = true;
			break;
		case -27:
		case -22:
			GameCanvas.keyMyHold[41] = true;
			GameCanvas.keyMyPressed[41] = true;
			return true;
		case 109:
			GameCanvas.keyMyHold[42] = true;
			GameCanvas.keyMyPressed[42] = true;
			return true;
		case 112:
			GameCanvas.keyMyHold[43] = true;
			GameCanvas.keyMyPressed[43] = true;
			return true;
		case 111:
			GameCanvas.keyMyHold[44] = true;
			GameCanvas.keyMyPressed[44] = true;
			return true;
		case 99:
			GameCanvas.keyMyHold[48] = true;
			GameCanvas.keyMyPressed[48] = true;
			return true;
		case 105:
			GameCanvas.keyMyHold[46] = true;
			GameCanvas.keyMyPressed[46] = true;
			return true;
		case 113:
			GameCanvas.keyMyHold[47] = true;
			GameCanvas.keyMyPressed[47] = true;
			return true;
		case 121:
			GameCanvas.keyMyHold[45] = true;
			GameCanvas.keyMyPressed[45] = true;
			return true;
		case 120:
			GameCanvas.keyMyHold[49] = true;
			GameCanvas.keyMyPressed[49] = true;
			return true;
		case 101:
			GameCanvas.keyMyHold[43] = true;
			GameCanvas.keyMyPressed[43] = true;
			return true;
		case 98:
			GameCanvas.keyMyHold[51] = true;
			GameCanvas.keyMyPressed[51] = true;
			return true;
		}
		return false;
	}

	public bool keyReleasedPc(int keycode)
	{
		switch (keycode)
		{
		case 97:
			GameCanvas.keyMyHold[34] = false;
			GameCanvas.keyMyPressed[34] = false;
			return true;
		case 119:
			GameCanvas.keyMyHold[32] = false;
			GameCanvas.keyMyPressed[32] = false;
			return true;
		case 100:
			GameCanvas.keyMyHold[36] = false;
			GameCanvas.keyMyPressed[36] = false;
			return true;
		case 115:
			GameCanvas.keyMyHold[38] = false;
			GameCanvas.keyMyPressed[38] = false;
			return true;
		case 49:
		case 103:
			GameCanvas.keyMyHold[31] = false;
			GameCanvas.keyMyPressed[31] = false;
			return true;
		case 50:
		case 104:
			GameCanvas.keyMyHold[33] = false;
			GameCanvas.keyMyPressed[33] = false;
			return true;
		case 51:
		case 106:
			GameCanvas.keyMyHold[35] = false;
			GameCanvas.keyMyPressed[35] = false;
			return true;
		case 52:
		case 107:
			GameCanvas.keyMyHold[37] = false;
			GameCanvas.keyMyPressed[37] = false;
			return true;
		case 53:
		case 118:
			GameCanvas.keyMyHold[39] = false;
			GameCanvas.keyMyPressed[39] = false;
			break;
		case -21:
			GameCanvas.keyMyHold[40] = false;
			GameCanvas.keyMyPressed[40] = false;
			break;
		case -27:
		case -22:
			GameCanvas.keyMyHold[41] = false;
			GameCanvas.keyMyPressed[41] = false;
			return true;
		case 109:
			GameCanvas.keyMyHold[42] = false;
			GameCanvas.keyMyPressed[42] = false;
			return true;
		case 112:
			GameCanvas.keyMyHold[50] = false;
			GameCanvas.keyMyPressed[50] = false;
			return true;
		case 111:
			GameCanvas.keyMyHold[44] = false;
			GameCanvas.keyMyPressed[44] = false;
			return true;
		case 99:
			GameCanvas.keyMyHold[48] = false;
			GameCanvas.keyMyPressed[48] = false;
			return true;
		case 105:
			GameCanvas.keyMyHold[46] = false;
			GameCanvas.keyMyPressed[46] = false;
			return true;
		case 113:
			GameCanvas.keyMyHold[47] = false;
			GameCanvas.keyMyPressed[47] = false;
			return true;
		case 121:
			GameCanvas.keyMyHold[45] = false;
			GameCanvas.keyMyPressed[45] = false;
			return true;
		case 120:
			GameCanvas.keyMyHold[49] = false;
			GameCanvas.keyMyPressed[49] = false;
			return true;
		case 101:
			GameCanvas.keyMyHold[43] = false;
			GameCanvas.keyMyPressed[43] = false;
			return true;
		case 98:
			GameCanvas.keyMyHold[51] = false;
			GameCanvas.keyMyPressed[51] = false;
			return true;
		}
		return false;
	}
}
