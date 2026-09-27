using System;
using System.Collections;
using System.Reflection;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000060 RID: 96
	[DefaultMember("Item")]
	[Serializable]
	public class CaptureCollection : ICollection, IEnumerable
	{
		// Token: 0x06000197 RID: 407 RVA: 0x000073B8 File Offset: 0x000055B8
		internal CaptureCollection(int n)
		{
			this.list = new Capture[n];
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000073CC File Offset: 0x000055CC
		public int Count
		{
			get
			{
				return this.list.Length;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000073D8 File Offset: 0x000055D8
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000073DC File Offset: 0x000055DC
		internal void SetValue(Capture cap, int i)
		{
			this.list[i] = cap;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000073E8 File Offset: 0x000055E8
		public object SyncRoot
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000073F0 File Offset: 0x000055F0
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007400 File Offset: 0x00005600
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x040008EA RID: 2282
		private Capture[] list;
	}
}
