public class FrameEff
{
	public mVector listPartTop = new mVector();

	public mVector listPartBottom = new mVector();

	public sbyte xShadow;

	public sbyte yShadow;

	public FrameEff(mVector listtop, mVector listbottom)
	{
		listPartTop = listtop;
		listPartBottom = listbottom;
	}

	public mVector getListPartPaint()
	{
		mVector mVector2 = new mVector();
		for (int i = 0; i < listPartBottom.size(); i++)
		{
			mVector2.addElement(listPartBottom.elementAt(i));
		}
		for (int j = 0; j < listPartTop.size(); j++)
		{
			mVector2.addElement(listPartTop.elementAt(j));
		}
		return mVector2;
	}
}
