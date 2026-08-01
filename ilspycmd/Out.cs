using System;
using UnityEngine;

public class Out
{
	public static void printLine(string text)
	{
		Debug.Log("aaa: " + text);
	}

	public static void printError(Exception e)
	{
		Debug.LogError(e);
	}
}
