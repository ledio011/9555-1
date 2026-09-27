using System;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000B0 RID: 176
	public struct Mathf
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x00011C1C File Offset: 0x0000FE1C
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00011C28 File Offset: 0x0000FE28
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00011C34 File Offset: 0x0000FE34
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00011C40 File Offset: 0x0000FE40
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00011C4C File Offset: 0x0000FE4C
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00011C58 File Offset: 0x0000FE58
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00011C64 File Offset: 0x0000FE64
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00011C70 File Offset: 0x0000FE70
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00011C84 File Offset: 0x0000FE84
		public static float Min(float a, float b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00011C94 File Offset: 0x0000FE94
		public static float Min(params float[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0f;
			}
			float num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] < num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		public static float Max(float a, float b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00011CF8 File Offset: 0x0000FEF8
		public static float Max(params float[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0f;
			}
			float num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] > num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00011D3C File Offset: 0x0000FF3C
		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00011D58 File Offset: 0x0000FF58
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00011D64 File Offset: 0x0000FF64
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00011D70 File Offset: 0x0000FF70
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00011D7C File Offset: 0x0000FF7C
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00011D88 File Offset: 0x0000FF88
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00011D94 File Offset: 0x0000FF94
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00011DAC File Offset: 0x0000FFAC
		public static float Sign(float f)
		{
			return (f < 0f) ? -1f : 1f;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00011E00 File Offset: 0x00010000
		public static float Clamp01(float value)
		{
			if (value < 0f)
			{
				return 0f;
			}
			if (value > 1f)
			{
				return 1f;
			}
			return value;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00011E28 File Offset: 0x00010028
		public static float Lerp(float from, float to, float t)
		{
			return from + (to - from) * Mathf.Clamp01(t);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00011E38 File Offset: 0x00010038
		public static float LerpAngle(float a, float b, float t)
		{
			float num = Mathf.Repeat(b - a, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return a + num * Mathf.Clamp01(t);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00011E70 File Offset: 0x00010070
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00011EA0 File Offset: 0x000100A0
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), 1.1E-44f);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00011ED0 File Offset: 0x000100D0
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00011EF4 File Offset: 0x000100F4
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			if (num5 - current > 0f == num8 > num5)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00011FA4 File Offset: 0x000101A4
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00011FC4 File Offset: 0x000101C4
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00011FE8 File Offset: 0x000101E8
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00012004 File Offset: 0x00010204
		public static float Repeat(float t, float length)
		{
			return t - Mathf.Floor(t / length) * length;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00012014 File Offset: 0x00010214
		public static float InverseLerp(float from, float to, float value)
		{
			if (from < to)
			{
				if (value < from)
				{
					return 0f;
				}
				if (value > to)
				{
					return 1f;
				}
				value -= from;
				value /= to - from;
				return value;
			}
			else
			{
				if (from <= to)
				{
					return 0f;
				}
				if (value < to)
				{
					return 1f;
				}
				if (value > from)
				{
					return 0f;
				}
				return 1f - (value - to) / (from - to);
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00012084 File Offset: 0x00010284
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return num;
		}

		// Token: 0x040002E4 RID: 740
		public const float PI = 3.1415927f;

		// Token: 0x040002E5 RID: 741
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x040002E6 RID: 742
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x040002E7 RID: 743
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x040002E8 RID: 744
		public const float Rad2Deg = 57.29578f;

		// Token: 0x040002E9 RID: 745
		public const float Epsilon = 1E-45f;
	}
}
