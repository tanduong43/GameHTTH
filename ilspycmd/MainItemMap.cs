public class MainItemMap : MainObject
{
	public sbyte TypeItem;

	public sbyte layer;

	public const sbyte ITEM_MAP = 0;

	public const sbyte EFF_MAP = 1;

	public const sbyte BOAT_MAP = 1;

	public short IDItem;

	public short IDImage;

	public int[][] Block;

	public mImage img;

	public bool isLoadDataOk = true;

	public MainItemMap()
	{
	}

	public MainItemMap(short ID)
	{
		IDItem = ID;
		Block = mSystem.new_M_Int(0, 2);
		IDImage = -1;
		layer = -1;
		dx = 0;
		dy = 0;
		isLoadDataOk = false;
	}

	public MainItemMap(short IDItem, short IDImage, int dx, int dy, int[][] Block, sbyte layer)
	{
		this.IDItem = IDItem;
		this.IDImage = IDImage;
		base.dx = dx;
		base.dy = dy;
		this.Block = Block;
		this.layer = layer;
		isLoadDataOk = true;
	}

	public override void paint(mGraphics g)
	{
	}

	public override void update()
	{
	}

	public virtual bool isInScreen()
	{
		if ((hOne == 0 || wOne == 0) && TypeItem != 1)
		{
			MainImage imageAll = ObjectData.getImageAll(IDImage, ObjectData.HashImageItemMap, 0);
			if (imageAll.img != null)
			{
				wOne = mImage.getImageWidth(imageAll.img.image);
				hOne = mImage.getImageHeight(imageAll.img.image);
			}
		}
		if (x + dx + wOne < MainScreen.cameraMain.xCam || x + dx - wOne > MainScreen.cameraMain.xCam + MotherCanvas.w || y + dy + hOne < MainScreen.cameraMain.yCam || y + dy - hOne > MainScreen.cameraMain.yCam + MotherCanvas.h)
		{
			return false;
		}
		return true;
	}
}
