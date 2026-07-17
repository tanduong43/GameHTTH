namespace hqx;

public class Hqx_2x : Hqx
{
	public static void hq2x_32_rb(int[] sp, int[] dp, int Xres, int Yres)
	{
		hq2x_32_rb(sp, dp, Xres, Yres, 48, 7, 6, 0, wrapX: false, wrapY: false);
	}

	public static void hq2x_32_rb(int[] sp, int[] dp, int Xres, int Yres, int trY, int trU, int trV, int trA, bool wrapX, bool wrapY)
	{
		int num = 0;
		int num2 = 0;
		trY <<= 16;
		trU <<= 8;
		int num3 = Xres * 2;
		int[] array = new int[9];
		for (int i = 0; i < Yres; i++)
		{
			int num4 = ((i > 0) ? (-Xres) : (wrapY ? (Xres * (Yres - 1)) : 0));
			int num5 = ((i < Yres - 1) ? Xres : (wrapY ? (-(Xres * (Yres - 1))) : 0));
			for (int j = 0; j < Xres; j++)
			{
				array[1] = sp[num + num4];
				array[4] = sp[num];
				array[7] = sp[num + num5];
				if (j > 0)
				{
					array[0] = sp[num + num4 - 1];
					array[3] = sp[num - 1];
					array[6] = sp[num + num5 - 1];
				}
				else if (wrapX)
				{
					array[0] = sp[num + num4 + Xres - 1];
					array[3] = sp[num + Xres - 1];
					array[6] = sp[num + num5 + Xres - 1];
				}
				else
				{
					array[0] = array[1];
					array[3] = array[4];
					array[6] = array[7];
				}
				if (j < Xres - 1)
				{
					array[2] = sp[num + num4 + 1];
					array[5] = sp[num + 1];
					array[8] = sp[num + num5 + 1];
				}
				else if (wrapX)
				{
					array[2] = sp[num + num4 - Xres + 1];
					array[5] = sp[num - Xres + 1];
					array[8] = sp[num + num5 - Xres + 1];
				}
				else
				{
					array[2] = array[1];
					array[5] = array[4];
					array[8] = array[7];
				}
				int num6 = 0;
				int num7 = 1;
				for (int k = 0; k < 9; k++)
				{
					if (k != 4)
					{
						if (array[k] != array[4] && Hqx.diff(array[4], array[k], trY, trU, trV, trA))
						{
							num6 |= num7;
						}
						num7 <<= 1;
					}
				}
				switch (num6)
				{
				case 0:
				case 1:
				case 4:
				case 5:
				case 32:
				case 33:
				case 36:
				case 37:
				case 128:
				case 129:
				case 132:
				case 133:
				case 160:
				case 161:
				case 164:
				case 165:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 2:
				case 34:
				case 130:
				case 162:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 16:
				case 17:
				case 48:
				case 49:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 64:
				case 65:
				case 68:
				case 69:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 8:
				case 12:
				case 136:
				case 140:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 3:
				case 35:
				case 131:
				case 163:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 6:
				case 38:
				case 134:
				case 166:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 20:
				case 21:
				case 52:
				case 53:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 144:
				case 145:
				case 176:
				case 177:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 192:
				case 193:
				case 196:
				case 197:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 96:
				case 97:
				case 100:
				case 101:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 40:
				case 44:
				case 168:
				case 172:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 9:
				case 13:
				case 137:
				case 141:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 18:
				case 50:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 80:
				case 81:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 72:
				case 76:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 10:
				case 138:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 66:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 24:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 7:
				case 39:
				case 135:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 148:
				case 149:
				case 180:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 224:
				case 225:
				case 228:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 41:
				case 45:
				case 169:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 22:
				case 54:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 208:
				case 209:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 104:
				case 108:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 11:
				case 139:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 19:
				case 51:
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2] = Interpolation.Mix4To2To1(array[4], array[1], array[3]);
						dp[num2 + 1] = Interpolation.Mix2To3To3(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 146:
				case 178:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To3To3(array[4], array[1], array[5]);
						dp[num2 + num3 + 1] = Interpolation.Mix4To2To1(array[4], array[5], array[7]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					break;
				case 84:
				case 85:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix4To2To1(array[4], array[5], array[1]);
						dp[num2 + num3 + 1] = Interpolation.Mix2To3To3(array[4], array[5], array[7]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					break;
				case 112:
				case 113:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix4To2To1(array[4], array[7], array[3]);
						dp[num2 + num3 + 1] = Interpolation.Mix2To3To3(array[4], array[5], array[7]);
					}
					break;
				case 200:
				case 204:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To3To3(array[4], array[7], array[3]);
						dp[num2 + num3 + 1] = Interpolation.Mix4To2To1(array[4], array[7], array[5]);
					}
					break;
				case 73:
				case 77:
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2] = Interpolation.Mix4To2To1(array[4], array[3], array[1]);
						dp[num2 + num3] = Interpolation.Mix2To3To3(array[4], array[7], array[3]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 42:
				case 170:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					}
					else
					{
						dp[num2] = Interpolation.Mix2To3To3(array[4], array[3], array[1]);
						dp[num2 + num3] = Interpolation.Mix4To2To1(array[4], array[3], array[7]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 14:
				case 142:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					}
					else
					{
						dp[num2] = Interpolation.Mix2To3To3(array[4], array[3], array[1]);
						dp[num2 + 1] = Interpolation.Mix4To2To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 67:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 70:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 28:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 152:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 194:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 98:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 56:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 25:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 26:
				case 31:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 82:
				case 214:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 88:
				case 248:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 74:
				case 107:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 27:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 86:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 216:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 106:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 30:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 210:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 120:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 75:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 29:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 198:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 184:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 99:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 57:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 71:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 156:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 226:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 60:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 195:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 102:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 153:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 58:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 83:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 92:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 202:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 78:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 154:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 114:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 89:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 90:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 23:
				case 55:
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix4To2To1(array[4], array[1], array[3]);
						dp[num2 + 1] = Interpolation.Mix2To3To3(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 150:
				case 182:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To3To3(array[4], array[1], array[5]);
						dp[num2 + num3 + 1] = Interpolation.Mix4To2To1(array[4], array[5], array[7]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					break;
				case 212:
				case 213:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix4To2To1(array[4], array[5], array[1]);
						dp[num2 + num3 + 1] = Interpolation.Mix2To3To3(array[4], array[5], array[7]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					break;
				case 240:
				case 241:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix4To2To1(array[4], array[7], array[3]);
						dp[num2 + num3 + 1] = Interpolation.Mix2To3To3(array[4], array[5], array[7]);
					}
					break;
				case 232:
				case 236:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To3To3(array[4], array[7], array[3]);
						dp[num2 + num3 + 1] = Interpolation.Mix4To2To1(array[4], array[7], array[5]);
					}
					break;
				case 105:
				case 109:
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix4To2To1(array[4], array[3], array[1]);
						dp[num2 + num3] = Interpolation.Mix2To3To3(array[4], array[7], array[3]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 43:
				case 171:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					}
					else
					{
						dp[num2] = Interpolation.Mix2To3To3(array[4], array[3], array[1]);
						dp[num2 + num3] = Interpolation.Mix4To2To1(array[4], array[3], array[7]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 15:
				case 143:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					}
					else
					{
						dp[num2] = Interpolation.Mix2To3To3(array[4], array[3], array[1]);
						dp[num2 + 1] = Interpolation.Mix4To2To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 124:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 203:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 62:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 211:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 118:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 217:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 110:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 155:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 188:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 185:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 61:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 157:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 103:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 227:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 230:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 199:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 220:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 158:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 234:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 242:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 59:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 121:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 87:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 79:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 122:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 94:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 218:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 91:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 229:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 167:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 173:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 181:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 186:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 115:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 93:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 206:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 201:
				case 205:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix6To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 46:
				case 174:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					}
					else
					{
						dp[num2] = Interpolation.Mix6To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 147:
				case 179:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix6To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 116:
				case 117:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix6To1To1(array[4], array[5], array[7]);
					}
					break;
				case 189:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 231:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 126:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 219:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 125:
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix4To2To1(array[4], array[3], array[1]);
						dp[num2 + num3] = Interpolation.Mix2To3To3(array[4], array[7], array[3]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 221:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix4To2To1(array[4], array[5], array[1]);
						dp[num2 + num3 + 1] = Interpolation.Mix2To3To3(array[4], array[5], array[7]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					break;
				case 207:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
						dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					}
					else
					{
						dp[num2] = Interpolation.Mix2To3To3(array[4], array[3], array[1]);
						dp[num2 + 1] = Interpolation.Mix4To2To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 238:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To3To3(array[4], array[7], array[3]);
						dp[num2 + num3 + 1] = Interpolation.Mix4To2To1(array[4], array[7], array[5]);
					}
					break;
				case 190:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
						dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To3To3(array[4], array[1], array[5]);
						dp[num2 + num3 + 1] = Interpolation.Mix4To2To1(array[4], array[5], array[7]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 187:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					}
					else
					{
						dp[num2] = Interpolation.Mix2To3To3(array[4], array[3], array[1]);
						dp[num2 + num3] = Interpolation.Mix4To2To1(array[4], array[3], array[7]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 243:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix4To2To1(array[4], array[7], array[3]);
						dp[num2 + num3 + 1] = Interpolation.Mix2To3To3(array[4], array[5], array[7]);
					}
					break;
				case 119:
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix4To2To1(array[4], array[1], array[3]);
						dp[num2 + 1] = Interpolation.Mix2To3To3(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 233:
				case 237:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix14To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 47:
				case 175:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix14To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					break;
				case 151:
				case 183:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix14To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 244:
				case 245:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix14To1To1(array[4], array[5], array[7]);
					}
					break;
				case 250:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 123:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 95:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 222:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 252:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix14To1To1(array[4], array[5], array[7]);
					}
					break;
				case 249:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix14To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 235:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[2], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix14To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 111:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix14To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[5]);
					break;
				case 63:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix14To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[8], array[7]);
					break;
				case 159:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix14To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 215:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix14To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[6], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 246:
					dp[num2] = Interpolation.Mix2To1To1(array[4], array[0], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix14To1To1(array[4], array[5], array[7]);
					}
					break;
				case 254:
					dp[num2] = Interpolation.Mix3To1(array[4], array[0]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix14To1To1(array[4], array[5], array[7]);
					}
					break;
				case 253:
					dp[num2] = Interpolation.Mix3To1(array[4], array[1]);
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[1]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix14To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix14To1To1(array[4], array[5], array[7]);
					}
					break;
				case 251:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[2]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix14To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 239:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix14To1To1(array[4], array[3], array[1]);
					}
					dp[num2 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix14To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[5]);
					break;
				case 127:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix14To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix2To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix2To1To1(array[4], array[7], array[3]);
					}
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[8]);
					break;
				case 191:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix14To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix14To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[7]);
					dp[num2 + num3 + 1] = Interpolation.Mix3To1(array[4], array[7]);
					break;
				case 223:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix2To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix14To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[6]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix2To1To1(array[4], array[5], array[7]);
					}
					break;
				case 247:
					dp[num2] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix14To1To1(array[4], array[1], array[5]);
					}
					dp[num2 + num3] = Interpolation.Mix3To1(array[4], array[3]);
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix14To1To1(array[4], array[5], array[7]);
					}
					break;
				case 255:
					if (Hqx.diff(array[3], array[1], trY, trU, trV, trA))
					{
						dp[num2] = array[4];
					}
					else
					{
						dp[num2] = Interpolation.Mix14To1To1(array[4], array[3], array[1]);
					}
					if (Hqx.diff(array[1], array[5], trY, trU, trV, trA))
					{
						dp[num2 + 1] = array[4];
					}
					else
					{
						dp[num2 + 1] = Interpolation.Mix14To1To1(array[4], array[1], array[5]);
					}
					if (Hqx.diff(array[7], array[3], trY, trU, trV, trA))
					{
						dp[num2 + num3] = array[4];
					}
					else
					{
						dp[num2 + num3] = Interpolation.Mix14To1To1(array[4], array[7], array[3]);
					}
					if (Hqx.diff(array[5], array[7], trY, trU, trV, trA))
					{
						dp[num2 + num3 + 1] = array[4];
					}
					else
					{
						dp[num2 + num3 + 1] = Interpolation.Mix14To1To1(array[4], array[5], array[7]);
					}
					break;
				}
				num++;
				num2 += 2;
			}
			num2 += num3;
		}
	}
}
