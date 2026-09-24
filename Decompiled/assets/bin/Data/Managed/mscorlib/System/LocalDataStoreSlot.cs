using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x0200014B RID: 331
	[ComVisible(true)]
	public sealed class LocalDataStoreSlot
	{
		// Token: 0x06000CD0 RID: 3280 RVA: 0x00031CAC File Offset: 0x0002FEAC
		internal LocalDataStoreSlot(bool in_thread)
		{
			this.thread_local = in_thread;
			object obj = LocalDataStoreSlot.lock_obj;
			lock (obj)
			{
				bool[] array;
				if (in_thread)
				{
					array = LocalDataStoreSlot.slot_bitmap_thread;
				}
				else
				{
					array = LocalDataStoreSlot.slot_bitmap_context;
				}
				int i;
				if (array != null)
				{
					for (i = 0; i < array.Length; i++)
					{
						if (!array[i])
						{
							this.slot = i;
							array[i] = true;
							return;
						}
					}
					bool[] array2 = new bool[i + 2];
					array.CopyTo(array2, 0);
					array = array2;
				}
				else
				{
					array = new bool[2];
					i = 0;
				}
				array[i] = true;
				this.slot = i;
				if (in_thread)
				{
					LocalDataStoreSlot.slot_bitmap_thread = array;
				}
				else
				{
					LocalDataStoreSlot.slot_bitmap_context = array;
				}
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00031D88 File Offset: 0x0002FF88
		protected override void Finalize()
		{
			try
			{
				Thread.FreeLocalSlotValues(this.slot, this.thread_local);
				object obj = LocalDataStoreSlot.lock_obj;
				lock (obj)
				{
					if (this.thread_local)
					{
						LocalDataStoreSlot.slot_bitmap_thread[this.slot] = false;
					}
					else
					{
						LocalDataStoreSlot.slot_bitmap_context[this.slot] = false;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04000551 RID: 1361
		internal int slot;

		// Token: 0x04000552 RID: 1362
		internal bool thread_local;

		// Token: 0x04000553 RID: 1363
		private static object lock_obj = new object();

		// Token: 0x04000554 RID: 1364
		private static bool[] slot_bitmap_thread;

		// Token: 0x04000555 RID: 1365
		private static bool[] slot_bitmap_context;
	}
}
