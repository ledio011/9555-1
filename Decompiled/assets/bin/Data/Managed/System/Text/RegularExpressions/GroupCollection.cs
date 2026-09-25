using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class GroupCollection : ICollection, IEnumerable
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000858C File Offset: 0x0000678C
		internal GroupCollection(int n, int gap)
		{
			this.list = new Group[n];
			this.gap = gap;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000085A8 File Offset: 0x000067A8
		public int Count
		{
			get
			{
				return this.list.Length;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000085B4 File Offset: 0x000067B4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700006A RID: 106
		public Group this[int i]
		{
			get
			{
				if (i >= this.gap)
				{
					Match match = (Match)this.list[0];
					i = ((match != Match.Empty) ? match.Regex.GetGroupIndex(i) : -1);
				}
				return (i >= 0) ? this.list[i] : Group.Fail;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008618 File Offset: 0x00006818
		internal void SetValue(Group g, int i)
		{
			this.list[i] = g;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008624 File Offset: 0x00006824
		public object SyncRoot
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000862C File Offset: 0x0000682C
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000863C File Offset: 0x0000683C
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04000985 RID: 2437
		private Group[] list;

		// Token: 0x04000986 RID: 2438
		private int gap;
	}
}
