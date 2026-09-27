using System;

namespace System.ComponentModel
{
	// Token: 0x0200001A RID: 26
	public sealed class EventHandlerList : IDisposable
	{
		// Token: 0x17000019 RID: 25
		public Delegate this[object key]
		{
			get
			{
				if (key == null)
				{
					return this.null_entry;
				}
				ListEntry listEntry = this.FindEntry(key);
				if (listEntry != null)
				{
					return listEntry.value;
				}
				return null;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002A94 File Offset: 0x00000C94
		public void Dispose()
		{
			this.entries = null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private ListEntry FindEntry(object key)
		{
			for (ListEntry next = this.entries; next != null; next = next.next)
			{
				if (next.key == key)
				{
					return next;
				}
			}
			return null;
		}

		// Token: 0x04000041 RID: 65
		private ListEntry entries;

		// Token: 0x04000042 RID: 66
		private Delegate null_entry;
	}
}
