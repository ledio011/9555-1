using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200009D RID: 157
	[DebuggerDisplay("Count={Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView<>))]
	[Serializable]
	public class List<T> : ICollection<T>, IEnumerable<T>, IList<T>, ICollection, IEnumerable, IList
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x000161A0 File Offset: 0x000143A0
		public List()
		{
			this._items = List<T>.EmptyArray;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000161B4 File Offset: 0x000143B4
		public List(IEnumerable<T> collection)
		{
			this.CheckCollection(collection);
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 == null)
			{
				this._items = List<T>.EmptyArray;
				this.AddEnumerable(collection);
			}
			else
			{
				this._items = new T[collection2.Count];
				this.AddCollection(collection2);
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001620C File Offset: 0x0001440C
		public List(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this._items = new T[capacity];
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00016244 File Offset: 0x00014444
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00016254 File Offset: 0x00014454
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001626C File Offset: 0x0001446C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001627C File Offset: 0x0001447C
		int IList.Add(object item)
		{
			try
			{
				this.Add((T)((object)item));
				return this._size - 1;
			}
			catch (NullReferenceException)
			{
			}
			catch (InvalidCastException)
			{
			}
			throw new ArgumentException("item");
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000162DC File Offset: 0x000144DC
		bool IList.Contains(object item)
		{
			try
			{
				return this.Contains((T)((object)item));
			}
			catch (NullReferenceException)
			{
			}
			catch (InvalidCastException)
			{
			}
			return false;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001632C File Offset: 0x0001452C
		int IList.IndexOf(object item)
		{
			try
			{
				return this.IndexOf((T)((object)item));
			}
			catch (NullReferenceException)
			{
			}
			catch (InvalidCastException)
			{
			}
			return -1;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001637C File Offset: 0x0001457C
		void IList.Insert(int index, object item)
		{
			this.CheckIndex(index);
			try
			{
				this.Insert(index, (T)((object)item));
				return;
			}
			catch (NullReferenceException)
			{
			}
			catch (InvalidCastException)
			{
			}
			throw new ArgumentException("item");
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000163DC File Offset: 0x000145DC
		void IList.Remove(object item)
		{
			try
			{
				this.Remove((T)((object)item));
			}
			catch (NullReferenceException)
			{
			}
			catch (InvalidCastException)
			{
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00016428 File Offset: 0x00014628
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001642C File Offset: 0x0001462C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00016430 File Offset: 0x00014630
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00016434 File Offset: 0x00014634
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00016438 File Offset: 0x00014638
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B6 RID: 182
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				try
				{
					this[index] = (T)((object)value);
					return;
				}
				catch (NullReferenceException)
				{
				}
				catch (InvalidCastException)
				{
				}
				throw new ArgumentException("value");
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000164A4 File Offset: 0x000146A4
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.GrowIfNeeded(1);
			}
			this._items[this._size++] = item;
			this._version++;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000164F8 File Offset: 0x000146F8
		private void GrowIfNeeded(int newCount)
		{
			int num = this._size + newCount;
			if (num > this._items.Length)
			{
				this.Capacity = Math.Max(Math.Max(this.Capacity * 2, 4), num);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00016538 File Offset: 0x00014738
		private void CheckRange(int idx, int count)
		{
			if (idx < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (idx + count > this._size)
			{
				throw new ArgumentException("index and count exceed length of list");
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00016578 File Offset: 0x00014778
		private void AddCollection(ICollection<T> collection)
		{
			int count = collection.Count;
			if (count == 0)
			{
				return;
			}
			this.GrowIfNeeded(count);
			collection.CopyTo(this._items, this._size);
			this._size += count;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000165BC File Offset: 0x000147BC
		private void AddEnumerable(IEnumerable<T> enumerable)
		{
			foreach (T item in enumerable)
			{
				this.Add(item);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016610 File Offset: 0x00014810
		public void AddRange(IEnumerable<T> collection)
		{
			this.CheckCollection(collection);
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				this.AddCollection(collection2);
			}
			else
			{
				this.AddEnumerable(collection);
			}
			this._version++;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016654 File Offset: 0x00014854
		public void Clear()
		{
			Array.Clear(this._items, 0, this._items.Length);
			this._size = 0;
			this._version++;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016680 File Offset: 0x00014880
		public bool Contains(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size) != -1;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001669C File Offset: 0x0001489C
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000166B4 File Offset: 0x000148B4
		public T Find(Predicate<T> match)
		{
			List<T>.CheckMatch(match);
			int index = this.GetIndex(0, this._size, match);
			return (index == -1) ? default(T) : this._items[index];
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000166F8 File Offset: 0x000148F8
		private static void CheckMatch(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001670C File Offset: 0x0001490C
		private int GetIndex(int startIndex, int count, Predicate<T> match)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001674C File Offset: 0x0001494C
		public List<T>.Enumerator GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00016754 File Offset: 0x00014954
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001676C File Offset: 0x0001496C
		private void Shift(int start, int delta)
		{
			if (delta < 0)
			{
				start -= delta;
			}
			if (start < this._size)
			{
				Array.Copy(this._items, start, this._items, start + delta, this._size - start);
			}
			this._size += delta;
			if (delta < 0)
			{
				Array.Clear(this._items, this._size, -delta);
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000167D8 File Offset: 0x000149D8
		private void CheckIndex(int index)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000167F8 File Offset: 0x000149F8
		public void Insert(int index, T item)
		{
			this.CheckIndex(index);
			if (this._size == this._items.Length)
			{
				this.GrowIfNeeded(1);
			}
			this.Shift(index, 1);
			this._items[index] = item;
			this._version++;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001684C File Offset: 0x00014A4C
		private void CheckCollection(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00016860 File Offset: 0x00014A60
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
			return num != -1;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001688C File Offset: 0x00014A8C
		public int RemoveAll(Predicate<T> match)
		{
			List<T>.CheckMatch(match);
			int i;
			for (i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					break;
				}
			}
			if (i == this._size)
			{
				return 0;
			}
			this._version++;
			int j;
			for (j = i + 1; j < this._size; j++)
			{
				if (!match(this._items[j]))
				{
					this._items[i++] = this._items[j];
				}
			}
			if (j - i > 0)
			{
				Array.Clear(this._items, i, j - i);
			}
			this._size = i;
			return j - i;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016960 File Offset: 0x00014B60
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.Shift(index, -1);
			Array.Clear(this._items, this._size, 1);
			this._version++;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000169B4 File Offset: 0x00014BB4
		public void RemoveRange(int index, int count)
		{
			this.CheckRange(index, count);
			if (count > 0)
			{
				this.Shift(index, -count);
				Array.Clear(this._items, this._size, count);
				this._version++;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000169F0 File Offset: 0x00014BF0
		public void Sort()
		{
			Array.Sort<T>(this._items, 0, this._size, Comparer<T>.Default);
			this._version++;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016A18 File Offset: 0x00014C18
		public void Sort(Comparison<T> comparison)
		{
			Array.Sort<T>(this._items, this._size, comparison);
			this._version++;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00016A3C File Offset: 0x00014C3C
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, array, this._size);
			return array;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00016A68 File Offset: 0x00014C68
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x00016A74 File Offset: 0x00014C74
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException();
				}
				Array.Resize<T>(ref this._items, value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00016A94 File Offset: 0x00014C94
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170000B9 RID: 185
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._items[index];
			}
			set
			{
				this.CheckIndex(index);
				if (index == this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._items[index] = value;
			}
		}

		// Token: 0x04000209 RID: 521
		private const int DefaultCapacity = 4;

		// Token: 0x0400020A RID: 522
		private T[] _items;

		// Token: 0x0400020B RID: 523
		private int _size;

		// Token: 0x0400020C RID: 524
		private int _version;

		// Token: 0x0400020D RID: 525
		private static readonly T[] EmptyArray = new T[0];

		// Token: 0x0200009E RID: 158
		[Serializable]
		public struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x06000555 RID: 1365 RVA: 0x00016AF4 File Offset: 0x00014CF4
			internal Enumerator(List<T> l)
			{
				this.l = l;
				this.ver = l._version;
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x00016B0C File Offset: 0x00014D0C
			void IEnumerator.Reset()
			{
				this.VerifyState();
				this.next = 0;
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x00016B1C File Offset: 0x00014D1C
			object IEnumerator.Current
			{
				get
				{
					this.VerifyState();
					if (this.next <= 0)
					{
						throw new InvalidOperationException();
					}
					return this.current;
				}
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x00016B44 File Offset: 0x00014D44
			public void Dispose()
			{
				this.l = null;
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x00016B50 File Offset: 0x00014D50
			private void VerifyState()
			{
				if (this.l == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.ver != this.l._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x00016BA4 File Offset: 0x00014DA4
			public bool MoveNext()
			{
				this.VerifyState();
				if (this.next < 0)
				{
					return false;
				}
				if (this.next < this.l._size)
				{
					this.current = this.l._items[this.next++];
					return true;
				}
				this.next = -1;
				return false;
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600055B RID: 1371 RVA: 0x00016C0C File Offset: 0x00014E0C
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x0400020E RID: 526
			private List<T> l;

			// Token: 0x0400020F RID: 527
			private int next;

			// Token: 0x04000210 RID: 528
			private int ver;

			// Token: 0x04000211 RID: 529
			private T current;
		}
	}
}
