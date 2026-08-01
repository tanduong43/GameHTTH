public class GamePad
{
	private int xC;

	private int yC;

	private int xM;

	private int yM;

	private int xMLast;

	private int yMLast;

	private int R;

	private int r;

	private int d;

	private int xTemp;

	private int yTemp;

	private int deltaX;

	private int deltaY;

	private int delta;

	private int angle;

	public int xZone;

	public int yZone;

	public int wZone;

	public int hZone;

	private bool isGamePad;

	public bool isSmallGamePad;

	public bool isMediumGamePad;

	public bool isLargeGamePad;

	public GamePad()
	{
		R = 28;
		if (MotherCanvas.w < 300)
		{
			isSmallGamePad = true;
			isMediumGamePad = false;
			isLargeGamePad = false;
		}
		if (MotherCanvas.w >= 300 && MotherCanvas.w <= 380)
		{
			isSmallGamePad = false;
			isMediumGamePad = true;
			isLargeGamePad = false;
		}
		if (MotherCanvas.w > 380)
		{
			isSmallGamePad = false;
			isMediumGamePad = false;
			isLargeGamePad = true;
		}
		if (!isLargeGamePad)
		{
			xZone = 0;
			wZone = MotherCanvas.hw;
			yZone = MotherCanvas.hh >> 1;
			hZone = MotherCanvas.h - 80;
		}
		else
		{
			xZone = 0;
			wZone = MotherCanvas.hw / 4 * 3 - 20;
			yZone = MotherCanvas.hh >> 1;
			hZone = MotherCanvas.h;
		}
	}

	public void update()
	{
	}

	private bool checkPointerMove(int distance)
	{
		return true;
	}

	private void resetHold()
	{
		GameCanvas.clearKeyHold();
	}

	public void paint(mGraphics g)
	{
	}

	public bool disableCheckDrag()
	{
		return isGamePad;
	}

	public bool disableClickMove()
	{
		return false;
	}
}
