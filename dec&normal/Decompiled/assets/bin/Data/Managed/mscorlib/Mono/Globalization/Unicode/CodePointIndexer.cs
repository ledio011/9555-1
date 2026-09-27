using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200002E RID: 46
	internal class CodePointIndexer
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003A50 File Offset: 0x00001C50
		public CodePointIndexer(int[] starts, int[] ends, int defaultIndex, int defaultCP)
		{
			this.defaultIndex = defaultIndex;
			this.defaultCP = defaultCP;
			this.ranges = new CodePointIndexer.TableRange[starts.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new CodePointIndexer.TableRange(starts[i], ends[i], (i != 0) ? (this.ranges[i - 1].IndexStart + this.ranges[i - 1].Count) : 0);
			}
			for (int j = 0; j < this.ranges.Length; j++)
			{
				this.TotalCount += this.ranges[j].Count;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003B20 File Offset: 0x00001D20
		public int ToIndex(int cp)
		{
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (cp < this.ranges[i].Start)
				{
					return this.defaultIndex;
				}
				if (cp < this.ranges[i].End)
				{
					return cp - this.ranges[i].Start + this.ranges[i].IndexStart;
				}
			}
			return this.defaultIndex;
		}

		// Token: 0x04000074 RID: 116
		private readonly CodePointIndexer.TableRange[] ranges;

		// Token: 0x04000075 RID: 117
		public readonly int TotalCount;

		// Token: 0x04000076 RID: 118
		private int defaultIndex;

		// Token: 0x04000077 RID: 119
		private int defaultCP;

		// Token: 0x0200002F RID: 47
		[Serializable]
		internal struct TableRange
		{
			// Token: 0x0600006C RID: 108 RVA: 0x00003BA8 File Offset: 0x00001DA8
			public TableRange(int start, int end, int indexStart)
			{
				this.Start = start;
				this.End = end;
				this.Count = this.End - this.Start;
				this.IndexStart = indexStart;
				this.IndexEnd = this.IndexStart + this.Count;
			}

			// Token: 0x04000078 RID: 120
			public readonly int Start;

			// Token: 0x04000079 RID: 121
			public readonly int End;

			// Token: 0x0400007A RID: 122
			public readonly int Count;

			// Token: 0x0400007B RID: 123
			public readonly int IndexStart;

			// Token: 0x0400007C RID: 124
			public readonly int IndexEnd;
		}
	}
}
