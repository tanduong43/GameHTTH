using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static Main main;

	public static mGraphics g;

	public static GameMidlet midlet;

	public static string res = "res";

	public static string mainThreadName;

	public static bool started;

	public static bool isIpod;

	public static bool isIphone4;

	public static bool isWindowsPhone;

	public static bool isIPhone;

	public static bool IphoneVersionApp;

	public static string IMEI;

	public static int versionIp;

	public static int numberQuit = 1;

	public static int typeClient = 4;

	public const sbyte PC_VERSION = 4;

	public const sbyte IP_APPSTORE = 5;

	public const sbyte WINDOWSPHONE = 6;

	public const sbyte IP_JB = 3;

	private Queue<IEnumerator> jobs = new Queue<IEnumerator>();

	private int updateCount;

	private int paintCount;

	private int count;

	private bool isRun;

	public static int waitTick;

	public static int f;

	private int valueKey;

	public static bool isResume;

	public static bool isMiniApp = true;

	public static bool isQuitApp;

	private Vector2 lastMousePos;

	public static int a = 1;

	public bool localhost;

	public static bool isCompactDevice = true;

	private void Start()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Invalid comparison between Unknown and I4
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Invalid comparison between Unknown and I4
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Invalid comparison between Unknown and I4
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Invalid comparison between Unknown and I4
		if (started)
		{
			return;
		}
		Time.timeScale = 1.8f;
		if (Thread.CurrentThread.Name != "Main")
		{
			Thread.CurrentThread.Name = "Main";
		}
		mainThreadName = Thread.CurrentThread.Name;
		if ((int)Application.platform == 11 || (int)Application.platform == 8)
		{
			GameMidlet.isPC = false;
			if ((int)Application.platform == 11)
			{
				GameMidlet.DEVICE = 1;
			}
			else if ((int)Application.platform == 8)
			{
				GameMidlet.DEVICE = 5;
				IphoneVersionApp = true;
			}
		}
		else
		{
			GameMidlet.isPC = true;
			GameMidlet.DEVICE = 2;
		}
		started = true;
		GameCanvas.readGraphicsPC();
		if (GameMidlet.isPC)
		{
			if (GameCanvas.lv == 0)
			{
				Screen.SetResolution(600, 355, false);
			}
			else
			{
				Screen.SetResolution(1024, 550, false);
			}
		}
		else
		{
			Screen.fullScreen = true;
		}
	}

	private void SetInit()
	{
		((Behaviour)this).enabled = true;
	}

	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	private void OnGUI()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (count >= 10)
		{
			checkInput();
			Session_ME.update();
			if (((object)Event.current.type/*cast due to constrained. prefix*/).Equals((object)(EventType)7) && GameMidlet.gameCanvas != null)
			{
				GameMidlet.gameCanvas.paint(g);
				paintCount++;
				g.reset();
			}
		}
	}

	public void setsizeChange()
	{
		if (!isRun)
		{
			Screen.orientation = (ScreenOrientation)3;
			Application.runInBackground = true;
			Application.targetFrameRate = 30;
			((MonoBehaviour)this).useGUILayout = false;
			isCompactDevice = detectCompactDevice();
			if ((Object)(object)main == (Object)null)
			{
				main = this;
			}
			isRun = true;
			ScaleGUI.initScaleGUI();
			if (GameMidlet.isPC)
			{
				IMEI = SystemInfo.deviceUniqueIdentifier;
				Screen.fullScreen = false;
				typeClient = 4;
			}
			else
			{
				IMEI = GetMacAddress();
				Screen.fullScreen = true;
				typeClient = 2;
			}
			if (isWindowsPhone)
			{
				typeClient = 6;
			}
			if (IphoneVersionApp)
			{
				typeClient = 5;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen)
			{
				isIpod = true;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPhone4)
			{
				isIphone4 = true;
			}
			g = new mGraphics();
			midlet = new GameMidlet();
			Key.mapKeyPC();
			g.CreateLineMaterial();
		}
		else if (ScaleGUI.WIDTH != (float)Screen.width || ScaleGUI.HEIGHT != (float)Screen.height)
		{
			ScaleGUI.initScaleGUI();
			if (MotherCanvas.instance != null)
			{
				MotherCanvas.instance.checkZoomLevel();
			}
		}
	}

	public static void setBackupIcloud(string path)
	{
	}

	public string GetMacAddress()
	{
		try
		{
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			if (!string.IsNullOrEmpty(deviceUniqueIdentifier))
			{
				return deviceUniqueIdentifier;
			}
		}
		catch (Exception)
		{
		}
		try
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			for (int i = 0; i < allNetworkInterfaces.Length; i++)
			{
				PhysicalAddress physicalAddress = allNetworkInterfaces[i].GetPhysicalAddress();
				if (physicalAddress != null && physicalAddress.ToString() != string.Empty)
				{
					return physicalAddress.ToString();
				}
			}
		}
		catch (Exception)
		{
		}
		return "device_" + Guid.NewGuid().ToString("N");
	}

	public void doClearRMS()
	{
	}

	public static void closeKeyBoard()
	{
		if (TField.kb != null)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	private void FixedUpdate()
	{
		Rms.update();
		SaveImageRMS.update();
		count++;
		if (count < 10)
		{
			return;
		}
		Image.update();
		setsizeChange();
		updateCount++;
		ipKeyboard.update();
		if (GameMidlet.gameCanvas != null)
		{
			GameMidlet.gameCanvas.update();
		}
		DataInputStream.update();
		Net.update();
		f++;
		if (f > 8)
		{
			f = 0;
		}
		if (GameCanvas.isDisConnect)
		{
			GameCanvas.isDisConnect = false;
			string info = T.disconnect;
			if (GameCanvas.infoDisConnect != null && GameCanvas.infoDisConnect.Length > 10)
			{
				info = GameCanvas.infoDisConnect;
				GameCanvas.infoDisConnect = string.Empty;
			}
			bool flag = false;
			mVector mVector2 = new mVector();
			if (GameCanvas.currentScreen != GameCanvas.loginScr && GameCanvas.currentScreen != GameCanvas.loadMapScr)
			{
				mVector2.addElement(GameScreen.cmdReConnect);
				flag = true;
			}
			mVector2.addElement(GameCanvas.gameScr.cmdExit);
			if (flag)
			{
				GameCanvas.Start_ReConect_DiaLog(info, mVector2, isCmdClose: false);
			}
			else
			{
				GameCanvas.Start_Normal_DiaLog(info, mVector2, isCmdClose: false);
			}
		}
	}

	private void Awake()
	{
		((MonoBehaviour)this).useGUILayout = false;
		main = this;
		Rms.GetiPhoneDocumentsPath();
	}

	private void Update()
	{
		while (jobs.Count > 0)
		{
			((MonoBehaviour)this).StartCoroutine(jobs.Dequeue());
		}
	}

	internal void AddJob(IEnumerator newJob)
	{
		jobs.Enqueue(newJob);
	}

	private void checkInput()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Invalid comparison between Unknown and I4
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Invalid comparison between Unknown and I4
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Invalid comparison between Unknown and I4
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Invalid comparison between Unknown and I4
		if (Input.GetMouseButtonDown(0) && valueKey == 0)
		{
			valueKey = 1;
			Vector3 mousePosition = Input.mousePosition;
			GameMidlet.gameCanvas.onPointerPressed((int)(mousePosition.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			lastMousePos.x = mousePosition.x / (float)mGraphics.zoomLevel;
			lastMousePos.y = mousePosition.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = Input.mousePosition;
			GameMidlet.gameCanvas.onPointerDragged((int)(mousePosition2.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition2.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			lastMousePos.x = mousePosition2.x / (float)mGraphics.zoomLevel;
			lastMousePos.y = mousePosition2.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButtonUp(0) && valueKey == 1)
		{
			valueKey = 0;
			Vector3 mousePosition3 = Input.mousePosition;
			lastMousePos.x = mousePosition3.x / (float)mGraphics.zoomLevel;
			lastMousePos.y = mousePosition3.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			GameMidlet.gameCanvas.onPointerReleased((int)(mousePosition3.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition3.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
		}
		if (Input.anyKeyDown && (int)Event.current.type == 4)
		{
			int num = MyKeyMap.map(Event.current.keyCode);
			if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
			{
				KeyCode keyCode = Event.current.keyCode;
				if ((int)keyCode != 45)
				{
					if ((int)keyCode == 50)
					{
						num = 64;
					}
				}
				else
				{
					num = 95;
				}
			}
			if (num != 0)
			{
				GameMidlet.gameCanvas.keyPressed(num);
			}
		}
		if ((int)Event.current.type == 5)
		{
			int num2 = MyKeyMap.map(Event.current.keyCode);
			if (num2 != 0)
			{
				GameMidlet.gameCanvas.keyReleased(num2);
			}
		}
		if (GameMidlet.isPC)
		{
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			_ = (int)x / mGraphics.zoomLevel;
			_ = (Screen.height - (int)y) / mGraphics.zoomLevel;
		}
	}

	private void OnApplicationQuit()
	{
		Debug.LogWarning((object)"APP QUIT");
		((MonoBehaviour)this).StopAllCoroutines();
		SaveImageRMS.stop();
		Session_ME.gI().close();
	}

	private void OnDestroy()
	{
		((MonoBehaviour)this).StopAllCoroutines();
		SaveImageRMS.stop();
		Session_ME.gI().close();
	}

	private void OnApplicationPause(bool paused)
	{
		isResume = false;
		if (!paused)
		{
			isResume = true;
		}
		if (TField.kb != null)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
		if (isQuitApp)
		{
			Application.Quit();
		}
	}

	public static void exit()
	{
		if ((Object)(object)main != (Object)null)
		{
			main.OnApplicationQuit();
		}
		Application.Quit();
	}

	public static bool detectCompactDevice()
	{
		if (iPhoneSettings.generation == iPhoneGeneration.iPhone || iPhoneSettings.generation == iPhoneGeneration.iPhone3G || iPhoneSettings.generation == iPhoneGeneration.iPodTouch1Gen || iPhoneSettings.generation == iPhoneGeneration.iPodTouch2Gen)
		{
			return false;
		}
		return true;
	}

	public static bool checkCanSendSMS()
	{
		if (iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen)
		{
			return true;
		}
		return false;
	}
}
