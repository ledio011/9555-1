using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System
{
	// Token: 0x0200014D RID: 333
	public static class Math
	{
		// Token: 0x06000CD9 RID: 3289 RVA: 0x00031EA4 File Offset: 0x000300A4
		public static float Abs(float value)
		{
			return (value >= 0f) ? value : (-value);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00031EBC File Offset: 0x000300BC
		public static int Abs(int value)
		{
			if (value == -2147483648)
			{
				throw new OverflowException(Locale.GetText("Value is too small."));
			}
			return (value >= 0) ? value : (-value);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00031EE8 File Offset: 0x000300E8
		public static long Abs(long value)
		{
			if (value == -9223372036854775808L)
			{
				throw new OverflowException(Locale.GetText("Value is too small."));
			}
			return (value >= 0L) ? value : (-value);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00031F1C File Offset: 0x0003011C
		public static double Ceiling(double a)
		{
			double num = Math.Floor(a);
			if (num != a)
			{
				num += 1.0;
			}
			return num;
		}

		// Token: 0x06000CDD RID: 3293
		[MethodImpl(4096)]
		public static extern double Floor(double d);

		// Token: 0x06000CDE RID: 3294 RVA: 0x00031F44 File Offset: 0x00030144
		public static double Log(double a, double newBase)
		{
			double num = Math.Log(a) / Math.Log(newBase);
			return (num != 0.0) ? num : 0.0;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00031F80 File Offset: 0x00030180
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Max(int val1, int val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00031F90 File Offset: 0x00030190
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static uint Max(uint val1, uint val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00031FA0 File Offset: 0x000301A0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Min(int val1, int val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00031FB0 File Offset: 0x000301B0
		public static decimal Round(decimal d)
		{
			decimal num = decimal.Floor(d);
			decimal d2 = d - num;
			if ((d2 == 0.5m && 2.0m * (num / 2.0m - decimal.Floor(num / 2.0m)) != 0m) || d2 > 0.5m)
			{
				num = ++num;
			}
			return num;
		}

		// Token: 0x06000CE3 RID: 3299
		[MethodImpl(4096)]
		public static extern double Round(double a);

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003204C File Offset: 0x0003024C
		public static double Truncate(double d)
		{
			if (d > 0.0)
			{
				return Math.Floor(d);
			}
			if (d < 0.0)
			{
				return Math.Ceiling(d);
			}
			return d;
		}

		// Token: 0x06000CE5 RID: 3301
		[MethodImpl(4096)]
		public static extern double Sin(double a);

		// Token: 0x06000CE6 RID: 3302
		[MethodImpl(4096)]
		public static extern double Cos(double d);

		// Token: 0x06000CE7 RID: 3303
		[MethodImpl(4096)]
		public static extern double Tan(double a);

		// Token: 0x06000CE8 RID: 3304
		[MethodImpl(4096)]
		public static extern double Acos(double d);

		// Token: 0x06000CE9 RID: 3305
		[MethodImpl(4096)]
		public static extern double Asin(double d);

		// Token: 0x06000CEA RID: 3306
		[MethodImpl(4096)]
		public static extern double Atan2(double y, double x);

		// Token: 0x06000CEB RID: 3307
		[MethodImpl(4096)]
		public static extern double Log(double d);

		// Token: 0x06000CEC RID: 3308
		[MethodImpl(4096)]
		public static extern double Pow(double x, double y);

		// Token: 0x06000CED RID: 3309
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern double Sqrt(double d);

		// Token: 0x04000557 RID: 1367
		public const double E = 2.718281828459045;

		// Token: 0x04000558 RID: 1368
		public const double PI = 3.141592653589793;
	}
}
