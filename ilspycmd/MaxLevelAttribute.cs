public class MaxLevelAttribute
{
	public int value;

	public int maxValue;

	public int Id;

	public string name;

	public MaxLevelAttribute(int Id, string name, int value, int maxValue)
	{
		this.Id = Id;
		this.name = name;
		this.value = value;
		this.maxValue = maxValue;
	}
}
