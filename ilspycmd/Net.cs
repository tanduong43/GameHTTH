using UnityEngine;

internal class Net
{
	public static WWW www;

	public static IKAction h;

	public static void update()
	{
		if (www != null && www.isDone)
		{
			string text = string.Empty;
			if (www.error == null || www.error.Equals(string.Empty))
			{
				text = www.text;
			}
			www = null;
			if (h != null)
			{
				h.perform(text);
			}
		}
	}

	public static void connectHTTP(string link, IKAction h)
	{
		if (www != null)
		{
			Cout.LogError("GET HTTP BUSY");
		}
		www = new WWW(link);
		Net.h = h;
	}
}
