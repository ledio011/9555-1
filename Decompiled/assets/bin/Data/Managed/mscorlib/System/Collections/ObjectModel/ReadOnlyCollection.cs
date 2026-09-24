using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x020000B1 RID: 177
	[ComVisible(false)]
	[Serializable]
	public class ReadOnlyCollection<T> : ICollection<!0>, IEnumerable<!0>, IList<T>, ICollection, IEnumerable, IList
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x000183BC File Offset: 0x000165BC
		public ReadOnlyCollection(IList<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.list = list;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000183DC File Offset: 0x000165DC
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000183E4 File Offset: 0x000165E4
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000183EC File Offset: 0x000165EC
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000183F4 File Offset: 0x000165F4
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000183FC File Offset: 0x000165FC
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000EF RID: 239
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00018418 File Offset: 0x00016618
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001841C File Offset: 0x0001661C
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.list).CopyTo(array, index);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00018430 File Offset: 0x00016630
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00018440 File Offset: 0x00016640
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00018448 File Offset: 0x00016648
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018450 File Offset: 0x00016650
		bool IList.Contains(object value)
		{
			return Collection<T>.IsValidItem(value) && this.list.Contains((T)((object)value));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00018470 File Offset: 0x00016670
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsValidItem(value))
			{
				return this.list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00018490 File Offset: 0x00016690
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00018498 File Offset: 0x00016698
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000184A0 File Offset: 0x000166A0
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000184A8 File Offset: 0x000166A8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000184AC File Offset: 0x000166AC
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000184B0 File Offset: 0x000166B0
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000184B4 File Offset: 0x000166B4
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F5 RID: 245
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000184D4 File Offset: 0x000166D4
		public bool Contains(T value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000184E4 File Offset: 0x000166E4
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000184F4 File Offset: 0x000166F4
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00018504 File Offset: 0x00016704
		public int IndexOf(T value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00018514 File Offset: 0x00016714
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170000F7 RID: 247
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x04000234 RID: 564
		private IList<T> list;
	}
}
