using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200007F RID: 127
	[ComVisible(true)]
	[Serializable]
	public abstract class CollectionBase : ICollection, IEnumerable, IList
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00014334 File Offset: 0x00012534
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00014344 File Offset: 0x00012544
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00014354 File Offset: 0x00012554
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00014364 File Offset: 0x00012564
		int IList.Add(object value)
		{
			this.OnValidate(value);
			int count = this.InnerList.Count;
			this.OnInsert(count, value);
			this.InnerList.Add(value);
			try
			{
				this.OnInsertComplete(count, value);
			}
			catch
			{
				this.InnerList.RemoveAt(count);
				throw;
			}
			return count;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000143CC File Offset: 0x000125CC
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000143DC File Offset: 0x000125DC
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000143EC File Offset: 0x000125EC
		void IList.Insert(int index, object value)
		{
			this.OnValidate(value);
			this.OnInsert(index, value);
			this.InnerList.Insert(index, value);
			try
			{
				this.OnInsertComplete(index, value);
			}
			catch
			{
				this.InnerList.RemoveAt(index);
				throw;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014448 File Offset: 0x00012648
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			int num = this.InnerList.IndexOf(value);
			if (num == -1)
			{
				throw new ArgumentException("The element cannot be found.", "value");
			}
			this.OnRemove(num, value);
			this.InnerList.Remove(value);
			this.OnRemoveComplete(num, value);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0001449C File Offset: 0x0001269C
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x000144AC File Offset: 0x000126AC
		bool IList.IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		// Token: 0x1700007F RID: 127
		object IList.this[int index]
		{
			get
			{
				return this.InnerList[index];
			}
			set
			{
				if (index < 0 || index >= this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.OnValidate(value);
				object obj = this.InnerList[index];
				this.OnSet(index, obj, value);
				this.InnerList[index] = value;
				try
				{
					this.OnSetComplete(index, obj, value);
				}
				catch
				{
					this.InnerList[index] = obj;
					throw;
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001455C File Offset: 0x0001275C
		public int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001456C File Offset: 0x0001276C
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001457C File Offset: 0x0001277C
		public void Clear()
		{
			this.OnClear();
			this.InnerList.Clear();
			this.OnClearComplete();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00014598 File Offset: 0x00012798
		public void RemoveAt(int index)
		{
			object value = this.InnerList[index];
			this.OnValidate(value);
			this.OnRemove(index, value);
			this.InnerList.RemoveAt(index);
			this.OnRemoveComplete(index, value);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000145D8 File Offset: 0x000127D8
		protected ArrayList InnerList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000145F8 File Offset: 0x000127F8
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000145FC File Offset: 0x000127FC
		protected virtual void OnClear()
		{
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014600 File Offset: 0x00012800
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014604 File Offset: 0x00012804
		protected virtual void OnInsert(int index, object value)
		{
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014608 File Offset: 0x00012808
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001460C File Offset: 0x0001280C
		protected virtual void OnRemove(int index, object value)
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014610 File Offset: 0x00012810
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014614 File Offset: 0x00012814
		protected virtual void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00014618 File Offset: 0x00012818
		protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001461C File Offset: 0x0001281C
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("CollectionBase.OnValidate: Invalid parameter value passed to method: null");
			}
		}

		// Token: 0x040001E1 RID: 481
		private ArrayList list;
	}
}
