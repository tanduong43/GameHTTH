public class Message
{
	public sbyte command;

	private myReader dis;

	private myWriter dos;

	public byte isOld;

	public Message(int command)
	{
		this.command = (sbyte)command;
		dos = new myWriter();
	}

	public Message()
	{
		dos = new myWriter();
	}

	public Message(sbyte command)
	{
		this.command = command;
		dos = new myWriter();
	}

	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		dis = new myReader(data);
	}

	public sbyte[] getData()
	{
		return dos.getData();
	}

	public myReader reader()
	{
		return dis;
	}

	public myWriter writer()
	{
		return dos;
	}

	public int readInt3Byte()
	{
		int num = dis.readByte() + 128;
		int num2 = dis.readByte() + 128;
		int num3 = dis.readByte() + 128;
		return (num3 * 256 + num2) * 256 + num;
	}

	public void cleanup()
	{
	}
}
