using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000176 RID: 374
	[ComVisible(true)]
	[Serializable]
	public class Random
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x00037FA8 File Offset: 0x000361A8
		public Random() : this(Environment.TickCount)
		{
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00037FB8 File Offset: 0x000361B8
		public Random(int Seed)
		{
			int num = 161803398 - Math.Abs(Seed);
			this.SeedArray[55] = num;
			int num2 = 1;
			for (int i = 1; i < 55; i++)
			{
				int num3 = 21 * i % 55;
				this.SeedArray[num3] = num2;
				num2 = num - num2;
				if (num2 < 0)
				{
					num2 += int.MaxValue;
				}
				num = this.SeedArray[num3];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					this.SeedArray[k] -= this.SeedArray[1 + (k + 30) % 55];
					if (this.SeedArray[k] < 0)
					{
						this.SeedArray[k] += int.MaxValue;
					}
				}
			}
			this.inext = 0;
			this.inextp = 31;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000380B4 File Offset: 0x000362B4
		protected virtual double Sample()
		{
			if (++this.inext >= 56)
			{
				this.inext = 1;
			}
			if (++this.inextp >= 56)
			{
				this.inextp = 1;
			}
			int num = this.SeedArray[this.inext] - this.SeedArray[this.inextp];
			if (num < 0)
			{
				num += int.MaxValue;
			}
			this.SeedArray[this.inext] = num;
			return (double)num * 4.656612875245797E-10;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00038144 File Offset: 0x00036344
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Min value is greater than max value."));
			}
			uint num = (uint)(maxValue - minValue);
			if (num <= 1U)
			{
				return minValue;
			}
			return (int)((ulong)((uint)(this.Sample() * num)) + (ulong)((long)minValue));
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00038188 File Offset: 0x00036388
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)(this.Sample() * 256.0);
			}
		}

		// Token: 0x040005D7 RID: 1495
		private const int MBIG = 2147483647;

		// Token: 0x040005D8 RID: 1496
		private const int MSEED = 161803398;

		// Token: 0x040005D9 RID: 1497
		private const int MZ = 0;

		// Token: 0x040005DA RID: 1498
		private int inext;

		// Token: 0x040005DB RID: 1499
		private int inextp;

		// Token: 0x040005DC RID: 1500
		private int[] SeedArray = new int[56];
	}
}
