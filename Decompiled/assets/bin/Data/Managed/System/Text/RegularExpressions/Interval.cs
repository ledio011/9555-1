using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200006F RID: 111
	internal struct Interval : IComparable
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000A39C File Offset: 0x0000859C
		public Interval(int low, int high)
		{
			if (low > high)
			{
				int num = low;
				low = high;
				high = num;
			}
			this.low = low;
			this.high = high;
			this.contiguous = true;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A3D0 File Offset: 0x000085D0
		public static Interval Empty
		{
			get
			{
				Interval result;
				result.low = 0;
				result.high = result.low - 1;
				result.contiguous = true;
				return result;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A400 File Offset: 0x00008600
		public bool IsDiscontiguous
		{
			get
			{
				return !this.contiguous;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A40C File Offset: 0x0000860C
		public bool IsSingleton
		{
			get
			{
				return this.contiguous && this.low == this.high;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A42C File Offset: 0x0000862C
		public bool IsEmpty
		{
			get
			{
				return this.low > this.high;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A43C File Offset: 0x0000863C
		public int Size
		{
			get
			{
				if (this.IsEmpty)
				{
					return 0;
				}
				return this.high - this.low + 1;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A45C File Offset: 0x0000865C
		public bool IsDisjoint(Interval i)
		{
			return this.IsEmpty || i.IsEmpty || (this.low > i.high || i.low > this.high);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A4AC File Offset: 0x000086AC
		public bool IsAdjacent(Interval i)
		{
			return !this.IsEmpty && !i.IsEmpty && (this.low == i.high + 1 || this.high == i.low - 1);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A4FC File Offset: 0x000086FC
		public bool Contains(Interval i)
		{
			return (!this.IsEmpty && i.IsEmpty) || (!this.IsEmpty && this.low <= i.low && i.high <= this.high);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A558 File Offset: 0x00008758
		public bool Contains(int i)
		{
			return this.low <= i && i <= this.high;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A578 File Offset: 0x00008778
		public bool Intersects(Interval i)
		{
			return !this.IsEmpty && !i.IsEmpty && ((this.Contains(i.low) && !this.Contains(i.high)) || (this.Contains(i.high) && !this.Contains(i.low)));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A5EC File Offset: 0x000087EC
		public void Merge(Interval i)
		{
			if (i.IsEmpty)
			{
				return;
			}
			if (this.IsEmpty)
			{
				this.low = i.low;
				this.high = i.high;
			}
			if (i.low < this.low)
			{
				this.low = i.low;
			}
			if (i.high > this.high)
			{
				this.high = i.high;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A66C File Offset: 0x0000886C
		public int CompareTo(object o)
		{
			return this.low - ((Interval)o).low;
		}

		// Token: 0x040009A8 RID: 2472
		public int low;

		// Token: 0x040009A9 RID: 2473
		public int high;

		// Token: 0x040009AA RID: 2474
		public bool contiguous;
	}
}
