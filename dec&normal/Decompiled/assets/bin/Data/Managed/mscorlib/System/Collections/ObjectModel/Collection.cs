using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
	// Token: 0x020000B0 RID: 176
	[ComVisible(false)]
	[Serializable]
	public class Collection<T> : ICollection<!0>, IEnumerable<!0>, IList<T>, ICollection, IEnumerable, IList
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00018090 File Offset: 0x00016290
		public Collection()
		{
			List<T> list = new List<T>();
			IList list2 = list;
			this.syncRoot = list2.SyncRoot;
			this.list = list;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000180C0 File Offset: 0x000162C0
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000180D0 File Offset: 0x000162D0
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.list).CopyTo(array, index);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000180E4 File Offset: 0x000162E4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000180F4 File Offset: 0x000162F4
		int IList.Add(object value)
		{
			int count = this.list.Count;
			this.InsertItem(count, Collection<T>.ConvertItem(value));
			return count;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001811C File Offset: 0x0001631C
		bool IList.Contains(object value)
		{
			return Collection<T>.IsValidItem(value) && this.list.Contains((T)((object)value));
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001813C File Offset: 0x0001633C
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsValidItem(value))
			{
				return this.list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001815C File Offset: 0x0001635C
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, Collection<T>.ConvertItem(value));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001816C File Offset: 0x0001636C
		void IList.Remove(object value)
		{
			Collection<T>.CheckWritable(this.list);
			int index = this.IndexOf(Collection<T>.ConvertItem(value));
			this.RemoveItem(index);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00018198 File Offset: 0x00016398
		bool ICollection.IsSynchronized
		{
			get
			{
				return Collection<T>.IsSynchronized(this.list);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000181A8 File Offset: 0x000163A8
		object ICollection.SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000181B0 File Offset: 0x000163B0
		bool IList.IsFixedSize
		{
			get
			{
				return Collection<T>.IsFixedSize(this.list);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000181C0 File Offset: 0x000163C0
		bool IList.IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		// Token: 0x170000EC RID: 236
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.SetItem(index, Collection<T>.ConvertItem(value));
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000181F4 File Offset: 0x000163F4
		public void Add(T item)
		{
			int count = this.list.Count;
			this.InsertItem(count, item);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00018218 File Offset: 0x00016418
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00018220 File Offset: 0x00016420
		protected virtual void ClearItems()
		{
			this.list.Clear();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00018230 File Offset: 0x00016430
		public bool Contains(T item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00018240 File Offset: 0x00016440
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00018250 File Offset: 0x00016450
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00018260 File Offset: 0x00016460
		public int IndexOf(T item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00018270 File Offset: 0x00016470
		public void Insert(int index, T item)
		{
			this.InsertItem(index, item);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001827C File Offset: 0x0001647C
		protected virtual void InsertItem(int index, T item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001828C File Offset: 0x0001648C
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num == -1)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000182B4 File Offset: 0x000164B4
		public void RemoveAt(int index)
		{
			this.RemoveItem(index);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000182C0 File Offset: 0x000164C0
		protected virtual void RemoveItem(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000182D0 File Offset: 0x000164D0
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170000EE RID: 238
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000182FC File Offset: 0x000164FC
		protected virtual void SetItem(int index, T item)
		{
			this.list[index] = item;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001830C File Offset: 0x0001650C
		internal static bool IsValidItem(object item)
		{
			return item is T || (item == null && !typeof(T).IsValueType);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00018338 File Offset: 0x00016538
		internal static T ConvertItem(object item)
		{
			if (Collection<T>.IsValidItem(item))
			{
				return (T)((object)item);
			}
			throw new ArgumentException("item");
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00018358 File Offset: 0x00016558
		internal static void CheckWritable(IList<T> list)
		{
			if (list.IsReadOnly)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001836C File Offset: 0x0001656C
		internal static bool IsSynchronized(IList<T> list)
		{
			ICollection collection = list as ICollection;
			return collection != null && collection.IsSynchronized;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00018394 File Offset: 0x00016594
		internal static bool IsFixedSize(IList<T> list)
		{
			IList list2 = list as IList;
			return list2 != null && list2.IsFixedSize;
		}

		// Token: 0x04000232 RID: 562
		private IList<T> list;

		// Token: 0x04000233 RID: 563
		private object syncRoot;
	}
}
