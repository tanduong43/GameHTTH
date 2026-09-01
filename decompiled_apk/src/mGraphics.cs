using System;
using System.Collections;
using UnityEngine;

public class mGraphics
{
	public static int HCENTER = 1;

	public static int VCENTER = 2;

	public static int LEFT = 4;

	public static int RIGHT = 8;

	public static int TOP = 16;

	public static int BOTTOM = 32;

	private float r;

	private float g;

	private float b;

	private float a;

	public int clipX;

	public int clipY;

	public int clipW;

	public int clipH;

	private bool isClip;

	private bool isTranslate = true;

	private int translateX;

	private int translateY;

	private float translateXf;

	private float translateYf;

	public static int zoomLevel = 2;

	public const int BASELINE = 64;

	public const int SOLID = 0;

	public const int DOTTED = 1;

	public const int TRANS_MIRROR = 2;

	public const int TRANS_MIRROR_ROT180 = 1;

	public const int TRANS_MIRROR_ROT270 = 4;

	public const int TRANS_MIRROR_ROT90 = 7;

	public const int TRANS_NONE = 0;

	public const int TRANS_ROT180 = 3;

	public const int TRANS_ROT270 = 6;

	public const int TRANS_ROT90 = 5;

	public static Hashtable cachedTextures = new Hashtable();

	public static int addYWhenOpenKeyBoard;

	public int translateXNew;

	public int translateYNew;

	public bool isTranslateNew;

	private int clipTX;

	private int clipTY;

	private int clipXNew;

	private int clipYNew;

	private int clipWNew;

	private int clipHNew;

	private bool isClipNew;

	private int xTemp;

	private int yTemp;

	private int currentBGColor;

	private Vector2 pos = new Vector2(0f, 0f);

	private Rect rect;

	private Matrix4x4 matrixBackup;

	private Vector2 pivot;

	public Vector2 size = new Vector2(128f, 128f);

	public Vector2 relativePosition = new Vector2(0f, 0f);

	public static Color transParentColor = new Color(1f, 1f, 1f, 0f);

	private Material lineMaterial;

	private void cache(string key, Texture value)
	{
		if (cachedTextures.Count > 400)
		{
			cachedTextures.Clear();
		}
		if (value.width * value.height < MotherCanvas.w * MotherCanvas.h)
		{
			cachedTextures.Add(key, value);
		}
	}

	public void translate(int tx, int ty)
	{
		tx *= zoomLevel;
		ty *= zoomLevel;
		translateX += tx;
		translateY += ty;
		isTranslate = true;
		if (translateX == 0 && translateY == 0)
		{
			isTranslate = false;
		}
	}

	public void translateNew(int tx, int ty)
	{
		tx *= zoomLevel;
		ty *= zoomLevel;
		translateXNew = tx;
		translateYNew = ty;
		isTranslateNew = true;
		if (translateXNew == 0 && translateYNew == 0)
		{
			isTranslateNew = false;
		}
	}

	public void translate(float x, float y)
	{
		translateXf += x;
		translateYf += y;
		isTranslate = true;
		if (translateXf == 0f && translateYf == 0f)
		{
			isTranslate = false;
		}
	}

	public int getTranslateX()
	{
		return translateX / zoomLevel;
	}

	public int getTranslateY()
	{
		return translateY / zoomLevel + addYWhenOpenKeyBoard;
	}

	public void setClip(int x, int y, int w, int h)
	{
		x *= zoomLevel;
		y *= zoomLevel;
		w *= zoomLevel;
		h *= zoomLevel;
		clipTX = translateX;
		clipTY = translateY;
		clipX = x;
		clipY = y;
		clipW = w;
		clipH = h;
		isClip = true;
	}

	public void setClipNew(int x, int y, int w, int h)
	{
		x *= zoomLevel;
		y *= zoomLevel;
		w *= zoomLevel;
		h *= zoomLevel;
		xTemp = x;
		yTemp = y;
		if (isTranslateNew)
		{
			xTemp -= translateXNew;
			yTemp -= translateYNew;
		}
		clipXNew = xTemp;
		clipYNew = yTemp;
		clipWNew = w;
		clipHNew = h;
		isClipNew = true;
	}

	public void fillRect(int x, int y, int w, int h, int color, int alpha)
	{
		float alpha2 = 0.5f;
		setColor(color, alpha2);
		fillRect(x, y, w, h, isA: false);
	}

	public void fillRect(int x, int y, int w, int h)
	{
		fillRect(x, y, w, h, isA: false);
	}

	public void drawLine(int x1, int y1, int x2, int y2, bool isA)
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		if (y1 == y2)
		{
			if (x1 > x2)
			{
				int num = x2;
				x2 = x1;
				x1 = num;
			}
			fillRect(x1, y1, Math.Max(1, x2 - x1), 1, isA);
			return;
		}
		if (x1 == x2)
		{
			if (y1 > y2)
			{
				int num2 = y2;
				y2 = y1;
				y1 = num2;
			}
			fillRect(x1, y1, 1, Math.Max(1, y2 - y1), isA);
			return;
		}
		x1 *= zoomLevel;
		y1 *= zoomLevel;
		x2 *= zoomLevel;
		y2 *= zoomLevel;
		if (isTranslate)
		{
			x1 += translateX;
			y1 += translateY;
			x2 += translateX;
			y2 += translateY;
		}
		string key = "dl" + r + "_" + g + "_" + b;
		Texture2D val = (Texture2D)cachedTextures[key];
		if ((Object)(object)val == (Object)null)
		{
			val = new Texture2D(1, 1);
			Color val2 = default(Color);
			((Color)(ref val2))._002Ector(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), 1f);
			val.SetPixel(0, 0, val2);
			val.Apply();
			cache(key, (Texture)(object)val);
		}
		Vector2 val3 = default(Vector2);
		((Vector2)(ref val3))._002Ector((float)x1, (float)y1);
		Vector2 val4 = new Vector2((float)x2, (float)y2) - val3;
		if (Mathf.Approximately(val4.x, 0f))
		{
			fillRect(x1 / zoomLevel, y1 / zoomLevel, 1, Math.Max(1, (int)(((Vector2)(ref val4)).magnitude / (float)zoomLevel)), isA);
			return;
		}
		float num3 = 57.29578f * Mathf.Atan(val4.y / val4.x);
		if (val4.x < 0f)
		{
			num3 += 180f;
		}
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		if (isClip)
		{
			num5 = clipX;
			num6 = clipY;
			num7 = clipW;
			num8 = clipH;
			if (isTranslate)
			{
				num5 += clipTX;
				num6 += clipTY;
			}
		}
		if (isClip)
		{
			GUI.BeginGroup(new Rect((float)num5, (float)num6, (float)num7, (float)num8));
		}
		GUIUtility.RotateAroundPivot(num3, val3);
		Graphics.DrawTexture(new Rect(val3.x - (float)num5, val3.y - (float)num4 - (float)num6, ((Vector2)(ref val4)).magnitude, (float)zoomLevel), (Texture)(object)val);
		GUIUtility.RotateAroundPivot(0f - num3, val3);
		if (isClip)
		{
			GUI.EndGroup();
		}
	}

	public void drawLine(int x1, int y1, int x2, int y2)
	{
		drawLine(x1, y1, x2, y2, isA: false);
	}

	public Color setColorMiniMap(int rgb)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		int num = rgb & 0xFF;
		int num2 = (rgb >> 8) & 0xFF;
		int num3 = (rgb >> 16) & 0xFF;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		return new Color((float)num3 / 256f, num5, num4);
	}

	public float[] getRGB(Color cl)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		float num = 256f * cl.r;
		float num2 = 256f * cl.g;
		float num3 = 256f * cl.b;
		return new float[3] { num, num2, num3 };
	}

	public void drawRect(int x, int y, int w, int h, bool isA)
	{
		int num = 1;
		fillRect(x, y, w, num, isA);
		fillRect(x, y, num, h, isA);
		fillRect(x + w, y, num, h + 1, isA);
		fillRect(x, y + h, w + 1, num, isA);
	}

	public void drawRect(int x, int y, int w, int h)
	{
		int num = 1;
		fillRect(x, y, w, num);
		fillRect(x, y, num, h);
		fillRect(x + w, y, num, h + 1);
		fillRect(x, y + h, w + 1, num);
	}

	public void drawArc(int x, int y, int w, int h, int a, int b, bool isA)
	{
		int num = 1;
		fillRect(x, y, w, num, isA);
		fillRect(x, y, num, h, isA);
		fillRect(x + w, y, num, h + 1, isA);
		fillRect(x, y + h, w + 1, num, isA);
	}

	public void fillRect(int x, int y, int w, int h, bool isA)
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		x *= zoomLevel;
		y *= zoomLevel;
		w *= zoomLevel;
		h *= zoomLevel;
		if (w < 0 || h < 0)
		{
			return;
		}
		if (isTranslate)
		{
			x += translateX;
			y += translateY;
		}
		int num = 1;
		int num2 = 1;
		string key = "fr" + num + num2 + r + g + b + a;
		Texture2D val = (Texture2D)cachedTextures[key];
		if ((Object)(object)val == (Object)null)
		{
			val = new Texture2D(num, num2);
			Color val2 = default(Color);
			((Color)(ref val2))._002Ector(r, g, b, a);
			val.SetPixel(0, 0, val2);
			val.Apply();
			cache(key, (Texture)(object)val);
		}
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (isClip)
		{
			num3 = clipX;
			num4 = clipY;
			num5 = clipW;
			num6 = clipH;
			if (isTranslate)
			{
				num3 += clipTX;
				num4 += clipTY;
			}
		}
		if (isClip)
		{
			GUI.BeginGroup(new Rect((float)num3, (float)num4, (float)num5, (float)num6));
		}
		GUI.DrawTexture(new Rect((float)(x - num3), (float)(y - num4), (float)w, (float)h), (Texture)(object)val);
		if (isClip)
		{
			GUI.EndGroup();
		}
	}

	public void fillArc(int x, int y, int w, int h, int a, int b, bool isSetClip)
	{
		fillRect(x, y, w, h, isA: false);
	}

	public void setColor(int rgb)
	{
		int num = rgb & 0xFF;
		int num2 = (rgb >> 8) & 0xFF;
		int num3 = (rgb >> 16) & 0xFF;
		b = (float)num / 256f;
		g = (float)num2 / 256f;
		r = (float)num3 / 256f;
		a = 255f;
	}

	public void setColor(Color color)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		b = color.b;
		g = color.g;
		r = color.r;
	}

	public void setBgColor(int rgb)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		if (rgb != currentBGColor)
		{
			currentBGColor = rgb;
			int num = rgb & 0xFF;
			int num2 = (rgb >> 8) & 0xFF;
			int num3 = (rgb >> 16) & 0xFF;
			b = (float)num / 256f;
			g = (float)num2 / 256f;
			r = (float)num3 / 256f;
			((Component)Main.main).GetComponent<Camera>().backgroundColor = new Color(r, g, b);
		}
	}

	public void drawString(string s, int x, int y, GUIStyle style)
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		x *= zoomLevel;
		y *= zoomLevel;
		if (isTranslate)
		{
			x += translateX;
			y += translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (isClip)
		{
			num = clipX;
			num2 = clipY;
			num3 = clipW;
			num4 = clipH;
			if (isTranslate)
			{
				num += clipTX;
				num2 += clipTY;
			}
		}
		if (isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2), ScaleGUI.WIDTH, 100f), s, style);
		if (isClip)
		{
			GUI.EndGroup();
		}
	}

	public void drawString(string s, int x, int y, GUIStyle style, bool useClipNew)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		x *= zoomLevel;
		y *= zoomLevel;
		if (isTranslate)
		{
			x += translateX;
			y += translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (isClipNew)
		{
			num = clipXNew;
			num2 = clipYNew;
			num3 = clipWNew;
			num4 = clipHNew;
			if (isTranslateNew)
			{
				num = clipXNew;
				num2 = clipYNew;
			}
		}
		if (isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2), ScaleGUI.WIDTH, 100f), s, style);
		if (isClip)
		{
			GUI.EndGroup();
		}
	}

	public void drawString(string s, int x, int y, int archor)
	{
		int num = -8;
		mFont.tahoma_7_white.drawString(this, s, x, y + num, archor);
	}

	public void setColor(int rgb, float alpha)
	{
		int num = rgb & 0xFF;
		int num2 = (rgb >> 8) & 0xFF;
		int num3 = (rgb >> 16) & 0xFF;
		b = (float)num / 256f;
		g = (float)num2 / 256f;
		r = (float)num3 / 256f;
		a = alpha;
	}

	public void drawString(string s, int x, int y, GUIStyle style, int w)
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		x *= zoomLevel;
		y *= zoomLevel;
		if (isTranslate)
		{
			x += translateX;
			y += translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (isClip)
		{
			num = clipX;
			num2 = clipY;
			num3 = clipW;
			num4 = clipH;
			if (isTranslate)
			{
				num += clipTX;
				num2 += clipTY;
			}
		}
		if (isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2 - 4), (float)w, 100f), s, style);
		if (isClip)
		{
			GUI.EndGroup();
		}
	}

	private void UpdatePos(int anchor)
	{
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = default(Vector2);
		((Vector2)(ref val))._002Ector(0f, 0f);
		switch (anchor)
		{
		case 3:
			((Vector2)(ref val))._002Ector(size.x / 2f, size.y / 2f);
			break;
		case 20:
			((Vector2)(ref val))._002Ector(0f, 0f);
			break;
		case 17:
			((Vector2)(ref val))._002Ector((float)(Screen.width / 2), 0f);
			break;
		case 24:
			((Vector2)(ref val))._002Ector((float)Screen.width, 0f);
			break;
		case 6:
			((Vector2)(ref val))._002Ector(0f, (float)(Screen.height / 2));
			break;
		case 10:
			((Vector2)(ref val))._002Ector((float)Screen.width, (float)(Screen.height / 2));
			break;
		case 36:
			((Vector2)(ref val))._002Ector(0f, (float)Screen.height);
			break;
		case 33:
			((Vector2)(ref val))._002Ector((float)(Screen.width / 2), (float)Screen.height);
			break;
		case 40:
			((Vector2)(ref val))._002Ector((float)Screen.width, (float)Screen.height);
			break;
		}
		pos = val + relativePosition;
		rect = new Rect(pos.x - size.x * 0.5f, pos.y - size.y * 0.5f, size.x, size.y);
		pivot = new Vector2(((Rect)(ref rect)).xMin + ((Rect)(ref rect)).width * 0.5f, ((Rect)(ref rect)).yMin + ((Rect)(ref rect)).height * 0.5f);
	}

	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8, bool isA)
	{
		x *= zoomLevel;
		y *= zoomLevel;
		x0 *= zoomLevel;
		y0 *= zoomLevel;
		w0 *= zoomLevel;
		h0 *= zoomLevel;
		_drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8)
	{
		x *= zoomLevel;
		y *= zoomLevel;
		x0 *= zoomLevel;
		y0 *= zoomLevel;
		w0 *= zoomLevel;
		h0 *= zoomLevel;
		_drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	public void drawRegion(mImage arg0, int x0, int y0, int w0, int h0, int arg5, float x, float y, int arg8)
	{
		x *= (float)zoomLevel;
		y *= (float)zoomLevel;
		x0 *= zoomLevel;
		y0 *= zoomLevel;
		w0 *= zoomLevel;
		h0 *= zoomLevel;
		__drawRegion(arg0.image, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	public void __drawRegion(Image image, int x0, int y0, int w, int h, int transform, float x, float y, int anchor)
	{
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		if (image == null)
		{
			return;
		}
		if (isTranslate)
		{
			x += (float)translateX;
			y += (float)translateY;
		}
		float num = w;
		float num2 = h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 1f;
		float num6 = 0f;
		int num7 = 1;
		if ((uint)(transform - 4) <= 3u)
		{
			num = h;
			num2 = w;
		}
		int num8 = 0;
		int num9 = 0;
		switch (anchor)
		{
		case 20:
			num8 = 0;
			num9 = 0;
			break;
		case 17:
			num8 = (int)num / 2;
			num9 = 0;
			break;
		case 24:
			num8 = (int)num;
			num9 = 0;
			break;
		case 6:
			num8 = 0;
			num9 = (int)num2 / 2;
			break;
		case 3:
			num8 = (int)num / 2;
			num9 = (int)num2 / 2;
			break;
		case 10:
			num8 = (int)num;
			num9 = (int)num2 / 2;
			break;
		case 36:
			num8 = 0;
			num9 = (int)num2;
			break;
		case 33:
			num8 = (int)num / 2;
			num9 = (int)num2;
			break;
		case 40:
			num8 = (int)num;
			num9 = (int)num2;
			break;
		}
		x -= (float)num8;
		y -= (float)num9;
		int num10 = 0;
		int num11 = 0;
		int num12 = 0;
		int num13 = 0;
		if (isClip)
		{
			num10 = clipX;
			num11 = clipY;
			num12 = clipW;
			num13 = clipH;
			if (isTranslate)
			{
				num10 += clipTX;
				num11 += clipTY;
			}
			Rect r = default(Rect);
			((Rect)(ref r))._002Ector(x, y, (float)w, (float)h);
			Rect r2 = default(Rect);
			((Rect)(ref r2))._002Ector((float)num10, (float)num11, (float)num12, (float)num13);
			Rect val = intersectRect(r, r2);
			if (((Rect)(ref val)).width <= 0f || ((Rect)(ref val)).height <= 0f)
			{
				return;
			}
			num = ((Rect)(ref val)).width;
			num2 = ((Rect)(ref val)).height;
			num3 = ((Rect)(ref val)).x - ((Rect)(ref r)).x;
			num4 = ((Rect)(ref val)).y - ((Rect)(ref r)).y;
		}
		float num14 = 0f;
		float num15 = 0f;
		switch (transform)
		{
		case 2:
			num14 += num;
			num5 = -1f;
			if (isClip)
			{
				if ((float)num10 > x)
				{
					num6 = 0f - num3;
				}
				else if ((float)(num10 + num12) < x + (float)w)
				{
					num6 = 0f - ((float)(num10 + num12) - x - (float)w);
				}
			}
			break;
		case 1:
			num7 = -1;
			num15 += num2;
			break;
		case 3:
			num7 = -1;
			num15 += num2;
			num5 = -1f;
			num14 += num;
			break;
		}
		int num16 = 0;
		int num17 = 0;
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			matrixBackup = GUI.matrix;
			size = new Vector2((float)w, (float)h);
			relativePosition = new Vector2(x, y);
			UpdatePos(3);
			switch (transform)
			{
			case 6:
				UpdatePos(3);
				break;
			case 5:
				size = new Vector2((float)w, (float)h);
				UpdatePos(3);
				break;
			}
			switch (transform)
			{
			case 5:
				GUIUtility.RotateAroundPivot(90f, pivot);
				num14 = num2;
				break;
			case 6:
				GUIUtility.RotateAroundPivot(270f, pivot);
				break;
			case 4:
				GUIUtility.RotateAroundPivot(270f, pivot);
				num14 += num;
				num5 = -1f;
				if (isClip)
				{
					if ((float)num10 > x)
					{
						num6 = 0f - num3;
					}
					else if ((float)(num10 + num12) < x + (float)w)
					{
						num6 = 0f - ((float)(num10 + num12) - x - (float)w);
					}
				}
				break;
			case 7:
				GUIUtility.RotateAroundPivot(270f, pivot);
				num7 = -1;
				num15 += num2;
				break;
			}
		}
		Graphics.DrawTexture(new Rect(x + num3 + num14 + (float)num16, y + num4 + (float)num17 + num15, num * num5, num2 * (float)num7), (Texture)(object)image.texture, new Rect(((float)x0 + num3 + num6) / (float)((Texture)image.texture).width, ((float)((Texture)image.texture).height - num2 - ((float)y0 + num4)) / (float)((Texture)image.texture).height, num / (float)((Texture)image.texture).width, num2 / (float)((Texture)image.texture).height), 0, 0, 0, 0);
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			GUI.matrix = matrixBackup;
		}
	}

	public void _drawRegion(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		if (image == null)
		{
			return;
		}
		if (isTranslate)
		{
			x += translateX;
			y += translateY;
		}
		float num = w;
		float num2 = h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		float num8 = 0f;
		int num9 = 1;
		if ((anchor & HCENTER) == HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & VCENTER) == VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & RIGHT) == RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & BOTTOM) == BOTTOM)
		{
			num6 -= num2;
		}
		x += (int)num5;
		y += (int)num6;
		int num10 = 0;
		int num11 = 0;
		int num12 = 0;
		int num13 = 0;
		if (isClip)
		{
			num10 = clipX;
			num11 = clipY;
			num12 = clipW;
			num13 = clipH;
			if (isTranslate)
			{
				num10 += clipTX;
				num11 += clipTY;
			}
			Rect r = default(Rect);
			((Rect)(ref r))._002Ector((float)x, (float)y, (float)w, (float)h);
			Rect r2 = default(Rect);
			((Rect)(ref r2))._002Ector((float)num10, (float)num11, (float)num12, (float)num13);
			Rect val = intersectRect(r, r2);
			if (((Rect)(ref val)).width <= 0f || ((Rect)(ref val)).height <= 0f)
			{
				return;
			}
			num = ((Rect)(ref val)).width;
			num2 = ((Rect)(ref val)).height;
			num3 = ((Rect)(ref val)).x - ((Rect)(ref r)).x;
			num4 = ((Rect)(ref val)).y - ((Rect)(ref r)).y;
		}
		float num14 = 0f;
		float num15 = 0f;
		switch (transform)
		{
		case 2:
			num14 += num;
			num7 = -1f;
			if (isClip)
			{
				if (num10 > x)
				{
					num8 = 0f - num3;
				}
				else if (num10 + num12 < x + w)
				{
					num8 = -(num10 + num12 - x - w);
				}
			}
			break;
		case 1:
			num9 = -1;
			num15 += num2;
			break;
		case 3:
			num9 = -1;
			num15 += num2;
			num7 = -1f;
			num14 += num;
			break;
		}
		int num16 = 0;
		int num17 = 0;
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			matrixBackup = GUI.matrix;
			size = new Vector2((float)w, (float)h);
			relativePosition = new Vector2((float)x, (float)y);
			UpdatePos(3);
			switch (transform)
			{
			case 6:
				UpdatePos(3);
				break;
			case 5:
				size = new Vector2((float)w, (float)h);
				UpdatePos(3);
				break;
			}
			switch (transform)
			{
			case 5:
				GUIUtility.RotateAroundPivot(90f, pivot);
				break;
			case 6:
				GUIUtility.RotateAroundPivot(270f, pivot);
				break;
			case 4:
				GUIUtility.RotateAroundPivot(270f, pivot);
				num14 += num;
				num7 = -1f;
				if (isClip)
				{
					if (num10 > x)
					{
						num8 = 0f - num3;
					}
					else if (num10 + num12 < x + w)
					{
						num8 = -(num10 + num12 - x - w);
					}
				}
				break;
			case 7:
				GUIUtility.RotateAroundPivot(270f, pivot);
				num9 = -1;
				num15 += num2;
				break;
			}
		}
		Graphics.DrawTexture(new Rect((float)x + num3 + num14 + (float)num16, (float)y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), (Texture)(object)image.texture, new Rect((x0 + num3 + num8) / (float)((Texture)image.texture).width, ((float)((Texture)image.texture).height - num2 - (y0 + num4)) / (float)((Texture)image.texture).height, num / (float)((Texture)image.texture).width, num2 / (float)((Texture)image.texture).height), 0, 0, 0, 0);
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			GUI.matrix = matrixBackup;
		}
	}

	public void drawRegion2(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		GUI.color = image.colorBlend;
		if (isTranslate)
		{
			x += translateX;
			y += translateY;
		}
		string key = "dg" + x0 + y0 + w + h + transform + image.GetHashCode();
		Texture2D val = (Texture2D)cachedTextures[key];
		if ((Object)(object)val == (Object)null)
		{
			val = Image.createImage(image, (int)x0, (int)y0, w, h, transform).texture;
			cache(key, (Texture)(object)val);
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		float num5 = w;
		float num6 = h;
		float num7 = 0f;
		float num8 = 0f;
		if ((anchor & HCENTER) == HCENTER)
		{
			num7 -= num5 / 2f;
		}
		if ((anchor & VCENTER) == VCENTER)
		{
			num8 -= num6 / 2f;
		}
		if ((anchor & RIGHT) == RIGHT)
		{
			num7 -= num5;
		}
		if ((anchor & BOTTOM) == BOTTOM)
		{
			num8 -= num6;
		}
		x += (int)num7;
		y += (int)num8;
		if (isClip)
		{
			num = clipX;
			num2 = clipY;
			num3 = clipW;
			num4 = clipH;
			if (isTranslate)
			{
				num += clipTX;
				num2 += clipTY;
			}
		}
		if (isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.DrawTexture(new Rect((float)(x - num), (float)(y - num2), (float)w, (float)h), (Texture)(object)val);
		if (isClip)
		{
			GUI.EndGroup();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
	}

	public void drawImagaByDrawTexture(Image image, float x, float y)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		x *= (float)zoomLevel;
		y *= (float)zoomLevel;
		GUI.DrawTexture(new Rect(x + (float)translateX, y + (float)translateY, (float)image.getRealImageWidth(), (float)image.getRealImageHeight()), (Texture)(object)image.texture);
	}

	public void drawImage(mImage image, int x, int y, int anchor)
	{
		if (image != null && image.image != null)
		{
			drawRegion(image, 0, 0, mImage.getImageWidth(image.image), mImage.getImageHeight(image.image), 0, x, y, anchor);
		}
	}

	public void drawRoundRect(int x, int y, int w, int h, int arcWidth, int arcHeight)
	{
		drawRect(x, y, w, h, isA: false);
	}

	public void fillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
	{
		fillRect(x, y, width, height, isA: false);
	}

	public void reset()
	{
		isClip = false;
		isTranslate = false;
		translateX = 0;
		translateY = 0;
	}

	public Rect intersectRect(Rect r1, Rect r2)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		float num = ((Rect)(ref r1)).x;
		float num2 = ((Rect)(ref r1)).y;
		float x = ((Rect)(ref r2)).x;
		float y = ((Rect)(ref r2)).y;
		float num3 = num;
		num3 += ((Rect)(ref r1)).width;
		float num4 = num2;
		num4 += ((Rect)(ref r1)).height;
		float num5 = x;
		num5 += ((Rect)(ref r2)).width;
		float num6 = y;
		num6 += ((Rect)(ref r2)).height;
		if (num < x)
		{
			num = x;
		}
		if (num2 < y)
		{
			num2 = y;
		}
		if (num3 > num5)
		{
			num3 = num5;
		}
		if (num4 > num6)
		{
			num4 = num6;
		}
		num3 -= num;
		num4 -= num2;
		if (num3 < -30000f)
		{
			num3 = -30000f;
		}
		if (num4 < -30000f)
		{
			num4 = -30000f;
		}
		return new Rect(num, num2, (float)(int)num3, (float)(int)num4);
	}

	public void drawImageScale(Image image, int x, int y, int w, int h, int tranform)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		GUI.color = Color.red;
		x *= zoomLevel;
		y *= zoomLevel;
		w *= zoomLevel;
		h *= zoomLevel;
		if (image != null)
		{
			Graphics.DrawTexture(new Rect((float)(x + translateX), (float)(y + translateY), (float)((tranform != 0) ? (-w) : w), (float)h), (Texture)(object)image.texture);
		}
	}

	public void drawImageSimple(Image image, int x, int y)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		x *= zoomLevel;
		y *= zoomLevel;
		if (image != null)
		{
			Graphics.DrawTexture(new Rect((float)x, (float)y, (float)image.w, (float)image.h), (Texture)(object)image.texture);
		}
	}

	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	public static bool isNotTranColor(Color color)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (color == Color.clear || color == transParentColor)
		{
			return false;
		}
		return true;
	}

	public static Image blend(Image img0, float level, int rgb)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		int num = rgb & 0xFF;
		int num2 = (rgb >> 8) & 0xFF;
		int num3 = (rgb >> 16) & 0xFF;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color val = new Color(num6, num5, num4);
		Color[] pixels = img0.texture.GetPixels();
		float num7 = val.r;
		float num8 = val.g;
		float num9 = val.b;
		for (int i = 0; i < pixels.Length; i++)
		{
			Color val2 = pixels[i];
			if (isNotTranColor(val2))
			{
				float num10 = (num7 - val2.r) * level + val2.r;
				float num11 = (num8 - val2.g) * level + val2.g;
				float num12 = (num9 - val2.b) * level + val2.b;
				if (num10 > 255f)
				{
					num10 = 255f;
				}
				if (num10 < 0f)
				{
					num10 = 0f;
				}
				if (num11 > 255f)
				{
					num11 = 255f;
				}
				if (num11 < 0f)
				{
					num11 = 0f;
				}
				if (num12 < 0f)
				{
					num12 = 0f;
				}
				if (num12 > 255f)
				{
					num12 = 255f;
				}
				pixels[i].r = num10;
				pixels[i].g = num11;
				pixels[i].b = num12;
			}
		}
		Image image = Image.createImage(img0.getRealImageWidth(), img0.getRealImageHeight());
		image.texture.SetPixels(pixels);
		Image.setTextureQuality(image.texture);
		image.texture.Apply();
		Cout.LogError2("BLEND ----------------------------------------------------");
		return image;
	}

	public static Color setColorObj(int rgb)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		int num = rgb & 0xFF;
		int num2 = (rgb >> 8) & 0xFF;
		int num3 = (rgb >> 16) & 0xFF;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		return new Color((float)num3 / 256f, num5, num4);
	}

	public void fillTrans(Image imgTrans, int x, int y, int w, int h)
	{
		setColor(0, 0.5f);
		fillRect(x * zoomLevel, y * zoomLevel, w * zoomLevel, h * zoomLevel, isA: false);
	}

	public static int blendColor(float level, int color, int colorBlend)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Color val = setColorObj(colorBlend);
		float num = val.r * 255f;
		float num2 = val.g * 255f;
		float num3 = val.b * 255f;
		Color val2 = setColorObj(color);
		float num4 = (num + val2.r) * level + val2.r;
		float num5 = (num2 + val2.g) * level + val2.g;
		float num6 = (num3 + val2.b) * level + val2.b;
		if (num4 > 255f)
		{
			num4 = 255f;
		}
		if (num4 < 0f)
		{
			num4 = 0f;
		}
		if (num5 > 255f)
		{
			num5 = 255f;
		}
		if (num5 < 0f)
		{
			num5 = 0f;
		}
		if (num6 < 0f)
		{
			num6 = 0f;
		}
		if (num6 > 255f)
		{
			num6 = 255f;
		}
		return (int)num6 & (255 + ((int)num5 << 8)) & (255 + ((int)num4 << 16)) & 0xFF;
	}

	public static int getIntByColor(Color cl)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		float num = cl.r * 255f;
		float num2 = cl.b * 255f;
		float num3 = cl.g * 255f;
		return (((int)num & 0xFF) << 16) | (((int)num3 & 0xFF) << 8) | ((int)num2 & 0xFF);
	}

	public static int getRealImageWidth(Image img)
	{
		return img.w;
	}

	public static int getRealImageHeight(Image img)
	{
		return img.h;
	}

	public void fillArg(int i, int j, int k, int l, int m, int n)
	{
		fillRect(i * zoomLevel, j * zoomLevel, k * zoomLevel, l * zoomLevel, isA: false);
	}

	public void CreateLineMaterial()
	{
		Object.op_Implicit((Object)(object)lineMaterial);
	}

	public void drawlineGL(mVector totalLine)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		lineMaterial.SetPass(0);
		GL.PushMatrix();
		GL.Begin(1);
		for (int i = 0; i < totalLine.size(); i++)
		{
			mLine mLine2 = (mLine)totalLine.elementAt(i);
			GL.Color(new Color(mLine2.r, mLine2.g, mLine2.b, mLine2.a));
			int num = mLine2.x1 * zoomLevel;
			int num2 = mLine2.y1 * zoomLevel;
			int num3 = mLine2.x2 * zoomLevel;
			int num4 = mLine2.y2 * zoomLevel;
			if (isTranslate)
			{
				num += translateX;
				num2 += translateY;
				num3 += translateX;
				num4 += translateY;
			}
			for (int j = 0; j < zoomLevel; j++)
			{
				GL.Vertex(Vector2.op_Implicit(new Vector2((float)(num + j), (float)(num2 + j))));
				GL.Vertex(Vector2.op_Implicit(new Vector2((float)(num3 + j), (float)(num4 + j))));
				if (j > 0)
				{
					GL.Vertex(Vector2.op_Implicit(new Vector2((float)(num + j), (float)num2)));
					GL.Vertex(Vector2.op_Implicit(new Vector2((float)(num3 + j), (float)num4)));
					GL.Vertex(Vector2.op_Implicit(new Vector2((float)num, (float)(num2 + j))));
					GL.Vertex(Vector2.op_Implicit(new Vector2((float)num3, (float)(num4 + j))));
				}
			}
		}
		GL.End();
		GL.PopMatrix();
		totalLine.removeAllElements();
	}

	public void drawLine(mGraphics g, int x, int y, int xTo, int yTo, int nLine, int color)
	{
		mVector mVector2 = new mVector();
		for (int i = 0; i < nLine; i++)
		{
			mVector2.addElement(new mLine(x, y, xTo + i, yTo + i, color));
		}
		g.drawlineGL(mVector2);
	}

	public void saveCanvas()
	{
	}

	public void ClipRec(int x, int i, int win, int hcur)
	{
	}

	public static void resetTransAndroid(mGraphics g2)
	{
	}

	public void restoreCanvas()
	{
	}

	public void fillRecAlpla(int x, int y, int w, int h, int color)
	{
		drawRecAlpa(0, 0, GameCanvas.loadmap.mapW * 24, y, color);
		drawRecAlpa(0, y, x, GameCanvas.loadmap.mapH * 24 - y, color);
		drawRecAlpa(x, y + h, GameCanvas.loadmap.mapW * 24 - x, GameCanvas.loadmap.mapH * 24 - (y + h), color);
		drawRecAlpa(x + w, y, GameCanvas.loadmap.mapW * 24 - (x + w), h, color);
		int num = 100;
		drawRecAlpa(0, -num, GameCanvas.loadmap.mapW * 24, num, color);
	}

	public void drawRecAlpa(int x, int y, int w, int h, int color)
	{
		float alpha = 0.5f;
		setColor(color, alpha);
		fillRect(x, y, w, h, isA: false);
	}
}
