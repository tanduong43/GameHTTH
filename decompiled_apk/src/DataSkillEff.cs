using System;

public class DataSkillEff
{
	public sbyte Frame;

	public sbyte f;

	public long timeremove;

	public long timeGetBack;

	public mVector listFrame = new mVector();

	public mVector listAnima = new mVector();

	public SmallImage[] smallImage;

	public sbyte[][] frameChar = new sbyte[4][];

	public sbyte[] sequence;

	public int fw;

	public int fh;

	public sbyte[] indexSplash = new sbyte[4];

	private int loop;

	public sbyte waitLoop;

	public bool isLoadData;

	public short idEff;

	public bool canremove;

	public static MyHashTable ALL_EFF_DATA = new MyHashTable();

	public static MyHashTable ALL_IMAGE_EFF_DATA = new MyHashTable();

	public sbyte typeupdate;

	public const sbyte NORMAL = 0;

	public const sbyte REMOVE_BY_FRAME = 1;

	public const sbyte REMOVE_BY_TIME = 2;

	public const sbyte NO_REMOVE = 3;

	public int x;

	public int y;

	public sbyte typeMove;

	public const sbyte CHOANG = 1;

	public const sbyte BIENHINH = 2;

	public bool isbuff;

	private int min;

	public bool wantDestroy;

	private long lasttime;

	private long timelive;

	public DataSkillEff(short idEff, sbyte[] data)
	{
	}

	public DataSkillEff(short idEff, int time)
	{
		this.idEff = idEff;
		switch (time)
		{
		case -1:
			typeupdate = 3;
			break;
		case 0:
			typeupdate = 1;
			break;
		default:
			typeupdate = 2;
			timelive = mSystem.currentTimeMillis() + time;
			break;
		}
		load();
	}

	public DataSkillEff(short idEff, int time, sbyte typemove, sbyte loop)
	{
		this.idEff = idEff;
		typeMove = typemove;
		waitLoop = loop;
		switch (time)
		{
		case -1:
			typeupdate = 3;
			break;
		case 0:
			typeupdate = 1;
			break;
		default:
			typeupdate = 2;
			timelive = mSystem.currentTimeMillis() + time;
			break;
		}
		load();
	}

	public DataSkillEff(short ideff, int x, int y)
	{
		idEff = ideff;
		this.x = x;
		this.y = y;
		typeupdate = 1;
		load();
	}

	public DataSkillEff(sbyte[] array)
	{
		loadData(array);
	}

	public void loadData(sbyte[] array)
	{
		if (array == null)
		{
			return;
		}
		DataInputStream dataInputStream = null;
		try
		{
			bool flag = true;
			listFrame.removeAllElements();
			smallImage = null;
			dataInputStream = new DataInputStream(new ByteArrayInputStream(array));
			int num = dataInputStream.readByte();
			smallImage = new SmallImage[num];
			for (int i = 0; i < num; i++)
			{
				smallImage[i] = new SmallImage(dataInputStream.readUnsignedByte(), dataInputStream.readUnsignedByte(), dataInputStream.readUnsignedByte(), dataInputStream.readUnsignedByte(), dataInputStream.readUnsignedByte());
			}
			int num2 = 0;
			int num3 = 10000;
			int num4 = dataInputStream.readShort();
			for (int j = 0; j < num4; j++)
			{
				sbyte b = dataInputStream.readByte();
				mVector mVector2 = new mVector();
				mVector mVector3 = new mVector();
				for (int k = 0; k < b; k++)
				{
					PartFrame partFrame = new PartFrame(dataInputStream.readShort(), dataInputStream.readShort(), dataInputStream.readByte());
					if (flag)
					{
						partFrame.flip = dataInputStream.readByte();
						partFrame.onTop = dataInputStream.readByte();
					}
					if (partFrame.onTop == 0)
					{
						mVector2.addElement(partFrame);
					}
					else
					{
						mVector3.addElement(partFrame);
					}
					if (num2 < CRes.abs(partFrame.dy))
					{
						num2 = CRes.abs(partFrame.dy);
					}
					if (CRes.abs(partFrame.dy) < num3)
					{
						num3 = CRes.abs(partFrame.dy);
					}
				}
				listFrame.addElement(new FrameEff(mVector2, mVector3));
			}
			fw = smallImage[0].w;
			min = num3;
			fh = (short)num2;
			short num5 = 0;
			num5 = ((!flag) ? dataInputStream.readShort() : ((short)dataInputStream.readUnsignedByte()));
			sequence = new sbyte[num5];
			for (int l = 0; l < num5; l++)
			{
				sequence[l] = (sbyte)dataInputStream.readShort();
			}
			if (flag)
			{
				dataInputStream.readByte();
				num5 = dataInputStream.readByte();
				frameChar[0] = new sbyte[num5];
				for (int m = 0; m < num5; m++)
				{
					frameChar[0][m] = dataInputStream.readByte();
				}
				num5 = dataInputStream.readByte();
				frameChar[1] = new sbyte[num5];
				for (int n = 0; n < num5; n++)
				{
					frameChar[1][n] = dataInputStream.readByte();
				}
				num5 = dataInputStream.readByte();
				frameChar[3] = new sbyte[num5];
				for (int num6 = 0; num6 < num5; num6++)
				{
					frameChar[3][num6] = dataInputStream.readByte();
				}
			}
			isLoadData = true;
			try
			{
				indexSplash[0] = (sbyte)(frameChar[0].Length - 7);
				indexSplash[1] = (sbyte)(frameChar[1].Length - 7);
				indexSplash[2] = (sbyte)(frameChar[3].Length - 7);
				indexSplash[3] = (sbyte)(frameChar[3].Length - 7);
			}
			catch (Exception)
			{
			}
			indexSplash[0] = dataInputStream.readByte();
			indexSplash[1] = dataInputStream.readByte();
			indexSplash[2] = dataInputStream.readByte();
			indexSplash[3] = indexSplash[2];
		}
		catch (Exception)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception)
			{
			}
		}
	}

	public void load()
	{
		EffectData effectData = (EffectData)ALL_EFF_DATA.get(idEff + string.Empty);
		if (effectData == null)
		{
			sbyte[] array = CRes.loadRMS("DataSkillEff" + idEff);
			if (array != null)
			{
				effectData = readData(array, isSave: false);
			}
		}
		if (effectData == null)
		{
			effectData = new EffectData();
			ALL_EFF_DATA.put(idEff + string.Empty, effectData);
			GlobalService.gI().getDataSkillEFf(0, idEff);
			mSystem.outz("[EFF Client] Requesting effect data for id=" + idEff);
		}
		if (effectData != null && effectData.data != null)
		{
			effectData.count = GameCanvas.timeNow / 1000;
			loadData(effectData.data);
			isLoadData = true;
			mSystem.outz("[EFF Client] Loaded effect data for id=" + idEff + " frames=" + listFrame.size());
		}
	}

	public bool isHavedata()
	{
		if (isLoadData)
		{
			return true;
		}
		load();
		return false;
	}

	public void paintTopEff(mGraphics g, int x, int y, int hOne)
	{
		if (!isHavedata() || (typeupdate == 3 && Frame == -1) || Frame >= listFrame.size())
		{
			return;
		}
		FrameEff frameEff = (FrameEff)listFrame.elementAt(Frame);
		try
		{
			mVector listPartTop = frameEff.listPartTop;
			for (int i = 0; i < listPartTop.size(); i++)
			{
				PartFrame partFrame = (PartFrame)listPartTop.elementAt(i);
				SmallImage smallImage = this.smallImage[partFrame.idSmallImg];
				mImage image = getImage();
				if (image != null && image.image != null)
				{
					int dx = partFrame.dx;
					int w = smallImage.w;
					int h = smallImage.h;
					int x2 = smallImage.x;
					int y2 = smallImage.y;
					int num = 0;
					if (hOne == 62 && min >= 50)
					{
						num = -8;
					}
					g.drawRegion(image, x2, y2, w, h, (partFrame.flip == 1) ? 2 : 0, x + dx, y + partFrame.dy + num, 0);
				}
			}
		}
		catch (Exception)
		{
			mSystem.println(" loi tai e  " + idEff);
		}
	}

	public void paintTopEff(mGraphics g)
	{
		if (!isHavedata() || (typeupdate == 3 && Frame == -1) || Frame >= listFrame.size())
		{
			return;
		}
		FrameEff frameEff = (FrameEff)listFrame.elementAt(Frame);
		try
		{
			mVector listPartTop = frameEff.listPartTop;
			for (int i = 0; i < listPartTop.size(); i++)
			{
				PartFrame partFrame = (PartFrame)listPartTop.elementAt(i);
				SmallImage smallImage = this.smallImage[partFrame.idSmallImg];
				mImage image = getImage();
				if (image != null && image.image != null)
				{
					int dx = partFrame.dx;
					int w = smallImage.w;
					int h = smallImage.h;
					int x = smallImage.x;
					int y = smallImage.y;
					g.drawRegion(image, x, y, w, h, (partFrame.flip == 1) ? 2 : 0, this.x + dx, this.y + partFrame.dy, 0);
				}
			}
		}
		catch (Exception)
		{
			mSystem.println(" loi tai e  " + idEff);
		}
	}

	public void paint(mGraphics g)
	{
		if (!isHavedata() || Frame >= listFrame.size())
		{
			return;
		}
		FrameEff frameEff = (FrameEff)listFrame.elementAt(Frame);
		try
		{
			mVector listPartPaint = frameEff.getListPartPaint();
			for (int i = 0; i < listPartPaint.size(); i++)
			{
				PartFrame partFrame = (PartFrame)listPartPaint.elementAt(i);
				SmallImage smallImage = this.smallImage[partFrame.idSmallImg];
				mImage image = getImage();
				if (image != null && image.image != null)
				{
					int dx = partFrame.dx;
					int w = smallImage.w;
					int h = smallImage.h;
					int x = smallImage.x;
					int y = smallImage.y;
					g.drawRegion(image, x, y, w, h, (partFrame.flip == 1) ? 2 : 0, this.x + dx, this.y + partFrame.dy, 0);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void paintBottomEff(mGraphics g, int x, int y, int hOne)
	{
		if (!isHavedata() || (typeupdate == 3 && Frame == -1) || Frame >= listFrame.size())
		{
			return;
		}
		FrameEff frameEff = (FrameEff)listFrame.elementAt(Frame);
		try
		{
			mVector listPartBottom = frameEff.listPartBottom;
			for (int i = 0; i < listPartBottom.size(); i++)
			{
				PartFrame partFrame = (PartFrame)listPartBottom.elementAt(i);
				SmallImage smallImage = this.smallImage[partFrame.idSmallImg];
				mImage image = getImage();
				if (image != null && image.image != null)
				{
					int dx = partFrame.dx;
					int w = smallImage.w;
					int h = smallImage.h;
					int x2 = smallImage.x;
					int y2 = smallImage.y;
					int num = 0;
					if (hOne == 62 && min >= 50)
					{
						num = -8;
					}
					g.drawRegion(image, x2, y2, w, h, (partFrame.flip == 1) ? 2 : 0, x + dx, y + partFrame.dy + num, 0);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void paintBottomEff(mGraphics g)
	{
		if (!isHavedata() || (typeupdate == 3 && Frame == -1) || Frame >= listFrame.size())
		{
			return;
		}
		FrameEff frameEff = (FrameEff)listFrame.elementAt(Frame);
		try
		{
			mVector listPartBottom = frameEff.listPartBottom;
			for (int i = 0; i < listPartBottom.size(); i++)
			{
				PartFrame partFrame = (PartFrame)listPartBottom.elementAt(i);
				SmallImage smallImage = this.smallImage[partFrame.idSmallImg];
				mImage image = getImage();
				if (image != null && image.image != null)
				{
					int dx = partFrame.dx;
					int w = smallImage.w;
					int h = smallImage.h;
					int x = smallImage.x;
					int y = smallImage.y;
					g.drawRegion(image, x, y, w, h, (partFrame.flip == 1) ? 2 : 0, this.x + dx, this.y + partFrame.dy, 0);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void setFrame(int fr)
	{
		Frame = (sbyte)fr;
	}

	public mImage getImage()
	{
		EffectData obj = (EffectData)ALL_EFF_DATA.get(idEff + string.Empty);
		obj.count = GameCanvas.timeNow / 1000;
		return obj.img;
	}

	public static EffectData readData(sbyte[] dataeff, bool isSave)
	{
		EffectData effectData = null;
		try
		{
			DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(dataeff));
			short id = dataInputStream.readShort();
			short num = dataInputStream.readShort();
			sbyte[] data = new sbyte[num];
			dataInputStream.read(ref data);
			sbyte[] data2 = new sbyte[dataInputStream.available()];
			dataInputStream.read(ref data2);
			effectData = (EffectData)ALL_EFF_DATA.get(id + string.Empty);
			if (effectData == null)
			{
				effectData = new EffectData();
				ALL_EFF_DATA.put(id + string.Empty, effectData);
			}
			effectData.setData(data, data2);
			effectData.count = GameCanvas.timeNow / 1000;
			if (isSave)
			{
				saveDataSkillEff(dataeff, id);
			}
			mSystem.outz("[EFF Client] readData OK for id=" + id + ", dataLen=" + num + ", imgLen=" + data2.Length);
		}
		catch (Exception ex)
		{
			mSystem.outz("[EFF Client Error] readData failed: " + ex.Message);
		}
		return effectData;
	}

	public static void saveDataSkillEff(sbyte[] dataSave, short id)
	{
		try
		{
			CRes.saveRMS("DataSkillEff" + id, dataSave);
		}
		catch (Exception)
		{
		}
	}

	public void setLoop(int loop)
	{
		this.loop = loop;
	}

	public void update()
	{
		if (!isHavedata() || listFrame.size() <= 0)
		{
			return;
		}
		try
		{
			switch (typeupdate)
			{
			case 0:
				f++;
				if (f >= sequence.Length)
				{
					if (!canremove)
					{
						wantDestroy = true;
					}
					f = 0;
				}
				Frame = sequence[f];
				break;
			case 1:
				f++;
				if (f >= sequence.Length)
				{
					f = 0;
					wantDestroy = true;
				}
				Frame = sequence[f];
				break;
			case 2:
				f++;
				if (f >= sequence.Length)
				{
					f = 0;
				}
				if (timelive - mSystem.currentTimeMillis() < 0)
				{
					wantDestroy = true;
				}
				Frame = sequence[f];
				break;
			case 3:
				f++;
				if (f >= sequence.Length)
				{
					if (waitLoop > 0)
					{
						if (mSystem.currentTimeMillis() - lasttime > waitLoop * 1000)
						{
							lasttime = mSystem.currentTimeMillis();
							f = 0;
						}
					}
					else
					{
						f = 0;
					}
				}
				if (f < sequence.Length)
				{
					Frame = sequence[f];
				}
				else
				{
					Frame = -1;
				}
				break;
			}
		}
		catch (Exception ex)
		{
			mSystem.outz("[EFF Client Error] update eff failed: " + ex.Message);
		}
	}
}
