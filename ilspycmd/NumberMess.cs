public class NumberMess
{
	public int yNum;

	public int vNum;

	public bool isUpdatevNum;

	public void update()
	{
		if (yNum == 0)
		{
			if (CRes.random(50) == 0)
			{
				vNum = -CRes.random(2, 4);
				isUpdatevNum = true;
			}
		}
		else if (yNum > 0)
		{
			isUpdatevNum = false;
			vNum = 0;
			yNum = 0;
		}
		if (isUpdatevNum || yNum != 0)
		{
			yNum += vNum;
		}
		vNum++;
	}
}
