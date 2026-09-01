using System.Threading;
using UnityEngine;

public class mSound
{
	private const int INTERVAL = 5;

	private const int MAXTIME = 100;

	public static int status;

	public static int postem;

	public static int timestart;

	private static string filenametemp;

	private static float volumetem;

	public static bool isSound = true;

	public static bool isMusic = true;

	public static bool isNotPlay;

	public static AudioSource SoundWater;

	public static AudioSource SoundRun;

	public static AudioSource SoundBGLoop;

	public static float volumeSound = 0.7f;

	public static float volumeMusic = 0.8f;

	public static AudioClip[] music;

	public static GameObject[] player;

	public static int l1;

	public static int idCurent = -1;

	public static void stopAll()
	{
		stopAllz();
	}

	public static bool isPlaying()
	{
		return false;
	}

	public static void init()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject
		{
			name = "Audio Player"
		};
		val.transform.position = Vector3.zero;
		val.AddComponent<AudioListener>();
		SoundBGLoop = val.AddComponent<AudioSource>();
	}

	public static void init(int musicID, int sID)
	{
		if (player == null && music == null)
		{
			init();
			l1 = musicID;
			player = (GameObject[])(object)new GameObject[musicID + sID];
			music = (AudioClip[])(object)new AudioClip[musicID + sID];
			for (int i = 0; i < player.Length; i++)
			{
				getAssetSoundFile((i >= l1) ? ("/sound/s" + (i - l1)) : ("/sound/m" + i), i);
			}
		}
	}

	public static void playSound(int id, float volume)
	{
		if (isSound && id >= 0 && id <= music.Length - l1 - 1)
		{
			play(id + l1, volume);
		}
	}

	public static void playSound1(int id, float volume)
	{
		play(id, volume);
	}

	public static void getAssetSoundFile(string fileName, int pos)
	{
		stop(pos);
		_ = string.Empty;
		load(Main.res + fileName, pos);
	}

	public static void stopSoundAll()
	{
		for (int i = 0; i < music.Length; i++)
		{
			stop(i);
		}
	}

	public static void stopAllz()
	{
		for (int i = 0; i < music.Length; i++)
		{
			stop(i);
		}
		for (int j = 0; j < l1; j++)
		{
			sTopSoundBG(j);
		}
	}

	public static void stopAllBg()
	{
		for (int i = 0; i < music.Length; i++)
		{
			stop(i);
		}
		sTopSoundBG(0);
		sTopSoundRun();
		stopSoundNatural(0);
	}

	public static void update()
	{
	}

	public static void stopMusic(int x)
	{
		stop(x);
	}

	public static void play(int id, float volume)
	{
		start(volume, id);
	}

	public static void playSoundRun(int id, float volume)
	{
		if (!((Object)(object)SoundRun == (Object)null))
		{
			((Component)SoundRun).GetComponent<AudioSource>().loop = true;
			((Component)SoundRun).GetComponent<AudioSource>().clip = music[id];
			((Component)SoundRun).GetComponent<AudioSource>().volume = volume;
			((Component)SoundRun).GetComponent<AudioSource>().Play();
		}
	}

	public static void sTopSoundRun()
	{
		((Component)SoundRun).GetComponent<AudioSource>().Stop();
	}

	public static bool isPlayingSound()
	{
		if ((Object)(object)SoundRun == (Object)null)
		{
			return false;
		}
		return ((Component)SoundRun).GetComponent<AudioSource>().isPlaying;
	}

	public static void playSoundNatural(int id, float volume, bool isLoop)
	{
		if (!((Object)(object)SoundBGLoop == (Object)null))
		{
			((Component)SoundWater).GetComponent<AudioSource>().loop = isLoop;
			((Component)SoundWater).GetComponent<AudioSource>().clip = music[id];
			((Component)SoundWater).GetComponent<AudioSource>().volume = volume;
			((Component)SoundWater).GetComponent<AudioSource>().Play();
		}
	}

	public static void stopSoundNatural(int id)
	{
		((Component)SoundWater).GetComponent<AudioSource>().Stop();
	}

	public static bool isPlayingSoundatural(int id)
	{
		if ((Object)(object)SoundWater == (Object)null)
		{
			return false;
		}
		return ((Component)SoundWater).GetComponent<AudioSource>().isPlaying;
	}

	public static void playMus(int type, float vl, bool loop)
	{
		if (isMusic && type >= 0 && type <= l1 - 1)
		{
			playSoundBGLoop(type, vl);
		}
	}

	public static void playSoundBGLoop(int id, float volume)
	{
		if (!((Object)(object)SoundBGLoop == (Object)null) && id != idCurent)
		{
			((Component)SoundBGLoop).GetComponent<AudioSource>().loop = true;
			((Component)SoundBGLoop).GetComponent<AudioSource>().clip = music[id];
			((Component)SoundBGLoop).GetComponent<AudioSource>().volume = volume;
			((Component)SoundBGLoop).GetComponent<AudioSource>().Play();
			idCurent = id;
		}
	}

	public static void sTopSoundBG(int id)
	{
		((Component)SoundBGLoop).GetComponent<AudioSource>().Stop();
	}

	public static bool isPlayingSoundBG(int id)
	{
		if ((Object)(object)SoundBGLoop == (Object)null)
		{
			return false;
		}
		return ((Component)SoundBGLoop).GetComponent<AudioSource>().isPlaying;
	}

	public static void load(string filename, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			__load(filename, pos);
		}
		else
		{
			_load(filename, pos);
		}
	}

	private static void _load(string filename, int pos)
	{
		if (status != 0)
		{
			Cout.LogError("CANNOT LOAD AUDIO " + filename + " WHEN LOADING " + filenametemp);
			return;
		}
		filenametemp = filename;
		postem = pos;
		status = 2;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Cout.LogError("TOO LONG FOR LOAD AUDIO " + filename);
			return;
		}
		Cout.Log("Load Audio " + filename + " done in " + i * 5 + "ms");
	}

	private static void __load(string filename, int pos)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
		GameObject.Find("Main Camera").AddComponent<AudioSource>();
		player[pos] = GameObject.Find("Main Camera");
	}

	public static void start(float volume, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			__start(volume, pos);
		}
		else
		{
			_start(volume, pos);
		}
	}

	public static void _start(float volume, int pos)
	{
		if (status != 0)
		{
			Debug.LogError((object)"CANNOT START AUDIO WHEN STARTING");
			return;
		}
		volumetem = volume;
		postem = pos;
		status = 3;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError((object)"TOO LONG FOR START AUDIO");
		}
		else
		{
			Debug.Log((object)("Start Audio done in " + i * 5 + "ms"));
		}
	}

	public static void __start(float volume, int pos)
	{
		if (!((Object)(object)player[pos] == (Object)null))
		{
			player[pos].GetComponent<AudioSource>().PlayOneShot(music[pos], volume);
		}
	}

	public static void stop(int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			__stop(pos);
		}
		else
		{
			_stop(pos);
		}
	}

	public static void _stop(int pos)
	{
		if (status != 0)
		{
			Debug.LogError((object)"CANNOT STOP AUDIO WHEN STOPPING");
			return;
		}
		postem = pos;
		status = 4;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError((object)"TOO LONG FOR STOP AUDIO");
		}
		else
		{
			Debug.Log((object)("Stop Audio done in " + i * 5 + "ms"));
		}
	}

	public static void __stop(int pos)
	{
		if ((Object)(object)player[pos] != (Object)null)
		{
			player[pos].GetComponent<AudioSource>().Stop();
		}
	}
}
