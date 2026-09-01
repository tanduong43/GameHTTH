using System;

public class EffectAuto : MainItemMap
{
	private MainEffectAuto eff;

	private int nCountReplay;

	private bool isPaint = true;

	private int typeEffect;

	private int valueEffect;

	private new long timeBegin;

	private int indexSound = -1;

	private int timePlaySound = -1;

	private int tickupdate;

	public static long timeSoundFIRE;

	public static long timeSoundWATER;

	public static long timeSoundPHUNNUOC;

	public static long timeSoundGIOTNUOC;

	public EffectAuto(string keystr, string value)
	{
		TypeItem = 1;
		string[] array = mFont.split(value, ";");
		IDItem = short.Parse(array[0]);
		IDImage = short.Parse(array[1]);
		x = int.Parse(array[2]) * LoadMap.wTile;
		y = int.Parse(array[3]) * LoadMap.wTile;
		dx = int.Parse(array[4]);
		dy = int.Parse(array[5]);
		typeEffect = int.Parse(array[6]);
		valueEffect = int.Parse(array[7]);
		wOne = 70;
		hOne = 70;
		ySort = y;
		setDataEff();
	}

	public void setDataEff()
	{
		eff = loadTemEff(IDImage);
		timeBegin = GameCanvas.timeNow;
		isPaint = true;
		setPlaySound();
	}

	public override void paint(mGraphics g)
	{
		if (eff == null || eff.img == null || !isPaint)
		{
			return;
		}
		int num = eff.mRunFrame[f];
		int num2 = eff.mFrame[num].mpart.Length;
		for (int i = 0; i < num2; i++)
		{
			MainPartImage mainPartImage = (MainPartImage)eff.hashImage.get(string.Empty + eff.mFrame[num].mpart[i].idPartImage);
			if (mainPartImage != null)
			{
				g.drawRegion(eff.img, mainPartImage.x, mainPartImage.y, mainPartImage.w, mainPartImage.h, 0, x + dx + eff.mFrame[num].mpart[i].x, y + dy + eff.mFrame[num].mpart[i].y, 0);
			}
		}
	}

	public override void update()
	{
		if (eff == null || eff.img == null)
		{
			tickupdate++;
			if (tickupdate > 50)
			{
				tickupdate = 0;
				eff = loadTemEff(IDImage);
			}
			return;
		}
		if (f >= eff.mRunFrame.Length - 1)
		{
			switch (typeEffect)
			{
			case 0:
				nCountReplay++;
				isPaint = false;
				if (nCountReplay >= valueEffect)
				{
					nCountReplay = 0;
					isPaint = true;
					f = 0;
				}
				break;
			case 1:
				f = 0;
				break;
			case 2:
				isPaint = false;
				if (GameCanvas.gameTick % 5 == 0 && (GameCanvas.timeNow - timeBegin) / 1000 > valueEffect)
				{
					timeBegin = GameCanvas.timeNow;
					f = 0;
					isPaint = true;
				}
				break;
			case 4:
				if (CRes.random(valueEffect) == 0)
				{
					f = 0;
					if (indexSound >= 0 && CRes.random(timePlaySound) == 0 && isInScreen())
					{
						mSound.playSound(indexSound, mSound.volumeSound);
					}
				}
				break;
			}
		}
		else
		{
			f++;
		}
		setBeginSound();
	}

	private void setBeginSound()
	{
	}

	public MainEffectAuto loadTemEff(short id)
	{
		MainEffectAuto mainEffectAuto = (MainEffectAuto)MainEffectAuto.hashTemEffAuto.get(string.Empty + id);
		if (mainEffectAuto == null)
		{
			mainEffectAuto = checkLoadRMS(id);
			MainEffectAuto.hashTemEffAuto.put(id + string.Empty, mainEffectAuto);
		}
		return mainEffectAuto;
	}

	public MainEffectAuto checkLoadRMS(short id)
	{
		sbyte[] array = null;
		MainEffectAuto result = new MainEffectAuto();
		if (GameMidlet.DEVICE != 0)
		{
			array = CRes.loadRMS("SUB_dataeffauto" + id);
		}
		if (array == null)
		{
			GlobalService.gI().getDataEffAuto(id);
		}
		else
		{
			result = readData(array, isSave: false);
		}
		return result;
	}

	public static MainEffectAuto readData(sbyte[] dataeff, bool isSave)
	{
		MainEffectAuto mainEffectAuto = null;
		try
		{
			DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(dataeff));
			short id = dataInputStream.readShort();
			sbyte[] data = new sbyte[dataInputStream.readShort()];
			dataInputStream.read(ref data);
			DataInputStream data2 = new DataInputStream(new ByteArrayInputStream(data));
			sbyte[] data3 = new sbyte[dataInputStream.available()];
			dataInputStream.read(ref data3);
			mainEffectAuto = new MainEffectAuto(data2, data3);
			MainEffectAuto.hashTemEffAuto.put(id + string.Empty, mainEffectAuto);
			if (GameMidlet.DEVICE != 0 && isSave)
			{
				saveDataEffAuto(dataeff, id);
			}
		}
		catch (Exception)
		{
		}
		return mainEffectAuto;
	}

	public static void saveDataEffAuto(sbyte[] dataSave, short id)
	{
		try
		{
			CRes.saveRMS("SUB_dataeffauto" + id, dataSave);
		}
		catch (Exception)
		{
		}
	}

	public void setPlaySound()
	{
	}
}
