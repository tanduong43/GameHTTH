public class MainScreen : AvMain
{
	private MainScreen mainScreen;

	public MainScreen lastScreen;

	public static Camera cameraMain;

	public static Camera cameraSub;

	public virtual void Show()
	{
		GameCanvas.clearKeyPressed();
		GameCanvas.menuCur.isShowMenu = false;
		GameCanvas.end_Dialog();
		GameCanvas.currentScreen = this;
		CheckClickCmd();
		setxyPlus12();
	}

	public virtual void Show(MainScreen screen)
	{
		if (screen != null)
		{
			lastScreen = screen;
		}
		GameCanvas.clearKeyPressed();
		GameCanvas.currentScreen = this;
		GameCanvas.end_Dialog();
		CheckClickCmd();
		setxyPlus12();
	}

	public virtual void setxyPlus12()
	{
		GameCanvas.xPlus12 = 2;
		GameCanvas.yPlus12 = 2;
	}

	public void CheckClickCmd()
	{
		if (left != null)
		{
			left.timeSelect = 0;
		}
		if (right != null)
		{
			right.timeSelect = 0;
		}
		if (center != null)
		{
			center.timeSelect = 0;
		}
	}

	public override void paint(mGraphics g)
	{
		base.paint(g);
	}

	public override void update()
	{
	}

	public virtual void keyPress(int keyCode)
	{
	}

	public virtual bool keyBack()
	{
		if (GameCanvas.currentDialog != null || GameCanvas.subDialog != null)
		{
			if (GameCanvas.currentDialog.isBack)
			{
				GameCanvas.end_Dialog();
			}
			return true;
		}
		if (GameCanvas.menuCur.isShowMenu)
		{
			GameCanvas.menuCur.isShowMenu = false;
			return true;
		}
		return false;
	}

	public void setTypeTab(sbyte type)
	{
		if (GameCanvas.currentScreen == GameCanvas.tabAllScr)
		{
			GameCanvas.tabAllScr.typeCurrent = type;
		}
		if (GameCanvas.currentScreen == GameCanvas.tabShopScr)
		{
			GameCanvas.tabShopScr.typeCurrent = type;
		}
		if (GameCanvas.currentScreen == GameCanvas.tabMarketScr)
		{
			GameCanvas.tabMarketScr.typeCurrent = type;
		}
	}

	public bool setCurTypetab(sbyte type)
	{
		if (GameCanvas.currentScreen == GameCanvas.tabAllScr && GameCanvas.tabAllScr.typeCurrent == type)
		{
			return true;
		}
		if (GameCanvas.currentScreen == GameCanvas.tabShopScr && GameCanvas.tabShopScr.typeCurrent == type)
		{
			return true;
		}
		if (GameCanvas.currentScreen == GameCanvas.tabMarketScr && GameCanvas.tabMarketScr.typeCurrent == type)
		{
			return true;
		}
		return false;
	}
}
