using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NativeWebSocket;
using UnityEngine;

public class Session_WS : ISession
{
	public class Sender
	{
		public List<Message> sendingMessage = new List<Message>();

		public void AddMessage(Message message)
		{
			sendingMessage.Add(message);
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static WebSocketOpenEventHandler _003C_003E9__25_0;

		public static WebSocketErrorEventHandler _003C_003E9__25_2;

		public static WebSocketCloseEventHandler _003C_003E9__25_3;

		internal async void _003CInitWebSocket_003Eb__25_0()
		{
			Debug.Log((object)"[WS] Connected successfully!");
			connected = true;
			connecting = false;
			timeConnected = Environment.TickCount;
			if (messageHandler != null)
			{
				messageHandler.onConnectOK();
			}
			await doSendMessage(new Message((sbyte)(-27)));
		}

		internal void _003CInitWebSocket_003Eb__25_2(string errMsg)
		{
			Debug.LogError((object)("[WS] Error: " + errMsg));
			connecting = false;
			if (messageHandler != null)
			{
				messageHandler.onConnectionFail();
			}
		}

		internal unsafe void _003CInitWebSocket_003Eb__25_3(WebSocketCloseCode closeCode)
		{
			Debug.LogWarning((object)("[WS] Closed with code: " + ((object)(*(WebSocketCloseCode*)(&closeCode))/*cast due to constrained. prefix*/).ToString()));
			connected = false;
			connecting = false;
			if (messageHandler != null)
			{
				if (Environment.TickCount - timeConnected > 500)
				{
					messageHandler.onDisconnected();
				}
				else
				{
					messageHandler.onConnectionFail();
				}
			}
			cleanNetwork();
		}
	}

	private static Session_WS instance = new Session_WS();

	private static Sender sender = new Sender();

	public static IMessageHandler messageHandler;

	public static bool connected;

	public static bool connecting;

	public static bool isStart;

	public static int sendByteCount;

	public static int recvByteCount;

	public static string strRecvByteCount = string.Empty;

	private static bool getKeyComplete;

	public static sbyte[] key = null;

	private static sbyte curR;

	private static sbyte curW;

	private static int timeConnected;

	public static mVector recieveMsg = new mVector();

	private WebSocket websocket;

	private string currentHost;

	private int currentPort;

	public Session_WS()
	{
		Debug.Log((object)"init Session_WS (WebSocket Client)");
	}

	public static Session_WS gI()
	{
		if (instance == null)
		{
			instance = new Session_WS();
		}
		return instance;
	}

	public bool isConnected()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		if (connected && websocket != null)
		{
			return (int)websocket.State == 1;
		}
		return false;
	}

	public void setHandler(IMessageHandler msgHandler)
	{
		messageHandler = msgHandler;
	}

	public void clearSendingMessage()
	{
		sender.sendingMessage.Clear();
	}

	public void connect(string host, int port)
	{
		Debug.Log((object)("[WS] connect ...! " + connected + " :: " + connecting + " | " + host + ":" + port));
		if (connected || connecting)
		{
			return;
		}
		currentHost = host;
		currentPort = port;
		getKeyComplete = false;
		key = null;
		curR = 0;
		curW = 0;
		connecting = true;
		string text;
		if (host.StartsWith("ws://") || host.StartsWith("wss://"))
		{
			text = host;
		}
		else
		{
			string text2 = host;
			if (!string.IsNullOrEmpty(Application.absoluteURL))
			{
				try
				{
					Uri uri = new Uri(Application.absoluteURL);
					if (!string.IsNullOrEmpty(uri.Host))
					{
						text2 = uri.Host;
					}
				}
				catch
				{
				}
			}
			text = ((!Application.absoluteURL.StartsWith("https://")) ? ("ws://" + host + ":" + port) : ("wss://" + text2 + "/ws"));
		}
		Debug.Log((object)("[WS] Connecting to: " + text));
		InitWebSocket(text);
	}

	private unsafe async void InitWebSocket(string url)
	{
		_ = 1;
		try
		{
			if (websocket != null)
			{
				await websocket.Close();
				websocket = null;
			}
			websocket = new WebSocket(url, (Dictionary<string, string>)null);
			WebSocket obj = websocket;
			object obj2 = _003C_003Ec._003C_003E9__25_0;
			if (obj2 == null)
			{
				WebSocketOpenEventHandler val = async delegate
				{
					Debug.Log((object)"[WS] Connected successfully!");
					connected = true;
					connecting = false;
					timeConnected = Environment.TickCount;
					if (messageHandler != null)
					{
						messageHandler.onConnectOK();
					}
					await doSendMessage(new Message((sbyte)(-27)));
				};
				_003C_003Ec._003C_003E9__25_0 = val;
				obj2 = (object)val;
			}
			obj.OnOpen += (WebSocketOpenEventHandler)obj2;
			websocket.OnMessage += (WebSocketMessageEventHandler)delegate(byte[] bytes)
			{
				ProcessIncomingBytes(bytes);
			};
			WebSocket obj3 = websocket;
			object obj4 = _003C_003Ec._003C_003E9__25_2;
			if (obj4 == null)
			{
				WebSocketErrorEventHandler val2 = delegate(string errMsg)
				{
					Debug.LogError((object)("[WS] Error: " + errMsg));
					connecting = false;
					if (messageHandler != null)
					{
						messageHandler.onConnectionFail();
					}
				};
				_003C_003Ec._003C_003E9__25_2 = val2;
				obj4 = (object)val2;
			}
			obj3.OnError += (WebSocketErrorEventHandler)obj4;
			WebSocket obj5 = websocket;
			object obj6 = _003C_003Ec._003C_003E9__25_3;
			if (obj6 == null)
			{
				WebSocketCloseEventHandler val3 = delegate(WebSocketCloseCode closeCode)
				{
					Debug.LogWarning((object)("[WS] Closed with code: " + ((object)(*(WebSocketCloseCode*)(&closeCode))/*cast due to constrained. prefix*/).ToString()));
					connected = false;
					connecting = false;
					if (messageHandler != null)
					{
						if (Environment.TickCount - timeConnected > 500)
						{
							messageHandler.onDisconnected();
						}
						else
						{
							messageHandler.onConnectionFail();
						}
					}
					cleanNetwork();
				};
				_003C_003Ec._003C_003E9__25_3 = val3;
				obj6 = (object)val3;
			}
			obj5.OnClose += (WebSocketCloseEventHandler)obj6;
			await websocket.Connect();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("[WS] Connect Exception: " + ex.Message));
			connecting = false;
			connected = false;
			if (messageHandler != null)
			{
				messageHandler.onConnectionFail();
			}
		}
	}

	private void ProcessIncomingBytes(byte[] bytes)
	{
		try
		{
			if (bytes == null || bytes.Length == 0)
			{
				return;
			}
			using MemoryStream input = new MemoryStream(bytes);
			using BinaryReader binaryReader = new BinaryReader(input, Encoding.UTF8);
			sbyte b = binaryReader.ReadSByte();
			if (getKeyComplete)
			{
				b = readKey(b);
			}
			int num;
			if (getKeyComplete)
			{
				if (b == -39 || b == -101 || b == -93 || b == 76 || b == -102)
				{
					sbyte b2 = binaryReader.ReadSByte();
					sbyte b3 = binaryReader.ReadSByte();
					sbyte b4 = binaryReader.ReadSByte();
					sbyte b5 = binaryReader.ReadSByte();
					num = ((readKey(b2) & 0xFF) << 24) | ((readKey(b3) & 0xFF) << 16) | ((readKey(b4) & 0xFF) << 8) | (readKey(b5) & 0xFF);
				}
				else
				{
					sbyte b6 = binaryReader.ReadSByte();
					sbyte b7 = binaryReader.ReadSByte();
					num = ((readKey(b6) & 0xFF) << 8) | (readKey(b7) & 0xFF);
				}
			}
			else if (b == -39)
			{
				num = binaryReader.ReadInt32();
			}
			else
			{
				sbyte num2 = binaryReader.ReadSByte();
				sbyte b8 = binaryReader.ReadSByte();
				num = (num2 & 0xFF00) | (b8 & 0xFF);
			}
			sbyte[] array = new sbyte[num];
			if (num > 0)
			{
				byte[] array2 = binaryReader.ReadBytes(num);
				Buffer.BlockCopy(array2, 0, array, 0, array2.Length);
			}
			recvByteCount += 5 + num;
			int num3 = recvByteCount + sendByteCount;
			strRecvByteCount = num3 / 1024 + "." + num3 % 1024 / 102 + "Kb";
			if (getKeyComplete && num > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = readKey(array[i]);
				}
			}
			Message message = new Message(b, array);
			if (message.command == -27)
			{
				getKey(message);
			}
			else
			{
				onRecieveMsg(message);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("[WS] Error reading message: " + ex.Message + "\n" + ex.StackTrace));
		}
	}

	private void getKey(Message message)
	{
		try
		{
			sbyte b = message.reader().readSByte();
			key = new sbyte[b];
			for (int i = 0; i < b; i++)
			{
				key[i] = message.reader().readSByte();
			}
			for (int j = 0; j < key.Length - 1; j++)
			{
				key[j + 1] ^= key[j];
			}
			getKeyComplete = true;
			Debug.Log((object)"[WS] Encryption key exchange completed!");
			checkAndSendPendingMessages();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("[WS] getKey error: " + ex.Message));
		}
	}

	public void sendMessage(Message message)
	{
		sender.AddMessage(message);
		checkAndSendPendingMessages();
	}

	public static async void checkAndSendPendingMessages()
	{
		if (instance != null && instance.websocket != null && (int)instance.websocket.State == 1 && getKeyComplete)
		{
			while (sender.sendingMessage.Count > 0)
			{
				Message message = sender.sendingMessage[0];
				sender.sendingMessage.RemoveAt(0);
				await doSendMessage(message);
			}
		}
	}

	private static async Task doSendMessage(Message message)
	{
		if (instance == null || instance.websocket == null || (int)instance.websocket.State != 1)
		{
			return;
		}
		try
		{
			sbyte[] data = message.getData();
			using MemoryStream ms = new MemoryStream();
			using BinaryWriter dos = new BinaryWriter(ms, Encoding.UTF8);
			if (getKeyComplete)
			{
				sbyte value = writeKey(message.command);
				dos.Write(value);
			}
			else
			{
				dos.Write(message.command);
			}
			if (data != null)
			{
				int num = data.Length;
				if (getKeyComplete)
				{
					int num2 = writeKey((sbyte)(num >> 8));
					dos.Write((sbyte)num2);
					int num3 = writeKey((sbyte)(num & 0xFF));
					dos.Write((sbyte)num3);
				}
				else
				{
					dos.Write((ushort)num);
				}
				if (getKeyComplete)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = writeKey(data[i]);
						dos.Write(value2);
					}
				}
				else
				{
					for (int j = 0; j < data.Length; j++)
					{
						dos.Write((byte)data[j]);
					}
				}
				sendByteCount += 5 + data.Length;
			}
			else
			{
				if (getKeyComplete)
				{
					int num4 = writeKey((sbyte)0);
					dos.Write((sbyte)num4);
					int num5 = writeKey((sbyte)0);
					dos.Write((sbyte)num5);
				}
				else
				{
					dos.Write((ushort)0);
				}
				sendByteCount += 5;
			}
			dos.Flush();
			byte[] array = ms.ToArray();
			await instance.websocket.Send(array);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("[WS] Send error: " + ex.Message));
		}
	}

	public static sbyte readKey(sbyte b)
	{
		sbyte result = (sbyte)((key[curR++] & 0xFF) ^ (b & 0xFF));
		if (curR >= key.Length)
		{
			curR %= (sbyte)key.Length;
		}
		return result;
	}

	public static sbyte writeKey(sbyte b)
	{
		sbyte result = (sbyte)((key[curW++] & 0xFF) ^ (b & 0xFF));
		if (curW >= key.Length)
		{
			curW %= (sbyte)key.Length;
		}
		return result;
	}

	public static void onRecieveMsg(Message msg)
	{
		if (messageHandler != null)
		{
			recieveMsg.addElement(msg);
		}
	}

	public static void update()
	{
		if (instance != null && instance.websocket != null)
		{
			instance.websocket.DispatchMessageQueue();
		}
		checkAndSendPendingMessages();
		while (recieveMsg.size() > 0)
		{
			Message message = (Message)recieveMsg.elementAt(0);
			if (message == null)
			{
				recieveMsg.removeElementAt(0);
				break;
			}
			if (messageHandler != null)
			{
				messageHandler.onMessage(message);
			}
			recieveMsg.removeElementAt(0);
		}
	}

	public async void close()
	{
		recieveMsg.removeAllElements();
		sender.sendingMessage.Clear();
		cleanNetwork();
		isStart = false;
		if (websocket != null)
		{
			await websocket.Close();
			websocket = null;
		}
	}

	private static void cleanNetwork()
	{
		key = null;
		curR = 0;
		curW = 0;
		connected = false;
		connecting = false;
	}
}
