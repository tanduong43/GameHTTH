using System;
using UnityEngine;

public class ipKeyboard
{
	public static TouchScreenKeyboard tk;

	public static int TEXT;

	public static int NUMBERIC = 1;

	public static int PASS = 2;

	private static IKAction act;

	public static void openKeyBoard(string caption, int type, string text, IKAction action)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		act = action;
		TouchScreenKeyboardType val = (TouchScreenKeyboardType)((type == 0 || type == 2) ? 1 : 4);
		TouchScreenKeyboard.hideInput = false;
		tk = TouchScreenKeyboard.Open(text, val);
	}

	public static void update()
	{
		try
		{
			if (tk != null && tk.done)
			{
				if (act != null)
				{
					act.perform(tk.text);
				}
				tk.text = string.Empty;
				tk = null;
			}
		}
		catch (Exception)
		{
		}
	}
}
