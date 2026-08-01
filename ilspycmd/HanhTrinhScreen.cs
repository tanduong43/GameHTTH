public class HanhTrinhScreen : MainScreen
{
	public string nameList = string.Empty;

	public static HanhTrinhScreen instance;

	protected int x;

	protected int y;

	protected int w;

	protected int h;

	private static int[][] mpos = new int[12][]
	{
		new int[2] { 50, 55 },
		new int[2] { 60, 105 },
		new int[2] { 45, 155 },
		new int[2] { 90, 175 },
		new int[2] { 110, 125 },
		new int[2] { 100, 75 },
		new int[2] { 150, 45 },
		new int[2] { 210, 55 },
		new int[2] { 165, 95 },
		new int[2] { 210, 125 },
		new int[2] { 160, 155 },
		new int[2] { 210, 180 }
	};

	private FrameImage fraNen;

	private iCommand cmdClose;

	public mVector vecCmd = new mVector();

	public string[] mTenLang;

	public short[] mIcon;

	public HanhTrinhScreen()
	{
		w = 260;
		h = 215;
		x = MotherCanvas.hw - w / 2;
		y = MotherCanvas.hh - h / 2;
		cmdClose = new iCommand(T.close, -1, this);
		vecCmd.addElement(cmdClose);
		if (GameCanvas.isTouch)
		{
			cmdClose.setPos(x + w - 13, y + 13, MainTab.fraCloseTab, string.Empty);
		}
		else
		{
			right = cmdClose;
		}
	}

	public override void paint(mGraphics g)
	{
		if (lastScreen != null)
		{
			lastScreen.paint(g);
		}
		GameCanvas.resetTrans(g);
		AvMain.paintBG_AChi(g, x, y, w, h, 0);
		AvMain.FontBorderColor(g, nameList, MotherCanvas.hw, y - 20, 2, 6, 5);
		for (int i = 1; i < mpos.Length; i++)
		{
			g.setColor(16711680);
			g.drawLine(x + mpos[i][0], y + mpos[i][1], x + mpos[i - 1][0], y + mpos[i - 1][1]);
		}
		for (int j = 0; j < mpos.Length; j++)
		{
			if (fraNen == null)
			{
				fraNen = new FrameImage(mImage.createImage("/interface/kham_hanh_trinh.png"), 2);
			}
			else if (mIcon[j] == -1)
			{
				fraNen.drawFrame(1, x + mpos[j][0], y + mpos[j][1], 0, 3, g);
			}
			else
			{
				fraNen.drawFrame(0, x + mpos[j][0], y + mpos[j][1], 0, 3, g);
			}
			if (mIcon[j] != -1)
			{
				MainImage imageAll = ObjectData.getImageAll(mIcon[j], ObjectData.hashImagePotion, 2000);
				if (imageAll != null && imageAll.img != null)
				{
					g.drawImage(imageAll.img, x + mpos[j][0], y + mpos[j][1], 3);
				}
			}
			if (mpos[j][1] >= 175)
			{
				mFont.tahoma_7_black.drawString(g, mTenLang[j], x + mpos[j][0], y + mpos[j][1] + 13, 2);
			}
			else
			{
				mFont.tahoma_7_black.drawString(g, mTenLang[j], x + mpos[j][0], y + mpos[j][1] - 23, 2);
			}
		}
		for (int k = 0; k < vecCmd.size(); k++)
		{
			iCommand iCommand2 = (iCommand)vecCmd.elementAt(k);
			iCommand2.paint(g, iCommand2.xCmd, iCommand2.yCmd);
		}
	}

	public override void commandPointer(int index, int subIndex)
	{
		if (index == -1)
		{
			if (lastScreen != null)
			{
				lastScreen.Show(lastScreen.lastScreen);
			}
			else
			{
				GameCanvas.gameScr.Show();
			}
		}
	}

	public override void updatePointer()
	{
		if (vecCmd != null)
		{
			for (int i = 0; i < vecCmd.size(); i++)
			{
				iCommand iCommand2 = (iCommand)vecCmd.elementAt(i);
				iCommand2.updatePointer();
			}
		}
		base.updatePointer();
	}
}
