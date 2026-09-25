using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000342 RID: 834
	[DefaultMember("Item")]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x060018F6 RID: 6390 RVA: 0x0005BB6C File Offset: 0x00059D6C
		internal KeyContainerPermissionAccessEntryCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0005BB80 File Offset: 0x00059D80
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0005BB90 File Offset: 0x00059D90
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this._list);
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0005BBA0 File Offset: 0x00059DA0
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0005BBB0 File Offset: 0x00059DB0
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0005BBB4 File Offset: 0x00059DB4
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000D89 RID: 3465
		private ArrayList _list;
	}
}
