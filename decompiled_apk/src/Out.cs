using System;
using System.Threading;
using UnityEngine;

public class Out
{
	public static void printLine(string text)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Debug.Log((object)("aaa: " + text));
		}
		else
		{
			Console.WriteLine("aaa: " + text);
		}
	}

	public static void printError(Exception e)
	{
	}
}
