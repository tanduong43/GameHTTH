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
		GameObject gameObject = new GameObject();
		gameObject.name = "Audio Player";
		gameObject.transform.position = Vector3.zero;
		gameObject.AddComponent<AudioListener>();
		SoundBGLoop = gameObject.AddComponent<AudioSource>();
	}

	public static void init(int musicID, int sID)
	{
		if (player == null && music == null)
		{
			init();
			l1 = musicID;
			player = new GameObject[musicID + sID];
			music = new AudioClip[musicID + sID];
			for (int i = 0; i < player.Length; i++)
			{
				string fileName = ((i >= l1) ? ("/sound/s" + (i - l1)) : ("/sound/m" + i));
				getAssetSoundFile(fileName, i);
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
		string empty = string.Empty;
		empty = Main.res + fileName;
		load(empty, pos);
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
		if (!(SoundRun == null))
		{
			SoundRun.GetComponent<AudioSource>().loop = true;
			SoundRun.GetComponent<AudioSource>().clip = music[id];
			SoundRun.GetComponent<AudioSource>().volume = volume;
			SoundRun.GetComponent<AudioSource>().Play();
		}
	}

	public static void sTopSoundRun()
	{
		SoundRun.GetComponent<AudioSource>().Stop();
	}

	public static bool isPlayingSound()
	{
		if (SoundRun == null)
		{
			return false;
		}
		return SoundRun.GetComponent<AudioSource>().isPlaying;
	}

	public static void playSoundNatural(int id, float volume, bool isLoop)
	{
		if (!(SoundBGLoop == null))
		{
			SoundWater.GetComponent<AudioSource>().loop = isLoop;
			SoundWater.GetComponent<AudioSource>().clip = music[id];
			SoundWater.GetComponent<AudioSource>().volume = volume;
			SoundWater.GetComponent<AudioSource>().Play();
		}
	}

	public static void stopSoundNatural(int id)
	{
		SoundWater.GetComponent<AudioSource>().Stop();
	}

	public static bool isPlayingSoundatural(int id)
	{
		if (SoundWater == null)
		{
			return false;
		}
		return SoundWater.GetComponent<AudioSource>().isPlaying;
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
		if (!(SoundBGLoop == null) && id != idCurent)
		{
			SoundBGLoop.GetComponent<AudioSource>().loop = true;
			SoundBGLoop.GetComponent<AudioSource>().clip = music[id];
			SoundBGLoop.GetComponent<AudioSource>().volume = volume;
			SoundBGLoop.GetComponent<AudioSource>().Play();
			idCurent = id;
		}
	}

	public static void sTopSoundBG(int id)
	{
		SoundBGLoop.GetComponent<AudioSource>().Stop();
	}

	public static bool isPlayingSoundBG(int id)
	{
		if (SoundBGLoop == null)
		{
			return false;
		}
		return SoundBGLoop.GetComponent<AudioSource>().isPlaying;
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
			Debug.LogError("CANNOT START AUDIO WHEN STARTING");
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
			Debug.LogError("TOO LONG FOR START AUDIO");
		}
		else
		{
			Debug.Log("Start Audio done in " + i * 5 + "ms");
		}
	}

	public static void __start(float volume, int pos)
	{
		if (!(player[pos] == null))
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
			Debug.LogError("CANNOT STOP AUDIO WHEN STOPPING");
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
			Debug.LogError("TOO LONG FOR STOP AUDIO");
		}
		else
		{
			Debug.Log("Stop Audio done in " + i * 5 + "ms");
		}
	}

	public static void __stop(int pos)
	{
		if (player[pos] != null)
		{
			player[pos].GetComponent<AudioSource>().Stop();
		}
	}
}
