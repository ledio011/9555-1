using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000343 RID: 835
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x060018FC RID: 6396 RVA: 0x0005BBB8 File Offset: 0x00059DB8
		internal KeyContainerPermissionAccessEntryEnumerator(ArrayList list)
		{
			this.e = list.GetEnumerator();
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0005BBCC File Offset: 0x00059DCC
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0005BBDC File Offset: 0x00059DDC
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0005BBEC File Offset: 0x00059DEC
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x04000D8A RID: 3466
		private IEnumerator e;
	}
}
