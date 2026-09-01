public class FrameImage
{
	public int frameWidth;

	public int frameHeight;

	public int nFrame = 1;

	public int maxNumFrame = 1;

	public int indexSuper;

	public mImage imgFrame;

	private int Id = -1;

	private bool lowG;

	private bool isFormFrame;

	public FrameImage(mImage img, int width, int height)
	{
		imgFrame = img;
		frameWidth = width;
		frameHeight = height;
		nFrame = mImage.getImageHeight(img.image) / height;
	}

	public FrameImage(mImage img, int width, int height, int maxNumFrame)
	{
		imgFrame = img;
		frameWidth = width;
		frameHeight = height;
		this.maxNumFrame = maxNumFrame;
		nFrame = mImage.getImageWidth(imgFrame.image) / width * maxNumFrame;
	}

	public FrameImage(mImage img, int width, int height, int maxNumFrame, sbyte frameSuper)
	{
		indexSuper = frameSuper;
		imgFrame = img;
		frameWidth = width;
		frameHeight = height;
		this.maxNumFrame = maxNumFrame;
		nFrame = mImage.getImageWidth(imgFrame.image) / width * maxNumFrame;
	}

	public FrameImage(int ID, int width, int height)
	{
		Id = ID;
		frameWidth = width;
		frameHeight = height;
		imgFrame = getImage();
		if (imgFrame != null && imgFrame.image != null)
		{
			nFrame = mImage.getImageHeight(imgFrame.image) / height;
			maxNumFrame = nFrame;
		}
	}

	public FrameImage(mImage img, int numframe)
	{
		imgFrame = img;
		nFrame = numframe;
		maxNumFrame = nFrame;
		frameWidth = mImage.getImageWidth(imgFrame.image);
		frameHeight = mImage.getImageHeight(imgFrame.image) / numframe;
	}

	public FrameImage(int ID, int numframe)
	{
		Id = ID;
		nFrame = numframe;
		maxNumFrame = nFrame;
		imgFrame = getImage();
		isFormFrame = true;
		if (imgFrame != null && imgFrame.image != null)
		{
			frameWidth = mImage.getImageWidth(imgFrame.image);
			frameHeight = mImage.getImageHeight(imgFrame.image) / numframe;
		}
	}

	public FrameImage(int ID, int width, int height, int width2, int height2)
	{
		Id = ID;
		if (GameCanvas.lowGraphic)
		{
			frameWidth = width2;
			frameHeight = height2;
			lowG = true;
		}
		else
		{
			frameWidth = width;
			frameHeight = height;
		}
		imgFrame = getImage();
		if (imgFrame != null && imgFrame.image != null)
		{
			nFrame = mImage.getImageHeight(imgFrame.image) / frameHeight;
			maxNumFrame = nFrame;
		}
	}

	public FrameImage(int ID, int width, int height, int maxNumFrame)
	{
		createFrameImgNew(ID, width, height, maxNumFrame);
	}

	public FrameImage(mImage img, int ID, int width, int height, int maxNumFrame)
	{
		Id = ID;
		frameWidth = width;
		frameHeight = height;
		this.maxNumFrame = maxNumFrame;
		imgFrame = img;
		if (imgFrame != null && imgFrame.image != null)
		{
			nFrame = mImage.getImageWidth(imgFrame.image) / width * maxNumFrame;
		}
	}

	public FrameImage(int ID, int width, int height, sbyte maxNumFrame, sbyte frameSuper)
	{
		indexSuper = frameSuper;
		createFrameImgNew(ID, width, height, maxNumFrame);
	}

	public FrameImage(int ID, int width, int height, int width2, int height2, int maxNumFrame)
	{
		Id = ID;
		if (GameCanvas.lowGraphic)
		{
			frameWidth = width2;
			frameHeight = height2;
			lowG = true;
		}
		else
		{
			frameWidth = width;
			frameHeight = height;
		}
		this.maxNumFrame = maxNumFrame;
		imgFrame = getImage();
		if (imgFrame != null && imgFrame.image != null)
		{
			nFrame = mImage.getImageWidth(imgFrame.image) / frameWidth * maxNumFrame;
		}
	}

	public void createFrameImgNew(int ID, int width, int height, int maxNumFrame)
	{
		Id = ID;
		frameWidth = width;
		frameHeight = height;
		this.maxNumFrame = maxNumFrame;
		imgFrame = getImage();
		if (imgFrame != null && imgFrame.image != null)
		{
			nFrame = mImage.getImageWidth(imgFrame.image) / width * maxNumFrame;
		}
	}

	public void drawFrame(int idx, int x, int y, int trans, int orthor, mGraphics g)
	{
		if (imgFrame == null)
		{
			imgFrame = getImage();
			if (imgFrame != null && imgFrame.image != null)
			{
				if (isFormFrame)
				{
					frameWidth = mImage.getImageWidth(imgFrame.image);
					frameHeight = mImage.getImageHeight(imgFrame.image) / nFrame;
				}
				else
				{
					nFrame = mImage.getImageHeight(imgFrame.image) / frameHeight;
					maxNumFrame = nFrame;
				}
			}
		}
		else if (idx >= 0 && idx < nFrame)
		{
			g.drawRegion(imgFrame, 0, idx * frameHeight, frameWidth, frameHeight, trans, x, y, orthor);
		}
	}

	public mImage getImageFrame()
	{
		if (imgFrame != null)
		{
			return imgFrame;
		}
		if (lowG)
		{
			imgFrame = ObjectData.getImageAll((short)Id, ObjectData.HashImageEffClientLow, 25000).img;
		}
		else
		{
			imgFrame = ObjectData.getImageAll((short)Id, ObjectData.HashImageEffClient, 24000).img;
		}
		return imgFrame;
	}

	public mImage getImage()
	{
		if (lowG)
		{
			return ObjectData.getImageAll((short)Id, ObjectData.HashImageEffClientLow, 25000).img;
		}
		return ObjectData.getImageAll((short)Id, ObjectData.HashImageEffClient, 24000).img;
	}

	public void drawFrameNew_BeginSuper(int idx, int x, int y, int trans, int orthor, mGraphics g)
	{
		int num = idx + indexSuper * maxNumFrame;
		if (imgFrame == null)
		{
			imgFrame = getImage();
			if (imgFrame != null && imgFrame.image != null)
			{
				if (isFormFrame)
				{
					frameWidth = mImage.getImageWidth(imgFrame.image);
					frameHeight = mImage.getImageHeight(imgFrame.image) / nFrame;
				}
				else
				{
					nFrame = mImage.getImageWidth(imgFrame.image) / frameWidth * maxNumFrame;
				}
			}
		}
		else if (num >= 0 && num < nFrame)
		{
			g.drawRegion(imgFrame, num / maxNumFrame * frameWidth, num % maxNumFrame * frameHeight, frameWidth, frameHeight, trans, x, y, orthor);
		}
	}

	public void drawFrameNew(int idx, int x, int y, int trans, int orthor, mGraphics g)
	{
		if (imgFrame == null)
		{
			imgFrame = getImage();
			if (imgFrame != null && imgFrame.image != null)
			{
				if (isFormFrame)
				{
					frameWidth = mImage.getImageWidth(imgFrame.image);
					frameHeight = mImage.getImageHeight(imgFrame.image) / nFrame;
				}
				else
				{
					nFrame = mImage.getImageWidth(imgFrame.image) / frameWidth * maxNumFrame;
				}
			}
		}
		else if (idx >= 0 && idx < nFrame)
		{
			g.drawRegion(imgFrame, idx / maxNumFrame * frameWidth, idx % maxNumFrame * frameHeight, frameWidth, frameHeight, trans, x, y, orthor);
		}
	}
}
