using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000070 RID: 112
	internal class IntervalCollection : ICollection, IEnumerable
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000A690 File Offset: 0x00008890
		public IntervalCollection()
		{
			this.intervals = new ArrayList();
		}

		// Token: 0x17000081 RID: 129
		public Interval this[int i]
		{
			get
			{
				return (Interval)this.intervals[i];
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public void Add(Interval i)
		{
			this.intervals.Add(i);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A6CC File Offset: 0x000088CC
		public void Normalize()
		{
			this.intervals.Sort();
			int i = 0;
			while (i < this.intervals.Count - 1)
			{
				Interval interval = (Interval)this.intervals[i];
				Interval i2 = (Interval)this.intervals[i + 1];
				if (!interval.IsDisjoint(i2) || interval.IsAdjacent(i2))
				{
					interval.Merge(i2);
					this.intervals[i] = interval;
					this.intervals.RemoveAt(i + 1);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A770 File Offset: 0x00008970
		public IntervalCollection GetMetaCollection(IntervalCollection.CostDelegate cost_del)
		{
			IntervalCollection intervalCollection = new IntervalCollection();
			this.Normalize();
			this.Optimize(0, this.Count - 1, intervalCollection, cost_del);
			intervalCollection.intervals.Sort();
			return intervalCollection;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A7A8 File Offset: 0x000089A8
		private void Optimize(int begin, int end, IntervalCollection meta, IntervalCollection.CostDelegate cost_del)
		{
			Interval i;
			i.contiguous = false;
			int num = -1;
			int num2 = -1;
			double num3 = 0.0;
			for (int j = begin; j <= end; j++)
			{
				i.low = this[j].low;
				double num4 = 0.0;
				for (int k = j; k <= end; k++)
				{
					i.high = this[k].high;
					num4 += cost_del(this[k]);
					double num5 = cost_del(i);
					if (num5 < num4 && num4 > num3)
					{
						num = j;
						num2 = k;
						num3 = num4;
					}
				}
			}
			if (num < 0)
			{
				for (int l = begin; l <= end; l++)
				{
					meta.Add(this[l]);
				}
			}
			else
			{
				i.low = this[num].low;
				i.high = this[num2].high;
				meta.Add(i);
				if (num > begin)
				{
					this.Optimize(begin, num - 1, meta, cost_del);
				}
				if (num2 < end)
				{
					this.Optimize(num2 + 1, end, meta, cost_del);
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public int Count
		{
			get
			{
				return this.intervals.Count;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000A908 File Offset: 0x00008B08
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000A90C File Offset: 0x00008B0C
		public object SyncRoot
		{
			get
			{
				return this.intervals;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A914 File Offset: 0x00008B14
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this.intervals)
			{
				Interval interval = (Interval)obj;
				if (index > array.Length)
				{
					break;
				}
				array.SetValue(interval, index++);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A994 File Offset: 0x00008B94
		public IEnumerator GetEnumerator()
		{
			return new IntervalCollection.Enumerator(this.intervals);
		}

		// Token: 0x040009AB RID: 2475
		private ArrayList intervals;

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x06000224 RID: 548
		public delegate double CostDelegate(Interval i);

		// Token: 0x02000072 RID: 114
		private class Enumerator : IEnumerator
		{
			// Token: 0x06000227 RID: 551 RVA: 0x0000A9A4 File Offset: 0x00008BA4
			public Enumerator(IList list)
			{
				this.list = list;
				this.Reset();
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A9BC File Offset: 0x00008BBC
			public object Current
			{
				get
				{
					if (this.ptr >= this.list.Count)
					{
						throw new InvalidOperationException();
					}
					return this.list[this.ptr];
				}
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000A9EC File Offset: 0x00008BEC
			public bool MoveNext()
			{
				if (this.ptr > this.list.Count)
				{
					throw new InvalidOperationException();
				}
				return ++this.ptr < this.list.Count;
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000AA34 File Offset: 0x00008C34
			public void Reset()
			{
				this.ptr = -1;
			}

			// Token: 0x040009AC RID: 2476
			private IList list;

			// Token: 0x040009AD RID: 2477
			private int ptr;
		}
	}
}
