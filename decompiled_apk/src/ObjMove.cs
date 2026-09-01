public class ObjMove
{
	public sbyte typeObj;

	public short id;

	public short x;

	public short y;

	public bool isRemove;

	public ObjMove(sbyte cat, short id, short x, short y)
	{
		typeObj = cat;
		this.id = id;
		this.x = x;
		this.y = y;
	}
}
